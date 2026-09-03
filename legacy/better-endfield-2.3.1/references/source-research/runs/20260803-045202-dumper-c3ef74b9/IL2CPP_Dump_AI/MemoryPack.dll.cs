// ========================================================
// Dumped by @desirepro
// Assembly: MemoryPack.dll
// Classes:  165
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

CLASS: MemoryPackRegister`1
TYPE:  class
TOKEN: 0x2000006
FIELDS:
  public    static  System.Func<System.Boolean>     s_initFunc  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000006  System.Void .ctor()
END_CLASS

CLASS: Check`1
TYPE:  static class
TOKEN: 0x2000018
FIELDS:
  public    static  System.Boolean                  registered  // static @ 0x0
METHODS:
END_CLASS

CLASS: Cache`1
TYPE:  static class
TOKEN: 0x2000019
FIELDS:
  private   static  MemoryPack.IMemoryPackFormatter<T>_formatter  // static @ 0x0
PROPERTIES:
  formatter  get=-1  // not resolved  set=-1  // not resolved
METHODS:
END_CLASS

CLASS: SerializerWriterThreadStaticState
TYPE:  sealed class
TOKEN: 0x2000029
SIZE:  0x20
FIELDS:
  public            MemoryPack.Internal.ReusableLinkedArrayBufferWriterBufferWriter  // 0x10
  public            MemoryPack.MemoryPackWriterOptionalStateOptionalState  // 0x18
METHODS:
  RVA=0x0A707DC8  token=0x6000128  System.Void .ctor()
  RVA=0x0A707D74  token=0x6000129  System.Void Init(MemoryPack.MemoryPackSerializerOptions options)
  RVA=0x0A707D94  token=0x600012A  System.Void Reset()
END_CLASS

CLASS: <DeserializeAsync>d__6`1
TYPE:  sealed struct
TOKEN: 0x200002A
IMPLEMENTS: System.Runtime.CompilerServices.IAsyncStateMachine
FIELDS:
  public            System.Int32                    <>1__state  // 0x0
  public            System.Runtime.CompilerServices.AsyncValueTaskMethodBuilder<T><>t__builder  // 0x0
  public            System.IO.Stream                stream  // 0x0
  public            System.Threading.CancellationTokencancellationToken  // 0x0
  public            MemoryPack.MemoryPackSerializerOptionsoptions  // 0x0
  private           MemoryPack.Internal.ReusableReadOnlySequenceBuilder<builder>5__2  // 0x0
  private           System.Byte[]                   <buffer>5__3  // 0x0
  private           System.Int32                    <offset>5__4  // 0x0
  private           System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.Int32><>u__1  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600012B  System.Void MoveNext()
  RVA=-1  // not resolved  token=0x600012C  System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine)
END_CLASS

CLASS: <DeserializeAsync>d__11
TYPE:  sealed struct
TOKEN: 0x200002B
SIZE:  0x88
IMPLEMENTS: System.Runtime.CompilerServices.IAsyncStateMachine
FIELDS:
  public            System.Int32                    <>1__state  // 0x10
  public            System.Runtime.CompilerServices.AsyncValueTaskMethodBuilder<System.Object><>t__builder  // 0x18
  public            System.IO.Stream                stream  // 0x40
  public            System.Threading.CancellationTokencancellationToken  // 0x48
  public            System.Type                     type  // 0x50
  public            MemoryPack.MemoryPackSerializerOptionsoptions  // 0x58
  private           MemoryPack.Internal.ReusableReadOnlySequenceBuilder<builder>5__2  // 0x60
  private           System.Byte[]                   <buffer>5__3  // 0x68
  private           System.Int32                    <offset>5__4  // 0x70
  private           System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.Int32><>u__1  // 0x78
METHODS:
  RVA=0x0A7096A0  token=0x600012D  System.Void MoveNext()
  RVA=0x0A70A1AC  token=0x600012E  System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine)
END_CLASS

CLASS: ReferenceEqualityComparer
TYPE:  sealed class
TOKEN: 0x2000030
SIZE:  0x10
IMPLEMENTS: System.Collections.Generic.IEqualityComparer`1
FIELDS:
  private   static readonly MemoryPack.MemoryPackWriterOptionalState.ReferenceEqualityComparer<Instance>k__BackingField  // static @ 0x0
PROPERTIES:
  Instance  get=0x0A7064B0
METHODS:
  RVA=0x041E1670  token=0x600014B  System.Void .ctor()
  RVA=0x0829C278  token=0x600014D  System.Boolean Equals(System.Object x, System.Object y)
  RVA=0x04DA3AC0  token=0x600014E  System.Int32 GetHashCode(System.Object obj)
  RVA=0x0A70644C  token=0x600014F  System.Void .cctor()
END_CLASS

CLASS: State
TYPE:  sealed struct
TOKEN: 0x200003C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  MemoryPack.Internal.ReusableLinkedArrayBufferWriter.Enumerator.StateFirstBuffer  // const
  public    static  MemoryPack.Internal.ReusableLinkedArrayBufferWriter.Enumerator.StateBuffersInit  // const
  public    static  MemoryPack.Internal.ReusableLinkedArrayBufferWriter.Enumerator.StateBuffersIterate  // const
  public    static  MemoryPack.Internal.ReusableLinkedArrayBufferWriter.Enumerator.StateCurrent  // const
  public    static  MemoryPack.Internal.ReusableLinkedArrayBufferWriter.Enumerator.StateEnd  // const
METHODS:
END_CLASS

CLASS: Enumerator
TYPE:  sealed struct
TOKEN: 0x200003B
SIZE:  0x50
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           MemoryPack.Internal.ReusableLinkedArrayBufferWriterparent  // 0x10
  private           MemoryPack.Internal.ReusableLinkedArrayBufferWriter.Enumerator.Statestate  // 0x18
  private           System.Memory<System.Byte>      current  // 0x20
  private           System.Collections.Generic.List.Enumerator<MemoryPack.Internal.BufferSegment>buffersEnumerator  // 0x30
PROPERTIES:
  Current  get=0x02B2D510
  System.Collections.IEnumerator.Current  get=0x0A7000B8
METHODS:
  RVA=0x0A700104  token=0x6000173  System.Void .ctor(MemoryPack.Internal.ReusableLinkedArrayBufferWriter parent)
  RVA=0x041E1670  token=0x6000176  System.Void Dispose()
  RVA=0x0A6FFEF8  token=0x6000177  System.Boolean MoveNext()
  RVA=0x0A70006C  token=0x6000178  System.Void Reset()
END_CLASS

CLASS: <WriteToAndResetAsync>d__19
TYPE:  sealed struct
TOKEN: 0x200003D
SIZE:  0x90
IMPLEMENTS: System.Runtime.CompilerServices.IAsyncStateMachine
FIELDS:
  public            System.Int32                    <>1__state  // 0x10
  public            System.Runtime.CompilerServices.AsyncValueTaskMethodBuilder<>t__builder  // 0x18
  public            MemoryPack.Internal.ReusableLinkedArrayBufferWriter<>4__this  // 0x38
  public            System.IO.Stream                stream  // 0x40
  public            System.Threading.CancellationTokencancellationToken  // 0x48
  private           System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<>u__1  // 0x50
  private           System.Collections.Generic.List.Enumerator<MemoryPack.Internal.BufferSegment><>7__wrap1  // 0x60
  private           MemoryPack.Internal.BufferSegment<item>5__3  // 0x80
METHODS:
  RVA=0x0A70A6E4  token=0x6000179  System.Void MoveNext()
  RVA=0x0A709694  token=0x600017A  System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine)
END_CLASS

CLASS: Segment
TYPE:  class
TOKEN: 0x2000041
SIZE:  0x38
EXTENDS: System.Buffers.ReadOnlySequenceSegment`1
FIELDS:
  private           System.Boolean                  returnToPool  // 0x30
METHODS:
  RVA=0x0A707D3C  token=0x600018B  System.Void .ctor()
  RVA=0x0A707C80  token=0x600018C  System.Void SetBuffer(System.ReadOnlyMemory<System.Byte> buffer, System.Boolean returnToPool)
  RVA=0x0A707B40  token=0x600018D  System.Void Reset()
  RVA=0x0A707CDC  token=0x600018E  System.Void SetRunningIndexAndNext(System.Int64 runningIndex, MemoryPack.Internal.ReusableReadOnlySequenceBuilder.Segment nextSegment)
END_CLASS

CLASS: Cache`1
TYPE:  static class
TOKEN: 0x2000043
FIELDS:
  public    static  System.Boolean                  IsReferenceOrNullable  // static @ 0x0
  public    static  System.Boolean                  IsUnmanagedSZArray  // static @ 0x0
  public    static  System.Int32                    UnmanagedSZArrayElementSize  // static @ 0x0
  public    static  System.Boolean                  IsFixedSizeMemoryPackable  // static @ 0x0
  public    static  System.Int32                    MemoryPackableFixedSize  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000193  System.Void .cctor()
END_CLASS

CLASS: TypeKind
TYPE:  sealed struct
TOKEN: 0x2000044
SIZE:  0x11
FIELDS:
  public            System.Byte                     value__  // 0x10
  public    static  MemoryPack.Internal.TypeHelpers.TypeKindNone  // const
  public    static  MemoryPack.Internal.TypeHelpers.TypeKindUnmanagedSZArray  // const
  public    static  MemoryPack.Internal.TypeHelpers.TypeKindFixedSizeMemoryPackable  // const
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000066
FIELDS:
  public    static readonly MemoryPack.Formatters.DynamicUnionFormatter.<>c<T><>9  // static @ 0x0
  public    static  System.Func<System.ValueTuple<System.UInt16,System.Type>,System.Type><>9__2_0  // static @ 0x0
  public    static  System.Func<System.ValueTuple<System.UInt16,System.Type>,System.UInt16><>9__2_1  // static @ 0x0
  public    static  System.Func<System.ValueTuple<System.UInt16,System.Type>,System.UInt16><>9__2_2  // static @ 0x0
  public    static  System.Func<System.ValueTuple<System.UInt16,System.Type>,System.Type><>9__2_3  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60001DB  System.Void .cctor()
  RVA=-1  // not resolved  token=0x60001DC  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60001DD  System.Type <.ctor>b__2_0(System.ValueTuple<System.UInt16,System.Type> x)
  RVA=-1  // not resolved  token=0x60001DE  System.UInt16 <.ctor>b__2_1(System.ValueTuple<System.UInt16,System.Type> x)
  RVA=-1  // not resolved  token=0x60001DF  System.UInt16 <.ctor>b__2_2(System.ValueTuple<System.UInt16,System.Type> x)
  RVA=-1  // not resolved  token=0x60001E0  System.Type <.ctor>b__2_3(System.ValueTuple<System.UInt16,System.Type> x)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000086
SIZE:  0x10
FIELDS:
  public    static readonly MemoryPack.Formatters.BrotliStringFormatter.<>c<>9  // static @ 0x0
  public    static  System.Buffers.SpanAction<System.Char,System.ValueTuple<System.IntPtr,System.Int32,System.Int32,System.Runtime.CompilerServices.StrongBox<System.Int32>>><>9__10_1  // static @ 0x8
  public    static  System.Buffers.SpanAction<System.Char,System.ValueTuple<System.Buffers.ReadOnlySequence<System.Byte>,System.Int64,System.Int32,System.Runtime.CompilerServices.StrongBox<System.Int32>>><>9__10_0  // static @ 0x10
METHODS:
  RVA=0x0A70A680  token=0x6000238  System.Void .cctor()
  RVA=0x041E1670  token=0x6000239  System.Void .ctor()
  RVA=0x0A70A4A4  token=0x600023A  System.Void <Deserialize>b__10_1(System.Span<System.Char> stringSpan, System.ValueTuple<System.IntPtr,System.Int32,System.Int32,System.Runtime.CompilerServices.StrongBox<System.Int32>> state)
  RVA=0x0A70A1F8  token=0x600023B  System.Void <Deserialize>b__10_0(System.Span<System.Char> stringSpan, System.ValueTuple<System.Buffers.ReadOnlySequence<System.Byte>,System.Int64,System.Int32,System.Runtime.CompilerServices.StrongBox<System.Int32>> state)
END_CLASS

CLASS: <CopyToAsync>d__11
TYPE:  sealed struct
TOKEN: 0x20000A0
SIZE:  0xD8
IMPLEMENTS: System.Runtime.CompilerServices.IAsyncStateMachine
FIELDS:
  public            System.Int32                    <>1__state  // 0x10
  public            System.Runtime.CompilerServices.AsyncValueTaskMethodBuilder<>t__builder  // 0x18
  public            MemoryPack.Compression.BrotliCompressor<>4__this  // 0x38
  public            System.Int32                    bufferSize  // 0x40
  public            System.IO.Stream                stream  // 0x48
  public            System.Threading.CancellationTokencancellationToken  // 0x50
  private           System.IO.Compression.BrotliEncoder<encoder>5__2  // 0x58
  private           System.Byte[]                   <buffer>5__3  // 0x68
  private           MemoryPack.Internal.ReusableLinkedArrayBufferWriter.Enumerator<>7__wrap3  // 0x70
  private           System.Memory<System.Byte>      <source>5__5  // 0xb0
  private           System.Buffers.OperationStatus  <lastResult>5__6  // 0xc0
  private           System.Int32                    <bytesConsumed>5__7  // 0xc4
  private           System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<>u__1  // 0xc8
METHODS:
  RVA=0x0A708964  token=0x600027E  System.Void MoveNext()
  RVA=0x0A709694  token=0x600027F  System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine)
END_CLASS

CLASS: <PrivateImplementationDetails>
TYPE:  sealed class
TOKEN: 0x20000A5
SIZE:  0x10
FIELDS:
  private   static readonly System.Int32                    AD95131BC0B799C0B1AF477FB14FCF26A6A9F76079E48BF090ACB7E8367BFD0E  // static @ 0x0
  private   static readonly System.Int32                    DF3F619804A92FDB4057192DC43DD748EA778ADC52BC498CE80524C014B81119  // static @ 0x4
METHODS:
END_CLASS

CLASS: MemoryPack.MemoryPackableAttribute
TYPE:  sealed class
TOKEN: 0x2000008
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  private   readonly MemoryPack.GenerateType         <GenerateType>k__BackingField  // 0x10
  private   readonly MemoryPack.SerializeLayout      <SerializeLayout>k__BackingField  // 0x14
PROPERTIES:
  GenerateType  get=0x02B2E2D0
  SerializeLayout  get=0x04D882A0
METHODS:
  RVA=0x0A7062E4  token=0x600000A  System.Void .ctor(MemoryPack.GenerateType generateType)
  RVA=0x04DA3AB0  token=0x600000B  System.Void .ctor(MemoryPack.SerializeLayout serializeLayout)
  RVA=0x04D88290  token=0x600000C  System.Void .ctor(MemoryPack.GenerateType generateType, MemoryPack.SerializeLayout serializeLayout)
END_CLASS

CLASS: MemoryPack.GenerateType
TYPE:  sealed struct
TOKEN: 0x2000009
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  MemoryPack.GenerateType         Object  // const
  public    static  MemoryPack.GenerateType         VersionTolerant  // const
  public    static  MemoryPack.GenerateType         CircularReference  // const
  public    static  MemoryPack.GenerateType         Collection  // const
  public    static  MemoryPack.GenerateType         NoGenerate  // const
METHODS:
END_CLASS

CLASS: MemoryPack.SerializeLayout
TYPE:  sealed struct
TOKEN: 0x200000A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  MemoryPack.SerializeLayout      Sequential  // const
  public    static  MemoryPack.SerializeLayout      Explicit  // const
METHODS:
END_CLASS

CLASS: MemoryPack.MemoryPackUnionAttribute
TYPE:  sealed class
TOKEN: 0x200000B
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  private   readonly System.UInt16                   <Tag>k__BackingField  // 0x10
  private   readonly System.Type                     <Type>k__BackingField  // 0x18
PROPERTIES:
  Tag  get=0x04D96190
  Type  get=0x0385B100
METHODS:
  RVA=0x0A704CE0  token=0x600000F  System.Void .ctor(System.UInt16 tag, System.Type type)
END_CLASS

CLASS: MemoryPack.MemoryPackUnionFormatterAttribute
TYPE:  sealed class
TOKEN: 0x200000C
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  private   readonly System.Type                     <Type>k__BackingField  // 0x10
PROPERTIES:
  Type  get=0x02B2ECC0
METHODS:
  RVA=0x053908C0  token=0x6000011  System.Void .ctor(System.Type type)
END_CLASS

CLASS: MemoryPack.MemoryPackAllowSerializeAttribute
TYPE:  sealed class
TOKEN: 0x200000D
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000012  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.MemoryPackOrderAttribute
TYPE:  sealed class
TOKEN: 0x200000E
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  private   readonly System.Int32                    <Order>k__BackingField  // 0x10
PROPERTIES:
  Order  get=0x02B2E2D0
METHODS:
  RVA=0x014F51F0  token=0x6000014  System.Void .ctor(System.Int32 order)
END_CLASS

CLASS: MemoryPack.MemoryPackIgnoreAttribute
TYPE:  sealed class
TOKEN: 0x200000F
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000015  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.MemoryPackIncludeAttribute
TYPE:  sealed class
TOKEN: 0x2000010
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000016  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.MemoryPackConstructorAttribute
TYPE:  sealed class
TOKEN: 0x2000011
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000017  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.MemoryPackOnSerializingAttribute
TYPE:  sealed class
TOKEN: 0x2000012
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000018  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.MemoryPackOnSerializedAttribute
TYPE:  sealed class
TOKEN: 0x2000013
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000019  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.MemoryPackOnDeserializingAttribute
TYPE:  sealed class
TOKEN: 0x2000014
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600001A  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.MemoryPackOnDeserializedAttribute
TYPE:  sealed class
TOKEN: 0x2000015
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600001B  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.GenerateTypeScriptAttribute
TYPE:  sealed class
TOKEN: 0x2000016
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600001C  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.MemoryPackFormatterProvider
TYPE:  static class
TOKEN: 0x2000017
SIZE:  0x10
FIELDS:
  private   static readonly System.Collections.Generic.Dictionary<System.Type,System.Type>ArrayLikeFormatters  // static @ 0x0
  private   static readonly System.Collections.Generic.Dictionary<System.Type,System.Type>CollectionFormatters  // static @ 0x8
  private   static readonly System.Collections.Generic.Dictionary<System.Type,System.Type>InterfaceCollectionFormatters  // static @ 0x10
  private   static readonly System.Collections.Concurrent.ConcurrentDictionary<System.Type,MemoryPack.IMemoryPackFormatter>formatters  // static @ 0x18
  private   static readonly System.Collections.Concurrent.ConcurrentDictionary<System.Type,System.Type>genericFormatterFactory  // static @ 0x20
  private   static readonly System.Collections.Concurrent.ConcurrentDictionary<System.Type,System.Type>genericCollectionFormatterFactory  // static @ 0x28
  private   static readonly System.Collections.Generic.Dictionary<System.Type,System.Type>KnownGenericTypeFormatters  // static @ 0x30
  private   static  System.Object                   lockObj  // static @ 0x38
METHODS:
  RVA=0x039D8040  token=0x600001D  System.Void RegisterInitialFormatters()
  RVA=0x039D94A0  token=0x600001E  System.Void .cctor()
  RVA=-1  // generic def  token=0x600001F  System.Boolean IsRegistered()
  RVA=-1  // generic def  token=0x6000020  System.Void Register(MemoryPack.IMemoryPackFormatter<T> formatter)
  RVA=0x0A700E08  token=0x6000021  System.Void RegisterGenericType(System.Type genericType, System.Type genericFormatterType)
  RVA=-1  // generic def  token=0x6000022  System.Void RegisterCollection()
  RVA=0x0A700BB0  token=0x6000023  System.Void RegisterCollection(System.Type genericCollectionType)
  RVA=-1  // generic def  token=0x6000024  System.Void RegisterSet()
  RVA=0x0A700F20  token=0x6000025  System.Void RegisterSet(System.Type genericSetType)
  RVA=-1  // generic def  token=0x6000026  System.Void RegisterDictionary()
  RVA=0x0A700CDC  token=0x6000027  System.Void RegisterDictionary(System.Type genericDictionaryType)
  RVA=-1  // generic def  token=0x6000028  MemoryPack.IMemoryPackFormatter<T> GetFormatter()
  RVA=0x0A7009AC  token=0x6000029  MemoryPack.IMemoryPackFormatter GetFormatter(System.Type type)
  RVA=0x0A70121C  token=0x600002A  System.Boolean TryInvokeRegisterFormatter(System.Type type)
  RVA=-1  // generic def  token=0x600002B  System.Void ForceRegister(MemoryPack.IMemoryPackFormatter<T> formatter)
  RVA=0x0A7005E0  token=0x600002C  System.Object CreateGenericFormatter(System.Type type, System.Boolean typeIsReferenceOrContainsReferences)
  RVA=0x0A70116C  token=0x600002D  System.Type TryCreateGenericFormatterType(System.Type type, System.Collections.Generic.IDictionary<System.Type,System.Type> knownTypes)
  RVA=0x0A70104C  token=0x600002E  System.Type TryCreateGenericCollectionFormatterType(System.Type type)
  RVA=0x0428B4C0  token=0x600002F  System.Void RegisterWellKnownTypesFormatters()
  RVA=-1  // generic def  token=0x6000030  System.Void UnityRegister()
END_CLASS

CLASS: MemoryPack.IMemoryPackFormatterRegister
TYPE:  interface
TOKEN: 0x200001A
FIELDS:
METHODS:
END_CLASS

CLASS: MemoryPack.IMemoryPackable`1
TYPE:  interface
TOKEN: 0x200001B
IMPLEMENTS: MemoryPack.IMemoryPackFormatterRegister
FIELDS:
METHODS:
END_CLASS

CLASS: MemoryPack.IMemoryPackFormatter
TYPE:  interface
TOKEN: 0x200001C
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000033  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Object& value)
END_CLASS

CLASS: MemoryPack.IMemoryPackFormatter`1
TYPE:  interface
TOKEN: 0x200001D
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000034  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, T& value)
END_CLASS

CLASS: MemoryPack.MemoryPackFormatter`1
TYPE:  abstract class
TOKEN: 0x200001E
IMPLEMENTS: MemoryPack.IMemoryPackFormatter`1 MemoryPack.IMemoryPackFormatter
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000035  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, T& value)
  RVA=-1  // not resolved  token=0x6000036  System.Void MemoryPack.IMemoryPackFormatter.Deserialize(MemoryPack.MemoryPackReader& reader, System.Object& value)
  RVA=-1  // not resolved  token=0x6000037  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.MemoryPackCode
TYPE:  static class
TOKEN: 0x200001F
SIZE:  0x10
FIELDS:
  public    static  System.Int32                    NullCollection  // const
  public    static  System.Byte                     WideTag  // const
  public    static  System.Byte                     ReferenceId  // const
  public    static  System.Byte                     Reserved1  // const
  public    static  System.Byte                     Reserved2  // const
  public    static  System.Byte                     Reserved3  // const
  public    static  System.Byte                     Reserved4  // const
  public    static  System.Byte                     Reserved5  // const
  public    static  System.Byte                     NullObject  // const
PROPERTIES:
  NullCollectionData  get=0x0A700528
  ZeroCollectionData  get=0x0A700584
METHODS:
END_CLASS

CLASS: MemoryPack.ErrorMemoryPackFormatter
TYPE:  sealed class
TOKEN: 0x2000020
SIZE:  0x20
IMPLEMENTS: MemoryPack.IMemoryPackFormatter
FIELDS:
  private   readonly System.Type                     type  // 0x10
  private   readonly System.String                   message  // 0x18
METHODS:
  RVA=0x03E8CD90  token=0x600003A  System.Void .ctor(System.Type type)
  RVA=0x032AF420  token=0x600003B  System.Void .ctor(System.Type type, System.String message)
  RVA=0x0A700138  token=0x600003C  System.Void Serialize(MemoryPack.MemoryPackWriter& writer, System.Object& value)
  RVA=0x0A700138  token=0x600003D  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Object& value)
  RVA=0x0A700144  token=0x600003E  System.Void Throw()
END_CLASS

CLASS: MemoryPack.ErrorMemoryPackFormatter`1
TYPE:  sealed class
TOKEN: 0x2000021
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
  private   readonly System.Exception                exception  // 0x0
  private   readonly System.String                   message  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600003F  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000040  System.Void .ctor(System.Exception exception)
  RVA=-1  // not resolved  token=0x6000041  System.Void .ctor(System.String message)
  RVA=-1  // not resolved  token=0x6000042  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, T& value)
  RVA=-1  // not resolved  token=0x6000043  System.Void Throw()
END_CLASS

CLASS: MemoryPack.MemoryPackReader
TYPE:  sealed struct
TOKEN: 0x2000022
SIZE:  0x60
FIELDS:
  private           System.Buffers.ReadOnlySequence<System.Byte>bufferSource  // 0x10
  private   readonly System.Int64                    totalLength  // 0x28
  private           System.ReadOnlySpan<System.Byte>bufferReference  // 0x30
  private           System.Int32                    bufferLength  // 0x40
  private           System.Byte[]                   rentBuffer  // 0x48
  private           System.Int32                    advancedCount  // 0x50
  private           System.Int32                    consumed  // 0x54
  private   readonly MemoryPack.MemoryPackReaderOptionalStateoptionalState  // 0x58
PROPERTIES:
  Consumed  get=0x04D86530
  Remaining  get=0x04DA3AA0
  OptionalState  get=0x04D86200
  Options  get=0x0A7034A4
METHODS:
  RVA=0x0A70338C  token=0x6000048  System.Void .ctor(System.Buffers.ReadOnlySequence<System.Byte>& sequence, MemoryPack.MemoryPackReaderOptionalState optionalState)
  RVA=0x03B70820  token=0x6000049  System.Void .ctor(System.ReadOnlySpan<System.Byte> buffer, MemoryPack.MemoryPackReaderOptionalState optionalState)
  RVA=0x0A701DB8  token=0x600004A  System.Byte& GetSpanReference(System.Int32 sizeHint)
  RVA=0x0A701828  token=0x600004B  System.Byte& GetNextSpan(System.Int32 sizeHint)
  RVA=0x0A7016E8  token=0x600004C  System.Void Advance(System.Int32 count)
  RVA=0x0A703050  token=0x600004D  System.Boolean TryAdvanceSequence(System.Int32 count)
  RVA=0x0A701BA4  token=0x600004E  System.Void GetRemainingSource(System.ReadOnlySpan<System.Byte>& singleSource, System.Buffers.ReadOnlySequence<System.Byte>& remainingSource)
  RVA=0x0A701770  token=0x600004F  System.Void Dispose()
  RVA=0x0A7017E0  token=0x6000050  MemoryPack.IMemoryPackFormatter GetFormatter(System.Type type)
  RVA=-1  // generic def  token=0x6000051  MemoryPack.IMemoryPackFormatter<T> GetFormatter()
  RVA=0x0A7032B4  token=0x6000052  System.Boolean TryReadObjectHeader(System.Byte& memberCount)
  RVA=0x0A7032F4  token=0x6000053  System.Boolean TryReadUnionHeader(System.UInt16& tag)
  RVA=0x0A703258  token=0x6000054  System.Boolean TryReadCollectionHeader(System.Int32& length)
  RVA=0x0A701E20  token=0x6000055  System.Boolean PeekIsNull()
  RVA=0x0A7031BC  token=0x6000056  System.Boolean TryPeekObjectHeader(System.Byte& memberCount)
  RVA=0x0A7031E0  token=0x6000057  System.Boolean TryPeekUnionHeader(System.UInt16& tag)
  RVA=0x0A703234  token=0x6000058  System.Boolean TryPeekUshort(System.UInt16& tag)
  RVA=0x0A70316C  token=0x6000059  System.Boolean TryPeekCollectionHeader(System.Int32& length)
  RVA=0x0A6FCE34  token=0x600005A  System.Boolean DangerousTryReadCollectionHeader(System.Int32& length)
  RVA=0x0A701E3C  token=0x600005B  System.String ReadString()
  RVA=0x0A701EA0  token=0x600005C  System.String ReadUtf16(System.Int32 length)
  RVA=0x02FEC5F0  token=0x600005D  System.String ReadUtf8(System.Int32 utf8Length)
  RVA=-1  // generic def  token=0x600005E  T1 ReadUnmanaged()
  RVA=-1  // generic def  token=0x600005F  System.Void ReadPackable(T& value)
  RVA=-1  // generic def  token=0x6000060  T ReadPackable()
  RVA=-1  // generic def  token=0x6000061  System.Void ReadValue(T& value)
  RVA=-1  // generic def  token=0x6000062  T ReadValue()
  RVA=0x0A701F70  token=0x6000063  System.Void ReadValue(System.Type type, System.Object& value)
  RVA=0x0A701FE0  token=0x6000064  System.Object ReadValue(System.Type type)
  RVA=-1  // generic def  token=0x6000065  System.Void ReadValueWithFormatter(TFormatter formatter, T& value)
  RVA=-1  // generic def  token=0x6000066  T ReadValueWithFormatter(TFormatter formatter)
  RVA=-1  // generic def  token=0x6000067  T[] ReadArray()
  RVA=-1  // generic def  token=0x6000068  System.Void ReadArray(T[]& value)
  RVA=-1  // generic def  token=0x6000069  System.Void ReadSpan(System.Span<T>& value)
  RVA=-1  // generic def  token=0x600006A  T[] ReadPackableArray()
  RVA=-1  // generic def  token=0x600006B  System.Void ReadPackableArray(T[]& value)
  RVA=-1  // generic def  token=0x600006C  System.Void ReadPackableSpan(System.Span<T>& value)
  RVA=-1  // generic def  token=0x600006D  T[] ReadUnmanagedArray()
  RVA=-1  // generic def  token=0x600006E  System.Void ReadUnmanagedArray(T[]& value)
  RVA=-1  // generic def  token=0x600006F  System.Void ReadUnmanagedSpan(System.Span<T>& value)
  RVA=-1  // generic def  token=0x6000070  T[] DangerousReadUnmanagedArray()
  RVA=-1  // generic def  token=0x6000071  System.Void DangerousReadUnmanagedArray(T[]& value)
  RVA=-1  // generic def  token=0x6000072  System.Void DangerousReadUnmanagedSpan(System.Span<T>& value)
  RVA=-1  // generic def  token=0x6000073  System.Void ReadSpanWithoutReadLengthHeader(System.Int32 length, System.Span<T>& value)
  RVA=-1  // generic def  token=0x6000074  System.Void ReadPackableSpanWithoutReadLengthHeader(System.Int32 length, System.Span<T>& value)
  RVA=-1  // generic def  token=0x6000075  System.Void DangerousReadUnmanagedSpanView(System.Boolean& isNull, System.ReadOnlySpan<System.Byte>& view)
  RVA=-1  // generic def  token=0x6000076  System.Void ReadUnmanaged(T1& value1)
  RVA=-1  // generic def  token=0x6000077  System.Void ReadUnmanaged(T1& value1, T2& value2)
  RVA=-1  // generic def  token=0x6000078  System.Void ReadUnmanaged(T1& value1, T2& value2, T3& value3)
  RVA=-1  // generic def  token=0x6000079  System.Void ReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4)
  RVA=-1  // generic def  token=0x600007A  System.Void ReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5)
  RVA=-1  // generic def  token=0x600007B  System.Void ReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6)
  RVA=-1  // generic def  token=0x600007C  System.Void ReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7)
  RVA=-1  // generic def  token=0x600007D  System.Void ReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8)
  RVA=-1  // generic def  token=0x600007E  System.Void ReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9)
  RVA=-1  // generic def  token=0x600007F  System.Void ReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10)
  RVA=-1  // generic def  token=0x6000080  System.Void ReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11)
  RVA=-1  // generic def  token=0x6000081  System.Void ReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12)
  RVA=-1  // generic def  token=0x6000082  System.Void ReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13)
  RVA=-1  // generic def  token=0x6000083  System.Void ReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13, T14& value14)
  RVA=-1  // generic def  token=0x6000084  System.Void ReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13, T14& value14, T15& value15)
  RVA=-1  // generic def  token=0x6000085  System.Void DangerousReadUnmanaged(T1& value1)
  RVA=-1  // generic def  token=0x6000086  System.Void DangerousReadUnmanaged(T1& value1, T2& value2)
  RVA=-1  // generic def  token=0x6000087  System.Void DangerousReadUnmanaged(T1& value1, T2& value2, T3& value3)
  RVA=-1  // generic def  token=0x6000088  System.Void DangerousReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4)
  RVA=-1  // generic def  token=0x6000089  System.Void DangerousReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5)
  RVA=-1  // generic def  token=0x600008A  System.Void DangerousReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6)
  RVA=-1  // generic def  token=0x600008B  System.Void DangerousReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7)
  RVA=-1  // generic def  token=0x600008C  System.Void DangerousReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8)
  RVA=-1  // generic def  token=0x600008D  System.Void DangerousReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9)
  RVA=-1  // generic def  token=0x600008E  System.Void DangerousReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10)
  RVA=-1  // generic def  token=0x600008F  System.Void DangerousReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11)
  RVA=-1  // generic def  token=0x6000090  System.Void DangerousReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12)
  RVA=-1  // generic def  token=0x6000091  System.Void DangerousReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13)
  RVA=-1  // generic def  token=0x6000092  System.Void DangerousReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13, T14& value14)
  RVA=-1  // generic def  token=0x6000093  System.Void DangerousReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13, T14& value14, T15& value15)
  RVA=0x0A702054  token=0x6000094  System.Byte ReadVarIntByte()
  RVA=0x0A702850  token=0x6000095  System.SByte ReadVarIntSByte()
  RVA=0x0A702AC4  token=0x6000096  System.UInt16 ReadVarIntUInt16()
  RVA=0x0A702260  token=0x6000097  System.Int16 ReadVarIntInt16()
  RVA=0x0A702CCC  token=0x6000098  System.UInt32 ReadVarIntUInt32()
  RVA=0x0A702494  token=0x6000099  System.Int32 ReadVarIntInt32()
  RVA=0x0A702EA0  token=0x600009A  System.UInt64 ReadVarIntUInt64()
  RVA=0x0A70267C  token=0x600009B  System.Int64 ReadVarIntInt64()
END_CLASS

CLASS: MemoryPack.MemoryPackReaderOptionalStatePool
TYPE:  static class
TOKEN: 0x2000023
SIZE:  0x10
FIELDS:
  private   static readonly System.Collections.Concurrent.ConcurrentQueue<MemoryPack.MemoryPackReaderOptionalState>queue  // static @ 0x0
METHODS:
  RVA=0x0A701380  token=0x600009C  MemoryPack.MemoryPackReaderOptionalState Rent(MemoryPack.MemoryPackSerializerOptions options)
  RVA=0x0A701454  token=0x600009D  System.Void Return(MemoryPack.MemoryPackReaderOptionalState state)
  RVA=0x0A7014D0  token=0x600009E  System.Void .cctor()
END_CLASS

CLASS: MemoryPack.MemoryPackReaderOptionalState
TYPE:  sealed class
TOKEN: 0x2000024
SIZE:  0x20
IMPLEMENTS: System.IDisposable
FIELDS:
  private   readonly System.Collections.Generic.Dictionary<System.UInt32,System.Object>refToObject  // 0x10
  private           MemoryPack.MemoryPackSerializerOptions<Options>k__BackingField  // 0x18
PROPERTIES:
  Options  get=0x0385B100  set=0x05392C40
METHODS:
  RVA=0x03B70420  token=0x60000A1  System.Void .ctor()
  RVA=0x03B70730  token=0x60000A2  System.Void Init(MemoryPack.MemoryPackSerializerOptions options)
  RVA=0x0A7015FC  token=0x60000A3  System.Object GetObjectReference(System.UInt32 id)
  RVA=0x0A70155C  token=0x60000A4  System.Void AddObjectReference(System.UInt32 id, System.Object value)
  RVA=0x03B70790  token=0x60000A5  System.Void Reset()
  RVA=0x0A70169C  token=0x60000A6  System.Void System.IDisposable.Dispose()
END_CLASS

CLASS: MemoryPack.VarIntCodes
TYPE:  static class
TOKEN: 0x2000025
SIZE:  0x10
FIELDS:
  public    static  System.Byte                     MaxSingleValue  // const
  public    static  System.SByte                    MinSingleValue  // const
  public    static  System.SByte                    Byte  // const
  public    static  System.SByte                    SByte  // const
  public    static  System.SByte                    UInt16  // const
  public    static  System.SByte                    Int16  // const
  public    static  System.SByte                    UInt32  // const
  public    static  System.SByte                    Int32  // const
  public    static  System.SByte                    UInt64  // const
  public    static  System.SByte                    Int64  // const
METHODS:
END_CLASS

CLASS: MemoryPack.MemoryPackWriter
TYPE:  sealed struct
TOKEN: 0x2000026
SIZE:  0x48
FIELDS:
  private   static  System.Int32                    DepthLimit  // const
  private           System.Buffers.IBufferWriter<System.Byte>bufferWriter  // 0x10
  private           System.Span<System.Byte>        bufferReference  // 0x18
  private           System.Int32                    bufferLength  // 0x28
  private           System.Int32                    advancedCount  // 0x2c
  private           System.Int32                    depth  // 0x30
  private           System.Int32                    writtenCount  // 0x34
  private   readonly System.Boolean                  serializeStringAsUtf8  // 0x38
  private   readonly MemoryPack.MemoryPackWriterOptionalStateoptionalState  // 0x40
PROPERTIES:
  WrittenCount  get=0x011F0020
  BufferLength  get=0x011EF5B0
  OptionalState  get=0x011F36E0
  Options  get=0x0A7062C8
METHODS:
  RVA=0x0A705F98  token=0x60000A7  System.Void WriteVarInt(System.Byte x)
  RVA=0x0A706014  token=0x60000A8  System.Void WriteVarInt(System.SByte x)
  RVA=0x0A705BE4  token=0x60000A9  System.Void WriteVarInt(System.UInt16 x)
  RVA=0x0A705B14  token=0x60000AA  System.Void WriteVarInt(System.Int16 x)
  RVA=0x0A706090  token=0x60000AB  System.Void WriteVarInt(System.UInt32 x)
  RVA=0x0A705D48  token=0x60000AC  System.Void WriteVarInt(System.Int32 x)
  RVA=0x0A705C64  token=0x60000AD  System.Void WriteVarInt(System.UInt64 x)
  RVA=0x0A705E44  token=0x60000AE  System.Void WriteVarInt(System.Int64 x)
  RVA=0x0A706284  token=0x60000B3  System.Void .ctor(System.Buffers.IBufferWriter<System.Byte>& writer, MemoryPack.MemoryPackWriterOptionalState optionalState)
  RVA=0x0A70613C  token=0x60000B4  System.Void .ctor(System.Buffers.IBufferWriter<System.Byte>& writer, System.Byte[] firstBufferOfWriter, MemoryPack.MemoryPackWriterOptionalState optionalState)
  RVA=0x0A7061EC  token=0x60000B5  System.Void .ctor(System.Buffers.IBufferWriter<System.Byte>& writer, System.Span<System.Byte> firstBufferOfWriter, MemoryPack.MemoryPackWriterOptionalState optionalState)
  RVA=0x0A70539C  token=0x60000B6  System.Byte& GetSpanReference(System.Int32 sizeHint)
  RVA=0x0A7054B8  token=0x60000B7  System.Void RequestNewBuffer(System.Int32 sizeHint)
  RVA=0x0A70527C  token=0x60000B8  System.Void Advance(System.Int32 count)
  RVA=0x0A7052F0  token=0x60000B9  System.Void Flush()
  RVA=0x0A705354  token=0x60000BA  MemoryPack.IMemoryPackFormatter GetFormatter(System.Type type)
  RVA=-1  // generic def  token=0x60000BB  MemoryPack.IMemoryPackFormatter<T> GetFormatter()
  RVA=0x0A705400  token=0x60000BC  System.Int32 GetStringWriteLength(System.String value)
  RVA=-1  // generic def  token=0x60000BD  System.Int32 GetUnmanageArrayWriteLength(T[] value)
  RVA=0x0A7055A8  token=0x60000BE  System.Void WriteObjectHeader(System.Byte memberCount)
  RVA=0x0A705580  token=0x60000BF  System.Void WriteNullObjectHeader()
  RVA=0x0A7055EC  token=0x60000C0  System.Void WriteObjectReferenceId(System.UInt32 referenceId)
  RVA=0x0A70564C  token=0x60000C1  System.Void WriteUnionHeader(System.UInt16 tag)
  RVA=0x0A705580  token=0x60000C2  System.Void WriteNullUnionHeader()
  RVA=0x0A70554C  token=0x60000C3  System.Void WriteCollectionHeader(System.Int32 length)
  RVA=0x0A6FCEA0  token=0x60000C4  System.Void WriteNullCollectionHeader()
  RVA=0x0A70562C  token=0x60000C5  System.Void WriteString(System.String value)
  RVA=0x0A7057E8  token=0x60000C6  System.Void WriteUtf16(System.String value)
  RVA=0x0A7056AC  token=0x60000C7  System.Void WriteUtf16(System.ReadOnlySpan<System.Char> value)
  RVA=0x0A70593C  token=0x60000C8  System.Void WriteUtf8(System.String value)
  RVA=0x0A705A68  token=0x60000C9  System.Void WriteUtf8(System.ReadOnlySpan<System.Byte> utf8Value, System.Int32 utf16Length)
  RVA=-1  // generic def  token=0x60000CA  System.Void WriteUnmanaged(T1& value1)
  RVA=-1  // generic def  token=0x60000CB  System.Void WriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1)
  RVA=-1  // generic def  token=0x60000CC  System.Void WriteUnmanaged(T1& value1, T2& value2)
  RVA=-1  // generic def  token=0x60000CD  System.Void WriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2)
  RVA=-1  // generic def  token=0x60000CE  System.Void WriteUnmanaged(T1& value1, T2& value2, T3& value3)
  RVA=-1  // generic def  token=0x60000CF  System.Void WriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3)
  RVA=-1  // generic def  token=0x60000D0  System.Void WriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4)
  RVA=-1  // generic def  token=0x60000D1  System.Void WriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4)
  RVA=-1  // generic def  token=0x60000D2  System.Void WriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5)
  RVA=-1  // generic def  token=0x60000D3  System.Void WriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5)
  RVA=-1  // generic def  token=0x60000D4  System.Void WriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6)
  RVA=-1  // generic def  token=0x60000D5  System.Void WriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6)
  RVA=-1  // generic def  token=0x60000D6  System.Void WriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7)
  RVA=-1  // generic def  token=0x60000D7  System.Void WriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7)
  RVA=-1  // generic def  token=0x60000D8  System.Void WriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8)
  RVA=-1  // generic def  token=0x60000D9  System.Void WriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8)
  RVA=-1  // generic def  token=0x60000DA  System.Void WriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9)
  RVA=-1  // generic def  token=0x60000DB  System.Void WriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9)
  RVA=-1  // generic def  token=0x60000DC  System.Void WriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10)
  RVA=-1  // generic def  token=0x60000DD  System.Void WriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10)
  RVA=-1  // generic def  token=0x60000DE  System.Void WriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11)
  RVA=-1  // generic def  token=0x60000DF  System.Void WriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11)
  RVA=-1  // generic def  token=0x60000E0  System.Void WriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12)
  RVA=-1  // generic def  token=0x60000E1  System.Void WriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12)
  RVA=-1  // generic def  token=0x60000E2  System.Void WriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13)
  RVA=-1  // generic def  token=0x60000E3  System.Void WriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13)
  RVA=-1  // generic def  token=0x60000E4  System.Void WriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13, T14& value14)
  RVA=-1  // generic def  token=0x60000E5  System.Void WriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13, T14& value14)
  RVA=-1  // generic def  token=0x60000E6  System.Void WriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13, T14& value14, T15& value15)
  RVA=-1  // generic def  token=0x60000E7  System.Void WriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13, T14& value14, T15& value15)
  RVA=-1  // generic def  token=0x60000E8  System.Void DangerousWriteUnmanaged(T1& value1)
  RVA=-1  // generic def  token=0x60000E9  System.Void DangerousWriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1)
  RVA=-1  // generic def  token=0x60000EA  System.Void DangerousWriteUnmanaged(T1& value1, T2& value2)
  RVA=-1  // generic def  token=0x60000EB  System.Void DangerousWriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2)
  RVA=-1  // generic def  token=0x60000EC  System.Void DangerousWriteUnmanaged(T1& value1, T2& value2, T3& value3)
  RVA=-1  // generic def  token=0x60000ED  System.Void DangerousWriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3)
  RVA=-1  // generic def  token=0x60000EE  System.Void DangerousWriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4)
  RVA=-1  // generic def  token=0x60000EF  System.Void DangerousWriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4)
  RVA=-1  // generic def  token=0x60000F0  System.Void DangerousWriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5)
  RVA=-1  // generic def  token=0x60000F1  System.Void DangerousWriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5)
  RVA=-1  // generic def  token=0x60000F2  System.Void DangerousWriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6)
  RVA=-1  // generic def  token=0x60000F3  System.Void DangerousWriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6)
  RVA=-1  // generic def  token=0x60000F4  System.Void DangerousWriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7)
  RVA=-1  // generic def  token=0x60000F5  System.Void DangerousWriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7)
  RVA=-1  // generic def  token=0x60000F6  System.Void DangerousWriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8)
  RVA=-1  // generic def  token=0x60000F7  System.Void DangerousWriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8)
  RVA=-1  // generic def  token=0x60000F8  System.Void DangerousWriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9)
  RVA=-1  // generic def  token=0x60000F9  System.Void DangerousWriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9)
  RVA=-1  // generic def  token=0x60000FA  System.Void DangerousWriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10)
  RVA=-1  // generic def  token=0x60000FB  System.Void DangerousWriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10)
  RVA=-1  // generic def  token=0x60000FC  System.Void DangerousWriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11)
  RVA=-1  // generic def  token=0x60000FD  System.Void DangerousWriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11)
  RVA=-1  // generic def  token=0x60000FE  System.Void DangerousWriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12)
  RVA=-1  // generic def  token=0x60000FF  System.Void DangerousWriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12)
  RVA=-1  // generic def  token=0x6000100  System.Void DangerousWriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13)
  RVA=-1  // generic def  token=0x6000101  System.Void DangerousWriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13)
  RVA=-1  // generic def  token=0x6000102  System.Void DangerousWriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13, T14& value14)
  RVA=-1  // generic def  token=0x6000103  System.Void DangerousWriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13, T14& value14)
  RVA=-1  // generic def  token=0x6000104  System.Void DangerousWriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13, T14& value14, T15& value15)
  RVA=-1  // generic def  token=0x6000105  System.Void DangerousWriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13, T14& value14, T15& value15)
END_CLASS

CLASS: MemoryPack.MemoryPackSerializationException
TYPE:  class
TOKEN: 0x2000027
SIZE:  0x90
EXTENDS: System.Exception
FIELDS:
METHODS:
  RVA=0x039985D0  token=0x6000106  System.Void .ctor(System.String message)
  RVA=0x0A703F80  token=0x6000107  System.Void .ctor(System.String message, System.Exception innerException)
  RVA=0x0A703B88  token=0x6000108  System.Void ThrowMessage(System.String message)
  RVA=0x0A703974  token=0x6000109  System.Void ThrowInvalidPropertyCount(System.Byte expected, System.Byte actual)
  RVA=0x03998300  token=0x600010A  System.Void ThrowInvalidPropertyCount(System.Type type, System.Byte expected, System.Byte actual)
  RVA=0x0A7038BC  token=0x600010B  System.Void ThrowInvalidCollection()
  RVA=0x0A703A28  token=0x600010C  System.Void ThrowInvalidRange(System.Int32 expected, System.Int32 actual)
  RVA=0x0A703860  token=0x600010D  System.Void ThrowInvalidAdvance()
  RVA=0x0A703E94  token=0x600010E  System.Void ThrowSequenceReachedEnd()
  RVA=0x0A703EF0  token=0x600010F  System.Void ThrowWriteInvalidMemberCount(System.Byte memberCount)
  RVA=0x0A7037D0  token=0x6000110  System.Void ThrowInsufficientBufferUnless(System.Int32 length)
  RVA=0x0A703D0C  token=0x6000111  System.Void ThrowNotRegisteredInProvider(System.Type type)
  RVA=0x0A703E04  token=0x6000112  System.Void ThrowRegisterInProviderFailed(System.Type type, System.Exception innerException)
  RVA=0x0A703BE0  token=0x6000113  System.Void ThrowNotFoundInUnionType(System.Type actualType, System.Type baseType)
  RVA=0x0A703ADC  token=0x6000114  System.Void ThrowInvalidTag(System.UInt16 tag, System.Type baseType)
  RVA=0x0A703D90  token=0x6000115  System.Void ThrowReachedDepthLimit(System.Type type)
  RVA=0x0A703918  token=0x6000116  System.Void ThrowInvalidConcurrrentCollectionOperation()
  RVA=0x0A7036BC  token=0x6000117  System.Void ThrowDeserializeObjectIsNull(System.String target)
  RVA=0x0A703740  token=0x6000118  System.Void ThrowFailedEncoding(System.Buffers.OperationStatus status)
  RVA=0x0A703578  token=0x6000119  System.Void ThrowCompressionFailed(System.Buffers.OperationStatus status)
  RVA=0x0A70351C  token=0x600011A  System.Void ThrowCompressionFailed()
  RVA=0x0A7034C0  token=0x600011B  System.Void ThrowAlreadyDecompressed()
  RVA=0x0A703608  token=0x600011C  System.Void ThrowDecompressionSizeLimitExceeded(System.Int32 limit, System.Int32 size)
END_CLASS

CLASS: MemoryPack.MemoryPackSerializer
TYPE:  static class
TOKEN: 0x2000028
SIZE:  0x10
FIELDS:
  private   static  MemoryPack.MemoryPackReaderOptionalStatethreadStaticReaderOptionalState  // static @ 0xffffffff
  private   static  MemoryPack.MemoryPackSerializer.SerializerWriterThreadStaticStatethreadStaticState  // static @ 0xffffffff
  private   static  MemoryPack.MemoryPackWriterOptionalStatethreadStaticWriterOptionalState  // static @ 0xffffffff
METHODS:
  RVA=-1  // generic def  token=0x600011D  T Deserialize(System.ReadOnlySpan<System.Byte> buffer, MemoryPack.MemoryPackSerializerOptions options)
  RVA=-1  // generic def  token=0x600011E  System.Int32 Deserialize(System.ReadOnlySpan<System.Byte> buffer, T& value, MemoryPack.MemoryPackSerializerOptions options)
  RVA=-1  // generic def  token=0x600011F  T Deserialize(System.Buffers.ReadOnlySequence<System.Byte>& buffer, MemoryPack.MemoryPackSerializerOptions options)
  RVA=-1  // generic def  token=0x6000120  System.Int32 Deserialize(System.Buffers.ReadOnlySequence<System.Byte>& buffer, T& value, MemoryPack.MemoryPackSerializerOptions options)
  RVA=0x0A70462C  token=0x6000121  System.Int32 Deserialize(System.Buffers.ReadOnlySequence<System.Byte>& buffer, System.Object& value, System.Type type, MemoryPack.MemoryPackSerializerOptions options)
  RVA=-1  // generic def  token=0x6000122  System.Threading.Tasks.ValueTask<T> DeserializeAsync(System.IO.Stream stream, MemoryPack.MemoryPackSerializerOptions options, System.Threading.CancellationToken cancellationToken)
  RVA=0x0A7045F8  token=0x6000123  System.Object Deserialize(System.Type type, System.ReadOnlySpan<System.Byte> buffer, MemoryPack.MemoryPackSerializerOptions options)
  RVA=0x0A704AD0  token=0x6000124  System.Int32 Deserialize(System.Type type, System.ReadOnlySpan<System.Byte> buffer, System.Object& value, MemoryPack.MemoryPackSerializerOptions options)
  RVA=0x0A704854  token=0x6000125  System.Object Deserialize(System.Type type, System.Buffers.ReadOnlySequence<System.Byte>& buffer, MemoryPack.MemoryPackSerializerOptions options)
  RVA=0x0A70487C  token=0x6000126  System.Int32 Deserialize(System.Type type, System.Buffers.ReadOnlySequence<System.Byte>& buffer, System.Object& value, MemoryPack.MemoryPackSerializerOptions options)
  RVA=0x0A7044D0  token=0x6000127  System.Threading.Tasks.ValueTask<System.Object> DeserializeAsync(System.Type type, System.IO.Stream stream, MemoryPack.MemoryPackSerializerOptions options, System.Threading.CancellationToken cancellationToken)
END_CLASS

CLASS: MemoryPack.MemoryPackSerializerOptions
TYPE:  class
TOKEN: 0x200002C
SIZE:  0x20
IMPLEMENTS: System.IEquatable`1
FIELDS:
  public    static readonly MemoryPack.MemoryPackSerializerOptionsDefault  // static @ 0x0
  public    static readonly MemoryPack.MemoryPackSerializerOptionsUtf8  // static @ 0x8
  public    static readonly MemoryPack.MemoryPackSerializerOptionsUtf16  // static @ 0x10
  private   readonly MemoryPack.StringEncoding       <StringEncoding>k__BackingField  // 0x10
  private   readonly System.IServiceProvider         <ServiceProvider>k__BackingField  // 0x18
PROPERTIES:
  EqualityContract  get=0x0A704414
  StringEncoding  get=0x015EFCE0  set=0x04D86130
  ServiceProvider  get=0x0385B100  set=0x05392C40
METHODS:
  RVA=0x0A704324  token=0x6000134  System.String ToString()
  RVA=0x0A704214  token=0x6000135  System.Boolean PrintMembers(System.Text.StringBuilder builder)
  RVA=0x0A70447C  token=0x6000136  System.Boolean op_Inequality(MemoryPack.MemoryPackSerializerOptions r1, MemoryPack.MemoryPackSerializerOptions r2)
  RVA=0x0A704464  token=0x6000137  System.Boolean op_Equality(MemoryPack.MemoryPackSerializerOptions r1, MemoryPack.MemoryPackSerializerOptions r2)
  RVA=0x0A70411C  token=0x6000138  System.Int32 GetHashCode()
  RVA=0x0A703FE0  token=0x6000139  System.Boolean Equals(System.Object obj)
  RVA=0x0A704034  token=0x600013A  System.Boolean Equals(MemoryPack.MemoryPackSerializerOptions other)
  RVA=0x049C6A90  token=0x600013B  MemoryPack.MemoryPackSerializerOptions <Clone>$()
  RVA=0x049C6AF0  token=0x600013C  System.Void .ctor(MemoryPack.MemoryPackSerializerOptions original)
  RVA=0x041E1670  token=0x600013D  System.Void .ctor()
  RVA=0x049C6980  token=0x600013E  System.Void .cctor()
END_CLASS

CLASS: MemoryPack.StringEncoding
TYPE:  sealed struct
TOKEN: 0x200002D
SIZE:  0x11
FIELDS:
  public            System.Byte                     value__  // 0x10
  public    static  MemoryPack.StringEncoding       Utf16  // const
  public    static  MemoryPack.StringEncoding       Utf8  // const
METHODS:
END_CLASS

CLASS: MemoryPack.MemoryPackWriterOptionalStatePool
TYPE:  static class
TOKEN: 0x200002E
SIZE:  0x10
FIELDS:
  private   static readonly System.Collections.Concurrent.ConcurrentQueue<MemoryPack.MemoryPackWriterOptionalState>queue  // static @ 0x0
METHODS:
  RVA=0x0A704CF4  token=0x600013F  MemoryPack.MemoryPackWriterOptionalState Rent(MemoryPack.MemoryPackSerializerOptions options)
  RVA=0x0A704DC8  token=0x6000140  System.Void Return(MemoryPack.MemoryPackWriterOptionalState state)
  RVA=0x0A704E44  token=0x6000141  System.Void .cctor()
END_CLASS

CLASS: MemoryPack.MemoryPackWriterOptionalState
TYPE:  sealed class
TOKEN: 0x200002F
SIZE:  0x28
IMPLEMENTS: System.IDisposable
FIELDS:
  private   static readonly MemoryPack.MemoryPackWriterOptionalStateNullState  // static @ 0x0
  private           System.UInt32                   nextId  // 0x10
  private   readonly System.Collections.Generic.Dictionary<System.Object,System.UInt32>objectToRef  // 0x18
  private           MemoryPack.MemoryPackSerializerOptions<Options>k__BackingField  // 0x20
PROPERTIES:
  Options  get=0x04D862C0  set=0x05390F40
METHODS:
  RVA=0x0A70512C  token=0x6000144  System.Void .ctor()
  RVA=0x0A705214  token=0x6000145  System.Void .ctor(System.Boolean _)
  RVA=0x0A704FA4  token=0x6000146  System.Void Init(MemoryPack.MemoryPackSerializerOptions options)
  RVA=0x0A705014  token=0x6000147  System.Void Reset()
  RVA=0x0A704ED0  token=0x6000148  System.ValueTuple<System.Boolean,System.UInt32> GetOrAddReference(System.Object value)
  RVA=0x0A70506C  token=0x6000149  System.Void System.IDisposable.Dispose()
  RVA=0x0A7050B8  token=0x600014A  System.Void .cctor()
END_CLASS

CLASS: MemoryPack.AnimationCurveFormatter
TYPE:  sealed class
TOKEN: 0x2000031
SIZE:  0x10
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=0x0A6FD40C  token=0x6000150  System.Void SerializeKeyFrame(UnityEngine.Keyframe keyframe, MemoryPack.MemoryPackWriter& writer)
  RVA=0x03663040  token=0x6000151  System.Void DeserializeKeyFrame(MemoryPack.MemoryPackReader& reader, UnityEngine.Keyframe& keyframe)
  RVA=0x03662B30  token=0x6000152  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, UnityEngine.AnimationCurve& value)
  RVA=0x039D8F50  token=0x6000153  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.GradientFormatter
TYPE:  sealed class
TOKEN: 0x2000032
SIZE:  0x10
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=0x0A7002E8  token=0x6000154  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, UnityEngine.Gradient& value)
  RVA=0x039D8F20  token=0x6000155  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.RectOffsetFormatter
TYPE:  sealed class
TOKEN: 0x2000033
SIZE:  0x10
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=0x0A7062F8  token=0x6000156  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, UnityEngine.RectOffset& value)
  RVA=0x039D8EF0  token=0x6000157  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Compression.BitPackFormatter
TYPE:  sealed class
TOKEN: 0x200009E
SIZE:  0x10
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
  public    static readonly MemoryPack.Compression.BitPackFormatterDefault  // static @ 0x0
METHODS:
  RVA=0x0A6FD6E8  token=0x600026B  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Boolean[]& value)
  RVA=0x0A6FD86C  token=0x600026C  System.Boolean Get(System.Int32 data, System.Int32 index)
  RVA=0x0A6FD884  token=0x600026D  System.Void Set(System.Int32& data, System.Int32 index, System.Boolean value)
  RVA=0x0A6FD918  token=0x600026E  System.Void .ctor()
  RVA=0x0A6FD8A8  token=0x600026F  System.Void .cctor()
END_CLASS

CLASS: MemoryPack.Compression.BrotliCompressor
TYPE:  class
TOKEN: 0x200009F
SIZE:  0x20
IMPLEMENTS: System.Buffers.IBufferWriter`1 System.IDisposable
FIELDS:
  private           MemoryPack.Internal.ReusableLinkedArrayBufferWriterbufferWriter  // 0x10
  private   readonly System.Int32                    quality  // 0x18
  private   readonly System.Int32                    window  // 0x1c
METHODS:
  RVA=0x0A6FEA90  token=0x6000270  System.Void .ctor(System.IO.Compression.CompressionLevel compressionLevel)
  RVA=0x0A6FE9EC  token=0x6000271  System.Void .ctor(System.IO.Compression.CompressionLevel compressionLevel, System.Int32 window)
  RVA=0x0A6FEA24  token=0x6000272  System.Void .ctor(System.Int32 quality, System.Int32 window)
  RVA=0x0A6FE370  token=0x6000273  System.Void System.Buffers.IBufferWriter<System.Byte>.Advance(System.Int32 count)
  RVA=0x0A6FE3B8  token=0x6000274  System.Memory<System.Byte> System.Buffers.IBufferWriter<System.Byte>.GetMemory(System.Int32 sizeHint)
  RVA=0x0A6FE3F4  token=0x6000275  System.Span<System.Byte> System.Buffers.IBufferWriter<System.Byte>.GetSpan(System.Int32 sizeHint)
  RVA=0x0A6FE4AC  token=0x6000276  System.Byte[] ToArray()
  RVA=0x0A6FDD68  token=0x6000277  System.Void CopyTo(System.Buffers.IBufferWriter<System.Byte>& destBufferWriter)
  RVA=0x0A6FDC74  token=0x6000278  System.Threading.Tasks.ValueTask CopyToAsync(System.IO.Stream stream, System.Int32 bufferSize, System.Threading.CancellationToken cancellationToken)
  RVA=0x0A6FE03C  token=0x6000279  System.Void CopyTo(MemoryPack.MemoryPackWriter& memoryPackWriter)
  RVA=0x0A6FD940  token=0x600027A  System.Int32 CompressCore(System.IO.Compression.BrotliEncoder& encoder, System.ReadOnlySpan<System.Byte> source, System.Buffers.IBufferWriter<System.Byte>& destBufferWriter, System.Nullable<System.Int32> initialLength, System.Boolean isFinalBlock)
  RVA=0x0A6FDAC0  token=0x600027B  System.Int32 CompressCore(System.IO.Compression.BrotliEncoder& encoder, System.ReadOnlySpan<System.Byte> source, MemoryPack.MemoryPackWriter& destBufferWriter, System.Nullable<System.Int32> initialLength, System.Boolean isFinalBlock)
  RVA=0x0A6FE304  token=0x600027C  System.Void Dispose()
  RVA=0x0A6FE450  token=0x600027D  System.Void ThrowIfDisposed()
END_CLASS

CLASS: MemoryPack.Compression.BrotliUtils
TYPE:  static class
TOKEN: 0x20000A1
SIZE:  0x10
FIELDS:
  public    static  System.Int32                    WindowBits_Min  // const
  public    static  System.Int32                    WindowBits_Default  // const
  public    static  System.Int32                    WindowBits_Max  // const
  public    static  System.Int32                    Quality_Min  // const
  public    static  System.Int32                    Quality_Default  // const
  public    static  System.Int32                    Quality_Max  // const
  public    static  System.Int32                    MaxInputSize  // const
METHODS:
  RVA=0x0A6FFBF0  token=0x6000280  System.Int32 GetQualityFromCompressionLevel(System.IO.Compression.CompressionLevel compressionLevel)
  RVA=0x0A6FFBD0  token=0x6000281  System.Int32 BrotliEncoderMaxCompressedSize(System.Int32 input_size)
END_CLASS

CLASS: MemoryPack.Compression.BrotliDecompressor
TYPE:  sealed struct
TOKEN: 0x20000A2
SIZE:  0x18
IMPLEMENTS: System.IDisposable
FIELDS:
  private           MemoryPack.Internal.ReusableReadOnlySequenceBuildersequenceBuilder  // 0x10
METHODS:
  RVA=0x0A6FF13C  token=0x6000282  System.Buffers.ReadOnlySequence<System.Byte> Decompress(System.ReadOnlySpan<System.Byte> compressedSpan)
  RVA=0x0A6FEFCC  token=0x6000283  System.Buffers.ReadOnlySequence<System.Byte> Decompress(System.ReadOnlySpan<System.Byte> compressedSpan, System.Int32& consumed)
  RVA=0x0A6FF184  token=0x6000284  System.Buffers.ReadOnlySequence<System.Byte> Decompress(System.Buffers.ReadOnlySequence<System.Byte> compressedSequence)
  RVA=0x0A6FED28  token=0x6000285  System.Buffers.ReadOnlySequence<System.Byte> Decompress(System.Buffers.ReadOnlySequence<System.Byte> compressedSequence, System.Int32& consumed)
  RVA=0x0A6FEABC  token=0x6000286  System.Void DecompressCore(System.Buffers.OperationStatus& status, System.IO.Compression.BrotliDecoder& decoder, System.ReadOnlySpan<System.Byte> source, System.Int32& consumed)
  RVA=0x0A6FF1D8  token=0x6000287  System.Void Dispose()
  RVA=0x0A6FF238  token=0x6000288  System.Int32 GetDoubleCapacity(System.Int32 length)
END_CLASS

CLASS: MemoryPack.Compression.BrotliFormatter
TYPE:  sealed class
TOKEN: 0x20000A3
SIZE:  0x20
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
  private   static  System.Int32                    DefaultDecompssionSizeLimit  // const
  public    static readonly MemoryPack.Compression.BrotliFormatterDefault  // static @ 0x0
  private   readonly System.IO.Compression.CompressionLevelcompressionLevel  // 0x10
  private   readonly System.Int32                    window  // 0x14
  private   readonly System.Int32                    decompressionSizeLimit  // 0x18
METHODS:
  RVA=0x0A6FF550  token=0x6000289  System.Void .ctor()
  RVA=0x0A6FF5C0  token=0x600028A  System.Void .ctor(System.IO.Compression.CompressionLevel compressionLevel)
  RVA=0x0A6FF530  token=0x600028B  System.Void .ctor(System.IO.Compression.CompressionLevel compressionLevel, System.Int32 window)
  RVA=0x0A6FF564  token=0x600028C  System.Void .ctor(System.IO.Compression.CompressionLevel compressionLevel, System.Int32 window, System.Int32 decompressionSizeLimit)
  RVA=0x0A6FF284  token=0x600028D  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Byte[]& value)
  RVA=0x0A6FF4C0  token=0x600028E  System.Void .cctor()
END_CLASS

CLASS: MemoryPack.Compression.BrotliFormatter`1
TYPE:  sealed class
TOKEN: 0x20000A4
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
  private   static  System.Int32                    DefaultDecompssionSizeLimit  // const
  public    static readonly MemoryPack.Compression.BrotliFormatterDefault  // static @ 0x0
  private   readonly System.IO.Compression.CompressionLevelcompressionLevel  // 0x0
  private   readonly System.Int32                    window  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600028F  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000290  System.Void .ctor(System.IO.Compression.CompressionLevel compressionLevel)
  RVA=-1  // not resolved  token=0x6000291  System.Void .ctor(System.IO.Compression.CompressionLevel compressionLevel, System.Int32 window)
  RVA=-1  // not resolved  token=0x6000292  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, T& value)
  RVA=-1  // not resolved  token=0x6000293  System.Void .cctor()
END_CLASS

CLASS: MemoryPack.Formatters.UnmanagedArrayFormatter`1
TYPE:  sealed class
TOKEN: 0x2000045
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000194  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, T[]& value)
  RVA=-1  // not resolved  token=0x6000195  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.DangerousUnmanagedArrayFormatter`1
TYPE:  sealed class
TOKEN: 0x2000046
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000196  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, T[]& value)
  RVA=-1  // not resolved  token=0x6000197  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.ArrayFormatter`1
TYPE:  sealed class
TOKEN: 0x2000047
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000198  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, T[]& value)
  RVA=-1  // not resolved  token=0x6000199  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.ArraySegmentFormatter`1
TYPE:  sealed class
TOKEN: 0x2000048
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600019A  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.ArraySegment<T>& value)
  RVA=-1  // not resolved  token=0x600019B  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.MemoryFormatter`1
TYPE:  sealed class
TOKEN: 0x2000049
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600019C  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Memory<T>& value)
  RVA=-1  // not resolved  token=0x600019D  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.ReadOnlyMemoryFormatter`1
TYPE:  sealed class
TOKEN: 0x200004A
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600019E  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.ReadOnlyMemory<T>& value)
  RVA=-1  // not resolved  token=0x600019F  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.ReadOnlySequenceFormatter`1
TYPE:  sealed class
TOKEN: 0x200004B
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60001A0  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Buffers.ReadOnlySequence<T>& value)
  RVA=-1  // not resolved  token=0x60001A1  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.MemoryPoolFormatter`1
TYPE:  sealed class
TOKEN: 0x200004C
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60001A2  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Memory<T>& value)
  RVA=-1  // not resolved  token=0x60001A3  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.ReadOnlyMemoryPoolFormatter`1
TYPE:  sealed class
TOKEN: 0x200004D
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60001A4  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.ReadOnlyMemory<T>& value)
  RVA=-1  // not resolved  token=0x60001A5  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.BigIntegerFormatter
TYPE:  sealed class
TOKEN: 0x200004E
SIZE:  0x10
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=0x0A6FD4D8  token=0x60001A6  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Numerics.BigInteger& value)
  RVA=0x04D81920  token=0x60001A7  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.BitArrayFormatter
TYPE:  sealed class
TOKEN: 0x200004F
SIZE:  0x10
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=0x0A6FD5FC  token=0x60001A8  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.BitArray& value)
  RVA=0x04D818F0  token=0x60001A9  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.BitArrayView
TYPE:  class
TOKEN: 0x2000050
SIZE:  0x20
FIELDS:
  public            System.Int32[]                  m_array  // 0x10
  public            System.Int32                    m_length  // 0x18
  public            System.Int32                    _version  // 0x1c
METHODS:
  RVA=0x041E1670  token=0x60001AA  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.ListFormatter
TYPE:  static class
TOKEN: 0x2000051
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x60001AB  System.Collections.Generic.List<T> DeserializePackable(MemoryPack.MemoryPackReader& reader)
  RVA=-1  // generic def  token=0x60001AC  System.Void DeserializePackable(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.List<T>& value)
END_CLASS

CLASS: MemoryPack.Formatters.ListFormatter`1
TYPE:  sealed class
TOKEN: 0x2000052
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60001AD  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.List<T>& value)
  RVA=-1  // not resolved  token=0x60001AE  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.StackFormatter`1
TYPE:  sealed class
TOKEN: 0x2000053
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60001AF  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.Stack<T>& value)
  RVA=-1  // not resolved  token=0x60001B0  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.QueueFormatter`1
TYPE:  sealed class
TOKEN: 0x2000054
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60001B1  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.Queue<T>& value)
  RVA=-1  // not resolved  token=0x60001B2  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.LinkedListFormatter`1
TYPE:  sealed class
TOKEN: 0x2000055
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60001B3  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.LinkedList<T>& value)
  RVA=-1  // not resolved  token=0x60001B4  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.HashSetFormatter`1
TYPE:  sealed class
TOKEN: 0x2000056
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
  private   readonly System.Collections.Generic.IEqualityComparer<T>equalityComparer  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60001B5  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60001B6  System.Void .ctor(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
  RVA=-1  // not resolved  token=0x60001B7  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.HashSet<T>& value)
END_CLASS

CLASS: MemoryPack.Formatters.SortedSetFormatter`1
TYPE:  sealed class
TOKEN: 0x2000057
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
  private   readonly System.Collections.Generic.IComparer<T>comparer  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60001B8  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60001B9  System.Void .ctor(System.Collections.Generic.IComparer<T> comparer)
  RVA=-1  // not resolved  token=0x60001BA  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.SortedSet<T>& value)
END_CLASS

CLASS: MemoryPack.Formatters.CollectionFormatter`1
TYPE:  sealed class
TOKEN: 0x2000058
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60001BB  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.ObjectModel.Collection<T>& value)
  RVA=-1  // not resolved  token=0x60001BC  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.ObservableCollectionFormatter`1
TYPE:  sealed class
TOKEN: 0x2000059
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60001BD  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.ObjectModel.ObservableCollection<T>& value)
  RVA=-1  // not resolved  token=0x60001BE  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.ConcurrentQueueFormatter`1
TYPE:  sealed class
TOKEN: 0x200005A
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60001BF  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Concurrent.ConcurrentQueue<T>& value)
  RVA=-1  // not resolved  token=0x60001C0  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.ConcurrentStackFormatter`1
TYPE:  sealed class
TOKEN: 0x200005B
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60001C1  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Concurrent.ConcurrentStack<T>& value)
  RVA=-1  // not resolved  token=0x60001C2  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.ConcurrentBagFormatter`1
TYPE:  sealed class
TOKEN: 0x200005C
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60001C3  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Concurrent.ConcurrentBag<T>& value)
  RVA=-1  // not resolved  token=0x60001C4  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.DictionaryFormatter`2
TYPE:  sealed class
TOKEN: 0x200005D
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
  private   readonly System.Collections.Generic.IEqualityComparer<TKey>equalityComparer  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60001C5  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60001C6  System.Void .ctor(System.Collections.Generic.IEqualityComparer<TKey> equalityComparer)
  RVA=-1  // not resolved  token=0x60001C7  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.Dictionary<TKey,TValue>& value)
END_CLASS

CLASS: MemoryPack.Formatters.SortedDictionaryFormatter`2
TYPE:  sealed class
TOKEN: 0x200005E
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
  private   readonly System.Collections.Generic.IComparer<TKey>comparer  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60001C8  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60001C9  System.Void .ctor(System.Collections.Generic.IComparer<TKey> comparer)
  RVA=-1  // not resolved  token=0x60001CA  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.SortedDictionary<TKey,TValue>& value)
END_CLASS

CLASS: MemoryPack.Formatters.SortedListFormatter`2
TYPE:  sealed class
TOKEN: 0x200005F
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
  private   readonly System.Collections.Generic.IComparer<TKey>comparer  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60001CB  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60001CC  System.Void .ctor(System.Collections.Generic.IComparer<TKey> comparer)
  RVA=-1  // not resolved  token=0x60001CD  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.SortedList<TKey,TValue>& value)
END_CLASS

CLASS: MemoryPack.Formatters.ConcurrentDictionaryFormatter`2
TYPE:  sealed class
TOKEN: 0x2000060
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
  private   readonly System.Collections.Generic.IEqualityComparer<TKey>equalityComparer  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60001CE  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60001CF  System.Void .ctor(System.Collections.Generic.IEqualityComparer<TKey> equalityComparer)
  RVA=-1  // not resolved  token=0x60001D0  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>& value)
END_CLASS

CLASS: MemoryPack.Formatters.ReadOnlyCollectionFormatter`1
TYPE:  sealed class
TOKEN: 0x2000061
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60001D1  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.ObjectModel.ReadOnlyCollection<T>& value)
  RVA=-1  // not resolved  token=0x60001D2  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.ReadOnlyObservableCollectionFormatter`1
TYPE:  sealed class
TOKEN: 0x2000062
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60001D3  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.ObjectModel.ReadOnlyObservableCollection<T>& value)
  RVA=-1  // not resolved  token=0x60001D4  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.BlockingCollectionFormatter`1
TYPE:  sealed class
TOKEN: 0x2000063
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60001D5  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Concurrent.BlockingCollection<T>& value)
  RVA=-1  // not resolved  token=0x60001D6  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.CultureInfoFormatter
TYPE:  sealed class
TOKEN: 0x2000064
SIZE:  0x10
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=0x0A6FFE80  token=0x60001D7  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Globalization.CultureInfo& value)
  RVA=0x04D81710  token=0x60001D8  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.DynamicUnionFormatter`1
TYPE:  sealed class
TOKEN: 0x2000065
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
  private   readonly System.Collections.Generic.Dictionary<System.Type,System.UInt16>typeToTag  // 0x0
  private   readonly System.Collections.Generic.Dictionary<System.UInt16,System.Type>tagToType  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60001D9  System.Void .ctor(System.ValueTuple<System.UInt16,System.Type>[] memoryPackUnions)
  RVA=-1  // not resolved  token=0x60001DA  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, T& value)
END_CLASS

CLASS: MemoryPack.Formatters.GenericCollectionFormatter`2
TYPE:  sealed class
TOKEN: 0x2000067
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60001E1  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, TCollection& value)
  RVA=-1  // not resolved  token=0x60001E2  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.GenericSetFormatterBase`2
TYPE:  abstract class
TOKEN: 0x2000068
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60001E3  TSet CreateSet()
  RVA=-1  // not resolved  token=0x60001E4  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, TSet& value)
  RVA=-1  // not resolved  token=0x60001E5  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.GenericSetFormatter`2
TYPE:  sealed class
TOKEN: 0x2000069
EXTENDS: MemoryPack.Formatters.GenericSetFormatterBase`2
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60001E6  TSet CreateSet()
  RVA=-1  // not resolved  token=0x60001E7  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.GenericDictionaryFormatterBase`3
TYPE:  abstract class
TOKEN: 0x200006A
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60001E8  TDictionary CreateDictionary()
  RVA=-1  // not resolved  token=0x60001E9  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, TDictionary& value)
  RVA=-1  // not resolved  token=0x60001EA  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.GenericDictionaryFormatter`3
TYPE:  sealed class
TOKEN: 0x200006B
EXTENDS: MemoryPack.Formatters.GenericDictionaryFormatterBase`3
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60001EB  TDictionary CreateDictionary()
  RVA=-1  // not resolved  token=0x60001EC  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.InterfaceCollectionFormatterUtils
TYPE:  static class
TOKEN: 0x200006C
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x60001ED  System.Collections.Generic.List<T> ReadList(MemoryPack.MemoryPackReader& reader)
END_CLASS

CLASS: MemoryPack.Formatters.InterfaceEnumerableFormatter`1
TYPE:  sealed class
TOKEN: 0x200006D
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60001EE  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.IEnumerable<T>& value)
  RVA=-1  // not resolved  token=0x60001EF  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.InterfaceCollectionFormatter`1
TYPE:  sealed class
TOKEN: 0x200006E
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60001F0  System.Void .cctor()
  RVA=-1  // not resolved  token=0x60001F1  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.ICollection<T>& value)
  RVA=-1  // not resolved  token=0x60001F2  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.InterfaceReadOnlyCollectionFormatter`1
TYPE:  sealed class
TOKEN: 0x200006F
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60001F3  System.Void .cctor()
  RVA=-1  // not resolved  token=0x60001F4  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.IReadOnlyCollection<T>& value)
  RVA=-1  // not resolved  token=0x60001F5  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.InterfaceListFormatter`1
TYPE:  sealed class
TOKEN: 0x2000070
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60001F6  System.Void .cctor()
  RVA=-1  // not resolved  token=0x60001F7  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.IList<T>& value)
  RVA=-1  // not resolved  token=0x60001F8  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.InterfaceReadOnlyListFormatter`1
TYPE:  sealed class
TOKEN: 0x2000071
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60001F9  System.Void .cctor()
  RVA=-1  // not resolved  token=0x60001FA  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.IReadOnlyList<T>& value)
  RVA=-1  // not resolved  token=0x60001FB  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.InterfaceDictionaryFormatter`2
TYPE:  sealed class
TOKEN: 0x2000072
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
  private   readonly System.Collections.Generic.IEqualityComparer<TKey>equalityComparer  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60001FC  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60001FD  System.Void .ctor(System.Collections.Generic.IEqualityComparer<TKey> equalityComparer)
  RVA=-1  // not resolved  token=0x60001FE  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.IDictionary<TKey,TValue>& value)
END_CLASS

CLASS: MemoryPack.Formatters.InterfaceReadOnlyDictionaryFormatter`2
TYPE:  sealed class
TOKEN: 0x2000073
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
  private   readonly System.Collections.Generic.IEqualityComparer<TKey>equalityComparer  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60001FF  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000200  System.Void .ctor(System.Collections.Generic.IEqualityComparer<TKey> equalityComparer)
  RVA=-1  // not resolved  token=0x6000201  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.IReadOnlyDictionary<TKey,TValue>& value)
END_CLASS

CLASS: MemoryPack.Formatters.InterfaceLookupFormatter`2
TYPE:  sealed class
TOKEN: 0x2000074
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
  private   readonly System.Collections.Generic.IEqualityComparer<TKey>equalityComparer  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000202  System.Void .cctor()
  RVA=-1  // not resolved  token=0x6000203  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000204  System.Void .ctor(System.Collections.Generic.IEqualityComparer<TKey> equalityComparer)
  RVA=-1  // not resolved  token=0x6000205  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Linq.ILookup<TKey,TElement>& value)
END_CLASS

CLASS: MemoryPack.Formatters.InterfaceGroupingFormatter`2
TYPE:  sealed class
TOKEN: 0x2000075
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000206  System.Void .cctor()
  RVA=-1  // not resolved  token=0x6000207  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Linq.IGrouping<TKey,TElement>& value)
  RVA=-1  // not resolved  token=0x6000208  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.InterfaceSetFormatter`1
TYPE:  sealed class
TOKEN: 0x2000076
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
  private   readonly System.Collections.Generic.IEqualityComparer<T>equalityComparer  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000209  System.Void .cctor()
  RVA=-1  // not resolved  token=0x600020A  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600020B  System.Void .ctor(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
  RVA=-1  // not resolved  token=0x600020C  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.ISet<T>& value)
END_CLASS

CLASS: MemoryPack.Formatters.Grouping`2
TYPE:  sealed class
TOKEN: 0x2000077
IMPLEMENTS: System.Linq.IGrouping`2 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly TKey                            key  // 0x0
  private   readonly System.Collections.Generic.IEnumerable<TElement>elements  // 0x0
PROPERTIES:
  Key  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600020D  System.Void .ctor(TKey key, System.Collections.Generic.IEnumerable<TElement> elements)
  RVA=-1  // not resolved  token=0x600020F  System.Collections.Generic.IEnumerator<TElement> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000210  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: MemoryPack.Formatters.Lookup`2
TYPE:  sealed class
TOKEN: 0x2000078
IMPLEMENTS: System.Linq.ILookup`2 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly System.Collections.Generic.Dictionary<TKey,System.Linq.IGrouping<TKey,TElement>>groupings  // 0x0
PROPERTIES:
  Item  get=-1  // not resolved
  Count  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000211  System.Void .ctor(System.Collections.Generic.Dictionary<TKey,System.Linq.IGrouping<TKey,TElement>> groupings)
  RVA=-1  // not resolved  token=0x6000214  System.Boolean Contains(TKey key)
  RVA=-1  // not resolved  token=0x6000215  System.Collections.Generic.IEnumerator<System.Linq.IGrouping<TKey,TElement>> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000216  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: MemoryPack.Formatters.KeyValuePairFormatter
TYPE:  static class
TOKEN: 0x2000079
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000217  System.Void Deserialize(MemoryPack.IMemoryPackFormatter<TKey> keyFormatter, MemoryPack.IMemoryPackFormatter<TValue> valueFormatter, MemoryPack.MemoryPackReader& reader, TKey& key, TValue& value)
END_CLASS

CLASS: MemoryPack.Formatters.KeyValuePairFormatter`2
TYPE:  sealed class
TOKEN: 0x200007A
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000218  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.KeyValuePair<TKey,TValue>& value)
  RVA=-1  // not resolved  token=0x6000219  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.LazyFormatter`1
TYPE:  sealed class
TOKEN: 0x200007B
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600021A  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Lazy<T>& value)
  RVA=-1  // not resolved  token=0x600021B  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.TwoDimensionalArrayFormatter`1
TYPE:  sealed class
TOKEN: 0x200007C
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600021C  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, T[,]& value)
  RVA=-1  // not resolved  token=0x600021D  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.ThreeDimensionalArrayFormatter`1
TYPE:  sealed class
TOKEN: 0x200007D
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600021E  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, T[,,]& value)
  RVA=-1  // not resolved  token=0x600021F  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.FourDimensionalArrayFormatter`1
TYPE:  sealed class
TOKEN: 0x200007E
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000220  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, T[,,,]& value)
  RVA=-1  // not resolved  token=0x6000221  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.NullableFormatter`1
TYPE:  sealed class
TOKEN: 0x200007F
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000222  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Nullable<T>& value)
  RVA=-1  // not resolved  token=0x6000223  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.StringBuilderFormatter
TYPE:  sealed class
TOKEN: 0x2000080
SIZE:  0x10
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=0x0A707E6C  token=0x6000224  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Text.StringBuilder& value)
  RVA=0x04D80CF0  token=0x6000225  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.StringFormatter
TYPE:  sealed class
TOKEN: 0x2000081
SIZE:  0x10
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
  public    static readonly MemoryPack.Formatters.StringFormatterDefault  // static @ 0x0
METHODS:
  RVA=0x03C32650  token=0x6000226  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.String& value)
  RVA=0x04D109A0  token=0x6000227  System.Void .ctor()
  RVA=0x04D10930  token=0x6000228  System.Void .cctor()
END_CLASS

CLASS: MemoryPack.Formatters.Utf8StringFormatter
TYPE:  sealed class
TOKEN: 0x2000082
SIZE:  0x10
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
  public    static readonly MemoryPack.Formatters.Utf8StringFormatterDefault  // static @ 0x0
METHODS:
  RVA=0x067C5DC0  token=0x6000229  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.String& value)
  RVA=0x0A70B12C  token=0x600022A  System.Void .ctor()
  RVA=0x0A70B0BC  token=0x600022B  System.Void .cctor()
END_CLASS

CLASS: MemoryPack.Formatters.Utf16StringFormatter
TYPE:  sealed class
TOKEN: 0x2000083
SIZE:  0x10
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
  public    static readonly MemoryPack.Formatters.Utf16StringFormatterDefault  // static @ 0x0
METHODS:
  RVA=0x067C5DC0  token=0x600022C  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.String& value)
  RVA=0x0A70B094  token=0x600022D  System.Void .ctor()
  RVA=0x0A70B024  token=0x600022E  System.Void .cctor()
END_CLASS

CLASS: MemoryPack.Formatters.InternStringFormatter
TYPE:  sealed class
TOKEN: 0x2000084
SIZE:  0x10
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
  public    static readonly MemoryPack.Formatters.InternStringFormatterDefault  // static @ 0x0
METHODS:
  RVA=0x0A700444  token=0x600022F  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.String& value)
  RVA=0x0A7004F0  token=0x6000230  System.Void .ctor()
  RVA=0x0A700480  token=0x6000231  System.Void .cctor()
END_CLASS

CLASS: MemoryPack.Formatters.BrotliStringFormatter
TYPE:  sealed class
TOKEN: 0x2000085
SIZE:  0x20
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
  private   static  System.Runtime.CompilerServices.StrongBox<System.Int32>threadStaticConsumedBox  // static @ 0xffffffff
  private   static  System.Int32                    DefaultDecompssionSizeLimit  // const
  public    static readonly MemoryPack.Formatters.BrotliStringFormatterDefault  // static @ 0x0
  private   readonly System.IO.Compression.CompressionLevelcompressionLevel  // 0x10
  private   readonly System.Int32                    window  // 0x14
  private   readonly System.Int32                    decompressionSizeLimit  // 0x18
METHODS:
  RVA=0x0A6FFB64  token=0x6000232  System.Void .ctor()
  RVA=0x0A6FFB18  token=0x6000233  System.Void .ctor(System.IO.Compression.CompressionLevel compressionLevel)
  RVA=0x0A6FFB44  token=0x6000234  System.Void .ctor(System.IO.Compression.CompressionLevel compressionLevel, System.Int32 window)
  RVA=0x0A6FFB74  token=0x6000235  System.Void .ctor(System.IO.Compression.CompressionLevel compressionLevel, System.Int32 window, System.Int32 decompressionSizeLimit)
  RVA=0x0A6FF5D0  token=0x6000236  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.String& value)
  RVA=0x0A6FFAA8  token=0x6000237  System.Void .cctor()
END_CLASS

CLASS: MemoryPack.Formatters.TimeZoneInfoFormatter
TYPE:  sealed class
TOKEN: 0x2000087
SIZE:  0x10
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=0x0A708014  token=0x600023C  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.TimeZoneInfo& value)
  RVA=0x04D80C90  token=0x600023D  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.TupleFormatterTypes
TYPE:  static class
TOKEN: 0x2000088
SIZE:  0x10
FIELDS:
  public    static readonly System.Collections.Generic.Dictionary<System.Type,System.Type>TupleFormatters  // static @ 0x0
METHODS:
  RVA=0x0A70808C  token=0x600023E  System.Void .cctor()
END_CLASS

CLASS: MemoryPack.Formatters.TupleFormatter`1
TYPE:  sealed class
TOKEN: 0x2000089
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600023F  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Tuple<T1>& value)
  RVA=-1  // not resolved  token=0x6000240  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.TupleFormatter`2
TYPE:  sealed class
TOKEN: 0x200008A
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000241  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Tuple<T1,T2>& value)
  RVA=-1  // not resolved  token=0x6000242  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.TupleFormatter`3
TYPE:  sealed class
TOKEN: 0x200008B
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000243  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Tuple<T1,T2,T3>& value)
  RVA=-1  // not resolved  token=0x6000244  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.TupleFormatter`4
TYPE:  sealed class
TOKEN: 0x200008C
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000245  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Tuple<T1,T2,T3,T4>& value)
  RVA=-1  // not resolved  token=0x6000246  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.TupleFormatter`5
TYPE:  sealed class
TOKEN: 0x200008D
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000247  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Tuple<T1,T2,T3,T4,T5>& value)
  RVA=-1  // not resolved  token=0x6000248  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.TupleFormatter`6
TYPE:  sealed class
TOKEN: 0x200008E
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000249  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Tuple<T1,T2,T3,T4,T5,T6>& value)
  RVA=-1  // not resolved  token=0x600024A  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.TupleFormatter`7
TYPE:  sealed class
TOKEN: 0x200008F
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600024B  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Tuple<T1,T2,T3,T4,T5,T6,T7>& value)
  RVA=-1  // not resolved  token=0x600024C  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.TupleFormatter`8
TYPE:  sealed class
TOKEN: 0x2000090
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600024D  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Tuple<T1,T2,T3,T4,T5,T6,T7,TRest>& value)
  RVA=-1  // not resolved  token=0x600024E  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.ValueTupleFormatter`1
TYPE:  sealed class
TOKEN: 0x2000091
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600024F  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.ValueTuple<T1>& value)
  RVA=-1  // not resolved  token=0x6000250  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.ValueTupleFormatter`2
TYPE:  sealed class
TOKEN: 0x2000092
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000251  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.ValueTuple<T1,T2>& value)
  RVA=-1  // not resolved  token=0x6000252  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.ValueTupleFormatter`3
TYPE:  sealed class
TOKEN: 0x2000093
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000253  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.ValueTuple<T1,T2,T3>& value)
  RVA=-1  // not resolved  token=0x6000254  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.ValueTupleFormatter`4
TYPE:  sealed class
TOKEN: 0x2000094
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000255  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.ValueTuple<T1,T2,T3,T4>& value)
  RVA=-1  // not resolved  token=0x6000256  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.ValueTupleFormatter`5
TYPE:  sealed class
TOKEN: 0x2000095
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000257  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.ValueTuple<T1,T2,T3,T4,T5>& value)
  RVA=-1  // not resolved  token=0x6000258  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.ValueTupleFormatter`6
TYPE:  sealed class
TOKEN: 0x2000096
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000259  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.ValueTuple<T1,T2,T3,T4,T5,T6>& value)
  RVA=-1  // not resolved  token=0x600025A  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.ValueTupleFormatter`7
TYPE:  sealed class
TOKEN: 0x2000097
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600025B  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.ValueTuple<T1,T2,T3,T4,T5,T6,T7>& value)
  RVA=-1  // not resolved  token=0x600025C  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.ValueTupleFormatter`8
TYPE:  sealed class
TOKEN: 0x2000098
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600025D  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.ValueTuple<T1,T2,T3,T4,T5,T6,T7,TRest>& value)
  RVA=-1  // not resolved  token=0x600025E  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.TypeFormatter
TYPE:  sealed class
TOKEN: 0x2000099
SIZE:  0x10
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
  private   static readonly System.Text.RegularExpressions.Regex_shortTypeNameRegex  // static @ 0x0
METHODS:
  RVA=0x0A7086A8  token=0x600025F  System.Text.RegularExpressions.Regex ShortTypeNameRegex()
  RVA=0x0A708614  token=0x6000260  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Type& value)
  RVA=0x04D80C00  token=0x6000261  System.Void .ctor()
  RVA=0x04CF7430  token=0x6000262  System.Void .cctor()
END_CLASS

CLASS: MemoryPack.Formatters.UnmanagedFormatter`1
TYPE:  sealed class
TOKEN: 0x200009A
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000263  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, T& value)
  RVA=-1  // not resolved  token=0x6000264  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.DangerousUnmanagedFormatter`1
TYPE:  sealed class
TOKEN: 0x200009B
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000265  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, T& value)
  RVA=-1  // not resolved  token=0x6000266  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.UriFormatter
TYPE:  sealed class
TOKEN: 0x200009C
SIZE:  0x10
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=0x0A70AF98  token=0x6000267  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Uri& value)
  RVA=0x04D80BA0  token=0x6000268  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Formatters.VersionFormatter
TYPE:  sealed class
TOKEN: 0x200009D
SIZE:  0x10
EXTENDS: MemoryPack.MemoryPackFormatter`1
FIELDS:
METHODS:
  RVA=0x0A70B154  token=0x6000269  System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Version& value)
  RVA=0x04D80B10  token=0x600026A  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Internal.EnumerableExtensions
TYPE:  static class
TOKEN: 0x2000034
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000158  System.Boolean TryGetNonEnumeratedCountEx(System.Collections.Generic.IEnumerable<T> value, System.Int32& count)
END_CLASS

CLASS: MemoryPack.Internal.FixedArrayBufferWriter
TYPE:  sealed struct
TOKEN: 0x2000035
SIZE:  0x20
IMPLEMENTS: System.Buffers.IBufferWriter`1
FIELDS:
  private           System.Byte[]                   buffer  // 0x10
  private           System.Int32                    written  // 0x18
METHODS:
  RVA=0x05393828  token=0x6000159  System.Void .ctor(System.Byte[] buffer)
  RVA=0x04DA3A90  token=0x600015A  System.Void Advance(System.Int32 count)
  RVA=0x0A7001B4  token=0x600015B  System.Memory<System.Byte> GetMemory(System.Int32 sizeHint)
  RVA=0x0A700254  token=0x600015C  System.Span<System.Byte> GetSpan(System.Int32 sizeHint)
  RVA=0x0A700164  token=0x600015D  System.Byte[] GetFilledBuffer()
END_CLASS

CLASS: MemoryPack.Internal.MathEx
TYPE:  static class
TOKEN: 0x2000036
SIZE:  0x10
FIELDS:
  private   static  System.Int32                    ArrayMexLength  // const
METHODS:
  RVA=0x0A700518  token=0x600015E  System.Int32 NewArrayCapacity(System.Int32 size)
END_CLASS

CLASS: MemoryPack.Internal.MemoryMarshalEx
TYPE:  static class
TOKEN: 0x2000037
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x600015F  T& GetArrayDataReference(T[] array)
  RVA=-1  // generic def  token=0x6000160  T[] AllocateUninitializedArray(System.Int32 length, System.Boolean pinned)
END_CLASS

CLASS: MemoryPack.Internal.PreserveAttribute
TYPE:  sealed class
TOKEN: 0x2000038
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000161  System.Void .ctor()
END_CLASS

CLASS: MemoryPack.Internal.ReusableLinkedArrayBufferWriterPool
TYPE:  static class
TOKEN: 0x2000039
SIZE:  0x10
FIELDS:
  private   static readonly System.Collections.Concurrent.ConcurrentQueue<MemoryPack.Internal.ReusableLinkedArrayBufferWriter>queue  // static @ 0x0
METHODS:
  RVA=0x0A706500  token=0x6000162  MemoryPack.Internal.ReusableLinkedArrayBufferWriter Rent()
  RVA=0x0A7065B4  token=0x6000163  System.Void Return(MemoryPack.Internal.ReusableLinkedArrayBufferWriter writer)
  RVA=0x0A706630  token=0x6000164  System.Void .cctor()
END_CLASS

CLASS: MemoryPack.Internal.ReusableLinkedArrayBufferWriter
TYPE:  sealed class
TOKEN: 0x200003A
SIZE:  0x40
IMPLEMENTS: System.Buffers.IBufferWriter`1
FIELDS:
  private   static  System.Int32                    InitialBufferSize  // const
  private   static readonly System.Byte[]                   noUseFirstBufferSentinel  // static @ 0x0
  private           System.Collections.Generic.List<MemoryPack.Internal.BufferSegment>buffers  // 0x10
  private           System.Byte[]                   firstBuffer  // 0x18
  private           System.Int32                    firstBufferWritten  // 0x20
  private           MemoryPack.Internal.BufferSegmentcurrent  // 0x28
  private           System.Int32                    nextBufferSize  // 0x38
  private           System.Int32                    totalWritten  // 0x3c
PROPERTIES:
  TotalWritten  get=0x04D86640
  UseFirstBuffer  get=0x0A707338
METHODS:
  RVA=0x0A707234  token=0x6000167  System.Void .ctor(System.Boolean useFirstBuffer, System.Boolean pinned)
  RVA=0x0385B100  token=0x6000168  System.Byte[] DangerousGetFirstBuffer()
  RVA=0x0A706708  token=0x6000169  System.Memory<System.Byte> GetMemory(System.Int32 sizeHint)
  RVA=0x0A706754  token=0x600016A  System.Span<System.Byte> GetSpan(System.Int32 sizeHint)
  RVA=0x0A7066BC  token=0x600016B  System.Void Advance(System.Int32 count)
  RVA=0x0A706A44  token=0x600016C  System.Byte[] ToArrayAndReset()
  RVA=0x0A706E94  token=0x600016D  System.Void WriteToAndReset(MemoryPack.MemoryPackWriter& writer)
  RVA=0x0A706DB0  token=0x600016E  System.Threading.Tasks.ValueTask WriteToAndResetAsync(System.IO.Stream stream, System.Threading.CancellationToken cancellationToken)
  RVA=0x0A7066DC  token=0x600016F  MemoryPack.Internal.ReusableLinkedArrayBufferWriter.Enumerator GetEnumerator()
  RVA=0x0A706898  token=0x6000170  System.Void ResetCore()
  RVA=0x0A7068F4  token=0x6000171  System.Void Reset()
  RVA=0x0A7071D0  token=0x6000172  System.Void .cctor()
END_CLASS

CLASS: MemoryPack.Internal.BufferSegment
TYPE:  sealed struct
TOKEN: 0x200003E
SIZE:  0x20
FIELDS:
  private           System.Byte[]                   buffer  // 0x10
  private           System.Int32                    written  // 0x18
PROPERTIES:
  IsNull  get=0x0314CD60
  WrittenCount  get=0x04D88B10
  WrittenBuffer  get=0x0A6FFDB8
  WrittenMemory  get=0x0A6FFE1C
  FreeBuffer  get=0x0A6FFD64
METHODS:
  RVA=0x0A6FFCE4  token=0x6000180  System.Void .ctor(System.Int32 size)
  RVA=0x04DA3A90  token=0x6000181  System.Void Advance(System.Int32 count)
  RVA=0x0A6FFC68  token=0x6000182  System.Void Clear()
END_CLASS

CLASS: MemoryPack.Internal.ReusableReadOnlySequenceBuilderPool
TYPE:  static class
TOKEN: 0x200003F
SIZE:  0x10
FIELDS:
  private   static readonly System.Collections.Concurrent.ConcurrentQueue<MemoryPack.Internal.ReusableReadOnlySequenceBuilder>queue  // static @ 0x0
METHODS:
  RVA=0x0A707388  token=0x6000183  MemoryPack.Internal.ReusableReadOnlySequenceBuilder Rent()
  RVA=0x0A707434  token=0x6000184  System.Void Return(MemoryPack.Internal.ReusableReadOnlySequenceBuilder builder)
  RVA=0x0A7074B0  token=0x6000185  System.Void .cctor()
END_CLASS

CLASS: MemoryPack.Internal.ReusableReadOnlySequenceBuilder
TYPE:  sealed class
TOKEN: 0x2000040
SIZE:  0x20
FIELDS:
  private   readonly System.Collections.Generic.Stack<MemoryPack.Internal.ReusableReadOnlySequenceBuilder.Segment>segmentPool  // 0x10
  private   readonly System.Collections.Generic.List<MemoryPack.Internal.ReusableReadOnlySequenceBuilder.Segment>list  // 0x18
METHODS:
  RVA=0x0A707A80  token=0x6000186  System.Void .ctor()
  RVA=0x0A70753C  token=0x6000187  System.Void Add(System.ReadOnlyMemory<System.Byte> buffer, System.Boolean returnToPool)
  RVA=0x0A7079E4  token=0x6000188  System.Boolean TryGetSingleMemory(System.ReadOnlyMemory<System.Byte>& memory)
  RVA=0x0A707638  token=0x6000189  System.Buffers.ReadOnlySequence<System.Byte> Build()
  RVA=0x0A70786C  token=0x600018A  System.Void Reset()
END_CLASS

CLASS: MemoryPack.Internal.TypeHelpers
TYPE:  static class
TOKEN: 0x2000042
SIZE:  0x10
FIELDS:
  private   static readonly System.Reflection.MethodInfo    isReferenceOrContainsReferences  // static @ 0x0
  private   static readonly System.Reflection.MethodInfo    unsafeSizeOf  // static @ 0x8
METHODS:
  RVA=-1  // generic def  token=0x600018F  System.Boolean IsReferenceOrNullable()
  RVA=-1  // generic def  token=0x6000190  MemoryPack.Internal.TypeHelpers.TypeKind TryGetUnmanagedSZArrayElementSizeOrMemoryPackableFixedSize(System.Int32& size)
  RVA=0x0A7086F8  token=0x6000191  System.Boolean IsAnonymous(System.Type type)
  RVA=0x0A708850  token=0x6000192  System.Void .cctor()
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

CLASS: System.Runtime.CompilerServices.NullableAttribute
TYPE:  sealed class
TOKEN: 0x2000004
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.Byte[]                   NullableFlags  // 0x10
METHODS:
  RVA=0x03F26320  token=0x6000003  System.Void .ctor(System.Byte )
  RVA=0x053908C0  token=0x6000004  System.Void .ctor(System.Byte[] )
END_CLASS

CLASS: System.Runtime.CompilerServices.NullableContextAttribute
TYPE:  sealed class
TOKEN: 0x2000005
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.Byte                     Flag  // 0x10
METHODS:
  RVA=0x04D86130  token=0x6000005  System.Void .ctor(System.Byte )
END_CLASS

CLASS: System.Runtime.CompilerServices.IsExternalInit
TYPE:  sealed class
TOKEN: 0x2000007
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000007  System.Void .ctor()
END_CLASS

