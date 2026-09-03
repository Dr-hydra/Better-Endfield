// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.UnityWebRequestModule.dll
// Classes:  15
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000008  // size: 0x14
    public sealed struct UnityWebRequestMethod
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestMethod Get;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestMethod Post;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestMethod Put;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestMethod Head;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestMethod Custom;  // const

    }

    // TypeToken: 0x2000009  // size: 0x14
    public sealed struct UnityWebRequestError
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestError OK;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestError Unknown;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestError SDKError;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestError UnsupportedProtocol;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestError MalformattedUrl;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestError CannotResolveProxy;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestError CannotResolveHost;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestError CannotConnectToHost;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestError AccessDenied;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestError GenericHttpError;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestError WriteError;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestError ReadError;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestError OutOfMemory;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestError Timeout;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestError HTTPPostError;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestError SSLCannotConnect;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestError Aborted;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestError TooManyRedirects;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestError ReceivedNoData;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestError SSLNotSupported;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestError FailedToSendData;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestError FailedToReceiveData;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestError SSLCertificateError;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestError SSLCipherNotAvailable;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestError SSLCACertError;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestError UnrecognizedContentEncoding;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestError LoginFailed;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestError SSLShutdownFailed;  // const
        public static UnityEngine.Networking.UnityWebRequest.UnityWebRequestError NoInternetConnection;  // const

    }

    // TypeToken: 0x200000A  // size: 0x14
    public sealed struct Result
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.Networking.UnityWebRequest.Result InProgress;  // const
        public static UnityEngine.Networking.UnityWebRequest.Result Success;  // const
        public static UnityEngine.Networking.UnityWebRequest.Result ConnectionError;  // const
        public static UnityEngine.Networking.UnityWebRequest.Result ProtocolError;  // const
        public static UnityEngine.Networking.UnityWebRequest.Result DataProcessingError;  // const

    }

namespace UnityEngine
{

    // TypeToken: 0x2000003  // size: 0x40
    public class WWWForm
    {
        // Fields
        private System.Collections.Generic.List<System.Byte[]> formData;  // 0x10
        private System.Collections.Generic.List<System.String> fieldNames;  // 0x18
        private System.Collections.Generic.List<System.String> fileNames;  // 0x20
        private System.Collections.Generic.List<System.String> types;  // 0x28
        private System.Byte[] boundary;  // 0x30
        private System.Boolean containsFiles;  // 0x38
        private static System.Byte[] dDash;  // static @ 0x0
        private static System.Byte[] crlf;  // static @ 0x8
        private static System.Byte[] contentTypeHeader;  // static @ 0x10
        private static System.Byte[] dispositionHeader;  // static @ 0x18
        private static System.Byte[] endQuote;  // static @ 0x20
        private static System.Byte[] fileNameField;  // static @ 0x28
        private static System.Byte[] ampersand;  // static @ 0x30
        private static System.Byte[] equal;  // static @ 0x38

        // Properties
        System.Text.Encoding DefaultEncoding { get; /* RVA: 0x0B58BA8C */ }
        System.Collections.Generic.Dictionary<System.String,System.String> headers { get; /* RVA: 0x0B58C778 */ }
        System.Byte[] data { get; /* RVA: 0x0B58BA98 */ }

        // Methods
        // RVA: 0x0B58B7B0  token: 0x6000009
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000004  // size: 0x10
    public class WWWTranscoder
    {
        // Fields
        private static System.Byte[] ucHexChars;  // static @ 0x0
        private static System.Byte[] lcHexChars;  // static @ 0x8
        private static System.Byte urlEscapeChar;  // static @ 0x10
        private static System.Byte[] urlSpace;  // static @ 0x18
        private static System.Byte[] dataSpace;  // static @ 0x20
        private static System.Byte[] urlForbidden;  // static @ 0x28
        private static System.Byte qpEscapeChar;  // static @ 0x30
        private static System.Byte[] qpSpace;  // static @ 0x38
        private static System.Byte[] qpForbidden;  // static @ 0x40

        // Methods
        // RVA: 0x0B58D090  token: 0x600000A
        private static System.Byte Hex2Byte(System.Byte[] b, System.Int32 offset) { }
        // RVA: 0x0B58C890  token: 0x600000B
        private static System.Void Byte2Hex(System.Byte b, System.Byte[] hexChars, System.Byte& byte0, System.Byte& byte1) { }
        // RVA: 0x0B58D350  token: 0x600000C
        public static System.Byte[] URLEncode(System.Byte[] toEncode) { }
        // RVA: 0x0B58C984  token: 0x600000D
        public static System.String DataEncode(System.String toEncode) { }
        // RVA: 0x0B58CA40  token: 0x600000E
        public static System.String DataEncode(System.String toEncode, System.Text.Encoding e) { }
        // RVA: 0x0B58C9DC  token: 0x600000F
        public static System.Byte[] DataEncode(System.Byte[] toEncode) { }
        // RVA: 0x0B58D10C  token: 0x6000010
        public static System.String QPEncode(System.String toEncode, System.Text.Encoding e) { }
        // RVA: 0x0B58CD58  token: 0x6000011
        public static System.Byte[] Encode(System.Byte[] input, System.Byte escapeChar, System.Byte[] space, System.Byte[] forbidden, System.Boolean uppercase) { }
        // RVA: 0x0B58C8D0  token: 0x6000012
        private static System.Boolean ByteArrayContains(System.Byte[] array, System.Byte b) { }
        // RVA: 0x0B58D2F8  token: 0x6000013
        public static System.Byte[] URLDecode(System.Byte[] toEncode) { }
        // RVA: 0x0B58C910  token: 0x6000014
        private static System.Boolean ByteSubArrayEquals(System.Byte[] array, System.Int32 index, System.Byte[] comperand) { }
        // RVA: 0x0B58CB14  token: 0x6000015
        public static System.Byte[] Decode(System.Byte[] input, System.Byte escapeChar, System.Byte[] space) { }
        // RVA: 0x0B58D210  token: 0x6000016
        public static System.Boolean SevenBitClean(System.String s, System.Text.Encoding e) { }
        // RVA: 0x0B58D1E0  token: 0x6000017
        public static System.Boolean SevenBitClean(System.Byte* input, System.Int32 inputLength) { }
        // RVA: 0x0B58D3B4  token: 0x6000018
        private static System.Void .cctor() { }

    }

}

namespace UnityEngine.Networking
{

    // TypeToken: 0x2000005
    public interface IMultipartFormSection
    {
        // Properties
        System.String sectionName { get; /* RVA: -1  // abstract */ }
        System.Byte[] sectionData { get; /* RVA: -1  // abstract */ }
        System.String fileName { get; /* RVA: -1  // abstract */ }
        System.String contentType { get; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x2000006  // size: 0x28
    public class UnityWebRequestAsyncOperation : UnityEngine.AsyncOperation
    {
        // Fields
        private UnityEngine.Networking.UnityWebRequest <webRequest>k__BackingField;  // 0x20

        // Properties
        UnityEngine.Networking.UnityWebRequest webRequest { set; /* RVA: 0x05390F40 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x600001E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000007  // size: 0x40
    public class UnityWebRequest : System.IDisposable
    {
        // Fields
        private System.IntPtr m_Ptr;  // 0x10
        private UnityEngine.Networking.DownloadHandler m_DownloadHandler;  // 0x18
        private UnityEngine.Networking.UploadHandler m_UploadHandler;  // 0x20
        private UnityEngine.Networking.CertificateHandler m_CertificateHandler;  // 0x28
        private System.Uri m_Uri;  // 0x30
        public static System.String kHttpVerbGET;  // const
        public static System.String kHttpVerbHEAD;  // const
        public static System.String kHttpVerbPOST;  // const
        public static System.String kHttpVerbPUT;  // const
        public static System.String kHttpVerbCREATE;  // const
        public static System.String kHttpVerbDELETE;  // const
        private System.Boolean <disposeCertificateHandlerOnDispose>k__BackingField;  // 0x38
        private System.Boolean <disposeDownloadHandlerOnDispose>k__BackingField;  // 0x39
        private System.Boolean <disposeUploadHandlerOnDispose>k__BackingField;  // 0x3a

        // Properties
        System.Boolean disposeCertificateHandlerOnDispose { get; /* RVA: 0x011F2A70 */ set; /* RVA: 0x011F2A80 */ }
        System.Boolean disposeDownloadHandlerOnDispose { get; /* RVA: 0x04D86650 */ set; /* RVA: 0x04D86680 */ }
        System.Boolean disposeUploadHandlerOnDispose { get; /* RVA: 0x04D86660 */ set; /* RVA: 0x04D86690 */ }
        System.String method { get; /* RVA: 0x0B58B140 */ set; /* RVA: 0x03E38100 */ }
        System.String error { get; /* RVA: 0x03E375E0 */ }
        System.Boolean use100Continue { get; /* RVA: 0x0B58B308 */ set; /* RVA: 0x0B58B6E4 */ }
        System.Boolean useHttpContinue { get; /* RVA: 0x0B58B33C */ set; /* RVA: 0x0B58B728 */ }
        System.String url { get; /* RVA: 0x0B58B2FC */ set; /* RVA: 0x03E38330 */ }
        System.Uri uri { get; /* RVA: 0x0B58B290 */ set; /* RVA: 0x0B58B5F8 */ }
        System.Int64 responseCode { get; /* RVA: 0x03E375A0 */ }
        System.Single uploadProgress { get; /* RVA: 0x0B58B21C */ }
        System.Boolean isModifiable { get; /* RVA: 0x03E382F0 */ }
        System.Boolean isDone { get; /* RVA: 0x03E37460 */ }
        System.Boolean isNetworkError { get; /* RVA: 0x0B58B128 */ }
        System.Boolean isHttpError { get; /* RVA: 0x0B58B110 */ }
        UnityEngine.Networking.UnityWebRequest.Result result { get; /* RVA: 0x03E37630 */ }
        System.Single downloadProgress { get; /* RVA: 0x0B58B09C */ }
        System.UInt64 uploadedBytes { get; /* RVA: 0x0B58B25C */ }
        System.UInt64 downloadedBytes { get; /* RVA: 0x0B58B0DC */ }
        System.Int32 redirectLimit { get; /* RVA: 0x0B58B1E0 */ set; /* RVA: 0x0B58B510 */ }
        System.Boolean chunkedTransfer { get; /* RVA: 0x0B58B090 */ set; /* RVA: 0x0B58B430 */ }
        UnityEngine.Networking.UploadHandler uploadHandler { get; /* RVA: 0x04DBA910 */ set; /* RVA: 0x03E38060 */ }
        UnityEngine.Networking.DownloadHandler downloadHandler { get; /* RVA: 0x04D96330 */ set; /* RVA: 0x03E37FC0 */ }
        UnityEngine.Networking.CertificateHandler certificateHandler { get; /* RVA: 0x04DBBE70 */ set; /* RVA: 0x0B58B348 */ }
        System.Int32 timeout { get; /* RVA: 0x0B58B1F8 */ set; /* RVA: 0x03E37EE0 */ }
        System.Boolean suppressErrorsToConsole { get; /* RVA: 0x0B58B1EC */ set; /* RVA: 0x0B58B518 */ }

        // Methods
        // RVA: 0x0B5894A4  token: 0x600001F
        private static System.String GetWebErrorString(UnityEngine.Networking.UnityWebRequest.UnityWebRequestError err) { }
        // RVA: 0x0B5890A0  token: 0x6000020
        private static System.String GetHTTPStatusString(System.Int64 responseCode) { }
        // RVA: 0x0B588C34  token: 0x6000027
        public static System.Void ClearCookieCache() { }
        // RVA: 0x0B588C40  token: 0x6000028
        public static System.Void ClearCookieCache(System.Uri uri) { }
        // RVA: 0x0B588BF0  token: 0x6000029
        private static System.Void ClearCookieCache(System.String domain, System.String path) { }
        // RVA: 0x03E37DD0  token: 0x600002A
        private static System.IntPtr Create() { }
        // RVA: 0x034AD5B0  token: 0x600002B
        private System.Void Release() { }
        // RVA: 0x034AD580  token: 0x600002C
        private System.Void InternalDestroy() { }
        // RVA: 0x04DBF3A0  token: 0x600002D
        private System.Void InternalSetDefaults() { }
        // RVA: 0x0B58AFEC  token: 0x600002E
        public System.Void .ctor() { }
        // RVA: 0x0B58B010  token: 0x600002F
        public System.Void .ctor(System.String url) { }
        // RVA: 0x0B58B050  token: 0x6000030
        public System.Void .ctor(System.Uri uri) { }
        // RVA: 0x03E34AF0  token: 0x6000031
        public System.Void .ctor(System.String url, System.String method) { }
        // RVA: 0x0B58AF10  token: 0x6000032
        public System.Void .ctor(System.Uri uri, System.String method) { }
        // RVA: 0x03E37D40  token: 0x6000033
        public System.Void .ctor(System.String url, System.String method, UnityEngine.Networking.DownloadHandler downloadHandler, UnityEngine.Networking.UploadHandler uploadHandler) { }
        // RVA: 0x0B58AF68  token: 0x6000034
        public System.Void .ctor(System.Uri uri, System.String method, UnityEngine.Networking.DownloadHandler downloadHandler, UnityEngine.Networking.UploadHandler uploadHandler) { }
        // RVA: 0x0B588EBC  token: 0x6000035
        protected virtual System.Void Finalize() { }
        // RVA: 0x034AD510  token: 0x6000036
        public virtual System.Void Dispose() { }
        // RVA: 0x034AD630  token: 0x6000037
        private System.Void DisposeHandlers() { }
        // RVA: 0x03E372F0  token: 0x6000038
        private UnityEngine.Networking.UnityWebRequestAsyncOperation BeginWebRequest() { }
        // RVA: 0x0B58A000  token: 0x6000039
        public UnityEngine.AsyncOperation Send() { }
        // RVA: 0x03E372C0  token: 0x600003A
        public UnityEngine.Networking.UnityWebRequestAsyncOperation SendWebRequest() { }
        // RVA: 0x034AD5F0  token: 0x600003B
        public System.Void Abort() { }
        // RVA: 0x03E38210  token: 0x600003C
        private UnityEngine.Networking.UnityWebRequest.UnityWebRequestError SetMethod(UnityEngine.Networking.UnityWebRequest.UnityWebRequestMethod methodType) { }
        // RVA: 0x03E381D0  token: 0x600003D
        private System.Void InternalSetMethod(UnityEngine.Networking.UnityWebRequest.UnityWebRequestMethod methodType) { }
        // RVA: 0x0B58A8D4  token: 0x600003E
        private UnityEngine.Networking.UnityWebRequest.UnityWebRequestError SetCustomMethod(System.String customMethodName) { }
        // RVA: 0x0B589674  token: 0x600003F
        private System.Void InternalSetCustomMethod(System.String customMethodName) { }
        // RVA: 0x0B5890D4  token: 0x6000040
        private UnityEngine.Networking.UnityWebRequest.UnityWebRequestMethod GetMethod() { }
        // RVA: 0x0B589004  token: 0x6000041
        private System.String GetCustomMethod() { }
        // RVA: 0x0B58906C  token: 0x6000044
        private UnityEngine.Networking.UnityWebRequest.UnityWebRequestError GetError() { }
        // RVA: 0x0B589470  token: 0x600004E
        private System.String GetUrl() { }
        // RVA: 0x03E382A0  token: 0x600004F
        private UnityEngine.Networking.UnityWebRequest.UnityWebRequestError SetUrl(System.String url) { }
        // RVA: 0x03E38260  token: 0x6000050
        private System.Void InternalSetUrl(System.String url) { }
        // RVA: 0x0B58943C  token: 0x6000052
        private System.Single GetUploadProgress() { }
        // RVA: 0x0B589754  token: 0x6000053
        private System.Boolean IsExecuting() { }
        // RVA: 0x0B589038  token: 0x600005A
        private System.Single GetDownloadProgress() { }
        // RVA: 0x0B589108  token: 0x600005E
        private System.Int32 GetRedirectLimit() { }
        // RVA: 0x0B58A918  token: 0x600005F
        private System.Void SetRedirectLimitFromScripting(System.Int32 limit) { }
        // RVA: 0x0B588FD0  token: 0x6000062
        private System.Boolean GetChunked() { }
        // RVA: 0x0B58A890  token: 0x6000063
        private UnityEngine.Networking.UnityWebRequest.UnityWebRequestError SetChunked(System.Boolean chunked) { }
        // RVA: 0x0B58913C  token: 0x6000066
        public System.String GetRequestHeader(System.String name) { }
        // RVA: 0x03E37E80  token: 0x6000067
        private UnityEngine.Networking.UnityWebRequest.UnityWebRequestError InternalSetRequestHeader(System.String name, System.String value) { }
        // RVA: 0x03E37E10  token: 0x6000068
        public System.Void SetRequestHeader(System.String name, System.String value) { }
        // RVA: 0x0B5891B4  token: 0x6000069
        public System.String GetResponseHeader(System.String name) { }
        // RVA: 0x0B589180  token: 0x600006A
        private System.String[] GetResponseHeaderKeys() { }
        // RVA: 0x0B5891F8  token: 0x600006B
        public System.Collections.Generic.Dictionary<System.String,System.String> GetResponseHeaders() { }
        // RVA: 0x03E380B0  token: 0x600006C
        private UnityEngine.Networking.UnityWebRequest.UnityWebRequestError SetUploadHandler(UnityEngine.Networking.UploadHandler uh) { }
        // RVA: 0x03E38010  token: 0x600006F
        private UnityEngine.Networking.UnityWebRequest.UnityWebRequestError SetDownloadHandler(UnityEngine.Networking.DownloadHandler dh) { }
        // RVA: 0x0B58A84C  token: 0x6000072
        private UnityEngine.Networking.UnityWebRequest.UnityWebRequestError SetCertificateHandler(UnityEngine.Networking.CertificateHandler ch) { }
        // RVA: 0x0B589408  token: 0x6000075
        private System.Int32 GetTimeoutMsec() { }
        // RVA: 0x03E37F70  token: 0x6000076
        private UnityEngine.Networking.UnityWebRequest.UnityWebRequestError SetTimeoutMsec(System.Int32 timeout) { }
        // RVA: 0x0B58931C  token: 0x6000079
        private System.Boolean GetSuppressErrorsToConsole() { }
        // RVA: 0x0B58A958  token: 0x600007A
        private UnityEngine.Networking.UnityWebRequest.UnityWebRequestError SetSuppressErrorsToConsole(System.Boolean suppress) { }
        // RVA: 0x03E37C60  token: 0x600007D
        public static UnityEngine.Networking.UnityWebRequest Get(System.String uri) { }
        // RVA: 0x0B5894D8  token: 0x600007E
        public static UnityEngine.Networking.UnityWebRequest Get(System.Uri uri) { }
        // RVA: 0x0B588D70  token: 0x600007F
        public static UnityEngine.Networking.UnityWebRequest Delete(System.String uri) { }
        // RVA: 0x0B588CFC  token: 0x6000080
        public static UnityEngine.Networking.UnityWebRequest Delete(System.Uri uri) { }
        // RVA: 0x0B58958C  token: 0x6000081
        public static UnityEngine.Networking.UnityWebRequest Head(System.String uri) { }
        // RVA: 0x0B589600  token: 0x6000082
        public static UnityEngine.Networking.UnityWebRequest Head(System.Uri uri) { }
        // RVA: 0x0B5893AC  token: 0x6000083
        public static UnityEngine.Networking.UnityWebRequest GetTexture(System.String uri) { }
        // RVA: 0x0B589350  token: 0x6000084
        public static UnityEngine.Networking.UnityWebRequest GetTexture(System.String uri, System.Boolean nonReadable) { }
        // RVA: 0x04DA17C0  token: 0x6000085
        public static UnityEngine.Networking.UnityWebRequest GetAudioClip(System.String uri, UnityEngine.AudioType audioType) { }
        // RVA: 0x04DA17C0  token: 0x6000086
        public static UnityEngine.Networking.UnityWebRequest GetAssetBundle(System.String uri) { }
        // RVA: 0x04DA17C0  token: 0x6000087
        public static UnityEngine.Networking.UnityWebRequest GetAssetBundle(System.String uri, System.UInt32 crc) { }
        // RVA: 0x04DA17C0  token: 0x6000088
        public static UnityEngine.Networking.UnityWebRequest GetAssetBundle(System.String uri, System.UInt32 version, System.UInt32 crc) { }
        // RVA: 0x04DA17C0  token: 0x6000089
        public static UnityEngine.Networking.UnityWebRequest GetAssetBundle(System.String uri, UnityEngine.Hash128 hash, System.UInt32 crc) { }
        // RVA: 0x04DA17C0  token: 0x600008A
        public static UnityEngine.Networking.UnityWebRequest GetAssetBundle(System.String uri, UnityEngine.CachedAssetBundle cachedAssetBundle, System.UInt32 crc) { }
        // RVA: 0x0B589E00  token: 0x600008B
        public static UnityEngine.Networking.UnityWebRequest Put(System.String uri, System.Byte[] bodyData) { }
        // RVA: 0x0B589D14  token: 0x600008C
        public static UnityEngine.Networking.UnityWebRequest Put(System.Uri uri, System.Byte[] bodyData) { }
        // RVA: 0x0B589EEC  token: 0x600008D
        public static UnityEngine.Networking.UnityWebRequest Put(System.String uri, System.String bodyData) { }
        // RVA: 0x0B589C00  token: 0x600008E
        public static UnityEngine.Networking.UnityWebRequest Put(System.Uri uri, System.String bodyData) { }
        // RVA: 0x03E352A0  token: 0x600008F
        public static UnityEngine.Networking.UnityWebRequest Post(System.String uri, System.String postData) { }
        // RVA: 0x0B5897BC  token: 0x6000090
        public static UnityEngine.Networking.UnityWebRequest Post(System.Uri uri, System.String postData) { }
        // RVA: 0x03E34A40  token: 0x6000091
        private static System.Void SetupPost(UnityEngine.Networking.UnityWebRequest request, System.String postData) { }
        // RVA: 0x0B589910  token: 0x6000092
        public static UnityEngine.Networking.UnityWebRequest Post(System.String uri, UnityEngine.WWWForm formData) { }
        // RVA: 0x0B58984C  token: 0x6000093
        public static UnityEngine.Networking.UnityWebRequest Post(System.Uri uri, UnityEngine.WWWForm formData) { }
        // RVA: 0x0B58AAA0  token: 0x6000094
        private static System.Void SetupPost(UnityEngine.Networking.UnityWebRequest request, UnityEngine.WWWForm formData) { }
        // RVA: 0x0B589788  token: 0x6000095
        public static UnityEngine.Networking.UnityWebRequest Post(System.String uri, System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> multipartFormSections) { }
        // RVA: 0x0B5898DC  token: 0x6000096
        public static UnityEngine.Networking.UnityWebRequest Post(System.Uri uri, System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> multipartFormSections) { }
        // RVA: 0x0B589A40  token: 0x6000097
        public static UnityEngine.Networking.UnityWebRequest Post(System.String uri, System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> multipartFormSections, System.Byte[] boundary) { }
        // RVA: 0x0B5899A0  token: 0x6000098
        public static UnityEngine.Networking.UnityWebRequest Post(System.Uri uri, System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> multipartFormSections, System.Byte[] boundary) { }
        // RVA: 0x0B58ACEC  token: 0x6000099
        private static System.Void SetupPost(UnityEngine.Networking.UnityWebRequest request, System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> multipartFormSections, System.Byte[] boundary) { }
        // RVA: 0x0B589B70  token: 0x600009A
        public static UnityEngine.Networking.UnityWebRequest Post(System.String uri, System.Collections.Generic.Dictionary<System.String,System.String> formFields) { }
        // RVA: 0x0B589AE0  token: 0x600009B
        public static UnityEngine.Networking.UnityWebRequest Post(System.Uri uri, System.Collections.Generic.Dictionary<System.String,System.String> formFields) { }
        // RVA: 0x0B58A99C  token: 0x600009C
        private static System.Void SetupPost(UnityEngine.Networking.UnityWebRequest request, System.Collections.Generic.Dictionary<System.String,System.String> formFields) { }
        // RVA: 0x0B588DE4  token: 0x600009D
        public static System.String EscapeURL(System.String s) { }
        // RVA: 0x0B588E08  token: 0x600009E
        public static System.String EscapeURL(System.String s, System.Text.Encoding e) { }
        // RVA: 0x0B58AEEC  token: 0x600009F
        public static System.String UnEscapeURL(System.String s) { }
        // RVA: 0x0B58AE3C  token: 0x60000A0
        public static System.String UnEscapeURL(System.String s, System.Text.Encoding e) { }
        // RVA: 0x0B58A00C  token: 0x60000A1
        public static System.Byte[] SerializeFormSections(System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> multipartFormSections, System.Byte[] boundary) { }
        // RVA: 0x0B588F38  token: 0x60000A2
        public static System.Byte[] GenerateBoundary() { }
        // RVA: 0x0B58A614  token: 0x60000A3
        public static System.Byte[] SerializeSimpleForm(System.Collections.Generic.Dictionary<System.String,System.String> formFields) { }

    }

    // TypeToken: 0x200000B  // size: 0x18
    public class CertificateHandler : System.IDisposable
    {
        // Fields
        private System.IntPtr m_Ptr;  // 0x10

        // Methods
        // RVA: 0x0B588BBC  token: 0x60000A4
        private System.Void Release() { }
        // RVA: 0x04D85B20  token: 0x60000A5
        protected virtual System.Boolean ValidateCertificate(System.Byte[] certificateData) { }
        // RVA: 0x04D85B20  token: 0x60000A6
        private System.Boolean ValidateCertificateNative(System.Byte[] certificateData) { }
        // RVA: 0x0B588B94  token: 0x60000A7
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x200000C  // size: 0x18
    public class DownloadHandler : System.IDisposable
    {
        // Fields
        private System.IntPtr m_Ptr;  // 0x10

        // Properties
        System.Byte[] data { get; /* RVA: 0x03E37590 */ }
        System.String text { get; /* RVA: 0x05396108 */ }

        // Methods
        // RVA: 0x034AD730  token: 0x60000A8
        private System.Void Release() { }
        // RVA: 0x041E1670  token: 0x60000A9
        private System.Void .ctor() { }
        // RVA: 0x0496E9B0  token: 0x60000AA
        protected virtual System.Void Finalize() { }
        // RVA: 0x034AD6C0  token: 0x60000AB
        public virtual System.Void Dispose() { }
        // RVA: 0x0302E290  token: 0x60000AE
        protected virtual Unity.Collections.NativeArray<System.Byte> GetNativeData() { }
        // RVA: 0x03E37590  token: 0x60000AF
        protected virtual System.Byte[] GetData() { }
        // RVA: 0x034AD7F0  token: 0x60000B0
        protected virtual System.String GetText() { }
        // RVA: 0x034AD9F0  token: 0x60000B1
        private System.Text.Encoding GetTextEncoder() { }
        // RVA: 0x034ADB60  token: 0x60000B2
        private System.String GetContentType() { }
        // RVA: 0x04D9AE10  token: 0x60000B3
        protected virtual System.Boolean ReceiveData(System.Byte[] data, System.Int32 dataLength) { }
        // RVA: 0x041E1670  token: 0x60000B4
        protected virtual System.Void ReceiveContentLengthHeader(System.UInt64 contentLength) { }
        // RVA: 0x041E1670  token: 0x60000B5
        protected virtual System.Void ReceiveContentLength(System.Int32 contentLength) { }
        // RVA: 0x041E1670  token: 0x60000B6
        protected virtual System.Void CompleteContent() { }
        // RVA: 0x04DBAEC0  token: 0x60000B7
        protected virtual System.Single GetProgress() { }
        // RVA: 0x034AD9A0  token: 0x60000B8
        private static System.Byte* InternalGetByteArray(UnityEngine.Networking.DownloadHandler dh, System.Int32& length) { }
        // RVA: 0x034AD770  token: 0x60000B9
        private static System.Byte[] InternalGetByteArray(UnityEngine.Networking.DownloadHandler dh) { }
        // RVA: 0x034AD8E0  token: 0x60000BA
        private static Unity.Collections.NativeArray<System.Byte> InternalGetNativeArray(UnityEngine.Networking.DownloadHandler dh, Unity.Collections.NativeArray<System.Byte>& nativeArray) { }
        // RVA: 0x034AD6F0  token: 0x60000BB
        private static System.Void DisposeNativeArray(Unity.Collections.NativeArray<System.Byte>& data) { }
        // RVA: 0x034ADBA0  token: 0x60000BC
        private static System.Void CreateNativeArrayForNativeData(Unity.Collections.NativeArray<System.Byte>& data, System.Byte* bytes, System.Int32 length) { }

    }

    // TypeToken: 0x200000D  // size: 0x28
    public sealed class DownloadHandlerBuffer : UnityEngine.Networking.DownloadHandler
    {
        // Fields
        private Unity.Collections.NativeArray<System.Byte> m_NativeData;  // 0x18

        // Methods
        // RVA: 0x034ADC00  token: 0x60000BD
        private static System.IntPtr Create(UnityEngine.Networking.DownloadHandlerBuffer obj) { }
        // RVA: 0x03E37D20  token: 0x60000BE
        private System.Void InternalCreateBuffer() { }
        // RVA: 0x03E37D20  token: 0x60000BF
        public System.Void .ctor() { }
        // RVA: 0x034AD8B0  token: 0x60000C0
        protected virtual Unity.Collections.NativeArray<System.Byte> GetNativeData() { }
        // RVA: 0x034AD690  token: 0x60000C1
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x200000E  // size: 0x18
    public class UploadHandler : System.IDisposable
    {
        // Fields
        private System.IntPtr m_Ptr;  // 0x10

        // Properties
        System.String contentType { set; /* RVA: 0x05396100 */ }

        // Methods
        // RVA: 0x034ADC40  token: 0x60000C2
        private System.Void Release() { }
        // RVA: 0x041E1670  token: 0x60000C3
        private System.Void .ctor() { }
        // RVA: 0x0496E9B0  token: 0x60000C4
        protected virtual System.Void Finalize() { }
        // RVA: 0x034AD140  token: 0x60000C5
        public virtual System.Void Dispose() { }
        // RVA: 0x05396100  token: 0x60000C7
        private virtual System.Void SetContentType(System.String newContentType) { }
        // RVA: 0x034ADC80  token: 0x60000C8
        private System.Void InternalSetContentType(System.String newContentType) { }

    }

    // TypeToken: 0x200000F  // size: 0x28
    public sealed class UploadHandlerRaw : UnityEngine.Networking.UploadHandler
    {
        // Fields
        private Unity.Collections.NativeArray<System.Byte> m_Payload;  // 0x18

        // Methods
        // RVA: 0x034ADCD0  token: 0x60000C9
        private static System.IntPtr Create(UnityEngine.Networking.UploadHandlerRaw self, System.Byte* data, System.Int32 dataLength) { }
        // RVA: 0x034AD040  token: 0x60000CA
        public System.Void .ctor(System.Byte[] data) { }
        // RVA: 0x034AD2D0  token: 0x60000CB
        public System.Void .ctor(Unity.Collections.NativeArray<System.Byte> data, System.Boolean transferOwnership) { }
        // RVA: 0x034AD0E0  token: 0x60000CC
        public virtual System.Void Dispose() { }

    }

}

namespace UnityEngineInternal
{

    // TypeToken: 0x2000002  // size: 0x10
    public static class WebRequestUtils
    {
        // Fields
        private static System.Text.RegularExpressions.Regex domainRegex;  // static @ 0x0

        // Methods
        // RVA: 0x0B58D674  token: 0x6000001
        private static System.String RedirectTo(System.String baseUri, System.String redirectUri) { }
        // RVA: 0x03E383C0  token: 0x6000002
        private static System.String MakeInitialUrl(System.String targetUrl, System.String localUrl) { }
        // RVA: 0x03E3D740  token: 0x6000003
        private static System.String MakeUriString(System.Uri targetUri, System.String targetUrl, System.Boolean prependProtocol) { }
        // RVA: 0x0B58D784  token: 0x6000004
        private static System.String URLDecode(System.String encoded) { }
        // RVA: 0x0432B6E0  token: 0x6000005
        private static System.Void .cctor() { }

    }

}

