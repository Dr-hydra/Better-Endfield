// ========================================================
// Dumped by @desirepro
// Assembly: Hypergryph.EventLogSDK.dll
// Classes:  10
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

CLASS: Hypergryph.SDK.CloudGameUtil
TYPE:  class
TOKEN: 0x2000002
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x040BC870  token=0x6000001  System.Boolean IsCloudGame()
END_CLASS

CLASS: Hypergryph.SDK.IEventLogSDK
TYPE:  interface
TOKEN: 0x2000003
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000002  System.Boolean setEnvironment(System.String env)
  RVA=-1  // abstract  token=0x6000003  System.Boolean enableRealTimeSend(System.Boolean enable)
  RVA=-1  // abstract  token=0x6000004  System.Boolean init(System.String appId, System.String regionTag)
  RVA=-1  // abstract  token=0x6000005  System.Boolean setGlobalProperties(System.String appId, System.String globalProperties)
  RVA=-1  // abstract  token=0x6000006  System.Boolean unsetGlobalProperties(System.String appId, System.String propertyKeys)
  RVA=-1  // abstract  token=0x6000007  System.Void clearGlobalProperties(System.String appId)
  RVA=-1  // abstract  token=0x6000008  System.Boolean eventTrack(System.String appId, System.String name, System.String properties)
  RVA=-1  // abstract  token=0x6000009  System.Boolean appStartEvent(System.String appId, System.String channel1, System.String channel2, System.Boolean beat, System.String properties)
  RVA=-1  // abstract  token=0x600000A  System.Void pauseBeat(System.String appId)
  RVA=-1  // abstract  token=0x600000B  System.Void resumeBeat(System.String appId)
  RVA=-1  // abstract  token=0x600000C  System.Boolean userLoginEvent(System.String appId, System.String userId, System.String properties)
  RVA=-1  // abstract  token=0x600000D  System.Void unsetUser(System.String appId)
  RVA=-1  // abstract  token=0x600000E  System.Boolean characterLoginEvent(System.String appId, System.String characterId, System.String serverId, System.String properties)
  RVA=-1  // abstract  token=0x600000F  System.Void unsetCharacter(System.String appId)
  RVA=-1  // abstract  token=0x6000010  System.String getPresetProperties(System.String appId)
  RVA=-1  // abstract  token=0x6000011  System.Void flush(System.String appId)
  RVA=-1  // abstract  token=0x6000012  System.String getStaticPresetProperties(System.String appId)
  RVA=-1  // abstract  token=0x6000013  System.String getDeviceIdProperties(System.String appId)
  RVA=-1  // abstract  token=0x6000014  System.Boolean setGlobalPropertiesV2(System.String globalProperties)
  RVA=-1  // abstract  token=0x6000015  System.Boolean unsetGlobalPropertiesV2(System.String propertyKeys)
  RVA=-1  // abstract  token=0x6000016  System.Void clearGlobalPropertiesV2()
  RVA=-1  // abstract  token=0x6000017  System.Boolean eventTrackV2(System.String name, System.String properties)
  RVA=-1  // abstract  token=0x6000018  System.Boolean appStartEventV2(System.String channel1, System.String channel2, System.Boolean beat, System.String properties)
  RVA=-1  // abstract  token=0x6000019  System.Void pauseBeatV2()
  RVA=-1  // abstract  token=0x600001A  System.Void resumeBeatV2()
  RVA=-1  // abstract  token=0x600001B  System.Boolean userLoginEventV2(System.String userId, System.String properties)
  RVA=-1  // abstract  token=0x600001C  System.Void unsetUserV2()
  RVA=-1  // abstract  token=0x600001D  System.Boolean characterLoginEventV2(System.String characterId, System.String serverId, System.String properties)
  RVA=-1  // abstract  token=0x600001E  System.Void unsetCharacterV2()
  RVA=-1  // abstract  token=0x600001F  System.String getPresetPropertiesV2()
  RVA=-1  // abstract  token=0x6000020  System.Void flushV2()
  RVA=-1  // abstract  token=0x6000021  System.String getStaticPresetPropertiesV2()
  RVA=-1  // abstract  token=0x6000022  System.String getDeviceIdPropertiesV2()
  RVA=-1  // abstract  token=0x6000023  System.Boolean eventTrackV3(System.String appId, System.String name, System.String properties)
END_CLASS

CLASS: Hypergryph.SDK.HGEventLogSDKBridge
TYPE:  static class
TOKEN: 0x2000004
SIZE:  0x10
FIELDS:
  private   static  Hypergryph.SDK.IEventLogSDK     s_el  // static @ 0x0
PROPERTIES:
  eventLogSdk  get=0x040BC440
METHODS:
END_CLASS

CLASS: Hypergryph.SDK.HGEventLogSDKAppInstance
TYPE:  class
TOKEN: 0x2000005
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
METHODS:
  RVA=0x09DDD450  token=0x6000025  System.Boolean SetEnvironment(System.String env)
  RVA=0x09DDD47C  token=0x6000026  System.Boolean SetGlobalProperties(System.String globalProperties)
  RVA=0x09DDD54C  token=0x6000027  System.Boolean UnsetGlobalProperties(System.String propertyKeys)
  RVA=0x09DDD218  token=0x6000028  System.Void ClearGlobalProperties()
  RVA=0x040BC3E0  token=0x6000029  System.Boolean EventTrack(System.String name, System.String properties)
  RVA=0x09DDD0C0  token=0x600002A  System.Boolean AppStartEvent(System.String channel1, System.String channel2, System.Boolean beat, System.String properties)
  RVA=0x09DDD61C  token=0x600002B  System.Boolean UserLoginEvent(System.String userId, System.String properties)
  RVA=0x09DDD5BC  token=0x600002C  System.Void UnsetUser()
  RVA=0x09DDD174  token=0x600002D  System.Boolean CharacterLoginEvent(System.String characterId, System.String serverId, System.String properties)
  RVA=0x09DDD4EC  token=0x600002E  System.Void UnsetCharacter()
  RVA=0x040BC220  token=0x600002F  System.String GetPresetProperties()
  RVA=0x09DDD408  token=0x6000030  System.Void PauseBeat()
  RVA=0x09DDD42C  token=0x6000031  System.Void ResumeBeat()
  RVA=0x09DDD2A4  token=0x6000032  System.Void Flush()
  RVA=0x09DDD278  token=0x6000033  System.Boolean EnableRealTimeSend(System.Boolean enable)
  RVA=0x09DDD368  token=0x6000034  System.String GetStaticPresetProperties()
  RVA=0x09DDD304  token=0x6000035  System.String GetDeviceIdProperties()
  RVA=0x09DDD3CC  token=0x6000036  System.Void OnApplicationPause(System.Boolean pause)
  RVA=0x05393520  token=0x6000037  System.Void .ctor()
END_CLASS

CLASS: Hypergryph.SDK.HGEventLogSDKInstance
TYPE:  class
TOKEN: 0x2000006
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x040BA9B0  token=0x6000038  System.Boolean init(System.String appId, System.String region)
  RVA=0x09DDD6A0  token=0x6000039  System.Boolean EventTrackWithAppId(System.String appId, System.String name, System.String properties)
END_CLASS

CLASS: Hypergryph.SDK.HGEventLogSDKMgr
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private   static  System.String                   appIdForInstance  // static @ 0x0
METHODS:
  RVA=0x09DDDF84  token=0x600003A  System.Boolean SetEnvironment(System.String env)
  RVA=0x09DDDD80  token=0x600003B  System.Boolean Init(System.String appId, System.String regionTag)
  RVA=0x09DDDFE8  token=0x600003C  System.Boolean SetGlobalProperties(System.String globalProperties)
  RVA=0x09DDE15C  token=0x600003D  System.Boolean UnsetGlobalProperties(System.String propertyKeys)
  RVA=0x09DDD940  token=0x600003E  System.Void ClearGlobalProperties()
  RVA=0x09DDD9EC  token=0x600003F  System.Boolean EventTrack(System.String name, System.String properties)
  RVA=0x09DDD744  token=0x6000040  System.Boolean AppStartEvent(System.String channel1, System.String channel2, System.Boolean beat, System.String properties)
  RVA=0x09DDE2D0  token=0x6000041  System.Boolean UserLoginEvent(System.String userId, System.String properties)
  RVA=0x09DDE224  token=0x6000042  System.Void UnsetUser()
  RVA=0x09DDD850  token=0x6000043  System.Boolean CharacterLoginEvent(System.String characterId, System.String serverId, System.String properties)
  RVA=0x09DDE0B0  token=0x6000044  System.Void UnsetCharacter()
  RVA=0x09DDDC20  token=0x6000045  System.String GetPresetProperties()
  RVA=0x09DDDEBC  token=0x6000046  System.Void PauseBeat()
  RVA=0x09DDDF20  token=0x6000047  System.Void ResumeBeat()
  RVA=0x09DDDAC4  token=0x6000048  System.Void Flush()
  RVA=0x09DDD278  token=0x6000049  System.Boolean EnableRealTimeSend(System.Boolean enable)
  RVA=0x09DDDCD0  token=0x600004A  System.String GetStaticPresetProperties()
  RVA=0x09DDDB70  token=0x600004B  System.String GetDeviceIdProperties()
  RVA=0x09DDDE1C  token=0x600004C  System.Void OnApplicationPause(System.Boolean pause)
  RVA=0x05393520  token=0x600004D  System.Void .ctor()
END_CLASS

CLASS: Hypergryph.SDK.HGEventLogSDKPluginCloud
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x20
IMPLEMENTS: Hypergryph.SDK.IEventLogSDK
FIELDS:
  private           System.Object                   _cloudClientBridgeInstance  // 0x10
  private           System.Reflection.MethodInfo    _sendDataWithKeyMethod  // 0x18
METHODS:
  RVA=0x09DDE444  token=0x600004E  System.Boolean EventLogInitInstance(System.String appId, System.String regionTag)
  RVA=0x09DDE3A8  token=0x600004F  System.Boolean EventLogEvent(System.String appId, System.String name, System.String properties)
  RVA=0x09DDE4BC  token=0x6000050  System.Void SendDataWithKey(System.String key, System.String data, System.Action<System.String> callback)
  RVA=0x09DDEB68  token=0x6000051  System.Boolean appStartEvent(System.String appId, System.String channel1, System.String channel2, System.Boolean beat, System.String properties)
  RVA=0x09DDE77C  token=0x6000052  System.Boolean appStartEventV2(System.String channel1, System.String channel2, System.Boolean beat, System.String properties)
  RVA=0x09DDEF3C  token=0x6000053  System.Boolean characterLoginEvent(System.String appId, System.String characterId, System.String serverId, System.String properties)
  RVA=0x09DDEBB8  token=0x6000054  System.Boolean characterLoginEventV2(System.String characterId, System.String serverId, System.String properties)
  RVA=0x09DDF198  token=0x6000055  System.Void clearGlobalProperties(System.String appId)
  RVA=0x09DDEF8C  token=0x6000056  System.Void clearGlobalPropertiesV2()
  RVA=0x09DDF1EC  token=0x6000057  System.Boolean enableRealTimeSend(System.Boolean enable)
  RVA=0x09DDF7F0  token=0x6000058  System.Boolean eventTrack(System.String appId, System.String name, System.String properties)
  RVA=0x09DDF49C  token=0x6000059  System.Boolean eventTrackV2(System.String name, System.String properties)
  RVA=0x09DDF760  token=0x600005A  System.Boolean eventTrackV3(System.String appId, System.String name, System.String properties)
  RVA=0x09DDFB50  token=0x600005B  System.Void flush(System.String appId)
  RVA=0x09DDF944  token=0x600005C  System.Void flushV2()
  RVA=0x09DDFC0C  token=0x600005D  System.String getDeviceIdProperties(System.String appId)
  RVA=0x09DDFBA4  token=0x600005E  System.String getDeviceIdPropertiesV2()
  RVA=0x09DDFCDC  token=0x600005F  System.String getPresetProperties(System.String appId)
  RVA=0x09DDFC74  token=0x6000060  System.String getPresetPropertiesV2()
  RVA=0x09DDFDAC  token=0x6000061  System.String getStaticPresetProperties(System.String appId)
  RVA=0x09DDFD44  token=0x6000062  System.String getStaticPresetPropertiesV2()
  RVA=0x09DDFE14  token=0x6000063  System.Boolean init(System.String appId, System.String regionTag)
  RVA=0x09DE0160  token=0x6000064  System.Void pauseBeat(System.String appId)
  RVA=0x09DDFF54  token=0x6000065  System.Void pauseBeatV2()
  RVA=0x09DE03C0  token=0x6000066  System.Void resumeBeat(System.String appId)
  RVA=0x09DE01B4  token=0x6000067  System.Void resumeBeatV2()
  RVA=0x09DE0414  token=0x6000068  System.Boolean setEnvironment(System.String env)
  RVA=0x09DE0924  token=0x6000069  System.Boolean setGlobalProperties(System.String appId, System.String globalProperties)
  RVA=0x09DE069C  token=0x600006A  System.Boolean setGlobalPropertiesV2(System.String globalProperties)
  RVA=0x09DE0B80  token=0x600006B  System.Void unsetCharacter(System.String appId)
  RVA=0x09DE0974  token=0x600006C  System.Void unsetCharacterV2()
  RVA=0x09DE0E5C  token=0x600006D  System.Boolean unsetGlobalProperties(System.String appId, System.String propertyKeys)
  RVA=0x09DE0BD4  token=0x600006E  System.Boolean unsetGlobalPropertiesV2(System.String propertyKeys)
  RVA=0x09DE10B8  token=0x600006F  System.Void unsetUser(System.String appId)
  RVA=0x09DE0EAC  token=0x6000070  System.Void unsetUserV2()
  RVA=0x09DE13D0  token=0x6000071  System.Boolean userLoginEvent(System.String appId, System.String userId, System.String properties)
  RVA=0x09DE110C  token=0x6000072  System.Boolean userLoginEventV2(System.String userId, System.String properties)
  RVA=0x041E1670  token=0x6000073  System.Void .ctor()
END_CLASS

CLASS: Hypergryph.SDK.HGEventLogSDKPluginWindows
TYPE:  class
TOKEN: 0x2000009
SIZE:  0x10
IMPLEMENTS: Hypergryph.SDK.IEventLogSDK
FIELDS:
METHODS:
  RVA=0x09DE1E84  token=0x6000074  System.Boolean EventLogSetEnvironment(System.String env)
  RVA=0x040BA7E0  token=0x6000075  System.Boolean EventLogInitInstance(System.String appId, System.String regionTag)
  RVA=0x09DE1F3C  token=0x6000076  System.Boolean EventLogSetGlobalProperties(System.String appId, System.String globalProperties)
  RVA=0x09DE2128  token=0x6000077  System.Boolean EventLogUnsetGlobalProperties(System.String appId, System.String propertyKeys)
  RVA=0x09DE19C8  token=0x6000078  System.Boolean EventLogClearGlobalProperties(System.String appId)
  RVA=0x09DE1A9C  token=0x6000079  System.Boolean EventLogEvent(System.String appId, System.String name, System.String properties)
  RVA=0x09DE1514  token=0x600007A  System.Boolean EventLogAppStartEvent(System.String appId, System.String channel1, System.String channel2, System.Boolean beat, System.String properties)
  RVA=0x09DE1650  token=0x600007B  System.Boolean EventLogBeatPause(System.String appId)
  RVA=0x09DE1714  token=0x600007C  System.Boolean EventLogBeatResume(System.String appId)
  RVA=0x09DE22F8  token=0x600007D  System.Boolean EventLogUserLoginEvent(System.String appId, System.String userId, System.String properties)
  RVA=0x09DE2214  token=0x600007E  System.Boolean EventLogUnsetUser(System.String appId)
  RVA=0x09DE1890  token=0x600007F  System.Boolean EventLogCharacterLoginEvent(System.String appId, System.String characterId, System.String serverId, System.String properties)
  RVA=0x09DE2030  token=0x6000080  System.Boolean EventLogUnsetCharacter(System.String appId)
  RVA=0x09DE1D20  token=0x6000081  System.IntPtr EventLogGetPresetProperties(System.String appId)
  RVA=0x09DE1DEC  token=0x6000082  System.IntPtr EventLogGetStaticPresetProperties(System.String appId)
  RVA=0x09DE1C88  token=0x6000083  System.IntPtr EventLogGetDeviceIdProperties(System.String appId)
  RVA=0x09DE1A24  token=0x6000084  System.Boolean EventLogEnableRealTimeSend(System.Boolean enable)
  RVA=0x09DE1BA0  token=0x6000085  System.Void EventLogFlush(System.String appId)
  RVA=0x040BC600  token=0x6000086  System.Boolean EventLogFree(System.IntPtr buf)
  RVA=0x09DE208C  token=0x6000087  System.Boolean EventLogUnsetGlobalPropertiesV2(System.String property_keys)
  RVA=0x09DE1EE0  token=0x6000088  System.Boolean EventLogSetGlobalPropertiesV2(System.String global_properties)
  RVA=0x09DE1994  token=0x6000089  System.Void EventLogClearGlobalPropertiesV2()
  RVA=0x0445BAF0  token=0x600008A  System.Boolean EventLogEventV2(System.String name, System.String json_str)
  RVA=0x09DE1420  token=0x600008B  System.Boolean EventLogAppStartEventV2(System.String channel1, System.String channel2, System.Boolean beat, System.String json_str)
  RVA=0x09DE1624  token=0x600008C  System.Void EventLogBeatPauseV2()
  RVA=0x09DE16AC  token=0x600008D  System.Void EventLogBeatResumeV2()
  RVA=0x09DE2270  token=0x600008E  System.Boolean EventLogUserLoginEventV2(System.String user_id, System.String json_str)
  RVA=0x09DE21E8  token=0x600008F  System.Void EventLogUnsetUserV2()
  RVA=0x09DE17B0  token=0x6000090  System.Boolean EventLogCharacterLoginEventV2(System.String character_id, System.String server_id, System.String json_str)
  RVA=0x09DE1FFC  token=0x6000091  System.Void EventLogUnsetCharacterV2()
  RVA=0x040BC300  token=0x6000092  System.IntPtr EventLogGetPresetPropertiesV2()
  RVA=0x09DE1DB8  token=0x6000093  System.IntPtr EventLogGetStaticPresetPropertiesV2()
  RVA=0x09DE1C20  token=0x6000094  System.IntPtr EventLogGetDeviceIdPropertiesV2()
  RVA=0x09DE1B38  token=0x6000095  System.Void EventLogFlushV2()
  RVA=0x041E1670  token=0x6000096  System.Void .ctor()
  RVA=0x09DE2D34  token=0x6000097  System.Boolean setEnvironment(System.String env)
  RVA=0x040BC680  token=0x6000098  System.Boolean init(System.String appId, System.String regionTag)
  RVA=0x09DE2DBC  token=0x6000099  System.Boolean setGlobalProperties(System.String appId, System.String globalProperties)
  RVA=0x09DE2ED0  token=0x600009A  System.Boolean unsetGlobalProperties(System.String appId, System.String propertyKeys)
  RVA=0x09DE2560  token=0x600009B  System.Void clearGlobalProperties(System.String appId)
  RVA=0x09DE2668  token=0x600009C  System.Boolean eventTrack(System.String appId, System.String name, System.String properties)
  RVA=0x09DE2408  token=0x600009D  System.Boolean appStartEvent(System.String appId, System.String channel1, System.String channel2, System.Boolean beat, System.String properties)
  RVA=0x09DE2C34  token=0x600009E  System.Void pauseBeat(System.String appId)
  RVA=0x09DE2CB8  token=0x600009F  System.Void resumeBeat(System.String appId)
  RVA=0x09DE2FEC  token=0x60000A0  System.Boolean userLoginEvent(System.String appId, System.String userId, System.String properties)
  RVA=0x09DE2F5C  token=0x60000A1  System.Void unsetUser(System.String appId)
  RVA=0x09DE24C0  token=0x60000A2  System.Boolean characterLoginEvent(System.String appId, System.String characterId, System.String serverId, System.String properties)
  RVA=0x09DE2E48  token=0x60000A3  System.Void unsetCharacter(System.String appId)
  RVA=0x09DE2958  token=0x60000A4  System.String getPresetProperties(System.String appId)
  RVA=0x09DE2B38  token=0x60000A5  System.String getStaticPresetProperties(System.String appId)
  RVA=0x09DE2864  token=0x60000A6  System.String getDeviceIdProperties(System.String appId)
  RVA=0x09DE26FC  token=0x60000A7  System.Void flush(System.String appId)
  RVA=0x09DE25DC  token=0x60000A8  System.Boolean enableRealTimeSend(System.Boolean enable)
  RVA=0x09DE2DB0  token=0x60000A9  System.Boolean setGlobalPropertiesV2(System.String globalProperties)
  RVA=0x09DE2EC4  token=0x60000AA  System.Boolean unsetGlobalPropertiesV2(System.String propertyKeys)
  RVA=0x09DE2558  token=0x60000AB  System.Void clearGlobalPropertiesV2()
  RVA=0x05396E8C  token=0x60000AC  System.Boolean eventTrackV2(System.String name, System.String properties)
  RVA=0x09DE23D8  token=0x60000AD  System.Boolean appStartEventV2(System.String channel1, System.String channel2, System.Boolean beat, System.String properties)
  RVA=0x09DE2C2C  token=0x60000AE  System.Void pauseBeatV2()
  RVA=0x09DE2CB0  token=0x60000AF  System.Void resumeBeatV2()
  RVA=0x09DE2FD8  token=0x60000B0  System.Boolean userLoginEventV2(System.String userId, System.String properties)
  RVA=0x09DE2F54  token=0x60000B1  System.Void unsetUserV2()
  RVA=0x09DE24A8  token=0x60000B2  System.Boolean characterLoginEventV2(System.String characterId, System.String serverId, System.String properties)
  RVA=0x09DE2E40  token=0x60000B3  System.Void unsetCharacterV2()
  RVA=0x040BC270  token=0x60000B4  System.String getPresetPropertiesV2()
  RVA=0x09DE26F4  token=0x60000B5  System.Void flushV2()
  RVA=0x09DE2A4C  token=0x60000B6  System.String getStaticPresetPropertiesV2()
  RVA=0x09DE2778  token=0x60000B7  System.String getDeviceIdPropertiesV2()
  RVA=0x09DE2658  token=0x60000B8  System.Boolean eventTrackV3(System.String appId, System.String name, System.String properties)
END_CLASS

CLASS: Hypergryph.SDK.HGEventLogSDKVersion
TYPE:  static class
TOKEN: 0x200000A
SIZE:  0x10
FIELDS:
  public    static readonly System.String                   VERSION  // static @ 0x0
METHODS:
  RVA=0x04D50680  token=0x60000B9  System.Void .cctor()
END_CLASS

