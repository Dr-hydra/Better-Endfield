// ========================================================
// Dumped by @desirepro
// Assembly: HGSDKInterfaces.dll
// Classes:  34
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000002  // size: 0x14
    public sealed struct CSLogSeverity
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CSLogSeverity LogSilent;  // const
        public static CSLogSeverity LogError;  // const
        public static CSLogSeverity LogWarning;  // const
        public static CSLogSeverity LogInfo;  // const
        public static CSLogSeverity LogDebug;  // const
        public static CSLogSeverity LogVerbose;  // const

    }

    // TypeToken: 0x2000003  // size: 0x14
    public sealed struct CSReportType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static CSReportType InterfaceReport;  // const
        public static CSReportType LogCallback;  // const
        public static CSReportType LogCallbackThreaded;  // const

    }

    // TypeToken: 0x2000004  // size: 0x14
    public sealed struct JankReportInfoOption
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static JankReportInfoOption JankOnlyBasicInfo;  // const
        public static JankReportInfoOption JankSystemLog;  // const
        public static JankReportInfoOption JankCustomLog;  // const
        public static JankReportInfoOption JankCustomKv;  // const
        public static JankReportInfoOption JankCallbackInfo;  // const
        public static JankReportInfoOption JankAutoDumpStack;  // const
        public static JankReportInfoOption JankAndroidAllJavaStack;  // const

    }

    // TypeToken: 0x2000006  // size: 0x10
    public class HGCrashSightCallback : CrashSightCallback
    {
        // Methods
        // RVA: 0x0321CB90  token: 0x6000065
        public virtual System.String OnCrashBaseRetEvent(System.Int32 methodId, System.Int32 crashType) { }
        // RVA: 0x04D7C2B0  token: 0x6000066
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000007  // size: 0x10
    public class HGCrashSightLogCallback : CrashSightLogCallback
    {
        // Methods
        // RVA: 0x09D2CCD0  token: 0x6000067
        public virtual System.String OnSetLogPathEvent(System.Int32 methodId, System.Int32 crashType) { }
        // RVA: 0x09D2CB70  token: 0x6000068
        public virtual System.Void OnLogUploadResultEvent(System.Int32 methodId, System.Int32 crashType, System.Int32 result) { }
        // RVA: 0x04D7C270  token: 0x6000069
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000008  // size: 0x80
    public sealed class LogCallbackDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0673C544  token: 0x600006A
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x01546280  token: 0x600006B
        public virtual System.Void Invoke(System.String condition, System.String stackTrace, UnityEngine.LogType type) { }
        // RVA: 0x09D2CDA4  token: 0x600006C
        public virtual System.IAsyncResult BeginInvoke(System.String condition, System.String stackTrace, UnityEngine.LogType type, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x600006D
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000009  // size: 0x80
    public sealed class LogFilterDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x04CF2860  token: 0x600006E
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x014F3120  token: 0x600006F
        public virtual System.Boolean Invoke(System.String condition, System.String stackTrace, UnityEngine.LogType type) { }
        // RVA: 0x09D2CE2C  token: 0x6000070
        public virtual System.IAsyncResult BeginInvoke(System.String condition, System.String stackTrace, UnityEngine.LogType type, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE668  token: 0x6000071
        public virtual System.Boolean EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000005  // size: 0x10
    public sealed class CrashSightAgent
    {
        // Fields
        private static CrashSightAgent.HGCrashSightCallback _crashSightCallback;  // static @ 0x0
        private static CrashSightAgent.HGCrashSightLogCallback _crashSightLogCallback;  // static @ 0x8
        private static System.String crashUploadUrl;  // static @ 0x10
        public static System.Collections.Generic.List<System.Int32> callbackThreads;  // static @ 0x18
        public static System.Object callbackThreadsLock;  // static @ 0x20
        private static CrashSightAgent.LogCallbackDelegate _LogCallbackEventHandler;  // static @ 0x28
        private static CrashSightAgent.LogFilterDelegate s_logFilter;  // static @ 0x30
        private static System.Boolean _isInitialized;  // static @ 0x38
        private static UnityEngine.LogType _autoReportLogLevel;  // static @ 0x3c
        private static System.Boolean _debugMode;  // static @ 0x40
        private static System.Boolean _autoQuitApplicationAfterReport;  // static @ 0x41
        private static System.Func<System.Collections.Generic.Dictionary<System.String,System.String>> _LogCallbackExtrasHandler;  // static @ 0x48
        private static System.Boolean _uncaughtAutoReportOnce;  // static @ 0x50
        public static readonly System.String s_UnityBuildVersion;  // static @ 0x58
        public static readonly System.String s_CommandLine;  // static @ 0x60
        public static readonly System.String s_UserName;  // static @ 0x68
        public static readonly System.String s_UserDomainName;  // static @ 0x70
        public static readonly System.String s_DeviceModel;  // static @ 0x78
        public static readonly System.String s_DeviceName;  // static @ 0x80
        public static readonly System.String s_DeviceUniqueIdentifier;  // static @ 0x88
        public static readonly System.String s_DeviceType;  // static @ 0x90
        public static readonly System.String s_GraphicsDeviceName;  // static @ 0x98
        public static readonly System.String s_GraphicsDeviceType;  // static @ 0xa0
        public static readonly System.String s_GraphicsDeviceVersion;  // static @ 0xa8
        public static readonly System.String s_GraphicsDeviceVendor;  // static @ 0xb0
        public static readonly System.Int32 s_GraphicsMemorySize;  // static @ 0xb8
        public static readonly System.String s_NvidiaDriverVersion;  // static @ 0xc0
        public static readonly System.String[] s_BootParams;  // static @ 0xc8
        public static System.String[] s_vmas;  // static @ 0xd0

        // Properties
        System.Boolean AutoQuitApplicationAfterReport { get; /* RVA: 0x09D2B4B0 */ }
        System.Boolean IsInitialized { get; /* RVA: 0x09D2B500 */ }

        // Events
        event CrashSightAgent.LogCallbackDelegate _LogCallbackEventHandler;

        // Methods
        // RVA: 0x09D2A0C8  token: 0x6000003
        public static System.Void SetLogFilter(CrashSightAgent.LogFilterDelegate filter) { }
        // RVA: 0x03B11DF0  token: 0x6000004
        public static System.Void InitWithAppId(System.String appId, System.Boolean forceOnUiThread) { }
        // RVA: 0x09D29968  token: 0x6000005
        public static System.Void ReportException(System.Exception e, System.String message) { }
        // RVA: 0x09D2974C  token: 0x6000006
        public static System.Void ReportException(System.String name, System.String message, System.String stackTrace) { }
        // RVA: 0x09D298B4  token: 0x6000007
        public static System.Void ReportException(System.Int32 type, System.String exceptionName, System.String exceptionMsg, System.String exceptionStack, System.Collections.Generic.Dictionary<System.String,System.String> extInfo, System.Int32 dumpNativeType, System.String errorAttachmentPath) { }
        // RVA: 0x03A86070  token: 0x6000008
        public static System.Void SetUserId(System.String userId) { }
        // RVA: 0x0321F280  token: 0x6000009
        public static System.Void AddSceneData(System.String key, System.String value) { }
        // RVA: 0x043A7E70  token: 0x600000A
        public static System.Void SetUserValue(System.String key, System.Int32 value) { }
        // RVA: 0x0321F110  token: 0x600000B
        public static System.Void SetUserValue(System.String key, System.String value) { }
        // RVA: 0x03B5BFD0  token: 0x600000C
        public static System.Void SetUserValue(System.String key, System.String[] value) { }
        // RVA: 0x04A223E0  token: 0x600000D
        public static System.Void SetAppVersion(System.String appVersion) { }
        // RVA: 0x04B0E060  token: 0x600000E
        public static System.Void ConfigCrashServerUrl(System.String crashServerUrl) { }
        // RVA: 0x09D2A128  token: 0x600000F
        public static System.Void SetLogPath(System.String logPath) { }
        // RVA: 0x04A064E0  token: 0x6000010
        public static System.Void ConfigDebugMode(System.Boolean enable) { }
        // RVA: 0x09D29D88  token: 0x6000011
        public static System.Void SetDeviceId(System.String deviceId) { }
        // RVA: 0x09D28C18  token: 0x6000012
        public static System.Void ConfigCrashReporter(System.Int32 logLevel) { }
        // RVA: 0x09D28BD0  token: 0x6000013
        public static System.Void ConfigCrashReporter(CSLogSeverity logLevel) { }
        // RVA: 0x03608920  token: 0x6000014
        public static System.Void PrintLog(CSLogSeverity level, System.String format, System.Object[] args) { }
        // RVA: 0x09D2A6D0  token: 0x6000015
        public static System.Void TestNativeCrash() { }
        // RVA: 0x09D29E68  token: 0x6000016
        public static System.Void SetEnvironmentName(System.String serverEnv) { }
        // RVA: 0x09D28E3C  token: 0x6000017
        public static System.Void EnableAnrMonitor(System.Int32 timeoutMs) { }
        // RVA: 0x09D28DFC  token: 0x6000018
        public static System.Void DisableAnrMonitor() { }
        // RVA: 0x03B11C50  token: 0x6000019
        public static System.Void RegisterCrashCallback(CrashSightCallback callback) { }
        // RVA: 0x09D2A8E0  token: 0x600001A
        public static System.Void UnregisterCrashCallback() { }
        // RVA: 0x03B11B40  token: 0x600001B
        public static System.Void RegisterCrashLogCallback(CrashSightLogCallback callback) { }
        // RVA: 0x09D28E84  token: 0x600001C
        public static System.Void EnableExceptionHandler() { }
        // RVA: 0x09D295CC  token: 0x600001D
        public static System.Void RegisterLogCallback(CrashSightAgent.LogCallbackDelegate handler) { }
        // RVA: 0x09D2A920  token: 0x600001E
        public static System.Void UnregisterLogCallback(CrashSightAgent.LogCallbackDelegate handler) { }
        // RVA: 0x09D2A010  token: 0x600001F
        public static System.Void SetLogCallbackExtrasHandler(System.Func<System.Collections.Generic.Dictionary<System.String,System.String>> handler) { }
        // RVA: 0x09D28B34  token: 0x6000020
        public static System.Void ConfigAutoQuitApplication(System.Boolean autoQuit) { }
        // RVA: 0x0321F230  token: 0x6000022
        public static System.Void DebugLog(System.String tag, System.String format) { }
        // RVA: 0x03B0D1D0  token: 0x6000024
        public static System.Void _RegisterExceptionHandler() { }
        // RVA: 0x09D2B0E8  token: 0x6000025
        public static System.Void _UnregisterExceptionHandler() { }
        // RVA: 0x041E1670  token: 0x6000026
        public static System.Void SetCrashSightStackTraceEnable(System.Boolean enable) { }
        // RVA: 0x09D28B88  token: 0x6000027
        public static System.Void ConfigCallbackType(System.Int32 callbackType) { }
        // RVA: 0x09D29DD4  token: 0x6000028
        public static System.Void SetDeviceModel(System.String deviceModel) { }
        // RVA: 0x09D29B58  token: 0x6000029
        public static System.Void ReportLogInfo(System.String msgType, System.String msg) { }
        // RVA: 0x09D2A280  token: 0x600002A
        public static System.Void SetScene(System.String sceneId, System.Boolean upload) { }
        // RVA: 0x09D2A378  token: 0x600002B
        public static System.Void SetScene(System.Int32 sceneId, System.Boolean upload) { }
        // RVA: 0x09D28F14  token: 0x600002C
        public static System.Int64 GetCrashThreadId() { }
        // RVA: 0x09D29CF0  token: 0x600002D
        public static System.Void SetCustomizedDeviceID(System.String deviceId) { }
        // RVA: 0x09D2907C  token: 0x600002E
        public static System.String GetSDKDefinedDeviceID() { }
        // RVA: 0x09D29D3C  token: 0x600002F
        public static System.Void SetCustomizedMatchID(System.String matchId) { }
        // RVA: 0x09D290BC  token: 0x6000030
        public static System.String GetSDKSessionID() { }
        // RVA: 0x09D2A7D8  token: 0x6000031
        public static System.Void TestOomCrash() { }
        // RVA: 0x09D2A64C  token: 0x6000032
        public static System.Void TestJavaCrash() { }
        // RVA: 0x09D2A5C8  token: 0x6000033
        public static System.Void TestANR() { }
        // RVA: 0x09D28FA0  token: 0x6000034
        public static System.String GetCrashUuid() { }
        // RVA: 0x09D2A174  token: 0x6000035
        public static System.Void SetLogcatBufferSize(System.Int32 size) { }
        // RVA: 0x09D2A754  token: 0x6000036
        public static System.Void TestOcCrash() { }
        // RVA: 0x09D2A498  token: 0x6000037
        public static System.Void StartDumpRoutine(System.Int32 dumpMode, System.Int32 startTimeMode, System.Int64 startTime, System.Int64 dumpInterval, System.Int32 dumpTimes, System.Boolean saveLocal, System.String savePath) { }
        // RVA: 0x09D2A548  token: 0x6000038
        public static System.Void StartMonitorFdCount(System.Int32 interval, System.Int32 limit, System.Int32 dumpType) { }
        // RVA: 0x09D2B444  token: 0x6000039
        public static System.Int32 getExceptionType(System.String name) { }
        // RVA: 0x09D2A85C  token: 0x600003A
        public static System.Void TestUseAfterFree() { }
        // RVA: 0x09D2954C  token: 0x600003B
        public static System.Void ReRegistAllMonitors() { }
        // RVA: 0x09D28AC8  token: 0x600003C
        public static System.Void CloseAllMonitors() { }
        // RVA: 0x09D2B6C0  token: 0x600003D
        public static System.Void setEnableGetPackageInfo(System.Boolean enable) { }
        // RVA: 0x09D2B608  token: 0x600003E
        public static System.Void setCatchMultiSignal(System.Boolean enable) { }
        // RVA: 0x09D2B38C  token: 0x600003F
        public static System.Void enableDetailedPageTracing(System.Boolean enable) { }
        // RVA: 0x09D294C4  token: 0x6000040
        public static System.Boolean IsLastSessionCrash() { }
        // RVA: 0x09D28FE0  token: 0x6000041
        public static System.String GetLastSessionUserId() { }
        // RVA: 0x09D28A44  token: 0x6000042
        public static System.Boolean CheckFdCount(System.Int32 limit, System.Int32 dumpType, System.Boolean upload) { }
        // RVA: 0x09D2A1BC  token: 0x6000043
        public static System.Void SetOomLogPath(System.String logPath) { }
        // RVA: 0x09D29AC4  token: 0x6000044
        public static System.Void ReportJank(System.Int32 type, System.String exceptionName, System.String exceptionMsg, System.String exceptionStack, System.String paramsJson, System.Int32 reportInfoOption, System.String jankAttachmentPath) { }
        // RVA: 0x09D29BB4  token: 0x6000045
        public static System.Void ReportStuck(System.Int32 threadId, System.Int32 maxChecks, System.Int64 checkInterval, System.String name, System.String message, System.Collections.Generic.Dictionary<System.String,System.String> extInfo, System.Int32 dumpNativeType, System.String attachPath) { }
        // RVA: 0x09D2A3D8  token: 0x6000046
        public static System.Void SetVehEnable(System.Boolean enable) { }
        // RVA: 0x09D2965C  token: 0x6000047
        public static System.Void ReportCrash() { }
        // RVA: 0x09D296C8  token: 0x6000048
        public static System.Void ReportDump(System.String dump_path, System.Boolean is_async) { }
        // RVA: 0x09D29F9C  token: 0x6000049
        public static System.Void SetExtraHandler(System.Boolean extra_handle_enable) { }
        // RVA: 0x09D2A9D0  token: 0x600004A
        public static System.Void UploadGivenPathDump(System.String dump_dir, System.Boolean is_extra_check) { }
        // RVA: 0x09D29E20  token: 0x600004B
        public static System.Void SetDumpType(System.Int32 dump_type) { }
        // RVA: 0x09D289F8  token: 0x600004C
        public static System.Void AddValidExpCode(System.UInt64 exp_code) { }
        // RVA: 0x09D2A984  token: 0x600004D
        public static System.Void UploadCrashWithGuid(System.String guid) { }
        // RVA: 0x09D29C58  token: 0x600004E
        public static System.Void SetCrashUploadEnable(System.Boolean enable) { }
        // RVA: 0x09D2A44C  token: 0x600004F
        public static System.Void SetWorkSpace(System.String workspace) { }
        // RVA: 0x09D29CA4  token: 0x6000050
        public static System.Void SetCustomAttachDir(System.String path) { }
        // RVA: 0x09D29F28  token: 0x6000051
        public static System.Void SetErrorUploadInterval(System.Int32 interval) { }
        // RVA: 0x09D29EB4  token: 0x6000052
        public static System.Void SetErrorUploadEnable(System.Boolean enable) { }
        // RVA: 0x09D2A208  token: 0x6000053
        public static System.Void SetRecordFileDir(System.String record_dir) { }
        // RVA: 0x09D290FC  token: 0x6000054
        public static System.Void InitContext(System.String userId, System.String version, System.String key) { }
        // RVA: 0x09D292DC  token: 0x6000055
        public static System.Void Init(System.String app_id, System.String app_key, System.String app_version) { }
        // RVA: 0x09D28C60  token: 0x6000056
        public static System.Void ConfigDefault(System.String channel, System.String version, System.String user, System.Int64 delay) { }
        // RVA: 0x0321F3F0  token: 0x6000057
        private static System.Void _OnLogCallbackHandlerMain(System.String condition, System.String stackTrace, UnityEngine.LogType type) { }
        // RVA: 0x0321F450  token: 0x6000058
        private static System.Void _OnLogCallbackHandlerThreaded(System.String condition, System.String stackTrace, UnityEngine.LogType type) { }
        // RVA: 0x0321F4B0  token: 0x6000059
        private static System.Void _OnLogCallbackHandler(System.String condition, System.String stackTrace, UnityEngine.LogType type, CSReportType rType) { }
        // RVA: 0x09D2AF2C  token: 0x600005A
        private static System.Void _OnUncaughtExceptionHandler(System.Object sender, System.UnhandledExceptionEventArgs args) { }
        // RVA: 0x09D2AA54  token: 0x600005B
        private static System.Void _HandleException(System.Exception e, System.String message, System.Boolean uncaught) { }
        // RVA: 0x02F0CE30  token: 0x600005C
        private static System.Boolean ShouldSkipFrame(System.String frame) { }
        // RVA: 0x02F0E3C0  token: 0x600005D
        private static System.Void _reportException(System.Boolean uncaught, System.String name, System.String reason, System.String stackTrace) { }
        // RVA: 0x0321EDC0  token: 0x600005E
        private static System.Void AddTrackCustomData() { }
        // RVA: 0x041E1670  token: 0x600005F
        private static System.Void AddLocalLogFile() { }
        // RVA: 0x0321FBA0  token: 0x6000060
        private static System.Int32 valueOf(UnityEngine.LogType logLevel) { }
        // RVA: 0x0321FAD0  token: 0x6000061
        private static System.Boolean isEnableAutoReport(UnityEngine.LogType logLevel) { }
        // RVA: 0x0321F7B0  token: 0x6000062
        private static System.Void _HandleException(UnityEngine.LogType logLevel, System.String name, System.String message, System.String stackTrace, System.Boolean uncaught, CSReportType rType) { }
        // RVA: 0x041E1670  token: 0x6000063
        public System.Void .ctor() { }
        // RVA: 0x041A7870  token: 0x6000064
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000B  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly CrashSightAnrMonitor.<>c <>9;  // static @ 0x0
        public static System.Action <>9__11_0;  // static @ 0x8
        public static System.Action <>9__11_1;  // static @ 0x10
        public static System.Action <>9__11_2;  // static @ 0x18

        // Methods
        // RVA: 0x09D2E9D8  token: 0x600007B
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600007C
        public System.Void .ctor() { }
        // RVA: 0x09D2E918  token: 0x600007D
        private System.Void <Start>b__11_0() { }
        // RVA: 0x09D2E958  token: 0x600007E
        private System.Void <Start>b__11_1() { }
        // RVA: 0x09D2E998  token: 0x600007F
        private System.Void <Start>b__11_2() { }

    }

    // TypeToken: 0x200000C  // size: 0x28
    public sealed class <UpdateUiStatus>d__13 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        private UnityEngine.WaitForSecondsRealtime <waitForSeconds>5__2;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000080
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000081
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x09D2EA3C  token: 0x6000082
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x09D2EB98  token: 0x6000084
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200000A  // size: 0x10
    public class CrashSightAnrMonitor
    {
        // Fields
        private static CrashSightMonoBehaviour monoBehaviour;  // static @ 0x0
        private static System.Int32 anrTimeoutMs;  // static @ 0x8
        private static System.Int32 detectionTimeoutMs;  // static @ 0xc
        private static System.Boolean <Paused>k__BackingField;  // static @ 0x10
        private static System.Int32 ticksSinceUiUpdate;  // static @ 0x14
        private static System.Boolean reported;  // static @ 0x18
        private static System.Boolean running;  // static @ 0x19
        private static System.Threading.Thread thread;  // static @ 0x20

        // Properties
        System.Boolean Paused { get; /* RVA: 0x09D2C088 */ set; /* RVA: 0x09D2C0D8 */ }

        // Methods
        // RVA: 0x09D2BAA0  token: 0x6000074
        public static System.Void Start(System.Int32 timeoutMs) { }
        // RVA: 0x09D2BF38  token: 0x6000075
        public static System.Void Stop() { }
        // RVA: 0x09D2BF88  token: 0x6000076
        private static System.Collections.IEnumerator UpdateUiStatus() { }
        // RVA: 0x09D2B844  token: 0x6000077
        private static System.Void Run() { }
        // RVA: 0x09D2B778  token: 0x6000078
        private static System.Void Report() { }
        // RVA: 0x041E1670  token: 0x6000079
        public System.Void .ctor() { }
        // RVA: 0x09D2BFCC  token: 0x600007A
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000D  // size: 0x90
    public class UnityAnrException : System.Exception
    {
        // Methods
        // RVA: 0x09D366AC  token: 0x6000086
        private System.Void .ctor() { }
        // RVA: 0x09D36650  token: 0x6000087
        private System.Void .ctor(System.String message) { }
        // RVA: 0x09D366F8  token: 0x6000088
        private System.Void .ctor(System.String message, System.Exception innerException) { }

    }

    // TypeToken: 0x200000E  // size: 0x10
    public abstract class CrashSightCallback
    {
        // Fields
        private static System.Boolean isQuitting;  // static @ 0x0
        private static System.Object quitLocker;  // static @ 0x8

        // Methods
        // RVA: 0x033295B0  token: 0x6000089
        private static System.Void .cctor() { }
        // RVA: 0x09D2C230  token: 0x600008A
        private static System.Void OnApplicationQuitting() { }
        // RVA: 0x09D2C12C  token: 0x600008B
        public static System.Boolean IsQuitting() { }
        // RVA: -1  // abstract  token: 0x600008C
        public virtual System.String OnCrashBaseRetEvent(System.Int32 methodId, System.Int32 crashType) { }
        // RVA: 0x041E1670  token: 0x600008D
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200000F  // size: 0x10
    public abstract class CrashSightLogCallback
    {
        // Fields
        private static System.Boolean isQuitting;  // static @ 0x0
        private static System.Object quitLocker;  // static @ 0x8

        // Methods
        // RVA: 0x03329690  token: 0x600008E
        private static System.Void .cctor() { }
        // RVA: 0x09D2C428  token: 0x600008F
        private static System.Void OnApplicationQuitting() { }
        // RVA: 0x09D2C324  token: 0x6000090
        public static System.Boolean IsQuitting() { }
        // RVA: -1  // abstract  token: 0x6000091
        public virtual System.String OnSetLogPathEvent(System.Int32 methodId, System.Int32 crashType) { }
        // RVA: -1  // abstract  token: 0x6000092
        public virtual System.Void OnLogUploadResultEvent(System.Int32 methodId, System.Int32 crashType, System.Int32 result) { }
        // RVA: 0x041E1670  token: 0x6000093
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000010  // size: 0x38
    public class CrashSightMonoBehaviour : UnityEngine.MonoBehaviour
    {
        // Fields
        private static CrashSightMonoBehaviour _instance;  // static @ 0x0
        private System.Action ApplicationResuming;  // 0x18
        private System.Action ApplicationPausing;  // 0x20
        private System.Action ApplicationQuitting;  // 0x28
        private System.Boolean _isRunning;  // 0x30

        // Properties
        CrashSightMonoBehaviour Instance { get; /* RVA: 0x09D2C7E8 */ }

        // Events
        event System.Action ApplicationResuming;
        event System.Action ApplicationPausing;
        event System.Action ApplicationQuitting;

        // Methods
        // RVA: 0x09D2C5DC  token: 0x600009B
        public System.Void UpdatePauseStatus(System.Boolean paused) { }
        // RVA: 0x09D2C578  token: 0x600009C
        private System.Void OnApplicationPause(System.Boolean pauseStatus) { }
        // RVA: 0x09D2C56C  token: 0x600009D
        private System.Void OnApplicationFocus(System.Boolean hasFocus) { }
        // RVA: 0x09D2C580  token: 0x600009E
        private System.Void OnApplicationQuit() { }
        // RVA: 0x09D2C51C  token: 0x600009F
        private System.Void Awake() { }
        // RVA: 0x09D2C610  token: 0x60000A0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000011  // size: 0x10
    public class CrashSightStackTrace
    {
        // Fields
        public static System.Boolean enable;  // static @ 0x0
        private static System.String stackTrace;  // static @ 0x8

        // Methods
        // RVA: 0x041E1670  token: 0x60000A1
        public static System.Void setEnable(System.Boolean enable) { }
        // RVA: 0x09D2CAC4  token: 0x60000A2
        public static System.String ExtractStackTrace() { }
        // RVA: 0x041E1670  token: 0x60000A3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001B  // size: 0x14
    public sealed struct Level
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static GCloud.UQM.UQMLog.Level None;  // const
        public static GCloud.UQM.UQMLog.Level Log;  // const
        public static GCloud.UQM.UQMLog.Level Warning;  // const
        public static GCloud.UQM.UQMLog.Level Error;  // const

    }

    // TypeToken: 0x200001E  // size: 0x80
    public sealed class UQMRetJsonEventHandler : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x03B12300  token: 0x600013D
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x014F3120  token: 0x600013E
        public virtual System.String Invoke(System.Int32 methodId, System.Int32 callType, System.Int32 logUploadResult) { }
        // RVA: 0x09D364BC  token: 0x600013F
        public virtual System.IAsyncResult BeginInvoke(System.Int32 methodId, System.Int32 callType, System.Int32 logUploadResult, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x05397E84  token: 0x6000140
        public virtual System.String EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000021  // size: 0x14
    public sealed struct TOKEN
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static GCloud.UQM.MiniJSON.Json.Parser.TOKEN NONE;  // const
        public static GCloud.UQM.MiniJSON.Json.Parser.TOKEN CURLY_OPEN;  // const
        public static GCloud.UQM.MiniJSON.Json.Parser.TOKEN CURLY_CLOSE;  // const
        public static GCloud.UQM.MiniJSON.Json.Parser.TOKEN SQUARED_OPEN;  // const
        public static GCloud.UQM.MiniJSON.Json.Parser.TOKEN SQUARED_CLOSE;  // const
        public static GCloud.UQM.MiniJSON.Json.Parser.TOKEN COLON;  // const
        public static GCloud.UQM.MiniJSON.Json.Parser.TOKEN COMMA;  // const
        public static GCloud.UQM.MiniJSON.Json.Parser.TOKEN STRING;  // const
        public static GCloud.UQM.MiniJSON.Json.Parser.TOKEN NUMBER;  // const
        public static GCloud.UQM.MiniJSON.Json.Parser.TOKEN TRUE;  // const
        public static GCloud.UQM.MiniJSON.Json.Parser.TOKEN FALSE;  // const
        public static GCloud.UQM.MiniJSON.Json.Parser.TOKEN NULL;  // const

    }

    // TypeToken: 0x2000020  // size: 0x18
    public sealed class Parser : System.IDisposable
    {
        // Fields
        private static System.String WORD_BREAK;  // const
        private System.IO.StringReader json;  // 0x10

        // Properties
        System.Char PeekChar { get; /* RVA: 0x09D2DA30 */ }
        System.Char NextChar { get; /* RVA: 0x09D2D700 */ }
        System.String NextWord { get; /* RVA: 0x09D2D980 */ }
        GCloud.UQM.MiniJSON.Json.Parser.TOKEN NextToken { get; /* RVA: 0x09D2D75C */ }

        // Methods
        // RVA: 0x09D2CF7C  token: 0x6000143
        public static System.Boolean IsWordBreak(System.Char c) { }
        // RVA: 0x09D2D688  token: 0x6000144
        private System.Void .ctor(System.String jsonString) { }
        // RVA: 0x09D2D590  token: 0x6000145
        public static System.Object Parse(System.String jsonString) { }
        // RVA: 0x09D2CEB4  token: 0x6000146
        public virtual System.Void Dispose() { }
        // RVA: 0x09D2D234  token: 0x6000147
        private System.Collections.Generic.Dictionary<System.String,System.Object> ParseObject() { }
        // RVA: 0x09D2CFF4  token: 0x6000148
        private System.Collections.Generic.List<System.Object> ParseArray() { }
        // RVA: 0x09D2D56C  token: 0x6000149
        private System.Object ParseValue() { }
        // RVA: 0x09D2D0BC  token: 0x600014A
        private System.Object ParseByToken(GCloud.UQM.MiniJSON.Json.Parser.TOKEN token) { }
        // RVA: 0x09D2D340  token: 0x600014B
        private System.String ParseString() { }
        // RVA: 0x09D2D170  token: 0x600014C
        private System.Object ParseNumber() { }
        // RVA: 0x09D2CEEC  token: 0x600014D
        private System.Void EatWhitespace() { }

    }

    // TypeToken: 0x2000022  // size: 0x18
    public sealed class Serializer
    {
        // Fields
        private System.Text.StringBuilder builder;  // 0x10

        // Methods
        // RVA: 0x09D2E8B4  token: 0x6000152
        private System.Void .ctor() { }
        // RVA: 0x09D2E83C  token: 0x6000153
        public static System.String Serialize(System.Object obj) { }
        // RVA: 0x09D2E6B8  token: 0x6000154
        private System.Void SerializeValue(System.Object value) { }
        // RVA: 0x09D2DE10  token: 0x6000155
        private System.Void SerializeObject(System.Collections.IDictionary obj) { }
        // RVA: 0x09D2DA8C  token: 0x6000156
        private System.Void SerializeArray(System.Collections.IList anArray) { }
        // RVA: 0x09D2E440  token: 0x6000157
        private System.Void SerializeString(System.String str) { }
        // RVA: 0x09D2E218  token: 0x6000158
        private System.Void SerializeOther(System.Object value) { }

    }

namespace GCloud.UQM
{

    // TypeToken: 0x2000012
    public sealed class OnUQMRetEventHandler`1 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x60000A4
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x60000A5
        public virtual System.Void Invoke(T ret) { }
        // RVA: -1  // runtime  token: 0x60000A6
        public virtual System.IAsyncResult BeginInvoke(T ret, System.AsyncCallback callback, System.Object object) { }
        // RVA: -1  // runtime  token: 0x60000A7
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000013
    public sealed class OnUQMStringRetEventHandler`1 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x60000A8
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x60000A9
        public virtual System.String Invoke(T ret, T crashType) { }
        // RVA: -1  // runtime  token: 0x60000AA
        public virtual System.IAsyncResult BeginInvoke(T ret, T crashType, System.AsyncCallback callback, System.Object object) { }
        // RVA: -1  // runtime  token: 0x60000AB
        public virtual System.String EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000014
    public sealed class OnUQMStringRetSetLogPathEventHandler`1 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x60000AC
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x60000AD
        public virtual System.String Invoke(T ret, T crashType) { }
        // RVA: -1  // runtime  token: 0x60000AE
        public virtual System.IAsyncResult BeginInvoke(T ret, T crashType, System.AsyncCallback callback, System.Object object) { }
        // RVA: -1  // runtime  token: 0x60000AF
        public virtual System.String EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000015
    public sealed class OnUQMRetLogUploadEventHandler`1 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x60000B0
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x60000B1
        public virtual System.Void Invoke(T ret, T crashType, T result) { }
        // RVA: -1  // runtime  token: 0x60000B2
        public virtual System.IAsyncResult BeginInvoke(T ret, T crashType, T result, System.AsyncCallback callback, System.Object object) { }
        // RVA: -1  // runtime  token: 0x60000B3
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000016  // size: 0x10
    public class UQM
    {
        // Fields
        public static System.String LibName;  // const
        private static System.Boolean initialized;  // static @ 0x0
        public static System.Boolean isDebug;  // static @ 0x1

        // Methods
        // RVA: 0x09D36554  token: 0x60000B4
        public static System.Void Init() { }
        // RVA: 0x041E1670  token: 0x60000B5
        public System.Void .ctor() { }
        // RVA: 0x09D36618  token: 0x60000B6
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000017  // size: 0x14
    public sealed struct UQMCrashLevel
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static GCloud.UQM.UQMCrashLevel CSLogLevelSilent;  // const
        public static GCloud.UQM.UQMCrashLevel CSLogLevelError;  // const
        public static GCloud.UQM.UQMCrashLevel CSLogLevelWarn;  // const
        public static GCloud.UQM.UQMCrashLevel CSLogLevelInfo;  // const
        public static GCloud.UQM.UQMCrashLevel CSLogLevelDebug;  // const
        public static GCloud.UQM.UQMCrashLevel CSLogLevelVerbose;  // const

    }

    // TypeToken: 0x2000018  // size: 0x10
    public static class UQMCrash
    {
        // Fields
        private static GCloud.UQM.OnUQMStringRetEventHandler<System.Int32> CrashBaseRetEvent;  // static @ 0x0
        private static GCloud.UQM.OnUQMStringRetSetLogPathEventHandler<System.Int32> CrashSetLogPathRetEvent;  // static @ 0x8
        private static GCloud.UQM.OnUQMRetLogUploadEventHandler<System.Int32> CrashLogUploadRetEvent;  // static @ 0x10
        private static UnityEngine.AndroidJavaClass _gameAgentClass;  // static @ 0x18
        private static System.Boolean _isLoadedSo;  // static @ 0x20
        private static System.Int32 _gameType;  // static @ 0x24
        private static readonly System.String GAME_AGENT_CLASS;  // static @ 0x28

        // Properties
        UnityEngine.AndroidJavaClass CrashSightPlatform { get; /* RVA: 0x09D35954 */ }

        // Events
        event GCloud.UQM.OnUQMStringRetEventHandler<System.Int32> CrashBaseRetEvent;
        event GCloud.UQM.OnUQMStringRetSetLogPathEventHandler<System.Int32> CrashSetLogPathRetEvent;
        event GCloud.UQM.OnUQMRetLogUploadEventHandler<System.Int32> CrashLogUploadRetEvent;

        // Methods
        // RVA: 0x09D2EE6C  token: 0x60000B7
        private static System.Void CS_InitContext(System.String id, System.String version, System.String key) { }
        // RVA: 0x03608AB0  token: 0x60000B8
        private static System.Void CS_ReportExceptionW(System.Int32 type, System.String name, System.String message, System.String stack_trace, System.String extras, System.Boolean is_async, System.String attachmentPath) { }
        // RVA: 0x03608980  token: 0x60000B9
        private static System.Void CS_SetUserValue(System.String key, System.String value) { }
        // RVA: 0x09D2F518  token: 0x60000BA
        private static System.Void CS_SetVehEnable(System.Boolean enable) { }
        // RVA: 0x09D2F4E4  token: 0x60000BB
        private static System.Void CS_SetExtraHandler(System.Boolean extra_handle_enable) { }
        // RVA: 0x09D2F2D4  token: 0x60000BC
        private static System.Void CS_SetCustomLogDirW(System.String log_path) { }
        // RVA: 0x03A86210  token: 0x60000BD
        private static System.Void CS_SetUserId(System.String user_id) { }
        // RVA: 0x09D2EF3C  token: 0x60000BE
        private static System.Void CS_MonitorEnable(System.Boolean enable) { }
        // RVA: 0x03608A00  token: 0x60000BF
        private static System.Void CS_PrintLog(System.Int32 level, System.String tag, System.String format, System.String arg) { }
        // RVA: 0x09D2F704  token: 0x60000C0
        private static System.Void CS_UploadGivenPathDump(System.String dump_dir, System.Boolean is_extra_check) { }
        // RVA: 0x09D2EFB4  token: 0x60000C1
        private static System.Void CS_ReportCrash() { }
        // RVA: 0x09D2F01C  token: 0x60000C2
        private static System.Void CS_ReportDump(System.String dump_dir, System.Boolean is_async) { }
        // RVA: 0x09D2F458  token: 0x60000C3
        private static System.Void CS_SetEnvironmentName(System.String name) { }
        // RVA: 0x03B12470  token: 0x60000C4
        private static System.Void CS_InitWithAppId(System.String app_id) { }
        // RVA: 0x04A22540  token: 0x60000C5
        private static System.Void CS_SetAppVersion(System.String app_version) { }
        // RVA: 0x038277E0  token: 0x60000C6
        private static System.Void CS_ConfigCrashServerUrl(System.String crash_server_url) { }
        // RVA: 0x03827EB0  token: 0x60000C7
        private static System.Void CS_ConfigDebugMode(System.Boolean enable) { }
        // RVA: 0x09D2F354  token: 0x60000C8
        private static System.Void CS_SetDeviceId(System.String device_id) { }
        // RVA: 0x09D2EDF4  token: 0x60000C9
        private static System.Void CS_ConfigCrashReporter(System.Int32 log_level) { }
        // RVA: 0x09D2F610  token: 0x60000CA
        private static System.Void CS_TestNativeCrash() { }
        // RVA: 0x09D2F3E0  token: 0x60000CB
        private static System.Void CS_SetDumpType(System.Int32 dump_type) { }
        // RVA: 0x09D2ED7C  token: 0x60000CC
        private static System.Void CS_AddValidExpCode(System.UInt64 exp_code) { }
        // RVA: 0x09D2F678  token: 0x60000CD
        private static System.Void CS_UploadCrashWithGuid(System.String guid) { }
        // RVA: 0x09D2F1DC  token: 0x60000CE
        private static System.Void CS_SetCrashUploadEnable(System.Boolean enable) { }
        // RVA: 0x09D2F590  token: 0x60000CF
        private static System.Void CS_SetWorkSpaceW(System.String workspace) { }
        // RVA: 0x038275C0  token: 0x60000D0
        private static System.Void CS_SetEngineInfo(System.String version, System.String buildConfig, System.String language, System.String locale) { }
        // RVA: 0x09D2F254  token: 0x60000D1
        private static System.Void CS_SetCustomAttachDirW(System.String log_path) { }
        // RVA: 0x09D2F798  token: 0x60000D2
        private static System.Void CS_UseSavedUserId(System.Boolean enable) { }
        // RVA: 0x09D2F0B0  token: 0x60000D3
        private static System.Void CS_ReportStuck(System.Int32 threadId, System.Int32 maxChecks, System.Int32 checkInterval, System.String name, System.String message, System.String extraInfo, System.Int32 dumpNativeType, System.String attachPath) { }
        // RVA: 0x041E1670  token: 0x60000DB
        private static System.Void LoadCrashSightCoreSo() { }
        // RVA: 0x09D2FD74  token: 0x60000DC
        public static System.Void ConfigCallbackType(System.Int32 callbackType) { }
        // RVA: 0x03B124D0  token: 0x60000DD
        public static System.Void ConfigGameType(System.Int32 gameType) { }
        // RVA: 0x09D2FBE0  token: 0x60000DE
        public static System.Void ConfigAutoReportLogLevel(System.Int32 level) { }
        // RVA: 0x04B0E110  token: 0x60000DF
        public static System.Void ConfigCrashServerUrl(System.String serverUrl) { }
        // RVA: 0x04A065D0  token: 0x60000E0
        public static System.Void ConfigDebugMode(System.Boolean enable) { }
        // RVA: 0x09D2FEFC  token: 0x60000E1
        public static System.Void ConfigDefault(System.String channel, System.String version, System.String user, System.Int64 delay) { }
        // RVA: 0x03B12380  token: 0x60000E2
        public static System.Void InitWithAppId(System.String appId, System.Boolean forceOnUiThread) { }
        // RVA: 0x09D308B0  token: 0x60000E3
        public static System.Void InitContext(System.String userId, System.String version, System.String key) { }
        // RVA: 0x03608BF0  token: 0x60000E4
        public static System.Void LogRecord(System.Int32 level, System.String message) { }
        // RVA: 0x0321F170  token: 0x60000E5
        public static System.Void AddSceneData(System.String k, System.String v) { }
        // RVA: 0x03608620  token: 0x60000E6
        public static System.Void ReportException(System.Int32 type, System.String name, System.String message, System.String stackTrace, System.String extras, System.Boolean quitProgram) { }
        // RVA: 0x09D31B88  token: 0x60000E7
        public static System.Void ReportException(System.Int32 type, System.String exceptionName, System.String exceptionMsg, System.String exceptionStack, System.Collections.Generic.Dictionary<System.String,System.String> extInfo, System.Int32 dumpNativeType, System.String errorAttachmentPath) { }
        // RVA: 0x03A86150  token: 0x60000E8
        public static System.Void SetUserId(System.String userId) { }
        // RVA: 0x09D33CB4  token: 0x60000E9
        public static System.Void SetScene(System.String sceneId, System.Boolean upload) { }
        // RVA: 0x09D316E4  token: 0x60000EA
        public static System.Void ReRegistAllMonitors() { }
        // RVA: 0x09D2FA78  token: 0x60000EB
        public static System.Void CloseAllMonitors() { }
        // RVA: 0x09D320A4  token: 0x60000EC
        public static System.Void ReportLogInfo(System.String msgType, System.String msg) { }
        // RVA: 0x04A22490  token: 0x60000ED
        public static System.Void SetAppVersion(System.String appVersion) { }
        // RVA: 0x09D32C38  token: 0x60000EE
        public static System.Void SetDeviceId(System.String deviceId) { }
        // RVA: 0x09D32940  token: 0x60000EF
        public static System.Void SetCustomizedDeviceID(System.String deviceId) { }
        // RVA: 0x09D305B8  token: 0x60000F0
        public static System.String GetSDKDefinedDeviceID() { }
        // RVA: 0x09D32ABC  token: 0x60000F1
        public static System.Void SetCustomizedMatchID(System.String matchId) { }
        // RVA: 0x09D30734  token: 0x60000F2
        public static System.String GetSDKSessionID() { }
        // RVA: 0x09D302C0  token: 0x60000F3
        public static System.String GetCrashUuid() { }
        // RVA: 0x09D32DC0  token: 0x60000F4
        public static System.Void SetDeviceModel(System.String deviceModel) { }
        // RVA: 0x09D336DC  token: 0x60000F5
        public static System.Void SetLogPath(System.String logPath) { }
        // RVA: 0x03B11D80  token: 0x60000F6
        public static System.Void SetCrashCallback() { }
        // RVA: 0x09D35190  token: 0x60000F7
        public static System.Void UnsetCrashCallback() { }
        // RVA: 0x03B11D10  token: 0x60000F8
        public static System.Void SetCrashLogCallback() { }
        // RVA: 0x03F04C00  token: 0x60000F9
        private static System.String OnCrashCallbackMessage(System.Int32 methodId, System.Int32 crashType) { }
        // RVA: 0x09D30DC4  token: 0x60000FA
        private static System.String OnCrashCallbackData(System.Int32 methodId, System.Int32 crashType) { }
        // RVA: 0x09D31378  token: 0x60000FB
        private static System.String OnCrashSetLogPathMessage(System.Int32 methodId, System.Int32 crashType) { }
        // RVA: 0x09D3117C  token: 0x60000FC
        private static System.String OnCrashLogUploadMessage(System.Int32 methodId, System.Int32 crashType, System.Int32 result) { }
        // RVA: 0x09D30FA0  token: 0x60000FD
        private static System.String OnCrashCallbackNoRet(System.Int32 methodId, System.Int32 crashType) { }
        // RVA: 0x03B11A30  token: 0x60000FE
        public static System.Void ConfigCallBack() { }
        // RVA: 0x09D3513C  token: 0x60000FF
        public static System.Void UnregisterCallBack() { }
        // RVA: 0x03B119D0  token: 0x6000100
        public static System.Void ConfigLogCallBack() { }
        // RVA: 0x09D34E6C  token: 0x6000101
        public static System.Void TestOomCrash() { }
        // RVA: 0x09D34A2C  token: 0x6000102
        public static System.Void TestJavaCrash() { }
        // RVA: 0x09D34D04  token: 0x6000103
        public static System.Void TestOcCrash() { }
        // RVA: 0x09D34B94  token: 0x6000104
        public static System.Void TestNativeCrash() { }
        // RVA: 0x09D348C4  token: 0x6000105
        public static System.Void TestANR() { }
        // RVA: 0x09D3016C  token: 0x6000106
        public static System.Int64 GetCrashThreadId() { }
        // RVA: 0x09D33864  token: 0x6000107
        public static System.Void SetLogcatBufferSize(System.Int32 size) { }
        // RVA: 0x09D325CC  token: 0x6000108
        public static System.Void SetCallbackMsg(System.String data) { }
        // RVA: 0x09D3432C  token: 0x6000109
        public static System.Void StartDumpRoutine(System.Int32 dumpMode, System.Int32 startTimeMode, System.Int64 startTime, System.Int64 dumpInterval, System.Int32 dumpTimes, System.Boolean saveLocal, System.String savePath) { }
        // RVA: 0x09D34684  token: 0x600010A
        public static System.Void StartMonitorFdCount(System.Int32 interval, System.Int32 limit, System.Int32 dumpType) { }
        // RVA: 0x09D357D4  token: 0x600010B
        public static System.Int32 getExceptionType(System.String name) { }
        // RVA: 0x09D34FD4  token: 0x600010C
        public static System.Void TestUseAfterFree() { }
        // RVA: 0x09D33E70  token: 0x600010D
        public static System.Void SetServerEnv(System.String serverEnv) { }
        // RVA: 0x09D33FF8  token: 0x600010E
        public static System.Void SetVehEnable(System.Boolean enable) { }
        // RVA: 0x09D3184C  token: 0x600010F
        public static System.Void ReportCrash() { }
        // RVA: 0x09D319BC  token: 0x6000110
        public static System.Void ReportDump(System.String dump_path, System.Boolean is_async) { }
        // RVA: 0x09D33530  token: 0x6000111
        public static System.Void SetExtraHandler(System.Boolean extra_handle_enable) { }
        // RVA: 0x09D35468  token: 0x6000112
        public static System.Void UploadGivenPathDump(System.String dump_dir, System.Boolean is_extra_check) { }
        // RVA: 0x09D333C0  token: 0x6000113
        public static System.Void SetErrorUploadInterval(System.Int32 interval) { }
        // RVA: 0x09D33238  token: 0x6000114
        public static System.Void SetErrorUploadEnable(System.Boolean enable) { }
        // RVA: 0x09D33B50  token: 0x6000115
        public static System.Void SetRecordFileDir(System.String record_dir) { }
        // RVA: 0x09D30A58  token: 0x6000116
        public static System.Void Init(System.String app_id, System.String app_key, System.String app_version) { }
        // RVA: 0x09D35E2C  token: 0x6000117
        public static System.Void setEnableGetPackageInfo(System.Boolean enable) { }
        // RVA: 0x09D32F3C  token: 0x6000118
        public static System.Void SetDumpType(System.Int32 dump_type) { }
        // RVA: 0x09D2EBE4  token: 0x6000119
        public static System.Void AddValidExpCode(System.UInt64 exp_code) { }
        // RVA: 0x09D352E0  token: 0x600011A
        public static System.Void UploadCrashWithGuid(System.String guid) { }
        // RVA: 0x09D32748  token: 0x600011B
        public static System.Void SetCrashUploadEnable(System.Boolean enable) { }
        // RVA: 0x09D341A4  token: 0x600011C
        public static System.Void SetWorkSpace(System.String workspace) { }
        // RVA: 0x03B0B510  token: 0x600011D
        public static System.Void SetEngineInfo(System.String version, System.String buildConfig, System.String language, System.String locale) { }
        // RVA: 0x09D35C8C  token: 0x600011E
        public static System.Void setCatchMultiSignal(System.Boolean enable) { }
        // RVA: 0x09D35634  token: 0x600011F
        public static System.Void enableDetailedPageTracing(System.Boolean enable) { }
        // RVA: 0x09D35FCC  token: 0x6000120
        public static System.Void useSavedUserId(System.Boolean enable) { }
        // RVA: 0x09D328F4  token: 0x6000121
        public static System.Void SetCustomAttachDir(System.String path) { }
        // RVA: 0x09D30C70  token: 0x6000122
        public static System.Boolean IsLastSessionCrash() { }
        // RVA: 0x09D3043C  token: 0x6000123
        public static System.String GetLastSessionUserId() { }
        // RVA: 0x09D2F810  token: 0x6000124
        public static System.Boolean CheckFdCount(System.Int32 limit, System.Int32 dumpType, System.Boolean upload) { }
        // RVA: 0x09D339D4  token: 0x6000125
        public static System.Void SetOomLogPath(System.String logPath) { }
        // RVA: 0x09D31D8C  token: 0x6000126
        public static System.Void ReportJank(System.Int32 type, System.String exceptionName, System.String exceptionMsg, System.String exceptionStack, System.String paramsJson, System.Int32 reportInfoOption, System.String jankAttachmentPath) { }
        // RVA: 0x09D3155C  token: 0x6000127
        public static System.Void ProcessEngineAnr(System.Int32 type) { }
        // RVA: 0x09D330D0  token: 0x6000128
        public static System.Void SetEngineMainThread() { }
        // RVA: 0x09D3220C  token: 0x6000129
        public static System.Void ReportStuck(System.Int32 threadId, System.Int32 maxChecks, System.Int64 checkInterval, System.String name, System.String message, System.Collections.Generic.Dictionary<System.String,System.String> extInfo, System.Int32 dumpNativeType, System.String attachPath) { }
        // RVA: 0x04CE9450  token: 0x600012A
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000019  // size: 0x14
    public sealed struct UQMMethodNameID
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static GCloud.UQM.UQMMethodNameID UQM_CRASH_CALLBACK_EXTRA_DATA;  // const
        public static GCloud.UQM.UQMMethodNameID UQM_CRASH_CALLBACK_EXTRA_MESSAGE;  // const
        public static GCloud.UQM.UQMMethodNameID UQM_CRASH_CALLBACK_SET_LOG_PATH;  // const
        public static GCloud.UQM.UQMMethodNameID UQM_CRASH_CALLBACK_LOG_UPLOAD_RESULT;  // const

    }

    // TypeToken: 0x200001A  // size: 0x10
    public class UQMLog
    {
        // Fields
        private static GCloud.UQM.UQMLog.Level level;  // static @ 0x0
        private static System.String header;  // const

        // Methods
        // RVA: 0x09D36414  token: 0x600012B
        public static System.Void SetLevel(GCloud.UQM.UQMLog.Level l) { }
        // RVA: 0x03608BB0  token: 0x600012C
        public static System.Void Log(System.String message) { }
        // RVA: 0x09D36384  token: 0x600012D
        public static System.Void LogWarning(System.String message) { }
        // RVA: 0x09D362F4  token: 0x600012E
        public static System.Void LogError(System.String message) { }
        // RVA: 0x09D36178  token: 0x600012F
        public static System.Void FullLog(System.String message) { }
        // RVA: 0x041E1670  token: 0x6000130
        public System.Void .ctor() { }
        // RVA: 0x04D79810  token: 0x6000131
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200001C  // size: 0x20
    public class RetArgsWrapper
    {
        // Fields
        private readonly System.Int32 methodId;  // 0x10
        private readonly System.Int32 crashType;  // 0x14
        private readonly System.Int32 logUploadResult;  // 0x18

        // Properties
        System.Int32 MethodId { get; /* RVA: 0x02B2E2D0 */ }
        System.Int32 CrashType { get; /* RVA: 0x04D882A0 */ }
        System.Int32 LogUploadResult { get; /* RVA: 0x011EF5B0 */ }

        // Methods
        // RVA: 0x04D88580  token: 0x6000135
        public System.Void .ctor(System.Int32 _methodId, System.Int32 _crashType, System.Int32 _logUploadResult) { }

    }

    // TypeToken: 0x200001D  // size: 0x18
    public class UQMMessageCenter : UnityEngine.MonoBehaviour
    {
        // Fields
        private static System.Boolean initialzed;  // static @ 0x0
        private static GCloud.UQM.UQMMessageCenter instance;  // static @ 0x8

        // Properties
        GCloud.UQM.UQMMessageCenter Instance { get; /* RVA: 0x03B11740 */ }

        // Methods
        // RVA: 0x03F043E0  token: 0x6000136
        public static System.String OnUQMRet(System.Int32 methodId, System.Int32 crashType, System.Int32 logUploadResult) { }
        // RVA: 0x038270A0  token: 0x6000137
        private static System.Void cs_setUnityCallback(GCloud.UQM.UQMMessageCenter.UQMRetJsonEventHandler eventHandler) { }
        // RVA: 0x03B12210  token: 0x6000139
        public System.Void Init() { }
        // RVA: 0x09D36468  token: 0x600013A
        public System.Void Uninit() { }
        // RVA: 0x03F04AA0  token: 0x600013B
        private static System.String SynchronousDelegate(System.Object arg) { }
        // RVA: 0x05393520  token: 0x600013C
        public System.Void .ctor() { }

    }

}

namespace GCloud.UQM.MiniJSON
{

    // TypeToken: 0x200001F  // size: 0x10
    public static class Json
    {
        // Methods
        // RVA: 0x09D2CD80  token: 0x6000141
        public static System.Object Deserialize(System.String json) { }
        // RVA: 0x09D2CD9C  token: 0x6000142
        public static System.String Serialize(System.Object obj) { }

    }

}

