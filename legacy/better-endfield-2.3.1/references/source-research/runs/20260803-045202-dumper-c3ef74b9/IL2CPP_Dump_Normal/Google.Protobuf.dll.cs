// ========================================================
// Dumped by @desirepro
// Assembly: Google.Protobuf.dll
// Classes:  318
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000009  // size: 0x50
    public sealed struct <FromStreamAsyncCore>d__0 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncTaskMethodBuilder<Google.Protobuf.ByteString> <>t__builder;  // 0x18
        public System.IO.Stream stream;  // 0x30
        public System.Threading.CancellationToken cancellationToken;  // 0x38
        private System.IO.MemoryStream <memoryStream>5__2;  // 0x40
        private System.Runtime.CompilerServices.TaskAwaiter <>u__1;  // 0x48

        // Methods
        // RVA: 0x09AE1984  token: 0x6000035
        private virtual System.Void MoveNext() { }
        // RVA: 0x09AE1CF0  token: 0x6000036
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x200000C  // size: 0x90
    public sealed class OutOfSpaceException : System.IO.IOException
    {
        // Methods
        // RVA: 0x09AE0AFC  token: 0x60000B7
        private System.Void .ctor() { }

    }

    // TypeToken: 0x2000011  // size: 0x10
    public sealed class ExtensionComparer : System.Collections.Generic.IEqualityComparer`1
    {
        // Fields
        private static Google.Protobuf.ExtensionRegistry.ExtensionComparer Instance;  // static @ 0x0

        // Methods
        // RVA: 0x09ACF470  token: 0x60000D4
        public virtual System.Boolean Equals(Google.Protobuf.Extension a, Google.Protobuf.Extension b) { }
        // RVA: 0x09ACF544  token: 0x60000D5
        public virtual System.Int32 GetHashCode(Google.Protobuf.Extension a) { }
        // RVA: 0x041E1670  token: 0x60000D6
        public System.Void .ctor() { }
        // RVA: 0x09ACF5CC  token: 0x60000D7
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000012  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.ExtensionRegistry.<>c <>9;  // static @ 0x0
        public static System.Func<System.Collections.Generic.KeyValuePair<Google.Protobuf.ObjectIntPair<System.Type>,Google.Protobuf.Extension>,Google.Protobuf.ObjectIntPair<System.Type>> <>9__3_0;  // static @ 0x8
        public static System.Func<System.Collections.Generic.KeyValuePair<Google.Protobuf.ObjectIntPair<System.Type>,Google.Protobuf.Extension>,Google.Protobuf.Extension> <>9__3_1;  // static @ 0x10

        // Methods
        // RVA: 0x09AE25C4  token: 0x60000D8
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60000D9
        public System.Void .ctor() { }
        // RVA: 0x09AE22A0  token: 0x60000DA
        private Google.Protobuf.ObjectIntPair<System.Type> <.ctor>b__3_0(System.Collections.Generic.KeyValuePair<Google.Protobuf.ObjectIntPair<System.Type>,Google.Protobuf.Extension> k) { }
        // RVA: 0x09AE22E0  token: 0x60000DB
        private Google.Protobuf.Extension <.ctor>b__3_1(System.Collections.Generic.KeyValuePair<Google.Protobuf.ObjectIntPair<System.Type>,Google.Protobuf.Extension> v) { }

    }

    // TypeToken: 0x2000015
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.ExtensionSet.<>c<TTarget> <>9;  // static @ 0x0
        public static System.Func<Google.Protobuf.IExtensionValue,System.Boolean> <>9__8_0;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60000F0
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x60000F1
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60000F2
        private System.Boolean <IsInitialized>b__8_0(Google.Protobuf.IExtensionValue v) { }

    }

    // TypeToken: 0x200001B
    public sealed class <>c__DisplayClass3_0`1
    {
        // Fields
        public Google.Protobuf.FieldCodec<T> nestedCoded;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600013A
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x600013B
        private System.Nullable<T> <GetReader>b__0(Google.Protobuf.ParseContext& ctx) { }

    }

    // TypeToken: 0x200001A  // size: 0x10
    public static class WrapperCodecs
    {
        // Fields
        private static readonly System.Collections.Generic.Dictionary<System.Type,System.Object> Codecs;  // static @ 0x0
        private static readonly System.Collections.Generic.Dictionary<System.Type,System.Object> Readers;  // static @ 0x8

        // Methods
        // RVA: -1  // generic def  token: 0x6000134
        private static Google.Protobuf.FieldCodec<T> GetCodec() { }
        // RVA: -1  // generic def  token: 0x6000135
        private static Google.Protobuf.ValueReader<System.Nullable<T>> GetReader() { }
        // RVA: -1  // generic def  token: 0x6000136
        private static T Read(Google.Protobuf.ParseContext& ctx, Google.Protobuf.FieldCodec<T> codec) { }
        // RVA: -1  // generic def  token: 0x6000137
        private static System.Void Write(Google.Protobuf.WriteContext& ctx, T value, Google.Protobuf.FieldCodec<T> codec) { }
        // RVA: -1  // generic def  token: 0x6000138
        private static System.Int32 CalculateSize(T value, Google.Protobuf.FieldCodec<T> codec) { }
        // RVA: 0x09AE268C  token: 0x6000139
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200001C  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.FieldCodec.<>c <>9;  // static @ 0x0
        public static Google.Protobuf.ValueReader<System.String> <>9__16_0;  // static @ 0x8
        public static Google.Protobuf.ValueWriter<System.String> <>9__16_1;  // static @ 0x10
        public static Google.Protobuf.ValueReader<Google.Protobuf.ByteString> <>9__17_0;  // static @ 0x18
        public static Google.Protobuf.ValueWriter<Google.Protobuf.ByteString> <>9__17_1;  // static @ 0x20
        public static Google.Protobuf.ValueReader<System.Boolean> <>9__18_0;  // static @ 0x28
        public static Google.Protobuf.ValueWriter<System.Boolean> <>9__18_1;  // static @ 0x30
        public static Google.Protobuf.ValueReader<System.Int32> <>9__19_0;  // static @ 0x38
        public static Google.Protobuf.ValueWriter<System.Int32> <>9__19_1;  // static @ 0x40
        public static Google.Protobuf.ValueReader<System.Int32> <>9__20_0;  // static @ 0x48
        public static Google.Protobuf.ValueWriter<System.Int32> <>9__20_1;  // static @ 0x50
        public static Google.Protobuf.ValueReader<System.UInt32> <>9__21_0;  // static @ 0x58
        public static Google.Protobuf.ValueWriter<System.UInt32> <>9__21_1;  // static @ 0x60
        public static Google.Protobuf.ValueReader<System.Int32> <>9__22_0;  // static @ 0x68
        public static Google.Protobuf.ValueWriter<System.Int32> <>9__22_1;  // static @ 0x70
        public static Google.Protobuf.ValueReader<System.UInt32> <>9__23_0;  // static @ 0x78
        public static Google.Protobuf.ValueWriter<System.UInt32> <>9__23_1;  // static @ 0x80
        public static Google.Protobuf.ValueReader<System.Int64> <>9__24_0;  // static @ 0x88
        public static Google.Protobuf.ValueWriter<System.Int64> <>9__24_1;  // static @ 0x90
        public static Google.Protobuf.ValueReader<System.Int64> <>9__25_0;  // static @ 0x98
        public static Google.Protobuf.ValueWriter<System.Int64> <>9__25_1;  // static @ 0xa0
        public static Google.Protobuf.ValueReader<System.UInt64> <>9__26_0;  // static @ 0xa8
        public static Google.Protobuf.ValueWriter<System.UInt64> <>9__26_1;  // static @ 0xb0
        public static Google.Protobuf.ValueReader<System.Int64> <>9__27_0;  // static @ 0xb8
        public static Google.Protobuf.ValueWriter<System.Int64> <>9__27_1;  // static @ 0xc0
        public static Google.Protobuf.ValueReader<System.UInt64> <>9__28_0;  // static @ 0xc8
        public static Google.Protobuf.ValueWriter<System.UInt64> <>9__28_1;  // static @ 0xd0
        public static Google.Protobuf.ValueReader<System.Single> <>9__29_0;  // static @ 0xd8
        public static Google.Protobuf.ValueWriter<System.Single> <>9__29_1;  // static @ 0xe0
        public static Google.Protobuf.ValueReader<System.Double> <>9__30_0;  // static @ 0xe8
        public static Google.Protobuf.ValueWriter<System.Double> <>9__30_1;  // static @ 0xf0

        // Methods
        // RVA: 0x04D43B10  token: 0x600013C
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600013D
        public System.Void .ctor() { }
        // RVA: 0x03537440  token: 0x600013E
        private System.String <ForString>b__16_0(Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x04693A60  token: 0x600013F
        private System.Void <ForString>b__16_1(Google.Protobuf.WriteContext& ctx, System.String value) { }
        // RVA: 0x09AE1D50  token: 0x6000140
        private Google.Protobuf.ByteString <ForBytes>b__17_0(Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x09AE1D5C  token: 0x6000141
        private System.Void <ForBytes>b__17_1(Google.Protobuf.WriteContext& ctx, Google.Protobuf.ByteString value) { }
        // RVA: 0x035369C0  token: 0x6000142
        private System.Boolean <ForBool>b__18_0(Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x03076EF0  token: 0x6000143
        private System.Void <ForBool>b__18_1(Google.Protobuf.WriteContext& ctx, System.Boolean value) { }
        // RVA: 0x03537920  token: 0x6000144
        private System.Int32 <ForInt32>b__19_0(Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x09AE1DF4  token: 0x6000145
        private System.Void <ForInt32>b__19_1(Google.Protobuf.WriteContext& output, System.Int32 value) { }
        // RVA: 0x09AE1E28  token: 0x6000146
        private System.Int32 <ForSInt32>b__20_0(Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x09AE1E4C  token: 0x6000147
        private System.Void <ForSInt32>b__20_1(Google.Protobuf.WriteContext& output, System.Int32 value) { }
        // RVA: 0x09AE1D90  token: 0x6000148
        private System.UInt32 <ForFixed32>b__21_0(Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x09AE1DA0  token: 0x6000149
        private System.Void <ForFixed32>b__21_1(Google.Protobuf.WriteContext& output, System.UInt32 value) { }
        // RVA: 0x09AE1D90  token: 0x600014A
        private System.Int32 <ForSFixed32>b__22_0(Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x09AE1E04  token: 0x600014B
        private System.Void <ForSFixed32>b__22_1(Google.Protobuf.WriteContext& output, System.Int32 value) { }
        // RVA: 0x03539A40  token: 0x600014C
        private System.UInt32 <ForUInt32>b__23_0(Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x09AE1E98  token: 0x600014D
        private System.Void <ForUInt32>b__23_1(Google.Protobuf.WriteContext& output, System.UInt32 value) { }
        // RVA: 0x035371A0  token: 0x600014E
        private System.Int64 <ForInt64>b__24_0(Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x04B3A6F0  token: 0x600014F
        private System.Void <ForInt64>b__24_1(Google.Protobuf.WriteContext& output, System.Int64 value) { }
        // RVA: 0x09AE1E5C  token: 0x6000150
        private System.Int64 <ForSInt64>b__25_0(Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x09AE1E84  token: 0x6000151
        private System.Void <ForSInt64>b__25_1(Google.Protobuf.WriteContext& output, System.Int64 value) { }
        // RVA: 0x09AE1DB0  token: 0x6000152
        private System.UInt64 <ForFixed64>b__26_0(Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x09AE1DC0  token: 0x6000153
        private System.Void <ForFixed64>b__26_1(Google.Protobuf.WriteContext& output, System.UInt64 value) { }
        // RVA: 0x09AE1DB0  token: 0x6000154
        private System.Int64 <ForSFixed64>b__27_0(Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x09AE1E14  token: 0x6000155
        private System.Void <ForSFixed64>b__27_1(Google.Protobuf.WriteContext& output, System.Int64 value) { }
        // RVA: 0x0353B690  token: 0x6000156
        private System.UInt64 <ForUInt64>b__28_0(Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x0484C110  token: 0x6000157
        private System.Void <ForUInt64>b__28_1(Google.Protobuf.WriteContext& output, System.UInt64 value) { }
        // RVA: 0x09AE1DD4  token: 0x6000158
        private System.Single <ForFloat>b__29_0(Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x09AE1DE4  token: 0x6000159
        private System.Void <ForFloat>b__29_1(Google.Protobuf.WriteContext& output, System.Single value) { }
        // RVA: 0x09AE1D70  token: 0x600015A
        private System.Double <ForDouble>b__30_0(Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x09AE1D80  token: 0x600015B
        private System.Void <ForDouble>b__30_1(Google.Protobuf.WriteContext& output, System.Double value) { }

    }

    // TypeToken: 0x200001D
    public sealed class <>c__DisplayClass31_0`1
    {
        // Fields
        public System.Func<System.Int32,T> fromInt32;  // 0x0
        public System.Func<T,System.Int32> toInt32;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600015C
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x600015D
        private T <ForEnum>b__0(Google.Protobuf.ParseContext& ctx) { }
        // RVA: -1  // not resolved  token: 0x600015E
        private System.Void <ForEnum>b__1(Google.Protobuf.WriteContext& output, T value) { }
        // RVA: -1  // not resolved  token: 0x600015F
        private System.Int32 <ForEnum>b__2(T value) { }

    }

    // TypeToken: 0x200001E
    public sealed class <>c__DisplayClass32_0`1
    {
        // Fields
        public Google.Protobuf.MessageParser<T> parser;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000160
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000161
        private T <ForMessage>b__0(Google.Protobuf.ParseContext& ctx) { }
        // RVA: -1  // not resolved  token: 0x6000162
        private System.Void <ForMessage>b__2(Google.Protobuf.ParseContext& ctx, T& v) { }

    }

    // TypeToken: 0x200001F
    public sealed class <>c__32`1
    {
        // Fields
        public static readonly Google.Protobuf.FieldCodec.<>c__32<T> <>9;  // static @ 0x0
        public static Google.Protobuf.ValueWriter<T> <>9__32_1;  // static @ 0x0
        public static Google.Protobuf.FieldCodec.ValuesMerger<T> <>9__32_3;  // static @ 0x0
        public static System.Func<T,System.Int32> <>9__32_4;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000163
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x6000164
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000165
        private System.Void <ForMessage>b__32_1(Google.Protobuf.WriteContext& output, T value) { }
        // RVA: -1  // not resolved  token: 0x6000166
        private System.Boolean <ForMessage>b__32_3(T& v, T v2) { }
        // RVA: -1  // not resolved  token: 0x6000167
        private System.Int32 <ForMessage>b__32_4(T message) { }

    }

    // TypeToken: 0x2000020
    public sealed class <>c__DisplayClass33_0`1
    {
        // Fields
        public Google.Protobuf.MessageParser<T> parser;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000168
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000169
        private T <ForGroup>b__0(Google.Protobuf.ParseContext& ctx) { }
        // RVA: -1  // not resolved  token: 0x600016A
        private System.Void <ForGroup>b__2(Google.Protobuf.ParseContext& ctx, T& v) { }

    }

    // TypeToken: 0x2000021
    public sealed class <>c__33`1
    {
        // Fields
        public static readonly Google.Protobuf.FieldCodec.<>c__33<T> <>9;  // static @ 0x0
        public static Google.Protobuf.ValueWriter<T> <>9__33_1;  // static @ 0x0
        public static Google.Protobuf.FieldCodec.ValuesMerger<T> <>9__33_3;  // static @ 0x0
        public static System.Func<T,System.Int32> <>9__33_4;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600016B
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x600016C
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x600016D
        private System.Void <ForGroup>b__33_1(Google.Protobuf.WriteContext& output, T value) { }
        // RVA: -1  // not resolved  token: 0x600016E
        private System.Boolean <ForGroup>b__33_3(T& v, T v2) { }
        // RVA: -1  // not resolved  token: 0x600016F
        private System.Int32 <ForGroup>b__33_4(T message) { }

    }

    // TypeToken: 0x2000022
    public sealed class <>c__DisplayClass34_0`1
    {
        // Fields
        public Google.Protobuf.FieldCodec<T> nestedCodec;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000170
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000171
        private T <ForClassWrapper>b__0(Google.Protobuf.ParseContext& ctx) { }
        // RVA: -1  // not resolved  token: 0x6000172
        private System.Void <ForClassWrapper>b__1(Google.Protobuf.WriteContext& output, T value) { }
        // RVA: -1  // not resolved  token: 0x6000173
        private System.Void <ForClassWrapper>b__2(Google.Protobuf.ParseContext& ctx, T& v) { }
        // RVA: -1  // not resolved  token: 0x6000174
        private System.Int32 <ForClassWrapper>b__4(T value) { }

    }

    // TypeToken: 0x2000023
    public sealed class <>c__34`1
    {
        // Fields
        public static readonly Google.Protobuf.FieldCodec.<>c__34<T> <>9;  // static @ 0x0
        public static Google.Protobuf.FieldCodec.ValuesMerger<T> <>9__34_3;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000175
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x6000176
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000177
        private System.Boolean <ForClassWrapper>b__34_3(T& v, T v2) { }

    }

    // TypeToken: 0x2000024
    public sealed class <>c__DisplayClass35_0`1
    {
        // Fields
        public Google.Protobuf.FieldCodec<T> nestedCodec;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000178
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000179
        private System.Void <ForStructWrapper>b__0(Google.Protobuf.WriteContext& output, System.Nullable<T> value) { }
        // RVA: -1  // not resolved  token: 0x600017A
        private System.Void <ForStructWrapper>b__1(Google.Protobuf.ParseContext& ctx, System.Nullable<T>& v) { }
        // RVA: -1  // not resolved  token: 0x600017B
        private System.Int32 <ForStructWrapper>b__3(System.Nullable<T> value) { }

    }

    // TypeToken: 0x2000025
    public sealed class <>c__35`1
    {
        // Fields
        public static readonly Google.Protobuf.FieldCodec.<>c__35<T> <>9;  // static @ 0x0
        public static Google.Protobuf.FieldCodec.ValuesMerger<System.Nullable<T>> <>9__35_2;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600017C
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x600017D
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x600017E
        private System.Boolean <ForStructWrapper>b__35_2(System.Nullable<T>& v, System.Nullable<T> v2) { }

    }

    // TypeToken: 0x2000029
    public sealed class InputMerger : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x600019E
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x600019F
        public virtual System.Void Invoke(Google.Protobuf.ParseContext& ctx, T& value) { }
        // RVA: -1  // runtime  token: 0x60001A0
        public virtual System.IAsyncResult BeginInvoke(Google.Protobuf.ParseContext& ctx, T& value, System.AsyncCallback callback, System.Object object) { }
        // RVA: -1  // runtime  token: 0x60001A1
        public virtual System.Void EndInvoke(Google.Protobuf.ParseContext& ctx, T& value, System.IAsyncResult result) { }

    }

    // TypeToken: 0x200002A
    public sealed class ValuesMerger : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x60001A2
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x60001A3
        public virtual System.Boolean Invoke(T& value, T other) { }
        // RVA: -1  // runtime  token: 0x60001A4
        public virtual System.IAsyncResult BeginInvoke(T& value, T other, System.AsyncCallback callback, System.Object object) { }
        // RVA: -1  // runtime  token: 0x60001A5
        public virtual System.Boolean EndInvoke(T& value, System.IAsyncResult result) { }

    }

    // TypeToken: 0x200002B
    public sealed class <>c__DisplayClass38_0
    {
        // Fields
        public System.Int32 fixedSize;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60001A6
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60001A7
        private System.Int32 <.ctor>b__0(T _) { }

    }

    // TypeToken: 0x200002C
    public sealed class <>c__DisplayClass39_0
    {
        // Fields
        public Google.Protobuf.ValueReader<T> reader;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60001A8
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60001A9
        private System.Void <.ctor>b__0(Google.Protobuf.ParseContext& ctx, T& v) { }

    }

    // TypeToken: 0x200002D
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.FieldCodec.<>c<T> <>9;  // static @ 0x0
        public static Google.Protobuf.FieldCodec.ValuesMerger<T> <>9__39_1;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60001AA
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x60001AB
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60001AC
        private System.Boolean <.ctor>b__39_1(T& v, T v2) { }

    }

    // TypeToken: 0x200002F  // size: 0x18
    public sealed class Node
    {
        // Fields
        private readonly System.Collections.Generic.Dictionary<System.String,Google.Protobuf.FieldMaskTree.Node> <Children>k__BackingField;  // 0x10

        // Properties
        System.Collections.Generic.Dictionary<System.String,Google.Protobuf.FieldMaskTree.Node> Children { get; /* RVA: 0x02B2ECC0 */ }

        // Methods
        // RVA: 0x09AE0498  token: 0x60001B8
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003A  // size: 0x28
    public sealed class Settings
    {
        // Fields
        private static readonly Google.Protobuf.JsonFormatter.Settings <Default>k__BackingField;  // static @ 0x0
        private readonly System.Boolean <FormatDefaultValues>k__BackingField;  // 0x10
        private readonly Google.Protobuf.Reflection.TypeRegistry <TypeRegistry>k__BackingField;  // 0x18
        private readonly System.Boolean <FormatEnumsAsIntegers>k__BackingField;  // 0x20
        private readonly System.Boolean <PreserveProtoFieldNames>k__BackingField;  // 0x21

        // Properties
        Google.Protobuf.JsonFormatter.Settings Default { get; /* RVA: 0x09AE18E4 */ }
        System.Boolean FormatDefaultValues { get; /* RVA: 0x015EFCE0 */ }
        Google.Protobuf.Reflection.TypeRegistry TypeRegistry { get; /* RVA: 0x0385B100 */ }
        System.Boolean FormatEnumsAsIntegers { get; /* RVA: 0x04D866B0 */ }
        System.Boolean PreserveProtoFieldNames { get; /* RVA: 0x04D86850 */ }

        // Methods
        // RVA: 0x09AE1600  token: 0x60001F6
        private static System.Void .cctor() { }
        // RVA: 0x09AE16E8  token: 0x60001FB
        public System.Void .ctor(System.Boolean formatDefaultValues) { }
        // RVA: 0x09AE17B4  token: 0x60001FC
        public System.Void .ctor(System.Boolean formatDefaultValues, Google.Protobuf.Reflection.TypeRegistry typeRegistry) { }
        // RVA: 0x09AE17D4  token: 0x60001FD
        private System.Void .ctor(System.Boolean formatDefaultValues, Google.Protobuf.Reflection.TypeRegistry typeRegistry, System.Boolean formatEnumsAsIntegers, System.Boolean preserveProtoFieldNames) { }
        // RVA: 0x09AE11C8  token: 0x60001FE
        public Google.Protobuf.JsonFormatter.Settings WithFormatDefaultValues(System.Boolean formatDefaultValues) { }
        // RVA: 0x09AE14AC  token: 0x60001FF
        public Google.Protobuf.JsonFormatter.Settings WithTypeRegistry(Google.Protobuf.Reflection.TypeRegistry typeRegistry) { }
        // RVA: 0x09AE1264  token: 0x6000200
        public Google.Protobuf.JsonFormatter.Settings WithFormatEnumsAsIntegers(System.Boolean formatEnumsAsIntegers) { }
        // RVA: 0x09AE1388  token: 0x6000201
        public Google.Protobuf.JsonFormatter.Settings WithPreserveProtoFieldNames(System.Boolean preserveProtoFieldNames) { }

    }

    // TypeToken: 0x200003C  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.JsonFormatter.OriginalEnumValueHelper.<>c <>9;  // static @ 0x0
        public static System.Func<System.Reflection.FieldInfo,System.Boolean> <>9__2_0;  // static @ 0x8
        public static System.Func<System.Reflection.FieldInfo,System.Boolean> <>9__2_1;  // static @ 0x10
        public static System.Func<System.Reflection.FieldInfo,System.Object> <>9__2_2;  // static @ 0x18
        public static System.Func<System.Reflection.FieldInfo,System.String> <>9__2_3;  // static @ 0x20

        // Methods
        // RVA: 0x09AE2628  token: 0x6000205
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000206
        public System.Void .ctor() { }
        // RVA: 0x09AE1EA8  token: 0x6000207
        private System.Boolean <GetNameMapping>b__2_0(System.Reflection.FieldInfo f) { }
        // RVA: 0x09AE1EC8  token: 0x6000208
        private System.Boolean <GetNameMapping>b__2_1(System.Reflection.FieldInfo f) { }
        // RVA: 0x09AE1F2C  token: 0x6000209
        private System.Object <GetNameMapping>b__2_2(System.Reflection.FieldInfo f) { }
        // RVA: 0x09AE1F4C  token: 0x600020A
        private System.String <GetNameMapping>b__2_3(System.Reflection.FieldInfo f) { }

    }

    // TypeToken: 0x200003B  // size: 0x10
    public static class OriginalEnumValueHelper
    {
        // Fields
        private static readonly System.Collections.Generic.Dictionary<System.Type,System.Collections.Generic.Dictionary<System.Object,System.String>> dictionaries;  // static @ 0x0

        // Methods
        // RVA: 0x09AE086C  token: 0x6000202
        private static System.String GetOriginalName(System.Object value) { }
        // RVA: 0x09AE0510  token: 0x6000203
        private static System.Collections.Generic.Dictionary<System.Object,System.String> GetNameMapping(System.Type enumType) { }
        // RVA: 0x09AE0A70  token: 0x6000204
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200003E  // size: 0x28
    public sealed class Settings
    {
        // Fields
        private static readonly Google.Protobuf.JsonParser.Settings <Default>k__BackingField;  // static @ 0x0
        private readonly System.Int32 <RecursionLimit>k__BackingField;  // 0x10
        private readonly Google.Protobuf.Reflection.TypeRegistry <TypeRegistry>k__BackingField;  // 0x18
        private readonly System.Boolean <IgnoreUnknownFields>k__BackingField;  // 0x20

        // Properties
        Google.Protobuf.JsonParser.Settings Default { get; /* RVA: 0x09AE1934 */ }
        System.Int32 RecursionLimit { get; /* RVA: 0x02B2E2D0 */ }
        Google.Protobuf.Reflection.TypeRegistry TypeRegistry { get; /* RVA: 0x0385B100 */ }
        System.Boolean IgnoreUnknownFields { get; /* RVA: 0x04D866B0 */ }

        // Methods
        // RVA: 0x09AE1674  token: 0x600022C
        private static System.Void .cctor() { }
        // RVA: 0x09AE185C  token: 0x6000230
        private System.Void .ctor(System.Int32 recursionLimit, Google.Protobuf.Reflection.TypeRegistry typeRegistry, System.Boolean ignoreUnknownFields) { }
        // RVA: 0x09AE175C  token: 0x6000231
        public System.Void .ctor(System.Int32 recursionLimit) { }
        // RVA: 0x09AE1740  token: 0x6000232
        public System.Void .ctor(System.Int32 recursionLimit, Google.Protobuf.Reflection.TypeRegistry typeRegistry) { }
        // RVA: 0x09AE1300  token: 0x6000233
        public Google.Protobuf.JsonParser.Settings WithIgnoreUnknownFields(System.Boolean ignoreUnknownFields) { }
        // RVA: 0x09AE1424  token: 0x6000234
        public Google.Protobuf.JsonParser.Settings WithRecursionLimit(System.Int32 recursionLimit) { }
        // RVA: 0x09AE1548  token: 0x6000235
        public Google.Protobuf.JsonParser.Settings WithTypeRegistry(Google.Protobuf.Reflection.TypeRegistry typeRegistry) { }

    }

    // TypeToken: 0x200003F  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.JsonParser.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09AE2560  token: 0x6000236
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000237
        public System.Void .ctor() { }
        // RVA: 0x09AE1FC8  token: 0x6000238
        private System.Boolean <.cctor>b__41_0(Google.Protobuf.Reflection.EnumDescriptor ed) { }
        // RVA: 0x09AE203C  token: 0x6000239
        private System.Void <.cctor>b__41_1(Google.Protobuf.JsonParser parser, Google.Protobuf.IMessage message, Google.Protobuf.JsonTokenizer tokenizer) { }
        // RVA: 0x09AE20A4  token: 0x600023A
        private System.Void <.cctor>b__41_2(Google.Protobuf.JsonParser parser, Google.Protobuf.IMessage message, Google.Protobuf.JsonTokenizer tokenizer) { }
        // RVA: 0x09AE210C  token: 0x600023B
        private System.Void <.cctor>b__41_3(Google.Protobuf.JsonParser parser, Google.Protobuf.IMessage message, Google.Protobuf.JsonTokenizer tokenizer) { }
        // RVA: 0x09AE213C  token: 0x600023C
        private System.Void <.cctor>b__41_4(Google.Protobuf.JsonParser parser, Google.Protobuf.IMessage message, Google.Protobuf.JsonTokenizer tokenizer) { }
        // RVA: 0x09AE21D8  token: 0x600023D
        private System.Void <.cctor>b__41_5(Google.Protobuf.JsonParser parser, Google.Protobuf.IMessage message, Google.Protobuf.JsonTokenizer tokenizer) { }
        // RVA: 0x09AE2208  token: 0x600023E
        private System.Void <.cctor>b__41_6(Google.Protobuf.JsonParser parser, Google.Protobuf.IMessage message, Google.Protobuf.JsonTokenizer tokenizer) { }
        // RVA: 0x09AE2238  token: 0x600023F
        private System.Void <.cctor>b__41_7(Google.Protobuf.JsonParser parser, Google.Protobuf.IMessage message, Google.Protobuf.JsonTokenizer tokenizer) { }

    }

    // TypeToken: 0x2000041  // size: 0x14
    public sealed struct TokenType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Google.Protobuf.JsonToken.TokenType Null;  // const
        public static Google.Protobuf.JsonToken.TokenType False;  // const
        public static Google.Protobuf.JsonToken.TokenType True;  // const
        public static Google.Protobuf.JsonToken.TokenType StringValue;  // const
        public static Google.Protobuf.JsonToken.TokenType Number;  // const
        public static Google.Protobuf.JsonToken.TokenType Name;  // const
        public static Google.Protobuf.JsonToken.TokenType StartObject;  // const
        public static Google.Protobuf.JsonToken.TokenType EndObject;  // const
        public static Google.Protobuf.JsonToken.TokenType StartArray;  // const
        public static Google.Protobuf.JsonToken.TokenType EndArray;  // const
        public static Google.Protobuf.JsonToken.TokenType EndDocument;  // const

    }

    // TypeToken: 0x2000043  // size: 0x38
    public class JsonReplayTokenizer : Google.Protobuf.JsonTokenizer
    {
        // Fields
        private readonly System.Collections.Generic.IList<Google.Protobuf.JsonToken> tokens;  // 0x20
        private readonly Google.Protobuf.JsonTokenizer nextTokenizer;  // 0x28
        private System.Int32 nextTokenIndex;  // 0x30

        // Methods
        // RVA: 0x09ADC998  token: 0x600025D
        private System.Void .ctor(System.Collections.Generic.IList<Google.Protobuf.JsonToken> tokens, Google.Protobuf.JsonTokenizer nextTokenizer) { }
        // RVA: 0x09ADC908  token: 0x600025E
        protected virtual Google.Protobuf.JsonToken NextImpl() { }

    }

    // TypeToken: 0x2000045  // size: 0x14
    public sealed struct ContainerType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Google.Protobuf.JsonTokenizer.JsonTextTokenizer.ContainerType Document;  // const
        public static Google.Protobuf.JsonTokenizer.JsonTextTokenizer.ContainerType Object;  // const
        public static Google.Protobuf.JsonTokenizer.JsonTextTokenizer.ContainerType Array;  // const

    }

    // TypeToken: 0x2000046  // size: 0x14
    public sealed struct State
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Google.Protobuf.JsonTokenizer.JsonTextTokenizer.State StartOfDocument;  // const
        public static Google.Protobuf.JsonTokenizer.JsonTextTokenizer.State ExpectedEndOfDocument;  // const
        public static Google.Protobuf.JsonTokenizer.JsonTextTokenizer.State ReaderExhausted;  // const
        public static Google.Protobuf.JsonTokenizer.JsonTextTokenizer.State ObjectStart;  // const
        public static Google.Protobuf.JsonTokenizer.JsonTextTokenizer.State ObjectBeforeColon;  // const
        public static Google.Protobuf.JsonTokenizer.JsonTextTokenizer.State ObjectAfterColon;  // const
        public static Google.Protobuf.JsonTokenizer.JsonTextTokenizer.State ObjectAfterProperty;  // const
        public static Google.Protobuf.JsonTokenizer.JsonTextTokenizer.State ObjectAfterComma;  // const
        public static Google.Protobuf.JsonTokenizer.JsonTextTokenizer.State ArrayStart;  // const
        public static Google.Protobuf.JsonTokenizer.JsonTextTokenizer.State ArrayAfterValue;  // const
        public static Google.Protobuf.JsonTokenizer.JsonTextTokenizer.State ArrayAfterComma;  // const

    }

    // TypeToken: 0x2000047  // size: 0x20
    public class PushBackReader
    {
        // Fields
        private readonly System.IO.TextReader reader;  // 0x10
        private System.Nullable<System.Char> nextChar;  // 0x18

        // Methods
        // RVA: 0x053908C0  token: 0x600026E
        private System.Void .ctor(System.IO.TextReader reader) { }
        // RVA: 0x09AE114C  token: 0x600026F
        private System.Nullable<System.Char> Read() { }
        // RVA: 0x09AE10BC  token: 0x6000270
        private System.Char ReadOrFail(System.String messageOnFailure) { }
        // RVA: 0x09AE1008  token: 0x6000271
        private System.Void PushBack(System.Char c) { }
        // RVA: 0x09AE0FA8  token: 0x6000272
        private Google.Protobuf.InvalidJsonException CreateException(System.String message) { }

    }

    // TypeToken: 0x2000044  // size: 0x38
    public sealed class JsonTextTokenizer : Google.Protobuf.JsonTokenizer
    {
        // Fields
        private static readonly Google.Protobuf.JsonTokenizer.JsonTextTokenizer.State ValueStates;  // static @ 0x0
        private readonly System.Collections.Generic.Stack<Google.Protobuf.JsonTokenizer.JsonTextTokenizer.ContainerType> containerStack;  // 0x20
        private readonly Google.Protobuf.JsonTokenizer.JsonTextTokenizer.PushBackReader reader;  // 0x28
        private Google.Protobuf.JsonTokenizer.JsonTextTokenizer.State state;  // 0x30

        // Methods
        // RVA: 0x09ADE1B4  token: 0x600025F
        private System.Void .ctor(System.IO.TextReader reader) { }
        // RVA: 0x09ADCBEC  token: 0x6000260
        protected virtual Google.Protobuf.JsonToken NextImpl() { }
        // RVA: 0x09ADE0DC  token: 0x6000261
        private System.Void ValidateState(Google.Protobuf.JsonTokenizer.JsonTextTokenizer.State validStates, System.String errorPrefix) { }
        // RVA: 0x09ADDC7C  token: 0x6000262
        private System.String ReadString() { }
        // RVA: 0x09ADD3E4  token: 0x6000263
        private System.Char ReadEscapedCharacter() { }
        // RVA: 0x09ADDEB4  token: 0x6000264
        private System.Char ReadUnicodeEscape() { }
        // RVA: 0x09ADCA98  token: 0x6000265
        private System.Void ConsumeLiteral(System.String text) { }
        // RVA: 0x09ADD900  token: 0x6000266
        private System.Double ReadNumber(System.Char initialCharacter) { }
        // RVA: 0x09ADD7D0  token: 0x6000267
        private System.Nullable<System.Char> ReadInt(System.Text.StringBuilder builder) { }
        // RVA: 0x09ADD73C  token: 0x6000268
        private System.Nullable<System.Char> ReadFrac(System.Text.StringBuilder builder) { }
        // RVA: 0x09ADD550  token: 0x6000269
        private System.Nullable<System.Char> ReadExp(System.Text.StringBuilder builder) { }
        // RVA: 0x09ADC9C0  token: 0x600026A
        private System.Nullable<System.Char> ConsumeDigits(System.Text.StringBuilder builder, System.Int32& count) { }
        // RVA: 0x09ADDFEC  token: 0x600026B
        private System.Void ValidateAndModifyStateForValue(System.String errorPrefix) { }
        // RVA: 0x09ADD2AC  token: 0x600026C
        private System.Void PopContainer() { }
        // RVA: 0x09ADE178  token: 0x600026D
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200004A  // size: 0x18
    public sealed class <>c__DisplayClass13_0
    {
        // Fields
        public Google.Protobuf.IMessage message;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000294
        public System.Void .ctor() { }
        // RVA: 0x09AE2318  token: 0x6000295
        private System.Boolean <IsInitialized>b__0(Google.Protobuf.Reflection.FieldDescriptor f) { }

    }

    // TypeToken: 0x200004D
    public sealed class <>c__DisplayClass2_0
    {
        // Fields
        public System.Func<T> factory;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60002B4
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60002B5
        private Google.Protobuf.IMessage <.ctor>b__0() { }

    }

    // TypeToken: 0x200005A  // size: 0x14
    public sealed struct WireType
    {
        // Fields
        public System.UInt32 value__;  // 0x10
        public static Google.Protobuf.WireFormat.WireType Varint;  // const
        public static Google.Protobuf.WireFormat.WireType Fixed64;  // const
        public static Google.Protobuf.WireFormat.WireType LengthDelimited;  // const
        public static Google.Protobuf.WireFormat.WireType StartGroup;  // const
        public static Google.Protobuf.WireFormat.WireType EndGroup;  // const
        public static Google.Protobuf.WireFormat.WireType Fixed32;  // const

    }

    // TypeToken: 0x2000062  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.WellKnownTypes.Any.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09AF05C4  token: 0x60003C6
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60003C7
        public System.Void .ctor() { }
        // RVA: 0x09AF00B0  token: 0x60003C8
        private Google.Protobuf.WellKnownTypes.Any <.cctor>b__40_0() { }

    }

    // TypeToken: 0x2000065  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.WellKnownTypes.Api.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09AF036C  token: 0x60003E7
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60003E8
        public System.Void .ctor() { }
        // RVA: 0x09AF01C8  token: 0x60003E9
        private Google.Protobuf.WellKnownTypes.Api <.cctor>b__57_0() { }

    }

    // TypeToken: 0x2000067  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.WellKnownTypes.Method.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09AF0628  token: 0x6000408
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000409
        public System.Void .ctor() { }
        // RVA: 0x09AF0218  token: 0x600040A
        private Google.Protobuf.WellKnownTypes.Method <.cctor>b__57_0() { }

    }

    // TypeToken: 0x2000069  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.WellKnownTypes.Mixin.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09AF0308  token: 0x6000420
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000421
        public System.Void .ctor() { }
        // RVA: 0x09AF0060  token: 0x6000422
        private Google.Protobuf.WellKnownTypes.Mixin <.cctor>b__32_0() { }

    }

    // TypeToken: 0x200006C  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.WellKnownTypes.Duration.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09AF0560  token: 0x6000444
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000445
        public System.Void .ctor() { }
        // RVA: 0x09AF0150  token: 0x6000446
        private Google.Protobuf.WellKnownTypes.Duration <.cctor>b__48_0() { }

    }

    // TypeToken: 0x200006F  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.WellKnownTypes.Empty.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09AF0434  token: 0x600045A
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600045B
        public System.Void .ctor() { }
        // RVA: 0x09AEFF34  token: 0x600045C
        private Google.Protobuf.WellKnownTypes.Empty <.cctor>b__22_0() { }

    }

    // TypeToken: 0x2000072  // size: 0x18
    public sealed class MergeOptions
    {
        // Fields
        private System.Boolean <ReplaceMessageFields>k__BackingField;  // 0x10
        private System.Boolean <ReplaceRepeatedFields>k__BackingField;  // 0x11
        private System.Boolean <ReplacePrimitiveFields>k__BackingField;  // 0x12

        // Properties
        System.Boolean ReplaceMessageFields { get; /* RVA: 0x015EFCE0 */ set; /* RVA: 0x04D86130 */ }
        System.Boolean ReplaceRepeatedFields { get; /* RVA: 0x04D88380 */ set; /* RVA: 0x04D88460 */ }
        System.Boolean ReplacePrimitiveFields { get; /* RVA: 0x04D88390 */ set; /* RVA: 0x04DA0900 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000488
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000073  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.WellKnownTypes.FieldMask.<>c <>9;  // static @ 0x0
        public static System.Func<System.String,System.Boolean> <>9__29_0;  // static @ 0x8

        // Methods
        // RVA: 0x09AF068C  token: 0x6000489
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600048A
        public System.Void .ctor() { }
        // RVA: 0x09AEFEF0  token: 0x600048B
        private System.Boolean <ToJson>b__29_0(System.String p) { }
        // RVA: 0x09AF0100  token: 0x600048C
        private Google.Protobuf.WellKnownTypes.FieldMask <.cctor>b__47_0() { }

    }

    // TypeToken: 0x2000076  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.WellKnownTypes.SourceContext.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09AF03D0  token: 0x60004A2
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60004A3
        public System.Void .ctor() { }
        // RVA: 0x09AF0010  token: 0x60004A4
        private Google.Protobuf.WellKnownTypes.SourceContext <.cctor>b__27_0() { }

    }

    // TypeToken: 0x200007A  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.WellKnownTypes.Struct.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09AF04FC  token: 0x60004B9
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60004BA
        public System.Void .ctor() { }
        // RVA: 0x09AEFF70  token: 0x60004BB
        private Google.Protobuf.WellKnownTypes.Struct <.cctor>b__27_0() { }

    }

    // TypeToken: 0x200007C  // size: 0x14
    public sealed struct KindOneofCase
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Google.Protobuf.WellKnownTypes.Value.KindOneofCase None;  // const
        public static Google.Protobuf.WellKnownTypes.Value.KindOneofCase NullValue;  // const
        public static Google.Protobuf.WellKnownTypes.Value.KindOneofCase NumberValue;  // const
        public static Google.Protobuf.WellKnownTypes.Value.KindOneofCase StringValue;  // const
        public static Google.Protobuf.WellKnownTypes.Value.KindOneofCase BoolValue;  // const
        public static Google.Protobuf.WellKnownTypes.Value.KindOneofCase StructValue;  // const
        public static Google.Protobuf.WellKnownTypes.Value.KindOneofCase ListValue;  // const

    }

    // TypeToken: 0x200007D  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.WellKnownTypes.Value.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09AF02A4  token: 0x60004E1
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60004E2
        public System.Void .ctor() { }
        // RVA: 0x09AF0268  token: 0x60004E3
        private Google.Protobuf.WellKnownTypes.Value <.cctor>b__58_0() { }

    }

    // TypeToken: 0x200007F  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.WellKnownTypes.ListValue.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09AF06F0  token: 0x60004F6
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60004F7
        public System.Void .ctor() { }
        // RVA: 0x09AEFFC0  token: 0x60004F8
        private Google.Protobuf.WellKnownTypes.ListValue <.cctor>b__27_0() { }

    }

    // TypeToken: 0x2000083  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.WellKnownTypes.Timestamp.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09AF0498  token: 0x6000525
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000526
        public System.Void .ctor() { }
        // RVA: 0x09AF018C  token: 0x6000527
        private Google.Protobuf.WellKnownTypes.Timestamp <.cctor>b__55_0() { }

    }

    // TypeToken: 0x2000087  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.WellKnownTypes.Type.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B07B18  token: 0x6000544
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000545
        public System.Void .ctor() { }
        // RVA: 0x09B07910  token: 0x6000546
        private Google.Protobuf.WellKnownTypes.Type <.cctor>b__52_0() { }

    }

    // TypeToken: 0x200008A  // size: 0x14
    public sealed struct Kind
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Google.Protobuf.WellKnownTypes.Field.Types.Kind TypeUnknown;  // const
        public static Google.Protobuf.WellKnownTypes.Field.Types.Kind TypeDouble;  // const
        public static Google.Protobuf.WellKnownTypes.Field.Types.Kind TypeFloat;  // const
        public static Google.Protobuf.WellKnownTypes.Field.Types.Kind TypeInt64;  // const
        public static Google.Protobuf.WellKnownTypes.Field.Types.Kind TypeUint64;  // const
        public static Google.Protobuf.WellKnownTypes.Field.Types.Kind TypeInt32;  // const
        public static Google.Protobuf.WellKnownTypes.Field.Types.Kind TypeFixed64;  // const
        public static Google.Protobuf.WellKnownTypes.Field.Types.Kind TypeFixed32;  // const
        public static Google.Protobuf.WellKnownTypes.Field.Types.Kind TypeBool;  // const
        public static Google.Protobuf.WellKnownTypes.Field.Types.Kind TypeString;  // const
        public static Google.Protobuf.WellKnownTypes.Field.Types.Kind TypeGroup;  // const
        public static Google.Protobuf.WellKnownTypes.Field.Types.Kind TypeMessage;  // const
        public static Google.Protobuf.WellKnownTypes.Field.Types.Kind TypeBytes;  // const
        public static Google.Protobuf.WellKnownTypes.Field.Types.Kind TypeUint32;  // const
        public static Google.Protobuf.WellKnownTypes.Field.Types.Kind TypeEnum;  // const
        public static Google.Protobuf.WellKnownTypes.Field.Types.Kind TypeSfixed32;  // const
        public static Google.Protobuf.WellKnownTypes.Field.Types.Kind TypeSfixed64;  // const
        public static Google.Protobuf.WellKnownTypes.Field.Types.Kind TypeSint32;  // const
        public static Google.Protobuf.WellKnownTypes.Field.Types.Kind TypeSint64;  // const

    }

    // TypeToken: 0x200008B  // size: 0x14
    public sealed struct Cardinality
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Google.Protobuf.WellKnownTypes.Field.Types.Cardinality Unknown;  // const
        public static Google.Protobuf.WellKnownTypes.Field.Types.Cardinality Optional;  // const
        public static Google.Protobuf.WellKnownTypes.Field.Types.Cardinality Required;  // const
        public static Google.Protobuf.WellKnownTypes.Field.Types.Cardinality Repeated;  // const

    }

    // TypeToken: 0x2000089  // size: 0x10
    public static class Types
    {
    }

    // TypeToken: 0x200008C  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.WellKnownTypes.Field.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B08090  token: 0x600056B
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600056C
        public System.Void .ctor() { }
        // RVA: 0x09B07960  token: 0x600056D
        private Google.Protobuf.WellKnownTypes.Field <.cctor>b__73_0() { }

    }

    // TypeToken: 0x200008E  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.WellKnownTypes.Enum.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B080F4  token: 0x6000587
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000588
        public System.Void .ctor() { }
        // RVA: 0x09B078C0  token: 0x6000589
        private Google.Protobuf.WellKnownTypes.Enum <.cctor>b__47_0() { }

    }

    // TypeToken: 0x2000090  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.WellKnownTypes.EnumValue.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B07FC8  token: 0x60005A0
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60005A1
        public System.Void .ctor() { }
        // RVA: 0x09B077A8  token: 0x60005A2
        private Google.Protobuf.WellKnownTypes.EnumValue <.cctor>b__37_0() { }

    }

    // TypeToken: 0x2000092  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.WellKnownTypes.Option.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B07D0C  token: 0x60005B8
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60005B9
        public System.Void .ctor() { }
        // RVA: 0x09B0771C  token: 0x60005BA
        private Google.Protobuf.WellKnownTypes.Option <.cctor>b__32_0() { }

    }

    // TypeToken: 0x2000095  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.WellKnownTypes.DoubleValue.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B0802C  token: 0x60005D0
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60005D1
        public System.Void .ctor() { }
        // RVA: 0x09B076E0  token: 0x60005D2
        private Google.Protobuf.WellKnownTypes.DoubleValue <.cctor>b__27_0() { }

    }

    // TypeToken: 0x2000097  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.WellKnownTypes.FloatValue.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B07F64  token: 0x60005E6
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60005E7
        public System.Void .ctor() { }
        // RVA: 0x09B07668  token: 0x60005E8
        private Google.Protobuf.WellKnownTypes.FloatValue <.cctor>b__27_0() { }

    }

    // TypeToken: 0x2000099  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.WellKnownTypes.Int64Value.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B07F00  token: 0x60005FC
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60005FD
        public System.Void .ctor() { }
        // RVA: 0x09B076A4  token: 0x60005FE
        private Google.Protobuf.WellKnownTypes.Int64Value <.cctor>b__27_0() { }

    }

    // TypeToken: 0x200009B  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.WellKnownTypes.UInt64Value.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B07E9C  token: 0x6000612
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000613
        public System.Void .ctor() { }
        // RVA: 0x09B07514  token: 0x6000614
        private Google.Protobuf.WellKnownTypes.UInt64Value <.cctor>b__27_0() { }

    }

    // TypeToken: 0x200009D  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.WellKnownTypes.Int32Value.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B07CA8  token: 0x6000628
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000629
        public System.Void .ctor() { }
        // RVA: 0x09B0762C  token: 0x600062A
        private Google.Protobuf.WellKnownTypes.Int32Value <.cctor>b__27_0() { }

    }

    // TypeToken: 0x200009F  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.WellKnownTypes.UInt32Value.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B07C44  token: 0x600063E
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600063F
        public System.Void .ctor() { }
        // RVA: 0x09B074D8  token: 0x6000640
        private Google.Protobuf.WellKnownTypes.UInt32Value <.cctor>b__27_0() { }

    }

    // TypeToken: 0x20000A1  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.WellKnownTypes.BoolValue.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B08284  token: 0x6000654
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000655
        public System.Void .ctor() { }
        // RVA: 0x09B07550  token: 0x6000656
        private Google.Protobuf.WellKnownTypes.BoolValue <.cctor>b__27_0() { }

    }

    // TypeToken: 0x20000A3  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.WellKnownTypes.StringValue.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B08158  token: 0x600066A
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600066B
        public System.Void .ctor() { }
        // RVA: 0x09B075DC  token: 0x600066C
        private Google.Protobuf.WellKnownTypes.StringValue <.cctor>b__27_0() { }

    }

    // TypeToken: 0x20000A5  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.WellKnownTypes.BytesValue.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B08220  token: 0x6000680
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000681
        public System.Void .ctor() { }
        // RVA: 0x09B07488  token: 0x6000682
        private Google.Protobuf.WellKnownTypes.BytesValue <.cctor>b__27_0() { }

    }

    // TypeToken: 0x20000A7
    public sealed class <>c__19`1
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.CustomOptions.<>c__19<T> <>9;  // static @ 0x0
        public static System.Func<T,Google.Protobuf.ByteString> <>9__19_0;  // static @ 0x0
        public static System.Func<T,Google.Protobuf.ByteString,T> <>9__19_1;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000696
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x6000697
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000698
        private Google.Protobuf.ByteString <TryGetMessage>b__19_0(T v) { }
        // RVA: -1  // not resolved  token: 0x6000699
        private T <TryGetMessage>b__19_1(T t, Google.Protobuf.ByteString b) { }

    }

    // TypeToken: 0x20000AA  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.FileDescriptorSet.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B081BC  token: 0x60006AE
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60006AF
        public System.Void .ctor() { }
        // RVA: 0x09B0758C  token: 0x60006B0
        private Google.Protobuf.Reflection.FileDescriptorSet <.cctor>b__27_0() { }

    }

    // TypeToken: 0x20000AC  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.FileDescriptorProto.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B07BE0  token: 0x60006D9
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60006DA
        public System.Void .ctor() { }
        // RVA: 0x09B07A00  token: 0x60006DB
        private Google.Protobuf.Reflection.FileDescriptorProto <.cctor>b__94_0() { }

    }

    // TypeToken: 0x20000B0  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.DescriptorProto.Types.ExtensionRange.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B07D70  token: 0x6000716
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000717
        public System.Void .ctor() { }
        // RVA: 0x09B07884  token: 0x6000718
        private Google.Protobuf.Reflection.DescriptorProto.Types.ExtensionRange <.cctor>b__46_0() { }

    }

    // TypeToken: 0x20000AF  // size: 0x30
    public sealed class ExtensionRange : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.DescriptorProto.Types.ExtensionRange> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        private System.Int32 _hasBits0;  // 0x18
        public static System.Int32 StartFieldNumber;  // const
        private static readonly System.Int32 StartDefaultValue;  // static @ 0x8
        private System.Int32 start_;  // 0x1c
        public static System.Int32 EndFieldNumber;  // const
        private static readonly System.Int32 EndDefaultValue;  // static @ 0xc
        private System.Int32 end_;  // 0x20
        public static System.Int32 OptionsFieldNumber;  // const
        private Google.Protobuf.Reflection.ExtensionRangeOptions options_;  // 0x28

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.Reflection.DescriptorProto.Types.ExtensionRange> Parser { get; /* RVA: 0x09AFF0B0 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09AFEFF4 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09AFF318 */ }
        System.Int32 Start { get; /* RVA: 0x09AFF100 */ set; /* RVA: 0x04DA0E10 */ }
        System.Boolean HasStart { get; /* RVA: 0x0314ECE0 */ }
        System.Int32 End { get; /* RVA: 0x09AFF054 */ set; /* RVA: 0x04DA0E00 */ }
        System.Boolean HasEnd { get; /* RVA: 0x09AFF0A8 */ }
        Google.Protobuf.Reflection.ExtensionRangeOptions Options { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60006FE
        public System.Void .ctor() { }
        // RVA: 0x09AFEF80  token: 0x60006FF
        public System.Void .ctor(Google.Protobuf.Reflection.DescriptorProto.Types.ExtensionRange other) { }
        // RVA: 0x09AFEB5C  token: 0x6000700
        public virtual Google.Protobuf.Reflection.DescriptorProto.Types.ExtensionRange Clone() { }
        // RVA: 0x04D86DE0  token: 0x6000704
        public System.Void ClearStart() { }
        // RVA: 0x04DA0DF0  token: 0x6000708
        public System.Void ClearEnd() { }
        // RVA: 0x09AFEC48  token: 0x600070B
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09AFEBBC  token: 0x600070C
        public virtual System.Boolean Equals(Google.Protobuf.Reflection.DescriptorProto.Types.ExtensionRange other) { }
        // RVA: 0x09AFECAC  token: 0x600070D
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09AFEE08  token: 0x600070E
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x600070F
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09AFF270  token: 0x6000710
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09AFEA8C  token: 0x6000711
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09AFED1C  token: 0x6000712
        public virtual System.Void MergeFrom(Google.Protobuf.Reflection.DescriptorProto.Types.ExtensionRange other) { }
        // RVA: 0x04D51840  token: 0x6000713
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09AFF154  token: 0x6000714
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09AFEE54  token: 0x6000715
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000B2  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.DescriptorProto.Types.ReservedRange.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B07A50  token: 0x6000732
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000733
        public System.Void .ctor() { }
        // RVA: 0x09B07848  token: 0x6000734
        private Google.Protobuf.Reflection.DescriptorProto.Types.ReservedRange <.cctor>b__41_0() { }

    }

    // TypeToken: 0x20000B1  // size: 0x28
    public sealed class ReservedRange : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.DescriptorProto.Types.ReservedRange> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        private System.Int32 _hasBits0;  // 0x18
        public static System.Int32 StartFieldNumber;  // const
        private static readonly System.Int32 StartDefaultValue;  // static @ 0x8
        private System.Int32 start_;  // 0x1c
        public static System.Int32 EndFieldNumber;  // const
        private static readonly System.Int32 EndDefaultValue;  // static @ 0xc
        private System.Int32 end_;  // 0x20

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.Reflection.DescriptorProto.Types.ReservedRange> Parser { get; /* RVA: 0x09B05D84 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B05CCC */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B05F4C */ }
        System.Int32 Start { get; /* RVA: 0x09B05DD4 */ set; /* RVA: 0x04DA0E10 */ }
        System.Boolean HasStart { get; /* RVA: 0x0314ECE0 */ }
        System.Int32 End { get; /* RVA: 0x09B05D30 */ set; /* RVA: 0x04DA0E00 */ }
        System.Boolean HasEnd { get; /* RVA: 0x09AFF0A8 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x600071C
        public System.Void .ctor() { }
        // RVA: 0x0637A38C  token: 0x600071D
        public System.Void .ctor(Google.Protobuf.Reflection.DescriptorProto.Types.ReservedRange other) { }
        // RVA: 0x09B05954  token: 0x600071E
        public virtual Google.Protobuf.Reflection.DescriptorProto.Types.ReservedRange Clone() { }
        // RVA: 0x04D86DE0  token: 0x6000722
        public System.Void ClearStart() { }
        // RVA: 0x04DA0DF0  token: 0x6000726
        public System.Void ClearEnd() { }
        // RVA: 0x09B059B4  token: 0x6000727
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09B05A18  token: 0x6000728
        public virtual System.Boolean Equals(Google.Protobuf.Reflection.DescriptorProto.Types.ReservedRange other) { }
        // RVA: 0x09B05A90  token: 0x6000729
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B05B54  token: 0x600072A
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x600072B
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09B05EC4  token: 0x600072C
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09B058A8  token: 0x600072D
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09B05AE8  token: 0x600072E
        public virtual System.Void MergeFrom(Google.Protobuf.Reflection.DescriptorProto.Types.ReservedRange other) { }
        // RVA: 0x04D51840  token: 0x600072F
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09B05E28  token: 0x6000730
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09B05BA0  token: 0x6000731
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000AE  // size: 0x10
    public static class Types
    {
    }

    // TypeToken: 0x20000B3  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.DescriptorProto.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B07AB4  token: 0x6000735
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000736
        public System.Void .ctor() { }
        // RVA: 0x09B079B0  token: 0x6000737
        private Google.Protobuf.Reflection.DescriptorProto <.cctor>b__77_0() { }

    }

    // TypeToken: 0x20000B5  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.ExtensionRangeOptions.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B07DD4  token: 0x6000752
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000753
        public System.Void .ctor() { }
        // RVA: 0x09B077F8  token: 0x6000754
        private Google.Protobuf.Reflection.ExtensionRangeOptions <.cctor>b__37_0() { }

    }

    // TypeToken: 0x20000B8  // size: 0x14
    public sealed struct Type
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Google.Protobuf.Reflection.FieldDescriptorProto.Types.Type Double;  // const
        public static Google.Protobuf.Reflection.FieldDescriptorProto.Types.Type Float;  // const
        public static Google.Protobuf.Reflection.FieldDescriptorProto.Types.Type Int64;  // const
        public static Google.Protobuf.Reflection.FieldDescriptorProto.Types.Type Uint64;  // const
        public static Google.Protobuf.Reflection.FieldDescriptorProto.Types.Type Int32;  // const
        public static Google.Protobuf.Reflection.FieldDescriptorProto.Types.Type Fixed64;  // const
        public static Google.Protobuf.Reflection.FieldDescriptorProto.Types.Type Fixed32;  // const
        public static Google.Protobuf.Reflection.FieldDescriptorProto.Types.Type Bool;  // const
        public static Google.Protobuf.Reflection.FieldDescriptorProto.Types.Type String;  // const
        public static Google.Protobuf.Reflection.FieldDescriptorProto.Types.Type Group;  // const
        public static Google.Protobuf.Reflection.FieldDescriptorProto.Types.Type Message;  // const
        public static Google.Protobuf.Reflection.FieldDescriptorProto.Types.Type Bytes;  // const
        public static Google.Protobuf.Reflection.FieldDescriptorProto.Types.Type Uint32;  // const
        public static Google.Protobuf.Reflection.FieldDescriptorProto.Types.Type Enum;  // const
        public static Google.Protobuf.Reflection.FieldDescriptorProto.Types.Type Sfixed32;  // const
        public static Google.Protobuf.Reflection.FieldDescriptorProto.Types.Type Sfixed64;  // const
        public static Google.Protobuf.Reflection.FieldDescriptorProto.Types.Type Sint32;  // const
        public static Google.Protobuf.Reflection.FieldDescriptorProto.Types.Type Sint64;  // const

    }

    // TypeToken: 0x20000B9  // size: 0x14
    public sealed struct Label
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Google.Protobuf.Reflection.FieldDescriptorProto.Types.Label Optional;  // const
        public static Google.Protobuf.Reflection.FieldDescriptorProto.Types.Label Required;  // const
        public static Google.Protobuf.Reflection.FieldDescriptorProto.Types.Label Repeated;  // const

    }

    // TypeToken: 0x20000B7  // size: 0x10
    public static class Types
    {
    }

    // TypeToken: 0x20000BA  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.FieldDescriptorProto.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B07E38  token: 0x6000790
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000791
        public System.Void .ctor() { }
        // RVA: 0x09B0744C  token: 0x6000792
        private Google.Protobuf.Reflection.FieldDescriptorProto <.cctor>b__119_0() { }

    }

    // TypeToken: 0x20000BC  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.OneofDescriptorProto.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B07B7C  token: 0x60007AA
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60007AB
        public System.Void .ctor() { }
        // RVA: 0x09B0776C  token: 0x60007AC
        private Google.Protobuf.Reflection.OneofDescriptorProto <.cctor>b__36_0() { }

    }

    // TypeToken: 0x20000C0  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.EnumDescriptorProto.Types.EnumReservedRange.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B1A180  token: 0x60007E0
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60007E1
        public System.Void .ctor() { }
        // RVA: 0x09B1984C  token: 0x60007E2
        private Google.Protobuf.Reflection.EnumDescriptorProto.Types.EnumReservedRange <.cctor>b__41_0() { }

    }

    // TypeToken: 0x20000BF  // size: 0x28
    public sealed class EnumReservedRange : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.EnumDescriptorProto.Types.EnumReservedRange> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        private System.Int32 _hasBits0;  // 0x18
        public static System.Int32 StartFieldNumber;  // const
        private static readonly System.Int32 StartDefaultValue;  // static @ 0x8
        private System.Int32 start_;  // 0x1c
        public static System.Int32 EndFieldNumber;  // const
        private static readonly System.Int32 EndDefaultValue;  // static @ 0xc
        private System.Int32 end_;  // 0x20

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.Reflection.EnumDescriptorProto.Types.EnumReservedRange> Parser { get; /* RVA: 0x09B0CE94 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B0CDE0 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B0CFC0 */ }
        System.Int32 Start { get; /* RVA: 0x09B0CEE4 */ set; /* RVA: 0x04DA0E10 */ }
        System.Boolean HasStart { get; /* RVA: 0x0314ECE0 */ }
        System.Int32 End { get; /* RVA: 0x09B0CE40 */ set; /* RVA: 0x04DA0E00 */ }
        System.Boolean HasEnd { get; /* RVA: 0x09AFF0A8 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60007CA
        public System.Void .ctor() { }
        // RVA: 0x0637A38C  token: 0x60007CB
        public System.Void .ctor(Google.Protobuf.Reflection.EnumDescriptorProto.Types.EnumReservedRange other) { }
        // RVA: 0x09B0CA68  token: 0x60007CC
        public virtual Google.Protobuf.Reflection.EnumDescriptorProto.Types.EnumReservedRange Clone() { }
        // RVA: 0x04D86DE0  token: 0x60007D0
        public System.Void ClearStart() { }
        // RVA: 0x04DA0DF0  token: 0x60007D4
        public System.Void ClearEnd() { }
        // RVA: 0x09B0CAC8  token: 0x60007D5
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09B0CB2C  token: 0x60007D6
        public virtual System.Boolean Equals(Google.Protobuf.Reflection.EnumDescriptorProto.Types.EnumReservedRange other) { }
        // RVA: 0x09B0CBA4  token: 0x60007D7
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B0CC68  token: 0x60007D8
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x60007D9
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09B0CF38  token: 0x60007DA
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09B0C9BC  token: 0x60007DB
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09B0CBFC  token: 0x60007DC
        public virtual System.Void MergeFrom(Google.Protobuf.Reflection.EnumDescriptorProto.Types.EnumReservedRange other) { }
        // RVA: 0x04D51840  token: 0x60007DD
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09B05E28  token: 0x60007DE
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09B0CCB4  token: 0x60007DF
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000BE  // size: 0x10
    public static class Types
    {
    }

    // TypeToken: 0x20000C1  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.EnumDescriptorProto.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B19EC4  token: 0x60007E3
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60007E4
        public System.Void .ctor() { }
        // RVA: 0x09B19964  token: 0x60007E5
        private Google.Protobuf.Reflection.EnumDescriptorProto <.cctor>b__52_0() { }

    }

    // TypeToken: 0x20000C3  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.EnumValueDescriptorProto.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B1A0B8  token: 0x6000801
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000802
        public System.Void .ctor() { }
        // RVA: 0x09B19888  token: 0x6000803
        private Google.Protobuf.Reflection.EnumValueDescriptorProto <.cctor>b__46_0() { }

    }

    // TypeToken: 0x20000C5  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.ServiceDescriptorProto.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B1A2AC  token: 0x600081C
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600081D
        public System.Void .ctor() { }
        // RVA: 0x09B197C0  token: 0x600081E
        private Google.Protobuf.Reflection.ServiceDescriptorProto <.cctor>b__41_0() { }

    }

    // TypeToken: 0x20000C7  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.MethodDescriptorProto.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B19C6C  token: 0x6000846
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000847
        public System.Void .ctor() { }
        // RVA: 0x09B19AF4  token: 0x6000848
        private Google.Protobuf.Reflection.MethodDescriptorProto <.cctor>b__73_0() { }

    }

    // TypeToken: 0x20000CA  // size: 0x14
    public sealed struct OptimizeMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Google.Protobuf.Reflection.FileOptions.Types.OptimizeMode Speed;  // const
        public static Google.Protobuf.Reflection.FileOptions.Types.OptimizeMode CodeSize;  // const
        public static Google.Protobuf.Reflection.FileOptions.Types.OptimizeMode LiteRuntime;  // const

    }

    // TypeToken: 0x20000C9  // size: 0x10
    public static class Types
    {
    }

    // TypeToken: 0x20000CB  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.FileOptions.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B1A1E4  token: 0x60008B3
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60008B4
        public System.Void .ctor() { }
        // RVA: 0x09B19680  token: 0x60008B5
        private Google.Protobuf.Reflection.FileOptions <.cctor>b__219_0() { }

    }

    // TypeToken: 0x20000CD  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.MessageOptions.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B19FF0  token: 0x60008E0
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60008E1
        public System.Void .ctor() { }
        // RVA: 0x09B19B30  token: 0x60008E2
        private Google.Protobuf.Reflection.MessageOptions <.cctor>b__74_0() { }

    }

    // TypeToken: 0x20000D0  // size: 0x14
    public sealed struct CType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Google.Protobuf.Reflection.FieldOptions.Types.CType String;  // const
        public static Google.Protobuf.Reflection.FieldOptions.Types.CType Cord;  // const
        public static Google.Protobuf.Reflection.FieldOptions.Types.CType StringPiece;  // const

    }

    // TypeToken: 0x20000D1  // size: 0x14
    public sealed struct JSType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Google.Protobuf.Reflection.FieldOptions.Types.JSType JsNormal;  // const
        public static Google.Protobuf.Reflection.FieldOptions.Types.JSType JsString;  // const
        public static Google.Protobuf.Reflection.FieldOptions.Types.JSType JsNumber;  // const

    }

    // TypeToken: 0x20000CF  // size: 0x10
    public static class Types
    {
    }

    // TypeToken: 0x20000D2  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.FieldOptions.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B19E60  token: 0x6000919
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600091A
        public System.Void .ctor() { }
        // RVA: 0x09B19630  token: 0x600091B
        private Google.Protobuf.Reflection.FieldOptions <.cctor>b__102_0() { }

    }

    // TypeToken: 0x20000D4  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.OneofOptions.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B1A248  token: 0x6000936
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000937
        public System.Void .ctor() { }
        // RVA: 0x09B19770  token: 0x6000938
        private Google.Protobuf.Reflection.OneofOptions <.cctor>b__37_0() { }

    }

    // TypeToken: 0x20000D6  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.EnumOptions.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B19D34  token: 0x600095B
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600095C
        public System.Void .ctor() { }
        // RVA: 0x09B19A54  token: 0x600095D
        private Google.Protobuf.Reflection.EnumOptions <.cctor>b__56_0() { }

    }

    // TypeToken: 0x20000D8  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.EnumValueOptions.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B19D98  token: 0x600097C
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600097D
        public System.Void .ctor() { }
        // RVA: 0x09B19914  token: 0x600097E
        private Google.Protobuf.Reflection.EnumValueOptions <.cctor>b__47_0() { }

    }

    // TypeToken: 0x20000DA  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.ServiceOptions.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B19DFC  token: 0x600099D
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600099E
        public System.Void .ctor() { }
        // RVA: 0x09B198C4  token: 0x600099F
        private Google.Protobuf.Reflection.ServiceOptions <.cctor>b__47_0() { }

    }

    // TypeToken: 0x20000DD  // size: 0x14
    public sealed struct IdempotencyLevel
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Google.Protobuf.Reflection.MethodOptions.Types.IdempotencyLevel IdempotencyUnknown;  // const
        public static Google.Protobuf.Reflection.MethodOptions.Types.IdempotencyLevel NoSideEffects;  // const
        public static Google.Protobuf.Reflection.MethodOptions.Types.IdempotencyLevel Idempotent;  // const

    }

    // TypeToken: 0x20000DC  // size: 0x10
    public static class Types
    {
    }

    // TypeToken: 0x20000DE  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.MethodOptions.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B19F8C  token: 0x60009C2
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60009C3
        public System.Void .ctor() { }
        // RVA: 0x09B19AA4  token: 0x60009C4
        private Google.Protobuf.Reflection.MethodOptions <.cctor>b__57_0() { }

    }

    // TypeToken: 0x20000E2  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.UninterpretedOption.Types.NamePart.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B1A310  token: 0x6000A08
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000A09
        public System.Void .ctor() { }
        // RVA: 0x09B19810  token: 0x6000A0A
        private Google.Protobuf.Reflection.UninterpretedOption.Types.NamePart <.cctor>b__41_0() { }

    }

    // TypeToken: 0x20000E1  // size: 0x30
    public sealed class NamePart : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.UninterpretedOption.Types.NamePart> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        private System.Int32 _hasBits0;  // 0x18
        public static System.Int32 NamePart_FieldNumber;  // const
        private static readonly System.String NamePart_DefaultValue;  // static @ 0x8
        private System.String namePart_;  // 0x20
        public static System.Int32 IsExtensionFieldNumber;  // const
        private static readonly System.Boolean IsExtensionDefaultValue;  // static @ 0x10
        private System.Boolean isExtension_;  // 0x28

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.Reflection.UninterpretedOption.Types.NamePart> Parser { get; /* RVA: 0x09B16F28 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B16E20 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B17094 */ }
        System.String NamePart_ { get; /* RVA: 0x09B16ED4 */ set; /* RVA: 0x09B170D4 */ }
        System.Boolean HasNamePart_ { get; /* RVA: 0x09B00070 */ }
        System.Boolean IsExtension { get; /* RVA: 0x09B16E80 */ set; /* RVA: 0x04DA10E0 */ }
        System.Boolean HasIsExtension { get; /* RVA: 0x0314ECE0 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60009F2
        public System.Void .ctor() { }
        // RVA: 0x09B16DCC  token: 0x60009F3
        public System.Void .ctor(Google.Protobuf.Reflection.UninterpretedOption.Types.NamePart other) { }
        // RVA: 0x09B169B8  token: 0x60009F4
        public virtual Google.Protobuf.Reflection.UninterpretedOption.Types.NamePart Clone() { }
        // RVA: 0x09AFF598  token: 0x60009F8
        public System.Void ClearNamePart_() { }
        // RVA: 0x04D86DE0  token: 0x60009FC
        public System.Void ClearIsExtension() { }
        // RVA: 0x09B16A18  token: 0x60009FD
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09B16A7C  token: 0x60009FE
        public virtual System.Boolean Equals(Google.Protobuf.Reflection.UninterpretedOption.Types.NamePart other) { }
        // RVA: 0x09B16B04  token: 0x60009FF
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B16C24  token: 0x6000A00
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x6000A01
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09B1700C  token: 0x6000A02
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09B16934  token: 0x6000A03
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09B16BB0  token: 0x6000A04
        public virtual System.Void MergeFrom(Google.Protobuf.Reflection.UninterpretedOption.Types.NamePart other) { }
        // RVA: 0x04D51840  token: 0x6000A05
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09B16F78  token: 0x6000A06
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09B16C70  token: 0x6000A07
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000E0  // size: 0x10
    public static class Types
    {
    }

    // TypeToken: 0x20000E3  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.UninterpretedOption.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B1A054  token: 0x6000A0B
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000A0C
        public System.Void .ctor() { }
        // RVA: 0x09B19B80  token: 0x6000A0D
        private Google.Protobuf.Reflection.UninterpretedOption <.cctor>b__83_0() { }

    }

    // TypeToken: 0x20000E7  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.SourceCodeInfo.Types.Location.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B19CD0  token: 0x6000A3C
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000A3D
        public System.Void .ctor() { }
        // RVA: 0x09B199B4  token: 0x6000A3E
        private Google.Protobuf.Reflection.SourceCodeInfo.Types.Location <.cctor>b__55_0() { }

    }

    // TypeToken: 0x20000E6  // size: 0x40
    public sealed class Location : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.SourceCodeInfo.Types.Location> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 PathFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<System.Int32> _repeated_path_codec;  // static @ 0x8
        private readonly Google.Protobuf.Collections.RepeatedField<System.Int32> path_;  // 0x18
        public static System.Int32 SpanFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<System.Int32> _repeated_span_codec;  // static @ 0x10
        private readonly Google.Protobuf.Collections.RepeatedField<System.Int32> span_;  // 0x20
        public static System.Int32 LeadingCommentsFieldNumber;  // const
        private static readonly System.String LeadingCommentsDefaultValue;  // static @ 0x18
        private System.String leadingComments_;  // 0x28
        public static System.Int32 TrailingCommentsFieldNumber;  // const
        private static readonly System.String TrailingCommentsDefaultValue;  // static @ 0x20
        private System.String trailingComments_;  // 0x30
        public static System.Int32 LeadingDetachedCommentsFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<System.String> _repeated_leadingDetachedComments_codec;  // static @ 0x28
        private readonly Google.Protobuf.Collections.RepeatedField<System.String> leadingDetachedComments_;  // 0x38

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.Reflection.SourceCodeInfo.Types.Location> Parser { get; /* RVA: 0x09B13D3C */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B13C88 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B1412C */ }
        Google.Protobuf.Collections.RepeatedField<System.Int32> Path { get; /* RVA: 0x0385B100 */ }
        Google.Protobuf.Collections.RepeatedField<System.Int32> Span { get; /* RVA: 0x04D862C0 */ }
        System.String LeadingComments { get; /* RVA: 0x09B13CE8 */ set; /* RVA: 0x09B1416C */ }
        System.Boolean HasLeadingComments { get; /* RVA: 0x068C60A8 */ }
        System.String TrailingComments { get; /* RVA: 0x09B13D8C */ set; /* RVA: 0x09B141D4 */ }
        System.Boolean HasTrailingComments { get; /* RVA: 0x09B11948 */ }
        Google.Protobuf.Collections.RepeatedField<System.String> LeadingDetachedComments { get; /* RVA: 0x04D85A50 */ }

        // Methods
        // RVA: 0x09B13A94  token: 0x6000A23
        public System.Void .ctor() { }
        // RVA: 0x09B13B88  token: 0x6000A24
        public System.Void .ctor(Google.Protobuf.Reflection.SourceCodeInfo.Types.Location other) { }
        // RVA: 0x09B13454  token: 0x6000A25
        public virtual Google.Protobuf.Reflection.SourceCodeInfo.Types.Location Clone() { }
        // RVA: 0x09849400  token: 0x6000A2B
        public System.Void ClearLeadingComments() { }
        // RVA: 0x067B5A00  token: 0x6000A2F
        public System.Void ClearTrailingComments() { }
        // RVA: 0x09B134B4  token: 0x6000A31
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09B13518  token: 0x6000A32
        public virtual System.Boolean Equals(Google.Protobuf.Reflection.SourceCodeInfo.Types.Location other) { }
        // RVA: 0x09B13650  token: 0x6000A33
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B13830  token: 0x6000A34
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x6000A35
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09B13FB8  token: 0x6000A36
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09B132D8  token: 0x6000A37
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09B13730  token: 0x6000A38
        public virtual System.Void MergeFrom(Google.Protobuf.Reflection.SourceCodeInfo.Types.Location other) { }
        // RVA: 0x04D51840  token: 0x6000A39
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09B13DE0  token: 0x6000A3A
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09B1387C  token: 0x6000A3B
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000E5  // size: 0x10
    public static class Types
    {
    }

    // TypeToken: 0x20000E8  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.SourceCodeInfo.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B19F28  token: 0x6000A3F
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000A40
        public System.Void .ctor() { }
        // RVA: 0x09B19720  token: 0x6000A41
        private Google.Protobuf.Reflection.SourceCodeInfo <.cctor>b__28_0() { }

    }

    // TypeToken: 0x20000EC  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.GeneratedCodeInfo.Types.Annotation.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B1A374  token: 0x6000A72
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000A73
        public System.Void .ctor() { }
        // RVA: 0x09B19A04  token: 0x6000A74
        private Google.Protobuf.Reflection.GeneratedCodeInfo.Types.Annotation <.cctor>b__55_0() { }

    }

    // TypeToken: 0x20000EB  // size: 0x38
    public sealed class Annotation : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.GeneratedCodeInfo.Types.Annotation> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        private System.Int32 _hasBits0;  // 0x18
        public static System.Int32 PathFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<System.Int32> _repeated_path_codec;  // static @ 0x8
        private readonly Google.Protobuf.Collections.RepeatedField<System.Int32> path_;  // 0x20
        public static System.Int32 SourceFileFieldNumber;  // const
        private static readonly System.String SourceFileDefaultValue;  // static @ 0x10
        private System.String sourceFile_;  // 0x28
        public static System.Int32 BeginFieldNumber;  // const
        private static readonly System.Int32 BeginDefaultValue;  // static @ 0x18
        private System.Int32 begin_;  // 0x30
        public static System.Int32 EndFieldNumber;  // const
        private static readonly System.Int32 EndDefaultValue;  // static @ 0x1c
        private System.Int32 end_;  // 0x34

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.Reflection.GeneratedCodeInfo.Types.Annotation> Parser { get; /* RVA: 0x09B09EDC */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B09E28 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B0A1F8 */ }
        Google.Protobuf.Collections.RepeatedField<System.Int32> Path { get; /* RVA: 0x04D862C0 */ }
        System.String SourceFile { get; /* RVA: 0x09B09F2C */ set; /* RVA: 0x09B0A238 */ }
        System.Boolean HasSourceFile { get; /* RVA: 0x068C60A8 */ }
        System.Int32 Begin { get; /* RVA: 0x09B09DD4 */ set; /* RVA: 0x04DA0EA0 */ }
        System.Boolean HasBegin { get; /* RVA: 0x0314ECE0 */ }
        System.Int32 End { get; /* RVA: 0x09B09E88 */ set; /* RVA: 0x04DA0EB0 */ }
        System.Boolean HasEnd { get; /* RVA: 0x09AFF0A8 */ }

        // Methods
        // RVA: 0x09B09D5C  token: 0x6000A57
        public System.Void .ctor() { }
        // RVA: 0x09B09CB0  token: 0x6000A58
        public System.Void .ctor(Google.Protobuf.Reflection.GeneratedCodeInfo.Types.Annotation other) { }
        // RVA: 0x09B097AC  token: 0x6000A59
        public virtual Google.Protobuf.Reflection.GeneratedCodeInfo.Types.Annotation Clone() { }
        // RVA: 0x09849400  token: 0x6000A5E
        public System.Void ClearSourceFile() { }
        // RVA: 0x04D86DE0  token: 0x6000A62
        public System.Void ClearBegin() { }
        // RVA: 0x04DA0DF0  token: 0x6000A66
        public System.Void ClearEnd() { }
        // RVA: 0x09B098F8  token: 0x6000A67
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09B0980C  token: 0x6000A68
        public virtual System.Boolean Equals(Google.Protobuf.Reflection.GeneratedCodeInfo.Types.Annotation other) { }
        // RVA: 0x09B0995C  token: 0x6000A69
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B09AC0  token: 0x6000A6A
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x6000A6B
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09B0A0D0  token: 0x6000A6C
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09B09678  token: 0x6000A6D
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09B099F4  token: 0x6000A6E
        public virtual System.Void MergeFrom(Google.Protobuf.Reflection.GeneratedCodeInfo.Types.Annotation other) { }
        // RVA: 0x04D51840  token: 0x6000A6F
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09B09F80  token: 0x6000A70
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09B09B0C  token: 0x6000A71
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000EA  // size: 0x10
    public static class Types
    {
    }

    // TypeToken: 0x20000ED  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.GeneratedCodeInfo.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x09B1A11C  token: 0x6000A75
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000A76
        public System.Void .ctor() { }
        // RVA: 0x09B196D0  token: 0x6000A77
        private Google.Protobuf.Reflection.GeneratedCodeInfo <.cctor>b__28_0() { }

    }

    // TypeToken: 0x20000F2
    public sealed class IndexedConverter`2 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000A96
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000A97
        public virtual TOutput Invoke(TInput element, System.Int32 index) { }
        // RVA: -1  // runtime  token: 0x6000A98
        public virtual System.IAsyncResult BeginInvoke(TInput element, System.Int32 index, System.AsyncCallback callback, System.Object object) { }
        // RVA: -1  // runtime  token: 0x6000A99
        public virtual TOutput EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x20000F5  // size: 0x20
    public sealed class <>c__DisplayClass4_0
    {
        // Fields
        public Google.Protobuf.Reflection.FileDescriptor file;  // 0x10
        public Google.Protobuf.Reflection.EnumDescriptor <>4__this;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000AAC
        public System.Void .ctor() { }
        // RVA: 0x09B19BD0  token: 0x6000AAD
        private Google.Protobuf.Reflection.EnumValueDescriptor <.ctor>b__0(Google.Protobuf.Reflection.EnumValueDescriptorProto value, System.Int32 i) { }

    }

    // TypeToken: 0x20000F9  // size: 0x20
    public sealed class <>c__DisplayClass2_0
    {
        // Fields
        public Google.Protobuf.Extension[] extensions;  // 0x10
        public Google.Protobuf.Reflection.FileDescriptor file;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000AC4
        public System.Void .ctor() { }
        // RVA: 0x09B246E0  token: 0x6000AC5
        private Google.Protobuf.Reflection.FieldDescriptor <.ctor>b__0(Google.Protobuf.Reflection.FieldDescriptorProto extension, System.Int32 i) { }

    }

    // TypeToken: 0x20000FA  // size: 0x20
    public sealed class <>c__DisplayClass3_0
    {
        // Fields
        public Google.Protobuf.Extension[] extensions;  // 0x10
        public Google.Protobuf.Reflection.MessageDescriptor message;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000AC6
        public System.Void .ctor() { }
        // RVA: 0x09B24AF8  token: 0x6000AC7
        private Google.Protobuf.Reflection.FieldDescriptor <.ctor>b__0(Google.Protobuf.Reflection.FieldDescriptorProto extension, System.Int32 i) { }

    }

    // TypeToken: 0x20000FB  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.ExtensionCollection.<>c <>9;  // static @ 0x0
        public static System.Func<System.Collections.Generic.KeyValuePair<Google.Protobuf.Reflection.MessageDescriptor,System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor>>,Google.Protobuf.Reflection.MessageDescriptor> <>9__9_0;  // static @ 0x8
        public static System.Func<System.Collections.Generic.KeyValuePair<Google.Protobuf.Reflection.MessageDescriptor,System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor>>,System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor>> <>9__9_1;  // static @ 0x10
        public static System.Func<System.Collections.Generic.KeyValuePair<Google.Protobuf.Reflection.MessageDescriptor,System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor>>,Google.Protobuf.Reflection.MessageDescriptor> <>9__9_2;  // static @ 0x18
        public static System.Func<Google.Protobuf.Reflection.FieldDescriptor,System.Int32> <>9__9_4;  // static @ 0x20
        public static System.Func<System.Collections.Generic.KeyValuePair<Google.Protobuf.Reflection.MessageDescriptor,System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor>>,System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor>> <>9__9_3;  // static @ 0x28

        // Methods
        // RVA: 0x09B25134  token: 0x6000AC8
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000AC9
        public System.Void .ctor() { }
        // RVA: 0x09B243B8  token: 0x6000ACA
        private Google.Protobuf.Reflection.MessageDescriptor <CrossLink>b__9_0(System.Collections.Generic.KeyValuePair<Google.Protobuf.Reflection.MessageDescriptor,System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor>> kvp) { }
        // RVA: 0x09B243EC  token: 0x6000ACB
        private System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor> <CrossLink>b__9_1(System.Collections.Generic.KeyValuePair<Google.Protobuf.Reflection.MessageDescriptor,System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor>> kvp) { }
        // RVA: 0x09B24468  token: 0x6000ACC
        private Google.Protobuf.Reflection.MessageDescriptor <CrossLink>b__9_2(System.Collections.Generic.KeyValuePair<Google.Protobuf.Reflection.MessageDescriptor,System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor>> kvp) { }
        // RVA: 0x09B2449C  token: 0x6000ACD
        private System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor> <CrossLink>b__9_3(System.Collections.Generic.KeyValuePair<Google.Protobuf.Reflection.MessageDescriptor,System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor>> kvp) { }
        // RVA: 0x09B24614  token: 0x6000ACE
        private System.Int32 <CrossLink>b__9_4(Google.Protobuf.Reflection.FieldDescriptor field) { }

    }

    // TypeToken: 0x2000101  // size: 0x20
    public sealed class <>c__DisplayClass2_0
    {
        // Fields
        public Google.Protobuf.Reflection.FileDescriptor <>4__this;  // 0x10
        public Google.Protobuf.Reflection.GeneratedClrTypeInfo generatedCodeInfo;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000B1C
        public System.Void .ctor() { }
        // RVA: 0x09B24890  token: 0x6000B1D
        private Google.Protobuf.Reflection.MessageDescriptor <.ctor>b__0(Google.Protobuf.Reflection.DescriptorProto message, System.Int32 index) { }
        // RVA: 0x09B24950  token: 0x6000B1E
        private Google.Protobuf.Reflection.EnumDescriptor <.ctor>b__1(Google.Protobuf.Reflection.EnumDescriptorProto enumType, System.Int32 index) { }
        // RVA: 0x09B24A10  token: 0x6000B1F
        private Google.Protobuf.Reflection.ServiceDescriptor <.ctor>b__2(Google.Protobuf.Reflection.ServiceDescriptorProto service, System.Int32 index) { }

    }

    // TypeToken: 0x2000102  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.FileDescriptor.<>c <>9;  // static @ 0x0
        public static System.Func<Google.Protobuf.Reflection.FileDescriptor,System.String> <>9__9_0;  // static @ 0x8
        public static System.Func<Google.Protobuf.Reflection.GeneratedClrTypeInfo,System.Boolean> <>9__54_0;  // static @ 0x10
        public static System.Func<Google.Protobuf.Reflection.FieldDescriptor,Google.Protobuf.Extension> <>9__55_0;  // static @ 0x18
        public static System.Func<Google.Protobuf.Extension,System.Boolean> <>9__55_1;  // static @ 0x20
        public static System.Func<Google.Protobuf.Reflection.FieldDescriptor,Google.Protobuf.Extension> <>9__56_0;  // static @ 0x28
        public static System.Func<Google.Protobuf.Extension,System.Boolean> <>9__56_1;  // static @ 0x30

        // Methods
        // RVA: 0x09B252C4  token: 0x6000B20
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000B21
        public System.Void .ctor() { }
        // RVA: 0x09B24634  token: 0x6000B22
        private System.String <DeterminePublicDependencies>b__9_0(Google.Protobuf.Reflection.FileDescriptor file) { }
        // RVA: 0x0721682C  token: 0x6000B23
        private System.Boolean <GetAllGeneratedExtensions>b__54_0(Google.Protobuf.Reflection.GeneratedClrTypeInfo t) { }
        // RVA: 0x09B2466C  token: 0x6000B24
        private Google.Protobuf.Extension <GetAllDependedExtensions>b__55_0(Google.Protobuf.Reflection.FieldDescriptor s) { }
        // RVA: 0x0721682C  token: 0x6000B25
        private System.Boolean <GetAllDependedExtensions>b__55_1(Google.Protobuf.Extension e) { }
        // RVA: 0x09B2466C  token: 0x6000B26
        private Google.Protobuf.Extension <GetAllDependedExtensionsFromMessage>b__56_0(Google.Protobuf.Reflection.FieldDescriptor s) { }
        // RVA: 0x0721682C  token: 0x6000B27
        private System.Boolean <GetAllDependedExtensionsFromMessage>b__56_1(Google.Protobuf.Extension e) { }

    }

    // TypeToken: 0x2000108  // size: 0x18
    public sealed class FieldCollection
    {
        // Fields
        private readonly Google.Protobuf.Reflection.MessageDescriptor messageDescriptor;  // 0x10

        // Properties
        Google.Protobuf.Reflection.FieldDescriptor Item { get; /* RVA: 0x09B1C140 */ }
        Google.Protobuf.Reflection.FieldDescriptor Item { get; /* RVA: 0x09B1C140 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x6000B5C
        private System.Void .ctor(Google.Protobuf.Reflection.MessageDescriptor messageDescriptor) { }
        // RVA: 0x09B1C124  token: 0x6000B5D
        public System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor> InDeclarationOrder() { }
        // RVA: 0x08110B18  token: 0x6000B5E
        public System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor> InFieldNumberOrder() { }
        // RVA: 0x09B1C108  token: 0x6000B5F
        private System.Collections.Generic.IDictionary<System.String,Google.Protobuf.Reflection.FieldDescriptor> ByJsonName() { }

    }

    // TypeToken: 0x2000109  // size: 0x28
    public sealed class <>c__DisplayClass5_0
    {
        // Fields
        public Google.Protobuf.Reflection.FileDescriptor file;  // 0x10
        public Google.Protobuf.Reflection.MessageDescriptor <>4__this;  // 0x18
        public Google.Protobuf.Reflection.GeneratedClrTypeInfo generatedCodeInfo;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x6000B62
        public System.Void .ctor() { }
        // RVA: 0x09B24E20  token: 0x6000B63
        private Google.Protobuf.Reflection.OneofDescriptor <.ctor>b__0(Google.Protobuf.Reflection.OneofDescriptorProto oneof, System.Int32 index) { }
        // RVA: 0x09B24EE4  token: 0x6000B64
        private Google.Protobuf.Reflection.MessageDescriptor <.ctor>b__1(Google.Protobuf.Reflection.DescriptorProto type, System.Int32 index) { }
        // RVA: 0x09B24FA8  token: 0x6000B65
        private Google.Protobuf.Reflection.EnumDescriptor <.ctor>b__2(Google.Protobuf.Reflection.EnumDescriptorProto type, System.Int32 index) { }
        // RVA: 0x09B2506C  token: 0x6000B66
        private Google.Protobuf.Reflection.FieldDescriptor <.ctor>b__3(Google.Protobuf.Reflection.FieldDescriptorProto field, System.Int32 index) { }

    }

    // TypeToken: 0x200010A  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.MessageDescriptor.<>c <>9;  // static @ 0x0
        public static System.Func<Google.Protobuf.Reflection.FieldDescriptor,System.Int32> <>9__5_4;  // static @ 0x8

        // Methods
        // RVA: 0x09B25260  token: 0x6000B67
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000B68
        public System.Void .ctor() { }
        // RVA: 0x09B24614  token: 0x6000B69
        private System.Int32 <.ctor>b__5_4(Google.Protobuf.Reflection.FieldDescriptor field) { }

    }

    // TypeToken: 0x200010D  // size: 0x18
    public sealed class <>c__DisplayClass4_0
    {
        // Fields
        public Google.Protobuf.Reflection.OneofDescriptor descriptor;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000B7E
        public System.Void .ctor() { }
        // RVA: 0x09B24D1C  token: 0x6000B7F
        private System.Int32 <ForSyntheticOneof>b__0(Google.Protobuf.IMessage message) { }
        // RVA: 0x09B24DB4  token: 0x6000B80
        private System.Void <ForSyntheticOneof>b__1(Google.Protobuf.IMessage message) { }

    }

    // TypeToken: 0x200010F  // size: 0x18
    public sealed class <>c__DisplayClass3_0
    {
        // Fields
        public System.Int32 index;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000B8F
        public System.Void .ctor() { }
        // RVA: 0x09B24A98  token: 0x6000B90
        private System.Boolean <.ctor>b__0(Google.Protobuf.Reflection.FieldDescriptorProto fieldProto) { }

    }

    // TypeToken: 0x2000113
    public interface IReflectionHelper
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000BA7
        public virtual System.Func<Google.Protobuf.IMessage,System.Int32> CreateFuncIMessageInt32(System.Reflection.MethodInfo method) { }
        // RVA: -1  // abstract  token: 0x6000BA8
        public virtual System.Action<Google.Protobuf.IMessage> CreateActionIMessage(System.Reflection.MethodInfo method) { }
        // RVA: -1  // abstract  token: 0x6000BA9
        public virtual System.Func<Google.Protobuf.IMessage,System.Object> CreateFuncIMessageObject(System.Reflection.MethodInfo method) { }
        // RVA: -1  // abstract  token: 0x6000BAA
        public virtual System.Action<Google.Protobuf.IMessage,System.Object> CreateActionIMessageObject(System.Reflection.MethodInfo method) { }
        // RVA: -1  // abstract  token: 0x6000BAB
        public virtual System.Func<Google.Protobuf.IMessage,System.Boolean> CreateFuncIMessageBool(System.Reflection.MethodInfo method) { }

    }

    // TypeToken: 0x2000114
    public interface IExtensionReflectionHelper
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000BAC
        public virtual System.Object GetExtension(Google.Protobuf.IMessage message) { }
        // RVA: -1  // abstract  token: 0x6000BAD
        public virtual System.Void SetExtension(Google.Protobuf.IMessage message, System.Object value) { }
        // RVA: -1  // abstract  token: 0x6000BAE
        public virtual System.Boolean HasExtension(Google.Protobuf.IMessage message) { }
        // RVA: -1  // abstract  token: 0x6000BAF
        public virtual System.Void ClearExtension(Google.Protobuf.IMessage message) { }

    }

    // TypeToken: 0x2000115
    public interface IExtensionSetReflector
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000BB0
        public virtual System.Func<Google.Protobuf.IMessage,System.Boolean> CreateIsInitializedCaller() { }

    }

    // TypeToken: 0x2000117
    public sealed class <>c__DisplayClass0_0
    {
        // Fields
        public System.Func<T1,System.Int32> del;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000BB7
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000BB8
        private System.Int32 <CreateFuncIMessageInt32>b__0(Google.Protobuf.IMessage message) { }

    }

    // TypeToken: 0x2000118
    public sealed class <>c__DisplayClass0_1
    {
        // Fields
        public System.Func<T1,T2> del;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000BB9
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000BBA
        private System.Int32 <CreateFuncIMessageInt32>b__1(Google.Protobuf.IMessage message) { }

    }

    // TypeToken: 0x2000119
    public sealed class <>c__DisplayClass1_0
    {
        // Fields
        public System.Action<T1> del;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000BBB
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000BBC
        private System.Void <CreateActionIMessage>b__0(Google.Protobuf.IMessage message) { }

    }

    // TypeToken: 0x200011A
    public sealed class <>c__DisplayClass2_0
    {
        // Fields
        public System.Func<T1,T2> del;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000BBD
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000BBE
        private System.Object <CreateFuncIMessageObject>b__0(Google.Protobuf.IMessage message) { }

    }

    // TypeToken: 0x200011B
    public sealed class <>c__DisplayClass3_0
    {
        // Fields
        public System.Action<T1,T2> del;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000BBF
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000BC0
        private System.Void <CreateActionIMessageObject>b__0(Google.Protobuf.IMessage message, System.Object arg) { }

    }

    // TypeToken: 0x200011C
    public sealed class <>c__DisplayClass4_0
    {
        // Fields
        public System.Func<T1,System.Boolean> del;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000BC1
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000BC2
        private System.Boolean <CreateFuncIMessageBool>b__0(Google.Protobuf.IMessage message) { }

    }

    // TypeToken: 0x2000116
    public class ReflectionHelper`2 : IReflectionHelper
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000BB1
        public virtual System.Func<Google.Protobuf.IMessage,System.Int32> CreateFuncIMessageInt32(System.Reflection.MethodInfo method) { }
        // RVA: -1  // not resolved  token: 0x6000BB2
        public virtual System.Action<Google.Protobuf.IMessage> CreateActionIMessage(System.Reflection.MethodInfo method) { }
        // RVA: -1  // not resolved  token: 0x6000BB3
        public virtual System.Func<Google.Protobuf.IMessage,System.Object> CreateFuncIMessageObject(System.Reflection.MethodInfo method) { }
        // RVA: -1  // not resolved  token: 0x6000BB4
        public virtual System.Action<Google.Protobuf.IMessage,System.Object> CreateActionIMessageObject(System.Reflection.MethodInfo method) { }
        // RVA: -1  // not resolved  token: 0x6000BB5
        public virtual System.Func<Google.Protobuf.IMessage,System.Boolean> CreateFuncIMessageBool(System.Reflection.MethodInfo method) { }
        // RVA: -1  // not resolved  token: 0x6000BB6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200011D
    public class ExtensionReflectionHelper`2 : IExtensionReflectionHelper
    {
        // Fields
        private readonly Google.Protobuf.Extension extension;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000BC3
        public System.Void .ctor(Google.Protobuf.Extension extension) { }
        // RVA: -1  // not resolved  token: 0x6000BC4
        public virtual System.Object GetExtension(Google.Protobuf.IMessage message) { }
        // RVA: -1  // not resolved  token: 0x6000BC5
        public virtual System.Boolean HasExtension(Google.Protobuf.IMessage message) { }
        // RVA: -1  // not resolved  token: 0x6000BC6
        public virtual System.Void SetExtension(Google.Protobuf.IMessage message, System.Object value) { }
        // RVA: -1  // not resolved  token: 0x6000BC7
        public virtual System.Void ClearExtension(Google.Protobuf.IMessage message) { }

    }

    // TypeToken: 0x200011F
    public sealed class <>c__DisplayClass0_0
    {
        // Fields
        public System.Func<T1,Google.Protobuf.ExtensionSet<T1>> getFunc;  // 0x0
        public System.Func<Google.Protobuf.ExtensionSet<T1>,System.Boolean> initializedFunc;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000BCA
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000BCB
        private System.Boolean <CreateIsInitializedCaller>b__0(Google.Protobuf.IMessage m) { }

    }

    // TypeToken: 0x200011E
    public class ExtensionSetReflector`1 : IExtensionSetReflector
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000BC8
        public virtual System.Func<Google.Protobuf.IMessage,System.Boolean> CreateIsInitializedCaller() { }
        // RVA: -1  // not resolved  token: 0x6000BC9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000120  // size: 0x14
    public sealed struct SampleEnum
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Google.Protobuf.Reflection.ReflectionUtil.SampleEnum X;  // const

    }

    // TypeToken: 0x2000123  // size: 0x20
    public sealed class <>c__DisplayClass2_0
    {
        // Fields
        public Google.Protobuf.Reflection.FileDescriptor file;  // 0x10
        public Google.Protobuf.Reflection.ServiceDescriptor <>4__this;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000BDC
        public System.Void .ctor() { }
        // RVA: 0x09B247F4  token: 0x6000BDD
        private Google.Protobuf.Reflection.MethodDescriptor <.ctor>b__0(Google.Protobuf.Reflection.MethodDescriptorProto method, System.Int32 i) { }

    }

    // TypeToken: 0x2000125  // size: 0x20
    public sealed class <>c__DisplayClass3_0
    {
        // Fields
        public Google.Protobuf.Reflection.SingleFieldAccessor <>4__this;  // 0x10
        public Google.Protobuf.Reflection.FieldDescriptor descriptor;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000BE3
        public System.Void .ctor() { }
        // RVA: 0x09B24AD0  token: 0x6000BE4
        private System.Boolean <.ctor>b__0(Google.Protobuf.IMessage message) { }
        // RVA: 0x09B24C30  token: 0x6000BE5
        private System.Void <.ctor>b__1(Google.Protobuf.IMessage message) { }

    }

    // TypeToken: 0x2000126  // size: 0x20
    public sealed class <>c__DisplayClass3_1
    {
        // Fields
        public Google.Protobuf.Reflection.OneofAccessor oneofAccessor;  // 0x10
        public Google.Protobuf.Reflection.SingleFieldAccessor.<>c__DisplayClass3_0 CS$<>8__locals1;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000BE6
        public System.Void .ctor() { }
        // RVA: 0x09B24C58  token: 0x6000BE7
        private System.Boolean <.ctor>b__2(Google.Protobuf.IMessage message) { }
        // RVA: 0x09B24C90  token: 0x6000BE8
        private System.Void <.ctor>b__3(Google.Protobuf.IMessage message) { }

    }

    // TypeToken: 0x2000127  // size: 0x20
    public sealed class <>c__DisplayClass3_2
    {
        // Fields
        public System.Object defaultValue;  // 0x10
        public Google.Protobuf.Reflection.SingleFieldAccessor.<>c__DisplayClass3_0 CS$<>8__locals2;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000BE9
        public System.Void .ctor() { }
        // RVA: 0x09B24CE8  token: 0x6000BEA
        private System.Void <.ctor>b__5(Google.Protobuf.IMessage message) { }

    }

    // TypeToken: 0x2000128  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.SingleFieldAccessor.<>c <>9;  // static @ 0x0
        public static System.Func<Google.Protobuf.IMessage,System.Boolean> <>9__3_4;  // static @ 0x8

        // Methods
        // RVA: 0x09B251FC  token: 0x6000BEB
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000BEC
        public System.Void .ctor() { }
        // RVA: 0x09B24684  token: 0x6000BED
        private System.Boolean <.ctor>b__3_4(Google.Protobuf.IMessage message) { }

    }

    // TypeToken: 0x200012A  // size: 0x20
    public class Builder
    {
        // Fields
        private readonly System.Collections.Generic.Dictionary<System.String,Google.Protobuf.Reflection.MessageDescriptor> types;  // 0x10
        private readonly System.Collections.Generic.HashSet<System.String> fileDescriptorNames;  // 0x18

        // Methods
        // RVA: 0x09B1BF9C  token: 0x6000BF6
        private System.Void .ctor() { }
        // RVA: 0x09B1BAF4  token: 0x6000BF7
        private System.Void AddFile(Google.Protobuf.Reflection.FileDescriptor fileDescriptor) { }
        // RVA: 0x09B1BDB4  token: 0x6000BF8
        private System.Void AddMessage(Google.Protobuf.Reflection.MessageDescriptor messageDescriptor) { }
        // RVA: 0x09B1BF38  token: 0x6000BF9
        private Google.Protobuf.Reflection.TypeRegistry Build() { }

    }

    // TypeToken: 0x200012B  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Reflection.TypeRegistry.<>c <>9;  // static @ 0x0
        public static System.Func<Google.Protobuf.Reflection.MessageDescriptor,Google.Protobuf.Reflection.FileDescriptor> <>9__9_0;  // static @ 0x8

        // Methods
        // RVA: 0x09B25198  token: 0x6000BFA
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000BFB
        public System.Void .ctor() { }
        // RVA: 0x09B24654  token: 0x6000BFC
        private Google.Protobuf.Reflection.FileDescriptor <FromMessages>b__9_0(Google.Protobuf.Reflection.MessageDescriptor md) { }

    }

    // TypeToken: 0x2000130
    public class DictionaryEnumerator : System.Collections.IDictionaryEnumerator, System.Collections.IEnumerator
    {
        // Fields
        private readonly System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey,TValue>> enumerator;  // 0x0

        // Properties
        System.Object Current { get; /* RVA: -1  // not resolved */ }
        System.Collections.DictionaryEntry Entry { get; /* RVA: -1  // not resolved */ }
        System.Object Key { get; /* RVA: -1  // not resolved */ }
        System.Object Value { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000C32
        private System.Void .ctor(System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey,TValue>> enumerator) { }
        // RVA: -1  // not resolved  token: 0x6000C33
        public virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x6000C34
        public virtual System.Void Reset() { }

    }

    // TypeToken: 0x2000131
    public sealed class Codec
    {
        // Fields
        private readonly Google.Protobuf.FieldCodec<TKey> keyCodec;  // 0x0
        private readonly Google.Protobuf.FieldCodec<TValue> valueCodec;  // 0x0
        private readonly System.UInt32 mapTag;  // 0x0

        // Properties
        Google.Protobuf.FieldCodec<TKey> KeyCodec { get; /* RVA: -1  // not resolved */ }
        Google.Protobuf.FieldCodec<TValue> ValueCodec { get; /* RVA: -1  // not resolved */ }
        System.UInt32 MapTag { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000C39
        public System.Void .ctor(Google.Protobuf.FieldCodec<TKey> keyCodec, Google.Protobuf.FieldCodec<TValue> valueCodec, System.UInt32 mapTag) { }

    }

    // TypeToken: 0x2000132
    public class MapView`1 : System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.ICollection
    {
        // Fields
        private readonly Google.Protobuf.Collections.MapField<TKey,TValue> parent;  // 0x0
        private readonly System.Func<System.Collections.Generic.KeyValuePair<TKey,TValue>,T> projection;  // 0x0
        private readonly System.Func<T,System.Boolean> containsCheck;  // 0x0

        // Properties
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        System.Boolean IsReadOnly { get; /* RVA: -1  // not resolved */ }
        System.Boolean IsSynchronized { get; /* RVA: -1  // not resolved */ }
        System.Object SyncRoot { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000C3D
        private System.Void .ctor(Google.Protobuf.Collections.MapField<TKey,TValue> parent, System.Func<System.Collections.Generic.KeyValuePair<TKey,TValue>,T> projection, System.Func<T,System.Boolean> containsCheck) { }
        // RVA: -1  // not resolved  token: 0x6000C42
        public virtual System.Void Add(T item) { }
        // RVA: -1  // not resolved  token: 0x6000C43
        public virtual System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x6000C44
        public virtual System.Boolean Contains(T item) { }
        // RVA: -1  // not resolved  token: 0x6000C45
        public virtual System.Void CopyTo(T[] array, System.Int32 arrayIndex) { }
        // RVA: -1  // not resolved  token: 0x6000C46
        public virtual System.Collections.Generic.IEnumerator<T> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000C47
        public virtual System.Boolean Remove(T item) { }
        // RVA: -1  // not resolved  token: 0x6000C48
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000C49
        public virtual System.Void CopyTo(System.Array array, System.Int32 index) { }

    }

    // TypeToken: 0x2000133
    public sealed class <>c__DisplayClass7_0
    {
        // Fields
        public TValue value;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000C4A
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000C4B
        private System.Boolean <ContainsValue>b__0(System.Collections.Generic.KeyValuePair<TKey,TValue> pair) { }

    }

    // TypeToken: 0x2000134
    public sealed class <>c
    {
        // Fields
        public static readonly Google.Protobuf.Collections.MapField.<>c<TKey,TValue> <>9;  // static @ 0x0
        public static System.Func<System.Collections.Generic.KeyValuePair<TKey,TValue>,TKey> <>9__14_0;  // static @ 0x0
        public static System.Func<System.Collections.Generic.KeyValuePair<TKey,TValue>,TValue> <>9__16_0;  // static @ 0x0
        public static System.Func<System.Collections.Generic.KeyValuePair<TKey,TValue>,System.Collections.DictionaryEntry> <>9__43_0;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000C4C
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x6000C4D
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000C4E
        private TKey <get_Keys>b__14_0(System.Collections.Generic.KeyValuePair<TKey,TValue> pair) { }
        // RVA: -1  // not resolved  token: 0x6000C4F
        private TValue <get_Values>b__16_0(System.Collections.Generic.KeyValuePair<TKey,TValue> pair) { }
        // RVA: -1  // not resolved  token: 0x6000C50
        private System.Collections.DictionaryEntry <System.Collections.ICollection.CopyTo>b__43_0(System.Collections.Generic.KeyValuePair<TKey,TValue> pair) { }

    }

    // TypeToken: 0x2000136  // size: 0x10
    public class BitwiseDoubleEqualityComparerImpl : System.Collections.Generic.EqualityComparer`1
    {
        // Methods
        // RVA: 0x09B1B6B8  token: 0x6000C57
        public virtual System.Boolean Equals(System.Double x, System.Double y) { }
        // RVA: 0x09B1B714  token: 0x6000C58
        public virtual System.Int32 GetHashCode(System.Double obj) { }
        // RVA: 0x049DEDE0  token: 0x6000C59
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000137  // size: 0x10
    public class BitwiseSingleEqualityComparerImpl : System.Collections.Generic.EqualityComparer`1
    {
        // Methods
        // RVA: 0x09B1BA34  token: 0x6000C5A
        public virtual System.Boolean Equals(System.Single x, System.Single y) { }
        // RVA: 0x09B1BAA0  token: 0x6000C5B
        public virtual System.Int32 GetHashCode(System.Single obj) { }
        // RVA: 0x049DEDB0  token: 0x6000C5C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000138  // size: 0x10
    public class BitwiseNullableDoubleEqualityComparerImpl : System.Collections.Generic.EqualityComparer`1
    {
        // Methods
        // RVA: 0x09B1B76C  token: 0x6000C5D
        public virtual System.Boolean Equals(System.Nullable<System.Double> x, System.Nullable<System.Double> y) { }
        // RVA: 0x09B1B834  token: 0x6000C5E
        public virtual System.Int32 GetHashCode(System.Nullable<System.Double> obj) { }
        // RVA: 0x049DED80  token: 0x6000C5F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000139  // size: 0x10
    public class BitwiseNullableSingleEqualityComparerImpl : System.Collections.Generic.EqualityComparer`1
    {
        // Methods
        // RVA: 0x09B1B8CC  token: 0x6000C60
        public virtual System.Boolean Equals(System.Nullable<System.Single> x, System.Nullable<System.Single> y) { }
        // RVA: 0x09B1B99C  token: 0x6000C61
        public virtual System.Int32 GetHashCode(System.Nullable<System.Single> obj) { }
        // RVA: 0x049DED50  token: 0x6000C62
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200013C
    public sealed class <GetEnumerator>d__29 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private T <>2__current;  // 0x0
        public Google.Protobuf.Collections.RepeatedField<T> <>4__this;  // 0x0
        private System.Int32 <i>5__2;  // 0x0

        // Properties
        T System.Collections.Generic.IEnumerator<T>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000CA5
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x6000CA6
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000CA7
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x6000CA9
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200013E  // size: 0x3C
    public sealed struct __StaticArrayInitTypeSize=44
    {
    }

    // TypeToken: 0x200013D  // size: 0x10
    public sealed class <PrivateImplementationDetails>
    {
        // Fields
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=44 1EFF1891409D680B30DE202BD7A16F945ED09F02601C0E7B90FD917BE1162300;  // static @ 0x0

    }

namespace Google.Protobuf
{

    // TypeToken: 0x2000006  // size: 0x10
    public static class ByteArray
    {
        // Fields
        private static System.Int32 CopyThreshold;  // const

        // Methods
        // RVA: 0x032A8AA0  token: 0x6000012
        private static System.Void Copy(System.Byte[] src, System.Int32 srcOffset, System.Byte[] dst, System.Int32 dstOffset, System.Int32 count) { }
        // RVA: 0x09ACC204  token: 0x6000013
        private static System.Void Reverse(System.Byte[] bytes) { }

    }

    // TypeToken: 0x2000007  // size: 0x20
    public sealed class ByteString : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.IEquatable`1
    {
        // Fields
        private static readonly Google.Protobuf.ByteString empty;  // static @ 0x0
        private readonly System.ReadOnlyMemory<System.Byte> bytes;  // 0x10

        // Properties
        Google.Protobuf.ByteString Empty { get; /* RVA: 0x09ACCEC4 */ }
        System.Int32 Length { get; /* RVA: 0x04DA0CD0 */ }
        System.Boolean IsEmpty { get; /* RVA: 0x09ACCEF8 */ }
        System.ReadOnlySpan<System.Byte> Span { get; /* RVA: 0x053972F0 */ }
        System.ReadOnlyMemory<System.Byte> Memory { get; /* RVA: 0x02B2D510 */ }
        System.Byte Item { get; /* RVA: 0x09ACCF04 */ }

        // Methods
        // RVA: 0x032A6AF0  token: 0x6000014
        private static Google.Protobuf.ByteString AttachBytes(System.ReadOnlyMemory<System.Byte> bytes) { }
        // RVA: 0x032A6A60  token: 0x6000015
        private static Google.Protobuf.ByteString AttachBytes(System.Byte[] bytes) { }
        // RVA: 0x032A8A90  token: 0x6000016
        private System.Void .ctor(System.ReadOnlyMemory<System.Byte> bytes) { }
        // RVA: 0x032196A0  token: 0x600001C
        public System.Byte[] ToByteArray() { }
        // RVA: 0x09ACCB90  token: 0x600001D
        public System.String ToBase64() { }
        // RVA: 0x09ACC7B4  token: 0x600001E
        public static Google.Protobuf.ByteString FromBase64(System.String bytes) { }
        // RVA: 0x09ACC8FC  token: 0x600001F
        public static Google.Protobuf.ByteString FromStream(System.IO.Stream stream) { }
        // RVA: 0x09ACC894  token: 0x6000020
        public static System.Threading.Tasks.Task<Google.Protobuf.ByteString> FromStreamAsync(System.IO.Stream stream, System.Threading.CancellationToken cancellationToken) { }
        // RVA: 0x04CCA610  token: 0x6000021
        public static Google.Protobuf.ByteString CopyFrom(System.Byte[] bytes) { }
        // RVA: 0x032A89E0  token: 0x6000022
        public static Google.Protobuf.ByteString CopyFrom(System.Byte[] bytes, System.Int32 offset, System.Int32 count) { }
        // RVA: 0x09ACC458  token: 0x6000023
        public static Google.Protobuf.ByteString CopyFrom(System.ReadOnlySpan<System.Byte> bytes) { }
        // RVA: 0x09ACC3B4  token: 0x6000024
        public static Google.Protobuf.ByteString CopyFrom(System.String text, System.Text.Encoding encoding) { }
        // RVA: 0x09ACC35C  token: 0x6000025
        public static Google.Protobuf.ByteString CopyFromUtf8(System.String text) { }
        // RVA: 0x09ACCCC8  token: 0x6000027
        public System.String ToString(System.Text.Encoding encoding) { }
        // RVA: 0x09ACCCA4  token: 0x6000028
        public System.String ToStringUtf8() { }
        // RVA: 0x09ACCA8C  token: 0x6000029
        public virtual System.Collections.Generic.IEnumerator<System.Byte> GetEnumerator() { }
        // RVA: 0x09ACCB88  token: 0x600002A
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: 0x09ACC580  token: 0x600002B
        public Google.Protobuf.CodedInputStream CreateCodedInput() { }
        // RVA: 0x03218B90  token: 0x600002C
        public static System.Boolean op_Equality(Google.Protobuf.ByteString lhs, Google.Protobuf.ByteString rhs) { }
        // RVA: 0x09ACCF64  token: 0x600002D
        public static System.Boolean op_Inequality(Google.Protobuf.ByteString lhs, Google.Protobuf.ByteString rhs) { }
        // RVA: 0x09ACC6E8  token: 0x600002E
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x09ACCAFC  token: 0x600002F
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09ACC758  token: 0x6000030
        public virtual System.Boolean Equals(Google.Protobuf.ByteString other) { }
        // RVA: 0x09ACC4F8  token: 0x6000031
        public System.Void CopyTo(System.Byte[] array, System.Int32 position) { }
        // RVA: 0x09ACCDC4  token: 0x6000032
        public System.Void WriteTo(System.IO.Stream outputStream) { }
        // RVA: 0x04CD7D50  token: 0x6000033
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000008  // size: 0x10
    public static class ByteStringAsync
    {
        // Methods
        // RVA: 0x09ACC26C  token: 0x6000034
        private static System.Threading.Tasks.Task<Google.Protobuf.ByteString> FromStreamAsyncCore(System.IO.Stream stream, System.Threading.CancellationToken cancellationToken) { }

    }

    // TypeToken: 0x200000A  // size: 0xA8
    public sealed class CodedInputStream : System.IDisposable
    {
        // Fields
        private readonly System.Boolean leaveOpen;  // 0x10
        private readonly System.Byte[] buffer;  // 0x18
        private readonly System.IO.Stream input;  // 0x20
        private Google.Protobuf.ParserInternalState state;  // 0x28
        private static System.Int32 DefaultRecursionLimit;  // const
        private static System.Int32 DefaultSizeLimit;  // const
        private static System.Int32 BufferSize;  // const

        // Properties
        System.Int64 Position { get; /* RVA: 0x09ACDD88 */ }
        System.UInt32 LastTag { get; /* RVA: 0x04D86890 */ }
        System.Int32 SizeLimit { get; /* RVA: 0x04D86B20 */ }
        System.Int32 RecursionLimit { get; /* RVA: 0x04D862D0 */ }
        System.Boolean DiscardUnknownFields { get; /* RVA: 0x04D92ED0 */ set; /* RVA: 0x04D92F60 */ }
        Google.Protobuf.ExtensionRegistry ExtensionRegistry { get; /* RVA: 0x04D86210 */ set; /* RVA: 0x05391334 */ }
        System.Byte[] InternalBuffer { get; /* RVA: 0x0385B100 */ }
        System.IO.Stream InternalInputStream { get; /* RVA: 0x04D862C0 */ }
        Google.Protobuf.ParserInternalState& InternalState { get; /* RVA: 0x04DA0CE0 */ }
        System.Boolean ReachedLimit { get; /* RVA: 0x09ACDDC4 */ }
        System.Boolean IsAtEnd { get; /* RVA: 0x09ACDD24 */ }

        // Methods
        // RVA: 0x09ACDC9C  token: 0x6000037
        public System.Void .ctor(System.Byte[] buffer) { }
        // RVA: 0x03143150  token: 0x6000038
        public System.Void .ctor(System.Byte[] buffer, System.Int32 offset, System.Int32 length) { }
        // RVA: 0x09ACDAC8  token: 0x6000039
        public System.Void .ctor(System.IO.Stream input) { }
        // RVA: 0x09ACDBF4  token: 0x600003A
        public System.Void .ctor(System.IO.Stream input, System.Boolean leaveOpen) { }
        // RVA: 0x031437F0  token: 0x600003B
        private System.Void .ctor(System.IO.Stream input, System.Byte[] buffer, System.Int32 bufferPos, System.Int32 bufferSize, System.Boolean leaveOpen) { }
        // RVA: 0x09ACDAD4  token: 0x600003C
        private System.Void .ctor(System.IO.Stream input, System.Byte[] buffer, System.Int32 bufferPos, System.Int32 bufferSize, System.Int32 sizeLimit, System.Int32 recursionLimit, System.Boolean leaveOpen) { }
        // RVA: 0x09ACCFB8  token: 0x600003D
        public static Google.Protobuf.CodedInputStream CreateWithLimits(System.IO.Stream input, System.Int32 sizeLimit, System.Int32 recursionLimit) { }
        // RVA: 0x09ACD078  token: 0x6000049
        public virtual System.Void Dispose() { }
        // RVA: 0x031437B0  token: 0x600004A
        private System.Void CheckReadEndOfStreamTag() { }
        // RVA: 0x09ACD118  token: 0x600004B
        public System.UInt32 PeekTag() { }
        // RVA: 0x09ACD8D8  token: 0x600004C
        public System.UInt32 ReadTag() { }
        // RVA: 0x09ACDA48  token: 0x600004D
        public System.Void SkipLastField() { }
        // RVA: 0x09ACD9BC  token: 0x600004E
        private System.Void SkipGroup(System.UInt32 startGroupTag) { }
        // RVA: 0x09ACD218  token: 0x600004F
        public System.Double ReadDouble() { }
        // RVA: 0x09ACD298  token: 0x6000050
        public System.Single ReadFloat() { }
        // RVA: 0x09ACD3FC  token: 0x6000051
        public System.UInt64 ReadUInt64() { }
        // RVA: 0x09ACD3FC  token: 0x6000052
        public System.Int64 ReadInt64() { }
        // RVA: 0x09ACD280  token: 0x6000053
        public System.Int32 ReadInt32() { }
        // RVA: 0x09ACD290  token: 0x6000054
        public System.UInt64 ReadFixed64() { }
        // RVA: 0x09ACD288  token: 0x6000055
        public System.UInt32 ReadFixed32() { }
        // RVA: 0x09ACD19C  token: 0x6000056
        public System.Boolean ReadBool() { }
        // RVA: 0x09ACD874  token: 0x6000057
        public System.String ReadString() { }
        // RVA: 0x09ACD46C  token: 0x6000058
        public System.Void ReadMessage(Google.Protobuf.IMessage builder) { }
        // RVA: 0x09ACD300  token: 0x6000059
        public System.Void ReadGroup(Google.Protobuf.IMessage builder) { }
        // RVA: 0x09ACD1B4  token: 0x600005A
        public Google.Protobuf.ByteString ReadBytes() { }
        // RVA: 0x09ACD280  token: 0x600005B
        public System.UInt32 ReadUInt32() { }
        // RVA: 0x09ACD280  token: 0x600005C
        public System.Int32 ReadEnum() { }
        // RVA: 0x09ACD288  token: 0x600005D
        public System.Int32 ReadSFixed32() { }
        // RVA: 0x09ACD290  token: 0x600005E
        public System.Int64 ReadSFixed64() { }
        // RVA: 0x09ACD838  token: 0x600005F
        public System.Int32 ReadSInt32() { }
        // RVA: 0x09ACD854  token: 0x6000060
        public System.Int64 ReadSInt64() { }
        // RVA: 0x09ACD404  token: 0x6000061
        public System.Int32 ReadLength() { }
        // RVA: 0x09ACD0A4  token: 0x6000062
        public System.Boolean MaybeConsumeTag(System.UInt32 tag) { }
        // RVA: 0x09ACD768  token: 0x6000063
        private System.UInt32 ReadRawVarint32() { }
        // RVA: 0x09ACD760  token: 0x6000064
        private static System.UInt32 ReadRawVarint32(System.IO.Stream input) { }
        // RVA: 0x09ACD7D0  token: 0x6000065
        private System.UInt64 ReadRawVarint64() { }
        // RVA: 0x09ACD690  token: 0x6000066
        private System.UInt32 ReadRawLittleEndian32() { }
        // RVA: 0x09ACD6F8  token: 0x6000067
        private System.UInt64 ReadRawLittleEndian64() { }
        // RVA: 0x09ACD190  token: 0x6000068
        private System.Int32 PushLimit(System.Int32 byteLimit) { }
        // RVA: 0x09ACD180  token: 0x6000069
        private System.Void PopLimit(System.Int32 oldLimit) { }
        // RVA: 0x09ACD940  token: 0x600006C
        private System.Boolean RefillBuffer(System.Boolean mustSucceed) { }
        // RVA: 0x09ACD61C  token: 0x600006D
        private System.Byte[] ReadRawBytes(System.Int32 size) { }
        // RVA: 0x03142380  token: 0x600006E
        public System.Void ReadRawMessage(Google.Protobuf.IMessage message) { }

    }

    // TypeToken: 0x200000B  // size: 0x40
    public sealed class CodedOutputStream : System.IDisposable
    {
        // Fields
        private static System.Int32 LittleEndian64Size;  // const
        private static System.Int32 LittleEndian32Size;  // const
        private static System.Int32 DoubleSize;  // const
        private static System.Int32 FloatSize;  // const
        private static System.Int32 BoolSize;  // const
        public static readonly System.Int32 DefaultBufferSize;  // static @ 0x0
        private readonly System.Boolean leaveOpen;  // 0x10
        private readonly System.Byte[] buffer;  // 0x18
        private Google.Protobuf.WriterInternalState state;  // 0x20
        private readonly System.IO.Stream output;  // 0x38

        // Properties
        System.Int64 Position { get; /* RVA: 0x09ACF430 */ }
        System.Int32 NonOutputPosition { get; /* RVA: 0x011F0020 */ }
        System.Int32 SpaceLeft { get; /* RVA: 0x09ACF464 */ }
        System.Byte[] InternalBuffer { get; /* RVA: 0x0385B100 */ }
        System.IO.Stream InternalOutputStream { get; /* RVA: 0x04D85A50 */ }
        Google.Protobuf.WriterInternalState& InternalState { get; /* RVA: 0x04D9E550 */ }

        // Methods
        // RVA: 0x04D8D060  token: 0x600006F
        public static System.Int32 ComputeDoubleSize(System.Double value) { }
        // RVA: 0x04D882B0  token: 0x6000070
        public static System.Int32 ComputeFloatSize(System.Single value) { }
        // RVA: 0x032943D0  token: 0x6000071
        public static System.Int32 ComputeUInt64Size(System.UInt64 value) { }
        // RVA: 0x032942E0  token: 0x6000072
        public static System.Int32 ComputeInt64Size(System.Int64 value) { }
        // RVA: 0x03296190  token: 0x6000073
        public static System.Int32 ComputeInt32Size(System.Int32 value) { }
        // RVA: 0x04D8D060  token: 0x6000074
        public static System.Int32 ComputeFixed64Size(System.UInt64 value) { }
        // RVA: 0x04D882B0  token: 0x6000075
        public static System.Int32 ComputeFixed32Size(System.UInt32 value) { }
        // RVA: 0x04D85B80  token: 0x6000076
        public static System.Int32 ComputeBoolSize(System.Boolean value) { }
        // RVA: 0x032947A0  token: 0x6000077
        public static System.Int32 ComputeStringSize(System.String value) { }
        // RVA: 0x09ACDDDC  token: 0x6000078
        public static System.Int32 ComputeGroupSize(Google.Protobuf.IMessage value) { }
        // RVA: 0x032960B0  token: 0x6000079
        public static System.Int32 ComputeMessageSize(Google.Protobuf.IMessage value) { }
        // RVA: 0x0435D040  token: 0x600007A
        public static System.Int32 ComputeBytesSize(Google.Protobuf.ByteString value) { }
        // RVA: 0x03295F90  token: 0x600007B
        public static System.Int32 ComputeUInt32Size(System.UInt32 value) { }
        // RVA: 0x03296010  token: 0x600007C
        public static System.Int32 ComputeEnumSize(System.Int32 value) { }
        // RVA: 0x04D882B0  token: 0x600007D
        public static System.Int32 ComputeSFixed32Size(System.Int32 value) { }
        // RVA: 0x04D8D060  token: 0x600007E
        public static System.Int32 ComputeSFixed64Size(System.Int64 value) { }
        // RVA: 0x09ACDE28  token: 0x600007F
        public static System.Int32 ComputeSInt32Size(System.Int32 value) { }
        // RVA: 0x09ACDE88  token: 0x6000080
        public static System.Int32 ComputeSInt64Size(System.Int64 value) { }
        // RVA: 0x03295F90  token: 0x6000081
        public static System.Int32 ComputeLengthSize(System.Int32 length) { }
        // RVA: 0x03296220  token: 0x6000082
        public static System.Int32 ComputeRawVarint32Size(System.UInt32 value) { }
        // RVA: 0x032944C0  token: 0x6000083
        public static System.Int32 ComputeRawVarint64Size(System.UInt64 value) { }
        // RVA: 0x09ACDEEC  token: 0x6000084
        public static System.Int32 ComputeTagSize(System.Int32 fieldNumber) { }
        // RVA: 0x09ACF204  token: 0x6000085
        public System.Void .ctor(System.Byte[] flatArray) { }
        // RVA: 0x031436C0  token: 0x6000086
        public System.Void .ctor(System.Byte[] buffer, System.Int32 offset, System.Int32 length) { }
        // RVA: 0x09ACF29C  token: 0x6000087
        private System.Void .ctor(System.IO.Stream output, System.Byte[] buffer, System.Boolean leaveOpen) { }
        // RVA: 0x09ACF3C8  token: 0x6000088
        public System.Void .ctor(System.IO.Stream output) { }
        // RVA: 0x09ACF230  token: 0x6000089
        public System.Void .ctor(System.IO.Stream output, System.Int32 bufferSize) { }
        // RVA: 0x09ACF18C  token: 0x600008A
        public System.Void .ctor(System.IO.Stream output, System.Boolean leaveOpen) { }
        // RVA: 0x09ACF350  token: 0x600008B
        public System.Void .ctor(System.IO.Stream output, System.Int32 bufferSize, System.Boolean leaveOpen) { }
        // RVA: 0x09ACE0D0  token: 0x600008E
        public System.Void WriteDouble(System.Double value) { }
        // RVA: 0x09ACE2D4  token: 0x600008F
        public System.Void WriteFloat(System.Single value) { }
        // RVA: 0x09ACF10C  token: 0x6000090
        public System.Void WriteUInt64(System.UInt64 value) { }
        // RVA: 0x09ACE4EC  token: 0x6000091
        public System.Void WriteInt64(System.Int64 value) { }
        // RVA: 0x09ACE46C  token: 0x6000092
        public System.Void WriteInt32(System.Int32 value) { }
        // RVA: 0x09ACE254  token: 0x6000093
        public System.Void WriteFixed64(System.UInt64 value) { }
        // RVA: 0x09ACE1D4  token: 0x6000094
        public System.Void WriteFixed32(System.UInt32 value) { }
        // RVA: 0x09ACDFD0  token: 0x6000095
        public System.Void WriteBool(System.Boolean value) { }
        // RVA: 0x09ACEEF8  token: 0x6000096
        public System.Void WriteString(System.String value) { }
        // RVA: 0x09ACE5EC  token: 0x6000097
        public System.Void WriteMessage(Google.Protobuf.IMessage value) { }
        // RVA: 0x03078F90  token: 0x6000098
        public System.Void WriteRawMessage(Google.Protobuf.IMessage value) { }
        // RVA: 0x09ACE358  token: 0x6000099
        public System.Void WriteGroup(Google.Protobuf.IMessage value) { }
        // RVA: 0x09ACE050  token: 0x600009A
        public System.Void WriteBytes(Google.Protobuf.ByteString value) { }
        // RVA: 0x09ACF08C  token: 0x600009B
        public System.Void WriteUInt32(System.UInt32 value) { }
        // RVA: 0x09ACE154  token: 0x600009C
        public System.Void WriteEnum(System.Int32 value) { }
        // RVA: 0x09ACECF8  token: 0x600009D
        public System.Void WriteSFixed32(System.Int32 value) { }
        // RVA: 0x09ACED78  token: 0x600009E
        public System.Void WriteSFixed64(System.Int64 value) { }
        // RVA: 0x09ACEDF8  token: 0x600009F
        public System.Void WriteSInt32(System.Int32 value) { }
        // RVA: 0x09ACEE78  token: 0x60000A0
        public System.Void WriteSInt64(System.Int64 value) { }
        // RVA: 0x09ACE56C  token: 0x60000A1
        public System.Void WriteLength(System.Int32 length) { }
        // RVA: 0x09ACEFF8  token: 0x60000A2
        public System.Void WriteTag(System.Int32 fieldNumber, Google.Protobuf.WireFormat.WireType type) { }
        // RVA: 0x09ACEF78  token: 0x60000A3
        public System.Void WriteTag(System.UInt32 tag) { }
        // RVA: 0x09ACEB78  token: 0x60000A4
        public System.Void WriteRawTag(System.Byte b1) { }
        // RVA: 0x09ACEAE4  token: 0x60000A5
        public System.Void WriteRawTag(System.Byte b1, System.Byte b2) { }
        // RVA: 0x09ACE98C  token: 0x60000A6
        public System.Void WriteRawTag(System.Byte b1, System.Byte b2, System.Byte b3) { }
        // RVA: 0x09ACEA34  token: 0x60000A7
        public System.Void WriteRawTag(System.Byte b1, System.Byte b2, System.Byte b3, System.Byte b4) { }
        // RVA: 0x09ACE8D0  token: 0x60000A8
        public System.Void WriteRawTag(System.Byte b1, System.Byte b2, System.Byte b3, System.Byte b4, System.Byte b5) { }
        // RVA: 0x09ACEBF8  token: 0x60000A9
        private System.Void WriteRawVarint32(System.UInt32 value) { }
        // RVA: 0x09ACEC78  token: 0x60000AA
        private System.Void WriteRawVarint64(System.UInt64 value) { }
        // RVA: 0x09ACE7D0  token: 0x60000AB
        private System.Void WriteRawLittleEndian32(System.UInt32 value) { }
        // RVA: 0x09ACE850  token: 0x60000AC
        private System.Void WriteRawLittleEndian64(System.UInt64 value) { }
        // RVA: 0x09ACE700  token: 0x60000AD
        private System.Void WriteRawBytes(System.Byte[] value) { }
        // RVA: 0x09ACE72C  token: 0x60000AE
        private System.Void WriteRawBytes(System.Byte[] value, System.Int32 offset, System.Int32 length) { }
        // RVA: 0x09ACDF3C  token: 0x60000AF
        public virtual System.Void Dispose() { }
        // RVA: 0x09ACDF78  token: 0x60000B0
        public System.Void Flush() { }
        // RVA: 0x09ACDDD0  token: 0x60000B1
        public System.Void CheckNoSpaceLeft() { }
        // RVA: 0x04D7AB50  token: 0x60000B6
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000D  // size: 0x18
    public abstract class Extension
    {
        // Fields
        private readonly System.Int32 <FieldNumber>k__BackingField;  // 0x10

        // Properties
        System.Type TargetType { get; /* RVA: -1  // abstract */ }
        System.Int32 FieldNumber { get; /* RVA: 0x02B2E2D0 */ }
        System.Boolean IsRepeated { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60000B9
        protected System.Void .ctor(System.Int32 fieldNumber) { }
        // RVA: -1  // abstract  token: 0x60000BA
        private virtual Google.Protobuf.IExtensionValue CreateValue() { }

    }

    // TypeToken: 0x200000E
    public sealed class Extension`2 : Google.Protobuf.Extension
    {
        // Fields
        private readonly Google.Protobuf.FieldCodec<TValue> codec;  // 0x0

        // Properties
        TValue DefaultValue { get; /* RVA: -1  // not resolved */ }
        System.Type TargetType { get; /* RVA: -1  // not resolved */ }
        System.Boolean IsRepeated { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60000BD
        public System.Void .ctor(System.Int32 fieldNumber, Google.Protobuf.FieldCodec<TValue> codec) { }
        // RVA: -1  // not resolved  token: 0x60000C1
        private virtual Google.Protobuf.IExtensionValue CreateValue() { }

    }

    // TypeToken: 0x200000F
    public sealed class RepeatedExtension`2 : Google.Protobuf.Extension
    {
        // Fields
        private readonly Google.Protobuf.FieldCodec<TValue> codec;  // 0x0

        // Properties
        System.Type TargetType { get; /* RVA: -1  // not resolved */ }
        System.Boolean IsRepeated { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60000C2
        public System.Void .ctor(System.Int32 fieldNumber, Google.Protobuf.FieldCodec<TValue> codec) { }
        // RVA: -1  // not resolved  token: 0x60000C5
        private virtual Google.Protobuf.IExtensionValue CreateValue() { }

    }

    // TypeToken: 0x2000010  // size: 0x18
    public sealed class ExtensionRegistry : System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, Google.Protobuf.IDeepCloneable`1
    {
        // Fields
        private System.Collections.Generic.IDictionary<Google.Protobuf.ObjectIntPair<System.Type>,Google.Protobuf.Extension> extensions;  // 0x10

        // Properties
        System.Int32 Count { get; /* RVA: 0x09ACFFDC */ }
        System.Boolean System.Collections.Generic.ICollection<Google.Protobuf.Extension>.IsReadOnly { get; /* RVA: 0x012081B0 */ }

        // Methods
        // RVA: 0x09ACFD8C  token: 0x60000C6
        public System.Void .ctor() { }
        // RVA: 0x09ACFE04  token: 0x60000C7
        private System.Void .ctor(System.Collections.Generic.IDictionary<Google.Protobuf.ObjectIntPair<System.Type>,Google.Protobuf.Extension> collection) { }
        // RVA: 0x09ACF900  token: 0x60000CA
        private System.Boolean ContainsInputField(System.UInt32 lastTag, System.Type target, Google.Protobuf.Extension& extension) { }
        // RVA: 0x09ACF784  token: 0x60000CB
        public virtual System.Void Add(Google.Protobuf.Extension extension) { }
        // RVA: 0x09ACF630  token: 0x60000CC
        public System.Void AddRange(System.Collections.Generic.IEnumerable<Google.Protobuf.Extension> extensions) { }
        // RVA: 0x09ACF850  token: 0x60000CD
        public virtual System.Void Clear() { }
        // RVA: 0x09ACF9A4  token: 0x60000CE
        public virtual System.Boolean Contains(Google.Protobuf.Extension item) { }
        // RVA: 0x09ACFBA8  token: 0x60000CF
        private virtual System.Void System.Collections.Generic.ICollection<Google.Protobuf.Extension>.CopyTo(Google.Protobuf.Extension[] array, System.Int32 arrayIndex) { }
        // RVA: 0x09ACFA70  token: 0x60000D0
        public virtual System.Collections.Generic.IEnumerator<Google.Protobuf.Extension> GetEnumerator() { }
        // RVA: 0x09ACFADC  token: 0x60000D1
        public virtual System.Boolean Remove(Google.Protobuf.Extension item) { }
        // RVA: 0x09ACFD84  token: 0x60000D2
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: 0x09ACF89C  token: 0x60000D3
        public virtual Google.Protobuf.ExtensionRegistry Clone() { }

    }

    // TypeToken: 0x2000013  // size: 0x10
    public static class ExtensionSet
    {
        // Methods
        // RVA: -1  // generic def  token: 0x60000DC
        private static System.Boolean TryGetValue(Google.Protobuf.ExtensionSet<TTarget>& set, Google.Protobuf.Extension extension, Google.Protobuf.IExtensionValue& value) { }
        // RVA: -1  // generic def  token: 0x60000DD
        public static TValue Get(Google.Protobuf.ExtensionSet<TTarget>& set, Google.Protobuf.Extension<TTarget,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x60000DE
        public static Google.Protobuf.Collections.RepeatedField<TValue> Get(Google.Protobuf.ExtensionSet<TTarget>& set, Google.Protobuf.RepeatedExtension<TTarget,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x60000DF
        public static Google.Protobuf.Collections.RepeatedField<TValue> GetOrInitialize(Google.Protobuf.ExtensionSet<TTarget>& set, Google.Protobuf.RepeatedExtension<TTarget,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x60000E0
        public static System.Void Set(Google.Protobuf.ExtensionSet<TTarget>& set, Google.Protobuf.Extension<TTarget,TValue> extension, TValue value) { }
        // RVA: -1  // generic def  token: 0x60000E1
        public static System.Boolean Has(Google.Protobuf.ExtensionSet<TTarget>& set, Google.Protobuf.Extension<TTarget,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x60000E2
        public static System.Void Clear(Google.Protobuf.ExtensionSet<TTarget>& set, Google.Protobuf.Extension<TTarget,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x60000E3
        public static System.Void Clear(Google.Protobuf.ExtensionSet<TTarget>& set, Google.Protobuf.RepeatedExtension<TTarget,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x60000E4
        public static System.Boolean TryMergeFieldFrom(Google.Protobuf.ExtensionSet<TTarget>& set, Google.Protobuf.CodedInputStream stream) { }
        // RVA: -1  // generic def  token: 0x60000E5
        public static System.Boolean TryMergeFieldFrom(Google.Protobuf.ExtensionSet<TTarget>& set, Google.Protobuf.ParseContext& ctx) { }
        // RVA: -1  // generic def  token: 0x60000E6
        public static System.Void MergeFrom(Google.Protobuf.ExtensionSet<TTarget>& first, Google.Protobuf.ExtensionSet<TTarget> second) { }
        // RVA: -1  // generic def  token: 0x60000E7
        public static Google.Protobuf.ExtensionSet<TTarget> Clone(Google.Protobuf.ExtensionSet<TTarget> set) { }

    }

    // TypeToken: 0x2000014
    public sealed class ExtensionSet`1
    {
        // Fields
        private readonly System.Collections.Generic.Dictionary<System.Int32,Google.Protobuf.IExtensionValue> <ValuesByNumber>k__BackingField;  // 0x0

        // Properties
        System.Collections.Generic.Dictionary<System.Int32,Google.Protobuf.IExtensionValue> ValuesByNumber { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60000E9
        public virtual System.Int32 GetHashCode() { }
        // RVA: -1  // not resolved  token: 0x60000EA
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: -1  // not resolved  token: 0x60000EB
        public System.Int32 CalculateSize() { }
        // RVA: -1  // not resolved  token: 0x60000EC
        public System.Void WriteTo(Google.Protobuf.CodedOutputStream stream) { }
        // RVA: -1  // not resolved  token: 0x60000ED
        public System.Void WriteTo(Google.Protobuf.WriteContext& ctx) { }
        // RVA: -1  // not resolved  token: 0x60000EE
        private System.Boolean IsInitialized() { }
        // RVA: -1  // not resolved  token: 0x60000EF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000016
    public interface IExtensionValue : System.IEquatable`1, Google.Protobuf.IDeepCloneable`1
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60000F3
        public virtual System.Void MergeFrom(Google.Protobuf.ParseContext& ctx) { }
        // RVA: -1  // abstract  token: 0x60000F4
        public virtual System.Void MergeFrom(Google.Protobuf.IExtensionValue value) { }
        // RVA: -1  // abstract  token: 0x60000F5
        public virtual System.Void WriteTo(Google.Protobuf.WriteContext& ctx) { }
        // RVA: -1  // abstract  token: 0x60000F6
        public virtual System.Int32 CalculateSize() { }
        // RVA: -1  // abstract  token: 0x60000F7
        public virtual System.Boolean IsInitialized() { }
        // RVA: -1  // abstract  token: 0x60000F8
        public virtual System.Object GetValue() { }

    }

    // TypeToken: 0x2000017
    public sealed class ExtensionValue`1 : Google.Protobuf.IExtensionValue, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1
    {
        // Fields
        private T field;  // 0x0
        private Google.Protobuf.FieldCodec<T> codec;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60000F9
        private System.Void .ctor(Google.Protobuf.FieldCodec<T> codec) { }
        // RVA: -1  // not resolved  token: 0x60000FA
        public virtual System.Int32 CalculateSize() { }
        // RVA: -1  // not resolved  token: 0x60000FB
        public virtual Google.Protobuf.IExtensionValue Clone() { }
        // RVA: -1  // not resolved  token: 0x60000FC
        public virtual System.Boolean Equals(Google.Protobuf.IExtensionValue other) { }
        // RVA: -1  // not resolved  token: 0x60000FD
        public virtual System.Int32 GetHashCode() { }
        // RVA: -1  // not resolved  token: 0x60000FE
        public virtual System.Void MergeFrom(Google.Protobuf.ParseContext& ctx) { }
        // RVA: -1  // not resolved  token: 0x60000FF
        public virtual System.Void MergeFrom(Google.Protobuf.IExtensionValue value) { }
        // RVA: -1  // not resolved  token: 0x6000100
        public virtual System.Void WriteTo(Google.Protobuf.WriteContext& ctx) { }
        // RVA: -1  // not resolved  token: 0x6000101
        public T GetValue() { }
        // RVA: -1  // not resolved  token: 0x6000102
        private virtual System.Object Google.Protobuf.IExtensionValue.GetValue() { }
        // RVA: -1  // not resolved  token: 0x6000103
        public System.Void SetValue(T value) { }
        // RVA: -1  // not resolved  token: 0x6000104
        public virtual System.Boolean IsInitialized() { }

    }

    // TypeToken: 0x2000018
    public sealed class RepeatedExtensionValue`1 : Google.Protobuf.IExtensionValue, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1
    {
        // Fields
        private Google.Protobuf.Collections.RepeatedField<T> field;  // 0x0
        private readonly Google.Protobuf.FieldCodec<T> codec;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000105
        private System.Void .ctor(Google.Protobuf.FieldCodec<T> codec) { }
        // RVA: -1  // not resolved  token: 0x6000106
        public virtual System.Int32 CalculateSize() { }
        // RVA: -1  // not resolved  token: 0x6000107
        public virtual Google.Protobuf.IExtensionValue Clone() { }
        // RVA: -1  // not resolved  token: 0x6000108
        public virtual System.Boolean Equals(Google.Protobuf.IExtensionValue other) { }
        // RVA: -1  // not resolved  token: 0x6000109
        public virtual System.Int32 GetHashCode() { }
        // RVA: -1  // not resolved  token: 0x600010A
        public virtual System.Void MergeFrom(Google.Protobuf.ParseContext& ctx) { }
        // RVA: -1  // not resolved  token: 0x600010B
        public virtual System.Void MergeFrom(Google.Protobuf.IExtensionValue value) { }
        // RVA: -1  // not resolved  token: 0x600010C
        public virtual System.Void WriteTo(Google.Protobuf.WriteContext& ctx) { }
        // RVA: -1  // not resolved  token: 0x600010D
        public Google.Protobuf.Collections.RepeatedField<T> GetValue() { }
        // RVA: -1  // not resolved  token: 0x600010E
        private virtual System.Object Google.Protobuf.IExtensionValue.GetValue() { }
        // RVA: -1  // not resolved  token: 0x600010F
        public virtual System.Boolean IsInitialized() { }

    }

    // TypeToken: 0x2000019  // size: 0x10
    public static class FieldCodec
    {
        // Methods
        // RVA: 0x04109440  token: 0x6000110
        public static Google.Protobuf.FieldCodec<System.String> ForString(System.UInt32 tag) { }
        // RVA: 0x09AD0024  token: 0x6000111
        public static Google.Protobuf.FieldCodec<Google.Protobuf.ByteString> ForBytes(System.UInt32 tag) { }
        // RVA: 0x05396FD4  token: 0x6000112
        public static Google.Protobuf.FieldCodec<System.Boolean> ForBool(System.UInt32 tag) { }
        // RVA: 0x05396FBC  token: 0x6000113
        public static Google.Protobuf.FieldCodec<System.Int32> ForInt32(System.UInt32 tag) { }
        // RVA: 0x09AD0B1C  token: 0x6000114
        public static Google.Protobuf.FieldCodec<System.Int32> ForSInt32(System.UInt32 tag) { }
        // RVA: 0x09AD0278  token: 0x6000115
        public static Google.Protobuf.FieldCodec<System.UInt32> ForFixed32(System.UInt32 tag) { }
        // RVA: 0x09AD06AC  token: 0x6000116
        public static Google.Protobuf.FieldCodec<System.Int32> ForSFixed32(System.UInt32 tag) { }
        // RVA: 0x05396FB0  token: 0x6000117
        public static Google.Protobuf.FieldCodec<System.UInt32> ForUInt32(System.UInt32 tag) { }
        // RVA: 0x05396FE0  token: 0x6000118
        public static Google.Protobuf.FieldCodec<System.Int64> ForInt64(System.UInt32 tag) { }
        // RVA: 0x09AD0D78  token: 0x6000119
        public static Google.Protobuf.FieldCodec<System.Int64> ForSInt64(System.UInt32 tag) { }
        // RVA: 0x09AD0284  token: 0x600011A
        public static Google.Protobuf.FieldCodec<System.UInt64> ForFixed64(System.UInt32 tag) { }
        // RVA: 0x09AD08D0  token: 0x600011B
        public static Google.Protobuf.FieldCodec<System.Int64> ForSFixed64(System.UInt32 tag) { }
        // RVA: 0x0539778C  token: 0x600011C
        public static Google.Protobuf.FieldCodec<System.UInt64> ForUInt64(System.UInt32 tag) { }
        // RVA: 0x05396FC8  token: 0x600011D
        public static Google.Protobuf.FieldCodec<System.Single> ForFloat(System.UInt32 tag) { }
        // RVA: 0x05398044  token: 0x600011E
        public static Google.Protobuf.FieldCodec<System.Double> ForDouble(System.UInt32 tag) { }
        // RVA: -1  // generic def  token: 0x600011F
        public static Google.Protobuf.FieldCodec<T> ForEnum(System.UInt32 tag, System.Func<T,System.Int32> toInt32, System.Func<System.Int32,T> fromInt32) { }
        // RVA: 0x04109480  token: 0x6000120
        public static Google.Protobuf.FieldCodec<System.String> ForString(System.UInt32 tag, System.String defaultValue) { }
        // RVA: 0x03218910  token: 0x6000121
        public static Google.Protobuf.FieldCodec<Google.Protobuf.ByteString> ForBytes(System.UInt32 tag, Google.Protobuf.ByteString defaultValue) { }
        // RVA: 0x0410C790  token: 0x6000122
        public static Google.Protobuf.FieldCodec<System.Boolean> ForBool(System.UInt32 tag, System.Boolean defaultValue) { }
        // RVA: 0x0410B330  token: 0x6000123
        public static Google.Protobuf.FieldCodec<System.Int32> ForInt32(System.UInt32 tag, System.Int32 defaultValue) { }
        // RVA: 0x09AD08DC  token: 0x6000124
        public static Google.Protobuf.FieldCodec<System.Int32> ForSInt32(System.UInt32 tag, System.Int32 defaultValue) { }
        // RVA: 0x09AD0074  token: 0x6000125
        public static Google.Protobuf.FieldCodec<System.UInt32> ForFixed32(System.UInt32 tag, System.UInt32 defaultValue) { }
        // RVA: 0x09AD04A8  token: 0x6000126
        public static Google.Protobuf.FieldCodec<System.Int32> ForSFixed32(System.UInt32 tag, System.Int32 defaultValue) { }
        // RVA: 0x04110D00  token: 0x6000127
        public static Google.Protobuf.FieldCodec<System.UInt32> ForUInt32(System.UInt32 tag, System.UInt32 defaultValue) { }
        // RVA: 0x0410D000  token: 0x6000128
        public static Google.Protobuf.FieldCodec<System.Int64> ForInt64(System.UInt32 tag, System.Int64 defaultValue) { }
        // RVA: 0x09AD0B28  token: 0x6000129
        public static Google.Protobuf.FieldCodec<System.Int64> ForSInt64(System.UInt32 tag, System.Int64 defaultValue) { }
        // RVA: 0x09AD0290  token: 0x600012A
        public static Google.Protobuf.FieldCodec<System.UInt64> ForFixed64(System.UInt32 tag, System.UInt64 defaultValue) { }
        // RVA: 0x09AD06B8  token: 0x600012B
        public static Google.Protobuf.FieldCodec<System.Int64> ForSFixed64(System.UInt32 tag, System.Int64 defaultValue) { }
        // RVA: 0x044A0EC0  token: 0x600012C
        public static Google.Protobuf.FieldCodec<System.UInt64> ForUInt64(System.UInt32 tag, System.UInt64 defaultValue) { }
        // RVA: 0x049E43F0  token: 0x600012D
        public static Google.Protobuf.FieldCodec<System.Single> ForFloat(System.UInt32 tag, System.Single defaultValue) { }
        // RVA: 0x049E4630  token: 0x600012E
        public static Google.Protobuf.FieldCodec<System.Double> ForDouble(System.UInt32 tag, System.Double defaultValue) { }
        // RVA: -1  // generic def  token: 0x600012F
        public static Google.Protobuf.FieldCodec<T> ForEnum(System.UInt32 tag, System.Func<T,System.Int32> toInt32, System.Func<System.Int32,T> fromInt32, T defaultValue) { }
        // RVA: -1  // generic def  token: 0x6000130
        public static Google.Protobuf.FieldCodec<T> ForMessage(System.UInt32 tag, Google.Protobuf.MessageParser<T> parser) { }
        // RVA: -1  // generic def  token: 0x6000131
        public static Google.Protobuf.FieldCodec<T> ForGroup(System.UInt32 startTag, System.UInt32 endTag, Google.Protobuf.MessageParser<T> parser) { }
        // RVA: -1  // generic def  token: 0x6000132
        public static Google.Protobuf.FieldCodec<T> ForClassWrapper(System.UInt32 tag) { }
        // RVA: -1  // generic def  token: 0x6000133
        public static Google.Protobuf.FieldCodec<System.Nullable<T>> ForStructWrapper(System.UInt32 tag) { }

    }

    // TypeToken: 0x2000026
    public sealed class ValueReader`1 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x600017F
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000180
        public virtual TValue Invoke(Google.Protobuf.ParseContext& ctx) { }
        // RVA: -1  // runtime  token: 0x6000181
        public virtual System.IAsyncResult BeginInvoke(Google.Protobuf.ParseContext& ctx, System.AsyncCallback callback, System.Object object) { }
        // RVA: -1  // runtime  token: 0x6000182
        public virtual TValue EndInvoke(Google.Protobuf.ParseContext& ctx, System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000027
    public sealed class ValueWriter`1 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000183
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000184
        public virtual System.Void Invoke(Google.Protobuf.WriteContext& ctx, T value) { }
        // RVA: -1  // runtime  token: 0x6000185
        public virtual System.IAsyncResult BeginInvoke(Google.Protobuf.WriteContext& ctx, T value, System.AsyncCallback callback, System.Object object) { }
        // RVA: -1  // runtime  token: 0x6000186
        public virtual System.Void EndInvoke(Google.Protobuf.WriteContext& ctx, System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000028
    public sealed class FieldCodec`1
    {
        // Fields
        private static readonly System.Collections.Generic.EqualityComparer<T> EqualityComparer;  // static @ 0x0
        private static readonly T DefaultDefault;  // static @ 0x0
        private static readonly System.Boolean TypeSupportsPacking;  // static @ 0x0
        private readonly System.Boolean <PackedRepeatedField>k__BackingField;  // 0x0
        private readonly Google.Protobuf.ValueWriter<T> <ValueWriter>k__BackingField;  // 0x0
        private readonly System.Func<T,System.Int32> <ValueSizeCalculator>k__BackingField;  // 0x0
        private readonly Google.Protobuf.ValueReader<T> <ValueReader>k__BackingField;  // 0x0
        private readonly Google.Protobuf.FieldCodec.InputMerger<T> <ValueMerger>k__BackingField;  // 0x0
        private readonly Google.Protobuf.FieldCodec.ValuesMerger<T> <FieldMerger>k__BackingField;  // 0x0
        private readonly System.Int32 <FixedSize>k__BackingField;  // 0x0
        private readonly System.UInt32 <Tag>k__BackingField;  // 0x0
        private readonly System.UInt32 <EndTag>k__BackingField;  // 0x0
        private readonly T <DefaultValue>k__BackingField;  // 0x0
        private readonly System.Int32 tagSize;  // 0x0

        // Properties
        System.Boolean PackedRepeatedField { get; /* RVA: -1  // not resolved */ }
        Google.Protobuf.ValueWriter<T> ValueWriter { get; /* RVA: -1  // not resolved */ }
        System.Func<T,System.Int32> ValueSizeCalculator { get; /* RVA: -1  // not resolved */ }
        Google.Protobuf.ValueReader<T> ValueReader { get; /* RVA: -1  // not resolved */ }
        Google.Protobuf.FieldCodec.InputMerger<T> ValueMerger { get; /* RVA: -1  // not resolved */ }
        Google.Protobuf.FieldCodec.ValuesMerger<T> FieldMerger { get; /* RVA: -1  // not resolved */ }
        System.Int32 FixedSize { get; /* RVA: -1  // not resolved */ }
        System.UInt32 Tag { get; /* RVA: -1  // not resolved */ }
        System.UInt32 EndTag { get; /* RVA: -1  // not resolved */ }
        T DefaultValue { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000187
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x6000188
        private static System.Boolean IsPackedRepeatedField(System.UInt32 tag) { }
        // RVA: -1  // not resolved  token: 0x6000193
        private System.Void .ctor(Google.Protobuf.ValueReader<T> reader, Google.Protobuf.ValueWriter<T> writer, System.Int32 fixedSize, System.UInt32 tag, T defaultValue) { }
        // RVA: -1  // not resolved  token: 0x6000194
        private System.Void .ctor(Google.Protobuf.ValueReader<T> reader, Google.Protobuf.ValueWriter<T> writer, System.Func<T,System.Int32> sizeCalculator, System.UInt32 tag, T defaultValue) { }
        // RVA: -1  // not resolved  token: 0x6000195
        private System.Void .ctor(Google.Protobuf.ValueReader<T> reader, Google.Protobuf.ValueWriter<T> writer, Google.Protobuf.FieldCodec.InputMerger<T> inputMerger, Google.Protobuf.FieldCodec.ValuesMerger<T> valuesMerger, System.Func<T,System.Int32> sizeCalculator, System.UInt32 tag, System.UInt32 endTag) { }
        // RVA: -1  // not resolved  token: 0x6000196
        private System.Void .ctor(Google.Protobuf.ValueReader<T> reader, Google.Protobuf.ValueWriter<T> writer, Google.Protobuf.FieldCodec.InputMerger<T> inputMerger, Google.Protobuf.FieldCodec.ValuesMerger<T> valuesMerger, System.Func<T,System.Int32> sizeCalculator, System.UInt32 tag, System.UInt32 endTag, T defaultValue) { }
        // RVA: -1  // not resolved  token: 0x6000197
        public System.Void WriteTagAndValue(Google.Protobuf.CodedOutputStream output, T value) { }
        // RVA: -1  // not resolved  token: 0x6000198
        public System.Void WriteTagAndValue(Google.Protobuf.WriteContext& ctx, T value) { }
        // RVA: -1  // not resolved  token: 0x6000199
        public T Read(Google.Protobuf.CodedInputStream input) { }
        // RVA: -1  // not resolved  token: 0x600019A
        public T Read(Google.Protobuf.ParseContext& ctx) { }
        // RVA: -1  // not resolved  token: 0x600019B
        public System.Int32 CalculateSizeWithTag(T value) { }
        // RVA: -1  // not resolved  token: 0x600019C
        private System.Int32 CalculateUnconditionalSizeWithTag(T value) { }
        // RVA: -1  // not resolved  token: 0x600019D
        private System.Boolean IsDefault(T value) { }

    }

    // TypeToken: 0x200002E  // size: 0x18
    public sealed class FieldMaskTree
    {
        // Fields
        private static System.Char FIELD_PATH_SEPARATOR;  // const
        private readonly Google.Protobuf.FieldMaskTree.Node root;  // 0x10

        // Methods
        // RVA: 0x09AD20E0  token: 0x60001AD
        public System.Void .ctor() { }
        // RVA: 0x09AD2144  token: 0x60001AE
        public System.Void .ctor(Google.Protobuf.WellKnownTypes.FieldMask mask) { }
        // RVA: 0x09AD20B8  token: 0x60001AF
        public virtual System.String ToString() { }
        // RVA: 0x09AD0D84  token: 0x60001B0
        public Google.Protobuf.FieldMaskTree AddFieldPath(System.String path) { }
        // RVA: 0x09AD1454  token: 0x60001B1
        public Google.Protobuf.FieldMaskTree MergeFromFieldMask(Google.Protobuf.WellKnownTypes.FieldMask mask) { }
        // RVA: 0x09AD1FA0  token: 0x60001B2
        public Google.Protobuf.WellKnownTypes.FieldMask ToFieldMask() { }
        // RVA: 0x09AD0F30  token: 0x60001B3
        private System.Void GetFieldPaths(Google.Protobuf.FieldMaskTree.Node node, System.String path, System.Collections.Generic.List<System.String> paths) { }
        // RVA: 0x09AD114C  token: 0x60001B4
        public System.Void IntersectFieldPath(System.String path, Google.Protobuf.FieldMaskTree output) { }
        // RVA: 0x09AD15A0  token: 0x60001B5
        public System.Void Merge(Google.Protobuf.IMessage source, Google.Protobuf.IMessage destination, Google.Protobuf.WellKnownTypes.FieldMask.MergeOptions options) { }
        // RVA: 0x09AD16F4  token: 0x60001B6
        private System.Void Merge(Google.Protobuf.FieldMaskTree.Node node, System.String path, Google.Protobuf.IMessage source, Google.Protobuf.IMessage destination, Google.Protobuf.WellKnownTypes.FieldMask.MergeOptions options) { }

    }

    // TypeToken: 0x2000030  // size: 0x10
    public static class FrameworkPortability
    {
        // Fields
        private static readonly System.Text.RegularExpressions.RegexOptions CompiledRegexWhereAvailable;  // static @ 0x0

        // Methods
        // RVA: 0x09AD21C4  token: 0x60001B9
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000031
    public interface IBufferMessage : Google.Protobuf.IMessage
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60001BA
        public virtual System.Void InternalMergeFrom(Google.Protobuf.ParseContext& ctx) { }
        // RVA: -1  // abstract  token: 0x60001BB
        public virtual System.Void InternalWriteTo(Google.Protobuf.WriteContext& ctx) { }

    }

    // TypeToken: 0x2000032
    public interface ICustomDiagnosticMessage : Google.Protobuf.IMessage
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60001BC
        public virtual System.String ToDiagnosticString() { }

    }

    // TypeToken: 0x2000033
    public interface IDeepCloneable`1
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60001BD
        public virtual T Clone() { }

    }

    // TypeToken: 0x2000034
    public interface IExtendableMessage`1 : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60001BE
        public virtual TValue GetExtension(Google.Protobuf.Extension<T,TValue> extension) { }
        // RVA: -1  // abstract  token: 0x60001BF
        public virtual Google.Protobuf.Collections.RepeatedField<TValue> GetExtension(Google.Protobuf.RepeatedExtension<T,TValue> extension) { }
        // RVA: -1  // abstract  token: 0x60001C0
        public virtual Google.Protobuf.Collections.RepeatedField<TValue> GetOrInitializeExtension(Google.Protobuf.RepeatedExtension<T,TValue> extension) { }
        // RVA: -1  // abstract  token: 0x60001C1
        public virtual System.Void SetExtension(Google.Protobuf.Extension<T,TValue> extension, TValue value) { }
        // RVA: -1  // abstract  token: 0x60001C2
        public virtual System.Boolean HasExtension(Google.Protobuf.Extension<T,TValue> extension) { }
        // RVA: -1  // abstract  token: 0x60001C3
        public virtual System.Void ClearExtension(Google.Protobuf.Extension<T,TValue> extension) { }
        // RVA: -1  // abstract  token: 0x60001C4
        public virtual System.Void ClearExtension(Google.Protobuf.RepeatedExtension<T,TValue> extension) { }

    }

    // TypeToken: 0x2000035
    public interface IMessage
    {
        // Properties
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x60001C5
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: -1  // abstract  token: 0x60001C6
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: -1  // abstract  token: 0x60001C7
        public virtual System.Int32 CalculateSize() { }

    }

    // TypeToken: 0x2000036
    public interface IMessage`1 : Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60001C9
        public virtual System.Void MergeFrom(T message) { }

    }

    // TypeToken: 0x2000037  // size: 0x90
    public sealed class InvalidJsonException : System.IO.IOException
    {
        // Methods
        // RVA: 0x09AD2294  token: 0x60001CA
        private System.Void .ctor(System.String message) { }

    }

    // TypeToken: 0x2000038  // size: 0x90
    public sealed class InvalidProtocolBufferException : System.IO.IOException
    {
        // Methods
        // RVA: 0x09AD2294  token: 0x60001CB
        private System.Void .ctor(System.String message) { }
        // RVA: 0x09AD27D8  token: 0x60001CC
        private System.Void .ctor(System.String message, System.Exception innerException) { }
        // RVA: 0x09AD25E4  token: 0x60001CD
        private static Google.Protobuf.InvalidProtocolBufferException MoreDataAvailable() { }
        // RVA: 0x09AD2774  token: 0x60001CE
        private static Google.Protobuf.InvalidProtocolBufferException TruncatedMessage() { }
        // RVA: 0x09AD2648  token: 0x60001CF
        private static Google.Protobuf.InvalidProtocolBufferException NegativeSize() { }
        // RVA: 0x09AD251C  token: 0x60001D0
        private static Google.Protobuf.InvalidProtocolBufferException MalformedVarint() { }
        // RVA: 0x09AD23F0  token: 0x60001D1
        private static Google.Protobuf.InvalidProtocolBufferException InvalidTag() { }
        // RVA: 0x09AD2454  token: 0x60001D2
        private static Google.Protobuf.InvalidProtocolBufferException InvalidWireType() { }
        // RVA: 0x09AD22B4  token: 0x60001D3
        private static Google.Protobuf.InvalidProtocolBufferException InvalidBase64(System.Exception innerException) { }
        // RVA: 0x09AD2328  token: 0x60001D4
        private static Google.Protobuf.InvalidProtocolBufferException InvalidEndTag() { }
        // RVA: 0x09AD26AC  token: 0x60001D5
        private static Google.Protobuf.InvalidProtocolBufferException RecursionLimitExceeded() { }
        // RVA: 0x09AD24B8  token: 0x60001D6
        private static Google.Protobuf.InvalidProtocolBufferException JsonRecursionLimitExceeded() { }
        // RVA: 0x09AD2710  token: 0x60001D7
        private static Google.Protobuf.InvalidProtocolBufferException SizeLimitExceeded() { }
        // RVA: 0x09AD238C  token: 0x60001D8
        private static Google.Protobuf.InvalidProtocolBufferException InvalidMessageStreamTag() { }
        // RVA: 0x09AD2580  token: 0x60001D9
        private static Google.Protobuf.InvalidProtocolBufferException MissingFields() { }

    }

    // TypeToken: 0x2000039  // size: 0x18
    public sealed class JsonFormatter
    {
        // Fields
        private static System.String AnyTypeUrlField;  // const
        private static System.String AnyDiagnosticValueField;  // const
        private static System.String AnyWellKnownTypeValueField;  // const
        private static System.String TypeUrlPrefix;  // const
        private static System.String NameValueSeparator;  // const
        private static System.String PropertySeparator;  // const
        private static readonly Google.Protobuf.JsonFormatter <Default>k__BackingField;  // static @ 0x0
        private static readonly Google.Protobuf.JsonFormatter diagnosticFormatter;  // static @ 0x8
        private static readonly System.String[] CommonRepresentations;  // static @ 0x10
        private readonly Google.Protobuf.JsonFormatter.Settings settings;  // 0x10
        private static System.String Hex;  // const

        // Properties
        Google.Protobuf.JsonFormatter Default { get; /* RVA: 0x09AD79B4 */ }
        System.Boolean DiagnosticOnly { get; /* RVA: 0x09AD7A04 */ }

        // Methods
        // RVA: 0x09AD67A4  token: 0x60001DB
        private static System.Void .cctor() { }
        // RVA: 0x09AD794C  token: 0x60001DD
        public System.Void .ctor(Google.Protobuf.JsonFormatter.Settings settings) { }
        // RVA: 0x09AD27E0  token: 0x60001DE
        public System.String Format(Google.Protobuf.IMessage message) { }
        // RVA: 0x09AD2864  token: 0x60001DF
        public System.Void Format(Google.Protobuf.IMessage message, System.IO.TextWriter writer) { }
        // RVA: 0x09AD2F8C  token: 0x60001E0
        public static System.String ToDiagnosticString(Google.Protobuf.IMessage message) { }
        // RVA: 0x09AD4FD4  token: 0x60001E1
        private System.Void WriteMessage(System.IO.TextWriter writer, Google.Protobuf.IMessage message) { }
        // RVA: 0x09AD4CB0  token: 0x60001E2
        private System.Boolean WriteMessageFields(System.IO.TextWriter writer, Google.Protobuf.IMessage message, System.Boolean assumeFirstFieldWritten) { }
        // RVA: 0x09AD2EE0  token: 0x60001E3
        private System.Boolean ShouldFormatFieldValue(Google.Protobuf.IMessage message, Google.Protobuf.Reflection.FieldDescriptor field, System.Object value) { }
        // RVA: 0x09AD3018  token: 0x60001E4
        private static System.String ToJsonName(System.String name) { }
        // RVA: 0x09AD2974  token: 0x60001E5
        private static System.String FromJsonName(System.String name) { }
        // RVA: 0x09AD5140  token: 0x60001E6
        private static System.Void WriteNull(System.IO.TextWriter writer) { }
        // RVA: 0x09AD2B94  token: 0x60001E7
        private static System.Boolean IsDefaultValue(Google.Protobuf.Reflection.FieldDescriptor descriptor, System.Object value) { }
        // RVA: 0x09AD5CEC  token: 0x60001E8
        public System.Void WriteValue(System.IO.TextWriter writer, System.Object value) { }
        // RVA: 0x09AD6334  token: 0x60001E9
        private System.Void WriteWellKnownTypeValue(System.IO.TextWriter writer, Google.Protobuf.Reflection.MessageDescriptor descriptor, System.Object value) { }
        // RVA: 0x09AD5B60  token: 0x60001EA
        private System.Void WriteTimestamp(System.IO.TextWriter writer, Google.Protobuf.IMessage value) { }
        // RVA: 0x09AD47D4  token: 0x60001EB
        private System.Void WriteDuration(System.IO.TextWriter writer, Google.Protobuf.IMessage value) { }
        // RVA: 0x09AD4960  token: 0x60001EC
        private System.Void WriteFieldMask(System.IO.TextWriter writer, Google.Protobuf.IMessage value) { }
        // RVA: 0x09AD3124  token: 0x60001ED
        private System.Void WriteAny(System.IO.TextWriter writer, Google.Protobuf.IMessage value) { }
        // RVA: 0x09AD34E0  token: 0x60001EE
        private System.Void WriteDiagnosticOnlyAny(System.IO.TextWriter writer, Google.Protobuf.IMessage value) { }
        // RVA: 0x09AD5730  token: 0x60001EF
        private System.Void WriteStruct(System.IO.TextWriter writer, Google.Protobuf.IMessage message) { }
        // RVA: 0x09AD54B0  token: 0x60001F0
        private System.Void WriteStructFieldValue(System.IO.TextWriter writer, Google.Protobuf.IMessage message) { }
        // RVA: 0x09AD4A6C  token: 0x60001F1
        private System.Void WriteList(System.IO.TextWriter writer, System.Collections.IList list) { }
        // RVA: 0x09AD376C  token: 0x60001F2
        private System.Void WriteDictionary(System.IO.TextWriter writer, System.Collections.IDictionary dictionary) { }
        // RVA: 0x09AD518C  token: 0x60001F3
        private static System.Void WriteString(System.IO.TextWriter writer, System.String text) { }
        // RVA: 0x09AD2A78  token: 0x60001F4
        private static System.Void HexEncodeUtf16CodeUnit(System.IO.TextWriter writer, System.Char c) { }

    }

    // TypeToken: 0x200003D  // size: 0x18
    public sealed class JsonParser
    {
        // Fields
        private static readonly System.Text.RegularExpressions.Regex TimestampRegex;  // static @ 0x0
        private static readonly System.Text.RegularExpressions.Regex DurationRegex;  // static @ 0x8
        private static readonly System.Int32[] SubsecondScalingFactors;  // static @ 0x10
        private static readonly System.Char[] FieldMaskPathSeparators;  // static @ 0x18
        private static readonly Google.Protobuf.Reflection.EnumDescriptor NullValueDescriptor;  // static @ 0x20
        private static readonly Google.Protobuf.JsonParser defaultInstance;  // static @ 0x28
        private static readonly System.Collections.Generic.Dictionary<System.String,System.Action<Google.Protobuf.JsonParser,Google.Protobuf.IMessage,Google.Protobuf.JsonTokenizer>> WellKnownTypeHandlers;  // static @ 0x30
        private readonly Google.Protobuf.JsonParser.Settings settings;  // 0x10

        // Properties
        Google.Protobuf.JsonParser Default { get; /* RVA: 0x09ADC8B8 */ }

        // Methods
        // RVA: 0x09AD9C90  token: 0x600020B
        private static System.Void MergeWrapperField(Google.Protobuf.JsonParser parser, Google.Protobuf.IMessage message, Google.Protobuf.JsonTokenizer tokenizer) { }
        // RVA: 0x09ADC850  token: 0x600020D
        public System.Void .ctor(Google.Protobuf.JsonParser.Settings settings) { }
        // RVA: 0x09ADA2CC  token: 0x600020E
        private System.Void Merge(Google.Protobuf.IMessage message, System.String json) { }
        // RVA: 0x09ADA1EC  token: 0x600020F
        private System.Void Merge(Google.Protobuf.IMessage message, System.IO.TextReader jsonReader) { }
        // RVA: 0x09AD9D2C  token: 0x6000210
        private System.Void Merge(Google.Protobuf.IMessage message, Google.Protobuf.JsonTokenizer tokenizer) { }
        // RVA: 0x09AD88F8  token: 0x6000211
        private System.Void MergeField(Google.Protobuf.IMessage message, Google.Protobuf.Reflection.FieldDescriptor field, Google.Protobuf.JsonTokenizer tokenizer) { }
        // RVA: 0x09AD8D08  token: 0x6000212
        private System.Void MergeRepeatedField(Google.Protobuf.IMessage message, Google.Protobuf.Reflection.FieldDescriptor field, Google.Protobuf.JsonTokenizer tokenizer) { }
        // RVA: 0x09AD8A7C  token: 0x6000213
        private System.Void MergeMapField(Google.Protobuf.IMessage message, Google.Protobuf.Reflection.FieldDescriptor field, Google.Protobuf.JsonTokenizer tokenizer) { }
        // RVA: 0x09AD7C64  token: 0x6000214
        private static System.Boolean IsGoogleProtobufValueField(Google.Protobuf.Reflection.FieldDescriptor field) { }
        // RVA: 0x09AD7BDC  token: 0x6000215
        private static System.Boolean IsGoogleProtobufNullValueField(Google.Protobuf.Reflection.FieldDescriptor field) { }
        // RVA: 0x09ADB4A4  token: 0x6000216
        private System.Object ParseSingleValue(Google.Protobuf.Reflection.FieldDescriptor field, Google.Protobuf.JsonTokenizer tokenizer) { }
        // RVA: -1  // generic def  token: 0x6000217
        public T Parse(System.String json) { }
        // RVA: -1  // generic def  token: 0x6000218
        public T Parse(System.IO.TextReader jsonReader) { }
        // RVA: 0x09ADB8D8  token: 0x6000219
        public Google.Protobuf.IMessage Parse(System.String json, Google.Protobuf.Reflection.MessageDescriptor descriptor) { }
        // RVA: 0x09ADB810  token: 0x600021A
        public Google.Protobuf.IMessage Parse(System.IO.TextReader jsonReader, Google.Protobuf.Reflection.MessageDescriptor descriptor) { }
        // RVA: 0x09AD8EF0  token: 0x600021B
        private System.Void MergeStructValue(Google.Protobuf.IMessage message, Google.Protobuf.JsonTokenizer tokenizer) { }
        // RVA: 0x09AD9214  token: 0x600021C
        private System.Void MergeStruct(Google.Protobuf.IMessage message, Google.Protobuf.JsonTokenizer tokenizer) { }
        // RVA: 0x09AD7CE4  token: 0x600021D
        private System.Void MergeAny(Google.Protobuf.IMessage message, Google.Protobuf.JsonTokenizer tokenizer) { }
        // RVA: 0x09AD9B2C  token: 0x600021E
        private System.Void MergeWellKnownTypeAnyBody(Google.Protobuf.IMessage body, Google.Protobuf.JsonTokenizer tokenizer) { }
        // RVA: 0x09ADA384  token: 0x600021F
        private static System.Object ParseMapKey(Google.Protobuf.Reflection.FieldDescriptor field, System.String keyText) { }
        // RVA: 0x09ADA7F0  token: 0x6000220
        private static System.Object ParseSingleNumberValue(Google.Protobuf.Reflection.FieldDescriptor field, Google.Protobuf.JsonToken token) { }
        // RVA: 0x09AD7A50  token: 0x6000221
        private static System.Void CheckInteger(System.Double value) { }
        // RVA: 0x09ADADD4  token: 0x6000222
        private static System.Object ParseSingleStringValue(Google.Protobuf.Reflection.FieldDescriptor field, System.String text) { }
        // RVA: 0x09ADA354  token: 0x6000223
        private static Google.Protobuf.IMessage NewMessageForField(Google.Protobuf.Reflection.FieldDescriptor field) { }
        // RVA: -1  // generic def  token: 0x6000224
        private static T ParseNumericString(System.String text, System.Func<System.String,System.Globalization.NumberStyles,System.IFormatProvider,T> parser) { }
        // RVA: 0x09ADBB6C  token: 0x6000225
        private static System.Void ValidateInfinityAndNan(System.String text, System.Boolean isPositiveInfinity, System.Boolean isNegativeInfinity, System.Boolean isNaN) { }
        // RVA: 0x09AD9334  token: 0x6000226
        private static System.Void MergeTimestamp(Google.Protobuf.IMessage message, Google.Protobuf.JsonToken token) { }
        // RVA: 0x09AD8110  token: 0x6000227
        private static System.Void MergeDuration(Google.Protobuf.IMessage message, Google.Protobuf.JsonToken token) { }
        // RVA: 0x09AD8730  token: 0x6000228
        private static System.Void MergeFieldMask(Google.Protobuf.IMessage message, Google.Protobuf.JsonToken token) { }
        // RVA: 0x09ADB9BC  token: 0x6000229
        private static System.String ToSnakeCase(System.String text) { }
        // RVA: 0x09ADBC8C  token: 0x600022A
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000040  // size: 0x28
    public sealed class JsonToken : System.IEquatable`1
    {
        // Fields
        private static readonly Google.Protobuf.JsonToken _true;  // static @ 0x0
        private static readonly Google.Protobuf.JsonToken _false;  // static @ 0x8
        private static readonly Google.Protobuf.JsonToken _null;  // static @ 0x10
        private static readonly Google.Protobuf.JsonToken startObject;  // static @ 0x18
        private static readonly Google.Protobuf.JsonToken endObject;  // static @ 0x20
        private static readonly Google.Protobuf.JsonToken startArray;  // static @ 0x28
        private static readonly Google.Protobuf.JsonToken endArray;  // static @ 0x30
        private static readonly Google.Protobuf.JsonToken endDocument;  // static @ 0x38
        private readonly Google.Protobuf.JsonToken.TokenType type;  // 0x10
        private readonly System.String stringValue;  // 0x18
        private readonly System.Double numberValue;  // 0x20

        // Properties
        Google.Protobuf.JsonToken Null { get; /* RVA: 0x09ADEB90 */ }
        Google.Protobuf.JsonToken False { get; /* RVA: 0x09ADEB40 */ }
        Google.Protobuf.JsonToken True { get; /* RVA: 0x09ADEC80 */ }
        Google.Protobuf.JsonToken StartObject { get; /* RVA: 0x09ADEC30 */ }
        Google.Protobuf.JsonToken EndObject { get; /* RVA: 0x09ADEAF0 */ }
        Google.Protobuf.JsonToken StartArray { get; /* RVA: 0x09ADEBE0 */ }
        Google.Protobuf.JsonToken EndArray { get; /* RVA: 0x09ADEA50 */ }
        Google.Protobuf.JsonToken EndDocument { get; /* RVA: 0x09ADEAA0 */ }
        Google.Protobuf.JsonToken.TokenType Type { get; /* RVA: 0x02B2E2D0 */ }
        System.String StringValue { get; /* RVA: 0x0385B100 */ }
        System.Double NumberValue { get; /* RVA: 0x04D86970 */ }

        // Methods
        // RVA: 0x09ADE3C8  token: 0x6000248
        private static Google.Protobuf.JsonToken Name(System.String name) { }
        // RVA: 0x09ADE6D4  token: 0x6000249
        private static Google.Protobuf.JsonToken Value(System.String value) { }
        // RVA: 0x09ADE668  token: 0x600024A
        private static Google.Protobuf.JsonToken Value(System.Double value) { }
        // RVA: 0x09ADEA2C  token: 0x600024E
        private System.Void .ctor(Google.Protobuf.JsonToken.TokenType type, System.String stringValue, System.Double numberValue) { }
        // RVA: 0x09ADE2F0  token: 0x600024F
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x09ADE354  token: 0x6000250
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09ADE434  token: 0x6000251
        public virtual System.String ToString() { }
        // RVA: 0x09ADE29C  token: 0x6000252
        public virtual System.Boolean Equals(Google.Protobuf.JsonToken other) { }
        // RVA: 0x09ADE740  token: 0x6000253
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000042  // size: 0x20
    public abstract class JsonTokenizer
    {
        // Fields
        private Google.Protobuf.JsonToken bufferedToken;  // 0x10
        private System.Int32 <ObjectDepth>k__BackingField;  // 0x18

        // Properties
        System.Int32 ObjectDepth { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x011EF9B0 */ }

        // Methods
        // RVA: 0x09ADED40  token: 0x6000254
        private static Google.Protobuf.JsonTokenizer FromTextReader(System.IO.TextReader reader) { }
        // RVA: 0x09ADECD0  token: 0x6000255
        private static Google.Protobuf.JsonTokenizer FromReplayedTokens(System.Collections.Generic.IList<Google.Protobuf.JsonToken> tokens, Google.Protobuf.JsonTokenizer continuation) { }
        // RVA: 0x09ADEE10  token: 0x6000258
        private System.Void PushBack(Google.Protobuf.JsonToken token) { }
        // RVA: 0x09ADEDA0  token: 0x6000259
        private Google.Protobuf.JsonToken Next() { }
        // RVA: -1  // abstract  token: 0x600025A
        protected virtual Google.Protobuf.JsonToken NextImpl() { }
        // RVA: 0x09ADEEA8  token: 0x600025B
        private System.Void SkipValue() { }
        // RVA: 0x041E1670  token: 0x600025C
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000048  // size: 0x38
    public sealed class LimitedInputStream : System.IO.Stream
    {
        // Fields
        private readonly System.IO.Stream proxied;  // 0x28
        private System.Int32 bytesLeft;  // 0x30

        // Properties
        System.Boolean CanRead { get; /* RVA: 0x02FFF600 */ }
        System.Boolean CanSeek { get; /* RVA: 0x012081B0 */ }
        System.Boolean CanWrite { get; /* RVA: 0x012081B0 */ }
        System.Int64 Length { get; /* RVA: 0x09ADF0DC */ }
        System.Int64 Position { get; /* RVA: 0x09ADF128 */ set; /* RVA: 0x09ADF174 */ }

        // Methods
        // RVA: 0x09ADF07C  token: 0x6000273
        private System.Void .ctor(System.IO.Stream proxied, System.Int32 size) { }
        // RVA: 0x041E1670  token: 0x6000277
        public virtual System.Void Flush() { }
        // RVA: 0x09ADEEF8  token: 0x600027B
        public virtual System.Int32 Read(System.Byte[] buffer, System.Int32 offset, System.Int32 count) { }
        // RVA: 0x09ADEF98  token: 0x600027C
        public virtual System.Int64 Seek(System.Int64 offset, System.IO.SeekOrigin origin) { }
        // RVA: 0x09ADEFE4  token: 0x600027D
        public virtual System.Void SetLength(System.Int64 value) { }
        // RVA: 0x09ADF030  token: 0x600027E
        public virtual System.Void Write(System.Byte[] buffer, System.Int32 offset, System.Int32 count) { }

    }

    // TypeToken: 0x2000049  // size: 0x10
    public static class MessageExtensions
    {
        // Methods
        // RVA: 0x09ADF5A0  token: 0x600027F
        public static System.Void MergeFrom(Google.Protobuf.IMessage message, System.Byte[] data) { }
        // RVA: 0x05394858  token: 0x6000280
        public static System.Void MergeFrom(Google.Protobuf.IMessage message, System.Byte[] data, System.Int32 offset, System.Int32 length) { }
        // RVA: 0x09ADF468  token: 0x6000281
        public static System.Void MergeFrom(Google.Protobuf.IMessage message, Google.Protobuf.ByteString data) { }
        // RVA: 0x09ADF488  token: 0x6000282
        public static System.Void MergeFrom(Google.Protobuf.IMessage message, System.IO.Stream input) { }
        // RVA: 0x05394824  token: 0x6000283
        public static System.Void MergeFrom(Google.Protobuf.IMessage message, System.ReadOnlySpan<System.Byte> span) { }
        // RVA: 0x09ADF448  token: 0x6000284
        public static System.Void MergeDelimitedFrom(Google.Protobuf.IMessage message, System.IO.Stream input) { }
        // RVA: 0x09ADF8C8  token: 0x6000285
        public static System.Byte[] ToByteArray(Google.Protobuf.IMessage message) { }
        // RVA: 0x03143590  token: 0x6000286
        public static System.Int32 WriteToByteArray(Google.Protobuf.IMessage message, System.Byte[] buffer, System.Int32 pos) { }
        // RVA: 0x09ADFB40  token: 0x6000287
        public static System.Void WriteTo(Google.Protobuf.IMessage message, System.IO.Stream output) { }
        // RVA: 0x09ADFA34  token: 0x6000288
        public static System.Void WriteDelimitedTo(Google.Protobuf.IMessage message, System.IO.Stream output) { }
        // RVA: 0x09ADF9B8  token: 0x6000289
        public static Google.Protobuf.ByteString ToByteString(Google.Protobuf.IMessage message) { }
        // RVA: 0x09ADFD00  token: 0x600028A
        public static System.Void WriteTo(Google.Protobuf.IMessage message, System.Buffers.IBufferWriter<System.Byte> output) { }
        // RVA: 0x09ADFC2C  token: 0x600028B
        public static System.Void WriteTo(Google.Protobuf.IMessage message, System.Span<System.Byte> output) { }
        // RVA: 0x09ADF1C0  token: 0x600028C
        public static System.Boolean IsInitialized(Google.Protobuf.IMessage message) { }
        // RVA: 0x09ADF5C0  token: 0x600028D
        private static System.Void MergeFrom(Google.Protobuf.IMessage message, System.Byte[] data, System.Boolean discardUnknownFields, Google.Protobuf.ExtensionRegistry registry) { }
        // RVA: 0x03143020  token: 0x600028E
        private static System.Void MergeFrom(Google.Protobuf.IMessage message, System.Byte[] data, System.Int32 offset, System.Int32 length, System.Boolean discardUnknownFields, Google.Protobuf.ExtensionRegistry registry) { }
        // RVA: 0x09ADF4A8  token: 0x600028F
        private static System.Void MergeFrom(Google.Protobuf.IMessage message, Google.Protobuf.ByteString data, System.Boolean discardUnknownFields, Google.Protobuf.ExtensionRegistry registry) { }
        // RVA: 0x09ADF6DC  token: 0x6000290
        private static System.Void MergeFrom(Google.Protobuf.IMessage message, System.IO.Stream input, System.Boolean discardUnknownFields, Google.Protobuf.ExtensionRegistry registry) { }
        // RVA: 0x09ADF7FC  token: 0x6000291
        private static System.Void MergeFrom(Google.Protobuf.IMessage message, System.Buffers.ReadOnlySequence<System.Byte> data, System.Boolean discardUnknownFields, Google.Protobuf.ExtensionRegistry registry) { }
        // RVA: 0x03DF4750  token: 0x6000292
        private static System.Void MergeFrom(Google.Protobuf.IMessage message, System.ReadOnlySpan<System.Byte> data, System.Boolean discardUnknownFields, Google.Protobuf.ExtensionRegistry registry) { }
        // RVA: 0x09ADF344  token: 0x6000293
        private static System.Void MergeDelimitedFrom(Google.Protobuf.IMessage message, System.IO.Stream input, System.Boolean discardUnknownFields, Google.Protobuf.ExtensionRegistry registry) { }

    }

    // TypeToken: 0x200004B  // size: 0x28
    public class MessageParser
    {
        // Fields
        private System.Func<Google.Protobuf.IMessage> factory;  // 0x10
        private readonly System.Boolean <DiscardUnknownFields>k__BackingField;  // 0x18
        private readonly Google.Protobuf.ExtensionRegistry <Extensions>k__BackingField;  // 0x20

        // Properties
        System.Boolean DiscardUnknownFields { get; /* RVA: 0x04D86340 */ }
        Google.Protobuf.ExtensionRegistry Extensions { get; /* RVA: 0x04D862C0 */ }

        // Methods
        // RVA: 0x04044A30  token: 0x6000298
        private System.Void .ctor(System.Func<Google.Protobuf.IMessage> factory, System.Boolean discardUnknownFields, Google.Protobuf.ExtensionRegistry extensions) { }
        // RVA: 0x09ADFE08  token: 0x6000299
        private Google.Protobuf.IMessage CreateTemplate() { }
        // RVA: 0x09AE0184  token: 0x600029A
        public Google.Protobuf.IMessage ParseFrom(System.Byte[] data) { }
        // RVA: 0x09AE0048  token: 0x600029B
        public Google.Protobuf.IMessage ParseFrom(System.Byte[] data, System.Int32 offset, System.Int32 length) { }
        // RVA: 0x09AE0128  token: 0x600029C
        public Google.Protobuf.IMessage ParseFrom(Google.Protobuf.ByteString data) { }
        // RVA: 0x09AE00CC  token: 0x600029D
        public Google.Protobuf.IMessage ParseFrom(System.IO.Stream input) { }
        // RVA: 0x09AE0248  token: 0x600029E
        public Google.Protobuf.IMessage ParseFrom(System.Buffers.ReadOnlySequence<System.Byte> data) { }
        // RVA: 0x09AE01E0  token: 0x600029F
        public Google.Protobuf.IMessage ParseFrom(System.ReadOnlySpan<System.Byte> data) { }
        // RVA: 0x09ADFFEC  token: 0x60002A0
        public Google.Protobuf.IMessage ParseDelimitedFrom(System.IO.Stream input) { }
        // RVA: 0x09AE02B8  token: 0x60002A1
        public Google.Protobuf.IMessage ParseFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09AE030C  token: 0x60002A2
        public Google.Protobuf.IMessage ParseJson(System.String json) { }
        // RVA: 0x09ADFE28  token: 0x60002A3
        private System.Void MergeFrom(Google.Protobuf.IMessage message, Google.Protobuf.CodedInputStream codedInput) { }
        // RVA: 0x09AE0388  token: 0x60002A4
        public Google.Protobuf.MessageParser WithDiscardUnknownFields(System.Boolean discardUnknownFields) { }
        // RVA: 0x09AE0410  token: 0x60002A5
        public Google.Protobuf.MessageParser WithExtensionRegistry(Google.Protobuf.ExtensionRegistry registry) { }

    }

    // TypeToken: 0x200004C
    public sealed class MessageParser`1 : Google.Protobuf.MessageParser
    {
        // Fields
        private readonly System.Func<T> factory;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60002A6
        public System.Void .ctor(System.Func<T> factory) { }
        // RVA: -1  // not resolved  token: 0x60002A7
        private System.Void .ctor(System.Func<T> factory, System.Boolean discardUnknownFields, Google.Protobuf.ExtensionRegistry extensions) { }
        // RVA: -1  // not resolved  token: 0x60002A8
        private T CreateTemplate() { }
        // RVA: -1  // not resolved  token: 0x60002A9
        public T ParseFrom(System.Byte[] data) { }
        // RVA: -1  // not resolved  token: 0x60002AA
        public T ParseFrom(System.Byte[] data, System.Int32 offset, System.Int32 length) { }
        // RVA: -1  // not resolved  token: 0x60002AB
        public T ParseFrom(Google.Protobuf.ByteString data) { }
        // RVA: -1  // not resolved  token: 0x60002AC
        public T ParseFrom(System.IO.Stream input) { }
        // RVA: -1  // not resolved  token: 0x60002AD
        public T ParseFrom(System.Buffers.ReadOnlySequence<System.Byte> data) { }
        // RVA: -1  // not resolved  token: 0x60002AE
        public T ParseFrom(System.ReadOnlySpan<System.Byte> data) { }
        // RVA: -1  // not resolved  token: 0x60002AF
        public T ParseDelimitedFrom(System.IO.Stream input) { }
        // RVA: -1  // not resolved  token: 0x60002B0
        public T ParseFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: -1  // not resolved  token: 0x60002B1
        public T ParseJson(System.String json) { }
        // RVA: -1  // not resolved  token: 0x60002B2
        public Google.Protobuf.MessageParser<T> WithDiscardUnknownFields(System.Boolean discardUnknownFields) { }
        // RVA: -1  // not resolved  token: 0x60002B3
        public Google.Protobuf.MessageParser<T> WithExtensionRegistry(Google.Protobuf.ExtensionRegistry registry) { }

    }

    // TypeToken: 0x200004E
    public sealed struct ObjectIntPair`1 : System.IEquatable`1
    {
        // Fields
        private readonly System.Int32 number;  // 0x0
        private readonly T obj;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60002B6
        private System.Void .ctor(T obj, System.Int32 number) { }
        // RVA: -1  // not resolved  token: 0x60002B7
        public virtual System.Boolean Equals(Google.Protobuf.ObjectIntPair<T> other) { }
        // RVA: -1  // not resolved  token: 0x60002B8
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: -1  // not resolved  token: 0x60002B9
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x200004F  // size: 0xA0
    public sealed struct ParseContext
    {
        // Fields
        private static System.Int32 DefaultRecursionLimit;  // const
        private static System.Int32 DefaultSizeLimit;  // const
        private System.ReadOnlySpan<System.Byte> buffer;  // 0x10
        private Google.Protobuf.ParserInternalState state;  // 0x20

        // Properties
        System.UInt32 LastTag { get; /* RVA: 0x04D868A0 */ }
        System.Boolean DiscardUnknownFields { get; /* RVA: 0x02A80FA0 */ set; /* RVA: 0x02A80FB0 */ }
        Google.Protobuf.ExtensionRegistry ExtensionRegistry { get; /* RVA: 0x04D861F0 */ set; /* RVA: 0x05395D30 */ }

        // Methods
        // RVA: 0x09AE0CEC  token: 0x60002BA
        private static System.Void Initialize(System.ReadOnlySpan<System.Byte> buffer, Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x04DA0CF0  token: 0x60002BB
        private static System.Void Initialize(System.ReadOnlySpan<System.Byte> buffer, Google.Protobuf.ParserInternalState& state, Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x09AE0C20  token: 0x60002BC
        private static System.Void Initialize(Google.Protobuf.CodedInputStream input, Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x09AE0BF4  token: 0x60002BD
        private static System.Void Initialize(System.Buffers.ReadOnlySequence<System.Byte> input, Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x09AE0B40  token: 0x60002BE
        private static System.Void Initialize(System.Buffers.ReadOnlySequence<System.Byte> input, System.Int32 recursionLimit, Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x09AE0F9C  token: 0x60002C4
        public System.UInt32 ReadTag() { }
        // RVA: 0x09AE0E4C  token: 0x60002C5
        public System.Double ReadDouble() { }
        // RVA: 0x09AE0E7C  token: 0x60002C6
        public System.Single ReadFloat() { }
        // RVA: 0x09AE0EE4  token: 0x60002C7
        public System.UInt64 ReadUInt64() { }
        // RVA: 0x09AE0EE4  token: 0x60002C8
        public System.Int64 ReadInt64() { }
        // RVA: 0x09AE0E58  token: 0x60002C9
        public System.Int32 ReadInt32() { }
        // RVA: 0x09AE0E70  token: 0x60002CA
        public System.UInt64 ReadFixed64() { }
        // RVA: 0x09AE0E64  token: 0x60002CB
        public System.UInt32 ReadFixed32() { }
        // RVA: 0x09AE0E24  token: 0x60002CC
        public System.Boolean ReadBool() { }
        // RVA: 0x09AE0F90  token: 0x60002CD
        public System.String ReadString() { }
        // RVA: 0x09AE0EF0  token: 0x60002CE
        public System.Void ReadMessage(Google.Protobuf.IMessage message) { }
        // RVA: 0x09AE0E88  token: 0x60002CF
        public System.Void ReadGroup(Google.Protobuf.IMessage message) { }
        // RVA: 0x09AE0E40  token: 0x60002D0
        public Google.Protobuf.ByteString ReadBytes() { }
        // RVA: 0x09AE0E58  token: 0x60002D1
        public System.UInt32 ReadUInt32() { }
        // RVA: 0x09AE0E58  token: 0x60002D2
        public System.Int32 ReadEnum() { }
        // RVA: 0x09AE0E64  token: 0x60002D3
        public System.Int32 ReadSFixed32() { }
        // RVA: 0x09AE0E70  token: 0x60002D4
        public System.Int64 ReadSFixed64() { }
        // RVA: 0x09AE0F4C  token: 0x60002D5
        public System.Int32 ReadSInt32() { }
        // RVA: 0x09AE0F6C  token: 0x60002D6
        public System.Int64 ReadSInt64() { }
        // RVA: 0x09AE0E58  token: 0x60002D7
        public System.Int32 ReadLength() { }
        // RVA: 0x03E9EAE0  token: 0x60002D8
        private System.Void CopyStateTo(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09AE0DB0  token: 0x60002D9
        private System.Void LoadStateFrom(Google.Protobuf.CodedInputStream input) { }

    }

    // TypeToken: 0x2000050  // size: 0x90
    public sealed struct ParserInternalState
    {
        // Fields
        private System.Int32 bufferPos;  // 0x10
        private System.Int32 bufferSize;  // 0x14
        private System.Int32 bufferSizeAfterLimit;  // 0x18
        private System.Int32 currentLimit;  // 0x1c
        private System.Int32 totalBytesRetired;  // 0x20
        private System.Int32 recursionDepth;  // 0x24
        private Google.Protobuf.SegmentedBufferHelper segmentedBufferHelper;  // 0x28
        private System.UInt32 lastTag;  // 0x70
        private System.UInt32 nextTag;  // 0x74
        private System.Boolean hasNextTag;  // 0x78
        private System.Int32 sizeLimit;  // 0x7c
        private System.Int32 recursionLimit;  // 0x80
        private System.Boolean <DiscardUnknownFields>k__BackingField;  // 0x84
        private Google.Protobuf.ExtensionRegistry <ExtensionRegistry>k__BackingField;  // 0x88

        // Properties
        Google.Protobuf.CodedInputStream CodedInputStream { get; /* RVA: 0x02E56440 */ }
        System.Boolean DiscardUnknownFields { get; /* RVA: 0x0157A7D0 */ set; /* RVA: 0x0157A810 */ }
        Google.Protobuf.ExtensionRegistry ExtensionRegistry { get; /* RVA: 0x04D862A0 */ set; /* RVA: 0x06402320 */ }

    }

    // TypeToken: 0x2000051  // size: 0x10
    public static class ParsingPrimitives
    {
        // Fields
        private static System.Int32 StackallocThreshold;  // const

        // Methods
        // RVA: 0x09AEB504  token: 0x60002DF
        public static System.Int32 ParseLength(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x02F7EC00  token: 0x60002E0
        public static System.UInt32 ParseTag(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x02F7D9A0  token: 0x60002E1
        public static System.Boolean MaybeConsumeTag(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state, System.UInt32 tag) { }
        // RVA: 0x02F7DA00  token: 0x60002E2
        public static System.UInt32 PeekTag(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x035372C0  token: 0x60002E3
        public static System.UInt64 ParseRawVarint64(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x035373C0  token: 0x60002E4
        private static System.UInt64 ParseRawVarint64SlowPath(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x02F7ED20  token: 0x60002E5
        public static System.UInt32 ParseRawVarint32(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x02F7DA40  token: 0x60002E6
        private static System.UInt32 ParseRawVarint32SlowPath(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x09AEB598  token: 0x60002E7
        public static System.UInt32 ParseRawLittleEndian32(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x09AEB50C  token: 0x60002E8
        private static System.UInt32 ParseRawLittleEndian32SlowPath(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x09AEB720  token: 0x60002E9
        public static System.UInt64 ParseRawLittleEndian64(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x09AEB624  token: 0x60002EA
        private static System.UInt64 ParseRawLittleEndian64SlowPath(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x03D6C820  token: 0x60002EB
        public static System.Double ParseDouble(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x03DB8140  token: 0x60002EC
        public static System.Single ParseFloat(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x09AEB3E0  token: 0x60002ED
        private static System.Single ParseFloatSlow(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x02F84F70  token: 0x60002EE
        public static System.Byte[] ReadRawBytes(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state, System.Int32 size) { }
        // RVA: 0x09AEB984  token: 0x60002EF
        private static System.Byte[] ReadRawBytesSlow(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state, System.Int32 size) { }
        // RVA: 0x09AEBEF4  token: 0x60002F0
        public static System.Void SkipRawBytes(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state, System.Int32 size) { }
        // RVA: 0x09AEBEC0  token: 0x60002F1
        public static System.String ReadString(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x09AEB7AC  token: 0x60002F2
        public static Google.Protobuf.ByteString ReadBytes(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x02F857F0  token: 0x60002F3
        public static System.String ReadRawString(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state, System.Int32 length) { }
        // RVA: 0x02F862A0  token: 0x60002F4
        private static System.String ReadStringSlow(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state, System.Int32 length) { }
        // RVA: 0x02F86360  token: 0x60002F5
        private static System.Void ValidateCurrentLimit(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state, System.Int32 size) { }
        // RVA: 0x02F7EBC0  token: 0x60002F6
        private static System.Byte ReadRawByte(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x09AEBDD0  token: 0x60002F7
        public static System.UInt32 ReadRawVarint32(System.IO.Stream input) { }
        // RVA: 0x04DA0D60  token: 0x60002F8
        public static System.Int32 DecodeZigZag32(System.UInt32 n) { }
        // RVA: 0x04DA0D70  token: 0x60002F9
        public static System.Int64 DecodeZigZag64(System.UInt64 n) { }
        // RVA: 0x03977980  token: 0x60002FA
        public static System.Boolean IsDataAvailable(Google.Protobuf.ParserInternalState& state, System.Int32 size) { }
        // RVA: 0x09AEB334  token: 0x60002FB
        private static System.Boolean IsDataAvailableInSource(Google.Protobuf.ParserInternalState& state, System.Int32 size) { }
        // RVA: 0x09AEB81C  token: 0x60002FC
        private static System.Void ReadRawBytesIntoSpan(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state, System.Int32 length, System.Span<System.Byte> byteSpan) { }

    }

    // TypeToken: 0x2000052  // size: 0x10
    public static class ParsingPrimitivesMessages
    {
        // Fields
        private static readonly System.Byte[] ZeroLengthMessageStreamData;  // static @ 0x0

        // Methods
        // RVA: 0x09AEA7E4  token: 0x60002FD
        public static System.Void SkipLastField(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x09AEA658  token: 0x60002FE
        public static System.Void SkipGroup(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state, System.UInt32 startGroupTag) { }
        // RVA: 0x02F85C20  token: 0x60002FF
        public static System.Void ReadMessage(Google.Protobuf.ParseContext& ctx, Google.Protobuf.IMessage message) { }
        // RVA: -1  // generic def  token: 0x6000300
        public static System.Collections.Generic.KeyValuePair<TKey,TValue> ReadMapEntry(Google.Protobuf.ParseContext& ctx, Google.Protobuf.Collections.MapField.Codec<TKey,TValue> codec) { }
        // RVA: 0x09AEA4FC  token: 0x6000301
        public static System.Void ReadGroup(Google.Protobuf.ParseContext& ctx, Google.Protobuf.IMessage message) { }
        // RVA: 0x09AEA5A4  token: 0x6000302
        public static System.Void ReadGroup(Google.Protobuf.ParseContext& ctx, System.Int32 fieldNumber, Google.Protobuf.UnknownFieldSet set) { }
        // RVA: 0x02F85E00  token: 0x6000303
        public static System.Void ReadRawMessage(Google.Protobuf.ParseContext& ctx, Google.Protobuf.IMessage message) { }
        // RVA: 0x02F85DC0  token: 0x6000304
        public static System.Void CheckReadEndOfStreamTag(Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x09AEA4C0  token: 0x6000305
        private static System.Void CheckLastTagWas(Google.Protobuf.ParserInternalState& state, System.UInt32 expectedTag) { }
        // RVA: 0x04D36760  token: 0x6000306
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000053  // size: 0x10
    public static class ParsingPrimitivesWrappers
    {
        // Methods
        // RVA: 0x09AEAC30  token: 0x6000307
        private static System.Nullable<System.Single> ReadFloatWrapperLittleEndian(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x09AEACF8  token: 0x6000308
        private static System.Nullable<System.Single> ReadFloatWrapperSlow(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x09AEAA4C  token: 0x6000309
        private static System.Nullable<System.Double> ReadDoubleWrapperLittleEndian(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x09AEAB48  token: 0x600030A
        private static System.Nullable<System.Double> ReadDoubleWrapperSlow(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x09AEA980  token: 0x600030B
        private static System.Nullable<System.Boolean> ReadBoolWrapper(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x09AEB014  token: 0x600030C
        private static System.Nullable<System.UInt32> ReadUInt32Wrapper(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x09AEAF3C  token: 0x600030D
        private static System.Nullable<System.UInt32> ReadUInt32WrapperSlow(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x09AEADF0  token: 0x600030E
        private static System.Nullable<System.Int32> ReadInt32Wrapper(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x09AEB214  token: 0x600030F
        private static System.Nullable<System.UInt64> ReadUInt64Wrapper(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x09AEB130  token: 0x6000310
        private static System.Nullable<System.UInt64> ReadUInt64WrapperSlow(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x09AEAE9C  token: 0x6000311
        private static System.Nullable<System.Int64> ReadInt64Wrapper(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x09AEACEC  token: 0x6000312
        private static System.Nullable<System.Single> ReadFloatWrapperLittleEndian(Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x09AEADD8  token: 0x6000313
        private static System.Nullable<System.Single> ReadFloatWrapperSlow(Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x09AEAA20  token: 0x6000314
        private static System.Nullable<System.Double> ReadDoubleWrapperLittleEndian(Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x09AEAB1C  token: 0x6000315
        private static System.Nullable<System.Double> ReadDoubleWrapperSlow(Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x09AEAA14  token: 0x6000316
        private static System.Nullable<System.Boolean> ReadBoolWrapper(Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x09AEB0F8  token: 0x6000317
        private static System.Nullable<System.UInt32> ReadUInt32Wrapper(Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x09AEADE4  token: 0x6000318
        private static System.Nullable<System.Int32> ReadInt32Wrapper(Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x09AEB308  token: 0x6000319
        private static System.Nullable<System.UInt64> ReadUInt64Wrapper(Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x09AEB104  token: 0x600031A
        private static System.Nullable<System.UInt64> ReadUInt64WrapperSlow(Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x09AEAE70  token: 0x600031B
        private static System.Nullable<System.Int64> ReadInt64Wrapper(Google.Protobuf.ParseContext& ctx) { }

    }

    // TypeToken: 0x2000054  // size: 0x10
    public static class ProtoPreconditions
    {
        // Methods
        // RVA: -1  // generic def  token: 0x600031C
        public static T CheckNotNull(T value, System.String name) { }
        // RVA: -1  // generic def  token: 0x600031D
        private static T CheckNotNullUnconstrained(T value, System.String name) { }

    }

    // TypeToken: 0x2000055  // size: 0x58
    public sealed struct SegmentedBufferHelper
    {
        // Fields
        private System.Nullable<System.Int32> totalLength;  // 0x10
        private System.Buffers.ReadOnlySequence.Enumerator<System.Byte> readOnlySequenceEnumerator;  // 0x18
        private Google.Protobuf.CodedInputStream codedInputStream;  // 0x50

        // Properties
        System.Nullable<System.Int32> TotalLength { get; /* RVA: 0x02B76770 */ }
        Google.Protobuf.CodedInputStream CodedInputStream { get; /* RVA: 0x04D85A60 */ }

        // Methods
        // RVA: 0x09AEBFA8  token: 0x600031E
        public static System.Void Initialize(Google.Protobuf.CodedInputStream codedInputStream, Google.Protobuf.SegmentedBufferHelper& instance) { }
        // RVA: 0x09AEC040  token: 0x600031F
        public static System.Void Initialize(System.Buffers.ReadOnlySequence<System.Byte> sequence, Google.Protobuf.SegmentedBufferHelper& instance, System.ReadOnlySpan<System.Byte>& firstSpan) { }
        // RVA: 0x02F7DC40  token: 0x6000320
        public System.Boolean RefillBuffer(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state, System.Boolean mustSucceed) { }
        // RVA: 0x02F85D30  token: 0x6000323
        public static System.Int32 PushLimit(Google.Protobuf.ParserInternalState& state, System.Int32 byteLimit) { }
        // RVA: 0x05390AA0  token: 0x6000324
        public static System.Void PopLimit(Google.Protobuf.ParserInternalState& state, System.Int32 oldLimit) { }
        // RVA: 0x02F85DE0  token: 0x6000325
        public static System.Boolean IsReachedLimit(Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x09AEC238  token: 0x6000326
        public static System.Boolean IsAtEnd(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x03EE6570  token: 0x6000327
        private System.Boolean RefillFromReadOnlySequence(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state, System.Boolean mustSucceed) { }
        // RVA: 0x02F7DB40  token: 0x6000328
        private System.Boolean RefillFromCodedInputStream(System.ReadOnlySpan<System.Byte>& buffer, Google.Protobuf.ParserInternalState& state, System.Boolean mustSucceed) { }
        // RVA: 0x02F85D90  token: 0x6000329
        private static System.Void RecomputeBufferSizeAfterLimit(Google.Protobuf.ParserInternalState& state) { }
        // RVA: 0x02F7DBD0  token: 0x600032A
        private static System.Void CheckCurrentBufferIsEmpty(Google.Protobuf.ParserInternalState& state) { }

    }

    // TypeToken: 0x2000056  // size: 0x38
    public sealed class UnknownField
    {
        // Fields
        private System.Collections.Generic.List<System.UInt64> varintList;  // 0x10
        private System.Collections.Generic.List<System.UInt32> fixed32List;  // 0x18
        private System.Collections.Generic.List<System.UInt64> fixed64List;  // 0x20
        private System.Collections.Generic.List<Google.Protobuf.ByteString> lengthDelimitedList;  // 0x28
        private System.Collections.Generic.List<Google.Protobuf.UnknownFieldSet> groupList;  // 0x30

        // Methods
        // RVA: 0x041E1670  token: 0x600032B
        public System.Void .ctor() { }
        // RVA: 0x09AF1AD8  token: 0x600032C
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09AF1BDC  token: 0x600032D
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09AF22D8  token: 0x600032E
        private System.Void WriteTo(System.Int32 fieldNumber, Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09AF1CC4  token: 0x600032F
        private System.Int32 GetSerializedSize(System.Int32 fieldNumber) { }
        // RVA: 0x09AF21C8  token: 0x6000330
        private Google.Protobuf.UnknownField MergeFrom(Google.Protobuf.UnknownField other) { }
        // RVA: -1  // generic def  token: 0x6000331
        private static System.Collections.Generic.List<T> AddAll(System.Collections.Generic.List<T> current, System.Collections.Generic.IList<T> extras) { }
        // RVA: 0x09AF1A7C  token: 0x6000332
        private Google.Protobuf.UnknownField AddVarint(System.UInt64 value) { }
        // RVA: 0x09AF1910  token: 0x6000333
        private Google.Protobuf.UnknownField AddFixed32(System.UInt32 value) { }
        // RVA: 0x09AF1968  token: 0x6000334
        private Google.Protobuf.UnknownField AddFixed64(System.UInt64 value) { }
        // RVA: 0x09AF1A20  token: 0x6000335
        private Google.Protobuf.UnknownField AddLengthDelimited(Google.Protobuf.ByteString value) { }
        // RVA: 0x09AF19C4  token: 0x6000336
        private Google.Protobuf.UnknownField AddGroup(Google.Protobuf.UnknownFieldSet value) { }
        // RVA: -1  // generic def  token: 0x6000337
        private static System.Collections.Generic.List<T> Add(System.Collections.Generic.List<T> list, T value) { }

    }

    // TypeToken: 0x2000057  // size: 0x28
    public sealed class UnknownFieldSet
    {
        // Fields
        private readonly System.Collections.Generic.IDictionary<System.Int32,Google.Protobuf.UnknownField> fields;  // 0x10
        private System.Int32 lastFieldNumber;  // 0x18
        private Google.Protobuf.UnknownField lastField;  // 0x20

        // Methods
        // RVA: 0x09AF1898  token: 0x6000338
        private System.Void .ctor() { }
        // RVA: 0x09AF0E58  token: 0x6000339
        private System.Boolean HasField(System.Int32 field) { }
        // RVA: 0x09AF15FC  token: 0x600033A
        public System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09AF16C0  token: 0x600033B
        public System.Void WriteTo(Google.Protobuf.WriteContext& ctx) { }
        // RVA: 0x09AF081C  token: 0x600033C
        public System.Int32 CalculateSize() { }
        // RVA: 0x09AF0980  token: 0x600033D
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09AF0C0C  token: 0x600033E
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09AF0D98  token: 0x600033F
        private Google.Protobuf.UnknownField GetOrAddField(System.Int32 number) { }
        // RVA: 0x09AF0754  token: 0x6000340
        private Google.Protobuf.UnknownFieldSet AddOrReplaceField(System.Int32 number, Google.Protobuf.UnknownField field) { }
        // RVA: 0x09AF1094  token: 0x6000341
        private System.Boolean MergeFieldFrom(Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x09AF15B0  token: 0x6000342
        private System.Void MergeGroupFrom(Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x09AF0FA8  token: 0x6000343
        public static Google.Protobuf.UnknownFieldSet MergeFieldFrom(Google.Protobuf.UnknownFieldSet unknownFields, Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09AF0EB0  token: 0x6000344
        public static Google.Protobuf.UnknownFieldSet MergeFieldFrom(Google.Protobuf.UnknownFieldSet unknownFields, Google.Protobuf.ParseContext& ctx) { }
        // RVA: 0x09AF13D0  token: 0x6000345
        private Google.Protobuf.UnknownFieldSet MergeFrom(Google.Protobuf.UnknownFieldSet other) { }
        // RVA: 0x09AF1358  token: 0x6000346
        public static Google.Protobuf.UnknownFieldSet MergeFrom(Google.Protobuf.UnknownFieldSet unknownFields, Google.Protobuf.UnknownFieldSet other) { }
        // RVA: 0x09AF1288  token: 0x6000347
        private Google.Protobuf.UnknownFieldSet MergeField(System.Int32 number, Google.Protobuf.UnknownField field) { }
        // RVA: 0x03218CC0  token: 0x6000348
        public static Google.Protobuf.UnknownFieldSet Clone(Google.Protobuf.UnknownFieldSet other) { }

    }

    // TypeToken: 0x2000058  // size: 0x10
    public static class UnsafeByteOperations
    {
        // Methods
        // RVA: 0x09AF28A8  token: 0x6000349
        public static Google.Protobuf.ByteString UnsafeWrap(System.ReadOnlyMemory<System.Byte> bytes) { }

    }

    // TypeToken: 0x2000059  // size: 0x10
    public static class WireFormat
    {
        // Fields
        private static System.Int32 TagTypeBits;  // const
        private static System.UInt32 TagTypeMask;  // const

        // Methods
        // RVA: 0x04DA0D90  token: 0x600034A
        public static Google.Protobuf.WireFormat.WireType GetTagWireType(System.UInt32 tag) { }
        // RVA: 0x04DA0D80  token: 0x600034B
        public static System.Int32 GetTagFieldNumber(System.UInt32 tag) { }
        // RVA: 0x04DA0DA0  token: 0x600034C
        public static System.UInt32 MakeTag(System.Int32 fieldNumber, Google.Protobuf.WireFormat.WireType wireType) { }

    }

    // TypeToken: 0x200005B  // size: 0x20
    public sealed struct WriteBufferHelper
    {
        // Fields
        private System.Buffers.IBufferWriter<System.Byte> bufferWriter;  // 0x10
        private Google.Protobuf.CodedOutputStream codedOutputStream;  // 0x18

        // Properties
        Google.Protobuf.CodedOutputStream CodedOutputStream { get; /* RVA: 0x04D88320 */ }

        // Methods
        // RVA: 0x03143780  token: 0x600034E
        public static System.Void Initialize(Google.Protobuf.CodedOutputStream codedOutputStream, Google.Protobuf.WriteBufferHelper& instance) { }
        // RVA: 0x09ACC1D0  token: 0x600034F
        public static System.Void Initialize(System.Buffers.IBufferWriter<System.Byte> bufferWriter, Google.Protobuf.WriteBufferHelper& instance, System.Span<System.Byte>& buffer) { }
        // RVA: 0x09ACC1AC  token: 0x6000350
        public static System.Void InitializeNonRefreshable(Google.Protobuf.WriteBufferHelper& instance) { }
        // RVA: 0x09ACC01C  token: 0x6000351
        public static System.Void CheckNoSpaceLeft(Google.Protobuf.WriterInternalState& state) { }
        // RVA: 0x09ACC130  token: 0x6000352
        public static System.Int32 GetSpaceLeft(Google.Protobuf.WriterInternalState& state) { }
        // RVA: 0x09AF3E18  token: 0x6000353
        public static System.Void RefreshBuffer(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state) { }
        // RVA: 0x09AF3D70  token: 0x6000354
        public static System.Void Flush(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state) { }

    }

    // TypeToken: 0x200005C  // size: 0x38
    public sealed struct WriteContext
    {
        // Fields
        private System.Span<System.Byte> buffer;  // 0x10
        private Google.Protobuf.WriterInternalState state;  // 0x20

        // Methods
        // RVA: 0x04DA0DB0  token: 0x6000355
        private static System.Void Initialize(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, Google.Protobuf.WriteContext& ctx) { }
        // RVA: 0x09AF3FA8  token: 0x6000356
        private static System.Void Initialize(Google.Protobuf.CodedOutputStream output, Google.Protobuf.WriteContext& ctx) { }
        // RVA: 0x09AF4024  token: 0x6000357
        private static System.Void Initialize(System.Buffers.IBufferWriter<System.Byte> output, Google.Protobuf.WriteContext& ctx) { }
        // RVA: 0x09AF3F4C  token: 0x6000358
        private static System.Void Initialize(System.Span<System.Byte>& buffer, Google.Protobuf.WriteContext& ctx) { }
        // RVA: 0x03077670  token: 0x6000359
        public System.Void WriteDouble(System.Double value) { }
        // RVA: 0x0307AA60  token: 0x600035A
        public System.Void WriteFloat(System.Single value) { }
        // RVA: 0x03078680  token: 0x600035B
        public System.Void WriteUInt64(System.UInt64 value) { }
        // RVA: 0x03078680  token: 0x600035C
        public System.Void WriteInt64(System.Int64 value) { }
        // RVA: 0x03079380  token: 0x600035D
        public System.Void WriteInt32(System.Int32 value) { }
        // RVA: 0x09AF4108  token: 0x600035E
        public System.Void WriteFixed64(System.UInt64 value) { }
        // RVA: 0x09AF40B4  token: 0x600035F
        public System.Void WriteFixed32(System.UInt32 value) { }
        // RVA: 0x03077520  token: 0x6000360
        public System.Void WriteBool(System.Boolean value) { }
        // RVA: 0x03078230  token: 0x6000361
        public System.Void WriteString(System.String value) { }
        // RVA: 0x05390FA0  token: 0x6000362
        public System.Void WriteMessage(Google.Protobuf.IMessage value) { }
        // RVA: 0x09AF415C  token: 0x6000363
        public System.Void WriteGroup(Google.Protobuf.IMessage value) { }
        // RVA: 0x04213430  token: 0x6000364
        public System.Void WriteBytes(Google.Protobuf.ByteString value) { }
        // RVA: 0x030792A0  token: 0x6000365
        public System.Void WriteUInt32(System.UInt32 value) { }
        // RVA: 0x03079490  token: 0x6000366
        public System.Void WriteEnum(System.Int32 value) { }
        // RVA: 0x09AF42F0  token: 0x6000367
        public System.Void WriteSFixed32(System.Int32 value) { }
        // RVA: 0x09AF4344  token: 0x6000368
        public System.Void WriteSFixed64(System.Int64 value) { }
        // RVA: 0x09AF4398  token: 0x6000369
        public System.Void WriteSInt32(System.Int32 value) { }
        // RVA: 0x09AF43EC  token: 0x600036A
        public System.Void WriteSInt64(System.Int64 value) { }
        // RVA: 0x04592CD0  token: 0x600036B
        public System.Void WriteLength(System.Int32 length) { }
        // RVA: 0x09AF4440  token: 0x600036C
        public System.Void WriteTag(System.Int32 fieldNumber, Google.Protobuf.WireFormat.WireType type) { }
        // RVA: 0x03078910  token: 0x600036D
        public System.Void WriteTag(System.UInt32 tag) { }
        // RVA: 0x0307A030  token: 0x600036E
        public System.Void WriteRawTag(System.Byte b1) { }
        // RVA: 0x03078E70  token: 0x600036F
        public System.Void WriteRawTag(System.Byte b1, System.Byte b2) { }
        // RVA: 0x09AF41E8  token: 0x6000370
        public System.Void WriteRawTag(System.Byte b1, System.Byte b2, System.Byte b3) { }
        // RVA: 0x09AF4164  token: 0x6000371
        public System.Void WriteRawTag(System.Byte b1, System.Byte b2, System.Byte b3, System.Byte b4) { }
        // RVA: 0x09AF4264  token: 0x6000372
        public System.Void WriteRawTag(System.Byte b1, System.Byte b2, System.Byte b3, System.Byte b4, System.Byte b5) { }
        // RVA: 0x09AF3F40  token: 0x6000373
        private System.Void Flush() { }
        // RVA: 0x09AF3F34  token: 0x6000374
        private System.Void CheckNoSpaceLeft() { }
        // RVA: 0x03F21110  token: 0x6000375
        private System.Void CopyStateTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09AF408C  token: 0x6000376
        private System.Void LoadStateFrom(Google.Protobuf.CodedOutputStream output) { }

    }

    // TypeToken: 0x200005D  // size: 0x28
    public sealed struct WriterInternalState
    {
        // Fields
        private System.Int32 limit;  // 0x10
        private System.Int32 position;  // 0x14
        private Google.Protobuf.WriteBufferHelper writeBufferHelper;  // 0x18

        // Properties
        Google.Protobuf.CodedOutputStream CodedOutputStream { get; /* RVA: 0x02B2ECC0 */ }

    }

    // TypeToken: 0x200005E  // size: 0x10
    public static class WritingPrimitives
    {
        // Fields
        private static readonly System.Text.Encoding Utf8Encoding;  // static @ 0x0

        // Methods
        // RVA: 0x030776C0  token: 0x6000378
        public static System.Void WriteDouble(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Double value) { }
        // RVA: 0x0307AB70  token: 0x6000379
        public static System.Void WriteFloat(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Single value) { }
        // RVA: 0x09AF484C  token: 0x600037A
        private static System.Void WriteFloatSlowPath(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Single value) { }
        // RVA: 0x03078770  token: 0x600037B
        public static System.Void WriteUInt64(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.UInt64 value) { }
        // RVA: 0x03078770  token: 0x600037C
        public static System.Void WriteInt64(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Int64 value) { }
        // RVA: 0x030796D0  token: 0x600037D
        public static System.Void WriteInt32(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Int32 value) { }
        // RVA: 0x09AF47EC  token: 0x600037E
        public static System.Void WriteFixed64(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.UInt64 value) { }
        // RVA: 0x09AF478C  token: 0x600037F
        public static System.Void WriteFixed32(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.UInt32 value) { }
        // RVA: 0x03077570  token: 0x6000380
        public static System.Void WriteBool(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Boolean value) { }
        // RVA: 0x03078280  token: 0x6000381
        public static System.Void WriteString(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.String value) { }
        // RVA: 0x03077890  token: 0x6000382
        private static System.Void WriteAsciiStringToBuffer(System.Span<System.Byte> buffer, Google.Protobuf.WriterInternalState& state, System.String value, System.Int32 length) { }
        // RVA: 0x09AF4704  token: 0x6000383
        private static System.Void NarrowFourUtf16CharsToAsciiAndWriteToBuffer(System.Byte& outputBuffer, System.UInt64 value) { }
        // RVA: 0x03078150  token: 0x6000384
        private static System.Int32 WriteStringToBuffer(System.Span<System.Byte> buffer, Google.Protobuf.WriterInternalState& state, System.String value) { }
        // RVA: 0x04213480  token: 0x6000385
        public static System.Void WriteBytes(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, Google.Protobuf.ByteString value) { }
        // RVA: 0x03078960  token: 0x6000386
        public static System.Void WriteUInt32(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.UInt32 value) { }
        // RVA: 0x030795C0  token: 0x6000387
        public static System.Void WriteEnum(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Int32 value) { }
        // RVA: 0x09AF52F8  token: 0x6000388
        public static System.Void WriteSFixed32(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Int32 value) { }
        // RVA: 0x09AF5358  token: 0x6000389
        public static System.Void WriteSFixed64(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Int64 value) { }
        // RVA: 0x09AF53B8  token: 0x600038A
        public static System.Void WriteSInt32(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Int32 value) { }
        // RVA: 0x09AF5424  token: 0x600038B
        public static System.Void WriteSInt64(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Int64 value) { }
        // RVA: 0x03078960  token: 0x600038C
        public static System.Void WriteLength(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Int32 length) { }
        // RVA: 0x030797C0  token: 0x600038D
        public static System.Void WriteRawVarint32(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.UInt32 value) { }
        // RVA: 0x03078840  token: 0x600038E
        public static System.Void WriteRawVarint64(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.UInt64 value) { }
        // RVA: 0x09AF4BC8  token: 0x600038F
        public static System.Void WriteRawLittleEndian32(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.UInt32 value) { }
        // RVA: 0x09AF4B28  token: 0x6000390
        private static System.Void WriteRawLittleEndian32SlowPath(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.UInt32 value) { }
        // RVA: 0x03077730  token: 0x6000391
        public static System.Void WriteRawLittleEndian64(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.UInt64 value) { }
        // RVA: 0x09AF4C84  token: 0x6000392
        public static System.Void WriteRawLittleEndian64SlowPath(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.UInt64 value) { }
        // RVA: 0x0307A120  token: 0x6000393
        private static System.Void WriteRawByte(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Byte value) { }
        // RVA: 0x09AF49E0  token: 0x6000394
        public static System.Void WriteRawBytes(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Byte[] value) { }
        // RVA: 0x09AF4A84  token: 0x6000395
        public static System.Void WriteRawBytes(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Byte[] value, System.Int32 offset, System.Int32 length) { }
        // RVA: 0x04213520  token: 0x6000396
        public static System.Void WriteRawBytes(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.ReadOnlySpan<System.Byte> value) { }
        // RVA: 0x09AF5490  token: 0x6000397
        public static System.Void WriteTag(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Int32 fieldNumber, Google.Protobuf.WireFormat.WireType type) { }
        // RVA: 0x03078960  token: 0x6000398
        public static System.Void WriteTag(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.UInt32 tag) { }
        // RVA: 0x0307A0B0  token: 0x6000399
        public static System.Void WriteRawTag(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Byte b1) { }
        // RVA: 0x03078F10  token: 0x600039A
        public static System.Void WriteRawTag(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Byte b1, System.Byte b2) { }
        // RVA: 0x09AF4F68  token: 0x600039B
        private static System.Void WriteRawTagSlowPath(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Byte b1, System.Byte b2) { }
        // RVA: 0x09AF5218  token: 0x600039C
        public static System.Void WriteRawTag(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Byte b1, System.Byte b2, System.Byte b3) { }
        // RVA: 0x09AF4E30  token: 0x600039D
        private static System.Void WriteRawTagSlowPath(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Byte b1, System.Byte b2, System.Byte b3) { }
        // RVA: 0x09AF4FE8  token: 0x600039E
        public static System.Void WriteRawTag(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Byte b1, System.Byte b2, System.Byte b3, System.Byte b4) { }
        // RVA: 0x09AF4EC4  token: 0x600039F
        private static System.Void WriteRawTagSlowPath(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Byte b1, System.Byte b2, System.Byte b3, System.Byte b4) { }
        // RVA: 0x09AF50EC  token: 0x60003A0
        public static System.Void WriteRawTag(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Byte b1, System.Byte b2, System.Byte b3, System.Byte b4, System.Byte b5) { }
        // RVA: 0x09AF4D78  token: 0x60003A1
        private static System.Void WriteRawTagSlowPath(System.Span<System.Byte>& buffer, Google.Protobuf.WriterInternalState& state, System.Byte b1, System.Byte b2, System.Byte b3, System.Byte b4, System.Byte b5) { }
        // RVA: 0x04DA0DD0  token: 0x60003A2
        public static System.UInt32 EncodeZigZag32(System.Int32 n) { }
        // RVA: 0x04DA0DE0  token: 0x60003A3
        public static System.UInt64 EncodeZigZag64(System.Int64 n) { }
        // RVA: 0x04D56D70  token: 0x60003A4
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200005F  // size: 0x10
    public static class WritingPrimitivesMessages
    {
        // Methods
        // RVA: 0x09AF44A8  token: 0x60003A5
        public static System.Void WriteMessage(Google.Protobuf.WriteContext& ctx, Google.Protobuf.IMessage value) { }
        // RVA: 0x09AF415C  token: 0x60003A6
        public static System.Void WriteGroup(Google.Protobuf.WriteContext& ctx, Google.Protobuf.IMessage value) { }
        // RVA: 0x09AF4540  token: 0x60003A7
        public static System.Void WriteRawMessage(Google.Protobuf.WriteContext& ctx, Google.Protobuf.IMessage message) { }

    }

}

namespace Google.Protobuf.Collections
{

    // TypeToken: 0x200012E  // size: 0x10
    public static class Lists
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000C02
        public static System.Boolean Equals(System.Collections.Generic.List<T> left, System.Collections.Generic.List<T> right) { }
        // RVA: -1  // generic def  token: 0x6000C03
        public static System.Int32 GetHashCode(System.Collections.Generic.List<T> list) { }

    }

    // TypeToken: 0x200012F
    public sealed class MapField`2 : Google.Protobuf.IDeepCloneable`1, System.Collections.Generic.IDictionary`2, System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.IEquatable`1, System.Collections.IDictionary, System.Collections.ICollection, System.Collections.Generic.IReadOnlyDictionary`2, System.Collections.Generic.IReadOnlyCollection`1
    {
        // Fields
        private static readonly System.Collections.Generic.EqualityComparer<TValue> ValueEqualityComparer;  // static @ 0x0
        private static readonly System.Collections.Generic.EqualityComparer<TKey> KeyEqualityComparer;  // static @ 0x0
        private readonly System.Collections.Generic.Dictionary<TKey,System.Collections.Generic.LinkedListNode<System.Collections.Generic.KeyValuePair<TKey,TValue>>> map;  // 0x0
        private readonly System.Collections.Generic.LinkedList<System.Collections.Generic.KeyValuePair<TKey,TValue>> list;  // 0x0

        // Properties
        TValue Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Collections.Generic.ICollection<TKey> Keys { get; /* RVA: -1  // not resolved */ }
        System.Collections.Generic.ICollection<TValue> Values { get; /* RVA: -1  // not resolved */ }
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        System.Boolean IsReadOnly { get; /* RVA: -1  // not resolved */ }
        System.Boolean System.Collections.IDictionary.IsFixedSize { get; /* RVA: -1  // not resolved */ }
        System.Collections.ICollection System.Collections.IDictionary.Keys { get; /* RVA: -1  // not resolved */ }
        System.Collections.ICollection System.Collections.IDictionary.Values { get; /* RVA: -1  // not resolved */ }
        System.Boolean System.Collections.ICollection.IsSynchronized { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.ICollection.SyncRoot { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IDictionary.Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Collections.Generic.IEnumerable<TKey> System.Collections.Generic.IReadOnlyDictionary<TKey,TValue>.Keys { get; /* RVA: -1  // not resolved */ }
        System.Collections.Generic.IEnumerable<TValue> System.Collections.Generic.IReadOnlyDictionary<TKey,TValue>.Values { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000C04
        public virtual Google.Protobuf.Collections.MapField<TKey,TValue> Clone() { }
        // RVA: -1  // not resolved  token: 0x6000C05
        public virtual System.Void Add(TKey key, TValue value) { }
        // RVA: -1  // not resolved  token: 0x6000C06
        public virtual System.Boolean ContainsKey(TKey key) { }
        // RVA: -1  // not resolved  token: 0x6000C07
        private System.Boolean ContainsValue(TValue value) { }
        // RVA: -1  // not resolved  token: 0x6000C08
        public virtual System.Boolean Remove(TKey key) { }
        // RVA: -1  // not resolved  token: 0x6000C09
        public virtual System.Boolean TryGetValue(TKey key, TValue& value) { }
        // RVA: -1  // not resolved  token: 0x6000C0E
        public System.Void Add(System.Collections.Generic.IDictionary<TKey,TValue> entries) { }
        // RVA: -1  // not resolved  token: 0x6000C0F
        public virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey,TValue>> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000C10
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000C11
        private virtual System.Void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.Add(System.Collections.Generic.KeyValuePair<TKey,TValue> item) { }
        // RVA: -1  // not resolved  token: 0x6000C12
        public virtual System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x6000C13
        private virtual System.Boolean System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.Contains(System.Collections.Generic.KeyValuePair<TKey,TValue> item) { }
        // RVA: -1  // not resolved  token: 0x6000C14
        private virtual System.Void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.CopyTo(System.Collections.Generic.KeyValuePair<TKey,TValue>[] array, System.Int32 arrayIndex) { }
        // RVA: -1  // not resolved  token: 0x6000C15
        private virtual System.Boolean System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<TKey,TValue>>.Remove(System.Collections.Generic.KeyValuePair<TKey,TValue> item) { }
        // RVA: -1  // not resolved  token: 0x6000C18
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: -1  // not resolved  token: 0x6000C19
        public virtual System.Int32 GetHashCode() { }
        // RVA: -1  // not resolved  token: 0x6000C1A
        public virtual System.Boolean Equals(Google.Protobuf.Collections.MapField<TKey,TValue> other) { }
        // RVA: -1  // not resolved  token: 0x6000C1B
        public System.Void AddEntriesFrom(Google.Protobuf.CodedInputStream input, Google.Protobuf.Collections.MapField.Codec<TKey,TValue> codec) { }
        // RVA: -1  // not resolved  token: 0x6000C1C
        public System.Void AddEntriesFrom(Google.Protobuf.ParseContext& ctx, Google.Protobuf.Collections.MapField.Codec<TKey,TValue> codec) { }
        // RVA: -1  // not resolved  token: 0x6000C1D
        public System.Void WriteTo(Google.Protobuf.CodedOutputStream output, Google.Protobuf.Collections.MapField.Codec<TKey,TValue> codec) { }
        // RVA: -1  // not resolved  token: 0x6000C1E
        public System.Void WriteTo(Google.Protobuf.WriteContext& ctx, Google.Protobuf.Collections.MapField.Codec<TKey,TValue> codec) { }
        // RVA: -1  // not resolved  token: 0x6000C1F
        public System.Int32 CalculateSize(Google.Protobuf.Collections.MapField.Codec<TKey,TValue> codec) { }
        // RVA: -1  // not resolved  token: 0x6000C20
        private static System.Int32 CalculateEntrySize(Google.Protobuf.Collections.MapField.Codec<TKey,TValue> codec, System.Collections.Generic.KeyValuePair<TKey,TValue> entry) { }
        // RVA: -1  // not resolved  token: 0x6000C21
        public virtual System.String ToString() { }
        // RVA: -1  // not resolved  token: 0x6000C22
        private virtual System.Void System.Collections.IDictionary.Add(System.Object key, System.Object value) { }
        // RVA: -1  // not resolved  token: 0x6000C23
        private virtual System.Boolean System.Collections.IDictionary.Contains(System.Object key) { }
        // RVA: -1  // not resolved  token: 0x6000C24
        private virtual System.Collections.IDictionaryEnumerator System.Collections.IDictionary.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000C25
        private virtual System.Void System.Collections.IDictionary.Remove(System.Object key) { }
        // RVA: -1  // not resolved  token: 0x6000C26
        private virtual System.Void System.Collections.ICollection.CopyTo(System.Array array, System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x6000C30
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000C31
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000135  // size: 0x10
    public static class ProtobufEqualityComparers
    {
        // Fields
        private static readonly System.Collections.Generic.EqualityComparer<System.Double> <BitwiseDoubleEqualityComparer>k__BackingField;  // static @ 0x0
        private static readonly System.Collections.Generic.EqualityComparer<System.Single> <BitwiseSingleEqualityComparer>k__BackingField;  // static @ 0x8
        private static readonly System.Collections.Generic.EqualityComparer<System.Nullable<System.Double>> <BitwiseNullableDoubleEqualityComparer>k__BackingField;  // static @ 0x10
        private static readonly System.Collections.Generic.EqualityComparer<System.Nullable<System.Single>> <BitwiseNullableSingleEqualityComparer>k__BackingField;  // static @ 0x18

        // Properties
        System.Collections.Generic.EqualityComparer<System.Double> BitwiseDoubleEqualityComparer { get; /* RVA: 0x09B22184 */ }
        System.Collections.Generic.EqualityComparer<System.Single> BitwiseSingleEqualityComparer { get; /* RVA: 0x09B22274 */ }
        System.Collections.Generic.EqualityComparer<System.Nullable<System.Double>> BitwiseNullableDoubleEqualityComparer { get; /* RVA: 0x09B221D4 */ }
        System.Collections.Generic.EqualityComparer<System.Nullable<System.Single>> BitwiseNullableSingleEqualityComparer { get; /* RVA: 0x09B22224 */ }

        // Methods
        // RVA: -1  // generic def  token: 0x6000C51
        public static System.Collections.Generic.EqualityComparer<T> GetEqualityComparer() { }
        // RVA: 0x049DEBD0  token: 0x6000C56
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200013A
    public sealed class ReadOnlyDictionary`2 : System.Collections.Generic.IDictionary`2, System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly System.Collections.Generic.IDictionary<TKey,TValue> wrapped;  // 0x0

        // Properties
        System.Collections.Generic.ICollection<TKey> Keys { get; /* RVA: -1  // not resolved */ }
        System.Collections.Generic.ICollection<TValue> Values { get; /* RVA: -1  // not resolved */ }
        TValue Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        System.Boolean IsReadOnly { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000C63
        public System.Void .ctor(System.Collections.Generic.IDictionary<TKey,TValue> wrapped) { }
        // RVA: -1  // not resolved  token: 0x6000C64
        public virtual System.Void Add(TKey key, TValue value) { }
        // RVA: -1  // not resolved  token: 0x6000C65
        public virtual System.Boolean ContainsKey(TKey key) { }
        // RVA: -1  // not resolved  token: 0x6000C67
        public virtual System.Boolean Remove(TKey key) { }
        // RVA: -1  // not resolved  token: 0x6000C68
        public virtual System.Boolean TryGetValue(TKey key, TValue& value) { }
        // RVA: -1  // not resolved  token: 0x6000C6C
        public virtual System.Void Add(System.Collections.Generic.KeyValuePair<TKey,TValue> item) { }
        // RVA: -1  // not resolved  token: 0x6000C6D
        public virtual System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x6000C6E
        public virtual System.Boolean Contains(System.Collections.Generic.KeyValuePair<TKey,TValue> item) { }
        // RVA: -1  // not resolved  token: 0x6000C6F
        public virtual System.Void CopyTo(System.Collections.Generic.KeyValuePair<TKey,TValue>[] array, System.Int32 arrayIndex) { }
        // RVA: -1  // not resolved  token: 0x6000C72
        public virtual System.Boolean Remove(System.Collections.Generic.KeyValuePair<TKey,TValue> item) { }
        // RVA: -1  // not resolved  token: 0x6000C73
        public virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey,TValue>> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000C74
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000C75
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: -1  // not resolved  token: 0x6000C76
        public virtual System.Int32 GetHashCode() { }
        // RVA: -1  // not resolved  token: 0x6000C77
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200013B
    public sealed class RepeatedField`1 : System.Collections.Generic.IList`1, System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.IList, System.Collections.ICollection, Google.Protobuf.IDeepCloneable`1, System.IEquatable`1, System.Collections.Generic.IReadOnlyList`1, System.Collections.Generic.IReadOnlyCollection`1
    {
        // Fields
        private static readonly System.Collections.Generic.EqualityComparer<T> EqualityComparer;  // static @ 0x0
        private static readonly T[] EmptyArray;  // static @ 0x0
        private static System.Int32 MinArraySize;  // const
        private T[] array;  // 0x0
        private System.Int32 count;  // 0x0

        // Properties
        System.Int32 Capacity { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        System.Boolean IsReadOnly { get; /* RVA: -1  // not resolved */ }
        T Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Boolean System.Collections.IList.IsFixedSize { get; /* RVA: -1  // not resolved */ }
        System.Boolean System.Collections.ICollection.IsSynchronized { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.ICollection.SyncRoot { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IList.Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000C78
        public virtual Google.Protobuf.Collections.RepeatedField<T> Clone() { }
        // RVA: -1  // not resolved  token: 0x6000C79
        public System.Void AddEntriesFrom(Google.Protobuf.CodedInputStream input, Google.Protobuf.FieldCodec<T> codec) { }
        // RVA: -1  // not resolved  token: 0x6000C7A
        public System.Void AddEntriesFrom(Google.Protobuf.ParseContext& ctx, Google.Protobuf.FieldCodec<T> codec) { }
        // RVA: -1  // not resolved  token: 0x6000C7B
        public System.Int32 CalculateSize(Google.Protobuf.FieldCodec<T> codec) { }
        // RVA: -1  // not resolved  token: 0x6000C7C
        private System.Int32 CalculatePackedDataSize(Google.Protobuf.FieldCodec<T> codec) { }
        // RVA: -1  // not resolved  token: 0x6000C7D
        public System.Void WriteTo(Google.Protobuf.CodedOutputStream output, Google.Protobuf.FieldCodec<T> codec) { }
        // RVA: -1  // not resolved  token: 0x6000C7E
        public System.Void WriteTo(Google.Protobuf.WriteContext& ctx, Google.Protobuf.FieldCodec<T> codec) { }
        // RVA: -1  // not resolved  token: 0x6000C81
        private System.Void EnsureSize(System.Int32 size) { }
        // RVA: -1  // not resolved  token: 0x6000C82
        private System.Void SetSize(System.Int32 size) { }
        // RVA: -1  // not resolved  token: 0x6000C83
        public System.Void SetCount(System.Int32 newCount) { }
        // RVA: -1  // not resolved  token: 0x6000C84
        public virtual System.Void Add(T item) { }
        // RVA: -1  // not resolved  token: 0x6000C85
        public virtual System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x6000C86
        public virtual System.Boolean Contains(T item) { }
        // RVA: -1  // not resolved  token: 0x6000C87
        public virtual System.Void CopyTo(T[] array, System.Int32 arrayIndex) { }
        // RVA: -1  // not resolved  token: 0x6000C88
        public virtual System.Boolean Remove(T item) { }
        // RVA: -1  // not resolved  token: 0x6000C8B
        public System.Void AddRange(System.Collections.Generic.IEnumerable<T> values) { }
        // RVA: -1  // not resolved  token: 0x6000C8C
        public System.Void Add(System.Collections.Generic.IEnumerable<T> values) { }
        // RVA: -1  // not resolved  token: 0x6000C8D
        public virtual System.Collections.Generic.IEnumerator<T> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000C8E
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: -1  // not resolved  token: 0x6000C8F
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000C90
        public virtual System.Int32 GetHashCode() { }
        // RVA: -1  // not resolved  token: 0x6000C91
        public virtual System.Boolean Equals(Google.Protobuf.Collections.RepeatedField<T> other) { }
        // RVA: -1  // not resolved  token: 0x6000C92
        public virtual System.Int32 IndexOf(T item) { }
        // RVA: -1  // not resolved  token: 0x6000C93
        public virtual System.Void Insert(System.Int32 index, T item) { }
        // RVA: -1  // not resolved  token: 0x6000C94
        public virtual System.Void RemoveAt(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x6000C95
        public virtual System.String ToString() { }
        // RVA: -1  // not resolved  token: 0x6000C99
        private virtual System.Void System.Collections.ICollection.CopyTo(System.Array array, System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x6000C9E
        private virtual System.Int32 System.Collections.IList.Add(System.Object value) { }
        // RVA: -1  // not resolved  token: 0x6000C9F
        private virtual System.Boolean System.Collections.IList.Contains(System.Object value) { }
        // RVA: -1  // not resolved  token: 0x6000CA0
        private virtual System.Int32 System.Collections.IList.IndexOf(System.Object value) { }
        // RVA: -1  // not resolved  token: 0x6000CA1
        private virtual System.Void System.Collections.IList.Insert(System.Int32 index, System.Object value) { }
        // RVA: -1  // not resolved  token: 0x6000CA2
        private virtual System.Void System.Collections.IList.Remove(System.Object value) { }
        // RVA: -1  // not resolved  token: 0x6000CA3
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000CA4
        private static System.Void .cctor() { }

    }

}

namespace Google.Protobuf.Compatibility
{

    // TypeToken: 0x200012C  // size: 0x10
    public static class PropertyInfoExtensions
    {
        // Methods
        // RVA: 0x09B220E4  token: 0x6000BFD
        private static System.Reflection.MethodInfo GetGetMethod(System.Reflection.PropertyInfo target) { }
        // RVA: 0x09B22134  token: 0x6000BFE
        private static System.Reflection.MethodInfo GetSetMethod(System.Reflection.PropertyInfo target) { }

    }

    // TypeToken: 0x200012D  // size: 0x10
    public static class TypeExtensions
    {
        // Methods
        // RVA: 0x09B23E44  token: 0x6000BFF
        private static System.Boolean IsAssignableFrom(System.Type target, System.Type c) { }
        // RVA: 0x09B23D30  token: 0x6000C00
        private static System.Reflection.PropertyInfo GetProperty(System.Type target, System.String name) { }
        // RVA: 0x09B23C78  token: 0x6000C01
        private static System.Reflection.MethodInfo GetMethod(System.Type target, System.String name) { }

    }

}

namespace Google.Protobuf.Reflection
{

    // TypeToken: 0x20000A6  // size: 0x18
    public sealed class CustomOptions
    {
        // Fields
        private static System.String UnreferencedCodeMessage;  // const
        private static readonly System.Object[] EmptyParameters;  // static @ 0x0
        private readonly System.Collections.Generic.IDictionary<System.Int32,Google.Protobuf.IExtensionValue> values;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000683
        private System.Void .ctor(System.Collections.Generic.IDictionary<System.Int32,Google.Protobuf.IExtensionValue> values) { }
        // RVA: 0x09AF63CC  token: 0x6000684
        public System.Boolean TryGetBool(System.Int32 field, System.Boolean& value) { }
        // RVA: 0x09AF653C  token: 0x6000685
        public System.Boolean TryGetInt32(System.Int32 field, System.Int32& value) { }
        // RVA: 0x09AF6594  token: 0x6000686
        public System.Boolean TryGetInt64(System.Int32 field, System.Int64& value) { }
        // RVA: 0x09AF64D4  token: 0x6000687
        public System.Boolean TryGetFixed32(System.Int32 field, System.UInt32& value) { }
        // RVA: 0x09AF64DC  token: 0x6000688
        public System.Boolean TryGetFixed64(System.Int32 field, System.UInt64& value) { }
        // RVA: 0x09AF65EC  token: 0x6000689
        public System.Boolean TryGetSFixed32(System.Int32 field, System.Int32& value) { }
        // RVA: 0x09AF65F4  token: 0x600068A
        public System.Boolean TryGetSFixed64(System.Int32 field, System.Int64& value) { }
        // RVA: 0x09AF65FC  token: 0x600068B
        public System.Boolean TryGetSInt32(System.Int32 field, System.Int32& value) { }
        // RVA: 0x09AF6654  token: 0x600068C
        public System.Boolean TryGetSInt64(System.Int32 field, System.Int64& value) { }
        // RVA: 0x09AF6704  token: 0x600068D
        public System.Boolean TryGetUInt32(System.Int32 field, System.UInt32& value) { }
        // RVA: 0x09AF675C  token: 0x600068E
        public System.Boolean TryGetUInt64(System.Int32 field, System.UInt64& value) { }
        // RVA: 0x09AF64E4  token: 0x600068F
        public System.Boolean TryGetFloat(System.Int32 field, System.Single& value) { }
        // RVA: 0x09AF647C  token: 0x6000690
        public System.Boolean TryGetDouble(System.Int32 field, System.Double& value) { }
        // RVA: 0x09AF66AC  token: 0x6000691
        public System.Boolean TryGetString(System.Int32 field, System.String& value) { }
        // RVA: 0x09AF6424  token: 0x6000692
        public System.Boolean TryGetBytes(System.Int32 field, Google.Protobuf.ByteString& value) { }
        // RVA: -1  // generic def  token: 0x6000693
        public System.Boolean TryGetMessage(System.Int32 field, T& value) { }
        // RVA: -1  // generic def  token: 0x6000694
        private System.Boolean TryGetPrimitiveValue(System.Int32 field, T& value) { }
        // RVA: 0x09AF67B4  token: 0x6000695
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000A8  // size: 0x10
    public static class DescriptorReflection
    {
        // Fields
        private static Google.Protobuf.Reflection.FileDescriptor descriptor;  // static @ 0x0

        // Properties
        Google.Protobuf.Reflection.FileDescriptor Descriptor { get; /* RVA: 0x09AFB72C */ }

        // Methods
        // RVA: 0x09AF8150  token: 0x600069B
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000A9  // size: 0x20
    public sealed class FileDescriptorSet : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.FileDescriptorSet> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 FileFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.FileDescriptorProto> _repeated_file_codec;  // static @ 0x8
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.FileDescriptorProto> file_;  // 0x18

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.Reflection.FileDescriptorSet> Parser { get; /* RVA: 0x09B03C90 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B03C30 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B03E54 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.FileDescriptorProto> File { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x09B03BB8  token: 0x600069F
        public System.Void .ctor() { }
        // RVA: 0x09B03B30  token: 0x60006A0
        public System.Void .ctor(Google.Protobuf.Reflection.FileDescriptorSet other) { }
        // RVA: 0x09B037BC  token: 0x60006A1
        public virtual Google.Protobuf.Reflection.FileDescriptorSet Clone() { }
        // RVA: 0x09B0381C  token: 0x60006A3
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09B03880  token: 0x60006A4
        public virtual System.Boolean Equals(Google.Protobuf.Reflection.FileDescriptorSet other) { }
        // RVA: 0x0638F5B8  token: 0x60006A5
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B03974  token: 0x60006A6
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x60006A7
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09B03DB0  token: 0x60006A8
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09B03728  token: 0x60006A9
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09B038FC  token: 0x60006AA
        public virtual System.Void MergeFrom(Google.Protobuf.Reflection.FileDescriptorSet other) { }
        // RVA: 0x04D51840  token: 0x60006AB
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09B03CE0  token: 0x60006AC
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09B039C0  token: 0x60006AD
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000AB  // size: 0x78
    public sealed class FileDescriptorProto : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.FileDescriptorProto> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 NameFieldNumber;  // const
        private static readonly System.String NameDefaultValue;  // static @ 0x8
        private System.String name_;  // 0x18
        public static System.Int32 PackageFieldNumber;  // const
        private static readonly System.String PackageDefaultValue;  // static @ 0x10
        private System.String package_;  // 0x20
        public static System.Int32 DependencyFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<System.String> _repeated_dependency_codec;  // static @ 0x18
        private readonly Google.Protobuf.Collections.RepeatedField<System.String> dependency_;  // 0x28
        public static System.Int32 PublicDependencyFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<System.Int32> _repeated_publicDependency_codec;  // static @ 0x20
        private readonly Google.Protobuf.Collections.RepeatedField<System.Int32> publicDependency_;  // 0x30
        public static System.Int32 WeakDependencyFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<System.Int32> _repeated_weakDependency_codec;  // static @ 0x28
        private readonly Google.Protobuf.Collections.RepeatedField<System.Int32> weakDependency_;  // 0x38
        public static System.Int32 MessageTypeFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.DescriptorProto> _repeated_messageType_codec;  // static @ 0x30
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.DescriptorProto> messageType_;  // 0x40
        public static System.Int32 EnumTypeFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.EnumDescriptorProto> _repeated_enumType_codec;  // static @ 0x38
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.EnumDescriptorProto> enumType_;  // 0x48
        public static System.Int32 ServiceFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.ServiceDescriptorProto> _repeated_service_codec;  // static @ 0x40
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.ServiceDescriptorProto> service_;  // 0x50
        public static System.Int32 ExtensionFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.FieldDescriptorProto> _repeated_extension_codec;  // static @ 0x48
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.FieldDescriptorProto> extension_;  // 0x58
        public static System.Int32 OptionsFieldNumber;  // const
        private Google.Protobuf.Reflection.FileOptions options_;  // 0x60
        public static System.Int32 SourceCodeInfoFieldNumber;  // const
        private Google.Protobuf.Reflection.SourceCodeInfo sourceCodeInfo_;  // 0x68
        public static System.Int32 SyntaxFieldNumber;  // const
        private static readonly System.String SyntaxDefaultValue;  // static @ 0x50
        private System.String syntax_;  // 0x70

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.Reflection.FileDescriptorProto> Parser { get; /* RVA: 0x09B02E60 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B02D4C */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B035B0 */ }
        System.String Name { get; /* RVA: 0x09B02DB8 */ set; /* RVA: 0x09B035F0 */ }
        System.Boolean HasName { get; /* RVA: 0x067B9E50 */ }
        System.String Package { get; /* RVA: 0x09B02E0C */ set; /* RVA: 0x09B03658 */ }
        System.Boolean HasPackage { get; /* RVA: 0x09B00070 */ }
        Google.Protobuf.Collections.RepeatedField<System.String> Dependency { get; /* RVA: 0x04D86240 */ }
        Google.Protobuf.Collections.RepeatedField<System.Int32> PublicDependency { get; /* RVA: 0x011F36E0 */ }
        Google.Protobuf.Collections.RepeatedField<System.Int32> WeakDependency { get; /* RVA: 0x04D85A50 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.DescriptorProto> MessageType { get; /* RVA: 0x04D85A60 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.EnumDescriptorProto> EnumType { get; /* RVA: 0x04D86200 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.ServiceDescriptorProto> Service { get; /* RVA: 0x04D86270 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.FieldDescriptorProto> Extension { get; /* RVA: 0x02E56440 */ }
        Google.Protobuf.Reflection.FileOptions Options { get; /* RVA: 0x04D86280 */ set; /* RVA: 0x063CC164 */ }
        Google.Protobuf.Reflection.SourceCodeInfo SourceCodeInfo { get; /* RVA: 0x04D86230 */ set; /* RVA: 0x0485F170 */ }
        System.String Syntax { get; /* RVA: 0x09B02EB0 */ set; /* RVA: 0x09B036C0 */ }
        System.Boolean HasSyntax { get; /* RVA: 0x09B02DAC */ }

        // Methods
        // RVA: 0x09B02B24  token: 0x60006B4
        public System.Void .ctor() { }
        // RVA: 0x09B028F4  token: 0x60006B5
        public System.Void .ctor(Google.Protobuf.Reflection.FileDescriptorProto other) { }
        // RVA: 0x09B01D78  token: 0x60006B6
        public virtual Google.Protobuf.Reflection.FileDescriptorProto Clone() { }
        // RVA: 0x05390C40  token: 0x60006BA
        public System.Void ClearName() { }
        // RVA: 0x09AFF598  token: 0x60006BE
        public System.Void ClearPackage() { }
        // RVA: 0x09B01D64  token: 0x60006CD
        public System.Void ClearSyntax() { }
        // RVA: 0x09B01DD8  token: 0x60006CE
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09B01E3C  token: 0x60006CF
        public virtual System.Boolean Equals(Google.Protobuf.Reflection.FileDescriptorProto other) { }
        // RVA: 0x09B02090  token: 0x60006D0
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B024C4  token: 0x60006D1
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x60006D2
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09B032E8  token: 0x60006D3
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09B01A84  token: 0x60006D4
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09B02234  token: 0x60006D5
        public virtual System.Void MergeFrom(Google.Protobuf.Reflection.FileDescriptorProto other) { }
        // RVA: 0x04D51840  token: 0x60006D6
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09B02F04  token: 0x60006D7
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09B02510  token: 0x60006D8
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000AD  // size: 0x68
    public sealed class DescriptorProto : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.DescriptorProto> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 NameFieldNumber;  // const
        private static readonly System.String NameDefaultValue;  // static @ 0x8
        private System.String name_;  // 0x18
        public static System.Int32 FieldFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.FieldDescriptorProto> _repeated_field_codec;  // static @ 0x10
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.FieldDescriptorProto> field_;  // 0x20
        public static System.Int32 ExtensionFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.FieldDescriptorProto> _repeated_extension_codec;  // static @ 0x18
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.FieldDescriptorProto> extension_;  // 0x28
        public static System.Int32 NestedTypeFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.DescriptorProto> _repeated_nestedType_codec;  // static @ 0x20
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.DescriptorProto> nestedType_;  // 0x30
        public static System.Int32 EnumTypeFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.EnumDescriptorProto> _repeated_enumType_codec;  // static @ 0x28
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.EnumDescriptorProto> enumType_;  // 0x38
        public static System.Int32 ExtensionRangeFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.DescriptorProto.Types.ExtensionRange> _repeated_extensionRange_codec;  // static @ 0x30
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.DescriptorProto.Types.ExtensionRange> extensionRange_;  // 0x40
        public static System.Int32 OneofDeclFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.OneofDescriptorProto> _repeated_oneofDecl_codec;  // static @ 0x38
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.OneofDescriptorProto> oneofDecl_;  // 0x48
        public static System.Int32 OptionsFieldNumber;  // const
        private Google.Protobuf.Reflection.MessageOptions options_;  // 0x50
        public static System.Int32 ReservedRangeFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.DescriptorProto.Types.ReservedRange> _repeated_reservedRange_codec;  // static @ 0x40
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.DescriptorProto.Types.ReservedRange> reservedRange_;  // 0x58
        public static System.Int32 ReservedNameFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<System.String> _repeated_reservedName_codec;  // static @ 0x48
        private readonly Google.Protobuf.Collections.RepeatedField<System.String> reservedName_;  // 0x60

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.Reflection.DescriptorProto> Parser { get; /* RVA: 0x09AF7A74 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09AF79C0 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09AF80A8 */ }
        System.String Name { get; /* RVA: 0x09AF7A20 */ set; /* RVA: 0x09AF80E8 */ }
        System.Boolean HasName { get; /* RVA: 0x067B9E50 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.FieldDescriptorProto> Field { get; /* RVA: 0x04D862C0 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.FieldDescriptorProto> Extension { get; /* RVA: 0x04D86240 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.DescriptorProto> NestedType { get; /* RVA: 0x011F36E0 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.EnumDescriptorProto> EnumType { get; /* RVA: 0x04D85A50 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.DescriptorProto.Types.ExtensionRange> ExtensionRange { get; /* RVA: 0x04D85A60 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.OneofDescriptorProto> OneofDecl { get; /* RVA: 0x04D86200 */ }
        Google.Protobuf.Reflection.MessageOptions Options { get; /* RVA: 0x04D86270 */ set; /* RVA: 0x02FDAEA0 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.DescriptorProto.Types.ReservedRange> ReservedRange { get; /* RVA: 0x02E56440 */ }
        Google.Protobuf.Collections.RepeatedField<System.String> ReservedName { get; /* RVA: 0x04D86280 */ }

        // Methods
        // RVA: 0x09AF774C  token: 0x60006DF
        public System.Void .ctor() { }
        // RVA: 0x09AF7540  token: 0x60006E0
        public System.Void .ctor(Google.Protobuf.Reflection.DescriptorProto other) { }
        // RVA: 0x09AF6AB8  token: 0x60006E1
        public virtual Google.Protobuf.Reflection.DescriptorProto Clone() { }
        // RVA: 0x05390C40  token: 0x60006E5
        public System.Void ClearName() { }
        // RVA: 0x09AF6D24  token: 0x60006F0
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09AF6B18  token: 0x60006F1
        public virtual System.Boolean Equals(Google.Protobuf.Reflection.DescriptorProto other) { }
        // RVA: 0x09AF6D88  token: 0x60006F2
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09AF70E8  token: 0x60006F3
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x60006F4
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09AF7E24  token: 0x60006F5
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09AF6818  token: 0x60006F6
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09AF6ED4  token: 0x60006F7
        public virtual System.Void MergeFrom(Google.Protobuf.Reflection.DescriptorProto other) { }
        // RVA: 0x04D51840  token: 0x60006F8
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09AF7AC4  token: 0x60006F9
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09AF7134  token: 0x60006FA
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000B4  // size: 0x28
    public sealed class ExtensionRangeOptions : Google.Protobuf.IExtendableMessage`1, Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.ExtensionRangeOptions> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        private Google.Protobuf.ExtensionSet<Google.Protobuf.Reflection.ExtensionRangeOptions> _extensions;  // 0x18
        public static System.Int32 UninterpretedOptionFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.UninterpretedOption> _repeated_uninterpretedOption_codec;  // static @ 0x8
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.UninterpretedOption> uninterpretedOption_;  // 0x20

        // Properties
        Google.Protobuf.ExtensionSet<Google.Protobuf.Reflection.ExtensionRangeOptions> _Extensions { get; /* RVA: 0x0385B100 */ }
        Google.Protobuf.MessageParser<Google.Protobuf.Reflection.ExtensionRangeOptions> Parser { get; /* RVA: 0x09AFE834 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09AFE7D4 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09AFEA4C */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.UninterpretedOption> UninterpretedOption { get; /* RVA: 0x04D862C0 */ }

        // Methods
        // RVA: 0x09AFE75C  token: 0x600073C
        public System.Void .ctor() { }
        // RVA: 0x09AFE6AC  token: 0x600073D
        public System.Void .ctor(Google.Protobuf.Reflection.ExtensionRangeOptions other) { }
        // RVA: 0x09AFE29C  token: 0x600073E
        public virtual Google.Protobuf.Reflection.ExtensionRangeOptions Clone() { }
        // RVA: 0x09AFE38C  token: 0x6000740
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09AFE2FC  token: 0x6000741
        public virtual System.Boolean Equals(Google.Protobuf.Reflection.ExtensionRangeOptions other) { }
        // RVA: 0x09AFE3F0  token: 0x6000742
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09AFE4F0  token: 0x6000743
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x6000744
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09AFE984  token: 0x6000745
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09AFE1E0  token: 0x6000746
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09AFE458  token: 0x6000747
        public virtual System.Void MergeFrom(Google.Protobuf.Reflection.ExtensionRangeOptions other) { }
        // RVA: 0x04D51840  token: 0x6000748
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09AFE884  token: 0x6000749
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: -1  // generic def  token: 0x600074A
        public virtual TValue GetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.ExtensionRangeOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x600074B
        public virtual Google.Protobuf.Collections.RepeatedField<TValue> GetExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.ExtensionRangeOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x600074C
        public virtual Google.Protobuf.Collections.RepeatedField<TValue> GetOrInitializeExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.ExtensionRangeOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x600074D
        public virtual System.Void SetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.ExtensionRangeOptions,TValue> extension, TValue value) { }
        // RVA: -1  // generic def  token: 0x600074E
        public virtual System.Boolean HasExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.ExtensionRangeOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x600074F
        public virtual System.Void ClearExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.ExtensionRangeOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x6000750
        public virtual System.Void ClearExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.ExtensionRangeOptions,TValue> extension) { }
        // RVA: 0x09AFE53C  token: 0x6000751
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000B6  // size: 0x70
    public sealed class FieldDescriptorProto : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.FieldDescriptorProto> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        private System.Int32 _hasBits0;  // 0x18
        public static System.Int32 NameFieldNumber;  // const
        private static readonly System.String NameDefaultValue;  // static @ 0x8
        private System.String name_;  // 0x20
        public static System.Int32 NumberFieldNumber;  // const
        private static readonly System.Int32 NumberDefaultValue;  // static @ 0x10
        private System.Int32 number_;  // 0x28
        public static System.Int32 LabelFieldNumber;  // const
        private static readonly Google.Protobuf.Reflection.FieldDescriptorProto.Types.Label LabelDefaultValue;  // static @ 0x14
        private Google.Protobuf.Reflection.FieldDescriptorProto.Types.Label label_;  // 0x2c
        public static System.Int32 TypeFieldNumber;  // const
        private static readonly Google.Protobuf.Reflection.FieldDescriptorProto.Types.Type TypeDefaultValue;  // static @ 0x18
        private Google.Protobuf.Reflection.FieldDescriptorProto.Types.Type type_;  // 0x30
        public static System.Int32 TypeNameFieldNumber;  // const
        private static readonly System.String TypeNameDefaultValue;  // static @ 0x20
        private System.String typeName_;  // 0x38
        public static System.Int32 ExtendeeFieldNumber;  // const
        private static readonly System.String ExtendeeDefaultValue;  // static @ 0x28
        private System.String extendee_;  // 0x40
        public static System.Int32 DefaultValueFieldNumber;  // const
        private static readonly System.String DefaultValueDefaultValue;  // static @ 0x30
        private System.String defaultValue_;  // 0x48
        public static System.Int32 OneofIndexFieldNumber;  // const
        private static readonly System.Int32 OneofIndexDefaultValue;  // static @ 0x38
        private System.Int32 oneofIndex_;  // 0x50
        public static System.Int32 JsonNameFieldNumber;  // const
        private static readonly System.String JsonNameDefaultValue;  // static @ 0x40
        private System.String jsonName_;  // 0x58
        public static System.Int32 OptionsFieldNumber;  // const
        private Google.Protobuf.Reflection.FieldOptions options_;  // 0x60
        public static System.Int32 Proto3OptionalFieldNumber;  // const
        private static readonly System.Boolean Proto3OptionalDefaultValue;  // static @ 0x48
        private System.Boolean proto3Optional_;  // 0x68

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.Reflection.FieldDescriptorProto> Parser { get; /* RVA: 0x09B00244 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09AFFFA4 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B007D4 */ }
        System.String Name { get; /* RVA: 0x09B00148 */ set; /* RVA: 0x09B0094C */ }
        System.Boolean HasName { get; /* RVA: 0x09B00070 */ }
        System.Int32 Number { get; /* RVA: 0x09B0019C */ set; /* RVA: 0x04DA0E60 */ }
        System.Boolean HasNumber { get; /* RVA: 0x0314ECE0 */ }
        Google.Protobuf.Reflection.FieldDescriptorProto.Types.Label Label { get; /* RVA: 0x09B000F4 */ set; /* RVA: 0x04DA0E50 */ }
        System.Boolean HasLabel { get; /* RVA: 0x09AFF0A8 */ }
        Google.Protobuf.Reflection.FieldDescriptorProto.Types.Type Type { get; /* RVA: 0x09B0033C */ set; /* RVA: 0x04DA0E90 */ }
        System.Boolean HasType { get; /* RVA: 0x09B00098 */ }
        System.String TypeName { get; /* RVA: 0x09B002E8 */ set; /* RVA: 0x09B009B4 */ }
        System.Boolean HasTypeName { get; /* RVA: 0x09B0008C */ }
        System.String Extendee { get; /* RVA: 0x09B00004 */ set; /* RVA: 0x09B0087C */ }
        System.Boolean HasExtendee { get; /* RVA: 0x09B00058 */ }
        System.String DefaultValue { get; /* RVA: 0x09AFFF50 */ set; /* RVA: 0x09B00814 */ }
        System.Boolean HasDefaultValue { get; /* RVA: 0x090BCB14 */ }
        System.Int32 OneofIndex { get; /* RVA: 0x09B001F0 */ set; /* RVA: 0x04DA0E70 */ }
        System.Boolean HasOneofIndex { get; /* RVA: 0x09B0007C */ }
        System.String JsonName { get; /* RVA: 0x09B000A0 */ set; /* RVA: 0x09B008E4 */ }
        System.Boolean HasJsonName { get; /* RVA: 0x09B00064 */ }
        Google.Protobuf.Reflection.FieldOptions Options { get; /* RVA: 0x04D86280 */ set; /* RVA: 0x063CC164 */ }
        System.Boolean Proto3Optional { get; /* RVA: 0x09B00294 */ set; /* RVA: 0x04DA0E80 */ }
        System.Boolean HasProto3Optional { get; /* RVA: 0x09B00084 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000758
        public System.Void .ctor() { }
        // RVA: 0x09AFFE6C  token: 0x6000759
        public System.Void .ctor(Google.Protobuf.Reflection.FieldDescriptorProto other) { }
        // RVA: 0x09AFF5AC  token: 0x600075A
        public virtual Google.Protobuf.Reflection.FieldDescriptorProto Clone() { }
        // RVA: 0x09AFF598  token: 0x600075E
        public System.Void ClearName() { }
        // RVA: 0x04D86DE0  token: 0x6000762
        public System.Void ClearNumber() { }
        // RVA: 0x04DA0DF0  token: 0x6000766
        public System.Void ClearLabel() { }
        // RVA: 0x04DA0E40  token: 0x600076A
        public System.Void ClearType() { }
        // RVA: 0x0709F154  token: 0x600076E
        public System.Void ClearTypeName() { }
        // RVA: 0x09AFF570  token: 0x6000772
        public System.Void ClearExtendee() { }
        // RVA: 0x070A7CA8  token: 0x6000776
        public System.Void ClearDefaultValue() { }
        // RVA: 0x04DA0E20  token: 0x600077A
        public System.Void ClearOneofIndex() { }
        // RVA: 0x09AFF584  token: 0x600077E
        public System.Void ClearJsonName() { }
        // RVA: 0x04DA0E30  token: 0x6000784
        public System.Void ClearProto3Optional() { }
        // RVA: 0x09AFF60C  token: 0x6000785
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09AFF670  token: 0x6000786
        public virtual System.Boolean Equals(Google.Protobuf.Reflection.FieldDescriptorProto other) { }
        // RVA: 0x09AFF83C  token: 0x6000787
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09AFFBB8  token: 0x6000788
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x6000789
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09B005D0  token: 0x600078A
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09AFF358  token: 0x600078B
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09AFF9EC  token: 0x600078C
        public virtual System.Void MergeFrom(Google.Protobuf.Reflection.FieldDescriptorProto other) { }
        // RVA: 0x04D51840  token: 0x600078D
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09B00390  token: 0x600078E
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09AFFC04  token: 0x600078F
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000BB  // size: 0x28
    public sealed class OneofDescriptorProto : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.OneofDescriptorProto> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 NameFieldNumber;  // const
        private static readonly System.String NameDefaultValue;  // static @ 0x8
        private System.String name_;  // 0x18
        public static System.Int32 OptionsFieldNumber;  // const
        private Google.Protobuf.Reflection.OneofOptions options_;  // 0x20

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.Reflection.OneofDescriptorProto> Parser { get; /* RVA: 0x09B04F98 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B04EE4 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B0515C */ }
        System.String Name { get; /* RVA: 0x09B04F44 */ set; /* RVA: 0x09B0519C */ }
        System.Boolean HasName { get; /* RVA: 0x067B9E50 */ }
        Google.Protobuf.Reflection.OneofOptions Options { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000796
        public System.Void .ctor() { }
        // RVA: 0x09B04E70  token: 0x6000797
        public System.Void .ctor(Google.Protobuf.Reflection.OneofDescriptorProto other) { }
        // RVA: 0x09B04A48  token: 0x6000798
        public virtual Google.Protobuf.Reflection.OneofDescriptorProto Clone() { }
        // RVA: 0x05390C40  token: 0x600079C
        public System.Void ClearName() { }
        // RVA: 0x09B04B2C  token: 0x600079F
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09B04AA8  token: 0x60007A0
        public virtual System.Boolean Equals(Google.Protobuf.Reflection.OneofDescriptorProto other) { }
        // RVA: 0x09B04B90  token: 0x60007A1
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B04CE0  token: 0x60007A2
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x60007A3
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09B050DC  token: 0x60007A4
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09B049A0  token: 0x60007A5
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09B04C04  token: 0x60007A6
        public virtual System.Void MergeFrom(Google.Protobuf.Reflection.OneofDescriptorProto other) { }
        // RVA: 0x04D51840  token: 0x60007A7
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09B04FE8  token: 0x60007A8
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09B04D2C  token: 0x60007A9
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000BD  // size: 0x40
    public sealed class EnumDescriptorProto : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.EnumDescriptorProto> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 NameFieldNumber;  // const
        private static readonly System.String NameDefaultValue;  // static @ 0x8
        private System.String name_;  // 0x18
        public static System.Int32 ValueFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.EnumValueDescriptorProto> _repeated_value_codec;  // static @ 0x10
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.EnumValueDescriptorProto> value_;  // 0x20
        public static System.Int32 OptionsFieldNumber;  // const
        private Google.Protobuf.Reflection.EnumOptions options_;  // 0x28
        public static System.Int32 ReservedRangeFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.EnumDescriptorProto.Types.EnumReservedRange> _repeated_reservedRange_codec;  // static @ 0x18
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.EnumDescriptorProto.Types.EnumReservedRange> reservedRange_;  // 0x30
        public static System.Int32 ReservedNameFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<System.String> _repeated_reservedName_codec;  // static @ 0x20
        private readonly Google.Protobuf.Collections.RepeatedField<System.String> reservedName_;  // 0x38

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.Reflection.EnumDescriptorProto> Parser { get; /* RVA: 0x09AFC6A8 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09AFC5F4 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09AFCA54 */ }
        System.String Name { get; /* RVA: 0x09AFC654 */ set; /* RVA: 0x09AFCA94 */ }
        System.Boolean HasName { get; /* RVA: 0x067B9E50 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.EnumValueDescriptorProto> Value { get; /* RVA: 0x04D862C0 */ }
        Google.Protobuf.Reflection.EnumOptions Options { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.EnumDescriptorProto.Types.EnumReservedRange> ReservedRange { get; /* RVA: 0x011F36E0 */ }
        Google.Protobuf.Collections.RepeatedField<System.String> ReservedName { get; /* RVA: 0x04D85A50 */ }

        // Methods
        // RVA: 0x09AFC3C8  token: 0x60007B0
        public System.Void .ctor() { }
        // RVA: 0x09AFC4D4  token: 0x60007B1
        public System.Void .ctor(Google.Protobuf.Reflection.EnumDescriptorProto other) { }
        // RVA: 0x09AFBD28  token: 0x60007B2
        public virtual Google.Protobuf.Reflection.EnumDescriptorProto Clone() { }
        // RVA: 0x05390C40  token: 0x60007B6
        public System.Void ClearName() { }
        // RVA: 0x09AFBD88  token: 0x60007BC
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09AFBDEC  token: 0x60007BD
        public virtual System.Boolean Equals(Google.Protobuf.Reflection.EnumDescriptorProto other) { }
        // RVA: 0x09AFBF10  token: 0x60007BE
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09AFC130  token: 0x60007BF
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x60007C0
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09AFC8E4  token: 0x60007C1
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09AFBBA0  token: 0x60007C2
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09AFBFDC  token: 0x60007C3
        public virtual System.Void MergeFrom(Google.Protobuf.Reflection.EnumDescriptorProto other) { }
        // RVA: 0x04D51840  token: 0x60007C4
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09AFC6F8  token: 0x60007C5
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09AFC17C  token: 0x60007C6
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000C2  // size: 0x38
    public sealed class EnumValueDescriptorProto : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.EnumValueDescriptorProto> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        private System.Int32 _hasBits0;  // 0x18
        public static System.Int32 NameFieldNumber;  // const
        private static readonly System.String NameDefaultValue;  // static @ 0x8
        private System.String name_;  // 0x20
        public static System.Int32 NumberFieldNumber;  // const
        private static readonly System.Int32 NumberDefaultValue;  // static @ 0x10
        private System.Int32 number_;  // 0x28
        public static System.Int32 OptionsFieldNumber;  // const
        private Google.Protobuf.Reflection.EnumValueOptions options_;  // 0x30

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.Reflection.EnumValueDescriptorProto> Parser { get; /* RVA: 0x09B0D6E0 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B0D5D8 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B0D8F8 */ }
        System.String Name { get; /* RVA: 0x09B0D638 */ set; /* RVA: 0x09B0D938 */ }
        System.Boolean HasName { get; /* RVA: 0x09B00070 */ }
        System.Int32 Number { get; /* RVA: 0x09B0D68C */ set; /* RVA: 0x04DA0E60 */ }
        System.Boolean HasNumber { get; /* RVA: 0x0314ECE0 */ }
        Google.Protobuf.Reflection.EnumValueOptions Options { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60007E9
        public System.Void .ctor() { }
        // RVA: 0x09B0D558  token: 0x60007EA
        public System.Void .ctor(Google.Protobuf.Reflection.EnumValueDescriptorProto other) { }
        // RVA: 0x09B0D0D4  token: 0x60007EB
        public virtual Google.Protobuf.Reflection.EnumValueDescriptorProto Clone() { }
        // RVA: 0x09AFF598  token: 0x60007EF
        public System.Void ClearName() { }
        // RVA: 0x04D86DE0  token: 0x60007F3
        public System.Void ClearNumber() { }
        // RVA: 0x09B0D134  token: 0x60007F6
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09B0D198  token: 0x60007F7
        public virtual System.Boolean Equals(Google.Protobuf.Reflection.EnumValueDescriptorProto other) { }
        // RVA: 0x09B0D234  token: 0x60007F8
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B0D3B0  token: 0x60007F9
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x60007FA
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09B0D850  token: 0x60007FB
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09B0D000  token: 0x60007FC
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09B0D2BC  token: 0x60007FD
        public virtual System.Void MergeFrom(Google.Protobuf.Reflection.EnumValueDescriptorProto other) { }
        // RVA: 0x04D51840  token: 0x60007FE
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09B0D730  token: 0x60007FF
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09B0D3FC  token: 0x6000800
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000C4  // size: 0x30
    public sealed class ServiceDescriptorProto : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.ServiceDescriptorProto> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 NameFieldNumber;  // const
        private static readonly System.String NameDefaultValue;  // static @ 0x8
        private System.String name_;  // 0x18
        public static System.Int32 MethodFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.MethodDescriptorProto> _repeated_method_codec;  // static @ 0x10
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.MethodDescriptorProto> method_;  // 0x20
        public static System.Int32 OptionsFieldNumber;  // const
        private Google.Protobuf.Reflection.ServiceOptions options_;  // 0x28

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.Reflection.ServiceDescriptorProto> Parser { get; /* RVA: 0x09B1817C */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B180C8 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B18410 */ }
        System.String Name { get; /* RVA: 0x09B18128 */ set; /* RVA: 0x09B18450 */ }
        System.Boolean HasName { get; /* RVA: 0x067B9E50 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.MethodDescriptorProto> Method { get; /* RVA: 0x04D862C0 */ }
        Google.Protobuf.Reflection.ServiceOptions Options { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }

        // Methods
        // RVA: 0x09B17F8C  token: 0x6000807
        public System.Void .ctor() { }
        // RVA: 0x09B18004  token: 0x6000808
        public System.Void .ctor(Google.Protobuf.Reflection.ServiceDescriptorProto other) { }
        // RVA: 0x09B17A80  token: 0x6000809
        public virtual Google.Protobuf.Reflection.ServiceDescriptorProto Clone() { }
        // RVA: 0x05390C40  token: 0x600080D
        public System.Void ClearName() { }
        // RVA: 0x09B17BA4  token: 0x6000811
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09B17AE0  token: 0x6000812
        public virtual System.Boolean Equals(Google.Protobuf.Reflection.ServiceDescriptorProto other) { }
        // RVA: 0x09B17C08  token: 0x6000813
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B17D94  token: 0x6000814
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x6000815
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09B18320  token: 0x6000816
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09B17980  token: 0x6000817
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09B17C90  token: 0x6000818
        public virtual System.Void MergeFrom(Google.Protobuf.Reflection.ServiceDescriptorProto other) { }
        // RVA: 0x04D51840  token: 0x6000819
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09B181CC  token: 0x600081A
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09B17DE0  token: 0x600081B
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000C6  // size: 0x48
    public sealed class MethodDescriptorProto : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.MethodDescriptorProto> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        private System.Int32 _hasBits0;  // 0x18
        public static System.Int32 NameFieldNumber;  // const
        private static readonly System.String NameDefaultValue;  // static @ 0x8
        private System.String name_;  // 0x20
        public static System.Int32 InputTypeFieldNumber;  // const
        private static readonly System.String InputTypeDefaultValue;  // static @ 0x10
        private System.String inputType_;  // 0x28
        public static System.Int32 OutputTypeFieldNumber;  // const
        private static readonly System.String OutputTypeDefaultValue;  // static @ 0x18
        private System.String outputType_;  // 0x30
        public static System.Int32 OptionsFieldNumber;  // const
        private Google.Protobuf.Reflection.MethodOptions options_;  // 0x38
        public static System.Int32 ClientStreamingFieldNumber;  // const
        private static readonly System.Boolean ClientStreamingDefaultValue;  // static @ 0x20
        private System.Boolean clientStreaming_;  // 0x40
        public static System.Int32 ServerStreamingFieldNumber;  // const
        private static readonly System.Boolean ServerStreamingDefaultValue;  // static @ 0x21
        private System.Boolean serverStreaming_;  // 0x41

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.Reflection.MethodDescriptorProto> Parser { get; /* RVA: 0x09B1592C */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B157D0 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B15C7C */ }
        System.String Name { get; /* RVA: 0x09B15884 */ set; /* RVA: 0x09B15D24 */ }
        System.Boolean HasName { get; /* RVA: 0x09B00070 */ }
        System.String InputType { get; /* RVA: 0x09B15830 */ set; /* RVA: 0x09B15CBC */ }
        System.Boolean HasInputType { get; /* RVA: 0x068C60A8 */ }
        System.String OutputType { get; /* RVA: 0x09B158D8 */ set; /* RVA: 0x09B15D8C */ }
        System.Boolean HasOutputType { get; /* RVA: 0x09B11948 */ }
        Google.Protobuf.Reflection.MethodOptions Options { get; /* RVA: 0x04D85A50 */ set; /* RVA: 0x046A91B0 */ }
        System.Boolean ClientStreaming { get; /* RVA: 0x09B1577C */ set; /* RVA: 0x04DA10B0 */ }
        System.Boolean HasClientStreaming { get; /* RVA: 0x0314ECE0 */ }
        System.Boolean ServerStreaming { get; /* RVA: 0x09B1597C */ set; /* RVA: 0x04DA10C0 */ }
        System.Boolean HasServerStreaming { get; /* RVA: 0x09AFF0A8 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000822
        public System.Void .ctor() { }
        // RVA: 0x09B156D0  token: 0x6000823
        public System.Void .ctor(Google.Protobuf.Reflection.MethodDescriptorProto other) { }
        // RVA: 0x09B15058  token: 0x6000824
        public virtual Google.Protobuf.Reflection.MethodDescriptorProto Clone() { }
        // RVA: 0x09AFF598  token: 0x6000828
        public System.Void ClearName() { }
        // RVA: 0x09849400  token: 0x600082C
        public System.Void ClearInputType() { }
        // RVA: 0x067B5A00  token: 0x6000830
        public System.Void ClearOutputType() { }
        // RVA: 0x04D86DE0  token: 0x6000836
        public System.Void ClearClientStreaming() { }
        // RVA: 0x04DA0DF0  token: 0x600083A
        public System.Void ClearServerStreaming() { }
        // RVA: 0x09B151D0  token: 0x600083B
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09B150B8  token: 0x600083C
        public virtual System.Boolean Equals(Google.Protobuf.Reflection.MethodDescriptorProto other) { }
        // RVA: 0x09B15234  token: 0x600083D
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B154BC  token: 0x600083E
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x600083F
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09B15B50  token: 0x6000840
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09B14F40  token: 0x6000841
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09B15374  token: 0x6000842
        public virtual System.Void MergeFrom(Google.Protobuf.Reflection.MethodDescriptorProto other) { }
        // RVA: 0x04D51840  token: 0x6000843
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09B159D0  token: 0x6000844
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09B15508  token: 0x6000845
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000C8  // size: 0x90
    public sealed class FileOptions : Google.Protobuf.IExtendableMessage`1, Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.FileOptions> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        private Google.Protobuf.ExtensionSet<Google.Protobuf.Reflection.FileOptions> _extensions;  // 0x18
        private System.Int32 _hasBits0;  // 0x20
        public static System.Int32 JavaPackageFieldNumber;  // const
        private static readonly System.String JavaPackageDefaultValue;  // static @ 0x8
        private System.String javaPackage_;  // 0x28
        public static System.Int32 JavaOuterClassnameFieldNumber;  // const
        private static readonly System.String JavaOuterClassnameDefaultValue;  // static @ 0x10
        private System.String javaOuterClassname_;  // 0x30
        public static System.Int32 JavaMultipleFilesFieldNumber;  // const
        private static readonly System.Boolean JavaMultipleFilesDefaultValue;  // static @ 0x18
        private System.Boolean javaMultipleFiles_;  // 0x38
        public static System.Int32 JavaGenerateEqualsAndHashFieldNumber;  // const
        private static readonly System.Boolean JavaGenerateEqualsAndHashDefaultValue;  // static @ 0x19
        private System.Boolean javaGenerateEqualsAndHash_;  // 0x39
        public static System.Int32 JavaStringCheckUtf8FieldNumber;  // const
        private static readonly System.Boolean JavaStringCheckUtf8DefaultValue;  // static @ 0x1a
        private System.Boolean javaStringCheckUtf8_;  // 0x3a
        public static System.Int32 OptimizeForFieldNumber;  // const
        private static readonly Google.Protobuf.Reflection.FileOptions.Types.OptimizeMode OptimizeForDefaultValue;  // static @ 0x1c
        private Google.Protobuf.Reflection.FileOptions.Types.OptimizeMode optimizeFor_;  // 0x3c
        public static System.Int32 GoPackageFieldNumber;  // const
        private static readonly System.String GoPackageDefaultValue;  // static @ 0x20
        private System.String goPackage_;  // 0x40
        public static System.Int32 CcGenericServicesFieldNumber;  // const
        private static readonly System.Boolean CcGenericServicesDefaultValue;  // static @ 0x28
        private System.Boolean ccGenericServices_;  // 0x48
        public static System.Int32 JavaGenericServicesFieldNumber;  // const
        private static readonly System.Boolean JavaGenericServicesDefaultValue;  // static @ 0x29
        private System.Boolean javaGenericServices_;  // 0x49
        public static System.Int32 PyGenericServicesFieldNumber;  // const
        private static readonly System.Boolean PyGenericServicesDefaultValue;  // static @ 0x2a
        private System.Boolean pyGenericServices_;  // 0x4a
        public static System.Int32 PhpGenericServicesFieldNumber;  // const
        private static readonly System.Boolean PhpGenericServicesDefaultValue;  // static @ 0x2b
        private System.Boolean phpGenericServices_;  // 0x4b
        public static System.Int32 DeprecatedFieldNumber;  // const
        private static readonly System.Boolean DeprecatedDefaultValue;  // static @ 0x2c
        private System.Boolean deprecated_;  // 0x4c
        public static System.Int32 CcEnableArenasFieldNumber;  // const
        private static readonly System.Boolean CcEnableArenasDefaultValue;  // static @ 0x2d
        private System.Boolean ccEnableArenas_;  // 0x4d
        public static System.Int32 ObjcClassPrefixFieldNumber;  // const
        private static readonly System.String ObjcClassPrefixDefaultValue;  // static @ 0x30
        private System.String objcClassPrefix_;  // 0x50
        public static System.Int32 CsharpNamespaceFieldNumber;  // const
        private static readonly System.String CsharpNamespaceDefaultValue;  // static @ 0x38
        private System.String csharpNamespace_;  // 0x58
        public static System.Int32 SwiftPrefixFieldNumber;  // const
        private static readonly System.String SwiftPrefixDefaultValue;  // static @ 0x40
        private System.String swiftPrefix_;  // 0x60
        public static System.Int32 PhpClassPrefixFieldNumber;  // const
        private static readonly System.String PhpClassPrefixDefaultValue;  // static @ 0x48
        private System.String phpClassPrefix_;  // 0x68
        public static System.Int32 PhpNamespaceFieldNumber;  // const
        private static readonly System.String PhpNamespaceDefaultValue;  // static @ 0x50
        private System.String phpNamespace_;  // 0x70
        public static System.Int32 PhpMetadataNamespaceFieldNumber;  // const
        private static readonly System.String PhpMetadataNamespaceDefaultValue;  // static @ 0x58
        private System.String phpMetadataNamespace_;  // 0x78
        public static System.Int32 RubyPackageFieldNumber;  // const
        private static readonly System.String RubyPackageDefaultValue;  // static @ 0x60
        private System.String rubyPackage_;  // 0x80
        public static System.Int32 UninterpretedOptionFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.UninterpretedOption> _repeated_uninterpretedOption_codec;  // static @ 0x68
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.UninterpretedOption> uninterpretedOption_;  // 0x88

        // Properties
        Google.Protobuf.ExtensionSet<Google.Protobuf.Reflection.FileOptions> _Extensions { get; /* RVA: 0x0385B100 */ }
        Google.Protobuf.MessageParser<Google.Protobuf.Reflection.FileOptions> Parser { get; /* RVA: 0x09B11C30 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B11888 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B12718 */ }
        System.String JavaPackage { get; /* RVA: 0x09B11AE0 */ set; /* RVA: 0x09B12890 */ }
        System.Boolean HasJavaPackage { get; /* RVA: 0x068C60A8 */ }
        System.String JavaOuterClassname { get; /* RVA: 0x09B11A8C */ set; /* RVA: 0x09B12828 */ }
        System.Boolean HasJavaOuterClassname { get; /* RVA: 0x09B11948 */ }
        System.Boolean JavaMultipleFiles { get; /* RVA: 0x09B11A38 */ set; /* RVA: 0x04DA1040 */ }
        System.Boolean HasJavaMultipleFiles { get; /* RVA: 0x09B0C6D4 */ }
        System.Boolean JavaGenerateEqualsAndHash { get; /* RVA: 0x09B11990 */ set; /* RVA: 0x04DA1020 */ }
        System.Boolean HasJavaGenerateEqualsAndHash { get; /* RVA: 0x09B0FC9C */ }
        System.Boolean JavaStringCheckUtf8 { get; /* RVA: 0x09B11B34 */ set; /* RVA: 0x04DA1050 */ }
        System.Boolean HasJavaStringCheckUtf8 { get; /* RVA: 0x09B11954 */ }
        Google.Protobuf.Reflection.FileOptions.Types.OptimizeMode OptimizeFor { get; /* RVA: 0x09B11BDC */ set; /* RVA: 0x04DA1060 */ }
        System.Boolean HasOptimizeFor { get; /* RVA: 0x09B0C6CC */ }
        System.String GoPackage { get; /* RVA: 0x09B118E8 */ set; /* RVA: 0x09B127C0 */ }
        System.Boolean HasGoPackage { get; /* RVA: 0x09B00058 */ }
        System.Boolean CcGenericServices { get; /* RVA: 0x09B1178C */ set; /* RVA: 0x04DA1000 */ }
        System.Boolean HasCcGenericServices { get; /* RVA: 0x09B0FC7C */ }
        System.Boolean JavaGenericServices { get; /* RVA: 0x09B119E4 */ set; /* RVA: 0x04DA1030 */ }
        System.Boolean HasJavaGenericServices { get; /* RVA: 0x09B0FC8C */ }
        System.Boolean PyGenericServices { get; /* RVA: 0x09B11DD4 */ set; /* RVA: 0x04DA1080 */ }
        System.Boolean HasPyGenericServices { get; /* RVA: 0x09B0FC84 */ }
        System.Boolean PhpGenericServices { get; /* RVA: 0x09B11CD4 */ set; /* RVA: 0x04DA1070 */ }
        System.Boolean HasPhpGenericServices { get; /* RVA: 0x0481D0E0 */ }
        System.Boolean Deprecated { get; /* RVA: 0x09B11834 */ set; /* RVA: 0x04DA1010 */ }
        System.Boolean HasDeprecated { get; /* RVA: 0x09B0FC94 */ }
        System.Boolean CcEnableArenas { get; /* RVA: 0x09B11734 */ set; /* RVA: 0x04DA0FF0 */ }
        System.Boolean HasCcEnableArenas { get; /* RVA: 0x09B1193C */ }
        System.String ObjcClassPrefix { get; /* RVA: 0x09B11B88 */ set; /* RVA: 0x09B128F8 */ }
        System.Boolean HasObjcClassPrefix { get; /* RVA: 0x084774C8 */ }
        System.String CsharpNamespace { get; /* RVA: 0x09B117E0 */ set; /* RVA: 0x09B12758 */ }
        System.Boolean HasCsharpNamespace { get; /* RVA: 0x09B00064 */ }
        System.String SwiftPrefix { get; /* RVA: 0x09B11E80 */ set; /* RVA: 0x09B12B04 */ }
        System.Boolean HasSwiftPrefix { get; /* RVA: 0x09B11984 */ }
        System.String PhpClassPrefix { get; /* RVA: 0x09B11C80 */ set; /* RVA: 0x09B12960 */ }
        System.Boolean HasPhpClassPrefix { get; /* RVA: 0x09B11960 */ }
        System.String PhpNamespace { get; /* RVA: 0x09B11D80 */ set; /* RVA: 0x09B12A30 */ }
        System.Boolean HasPhpNamespace { get; /* RVA: 0x09B02DAC */ }
        System.String PhpMetadataNamespace { get; /* RVA: 0x09B11D2C */ set; /* RVA: 0x09B129C8 */ }
        System.Boolean HasPhpMetadataNamespace { get; /* RVA: 0x09B1196C */ }
        System.String RubyPackage { get; /* RVA: 0x09B11E28 */ set; /* RVA: 0x09B12A98 */ }
        System.Boolean HasRubyPackage { get; /* RVA: 0x09B11978 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.UninterpretedOption> UninterpretedOption { get; /* RVA: 0x04D861F0 */ }

        // Methods
        // RVA: 0x09B11504  token: 0x600084D
        public System.Void .ctor() { }
        // RVA: 0x09B11580  token: 0x600084E
        public System.Void .ctor(Google.Protobuf.Reflection.FileOptions other) { }
        // RVA: 0x09B10618  token: 0x600084F
        public virtual Google.Protobuf.Reflection.FileOptions Clone() { }
        // RVA: 0x09849400  token: 0x6000853
        public System.Void ClearJavaPackage() { }
        // RVA: 0x067B5A00  token: 0x6000857
        public System.Void ClearJavaOuterClassname() { }
        // RVA: 0x04DA0ED0  token: 0x600085B
        public System.Void ClearJavaMultipleFiles() { }
        // RVA: 0x04DA0F40  token: 0x600085F
        public System.Void ClearJavaGenerateEqualsAndHash() { }
        // RVA: 0x04DA0FD0  token: 0x6000863
        public System.Void ClearJavaStringCheckUtf8() { }
        // RVA: 0x04DA0EC0  token: 0x6000867
        public System.Void ClearOptimizeFor() { }
        // RVA: 0x09AFF570  token: 0x600086B
        public System.Void ClearGoPackage() { }
        // RVA: 0x04DA0F00  token: 0x600086F
        public System.Void ClearCcGenericServices() { }
        // RVA: 0x04DA0F20  token: 0x6000873
        public System.Void ClearJavaGenericServices() { }
        // RVA: 0x04DA0F10  token: 0x6000877
        public System.Void ClearPyGenericServices() { }
        // RVA: 0x04DA0FE0  token: 0x600087B
        public System.Void ClearPhpGenericServices() { }
        // RVA: 0x04DA0F30  token: 0x600087F
        public System.Void ClearDeprecated() { }
        // RVA: 0x04DA0FC0  token: 0x6000883
        public System.Void ClearCcEnableArenas() { }
        // RVA: 0x0709F140  token: 0x6000887
        public System.Void ClearObjcClassPrefix() { }
        // RVA: 0x09AFF584  token: 0x600088B
        public System.Void ClearCsharpNamespace() { }
        // RVA: 0x06973C40  token: 0x600088F
        public System.Void ClearSwiftPrefix() { }
        // RVA: 0x09B105DC  token: 0x6000893
        public System.Void ClearPhpClassPrefix() { }
        // RVA: 0x09B01D64  token: 0x6000897
        public System.Void ClearPhpNamespace() { }
        // RVA: 0x09B105F0  token: 0x600089B
        public System.Void ClearPhpMetadataNamespace() { }
        // RVA: 0x09B10604  token: 0x600089F
        public System.Void ClearRubyPackage() { }
        // RVA: 0x09B10678  token: 0x60008A1
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09B106DC  token: 0x60008A2
        public virtual System.Boolean Equals(Google.Protobuf.Reflection.FileOptions other) { }
        // RVA: 0x09B10A6C  token: 0x60008A3
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B110A4  token: 0x60008A4
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x60008A5
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09B122B8  token: 0x60008A6
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09B102A4  token: 0x60008A7
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09B10DD4  token: 0x60008A8
        public virtual System.Void MergeFrom(Google.Protobuf.Reflection.FileOptions other) { }
        // RVA: 0x04D51840  token: 0x60008A9
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09B11ED4  token: 0x60008AA
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: -1  // generic def  token: 0x60008AB
        public virtual TValue GetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.FileOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x60008AC
        public virtual Google.Protobuf.Collections.RepeatedField<TValue> GetExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.FileOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x60008AD
        public virtual Google.Protobuf.Collections.RepeatedField<TValue> GetOrInitializeExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.FileOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x60008AE
        public virtual System.Void SetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.FileOptions,TValue> extension, TValue value) { }
        // RVA: -1  // generic def  token: 0x60008AF
        public virtual System.Boolean HasExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.FileOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x60008B0
        public virtual System.Void ClearExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.FileOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x60008B1
        public virtual System.Void ClearExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.FileOptions,TValue> extension) { }
        // RVA: 0x09B110F0  token: 0x60008B2
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000CC  // size: 0x30
    public sealed class MessageOptions : Google.Protobuf.IExtendableMessage`1, Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.MessageOptions> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        private Google.Protobuf.ExtensionSet<Google.Protobuf.Reflection.MessageOptions> _extensions;  // 0x18
        private System.Int32 _hasBits0;  // 0x20
        public static System.Int32 MessageSetWireFormatFieldNumber;  // const
        private static readonly System.Boolean MessageSetWireFormatDefaultValue;  // static @ 0x8
        private System.Boolean messageSetWireFormat_;  // 0x24
        public static System.Int32 NoStandardDescriptorAccessorFieldNumber;  // const
        private static readonly System.Boolean NoStandardDescriptorAccessorDefaultValue;  // static @ 0x9
        private System.Boolean noStandardDescriptorAccessor_;  // 0x25
        public static System.Int32 DeprecatedFieldNumber;  // const
        private static readonly System.Boolean DeprecatedDefaultValue;  // static @ 0xa
        private System.Boolean deprecated_;  // 0x26
        public static System.Int32 MapEntryFieldNumber;  // const
        private static readonly System.Boolean MapEntryDefaultValue;  // static @ 0xb
        private System.Boolean mapEntry_;  // 0x27
        public static System.Int32 UninterpretedOptionFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.UninterpretedOption> _repeated_uninterpretedOption_codec;  // static @ 0x10
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.UninterpretedOption> uninterpretedOption_;  // 0x28

        // Properties
        Google.Protobuf.ExtensionSet<Google.Protobuf.Reflection.MessageOptions> _Extensions { get; /* RVA: 0x0385B100 */ }
        Google.Protobuf.MessageParser<Google.Protobuf.Reflection.MessageOptions> Parser { get; /* RVA: 0x09B14BD8 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B14A7C */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B14F00 */ }
        System.Boolean MessageSetWireFormat { get; /* RVA: 0x09B14B30 */ set; /* RVA: 0x04DA0EE0 */ }
        System.Boolean HasMessageSetWireFormat { get; /* RVA: 0x09B0C6CC */ }
        System.Boolean NoStandardDescriptorAccessor { get; /* RVA: 0x09B14B84 */ set; /* RVA: 0x04DA0EF0 */ }
        System.Boolean HasNoStandardDescriptorAccessor { get; /* RVA: 0x09B0C6D4 */ }
        System.Boolean Deprecated { get; /* RVA: 0x09B14A28 */ set; /* RVA: 0x04DA1090 */ }
        System.Boolean HasDeprecated { get; /* RVA: 0x09B0FC7C */ }
        System.Boolean MapEntry { get; /* RVA: 0x09B14ADC */ set; /* RVA: 0x04DA10A0 */ }
        System.Boolean HasMapEntry { get; /* RVA: 0x09B0FC8C */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.UninterpretedOption> UninterpretedOption { get; /* RVA: 0x04D86240 */ }

        // Methods
        // RVA: 0x09B149B0  token: 0x60008BA
        public System.Void .ctor() { }
        // RVA: 0x09B148E4  token: 0x60008BB
        public System.Void .ctor(Google.Protobuf.Reflection.MessageOptions other) { }
        // RVA: 0x09B14324  token: 0x60008BC
        public virtual Google.Protobuf.Reflection.MessageOptions Clone() { }
        // RVA: 0x04DA0EC0  token: 0x60008C0
        public System.Void ClearMessageSetWireFormat() { }
        // RVA: 0x04DA0ED0  token: 0x60008C4
        public System.Void ClearNoStandardDescriptorAccessor() { }
        // RVA: 0x04DA0F00  token: 0x60008C8
        public System.Void ClearDeprecated() { }
        // RVA: 0x04DA0F20  token: 0x60008CC
        public System.Void ClearMapEntry() { }
        // RVA: 0x09B14384  token: 0x60008CE
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09B143E8  token: 0x60008CF
        public virtual System.Boolean Equals(Google.Protobuf.Reflection.MessageOptions other) { }
        // RVA: 0x09B144FC  token: 0x60008D0
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B14708  token: 0x60008D1
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x60008D2
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09B14D90  token: 0x60008D3
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09B1423C  token: 0x60008D4
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09B14610  token: 0x60008D5
        public virtual System.Void MergeFrom(Google.Protobuf.Reflection.MessageOptions other) { }
        // RVA: 0x04D51840  token: 0x60008D6
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09B14C28  token: 0x60008D7
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: -1  // generic def  token: 0x60008D8
        public virtual TValue GetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.MessageOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x60008D9
        public virtual Google.Protobuf.Collections.RepeatedField<TValue> GetExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.MessageOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x60008DA
        public virtual Google.Protobuf.Collections.RepeatedField<TValue> GetOrInitializeExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.MessageOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x60008DB
        public virtual System.Void SetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.MessageOptions,TValue> extension, TValue value) { }
        // RVA: -1  // generic def  token: 0x60008DC
        public virtual System.Boolean HasExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.MessageOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x60008DD
        public virtual System.Void ClearExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.MessageOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x60008DE
        public virtual System.Void ClearExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.MessageOptions,TValue> extension) { }
        // RVA: 0x09B14754  token: 0x60008DF
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000CE  // size: 0x40
    public sealed class FieldOptions : Google.Protobuf.IExtendableMessage`1, Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.FieldOptions> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        private Google.Protobuf.ExtensionSet<Google.Protobuf.Reflection.FieldOptions> _extensions;  // 0x18
        private System.Int32 _hasBits0;  // 0x20
        public static System.Int32 CtypeFieldNumber;  // const
        private static readonly Google.Protobuf.Reflection.FieldOptions.Types.CType CtypeDefaultValue;  // static @ 0x8
        private Google.Protobuf.Reflection.FieldOptions.Types.CType ctype_;  // 0x24
        public static System.Int32 PackedFieldNumber;  // const
        private static readonly System.Boolean PackedDefaultValue;  // static @ 0xc
        private System.Boolean packed_;  // 0x28
        public static System.Int32 JstypeFieldNumber;  // const
        private static readonly Google.Protobuf.Reflection.FieldOptions.Types.JSType JstypeDefaultValue;  // static @ 0x10
        private Google.Protobuf.Reflection.FieldOptions.Types.JSType jstype_;  // 0x2c
        public static System.Int32 LazyFieldNumber;  // const
        private static readonly System.Boolean LazyDefaultValue;  // static @ 0x14
        private System.Boolean lazy_;  // 0x30
        public static System.Int32 UnverifiedLazyFieldNumber;  // const
        private static readonly System.Boolean UnverifiedLazyDefaultValue;  // static @ 0x15
        private System.Boolean unverifiedLazy_;  // 0x31
        public static System.Int32 DeprecatedFieldNumber;  // const
        private static readonly System.Boolean DeprecatedDefaultValue;  // static @ 0x16
        private System.Boolean deprecated_;  // 0x32
        public static System.Int32 WeakFieldNumber;  // const
        private static readonly System.Boolean WeakDefaultValue;  // static @ 0x17
        private System.Boolean weak_;  // 0x33
        public static System.Int32 UninterpretedOptionFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.UninterpretedOption> _repeated_uninterpretedOption_codec;  // static @ 0x18
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.UninterpretedOption> uninterpretedOption_;  // 0x38

        // Properties
        Google.Protobuf.ExtensionSet<Google.Protobuf.Reflection.FieldOptions> _Extensions { get; /* RVA: 0x0385B100 */ }
        Google.Protobuf.MessageParser<Google.Protobuf.Reflection.FieldOptions> Parser { get; /* RVA: 0x09B0FDA0 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B0FC1C */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B10264 */ }
        Google.Protobuf.Reflection.FieldOptions.Types.CType Ctype { get; /* RVA: 0x09B0FB74 */ set; /* RVA: 0x04DA0F50 */ }
        System.Boolean HasCtype { get; /* RVA: 0x09B0C6CC */ }
        System.Boolean Packed { get; /* RVA: 0x09B0FD4C */ set; /* RVA: 0x04DA0F90 */ }
        System.Boolean HasPacked { get; /* RVA: 0x09B0C6D4 */ }
        Google.Protobuf.Reflection.FieldOptions.Types.JSType Jstype { get; /* RVA: 0x09B0FCA4 */ set; /* RVA: 0x04DA0F70 */ }
        System.Boolean HasJstype { get; /* RVA: 0x09B0FC84 */ }
        System.Boolean Lazy { get; /* RVA: 0x09B0FCF8 */ set; /* RVA: 0x04DA0F80 */ }
        System.Boolean HasLazy { get; /* RVA: 0x09B0FC8C */ }
        System.Boolean UnverifiedLazy { get; /* RVA: 0x09B0FDF0 */ set; /* RVA: 0x04DA0FA0 */ }
        System.Boolean HasUnverifiedLazy { get; /* RVA: 0x09B0FC94 */ }
        System.Boolean Deprecated { get; /* RVA: 0x09B0FBC8 */ set; /* RVA: 0x04DA0F60 */ }
        System.Boolean HasDeprecated { get; /* RVA: 0x09B0FC7C */ }
        System.Boolean Weak { get; /* RVA: 0x09B0FE44 */ set; /* RVA: 0x04DA0FB0 */ }
        System.Boolean HasWeak { get; /* RVA: 0x09B0FC9C */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.UninterpretedOption> UninterpretedOption { get; /* RVA: 0x04D85A50 */ }

        // Methods
        // RVA: 0x09B0FAFC  token: 0x60008E7
        public System.Void .ctor() { }
        // RVA: 0x09B0FA18  token: 0x60008E8
        public System.Void .ctor(Google.Protobuf.Reflection.FieldOptions other) { }
        // RVA: 0x09B0F340  token: 0x60008E9
        public virtual Google.Protobuf.Reflection.FieldOptions Clone() { }
        // RVA: 0x04DA0EC0  token: 0x60008ED
        public System.Void ClearCtype() { }
        // RVA: 0x04DA0ED0  token: 0x60008F1
        public System.Void ClearPacked() { }
        // RVA: 0x04DA0F10  token: 0x60008F5
        public System.Void ClearJstype() { }
        // RVA: 0x04DA0F20  token: 0x60008F9
        public System.Void ClearLazy() { }
        // RVA: 0x04DA0F30  token: 0x60008FD
        public System.Void ClearUnverifiedLazy() { }
        // RVA: 0x04DA0F00  token: 0x6000901
        public System.Void ClearDeprecated() { }
        // RVA: 0x04DA0F40  token: 0x6000905
        public System.Void ClearWeak() { }
        // RVA: 0x09B0F3A0  token: 0x6000907
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09B0F404  token: 0x6000908
        public virtual System.Boolean Equals(Google.Protobuf.Reflection.FieldOptions other) { }
        // RVA: 0x09B0F570  token: 0x6000909
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B0F808  token: 0x600090A
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x600090B
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09B10074  token: 0x600090C
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09B0F1D8  token: 0x600090D
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09B0F6C8  token: 0x600090E
        public virtual System.Void MergeFrom(Google.Protobuf.Reflection.FieldOptions other) { }
        // RVA: 0x04D51840  token: 0x600090F
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09B0FE98  token: 0x6000910
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: -1  // generic def  token: 0x6000911
        public virtual TValue GetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.FieldOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x6000912
        public virtual Google.Protobuf.Collections.RepeatedField<TValue> GetExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.FieldOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x6000913
        public virtual Google.Protobuf.Collections.RepeatedField<TValue> GetOrInitializeExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.FieldOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x6000914
        public virtual System.Void SetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.FieldOptions,TValue> extension, TValue value) { }
        // RVA: -1  // generic def  token: 0x6000915
        public virtual System.Boolean HasExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.FieldOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x6000916
        public virtual System.Void ClearExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.FieldOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x6000917
        public virtual System.Void ClearExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.FieldOptions,TValue> extension) { }
        // RVA: 0x09B0F854  token: 0x6000918
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000D3  // size: 0x28
    public sealed class OneofOptions : Google.Protobuf.IExtendableMessage`1, Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.OneofOptions> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        private Google.Protobuf.ExtensionSet<Google.Protobuf.Reflection.OneofOptions> _extensions;  // 0x18
        public static System.Int32 UninterpretedOptionFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.UninterpretedOption> _repeated_uninterpretedOption_codec;  // static @ 0x8
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.UninterpretedOption> uninterpretedOption_;  // 0x20

        // Properties
        Google.Protobuf.ExtensionSet<Google.Protobuf.Reflection.OneofOptions> _Extensions { get; /* RVA: 0x0385B100 */ }
        Google.Protobuf.MessageParser<Google.Protobuf.Reflection.OneofOptions> Parser { get; /* RVA: 0x09B17728 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B176C8 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B17940 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.UninterpretedOption> UninterpretedOption { get; /* RVA: 0x04D862C0 */ }

        // Methods
        // RVA: 0x09B17650  token: 0x6000920
        public System.Void .ctor() { }
        // RVA: 0x09B175A0  token: 0x6000921
        public System.Void .ctor(Google.Protobuf.Reflection.OneofOptions other) { }
        // RVA: 0x09B171F8  token: 0x6000922
        public virtual Google.Protobuf.Reflection.OneofOptions Clone() { }
        // RVA: 0x09B172E8  token: 0x6000924
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09B17258  token: 0x6000925
        public virtual System.Boolean Equals(Google.Protobuf.Reflection.OneofOptions other) { }
        // RVA: 0x09AFE3F0  token: 0x6000926
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B173E4  token: 0x6000927
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x6000928
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09B17878  token: 0x6000929
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09B1713C  token: 0x600092A
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09B1734C  token: 0x600092B
        public virtual System.Void MergeFrom(Google.Protobuf.Reflection.OneofOptions other) { }
        // RVA: 0x04D51840  token: 0x600092C
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09B17778  token: 0x600092D
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: -1  // generic def  token: 0x600092E
        public virtual TValue GetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.OneofOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x600092F
        public virtual Google.Protobuf.Collections.RepeatedField<TValue> GetExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.OneofOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x6000930
        public virtual Google.Protobuf.Collections.RepeatedField<TValue> GetOrInitializeExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.OneofOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x6000931
        public virtual System.Void SetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.OneofOptions,TValue> extension, TValue value) { }
        // RVA: -1  // generic def  token: 0x6000932
        public virtual System.Boolean HasExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.OneofOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x6000933
        public virtual System.Void ClearExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.OneofOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x6000934
        public virtual System.Void ClearExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.OneofOptions,TValue> extension) { }
        // RVA: 0x09B17430  token: 0x6000935
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000D5  // size: 0x30
    public sealed class EnumOptions : Google.Protobuf.IExtendableMessage`1, Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.EnumOptions> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        private Google.Protobuf.ExtensionSet<Google.Protobuf.Reflection.EnumOptions> _extensions;  // 0x18
        private System.Int32 _hasBits0;  // 0x20
        public static System.Int32 AllowAliasFieldNumber;  // const
        private static readonly System.Boolean AllowAliasDefaultValue;  // static @ 0x8
        private System.Boolean allowAlias_;  // 0x24
        public static System.Int32 DeprecatedFieldNumber;  // const
        private static readonly System.Boolean DeprecatedDefaultValue;  // static @ 0x9
        private System.Boolean deprecated_;  // 0x25
        public static System.Int32 UninterpretedOptionFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.UninterpretedOption> _repeated_uninterpretedOption_codec;  // static @ 0x10
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.UninterpretedOption> uninterpretedOption_;  // 0x28

        // Properties
        Google.Protobuf.ExtensionSet<Google.Protobuf.Reflection.EnumOptions> _Extensions { get; /* RVA: 0x0385B100 */ }
        Google.Protobuf.MessageParser<Google.Protobuf.Reflection.EnumOptions> Parser { get; /* RVA: 0x09B0C6DC */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B0C66C */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B0C97C */ }
        System.Boolean AllowAlias { get; /* RVA: 0x09B0C5C4 */ set; /* RVA: 0x04DA0EE0 */ }
        System.Boolean HasAllowAlias { get; /* RVA: 0x09B0C6CC */ }
        System.Boolean Deprecated { get; /* RVA: 0x09B0C618 */ set; /* RVA: 0x04DA0EF0 */ }
        System.Boolean HasDeprecated { get; /* RVA: 0x09B0C6D4 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.UninterpretedOption> UninterpretedOption { get; /* RVA: 0x04D86240 */ }

        // Methods
        // RVA: 0x09B0C54C  token: 0x600093D
        public System.Void .ctor() { }
        // RVA: 0x09B0C48C  token: 0x600093E
        public System.Void .ctor(Google.Protobuf.Reflection.EnumOptions other) { }
        // RVA: 0x09B0BF80  token: 0x600093F
        public virtual Google.Protobuf.Reflection.EnumOptions Clone() { }
        // RVA: 0x04DA0EC0  token: 0x6000943
        public System.Void ClearAllowAlias() { }
        // RVA: 0x04DA0ED0  token: 0x6000947
        public System.Void ClearDeprecated() { }
        // RVA: 0x09B0BFE0  token: 0x6000949
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09B0C044  token: 0x600094A
        public virtual System.Boolean Equals(Google.Protobuf.Reflection.EnumOptions other) { }
        // RVA: 0x09B0C118  token: 0x600094B
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B0C2B0  token: 0x600094C
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x600094D
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09B0C860  token: 0x600094E
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09B0BEA8  token: 0x600094F
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09B0C1E8  token: 0x6000950
        public virtual System.Void MergeFrom(Google.Protobuf.Reflection.EnumOptions other) { }
        // RVA: 0x04D51840  token: 0x6000951
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09B0C72C  token: 0x6000952
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: -1  // generic def  token: 0x6000953
        public virtual TValue GetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.EnumOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x6000954
        public virtual Google.Protobuf.Collections.RepeatedField<TValue> GetExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.EnumOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x6000955
        public virtual Google.Protobuf.Collections.RepeatedField<TValue> GetOrInitializeExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.EnumOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x6000956
        public virtual System.Void SetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.EnumOptions,TValue> extension, TValue value) { }
        // RVA: -1  // generic def  token: 0x6000957
        public virtual System.Boolean HasExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.EnumOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x6000958
        public virtual System.Void ClearExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.EnumOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x6000959
        public virtual System.Void ClearExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.EnumOptions,TValue> extension) { }
        // RVA: 0x09B0C2FC  token: 0x600095A
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000D7  // size: 0x30
    public sealed class EnumValueOptions : Google.Protobuf.IExtendableMessage`1, Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.EnumValueOptions> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        private Google.Protobuf.ExtensionSet<Google.Protobuf.Reflection.EnumValueOptions> _extensions;  // 0x18
        private System.Int32 _hasBits0;  // 0x20
        public static System.Int32 DeprecatedFieldNumber;  // const
        private static readonly System.Boolean DeprecatedDefaultValue;  // static @ 0x8
        private System.Boolean deprecated_;  // 0x24
        public static System.Int32 UninterpretedOptionFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.UninterpretedOption> _repeated_uninterpretedOption_codec;  // static @ 0x10
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.UninterpretedOption> uninterpretedOption_;  // 0x28

        // Properties
        Google.Protobuf.ExtensionSet<Google.Protobuf.Reflection.EnumValueOptions> _Extensions { get; /* RVA: 0x0385B100 */ }
        Google.Protobuf.MessageParser<Google.Protobuf.Reflection.EnumValueOptions> Parser { get; /* RVA: 0x09B0E30C */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B0E2AC */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B0E568 */ }
        System.Boolean Deprecated { get; /* RVA: 0x09B0E258 */ set; /* RVA: 0x04DA0EE0 */ }
        System.Boolean HasDeprecated { get; /* RVA: 0x09B0C6CC */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.UninterpretedOption> UninterpretedOption { get; /* RVA: 0x04D86240 */ }

        // Methods
        // RVA: 0x09B0E1E0  token: 0x6000962
        public System.Void .ctor() { }
        // RVA: 0x09B0E124  token: 0x6000963
        public System.Void .ctor(Google.Protobuf.Reflection.EnumValueOptions other) { }
        // RVA: 0x09B0DC80  token: 0x6000964
        public virtual Google.Protobuf.Reflection.EnumValueOptions Clone() { }
        // RVA: 0x04DA0EC0  token: 0x6000968
        public System.Void ClearDeprecated() { }
        // RVA: 0x09B0DCE0  token: 0x600096A
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09B0DD44  token: 0x600096B
        public virtual System.Boolean Equals(Google.Protobuf.Reflection.EnumValueOptions other) { }
        // RVA: 0x09B0DDF8  token: 0x600096C
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B0DF54  token: 0x600096D
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x600096E
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09B0E474  token: 0x600096F
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09B0DBB4  token: 0x6000970
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09B0DEA4  token: 0x6000971
        public virtual System.Void MergeFrom(Google.Protobuf.Reflection.EnumValueOptions other) { }
        // RVA: 0x04D51840  token: 0x6000972
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09B0E35C  token: 0x6000973
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: -1  // generic def  token: 0x6000974
        public virtual TValue GetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.EnumValueOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x6000975
        public virtual Google.Protobuf.Collections.RepeatedField<TValue> GetExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.EnumValueOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x6000976
        public virtual Google.Protobuf.Collections.RepeatedField<TValue> GetOrInitializeExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.EnumValueOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x6000977
        public virtual System.Void SetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.EnumValueOptions,TValue> extension, TValue value) { }
        // RVA: -1  // generic def  token: 0x6000978
        public virtual System.Boolean HasExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.EnumValueOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x6000979
        public virtual System.Void ClearExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.EnumValueOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x600097A
        public virtual System.Void ClearExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.EnumValueOptions,TValue> extension) { }
        // RVA: 0x09B0DFA0  token: 0x600097B
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000D9  // size: 0x30
    public sealed class ServiceOptions : Google.Protobuf.IExtendableMessage`1, Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.ServiceOptions> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        private Google.Protobuf.ExtensionSet<Google.Protobuf.Reflection.ServiceOptions> _extensions;  // 0x18
        private System.Int32 _hasBits0;  // 0x20
        public static System.Int32 DeprecatedFieldNumber;  // const
        private static readonly System.Boolean DeprecatedDefaultValue;  // static @ 0x8
        private System.Boolean deprecated_;  // 0x24
        public static System.Int32 UninterpretedOptionFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.UninterpretedOption> _repeated_uninterpretedOption_codec;  // static @ 0x10
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.UninterpretedOption> uninterpretedOption_;  // 0x28

        // Properties
        Google.Protobuf.ExtensionSet<Google.Protobuf.Reflection.ServiceOptions> _Extensions { get; /* RVA: 0x0385B100 */ }
        Google.Protobuf.MessageParser<Google.Protobuf.Reflection.ServiceOptions> Parser { get; /* RVA: 0x09B18C20 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B18BC0 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B18E84 */ }
        System.Boolean Deprecated { get; /* RVA: 0x09B18B6C */ set; /* RVA: 0x04DA0EE0 */ }
        System.Boolean HasDeprecated { get; /* RVA: 0x09B0C6CC */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.UninterpretedOption> UninterpretedOption { get; /* RVA: 0x04D86240 */ }

        // Methods
        // RVA: 0x09B18A38  token: 0x6000983
        public System.Void .ctor() { }
        // RVA: 0x09B18AB0  token: 0x6000984
        public System.Void .ctor(Google.Protobuf.Reflection.ServiceOptions other) { }
        // RVA: 0x09B18594  token: 0x6000985
        public virtual Google.Protobuf.Reflection.ServiceOptions Clone() { }
        // RVA: 0x04DA0EC0  token: 0x6000989
        public System.Void ClearDeprecated() { }
        // RVA: 0x09B186A8  token: 0x600098B
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09B185F4  token: 0x600098C
        public virtual System.Boolean Equals(Google.Protobuf.Reflection.ServiceOptions other) { }
        // RVA: 0x09B1870C  token: 0x600098D
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B18868  token: 0x600098E
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x600098F
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09B18D8C  token: 0x6000990
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09B184B8  token: 0x6000991
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09B187B8  token: 0x6000992
        public virtual System.Void MergeFrom(Google.Protobuf.Reflection.ServiceOptions other) { }
        // RVA: 0x04D51840  token: 0x6000993
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09B18C70  token: 0x6000994
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: -1  // generic def  token: 0x6000995
        public virtual TValue GetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.ServiceOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x6000996
        public virtual Google.Protobuf.Collections.RepeatedField<TValue> GetExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.ServiceOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x6000997
        public virtual Google.Protobuf.Collections.RepeatedField<TValue> GetOrInitializeExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.ServiceOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x6000998
        public virtual System.Void SetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.ServiceOptions,TValue> extension, TValue value) { }
        // RVA: -1  // generic def  token: 0x6000999
        public virtual System.Boolean HasExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.ServiceOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x600099A
        public virtual System.Void ClearExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.ServiceOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x600099B
        public virtual System.Void ClearExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.ServiceOptions,TValue> extension) { }
        // RVA: 0x09B188B4  token: 0x600099C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000DB  // size: 0x38
    public sealed class MethodOptions : Google.Protobuf.IExtendableMessage`1, Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.MethodOptions> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        private Google.Protobuf.ExtensionSet<Google.Protobuf.Reflection.MethodOptions> _extensions;  // 0x18
        private System.Int32 _hasBits0;  // 0x20
        public static System.Int32 DeprecatedFieldNumber;  // const
        private static readonly System.Boolean DeprecatedDefaultValue;  // static @ 0x8
        private System.Boolean deprecated_;  // 0x24
        public static System.Int32 IdempotencyLevelFieldNumber;  // const
        private static readonly Google.Protobuf.Reflection.MethodOptions.Types.IdempotencyLevel IdempotencyLevelDefaultValue;  // static @ 0xc
        private Google.Protobuf.Reflection.MethodOptions.Types.IdempotencyLevel idempotencyLevel_;  // 0x28
        public static System.Int32 UninterpretedOptionFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.UninterpretedOption> _repeated_uninterpretedOption_codec;  // static @ 0x10
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.UninterpretedOption> uninterpretedOption_;  // 0x30

        // Properties
        Google.Protobuf.ExtensionSet<Google.Protobuf.Reflection.MethodOptions> _Extensions { get; /* RVA: 0x0385B100 */ }
        Google.Protobuf.MessageParser<Google.Protobuf.Reflection.MethodOptions> Parser { get; /* RVA: 0x09B16644 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B16590 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B168F4 */ }
        System.Boolean Deprecated { get; /* RVA: 0x09B1653C */ set; /* RVA: 0x04DA0EE0 */ }
        System.Boolean HasDeprecated { get; /* RVA: 0x09B0C6CC */ }
        Google.Protobuf.Reflection.MethodOptions.Types.IdempotencyLevel IdempotencyLevel { get; /* RVA: 0x09B165F0 */ set; /* RVA: 0x04DA10D0 */ }
        System.Boolean HasIdempotencyLevel { get; /* RVA: 0x09B0C6D4 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.UninterpretedOption> UninterpretedOption { get; /* RVA: 0x011F36E0 */ }

        // Methods
        // RVA: 0x09B164C4  token: 0x60009A4
        public System.Void .ctor() { }
        // RVA: 0x09B16404  token: 0x60009A5
        public System.Void .ctor(Google.Protobuf.Reflection.MethodOptions other) { }
        // RVA: 0x09B15F08  token: 0x60009A6
        public virtual Google.Protobuf.Reflection.MethodOptions Clone() { }
        // RVA: 0x04DA0EC0  token: 0x60009AA
        public System.Void ClearDeprecated() { }
        // RVA: 0x04DA0ED0  token: 0x60009AE
        public System.Void ClearIdempotencyLevel() { }
        // RVA: 0x09B15F68  token: 0x60009B0
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09B15FCC  token: 0x60009B1
        public virtual System.Boolean Equals(Google.Protobuf.Reflection.MethodOptions other) { }
        // RVA: 0x09B160A0  token: 0x60009B2
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B16224  token: 0x60009B3
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x60009B4
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09B167D0  token: 0x60009B5
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09B15DF4  token: 0x60009B6
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09B1615C  token: 0x60009B7
        public virtual System.Void MergeFrom(Google.Protobuf.Reflection.MethodOptions other) { }
        // RVA: 0x04D51840  token: 0x60009B8
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09B16694  token: 0x60009B9
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: -1  // generic def  token: 0x60009BA
        public virtual TValue GetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.MethodOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x60009BB
        public virtual Google.Protobuf.Collections.RepeatedField<TValue> GetExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.MethodOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x60009BC
        public virtual Google.Protobuf.Collections.RepeatedField<TValue> GetOrInitializeExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.MethodOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x60009BD
        public virtual System.Void SetExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.MethodOptions,TValue> extension, TValue value) { }
        // RVA: -1  // generic def  token: 0x60009BE
        public virtual System.Boolean HasExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.MethodOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x60009BF
        public virtual System.Void ClearExtension(Google.Protobuf.Extension<Google.Protobuf.Reflection.MethodOptions,TValue> extension) { }
        // RVA: -1  // generic def  token: 0x60009C0
        public virtual System.Void ClearExtension(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.MethodOptions,TValue> extension) { }
        // RVA: 0x09B16270  token: 0x60009C1
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000DF  // size: 0x58
    public sealed class UninterpretedOption : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.UninterpretedOption> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        private System.Int32 _hasBits0;  // 0x18
        public static System.Int32 NameFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.UninterpretedOption.Types.NamePart> _repeated_name_codec;  // static @ 0x8
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.UninterpretedOption.Types.NamePart> name_;  // 0x20
        public static System.Int32 IdentifierValueFieldNumber;  // const
        private static readonly System.String IdentifierValueDefaultValue;  // static @ 0x10
        private System.String identifierValue_;  // 0x28
        public static System.Int32 PositiveIntValueFieldNumber;  // const
        private static readonly System.UInt64 PositiveIntValueDefaultValue;  // static @ 0x18
        private System.UInt64 positiveIntValue_;  // 0x30
        public static System.Int32 NegativeIntValueFieldNumber;  // const
        private static readonly System.Int64 NegativeIntValueDefaultValue;  // static @ 0x20
        private System.Int64 negativeIntValue_;  // 0x38
        public static System.Int32 DoubleValueFieldNumber;  // const
        private static readonly System.Double DoubleValueDefaultValue;  // static @ 0x28
        private System.Double doubleValue_;  // 0x40
        public static System.Int32 StringValueFieldNumber;  // const
        private static readonly Google.Protobuf.ByteString StringValueDefaultValue;  // static @ 0x30
        private Google.Protobuf.ByteString stringValue_;  // 0x48
        public static System.Int32 AggregateValueFieldNumber;  // const
        private static readonly System.String AggregateValueDefaultValue;  // static @ 0x38
        private System.String aggregateValue_;  // 0x50

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.Reflection.UninterpretedOption> Parser { get; /* RVA: 0x09B1B0C8 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B1AF14 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B1B540 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.UninterpretedOption.Types.NamePart> Name { get; /* RVA: 0x04D862C0 */ }
        System.String IdentifierValue { get; /* RVA: 0x09B1B01C */ set; /* RVA: 0x09B1B5E8 */ }
        System.Boolean HasIdentifierValue { get; /* RVA: 0x068C60A8 */ }
        System.UInt64 PositiveIntValue { get; /* RVA: 0x09B1B118 */ set; /* RVA: 0x04DA1110 */ }
        System.Boolean HasPositiveIntValue { get; /* RVA: 0x0314ECE0 */ }
        System.Int64 NegativeIntValue { get; /* RVA: 0x09B1B070 */ set; /* RVA: 0x04DA1100 */ }
        System.Boolean HasNegativeIntValue { get; /* RVA: 0x09AFF0A8 */ }
        System.Double DoubleValue { get; /* RVA: 0x09B1AF74 */ set; /* RVA: 0x04DA10F0 */ }
        System.Boolean HasDoubleValue { get; /* RVA: 0x09B00098 */ }
        Google.Protobuf.ByteString StringValue { get; /* RVA: 0x09B1B170 */ set; /* RVA: 0x09B1B650 */ }
        System.Boolean HasStringValue { get; /* RVA: 0x09B1AFCC */ }
        System.String AggregateValue { get; /* RVA: 0x09B1AEC0 */ set; /* RVA: 0x09B1B580 */ }
        System.Boolean HasAggregateValue { get; /* RVA: 0x084774C8 */ }

        // Methods
        // RVA: 0x09B1AE48  token: 0x60009C8
        public System.Void .ctor() { }
        // RVA: 0x09B1AD68  token: 0x60009C9
        public System.Void .ctor(Google.Protobuf.Reflection.UninterpretedOption other) { }
        // RVA: 0x09B1A588  token: 0x60009CA
        public virtual Google.Protobuf.Reflection.UninterpretedOption Clone() { }
        // RVA: 0x09849400  token: 0x60009CF
        public System.Void ClearIdentifierValue() { }
        // RVA: 0x04D86DE0  token: 0x60009D3
        public System.Void ClearPositiveIntValue() { }
        // RVA: 0x04DA0DF0  token: 0x60009D7
        public System.Void ClearNegativeIntValue() { }
        // RVA: 0x04DA0E40  token: 0x60009DB
        public System.Void ClearDoubleValue() { }
        // RVA: 0x070A7CA8  token: 0x60009DF
        public System.Void ClearStringValue() { }
        // RVA: 0x0709F140  token: 0x60009E3
        public System.Void ClearAggregateValue() { }
        // RVA: 0x09B1A5E8  token: 0x60009E4
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09B1A64C  token: 0x60009E5
        public virtual System.Boolean Equals(Google.Protobuf.Reflection.UninterpretedOption other) { }
        // RVA: 0x09B1A824  token: 0x60009E6
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B1AAC0  token: 0x60009E7
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x60009E8
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09B1B38C  token: 0x60009E9
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09B1A3D8  token: 0x60009EA
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09B1A994  token: 0x60009EB
        public virtual System.Void MergeFrom(Google.Protobuf.Reflection.UninterpretedOption other) { }
        // RVA: 0x04D51840  token: 0x60009EC
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09B1B1C4  token: 0x60009ED
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09B1AB0C  token: 0x60009EE
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000E4  // size: 0x20
    public sealed class SourceCodeInfo : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.SourceCodeInfo> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 LocationFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.SourceCodeInfo.Types.Location> _repeated_location_codec;  // static @ 0x8
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.SourceCodeInfo.Types.Location> location_;  // 0x18

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.Reflection.SourceCodeInfo> Parser { get; /* RVA: 0x09B1942C */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B193CC */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B195F0 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.SourceCodeInfo.Types.Location> Location { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x09B192CC  token: 0x6000A11
        public System.Void .ctor() { }
        // RVA: 0x09B19344  token: 0x6000A12
        public System.Void .ctor(Google.Protobuf.Reflection.SourceCodeInfo other) { }
        // RVA: 0x09B18F58  token: 0x6000A13
        public virtual Google.Protobuf.Reflection.SourceCodeInfo Clone() { }
        // RVA: 0x09B19034  token: 0x6000A15
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09B18FB8  token: 0x6000A16
        public virtual System.Boolean Equals(Google.Protobuf.Reflection.SourceCodeInfo other) { }
        // RVA: 0x0638F5B8  token: 0x6000A17
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B19110  token: 0x6000A18
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x6000A19
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09B1954C  token: 0x6000A1A
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09B18EC4  token: 0x6000A1B
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09B19098  token: 0x6000A1C
        public virtual System.Void MergeFrom(Google.Protobuf.Reflection.SourceCodeInfo other) { }
        // RVA: 0x04D51840  token: 0x6000A1D
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09B1947C  token: 0x6000A1E
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09B1915C  token: 0x6000A1F
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000E9  // size: 0x20
    public sealed class GeneratedCodeInfo : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.Reflection.GeneratedCodeInfo> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 AnnotationFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.Reflection.GeneratedCodeInfo.Types.Annotation> _repeated_annotation_codec;  // static @ 0x8
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.GeneratedCodeInfo.Types.Annotation> annotation_;  // 0x18

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.Reflection.GeneratedCodeInfo> Parser { get; /* RVA: 0x09B130D4 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B13074 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B13298 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.Reflection.GeneratedCodeInfo.Types.Annotation> Annotation { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x09B12FFC  token: 0x6000A45
        public System.Void .ctor() { }
        // RVA: 0x09B12F74  token: 0x6000A46
        public System.Void .ctor(Google.Protobuf.Reflection.GeneratedCodeInfo other) { }
        // RVA: 0x09B12C00  token: 0x6000A47
        public virtual Google.Protobuf.Reflection.GeneratedCodeInfo Clone() { }
        // RVA: 0x09B12C60  token: 0x6000A49
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09B12CC4  token: 0x6000A4A
        public virtual System.Boolean Equals(Google.Protobuf.Reflection.GeneratedCodeInfo other) { }
        // RVA: 0x0638F5B8  token: 0x6000A4B
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B12DB8  token: 0x6000A4C
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x6000A4D
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09B131F4  token: 0x6000A4E
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09B12B6C  token: 0x6000A4F
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09B12D40  token: 0x6000A50
        public virtual System.Void MergeFrom(Google.Protobuf.Reflection.GeneratedCodeInfo other) { }
        // RVA: 0x04D51840  token: 0x6000A51
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09B13124  token: 0x6000A52
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09B12E04  token: 0x6000A53
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000EE  // size: 0x28
    public abstract class DescriptorBase : Google.Protobuf.Reflection.IDescriptor
    {
        // Fields
        private readonly System.Int32 <Index>k__BackingField;  // 0x10
        private readonly System.String <FullName>k__BackingField;  // 0x18
        private readonly Google.Protobuf.Reflection.FileDescriptor <File>k__BackingField;  // 0x20

        // Properties
        System.Int32 Index { get; /* RVA: 0x02B2E2D0 */ }
        System.String Name { get; /* RVA: -1  // abstract */ }
        System.String FullName { get; /* RVA: 0x0385B100 */ }
        Google.Protobuf.Reflection.FileDescriptor File { get; /* RVA: 0x04D862C0 */ }
        Google.Protobuf.Reflection.DescriptorDeclaration Declaration { get; /* RVA: 0x09B0A2D0 */ }

        // Methods
        // RVA: 0x09B0A2A0  token: 0x6000A78
        private System.Void .ctor(Google.Protobuf.Reflection.FileDescriptor file, System.String fullName, System.Int32 index) { }
        // RVA: 0x011EC580  token: 0x6000A7E
        private virtual System.Collections.Generic.IReadOnlyList<Google.Protobuf.Reflection.DescriptorBase> GetNestedDescriptorListForField(System.Int32 fieldNumber) { }

    }

    // TypeToken: 0x20000EF  // size: 0x40
    public sealed class DescriptorDeclaration
    {
        // Fields
        private readonly Google.Protobuf.Reflection.IDescriptor <Descriptor>k__BackingField;  // 0x10
        private readonly System.Int32 <StartLine>k__BackingField;  // 0x18
        private readonly System.Int32 <StartColumn>k__BackingField;  // 0x1c
        private readonly System.Int32 <EndLine>k__BackingField;  // 0x20
        private readonly System.Int32 <EndColumn>k__BackingField;  // 0x24
        private readonly System.String <LeadingComments>k__BackingField;  // 0x28
        private readonly System.String <TrailingComments>k__BackingField;  // 0x30
        private readonly System.Collections.Generic.IReadOnlyList<System.String> <LeadingDetachedComments>k__BackingField;  // 0x38

        // Properties
        Google.Protobuf.Reflection.IDescriptor Descriptor { get; /* RVA: 0x02B2ECC0 */ }
        System.Int32 StartLine { get; /* RVA: 0x011EF5B0 */ }
        System.Int32 StartColumn { get; /* RVA: 0x04D86310 */ }
        System.Int32 EndLine { get; /* RVA: 0x02B70F90 */ }
        System.Int32 EndColumn { get; /* RVA: 0x011F0020 */ }
        System.String LeadingComments { get; /* RVA: 0x04D86240 */ }
        System.String TrailingComments { get; /* RVA: 0x011F36E0 */ }
        System.Collections.Generic.IReadOnlyList<System.String> LeadingDetachedComments { get; /* RVA: 0x04D85A50 */ }

        // Methods
        // RVA: 0x09B0A368  token: 0x6000A87
        private System.Void .ctor(Google.Protobuf.Reflection.IDescriptor descriptor, Google.Protobuf.Reflection.SourceCodeInfo.Types.Location location) { }
        // RVA: 0x09B0A2F8  token: 0x6000A88
        private static Google.Protobuf.Reflection.DescriptorDeclaration FromProto(Google.Protobuf.Reflection.IDescriptor descriptor, Google.Protobuf.Reflection.SourceCodeInfo.Types.Location location) { }

    }

    // TypeToken: 0x20000F0  // size: 0x30
    public sealed class DescriptorPool
    {
        // Fields
        private readonly System.Collections.Generic.IDictionary<System.String,Google.Protobuf.Reflection.IDescriptor> descriptorsByName;  // 0x10
        private readonly System.Collections.Generic.IDictionary<Google.Protobuf.ObjectIntPair<Google.Protobuf.Reflection.IDescriptor>,Google.Protobuf.Reflection.FieldDescriptor> fieldsByNumber;  // 0x18
        private readonly System.Collections.Generic.IDictionary<Google.Protobuf.ObjectIntPair<Google.Protobuf.Reflection.IDescriptor>,Google.Protobuf.Reflection.EnumValueDescriptor> enumValuesByNumber;  // 0x20
        private readonly System.Collections.Generic.HashSet<Google.Protobuf.Reflection.FileDescriptor> dependencies;  // 0x28
        private static readonly System.Text.RegularExpressions.Regex ValidationRegex;  // static @ 0x0

        // Methods
        // RVA: 0x09B0B538  token: 0x6000A89
        private System.Void .ctor(System.Collections.Generic.IEnumerable<Google.Protobuf.Reflection.FileDescriptor> dependencyFiles) { }
        // RVA: 0x09B0AEB0  token: 0x6000A8A
        private System.Void ImportPublicDependencies(Google.Protobuf.Reflection.FileDescriptor file) { }
        // RVA: -1  // generic def  token: 0x6000A8B
        private T FindSymbol(System.String fullName) { }
        // RVA: 0x09B0A804  token: 0x6000A8C
        private System.Void AddPackage(System.String fullName, Google.Protobuf.Reflection.FileDescriptor file) { }
        // RVA: 0x09B0AA6C  token: 0x6000A8D
        private System.Void AddSymbol(Google.Protobuf.Reflection.IDescriptor descriptor) { }
        // RVA: 0x09B0B2E4  token: 0x6000A8E
        private static System.Void ValidateSymbolName(Google.Protobuf.Reflection.IDescriptor descriptor) { }
        // RVA: 0x09B0AE0C  token: 0x6000A8F
        private Google.Protobuf.Reflection.FieldDescriptor FindFieldByNumber(Google.Protobuf.Reflection.MessageDescriptor messageDescriptor, System.Int32 number) { }
        // RVA: 0x09B0AD68  token: 0x6000A90
        private Google.Protobuf.Reflection.EnumValueDescriptor FindEnumValueByNumber(Google.Protobuf.Reflection.EnumDescriptor enumDescriptor, System.Int32 number) { }
        // RVA: 0x09B0A5CC  token: 0x6000A91
        private System.Void AddFieldByNumber(Google.Protobuf.Reflection.FieldDescriptor field) { }
        // RVA: 0x09B0A508  token: 0x6000A92
        private System.Void AddEnumValueByNumber(Google.Protobuf.Reflection.EnumValueDescriptor enumValue) { }
        // RVA: 0x09B0B024  token: 0x6000A93
        private Google.Protobuf.Reflection.IDescriptor LookupSymbol(System.String name, Google.Protobuf.Reflection.IDescriptor relativeTo) { }
        // RVA: 0x09B0B474  token: 0x6000A94
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000F1  // size: 0x10
    public static class DescriptorUtil
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000A95
        private static System.Collections.Generic.IList<TOutput> ConvertAndMakeReadOnly(System.Collections.Generic.IList<TInput> input, Google.Protobuf.Reflection.DescriptorUtil.IndexedConverter<TInput,TOutput> converter) { }

    }

    // TypeToken: 0x20000F3  // size: 0xA0
    public sealed class DescriptorValidationException : System.Exception
    {
        // Fields
        private readonly System.String name;  // 0x90
        private readonly System.String description;  // 0x98

        // Properties
        System.String ProblemSymbolName { get; /* RVA: 0x04D86220 */ }
        System.String Description { get; /* RVA: 0x04D85EE0 */ }

        // Methods
        // RVA: 0x09B0B8E8  token: 0x6000A9C
        private System.Void .ctor(Google.Protobuf.Reflection.IDescriptor problemDescriptor, System.String description) { }
        // RVA: 0x09B0B9B4  token: 0x6000A9D
        private System.Void .ctor(Google.Protobuf.Reflection.IDescriptor problemDescriptor, System.String description, System.Exception cause) { }

    }

    // TypeToken: 0x20000F4  // size: 0x48
    public sealed class EnumDescriptor : Google.Protobuf.Reflection.DescriptorBase
    {
        // Fields
        private readonly Google.Protobuf.Reflection.EnumDescriptorProto proto;  // 0x28
        private readonly Google.Protobuf.Reflection.MessageDescriptor containingType;  // 0x30
        private readonly System.Collections.Generic.IList<Google.Protobuf.Reflection.EnumValueDescriptor> values;  // 0x38
        private readonly System.Type clrType;  // 0x40

        // Properties
        Google.Protobuf.Reflection.EnumDescriptorProto Proto { get; /* RVA: 0x04D86240 */ }
        System.String Name { get; /* RVA: 0x09B0BE88 */ }
        System.Type ClrType { get; /* RVA: 0x04D85A60 */ }
        Google.Protobuf.Reflection.MessageDescriptor ContainingType { get; /* RVA: 0x011F36E0 */ }
        System.Collections.Generic.IList<Google.Protobuf.Reflection.EnumValueDescriptor> Values { get; /* RVA: 0x04D85A50 */ }
        Google.Protobuf.Reflection.CustomOptions CustomOptions { get; /* RVA: 0x09B0BDF8 */ }

        // Methods
        // RVA: 0x09B0BBEC  token: 0x6000A9E
        private System.Void .ctor(Google.Protobuf.Reflection.EnumDescriptorProto proto, Google.Protobuf.Reflection.FileDescriptor file, Google.Protobuf.Reflection.MessageDescriptor parent, System.Int32 index, System.Type clrType) { }
        // RVA: 0x09B0BBCC  token: 0x6000AA0
        public Google.Protobuf.Reflection.EnumDescriptorProto ToProto() { }
        // RVA: 0x09B0BB50  token: 0x6000AA2
        private virtual System.Collections.Generic.IReadOnlyList<Google.Protobuf.Reflection.DescriptorBase> GetNestedDescriptorListForField(System.Int32 fieldNumber) { }
        // RVA: 0x09B0BB20  token: 0x6000AA6
        public Google.Protobuf.Reflection.EnumValueDescriptor FindValueByNumber(System.Int32 number) { }
        // RVA: 0x09B0BA94  token: 0x6000AA7
        public Google.Protobuf.Reflection.EnumValueDescriptor FindValueByName(System.String name) { }
        // RVA: 0x09B0BBA0  token: 0x6000AA9
        public Google.Protobuf.Reflection.EnumOptions GetOptions() { }
        // RVA: -1  // generic def  token: 0x6000AAA
        public T GetOption(Google.Protobuf.Extension<Google.Protobuf.Reflection.EnumOptions,T> extension) { }
        // RVA: -1  // generic def  token: 0x6000AAB
        public Google.Protobuf.Collections.RepeatedField<T> GetOption(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.EnumOptions,T> extension) { }

    }

    // TypeToken: 0x20000F6  // size: 0x38
    public sealed class EnumValueDescriptor : Google.Protobuf.Reflection.DescriptorBase
    {
        // Fields
        private readonly Google.Protobuf.Reflection.EnumDescriptor enumDescriptor;  // 0x28
        private readonly Google.Protobuf.Reflection.EnumValueDescriptorProto proto;  // 0x30

        // Properties
        Google.Protobuf.Reflection.EnumValueDescriptorProto Proto { get; /* RVA: 0x011F36E0 */ }
        System.String Name { get; /* RVA: 0x09B0DB74 */ }
        System.Int32 Number { get; /* RVA: 0x09B0DB94 */ }
        Google.Protobuf.Reflection.EnumDescriptor EnumDescriptor { get; /* RVA: 0x04D86240 */ }
        Google.Protobuf.Reflection.CustomOptions CustomOptions { get; /* RVA: 0x09B0DAE4 */ }

        // Methods
        // RVA: 0x09B0D9EC  token: 0x6000AAE
        private System.Void .ctor(Google.Protobuf.Reflection.EnumValueDescriptorProto proto, Google.Protobuf.Reflection.FileDescriptor file, Google.Protobuf.Reflection.EnumDescriptor parent, System.Int32 index) { }
        // RVA: 0x09B0D9CC  token: 0x6000AB0
        public Google.Protobuf.Reflection.EnumValueDescriptorProto ToProto() { }
        // RVA: 0x09B0D9A0  token: 0x6000AB5
        public Google.Protobuf.Reflection.EnumValueOptions GetOptions() { }
        // RVA: -1  // generic def  token: 0x6000AB6
        public T GetOption(Google.Protobuf.Extension<Google.Protobuf.Reflection.EnumValueOptions,T> extension) { }
        // RVA: -1  // generic def  token: 0x6000AB7
        public Google.Protobuf.Collections.RepeatedField<T> GetOption(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.EnumValueOptions,T> extension) { }

    }

    // TypeToken: 0x20000F7  // size: 0x28
    public sealed class ExtensionAccessor : Google.Protobuf.Reflection.IFieldAccessor
    {
        // Fields
        private readonly Google.Protobuf.Extension extension;  // 0x10
        private readonly Google.Protobuf.Reflection.ReflectionUtil.IExtensionReflectionHelper helper;  // 0x18
        private readonly Google.Protobuf.Reflection.FieldDescriptor <Descriptor>k__BackingField;  // 0x20

        // Properties
        Google.Protobuf.Reflection.FieldDescriptor Descriptor { get; /* RVA: 0x04D862C0 */ }

        // Methods
        // RVA: 0x09B0E770  token: 0x6000AB8
        private System.Void .ctor(Google.Protobuf.Reflection.FieldDescriptor descriptor) { }
        // RVA: 0x09B0E5A8  token: 0x6000ABA
        public virtual System.Void Clear(Google.Protobuf.IMessage message) { }
        // RVA: 0x09B0E680  token: 0x6000ABB
        public virtual System.Boolean HasValue(Google.Protobuf.IMessage message) { }
        // RVA: 0x09B0E614  token: 0x6000ABC
        public virtual System.Object GetValue(Google.Protobuf.IMessage message) { }
        // RVA: 0x09B0E6EC  token: 0x6000ABD
        public virtual System.Void SetValue(Google.Protobuf.IMessage message, System.Object value) { }

    }

    // TypeToken: 0x20000F8  // size: 0x28
    public sealed class ExtensionCollection
    {
        // Fields
        private System.Collections.Generic.IDictionary<Google.Protobuf.Reflection.MessageDescriptor,System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor>> extensionsByTypeInDeclarationOrder;  // 0x10
        private System.Collections.Generic.IDictionary<Google.Protobuf.Reflection.MessageDescriptor,System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor>> extensionsByTypeInNumberOrder;  // 0x18
        private readonly System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor> <UnorderedExtensions>k__BackingField;  // 0x20

        // Properties
        System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor> UnorderedExtensions { get; /* RVA: 0x04D862C0 */ }

        // Methods
        // RVA: 0x09B0F0D0  token: 0x6000ABE
        private System.Void .ctor(Google.Protobuf.Reflection.FileDescriptor file, Google.Protobuf.Extension[] extensions) { }
        // RVA: 0x09B0EFC8  token: 0x6000ABF
        private System.Void .ctor(Google.Protobuf.Reflection.MessageDescriptor message, Google.Protobuf.Extension[] extensions) { }
        // RVA: 0x09B0EF28  token: 0x6000AC1
        public System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor> GetExtensionsInDeclarationOrder(Google.Protobuf.Reflection.MessageDescriptor descriptor) { }
        // RVA: 0x09B0EF78  token: 0x6000AC2
        public System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor> GetExtensionsInNumberOrder(Google.Protobuf.Reflection.MessageDescriptor descriptor) { }
        // RVA: 0x09B0E7F8  token: 0x6000AC3
        private System.Void CrossLink() { }

    }

    // TypeToken: 0x20000FC  // size: 0x20
    public abstract class FieldAccessorBase : Google.Protobuf.Reflection.IFieldAccessor
    {
        // Fields
        private readonly System.Func<Google.Protobuf.IMessage,System.Object> getValueDelegate;  // 0x10
        private readonly Google.Protobuf.Reflection.FieldDescriptor descriptor;  // 0x18

        // Properties
        Google.Protobuf.Reflection.FieldDescriptor Descriptor { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x09B1C07C  token: 0x6000ACF
        private System.Void .ctor(System.Reflection.PropertyInfo property, Google.Protobuf.Reflection.FieldDescriptor descriptor) { }
        // RVA: 0x09B1C05C  token: 0x6000AD1
        public virtual System.Object GetValue(Google.Protobuf.IMessage message) { }
        // RVA: -1  // abstract  token: 0x6000AD2
        public virtual System.Boolean HasValue(Google.Protobuf.IMessage message) { }
        // RVA: -1  // abstract  token: 0x6000AD3
        public virtual System.Void Clear(Google.Protobuf.IMessage message) { }
        // RVA: -1  // abstract  token: 0x6000AD4
        public virtual System.Void SetValue(Google.Protobuf.IMessage message, System.Object value) { }

    }

    // TypeToken: 0x20000FD  // size: 0x80
    public sealed class FieldDescriptor : Google.Protobuf.Reflection.DescriptorBase, System.IComparable`1
    {
        // Fields
        private Google.Protobuf.Reflection.EnumDescriptor enumType;  // 0x28
        private Google.Protobuf.Reflection.MessageDescriptor extendeeType;  // 0x30
        private Google.Protobuf.Reflection.MessageDescriptor messageType;  // 0x38
        private Google.Protobuf.Reflection.FieldType fieldType;  // 0x40
        private Google.Protobuf.Reflection.IFieldAccessor accessor;  // 0x48
        private readonly Google.Protobuf.Reflection.MessageDescriptor <ContainingType>k__BackingField;  // 0x50
        private readonly Google.Protobuf.Reflection.OneofDescriptor <ContainingOneof>k__BackingField;  // 0x58
        private readonly System.String <JsonName>k__BackingField;  // 0x60
        private readonly System.String <PropertyName>k__BackingField;  // 0x68
        private readonly Google.Protobuf.Reflection.FieldDescriptorProto <Proto>k__BackingField;  // 0x70
        private readonly Google.Protobuf.Extension <Extension>k__BackingField;  // 0x78

        // Properties
        Google.Protobuf.Reflection.MessageDescriptor ContainingType { get; /* RVA: 0x04D86270 */ }
        Google.Protobuf.Reflection.OneofDescriptor ContainingOneof { get; /* RVA: 0x02E56440 */ }
        Google.Protobuf.Reflection.OneofDescriptor RealContainingOneof { get; /* RVA: 0x09B1D2A0 */ }
        System.String JsonName { get; /* RVA: 0x04D86280 */ }
        System.String PropertyName { get; /* RVA: 0x04D86230 */ }
        System.Boolean HasPresence { get; /* RVA: 0x09B1D098 */ }
        Google.Protobuf.Reflection.FieldDescriptorProto Proto { get; /* RVA: 0x04D862B0 */ }
        Google.Protobuf.Extension Extension { get; /* RVA: 0x04D862A0 */ }
        System.String Name { get; /* RVA: 0x09B1D280 */ }
        Google.Protobuf.Reflection.IFieldAccessor Accessor { get; /* RVA: 0x04D86200 */ }
        System.Boolean IsRepeated { get; /* RVA: 0x09B1D1C0 */ }
        System.Boolean IsRequired { get; /* RVA: 0x09B1D1E8 */ }
        System.Boolean IsMap { get; /* RVA: 0x09B1D124 */ }
        System.Boolean IsPacked { get; /* RVA: 0x09B1D164 */ }
        System.Boolean IsExtension { get; /* RVA: 0x09B1D104 */ }
        Google.Protobuf.Reflection.FieldType FieldType { get; /* RVA: 0x04D865E0 */ }
        System.Int32 FieldNumber { get; /* RVA: 0x09B1D078 */ }
        Google.Protobuf.Reflection.EnumDescriptor EnumType { get; /* RVA: 0x09B1CF98 */ }
        Google.Protobuf.Reflection.MessageDescriptor MessageType { get; /* RVA: 0x09B1D210 */ }
        Google.Protobuf.Reflection.MessageDescriptor ExtendeeType { get; /* RVA: 0x09B1D004 */ }
        Google.Protobuf.Reflection.CustomOptions CustomOptions { get; /* RVA: 0x09B1CF08 */ }

        // Methods
        // RVA: 0x09B1CBD0  token: 0x6000ADC
        public Google.Protobuf.Reflection.FieldDescriptorProto ToProto() { }
        // RVA: 0x09B1CBF0  token: 0x6000ADE
        private System.Void .ctor(Google.Protobuf.Reflection.FieldDescriptorProto proto, Google.Protobuf.Reflection.FileDescriptor file, Google.Protobuf.Reflection.MessageDescriptor parent, System.Int32 index, System.String propertyName, Google.Protobuf.Extension extension) { }
        // RVA: 0x09B1CA3C  token: 0x6000AE1
        private static Google.Protobuf.Reflection.FieldType GetFieldTypeFromProtoType(Google.Protobuf.Reflection.FieldDescriptorProto.Types.Type type) { }
        // RVA: 0x09B1C230  token: 0x6000AE9
        public virtual System.Int32 CompareTo(Google.Protobuf.Reflection.FieldDescriptor other) { }
        // RVA: 0x09B1CBA4  token: 0x6000AEE
        public Google.Protobuf.Reflection.FieldOptions GetOptions() { }
        // RVA: -1  // generic def  token: 0x6000AEF
        public T GetOption(Google.Protobuf.Extension<Google.Protobuf.Reflection.FieldOptions,T> extension) { }
        // RVA: -1  // generic def  token: 0x6000AF0
        public Google.Protobuf.Collections.RepeatedField<T> GetOption(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.FieldOptions,T> extension) { }
        // RVA: 0x09B1C4C8  token: 0x6000AF1
        private System.Void CrossLink() { }
        // RVA: 0x09B1C2C4  token: 0x6000AF2
        private Google.Protobuf.Reflection.IFieldAccessor CreateAccessor() { }

    }

    // TypeToken: 0x20000FE  // size: 0x14
    public sealed struct FieldType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Google.Protobuf.Reflection.FieldType Double;  // const
        public static Google.Protobuf.Reflection.FieldType Float;  // const
        public static Google.Protobuf.Reflection.FieldType Int64;  // const
        public static Google.Protobuf.Reflection.FieldType UInt64;  // const
        public static Google.Protobuf.Reflection.FieldType Int32;  // const
        public static Google.Protobuf.Reflection.FieldType Fixed64;  // const
        public static Google.Protobuf.Reflection.FieldType Fixed32;  // const
        public static Google.Protobuf.Reflection.FieldType Bool;  // const
        public static Google.Protobuf.Reflection.FieldType String;  // const
        public static Google.Protobuf.Reflection.FieldType Group;  // const
        public static Google.Protobuf.Reflection.FieldType Message;  // const
        public static Google.Protobuf.Reflection.FieldType Bytes;  // const
        public static Google.Protobuf.Reflection.FieldType UInt32;  // const
        public static Google.Protobuf.Reflection.FieldType SFixed32;  // const
        public static Google.Protobuf.Reflection.FieldType SFixed64;  // const
        public static Google.Protobuf.Reflection.FieldType SInt32;  // const
        public static Google.Protobuf.Reflection.FieldType SInt64;  // const
        public static Google.Protobuf.Reflection.FieldType Enum;  // const

    }

    // TypeToken: 0x20000FF  // size: 0x14
    public sealed struct Syntax
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Google.Protobuf.Reflection.Syntax Proto2;  // const
        public static Google.Protobuf.Reflection.Syntax Proto3;  // const
        public static Google.Protobuf.Reflection.Syntax Unknown;  // const

    }

    // TypeToken: 0x2000100  // size: 0x68
    public sealed class FileDescriptor : Google.Protobuf.Reflection.IDescriptor
    {
        // Fields
        private readonly System.Lazy<System.Collections.Generic.Dictionary<Google.Protobuf.Reflection.IDescriptor,Google.Protobuf.Reflection.DescriptorDeclaration>> declarations;  // 0x10
        private readonly Google.Protobuf.Reflection.FileDescriptorProto <Proto>k__BackingField;  // 0x18
        private readonly Google.Protobuf.Reflection.Syntax <Syntax>k__BackingField;  // 0x20
        private readonly System.Collections.Generic.IList<Google.Protobuf.Reflection.MessageDescriptor> <MessageTypes>k__BackingField;  // 0x28
        private readonly System.Collections.Generic.IList<Google.Protobuf.Reflection.EnumDescriptor> <EnumTypes>k__BackingField;  // 0x30
        private readonly System.Collections.Generic.IList<Google.Protobuf.Reflection.ServiceDescriptor> <Services>k__BackingField;  // 0x38
        private readonly Google.Protobuf.Reflection.ExtensionCollection <Extensions>k__BackingField;  // 0x40
        private readonly System.Collections.Generic.IList<Google.Protobuf.Reflection.FileDescriptor> <Dependencies>k__BackingField;  // 0x48
        private readonly System.Collections.Generic.IList<Google.Protobuf.Reflection.FileDescriptor> <PublicDependencies>k__BackingField;  // 0x50
        private readonly Google.Protobuf.ByteString <SerializedData>k__BackingField;  // 0x58
        private readonly Google.Protobuf.Reflection.DescriptorPool <DescriptorPool>k__BackingField;  // 0x60

        // Properties
        Google.Protobuf.Reflection.FileDescriptorProto Proto { get; /* RVA: 0x0385B100 */ }
        Google.Protobuf.Reflection.Syntax Syntax { get; /* RVA: 0x02B70F90 */ }
        System.String Name { get; /* RVA: 0x09B1F874 */ }
        System.String Package { get; /* RVA: 0x09B1F894 */ }
        System.Collections.Generic.IList<Google.Protobuf.Reflection.MessageDescriptor> MessageTypes { get; /* RVA: 0x04D86240 */ }
        System.Collections.Generic.IList<Google.Protobuf.Reflection.EnumDescriptor> EnumTypes { get; /* RVA: 0x011F36E0 */ }
        System.Collections.Generic.IList<Google.Protobuf.Reflection.ServiceDescriptor> Services { get; /* RVA: 0x04D85A50 */ }
        Google.Protobuf.Reflection.ExtensionCollection Extensions { get; /* RVA: 0x04D85A60 */ }
        System.Collections.Generic.IList<Google.Protobuf.Reflection.FileDescriptor> Dependencies { get; /* RVA: 0x04D86200 */ }
        System.Collections.Generic.IList<Google.Protobuf.Reflection.FileDescriptor> PublicDependencies { get; /* RVA: 0x04D86270 */ }
        Google.Protobuf.ByteString SerializedData { get; /* RVA: 0x02E56440 */ }
        System.String Google.Protobuf.Reflection.IDescriptor.FullName { get; /* RVA: 0x09B1F248 */ }
        Google.Protobuf.Reflection.FileDescriptor Google.Protobuf.Reflection.IDescriptor.File { get; /* RVA: 0x0464E630 */ }
        Google.Protobuf.Reflection.DescriptorPool DescriptorPool { get; /* RVA: 0x04D86280 */ }
        Google.Protobuf.Reflection.FileDescriptor DescriptorProtoFileDescriptor { get; /* RVA: 0x09B1F834 */ }
        Google.Protobuf.Reflection.CustomOptions CustomOptions { get; /* RVA: 0x09B1F7A4 */ }

        // Methods
        // RVA: 0x09B1F2B8  token: 0x6000AF3
        private static System.Void .cctor() { }
        // RVA: 0x09B1F348  token: 0x6000AF4
        private System.Void .ctor(Google.Protobuf.ByteString descriptorData, Google.Protobuf.Reflection.FileDescriptorProto proto, System.Collections.Generic.IEnumerable<Google.Protobuf.Reflection.FileDescriptor> dependencies, Google.Protobuf.Reflection.DescriptorPool pool, System.Boolean allowUnknownDependencies, Google.Protobuf.Reflection.GeneratedClrTypeInfo generatedCodeInfo) { }
        // RVA: 0x09B1DBD8  token: 0x6000AF5
        private System.Collections.Generic.Dictionary<Google.Protobuf.Reflection.IDescriptor,Google.Protobuf.Reflection.DescriptorDeclaration> CreateDeclarationMap() { }
        // RVA: 0x09B1E390  token: 0x6000AF6
        private Google.Protobuf.Reflection.IDescriptor FindDescriptorForPath(System.Collections.Generic.IList<System.Int32> path) { }
        // RVA: 0x09B1F0F0  token: 0x6000AF7
        private Google.Protobuf.Reflection.DescriptorBase GetDescriptorFromList(System.Collections.Generic.IReadOnlyList<Google.Protobuf.Reflection.DescriptorBase> list, System.Int32 index) { }
        // RVA: 0x09B1F1B4  token: 0x6000AF8
        private System.Collections.Generic.IReadOnlyList<Google.Protobuf.Reflection.DescriptorBase> GetNestedDescriptorListForField(System.Int32 fieldNumber) { }
        // RVA: 0x09B1F068  token: 0x6000AF9
        private Google.Protobuf.Reflection.DescriptorDeclaration GetDeclaration(Google.Protobuf.Reflection.IDescriptor descriptor) { }
        // RVA: 0x09B1DB48  token: 0x6000AFA
        private System.String ComputeFullName(Google.Protobuf.Reflection.MessageDescriptor parent, System.String name) { }
        // RVA: 0x09B1E03C  token: 0x6000AFB
        private static System.Collections.Generic.IList<Google.Protobuf.Reflection.FileDescriptor> DeterminePublicDependencies(Google.Protobuf.Reflection.FileDescriptor this, Google.Protobuf.Reflection.FileDescriptorProto proto, System.Collections.Generic.IEnumerable<Google.Protobuf.Reflection.FileDescriptor> dependencies, System.Boolean allowUnknownDependencies) { }
        // RVA: 0x09B1F250  token: 0x6000AFD
        public Google.Protobuf.Reflection.FileDescriptorProto ToProto() { }
        // RVA: -1  // generic def  token: 0x6000B0B
        public T FindTypeByName(System.String name) { }
        // RVA: 0x09B1D9C0  token: 0x6000B0C
        private static Google.Protobuf.Reflection.FileDescriptor BuildFrom(Google.Protobuf.ByteString descriptorData, Google.Protobuf.Reflection.FileDescriptorProto proto, Google.Protobuf.Reflection.FileDescriptor[] dependencies, System.Boolean allowUnknownDependencies, Google.Protobuf.Reflection.GeneratedClrTypeInfo generatedCodeInfo) { }
        // RVA: 0x09B1DDEC  token: 0x6000B0D
        private System.Void CrossLink() { }
        // RVA: 0x09B1E4EC  token: 0x6000B0E
        public static Google.Protobuf.Reflection.FileDescriptor FromGeneratedCode(System.Byte[] descriptorData, Google.Protobuf.Reflection.FileDescriptor[] dependencies, Google.Protobuf.Reflection.GeneratedClrTypeInfo generatedCodeInfo) { }
        // RVA: 0x09B1EDA0  token: 0x6000B0F
        private static System.Collections.Generic.IEnumerable<Google.Protobuf.Extension> GetAllExtensions(Google.Protobuf.Reflection.FileDescriptor[] dependencies, Google.Protobuf.Reflection.GeneratedClrTypeInfo generatedInfo) { }
        // RVA: 0x09B1EEC4  token: 0x6000B10
        private static System.Collections.Generic.IEnumerable<Google.Protobuf.Extension> GetAllGeneratedExtensions(Google.Protobuf.Reflection.GeneratedClrTypeInfo generated) { }
        // RVA: 0x09B1EA88  token: 0x6000B11
        private static System.Collections.Generic.IEnumerable<Google.Protobuf.Extension> GetAllDependedExtensions(Google.Protobuf.Reflection.FileDescriptor descriptor) { }
        // RVA: 0x09B1E808  token: 0x6000B12
        private static System.Collections.Generic.IEnumerable<Google.Protobuf.Extension> GetAllDependedExtensionsFromMessage(Google.Protobuf.Reflection.MessageDescriptor descriptor) { }
        // RVA: 0x09B1D300  token: 0x6000B13
        public static System.Collections.Generic.IReadOnlyList<Google.Protobuf.Reflection.FileDescriptor> BuildFromByteStrings(System.Collections.Generic.IEnumerable<Google.Protobuf.ByteString> descriptorData, Google.Protobuf.ExtensionRegistry registry) { }
        // RVA: 0x09B1D2B4  token: 0x6000B14
        public static System.Collections.Generic.IReadOnlyList<Google.Protobuf.Reflection.FileDescriptor> BuildFromByteStrings(System.Collections.Generic.IEnumerable<Google.Protobuf.ByteString> descriptorData) { }
        // RVA: 0x09B1F270  token: 0x6000B15
        public virtual System.String ToString() { }
        // RVA: 0x09B1F21C  token: 0x6000B18
        public Google.Protobuf.Reflection.FileOptions GetOptions() { }
        // RVA: -1  // generic def  token: 0x6000B19
        public T GetOption(Google.Protobuf.Extension<Google.Protobuf.Reflection.FileOptions,T> extension) { }
        // RVA: -1  // generic def  token: 0x6000B1A
        public Google.Protobuf.Collections.RepeatedField<T> GetOption(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.FileOptions,T> extension) { }
        // RVA: -1  // generic def  token: 0x6000B1B
        public static System.Void ForceReflectionInitialization() { }

    }

    // TypeToken: 0x2000103  // size: 0x48
    public sealed class GeneratedClrTypeInfo
    {
        // Fields
        private static readonly System.String[] EmptyNames;  // static @ 0x0
        private static readonly Google.Protobuf.Reflection.GeneratedClrTypeInfo[] EmptyCodeInfo;  // static @ 0x8
        private static readonly Google.Protobuf.Extension[] EmptyExtensions;  // static @ 0x10
        private static System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes MessageAccessibility;  // const
        private System.Type <ClrType>k__BackingField;  // 0x10
        private readonly Google.Protobuf.MessageParser <Parser>k__BackingField;  // 0x18
        private readonly System.String[] <PropertyNames>k__BackingField;  // 0x20
        private readonly Google.Protobuf.Extension[] <Extensions>k__BackingField;  // 0x28
        private readonly System.String[] <OneofNames>k__BackingField;  // 0x30
        private readonly Google.Protobuf.Reflection.GeneratedClrTypeInfo[] <NestedTypes>k__BackingField;  // 0x38
        private readonly System.Type[] <NestedEnums>k__BackingField;  // 0x40

        // Properties
        System.Type ClrType { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        Google.Protobuf.MessageParser Parser { get; /* RVA: 0x0385B100 */ }
        System.String[] PropertyNames { get; /* RVA: 0x04D862C0 */ }
        Google.Protobuf.Extension[] Extensions { get; /* RVA: 0x04D86240 */ }
        System.String[] OneofNames { get; /* RVA: 0x011F36E0 */ }
        Google.Protobuf.Reflection.GeneratedClrTypeInfo[] NestedTypes { get; /* RVA: 0x04D85A50 */ }
        System.Type[] NestedEnums { get; /* RVA: 0x04D85A60 */ }

        // Methods
        // RVA: 0x09B1F9C8  token: 0x6000B30
        public System.Void .ctor(System.Type clrType, Google.Protobuf.MessageParser parser, System.String[] propertyNames, System.String[] oneofNames, System.Type[] nestedEnums, Google.Protobuf.Extension[] extensions, Google.Protobuf.Reflection.GeneratedClrTypeInfo[] nestedTypes) { }
        // RVA: 0x09B1FB80  token: 0x6000B31
        public System.Void .ctor(System.Type clrType, Google.Protobuf.MessageParser parser, System.String[] propertyNames, System.String[] oneofNames, System.Type[] nestedEnums, Google.Protobuf.Reflection.GeneratedClrTypeInfo[] nestedTypes) { }
        // RVA: 0x09B1FB4C  token: 0x6000B32
        public System.Void .ctor(System.Type[] nestedEnums, Google.Protobuf.Extension[] extensions, Google.Protobuf.Reflection.GeneratedClrTypeInfo[] nestedTypes) { }
        // RVA: 0x09B1F99C  token: 0x6000B33
        public System.Void .ctor(System.Type[] nestedEnums, Google.Protobuf.Reflection.GeneratedClrTypeInfo[] nestedTypes) { }
        // RVA: 0x09B1F8B4  token: 0x6000B34
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000104
    public interface IDescriptor
    {
        // Properties
        System.String Name { get; /* RVA: -1  // abstract */ }
        System.String FullName { get; /* RVA: -1  // abstract */ }
        Google.Protobuf.Reflection.FileDescriptor File { get; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x2000105
    public interface IFieldAccessor
    {
        // Properties
        Google.Protobuf.Reflection.FieldDescriptor Descriptor { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000B39
        public virtual System.Void Clear(Google.Protobuf.IMessage message) { }
        // RVA: -1  // abstract  token: 0x6000B3A
        public virtual System.Object GetValue(Google.Protobuf.IMessage message) { }
        // RVA: -1  // abstract  token: 0x6000B3B
        public virtual System.Boolean HasValue(Google.Protobuf.IMessage message) { }
        // RVA: -1  // abstract  token: 0x6000B3C
        public virtual System.Void SetValue(Google.Protobuf.IMessage message, System.Object value) { }

    }

    // TypeToken: 0x2000106  // size: 0x20
    public sealed class MapFieldAccessor : Google.Protobuf.Reflection.FieldAccessorBase
    {
        // Methods
        // RVA: 0x09B1FCF8  token: 0x6000B3D
        private System.Void .ctor(System.Reflection.PropertyInfo property, Google.Protobuf.Reflection.FieldDescriptor descriptor) { }
        // RVA: 0x09B1FBC4  token: 0x6000B3E
        public virtual System.Void Clear(Google.Protobuf.IMessage message) { }
        // RVA: 0x09B1FC40  token: 0x6000B3F
        public virtual System.Boolean HasValue(Google.Protobuf.IMessage message) { }
        // RVA: 0x09B1FC9C  token: 0x6000B40
        public virtual System.Void SetValue(Google.Protobuf.IMessage message, System.Object value) { }

    }

    // TypeToken: 0x2000107  // size: 0x98
    public sealed class MessageDescriptor : Google.Protobuf.Reflection.DescriptorBase
    {
        // Fields
        private static readonly System.Collections.Generic.HashSet<System.String> WellKnownTypeNames;  // static @ 0x0
        private readonly System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor> fieldsInDeclarationOrder;  // 0x28
        private readonly System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor> fieldsInNumberOrder;  // 0x30
        private readonly System.Collections.Generic.IDictionary<System.String,Google.Protobuf.Reflection.FieldDescriptor> jsonFieldMap;  // 0x38
        private System.Func<Google.Protobuf.IMessage,System.Boolean> extensionSetIsInitialized;  // 0x40
        private readonly Google.Protobuf.Reflection.DescriptorProto <Proto>k__BackingField;  // 0x48
        private readonly System.Type <ClrType>k__BackingField;  // 0x50
        private readonly Google.Protobuf.MessageParser <Parser>k__BackingField;  // 0x58
        private readonly Google.Protobuf.Reflection.MessageDescriptor <ContainingType>k__BackingField;  // 0x60
        private readonly Google.Protobuf.Reflection.MessageDescriptor.FieldCollection <Fields>k__BackingField;  // 0x68
        private readonly Google.Protobuf.Reflection.ExtensionCollection <Extensions>k__BackingField;  // 0x70
        private readonly System.Collections.Generic.IList<Google.Protobuf.Reflection.MessageDescriptor> <NestedTypes>k__BackingField;  // 0x78
        private readonly System.Collections.Generic.IList<Google.Protobuf.Reflection.EnumDescriptor> <EnumTypes>k__BackingField;  // 0x80
        private readonly System.Collections.Generic.IList<Google.Protobuf.Reflection.OneofDescriptor> <Oneofs>k__BackingField;  // 0x88
        private readonly System.Int32 <RealOneofCount>k__BackingField;  // 0x90

        // Properties
        System.String Name { get; /* RVA: 0x09B211CC */ }
        Google.Protobuf.Reflection.DescriptorProto Proto { get; /* RVA: 0x04D86200 */ }
        System.Type ClrType { get; /* RVA: 0x04D86270 */ }
        Google.Protobuf.MessageParser Parser { get; /* RVA: 0x02E56440 */ }
        System.Boolean IsWellKnownType { get; /* RVA: 0x09B21080 */ }
        System.Boolean IsWrapperType { get; /* RVA: 0x09B21140 */ }
        Google.Protobuf.Reflection.MessageDescriptor ContainingType { get; /* RVA: 0x04D86280 */ }
        Google.Protobuf.Reflection.MessageDescriptor.FieldCollection Fields { get; /* RVA: 0x04D86230 */ }
        Google.Protobuf.Reflection.ExtensionCollection Extensions { get; /* RVA: 0x04D862B0 */ }
        System.Collections.Generic.IList<Google.Protobuf.Reflection.MessageDescriptor> NestedTypes { get; /* RVA: 0x04D862A0 */ }
        System.Collections.Generic.IList<Google.Protobuf.Reflection.EnumDescriptor> EnumTypes { get; /* RVA: 0x04D86260 */ }
        System.Collections.Generic.IList<Google.Protobuf.Reflection.OneofDescriptor> Oneofs { get; /* RVA: 0x04D861F0 */ }
        System.Int32 RealOneofCount { get; /* RVA: 0x04D86B60 */ }
        Google.Protobuf.Reflection.CustomOptions CustomOptions { get; /* RVA: 0x09B20FF0 */ }

        // Methods
        // RVA: 0x09B20688  token: 0x6000B41
        private System.Void .ctor(Google.Protobuf.Reflection.DescriptorProto proto, Google.Protobuf.Reflection.FileDescriptor file, Google.Protobuf.Reflection.MessageDescriptor parent, System.Int32 typeIndex, Google.Protobuf.Reflection.GeneratedClrTypeInfo generatedCodeInfo) { }
        // RVA: 0x09B1FD00  token: 0x6000B42
        private static System.Collections.ObjectModel.ReadOnlyDictionary<System.String,Google.Protobuf.Reflection.FieldDescriptor> CreateJsonFieldMap(System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor> fields) { }
        // RVA: 0x09B2032C  token: 0x6000B44
        private virtual System.Collections.Generic.IReadOnlyList<Google.Protobuf.Reflection.DescriptorBase> GetNestedDescriptorListForField(System.Int32 fieldNumber) { }
        // RVA: 0x09B20478  token: 0x6000B46
        public Google.Protobuf.Reflection.DescriptorProto ToProto() { }
        // RVA: 0x09B203C4  token: 0x6000B47
        private System.Boolean IsExtensionsInitialized(Google.Protobuf.IMessage message) { }
        // RVA: 0x09B20270  token: 0x6000B53
        public Google.Protobuf.Reflection.FieldDescriptor FindFieldByName(System.String name) { }
        // RVA: 0x09B202FC  token: 0x6000B54
        public Google.Protobuf.Reflection.FieldDescriptor FindFieldByNumber(System.Int32 number) { }
        // RVA: -1  // generic def  token: 0x6000B55
        public T FindDescriptor(System.String name) { }
        // RVA: 0x09B20398  token: 0x6000B57
        public Google.Protobuf.Reflection.MessageOptions GetOptions() { }
        // RVA: -1  // generic def  token: 0x6000B58
        public T GetOption(Google.Protobuf.Extension<Google.Protobuf.Reflection.MessageOptions,T> extension) { }
        // RVA: -1  // generic def  token: 0x6000B59
        public Google.Protobuf.Collections.RepeatedField<T> GetOption(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.MessageOptions,T> extension) { }
        // RVA: 0x09B1FF20  token: 0x6000B5A
        private System.Void CrossLink() { }
        // RVA: 0x09B20498  token: 0x6000B5B
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200010B  // size: 0x48
    public sealed class MethodDescriptor : Google.Protobuf.Reflection.DescriptorBase
    {
        // Fields
        private readonly Google.Protobuf.Reflection.MethodDescriptorProto proto;  // 0x28
        private readonly Google.Protobuf.Reflection.ServiceDescriptor service;  // 0x30
        private Google.Protobuf.Reflection.MessageDescriptor inputType;  // 0x38
        private Google.Protobuf.Reflection.MessageDescriptor outputType;  // 0x40

        // Properties
        Google.Protobuf.Reflection.ServiceDescriptor Service { get; /* RVA: 0x011F36E0 */ }
        Google.Protobuf.Reflection.MessageDescriptor InputType { get; /* RVA: 0x04D85A50 */ }
        Google.Protobuf.Reflection.MessageDescriptor OutputType { get; /* RVA: 0x04D85A60 */ }
        System.Boolean IsClientStreaming { get; /* RVA: 0x09B215F4 */ }
        System.Boolean IsServerStreaming { get; /* RVA: 0x09B21614 */ }
        Google.Protobuf.Reflection.CustomOptions CustomOptions { get; /* RVA: 0x09B21564 */ }
        Google.Protobuf.Reflection.MethodDescriptorProto Proto { get; /* RVA: 0x04D86240 */ }
        System.String Name { get; /* RVA: 0x09B21634 */ }

        // Methods
        // RVA: 0x09B21434  token: 0x6000B70
        public Google.Protobuf.Reflection.MethodOptions GetOptions() { }
        // RVA: -1  // generic def  token: 0x6000B71
        public T GetOption(Google.Protobuf.Extension<Google.Protobuf.Reflection.MethodOptions,T> extension) { }
        // RVA: -1  // generic def  token: 0x6000B72
        public Google.Protobuf.Collections.RepeatedField<T> GetOption(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.MethodOptions,T> extension) { }
        // RVA: 0x09B21480  token: 0x6000B73
        private System.Void .ctor(Google.Protobuf.Reflection.MethodDescriptorProto proto, Google.Protobuf.Reflection.FileDescriptor file, Google.Protobuf.Reflection.ServiceDescriptor parent, System.Int32 index) { }
        // RVA: 0x09B21460  token: 0x6000B75
        public Google.Protobuf.Reflection.MethodDescriptorProto ToProto() { }
        // RVA: 0x09B211EC  token: 0x6000B77
        private System.Void CrossLink() { }

    }

    // TypeToken: 0x200010C  // size: 0x28
    public sealed class OneofAccessor
    {
        // Fields
        private readonly System.Func<Google.Protobuf.IMessage,System.Int32> caseDelegate;  // 0x10
        private readonly System.Action<Google.Protobuf.IMessage> clearDelegate;  // 0x18
        private readonly Google.Protobuf.Reflection.OneofDescriptor <Descriptor>k__BackingField;  // 0x20

        // Properties
        Google.Protobuf.Reflection.OneofDescriptor Descriptor { get; /* RVA: 0x04D862C0 */ }

        // Methods
        // RVA: 0x09B218C4  token: 0x6000B78
        private System.Void .ctor(Google.Protobuf.Reflection.OneofDescriptor descriptor, System.Func<Google.Protobuf.IMessage,System.Int32> caseDelegate, System.Action<Google.Protobuf.IMessage> clearDelegate) { }
        // RVA: 0x09B21674  token: 0x6000B79
        private static Google.Protobuf.Reflection.OneofAccessor ForRegularOneof(Google.Protobuf.Reflection.OneofDescriptor descriptor, System.Reflection.PropertyInfo caseProperty, System.Reflection.MethodInfo clearMethod) { }
        // RVA: 0x09B2173C  token: 0x6000B7A
        private static Google.Protobuf.Reflection.OneofAccessor ForSyntheticOneof(Google.Protobuf.Reflection.OneofDescriptor descriptor) { }
        // RVA: 0x09B21654  token: 0x6000B7C
        public System.Void Clear(Google.Protobuf.IMessage message) { }
        // RVA: 0x09B21874  token: 0x6000B7D
        public Google.Protobuf.Reflection.FieldDescriptor GetCaseFieldDescriptor(Google.Protobuf.IMessage message) { }

    }

    // TypeToken: 0x200010E  // size: 0x50
    public sealed class OneofDescriptor : Google.Protobuf.Reflection.DescriptorBase
    {
        // Fields
        private Google.Protobuf.Reflection.MessageDescriptor containingType;  // 0x28
        private System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor> fields;  // 0x30
        private readonly Google.Protobuf.Reflection.OneofAccessor accessor;  // 0x38
        private readonly Google.Protobuf.Reflection.OneofDescriptorProto <Proto>k__BackingField;  // 0x40
        private readonly System.Boolean <IsSynthetic>k__BackingField;  // 0x48

        // Properties
        System.String Name { get; /* RVA: 0x09B22088 */ }
        Google.Protobuf.Reflection.OneofDescriptorProto Proto { get; /* RVA: 0x04D85A60 */ }
        Google.Protobuf.Reflection.MessageDescriptor ContainingType { get; /* RVA: 0x04D86240 */ }
        System.Collections.Generic.IList<Google.Protobuf.Reflection.FieldDescriptor> Fields { get; /* RVA: 0x011F36E0 */ }
        System.Boolean IsSynthetic { get; /* RVA: 0x04D867D0 */ }
        Google.Protobuf.Reflection.OneofAccessor Accessor { get; /* RVA: 0x04D85A50 */ }
        Google.Protobuf.Reflection.CustomOptions CustomOptions { get; /* RVA: 0x09B21FF8 */ }

        // Methods
        // RVA: 0x09B21E58  token: 0x6000B81
        private System.Void .ctor(Google.Protobuf.Reflection.OneofDescriptorProto proto, Google.Protobuf.Reflection.FileDescriptor file, Google.Protobuf.Reflection.MessageDescriptor parent, System.Int32 index, System.String clrName) { }
        // RVA: 0x09B21E38  token: 0x6000B84
        public Google.Protobuf.Reflection.OneofDescriptorProto ToProto() { }
        // RVA: 0x09B21E0C  token: 0x6000B8A
        public Google.Protobuf.Reflection.OneofOptions GetOptions() { }
        // RVA: -1  // generic def  token: 0x6000B8B
        public T GetOption(Google.Protobuf.Extension<Google.Protobuf.Reflection.OneofOptions,T> extension) { }
        // RVA: -1  // generic def  token: 0x6000B8C
        public Google.Protobuf.Collections.RepeatedField<T> GetOption(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.OneofOptions,T> extension) { }
        // RVA: 0x09B21BA0  token: 0x6000B8D
        private System.Void CrossLink() { }
        // RVA: 0x09B218FC  token: 0x6000B8E
        private Google.Protobuf.Reflection.OneofAccessor CreateAccessor(System.String clrName) { }

    }

    // TypeToken: 0x2000110  // size: 0x20
    public class OriginalNameAttribute : System.Attribute
    {
        // Fields
        private System.String <Name>k__BackingField;  // 0x10
        private System.Boolean <PreferredAlias>k__BackingField;  // 0x18

        // Properties
        System.String Name { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        System.Boolean PreferredAlias { get; /* RVA: 0x04D86340 */ set; /* RVA: 0x04D86350 */ }

        // Methods
        // RVA: 0x03537000  token: 0x6000B95
        public System.Void .ctor(System.String name) { }

    }

    // TypeToken: 0x2000111  // size: 0x28
    public sealed class PackageDescriptor : Google.Protobuf.Reflection.IDescriptor
    {
        // Fields
        private readonly System.String name;  // 0x10
        private readonly System.String fullName;  // 0x18
        private readonly Google.Protobuf.Reflection.FileDescriptor file;  // 0x20

        // Properties
        System.String Name { get; /* RVA: 0x02B2ECC0 */ }
        System.String FullName { get; /* RVA: 0x0385B100 */ }
        Google.Protobuf.Reflection.FileDescriptor File { get; /* RVA: 0x04D862C0 */ }

        // Methods
        // RVA: 0x09B220A8  token: 0x6000B96
        private System.Void .ctor(System.String name, System.String fullName, Google.Protobuf.Reflection.FileDescriptor file) { }

    }

    // TypeToken: 0x2000112  // size: 0x10
    public static class ReflectionUtil
    {
        // Fields
        private static readonly System.Type[] EmptyTypes;  // static @ 0x0
        private static readonly System.Boolean <CanConvertEnumFuncToInt32Func>k__BackingField;  // static @ 0x8

        // Properties
        System.Boolean CanConvertEnumFuncToInt32Func { get; /* RVA: 0x09B22DF8 */ }

        // Methods
        // RVA: 0x09B22BF8  token: 0x6000B9A
        private static System.Void .cctor() { }
        // RVA: -1  // generic def  token: 0x6000B9B
        private static System.Void ForceInitialize() { }
        // RVA: 0x09B228F4  token: 0x6000B9C
        private static System.Func<Google.Protobuf.IMessage,System.Object> CreateFuncIMessageObject(System.Reflection.MethodInfo method) { }
        // RVA: 0x09B2282C  token: 0x6000B9D
        private static System.Func<Google.Protobuf.IMessage,System.Int32> CreateFuncIMessageInt32(System.Reflection.MethodInfo method) { }
        // RVA: 0x09B223F0  token: 0x6000B9E
        private static System.Action<Google.Protobuf.IMessage,System.Object> CreateActionIMessageObject(System.Reflection.MethodInfo method) { }
        // RVA: 0x09B224D4  token: 0x6000B9F
        private static System.Action<Google.Protobuf.IMessage> CreateActionIMessage(System.Reflection.MethodInfo method) { }
        // RVA: 0x09B22764  token: 0x6000BA0
        private static System.Func<Google.Protobuf.IMessage,System.Boolean> CreateFuncIMessageBool(System.Reflection.MethodInfo method) { }
        // RVA: 0x09B229BC  token: 0x6000BA1
        private static System.Func<Google.Protobuf.IMessage,System.Boolean> CreateIsInitializedCaller(System.Type msg) { }
        // RVA: 0x09B225C4  token: 0x6000BA2
        private static Google.Protobuf.Reflection.ReflectionUtil.IExtensionReflectionHelper CreateExtensionHelper(Google.Protobuf.Extension extension) { }
        // RVA: 0x09B22AEC  token: 0x6000BA3
        private static Google.Protobuf.Reflection.ReflectionUtil.IReflectionHelper GetReflectionHelper(System.Type t1, System.Type t2) { }
        // RVA: 0x09B222C4  token: 0x6000BA5
        private static System.Boolean CheckCanConvertEnumFuncToInt32Func() { }
        // RVA: 0x011EC580  token: 0x6000BA6
        public static Google.Protobuf.Reflection.ReflectionUtil.SampleEnum SampleEnumMethod() { }

    }

    // TypeToken: 0x2000121  // size: 0x20
    public sealed class RepeatedFieldAccessor : Google.Protobuf.Reflection.FieldAccessorBase
    {
        // Methods
        // RVA: 0x09B1FCF8  token: 0x6000BCC
        private System.Void .ctor(System.Reflection.PropertyInfo property, Google.Protobuf.Reflection.FieldDescriptor descriptor) { }
        // RVA: 0x09B22E48  token: 0x6000BCD
        public virtual System.Void Clear(Google.Protobuf.IMessage message) { }
        // RVA: 0x09B22EC4  token: 0x6000BCE
        public virtual System.Boolean HasValue(Google.Protobuf.IMessage message) { }
        // RVA: 0x09B22F20  token: 0x6000BCF
        public virtual System.Void SetValue(Google.Protobuf.IMessage message, System.Object value) { }

    }

    // TypeToken: 0x2000122  // size: 0x38
    public sealed class ServiceDescriptor : Google.Protobuf.Reflection.DescriptorBase
    {
        // Fields
        private readonly Google.Protobuf.Reflection.ServiceDescriptorProto proto;  // 0x28
        private readonly System.Collections.Generic.IList<Google.Protobuf.Reflection.MethodDescriptor> methods;  // 0x30

        // Properties
        System.String Name { get; /* RVA: 0x09B233C8 */ }
        Google.Protobuf.Reflection.ServiceDescriptorProto Proto { get; /* RVA: 0x04D86240 */ }
        System.Collections.Generic.IList<Google.Protobuf.Reflection.MethodDescriptor> Methods { get; /* RVA: 0x011F36E0 */ }
        Google.Protobuf.Reflection.CustomOptions CustomOptions { get; /* RVA: 0x09B23338 */ }

        // Methods
        // RVA: 0x09B231C4  token: 0x6000BD0
        private System.Void .ctor(Google.Protobuf.Reflection.ServiceDescriptorProto proto, Google.Protobuf.Reflection.FileDescriptor file, System.Int32 index) { }
        // RVA: 0x09B23128  token: 0x6000BD2
        private virtual System.Collections.Generic.IReadOnlyList<Google.Protobuf.Reflection.DescriptorBase> GetNestedDescriptorListForField(System.Int32 fieldNumber) { }
        // RVA: 0x09B231A4  token: 0x6000BD4
        public Google.Protobuf.Reflection.ServiceDescriptorProto ToProto() { }
        // RVA: 0x09B2309C  token: 0x6000BD6
        public Google.Protobuf.Reflection.MethodDescriptor FindMethodByName(System.String name) { }
        // RVA: 0x09B23178  token: 0x6000BD8
        public Google.Protobuf.Reflection.ServiceOptions GetOptions() { }
        // RVA: -1  // generic def  token: 0x6000BD9
        public T GetOption(Google.Protobuf.Extension<Google.Protobuf.Reflection.ServiceOptions,T> extension) { }
        // RVA: -1  // generic def  token: 0x6000BDA
        public Google.Protobuf.Collections.RepeatedField<T> GetOption(Google.Protobuf.RepeatedExtension<Google.Protobuf.Reflection.ServiceOptions,T> extension) { }
        // RVA: 0x09B22F7C  token: 0x6000BDB
        private System.Void CrossLink() { }

    }

    // TypeToken: 0x2000124  // size: 0x38
    public sealed class SingleFieldAccessor : Google.Protobuf.Reflection.FieldAccessorBase
    {
        // Fields
        private readonly System.Action<Google.Protobuf.IMessage,System.Object> setValueDelegate;  // 0x20
        private readonly System.Action<Google.Protobuf.IMessage> clearDelegate;  // 0x28
        private readonly System.Func<Google.Protobuf.IMessage,System.Boolean> hasDelegate;  // 0x30

        // Methods
        // RVA: 0x09B23658  token: 0x6000BDE
        private System.Void .ctor(System.Type messageType, System.Reflection.PropertyInfo property, Google.Protobuf.Reflection.FieldDescriptor descriptor) { }
        // RVA: 0x09B233E8  token: 0x6000BDF
        private static System.Object GetDefaultValue(Google.Protobuf.Reflection.FieldDescriptor descriptor) { }
        // RVA: 0x0699C87C  token: 0x6000BE0
        public virtual System.Void Clear(Google.Protobuf.IMessage message) { }
        // RVA: 0x09B23618  token: 0x6000BE1
        public virtual System.Boolean HasValue(Google.Protobuf.IMessage message) { }
        // RVA: 0x09B23638  token: 0x6000BE2
        public virtual System.Void SetValue(Google.Protobuf.IMessage message, System.Object value) { }

    }

    // TypeToken: 0x2000129  // size: 0x18
    public sealed class TypeRegistry
    {
        // Fields
        private static readonly Google.Protobuf.Reflection.TypeRegistry <Empty>k__BackingField;  // static @ 0x0
        private readonly System.Collections.Generic.Dictionary<System.String,Google.Protobuf.Reflection.MessageDescriptor> fullNameToMessageMap;  // 0x10

        // Properties
        Google.Protobuf.Reflection.TypeRegistry Empty { get; /* RVA: 0x09B24368 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x6000BEF
        private System.Void .ctor(System.Collections.Generic.Dictionary<System.String,Google.Protobuf.Reflection.MessageDescriptor> fullNameToMessageMap) { }
        // RVA: 0x09B23EA0  token: 0x6000BF0
        public Google.Protobuf.Reflection.MessageDescriptor Find(System.String fullName) { }
        // RVA: 0x09B23F08  token: 0x6000BF1
        public static Google.Protobuf.Reflection.TypeRegistry FromFiles(Google.Protobuf.Reflection.FileDescriptor[] fileDescriptors) { }
        // RVA: 0x09B23F54  token: 0x6000BF2
        public static Google.Protobuf.Reflection.TypeRegistry FromFiles(System.Collections.Generic.IEnumerable<Google.Protobuf.Reflection.FileDescriptor> fileDescriptors) { }
        // RVA: 0x09B2410C  token: 0x6000BF3
        public static Google.Protobuf.Reflection.TypeRegistry FromMessages(Google.Protobuf.Reflection.MessageDescriptor[] messageDescriptors) { }
        // RVA: 0x09B24158  token: 0x6000BF4
        public static Google.Protobuf.Reflection.TypeRegistry FromMessages(System.Collections.Generic.IEnumerable<Google.Protobuf.Reflection.MessageDescriptor> messageDescriptors) { }
        // RVA: 0x09B242B0  token: 0x6000BF5
        private static System.Void .cctor() { }

    }

}

namespace Google.Protobuf.WellKnownTypes
{

    // TypeToken: 0x2000060  // size: 0x10
    public static class AnyReflection
    {
        // Fields
        private static Google.Protobuf.Reflection.FileDescriptor descriptor;  // static @ 0x0

        // Properties
        Google.Protobuf.Reflection.FileDescriptor Descriptor { get; /* RVA: 0x09AE3438 */ }

        // Methods
        // RVA: 0x09AE30E0  token: 0x60003A9
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000061  // size: 0x28
    public sealed class Any : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Any> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 TypeUrlFieldNumber;  // const
        private System.String typeUrl_;  // 0x18
        public static System.Int32 ValueFieldNumber;  // const
        private Google.Protobuf.ByteString value_;  // 0x20
        private static System.String DefaultPrefix;  // const

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Any> Parser { get; /* RVA: 0x09AE3D6C */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09AE3D0C */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09AE3EF8 */ }
        System.String TypeUrl { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x09AE3F38 */ }
        Google.Protobuf.ByteString Value { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x09AE3FA0 */ }

        // Methods
        // RVA: 0x09AE3CA4  token: 0x60003AD
        public System.Void .ctor() { }
        // RVA: 0x09AE3C3C  token: 0x60003AE
        public System.Void .ctor(Google.Protobuf.WellKnownTypes.Any other) { }
        // RVA: 0x09AE3540  token: 0x60003AF
        public virtual Google.Protobuf.WellKnownTypes.Any Clone() { }
        // RVA: 0x09AE35A0  token: 0x60003B4
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09AE3604  token: 0x60003B5
        public virtual System.Boolean Equals(Google.Protobuf.WellKnownTypes.Any other) { }
        // RVA: 0x09AE36AC  token: 0x60003B6
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09AE3AEC  token: 0x60003B7
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x60003B8
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09AE3E68  token: 0x60003B9
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09AE3488  token: 0x60003BA
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09AE38E0  token: 0x60003BB
        public virtual System.Void MergeFrom(Google.Protobuf.WellKnownTypes.Any other) { }
        // RVA: 0x04D51840  token: 0x60003BC
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09AE3DBC  token: 0x60003BD
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09AE37BC  token: 0x60003BE
        private static System.String GetTypeUrl(Google.Protobuf.Reflection.MessageDescriptor descriptor, System.String prefix) { }
        // RVA: 0x09AE3728  token: 0x60003BF
        public static System.String GetTypeName(System.String typeUrl) { }
        // RVA: 0x09AE3848  token: 0x60003C0
        public System.Boolean Is(Google.Protobuf.Reflection.MessageDescriptor descriptor) { }
        // RVA: -1  // generic def  token: 0x60003C1
        public T Unpack() { }
        // RVA: -1  // generic def  token: 0x60003C2
        public System.Boolean TryUnpack(T& result) { }
        // RVA: 0x09AE3A98  token: 0x60003C3
        public static Google.Protobuf.WellKnownTypes.Any Pack(Google.Protobuf.IMessage message) { }
        // RVA: 0x09AE3960  token: 0x60003C4
        public static Google.Protobuf.WellKnownTypes.Any Pack(Google.Protobuf.IMessage message, System.String typeUrlPrefix) { }
        // RVA: 0x09AE3B38  token: 0x60003C5
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000063  // size: 0x10
    public static class ApiReflection
    {
        // Fields
        private static Google.Protobuf.Reflection.FileDescriptor descriptor;  // static @ 0x0

        // Properties
        Google.Protobuf.Reflection.FileDescriptor Descriptor { get; /* RVA: 0x09AE4804 */ }

        // Methods
        // RVA: 0x09AE4008  token: 0x60003CA
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000064  // size: 0x50
    public sealed class Api : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Api> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 NameFieldNumber;  // const
        private System.String name_;  // 0x18
        public static System.Int32 MethodsFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.WellKnownTypes.Method> _repeated_methods_codec;  // static @ 0x8
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.Method> methods_;  // 0x20
        public static System.Int32 OptionsFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.WellKnownTypes.Option> _repeated_options_codec;  // static @ 0x10
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.Option> options_;  // 0x28
        public static System.Int32 VersionFieldNumber;  // const
        private System.String version_;  // 0x30
        public static System.Int32 SourceContextFieldNumber;  // const
        private Google.Protobuf.WellKnownTypes.SourceContext sourceContext_;  // 0x38
        public static System.Int32 MixinsFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.WellKnownTypes.Mixin> _repeated_mixins_codec;  // static @ 0x18
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.Mixin> mixins_;  // 0x40
        public static System.Int32 SyntaxFieldNumber;  // const
        private Google.Protobuf.WellKnownTypes.Syntax syntax_;  // 0x48

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Api> Parser { get; /* RVA: 0x09AE53FC */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09AE539C */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09AE585C */ }
        System.String Name { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x09AE589C */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.Method> Methods { get; /* RVA: 0x04D862C0 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.Option> Options { get; /* RVA: 0x04D86240 */ }
        System.String Version { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x09AE5904 */ }
        Google.Protobuf.WellKnownTypes.SourceContext SourceContext { get; /* RVA: 0x04D85A50 */ set; /* RVA: 0x046A91B0 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.Mixin> Mixins { get; /* RVA: 0x04D85A60 */ }
        Google.Protobuf.WellKnownTypes.Syntax Syntax { get; /* RVA: 0x04D863D0 */ set; /* RVA: 0x04D86450 */ }

        // Methods
        // RVA: 0x09AE5120  token: 0x60003CE
        public System.Void .ctor() { }
        // RVA: 0x09AE5260  token: 0x60003CF
        public System.Void .ctor(Google.Protobuf.WellKnownTypes.Api other) { }
        // RVA: 0x09AE4A34  token: 0x60003D0
        public virtual Google.Protobuf.WellKnownTypes.Api Clone() { }
        // RVA: 0x09AE4A94  token: 0x60003DC
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09AE4AF8  token: 0x60003DD
        public virtual System.Boolean Equals(Google.Protobuf.WellKnownTypes.Api other) { }
        // RVA: 0x09AE4C20  token: 0x60003DE
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09AE4E94  token: 0x60003DF
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x60003E0
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09AE5698  token: 0x60003E1
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09AE4854  token: 0x60003E2
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09AE4D14  token: 0x60003E3
        public virtual System.Void MergeFrom(Google.Protobuf.WellKnownTypes.Api other) { }
        // RVA: 0x04D51840  token: 0x60003E4
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09AE544C  token: 0x60003E5
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09AE4EE0  token: 0x60003E6
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000066  // size: 0x50
    public sealed class Method : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Method> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 NameFieldNumber;  // const
        private System.String name_;  // 0x18
        public static System.Int32 RequestTypeUrlFieldNumber;  // const
        private System.String requestTypeUrl_;  // 0x20
        public static System.Int32 RequestStreamingFieldNumber;  // const
        private System.Boolean requestStreaming_;  // 0x28
        public static System.Int32 ResponseTypeUrlFieldNumber;  // const
        private System.String responseTypeUrl_;  // 0x30
        public static System.Int32 ResponseStreamingFieldNumber;  // const
        private System.Boolean responseStreaming_;  // 0x38
        public static System.Int32 OptionsFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.WellKnownTypes.Option> _repeated_options_codec;  // static @ 0x8
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.Option> options_;  // 0x40
        public static System.Int32 SyntaxFieldNumber;  // const
        private Google.Protobuf.WellKnownTypes.Syntax syntax_;  // 0x48

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Method> Parser { get; /* RVA: 0x09AE9950 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09AE98F0 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09AE9CDC */ }
        System.String Name { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x09AE9D1C */ }
        System.String RequestTypeUrl { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x09AE9D84 */ }
        System.Boolean RequestStreaming { get; /* RVA: 0x011F33C0 */ set; /* RVA: 0x011F33D0 */ }
        System.String ResponseTypeUrl { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x09AE9DEC */ }
        System.Boolean ResponseStreaming { get; /* RVA: 0x011F2A70 */ set; /* RVA: 0x011F2A80 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.Option> Options { get; /* RVA: 0x04D85A60 */ }
        Google.Protobuf.WellKnownTypes.Syntax Syntax { get; /* RVA: 0x04D863D0 */ set; /* RVA: 0x04D86450 */ }

        // Methods
        // RVA: 0x09AE9830  token: 0x60003ED
        public System.Void .ctor() { }
        // RVA: 0x09AE975C  token: 0x60003EE
        public System.Void .ctor(Google.Protobuf.WellKnownTypes.Method other) { }
        // RVA: 0x09AE91F8  token: 0x60003EF
        public virtual Google.Protobuf.WellKnownTypes.Method Clone() { }
        // RVA: 0x09AE9330  token: 0x60003FD
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09AE9258  token: 0x60003FE
        public virtual System.Boolean Equals(Google.Protobuf.WellKnownTypes.Method other) { }
        // RVA: 0x09AE9394  token: 0x60003FF
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09AE95A0  token: 0x6000400
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x6000401
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09AE9B48  token: 0x6000402
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09AE906C  token: 0x6000403
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09AE94A0  token: 0x6000404
        public virtual System.Void MergeFrom(Google.Protobuf.WellKnownTypes.Method other) { }
        // RVA: 0x04D51840  token: 0x6000405
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09AE99A0  token: 0x6000406
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09AE95EC  token: 0x6000407
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000068  // size: 0x28
    public sealed class Mixin : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Mixin> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 NameFieldNumber;  // const
        private System.String name_;  // 0x18
        public static System.Int32 RootFieldNumber;  // const
        private System.String root_;  // 0x20

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Mixin> Parser { get; /* RVA: 0x09AEA2B4 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09AEA254 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09AEA3B0 */ }
        System.String Name { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x09AEA3F0 */ }
        System.String Root { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x09AEA458 */ }

        // Methods
        // RVA: 0x09AEA200  token: 0x600040E
        public System.Void .ctor() { }
        // RVA: 0x09AEA198  token: 0x600040F
        public System.Void .ctor(Google.Protobuf.WellKnownTypes.Mixin other) { }
        // RVA: 0x09AE9F08  token: 0x6000410
        public virtual Google.Protobuf.WellKnownTypes.Mixin Clone() { }
        // RVA: 0x09AE9F68  token: 0x6000415
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x063C4394  token: 0x6000416
        public virtual System.Boolean Equals(Google.Protobuf.WellKnownTypes.Mixin other) { }
        // RVA: 0x063C43FC  token: 0x6000417
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09AEA048  token: 0x6000418
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x6000419
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x063C47AC  token: 0x600041A
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09AE9E54  token: 0x600041B
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09AE9FCC  token: 0x600041C
        public virtual System.Void MergeFrom(Google.Protobuf.WellKnownTypes.Mixin other) { }
        // RVA: 0x04D51840  token: 0x600041D
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09AEA304  token: 0x600041E
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09AEA094  token: 0x600041F
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200006A  // size: 0x10
    public static class DurationReflection
    {
        // Fields
        private static Google.Protobuf.Reflection.FileDescriptor descriptor;  // static @ 0x0

        // Properties
        Google.Protobuf.Reflection.FileDescriptor Descriptor { get; /* RVA: 0x09AE5CE4 */ }

        // Methods
        // RVA: 0x09AE596C  token: 0x6000424
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200006B  // size: 0x28
    public sealed class Duration : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage, Google.Protobuf.ICustomDiagnosticMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Duration> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 SecondsFieldNumber;  // const
        private System.Int64 seconds_;  // 0x18
        public static System.Int32 NanosFieldNumber;  // const
        private System.Int32 nanos_;  // 0x20
        public static System.Int32 NanosecondsPerSecond;  // const
        public static System.Int32 NanosecondsPerTick;  // const
        public static System.Int64 MaxSeconds;  // const
        public static System.Int64 MinSeconds;  // const
        private static System.Int32 MaxNanoseconds;  // const
        private static System.Int32 MinNanoseconds;  // const

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Duration> Parser { get; /* RVA: 0x09AE6888 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09AE67F4 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09AE6C60 */ }
        System.Int64 Seconds { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x011EC9D0 */ }
        System.Int32 Nanos { get; /* RVA: 0x02B70F90 */ set; /* RVA: 0x04D86330 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000428
        public System.Void .ctor() { }
        // RVA: 0x0637B49C  token: 0x6000429
        public System.Void .ctor(Google.Protobuf.WellKnownTypes.Duration other) { }
        // RVA: 0x09AE5F20  token: 0x600042A
        public virtual Google.Protobuf.WellKnownTypes.Duration Clone() { }
        // RVA: 0x09AE5F80  token: 0x600042F
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x0637BDB8  token: 0x6000430
        public virtual System.Boolean Equals(Google.Protobuf.WellKnownTypes.Duration other) { }
        // RVA: 0x0637BDEC  token: 0x6000431
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09AE653C  token: 0x6000432
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x6000433
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x0637C018  token: 0x6000434
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09AE5E84  token: 0x6000435
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x0637BE38  token: 0x6000436
        public virtual System.Void MergeFrom(Google.Protobuf.WellKnownTypes.Duration other) { }
        // RVA: 0x04D51840  token: 0x6000437
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x0637BF80  token: 0x6000438
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09AE6168  token: 0x6000439
        private static System.Boolean IsNormalized(System.Int64 seconds, System.Int32 nanoseconds) { }
        // RVA: 0x09AE6588  token: 0x600043A
        public System.TimeSpan ToTimeSpan() { }
        // RVA: 0x09AE5FE4  token: 0x600043B
        public static Google.Protobuf.WellKnownTypes.Duration FromTimeSpan(System.TimeSpan timeSpan) { }
        // RVA: 0x09AE6B68  token: 0x600043C
        public static Google.Protobuf.WellKnownTypes.Duration op_UnaryNegation(Google.Protobuf.WellKnownTypes.Duration value) { }
        // RVA: 0x09AE68D8  token: 0x600043D
        public static Google.Protobuf.WellKnownTypes.Duration op_Addition(Google.Protobuf.WellKnownTypes.Duration lhs, Google.Protobuf.WellKnownTypes.Duration rhs) { }
        // RVA: 0x09AE6A1C  token: 0x600043E
        public static Google.Protobuf.WellKnownTypes.Duration op_Subtraction(Google.Protobuf.WellKnownTypes.Duration lhs, Google.Protobuf.WellKnownTypes.Duration rhs) { }
        // RVA: 0x09AE61FC  token: 0x600043F
        private static Google.Protobuf.WellKnownTypes.Duration Normalize(System.Int64 seconds, System.Int32 nanoseconds) { }
        // RVA: 0x09AE62F0  token: 0x6000440
        private static System.String ToJson(System.Int64 seconds, System.Int32 nanoseconds, System.Boolean diagnosticOnly) { }
        // RVA: 0x09AE6298  token: 0x6000441
        public virtual System.String ToDiagnosticString() { }
        // RVA: 0x09AE5D34  token: 0x6000442
        private static System.Void AppendNanoseconds(System.Text.StringBuilder builder, System.Int32 nanos) { }
        // RVA: 0x09AE66F0  token: 0x6000443
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200006D  // size: 0x10
    public static class EmptyReflection
    {
        // Fields
        private static Google.Protobuf.Reflection.FileDescriptor descriptor;  // static @ 0x0

        // Properties
        Google.Protobuf.Reflection.FileDescriptor Descriptor { get; /* RVA: 0x09AE6F98 */ }

        // Methods
        // RVA: 0x09AE6CA0  token: 0x6000448
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200006E  // size: 0x18
    public sealed class Empty : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Empty> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Empty> Parser { get; /* RVA: 0x09AE7290 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09AE71FC */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09AE72E0 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x600044C
        public System.Void .ctor() { }
        // RVA: 0x0639FAEC  token: 0x600044D
        public System.Void .ctor(Google.Protobuf.WellKnownTypes.Empty other) { }
        // RVA: 0x09AE6FE8  token: 0x600044E
        public virtual Google.Protobuf.WellKnownTypes.Empty Clone() { }
        // RVA: 0x09AE7048  token: 0x600044F
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x0639F91C  token: 0x6000450
        public virtual System.Boolean Equals(Google.Protobuf.WellKnownTypes.Empty other) { }
        // RVA: 0x0639F940  token: 0x6000451
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09AE70AC  token: 0x6000452
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x6000453
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x0639FC2C  token: 0x6000454
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x0639F838  token: 0x6000455
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x0639F96C  token: 0x6000456
        public virtual System.Void MergeFrom(Google.Protobuf.WellKnownTypes.Empty other) { }
        // RVA: 0x04D51840  token: 0x6000457
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x0639FBD0  token: 0x6000458
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09AE70F8  token: 0x6000459
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000070  // size: 0x10
    public static class FieldMaskReflection
    {
        // Fields
        private static Google.Protobuf.Reflection.FileDescriptor descriptor;  // static @ 0x0

        // Properties
        Google.Protobuf.Reflection.FileDescriptor Descriptor { get; /* RVA: 0x09AE7658 */ }

        // Methods
        // RVA: 0x09AE7320  token: 0x600045E
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000071  // size: 0x20
    public sealed class FieldMask : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage, Google.Protobuf.ICustomDiagnosticMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.FieldMask> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 PathsFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<System.String> _repeated_paths_codec;  // static @ 0x8
        private readonly Google.Protobuf.Collections.RepeatedField<System.String> paths_;  // 0x18
        private static System.Char FIELD_PATH_SEPARATOR;  // const
        private static System.Char FIELD_SEPARATOR_REGEX;  // const

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.FieldMask> Parser { get; /* RVA: 0x09AE86FC */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09AE8668 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09AE88C0 */ }
        Google.Protobuf.Collections.RepeatedField<System.String> Paths { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x09AE85F0  token: 0x6000462
        public System.Void .ctor() { }
        // RVA: 0x09AE8568  token: 0x6000463
        public System.Void .ctor(Google.Protobuf.WellKnownTypes.FieldMask other) { }
        // RVA: 0x09AE773C  token: 0x6000464
        public virtual Google.Protobuf.WellKnownTypes.FieldMask Clone() { }
        // RVA: 0x09AE779C  token: 0x6000466
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09AE7800  token: 0x6000467
        public virtual System.Boolean Equals(Google.Protobuf.WellKnownTypes.FieldMask other) { }
        // RVA: 0x0638F5B8  token: 0x6000468
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09AE8338  token: 0x6000469
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x600046A
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09AE881C  token: 0x600046B
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09AE76A8  token: 0x600046C
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09AE7DAC  token: 0x600046D
        public virtual System.Void MergeFrom(Google.Protobuf.WellKnownTypes.FieldMask other) { }
        // RVA: 0x04D51840  token: 0x600046E
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09AE874C  token: 0x600046F
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09AE7FEC  token: 0x6000470
        private static System.String ToJson(System.Collections.Generic.IList<System.String> paths, System.Boolean diagnosticOnly) { }
        // RVA: 0x09AE7F9C  token: 0x6000471
        public virtual System.String ToDiagnosticString() { }
        // RVA: 0x09AE787C  token: 0x6000472
        public static Google.Protobuf.WellKnownTypes.FieldMask FromString(System.String value) { }
        // RVA: -1  // generic def  token: 0x6000473
        public static Google.Protobuf.WellKnownTypes.FieldMask FromString(System.String value) { }
        // RVA: -1  // generic def  token: 0x6000474
        public static Google.Protobuf.WellKnownTypes.FieldMask FromStringEnumerable(System.Collections.Generic.IEnumerable<System.String> paths) { }
        // RVA: -1  // generic def  token: 0x6000475
        public static Google.Protobuf.WellKnownTypes.FieldMask FromFieldNumbers(System.Int32[] fieldNumbers) { }
        // RVA: -1  // generic def  token: 0x6000476
        public static Google.Protobuf.WellKnownTypes.FieldMask FromFieldNumbers(System.Collections.Generic.IEnumerable<System.Int32> fieldNumbers) { }
        // RVA: 0x09AE7B14  token: 0x6000477
        private static System.Boolean IsPathValid(System.String input) { }
        // RVA: -1  // generic def  token: 0x6000478
        public static System.Boolean IsValid(Google.Protobuf.WellKnownTypes.FieldMask fieldMask) { }
        // RVA: 0x09AE7B88  token: 0x6000479
        public static System.Boolean IsValid(Google.Protobuf.Reflection.MessageDescriptor descriptor, Google.Protobuf.WellKnownTypes.FieldMask fieldMask) { }
        // RVA: -1  // generic def  token: 0x600047A
        public static System.Boolean IsValid(System.String path) { }
        // RVA: 0x09AE7CF8  token: 0x600047B
        public static System.Boolean IsValid(Google.Protobuf.Reflection.MessageDescriptor descriptor, System.String path) { }
        // RVA: 0x09AE7F34  token: 0x600047C
        public Google.Protobuf.WellKnownTypes.FieldMask Normalize() { }
        // RVA: 0x09AE8384  token: 0x600047D
        public Google.Protobuf.WellKnownTypes.FieldMask Union(Google.Protobuf.WellKnownTypes.FieldMask[] otherMasks) { }
        // RVA: 0x09AE7938  token: 0x600047E
        public Google.Protobuf.WellKnownTypes.FieldMask Intersection(Google.Protobuf.WellKnownTypes.FieldMask additionalMask) { }
        // RVA: 0x09AE7E24  token: 0x600047F
        public System.Void Merge(Google.Protobuf.IMessage source, Google.Protobuf.IMessage destination, Google.Protobuf.WellKnownTypes.FieldMask.MergeOptions options) { }
        // RVA: 0x09AE7EC0  token: 0x6000480
        public System.Void Merge(Google.Protobuf.IMessage source, Google.Protobuf.IMessage destination) { }
        // RVA: 0x09AE842C  token: 0x6000481
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000074  // size: 0x10
    public static class SourceContextReflection
    {
        // Fields
        private static Google.Protobuf.Reflection.FileDescriptor descriptor;  // static @ 0x0

        // Properties
        Google.Protobuf.Reflection.FileDescriptor Descriptor { get; /* RVA: 0x09AEC5C4 */ }

        // Methods
        // RVA: 0x09AEC26C  token: 0x600048E
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000075  // size: 0x20
    public sealed class SourceContext : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.SourceContext> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 FileNameFieldNumber;  // const
        private System.String fileName_;  // 0x18

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.SourceContext> Parser { get; /* RVA: 0x09AEC9FC */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09AEC99C */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09AECAC8 */ }
        System.String FileName { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x09AECB08 */ }

        // Methods
        // RVA: 0x09AEC95C  token: 0x6000492
        public System.Void .ctor() { }
        // RVA: 0x09AEC904  token: 0x6000493
        public System.Void .ctor(Google.Protobuf.WellKnownTypes.SourceContext other) { }
        // RVA: 0x09AEC690  token: 0x6000494
        public virtual Google.Protobuf.WellKnownTypes.SourceContext Clone() { }
        // RVA: 0x09AEC6F0  token: 0x6000497
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x0639C940  token: 0x6000498
        public virtual System.Boolean Equals(Google.Protobuf.WellKnownTypes.SourceContext other) { }
        // RVA: 0x0639C9F8  token: 0x6000499
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09AEC7B4  token: 0x600049A
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x600049B
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x0639CDC4  token: 0x600049C
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09AEC614  token: 0x600049D
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09AEC754  token: 0x600049E
        public virtual System.Void MergeFrom(Google.Protobuf.WellKnownTypes.SourceContext other) { }
        // RVA: 0x04D51840  token: 0x600049F
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09AECA4C  token: 0x60004A0
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09AEC800  token: 0x60004A1
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000077  // size: 0x10
    public static class StructReflection
    {
        // Fields
        private static Google.Protobuf.Reflection.FileDescriptor descriptor;  // static @ 0x0

        // Properties
        Google.Protobuf.Reflection.FileDescriptor Descriptor { get; /* RVA: 0x09AED2E0 */ }

        // Methods
        // RVA: 0x09AECB70  token: 0x60004A6
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000078  // size: 0x14
    public sealed struct NullValue
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Google.Protobuf.WellKnownTypes.NullValue NullValue;  // const

    }

    // TypeToken: 0x2000079  // size: 0x20
    public sealed class Struct : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Struct> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 FieldsFieldNumber;  // const
        private static readonly Google.Protobuf.Collections.MapField.Codec<System.String,Google.Protobuf.WellKnownTypes.Value> _map_fields_codec;  // static @ 0x8
        private readonly Google.Protobuf.Collections.MapField<System.String,Google.Protobuf.WellKnownTypes.Value> fields_;  // 0x18

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Struct> Parser { get; /* RVA: 0x09AED918 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09AED8B8 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09AEDADC */ }
        Google.Protobuf.Collections.MapField<System.String,Google.Protobuf.WellKnownTypes.Value> Fields { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x09AED840  token: 0x60004AA
        public System.Void .ctor() { }
        // RVA: 0x09AED7B8  token: 0x60004AB
        public System.Void .ctor(Google.Protobuf.WellKnownTypes.Struct other) { }
        // RVA: 0x09AED3C4  token: 0x60004AC
        public virtual Google.Protobuf.WellKnownTypes.Struct Clone() { }
        // RVA: 0x09AED424  token: 0x60004AE
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09AED488  token: 0x60004AF
        public virtual System.Boolean Equals(Google.Protobuf.WellKnownTypes.Struct other) { }
        // RVA: 0x0638F5B8  token: 0x60004B0
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09AED57C  token: 0x60004B1
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x60004B2
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09AEDA38  token: 0x60004B3
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09AED330  token: 0x60004B4
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09AED504  token: 0x60004B5
        public virtual System.Void MergeFrom(Google.Protobuf.WellKnownTypes.Struct other) { }
        // RVA: 0x04D51840  token: 0x60004B6
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09AED968  token: 0x60004B7
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09AED5C8  token: 0x60004B8
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200007B  // size: 0x28
    public sealed class Value : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Value> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 NullValueFieldNumber;  // const
        public static System.Int32 NumberValueFieldNumber;  // const
        public static System.Int32 StringValueFieldNumber;  // const
        public static System.Int32 BoolValueFieldNumber;  // const
        public static System.Int32 StructValueFieldNumber;  // const
        public static System.Int32 ListValueFieldNumber;  // const
        private System.Object kind_;  // 0x18
        private Google.Protobuf.WellKnownTypes.Value.KindOneofCase kindCase_;  // 0x20

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Value> Parser { get; /* RVA: 0x09AF36D8 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09AF3590 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09AF3B48 */ }
        Google.Protobuf.WellKnownTypes.NullValue NullValue { get; /* RVA: 0x09AF3644 */ set; /* RVA: 0x09AF3C14 */ }
        System.Double NumberValue { get; /* RVA: 0x09AF368C */ set; /* RVA: 0x09AF3C74 */ }
        System.String StringValue { get; /* RVA: 0x09AF3728 */ set; /* RVA: 0x09AF3CD8 */ }
        System.Boolean BoolValue { get; /* RVA: 0x09AF3548 */ set; /* RVA: 0x09AF3B88 */ }
        Google.Protobuf.WellKnownTypes.Struct StructValue { get; /* RVA: 0x09AF379C */ set; /* RVA: 0x09AF3D48 */ }
        Google.Protobuf.WellKnownTypes.ListValue ListValue { get; /* RVA: 0x09AF35F0 */ set; /* RVA: 0x09AF3BEC */ }
        Google.Protobuf.WellKnownTypes.Value.KindOneofCase KindCase { get; /* RVA: 0x02B70F90 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60004BF
        public System.Void .ctor() { }
        // RVA: 0x09AF340C  token: 0x60004C0
        public System.Void .ctor(Google.Protobuf.WellKnownTypes.Value other) { }
        // RVA: 0x09AF2A20  token: 0x60004C1
        public virtual Google.Protobuf.WellKnownTypes.Value Clone() { }
        // RVA: 0x063C8830  token: 0x60004CF
        public System.Void ClearKind() { }
        // RVA: 0x09AF2A80  token: 0x60004D0
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09AF2AE4  token: 0x60004D1
        public virtual System.Boolean Equals(Google.Protobuf.WellKnownTypes.Value other) { }
        // RVA: 0x09AF2F80  token: 0x60004D2
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09AF32BC  token: 0x60004D3
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x60004D4
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09AF3A1C  token: 0x60004D5
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09AF28FC  token: 0x60004D6
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09AF30D4  token: 0x60004D7
        public virtual System.Void MergeFrom(Google.Protobuf.WellKnownTypes.Value other) { }
        // RVA: 0x04D51840  token: 0x60004D8
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09AF37F0  token: 0x60004D9
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09AF2E60  token: 0x60004DA
        public static Google.Protobuf.WellKnownTypes.Value ForString(System.String value) { }
        // RVA: 0x09AF2E00  token: 0x60004DB
        public static Google.Protobuf.WellKnownTypes.Value ForNumber(System.Double value) { }
        // RVA: 0x09AF2C64  token: 0x60004DC
        public static Google.Protobuf.WellKnownTypes.Value ForBool(System.Boolean value) { }
        // RVA: 0x09AF2DAC  token: 0x60004DD
        public static Google.Protobuf.WellKnownTypes.Value ForNull() { }
        // RVA: 0x09AF2CC4  token: 0x60004DE
        public static Google.Protobuf.WellKnownTypes.Value ForList(Google.Protobuf.WellKnownTypes.Value[] values) { }
        // RVA: 0x09AF2EF0  token: 0x60004DF
        public static Google.Protobuf.WellKnownTypes.Value ForStruct(Google.Protobuf.WellKnownTypes.Struct value) { }
        // RVA: 0x09AF3308  token: 0x60004E0
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200007E  // size: 0x20
    public sealed class ListValue : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.ListValue> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 ValuesFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.WellKnownTypes.Value> _repeated_values_codec;  // static @ 0x8
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.Value> values_;  // 0x18

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.ListValue> Parser { get; /* RVA: 0x09AE8E68 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09AE8E08 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09AE902C */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.Value> Values { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x09AE8D90  token: 0x60004E7
        public System.Void .ctor() { }
        // RVA: 0x09AE8D08  token: 0x60004E8
        public System.Void .ctor(Google.Protobuf.WellKnownTypes.ListValue other) { }
        // RVA: 0x09AE8994  token: 0x60004E9
        public virtual Google.Protobuf.WellKnownTypes.ListValue Clone() { }
        // RVA: 0x09AE89F4  token: 0x60004EB
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09AE8A58  token: 0x60004EC
        public virtual System.Boolean Equals(Google.Protobuf.WellKnownTypes.ListValue other) { }
        // RVA: 0x0638F5B8  token: 0x60004ED
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09AE8B4C  token: 0x60004EE
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x60004EF
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09AE8F88  token: 0x60004F0
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09AE8900  token: 0x60004F1
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09AE8AD4  token: 0x60004F2
        public virtual System.Void MergeFrom(Google.Protobuf.WellKnownTypes.ListValue other) { }
        // RVA: 0x04D51840  token: 0x60004F3
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09AE8EB8  token: 0x60004F4
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09AE8B98  token: 0x60004F5
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000080  // size: 0x10
    public static class TimeExtensions
    {
        // Methods
        // RVA: 0x09AEDB68  token: 0x60004F9
        public static Google.Protobuf.WellKnownTypes.Timestamp ToTimestamp(System.DateTime dateTime) { }
        // RVA: 0x09AEDBB4  token: 0x60004FA
        public static Google.Protobuf.WellKnownTypes.Timestamp ToTimestamp(System.DateTimeOffset dateTimeOffset) { }
        // RVA: 0x09AEDB1C  token: 0x60004FB
        public static Google.Protobuf.WellKnownTypes.Duration ToDuration(System.TimeSpan timeSpan) { }

    }

    // TypeToken: 0x2000081  // size: 0x10
    public static class TimestampReflection
    {
        // Fields
        private static Google.Protobuf.Reflection.FileDescriptor descriptor;  // static @ 0x0

        // Properties
        Google.Protobuf.Reflection.FileDescriptor Descriptor { get; /* RVA: 0x09AEDF80 */ }

        // Methods
        // RVA: 0x09AEDC08  token: 0x60004FD
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000082  // size: 0x28
    public sealed class Timestamp : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage, Google.Protobuf.ICustomDiagnosticMessage, System.IComparable`1
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Timestamp> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 SecondsFieldNumber;  // const
        private System.Int64 seconds_;  // 0x18
        public static System.Int32 NanosFieldNumber;  // const
        private System.Int32 nanos_;  // 0x20
        private static readonly System.DateTime UnixEpoch;  // static @ 0x8
        private static System.Int64 BclSecondsAtUnixEpoch;  // const
        private static System.Int64 UnixSecondsAtBclMaxValue;  // const
        private static System.Int64 UnixSecondsAtBclMinValue;  // const
        private static System.Int32 MaxNanos;  // const

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Timestamp> Parser { get; /* RVA: 0x09AEEB8C */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09AEEAF8 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09AEF0CC */ }
        System.Int64 Seconds { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x011EC9D0 */ }
        System.Int32 Nanos { get; /* RVA: 0x02B70F90 */ set; /* RVA: 0x04D86330 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000501
        public System.Void .ctor() { }
        // RVA: 0x0637B49C  token: 0x6000502
        public System.Void .ctor(Google.Protobuf.WellKnownTypes.Timestamp other) { }
        // RVA: 0x09AEE06C  token: 0x6000503
        public virtual Google.Protobuf.WellKnownTypes.Timestamp Clone() { }
        // RVA: 0x09AEE160  token: 0x6000508
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x0637BDB8  token: 0x6000509
        public virtual System.Boolean Equals(Google.Protobuf.WellKnownTypes.Timestamp other) { }
        // RVA: 0x0637BDEC  token: 0x600050A
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09AEE94C  token: 0x600050B
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x600050C
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x0637C018  token: 0x600050D
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09AEDFD0  token: 0x600050E
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09AEE3C0  token: 0x600050F
        public virtual System.Void MergeFrom(Google.Protobuf.WellKnownTypes.Timestamp other) { }
        // RVA: 0x04D51840  token: 0x6000510
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x0637BF80  token: 0x6000511
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09AEE394  token: 0x6000512
        private static System.Boolean IsNormalized(System.Int64 seconds, System.Int32 nanoseconds) { }
        // RVA: 0x09AEEE28  token: 0x6000513
        public static Google.Protobuf.WellKnownTypes.Duration op_Subtraction(Google.Protobuf.WellKnownTypes.Timestamp lhs, Google.Protobuf.WellKnownTypes.Timestamp rhs) { }
        // RVA: 0x09AEEBDC  token: 0x6000514
        public static Google.Protobuf.WellKnownTypes.Timestamp op_Addition(Google.Protobuf.WellKnownTypes.Timestamp lhs, Google.Protobuf.WellKnownTypes.Duration rhs) { }
        // RVA: 0x09AEEF74  token: 0x6000515
        public static Google.Protobuf.WellKnownTypes.Timestamp op_Subtraction(Google.Protobuf.WellKnownTypes.Timestamp lhs, Google.Protobuf.WellKnownTypes.Duration rhs) { }
        // RVA: 0x09AEE55C  token: 0x6000516
        public System.DateTime ToDateTime() { }
        // RVA: 0x09AEE4E4  token: 0x6000517
        public System.DateTimeOffset ToDateTimeOffset() { }
        // RVA: 0x09AEE22C  token: 0x6000518
        public static Google.Protobuf.WellKnownTypes.Timestamp FromDateTime(System.DateTime dateTime) { }
        // RVA: 0x09AEE1C4  token: 0x6000519
        public static Google.Protobuf.WellKnownTypes.Timestamp FromDateTimeOffset(System.DateTimeOffset dateTimeOffset) { }
        // RVA: 0x09AEE458  token: 0x600051A
        private static Google.Protobuf.WellKnownTypes.Timestamp Normalize(System.Int64 seconds, System.Int32 nanoseconds) { }
        // RVA: 0x09AEE6DC  token: 0x600051B
        private static System.String ToJson(System.Int64 seconds, System.Int32 nanoseconds, System.Boolean diagnosticOnly) { }
        // RVA: 0x09AEE0CC  token: 0x600051C
        public virtual System.Int32 CompareTo(Google.Protobuf.WellKnownTypes.Timestamp other) { }
        // RVA: 0x09AEEE08  token: 0x600051D
        public static System.Boolean op_LessThan(Google.Protobuf.WellKnownTypes.Timestamp a, Google.Protobuf.WellKnownTypes.Timestamp b) { }
        // RVA: 0x09AEED6C  token: 0x600051E
        public static System.Boolean op_GreaterThan(Google.Protobuf.WellKnownTypes.Timestamp a, Google.Protobuf.WellKnownTypes.Timestamp b) { }
        // RVA: 0x09AEEDE4  token: 0x600051F
        public static System.Boolean op_LessThanOrEqual(Google.Protobuf.WellKnownTypes.Timestamp a, Google.Protobuf.WellKnownTypes.Timestamp b) { }
        // RVA: 0x09AEED48  token: 0x6000520
        public static System.Boolean op_GreaterThanOrEqual(Google.Protobuf.WellKnownTypes.Timestamp a, Google.Protobuf.WellKnownTypes.Timestamp b) { }
        // RVA: 0x09AEED2C  token: 0x6000521
        public static System.Boolean op_Equality(Google.Protobuf.WellKnownTypes.Timestamp a, Google.Protobuf.WellKnownTypes.Timestamp b) { }
        // RVA: 0x09AEED90  token: 0x6000522
        public static System.Boolean op_Inequality(Google.Protobuf.WellKnownTypes.Timestamp a, Google.Protobuf.WellKnownTypes.Timestamp b) { }
        // RVA: 0x09AEE684  token: 0x6000523
        public virtual System.String ToDiagnosticString() { }
        // RVA: 0x09AEE998  token: 0x6000524
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000084  // size: 0x10
    public static class TypeReflection
    {
        // Fields
        private static Google.Protobuf.Reflection.FileDescriptor descriptor;  // static @ 0x0

        // Properties
        Google.Protobuf.Reflection.FileDescriptor Descriptor { get; /* RVA: 0x09AEFEA0 */ }

        // Methods
        // RVA: 0x09AEF10C  token: 0x6000529
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000085  // size: 0x14
    public sealed struct Syntax
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Google.Protobuf.WellKnownTypes.Syntax Proto2;  // const
        public static Google.Protobuf.WellKnownTypes.Syntax Proto3;  // const

    }

    // TypeToken: 0x2000086  // size: 0x48
    public sealed class Type : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Type> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 NameFieldNumber;  // const
        private System.String name_;  // 0x18
        public static System.Int32 FieldsFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.WellKnownTypes.Field> _repeated_fields_codec;  // static @ 0x8
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.Field> fields_;  // 0x20
        public static System.Int32 OneofsFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<System.String> _repeated_oneofs_codec;  // static @ 0x10
        private readonly Google.Protobuf.Collections.RepeatedField<System.String> oneofs_;  // 0x28
        public static System.Int32 OptionsFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.WellKnownTypes.Option> _repeated_options_codec;  // static @ 0x18
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.Option> options_;  // 0x30
        public static System.Int32 SourceContextFieldNumber;  // const
        private Google.Protobuf.WellKnownTypes.SourceContext sourceContext_;  // 0x38
        public static System.Int32 SyntaxFieldNumber;  // const
        private Google.Protobuf.WellKnownTypes.Syntax syntax_;  // 0x40

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Type> Parser { get; /* RVA: 0x09B06FAC */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B06F4C */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B073A4 */ }
        System.String Name { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x09B073E4 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.Field> Fields { get; /* RVA: 0x04D862C0 */ }
        Google.Protobuf.Collections.RepeatedField<System.String> Oneofs { get; /* RVA: 0x04D86240 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.Option> Options { get; /* RVA: 0x011F36E0 */ }
        Google.Protobuf.WellKnownTypes.SourceContext SourceContext { get; /* RVA: 0x04D85A50 */ set; /* RVA: 0x046A91B0 */ }
        Google.Protobuf.WellKnownTypes.Syntax Syntax { get; /* RVA: 0x04D865E0 */ set; /* RVA: 0x04D86610 */ }

        // Methods
        // RVA: 0x09B06CEC  token: 0x600052D
        public System.Void .ctor() { }
        // RVA: 0x09B06E18  token: 0x600052E
        public System.Void .ctor(Google.Protobuf.WellKnownTypes.Type other) { }
        // RVA: 0x09B06680  token: 0x600052F
        public virtual Google.Protobuf.WellKnownTypes.Type Clone() { }
        // RVA: 0x09B067EC  token: 0x6000539
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09B066E0  token: 0x600053A
        public virtual System.Boolean Equals(Google.Protobuf.WellKnownTypes.Type other) { }
        // RVA: 0x09B06850  token: 0x600053B
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B06A94  token: 0x600053C
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x600053D
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09B0721C  token: 0x600053E
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09B064E8  token: 0x600053F
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09B06930  token: 0x6000540
        public virtual System.Void MergeFrom(Google.Protobuf.WellKnownTypes.Type other) { }
        // RVA: 0x04D51840  token: 0x6000541
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09B06FFC  token: 0x6000542
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09B06AE0  token: 0x6000543
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000088  // size: 0x58
    public sealed class Field : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Field> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 KindFieldNumber;  // const
        private Google.Protobuf.WellKnownTypes.Field.Types.Kind kind_;  // 0x18
        public static System.Int32 CardinalityFieldNumber;  // const
        private Google.Protobuf.WellKnownTypes.Field.Types.Cardinality cardinality_;  // 0x1c
        public static System.Int32 NumberFieldNumber;  // const
        private System.Int32 number_;  // 0x20
        public static System.Int32 NameFieldNumber;  // const
        private System.String name_;  // 0x28
        public static System.Int32 TypeUrlFieldNumber;  // const
        private System.String typeUrl_;  // 0x30
        public static System.Int32 OneofIndexFieldNumber;  // const
        private System.Int32 oneofIndex_;  // 0x38
        public static System.Int32 PackedFieldNumber;  // const
        private System.Boolean packed_;  // 0x3c
        public static System.Int32 OptionsFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.WellKnownTypes.Option> _repeated_options_codec;  // static @ 0x8
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.Option> options_;  // 0x40
        public static System.Int32 JsonNameFieldNumber;  // const
        private System.String jsonName_;  // 0x48
        public static System.Int32 DefaultValueFieldNumber;  // const
        private System.String defaultValue_;  // 0x50

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Field> Parser { get; /* RVA: 0x09B01440 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B013E0 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B018A4 */ }
        Google.Protobuf.WellKnownTypes.Field.Types.Kind Kind { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x011EF9B0 */ }
        Google.Protobuf.WellKnownTypes.Field.Types.Cardinality Cardinality { get; /* RVA: 0x04D86310 */ set; /* RVA: 0x04D86320 */ }
        System.Int32 Number { get; /* RVA: 0x02B70F90 */ set; /* RVA: 0x04D86330 */ }
        System.String Name { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x09B019B4 */ }
        System.String TypeUrl { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x09B01A1C */ }
        System.Int32 OneofIndex { get; /* RVA: 0x04D863B0 */ set; /* RVA: 0x04D86430 */ }
        System.Boolean Packed { get; /* RVA: 0x04D86BD0 */ set; /* RVA: 0x04D86BE0 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.Option> Options { get; /* RVA: 0x04D85A60 */ }
        System.String JsonName { get; /* RVA: 0x04D86200 */ set; /* RVA: 0x09B0194C */ }
        System.String DefaultValue { get; /* RVA: 0x04D86270 */ set; /* RVA: 0x09B018E4 */ }

        // Methods
        // RVA: 0x09B0130C  token: 0x600054A
        public System.Void .ctor() { }
        // RVA: 0x09B0121C  token: 0x600054B
        public System.Void .ctor(Google.Protobuf.WellKnownTypes.Field other) { }
        // RVA: 0x09B00C34  token: 0x600054C
        public virtual Google.Protobuf.WellKnownTypes.Field Clone() { }
        // RVA: 0x09B00DA0  token: 0x6000560
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09B00C94  token: 0x6000561
        public virtual System.Boolean Equals(Google.Protobuf.WellKnownTypes.Field other) { }
        // RVA: 0x09B00E04  token: 0x6000562
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B01060  token: 0x6000563
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x6000564
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09B016A4  token: 0x6000565
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09B00A1C  token: 0x6000566
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09B00F2C  token: 0x6000567
        public virtual System.Void MergeFrom(Google.Protobuf.WellKnownTypes.Field other) { }
        // RVA: 0x04D51840  token: 0x6000568
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09B01490  token: 0x6000569
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09B010AC  token: 0x600056A
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200008D  // size: 0x40
    public sealed class Enum : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Enum> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 NameFieldNumber;  // const
        private System.String name_;  // 0x18
        public static System.Int32 EnumvalueFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.WellKnownTypes.EnumValue> _repeated_enumvalue_codec;  // static @ 0x8
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.EnumValue> enumvalue_;  // 0x20
        public static System.Int32 OptionsFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.WellKnownTypes.Option> _repeated_options_codec;  // static @ 0x10
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.Option> options_;  // 0x28
        public static System.Int32 SourceContextFieldNumber;  // const
        private Google.Protobuf.WellKnownTypes.SourceContext sourceContext_;  // 0x30
        public static System.Int32 SyntaxFieldNumber;  // const
        private Google.Protobuf.WellKnownTypes.Syntax syntax_;  // 0x38

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Enum> Parser { get; /* RVA: 0x09AFDDCC */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09AFDD6C */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09AFE138 */ }
        System.String Name { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x09AFE178 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.EnumValue> Enumvalue { get; /* RVA: 0x04D862C0 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.Option> Options { get; /* RVA: 0x04D86240 */ }
        Google.Protobuf.WellKnownTypes.SourceContext SourceContext { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }
        Google.Protobuf.WellKnownTypes.Syntax Syntax { get; /* RVA: 0x04D863B0 */ set; /* RVA: 0x04D86430 */ }

        // Methods
        // RVA: 0x09AFDB88  token: 0x6000571
        public System.Void .ctor() { }
        // RVA: 0x09AFDC68  token: 0x6000572
        public System.Void .ctor(Google.Protobuf.WellKnownTypes.Enum other) { }
        // RVA: 0x09AFD5C8  token: 0x6000573
        public virtual Google.Protobuf.WellKnownTypes.Enum Clone() { }
        // RVA: 0x09AFD628  token: 0x600057C
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09AFD68C  token: 0x600057D
        public virtual System.Boolean Equals(Google.Protobuf.WellKnownTypes.Enum other) { }
        // RVA: 0x09AFD76C  token: 0x600057E
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09AFD964  token: 0x600057F
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x6000580
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09AFDFEC  token: 0x6000581
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09AFD45C  token: 0x6000582
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09AFD828  token: 0x6000583
        public virtual System.Void MergeFrom(Google.Protobuf.WellKnownTypes.Enum other) { }
        // RVA: 0x04D51840  token: 0x6000584
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09AFDE1C  token: 0x6000585
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09AFD9B0  token: 0x6000586
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200008F  // size: 0x30
    public sealed class EnumValue : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.EnumValue> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 NameFieldNumber;  // const
        private System.String name_;  // 0x18
        public static System.Int32 NumberFieldNumber;  // const
        private System.Int32 number_;  // 0x20
        public static System.Int32 OptionsFieldNumber;  // const
        private static readonly Google.Protobuf.FieldCodec<Google.Protobuf.WellKnownTypes.Option> _repeated_options_codec;  // static @ 0x8
        private readonly Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.Option> options_;  // 0x28

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.EnumValue> Parser { get; /* RVA: 0x09AFD14C */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09AFD0EC */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09AFD3B4 */ }
        System.String Name { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x09AFD3F4 */ }
        System.Int32 Number { get; /* RVA: 0x02B70F90 */ set; /* RVA: 0x04D86330 */ }
        Google.Protobuf.Collections.RepeatedField<Google.Protobuf.WellKnownTypes.Option> Options { get; /* RVA: 0x04D86240 */ }

        // Methods
        // RVA: 0x09AFD054  token: 0x600058D
        public System.Void .ctor() { }
        // RVA: 0x09AFCFB4  token: 0x600058E
        public System.Void .ctor(Google.Protobuf.WellKnownTypes.EnumValue other) { }
        // RVA: 0x09AFCBF8  token: 0x600058F
        public virtual Google.Protobuf.WellKnownTypes.EnumValue Clone() { }
        // RVA: 0x09AFCC58  token: 0x6000595
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09AFCCBC  token: 0x6000596
        public virtual System.Boolean Equals(Google.Protobuf.WellKnownTypes.EnumValue other) { }
        // RVA: 0x063CF728  token: 0x6000597
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09AFCDF8  token: 0x6000598
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x6000599
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x09AFD2C0  token: 0x600059A
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09AFCAFC  token: 0x600059B
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09AFCD54  token: 0x600059C
        public virtual System.Void MergeFrom(Google.Protobuf.WellKnownTypes.EnumValue other) { }
        // RVA: 0x04D51840  token: 0x600059D
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09AFD19C  token: 0x600059E
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09AFCE44  token: 0x600059F
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000091  // size: 0x28
    public sealed class Option : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Option> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 NameFieldNumber;  // const
        private System.String name_;  // 0x18
        public static System.Int32 ValueFieldNumber;  // const
        private Google.Protobuf.WellKnownTypes.Any value_;  // 0x20

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Option> Parser { get; /* RVA: 0x09B056BC */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B0565C */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B05800 */ }
        System.String Name { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x09B05840 */ }
        Google.Protobuf.WellKnownTypes.Any Value { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }

        // Methods
        // RVA: 0x09B0561C  token: 0x60005A6
        public System.Void .ctor() { }
        // RVA: 0x09B055A0  token: 0x60005A7
        public System.Void .ctor(Google.Protobuf.WellKnownTypes.Option other) { }
        // RVA: 0x09B052B0  token: 0x60005A8
        public virtual Google.Protobuf.WellKnownTypes.Option Clone() { }
        // RVA: 0x09B05310  token: 0x60005AD
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x063D1E6C  token: 0x60005AE
        public virtual System.Boolean Equals(Google.Protobuf.WellKnownTypes.Option other) { }
        // RVA: 0x063D1ED4  token: 0x60005AF
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B05450  token: 0x60005B0
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x60005B1
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x063D2324  token: 0x60005B2
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09B05204  token: 0x60005B3
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09B05374  token: 0x60005B4
        public virtual System.Void MergeFrom(Google.Protobuf.WellKnownTypes.Option other) { }
        // RVA: 0x04D51840  token: 0x60005B5
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09B0570C  token: 0x60005B6
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09B0549C  token: 0x60005B7
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000093  // size: 0x10
    public static class WrappersReflection
    {
        // Fields
        private static Google.Protobuf.Reflection.FileDescriptor descriptor;  // static @ 0x0
        private static System.Int32 WrapperValueFieldNumber;  // const

        // Properties
        Google.Protobuf.Reflection.FileDescriptor Descriptor { get; /* RVA: 0x09B09530 */ }

        // Methods
        // RVA: 0x09B089CC  token: 0x60005BC
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000094  // size: 0x20
    public sealed class DoubleValue : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.DoubleValue> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 ValueFieldNumber;  // const
        private System.Double value_;  // 0x18

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.DoubleValue> Parser { get; /* RVA: 0x09AFBB10 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09AFBAB0 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09AFBB60 */ }
        System.Double Value { get; /* RVA: 0x04D86360 */ set; /* RVA: 0x04D86370 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60005C0
        public System.Void .ctor() { }
        // RVA: 0x0638E034  token: 0x60005C1
        public System.Void .ctor(Google.Protobuf.WellKnownTypes.DoubleValue other) { }
        // RVA: 0x09AFB77C  token: 0x60005C2
        public virtual Google.Protobuf.WellKnownTypes.DoubleValue Clone() { }
        // RVA: 0x09AFB868  token: 0x60005C5
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09AFB7DC  token: 0x60005C6
        public virtual System.Boolean Equals(Google.Protobuf.WellKnownTypes.DoubleValue other) { }
        // RVA: 0x09AFB8CC  token: 0x60005C7
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09AFB960  token: 0x60005C8
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x60005C9
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x06619F80  token: 0x60005CA
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x06619B4C  token: 0x60005CB
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x06619D6C  token: 0x60005CC
        public virtual System.Void MergeFrom(Google.Protobuf.WellKnownTypes.DoubleValue other) { }
        // RVA: 0x04D51840  token: 0x60005CD
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x06619F0C  token: 0x60005CE
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09AFB9AC  token: 0x60005CF
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000096  // size: 0x20
    public sealed class FloatValue : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.FloatValue> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 ValueFieldNumber;  // const
        private System.Single value_;  // 0x18

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.FloatValue> Parser { get; /* RVA: 0x09B0422C */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B041CC */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B0427C */ }
        System.Single Value { get; /* RVA: 0x04D86380 */ set; /* RVA: 0x04D86390 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60005D6
        public System.Void .ctor() { }
        // RVA: 0x063BE028  token: 0x60005D7
        public System.Void .ctor(Google.Protobuf.WellKnownTypes.FloatValue other) { }
        // RVA: 0x09B03E94  token: 0x60005D8
        public virtual Google.Protobuf.WellKnownTypes.FloatValue Clone() { }
        // RVA: 0x09B03EF4  token: 0x60005DB
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09B03F58  token: 0x60005DC
        public virtual System.Boolean Equals(Google.Protobuf.WellKnownTypes.FloatValue other) { }
        // RVA: 0x09B03FE8  token: 0x60005DD
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B0407C  token: 0x60005DE
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x60005DF
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x066536E8  token: 0x60005E0
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x066532B4  token: 0x60005E1
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x066534D8  token: 0x60005E2
        public virtual System.Void MergeFrom(Google.Protobuf.WellKnownTypes.FloatValue other) { }
        // RVA: 0x04D51840  token: 0x60005E3
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x06653674  token: 0x60005E4
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09B040C8  token: 0x60005E5
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000098  // size: 0x20
    public sealed class Int64Value : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Int64Value> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 ValueFieldNumber;  // const
        private System.Int64 value_;  // 0x18

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Int64Value> Parser { get; /* RVA: 0x09B04910 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B048B0 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B04960 */ }
        System.Int64 Value { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x011EC9D0 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60005EC
        public System.Void .ctor() { }
        // RVA: 0x0638E034  token: 0x60005ED
        public System.Void .ctor(Google.Protobuf.WellKnownTypes.Int64Value other) { }
        // RVA: 0x09B0469C  token: 0x60005EE
        public virtual Google.Protobuf.WellKnownTypes.Int64Value Clone() { }
        // RVA: 0x09B046FC  token: 0x60005F1
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x0638DE34  token: 0x60005F2
        public virtual System.Boolean Equals(Google.Protobuf.WellKnownTypes.Int64Value other) { }
        // RVA: 0x0638DE60  token: 0x60005F3
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B04760  token: 0x60005F4
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x60005F5
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x0638E194  token: 0x60005F6
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09B0462C  token: 0x60005F7
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x0638DEA4  token: 0x60005F8
        public virtual System.Void MergeFrom(Google.Protobuf.WellKnownTypes.Int64Value other) { }
        // RVA: 0x04D51840  token: 0x60005F9
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x0638E120  token: 0x60005FA
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09B047AC  token: 0x60005FB
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200009A  // size: 0x20
    public sealed class UInt64Value : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.UInt64Value> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 ValueFieldNumber;  // const
        private System.UInt64 value_;  // 0x18

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.UInt64Value> Parser { get; /* RVA: 0x09B0893C */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B088DC */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B0898C */ }
        System.UInt64 Value { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x011EC9D0 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000602
        public System.Void .ctor() { }
        // RVA: 0x0638E034  token: 0x6000603
        public System.Void .ctor(Google.Protobuf.WellKnownTypes.UInt64Value other) { }
        // RVA: 0x09B086C8  token: 0x6000604
        public virtual Google.Protobuf.WellKnownTypes.UInt64Value Clone() { }
        // RVA: 0x09B08728  token: 0x6000607
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x0638DE34  token: 0x6000608
        public virtual System.Boolean Equals(Google.Protobuf.WellKnownTypes.UInt64Value other) { }
        // RVA: 0x063A2E14  token: 0x6000609
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B0878C  token: 0x600060A
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x600060B
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x0638E194  token: 0x600060C
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09B08658  token: 0x600060D
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x0638DEA4  token: 0x600060E
        public virtual System.Void MergeFrom(Google.Protobuf.WellKnownTypes.UInt64Value other) { }
        // RVA: 0x04D51840  token: 0x600060F
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x0638E120  token: 0x6000610
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09B087D8  token: 0x6000611
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200009C  // size: 0x20
    public sealed class Int32Value : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Int32Value> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 ValueFieldNumber;  // const
        private System.Int32 value_;  // 0x18

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.Int32Value> Parser { get; /* RVA: 0x09B0459C */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B0453C */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B045EC */ }
        System.Int32 Value { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x011EF9B0 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000618
        public System.Void .ctor() { }
        // RVA: 0x063BE028  token: 0x6000619
        public System.Void .ctor(Google.Protobuf.WellKnownTypes.Int32Value other) { }
        // RVA: 0x09B04328  token: 0x600061A
        public virtual Google.Protobuf.WellKnownTypes.Int32Value Clone() { }
        // RVA: 0x09B04388  token: 0x600061D
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x063BDE20  token: 0x600061E
        public virtual System.Boolean Equals(Google.Protobuf.WellKnownTypes.Int32Value other) { }
        // RVA: 0x063BDEB0  token: 0x600061F
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B043EC  token: 0x6000620
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x6000621
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x011EF950  token: 0x6000622
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09B042BC  token: 0x6000623
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x063BDEE8  token: 0x6000624
        public virtual System.Void MergeFrom(Google.Protobuf.WellKnownTypes.Int32Value other) { }
        // RVA: 0x04D51840  token: 0x6000625
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x063BE0B4  token: 0x6000626
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09B04438  token: 0x6000627
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200009E  // size: 0x20
    public sealed class UInt32Value : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.UInt32Value> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 ValueFieldNumber;  // const
        private System.UInt32 value_;  // 0x18

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.UInt32Value> Parser { get; /* RVA: 0x09B085C8 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B08568 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B08618 */ }
        System.UInt32 Value { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x011EF9B0 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x600062E
        public System.Void .ctor() { }
        // RVA: 0x063BE028  token: 0x600062F
        public System.Void .ctor(Google.Protobuf.WellKnownTypes.UInt32Value other) { }
        // RVA: 0x09B08354  token: 0x6000630
        public virtual Google.Protobuf.WellKnownTypes.UInt32Value Clone() { }
        // RVA: 0x09B083B4  token: 0x6000633
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x063BDE20  token: 0x6000634
        public virtual System.Boolean Equals(Google.Protobuf.WellKnownTypes.UInt32Value other) { }
        // RVA: 0x063BDEB0  token: 0x6000635
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B08418  token: 0x6000636
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x6000637
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x064B2684  token: 0x6000638
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09B082E8  token: 0x6000639
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x063BDEE8  token: 0x600063A
        public virtual System.Void MergeFrom(Google.Protobuf.WellKnownTypes.UInt32Value other) { }
        // RVA: 0x04D51840  token: 0x600063B
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x063BE0B4  token: 0x600063C
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09B08464  token: 0x600063D
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000A0  // size: 0x20
    public sealed class BoolValue : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.BoolValue> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 ValueFieldNumber;  // const
        private System.Boolean value_;  // 0x18

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.BoolValue> Parser { get; /* RVA: 0x09AF5D34 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09AF5CD4 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09AF5D84 */ }
        System.Boolean Value { get; /* RVA: 0x04D86340 */ set; /* RVA: 0x04D86350 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000644
        public System.Void .ctor() { }
        // RVA: 0x063D6A44  token: 0x6000645
        public System.Void .ctor(Google.Protobuf.WellKnownTypes.BoolValue other) { }
        // RVA: 0x09AF5A58  token: 0x6000646
        public virtual Google.Protobuf.WellKnownTypes.BoolValue Clone() { }
        // RVA: 0x09AF5AB8  token: 0x6000649
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x063D6870  token: 0x600064A
        public virtual System.Boolean Equals(Google.Protobuf.WellKnownTypes.BoolValue other) { }
        // RVA: 0x09AF5B1C  token: 0x600064B
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09AF5B84  token: 0x600064C
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x600064D
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x063D6B3C  token: 0x600064E
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x063D677C  token: 0x600064F
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x063D6904  token: 0x6000650
        public virtual System.Void MergeFrom(Google.Protobuf.WellKnownTypes.BoolValue other) { }
        // RVA: 0x04D51840  token: 0x6000651
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x063D6AD0  token: 0x6000652
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09AF5BD0  token: 0x6000653
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000A2  // size: 0x20
    public sealed class StringValue : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.StringValue> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 ValueFieldNumber;  // const
        private System.String value_;  // 0x18

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.StringValue> Parser { get; /* RVA: 0x09B06374 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09B06314 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09B06440 */ }
        System.String Value { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x09B06480 */ }

        // Methods
        // RVA: 0x09B0627C  token: 0x600065A
        public System.Void .ctor() { }
        // RVA: 0x09B062BC  token: 0x600065B
        public System.Void .ctor(Google.Protobuf.WellKnownTypes.StringValue other) { }
        // RVA: 0x09B06008  token: 0x600065C
        public virtual Google.Protobuf.WellKnownTypes.StringValue Clone() { }
        // RVA: 0x09B06068  token: 0x600065F
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x0639C940  token: 0x6000660
        public virtual System.Boolean Equals(Google.Protobuf.WellKnownTypes.StringValue other) { }
        // RVA: 0x0639C9F8  token: 0x6000661
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09B0612C  token: 0x6000662
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x6000663
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x0639CDC4  token: 0x6000664
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09B05F8C  token: 0x6000665
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09B060CC  token: 0x6000666
        public virtual System.Void MergeFrom(Google.Protobuf.WellKnownTypes.StringValue other) { }
        // RVA: 0x04D51840  token: 0x6000667
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09B063C4  token: 0x6000668
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09B06178  token: 0x6000669
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000A4  // size: 0x20
    public sealed class BytesValue : Google.Protobuf.IMessage`1, Google.Protobuf.IMessage, System.IEquatable`1, Google.Protobuf.IDeepCloneable`1, Google.Protobuf.IBufferMessage
    {
        // Fields
        private static readonly Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.BytesValue> _parser;  // static @ 0x0
        private Google.Protobuf.UnknownFieldSet _unknownFields;  // 0x10
        public static System.Int32 ValueFieldNumber;  // const
        private Google.Protobuf.ByteString value_;  // 0x18

        // Properties
        Google.Protobuf.MessageParser<Google.Protobuf.WellKnownTypes.BytesValue> Parser { get; /* RVA: 0x09AF6258 */ }
        Google.Protobuf.Reflection.MessageDescriptor Descriptor { get; /* RVA: 0x09AF61F8 */ }
        Google.Protobuf.Reflection.MessageDescriptor pb::Google.Protobuf.IMessage.Descriptor { get; /* RVA: 0x09AF6324 */ }
        Google.Protobuf.ByteString Value { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x09AF6364 */ }

        // Methods
        // RVA: 0x09AF61A4  token: 0x6000670
        public System.Void .ctor() { }
        // RVA: 0x09AF614C  token: 0x6000671
        public System.Void .ctor(Google.Protobuf.WellKnownTypes.BytesValue other) { }
        // RVA: 0x09AF5E44  token: 0x6000672
        public virtual Google.Protobuf.WellKnownTypes.BytesValue Clone() { }
        // RVA: 0x09AF5F38  token: 0x6000675
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x09AF5EA4  token: 0x6000676
        public virtual System.Boolean Equals(Google.Protobuf.WellKnownTypes.BytesValue other) { }
        // RVA: 0x063E7124  token: 0x6000677
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x09AF5FFC  token: 0x6000678
        public virtual System.String ToString() { }
        // RVA: 0x04D52400  token: 0x6000679
        public virtual System.Void WriteTo(Google.Protobuf.CodedOutputStream output) { }
        // RVA: 0x063E745C  token: 0x600067A
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalWriteTo(Google.Protobuf.WriteContext& output) { }
        // RVA: 0x09AF5DC4  token: 0x600067B
        public virtual System.Int32 CalculateSize() { }
        // RVA: 0x09AF5F9C  token: 0x600067C
        public virtual System.Void MergeFrom(Google.Protobuf.WellKnownTypes.BytesValue other) { }
        // RVA: 0x04D51840  token: 0x600067D
        public virtual System.Void MergeFrom(Google.Protobuf.CodedInputStream input) { }
        // RVA: 0x09AF62A8  token: 0x600067E
        private virtual System.Void pb::Google.Protobuf.IBufferMessage.InternalMergeFrom(Google.Protobuf.ParseContext& input) { }
        // RVA: 0x09AF6048  token: 0x600067F
        private static System.Void .cctor() { }

    }

}

namespace System.Diagnostics.CodeAnalysis
{

    // TypeToken: 0x2000002  // size: 0x18
    public sealed class DynamicallyAccessedMembersAttribute : System.Attribute
    {
        // Fields
        private readonly System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes <MemberTypes>k__BackingField;  // 0x10

        // Properties
        System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes MemberTypes { get; /* RVA: 0x02B2E2D0 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000001
        public System.Void .ctor(System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes memberTypes) { }

    }

    // TypeToken: 0x2000003  // size: 0x14
    public sealed struct DynamicallyAccessedMemberTypes
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes None;  // const
        public static System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes PublicParameterlessConstructor;  // const
        public static System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes PublicConstructors;  // const
        public static System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes NonPublicConstructors;  // const
        public static System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes PublicMethods;  // const
        public static System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes NonPublicMethods;  // const
        public static System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes PublicFields;  // const
        public static System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes NonPublicFields;  // const
        public static System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes PublicNestedTypes;  // const
        public static System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes NonPublicNestedTypes;  // const
        public static System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes PublicProperties;  // const
        public static System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes NonPublicProperties;  // const
        public static System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes PublicEvents;  // const
        public static System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes NonPublicEvents;  // const
        public static System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes Interfaces;  // const
        public static System.Diagnostics.CodeAnalysis.DynamicallyAccessedMemberTypes All;  // const

    }

    // TypeToken: 0x2000004  // size: 0x20
    public sealed class RequiresUnreferencedCodeAttribute : System.Attribute
    {
        // Fields
        private readonly System.String <Message>k__BackingField;  // 0x10
        private System.String <Url>k__BackingField;  // 0x18

        // Properties
        System.String Message { get; /* RVA: 0x02B2ECC0 */ }
        System.String Url { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x6000003
        public System.Void .ctor(System.String message) { }

    }

    // TypeToken: 0x2000005  // size: 0x40
    public sealed class UnconditionalSuppressMessageAttribute : System.Attribute
    {
        // Fields
        private readonly System.String <Category>k__BackingField;  // 0x10
        private readonly System.String <CheckId>k__BackingField;  // 0x18
        private System.String <Scope>k__BackingField;  // 0x20
        private System.String <Target>k__BackingField;  // 0x28
        private System.String <MessageId>k__BackingField;  // 0x30
        private System.String <Justification>k__BackingField;  // 0x38

        // Properties
        System.String Category { get; /* RVA: 0x02B2ECC0 */ }
        System.String CheckId { get; /* RVA: 0x0385B100 */ }
        System.String Scope { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        System.String Target { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }
        System.String MessageId { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }
        System.String Justification { get; /* RVA: 0x04D85A50 */ set; /* RVA: 0x046A91B0 */ }

        // Methods
        // RVA: 0x032AF420  token: 0x6000007
        public System.Void .ctor(System.String category, System.String checkId) { }

    }

}

