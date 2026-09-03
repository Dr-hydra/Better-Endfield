// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.UnityWebRequestModule.dll
// Classes:  15
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

CLASS: UnityWebRequestMethod
TYPE:  sealed struct
TOKEN: 0x2000008
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestMethodGet  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestMethodPost  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestMethodPut  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestMethodHead  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestMethodCustom  // const
METHODS:
END_CLASS

CLASS: UnityWebRequestError
TYPE:  sealed struct
TOKEN: 0x2000009
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestErrorOK  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestErrorUnknown  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestErrorSDKError  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestErrorUnsupportedProtocol  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestErrorMalformattedUrl  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestErrorCannotResolveProxy  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestErrorCannotResolveHost  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestErrorCannotConnectToHost  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestErrorAccessDenied  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestErrorGenericHttpError  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestErrorWriteError  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestErrorReadError  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestErrorOutOfMemory  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestErrorTimeout  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestErrorHTTPPostError  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestErrorSSLCannotConnect  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestErrorAborted  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestErrorTooManyRedirects  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestErrorReceivedNoData  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestErrorSSLNotSupported  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestErrorFailedToSendData  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestErrorFailedToReceiveData  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestErrorSSLCertificateError  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestErrorSSLCipherNotAvailable  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestErrorSSLCACertError  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestErrorUnrecognizedContentEncoding  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestErrorLoginFailed  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestErrorSSLShutdownFailed  // const
  public    static  UnityEngine.Networking.UnityWebRequest.UnityWebRequestErrorNoInternetConnection  // const
METHODS:
END_CLASS

CLASS: Result
TYPE:  sealed struct
TOKEN: 0x200000A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Networking.UnityWebRequest.ResultInProgress  // const
  public    static  UnityEngine.Networking.UnityWebRequest.ResultSuccess  // const
  public    static  UnityEngine.Networking.UnityWebRequest.ResultConnectionError  // const
  public    static  UnityEngine.Networking.UnityWebRequest.ResultProtocolError  // const
  public    static  UnityEngine.Networking.UnityWebRequest.ResultDataProcessingError  // const
METHODS:
END_CLASS

CLASS: UnityEngine.WWWForm
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x40
FIELDS:
  private           System.Collections.Generic.List<System.Byte[]>formData  // 0x10
  private           System.Collections.Generic.List<System.String>fieldNames  // 0x18
  private           System.Collections.Generic.List<System.String>fileNames  // 0x20
  private           System.Collections.Generic.List<System.String>types  // 0x28
  private           System.Byte[]                   boundary  // 0x30
  private           System.Boolean                  containsFiles  // 0x38
  private   static  System.Byte[]                   dDash  // static @ 0x0
  private   static  System.Byte[]                   crlf  // static @ 0x8
  private   static  System.Byte[]                   contentTypeHeader  // static @ 0x10
  private   static  System.Byte[]                   dispositionHeader  // static @ 0x18
  private   static  System.Byte[]                   endQuote  // static @ 0x20
  private   static  System.Byte[]                   fileNameField  // static @ 0x28
  private   static  System.Byte[]                   ampersand  // static @ 0x30
  private   static  System.Byte[]                   equal  // static @ 0x38
PROPERTIES:
  DefaultEncoding  get=0x0B58BA8C
  headers  get=0x0B58C778
  data  get=0x0B58BA98
METHODS:
  RVA=0x0B58B7B0  token=0x6000009  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.WWWTranscoder
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x10
FIELDS:
  private   static  System.Byte[]                   ucHexChars  // static @ 0x0
  private   static  System.Byte[]                   lcHexChars  // static @ 0x8
  private   static  System.Byte                     urlEscapeChar  // static @ 0x10
  private   static  System.Byte[]                   urlSpace  // static @ 0x18
  private   static  System.Byte[]                   dataSpace  // static @ 0x20
  private   static  System.Byte[]                   urlForbidden  // static @ 0x28
  private   static  System.Byte                     qpEscapeChar  // static @ 0x30
  private   static  System.Byte[]                   qpSpace  // static @ 0x38
  private   static  System.Byte[]                   qpForbidden  // static @ 0x40
METHODS:
  RVA=0x0B58D090  token=0x600000A  System.Byte Hex2Byte(System.Byte[] b, System.Int32 offset)
  RVA=0x0B58C890  token=0x600000B  System.Void Byte2Hex(System.Byte b, System.Byte[] hexChars, System.Byte& byte0, System.Byte& byte1)
  RVA=0x0B58D350  token=0x600000C  System.Byte[] URLEncode(System.Byte[] toEncode)
  RVA=0x0B58C984  token=0x600000D  System.String DataEncode(System.String toEncode)
  RVA=0x0B58CA40  token=0x600000E  System.String DataEncode(System.String toEncode, System.Text.Encoding e)
  RVA=0x0B58C9DC  token=0x600000F  System.Byte[] DataEncode(System.Byte[] toEncode)
  RVA=0x0B58D10C  token=0x6000010  System.String QPEncode(System.String toEncode, System.Text.Encoding e)
  RVA=0x0B58CD58  token=0x6000011  System.Byte[] Encode(System.Byte[] input, System.Byte escapeChar, System.Byte[] space, System.Byte[] forbidden, System.Boolean uppercase)
  RVA=0x0B58C8D0  token=0x6000012  System.Boolean ByteArrayContains(System.Byte[] array, System.Byte b)
  RVA=0x0B58D2F8  token=0x6000013  System.Byte[] URLDecode(System.Byte[] toEncode)
  RVA=0x0B58C910  token=0x6000014  System.Boolean ByteSubArrayEquals(System.Byte[] array, System.Int32 index, System.Byte[] comperand)
  RVA=0x0B58CB14  token=0x6000015  System.Byte[] Decode(System.Byte[] input, System.Byte escapeChar, System.Byte[] space)
  RVA=0x0B58D210  token=0x6000016  System.Boolean SevenBitClean(System.String s, System.Text.Encoding e)
  RVA=0x0B58D1E0  token=0x6000017  System.Boolean SevenBitClean(System.Byte* input, System.Int32 inputLength)
  RVA=0x0B58D3B4  token=0x6000018  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Networking.IMultipartFormSection
TYPE:  interface
TOKEN: 0x2000005
FIELDS:
PROPERTIES:
  sectionName  get=-1  // abstract
  sectionData  get=-1  // abstract
  fileName  get=-1  // abstract
  contentType  get=-1  // abstract
METHODS:
END_CLASS

CLASS: UnityEngine.Networking.UnityWebRequestAsyncOperation
TYPE:  class
TOKEN: 0x2000006
SIZE:  0x28
EXTENDS: UnityEngine.AsyncOperation
FIELDS:
  private           UnityEngine.Networking.UnityWebRequest<webRequest>k__BackingField  // 0x20
PROPERTIES:
  webRequest  set=0x05390F40
METHODS:
  RVA=0x041E1670  token=0x600001E  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.Networking.UnityWebRequest
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x40
IMPLEMENTS: System.IDisposable
FIELDS:
  private           System.IntPtr                   m_Ptr  // 0x10
  private           UnityEngine.Networking.DownloadHandlerm_DownloadHandler  // 0x18
  private           UnityEngine.Networking.UploadHandlerm_UploadHandler  // 0x20
  private           UnityEngine.Networking.CertificateHandlerm_CertificateHandler  // 0x28
  private           System.Uri                      m_Uri  // 0x30
  public    static  System.String                   kHttpVerbGET  // const
  public    static  System.String                   kHttpVerbHEAD  // const
  public    static  System.String                   kHttpVerbPOST  // const
  public    static  System.String                   kHttpVerbPUT  // const
  public    static  System.String                   kHttpVerbCREATE  // const
  public    static  System.String                   kHttpVerbDELETE  // const
  private           System.Boolean                  <disposeCertificateHandlerOnDispose>k__BackingField  // 0x38
  private           System.Boolean                  <disposeDownloadHandlerOnDispose>k__BackingField  // 0x39
  private           System.Boolean                  <disposeUploadHandlerOnDispose>k__BackingField  // 0x3a
PROPERTIES:
  disposeCertificateHandlerOnDispose  get=0x011F2A70  set=0x011F2A80
  disposeDownloadHandlerOnDispose  get=0x04D86650  set=0x04D86680
  disposeUploadHandlerOnDispose  get=0x04D86660  set=0x04D86690
  method  get=0x0B58B140  set=0x03E38100
  error  get=0x03E375E0
  use100Continue  get=0x0B58B308  set=0x0B58B6E4
  useHttpContinue  get=0x0B58B33C  set=0x0B58B728
  url  get=0x0B58B2FC  set=0x03E38330
  uri  get=0x0B58B290  set=0x0B58B5F8
  responseCode  get=0x03E375A0
  uploadProgress  get=0x0B58B21C
  isModifiable  get=0x03E382F0
  isDone  get=0x03E37460
  isNetworkError  get=0x0B58B128
  isHttpError  get=0x0B58B110
  result  get=0x03E37630
  downloadProgress  get=0x0B58B09C
  uploadedBytes  get=0x0B58B25C
  downloadedBytes  get=0x0B58B0DC
  redirectLimit  get=0x0B58B1E0  set=0x0B58B510
  chunkedTransfer  get=0x0B58B090  set=0x0B58B430
  uploadHandler  get=0x04DBA910  set=0x03E38060
  downloadHandler  get=0x04D96330  set=0x03E37FC0
  certificateHandler  get=0x04DBBE70  set=0x0B58B348
  timeout  get=0x0B58B1F8  set=0x03E37EE0
  suppressErrorsToConsole  get=0x0B58B1EC  set=0x0B58B518
METHODS:
  RVA=0x0B5894A4  token=0x600001F  System.String GetWebErrorString(UnityEngine.Networking.UnityWebRequest.UnityWebRequestError err)
  RVA=0x0B5890A0  token=0x6000020  System.String GetHTTPStatusString(System.Int64 responseCode)
  RVA=0x0B588C34  token=0x6000027  System.Void ClearCookieCache()
  RVA=0x0B588C40  token=0x6000028  System.Void ClearCookieCache(System.Uri uri)
  RVA=0x0B588BF0  token=0x6000029  System.Void ClearCookieCache(System.String domain, System.String path)
  RVA=0x03E37DD0  token=0x600002A  System.IntPtr Create()
  RVA=0x034AD5B0  token=0x600002B  System.Void Release()
  RVA=0x034AD580  token=0x600002C  System.Void InternalDestroy()
  RVA=0x04DBF3A0  token=0x600002D  System.Void InternalSetDefaults()
  RVA=0x0B58AFEC  token=0x600002E  System.Void .ctor()
  RVA=0x0B58B010  token=0x600002F  System.Void .ctor(System.String url)
  RVA=0x0B58B050  token=0x6000030  System.Void .ctor(System.Uri uri)
  RVA=0x03E34AF0  token=0x6000031  System.Void .ctor(System.String url, System.String method)
  RVA=0x0B58AF10  token=0x6000032  System.Void .ctor(System.Uri uri, System.String method)
  RVA=0x03E37D40  token=0x6000033  System.Void .ctor(System.String url, System.String method, UnityEngine.Networking.DownloadHandler downloadHandler, UnityEngine.Networking.UploadHandler uploadHandler)
  RVA=0x0B58AF68  token=0x6000034  System.Void .ctor(System.Uri uri, System.String method, UnityEngine.Networking.DownloadHandler downloadHandler, UnityEngine.Networking.UploadHandler uploadHandler)
  RVA=0x0B588EBC  token=0x6000035  System.Void Finalize()
  RVA=0x034AD510  token=0x6000036  System.Void Dispose()
  RVA=0x034AD630  token=0x6000037  System.Void DisposeHandlers()
  RVA=0x03E372F0  token=0x6000038  UnityEngine.Networking.UnityWebRequestAsyncOperation BeginWebRequest()
  RVA=0x0B58A000  token=0x6000039  UnityEngine.AsyncOperation Send()
  RVA=0x03E372C0  token=0x600003A  UnityEngine.Networking.UnityWebRequestAsyncOperation SendWebRequest()
  RVA=0x034AD5F0  token=0x600003B  System.Void Abort()
  RVA=0x03E38210  token=0x600003C  UnityEngine.Networking.UnityWebRequest.UnityWebRequestError SetMethod(UnityEngine.Networking.UnityWebRequest.UnityWebRequestMethod methodType)
  RVA=0x03E381D0  token=0x600003D  System.Void InternalSetMethod(UnityEngine.Networking.UnityWebRequest.UnityWebRequestMethod methodType)
  RVA=0x0B58A8D4  token=0x600003E  UnityEngine.Networking.UnityWebRequest.UnityWebRequestError SetCustomMethod(System.String customMethodName)
  RVA=0x0B589674  token=0x600003F  System.Void InternalSetCustomMethod(System.String customMethodName)
  RVA=0x0B5890D4  token=0x6000040  UnityEngine.Networking.UnityWebRequest.UnityWebRequestMethod GetMethod()
  RVA=0x0B589004  token=0x6000041  System.String GetCustomMethod()
  RVA=0x0B58906C  token=0x6000044  UnityEngine.Networking.UnityWebRequest.UnityWebRequestError GetError()
  RVA=0x0B589470  token=0x600004E  System.String GetUrl()
  RVA=0x03E382A0  token=0x600004F  UnityEngine.Networking.UnityWebRequest.UnityWebRequestError SetUrl(System.String url)
  RVA=0x03E38260  token=0x6000050  System.Void InternalSetUrl(System.String url)
  RVA=0x0B58943C  token=0x6000052  System.Single GetUploadProgress()
  RVA=0x0B589754  token=0x6000053  System.Boolean IsExecuting()
  RVA=0x0B589038  token=0x600005A  System.Single GetDownloadProgress()
  RVA=0x0B589108  token=0x600005E  System.Int32 GetRedirectLimit()
  RVA=0x0B58A918  token=0x600005F  System.Void SetRedirectLimitFromScripting(System.Int32 limit)
  RVA=0x0B588FD0  token=0x6000062  System.Boolean GetChunked()
  RVA=0x0B58A890  token=0x6000063  UnityEngine.Networking.UnityWebRequest.UnityWebRequestError SetChunked(System.Boolean chunked)
  RVA=0x0B58913C  token=0x6000066  System.String GetRequestHeader(System.String name)
  RVA=0x03E37E80  token=0x6000067  UnityEngine.Networking.UnityWebRequest.UnityWebRequestError InternalSetRequestHeader(System.String name, System.String value)
  RVA=0x03E37E10  token=0x6000068  System.Void SetRequestHeader(System.String name, System.String value)
  RVA=0x0B5891B4  token=0x6000069  System.String GetResponseHeader(System.String name)
  RVA=0x0B589180  token=0x600006A  System.String[] GetResponseHeaderKeys()
  RVA=0x0B5891F8  token=0x600006B  System.Collections.Generic.Dictionary<System.String,System.String> GetResponseHeaders()
  RVA=0x03E380B0  token=0x600006C  UnityEngine.Networking.UnityWebRequest.UnityWebRequestError SetUploadHandler(UnityEngine.Networking.UploadHandler uh)
  RVA=0x03E38010  token=0x600006F  UnityEngine.Networking.UnityWebRequest.UnityWebRequestError SetDownloadHandler(UnityEngine.Networking.DownloadHandler dh)
  RVA=0x0B58A84C  token=0x6000072  UnityEngine.Networking.UnityWebRequest.UnityWebRequestError SetCertificateHandler(UnityEngine.Networking.CertificateHandler ch)
  RVA=0x0B589408  token=0x6000075  System.Int32 GetTimeoutMsec()
  RVA=0x03E37F70  token=0x6000076  UnityEngine.Networking.UnityWebRequest.UnityWebRequestError SetTimeoutMsec(System.Int32 timeout)
  RVA=0x0B58931C  token=0x6000079  System.Boolean GetSuppressErrorsToConsole()
  RVA=0x0B58A958  token=0x600007A  UnityEngine.Networking.UnityWebRequest.UnityWebRequestError SetSuppressErrorsToConsole(System.Boolean suppress)
  RVA=0x03E37C60  token=0x600007D  UnityEngine.Networking.UnityWebRequest Get(System.String uri)
  RVA=0x0B5894D8  token=0x600007E  UnityEngine.Networking.UnityWebRequest Get(System.Uri uri)
  RVA=0x0B588D70  token=0x600007F  UnityEngine.Networking.UnityWebRequest Delete(System.String uri)
  RVA=0x0B588CFC  token=0x6000080  UnityEngine.Networking.UnityWebRequest Delete(System.Uri uri)
  RVA=0x0B58958C  token=0x6000081  UnityEngine.Networking.UnityWebRequest Head(System.String uri)
  RVA=0x0B589600  token=0x6000082  UnityEngine.Networking.UnityWebRequest Head(System.Uri uri)
  RVA=0x0B5893AC  token=0x6000083  UnityEngine.Networking.UnityWebRequest GetTexture(System.String uri)
  RVA=0x0B589350  token=0x6000084  UnityEngine.Networking.UnityWebRequest GetTexture(System.String uri, System.Boolean nonReadable)
  RVA=0x04DA17C0  token=0x6000085  UnityEngine.Networking.UnityWebRequest GetAudioClip(System.String uri, UnityEngine.AudioType audioType)
  RVA=0x04DA17C0  token=0x6000086  UnityEngine.Networking.UnityWebRequest GetAssetBundle(System.String uri)
  RVA=0x04DA17C0  token=0x6000087  UnityEngine.Networking.UnityWebRequest GetAssetBundle(System.String uri, System.UInt32 crc)
  RVA=0x04DA17C0  token=0x6000088  UnityEngine.Networking.UnityWebRequest GetAssetBundle(System.String uri, System.UInt32 version, System.UInt32 crc)
  RVA=0x04DA17C0  token=0x6000089  UnityEngine.Networking.UnityWebRequest GetAssetBundle(System.String uri, UnityEngine.Hash128 hash, System.UInt32 crc)
  RVA=0x04DA17C0  token=0x600008A  UnityEngine.Networking.UnityWebRequest GetAssetBundle(System.String uri, UnityEngine.CachedAssetBundle cachedAssetBundle, System.UInt32 crc)
  RVA=0x0B589E00  token=0x600008B  UnityEngine.Networking.UnityWebRequest Put(System.String uri, System.Byte[] bodyData)
  RVA=0x0B589D14  token=0x600008C  UnityEngine.Networking.UnityWebRequest Put(System.Uri uri, System.Byte[] bodyData)
  RVA=0x0B589EEC  token=0x600008D  UnityEngine.Networking.UnityWebRequest Put(System.String uri, System.String bodyData)
  RVA=0x0B589C00  token=0x600008E  UnityEngine.Networking.UnityWebRequest Put(System.Uri uri, System.String bodyData)
  RVA=0x03E352A0  token=0x600008F  UnityEngine.Networking.UnityWebRequest Post(System.String uri, System.String postData)
  RVA=0x0B5897BC  token=0x6000090  UnityEngine.Networking.UnityWebRequest Post(System.Uri uri, System.String postData)
  RVA=0x03E34A40  token=0x6000091  System.Void SetupPost(UnityEngine.Networking.UnityWebRequest request, System.String postData)
  RVA=0x0B589910  token=0x6000092  UnityEngine.Networking.UnityWebRequest Post(System.String uri, UnityEngine.WWWForm formData)
  RVA=0x0B58984C  token=0x6000093  UnityEngine.Networking.UnityWebRequest Post(System.Uri uri, UnityEngine.WWWForm formData)
  RVA=0x0B58AAA0  token=0x6000094  System.Void SetupPost(UnityEngine.Networking.UnityWebRequest request, UnityEngine.WWWForm formData)
  RVA=0x0B589788  token=0x6000095  UnityEngine.Networking.UnityWebRequest Post(System.String uri, System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> multipartFormSections)
  RVA=0x0B5898DC  token=0x6000096  UnityEngine.Networking.UnityWebRequest Post(System.Uri uri, System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> multipartFormSections)
  RVA=0x0B589A40  token=0x6000097  UnityEngine.Networking.UnityWebRequest Post(System.String uri, System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> multipartFormSections, System.Byte[] boundary)
  RVA=0x0B5899A0  token=0x6000098  UnityEngine.Networking.UnityWebRequest Post(System.Uri uri, System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> multipartFormSections, System.Byte[] boundary)
  RVA=0x0B58ACEC  token=0x6000099  System.Void SetupPost(UnityEngine.Networking.UnityWebRequest request, System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> multipartFormSections, System.Byte[] boundary)
  RVA=0x0B589B70  token=0x600009A  UnityEngine.Networking.UnityWebRequest Post(System.String uri, System.Collections.Generic.Dictionary<System.String,System.String> formFields)
  RVA=0x0B589AE0  token=0x600009B  UnityEngine.Networking.UnityWebRequest Post(System.Uri uri, System.Collections.Generic.Dictionary<System.String,System.String> formFields)
  RVA=0x0B58A99C  token=0x600009C  System.Void SetupPost(UnityEngine.Networking.UnityWebRequest request, System.Collections.Generic.Dictionary<System.String,System.String> formFields)
  RVA=0x0B588DE4  token=0x600009D  System.String EscapeURL(System.String s)
  RVA=0x0B588E08  token=0x600009E  System.String EscapeURL(System.String s, System.Text.Encoding e)
  RVA=0x0B58AEEC  token=0x600009F  System.String UnEscapeURL(System.String s)
  RVA=0x0B58AE3C  token=0x60000A0  System.String UnEscapeURL(System.String s, System.Text.Encoding e)
  RVA=0x0B58A00C  token=0x60000A1  System.Byte[] SerializeFormSections(System.Collections.Generic.List<UnityEngine.Networking.IMultipartFormSection> multipartFormSections, System.Byte[] boundary)
  RVA=0x0B588F38  token=0x60000A2  System.Byte[] GenerateBoundary()
  RVA=0x0B58A614  token=0x60000A3  System.Byte[] SerializeSimpleForm(System.Collections.Generic.Dictionary<System.String,System.String> formFields)
END_CLASS

CLASS: UnityEngine.Networking.CertificateHandler
TYPE:  class
TOKEN: 0x200000B
SIZE:  0x18
IMPLEMENTS: System.IDisposable
FIELDS:
  private           System.IntPtr                   m_Ptr  // 0x10
METHODS:
  RVA=0x0B588BBC  token=0x60000A4  System.Void Release()
  RVA=0x04D85B20  token=0x60000A5  System.Boolean ValidateCertificate(System.Byte[] certificateData)
  RVA=0x04D85B20  token=0x60000A6  System.Boolean ValidateCertificateNative(System.Byte[] certificateData)
  RVA=0x0B588B94  token=0x60000A7  System.Void Dispose()
END_CLASS

CLASS: UnityEngine.Networking.DownloadHandler
TYPE:  class
TOKEN: 0x200000C
SIZE:  0x18
IMPLEMENTS: System.IDisposable
FIELDS:
  private           System.IntPtr                   m_Ptr  // 0x10
PROPERTIES:
  data  get=0x03E37590
  text  get=0x05396108
METHODS:
  RVA=0x034AD730  token=0x60000A8  System.Void Release()
  RVA=0x041E1670  token=0x60000A9  System.Void .ctor()
  RVA=0x0496E9B0  token=0x60000AA  System.Void Finalize()
  RVA=0x034AD6C0  token=0x60000AB  System.Void Dispose()
  RVA=0x0302E290  token=0x60000AE  Unity.Collections.NativeArray<System.Byte> GetNativeData()
  RVA=0x03E37590  token=0x60000AF  System.Byte[] GetData()
  RVA=0x034AD7F0  token=0x60000B0  System.String GetText()
  RVA=0x034AD9F0  token=0x60000B1  System.Text.Encoding GetTextEncoder()
  RVA=0x034ADB60  token=0x60000B2  System.String GetContentType()
  RVA=0x04D9AE10  token=0x60000B3  System.Boolean ReceiveData(System.Byte[] data, System.Int32 dataLength)
  RVA=0x041E1670  token=0x60000B4  System.Void ReceiveContentLengthHeader(System.UInt64 contentLength)
  RVA=0x041E1670  token=0x60000B5  System.Void ReceiveContentLength(System.Int32 contentLength)
  RVA=0x041E1670  token=0x60000B6  System.Void CompleteContent()
  RVA=0x04DBAEC0  token=0x60000B7  System.Single GetProgress()
  RVA=0x034AD9A0  token=0x60000B8  System.Byte* InternalGetByteArray(UnityEngine.Networking.DownloadHandler dh, System.Int32& length)
  RVA=0x034AD770  token=0x60000B9  System.Byte[] InternalGetByteArray(UnityEngine.Networking.DownloadHandler dh)
  RVA=0x034AD8E0  token=0x60000BA  Unity.Collections.NativeArray<System.Byte> InternalGetNativeArray(UnityEngine.Networking.DownloadHandler dh, Unity.Collections.NativeArray<System.Byte>& nativeArray)
  RVA=0x034AD6F0  token=0x60000BB  System.Void DisposeNativeArray(Unity.Collections.NativeArray<System.Byte>& data)
  RVA=0x034ADBA0  token=0x60000BC  System.Void CreateNativeArrayForNativeData(Unity.Collections.NativeArray<System.Byte>& data, System.Byte* bytes, System.Int32 length)
END_CLASS

CLASS: UnityEngine.Networking.DownloadHandlerBuffer
TYPE:  sealed class
TOKEN: 0x200000D
SIZE:  0x28
EXTENDS: UnityEngine.Networking.DownloadHandler
FIELDS:
  private           Unity.Collections.NativeArray<System.Byte>m_NativeData  // 0x18
METHODS:
  RVA=0x034ADC00  token=0x60000BD  System.IntPtr Create(UnityEngine.Networking.DownloadHandlerBuffer obj)
  RVA=0x03E37D20  token=0x60000BE  System.Void InternalCreateBuffer()
  RVA=0x03E37D20  token=0x60000BF  System.Void .ctor()
  RVA=0x034AD8B0  token=0x60000C0  Unity.Collections.NativeArray<System.Byte> GetNativeData()
  RVA=0x034AD690  token=0x60000C1  System.Void Dispose()
END_CLASS

CLASS: UnityEngine.Networking.UploadHandler
TYPE:  class
TOKEN: 0x200000E
SIZE:  0x18
IMPLEMENTS: System.IDisposable
FIELDS:
  private           System.IntPtr                   m_Ptr  // 0x10
PROPERTIES:
  contentType  set=0x05396100
METHODS:
  RVA=0x034ADC40  token=0x60000C2  System.Void Release()
  RVA=0x041E1670  token=0x60000C3  System.Void .ctor()
  RVA=0x0496E9B0  token=0x60000C4  System.Void Finalize()
  RVA=0x034AD140  token=0x60000C5  System.Void Dispose()
  RVA=0x05396100  token=0x60000C7  System.Void SetContentType(System.String newContentType)
  RVA=0x034ADC80  token=0x60000C8  System.Void InternalSetContentType(System.String newContentType)
END_CLASS

CLASS: UnityEngine.Networking.UploadHandlerRaw
TYPE:  sealed class
TOKEN: 0x200000F
SIZE:  0x28
EXTENDS: UnityEngine.Networking.UploadHandler
FIELDS:
  private           Unity.Collections.NativeArray<System.Byte>m_Payload  // 0x18
METHODS:
  RVA=0x034ADCD0  token=0x60000C9  System.IntPtr Create(UnityEngine.Networking.UploadHandlerRaw self, System.Byte* data, System.Int32 dataLength)
  RVA=0x034AD040  token=0x60000CA  System.Void .ctor(System.Byte[] data)
  RVA=0x034AD2D0  token=0x60000CB  System.Void .ctor(Unity.Collections.NativeArray<System.Byte> data, System.Boolean transferOwnership)
  RVA=0x034AD0E0  token=0x60000CC  System.Void Dispose()
END_CLASS

CLASS: UnityEngineInternal.WebRequestUtils
TYPE:  static class
TOKEN: 0x2000002
SIZE:  0x10
FIELDS:
  private   static  System.Text.RegularExpressions.RegexdomainRegex  // static @ 0x0
METHODS:
  RVA=0x0B58D674  token=0x6000001  System.String RedirectTo(System.String baseUri, System.String redirectUri)
  RVA=0x03E383C0  token=0x6000002  System.String MakeInitialUrl(System.String targetUrl, System.String localUrl)
  RVA=0x03E3D740  token=0x6000003  System.String MakeUriString(System.Uri targetUri, System.String targetUrl, System.Boolean prependProtocol)
  RVA=0x0B58D784  token=0x6000004  System.String URLDecode(System.String encoded)
  RVA=0x0432B6E0  token=0x6000005  System.Void .cctor()
END_CLASS

