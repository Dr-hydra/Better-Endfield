// ========================================================
// Dumped by @desirepro
// Assembly: Beyond.Dlogger.Runtime.dll
// Classes:  23
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000008  // size: 0x18
    public class Options
    {
        // Fields
        public System.Collections.Generic.Dictionary<Beyond.EnableLogType,System.Boolean> remoteSet;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000197
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000009  // size: 0x80
    public sealed class LogCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x03DD5140  token: 0x6000198
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x6000199
        public virtual System.Void Invoke(Beyond.Log.FLogMessage& msg) { }

    }

    // TypeToken: 0x200000A  // size: 0x20
    public class DebugContext
    {
        // Fields
        public Beyond.ELogLevel logLevelMask;  // 0x10
        public System.String info;  // 0x18

        // Methods
        // RVA: 0x04D861E0  token: 0x600019A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000B  // size: 0x11
    public sealed struct DebugContextScope : System.IDisposable
    {
        // Methods
        // RVA: 0x06373488  token: 0x600019B
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x200000C  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Beyond.DLogger.<>c <>9;  // static @ 0x0
        public static UnityEngine.LowLevel.PlayerLoopSystem.UpdateFunction <>9__432_0;  // static @ 0x8

        // Methods
        // RVA: 0x04D4B8B0  token: 0x600019C
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600019D
        public System.Void .ctor() { }
        // RVA: 0x03234530  token: 0x600019E
        private System.Void <_AddCustomGameLoop>b__432_0() { }

    }

    // TypeToken: 0x200000D  // size: 0x20
    public sealed class <>c__DisplayClass433_0
    {
        // Fields
        public System.String categoryName;  // 0x10
        public System.String systemName;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x600019F
        public System.Void .ctor() { }
        // RVA: 0x04294210  token: 0x60001A0
        private System.Boolean <AddPlayerLoop>b__0(UnityEngine.LowLevel.PlayerLoopSystem s) { }
        // RVA: 0x063788FC  token: 0x60001A1
        private System.Boolean <AddPlayerLoop>b__1(UnityEngine.LowLevel.PlayerLoopSystem h) { }

    }

    // TypeToken: 0x200000E  // size: 0x20
    public sealed class <>c__DisplayClass434_0
    {
        // Fields
        public System.Type t;  // 0x10
        public System.Func<UnityEngine.LowLevel.PlayerLoopSystem,System.Boolean> <>9__0;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60001A2
        public System.Void .ctor() { }
        // RVA: 0x04546160  token: 0x60001A3
        private System.Boolean <CheckRegist>b__0(UnityEngine.LowLevel.PlayerLoopSystem x) { }

    }

namespace Beyond
{

    // TypeToken: 0x2000002  // size: 0x11
    public sealed struct ELogChannel
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static Beyond.ELogChannel None;  // const
        public static Beyond.ELogChannel Res;  // const
        public static Beyond.ELogChannel Cfg;  // const
        public static Beyond.ELogChannel Battle;  // const
        public static Beyond.ELogChannel GamePlay;  // const
        public static Beyond.ELogChannel UI;  // const
        public static Beyond.ELogChannel Effect;  // const
        public static Beyond.ELogChannel Audio;  // const
        public static Beyond.ELogChannel Network;  // const
        public static Beyond.ELogChannel Dialog;  // const
        public static Beyond.ELogChannel Cutscene;  // const
        public static Beyond.ELogChannel HGRP;  // const
        public static Beyond.ELogChannel Build;  // const
        public static Beyond.ELogChannel Camera;  // const
        public static Beyond.ELogChannel Animation;  // const
        public static Beyond.ELogChannel Scene;  // const
        public static Beyond.ELogChannel Action;  // const
        public static Beyond.ELogChannel Tool;  // const
        public static Beyond.ELogChannel Inventory;  // const
        public static Beyond.ELogChannel Lua;  // const
        public static Beyond.ELogChannel Debug;  // const
        public static Beyond.ELogChannel LevelScript;  // const
        public static Beyond.ELogChannel Movement;  // const
        public static Beyond.ELogChannel Interactive;  // const
        public static Beyond.ELogChannel Common;  // const
        public static Beyond.ELogChannel Editor;  // const
        public static Beyond.ELogChannel Streaming;  // const
        public static Beyond.ELogChannel Factory;  // const
        public static Beyond.ELogChannel Utils;  // const
        public static Beyond.ELogChannel NPC;  // const
        public static Beyond.ELogChannel VIDEO;  // const
        public static Beyond.ELogChannel I18N;  // const
        public static Beyond.ELogChannel Guide;  // const
        public static Beyond.ELogChannel HGEventLog;  // const
        public static Beyond.ELogChannel RedDot;  // const
        public static Beyond.ELogChannel NetworkCore;  // const
        public static Beyond.ELogChannel ExportScene;  // const
        public static Beyond.ELogChannel ExportSceneValidation;  // const
        public static Beyond.ELogChannel HGNetLog;  // const
        public static Beyond.ELogChannel IFix;  // const
        public static Beyond.ELogChannel SDK;  // const
        public static Beyond.ELogChannel VFS;  // const
        public static Beyond.ELogChannel Preload;  // const
        public static Beyond.ELogChannel Voice;  // const
        public static Beyond.ELogChannel CmdExport;  // const
        public static Beyond.ELogChannel BossBattler;  // const
        public static Beyond.ELogChannel ActionDetail;  // const
        public static Beyond.ELogChannel ActionExecutor;  // const
        public static Beyond.ELogChannel GameEventManager;  // const
        public static Beyond.ELogChannel ClientData;  // const
        public static Beyond.ELogChannel P4;  // const
        public static Beyond.ELogChannel NavMesh;  // const
        public static Beyond.ELogChannel FMV;  // const
        public static Beyond.ELogChannel Scope;  // const
        public static Beyond.ELogChannel MiniGame;  // const
        public static Beyond.ELogChannel Spawner;  // const
        public static Beyond.ELogChannel AI;  // const
        public static Beyond.ELogChannel Validation;  // const
        public static Beyond.ELogChannel Luna;  // const
        public static Beyond.ELogChannel LevelScriptTask;  // const
        public static Beyond.ELogChannel Dungeon;  // const
        public static Beyond.ELogChannel WorldLifeSpan;  // const
        public static Beyond.ELogChannel SNS;  // const
        public static Beyond.ELogChannel HttpGm;  // const
        public static Beyond.ELogChannel LevelEditor;  // const
        public static Beyond.ELogChannel TowerDefense;  // const
        public static Beyond.ELogChannel Spaceship;  // const
        public static Beyond.ELogChannel Physics;  // const
        public static Beyond.ELogChannel Weapon;  // const
        public static Beyond.ELogChannel DataManager;  // const
        public static Beyond.ELogChannel TempFile;  // const
        public static Beyond.ELogChannel Quality;  // const
        public static Beyond.ELogChannel Map;  // const
        public static Beyond.ELogChannel Bark;  // const
        public static Beyond.ELogChannel Entity;  // const
        public static Beyond.ELogChannel Squad;  // const
        public static Beyond.ELogChannel LevelSeq;  // const
        public static Beyond.ELogChannel Mission;  // const
        public static Beyond.ELogChannel Tracking;  // const
        public static Beyond.ELogChannel Friend;  // const
        public static Beyond.ELogChannel Lsm;  // const
        public static Beyond.ELogChannel ConditionRuntime;  // const
        public static Beyond.ELogChannel DynamicScene;  // const
        public static Beyond.ELogChannel ModelView;  // const
        public static Beyond.ELogChannel WaterDrone;  // const
        public static Beyond.ELogChannel HelloWorld;  // const
        public static Beyond.ELogChannel MapRegion;  // const
        public static Beyond.ELogChannel GameSetting;  // const
        public static Beyond.ELogChannel LevelLoader;  // const
        public static Beyond.ELogChannel PS5;  // const
        public static Beyond.ELogChannel Activity;  // const
        public static Beyond.ELogChannel Login;  // const
        public static Beyond.ELogChannel SkeletalMorph;  // const
        public static Beyond.ELogChannel Gm;  // const
        public static Beyond.ELogChannel AbilityAction;  // const
        public static Beyond.ELogChannel AbilityActionEssential;  // const
        public static Beyond.ELogChannel SystemPoi;  // const
        public static Beyond.ELogChannel CharPerform;  // const
        public static Beyond.ELogChannel BackgroundNotification;  // const
        public static Beyond.ELogChannel CloudGame;  // const
        public static Beyond.ELogChannel GlobalTag;  // const
        public static Beyond.ELogChannel MoveSync;  // const
        public static Beyond.ELogChannel SubVersionControl;  // const
        public static Beyond.ELogChannel SimulationTraining;  // const
        public static Beyond.ELogChannel BlackScreen;  // const
        public static Beyond.ELogChannel Max;  // const

    }

    // TypeToken: 0x2000003  // size: 0x11
    public sealed struct ELogLevel
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static Beyond.ELogLevel None;  // const
        public static Beyond.ELogLevel Info;  // const
        public static Beyond.ELogLevel Warning;  // const
        public static Beyond.ELogLevel Error;  // const
        public static Beyond.ELogLevel Critical;  // const
        public static Beyond.ELogLevel Important;  // const
        public static Beyond.ELogLevel Exception;  // const
        public static Beyond.ELogLevel All;  // const

    }

    // TypeToken: 0x2000004  // size: 0x11
    public sealed struct EColorTag
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static Beyond.EColorTag None;  // const
        public static Beyond.EColorTag Red;  // const
        public static Beyond.EColorTag Yellow;  // const
        public static Beyond.EColorTag Green;  // const
        public static Beyond.EColorTag Blue;  // const
        public static Beyond.EColorTag White;  // const
        public static Beyond.EColorTag Black;  // const
        public static Beyond.EColorTag Grey;  // const
        public static Beyond.EColorTag Cyan;  // const
        public static Beyond.EColorTag Magenta;  // const
        public static Beyond.EColorTag Critical;  // const

    }

    // TypeToken: 0x2000005  // size: 0x10
    public static class LogDefine
    {
        // Fields
        private static System.Collections.Generic.Dictionary<System.Int32,System.String> s_logNameCacheDict;  // static @ 0x0

        // Methods
        // RVA: 0x063785C0  token: 0x6000001
        public static System.String ConvertColorTagToName(Beyond.EColorTag colorTag) { }
        // RVA: 0x0313C440  token: 0x6000002
        public static System.String ConvertLogChannelToName(Beyond.ELogChannel logChannel) { }
        // RVA: 0x04D17A30  token: 0x6000003
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000006  // size: 0x14
    public sealed struct EnableLogType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.EnableLogType DevOnly;  // const
        public static Beyond.EnableLogType Release;  // const
        public static Beyond.EnableLogType Login;  // const
        public static Beyond.EnableLogType EntitySpawn;  // const
        public static Beyond.EnableLogType NpcSpawn;  // const
        public static Beyond.EnableLogType Proxy;  // const
        public static Beyond.EnableLogType NpcAction;  // const
        public static Beyond.EnableLogType GuideState;  // const
        public static Beyond.EnableLogType MainCharacter;  // const
        public static Beyond.EnableLogType Perform;  // const
        public static Beyond.EnableLogType LevelLoader;  // const
        public static Beyond.EnableLogType Dialog;  // const
        public static Beyond.EnableLogType MainHudActionQueue;  // const
        public static Beyond.EnableLogType AirWall;  // const
        public static Beyond.EnableLogType Gm;  // const
        public static Beyond.EnableLogType I18N;  // const
        public static Beyond.EnableLogType Count;  // const

    }

    // TypeToken: 0x2000007  // size: 0x10
    public static class DLogger
    {
        // Fields
        private static System.Boolean[] s_remoteSet;  // static @ 0x0
        private static Beyond.Log.ILogger s_consoleLogger;  // static @ 0x8
        private static Beyond.Log.SceneViewMessageLogger s_sceneViewLogger;  // static @ 0x10
        private static Beyond.Log.UnionLogger s_defaultLogger;  // static @ 0x18
        private static Beyond.ELogLevel ms_enabledLogLevel;  // static @ 0x20
        private static Beyond.ELogLevel[] ms_enabledLogChannel;  // static @ 0x28
        private static Beyond.DLogger.LogCallback ms_logCriticalAndExceptionCallbackHandler;  // static @ 0x30
        private static Beyond.DLogger.LogCallback ms_onBeforeLogError;  // static @ 0x38
        private static Beyond.DLogger.LogCallback ms_onBeforeLogCriticalAndException;  // static @ 0x40
        public static System.Func<System.String> s_getLuaStackTraceHandler;  // static @ 0x48
        private static System.Collections.Generic.Stack<Beyond.DLogger.DebugContext> s_debugContextPool;  // static @ 0x50
        private static System.Collections.Generic.List<Beyond.DLogger.DebugContext> s_debugContextList;  // static @ 0x58
        private static System.Int32 <FrameCountThreadSafe>k__BackingField;  // static @ 0x60
        private static System.Int32 <LogPing>k__BackingField;  // static @ 0x64
        private static UnityEngine.Vector3 <LogPlayerPos>k__BackingField;  // static @ 0x68
        public static System.Int32 ExceptionCount;  // static @ 0x74
        public static System.Int32 CriticalCount;  // static @ 0x78
        public static System.Int32 ErrorCount;  // static @ 0x7c
        public static System.Int32 ImportantCount;  // static @ 0x80
        public static System.Int32 WarningCount;  // static @ 0x84
        public static System.Int32 InfoCount;  // static @ 0x88
        public static System.Int32 PauseCount;  // static @ 0x8c
        public static System.Int32 ReturnLoginCount;  // static @ 0x90

        // Properties
        Beyond.ELogLevel[] s_enabledLogChannel { get; /* RVA: 0x0332EE30 */ }
        System.Int32 FrameCountThreadSafe { get; /* RVA: 0x06372F94 */ set; /* RVA: 0x0637338C */ }
        System.Int32 LogPing { get; /* RVA: 0x06372FE4 */ set; /* RVA: 0x063733E0 */ }
        UnityEngine.Vector3 LogPlayerPos { get; /* RVA: 0x06373034 */ set; /* RVA: 0x06373434 */ }

        // Events
        event Beyond.DLogger.LogCallback s_logCriticalAndExceptionMessageReceived;
        event Beyond.DLogger.LogCallback s_onBeforeLogError;
        event Beyond.DLogger.LogCallback s_onBeforeLogCriticalAndException;

        // Methods
        // RVA: 0x045E16D0  token: 0x6000004
        public static System.Void SetImportantLogConfig(Beyond.DLogger.Options options) { }
        // RVA: 0x04CDEDC0  token: 0x6000008
        private static System.Void _RunOnStart() { }
        // RVA: 0x0313C580  token: 0x6000009
        private static System.Void UpdateLogCount(Beyond.Log.FLogMessage& msg) { }
        // RVA: 0x03E3E1B0  token: 0x600000E
        public static System.Void SetServerLogger(Beyond.Log.ILogger logger) { }
        // RVA: 0x063724A4  token: 0x600000F
        public static System.Void SetLogLevel(Beyond.ELogLevel level) { }
        // RVA: 0x0332EEC0  token: 0x6000010
        public static System.Boolean CheckLogLevel(Beyond.ELogLevel level) { }
        // RVA: 0x063723E4  token: 0x6000011
        public static System.Void SetLogChannelEnabled(Beyond.ELogChannel channel, Beyond.ELogLevel level, System.Boolean val) { }
        // RVA: 0x0332ED70  token: 0x6000012
        public static System.Boolean CheckLogChannelEnabled(Beyond.ELogChannel channel, Beyond.ELogLevel level) { }
        // RVA: 0x06371D3C  token: 0x6000013
        public static System.Void Log(System.String message) { }
        // RVA: -1  // generic def  token: 0x6000014
        public static System.Void Log(System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x6000015
        public static System.Void Log(System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x6000016
        public static System.Void Log(System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x6000017
        public static System.Void Log(System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x6000018
        public static System.Void Log(System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x6000019
        public static System.Void Log(System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x600001A
        public static System.Void Log(System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x06371CA4  token: 0x600001B
        public static System.Void Log(Beyond.ELogChannel channel, System.String message) { }
        // RVA: -1  // generic def  token: 0x600001C
        public static System.Void Log(Beyond.ELogChannel channel, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x600001D
        public static System.Void Log(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x600001E
        public static System.Void Log(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x600001F
        public static System.Void Log(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x6000020
        public static System.Void Log(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x6000021
        public static System.Void Log(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x6000022
        public static System.Void Log(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x063721F8  token: 0x6000023
        public static System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message) { }
        // RVA: -1  // generic def  token: 0x6000024
        public static System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x6000025
        public static System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x6000026
        public static System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x6000027
        public static System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x6000028
        public static System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x6000029
        public static System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x600002A
        public static System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x06372164  token: 0x600002B
        public static System.Void Log(UnityEngine.Object context, System.String message) { }
        // RVA: -1  // generic def  token: 0x600002C
        public static System.Void Log(UnityEngine.Object context, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x600002D
        public static System.Void Log(UnityEngine.Object context, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x600002E
        public static System.Void Log(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x600002F
        public static System.Void Log(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x6000030
        public static System.Void Log(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x6000031
        public static System.Void Log(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: 0x06372038  token: 0x6000032
        public static System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message) { }
        // RVA: -1  // generic def  token: 0x6000033
        public static System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x6000034
        public static System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x6000035
        public static System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x6000036
        public static System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x6000037
        public static System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x6000038
        public static System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x6000039
        public static System.Void Log(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x063700B0  token: 0x600003A
        public static System.Void LogEditorAlwaysPrint(System.String message) { }
        // RVA: -1  // generic def  token: 0x600003B
        public static System.Void LogEditorAlwaysPrint(System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x600003C
        public static System.Void LogEditorAlwaysPrint(System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x600003D
        public static System.Void LogEditorAlwaysPrint(System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x600003E
        public static System.Void LogEditorAlwaysPrint(System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x600003F
        public static System.Void LogEditorAlwaysPrint(System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x6000040
        public static System.Void LogEditorAlwaysPrint(System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x6000041
        public static System.Void LogEditorAlwaysPrint(System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x06370010  token: 0x6000042
        public static System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, System.String message) { }
        // RVA: -1  // generic def  token: 0x6000043
        public static System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x6000044
        public static System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x6000045
        public static System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x6000046
        public static System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x6000047
        public static System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x6000048
        public static System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x6000049
        public static System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x06370164  token: 0x600004A
        public static System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message) { }
        // RVA: -1  // generic def  token: 0x600004B
        public static System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x600004C
        public static System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x600004D
        public static System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x600004E
        public static System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x600004F
        public static System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x6000050
        public static System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x6000051
        public static System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x06370060  token: 0x6000052
        public static System.Void LogEditorAlwaysPrint(UnityEngine.Object context, System.String message) { }
        // RVA: -1  // generic def  token: 0x6000053
        public static System.Void LogEditorAlwaysPrint(UnityEngine.Object context, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x6000054
        public static System.Void LogEditorAlwaysPrint(UnityEngine.Object context, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x6000055
        public static System.Void LogEditorAlwaysPrint(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x6000056
        public static System.Void LogEditorAlwaysPrint(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x6000057
        public static System.Void LogEditorAlwaysPrint(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x6000058
        public static System.Void LogEditorAlwaysPrint(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: 0x063700F0  token: 0x6000059
        public static System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message) { }
        // RVA: -1  // generic def  token: 0x600005A
        public static System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x600005B
        public static System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x600005C
        public static System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x600005D
        public static System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x600005E
        public static System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x600005F
        public static System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x6000060
        public static System.Void LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x06371C1C  token: 0x6000061
        public static System.Void LogWarning(System.String message) { }
        // RVA: 0x0637190C  token: 0x6000062
        public static System.Void LogWarning(UnityEngine.Object context, System.String message) { }
        // RVA: -1  // generic def  token: 0x6000063
        public static System.Void LogWarning(System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x6000064
        public static System.Void LogWarning(System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x6000065
        public static System.Void LogWarning(System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x6000066
        public static System.Void LogWarning(System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x6000067
        public static System.Void LogWarning(System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x6000068
        public static System.Void LogWarning(System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x6000069
        public static System.Void LogWarning(System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x06371A5C  token: 0x600006A
        public static System.Void LogWarning(Beyond.ELogChannel channel, System.String message) { }
        // RVA: -1  // generic def  token: 0x600006B
        public static System.Void LogWarning(Beyond.ELogChannel channel, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x600006C
        public static System.Void LogWarning(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x600006D
        public static System.Void LogWarning(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x600006E
        public static System.Void LogWarning(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x600006F
        public static System.Void LogWarning(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x6000070
        public static System.Void LogWarning(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x6000071
        public static System.Void LogWarning(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x06371B70  token: 0x6000072
        public static System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message) { }
        // RVA: -1  // generic def  token: 0x6000073
        public static System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x6000074
        public static System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x6000075
        public static System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x6000076
        public static System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x6000077
        public static System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x6000078
        public static System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x6000079
        public static System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x063719A0  token: 0x600007A
        public static System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message) { }
        // RVA: -1  // generic def  token: 0x600007B
        public static System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x600007C
        public static System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x600007D
        public static System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x600007E
        public static System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x600007F
        public static System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x6000080
        public static System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x6000081
        public static System.Void LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x04D096D0  token: 0x6000082
        public static System.Void LogError(System.String message) { }
        // RVA: 0x063707E0  token: 0x6000083
        public static System.Void LogErrorWithLuaStack(Beyond.ELogChannel channel, System.String message) { }
        // RVA: -1  // generic def  token: 0x6000084
        public static System.Void LogErrorWithLuaStack(Beyond.ELogChannel channel, System.String message, T1 param1) { }
        // RVA: -1  // generic def  token: 0x6000085
        public static System.Void LogErrorWithLuaStack(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x6000086
        public static System.Void LogErrorWithLuaStack(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: 0x06370764  token: 0x6000087
        public static System.Void LogErrorWithLuaStack(System.String message) { }
        // RVA: -1  // generic def  token: 0x6000088
        public static System.Void LogError(System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x6000089
        public static System.Void LogError(System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x600008A
        public static System.Void LogError(System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x600008B
        public static System.Void LogError(System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x600008C
        public static System.Void LogError(System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x600008D
        public static System.Void LogError(System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x600008E
        public static System.Void LogError(System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x0374C520  token: 0x600008F
        public static System.Void LogError(Beyond.ELogChannel channel, System.String message) { }
        // RVA: -1  // generic def  token: 0x6000090
        public static System.Void LogError(Beyond.ELogChannel channel, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x6000091
        public static System.Void LogError(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x6000092
        public static System.Void LogError(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x6000093
        public static System.Void LogError(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x6000094
        public static System.Void LogError(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x6000095
        public static System.Void LogError(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x6000096
        public static System.Void LogError(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x06370C50  token: 0x6000097
        public static System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message) { }
        // RVA: -1  // generic def  token: 0x6000098
        public static System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x6000099
        public static System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x600009A
        public static System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x600009B
        public static System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x600009C
        public static System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x600009D
        public static System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x600009E
        public static System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x063709E0  token: 0x600009F
        public static System.Void LogError(UnityEngine.Object context, System.String message) { }
        // RVA: -1  // generic def  token: 0x60000A0
        public static System.Void LogError(UnityEngine.Object context, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x60000A1
        public static System.Void LogError(UnityEngine.Object context, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x60000A2
        public static System.Void LogError(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x60000A3
        public static System.Void LogError(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x60000A4
        public static System.Void LogError(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x60000A5
        public static System.Void LogError(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x60000A6
        public static System.Void LogError(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x06370B80  token: 0x60000A7
        public static System.Void LogError(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message) { }
        // RVA: -1  // generic def  token: 0x60000A8
        public static System.Void LogError(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x60000A9
        public static System.Void LogError(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x60000AA
        public static System.Void LogError(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x60000AB
        public static System.Void LogError(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x60000AC
        public static System.Void LogError(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x60000AD
        public static System.Void LogError(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x60000AE
        public static System.Void LogError(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x06370A9C  token: 0x60000AF
        public static System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message) { }
        // RVA: -1  // generic def  token: 0x60000B0
        public static System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x60000B1
        public static System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x60000B2
        public static System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x60000B3
        public static System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x60000B4
        public static System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x60000B5
        public static System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x60000B6
        public static System.Void LogError(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x0636EFE0  token: 0x60000B7
        public static System.Void IFixLogError(System.String message) { }
        // RVA: 0x0636EAA4  token: 0x60000B8
        public static System.Void IFixLogError(System.String message, System.String param1) { }
        // RVA: 0x0636EED0  token: 0x60000B9
        public static System.Void IFixLogError(System.String message, System.String param1, System.String param2) { }
        // RVA: 0x0636F6C4  token: 0x60000BA
        public static System.Void IFixLogError(System.String message, System.String param1, System.String param2, System.String param3) { }
        // RVA: 0x0636F5A0  token: 0x60000BB
        public static System.Void IFixLogError(System.String message, System.String param1, System.String param2, System.String param3, System.String param4) { }
        // RVA: 0x0636F1A0  token: 0x60000BC
        public static System.Void IFixLogError(System.String message, System.String param1, System.String param2, System.String param3, System.String param4, System.String param5) { }
        // RVA: 0x0636E95C  token: 0x60000BD
        public static System.Void IFixLogError(System.String message, System.String param1, System.String param2, System.String param3, System.String param4, System.String param5, System.String param6) { }
        // RVA: 0x0636EBAC  token: 0x60000BE
        public static System.Void IFixLogError(System.String message, System.String param1, System.String param2, System.String param3, System.String param4, System.String param5, System.String param6, System.String param7) { }
        // RVA: 0x0636EE14  token: 0x60000BF
        public static System.Void IFixLogError(Beyond.ELogChannel channel, System.String message) { }
        // RVA: 0x0636ED08  token: 0x60000C0
        public static System.Void IFixLogError(Beyond.ELogChannel channel, System.String message, System.String param1) { }
        // RVA: 0x0636F08C  token: 0x60000C1
        public static System.Void IFixLogError(Beyond.ELogChannel channel, System.String message, System.String param1, System.String param2) { }
        // RVA: 0x0636F918  token: 0x60000C2
        public static System.Void IFixLogError(Beyond.ELogChannel channel, System.String message, System.String param1, System.String param2, System.String param3) { }
        // RVA: 0x0636F7DC  token: 0x60000C3
        public static System.Void IFixLogError(Beyond.ELogChannel channel, System.String message, System.String param1, System.String param2, System.String param3, System.String param4) { }
        // RVA: 0x0636F2DC  token: 0x60000C4
        public static System.Void IFixLogError(Beyond.ELogChannel channel, System.String message, System.String param1, System.String param2, System.String param3, System.String param4, System.String param5) { }
        // RVA: 0x0636FA44  token: 0x60000C5
        public static System.Void IFixLogError(Beyond.ELogChannel channel, System.String message, System.String param1, System.String param2, System.String param3, System.String param4, System.String param5, System.String param6) { }
        // RVA: 0x0636F42C  token: 0x60000C6
        public static System.Void IFixLogError(Beyond.ELogChannel channel, System.String message, System.String param1, System.String param2, System.String param3, System.String param4, System.String param5, System.String param6, System.String param7) { }
        // RVA: 0x06370604  token: 0x60000C7
        public static System.Void LogErrorWithFilter(System.String message, System.String filter) { }
        // RVA: -1  // generic def  token: 0x60000C8
        public static System.Void LogErrorWithFilter(System.String message, System.String filter, T param1) { }
        // RVA: -1  // generic def  token: 0x60000C9
        public static System.Void LogErrorWithFilter(System.String message, System.String filter, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x60000CA
        public static System.Void LogErrorWithFilter(System.String message, System.String filter, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x60000CB
        public static System.Void LogErrorWithFilter(System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x60000CC
        public static System.Void LogErrorWithFilter(System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x60000CD
        public static System.Void LogErrorWithFilter(System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x60000CE
        public static System.Void LogErrorWithFilter(System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x0637055C  token: 0x60000CF
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, System.String message, System.String filter) { }
        // RVA: -1  // generic def  token: 0x60000D0
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, System.String message, System.String filter, T param1) { }
        // RVA: -1  // generic def  token: 0x60000D1
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, System.String message, System.String filter, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x60000D2
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, System.String message, System.String filter, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x60000D3
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x60000D4
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x60000D5
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x60000D6
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x06370384  token: 0x60000D7
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, System.String filter) { }
        // RVA: -1  // generic def  token: 0x60000D8
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, System.String filter, T param1) { }
        // RVA: -1  // generic def  token: 0x60000D9
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, System.String filter, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x60000DA
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, System.String filter, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x60000DB
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x60000DC
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x60000DD
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x60000DE
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x063702DC  token: 0x60000DF
        public static System.Void LogErrorWithFilter(UnityEngine.Object context, System.String message, System.String filter) { }
        // RVA: -1  // generic def  token: 0x60000E0
        public static System.Void LogErrorWithFilter(UnityEngine.Object context, System.String message, System.String filter, T param1) { }
        // RVA: -1  // generic def  token: 0x60000E1
        public static System.Void LogErrorWithFilter(UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x60000E2
        public static System.Void LogErrorWithFilter(UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x60000E3
        public static System.Void LogErrorWithFilter(UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x60000E4
        public static System.Void LogErrorWithFilter(UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x60000E5
        public static System.Void LogErrorWithFilter(UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x60000E6
        public static System.Void LogErrorWithFilter(UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x06370440  token: 0x60000E7
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, System.String filter) { }
        // RVA: -1  // generic def  token: 0x60000E8
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, System.String filter, T param1) { }
        // RVA: -1  // generic def  token: 0x60000E9
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x60000EA
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x60000EB
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x60000EC
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x60000ED
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x60000EE
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x06370698  token: 0x60000EF
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter) { }
        // RVA: -1  // generic def  token: 0x60000F0
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter, T param1) { }
        // RVA: -1  // generic def  token: 0x60000F1
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x60000F2
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x60000F3
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x60000F4
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x60000F5
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x60000F6
        public static System.Void LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x0636FF64  token: 0x60000F7
        public static System.Void LogCritical(System.String message) { }
        // RVA: -1  // generic def  token: 0x60000F8
        public static System.Void LogCritical(System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x60000F9
        public static System.Void LogCritical(System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x60000FA
        public static System.Void LogCritical(System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x60000FB
        public static System.Void LogCritical(System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x60000FC
        public static System.Void LogCritical(System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x60000FD
        public static System.Void LogCritical(System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x60000FE
        public static System.Void LogCritical(System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x0636FDD8  token: 0x60000FF
        public static System.Void LogCritical(UnityEngine.Object context, System.String message) { }
        // RVA: -1  // generic def  token: 0x6000100
        public static System.Void LogCritical(UnityEngine.Object context, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x6000101
        public static System.Void LogCritical(UnityEngine.Object context, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x6000102
        public static System.Void LogCritical(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x6000103
        public static System.Void LogCritical(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x6000104
        public static System.Void LogCritical(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x6000105
        public static System.Void LogCritical(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x6000106
        public static System.Void LogCritical(UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x0636FCC0  token: 0x6000107
        public static System.Void LogCritical(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message) { }
        // RVA: 0x0421C2E0  token: 0x6000108
        public static System.Void LogCritical(Beyond.ELogChannel channel, System.String message) { }
        // RVA: -1  // generic def  token: 0x6000109
        public static System.Void LogCritical(Beyond.ELogChannel channel, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x600010A
        public static System.Void LogCritical(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x600010B
        public static System.Void LogCritical(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x600010C
        public static System.Void LogCritical(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x600010D
        public static System.Void LogCritical(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x600010E
        public static System.Void LogCritical(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x600010F
        public static System.Void LogCritical(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x0636FE94  token: 0x6000110
        public static System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message) { }
        // RVA: -1  // generic def  token: 0x6000111
        public static System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x6000112
        public static System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x6000113
        public static System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x6000114
        public static System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x6000115
        public static System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x6000116
        public static System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x6000117
        public static System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x0636FBA0  token: 0x6000118
        public static System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message) { }
        // RVA: -1  // generic def  token: 0x6000119
        public static System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x600011A
        public static System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x600011B
        public static System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x600011C
        public static System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x600011D
        public static System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x600011E
        public static System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x600011F
        public static System.Void LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: -1  // generic def  token: 0x6000120
        public static System.Void LogCritical(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x6000121
        public static System.Void LogCritical(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x6000122
        public static System.Void LogCritical(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x6000123
        public static System.Void LogCritical(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x6000124
        public static System.Void LogCritical(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x6000125
        public static System.Void LogCritical(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x6000126
        public static System.Void LogCritical(Beyond.ELogChannel channel, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x06371180  token: 0x6000127
        public static System.Void LogException(System.Exception e) { }
        // RVA: 0x06371204  token: 0x6000128
        public static System.Void LogException(UnityEngine.Object context, System.Exception e) { }
        // RVA: 0x06370EA8  token: 0x6000129
        public static System.Void LogException(Beyond.ELogChannel channel, System.Exception e) { }
        // RVA: 0x063710DC  token: 0x600012A
        public static System.Void LogException(Beyond.ELogChannel channel, Beyond.EColorTag color, System.Exception e) { }
        // RVA: 0x06370F38  token: 0x600012B
        public static System.Void LogException(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.Exception e) { }
        // RVA: 0x03AF76E0  token: 0x600012C
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, System.String message) { }
        // RVA: -1  // generic def  token: 0x600012D
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x600012E
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x600012F
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x6000130
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x6000131
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x6000132
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x6000133
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x0332DDD0  token: 0x6000134
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, System.String message) { }
        // RVA: -1  // generic def  token: 0x6000135
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x6000136
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x6000137
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x6000138
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x6000139
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x600013A
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x600013B
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x06371294  token: 0x600013C
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message) { }
        // RVA: -1  // generic def  token: 0x600013D
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x600013E
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x600013F
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x6000140
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x6000141
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x6000142
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x6000143
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x0637134C  token: 0x6000144
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message) { }
        // RVA: -1  // generic def  token: 0x6000145
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x6000146
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x6000147
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x6000148
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: -1  // generic def  token: 0x6000149
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5) { }
        // RVA: -1  // generic def  token: 0x600014A
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6) { }
        // RVA: -1  // generic def  token: 0x600014B
        public static System.Void LogImportant(Beyond.EnableLogType enableLogType, Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x06372C14  token: 0x600014C
        private static System.Void _Log(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter) { }
        // RVA: -1  // generic def  token: 0x600014D
        private static System.Void _Log(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x06372A74  token: 0x600014E
        private static System.Void _LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter) { }
        // RVA: -1  // generic def  token: 0x600014F
        private static System.Void _LogEditorAlwaysPrint(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x06372B44  token: 0x6000150
        private static System.Void _LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter) { }
        // RVA: -1  // generic def  token: 0x6000151
        private static System.Void _LogWarning(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x0332E670  token: 0x6000152
        private static System.Void _LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter) { }
        // RVA: -1  // generic def  token: 0x6000153
        private static System.Void _LogErrorWithFilter(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x0332DE50  token: 0x6000154
        private static System.Void _LogImportant(Beyond.ELogChannel channel, Beyond.EnableLogType enableLogType, Beyond.EColorTag color, UnityEngine.Object context, System.String message) { }
        // RVA: -1  // generic def  token: 0x6000155
        private static System.Void _LogImportant(Beyond.ELogChannel channel, Beyond.EnableLogType enableLogType, Beyond.EColorTag color, UnityEngine.Object context, System.String message, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x0421C230  token: 0x6000156
        private static System.Void _LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter) { }
        // RVA: -1  // generic def  token: 0x6000157
        private static System.Void _LogCritical(Beyond.ELogChannel channel, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x0332E720  token: 0x6000158
        private static System.Void _ApplyLog(Beyond.ELogChannel channel, Beyond.ELogLevel level, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter) { }
        // RVA: 0x0332ECB0  token: 0x6000159
        private static System.Void _LogWithFilter(Beyond.ELogChannel channel, Beyond.ELogLevel level, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter) { }
        // RVA: -1  // generic def  token: 0x600015A
        private static System.Void _ApplyLog(Beyond.ELogChannel channel, Beyond.ELogLevel level, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: -1  // generic def  token: 0x600015B
        private static System.Void _LogWithFilter(Beyond.ELogChannel channel, Beyond.ELogLevel level, Beyond.EColorTag color, UnityEngine.Object context, System.String message, System.String filter, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7) { }
        // RVA: 0x06371414  token: 0x600015C
        public static System.Void LogToConsole(Beyond.ELogChannel channel, System.String message) { }
        // RVA: -1  // generic def  token: 0x600015D
        public static System.Void LogToConsole(Beyond.ELogChannel channel, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x600015E
        public static System.Void LogToConsole(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2) { }
        // RVA: 0x063701C8  token: 0x600015F
        public static System.Void LogErrorToConsole(Beyond.ELogChannel channel, System.String message) { }
        // RVA: -1  // generic def  token: 0x6000160
        public static System.Void LogErrorToConsole(Beyond.ELogChannel channel, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x6000161
        public static System.Void LogErrorToConsole(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x6000162
        public static System.Void LogErrorToConsole(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3) { }
        // RVA: -1  // generic def  token: 0x6000163
        public static System.Void LogErrorToConsole(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2, T3 param3, T4 param4) { }
        // RVA: 0x06370D20  token: 0x6000164
        public static System.Void LogExceptionToConsole(Beyond.ELogChannel channel, System.Exception e) { }
        // RVA: 0x06372520  token: 0x6000165
        public static System.Void SetLogToSceneViewEnabled(System.Boolean enabled) { }
        // RVA: 0x0637168C  token: 0x6000166
        private static System.Void LogToSceneViewImpl(Beyond.ELogLevel logLevel, Beyond.ELogChannel channel, System.String message, System.Single lifeTime) { }
        // RVA: 0x0636E8CC  token: 0x6000167
        public static System.Void ClearSceneViewLogs(System.String message) { }
        // RVA: 0x0636E83C  token: 0x6000168
        public static System.Void ClearSceneViewLogsRegex(System.String message) { }
        // RVA: 0x063718A8  token: 0x6000169
        public static System.Void LogToSceneView(Beyond.ELogChannel channel, System.Single lifeTime, System.String message) { }
        // RVA: 0x06371858  token: 0x600016A
        public static System.Void LogToSceneView(Beyond.ELogChannel channel, System.String message) { }
        // RVA: -1  // generic def  token: 0x600016B
        public static System.Void LogToSceneView(Beyond.ELogChannel channel, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x600016C
        public static System.Void LogToSceneView(Beyond.ELogChannel channel, System.Single lifeTime, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x600016D
        public static System.Void LogToSceneView(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x600016E
        public static System.Void LogToSceneView(Beyond.ELogChannel channel, System.Single lifeTime, System.String message, T1 param1, T2 param2) { }
        // RVA: 0x063717A4  token: 0x600016F
        public static System.Void LogToSceneViewWarning(Beyond.ELogChannel channel, System.Single lifeTime, System.String message) { }
        // RVA: 0x06371808  token: 0x6000170
        public static System.Void LogToSceneViewWarning(Beyond.ELogChannel channel, System.String message) { }
        // RVA: -1  // generic def  token: 0x6000171
        public static System.Void LogToSceneViewWarning(Beyond.ELogChannel channel, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x6000172
        public static System.Void LogToSceneViewWarning(Beyond.ELogChannel channel, System.Single lifeTime, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x6000173
        public static System.Void LogToSceneViewWarning(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x6000174
        public static System.Void LogToSceneViewWarning(Beyond.ELogChannel channel, System.Single lifeTime, System.String message, T1 param1, T2 param2) { }
        // RVA: 0x06371628  token: 0x6000175
        public static System.Void LogToSceneViewError(Beyond.ELogChannel channel, System.Single lifeTime, System.String message) { }
        // RVA: 0x063715D8  token: 0x6000176
        public static System.Void LogToSceneViewError(Beyond.ELogChannel channel, System.String message) { }
        // RVA: -1  // generic def  token: 0x6000177
        public static System.Void LogToSceneViewError(Beyond.ELogChannel channel, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x6000178
        public static System.Void LogToSceneViewError(Beyond.ELogChannel channel, System.Single lifeTime, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x6000179
        public static System.Void LogToSceneViewError(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x600017A
        public static System.Void LogToSceneViewError(Beyond.ELogChannel channel, System.Single lifeTime, System.String message, T1 param1, T2 param2) { }
        // RVA: 0x06371524  token: 0x600017B
        public static System.Void LogToSceneViewCritical(Beyond.ELogChannel channel, System.Single lifeTime, System.String message) { }
        // RVA: 0x06371588  token: 0x600017C
        public static System.Void LogToSceneViewCritical(Beyond.ELogChannel channel, System.String message) { }
        // RVA: -1  // generic def  token: 0x600017D
        public static System.Void LogToSceneViewCritical(Beyond.ELogChannel channel, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x600017E
        public static System.Void LogToSceneViewCritical(Beyond.ELogChannel channel, System.Single lifeTime, System.String message, T param1) { }
        // RVA: -1  // generic def  token: 0x600017F
        public static System.Void LogToSceneViewCritical(Beyond.ELogChannel channel, System.String message, T1 param1, T2 param2) { }
        // RVA: -1  // generic def  token: 0x6000180
        public static System.Void LogToSceneViewCritical(Beyond.ELogChannel channel, System.Single lifeTime, System.String message, T1 param1, T2 param2) { }
        // RVA: 0x06371DC4  token: 0x6000181
        public static System.Void Log(Beyond.ELogLevel logLevel, Beyond.ELogChannel channel, System.String message, UnityEngine.Object context) { }
        // RVA: 0x06371EF8  token: 0x6000182
        public static System.Void Log(Beyond.ELogLevel logLevel, Beyond.ELogChannel channel, Beyond.EColorTag color, System.String message, UnityEngine.Object context) { }
        // RVA: 0x063726BC  token: 0x6000183
        private static System.Void _DefaultLog(Beyond.Log.FLogMessage& msg) { }
        // RVA: 0x06372660  token: 0x6000184
        private static System.Void _ConsoleLog(Beyond.Log.FLogMessage& msg) { }
        // RVA: 0x06372D24  token: 0x6000185
        private static System.Void _SceneViewLog(Beyond.Log.FLogMessage& msg, System.Single time) { }
        // RVA: 0x063725A8  token: 0x6000186
        private static Beyond.DLogger.DebugContext _AllocateDebugContext() { }
        // RVA: 0x06372340  token: 0x6000187
        public static Beyond.DLogger.DebugContextScope SetDebugContextInfo_MustUseUsing(System.String info, Beyond.ELogLevel logLevelMask) { }
        // RVA: 0x0313B970  token: 0x6000188
        public static System.String GetDebugContextInfo(Beyond.ELogLevel logLevelMask) { }
        // RVA: 0x063722A4  token: 0x600018F
        public static System.Void PushPing(System.Int32 ping) { }
        // RVA: 0x063722EC  token: 0x6000190
        public static System.Void PushPlayerPos(UnityEngine.Vector3 pos) { }
        // RVA: 0x03328BE0  token: 0x6000191
        public static System.Void Init() { }
        // RVA: 0x033283A0  token: 0x6000192
        private static System.Void _AddCustomGameLoop(UnityEngine.LowLevel.PlayerLoopSystem& playerLoop) { }
        // RVA: 0x033280D0  token: 0x6000193
        private static System.Void AddPlayerLoop(UnityEngine.LowLevel.PlayerLoopSystem method, UnityEngine.LowLevel.PlayerLoopSystem& playerLoop, System.String categoryName, System.String systemName, System.Boolean last, System.Boolean before) { }
        // RVA: 0x03324980  token: 0x6000194
        private static System.Boolean CheckRegist(UnityEngine.LowLevel.PlayerLoopSystem& playerLoop) { }
        // RVA: 0x06372CE4  token: 0x6000195
        private static System.Void _OnApplicationQuit() { }
        // RVA: 0x048615A0  token: 0x6000196
        private static System.Void .cctor() { }

    }

}

namespace Beyond.Log
{

    // TypeToken: 0x200000F  // size: 0x30
    public sealed struct BitArray256
    {
        // Fields
        private System.UInt64 data1;  // 0x10
        private System.UInt64 data2;  // 0x18
        private System.UInt64 data3;  // 0x20
        private System.UInt64 data4;  // 0x28

        // Properties
        System.UInt32 capacity { get; /* RVA: 0x0636DF80 */ }
        System.Boolean allFalse { get; /* RVA: 0x0636DEA8 */ }
        System.Boolean allTrue { get; /* RVA: 0x0636DF14 */ }
        System.String humanizedData { get; /* RVA: 0x0636DFCC */ }
        System.Boolean Item { get; /* RVA: 0x0636DE48 */ set; /* RVA: 0x0636E6C0 */ }

        // Methods
        // RVA: 0x04D861C0  token: 0x60001AA
        public System.Void .ctor(System.UInt64 initValue1, System.UInt64 initValue2, System.UInt64 initValue3, System.UInt64 initValue4) { }
        // RVA: 0x0636DD3C  token: 0x60001AB
        public System.Void .ctor(System.Collections.Generic.IEnumerable<System.UInt32> bitIndexTrue) { }
        // RVA: 0x0636E618  token: 0x60001AC
        public static Beyond.Log.BitArray256 op_OnesComplement(Beyond.Log.BitArray256 a) { }
        // RVA: 0x0636E3C0  token: 0x60001AD
        public static Beyond.Log.BitArray256 op_BitwiseOr(Beyond.Log.BitArray256 a, Beyond.Log.BitArray256 b) { }
        // RVA: 0x0636E2C8  token: 0x60001AE
        public static Beyond.Log.BitArray256 op_BitwiseAnd(Beyond.Log.BitArray256 a, Beyond.Log.BitArray256 b) { }
        // RVA: 0x0636E4B8  token: 0x60001AF
        public static System.Boolean op_Equality(Beyond.Log.BitArray256 a, Beyond.Log.BitArray256 b) { }
        // RVA: 0x0636E568  token: 0x60001B0
        public static System.Boolean op_Inequality(Beyond.Log.BitArray256 a, Beyond.Log.BitArray256 b) { }
        // RVA: 0x0636D8E0  token: 0x60001B1
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0636DA50  token: 0x60001B2
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0636D9C4  token: 0x60001B3
        public System.Boolean GetBool(System.Byte index) { }
        // RVA: 0x0636DB98  token: 0x60001B4
        public System.Void SetBool(System.UInt32 index, System.Boolean value) { }
        // RVA: 0x0636DB3C  token: 0x60001B5
        public System.Void SetAllTrue() { }
        // RVA: 0x0636DAE0  token: 0x60001B6
        public System.Void SetAllFalse() { }
        // RVA: 0x03E3E150  token: 0x60001B7
        public static Beyond.Log.BitArray256 AllTrue() { }
        // RVA: 0x0636D864  token: 0x60001B8
        public static Beyond.Log.BitArray256 AllFalse() { }
        // RVA: 0x0636DC7C  token: 0x60001B9
        public System.Boolean <>iFixBaseProxy_Equals(System.Object P0) { }
        // RVA: 0x0636DCE4  token: 0x60001BA
        public System.Int32 <>iFixBaseProxy_GetHashCode() { }

    }

    // TypeToken: 0x2000010  // size: 0x10
    public class ConsoleLogger : Beyond.Log.ILogger
    {
        // Fields
        public static readonly System.String LOG_TAG;  // static @ 0x0

        // Methods
        // RVA: 0x0636E738  token: 0x60001BB
        private static System.Boolean _CheckIsMainThread() { }
        // RVA: 0x0636E7E8  token: 0x60001BC
        private static System.String _GetCurrentThreadName() { }
        // RVA: 0x0636E794  token: 0x60001BD
        private static System.Int32 _GetCurrentThreadID() { }
        // RVA: 0x0313C1A0  token: 0x60001BE
        public virtual System.Void Log(Beyond.Log.FLogMessage& msg) { }
        // RVA: 0x0313C6C0  token: 0x60001BF
        private static System.Void LogToUnity(Beyond.ELogLevel level, System.String msg, System.Exception exception, UnityEngine.Object context) { }
        // RVA: 0x0313C2C0  token: 0x60001C0
        private static System.String _BuildLogForRuntime(System.Int64 msgTime, System.Int32 frameCount, System.String message, Beyond.ELogChannel channel, Beyond.EColorTag color, Beyond.ELogLevel logLevel) { }
        // RVA: 0x041E1670  token: 0x60001C1
        public System.Void .ctor() { }
        // RVA: 0x04D50940  token: 0x60001C2
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000011  // size: 0x50
    public sealed struct FLogMessage
    {
        // Fields
        public System.String message;  // 0x10
        public System.Int64 msgTime;  // 0x18
        public System.Exception exception;  // 0x20
        public Beyond.ELogLevel logLevel;  // 0x28
        public Beyond.EColorTag colorTag;  // 0x29
        public Beyond.ELogChannel channel;  // 0x2a
        public UnityEngine.Object context;  // 0x30
        public System.Int32 ping;  // 0x38
        public System.Int32 frameCount;  // 0x3c
        public UnityEngine.Vector3 playerPos;  // 0x40

        // Methods
        // RVA: 0x0313AAA0  token: 0x60001C3
        public static Beyond.Log.FLogMessage CreateFromUnityLogCallback(Beyond.ELogChannel channel, System.String message, System.String stacktrace, UnityEngine.LogType type) { }

    }

    // TypeToken: 0x2000012
    public interface ILogger
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60001C4
        public virtual System.Void Log(Beyond.Log.FLogMessage& msg) { }

    }

    // TypeToken: 0x2000013  // size: 0x10
    public class LocalLogger
    {
        // Fields
        private static System.Int32 mainThreadId;  // static @ 0x0
        private static System.Boolean s_initLog;  // static @ 0x4
        private static System.IO.StreamWriter s_StreamWriter;  // static @ 0x8
        private static System.Collections.Generic.List<System.IO.StreamWriter> allStreamWriter;  // static @ 0x10
        private static System.Collections.Concurrent.ConcurrentStack<Beyond.UnSafeString> s_stack;  // static @ 0x18
        private static Beyond.UnSafeString s_mainCache;  // static @ 0x20
        private static System.String s_LogFilePath;  // static @ 0x28
        private static System.String[] s_logTypeString;  // static @ 0x30
        private static System.IO.StreamWriter _sThreadStreamWriter;  // static @ 0xffffffff

        // Properties
        System.String persistentDataPath { get; /* RVA: 0x03828F80 */ }

        // Methods
        // RVA: 0x049D7110  token: 0x60001C6
        private static System.Void .cctor() { }
        // RVA: 0x04D4E070  token: 0x60001C7
        private static System.Void SubsystemRegistration() { }
        // RVA: 0x038288B0  token: 0x60001C8
        public static System.Void LogInit(System.Boolean forceDebug) { }
        // RVA: 0x03E3E8F0  token: 0x60001C9
        private static System.Void InitLocalLogger(System.String path) { }
        // RVA: 0x06377EB8  token: 0x60001CA
        private static System.Void OnWantsToShutdown() { }
        // RVA: 0x06377D84  token: 0x60001CB
        public static System.String FlushFile() { }
        // RVA: 0x06378508  token: 0x60001CC
        public static System.String getLogFileLocalPath() { }
        // RVA: 0x033303B0  token: 0x60001CD
        private static System.Void _UnityLogCallback(System.String condition, System.String stackTrace, UnityEngine.LogType type) { }
        // RVA: 0x0332EF40  token: 0x60001CE
        private static System.Void _UnityLogCallbackThread(System.String condition, System.String stackTrace, UnityEngine.LogType type) { }
        // RVA: 0x0637836C  token: 0x60001CF
        private static System.Void _GetTime(Beyond.UnSafeString unSafeString) { }
        // RVA: 0x063782A8  token: 0x60001D0
        private static System.Void _GetLogType(Beyond.UnSafeString unSafeString, UnityEngine.LogType type) { }
        // RVA: 0x03330A40  token: 0x60001D1
        public static Beyond.UnSafeString Alloc() { }
        // RVA: 0x03330B30  token: 0x60001D2
        public static System.Void Release(Beyond.UnSafeString value) { }
        // RVA: 0x041E1670  token: 0x60001D3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000014  // size: 0x18
    public class SceneViewMessageLogger : Beyond.Log.ILogger
    {
        // Fields
        public System.Boolean enabled;  // 0x10

        // Methods
        // RVA: 0x015EFCD0  token: 0x60001D4
        public System.Void .ctor() { }
        // RVA: 0x063788A8  token: 0x60001D5
        public virtual System.Void Log(Beyond.Log.FLogMessage& msg) { }
        // RVA: 0x06378840  token: 0x60001D6
        public System.Void Log(Beyond.Log.FLogMessage& msg, System.Single lifeTime) { }
        // RVA: 0x063787F0  token: 0x60001D7
        public System.Void ClearLogs(System.String msg) { }
        // RVA: 0x063787A0  token: 0x60001D8
        public System.Void ClearLogsRegex(System.String regexMsg) { }

    }

    // TypeToken: 0x2000015  // size: 0x20
    public class UnionLogger : Beyond.Log.ILogger
    {
        // Fields
        private Beyond.Log.ILogger s_consoleLogger;  // 0x10
        private Beyond.Log.ILogger s_netServerLogger;  // 0x18
        private static System.Int32 MAX_MSG_LEN;  // const

        // Methods
        // RVA: 0x03E3E240  token: 0x60001D9
        public System.Void SetServerLogger(Beyond.Log.ILogger log) { }
        // RVA: 0x0313C020  token: 0x60001DA
        public virtual System.Void Log(Beyond.Log.FLogMessage& msg) { }
        // RVA: 0x04861930  token: 0x60001DB
        public System.Void .ctor() { }

    }

}

namespace IFix
{

    // TypeToken: 0x2000016  // size: 0x28
    public class ILFixDynamicMethodWrapper
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10
        private System.Int32 methodId;  // 0x18
        private System.Object anonObj;  // 0x20
        public static IFix.ILFixDynamicMethodWrapper[] wrapperArray;  // static @ 0x0

        // Methods
        // RVA: 0x063735B8  token: 0x60001DC
        public System.String __Gen_Wrap_0(Beyond.EColorTag P0) { }
        // RVA: 0x0637406C  token: 0x60001DD
        public System.String __Gen_Wrap_1(Beyond.ELogChannel P0) { }
        // RVA: 0x063633B4  token: 0x60001DE
        public System.Void __Gen_Wrap_2(System.Object P0) { }
        // RVA: 0x063755E0  token: 0x60001DF
        public Beyond.ELogLevel[] __Gen_Wrap_3() { }
        // RVA: 0x063761FC  token: 0x60001E0
        public System.Void __Gen_Wrap_4() { }
        // RVA: 0x06376F44  token: 0x60001E1
        public System.Void __Gen_Wrap_5(Beyond.Log.FLogMessage& P0) { }
        // RVA: 0x06377B18  token: 0x60001E2
        public System.Void __Gen_Wrap_6(System.Object P0, System.Object P1) { }
        // RVA: 0x06377BF4  token: 0x60001E3
        public System.Void __Gen_Wrap_7(Beyond.ELogLevel P0) { }
        // RVA: 0x06363214  token: 0x60001E4
        public System.Boolean __Gen_Wrap_8(Beyond.ELogLevel P0) { }
        // RVA: 0x06377CA0  token: 0x60001E5
        public System.Void __Gen_Wrap_9(Beyond.ELogChannel P0, Beyond.ELogLevel P1, System.Boolean P2) { }
        // RVA: 0x06373694  token: 0x60001E6
        public System.Boolean __Gen_Wrap_10(Beyond.ELogChannel P0, Beyond.ELogLevel P1) { }
        // RVA: 0x06373780  token: 0x60001E7
        public System.Void __Gen_Wrap_11(Beyond.ELogChannel P0, Beyond.ELogLevel P1, Beyond.EColorTag P2, System.Object P3, System.Object P4, System.Object P5) { }
        // RVA: 0x063738A8  token: 0x60001E8
        public System.Void __Gen_Wrap_12(Beyond.ELogChannel P0, Beyond.EColorTag P1, System.Object P2, System.Object P3, System.Object P4) { }
        // RVA: 0x063739C0  token: 0x60001E9
        public System.Void __Gen_Wrap_13(Beyond.ELogChannel P0, System.Object P1) { }
        // RVA: 0x06373A9C  token: 0x60001EA
        public System.Void __Gen_Wrap_14(Beyond.ELogChannel P0, Beyond.EColorTag P1, System.Object P2) { }
        // RVA: 0x06373B94  token: 0x60001EB
        public System.Void __Gen_Wrap_15(Beyond.ELogChannel P0, Beyond.EColorTag P1, System.Object P2, System.Object P3) { }
        // RVA: 0x06373C9C  token: 0x60001EC
        public System.Void __Gen_Wrap_16(Beyond.ELogChannel P0, System.Object P1, System.Object P2) { }
        // RVA: 0x06373D80  token: 0x60001ED
        public System.Void __Gen_Wrap_17(System.Object P0, System.Object P1, System.Object P2) { }
        // RVA: 0x06373E64  token: 0x60001EE
        public System.Void __Gen_Wrap_18(System.Object P0, System.Object P1, System.Object P2, System.Object P3) { }
        // RVA: 0x06373F5C  token: 0x60001EF
        public System.Void __Gen_Wrap_19(System.Object P0, System.Object P1, System.Object P2, System.Object P3, System.Object P4) { }
        // RVA: 0x0637415C  token: 0x60001F0
        public System.Void __Gen_Wrap_20(System.Object P0, System.Object P1, System.Object P2, System.Object P3, System.Object P4, System.Object P5) { }
        // RVA: 0x06374274  token: 0x60001F1
        public System.Void __Gen_Wrap_21(System.Object P0, System.Object P1, System.Object P2, System.Object P3, System.Object P4, System.Object P5, System.Object P6) { }
        // RVA: 0x0637439C  token: 0x60001F2
        public System.Void __Gen_Wrap_22(System.Object P0, System.Object P1, System.Object P2, System.Object P3, System.Object P4, System.Object P5, System.Object P6, System.Object P7) { }
        // RVA: 0x063744D4  token: 0x60001F3
        public System.Void __Gen_Wrap_23(Beyond.ELogChannel P0, System.Object P1, System.Object P2, System.Object P3) { }
        // RVA: 0x063745CC  token: 0x60001F4
        public System.Void __Gen_Wrap_24(Beyond.ELogChannel P0, System.Object P1, System.Object P2, System.Object P3, System.Object P4) { }
        // RVA: 0x063746D8  token: 0x60001F5
        public System.Void __Gen_Wrap_25(Beyond.ELogChannel P0, System.Object P1, System.Object P2, System.Object P3, System.Object P4, System.Object P5) { }
        // RVA: 0x063747F0  token: 0x60001F6
        public System.Void __Gen_Wrap_26(Beyond.ELogChannel P0, System.Object P1, System.Object P2, System.Object P3, System.Object P4, System.Object P5, System.Object P6) { }
        // RVA: 0x06374918  token: 0x60001F7
        public System.Void __Gen_Wrap_27(Beyond.ELogChannel P0, System.Object P1, System.Object P2, System.Object P3, System.Object P4, System.Object P5, System.Object P6, System.Object P7) { }
        // RVA: 0x06374A50  token: 0x60001F8
        public System.Void __Gen_Wrap_28(Beyond.ELogChannel P0, System.Object P1, System.Object P2, System.Object P3, System.Object P4, System.Object P5, System.Object P6, System.Object P7, System.Object P8) { }
        // RVA: 0x06374B98  token: 0x60001F9
        public System.Void __Gen_Wrap_29(Beyond.ELogChannel P0, Beyond.EnableLogType P1, Beyond.EColorTag P2, System.Object P3, System.Object P4) { }
        // RVA: 0x06374CB0  token: 0x60001FA
        public System.Void __Gen_Wrap_30(Beyond.EnableLogType P0, System.Object P1) { }
        // RVA: 0x06374D8C  token: 0x60001FB
        public System.Void __Gen_Wrap_31(Beyond.EnableLogType P0, Beyond.ELogChannel P1, System.Object P2) { }
        // RVA: 0x06374E84  token: 0x60001FC
        public System.Void __Gen_Wrap_32(Beyond.EnableLogType P0, Beyond.ELogChannel P1, Beyond.EColorTag P2, System.Object P3) { }
        // RVA: 0x06374F7C  token: 0x60001FD
        public System.Void __Gen_Wrap_33(Beyond.EnableLogType P0, Beyond.ELogChannel P1, Beyond.EColorTag P2, System.Object P3, System.Object P4) { }
        // RVA: 0x06375088  token: 0x60001FE
        public System.Void __Gen_Wrap_34(System.Boolean P0) { }
        // RVA: 0x06375148  token: 0x60001FF
        public System.Void __Gen_Wrap_35(Beyond.ELogLevel P0, Beyond.ELogChannel P1, System.Object P2, System.Single P3) { }
        // RVA: 0x06375240  token: 0x6000200
        public System.Void __Gen_Wrap_36(Beyond.ELogChannel P0, System.Single P1, System.Object P2) { }
        // RVA: 0x06375324  token: 0x6000201
        public System.Void __Gen_Wrap_37(Beyond.ELogLevel P0, Beyond.ELogChannel P1, System.Object P2, System.Object P3) { }
        // RVA: 0x0637541C  token: 0x6000202
        public System.Void __Gen_Wrap_38(Beyond.ELogLevel P0, Beyond.ELogChannel P1, Beyond.EColorTag P2, System.Object P3, System.Object P4) { }
        // RVA: 0x06375528  token: 0x6000203
        public Beyond.DLogger.DebugContext __Gen_Wrap_39() { }
        // RVA: 0x063756AC  token: 0x6000204
        public Beyond.DLogger.DebugContextScope __Gen_Wrap_40(System.Object P0, Beyond.ELogLevel P1) { }
        // RVA: 0x063757A4  token: 0x6000205
        public System.String __Gen_Wrap_41(Beyond.ELogLevel P0) { }
        // RVA: 0x06375894  token: 0x6000206
        public System.Boolean __Gen_Wrap_43(UnityEngine.LowLevel.PlayerLoopSystem& P0) { }
        // RVA: 0x063759F0  token: 0x6000207
        public System.Void __Gen_Wrap_44(UnityEngine.LowLevel.PlayerLoopSystem P0, UnityEngine.LowLevel.PlayerLoopSystem& P1, System.Object P2, System.Object P3, System.Boolean P4, System.Boolean P5) { }
        // RVA: 0x06375BC8  token: 0x6000208
        public System.Void __Gen_Wrap_45(UnityEngine.LowLevel.PlayerLoopSystem& P0) { }
        // RVA: 0x06375D14  token: 0x6000209
        public System.Void __Gen_Wrap_46(Beyond.DLogger.DebugContextScope& P0) { }
        // RVA: 0x06375E2C  token: 0x600020A
        public System.UInt32 __Gen_Wrap_47(Beyond.Log.BitArray256& P0) { }
        // RVA: 0x06375F6C  token: 0x600020B
        public System.Boolean __Gen_Wrap_48(Beyond.Log.BitArray256& P0) { }
        // RVA: 0x063760AC  token: 0x600020C
        public System.String __Gen_Wrap_49(Beyond.Log.BitArray256& P0) { }
        // RVA: 0x06376284  token: 0x600020D
        public System.Boolean __Gen_Wrap_50(Beyond.Log.BitArray256& P0, System.Byte P1) { }
        // RVA: 0x063763F0  token: 0x600020E
        public System.Void __Gen_Wrap_51(Beyond.Log.BitArray256& P0, System.UInt32 P1, System.Boolean P2) { }
        // RVA: 0x06376554  token: 0x600020F
        public System.Void __Gen_Wrap_52(Beyond.Log.BitArray256& P0, System.Byte P1, System.Boolean P2) { }
        // RVA: 0x063766BC  token: 0x6000210
        public Beyond.Log.BitArray256 __Gen_Wrap_53(Beyond.Log.BitArray256 P0) { }
        // RVA: 0x063767E8  token: 0x6000211
        public Beyond.Log.BitArray256 __Gen_Wrap_54(Beyond.Log.BitArray256 P0, Beyond.Log.BitArray256 P1) { }
        // RVA: 0x06376950  token: 0x6000212
        public System.Boolean __Gen_Wrap_55(Beyond.Log.BitArray256 P0, Beyond.Log.BitArray256 P1) { }
        // RVA: 0x06376A84  token: 0x6000213
        public System.Boolean __Gen_Wrap_56(Beyond.Log.BitArray256& P0, System.Object P1) { }
        // RVA: 0x06376BE0  token: 0x6000214
        public System.Int32 __Gen_Wrap_57(Beyond.Log.BitArray256& P0) { }
        // RVA: 0x06376D20  token: 0x6000215
        public System.Void __Gen_Wrap_58(Beyond.Log.BitArray256& P0) { }
        // RVA: 0x06376E50  token: 0x6000216
        public Beyond.Log.BitArray256 __Gen_Wrap_59() { }
        // RVA: 0x063770B0  token: 0x6000217
        public System.Boolean __Gen_Wrap_60() { }
        // RVA: 0x06377148  token: 0x6000218
        public System.String __Gen_Wrap_61() { }
        // RVA: 0x06377214  token: 0x6000219
        public System.Int32 __Gen_Wrap_62() { }
        // RVA: 0x063772AC  token: 0x600021A
        public System.String __Gen_Wrap_63(System.Int64 P0, System.Int32 P1, System.Object P2, Beyond.ELogChannel P3, Beyond.EColorTag P4, Beyond.ELogLevel P5) { }
        // RVA: 0x06377400  token: 0x600021B
        public System.Void __Gen_Wrap_64(Beyond.ELogLevel P0, System.Object P1, System.Object P2, System.Object P3) { }
        // RVA: 0x06377508  token: 0x600021C
        public System.Void __Gen_Wrap_65(System.Object P0, Beyond.Log.FLogMessage& P1) { }
        // RVA: 0x0637768C  token: 0x600021D
        public Beyond.Log.FLogMessage __Gen_Wrap_66(Beyond.ELogChannel P0, System.Object P1, System.Object P2, UnityEngine.LogType P3) { }
        // RVA: 0x063777EC  token: 0x600021E
        public Beyond.UnSafeString __Gen_Wrap_67() { }
        // RVA: 0x063778A4  token: 0x600021F
        public System.Void __Gen_Wrap_68(System.Object P0, System.Object P1, UnityEngine.LogType P2) { }
        // RVA: 0x06377988  token: 0x6000220
        public System.Void __Gen_Wrap_69(System.Object P0, Beyond.Log.FLogMessage& P1, System.Single P2) { }
        // RVA: 0x04D373A0  token: 0x6000221
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000017  // size: 0x10
    public class WrappersManagerImpl
    {
        // Methods
        // RVA: 0x06378938  token: 0x6000222
        public static IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id) { }
        // RVA: 0x03330C20  token: 0x6000223
        public static System.Boolean IsPatched(System.Int32 id) { }

    }

}

