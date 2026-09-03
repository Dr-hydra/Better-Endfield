// ========================================================
// Dumped by @desirepro
// Assembly: ACESDK_WIN.dll
// Classes:  30
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000007  // size: 0x80
    public sealed class InitAceClient5Routine : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0539B034  token: 0x6000012
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x014F3120  token: 0x6000013
        public virtual AceSdk.AceSdkResult Invoke(System.UInt32 ver, System.IntPtr opt, System.IntPtr& client_obj_ptr) { }
        // RVA: 0x0539AF88  token: 0x6000014
        public virtual System.IAsyncResult BeginInvoke(System.UInt32 ver, System.IntPtr opt, System.IntPtr& client_obj_ptr, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x0539ADF8  token: 0x6000015
        public virtual AceSdk.AceSdkResult EndInvoke(System.IntPtr& client_obj_ptr, System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000008  // size: 0x80
    public sealed class LogInRoutine : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0539B1B8  token: 0x6000016
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x014D3FD0  token: 0x6000017
        public virtual AceSdk.AceSdkResult Invoke(System.IntPtr handle, System.String account, System.UInt32 account_type, System.UInt32 world_id, System.String login_ticket) { }
        // RVA: 0x0539B0FC  token: 0x6000018
        public virtual System.IAsyncResult BeginInvoke(System.IntPtr handle, System.String account, System.UInt32 account_type, System.UInt32 world_id, System.String login_ticket, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x0539AB64  token: 0x6000019
        public virtual AceSdk.AceSdkResult EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000009  // size: 0x80
    public sealed class LogOutRoutine : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0539AC94  token: 0x600001A
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x014E2060  token: 0x600001B
        public virtual AceSdk.AceSdkResult Invoke(System.IntPtr handle) { }
        // RVA: 0x0539B280  token: 0x600001C
        public virtual System.IAsyncResult BeginInvoke(System.IntPtr handle, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x0539AB64  token: 0x600001D
        public virtual AceSdk.AceSdkResult EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x200000A  // size: 0x80
    public sealed class DeinitRoutine : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0539AC94  token: 0x600001E
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x014E2060  token: 0x600001F
        public virtual AceSdk.AceSdkResult Invoke(System.IntPtr handle) { }
        // RVA: 0x0539AAEC  token: 0x6000020
        public virtual System.IAsyncResult BeginInvoke(System.IntPtr handle, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x0539AB64  token: 0x6000021
        public virtual AceSdk.AceSdkResult EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x200000B  // size: 0x80
    public sealed class GetPacketRoutine : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0539AE24  token: 0x6000022
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x015802E0  token: 0x6000023
        public virtual AceSdk.AceSdkResult Invoke(System.IntPtr handle, AceSdk.AceSdkClient.AceSdkClientPacket& packet) { }
        // RVA: 0x0539AEEC  token: 0x6000024
        public virtual System.IAsyncResult BeginInvoke(System.IntPtr handle, AceSdk.AceSdkClient.AceSdkClientPacket& packet, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x0539ADF8  token: 0x6000025
        public virtual AceSdk.AceSdkResult EndInvoke(AceSdk.AceSdkClient.AceSdkClientPacket& packet, System.IAsyncResult result) { }

    }

    // TypeToken: 0x200000C  // size: 0x80
    public sealed class OnPacketReceivedRoutine : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0539B430  token: 0x6000026
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x014F3120  token: 0x6000027
        public virtual AceSdk.AceSdkResult Invoke(System.IntPtr handle, System.Byte[] data, System.UInt32 len) { }
        // RVA: 0x0539B394  token: 0x6000028
        public virtual System.IAsyncResult BeginInvoke(System.IntPtr handle, System.Byte[] data, System.UInt32 len, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x0539AB64  token: 0x6000029
        public virtual AceSdk.AceSdkResult EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x200000D  // size: 0x80
    public sealed class GetLightFeaturePacketRoutine : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0539AE24  token: 0x600002A
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x015802E0  token: 0x600002B
        public virtual AceSdk.AceSdkResult Invoke(System.IntPtr handle, AceSdk.AceSdkClient.AceSdkClientLightFeaturePacket& packet) { }
        // RVA: 0x0539AD5C  token: 0x600002C
        public virtual System.IAsyncResult BeginInvoke(System.IntPtr handle, AceSdk.AceSdkClient.AceSdkClientLightFeaturePacket& packet, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x0539ADF8  token: 0x600002D
        public virtual AceSdk.AceSdkResult EndInvoke(AceSdk.AceSdkClient.AceSdkClientLightFeaturePacket& packet, System.IAsyncResult result) { }

    }

    // TypeToken: 0x200000E  // size: 0x80
    public sealed class OnLightFeaturePacketReceivedRoutine : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0539AE24  token: 0x600002E
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x015802E0  token: 0x600002F
        public virtual AceSdk.AceSdkResult Invoke(System.IntPtr handle, AceSdk.AceSdkClient.AceSdkClientLightFeature& data) { }
        // RVA: 0x0539B2F8  token: 0x6000030
        public virtual System.IAsyncResult BeginInvoke(System.IntPtr handle, AceSdk.AceSdkClient.AceSdkClientLightFeature& data, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x0539ADF8  token: 0x6000031
        public virtual AceSdk.AceSdkResult EndInvoke(AceSdk.AceSdkClient.AceSdkClientLightFeature& data, System.IAsyncResult result) { }

    }

    // TypeToken: 0x200000F  // size: 0x58
    public sealed struct AceSdkClientObject
    {
        // Fields
        public System.IntPtr handle_;  // 0x10
        public System.IntPtr _deprecated1;  // 0x18
        public AceSdk.AceSdkClient.LogOutRoutine log_out_;  // 0x20
        public AceSdk.AceSdkClient.DeinitRoutine deinit_;  // 0x28
        public AceSdk.AceSdkClient.GetPacketRoutine get_packet_;  // 0x30
        public AceSdk.AceSdkClient.OnPacketReceivedRoutine on_packet_received_;  // 0x38
        public AceSdk.AceSdkClient.LogInRoutine log_in_;  // 0x40
        public AceSdk.AceSdkClient.GetLightFeaturePacketRoutine get_light_feature_packet_;  // 0x48
        public AceSdk.AceSdkClient.OnLightFeaturePacketReceivedRoutine on_light_feature_packet_received_;  // 0x50

    }

    // TypeToken: 0x2000010  // size: 0x20
    public sealed struct AceSdkClientPacket
    {
        // Fields
        public System.Byte[] buffer_;  // 0x10
        public System.UInt32 len_;  // 0x18

    }

    // TypeToken: 0x2000011  // size: 0x20
    public sealed struct AceSdkClientLightFeaturePacket
    {
        // Fields
        public System.Byte[] buffer_;  // 0x10
        public System.UInt32 len_;  // 0x18

    }

    // TypeToken: 0x2000012  // size: 0x28
    public sealed struct AceSdkClientLightFeature
    {
        // Fields
        public System.String name_;  // 0x10
        public System.IntPtr data_;  // 0x18
        public System.UInt32 data_len_;  // 0x20
        public System.UInt32 crc_;  // 0x24

    }

    // TypeToken: 0x200001C  // size: 0x1E
    public sealed struct __StaticArrayInitTypeSize=14
    {
    }

    // TypeToken: 0x200001D  // size: 0x31
    public sealed struct __StaticArrayInitTypeSize=33
    {
    }

    // TypeToken: 0x200001E  // size: 0x36
    public sealed struct __StaticArrayInitTypeSize=38
    {
    }

    // TypeToken: 0x200001B  // size: 0x10
    public sealed class <PrivateImplementationDetails>
    {
        // Fields
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=33 069D1FC04DF5D3A97841869D60961B287D2BF035BAE96ED2B8B31CDC3A9084AF;  // static @ 0x0
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=14 A79B64018AA2094EC519F1C00795ED3CD8C3F6B942D7CA802FF2B45B51A55B9C;  // static @ 0x21
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=38 EF03B53ABC241FB166156A9BAB8EB62CCE1FD43274D0BAF46A38C9FED67CCB00;  // static @ 0x2f

    }

namespace AceSdk
{

    // TypeToken: 0x2000003  // size: 0x14
    public sealed struct AceSdkAccountType
    {
        // Fields
        public System.UInt32 value__;  // 0x10
        public static AceSdk.AceSdkAccountType ACE_SDK_ACCOUNT_TYPE_QQ;  // const
        public static AceSdk.AceSdkAccountType ACE_SDK_ACCOUNT_TYPE_WECHAT;  // const
        public static AceSdk.AceSdkAccountType ACE_SDK_ACCOUNT_TYPE_BAIDU;  // const
        public static AceSdk.AceSdkAccountType ACE_SDK_ACCOUNT_TYPE_QQ_OPENID;  // const
        public static AceSdk.AceSdkAccountType ACE_SDK_ACCOUNT_TYPE_VISITOR;  // const
        public static AceSdk.AceSdkAccountType ACE_SDK_ACCOUNT_TYPE_GOPENID;  // const
        public static AceSdk.AceSdkAccountType ACE_SDK_ACCOUNT_TYPE_GOOGLE;  // const
        public static AceSdk.AceSdkAccountType ACE_SDK_ACCOUNT_TYPE_WEGAME_MAIL;  // const
        public static AceSdk.AceSdkAccountType ACE_SDK_ACCOUNT_TYPE_4399;  // const
        public static AceSdk.AceSdkAccountType ACE_SDK_ACCOUNT_TYPE_GARENA;  // const
        public static AceSdk.AceSdkAccountType ACE_SDK_ACCOUNT_TYPE_GIANT_NETWORK;  // const
        public static AceSdk.AceSdkAccountType ACE_SDK_ACCOUNT_TYPE_WEGAME;  // const
        public static AceSdk.AceSdkAccountType ACE_SDK_ACCOUNT_TYPE_STEAM;  // const
        public static AceSdk.AceSdkAccountType ACE_SDK_ACCOUNT_TYPE_WEGAME_COMMON_ID;  // const
        public static AceSdk.AceSdkAccountType ACE_SDK_ACCOUNT_TYPE_WOODUAN;  // const
        public static AceSdk.AceSdkAccountType ACE_SDK_ACCOUNT_TYPE_VK;  // const
        public static AceSdk.AceSdkAccountType ACE_SDK_ACCOUNT_TYPE_COMMON_ID;  // const
        public static AceSdk.AceSdkAccountType ACE_SDK_ACCOUNT_TYPE_FACEBOOK;  // const
        public static AceSdk.AceSdkAccountType ACE_SDK_ACCOUNT_TYPE_SUPERCELL;  // const
        public static AceSdk.AceSdkAccountType ACE_SDK_ACCOUNT_TYPE_TCOMMIC;  // const
        public static AceSdk.AceSdkAccountType ACE_SDK_ACCOUNT_TYPE_UPLAY;  // const
        public static AceSdk.AceSdkAccountType ACE_SDK_ACCOUNT_TYPE_PHONE_OPENID;  // const
        public static AceSdk.AceSdkAccountType ACE_SDK_ACCOUNT_TYPE_LINE;  // const
        public static AceSdk.AceSdkAccountType ACE_SDK_ACCOUNT_TYPE_APPLE;  // const

    }

    // TypeToken: 0x2000004  // size: 0x14
    public sealed struct AceSdkAccountPlatID
    {
        // Fields
        public System.UInt32 value__;  // 0x10
        public static AceSdk.AceSdkAccountPlatID ACE_SDK_PLAT_ID_IOS;  // const
        public static AceSdk.AceSdkAccountPlatID ACE_SDK_PLAT_ID_ANDROID;  // const
        public static AceSdk.AceSdkAccountPlatID ACE_SDK_PLAT_ID_PC_CLIENT;  // const
        public static AceSdk.AceSdkAccountPlatID ACE_SDK_PLAT_ID_LINUX;  // const
        public static AceSdk.AceSdkAccountPlatID ACE_SDK_PLAT_ID_SWITCH;  // const
        public static AceSdk.AceSdkAccountPlatID ACE_SDK_PLAT_ID_MAC;  // const
        public static AceSdk.AceSdkAccountPlatID ACE_SDK_PLAT_ID_PS_CLIENT;  // const
        public static AceSdk.AceSdkAccountPlatID ACE_SDK_PLAT_ID_XBOX_CLIENT;  // const
        public static AceSdk.AceSdkAccountPlatID ACE_SDK_PLAT_ID_PC_EMULATOR_CLIENT;  // const
        public static AceSdk.AceSdkAccountPlatID ACE_SDK_PLAT_ID_HARMONY_MOBILE;  // const
        public static AceSdk.AceSdkAccountPlatID ACE_SDK_PLAT_ID_HARMONY_PC;  // const
        public static AceSdk.AceSdkAccountPlatID ACE_SDK_PLAT_ID_UNKNOWN;  // const

    }

    // TypeToken: 0x2000005  // size: 0x28
    public class AceSdkAccount
    {
        // Fields
        public static System.Int32 ACE_SDK_MAX_ACCOUNT_LEN;  // const
        public System.Text.StringBuilder account_;  // 0x10
        public AceSdk.AceSdkAccountType account_type_;  // 0x18
        public AceSdk.AceSdkAccountPlatID plat_id_;  // 0x1c
        public System.UInt32 game_id_;  // 0x20
        public System.UInt32 world_id_;  // 0x24

        // Methods
        // RVA: 0x041E1670  token: 0x6000002
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000006  // size: 0x68
    public class AceSdkClient
    {
        // Fields
        private AceSdk.AceSdkClient.AceSdkClientObject client_obj_;  // 0x10
        private System.Byte[] get_packet_buffer_;  // 0x58
        private System.Byte[] get_light_feature_packet_buffer_;  // 0x60

        // Methods
        // RVA: 0x0539A5CC  token: 0x6000003
        public static AceSdk.AceSdkResult ace_sdk_client_init(AceSdk.AceSdkClient& ace_client) { }
        // RVA: 0x0539A3C8  token: 0x6000004
        public AceSdk.AceSdkResult ace_sdk_client_deinit() { }
        // RVA: 0x0539A844  token: 0x6000005
        public AceSdk.AceSdkResult ace_sdk_client_log_in(System.String account_id, AceSdk.AceSdkAccountType account_type, System.UInt32 world_id, System.String login_ticket) { }
        // RVA: 0x0539A88C  token: 0x6000006
        public AceSdk.AceSdkResult ace_sdk_client_log_out() { }
        // RVA: 0x0539A4E0  token: 0x6000007
        public AceSdk.AceSdkResult ace_sdk_client_get_packet(System.Byte[]& packet) { }
        // RVA: 0x0539A9A4  token: 0x6000008
        public AceSdk.AceSdkResult ace_sdk_client_on_packet_received(System.Byte[] packet) { }
        // RVA: 0x0539A3F4  token: 0x6000009
        public AceSdk.AceSdkResult ace_sdk_client_get_light_feature_packet(System.Byte[]& packet) { }
        // RVA: 0x0539A8B8  token: 0x600000A
        public AceSdk.AceSdkResult ace_sdk_client_on_light_feature_packet_received(System.Byte[] lf_data, System.String name, System.UInt32 crc) { }
        // RVA: 0x0539A2D0  token: 0x600000B
        private System.Void .ctor(System.IntPtr client_obj_ptr) { }
        // RVA: 0x0539A9E0  token: 0x600000C
        private static System.Int32 get_exe_module_path(System.Text.StringBuilder path, System.Int32 len) { }
        // RVA: 0x05399FD4  token: 0x600000D
        private static System.String DecryptString(System.Byte[] encrypted, System.Byte key) { }
        // RVA: 0x0539A258  token: 0x600000E
        private static System.IntPtr LoadLibrary(System.String lpLibFileName) { }
        // RVA: 0x0539A1AC  token: 0x600000F
        private static System.IntPtr GetProcAddress(System.IntPtr hModule, System.String lpProcName) { }
        // RVA: 0x0539A130  token: 0x6000010
        private static System.IntPtr GetProcAddressByOrdinal(System.IntPtr hModule, System.Int32 nOrdinal) { }
        // RVA: 0x0539A068  token: 0x6000011
        private static System.UInt32 GetModuleFileName(System.IntPtr hModule, System.Text.StringBuilder lpFilename, System.Int32 nSize) { }

    }

    // TypeToken: 0x2000013  // size: 0x14
    public sealed struct AceSdkResult
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_OK;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_NOT_SUPPORTED;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_INVALID_PARAMETER;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_CALC_PATH_FAILED;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_LOAD_LIBRARY_FAILED;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_UNLOAD_LIBRARY_FAILED;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_SYMBOL_NOT_FOUND;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_INIT_NOT_CALLED_OR_FAILED;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_INTERNAL_ERROR;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_ACCOUNT_TOO_LONG;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_ACCOUNT_ILLEGAL_CHARACTER;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_NO_PACKET_NEED_SENDING;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_ILLEGAL_LOG_IN;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_COMMON_END;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_DEVELOP_LANGUAGE_BEGIN;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_PACK_STRUCT_1BYTE_ERROR;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_UNPACK_STRUCT_1BYTE_ERROR;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_PACK_STRUCT_1BYTE_LEN_ERROR;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESLUT_JAVA_JNI_BEGIN;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESLUT_JAVA_JNI_ENV_NULL;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESLUT_JAVA_JNI_JVM_NULL;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESLUT_JAVA_JNI_JOBJECT_NULL;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESLUT_JAVA_JNI_CLASS_NULL;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESLUT_JAVA_JNI_REFLECT_BS_DATA_FAILED;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESLUT_JAVA_JNI_REFLECT_BS_OFFSET_FAILED;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESLUT_JAVA_JNI_REFLECT_BS_CAPACITY_FAILED;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESLUT_JAVA_JNI_REFLECT_BS_LENGTH_ERROR;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESLUT_JAVA_ARRAY_LENTGH_ERROR;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESLUT_JAVA_JNI_END;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_DEVELOP_LANGUAGE_END;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_BUSI_INTERFACE_BEGIN;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESLUT_LIGHT_FEATURE_BEGIN;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESLUT_LIGHT_FEATURE_NOT_OPENED;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESLUT_LIGHT_FEATURE_END;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESLUT_UIC_BEGIN;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_UIC_TEXT_INTERF_CALL_FAILED;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_UIC_TEXT_CALLBACK_HAS_NOT_SET;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_UIC_PIC_INTERF_CALL_FAILED;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_UIC_PIC_CALLBACK_HAS_NOT_SET;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESLUT_UIC_END;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_SAFEMODE_BEGIN;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_SAFEMODE_CALLBACK_HAS_NOT_SET;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_SAFEMODE_INTERF_CALL_FAILED;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_SAFEMODE_END;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_GAME_SESSION_BEGIN;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_GAME_SESSION_INTERF_CALL_FAILED;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_GAME_SESSION_END;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_BUSI_INTERFACE_END;  // const
        public static AceSdk.AceSdkResult ACE_SDK_RESULT_MAX_INT32;  // const

    }

    // TypeToken: 0x2000014  // size: 0x10
    public class AceSdkTool
    {
        // Methods
        // RVA: 0x0539AA60  token: 0x6000032
        public static System.String get_string_from_bytearray_until_zero(System.Byte[] input_bytes) { }
        // RVA: 0x041E1670  token: 0x6000033
        public System.Void .ctor() { }

    }

}

namespace Beyond.AceSdk
{

    // TypeToken: 0x200001A  // size: 0x40
    public class AceSDKInterfaceWin
    {
        // Fields
        private static Beyond.AceSdk.AceSDKInterfaceWin instance;  // static @ 0x0
        public AceSdk.AceSdkClient ace_client;  // 0x10
        private System.Threading.Thread m_netThread;  // 0x18
        private System.Boolean b_stopThread;  // 0x20
        private System.String s_LoginAccount;  // 0x28
        private System.Collections.Generic.HashSet<System.String> login_ids;  // 0x30
        private System.Boolean isGamePaused;  // 0x38

        // Properties
        Beyond.AceSdk.AceSDKInterfaceWin Instance { get; /* RVA: 0x05399F44 */ }

        // Methods
        // RVA: 0x05399E14  token: 0x600003A
        public System.Void .ctor() { }
        // RVA: 0x053999CC  token: 0x600003B
        protected virtual System.Void Finalize() { }
        // RVA: 0x05399A98  token: 0x600003C
        public System.Void InitACESDK() { }
        // RVA: 0x05399964  token: 0x600003D
        public System.Void DeinitACESDK() { }
        // RVA: 0x011F2A80  token: 0x600003E
        public System.Void OnApplicationPause(System.Boolean pause) { }
        // RVA: 0x053997C0  token: 0x600003F
        public System.Void AceSdkClientLogIn(System.String account_id, System.String ticket, System.UInt32 world_id) { }
        // RVA: 0x053998E8  token: 0x6000040
        public System.Void AceSdkClientLogOut() { }
        // RVA: 0x05399CEC  token: 0x6000041
        public System.Void StartNetThread() { }
        // RVA: 0x05399B10  token: 0x6000042
        private System.Void NetworkThread(AceSdk.AceSdkClient ace_client) { }
        // RVA: 0x05399C1C  token: 0x6000043
        private static System.Void SendPktToGameServer(System.Byte[] pkg) { }
        // RVA: 0x05399C08  token: 0x6000044
        public System.Void OnAceSdkPacketReceived(System.Byte[] pkt) { }
        // RVA: 0x05399E08  token: 0x6000045
        private System.Void <StartNetThread>b__16_0() { }

    }

}

namespace Beyond.SourceGenerator
{

    // TypeToken: 0x2000015  // size: 0x18
    public class ECSComponentAttribute : System.Attribute
    {
        // Fields
        public System.Runtime.InteropServices.LayoutKind layoutKind;  // 0x10
        public System.Boolean isTag;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x6000034
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000016  // size: 0x20
    public class DataNAttribute : System.Attribute
    {
        // Fields
        public System.Int32 capacity;  // 0x10
        public System.Int32 tSize;  // 0x14
        public System.Boolean ring;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000035
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000017  // size: 0x10
    public static class AnimatorBlackboardUtility
    {
        // Fields
        private static System.Boolean s_enableFastPathBuffer;  // static @ 0x0

        // Methods
        // RVA: -1  // generic def  token: 0x6000036
        private static System.Void ClearAndEnsureDictionaryCapacity(System.Collections.Generic.Dictionary<TKey,TValue>& dict, System.Int32 capacity) { }

    }

    // TypeToken: 0x2000018  // size: 0x10
    public class AnimatorBlackboardAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000037
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000019  // size: 0x10
    public class CameraControlConfigAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000038
        public System.Void .ctor() { }

    }

}

namespace HG.Rendering.Runtime
{

    // TypeToken: 0x2000002  // size: 0x10
    public class HGConstantBufferLayoutAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000001
        public System.Void .ctor() { }

    }

}

