// ========================================================
// Dumped by @desirepro
// Assembly: Beyond.Dlogger.Runtime.dll
// Classes:  23
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

CLASS: Options
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x18
FIELDS:
  public            System.Collections.Generic.Dictionary<Beyond.EnableLogType,System.Boolean>remoteSet  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000197  System.Void .ctor()
END_CLASS

CLASS: LogCallback
TYPE:  sealed class
TOKEN: 0x2000009
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x03DD5140  token=0x6000198  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x6000199  System.Void Invoke(Beyond.Log.FLogMessage& msg)
END_CLASS

CLASS: DebugContext
TYPE:  class
TOKEN: 0x200000A
SIZE:  0x20
FIELDS:
  public            Beyond.ELogLevel                logLevelMask  // 0x10
  public            System.String                   info  // 0x18
METHODS:
  RVA=0x04D861E0  token=0x600019A  System.Void .ctor()
END_CLASS

CLASS: DebugContextScope
TYPE:  sealed struct
TOKEN: 0x200000B
SIZE:  0x11
IMPLEMENTS: System.IDisposable
FIELDS:
METHODS:
  RVA=0x06373488  token=0x600019B  System.Void Dispose()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200000C
SIZE:  0x10
FIELDS:
  public    static readonly Beyond.DLogger.<>c              <>9  // static @ 0x0
  public    static  UnityEngine.LowLevel.PlayerLoopSystem.UpdateFunction<>9__432_0  // static @ 0x8
METHODS:
  RVA=0x04D4B8B0  token=0x600019C  System.Void .cctor()
  RVA=0x041E1670  token=0x600019D  System.Void .ctor()
  RVA=0x03234530  token=0x600019E  System.Void <_AddCustomGameLoop>b__432_0()
END_CLASS

CLASS: <>c__DisplayClass433_0
TYPE:  sealed class
TOKEN: 0x200000D
SIZE:  0x20
FIELDS:
  public            System.String                   categoryName  // 0x10
  public            System.String                   systemName  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600019F  System.Void .ctor()
  RVA=0x04294210  token=0x60001A0  System.Boolean <AddPlayerLoop>b__0(UnityEngine.LowLevel.PlayerLoopSystem s)
  RVA=0x063788FC  token=0x60001A1  System.Boolean <AddPlayerLoop>b__1(UnityEngine.LowLevel.PlayerLoopSystem h)
END_CLASS

CLASS: <>c__DisplayClass434_0
TYPE:  sealed class
TOKEN: 0x200000E
SIZE:  0x20
FIELDS:
  public            System.Type                     t  // 0x10
  public            System.Func<UnityEngine.LowLevel.PlayerLoopSystem,System.Boolean><>9__0  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60001A2  System.Void .ctor()
  RVA=0x04546160  token=0x60001A3  System.Boolean <CheckRegist>b__0(UnityEngine.LowLevel.PlayerLoopSystem x)
END_CLASS

CLASS: Beyond.ELogChannel
TYPE:  sealed struct
TOKEN: 0x2000002
SIZE:  0x11
FIELDS:
  public            System.Byte                     value__  // 0x10
  public    static  Beyond.ELogChannel              None  // const
  public    static  Beyond.ELogChannel              Res  // const
  public    static  Beyond.ELogChannel              Cfg  // const
  public    static  Beyond.ELogChannel              Battle  // const
  public    static  Beyond.ELogChannel              GamePlay  // const
  public    static  Beyond.ELogChannel              UI  // const
  public    static  Beyond.ELogChannel              Effect  // const
  public    static  Beyond.ELogChannel              Audio  // const
  public    static  Beyond.ELogChannel              Network  // const
  public    static  Beyond.ELogChannel              Dialog  // const
  public    static  Beyond.ELogChannel              Cutscene  // const
  public    static  Beyond.ELogChannel              HGRP  // const
  public    static  Beyond.ELogChannel              Build  // const
  public    static  Beyond.ELogChannel              Camera  // const
  public    static  Beyond.ELogChannel              Animation  // const
  public    static  Beyond.ELogChannel              Scene  // const
  public    static  Beyond.ELogChannel              Action  // const
  public    static  Beyond.ELogChannel              Tool  // const
  public    static  Beyond.ELogChannel              Inventory  // const
  public    static  Beyond.ELogChannel              Lua  // const
  public    static  Beyond.ELogChannel              Debug  // const
  public    static  Beyond.ELogChannel              LevelScript  // const
  public    static  Beyond.ELogChannel              Movement  // const
  public    static  Beyond.ELogChannel              Interactive  // const
  public    static  Beyond.ELogChannel              Common  // const
  public    static  Beyond.ELogChannel              Editor  // const
  public    static  Beyond.ELogChannel              Streaming  // const
  public    static  Beyond.ELogChannel              Factory  // const
  public    static  Beyond.ELogChannel              Utils  // const
  public    static  Beyond.ELogChannel              NPC  // const
  public    static  Beyond.ELogChannel              VIDEO  // const
  public    static  Beyond.ELogChannel              I18N  // const
  public    static  Beyond.ELogChannel              Guide  // const
  public    static  Beyond.ELogChannel              HGEventLog  // const
  public    static  Beyond.ELogChannel              RedDot  // const
  public    static  Beyond.ELogChannel              NetworkCore  // const
  public    static  Beyond.ELogChannel              ExportScene  // const
  public    static  Beyond.ELogChannel              ExportSceneValidation  // const
  public    static  Beyond.ELogChannel              HGNetLog  // const
  public    static  Beyond.ELogChannel              IFix  // const
  public    static  Beyond.ELogChannel              SDK  // const
  public    static  Beyond.ELogChannel              VFS  // const
  public    static  Beyond.ELogChannel              Preload  // const
  public    static  Beyond.ELogChannel              Voice  // const
  public    static  Beyond.ELogChannel              CmdExport  // const
  public    static  Beyond.ELogChannel              BossBattler  // const
  public    static  Beyond.ELogChannel              ActionDetail  // const
  public    static  Beyond.ELogChannel              ActionExecutor  // const
  public    static  Beyond.ELogChannel              GameEventManager  // const
  public    static  Beyond.ELogChannel              ClientData  // const
  public    static  Beyond.ELogChannel              P4  // const
  public    static  Beyond.ELogChannel              NavMesh  // const
  public    static  Beyond.ELogChannel              FMV  // const
  public    static  Beyond.ELogChannel              Scope  // const
  public    static  Beyond.ELogChannel              MiniGame  // const
  public    static  Beyond.ELogChannel              Spawner  // const
  public    static  Beyond.ELogChannel              AI  // const
  public    static  Beyond.ELogChannel              Validation  // const
  public    static  Beyond.ELogChannel              Luna  // const
  public    static  Beyond.ELogChannel              LevelScriptTask  // const
  public    static  Beyond.ELogChannel              Dungeon  // const
  public    static  Beyond.ELogChannel              WorldLifeSpan  // const
  public    static  Beyond.ELogChannel              SNS  // const
  public    static  Beyond.ELogChannel              HttpGm  // const
  public    static  Beyond.ELogChannel              LevelEditor  // const
  public    static  Beyond.ELogChannel              TowerDefense  // const
  public    static  Beyond.ELogChannel              Spaceship  // const
  public    static  Beyond.ELogChannel              Physics  // const
  public    static  Beyond.ELogChannel              Weapon  // const
  public    static  Beyond.ELogChannel              DataManager  // const
  public    static  Beyond.ELogChannel              TempFile  // const
  public    static  Beyond.ELogChannel              Quality  // const
  public    static  Beyond.ELogChannel              Map  // const
  public    static  Beyond.ELogChannel              Bark  // const
  public    static  Beyond.ELogChannel              Entity  // const
  public    static  Beyond.ELogChannel              Squad  // const
  public    static  Beyond.ELogChannel              LevelSeq  // const
  public    static  Beyond.ELogChannel              Mission  // const
  public    static  Beyond.ELogChannel              Tracking  // const
  public    static  Beyond.ELogChannel              Friend  // const
  public    static  Beyond.ELogChannel              Lsm  // const
  public    static  Beyond.ELogChannel              ConditionRuntime  // const
  public    static  Beyond.ELogChannel              DynamicScene  // const
  public    static  Beyond.ELogChannel              ModelView  // const
  public    static  Beyond.ELogChannel              WaterDrone  // const
  public    static  Beyond.ELogChannel              HelloWorld  // const
  public    static  Beyond.ELogChannel              MapRegion  // const
  public    static  Beyond.ELogChannel              GameSetting  // const
  public    static  Beyond.ELogChannel              LevelLoader  // const
  public    static  Beyond.ELogChannel              PS5  // const
  public    static  Beyond.ELogChannel              Activity  // const
  public    static  Beyond.ELogChannel              Login  // const
  public    static  Beyond.ELogChannel              SkeletalMorph  // const
  public    static  Beyond.ELogChannel              Gm  // const
  public    static  Beyond.ELogChannel              AbilityAction  // const
  public    static  Beyond.ELogChannel              AbilityActionEssential  // const
  public    static  Beyond.ELogChannel              SystemPoi  // const
  public    static  Beyond.ELogChannel              CharPerform  // const
  public    static  Beyond.ELogChannel              BackgroundNotification  // const
  public    static  Beyond.ELogChannel              CloudGame  // const
  public    static  Beyond.ELogChannel              GlobalTag  // const
  public    static  Beyond.ELogChannel              MoveSync  // const
  public    static  Beyond.ELogChannel              SubVersionControl  // const
  public    static  Beyond.ELogChannel              SimulationTraining  // const
  public    static  Beyond.ELogChannel              BlackScreen  // const
  public    static  Beyond.ELogChannel              Max  // const
METHODS:
END_CLASS

CLASS: Beyond.ELogLevel
TYPE:  sealed struct
TOKEN: 0x2000003
SIZE:  0x11
FIELDS:
  public            System.Byte                     value__  // 0x10
  public    static  Beyond.ELogLevel                None  // const
  public    static  Beyond.ELogLevel                Info  // const
  public    static  Beyond.ELogLevel                Warning  // const
  public    static  Beyond.ELogLevel                Error  // const
  public    static  Beyond.ELogLevel                Critical  // const
  public    static  Beyond.ELogLevel                Important  // const
  public    static  Beyond.ELogLevel                Exception  // const
  public    static  Beyond.ELogLevel                All  // const
METHODS:
END_CLASS

CLASS: Beyond.EColorTag
TYPE:  sealed struct
TOKEN: 0x2000004
SIZE:  0x11
FIELDS:
  public            System.Byte                     value__  // 0x10
  public    static  Beyond.EColorTag                None  // const
  public    static  Beyond.EColorTag                Red  // const
  public    static  Beyond.EColorTag                Yellow  // const
  public    static  Beyond.EColorTag                Green  // const
  public    static  Beyond.EColorTag                Blue  // const
  public    static  Beyond.EColorTag                White  // const
  public    static  Beyond.EColorTag                Black  // const
  public    static  Beyond.EColorTag                Grey  // const
  public    static  Beyond.EColorTag                Cyan  // const
  public    static  Beyond.EColorTag                Magenta  // const
  public    static  Beyond.EColorTag                Critical  // const
METHODS:
END_CLASS

CLASS: Beyond.LogDefine
TYPE:  static class
TOKEN: 0x2000005
SIZE:  0x10
FIELDS:
  private   static  System.Collections.Generic.Dictionary<System.Int32,System.String>s_logNameCacheDict  // static @ 0x0
METHODS:
  RVA=0x063785C0  token=0x6000001  System.String ConvertColorTagToName(Beyond.EColorTag colorTag)
  RVA=0x0313C440  token=0x6000002  System.String ConvertLogChannelToName(Beyond.ELogChannel logChannel)
  RVA=0x04D17A30  token=0x6000003  System.Void .cctor()
END_CLASS

CLASS: Beyond.EnableLogType
TYPE:  sealed struct
TOKEN: 0x2000006
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.EnableLogType            DevOnly  // const
  public    static  Beyond.EnableLogType            Release  // const
  public    static  Beyond.EnableLogType            Login  // const
  public    static  Beyond.EnableLogType            EntitySpawn  // const
  public    static  Beyond.EnableLogType            NpcSpawn  // const
  public    static  Beyond.EnableLogType            Proxy  // const
  public    static  Beyond.EnableLogType            NpcAction  // const
  public    static  Beyond.EnableLogType            GuideState  // const
  public    static  Beyond.EnableLogType            MainCharacter  // const
  public    static  Beyond.EnableLogType            Perform  // const
  public    static  Beyond.EnableLogType            LevelLoader  // const
  public    static  Beyond.EnableLogType            Dialog  // const
  public    static  Beyond.EnableLogType            MainHudActionQueue  // const
  public    static  Beyond.EnableLogType            AirWall  // const
  public    static  Beyond.EnableLogType            Gm  // const
  public    static  Beyond.EnableLogType            I18N  // const
  public    static  Beyond.EnableLogType            Count  // const
METHODS:
END_CLASS

CLASS: Beyond.DLogger
TYPE:  static class
TOKEN: 0x2000007
SIZE:  0x10
FIELDS:
  private   static  System.Boolean[]                s_remoteSet  // static @ 0x0
  private   static  Beyond.Log.ILogger              s_consoleLogger  // static @ 0x8
  private   static  Beyond.Log.SceneViewMessageLoggers_sceneViewLogger  // static @ 0x10
  private   static  Beyond.Log.UnionLogger          s_defaultLogger  // static @ 0x18
  private   static  Beyond.ELogLevel                ms_enabledLogLevel  // static @ 0x20
  private   static  Beyond.ELogLevel[]              ms_enabledLogChannel  // static @ 0x28
  private   static  Beyond.DLogger.LogCallback      ms_logCriticalAndExceptionCallbackHandler  // static @ 0x30
  private   static  Beyond.DLogger.LogCallback      ms_onBeforeLogError  // static @ 0x38
  private   static  Beyond.DLogger.LogCallback      ms_onBeforeLogCriticalAndException  // static @ 0x40
  public    static  System.Func<System.String>      s_getLuaStackTraceHandler  // static @ 0x48
  private   static  System.Collections.Generic.Stack<Beyond.DLogger.DebugContext>s_debugContextPool  // static @ 0x50
  private   static  System.Collections.Generic.List<Beyond.DLogger.DebugContext>s_debugContextList  // static @ 0x58
  private   static  System.Int32                    <FrameCountThreadSafe>k__BackingField  // static @ 0x60
  private   static  System.Int32                    <LogPing>k__BackingField  // static @ 0x64
  private   static  UnityEngine.Vector3             <LogPlayerPos>k__BackingField  // static @ 0x68
  public    static  System.Int32                    ExceptionCount  // static @ 0x74
  public    static  System.Int32                    CriticalCount  // static @ 0x78
  public    static  System.Int32                    ErrorCount  // static @ 0x7c
  public    static  System.Int32                    ImportantCount  // static @ 0x80
  public    static  System.Int32                    WarningCount  // static @ 0x84
  public    static  System.Int32                    InfoCount  // static @ 0x88
  public    static  System.Int32                    PauseCount  // static @ 0x8c
  public    static  System.Int32                    ReturnLoginCount  // static @ 0x90
PROPERTIES:
  s_enabledLogChannel  get=0x0332EE30
  FrameCountThreadSafe  get=0x06372F94  set=0x0637338C
  LogPing  get=0x06372FE4  set=0x063733E0
  LogPlayerPos  get=0x06373034  set=0x06373434
EVENTS:
  s_logCriticalAndExceptionMessageReceived  add=add_s_logCriticalAndExceptionMessageReceived  remove=remove_s_logCriticalAndExceptionMessageReceived
  s_onBeforeLogError  add=add_s_onBeforeLogError  remove=remove_s_onBeforeLogError
  s_onBeforeLogCriticalAndException  add=add_s_onBeforeLogCriticalAndException  remove=remove_s_onBeforeLogCriticalAndException
METHODS:
  RVA=0x045E16D0  token=0x6000004  System.Void SetImportantLogConfig(Beyond.DLogger.Options options)
  RVA=0x04CDEDC0  token=0x6000008  System.Void _RunOnStart()
  RVA=0x0313C580  token=0x6000009  System.Void UpdateLogCount(Beyond.Log.FLogMessage& msg)
  RVA=0x03E3E1B0  token=0x600000E  System.Void SetServerLogger(Beyond.Log.ILogger logger)
  RVA=0x063724A4  token=0x600000F  System.Void SetLogLevel(Beyond.ELogLevel level)
  RVA=0x0332EEC0  token=0x6000010  System.Boolean CheckLogLevel(Beyond.ELogLevel level)
  RVA=0x063723E4  token=0x6000011  System.Void SetLogChannelEnabled(Beyond.ELogChannel channel, Beyond.ELogLevel level, System.Boolean val)
  RVA=0x0332ED70  token=0x6000012  System.Boolean CheckLogChannelEnabled(Beyond.ELogChannel channel, Beyond.ELogLevel level)
  RVA=0x06371D3C  token=0x6000013  System.Void Log(System.String message)
  RVA=-1  // generic def  token=0x6000014  System.Void Log(System.String message, T param1)
  RVA=-1  // generic def  token=0x6000015  System.Void Log(System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x6000016  System.Void Log(System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x6000017  System.Void Log(System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x6000018  System.Void Log(System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x6000019  System.Void Log(System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x600001A  System.Void Log(System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x06371CA4  token=0x600001B  System.Void Log(Beyond.ELogChannel channel, System.String message)
  RVA=-1  // generic def  token=0x600001C  System.Void Log(Beyond.ELogChannel channel, System.String message, T param1)
  RVA=-1  // generic def  token=0x600001D  System.Void Log(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x600001E  System.Void Log(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x600001F  System.Void Log(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x6000020  System.Void Log(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x6000021  System.Void Log(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x6000022  System.Void Log(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x063721F8  token=0x6000023  System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message)
  RVA=-1  // generic def  token=0x6000024  System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T param1)
  RVA=-1  // generic def  token=0x6000025  System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x6000026  System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x6000027  System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x6000028  System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x6000029  System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x600002A  System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x06372164  token=0x600002B  System.Void Log(UnityEngine.Object context, System.String message)
  RVA=-1  // generic def  token=0x600002C  System.Void Log(UnityEngine.Object context, System.String message, T param1)
  RVA=-1  // generic def  token=0x600002D  System.Void Log(UnityEngine.Object context, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x600002E  System.Void Log(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x600002F  System.Void Log(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x6000030  System.Void Log(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x6000031  System.Void Log(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=0x06372038  token=0x6000032  System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message)
  RVA=-1  // generic def  token=0x6000033  System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T param1)
  RVA=-1  // generic def  token=0x6000034  System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x6000035  System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x6000036  System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x6000037  System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x6000038  System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x6000039  System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x063700B0  token=0x600003A  System.Void LogEditorAlwaysPrint(System.String message)
  RVA=-1  // generic def  token=0x600003B  System.Void LogEditorAlwaysPrint(System.String message, T param1)
  RVA=-1  // generic def  token=0x600003C  System.Void LogEditorAlwaysPrint(System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x600003D  System.Void LogEditorAlwaysPrint(System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x600003E  System.Void LogEditorAlwaysPrint(System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x600003F  System.Void LogEditorAlwaysPrint(System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x6000040  System.Void LogEditorAlwaysPrint(System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x6000041  System.Void LogEditorAlwaysPrint(System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x06370010  token=0x6000042  System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, System.String message)
  RVA=-1  // generic def  token=0x6000043  System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, System.String message, T param1)
  RVA=-1  // generic def  token=0x6000044  System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x6000045  System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x6000046  System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x6000047  System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x6000048  System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x6000049  System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x06370164  token=0x600004A  System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message)
  RVA=-1  // generic def  token=0x600004B  System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T param1)
  RVA=-1  // generic def  token=0x600004C  System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x600004D  System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x600004E  System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x600004F  System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x6000050  System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x6000051  System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x06370060  token=0x6000052  System.Void LogEditorAlwaysPrint(UnityEngine.Object context, System.String message)
  RVA=-1  // generic def  token=0x6000053  System.Void LogEditorAlwaysPrint(UnityEngine.Object context, System.String message, T param1)
  RVA=-1  // generic def  token=0x6000054  System.Void LogEditorAlwaysPrint(UnityEngine.Object context, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x6000055  System.Void LogEditorAlwaysPrint(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x6000056  System.Void LogEditorAlwaysPrint(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x6000057  System.Void LogEditorAlwaysPrint(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x6000058  System.Void LogEditorAlwaysPrint(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=0x063700F0  token=0x6000059  System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message)
  RVA=-1  // generic def  token=0x600005A  System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T param1)
  RVA=-1  // generic def  token=0x600005B  System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x600005C  System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x600005D  System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x600005E  System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x600005F  System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x6000060  System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x06371C1C  token=0x6000061  System.Void LogWarning(System.String message)
  RVA=0x0637190C  token=0x6000062  System.Void LogWarning(UnityEngine.Object context, System.String message)
  RVA=-1  // generic def  token=0x6000063  System.Void LogWarning(System.String message, T param1)
  RVA=-1  // generic def  token=0x6000064  System.Void LogWarning(System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x6000065  System.Void LogWarning(System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x6000066  System.Void LogWarning(System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x6000067  System.Void LogWarning(System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x6000068  System.Void LogWarning(System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x6000069  System.Void LogWarning(System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x06371A5C  token=0x600006A  System.Void LogWarning(Beyond.ELogChannel channel, System.String message)
  RVA=-1  // generic def  token=0x600006B  System.Void LogWarning(Beyond.ELogChannel channel, System.String message, T param1)
  RVA=-1  // generic def  token=0x600006C  System.Void LogWarning(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x600006D  System.Void LogWarning(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x600006E  System.Void LogWarning(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x600006F  System.Void LogWarning(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x6000070  System.Void LogWarning(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x6000071  System.Void LogWarning(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x06371B70  token=0x6000072  System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message)
  RVA=-1  // generic def  token=0x6000073  System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T param1)
  RVA=-1  // generic def  token=0x6000074  System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x6000075  System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x6000076  System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x6000077  System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x6000078  System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x6000079  System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x063719A0  token=0x600007A  System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message)
  RVA=-1  // generic def  token=0x600007B  System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T param1)
  RVA=-1  // generic def  token=0x600007C  System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x600007D  System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x600007E  System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x600007F  System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x6000080  System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x6000081  System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x04D096D0  token=0x6000082  System.Void LogError(System.String message)
  RVA=0x063707E0  token=0x6000083  System.Void LogErrorWithLuaStack(Beyond.ELogChannel channel, System.String message)
  RVA=-1  // generic def  token=0x6000084  System.Void LogErrorWithLuaStack(Beyond.ELogChannel channel, System.String message, T1 param1)
  RVA=-1  // generic def  token=0x6000085  System.Void LogErrorWithLuaStack(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x6000086  System.Void LogErrorWithLuaStack(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3)
  RVA=0x06370764  token=0x6000087  System.Void LogErrorWithLuaStack(System.String message)
  RVA=-1  // generic def  token=0x6000088  System.Void LogError(System.String message, T param1)
  RVA=-1  // generic def  token=0x6000089  System.Void LogError(System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x600008A  System.Void LogError(System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x600008B  System.Void LogError(System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x600008C  System.Void LogError(System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x600008D  System.Void LogError(System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x600008E  System.Void LogError(System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x0374C520  token=0x600008F  System.Void LogError(Beyond.ELogChannel channel, System.String message)
  RVA=-1  // generic def  token=0x6000090  System.Void LogError(Beyond.ELogChannel channel, System.String message, T param1)
  RVA=-1  // generic def  token=0x6000091  System.Void LogError(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x6000092  System.Void LogError(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x6000093  System.Void LogError(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x6000094  System.Void LogError(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x6000095  System.Void LogError(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x6000096  System.Void LogError(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x06370C50  token=0x6000097  System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message)
  RVA=-1  // generic def  token=0x6000098  System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T param1)
  RVA=-1  // generic def  token=0x6000099  System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x600009A  System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x600009B  System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x600009C  System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x600009D  System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x600009E  System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x063709E0  token=0x600009F  System.Void LogError(UnityEngine.Object context, System.String message)
  RVA=-1  // generic def  token=0x60000A0  System.Void LogError(UnityEngine.Object context, System.String message, T param1)
  RVA=-1  // generic def  token=0x60000A1  System.Void LogError(UnityEngine.Object context, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x60000A2  System.Void LogError(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x60000A3  System.Void LogError(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x60000A4  System.Void LogError(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x60000A5  System.Void LogError(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x60000A6  System.Void LogError(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x06370B80  token=0x60000A7  System.Void LogError(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message)
  RVA=-1  // generic def  token=0x60000A8  System.Void LogError(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, T param1)
  RVA=-1  // generic def  token=0x60000A9  System.Void LogError(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x60000AA  System.Void LogError(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x60000AB  System.Void LogError(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x60000AC  System.Void LogError(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x60000AD  System.Void LogError(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x60000AE  System.Void LogError(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x06370A9C  token=0x60000AF  System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message)
  RVA=-1  // generic def  token=0x60000B0  System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T param1)
  RVA=-1  // generic def  token=0x60000B1  System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x60000B2  System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x60000B3  System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x60000B4  System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x60000B5  System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x60000B6  System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x0636EFE0  token=0x60000B7  System.Void IFixLogError(System.String message)
  RVA=0x0636EAA4  token=0x60000B8  System.Void IFixLogError(System.String message, System.String param1)
  RVA=0x0636EED0  token=0x60000B9  System.Void IFixLogError(System.String message, System.String param1, System.String param2)
  RVA=0x0636F6C4  token=0x60000BA  System.Void IFixLogError(System.String message, System.String param1, System.String param2, System.String param3)
  RVA=0x0636F5A0  token=0x60000BB  System.Void IFixLogError(System.String message, System.String param1, System.String param2, System.String param3, System.String param4)
  RVA=0x0636F1A0  token=0x60000BC  System.Void IFixLogError(System.String message, System.String param1, System.String param2, System.String param3, System.String param4, System.String param5)
  RVA=0x0636E95C  token=0x60000BD  System.Void IFixLogError(System.String message, System.String param1, System.String param2, System.String param3, System.String param4, System.String param5, System.String param6)
  RVA=0x0636EBAC  token=0x60000BE  System.Void IFixLogError(System.String message, System.String param1, System.String param2, System.String param3, System.String param4, System.String param5, System.String param6, System.String param7)
  RVA=0x0636EE14  token=0x60000BF  System.Void IFixLogError(Beyond.ELogChannel channel, System.String message)
  RVA=0x0636ED08  token=0x60000C0  System.Void IFixLogError(Beyond.ELogChannel channel, System.String message, System.String param1)
  RVA=0x0636F08C  token=0x60000C1  System.Void IFixLogError(Beyond.ELogChannel channel, System.String message, System.String param1, System.String param2)
  RVA=0x0636F918  token=0x60000C2  System.Void IFixLogError(Beyond.ELogChannel channel, System.String message, System.String param1, System.String param2, System.String param3)
  RVA=0x0636F7DC  token=0x60000C3  System.Void IFixLogError(Beyond.ELogChannel channel, System.String message, System.String param1, System.String param2, System.String param3, System.String param4)
  RVA=0x0636F2DC  token=0x60000C4  System.Void IFixLogError(Beyond.ELogChannel channel, System.String message, System.String param1, System.String param2, System.String param3, System.String param4, System.String param5)
  RVA=0x0636FA44  token=0x60000C5  System.Void IFixLogError(Beyond.ELogChannel channel, System.String message, System.String param1, System.String param2, System.String param3, System.String param4, System.String param5, System.String param6)
  RVA=0x0636F42C  token=0x60000C6  System.Void IFixLogError(Beyond.ELogChannel channel, System.String message, System.String param1, System.String param2, System.String param3, System.String param4, System.String param5, System.String param6, System.String param7)
  RVA=0x06370604  token=0x60000C7  System.Void LogErrorWithFilter(System.String message, System.String filter)
  RVA=-1  // generic def  token=0x60000C8  System.Void LogErrorWithFilter(System.String message, System.String filter, T param1)
  RVA=-1  // generic def  token=0x60000C9  System.Void LogErrorWithFilter(System.String message, System.String filter, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x60000CA  System.Void LogErrorWithFilter(System.String message, System.String filter, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x60000CB  System.Void LogErrorWithFilter(System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x60000CC  System.Void LogErrorWithFilter(System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x60000CD  System.Void LogErrorWithFilter(System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x60000CE  System.Void LogErrorWithFilter(System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x0637055C  token=0x60000CF  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, System.String message, System.String filter)
  RVA=-1  // generic def  token=0x60000D0  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, System.String message, System.String filter, T param1)
  RVA=-1  // generic def  token=0x60000D1  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, System.String message, System.String filter, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x60000D2  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, System.String message, System.String filter, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x60000D3  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x60000D4  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x60000D5  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x60000D6  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x06370384  token=0x60000D7  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, System.String filter)
  RVA=-1  // generic def  token=0x60000D8  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, System.String filter, T param1)
  RVA=-1  // generic def  token=0x60000D9  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, System.String filter, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x60000DA  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, System.String filter, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x60000DB  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x60000DC  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x60000DD  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x60000DE  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x063702DC  token=0x60000DF  System.Void LogErrorWithFilter(UnityEngine.Object context, System.String message, System.String filter)
  RVA=-1  // generic def  token=0x60000E0  System.Void LogErrorWithFilter(UnityEngine.Object context, System.String message, System.String filter, T param1)
  RVA=-1  // generic def  token=0x60000E1  System.Void LogErrorWithFilter(UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x60000E2  System.Void LogErrorWithFilter(UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x60000E3  System.Void LogErrorWithFilter(UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x60000E4  System.Void LogErrorWithFilter(UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x60000E5  System.Void LogErrorWithFilter(UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x60000E6  System.Void LogErrorWithFilter(UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x06370440  token=0x60000E7  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, System.String filter)
  RVA=-1  // generic def  token=0x60000E8  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, System.String filter, T param1)
  RVA=-1  // generic def  token=0x60000E9  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x60000EA  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x60000EB  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x60000EC  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x60000ED  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x60000EE  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x06370698  token=0x60000EF  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter)
  RVA=-1  // generic def  token=0x60000F0  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter, T param1)
  RVA=-1  // generic def  token=0x60000F1  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x60000F2  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x60000F3  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x60000F4  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x60000F5  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x60000F6  System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x0636FF64  token=0x60000F7  System.Void LogCritical(System.String message)
  RVA=-1  // generic def  token=0x60000F8  System.Void LogCritical(System.String message, T param1)
  RVA=-1  // generic def  token=0x60000F9  System.Void LogCritical(System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x60000FA  System.Void LogCritical(System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x60000FB  System.Void LogCritical(System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x60000FC  System.Void LogCritical(System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x60000FD  System.Void LogCritical(System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x60000FE  System.Void LogCritical(System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x0636FDD8  token=0x60000FF  System.Void LogCritical(UnityEngine.Object context, System.String message)
  RVA=-1  // generic def  token=0x6000100  System.Void LogCritical(UnityEngine.Object context, System.String message, T param1)
  RVA=-1  // generic def  token=0x6000101  System.Void LogCritical(UnityEngine.Object context, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x6000102  System.Void LogCritical(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x6000103  System.Void LogCritical(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x6000104  System.Void LogCritical(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x6000105  System.Void LogCritical(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x6000106  System.Void LogCritical(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x0636FCC0  token=0x6000107  System.Void LogCritical(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message)
  RVA=0x0421C2E0  token=0x6000108  System.Void LogCritical(Beyond.ELogChannel channel, System.String message)
  RVA=-1  // generic def  token=0x6000109  System.Void LogCritical(Beyond.ELogChannel channel, System.String message, T param1)
  RVA=-1  // generic def  token=0x600010A  System.Void LogCritical(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x600010B  System.Void LogCritical(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x600010C  System.Void LogCritical(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x600010D  System.Void LogCritical(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x600010E  System.Void LogCritical(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x600010F  System.Void LogCritical(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x0636FE94  token=0x6000110  System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message)
  RVA=-1  // generic def  token=0x6000111  System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T param1)
  RVA=-1  // generic def  token=0x6000112  System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x6000113  System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x6000114  System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x6000115  System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x6000116  System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x6000117  System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x0636FBA0  token=0x6000118  System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message)
  RVA=-1  // generic def  token=0x6000119  System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T param1)
  RVA=-1  // generic def  token=0x600011A  System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x600011B  System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x600011C  System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x600011D  System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x600011E  System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x600011F  System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=-1  // generic def  token=0x6000120  System.Void LogCritical(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, T param1)
  RVA=-1  // generic def  token=0x6000121  System.Void LogCritical(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x6000122  System.Void LogCritical(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x6000123  System.Void LogCritical(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x6000124  System.Void LogCritical(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x6000125  System.Void LogCritical(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x6000126  System.Void LogCritical(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x06371180  token=0x6000127  System.Void LogException(System.Exception e)
  RVA=0x06371204  token=0x6000128  System.Void LogException(UnityEngine.Object context, System.Exception e)
  RVA=0x06370EA8  token=0x6000129  System.Void LogException(Beyond.ELogChannel channel, System.Exception e)
  RVA=0x063710DC  token=0x600012A  System.Void LogException(Beyond.ELogChannel channel, Beyond.EColorTag color, System.Exception e)
  RVA=0x06370F38  token=0x600012B  System.Void LogException(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.Exception e)
  RVA=0x03AF76E0  token=0x600012C  System.Void LogImportant(Beyond.EnableLogType enableLogType, System.String message)
  RVA=-1  // generic def  token=0x600012D  System.Void LogImportant(Beyond.EnableLogType enableLogType, System.String message, T param1)
  RVA=-1  // generic def  token=0x600012E  System.Void LogImportant(Beyond.EnableLogType enableLogType, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x600012F  System.Void LogImportant(Beyond.EnableLogType enableLogType, System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x6000130  System.Void LogImportant(Beyond.EnableLogType enableLogType, System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x6000131  System.Void LogImportant(Beyond.EnableLogType enableLogType, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x6000132  System.Void LogImportant(Beyond.EnableLogType enableLogType, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x6000133  System.Void LogImportant(Beyond.EnableLogType enableLogType, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x0332DDD0  token=0x6000134  System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, System.String message)
  RVA=-1  // generic def  token=0x6000135  System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, System.String message, T param1)
  RVA=-1  // generic def  token=0x6000136  System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x6000137  System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x6000138  System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x6000139  System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x600013A  System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x600013B  System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x06371294  token=0x600013C  System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message)
  RVA=-1  // generic def  token=0x600013D  System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T param1)
  RVA=-1  // generic def  token=0x600013E  System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x600013F  System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x6000140  System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x6000141  System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x6000142  System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x6000143  System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x0637134C  token=0x6000144  System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message)
  RVA=-1  // generic def  token=0x6000145  System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T param1)
  RVA=-1  // generic def  token=0x6000146  System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x6000147  System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x6000148  System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=-1  // generic def  token=0x6000149  System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
  RVA=-1  // generic def  token=0x600014A  System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
  RVA=-1  // generic def  token=0x600014B  System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x06372C14  token=0x600014C  System.Void _Log(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter)
  RVA=-1  // generic def  token=0x600014D  System.Void _Log(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x06372A74  token=0x600014E  System.Void _LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter)
  RVA=-1  // generic def  token=0x600014F  System.Void _LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x06372B44  token=0x6000150  System.Void _LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter)
  RVA=-1  // generic def  token=0x6000151  System.Void _LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x0332E670  token=0x6000152  System.Void _LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter)
  RVA=-1  // generic def  token=0x6000153  System.Void _LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x0332DE50  token=0x6000154  System.Void _LogImportant(Beyond.ELogChannel channel, Beyond.EnableLogType enableLogType, Beyond.EColorTag color, UnityEngine.Object context, System.String message)
  RVA=-1  // generic def  token=0x6000155  System.Void _LogImportant(Beyond.ELogChannel channel, Beyond.EnableLogType enableLogType, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x0421C230  token=0x6000156  System.Void _LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter)
  RVA=-1  // generic def  token=0x6000157  System.Void _LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x0332E720  token=0x6000158  System.Void _ApplyLog(Beyond.ELogChannel channel, Beyond.ELogLevel level, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter)
  RVA=0x0332ECB0  token=0x6000159  System.Void _LogWithFilter(Beyond.ELogChannel channel, Beyond.ELogLevel level, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter)
  RVA=-1  // generic def  token=0x600015A  System.Void _ApplyLog(Beyond.ELogChannel channel, Beyond.ELogLevel level, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=-1  // generic def  token=0x600015B  System.Void _LogWithFilter(Beyond.ELogChannel channel, Beyond.ELogLevel level, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
  RVA=0x06371414  token=0x600015C  System.Void LogToConsole(Beyond.ELogChannel channel, System.String message)
  RVA=-1  // generic def  token=0x600015D  System.Void LogToConsole(Beyond.ELogChannel channel, System.String message, T param1)
  RVA=-1  // generic def  token=0x600015E  System.Void LogToConsole(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2)
  RVA=0x063701C8  token=0x600015F  System.Void LogErrorToConsole(Beyond.ELogChannel channel, System.String message)
  RVA=-1  // generic def  token=0x6000160  System.Void LogErrorToConsole(Beyond.ELogChannel channel, System.String message, T param1)
  RVA=-1  // generic def  token=0x6000161  System.Void LogErrorToConsole(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x6000162  System.Void LogErrorToConsole(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3)
  RVA=-1  // generic def  token=0x6000163  System.Void LogErrorToConsole(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4)
  RVA=0x06370D20  token=0x6000164  System.Void LogExceptionToConsole(Beyond.ELogChannel channel, System.Exception e)
  RVA=0x06372520  token=0x6000165  System.Void SetLogToSceneViewEnabled(System.Boolean enabled)
  RVA=0x0637168C  token=0x6000166  System.Void LogToSceneViewImpl(Beyond.ELogLevel logLevel, Beyond.ELogChannel channel, System.String message, System.Single lifeTime)
  RVA=0x0636E8CC  token=0x6000167  System.Void ClearSceneViewLogs(System.String message)
  RVA=0x0636E83C  token=0x6000168  System.Void ClearSceneViewLogsRegex(System.String message)
  RVA=0x063718A8  token=0x6000169  System.Void LogToSceneView(Beyond.ELogChannel channel, System.Single lifeTime, System.String message)
  RVA=0x06371858  token=0x600016A  System.Void LogToSceneView(Beyond.ELogChannel channel, System.String message)
  RVA=-1  // generic def  token=0x600016B  System.Void LogToSceneView(Beyond.ELogChannel channel, System.String message, T param1)
  RVA=-1  // generic def  token=0x600016C  System.Void LogToSceneView(Beyond.ELogChannel channel, System.Single lifeTime, System.String message, T param1)
  RVA=-1  // generic def  token=0x600016D  System.Void LogToSceneView(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x600016E  System.Void LogToSceneView(Beyond.ELogChannel channel, System.Single lifeTime, System.String message, T1 param1, T2 param2)
  RVA=0x063717A4  token=0x600016F  System.Void LogToSceneViewWarning(Beyond.ELogChannel channel, System.Single lifeTime, System.String message)
  RVA=0x06371808  token=0x6000170  System.Void LogToSceneViewWarning(Beyond.ELogChannel channel, System.String message)
  RVA=-1  // generic def  token=0x6000171  System.Void LogToSceneViewWarning(Beyond.ELogChannel channel, System.String message, T param1)
  RVA=-1  // generic def  token=0x6000172  System.Void LogToSceneViewWarning(Beyond.ELogChannel channel, System.Single lifeTime, System.String message, T param1)
  RVA=-1  // generic def  token=0x6000173  System.Void LogToSceneViewWarning(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x6000174  System.Void LogToSceneViewWarning(Beyond.ELogChannel channel, System.Single lifeTime, System.String message, T1 param1, T2 param2)
  RVA=0x06371628  token=0x6000175  System.Void LogToSceneViewError(Beyond.ELogChannel channel, System.Single lifeTime, System.String message)
  RVA=0x063715D8  token=0x6000176  System.Void LogToSceneViewError(Beyond.ELogChannel channel, System.String message)
  RVA=-1  // generic def  token=0x6000177  System.Void LogToSceneViewError(Beyond.ELogChannel channel, System.String message, T param1)
  RVA=-1  // generic def  token=0x6000178  System.Void LogToSceneViewError(Beyond.ELogChannel channel, System.Single lifeTime, System.String message, T param1)
  RVA=-1  // generic def  token=0x6000179  System.Void LogToSceneViewError(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x600017A  System.Void LogToSceneViewError(Beyond.ELogChannel channel, System.Single lifeTime, System.String message, T1 param1, T2 param2)
  RVA=0x06371524  token=0x600017B  System.Void LogToSceneViewCritical(Beyond.ELogChannel channel, System.Single lifeTime, System.String message)
  RVA=0x06371588  token=0x600017C  System.Void LogToSceneViewCritical(Beyond.ELogChannel channel, System.String message)
  RVA=-1  // generic def  token=0x600017D  System.Void LogToSceneViewCritical(Beyond.ELogChannel channel, System.String message, T param1)
  RVA=-1  // generic def  token=0x600017E  System.Void LogToSceneViewCritical(Beyond.ELogChannel channel, System.Single lifeTime, System.String message, T param1)
  RVA=-1  // generic def  token=0x600017F  System.Void LogToSceneViewCritical(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2)
  RVA=-1  // generic def  token=0x6000180  System.Void LogToSceneViewCritical(Beyond.ELogChannel channel, System.Single lifeTime, System.String message, T1 param1, T2 param2)
  RVA=0x06371DC4  token=0x6000181  System.Void Log(Beyond.ELogLevel logLevel, Beyond.ELogChannel channel, System.String message, UnityEngine.Object context)
  RVA=0x06371EF8  token=0x6000182  System.Void Log(Beyond.ELogLevel logLevel, Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, UnityEngine.Object context)
  RVA=0x063726BC  token=0x6000183  System.Void _DefaultLog(Beyond.Log.FLogMessage& msg)
  RVA=0x06372660  token=0x6000184  System.Void _ConsoleLog(Beyond.Log.FLogMessage& msg)
  RVA=0x06372D24  token=0x6000185  System.Void _SceneViewLog(Beyond.Log.FLogMessage& msg, System.Single time)
  RVA=0x063725A8  token=0x6000186  Beyond.DLogger.DebugContext _AllocateDebugContext()
  RVA=0x06372340  token=0x6000187  Beyond.DLogger.DebugContextScope SetDebugContextInfo_MustUseUsing(System.String info, Beyond.ELogLevel logLevelMask)
  RVA=0x0313B970  token=0x6000188  System.String GetDebugContextInfo(Beyond.ELogLevel logLevelMask)
  RVA=0x063722A4  token=0x600018F  System.Void PushPing(System.Int32 ping)
  RVA=0x063722EC  token=0x6000190  System.Void PushPlayerPos(UnityEngine.Vector3 pos)
  RVA=0x03328BE0  token=0x6000191  System.Void Init()
  RVA=0x033283A0  token=0x6000192  System.Void _AddCustomGameLoop(UnityEngine.LowLevel.PlayerLoopSystem& playerLoop)
  RVA=0x033280D0  token=0x6000193  System.Void AddPlayerLoop(UnityEngine.LowLevel.PlayerLoopSystem method, UnityEngine.LowLevel.PlayerLoopSystem& playerLoop, System.String categoryName, System.String systemName, System.Boolean last, System.Boolean before)
  RVA=0x03324980  token=0x6000194  System.Boolean CheckRegist(UnityEngine.LowLevel.PlayerLoopSystem& playerLoop)
  RVA=0x06372CE4  token=0x6000195  System.Void _OnApplicationQuit()
  RVA=0x048615A0  token=0x6000196  System.Void .cctor()
END_CLASS

CLASS: Beyond.Log.BitArray256
TYPE:  sealed struct
TOKEN: 0x200000F
SIZE:  0x30
FIELDS:
  private           System.UInt64                   data1  // 0x10
  private           System.UInt64                   data2  // 0x18
  private           System.UInt64                   data3  // 0x20
  private           System.UInt64                   data4  // 0x28
PROPERTIES:
  capacity  get=0x0636DF80
  allFalse  get=0x0636DEA8
  allTrue  get=0x0636DF14
  humanizedData  get=0x0636DFCC
  Item  get=0x0636DE48  set=0x0636E6C0
METHODS:
  RVA=0x04D861C0  token=0x60001AA  System.Void .ctor(System.UInt64 initValue1, System.UInt64 initValue2, System.UInt64 initValue3, System.UInt64 initValue4)
  RVA=0x0636DD3C  token=0x60001AB  System.Void .ctor(System.Collections.Generic.IEnumerable<System.UInt32> bitIndexTrue)
  RVA=0x0636E618  token=0x60001AC  Beyond.Log.BitArray256 op_OnesComplement(Beyond.Log.BitArray256 a)
  RVA=0x0636E3C0  token=0x60001AD  Beyond.Log.BitArray256 op_BitwiseOr(Beyond.Log.BitArray256 a, Beyond.Log.BitArray256 b)
  RVA=0x0636E2C8  token=0x60001AE  Beyond.Log.BitArray256 op_BitwiseAnd(Beyond.Log.BitArray256 a, Beyond.Log.BitArray256 b)
  RVA=0x0636E4B8  token=0x60001AF  System.Boolean op_Equality(Beyond.Log.BitArray256 a, Beyond.Log.BitArray256 b)
  RVA=0x0636E568  token=0x60001B0  System.Boolean op_Inequality(Beyond.Log.BitArray256 a, Beyond.Log.BitArray256 b)
  RVA=0x0636D8E0  token=0x60001B1  System.Boolean Equals(System.Object obj)
  RVA=0x0636DA50  token=0x60001B2  System.Int32 GetHashCode()
  RVA=0x0636D9C4  token=0x60001B3  System.Boolean GetBool(System.Byte index)
  RVA=0x0636DB98  token=0x60001B4  System.Void SetBool(System.UInt32 index, System.Boolean value)
  RVA=0x0636DB3C  token=0x60001B5  System.Void SetAllTrue()
  RVA=0x0636DAE0  token=0x60001B6  System.Void SetAllFalse()
  RVA=0x03E3E150  token=0x60001B7  Beyond.Log.BitArray256 AllTrue()
  RVA=0x0636D864  token=0x60001B8  Beyond.Log.BitArray256 AllFalse()
  RVA=0x0636DC7C  token=0x60001B9  System.Boolean <>iFixBaseProxy_Equals(System.Object P0)
  RVA=0x0636DCE4  token=0x60001BA  System.Int32 <>iFixBaseProxy_GetHashCode()
END_CLASS

CLASS: Beyond.Log.ConsoleLogger
TYPE:  class
TOKEN: 0x2000010
SIZE:  0x10
IMPLEMENTS: Beyond.Log.ILogger
FIELDS:
  public    static readonly System.String                   LOG_TAG  // static @ 0x0
METHODS:
  RVA=0x0636E738  token=0x60001BB  System.Boolean _CheckIsMainThread()
  RVA=0x0636E7E8  token=0x60001BC  System.String _GetCurrentThreadName()
  RVA=0x0636E794  token=0x60001BD  System.Int32 _GetCurrentThreadID()
  RVA=0x0313C1A0  token=0x60001BE  System.Void Log(Beyond.Log.FLogMessage& msg)
  RVA=0x0313C6C0  token=0x60001BF  System.Void LogToUnity(Beyond.ELogLevel level, System.String msg, System.Exception exception, UnityEngine.Object context)
  RVA=0x0313C2C0  token=0x60001C0  System.String _BuildLogForRuntime(System.Int64 msgTime, System.Int32 frameCount, System.String message, Beyond.ELogChannel channel, Beyond.EColorTag color, Beyond.ELogLevel logLevel)
  RVA=0x041E1670  token=0x60001C1  System.Void .ctor()
  RVA=0x04D50940  token=0x60001C2  System.Void .cctor()
END_CLASS

CLASS: Beyond.Log.FLogMessage
TYPE:  sealed struct
TOKEN: 0x2000011
SIZE:  0x50
FIELDS:
  public            System.String                   message  // 0x10
  public            System.Int64                    msgTime  // 0x18
  public            System.Exception                exception  // 0x20
  public            Beyond.ELogLevel                logLevel  // 0x28
  public            Beyond.EColorTag                colorTag  // 0x29
  public            Beyond.ELogChannel              channel  // 0x2a
  public            UnityEngine.Object              context  // 0x30
  public            System.Int32                    ping  // 0x38
  public            System.Int32                    frameCount  // 0x3c
  public            UnityEngine.Vector3             playerPos  // 0x40
METHODS:
  RVA=0x0313AAA0  token=0x60001C3  Beyond.Log.FLogMessage CreateFromUnityLogCallback(Beyond.ELogChannel channel, System.String message, System.String stacktrace, UnityEngine.LogType type)
END_CLASS

CLASS: Beyond.Log.ILogger
TYPE:  interface
TOKEN: 0x2000012
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60001C4  System.Void Log(Beyond.Log.FLogMessage& msg)
END_CLASS

CLASS: Beyond.Log.LocalLogger
TYPE:  class
TOKEN: 0x2000013
SIZE:  0x10
FIELDS:
  private   static  System.Int32                    mainThreadId  // static @ 0x0
  private   static  System.Boolean                  s_initLog  // static @ 0x4
  private   static  System.IO.StreamWriter          s_StreamWriter  // static @ 0x8
  private   static  System.Collections.Generic.List<System.IO.StreamWriter>allStreamWriter  // static @ 0x10
  private   static  System.Collections.Concurrent.ConcurrentStack<Beyond.UnSafeString>s_stack  // static @ 0x18
  private   static  Beyond.UnSafeString             s_mainCache  // static @ 0x20
  private   static  System.String                   s_LogFilePath  // static @ 0x28
  private   static  System.String[]                 s_logTypeString  // static @ 0x30
  private   static  System.IO.StreamWriter          _sThreadStreamWriter  // static @ 0xffffffff
PROPERTIES:
  persistentDataPath  get=0x03828F80
METHODS:
  RVA=0x049D7110  token=0x60001C6  System.Void .cctor()
  RVA=0x04D4E070  token=0x60001C7  System.Void SubsystemRegistration()
  RVA=0x038288B0  token=0x60001C8  System.Void LogInit(System.Boolean forceDebug)
  RVA=0x03E3E8F0  token=0x60001C9  System.Void InitLocalLogger(System.String path)
  RVA=0x06377EB8  token=0x60001CA  System.Void OnWantsToShutdown()
  RVA=0x06377D84  token=0x60001CB  System.String FlushFile()
  RVA=0x06378508  token=0x60001CC  System.String getLogFileLocalPath()
  RVA=0x033303B0  token=0x60001CD  System.Void _UnityLogCallback(System.String condition, System.String stackTrace, UnityEngine.LogType type)
  RVA=0x0332EF40  token=0x60001CE  System.Void _UnityLogCallbackThread(System.String condition, System.String stackTrace, UnityEngine.LogType type)
  RVA=0x0637836C  token=0x60001CF  System.Void _GetTime(Beyond.UnSafeString unSafeString)
  RVA=0x063782A8  token=0x60001D0  System.Void _GetLogType(Beyond.UnSafeString unSafeString, UnityEngine.LogType type)
  RVA=0x03330A40  token=0x60001D1  Beyond.UnSafeString Alloc()
  RVA=0x03330B30  token=0x60001D2  System.Void Release(Beyond.UnSafeString value)
  RVA=0x041E1670  token=0x60001D3  System.Void .ctor()
END_CLASS

CLASS: Beyond.Log.SceneViewMessageLogger
TYPE:  class
TOKEN: 0x2000014
SIZE:  0x18
IMPLEMENTS: Beyond.Log.ILogger
FIELDS:
  public            System.Boolean                  enabled  // 0x10
METHODS:
  RVA=0x015EFCD0  token=0x60001D4  System.Void .ctor()
  RVA=0x063788A8  token=0x60001D5  System.Void Log(Beyond.Log.FLogMessage& msg)
  RVA=0x06378840  token=0x60001D6  System.Void Log(Beyond.Log.FLogMessage& msg, System.Single lifeTime)
  RVA=0x063787F0  token=0x60001D7  System.Void ClearLogs(System.String msg)
  RVA=0x063787A0  token=0x60001D8  System.Void ClearLogsRegex(System.String regexMsg)
END_CLASS

CLASS: Beyond.Log.UnionLogger
TYPE:  class
TOKEN: 0x2000015
SIZE:  0x20
IMPLEMENTS: Beyond.Log.ILogger
FIELDS:
  private           Beyond.Log.ILogger              s_consoleLogger  // 0x10
  private           Beyond.Log.ILogger              s_netServerLogger  // 0x18
  private   static  System.Int32                    MAX_MSG_LEN  // const
METHODS:
  RVA=0x03E3E240  token=0x60001D9  System.Void SetServerLogger(Beyond.Log.ILogger log)
  RVA=0x0313C020  token=0x60001DA  System.Void Log(Beyond.Log.FLogMessage& msg)
  RVA=0x04861930  token=0x60001DB  System.Void .ctor()
END_CLASS

CLASS: IFix.ILFixDynamicMethodWrapper
TYPE:  class
TOKEN: 0x2000016
SIZE:  0x28
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
  private           System.Int32                    methodId  // 0x18
  private           System.Object                   anonObj  // 0x20
  public    static  IFix.ILFixDynamicMethodWrapper[]wrapperArray  // static @ 0x0
METHODS:
  RVA=0x063735B8  token=0x60001DC  System.String __Gen_Wrap_0(Beyond.EColorTag P0)
  RVA=0x0637406C  token=0x60001DD  System.String __Gen_Wrap_1(Beyond.ELogChannel P0)
  RVA=0x063633B4  token=0x60001DE  System.Void __Gen_Wrap_2(System.Object P0)
  RVA=0x063755E0  token=0x60001DF  Beyond.ELogLevel[] __Gen_Wrap_3()
  RVA=0x063761FC  token=0x60001E0  System.Void __Gen_Wrap_4()
  RVA=0x06376F44  token=0x60001E1  System.Void __Gen_Wrap_5(Beyond.Log.FLogMessage& P0)
  RVA=0x06377B18  token=0x60001E2  System.Void __Gen_Wrap_6(System.Object P0, System.Object P1)
  RVA=0x06377BF4  token=0x60001E3  System.Void __Gen_Wrap_7(Beyond.ELogLevel P0)
  RVA=0x06363214  token=0x60001E4  System.Boolean __Gen_Wrap_8(Beyond.ELogLevel P0)
  RVA=0x06377CA0  token=0x60001E5  System.Void __Gen_Wrap_9(Beyond.ELogChannel P0, Beyond.ELogLevel P1, System.Boolean P2)
  RVA=0x06373694  token=0x60001E6  System.Boolean __Gen_Wrap_10(Beyond.ELogChannel P0, Beyond.ELogLevel P1)
  RVA=0x06373780  token=0x60001E7  System.Void __Gen_Wrap_11(Beyond.ELogChannel P0, Beyond.ELogLevel P1, Beyond.EColorTag P2, System.Object P3, System.Object P4, System.Object P5)
  RVA=0x063738A8  token=0x60001E8  System.Void __Gen_Wrap_12(Beyond.ELogChannel P0, Beyond.EColorTag P1, System.Object P2, System.Object P3, System.Object P4)
  RVA=0x063739C0  token=0x60001E9  System.Void __Gen_Wrap_13(Beyond.ELogChannel P0, System.Object P1)
  RVA=0x06373A9C  token=0x60001EA  System.Void __Gen_Wrap_14(Beyond.ELogChannel P0, Beyond.EColorTag P1, System.Object P2)
  RVA=0x06373B94  token=0x60001EB  System.Void __Gen_Wrap_15(Beyond.ELogChannel P0, Beyond.EColorTag P1, System.Object P2, System.Object P3)
  RVA=0x06373C9C  token=0x60001EC  System.Void __Gen_Wrap_16(Beyond.ELogChannel P0, System.Object P1, System.Object P2)
  RVA=0x06373D80  token=0x60001ED  System.Void __Gen_Wrap_17(System.Object P0, System.Object P1, System.Object P2)
  RVA=0x06373E64  token=0x60001EE  System.Void __Gen_Wrap_18(System.Object P0, System.Object P1, System.Object P2, System.Object P3)
  RVA=0x06373F5C  token=0x60001EF  System.Void __Gen_Wrap_19(System.Object P0, System.Object P1, System.Object P2, System.Object P3, System.Object P4)
  RVA=0x0637415C  token=0x60001F0  System.Void __Gen_Wrap_20(System.Object P0, System.Object P1, System.Object P2, System.Object P3, System.Object P4, System.Object P5)
  RVA=0x06374274  token=0x60001F1  System.Void __Gen_Wrap_21(System.Object P0, System.Object P1, System.Object P2, System.Object P3, System.Object P4, System.Object P5, System.Object P6)
  RVA=0x0637439C  token=0x60001F2  System.Void __Gen_Wrap_22(System.Object P0, System.Object P1, System.Object P2, System.Object P3, System.Object P4, System.Object P5, System.Object P6, System.Object P7)
  RVA=0x063744D4  token=0x60001F3  System.Void __Gen_Wrap_23(Beyond.ELogChannel P0, System.Object P1, System.Object P2, System.Object P3)
  RVA=0x063745CC  token=0x60001F4  System.Void __Gen_Wrap_24(Beyond.ELogChannel P0, System.Object P1, System.Object P2, System.Object P3, System.Object P4)
  RVA=0x063746D8  token=0x60001F5  System.Void __Gen_Wrap_25(Beyond.ELogChannel P0, System.Object P1, System.Object P2, System.Object P3, System.Object P4, System.Object P5)
  RVA=0x063747F0  token=0x60001F6  System.Void __Gen_Wrap_26(Beyond.ELogChannel P0, System.Object P1, System.Object P2, System.Object P3, System.Object P4, System.Object P5, System.Object P6)
  RVA=0x06374918  token=0x60001F7  System.Void __Gen_Wrap_27(Beyond.ELogChannel P0, System.Object P1, System.Object P2, System.Object P3, System.Object P4, System.Object P5, System.Object P6, System.Object P7)
  RVA=0x06374A50  token=0x60001F8  System.Void __Gen_Wrap_28(Beyond.ELogChannel P0, System.Object P1, System.Object P2, System.Object P3, System.Object P4, System.Object P5, System.Object P6, System.Object P7, System.Object P8)
  RVA=0x06374B98  token=0x60001F9  System.Void __Gen_Wrap_29(Beyond.ELogChannel P0, Beyond.EnableLogType P1, Beyond.EColorTag P2, System.Object P3, System.Object P4)
  RVA=0x06374CB0  token=0x60001FA  System.Void __Gen_Wrap_30(Beyond.EnableLogType P0, System.Object P1)
  RVA=0x06374D8C  token=0x60001FB  System.Void __Gen_Wrap_31(Beyond.EnableLogType P0, Beyond.ELogChannel P1, System.Object P2)
  RVA=0x06374E84  token=0x60001FC  System.Void __Gen_Wrap_32(Beyond.EnableLogType P0, Beyond.ELogChannel P1, Beyond.EColorTag P2, System.Object P3)
  RVA=0x06374F7C  token=0x60001FD  System.Void __Gen_Wrap_33(Beyond.EnableLogType P0, Beyond.ELogChannel P1, Beyond.EColorTag P2, System.Object P3, System.Object P4)
  RVA=0x06375088  token=0x60001FE  System.Void __Gen_Wrap_34(System.Boolean P0)
  RVA=0x06375148  token=0x60001FF  System.Void __Gen_Wrap_35(Beyond.ELogLevel P0, Beyond.ELogChannel P1, System.Object P2, System.Single P3)
  RVA=0x06375240  token=0x6000200  System.Void __Gen_Wrap_36(Beyond.ELogChannel P0, System.Single P1, System.Object P2)
  RVA=0x06375324  token=0x6000201  System.Void __Gen_Wrap_37(Beyond.ELogLevel P0, Beyond.ELogChannel P1, System.Object P2, System.Object P3)
  RVA=0x0637541C  token=0x6000202  System.Void __Gen_Wrap_38(Beyond.ELogLevel P0, Beyond.ELogChannel P1, Beyond.EColorTag P2, System.Object P3, System.Object P4)
  RVA=0x06375528  token=0x6000203  Beyond.DLogger.DebugContext __Gen_Wrap_39()
  RVA=0x063756AC  token=0x6000204  Beyond.DLogger.DebugContextScope __Gen_Wrap_40(System.Object P0, Beyond.ELogLevel P1)
  RVA=0x063757A4  token=0x6000205  System.String __Gen_Wrap_41(Beyond.ELogLevel P0)
  RVA=0x06375894  token=0x6000206  System.Boolean __Gen_Wrap_43(UnityEngine.LowLevel.PlayerLoopSystem& P0)
  RVA=0x063759F0  token=0x6000207  System.Void __Gen_Wrap_44(UnityEngine.LowLevel.PlayerLoopSystem P0, UnityEngine.LowLevel.PlayerLoopSystem& P1, System.Object P2, System.Object P3, System.Boolean P4, System.Boolean P5)
  RVA=0x06375BC8  token=0x6000208  System.Void __Gen_Wrap_45(UnityEngine.LowLevel.PlayerLoopSystem& P0)
  RVA=0x06375D14  token=0x6000209  System.Void __Gen_Wrap_46(Beyond.DLogger.DebugContextScope& P0)
  RVA=0x06375E2C  token=0x600020A  System.UInt32 __Gen_Wrap_47(Beyond.Log.BitArray256& P0)
  RVA=0x06375F6C  token=0x600020B  System.Boolean __Gen_Wrap_48(Beyond.Log.BitArray256& P0)
  RVA=0x063760AC  token=0x600020C  System.String __Gen_Wrap_49(Beyond.Log.BitArray256& P0)
  RVA=0x06376284  token=0x600020D  System.Boolean __Gen_Wrap_50(Beyond.Log.BitArray256& P0, System.Byte P1)
  RVA=0x063763F0  token=0x600020E  System.Void __Gen_Wrap_51(Beyond.Log.BitArray256& P0, System.UInt32 P1, System.Boolean P2)
  RVA=0x06376554  token=0x600020F  System.Void __Gen_Wrap_52(Beyond.Log.BitArray256& P0, System.Byte P1, System.Boolean P2)
  RVA=0x063766BC  token=0x6000210  Beyond.Log.BitArray256 __Gen_Wrap_53(Beyond.Log.BitArray256 P0)
  RVA=0x063767E8  token=0x6000211  Beyond.Log.BitArray256 __Gen_Wrap_54(Beyond.Log.BitArray256 P0, Beyond.Log.BitArray256 P1)
  RVA=0x06376950  token=0x6000212  System.Boolean __Gen_Wrap_55(Beyond.Log.BitArray256 P0, Beyond.Log.BitArray256 P1)
  RVA=0x06376A84  token=0x6000213  System.Boolean __Gen_Wrap_56(Beyond.Log.BitArray256& P0, System.Object P1)
  RVA=0x06376BE0  token=0x6000214  System.Int32 __Gen_Wrap_57(Beyond.Log.BitArray256& P0)
  RVA=0x06376D20  token=0x6000215  System.Void __Gen_Wrap_58(Beyond.Log.BitArray256& P0)
  RVA=0x06376E50  token=0x6000216  Beyond.Log.BitArray256 __Gen_Wrap_59()
  RVA=0x063770B0  token=0x6000217  System.Boolean __Gen_Wrap_60()
  RVA=0x06377148  token=0x6000218  System.String __Gen_Wrap_61()
  RVA=0x06377214  token=0x6000219  System.Int32 __Gen_Wrap_62()
  RVA=0x063772AC  token=0x600021A  System.String __Gen_Wrap_63(System.Int64 P0, System.Int32 P1, System.Object P2, Beyond.ELogChannel P3, Beyond.EColorTag P4, Beyond.ELogLevel P5)
  RVA=0x06377400  token=0x600021B  System.Void __Gen_Wrap_64(Beyond.ELogLevel P0, System.Object P1, System.Object P2, System.Object P3)
  RVA=0x06377508  token=0x600021C  System.Void __Gen_Wrap_65(System.Object P0, Beyond.Log.FLogMessage& P1)
  RVA=0x0637768C  token=0x600021D  Beyond.Log.FLogMessage __Gen_Wrap_66(Beyond.ELogChannel P0, System.Object P1, System.Object P2, UnityEngine.LogType P3)
  RVA=0x063777EC  token=0x600021E  Beyond.UnSafeString __Gen_Wrap_67()
  RVA=0x063778A4  token=0x600021F  System.Void __Gen_Wrap_68(System.Object P0, System.Object P1, UnityEngine.LogType P2)
  RVA=0x06377988  token=0x6000220  System.Void __Gen_Wrap_69(System.Object P0, Beyond.Log.FLogMessage& P1, System.Single P2)
  RVA=0x04D373A0  token=0x6000221  System.Void .cctor()
END_CLASS

CLASS: IFix.WrappersManagerImpl
TYPE:  class
TOKEN: 0x2000017
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x06378938  token=0x6000222  IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id)
  RVA=0x03330C20  token=0x6000223  System.Boolean IsPatched(System.Int32 id)
END_CLASS

