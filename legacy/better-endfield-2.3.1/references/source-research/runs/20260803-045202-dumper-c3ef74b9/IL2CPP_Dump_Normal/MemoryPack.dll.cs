// ========================================================
// Dumped by @desirepro
// Assembly: MemoryPack.dll
// Classes:  165
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000006
    public class MemoryPackRegister`1
    {
        // Fields
        public static System.Func<System.Boolean> s_initFunc;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000006
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000018
    public static class Check`1
    {
        // Fields
        public static System.Boolean registered;  // static @ 0x0

    }

    // TypeToken: 0x2000019
    public static class Cache`1
    {
        // Fields
        private static MemoryPack.IMemoryPackFormatter<T> _formatter;  // static @ 0x0

        // Properties
        MemoryPack.IMemoryPackFormatter<T> formatter { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

    }

    // TypeToken: 0x2000029  // size: 0x20
    public sealed class SerializerWriterThreadStaticState
    {
        // Fields
        public MemoryPack.Internal.ReusableLinkedArrayBufferWriter BufferWriter;  // 0x10
        public MemoryPack.MemoryPackWriterOptionalState OptionalState;  // 0x18

        // Methods
        // RVA: 0x0A707DC8  token: 0x6000128
        public System.Void .ctor() { }
        // RVA: 0x0A707D74  token: 0x6000129
        public System.Void Init(MemoryPack.MemoryPackSerializerOptions options) { }
        // RVA: 0x0A707D94  token: 0x600012A
        public System.Void Reset() { }

    }

    // TypeToken: 0x200002A
    public sealed struct <DeserializeAsync>d__6`1 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x0
        public System.Runtime.CompilerServices.AsyncValueTaskMethodBuilder<T> <>t__builder;  // 0x0
        public System.IO.Stream stream;  // 0x0
        public System.Threading.CancellationToken cancellationToken;  // 0x0
        public MemoryPack.MemoryPackSerializerOptions options;  // 0x0
        private MemoryPack.Internal.ReusableReadOnlySequenceBuilder <builder>5__2;  // 0x0
        private System.Byte[] <buffer>5__3;  // 0x0
        private System.Int32 <offset>5__4;  // 0x0
        private System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.Int32> <>u__1;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600012B
        private virtual System.Void MoveNext() { }
        // RVA: -1  // not resolved  token: 0x600012C
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x200002B  // size: 0x88
    public sealed struct <DeserializeAsync>d__11 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncValueTaskMethodBuilder<System.Object> <>t__builder;  // 0x18
        public System.IO.Stream stream;  // 0x40
        public System.Threading.CancellationToken cancellationToken;  // 0x48
        public System.Type type;  // 0x50
        public MemoryPack.MemoryPackSerializerOptions options;  // 0x58
        private MemoryPack.Internal.ReusableReadOnlySequenceBuilder <builder>5__2;  // 0x60
        private System.Byte[] <buffer>5__3;  // 0x68
        private System.Int32 <offset>5__4;  // 0x70
        private System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.Int32> <>u__1;  // 0x78

        // Methods
        // RVA: 0x0A7096A0  token: 0x600012D
        private virtual System.Void MoveNext() { }
        // RVA: 0x0A70A1AC  token: 0x600012E
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x2000030  // size: 0x10
    public sealed class ReferenceEqualityComparer : System.Collections.Generic.IEqualityComparer`1
    {
        // Fields
        private static readonly MemoryPack.MemoryPackWriterOptionalState.ReferenceEqualityComparer <Instance>k__BackingField;  // static @ 0x0

        // Properties
        MemoryPack.MemoryPackWriterOptionalState.ReferenceEqualityComparer Instance { get; /* RVA: 0x0A7064B0 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x600014B
        private System.Void .ctor() { }
        // RVA: 0x0829C278  token: 0x600014D
        public virtual System.Boolean Equals(System.Object x, System.Object y) { }
        // RVA: 0x04DA3AC0  token: 0x600014E
        public virtual System.Int32 GetHashCode(System.Object obj) { }
        // RVA: 0x0A70644C  token: 0x600014F
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200003C  // size: 0x14
    public sealed struct State
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static MemoryPack.Internal.ReusableLinkedArrayBufferWriter.Enumerator.State FirstBuffer;  // const
        public static MemoryPack.Internal.ReusableLinkedArrayBufferWriter.Enumerator.State BuffersInit;  // const
        public static MemoryPack.Internal.ReusableLinkedArrayBufferWriter.Enumerator.State BuffersIterate;  // const
        public static MemoryPack.Internal.ReusableLinkedArrayBufferWriter.Enumerator.State Current;  // const
        public static MemoryPack.Internal.ReusableLinkedArrayBufferWriter.Enumerator.State End;  // const

    }

    // TypeToken: 0x200003B  // size: 0x50
    public sealed struct Enumerator : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private MemoryPack.Internal.ReusableLinkedArrayBufferWriter parent;  // 0x10
        private MemoryPack.Internal.ReusableLinkedArrayBufferWriter.Enumerator.State state;  // 0x18
        private System.Memory<System.Byte> current;  // 0x20
        private System.Collections.Generic.List.Enumerator<MemoryPack.Internal.BufferSegment> buffersEnumerator;  // 0x30

        // Properties
        System.Memory<System.Byte> Current { get; /* RVA: 0x02B2D510 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0A7000B8 */ }

        // Methods
        // RVA: 0x0A700104  token: 0x6000173
        public System.Void .ctor(MemoryPack.Internal.ReusableLinkedArrayBufferWriter parent) { }
        // RVA: 0x041E1670  token: 0x6000176
        public virtual System.Void Dispose() { }
        // RVA: 0x0A6FFEF8  token: 0x6000177
        public virtual System.Boolean MoveNext() { }
        // RVA: 0x0A70006C  token: 0x6000178
        public virtual System.Void Reset() { }

    }

    // TypeToken: 0x200003D  // size: 0x90
    public sealed struct <WriteToAndResetAsync>d__19 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncValueTaskMethodBuilder <>t__builder;  // 0x18
        public MemoryPack.Internal.ReusableLinkedArrayBufferWriter <>4__this;  // 0x38
        public System.IO.Stream stream;  // 0x40
        public System.Threading.CancellationToken cancellationToken;  // 0x48
        private System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter <>u__1;  // 0x50
        private System.Collections.Generic.List.Enumerator<MemoryPack.Internal.BufferSegment> <>7__wrap1;  // 0x60
        private MemoryPack.Internal.BufferSegment <item>5__3;  // 0x80

        // Methods
        // RVA: 0x0A70A6E4  token: 0x6000179
        private virtual System.Void MoveNext() { }
        // RVA: 0x0A709694  token: 0x600017A
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x2000041  // size: 0x38
    public class Segment : System.Buffers.ReadOnlySequenceSegment`1
    {
        // Fields
        private System.Boolean returnToPool;  // 0x30

        // Methods
        // RVA: 0x0A707D3C  token: 0x600018B
        public System.Void .ctor() { }
        // RVA: 0x0A707C80  token: 0x600018C
        public System.Void SetBuffer(System.ReadOnlyMemory<System.Byte> buffer, System.Boolean returnToPool) { }
        // RVA: 0x0A707B40  token: 0x600018D
        public System.Void Reset() { }
        // RVA: 0x0A707CDC  token: 0x600018E
        public System.Void SetRunningIndexAndNext(System.Int64 runningIndex, MemoryPack.Internal.ReusableReadOnlySequenceBuilder.Segment nextSegment) { }

    }

    // TypeToken: 0x2000043
    public static class Cache`1
    {
        // Fields
        public static System.Boolean IsReferenceOrNullable;  // static @ 0x0
        public static System.Boolean IsUnmanagedSZArray;  // static @ 0x0
        public static System.Int32 UnmanagedSZArrayElementSize;  // static @ 0x0
        public static System.Boolean IsFixedSizeMemoryPackable;  // static @ 0x0
        public static System.Int32 MemoryPackableFixedSize;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000193
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000044  // size: 0x11
    public sealed struct TypeKind
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static MemoryPack.Internal.TypeHelpers.TypeKind None;  // const
        public static MemoryPack.Internal.TypeHelpers.TypeKind UnmanagedSZArray;  // const
        public static MemoryPack.Internal.TypeHelpers.TypeKind FixedSizeMemoryPackable;  // const

    }

    // TypeToken: 0x2000066
    public sealed class <>c
    {
        // Fields
        public static readonly MemoryPack.Formatters.DynamicUnionFormatter.<>c<T> <>9;  // static @ 0x0
        public static System.Func<System.ValueTuple<System.UInt16,System.Type>,System.Type> <>9__2_0;  // static @ 0x0
        public static System.Func<System.ValueTuple<System.UInt16,System.Type>,System.UInt16> <>9__2_1;  // static @ 0x0
        public static System.Func<System.ValueTuple<System.UInt16,System.Type>,System.UInt16> <>9__2_2;  // static @ 0x0
        public static System.Func<System.ValueTuple<System.UInt16,System.Type>,System.Type> <>9__2_3;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60001DB
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x60001DC
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60001DD
        private System.Type <.ctor>b__2_0(System.ValueTuple<System.UInt16,System.Type> x) { }
        // RVA: -1  // not resolved  token: 0x60001DE
        private System.UInt16 <.ctor>b__2_1(System.ValueTuple<System.UInt16,System.Type> x) { }
        // RVA: -1  // not resolved  token: 0x60001DF
        private System.UInt16 <.ctor>b__2_2(System.ValueTuple<System.UInt16,System.Type> x) { }
        // RVA: -1  // not resolved  token: 0x60001E0
        private System.Type <.ctor>b__2_3(System.ValueTuple<System.UInt16,System.Type> x) { }

    }

    // TypeToken: 0x2000086  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly MemoryPack.Formatters.BrotliStringFormatter.<>c <>9;  // static @ 0x0
        public static System.Buffers.SpanAction<System.Char,System.ValueTuple<System.IntPtr,System.Int32,System.Int32,System.Runtime.CompilerServices.StrongBox<System.Int32>>> <>9__10_1;  // static @ 0x8
        public static System.Buffers.SpanAction<System.Char,System.ValueTuple<System.Buffers.ReadOnlySequence<System.Byte>,System.Int64,System.Int32,System.Runtime.CompilerServices.StrongBox<System.Int32>>> <>9__10_0;  // static @ 0x10

        // Methods
        // RVA: 0x0A70A680  token: 0x6000238
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000239
        public System.Void .ctor() { }
        // RVA: 0x0A70A4A4  token: 0x600023A
        private System.Void <Deserialize>b__10_1(System.Span<System.Char> stringSpan, System.ValueTuple<System.IntPtr,System.Int32,System.Int32,System.Runtime.CompilerServices.StrongBox<System.Int32>> state) { }
        // RVA: 0x0A70A1F8  token: 0x600023B
        private System.Void <Deserialize>b__10_0(System.Span<System.Char> stringSpan, System.ValueTuple<System.Buffers.ReadOnlySequence<System.Byte>,System.Int64,System.Int32,System.Runtime.CompilerServices.StrongBox<System.Int32>> state) { }

    }

    // TypeToken: 0x20000A0  // size: 0xD8
    public sealed struct <CopyToAsync>d__11 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncValueTaskMethodBuilder <>t__builder;  // 0x18
        public MemoryPack.Compression.BrotliCompressor <>4__this;  // 0x38
        public System.Int32 bufferSize;  // 0x40
        public System.IO.Stream stream;  // 0x48
        public System.Threading.CancellationToken cancellationToken;  // 0x50
        private System.IO.Compression.BrotliEncoder <encoder>5__2;  // 0x58
        private System.Byte[] <buffer>5__3;  // 0x68
        private MemoryPack.Internal.ReusableLinkedArrayBufferWriter.Enumerator <>7__wrap3;  // 0x70
        private System.Memory<System.Byte> <source>5__5;  // 0xb0
        private System.Buffers.OperationStatus <lastResult>5__6;  // 0xc0
        private System.Int32 <bytesConsumed>5__7;  // 0xc4
        private System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter <>u__1;  // 0xc8

        // Methods
        // RVA: 0x0A708964  token: 0x600027E
        private virtual System.Void MoveNext() { }
        // RVA: 0x0A709694  token: 0x600027F
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x20000A5  // size: 0x10
    public sealed class <PrivateImplementationDetails>
    {
        // Fields
        private static readonly System.Int32 AD95131BC0B799C0B1AF477FB14FCF26A6A9F76079E48BF090ACB7E8367BFD0E;  // static @ 0x0
        private static readonly System.Int32 DF3F619804A92FDB4057192DC43DD748EA778ADC52BC498CE80524C014B81119;  // static @ 0x4

    }

namespace MemoryPack
{

    // TypeToken: 0x2000008  // size: 0x18
    public sealed class MemoryPackableAttribute : System.Attribute
    {
        // Fields
        private readonly MemoryPack.GenerateType <GenerateType>k__BackingField;  // 0x10
        private readonly MemoryPack.SerializeLayout <SerializeLayout>k__BackingField;  // 0x14

        // Properties
        MemoryPack.GenerateType GenerateType { get; /* RVA: 0x02B2E2D0 */ }
        MemoryPack.SerializeLayout SerializeLayout { get; /* RVA: 0x04D882A0 */ }

        // Methods
        // RVA: 0x0A7062E4  token: 0x600000A
        public System.Void .ctor(MemoryPack.GenerateType generateType) { }
        // RVA: 0x04DA3AB0  token: 0x600000B
        public System.Void .ctor(MemoryPack.SerializeLayout serializeLayout) { }
        // RVA: 0x04D88290  token: 0x600000C
        public System.Void .ctor(MemoryPack.GenerateType generateType, MemoryPack.SerializeLayout serializeLayout) { }

    }

    // TypeToken: 0x2000009  // size: 0x14
    public sealed struct GenerateType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static MemoryPack.GenerateType Object;  // const
        public static MemoryPack.GenerateType VersionTolerant;  // const
        public static MemoryPack.GenerateType CircularReference;  // const
        public static MemoryPack.GenerateType Collection;  // const
        public static MemoryPack.GenerateType NoGenerate;  // const

    }

    // TypeToken: 0x200000A  // size: 0x14
    public sealed struct SerializeLayout
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static MemoryPack.SerializeLayout Sequential;  // const
        public static MemoryPack.SerializeLayout Explicit;  // const

    }

    // TypeToken: 0x200000B  // size: 0x20
    public sealed class MemoryPackUnionAttribute : System.Attribute
    {
        // Fields
        private readonly System.UInt16 <Tag>k__BackingField;  // 0x10
        private readonly System.Type <Type>k__BackingField;  // 0x18

        // Properties
        System.UInt16 Tag { get; /* RVA: 0x04D96190 */ }
        System.Type Type { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x0A704CE0  token: 0x600000F
        public System.Void .ctor(System.UInt16 tag, System.Type type) { }

    }

    // TypeToken: 0x200000C  // size: 0x18
    public sealed class MemoryPackUnionFormatterAttribute : System.Attribute
    {
        // Fields
        private readonly System.Type <Type>k__BackingField;  // 0x10

        // Properties
        System.Type Type { get; /* RVA: 0x02B2ECC0 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x6000011
        public System.Void .ctor(System.Type type) { }

    }

    // TypeToken: 0x200000D  // size: 0x10
    public sealed class MemoryPackAllowSerializeAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000012
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000E  // size: 0x18
    public sealed class MemoryPackOrderAttribute : System.Attribute
    {
        // Fields
        private readonly System.Int32 <Order>k__BackingField;  // 0x10

        // Properties
        System.Int32 Order { get; /* RVA: 0x02B2E2D0 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000014
        public System.Void .ctor(System.Int32 order) { }

    }

    // TypeToken: 0x200000F  // size: 0x10
    public sealed class MemoryPackIgnoreAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000015
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000010  // size: 0x10
    public sealed class MemoryPackIncludeAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000016
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000011  // size: 0x10
    public sealed class MemoryPackConstructorAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000017
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000012  // size: 0x10
    public sealed class MemoryPackOnSerializingAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000018
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000013  // size: 0x10
    public sealed class MemoryPackOnSerializedAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000019
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000014  // size: 0x10
    public sealed class MemoryPackOnDeserializingAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600001A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000015  // size: 0x10
    public sealed class MemoryPackOnDeserializedAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600001B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000016  // size: 0x10
    public sealed class GenerateTypeScriptAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600001C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000017  // size: 0x10
    public static class MemoryPackFormatterProvider
    {
        // Fields
        private static readonly System.Collections.Generic.Dictionary<System.Type,System.Type> ArrayLikeFormatters;  // static @ 0x0
        private static readonly System.Collections.Generic.Dictionary<System.Type,System.Type> CollectionFormatters;  // static @ 0x8
        private static readonly System.Collections.Generic.Dictionary<System.Type,System.Type> InterfaceCollectionFormatters;  // static @ 0x10
        private static readonly System.Collections.Concurrent.ConcurrentDictionary<System.Type,MemoryPack.IMemoryPackFormatter> formatters;  // static @ 0x18
        private static readonly System.Collections.Concurrent.ConcurrentDictionary<System.Type,System.Type> genericFormatterFactory;  // static @ 0x20
        private static readonly System.Collections.Concurrent.ConcurrentDictionary<System.Type,System.Type> genericCollectionFormatterFactory;  // static @ 0x28
        private static readonly System.Collections.Generic.Dictionary<System.Type,System.Type> KnownGenericTypeFormatters;  // static @ 0x30
        private static System.Object lockObj;  // static @ 0x38

        // Methods
        // RVA: 0x039D8040  token: 0x600001D
        private static System.Void RegisterInitialFormatters() { }
        // RVA: 0x039D94A0  token: 0x600001E
        private static System.Void .cctor() { }
        // RVA: -1  // generic def  token: 0x600001F
        public static System.Boolean IsRegistered() { }
        // RVA: -1  // generic def  token: 0x6000020
        public static System.Void Register(MemoryPack.IMemoryPackFormatter<T> formatter) { }
        // RVA: 0x0A700E08  token: 0x6000021
        public static System.Void RegisterGenericType(System.Type genericType, System.Type genericFormatterType) { }
        // RVA: -1  // generic def  token: 0x6000022
        public static System.Void RegisterCollection() { }
        // RVA: 0x0A700BB0  token: 0x6000023
        public static System.Void RegisterCollection(System.Type genericCollectionType) { }
        // RVA: -1  // generic def  token: 0x6000024
        public static System.Void RegisterSet() { }
        // RVA: 0x0A700F20  token: 0x6000025
        public static System.Void RegisterSet(System.Type genericSetType) { }
        // RVA: -1  // generic def  token: 0x6000026
        public static System.Void RegisterDictionary() { }
        // RVA: 0x0A700CDC  token: 0x6000027
        public static System.Void RegisterDictionary(System.Type genericDictionaryType) { }
        // RVA: -1  // generic def  token: 0x6000028
        public static MemoryPack.IMemoryPackFormatter<T> GetFormatter() { }
        // RVA: 0x0A7009AC  token: 0x6000029
        private static MemoryPack.IMemoryPackFormatter GetFormatter(System.Type type) { }
        // RVA: 0x0A70121C  token: 0x600002A
        private static System.Boolean TryInvokeRegisterFormatter(System.Type type) { }
        // RVA: -1  // generic def  token: 0x600002B
        public static System.Void ForceRegister(MemoryPack.IMemoryPackFormatter<T> formatter) { }
        // RVA: 0x0A7005E0  token: 0x600002C
        private static System.Object CreateGenericFormatter(System.Type type, System.Boolean typeIsReferenceOrContainsReferences) { }
        // RVA: 0x0A70116C  token: 0x600002D
        private static System.Type TryCreateGenericFormatterType(System.Type type, System.Collections.Generic.IDictionary<System.Type,System.Type> knownTypes) { }
        // RVA: 0x0A70104C  token: 0x600002E
        private static System.Type TryCreateGenericCollectionFormatterType(System.Type type) { }
        // RVA: 0x0428B4C0  token: 0x600002F
        private static System.Void RegisterWellKnownTypesFormatters() { }
        // RVA: -1  // generic def  token: 0x6000030
        private static System.Void UnityRegister() { }

    }

    // TypeToken: 0x200001A
    public interface IMemoryPackFormatterRegister
    {
    }

    // TypeToken: 0x200001B
    public interface IMemoryPackable`1 : MemoryPack.IMemoryPackFormatterRegister
    {
    }

    // TypeToken: 0x200001C
    public interface IMemoryPackFormatter
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000033
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Object& value) { }

    }

    // TypeToken: 0x200001D
    public interface IMemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000034
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, T& value) { }

    }

    // TypeToken: 0x200001E
    public abstract class MemoryPackFormatter`1 : MemoryPack.IMemoryPackFormatter`1, MemoryPack.IMemoryPackFormatter
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000035
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, T& value) { }
        // RVA: -1  // not resolved  token: 0x6000036
        private virtual System.Void MemoryPack.IMemoryPackFormatter.Deserialize(MemoryPack.MemoryPackReader& reader, System.Object& value) { }
        // RVA: -1  // not resolved  token: 0x6000037
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200001F  // size: 0x10
    public static class MemoryPackCode
    {
        // Fields
        public static System.Int32 NullCollection;  // const
        public static System.Byte WideTag;  // const
        public static System.Byte ReferenceId;  // const
        public static System.Byte Reserved1;  // const
        public static System.Byte Reserved2;  // const
        public static System.Byte Reserved3;  // const
        public static System.Byte Reserved4;  // const
        public static System.Byte Reserved5;  // const
        public static System.Byte NullObject;  // const

        // Properties
        System.ReadOnlySpan<System.Byte> NullCollectionData { get; /* RVA: 0x0A700528 */ }
        System.ReadOnlySpan<System.Byte> ZeroCollectionData { get; /* RVA: 0x0A700584 */ }

    }

    // TypeToken: 0x2000020  // size: 0x20
    public sealed class ErrorMemoryPackFormatter : MemoryPack.IMemoryPackFormatter
    {
        // Fields
        private readonly System.Type type;  // 0x10
        private readonly System.String message;  // 0x18

        // Methods
        // RVA: 0x03E8CD90  token: 0x600003A
        public System.Void .ctor(System.Type type) { }
        // RVA: 0x032AF420  token: 0x600003B
        public System.Void .ctor(System.Type type, System.String message) { }
        // RVA: 0x0A700138  token: 0x600003C
        public System.Void Serialize(MemoryPack.MemoryPackWriter& writer, System.Object& value) { }
        // RVA: 0x0A700138  token: 0x600003D
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Object& value) { }
        // RVA: 0x0A700144  token: 0x600003E
        private System.Void Throw() { }

    }

    // TypeToken: 0x2000021
    public sealed class ErrorMemoryPackFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Fields
        private readonly System.Exception exception;  // 0x0
        private readonly System.String message;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600003F
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000040
        public System.Void .ctor(System.Exception exception) { }
        // RVA: -1  // not resolved  token: 0x6000041
        public System.Void .ctor(System.String message) { }
        // RVA: -1  // not resolved  token: 0x6000042
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, T& value) { }
        // RVA: -1  // not resolved  token: 0x6000043
        private System.Void Throw() { }

    }

    // TypeToken: 0x2000022  // size: 0x60
    public sealed struct MemoryPackReader
    {
        // Fields
        private System.Buffers.ReadOnlySequence<System.Byte> bufferSource;  // 0x10
        private readonly System.Int64 totalLength;  // 0x28
        private System.ReadOnlySpan<System.Byte> bufferReference;  // 0x30
        private System.Int32 bufferLength;  // 0x40
        private System.Byte[] rentBuffer;  // 0x48
        private System.Int32 advancedCount;  // 0x50
        private System.Int32 consumed;  // 0x54
        private readonly MemoryPack.MemoryPackReaderOptionalState optionalState;  // 0x58

        // Properties
        System.Int32 Consumed { get; /* RVA: 0x04D86530 */ }
        System.Int64 Remaining { get; /* RVA: 0x04DA3AA0 */ }
        MemoryPack.MemoryPackReaderOptionalState OptionalState { get; /* RVA: 0x04D86200 */ }
        MemoryPack.MemoryPackSerializerOptions Options { get; /* RVA: 0x0A7034A4 */ }

        // Methods
        // RVA: 0x0A70338C  token: 0x6000048
        public System.Void .ctor(System.Buffers.ReadOnlySequence<System.Byte>& sequence, MemoryPack.MemoryPackReaderOptionalState optionalState) { }
        // RVA: 0x03B70820  token: 0x6000049
        public System.Void .ctor(System.ReadOnlySpan<System.Byte> buffer, MemoryPack.MemoryPackReaderOptionalState optionalState) { }
        // RVA: 0x0A701DB8  token: 0x600004A
        public System.Byte& GetSpanReference(System.Int32 sizeHint) { }
        // RVA: 0x0A701828  token: 0x600004B
        private System.Byte& GetNextSpan(System.Int32 sizeHint) { }
        // RVA: 0x0A7016E8  token: 0x600004C
        public System.Void Advance(System.Int32 count) { }
        // RVA: 0x0A703050  token: 0x600004D
        private System.Boolean TryAdvanceSequence(System.Int32 count) { }
        // RVA: 0x0A701BA4  token: 0x600004E
        public System.Void GetRemainingSource(System.ReadOnlySpan<System.Byte>& singleSource, System.Buffers.ReadOnlySequence<System.Byte>& remainingSource) { }
        // RVA: 0x0A701770  token: 0x600004F
        public System.Void Dispose() { }
        // RVA: 0x0A7017E0  token: 0x6000050
        public MemoryPack.IMemoryPackFormatter GetFormatter(System.Type type) { }
        // RVA: -1  // generic def  token: 0x6000051
        public MemoryPack.IMemoryPackFormatter<T> GetFormatter() { }
        // RVA: 0x0A7032B4  token: 0x6000052
        public System.Boolean TryReadObjectHeader(System.Byte& memberCount) { }
        // RVA: 0x0A7032F4  token: 0x6000053
        public System.Boolean TryReadUnionHeader(System.UInt16& tag) { }
        // RVA: 0x0A703258  token: 0x6000054
        public System.Boolean TryReadCollectionHeader(System.Int32& length) { }
        // RVA: 0x0A701E20  token: 0x6000055
        public System.Boolean PeekIsNull() { }
        // RVA: 0x0A7031BC  token: 0x6000056
        public System.Boolean TryPeekObjectHeader(System.Byte& memberCount) { }
        // RVA: 0x0A7031E0  token: 0x6000057
        public System.Boolean TryPeekUnionHeader(System.UInt16& tag) { }
        // RVA: 0x0A703234  token: 0x6000058
        public System.Boolean TryPeekUshort(System.UInt16& tag) { }
        // RVA: 0x0A70316C  token: 0x6000059
        public System.Boolean TryPeekCollectionHeader(System.Int32& length) { }
        // RVA: 0x0A6FCE34  token: 0x600005A
        public System.Boolean DangerousTryReadCollectionHeader(System.Int32& length) { }
        // RVA: 0x0A701E3C  token: 0x600005B
        public System.String ReadString() { }
        // RVA: 0x0A701EA0  token: 0x600005C
        private System.String ReadUtf16(System.Int32 length) { }
        // RVA: 0x02FEC5F0  token: 0x600005D
        public System.String ReadUtf8(System.Int32 utf8Length) { }
        // RVA: -1  // generic def  token: 0x600005E
        public T1 ReadUnmanaged() { }
        // RVA: -1  // generic def  token: 0x600005F
        public System.Void ReadPackable(T& value) { }
        // RVA: -1  // generic def  token: 0x6000060
        public T ReadPackable() { }
        // RVA: -1  // generic def  token: 0x6000061
        public System.Void ReadValue(T& value) { }
        // RVA: -1  // generic def  token: 0x6000062
        public T ReadValue() { }
        // RVA: 0x0A701F70  token: 0x6000063
        public System.Void ReadValue(System.Type type, System.Object& value) { }
        // RVA: 0x0A701FE0  token: 0x6000064
        public System.Object ReadValue(System.Type type) { }
        // RVA: -1  // generic def  token: 0x6000065
        public System.Void ReadValueWithFormatter(TFormatter formatter, T& value) { }
        // RVA: -1  // generic def  token: 0x6000066
        public T ReadValueWithFormatter(TFormatter formatter) { }
        // RVA: -1  // generic def  token: 0x6000067
        public T[] ReadArray() { }
        // RVA: -1  // generic def  token: 0x6000068
        public System.Void ReadArray(T[]& value) { }
        // RVA: -1  // generic def  token: 0x6000069
        public System.Void ReadSpan(System.Span<T>& value) { }
        // RVA: -1  // generic def  token: 0x600006A
        public T[] ReadPackableArray() { }
        // RVA: -1  // generic def  token: 0x600006B
        public System.Void ReadPackableArray(T[]& value) { }
        // RVA: -1  // generic def  token: 0x600006C
        public System.Void ReadPackableSpan(System.Span<T>& value) { }
        // RVA: -1  // generic def  token: 0x600006D
        public T[] ReadUnmanagedArray() { }
        // RVA: -1  // generic def  token: 0x600006E
        public System.Void ReadUnmanagedArray(T[]& value) { }
        // RVA: -1  // generic def  token: 0x600006F
        public System.Void ReadUnmanagedSpan(System.Span<T>& value) { }
        // RVA: -1  // generic def  token: 0x6000070
        public T[] DangerousReadUnmanagedArray() { }
        // RVA: -1  // generic def  token: 0x6000071
        public System.Void DangerousReadUnmanagedArray(T[]& value) { }
        // RVA: -1  // generic def  token: 0x6000072
        public System.Void DangerousReadUnmanagedSpan(System.Span<T>& value) { }
        // RVA: -1  // generic def  token: 0x6000073
        public System.Void ReadSpanWithoutReadLengthHeader(System.Int32 length, System.Span<T>& value) { }
        // RVA: -1  // generic def  token: 0x6000074
        public System.Void ReadPackableSpanWithoutReadLengthHeader(System.Int32 length, System.Span<T>& value) { }
        // RVA: -1  // generic def  token: 0x6000075
        public System.Void DangerousReadUnmanagedSpanView(System.Boolean& isNull, System.ReadOnlySpan<System.Byte>& view) { }
        // RVA: -1  // generic def  token: 0x6000076
        public System.Void ReadUnmanaged(T1& value1) { }
        // RVA: -1  // generic def  token: 0x6000077
        public System.Void ReadUnmanaged(T1& value1, T2& value2) { }
        // RVA: -1  // generic def  token: 0x6000078
        public System.Void ReadUnmanaged(T1& value1, T2& value2, T3& value3) { }
        // RVA: -1  // generic def  token: 0x6000079
        public System.Void ReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4) { }
        // RVA: -1  // generic def  token: 0x600007A
        public System.Void ReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5) { }
        // RVA: -1  // generic def  token: 0x600007B
        public System.Void ReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6) { }
        // RVA: -1  // generic def  token: 0x600007C
        public System.Void ReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7) { }
        // RVA: -1  // generic def  token: 0x600007D
        public System.Void ReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8) { }
        // RVA: -1  // generic def  token: 0x600007E
        public System.Void ReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9) { }
        // RVA: -1  // generic def  token: 0x600007F
        public System.Void ReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10) { }
        // RVA: -1  // generic def  token: 0x6000080
        public System.Void ReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11) { }
        // RVA: -1  // generic def  token: 0x6000081
        public System.Void ReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12) { }
        // RVA: -1  // generic def  token: 0x6000082
        public System.Void ReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13) { }
        // RVA: -1  // generic def  token: 0x6000083
        public System.Void ReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13, T14& value14) { }
        // RVA: -1  // generic def  token: 0x6000084
        public System.Void ReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13, T14& value14, T15& value15) { }
        // RVA: -1  // generic def  token: 0x6000085
        public System.Void DangerousReadUnmanaged(T1& value1) { }
        // RVA: -1  // generic def  token: 0x6000086
        public System.Void DangerousReadUnmanaged(T1& value1, T2& value2) { }
        // RVA: -1  // generic def  token: 0x6000087
        public System.Void DangerousReadUnmanaged(T1& value1, T2& value2, T3& value3) { }
        // RVA: -1  // generic def  token: 0x6000088
        public System.Void DangerousReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4) { }
        // RVA: -1  // generic def  token: 0x6000089
        public System.Void DangerousReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5) { }
        // RVA: -1  // generic def  token: 0x600008A
        public System.Void DangerousReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6) { }
        // RVA: -1  // generic def  token: 0x600008B
        public System.Void DangerousReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7) { }
        // RVA: -1  // generic def  token: 0x600008C
        public System.Void DangerousReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8) { }
        // RVA: -1  // generic def  token: 0x600008D
        public System.Void DangerousReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9) { }
        // RVA: -1  // generic def  token: 0x600008E
        public System.Void DangerousReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10) { }
        // RVA: -1  // generic def  token: 0x600008F
        public System.Void DangerousReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11) { }
        // RVA: -1  // generic def  token: 0x6000090
        public System.Void DangerousReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12) { }
        // RVA: -1  // generic def  token: 0x6000091
        public System.Void DangerousReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13) { }
        // RVA: -1  // generic def  token: 0x6000092
        public System.Void DangerousReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13, T14& value14) { }
        // RVA: -1  // generic def  token: 0x6000093
        public System.Void DangerousReadUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13, T14& value14, T15& value15) { }
        // RVA: 0x0A702054  token: 0x6000094
        public System.Byte ReadVarIntByte() { }
        // RVA: 0x0A702850  token: 0x6000095
        public System.SByte ReadVarIntSByte() { }
        // RVA: 0x0A702AC4  token: 0x6000096
        public System.UInt16 ReadVarIntUInt16() { }
        // RVA: 0x0A702260  token: 0x6000097
        public System.Int16 ReadVarIntInt16() { }
        // RVA: 0x0A702CCC  token: 0x6000098
        public System.UInt32 ReadVarIntUInt32() { }
        // RVA: 0x0A702494  token: 0x6000099
        public System.Int32 ReadVarIntInt32() { }
        // RVA: 0x0A702EA0  token: 0x600009A
        public System.UInt64 ReadVarIntUInt64() { }
        // RVA: 0x0A70267C  token: 0x600009B
        public System.Int64 ReadVarIntInt64() { }

    }

    // TypeToken: 0x2000023  // size: 0x10
    public static class MemoryPackReaderOptionalStatePool
    {
        // Fields
        private static readonly System.Collections.Concurrent.ConcurrentQueue<MemoryPack.MemoryPackReaderOptionalState> queue;  // static @ 0x0

        // Methods
        // RVA: 0x0A701380  token: 0x600009C
        public static MemoryPack.MemoryPackReaderOptionalState Rent(MemoryPack.MemoryPackSerializerOptions options) { }
        // RVA: 0x0A701454  token: 0x600009D
        private static System.Void Return(MemoryPack.MemoryPackReaderOptionalState state) { }
        // RVA: 0x0A7014D0  token: 0x600009E
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000024  // size: 0x20
    public sealed class MemoryPackReaderOptionalState : System.IDisposable
    {
        // Fields
        private readonly System.Collections.Generic.Dictionary<System.UInt32,System.Object> refToObject;  // 0x10
        private MemoryPack.MemoryPackSerializerOptions <Options>k__BackingField;  // 0x18

        // Properties
        MemoryPack.MemoryPackSerializerOptions Options { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }

        // Methods
        // RVA: 0x03B70420  token: 0x60000A1
        private System.Void .ctor() { }
        // RVA: 0x03B70730  token: 0x60000A2
        private System.Void Init(MemoryPack.MemoryPackSerializerOptions options) { }
        // RVA: 0x0A7015FC  token: 0x60000A3
        public System.Object GetObjectReference(System.UInt32 id) { }
        // RVA: 0x0A70155C  token: 0x60000A4
        public System.Void AddObjectReference(System.UInt32 id, System.Object value) { }
        // RVA: 0x03B70790  token: 0x60000A5
        public System.Void Reset() { }
        // RVA: 0x0A70169C  token: 0x60000A6
        private virtual System.Void System.IDisposable.Dispose() { }

    }

    // TypeToken: 0x2000025  // size: 0x10
    public static class VarIntCodes
    {
        // Fields
        public static System.Byte MaxSingleValue;  // const
        public static System.SByte MinSingleValue;  // const
        public static System.SByte Byte;  // const
        public static System.SByte SByte;  // const
        public static System.SByte UInt16;  // const
        public static System.SByte Int16;  // const
        public static System.SByte UInt32;  // const
        public static System.SByte Int32;  // const
        public static System.SByte UInt64;  // const
        public static System.SByte Int64;  // const

    }

    // TypeToken: 0x2000026  // size: 0x48
    public sealed struct MemoryPackWriter
    {
        // Fields
        private static System.Int32 DepthLimit;  // const
        private System.Buffers.IBufferWriter<System.Byte> bufferWriter;  // 0x10
        private System.Span<System.Byte> bufferReference;  // 0x18
        private System.Int32 bufferLength;  // 0x28
        private System.Int32 advancedCount;  // 0x2c
        private System.Int32 depth;  // 0x30
        private System.Int32 writtenCount;  // 0x34
        private readonly System.Boolean serializeStringAsUtf8;  // 0x38
        private readonly MemoryPack.MemoryPackWriterOptionalState optionalState;  // 0x40

        // Properties
        System.Int32 WrittenCount { get; /* RVA: 0x011F0020 */ }
        System.Int32 BufferLength { get; /* RVA: 0x011EF5B0 */ }
        MemoryPack.MemoryPackWriterOptionalState OptionalState { get; /* RVA: 0x011F36E0 */ }
        MemoryPack.MemoryPackSerializerOptions Options { get; /* RVA: 0x0A7062C8 */ }

        // Methods
        // RVA: 0x0A705F98  token: 0x60000A7
        public System.Void WriteVarInt(System.Byte x) { }
        // RVA: 0x0A706014  token: 0x60000A8
        public System.Void WriteVarInt(System.SByte x) { }
        // RVA: 0x0A705BE4  token: 0x60000A9
        public System.Void WriteVarInt(System.UInt16 x) { }
        // RVA: 0x0A705B14  token: 0x60000AA
        public System.Void WriteVarInt(System.Int16 x) { }
        // RVA: 0x0A706090  token: 0x60000AB
        public System.Void WriteVarInt(System.UInt32 x) { }
        // RVA: 0x0A705D48  token: 0x60000AC
        public System.Void WriteVarInt(System.Int32 x) { }
        // RVA: 0x0A705C64  token: 0x60000AD
        public System.Void WriteVarInt(System.UInt64 x) { }
        // RVA: 0x0A705E44  token: 0x60000AE
        public System.Void WriteVarInt(System.Int64 x) { }
        // RVA: 0x0A706284  token: 0x60000B3
        public System.Void .ctor(System.Buffers.IBufferWriter<System.Byte>& writer, MemoryPack.MemoryPackWriterOptionalState optionalState) { }
        // RVA: 0x0A70613C  token: 0x60000B4
        public System.Void .ctor(System.Buffers.IBufferWriter<System.Byte>& writer, System.Byte[] firstBufferOfWriter, MemoryPack.MemoryPackWriterOptionalState optionalState) { }
        // RVA: 0x0A7061EC  token: 0x60000B5
        public System.Void .ctor(System.Buffers.IBufferWriter<System.Byte>& writer, System.Span<System.Byte> firstBufferOfWriter, MemoryPack.MemoryPackWriterOptionalState optionalState) { }
        // RVA: 0x0A70539C  token: 0x60000B6
        public System.Byte& GetSpanReference(System.Int32 sizeHint) { }
        // RVA: 0x0A7054B8  token: 0x60000B7
        private System.Void RequestNewBuffer(System.Int32 sizeHint) { }
        // RVA: 0x0A70527C  token: 0x60000B8
        public System.Void Advance(System.Int32 count) { }
        // RVA: 0x0A7052F0  token: 0x60000B9
        public System.Void Flush() { }
        // RVA: 0x0A705354  token: 0x60000BA
        public MemoryPack.IMemoryPackFormatter GetFormatter(System.Type type) { }
        // RVA: -1  // generic def  token: 0x60000BB
        public MemoryPack.IMemoryPackFormatter<T> GetFormatter() { }
        // RVA: 0x0A705400  token: 0x60000BC
        public System.Int32 GetStringWriteLength(System.String value) { }
        // RVA: -1  // generic def  token: 0x60000BD
        public System.Int32 GetUnmanageArrayWriteLength(T[] value) { }
        // RVA: 0x0A7055A8  token: 0x60000BE
        public System.Void WriteObjectHeader(System.Byte memberCount) { }
        // RVA: 0x0A705580  token: 0x60000BF
        public System.Void WriteNullObjectHeader() { }
        // RVA: 0x0A7055EC  token: 0x60000C0
        public System.Void WriteObjectReferenceId(System.UInt32 referenceId) { }
        // RVA: 0x0A70564C  token: 0x60000C1
        public System.Void WriteUnionHeader(System.UInt16 tag) { }
        // RVA: 0x0A705580  token: 0x60000C2
        public System.Void WriteNullUnionHeader() { }
        // RVA: 0x0A70554C  token: 0x60000C3
        public System.Void WriteCollectionHeader(System.Int32 length) { }
        // RVA: 0x0A6FCEA0  token: 0x60000C4
        public System.Void WriteNullCollectionHeader() { }
        // RVA: 0x0A70562C  token: 0x60000C5
        public System.Void WriteString(System.String value) { }
        // RVA: 0x0A7057E8  token: 0x60000C6
        public System.Void WriteUtf16(System.String value) { }
        // RVA: 0x0A7056AC  token: 0x60000C7
        public System.Void WriteUtf16(System.ReadOnlySpan<System.Char> value) { }
        // RVA: 0x0A70593C  token: 0x60000C8
        public System.Void WriteUtf8(System.String value) { }
        // RVA: 0x0A705A68  token: 0x60000C9
        public System.Void WriteUtf8(System.ReadOnlySpan<System.Byte> utf8Value, System.Int32 utf16Length) { }
        // RVA: -1  // generic def  token: 0x60000CA
        public System.Void WriteUnmanaged(T1& value1) { }
        // RVA: -1  // generic def  token: 0x60000CB
        public System.Void WriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1) { }
        // RVA: -1  // generic def  token: 0x60000CC
        public System.Void WriteUnmanaged(T1& value1, T2& value2) { }
        // RVA: -1  // generic def  token: 0x60000CD
        public System.Void WriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2) { }
        // RVA: -1  // generic def  token: 0x60000CE
        public System.Void WriteUnmanaged(T1& value1, T2& value2, T3& value3) { }
        // RVA: -1  // generic def  token: 0x60000CF
        public System.Void WriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3) { }
        // RVA: -1  // generic def  token: 0x60000D0
        public System.Void WriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4) { }
        // RVA: -1  // generic def  token: 0x60000D1
        public System.Void WriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4) { }
        // RVA: -1  // generic def  token: 0x60000D2
        public System.Void WriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5) { }
        // RVA: -1  // generic def  token: 0x60000D3
        public System.Void WriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5) { }
        // RVA: -1  // generic def  token: 0x60000D4
        public System.Void WriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6) { }
        // RVA: -1  // generic def  token: 0x60000D5
        public System.Void WriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6) { }
        // RVA: -1  // generic def  token: 0x60000D6
        public System.Void WriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7) { }
        // RVA: -1  // generic def  token: 0x60000D7
        public System.Void WriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7) { }
        // RVA: -1  // generic def  token: 0x60000D8
        public System.Void WriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8) { }
        // RVA: -1  // generic def  token: 0x60000D9
        public System.Void WriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8) { }
        // RVA: -1  // generic def  token: 0x60000DA
        public System.Void WriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9) { }
        // RVA: -1  // generic def  token: 0x60000DB
        public System.Void WriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9) { }
        // RVA: -1  // generic def  token: 0x60000DC
        public System.Void WriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10) { }
        // RVA: -1  // generic def  token: 0x60000DD
        public System.Void WriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10) { }
        // RVA: -1  // generic def  token: 0x60000DE
        public System.Void WriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11) { }
        // RVA: -1  // generic def  token: 0x60000DF
        public System.Void WriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11) { }
        // RVA: -1  // generic def  token: 0x60000E0
        public System.Void WriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12) { }
        // RVA: -1  // generic def  token: 0x60000E1
        public System.Void WriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12) { }
        // RVA: -1  // generic def  token: 0x60000E2
        public System.Void WriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13) { }
        // RVA: -1  // generic def  token: 0x60000E3
        public System.Void WriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13) { }
        // RVA: -1  // generic def  token: 0x60000E4
        public System.Void WriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13, T14& value14) { }
        // RVA: -1  // generic def  token: 0x60000E5
        public System.Void WriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13, T14& value14) { }
        // RVA: -1  // generic def  token: 0x60000E6
        public System.Void WriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13, T14& value14, T15& value15) { }
        // RVA: -1  // generic def  token: 0x60000E7
        public System.Void WriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13, T14& value14, T15& value15) { }
        // RVA: -1  // generic def  token: 0x60000E8
        public System.Void DangerousWriteUnmanaged(T1& value1) { }
        // RVA: -1  // generic def  token: 0x60000E9
        public System.Void DangerousWriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1) { }
        // RVA: -1  // generic def  token: 0x60000EA
        public System.Void DangerousWriteUnmanaged(T1& value1, T2& value2) { }
        // RVA: -1  // generic def  token: 0x60000EB
        public System.Void DangerousWriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2) { }
        // RVA: -1  // generic def  token: 0x60000EC
        public System.Void DangerousWriteUnmanaged(T1& value1, T2& value2, T3& value3) { }
        // RVA: -1  // generic def  token: 0x60000ED
        public System.Void DangerousWriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3) { }
        // RVA: -1  // generic def  token: 0x60000EE
        public System.Void DangerousWriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4) { }
        // RVA: -1  // generic def  token: 0x60000EF
        public System.Void DangerousWriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4) { }
        // RVA: -1  // generic def  token: 0x60000F0
        public System.Void DangerousWriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5) { }
        // RVA: -1  // generic def  token: 0x60000F1
        public System.Void DangerousWriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5) { }
        // RVA: -1  // generic def  token: 0x60000F2
        public System.Void DangerousWriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6) { }
        // RVA: -1  // generic def  token: 0x60000F3
        public System.Void DangerousWriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6) { }
        // RVA: -1  // generic def  token: 0x60000F4
        public System.Void DangerousWriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7) { }
        // RVA: -1  // generic def  token: 0x60000F5
        public System.Void DangerousWriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7) { }
        // RVA: -1  // generic def  token: 0x60000F6
        public System.Void DangerousWriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8) { }
        // RVA: -1  // generic def  token: 0x60000F7
        public System.Void DangerousWriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8) { }
        // RVA: -1  // generic def  token: 0x60000F8
        public System.Void DangerousWriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9) { }
        // RVA: -1  // generic def  token: 0x60000F9
        public System.Void DangerousWriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9) { }
        // RVA: -1  // generic def  token: 0x60000FA
        public System.Void DangerousWriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10) { }
        // RVA: -1  // generic def  token: 0x60000FB
        public System.Void DangerousWriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10) { }
        // RVA: -1  // generic def  token: 0x60000FC
        public System.Void DangerousWriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11) { }
        // RVA: -1  // generic def  token: 0x60000FD
        public System.Void DangerousWriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11) { }
        // RVA: -1  // generic def  token: 0x60000FE
        public System.Void DangerousWriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12) { }
        // RVA: -1  // generic def  token: 0x60000FF
        public System.Void DangerousWriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12) { }
        // RVA: -1  // generic def  token: 0x6000100
        public System.Void DangerousWriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13) { }
        // RVA: -1  // generic def  token: 0x6000101
        public System.Void DangerousWriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13) { }
        // RVA: -1  // generic def  token: 0x6000102
        public System.Void DangerousWriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13, T14& value14) { }
        // RVA: -1  // generic def  token: 0x6000103
        public System.Void DangerousWriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13, T14& value14) { }
        // RVA: -1  // generic def  token: 0x6000104
        public System.Void DangerousWriteUnmanaged(T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13, T14& value14, T15& value15) { }
        // RVA: -1  // generic def  token: 0x6000105
        public System.Void DangerousWriteUnmanagedWithObjectHeader(System.Byte propertyCount, T1& value1, T2& value2, T3& value3, T4& value4, T5& value5, T6& value6, T7& value7, T8& value8, T9& value9, T10& value10, T11& value11, T12& value12, T13& value13, T14& value14, T15& value15) { }

    }

    // TypeToken: 0x2000027  // size: 0x90
    public class MemoryPackSerializationException : System.Exception
    {
        // Methods
        // RVA: 0x039985D0  token: 0x6000106
        public System.Void .ctor(System.String message) { }
        // RVA: 0x0A703F80  token: 0x6000107
        public System.Void .ctor(System.String message, System.Exception innerException) { }
        // RVA: 0x0A703B88  token: 0x6000108
        public static System.Void ThrowMessage(System.String message) { }
        // RVA: 0x0A703974  token: 0x6000109
        public static System.Void ThrowInvalidPropertyCount(System.Byte expected, System.Byte actual) { }
        // RVA: 0x03998300  token: 0x600010A
        public static System.Void ThrowInvalidPropertyCount(System.Type type, System.Byte expected, System.Byte actual) { }
        // RVA: 0x0A7038BC  token: 0x600010B
        public static System.Void ThrowInvalidCollection() { }
        // RVA: 0x0A703A28  token: 0x600010C
        public static System.Void ThrowInvalidRange(System.Int32 expected, System.Int32 actual) { }
        // RVA: 0x0A703860  token: 0x600010D
        public static System.Void ThrowInvalidAdvance() { }
        // RVA: 0x0A703E94  token: 0x600010E
        public static System.Void ThrowSequenceReachedEnd() { }
        // RVA: 0x0A703EF0  token: 0x600010F
        public static System.Void ThrowWriteInvalidMemberCount(System.Byte memberCount) { }
        // RVA: 0x0A7037D0  token: 0x6000110
        public static System.Void ThrowInsufficientBufferUnless(System.Int32 length) { }
        // RVA: 0x0A703D0C  token: 0x6000111
        public static System.Void ThrowNotRegisteredInProvider(System.Type type) { }
        // RVA: 0x0A703E04  token: 0x6000112
        public static System.Void ThrowRegisterInProviderFailed(System.Type type, System.Exception innerException) { }
        // RVA: 0x0A703BE0  token: 0x6000113
        public static System.Void ThrowNotFoundInUnionType(System.Type actualType, System.Type baseType) { }
        // RVA: 0x0A703ADC  token: 0x6000114
        public static System.Void ThrowInvalidTag(System.UInt16 tag, System.Type baseType) { }
        // RVA: 0x0A703D90  token: 0x6000115
        public static System.Void ThrowReachedDepthLimit(System.Type type) { }
        // RVA: 0x0A703918  token: 0x6000116
        public static System.Void ThrowInvalidConcurrrentCollectionOperation() { }
        // RVA: 0x0A7036BC  token: 0x6000117
        public static System.Void ThrowDeserializeObjectIsNull(System.String target) { }
        // RVA: 0x0A703740  token: 0x6000118
        public static System.Void ThrowFailedEncoding(System.Buffers.OperationStatus status) { }
        // RVA: 0x0A703578  token: 0x6000119
        public static System.Void ThrowCompressionFailed(System.Buffers.OperationStatus status) { }
        // RVA: 0x0A70351C  token: 0x600011A
        public static System.Void ThrowCompressionFailed() { }
        // RVA: 0x0A7034C0  token: 0x600011B
        public static System.Void ThrowAlreadyDecompressed() { }
        // RVA: 0x0A703608  token: 0x600011C
        public static System.Void ThrowDecompressionSizeLimitExceeded(System.Int32 limit, System.Int32 size) { }

    }

    // TypeToken: 0x2000028  // size: 0x10
    public static class MemoryPackSerializer
    {
        // Fields
        private static MemoryPack.MemoryPackReaderOptionalState threadStaticReaderOptionalState;  // static @ 0xffffffff
        private static MemoryPack.MemoryPackSerializer.SerializerWriterThreadStaticState threadStaticState;  // static @ 0xffffffff
        private static MemoryPack.MemoryPackWriterOptionalState threadStaticWriterOptionalState;  // static @ 0xffffffff

        // Methods
        // RVA: -1  // generic def  token: 0x600011D
        public static T Deserialize(System.ReadOnlySpan<System.Byte> buffer, MemoryPack.MemoryPackSerializerOptions options) { }
        // RVA: -1  // generic def  token: 0x600011E
        public static System.Int32 Deserialize(System.ReadOnlySpan<System.Byte> buffer, T& value, MemoryPack.MemoryPackSerializerOptions options) { }
        // RVA: -1  // generic def  token: 0x600011F
        public static T Deserialize(System.Buffers.ReadOnlySequence<System.Byte>& buffer, MemoryPack.MemoryPackSerializerOptions options) { }
        // RVA: -1  // generic def  token: 0x6000120
        public static System.Int32 Deserialize(System.Buffers.ReadOnlySequence<System.Byte>& buffer, T& value, MemoryPack.MemoryPackSerializerOptions options) { }
        // RVA: 0x0A70462C  token: 0x6000121
        public static System.Int32 Deserialize(System.Buffers.ReadOnlySequence<System.Byte>& buffer, System.Object& value, System.Type type, MemoryPack.MemoryPackSerializerOptions options) { }
        // RVA: -1  // generic def  token: 0x6000122
        public static System.Threading.Tasks.ValueTask<T> DeserializeAsync(System.IO.Stream stream, MemoryPack.MemoryPackSerializerOptions options, System.Threading.CancellationToken cancellationToken) { }
        // RVA: 0x0A7045F8  token: 0x6000123
        public static System.Object Deserialize(System.Type type, System.ReadOnlySpan<System.Byte> buffer, MemoryPack.MemoryPackSerializerOptions options) { }
        // RVA: 0x0A704AD0  token: 0x6000124
        public static System.Int32 Deserialize(System.Type type, System.ReadOnlySpan<System.Byte> buffer, System.Object& value, MemoryPack.MemoryPackSerializerOptions options) { }
        // RVA: 0x0A704854  token: 0x6000125
        public static System.Object Deserialize(System.Type type, System.Buffers.ReadOnlySequence<System.Byte>& buffer, MemoryPack.MemoryPackSerializerOptions options) { }
        // RVA: 0x0A70487C  token: 0x6000126
        public static System.Int32 Deserialize(System.Type type, System.Buffers.ReadOnlySequence<System.Byte>& buffer, System.Object& value, MemoryPack.MemoryPackSerializerOptions options) { }
        // RVA: 0x0A7044D0  token: 0x6000127
        public static System.Threading.Tasks.ValueTask<System.Object> DeserializeAsync(System.Type type, System.IO.Stream stream, MemoryPack.MemoryPackSerializerOptions options, System.Threading.CancellationToken cancellationToken) { }

    }

    // TypeToken: 0x200002C  // size: 0x20
    public class MemoryPackSerializerOptions : System.IEquatable`1
    {
        // Fields
        public static readonly MemoryPack.MemoryPackSerializerOptions Default;  // static @ 0x0
        public static readonly MemoryPack.MemoryPackSerializerOptions Utf8;  // static @ 0x8
        public static readonly MemoryPack.MemoryPackSerializerOptions Utf16;  // static @ 0x10
        private readonly MemoryPack.StringEncoding <StringEncoding>k__BackingField;  // 0x10
        private readonly System.IServiceProvider <ServiceProvider>k__BackingField;  // 0x18

        // Properties
        System.Type EqualityContract { get; /* RVA: 0x0A704414 */ }
        MemoryPack.StringEncoding StringEncoding { get; /* RVA: 0x015EFCE0 */ set; /* RVA: 0x04D86130 */ }
        System.IServiceProvider ServiceProvider { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }

        // Methods
        // RVA: 0x0A704324  token: 0x6000134
        public virtual System.String ToString() { }
        // RVA: 0x0A704214  token: 0x6000135
        protected virtual System.Boolean PrintMembers(System.Text.StringBuilder builder) { }
        // RVA: 0x0A70447C  token: 0x6000136
        public static System.Boolean op_Inequality(MemoryPack.MemoryPackSerializerOptions r1, MemoryPack.MemoryPackSerializerOptions r2) { }
        // RVA: 0x0A704464  token: 0x6000137
        public static System.Boolean op_Equality(MemoryPack.MemoryPackSerializerOptions r1, MemoryPack.MemoryPackSerializerOptions r2) { }
        // RVA: 0x0A70411C  token: 0x6000138
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0A703FE0  token: 0x6000139
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0A704034  token: 0x600013A
        public virtual System.Boolean Equals(MemoryPack.MemoryPackSerializerOptions other) { }
        // RVA: 0x049C6A90  token: 0x600013B
        public virtual MemoryPack.MemoryPackSerializerOptions <Clone>$() { }
        // RVA: 0x049C6AF0  token: 0x600013C
        protected System.Void .ctor(MemoryPack.MemoryPackSerializerOptions original) { }
        // RVA: 0x041E1670  token: 0x600013D
        public System.Void .ctor() { }
        // RVA: 0x049C6980  token: 0x600013E
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200002D  // size: 0x11
    public sealed struct StringEncoding
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static MemoryPack.StringEncoding Utf16;  // const
        public static MemoryPack.StringEncoding Utf8;  // const

    }

    // TypeToken: 0x200002E  // size: 0x10
    public static class MemoryPackWriterOptionalStatePool
    {
        // Fields
        private static readonly System.Collections.Concurrent.ConcurrentQueue<MemoryPack.MemoryPackWriterOptionalState> queue;  // static @ 0x0

        // Methods
        // RVA: 0x0A704CF4  token: 0x600013F
        public static MemoryPack.MemoryPackWriterOptionalState Rent(MemoryPack.MemoryPackSerializerOptions options) { }
        // RVA: 0x0A704DC8  token: 0x6000140
        private static System.Void Return(MemoryPack.MemoryPackWriterOptionalState state) { }
        // RVA: 0x0A704E44  token: 0x6000141
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200002F  // size: 0x28
    public sealed class MemoryPackWriterOptionalState : System.IDisposable
    {
        // Fields
        private static readonly MemoryPack.MemoryPackWriterOptionalState NullState;  // static @ 0x0
        private System.UInt32 nextId;  // 0x10
        private readonly System.Collections.Generic.Dictionary<System.Object,System.UInt32> objectToRef;  // 0x18
        private MemoryPack.MemoryPackSerializerOptions <Options>k__BackingField;  // 0x20

        // Properties
        MemoryPack.MemoryPackSerializerOptions Options { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }

        // Methods
        // RVA: 0x0A70512C  token: 0x6000144
        private System.Void .ctor() { }
        // RVA: 0x0A705214  token: 0x6000145
        private System.Void .ctor(System.Boolean _) { }
        // RVA: 0x0A704FA4  token: 0x6000146
        private System.Void Init(MemoryPack.MemoryPackSerializerOptions options) { }
        // RVA: 0x0A705014  token: 0x6000147
        public System.Void Reset() { }
        // RVA: 0x0A704ED0  token: 0x6000148
        public System.ValueTuple<System.Boolean,System.UInt32> GetOrAddReference(System.Object value) { }
        // RVA: 0x0A70506C  token: 0x6000149
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0A7050B8  token: 0x600014A
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000031  // size: 0x10
    public sealed class AnimationCurveFormatter : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: 0x0A6FD40C  token: 0x6000150
        private System.Void SerializeKeyFrame(UnityEngine.Keyframe keyframe, MemoryPack.MemoryPackWriter& writer) { }
        // RVA: 0x03663040  token: 0x6000151
        private System.Void DeserializeKeyFrame(MemoryPack.MemoryPackReader& reader, UnityEngine.Keyframe& keyframe) { }
        // RVA: 0x03662B30  token: 0x6000152
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, UnityEngine.AnimationCurve& value) { }
        // RVA: 0x039D8F50  token: 0x6000153
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000032  // size: 0x10
    public sealed class GradientFormatter : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: 0x0A7002E8  token: 0x6000154
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, UnityEngine.Gradient& value) { }
        // RVA: 0x039D8F20  token: 0x6000155
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000033  // size: 0x10
    public sealed class RectOffsetFormatter : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: 0x0A7062F8  token: 0x6000156
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, UnityEngine.RectOffset& value) { }
        // RVA: 0x039D8EF0  token: 0x6000157
        public System.Void .ctor() { }

    }

}

namespace MemoryPack.Compression
{

    // TypeToken: 0x200009E  // size: 0x10
    public sealed class BitPackFormatter : MemoryPack.MemoryPackFormatter`1
    {
        // Fields
        public static readonly MemoryPack.Compression.BitPackFormatter Default;  // static @ 0x0

        // Methods
        // RVA: 0x0A6FD6E8  token: 0x600026B
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Boolean[]& value) { }
        // RVA: 0x0A6FD86C  token: 0x600026C
        public System.Boolean Get(System.Int32 data, System.Int32 index) { }
        // RVA: 0x0A6FD884  token: 0x600026D
        public System.Void Set(System.Int32& data, System.Int32 index, System.Boolean value) { }
        // RVA: 0x0A6FD918  token: 0x600026E
        public System.Void .ctor() { }
        // RVA: 0x0A6FD8A8  token: 0x600026F
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200009F  // size: 0x20
    public class BrotliCompressor : System.Buffers.IBufferWriter`1, System.IDisposable
    {
        // Fields
        private MemoryPack.Internal.ReusableLinkedArrayBufferWriter bufferWriter;  // 0x10
        private readonly System.Int32 quality;  // 0x18
        private readonly System.Int32 window;  // 0x1c

        // Methods
        // RVA: 0x0A6FEA90  token: 0x6000270
        public System.Void .ctor(System.IO.Compression.CompressionLevel compressionLevel) { }
        // RVA: 0x0A6FE9EC  token: 0x6000271
        public System.Void .ctor(System.IO.Compression.CompressionLevel compressionLevel, System.Int32 window) { }
        // RVA: 0x0A6FEA24  token: 0x6000272
        public System.Void .ctor(System.Int32 quality, System.Int32 window) { }
        // RVA: 0x0A6FE370  token: 0x6000273
        private virtual System.Void System.Buffers.IBufferWriter<System.Byte>.Advance(System.Int32 count) { }
        // RVA: 0x0A6FE3B8  token: 0x6000274
        private virtual System.Memory<System.Byte> System.Buffers.IBufferWriter<System.Byte>.GetMemory(System.Int32 sizeHint) { }
        // RVA: 0x0A6FE3F4  token: 0x6000275
        private virtual System.Span<System.Byte> System.Buffers.IBufferWriter<System.Byte>.GetSpan(System.Int32 sizeHint) { }
        // RVA: 0x0A6FE4AC  token: 0x6000276
        public System.Byte[] ToArray() { }
        // RVA: 0x0A6FDD68  token: 0x6000277
        public System.Void CopyTo(System.Buffers.IBufferWriter<System.Byte>& destBufferWriter) { }
        // RVA: 0x0A6FDC74  token: 0x6000278
        public System.Threading.Tasks.ValueTask CopyToAsync(System.IO.Stream stream, System.Int32 bufferSize, System.Threading.CancellationToken cancellationToken) { }
        // RVA: 0x0A6FE03C  token: 0x6000279
        public System.Void CopyTo(MemoryPack.MemoryPackWriter& memoryPackWriter) { }
        // RVA: 0x0A6FD940  token: 0x600027A
        private static System.Int32 CompressCore(System.IO.Compression.BrotliEncoder& encoder, System.ReadOnlySpan<System.Byte> source, System.Buffers.IBufferWriter<System.Byte>& destBufferWriter, System.Nullable<System.Int32> initialLength, System.Boolean isFinalBlock) { }
        // RVA: 0x0A6FDAC0  token: 0x600027B
        private static System.Int32 CompressCore(System.IO.Compression.BrotliEncoder& encoder, System.ReadOnlySpan<System.Byte> source, MemoryPack.MemoryPackWriter& destBufferWriter, System.Nullable<System.Int32> initialLength, System.Boolean isFinalBlock) { }
        // RVA: 0x0A6FE304  token: 0x600027C
        public virtual System.Void Dispose() { }
        // RVA: 0x0A6FE450  token: 0x600027D
        private System.Void ThrowIfDisposed() { }

    }

    // TypeToken: 0x20000A1  // size: 0x10
    public static class BrotliUtils
    {
        // Fields
        public static System.Int32 WindowBits_Min;  // const
        public static System.Int32 WindowBits_Default;  // const
        public static System.Int32 WindowBits_Max;  // const
        public static System.Int32 Quality_Min;  // const
        public static System.Int32 Quality_Default;  // const
        public static System.Int32 Quality_Max;  // const
        public static System.Int32 MaxInputSize;  // const

        // Methods
        // RVA: 0x0A6FFBF0  token: 0x6000280
        private static System.Int32 GetQualityFromCompressionLevel(System.IO.Compression.CompressionLevel compressionLevel) { }
        // RVA: 0x0A6FFBD0  token: 0x6000281
        private static System.Int32 BrotliEncoderMaxCompressedSize(System.Int32 input_size) { }

    }

    // TypeToken: 0x20000A2  // size: 0x18
    public sealed struct BrotliDecompressor : System.IDisposable
    {
        // Fields
        private MemoryPack.Internal.ReusableReadOnlySequenceBuilder sequenceBuilder;  // 0x10

        // Methods
        // RVA: 0x0A6FF13C  token: 0x6000282
        public System.Buffers.ReadOnlySequence<System.Byte> Decompress(System.ReadOnlySpan<System.Byte> compressedSpan) { }
        // RVA: 0x0A6FEFCC  token: 0x6000283
        public System.Buffers.ReadOnlySequence<System.Byte> Decompress(System.ReadOnlySpan<System.Byte> compressedSpan, System.Int32& consumed) { }
        // RVA: 0x0A6FF184  token: 0x6000284
        public System.Buffers.ReadOnlySequence<System.Byte> Decompress(System.Buffers.ReadOnlySequence<System.Byte> compressedSequence) { }
        // RVA: 0x0A6FED28  token: 0x6000285
        public System.Buffers.ReadOnlySequence<System.Byte> Decompress(System.Buffers.ReadOnlySequence<System.Byte> compressedSequence, System.Int32& consumed) { }
        // RVA: 0x0A6FEABC  token: 0x6000286
        private System.Void DecompressCore(System.Buffers.OperationStatus& status, System.IO.Compression.BrotliDecoder& decoder, System.ReadOnlySpan<System.Byte> source, System.Int32& consumed) { }
        // RVA: 0x0A6FF1D8  token: 0x6000287
        public virtual System.Void Dispose() { }
        // RVA: 0x0A6FF238  token: 0x6000288
        private System.Int32 GetDoubleCapacity(System.Int32 length) { }

    }

    // TypeToken: 0x20000A3  // size: 0x20
    public sealed class BrotliFormatter : MemoryPack.MemoryPackFormatter`1
    {
        // Fields
        private static System.Int32 DefaultDecompssionSizeLimit;  // const
        public static readonly MemoryPack.Compression.BrotliFormatter Default;  // static @ 0x0
        private readonly System.IO.Compression.CompressionLevel compressionLevel;  // 0x10
        private readonly System.Int32 window;  // 0x14
        private readonly System.Int32 decompressionSizeLimit;  // 0x18

        // Methods
        // RVA: 0x0A6FF550  token: 0x6000289
        public System.Void .ctor() { }
        // RVA: 0x0A6FF5C0  token: 0x600028A
        public System.Void .ctor(System.IO.Compression.CompressionLevel compressionLevel) { }
        // RVA: 0x0A6FF530  token: 0x600028B
        public System.Void .ctor(System.IO.Compression.CompressionLevel compressionLevel, System.Int32 window) { }
        // RVA: 0x0A6FF564  token: 0x600028C
        public System.Void .ctor(System.IO.Compression.CompressionLevel compressionLevel, System.Int32 window, System.Int32 decompressionSizeLimit) { }
        // RVA: 0x0A6FF284  token: 0x600028D
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Byte[]& value) { }
        // RVA: 0x0A6FF4C0  token: 0x600028E
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000A4
    public sealed class BrotliFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Fields
        private static System.Int32 DefaultDecompssionSizeLimit;  // const
        public static readonly MemoryPack.Compression.BrotliFormatter Default;  // static @ 0x0
        private readonly System.IO.Compression.CompressionLevel compressionLevel;  // 0x0
        private readonly System.Int32 window;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600028F
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000290
        public System.Void .ctor(System.IO.Compression.CompressionLevel compressionLevel) { }
        // RVA: -1  // not resolved  token: 0x6000291
        public System.Void .ctor(System.IO.Compression.CompressionLevel compressionLevel, System.Int32 window) { }
        // RVA: -1  // not resolved  token: 0x6000292
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, T& value) { }
        // RVA: -1  // not resolved  token: 0x6000293
        private static System.Void .cctor() { }

    }

}

namespace MemoryPack.Formatters
{

    // TypeToken: 0x2000045
    public sealed class UnmanagedArrayFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000194
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, T[]& value) { }
        // RVA: -1  // not resolved  token: 0x6000195
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000046
    public sealed class DangerousUnmanagedArrayFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000196
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, T[]& value) { }
        // RVA: -1  // not resolved  token: 0x6000197
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000047
    public sealed class ArrayFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000198
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, T[]& value) { }
        // RVA: -1  // not resolved  token: 0x6000199
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000048
    public sealed class ArraySegmentFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x600019A
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.ArraySegment<T>& value) { }
        // RVA: -1  // not resolved  token: 0x600019B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000049
    public sealed class MemoryFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x600019C
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Memory<T>& value) { }
        // RVA: -1  // not resolved  token: 0x600019D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004A
    public sealed class ReadOnlyMemoryFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x600019E
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.ReadOnlyMemory<T>& value) { }
        // RVA: -1  // not resolved  token: 0x600019F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004B
    public sealed class ReadOnlySequenceFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60001A0
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Buffers.ReadOnlySequence<T>& value) { }
        // RVA: -1  // not resolved  token: 0x60001A1
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004C
    public sealed class MemoryPoolFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60001A2
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Memory<T>& value) { }
        // RVA: -1  // not resolved  token: 0x60001A3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004D
    public sealed class ReadOnlyMemoryPoolFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60001A4
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.ReadOnlyMemory<T>& value) { }
        // RVA: -1  // not resolved  token: 0x60001A5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004E  // size: 0x10
    public sealed class BigIntegerFormatter : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: 0x0A6FD4D8  token: 0x60001A6
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Numerics.BigInteger& value) { }
        // RVA: 0x04D81920  token: 0x60001A7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004F  // size: 0x10
    public sealed class BitArrayFormatter : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: 0x0A6FD5FC  token: 0x60001A8
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.BitArray& value) { }
        // RVA: 0x04D818F0  token: 0x60001A9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000050  // size: 0x20
    public class BitArrayView
    {
        // Fields
        public System.Int32[] m_array;  // 0x10
        public System.Int32 m_length;  // 0x18
        public System.Int32 _version;  // 0x1c

        // Methods
        // RVA: 0x041E1670  token: 0x60001AA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000051  // size: 0x10
    public static class ListFormatter
    {
        // Methods
        // RVA: -1  // generic def  token: 0x60001AB
        public static System.Collections.Generic.List<T> DeserializePackable(MemoryPack.MemoryPackReader& reader) { }
        // RVA: -1  // generic def  token: 0x60001AC
        public static System.Void DeserializePackable(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.List<T>& value) { }

    }

    // TypeToken: 0x2000052
    public sealed class ListFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60001AD
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.List<T>& value) { }
        // RVA: -1  // not resolved  token: 0x60001AE
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000053
    public sealed class StackFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60001AF
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.Stack<T>& value) { }
        // RVA: -1  // not resolved  token: 0x60001B0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000054
    public sealed class QueueFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60001B1
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.Queue<T>& value) { }
        // RVA: -1  // not resolved  token: 0x60001B2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000055
    public sealed class LinkedListFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60001B3
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.LinkedList<T>& value) { }
        // RVA: -1  // not resolved  token: 0x60001B4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000056
    public sealed class HashSetFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Fields
        private readonly System.Collections.Generic.IEqualityComparer<T> equalityComparer;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60001B5
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60001B6
        public System.Void .ctor(System.Collections.Generic.IEqualityComparer<T> equalityComparer) { }
        // RVA: -1  // not resolved  token: 0x60001B7
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.HashSet<T>& value) { }

    }

    // TypeToken: 0x2000057
    public sealed class SortedSetFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Fields
        private readonly System.Collections.Generic.IComparer<T> comparer;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60001B8
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60001B9
        public System.Void .ctor(System.Collections.Generic.IComparer<T> comparer) { }
        // RVA: -1  // not resolved  token: 0x60001BA
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.SortedSet<T>& value) { }

    }

    // TypeToken: 0x2000058
    public sealed class CollectionFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60001BB
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.ObjectModel.Collection<T>& value) { }
        // RVA: -1  // not resolved  token: 0x60001BC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000059
    public sealed class ObservableCollectionFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60001BD
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.ObjectModel.ObservableCollection<T>& value) { }
        // RVA: -1  // not resolved  token: 0x60001BE
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200005A
    public sealed class ConcurrentQueueFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60001BF
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Concurrent.ConcurrentQueue<T>& value) { }
        // RVA: -1  // not resolved  token: 0x60001C0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200005B
    public sealed class ConcurrentStackFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60001C1
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Concurrent.ConcurrentStack<T>& value) { }
        // RVA: -1  // not resolved  token: 0x60001C2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200005C
    public sealed class ConcurrentBagFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60001C3
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Concurrent.ConcurrentBag<T>& value) { }
        // RVA: -1  // not resolved  token: 0x60001C4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200005D
    public sealed class DictionaryFormatter`2 : MemoryPack.MemoryPackFormatter`1
    {
        // Fields
        private readonly System.Collections.Generic.IEqualityComparer<TKey> equalityComparer;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60001C5
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60001C6
        public System.Void .ctor(System.Collections.Generic.IEqualityComparer<TKey> equalityComparer) { }
        // RVA: -1  // not resolved  token: 0x60001C7
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.Dictionary<TKey,TValue>& value) { }

    }

    // TypeToken: 0x200005E
    public sealed class SortedDictionaryFormatter`2 : MemoryPack.MemoryPackFormatter`1
    {
        // Fields
        private readonly System.Collections.Generic.IComparer<TKey> comparer;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60001C8
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60001C9
        public System.Void .ctor(System.Collections.Generic.IComparer<TKey> comparer) { }
        // RVA: -1  // not resolved  token: 0x60001CA
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.SortedDictionary<TKey,TValue>& value) { }

    }

    // TypeToken: 0x200005F
    public sealed class SortedListFormatter`2 : MemoryPack.MemoryPackFormatter`1
    {
        // Fields
        private readonly System.Collections.Generic.IComparer<TKey> comparer;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60001CB
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60001CC
        public System.Void .ctor(System.Collections.Generic.IComparer<TKey> comparer) { }
        // RVA: -1  // not resolved  token: 0x60001CD
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.SortedList<TKey,TValue>& value) { }

    }

    // TypeToken: 0x2000060
    public sealed class ConcurrentDictionaryFormatter`2 : MemoryPack.MemoryPackFormatter`1
    {
        // Fields
        private readonly System.Collections.Generic.IEqualityComparer<TKey> equalityComparer;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60001CE
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60001CF
        public System.Void .ctor(System.Collections.Generic.IEqualityComparer<TKey> equalityComparer) { }
        // RVA: -1  // not resolved  token: 0x60001D0
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>& value) { }

    }

    // TypeToken: 0x2000061
    public sealed class ReadOnlyCollectionFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60001D1
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.ObjectModel.ReadOnlyCollection<T>& value) { }
        // RVA: -1  // not resolved  token: 0x60001D2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000062
    public sealed class ReadOnlyObservableCollectionFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60001D3
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.ObjectModel.ReadOnlyObservableCollection<T>& value) { }
        // RVA: -1  // not resolved  token: 0x60001D4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000063
    public sealed class BlockingCollectionFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60001D5
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Concurrent.BlockingCollection<T>& value) { }
        // RVA: -1  // not resolved  token: 0x60001D6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000064  // size: 0x10
    public sealed class CultureInfoFormatter : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: 0x0A6FFE80  token: 0x60001D7
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Globalization.CultureInfo& value) { }
        // RVA: 0x04D81710  token: 0x60001D8
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000065
    public sealed class DynamicUnionFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Fields
        private readonly System.Collections.Generic.Dictionary<System.Type,System.UInt16> typeToTag;  // 0x0
        private readonly System.Collections.Generic.Dictionary<System.UInt16,System.Type> tagToType;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60001D9
        public System.Void .ctor(System.ValueTuple<System.UInt16,System.Type>[] memoryPackUnions) { }
        // RVA: -1  // not resolved  token: 0x60001DA
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, T& value) { }

    }

    // TypeToken: 0x2000067
    public sealed class GenericCollectionFormatter`2 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60001E1
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, TCollection& value) { }
        // RVA: -1  // not resolved  token: 0x60001E2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000068
    public abstract class GenericSetFormatterBase`2 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60001E3
        protected virtual TSet CreateSet() { }
        // RVA: -1  // not resolved  token: 0x60001E4
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, TSet& value) { }
        // RVA: -1  // not resolved  token: 0x60001E5
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000069
    public sealed class GenericSetFormatter`2 : MemoryPack.Formatters.GenericSetFormatterBase`2
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60001E6
        protected virtual TSet CreateSet() { }
        // RVA: -1  // not resolved  token: 0x60001E7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200006A
    public abstract class GenericDictionaryFormatterBase`3 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60001E8
        protected virtual TDictionary CreateDictionary() { }
        // RVA: -1  // not resolved  token: 0x60001E9
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, TDictionary& value) { }
        // RVA: -1  // not resolved  token: 0x60001EA
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200006B
    public sealed class GenericDictionaryFormatter`3 : MemoryPack.Formatters.GenericDictionaryFormatterBase`3
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60001EB
        protected virtual TDictionary CreateDictionary() { }
        // RVA: -1  // not resolved  token: 0x60001EC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200006C  // size: 0x10
    public static class InterfaceCollectionFormatterUtils
    {
        // Methods
        // RVA: -1  // generic def  token: 0x60001ED
        public static System.Collections.Generic.List<T> ReadList(MemoryPack.MemoryPackReader& reader) { }

    }

    // TypeToken: 0x200006D
    public sealed class InterfaceEnumerableFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60001EE
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.IEnumerable<T>& value) { }
        // RVA: -1  // not resolved  token: 0x60001EF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200006E
    public sealed class InterfaceCollectionFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60001F0
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x60001F1
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.ICollection<T>& value) { }
        // RVA: -1  // not resolved  token: 0x60001F2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200006F
    public sealed class InterfaceReadOnlyCollectionFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60001F3
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x60001F4
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.IReadOnlyCollection<T>& value) { }
        // RVA: -1  // not resolved  token: 0x60001F5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000070
    public sealed class InterfaceListFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60001F6
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x60001F7
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.IList<T>& value) { }
        // RVA: -1  // not resolved  token: 0x60001F8
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000071
    public sealed class InterfaceReadOnlyListFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60001F9
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x60001FA
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.IReadOnlyList<T>& value) { }
        // RVA: -1  // not resolved  token: 0x60001FB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000072
    public sealed class InterfaceDictionaryFormatter`2 : MemoryPack.MemoryPackFormatter`1
    {
        // Fields
        private readonly System.Collections.Generic.IEqualityComparer<TKey> equalityComparer;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60001FC
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60001FD
        public System.Void .ctor(System.Collections.Generic.IEqualityComparer<TKey> equalityComparer) { }
        // RVA: -1  // not resolved  token: 0x60001FE
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.IDictionary<TKey,TValue>& value) { }

    }

    // TypeToken: 0x2000073
    public sealed class InterfaceReadOnlyDictionaryFormatter`2 : MemoryPack.MemoryPackFormatter`1
    {
        // Fields
        private readonly System.Collections.Generic.IEqualityComparer<TKey> equalityComparer;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60001FF
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000200
        public System.Void .ctor(System.Collections.Generic.IEqualityComparer<TKey> equalityComparer) { }
        // RVA: -1  // not resolved  token: 0x6000201
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.IReadOnlyDictionary<TKey,TValue>& value) { }

    }

    // TypeToken: 0x2000074
    public sealed class InterfaceLookupFormatter`2 : MemoryPack.MemoryPackFormatter`1
    {
        // Fields
        private readonly System.Collections.Generic.IEqualityComparer<TKey> equalityComparer;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000202
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x6000203
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000204
        public System.Void .ctor(System.Collections.Generic.IEqualityComparer<TKey> equalityComparer) { }
        // RVA: -1  // not resolved  token: 0x6000205
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Linq.ILookup<TKey,TElement>& value) { }

    }

    // TypeToken: 0x2000075
    public sealed class InterfaceGroupingFormatter`2 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000206
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x6000207
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Linq.IGrouping<TKey,TElement>& value) { }
        // RVA: -1  // not resolved  token: 0x6000208
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000076
    public sealed class InterfaceSetFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Fields
        private readonly System.Collections.Generic.IEqualityComparer<T> equalityComparer;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000209
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x600020A
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x600020B
        public System.Void .ctor(System.Collections.Generic.IEqualityComparer<T> equalityComparer) { }
        // RVA: -1  // not resolved  token: 0x600020C
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.ISet<T>& value) { }

    }

    // TypeToken: 0x2000077
    public sealed class Grouping`2 : System.Linq.IGrouping`2, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly TKey key;  // 0x0
        private readonly System.Collections.Generic.IEnumerable<TElement> elements;  // 0x0

        // Properties
        TKey Key { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600020D
        public System.Void .ctor(TKey key, System.Collections.Generic.IEnumerable<TElement> elements) { }
        // RVA: -1  // not resolved  token: 0x600020F
        public virtual System.Collections.Generic.IEnumerator<TElement> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000210
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000078
    public sealed class Lookup`2 : System.Linq.ILookup`2, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly System.Collections.Generic.Dictionary<TKey,System.Linq.IGrouping<TKey,TElement>> groupings;  // 0x0

        // Properties
        System.Collections.Generic.IEnumerable<TElement> Item { get; /* RVA: -1  // not resolved */ }
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000211
        public System.Void .ctor(System.Collections.Generic.Dictionary<TKey,System.Linq.IGrouping<TKey,TElement>> groupings) { }
        // RVA: -1  // not resolved  token: 0x6000214
        public virtual System.Boolean Contains(TKey key) { }
        // RVA: -1  // not resolved  token: 0x6000215
        public virtual System.Collections.Generic.IEnumerator<System.Linq.IGrouping<TKey,TElement>> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000216
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000079  // size: 0x10
    public static class KeyValuePairFormatter
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000217
        public static System.Void Deserialize(MemoryPack.IMemoryPackFormatter<TKey> keyFormatter, MemoryPack.IMemoryPackFormatter<TValue> valueFormatter, MemoryPack.MemoryPackReader& reader, TKey& key, TValue& value) { }

    }

    // TypeToken: 0x200007A
    public sealed class KeyValuePairFormatter`2 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000218
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Collections.Generic.KeyValuePair<TKey,TValue>& value) { }
        // RVA: -1  // not resolved  token: 0x6000219
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200007B
    public sealed class LazyFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x600021A
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Lazy<T>& value) { }
        // RVA: -1  // not resolved  token: 0x600021B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200007C
    public sealed class TwoDimensionalArrayFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x600021C
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, T[,]& value) { }
        // RVA: -1  // not resolved  token: 0x600021D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200007D
    public sealed class ThreeDimensionalArrayFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x600021E
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, T[,,]& value) { }
        // RVA: -1  // not resolved  token: 0x600021F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200007E
    public sealed class FourDimensionalArrayFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000220
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, T[,,,]& value) { }
        // RVA: -1  // not resolved  token: 0x6000221
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200007F
    public sealed class NullableFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000222
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Nullable<T>& value) { }
        // RVA: -1  // not resolved  token: 0x6000223
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000080  // size: 0x10
    public sealed class StringBuilderFormatter : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: 0x0A707E6C  token: 0x6000224
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Text.StringBuilder& value) { }
        // RVA: 0x04D80CF0  token: 0x6000225
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000081  // size: 0x10
    public sealed class StringFormatter : MemoryPack.MemoryPackFormatter`1
    {
        // Fields
        public static readonly MemoryPack.Formatters.StringFormatter Default;  // static @ 0x0

        // Methods
        // RVA: 0x03C32650  token: 0x6000226
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.String& value) { }
        // RVA: 0x04D109A0  token: 0x6000227
        public System.Void .ctor() { }
        // RVA: 0x04D10930  token: 0x6000228
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000082  // size: 0x10
    public sealed class Utf8StringFormatter : MemoryPack.MemoryPackFormatter`1
    {
        // Fields
        public static readonly MemoryPack.Formatters.Utf8StringFormatter Default;  // static @ 0x0

        // Methods
        // RVA: 0x067C5DC0  token: 0x6000229
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.String& value) { }
        // RVA: 0x0A70B12C  token: 0x600022A
        public System.Void .ctor() { }
        // RVA: 0x0A70B0BC  token: 0x600022B
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000083  // size: 0x10
    public sealed class Utf16StringFormatter : MemoryPack.MemoryPackFormatter`1
    {
        // Fields
        public static readonly MemoryPack.Formatters.Utf16StringFormatter Default;  // static @ 0x0

        // Methods
        // RVA: 0x067C5DC0  token: 0x600022C
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.String& value) { }
        // RVA: 0x0A70B094  token: 0x600022D
        public System.Void .ctor() { }
        // RVA: 0x0A70B024  token: 0x600022E
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000084  // size: 0x10
    public sealed class InternStringFormatter : MemoryPack.MemoryPackFormatter`1
    {
        // Fields
        public static readonly MemoryPack.Formatters.InternStringFormatter Default;  // static @ 0x0

        // Methods
        // RVA: 0x0A700444  token: 0x600022F
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.String& value) { }
        // RVA: 0x0A7004F0  token: 0x6000230
        public System.Void .ctor() { }
        // RVA: 0x0A700480  token: 0x6000231
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000085  // size: 0x20
    public sealed class BrotliStringFormatter : MemoryPack.MemoryPackFormatter`1
    {
        // Fields
        private static System.Runtime.CompilerServices.StrongBox<System.Int32> threadStaticConsumedBox;  // static @ 0xffffffff
        private static System.Int32 DefaultDecompssionSizeLimit;  // const
        public static readonly MemoryPack.Formatters.BrotliStringFormatter Default;  // static @ 0x0
        private readonly System.IO.Compression.CompressionLevel compressionLevel;  // 0x10
        private readonly System.Int32 window;  // 0x14
        private readonly System.Int32 decompressionSizeLimit;  // 0x18

        // Methods
        // RVA: 0x0A6FFB64  token: 0x6000232
        public System.Void .ctor() { }
        // RVA: 0x0A6FFB18  token: 0x6000233
        public System.Void .ctor(System.IO.Compression.CompressionLevel compressionLevel) { }
        // RVA: 0x0A6FFB44  token: 0x6000234
        public System.Void .ctor(System.IO.Compression.CompressionLevel compressionLevel, System.Int32 window) { }
        // RVA: 0x0A6FFB74  token: 0x6000235
        public System.Void .ctor(System.IO.Compression.CompressionLevel compressionLevel, System.Int32 window, System.Int32 decompressionSizeLimit) { }
        // RVA: 0x0A6FF5D0  token: 0x6000236
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.String& value) { }
        // RVA: 0x0A6FFAA8  token: 0x6000237
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000087  // size: 0x10
    public sealed class TimeZoneInfoFormatter : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: 0x0A708014  token: 0x600023C
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.TimeZoneInfo& value) { }
        // RVA: 0x04D80C90  token: 0x600023D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000088  // size: 0x10
    public static class TupleFormatterTypes
    {
        // Fields
        public static readonly System.Collections.Generic.Dictionary<System.Type,System.Type> TupleFormatters;  // static @ 0x0

        // Methods
        // RVA: 0x0A70808C  token: 0x600023E
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000089
    public sealed class TupleFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x600023F
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Tuple<T1>& value) { }
        // RVA: -1  // not resolved  token: 0x6000240
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200008A
    public sealed class TupleFormatter`2 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000241
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Tuple<T1,T2>& value) { }
        // RVA: -1  // not resolved  token: 0x6000242
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200008B
    public sealed class TupleFormatter`3 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000243
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Tuple<T1,T2,T3>& value) { }
        // RVA: -1  // not resolved  token: 0x6000244
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200008C
    public sealed class TupleFormatter`4 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000245
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Tuple<T1,T2,T3,T4>& value) { }
        // RVA: -1  // not resolved  token: 0x6000246
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200008D
    public sealed class TupleFormatter`5 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000247
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Tuple<T1,T2,T3,T4,T5>& value) { }
        // RVA: -1  // not resolved  token: 0x6000248
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200008E
    public sealed class TupleFormatter`6 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000249
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Tuple<T1,T2,T3,T4,T5,T6>& value) { }
        // RVA: -1  // not resolved  token: 0x600024A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200008F
    public sealed class TupleFormatter`7 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x600024B
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Tuple<T1,T2,T3,T4,T5,T6,T7>& value) { }
        // RVA: -1  // not resolved  token: 0x600024C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000090
    public sealed class TupleFormatter`8 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x600024D
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Tuple<T1,T2,T3,T4,T5,T6,T7,TRest>& value) { }
        // RVA: -1  // not resolved  token: 0x600024E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000091
    public sealed class ValueTupleFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x600024F
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.ValueTuple<T1>& value) { }
        // RVA: -1  // not resolved  token: 0x6000250
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000092
    public sealed class ValueTupleFormatter`2 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000251
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.ValueTuple<T1,T2>& value) { }
        // RVA: -1  // not resolved  token: 0x6000252
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000093
    public sealed class ValueTupleFormatter`3 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000253
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.ValueTuple<T1,T2,T3>& value) { }
        // RVA: -1  // not resolved  token: 0x6000254
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000094
    public sealed class ValueTupleFormatter`4 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000255
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.ValueTuple<T1,T2,T3,T4>& value) { }
        // RVA: -1  // not resolved  token: 0x6000256
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000095
    public sealed class ValueTupleFormatter`5 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000257
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.ValueTuple<T1,T2,T3,T4,T5>& value) { }
        // RVA: -1  // not resolved  token: 0x6000258
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000096
    public sealed class ValueTupleFormatter`6 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000259
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.ValueTuple<T1,T2,T3,T4,T5,T6>& value) { }
        // RVA: -1  // not resolved  token: 0x600025A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000097
    public sealed class ValueTupleFormatter`7 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x600025B
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.ValueTuple<T1,T2,T3,T4,T5,T6,T7>& value) { }
        // RVA: -1  // not resolved  token: 0x600025C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000098
    public sealed class ValueTupleFormatter`8 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x600025D
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.ValueTuple<T1,T2,T3,T4,T5,T6,T7,TRest>& value) { }
        // RVA: -1  // not resolved  token: 0x600025E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000099  // size: 0x10
    public sealed class TypeFormatter : MemoryPack.MemoryPackFormatter`1
    {
        // Fields
        private static readonly System.Text.RegularExpressions.Regex _shortTypeNameRegex;  // static @ 0x0

        // Methods
        // RVA: 0x0A7086A8  token: 0x600025F
        private static System.Text.RegularExpressions.Regex ShortTypeNameRegex() { }
        // RVA: 0x0A708614  token: 0x6000260
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Type& value) { }
        // RVA: 0x04D80C00  token: 0x6000261
        public System.Void .ctor() { }
        // RVA: 0x04CF7430  token: 0x6000262
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200009A
    public sealed class UnmanagedFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000263
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, T& value) { }
        // RVA: -1  // not resolved  token: 0x6000264
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200009B
    public sealed class DangerousUnmanagedFormatter`1 : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000265
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, T& value) { }
        // RVA: -1  // not resolved  token: 0x6000266
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200009C  // size: 0x10
    public sealed class UriFormatter : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: 0x0A70AF98  token: 0x6000267
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Uri& value) { }
        // RVA: 0x04D80BA0  token: 0x6000268
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200009D  // size: 0x10
    public sealed class VersionFormatter : MemoryPack.MemoryPackFormatter`1
    {
        // Methods
        // RVA: 0x0A70B154  token: 0x6000269
        public virtual System.Void Deserialize(MemoryPack.MemoryPackReader& reader, System.Version& value) { }
        // RVA: 0x04D80B10  token: 0x600026A
        public System.Void .ctor() { }

    }

}

namespace MemoryPack.Internal
{

    // TypeToken: 0x2000034  // size: 0x10
    public static class EnumerableExtensions
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000158
        public static System.Boolean TryGetNonEnumeratedCountEx(System.Collections.Generic.IEnumerable<T> value, System.Int32& count) { }

    }

    // TypeToken: 0x2000035  // size: 0x20
    public sealed struct FixedArrayBufferWriter : System.Buffers.IBufferWriter`1
    {
        // Fields
        private System.Byte[] buffer;  // 0x10
        private System.Int32 written;  // 0x18

        // Methods
        // RVA: 0x05393828  token: 0x6000159
        public System.Void .ctor(System.Byte[] buffer) { }
        // RVA: 0x04DA3A90  token: 0x600015A
        public virtual System.Void Advance(System.Int32 count) { }
        // RVA: 0x0A7001B4  token: 0x600015B
        public virtual System.Memory<System.Byte> GetMemory(System.Int32 sizeHint) { }
        // RVA: 0x0A700254  token: 0x600015C
        public virtual System.Span<System.Byte> GetSpan(System.Int32 sizeHint) { }
        // RVA: 0x0A700164  token: 0x600015D
        public System.Byte[] GetFilledBuffer() { }

    }

    // TypeToken: 0x2000036  // size: 0x10
    public static class MathEx
    {
        // Fields
        private static System.Int32 ArrayMexLength;  // const

        // Methods
        // RVA: 0x0A700518  token: 0x600015E
        public static System.Int32 NewArrayCapacity(System.Int32 size) { }

    }

    // TypeToken: 0x2000037  // size: 0x10
    public static class MemoryMarshalEx
    {
        // Methods
        // RVA: -1  // generic def  token: 0x600015F
        public static T& GetArrayDataReference(T[] array) { }
        // RVA: -1  // generic def  token: 0x6000160
        public static T[] AllocateUninitializedArray(System.Int32 length, System.Boolean pinned) { }

    }

    // TypeToken: 0x2000038  // size: 0x10
    public sealed class PreserveAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000161
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000039  // size: 0x10
    public static class ReusableLinkedArrayBufferWriterPool
    {
        // Fields
        private static readonly System.Collections.Concurrent.ConcurrentQueue<MemoryPack.Internal.ReusableLinkedArrayBufferWriter> queue;  // static @ 0x0

        // Methods
        // RVA: 0x0A706500  token: 0x6000162
        public static MemoryPack.Internal.ReusableLinkedArrayBufferWriter Rent() { }
        // RVA: 0x0A7065B4  token: 0x6000163
        public static System.Void Return(MemoryPack.Internal.ReusableLinkedArrayBufferWriter writer) { }
        // RVA: 0x0A706630  token: 0x6000164
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200003A  // size: 0x40
    public sealed class ReusableLinkedArrayBufferWriter : System.Buffers.IBufferWriter`1
    {
        // Fields
        private static System.Int32 InitialBufferSize;  // const
        private static readonly System.Byte[] noUseFirstBufferSentinel;  // static @ 0x0
        private System.Collections.Generic.List<MemoryPack.Internal.BufferSegment> buffers;  // 0x10
        private System.Byte[] firstBuffer;  // 0x18
        private System.Int32 firstBufferWritten;  // 0x20
        private MemoryPack.Internal.BufferSegment current;  // 0x28
        private System.Int32 nextBufferSize;  // 0x38
        private System.Int32 totalWritten;  // 0x3c

        // Properties
        System.Int32 TotalWritten { get; /* RVA: 0x04D86640 */ }
        System.Boolean UseFirstBuffer { get; /* RVA: 0x0A707338 */ }

        // Methods
        // RVA: 0x0A707234  token: 0x6000167
        public System.Void .ctor(System.Boolean useFirstBuffer, System.Boolean pinned) { }
        // RVA: 0x0385B100  token: 0x6000168
        public System.Byte[] DangerousGetFirstBuffer() { }
        // RVA: 0x0A706708  token: 0x6000169
        public virtual System.Memory<System.Byte> GetMemory(System.Int32 sizeHint) { }
        // RVA: 0x0A706754  token: 0x600016A
        public virtual System.Span<System.Byte> GetSpan(System.Int32 sizeHint) { }
        // RVA: 0x0A7066BC  token: 0x600016B
        public virtual System.Void Advance(System.Int32 count) { }
        // RVA: 0x0A706A44  token: 0x600016C
        public System.Byte[] ToArrayAndReset() { }
        // RVA: 0x0A706E94  token: 0x600016D
        public System.Void WriteToAndReset(MemoryPack.MemoryPackWriter& writer) { }
        // RVA: 0x0A706DB0  token: 0x600016E
        public System.Threading.Tasks.ValueTask WriteToAndResetAsync(System.IO.Stream stream, System.Threading.CancellationToken cancellationToken) { }
        // RVA: 0x0A7066DC  token: 0x600016F
        public MemoryPack.Internal.ReusableLinkedArrayBufferWriter.Enumerator GetEnumerator() { }
        // RVA: 0x0A706898  token: 0x6000170
        private System.Void ResetCore() { }
        // RVA: 0x0A7068F4  token: 0x6000171
        public System.Void Reset() { }
        // RVA: 0x0A7071D0  token: 0x6000172
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200003E  // size: 0x20
    public sealed struct BufferSegment
    {
        // Fields
        private System.Byte[] buffer;  // 0x10
        private System.Int32 written;  // 0x18

        // Properties
        System.Boolean IsNull { get; /* RVA: 0x0314CD60 */ }
        System.Int32 WrittenCount { get; /* RVA: 0x04D88B10 */ }
        System.Span<System.Byte> WrittenBuffer { get; /* RVA: 0x0A6FFDB8 */ }
        System.Memory<System.Byte> WrittenMemory { get; /* RVA: 0x0A6FFE1C */ }
        System.Span<System.Byte> FreeBuffer { get; /* RVA: 0x0A6FFD64 */ }

        // Methods
        // RVA: 0x0A6FFCE4  token: 0x6000180
        public System.Void .ctor(System.Int32 size) { }
        // RVA: 0x04DA3A90  token: 0x6000181
        public System.Void Advance(System.Int32 count) { }
        // RVA: 0x0A6FFC68  token: 0x6000182
        public System.Void Clear() { }

    }

    // TypeToken: 0x200003F  // size: 0x10
    public static class ReusableReadOnlySequenceBuilderPool
    {
        // Fields
        private static readonly System.Collections.Concurrent.ConcurrentQueue<MemoryPack.Internal.ReusableReadOnlySequenceBuilder> queue;  // static @ 0x0

        // Methods
        // RVA: 0x0A707388  token: 0x6000183
        public static MemoryPack.Internal.ReusableReadOnlySequenceBuilder Rent() { }
        // RVA: 0x0A707434  token: 0x6000184
        public static System.Void Return(MemoryPack.Internal.ReusableReadOnlySequenceBuilder builder) { }
        // RVA: 0x0A7074B0  token: 0x6000185
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000040  // size: 0x20
    public sealed class ReusableReadOnlySequenceBuilder
    {
        // Fields
        private readonly System.Collections.Generic.Stack<MemoryPack.Internal.ReusableReadOnlySequenceBuilder.Segment> segmentPool;  // 0x10
        private readonly System.Collections.Generic.List<MemoryPack.Internal.ReusableReadOnlySequenceBuilder.Segment> list;  // 0x18

        // Methods
        // RVA: 0x0A707A80  token: 0x6000186
        public System.Void .ctor() { }
        // RVA: 0x0A70753C  token: 0x6000187
        public System.Void Add(System.ReadOnlyMemory<System.Byte> buffer, System.Boolean returnToPool) { }
        // RVA: 0x0A7079E4  token: 0x6000188
        public System.Boolean TryGetSingleMemory(System.ReadOnlyMemory<System.Byte>& memory) { }
        // RVA: 0x0A707638  token: 0x6000189
        public System.Buffers.ReadOnlySequence<System.Byte> Build() { }
        // RVA: 0x0A70786C  token: 0x600018A
        public System.Void Reset() { }

    }

    // TypeToken: 0x2000042  // size: 0x10
    public static class TypeHelpers
    {
        // Fields
        private static readonly System.Reflection.MethodInfo isReferenceOrContainsReferences;  // static @ 0x0
        private static readonly System.Reflection.MethodInfo unsafeSizeOf;  // static @ 0x8

        // Methods
        // RVA: -1  // generic def  token: 0x600018F
        public static System.Boolean IsReferenceOrNullable() { }
        // RVA: -1  // generic def  token: 0x6000190
        public static MemoryPack.Internal.TypeHelpers.TypeKind TryGetUnmanagedSZArrayElementSizeOrMemoryPackableFixedSize(System.Int32& size) { }
        // RVA: 0x0A7086F8  token: 0x6000191
        public static System.Boolean IsAnonymous(System.Type type) { }
        // RVA: 0x0A708850  token: 0x6000192
        private static System.Void .cctor() { }

    }

}

namespace Microsoft.CodeAnalysis
{

    // TypeToken: 0x2000002  // size: 0x10
    public sealed class EmbeddedAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000001
        public System.Void .ctor() { }

    }

}

namespace System.Runtime.CompilerServices
{

    // TypeToken: 0x2000003  // size: 0x10
    public sealed class IsUnmanagedAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000002
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000004  // size: 0x18
    public sealed class NullableAttribute : System.Attribute
    {
        // Fields
        public readonly System.Byte[] NullableFlags;  // 0x10

        // Methods
        // RVA: 0x03F26320  token: 0x6000003
        public System.Void .ctor(System.Byte ) { }
        // RVA: 0x053908C0  token: 0x6000004
        public System.Void .ctor(System.Byte[] ) { }

    }

    // TypeToken: 0x2000005  // size: 0x18
    public sealed class NullableContextAttribute : System.Attribute
    {
        // Fields
        public readonly System.Byte Flag;  // 0x10

        // Methods
        // RVA: 0x04D86130  token: 0x6000005
        public System.Void .ctor(System.Byte ) { }

    }

    // TypeToken: 0x2000007  // size: 0x10
    public sealed class IsExternalInit
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000007
        public System.Void .ctor() { }

    }

}

