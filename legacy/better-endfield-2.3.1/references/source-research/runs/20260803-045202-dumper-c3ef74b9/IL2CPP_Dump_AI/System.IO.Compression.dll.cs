// ========================================================
// Dumped by @desirepro
// Assembly: System.IO.Compression.dll
// Classes:  18
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

CLASS: Brotli
TYPE:  static class
TOKEN: 0x2000003
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0454C8D0  token=0x6000001  Microsoft.Win32.SafeHandles.SafeBrotliDecoderHandle BrotliDecoderCreateInstance(System.IntPtr allocFunc, System.IntPtr freeFunc, System.IntPtr opaque)
  RVA=0x03078030  token=0x6000002  System.Int32 BrotliDecoderDecompressStream(Microsoft.Win32.SafeHandles.SafeBrotliDecoderHandle state, System.IntPtr& availableIn, System.Byte** nextIn, System.IntPtr& availableOut, System.Byte** nextOut, System.IntPtr& totalOut)
  RVA=0x053982CC  token=0x6000003  System.Void BrotliDecoderDestroyInstance(System.IntPtr state)
  RVA=0x03077FD0  token=0x6000004  System.Boolean BrotliDecoderIsFinished(Microsoft.Win32.SafeHandles.SafeBrotliDecoderHandle state)
  RVA=0x0AE10054  token=0x6000005  Microsoft.Win32.SafeHandles.SafeBrotliEncoderHandle BrotliEncoderCreateInstance(System.IntPtr allocFunc, System.IntPtr freeFunc, System.IntPtr opaque)
  RVA=0x0AE10144  token=0x6000006  System.Boolean BrotliEncoderSetParameter(Microsoft.Win32.SafeHandles.SafeBrotliEncoderHandle state, System.IO.Compression.BrotliEncoderParameter parameter, System.UInt32 value)
  RVA=0x0AE0FFA4  token=0x6000007  System.Boolean BrotliEncoderCompressStream(Microsoft.Win32.SafeHandles.SafeBrotliEncoderHandle state, System.IO.Compression.BrotliEncoderOperation op, System.IntPtr& availableIn, System.Byte** nextIn, System.IntPtr& availableOut, System.Byte** nextOut, System.IntPtr& totalOut)
  RVA=0x0AE100D4  token=0x6000008  System.Boolean BrotliEncoderHasMoreOutput(Microsoft.Win32.SafeHandles.SafeBrotliEncoderHandle state)
  RVA=0x0AE100CC  token=0x6000009  System.Void BrotliEncoderDestroyInstance(System.IntPtr state)
END_CLASS

CLASS: Interop
TYPE:  static class
TOKEN: 0x2000002
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: SR
TYPE:  static class
TOKEN: 0x2000004
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0AE10244  token=0x600000A  System.String Format(System.String resourceFormat, System.Object p1)
  RVA=0x0AE101C4  token=0x600000B  System.String Format(System.String resourceFormat, System.Object p1, System.Object p2, System.Object p3)
END_CLASS

CLASS: <FinishReadAsyncMemory>d__41
TYPE:  sealed struct
TOKEN: 0x2000006
SIZE:  0x70
IMPLEMENTS: System.Runtime.CompilerServices.IAsyncStateMachine
FIELDS:
  public            System.Int32                    <>1__state  // 0x10
  public            System.Runtime.CompilerServices.AsyncValueTaskMethodBuilder<System.Int32><>t__builder  // 0x18
  public            System.IO.Compression.BrotliStream<>4__this  // 0x38
  public            System.Threading.CancellationTokencancellationToken  // 0x40
  public            System.Memory<System.Byte>      buffer  // 0x48
  private           System.Int32                    <totalWritten>5__2  // 0x58
  private           System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<System.Int32><>u__1  // 0x60
METHODS:
  RVA=0x0AE10564  token=0x6000031  System.Void MoveNext()
  RVA=0x0AE10E5C  token=0x6000032  System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine)
END_CLASS

CLASS: <WriteAsyncMemoryCore>d__52
TYPE:  sealed struct
TOKEN: 0x2000007
SIZE:  0x68
IMPLEMENTS: System.Runtime.CompilerServices.IAsyncStateMachine
FIELDS:
  public            System.Int32                    <>1__state  // 0x10
  public            System.Runtime.CompilerServices.AsyncTaskMethodBuilder<>t__builder  // 0x18
  public            System.IO.Compression.BrotliStream<>4__this  // 0x30
  public            System.ReadOnlyMemory<System.Byte>buffer  // 0x38
  public            System.Threading.CancellationTokencancellationToken  // 0x48
  private           System.Buffers.OperationStatus  <lastResult>5__2  // 0x50
  private           System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<>u__1  // 0x58
METHODS:
  RVA=0x0AE11420  token=0x6000033  System.Void MoveNext()
  RVA=0x0AE119C8  token=0x6000034  System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine)
END_CLASS

CLASS: <FlushAsyncCore>d__55
TYPE:  sealed struct
TOKEN: 0x2000008
SIZE:  0x58
IMPLEMENTS: System.Runtime.CompilerServices.IAsyncStateMachine
FIELDS:
  public            System.Int32                    <>1__state  // 0x10
  public            System.Runtime.CompilerServices.AsyncTaskMethodBuilder<>t__builder  // 0x18
  public            System.IO.Compression.BrotliStream<>4__this  // 0x30
  public            System.Threading.CancellationTokencancellationToken  // 0x38
  private           System.Buffers.OperationStatus  <lastResult>5__2  // 0x40
  private           System.Runtime.CompilerServices.ConfiguredValueTaskAwaitable.ConfiguredValueTaskAwaiter<>u__1  // 0x48
METHODS:
  RVA=0x0AE10EA8  token=0x6000035  System.Void MoveNext()
  RVA=0x0AE113C4  token=0x6000036  System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine)
END_CLASS

CLASS: TaskWrapperAsyncResult
TYPE:  sealed class
TOKEN: 0x200000F
SIZE:  0x28
IMPLEMENTS: System.IAsyncResult
FIELDS:
  private   readonly System.Threading.Tasks.Task     Task  // 0x10
  private   readonly System.Object                   _state  // 0x18
  private   readonly System.Boolean                  _completedSynchronously  // 0x20
PROPERTIES:
  System.IAsyncResult.AsyncState  get=0x0385B100
  System.IAsyncResult.CompletedSynchronously  get=0x04D866B0
  System.IAsyncResult.IsCompleted  get=0x0A88881C
  System.IAsyncResult.AsyncWaitHandle  get=0x0A8887FC
METHODS:
  RVA=0x04D33BE0  token=0x600004D  System.Void .ctor(System.Threading.Tasks.Task task, System.Object state, System.Boolean completedSynchronously)
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x2000010
SIZE:  0x20
FIELDS:
  public            System.AsyncCallback            callback  // 0x10
  public            System.IAsyncResult             asyncResult  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000052  System.Void .ctor()
  RVA=0x0A88CE18  token=0x6000053  System.Void <InvokeCallbackWhenTaskCompletes>b__0()
END_CLASS

CLASS: Microsoft.Win32.SafeHandles.SafeBrotliEncoderHandle
TYPE:  sealed class
TOKEN: 0x2000011
SIZE:  0x20
EXTENDS: System.Runtime.InteropServices.SafeHandle
FIELDS:
PROPERTIES:
  IsInvalid  get=0x03BA32C0
METHODS:
  RVA=0x053948D4  token=0x6000054  System.Void .ctor()
  RVA=0x0AE102A0  token=0x6000055  System.Boolean ReleaseHandle()
END_CLASS

CLASS: Microsoft.Win32.SafeHandles.SafeBrotliDecoderHandle
TYPE:  sealed class
TOKEN: 0x2000012
SIZE:  0x20
EXTENDS: System.Runtime.InteropServices.SafeHandle
FIELDS:
PROPERTIES:
  IsInvalid  get=0x03BA32C0
METHODS:
  RVA=0x053948D4  token=0x6000057  System.Void .ctor()
  RVA=0x04D7AFF0  token=0x6000058  System.Boolean ReleaseHandle()
END_CLASS

CLASS: System.IO.Compression.BrotliStream
TYPE:  sealed class
TOKEN: 0x2000005
SIZE:  0x70
EXTENDS: System.IO.Stream
FIELDS:
  private           System.IO.Stream                _stream  // 0x28
  private   readonly System.Byte[]                   _buffer  // 0x30
  private   readonly System.Boolean                  _leaveOpen  // 0x38
  private   readonly System.IO.Compression.CompressionMode_mode  // 0x3c
  private           System.Int32                    _activeAsyncOperation  // 0x40
  private           System.IO.Compression.BrotliDecoder_decoder  // 0x48
  private           System.Int32                    _bufferOffset  // 0x58
  private           System.Int32                    _bufferCount  // 0x5c
  private           System.IO.Compression.BrotliEncoder_encoder  // 0x60
PROPERTIES:
  CanRead  get=0x0AE0FE44
  CanWrite  get=0x0AE0FE70
  CanSeek  get=0x012081B0
  Length  get=0x0AE0FE9C
  Position  get=0x0AE0FEE8  set=0x0AE0FF34
  AsyncOperationIsActive  get=0x0AE0FE3C
METHODS:
  RVA=0x04A43A50  token=0x600000C  System.Void .ctor(System.IO.Stream stream, System.IO.Compression.CompressionMode mode, System.Boolean leaveOpen)
  RVA=0x030780C0  token=0x600000D  System.Void EnsureNotDisposed()
  RVA=0x04B21780  token=0x600000E  System.Void Dispose(System.Boolean disposing)
  RVA=0x0AE0F600  token=0x600000F  System.Void ValidateParameters(System.Byte[] array, System.Int32 offset, System.Int32 count)
  RVA=0x0AE0F50C  token=0x6000013  System.Int64 Seek(System.Int64 offset, System.IO.SeekOrigin origin)
  RVA=0x0AE0F558  token=0x6000017  System.Void SetLength(System.Int64 value)
  RVA=0x0AE0EDD4  token=0x6000019  System.Void EnsureNoActiveAsyncOperation()
  RVA=0x0AE0EC50  token=0x600001A  System.Void AsyncOperationStarting()
  RVA=0x0AE0EC40  token=0x600001B  System.Void AsyncOperationCompleting()
  RVA=0x0AE0F5A4  token=0x600001C  System.Void ThrowInvalidBeginCall()
  RVA=0x0AE0F46C  token=0x600001D  System.Int32 Read(System.Byte[] buffer, System.Int32 offset, System.Int32 count)
  RVA=0x03077A60  token=0x600001E  System.Int32 Read(System.Span<System.Byte> buffer)
  RVA=0x0AE0EC70  token=0x600001F  System.IAsyncResult BeginRead(System.Byte[] buffer, System.Int32 offset, System.Int32 count, System.AsyncCallback asyncCallback, System.Object asyncState)
  RVA=0x0AE0ED8C  token=0x6000020  System.Int32 EndRead(System.IAsyncResult asyncResult)
  RVA=0x0AE0F38C  token=0x6000021  System.Threading.Tasks.Task<System.Int32> ReadAsync(System.Byte[] buffer, System.Int32 offset, System.Int32 count, System.Threading.CancellationToken cancellationToken)
  RVA=0x0AE0F220  token=0x6000022  System.Threading.Tasks.ValueTask<System.Int32> ReadAsync(System.Memory<System.Byte> buffer, System.Threading.CancellationToken cancellationToken)
  RVA=0x0AE0EDEC  token=0x6000023  System.Threading.Tasks.ValueTask<System.Int32> FinishReadAsyncMemory(System.Memory<System.Byte> buffer, System.Threading.CancellationToken cancellationToken)
  RVA=0x0AE0FDC4  token=0x6000024  System.Void .ctor(System.IO.Stream stream, System.IO.Compression.CompressionLevel compressionLevel)
  RVA=0x0AE0FDE0  token=0x6000025  System.Void .ctor(System.IO.Stream stream, System.IO.Compression.CompressionLevel compressionLevel, System.Boolean leaveOpen)
  RVA=0x0AE0FD04  token=0x6000026  System.Void Write(System.Byte[] buffer, System.Int32 offset, System.Int32 count)
  RVA=0x0AE0FDA0  token=0x6000027  System.Void Write(System.ReadOnlySpan<System.Byte> buffer)
  RVA=0x0AE0FAC8  token=0x6000028  System.Void WriteCore(System.ReadOnlySpan<System.Byte> buffer, System.Boolean isFinalBlock)
  RVA=0x0AE0ED00  token=0x6000029  System.IAsyncResult BeginWrite(System.Byte[] buffer, System.Int32 offset, System.Int32 count, System.AsyncCallback asyncCallback, System.Object asyncState)
  RVA=0x0AE0EDC8  token=0x600002A  System.Void EndWrite(System.IAsyncResult asyncResult)
  RVA=0x0AE0F9E0  token=0x600002B  System.Threading.Tasks.Task WriteAsync(System.Byte[] buffer, System.Int32 offset, System.Int32 count, System.Threading.CancellationToken cancellationToken)
  RVA=0x0AE0F894  token=0x600002C  System.Threading.Tasks.ValueTask WriteAsync(System.ReadOnlyMemory<System.Byte> buffer, System.Threading.CancellationToken cancellationToken)
  RVA=0x0AE0F7A4  token=0x600002D  System.Threading.Tasks.Task WriteAsyncMemoryCore(System.ReadOnlyMemory<System.Byte> buffer, System.Threading.CancellationToken cancellationToken)
  RVA=0x0AE0F07C  token=0x600002E  System.Void Flush()
  RVA=0x0AE0EFC4  token=0x600002F  System.Threading.Tasks.Task FlushAsync(System.Threading.CancellationToken cancellationToken)
  RVA=0x0AE0EEF0  token=0x6000030  System.Threading.Tasks.Task FlushAsyncCore(System.Threading.CancellationToken cancellationToken)
END_CLASS

CLASS: System.IO.Compression.BrotliUtils
TYPE:  static class
TOKEN: 0x2000009
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0AE0FF80  token=0x6000037  System.Int32 GetQualityFromCompressionLevel(System.IO.Compression.CompressionLevel level)
END_CLASS

CLASS: System.IO.Compression.BrotliDecoder
TYPE:  sealed struct
TOKEN: 0x200000A
SIZE:  0x20
IMPLEMENTS: System.IDisposable
FIELDS:
  private           Microsoft.Win32.SafeHandles.SafeBrotliDecoderHandle_state  // 0x10
  private           System.Boolean                  _disposed  // 0x18
METHODS:
  RVA=0x0454C880  token=0x6000038  System.Void InitializeDecoder()
  RVA=0x03077F80  token=0x6000039  System.Void EnsureInitialized()
  RVA=0x040F0480  token=0x600003A  System.Void Dispose()
  RVA=0x03077FB0  token=0x600003B  System.Void EnsureNotDisposed()
  RVA=0x03077CC0  token=0x600003C  System.Buffers.OperationStatus Decompress(System.ReadOnlySpan<System.Byte> source, System.Span<System.Byte> destination, System.Int32& bytesConsumed, System.Int32& bytesWritten)
END_CLASS

CLASS: System.IO.Compression.BrotliEncoder
TYPE:  sealed struct
TOKEN: 0x200000B
SIZE:  0x20
IMPLEMENTS: System.IDisposable
FIELDS:
  private           Microsoft.Win32.SafeHandles.SafeBrotliEncoderHandle_state  // 0x10
  private           System.Boolean                  _disposed  // 0x18
METHODS:
  RVA=0x0AE0EB74  token=0x600003D  System.Void .ctor(System.Int32 quality, System.Int32 window)
  RVA=0x0AE0E730  token=0x600003E  System.Void InitializeEncoder()
  RVA=0x0AE0E580  token=0x600003F  System.Void EnsureInitialized()
  RVA=0x04D7F810  token=0x6000040  System.Void Dispose()
  RVA=0x0AE0E5AC  token=0x6000041  System.Void EnsureNotDisposed()
  RVA=0x0AE0E7CC  token=0x6000042  System.Void SetQuality(System.Int32 quality)
  RVA=0x0AE0E99C  token=0x6000043  System.Void SetWindow(System.Int32 window)
  RVA=0x0AE0E630  token=0x6000044  System.Buffers.OperationStatus Flush(System.Memory<System.Byte> destination, System.Int32& bytesWritten)
  RVA=0x0AE0E6A8  token=0x6000045  System.Buffers.OperationStatus Flush(System.Span<System.Byte> destination, System.Int32& bytesWritten)
  RVA=0x0AE0E464  token=0x6000046  System.Buffers.OperationStatus Compress(System.ReadOnlyMemory<System.Byte> source, System.Memory<System.Byte> destination, System.Int32& bytesConsumed, System.Int32& bytesWritten, System.Boolean isFinalBlock)
  RVA=0x0AE0E534  token=0x6000047  System.Buffers.OperationStatus Compress(System.ReadOnlySpan<System.Byte> source, System.Span<System.Byte> destination, System.Int32& bytesConsumed, System.Int32& bytesWritten, System.Boolean isFinalBlock)
  RVA=0x0AE0E2A8  token=0x6000048  System.Buffers.OperationStatus Compress(System.ReadOnlySpan<System.Byte> source, System.Span<System.Byte> destination, System.Int32& bytesConsumed, System.Int32& bytesWritten, System.IO.Compression.BrotliEncoderOperation operation)
END_CLASS

CLASS: System.IO.Compression.BrotliEncoderOperation
TYPE:  sealed struct
TOKEN: 0x200000C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  System.IO.Compression.BrotliEncoderOperationProcess  // const
  public    static  System.IO.Compression.BrotliEncoderOperationFlush  // const
  public    static  System.IO.Compression.BrotliEncoderOperationFinish  // const
  public    static  System.IO.Compression.BrotliEncoderOperationEmitMetadata  // const
METHODS:
END_CLASS

CLASS: System.IO.Compression.BrotliEncoderParameter
TYPE:  sealed struct
TOKEN: 0x200000D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  System.IO.Compression.BrotliEncoderParameterMode  // const
  public    static  System.IO.Compression.BrotliEncoderParameterQuality  // const
  public    static  System.IO.Compression.BrotliEncoderParameterLGWin  // const
  public    static  System.IO.Compression.BrotliEncoderParameterLGBlock  // const
  public    static  System.IO.Compression.BrotliEncoderParameterLCModeling  // const
  public    static  System.IO.Compression.BrotliEncoderParameterSizeHint  // const
METHODS:
END_CLASS

CLASS: System.Threading.Tasks.TaskToApm
TYPE:  static class
TOKEN: 0x200000E
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0AE102B4  token=0x6000049  System.IAsyncResult Begin(System.Threading.Tasks.Task task, System.AsyncCallback callback, System.Object state)
  RVA=0x0AE103B0  token=0x600004A  System.Void End(System.IAsyncResult asyncResult)
  RVA=-1  // generic def  token=0x600004B  TResult End(System.IAsyncResult asyncResult)
  RVA=0x0AE1046C  token=0x600004C  System.Void InvokeCallbackWhenTaskCompletes(System.Threading.Tasks.Task antecedent, System.AsyncCallback callback, System.IAsyncResult asyncResult)
END_CLASS

