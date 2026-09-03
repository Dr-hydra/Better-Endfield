// ========================================================
// Dumped by @desirepro
// Assembly: Common.Beyond.XXEnc.dll
// Classes:  6
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

CLASS: <EncryptStreamAsync>d__18
TYPE:  sealed struct
TOKEN: 0x2000003
SIZE:  0x58
IMPLEMENTS: System.Runtime.CompilerServices.IAsyncStateMachine
FIELDS:
  public            System.Int32                    <>1__state  // 0x10
  public            System.Runtime.CompilerServices.AsyncTaskMethodBuilder<>t__builder  // 0x18
  public            Beyond.XXEnc.XXE1               <>4__this  // 0x30
  public            System.IO.Stream                output  // 0x38
  public            System.IO.Stream                input  // 0x40
  public            System.Int32                    howManyBytesToProcessAtTime  // 0x48
  private           System.Runtime.CompilerServices.TaskAwaiter<>u__1  // 0x50
METHODS:
  RVA=0x067CEE50  token=0x6000023  System.Void MoveNext()
  RVA=0x067CEFF4  token=0x6000024  System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine)
END_CLASS

CLASS: <DecryptStreamAsync>d__29
TYPE:  sealed struct
TOKEN: 0x2000004
SIZE:  0x58
IMPLEMENTS: System.Runtime.CompilerServices.IAsyncStateMachine
FIELDS:
  public            System.Int32                    <>1__state  // 0x10
  public            System.Runtime.CompilerServices.AsyncTaskMethodBuilder<>t__builder  // 0x18
  public            Beyond.XXEnc.XXE1               <>4__this  // 0x30
  public            System.IO.Stream                output  // 0x38
  public            System.IO.Stream                input  // 0x40
  public            System.Int32                    howManyBytesToProcessAtTime  // 0x48
  private           System.Runtime.CompilerServices.TaskAwaiter<>u__1  // 0x50
METHODS:
  RVA=0x067CEC50  token=0x6000025  System.Void MoveNext()
  RVA=0x067CEDF4  token=0x6000026  System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine)
END_CLASS

CLASS: <WorkStreamsAsync>d__35
TYPE:  sealed struct
TOKEN: 0x2000005
SIZE:  0x70
IMPLEMENTS: System.Runtime.CompilerServices.IAsyncStateMachine
FIELDS:
  public            System.Int32                    <>1__state  // 0x10
  public            System.Runtime.CompilerServices.AsyncTaskMethodBuilder<>t__builder  // 0x18
  public            System.Int32                    howManyBytesToProcessAtTime  // 0x30
  public            System.IO.Stream                input  // 0x38
  public            Beyond.XXEnc.XXE1               <>4__this  // 0x40
  public            System.IO.Stream                output  // 0x48
  private           System.Byte[]                   <readBytesBuffer>5__2  // 0x50
  private           System.Byte[]                   <writeBytesBuffer>5__3  // 0x58
  private           System.Runtime.CompilerServices.TaskAwaiter<System.Int32><>u__1  // 0x60
  private           System.Runtime.CompilerServices.TaskAwaiter<>u__2  // 0x68
METHODS:
  RVA=0x067CF050  token=0x6000027  System.Void MoveNext()
  RVA=0x067CF47C  token=0x6000028  System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine)
END_CLASS

CLASS: Beyond.XXEnc.XXE1
TYPE:  sealed class
TOKEN: 0x2000002
SIZE:  0x38
IMPLEMENTS: System.IDisposable
FIELDS:
  public    static  System.Int32                    allowedKeyLength  // const
  public    static  System.Int32                    allowedNonceLength  // const
  public    static  System.Int32                    processBytesAtTime  // const
  private   static  System.Int32                    stateLength  // const
  private   readonly System.UInt32[]                 state  // 0x10
  private           System.Boolean                  isDisposed  // 0x18
  private           System.UInt32[]                 x  // 0x20
  private           System.Byte[]                   tmp  // 0x28
  private           System.Byte                     processedIdx  // 0x30
  private   static readonly System.Byte[]                   sigma  // static @ 0x0
  private   static readonly System.Byte[]                   tau  // static @ 0x8
PROPERTIES:
  State  get=0x02B2ECC0
METHODS:
  RVA=0x02F225D0  token=0x6000001  System.Void .ctor(System.ReadOnlySpan<System.Byte> key, System.ReadOnlySpan<System.Byte> nonce, System.UInt32 counter)
  RVA=0x02F80600  token=0x6000003  System.Void KeySetup(System.ReadOnlySpan<System.Byte> key)
  RVA=0x02F22830  token=0x6000004  System.Void IvSetup(System.ReadOnlySpan<System.Byte> nonce, System.UInt32 counter)
  RVA=0x067CF6CC  token=0x6000005  System.Void EncryptBytes(System.Byte[] output, System.Byte[] input, System.Int32 numBytes)
  RVA=0x067CF7D4  token=0x6000006  System.Void EncryptStream(System.IO.Stream output, System.IO.Stream input, System.Int32 howManyBytesToProcessAtTime)
  RVA=0x067CF9A0  token=0x6000007  System.Threading.Tasks.Task EncryptStreamAsync(System.IO.Stream output, System.IO.Stream input, System.Int32 howManyBytesToProcessAtTime)
  RVA=0x067CF8F0  token=0x6000008  System.Void EncryptBytes(System.Byte[] output, System.Byte[] input)
  RVA=0x02F22C10  token=0x6000009  System.Void TransformBytes(System.Byte[] inoutput)
  RVA=0x05391348  token=0x600000A  System.Void TransformBytes(System.Byte[] inoutput, System.Int32 inoffset, System.Int32 len)
  RVA=0x05391394  token=0x600000B  System.Void TransformBytes(System.Span<System.Byte> inoutput)
  RVA=0x041E1670  token=0x600000C  System.Void CreateWorkSpace()
  RVA=0x067CF924  token=0x600000D  System.Byte[] EncryptBytes(System.Byte[] input, System.Int32 numBytes)
  RVA=0x067CF874  token=0x600000E  System.Byte[] EncryptBytes(System.Byte[] input)
  RVA=0x067CFA98  token=0x600000F  System.Byte[] EncryptString(System.String input)
  RVA=0x067CF6CC  token=0x6000010  System.Void DecryptBytes(System.Byte[] output, System.Byte[] input, System.Int32 numBytes)
  RVA=0x067CF7D4  token=0x6000011  System.Void DecryptStream(System.IO.Stream output, System.IO.Stream input, System.Int32 howManyBytesToProcessAtTime)
  RVA=0x067CF6DC  token=0x6000012  System.Threading.Tasks.Task DecryptStreamAsync(System.IO.Stream output, System.IO.Stream input, System.Int32 howManyBytesToProcessAtTime)
  RVA=0x067CF6A8  token=0x6000013  System.Void DecryptBytes(System.Byte[] output, System.Byte[] input)
  RVA=0x067CF62C  token=0x6000014  System.Byte[] DecryptBytes(System.Byte[] input, System.Int32 numBytes)
  RVA=0x067CF5B0  token=0x6000015  System.Byte[] DecryptBytes(System.Byte[] input)
  RVA=0x067CF7E4  token=0x6000016  System.String DecryptUTF8ByteArray(System.Byte[] input)
  RVA=0x067CFC78  token=0x6000017  System.Void WorkStreams(System.IO.Stream output, System.IO.Stream input, System.Int32 howManyBytesToProcessAtTime)
  RVA=0x067CFB80  token=0x6000018  System.Threading.Tasks.Task WorkStreamsAsync(System.IO.Stream output, System.IO.Stream input, System.Int32 howManyBytesToProcessAtTime)
  RVA=0x067CFB30  token=0x6000019  System.Void WorkBytes(System.Byte[] inoutput)
  RVA=0x067CFB5C  token=0x600001A  System.Void WorkBytes(System.Byte[] output, System.Byte[] input, System.Int32 numBytes)
  RVA=0x02EFF3D0  token=0x600001B  System.Void WorkBytes(System.Byte[] output, System.Int32 outoffset, System.Byte[] input, System.Int32 inoffset, System.Int32 numBytes)
  RVA=0x02F804B0  token=0x600001C  System.Void WorkBytes(System.Span<System.Byte> output, System.Span<System.Byte> input)
  RVA=0x02EFEEE0  token=0x600001D  System.Void RecalXORStream()
  RVA=0x02EFF4E0  token=0x600001E  System.Void QuarterRound(System.UInt32[] x, System.UInt32 a, System.UInt32 b, System.UInt32 c, System.UInt32 d)
  RVA=0x03128790  token=0x600001F  System.Void Finalize()
  RVA=0x03352F30  token=0x6000020  System.Void Dispose()
  RVA=0x031286A0  token=0x6000021  System.Void Dispose(System.Boolean disposing)
  RVA=0x04CAF1A0  token=0x6000022  System.Void .cctor()
END_CLASS

CLASS: Beyond.XXEnc.Util
TYPE:  static class
TOKEN: 0x2000006
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x04D882E0  token=0x6000029  System.UInt32 Rotate(System.UInt32 v, System.Int32 c)
  RVA=0x04D88300  token=0x600002A  System.UInt32 XOr(System.UInt32 v, System.UInt32 w)
  RVA=0x04D882D0  token=0x600002B  System.UInt32 Add(System.UInt32 v, System.UInt32 w)
  RVA=0x04D882C0  token=0x600002C  System.UInt32 AddOne(System.UInt32 v)
  RVA=0x067CF540  token=0x600002D  System.UInt32 U8To32Little(System.ReadOnlySpan<System.Byte> p, System.Int32 inputOffset)
  RVA=0x067CF4D8  token=0x600002E  System.Void ToBytes(System.Byte[] output, System.UInt32 input, System.Int32 outputOffset)
END_CLASS

