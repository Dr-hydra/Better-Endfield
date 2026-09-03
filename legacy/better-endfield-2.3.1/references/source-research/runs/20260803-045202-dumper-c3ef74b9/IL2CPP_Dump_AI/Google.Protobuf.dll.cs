// ========================================================
// Dumped by @desirepro
// Assembly: Google.Protobuf.dll
// Classes:  318
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

CLASS: <FromStreamAsyncCore>d__0
TYPE:  sealed struct
TOKEN: 0x2000009
SIZE:  0x50
IMPLEMENTS: System.Runtime.CompilerServices.IAsyncStateMachine
FIELDS:
  public            System.Int32                    <>1__state  // 0x10
  public            System.Runtime.CompilerServices.AsyncTaskMethodBuilder<Google.Protobuf.ByteString><>t__builder  // 0x18
  public            System.IO.Stream                stream  // 0x30
  public            System.Threading.CancellationTokencancellationToken  // 0x38
  private           System.IO.MemoryStream          <memoryStream>5__2  // 0x40
  private           System.Runtime.CompilerServices.TaskAwaiter<>u__1  // 0x48
METHODS:
  RVA=0x09AE1984  token=0x6000035  System.Void MoveNext()
  RVA=0x09AE1CF0  token=0x6000036  System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine)
END_CLASS

CLASS: OutOfSpaceException
TYPE:  sealed class
TOKEN: 0x200000C
SIZE:  0x90
EXTENDS: System.IO.IOException
FIELDS:
METHODS:
  RVA=0x09AE0AFC  token=0x60000B7  System.Void .ctor()
END_CLASS

CLASS: ExtensionComparer
TYPE:  sealed class
TOKEN: 0x2000011
SIZE:  0x10
IMPLEMENTS: System.Collections.Generic.IEqualityComparer`1
FIELDS:
  private   static  Google.Protobuf.ExtensionRegistry.ExtensionComparerInstance  // static @ 0x0
METHODS:
  RVA=0x09ACF470  token=0x60000D4  System.Boolean Equals(Google.Protobuf.Extension a, Google.Protobuf.Extension b)
  RVA=0x09ACF544  token=0x60000D5  System.Int32 GetHashCode(Google.Protobuf.Extension a)
  RVA=0x041E1670  token=0x60000D6  System.Void .ctor()
  RVA=0x09ACF5CC  token=0x60000D7  System.Void .cctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000012
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.ExtensionRegistry.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Collections.Generic.KeyValuePair<Google.Protobuf.ObjectIntPair<System.Type>,Google.Protobuf.Extension>,Google.Protobuf.ObjectIntPair<System.Type>><>9__3_0  // static @ 0x8
  public    static  System.Func<System.Collections.Generic.KeyValuePair<Google.Protobuf.ObjectIntPair<System.Type>,Google.Protobuf.Extension>,Google.Protobuf.Extension><>9__3_1  // static @ 0x10
METHODS:
  RVA=0x09AE25C4  token=0x60000D8  System.Void .cctor()
  RVA=0x041E1670  token=0x60000D9  System.Void .ctor()
  RVA=0x09AE22A0  token=0x60000DA  Google.Protobuf.ObjectIntPair<System.Type> <.ctor>b__3_0(System.Collections.Generic.KeyValuePair<Google.Protobuf.ObjectIntPair<System.Type>,Google.Protobuf.Extension> k)
  RVA=0x09AE22E0  token=0x60000DB  Google.Protobuf.Extension <.ctor>b__3_1(System.Collections.Generic.KeyValuePair<Google.Protobuf.ObjectIntPair<System.Type>,Google.Protobuf.Extension> v)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000015
FIELDS:
  public    static readonly Google.Protobuf.ExtensionSet.<>c<TTarget><>9  // static @ 0x0
  public    static  System.Func<Google.Protobuf.IExtensionValue,System.Boolean><>9__8_0  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60000F0  System.Void .cctor()
  RVA=-1  // not resolved  token=0x60000F1  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60000F2  System.Boolean <IsInitialized>b__8_0(Google.Protobuf.IExtensionValue v)
END_CLASS

CLASS: <>c__DisplayClass3_0`1
TYPE:  sealed class
TOKEN: 0x200001B
FIELDS:
  public            Google.Protobuf.FieldCodec<T>   nestedCoded  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600013A  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600013B  System.Nullable<T> <GetReader>b__0(Google.Protobuf.ParseContext& ctx)
END_CLASS

CLASS: WrapperCodecs
TYPE:  static class
TOKEN: 0x200001A
SIZE:  0x10
FIELDS:
  private   static readonly System.Collections.Generic.Dictionary<System.Type,System.Object>Codecs  // static @ 0x0
  private   static readonly System.Collections.Generic.Dictionary<System.Type,System.Object>Readers  // static @ 0x8
METHODS:
  RVA=-1  // generic def  token=0x6000134  Google.Protobuf.FieldCodec<T> GetCodec()
  RVA=-1  // generic def  token=0x6000135  Google.Protobuf.ValueReader<System.Nullable<T>> GetReader()
  RVA=-1  // generic def  token=0x6000136  T Read(Google.Protobuf.ParseContext& ctx, Google.Protobuf.FieldCodec<T> codec)
  RVA=-1  // generic def  token=0x6000137  System.Void Write(Google.Protobuf.WriteContext& ctx, T value, Google.Protobuf.FieldCodec<T> codec)
  RVA=-1  // generic def  token=0x6000138  System.Int32 CalculateSize(T value, Google.Protobuf.FieldCodec<T> codec)
  RVA=0x09AE268C  token=0x6000139  System.Void .cctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200001C
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.FieldCodec.<>c  <>9  // static @ 0x0
  public    static  Google.Protobuf.ValueReader<System.String><>9__16_0  // static @ 0x8
  public    static  Google.Protobuf.ValueWriter<System.String><>9__16_1  // static @ 0x10
  public    static  Google.Protobuf.ValueReader<Google.Protobuf.ByteString><>9__17_0  // static @ 0x18
  public    static  Google.Protobuf.ValueWriter<Google.Protobuf.ByteString><>9__17_1  // static @ 0x20
  public    static  Google.Protobuf.ValueReader<System.Boolean><>9__18_0  // static @ 0x28
  public    static  Google.Protobuf.ValueWriter<System.Boolean><>9__18_1  // static @ 0x30
  public    static  Google.Protobuf.ValueReader<System.Int32><>9__19_0  // static @ 0x38
  public    static  Google.Protobuf.ValueWriter<System.Int32><>9__19_1  // static @ 0x40
  public    static  Google.Protobuf.ValueReader<System.Int32><>9__20_0  // static @ 0x48
  public    static  Google.Protobuf.ValueWriter<System.Int32><>9__20_1  // static @ 0x50
  public    static  Google.Protobuf.ValueReader<System.UInt32><>9__21_0  // static @ 0x58
  public    static  Google.Protobuf.ValueWriter<System.UInt32><>9__21_1  // static @ 0x60
  public    static  Google.Protobuf.ValueReader<System.Int32><>9__22_0  // static @ 0x68
  public    static  Google.Protobuf.ValueWriter<System.Int32><>9__22_1  // static @ 0x70
  public    static  Google.Protobuf.ValueReader<System.UInt32><>9__23_0  // static @ 0x78
  public    static  Google.Protobuf.ValueWriter<System.UInt32><>9__23_1  // static @ 0x80
  public    static  Google.Protobuf.ValueReader<System.Int64><>9__24_0  // static @ 0x88
  public    static  Google.Protobuf.ValueWriter<System.Int64><>9__24_1  // static @ 0x90
  public    static  Google.Protobuf.ValueReader<System.Int64><>9__25_0  // static @ 0x98
  public    static  Google.Protobuf.ValueWriter<System.Int64><>9__25_1  // static @ 0xa0
  public    static  Google.Protobuf.ValueReader<System.UInt64><>9__26_0  // static @ 0xa8
  public    static  Google.Protobuf.ValueWriter<System.UInt64><>9__26_1  // static @ 0xb0
  public    static  Google.Protobuf.ValueReader<System.Int64><>9__27_0  // static @ 0xb8
  public    static  Google.Protobuf.ValueWriter<System.Int64><>9__27_1  // static @ 0xc0
  public    static  Google.Protobuf.ValueReader<System.UInt64><>9__28_0  // static @ 0xc8
  public    static  Google.Protobuf.ValueWriter<System.UInt64><>9__28_1  // static @ 0xd0
  public    static  Google.Protobuf.ValueReader<System.Single><>9__29_0  // static @ 0xd8
  public    static  Google.Protobuf.ValueWriter<System.Single><>9__29_1  // static @ 0xe0
  public    static  Google.Protobuf.ValueReader<System.Double><>9__30_0  // static @ 0xe8
  public    static  Google.Protobuf.ValueWriter<System.Double><>9__30_1  // static @ 0xf0
METHODS:
  RVA=0x04D43B10  token=0x600013C  System.Void .cctor()
  RVA=0x041E1670  token=0x600013D  System.Void .ctor()
  RVA=0x03537440  token=0x600013E  System.String <ForString>b__16_0(Google.Protobuf.ParseContext& ctx)
  RVA=0x04693A60  token=0x600013F  System.Void <ForString>b__16_1(Google.Protobuf.WriteContext& ctx, System.String value)
  RVA=0x09AE1D50  token=0x6000140  Google.Protobuf.ByteString <ForBytes>b__17_0(Google.Protobuf.ParseContext& ctx)
  RVA=0x09AE1D5C  token=0x6000141  System.Void <ForBytes>b__17_1(Google.Protobuf.WriteContext& ctx, Google.Protobuf.ByteString value)
  RVA=0x035369C0  token=0x6000142  System.Boolean <ForBool>b__18_0(Google.Protobuf.ParseContext& ctx)
  RVA=0x03076EF0  token=0x6000143  System.Void <ForBool>b__18_1(Google.Protobuf.WriteContext& ctx, System.Boolean value)
  RVA=0x03537920  token=0x6000144  System.Int32 <ForInt32>b__19_0(Google.Protobuf.ParseContext& ctx)
  RVA=0x09AE1DF4  token=0x6000145  System.Void <ForInt32>b__19_1(Google.Protobuf.WriteContext& output, System.Int32 value)
  RVA=0x09AE1E28  token=0x6000146  System.Int32 <ForSInt32>b__20_0(Google.Protobuf.ParseContext& ctx)
  RVA=0x09AE1E4C  token=0x6000147  System.Void <ForSInt32>b__20_1(Google.Protobuf.WriteContext& output, System.Int32 value)
  RVA=0x09AE1D90  token=0x6000148  System.UInt32 <ForFixed32>b__21_0(Google.Protobuf.ParseContext& ctx)
  RVA=0x09AE1DA0  token=0x6000149  System.Void <ForFixed32>b__21_1(Google.Protobuf.WriteContext& output, System.UInt32 value)
  RVA=0x09AE1D90  token=0x600014A  System.Int32 <ForSFixed32>b__22_0(Google.Protobuf.ParseContext& ctx)
  RVA=0x09AE1E04  token=0x600014B  System.Void <ForSFixed32>b__22_1(Google.Protobuf.WriteContext& output, System.Int32 value)
  RVA=0x03539A40  token=0x600014C  System.UInt32 <ForUInt32>b__23_0(Google.Protobuf.ParseContext& ctx)
  RVA=0x09AE1E98  token=0x600014D  System.Void <ForUInt32>b__23_1(Google.Protobuf.WriteContext& output, System.UInt32 value)
  RVA=0x035371A0  token=0x600014E  System.Int64 <ForInt64>b__24_0(Google.Protobuf.ParseContext& ctx)
  RVA=0x04B3A6F0  token=0x600014F  System.Void <ForInt64>b__24_1(Google.Protobuf.WriteContext& output, System.Int64 value)
  RVA=0x09AE1E5C  token=0x6000150  System.Int64 <ForSInt64>b__25_0(Google.Protobuf.ParseContext& ctx)
  RVA=0x09AE1E84  token=0x6000151  System.Void <ForSInt64>b__25_1(Google.Protobuf.WriteContext& output, System.Int64 value)
  RVA=0x09AE1DB0  token=0x6000152  System.UInt64 <ForFixed64>b__26_0(Google.Protobuf.ParseContext& ctx)
  RVA=0x09AE1DC0  token=0x6000153  System.Void <ForFixed64>b__26_1(Google.Protobuf.WriteContext& output, System.UInt64 value)
  RVA=0x09AE1DB0  token=0x6000154  System.Int64 <ForSFixed64>b__27_0(Google.Protobuf.ParseContext& ctx)
  RVA=0x09AE1E14  token=0x6000155  System.Void <ForSFixed64>b__27_1(Google.Protobuf.WriteContext& output, System.Int64 value)
  RVA=0x0353B690  token=0x6000156  System.UInt64 <ForUInt64>b__28_0(Google.Protobuf.ParseContext& ctx)
  RVA=0x0484C110  token=0x6000157  System.Void <ForUInt64>b__28_1(Google.Protobuf.WriteContext& output, System.UInt64 value)
  RVA=0x09AE1DD4  token=0x6000158  System.Single <ForFloat>b__29_0(Google.Protobuf.ParseContext& ctx)
  RVA=0x09AE1DE4  token=0x6000159  System.Void <ForFloat>b__29_1(Google.Protobuf.WriteContext& output, System.Single value)
  RVA=0x09AE1D70  token=0x600015A  System.Double <ForDouble>b__30_0(Google.Protobuf.ParseContext& ctx)
  RVA=0x09AE1D80  token=0x600015B  System.Void <ForDouble>b__30_1(Google.Protobuf.WriteContext& output, System.Double value)
END_CLASS

CLASS: <>c__DisplayClass31_0`1
TYPE:  sealed class
TOKEN: 0x200001D
FIELDS:
  public            System.Func<System.Int32,T>     fromInt32  // 0x0
  public            System.Func<T,System.Int32>     toInt32  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600015C  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600015D  T <ForEnum>b__0(Google.Protobuf.ParseContext& ctx)
  RVA=-1  // not resolved  token=0x600015E  System.Void <ForEnum>b__1(Google.Protobuf.WriteContext& output, T value)
  RVA=-1  // not resolved  token=0x600015F  System.Int32 <ForEnum>b__2(T value)
END_CLASS

CLASS: <>c__DisplayClass32_0`1
TYPE:  sealed class
TOKEN: 0x200001E
FIELDS:
  public            Google.Protobuf.MessageParser<T>parser  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000160  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000161  T <ForMessage>b__0(Google.Protobuf.ParseContext& ctx)
  RVA=-1  // not resolved  token=0x6000162  System.Void <ForMessage>b__2(Google.Protobuf.ParseContext& ctx, T& v)
END_CLASS

CLASS: <>c__32`1
TYPE:  sealed class
TOKEN: 0x200001F
FIELDS:
  public    static readonly Google.Protobuf.FieldCodec.<>c__32<T><>9  // static @ 0x0
  public    static  Google.Protobuf.ValueWriter<T>  <>9__32_1  // static @ 0x0
  public    static  Google.Protobuf.FieldCodec.ValuesMerger<T><>9__32_3  // static @ 0x0
  public    static  System.Func<T,System.Int32>     <>9__32_4  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000163  System.Void .cctor()
  RVA=-1  // not resolved  token=0x6000164  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000165  System.Void <ForMessage>b__32_1(Google.Protobuf.WriteContext& output, T value)
  RVA=-1  // not resolved  token=0x6000166  System.Boolean <ForMessage>b__32_3(T& v, T v2)
  RVA=-1  // not resolved  token=0x6000167  System.Int32 <ForMessage>b__32_4(T message)
END_CLASS

CLASS: <>c__DisplayClass33_0`1
TYPE:  sealed class
TOKEN: 0x2000020
FIELDS:
  public            Google.Protobuf.MessageParser<T>parser  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000168  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000169  T <ForGroup>b__0(Google.Protobuf.ParseContext& ctx)
  RVA=-1  // not resolved  token=0x600016A  System.Void <ForGroup>b__2(Google.Protobuf.ParseContext& ctx, T& v)
END_CLASS

CLASS: <>c__33`1
TYPE:  sealed class
TOKEN: 0x2000021
FIELDS:
  public    static readonly Google.Protobuf.FieldCodec.<>c__33<T><>9  // static @ 0x0
  public    static  Google.Protobuf.ValueWriter<T>  <>9__33_1  // static @ 0x0
  public    static  Google.Protobuf.FieldCodec.ValuesMerger<T><>9__33_3  // static @ 0x0
  public    static  System.Func<T,System.Int32>     <>9__33_4  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600016B  System.Void .cctor()
  RVA=-1  // not resolved  token=0x600016C  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600016D  System.Void <ForGroup>b__33_1(Google.Protobuf.WriteContext& output, T value)
  RVA=-1  // not resolved  token=0x600016E  System.Boolean <ForGroup>b__33_3(T& v, T v2)
  RVA=-1  // not resolved  token=0x600016F  System.Int32 <ForGroup>b__33_4(T message)
END_CLASS

CLASS: <>c__DisplayClass34_0`1
TYPE:  sealed class
TOKEN: 0x2000022
FIELDS:
  public            Google.Protobuf.FieldCodec<T>   nestedCodec  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000170  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000171  T <ForClassWrapper>b__0(Google.Protobuf.ParseContext& ctx)
  RVA=-1  // not resolved  token=0x6000172  System.Void <ForClassWrapper>b__1(Google.Protobuf.WriteContext& output, T value)
  RVA=-1  // not resolved  token=0x6000173  System.Void <ForClassWrapper>b__2(Google.Protobuf.ParseContext& ctx, T& v)
  RVA=-1  // not resolved  token=0x6000174  System.Int32 <ForClassWrapper>b__4(T value)
END_CLASS

CLASS: <>c__34`1
TYPE:  sealed class
TOKEN: 0x2000023
FIELDS:
  public    static readonly Google.Protobuf.FieldCodec.<>c__34<T><>9  // static @ 0x0
  public    static  Google.Protobuf.FieldCodec.ValuesMerger<T><>9__34_3  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000175  System.Void .cctor()
  RVA=-1  // not resolved  token=0x6000176  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000177  System.Boolean <ForClassWrapper>b__34_3(T& v, T v2)
END_CLASS

CLASS: <>c__DisplayClass35_0`1
TYPE:  sealed class
TOKEN: 0x2000024
FIELDS:
  public            Google.Protobuf.FieldCodec<T>   nestedCodec  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000178  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000179  System.Void <ForStructWrapper>b__0(Google.Protobuf.WriteContext& output, System.Nullable<T> value)
  RVA=-1  // not resolved  token=0x600017A  System.Void <ForStructWrapper>b__1(Google.Protobuf.ParseContext& ctx, System.Nullable<T>& v)
  RVA=-1  // not resolved  token=0x600017B  System.Int32 <ForStructWrapper>b__3(System.Nullable<T> value)
END_CLASS

CLASS: <>c__35`1
TYPE:  sealed class
TOKEN: 0x2000025
FIELDS:
  public    static readonly Google.Protobuf.FieldCodec.<>c__35<T><>9  // static @ 0x0
  public    static  Google.Protobuf.FieldCodec.ValuesMerger<System.Nullable<T>><>9__35_2  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600017C  System.Void .cctor()
  RVA=-1  // not resolved  token=0x600017D  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600017E  System.Boolean <ForStructWrapper>b__35_2(System.Nullable<T>& v, System.Nullable<T> v2)
END_CLASS

CLASS: InputMerger
TYPE:  sealed class
TOKEN: 0x2000029
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x600019E  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x600019F  System.Void Invoke(Google.Protobuf.ParseContext& ctx, T& value)
  RVA=-1  // runtime  token=0x60001A0  System.IAsyncResult BeginInvoke(Google.Protobuf.ParseContext& ctx, T& value, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x60001A1  System.Void EndInvoke(Google.Protobuf.ParseContext& ctx, T& value, System.IAsyncResult result)
END_CLASS

CLASS: ValuesMerger
TYPE:  sealed class
TOKEN: 0x200002A
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x60001A2  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x60001A3  System.Boolean Invoke(T& value, T other)
  RVA=-1  // runtime  token=0x60001A4  System.IAsyncResult BeginInvoke(T& value, T other, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x60001A5  System.Boolean EndInvoke(T& value, System.IAsyncResult result)
END_CLASS

CLASS: <>c__DisplayClass38_0
TYPE:  sealed class
TOKEN: 0x200002B
FIELDS:
  public            System.Int32                    fixedSize  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60001A6  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60001A7  System.Int32 <.ctor>b__0(T _)
END_CLASS

CLASS: <>c__DisplayClass39_0
TYPE:  sealed class
TOKEN: 0x200002C
FIELDS:
  public            Google.Protobuf.ValueReader<T>  reader  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60001A8  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60001A9  System.Void <.ctor>b__0(Google.Protobuf.ParseContext& ctx, T& v)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200002D
FIELDS:
  public    static readonly Google.Protobuf.FieldCodec.<>c<T><>9  // static @ 0x0
  public    static  Google.Protobuf.FieldCodec.ValuesMerger<T><>9__39_1  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60001AA  System.Void .cctor()
  RVA=-1  // not resolved  token=0x60001AB  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60001AC  System.Boolean <.ctor>b__39_1(T& v, T v2)
END_CLASS

CLASS: Node
TYPE:  sealed class
TOKEN: 0x200002F
SIZE:  0x18
FIELDS:
  private   readonly System.Collections.Generic.Dictionary<System.String,Google.Protobuf.FieldMaskTree.Node><Children>k__BackingField  // 0x10
PROPERTIES:
  Children  get=0x02B2ECC0
METHODS:
  RVA=0x09AE0498  token=0x60001B8  System.Void .ctor()
END_CLASS

CLASS: Settings
TYPE:  sealed class
TOKEN: 0x200003A
SIZE:  0x28
FIELDS:
  private   static readonly Google.Protobuf.JsonFormatter.Settings<Default>k__BackingField  // static @ 0x0
  private   readonly System.Boolean                  <FormatDefaultValues>k__BackingField  // 0x10
  private   readonly Google.Protobuf.Reflection.TypeRegistry<TypeRegistry>k__BackingField  // 0x18
  private   readonly System.Boolean                  <FormatEnumsAsIntegers>k__BackingField  // 0x20
  private   readonly System.Boolean                  <PreserveProtoFieldNames>k__BackingField  // 0x21
PROPERTIES:
  Default  get=0x09AE18E4
  FormatDefaultValues  get=0x015EFCE0
  TypeRegistry  get=0x0385B100
  FormatEnumsAsIntegers  get=0x04D866B0
  PreserveProtoFieldNames  get=0x04D86850
METHODS:
  RVA=0x09AE1600  token=0x60001F6  System.Void .cctor()
  RVA=0x09AE16E8  token=0x60001FB  System.Void .ctor(System.Boolean formatDefaultValues)
  RVA=0x09AE17B4  token=0x60001FC  System.Void .ctor(System.Boolean formatDefaultValues, Google.Protobuf.Reflection.TypeRegistry typeRegistry)
  RVA=0x09AE17D4  token=0x60001FD  System.Void .ctor(System.Boolean formatDefaultValues, Google.Protobuf.Reflection.TypeRegistry typeRegistry, System.Boolean formatEnumsAsIntegers, System.Boolean preserveProtoFieldNames)
  RVA=0x09AE11C8  token=0x60001FE  Google.Protobuf.JsonFormatter.Settings WithFormatDefaultValues(System.Boolean formatDefaultValues)
  RVA=0x09AE14AC  token=0x60001FF  Google.Protobuf.JsonFormatter.Settings WithTypeRegistry(Google.Protobuf.Reflection.TypeRegistry typeRegistry)
  RVA=0x09AE1264  token=0x6000200  Google.Protobuf.JsonFormatter.Settings WithFormatEnumsAsIntegers(System.Boolean formatEnumsAsIntegers)
  RVA=0x09AE1388  token=0x6000201  Google.Protobuf.JsonFormatter.Settings WithPreserveProtoFieldNames(System.Boolean preserveProtoFieldNames)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200003C
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.JsonFormatter.OriginalEnumValueHelper.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Reflection.FieldInfo,System.Boolean><>9__2_0  // static @ 0x8
  public    static  System.Func<System.Reflection.FieldInfo,System.Boolean><>9__2_1  // static @ 0x10
  public    static  System.Func<System.Reflection.FieldInfo,System.Object><>9__2_2  // static @ 0x18
  public    static  System.Func<System.Reflection.FieldInfo,System.String><>9__2_3  // static @ 0x20
METHODS:
  RVA=0x09AE2628  token=0x6000205  System.Void .cctor()
  RVA=0x041E1670  token=0x6000206  System.Void .ctor()
  RVA=0x09AE1EA8  token=0x6000207  System.Boolean <GetNameMapping>b__2_0(System.Reflection.FieldInfo f)
  RVA=0x09AE1EC8  token=0x6000208  System.Boolean <GetNameMapping>b__2_1(System.Reflection.FieldInfo f)
  RVA=0x09AE1F2C  token=0x6000209  System.Object <GetNameMapping>b__2_2(System.Reflection.FieldInfo f)
  RVA=0x09AE1F4C  token=0x600020A  System.String <GetNameMapping>b__2_3(System.Reflection.FieldInfo f)
END_CLASS

CLASS: OriginalEnumValueHelper
TYPE:  static class
TOKEN: 0x200003B
SIZE:  0x10
FIELDS:
  private   static readonly System.Collections.Generic.Dictionary<System.Type,System.Collections.Generic.Dictionary<System.Object,System.String>>dictionaries  // static @ 0x0
METHODS:
  RVA=0x09AE086C  token=0x6000202  System.String GetOriginalName(System.Object value)
  RVA=0x09AE0510  token=0x6000203  System.Collections.Generic.Dictionary<System.Object,System.String> GetNameMapping(System.Type enumType)
  RVA=0x09AE0A70  token=0x6000204  System.Void .cctor()
END_CLASS

CLASS: Settings
TYPE:  sealed class
TOKEN: 0x200003E
SIZE:  0x28
FIELDS:
  private   static readonly Google.Protobuf.JsonParser.Settings<Default>k__BackingField  // static @ 0x0
  private   readonly System.Int32                    <RecursionLimit>k__BackingField  // 0x10
  private   readonly Google.Protobuf.Reflection.TypeRegistry<TypeRegistry>k__BackingField  // 0x18
  private   readonly System.Boolean                  <IgnoreUnknownFields>k__BackingField  // 0x20
PROPERTIES:
  Default  get=0x09AE1934
  RecursionLimit  get=0x02B2E2D0
  TypeRegistry  get=0x0385B100
  IgnoreUnknownFields  get=0x04D866B0
METHODS:
  RVA=0x09AE1674  token=0x600022C  System.Void .cctor()
  RVA=0x09AE185C  token=0x6000230  System.Void .ctor(System.Int32 recursionLimit, Google.Protobuf.Reflection.TypeRegistry typeRegistry, System.Boolean ignoreUnknownFields)
  RVA=0x09AE175C  token=0x6000231  System.Void .ctor(System.Int32 recursionLimit)
  RVA=0x09AE1740  token=0x6000232  System.Void .ctor(System.Int32 recursionLimit, Google.Protobuf.Reflection.TypeRegistry typeRegistry)
  RVA=0x09AE1300  token=0x6000233  Google.Protobuf.JsonParser.Settings WithIgnoreUnknownFields(System.Boolean ignoreUnknownFields)
  RVA=0x09AE1424  token=0x6000234  Google.Protobuf.JsonParser.Settings WithRecursionLimit(System.Int32 recursionLimit)
  RVA=0x09AE1548  token=0x6000235  Google.Protobuf.JsonParser.Settings WithTypeRegistry(Google.Protobuf.Reflection.TypeRegistry typeRegistry)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200003F
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.JsonParser.<>c  <>9  // static @ 0x0
METHODS:
  RVA=0x09AE2560  token=0x6000236  System.Void .cctor()
  RVA=0x041E1670  token=0x6000237  System.Void .ctor()
  RVA=0x09AE1FC8  token=0x6000238  System.Boolean <.cctor>b__41_0(Google.Protobuf.Reflection.EnumDescriptor ed)
  RVA=0x09AE203C  token=0x6000239  System.Void <.cctor>b__41_1(Google.Protobuf.JsonParser parser, Google.Protobuf.IMessage message, Google.Protobuf.JsonTokenizer tokenizer)
  RVA=0x09AE20A4  token=0x600023A  System.Void <.cctor>b__41_2(Google.Protobuf.JsonParser parser, Google.Protobuf.IMessage message, Google.Protobuf.JsonTokenizer tokenizer)
  RVA=0x09AE210C  token=0x600023B  System.Void <.cctor>b__41_3(Google.Protobuf.JsonParser parser, Google.Protobuf.IMessage message, Google.Protobuf.JsonTokenizer tokenizer)
  RVA=0x09AE213C  token=0x600023C  System.Void <.cctor>b__41_4(Google.Protobuf.JsonParser parser, Google.Protobuf.IMessage message, Google.Protobuf.JsonTokenizer tokenizer)
  RVA=0x09AE21D8  token=0x600023D  System.Void <.cctor>b__41_5(Google.Protobuf.JsonParser parser, Google.Protobuf.IMessage message, Google.Protobuf.JsonTokenizer tokenizer)
  RVA=0x09AE2208  token=0x600023E  System.Void <.cctor>b__41_6(Google.Protobuf.JsonParser parser, Google.Protobuf.IMessage message, Google.Protobuf.JsonTokenizer tokenizer)
  RVA=0x09AE2238  token=0x600023F  System.Void <.cctor>b__41_7(Google.Protobuf.JsonParser parser, Google.Protobuf.IMessage message, Google.Protobuf.JsonTokenizer tokenizer)
END_CLASS

CLASS: TokenType
TYPE:  sealed struct
TOKEN: 0x2000041
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Google.Protobuf.JsonToken.TokenTypeNull  // const
  public    static  Google.Protobuf.JsonToken.TokenTypeFalse  // const
  public    static  Google.Protobuf.JsonToken.TokenTypeTrue  // const
  public    static  Google.Protobuf.JsonToken.TokenTypeStringValue  // const
  public    static  Google.Protobuf.JsonToken.TokenTypeNumber  // const
  public    static  Google.Protobuf.JsonToken.TokenTypeName  // const
  public    static  Google.Protobuf.JsonToken.TokenTypeStartObject  // const
  public    static  Google.Protobuf.JsonToken.TokenTypeEndObject  // const
  public    static  Google.Protobuf.JsonToken.TokenTypeStartArray  // const
  public    static  Google.Protobuf.JsonToken.TokenTypeEndArray  // const
  public    static  Google.Protobuf.JsonToken.TokenTypeEndDocument  // const
METHODS:
END_CLASS

CLASS: JsonReplayTokenizer
TYPE:  class
TOKEN: 0x2000043
SIZE:  0x38
EXTENDS: Google.Protobuf.JsonTokenizer
FIELDS:
  private   readonly System.Collections.Generic.IList<Google.Protobuf.JsonToken>tokens  // 0x20
  private   readonly Google.Protobuf.JsonTokenizer   nextTokenizer  // 0x28
  private           System.Int32                    nextTokenIndex  // 0x30
METHODS:
  RVA=0x09ADC998  token=0x600025D  System.Void .ctor(System.Collections.Generic.IList<Google.Protobuf.JsonToken> tokens, Google.Protobuf.JsonTokenizer nextTokenizer)
  RVA=0x09ADC908  token=0x600025E  Google.Protobuf.JsonToken NextImpl()
END_CLASS

CLASS: ContainerType
TYPE:  sealed struct
TOKEN: 0x2000045
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Google.Protobuf.JsonTokenizer.JsonTextTokenizer.ContainerTypeDocument  // const
  public    static  Google.Protobuf.JsonTokenizer.JsonTextTokenizer.ContainerTypeObject  // const
  public    static  Google.Protobuf.JsonTokenizer.JsonTextTokenizer.ContainerTypeArray  // const
METHODS:
END_CLASS

CLASS: State
TYPE:  sealed struct
TOKEN: 0x2000046
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Google.Protobuf.JsonTokenizer.JsonTextTokenizer.StateStartOfDocument  // const
  public    static  Google.Protobuf.JsonTokenizer.JsonTextTokenizer.StateExpectedEndOfDocument  // const
  public    static  Google.Protobuf.JsonTokenizer.JsonTextTokenizer.StateReaderExhausted  // const
  public    static  Google.Protobuf.JsonTokenizer.JsonTextTokenizer.StateObjectStart  // const
  public    static  Google.Protobuf.JsonTokenizer.JsonTextTokenizer.StateObjectBeforeColon  // const
  public    static  Google.Protobuf.JsonTokenizer.JsonTextTokenizer.StateObjectAfterColon  // const
  public    static  Google.Protobuf.JsonTokenizer.JsonTextTokenizer.StateObjectAfterProperty  // const
  public    static  Google.Protobuf.JsonTokenizer.JsonTextTokenizer.StateObjectAfterComma  // const
  public    static  Google.Protobuf.JsonTokenizer.JsonTextTokenizer.StateArrayStart  // const
  public    static  Google.Protobuf.JsonTokenizer.JsonTextTokenizer.StateArrayAfterValue  // const
  public    static  Google.Protobuf.JsonTokenizer.JsonTextTokenizer.StateArrayAfterComma  // const
METHODS:
END_CLASS

CLASS: PushBackReader
TYPE:  class
TOKEN: 0x2000047
SIZE:  0x20
FIELDS:
  private   readonly System.IO.TextReader            reader  // 0x10
  private           System.Nullable<System.Char>    nextChar  // 0x18
METHODS:
  RVA=0x053908C0  token=0x600026E  System.Void .ctor(System.IO.TextReader reader)
  RVA=0x09AE114C  token=0x600026F  System.Nullable<System.Char> Read()
  RVA=0x09AE10BC  token=0x6000270  System.Char ReadOrFail(System.String messageOnFailure)
  RVA=0x09AE1008  token=0x6000271  System.Void PushBack(System.Char c)
  RVA=0x09AE0FA8  token=0x6000272  Google.Protobuf.InvalidJsonException CreateException(System.String message)
END_CLASS

CLASS: JsonTextTokenizer
TYPE:  sealed class
TOKEN: 0x2000044
SIZE:  0x38
EXTENDS: Google.Protobuf.JsonTokenizer
FIELDS:
  private   static readonly Google.Protobuf.JsonTokenizer.JsonTextTokenizer.StateValueStates  // static @ 0x0
  private   readonly System.Collections.Generic.Stack<Google.Protobuf.JsonTokenizer.JsonTextTokenizer.ContainerType>containerStack  // 0x20
  private   readonly Google.Protobuf.JsonTokenizer.JsonTextTokenizer.PushBackReaderreader  // 0x28
  private           Google.Protobuf.JsonTokenizer.JsonTextTokenizer.Statestate  // 0x30
METHODS:
  RVA=0x09ADE1B4  token=0x600025F  System.Void .ctor(System.IO.TextReader reader)
  RVA=0x09ADCBEC  token=0x6000260  Google.Protobuf.JsonToken NextImpl()
  RVA=0x09ADE0DC  token=0x6000261  System.Void ValidateState(Google.Protobuf.JsonTokenizer.JsonTextTokenizer.State validStates, System.String errorPrefix)
  RVA=0x09ADDC7C  token=0x6000262  System.String ReadString()
  RVA=0x09ADD3E4  token=0x6000263  System.Char ReadEscapedCharacter()
  RVA=0x09ADDEB4  token=0x6000264  System.Char ReadUnicodeEscape()
  RVA=0x09ADCA98  token=0x6000265  System.Void ConsumeLiteral(System.String text)
  RVA=0x09ADD900  token=0x6000266  System.Double ReadNumber(System.Char initialCharacter)
  RVA=0x09ADD7D0  token=0x6000267  System.Nullable<System.Char> ReadInt(System.Text.StringBuilder builder)
  RVA=0x09ADD73C  token=0x6000268  System.Nullable<System.Char> ReadFrac(System.Text.StringBuilder builder)
  RVA=0x09ADD550  token=0x6000269  System.Nullable<System.Char> ReadExp(System.Text.StringBuilder builder)
  RVA=0x09ADC9C0  token=0x600026A  System.Nullable<System.Char> ConsumeDigits(System.Text.StringBuilder builder, System.Int32& count)
  RVA=0x09ADDFEC  token=0x600026B  System.Void ValidateAndModifyStateForValue(System.String errorPrefix)
  RVA=0x09ADD2AC  token=0x600026C  System.Void PopContainer()
  RVA=0x09ADE178  token=0x600026D  System.Void .cctor()
END_CLASS

CLASS: <>c__DisplayClass13_0
TYPE:  sealed class
TOKEN: 0x200004A
SIZE:  0x18
FIELDS:
  public            Google.Protobuf.IMessage        message  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000294  System.Void .ctor()
  RVA=0x09AE2318  token=0x6000295  System.Boolean <IsInitialized>b__0(Google.Protobuf.Reflection.FieldDescriptor f)
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x200004D
FIELDS:
  public            System.Func<T>                  factory  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60002B4  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60002B5  Google.Protobuf.IMessage <.ctor>b__0()
END_CLASS

CLASS: WireType
TYPE:  sealed struct
TOKEN: 0x200005A
SIZE:  0x14
FIELDS:
  public            System.UInt32                   value__  // 0x10
  public    static  Google.Protobuf.WireFormat.WireTypeVarint  // const
  public    static  Google.Protobuf.WireFormat.WireTypeFixed64  // const
  public    static  Google.Protobuf.WireFormat.WireTypeLengthDelimited  // const
  public    static  Google.Protobuf.WireFormat.WireTypeStartGroup  // const
  public    static  Google.Protobuf.WireFormat.WireTypeEndGroup  // const
  public    static  Google.Protobuf.WireFormat.WireTypeFixed32  // const
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000062
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.WellKnownTypes.Any.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09AF05C4  token=0x60003C6  System.Void .cctor()
  RVA=0x041E1670  token=0x60003C7  System.Void .ctor()
  RVA=0x09AF00B0  token=0x60003C8  Google.Protobuf.WellKnownTypes.Any <.cctor>b__40_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000065
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.WellKnownTypes.Api.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09AF036C  token=0x60003E7  System.Void .cctor()
  RVA=0x041E1670  token=0x60003E8  System.Void .ctor()
  RVA=0x09AF01C8  token=0x60003E9  Google.Protobuf.WellKnownTypes.Api <.cctor>b__57_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000067
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.WellKnownTypes.Method.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09AF0628  token=0x6000408  System.Void .cctor()
  RVA=0x041E1670  token=0x6000409  System.Void .ctor()
  RVA=0x09AF0218  token=0x600040A  Google.Protobuf.WellKnownTypes.Method <.cctor>b__57_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000069
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.WellKnownTypes.Mixin.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09AF0308  token=0x6000420  System.Void .cctor()
  RVA=0x041E1670  token=0x6000421  System.Void .ctor()
  RVA=0x09AF0060  token=0x6000422  Google.Protobuf.WellKnownTypes.Mixin <.cctor>b__32_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200006C
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.WellKnownTypes.Duration.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09AF0560  token=0x6000444  System.Void .cctor()
  RVA=0x041E1670  token=0x6000445  System.Void .ctor()
  RVA=0x09AF0150  token=0x6000446  Google.Protobuf.WellKnownTypes.Duration <.cctor>b__48_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200006F
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.WellKnownTypes.Empty.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09AF0434  token=0x600045A  System.Void .cctor()
  RVA=0x041E1670  token=0x600045B  System.Void .ctor()
  RVA=0x09AEFF34  token=0x600045C  Google.Protobuf.WellKnownTypes.Empty <.cctor>b__22_0()
END_CLASS

CLASS: MergeOptions
TYPE:  sealed class
TOKEN: 0x2000072
SIZE:  0x18
FIELDS:
  private           System.Boolean                  <ReplaceMessageFields>k__BackingField  // 0x10
  private           System.Boolean                  <ReplaceRepeatedFields>k__BackingField  // 0x11
  private           System.Boolean                  <ReplacePrimitiveFields>k__BackingField  // 0x12
PROPERTIES:
  ReplaceMessageFields  get=0x015EFCE0  set=0x04D86130
  ReplaceRepeatedFields  get=0x04D88380  set=0x04D88460
  ReplacePrimitiveFields  get=0x04D88390  set=0x04DA0900
METHODS:
  RVA=0x041E1670  token=0x6000488  System.Void .ctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000073
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.WellKnownTypes.FieldMask.<>c<>9  // static @ 0x0
  public    static  System.Func<System.String,System.Boolean><>9__29_0  // static @ 0x8
METHODS:
  RVA=0x09AF068C  token=0x6000489  System.Void .cctor()
  RVA=0x041E1670  token=0x600048A  System.Void .ctor()
  RVA=0x09AEFEF0  token=0x600048B  System.Boolean <ToJson>b__29_0(System.String p)
  RVA=0x09AF0100  token=0x600048C  Google.Protobuf.WellKnownTypes.FieldMask <.cctor>b__47_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000076
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.WellKnownTypes.SourceContext.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09AF03D0  token=0x60004A2  System.Void .cctor()
  RVA=0x041E1670  token=0x60004A3  System.Void .ctor()
  RVA=0x09AF0010  token=0x60004A4  Google.Protobuf.WellKnownTypes.SourceContext <.cctor>b__27_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200007A
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.WellKnownTypes.Struct.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09AF04FC  token=0x60004B9  System.Void .cctor()
  RVA=0x041E1670  token=0x60004BA  System.Void .ctor()
  RVA=0x09AEFF70  token=0x60004BB  Google.Protobuf.WellKnownTypes.Struct <.cctor>b__27_0()
END_CLASS

CLASS: KindOneofCase
TYPE:  sealed struct
TOKEN: 0x200007C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Google.Protobuf.WellKnownTypes.Value.KindOneofCaseNone  // const
  public    static  Google.Protobuf.WellKnownTypes.Value.KindOneofCaseNullValue  // const
  public    static  Google.Protobuf.WellKnownTypes.Value.KindOneofCaseNumberValue  // const
  public    static  Google.Protobuf.WellKnownTypes.Value.KindOneofCaseStringValue  // const
  public    static  Google.Protobuf.WellKnownTypes.Value.KindOneofCaseBoolValue  // const
  public    static  Google.Protobuf.WellKnownTypes.Value.KindOneofCaseStructValue  // const
  public    static  Google.Protobuf.WellKnownTypes.Value.KindOneofCaseListValue  // const
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200007D
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.WellKnownTypes.Value.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09AF02A4  token=0x60004E1  System.Void .cctor()
  RVA=0x041E1670  token=0x60004E2  System.Void .ctor()
  RVA=0x09AF0268  token=0x60004E3  Google.Protobuf.WellKnownTypes.Value <.cctor>b__58_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200007F
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.WellKnownTypes.ListValue.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09AF06F0  token=0x60004F6  System.Void .cctor()
  RVA=0x041E1670  token=0x60004F7  System.Void .ctor()
  RVA=0x09AEFFC0  token=0x60004F8  Google.Protobuf.WellKnownTypes.ListValue <.cctor>b__27_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000083
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.WellKnownTypes.Timestamp.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09AF0498  token=0x6000525  System.Void .cctor()
  RVA=0x041E1670  token=0x6000526  System.Void .ctor()
  RVA=0x09AF018C  token=0x6000527  Google.Protobuf.WellKnownTypes.Timestamp <.cctor>b__55_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000087
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.WellKnownTypes.Type.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B07B18  token=0x6000544  System.Void .cctor()
  RVA=0x041E1670  token=0x6000545  System.Void .ctor()
  RVA=0x09B07910  token=0x6000546  Google.Protobuf.WellKnownTypes.Type <.cctor>b__52_0()
END_CLASS

CLASS: Kind
TYPE:  sealed struct
TOKEN: 0x200008A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Google.Protobuf.WellKnownTypes.Field.Types.KindTypeUnknown  // const
  public    static  Google.Protobuf.WellKnownTypes.Field.Types.KindTypeDouble  // const
  public    static  Google.Protobuf.WellKnownTypes.Field.Types.KindTypeFloat  // const
  public    static  Google.Protobuf.WellKnownTypes.Field.Types.KindTypeInt64  // const
  public    static  Google.Protobuf.WellKnownTypes.Field.Types.KindTypeUint64  // const
  public    static  Google.Protobuf.WellKnownTypes.Field.Types.KindTypeInt32  // const
  public    static  Google.Protobuf.WellKnownTypes.Field.Types.KindTypeFixed64  // const
  public    static  Google.Protobuf.WellKnownTypes.Field.Types.KindTypeFixed32  // const
  public    static  Google.Protobuf.WellKnownTypes.Field.Types.KindTypeBool  // const
  public    static  Google.Protobuf.WellKnownTypes.Field.Types.KindTypeString  // const
  public    static  Google.Protobuf.WellKnownTypes.Field.Types.KindTypeGroup  // const
  public    static  Google.Protobuf.WellKnownTypes.Field.Types.KindTypeMessage  // const
  public    static  Google.Protobuf.WellKnownTypes.Field.Types.KindTypeBytes  // const
  public    static  Google.Protobuf.WellKnownTypes.Field.Types.KindTypeUint32  // const
  public    static  Google.Protobuf.WellKnownTypes.Field.Types.KindTypeEnum  // const
  public    static  Google.Protobuf.WellKnownTypes.Field.Types.KindTypeSfixed32  // const
  public    static  Google.Protobuf.WellKnownTypes.Field.Types.KindTypeSfixed64  // const
  public    static  Google.Protobuf.WellKnownTypes.Field.Types.KindTypeSint32  // const
  public    static  Google.Protobuf.WellKnownTypes.Field.Types.KindTypeSint64  // const
METHODS:
END_CLASS

CLASS: Cardinality
TYPE:  sealed struct
TOKEN: 0x200008B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Google.Protobuf.WellKnownTypes.Field.Types.CardinalityUnknown  // const
  public    static  Google.Protobuf.WellKnownTypes.Field.Types.CardinalityOptional  // const
  public    static  Google.Protobuf.WellKnownTypes.Field.Types.CardinalityRequired  // const
  public    static  Google.Protobuf.WellKnownTypes.Field.Types.CardinalityRepeated  // const
METHODS:
END_CLASS

CLASS: Types
TYPE:  static class
TOKEN: 0x2000089
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200008C
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.WellKnownTypes.Field.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B08090  token=0x600056B  System.Void .cctor()
  RVA=0x041E1670  token=0x600056C  System.Void .ctor()
  RVA=0x09B07960  token=0x600056D  Google.Protobuf.WellKnownTypes.Field <.cctor>b__73_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200008E
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.WellKnownTypes.Enum.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B080F4  token=0x6000587  System.Void .cctor()
  RVA=0x041E1670  token=0x6000588  System.Void .ctor()
  RVA=0x09B078C0  token=0x6000589  Google.Protobuf.WellKnownTypes.Enum <.cctor>b__47_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000090
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.WellKnownTypes.EnumValue.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B07FC8  token=0x60005A0  System.Void .cctor()
  RVA=0x041E1670  token=0x60005A1  System.Void .ctor()
  RVA=0x09B077A8  token=0x60005A2  Google.Protobuf.WellKnownTypes.EnumValue <.cctor>b__37_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000092
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.WellKnownTypes.Option.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B07D0C  token=0x60005B8  System.Void .cctor()
  RVA=0x041E1670  token=0x60005B9  System.Void .ctor()
  RVA=0x09B0771C  token=0x60005BA  Google.Protobuf.WellKnownTypes.Option <.cctor>b__32_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000095
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.WellKnownTypes.DoubleValue.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B0802C  token=0x60005D0  System.Void .cctor()
  RVA=0x041E1670  token=0x60005D1  System.Void .ctor()
  RVA=0x09B076E0  token=0x60005D2  Google.Protobuf.WellKnownTypes.DoubleValue <.cctor>b__27_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000097
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.WellKnownTypes.FloatValue.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B07F64  token=0x60005E6  System.Void .cctor()
  RVA=0x041E1670  token=0x60005E7  System.Void .ctor()
  RVA=0x09B07668  token=0x60005E8  Google.Protobuf.WellKnownTypes.FloatValue <.cctor>b__27_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000099
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.WellKnownTypes.Int64Value.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B07F00  token=0x60005FC  System.Void .cctor()
  RVA=0x041E1670  token=0x60005FD  System.Void .ctor()
  RVA=0x09B076A4  token=0x60005FE  Google.Protobuf.WellKnownTypes.Int64Value <.cctor>b__27_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200009B
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.WellKnownTypes.UInt64Value.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B07E9C  token=0x6000612  System.Void .cctor()
  RVA=0x041E1670  token=0x6000613  System.Void .ctor()
  RVA=0x09B07514  token=0x6000614  Google.Protobuf.WellKnownTypes.UInt64Value <.cctor>b__27_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200009D
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.WellKnownTypes.Int32Value.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B07CA8  token=0x6000628  System.Void .cctor()
  RVA=0x041E1670  token=0x6000629  System.Void .ctor()
  RVA=0x09B0762C  token=0x600062A  Google.Protobuf.WellKnownTypes.Int32Value <.cctor>b__27_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200009F
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.WellKnownTypes.UInt32Value.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B07C44  token=0x600063E  System.Void .cctor()
  RVA=0x041E1670  token=0x600063F  System.Void .ctor()
  RVA=0x09B074D8  token=0x6000640  Google.Protobuf.WellKnownTypes.UInt32Value <.cctor>b__27_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000A1
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.WellKnownTypes.BoolValue.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B08284  token=0x6000654  System.Void .cctor()
  RVA=0x041E1670  token=0x6000655  System.Void .ctor()
  RVA=0x09B07550  token=0x6000656  Google.Protobuf.WellKnownTypes.BoolValue <.cctor>b__27_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000A3
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.WellKnownTypes.StringValue.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B08158  token=0x600066A  System.Void .cctor()
  RVA=0x041E1670  token=0x600066B  System.Void .ctor()
  RVA=0x09B075DC  token=0x600066C  Google.Protobuf.WellKnownTypes.StringValue <.cctor>b__27_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000A5
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.WellKnownTypes.BytesValue.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B08220  token=0x6000680  System.Void .cctor()
  RVA=0x041E1670  token=0x6000681  System.Void .ctor()
  RVA=0x09B07488  token=0x6000682  Google.Protobuf.WellKnownTypes.BytesValue <.cctor>b__27_0()
END_CLASS

CLASS: <>c__19`1
TYPE:  sealed class
TOKEN: 0x20000A7
FIELDS:
  public    static readonly Google.Protobuf.Reflection.CustomOptions.<>c__19<T><>9  // static @ 0x0
  public    static  System.Func<T,Google.Protobuf.ByteString><>9__19_0  // static @ 0x0
  public    static  System.Func<T,Google.Protobuf.ByteString,T><>9__19_1  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000696  System.Void .cctor()
  RVA=-1  // not resolved  token=0x6000697  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000698  Google.Protobuf.ByteString <TryGetMessage>b__19_0(T v)
  RVA=-1  // not resolved  token=0x6000699  T <TryGetMessage>b__19_1(T t, Google.Protobuf.ByteString b)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000AA
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.FileDescriptorSet.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B081BC  token=0x60006AE  System.Void .cctor()
  RVA=0x041E1670  token=0x60006AF  System.Void .ctor()
  RVA=0x09B0758C  token=0x60006B0  Google.Protobuf.Reflection.FileDescriptorSet <.cctor>b__27_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000AC
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.FileDescriptorProto.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B07BE0  token=0x60006D9  System.Void .cctor()
  RVA=0x041E1670  token=0x60006DA  System.Void .ctor()
  RVA=0x09B07A00  token=0x60006DB  Google.Protobuf.Reflection.FileDescriptorProto <.cctor>b__94_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000B0
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.DescriptorProto.Types.ExtensionRange.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B07D70  token=0x6000716  System.Void .cctor()
  RVA=0x041E1670  token=0x6000717  System.Void .ctor()
  RVA=0x09B07884  token=0x6000718  Google.Protobuf.Reflection.DescriptorProto.Types.ExtensionRange <.cctor>b__46_0()
END_CLASS

CLASS: ExtensionRange
TYPE:  sealed class
TOKEN: 0x20000AF
SIZE:  0x30
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.DescriptorProto.Types.ExtensionRange>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  private           System.Int32                    _hasBits0  // 0x18
  public    static  System.Int32                    StartFieldNumber  // const
  private   static readonly System.Int32                    StartDefaultValue  // static @ 0x8
  private           System.Int32                    start_  // 0x1c
  public    static  System.Int32                    EndFieldNumber  // const
  private   static readonly System.Int32                    EndDefaultValue  // static @ 0xc
  private           System.Int32                    end_  // 0x20
  public    static  System.Int32                    OptionsFieldNumber  // const
  private           Google.Protobuf.Reflection.ExtensionRangeOptionsoptions_  // 0x28
PROPERTIES:
  Parser  get=0x09AFF0B0
  Descriptor  get=0x09AFEFF4
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09AFF318
  Start  get=0x09AFF100  set=0x04DA0E10
  HasStart  get=0x0314ECE0
  End  get=0x09AFF054  set=0x04DA0E00
  HasEnd  get=0x09AFF0A8
  Options  get=0x04D86240  set=0x02FDB880
METHODS:
  RVA=0x041E1670  token=0x60006FE  System.Void .ctor()
  RVA=0x09AFEF80  token=0x60006FF  System.Void .ctor(Google.Protobuf.Reflection.DescriptorProto.Types.ExtensionRange other)
  RVA=0x09AFEB5C  token=0x6000700  Google.Protobuf.Reflection.DescriptorProto.Types.ExtensionRange Clone()
  RVA=0x04D86DE0  token=0x6000704  System.Void ClearStart()
  RVA=0x04DA0DF0  token=0x6000708  System.Void ClearEnd()
  RVA=0x09AFEC48  token=0x600070B  System.Boolean Equals(System.Object other)
  RVA=0x09AFEBBC  token=0x600070C  System.Boolean Equals(Google.Protobuf.Reflection.DescriptorProto.Types.ExtensionRange other)
  RVA=0x09AFECAC  token=0x600070D  System.Int32 GetHashCode()
  RVA=0x09AFEE08  token=0x600070E  System.String ToString()
  RVA=0x04D52400  token=0x600070F  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09AFF270  token=0x6000710  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09AFEA8C  token=0x6000711  System.Int32 CalculateSize()
  RVA=0x09AFED1C  token=0x6000712  System.Void MergeFrom(Google.Protobuf.Reflection.DescriptorProto.Types.ExtensionRange other)
  RVA=0x04D51840  token=0x6000713  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09AFF154  token=0x6000714  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09AFEE54  token=0x6000715  System.Void .cctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000B2
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.DescriptorProto.Types.ReservedRange.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B07A50  token=0x6000732  System.Void .cctor()
  RVA=0x041E1670  token=0x6000733  System.Void .ctor()
  RVA=0x09B07848  token=0x6000734  Google.Protobuf.Reflection.DescriptorProto.Types.ReservedRange <.cctor>b__41_0()
END_CLASS

CLASS: ReservedRange
TYPE:  sealed class
TOKEN: 0x20000B1
SIZE:  0x28
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.DescriptorProto.Types.ReservedRange>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  private           System.Int32                    _hasBits0  // 0x18
  public    static  System.Int32                    StartFieldNumber  // const
  private   static readonly System.Int32                    StartDefaultValue  // static @ 0x8
  private           System.Int32                    start_  // 0x1c
  public    static  System.Int32                    EndFieldNumber  // const
  private   static readonly System.Int32                    EndDefaultValue  // static @ 0xc
  private           System.Int32                    end_  // 0x20
PROPERTIES:
  Parser  get=0x09B05D84
  Descriptor  get=0x09B05CCC
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B05F4C
  Start  get=0x09B05DD4  set=0x04DA0E10
  HasStart  get=0x0314ECE0
  End  get=0x09B05D30  set=0x04DA0E00
  HasEnd  get=0x09AFF0A8
METHODS:
  RVA=0x041E1670  token=0x600071C  System.Void .ctor()
  RVA=0x0637A38C  token=0x600071D  System.Void .ctor(Google.Protobuf.Reflection.DescriptorProto.Types.ReservedRange other)
  RVA=0x09B05954  token=0x600071E  Google.Protobuf.Reflection.DescriptorProto.Types.ReservedRange Clone()
  RVA=0x04D86DE0  token=0x6000722  System.Void ClearStart()
  RVA=0x04DA0DF0  token=0x6000726  System.Void ClearEnd()
  RVA=0x09B059B4  token=0x6000727  System.Boolean Equals(System.Object other)
  RVA=0x09B05A18  token=0x6000728  System.Boolean Equals(Google.Protobuf.Reflection.DescriptorProto.Types.ReservedRange other)
  RVA=0x09B05A90  token=0x6000729  System.Int32 GetHashCode()
  RVA=0x09B05B54  token=0x600072A  System.String ToString()
  RVA=0x04D52400  token=0x600072B  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09B05EC4  token=0x600072C  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09B058A8  token=0x600072D  System.Int32 CalculateSize()
  RVA=0x09B05AE8  token=0x600072E  System.Void MergeFrom(Google.Protobuf.Reflection.DescriptorProto.Types.ReservedRange other)
  RVA=0x04D51840  token=0x600072F  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09B05E28  token=0x6000730  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09B05BA0  token=0x6000731  System.Void .cctor()
END_CLASS

CLASS: Types
TYPE:  static class
TOKEN: 0x20000AE
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000B3
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.DescriptorProto.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B07AB4  token=0x6000735  System.Void .cctor()
  RVA=0x041E1670  token=0x6000736  System.Void .ctor()
  RVA=0x09B079B0  token=0x6000737  Google.Protobuf.Reflection.DescriptorProto <.cctor>b__77_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000B5
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.ExtensionRangeOptions.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B07DD4  token=0x6000752  System.Void .cctor()
  RVA=0x041E1670  token=0x6000753  System.Void .ctor()
  RVA=0x09B077F8  token=0x6000754  Google.Protobuf.Reflection.ExtensionRangeOptions <.cctor>b__37_0()
END_CLASS

CLASS: Type
TYPE:  sealed struct
TOKEN: 0x20000B8
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Google.Protobuf.Reflection.FieldDescriptorProto.Types.TypeDouble  // const
  public    static  Google.Protobuf.Reflection.FieldDescriptorProto.Types.TypeFloat  // const
  public    static  Google.Protobuf.Reflection.FieldDescriptorProto.Types.TypeInt64  // const
  public    static  Google.Protobuf.Reflection.FieldDescriptorProto.Types.TypeUint64  // const
  public    static  Google.Protobuf.Reflection.FieldDescriptorProto.Types.TypeInt32  // const
  public    static  Google.Protobuf.Reflection.FieldDescriptorProto.Types.TypeFixed64  // const
  public    static  Google.Protobuf.Reflection.FieldDescriptorProto.Types.TypeFixed32  // const
  public    static  Google.Protobuf.Reflection.FieldDescriptorProto.Types.TypeBool  // const
  public    static  Google.Protobuf.Reflection.FieldDescriptorProto.Types.TypeString  // const
  public    static  Google.Protobuf.Reflection.FieldDescriptorProto.Types.TypeGroup  // const
  public    static  Google.Protobuf.Reflection.FieldDescriptorProto.Types.TypeMessage  // const
  public    static  Google.Protobuf.Reflection.FieldDescriptorProto.Types.TypeBytes  // const
  public    static  Google.Protobuf.Reflection.FieldDescriptorProto.Types.TypeUint32  // const
  public    static  Google.Protobuf.Reflection.FieldDescriptorProto.Types.TypeEnum  // const
  public    static  Google.Protobuf.Reflection.FieldDescriptorProto.Types.TypeSfixed32  // const
  public    static  Google.Protobuf.Reflection.FieldDescriptorProto.Types.TypeSfixed64  // const
  public    static  Google.Protobuf.Reflection.FieldDescriptorProto.Types.TypeSint32  // const
  public    static  Google.Protobuf.Reflection.FieldDescriptorProto.Types.TypeSint64  // const
METHODS:
END_CLASS

CLASS: Label
TYPE:  sealed struct
TOKEN: 0x20000B9
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Google.Protobuf.Reflection.FieldDescriptorProto.Types.LabelOptional  // const
  public    static  Google.Protobuf.Reflection.FieldDescriptorProto.Types.LabelRequired  // const
  public    static  Google.Protobuf.Reflection.FieldDescriptorProto.Types.LabelRepeated  // const
METHODS:
END_CLASS

CLASS: Types
TYPE:  static class
TOKEN: 0x20000B7
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000BA
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.FieldDescriptorProto.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B07E38  token=0x6000790  System.Void .cctor()
  RVA=0x041E1670  token=0x6000791  System.Void .ctor()
  RVA=0x09B0744C  token=0x6000792  Google.Protobuf.Reflection.FieldDescriptorProto <.cctor>b__119_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000BC
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.OneofDescriptorProto.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B07B7C  token=0x60007AA  System.Void .cctor()
  RVA=0x041E1670  token=0x60007AB  System.Void .ctor()
  RVA=0x09B0776C  token=0x60007AC  Google.Protobuf.Reflection.OneofDescriptorProto <.cctor>b__36_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000C0
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.EnumDescriptorProto.Types.EnumReservedRange.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B1A180  token=0x60007E0  System.Void .cctor()
  RVA=0x041E1670  token=0x60007E1  System.Void .ctor()
  RVA=0x09B1984C  token=0x60007E2  Google.Protobuf.Reflection.EnumDescriptorProto.Types.EnumReservedRange <.cctor>b__41_0()
END_CLASS

CLASS: EnumReservedRange
TYPE:  sealed class
TOKEN: 0x20000BF
SIZE:  0x28
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.EnumDescriptorProto.Types.EnumReservedRange>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  private           System.Int32                    _hasBits0  // 0x18
  public    static  System.Int32                    StartFieldNumber  // const
  private   static readonly System.Int32                    StartDefaultValue  // static @ 0x8
  private           System.Int32                    start_  // 0x1c
  public    static  System.Int32                    EndFieldNumber  // const
  private   static readonly System.Int32                    EndDefaultValue  // static @ 0xc
  private           System.Int32                    end_  // 0x20
PROPERTIES:
  Parser  get=0x09B0CE94
  Descriptor  get=0x09B0CDE0
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B0CFC0
  Start  get=0x09B0CEE4  set=0x04DA0E10
  HasStart  get=0x0314ECE0
  End  get=0x09B0CE40  set=0x04DA0E00
  HasEnd  get=0x09AFF0A8
METHODS:
  RVA=0x041E1670  token=0x60007CA  System.Void .ctor()
  RVA=0x0637A38C  token=0x60007CB  System.Void .ctor(Google.Protobuf.Reflection.EnumDescriptorProto.Types.EnumReservedRange other)
  RVA=0x09B0CA68  token=0x60007CC  Google.Protobuf.Reflection.EnumDescriptorProto.Types.EnumReservedRange Clone()
  RVA=0x04D86DE0  token=0x60007D0  System.Void ClearStart()
  RVA=0x04DA0DF0  token=0x60007D4  System.Void ClearEnd()
  RVA=0x09B0CAC8  token=0x60007D5  System.Boolean Equals(System.Object other)
  RVA=0x09B0CB2C  token=0x60007D6  System.Boolean Equals(Google.Protobuf.Reflection.EnumDescriptorProto.Types.EnumReservedRange other)
  RVA=0x09B0CBA4  token=0x60007D7  System.Int32 GetHashCode()
  RVA=0x09B0CC68  token=0x60007D8  System.String ToString()
  RVA=0x04D52400  token=0x60007D9  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09B0CF38  token=0x60007DA  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09B0C9BC  token=0x60007DB  System.Int32 CalculateSize()
  RVA=0x09B0CBFC  token=0x60007DC  System.Void MergeFrom(Google.Protobuf.Reflection.EnumDescriptorProto.Types.EnumReservedRange other)
  RVA=0x04D51840  token=0x60007DD  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09B05E28  token=0x60007DE  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09B0CCB4  token=0x60007DF  System.Void .cctor()
END_CLASS

CLASS: Types
TYPE:  static class
TOKEN: 0x20000BE
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000C1
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.EnumDescriptorProto.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B19EC4  token=0x60007E3  System.Void .cctor()
  RVA=0x041E1670  token=0x60007E4  System.Void .ctor()
  RVA=0x09B19964  token=0x60007E5  Google.Protobuf.Reflection.EnumDescriptorProto <.cctor>b__52_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000C3
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.EnumValueDescriptorProto.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B1A0B8  token=0x6000801  System.Void .cctor()
  RVA=0x041E1670  token=0x6000802  System.Void .ctor()
  RVA=0x09B19888  token=0x6000803  Google.Protobuf.Reflection.EnumValueDescriptorProto <.cctor>b__46_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000C5
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.ServiceDescriptorProto.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B1A2AC  token=0x600081C  System.Void .cctor()
  RVA=0x041E1670  token=0x600081D  System.Void .ctor()
  RVA=0x09B197C0  token=0x600081E  Google.Protobuf.Reflection.ServiceDescriptorProto <.cctor>b__41_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000C7
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.MethodDescriptorProto.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B19C6C  token=0x6000846  System.Void .cctor()
  RVA=0x041E1670  token=0x6000847  System.Void .ctor()
  RVA=0x09B19AF4  token=0x6000848  Google.Protobuf.Reflection.MethodDescriptorProto <.cctor>b__73_0()
END_CLASS

CLASS: OptimizeMode
TYPE:  sealed struct
TOKEN: 0x20000CA
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Google.Protobuf.Reflection.FileOptions.Types.OptimizeModeSpeed  // const
  public    static  Google.Protobuf.Reflection.FileOptions.Types.OptimizeModeCodeSize  // const
  public    static  Google.Protobuf.Reflection.FileOptions.Types.OptimizeModeLiteRuntime  // const
METHODS:
END_CLASS

CLASS: Types
TYPE:  static class
TOKEN: 0x20000C9
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000CB
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.FileOptions.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B1A1E4  token=0x60008B3  System.Void .cctor()
  RVA=0x041E1670  token=0x60008B4  System.Void .ctor()
  RVA=0x09B19680  token=0x60008B5  Google.Protobuf.Reflection.FileOptions <.cctor>b__219_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000CD
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.MessageOptions.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B19FF0  token=0x60008E0  System.Void .cctor()
  RVA=0x041E1670  token=0x60008E1  System.Void .ctor()
  RVA=0x09B19B30  token=0x60008E2  Google.Protobuf.Reflection.MessageOptions <.cctor>b__74_0()
END_CLASS

CLASS: CType
TYPE:  sealed struct
TOKEN: 0x20000D0
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Google.Protobuf.Reflection.FieldOptions.Types.CTypeString  // const
  public    static  Google.Protobuf.Reflection.FieldOptions.Types.CTypeCord  // const
  public    static  Google.Protobuf.Reflection.FieldOptions.Types.CTypeStringPiece  // const
METHODS:
END_CLASS

CLASS: JSType
TYPE:  sealed struct
TOKEN: 0x20000D1
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Google.Protobuf.Reflection.FieldOptions.Types.JSTypeJsNormal  // const
  public    static  Google.Protobuf.Reflection.FieldOptions.Types.JSTypeJsString  // const
  public    static  Google.Protobuf.Reflection.FieldOptions.Types.JSTypeJsNumber  // const
METHODS:
END_CLASS

CLASS: Types
TYPE:  static class
TOKEN: 0x20000CF
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000D2
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.FieldOptions.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B19E60  token=0x6000919  System.Void .cctor()
  RVA=0x041E1670  token=0x600091A  System.Void .ctor()
  RVA=0x09B19630  token=0x600091B  Google.Protobuf.Reflection.FieldOptions <.cctor>b__102_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000D4
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.OneofOptions.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B1A248  token=0x6000936  System.Void .cctor()
  RVA=0x041E1670  token=0x6000937  System.Void .ctor()
  RVA=0x09B19770  token=0x6000938  Google.Protobuf.Reflection.OneofOptions <.cctor>b__37_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000D6
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.EnumOptions.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B19D34  token=0x600095B  System.Void .cctor()
  RVA=0x041E1670  token=0x600095C  System.Void .ctor()
  RVA=0x09B19A54  token=0x600095D  Google.Protobuf.Reflection.EnumOptions <.cctor>b__56_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000D8
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.EnumValueOptions.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B19D98  token=0x600097C  System.Void .cctor()
  RVA=0x041E1670  token=0x600097D  System.Void .ctor()
  RVA=0x09B19914  token=0x600097E  Google.Protobuf.Reflection.EnumValueOptions <.cctor>b__47_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000DA
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.ServiceOptions.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B19DFC  token=0x600099D  System.Void .cctor()
  RVA=0x041E1670  token=0x600099E  System.Void .ctor()
  RVA=0x09B198C4  token=0x600099F  Google.Protobuf.Reflection.ServiceOptions <.cctor>b__47_0()
END_CLASS

CLASS: IdempotencyLevel
TYPE:  sealed struct
TOKEN: 0x20000DD
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Google.Protobuf.Reflection.MethodOptions.Types.IdempotencyLevelIdempotencyUnknown  // const
  public    static  Google.Protobuf.Reflection.MethodOptions.Types.IdempotencyLevelNoSideEffects  // const
  public    static  Google.Protobuf.Reflection.MethodOptions.Types.IdempotencyLevelIdempotent  // const
METHODS:
END_CLASS

CLASS: Types
TYPE:  static class
TOKEN: 0x20000DC
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000DE
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.MethodOptions.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B19F8C  token=0x60009C2  System.Void .cctor()
  RVA=0x041E1670  token=0x60009C3  System.Void .ctor()
  RVA=0x09B19AA4  token=0x60009C4  Google.Protobuf.Reflection.MethodOptions <.cctor>b__57_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000E2
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.UninterpretedOption.Types.NamePart.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B1A310  token=0x6000A08  System.Void .cctor()
  RVA=0x041E1670  token=0x6000A09  System.Void .ctor()
  RVA=0x09B19810  token=0x6000A0A  Google.Protobuf.Reflection.UninterpretedOption.Types.NamePart <.cctor>b__41_0()
END_CLASS

CLASS: NamePart
TYPE:  sealed class
TOKEN: 0x20000E1
SIZE:  0x30
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.UninterpretedOption.Types.NamePart>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  private           System.Int32                    _hasBits0  // 0x18
  public    static  System.Int32                    NamePart_FieldNumber  // const
  private   static readonly System.String                   NamePart_DefaultValue  // static @ 0x8
  private           System.String                   namePart_  // 0x20
  public    static  System.Int32                    IsExtensionFieldNumber  // const
  private   static readonly System.Boolean                  IsExtensionDefaultValue  // static @ 0x10
  private           System.Boolean                  isExtension_  // 0x28
PROPERTIES:
  Parser  get=0x09B16F28
  Descriptor  get=0x09B16E20
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B17094
  NamePart_  get=0x09B16ED4  set=0x09B170D4
  HasNamePart_  get=0x09B00070
  IsExtension  get=0x09B16E80  set=0x04DA10E0
  HasIsExtension  get=0x0314ECE0
METHODS:
  RVA=0x041E1670  token=0x60009F2  System.Void .ctor()
  RVA=0x09B16DCC  token=0x60009F3  System.Void .ctor(Google.Protobuf.Reflection.UninterpretedOption.Types.NamePart other)
  RVA=0x09B169B8  token=0x60009F4  Google.Protobuf.Reflection.UninterpretedOption.Types.NamePart Clone()
  RVA=0x09AFF598  token=0x60009F8  System.Void ClearNamePart_()
  RVA=0x04D86DE0  token=0x60009FC  System.Void ClearIsExtension()
  RVA=0x09B16A18  token=0x60009FD  System.Boolean Equals(System.Object other)
  RVA=0x09B16A7C  token=0x60009FE  System.Boolean Equals(Google.Protobuf.Reflection.UninterpretedOption.Types.NamePart other)
  RVA=0x09B16B04  token=0x60009FF  System.Int32 GetHashCode()
  RVA=0x09B16C24  token=0x6000A00  System.String ToString()
  RVA=0x04D52400  token=0x6000A01  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09B1700C  token=0x6000A02  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09B16934  token=0x6000A03  System.Int32 CalculateSize()
  RVA=0x09B16BB0  token=0x6000A04  System.Void MergeFrom(Google.Protobuf.Reflection.UninterpretedOption.Types.NamePart other)
  RVA=0x04D51840  token=0x6000A05  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09B16F78  token=0x6000A06  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09B16C70  token=0x6000A07  System.Void .cctor()
END_CLASS

CLASS: Types
TYPE:  static class
TOKEN: 0x20000E0
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000E3
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.UninterpretedOption.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B1A054  token=0x6000A0B  System.Void .cctor()
  RVA=0x041E1670  token=0x6000A0C  System.Void .ctor()
  RVA=0x09B19B80  token=0x6000A0D  Google.Protobuf.Reflection.UninterpretedOption <.cctor>b__83_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000E7
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.SourceCodeInfo.Types.Location.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B19CD0  token=0x6000A3C  System.Void .cctor()
  RVA=0x041E1670  token=0x6000A3D  System.Void .ctor()
  RVA=0x09B199B4  token=0x6000A3E  Google.Protobuf.Reflection.SourceCodeInfo.Types.Location <.cctor>b__55_0()
END_CLASS

CLASS: Location
TYPE:  sealed class
TOKEN: 0x20000E6
SIZE:  0x40
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.SourceCodeInfo.Types.Location>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    PathFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<System.Int32>_repeated_path_codec  // static @ 0x8
  private   readonly Google.Protobuf.Collections.RepeatedField<System.Int32>path_  // 0x18
  public    static  System.Int32                    SpanFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<System.Int32>_repeated_span_codec  // static @ 0x10
  private   readonly Google.Protobuf.Collections.RepeatedField<System.Int32>span_  // 0x20
  public    static  System.Int32                    LeadingCommentsFieldNumber  // const
  private   static readonly System.String                   LeadingCommentsDefaultValue  // static @ 0x18
  private           System.String                   leadingComments_  // 0x28
  public    static  System.Int32                    TrailingCommentsFieldNumber  // const
  private   static readonly System.String                   TrailingCommentsDefaultValue  // static @ 0x20
  private           System.String                   trailingComments_  // 0x30
  public    static  System.Int32                    LeadingDetachedCommentsFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<System.String>_repeated_leadingDetachedComments_codec  // static @ 0x28
  private   readonly Google.Protobuf.Collections.RepeatedField<System.String>leadingDetachedComments_  // 0x38
PROPERTIES:
  Parser  get=0x09B13D3C
  Descriptor  get=0x09B13C88
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B1412C
  Path  get=0x0385B100
  Span  get=0x04D862C0
  LeadingComments  get=0x09B13CE8  set=0x09B1416C
  HasLeadingComments  get=0x068C60A8
  TrailingComments  get=0x09B13D8C  set=0x09B141D4
  HasTrailingComments  get=0x09B11948
  LeadingDetachedComments  get=0x04D85A50
METHODS:
  RVA=0x09B13A94  token=0x6000A23  System.Void .ctor()
  RVA=0x09B13B88  token=0x6000A24  System.Void .ctor(Google.Protobuf.Reflection.SourceCodeInfo.Types.Location other)
  RVA=0x09B13454  token=0x6000A25  Google.Protobuf.Reflection.SourceCodeInfo.Types.Location Clone()
  RVA=0x09849400  token=0x6000A2B  System.Void ClearLeadingComments()
  RVA=0x067B5A00  token=0x6000A2F  System.Void ClearTrailingComments()
  RVA=0x09B134B4  token=0x6000A31  System.Boolean Equals(System.Object other)
  RVA=0x09B13518  token=0x6000A32  System.Boolean Equals(Google.Protobuf.Reflection.SourceCodeInfo.Types.Location other)
  RVA=0x09B13650  token=0x6000A33  System.Int32 GetHashCode()
  RVA=0x09B13830  token=0x6000A34  System.String ToString()
  RVA=0x04D52400  token=0x6000A35  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09B13FB8  token=0x6000A36  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09B132D8  token=0x6000A37  System.Int32 CalculateSize()
  RVA=0x09B13730  token=0x6000A38  System.Void MergeFrom(Google.Protobuf.Reflection.SourceCodeInfo.Types.Location other)
  RVA=0x04D51840  token=0x6000A39  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09B13DE0  token=0x6000A3A  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09B1387C  token=0x6000A3B  System.Void .cctor()
END_CLASS

CLASS: Types
TYPE:  static class
TOKEN: 0x20000E5
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000E8
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.SourceCodeInfo.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B19F28  token=0x6000A3F  System.Void .cctor()
  RVA=0x041E1670  token=0x6000A40  System.Void .ctor()
  RVA=0x09B19720  token=0x6000A41  Google.Protobuf.Reflection.SourceCodeInfo <.cctor>b__28_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000EC
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.GeneratedCodeInfo.Types.Annotation.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B1A374  token=0x6000A72  System.Void .cctor()
  RVA=0x041E1670  token=0x6000A73  System.Void .ctor()
  RVA=0x09B19A04  token=0x6000A74  Google.Protobuf.Reflection.GeneratedCodeInfo.Types.Annotation <.cctor>b__55_0()
END_CLASS

CLASS: Annotation
TYPE:  sealed class
TOKEN: 0x20000EB
SIZE:  0x38
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.GeneratedCodeInfo.Types.Annotation>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  private           System.Int32                    _hasBits0  // 0x18
  public    static  System.Int32                    PathFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<System.Int32>_repeated_path_codec  // static @ 0x8
  private   readonly Google.Protobuf.Collections.RepeatedField<System.Int32>path_  // 0x20
  public    static  System.Int32                    SourceFileFieldNumber  // const
  private   static readonly System.String                   SourceFileDefaultValue  // static @ 0x10
  private           System.String                   sourceFile_  // 0x28
  public    static  System.Int32                    BeginFieldNumber  // const
  private   static readonly System.Int32                    BeginDefaultValue  // static @ 0x18
  private           System.Int32                    begin_  // 0x30
  public    static  System.Int32                    EndFieldNumber  // const
  private   static readonly System.Int32                    EndDefaultValue  // static @ 0x1c
  private           System.Int32                    end_  // 0x34
PROPERTIES:
  Parser  get=0x09B09EDC
  Descriptor  get=0x09B09E28
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B0A1F8
  Path  get=0x04D862C0
  SourceFile  get=0x09B09F2C  set=0x09B0A238
  HasSourceFile  get=0x068C60A8
  Begin  get=0x09B09DD4  set=0x04DA0EA0
  HasBegin  get=0x0314ECE0
  End  get=0x09B09E88  set=0x04DA0EB0
  HasEnd  get=0x09AFF0A8
METHODS:
  RVA=0x09B09D5C  token=0x6000A57  System.Void .ctor()
  RVA=0x09B09CB0  token=0x6000A58  System.Void .ctor(Google.Protobuf.Reflection.GeneratedCodeInfo.Types.Annotation other)
  RVA=0x09B097AC  token=0x6000A59  Google.Protobuf.Reflection.GeneratedCodeInfo.Types.Annotation Clone()
  RVA=0x09849400  token=0x6000A5E  System.Void ClearSourceFile()
  RVA=0x04D86DE0  token=0x6000A62  System.Void ClearBegin()
  RVA=0x04DA0DF0  token=0x6000A66  System.Void ClearEnd()
  RVA=0x09B098F8  token=0x6000A67  System.Boolean Equals(System.Object other)
  RVA=0x09B0980C  token=0x6000A68  System.Boolean Equals(Google.Protobuf.Reflection.GeneratedCodeInfo.Types.Annotation other)
  RVA=0x09B0995C  token=0x6000A69  System.Int32 GetHashCode()
  RVA=0x09B09AC0  token=0x6000A6A  System.String ToString()
  RVA=0x04D52400  token=0x6000A6B  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09B0A0D0  token=0x6000A6C  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09B09678  token=0x6000A6D  System.Int32 CalculateSize()
  RVA=0x09B099F4  token=0x6000A6E  System.Void MergeFrom(Google.Protobuf.Reflection.GeneratedCodeInfo.Types.Annotation other)
  RVA=0x04D51840  token=0x6000A6F  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09B09F80  token=0x6000A70  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09B09B0C  token=0x6000A71  System.Void .cctor()
END_CLASS

CLASS: Types
TYPE:  static class
TOKEN: 0x20000EA
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000ED
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.GeneratedCodeInfo.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x09B1A11C  token=0x6000A75  System.Void .cctor()
  RVA=0x041E1670  token=0x6000A76  System.Void .ctor()
  RVA=0x09B196D0  token=0x6000A77  Google.Protobuf.Reflection.GeneratedCodeInfo <.cctor>b__28_0()
END_CLASS

CLASS: IndexedConverter`2
TYPE:  sealed class
TOKEN: 0x20000F2
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000A96  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000A97  TOutput Invoke(TInput element, System.Int32 index)
  RVA=-1  // runtime  token=0x6000A98  System.IAsyncResult BeginInvoke(TInput element, System.Int32 index, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x6000A99  TOutput EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x20000F5
SIZE:  0x20
FIELDS:
  public            Google.Protobuf.Reflection.FileDescriptorfile  // 0x10
  public            Google.Protobuf.Reflection.EnumDescriptor<>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000AAC  System.Void .ctor()
  RVA=0x09B19BD0  token=0x6000AAD  Google.Protobuf.Reflection.EnumValueDescriptor <.ctor>b__0(Google.Protobuf.Reflection.EnumValueDescriptorProto value, System.Int32 i)
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x20000F9
SIZE:  0x20
FIELDS:
  public            Google.Protobuf.Extension[]     extensions  // 0x10
  public            Google.Protobuf.Reflection.FileDescriptorfile  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000AC4  System.Void .ctor()
  RVA=0x09B246E0  token=0x6000AC5  Google.Protobuf.Reflection.FieldDescriptor <.ctor>b__0(Google.Protobuf.Reflection.FieldDescriptorProto extension, System.Int32 i)
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x20000FA
SIZE:  0x20
FIELDS:
  public            Google.Protobuf.Extension[]     extensions  // 0x10
  public            Google.Protobuf.Reflection.MessageDescriptormessage  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000AC6  System.Void .ctor()
  RVA=0x09B24AF8  token=0x6000AC7  Google.Protobuf.Reflection.FieldDescriptor <.ctor>b__0(Google.Protobuf.Reflection.FieldDescriptorProto extension, System.Int32 i)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000FB
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.ExtensionCollection.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Collections.Generic.KeyValuePair<Google.Protobuf.Reflection.MessageDescriptor,System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor>>,Google.Protobuf.Reflection.MessageDescriptor><>9__9_0  // static @ 0x8
  public    static  System.Func<System.Collections.Generic.KeyValuePair<Google.Protobuf.Reflection.MessageDescriptor,System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor>>,System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor>><>9__9_1  // static @ 0x10
  public    static  System.Func<System.Collections.Generic.KeyValuePair<Google.Protobuf.Reflection.MessageDescriptor,System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor>>,Google.Protobuf.Reflection.MessageDescriptor><>9__9_2  // static @ 0x18
  public    static  System.Func<Google.Protobuf.Reflection.FieldDescriptor,System.Int32><>9__9_4  // static @ 0x20
  public    static  System.Func<System.Collections.Generic.KeyValuePair<Google.Protobuf.Reflection.MessageDescriptor,System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor>>,System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor>><>9__9_3  // static @ 0x28
METHODS:
  RVA=0x09B25134  token=0x6000AC8  System.Void .cctor()
  RVA=0x041E1670  token=0x6000AC9  System.Void .ctor()
  RVA=0x09B243B8  token=0x6000ACA  Google.Protobuf.Reflection.MessageDescriptor <CrossLink>b__9_0(System.Collections.Generic.KeyValuePair<Google.Protobuf.Reflection.MessageDescriptor,System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor>> kvp)
  RVA=0x09B243EC  token=0x6000ACB  System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor> <CrossLink>b__9_1(System.Collections.Generic.KeyValuePair<Google.Protobuf.Reflection.MessageDescriptor,System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor>> kvp)
  RVA=0x09B24468  token=0x6000ACC  Google.Protobuf.Reflection.MessageDescriptor <CrossLink>b__9_2(System.Collections.Generic.KeyValuePair<Google.Protobuf.Reflection.MessageDescriptor,System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor>> kvp)
  RVA=0x09B2449C  token=0x6000ACD  System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor> <CrossLink>b__9_3(System.Collections.Generic.KeyValuePair<Google.Protobuf.Reflection.MessageDescriptor,System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor>> kvp)
  RVA=0x09B24614  token=0x6000ACE  System.Int32 <CrossLink>b__9_4(Google.Protobuf.Reflection.FieldDescriptor field)
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x2000101
SIZE:  0x20
FIELDS:
  public            Google.Protobuf.Reflection.FileDescriptor<>4__this  // 0x10
  public            Google.Protobuf.Reflection.GeneratedClrTypeInfogeneratedCodeInfo  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000B1C  System.Void .ctor()
  RVA=0x09B24890  token=0x6000B1D  Google.Protobuf.Reflection.MessageDescriptor <.ctor>b__0(Google.Protobuf.Reflection.DescriptorProto message, System.Int32 index)
  RVA=0x09B24950  token=0x6000B1E  Google.Protobuf.Reflection.EnumDescriptor <.ctor>b__1(Google.Protobuf.Reflection.EnumDescriptorProto enumType, System.Int32 index)
  RVA=0x09B24A10  token=0x6000B1F  Google.Protobuf.Reflection.ServiceDescriptor <.ctor>b__2(Google.Protobuf.Reflection.ServiceDescriptorProto service, System.Int32 index)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000102
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.FileDescriptor.<>c<>9  // static @ 0x0
  public    static  System.Func<Google.Protobuf.Reflection.FileDescriptor,System.String><>9__9_0  // static @ 0x8
  public    static  System.Func<Google.Protobuf.Reflection.GeneratedClrTypeInfo,System.Boolean><>9__54_0  // static @ 0x10
  public    static  System.Func<Google.Protobuf.Reflection.FieldDescriptor,Google.Protobuf.Extension><>9__55_0  // static @ 0x18
  public    static  System.Func<Google.Protobuf.Extension,System.Boolean><>9__55_1  // static @ 0x20
  public    static  System.Func<Google.Protobuf.Reflection.FieldDescriptor,Google.Protobuf.Extension><>9__56_0  // static @ 0x28
  public    static  System.Func<Google.Protobuf.Extension,System.Boolean><>9__56_1  // static @ 0x30
METHODS:
  RVA=0x09B252C4  token=0x6000B20  System.Void .cctor()
  RVA=0x041E1670  token=0x6000B21  System.Void .ctor()
  RVA=0x09B24634  token=0x6000B22  System.String <DeterminePublicDependencies>b__9_0(Google.Protobuf.Reflection.FileDescriptor file)
  RVA=0x0721682C  token=0x6000B23  System.Boolean <GetAllGeneratedExtensions>b__54_0(Google.Protobuf.Reflection.GeneratedClrTypeInfo t)
  RVA=0x09B2466C  token=0x6000B24  Google.Protobuf.Extension <GetAllDependedExtensions>b__55_0(Google.Protobuf.Reflection.FieldDescriptor s)
  RVA=0x0721682C  token=0x6000B25  System.Boolean <GetAllDependedExtensions>b__55_1(Google.Protobuf.Extension e)
  RVA=0x09B2466C  token=0x6000B26  Google.Protobuf.Extension <GetAllDependedExtensionsFromMessage>b__56_0(Google.Protobuf.Reflection.FieldDescriptor s)
  RVA=0x0721682C  token=0x6000B27  System.Boolean <GetAllDependedExtensionsFromMessage>b__56_1(Google.Protobuf.Extension e)
END_CLASS

CLASS: FieldCollection
TYPE:  sealed class
TOKEN: 0x2000108
SIZE:  0x18
FIELDS:
  private   readonly Google.Protobuf.Reflection.MessageDescriptormessageDescriptor  // 0x10
PROPERTIES:
  Item  get=0x09B1C140
  Item  get=0x09B1C140
METHODS:
  RVA=0x053908C0  token=0x6000B5C  System.Void .ctor(Google.Protobuf.Reflection.MessageDescriptor messageDescriptor)
  RVA=0x09B1C124  token=0x6000B5D  System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor> InDeclarationOrder()
  RVA=0x08110B18  token=0x6000B5E  System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor> InFieldNumberOrder()
  RVA=0x09B1C108  token=0x6000B5F  System.Collections.Generic.IDictionary<System.String,Google.Protobuf.Reflection.FieldDescriptor> ByJsonName()
END_CLASS

CLASS: <>c__DisplayClass5_0
TYPE:  sealed class
TOKEN: 0x2000109
SIZE:  0x28
FIELDS:
  public            Google.Protobuf.Reflection.FileDescriptorfile  // 0x10
  public            Google.Protobuf.Reflection.MessageDescriptor<>4__this  // 0x18
  public            Google.Protobuf.Reflection.GeneratedClrTypeInfogeneratedCodeInfo  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000B62  System.Void .ctor()
  RVA=0x09B24E20  token=0x6000B63  Google.Protobuf.Reflection.OneofDescriptor <.ctor>b__0(Google.Protobuf.Reflection.OneofDescriptorProto oneof, System.Int32 index)
  RVA=0x09B24EE4  token=0x6000B64  Google.Protobuf.Reflection.MessageDescriptor <.ctor>b__1(Google.Protobuf.Reflection.DescriptorProto type, System.Int32 index)
  RVA=0x09B24FA8  token=0x6000B65  Google.Protobuf.Reflection.EnumDescriptor <.ctor>b__2(Google.Protobuf.Reflection.EnumDescriptorProto type, System.Int32 index)
  RVA=0x09B2506C  token=0x6000B66  Google.Protobuf.Reflection.FieldDescriptor <.ctor>b__3(Google.Protobuf.Reflection.FieldDescriptorProto field, System.Int32 index)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200010A
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.MessageDescriptor.<>c<>9  // static @ 0x0
  public    static  System.Func<Google.Protobuf.Reflection.FieldDescriptor,System.Int32><>9__5_4  // static @ 0x8
METHODS:
  RVA=0x09B25260  token=0x6000B67  System.Void .cctor()
  RVA=0x041E1670  token=0x6000B68  System.Void .ctor()
  RVA=0x09B24614  token=0x6000B69  System.Int32 <.ctor>b__5_4(Google.Protobuf.Reflection.FieldDescriptor field)
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x200010D
SIZE:  0x18
FIELDS:
  public            Google.Protobuf.Reflection.OneofDescriptordescriptor  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000B7E  System.Void .ctor()
  RVA=0x09B24D1C  token=0x6000B7F  System.Int32 <ForSyntheticOneof>b__0(Google.Protobuf.IMessage message)
  RVA=0x09B24DB4  token=0x6000B80  System.Void <ForSyntheticOneof>b__1(Google.Protobuf.IMessage message)
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x200010F
SIZE:  0x18
FIELDS:
  public            System.Int32                    index  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000B8F  System.Void .ctor()
  RVA=0x09B24A98  token=0x6000B90  System.Boolean <.ctor>b__0(Google.Protobuf.Reflection.FieldDescriptorProto fieldProto)
END_CLASS

CLASS: IReflectionHelper
TYPE:  interface
TOKEN: 0x2000113
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000BA7  System.Func<Google.Protobuf.IMessage,System.Int32> CreateFuncIMessageInt32(System.Reflection.MethodInfo method)
  RVA=-1  // abstract  token=0x6000BA8  System.Action<Google.Protobuf.IMessage> CreateActionIMessage(System.Reflection.MethodInfo method)
  RVA=-1  // abstract  token=0x6000BA9  System.Func<Google.Protobuf.IMessage,System.Object> CreateFuncIMessageObject(System.Reflection.MethodInfo method)
  RVA=-1  // abstract  token=0x6000BAA  System.Action<Google.Protobuf.IMessage,System.Object> CreateActionIMessageObject(System.Reflection.MethodInfo method)
  RVA=-1  // abstract  token=0x6000BAB  System.Func<Google.Protobuf.IMessage,System.Boolean> CreateFuncIMessageBool(System.Reflection.MethodInfo method)
END_CLASS

CLASS: IExtensionReflectionHelper
TYPE:  interface
TOKEN: 0x2000114
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000BAC  System.Object GetExtension(Google.Protobuf.IMessage message)
  RVA=-1  // abstract  token=0x6000BAD  System.Void SetExtension(Google.Protobuf.IMessage message, System.Object value)
  RVA=-1  // abstract  token=0x6000BAE  System.Boolean HasExtension(Google.Protobuf.IMessage message)
  RVA=-1  // abstract  token=0x6000BAF  System.Void ClearExtension(Google.Protobuf.IMessage message)
END_CLASS

CLASS: IExtensionSetReflector
TYPE:  interface
TOKEN: 0x2000115
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000BB0  System.Func<Google.Protobuf.IMessage,System.Boolean> CreateIsInitializedCaller()
END_CLASS

CLASS: <>c__DisplayClass0_0
TYPE:  sealed class
TOKEN: 0x2000117
FIELDS:
  public            System.Func<T1,System.Int32>    del  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000BB7  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000BB8  System.Int32 <CreateFuncIMessageInt32>b__0(Google.Protobuf.IMessage message)
END_CLASS

CLASS: <>c__DisplayClass0_1
TYPE:  sealed class
TOKEN: 0x2000118
FIELDS:
  public            System.Func<T1,T2>              del  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000BB9  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000BBA  System.Int32 <CreateFuncIMessageInt32>b__1(Google.Protobuf.IMessage message)
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x2000119
FIELDS:
  public            System.Action<T1>               del  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000BBB  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000BBC  System.Void <CreateActionIMessage>b__0(Google.Protobuf.IMessage message)
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x200011A
FIELDS:
  public            System.Func<T1,T2>              del  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000BBD  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000BBE  System.Object <CreateFuncIMessageObject>b__0(Google.Protobuf.IMessage message)
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x200011B
FIELDS:
  public            System.Action<T1,T2>            del  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000BBF  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000BC0  System.Void <CreateActionIMessageObject>b__0(Google.Protobuf.IMessage message, System.Object arg)
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x200011C
FIELDS:
  public            System.Func<T1,System.Boolean>  del  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000BC1  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000BC2  System.Boolean <CreateFuncIMessageBool>b__0(Google.Protobuf.IMessage message)
END_CLASS

CLASS: ReflectionHelper`2
TYPE:  class
TOKEN: 0x2000116
IMPLEMENTS: IReflectionHelper
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000BB1  System.Func<Google.Protobuf.IMessage,System.Int32> CreateFuncIMessageInt32(System.Reflection.MethodInfo method)
  RVA=-1  // not resolved  token=0x6000BB2  System.Action<Google.Protobuf.IMessage> CreateActionIMessage(System.Reflection.MethodInfo method)
  RVA=-1  // not resolved  token=0x6000BB3  System.Func<Google.Protobuf.IMessage,System.Object> CreateFuncIMessageObject(System.Reflection.MethodInfo method)
  RVA=-1  // not resolved  token=0x6000BB4  System.Action<Google.Protobuf.IMessage,System.Object> CreateActionIMessageObject(System.Reflection.MethodInfo method)
  RVA=-1  // not resolved  token=0x6000BB5  System.Func<Google.Protobuf.IMessage,System.Boolean> CreateFuncIMessageBool(System.Reflection.MethodInfo method)
  RVA=-1  // not resolved  token=0x6000BB6  System.Void .ctor()
END_CLASS

CLASS: ExtensionReflectionHelper`2
TYPE:  class
TOKEN: 0x200011D
IMPLEMENTS: IExtensionReflectionHelper
FIELDS:
  private   readonly Google.Protobuf.Extension       extension  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000BC3  System.Void .ctor(Google.Protobuf.Extension extension)
  RVA=-1  // not resolved  token=0x6000BC4  System.Object GetExtension(Google.Protobuf.IMessage message)
  RVA=-1  // not resolved  token=0x6000BC5  System.Boolean HasExtension(Google.Protobuf.IMessage message)
  RVA=-1  // not resolved  token=0x6000BC6  System.Void SetExtension(Google.Protobuf.IMessage message, System.Object value)
  RVA=-1  // not resolved  token=0x6000BC7  System.Void ClearExtension(Google.Protobuf.IMessage message)
END_CLASS

CLASS: <>c__DisplayClass0_0
TYPE:  sealed class
TOKEN: 0x200011F
FIELDS:
  public            System.Func<T1,Google.Protobuf.ExtensionSet<T1>>getFunc  // 0x0
  public            System.Func<Google.Protobuf.ExtensionSet<T1>,System.Boolean>initializedFunc  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000BCA  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000BCB  System.Boolean <CreateIsInitializedCaller>b__0(Google.Protobuf.IMessage m)
END_CLASS

CLASS: ExtensionSetReflector`1
TYPE:  class
TOKEN: 0x200011E
IMPLEMENTS: IExtensionSetReflector
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000BC8  System.Func<Google.Protobuf.IMessage,System.Boolean> CreateIsInitializedCaller()
  RVA=-1  // not resolved  token=0x6000BC9  System.Void .ctor()
END_CLASS

CLASS: SampleEnum
TYPE:  sealed struct
TOKEN: 0x2000120
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Google.Protobuf.Reflection.ReflectionUtil.SampleEnumX  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x2000123
SIZE:  0x20
FIELDS:
  public            Google.Protobuf.Reflection.FileDescriptorfile  // 0x10
  public            Google.Protobuf.Reflection.ServiceDescriptor<>4__this  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000BDC  System.Void .ctor()
  RVA=0x09B247F4  token=0x6000BDD  Google.Protobuf.Reflection.MethodDescriptor <.ctor>b__0(Google.Protobuf.Reflection.MethodDescriptorProto method, System.Int32 i)
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x2000125
SIZE:  0x20
FIELDS:
  public            Google.Protobuf.Reflection.SingleFieldAccessor<>4__this  // 0x10
  public            Google.Protobuf.Reflection.FieldDescriptordescriptor  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000BE3  System.Void .ctor()
  RVA=0x09B24AD0  token=0x6000BE4  System.Boolean <.ctor>b__0(Google.Protobuf.IMessage message)
  RVA=0x09B24C30  token=0x6000BE5  System.Void <.ctor>b__1(Google.Protobuf.IMessage message)
END_CLASS

CLASS: <>c__DisplayClass3_1
TYPE:  sealed class
TOKEN: 0x2000126
SIZE:  0x20
FIELDS:
  public            Google.Protobuf.Reflection.OneofAccessoroneofAccessor  // 0x10
  public            Google.Protobuf.Reflection.SingleFieldAccessor.<>c__DisplayClass3_0CS$<>8__locals1  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000BE6  System.Void .ctor()
  RVA=0x09B24C58  token=0x6000BE7  System.Boolean <.ctor>b__2(Google.Protobuf.IMessage message)
  RVA=0x09B24C90  token=0x6000BE8  System.Void <.ctor>b__3(Google.Protobuf.IMessage message)
END_CLASS

CLASS: <>c__DisplayClass3_2
TYPE:  sealed class
TOKEN: 0x2000127
SIZE:  0x20
FIELDS:
  public            System.Object                   defaultValue  // 0x10
  public            Google.Protobuf.Reflection.SingleFieldAccessor.<>c__DisplayClass3_0CS$<>8__locals2  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000BE9  System.Void .ctor()
  RVA=0x09B24CE8  token=0x6000BEA  System.Void <.ctor>b__5(Google.Protobuf.IMessage message)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000128
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.SingleFieldAccessor.<>c<>9  // static @ 0x0
  public    static  System.Func<Google.Protobuf.IMessage,System.Boolean><>9__3_4  // static @ 0x8
METHODS:
  RVA=0x09B251FC  token=0x6000BEB  System.Void .cctor()
  RVA=0x041E1670  token=0x6000BEC  System.Void .ctor()
  RVA=0x09B24684  token=0x6000BED  System.Boolean <.ctor>b__3_4(Google.Protobuf.IMessage message)
END_CLASS

CLASS: Builder
TYPE:  class
TOKEN: 0x200012A
SIZE:  0x20
FIELDS:
  private   readonly System.Collections.Generic.Dictionary<System.String,Google.Protobuf.Reflection.MessageDescriptor>types  // 0x10
  private   readonly System.Collections.Generic.HashSet<System.String>fileDescriptorNames  // 0x18
METHODS:
  RVA=0x09B1BF9C  token=0x6000BF6  System.Void .ctor()
  RVA=0x09B1BAF4  token=0x6000BF7  System.Void AddFile(Google.Protobuf.Reflection.FileDescriptor fileDescriptor)
  RVA=0x09B1BDB4  token=0x6000BF8  System.Void AddMessage(Google.Protobuf.Reflection.MessageDescriptor messageDescriptor)
  RVA=0x09B1BF38  token=0x6000BF9  Google.Protobuf.Reflection.TypeRegistry Build()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200012B
SIZE:  0x10
FIELDS:
  public    static readonly Google.Protobuf.Reflection.TypeRegistry.<>c<>9  // static @ 0x0
  public    static  System.Func<Google.Protobuf.Reflection.MessageDescriptor,Google.Protobuf.Reflection.FileDescriptor><>9__9_0  // static @ 0x8
METHODS:
  RVA=0x09B25198  token=0x6000BFA  System.Void .cctor()
  RVA=0x041E1670  token=0x6000BFB  System.Void .ctor()
  RVA=0x09B24654  token=0x6000BFC  Google.Protobuf.Reflection.FileDescriptor <FromMessages>b__9_0(Google.Protobuf.Reflection.MessageDescriptor md)
END_CLASS

CLASS: DictionaryEnumerator
TYPE:  class
TOKEN: 0x2000130
IMPLEMENTS: System.Collections.IDictionaryEnumerator System.Collections.IEnumerator
FIELDS:
  private   readonly System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey,TValue>>enumerator  // 0x0
PROPERTIES:
  Current  get=-1  // not resolved
  Entry  get=-1  // not resolved
  Key  get=-1  // not resolved
  Value  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000C32  System.Void .ctor(System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey,TValue>> enumerator)
  RVA=-1  // not resolved  token=0x6000C33  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x6000C34  System.Void Reset()
END_CLASS

CLASS: Codec
TYPE:  sealed class
TOKEN: 0x2000131
FIELDS:
  private   readonly Google.Protobuf.FieldCodec<TKey>keyCodec  // 0x0
  private   readonly Google.Protobuf.FieldCodec<TValue>valueCodec  // 0x0
  private   readonly System.UInt32                   mapTag  // 0x0
PROPERTIES:
  KeyCodec  get=-1  // not resolved
  ValueCodec  get=-1  // not resolved
  MapTag  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000C39  System.Void .ctor(Google.Protobuf.FieldCodec<TKey> keyCodec, Google.Protobuf.FieldCodec<TValue> valueCodec, System.UInt32 mapTag)
END_CLASS

CLASS: MapView`1
TYPE:  class
TOKEN: 0x2000132
IMPLEMENTS: System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.ICollection
FIELDS:
  private   readonly Google.Protobuf.Collections.MapField<TKey,TValue>parent  // 0x0
  private   readonly System.Func<System.Collections.Generic.KeyValuePair<TKey,TValue>,T>projection  // 0x0
  private   readonly System.Func<T,System.Boolean>   containsCheck  // 0x0
PROPERTIES:
  Count  get=-1  // not resolved
  IsReadOnly  get=-1  // not resolved
  IsSynchronized  get=-1  // not resolved
  SyncRoot  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000C3D  System.Void .ctor(Google.Protobuf.Collections.MapField<TKey,TValue> parent, System.Func<System.Collections.Generic.KeyValuePair<TKey,TValue>,T> projection, System.Func<T,System.Boolean> containsCheck)
  RVA=-1  // not resolved  token=0x6000C42  System.Void Add(T item)
  RVA=-1  // not resolved  token=0x6000C43  System.Void Clear()
  RVA=-1  // not resolved  token=0x6000C44  System.Boolean Contains(T item)
  RVA=-1  // not resolved  token=0x6000C45  System.Void CopyTo(T[] array, System.Int32 arrayIndex)
  RVA=-1  // not resolved  token=0x6000C46  System.Collections.Generic.IEnumerator<T> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000C47  System.Boolean Remove(T item)
  RVA=-1  // not resolved  token=0x6000C48  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000C49  System.Void CopyTo(System.Array array, System.Int32 index)
END_CLASS

CLASS: <>c__DisplayClass7_0
TYPE:  sealed class
TOKEN: 0x2000133
FIELDS:
  public            TValue                          value  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000C4A  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000C4B  System.Boolean <ContainsValue>b__0(System.Collections.Generic.KeyValuePair<TKey,TValue> pair)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000134
FIELDS:
  public    static readonly Google.Protobuf.Collections.MapField.<>c<TKey,TValue><>9  // static @ 0x0
  public    static  System.Func<System.Collections.Generic.KeyValuePair<TKey,TValue>,TKey><>9__14_0  // static @ 0x0
  public    static  System.Func<System.Collections.Generic.KeyValuePair<TKey,TValue>,TValue><>9__16_0  // static @ 0x0
  public    static  System.Func<System.Collections.Generic.KeyValuePair<TKey,TValue>,System.Collections.DictionaryEntry><>9__43_0  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000C4C  System.Void .cctor()
  RVA=-1  // not resolved  token=0x6000C4D  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000C4E  TKey <get_Keys>b__14_0(System.Collections.Generic.KeyValuePair<TKey,TValue> pair)
  RVA=-1  // not resolved  token=0x6000C4F  TValue <get_Values>b__16_0(System.Collections.Generic.KeyValuePair<TKey,TValue> pair)
  RVA=-1  // not resolved  token=0x6000C50  System.Collections.DictionaryEntry <System.Collections.ICollection.CopyTo>b__43_0(System.Collections.Generic.KeyValuePair<TKey,TValue> pair)
END_CLASS

CLASS: BitwiseDoubleEqualityComparerImpl
TYPE:  class
TOKEN: 0x2000136
SIZE:  0x10
EXTENDS: System.Collections.Generic.EqualityComparer`1
FIELDS:
METHODS:
  RVA=0x09B1B6B8  token=0x6000C57  System.Boolean Equals(System.Double x, System.Double y)
  RVA=0x09B1B714  token=0x6000C58  System.Int32 GetHashCode(System.Double obj)
  RVA=0x049DEDE0  token=0x6000C59  System.Void .ctor()
END_CLASS

CLASS: BitwiseSingleEqualityComparerImpl
TYPE:  class
TOKEN: 0x2000137
SIZE:  0x10
EXTENDS: System.Collections.Generic.EqualityComparer`1
FIELDS:
METHODS:
  RVA=0x09B1BA34  token=0x6000C5A  System.Boolean Equals(System.Single x, System.Single y)
  RVA=0x09B1BAA0  token=0x6000C5B  System.Int32 GetHashCode(System.Single obj)
  RVA=0x049DEDB0  token=0x6000C5C  System.Void .ctor()
END_CLASS

CLASS: BitwiseNullableDoubleEqualityComparerImpl
TYPE:  class
TOKEN: 0x2000138
SIZE:  0x10
EXTENDS: System.Collections.Generic.EqualityComparer`1
FIELDS:
METHODS:
  RVA=0x09B1B76C  token=0x6000C5D  System.Boolean Equals(System.Nullable<System.Double> x, System.Nullable<System.Double> y)
  RVA=0x09B1B834  token=0x6000C5E  System.Int32 GetHashCode(System.Nullable<System.Double> obj)
  RVA=0x049DED80  token=0x6000C5F  System.Void .ctor()
END_CLASS

CLASS: BitwiseNullableSingleEqualityComparerImpl
TYPE:  class
TOKEN: 0x2000139
SIZE:  0x10
EXTENDS: System.Collections.Generic.EqualityComparer`1
FIELDS:
METHODS:
  RVA=0x09B1B8CC  token=0x6000C60  System.Boolean Equals(System.Nullable<System.Single> x, System.Nullable<System.Single> y)
  RVA=0x09B1B99C  token=0x6000C61  System.Int32 GetHashCode(System.Nullable<System.Single> obj)
  RVA=0x049DED50  token=0x6000C62  System.Void .ctor()
END_CLASS

CLASS: <GetEnumerator>d__29
TYPE:  sealed class
TOKEN: 0x200013C
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           T                               <>2__current  // 0x0
  public            Google.Protobuf.Collections.RepeatedField<T><>4__this  // 0x0
  private           System.Int32                    <i>5__2  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<T>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000CA5  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x6000CA6  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x6000CA7  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x6000CA9  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: __StaticArrayInitTypeSize=44
TYPE:  sealed struct
TOKEN: 0x200013E
SIZE:  0x3C
FIELDS:
METHODS:
END_CLASS

CLASS: <PrivateImplementationDetails>
TYPE:  sealed class
TOKEN: 0x200013D
SIZE:  0x10
FIELDS:
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=441EFF1891409D680B30DE202BD7A16F945ED09F02601C0E7B90FD917BE1162300  // static @ 0x0
METHODS:
END_CLASS

CLASS: Google.Protobuf.ByteArray
TYPE:  static class
TOKEN: 0x2000006
SIZE:  0x10
FIELDS:
  private   static  System.Int32                    CopyThreshold  // const
METHODS:
  RVA=0x032A8AA0  token=0x6000012  System.Void Copy(System.Byte[] src, System.Int32 srcOffset, System.Byte[] dst, System.Int32 dstOffset, System.Int32 count)
  RVA=0x09ACC204  token=0x6000013  System.Void Reverse(System.Byte[] bytes)
END_CLASS

CLASS: Google.Protobuf.ByteString
TYPE:  sealed class
TOKEN: 0x2000007
SIZE:  0x20
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.IEquatable`1
FIELDS:
  private   static readonly Google.Protobuf.ByteString      empty  // static @ 0x0
  private   readonly System.ReadOnlyMemory<System.Byte>bytes  // 0x10
PROPERTIES:
  Empty  get=0x09ACCEC4
  Length  get=0x04DA0CD0
  IsEmpty  get=0x09ACCEF8
  Span  get=0x053972F0
  Memory  get=0x02B2D510
  Item  get=0x09ACCF04
METHODS:
  RVA=0x032A6AF0  token=0x6000014  Google.Protobuf.ByteString AttachBytes(System.ReadOnlyMemory<System.Byte> bytes)
  RVA=0x032A6A60  token=0x6000015  Google.Protobuf.ByteString AttachBytes(System.Byte[] bytes)
  RVA=0x032A8A90  token=0x6000016  System.Void .ctor(System.ReadOnlyMemory<System.Byte> bytes)
  RVA=0x032196A0  token=0x600001C  System.Byte[] ToByteArray()
  RVA=0x09ACCB90  token=0x600001D  System.String ToBase64()
  RVA=0x09ACC7B4  token=0x600001E  Google.Protobuf.ByteString FromBase64(System.String bytes)
  RVA=0x09ACC8FC  token=0x600001F  Google.Protobuf.ByteString FromStream(System.IO.Stream stream)
  RVA=0x09ACC894  token=0x6000020  System.Threading.Tasks.Task<Google.Protobuf.ByteString> FromStreamAsync(System.IO.Stream stream, System.Threading.CancellationToken cancellationToken)
  RVA=0x04CCA610  token=0x6000021  Google.Protobuf.ByteString CopyFrom(System.Byte[] bytes)
  RVA=0x032A89E0  token=0x6000022  Google.Protobuf.ByteString CopyFrom(System.Byte[] bytes, System.Int32 offset, System.Int32 count)
  RVA=0x09ACC458  token=0x6000023  Google.Protobuf.ByteString CopyFrom(System.ReadOnlySpan<System.Byte> bytes)
  RVA=0x09ACC3B4  token=0x6000024  Google.Protobuf.ByteString CopyFrom(System.String text, System.Text.Encoding encoding)
  RVA=0x09ACC35C  token=0x6000025  Google.Protobuf.ByteString CopyFromUtf8(System.String text)
  RVA=0x09ACCCC8  token=0x6000027  System.String ToString(System.Text.Encoding encoding)
  RVA=0x09ACCCA4  token=0x6000028  System.String ToStringUtf8()
  RVA=0x09ACCA8C  token=0x6000029  System.Collections.Generic.IEnumerator<System.Byte> GetEnumerator()
  RVA=0x09ACCB88  token=0x600002A  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=0x09ACC580  token=0x600002B  Google.Protobuf.CodedInputStream CreateCodedInput()
  RVA=0x03218B90  token=0x600002C  System.Boolean op_Equality(Google.Protobuf.ByteString lhs, Google.Protobuf.ByteString rhs)
  RVA=0x09ACCF64  token=0x600002D  System.Boolean op_Inequality(Google.Protobuf.ByteString lhs, Google.Protobuf.ByteString rhs)
  RVA=0x09ACC6E8  token=0x600002E  System.Boolean Equals(System.Object obj)
  RVA=0x09ACCAFC  token=0x600002F  System.Int32 GetHashCode()
  RVA=0x09ACC758  token=0x6000030  System.Boolean Equals(Google.Protobuf.ByteString other)
  RVA=0x09ACC4F8  token=0x6000031  System.Void CopyTo(System.Byte[] array, System.Int32 position)
  RVA=0x09ACCDC4  token=0x6000032  System.Void WriteTo(System.IO.Stream outputStream)
  RVA=0x04CD7D50  token=0x6000033  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.ByteStringAsync
TYPE:  static class
TOKEN: 0x2000008
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09ACC26C  token=0x6000034  System.Threading.Tasks.Task<Google.Protobuf.ByteString> FromStreamAsyncCore(System.IO.Stream stream, System.Threading.CancellationToken cancellationToken)
END_CLASS

CLASS: Google.Protobuf.CodedInputStream
TYPE:  sealed class
TOKEN: 0x200000A
SIZE:  0xA8
IMPLEMENTS: System.IDisposable
FIELDS:
  private   readonly System.Boolean                  leaveOpen  // 0x10
  private   readonly System.Byte[]                   buffer  // 0x18
  private   readonly System.IO.Stream                input  // 0x20
  private           Google.Protobuf.ParserInternalStatestate  // 0x28
  private   static  System.Int32                    DefaultRecursionLimit  // const
  private   static  System.Int32                    DefaultSizeLimit  // const
  private   static  System.Int32                    BufferSize  // const
PROPERTIES:
  Position  get=0x09ACDD88
  LastTag  get=0x04D86890
  SizeLimit  get=0x04D86B20
  RecursionLimit  get=0x04D862D0
  DiscardUnknownFields  get=0x04D92ED0  set=0x04D92F60
  ExtensionRegistry  get=0x04D86210  set=0x05391334
  InternalBuffer  get=0x0385B100
  InternalInputStream  get=0x04D862C0
  InternalState  get=0x04DA0CE0
  ReachedLimit  get=0x09ACDDC4
  IsAtEnd  get=0x09ACDD24
METHODS:
  RVA=0x09ACDC9C  token=0x6000037  System.Void .ctor(System.Byte[] buffer)
  RVA=0x03143150  token=0x6000038  System.Void .ctor(System.Byte[] buffer, System.Int32 offset, System.Int32 length)
  RVA=0x09ACDAC8  token=0x6000039  System.Void .ctor(System.IO.Stream input)
  RVA=0x09ACDBF4  token=0x600003A  System.Void .ctor(System.IO.Stream input, System.Boolean leaveOpen)
  RVA=0x031437F0  token=0x600003B  System.Void .ctor(System.IO.Stream input, System.Byte[] buffer, System.Int32 bufferPos, System.Int32 bufferSize, System.Boolean leaveOpen)
  RVA=0x09ACDAD4  token=0x600003C  System.Void .ctor(System.IO.Stream input, System.Byte[] buffer, System.Int32 bufferPos, System.Int32 bufferSize, System.Int32 sizeLimit, System.Int32 recursionLimit, System.Boolean leaveOpen)
  RVA=0x09ACCFB8  token=0x600003D  Google.Protobuf.CodedInputStream CreateWithLimits(System.IO.Stream input, System.Int32 sizeLimit, System.Int32 recursionLimit)
  RVA=0x09ACD078  token=0x6000049  System.Void Dispose()
  RVA=0x031437B0  token=0x600004A  System.Void CheckReadEndOfStreamTag()
  RVA=0x09ACD118  token=0x600004B  System.UInt32 PeekTag()
  RVA=0x09ACD8D8  token=0x600004C  System.UInt32 ReadTag()
  RVA=0x09ACDA48  token=0x600004D  System.Void SkipLastField()
  RVA=0x09ACD9BC  token=0x600004E  System.Void SkipGroup(System.UInt32 startGroupTag)
  RVA=0x09ACD218  token=0x600004F  System.Double ReadDouble()
  RVA=0x09ACD298  token=0x6000050  System.Single ReadFloat()
  RVA=0x09ACD3FC  token=0x6000051  System.UInt64 ReadUInt64()
  RVA=0x09ACD3FC  token=0x6000052  System.Int64 ReadInt64()
  RVA=0x09ACD280  token=0x6000053  System.Int32 ReadInt32()
  RVA=0x09ACD290  token=0x6000054  System.UInt64 ReadFixed64()
  RVA=0x09ACD288  token=0x6000055  System.UInt32 ReadFixed32()
  RVA=0x09ACD19C  token=0x6000056  System.Boolean ReadBool()
  RVA=0x09ACD874  token=0x6000057  System.String ReadString()
  RVA=0x09ACD46C  token=0x6000058  System.Void ReadMessage(Google.Protobuf.IMessage builder)
  RVA=0x09ACD300  token=0x6000059  System.Void ReadGroup(Google.Protobuf.IMessage builder)
  RVA=0x09ACD1B4  token=0x600005A  Google.Protobuf.ByteString ReadBytes()
  RVA=0x09ACD280  token=0x600005B  System.UInt32 ReadUInt32()
  RVA=0x09ACD280  token=0x600005C  System.Int32 ReadEnum()
  RVA=0x09ACD288  token=0x600005D  System.Int32 ReadSFixed32()
  RVA=0x09ACD290  token=0x600005E  System.Int64 ReadSFixed64()
  RVA=0x09ACD838  token=0x600005F  System.Int32 ReadSInt32()
  RVA=0x09ACD854  token=0x6000060  System.Int64 ReadSInt64()
  RVA=0x09ACD404  token=0x6000061  System.Int32 ReadLength()
  RVA=0x09ACD0A4  token=0x6000062  System.Boolean MaybeConsumeTag(System.UInt32 tag)
  RVA=0x09ACD768  token=0x6000063  System.UInt32 ReadRawVarint32()
  RVA=0x09ACD760  token=0x6000064  System.UInt32 ReadRawVarint32(System.IO.Stream input)
  RVA=0x09ACD7D0  token=0x6000065  System.UInt64 ReadRawVarint64()
  RVA=0x09ACD690  token=0x6000066  System.UInt32 ReadRawLittleEndian32()
  RVA=0x09ACD6F8  token=0x6000067  System.UInt64 ReadRawLittleEndian64()
  RVA=0x09ACD190  token=0x6000068  System.Int32 PushLimit(System.Int32 byteLimit)
  RVA=0x09ACD180  token=0x6000069  System.Void PopLimit(System.Int32 oldLimit)
  RVA=0x09ACD940  token=0x600006C  System.Boolean RefillBuffer(System.Boolean mustSucceed)
  RVA=0x09ACD61C  token=0x600006D  System.Byte[] ReadRawBytes(System.Int32 size)
  RVA=0x03142380  token=0x600006E  System.Void ReadRawMessage(Google.Protobuf.IMessage message)
END_CLASS

CLASS: Google.Protobuf.CodedOutputStream
TYPE:  sealed class
TOKEN: 0x200000B
SIZE:  0x40
IMPLEMENTS: System.IDisposable
FIELDS:
  private   static  System.Int32                    LittleEndian64Size  // const
  private   static  System.Int32                    LittleEndian32Size  // const
  private   static  System.Int32                    DoubleSize  // const
  private   static  System.Int32                    FloatSize  // const
  private   static  System.Int32                    BoolSize  // const
  public    static readonly System.Int32                    DefaultBufferSize  // static @ 0x0
  private   readonly System.Boolean                  leaveOpen  // 0x10
  private   readonly System.Byte[]                   buffer  // 0x18
  private           Google.Protobuf.WriterInternalStatestate  // 0x20
  private   readonly System.IO.Stream                output  // 0x38
PROPERTIES:
  Position  get=0x09ACF430
  NonOutputPosition  get=0x011F0020
  SpaceLeft  get=0x09ACF464
  InternalBuffer  get=0x0385B100
  InternalOutputStream  get=0x04D85A50
  InternalState  get=0x04D9E550
METHODS:
  RVA=0x04D8D060  token=0x600006F  System.Int32 ComputeDoubleSize(System.Double value)
  RVA=0x04D882B0  token=0x6000070  System.Int32 ComputeFloatSize(System.Single value)
  RVA=0x032943D0  token=0x6000071  System.Int32 ComputeUInt64Size(System.UInt64 value)
  RVA=0x032942E0  token=0x6000072  System.Int32 ComputeInt64Size(System.Int64 value)
  RVA=0x03296190  token=0x6000073  System.Int32 ComputeInt32Size(System.Int32 value)
  RVA=0x04D8D060  token=0x6000074  System.Int32 ComputeFixed64Size(System.UInt64 value)
  RVA=0x04D882B0  token=0x6000075  System.Int32 ComputeFixed32Size(System.UInt32 value)
  RVA=0x04D85B80  token=0x6000076  System.Int32 ComputeBoolSize(System.Boolean value)
  RVA=0x032947A0  token=0x6000077  System.Int32 ComputeStringSize(System.String value)
  RVA=0x09ACDDDC  token=0x6000078  System.Int32 ComputeGroupSize(Google.Protobuf.IMessage value)
  RVA=0x032960B0  token=0x6000079  System.Int32 ComputeMessageSize(Google.Protobuf.IMessage value)
  RVA=0x0435D040  token=0x600007A  System.Int32 ComputeBytesSize(Google.Protobuf.ByteString value)
  RVA=0x03295F90  token=0x600007B  System.Int32 ComputeUInt32Size(System.UInt32 value)
  RVA=0x03296010  token=0x600007C  System.Int32 ComputeEnumSize(System.Int32 value)
  RVA=0x04D882B0  token=0x600007D  System.Int32 ComputeSFixed32Size(System.Int32 value)
  RVA=0x04D8D060  token=0x600007E  System.Int32 ComputeSFixed64Size(System.Int64 value)
  RVA=0x09ACDE28  token=0x600007F  System.Int32 ComputeSInt32Size(System.Int32 value)
  RVA=0x09ACDE88  token=0x6000080  System.Int32 ComputeSInt64Size(System.Int64 value)
  RVA=0x03295F90  token=0x6000081  System.Int32 ComputeLengthSize(System.Int32 length)
  RVA=0x03296220  token=0x6000082  System.Int32 ComputeRawVarint32Size(System.UInt32 value)
  RVA=0x032944C0  token=0x6000083  System.Int32 ComputeRawVarint64Size(System.UInt64 value)
  RVA=0x09ACDEEC  token=0x6000084  System.Int32 ComputeTagSize(System.Int32 fieldNumber)
  RVA=0x09ACF204  token=0x6000085  System.Void .ctor(System.Byte[] flatArray)
  RVA=0x031436C0  token=0x6000086  System.Void .ctor(System.Byte[] buffer, System.Int32 offset, System.Int32 length)
  RVA=0x09ACF29C  token=0x6000087  System.Void .ctor(System.IO.Stream output, System.Byte[] buffer, System.Boolean leaveOpen)
  RVA=0x09ACF3C8  token=0x6000088  System.Void .ctor(System.IO.Stream output)
  RVA=0x09ACF230  token=0x6000089  System.Void .ctor(System.IO.Stream output, System.Int32 bufferSize)
  RVA=0x09ACF18C  token=0x600008A  System.Void .ctor(System.IO.Stream output, System.Boolean leaveOpen)
  RVA=0x09ACF350  token=0x600008B  System.Void .ctor(System.IO.Stream output, System.Int32 bufferSize, System.Boolean leaveOpen)
  RVA=0x09ACE0D0  token=0x600008E  System.Void WriteDouble(System.Double value)
  RVA=0x09ACE2D4  token=0x600008F  System.Void WriteFloat(System.Single value)
  RVA=0x09ACF10C  token=0x6000090  System.Void WriteUInt64(System.UInt64 value)
  RVA=0x09ACE4EC  token=0x6000091  System.Void WriteInt64(System.Int64 value)
  RVA=0x09ACE46C  token=0x6000092  System.Void WriteInt32(System.Int32 value)
  RVA=0x09ACE254  token=0x6000093  System.Void WriteFixed64(System.UInt64 value)
  RVA=0x09ACE1D4  token=0x6000094  System.Void WriteFixed32(System.UInt32 value)
  RVA=0x09ACDFD0  token=0x6000095  System.Void WriteBool(System.Boolean value)
  RVA=0x09ACEEF8  token=0x6000096  System.Void WriteString(System.String value)
  RVA=0x09ACE5EC  token=0x6000097  System.Void WriteMessage(Google.Protobuf.IMessage value)
  RVA=0x03078F90  token=0x6000098  System.Void WriteRawMessage(Google.Protobuf.IMessage value)
  RVA=0x09ACE358  token=0x6000099  System.Void WriteGroup(Google.Protobuf.IMessage value)
  RVA=0x09ACE050  token=0x600009A  System.Void WriteBytes(Google.Protobuf.ByteString value)
  RVA=0x09ACF08C  token=0x600009B  System.Void WriteUInt32(System.UInt32 value)
  RVA=0x09ACE154  token=0x600009C  System.Void WriteEnum(System.Int32 value)
  RVA=0x09ACECF8  token=0x600009D  System.Void WriteSFixed32(System.Int32 value)
  RVA=0x09ACED78  token=0x600009E  System.Void WriteSFixed64(System.Int64 value)
  RVA=0x09ACEDF8  token=0x600009F  System.Void WriteSInt32(System.Int32 value)
  RVA=0x09ACEE78  token=0x60000A0  System.Void WriteSInt64(System.Int64 value)
  RVA=0x09ACE56C  token=0x60000A1  System.Void WriteLength(System.Int32 length)
  RVA=0x09ACEFF8  token=0x60000A2  System.Void WriteTag(System.Int32 fieldNumber, Google.Protobuf.WireFormat.WireType type)
  RVA=0x09ACEF78  token=0x60000A3  System.Void WriteTag(System.UInt32 tag)
  RVA=0x09ACEB78  token=0x60000A4  System.Void WriteRawTag(System.Byte b1)
  RVA=0x09ACEAE4  token=0x60000A5  System.Void WriteRawTag(System.Byte b1, System.Byte b2)
  RVA=0x09ACE98C  token=0x60000A6  System.Void WriteRawTag(System.Byte b1, System.Byte b2, System.Byte b3)
  RVA=0x09ACEA34  token=0x60000A7  System.Void WriteRawTag(System.Byte b1, System.Byte b2, System.Byte b3, System.Byte b4)
  RVA=0x09ACE8D0  token=0x60000A8  System.Void WriteRawTag(System.Byte b1, System.Byte b2, System.Byte b3, System.Byte b4, System.Byte b5)
  RVA=0x09ACEBF8  token=0x60000A9  System.Void WriteRawVarint32(System.UInt32 value)
  RVA=0x09ACEC78  token=0x60000AA  System.Void WriteRawVarint64(System.UInt64 value)
  RVA=0x09ACE7D0  token=0x60000AB  System.Void WriteRawLittleEndian32(System.UInt32 value)
  RVA=0x09ACE850  token=0x60000AC  System.Void WriteRawLittleEndian64(System.UInt64 value)
  RVA=0x09ACE700  token=0x60000AD  System.Void WriteRawBytes(System.Byte[] value)
  RVA=0x09ACE72C  token=0x60000AE  System.Void WriteRawBytes(System.Byte[] value, System.Int32 offset, System.Int32 length)
  RVA=0x09ACDF3C  token=0x60000AF  System.Void Dispose()
  RVA=0x09ACDF78  token=0x60000B0  System.Void Flush()
  RVA=0x09ACDDD0  token=0x60000B1  System.Void CheckNoSpaceLeft()
  RVA=0x04D7AB50  token=0x60000B6  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.Extension
TYPE:  abstract class
TOKEN: 0x200000D
SIZE:  0x18
FIELDS:
  private   readonly System.Int32                    <FieldNumber>k__BackingField  // 0x10
PROPERTIES:
  TargetType  get=-1  // abstract
  FieldNumber  get=0x02B2E2D0
  IsRepeated  get=-1  // abstract
METHODS:
  RVA=0x014F51F0  token=0x60000B9  System.Void .ctor(System.Int32 fieldNumber)
  RVA=-1  // abstract  token=0x60000BA  Google.Protobuf.IExtensionValue CreateValue()
END_CLASS

CLASS: Google.Protobuf.Extension`2
TYPE:  sealed class
TOKEN: 0x200000E
EXTENDS: Google.Protobuf.Extension
FIELDS:
  private   readonly Google.Protobuf.FieldCodec<TValue>codec  // 0x0
PROPERTIES:
  DefaultValue  get=-1  // not resolved
  TargetType  get=-1  // not resolved
  IsRepeated  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60000BD  System.Void .ctor(System.Int32 fieldNumber, Google.Protobuf.FieldCodec<TValue> codec)
  RVA=-1  // not resolved  token=0x60000C1  Google.Protobuf.IExtensionValue CreateValue()
END_CLASS

CLASS: Google.Protobuf.RepeatedExtension`2
TYPE:  sealed class
TOKEN: 0x200000F
EXTENDS: Google.Protobuf.Extension
FIELDS:
  private   readonly Google.Protobuf.FieldCodec<TValue>codec  // 0x0
PROPERTIES:
  TargetType  get=-1  // not resolved
  IsRepeated  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60000C2  System.Void .ctor(System.Int32 fieldNumber, Google.Protobuf.FieldCodec<TValue> codec)
  RVA=-1  // not resolved  token=0x60000C5  Google.Protobuf.IExtensionValue CreateValue()
END_CLASS

CLASS: Google.Protobuf.ExtensionRegistry
TYPE:  sealed class
TOKEN: 0x2000010
SIZE:  0x18
IMPLEMENTS: System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable Google.Protobuf.IDeepCloneable`1
FIELDS:
  private           System.Collections.Generic.IDictionary<Google.Protobuf.ObjectIntPair<System.Type>,Google.Protobuf.Extension>extensions  // 0x10
PROPERTIES:
  Count  get=0x09ACFFDC
  System.Collections.Generic.ICollection<Google.Protobuf.Extension>.IsReadOnly  get=0x012081B0
METHODS:
  RVA=0x09ACFD8C  token=0x60000C6  System.Void .ctor()
  RVA=0x09ACFE04  token=0x60000C7  System.Void .ctor(System.Collections.Generic.IDictionary<Google.Protobuf.ObjectIntPair<System.Type>,Google.Protobuf.Extension> collection)
  RVA=0x09ACF900  token=0x60000CA  System.Boolean ContainsInputField(System.UInt32 lastTag, System.Type target, Google.Protobuf.Extension& extension)
  RVA=0x09ACF784  token=0x60000CB  System.Void Add(Google.Protobuf.Extension extension)
  RVA=0x09ACF630  token=0x60000CC  System.Void AddRange(System.Collections.Generic.IEnumerable<Google.Protobuf.Extension> extensions)
  RVA=0x09ACF850  token=0x60000CD  System.Void Clear()
  RVA=0x09ACF9A4  token=0x60000CE  System.Boolean Contains(Google.Protobuf.Extension item)
  RVA=0x09ACFBA8  token=0x60000CF  System.Void System.Collections.Generic.ICollection<Google.Protobuf.Extension>.CopyTo(Google.Protobuf.Extension[] array, System.Int32 arrayIndex)
  RVA=0x09ACFA70  token=0x60000D0  System.Collections.Generic.IEnumerator<Google.Protobuf.Extension> GetEnumerator()
  RVA=0x09ACFADC  token=0x60000D1  System.Boolean Remove(Google.Protobuf.Extension item)
  RVA=0x09ACFD84  token=0x60000D2  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=0x09ACF89C  token=0x60000D3  Google.Protobuf.ExtensionRegistry Clone()
END_CLASS

CLASS: Google.Protobuf.ExtensionSet
TYPE:  static class
TOKEN: 0x2000013
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x60000DC  System.Boolean TryGetValue(Google.Protobuf.ExtensionSet<TTarget>& set, Google.Protobuf.Extension extension, Google.Protobuf.IExtensionValue& value)
  RVA=-1  // generic def  token=0x60000DD  TValue Get(Google.Protobuf.ExtensionSet<TTarget>& set, Google.Protobuf.Extension<TTarget,TValue> extension)
  RVA=-1  // generic def  token=0x60000DE  Google.Protobuf.Collections.RepeatedField<TValue> Get(Google.Protobuf.ExtensionSet<TTarget>& set, Google.Protobuf.RepeatedExtension<TTarget,TValue> extension)
  RVA=-1  // generic def  token=0x60000DF  Google.Protobuf.Collections.RepeatedField<TValue> GetOrInitialize(Google.Protobuf.ExtensionSet<TTarget>& set, Google.Protobuf.RepeatedExtension<TTarget,TValue> extension)
  RVA=-1  // generic def  token=0x60000E0  System.Void Set(Google.Protobuf.ExtensionSet<TTarget>& set, Google.Protobuf.Extension<TTarget,TValue> extension, TValue value)
  RVA=-1  // generic def  token=0x60000E1  System.Boolean Has(Google.Protobuf.ExtensionSet<TTarget>& set, Google.Protobuf.Extension<TTarget,TValue> extension)
  RVA=-1  // generic def  token=0x60000E2  System.Void Clear(Google.Protobuf.ExtensionSet<TTarget>& set, Google.Protobuf.Extension<TTarget,TValue> extension)
  RVA=-1  // generic def  token=0x60000E3  System.Void Clear(Google.Protobuf.ExtensionSet<TTarget>& set, Google.Protobuf.RepeatedExtension<TTarget,TValue> extension)
  RVA=-1  // generic def  token=0x60000E4  System.Boolean TryMergeFieldFrom(Google.Protobuf.ExtensionSet<TTarget>& set, Google.Protobuf.CodedInputStream stream)
  RVA=-1  // generic def  token=0x60000E5  System.Boolean TryMergeFieldFrom(Google.Protobuf.ExtensionSet<TTarget>& set, Google.Protobuf.ParseContext& ctx)
  RVA=-1  // generic def  token=0x60000E6  System.Void MergeFrom(Google.Protobuf.ExtensionSet<TTarget>& first, Google.Protobuf.ExtensionSet<TTarget> second)
  RVA=-1  // generic def  token=0x60000E7  Google.Protobuf.ExtensionSet<TTarget> Clone(Google.Protobuf.ExtensionSet<TTarget> set)
END_CLASS

CLASS: Google.Protobuf.ExtensionSet`1
TYPE:  sealed class
TOKEN: 0x2000014
FIELDS:
  private   readonly System.Collections.Generic.Dictionary<System.Int32,Google.Protobuf.IExtensionValue><ValuesByNumber>k__BackingField  // 0x0
PROPERTIES:
  ValuesByNumber  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60000E9  System.Int32 GetHashCode()
  RVA=-1  // not resolved  token=0x60000EA  System.Boolean Equals(System.Object other)
  RVA=-1  // not resolved  token=0x60000EB  System.Int32 CalculateSize()
  RVA=-1  // not resolved  token=0x60000EC  System.Void WriteTo(Google.Protobuf.CodedOutputStream stream)
  RVA=-1  // not resolved  token=0x60000ED  System.Void WriteTo(Google.Protobuf.WriteContext& ctx)
  RVA=-1  // not resolved  token=0x60000EE  System.Boolean IsInitialized()
  RVA=-1  // not resolved  token=0x60000EF  System.Void .ctor()
END_CLASS

CLASS: Google.Protobuf.IExtensionValue
TYPE:  interface
TOKEN: 0x2000016
IMPLEMENTS: System.IEquatable`1 Google.Protobuf.IDeepCloneable`1
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60000F3  System.Void MergeFrom(Google.Protobuf.ParseContext& ctx)
  RVA=-1  // abstract  token=0x60000F4  System.Void MergeFrom(Google.Protobuf.IExtensionValue value)
  RVA=-1  // abstract  token=0x60000F5  System.Void WriteTo(Google.Protobuf.WriteContext& ctx)
  RVA=-1  // abstract  token=0x60000F6  System.Int32 CalculateSize()
  RVA=-1  // abstract  token=0x60000F7  System.Boolean IsInitialized()
  RVA=-1  // abstract  token=0x60000F8  System.Object GetValue()
END_CLASS

CLASS: Google.Protobuf.ExtensionValue`1
TYPE:  sealed class
TOKEN: 0x2000017
IMPLEMENTS: Google.Protobuf.IExtensionValue System.IEquatable`1 Google.Protobuf.IDeepCloneable`1
FIELDS:
  private           T                               field  // 0x0
  private           Google.Protobuf.FieldCodec<T>   codec  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60000F9  System.Void .ctor(Google.Protobuf.FieldCodec<T> codec)
  RVA=-1  // not resolved  token=0x60000FA  System.Int32 CalculateSize()
  RVA=-1  // not resolved  token=0x60000FB  Google.Protobuf.IExtensionValue Clone()
  RVA=-1  // not resolved  token=0x60000FC  System.Boolean Equals(Google.Protobuf.IExtensionValue other)
  RVA=-1  // not resolved  token=0x60000FD  System.Int32 GetHashCode()
  RVA=-1  // not resolved  token=0x60000FE  System.Void MergeFrom(Google.Protobuf.ParseContext& ctx)
  RVA=-1  // not resolved  token=0x60000FF  System.Void MergeFrom(Google.Protobuf.IExtensionValue value)
  RVA=-1  // not resolved  token=0x6000100  System.Void WriteTo(Google.Protobuf.WriteContext& ctx)
  RVA=-1  // not resolved  token=0x6000101  T GetValue()
  RVA=-1  // not resolved  token=0x6000102  System.Object Google.Protobuf.IExtensionValue.GetValue()
  RVA=-1  // not resolved  token=0x6000103  System.Void SetValue(T value)
  RVA=-1  // not resolved  token=0x6000104  System.Boolean IsInitialized()
END_CLASS

CLASS: Google.Protobuf.RepeatedExtensionValue`1
TYPE:  sealed class
TOKEN: 0x2000018
IMPLEMENTS: Google.Protobuf.IExtensionValue System.IEquatable`1 Google.Protobuf.IDeepCloneable`1
FIELDS:
  private           Google.Protobuf.Collections.RepeatedField<T>field  // 0x0
  private   readonly Google.Protobuf.FieldCodec<T>   codec  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000105  System.Void .ctor(Google.Protobuf.FieldCodec<T> codec)
  RVA=-1  // not resolved  token=0x6000106  System.Int32 CalculateSize()
  RVA=-1  // not resolved  token=0x6000107  Google.Protobuf.IExtensionValue Clone()
  RVA=-1  // not resolved  token=0x6000108  System.Boolean Equals(Google.Protobuf.IExtensionValue other)
  RVA=-1  // not resolved  token=0x6000109  System.Int32 GetHashCode()
  RVA=-1  // not resolved  token=0x600010A  System.Void MergeFrom(Google.Protobuf.ParseContext& ctx)
  RVA=-1  // not resolved  token=0x600010B  System.Void MergeFrom(Google.Protobuf.IExtensionValue value)
  RVA=-1  // not resolved  token=0x600010C  System.Void WriteTo(Google.Protobuf.WriteContext& ctx)
  RVA=-1  // not resolved  token=0x600010D  Google.Protobuf.Collections.RepeatedField<T> GetValue()
  RVA=-1  // not resolved  token=0x600010E  System.Object Google.Protobuf.IExtensionValue.GetValue()
  RVA=-1  // not resolved  token=0x600010F  System.Boolean IsInitialized()
END_CLASS

CLASS: Google.Protobuf.FieldCodec
TYPE:  static class
TOKEN: 0x2000019
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x04109440  token=0x6000110  Google.Protobuf.FieldCodec<System.String> ForString(System.UInt32 tag)
  RVA=0x09AD0024  token=0x6000111  Google.Protobuf.FieldCodec<Google.Protobuf.ByteString> ForBytes(System.UInt32 tag)
  RVA=0x05396FD4  token=0x6000112  Google.Protobuf.FieldCodec<System.Boolean> ForBool(System.UInt32 tag)
  RVA=0x05396FBC  token=0x6000113  Google.Protobuf.FieldCodec<System.Int32> ForInt32(System.UInt32 tag)
  RVA=0x09AD0B1C  token=0x6000114  Google.Protobuf.FieldCodec<System.Int32> ForSInt32(System.UInt32 tag)
  RVA=0x09AD0278  token=0x6000115  Google.Protobuf.FieldCodec<System.UInt32> ForFixed32(System.UInt32 tag)
  RVA=0x09AD06AC  token=0x6000116  Google.Protobuf.FieldCodec<System.Int32> ForSFixed32(System.UInt32 tag)
  RVA=0x05396FB0  token=0x6000117  Google.Protobuf.FieldCodec<System.UInt32> ForUInt32(System.UInt32 tag)
  RVA=0x05396FE0  token=0x6000118  Google.Protobuf.FieldCodec<System.Int64> ForInt64(System.UInt32 tag)
  RVA=0x09AD0D78  token=0x6000119  Google.Protobuf.FieldCodec<System.Int64> ForSInt64(System.UInt32 tag)
  RVA=0x09AD0284  token=0x600011A  Google.Protobuf.FieldCodec<System.UInt64> ForFixed64(System.UInt32 tag)
  RVA=0x09AD08D0  token=0x600011B  Google.Protobuf.FieldCodec<System.Int64> ForSFixed64(System.UInt32 tag)
  RVA=0x0539778C  token=0x600011C  Google.Protobuf.FieldCodec<System.UInt64> ForUInt64(System.UInt32 tag)
  RVA=0x05396FC8  token=0x600011D  Google.Protobuf.FieldCodec<System.Single> ForFloat(System.UInt32 tag)
  RVA=0x05398044  token=0x600011E  Google.Protobuf.FieldCodec<System.Double> ForDouble(System.UInt32 tag)
  RVA=-1  // generic def  token=0x600011F  Google.Protobuf.FieldCodec<T> ForEnum(System.UInt32 tag, System.Func<T,System.Int32> toInt32, System.Func<System.Int32,T> fromInt32)
  RVA=0x04109480  token=0x6000120  Google.Protobuf.FieldCodec<System.String> ForString(System.UInt32 tag, System.String defaultValue)
  RVA=0x03218910  token=0x6000121  Google.Protobuf.FieldCodec<Google.Protobuf.ByteString> ForBytes(System.UInt32 tag, Google.Protobuf.ByteString defaultValue)
  RVA=0x0410C790  token=0x6000122  Google.Protobuf.FieldCodec<System.Boolean> ForBool(System.UInt32 tag, System.Boolean defaultValue)
  RVA=0x0410B330  token=0x6000123  Google.Protobuf.FieldCodec<System.Int32> ForInt32(System.UInt32 tag, System.Int32 defaultValue)
  RVA=0x09AD08DC  token=0x6000124  Google.Protobuf.FieldCodec<System.Int32> ForSInt32(System.UInt32 tag, System.Int32 defaultValue)
  RVA=0x09AD0074  token=0x6000125  Google.Protobuf.FieldCodec<System.UInt32> ForFixed32(System.UInt32 tag, System.UInt32 defaultValue)
  RVA=0x09AD04A8  token=0x6000126  Google.Protobuf.FieldCodec<System.Int32> ForSFixed32(System.UInt32 tag, System.Int32 defaultValue)
  RVA=0x04110D00  token=0x6000127  Google.Protobuf.FieldCodec<System.UInt32> ForUInt32(System.UInt32 tag, System.UInt32 defaultValue)
  RVA=0x0410D000  token=0x6000128  Google.Protobuf.FieldCodec<System.Int64> ForInt64(System.UInt32 tag, System.Int64 defaultValue)
  RVA=0x09AD0B28  token=0x6000129  Google.Protobuf.FieldCodec<System.Int64> ForSInt64(System.UInt32 tag, System.Int64 defaultValue)
  RVA=0x09AD0290  token=0x600012A  Google.Protobuf.FieldCodec<System.UInt64> ForFixed64(System.UInt32 tag, System.UInt64 defaultValue)
  RVA=0x09AD06B8  token=0x600012B  Google.Protobuf.FieldCodec<System.Int64> ForSFixed64(System.UInt32 tag, System.Int64 defaultValue)
  RVA=0x044A0EC0  token=0x600012C  Google.Protobuf.FieldCodec<System.UInt64> ForUInt64(System.UInt32 tag, System.UInt64 defaultValue)
  RVA=0x049E43F0  token=0x600012D  Google.Protobuf.FieldCodec<System.Single> ForFloat(System.UInt32 tag, System.Single defaultValue)
  RVA=0x049E4630  token=0x600012E  Google.Protobuf.FieldCodec<System.Double> ForDouble(System.UInt32 tag, System.Double defaultValue)
  RVA=-1  // generic def  token=0x600012F  Google.Protobuf.FieldCodec<T> ForEnum(System.UInt32 tag, System.Func<T,System.Int32> toInt32, System.Func<System.Int32,T> fromInt32, T defaultValue)
  RVA=-1  // generic def  token=0x6000130  Google.Protobuf.FieldCodec<T> ForMessage(System.UInt32 tag, Google.Protobuf.MessageParser<T> parser)
  RVA=-1  // generic def  token=0x6000131  Google.Protobuf.FieldCodec<T> ForGroup(System.UInt32 startTag, System.UInt32 endTag, Google.Protobuf.MessageParser<T> parser)
  RVA=-1  // generic def  token=0x6000132  Google.Protobuf.FieldCodec<T> ForClassWrapper(System.UInt32 tag)
  RVA=-1  // generic def  token=0x6000133  Google.Protobuf.FieldCodec<System.Nullable<T>> ForStructWrapper(System.UInt32 tag)
END_CLASS

CLASS: Google.Protobuf.ValueReader`1
TYPE:  sealed class
TOKEN: 0x2000026
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x600017F  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000180  TValue Invoke(Google.Protobuf.ParseContext& ctx)
  RVA=-1  // runtime  token=0x6000181  System.IAsyncResult BeginInvoke(Google.Protobuf.ParseContext& ctx, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x6000182  TValue EndInvoke(Google.Protobuf.ParseContext& ctx, System.IAsyncResult result)
END_CLASS

CLASS: Google.Protobuf.ValueWriter`1
TYPE:  sealed class
TOKEN: 0x2000027
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000183  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000184  System.Void Invoke(Google.Protobuf.WriteContext& ctx, T value)
  RVA=-1  // runtime  token=0x6000185  System.IAsyncResult BeginInvoke(Google.Protobuf.WriteContext& ctx, T value, System.AsyncCallback callback, System.Object object)
  RVA=-1  // runtime  token=0x6000186  System.Void EndInvoke(Google.Protobuf.WriteContext& ctx, System.IAsyncResult result)
END_CLASS

CLASS: Google.Protobuf.FieldCodec`1
TYPE:  sealed class
TOKEN: 0x2000028
FIELDS:
  private   static readonly System.Collections.Generic.EqualityComparer<T>EqualityComparer  // static @ 0x0
  private   static readonly T                               DefaultDefault  // static @ 0x0
  private   static readonly System.Boolean                  TypeSupportsPacking  // static @ 0x0
  private   readonly System.Boolean                  <PackedRepeatedField>k__BackingField  // 0x0
  private   readonly Google.Protobuf.ValueWriter<T>  <ValueWriter>k__BackingField  // 0x0
  private   readonly System.Func<T,System.Int32>     <ValueSizeCalculator>k__BackingField  // 0x0
  private   readonly Google.Protobuf.ValueReader<T>  <ValueReader>k__BackingField  // 0x0
  private   readonly Google.Protobuf.FieldCodec.InputMerger<T><ValueMerger>k__BackingField  // 0x0
  private   readonly Google.Protobuf.FieldCodec.ValuesMerger<T><FieldMerger>k__BackingField  // 0x0
  private   readonly System.Int32                    <FixedSize>k__BackingField  // 0x0
  private   readonly System.UInt32                   <Tag>k__BackingField  // 0x0
  private   readonly System.UInt32                   <EndTag>k__BackingField  // 0x0
  private   readonly T                               <DefaultValue>k__BackingField  // 0x0
  private   readonly System.Int32                    tagSize  // 0x0
PROPERTIES:
  PackedRepeatedField  get=-1  // not resolved
  ValueWriter  get=-1  // not resolved
  ValueSizeCalculator  get=-1  // not resolved
  ValueReader  get=-1  // not resolved
  ValueMerger  get=-1  // not resolved
  FieldMerger  get=-1  // not resolved
  FixedSize  get=-1  // not resolved
  Tag  get=-1  // not resolved
  EndTag  get=-1  // not resolved
  DefaultValue  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000187  System.Void .cctor()
  RVA=-1  // not resolved  token=0x6000188  System.Boolean IsPackedRepeatedField(System.UInt32 tag)
  RVA=-1  // not resolved  token=0x6000193  System.Void .ctor(Google.Protobuf.ValueReader<T> reader, Google.Protobuf.ValueWriter<T> writer, System.Int32 fixedSize, System.UInt32 tag, T defaultValue)
  RVA=-1  // not resolved  token=0x6000194  System.Void .ctor(Google.Protobuf.ValueReader<T> reader, Google.Protobuf.ValueWriter<T> writer, System.Func<T,System.Int32> sizeCalculator, System.UInt32 tag, T defaultValue)
  RVA=-1  // not resolved  token=0x6000195  System.Void .ctor(Google.Protobuf.ValueReader<T> reader, Google.Protobuf.ValueWriter<T> writer, Google.Protobuf.FieldCodec.InputMerger<T> inputMerger, Google.Protobuf.FieldCodec.ValuesMerger<T> valuesMerger, System.Func<T,System.Int32> sizeCalculator, System.UInt32 tag, System.UInt32 endTag)
  RVA=-1  // not resolved  token=0x6000196  System.Void .ctor(Google.Protobuf.ValueReader<T> reader, Google.Protobuf.ValueWriter<T> writer, Google.Protobuf.FieldCodec.InputMerger<T> inputMerger, Google.Protobuf.FieldCodec.ValuesMerger<T> valuesMerger, System.Func<T,System.Int32> sizeCalculator, System.UInt32 tag, System.UInt32 endTag, T defaultValue)
  RVA=-1  // not resolved  token=0x6000197  System.Void WriteTagAndValue(Google.Protobuf.CodedOutputStream output, T value)
  RVA=-1  // not resolved  token=0x6000198  System.Void WriteTagAndValue(Google.Protobuf.WriteContext& ctx, T value)
  RVA=-1  // not resolved  token=0x6000199  T Read(Google.Protobuf.CodedInputStream input)
  RVA=-1  // not resolved  token=0x600019A  T Read(Google.Protobuf.ParseContext& ctx)
  RVA=-1  // not resolved  token=0x600019B  System.Int32 CalculateSizeWithTag(T value)
  RVA=-1  // not resolved  token=0x600019C  System.Int32 CalculateUnconditionalSizeWithTag(T value)
  RVA=-1  // not resolved  token=0x600019D  System.Boolean IsDefault(T value)
END_CLASS

CLASS: Google.Protobuf.FieldMaskTree
TYPE:  sealed class
TOKEN: 0x200002E
SIZE:  0x18
FIELDS:
  private   static  System.Char                     FIELD_PATH_SEPARATOR  // const
  private   readonly Google.Protobuf.FieldMaskTree.Noderoot  // 0x10
METHODS:
  RVA=0x09AD20E0  token=0x60001AD  System.Void .ctor()
  RVA=0x09AD2144  token=0x60001AE  System.Void .ctor(Google.Protobuf.WellKnownTypes.FieldMask mask)
  RVA=0x09AD20B8  token=0x60001AF  System.String ToString()
  RVA=0x09AD0D84  token=0x60001B0  Google.Protobuf.FieldMaskTree AddFieldPath(System.String path)
  RVA=0x09AD1454  token=0x60001B1  Google.Protobuf.FieldMaskTree MergeFromFieldMask(Google.Protobuf.WellKnownTypes.FieldMask mask)
  RVA=0x09AD1FA0  token=0x60001B2  Google.Protobuf.WellKnownTypes.FieldMask ToFieldMask()
  RVA=0x09AD0F30  token=0x60001B3  System.Void GetFieldPaths(Google.Protobuf.FieldMaskTree.Node node, System.String path, System.Collections.Generic.List<System.String> paths)
  RVA=0x09AD114C  token=0x60001B4  System.Void IntersectFieldPath(System.String path, Google.Protobuf.FieldMaskTree output)
  RVA=0x09AD15A0  token=0x60001B5  System.Void Merge(Google.Protobuf.IMessage source, Google.Protobuf.IMessage destination, Google.Protobuf.WellKnownTypes.FieldMask.MergeOptions options)
  RVA=0x09AD16F4  token=0x60001B6  System.Void Merge(Google.Protobuf.FieldMaskTree.Node node, System.String path, Google.Protobuf.IMessage source, Google.Protobuf.IMessage destination, Google.Protobuf.WellKnownTypes.FieldMask.MergeOptions options)
END_CLASS

CLASS: Google.Protobuf.FrameworkPortability
TYPE:  static class
TOKEN: 0x2000030
SIZE:  0x10
FIELDS:
  private   static readonly System.Text.RegularExpressions.RegexOptionsCompiledRegexWhereAvailable  // static @ 0x0
METHODS:
  RVA=0x09AD21C4  token=0x60001B9  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.IBufferMessage
TYPE:  interface
TOKEN: 0x2000031
IMPLEMENTS: Google.Protobuf.IMessage
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60001BA  System.Void InternalMergeFrom(Google.Protobuf.ParseContext& ctx)
  RVA=-1  // abstract  token=0x60001BB  System.Void InternalWriteTo(Google.Protobuf.WriteContext& ctx)
END_CLASS

CLASS: Google.Protobuf.ICustomDiagnosticMessage
TYPE:  interface
TOKEN: 0x2000032
IMPLEMENTS: Google.Protobuf.IMessage
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60001BC  System.String ToDiagnosticString()
END_CLASS

CLASS: Google.Protobuf.IDeepCloneable`1
TYPE:  interface
TOKEN: 0x2000033
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60001BD  T Clone()
END_CLASS

CLASS: Google.Protobuf.IExtendableMessage`1
TYPE:  interface
TOKEN: 0x2000034
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60001BE  TValue GetExtension(Google.Protobuf.Extension<T,TValue> extension)
  RVA=-1  // abstract  token=0x60001BF  Google.Protobuf.Collections.RepeatedField<TValue> GetExtension(Google.Protobuf.RepeatedExtension<T,TValue> extension)
  RVA=-1  // abstract  token=0x60001C0  Google.Protobuf.Collections.RepeatedField<TValue> GetOrInitializeExtension(Google.Protobuf.RepeatedExtension<T,TValue> extension)
  RVA=-1  // abstract  token=0x60001C1  System.Void SetExtension(Google.Protobuf.Extension<T,TValue> extension, TValue value)
  RVA=-1  // abstract  token=0x60001C2  System.Boolean HasExtension(Google.Protobuf.Extension<T,TValue> extension)
  RVA=-1  // abstract  token=0x60001C3  System.Void ClearExtension(Google.Protobuf.Extension<T,TValue> extension)
  RVA=-1  // abstract  token=0x60001C4  System.Void ClearExtension(Google.Protobuf.RepeatedExtension<T,TValue> extension)
END_CLASS

CLASS: Google.Protobuf.IMessage
TYPE:  interface
TOKEN: 0x2000035
FIELDS:
PROPERTIES:
  Descriptor  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x60001C5  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=-1  // abstract  token=0x60001C6  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=-1  // abstract  token=0x60001C7  System.Int32 CalculateSize()
END_CLASS

CLASS: Google.Protobuf.IMessage`1
TYPE:  interface
TOKEN: 0x2000036
IMPLEMENTS: Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60001C9  System.Void MergeFrom(T message)
END_CLASS

CLASS: Google.Protobuf.InvalidJsonException
TYPE:  sealed class
TOKEN: 0x2000037
SIZE:  0x90
EXTENDS: System.IO.IOException
FIELDS:
METHODS:
  RVA=0x09AD2294  token=0x60001CA  System.Void .ctor(System.String message)
END_CLASS

CLASS: Google.Protobuf.InvalidProtocolBufferException
TYPE:  sealed class
TOKEN: 0x2000038
SIZE:  0x90
EXTENDS: System.IO.IOException
FIELDS:
METHODS:
  RVA=0x09AD2294  token=0x60001CB  System.Void .ctor(System.String message)
  RVA=0x09AD27D8  token=0x60001CC  System.Void .ctor(System.String message, System.Exception innerException)
  RVA=0x09AD25E4  token=0x60001CD  Google.Protobuf.InvalidProtocolBufferException MoreDataAvailable()
  RVA=0x09AD2774  token=0x60001CE  Google.Protobuf.InvalidProtocolBufferException TruncatedMessage()
  RVA=0x09AD2648  token=0x60001CF  Google.Protobuf.InvalidProtocolBufferException NegativeSize()
  RVA=0x09AD251C  token=0x60001D0  Google.Protobuf.InvalidProtocolBufferException MalformedVarint()
  RVA=0x09AD23F0  token=0x60001D1  Google.Protobuf.InvalidProtocolBufferException InvalidTag()
  RVA=0x09AD2454  token=0x60001D2  Google.Protobuf.InvalidProtocolBufferException InvalidWireType()
  RVA=0x09AD22B4  token=0x60001D3  Google.Protobuf.InvalidProtocolBufferException InvalidBase64(System.Exception innerException)
  RVA=0x09AD2328  token=0x60001D4  Google.Protobuf.InvalidProtocolBufferException InvalidEndTag()
  RVA=0x09AD26AC  token=0x60001D5  Google.Protobuf.InvalidProtocolBufferException RecursionLimitExceeded()
  RVA=0x09AD24B8  token=0x60001D6  Google.Protobuf.InvalidProtocolBufferException JsonRecursionLimitExceeded()
  RVA=0x09AD2710  token=0x60001D7  Google.Protobuf.InvalidProtocolBufferException SizeLimitExceeded()
  RVA=0x09AD238C  token=0x60001D8  Google.Protobuf.InvalidProtocolBufferException InvalidMessageStreamTag()
  RVA=0x09AD2580  token=0x60001D9  Google.Protobuf.InvalidProtocolBufferException MissingFields()
END_CLASS

CLASS: Google.Protobuf.JsonFormatter
TYPE:  sealed class
TOKEN: 0x2000039
SIZE:  0x18
FIELDS:
  private   static  System.String                   AnyTypeUrlField  // const
  private   static  System.String                   AnyDiagnosticValueField  // const
  private   static  System.String                   AnyWellKnownTypeValueField  // const
  private   static  System.String                   TypeUrlPrefix  // const
  private   static  System.String                   NameValueSeparator  // const
  private   static  System.String                   PropertySeparator  // const
  private   static readonly Google.Protobuf.JsonFormatter   <Default>k__BackingField  // static @ 0x0
  private   static readonly Google.Protobuf.JsonFormatter   diagnosticFormatter  // static @ 0x8
  private   static readonly System.String[]                 CommonRepresentations  // static @ 0x10
  private   readonly Google.Protobuf.JsonFormatter.Settingssettings  // 0x10
  private   static  System.String                   Hex  // const
PROPERTIES:
  Default  get=0x09AD79B4
  DiagnosticOnly  get=0x09AD7A04
METHODS:
  RVA=0x09AD67A4  token=0x60001DB  System.Void .cctor()
  RVA=0x09AD794C  token=0x60001DD  System.Void .ctor(Google.Protobuf.JsonFormatter.Settings settings)
  RVA=0x09AD27E0  token=0x60001DE  System.String Format(Google.Protobuf.IMessage message)
  RVA=0x09AD2864  token=0x60001DF  System.Void Format(Google.Protobuf.IMessage message, System.IO.TextWriter writer)
  RVA=0x09AD2F8C  token=0x60001E0  System.String ToDiagnosticString(Google.Protobuf.IMessage message)
  RVA=0x09AD4FD4  token=0x60001E1  System.Void WriteMessage(System.IO.TextWriter writer, Google.Protobuf.IMessage message)
  RVA=0x09AD4CB0  token=0x60001E2  System.Boolean WriteMessageFields(System.IO.TextWriter writer, Google.Protobuf.IMessage message, System.Boolean assumeFirstFieldWritten)
  RVA=0x09AD2EE0  token=0x60001E3  System.Boolean ShouldFormatFieldValue(Google.Protobuf.IMessage message, Google.Protobuf.Reflection.FieldDescriptor field, System.Object value)
  RVA=0x09AD3018  token=0x60001E4  System.String ToJsonName(System.String name)
  RVA=0x09AD2974  token=0x60001E5  System.String FromJsonName(System.String name)
  RVA=0x09AD5140  token=0x60001E6  System.Void WriteNull(System.IO.TextWriter writer)
  RVA=0x09AD2B94  token=0x60001E7  System.Boolean IsDefaultValue(Google.Protobuf.Reflection.FieldDescriptor descriptor, System.Object value)
  RVA=0x09AD5CEC  token=0x60001E8  System.Void WriteValue(System.IO.TextWriter writer, System.Object value)
  RVA=0x09AD6334  token=0x60001E9  System.Void WriteWellKnownTypeValue(System.IO.TextWriter writer, Google.Protobuf.Reflection.MessageDescriptor descriptor, System.Object value)
  RVA=0x09AD5B60  token=0x60001EA  System.Void WriteTimestamp(System.IO.TextWriter writer, Google.Protobuf.IMessage value)
  RVA=0x09AD47D4  token=0x60001EB  System.Void WriteDuration(System.IO.TextWriter writer, Google.Protobuf.IMessage value)
  RVA=0x09AD4960  token=0x60001EC  System.Void WriteFieldMask(System.IO.TextWriter writer, Google.Protobuf.IMessage value)
  RVA=0x09AD3124  token=0x60001ED  System.Void WriteAny(System.IO.TextWriter writer, Google.Protobuf.IMessage value)
  RVA=0x09AD34E0  token=0x60001EE  System.Void WriteDiagnosticOnlyAny(System.IO.TextWriter writer, Google.Protobuf.IMessage value)
  RVA=0x09AD5730  token=0x60001EF  System.Void WriteStruct(System.IO.TextWriter writer, Google.Protobuf.IMessage message)
  RVA=0x09AD54B0  token=0x60001F0  System.Void WriteStructFieldValue(System.IO.TextWriter writer, Google.Protobuf.IMessage message)
  RVA=0x09AD4A6C  token=0x60001F1  System.Void WriteList(System.IO.TextWriter writer, System.Collections.IList list)
  RVA=0x09AD376C  token=0x60001F2  System.Void WriteDictionary(System.IO.TextWriter writer, System.Collections.IDictionary dictionary)
  RVA=0x09AD518C  token=0x60001F3  System.Void WriteString(System.IO.TextWriter writer, System.String text)
  RVA=0x09AD2A78  token=0x60001F4  System.Void HexEncodeUtf16CodeUnit(System.IO.TextWriter writer, System.Char c)
END_CLASS

CLASS: Google.Protobuf.JsonParser
TYPE:  sealed class
TOKEN: 0x200003D
SIZE:  0x18
FIELDS:
  private   static readonly System.Text.RegularExpressions.RegexTimestampRegex  // static @ 0x0
  private   static readonly System.Text.RegularExpressions.RegexDurationRegex  // static @ 0x8
  private   static readonly System.Int32[]                  SubsecondScalingFactors  // static @ 0x10
  private   static readonly System.Char[]                   FieldMaskPathSeparators  // static @ 0x18
  private   static readonly Google.Protobuf.Reflection.EnumDescriptorNullValueDescriptor  // static @ 0x20
  private   static readonly Google.Protobuf.JsonParser      defaultInstance  // static @ 0x28
  private   static readonly System.Collections.Generic.Dictionary<System.String,System.Action<Google.Protobuf.JsonParser,Google.Protobuf.IMessage,Google.Protobuf.JsonTokenizer>>WellKnownTypeHandlers  // static @ 0x30
  private   readonly Google.Protobuf.JsonParser.Settingssettings  // 0x10
PROPERTIES:
  Default  get=0x09ADC8B8
METHODS:
  RVA=0x09AD9C90  token=0x600020B  System.Void MergeWrapperField(Google.Protobuf.JsonParser parser, Google.Protobuf.IMessage message, Google.Protobuf.JsonTokenizer tokenizer)
  RVA=0x09ADC850  token=0x600020D  System.Void .ctor(Google.Protobuf.JsonParser.Settings settings)
  RVA=0x09ADA2CC  token=0x600020E  System.Void Merge(Google.Protobuf.IMessage message, System.String json)
  RVA=0x09ADA1EC  token=0x600020F  System.Void Merge(Google.Protobuf.IMessage message, System.IO.TextReader jsonReader)
  RVA=0x09AD9D2C  token=0x6000210  System.Void Merge(Google.Protobuf.IMessage message, Google.Protobuf.JsonTokenizer tokenizer)
  RVA=0x09AD88F8  token=0x6000211  System.Void MergeField(Google.Protobuf.IMessage message, Google.Protobuf.Reflection.FieldDescriptor field, Google.Protobuf.JsonTokenizer tokenizer)
  RVA=0x09AD8D08  token=0x6000212  System.Void MergeRepeatedField(Google.Protobuf.IMessage message, Google.Protobuf.Reflection.FieldDescriptor field, Google.Protobuf.JsonTokenizer tokenizer)
  RVA=0x09AD8A7C  token=0x6000213  System.Void MergeMapField(Google.Protobuf.IMessage message, Google.Protobuf.Reflection.FieldDescriptor field, Google.Protobuf.JsonTokenizer tokenizer)
  RVA=0x09AD7C64  token=0x6000214  System.Boolean IsGoogleProtobufValueField(Google.Protobuf.Reflection.FieldDescriptor field)
  RVA=0x09AD7BDC  token=0x6000215  System.Boolean IsGoogleProtobufNullValueField(Google.Protobuf.Reflection.FieldDescriptor field)
  RVA=0x09ADB4A4  token=0x6000216  System.Object ParseSingleValue(Google.Protobuf.Reflection.FieldDescriptor field, Google.Protobuf.JsonTokenizer tokenizer)
  RVA=-1  // generic def  token=0x6000217  T Parse(System.String json)
  RVA=-1  // generic def  token=0x6000218  T Parse(System.IO.TextReader jsonReader)
  RVA=0x09ADB8D8  token=0x6000219  Google.Protobuf.IMessage Parse(System.String json, Google.Protobuf.Reflection.MessageDescriptor descriptor)
  RVA=0x09ADB810  token=0x600021A  Google.Protobuf.IMessage Parse(System.IO.TextReader jsonReader, Google.Protobuf.Reflection.MessageDescriptor descriptor)
  RVA=0x09AD8EF0  token=0x600021B  System.Void MergeStructValue(Google.Protobuf.IMessage message, Google.Protobuf.JsonTokenizer tokenizer)
  RVA=0x09AD9214  token=0x600021C  System.Void MergeStruct(Google.Protobuf.IMessage message, Google.Protobuf.JsonTokenizer tokenizer)
  RVA=0x09AD7CE4  token=0x600021D  System.Void MergeAny(Google.Protobuf.IMessage message, Google.Protobuf.JsonTokenizer tokenizer)
  RVA=0x09AD9B2C  token=0x600021E  System.Void MergeWellKnownTypeAnyBody(Google.Protobuf.IMessage body, Google.Protobuf.JsonTokenizer tokenizer)
  RVA=0x09ADA384  token=0x600021F  System.Object ParseMapKey(Google.Protobuf.Reflection.FieldDescriptor field, System.String keyText)
  RVA=0x09ADA7F0  token=0x6000220  System.Object ParseSingleNumberValue(Google.Protobuf.Reflection.FieldDescriptor field, Google.Protobuf.JsonToken token)
  RVA=0x09AD7A50  token=0x6000221  System.Void CheckInteger(System.Double value)
  RVA=0x09ADADD4  token=0x6000222  System.Object ParseSingleStringValue(Google.Protobuf.Reflection.FieldDescriptor field, System.String text)
  RVA=0x09ADA354  token=0x6000223  Google.Protobuf.IMessage NewMessageForField(Google.Protobuf.Reflection.FieldDescriptor field)
  RVA=-1  // generic def  token=0x6000224  T ParseNumericString(System.String text, System.Func<System.String,System.Globalization.NumberStyles,System.IFormatProvider,T> parser)
  RVA=0x09ADBB6C  token=0x6000225  System.Void ValidateInfinityAndNan(System.String text, System.Boolean isPositiveInfinity, System.Boolean isNegativeInfinity, System.Boolean isNaN)
  RVA=0x09AD9334  token=0x6000226  System.Void MergeTimestamp(Google.Protobuf.IMessage message, Google.Protobuf.JsonToken token)
  RVA=0x09AD8110  token=0x6000227  System.Void MergeDuration(Google.Protobuf.IMessage message, Google.Protobuf.JsonToken token)
  RVA=0x09AD8730  token=0x6000228  System.Void MergeFieldMask(Google.Protobuf.IMessage message, Google.Protobuf.JsonToken token)
  RVA=0x09ADB9BC  token=0x6000229  System.String ToSnakeCase(System.String text)
  RVA=0x09ADBC8C  token=0x600022A  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.JsonToken
TYPE:  sealed class
TOKEN: 0x2000040
SIZE:  0x28
IMPLEMENTS: System.IEquatable`1
FIELDS:
  private   static readonly Google.Protobuf.JsonToken       _true  // static @ 0x0
  private   static readonly Google.Protobuf.JsonToken       _false  // static @ 0x8
  private   static readonly Google.Protobuf.JsonToken       _null  // static @ 0x10
  private   static readonly Google.Protobuf.JsonToken       startObject  // static @ 0x18
  private   static readonly Google.Protobuf.JsonToken       endObject  // static @ 0x20
  private   static readonly Google.Protobuf.JsonToken       startArray  // static @ 0x28
  private   static readonly Google.Protobuf.JsonToken       endArray  // static @ 0x30
  private   static readonly Google.Protobuf.JsonToken       endDocument  // static @ 0x38
  private   readonly Google.Protobuf.JsonToken.TokenTypetype  // 0x10
  private   readonly System.String                   stringValue  // 0x18
  private   readonly System.Double                   numberValue  // 0x20
PROPERTIES:
  Null  get=0x09ADEB90
  False  get=0x09ADEB40
  True  get=0x09ADEC80
  StartObject  get=0x09ADEC30
  EndObject  get=0x09ADEAF0
  StartArray  get=0x09ADEBE0
  EndArray  get=0x09ADEA50
  EndDocument  get=0x09ADEAA0
  Type  get=0x02B2E2D0
  StringValue  get=0x0385B100
  NumberValue  get=0x04D86970
METHODS:
  RVA=0x09ADE3C8  token=0x6000248  Google.Protobuf.JsonToken Name(System.String name)
  RVA=0x09ADE6D4  token=0x6000249  Google.Protobuf.JsonToken Value(System.String value)
  RVA=0x09ADE668  token=0x600024A  Google.Protobuf.JsonToken Value(System.Double value)
  RVA=0x09ADEA2C  token=0x600024E  System.Void .ctor(Google.Protobuf.JsonToken.TokenType type, System.String stringValue, System.Double numberValue)
  RVA=0x09ADE2F0  token=0x600024F  System.Boolean Equals(System.Object obj)
  RVA=0x09ADE354  token=0x6000250  System.Int32 GetHashCode()
  RVA=0x09ADE434  token=0x6000251  System.String ToString()
  RVA=0x09ADE29C  token=0x6000252  System.Boolean Equals(Google.Protobuf.JsonToken other)
  RVA=0x09ADE740  token=0x6000253  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.JsonTokenizer
TYPE:  abstract class
TOKEN: 0x2000042
SIZE:  0x20
FIELDS:
  private           Google.Protobuf.JsonToken       bufferedToken  // 0x10
  private           System.Int32                    <ObjectDepth>k__BackingField  // 0x18
PROPERTIES:
  ObjectDepth  get=0x011EF5B0  set=0x011EF9B0
METHODS:
  RVA=0x09ADED40  token=0x6000254  Google.Protobuf.JsonTokenizer FromTextReader(System.IO.TextReader reader)
  RVA=0x09ADECD0  token=0x6000255  Google.Protobuf.JsonTokenizer FromReplayedTokens(System.Collections.Generic.IList<Google.Protobuf.JsonToken> tokens, Google.Protobuf.JsonTokenizer continuation)
  RVA=0x09ADEE10  token=0x6000258  System.Void PushBack(Google.Protobuf.JsonToken token)
  RVA=0x09ADEDA0  token=0x6000259  Google.Protobuf.JsonToken Next()
  RVA=-1  // abstract  token=0x600025A  Google.Protobuf.JsonToken NextImpl()
  RVA=0x09ADEEA8  token=0x600025B  System.Void SkipValue()
  RVA=0x041E1670  token=0x600025C  System.Void .ctor()
END_CLASS

CLASS: Google.Protobuf.LimitedInputStream
TYPE:  sealed class
TOKEN: 0x2000048
SIZE:  0x38
EXTENDS: System.IO.Stream
FIELDS:
  private   readonly System.IO.Stream                proxied  // 0x28
  private           System.Int32                    bytesLeft  // 0x30
PROPERTIES:
  CanRead  get=0x02FFF600
  CanSeek  get=0x012081B0
  CanWrite  get=0x012081B0
  Length  get=0x09ADF0DC
  Position  get=0x09ADF128  set=0x09ADF174
METHODS:
  RVA=0x09ADF07C  token=0x6000273  System.Void .ctor(System.IO.Stream proxied, System.Int32 size)
  RVA=0x041E1670  token=0x6000277  System.Void Flush()
  RVA=0x09ADEEF8  token=0x600027B  System.Int32 Read(System.Byte[] buffer, System.Int32 offset, System.Int32 count)
  RVA=0x09ADEF98  token=0x600027C  System.Int64 Seek(System.Int64 offset, System.IO.SeekOrigin origin)
  RVA=0x09ADEFE4  token=0x600027D  System.Void SetLength(System.Int64 value)
  RVA=0x09ADF030  token=0x600027E  System.Void Write(System.Byte[] buffer, System.Int32 offset, System.Int32 count)
END_CLASS

CLASS: Google.Protobuf.MessageExtensions
TYPE:  static class
TOKEN: 0x2000049
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09ADF5A0  token=0x600027F  System.Void MergeFrom(Google.Protobuf.IMessage message, System.Byte[] data)
  RVA=0x05394858  token=0x6000280  System.Void MergeFrom(Google.Protobuf.IMessage message, System.Byte[] data, System.Int32 offset, System.Int32 length)
  RVA=0x09ADF468  token=0x6000281  System.Void MergeFrom(Google.Protobuf.IMessage message, Google.Protobuf.ByteString data)
  RVA=0x09ADF488  token=0x6000282  System.Void MergeFrom(Google.Protobuf.IMessage message, System.IO.Stream input)
  RVA=0x05394824  token=0x6000283  System.Void MergeFrom(Google.Protobuf.IMessage message, System.ReadOnlySpan<System.Byte> span)
  RVA=0x09ADF448  token=0x6000284  System.Void MergeDelimitedFrom(Google.Protobuf.IMessage message, System.IO.Stream input)
  RVA=0x09ADF8C8  token=0x6000285  System.Byte[] ToByteArray(Google.Protobuf.IMessage message)
  RVA=0x03143590  token=0x6000286  System.Int32 WriteToByteArray(Google.Protobuf.IMessage message, System.Byte[] buffer, System.Int32 pos)
  RVA=0x09ADFB40  token=0x6000287  System.Void WriteTo(Google.Protobuf.IMessage message, System.IO.Stream output)
  RVA=0x09ADFA34  token=0x6000288  System.Void WriteDelimitedTo(Google.Protobuf.IMessage message, System.IO.Stream output)
  RVA=0x09ADF9B8  token=0x6000289  Google.Protobuf.ByteString ToByteString(Google.Protobuf.IMessage message)
  RVA=0x09ADFD00  token=0x600028A  System.Void WriteTo(Google.Protobuf.IMessage message, System.Buffers.IBufferWriter<System.Byte> output)
  RVA=0x09ADFC2C  token=0x600028B  System.Void WriteTo(Google.Protobuf.IMessage message, System.Span<System.Byte> output)
  RVA=0x09ADF1C0  token=0x600028C  System.Boolean IsInitialized(Google.Protobuf.IMessage message)
  RVA=0x09ADF5C0  token=0x600028D  System.Void MergeFrom(Google.Protobuf.IMessage message, System.Byte[] data, System.Boolean discardUnknownFields, Google.Protobuf.ExtensionRegistry registry)
  RVA=0x03143020  token=0x600028E  System.Void MergeFrom(Google.Protobuf.IMessage message, System.Byte[] data, System.Int32 offset, System.Int32 length, System.Boolean discardUnknownFields, Google.Protobuf.ExtensionRegistry registry)
  RVA=0x09ADF4A8  token=0x600028F  System.Void MergeFrom(Google.Protobuf.IMessage message, Google.Protobuf.ByteString data, System.Boolean discardUnknownFields, Google.Protobuf.ExtensionRegistry registry)
  RVA=0x09ADF6DC  token=0x6000290  System.Void MergeFrom(Google.Protobuf.IMessage message, System.IO.Stream input, System.Boolean discardUnknownFields, Google.Protobuf.ExtensionRegistry registry)
  RVA=0x09ADF7FC  token=0x6000291  System.Void MergeFrom(Google.Protobuf.IMessage message, System.Buffers.ReadOnlySequence<System.Byte> data, System.Boolean discardUnknownFields, Google.Protobuf.ExtensionRegistry registry)
  RVA=0x03DF4750  token=0x6000292  System.Void MergeFrom(Google.Protobuf.IMessage message, System.ReadOnlySpan<System.Byte> data, System.Boolean discardUnknownFields, Google.Protobuf.ExtensionRegistry registry)
  RVA=0x09ADF344  token=0x6000293  System.Void MergeDelimitedFrom(Google.Protobuf.IMessage message, System.IO.Stream input, System.Boolean discardUnknownFields, Google.Protobuf.ExtensionRegistry registry)
END_CLASS

CLASS: Google.Protobuf.MessageParser
TYPE:  class
TOKEN: 0x200004B
SIZE:  0x28
FIELDS:
  private           System.Func<Google.Protobuf.IMessage>factory  // 0x10
  private   readonly System.Boolean                  <DiscardUnknownFields>k__BackingField  // 0x18
  private   readonly Google.Protobuf.ExtensionRegistry<Extensions>k__BackingField  // 0x20
PROPERTIES:
  DiscardUnknownFields  get=0x04D86340
  Extensions  get=0x04D862C0
METHODS:
  RVA=0x04044A30  token=0x6000298  System.Void .ctor(System.Func<Google.Protobuf.IMessage> factory, System.Boolean discardUnknownFields, Google.Protobuf.ExtensionRegistry extensions)
  RVA=0x09ADFE08  token=0x6000299  Google.Protobuf.IMessage CreateTemplate()
  RVA=0x09AE0184  token=0x600029A  Google.Protobuf.IMessage ParseFrom(System.Byte[] data)
  RVA=0x09AE0048  token=0x600029B  Google.Protobuf.IMessage ParseFrom(System.Byte[] data, System.Int32 offset, System.Int32 length)
  RVA=0x09AE0128  token=0x600029C  Google.Protobuf.IMessage ParseFrom(Google.Protobuf.ByteString data)
  RVA=0x09AE00CC  token=0x600029D  Google.Protobuf.IMessage ParseFrom(System.IO.Stream input)
  RVA=0x09AE0248  token=0x600029E  Google.Protobuf.IMessage ParseFrom(System.Buffers.ReadOnlySequence<System.Byte> data)
  RVA=0x09AE01E0  token=0x600029F  Google.Protobuf.IMessage ParseFrom(System.ReadOnlySpan<System.Byte> data)
  RVA=0x09ADFFEC  token=0x60002A0  Google.Protobuf.IMessage ParseDelimitedFrom(System.IO.Stream input)
  RVA=0x09AE02B8  token=0x60002A1  Google.Protobuf.IMessage ParseFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09AE030C  token=0x60002A2  Google.Protobuf.IMessage ParseJson(System.String json)
  RVA=0x09ADFE28  token=0x60002A3  System.Void MergeFrom(Google.Protobuf.IMessage message, Google.Protobuf.CodedInputStream codedInput)
  RVA=0x09AE0388  token=0x60002A4  Google.Protobuf.MessageParser WithDiscardUnknownFields(System.Boolean discardUnknownFields)
  RVA=0x09AE0410  token=0x60002A5  Google.Protobuf.MessageParser WithExtensionRegistry(Google.Protobuf.ExtensionRegistry registry)
END_CLASS

CLASS: Google.Protobuf.MessageParser`1
TYPE:  sealed class
TOKEN: 0x200004C
EXTENDS: Google.Protobuf.MessageParser
FIELDS:
  private   readonly System.Func<T>                  factory  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60002A6  System.Void .ctor(System.Func<T> factory)
  RVA=-1  // not resolved  token=0x60002A7  System.Void .ctor(System.Func<T> factory, System.Boolean discardUnknownFields, Google.Protobuf.ExtensionRegistry extensions)
  RVA=-1  // not resolved  token=0x60002A8  T CreateTemplate()
  RVA=-1  // not resolved  token=0x60002A9  T ParseFrom(System.Byte[] data)
  RVA=-1  // not resolved  token=0x60002AA  T ParseFrom(System.Byte[] data, System.Int32 offset, System.Int32 length)
  RVA=-1  // not resolved  token=0x60002AB  T ParseFrom(Google.Protobuf.ByteString data)
  RVA=-1  // not resolved  token=0x60002AC  T ParseFrom(System.IO.Stream input)
  RVA=-1  // not resolved  token=0x60002AD  T ParseFrom(System.Buffers.ReadOnlySequence<System.Byte> data)
  RVA=-1  // not resolved  token=0x60002AE  T ParseFrom(System.ReadOnlySpan<System.Byte> data)
  RVA=-1  // not resolved  token=0x60002AF  T ParseDelimitedFrom(System.IO.Stream input)
  RVA=-1  // not resolved  token=0x60002B0  T ParseFrom(Google.Protobuf.CodedInputStream input)
  RVA=-1  // not resolved  token=0x60002B1  T ParseJson(System.String json)
  RVA=-1  // not resolved  token=0x60002B2  Google.Protobuf.MessageParser<T> WithDiscardUnknownFields(System.Boolean discardUnknownFields)
  RVA=-1  // not resolved  token=0x60002B3  Google.Protobuf.MessageParser<T> WithExtensionRegistry(Google.Protobuf.ExtensionRegistry registry)
END_CLASS

CLASS: Google.Protobuf.ObjectIntPair`1
TYPE:  sealed struct
TOKEN: 0x200004E
IMPLEMENTS: System.IEquatable`1
FIELDS:
  private   readonly System.Int32                    number  // 0x0
  private   readonly T                               obj  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60002B6  System.Void .ctor(T obj, System.Int32 number)
  RVA=-1  // not resolved  token=0x60002B7  System.Boolean Equals(Google.Protobuf.ObjectIntPair<T> other)
  RVA=-1  // not resolved  token=0x60002B8  System.Boolean Equals(System.Object obj)
  RVA=-1  // not resolved  token=0x60002B9  System.Int32 GetHashCode()
END_CLASS

CLASS: Google.Protobuf.ParseContext
TYPE:  sealed struct
TOKEN: 0x200004F
SIZE:  0xA0
FIELDS:
  private   static  System.Int32                    DefaultRecursionLimit  // const
  private   static  System.Int32                    DefaultSizeLimit  // const
  private           System.ReadOnlySpan<System.Byte>buffer  // 0x10
  private           Google.Protobuf.ParserInternalStatestate  // 0x20
PROPERTIES:
  LastTag  get=0x04D868A0
  DiscardUnknownFields  get=0x02A80FA0  set=0x02A80FB0
  ExtensionRegistry  get=0x04D861F0  set=0x05395D30
METHODS:
  RVA=0x09AE0CEC  token=0x60002BA  System.Void Initialize(System.ReadOnlySpan<System.Byte> buffer, Google.Protobuf.ParseContext& ctx)
  RVA=0x04DA0CF0  token=0x60002BB  System.Void Initialize(System.ReadOnlySpan<System.Byte> buffer, Google.Protobuf.ParserInternalState& state, Google.Protobuf.ParseContext& ctx)
  RVA=0x09AE0C20  token=0x60002BC  System.Void Initialize(Google.Protobuf.CodedInputStream input, Google.Protobuf.ParseContext& ctx)
  RVA=0x09AE0BF4  token=0x60002BD  System.Void Initialize(System.Buffers.ReadOnlySequence<System.Byte> input, Google.Protobuf.ParseContext& ctx)
  RVA=0x09AE0B40  token=0x60002BE  System.Void Initialize(System.Buffers.ReadOnlySequence<System.Byte> input, System.Int32 recursionLimit, Google.Protobuf.ParseContext& ctx)
  RVA=0x09AE0F9C  token=0x60002C4  System.UInt32 ReadTag()
  RVA=0x09AE0E4C  token=0x60002C5  System.Double ReadDouble()
  RVA=0x09AE0E7C  token=0x60002C6  System.Single ReadFloat()
  RVA=0x09AE0EE4  token=0x60002C7  System.UInt64 ReadUInt64()
  RVA=0x09AE0EE4  token=0x60002C8  System.Int64 ReadInt64()
  RVA=0x09AE0E58  token=0x60002C9  System.Int32 ReadInt32()
  RVA=0x09AE0E70  token=0x60002CA  System.UInt64 ReadFixed64()
  RVA=0x09AE0E64  token=0x60002CB  System.UInt32 ReadFixed32()
  RVA=0x09AE0E24  token=0x60002CC  System.Boolean ReadBool()
  RVA=0x09AE0F90  token=0x60002CD  System.String ReadString()
  RVA=0x09AE0EF0  token=0x60002CE  System.Void ReadMessage(Google.Protobuf.IMessage message)
  RVA=0x09AE0E88  token=0x60002CF  System.Void ReadGroup(Google.Protobuf.IMessage message)
  RVA=0x09AE0E40  token=0x60002D0  Google.Protobuf.ByteString ReadBytes()
  RVA=0x09AE0E58  token=0x60002D1  System.UInt32 ReadUInt32()
  RVA=0x09AE0E58  token=0x60002D2  System.Int32 ReadEnum()
  RVA=0x09AE0E64  token=0x60002D3  System.Int32 ReadSFixed32()
  RVA=0x09AE0E70  token=0x60002D4  System.Int64 ReadSFixed64()
  RVA=0x09AE0F4C  token=0x60002D5  System.Int32 ReadSInt32()
  RVA=0x09AE0F6C  token=0x60002D6  System.Int64 ReadSInt64()
  RVA=0x09AE0E58  token=0x60002D7  System.Int32 ReadLength()
  RVA=0x03E9EAE0  token=0x60002D8  System.Void CopyStateTo(Google.Protobuf.CodedInputStream input)
  RVA=0x09AE0DB0  token=0x60002D9  System.Void LoadStateFrom(Google.Protobuf.CodedInputStream input)
END_CLASS

CLASS: Google.Protobuf.ParserInternalState
TYPE:  sealed struct
TOKEN: 0x2000050
SIZE:  0x90
FIELDS:
  private           System.Int32                    bufferPos  // 0x10
  private           System.Int32                    bufferSize  // 0x14
  private           System.Int32                    bufferSizeAfterLimit  // 0x18
  private           System.Int32                    currentLimit  // 0x1c
  private           System.Int32                    totalBytesRetired  // 0x20
  private           System.Int32                    recursionDepth  // 0x24
  private           Google.Protobuf.SegmentedBufferHelpersegmentedBufferHelper  // 0x28
  private           System.UInt32                   lastTag  // 0x70
  private           System.UInt32                   nextTag  // 0x74
  private           System.Boolean                  hasNextTag  // 0x78
  private           System.Int32                    sizeLimit  // 0x7c
  private           System.Int32                    recursionLimit  // 0x80
  private           System.Boolean                  <DiscardUnknownFields>k__BackingField  // 0x84
  private           Google.Protobuf.ExtensionRegistry<ExtensionRegistry>k__BackingField  // 0x88
PROPERTIES:
  CodedInputStream  get=0x02E56440
  DiscardUnknownFields  get=0x0157A7D0  set=0x0157A810
  ExtensionRegistry  get=0x04D862A0  set=0x06402320
METHODS:
END_CLASS

CLASS: Google.Protobuf.ParsingPrimitives
TYPE:  static class
TOKEN: 0x2000051
SIZE:  0x10
FIELDS:
  private   static  System.Int32                    StackallocThreshold  // const
METHODS:
  RVA=0x09AEB504  token=0x60002DF  System.Int32 ParseLength(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state)
  RVA=0x02F7EC00  token=0x60002E0  System.UInt32 ParseTag(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state)
  RVA=0x02F7D9A0  token=0x60002E1  System.Boolean MaybeConsumeTag(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state, System.UInt32 tag)
  RVA=0x02F7DA00  token=0x60002E2  System.UInt32 PeekTag(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state)
  RVA=0x035372C0  token=0x60002E3  System.UInt64 ParseRawVarint64(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state)
  RVA=0x035373C0  token=0x60002E4  System.UInt64 ParseRawVarint64SlowPath(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state)
  RVA=0x02F7ED20  token=0x60002E5  System.UInt32 ParseRawVarint32(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state)
  RVA=0x02F7DA40  token=0x60002E6  System.UInt32 ParseRawVarint32SlowPath(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state)
  RVA=0x09AEB598  token=0x60002E7  System.UInt32 ParseRawLittleEndian32(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state)
  RVA=0x09AEB50C  token=0x60002E8  System.UInt32 ParseRawLittleEndian32SlowPath(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state)
  RVA=0x09AEB720  token=0x60002E9  System.UInt64 ParseRawLittleEndian64(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state)
  RVA=0x09AEB624  token=0x60002EA  System.UInt64 ParseRawLittleEndian64SlowPath(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state)
  RVA=0x03D6C820  token=0x60002EB  System.Double ParseDouble(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state)
  RVA=0x03DB8140  token=0x60002EC  System.Single ParseFloat(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state)
  RVA=0x09AEB3E0  token=0x60002ED  System.Single ParseFloatSlow(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state)
  RVA=0x02F84F70  token=0x60002EE  System.Byte[] ReadRawBytes(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state, System.Int32 size)
  RVA=0x09AEB984  token=0x60002EF  System.Byte[] ReadRawBytesSlow(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state, System.Int32 size)
  RVA=0x09AEBEF4  token=0x60002F0  System.Void SkipRawBytes(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state, System.Int32 size)
  RVA=0x09AEBEC0  token=0x60002F1  System.String ReadString(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state)
  RVA=0x09AEB7AC  token=0x60002F2  Google.Protobuf.ByteString ReadBytes(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state)
  RVA=0x02F857F0  token=0x60002F3  System.String ReadRawString(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state, System.Int32 length)
  RVA=0x02F862A0  token=0x60002F4  System.String ReadStringSlow(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state, System.Int32 length)
  RVA=0x02F86360  token=0x60002F5  System.Void ValidateCurrentLimit(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state, System.Int32 size)
  RVA=0x02F7EBC0  token=0x60002F6  System.Byte ReadRawByte(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state)
  RVA=0x09AEBDD0  token=0x60002F7  System.UInt32 ReadRawVarint32(System.IO.Stream input)
  RVA=0x04DA0D60  token=0x60002F8  System.Int32 DecodeZigZag32(System.UInt32 n)
  RVA=0x04DA0D70  token=0x60002F9  System.Int64 DecodeZigZag64(System.UInt64 n)
  RVA=0x03977980  token=0x60002FA  System.Boolean IsDataAvailable(Google.Protobuf.ParserInternalState& state, System.Int32 size)
  RVA=0x09AEB334  token=0x60002FB  System.Boolean IsDataAvailableInSource(Google.Protobuf.ParserInternalState& state, System.Int32 size)
  RVA=0x09AEB81C  token=0x60002FC  System.Void ReadRawBytesIntoSpan(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state, System.Int32 length, System.Span<System.Byte> byteSpan)
END_CLASS

CLASS: Google.Protobuf.ParsingPrimitivesMessages
TYPE:  static class
TOKEN: 0x2000052
SIZE:  0x10
FIELDS:
  private   static readonly System.Byte[]                   ZeroLengthMessageStreamData  // static @ 0x0
METHODS:
  RVA=0x09AEA7E4  token=0x60002FD  System.Void SkipLastField(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state)
  RVA=0x09AEA658  token=0x60002FE  System.Void SkipGroup(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state, System.UInt32 startGroupTag)
  RVA=0x02F85C20  token=0x60002FF  System.Void ReadMessage(Google.Protobuf.ParseContext& ctx, Google.Protobuf.IMessage message)
  RVA=-1  // generic def  token=0x6000300  System.Collections.Generic.KeyValuePair<TKey,TValue> ReadMapEntry(Google.Protobuf.ParseContext& ctx, Google.Protobuf.Collections.MapField.Codec<TKey,TValue> codec)
  RVA=0x09AEA4FC  token=0x6000301  System.Void ReadGroup(Google.Protobuf.ParseContext& ctx, Google.Protobuf.IMessage message)
  RVA=0x09AEA5A4  token=0x6000302  System.Void ReadGroup(Google.Protobuf.ParseContext& ctx, System.Int32 fieldNumber, Google.Protobuf.UnknownFieldSet set)
  RVA=0x02F85E00  token=0x6000303  System.Void ReadRawMessage(Google.Protobuf.ParseContext& ctx, Google.Protobuf.IMessage message)
  RVA=0x02F85DC0  token=0x6000304  System.Void CheckReadEndOfStreamTag(Google.Protobuf.ParserInternalState& state)
  RVA=0x09AEA4C0  token=0x6000305  System.Void CheckLastTagWas(Google.Protobuf.ParserInternalState& state, System.UInt32 expectedTag)
  RVA=0x04D36760  token=0x6000306  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.ParsingPrimitivesWrappers
TYPE:  static class
TOKEN: 0x2000053
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09AEAC30  token=0x6000307  System.Nullable<System.Single> ReadFloatWrapperLittleEndian(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state)
  RVA=0x09AEACF8  token=0x6000308  System.Nullable<System.Single> ReadFloatWrapperSlow(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state)
  RVA=0x09AEAA4C  token=0x6000309  System.Nullable<System.Double> ReadDoubleWrapperLittleEndian(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state)
  RVA=0x09AEAB48  token=0x600030A  System.Nullable<System.Double> ReadDoubleWrapperSlow(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state)
  RVA=0x09AEA980  token=0x600030B  System.Nullable<System.Boolean> ReadBoolWrapper(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state)
  RVA=0x09AEB014  token=0x600030C  System.Nullable<System.UInt32> ReadUInt32Wrapper(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state)
  RVA=0x09AEAF3C  token=0x600030D  System.Nullable<System.UInt32> ReadUInt32WrapperSlow(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state)
  RVA=0x09AEADF0  token=0x600030E  System.Nullable<System.Int32> ReadInt32Wrapper(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state)
  RVA=0x09AEB214  token=0x600030F  System.Nullable<System.UInt64> ReadUInt64Wrapper(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state)
  RVA=0x09AEB130  token=0x6000310  System.Nullable<System.UInt64> ReadUInt64WrapperSlow(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state)
  RVA=0x09AEAE9C  token=0x6000311  System.Nullable<System.Int64> ReadInt64Wrapper(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state)
  RVA=0x09AEACEC  token=0x6000312  System.Nullable<System.Single> ReadFloatWrapperLittleEndian(Google.Protobuf.ParseContext& ctx)
  RVA=0x09AEADD8  token=0x6000313  System.Nullable<System.Single> ReadFloatWrapperSlow(Google.Protobuf.ParseContext& ctx)
  RVA=0x09AEAA20  token=0x6000314  System.Nullable<System.Double> ReadDoubleWrapperLittleEndian(Google.Protobuf.ParseContext& ctx)
  RVA=0x09AEAB1C  token=0x6000315  System.Nullable<System.Double> ReadDoubleWrapperSlow(Google.Protobuf.ParseContext& ctx)
  RVA=0x09AEAA14  token=0x6000316  System.Nullable<System.Boolean> ReadBoolWrapper(Google.Protobuf.ParseContext& ctx)
  RVA=0x09AEB0F8  token=0x6000317  System.Nullable<System.UInt32> ReadUInt32Wrapper(Google.Protobuf.ParseContext& ctx)
  RVA=0x09AEADE4  token=0x6000318  System.Nullable<System.Int32> ReadInt32Wrapper(Google.Protobuf.ParseContext& ctx)
  RVA=0x09AEB308  token=0x6000319  System.Nullable<System.UInt64> ReadUInt64Wrapper(Google.Protobuf.ParseContext& ctx)
  RVA=0x09AEB104  token=0x600031A  System.Nullable<System.UInt64> ReadUInt64WrapperSlow(Google.Protobuf.ParseContext& ctx)
  RVA=0x09AEAE70  token=0x600031B  System.Nullable<System.Int64> ReadInt64Wrapper(Google.Protobuf.ParseContext& ctx)
END_CLASS

CLASS: Google.Protobuf.ProtoPreconditions
TYPE:  static class
TOKEN: 0x2000054
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x600031C  T CheckNotNull(T value, System.String name)
  RVA=-1  // generic def  token=0x600031D  T CheckNotNullUnconstrained(T value, System.String name)
END_CLASS

CLASS: Google.Protobuf.SegmentedBufferHelper
TYPE:  sealed struct
TOKEN: 0x2000055
SIZE:  0x58
FIELDS:
  private           System.Nullable<System.Int32>   totalLength  // 0x10
  private           System.Buffers.ReadOnlySequence.Enumerator<System.Byte>readOnlySequenceEnumerator  // 0x18
  private           Google.Protobuf.CodedInputStreamcodedInputStream  // 0x50
PROPERTIES:
  TotalLength  get=0x02B76770
  CodedInputStream  get=0x04D85A60
METHODS:
  RVA=0x09AEBFA8  token=0x600031E  System.Void Initialize(Google.Protobuf.CodedInputStream codedInputStream, Google.Protobuf.SegmentedBufferHelper& instance)
  RVA=0x09AEC040  token=0x600031F  System.Void Initialize(System.Buffers.ReadOnlySequence<System.Byte> sequence, Google.Protobuf.SegmentedBufferHelper& instance, System.ReadOnlySpan<System.Byte>& firstSpan)
  RVA=0x02F7DC40  token=0x6000320  System.Boolean RefillBuffer(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state, System.Boolean mustSucceed)
  RVA=0x02F85D30  token=0x6000323  System.Int32 PushLimit(Google.Protobuf.ParserInternalState& state, System.Int32 byteLimit)
  RVA=0x05390AA0  token=0x6000324  System.Void PopLimit(Google.Protobuf.ParserInternalState& state, System.Int32 oldLimit)
  RVA=0x02F85DE0  token=0x6000325  System.Boolean IsReachedLimit(Google.Protobuf.ParserInternalState& state)
  RVA=0x09AEC238  token=0x6000326  System.Boolean IsAtEnd(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state)
  RVA=0x03EE6570  token=0x6000327  System.Boolean RefillFromReadOnlySequence(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state, System.Boolean mustSucceed)
  RVA=0x02F7DB40  token=0x6000328  System.Boolean RefillFromCodedInputStream(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state, System.Boolean mustSucceed)
  RVA=0x02F85D90  token=0x6000329  System.Void RecomputeBufferSizeAfterLimit(Google.Protobuf.ParserInternalState& state)
  RVA=0x02F7DBD0  token=0x600032A  System.Void CheckCurrentBufferIsEmpty(Google.Protobuf.ParserInternalState& state)
END_CLASS

CLASS: Google.Protobuf.UnknownField
TYPE:  sealed class
TOKEN: 0x2000056
SIZE:  0x38
FIELDS:
  private           System.Collections.Generic.List<System.UInt64>varintList  // 0x10
  private           System.Collections.Generic.List<System.UInt32>fixed32List  // 0x18
  private           System.Collections.Generic.List<System.UInt64>fixed64List  // 0x20
  private           System.Collections.Generic.List<Google.Protobuf.ByteString>lengthDelimitedList  // 0x28
  private           System.Collections.Generic.List<Google.Protobuf.UnknownFieldSet>groupList  // 0x30
METHODS:
  RVA=0x041E1670  token=0x600032B  System.Void .ctor()
  RVA=0x09AF1AD8  token=0x600032C  System.Boolean Equals(System.Object other)
  RVA=0x09AF1BDC  token=0x600032D  System.Int32 GetHashCode()
  RVA=0x09AF22D8  token=0x600032E  System.Void WriteTo(System.Int32 fieldNumber, Google.Protobuf.WriteContext& output)
  RVA=0x09AF1CC4  token=0x600032F  System.Int32 GetSerializedSize(System.Int32 fieldNumber)
  RVA=0x09AF21C8  token=0x6000330  Google.Protobuf.UnknownField MergeFrom(Google.Protobuf.UnknownField other)
  RVA=-1  // generic def  token=0x6000331  System.Collections.Generic.List<T> AddAll(System.Collections.Generic.List<T> current, System.Collections.Generic.IList<T> extras)
  RVA=0x09AF1A7C  token=0x6000332  Google.Protobuf.UnknownField AddVarint(System.UInt64 value)
  RVA=0x09AF1910  token=0x6000333  Google.Protobuf.UnknownField AddFixed32(System.UInt32 value)
  RVA=0x09AF1968  token=0x6000334  Google.Protobuf.UnknownField AddFixed64(System.UInt64 value)
  RVA=0x09AF1A20  token=0x6000335  Google.Protobuf.UnknownField AddLengthDelimited(Google.Protobuf.ByteString value)
  RVA=0x09AF19C4  token=0x6000336  Google.Protobuf.UnknownField AddGroup(Google.Protobuf.UnknownFieldSet value)
  RVA=-1  // generic def  token=0x6000337  System.Collections.Generic.List<T> Add(System.Collections.Generic.List<T> list, T value)
END_CLASS

CLASS: Google.Protobuf.UnknownFieldSet
TYPE:  sealed class
TOKEN: 0x2000057
SIZE:  0x28
FIELDS:
  private   readonly System.Collections.Generic.IDictionary<System.Int32,Google.Protobuf.UnknownField>fields  // 0x10
  private           System.Int32                    lastFieldNumber  // 0x18
  private           Google.Protobuf.UnknownField    lastField  // 0x20
METHODS:
  RVA=0x09AF1898  token=0x6000338  System.Void .ctor()
  RVA=0x09AF0E58  token=0x6000339  System.Boolean HasField(System.Int32 field)
  RVA=0x09AF15FC  token=0x600033A  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09AF16C0  token=0x600033B  System.Void WriteTo(Google.Protobuf.WriteContext& ctx)
  RVA=0x09AF081C  token=0x600033C  System.Int32 CalculateSize()
  RVA=0x09AF0980  token=0x600033D  System.Boolean Equals(System.Object other)
  RVA=0x09AF0C0C  token=0x600033E  System.Int32 GetHashCode()
  RVA=0x09AF0D98  token=0x600033F  Google.Protobuf.UnknownField GetOrAddField(System.Int32 number)
  RVA=0x09AF0754  token=0x6000340  Google.Protobuf.UnknownFieldSet AddOrReplaceField(System.Int32 number, Google.Protobuf.UnknownField field)
  RVA=0x09AF1094  token=0x6000341  System.Boolean MergeFieldFrom(Google.Protobuf.ParseContext& ctx)
  RVA=0x09AF15B0  token=0x6000342  System.Void MergeGroupFrom(Google.Protobuf.ParseContext& ctx)
  RVA=0x09AF0FA8  token=0x6000343  Google.Protobuf.UnknownFieldSet MergeFieldFrom(Google.Protobuf.UnknownFieldSet unknownFields, Google.Protobuf.CodedInputStream input)
  RVA=0x09AF0EB0  token=0x6000344  Google.Protobuf.UnknownFieldSet MergeFieldFrom(Google.Protobuf.UnknownFieldSet unknownFields, Google.Protobuf.ParseContext& ctx)
  RVA=0x09AF13D0  token=0x6000345  Google.Protobuf.UnknownFieldSet MergeFrom(Google.Protobuf.UnknownFieldSet other)
  RVA=0x09AF1358  token=0x6000346  Google.Protobuf.UnknownFieldSet MergeFrom(Google.Protobuf.UnknownFieldSet unknownFields, Google.Protobuf.UnknownFieldSet other)
  RVA=0x09AF1288  token=0x6000347  Google.Protobuf.UnknownFieldSet MergeField(System.Int32 number, Google.Protobuf.UnknownField field)
  RVA=0x03218CC0  token=0x6000348  Google.Protobuf.UnknownFieldSet Clone(Google.Protobuf.UnknownFieldSet other)
END_CLASS

CLASS: Google.Protobuf.UnsafeByteOperations
TYPE:  static class
TOKEN: 0x2000058
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09AF28A8  token=0x6000349  Google.Protobuf.ByteString UnsafeWrap(System.ReadOnlyMemory<System.Byte> bytes)
END_CLASS

CLASS: Google.Protobuf.WireFormat
TYPE:  static class
TOKEN: 0x2000059
SIZE:  0x10
FIELDS:
  private   static  System.Int32                    TagTypeBits  // const
  private   static  System.UInt32                   TagTypeMask  // const
METHODS:
  RVA=0x04DA0D90  token=0x600034A  Google.Protobuf.WireFormat.WireType GetTagWireType(System.UInt32 tag)
  RVA=0x04DA0D80  token=0x600034B  System.Int32 GetTagFieldNumber(System.UInt32 tag)
  RVA=0x04DA0DA0  token=0x600034C  System.UInt32 MakeTag(System.Int32 fieldNumber, Google.Protobuf.WireFormat.WireType wireType)
END_CLASS

CLASS: Google.Protobuf.WriteBufferHelper
TYPE:  sealed struct
TOKEN: 0x200005B
SIZE:  0x20
FIELDS:
  private           System.Buffers.IBufferWriter<System.Byte>bufferWriter  // 0x10
  private           Google.Protobuf.CodedOutputStreamcodedOutputStream  // 0x18
PROPERTIES:
  CodedOutputStream  get=0x04D88320
METHODS:
  RVA=0x03143780  token=0x600034E  System.Void Initialize(Google.Protobuf.CodedOutputStream codedOutputStream, Google.Protobuf.WriteBufferHelper& instance)
  RVA=0x09ACC1D0  token=0x600034F  System.Void Initialize(System.Buffers.IBufferWriter<System.Byte> bufferWriter, Google.Protobuf.WriteBufferHelper& instance, System.Span<System.Byte>& buffer)
  RVA=0x09ACC1AC  token=0x6000350  System.Void InitializeNonRefreshable(Google.Protobuf.WriteBufferHelper& instance)
  RVA=0x09ACC01C  token=0x6000351  System.Void CheckNoSpaceLeft(Google.Protobuf.WriterInternalState& state)
  RVA=0x09ACC130  token=0x6000352  System.Int32 GetSpaceLeft(Google.Protobuf.WriterInternalState& state)
  RVA=0x09AF3E18  token=0x6000353  System.Void RefreshBuffer(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state)
  RVA=0x09AF3D70  token=0x6000354  System.Void Flush(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state)
END_CLASS

CLASS: Google.Protobuf.WriteContext
TYPE:  sealed struct
TOKEN: 0x200005C
SIZE:  0x38
FIELDS:
  private           System.Span<System.Byte>        buffer  // 0x10
  private           Google.Protobuf.WriterInternalStatestate  // 0x20
METHODS:
  RVA=0x04DA0DB0  token=0x6000355  System.Void Initialize(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, Google.Protobuf.WriteContext& ctx)
  RVA=0x09AF3FA8  token=0x6000356  System.Void Initialize(Google.Protobuf.CodedOutputStream output, Google.Protobuf.WriteContext& ctx)
  RVA=0x09AF4024  token=0x6000357  System.Void Initialize(System.Buffers.IBufferWriter<System.Byte> output, Google.Protobuf.WriteContext& ctx)
  RVA=0x09AF3F4C  token=0x6000358  System.Void Initialize(System.Span<System.Byte>& buffer, Google.Protobuf.WriteContext& ctx)
  RVA=0x03077670  token=0x6000359  System.Void WriteDouble(System.Double value)
  RVA=0x0307AA60  token=0x600035A  System.Void WriteFloat(System.Single value)
  RVA=0x03078680  token=0x600035B  System.Void WriteUInt64(System.UInt64 value)
  RVA=0x03078680  token=0x600035C  System.Void WriteInt64(System.Int64 value)
  RVA=0x03079380  token=0x600035D  System.Void WriteInt32(System.Int32 value)
  RVA=0x09AF4108  token=0x600035E  System.Void WriteFixed64(System.UInt64 value)
  RVA=0x09AF40B4  token=0x600035F  System.Void WriteFixed32(System.UInt32 value)
  RVA=0x03077520  token=0x6000360  System.Void WriteBool(System.Boolean value)
  RVA=0x03078230  token=0x6000361  System.Void WriteString(System.String value)
  RVA=0x05390FA0  token=0x6000362  System.Void WriteMessage(Google.Protobuf.IMessage value)
  RVA=0x09AF415C  token=0x6000363  System.Void WriteGroup(Google.Protobuf.IMessage value)
  RVA=0x04213430  token=0x6000364  System.Void WriteBytes(Google.Protobuf.ByteString value)
  RVA=0x030792A0  token=0x6000365  System.Void WriteUInt32(System.UInt32 value)
  RVA=0x03079490  token=0x6000366  System.Void WriteEnum(System.Int32 value)
  RVA=0x09AF42F0  token=0x6000367  System.Void WriteSFixed32(System.Int32 value)
  RVA=0x09AF4344  token=0x6000368  System.Void WriteSFixed64(System.Int64 value)
  RVA=0x09AF4398  token=0x6000369  System.Void WriteSInt32(System.Int32 value)
  RVA=0x09AF43EC  token=0x600036A  System.Void WriteSInt64(System.Int64 value)
  RVA=0x04592CD0  token=0x600036B  System.Void WriteLength(System.Int32 length)
  RVA=0x09AF4440  token=0x600036C  System.Void WriteTag(System.Int32 fieldNumber, Google.Protobuf.WireFormat.WireType type)
  RVA=0x03078910  token=0x600036D  System.Void WriteTag(System.UInt32 tag)
  RVA=0x0307A030  token=0x600036E  System.Void WriteRawTag(System.Byte b1)
  RVA=0x03078E70  token=0x600036F  System.Void WriteRawTag(System.Byte b1, System.Byte b2)
  RVA=0x09AF41E8  token=0x6000370  System.Void WriteRawTag(System.Byte b1, System.Byte b2, System.Byte b3)
  RVA=0x09AF4164  token=0x6000371  System.Void WriteRawTag(System.Byte b1, System.Byte b2, System.Byte b3, System.Byte b4)
  RVA=0x09AF4264  token=0x6000372  System.Void WriteRawTag(System.Byte b1, System.Byte b2, System.Byte b3, System.Byte b4, System.Byte b5)
  RVA=0x09AF3F40  token=0x6000373  System.Void Flush()
  RVA=0x09AF3F34  token=0x6000374  System.Void CheckNoSpaceLeft()
  RVA=0x03F21110  token=0x6000375  System.Void CopyStateTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09AF408C  token=0x6000376  System.Void LoadStateFrom(Google.Protobuf.CodedOutputStream output)
END_CLASS

CLASS: Google.Protobuf.WriterInternalState
TYPE:  sealed struct
TOKEN: 0x200005D
SIZE:  0x28
FIELDS:
  private           System.Int32                    limit  // 0x10
  private           System.Int32                    position  // 0x14
  private           Google.Protobuf.WriteBufferHelperwriteBufferHelper  // 0x18
PROPERTIES:
  CodedOutputStream  get=0x02B2ECC0
METHODS:
END_CLASS

CLASS: Google.Protobuf.WritingPrimitives
TYPE:  static class
TOKEN: 0x200005E
SIZE:  0x10
FIELDS:
  private   static readonly System.Text.Encoding            Utf8Encoding  // static @ 0x0
METHODS:
  RVA=0x030776C0  token=0x6000378  System.Void WriteDouble(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Double value)
  RVA=0x0307AB70  token=0x6000379  System.Void WriteFloat(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Single value)
  RVA=0x09AF484C  token=0x600037A  System.Void WriteFloatSlowPath(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Single value)
  RVA=0x03078770  token=0x600037B  System.Void WriteUInt64(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.UInt64 value)
  RVA=0x03078770  token=0x600037C  System.Void WriteInt64(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Int64 value)
  RVA=0x030796D0  token=0x600037D  System.Void WriteInt32(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Int32 value)
  RVA=0x09AF47EC  token=0x600037E  System.Void WriteFixed64(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.UInt64 value)
  RVA=0x09AF478C  token=0x600037F  System.Void WriteFixed32(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.UInt32 value)
  RVA=0x03077570  token=0x6000380  System.Void WriteBool(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Boolean value)
  RVA=0x03078280  token=0x6000381  System.Void WriteString(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.String value)
  RVA=0x03077890  token=0x6000382  System.Void WriteAsciiStringToBuffer(System.Span<System.Byte> buffer, Google.Protobuf.WriterInternalState& state, System.String value, System.Int32 length)
  RVA=0x09AF4704  token=0x6000383  System.Void NarrowFourUtf16CharsToAsciiAndWriteToBuffer(System.Byte& outputBuffer, System.UInt64 value)
  RVA=0x03078150  token=0x6000384  System.Int32 WriteStringToBuffer(System.Span<System.Byte> buffer, Google.Protobuf.WriterInternalState& state, System.String value)
  RVA=0x04213480  token=0x6000385  System.Void WriteBytes(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, Google.Protobuf.ByteString value)
  RVA=0x03078960  token=0x6000386  System.Void WriteUInt32(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.UInt32 value)
  RVA=0x030795C0  token=0x6000387  System.Void WriteEnum(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Int32 value)
  RVA=0x09AF52F8  token=0x6000388  System.Void WriteSFixed32(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Int32 value)
  RVA=0x09AF5358  token=0x6000389  System.Void WriteSFixed64(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Int64 value)
  RVA=0x09AF53B8  token=0x600038A  System.Void WriteSInt32(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Int32 value)
  RVA=0x09AF5424  token=0x600038B  System.Void WriteSInt64(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Int64 value)
  RVA=0x03078960  token=0x600038C  System.Void WriteLength(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Int32 length)
  RVA=0x030797C0  token=0x600038D  System.Void WriteRawVarint32(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.UInt32 value)
  RVA=0x03078840  token=0x600038E  System.Void WriteRawVarint64(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.UInt64 value)
  RVA=0x09AF4BC8  token=0x600038F  System.Void WriteRawLittleEndian32(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.UInt32 value)
  RVA=0x09AF4B28  token=0x6000390  System.Void WriteRawLittleEndian32SlowPath(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.UInt32 value)
  RVA=0x03077730  token=0x6000391  System.Void WriteRawLittleEndian64(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.UInt64 value)
  RVA=0x09AF4C84  token=0x6000392  System.Void WriteRawLittleEndian64SlowPath(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.UInt64 value)
  RVA=0x0307A120  token=0x6000393  System.Void WriteRawByte(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Byte value)
  RVA=0x09AF49E0  token=0x6000394  System.Void WriteRawBytes(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Byte[] value)
  RVA=0x09AF4A84  token=0x6000395  System.Void WriteRawBytes(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Byte[] value, System.Int32 offset, System.Int32 length)
  RVA=0x04213520  token=0x6000396  System.Void WriteRawBytes(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.ReadOnlySpan<System.Byte> value)
  RVA=0x09AF5490  token=0x6000397  System.Void WriteTag(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Int32 fieldNumber, Google.Protobuf.WireFormat.WireType type)
  RVA=0x03078960  token=0x6000398  System.Void WriteTag(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.UInt32 tag)
  RVA=0x0307A0B0  token=0x6000399  System.Void WriteRawTag(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Byte b1)
  RVA=0x03078F10  token=0x600039A  System.Void WriteRawTag(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Byte b1, System.Byte b2)
  RVA=0x09AF4F68  token=0x600039B  System.Void WriteRawTagSlowPath(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Byte b1, System.Byte b2)
  RVA=0x09AF5218  token=0x600039C  System.Void WriteRawTag(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Byte b1, System.Byte b2, System.Byte b3)
  RVA=0x09AF4E30  token=0x600039D  System.Void WriteRawTagSlowPath(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Byte b1, System.Byte b2, System.Byte b3)
  RVA=0x09AF4FE8  token=0x600039E  System.Void WriteRawTag(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Byte b1, System.Byte b2, System.Byte b3, System.Byte b4)
  RVA=0x09AF4EC4  token=0x600039F  System.Void WriteRawTagSlowPath(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Byte b1, System.Byte b2, System.Byte b3, System.Byte b4)
  RVA=0x09AF50EC  token=0x60003A0  System.Void WriteRawTag(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Byte b1, System.Byte b2, System.Byte b3, System.Byte b4, System.Byte b5)
  RVA=0x09AF4D78  token=0x60003A1  System.Void WriteRawTagSlowPath(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Byte b1, System.Byte b2, System.Byte b3, System.Byte b4, System.Byte b5)
  RVA=0x04DA0DD0  token=0x60003A2  System.UInt32 EncodeZigZag32(System.Int32 n)
  RVA=0x04DA0DE0  token=0x60003A3  System.UInt64 EncodeZigZag64(System.Int64 n)
  RVA=0x04D56D70  token=0x60003A4  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WritingPrimitivesMessages
TYPE:  static class
TOKEN: 0x200005F
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09AF44A8  token=0x60003A5  System.Void WriteMessage(Google.Protobuf.WriteContext& ctx, Google.Protobuf.IMessage value)
  RVA=0x09AF415C  token=0x60003A6  System.Void WriteGroup(Google.Protobuf.WriteContext& ctx, Google.Protobuf.IMessage value)
  RVA=0x09AF4540  token=0x60003A7  System.Void WriteRawMessage(Google.Protobuf.WriteContext& ctx, Google.Protobuf.IMessage message)
END_CLASS

CLASS: Google.Protobuf.Collections.Lists
TYPE:  static class
TOKEN: 0x200012E
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000C02  System.Boolean Equals(System.Collections.Generic.List<T> left, System.Collections.Generic.List<T> right)
  RVA=-1  // generic def  token=0x6000C03  System.Int32 GetHashCode(System.Collections.Generic.List<T> list)
END_CLASS

CLASS: Google.Protobuf.Collections.MapField`2
TYPE:  sealed class
TOKEN: 0x200012F
IMPLEMENTS: Google.Protobuf.IDeepCloneable`1 System.Collections.Generic.IDictionary`2 System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.IEquatable`1 System.Collections.IDictionary System.Collections.ICollection System.Collections.Generic.IReadOnlyDictionary`2 System.Collections.Generic.IReadOnlyCollection`1
FIELDS:
  private   static readonly System.Collections.Generic.EqualityComparer<TValue>ValueEqualityComparer  // static @ 0x0
  private   static readonly System.Collections.Generic.EqualityComparer<TKey>KeyEqualityComparer  // static @ 0x0
  private   readonly System.Collections.Generic.Dictionary<TKey,System.Collections.Generic.LinkedListNode<System.Collections.Generic.KeyValuePair<TKey,TValue>>>map  // 0x0
  private   readonly System.Collections.Generic.LinkedList<System.Collections.Generic.KeyValuePair<TKey,TValue>>list  // 0x0
PROPERTIES:
  Item  get=-1  // not resolved  set=-1  // not resolved
  Keys  get=-1  // not resolved
  Values  get=-1  // not resolved
  Count  get=-1  // not resolved
  IsReadOnly  get=-1  // not resolved
  System.Collections.IDictionary.IsFixedSize  get=-1  // not resolved
  System.Collections.IDictionary.Keys  get=-1  // not resolved
  System.Collections.IDictionary.Values  get=-1  // not resolved
  System.Collections.ICollection.IsSynchronized  get=-1  // not resolved
  System.Collections.ICollection.SyncRoot  get=-1  // not resolved
  System.Collections.IDictionary.Item  get=-1  // not resolved  set=-1  // not resolved
  System.Collections.Generic.IReadOnlyDictionary<TKey,TValue>.Keys  get=-1  // not resolved
  System.Collections.Generic.IReadOnlyDictionary<TKey,TValue>.Values  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000C04  Google.Protobuf.Collections.MapField<TKey,TValue> Clone()
  RVA=-1  // not resolved  token=0x6000C05  System.Void Add(TKey key, TValue value)
  RVA=-1  // not resolved  token=0x6000C06  System.Boolean ContainsKey(TKey key)
  RVA=-1  // not resolved  token=0x6000C07  System.Boolean ContainsValue(TValue value)
  RVA=-1  // not resolved  token=0x6000C08  System.Boolean Remove(TKey key)
  RVA=-1  // not resolved  token=0x6000C09  System.Boolean TryGetValue(TKey key, TValue& value)
  RVA=-1  // not resolved  token=0x6000C0E  System.Void Add(System.Collections.Generic.IDictionary<TKey,TValue> entries)
  RVA=-1  // not resolved  token=0x6000C0F  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey,TValue>> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000C10  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000C11  System.Void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.Add(System.Collections.Generic.KeyValuePair<TKey,TValue> item)
  RVA=-1  // not resolved  token=0x6000C12  System.Void Clear()
  RVA=-1  // not resolved  token=0x6000C13  System.Boolean System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.Contains(System.Collections.Generic.KeyValuePair<TKey,TValue> item)
  RVA=-1  // not resolved  token=0x6000C14  System.Void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.CopyTo(System.Collections.Generic.KeyValuePair<TKey,TValue>[] array, System.Int32 arrayIndex)
  RVA=-1  // not resolved  token=0x6000C15  System.Boolean System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.Remove(System.Collections.Generic.KeyValuePair<TKey,TValue> item)
  RVA=-1  // not resolved  token=0x6000C18  System.Boolean Equals(System.Object other)
  RVA=-1  // not resolved  token=0x6000C19  System.Int32 GetHashCode()
  RVA=-1  // not resolved  token=0x6000C1A  System.Boolean Equals(Google.Protobuf.Collections.MapField<TKey,TValue> other)
  RVA=-1  // not resolved  token=0x6000C1B  System.Void AddEntriesFrom(Google.Protobuf.CodedInputStream input, Google.Protobuf.Collections.MapField.Codec<TKey,TValue> codec)
  RVA=-1  // not resolved  token=0x6000C1C  System.Void AddEntriesFrom(Google.Protobuf.ParseContext& ctx, Google.Protobuf.Collections.MapField.Codec<TKey,TValue> codec)
  RVA=-1  // not resolved  token=0x6000C1D  System.Void WriteTo(Google.Protobuf.CodedOutputStream output, Google.Protobuf.Collections.MapField.Codec<TKey,TValue> codec)
  RVA=-1  // not resolved  token=0x6000C1E  System.Void WriteTo(Google.Protobuf.WriteContext& ctx, Google.Protobuf.Collections.MapField.Codec<TKey,TValue> codec)
  RVA=-1  // not resolved  token=0x6000C1F  System.Int32 CalculateSize(Google.Protobuf.Collections.MapField.Codec<TKey,TValue> codec)
  RVA=-1  // not resolved  token=0x6000C20  System.Int32 CalculateEntrySize(Google.Protobuf.Collections.MapField.Codec<TKey,TValue> codec, System.Collections.Generic.KeyValuePair<TKey,TValue> entry)
  RVA=-1  // not resolved  token=0x6000C21  System.String ToString()
  RVA=-1  // not resolved  token=0x6000C22  System.Void System.Collections.IDictionary.Add(System.Object key, System.Object value)
  RVA=-1  // not resolved  token=0x6000C23  System.Boolean System.Collections.IDictionary.Contains(System.Object key)
  RVA=-1  // not resolved  token=0x6000C24  System.Collections.IDictionaryEnumerator System.Collections.IDictionary.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000C25  System.Void System.Collections.IDictionary.Remove(System.Object key)
  RVA=-1  // not resolved  token=0x6000C26  System.Void System.Collections.ICollection.CopyTo(System.Array array, System.Int32 index)
  RVA=-1  // not resolved  token=0x6000C30  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000C31  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.Collections.ProtobufEqualityComparers
TYPE:  static class
TOKEN: 0x2000135
SIZE:  0x10
FIELDS:
  private   static readonly System.Collections.Generic.EqualityComparer<System.Double><BitwiseDoubleEqualityComparer>k__BackingField  // static @ 0x0
  private   static readonly System.Collections.Generic.EqualityComparer<System.Single><BitwiseSingleEqualityComparer>k__BackingField  // static @ 0x8
  private   static readonly System.Collections.Generic.EqualityComparer<System.Nullable<System.Double>><BitwiseNullableDoubleEqualityComparer>k__BackingField  // static @ 0x10
  private   static readonly System.Collections.Generic.EqualityComparer<System.Nullable<System.Single>><BitwiseNullableSingleEqualityComparer>k__BackingField  // static @ 0x18
PROPERTIES:
  BitwiseDoubleEqualityComparer  get=0x09B22184
  BitwiseSingleEqualityComparer  get=0x09B22274
  BitwiseNullableDoubleEqualityComparer  get=0x09B221D4
  BitwiseNullableSingleEqualityComparer  get=0x09B22224
METHODS:
  RVA=-1  // generic def  token=0x6000C51  System.Collections.Generic.EqualityComparer<T> GetEqualityComparer()
  RVA=0x049DEBD0  token=0x6000C56  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.Collections.ReadOnlyDictionary`2
TYPE:  sealed class
TOKEN: 0x200013A
IMPLEMENTS: System.Collections.Generic.IDictionary`2 System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly System.Collections.Generic.IDictionary<TKey,TValue>wrapped  // 0x0
PROPERTIES:
  Keys  get=-1  // not resolved
  Values  get=-1  // not resolved
  Item  get=-1  // not resolved  set=-1  // not resolved
  Count  get=-1  // not resolved
  IsReadOnly  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000C63  System.Void .ctor(System.Collections.Generic.IDictionary<TKey,TValue> wrapped)
  RVA=-1  // not resolved  token=0x6000C64  System.Void Add(TKey key, TValue value)
  RVA=-1  // not resolved  token=0x6000C65  System.Boolean ContainsKey(TKey key)
  RVA=-1  // not resolved  token=0x6000C67  System.Boolean Remove(TKey key)
  RVA=-1  // not resolved  token=0x6000C68  System.Boolean TryGetValue(TKey key, TValue& value)
  RVA=-1  // not resolved  token=0x6000C6C  System.Void Add(System.Collections.Generic.KeyValuePair<TKey,TValue> item)
  RVA=-1  // not resolved  token=0x6000C6D  System.Void Clear()
  RVA=-1  // not resolved  token=0x6000C6E  System.Boolean Contains(System.Collections.Generic.KeyValuePair<TKey,TValue> item)
  RVA=-1  // not resolved  token=0x6000C6F  System.Void CopyTo(System.Collections.Generic.KeyValuePair<TKey,TValue>[] array, System.Int32 arrayIndex)
  RVA=-1  // not resolved  token=0x6000C72  System.Boolean Remove(System.Collections.Generic.KeyValuePair<TKey,TValue> item)
  RVA=-1  // not resolved  token=0x6000C73  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey,TValue>> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000C74  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000C75  System.Boolean Equals(System.Object obj)
  RVA=-1  // not resolved  token=0x6000C76  System.Int32 GetHashCode()
  RVA=-1  // not resolved  token=0x6000C77  System.String ToString()
END_CLASS

CLASS: Google.Protobuf.Collections.RepeatedField`1
TYPE:  sealed class
TOKEN: 0x200013B
IMPLEMENTS: System.Collections.Generic.IList`1 System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.IList System.Collections.ICollection Google.Protobuf.IDeepCloneable`1 System.IEquatable`1 System.Collections.Generic.IReadOnlyList`1 System.Collections.Generic.IReadOnlyCollection`1
FIELDS:
  private   static readonly System.Collections.Generic.EqualityComparer<T>EqualityComparer  // static @ 0x0
  private   static readonly T[]                             EmptyArray  // static @ 0x0
  private   static  System.Int32                    MinArraySize  // const
  private           T[]                             array  // 0x0
  private           System.Int32                    count  // 0x0
PROPERTIES:
  Capacity  get=-1  // not resolved  set=-1  // not resolved
  Count  get=-1  // not resolved
  IsReadOnly  get=-1  // not resolved
  Item  get=-1  // not resolved  set=-1  // not resolved
  System.Collections.IList.IsFixedSize  get=-1  // not resolved
  System.Collections.ICollection.IsSynchronized  get=-1  // not resolved
  System.Collections.ICollection.SyncRoot  get=-1  // not resolved
  System.Collections.IList.Item  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000C78  Google.Protobuf.Collections.RepeatedField<T> Clone()
  RVA=-1  // not resolved  token=0x6000C79  System.Void AddEntriesFrom(Google.Protobuf.CodedInputStream input, Google.Protobuf.FieldCodec<T> codec)
  RVA=-1  // not resolved  token=0x6000C7A  System.Void AddEntriesFrom(Google.Protobuf.ParseContext& ctx, Google.Protobuf.FieldCodec<T> codec)
  RVA=-1  // not resolved  token=0x6000C7B  System.Int32 CalculateSize(Google.Protobuf.FieldCodec<T> codec)
  RVA=-1  // not resolved  token=0x6000C7C  System.Int32 CalculatePackedDataSize(Google.Protobuf.FieldCodec<T> codec)
  RVA=-1  // not resolved  token=0x6000C7D  System.Void WriteTo(Google.Protobuf.CodedOutputStream output, Google.Protobuf.FieldCodec<T> codec)
  RVA=-1  // not resolved  token=0x6000C7E  System.Void WriteTo(Google.Protobuf.WriteContext& ctx, Google.Protobuf.FieldCodec<T> codec)
  RVA=-1  // not resolved  token=0x6000C81  System.Void EnsureSize(System.Int32 size)
  RVA=-1  // not resolved  token=0x6000C82  System.Void SetSize(System.Int32 size)
  RVA=-1  // not resolved  token=0x6000C83  System.Void SetCount(System.Int32 newCount)
  RVA=-1  // not resolved  token=0x6000C84  System.Void Add(T item)
  RVA=-1  // not resolved  token=0x6000C85  System.Void Clear()
  RVA=-1  // not resolved  token=0x6000C86  System.Boolean Contains(T item)
  RVA=-1  // not resolved  token=0x6000C87  System.Void CopyTo(T[] array, System.Int32 arrayIndex)
  RVA=-1  // not resolved  token=0x6000C88  System.Boolean Remove(T item)
  RVA=-1  // not resolved  token=0x6000C8B  System.Void AddRange(System.Collections.Generic.IEnumerable<T> values)
  RVA=-1  // not resolved  token=0x6000C8C  System.Void Add(System.Collections.Generic.IEnumerable<T> values)
  RVA=-1  // not resolved  token=0x6000C8D  System.Collections.Generic.IEnumerator<T> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000C8E  System.Boolean Equals(System.Object obj)
  RVA=-1  // not resolved  token=0x6000C8F  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000C90  System.Int32 GetHashCode()
  RVA=-1  // not resolved  token=0x6000C91  System.Boolean Equals(Google.Protobuf.Collections.RepeatedField<T> other)
  RVA=-1  // not resolved  token=0x6000C92  System.Int32 IndexOf(T item)
  RVA=-1  // not resolved  token=0x6000C93  System.Void Insert(System.Int32 index, T item)
  RVA=-1  // not resolved  token=0x6000C94  System.Void RemoveAt(System.Int32 index)
  RVA=-1  // not resolved  token=0x6000C95  System.String ToString()
  RVA=-1  // not resolved  token=0x6000C99  System.Void System.Collections.ICollection.CopyTo(System.Array array, System.Int32 index)
  RVA=-1  // not resolved  token=0x6000C9E  System.Int32 System.Collections.IList.Add(System.Object value)
  RVA=-1  // not resolved  token=0x6000C9F  System.Boolean System.Collections.IList.Contains(System.Object value)
  RVA=-1  // not resolved  token=0x6000CA0  System.Int32 System.Collections.IList.IndexOf(System.Object value)
  RVA=-1  // not resolved  token=0x6000CA1  System.Void System.Collections.IList.Insert(System.Int32 index, System.Object value)
  RVA=-1  // not resolved  token=0x6000CA2  System.Void System.Collections.IList.Remove(System.Object value)
  RVA=-1  // not resolved  token=0x6000CA3  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000CA4  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.Compatibility.PropertyInfoExtensions
TYPE:  static class
TOKEN: 0x200012C
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09B220E4  token=0x6000BFD  System.Reflection.MethodInfo GetGetMethod(System.Reflection.PropertyInfo target)
  RVA=0x09B22134  token=0x6000BFE  System.Reflection.MethodInfo GetSetMethod(System.Reflection.PropertyInfo target)
END_CLASS

CLASS: Google.Protobuf.Compatibility.TypeExtensions
TYPE:  static class
TOKEN: 0x200012D
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09B23E44  token=0x6000BFF  System.Boolean IsAssignableFrom(System.Type target, System.Type c)
  RVA=0x09B23D30  token=0x6000C00  System.Reflection.PropertyInfo GetProperty(System.Type target, System.String name)
  RVA=0x09B23C78  token=0x6000C01  System.Reflection.MethodInfo GetMethod(System.Type target, System.String name)
END_CLASS

CLASS: Google.Protobuf.Reflection.CustomOptions
TYPE:  sealed class
TOKEN: 0x20000A6
SIZE:  0x18
FIELDS:
  private   static  System.String                   UnreferencedCodeMessage  // const
  private   static readonly System.Object[]                 EmptyParameters  // static @ 0x0
  private   readonly System.Collections.Generic.IDictionary<System.Int32,Google.Protobuf.IExtensionValue>values  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000683  System.Void .ctor(System.Collections.Generic.IDictionary<System.Int32,Google.Protobuf.IExtensionValue> values)
  RVA=0x09AF63CC  token=0x6000684  System.Boolean TryGetBool(System.Int32 field, System.Boolean& value)
  RVA=0x09AF653C  token=0x6000685  System.Boolean TryGetInt32(System.Int32 field, System.Int32& value)
  RVA=0x09AF6594  token=0x6000686  System.Boolean TryGetInt64(System.Int32 field, System.Int64& value)
  RVA=0x09AF64D4  token=0x6000687  System.Boolean TryGetFixed32(System.Int32 field, System.UInt32& value)
  RVA=0x09AF64DC  token=0x6000688  System.Boolean TryGetFixed64(System.Int32 field, System.UInt64& value)
  RVA=0x09AF65EC  token=0x6000689  System.Boolean TryGetSFixed32(System.Int32 field, System.Int32& value)
  RVA=0x09AF65F4  token=0x600068A  System.Boolean TryGetSFixed64(System.Int32 field, System.Int64& value)
  RVA=0x09AF65FC  token=0x600068B  System.Boolean TryGetSInt32(System.Int32 field, System.Int32& value)
  RVA=0x09AF6654  token=0x600068C  System.Boolean TryGetSInt64(System.Int32 field, System.Int64& value)
  RVA=0x09AF6704  token=0x600068D  System.Boolean TryGetUInt32(System.Int32 field, System.UInt32& value)
  RVA=0x09AF675C  token=0x600068E  System.Boolean TryGetUInt64(System.Int32 field, System.UInt64& value)
  RVA=0x09AF64E4  token=0x600068F  System.Boolean TryGetFloat(System.Int32 field, System.Single& value)
  RVA=0x09AF647C  token=0x6000690  System.Boolean TryGetDouble(System.Int32 field, System.Double& value)
  RVA=0x09AF66AC  token=0x6000691  System.Boolean TryGetString(System.Int32 field, System.String& value)
  RVA=0x09AF6424  token=0x6000692  System.Boolean TryGetBytes(System.Int32 field, Google.Protobuf.ByteString& value)
  RVA=-1  // generic def  token=0x6000693  System.Boolean TryGetMessage(System.Int32 field, T& value)
  RVA=-1  // generic def  token=0x6000694  System.Boolean TryGetPrimitiveValue(System.Int32 field, T& value)
  RVA=0x09AF67B4  token=0x6000695  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.Reflection.DescriptorReflection
TYPE:  static class
TOKEN: 0x20000A8
SIZE:  0x10
FIELDS:
  private   static  Google.Protobuf.Reflection.FileDescriptordescriptor  // static @ 0x0
PROPERTIES:
  Descriptor  get=0x09AFB72C
METHODS:
  RVA=0x09AF8150  token=0x600069B  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.Reflection.FileDescriptorSet
TYPE:  sealed class
TOKEN: 0x20000A9
SIZE:  0x20
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.FileDescriptorSet>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    FileFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.FileDescriptorProto>_repeated_file_codec  // static @ 0x8
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.FileDescriptorProto>file_  // 0x18
PROPERTIES:
  Parser  get=0x09B03C90
  Descriptor  get=0x09B03C30
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B03E54
  File  get=0x0385B100
METHODS:
  RVA=0x09B03BB8  token=0x600069F  System.Void .ctor()
  RVA=0x09B03B30  token=0x60006A0  System.Void .ctor(Google.Protobuf.Reflection.FileDescriptorSet other)
  RVA=0x09B037BC  token=0x60006A1  Google.Protobuf.Reflection.FileDescriptorSet Clone()
  RVA=0x09B0381C  token=0x60006A3  System.Boolean Equals(System.Object other)
  RVA=0x09B03880  token=0x60006A4  System.Boolean Equals(Google.Protobuf.Reflection.FileDescriptorSet other)
  RVA=0x0638F5B8  token=0x60006A5  System.Int32 GetHashCode()
  RVA=0x09B03974  token=0x60006A6  System.String ToString()
  RVA=0x04D52400  token=0x60006A7  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09B03DB0  token=0x60006A8  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09B03728  token=0x60006A9  System.Int32 CalculateSize()
  RVA=0x09B038FC  token=0x60006AA  System.Void MergeFrom(Google.Protobuf.Reflection.FileDescriptorSet other)
  RVA=0x04D51840  token=0x60006AB  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09B03CE0  token=0x60006AC  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09B039C0  token=0x60006AD  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.Reflection.FileDescriptorProto
TYPE:  sealed class
TOKEN: 0x20000AB
SIZE:  0x78
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.FileDescriptorProto>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    NameFieldNumber  // const
  private   static readonly System.String                   NameDefaultValue  // static @ 0x8
  private           System.String                   name_  // 0x18
  public    static  System.Int32                    PackageFieldNumber  // const
  private   static readonly System.String                   PackageDefaultValue  // static @ 0x10
  private           System.String                   package_  // 0x20
  public    static  System.Int32                    DependencyFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<System.String>_repeated_dependency_codec  // static @ 0x18
  private   readonly Google.Protobuf.Collections.RepeatedField<System.String>dependency_  // 0x28
  public    static  System.Int32                    PublicDependencyFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<System.Int32>_repeated_publicDependency_codec  // static @ 0x20
  private   readonly Google.Protobuf.Collections.RepeatedField<System.Int32>publicDependency_  // 0x30
  public    static  System.Int32                    WeakDependencyFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<System.Int32>_repeated_weakDependency_codec  // static @ 0x28
  private   readonly Google.Protobuf.Collections.RepeatedField<System.Int32>weakDependency_  // 0x38
  public    static  System.Int32                    MessageTypeFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.DescriptorProto>_repeated_messageType_codec  // static @ 0x30
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.DescriptorProto>messageType_  // 0x40
  public    static  System.Int32                    EnumTypeFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.EnumDescriptorProto>_repeated_enumType_codec  // static @ 0x38
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.EnumDescriptorProto>enumType_  // 0x48
  public    static  System.Int32                    ServiceFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.ServiceDescriptorProto>_repeated_service_codec  // static @ 0x40
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.ServiceDescriptorProto>service_  // 0x50
  public    static  System.Int32                    ExtensionFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.FieldDescriptorProto>_repeated_extension_codec  // static @ 0x48
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.FieldDescriptorProto>extension_  // 0x58
  public    static  System.Int32                    OptionsFieldNumber  // const
  private           Google.Protobuf.Reflection.FileOptionsoptions_  // 0x60
  public    static  System.Int32                    SourceCodeInfoFieldNumber  // const
  private           Google.Protobuf.Reflection.SourceCodeInfosourceCodeInfo_  // 0x68
  public    static  System.Int32                    SyntaxFieldNumber  // const
  private   static readonly System.String                   SyntaxDefaultValue  // static @ 0x50
  private           System.String                   syntax_  // 0x70
PROPERTIES:
  Parser  get=0x09B02E60
  Descriptor  get=0x09B02D4C
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B035B0
  Name  get=0x09B02DB8  set=0x09B035F0
  HasName  get=0x067B9E50
  Package  get=0x09B02E0C  set=0x09B03658
  HasPackage  get=0x09B00070
  Dependency  get=0x04D86240
  PublicDependency  get=0x011F36E0
  WeakDependency  get=0x04D85A50
  MessageType  get=0x04D85A60
  EnumType  get=0x04D86200
  Service  get=0x04D86270
  Extension  get=0x02E56440
  Options  get=0x04D86280  set=0x063CC164
  SourceCodeInfo  get=0x04D86230  set=0x0485F170
  Syntax  get=0x09B02EB0  set=0x09B036C0
  HasSyntax  get=0x09B02DAC
METHODS:
  RVA=0x09B02B24  token=0x60006B4  System.Void .ctor()
  RVA=0x09B028F4  token=0x60006B5  System.Void .ctor(Google.Protobuf.Reflection.FileDescriptorProto other)
  RVA=0x09B01D78  token=0x60006B6  Google.Protobuf.Reflection.FileDescriptorProto Clone()
  RVA=0x05390C40  token=0x60006BA  System.Void ClearName()
  RVA=0x09AFF598  token=0x60006BE  System.Void ClearPackage()
  RVA=0x09B01D64  token=0x60006CD  System.Void ClearSyntax()
  RVA=0x09B01DD8  token=0x60006CE  System.Boolean Equals(System.Object other)
  RVA=0x09B01E3C  token=0x60006CF  System.Boolean Equals(Google.Protobuf.Reflection.FileDescriptorProto other)
  RVA=0x09B02090  token=0x60006D0  System.Int32 GetHashCode()
  RVA=0x09B024C4  token=0x60006D1  System.String ToString()
  RVA=0x04D52400  token=0x60006D2  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09B032E8  token=0x60006D3  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09B01A84  token=0x60006D4  System.Int32 CalculateSize()
  RVA=0x09B02234  token=0x60006D5  System.Void MergeFrom(Google.Protobuf.Reflection.FileDescriptorProto other)
  RVA=0x04D51840  token=0x60006D6  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09B02F04  token=0x60006D7  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09B02510  token=0x60006D8  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.Reflection.DescriptorProto
TYPE:  sealed class
TOKEN: 0x20000AD
SIZE:  0x68
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.DescriptorProto>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    NameFieldNumber  // const
  private   static readonly System.String                   NameDefaultValue  // static @ 0x8
  private           System.String                   name_  // 0x18
  public    static  System.Int32                    FieldFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.FieldDescriptorProto>_repeated_field_codec  // static @ 0x10
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.FieldDescriptorProto>field_  // 0x20
  public    static  System.Int32                    ExtensionFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.FieldDescriptorProto>_repeated_extension_codec  // static @ 0x18
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.FieldDescriptorProto>extension_  // 0x28
  public    static  System.Int32                    NestedTypeFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.DescriptorProto>_repeated_nestedType_codec  // static @ 0x20
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.DescriptorProto>nestedType_  // 0x30
  public    static  System.Int32                    EnumTypeFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.EnumDescriptorProto>_repeated_enumType_codec  // static @ 0x28
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.EnumDescriptorProto>enumType_  // 0x38
  public    static  System.Int32                    ExtensionRangeFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.DescriptorProto.Types.ExtensionRange>_repeated_extensionRange_codec  // static @ 0x30
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.DescriptorProto.Types.ExtensionRange>extensionRange_  // 0x40
  public    static  System.Int32                    OneofDeclFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.OneofDescriptorProto>_repeated_oneofDecl_codec  // static @ 0x38
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.OneofDescriptorProto>oneofDecl_  // 0x48
  public    static  System.Int32                    OptionsFieldNumber  // const
  private           Google.Protobuf.Reflection.MessageOptionsoptions_  // 0x50
  public    static  System.Int32                    ReservedRangeFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.DescriptorProto.Types.ReservedRange>_repeated_reservedRange_codec  // static @ 0x40
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.DescriptorProto.Types.ReservedRange>reservedRange_  // 0x58
  public    static  System.Int32                    ReservedNameFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<System.String>_repeated_reservedName_codec  // static @ 0x48
  private   readonly Google.Protobuf.Collections.RepeatedField<System.String>reservedName_  // 0x60
PROPERTIES:
  Parser  get=0x09AF7A74
  Descriptor  get=0x09AF79C0
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09AF80A8
  Name  get=0x09AF7A20  set=0x09AF80E8
  HasName  get=0x067B9E50
  Field  get=0x04D862C0
  Extension  get=0x04D86240
  NestedType  get=0x011F36E0
  EnumType  get=0x04D85A50
  ExtensionRange  get=0x04D85A60
  OneofDecl  get=0x04D86200
  Options  get=0x04D86270  set=0x02FDAEA0
  ReservedRange  get=0x02E56440
  ReservedName  get=0x04D86280
METHODS:
  RVA=0x09AF774C  token=0x60006DF  System.Void .ctor()
  RVA=0x09AF7540  token=0x60006E0  System.Void .ctor(Google.Protobuf.Reflection.DescriptorProto other)
  RVA=0x09AF6AB8  token=0x60006E1  Google.Protobuf.Reflection.DescriptorProto Clone()
  RVA=0x05390C40  token=0x60006E5  System.Void ClearName()
  RVA=0x09AF6D24  token=0x60006F0  System.Boolean Equals(System.Object other)
  RVA=0x09AF6B18  token=0x60006F1  System.Boolean Equals(Google.Protobuf.Reflection.DescriptorProto other)
  RVA=0x09AF6D88  token=0x60006F2  System.Int32 GetHashCode()
  RVA=0x09AF70E8  token=0x60006F3  System.String ToString()
  RVA=0x04D52400  token=0x60006F4  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09AF7E24  token=0x60006F5  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09AF6818  token=0x60006F6  System.Int32 CalculateSize()
  RVA=0x09AF6ED4  token=0x60006F7  System.Void MergeFrom(Google.Protobuf.Reflection.DescriptorProto other)
  RVA=0x04D51840  token=0x60006F8  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09AF7AC4  token=0x60006F9  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09AF7134  token=0x60006FA  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.Reflection.ExtensionRangeOptions
TYPE:  sealed class
TOKEN: 0x20000B4
SIZE:  0x28
IMPLEMENTS: Google.Protobuf.IExtendableMessage`1 Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.ExtensionRangeOptions>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  private           Google.Protobuf.ExtensionSet<Google.Protobuf.Reflection.ExtensionRangeOptions>_extensions  // 0x18
  public    static  System.Int32                    UninterpretedOptionFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.UninterpretedOption>_repeated_uninterpretedOption_codec  // static @ 0x8
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.UninterpretedOption>uninterpretedOption_  // 0x20
PROPERTIES:
  _Extensions  get=0x0385B100
  Parser  get=0x09AFE834
  Descriptor  get=0x09AFE7D4
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09AFEA4C
  UninterpretedOption  get=0x04D862C0
METHODS:
  RVA=0x09AFE75C  token=0x600073C  System.Void .ctor()
  RVA=0x09AFE6AC  token=0x600073D  System.Void .ctor(Google.Protobuf.Reflection.ExtensionRangeOptions other)
  RVA=0x09AFE29C  token=0x600073E  Google.Protobuf.Reflection.ExtensionRangeOptions Clone()
  RVA=0x09AFE38C  token=0x6000740  System.Boolean Equals(System.Object other)
  RVA=0x09AFE2FC  token=0x6000741  System.Boolean Equals(Google.Protobuf.Reflection.ExtensionRangeOptions other)
  RVA=0x09AFE3F0  token=0x6000742  System.Int32 GetHashCode()
  RVA=0x09AFE4F0  token=0x6000743  System.String ToString()
  RVA=0x04D52400  token=0x6000744  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09AFE984  token=0x6000745  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09AFE1E0  token=0x6000746  System.Int32 CalculateSize()
  RVA=0x09AFE458  token=0x6000747  System.Void MergeFrom(Google.Protobuf.Reflection.ExtensionRangeOptions other)
  RVA=0x04D51840  token=0x6000748  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09AFE884  token=0x6000749  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=-1  // generic def  token=0x600074A  TValue GetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.ExtensionRangeOptions,TValue> extension)
  RVA=-1  // generic def  token=0x600074B  Google.Protobuf.Collections.RepeatedField<TValue> GetExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.ExtensionRangeOptions,TValue> extension)
  RVA=-1  // generic def  token=0x600074C  Google.Protobuf.Collections.RepeatedField<TValue> GetOrInitializeExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.ExtensionRangeOptions,TValue> extension)
  RVA=-1  // generic def  token=0x600074D  System.Void SetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.ExtensionRangeOptions,TValue> extension, TValue value)
  RVA=-1  // generic def  token=0x600074E  System.Boolean HasExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.ExtensionRangeOptions,TValue> extension)
  RVA=-1  // generic def  token=0x600074F  System.Void ClearExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.ExtensionRangeOptions,TValue> extension)
  RVA=-1  // generic def  token=0x6000750  System.Void ClearExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.ExtensionRangeOptions,TValue> extension)
  RVA=0x09AFE53C  token=0x6000751  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.Reflection.FieldDescriptorProto
TYPE:  sealed class
TOKEN: 0x20000B6
SIZE:  0x70
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.FieldDescriptorProto>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  private           System.Int32                    _hasBits0  // 0x18
  public    static  System.Int32                    NameFieldNumber  // const
  private   static readonly System.String                   NameDefaultValue  // static @ 0x8
  private           System.String                   name_  // 0x20
  public    static  System.Int32                    NumberFieldNumber  // const
  private   static readonly System.Int32                    NumberDefaultValue  // static @ 0x10
  private           System.Int32                    number_  // 0x28
  public    static  System.Int32                    LabelFieldNumber  // const
  private   static readonly Google.Protobuf.Reflection.FieldDescriptorProto.Types.LabelLabelDefaultValue  // static @ 0x14
  private           Google.Protobuf.Reflection.FieldDescriptorProto.Types.Labellabel_  // 0x2c
  public    static  System.Int32                    TypeFieldNumber  // const
  private   static readonly Google.Protobuf.Reflection.FieldDescriptorProto.Types.TypeTypeDefaultValue  // static @ 0x18
  private           Google.Protobuf.Reflection.FieldDescriptorProto.Types.Typetype_  // 0x30
  public    static  System.Int32                    TypeNameFieldNumber  // const
  private   static readonly System.String                   TypeNameDefaultValue  // static @ 0x20
  private           System.String                   typeName_  // 0x38
  public    static  System.Int32                    ExtendeeFieldNumber  // const
  private   static readonly System.String                   ExtendeeDefaultValue  // static @ 0x28
  private           System.String                   extendee_  // 0x40
  public    static  System.Int32                    DefaultValueFieldNumber  // const
  private   static readonly System.String                   DefaultValueDefaultValue  // static @ 0x30
  private           System.String                   defaultValue_  // 0x48
  public    static  System.Int32                    OneofIndexFieldNumber  // const
  private   static readonly System.Int32                    OneofIndexDefaultValue  // static @ 0x38
  private           System.Int32                    oneofIndex_  // 0x50
  public    static  System.Int32                    JsonNameFieldNumber  // const
  private   static readonly System.String                   JsonNameDefaultValue  // static @ 0x40
  private           System.String                   jsonName_  // 0x58
  public    static  System.Int32                    OptionsFieldNumber  // const
  private           Google.Protobuf.Reflection.FieldOptionsoptions_  // 0x60
  public    static  System.Int32                    Proto3OptionalFieldNumber  // const
  private   static readonly System.Boolean                  Proto3OptionalDefaultValue  // static @ 0x48
  private           System.Boolean                  proto3Optional_  // 0x68
PROPERTIES:
  Parser  get=0x09B00244
  Descriptor  get=0x09AFFFA4
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B007D4
  Name  get=0x09B00148  set=0x09B0094C
  HasName  get=0x09B00070
  Number  get=0x09B0019C  set=0x04DA0E60
  HasNumber  get=0x0314ECE0
  Label  get=0x09B000F4  set=0x04DA0E50
  HasLabel  get=0x09AFF0A8
  Type  get=0x09B0033C  set=0x04DA0E90
  HasType  get=0x09B00098
  TypeName  get=0x09B002E8  set=0x09B009B4
  HasTypeName  get=0x09B0008C
  Extendee  get=0x09B00004  set=0x09B0087C
  HasExtendee  get=0x09B00058
  DefaultValue  get=0x09AFFF50  set=0x09B00814
  HasDefaultValue  get=0x090BCB14
  OneofIndex  get=0x09B001F0  set=0x04DA0E70
  HasOneofIndex  get=0x09B0007C
  JsonName  get=0x09B000A0  set=0x09B008E4
  HasJsonName  get=0x09B00064
  Options  get=0x04D86280  set=0x063CC164
  Proto3Optional  get=0x09B00294  set=0x04DA0E80
  HasProto3Optional  get=0x09B00084
METHODS:
  RVA=0x041E1670  token=0x6000758  System.Void .ctor()
  RVA=0x09AFFE6C  token=0x6000759  System.Void .ctor(Google.Protobuf.Reflection.FieldDescriptorProto other)
  RVA=0x09AFF5AC  token=0x600075A  Google.Protobuf.Reflection.FieldDescriptorProto Clone()
  RVA=0x09AFF598  token=0x600075E  System.Void ClearName()
  RVA=0x04D86DE0  token=0x6000762  System.Void ClearNumber()
  RVA=0x04DA0DF0  token=0x6000766  System.Void ClearLabel()
  RVA=0x04DA0E40  token=0x600076A  System.Void ClearType()
  RVA=0x0709F154  token=0x600076E  System.Void ClearTypeName()
  RVA=0x09AFF570  token=0x6000772  System.Void ClearExtendee()
  RVA=0x070A7CA8  token=0x6000776  System.Void ClearDefaultValue()
  RVA=0x04DA0E20  token=0x600077A  System.Void ClearOneofIndex()
  RVA=0x09AFF584  token=0x600077E  System.Void ClearJsonName()
  RVA=0x04DA0E30  token=0x6000784  System.Void ClearProto3Optional()
  RVA=0x09AFF60C  token=0x6000785  System.Boolean Equals(System.Object other)
  RVA=0x09AFF670  token=0x6000786  System.Boolean Equals(Google.Protobuf.Reflection.FieldDescriptorProto other)
  RVA=0x09AFF83C  token=0x6000787  System.Int32 GetHashCode()
  RVA=0x09AFFBB8  token=0x6000788  System.String ToString()
  RVA=0x04D52400  token=0x6000789  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09B005D0  token=0x600078A  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09AFF358  token=0x600078B  System.Int32 CalculateSize()
  RVA=0x09AFF9EC  token=0x600078C  System.Void MergeFrom(Google.Protobuf.Reflection.FieldDescriptorProto other)
  RVA=0x04D51840  token=0x600078D  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09B00390  token=0x600078E  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09AFFC04  token=0x600078F  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.Reflection.OneofDescriptorProto
TYPE:  sealed class
TOKEN: 0x20000BB
SIZE:  0x28
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.OneofDescriptorProto>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    NameFieldNumber  // const
  private   static readonly System.String                   NameDefaultValue  // static @ 0x8
  private           System.String                   name_  // 0x18
  public    static  System.Int32                    OptionsFieldNumber  // const
  private           Google.Protobuf.Reflection.OneofOptionsoptions_  // 0x20
PROPERTIES:
  Parser  get=0x09B04F98
  Descriptor  get=0x09B04EE4
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B0515C
  Name  get=0x09B04F44  set=0x09B0519C
  HasName  get=0x067B9E50
  Options  get=0x04D862C0  set=0x05390F40
METHODS:
  RVA=0x041E1670  token=0x6000796  System.Void .ctor()
  RVA=0x09B04E70  token=0x6000797  System.Void .ctor(Google.Protobuf.Reflection.OneofDescriptorProto other)
  RVA=0x09B04A48  token=0x6000798  Google.Protobuf.Reflection.OneofDescriptorProto Clone()
  RVA=0x05390C40  token=0x600079C  System.Void ClearName()
  RVA=0x09B04B2C  token=0x600079F  System.Boolean Equals(System.Object other)
  RVA=0x09B04AA8  token=0x60007A0  System.Boolean Equals(Google.Protobuf.Reflection.OneofDescriptorProto other)
  RVA=0x09B04B90  token=0x60007A1  System.Int32 GetHashCode()
  RVA=0x09B04CE0  token=0x60007A2  System.String ToString()
  RVA=0x04D52400  token=0x60007A3  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09B050DC  token=0x60007A4  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09B049A0  token=0x60007A5  System.Int32 CalculateSize()
  RVA=0x09B04C04  token=0x60007A6  System.Void MergeFrom(Google.Protobuf.Reflection.OneofDescriptorProto other)
  RVA=0x04D51840  token=0x60007A7  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09B04FE8  token=0x60007A8  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09B04D2C  token=0x60007A9  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.Reflection.EnumDescriptorProto
TYPE:  sealed class
TOKEN: 0x20000BD
SIZE:  0x40
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.EnumDescriptorProto>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    NameFieldNumber  // const
  private   static readonly System.String                   NameDefaultValue  // static @ 0x8
  private           System.String                   name_  // 0x18
  public    static  System.Int32                    ValueFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.EnumValueDescriptorProto>_repeated_value_codec  // static @ 0x10
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.EnumValueDescriptorProto>value_  // 0x20
  public    static  System.Int32                    OptionsFieldNumber  // const
  private           Google.Protobuf.Reflection.EnumOptionsoptions_  // 0x28
  public    static  System.Int32                    ReservedRangeFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.EnumDescriptorProto.Types.EnumReservedRange>_repeated_reservedRange_codec  // static @ 0x18
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.EnumDescriptorProto.Types.EnumReservedRange>reservedRange_  // 0x30
  public    static  System.Int32                    ReservedNameFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<System.String>_repeated_reservedName_codec  // static @ 0x20
  private   readonly Google.Protobuf.Collections.RepeatedField<System.String>reservedName_  // 0x38
PROPERTIES:
  Parser  get=0x09AFC6A8
  Descriptor  get=0x09AFC5F4
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09AFCA54
  Name  get=0x09AFC654  set=0x09AFCA94
  HasName  get=0x067B9E50
  Value  get=0x04D862C0
  Options  get=0x04D86240  set=0x02FDB880
  ReservedRange  get=0x011F36E0
  ReservedName  get=0x04D85A50
METHODS:
  RVA=0x09AFC3C8  token=0x60007B0  System.Void .ctor()
  RVA=0x09AFC4D4  token=0x60007B1  System.Void .ctor(Google.Protobuf.Reflection.EnumDescriptorProto other)
  RVA=0x09AFBD28  token=0x60007B2  Google.Protobuf.Reflection.EnumDescriptorProto Clone()
  RVA=0x05390C40  token=0x60007B6  System.Void ClearName()
  RVA=0x09AFBD88  token=0x60007BC  System.Boolean Equals(System.Object other)
  RVA=0x09AFBDEC  token=0x60007BD  System.Boolean Equals(Google.Protobuf.Reflection.EnumDescriptorProto other)
  RVA=0x09AFBF10  token=0x60007BE  System.Int32 GetHashCode()
  RVA=0x09AFC130  token=0x60007BF  System.String ToString()
  RVA=0x04D52400  token=0x60007C0  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09AFC8E4  token=0x60007C1  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09AFBBA0  token=0x60007C2  System.Int32 CalculateSize()
  RVA=0x09AFBFDC  token=0x60007C3  System.Void MergeFrom(Google.Protobuf.Reflection.EnumDescriptorProto other)
  RVA=0x04D51840  token=0x60007C4  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09AFC6F8  token=0x60007C5  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09AFC17C  token=0x60007C6  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.Reflection.EnumValueDescriptorProto
TYPE:  sealed class
TOKEN: 0x20000C2
SIZE:  0x38
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.EnumValueDescriptorProto>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  private           System.Int32                    _hasBits0  // 0x18
  public    static  System.Int32                    NameFieldNumber  // const
  private   static readonly System.String                   NameDefaultValue  // static @ 0x8
  private           System.String                   name_  // 0x20
  public    static  System.Int32                    NumberFieldNumber  // const
  private   static readonly System.Int32                    NumberDefaultValue  // static @ 0x10
  private           System.Int32                    number_  // 0x28
  public    static  System.Int32                    OptionsFieldNumber  // const
  private           Google.Protobuf.Reflection.EnumValueOptionsoptions_  // 0x30
PROPERTIES:
  Parser  get=0x09B0D6E0
  Descriptor  get=0x09B0D5D8
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B0D8F8
  Name  get=0x09B0D638  set=0x09B0D938
  HasName  get=0x09B00070
  Number  get=0x09B0D68C  set=0x04DA0E60
  HasNumber  get=0x0314ECE0
  Options  get=0x011F36E0  set=0x05396200
METHODS:
  RVA=0x041E1670  token=0x60007E9  System.Void .ctor()
  RVA=0x09B0D558  token=0x60007EA  System.Void .ctor(Google.Protobuf.Reflection.EnumValueDescriptorProto other)
  RVA=0x09B0D0D4  token=0x60007EB  Google.Protobuf.Reflection.EnumValueDescriptorProto Clone()
  RVA=0x09AFF598  token=0x60007EF  System.Void ClearName()
  RVA=0x04D86DE0  token=0x60007F3  System.Void ClearNumber()
  RVA=0x09B0D134  token=0x60007F6  System.Boolean Equals(System.Object other)
  RVA=0x09B0D198  token=0x60007F7  System.Boolean Equals(Google.Protobuf.Reflection.EnumValueDescriptorProto other)
  RVA=0x09B0D234  token=0x60007F8  System.Int32 GetHashCode()
  RVA=0x09B0D3B0  token=0x60007F9  System.String ToString()
  RVA=0x04D52400  token=0x60007FA  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09B0D850  token=0x60007FB  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09B0D000  token=0x60007FC  System.Int32 CalculateSize()
  RVA=0x09B0D2BC  token=0x60007FD  System.Void MergeFrom(Google.Protobuf.Reflection.EnumValueDescriptorProto other)
  RVA=0x04D51840  token=0x60007FE  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09B0D730  token=0x60007FF  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09B0D3FC  token=0x6000800  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.Reflection.ServiceDescriptorProto
TYPE:  sealed class
TOKEN: 0x20000C4
SIZE:  0x30
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.ServiceDescriptorProto>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    NameFieldNumber  // const
  private   static readonly System.String                   NameDefaultValue  // static @ 0x8
  private           System.String                   name_  // 0x18
  public    static  System.Int32                    MethodFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.MethodDescriptorProto>_repeated_method_codec  // static @ 0x10
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.MethodDescriptorProto>method_  // 0x20
  public    static  System.Int32                    OptionsFieldNumber  // const
  private           Google.Protobuf.Reflection.ServiceOptionsoptions_  // 0x28
PROPERTIES:
  Parser  get=0x09B1817C
  Descriptor  get=0x09B180C8
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B18410
  Name  get=0x09B18128  set=0x09B18450
  HasName  get=0x067B9E50
  Method  get=0x04D862C0
  Options  get=0x04D86240  set=0x02FDB880
METHODS:
  RVA=0x09B17F8C  token=0x6000807  System.Void .ctor()
  RVA=0x09B18004  token=0x6000808  System.Void .ctor(Google.Protobuf.Reflection.ServiceDescriptorProto other)
  RVA=0x09B17A80  token=0x6000809  Google.Protobuf.Reflection.ServiceDescriptorProto Clone()
  RVA=0x05390C40  token=0x600080D  System.Void ClearName()
  RVA=0x09B17BA4  token=0x6000811  System.Boolean Equals(System.Object other)
  RVA=0x09B17AE0  token=0x6000812  System.Boolean Equals(Google.Protobuf.Reflection.ServiceDescriptorProto other)
  RVA=0x09B17C08  token=0x6000813  System.Int32 GetHashCode()
  RVA=0x09B17D94  token=0x6000814  System.String ToString()
  RVA=0x04D52400  token=0x6000815  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09B18320  token=0x6000816  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09B17980  token=0x6000817  System.Int32 CalculateSize()
  RVA=0x09B17C90  token=0x6000818  System.Void MergeFrom(Google.Protobuf.Reflection.ServiceDescriptorProto other)
  RVA=0x04D51840  token=0x6000819  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09B181CC  token=0x600081A  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09B17DE0  token=0x600081B  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.Reflection.MethodDescriptorProto
TYPE:  sealed class
TOKEN: 0x20000C6
SIZE:  0x48
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.MethodDescriptorProto>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  private           System.Int32                    _hasBits0  // 0x18
  public    static  System.Int32                    NameFieldNumber  // const
  private   static readonly System.String                   NameDefaultValue  // static @ 0x8
  private           System.String                   name_  // 0x20
  public    static  System.Int32                    InputTypeFieldNumber  // const
  private   static readonly System.String                   InputTypeDefaultValue  // static @ 0x10
  private           System.String                   inputType_  // 0x28
  public    static  System.Int32                    OutputTypeFieldNumber  // const
  private   static readonly System.String                   OutputTypeDefaultValue  // static @ 0x18
  private           System.String                   outputType_  // 0x30
  public    static  System.Int32                    OptionsFieldNumber  // const
  private           Google.Protobuf.Reflection.MethodOptionsoptions_  // 0x38
  public    static  System.Int32                    ClientStreamingFieldNumber  // const
  private   static readonly System.Boolean                  ClientStreamingDefaultValue  // static @ 0x20
  private           System.Boolean                  clientStreaming_  // 0x40
  public    static  System.Int32                    ServerStreamingFieldNumber  // const
  private   static readonly System.Boolean                  ServerStreamingDefaultValue  // static @ 0x21
  private           System.Boolean                  serverStreaming_  // 0x41
PROPERTIES:
  Parser  get=0x09B1592C
  Descriptor  get=0x09B157D0
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B15C7C
  Name  get=0x09B15884  set=0x09B15D24
  HasName  get=0x09B00070
  InputType  get=0x09B15830  set=0x09B15CBC
  HasInputType  get=0x068C60A8
  OutputType  get=0x09B158D8  set=0x09B15D8C
  HasOutputType  get=0x09B11948
  Options  get=0x04D85A50  set=0x046A91B0
  ClientStreaming  get=0x09B1577C  set=0x04DA10B0
  HasClientStreaming  get=0x0314ECE0
  ServerStreaming  get=0x09B1597C  set=0x04DA10C0
  HasServerStreaming  get=0x09AFF0A8
METHODS:
  RVA=0x041E1670  token=0x6000822  System.Void .ctor()
  RVA=0x09B156D0  token=0x6000823  System.Void .ctor(Google.Protobuf.Reflection.MethodDescriptorProto other)
  RVA=0x09B15058  token=0x6000824  Google.Protobuf.Reflection.MethodDescriptorProto Clone()
  RVA=0x09AFF598  token=0x6000828  System.Void ClearName()
  RVA=0x09849400  token=0x600082C  System.Void ClearInputType()
  RVA=0x067B5A00  token=0x6000830  System.Void ClearOutputType()
  RVA=0x04D86DE0  token=0x6000836  System.Void ClearClientStreaming()
  RVA=0x04DA0DF0  token=0x600083A  System.Void ClearServerStreaming()
  RVA=0x09B151D0  token=0x600083B  System.Boolean Equals(System.Object other)
  RVA=0x09B150B8  token=0x600083C  System.Boolean Equals(Google.Protobuf.Reflection.MethodDescriptorProto other)
  RVA=0x09B15234  token=0x600083D  System.Int32 GetHashCode()
  RVA=0x09B154BC  token=0x600083E  System.String ToString()
  RVA=0x04D52400  token=0x600083F  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09B15B50  token=0x6000840  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09B14F40  token=0x6000841  System.Int32 CalculateSize()
  RVA=0x09B15374  token=0x6000842  System.Void MergeFrom(Google.Protobuf.Reflection.MethodDescriptorProto other)
  RVA=0x04D51840  token=0x6000843  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09B159D0  token=0x6000844  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09B15508  token=0x6000845  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.Reflection.FileOptions
TYPE:  sealed class
TOKEN: 0x20000C8
SIZE:  0x90
IMPLEMENTS: Google.Protobuf.IExtendableMessage`1 Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.FileOptions>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  private           Google.Protobuf.ExtensionSet<Google.Protobuf.Reflection.FileOptions>_extensions  // 0x18
  private           System.Int32                    _hasBits0  // 0x20
  public    static  System.Int32                    JavaPackageFieldNumber  // const
  private   static readonly System.String                   JavaPackageDefaultValue  // static @ 0x8
  private           System.String                   javaPackage_  // 0x28
  public    static  System.Int32                    JavaOuterClassnameFieldNumber  // const
  private   static readonly System.String                   JavaOuterClassnameDefaultValue  // static @ 0x10
  private           System.String                   javaOuterClassname_  // 0x30
  public    static  System.Int32                    JavaMultipleFilesFieldNumber  // const
  private   static readonly System.Boolean                  JavaMultipleFilesDefaultValue  // static @ 0x18
  private           System.Boolean                  javaMultipleFiles_  // 0x38
  public    static  System.Int32                    JavaGenerateEqualsAndHashFieldNumber  // const
  private   static readonly System.Boolean                  JavaGenerateEqualsAndHashDefaultValue  // static @ 0x19
  private           System.Boolean                  javaGenerateEqualsAndHash_  // 0x39
  public    static  System.Int32                    JavaStringCheckUtf8FieldNumber  // const
  private   static readonly System.Boolean                  JavaStringCheckUtf8DefaultValue  // static @ 0x1a
  private           System.Boolean                  javaStringCheckUtf8_  // 0x3a
  public    static  System.Int32                    OptimizeForFieldNumber  // const
  private   static readonly Google.Protobuf.Reflection.FileOptions.Types.OptimizeModeOptimizeForDefaultValue  // static @ 0x1c
  private           Google.Protobuf.Reflection.FileOptions.Types.OptimizeModeoptimizeFor_  // 0x3c
  public    static  System.Int32                    GoPackageFieldNumber  // const
  private   static readonly System.String                   GoPackageDefaultValue  // static @ 0x20
  private           System.String                   goPackage_  // 0x40
  public    static  System.Int32                    CcGenericServicesFieldNumber  // const
  private   static readonly System.Boolean                  CcGenericServicesDefaultValue  // static @ 0x28
  private           System.Boolean                  ccGenericServices_  // 0x48
  public    static  System.Int32                    JavaGenericServicesFieldNumber  // const
  private   static readonly System.Boolean                  JavaGenericServicesDefaultValue  // static @ 0x29
  private           System.Boolean                  javaGenericServices_  // 0x49
  public    static  System.Int32                    PyGenericServicesFieldNumber  // const
  private   static readonly System.Boolean                  PyGenericServicesDefaultValue  // static @ 0x2a
  private           System.Boolean                  pyGenericServices_  // 0x4a
  public    static  System.Int32                    PhpGenericServicesFieldNumber  // const
  private   static readonly System.Boolean                  PhpGenericServicesDefaultValue  // static @ 0x2b
  private           System.Boolean                  phpGenericServices_  // 0x4b
  public    static  System.Int32                    DeprecatedFieldNumber  // const
  private   static readonly System.Boolean                  DeprecatedDefaultValue  // static @ 0x2c
  private           System.Boolean                  deprecated_  // 0x4c
  public    static  System.Int32                    CcEnableArenasFieldNumber  // const
  private   static readonly System.Boolean                  CcEnableArenasDefaultValue  // static @ 0x2d
  private           System.Boolean                  ccEnableArenas_  // 0x4d
  public    static  System.Int32                    ObjcClassPrefixFieldNumber  // const
  private   static readonly System.String                   ObjcClassPrefixDefaultValue  // static @ 0x30
  private           System.String                   objcClassPrefix_  // 0x50
  public    static  System.Int32                    CsharpNamespaceFieldNumber  // const
  private   static readonly System.String                   CsharpNamespaceDefaultValue  // static @ 0x38
  private           System.String                   csharpNamespace_  // 0x58
  public    static  System.Int32                    SwiftPrefixFieldNumber  // const
  private   static readonly System.String                   SwiftPrefixDefaultValue  // static @ 0x40
  private           System.String                   swiftPrefix_  // 0x60
  public    static  System.Int32                    PhpClassPrefixFieldNumber  // const
  private   static readonly System.String                   PhpClassPrefixDefaultValue  // static @ 0x48
  private           System.String                   phpClassPrefix_  // 0x68
  public    static  System.Int32                    PhpNamespaceFieldNumber  // const
  private   static readonly System.String                   PhpNamespaceDefaultValue  // static @ 0x50
  private           System.String                   phpNamespace_  // 0x70
  public    static  System.Int32                    PhpMetadataNamespaceFieldNumber  // const
  private   static readonly System.String                   PhpMetadataNamespaceDefaultValue  // static @ 0x58
  private           System.String                   phpMetadataNamespace_  // 0x78
  public    static  System.Int32                    RubyPackageFieldNumber  // const
  private   static readonly System.String                   RubyPackageDefaultValue  // static @ 0x60
  private           System.String                   rubyPackage_  // 0x80
  public    static  System.Int32                    UninterpretedOptionFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.UninterpretedOption>_repeated_uninterpretedOption_codec  // static @ 0x68
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.UninterpretedOption>uninterpretedOption_  // 0x88
PROPERTIES:
  _Extensions  get=0x0385B100
  Parser  get=0x09B11C30
  Descriptor  get=0x09B11888
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B12718
  JavaPackage  get=0x09B11AE0  set=0x09B12890
  HasJavaPackage  get=0x068C60A8
  JavaOuterClassname  get=0x09B11A8C  set=0x09B12828
  HasJavaOuterClassname  get=0x09B11948
  JavaMultipleFiles  get=0x09B11A38  set=0x04DA1040
  HasJavaMultipleFiles  get=0x09B0C6D4
  JavaGenerateEqualsAndHash  get=0x09B11990  set=0x04DA1020
  HasJavaGenerateEqualsAndHash  get=0x09B0FC9C
  JavaStringCheckUtf8  get=0x09B11B34  set=0x04DA1050
  HasJavaStringCheckUtf8  get=0x09B11954
  OptimizeFor  get=0x09B11BDC  set=0x04DA1060
  HasOptimizeFor  get=0x09B0C6CC
  GoPackage  get=0x09B118E8  set=0x09B127C0
  HasGoPackage  get=0x09B00058
  CcGenericServices  get=0x09B1178C  set=0x04DA1000
  HasCcGenericServices  get=0x09B0FC7C
  JavaGenericServices  get=0x09B119E4  set=0x04DA1030
  HasJavaGenericServices  get=0x09B0FC8C
  PyGenericServices  get=0x09B11DD4  set=0x04DA1080
  HasPyGenericServices  get=0x09B0FC84
  PhpGenericServices  get=0x09B11CD4  set=0x04DA1070
  HasPhpGenericServices  get=0x0481D0E0
  Deprecated  get=0x09B11834  set=0x04DA1010
  HasDeprecated  get=0x09B0FC94
  CcEnableArenas  get=0x09B11734  set=0x04DA0FF0
  HasCcEnableArenas  get=0x09B1193C
  ObjcClassPrefix  get=0x09B11B88  set=0x09B128F8
  HasObjcClassPrefix  get=0x084774C8
  CsharpNamespace  get=0x09B117E0  set=0x09B12758
  HasCsharpNamespace  get=0x09B00064
  SwiftPrefix  get=0x09B11E80  set=0x09B12B04
  HasSwiftPrefix  get=0x09B11984
  PhpClassPrefix  get=0x09B11C80  set=0x09B12960
  HasPhpClassPrefix  get=0x09B11960
  PhpNamespace  get=0x09B11D80  set=0x09B12A30
  HasPhpNamespace  get=0x09B02DAC
  PhpMetadataNamespace  get=0x09B11D2C  set=0x09B129C8
  HasPhpMetadataNamespace  get=0x09B1196C
  RubyPackage  get=0x09B11E28  set=0x09B12A98
  HasRubyPackage  get=0x09B11978
  UninterpretedOption  get=0x04D861F0
METHODS:
  RVA=0x09B11504  token=0x600084D  System.Void .ctor()
  RVA=0x09B11580  token=0x600084E  System.Void .ctor(Google.Protobuf.Reflection.FileOptions other)
  RVA=0x09B10618  token=0x600084F  Google.Protobuf.Reflection.FileOptions Clone()
  RVA=0x09849400  token=0x6000853  System.Void ClearJavaPackage()
  RVA=0x067B5A00  token=0x6000857  System.Void ClearJavaOuterClassname()
  RVA=0x04DA0ED0  token=0x600085B  System.Void ClearJavaMultipleFiles()
  RVA=0x04DA0F40  token=0x600085F  System.Void ClearJavaGenerateEqualsAndHash()
  RVA=0x04DA0FD0  token=0x6000863  System.Void ClearJavaStringCheckUtf8()
  RVA=0x04DA0EC0  token=0x6000867  System.Void ClearOptimizeFor()
  RVA=0x09AFF570  token=0x600086B  System.Void ClearGoPackage()
  RVA=0x04DA0F00  token=0x600086F  System.Void ClearCcGenericServices()
  RVA=0x04DA0F20  token=0x6000873  System.Void ClearJavaGenericServices()
  RVA=0x04DA0F10  token=0x6000877  System.Void ClearPyGenericServices()
  RVA=0x04DA0FE0  token=0x600087B  System.Void ClearPhpGenericServices()
  RVA=0x04DA0F30  token=0x600087F  System.Void ClearDeprecated()
  RVA=0x04DA0FC0  token=0x6000883  System.Void ClearCcEnableArenas()
  RVA=0x0709F140  token=0x6000887  System.Void ClearObjcClassPrefix()
  RVA=0x09AFF584  token=0x600088B  System.Void ClearCsharpNamespace()
  RVA=0x06973C40  token=0x600088F  System.Void ClearSwiftPrefix()
  RVA=0x09B105DC  token=0x6000893  System.Void ClearPhpClassPrefix()
  RVA=0x09B01D64  token=0x6000897  System.Void ClearPhpNamespace()
  RVA=0x09B105F0  token=0x600089B  System.Void ClearPhpMetadataNamespace()
  RVA=0x09B10604  token=0x600089F  System.Void ClearRubyPackage()
  RVA=0x09B10678  token=0x60008A1  System.Boolean Equals(System.Object other)
  RVA=0x09B106DC  token=0x60008A2  System.Boolean Equals(Google.Protobuf.Reflection.FileOptions other)
  RVA=0x09B10A6C  token=0x60008A3  System.Int32 GetHashCode()
  RVA=0x09B110A4  token=0x60008A4  System.String ToString()
  RVA=0x04D52400  token=0x60008A5  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09B122B8  token=0x60008A6  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09B102A4  token=0x60008A7  System.Int32 CalculateSize()
  RVA=0x09B10DD4  token=0x60008A8  System.Void MergeFrom(Google.Protobuf.Reflection.FileOptions other)
  RVA=0x04D51840  token=0x60008A9  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09B11ED4  token=0x60008AA  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=-1  // generic def  token=0x60008AB  TValue GetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.FileOptions,TValue> extension)
  RVA=-1  // generic def  token=0x60008AC  Google.Protobuf.Collections.RepeatedField<TValue> GetExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.FileOptions,TValue> extension)
  RVA=-1  // generic def  token=0x60008AD  Google.Protobuf.Collections.RepeatedField<TValue> GetOrInitializeExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.FileOptions,TValue> extension)
  RVA=-1  // generic def  token=0x60008AE  System.Void SetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.FileOptions,TValue> extension, TValue value)
  RVA=-1  // generic def  token=0x60008AF  System.Boolean HasExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.FileOptions,TValue> extension)
  RVA=-1  // generic def  token=0x60008B0  System.Void ClearExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.FileOptions,TValue> extension)
  RVA=-1  // generic def  token=0x60008B1  System.Void ClearExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.FileOptions,TValue> extension)
  RVA=0x09B110F0  token=0x60008B2  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.Reflection.MessageOptions
TYPE:  sealed class
TOKEN: 0x20000CC
SIZE:  0x30
IMPLEMENTS: Google.Protobuf.IExtendableMessage`1 Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.MessageOptions>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  private           Google.Protobuf.ExtensionSet<Google.Protobuf.Reflection.MessageOptions>_extensions  // 0x18
  private           System.Int32                    _hasBits0  // 0x20
  public    static  System.Int32                    MessageSetWireFormatFieldNumber  // const
  private   static readonly System.Boolean                  MessageSetWireFormatDefaultValue  // static @ 0x8
  private           System.Boolean                  messageSetWireFormat_  // 0x24
  public    static  System.Int32                    NoStandardDescriptorAccessorFieldNumber  // const
  private   static readonly System.Boolean                  NoStandardDescriptorAccessorDefaultValue  // static @ 0x9
  private           System.Boolean                  noStandardDescriptorAccessor_  // 0x25
  public    static  System.Int32                    DeprecatedFieldNumber  // const
  private   static readonly System.Boolean                  DeprecatedDefaultValue  // static @ 0xa
  private           System.Boolean                  deprecated_  // 0x26
  public    static  System.Int32                    MapEntryFieldNumber  // const
  private   static readonly System.Boolean                  MapEntryDefaultValue  // static @ 0xb
  private           System.Boolean                  mapEntry_  // 0x27
  public    static  System.Int32                    UninterpretedOptionFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.UninterpretedOption>_repeated_uninterpretedOption_codec  // static @ 0x10
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.UninterpretedOption>uninterpretedOption_  // 0x28
PROPERTIES:
  _Extensions  get=0x0385B100
  Parser  get=0x09B14BD8
  Descriptor  get=0x09B14A7C
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B14F00
  MessageSetWireFormat  get=0x09B14B30  set=0x04DA0EE0
  HasMessageSetWireFormat  get=0x09B0C6CC
  NoStandardDescriptorAccessor  get=0x09B14B84  set=0x04DA0EF0
  HasNoStandardDescriptorAccessor  get=0x09B0C6D4
  Deprecated  get=0x09B14A28  set=0x04DA1090
  HasDeprecated  get=0x09B0FC7C
  MapEntry  get=0x09B14ADC  set=0x04DA10A0
  HasMapEntry  get=0x09B0FC8C
  UninterpretedOption  get=0x04D86240
METHODS:
  RVA=0x09B149B0  token=0x60008BA  System.Void .ctor()
  RVA=0x09B148E4  token=0x60008BB  System.Void .ctor(Google.Protobuf.Reflection.MessageOptions other)
  RVA=0x09B14324  token=0x60008BC  Google.Protobuf.Reflection.MessageOptions Clone()
  RVA=0x04DA0EC0  token=0x60008C0  System.Void ClearMessageSetWireFormat()
  RVA=0x04DA0ED0  token=0x60008C4  System.Void ClearNoStandardDescriptorAccessor()
  RVA=0x04DA0F00  token=0x60008C8  System.Void ClearDeprecated()
  RVA=0x04DA0F20  token=0x60008CC  System.Void ClearMapEntry()
  RVA=0x09B14384  token=0x60008CE  System.Boolean Equals(System.Object other)
  RVA=0x09B143E8  token=0x60008CF  System.Boolean Equals(Google.Protobuf.Reflection.MessageOptions other)
  RVA=0x09B144FC  token=0x60008D0  System.Int32 GetHashCode()
  RVA=0x09B14708  token=0x60008D1  System.String ToString()
  RVA=0x04D52400  token=0x60008D2  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09B14D90  token=0x60008D3  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09B1423C  token=0x60008D4  System.Int32 CalculateSize()
  RVA=0x09B14610  token=0x60008D5  System.Void MergeFrom(Google.Protobuf.Reflection.MessageOptions other)
  RVA=0x04D51840  token=0x60008D6  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09B14C28  token=0x60008D7  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=-1  // generic def  token=0x60008D8  TValue GetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.MessageOptions,TValue> extension)
  RVA=-1  // generic def  token=0x60008D9  Google.Protobuf.Collections.RepeatedField<TValue> GetExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.MessageOptions,TValue> extension)
  RVA=-1  // generic def  token=0x60008DA  Google.Protobuf.Collections.RepeatedField<TValue> GetOrInitializeExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.MessageOptions,TValue> extension)
  RVA=-1  // generic def  token=0x60008DB  System.Void SetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.MessageOptions,TValue> extension, TValue value)
  RVA=-1  // generic def  token=0x60008DC  System.Boolean HasExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.MessageOptions,TValue> extension)
  RVA=-1  // generic def  token=0x60008DD  System.Void ClearExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.MessageOptions,TValue> extension)
  RVA=-1  // generic def  token=0x60008DE  System.Void ClearExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.MessageOptions,TValue> extension)
  RVA=0x09B14754  token=0x60008DF  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.Reflection.FieldOptions
TYPE:  sealed class
TOKEN: 0x20000CE
SIZE:  0x40
IMPLEMENTS: Google.Protobuf.IExtendableMessage`1 Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.FieldOptions>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  private           Google.Protobuf.ExtensionSet<Google.Protobuf.Reflection.FieldOptions>_extensions  // 0x18
  private           System.Int32                    _hasBits0  // 0x20
  public    static  System.Int32                    CtypeFieldNumber  // const
  private   static readonly Google.Protobuf.Reflection.FieldOptions.Types.CTypeCtypeDefaultValue  // static @ 0x8
  private           Google.Protobuf.Reflection.FieldOptions.Types.CTypectype_  // 0x24
  public    static  System.Int32                    PackedFieldNumber  // const
  private   static readonly System.Boolean                  PackedDefaultValue  // static @ 0xc
  private           System.Boolean                  packed_  // 0x28
  public    static  System.Int32                    JstypeFieldNumber  // const
  private   static readonly Google.Protobuf.Reflection.FieldOptions.Types.JSTypeJstypeDefaultValue  // static @ 0x10
  private           Google.Protobuf.Reflection.FieldOptions.Types.JSTypejstype_  // 0x2c
  public    static  System.Int32                    LazyFieldNumber  // const
  private   static readonly System.Boolean                  LazyDefaultValue  // static @ 0x14
  private           System.Boolean                  lazy_  // 0x30
  public    static  System.Int32                    UnverifiedLazyFieldNumber  // const
  private   static readonly System.Boolean                  UnverifiedLazyDefaultValue  // static @ 0x15
  private           System.Boolean                  unverifiedLazy_  // 0x31
  public    static  System.Int32                    DeprecatedFieldNumber  // const
  private   static readonly System.Boolean                  DeprecatedDefaultValue  // static @ 0x16
  private           System.Boolean                  deprecated_  // 0x32
  public    static  System.Int32                    WeakFieldNumber  // const
  private   static readonly System.Boolean                  WeakDefaultValue  // static @ 0x17
  private           System.Boolean                  weak_  // 0x33
  public    static  System.Int32                    UninterpretedOptionFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.UninterpretedOption>_repeated_uninterpretedOption_codec  // static @ 0x18
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.UninterpretedOption>uninterpretedOption_  // 0x38
PROPERTIES:
  _Extensions  get=0x0385B100
  Parser  get=0x09B0FDA0
  Descriptor  get=0x09B0FC1C
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B10264
  Ctype  get=0x09B0FB74  set=0x04DA0F50
  HasCtype  get=0x09B0C6CC
  Packed  get=0x09B0FD4C  set=0x04DA0F90
  HasPacked  get=0x09B0C6D4
  Jstype  get=0x09B0FCA4  set=0x04DA0F70
  HasJstype  get=0x09B0FC84
  Lazy  get=0x09B0FCF8  set=0x04DA0F80
  HasLazy  get=0x09B0FC8C
  UnverifiedLazy  get=0x09B0FDF0  set=0x04DA0FA0
  HasUnverifiedLazy  get=0x09B0FC94
  Deprecated  get=0x09B0FBC8  set=0x04DA0F60
  HasDeprecated  get=0x09B0FC7C
  Weak  get=0x09B0FE44  set=0x04DA0FB0
  HasWeak  get=0x09B0FC9C
  UninterpretedOption  get=0x04D85A50
METHODS:
  RVA=0x09B0FAFC  token=0x60008E7  System.Void .ctor()
  RVA=0x09B0FA18  token=0x60008E8  System.Void .ctor(Google.Protobuf.Reflection.FieldOptions other)
  RVA=0x09B0F340  token=0x60008E9  Google.Protobuf.Reflection.FieldOptions Clone()
  RVA=0x04DA0EC0  token=0x60008ED  System.Void ClearCtype()
  RVA=0x04DA0ED0  token=0x60008F1  System.Void ClearPacked()
  RVA=0x04DA0F10  token=0x60008F5  System.Void ClearJstype()
  RVA=0x04DA0F20  token=0x60008F9  System.Void ClearLazy()
  RVA=0x04DA0F30  token=0x60008FD  System.Void ClearUnverifiedLazy()
  RVA=0x04DA0F00  token=0x6000901  System.Void ClearDeprecated()
  RVA=0x04DA0F40  token=0x6000905  System.Void ClearWeak()
  RVA=0x09B0F3A0  token=0x6000907  System.Boolean Equals(System.Object other)
  RVA=0x09B0F404  token=0x6000908  System.Boolean Equals(Google.Protobuf.Reflection.FieldOptions other)
  RVA=0x09B0F570  token=0x6000909  System.Int32 GetHashCode()
  RVA=0x09B0F808  token=0x600090A  System.String ToString()
  RVA=0x04D52400  token=0x600090B  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09B10074  token=0x600090C  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09B0F1D8  token=0x600090D  System.Int32 CalculateSize()
  RVA=0x09B0F6C8  token=0x600090E  System.Void MergeFrom(Google.Protobuf.Reflection.FieldOptions other)
  RVA=0x04D51840  token=0x600090F  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09B0FE98  token=0x6000910  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=-1  // generic def  token=0x6000911  TValue GetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.FieldOptions,TValue> extension)
  RVA=-1  // generic def  token=0x6000912  Google.Protobuf.Collections.RepeatedField<TValue> GetExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.FieldOptions,TValue> extension)
  RVA=-1  // generic def  token=0x6000913  Google.Protobuf.Collections.RepeatedField<TValue> GetOrInitializeExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.FieldOptions,TValue> extension)
  RVA=-1  // generic def  token=0x6000914  System.Void SetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.FieldOptions,TValue> extension, TValue value)
  RVA=-1  // generic def  token=0x6000915  System.Boolean HasExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.FieldOptions,TValue> extension)
  RVA=-1  // generic def  token=0x6000916  System.Void ClearExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.FieldOptions,TValue> extension)
  RVA=-1  // generic def  token=0x6000917  System.Void ClearExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.FieldOptions,TValue> extension)
  RVA=0x09B0F854  token=0x6000918  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.Reflection.OneofOptions
TYPE:  sealed class
TOKEN: 0x20000D3
SIZE:  0x28
IMPLEMENTS: Google.Protobuf.IExtendableMessage`1 Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.OneofOptions>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  private           Google.Protobuf.ExtensionSet<Google.Protobuf.Reflection.OneofOptions>_extensions  // 0x18
  public    static  System.Int32                    UninterpretedOptionFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.UninterpretedOption>_repeated_uninterpretedOption_codec  // static @ 0x8
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.UninterpretedOption>uninterpretedOption_  // 0x20
PROPERTIES:
  _Extensions  get=0x0385B100
  Parser  get=0x09B17728
  Descriptor  get=0x09B176C8
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B17940
  UninterpretedOption  get=0x04D862C0
METHODS:
  RVA=0x09B17650  token=0x6000920  System.Void .ctor()
  RVA=0x09B175A0  token=0x6000921  System.Void .ctor(Google.Protobuf.Reflection.OneofOptions other)
  RVA=0x09B171F8  token=0x6000922  Google.Protobuf.Reflection.OneofOptions Clone()
  RVA=0x09B172E8  token=0x6000924  System.Boolean Equals(System.Object other)
  RVA=0x09B17258  token=0x6000925  System.Boolean Equals(Google.Protobuf.Reflection.OneofOptions other)
  RVA=0x09AFE3F0  token=0x6000926  System.Int32 GetHashCode()
  RVA=0x09B173E4  token=0x6000927  System.String ToString()
  RVA=0x04D52400  token=0x6000928  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09B17878  token=0x6000929  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09B1713C  token=0x600092A  System.Int32 CalculateSize()
  RVA=0x09B1734C  token=0x600092B  System.Void MergeFrom(Google.Protobuf.Reflection.OneofOptions other)
  RVA=0x04D51840  token=0x600092C  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09B17778  token=0x600092D  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=-1  // generic def  token=0x600092E  TValue GetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.OneofOptions,TValue> extension)
  RVA=-1  // generic def  token=0x600092F  Google.Protobuf.Collections.RepeatedField<TValue> GetExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.OneofOptions,TValue> extension)
  RVA=-1  // generic def  token=0x6000930  Google.Protobuf.Collections.RepeatedField<TValue> GetOrInitializeExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.OneofOptions,TValue> extension)
  RVA=-1  // generic def  token=0x6000931  System.Void SetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.OneofOptions,TValue> extension, TValue value)
  RVA=-1  // generic def  token=0x6000932  System.Boolean HasExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.OneofOptions,TValue> extension)
  RVA=-1  // generic def  token=0x6000933  System.Void ClearExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.OneofOptions,TValue> extension)
  RVA=-1  // generic def  token=0x6000934  System.Void ClearExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.OneofOptions,TValue> extension)
  RVA=0x09B17430  token=0x6000935  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.Reflection.EnumOptions
TYPE:  sealed class
TOKEN: 0x20000D5
SIZE:  0x30
IMPLEMENTS: Google.Protobuf.IExtendableMessage`1 Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.EnumOptions>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  private           Google.Protobuf.ExtensionSet<Google.Protobuf.Reflection.EnumOptions>_extensions  // 0x18
  private           System.Int32                    _hasBits0  // 0x20
  public    static  System.Int32                    AllowAliasFieldNumber  // const
  private   static readonly System.Boolean                  AllowAliasDefaultValue  // static @ 0x8
  private           System.Boolean                  allowAlias_  // 0x24
  public    static  System.Int32                    DeprecatedFieldNumber  // const
  private   static readonly System.Boolean                  DeprecatedDefaultValue  // static @ 0x9
  private           System.Boolean                  deprecated_  // 0x25
  public    static  System.Int32                    UninterpretedOptionFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.UninterpretedOption>_repeated_uninterpretedOption_codec  // static @ 0x10
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.UninterpretedOption>uninterpretedOption_  // 0x28
PROPERTIES:
  _Extensions  get=0x0385B100
  Parser  get=0x09B0C6DC
  Descriptor  get=0x09B0C66C
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B0C97C
  AllowAlias  get=0x09B0C5C4  set=0x04DA0EE0
  HasAllowAlias  get=0x09B0C6CC
  Deprecated  get=0x09B0C618  set=0x04DA0EF0
  HasDeprecated  get=0x09B0C6D4
  UninterpretedOption  get=0x04D86240
METHODS:
  RVA=0x09B0C54C  token=0x600093D  System.Void .ctor()
  RVA=0x09B0C48C  token=0x600093E  System.Void .ctor(Google.Protobuf.Reflection.EnumOptions other)
  RVA=0x09B0BF80  token=0x600093F  Google.Protobuf.Reflection.EnumOptions Clone()
  RVA=0x04DA0EC0  token=0x6000943  System.Void ClearAllowAlias()
  RVA=0x04DA0ED0  token=0x6000947  System.Void ClearDeprecated()
  RVA=0x09B0BFE0  token=0x6000949  System.Boolean Equals(System.Object other)
  RVA=0x09B0C044  token=0x600094A  System.Boolean Equals(Google.Protobuf.Reflection.EnumOptions other)
  RVA=0x09B0C118  token=0x600094B  System.Int32 GetHashCode()
  RVA=0x09B0C2B0  token=0x600094C  System.String ToString()
  RVA=0x04D52400  token=0x600094D  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09B0C860  token=0x600094E  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09B0BEA8  token=0x600094F  System.Int32 CalculateSize()
  RVA=0x09B0C1E8  token=0x6000950  System.Void MergeFrom(Google.Protobuf.Reflection.EnumOptions other)
  RVA=0x04D51840  token=0x6000951  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09B0C72C  token=0x6000952  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=-1  // generic def  token=0x6000953  TValue GetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.EnumOptions,TValue> extension)
  RVA=-1  // generic def  token=0x6000954  Google.Protobuf.Collections.RepeatedField<TValue> GetExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.EnumOptions,TValue> extension)
  RVA=-1  // generic def  token=0x6000955  Google.Protobuf.Collections.RepeatedField<TValue> GetOrInitializeExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.EnumOptions,TValue> extension)
  RVA=-1  // generic def  token=0x6000956  System.Void SetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.EnumOptions,TValue> extension, TValue value)
  RVA=-1  // generic def  token=0x6000957  System.Boolean HasExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.EnumOptions,TValue> extension)
  RVA=-1  // generic def  token=0x6000958  System.Void ClearExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.EnumOptions,TValue> extension)
  RVA=-1  // generic def  token=0x6000959  System.Void ClearExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.EnumOptions,TValue> extension)
  RVA=0x09B0C2FC  token=0x600095A  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.Reflection.EnumValueOptions
TYPE:  sealed class
TOKEN: 0x20000D7
SIZE:  0x30
IMPLEMENTS: Google.Protobuf.IExtendableMessage`1 Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.EnumValueOptions>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  private           Google.Protobuf.ExtensionSet<Google.Protobuf.Reflection.EnumValueOptions>_extensions  // 0x18
  private           System.Int32                    _hasBits0  // 0x20
  public    static  System.Int32                    DeprecatedFieldNumber  // const
  private   static readonly System.Boolean                  DeprecatedDefaultValue  // static @ 0x8
  private           System.Boolean                  deprecated_  // 0x24
  public    static  System.Int32                    UninterpretedOptionFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.UninterpretedOption>_repeated_uninterpretedOption_codec  // static @ 0x10
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.UninterpretedOption>uninterpretedOption_  // 0x28
PROPERTIES:
  _Extensions  get=0x0385B100
  Parser  get=0x09B0E30C
  Descriptor  get=0x09B0E2AC
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B0E568
  Deprecated  get=0x09B0E258  set=0x04DA0EE0
  HasDeprecated  get=0x09B0C6CC
  UninterpretedOption  get=0x04D86240
METHODS:
  RVA=0x09B0E1E0  token=0x6000962  System.Void .ctor()
  RVA=0x09B0E124  token=0x6000963  System.Void .ctor(Google.Protobuf.Reflection.EnumValueOptions other)
  RVA=0x09B0DC80  token=0x6000964  Google.Protobuf.Reflection.EnumValueOptions Clone()
  RVA=0x04DA0EC0  token=0x6000968  System.Void ClearDeprecated()
  RVA=0x09B0DCE0  token=0x600096A  System.Boolean Equals(System.Object other)
  RVA=0x09B0DD44  token=0x600096B  System.Boolean Equals(Google.Protobuf.Reflection.EnumValueOptions other)
  RVA=0x09B0DDF8  token=0x600096C  System.Int32 GetHashCode()
  RVA=0x09B0DF54  token=0x600096D  System.String ToString()
  RVA=0x04D52400  token=0x600096E  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09B0E474  token=0x600096F  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09B0DBB4  token=0x6000970  System.Int32 CalculateSize()
  RVA=0x09B0DEA4  token=0x6000971  System.Void MergeFrom(Google.Protobuf.Reflection.EnumValueOptions other)
  RVA=0x04D51840  token=0x6000972  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09B0E35C  token=0x6000973  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=-1  // generic def  token=0x6000974  TValue GetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.EnumValueOptions,TValue> extension)
  RVA=-1  // generic def  token=0x6000975  Google.Protobuf.Collections.RepeatedField<TValue> GetExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.EnumValueOptions,TValue> extension)
  RVA=-1  // generic def  token=0x6000976  Google.Protobuf.Collections.RepeatedField<TValue> GetOrInitializeExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.EnumValueOptions,TValue> extension)
  RVA=-1  // generic def  token=0x6000977  System.Void SetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.EnumValueOptions,TValue> extension, TValue value)
  RVA=-1  // generic def  token=0x6000978  System.Boolean HasExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.EnumValueOptions,TValue> extension)
  RVA=-1  // generic def  token=0x6000979  System.Void ClearExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.EnumValueOptions,TValue> extension)
  RVA=-1  // generic def  token=0x600097A  System.Void ClearExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.EnumValueOptions,TValue> extension)
  RVA=0x09B0DFA0  token=0x600097B  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.Reflection.ServiceOptions
TYPE:  sealed class
TOKEN: 0x20000D9
SIZE:  0x30
IMPLEMENTS: Google.Protobuf.IExtendableMessage`1 Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.ServiceOptions>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  private           Google.Protobuf.ExtensionSet<Google.Protobuf.Reflection.ServiceOptions>_extensions  // 0x18
  private           System.Int32                    _hasBits0  // 0x20
  public    static  System.Int32                    DeprecatedFieldNumber  // const
  private   static readonly System.Boolean                  DeprecatedDefaultValue  // static @ 0x8
  private           System.Boolean                  deprecated_  // 0x24
  public    static  System.Int32                    UninterpretedOptionFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.UninterpretedOption>_repeated_uninterpretedOption_codec  // static @ 0x10
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.UninterpretedOption>uninterpretedOption_  // 0x28
PROPERTIES:
  _Extensions  get=0x0385B100
  Parser  get=0x09B18C20
  Descriptor  get=0x09B18BC0
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B18E84
  Deprecated  get=0x09B18B6C  set=0x04DA0EE0
  HasDeprecated  get=0x09B0C6CC
  UninterpretedOption  get=0x04D86240
METHODS:
  RVA=0x09B18A38  token=0x6000983  System.Void .ctor()
  RVA=0x09B18AB0  token=0x6000984  System.Void .ctor(Google.Protobuf.Reflection.ServiceOptions other)
  RVA=0x09B18594  token=0x6000985  Google.Protobuf.Reflection.ServiceOptions Clone()
  RVA=0x04DA0EC0  token=0x6000989  System.Void ClearDeprecated()
  RVA=0x09B186A8  token=0x600098B  System.Boolean Equals(System.Object other)
  RVA=0x09B185F4  token=0x600098C  System.Boolean Equals(Google.Protobuf.Reflection.ServiceOptions other)
  RVA=0x09B1870C  token=0x600098D  System.Int32 GetHashCode()
  RVA=0x09B18868  token=0x600098E  System.String ToString()
  RVA=0x04D52400  token=0x600098F  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09B18D8C  token=0x6000990  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09B184B8  token=0x6000991  System.Int32 CalculateSize()
  RVA=0x09B187B8  token=0x6000992  System.Void MergeFrom(Google.Protobuf.Reflection.ServiceOptions other)
  RVA=0x04D51840  token=0x6000993  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09B18C70  token=0x6000994  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=-1  // generic def  token=0x6000995  TValue GetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.ServiceOptions,TValue> extension)
  RVA=-1  // generic def  token=0x6000996  Google.Protobuf.Collections.RepeatedField<TValue> GetExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.ServiceOptions,TValue> extension)
  RVA=-1  // generic def  token=0x6000997  Google.Protobuf.Collections.RepeatedField<TValue> GetOrInitializeExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.ServiceOptions,TValue> extension)
  RVA=-1  // generic def  token=0x6000998  System.Void SetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.ServiceOptions,TValue> extension, TValue value)
  RVA=-1  // generic def  token=0x6000999  System.Boolean HasExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.ServiceOptions,TValue> extension)
  RVA=-1  // generic def  token=0x600099A  System.Void ClearExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.ServiceOptions,TValue> extension)
  RVA=-1  // generic def  token=0x600099B  System.Void ClearExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.ServiceOptions,TValue> extension)
  RVA=0x09B188B4  token=0x600099C  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.Reflection.MethodOptions
TYPE:  sealed class
TOKEN: 0x20000DB
SIZE:  0x38
IMPLEMENTS: Google.Protobuf.IExtendableMessage`1 Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.MethodOptions>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  private           Google.Protobuf.ExtensionSet<Google.Protobuf.Reflection.MethodOptions>_extensions  // 0x18
  private           System.Int32                    _hasBits0  // 0x20
  public    static  System.Int32                    DeprecatedFieldNumber  // const
  private   static readonly System.Boolean                  DeprecatedDefaultValue  // static @ 0x8
  private           System.Boolean                  deprecated_  // 0x24
  public    static  System.Int32                    IdempotencyLevelFieldNumber  // const
  private   static readonly Google.Protobuf.Reflection.MethodOptions.Types.IdempotencyLevelIdempotencyLevelDefaultValue  // static @ 0xc
  private           Google.Protobuf.Reflection.MethodOptions.Types.IdempotencyLevelidempotencyLevel_  // 0x28
  public    static  System.Int32                    UninterpretedOptionFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.UninterpretedOption>_repeated_uninterpretedOption_codec  // static @ 0x10
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.UninterpretedOption>uninterpretedOption_  // 0x30
PROPERTIES:
  _Extensions  get=0x0385B100
  Parser  get=0x09B16644
  Descriptor  get=0x09B16590
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B168F4
  Deprecated  get=0x09B1653C  set=0x04DA0EE0
  HasDeprecated  get=0x09B0C6CC
  IdempotencyLevel  get=0x09B165F0  set=0x04DA10D0
  HasIdempotencyLevel  get=0x09B0C6D4
  UninterpretedOption  get=0x011F36E0
METHODS:
  RVA=0x09B164C4  token=0x60009A4  System.Void .ctor()
  RVA=0x09B16404  token=0x60009A5  System.Void .ctor(Google.Protobuf.Reflection.MethodOptions other)
  RVA=0x09B15F08  token=0x60009A6  Google.Protobuf.Reflection.MethodOptions Clone()
  RVA=0x04DA0EC0  token=0x60009AA  System.Void ClearDeprecated()
  RVA=0x04DA0ED0  token=0x60009AE  System.Void ClearIdempotencyLevel()
  RVA=0x09B15F68  token=0x60009B0  System.Boolean Equals(System.Object other)
  RVA=0x09B15FCC  token=0x60009B1  System.Boolean Equals(Google.Protobuf.Reflection.MethodOptions other)
  RVA=0x09B160A0  token=0x60009B2  System.Int32 GetHashCode()
  RVA=0x09B16224  token=0x60009B3  System.String ToString()
  RVA=0x04D52400  token=0x60009B4  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09B167D0  token=0x60009B5  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09B15DF4  token=0x60009B6  System.Int32 CalculateSize()
  RVA=0x09B1615C  token=0x60009B7  System.Void MergeFrom(Google.Protobuf.Reflection.MethodOptions other)
  RVA=0x04D51840  token=0x60009B8  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09B16694  token=0x60009B9  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=-1  // generic def  token=0x60009BA  TValue GetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.MethodOptions,TValue> extension)
  RVA=-1  // generic def  token=0x60009BB  Google.Protobuf.Collections.RepeatedField<TValue> GetExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.MethodOptions,TValue> extension)
  RVA=-1  // generic def  token=0x60009BC  Google.Protobuf.Collections.RepeatedField<TValue> GetOrInitializeExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.MethodOptions,TValue> extension)
  RVA=-1  // generic def  token=0x60009BD  System.Void SetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.MethodOptions,TValue> extension, TValue value)
  RVA=-1  // generic def  token=0x60009BE  System.Boolean HasExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.MethodOptions,TValue> extension)
  RVA=-1  // generic def  token=0x60009BF  System.Void ClearExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.MethodOptions,TValue> extension)
  RVA=-1  // generic def  token=0x60009C0  System.Void ClearExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.MethodOptions,TValue> extension)
  RVA=0x09B16270  token=0x60009C1  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.Reflection.UninterpretedOption
TYPE:  sealed class
TOKEN: 0x20000DF
SIZE:  0x58
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.UninterpretedOption>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  private           System.Int32                    _hasBits0  // 0x18
  public    static  System.Int32                    NameFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.UninterpretedOption.Types.NamePart>_repeated_name_codec  // static @ 0x8
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.UninterpretedOption.Types.NamePart>name_  // 0x20
  public    static  System.Int32                    IdentifierValueFieldNumber  // const
  private   static readonly System.String                   IdentifierValueDefaultValue  // static @ 0x10
  private           System.String                   identifierValue_  // 0x28
  public    static  System.Int32                    PositiveIntValueFieldNumber  // const
  private   static readonly System.UInt64                   PositiveIntValueDefaultValue  // static @ 0x18
  private           System.UInt64                   positiveIntValue_  // 0x30
  public    static  System.Int32                    NegativeIntValueFieldNumber  // const
  private   static readonly System.Int64                    NegativeIntValueDefaultValue  // static @ 0x20
  private           System.Int64                    negativeIntValue_  // 0x38
  public    static  System.Int32                    DoubleValueFieldNumber  // const
  private   static readonly System.Double                   DoubleValueDefaultValue  // static @ 0x28
  private           System.Double                   doubleValue_  // 0x40
  public    static  System.Int32                    StringValueFieldNumber  // const
  private   static readonly Google.Protobuf.ByteString      StringValueDefaultValue  // static @ 0x30
  private           Google.Protobuf.ByteString      stringValue_  // 0x48
  public    static  System.Int32                    AggregateValueFieldNumber  // const
  private   static readonly System.String                   AggregateValueDefaultValue  // static @ 0x38
  private           System.String                   aggregateValue_  // 0x50
PROPERTIES:
  Parser  get=0x09B1B0C8
  Descriptor  get=0x09B1AF14
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B1B540
  Name  get=0x04D862C0
  IdentifierValue  get=0x09B1B01C  set=0x09B1B5E8
  HasIdentifierValue  get=0x068C60A8
  PositiveIntValue  get=0x09B1B118  set=0x04DA1110
  HasPositiveIntValue  get=0x0314ECE0
  NegativeIntValue  get=0x09B1B070  set=0x04DA1100
  HasNegativeIntValue  get=0x09AFF0A8
  DoubleValue  get=0x09B1AF74  set=0x04DA10F0
  HasDoubleValue  get=0x09B00098
  StringValue  get=0x09B1B170  set=0x09B1B650
  HasStringValue  get=0x09B1AFCC
  AggregateValue  get=0x09B1AEC0  set=0x09B1B580
  HasAggregateValue  get=0x084774C8
METHODS:
  RVA=0x09B1AE48  token=0x60009C8  System.Void .ctor()
  RVA=0x09B1AD68  token=0x60009C9  System.Void .ctor(Google.Protobuf.Reflection.UninterpretedOption other)
  RVA=0x09B1A588  token=0x60009CA  Google.Protobuf.Reflection.UninterpretedOption Clone()
  RVA=0x09849400  token=0x60009CF  System.Void ClearIdentifierValue()
  RVA=0x04D86DE0  token=0x60009D3  System.Void ClearPositiveIntValue()
  RVA=0x04DA0DF0  token=0x60009D7  System.Void ClearNegativeIntValue()
  RVA=0x04DA0E40  token=0x60009DB  System.Void ClearDoubleValue()
  RVA=0x070A7CA8  token=0x60009DF  System.Void ClearStringValue()
  RVA=0x0709F140  token=0x60009E3  System.Void ClearAggregateValue()
  RVA=0x09B1A5E8  token=0x60009E4  System.Boolean Equals(System.Object other)
  RVA=0x09B1A64C  token=0x60009E5  System.Boolean Equals(Google.Protobuf.Reflection.UninterpretedOption other)
  RVA=0x09B1A824  token=0x60009E6  System.Int32 GetHashCode()
  RVA=0x09B1AAC0  token=0x60009E7  System.String ToString()
  RVA=0x04D52400  token=0x60009E8  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09B1B38C  token=0x60009E9  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09B1A3D8  token=0x60009EA  System.Int32 CalculateSize()
  RVA=0x09B1A994  token=0x60009EB  System.Void MergeFrom(Google.Protobuf.Reflection.UninterpretedOption other)
  RVA=0x04D51840  token=0x60009EC  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09B1B1C4  token=0x60009ED  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09B1AB0C  token=0x60009EE  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.Reflection.SourceCodeInfo
TYPE:  sealed class
TOKEN: 0x20000E4
SIZE:  0x20
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.SourceCodeInfo>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    LocationFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.SourceCodeInfo.Types.Location>_repeated_location_codec  // static @ 0x8
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.SourceCodeInfo.Types.Location>location_  // 0x18
PROPERTIES:
  Parser  get=0x09B1942C
  Descriptor  get=0x09B193CC
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B195F0
  Location  get=0x0385B100
METHODS:
  RVA=0x09B192CC  token=0x6000A11  System.Void .ctor()
  RVA=0x09B19344  token=0x6000A12  System.Void .ctor(Google.Protobuf.Reflection.SourceCodeInfo other)
  RVA=0x09B18F58  token=0x6000A13  Google.Protobuf.Reflection.SourceCodeInfo Clone()
  RVA=0x09B19034  token=0x6000A15  System.Boolean Equals(System.Object other)
  RVA=0x09B18FB8  token=0x6000A16  System.Boolean Equals(Google.Protobuf.Reflection.SourceCodeInfo other)
  RVA=0x0638F5B8  token=0x6000A17  System.Int32 GetHashCode()
  RVA=0x09B19110  token=0x6000A18  System.String ToString()
  RVA=0x04D52400  token=0x6000A19  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09B1954C  token=0x6000A1A  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09B18EC4  token=0x6000A1B  System.Int32 CalculateSize()
  RVA=0x09B19098  token=0x6000A1C  System.Void MergeFrom(Google.Protobuf.Reflection.SourceCodeInfo other)
  RVA=0x04D51840  token=0x6000A1D  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09B1947C  token=0x6000A1E  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09B1915C  token=0x6000A1F  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.Reflection.GeneratedCodeInfo
TYPE:  sealed class
TOKEN: 0x20000E9
SIZE:  0x20
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.GeneratedCodeInfo>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    AnnotationFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.GeneratedCodeInfo.Types.Annotation>_repeated_annotation_codec  // static @ 0x8
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.GeneratedCodeInfo.Types.Annotation>annotation_  // 0x18
PROPERTIES:
  Parser  get=0x09B130D4
  Descriptor  get=0x09B13074
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B13298
  Annotation  get=0x0385B100
METHODS:
  RVA=0x09B12FFC  token=0x6000A45  System.Void .ctor()
  RVA=0x09B12F74  token=0x6000A46  System.Void .ctor(Google.Protobuf.Reflection.GeneratedCodeInfo other)
  RVA=0x09B12C00  token=0x6000A47  Google.Protobuf.Reflection.GeneratedCodeInfo Clone()
  RVA=0x09B12C60  token=0x6000A49  System.Boolean Equals(System.Object other)
  RVA=0x09B12CC4  token=0x6000A4A  System.Boolean Equals(Google.Protobuf.Reflection.GeneratedCodeInfo other)
  RVA=0x0638F5B8  token=0x6000A4B  System.Int32 GetHashCode()
  RVA=0x09B12DB8  token=0x6000A4C  System.String ToString()
  RVA=0x04D52400  token=0x6000A4D  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09B131F4  token=0x6000A4E  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09B12B6C  token=0x6000A4F  System.Int32 CalculateSize()
  RVA=0x09B12D40  token=0x6000A50  System.Void MergeFrom(Google.Protobuf.Reflection.GeneratedCodeInfo other)
  RVA=0x04D51840  token=0x6000A51  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09B13124  token=0x6000A52  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09B12E04  token=0x6000A53  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.Reflection.DescriptorBase
TYPE:  abstract class
TOKEN: 0x20000EE
SIZE:  0x28
IMPLEMENTS: Google.Protobuf.Reflection.IDescriptor
FIELDS:
  private   readonly System.Int32                    <Index>k__BackingField  // 0x10
  private   readonly System.String                   <FullName>k__BackingField  // 0x18
  private   readonly Google.Protobuf.Reflection.FileDescriptor<File>k__BackingField  // 0x20
PROPERTIES:
  Index  get=0x02B2E2D0
  Name  get=-1  // abstract
  FullName  get=0x0385B100
  File  get=0x04D862C0
  Declaration  get=0x09B0A2D0
METHODS:
  RVA=0x09B0A2A0  token=0x6000A78  System.Void .ctor(Google.Protobuf.Reflection.FileDescriptor file, System.String fullName, System.Int32 index)
  RVA=0x011EC580  token=0x6000A7E  System.Collections.Generic.IReadOnlyList<Google.Protobuf.Reflection.DescriptorBase> GetNestedDescriptorListForField(System.Int32 fieldNumber)
END_CLASS

CLASS: Google.Protobuf.Reflection.DescriptorDeclaration
TYPE:  sealed class
TOKEN: 0x20000EF
SIZE:  0x40
FIELDS:
  private   readonly Google.Protobuf.Reflection.IDescriptor<Descriptor>k__BackingField  // 0x10
  private   readonly System.Int32                    <StartLine>k__BackingField  // 0x18
  private   readonly System.Int32                    <StartColumn>k__BackingField  // 0x1c
  private   readonly System.Int32                    <EndLine>k__BackingField  // 0x20
  private   readonly System.Int32                    <EndColumn>k__BackingField  // 0x24
  private   readonly System.String                   <LeadingComments>k__BackingField  // 0x28
  private   readonly System.String                   <TrailingComments>k__BackingField  // 0x30
  private   readonly System.Collections.Generic.IReadOnlyList<System.String><LeadingDetachedComments>k__BackingField  // 0x38
PROPERTIES:
  Descriptor  get=0x02B2ECC0
  StartLine  get=0x011EF5B0
  StartColumn  get=0x04D86310
  EndLine  get=0x02B70F90
  EndColumn  get=0x011F0020
  LeadingComments  get=0x04D86240
  TrailingComments  get=0x011F36E0
  LeadingDetachedComments  get=0x04D85A50
METHODS:
  RVA=0x09B0A368  token=0x6000A87  System.Void .ctor(Google.Protobuf.Reflection.IDescriptor descriptor, Google.Protobuf.Reflection.SourceCodeInfo.Types.Location location)
  RVA=0x09B0A2F8  token=0x6000A88  Google.Protobuf.Reflection.DescriptorDeclaration FromProto(Google.Protobuf.Reflection.IDescriptor descriptor, Google.Protobuf.Reflection.SourceCodeInfo.Types.Location location)
END_CLASS

CLASS: Google.Protobuf.Reflection.DescriptorPool
TYPE:  sealed class
TOKEN: 0x20000F0
SIZE:  0x30
FIELDS:
  private   readonly System.Collections.Generic.IDictionary<System.String,Google.Protobuf.Reflection.IDescriptor>descriptorsByName  // 0x10
  private   readonly System.Collections.Generic.IDictionary<Google.Protobuf.ObjectIntPair<Google.Protobuf.Reflection.IDescriptor>,Google.Protobuf.Reflection.FieldDescriptor>fieldsByNumber  // 0x18
  private   readonly System.Collections.Generic.IDictionary<Google.Protobuf.ObjectIntPair<Google.Protobuf.Reflection.IDescriptor>,Google.Protobuf.Reflection.EnumValueDescriptor>enumValuesByNumber  // 0x20
  private   readonly System.Collections.Generic.HashSet<Google.Protobuf.Reflection.FileDescriptor>dependencies  // 0x28
  private   static readonly System.Text.RegularExpressions.RegexValidationRegex  // static @ 0x0
METHODS:
  RVA=0x09B0B538  token=0x6000A89  System.Void .ctor(System.Collections.Generic.IEnumerable<Google.Protobuf.Reflection.FileDescriptor> dependencyFiles)
  RVA=0x09B0AEB0  token=0x6000A8A  System.Void ImportPublicDependencies(Google.Protobuf.Reflection.FileDescriptor file)
  RVA=-1  // generic def  token=0x6000A8B  T FindSymbol(System.String fullName)
  RVA=0x09B0A804  token=0x6000A8C  System.Void AddPackage(System.String fullName, Google.Protobuf.Reflection.FileDescriptor file)
  RVA=0x09B0AA6C  token=0x6000A8D  System.Void AddSymbol(Google.Protobuf.Reflection.IDescriptor descriptor)
  RVA=0x09B0B2E4  token=0x6000A8E  System.Void ValidateSymbolName(Google.Protobuf.Reflection.IDescriptor descriptor)
  RVA=0x09B0AE0C  token=0x6000A8F  Google.Protobuf.Reflection.FieldDescriptor FindFieldByNumber(Google.Protobuf.Reflection.MessageDescriptor messageDescriptor, System.Int32 number)
  RVA=0x09B0AD68  token=0x6000A90  Google.Protobuf.Reflection.EnumValueDescriptor FindEnumValueByNumber(Google.Protobuf.Reflection.EnumDescriptor enumDescriptor, System.Int32 number)
  RVA=0x09B0A5CC  token=0x6000A91  System.Void AddFieldByNumber(Google.Protobuf.Reflection.FieldDescriptor field)
  RVA=0x09B0A508  token=0x6000A92  System.Void AddEnumValueByNumber(Google.Protobuf.Reflection.EnumValueDescriptor enumValue)
  RVA=0x09B0B024  token=0x6000A93  Google.Protobuf.Reflection.IDescriptor LookupSymbol(System.String name, Google.Protobuf.Reflection.IDescriptor relativeTo)
  RVA=0x09B0B474  token=0x6000A94  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.Reflection.DescriptorUtil
TYPE:  static class
TOKEN: 0x20000F1
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000A95  System.Collections.Generic.IList<TOutput> ConvertAndMakeReadOnly(System.Collections.Generic.IList<TInput> input, Google.Protobuf.Reflection.DescriptorUtil.IndexedConverter<TInput,TOutput> converter)
END_CLASS

CLASS: Google.Protobuf.Reflection.DescriptorValidationException
TYPE:  sealed class
TOKEN: 0x20000F3
SIZE:  0xA0
EXTENDS: System.Exception
FIELDS:
  private   readonly System.String                   name  // 0x90
  private   readonly System.String                   description  // 0x98
PROPERTIES:
  ProblemSymbolName  get=0x04D86220
  Description  get=0x04D85EE0
METHODS:
  RVA=0x09B0B8E8  token=0x6000A9C  System.Void .ctor(Google.Protobuf.Reflection.IDescriptor problemDescriptor, System.String description)
  RVA=0x09B0B9B4  token=0x6000A9D  System.Void .ctor(Google.Protobuf.Reflection.IDescriptor problemDescriptor, System.String description, System.Exception cause)
END_CLASS

CLASS: Google.Protobuf.Reflection.EnumDescriptor
TYPE:  sealed class
TOKEN: 0x20000F4
SIZE:  0x48
EXTENDS: Google.Protobuf.Reflection.DescriptorBase
FIELDS:
  private   readonly Google.Protobuf.Reflection.EnumDescriptorProtoproto  // 0x28
  private   readonly Google.Protobuf.Reflection.MessageDescriptorcontainingType  // 0x30
  private   readonly System.Collections.Generic.IList<Google.Protobuf.Reflection.EnumValueDescriptor>values  // 0x38
  private   readonly System.Type                     clrType  // 0x40
PROPERTIES:
  Proto  get=0x04D86240
  Name  get=0x09B0BE88
  ClrType  get=0x04D85A60
  ContainingType  get=0x011F36E0
  Values  get=0x04D85A50
  CustomOptions  get=0x09B0BDF8
METHODS:
  RVA=0x09B0BBEC  token=0x6000A9E  System.Void .ctor(Google.Protobuf.Reflection.EnumDescriptorProto proto, Google.Protobuf.Reflection.FileDescriptor file, Google.Protobuf.Reflection.MessageDescriptor parent, System.Int32 index, System.Type clrType)
  RVA=0x09B0BBCC  token=0x6000AA0  Google.Protobuf.Reflection.EnumDescriptorProto ToProto()
  RVA=0x09B0BB50  token=0x6000AA2  System.Collections.Generic.IReadOnlyList<Google.Protobuf.Reflection.DescriptorBase> GetNestedDescriptorListForField(System.Int32 fieldNumber)
  RVA=0x09B0BB20  token=0x6000AA6  Google.Protobuf.Reflection.EnumValueDescriptor FindValueByNumber(System.Int32 number)
  RVA=0x09B0BA94  token=0x6000AA7  Google.Protobuf.Reflection.EnumValueDescriptor FindValueByName(System.String name)
  RVA=0x09B0BBA0  token=0x6000AA9  Google.Protobuf.Reflection.EnumOptions GetOptions()
  RVA=-1  // generic def  token=0x6000AAA  T GetOption(Google.Protobuf.Extension<Google.Protobuf.Reflection.EnumOptions,T> extension)
  RVA=-1  // generic def  token=0x6000AAB  Google.Protobuf.Collections.RepeatedField<T> GetOption(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.EnumOptions,T> extension)
END_CLASS

CLASS: Google.Protobuf.Reflection.EnumValueDescriptor
TYPE:  sealed class
TOKEN: 0x20000F6
SIZE:  0x38
EXTENDS: Google.Protobuf.Reflection.DescriptorBase
FIELDS:
  private   readonly Google.Protobuf.Reflection.EnumDescriptorenumDescriptor  // 0x28
  private   readonly Google.Protobuf.Reflection.EnumValueDescriptorProtoproto  // 0x30
PROPERTIES:
  Proto  get=0x011F36E0
  Name  get=0x09B0DB74
  Number  get=0x09B0DB94
  EnumDescriptor  get=0x04D86240
  CustomOptions  get=0x09B0DAE4
METHODS:
  RVA=0x09B0D9EC  token=0x6000AAE  System.Void .ctor(Google.Protobuf.Reflection.EnumValueDescriptorProto proto, Google.Protobuf.Reflection.FileDescriptor file, Google.Protobuf.Reflection.EnumDescriptor parent, System.Int32 index)
  RVA=0x09B0D9CC  token=0x6000AB0  Google.Protobuf.Reflection.EnumValueDescriptorProto ToProto()
  RVA=0x09B0D9A0  token=0x6000AB5  Google.Protobuf.Reflection.EnumValueOptions GetOptions()
  RVA=-1  // generic def  token=0x6000AB6  T GetOption(Google.Protobuf.Extension<Google.Protobuf.Reflection.EnumValueOptions,T> extension)
  RVA=-1  // generic def  token=0x6000AB7  Google.Protobuf.Collections.RepeatedField<T> GetOption(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.EnumValueOptions,T> extension)
END_CLASS

CLASS: Google.Protobuf.Reflection.ExtensionAccessor
TYPE:  sealed class
TOKEN: 0x20000F7
SIZE:  0x28
IMPLEMENTS: Google.Protobuf.Reflection.IFieldAccessor
FIELDS:
  private   readonly Google.Protobuf.Extension       extension  // 0x10
  private   readonly Google.Protobuf.Reflection.ReflectionUtil.IExtensionReflectionHelperhelper  // 0x18
  private   readonly Google.Protobuf.Reflection.FieldDescriptor<Descriptor>k__BackingField  // 0x20
PROPERTIES:
  Descriptor  get=0x04D862C0
METHODS:
  RVA=0x09B0E770  token=0x6000AB8  System.Void .ctor(Google.Protobuf.Reflection.FieldDescriptor descriptor)
  RVA=0x09B0E5A8  token=0x6000ABA  System.Void Clear(Google.Protobuf.IMessage message)
  RVA=0x09B0E680  token=0x6000ABB  System.Boolean HasValue(Google.Protobuf.IMessage message)
  RVA=0x09B0E614  token=0x6000ABC  System.Object GetValue(Google.Protobuf.IMessage message)
  RVA=0x09B0E6EC  token=0x6000ABD  System.Void SetValue(Google.Protobuf.IMessage message, System.Object value)
END_CLASS

CLASS: Google.Protobuf.Reflection.ExtensionCollection
TYPE:  sealed class
TOKEN: 0x20000F8
SIZE:  0x28
FIELDS:
  private           System.Collections.Generic.IDictionary<Google.Protobuf.Reflection.MessageDescriptor,System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor>>extensionsByTypeInDeclarationOrder  // 0x10
  private           System.Collections.Generic.IDictionary<Google.Protobuf.Reflection.MessageDescriptor,System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor>>extensionsByTypeInNumberOrder  // 0x18
  private   readonly System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor><UnorderedExtensions>k__BackingField  // 0x20
PROPERTIES:
  UnorderedExtensions  get=0x04D862C0
METHODS:
  RVA=0x09B0F0D0  token=0x6000ABE  System.Void .ctor(Google.Protobuf.Reflection.FileDescriptor file, Google.Protobuf.Extension[] extensions)
  RVA=0x09B0EFC8  token=0x6000ABF  System.Void .ctor(Google.Protobuf.Reflection.MessageDescriptor message, Google.Protobuf.Extension[] extensions)
  RVA=0x09B0EF28  token=0x6000AC1  System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor> GetExtensionsInDeclarationOrder(Google.Protobuf.Reflection.MessageDescriptor descriptor)
  RVA=0x09B0EF78  token=0x6000AC2  System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor> GetExtensionsInNumberOrder(Google.Protobuf.Reflection.MessageDescriptor descriptor)
  RVA=0x09B0E7F8  token=0x6000AC3  System.Void CrossLink()
END_CLASS

CLASS: Google.Protobuf.Reflection.FieldAccessorBase
TYPE:  abstract class
TOKEN: 0x20000FC
SIZE:  0x20
IMPLEMENTS: Google.Protobuf.Reflection.IFieldAccessor
FIELDS:
  private   readonly System.Func<Google.Protobuf.IMessage,System.Object>getValueDelegate  // 0x10
  private   readonly Google.Protobuf.Reflection.FieldDescriptordescriptor  // 0x18
PROPERTIES:
  Descriptor  get=0x0385B100
METHODS:
  RVA=0x09B1C07C  token=0x6000ACF  System.Void .ctor(System.Reflection.PropertyInfo property, Google.Protobuf.Reflection.FieldDescriptor descriptor)
  RVA=0x09B1C05C  token=0x6000AD1  System.Object GetValue(Google.Protobuf.IMessage message)
  RVA=-1  // abstract  token=0x6000AD2  System.Boolean HasValue(Google.Protobuf.IMessage message)
  RVA=-1  // abstract  token=0x6000AD3  System.Void Clear(Google.Protobuf.IMessage message)
  RVA=-1  // abstract  token=0x6000AD4  System.Void SetValue(Google.Protobuf.IMessage message, System.Object value)
END_CLASS

CLASS: Google.Protobuf.Reflection.FieldDescriptor
TYPE:  sealed class
TOKEN: 0x20000FD
SIZE:  0x80
EXTENDS: Google.Protobuf.Reflection.DescriptorBase
IMPLEMENTS: System.IComparable`1
FIELDS:
  private           Google.Protobuf.Reflection.EnumDescriptorenumType  // 0x28
  private           Google.Protobuf.Reflection.MessageDescriptorextendeeType  // 0x30
  private           Google.Protobuf.Reflection.MessageDescriptormessageType  // 0x38
  private           Google.Protobuf.Reflection.FieldTypefieldType  // 0x40
  private           Google.Protobuf.Reflection.IFieldAccessoraccessor  // 0x48
  private   readonly Google.Protobuf.Reflection.MessageDescriptor<ContainingType>k__BackingField  // 0x50
  private   readonly Google.Protobuf.Reflection.OneofDescriptor<ContainingOneof>k__BackingField  // 0x58
  private   readonly System.String                   <JsonName>k__BackingField  // 0x60
  private   readonly System.String                   <PropertyName>k__BackingField  // 0x68
  private   readonly Google.Protobuf.Reflection.FieldDescriptorProto<Proto>k__BackingField  // 0x70
  private   readonly Google.Protobuf.Extension       <Extension>k__BackingField  // 0x78
PROPERTIES:
  ContainingType  get=0x04D86270
  ContainingOneof  get=0x02E56440
  RealContainingOneof  get=0x09B1D2A0
  JsonName  get=0x04D86280
  PropertyName  get=0x04D86230
  HasPresence  get=0x09B1D098
  Proto  get=0x04D862B0
  Extension  get=0x04D862A0
  Name  get=0x09B1D280
  Accessor  get=0x04D86200
  IsRepeated  get=0x09B1D1C0
  IsRequired  get=0x09B1D1E8
  IsMap  get=0x09B1D124
  IsPacked  get=0x09B1D164
  IsExtension  get=0x09B1D104
  FieldType  get=0x04D865E0
  FieldNumber  get=0x09B1D078
  EnumType  get=0x09B1CF98
  MessageType  get=0x09B1D210
  ExtendeeType  get=0x09B1D004
  CustomOptions  get=0x09B1CF08
METHODS:
  RVA=0x09B1CBD0  token=0x6000ADC  Google.Protobuf.Reflection.FieldDescriptorProto ToProto()
  RVA=0x09B1CBF0  token=0x6000ADE  System.Void .ctor(Google.Protobuf.Reflection.FieldDescriptorProto proto, Google.Protobuf.Reflection.FileDescriptor file, Google.Protobuf.Reflection.MessageDescriptor parent, System.Int32 index, System.String propertyName, Google.Protobuf.Extension extension)
  RVA=0x09B1CA3C  token=0x6000AE1  Google.Protobuf.Reflection.FieldType GetFieldTypeFromProtoType(Google.Protobuf.Reflection.FieldDescriptorProto.Types.Type type)
  RVA=0x09B1C230  token=0x6000AE9  System.Int32 CompareTo(Google.Protobuf.Reflection.FieldDescriptor other)
  RVA=0x09B1CBA4  token=0x6000AEE  Google.Protobuf.Reflection.FieldOptions GetOptions()
  RVA=-1  // generic def  token=0x6000AEF  T GetOption(Google.Protobuf.Extension<Google.Protobuf.Reflection.FieldOptions,T> extension)
  RVA=-1  // generic def  token=0x6000AF0  Google.Protobuf.Collections.RepeatedField<T> GetOption(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.FieldOptions,T> extension)
  RVA=0x09B1C4C8  token=0x6000AF1  System.Void CrossLink()
  RVA=0x09B1C2C4  token=0x6000AF2  Google.Protobuf.Reflection.IFieldAccessor CreateAccessor()
END_CLASS

CLASS: Google.Protobuf.Reflection.FieldType
TYPE:  sealed struct
TOKEN: 0x20000FE
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Google.Protobuf.Reflection.FieldTypeDouble  // const
  public    static  Google.Protobuf.Reflection.FieldTypeFloat  // const
  public    static  Google.Protobuf.Reflection.FieldTypeInt64  // const
  public    static  Google.Protobuf.Reflection.FieldTypeUInt64  // const
  public    static  Google.Protobuf.Reflection.FieldTypeInt32  // const
  public    static  Google.Protobuf.Reflection.FieldTypeFixed64  // const
  public    static  Google.Protobuf.Reflection.FieldTypeFixed32  // const
  public    static  Google.Protobuf.Reflection.FieldTypeBool  // const
  public    static  Google.Protobuf.Reflection.FieldTypeString  // const
  public    static  Google.Protobuf.Reflection.FieldTypeGroup  // const
  public    static  Google.Protobuf.Reflection.FieldTypeMessage  // const
  public    static  Google.Protobuf.Reflection.FieldTypeBytes  // const
  public    static  Google.Protobuf.Reflection.FieldTypeUInt32  // const
  public    static  Google.Protobuf.Reflection.FieldTypeSFixed32  // const
  public    static  Google.Protobuf.Reflection.FieldTypeSFixed64  // const
  public    static  Google.Protobuf.Reflection.FieldTypeSInt32  // const
  public    static  Google.Protobuf.Reflection.FieldTypeSInt64  // const
  public    static  Google.Protobuf.Reflection.FieldTypeEnum  // const
METHODS:
END_CLASS

CLASS: Google.Protobuf.Reflection.Syntax
TYPE:  sealed struct
TOKEN: 0x20000FF
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Google.Protobuf.Reflection.SyntaxProto2  // const
  public    static  Google.Protobuf.Reflection.SyntaxProto3  // const
  public    static  Google.Protobuf.Reflection.SyntaxUnknown  // const
METHODS:
END_CLASS

CLASS: Google.Protobuf.Reflection.FileDescriptor
TYPE:  sealed class
TOKEN: 0x2000100
SIZE:  0x68
IMPLEMENTS: Google.Protobuf.Reflection.IDescriptor
FIELDS:
  private   readonly System.Lazy<System.Collections.Generic.Dictionary<Google.Protobuf.Reflection.IDescriptor,Google.Protobuf.Reflection.DescriptorDeclaration>>declarations  // 0x10
  private   readonly Google.Protobuf.Reflection.FileDescriptorProto<Proto>k__BackingField  // 0x18
  private   readonly Google.Protobuf.Reflection.Syntax<Syntax>k__BackingField  // 0x20
  private   readonly System.Collections.Generic.IList<Google.Protobuf.Reflection.MessageDescriptor><MessageTypes>k__BackingField  // 0x28
  private   readonly System.Collections.Generic.IList<Google.Protobuf.Reflection.EnumDescriptor><EnumTypes>k__BackingField  // 0x30
  private   readonly System.Collections.Generic.IList<Google.Protobuf.Reflection.ServiceDescriptor><Services>k__BackingField  // 0x38
  private   readonly Google.Protobuf.Reflection.ExtensionCollection<Extensions>k__BackingField  // 0x40
  private   readonly System.Collections.Generic.IList<Google.Protobuf.Reflection.FileDescriptor><Dependencies>k__BackingField  // 0x48
  private   readonly System.Collections.Generic.IList<Google.Protobuf.Reflection.FileDescriptor><PublicDependencies>k__BackingField  // 0x50
  private   readonly Google.Protobuf.ByteString      <SerializedData>k__BackingField  // 0x58
  private   readonly Google.Protobuf.Reflection.DescriptorPool<DescriptorPool>k__BackingField  // 0x60
PROPERTIES:
  Proto  get=0x0385B100
  Syntax  get=0x02B70F90
  Name  get=0x09B1F874
  Package  get=0x09B1F894
  MessageTypes  get=0x04D86240
  EnumTypes  get=0x011F36E0
  Services  get=0x04D85A50
  Extensions  get=0x04D85A60
  Dependencies  get=0x04D86200
  PublicDependencies  get=0x04D86270
  SerializedData  get=0x02E56440
  Google.Protobuf.Reflection.IDescriptor.FullName  get=0x09B1F248
  Google.Protobuf.Reflection.IDescriptor.File  get=0x0464E630
  DescriptorPool  get=0x04D86280
  DescriptorProtoFileDescriptor  get=0x09B1F834
  CustomOptions  get=0x09B1F7A4
METHODS:
  RVA=0x09B1F2B8  token=0x6000AF3  System.Void .cctor()
  RVA=0x09B1F348  token=0x6000AF4  System.Void .ctor(Google.Protobuf.ByteString descriptorData, Google.Protobuf.Reflection.FileDescriptorProto proto, System.Collections.Generic.IEnumerable<Google.Protobuf.Reflection.FileDescriptor> dependencies, Google.Protobuf.Reflection.DescriptorPool pool, System.Boolean allowUnknownDependencies, Google.Protobuf.Reflection.GeneratedClrTypeInfo generatedCodeInfo)
  RVA=0x09B1DBD8  token=0x6000AF5  System.Collections.Generic.Dictionary<Google.Protobuf.Reflection.IDescriptor,Google.Protobuf.Reflection.DescriptorDeclaration> CreateDeclarationMap()
  RVA=0x09B1E390  token=0x6000AF6  Google.Protobuf.Reflection.IDescriptor FindDescriptorForPath(System.Collections.Generic.IList<System.Int32> path)
  RVA=0x09B1F0F0  token=0x6000AF7  Google.Protobuf.Reflection.DescriptorBase GetDescriptorFromList(System.Collections.Generic.IReadOnlyList<Google.Protobuf.Reflection.DescriptorBase> list, System.Int32 index)
  RVA=0x09B1F1B4  token=0x6000AF8  System.Collections.Generic.IReadOnlyList<Google.Protobuf.Reflection.DescriptorBase> GetNestedDescriptorListForField(System.Int32 fieldNumber)
  RVA=0x09B1F068  token=0x6000AF9  Google.Protobuf.Reflection.DescriptorDeclaration GetDeclaration(Google.Protobuf.Reflection.IDescriptor descriptor)
  RVA=0x09B1DB48  token=0x6000AFA  System.String ComputeFullName(Google.Protobuf.Reflection.MessageDescriptor parent, System.String name)
  RVA=0x09B1E03C  token=0x6000AFB  System.Collections.Generic.IList<Google.Protobuf.Reflection.FileDescriptor> DeterminePublicDependencies(Google.Protobuf.Reflection.FileDescriptor this, Google.Protobuf.Reflection.FileDescriptorProto proto, System.Collections.Generic.IEnumerable<Google.Protobuf.Reflection.FileDescriptor> dependencies, System.Boolean allowUnknownDependencies)
  RVA=0x09B1F250  token=0x6000AFD  Google.Protobuf.Reflection.FileDescriptorProto ToProto()
  RVA=-1  // generic def  token=0x6000B0B  T FindTypeByName(System.String name)
  RVA=0x09B1D9C0  token=0x6000B0C  Google.Protobuf.Reflection.FileDescriptor BuildFrom(Google.Protobuf.ByteString descriptorData, Google.Protobuf.Reflection.FileDescriptorProto proto, Google.Protobuf.Reflection.FileDescriptor[] dependencies, System.Boolean allowUnknownDependencies, Google.Protobuf.Reflection.GeneratedClrTypeInfo generatedCodeInfo)
  RVA=0x09B1DDEC  token=0x6000B0D  System.Void CrossLink()
  RVA=0x09B1E4EC  token=0x6000B0E  Google.Protobuf.Reflection.FileDescriptor FromGeneratedCode(System.Byte[] descriptorData, Google.Protobuf.Reflection.FileDescriptor[] dependencies, Google.Protobuf.Reflection.GeneratedClrTypeInfo generatedCodeInfo)
  RVA=0x09B1EDA0  token=0x6000B0F  System.Collections.Generic.IEnumerable<Google.Protobuf.Extension> GetAllExtensions(Google.Protobuf.Reflection.FileDescriptor[] dependencies, Google.Protobuf.Reflection.GeneratedClrTypeInfo generatedInfo)
  RVA=0x09B1EEC4  token=0x6000B10  System.Collections.Generic.IEnumerable<Google.Protobuf.Extension> GetAllGeneratedExtensions(Google.Protobuf.Reflection.GeneratedClrTypeInfo generated)
  RVA=0x09B1EA88  token=0x6000B11  System.Collections.Generic.IEnumerable<Google.Protobuf.Extension> GetAllDependedExtensions(Google.Protobuf.Reflection.FileDescriptor descriptor)
  RVA=0x09B1E808  token=0x6000B12  System.Collections.Generic.IEnumerable<Google.Protobuf.Extension> GetAllDependedExtensionsFromMessage(Google.Protobuf.Reflection.MessageDescriptor descriptor)
  RVA=0x09B1D300  token=0x6000B13  System.Collections.Generic.IReadOnlyList<Google.Protobuf.Reflection.FileDescriptor> BuildFromByteStrings(System.Collections.Generic.IEnumerable<Google.Protobuf.ByteString> descriptorData, Google.Protobuf.ExtensionRegistry registry)
  RVA=0x09B1D2B4  token=0x6000B14  System.Collections.Generic.IReadOnlyList<Google.Protobuf.Reflection.FileDescriptor> BuildFromByteStrings(System.Collections.Generic.IEnumerable<Google.Protobuf.ByteString> descriptorData)
  RVA=0x09B1F270  token=0x6000B15  System.String ToString()
  RVA=0x09B1F21C  token=0x6000B18  Google.Protobuf.Reflection.FileOptions GetOptions()
  RVA=-1  // generic def  token=0x6000B19  T GetOption(Google.Protobuf.Extension<Google.Protobuf.Reflection.FileOptions,T> extension)
  RVA=-1  // generic def  token=0x6000B1A  Google.Protobuf.Collections.RepeatedField<T> GetOption(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.FileOptions,T> extension)
  RVA=-1  // generic def  token=0x6000B1B  System.Void ForceReflectionInitialization()
END_CLASS

CLASS: Google.Protobuf.Reflection.GeneratedClrTypeInfo
TYPE:  sealed class
TOKEN: 0x2000103
SIZE:  0x48
FIELDS:
  private   static readonly System.String[]                 EmptyNames  // static @ 0x0
  private   static readonly Google.Protobuf.Reflection.GeneratedClrTypeInfo[]EmptyCodeInfo  // static @ 0x8
  private   static readonly Google.Protobuf.Extension[]     EmptyExtensions  // static @ 0x10
  private   static  System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypesMessageAccessibility  // const
  private           System.Type                     <ClrType>k__BackingField  // 0x10
  private   readonly Google.Protobuf.MessageParser   <Parser>k__BackingField  // 0x18
  private   readonly System.String[]                 <PropertyNames>k__BackingField  // 0x20
  private   readonly Google.Protobuf.Extension[]     <Extensions>k__BackingField  // 0x28
  private   readonly System.String[]                 <OneofNames>k__BackingField  // 0x30
  private   readonly Google.Protobuf.Reflection.GeneratedClrTypeInfo[]<NestedTypes>k__BackingField  // 0x38
  private   readonly System.Type[]                   <NestedEnums>k__BackingField  // 0x40
PROPERTIES:
  ClrType  get=0x02B2ECC0  set=0x053908C0
  Parser  get=0x0385B100
  PropertyNames  get=0x04D862C0
  Extensions  get=0x04D86240
  OneofNames  get=0x011F36E0
  NestedTypes  get=0x04D85A50
  NestedEnums  get=0x04D85A60
METHODS:
  RVA=0x09B1F9C8  token=0x6000B30  System.Void .ctor(System.Type clrType, Google.Protobuf.MessageParser parser, System.String[] propertyNames, System.String[] oneofNames, System.Type[] nestedEnums, Google.Protobuf.Extension[] extensions, Google.Protobuf.Reflection.GeneratedClrTypeInfo[] nestedTypes)
  RVA=0x09B1FB80  token=0x6000B31  System.Void .ctor(System.Type clrType, Google.Protobuf.MessageParser parser, System.String[] propertyNames, System.String[] oneofNames, System.Type[] nestedEnums, Google.Protobuf.Reflection.GeneratedClrTypeInfo[] nestedTypes)
  RVA=0x09B1FB4C  token=0x6000B32  System.Void .ctor(System.Type[] nestedEnums, Google.Protobuf.Extension[] extensions, Google.Protobuf.Reflection.GeneratedClrTypeInfo[] nestedTypes)
  RVA=0x09B1F99C  token=0x6000B33  System.Void .ctor(System.Type[] nestedEnums, Google.Protobuf.Reflection.GeneratedClrTypeInfo[] nestedTypes)
  RVA=0x09B1F8B4  token=0x6000B34  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.Reflection.IDescriptor
TYPE:  interface
TOKEN: 0x2000104
FIELDS:
PROPERTIES:
  Name  get=-1  // abstract
  FullName  get=-1  // abstract
  File  get=-1  // abstract
METHODS:
END_CLASS

CLASS: Google.Protobuf.Reflection.IFieldAccessor
TYPE:  interface
TOKEN: 0x2000105
FIELDS:
PROPERTIES:
  Descriptor  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000B39  System.Void Clear(Google.Protobuf.IMessage message)
  RVA=-1  // abstract  token=0x6000B3A  System.Object GetValue(Google.Protobuf.IMessage message)
  RVA=-1  // abstract  token=0x6000B3B  System.Boolean HasValue(Google.Protobuf.IMessage message)
  RVA=-1  // abstract  token=0x6000B3C  System.Void SetValue(Google.Protobuf.IMessage message, System.Object value)
END_CLASS

CLASS: Google.Protobuf.Reflection.MapFieldAccessor
TYPE:  sealed class
TOKEN: 0x2000106
SIZE:  0x20
EXTENDS: Google.Protobuf.Reflection.FieldAccessorBase
FIELDS:
METHODS:
  RVA=0x09B1FCF8  token=0x6000B3D  System.Void .ctor(System.Reflection.PropertyInfo property, Google.Protobuf.Reflection.FieldDescriptor descriptor)
  RVA=0x09B1FBC4  token=0x6000B3E  System.Void Clear(Google.Protobuf.IMessage message)
  RVA=0x09B1FC40  token=0x6000B3F  System.Boolean HasValue(Google.Protobuf.IMessage message)
  RVA=0x09B1FC9C  token=0x6000B40  System.Void SetValue(Google.Protobuf.IMessage message, System.Object value)
END_CLASS

CLASS: Google.Protobuf.Reflection.MessageDescriptor
TYPE:  sealed class
TOKEN: 0x2000107
SIZE:  0x98
EXTENDS: Google.Protobuf.Reflection.DescriptorBase
FIELDS:
  private   static readonly System.Collections.Generic.HashSet<System.String>WellKnownTypeNames  // static @ 0x0
  private   readonly System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor>fieldsInDeclarationOrder  // 0x28
  private   readonly System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor>fieldsInNumberOrder  // 0x30
  private   readonly System.Collections.Generic.IDictionary<System.String,Google.Protobuf.Reflection.FieldDescriptor>jsonFieldMap  // 0x38
  private           System.Func<Google.Protobuf.IMessage,System.Boolean>extensionSetIsInitialized  // 0x40
  private   readonly Google.Protobuf.Reflection.DescriptorProto<Proto>k__BackingField  // 0x48
  private   readonly System.Type                     <ClrType>k__BackingField  // 0x50
  private   readonly Google.Protobuf.MessageParser   <Parser>k__BackingField  // 0x58
  private   readonly Google.Protobuf.Reflection.MessageDescriptor<ContainingType>k__BackingField  // 0x60
  private   readonly Google.Protobuf.Reflection.MessageDescriptor.FieldCollection<Fields>k__BackingField  // 0x68
  private   readonly Google.Protobuf.Reflection.ExtensionCollection<Extensions>k__BackingField  // 0x70
  private   readonly System.Collections.Generic.IList<Google.Protobuf.Reflection.MessageDescriptor><NestedTypes>k__BackingField  // 0x78
  private   readonly System.Collections.Generic.IList<Google.Protobuf.Reflection.EnumDescriptor><EnumTypes>k__BackingField  // 0x80
  private   readonly System.Collections.Generic.IList<Google.Protobuf.Reflection.OneofDescriptor><Oneofs>k__BackingField  // 0x88
  private   readonly System.Int32                    <RealOneofCount>k__BackingField  // 0x90
PROPERTIES:
  Name  get=0x09B211CC
  Proto  get=0x04D86200
  ClrType  get=0x04D86270
  Parser  get=0x02E56440
  IsWellKnownType  get=0x09B21080
  IsWrapperType  get=0x09B21140
  ContainingType  get=0x04D86280
  Fields  get=0x04D86230
  Extensions  get=0x04D862B0
  NestedTypes  get=0x04D862A0
  EnumTypes  get=0x04D86260
  Oneofs  get=0x04D861F0
  RealOneofCount  get=0x04D86B60
  CustomOptions  get=0x09B20FF0
METHODS:
  RVA=0x09B20688  token=0x6000B41  System.Void .ctor(Google.Protobuf.Reflection.DescriptorProto proto, Google.Protobuf.Reflection.FileDescriptor file, Google.Protobuf.Reflection.MessageDescriptor parent, System.Int32 typeIndex, Google.Protobuf.Reflection.GeneratedClrTypeInfo generatedCodeInfo)
  RVA=0x09B1FD00  token=0x6000B42  System.Collections.ObjectModel.ReadOnlyDictionary<System.String,Google.Protobuf.Reflection.FieldDescriptor> CreateJsonFieldMap(System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor> fields)
  RVA=0x09B2032C  token=0x6000B44  System.Collections.Generic.IReadOnlyList<Google.Protobuf.Reflection.DescriptorBase> GetNestedDescriptorListForField(System.Int32 fieldNumber)
  RVA=0x09B20478  token=0x6000B46  Google.Protobuf.Reflection.DescriptorProto ToProto()
  RVA=0x09B203C4  token=0x6000B47  System.Boolean IsExtensionsInitialized(Google.Protobuf.IMessage message)
  RVA=0x09B20270  token=0x6000B53  Google.Protobuf.Reflection.FieldDescriptor FindFieldByName(System.String name)
  RVA=0x09B202FC  token=0x6000B54  Google.Protobuf.Reflection.FieldDescriptor FindFieldByNumber(System.Int32 number)
  RVA=-1  // generic def  token=0x6000B55  T FindDescriptor(System.String name)
  RVA=0x09B20398  token=0x6000B57  Google.Protobuf.Reflection.MessageOptions GetOptions()
  RVA=-1  // generic def  token=0x6000B58  T GetOption(Google.Protobuf.Extension<Google.Protobuf.Reflection.MessageOptions,T> extension)
  RVA=-1  // generic def  token=0x6000B59  Google.Protobuf.Collections.RepeatedField<T> GetOption(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.MessageOptions,T> extension)
  RVA=0x09B1FF20  token=0x6000B5A  System.Void CrossLink()
  RVA=0x09B20498  token=0x6000B5B  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.Reflection.MethodDescriptor
TYPE:  sealed class
TOKEN: 0x200010B
SIZE:  0x48
EXTENDS: Google.Protobuf.Reflection.DescriptorBase
FIELDS:
  private   readonly Google.Protobuf.Reflection.MethodDescriptorProtoproto  // 0x28
  private   readonly Google.Protobuf.Reflection.ServiceDescriptorservice  // 0x30
  private           Google.Protobuf.Reflection.MessageDescriptorinputType  // 0x38
  private           Google.Protobuf.Reflection.MessageDescriptoroutputType  // 0x40
PROPERTIES:
  Service  get=0x011F36E0
  InputType  get=0x04D85A50
  OutputType  get=0x04D85A60
  IsClientStreaming  get=0x09B215F4
  IsServerStreaming  get=0x09B21614
  CustomOptions  get=0x09B21564
  Proto  get=0x04D86240
  Name  get=0x09B21634
METHODS:
  RVA=0x09B21434  token=0x6000B70  Google.Protobuf.Reflection.MethodOptions GetOptions()
  RVA=-1  // generic def  token=0x6000B71  T GetOption(Google.Protobuf.Extension<Google.Protobuf.Reflection.MethodOptions,T> extension)
  RVA=-1  // generic def  token=0x6000B72  Google.Protobuf.Collections.RepeatedField<T> GetOption(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.MethodOptions,T> extension)
  RVA=0x09B21480  token=0x6000B73  System.Void .ctor(Google.Protobuf.Reflection.MethodDescriptorProto proto, Google.Protobuf.Reflection.FileDescriptor file, Google.Protobuf.Reflection.ServiceDescriptor parent, System.Int32 index)
  RVA=0x09B21460  token=0x6000B75  Google.Protobuf.Reflection.MethodDescriptorProto ToProto()
  RVA=0x09B211EC  token=0x6000B77  System.Void CrossLink()
END_CLASS

CLASS: Google.Protobuf.Reflection.OneofAccessor
TYPE:  sealed class
TOKEN: 0x200010C
SIZE:  0x28
FIELDS:
  private   readonly System.Func<Google.Protobuf.IMessage,System.Int32>caseDelegate  // 0x10
  private   readonly System.Action<Google.Protobuf.IMessage>clearDelegate  // 0x18
  private   readonly Google.Protobuf.Reflection.OneofDescriptor<Descriptor>k__BackingField  // 0x20
PROPERTIES:
  Descriptor  get=0x04D862C0
METHODS:
  RVA=0x09B218C4  token=0x6000B78  System.Void .ctor(Google.Protobuf.Reflection.OneofDescriptor descriptor, System.Func<Google.Protobuf.IMessage,System.Int32> caseDelegate, System.Action<Google.Protobuf.IMessage> clearDelegate)
  RVA=0x09B21674  token=0x6000B79  Google.Protobuf.Reflection.OneofAccessor ForRegularOneof(Google.Protobuf.Reflection.OneofDescriptor descriptor, System.Reflection.PropertyInfo caseProperty, System.Reflection.MethodInfo clearMethod)
  RVA=0x09B2173C  token=0x6000B7A  Google.Protobuf.Reflection.OneofAccessor ForSyntheticOneof(Google.Protobuf.Reflection.OneofDescriptor descriptor)
  RVA=0x09B21654  token=0x6000B7C  System.Void Clear(Google.Protobuf.IMessage message)
  RVA=0x09B21874  token=0x6000B7D  Google.Protobuf.Reflection.FieldDescriptor GetCaseFieldDescriptor(Google.Protobuf.IMessage message)
END_CLASS

CLASS: Google.Protobuf.Reflection.OneofDescriptor
TYPE:  sealed class
TOKEN: 0x200010E
SIZE:  0x50
EXTENDS: Google.Protobuf.Reflection.DescriptorBase
FIELDS:
  private           Google.Protobuf.Reflection.MessageDescriptorcontainingType  // 0x28
  private           System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor>fields  // 0x30
  private   readonly Google.Protobuf.Reflection.OneofAccessoraccessor  // 0x38
  private   readonly Google.Protobuf.Reflection.OneofDescriptorProto<Proto>k__BackingField  // 0x40
  private   readonly System.Boolean                  <IsSynthetic>k__BackingField  // 0x48
PROPERTIES:
  Name  get=0x09B22088
  Proto  get=0x04D85A60
  ContainingType  get=0x04D86240
  Fields  get=0x011F36E0
  IsSynthetic  get=0x04D867D0
  Accessor  get=0x04D85A50
  CustomOptions  get=0x09B21FF8
METHODS:
  RVA=0x09B21E58  token=0x6000B81  System.Void .ctor(Google.Protobuf.Reflection.OneofDescriptorProto proto, Google.Protobuf.Reflection.FileDescriptor file, Google.Protobuf.Reflection.MessageDescriptor parent, System.Int32 index, System.String clrName)
  RVA=0x09B21E38  token=0x6000B84  Google.Protobuf.Reflection.OneofDescriptorProto ToProto()
  RVA=0x09B21E0C  token=0x6000B8A  Google.Protobuf.Reflection.OneofOptions GetOptions()
  RVA=-1  // generic def  token=0x6000B8B  T GetOption(Google.Protobuf.Extension<Google.Protobuf.Reflection.OneofOptions,T> extension)
  RVA=-1  // generic def  token=0x6000B8C  Google.Protobuf.Collections.RepeatedField<T> GetOption(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.OneofOptions,T> extension)
  RVA=0x09B21BA0  token=0x6000B8D  System.Void CrossLink()
  RVA=0x09B218FC  token=0x6000B8E  Google.Protobuf.Reflection.OneofAccessor CreateAccessor(System.String clrName)
END_CLASS

CLASS: Google.Protobuf.Reflection.OriginalNameAttribute
TYPE:  class
TOKEN: 0x2000110
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  private           System.String                   <Name>k__BackingField  // 0x10
  private           System.Boolean                  <PreferredAlias>k__BackingField  // 0x18
PROPERTIES:
  Name  get=0x02B2ECC0  set=0x053908C0
  PreferredAlias  get=0x04D86340  set=0x04D86350
METHODS:
  RVA=0x03537000  token=0x6000B95  System.Void .ctor(System.String name)
END_CLASS

CLASS: Google.Protobuf.Reflection.PackageDescriptor
TYPE:  sealed class
TOKEN: 0x2000111
SIZE:  0x28
IMPLEMENTS: Google.Protobuf.Reflection.IDescriptor
FIELDS:
  private   readonly System.String                   name  // 0x10
  private   readonly System.String                   fullName  // 0x18
  private   readonly Google.Protobuf.Reflection.FileDescriptorfile  // 0x20
PROPERTIES:
  Name  get=0x02B2ECC0
  FullName  get=0x0385B100
  File  get=0x04D862C0
METHODS:
  RVA=0x09B220A8  token=0x6000B96  System.Void .ctor(System.String name, System.String fullName, Google.Protobuf.Reflection.FileDescriptor file)
END_CLASS

CLASS: Google.Protobuf.Reflection.ReflectionUtil
TYPE:  static class
TOKEN: 0x2000112
SIZE:  0x10
FIELDS:
  private   static readonly System.Type[]                   EmptyTypes  // static @ 0x0
  private   static readonly System.Boolean                  <CanConvertEnumFuncToInt32Func>k__BackingField  // static @ 0x8
PROPERTIES:
  CanConvertEnumFuncToInt32Func  get=0x09B22DF8
METHODS:
  RVA=0x09B22BF8  token=0x6000B9A  System.Void .cctor()
  RVA=-1  // generic def  token=0x6000B9B  System.Void ForceInitialize()
  RVA=0x09B228F4  token=0x6000B9C  System.Func<Google.Protobuf.IMessage,System.Object> CreateFuncIMessageObject(System.Reflection.MethodInfo method)
  RVA=0x09B2282C  token=0x6000B9D  System.Func<Google.Protobuf.IMessage,System.Int32> CreateFuncIMessageInt32(System.Reflection.MethodInfo method)
  RVA=0x09B223F0  token=0x6000B9E  System.Action<Google.Protobuf.IMessage,System.Object> CreateActionIMessageObject(System.Reflection.MethodInfo method)
  RVA=0x09B224D4  token=0x6000B9F  System.Action<Google.Protobuf.IMessage> CreateActionIMessage(System.Reflection.MethodInfo method)
  RVA=0x09B22764  token=0x6000BA0  System.Func<Google.Protobuf.IMessage,System.Boolean> CreateFuncIMessageBool(System.Reflection.MethodInfo method)
  RVA=0x09B229BC  token=0x6000BA1  System.Func<Google.Protobuf.IMessage,System.Boolean> CreateIsInitializedCaller(System.Type msg)
  RVA=0x09B225C4  token=0x6000BA2  Google.Protobuf.Reflection.ReflectionUtil.IExtensionReflectionHelper CreateExtensionHelper(Google.Protobuf.Extension extension)
  RVA=0x09B22AEC  token=0x6000BA3  Google.Protobuf.Reflection.ReflectionUtil.IReflectionHelper GetReflectionHelper(System.Type t1, System.Type t2)
  RVA=0x09B222C4  token=0x6000BA5  System.Boolean CheckCanConvertEnumFuncToInt32Func()
  RVA=0x011EC580  token=0x6000BA6  Google.Protobuf.Reflection.ReflectionUtil.SampleEnum SampleEnumMethod()
END_CLASS

CLASS: Google.Protobuf.Reflection.RepeatedFieldAccessor
TYPE:  sealed class
TOKEN: 0x2000121
SIZE:  0x20
EXTENDS: Google.Protobuf.Reflection.FieldAccessorBase
FIELDS:
METHODS:
  RVA=0x09B1FCF8  token=0x6000BCC  System.Void .ctor(System.Reflection.PropertyInfo property, Google.Protobuf.Reflection.FieldDescriptor descriptor)
  RVA=0x09B22E48  token=0x6000BCD  System.Void Clear(Google.Protobuf.IMessage message)
  RVA=0x09B22EC4  token=0x6000BCE  System.Boolean HasValue(Google.Protobuf.IMessage message)
  RVA=0x09B22F20  token=0x6000BCF  System.Void SetValue(Google.Protobuf.IMessage message, System.Object value)
END_CLASS

CLASS: Google.Protobuf.Reflection.ServiceDescriptor
TYPE:  sealed class
TOKEN: 0x2000122
SIZE:  0x38
EXTENDS: Google.Protobuf.Reflection.DescriptorBase
FIELDS:
  private   readonly Google.Protobuf.Reflection.ServiceDescriptorProtoproto  // 0x28
  private   readonly System.Collections.Generic.IList<Google.Protobuf.Reflection.MethodDescriptor>methods  // 0x30
PROPERTIES:
  Name  get=0x09B233C8
  Proto  get=0x04D86240
  Methods  get=0x011F36E0
  CustomOptions  get=0x09B23338
METHODS:
  RVA=0x09B231C4  token=0x6000BD0  System.Void .ctor(Google.Protobuf.Reflection.ServiceDescriptorProto proto, Google.Protobuf.Reflection.FileDescriptor file, System.Int32 index)
  RVA=0x09B23128  token=0x6000BD2  System.Collections.Generic.IReadOnlyList<Google.Protobuf.Reflection.DescriptorBase> GetNestedDescriptorListForField(System.Int32 fieldNumber)
  RVA=0x09B231A4  token=0x6000BD4  Google.Protobuf.Reflection.ServiceDescriptorProto ToProto()
  RVA=0x09B2309C  token=0x6000BD6  Google.Protobuf.Reflection.MethodDescriptor FindMethodByName(System.String name)
  RVA=0x09B23178  token=0x6000BD8  Google.Protobuf.Reflection.ServiceOptions GetOptions()
  RVA=-1  // generic def  token=0x6000BD9  T GetOption(Google.Protobuf.Extension<Google.Protobuf.Reflection.ServiceOptions,T> extension)
  RVA=-1  // generic def  token=0x6000BDA  Google.Protobuf.Collections.RepeatedField<T> GetOption(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.ServiceOptions,T> extension)
  RVA=0x09B22F7C  token=0x6000BDB  System.Void CrossLink()
END_CLASS

CLASS: Google.Protobuf.Reflection.SingleFieldAccessor
TYPE:  sealed class
TOKEN: 0x2000124
SIZE:  0x38
EXTENDS: Google.Protobuf.Reflection.FieldAccessorBase
FIELDS:
  private   readonly System.Action<Google.Protobuf.IMessage,System.Object>setValueDelegate  // 0x20
  private   readonly System.Action<Google.Protobuf.IMessage>clearDelegate  // 0x28
  private   readonly System.Func<Google.Protobuf.IMessage,System.Boolean>hasDelegate  // 0x30
METHODS:
  RVA=0x09B23658  token=0x6000BDE  System.Void .ctor(System.Type messageType, System.Reflection.PropertyInfo property, Google.Protobuf.Reflection.FieldDescriptor descriptor)
  RVA=0x09B233E8  token=0x6000BDF  System.Object GetDefaultValue(Google.Protobuf.Reflection.FieldDescriptor descriptor)
  RVA=0x0699C87C  token=0x6000BE0  System.Void Clear(Google.Protobuf.IMessage message)
  RVA=0x09B23618  token=0x6000BE1  System.Boolean HasValue(Google.Protobuf.IMessage message)
  RVA=0x09B23638  token=0x6000BE2  System.Void SetValue(Google.Protobuf.IMessage message, System.Object value)
END_CLASS

CLASS: Google.Protobuf.Reflection.TypeRegistry
TYPE:  sealed class
TOKEN: 0x2000129
SIZE:  0x18
FIELDS:
  private   static readonly Google.Protobuf.Reflection.TypeRegistry<Empty>k__BackingField  // static @ 0x0
  private   readonly System.Collections.Generic.Dictionary<System.String,Google.Protobuf.Reflection.MessageDescriptor>fullNameToMessageMap  // 0x10
PROPERTIES:
  Empty  get=0x09B24368
METHODS:
  RVA=0x053908C0  token=0x6000BEF  System.Void .ctor(System.Collections.Generic.Dictionary<System.String,Google.Protobuf.Reflection.MessageDescriptor> fullNameToMessageMap)
  RVA=0x09B23EA0  token=0x6000BF0  Google.Protobuf.Reflection.MessageDescriptor Find(System.String fullName)
  RVA=0x09B23F08  token=0x6000BF1  Google.Protobuf.Reflection.TypeRegistry FromFiles(Google.Protobuf.Reflection.FileDescriptor[] fileDescriptors)
  RVA=0x09B23F54  token=0x6000BF2  Google.Protobuf.Reflection.TypeRegistry FromFiles(System.Collections.Generic.IEnumerable<Google.Protobuf.Reflection.FileDescriptor> fileDescriptors)
  RVA=0x09B2410C  token=0x6000BF3  Google.Protobuf.Reflection.TypeRegistry FromMessages(Google.Protobuf.Reflection.MessageDescriptor[] messageDescriptors)
  RVA=0x09B24158  token=0x6000BF4  Google.Protobuf.Reflection.TypeRegistry FromMessages(System.Collections.Generic.IEnumerable<Google.Protobuf.Reflection.MessageDescriptor> messageDescriptors)
  RVA=0x09B242B0  token=0x6000BF5  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.AnyReflection
TYPE:  static class
TOKEN: 0x2000060
SIZE:  0x10
FIELDS:
  private   static  Google.Protobuf.Reflection.FileDescriptordescriptor  // static @ 0x0
PROPERTIES:
  Descriptor  get=0x09AE3438
METHODS:
  RVA=0x09AE30E0  token=0x60003A9  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.Any
TYPE:  sealed class
TOKEN: 0x2000061
SIZE:  0x28
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Any>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    TypeUrlFieldNumber  // const
  private           System.String                   typeUrl_  // 0x18
  public    static  System.Int32                    ValueFieldNumber  // const
  private           Google.Protobuf.ByteString      value_  // 0x20
  private   static  System.String                   DefaultPrefix  // const
PROPERTIES:
  Parser  get=0x09AE3D6C
  Descriptor  get=0x09AE3D0C
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09AE3EF8
  TypeUrl  get=0x0385B100  set=0x09AE3F38
  Value  get=0x04D862C0  set=0x09AE3FA0
METHODS:
  RVA=0x09AE3CA4  token=0x60003AD  System.Void .ctor()
  RVA=0x09AE3C3C  token=0x60003AE  System.Void .ctor(Google.Protobuf.WellKnownTypes.Any other)
  RVA=0x09AE3540  token=0x60003AF  Google.Protobuf.WellKnownTypes.Any Clone()
  RVA=0x09AE35A0  token=0x60003B4  System.Boolean Equals(System.Object other)
  RVA=0x09AE3604  token=0x60003B5  System.Boolean Equals(Google.Protobuf.WellKnownTypes.Any other)
  RVA=0x09AE36AC  token=0x60003B6  System.Int32 GetHashCode()
  RVA=0x09AE3AEC  token=0x60003B7  System.String ToString()
  RVA=0x04D52400  token=0x60003B8  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09AE3E68  token=0x60003B9  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09AE3488  token=0x60003BA  System.Int32 CalculateSize()
  RVA=0x09AE38E0  token=0x60003BB  System.Void MergeFrom(Google.Protobuf.WellKnownTypes.Any other)
  RVA=0x04D51840  token=0x60003BC  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09AE3DBC  token=0x60003BD  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09AE37BC  token=0x60003BE  System.String GetTypeUrl(Google.Protobuf.Reflection.MessageDescriptor descriptor, System.String prefix)
  RVA=0x09AE3728  token=0x60003BF  System.String GetTypeName(System.String typeUrl)
  RVA=0x09AE3848  token=0x60003C0  System.Boolean Is(Google.Protobuf.Reflection.MessageDescriptor descriptor)
  RVA=-1  // generic def  token=0x60003C1  T Unpack()
  RVA=-1  // generic def  token=0x60003C2  System.Boolean TryUnpack(T& result)
  RVA=0x09AE3A98  token=0x60003C3  Google.Protobuf.WellKnownTypes.Any Pack(Google.Protobuf.IMessage message)
  RVA=0x09AE3960  token=0x60003C4  Google.Protobuf.WellKnownTypes.Any Pack(Google.Protobuf.IMessage message, System.String typeUrlPrefix)
  RVA=0x09AE3B38  token=0x60003C5  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.ApiReflection
TYPE:  static class
TOKEN: 0x2000063
SIZE:  0x10
FIELDS:
  private   static  Google.Protobuf.Reflection.FileDescriptordescriptor  // static @ 0x0
PROPERTIES:
  Descriptor  get=0x09AE4804
METHODS:
  RVA=0x09AE4008  token=0x60003CA  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.Api
TYPE:  sealed class
TOKEN: 0x2000064
SIZE:  0x50
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Api>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    NameFieldNumber  // const
  private           System.String                   name_  // 0x18
  public    static  System.Int32                    MethodsFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.WellKnownTypes.Method>_repeated_methods_codec  // static @ 0x8
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.Method>methods_  // 0x20
  public    static  System.Int32                    OptionsFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.WellKnownTypes.Option>_repeated_options_codec  // static @ 0x10
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.Option>options_  // 0x28
  public    static  System.Int32                    VersionFieldNumber  // const
  private           System.String                   version_  // 0x30
  public    static  System.Int32                    SourceContextFieldNumber  // const
  private           Google.Protobuf.WellKnownTypes.SourceContextsourceContext_  // 0x38
  public    static  System.Int32                    MixinsFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.WellKnownTypes.Mixin>_repeated_mixins_codec  // static @ 0x18
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.Mixin>mixins_  // 0x40
  public    static  System.Int32                    SyntaxFieldNumber  // const
  private           Google.Protobuf.WellKnownTypes.Syntaxsyntax_  // 0x48
PROPERTIES:
  Parser  get=0x09AE53FC
  Descriptor  get=0x09AE539C
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09AE585C
  Name  get=0x0385B100  set=0x09AE589C
  Methods  get=0x04D862C0
  Options  get=0x04D86240
  Version  get=0x011F36E0  set=0x09AE5904
  SourceContext  get=0x04D85A50  set=0x046A91B0
  Mixins  get=0x04D85A60
  Syntax  get=0x04D863D0  set=0x04D86450
METHODS:
  RVA=0x09AE5120  token=0x60003CE  System.Void .ctor()
  RVA=0x09AE5260  token=0x60003CF  System.Void .ctor(Google.Protobuf.WellKnownTypes.Api other)
  RVA=0x09AE4A34  token=0x60003D0  Google.Protobuf.WellKnownTypes.Api Clone()
  RVA=0x09AE4A94  token=0x60003DC  System.Boolean Equals(System.Object other)
  RVA=0x09AE4AF8  token=0x60003DD  System.Boolean Equals(Google.Protobuf.WellKnownTypes.Api other)
  RVA=0x09AE4C20  token=0x60003DE  System.Int32 GetHashCode()
  RVA=0x09AE4E94  token=0x60003DF  System.String ToString()
  RVA=0x04D52400  token=0x60003E0  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09AE5698  token=0x60003E1  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09AE4854  token=0x60003E2  System.Int32 CalculateSize()
  RVA=0x09AE4D14  token=0x60003E3  System.Void MergeFrom(Google.Protobuf.WellKnownTypes.Api other)
  RVA=0x04D51840  token=0x60003E4  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09AE544C  token=0x60003E5  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09AE4EE0  token=0x60003E6  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.Method
TYPE:  sealed class
TOKEN: 0x2000066
SIZE:  0x50
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Method>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    NameFieldNumber  // const
  private           System.String                   name_  // 0x18
  public    static  System.Int32                    RequestTypeUrlFieldNumber  // const
  private           System.String                   requestTypeUrl_  // 0x20
  public    static  System.Int32                    RequestStreamingFieldNumber  // const
  private           System.Boolean                  requestStreaming_  // 0x28
  public    static  System.Int32                    ResponseTypeUrlFieldNumber  // const
  private           System.String                   responseTypeUrl_  // 0x30
  public    static  System.Int32                    ResponseStreamingFieldNumber  // const
  private           System.Boolean                  responseStreaming_  // 0x38
  public    static  System.Int32                    OptionsFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.WellKnownTypes.Option>_repeated_options_codec  // static @ 0x8
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.Option>options_  // 0x40
  public    static  System.Int32                    SyntaxFieldNumber  // const
  private           Google.Protobuf.WellKnownTypes.Syntaxsyntax_  // 0x48
PROPERTIES:
  Parser  get=0x09AE9950
  Descriptor  get=0x09AE98F0
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09AE9CDC
  Name  get=0x0385B100  set=0x09AE9D1C
  RequestTypeUrl  get=0x04D862C0  set=0x09AE9D84
  RequestStreaming  get=0x011F33C0  set=0x011F33D0
  ResponseTypeUrl  get=0x011F36E0  set=0x09AE9DEC
  ResponseStreaming  get=0x011F2A70  set=0x011F2A80
  Options  get=0x04D85A60
  Syntax  get=0x04D863D0  set=0x04D86450
METHODS:
  RVA=0x09AE9830  token=0x60003ED  System.Void .ctor()
  RVA=0x09AE975C  token=0x60003EE  System.Void .ctor(Google.Protobuf.WellKnownTypes.Method other)
  RVA=0x09AE91F8  token=0x60003EF  Google.Protobuf.WellKnownTypes.Method Clone()
  RVA=0x09AE9330  token=0x60003FD  System.Boolean Equals(System.Object other)
  RVA=0x09AE9258  token=0x60003FE  System.Boolean Equals(Google.Protobuf.WellKnownTypes.Method other)
  RVA=0x09AE9394  token=0x60003FF  System.Int32 GetHashCode()
  RVA=0x09AE95A0  token=0x6000400  System.String ToString()
  RVA=0x04D52400  token=0x6000401  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09AE9B48  token=0x6000402  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09AE906C  token=0x6000403  System.Int32 CalculateSize()
  RVA=0x09AE94A0  token=0x6000404  System.Void MergeFrom(Google.Protobuf.WellKnownTypes.Method other)
  RVA=0x04D51840  token=0x6000405  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09AE99A0  token=0x6000406  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09AE95EC  token=0x6000407  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.Mixin
TYPE:  sealed class
TOKEN: 0x2000068
SIZE:  0x28
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Mixin>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    NameFieldNumber  // const
  private           System.String                   name_  // 0x18
  public    static  System.Int32                    RootFieldNumber  // const
  private           System.String                   root_  // 0x20
PROPERTIES:
  Parser  get=0x09AEA2B4
  Descriptor  get=0x09AEA254
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09AEA3B0
  Name  get=0x0385B100  set=0x09AEA3F0
  Root  get=0x04D862C0  set=0x09AEA458
METHODS:
  RVA=0x09AEA200  token=0x600040E  System.Void .ctor()
  RVA=0x09AEA198  token=0x600040F  System.Void .ctor(Google.Protobuf.WellKnownTypes.Mixin other)
  RVA=0x09AE9F08  token=0x6000410  Google.Protobuf.WellKnownTypes.Mixin Clone()
  RVA=0x09AE9F68  token=0x6000415  System.Boolean Equals(System.Object other)
  RVA=0x063C4394  token=0x6000416  System.Boolean Equals(Google.Protobuf.WellKnownTypes.Mixin other)
  RVA=0x063C43FC  token=0x6000417  System.Int32 GetHashCode()
  RVA=0x09AEA048  token=0x6000418  System.String ToString()
  RVA=0x04D52400  token=0x6000419  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x063C47AC  token=0x600041A  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09AE9E54  token=0x600041B  System.Int32 CalculateSize()
  RVA=0x09AE9FCC  token=0x600041C  System.Void MergeFrom(Google.Protobuf.WellKnownTypes.Mixin other)
  RVA=0x04D51840  token=0x600041D  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09AEA304  token=0x600041E  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09AEA094  token=0x600041F  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.DurationReflection
TYPE:  static class
TOKEN: 0x200006A
SIZE:  0x10
FIELDS:
  private   static  Google.Protobuf.Reflection.FileDescriptordescriptor  // static @ 0x0
PROPERTIES:
  Descriptor  get=0x09AE5CE4
METHODS:
  RVA=0x09AE596C  token=0x6000424  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.Duration
TYPE:  sealed class
TOKEN: 0x200006B
SIZE:  0x28
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage Google.Protobuf.ICustomDiagnosticMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Duration>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    SecondsFieldNumber  // const
  private           System.Int64                    seconds_  // 0x18
  public    static  System.Int32                    NanosFieldNumber  // const
  private           System.Int32                    nanos_  // 0x20
  public    static  System.Int32                    NanosecondsPerSecond  // const
  public    static  System.Int32                    NanosecondsPerTick  // const
  public    static  System.Int64                    MaxSeconds  // const
  public    static  System.Int64                    MinSeconds  // const
  private   static  System.Int32                    MaxNanoseconds  // const
  private   static  System.Int32                    MinNanoseconds  // const
PROPERTIES:
  Parser  get=0x09AE6888
  Descriptor  get=0x09AE67F4
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09AE6C60
  Seconds  get=0x0385B100  set=0x011EC9D0
  Nanos  get=0x02B70F90  set=0x04D86330
METHODS:
  RVA=0x041E1670  token=0x6000428  System.Void .ctor()
  RVA=0x0637B49C  token=0x6000429  System.Void .ctor(Google.Protobuf.WellKnownTypes.Duration other)
  RVA=0x09AE5F20  token=0x600042A  Google.Protobuf.WellKnownTypes.Duration Clone()
  RVA=0x09AE5F80  token=0x600042F  System.Boolean Equals(System.Object other)
  RVA=0x0637BDB8  token=0x6000430  System.Boolean Equals(Google.Protobuf.WellKnownTypes.Duration other)
  RVA=0x0637BDEC  token=0x6000431  System.Int32 GetHashCode()
  RVA=0x09AE653C  token=0x6000432  System.String ToString()
  RVA=0x04D52400  token=0x6000433  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x0637C018  token=0x6000434  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09AE5E84  token=0x6000435  System.Int32 CalculateSize()
  RVA=0x0637BE38  token=0x6000436  System.Void MergeFrom(Google.Protobuf.WellKnownTypes.Duration other)
  RVA=0x04D51840  token=0x6000437  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x0637BF80  token=0x6000438  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09AE6168  token=0x6000439  System.Boolean IsNormalized(System.Int64 seconds, System.Int32 nanoseconds)
  RVA=0x09AE6588  token=0x600043A  System.TimeSpan ToTimeSpan()
  RVA=0x09AE5FE4  token=0x600043B  Google.Protobuf.WellKnownTypes.Duration FromTimeSpan(System.TimeSpan timeSpan)
  RVA=0x09AE6B68  token=0x600043C  Google.Protobuf.WellKnownTypes.Duration op_UnaryNegation(Google.Protobuf.WellKnownTypes.Duration value)
  RVA=0x09AE68D8  token=0x600043D  Google.Protobuf.WellKnownTypes.Duration op_Addition(Google.Protobuf.WellKnownTypes.Duration lhs, Google.Protobuf.WellKnownTypes.Duration rhs)
  RVA=0x09AE6A1C  token=0x600043E  Google.Protobuf.WellKnownTypes.Duration op_Subtraction(Google.Protobuf.WellKnownTypes.Duration lhs, Google.Protobuf.WellKnownTypes.Duration rhs)
  RVA=0x09AE61FC  token=0x600043F  Google.Protobuf.WellKnownTypes.Duration Normalize(System.Int64 seconds, System.Int32 nanoseconds)
  RVA=0x09AE62F0  token=0x6000440  System.String ToJson(System.Int64 seconds, System.Int32 nanoseconds, System.Boolean diagnosticOnly)
  RVA=0x09AE6298  token=0x6000441  System.String ToDiagnosticString()
  RVA=0x09AE5D34  token=0x6000442  System.Void AppendNanoseconds(System.Text.StringBuilder builder, System.Int32 nanos)
  RVA=0x09AE66F0  token=0x6000443  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.EmptyReflection
TYPE:  static class
TOKEN: 0x200006D
SIZE:  0x10
FIELDS:
  private   static  Google.Protobuf.Reflection.FileDescriptordescriptor  // static @ 0x0
PROPERTIES:
  Descriptor  get=0x09AE6F98
METHODS:
  RVA=0x09AE6CA0  token=0x6000448  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.Empty
TYPE:  sealed class
TOKEN: 0x200006E
SIZE:  0x18
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Empty>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
PROPERTIES:
  Parser  get=0x09AE7290
  Descriptor  get=0x09AE71FC
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09AE72E0
METHODS:
  RVA=0x041E1670  token=0x600044C  System.Void .ctor()
  RVA=0x0639FAEC  token=0x600044D  System.Void .ctor(Google.Protobuf.WellKnownTypes.Empty other)
  RVA=0x09AE6FE8  token=0x600044E  Google.Protobuf.WellKnownTypes.Empty Clone()
  RVA=0x09AE7048  token=0x600044F  System.Boolean Equals(System.Object other)
  RVA=0x0639F91C  token=0x6000450  System.Boolean Equals(Google.Protobuf.WellKnownTypes.Empty other)
  RVA=0x0639F940  token=0x6000451  System.Int32 GetHashCode()
  RVA=0x09AE70AC  token=0x6000452  System.String ToString()
  RVA=0x04D52400  token=0x6000453  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x0639FC2C  token=0x6000454  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x0639F838  token=0x6000455  System.Int32 CalculateSize()
  RVA=0x0639F96C  token=0x6000456  System.Void MergeFrom(Google.Protobuf.WellKnownTypes.Empty other)
  RVA=0x04D51840  token=0x6000457  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x0639FBD0  token=0x6000458  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09AE70F8  token=0x6000459  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.FieldMaskReflection
TYPE:  static class
TOKEN: 0x2000070
SIZE:  0x10
FIELDS:
  private   static  Google.Protobuf.Reflection.FileDescriptordescriptor  // static @ 0x0
PROPERTIES:
  Descriptor  get=0x09AE7658
METHODS:
  RVA=0x09AE7320  token=0x600045E  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.FieldMask
TYPE:  sealed class
TOKEN: 0x2000071
SIZE:  0x20
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage Google.Protobuf.ICustomDiagnosticMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.FieldMask>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    PathsFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<System.String>_repeated_paths_codec  // static @ 0x8
  private   readonly Google.Protobuf.Collections.RepeatedField<System.String>paths_  // 0x18
  private   static  System.Char                     FIELD_PATH_SEPARATOR  // const
  private   static  System.Char                     FIELD_SEPARATOR_REGEX  // const
PROPERTIES:
  Parser  get=0x09AE86FC
  Descriptor  get=0x09AE8668
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09AE88C0
  Paths  get=0x0385B100
METHODS:
  RVA=0x09AE85F0  token=0x6000462  System.Void .ctor()
  RVA=0x09AE8568  token=0x6000463  System.Void .ctor(Google.Protobuf.WellKnownTypes.FieldMask other)
  RVA=0x09AE773C  token=0x6000464  Google.Protobuf.WellKnownTypes.FieldMask Clone()
  RVA=0x09AE779C  token=0x6000466  System.Boolean Equals(System.Object other)
  RVA=0x09AE7800  token=0x6000467  System.Boolean Equals(Google.Protobuf.WellKnownTypes.FieldMask other)
  RVA=0x0638F5B8  token=0x6000468  System.Int32 GetHashCode()
  RVA=0x09AE8338  token=0x6000469  System.String ToString()
  RVA=0x04D52400  token=0x600046A  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09AE881C  token=0x600046B  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09AE76A8  token=0x600046C  System.Int32 CalculateSize()
  RVA=0x09AE7DAC  token=0x600046D  System.Void MergeFrom(Google.Protobuf.WellKnownTypes.FieldMask other)
  RVA=0x04D51840  token=0x600046E  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09AE874C  token=0x600046F  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09AE7FEC  token=0x6000470  System.String ToJson(System.Collections.Generic.IList<System.String> paths, System.Boolean diagnosticOnly)
  RVA=0x09AE7F9C  token=0x6000471  System.String ToDiagnosticString()
  RVA=0x09AE787C  token=0x6000472  Google.Protobuf.WellKnownTypes.FieldMask FromString(System.String value)
  RVA=-1  // generic def  token=0x6000473  Google.Protobuf.WellKnownTypes.FieldMask FromString(System.String value)
  RVA=-1  // generic def  token=0x6000474  Google.Protobuf.WellKnownTypes.FieldMask FromStringEnumerable(System.Collections.Generic.IEnumerable<System.String> paths)
  RVA=-1  // generic def  token=0x6000475  Google.Protobuf.WellKnownTypes.FieldMask FromFieldNumbers(System.Int32[] fieldNumbers)
  RVA=-1  // generic def  token=0x6000476  Google.Protobuf.WellKnownTypes.FieldMask FromFieldNumbers(System.Collections.Generic.IEnumerable<System.Int32> fieldNumbers)
  RVA=0x09AE7B14  token=0x6000477  System.Boolean IsPathValid(System.String input)
  RVA=-1  // generic def  token=0x6000478  System.Boolean IsValid(Google.Protobuf.WellKnownTypes.FieldMask fieldMask)
  RVA=0x09AE7B88  token=0x6000479  System.Boolean IsValid(Google.Protobuf.Reflection.MessageDescriptor descriptor, Google.Protobuf.WellKnownTypes.FieldMask fieldMask)
  RVA=-1  // generic def  token=0x600047A  System.Boolean IsValid(System.String path)
  RVA=0x09AE7CF8  token=0x600047B  System.Boolean IsValid(Google.Protobuf.Reflection.MessageDescriptor descriptor, System.String path)
  RVA=0x09AE7F34  token=0x600047C  Google.Protobuf.WellKnownTypes.FieldMask Normalize()
  RVA=0x09AE8384  token=0x600047D  Google.Protobuf.WellKnownTypes.FieldMask Union(Google.Protobuf.WellKnownTypes.FieldMask[] otherMasks)
  RVA=0x09AE7938  token=0x600047E  Google.Protobuf.WellKnownTypes.FieldMask Intersection(Google.Protobuf.WellKnownTypes.FieldMask additionalMask)
  RVA=0x09AE7E24  token=0x600047F  System.Void Merge(Google.Protobuf.IMessage source, Google.Protobuf.IMessage destination, Google.Protobuf.WellKnownTypes.FieldMask.MergeOptions options)
  RVA=0x09AE7EC0  token=0x6000480  System.Void Merge(Google.Protobuf.IMessage source, Google.Protobuf.IMessage destination)
  RVA=0x09AE842C  token=0x6000481  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.SourceContextReflection
TYPE:  static class
TOKEN: 0x2000074
SIZE:  0x10
FIELDS:
  private   static  Google.Protobuf.Reflection.FileDescriptordescriptor  // static @ 0x0
PROPERTIES:
  Descriptor  get=0x09AEC5C4
METHODS:
  RVA=0x09AEC26C  token=0x600048E  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.SourceContext
TYPE:  sealed class
TOKEN: 0x2000075
SIZE:  0x20
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.SourceContext>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    FileNameFieldNumber  // const
  private           System.String                   fileName_  // 0x18
PROPERTIES:
  Parser  get=0x09AEC9FC
  Descriptor  get=0x09AEC99C
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09AECAC8
  FileName  get=0x0385B100  set=0x09AECB08
METHODS:
  RVA=0x09AEC95C  token=0x6000492  System.Void .ctor()
  RVA=0x09AEC904  token=0x6000493  System.Void .ctor(Google.Protobuf.WellKnownTypes.SourceContext other)
  RVA=0x09AEC690  token=0x6000494  Google.Protobuf.WellKnownTypes.SourceContext Clone()
  RVA=0x09AEC6F0  token=0x6000497  System.Boolean Equals(System.Object other)
  RVA=0x0639C940  token=0x6000498  System.Boolean Equals(Google.Protobuf.WellKnownTypes.SourceContext other)
  RVA=0x0639C9F8  token=0x6000499  System.Int32 GetHashCode()
  RVA=0x09AEC7B4  token=0x600049A  System.String ToString()
  RVA=0x04D52400  token=0x600049B  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x0639CDC4  token=0x600049C  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09AEC614  token=0x600049D  System.Int32 CalculateSize()
  RVA=0x09AEC754  token=0x600049E  System.Void MergeFrom(Google.Protobuf.WellKnownTypes.SourceContext other)
  RVA=0x04D51840  token=0x600049F  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09AECA4C  token=0x60004A0  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09AEC800  token=0x60004A1  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.StructReflection
TYPE:  static class
TOKEN: 0x2000077
SIZE:  0x10
FIELDS:
  private   static  Google.Protobuf.Reflection.FileDescriptordescriptor  // static @ 0x0
PROPERTIES:
  Descriptor  get=0x09AED2E0
METHODS:
  RVA=0x09AECB70  token=0x60004A6  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.NullValue
TYPE:  sealed struct
TOKEN: 0x2000078
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Google.Protobuf.WellKnownTypes.NullValueNullValue  // const
METHODS:
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.Struct
TYPE:  sealed class
TOKEN: 0x2000079
SIZE:  0x20
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Struct>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    FieldsFieldNumber  // const
  private   static readonly Google.Protobuf.Collections.MapField.Codec<System.String,Google.Protobuf.WellKnownTypes.Value>_map_fields_codec  // static @ 0x8
  private   readonly Google.Protobuf.Collections.MapField<System.String,Google.Protobuf.WellKnownTypes.Value>fields_  // 0x18
PROPERTIES:
  Parser  get=0x09AED918
  Descriptor  get=0x09AED8B8
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09AEDADC
  Fields  get=0x0385B100
METHODS:
  RVA=0x09AED840  token=0x60004AA  System.Void .ctor()
  RVA=0x09AED7B8  token=0x60004AB  System.Void .ctor(Google.Protobuf.WellKnownTypes.Struct other)
  RVA=0x09AED3C4  token=0x60004AC  Google.Protobuf.WellKnownTypes.Struct Clone()
  RVA=0x09AED424  token=0x60004AE  System.Boolean Equals(System.Object other)
  RVA=0x09AED488  token=0x60004AF  System.Boolean Equals(Google.Protobuf.WellKnownTypes.Struct other)
  RVA=0x0638F5B8  token=0x60004B0  System.Int32 GetHashCode()
  RVA=0x09AED57C  token=0x60004B1  System.String ToString()
  RVA=0x04D52400  token=0x60004B2  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09AEDA38  token=0x60004B3  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09AED330  token=0x60004B4  System.Int32 CalculateSize()
  RVA=0x09AED504  token=0x60004B5  System.Void MergeFrom(Google.Protobuf.WellKnownTypes.Struct other)
  RVA=0x04D51840  token=0x60004B6  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09AED968  token=0x60004B7  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09AED5C8  token=0x60004B8  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.Value
TYPE:  sealed class
TOKEN: 0x200007B
SIZE:  0x28
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Value>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    NullValueFieldNumber  // const
  public    static  System.Int32                    NumberValueFieldNumber  // const
  public    static  System.Int32                    StringValueFieldNumber  // const
  public    static  System.Int32                    BoolValueFieldNumber  // const
  public    static  System.Int32                    StructValueFieldNumber  // const
  public    static  System.Int32                    ListValueFieldNumber  // const
  private           System.Object                   kind_  // 0x18
  private           Google.Protobuf.WellKnownTypes.Value.KindOneofCasekindCase_  // 0x20
PROPERTIES:
  Parser  get=0x09AF36D8
  Descriptor  get=0x09AF3590
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09AF3B48
  NullValue  get=0x09AF3644  set=0x09AF3C14
  NumberValue  get=0x09AF368C  set=0x09AF3C74
  StringValue  get=0x09AF3728  set=0x09AF3CD8
  BoolValue  get=0x09AF3548  set=0x09AF3B88
  StructValue  get=0x09AF379C  set=0x09AF3D48
  ListValue  get=0x09AF35F0  set=0x09AF3BEC
  KindCase  get=0x02B70F90
METHODS:
  RVA=0x041E1670  token=0x60004BF  System.Void .ctor()
  RVA=0x09AF340C  token=0x60004C0  System.Void .ctor(Google.Protobuf.WellKnownTypes.Value other)
  RVA=0x09AF2A20  token=0x60004C1  Google.Protobuf.WellKnownTypes.Value Clone()
  RVA=0x063C8830  token=0x60004CF  System.Void ClearKind()
  RVA=0x09AF2A80  token=0x60004D0  System.Boolean Equals(System.Object other)
  RVA=0x09AF2AE4  token=0x60004D1  System.Boolean Equals(Google.Protobuf.WellKnownTypes.Value other)
  RVA=0x09AF2F80  token=0x60004D2  System.Int32 GetHashCode()
  RVA=0x09AF32BC  token=0x60004D3  System.String ToString()
  RVA=0x04D52400  token=0x60004D4  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09AF3A1C  token=0x60004D5  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09AF28FC  token=0x60004D6  System.Int32 CalculateSize()
  RVA=0x09AF30D4  token=0x60004D7  System.Void MergeFrom(Google.Protobuf.WellKnownTypes.Value other)
  RVA=0x04D51840  token=0x60004D8  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09AF37F0  token=0x60004D9  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09AF2E60  token=0x60004DA  Google.Protobuf.WellKnownTypes.Value ForString(System.String value)
  RVA=0x09AF2E00  token=0x60004DB  Google.Protobuf.WellKnownTypes.Value ForNumber(System.Double value)
  RVA=0x09AF2C64  token=0x60004DC  Google.Protobuf.WellKnownTypes.Value ForBool(System.Boolean value)
  RVA=0x09AF2DAC  token=0x60004DD  Google.Protobuf.WellKnownTypes.Value ForNull()
  RVA=0x09AF2CC4  token=0x60004DE  Google.Protobuf.WellKnownTypes.Value ForList(Google.Protobuf.WellKnownTypes.Value[] values)
  RVA=0x09AF2EF0  token=0x60004DF  Google.Protobuf.WellKnownTypes.Value ForStruct(Google.Protobuf.WellKnownTypes.Struct value)
  RVA=0x09AF3308  token=0x60004E0  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.ListValue
TYPE:  sealed class
TOKEN: 0x200007E
SIZE:  0x20
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.ListValue>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    ValuesFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.WellKnownTypes.Value>_repeated_values_codec  // static @ 0x8
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.Value>values_  // 0x18
PROPERTIES:
  Parser  get=0x09AE8E68
  Descriptor  get=0x09AE8E08
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09AE902C
  Values  get=0x0385B100
METHODS:
  RVA=0x09AE8D90  token=0x60004E7  System.Void .ctor()
  RVA=0x09AE8D08  token=0x60004E8  System.Void .ctor(Google.Protobuf.WellKnownTypes.ListValue other)
  RVA=0x09AE8994  token=0x60004E9  Google.Protobuf.WellKnownTypes.ListValue Clone()
  RVA=0x09AE89F4  token=0x60004EB  System.Boolean Equals(System.Object other)
  RVA=0x09AE8A58  token=0x60004EC  System.Boolean Equals(Google.Protobuf.WellKnownTypes.ListValue other)
  RVA=0x0638F5B8  token=0x60004ED  System.Int32 GetHashCode()
  RVA=0x09AE8B4C  token=0x60004EE  System.String ToString()
  RVA=0x04D52400  token=0x60004EF  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09AE8F88  token=0x60004F0  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09AE8900  token=0x60004F1  System.Int32 CalculateSize()
  RVA=0x09AE8AD4  token=0x60004F2  System.Void MergeFrom(Google.Protobuf.WellKnownTypes.ListValue other)
  RVA=0x04D51840  token=0x60004F3  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09AE8EB8  token=0x60004F4  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09AE8B98  token=0x60004F5  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.TimeExtensions
TYPE:  static class
TOKEN: 0x2000080
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09AEDB68  token=0x60004F9  Google.Protobuf.WellKnownTypes.Timestamp ToTimestamp(System.DateTime dateTime)
  RVA=0x09AEDBB4  token=0x60004FA  Google.Protobuf.WellKnownTypes.Timestamp ToTimestamp(System.DateTimeOffset dateTimeOffset)
  RVA=0x09AEDB1C  token=0x60004FB  Google.Protobuf.WellKnownTypes.Duration ToDuration(System.TimeSpan timeSpan)
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.TimestampReflection
TYPE:  static class
TOKEN: 0x2000081
SIZE:  0x10
FIELDS:
  private   static  Google.Protobuf.Reflection.FileDescriptordescriptor  // static @ 0x0
PROPERTIES:
  Descriptor  get=0x09AEDF80
METHODS:
  RVA=0x09AEDC08  token=0x60004FD  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.Timestamp
TYPE:  sealed class
TOKEN: 0x2000082
SIZE:  0x28
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage Google.Protobuf.ICustomDiagnosticMessage System.IComparable`1
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Timestamp>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    SecondsFieldNumber  // const
  private           System.Int64                    seconds_  // 0x18
  public    static  System.Int32                    NanosFieldNumber  // const
  private           System.Int32                    nanos_  // 0x20
  private   static readonly System.DateTime                 UnixEpoch  // static @ 0x8
  private   static  System.Int64                    BclSecondsAtUnixEpoch  // const
  private   static  System.Int64                    UnixSecondsAtBclMaxValue  // const
  private   static  System.Int64                    UnixSecondsAtBclMinValue  // const
  private   static  System.Int32                    MaxNanos  // const
PROPERTIES:
  Parser  get=0x09AEEB8C
  Descriptor  get=0x09AEEAF8
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09AEF0CC
  Seconds  get=0x0385B100  set=0x011EC9D0
  Nanos  get=0x02B70F90  set=0x04D86330
METHODS:
  RVA=0x041E1670  token=0x6000501  System.Void .ctor()
  RVA=0x0637B49C  token=0x6000502  System.Void .ctor(Google.Protobuf.WellKnownTypes.Timestamp other)
  RVA=0x09AEE06C  token=0x6000503  Google.Protobuf.WellKnownTypes.Timestamp Clone()
  RVA=0x09AEE160  token=0x6000508  System.Boolean Equals(System.Object other)
  RVA=0x0637BDB8  token=0x6000509  System.Boolean Equals(Google.Protobuf.WellKnownTypes.Timestamp other)
  RVA=0x0637BDEC  token=0x600050A  System.Int32 GetHashCode()
  RVA=0x09AEE94C  token=0x600050B  System.String ToString()
  RVA=0x04D52400  token=0x600050C  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x0637C018  token=0x600050D  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09AEDFD0  token=0x600050E  System.Int32 CalculateSize()
  RVA=0x09AEE3C0  token=0x600050F  System.Void MergeFrom(Google.Protobuf.WellKnownTypes.Timestamp other)
  RVA=0x04D51840  token=0x6000510  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x0637BF80  token=0x6000511  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09AEE394  token=0x6000512  System.Boolean IsNormalized(System.Int64 seconds, System.Int32 nanoseconds)
  RVA=0x09AEEE28  token=0x6000513  Google.Protobuf.WellKnownTypes.Duration op_Subtraction(Google.Protobuf.WellKnownTypes.Timestamp lhs, Google.Protobuf.WellKnownTypes.Timestamp rhs)
  RVA=0x09AEEBDC  token=0x6000514  Google.Protobuf.WellKnownTypes.Timestamp op_Addition(Google.Protobuf.WellKnownTypes.Timestamp lhs, Google.Protobuf.WellKnownTypes.Duration rhs)
  RVA=0x09AEEF74  token=0x6000515  Google.Protobuf.WellKnownTypes.Timestamp op_Subtraction(Google.Protobuf.WellKnownTypes.Timestamp lhs, Google.Protobuf.WellKnownTypes.Duration rhs)
  RVA=0x09AEE55C  token=0x6000516  System.DateTime ToDateTime()
  RVA=0x09AEE4E4  token=0x6000517  System.DateTimeOffset ToDateTimeOffset()
  RVA=0x09AEE22C  token=0x6000518  Google.Protobuf.WellKnownTypes.Timestamp FromDateTime(System.DateTime dateTime)
  RVA=0x09AEE1C4  token=0x6000519  Google.Protobuf.WellKnownTypes.Timestamp FromDateTimeOffset(System.DateTimeOffset dateTimeOffset)
  RVA=0x09AEE458  token=0x600051A  Google.Protobuf.WellKnownTypes.Timestamp Normalize(System.Int64 seconds, System.Int32 nanoseconds)
  RVA=0x09AEE6DC  token=0x600051B  System.String ToJson(System.Int64 seconds, System.Int32 nanoseconds, System.Boolean diagnosticOnly)
  RVA=0x09AEE0CC  token=0x600051C  System.Int32 CompareTo(Google.Protobuf.WellKnownTypes.Timestamp other)
  RVA=0x09AEEE08  token=0x600051D  System.Boolean op_LessThan(Google.Protobuf.WellKnownTypes.Timestamp a, Google.Protobuf.WellKnownTypes.Timestamp b)
  RVA=0x09AEED6C  token=0x600051E  System.Boolean op_GreaterThan(Google.Protobuf.WellKnownTypes.Timestamp a, Google.Protobuf.WellKnownTypes.Timestamp b)
  RVA=0x09AEEDE4  token=0x600051F  System.Boolean op_LessThanOrEqual(Google.Protobuf.WellKnownTypes.Timestamp a, Google.Protobuf.WellKnownTypes.Timestamp b)
  RVA=0x09AEED48  token=0x6000520  System.Boolean op_GreaterThanOrEqual(Google.Protobuf.WellKnownTypes.Timestamp a, Google.Protobuf.WellKnownTypes.Timestamp b)
  RVA=0x09AEED2C  token=0x6000521  System.Boolean op_Equality(Google.Protobuf.WellKnownTypes.Timestamp a, Google.Protobuf.WellKnownTypes.Timestamp b)
  RVA=0x09AEED90  token=0x6000522  System.Boolean op_Inequality(Google.Protobuf.WellKnownTypes.Timestamp a, Google.Protobuf.WellKnownTypes.Timestamp b)
  RVA=0x09AEE684  token=0x6000523  System.String ToDiagnosticString()
  RVA=0x09AEE998  token=0x6000524  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.TypeReflection
TYPE:  static class
TOKEN: 0x2000084
SIZE:  0x10
FIELDS:
  private   static  Google.Protobuf.Reflection.FileDescriptordescriptor  // static @ 0x0
PROPERTIES:
  Descriptor  get=0x09AEFEA0
METHODS:
  RVA=0x09AEF10C  token=0x6000529  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.Syntax
TYPE:  sealed struct
TOKEN: 0x2000085
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Google.Protobuf.WellKnownTypes.SyntaxProto2  // const
  public    static  Google.Protobuf.WellKnownTypes.SyntaxProto3  // const
METHODS:
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.Type
TYPE:  sealed class
TOKEN: 0x2000086
SIZE:  0x48
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Type>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    NameFieldNumber  // const
  private           System.String                   name_  // 0x18
  public    static  System.Int32                    FieldsFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.WellKnownTypes.Field>_repeated_fields_codec  // static @ 0x8
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.Field>fields_  // 0x20
  public    static  System.Int32                    OneofsFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<System.String>_repeated_oneofs_codec  // static @ 0x10
  private   readonly Google.Protobuf.Collections.RepeatedField<System.String>oneofs_  // 0x28
  public    static  System.Int32                    OptionsFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.WellKnownTypes.Option>_repeated_options_codec  // static @ 0x18
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.Option>options_  // 0x30
  public    static  System.Int32                    SourceContextFieldNumber  // const
  private           Google.Protobuf.WellKnownTypes.SourceContextsourceContext_  // 0x38
  public    static  System.Int32                    SyntaxFieldNumber  // const
  private           Google.Protobuf.WellKnownTypes.Syntaxsyntax_  // 0x40
PROPERTIES:
  Parser  get=0x09B06FAC
  Descriptor  get=0x09B06F4C
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B073A4
  Name  get=0x0385B100  set=0x09B073E4
  Fields  get=0x04D862C0
  Oneofs  get=0x04D86240
  Options  get=0x011F36E0
  SourceContext  get=0x04D85A50  set=0x046A91B0
  Syntax  get=0x04D865E0  set=0x04D86610
METHODS:
  RVA=0x09B06CEC  token=0x600052D  System.Void .ctor()
  RVA=0x09B06E18  token=0x600052E  System.Void .ctor(Google.Protobuf.WellKnownTypes.Type other)
  RVA=0x09B06680  token=0x600052F  Google.Protobuf.WellKnownTypes.Type Clone()
  RVA=0x09B067EC  token=0x6000539  System.Boolean Equals(System.Object other)
  RVA=0x09B066E0  token=0x600053A  System.Boolean Equals(Google.Protobuf.WellKnownTypes.Type other)
  RVA=0x09B06850  token=0x600053B  System.Int32 GetHashCode()
  RVA=0x09B06A94  token=0x600053C  System.String ToString()
  RVA=0x04D52400  token=0x600053D  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09B0721C  token=0x600053E  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09B064E8  token=0x600053F  System.Int32 CalculateSize()
  RVA=0x09B06930  token=0x6000540  System.Void MergeFrom(Google.Protobuf.WellKnownTypes.Type other)
  RVA=0x04D51840  token=0x6000541  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09B06FFC  token=0x6000542  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09B06AE0  token=0x6000543  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.Field
TYPE:  sealed class
TOKEN: 0x2000088
SIZE:  0x58
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Field>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    KindFieldNumber  // const
  private           Google.Protobuf.WellKnownTypes.Field.Types.Kindkind_  // 0x18
  public    static  System.Int32                    CardinalityFieldNumber  // const
  private           Google.Protobuf.WellKnownTypes.Field.Types.Cardinalitycardinality_  // 0x1c
  public    static  System.Int32                    NumberFieldNumber  // const
  private           System.Int32                    number_  // 0x20
  public    static  System.Int32                    NameFieldNumber  // const
  private           System.String                   name_  // 0x28
  public    static  System.Int32                    TypeUrlFieldNumber  // const
  private           System.String                   typeUrl_  // 0x30
  public    static  System.Int32                    OneofIndexFieldNumber  // const
  private           System.Int32                    oneofIndex_  // 0x38
  public    static  System.Int32                    PackedFieldNumber  // const
  private           System.Boolean                  packed_  // 0x3c
  public    static  System.Int32                    OptionsFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.WellKnownTypes.Option>_repeated_options_codec  // static @ 0x8
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.Option>options_  // 0x40
  public    static  System.Int32                    JsonNameFieldNumber  // const
  private           System.String                   jsonName_  // 0x48
  public    static  System.Int32                    DefaultValueFieldNumber  // const
  private           System.String                   defaultValue_  // 0x50
PROPERTIES:
  Parser  get=0x09B01440
  Descriptor  get=0x09B013E0
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B018A4
  Kind  get=0x011EF5B0  set=0x011EF9B0
  Cardinality  get=0x04D86310  set=0x04D86320
  Number  get=0x02B70F90  set=0x04D86330
  Name  get=0x04D86240  set=0x09B019B4
  TypeUrl  get=0x011F36E0  set=0x09B01A1C
  OneofIndex  get=0x04D863B0  set=0x04D86430
  Packed  get=0x04D86BD0  set=0x04D86BE0
  Options  get=0x04D85A60
  JsonName  get=0x04D86200  set=0x09B0194C
  DefaultValue  get=0x04D86270  set=0x09B018E4
METHODS:
  RVA=0x09B0130C  token=0x600054A  System.Void .ctor()
  RVA=0x09B0121C  token=0x600054B  System.Void .ctor(Google.Protobuf.WellKnownTypes.Field other)
  RVA=0x09B00C34  token=0x600054C  Google.Protobuf.WellKnownTypes.Field Clone()
  RVA=0x09B00DA0  token=0x6000560  System.Boolean Equals(System.Object other)
  RVA=0x09B00C94  token=0x6000561  System.Boolean Equals(Google.Protobuf.WellKnownTypes.Field other)
  RVA=0x09B00E04  token=0x6000562  System.Int32 GetHashCode()
  RVA=0x09B01060  token=0x6000563  System.String ToString()
  RVA=0x04D52400  token=0x6000564  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09B016A4  token=0x6000565  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09B00A1C  token=0x6000566  System.Int32 CalculateSize()
  RVA=0x09B00F2C  token=0x6000567  System.Void MergeFrom(Google.Protobuf.WellKnownTypes.Field other)
  RVA=0x04D51840  token=0x6000568  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09B01490  token=0x6000569  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09B010AC  token=0x600056A  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.Enum
TYPE:  sealed class
TOKEN: 0x200008D
SIZE:  0x40
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Enum>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    NameFieldNumber  // const
  private           System.String                   name_  // 0x18
  public    static  System.Int32                    EnumvalueFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.WellKnownTypes.EnumValue>_repeated_enumvalue_codec  // static @ 0x8
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.EnumValue>enumvalue_  // 0x20
  public    static  System.Int32                    OptionsFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.WellKnownTypes.Option>_repeated_options_codec  // static @ 0x10
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.Option>options_  // 0x28
  public    static  System.Int32                    SourceContextFieldNumber  // const
  private           Google.Protobuf.WellKnownTypes.SourceContextsourceContext_  // 0x30
  public    static  System.Int32                    SyntaxFieldNumber  // const
  private           Google.Protobuf.WellKnownTypes.Syntaxsyntax_  // 0x38
PROPERTIES:
  Parser  get=0x09AFDDCC
  Descriptor  get=0x09AFDD6C
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09AFE138
  Name  get=0x0385B100  set=0x09AFE178
  Enumvalue  get=0x04D862C0
  Options  get=0x04D86240
  SourceContext  get=0x011F36E0  set=0x05396200
  Syntax  get=0x04D863B0  set=0x04D86430
METHODS:
  RVA=0x09AFDB88  token=0x6000571  System.Void .ctor()
  RVA=0x09AFDC68  token=0x6000572  System.Void .ctor(Google.Protobuf.WellKnownTypes.Enum other)
  RVA=0x09AFD5C8  token=0x6000573  Google.Protobuf.WellKnownTypes.Enum Clone()
  RVA=0x09AFD628  token=0x600057C  System.Boolean Equals(System.Object other)
  RVA=0x09AFD68C  token=0x600057D  System.Boolean Equals(Google.Protobuf.WellKnownTypes.Enum other)
  RVA=0x09AFD76C  token=0x600057E  System.Int32 GetHashCode()
  RVA=0x09AFD964  token=0x600057F  System.String ToString()
  RVA=0x04D52400  token=0x6000580  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09AFDFEC  token=0x6000581  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09AFD45C  token=0x6000582  System.Int32 CalculateSize()
  RVA=0x09AFD828  token=0x6000583  System.Void MergeFrom(Google.Protobuf.WellKnownTypes.Enum other)
  RVA=0x04D51840  token=0x6000584  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09AFDE1C  token=0x6000585  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09AFD9B0  token=0x6000586  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.EnumValue
TYPE:  sealed class
TOKEN: 0x200008F
SIZE:  0x30
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.EnumValue>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    NameFieldNumber  // const
  private           System.String                   name_  // 0x18
  public    static  System.Int32                    NumberFieldNumber  // const
  private           System.Int32                    number_  // 0x20
  public    static  System.Int32                    OptionsFieldNumber  // const
  private   static readonly Google.Protobuf.FieldCodec<Google.Protobuf.WellKnownTypes.Option>_repeated_options_codec  // static @ 0x8
  private   readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.Option>options_  // 0x28
PROPERTIES:
  Parser  get=0x09AFD14C
  Descriptor  get=0x09AFD0EC
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09AFD3B4
  Name  get=0x0385B100  set=0x09AFD3F4
  Number  get=0x02B70F90  set=0x04D86330
  Options  get=0x04D86240
METHODS:
  RVA=0x09AFD054  token=0x600058D  System.Void .ctor()
  RVA=0x09AFCFB4  token=0x600058E  System.Void .ctor(Google.Protobuf.WellKnownTypes.EnumValue other)
  RVA=0x09AFCBF8  token=0x600058F  Google.Protobuf.WellKnownTypes.EnumValue Clone()
  RVA=0x09AFCC58  token=0x6000595  System.Boolean Equals(System.Object other)
  RVA=0x09AFCCBC  token=0x6000596  System.Boolean Equals(Google.Protobuf.WellKnownTypes.EnumValue other)
  RVA=0x063CF728  token=0x6000597  System.Int32 GetHashCode()
  RVA=0x09AFCDF8  token=0x6000598  System.String ToString()
  RVA=0x04D52400  token=0x6000599  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x09AFD2C0  token=0x600059A  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09AFCAFC  token=0x600059B  System.Int32 CalculateSize()
  RVA=0x09AFCD54  token=0x600059C  System.Void MergeFrom(Google.Protobuf.WellKnownTypes.EnumValue other)
  RVA=0x04D51840  token=0x600059D  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09AFD19C  token=0x600059E  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09AFCE44  token=0x600059F  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.Option
TYPE:  sealed class
TOKEN: 0x2000091
SIZE:  0x28
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Option>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    NameFieldNumber  // const
  private           System.String                   name_  // 0x18
  public    static  System.Int32                    ValueFieldNumber  // const
  private           Google.Protobuf.WellKnownTypes.Anyvalue_  // 0x20
PROPERTIES:
  Parser  get=0x09B056BC
  Descriptor  get=0x09B0565C
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B05800
  Name  get=0x0385B100  set=0x09B05840
  Value  get=0x04D862C0  set=0x05390F40
METHODS:
  RVA=0x09B0561C  token=0x60005A6  System.Void .ctor()
  RVA=0x09B055A0  token=0x60005A7  System.Void .ctor(Google.Protobuf.WellKnownTypes.Option other)
  RVA=0x09B052B0  token=0x60005A8  Google.Protobuf.WellKnownTypes.Option Clone()
  RVA=0x09B05310  token=0x60005AD  System.Boolean Equals(System.Object other)
  RVA=0x063D1E6C  token=0x60005AE  System.Boolean Equals(Google.Protobuf.WellKnownTypes.Option other)
  RVA=0x063D1ED4  token=0x60005AF  System.Int32 GetHashCode()
  RVA=0x09B05450  token=0x60005B0  System.String ToString()
  RVA=0x04D52400  token=0x60005B1  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x063D2324  token=0x60005B2  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09B05204  token=0x60005B3  System.Int32 CalculateSize()
  RVA=0x09B05374  token=0x60005B4  System.Void MergeFrom(Google.Protobuf.WellKnownTypes.Option other)
  RVA=0x04D51840  token=0x60005B5  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09B0570C  token=0x60005B6  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09B0549C  token=0x60005B7  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.WrappersReflection
TYPE:  static class
TOKEN: 0x2000093
SIZE:  0x10
FIELDS:
  private   static  Google.Protobuf.Reflection.FileDescriptordescriptor  // static @ 0x0
  private   static  System.Int32                    WrapperValueFieldNumber  // const
PROPERTIES:
  Descriptor  get=0x09B09530
METHODS:
  RVA=0x09B089CC  token=0x60005BC  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.DoubleValue
TYPE:  sealed class
TOKEN: 0x2000094
SIZE:  0x20
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.DoubleValue>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    ValueFieldNumber  // const
  private           System.Double                   value_  // 0x18
PROPERTIES:
  Parser  get=0x09AFBB10
  Descriptor  get=0x09AFBAB0
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09AFBB60
  Value  get=0x04D86360  set=0x04D86370
METHODS:
  RVA=0x041E1670  token=0x60005C0  System.Void .ctor()
  RVA=0x0638E034  token=0x60005C1  System.Void .ctor(Google.Protobuf.WellKnownTypes.DoubleValue other)
  RVA=0x09AFB77C  token=0x60005C2  Google.Protobuf.WellKnownTypes.DoubleValue Clone()
  RVA=0x09AFB868  token=0x60005C5  System.Boolean Equals(System.Object other)
  RVA=0x09AFB7DC  token=0x60005C6  System.Boolean Equals(Google.Protobuf.WellKnownTypes.DoubleValue other)
  RVA=0x09AFB8CC  token=0x60005C7  System.Int32 GetHashCode()
  RVA=0x09AFB960  token=0x60005C8  System.String ToString()
  RVA=0x04D52400  token=0x60005C9  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x06619F80  token=0x60005CA  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x06619B4C  token=0x60005CB  System.Int32 CalculateSize()
  RVA=0x06619D6C  token=0x60005CC  System.Void MergeFrom(Google.Protobuf.WellKnownTypes.DoubleValue other)
  RVA=0x04D51840  token=0x60005CD  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x06619F0C  token=0x60005CE  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09AFB9AC  token=0x60005CF  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.FloatValue
TYPE:  sealed class
TOKEN: 0x2000096
SIZE:  0x20
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.FloatValue>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    ValueFieldNumber  // const
  private           System.Single                   value_  // 0x18
PROPERTIES:
  Parser  get=0x09B0422C
  Descriptor  get=0x09B041CC
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B0427C
  Value  get=0x04D86380  set=0x04D86390
METHODS:
  RVA=0x041E1670  token=0x60005D6  System.Void .ctor()
  RVA=0x063BE028  token=0x60005D7  System.Void .ctor(Google.Protobuf.WellKnownTypes.FloatValue other)
  RVA=0x09B03E94  token=0x60005D8  Google.Protobuf.WellKnownTypes.FloatValue Clone()
  RVA=0x09B03EF4  token=0x60005DB  System.Boolean Equals(System.Object other)
  RVA=0x09B03F58  token=0x60005DC  System.Boolean Equals(Google.Protobuf.WellKnownTypes.FloatValue other)
  RVA=0x09B03FE8  token=0x60005DD  System.Int32 GetHashCode()
  RVA=0x09B0407C  token=0x60005DE  System.String ToString()
  RVA=0x04D52400  token=0x60005DF  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x066536E8  token=0x60005E0  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x066532B4  token=0x60005E1  System.Int32 CalculateSize()
  RVA=0x066534D8  token=0x60005E2  System.Void MergeFrom(Google.Protobuf.WellKnownTypes.FloatValue other)
  RVA=0x04D51840  token=0x60005E3  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x06653674  token=0x60005E4  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09B040C8  token=0x60005E5  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.Int64Value
TYPE:  sealed class
TOKEN: 0x2000098
SIZE:  0x20
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Int64Value>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    ValueFieldNumber  // const
  private           System.Int64                    value_  // 0x18
PROPERTIES:
  Parser  get=0x09B04910
  Descriptor  get=0x09B048B0
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B04960
  Value  get=0x0385B100  set=0x011EC9D0
METHODS:
  RVA=0x041E1670  token=0x60005EC  System.Void .ctor()
  RVA=0x0638E034  token=0x60005ED  System.Void .ctor(Google.Protobuf.WellKnownTypes.Int64Value other)
  RVA=0x09B0469C  token=0x60005EE  Google.Protobuf.WellKnownTypes.Int64Value Clone()
  RVA=0x09B046FC  token=0x60005F1  System.Boolean Equals(System.Object other)
  RVA=0x0638DE34  token=0x60005F2  System.Boolean Equals(Google.Protobuf.WellKnownTypes.Int64Value other)
  RVA=0x0638DE60  token=0x60005F3  System.Int32 GetHashCode()
  RVA=0x09B04760  token=0x60005F4  System.String ToString()
  RVA=0x04D52400  token=0x60005F5  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x0638E194  token=0x60005F6  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09B0462C  token=0x60005F7  System.Int32 CalculateSize()
  RVA=0x0638DEA4  token=0x60005F8  System.Void MergeFrom(Google.Protobuf.WellKnownTypes.Int64Value other)
  RVA=0x04D51840  token=0x60005F9  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x0638E120  token=0x60005FA  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09B047AC  token=0x60005FB  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.UInt64Value
TYPE:  sealed class
TOKEN: 0x200009A
SIZE:  0x20
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.UInt64Value>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    ValueFieldNumber  // const
  private           System.UInt64                   value_  // 0x18
PROPERTIES:
  Parser  get=0x09B0893C
  Descriptor  get=0x09B088DC
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B0898C
  Value  get=0x0385B100  set=0x011EC9D0
METHODS:
  RVA=0x041E1670  token=0x6000602  System.Void .ctor()
  RVA=0x0638E034  token=0x6000603  System.Void .ctor(Google.Protobuf.WellKnownTypes.UInt64Value other)
  RVA=0x09B086C8  token=0x6000604  Google.Protobuf.WellKnownTypes.UInt64Value Clone()
  RVA=0x09B08728  token=0x6000607  System.Boolean Equals(System.Object other)
  RVA=0x0638DE34  token=0x6000608  System.Boolean Equals(Google.Protobuf.WellKnownTypes.UInt64Value other)
  RVA=0x063A2E14  token=0x6000609  System.Int32 GetHashCode()
  RVA=0x09B0878C  token=0x600060A  System.String ToString()
  RVA=0x04D52400  token=0x600060B  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x0638E194  token=0x600060C  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09B08658  token=0x600060D  System.Int32 CalculateSize()
  RVA=0x0638DEA4  token=0x600060E  System.Void MergeFrom(Google.Protobuf.WellKnownTypes.UInt64Value other)
  RVA=0x04D51840  token=0x600060F  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x0638E120  token=0x6000610  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09B087D8  token=0x6000611  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.Int32Value
TYPE:  sealed class
TOKEN: 0x200009C
SIZE:  0x20
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Int32Value>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    ValueFieldNumber  // const
  private           System.Int32                    value_  // 0x18
PROPERTIES:
  Parser  get=0x09B0459C
  Descriptor  get=0x09B0453C
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B045EC
  Value  get=0x011EF5B0  set=0x011EF9B0
METHODS:
  RVA=0x041E1670  token=0x6000618  System.Void .ctor()
  RVA=0x063BE028  token=0x6000619  System.Void .ctor(Google.Protobuf.WellKnownTypes.Int32Value other)
  RVA=0x09B04328  token=0x600061A  Google.Protobuf.WellKnownTypes.Int32Value Clone()
  RVA=0x09B04388  token=0x600061D  System.Boolean Equals(System.Object other)
  RVA=0x063BDE20  token=0x600061E  System.Boolean Equals(Google.Protobuf.WellKnownTypes.Int32Value other)
  RVA=0x063BDEB0  token=0x600061F  System.Int32 GetHashCode()
  RVA=0x09B043EC  token=0x6000620  System.String ToString()
  RVA=0x04D52400  token=0x6000621  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x011EF950  token=0x6000622  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09B042BC  token=0x6000623  System.Int32 CalculateSize()
  RVA=0x063BDEE8  token=0x6000624  System.Void MergeFrom(Google.Protobuf.WellKnownTypes.Int32Value other)
  RVA=0x04D51840  token=0x6000625  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x063BE0B4  token=0x6000626  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09B04438  token=0x6000627  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.UInt32Value
TYPE:  sealed class
TOKEN: 0x200009E
SIZE:  0x20
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.UInt32Value>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    ValueFieldNumber  // const
  private           System.UInt32                   value_  // 0x18
PROPERTIES:
  Parser  get=0x09B085C8
  Descriptor  get=0x09B08568
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B08618
  Value  get=0x011EF5B0  set=0x011EF9B0
METHODS:
  RVA=0x041E1670  token=0x600062E  System.Void .ctor()
  RVA=0x063BE028  token=0x600062F  System.Void .ctor(Google.Protobuf.WellKnownTypes.UInt32Value other)
  RVA=0x09B08354  token=0x6000630  Google.Protobuf.WellKnownTypes.UInt32Value Clone()
  RVA=0x09B083B4  token=0x6000633  System.Boolean Equals(System.Object other)
  RVA=0x063BDE20  token=0x6000634  System.Boolean Equals(Google.Protobuf.WellKnownTypes.UInt32Value other)
  RVA=0x063BDEB0  token=0x6000635  System.Int32 GetHashCode()
  RVA=0x09B08418  token=0x6000636  System.String ToString()
  RVA=0x04D52400  token=0x6000637  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x064B2684  token=0x6000638  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09B082E8  token=0x6000639  System.Int32 CalculateSize()
  RVA=0x063BDEE8  token=0x600063A  System.Void MergeFrom(Google.Protobuf.WellKnownTypes.UInt32Value other)
  RVA=0x04D51840  token=0x600063B  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x063BE0B4  token=0x600063C  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09B08464  token=0x600063D  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.BoolValue
TYPE:  sealed class
TOKEN: 0x20000A0
SIZE:  0x20
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.BoolValue>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    ValueFieldNumber  // const
  private           System.Boolean                  value_  // 0x18
PROPERTIES:
  Parser  get=0x09AF5D34
  Descriptor  get=0x09AF5CD4
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09AF5D84
  Value  get=0x04D86340  set=0x04D86350
METHODS:
  RVA=0x041E1670  token=0x6000644  System.Void .ctor()
  RVA=0x063D6A44  token=0x6000645  System.Void .ctor(Google.Protobuf.WellKnownTypes.BoolValue other)
  RVA=0x09AF5A58  token=0x6000646  Google.Protobuf.WellKnownTypes.BoolValue Clone()
  RVA=0x09AF5AB8  token=0x6000649  System.Boolean Equals(System.Object other)
  RVA=0x063D6870  token=0x600064A  System.Boolean Equals(Google.Protobuf.WellKnownTypes.BoolValue other)
  RVA=0x09AF5B1C  token=0x600064B  System.Int32 GetHashCode()
  RVA=0x09AF5B84  token=0x600064C  System.String ToString()
  RVA=0x04D52400  token=0x600064D  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x063D6B3C  token=0x600064E  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x063D677C  token=0x600064F  System.Int32 CalculateSize()
  RVA=0x063D6904  token=0x6000650  System.Void MergeFrom(Google.Protobuf.WellKnownTypes.BoolValue other)
  RVA=0x04D51840  token=0x6000651  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x063D6AD0  token=0x6000652  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09AF5BD0  token=0x6000653  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.StringValue
TYPE:  sealed class
TOKEN: 0x20000A2
SIZE:  0x20
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.StringValue>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    ValueFieldNumber  // const
  private           System.String                   value_  // 0x18
PROPERTIES:
  Parser  get=0x09B06374
  Descriptor  get=0x09B06314
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09B06440
  Value  get=0x0385B100  set=0x09B06480
METHODS:
  RVA=0x09B0627C  token=0x600065A  System.Void .ctor()
  RVA=0x09B062BC  token=0x600065B  System.Void .ctor(Google.Protobuf.WellKnownTypes.StringValue other)
  RVA=0x09B06008  token=0x600065C  Google.Protobuf.WellKnownTypes.StringValue Clone()
  RVA=0x09B06068  token=0x600065F  System.Boolean Equals(System.Object other)
  RVA=0x0639C940  token=0x6000660  System.Boolean Equals(Google.Protobuf.WellKnownTypes.StringValue other)
  RVA=0x0639C9F8  token=0x6000661  System.Int32 GetHashCode()
  RVA=0x09B0612C  token=0x6000662  System.String ToString()
  RVA=0x04D52400  token=0x6000663  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x0639CDC4  token=0x6000664  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09B05F8C  token=0x6000665  System.Int32 CalculateSize()
  RVA=0x09B060CC  token=0x6000666  System.Void MergeFrom(Google.Protobuf.WellKnownTypes.StringValue other)
  RVA=0x04D51840  token=0x6000667  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09B063C4  token=0x6000668  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09B06178  token=0x6000669  System.Void .cctor()
END_CLASS

CLASS: Google.Protobuf.WellKnownTypes.BytesValue
TYPE:  sealed class
TOKEN: 0x20000A4
SIZE:  0x20
IMPLEMENTS: Google.Protobuf.IMessage`1 Google.Protobuf.IMessage System.IEquatable`1 Google.Protobuf.IDeepCloneable`1 Google.Protobuf.IBufferMessage
FIELDS:
  private   static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.BytesValue>_parser  // static @ 0x0
  private           Google.Protobuf.UnknownFieldSet _unknownFields  // 0x10
  public    static  System.Int32                    ValueFieldNumber  // const
  private           Google.Protobuf.ByteString      value_  // 0x18
PROPERTIES:
  Parser  get=0x09AF6258
  Descriptor  get=0x09AF61F8
  pb::Google.Protobuf.IMessage.Descriptor  get=0x09AF6324
  Value  get=0x0385B100  set=0x09AF6364
METHODS:
  RVA=0x09AF61A4  token=0x6000670  System.Void .ctor()
  RVA=0x09AF614C  token=0x6000671  System.Void .ctor(Google.Protobuf.WellKnownTypes.BytesValue other)
  RVA=0x09AF5E44  token=0x6000672  Google.Protobuf.WellKnownTypes.BytesValue Clone()
  RVA=0x09AF5F38  token=0x6000675  System.Boolean Equals(System.Object other)
  RVA=0x09AF5EA4  token=0x6000676  System.Boolean Equals(Google.Protobuf.WellKnownTypes.BytesValue other)
  RVA=0x063E7124  token=0x6000677  System.Int32 GetHashCode()
  RVA=0x09AF5FFC  token=0x6000678  System.String ToString()
  RVA=0x04D52400  token=0x6000679  System.Void WriteTo(Google.Protobuf.CodedOutputStream output)
  RVA=0x063E745C  token=0x600067A  System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output)
  RVA=0x09AF5DC4  token=0x600067B  System.Int32 CalculateSize()
  RVA=0x09AF5F9C  token=0x600067C  System.Void MergeFrom(Google.Protobuf.WellKnownTypes.BytesValue other)
  RVA=0x04D51840  token=0x600067D  System.Void MergeFrom(Google.Protobuf.CodedInputStream input)
  RVA=0x09AF62A8  token=0x600067E  System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input)
  RVA=0x09AF6048  token=0x600067F  System.Void .cctor()
END_CLASS

CLASS: System.Diagnostics.CodeAnalysis.DynamicallyAccessedMembersAttribute
TYPE:  sealed class
TOKEN: 0x2000002
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  private   readonly System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes<MemberTypes>k__BackingField  // 0x10
PROPERTIES:
  MemberTypes  get=0x02B2E2D0
METHODS:
  RVA=0x014F51F0  token=0x6000001  System.Void .ctor(System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes memberTypes)
END_CLASS

CLASS: System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes
TYPE:  sealed struct
TOKEN: 0x2000003
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypesNone  // const
  public    static  System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypesPublicParameterlessConstructor  // const
  public    static  System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypesPublicConstructors  // const
  public    static  System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypesNonPublicConstructors  // const
  public    static  System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypesPublicMethods  // const
  public    static  System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypesNonPublicMethods  // const
  public    static  System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypesPublicFields  // const
  public    static  System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypesNonPublicFields  // const
  public    static  System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypesPublicNestedTypes  // const
  public    static  System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypesNonPublicNestedTypes  // const
  public    static  System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypesPublicProperties  // const
  public    static  System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypesNonPublicProperties  // const
  public    static  System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypesPublicEvents  // const
  public    static  System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypesNonPublicEvents  // const
  public    static  System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypesInterfaces  // const
  public    static  System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypesAll  // const
METHODS:
END_CLASS

CLASS: System.Diagnostics.CodeAnalysis.RequiresUnreferencedCodeAttribute
TYPE:  sealed class
TOKEN: 0x2000004
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  private   readonly System.String                   <Message>k__BackingField  // 0x10
  private           System.String                   <Url>k__BackingField  // 0x18
PROPERTIES:
  Message  get=0x02B2ECC0
  Url  get=0x0385B100  set=0x05392C40
METHODS:
  RVA=0x053908C0  token=0x6000003  System.Void .ctor(System.String message)
END_CLASS

CLASS: System.Diagnostics.CodeAnalysis.UnconditionalSuppressMessageAttribute
TYPE:  sealed class
TOKEN: 0x2000005
SIZE:  0x40
EXTENDS: System.Attribute
FIELDS:
  private   readonly System.String                   <Category>k__BackingField  // 0x10
  private   readonly System.String                   <CheckId>k__BackingField  // 0x18
  private           System.String                   <Scope>k__BackingField  // 0x20
  private           System.String                   <Target>k__BackingField  // 0x28
  private           System.String                   <MessageId>k__BackingField  // 0x30
  private           System.String                   <Justification>k__BackingField  // 0x38
PROPERTIES:
  Category  get=0x02B2ECC0
  CheckId  get=0x0385B100
  Scope  get=0x04D862C0  set=0x05390F40
  Target  get=0x04D86240  set=0x02FDB880
  MessageId  get=0x011F36E0  set=0x05396200
  Justification  get=0x04D85A50  set=0x046A91B0
METHODS:
  RVA=0x032AF420  token=0x6000007  System.Void .ctor(System.String category, System.String checkId)
END_CLASS

