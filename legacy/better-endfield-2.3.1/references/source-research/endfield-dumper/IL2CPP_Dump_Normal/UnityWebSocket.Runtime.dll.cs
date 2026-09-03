// ========================================================
// Dumped by @desirepro
// Assembly: UnityWebSocket.Runtime.dll
// Classes:  16
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x200000C  // size: 0x20
    public class SendBuffer
    {
        // Fields
        public System.Byte[] data;  // 0x10
        public System.Net.WebSockets.WebSocketMessageType type;  // 0x18

        // Methods
        // RVA: 0x03B46FF0  token: 0x6000052
        public System.Void .ctor(System.Byte[] data, System.Net.WebSockets.WebSocketMessageType type) { }

    }

    // TypeToken: 0x200000D  // size: 0x40
    public sealed struct <ConnectTask>d__36 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncTaskMethodBuilder <>t__builder;  // 0x18
        public UnityWebSocket.WebSocket <>4__this;  // 0x30
        private System.Runtime.CompilerServices.TaskAwaiter <>u__1;  // 0x38

        // Methods
        // RVA: 0x0B591938  token: 0x6000053
        private virtual System.Void MoveNext() { }
        // RVA: 0x0B591D04  token: 0x6000054
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x200000E  // size: 0x40
    public sealed struct <SendTask>d__42 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncTaskMethodBuilder <>t__builder;  // 0x18
        public UnityWebSocket.WebSocket <>4__this;  // 0x30
        private System.Runtime.CompilerServices.TaskAwaiter <>u__1;  // 0x38

        // Methods
        // RVA: 0x0B592760  token: 0x6000055
        private virtual System.Void MoveNext() { }
        // RVA: 0x0B592E38  token: 0x6000056
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

    // TypeToken: 0x200000F  // size: 0x68
    public sealed struct <ReceiveTask>d__43 : System.Runtime.CompilerServices.IAsyncStateMachine
    {
        // Fields
        public System.Int32 <>1__state;  // 0x10
        public System.Runtime.CompilerServices.AsyncTaskMethodBuilder <>t__builder;  // 0x18
        public UnityWebSocket.WebSocket <>4__this;  // 0x30
        private System.String <closeReason>5__2;  // 0x38
        private System.UInt16 <closeCode>5__3;  // 0x40
        private System.Boolean <isClosed>5__4;  // 0x42
        private System.ArraySegment<System.Byte> <segment>5__5;  // 0x48
        private System.IO.MemoryStream <ms>5__6;  // 0x58
        private System.Runtime.CompilerServices.TaskAwaiter<System.Net.WebSockets.WebSocketReceiveResult> <>u__1;  // 0x60

        // Methods
        // RVA: 0x0B591D60  token: 0x6000057
        private virtual System.Void MoveNext() { }
        // RVA: 0x0B592704  token: 0x6000058
        private virtual System.Void SetStateMachine(System.Runtime.CompilerServices.IAsyncStateMachine stateMachine) { }

    }

namespace UnityWebSocket
{

    // TypeToken: 0x2000002  // size: 0x28
    public class CloseEventArgs : System.EventArgs
    {
        // Fields
        private System.UInt16 <Code>k__BackingField;  // 0x10
        private System.String <Reason>k__BackingField;  // 0x18
        private System.Boolean <WasClean>k__BackingField;  // 0x20

        // Properties
        System.UInt16 Code { get; /* RVA: 0x04D96190 */ set; /* RVA: 0x04D94F60 */ }
        System.String Reason { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.Boolean WasClean { get; /* RVA: 0x04D866B0 */ set; /* RVA: 0x04D866D0 */ }
        UnityWebSocket.CloseStatusCode StatusCode { get; /* RVA: 0x0B5915F8 */ }

        // Methods
        // RVA: 0x0B5915B8  token: 0x6000001
        private System.Void .ctor() { }
        // RVA: 0x0B5915A4  token: 0x6000002
        private System.Void .ctor(System.UInt16 code) { }
        // RVA: 0x0B5915A4  token: 0x6000003
        private System.Void .ctor(UnityWebSocket.CloseStatusCode code) { }
        // RVA: 0x0B5915B0  token: 0x6000004
        private System.Void .ctor(UnityWebSocket.CloseStatusCode code, System.String reason) { }
        // RVA: 0x0B591544  token: 0x6000005
        private System.Void .ctor(System.UInt16 code, System.String reason) { }

    }

    // TypeToken: 0x2000003  // size: 0x12
    public sealed struct CloseStatusCode
    {
        // Fields
        public System.UInt16 value__;  // 0x10
        public static UnityWebSocket.CloseStatusCode Unknown;  // const
        public static UnityWebSocket.CloseStatusCode Normal;  // const
        public static UnityWebSocket.CloseStatusCode Away;  // const
        public static UnityWebSocket.CloseStatusCode ProtocolError;  // const
        public static UnityWebSocket.CloseStatusCode UnsupportedData;  // const
        public static UnityWebSocket.CloseStatusCode Undefined;  // const
        public static UnityWebSocket.CloseStatusCode NoStatus;  // const
        public static UnityWebSocket.CloseStatusCode Abnormal;  // const
        public static UnityWebSocket.CloseStatusCode InvalidData;  // const
        public static UnityWebSocket.CloseStatusCode PolicyViolation;  // const
        public static UnityWebSocket.CloseStatusCode TooBig;  // const
        public static UnityWebSocket.CloseStatusCode MandatoryExtension;  // const
        public static UnityWebSocket.CloseStatusCode ServerError;  // const
        public static UnityWebSocket.CloseStatusCode TlsHandshakeFailure;  // const

    }

    // TypeToken: 0x2000004  // size: 0x20
    public class ErrorEventArgs : System.EventArgs
    {
        // Fields
        private System.Exception <Exception>k__BackingField;  // 0x10
        private System.String <Message>k__BackingField;  // 0x18

        // Properties
        System.Exception Exception { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        System.String Message { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }

        // Methods
        // RVA: 0x0B59172C  token: 0x600000D
        private System.Void .ctor(System.String message) { }
        // RVA: 0x0B5916C0  token: 0x600000E
        private System.Void .ctor(System.String message, System.Exception exception) { }

    }

    // TypeToken: 0x2000005
    public interface IWebSocket
    {
        // Properties
        System.String Address { get; /* RVA: -1  // abstract */ }
        System.String[] SubProtocols { get; /* RVA: -1  // abstract */ }
        UnityWebSocket.WebSocketState ReadyState { get; /* RVA: -1  // abstract */ }
        System.String BinaryType { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }

        // Events
        event System.EventHandler<UnityWebSocket.OpenEventArgs> OnOpen;
        event System.EventHandler<UnityWebSocket.CloseEventArgs> OnClose;
        event System.EventHandler<UnityWebSocket.ErrorEventArgs> OnError;
        event System.EventHandler<UnityWebSocket.MessageEventArgs> OnMessage;

        // Methods
        // RVA: -1  // abstract  token: 0x6000013
        public virtual System.Void ConnectAsync() { }
        // RVA: -1  // abstract  token: 0x6000014
        public virtual System.Void CloseAsync() { }
        // RVA: -1  // abstract  token: 0x6000015
        public virtual System.Void SendAsync(System.Byte[] data) { }
        // RVA: -1  // abstract  token: 0x6000016
        public virtual System.Void SendAsync(System.String text) { }

    }

    // TypeToken: 0x2000006  // size: 0x28
    public class MessageEventArgs : System.EventArgs
    {
        // Fields
        private System.Byte[] _rawData;  // 0x10
        private System.String _data;  // 0x18
        private UnityWebSocket.Opcode <Opcode>k__BackingField;  // 0x20

        // Properties
        UnityWebSocket.Opcode Opcode { get; /* RVA: 0x04D866B0 */ set; /* RVA: 0x04D866D0 */ }
        System.String Data { get; /* RVA: 0x0B5918B0 */ }
        System.Byte[] RawData { get; /* RVA: 0x0B5918DC */ }
        System.Boolean IsBinary { get; /* RVA: 0x0B5918CC */ }
        System.Boolean IsText { get; /* RVA: 0x0B5918D4 */ }

        // Methods
        // RVA: 0x0B5917F0  token: 0x6000024
        private System.Void .ctor(UnityWebSocket.Opcode opcode, System.Byte[] rawData) { }
        // RVA: 0x0B591850  token: 0x6000025
        private System.Void .ctor(UnityWebSocket.Opcode opcode, System.String data) { }
        // RVA: 0x0B591738  token: 0x600002C
        private System.Void SetData() { }
        // RVA: 0x0B5917A0  token: 0x600002D
        private System.Void SetRawData() { }

    }

    // TypeToken: 0x2000007  // size: 0x11
    public sealed struct Opcode
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static UnityWebSocket.Opcode Text;  // const
        public static UnityWebSocket.Opcode Binary;  // const
        public static UnityWebSocket.Opcode Close;  // const

    }

    // TypeToken: 0x2000008  // size: 0x10
    public class OpenEventArgs : System.EventArgs
    {
        // Methods
        // RVA: 0x0B5918F8  token: 0x600002E
        private System.Void .ctor() { }

    }

    // TypeToken: 0x2000009  // size: 0x10
    public static class Settings
    {
        // Fields
        public static System.String GITHUB;  // const
        public static System.String QQ_GROUP;  // const
        public static System.String QQ_GROUP_LINK;  // const
        public static System.String EMAIL;  // const
        public static System.String AUHTOR;  // const
        public static System.String VERSION;  // const

    }

    // TypeToken: 0x200000A  // size: 0x12
    public sealed struct WebSocketState
    {
        // Fields
        public System.UInt16 value__;  // 0x10
        public static UnityWebSocket.WebSocketState Connecting;  // const
        public static UnityWebSocket.WebSocketState Open;  // const
        public static UnityWebSocket.WebSocketState Closing;  // const
        public static UnityWebSocket.WebSocketState Closed;  // const

    }

    // TypeToken: 0x200000B  // size: 0x78
    public class WebSocket : UnityWebSocket.IWebSocket
    {
        // Fields
        private System.String <Address>k__BackingField;  // 0x10
        private System.String[] <SubProtocols>k__BackingField;  // 0x18
        private System.String <BinaryType>k__BackingField;  // 0x20
        private System.EventHandler<UnityWebSocket.OpenEventArgs> OnOpen;  // 0x28
        private System.EventHandler<UnityWebSocket.CloseEventArgs> OnClose;  // 0x30
        private System.EventHandler<UnityWebSocket.ErrorEventArgs> OnError;  // 0x38
        private System.EventHandler<UnityWebSocket.MessageEventArgs> OnMessage;  // 0x40
        private System.Net.WebSockets.ClientWebSocket socket;  // 0x48
        private System.Object sendQueueLock;  // 0x50
        private System.Collections.Generic.Queue<UnityWebSocket.WebSocket.SendBuffer> sendQueue;  // 0x58
        private System.Boolean isSendTaskRunning;  // 0x60
        private readonly System.Collections.Generic.Queue<System.EventArgs> eventQueue;  // 0x68
        private readonly System.Object eventQueueLock;  // 0x70

        // Properties
        System.String Address { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        System.String[] SubProtocols { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        UnityWebSocket.WebSocketState ReadyState { get; /* RVA: 0x0B594828 */ }
        System.String BinaryType { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        System.Boolean isOpening { get; /* RVA: 0x0B594880 */ }

        // Events
        event System.EventHandler<UnityWebSocket.OpenEventArgs> OnOpen;
        event System.EventHandler<UnityWebSocket.CloseEventArgs> OnClose;
        event System.EventHandler<UnityWebSocket.ErrorEventArgs> OnError;
        event System.EventHandler<UnityWebSocket.MessageEventArgs> OnMessage;

        // Methods
        // RVA: 0x0B5944B0  token: 0x600003F
        public System.Void .ctor(System.String address) { }
        // RVA: 0x0B5941B4  token: 0x6000040
        public System.Void .ctor(System.String address, System.String subProtocol) { }
        // RVA: 0x0B59434C  token: 0x6000041
        public System.Void .ctor(System.String address, System.String[] subProtocols) { }
        // RVA: 0x0B593354  token: 0x6000042
        public virtual System.Void ConnectAsync() { }
        // RVA: 0x0B5932D8  token: 0x6000043
        public virtual System.Void CloseAsync() { }
        // RVA: 0x0B593B0C  token: 0x6000044
        public virtual System.Void SendAsync(System.Byte[] data) { }
        // RVA: 0x0B593B98  token: 0x6000045
        public virtual System.Void SendAsync(System.String text) { }
        // RVA: 0x0B5934F4  token: 0x6000046
        private System.Threading.Tasks.Task ConnectTask() { }
        // RVA: 0x0B593C40  token: 0x6000047
        private System.Void SendBufferAsync(UnityWebSocket.WebSocket.SendBuffer buffer) { }
        // RVA: 0x0B593E20  token: 0x6000048
        private System.Threading.Tasks.Task SendTask() { }
        // RVA: 0x0B593A50  token: 0x6000049
        private System.Threading.Tasks.Task ReceiveTask() { }
        // RVA: 0x0B593ED0  token: 0x600004A
        private System.Void SocketDispose() { }
        // RVA: 0x0B59382C  token: 0x600004B
        private System.Void HandleOpen() { }
        // RVA: 0x0B5937A4  token: 0x600004C
        private System.Void HandleMessage(UnityWebSocket.Opcode opcode, System.Byte[] rawData) { }
        // RVA: 0x0B5935A4  token: 0x600004D
        private System.Void HandleClose(System.UInt16 code, System.String reason) { }
        // RVA: 0x0B59362C  token: 0x600004E
        private System.Void HandleError(System.Exception exception) { }
        // RVA: 0x0B5936BC  token: 0x600004F
        private System.Void HandleEventSync(System.EventArgs eventArgs) { }
        // RVA: 0x0B593F38  token: 0x6000050
        private System.Void Update() { }
        // RVA: 0x0B593894  token: 0x6000051
        private static System.Void Log(System.String msg) { }

    }

    // TypeToken: 0x2000010  // size: 0x20
    public class WebSocketManager : UnityEngine.MonoBehaviour
    {
        // Fields
        private static System.String rootName;  // const
        private static UnityWebSocket.WebSocketManager _instance;  // static @ 0x0
        private readonly System.Collections.Generic.List<UnityWebSocket.WebSocket> sockets;  // 0x18

        // Properties
        UnityWebSocket.WebSocketManager Instance { get; /* RVA: 0x0B593260 */ }

        // Methods
        // RVA: 0x0B592F14  token: 0x600005A
        private System.Void Awake() { }
        // RVA: 0x0B592F64  token: 0x600005B
        public static System.Void CreateInstance() { }
        // RVA: 0x0B592E94  token: 0x600005C
        public System.Void Add(UnityWebSocket.WebSocket socket) { }
        // RVA: 0x0B5930D8  token: 0x600005D
        public System.Void Remove(UnityWebSocket.WebSocket socket) { }
        // RVA: 0x0B593158  token: 0x600005E
        private System.Void Update() { }
        // RVA: 0x0B5931E0  token: 0x600005F
        public System.Void .ctor() { }

    }

}

