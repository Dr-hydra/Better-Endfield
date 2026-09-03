// ========================================================
// Dumped by @desirepro
// Assembly: System.Net.Http.dll
// Classes:  84
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000003  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly System.Net.Http.HttpClientHandler.<>c <>9;  // static @ 0x0
        public static System.Net.Security.LocalCertificateSelectionCallback <>9__23_1;  // static @ 0x8

        // Methods
        // RVA: 0x0AE27950  token: 0x600000C
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600000D
        public System.Void .ctor() { }
        // RVA: 0x0AE27948  token: 0x600000E
        private System.Security.Cryptography.X509Certificates.X509Certificate <set_ClientCertificateOptions>b__23_1(System.Object sender, System.String targetHost, System.Security.Cryptography.X509Certificates.X509CertificateCollection localCertificates, System.Security.Cryptography.X509Certificates.X509Certificate remoteCertificate, System.String[] acceptableIssuers) { }

    }

    // TypeToken: 0x2000006  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly System.Net.Http.MonoWebRequestHandler.<>c <>9;  // static @ 0x0
        public static System.Func<System.String,System.Boolean> <>9__95_0;  // static @ 0x8
        public static System.Func<System.String,System.Boolean> <>9__96_0;  // static @ 0x10
        public static System.Action<System.Object> <>9__99_0;  // static @ 0x18

        // Methods
        // RVA: 0x0AE27AE0  token: 0x6000021
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000022
        public System.Void .ctor() { }
        // RVA: 0x0AE27680  token: 0x6000023
        private System.Boolean <GetConnectionKeepAlive>b__95_0(System.String l) { }
        // RVA: 0x0AE27644  token: 0x6000024
        private System.Boolean <CreateWebRequest>b__96_0(System.String l) { }
        // RVA: 0x0AE276C4  token: 0x6000025
        private System.Void <SendAsync>b__99_0(System.Object l) { }

    }

    // TypeToken: 0x2000007  // size: 0xB8
    public sealed struct <SendAsync>d__99 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Net.Http.HttpResponseMessage> <>t__builder;  // 0x18
        public System.Net.Http.MonoWebRequestHandler <>4__this;  // 0x30
        public System.Threading.CancellationToken cancellationToken;  // 0x38
        public System.Net.Http.HttpRequestMessage request;  // 0x40
        private System.Net.HttpWebRequest <wrequest>5__2;  // 0x48
        private System.Net.HttpWebResponse <wresponse>5__3;  // 0x50
        private System.Threading.CancellationTokenRegistration <>7__wrap3;  // 0x58
        private System.Net.Http.HttpContent <content>5__5;  // 0x70
        private System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;  // 0x78
        private System.IO.Stream <stream>5__6;  // 0x88
        private System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.IO.Stream> <>u__2;  // 0x90
        private System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.Net.WebResponse> <>u__3;  // 0xa0
        private System.Runtime.CompilerServices.TaskAwaiter<System.Net.Http.HttpResponseMessage> <>u__4;  // 0xb0

        // Methods
        // RVA: 0x0AE24438  token: 0x6000026
        private virtual System.Void MoveNext() { }
        // RVA: 0x0AE26054  token: 0x6000027
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x200000C  // size: 0x80
    public sealed struct <SendAsyncWorker>d__47 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.Net.Http.HttpResponseMessage> <>t__builder;  // 0x18
        public System.Net.Http.HttpClient <>4__this;  // 0x30
        public System.Threading.CancellationToken cancellationToken;  // 0x38
        public System.Net.Http.HttpRequestMessage request;  // 0x40
        public System.Net.Http.HttpCompletionOption completionOption;  // 0x48
        private System.Threading.CancellationTokenSource <lcts>5__2;  // 0x50
        private System.Net.Http.HttpResponseMessage <response>5__3;  // 0x58
        private System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter<System.Net.Http.HttpResponseMessage> <>u__1;  // 0x60
        private System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__2;  // 0x70

        // Methods
        // RVA: 0x0AE260B4  token: 0x600003A
        private virtual System.Void MoveNext() { }
        // RVA: 0x0AE267E4  token: 0x600003B
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x200000F  // size: 0x58
    public sealed class FixedMemoryStream : System.IO.MemoryStream
    {
        // Fields
        private readonly System.Int64 maxSize;  // 0x50

        // Methods
        // RVA: 0x0AE1594C  token: 0x600004C
        public System.Void .ctor(System.Int64 maxSize) { }
        // RVA: 0x0AE157F8  token: 0x600004D
        private System.Void CheckOverflow(System.Int32 count) { }
        // RVA: 0x0AE158B8  token: 0x600004E
        public virtual System.Void WriteByte(System.Byte value) { }
        // RVA: 0x0AE158F0  token: 0x600004F
        public virtual System.Void Write(System.Byte[] buffer, System.Int32 offset, System.Int32 count) { }

    }

    // TypeToken: 0x2000010  // size: 0x50
    public sealed struct <LoadIntoBufferAsync>d__17 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncTaskMethodBuilder <>t__builder;  // 0x18
        public System.Net.Http.HttpContent <>4__this;  // 0x30
        public System.Int64 maxBufferSize;  // 0x38
        private System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;  // 0x40

        // Methods
        // RVA: 0x0AE23DA0  token: 0x6000050
        private virtual System.Void MoveNext() { }
        // RVA: 0x0AE24018  token: 0x6000051
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x2000011  // size: 0x48
    public sealed struct <ReadAsStringAsync>d__20 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncTaskMethodBuilder<System.String> <>t__builder;  // 0x18
        public System.Net.Http.HttpContent <>4__this;  // 0x30
        private System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;  // 0x38

        // Methods
        // RVA: 0x0AE24074  token: 0x6000052
        private virtual System.Void MoveNext() { }
        // RVA: 0x0AE243D8  token: 0x6000053
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x2000019  // size: 0x70
    public sealed struct <SerializeToStreamAsync>d__8 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncTaskMethodBuilder <>t__builder;  // 0x18
        public System.Net.Http.MultipartContent <>4__this;  // 0x30
        public System.IO.Stream stream;  // 0x38
        public System.Net.TransportContext context;  // 0x40
        private System.Text.StringBuilder <sb>5__2;  // 0x48
        private System.Int32 <i>5__3;  // 0x50
        private System.Net.Http.HttpContent <c>5__4;  // 0x58
        private System.Runtime.CompilerServices.ConfiguredTaskAwaitable.ConfiguredTaskAwaiter <>u__1;  // 0x60

        // Methods
        // RVA: 0x0AE26844  token: 0x600008D
        private virtual System.Void MoveNext() { }
        // RVA: 0x0AE275E8  token: 0x600008E
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x2000029
    public class HeaderTypeInfo`2 : System.Net.Http.Headers.HeaderInfo
    {
        // Fields
        private readonly System.Net.Http.Headers.TryParseDelegate<T> parser;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000110
        public System.Void .ctor(System.String name, System.Net.Http.Headers.TryParseDelegate<T> parser, System.Net.Http.Headers.HttpHeaderKind headerKind) { }
        // RVA: -1  // not resolved  token: 0x6000111
        public virtual System.Void AddToCollection(System.Object collection, System.Object value) { }
        // RVA: -1  // not resolved  token: 0x6000112
        protected virtual System.Object CreateCollection(System.Net.Http.Headers.HttpHeaders headers, System.Net.Http.Headers.HeaderInfo headerInfo) { }
        // RVA: -1  // not resolved  token: 0x6000113
        public virtual System.Collections.Generic.List<System.String> ToStringCollection(System.Object collection) { }
        // RVA: -1  // not resolved  token: 0x6000114
        public virtual System.Boolean TryParse(System.String value, System.Object& result) { }

    }

    // TypeToken: 0x200002A
    public class CollectionHeaderTypeInfo`2 : HeaderTypeInfo`2
    {
        // Fields
        private readonly System.Int32 minimalCount;  // 0x0
        private readonly System.String separator;  // 0x0
        private readonly System.Net.Http.Headers.TryParseListDelegate<T> parser;  // 0x0

        // Properties
        System.String Separator { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000115
        public System.Void .ctor(System.String name, System.Net.Http.Headers.TryParseListDelegate<T> parser, System.Net.Http.Headers.HttpHeaderKind headerKind, System.Int32 minimalCount, System.String separator) { }
        // RVA: -1  // not resolved  token: 0x6000117
        public virtual System.Boolean TryParse(System.String value, System.Object& result) { }

    }

    // TypeToken: 0x200002F  // size: 0x28
    public class HeaderBucket
    {
        // Fields
        public System.Object Parsed;  // 0x10
        private System.Collections.Generic.List<System.String> values;  // 0x18
        public readonly System.Func<System.Object,System.String> CustomToString;  // 0x20

        // Properties
        System.Boolean HasStringValues { get; /* RVA: 0x0AE159C4 */ }
        System.Collections.Generic.List<System.String> Values { get; /* RVA: 0x0AE159DC */ set; /* RVA: 0x05392C40 */ }

        // Methods
        // RVA: 0x04D7D160  token: 0x6000142
        public System.Void .ctor(System.Object parsed, System.Func<System.Object,System.String> converter) { }
        // RVA: 0x0AE15978  token: 0x6000146
        public System.String ParsedToString() { }

    }

    // TypeToken: 0x2000030  // size: 0x58
    public sealed class <GetEnumerator>d__19 : System.Collections.Generic.IEnumerator`1, System.IDisposable, System.Collections.IEnumerator
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Collections.Generic.KeyValuePair<System.String,System.Collections.Generic.IEnumerable<System.String>> <>2__current;  // 0x18
        public System.Net.Http.Headers.HttpHeaders <>4__this;  // 0x28
        private System.Collections.Generic.Dictionary.Enumerator<System.String,System.Net.Http.Headers.HttpHeaders.HeaderBucket> <>7__wrap1;  // 0x30

        // Properties
        System.Collections.Generic.KeyValuePair<System.String,System.Collections.Generic.IEnumerable<System.String>> System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,System.Collections.Generic.IEnumerable<System.String>>>.Current { get; /* RVA: 0x04D890C0 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0AE23CF4 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000147
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x0AE23D3C  token: 0x6000148
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0AE23994  token: 0x6000149
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x04D88D80  token: 0x600014A
        private System.Void <>m__Finally1() { }
        // RVA: 0x0AE23CA8  token: 0x600014C
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000032  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly System.Net.Http.Headers.HttpRequestHeaders.<>c <>9;  // static @ 0x0
        public static System.Predicate<System.String> <>9__19_0;  // static @ 0x8
        public static System.Predicate<System.Net.Http.Headers.TransferCodingHeaderValue> <>9__29_0;  // static @ 0x10
        public static System.Predicate<System.Net.Http.Headers.TransferCodingHeaderValue> <>9__71_0;  // static @ 0x18

        // Methods
        // RVA: 0x0AE279B4  token: 0x6000158
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000159
        public System.Void .ctor() { }
        // RVA: 0x0AE27864  token: 0x600015A
        private System.Boolean <get_ConnectionClose>b__19_0(System.String l) { }
        // RVA: 0x0AE278A8  token: 0x600015B
        private System.Boolean <get_ExpectContinue>b__29_0(System.Net.Http.Headers.TransferCodingHeaderValue l) { }
        // RVA: 0x0AE278F8  token: 0x600015C
        private System.Boolean <get_TransferEncodingChunked>b__71_0(System.Net.Http.Headers.TransferCodingHeaderValue l) { }

    }

    // TypeToken: 0x2000035  // size: 0x14
    public sealed struct Type
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Net.Http.Headers.Token.Type Error;  // const
        public static System.Net.Http.Headers.Token.Type End;  // const
        public static System.Net.Http.Headers.Token.Type Token;  // const
        public static System.Net.Http.Headers.Token.Type QuotedString;  // const
        public static System.Net.Http.Headers.Token.Type SeparatorEqual;  // const
        public static System.Net.Http.Headers.Token.Type SeparatorSemicolon;  // const
        public static System.Net.Http.Headers.Token.Type SeparatorSlash;  // const
        public static System.Net.Http.Headers.Token.Type SeparatorDash;  // const
        public static System.Net.Http.Headers.Token.Type SeparatorComma;  // const
        public static System.Net.Http.Headers.Token.Type OpenParens;  // const

    }

    // TypeToken: 0x2000038  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly System.Net.Http.Headers.MediaTypeHeaderValue.<>c <>9;  // static @ 0x0
        public static System.Predicate<System.Net.Http.Headers.NameValueHeaderValue> <>9__6_0;  // static @ 0x8

        // Methods
        // RVA: 0x0AE27A7C  token: 0x6000189
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600018A
        public System.Void .ctor() { }
        // RVA: 0x0AE27814  token: 0x600018B
        private System.Boolean <get_CharSet>b__6_0(System.Net.Http.Headers.NameValueHeaderValue l) { }

    }

    // TypeToken: 0x200003D  // size: 0x10
    public static class Token
    {
        // Methods
        // RVA: 0x0AE2304C  token: 0x60001A7
        public static System.Void Check(System.String s) { }
        // RVA: 0x0AE231AC  token: 0x60001A8
        public static System.Boolean TryCheck(System.String s) { }

    }

    // TypeToken: 0x200003F  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly System.Net.Http.Headers.Parser.DateTime.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x0AE27B44  token: 0x60001AB
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60001AC
        public System.Void .ctor() { }
        // RVA: 0x0AE27724  token: 0x60001AD
        private System.String <.cctor>b__2_0(System.Object l) { }

    }

    // TypeToken: 0x200003E  // size: 0x10
    public static class DateTime
    {
        // Fields
        public static readonly System.Func<System.Object,System.String> ToString;  // static @ 0x0

        // Methods
        // RVA: 0x0AE15070  token: 0x60001A9
        public static System.Boolean TryParse(System.String input, System.DateTimeOffset& result) { }
        // RVA: 0x0AE150CC  token: 0x60001AA
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000040  // size: 0x10
    public static class EmailAddress
    {
        // Methods
        // RVA: 0x0AE15190  token: 0x60001AE
        public static System.Boolean TryParse(System.String input, System.String& result) { }

    }

    // TypeToken: 0x2000041  // size: 0x10
    public static class Host
    {
        // Methods
        // RVA: 0x0AE15AE4  token: 0x60001AF
        public static System.Boolean TryParse(System.String input, System.String& result) { }

    }

    // TypeToken: 0x2000042  // size: 0x10
    public static class Int
    {
        // Methods
        // RVA: 0x0AE1B680  token: 0x60001B0
        public static System.Boolean TryParse(System.String input, System.Int32& result) { }

    }

    // TypeToken: 0x2000043  // size: 0x10
    public static class Long
    {
        // Methods
        // RVA: 0x0AE1C368  token: 0x60001B1
        public static System.Boolean TryParse(System.String input, System.Int64& result) { }

    }

    // TypeToken: 0x2000045  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly System.Net.Http.Headers.Parser.MD5.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x0AE27A18  token: 0x60001B4
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60001B5
        public System.Void .ctor() { }
        // RVA: 0x0AE277B8  token: 0x60001B6
        private System.String <.cctor>b__2_0(System.Object l) { }

    }

    // TypeToken: 0x2000044  // size: 0x10
    public static class MD5
    {
        // Fields
        public static readonly System.Func<System.Object,System.String> ToString;  // static @ 0x0

        // Methods
        // RVA: 0x0AE1C3C8  token: 0x60001B2
        public static System.Boolean TryParse(System.String input, System.Byte[]& result) { }
        // RVA: 0x0AE1C4AC  token: 0x60001B3
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000046  // size: 0x10
    public static class TimeSpanSeconds
    {
        // Methods
        // RVA: 0x0AE22FB8  token: 0x60001B7
        public static System.Boolean TryParse(System.String input, System.TimeSpan& result) { }

    }

    // TypeToken: 0x2000047  // size: 0x10
    public static class Uri
    {
        // Methods
        // RVA: 0x0AE27BA8  token: 0x60001B8
        public static System.Boolean TryParse(System.String input, System.Uri& result) { }

    }

    // TypeToken: 0x2000054  // size: 0x8F
    public sealed struct __StaticArrayInitTypeSize=127
    {
    }

    // TypeToken: 0x2000053  // size: 0x10
    public sealed class <PrivateImplementationDetails>
    {
        // Fields
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=127 1D59178A3E2B293760F6FE72820F96FEC4071964A5B9E4BB13F7EA51510A4729;  // static @ 0x0

        // Methods
        // RVA: 0x0687A850  token: 0x600022F
        private static System.UInt32 ComputeStringHash(System.String s) { }

    }

namespace System.Net.Http
{

    // TypeToken: 0x2000002  // size: 0x20
    public class HttpClientHandler : System.Net.Http.HttpMessageHandler
    {
        // Fields
        private readonly System.Net.Http.IMonoHttpClientHandler _delegatingHandler;  // 0x10
        private System.Net.Http.ClientCertificateOption _clientCertificateOptions;  // 0x18

        // Properties
        System.Net.Http.ClientCertificateOption ClientCertificateOptions { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x0AE15E9C */ }
        System.Security.Cryptography.X509Certificates.X509CertificateCollection ClientCertificates { get; /* RVA: 0x0AE15D6C */ }

        // Methods
        // RVA: 0x0AE15B80  token: 0x6000001
        private static System.Net.Http.IMonoHttpClientHandler CreateDefaultHandler() { }
        // RVA: 0x0AE15D48  token: 0x6000002
        public System.Void .ctor() { }
        // RVA: 0x0AE15D20  token: 0x6000003
        private System.Void .ctor(System.Net.Http.IMonoHttpClientHandler handler) { }
        // RVA: 0x0AE15BD0  token: 0x6000004
        protected virtual System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x0AE15CB0  token: 0x6000005
        private System.Void ThrowForModifiedManagedSslOptionsIfStarted() { }
        // RVA: 0x0AE15C70  token: 0x6000009
        private System.Void SetWebRequestTimeout(System.TimeSpan timeout) { }
        // RVA: 0x0AE15C28  token: 0x600000A
        protected internal virtual System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> SendAsync(System.Net.Http.HttpRequestMessage request, System.Threading.CancellationToken cancellationToken) { }
        // RVA: 0x0AE15D04  token: 0x600000B
        private System.Security.Cryptography.X509Certificates.X509Certificate <set_ClientCertificateOptions>b__23_0(System.Object sender, System.String targetHost, System.Security.Cryptography.X509Certificates.X509CertificateCollection localCertificates, System.Security.Cryptography.X509Certificates.X509Certificate remoteCertificate, System.String[] acceptableIssuers) { }

    }

    // TypeToken: 0x2000004
    public interface IMonoHttpClientHandler : System.IDisposable
    {
        // Properties
        System.Net.Security.SslClientAuthenticationOptions SslOptions { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000011
        public virtual System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> SendAsync(System.Net.Http.HttpRequestMessage request, System.Threading.CancellationToken cancellationToken) { }
        // RVA: -1  // abstract  token: 0x6000012
        public virtual System.Void SetWebRequestTimeout(System.TimeSpan timeout) { }

    }

    // TypeToken: 0x2000005  // size: 0xB8
    public class MonoWebRequestHandler : System.Net.Http.IMonoHttpClientHandler, System.IDisposable
    {
        // Fields
        private static System.Int64 groupCounter;  // static @ 0x0
        private System.Boolean allowAutoRedirect;  // 0x10
        private System.Net.DecompressionMethods automaticDecompression;  // 0x14
        private System.Net.CookieContainer cookieContainer;  // 0x18
        private System.Net.ICredentials credentials;  // 0x20
        private System.Int32 maxAutomaticRedirections;  // 0x28
        private System.Int64 maxRequestContentBufferSize;  // 0x30
        private System.Boolean preAuthenticate;  // 0x38
        private System.Net.IWebProxy proxy;  // 0x40
        private System.Boolean useCookies;  // 0x48
        private System.Boolean useProxy;  // 0x49
        private System.Net.Security.SslClientAuthenticationOptions sslOptions;  // 0x50
        private System.Boolean allowPipelining;  // 0x58
        private System.Net.Cache.RequestCachePolicy cachePolicy;  // 0x60
        private System.Net.Security.AuthenticationLevel authenticationLevel;  // 0x68
        private System.TimeSpan continueTimeout;  // 0x70
        private System.Security.Principal.TokenImpersonationLevel impersonationLevel;  // 0x78
        private System.Int32 maxResponseHeadersLength;  // 0x7c
        private System.Int32 readWriteTimeout;  // 0x80
        private System.Net.Security.RemoteCertificateValidationCallback serverCertificateValidationCallback;  // 0x88
        private System.Boolean unsafeAuthenticatedConnectionSharing;  // 0x90
        private System.Boolean sentRequest;  // 0x91
        private System.String connectionGroupName;  // 0x98
        private System.Nullable<System.TimeSpan> timeout;  // 0xa0
        private System.Boolean disposed;  // 0xb0

        // Properties
        System.Net.CookieContainer CookieContainer { get; /* RVA: 0x0AE1E260 */ }
        System.Int64 MaxRequestContentBufferSize { get; /* RVA: 0x011F36E0 */ }
        System.Net.Security.SslClientAuthenticationOptions SslOptions { get; /* RVA: 0x0AE1E2D4 */ set; /* RVA: 0x0AE1E338 */ }

        // Methods
        // RVA: 0x0AE1E10C  token: 0x6000013
        public System.Void .ctor() { }
        // RVA: 0x0AE1DD3C  token: 0x6000014
        private System.Void EnsureModifiability() { }
        // RVA: 0x0AE1DD30  token: 0x6000019
        public virtual System.Void Dispose() { }
        // RVA: 0x0AE1DCC0  token: 0x600001A
        protected virtual System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x0AE1DDA8  token: 0x600001B
        private System.Boolean GetConnectionKeepAlive(System.Net.Http.Headers.HttpRequestHeaders headers) { }
        // RVA: 0x0AE1D4A0  token: 0x600001C
        private virtual System.Net.HttpWebRequest CreateWebRequest(System.Net.Http.HttpRequestMessage request) { }
        // RVA: 0x0AE1D304  token: 0x600001D
        private System.Net.Http.HttpResponseMessage CreateResponseMessage(System.Net.HttpWebResponse wr, System.Net.Http.HttpRequestMessage requestMessage, System.Threading.CancellationToken cancellationToken) { }
        // RVA: 0x0AE1DEC8  token: 0x600001E
        private static System.Boolean MethodHasBody(System.Net.Http.HttpMethod method) { }
        // RVA: 0x0AE1DFA8  token: 0x600001F
        public virtual System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> SendAsync(System.Net.Http.HttpRequestMessage request, System.Threading.CancellationToken cancellationToken) { }
        // RVA: 0x0AE1E0B4  token: 0x6000020
        private virtual System.Void System.Net.Http.IMonoHttpClientHandler.SetWebRequestTimeout(System.TimeSpan timeout) { }

    }

    // TypeToken: 0x2000008  // size: 0x10
    public static class PlatformHelper
    {
        // Methods
        // RVA: 0x0AE203B8  token: 0x6000028
        private static System.Boolean IsContentHeader(System.String name) { }
        // RVA: 0x0AE2035C  token: 0x6000029
        private static System.String GetSingleHeaderString(System.String name, System.Collections.Generic.IEnumerable<System.String> values) { }
        // RVA: 0x0AE202EC  token: 0x600002A
        private static System.Net.Http.StreamContent CreateStreamContent(System.IO.Stream stream, System.Threading.CancellationToken cancellationToken) { }

    }

    // TypeToken: 0x2000009  // size: 0x38
    public class ByteArrayContent : System.Net.Http.HttpContent
    {
        // Fields
        private readonly System.Byte[] content;  // 0x28
        private readonly System.Int32 offset;  // 0x30
        private readonly System.Int32 count;  // 0x34

        // Methods
        // RVA: 0x0AE11F7C  token: 0x600002B
        public System.Void .ctor(System.Byte[] content) { }
        // RVA: 0x0AE11F48  token: 0x600002C
        protected virtual System.Threading.Tasks.Task SerializeToStreamAsync(System.IO.Stream stream, System.Net.TransportContext context) { }
        // RVA: 0x04DAA2D0  token: 0x600002D
        protected internal virtual System.Boolean TryComputeLength(System.Int64& length) { }

    }

    // TypeToken: 0x200000A  // size: 0x14
    public sealed struct ClientCertificateOption
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Net.Http.ClientCertificateOption Manual;  // const
        public static System.Net.Http.ClientCertificateOption Automatic;  // const

    }

    // TypeToken: 0x200000B  // size: 0x50
    public class HttpClient : System.Net.Http.HttpMessageInvoker
    {
        // Fields
        private static readonly System.TimeSpan TimeoutDefault;  // static @ 0x0
        private System.Uri base_address;  // 0x20
        private System.Threading.CancellationTokenSource cts;  // 0x28
        private System.Boolean disposed;  // 0x30
        private System.Net.Http.Headers.HttpRequestHeaders headers;  // 0x38
        private System.Int64 buffer_size;  // 0x40
        private System.TimeSpan timeout;  // 0x48

        // Properties
        System.Net.Http.Headers.HttpRequestHeaders DefaultRequestHeaders { get; /* RVA: 0x0AE16834 */ }
        System.Int64 MaxResponseContentBufferSize { get; /* RVA: 0x04D85A60 */ }

        // Methods
        // RVA: 0x0AE167CC  token: 0x600002E
        public System.Void .ctor() { }
        // RVA: 0x0AE16708  token: 0x600002F
        public System.Void .ctor(System.Net.Http.HttpMessageHandler handler, System.Boolean disposeHandler) { }
        // RVA: 0x0AE16084  token: 0x6000032
        protected virtual System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x0AE160E4  token: 0x6000033
        public System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> PostAsync(System.String requestUri, System.Net.Http.HttpContent content) { }
        // RVA: 0x0AE162EC  token: 0x6000034
        public System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> SendAsync(System.Net.Http.HttpRequestMessage request) { }
        // RVA: 0x0AE16680  token: 0x6000035
        public virtual System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> SendAsync(System.Net.Http.HttpRequestMessage request, System.Threading.CancellationToken cancellationToken) { }
        // RVA: 0x0AE16348  token: 0x6000036
        public System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> SendAsync(System.Net.Http.HttpRequestMessage request, System.Net.Http.HttpCompletionOption completionOption, System.Threading.CancellationToken cancellationToken) { }
        // RVA: 0x0AE161D4  token: 0x6000037
        private System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> SendAsyncWorker(System.Net.Http.HttpRequestMessage request, System.Net.Http.HttpCompletionOption completionOption, System.Threading.CancellationToken cancellationToken) { }
        // RVA: 0x0AE166A8  token: 0x6000038
        private static System.Void .cctor() { }
        // RVA: 0x0AE166A0  token: 0x6000039
        private System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> <>n__0(System.Net.Http.HttpRequestMessage request, System.Threading.CancellationToken cancellationToken) { }

    }

    // TypeToken: 0x200000D  // size: 0x14
    public sealed struct HttpCompletionOption
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Net.Http.HttpCompletionOption ResponseContentRead;  // const
        public static System.Net.Http.HttpCompletionOption ResponseHeadersRead;  // const

    }

    // TypeToken: 0x200000E  // size: 0x28
    public abstract class HttpContent : System.IDisposable
    {
        // Fields
        private System.Net.Http.HttpContent.FixedMemoryStream buffer;  // 0x10
        private System.Boolean disposed;  // 0x18
        private System.Net.Http.Headers.HttpContentHeaders headers;  // 0x20

        // Properties
        System.Net.Http.Headers.HttpContentHeaders Headers { get; /* RVA: 0x0AE17060 */ }
        System.Nullable<System.Int64> LoadedBufferLength { get; /* RVA: 0x0AE170BC */ }

        // Methods
        // RVA: 0x0AE16BE4  token: 0x600003E
        public System.Threading.Tasks.Task CopyToAsync(System.IO.Stream stream) { }
        // RVA: 0x0AE16BF0  token: 0x600003F
        public System.Threading.Tasks.Task CopyToAsync(System.IO.Stream stream, System.Net.TransportContext context) { }
        // RVA: 0x0AE16C80  token: 0x6000040
        private static System.Net.Http.HttpContent.FixedMemoryStream CreateFixedMemoryStream(System.Int64 maxBufferSize) { }
        // RVA: 0x0AE16D08  token: 0x6000041
        public virtual System.Void Dispose() { }
        // RVA: 0x0AE16CE0  token: 0x6000042
        protected virtual System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x0AE16F10  token: 0x6000043
        public System.Threading.Tasks.Task LoadIntoBufferAsync() { }
        // RVA: 0x0AE16E48  token: 0x6000044
        public System.Threading.Tasks.Task LoadIntoBufferAsync(System.Int64 maxBufferSize) { }
        // RVA: 0x0AE16F20  token: 0x6000045
        public System.Threading.Tasks.Task<System.String> ReadAsStringAsync() { }
        // RVA: 0x0AE16D18  token: 0x6000046
        private static System.Text.Encoding GetEncodingFromBuffer(System.Byte[] buffer, System.Int32 length, System.Int32& preambleLength) { }
        // RVA: 0x0AE17004  token: 0x6000047
        private static System.Int32 StartsWith(System.Byte[] array, System.Int32 length, System.Byte[] value) { }
        // RVA: 0x0AE16FF4  token: 0x6000048
        private System.Threading.Tasks.Task SerializeToStreamAsync_internal(System.IO.Stream stream, System.Net.TransportContext context) { }
        // RVA: -1  // abstract  token: 0x6000049
        protected virtual System.Threading.Tasks.Task SerializeToStreamAsync(System.IO.Stream stream, System.Net.TransportContext context) { }
        // RVA: -1  // abstract  token: 0x600004A
        protected internal virtual System.Boolean TryComputeLength(System.Int64& length) { }
        // RVA: 0x041E1670  token: 0x600004B
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000012  // size: 0x10
    public abstract class HttpMessageHandler : System.IDisposable
    {
        // Methods
        // RVA: 0x0AE16D08  token: 0x6000054
        public virtual System.Void Dispose() { }
        // RVA: 0x041E1670  token: 0x6000055
        protected virtual System.Void Dispose(System.Boolean disposing) { }
        // RVA: -1  // abstract  token: 0x6000056
        protected internal virtual System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> SendAsync(System.Net.Http.HttpRequestMessage request, System.Threading.CancellationToken cancellationToken) { }
        // RVA: 0x041E1670  token: 0x6000057
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000013  // size: 0x20
    public class HttpMessageInvoker : System.IDisposable
    {
        // Fields
        private System.Net.Http.HttpMessageHandler handler;  // 0x10
        private readonly System.Boolean disposeHandler;  // 0x18

        // Methods
        // RVA: 0x0AE19DE4  token: 0x6000058
        public System.Void .ctor(System.Net.Http.HttpMessageHandler handler, System.Boolean disposeHandler) { }
        // RVA: 0x0AE16D08  token: 0x6000059
        public virtual System.Void Dispose() { }
        // RVA: 0x0AE19D54  token: 0x600005A
        protected virtual System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x0AE19D8C  token: 0x600005B
        public virtual System.Threading.Tasks.Task<System.Net.Http.HttpResponseMessage> SendAsync(System.Net.Http.HttpRequestMessage request, System.Threading.CancellationToken cancellationToken) { }

    }

    // TypeToken: 0x2000014  // size: 0x18
    public class HttpMethod : System.IEquatable`1
    {
        // Fields
        private static readonly System.Net.Http.HttpMethod delete_method;  // static @ 0x0
        private static readonly System.Net.Http.HttpMethod get_method;  // static @ 0x8
        private static readonly System.Net.Http.HttpMethod head_method;  // static @ 0x10
        private static readonly System.Net.Http.HttpMethod options_method;  // static @ 0x18
        private static readonly System.Net.Http.HttpMethod post_method;  // static @ 0x20
        private static readonly System.Net.Http.HttpMethod put_method;  // static @ 0x28
        private static readonly System.Net.Http.HttpMethod trace_method;  // static @ 0x30
        private readonly System.String method;  // 0x10

        // Properties
        System.String Method { get; /* RVA: 0x02B2ECC0 */ }
        System.Net.Http.HttpMethod Post { get; /* RVA: 0x0AE1A238 */ }

        // Methods
        // RVA: 0x0AE1A1A8  token: 0x600005C
        public System.Void .ctor(System.String method) { }
        // RVA: 0x0AE1A288  token: 0x600005F
        public static System.Boolean op_Equality(System.Net.Http.HttpMethod left, System.Net.Http.HttpMethod right) { }
        // RVA: 0x0AE19E60  token: 0x6000060
        public virtual System.Boolean Equals(System.Net.Http.HttpMethod other) { }
        // RVA: 0x0AE19E8C  token: 0x6000061
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x08479504  token: 0x6000062
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x02B2ECC0  token: 0x6000063
        public virtual System.String ToString() { }
        // RVA: 0x0AE19EE8  token: 0x6000064
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000015  // size: 0x90
    public class HttpRequestException : System.Exception
    {
        // Methods
        // RVA: 0x0AE1A2A4  token: 0x6000065
        public System.Void .ctor() { }
        // RVA: 0x0AE1A2F0  token: 0x6000066
        public System.Void .ctor(System.String message) { }
        // RVA: 0x0AE1A34C  token: 0x6000067
        public System.Void .ctor(System.String message, System.Exception inner) { }

    }

    // TypeToken: 0x2000016  // size: 0x40
    public class HttpRequestMessage : System.IDisposable
    {
        // Fields
        private System.Net.Http.Headers.HttpRequestHeaders headers;  // 0x10
        private System.Net.Http.HttpMethod method;  // 0x18
        private System.Version version;  // 0x20
        private System.Uri uri;  // 0x28
        private System.Boolean is_used;  // 0x30
        private System.Boolean disposed;  // 0x31
        private System.Net.Http.HttpContent <Content>k__BackingField;  // 0x38

        // Properties
        System.Net.Http.HttpContent Content { get; /* RVA: 0x04D85A50 */ set; /* RVA: 0x046A91B0 */ }
        System.Net.Http.Headers.HttpRequestHeaders Headers { get; /* RVA: 0x0AE1B0AC */ }
        System.Net.Http.HttpMethod Method { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x0AE1B158 */ }
        System.Uri RequestUri { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x0AE1B200 */ }
        System.Version Version { get; /* RVA: 0x0AE1B104 */ }

        // Methods
        // RVA: 0x0AE1AFE4  token: 0x6000068
        public System.Void .ctor(System.Net.Http.HttpMethod method, System.String requestUri) { }
        // RVA: 0x0AE1B07C  token: 0x6000069
        public System.Void .ctor(System.Net.Http.HttpMethod method, System.Uri requestUri) { }
        // RVA: 0x0AE1AC58  token: 0x6000071
        private static System.Boolean IsAllowedAbsoluteUri(System.Uri uri) { }
        // RVA: 0x0AE1AC20  token: 0x6000073
        public virtual System.Void Dispose() { }
        // RVA: 0x0AE1AC2C  token: 0x6000074
        protected virtual System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x0AE1AD88  token: 0x6000075
        private System.Boolean SetIsUsed() { }
        // RVA: 0x0AE1AD98  token: 0x6000076
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000017  // size: 0x48
    public class HttpResponseMessage : System.IDisposable
    {
        // Fields
        private System.Net.Http.Headers.HttpResponseHeaders headers;  // 0x10
        private System.String reasonPhrase;  // 0x18
        private System.Net.HttpStatusCode statusCode;  // 0x20
        private System.Version version;  // 0x28
        private System.Boolean disposed;  // 0x30
        private System.Net.Http.HttpContent <Content>k__BackingField;  // 0x38
        private System.Net.Http.HttpRequestMessage <RequestMessage>k__BackingField;  // 0x40

        // Properties
        System.Net.Http.HttpContent Content { get; /* RVA: 0x04D85A50 */ set; /* RVA: 0x046A91B0 */ }
        System.Net.Http.Headers.HttpResponseHeaders Headers { get; /* RVA: 0x0AE1B568 */ }
        System.String ReasonPhrase { get; /* RVA: 0x0AE1B5C0 */ set; /* RVA: 0x05392C40 */ }
        System.Net.Http.HttpRequestMessage RequestMessage { set; /* RVA: 0x042B4AE0 */ }
        System.Net.HttpStatusCode StatusCode { get; /* RVA: 0x02B70F90 */ set; /* RVA: 0x0AE1B628 */ }
        System.Version Version { get; /* RVA: 0x0AE1B5D4 */ }

        // Methods
        // RVA: 0x0AE1B560  token: 0x6000077
        public System.Void .ctor(System.Net.HttpStatusCode statusCode) { }
        // RVA: 0x0AE1B314  token: 0x6000081
        public virtual System.Void Dispose() { }
        // RVA: 0x0AE1B320  token: 0x6000082
        protected virtual System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x0AE1B34C  token: 0x6000083
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000018  // size: 0x38
    public class MultipartContent : System.Net.Http.HttpContent, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private System.Collections.Generic.List<System.Net.Http.HttpContent> nested_content;  // 0x28
        private readonly System.String boundary;  // 0x30

        // Methods
        // RVA: 0x0AE1EC54  token: 0x6000084
        public System.Void .ctor(System.String subtype) { }
        // RVA: 0x0AE1ECF0  token: 0x6000085
        public System.Void .ctor(System.String subtype, System.String boundary) { }
        // RVA: 0x0AE1E690  token: 0x6000086
        private static System.Boolean IsValidRFC2049(System.String s) { }
        // RVA: 0x0AE1E368  token: 0x6000087
        public virtual System.Void Add(System.Net.Http.HttpContent content) { }
        // RVA: 0x0AE1E468  token: 0x6000088
        protected virtual System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x0AE1E748  token: 0x6000089
        protected virtual System.Threading.Tasks.Task SerializeToStreamAsync(System.IO.Stream stream, System.Net.TransportContext context) { }
        // RVA: 0x0AE1E8B4  token: 0x600008A
        protected internal virtual System.Boolean TryComputeLength(System.Int64& length) { }
        // RVA: 0x0AE1E610  token: 0x600008B
        public virtual System.Collections.Generic.IEnumerator<System.Net.Http.HttpContent> GetEnumerator() { }
        // RVA: 0x0AE1E834  token: 0x600008C
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200001A  // size: 0x38
    public class MultipartFormDataContent : System.Net.Http.MultipartContent
    {
        // Methods
        // RVA: 0x0AE1F340  token: 0x600008F
        public System.Void .ctor() { }
        // RVA: 0x0AE1F300  token: 0x6000090
        public virtual System.Void Add(System.Net.Http.HttpContent content) { }
        // RVA: 0x0AE1F13C  token: 0x6000091
        public System.Void Add(System.Net.Http.HttpContent content, System.String name) { }
        // RVA: 0x0AE1F1E8  token: 0x6000092
        public System.Void Add(System.Net.Http.HttpContent content, System.String name, System.String fileName) { }
        // RVA: 0x0AE1F050  token: 0x6000093
        private System.Void AddContentDisposition(System.Net.Http.HttpContent content, System.String name, System.String fileName) { }

    }

    // TypeToken: 0x200001B  // size: 0x50
    public class StreamContent : System.Net.Http.HttpContent
    {
        // Fields
        private readonly System.IO.Stream content;  // 0x28
        private readonly System.Int32 bufferSize;  // 0x30
        private readonly System.Threading.CancellationToken cancellationToken;  // 0x38
        private readonly System.Int64 startPosition;  // 0x40
        private System.Boolean contentCopied;  // 0x48

        // Methods
        // RVA: 0x0AE2287C  token: 0x6000094
        public System.Void .ctor(System.IO.Stream content) { }
        // RVA: 0x0AE22778  token: 0x6000095
        public System.Void .ctor(System.IO.Stream content, System.Int32 bufferSize) { }
        // RVA: 0x0AE2288C  token: 0x6000096
        private System.Void .ctor(System.IO.Stream content, System.Threading.CancellationToken cancellationToken) { }
        // RVA: 0x0AE22600  token: 0x6000097
        protected virtual System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x0AE22648  token: 0x6000098
        protected virtual System.Threading.Tasks.Task SerializeToStreamAsync(System.IO.Stream stream, System.Net.TransportContext context) { }
        // RVA: 0x0AE2271C  token: 0x6000099
        protected internal virtual System.Boolean TryComputeLength(System.Int64& length) { }

    }

    // TypeToken: 0x200001C  // size: 0x38
    public class StringContent : System.Net.Http.ByteArrayContent
    {
        // Methods
        // RVA: 0x0AE228FC  token: 0x600009A
        public System.Void .ctor(System.String content) { }
        // RVA: 0x0AE2291C  token: 0x600009B
        public System.Void .ctor(System.String content, System.Text.Encoding encoding, System.String mediaType) { }
        // RVA: 0x0AE228C4  token: 0x600009C
        private static System.Byte[] GetByteArray(System.String content, System.Text.Encoding encoding) { }

    }

}

namespace System.Net.Http.Headers
{

    // TypeToken: 0x200001D  // size: 0x20
    public class AuthenticationHeaderValue : System.ICloneable
    {
        // Fields
        private System.String <Parameter>k__BackingField;  // 0x10
        private System.String <Scheme>k__BackingField;  // 0x18

        // Properties
        System.String Parameter { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        System.String Scheme { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }

        // Methods
        // RVA: 0x0AE11EFC  token: 0x600009D
        public System.Void .ctor(System.String scheme, System.String parameter) { }
        // RVA: 0x041E1670  token: 0x600009E
        private System.Void .ctor() { }
        // RVA: 0x03840500  token: 0x60000A3
        private virtual System.Object System.ICloneable.Clone() { }
        // RVA: 0x0AE11AA4  token: 0x60000A4
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AE11B20  token: 0x60000A5
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AE11D90  token: 0x60000A6
        public static System.Boolean TryParse(System.String input, System.Net.Http.Headers.AuthenticationHeaderValue& parsedValue) { }
        // RVA: 0x0AE11E4C  token: 0x60000A7
        private static System.Boolean TryParse(System.String input, System.Int32 minimalCount, System.Collections.Generic.List<System.Net.Http.Headers.AuthenticationHeaderValue>& result) { }
        // RVA: 0x0AE11BE0  token: 0x60000A8
        private static System.Boolean TryParseElement(System.Net.Http.Headers.Lexer lexer, System.Net.Http.Headers.AuthenticationHeaderValue& parsedValue, System.Net.Http.Headers.Token& t) { }
        // RVA: 0x0AE11B94  token: 0x60000A9
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200001E  // size: 0x78
    public class CacheControlHeaderValue : System.ICloneable
    {
        // Fields
        private System.Collections.Generic.List<System.Net.Http.Headers.NameValueHeaderValue> extensions;  // 0x10
        private System.Collections.Generic.List<System.String> no_cache_headers;  // 0x18
        private System.Collections.Generic.List<System.String> private_headers;  // 0x20
        private System.Nullable<System.TimeSpan> <MaxAge>k__BackingField;  // 0x28
        private System.Boolean <MaxStale>k__BackingField;  // 0x38
        private System.Nullable<System.TimeSpan> <MaxStaleLimit>k__BackingField;  // 0x40
        private System.Nullable<System.TimeSpan> <MinFresh>k__BackingField;  // 0x50
        private System.Boolean <MustRevalidate>k__BackingField;  // 0x60
        private System.Boolean <NoCache>k__BackingField;  // 0x61
        private System.Boolean <NoStore>k__BackingField;  // 0x62
        private System.Boolean <NoTransform>k__BackingField;  // 0x63
        private System.Boolean <OnlyIfCached>k__BackingField;  // 0x64
        private System.Boolean <Private>k__BackingField;  // 0x65
        private System.Boolean <ProxyRevalidate>k__BackingField;  // 0x66
        private System.Boolean <Public>k__BackingField;  // 0x67
        private System.Nullable<System.TimeSpan> <SharedMaxAge>k__BackingField;  // 0x68

        // Properties
        System.Collections.Generic.ICollection<System.Net.Http.Headers.NameValueHeaderValue> Extensions { get; /* RVA: 0x0AE137D4 */ }
        System.Nullable<System.TimeSpan> MaxAge { get; /* RVA: 0x04D8FAD0 */ set; /* RVA: 0x04D8FAE0 */ }
        System.Boolean MaxStale { get; /* RVA: 0x011F2A70 */ set; /* RVA: 0x011F2A80 */ }
        System.Nullable<System.TimeSpan> MaxStaleLimit { get; /* RVA: 0x04D905C0 */ set; /* RVA: 0x04D926F0 */ }
        System.Nullable<System.TimeSpan> MinFresh { get; /* RVA: 0x04D8CE10 */ set; /* RVA: 0x04D906B0 */ }
        System.Boolean MustRevalidate { get; /* RVA: 0x04D88340 */ set; /* RVA: 0x04D88350 */ }
        System.Boolean NoCache { get; /* RVA: 0x04D892D0 */ set; /* RVA: 0x04D8FF40 */ }
        System.Collections.Generic.ICollection<System.String> NoCacheHeaders { get; /* RVA: 0x0AE13858 */ }
        System.Boolean NoStore { get; /* RVA: 0x04D8FF20 */ set; /* RVA: 0x04D8FF30 */ }
        System.Boolean NoTransform { get; /* RVA: 0x04DAA2E0 */ set; /* RVA: 0x04DAA300 */ }
        System.Boolean OnlyIfCached { get; /* RVA: 0x04D8F050 */ set; /* RVA: 0x04D8F080 */ }
        System.Boolean Private { get; /* RVA: 0x04DAA1E0 */ set; /* RVA: 0x04DAA310 */ }
        System.Collections.Generic.ICollection<System.String> PrivateHeaders { get; /* RVA: 0x0AE138DC */ }
        System.Boolean ProxyRevalidate { get; /* RVA: 0x04DAA1D0 */ set; /* RVA: 0x04DAA320 */ }
        System.Boolean Public { get; /* RVA: 0x04DAA2F0 */ set; /* RVA: 0x04DAA330 */ }
        System.Nullable<System.TimeSpan> SharedMaxAge { get; /* RVA: 0x04DA63C0 */ set; /* RVA: 0x04DA63D0 */ }

        // Methods
        // RVA: 0x0AE12464  token: 0x60000C7
        private virtual System.Object System.ICloneable.Clone() { }
        // RVA: 0x0AE11FFC  token: 0x60000C8
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AE12240  token: 0x60000C9
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AE12FDC  token: 0x60000CA
        public static System.Boolean TryParse(System.String input, System.Net.Http.Headers.CacheControlHeaderValue& parsedValue) { }
        // RVA: 0x0AE12998  token: 0x60000CB
        public virtual System.String ToString() { }
        // RVA: 0x041E1670  token: 0x60000CC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001F  // size: 0x10
    public static class CollectionExtensions
    {
        // Methods
        // RVA: -1  // generic def  token: 0x60000CD
        public static System.Boolean SequenceEqual(System.Collections.Generic.List<TSource> first, System.Collections.Generic.List<TSource> second) { }
        // RVA: 0x0AE13960  token: 0x60000CE
        public static System.Void SetValue(System.Collections.Generic.List<System.Net.Http.Headers.NameValueHeaderValue> parameters, System.String key, System.String value) { }
        // RVA: -1  // generic def  token: 0x60000CF
        public static System.String ToString(System.Collections.Generic.List<T> list) { }
        // RVA: -1  // generic def  token: 0x60000D0
        public static System.Void ToStringBuilder(System.Collections.Generic.List<T> list, System.Text.StringBuilder sb) { }

    }

    // TypeToken: 0x2000020
    public sealed class ElementTryParser`1 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x60000D1
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x60000D2
        public virtual System.Boolean Invoke(System.Net.Http.Headers.Lexer lexer, T& parsedValue, System.Net.Http.Headers.Token& token) { }

    }

    // TypeToken: 0x2000021  // size: 0x10
    public static class CollectionParser
    {
        // Methods
        // RVA: -1  // generic def  token: 0x60000D3
        public static System.Boolean TryParse(System.String input, System.Int32 minimalCount, System.Net.Http.Headers.ElementTryParser<T> parser, System.Collections.Generic.List<T>& result) { }
        // RVA: 0x0AE13B64  token: 0x60000D4
        public static System.Boolean TryParse(System.String input, System.Int32 minimalCount, System.Collections.Generic.List<System.String>& result) { }
        // RVA: 0x0AE13A5C  token: 0x60000D5
        private static System.Boolean TryParseStringElement(System.Net.Http.Headers.Lexer lexer, System.String& parsedValue, System.Net.Http.Headers.Token& t) { }

    }

    // TypeToken: 0x2000022  // size: 0x20
    public class ContentDispositionHeaderValue : System.ICloneable
    {
        // Fields
        private System.String dispositionType;  // 0x10
        private System.Collections.Generic.List<System.Net.Http.Headers.NameValueHeaderValue> parameters;  // 0x18

        // Properties
        System.String DispositionType { set; /* RVA: 0x0AE146C8 */ }
        System.String FileName { set; /* RVA: 0x0AE1475C */ }
        System.String FileNameStar { set; /* RVA: 0x0AE146FC */ }
        System.String Name { set; /* RVA: 0x0AE147BC */ }
        System.Collections.Generic.ICollection<System.Net.Http.Headers.NameValueHeaderValue> Parameters { get; /* RVA: 0x0AE14644 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60000D6
        private System.Void .ctor() { }
        // RVA: 0x0AE1463C  token: 0x60000D7
        public System.Void .ctor(System.String dispositionType) { }
        // RVA: 0x0AE14360  token: 0x60000D8
        protected System.Void .ctor(System.Net.Http.Headers.ContentDispositionHeaderValue source) { }
        // RVA: 0x0AE14144  token: 0x60000DE
        private virtual System.Object System.ICloneable.Clone() { }
        // RVA: 0x0AE13FC8  token: 0x60000DF
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AE13C14  token: 0x60000E0
        private static System.String EncodeBase64Value(System.String value) { }
        // RVA: 0x0AE13DA0  token: 0x60000E1
        private static System.String EncodeRFC5987(System.String value) { }
        // RVA: 0x0AE14054  token: 0x60000E2
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AE140C4  token: 0x60000E3
        private System.Void SetValue(System.String key, System.String value) { }
        // RVA: 0x0AE141A4  token: 0x60000E4
        public virtual System.String ToString() { }
        // RVA: 0x0AE141D4  token: 0x60000E5
        public static System.Boolean TryParse(System.String input, System.Net.Http.Headers.ContentDispositionHeaderValue& parsedValue) { }

    }

    // TypeToken: 0x2000023  // size: 0x48
    public class ContentRangeHeaderValue : System.ICloneable
    {
        // Fields
        private System.String unit;  // 0x10
        private System.Nullable<System.Int64> <From>k__BackingField;  // 0x18
        private System.Nullable<System.Int64> <Length>k__BackingField;  // 0x28
        private System.Nullable<System.Int64> <To>k__BackingField;  // 0x38

        // Properties
        System.Nullable<System.Int64> From { get; /* RVA: 0x04D890C0 */ set; /* RVA: 0x04D890D0 */ }
        System.Nullable<System.Int64> Length { get; /* RVA: 0x04D8FAD0 */ set; /* RVA: 0x04D8FAE0 */ }
        System.Nullable<System.Int64> To { get; /* RVA: 0x04D8FD20 */ set; /* RVA: 0x04D91AB0 */ }
        System.String Unit { get; /* RVA: 0x02B2ECC0 */ }

        // Methods
        // RVA: 0x0AE15030  token: 0x60000E6
        private System.Void .ctor() { }
        // RVA: 0x03840500  token: 0x60000EE
        private virtual System.Object System.ICloneable.Clone() { }
        // RVA: 0x0AE1481C  token: 0x60000EF
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AE148F8  token: 0x60000F0
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AE14C00  token: 0x60000F1
        public static System.Boolean TryParse(System.String input, System.Net.Http.Headers.ContentRangeHeaderValue& parsedValue) { }
        // RVA: 0x0AE149F0  token: 0x60000F2
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000024  // size: 0x20
    public class EntityTagHeaderValue : System.ICloneable
    {
        // Fields
        private static readonly System.Net.Http.Headers.EntityTagHeaderValue any;  // static @ 0x0
        private System.Boolean <IsWeak>k__BackingField;  // 0x10
        private System.String <Tag>k__BackingField;  // 0x18

        // Properties
        System.Boolean IsWeak { get; /* RVA: 0x015EFCE0 */ set; /* RVA: 0x04D86130 */ }
        System.String Tag { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60000F3
        private System.Void .ctor() { }
        // RVA: 0x03840500  token: 0x60000F8
        private virtual System.Object System.ICloneable.Clone() { }
        // RVA: 0x0AE15280  token: 0x60000F9
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AE152FC  token: 0x60000FA
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AE155F0  token: 0x60000FB
        public static System.Boolean TryParse(System.String input, System.Net.Http.Headers.EntityTagHeaderValue& parsedValue) { }
        // RVA: 0x0AE153B4  token: 0x60000FC
        private static System.Boolean TryParseElement(System.Net.Http.Headers.Lexer lexer, System.Net.Http.Headers.EntityTagHeaderValue& parsedValue, System.Net.Http.Headers.Token& t) { }
        // RVA: 0x0AE156C4  token: 0x60000FD
        private static System.Boolean TryParse(System.String input, System.Int32 minimalCount, System.Collections.Generic.List<System.Net.Http.Headers.EntityTagHeaderValue>& result) { }
        // RVA: 0x0AE1536C  token: 0x60000FE
        public virtual System.String ToString() { }
        // RVA: 0x0AE15774  token: 0x60000FF
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000025  // size: 0x10
    public static class HashCodeCalculator
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000100
        public static System.Int32 Calculate(System.Collections.Generic.ICollection<T> list) { }

    }

    // TypeToken: 0x2000026
    public sealed class TryParseDelegate`1 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000101
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000102
        public virtual System.Boolean Invoke(System.String value, T& result) { }

    }

    // TypeToken: 0x2000027
    public sealed class TryParseListDelegate`1 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000103
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000104
        public virtual System.Boolean Invoke(System.String value, System.Int32 minimalCount, System.Collections.Generic.List<T>& result) { }

    }

    // TypeToken: 0x2000028  // size: 0x28
    public abstract class HeaderInfo
    {
        // Fields
        public System.Boolean AllowsMany;  // 0x10
        public readonly System.Net.Http.Headers.HttpHeaderKind HeaderKind;  // 0x14
        public readonly System.String Name;  // 0x18
        private System.Func<System.Object,System.String> <CustomToString>k__BackingField;  // 0x20

        // Properties
        System.Func<System.Object,System.String> CustomToString { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        System.String Separator { get; /* RVA: 0x0AE15A98 */ }

        // Methods
        // RVA: 0x0AE15A78  token: 0x6000105
        protected System.Void .ctor(System.String name, System.Net.Http.Headers.HttpHeaderKind headerKind) { }
        // RVA: -1  // generic def  token: 0x6000106
        public static System.Net.Http.Headers.HeaderInfo CreateSingle(System.String name, System.Net.Http.Headers.TryParseDelegate<T> parser, System.Net.Http.Headers.HttpHeaderKind headerKind, System.Func<System.Object,System.String> toString) { }
        // RVA: -1  // generic def  token: 0x6000107
        public static System.Net.Http.Headers.HeaderInfo CreateMulti(System.String name, System.Net.Http.Headers.TryParseListDelegate<T> elementParser, System.Net.Http.Headers.HttpHeaderKind headerKind, System.Int32 minimalCount, System.String separator) { }
        // RVA: 0x0AE15A38  token: 0x6000108
        public System.Object CreateCollection(System.Net.Http.Headers.HttpHeaders headers) { }
        // RVA: -1  // abstract  token: 0x600010C
        public virtual System.Void AddToCollection(System.Object collection, System.Object value) { }
        // RVA: -1  // abstract  token: 0x600010D
        protected virtual System.Object CreateCollection(System.Net.Http.Headers.HttpHeaders headers, System.Net.Http.Headers.HeaderInfo headerInfo) { }
        // RVA: -1  // abstract  token: 0x600010E
        public virtual System.Collections.Generic.List<System.String> ToStringCollection(System.Object collection) { }
        // RVA: -1  // abstract  token: 0x600010F
        public virtual System.Boolean TryParse(System.String value, System.Object& result) { }

    }

    // TypeToken: 0x200002B  // size: 0x28
    public sealed class HttpContentHeaders : System.Net.Http.Headers.HttpHeaders
    {
        // Fields
        private readonly System.Net.Http.HttpContent content;  // 0x20

        // Properties
        System.Net.Http.Headers.ContentDispositionHeaderValue ContentDisposition { get; /* RVA: 0x0AE168F8 */ set; /* RVA: 0x0AE16AB4 */ }
        System.Nullable<System.Int64> ContentLength { get; /* RVA: 0x0AE16944 */ set; /* RVA: 0x0AE16B18 */ }
        System.Net.Http.Headers.MediaTypeHeaderValue ContentType { get; /* RVA: 0x0AE16A68 */ set; /* RVA: 0x0AE16B80 */ }

        // Methods
        // RVA: 0x0AE168A8  token: 0x6000118
        private System.Void .ctor(System.Net.Http.HttpContent content) { }

    }

    // TypeToken: 0x200002C  // size: 0x14
    public sealed struct HttpHeaderKind
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Net.Http.Headers.HttpHeaderKind None;  // const
        public static System.Net.Http.Headers.HttpHeaderKind Request;  // const
        public static System.Net.Http.Headers.HttpHeaderKind Response;  // const
        public static System.Net.Http.Headers.HttpHeaderKind Content;  // const

    }

    // TypeToken: 0x200002D
    public sealed class HttpHeaderValueCollection`1 : System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly System.Collections.Generic.List<T> list;  // 0x0
        private readonly System.Net.Http.Headers.HttpHeaders headers;  // 0x0
        private readonly System.Net.Http.Headers.HeaderInfo headerInfo;  // 0x0
        private System.Collections.Generic.List<System.String> invalidValues;  // 0x0

        // Properties
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        System.Collections.Generic.List<System.String> InvalidValues { get; /* RVA: -1  // not resolved */ }
        System.Boolean IsReadOnly { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600011F
        private System.Void .ctor(System.Net.Http.Headers.HttpHeaders headers, System.Net.Http.Headers.HeaderInfo headerInfo) { }
        // RVA: -1  // not resolved  token: 0x6000123
        public virtual System.Void Add(T item) { }
        // RVA: -1  // not resolved  token: 0x6000124
        private System.Void AddRange(System.Collections.Generic.List<T> values) { }
        // RVA: -1  // not resolved  token: 0x6000125
        private System.Void AddInvalidValue(System.String invalidValue) { }
        // RVA: -1  // not resolved  token: 0x6000126
        public virtual System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x6000127
        public virtual System.Boolean Contains(T item) { }
        // RVA: -1  // not resolved  token: 0x6000128
        public virtual System.Void CopyTo(T[] array, System.Int32 arrayIndex) { }
        // RVA: -1  // not resolved  token: 0x6000129
        public virtual System.Boolean Remove(T item) { }
        // RVA: -1  // not resolved  token: 0x600012A
        public virtual System.String ToString() { }
        // RVA: -1  // not resolved  token: 0x600012B
        public virtual System.Collections.Generic.IEnumerator<T> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x600012C
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x600012D
        private T Find(System.Predicate<T> predicate) { }

    }

    // TypeToken: 0x200002E  // size: 0x20
    public abstract class HttpHeaders : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private static readonly System.Collections.Generic.Dictionary<System.String,System.Net.Http.Headers.HeaderInfo> known_headers;  // static @ 0x0
        private readonly System.Collections.Generic.Dictionary<System.String,System.Net.Http.Headers.HttpHeaders.HeaderBucket> headers;  // 0x10
        private readonly System.Net.Http.Headers.HttpHeaderKind HeaderKind;  // 0x18
        private System.Nullable<System.Boolean> connectionclose;  // 0x1c
        private System.Nullable<System.Boolean> transferEncodingChunked;  // 0x1e

        // Methods
        // RVA: 0x0AE180B4  token: 0x600012E
        private static System.Void .cctor() { }
        // RVA: 0x0AE19CE4  token: 0x600012F
        protected System.Void .ctor() { }
        // RVA: 0x0AE19CC0  token: 0x6000130
        private System.Void .ctor(System.Net.Http.Headers.HttpHeaderKind headerKind) { }
        // RVA: 0x0AE17138  token: 0x6000131
        private System.Boolean AddInternal(System.String name, System.Collections.Generic.IEnumerable<System.String> values, System.Net.Http.Headers.HeaderInfo headerInfo, System.Boolean ignoreInvalid) { }
        // RVA: 0x0AE17F48  token: 0x6000132
        public System.Boolean TryAddWithoutValidation(System.String name, System.Collections.Generic.IEnumerable<System.String> values) { }
        // RVA: 0x0AE175BC  token: 0x6000133
        private System.Net.Http.Headers.HeaderInfo CheckName(System.String name) { }
        // RVA: 0x0AE18004  token: 0x6000134
        private System.Boolean TryCheckName(System.String name, System.Net.Http.Headers.HeaderInfo& headerInfo) { }
        // RVA: 0x0AE17844  token: 0x6000135
        public virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,System.Collections.Generic.IEnumerable<System.String>>> GetEnumerator() { }
        // RVA: 0x0AE17D14  token: 0x6000136
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: 0x0AE17CB0  token: 0x6000137
        public System.Boolean Remove(System.String name) { }
        // RVA: 0x0AE17944  token: 0x6000138
        private static System.String GetSingleHeaderString(System.String key, System.Collections.Generic.IEnumerable<System.String> values) { }
        // RVA: 0x0AE17D1C  token: 0x6000139
        public virtual System.String ToString() { }
        // RVA: -1  // generic def  token: 0x600013A
        private System.Void AddOrRemove(System.String name, T value, System.Func<System.Object,System.String> converter) { }
        // RVA: -1  // generic def  token: 0x600013B
        private System.Void AddOrRemove(System.String name, System.Nullable<T> value) { }
        // RVA: -1  // generic def  token: 0x600013C
        private System.Void AddOrRemove(System.String name, System.Nullable<T> value, System.Func<System.Object,System.String> converter) { }
        // RVA: 0x0AE17724  token: 0x600013D
        private System.Collections.Generic.List<System.String> GetAllHeaderValues(System.Net.Http.Headers.HttpHeaders.HeaderBucket bucket, System.Net.Http.Headers.HeaderInfo headerInfo) { }
        // RVA: 0x0AE17884  token: 0x600013E
        private static System.Net.Http.Headers.HttpHeaderKind GetKnownHeaderKind(System.String name) { }
        // RVA: -1  // generic def  token: 0x600013F
        private T GetValue(System.String name) { }
        // RVA: -1  // generic def  token: 0x6000140
        private System.Net.Http.Headers.HttpHeaderValueCollection<T> GetValues(System.String name) { }
        // RVA: -1  // generic def  token: 0x6000141
        private System.Void SetValue(System.String name, T value, System.Func<System.Object,System.String> toStringConverter) { }

    }

    // TypeToken: 0x2000031  // size: 0x28
    public sealed class HttpRequestHeaders : System.Net.Http.Headers.HttpHeaders
    {
        // Fields
        private System.Nullable<System.Boolean> expectContinue;  // 0x20

        // Properties
        System.Net.Http.Headers.AuthenticationHeaderValue Authorization { set; /* RVA: 0x0AE1AAD8 */ }
        System.Net.Http.Headers.HttpHeaderValueCollection<System.String> Connection { get; /* RVA: 0x0AE1A72C */ }
        System.Nullable<System.Boolean> ConnectionClose { get; /* RVA: 0x0AE1A5B4 */ set; /* RVA: 0x0AE1AB3C */ }
        System.Nullable<System.Boolean> ExpectContinue { get; /* RVA: 0x0AE1A778 */ }
        System.String Host { get; /* RVA: 0x0AE1A8DC */ }
        System.Net.Http.Headers.HttpHeaderValueCollection<System.Net.Http.Headers.TransferCodingHeaderValue> TransferEncoding { get; /* RVA: 0x0AE1AA8C */ }
        System.Nullable<System.Boolean> TransferEncodingChunked { get; /* RVA: 0x0AE1A928 */ }

        // Methods
        // RVA: 0x0AE1A560  token: 0x600014E
        private System.Void .ctor() { }
        // RVA: 0x0AE1A3AC  token: 0x6000157
        private System.Void AddHeaders(System.Net.Http.Headers.HttpRequestHeaders headers) { }

    }

    // TypeToken: 0x2000033  // size: 0x20
    public sealed class HttpResponseHeaders : System.Net.Http.Headers.HttpHeaders
    {
        // Methods
        // RVA: 0x0AE1B2C0  token: 0x600015D
        private System.Void .ctor() { }

    }

    // TypeToken: 0x2000034  // size: 0x1C
    public sealed struct Token
    {
        // Fields
        public static readonly System.Net.Http.Headers.Token Empty;  // static @ 0x0
        private readonly System.Net.Http.Headers.Token.Type type;  // 0x10
        private System.Int32 <StartPosition>k__BackingField;  // 0x14
        private System.Int32 <EndPosition>k__BackingField;  // 0x18

        // Properties
        System.Int32 StartPosition { get; /* RVA: 0x04D88B20 */ set; /* RVA: 0x04D88B50 */ }
        System.Int32 EndPosition { get; /* RVA: 0x04D88B10 */ set; /* RVA: 0x04D88B40 */ }
        System.Net.Http.Headers.Token.Type Kind { get; /* RVA: 0x02B77620 */ }

        // Methods
        // RVA: 0x0AE2324C  token: 0x600015E
        public System.Void .ctor(System.Net.Http.Headers.Token.Type type, System.Int32 startPosition, System.Int32 endPosition) { }
        // RVA: 0x02B77620  token: 0x6000164
        public static System.Net.Http.Headers.Token.Type op_Implicit(System.Net.Http.Headers.Token token) { }
        // RVA: 0x0AE2315C  token: 0x6000165
        public virtual System.String ToString() { }
        // RVA: 0x0AE231E4  token: 0x6000166
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000036  // size: 0x20
    public class Lexer
    {
        // Fields
        private static readonly System.Boolean[] token_chars;  // static @ 0x0
        private static readonly System.Int32 last_token_char;  // static @ 0x8
        private static readonly System.String[] dt_formats;  // static @ 0x10
        private readonly System.String s;  // 0x10
        private System.Int32 pos;  // 0x18

        // Properties
        System.Int32 Position { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x011EF9B0 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x6000167
        public System.Void .ctor(System.String stream) { }
        // RVA: 0x0AE1B7F4  token: 0x600016A
        public System.String GetStringValue(System.Net.Http.Headers.Token token) { }
        // RVA: 0x0AE1B780  token: 0x600016B
        public System.String GetStringValue(System.Net.Http.Headers.Token start, System.Net.Http.Headers.Token end) { }
        // RVA: 0x0AE1B6E0  token: 0x600016C
        public System.String GetQuotedStringValue(System.Net.Http.Headers.Token start) { }
        // RVA: 0x0AE1B750  token: 0x600016D
        public System.String GetRemainingStringValue(System.Int32 position) { }
        // RVA: 0x0AE1B848  token: 0x600016E
        public System.Boolean IsStarStringValue(System.Net.Http.Headers.Token token) { }
        // RVA: 0x0AE1BFD4  token: 0x600016F
        public System.Boolean TryGetNumericValue(System.Net.Http.Headers.Token token, System.Int32& value) { }
        // RVA: 0x0AE1C068  token: 0x6000170
        public System.Boolean TryGetNumericValue(System.Net.Http.Headers.Token token, System.Int64& value) { }
        // RVA: 0x0AE1C0FC  token: 0x6000171
        public System.Nullable<System.TimeSpan> TryGetTimeSpanValue(System.Net.Http.Headers.Token token) { }
        // RVA: 0x0AE1BDA4  token: 0x6000172
        public System.Boolean TryGetDateValue(System.Net.Http.Headers.Token token, System.DateTimeOffset& value) { }
        // RVA: 0x0AE1BE7C  token: 0x6000173
        public static System.Boolean TryGetDateValue(System.String text, System.DateTimeOffset& value) { }
        // RVA: 0x0AE1BF40  token: 0x6000174
        public System.Boolean TryGetDoubleValue(System.Net.Http.Headers.Token token, System.Double& value) { }
        // RVA: 0x0AE1B934  token: 0x6000175
        public static System.Boolean IsValidToken(System.String input) { }
        // RVA: 0x0AE1B8CC  token: 0x6000176
        public static System.Boolean IsValidCharacter(System.Char input) { }
        // RVA: 0x04D9CE40  token: 0x6000177
        public System.Void EatChar() { }
        // RVA: 0x0AE1B9A0  token: 0x6000178
        public System.Int32 PeekChar() { }
        // RVA: 0x0AE1B9D4  token: 0x6000179
        public System.Boolean ScanCommentOptional(System.String& value, System.Net.Http.Headers.Token& readToken) { }
        // RVA: 0x0AE1BAFC  token: 0x600017A
        public System.Net.Http.Headers.Token Scan(System.Boolean recognizeDash) { }
        // RVA: 0x0AE1C1C0  token: 0x600017B
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000037  // size: 0x20
    public class MediaTypeHeaderValue : System.ICloneable
    {
        // Fields
        private System.Collections.Generic.List<System.Net.Http.Headers.NameValueHeaderValue> parameters;  // 0x10
        private System.String media_type;  // 0x18

        // Properties
        System.String CharSet { get; /* RVA: 0x0AE1CD00 */ set; /* RVA: 0x0AE1CEA8 */ }
        System.String MediaType { set; /* RVA: 0x0AE1CF54 */ }
        System.Collections.Generic.ICollection<System.Net.Http.Headers.NameValueHeaderValue> Parameters { get; /* RVA: 0x0AE1CE24 */ }

        // Methods
        // RVA: 0x0AE1CCF8  token: 0x600017C
        public System.Void .ctor(System.String mediaType) { }
        // RVA: 0x0AE1CA1C  token: 0x600017D
        protected System.Void .ctor(System.Net.Http.Headers.MediaTypeHeaderValue source) { }
        // RVA: 0x041E1670  token: 0x600017E
        private System.Void .ctor() { }
        // RVA: 0x0AE1C66C  token: 0x6000183
        private virtual System.Object System.ICloneable.Clone() { }
        // RVA: 0x0AE1C570  token: 0x6000184
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AE1C5FC  token: 0x6000185
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AE1C6CC  token: 0x6000186
        public virtual System.String ToString() { }
        // RVA: 0x0AE1C8A0  token: 0x6000187
        public static System.Boolean TryParse(System.String input, System.Net.Http.Headers.MediaTypeHeaderValue& parsedValue) { }
        // RVA: 0x0AE1C708  token: 0x6000188
        private static System.Nullable<System.Net.Http.Headers.Token> TryParseMediaType(System.Net.Http.Headers.Lexer lexer, System.String& media) { }

    }

    // TypeToken: 0x2000039  // size: 0x20
    public sealed class MediaTypeWithQualityHeaderValue : System.Net.Http.Headers.MediaTypeHeaderValue
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600018C
        private System.Void .ctor() { }
        // RVA: 0x0AE1D0E0  token: 0x600018D
        private static System.Boolean TryParseElement(System.Net.Http.Headers.Lexer lexer, System.Net.Http.Headers.MediaTypeWithQualityHeaderValue& parsedValue, System.Net.Http.Headers.Token& t) { }
        // RVA: 0x0AE1D254  token: 0x600018E
        private static System.Boolean TryParse(System.String input, System.Int32 minimalCount, System.Collections.Generic.List<System.Net.Http.Headers.MediaTypeWithQualityHeaderValue>& result) { }

    }

    // TypeToken: 0x200003A  // size: 0x20
    public class NameValueHeaderValue : System.ICloneable
    {
        // Fields
        private System.String value;  // 0x10
        private System.String <Name>k__BackingField;  // 0x18

        // Properties
        System.String Name { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.String Value { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x0AE1FB08 */ }

        // Methods
        // RVA: 0x0AE1FABC  token: 0x600018F
        public System.Void .ctor(System.String name, System.String value) { }
        // RVA: 0x0AE1FA80  token: 0x6000190
        protected internal System.Void .ctor(System.Net.Http.Headers.NameValueHeaderValue source) { }
        // RVA: 0x041E1670  token: 0x6000191
        private System.Void .ctor() { }
        // RVA: 0x0AE1F37C  token: 0x6000196
        private static System.Net.Http.Headers.NameValueHeaderValue Create(System.String name, System.String value) { }
        // RVA: 0x0AE1F498  token: 0x6000197
        private virtual System.Object System.ICloneable.Clone() { }
        // RVA: 0x0AE11B20  token: 0x6000198
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AE1F3EC  token: 0x6000199
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AE1F9D0  token: 0x600019A
        private static System.Boolean TryParsePragma(System.String input, System.Int32 minimalCount, System.Collections.Generic.List<System.Net.Http.Headers.NameValueHeaderValue>& result) { }
        // RVA: 0x0AE1F724  token: 0x600019B
        private static System.Boolean TryParseParameters(System.Net.Http.Headers.Lexer lexer, System.Collections.Generic.List<System.Net.Http.Headers.NameValueHeaderValue>& result, System.Net.Http.Headers.Token& t) { }
        // RVA: 0x0AE1F4F8  token: 0x600019C
        public virtual System.String ToString() { }
        // RVA: 0x0AE1F52C  token: 0x600019D
        private static System.Boolean TryParseElement(System.Net.Http.Headers.Lexer lexer, System.Net.Http.Headers.NameValueHeaderValue& parsedValue, System.Net.Http.Headers.Token& t) { }

    }

    // TypeToken: 0x200003B  // size: 0x28
    public class NameValueWithParametersHeaderValue : System.Net.Http.Headers.NameValueHeaderValue, System.ICloneable
    {
        // Fields
        private System.Collections.Generic.List<System.Net.Http.Headers.NameValueHeaderValue> parameters;  // 0x20

        // Properties
        System.Collections.Generic.ICollection<System.Net.Http.Headers.NameValueHeaderValue> Parameters { get; /* RVA: 0x0AE20268 */ }

        // Methods
        // RVA: 0x0AE20118  token: 0x600019E
        protected System.Void .ctor(System.Net.Http.Headers.NameValueWithParametersHeaderValue source) { }
        // RVA: 0x041E1670  token: 0x600019F
        private System.Void .ctor() { }
        // RVA: 0x0AE1FD3C  token: 0x60001A1
        private virtual System.Object System.ICloneable.Clone() { }
        // RVA: 0x0AE1FC58  token: 0x60001A2
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AE1FCE8  token: 0x60001A3
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AE1FD9C  token: 0x60001A4
        public virtual System.String ToString() { }
        // RVA: 0x0AE20068  token: 0x60001A5
        private static System.Boolean TryParse(System.String input, System.Int32 minimalCount, System.Collections.Generic.List<System.Net.Http.Headers.NameValueWithParametersHeaderValue>& result) { }
        // RVA: 0x0AE1FE24  token: 0x60001A6
        private static System.Boolean TryParseElement(System.Net.Http.Headers.Lexer lexer, System.Net.Http.Headers.NameValueWithParametersHeaderValue& parsedValue, System.Net.Http.Headers.Token& t) { }

    }

    // TypeToken: 0x200003C  // size: 0x10
    public static class Parser
    {
    }

    // TypeToken: 0x2000048  // size: 0x20
    public class ProductHeaderValue : System.ICloneable
    {
        // Fields
        private System.String <Name>k__BackingField;  // 0x10
        private System.String <Version>k__BackingField;  // 0x18

        // Properties
        System.String Name { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        System.String Version { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60001B9
        private System.Void .ctor() { }
        // RVA: 0x03840500  token: 0x60001BE
        private virtual System.Object System.ICloneable.Clone() { }
        // RVA: 0x0AE203EC  token: 0x60001BF
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AE2046C  token: 0x60001C0
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AE2070C  token: 0x60001C1
        private static System.Boolean TryParse(System.String input, System.Int32 minimalCount, System.Collections.Generic.List<System.Net.Http.Headers.ProductHeaderValue>& result) { }
        // RVA: 0x0AE2052C  token: 0x60001C2
        private static System.Boolean TryParseElement(System.Net.Http.Headers.Lexer lexer, System.Net.Http.Headers.ProductHeaderValue& parsedValue, System.Net.Http.Headers.Token& t) { }
        // RVA: 0x0AE204D8  token: 0x60001C3
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000049  // size: 0x20
    public class ProductInfoHeaderValue : System.ICloneable
    {
        // Fields
        private System.String <Comment>k__BackingField;  // 0x10
        private System.Net.Http.Headers.ProductHeaderValue <Product>k__BackingField;  // 0x18

        // Properties
        System.String Comment { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        System.Net.Http.Headers.ProductHeaderValue Product { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }

        // Methods
        // RVA: 0x0AE20BF4  token: 0x60001C4
        public System.Void .ctor(System.Net.Http.Headers.ProductHeaderValue product) { }
        // RVA: 0x041E1670  token: 0x60001C5
        private System.Void .ctor() { }
        // RVA: 0x03840500  token: 0x60001CA
        private virtual System.Object System.ICloneable.Clone() { }
        // RVA: 0x0AE207BC  token: 0x60001CB
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AE20830  token: 0x60001CC
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AE20AA4  token: 0x60001CD
        private static System.Boolean TryParse(System.String input, System.Int32 minimalCount, System.Collections.Generic.List<System.Net.Http.Headers.ProductInfoHeaderValue>& result) { }
        // RVA: 0x0AE20888  token: 0x60001CE
        private static System.Boolean TryParseElement(System.Net.Http.Headers.Lexer lexer, System.Net.Http.Headers.ProductInfoHeaderValue& parsedValue) { }
        // RVA: 0x0AE20860  token: 0x60001CF
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200004A  // size: 0x30
    public class RangeConditionHeaderValue : System.ICloneable
    {
        // Fields
        private System.Nullable<System.DateTimeOffset> <Date>k__BackingField;  // 0x10
        private System.Net.Http.Headers.EntityTagHeaderValue <EntityTag>k__BackingField;  // 0x28

        // Properties
        System.Nullable<System.DateTimeOffset> Date { get; /* RVA: 0x015AD710 */ set; /* RVA: 0x04D99830 */ }
        System.Net.Http.Headers.EntityTagHeaderValue EntityTag { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }

        // Methods
        // RVA: 0x0AE21140  token: 0x60001D0
        public System.Void .ctor(System.DateTimeOffset date) { }
        // RVA: 0x0AE211A0  token: 0x60001D1
        public System.Void .ctor(System.Net.Http.Headers.EntityTagHeaderValue entityTag) { }
        // RVA: 0x03840500  token: 0x60001D6
        private virtual System.Object System.ICloneable.Clone() { }
        // RVA: 0x0AE20C58  token: 0x60001D7
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AE20D5C  token: 0x60001D8
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AE20EAC  token: 0x60001D9
        public static System.Boolean TryParse(System.String input, System.Net.Http.Headers.RangeConditionHeaderValue& parsedValue) { }
        // RVA: 0x0AE20DC8  token: 0x60001DA
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200004B  // size: 0x20
    public class RangeHeaderValue : System.ICloneable
    {
        // Fields
        private System.Collections.Generic.List<System.Net.Http.Headers.RangeItemHeaderValue> ranges;  // 0x10
        private System.String unit;  // 0x18

        // Properties
        System.Collections.Generic.ICollection<System.Net.Http.Headers.RangeItemHeaderValue> Ranges { get; /* RVA: 0x0AE21AEC */ }
        System.String Unit { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x0AE21AAC  token: 0x60001DB
        public System.Void .ctor() { }
        // RVA: 0x0AE21964  token: 0x60001DC
        private System.Void .ctor(System.Net.Http.Headers.RangeHeaderValue source) { }
        // RVA: 0x0AE21310  token: 0x60001DF
        private virtual System.Object System.ICloneable.Clone() { }
        // RVA: 0x0AE21214  token: 0x60001E0
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AE212A0  token: 0x60001E1
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AE21494  token: 0x60001E2
        public static System.Boolean TryParse(System.String input, System.Net.Http.Headers.RangeHeaderValue& parsedValue) { }
        // RVA: 0x0AE21370  token: 0x60001E3
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200004C  // size: 0x30
    public class RangeItemHeaderValue : System.ICloneable
    {
        // Fields
        private System.Nullable<System.Int64> <From>k__BackingField;  // 0x10
        private System.Nullable<System.Int64> <To>k__BackingField;  // 0x20

        // Properties
        System.Nullable<System.Int64> From { get; /* RVA: 0x02B2D510 */ set; /* RVA: 0x04D8C980 */ }
        System.Nullable<System.Int64> To { get; /* RVA: 0x04D8C200 */ set; /* RVA: 0x04D8C210 */ }

        // Methods
        // RVA: 0x0AE21DBC  token: 0x60001E4
        public System.Void .ctor(System.Nullable<System.Int64> from, System.Nullable<System.Int64> to) { }
        // RVA: 0x03840500  token: 0x60001E9
        private virtual System.Object System.ICloneable.Clone() { }
        // RVA: 0x0AE21B70  token: 0x60001EA
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AE21C28  token: 0x60001EB
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AE21C98  token: 0x60001EC
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200004D  // size: 0x38
    public class RetryConditionHeaderValue : System.ICloneable
    {
        // Fields
        private System.Nullable<System.DateTimeOffset> <Date>k__BackingField;  // 0x10
        private System.Nullable<System.TimeSpan> <Delta>k__BackingField;  // 0x28

        // Properties
        System.Nullable<System.DateTimeOffset> Date { get; /* RVA: 0x015AD710 */ set; /* RVA: 0x04D99830 */ }
        System.Nullable<System.TimeSpan> Delta { get; /* RVA: 0x04D8FAD0 */ set; /* RVA: 0x04D8FAE0 */ }

        // Methods
        // RVA: 0x0AE225A0  token: 0x60001ED
        public System.Void .ctor(System.DateTimeOffset date) { }
        // RVA: 0x0AE224C4  token: 0x60001EE
        public System.Void .ctor(System.TimeSpan delta) { }
        // RVA: 0x03840500  token: 0x60001F3
        private virtual System.Object System.ICloneable.Clone() { }
        // RVA: 0x0AE21FAC  token: 0x60001F4
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AE220FC  token: 0x60001F5
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AE222CC  token: 0x60001F6
        public static System.Boolean TryParse(System.String input, System.Net.Http.Headers.RetryConditionHeaderValue& parsedValue) { }
        // RVA: 0x0AE22180  token: 0x60001F7
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200004E  // size: 0x28
    public class StringWithQualityHeaderValue : System.ICloneable
    {
        // Fields
        private System.Nullable<System.Double> <Quality>k__BackingField;  // 0x10
        private System.String <Value>k__BackingField;  // 0x20

        // Properties
        System.Nullable<System.Double> Quality { get; /* RVA: 0x02B2D510 */ set; /* RVA: 0x04D8C980 */ }
        System.String Value { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60001F8
        private System.Void .ctor() { }
        // RVA: 0x03840500  token: 0x60001FD
        private virtual System.Object System.ICloneable.Clone() { }
        // RVA: 0x0AE22A1C  token: 0x60001FE
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AE22AEC  token: 0x60001FF
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AE22F08  token: 0x6000200
        private static System.Boolean TryParse(System.String input, System.Int32 minimalCount, System.Collections.Generic.List<System.Net.Http.Headers.StringWithQualityHeaderValue>& result) { }
        // RVA: 0x0AE22C40  token: 0x6000201
        private static System.Boolean TryParseElement(System.Net.Http.Headers.Lexer lexer, System.Net.Http.Headers.StringWithQualityHeaderValue& parsedValue, System.Net.Http.Headers.Token& t) { }
        // RVA: 0x0AE22B68  token: 0x6000202
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200004F  // size: 0x20
    public class TransferCodingHeaderValue : System.ICloneable
    {
        // Fields
        private System.String value;  // 0x10
        private System.Collections.Generic.List<System.Net.Http.Headers.NameValueHeaderValue> parameters;  // 0x18

        // Properties
        System.Collections.Generic.ICollection<System.Net.Http.Headers.NameValueHeaderValue> Parameters { get; /* RVA: 0x0AE23910 */ }
        System.String Value { get; /* RVA: 0x02B2ECC0 */ }

        // Methods
        // RVA: 0x0AE23684  token: 0x6000203
        protected System.Void .ctor(System.Net.Http.Headers.TransferCodingHeaderValue source) { }
        // RVA: 0x041E1670  token: 0x6000204
        private System.Void .ctor() { }
        // RVA: 0x0AE233A4  token: 0x6000207
        private virtual System.Object System.ICloneable.Clone() { }
        // RVA: 0x0AE2329C  token: 0x6000208
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AE23328  token: 0x6000209
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AE23404  token: 0x600020A
        public virtual System.String ToString() { }
        // RVA: 0x0AE235D4  token: 0x600020B
        private static System.Boolean TryParse(System.String input, System.Int32 minimalCount, System.Collections.Generic.List<System.Net.Http.Headers.TransferCodingHeaderValue>& result) { }
        // RVA: 0x0AE2345C  token: 0x600020C
        private static System.Boolean TryParseElement(System.Net.Http.Headers.Lexer lexer, System.Net.Http.Headers.TransferCodingHeaderValue& parsedValue, System.Net.Http.Headers.Token& t) { }

    }

    // TypeToken: 0x2000050  // size: 0x20
    public sealed class TransferCodingWithQualityHeaderValue : System.Net.Http.Headers.TransferCodingHeaderValue
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600020D
        private System.Void .ctor() { }
        // RVA: 0x0AE27D74  token: 0x600020E
        private static System.Boolean TryParse(System.String input, System.Int32 minimalCount, System.Collections.Generic.List<System.Net.Http.Headers.TransferCodingWithQualityHeaderValue>& result) { }
        // RVA: 0x0AE27BFC  token: 0x600020F
        private static System.Boolean TryParseElement(System.Net.Http.Headers.Lexer lexer, System.Net.Http.Headers.TransferCodingWithQualityHeaderValue& parsedValue, System.Net.Http.Headers.Token& t) { }

    }

    // TypeToken: 0x2000051  // size: 0x30
    public class ViaHeaderValue : System.ICloneable
    {
        // Fields
        private System.String <Comment>k__BackingField;  // 0x10
        private System.String <ProtocolName>k__BackingField;  // 0x18
        private System.String <ProtocolVersion>k__BackingField;  // 0x20
        private System.String <ReceivedBy>k__BackingField;  // 0x28

        // Properties
        System.String Comment { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        System.String ProtocolName { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.String ProtocolVersion { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        System.String ReceivedBy { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000210
        private System.Void .ctor() { }
        // RVA: 0x03840500  token: 0x6000219
        private virtual System.Object System.ICloneable.Clone() { }
        // RVA: 0x0AE27E24  token: 0x600021A
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AE27ED4  token: 0x600021B
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AE283AC  token: 0x600021C
        private static System.Boolean TryParse(System.String input, System.Int32 minimalCount, System.Collections.Generic.List<System.Net.Http.Headers.ViaHeaderValue>& result) { }
        // RVA: 0x0AE280AC  token: 0x600021D
        private static System.Boolean TryParseElement(System.Net.Http.Headers.Lexer lexer, System.Net.Http.Headers.ViaHeaderValue& parsedValue, System.Net.Http.Headers.Token& t) { }
        // RVA: 0x0AE27F9C  token: 0x600021E
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000052  // size: 0x40
    public class WarningHeaderValue : System.ICloneable
    {
        // Fields
        private System.String <Agent>k__BackingField;  // 0x10
        private System.Int32 <Code>k__BackingField;  // 0x18
        private System.Nullable<System.DateTimeOffset> <Date>k__BackingField;  // 0x20
        private System.String <Text>k__BackingField;  // 0x38

        // Properties
        System.String Agent { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        System.Int32 Code { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x011EF9B0 */ }
        System.Nullable<System.DateTimeOffset> Date { get; /* RVA: 0x04D9E6A0 */ set; /* RVA: 0x04DAA340 */ }
        System.String Text { get; /* RVA: 0x04D85A50 */ set; /* RVA: 0x046A91B0 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x600021F
        private System.Void .ctor() { }
        // RVA: 0x0AE2863C  token: 0x6000228
        private static System.Boolean IsCodeValid(System.Int32 code) { }
        // RVA: 0x03840500  token: 0x6000229
        private virtual System.Object System.ICloneable.Clone() { }
        // RVA: 0x0AE2845C  token: 0x600022A
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AE28588  token: 0x600022B
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AE28B40  token: 0x600022C
        private static System.Boolean TryParse(System.String input, System.Int32 minimalCount, System.Collections.Generic.List<System.Net.Http.Headers.WarningHeaderValue>& result) { }
        // RVA: 0x0AE28834  token: 0x600022D
        private static System.Boolean TryParseElement(System.Net.Http.Headers.Lexer lexer, System.Net.Http.Headers.WarningHeaderValue& parsedValue, System.Net.Http.Headers.Token& t) { }
        // RVA: 0x0AE28650  token: 0x600022E
        public virtual System.String ToString() { }

    }

}

