// ========================================================
// Dumped by @desirepro
// Assembly: Hypergryph.EventLogSDK.dll
// Classes:  10
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

namespace Hypergryph.SDK
{

    // TypeToken: 0x2000002  // size: 0x10
    public class CloudGameUtil
    {
        // Methods
        // RVA: 0x040BC870  token: 0x6000001
        public static System.Boolean IsCloudGame() { }

    }

    // TypeToken: 0x2000003
    public interface IEventLogSDK
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000002
        public virtual System.Boolean setEnvironment(System.String env) { }
        // RVA: -1  // abstract  token: 0x6000003
        public virtual System.Boolean enableRealTimeSend(System.Boolean enable) { }
        // RVA: -1  // abstract  token: 0x6000004
        public virtual System.Boolean init(System.String appId, System.String regionTag) { }
        // RVA: -1  // abstract  token: 0x6000005
        public virtual System.Boolean setGlobalProperties(System.String appId, System.String globalProperties) { }
        // RVA: -1  // abstract  token: 0x6000006
        public virtual System.Boolean unsetGlobalProperties(System.String appId, System.String propertyKeys) { }
        // RVA: -1  // abstract  token: 0x6000007
        public virtual System.Void clearGlobalProperties(System.String appId) { }
        // RVA: -1  // abstract  token: 0x6000008
        public virtual System.Boolean eventTrack(System.String appId, System.String name, System.String properties) { }
        // RVA: -1  // abstract  token: 0x6000009
        public virtual System.Boolean appStartEvent(System.String appId, System.String channel1, System.String channel2, System.Boolean beat, System.String properties) { }
        // RVA: -1  // abstract  token: 0x600000A
        public virtual System.Void pauseBeat(System.String appId) { }
        // RVA: -1  // abstract  token: 0x600000B
        public virtual System.Void resumeBeat(System.String appId) { }
        // RVA: -1  // abstract  token: 0x600000C
        public virtual System.Boolean userLoginEvent(System.String appId, System.String userId, System.String properties) { }
        // RVA: -1  // abstract  token: 0x600000D
        public virtual System.Void unsetUser(System.String appId) { }
        // RVA: -1  // abstract  token: 0x600000E
        public virtual System.Boolean characterLoginEvent(System.String appId, System.String characterId, System.String serverId, System.String properties) { }
        // RVA: -1  // abstract  token: 0x600000F
        public virtual System.Void unsetCharacter(System.String appId) { }
        // RVA: -1  // abstract  token: 0x6000010
        public virtual System.String getPresetProperties(System.String appId) { }
        // RVA: -1  // abstract  token: 0x6000011
        public virtual System.Void flush(System.String appId) { }
        // RVA: -1  // abstract  token: 0x6000012
        public virtual System.String getStaticPresetProperties(System.String appId) { }
        // RVA: -1  // abstract  token: 0x6000013
        public virtual System.String getDeviceIdProperties(System.String appId) { }
        // RVA: -1  // abstract  token: 0x6000014
        public virtual System.Boolean setGlobalPropertiesV2(System.String globalProperties) { }
        // RVA: -1  // abstract  token: 0x6000015
        public virtual System.Boolean unsetGlobalPropertiesV2(System.String propertyKeys) { }
        // RVA: -1  // abstract  token: 0x6000016
        public virtual System.Void clearGlobalPropertiesV2() { }
        // RVA: -1  // abstract  token: 0x6000017
        public virtual System.Boolean eventTrackV2(System.String name, System.String properties) { }
        // RVA: -1  // abstract  token: 0x6000018
        public virtual System.Boolean appStartEventV2(System.String channel1, System.String channel2, System.Boolean beat, System.String properties) { }
        // RVA: -1  // abstract  token: 0x6000019
        public virtual System.Void pauseBeatV2() { }
        // RVA: -1  // abstract  token: 0x600001A
        public virtual System.Void resumeBeatV2() { }
        // RVA: -1  // abstract  token: 0x600001B
        public virtual System.Boolean userLoginEventV2(System.String userId, System.String properties) { }
        // RVA: -1  // abstract  token: 0x600001C
        public virtual System.Void unsetUserV2() { }
        // RVA: -1  // abstract  token: 0x600001D
        public virtual System.Boolean characterLoginEventV2(System.String characterId, System.String serverId, System.String properties) { }
        // RVA: -1  // abstract  token: 0x600001E
        public virtual System.Void unsetCharacterV2() { }
        // RVA: -1  // abstract  token: 0x600001F
        public virtual System.String getPresetPropertiesV2() { }
        // RVA: -1  // abstract  token: 0x6000020
        public virtual System.Void flushV2() { }
        // RVA: -1  // abstract  token: 0x6000021
        public virtual System.String getStaticPresetPropertiesV2() { }
        // RVA: -1  // abstract  token: 0x6000022
        public virtual System.String getDeviceIdPropertiesV2() { }
        // RVA: -1  // abstract  token: 0x6000023
        public virtual System.Boolean eventTrackV3(System.String appId, System.String name, System.String properties) { }

    }

    // TypeToken: 0x2000004  // size: 0x10
    public static class HGEventLogSDKBridge
    {
        // Fields
        private static Hypergryph.SDK.IEventLogSDK s_el;  // static @ 0x0

        // Properties
        Hypergryph.SDK.IEventLogSDK eventLogSdk { get; /* RVA: 0x040BC440 */ }

    }

    // TypeToken: 0x2000005  // size: 0x18
    public class HGEventLogSDKAppInstance : UnityEngine.MonoBehaviour
    {
        // Methods
        // RVA: 0x09DDD450  token: 0x6000025
        public static System.Boolean SetEnvironment(System.String env) { }
        // RVA: 0x09DDD47C  token: 0x6000026
        public static System.Boolean SetGlobalProperties(System.String globalProperties) { }
        // RVA: 0x09DDD54C  token: 0x6000027
        public static System.Boolean UnsetGlobalProperties(System.String propertyKeys) { }
        // RVA: 0x09DDD218  token: 0x6000028
        public static System.Void ClearGlobalProperties() { }
        // RVA: 0x040BC3E0  token: 0x6000029
        public static System.Boolean EventTrack(System.String name, System.String properties) { }
        // RVA: 0x09DDD0C0  token: 0x600002A
        public static System.Boolean AppStartEvent(System.String channel1, System.String channel2, System.Boolean beat, System.String properties) { }
        // RVA: 0x09DDD61C  token: 0x600002B
        public static System.Boolean UserLoginEvent(System.String userId, System.String properties) { }
        // RVA: 0x09DDD5BC  token: 0x600002C
        public static System.Void UnsetUser() { }
        // RVA: 0x09DDD174  token: 0x600002D
        public static System.Boolean CharacterLoginEvent(System.String characterId, System.String serverId, System.String properties) { }
        // RVA: 0x09DDD4EC  token: 0x600002E
        public static System.Void UnsetCharacter() { }
        // RVA: 0x040BC220  token: 0x600002F
        public static System.String GetPresetProperties() { }
        // RVA: 0x09DDD408  token: 0x6000030
        public static System.Void PauseBeat() { }
        // RVA: 0x09DDD42C  token: 0x6000031
        public static System.Void ResumeBeat() { }
        // RVA: 0x09DDD2A4  token: 0x6000032
        public static System.Void Flush() { }
        // RVA: 0x09DDD278  token: 0x6000033
        public static System.Boolean EnableRealTimeSend(System.Boolean enable) { }
        // RVA: 0x09DDD368  token: 0x6000034
        public static System.String GetStaticPresetProperties() { }
        // RVA: 0x09DDD304  token: 0x6000035
        public static System.String GetDeviceIdProperties() { }
        // RVA: 0x09DDD3CC  token: 0x6000036
        private System.Void OnApplicationPause(System.Boolean pause) { }
        // RVA: 0x05393520  token: 0x6000037
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000006  // size: 0x10
    public class HGEventLogSDKInstance
    {
        // Methods
        // RVA: 0x040BA9B0  token: 0x6000038
        public static System.Boolean init(System.String appId, System.String region) { }
        // RVA: 0x09DDD6A0  token: 0x6000039
        public static System.Boolean EventTrackWithAppId(System.String appId, System.String name, System.String properties) { }

    }

    // TypeToken: 0x2000007  // size: 0x18
    public class HGEventLogSDKMgr : UnityEngine.MonoBehaviour
    {
        // Fields
        private static System.String appIdForInstance;  // static @ 0x0

        // Methods
        // RVA: 0x09DDDF84  token: 0x600003A
        public static System.Boolean SetEnvironment(System.String env) { }
        // RVA: 0x09DDDD80  token: 0x600003B
        public static System.Boolean Init(System.String appId, System.String regionTag) { }
        // RVA: 0x09DDDFE8  token: 0x600003C
        public static System.Boolean SetGlobalProperties(System.String globalProperties) { }
        // RVA: 0x09DDE15C  token: 0x600003D
        public static System.Boolean UnsetGlobalProperties(System.String propertyKeys) { }
        // RVA: 0x09DDD940  token: 0x600003E
        public static System.Void ClearGlobalProperties() { }
        // RVA: 0x09DDD9EC  token: 0x600003F
        public static System.Boolean EventTrack(System.String name, System.String properties) { }
        // RVA: 0x09DDD744  token: 0x6000040
        public static System.Boolean AppStartEvent(System.String channel1, System.String channel2, System.Boolean beat, System.String properties) { }
        // RVA: 0x09DDE2D0  token: 0x6000041
        public static System.Boolean UserLoginEvent(System.String userId, System.String properties) { }
        // RVA: 0x09DDE224  token: 0x6000042
        public static System.Void UnsetUser() { }
        // RVA: 0x09DDD850  token: 0x6000043
        public static System.Boolean CharacterLoginEvent(System.String characterId, System.String serverId, System.String properties) { }
        // RVA: 0x09DDE0B0  token: 0x6000044
        public static System.Void UnsetCharacter() { }
        // RVA: 0x09DDDC20  token: 0x6000045
        public static System.String GetPresetProperties() { }
        // RVA: 0x09DDDEBC  token: 0x6000046
        public static System.Void PauseBeat() { }
        // RVA: 0x09DDDF20  token: 0x6000047
        public static System.Void ResumeBeat() { }
        // RVA: 0x09DDDAC4  token: 0x6000048
        public static System.Void Flush() { }
        // RVA: 0x09DDD278  token: 0x6000049
        public static System.Boolean EnableRealTimeSend(System.Boolean enable) { }
        // RVA: 0x09DDDCD0  token: 0x600004A
        public static System.String GetStaticPresetProperties() { }
        // RVA: 0x09DDDB70  token: 0x600004B
        public static System.String GetDeviceIdProperties() { }
        // RVA: 0x09DDDE1C  token: 0x600004C
        private System.Void OnApplicationPause(System.Boolean pause) { }
        // RVA: 0x05393520  token: 0x600004D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000008  // size: 0x20
    public class HGEventLogSDKPluginCloud : Hypergryph.SDK.IEventLogSDK
    {
        // Fields
        private System.Object _cloudClientBridgeInstance;  // 0x10
        private System.Reflection.MethodInfo _sendDataWithKeyMethod;  // 0x18

        // Methods
        // RVA: 0x09DDE444  token: 0x600004E
        private static System.Boolean EventLogInitInstance(System.String appId, System.String regionTag) { }
        // RVA: 0x09DDE3A8  token: 0x600004F
        private static System.Boolean EventLogEvent(System.String appId, System.String name, System.String properties) { }
        // RVA: 0x09DDE4BC  token: 0x6000050
        private System.Void SendDataWithKey(System.String key, System.String data, System.Action<System.String> callback) { }
        // RVA: 0x09DDEB68  token: 0x6000051
        public virtual System.Boolean appStartEvent(System.String appId, System.String channel1, System.String channel2, System.Boolean beat, System.String properties) { }
        // RVA: 0x09DDE77C  token: 0x6000052
        public virtual System.Boolean appStartEventV2(System.String channel1, System.String channel2, System.Boolean beat, System.String properties) { }
        // RVA: 0x09DDEF3C  token: 0x6000053
        public virtual System.Boolean characterLoginEvent(System.String appId, System.String characterId, System.String serverId, System.String properties) { }
        // RVA: 0x09DDEBB8  token: 0x6000054
        public virtual System.Boolean characterLoginEventV2(System.String characterId, System.String serverId, System.String properties) { }
        // RVA: 0x09DDF198  token: 0x6000055
        public virtual System.Void clearGlobalProperties(System.String appId) { }
        // RVA: 0x09DDEF8C  token: 0x6000056
        public virtual System.Void clearGlobalPropertiesV2() { }
        // RVA: 0x09DDF1EC  token: 0x6000057
        public virtual System.Boolean enableRealTimeSend(System.Boolean enable) { }
        // RVA: 0x09DDF7F0  token: 0x6000058
        public virtual System.Boolean eventTrack(System.String appId, System.String name, System.String properties) { }
        // RVA: 0x09DDF49C  token: 0x6000059
        public virtual System.Boolean eventTrackV2(System.String name, System.String properties) { }
        // RVA: 0x09DDF760  token: 0x600005A
        public virtual System.Boolean eventTrackV3(System.String appId, System.String name, System.String properties) { }
        // RVA: 0x09DDFB50  token: 0x600005B
        public virtual System.Void flush(System.String appId) { }
        // RVA: 0x09DDF944  token: 0x600005C
        public virtual System.Void flushV2() { }
        // RVA: 0x09DDFC0C  token: 0x600005D
        public virtual System.String getDeviceIdProperties(System.String appId) { }
        // RVA: 0x09DDFBA4  token: 0x600005E
        public virtual System.String getDeviceIdPropertiesV2() { }
        // RVA: 0x09DDFCDC  token: 0x600005F
        public virtual System.String getPresetProperties(System.String appId) { }
        // RVA: 0x09DDFC74  token: 0x6000060
        public virtual System.String getPresetPropertiesV2() { }
        // RVA: 0x09DDFDAC  token: 0x6000061
        public virtual System.String getStaticPresetProperties(System.String appId) { }
        // RVA: 0x09DDFD44  token: 0x6000062
        public virtual System.String getStaticPresetPropertiesV2() { }
        // RVA: 0x09DDFE14  token: 0x6000063
        public virtual System.Boolean init(System.String appId, System.String regionTag) { }
        // RVA: 0x09DE0160  token: 0x6000064
        public virtual System.Void pauseBeat(System.String appId) { }
        // RVA: 0x09DDFF54  token: 0x6000065
        public virtual System.Void pauseBeatV2() { }
        // RVA: 0x09DE03C0  token: 0x6000066
        public virtual System.Void resumeBeat(System.String appId) { }
        // RVA: 0x09DE01B4  token: 0x6000067
        public virtual System.Void resumeBeatV2() { }
        // RVA: 0x09DE0414  token: 0x6000068
        public virtual System.Boolean setEnvironment(System.String env) { }
        // RVA: 0x09DE0924  token: 0x6000069
        public virtual System.Boolean setGlobalProperties(System.String appId, System.String globalProperties) { }
        // RVA: 0x09DE069C  token: 0x600006A
        public virtual System.Boolean setGlobalPropertiesV2(System.String globalProperties) { }
        // RVA: 0x09DE0B80  token: 0x600006B
        public virtual System.Void unsetCharacter(System.String appId) { }
        // RVA: 0x09DE0974  token: 0x600006C
        public virtual System.Void unsetCharacterV2() { }
        // RVA: 0x09DE0E5C  token: 0x600006D
        public virtual System.Boolean unsetGlobalProperties(System.String appId, System.String propertyKeys) { }
        // RVA: 0x09DE0BD4  token: 0x600006E
        public virtual System.Boolean unsetGlobalPropertiesV2(System.String propertyKeys) { }
        // RVA: 0x09DE10B8  token: 0x600006F
        public virtual System.Void unsetUser(System.String appId) { }
        // RVA: 0x09DE0EAC  token: 0x6000070
        public virtual System.Void unsetUserV2() { }
        // RVA: 0x09DE13D0  token: 0x6000071
        public virtual System.Boolean userLoginEvent(System.String appId, System.String userId, System.String properties) { }
        // RVA: 0x09DE110C  token: 0x6000072
        public virtual System.Boolean userLoginEventV2(System.String userId, System.String properties) { }
        // RVA: 0x041E1670  token: 0x6000073
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000009  // size: 0x10
    public class HGEventLogSDKPluginWindows : Hypergryph.SDK.IEventLogSDK
    {
        // Methods
        // RVA: 0x09DE1E84  token: 0x6000074
        private static System.Boolean EventLogSetEnvironment(System.String env) { }
        // RVA: 0x040BA7E0  token: 0x6000075
        private static System.Boolean EventLogInitInstance(System.String appId, System.String regionTag) { }
        // RVA: 0x09DE1F3C  token: 0x6000076
        private static System.Boolean EventLogSetGlobalProperties(System.String appId, System.String globalProperties) { }
        // RVA: 0x09DE2128  token: 0x6000077
        private static System.Boolean EventLogUnsetGlobalProperties(System.String appId, System.String propertyKeys) { }
        // RVA: 0x09DE19C8  token: 0x6000078
        private static System.Boolean EventLogClearGlobalProperties(System.String appId) { }
        // RVA: 0x09DE1A9C  token: 0x6000079
        private static System.Boolean EventLogEvent(System.String appId, System.String name, System.String properties) { }
        // RVA: 0x09DE1514  token: 0x600007A
        private static System.Boolean EventLogAppStartEvent(System.String appId, System.String channel1, System.String channel2, System.Boolean beat, System.String properties) { }
        // RVA: 0x09DE1650  token: 0x600007B
        private static System.Boolean EventLogBeatPause(System.String appId) { }
        // RVA: 0x09DE1714  token: 0x600007C
        private static System.Boolean EventLogBeatResume(System.String appId) { }
        // RVA: 0x09DE22F8  token: 0x600007D
        private static System.Boolean EventLogUserLoginEvent(System.String appId, System.String userId, System.String properties) { }
        // RVA: 0x09DE2214  token: 0x600007E
        private static System.Boolean EventLogUnsetUser(System.String appId) { }
        // RVA: 0x09DE1890  token: 0x600007F
        private static System.Boolean EventLogCharacterLoginEvent(System.String appId, System.String characterId, System.String serverId, System.String properties) { }
        // RVA: 0x09DE2030  token: 0x6000080
        private static System.Boolean EventLogUnsetCharacter(System.String appId) { }
        // RVA: 0x09DE1D20  token: 0x6000081
        private static System.IntPtr EventLogGetPresetProperties(System.String appId) { }
        // RVA: 0x09DE1DEC  token: 0x6000082
        private static System.IntPtr EventLogGetStaticPresetProperties(System.String appId) { }
        // RVA: 0x09DE1C88  token: 0x6000083
        private static System.IntPtr EventLogGetDeviceIdProperties(System.String appId) { }
        // RVA: 0x09DE1A24  token: 0x6000084
        private static System.Boolean EventLogEnableRealTimeSend(System.Boolean enable) { }
        // RVA: 0x09DE1BA0  token: 0x6000085
        private static System.Void EventLogFlush(System.String appId) { }
        // RVA: 0x040BC600  token: 0x6000086
        private static System.Boolean EventLogFree(System.IntPtr buf) { }
        // RVA: 0x09DE208C  token: 0x6000087
        private static System.Boolean EventLogUnsetGlobalPropertiesV2(System.String property_keys) { }
        // RVA: 0x09DE1EE0  token: 0x6000088
        private static System.Boolean EventLogSetGlobalPropertiesV2(System.String global_properties) { }
        // RVA: 0x09DE1994  token: 0x6000089
        private static System.Void EventLogClearGlobalPropertiesV2() { }
        // RVA: 0x0445BAF0  token: 0x600008A
        private static System.Boolean EventLogEventV2(System.String name, System.String json_str) { }
        // RVA: 0x09DE1420  token: 0x600008B
        private static System.Boolean EventLogAppStartEventV2(System.String channel1, System.String channel2, System.Boolean beat, System.String json_str) { }
        // RVA: 0x09DE1624  token: 0x600008C
        private static System.Void EventLogBeatPauseV2() { }
        // RVA: 0x09DE16AC  token: 0x600008D
        private static System.Void EventLogBeatResumeV2() { }
        // RVA: 0x09DE2270  token: 0x600008E
        private static System.Boolean EventLogUserLoginEventV2(System.String user_id, System.String json_str) { }
        // RVA: 0x09DE21E8  token: 0x600008F
        private static System.Void EventLogUnsetUserV2() { }
        // RVA: 0x09DE17B0  token: 0x6000090
        private static System.Boolean EventLogCharacterLoginEventV2(System.String character_id, System.String server_id, System.String json_str) { }
        // RVA: 0x09DE1FFC  token: 0x6000091
        private static System.Void EventLogUnsetCharacterV2() { }
        // RVA: 0x040BC300  token: 0x6000092
        private static System.IntPtr EventLogGetPresetPropertiesV2() { }
        // RVA: 0x09DE1DB8  token: 0x6000093
        private static System.IntPtr EventLogGetStaticPresetPropertiesV2() { }
        // RVA: 0x09DE1C20  token: 0x6000094
        private static System.IntPtr EventLogGetDeviceIdPropertiesV2() { }
        // RVA: 0x09DE1B38  token: 0x6000095
        private static System.Void EventLogFlushV2() { }
        // RVA: 0x041E1670  token: 0x6000096
        public System.Void .ctor() { }
        // RVA: 0x09DE2D34  token: 0x6000097
        public virtual System.Boolean setEnvironment(System.String env) { }
        // RVA: 0x040BC680  token: 0x6000098
        public virtual System.Boolean init(System.String appId, System.String regionTag) { }
        // RVA: 0x09DE2DBC  token: 0x6000099
        public virtual System.Boolean setGlobalProperties(System.String appId, System.String globalProperties) { }
        // RVA: 0x09DE2ED0  token: 0x600009A
        public virtual System.Boolean unsetGlobalProperties(System.String appId, System.String propertyKeys) { }
        // RVA: 0x09DE2560  token: 0x600009B
        public virtual System.Void clearGlobalProperties(System.String appId) { }
        // RVA: 0x09DE2668  token: 0x600009C
        public virtual System.Boolean eventTrack(System.String appId, System.String name, System.String properties) { }
        // RVA: 0x09DE2408  token: 0x600009D
        public virtual System.Boolean appStartEvent(System.String appId, System.String channel1, System.String channel2, System.Boolean beat, System.String properties) { }
        // RVA: 0x09DE2C34  token: 0x600009E
        public virtual System.Void pauseBeat(System.String appId) { }
        // RVA: 0x09DE2CB8  token: 0x600009F
        public virtual System.Void resumeBeat(System.String appId) { }
        // RVA: 0x09DE2FEC  token: 0x60000A0
        public virtual System.Boolean userLoginEvent(System.String appId, System.String userId, System.String properties) { }
        // RVA: 0x09DE2F5C  token: 0x60000A1
        public virtual System.Void unsetUser(System.String appId) { }
        // RVA: 0x09DE24C0  token: 0x60000A2
        public virtual System.Boolean characterLoginEvent(System.String appId, System.String characterId, System.String serverId, System.String properties) { }
        // RVA: 0x09DE2E48  token: 0x60000A3
        public virtual System.Void unsetCharacter(System.String appId) { }
        // RVA: 0x09DE2958  token: 0x60000A4
        public virtual System.String getPresetProperties(System.String appId) { }
        // RVA: 0x09DE2B38  token: 0x60000A5
        public virtual System.String getStaticPresetProperties(System.String appId) { }
        // RVA: 0x09DE2864  token: 0x60000A6
        public virtual System.String getDeviceIdProperties(System.String appId) { }
        // RVA: 0x09DE26FC  token: 0x60000A7
        public virtual System.Void flush(System.String appId) { }
        // RVA: 0x09DE25DC  token: 0x60000A8
        public virtual System.Boolean enableRealTimeSend(System.Boolean enable) { }
        // RVA: 0x09DE2DB0  token: 0x60000A9
        public virtual System.Boolean setGlobalPropertiesV2(System.String globalProperties) { }
        // RVA: 0x09DE2EC4  token: 0x60000AA
        public virtual System.Boolean unsetGlobalPropertiesV2(System.String propertyKeys) { }
        // RVA: 0x09DE2558  token: 0x60000AB
        public virtual System.Void clearGlobalPropertiesV2() { }
        // RVA: 0x05396E8C  token: 0x60000AC
        public virtual System.Boolean eventTrackV2(System.String name, System.String properties) { }
        // RVA: 0x09DE23D8  token: 0x60000AD
        public virtual System.Boolean appStartEventV2(System.String channel1, System.String channel2, System.Boolean beat, System.String properties) { }
        // RVA: 0x09DE2C2C  token: 0x60000AE
        public virtual System.Void pauseBeatV2() { }
        // RVA: 0x09DE2CB0  token: 0x60000AF
        public virtual System.Void resumeBeatV2() { }
        // RVA: 0x09DE2FD8  token: 0x60000B0
        public virtual System.Boolean userLoginEventV2(System.String userId, System.String properties) { }
        // RVA: 0x09DE2F54  token: 0x60000B1
        public virtual System.Void unsetUserV2() { }
        // RVA: 0x09DE24A8  token: 0x60000B2
        public virtual System.Boolean characterLoginEventV2(System.String characterId, System.String serverId, System.String properties) { }
        // RVA: 0x09DE2E40  token: 0x60000B3
        public virtual System.Void unsetCharacterV2() { }
        // RVA: 0x040BC270  token: 0x60000B4
        public virtual System.String getPresetPropertiesV2() { }
        // RVA: 0x09DE26F4  token: 0x60000B5
        public virtual System.Void flushV2() { }
        // RVA: 0x09DE2A4C  token: 0x60000B6
        public virtual System.String getStaticPresetPropertiesV2() { }
        // RVA: 0x09DE2778  token: 0x60000B7
        public virtual System.String getDeviceIdPropertiesV2() { }
        // RVA: 0x09DE2658  token: 0x60000B8
        public virtual System.Boolean eventTrackV3(System.String appId, System.String name, System.String properties) { }

    }

    // TypeToken: 0x200000A  // size: 0x10
    public static class HGEventLogSDKVersion
    {
        // Fields
        public static readonly System.String VERSION;  // static @ 0x0

        // Methods
        // RVA: 0x04D50680  token: 0x60000B9
        private static System.Void .cctor() { }

    }

}

