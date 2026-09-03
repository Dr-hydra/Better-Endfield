// ========================================================
// Dumped by @desirepro
// Assembly: Lua.Beyond.dll
// Classes:  48
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

CLASS: DispatchEventFunc
TYPE:  sealed class
TOKEN: 0x200000A
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x04CF41E0  token=0x6000011  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x038E1420  token=0x6000012  System.Void Invoke(System.String eventName, XLua.LuaTable luaTable)
  RVA=0x05E43E3C  token=0x6000013  System.IAsyncResult BeginInvoke(System.String eventName, XLua.LuaTable luaTable, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x6000014  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: <>c__DisplayClass9_0
TYPE:  sealed class
TOKEN: 0x200000B
SIZE:  0x20
FIELDS:
  public            Beyond.Lua.LuaEventSystem       <>4__this  // 0x10
  public            System.String                   eventName  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000015  System.Void .ctor()
  RVA=0x04CAEE20  token=0x6000016  System.Void <DispatchEvent>b__0()
END_CLASS

CLASS: <>c__DisplayClass10_0`1
TYPE:  sealed class
TOKEN: 0x200000C
FIELDS:
  public            Beyond.Lua.LuaEventSystem       <>4__this  // 0x0
  public            System.String                   eventName  // 0x0
  public            T                               arg  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000017  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000018  System.Void <DispatchEvent>b__0()
END_CLASS

CLASS: <>c__DisplayClass11_0`2
TYPE:  sealed class
TOKEN: 0x200000D
FIELDS:
  public            Beyond.Lua.LuaEventSystem       <>4__this  // 0x0
  public            System.String                   eventName  // 0x0
  public            T0                              arg0  // 0x0
  public            T1                              arg1  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000019  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600001A  System.Void <DispatchEvent>b__0()
END_CLASS

CLASS: <>c__DisplayClass12_0`3
TYPE:  sealed class
TOKEN: 0x200000E
FIELDS:
  public            Beyond.Lua.LuaEventSystem       <>4__this  // 0x0
  public            System.String                   eventName  // 0x0
  public            T0                              arg0  // 0x0
  public            T1                              arg1  // 0x0
  public            T2                              arg2  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600001B  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600001C  System.Void <DispatchEvent>b__0()
END_CLASS

CLASS: <>c__DisplayClass13_0`4
TYPE:  sealed class
TOKEN: 0x200000F
FIELDS:
  public            Beyond.Lua.LuaEventSystem       <>4__this  // 0x0
  public            System.String                   eventName  // 0x0
  public            T0                              arg0  // 0x0
  public            T1                              arg1  // 0x0
  public            T2                              arg2  // 0x0
  public            T3                              arg3  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600001D  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600001E  System.Void <DispatchEvent>b__0()
END_CLASS

CLASS: <>c__DisplayClass14_0`5
TYPE:  sealed class
TOKEN: 0x2000010
FIELDS:
  public            Beyond.Lua.LuaEventSystem       <>4__this  // 0x0
  public            System.String                   eventName  // 0x0
  public            T0                              arg0  // 0x0
  public            T1                              arg1  // 0x0
  public            T2                              arg2  // 0x0
  public            T3                              arg3  // 0x0
  public            T4                              arg4  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600001F  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000020  System.Void <DispatchEvent>b__0()
END_CLASS

CLASS: ValueType
TYPE:  sealed struct
TOKEN: 0x2000013
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.Lua.LuaCustomConfig.ValueTypeBool  // const
  public    static  Beyond.Lua.LuaCustomConfig.ValueTypeInt  // const
  public    static  Beyond.Lua.LuaCustomConfig.ValueTypeFloat  // const
  public    static  Beyond.Lua.LuaCustomConfig.ValueTypeString  // const
  public    static  Beyond.Lua.LuaCustomConfig.ValueTypeVector2  // const
  public    static  Beyond.Lua.LuaCustomConfig.ValueTypeVector3  // const
  public    static  Beyond.Lua.LuaCustomConfig.ValueTypeColor  // const
  public    static  Beyond.Lua.LuaCustomConfig.ValueTypeLua  // const
  public    static  Beyond.Lua.LuaCustomConfig.ValueTypeGameObject  // const
  public    static  Beyond.Lua.LuaCustomConfig.ValueTypeRectTransform  // const
  public    static  Beyond.Lua.LuaCustomConfig.ValueTypeAnimationCurve  // const
  public    static  Beyond.Lua.LuaCustomConfig.ValueTypeLayerMask  // const
  public    static  Beyond.Lua.LuaCustomConfig.ValueTypeMaterial  // const
  public    static  Beyond.Lua.LuaCustomConfig.ValueTypeEnum  // const
  public    static  Beyond.Lua.LuaCustomConfig.ValueTypeVector4  // const
METHODS:
END_CLASS

CLASS: Item
TYPE:  class
TOKEN: 0x2000014
SIZE:  0xD8
IMPLEMENTS: Beyond.ILuaCallCSharp
FIELDS:
  public            System.String                   name  // 0x10
  public            System.String                   description  // 0x18
  public            Beyond.Lua.LuaCustomConfig.ValueTypevalueType  // 0x20
  public            System.Boolean                  boolValue  // 0x24
  public            System.Int32                    intValue  // 0x28
  public            System.Single                   floatValue  // 0x2c
  public            System.String                   stringValue  // 0x30
  public            UnityEngine.Vector2             vector2Value  // 0x38
  public            UnityEngine.Vector3             vector3Value  // 0x40
  public            UnityEngine.Vector4             vector4Value  // 0x4c
  public            UnityEngine.Color               colorValue  // 0x5c
  public            System.String                   luaValue  // 0x70
  public            UnityEngine.GameObject          gameObjectValue  // 0x78
  public            UnityEngine.RectTransform       rectTransformValue  // 0x80
  public            UnityEngine.AnimationCurve      curveValue  // 0x88
  public            UnityEngine.LayerMask           layerMaskValue  // 0x90
  public            UnityEngine.Material            material  // 0x98
  public            System.String                   enumTypeAQName  // 0xa0
  public            System.String                   enumTypeFullName  // 0xa8
  public            System.String                   enumAssemblyName  // 0xb0
  public            System.Int32                    enumValue  // 0xb8
  public            System.String                   luaTableJson  // 0xc0
  private           System.String                   m_lastLuaValue  // 0xc8
  private           System.Type                     m_cachedEnumType  // 0xd0
METHODS:
  RVA=0x09EB6B90  token=0x6000038  System.Boolean HasLuaValueChanged()
  RVA=0x09EB6C48  token=0x6000039  System.Boolean ShouldSerializeLuaTableJson()
  RVA=0x09EB6BEC  token=0x600003A  System.Void MarkLuaValueProcessed()
  RVA=0x09EB67F0  token=0x600003B  System.Type GetEnumType()
  RVA=0x09EB69DC  token=0x600003C  System.Object GetEnumValueObject()
  RVA=0x03E6D660  token=0x600003D  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass15_0
TYPE:  sealed class
TOKEN: 0x2000015
SIZE:  0x20
FIELDS:
  public            System.Collections.Generic.List<System.Object>keys1  // 0x10
  public            System.Collections.Generic.List<System.Object>keys2  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600003E  System.Void .ctor()
  RVA=0x09EBC634  token=0x600003F  System.Void <_CompareLuaTables>b__0(System.Object k, System.Object _)
  RVA=0x09EBC688  token=0x6000040  System.Void <_CompareLuaTables>b__1(System.Object k, System.Object _)
END_CLASS

CLASS: <>c__DisplayClass23_0
TYPE:  sealed class
TOKEN: 0x2000016
SIZE:  0x18
FIELDS:
  public            Newtonsoft.Json.Linq.JObject    jObject  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000041  System.Void .ctor()
  RVA=0x09EBC6DC  token=0x6000042  System.Void <ConvertLuaTableToJson>b__0(System.Object key, System.Object value)
END_CLASS

CLASS: LuaHotFixInvoker
TYPE:  sealed class
TOKEN: 0x2000019
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x09323EA8  token=0x6000048  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x014E2060  token=0x6000049  System.Boolean Invoke(System.String luaCode)
  RVA=0x047FDD70  token=0x600004A  System.IAsyncResult BeginInvoke(System.String luaCode, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE668  token=0x600004B  System.Boolean EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: TickEvent
TYPE:  class
TOKEN: 0x200001E
SIZE:  0x40
EXTENDS: Beyond.DisposedUnityEvent`1
FIELDS:
METHODS:
  RVA=0x03EB2740  token=0x6000080  System.Void .ctor()
END_CLASS

CLASS: ComponentRefDict
TYPE:  class
TOKEN: 0x2000020
SIZE:  0x28
EXTENDS: Beyond.SerializeReferenceDictionary`2
FIELDS:
METHODS:
  RVA=0x03698670  token=0x600008A  System.Void .ctor()
END_CLASS

CLASS: CustomUIStyleInfo
TYPE:  class
TOKEN: 0x2000022
SIZE:  0x20
FIELDS:
  public            Beyond.UI.CustomUIStyle         style  // 0x10
  public            UnityEngine.Component           component  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600008C  System.Void .ctor()
END_CLASS

CLASS: RefExtraInfo
TYPE:  class
TOKEN: 0x2000021
SIZE:  0x18
FIELDS:
  public            System.Collections.Generic.List<Beyond.Lua.LuaReference.RefExtraInfo.CustomUIStyleInfo>customUIStyles  // 0x10
METHODS:
  RVA=0x047D3E80  token=0x600008B  System.Void .ctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000023
SIZE:  0x10
FIELDS:
  public    static readonly Beyond.Lua.LuaReference.<>c     <>9  // static @ 0x0
  public    static  System.Predicate<Beyond.Lua.LuaReference><>9__8_0  // static @ 0x8
METHODS:
  RVA=0x09EBC8A4  token=0x600008D  System.Void .cctor()
  RVA=0x041E1670  token=0x600008E  System.Void .ctor()
  RVA=0x09EBC5E8  token=0x600008F  System.Boolean <Reset>b__8_0(Beyond.Lua.LuaReference item)
END_CLASS

CLASS: ImageContentParam
TYPE:  class
TOKEN: 0x200002B
SIZE:  0x18
FIELDS:
  public            System.String                   path  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60000F6  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass34_0
TYPE:  sealed class
TOKEN: 0x200002C
SIZE:  0x20
FIELDS:
  public            System.Single                   from  // 0x10
  public            XLua.LuaFunction                setter  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60000F7  System.Void .ctor()
  RVA=0x04D85F70  token=0x60000F8  System.Single <TweenTo>b__0()
  RVA=0x09EBC7E0  token=0x60000F9  System.Void <TweenTo>b__1(System.Single value)
END_CLASS

CLASS: Beyond.Lua.CsToLuaClass
TYPE:  abstract class
TOKEN: 0x2000008
SIZE:  0x18
FIELDS:
  protected         XLua.LuaTable                   m_luaTable  // 0x10
METHODS:
  RVA=0x09EB60B8  token=0x6000007  System.Void .ctor(System.String luaPath)
END_CLASS

CLASS: Beyond.Lua.LuaEventSystem
TYPE:  class
TOKEN: 0x2000009
SIZE:  0x48
IMPLEMENTS: Beyond.ILuaEventSystem Beyond.ICommonCallLua System.IDisposable
FIELDS:
  private           Beyond.Lua.LuaEventSystem.DispatchEventFuncm_luaNotify  // 0x10
  private           System.IntPtr                   L  // 0x18
  private           XLua.ObjectTranslator           m_translator  // 0x20
  private           System.IntPtr                   m_eventNameBytesPtr  // 0x28
  private           System.Byte[]                   m_eventNameBytes  // 0x30
  private           XLua.LuaEnv                     m_luaEnv  // 0x38
  private           System.Int32                    csNotifyRef  // 0x40
METHODS:
  RVA=0x04852550  token=0x6000008  System.Void .ctor()
  RVA=0x0423A270  token=0x6000009  System.Void DispatchEvent(System.String eventName)
  RVA=-1  // generic def  token=0x600000A  System.Void DispatchEvent(System.String eventName, T arg)
  RVA=-1  // generic def  token=0x600000B  System.Void DispatchEvent(System.String eventName, T0 arg0, T1 arg1)
  RVA=-1  // generic def  token=0x600000C  System.Void DispatchEvent(System.String eventName, T0 arg0, T1 arg1, T2 arg2)
  RVA=-1  // generic def  token=0x600000D  System.Void DispatchEvent(System.String eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3)
  RVA=-1  // generic def  token=0x600000E  System.Void DispatchEvent(System.String eventName, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
  RVA=0x03124F10  token=0x600000F  System.Void PushEventName(System.String eventName)
  RVA=0x09EB9230  token=0x6000010  System.Void Dispose()
END_CLASS

CLASS: Beyond.Lua.CSharpCallLuaRegistration
TYPE:  class
TOKEN: 0x2000011
SIZE:  0x18
FIELDS:
  private           Beyond.Lua.LuaEventSystem       m_luaEventSystem  // 0x10
METHODS:
  RVA=0x048523D0  token=0x6000021  System.Void Init()
  RVA=0x09EB6018  token=0x6000022  System.Void Dispose()
  RVA=0x041E1670  token=0x6000023  System.Void .ctor()
END_CLASS

CLASS: Beyond.Lua.LuaCustomConfig
TYPE:  class
TOKEN: 0x2000012
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Beyond.ILuaCallCSharp UnityEngine.ISerializationCallbackReceiver
FIELDS:
  public            System.Collections.Generic.List<Beyond.Lua.LuaCustomConfig.Item>itemList  // 0x18
  private           System.Collections.Generic.Dictionary<System.String,Beyond.Lua.LuaCustomConfig.Item>m_itemDict  // 0x20
PROPERTIES:
  itemDict  get=0x09EB904C
METHODS:
  RVA=0x0441E0E0  token=0x6000025  System.Void OnBeforeSerialize()
  RVA=0x0434A2A0  token=0x6000026  System.Void OnAfterDeserialize()
  RVA=0x09EB8AE0  token=0x6000027  System.Boolean _ProcessLuaTableItem(Beyond.Lua.LuaCustomConfig.Item item)
  RVA=0x09EB7100  token=0x6000028  System.Boolean ShouldProcessEnumItem(Beyond.Lua.LuaCustomConfig.Item item)
  RVA=0x09EB890C  token=0x6000029  System.Boolean _ProcessEnumItem(Beyond.Lua.LuaCustomConfig.Item item)
  RVA=0x09EB8D5C  token=0x600002A  System.Boolean _ValidateLuaTableConsistency(System.String luaCode, System.String json)
  RVA=0x09EB7BD8  token=0x600002B  XLua.LuaTable _JsonToLuaTableForValidation(System.String json, XLua.LuaEnv luaEnv)
  RVA=0x09EB7ECC  token=0x600002C  System.Void _PopulateLuaTableFromJTokenForValidation(XLua.LuaTable luaTable, Newtonsoft.Json.Linq.JToken jToken, XLua.LuaEnv luaEnv)
  RVA=0x09EB7754  token=0x600002D  System.Object _ConvertJValueToObjectForValidation(Newtonsoft.Json.Linq.JToken jValue)
  RVA=0x09EB71A0  token=0x600002E  System.Boolean _CompareLuaTables(XLua.LuaTable table1, XLua.LuaTable table2)
  RVA=0x09EB760C  token=0x600002F  System.Boolean _CompareValues(System.Object value1, System.Object value2)
  RVA=0x09EB7A4C  token=0x6000030  System.Boolean _IsNumericType(System.Object obj)
  RVA=0x03F27100  token=0x6000031  System.Void InitConfigTable(XLua.LuaTable configTable)
  RVA=0x09EB7D24  token=0x6000032  XLua.LuaTable _JsonToLuaTable(System.String json, XLua.LuaEnv luaEnv)
  RVA=0x09EB8364  token=0x6000033  System.Void _PopulateLuaTableFromJToken(XLua.LuaTable luaTable, Newtonsoft.Json.Linq.JToken jToken)
  RVA=0x09EB78CC  token=0x6000034  System.Object _ConvertJValueToObject(Newtonsoft.Json.Linq.JToken jValue)
  RVA=0x09EB6CB8  token=0x6000035  System.String ConvertLuaCodeToJson(System.String luaCode)
  RVA=0x09EB6F84  token=0x6000036  System.String ConvertLuaTableToJson(XLua.LuaTable luaTable)
  RVA=0x03698ED0  token=0x6000037  System.Void .ctor()
END_CLASS

CLASS: Beyond.Lua.LuaDebugSocketManager
TYPE:  class
TOKEN: 0x2000017
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
METHODS:
  RVA=0x04CC3270  token=0x6000043  System.Void .ctor()
END_CLASS

CLASS: Beyond.Lua.LuaHotFix
TYPE:  static class
TOKEN: 0x2000018
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09EB97A0  token=0x6000044  System.Void LuaHotFixTest()
  RVA=0x09EB96A8  token=0x6000045  System.Boolean HotFixLua(System.String lua)
  RVA=0x09EB92CC  token=0x6000046  System.Boolean HotFixLua(System.Byte[] lua)
  RVA=0x09EB9364  token=0x6000047  System.Boolean HotFixLua(System.Span<System.Byte> lua, System.Int32 length)
END_CLASS

CLASS: Beyond.Lua.LuaLogBuffer
TYPE:  static class
TOKEN: 0x200001A
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: Beyond.Lua.LoadLuaProxy
TYPE:  class
TOKEN: 0x200001B
SIZE:  0x10
IMPLEMENTS: XLua.ILoadLuaProxy
FIELDS:
METHODS:
  RVA=0x04CE2CF0  token=0x600004C  System.Void _RunOnStart()
  RVA=0x03600DA0  token=0x600004D  System.Byte[] LoadLua(System.String name)
  RVA=0x041E1670  token=0x600004E  System.Void .ctor()
END_CLASS

CLASS: Beyond.Lua.LuaMethodPointerModule
TYPE:  class
TOKEN: 0x200001C
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x04B32CB0  token=0x600004F  System.Void _RunOnStart()
  RVA=0x041E1670  token=0x6000050  System.Void .ctor()
END_CLASS

CLASS: Beyond.Lua.LuaManager
TYPE:  class
TOKEN: 0x200001D
SIZE:  0xC0
EXTENDS: Beyond.BaseTickManager
IMPLEMENTS: Beyond.ILuaProxy
FIELDS:
  private   static  Beyond.Lua.LuaManager           <instance>k__BackingField  // static @ 0x0
  private   static  System.Single                   TICK_INTERVAL  // const
  private   readonly Beyond.Lua.CSharpCallLuaRegistrationm_csCallLua  // 0x70
  private           Beyond.Gameplay.PeriodicTimer   m_envTickTimer  // 0x78
  public    static  System.Boolean                  s_xluaReflectionOpt  // static @ 0x8
  private           XLua.LuaEnv                     <luaEnv>k__BackingField  // 0x80
  public            System.Action<XLua.LuaTable,System.String,Beyond.Lua.LuaUIWidget>wrapUIWidgetFunction  // 0x88
  public            System.Action<XLua.LuaTable,System.String,Beyond.Lua.LuaReference>bindLuaRefFunction  // 0x90
  private           System.Boolean                  m_isQuittingApplication  // 0x98
  public            Beyond.Lua.LuaManager.TickEvent actionTick  // 0xa0
  public            Beyond.Lua.LuaManager.TickEvent actionLateTick  // 0xa8
  public            Beyond.Lua.LuaManager.TickEvent actionTailTick  // 0xb0
  public            Beyond.Lua.LuaManager.TickEvent actionRenderDone  // 0xb8
PROPERTIES:
  instance  get=0x04D613F0  set=0x09EBA0E0
  luaEnv  get=0x04D86260  set=0x04CD9970
  isQuittingApplication  get=0x09EBA094  set=0x09EBA13C
  frameTickGroup  get=0x04D7D310
METHODS:
  RVA=0x048162C0  token=0x6000053  System.Void CreateInstance()
  RVA=0x048163A0  token=0x6000054  System.Void DestroyInstance()
  RVA=0x03EB25B0  token=0x6000055  System.Void .ctor()
  RVA=0x03A74140  token=0x6000058  System.Boolean IsEnableMultithreadGC()
  RVA=0x032F1550  token=0x6000059  System.Boolean IsLuaGCing()
  RVA=0x042D2660  token=0x600005A  System.Void OnInit()
  RVA=0x032F1320  token=0x600005B  System.String GetLuaStack()
  RVA=0x04A79AD0  token=0x600005C  System.Void OnRelease()
  RVA=0x09EB9AF0  token=0x600005D  System.Void ReStartLuaProfiler()
  RVA=0x09EB9ECC  token=0x6000060  System.Void _ReleaseDelegateInAnotherStackFrame()
  RVA=0x0301EB50  token=0x6000061  System.Void Tick(System.Single deltaTime)
  RVA=0x0301F840  token=0x6000062  System.Void LateTick(System.Single deltaTime)
  RVA=0x0301F9D0  token=0x6000063  System.Void TailLateTick(System.Single deltaTime)
  RVA=0x09EB9878  token=0x6000064  System.String GetLuaFileRealPath(System.String modulePath)
  RVA=0x02F21AB0  token=0x6000065  System.Boolean IsLuaFileExist(System.String modulePath)
  RVA=0x09EB98FC  token=0x6000066  System.Int64 LoadLuaByteByFileRealPath(System.String modulePath, System.Int32& len)
  RVA=0x03CDC650  token=0x6000067  System.Byte[] LoadLua(System.String modulePath)
  RVA=0x09EB99C4  token=0x6000068  System.IntPtr LoadLuaBytePtr(System.String modulePath)
  RVA=0x049CB280  token=0x6000069  System.Byte[] LoadLuaBytes(System.String& modulePath)
  RVA=0x04852390  token=0x600006A  System.Void _RegisterCSharpCallLua()
  RVA=0x03601780  token=0x600006B  System.Void _Register3rdPartyLibraries(XLua.LuaEnv env)
  RVA=0x09EB9BF4  token=0x600006C  System.Void _HyperLuaLogCallback(System.IntPtr L, System.Int32 level, System.String text)
  RVA=0x09EB9CE0  token=0x600006D  System.Void _LuaUtilsLogCallback(System.Int32 level, System.String text)
  RVA=0x09EB9D70  token=0x600006E  System.Void _OnEnvLangChanged(Beyond.GEnums.EnvLang lang)
  RVA=0x09EB97E0  token=0x600006F  System.Object[] DoString(System.String code)
  RVA=0x038B54E0  token=0x6000070  System.IntPtr GetLuaState()
  RVA=0x04034F10  token=0x6000071  System.Void ClearAllUnityObjectUserdata()
  RVA=0x09EB9E44  token=0x6000072  System.Void _OnRenderDone(HG.Rendering.Runtime.HGRenderPipeCallbackArgs args)
  RVA=0x0301FC60  token=0x6000073  System.Void _OnRenderDone()
  RVA=0x0488B620  token=0x6000074  System.Boolean AddLuaEventAfterGCDo(UnityEngine.Events.UnityAction action)
  RVA=0x09EB9B30  token=0x6000075  System.Void RemoveLuaEventAfterGCDo(UnityEngine.Events.UnityAction action)
  RVA=0x0423A400  token=0x6000076  System.Void AddOnceLuaEventAfterGCDo(UnityEngine.Events.UnityAction action)
  RVA=0x03EC1580  token=0x6000077  System.Int32 LuaCalculateABPathHash(System.IntPtr L)
  RVA=0x02F34080  token=0x6000078  System.Void CasterStringPathHash(System.IntPtr L, System.Int32 index, Beyond.Resource.StringPathHash& o)
  RVA=0x02F33240  token=0x6000079  System.Boolean CheckStringPathHash(System.IntPtr L, System.Int32 index)
  RVA=0x04D7A1D0  token=0x600007B  System.Void .cctor()
  RVA=0x06AC5358  token=0x600007C  System.Void <>iFixBaseProxy_Tick(System.Single P0)
  RVA=0x06AC5328  token=0x600007D  System.Void <>iFixBaseProxy_LateTick(System.Single P0)
  RVA=0x06ADDC58  token=0x600007E  System.Void <>iFixBaseProxy_TailLateTick(System.Single P0)
  RVA=0x06D78630  token=0x600007F  Beyond.FrameTickGroup <>iFixBaseProxy_get_frameTickGroup()
END_CLASS

CLASS: Beyond.Lua.LuaReference
TYPE:  class
TOKEN: 0x200001F
SIZE:  0x48
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Beyond.ILuaCallCSharp
FIELDS:
  public            Beyond.Lua.LuaReference.ComponentRefDictrefDict  // 0x18
  public            Beyond.SerializeReferenceDictionary<System.String,Beyond.Lua.LuaReference.RefExtraInfo>refExtraInfoDict  // 0x20
  public            System.Boolean                  isRootRef  // 0x28
  public            System.Collections.Generic.List<Beyond.Lua.LuaReference>subReferences  // 0x30
  private           XLua.LuaTable                   m_table  // 0x38
  private           System.Collections.Generic.Dictionary<System.String,UnityEngine.Component>m_customUIStyleComp  // 0x40
METHODS:
  RVA=0x09EBA88C  token=0x6000081  System.Void Reset()
  RVA=0x032EF680  token=0x6000082  System.Void OnDestroy()
  RVA=0x032EFC60  token=0x6000083  System.Void _UnBindFromXluaRef()
  RVA=0x032EF6F0  token=0x6000084  System.Void BindToLua(XLua.LuaTable table)
  RVA=0x032F0170  token=0x6000085  System.Void _BindSelfReferences(XLua.LuaTable table)
  RVA=0x09EBA6A8  token=0x6000086  System.Boolean RefreshCustomUIStyleBindings()
  RVA=0x09EBAB20  token=0x6000087  System.Boolean _RefreshSelfCustomUIStyleBindings(XLua.LuaTable table)
  RVA=0x09EBA1C4  token=0x6000088  System.Boolean ContainsGameObject(UnityEngine.GameObject obj, System.Boolean recursive)
  RVA=0x03698340  token=0x6000089  System.Void .ctor()
END_CLASS

CLASS: Beyond.Lua.LuaTestReflectionOptStruct
TYPE:  sealed struct
TOKEN: 0x2000024
SIZE:  0x48
FIELDS:
  public            System.Int32                    a  // 0x10
  public            System.String                   b  // 0x18
  public            System.Int64                    l  // 0x20
  public            System.UInt64                   ul  // 0x28
  public            System.Single                   f  // 0x30
  public            System.Double                   d  // 0x38
  public            System.Boolean                  bl  // 0x40
PROPERTIES:
  a1  get=0x09EBBD7C
  b1  get=0x09EBBDC4
  l1  get=0x09EBBEF0
  ul1  get=0x09EBBF3C
  f1  get=0x09EBBEA4
  d1  get=0x09EBBE58
  bl1  get=0x09EBBE10
METHODS:
  RVA=0x09EBBD34  token=0x6000097  System.Void .ctor(System.Int32 a, System.String b, System.Int64 l, System.UInt64 ul, System.Single f, System.Double d, System.Boolean bl)
  RVA=0x09EBBB14  token=0x6000098  System.Int32 TestInt(System.Int32 a)
  RVA=0x09EBBCF4  token=0x6000099  System.Void TestVoid()
  RVA=0x09EBBC7C  token=0x600009A  System.Void TestStructParam(Beyond.Lua.LuaTestReflectionOptStruct testStruct)
  RVA=0x09EBBC2C  token=0x600009B  System.Void TestStructParam2(Beyond.Lua.LuaTestReflectionOptStruct& testStruct)
  RVA=0x09EBBBB8  token=0x600009C  System.Object TestReturnObj()
  RVA=0x09EBBB68  token=0x600009D  System.Void TestObjParam(System.Object obj)
END_CLASS

CLASS: Beyond.Lua.LuaTestReflectionOptStruct2
TYPE:  sealed struct
TOKEN: 0x2000025
SIZE:  0x48
FIELDS:
  public            System.Int32                    a  // 0x10
  public            System.String                   b  // 0x18
  public            System.Int64                    l  // 0x20
  public            System.UInt64                   ul  // 0x28
  public            System.Single                   f  // 0x30
  public            System.Double                   d  // 0x38
  public            System.Boolean                  bl  // 0x40
PROPERTIES:
  a1  get=0x09EBB908
  b1  get=0x09EBB950
  l1  get=0x09EBBA7C
  ul1  get=0x09EBBAC8
  f1  get=0x09EBBA30
  d1  get=0x09EBB9E4
  bl1  get=0x09EBB99C
METHODS:
  RVA=0x09EBB6E8  token=0x60000A5  System.Int32 TestInt(System.Int32 a)
  RVA=0x09EBB8C8  token=0x60000A6  System.Void TestVoid()
  RVA=0x09EBB850  token=0x60000A7  System.Void TestStructParam(Beyond.Lua.LuaTestReflectionOptStruct2 testStruct)
  RVA=0x09EBB800  token=0x60000A8  System.Void TestStructParam2(Beyond.Lua.LuaTestReflectionOptStruct2& testStruct)
  RVA=0x09EBB78C  token=0x60000A9  System.Object TestReturnObj()
  RVA=0x09EBB73C  token=0x60000AA  System.Void TestObjParam(System.Object obj)
END_CLASS

CLASS: Beyond.Lua.ETestEnum
TYPE:  sealed struct
TOKEN: 0x2000026
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.Lua.ETestEnum            Test1  // const
  public    static  Beyond.Lua.ETestEnum            Test2  // const
METHODS:
END_CLASS

CLASS: Beyond.Lua.LuaTestReflectionOpt2
TYPE:  class
TOKEN: 0x2000027
SIZE:  0x20
FIELDS:
  public            System.Collections.Generic.List<System.Int32>TestList  // 0x10
  public            System.Collections.Generic.Dictionary<System.Int32,System.String>TestDict  // 0x18
METHODS:
  RVA=0x09EBB51C  token=0x60000AB  System.Void TestVoid()
  RVA=0x09EBB168  token=0x60000AC  System.Int32 TestInt(System.Int32 a)
  RVA=0x09EBB398  token=0x60000AD  System.Void TestStructParam(Beyond.Lua.LuaTestReflectionOptStruct testStruct)
  RVA=0x09EBB320  token=0x60000AE  System.Void TestStructParam(Beyond.Lua.LuaTestReflectionOptStruct2 testStruct)
  RVA=0x09EBB2D0  token=0x60000AF  System.Void TestStructParam2(Beyond.Lua.LuaTestReflectionOptStruct& testStruct)
  RVA=0x09EBB25C  token=0x60000B0  System.Object TestReturnObj()
  RVA=0x09EBB1BC  token=0x60000B1  System.Void TestObjParam(System.Object obj)
  RVA=0x09EBB114  token=0x60000B2  System.Void TestEnum(Beyond.Lua.ETestEnum e)
  RVA=0x09EBB20C  token=0x60000B3  Beyond.Lua.ETestEnum TestReturnEnum()
  RVA=0x09EBB410  token=0x60000B4  Beyond.Lua.LuaTestReflectionOptStruct TestStruct(Beyond.Lua.LuaTestReflectionOptStruct a)
  RVA=0x09EBB55C  token=0x60000B5  System.Void .ctor()
END_CLASS

CLASS: Beyond.Lua.LuaTestReflectionOpt
TYPE:  class
TOKEN: 0x2000028
SIZE:  0x20
FIELDS:
  public            System.Collections.Generic.List<System.Int32>TestList  // 0x10
  public            System.Collections.Generic.Dictionary<System.Int32,System.String>TestDict  // 0x18
METHODS:
  RVA=0x09EBC3A8  token=0x60000B6  System.Void TestVoid()
  RVA=0x09EBBFDC  token=0x60000B7  System.Int32 TestInt(System.Int32 a)
  RVA=0x09EBC220  token=0x60000B8  System.Void TestStructParam(Beyond.Lua.LuaTestReflectionOptStruct testStruct)
  RVA=0x09EBC1A4  token=0x60000B9  System.Void TestStructParam(Beyond.Lua.LuaTestReflectionOptStruct2 testStruct)
  RVA=0x09EBC150  token=0x60000BA  System.Void TestStructParam2(Beyond.Lua.LuaTestReflectionOptStruct& testStruct)
  RVA=0x09EBC0D8  token=0x60000BB  System.Object TestReturnObj()
  RVA=0x09EBC034  token=0x60000BC  System.Void TestObjParam(System.Object obj)
  RVA=0x09EBBF88  token=0x60000BD  System.Void TestEnum(Beyond.Lua.ETestEnum e)
  RVA=0x09EBC088  token=0x60000BE  Beyond.Lua.ETestEnum TestReturnEnum()
  RVA=0x09EBC29C  token=0x60000BF  Beyond.Lua.LuaTestReflectionOptStruct TestStruct(Beyond.Lua.LuaTestReflectionOptStruct a)
  RVA=0x09EBC3EC  token=0x60000C0  System.Void .ctor()
END_CLASS

CLASS: Beyond.Lua.LuaUIWidget
TYPE:  class
TOKEN: 0x2000029
SIZE:  0x48
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Beyond.UI.IUIClearable Beyond.ILuaCallCSharp
FIELDS:
  public            System.String                   id  // 0x18
  public            System.Boolean                  isEmbeddedWidget  // 0x20
  private           XLua.LuaTable                   <table>k__BackingField  // 0x28
  public            XLua.LuaFunction                onEnable  // 0x30
  public            XLua.LuaFunction                onDisable  // 0x38
  public            XLua.LuaFunction                onDestroy  // 0x40
PROPERTIES:
  table  get=0x04D86240  set=0x044ECC30
METHODS:
  RVA=0x09EBC578  token=0x60000C3  System.Void Reset()
  RVA=0x0321CC80  token=0x60000C4  System.Void OnEnable()
  RVA=0x0321CCD0  token=0x60000C5  System.Void OnDisable()
  RVA=0x04209D50  token=0x60000C6  System.Void OnDestroy()
  RVA=0x032EF120  token=0x60000C7  System.Void ClearComponent()
  RVA=0x03695570  token=0x60000C8  System.Void .ctor()
END_CLASS

CLASS: Beyond.Lua.UtilsForLua
TYPE:  static class
TOKEN: 0x200002A
SIZE:  0x10
FIELDS:
  private   static  UnityEngine.Vector3[]           s_worldCorners  // static @ 0x0
METHODS:
  RVA=0x04B4E940  token=0x60000C9  UnityEngine.GameObject CreateObject(UnityEngine.GameObject asset)
  RVA=0x03B1CCD0  token=0x60000CA  UnityEngine.GameObject CreateObject(UnityEngine.GameObject asset, UnityEngine.Transform parent)
  RVA=0x03B1CC50  token=0x60000CB  UnityEngine.GameObject CreateObject(UnityEngine.GameObject asset, UnityEngine.GameObject parent)
  RVA=0x09EBE220  token=0x60000CC  System.Void UIContainerResize(UnityEngine.Transform container, System.Int32 size, System.Int32 templateIndex)
  RVA=0x09EBDFCC  token=0x60000CD  System.Int32 TurnOffLayer(System.Int32 value, System.String layerName)
  RVA=0x09EBE030  token=0x60000CE  System.Int32 TurnOnLayer(System.Int32 value, System.String layerName)
  RVA=0x09EBDE5C  token=0x60000CF  System.Int32 ToggleLayer(System.Int32 value, System.String layerName)
  RVA=0x09EBD7DC  token=0x60000D0  System.Void SetPosX(UnityEngine.Transform t, System.Single value)
  RVA=0x09EBD87C  token=0x60000D1  System.Void SetPosY(UnityEngine.Transform t, System.Single value)
  RVA=0x09EBD91C  token=0x60000D2  System.Void SetPosZ(UnityEngine.Transform t, System.Single value)
  RVA=0x09EBD9B0  token=0x60000D3  System.Void SetRotX(UnityEngine.Transform t, System.Single value)
  RVA=0x09EBDA50  token=0x60000D4  System.Void SetRotY(UnityEngine.Transform t, System.Single value)
  RVA=0x09EBDAF0  token=0x60000D5  System.Void SetRotZ(UnityEngine.Transform t, System.Single value)
  RVA=0x09EBDB84  token=0x60000D6  System.Void SetScaleX(UnityEngine.Transform t, System.Single value)
  RVA=0x09EBDC24  token=0x60000D7  System.Void SetScaleY(UnityEngine.Transform t, System.Single value)
  RVA=0x09EBDCC4  token=0x60000D8  System.Void SetScaleZ(UnityEngine.Transform t, System.Single value)
  RVA=0x03DC7E60  token=0x60000D9  System.Void ClearUIComponents(UnityEngine.GameObject gameObject)
  RVA=0x09EBD4D4  token=0x60000DA  System.Boolean RayCast(UnityEngine.RaycastHit& hitInfo, UnityEngine.Ray ray, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction interaction)
  RVA=0x09EBD5D0  token=0x60000DB  System.Int32 RaycastNonAlloc(UnityEngine.Ray ray, UnityEngine.RaycastHit[] results, System.Single maxDistance, System.Int32 layerMask, UnityEngine.QueryTriggerInteraction interaction)
  RVA=0x037D7060  token=0x60000DC  UnityEngine.Bounds GetRectTransformBounds(UnityEngine.RectTransform transform)
  RVA=0x09EBD6CC  token=0x60000DD  UnityEngine.Rect RectTransformToScreenRect(UnityEngine.RectTransform rectTransform, UnityEngine.Camera uiCamera)
  RVA=0x09EBD02C  token=0x60000DE  UnityEngine.Vector3 GetRectTransformCenterPosition(UnityEngine.RectTransform rectTransform)
  RVA=0x09EBC908  token=0x60000DF  UnityEngine.Bounds CalcBoundOfRectTransform(UnityEngine.RectTransform transform, UnityEngine.RectTransform local)
  RVA=0x09EBCB54  token=0x60000E0  UnityEngine.Vector2 ClampPointInRectTransform(UnityEngine.RectTransform source, UnityEngine.RectTransform target, UnityEngine.Vector2 sourcePoint)
  RVA=0x09EBD1C4  token=0x60000E1  System.Collections.Generic.List<System.String> GetStringList()
  RVA=0x09EBD160  token=0x60000E2  System.Int32 GetStringHash(System.String str)
  RVA=0x09EBCBFC  token=0x60000E3  System.Single EntityDistance(Beyond.Gameplay.Core.Entity a, Beyond.Gameplay.Core.Entity b)
  RVA=0x09EBD254  token=0x60000E4  System.Void OpenAccountCenter()
  RVA=0x09EBCCB8  token=0x60000E5  System.Void ExitGame()
  RVA=0x09EBCF0C  token=0x60000E6  System.String GetCurrentUID()
  RVA=0x09EBDD58  token=0x60000E7  System.Boolean StartPayment(System.String productId, System.String signParams)
  RVA=0x09EBDF34  token=0x60000E8  System.Boolean TryGetCharacterVolume(UnityEngine.Rendering.Volume volume, HG.Rendering.Runtime.HGCharacterVolume& hgCharacterVolume)
  RVA=0x09EBCD38  token=0x60000E9  System.Single GetAnimationCurveLength(UnityEngine.AnimationCurve curve)
  RVA=0x09EBE094  token=0x60000EA  DG.Tweening.Tweener TweenTo(System.Single from, System.Single to, System.Single duration, XLua.LuaFunction setter)
  RVA=0x04086AE0  token=0x60000EB  System.Boolean IsNull(UnityEngine.Object o)
  RVA=0x09EBD2C0  token=0x60000EC  Beyond.Lua.UtilsForLua.ImageContentParam ParseImageContent(System.String xml)
  RVA=0x09EBD760  token=0x60000ED  System.Void ReturnToLogin()
  RVA=0x09EBD484  token=0x60000EE  System.Void QuitGame(System.Int32 exitCode)
  RVA=0x09EBC9C0  token=0x60000EF  System.Boolean CheckExtremeDangerousByLevel(System.Int32 level)
  RVA=0x09EBDDE8  token=0x60000F0  System.Void ToggleCharInfoInUpgradePanelOption(System.Boolean isIn)
  RVA=0x09EBDEC0  token=0x60000F1  System.Void ToggleWeaponInUpgradePanelOption(System.Boolean isIn)
  RVA=0x09EBCE60  token=0x60000F2  Cinemachine.CinemachineTransposer GetCinemachineTransposer(Cinemachine.CinemachineVirtualCamera vcam)
  RVA=0x09EBCDB4  token=0x60000F3  Cinemachine.CinemachineComposer GetCinemachineComposer(Cinemachine.CinemachineVirtualCamera vcam)
  RVA=0x09EBCF90  token=0x60000F4  CinemachineCameraOffset GetOrAddCinemachineCameraOffset(Cinemachine.CinemachineVirtualCamera vcam)
  RVA=0x04D36290  token=0x60000F5  System.Void .cctor()
END_CLASS

CLASS: Beyond.SourceGenerator.CameraControlConfigAttribute
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000002  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.AnimatorBlackboardUtility
TYPE:  static class
TOKEN: 0x2000004
SIZE:  0x10
FIELDS:
  private   static  System.Boolean                  s_enableFastPathBuffer  // static @ 0x0
METHODS:
  RVA=-1  // generic def  token=0x6000003  System.Void ClearAndEnsureDictionaryCapacity(System.Collections.Generic.Dictionary<TKey,TValue>& dict, System.Int32 capacity)
END_CLASS

CLASS: Beyond.SourceGenerator.AnimatorBlackboardAttribute
TYPE:  class
TOKEN: 0x2000005
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000004  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.DataNAttribute
TYPE:  class
TOKEN: 0x2000006
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  public            System.Int32                    capacity  // 0x10
  public            System.Int32                    tSize  // 0x14
  public            System.Boolean                  ring  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000005  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.ECSComponentAttribute
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public            System.Runtime.InteropServices.LayoutKindlayoutKind  // 0x10
  public            System.Boolean                  isTag  // 0x14
METHODS:
  RVA=0x041E1670  token=0x6000006  System.Void .ctor()
END_CLASS

CLASS: HG.Rendering.Runtime.HGConstantBufferLayoutAttribute
TYPE:  class
TOKEN: 0x2000002
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000001  System.Void .ctor()
END_CLASS

CLASS: IFix.ILFixDynamicMethodWrapper
TYPE:  class
TOKEN: 0x200002D
SIZE:  0x28
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
  private           System.Int32                    methodId  // 0x18
  private           System.Object                   anonObj  // 0x20
  public    static  IFix.ILFixDynamicMethodWrapper[]wrapperArray  // static @ 0x0
METHODS:
  RVA=0x032B2C30  token=0x60000FA  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj)
  RVA=0x06377B18  token=0x60000FB  System.Void __Gen_Wrap_0(System.Object P0, System.Object P1)
  RVA=0x063623A8  token=0x60000FC  System.Boolean __Gen_Wrap_1()
  RVA=0x063625A4  token=0x60000FD  System.Void __Gen_Wrap_2()
  RVA=0x063633B4  token=0x60000FE  System.Void __Gen_Wrap_3(System.Object P0)
  RVA=0x09EB6370  token=0x60000FF  System.Collections.Generic.Dictionary<System.String,Beyond.Lua.LuaCustomConfig.Item> __Gen_Wrap_4(System.Object P0)
  RVA=0x09EB644C  token=0x6000100  System.String __Gen_Wrap_5(System.Object P0)
  RVA=0x09EB6528  token=0x6000101  System.Object __Gen_Wrap_6(System.Object P0, System.Object P1)
  RVA=0x06373E64  token=0x6000102  System.Void __Gen_Wrap_7(System.Object P0, System.Object P1, System.Object P2, System.Object P3)
  RVA=0x09EB6634  token=0x6000103  XLua.LuaTable __Gen_Wrap_8(System.Object P0, System.Object P1, System.Object P2)
  RVA=0x06366BC4  token=0x6000104  System.Boolean __Gen_Wrap_9(System.Object P0, System.Object P1)
  RVA=0x066D3350  token=0x6000105  System.Boolean __Gen_Wrap_10(System.Object P0, System.Object P1, System.Object P2)
  RVA=0x06364290  token=0x6000106  System.Void __Gen_Wrap_11(System.Object P0, System.Object P1, System.Object P2)
  RVA=0x09EB6190  token=0x6000107  System.Type __Gen_Wrap_12(System.Object P0)
  RVA=0x09EB6280  token=0x6000108  System.Object __Gen_Wrap_13(System.Object P0)
  RVA=0x063632E4  token=0x6000109  System.Boolean __Gen_Wrap_14(System.Object P0)
  RVA=0x02D54B10  token=0x600010A  System.Boolean __Gen_Wrap_15(System.Span<System.Byte> P0, System.Int32 P1)
  RVA=0x02D550D0  token=0x600010B  System.Byte[] __Gen_Wrap_16(System.Object P0, System.Object P1)
  RVA=0x02D55B00  token=0x600010C  System.Void __Gen_Wrap_17(System.Object P0, HG.Rendering.Runtime.HGRenderPipeCallbackArgs P1)
  RVA=0x02D56180  token=0x600010D  System.String __Gen_Wrap_18()
  RVA=0x01AF7A70  token=0x600010E  System.Void __Gen_Wrap_19(System.IntPtr P0, System.Int32 P1, System.Object P2)
  RVA=0x01FBD880  token=0x600010F  System.Void __Gen_Wrap_20(System.Int32 P0, System.Object P1)
  RVA=0x02D569C0  token=0x6000110  System.Byte[] __Gen_Wrap_21(System.Object P0, System.String& P1)
  RVA=0x012A0D50  token=0x6000111  System.Void __Gen_Wrap_22(System.Object P0, Beyond.GEnums.EnvLang P1)
  RVA=0x01B59610  token=0x6000112  System.Int32 __Gen_Wrap_23(System.IntPtr P0)
  RVA=0x02D57720  token=0x6000113  System.Void __Gen_Wrap_24(System.IntPtr P0, System.Int32 P1, Beyond.Resource.StringPathHash& P2)
  RVA=0x02D58010  token=0x6000114  System.Boolean __Gen_Wrap_25(System.IntPtr P0, System.Int32 P1)
  RVA=0x02D58240  token=0x6000115  System.Void __Gen_Wrap_26(System.Object P0, System.Boolean P1)
  RVA=0x01410050  token=0x6000116  System.Void __Gen_Wrap_27(System.Object P0, System.Single P1)
  RVA=0x02D58520  token=0x6000117  System.String __Gen_Wrap_28(System.Object P0, System.Object P1)
  RVA=0x02D58F40  token=0x6000118  System.Int64 __Gen_Wrap_29(System.Object P0, System.Object P1, System.Int32& P2)
  RVA=0x02D59310  token=0x6000119  System.IntPtr __Gen_Wrap_30(System.Object P0, System.Object P1)
  RVA=0x02D596A0  token=0x600011A  System.Object[] __Gen_Wrap_31(System.Object P0, System.Object P1)
  RVA=0x011D1C90  token=0x600011B  System.IntPtr __Gen_Wrap_32(System.Object P0)
  RVA=0x01298AE0  token=0x600011C  Beyond.FrameTickGroup __Gen_Wrap_33(System.Object P0)
  RVA=0x01738AF0  token=0x600011D  System.Boolean __Gen_Wrap_34(Beyond.Lua.LuaReference P0)
  RVA=0x0147A0D0  token=0x600011E  System.Boolean __Gen_Wrap_35(System.Object P0, System.Object P1, System.Boolean P2)
  RVA=0x02D5A0C0  token=0x600011F  System.Int32 __Gen_Wrap_36(Beyond.Lua.LuaTestReflectionOptStruct& P0)
  RVA=0x02D5AD00  token=0x6000120  System.String __Gen_Wrap_37(Beyond.Lua.LuaTestReflectionOptStruct& P0)
  RVA=0x02D5BC30  token=0x6000121  System.Int64 __Gen_Wrap_38(Beyond.Lua.LuaTestReflectionOptStruct& P0)
  RVA=0x02D5C870  token=0x6000122  System.UInt64 __Gen_Wrap_39(Beyond.Lua.LuaTestReflectionOptStruct& P0)
  RVA=0x02D5D4B0  token=0x6000123  System.Single __Gen_Wrap_40(Beyond.Lua.LuaTestReflectionOptStruct& P0)
  RVA=0x02D5E0F0  token=0x6000124  System.Double __Gen_Wrap_41(Beyond.Lua.LuaTestReflectionOptStruct& P0)
  RVA=0x02D5ED30  token=0x6000125  System.Boolean __Gen_Wrap_42(Beyond.Lua.LuaTestReflectionOptStruct& P0)
  RVA=0x02D5F970  token=0x6000126  System.Int32 __Gen_Wrap_43(Beyond.Lua.LuaTestReflectionOptStruct& P0, System.Int32 P1)
  RVA=0x02D605C0  token=0x6000127  System.Void __Gen_Wrap_44(Beyond.Lua.LuaTestReflectionOptStruct& P0)
  RVA=0x02D611F0  token=0x6000128  System.Void __Gen_Wrap_45(Beyond.Lua.LuaTestReflectionOptStruct& P0, Beyond.Lua.LuaTestReflectionOptStruct P1)
  RVA=0x02D61F30  token=0x6000129  System.Void __Gen_Wrap_46(Beyond.Lua.LuaTestReflectionOptStruct& P0, Beyond.Lua.LuaTestReflectionOptStruct& P1)
  RVA=0x02D62E90  token=0x600012A  System.Object __Gen_Wrap_47(Beyond.Lua.LuaTestReflectionOptStruct& P0)
  RVA=0x02D63DC0  token=0x600012B  System.Void __Gen_Wrap_48(Beyond.Lua.LuaTestReflectionOptStruct& P0, System.Object P1)
  RVA=0x02D64AE0  token=0x600012C  System.Int32 __Gen_Wrap_49(Beyond.Lua.LuaTestReflectionOptStruct2& P0)
  RVA=0x02D65720  token=0x600012D  System.String __Gen_Wrap_50(Beyond.Lua.LuaTestReflectionOptStruct2& P0)
  RVA=0x02D66650  token=0x600012E  System.Int64 __Gen_Wrap_51(Beyond.Lua.LuaTestReflectionOptStruct2& P0)
  RVA=0x02D67290  token=0x600012F  System.UInt64 __Gen_Wrap_52(Beyond.Lua.LuaTestReflectionOptStruct2& P0)
  RVA=0x02D67ED0  token=0x6000130  System.Single __Gen_Wrap_53(Beyond.Lua.LuaTestReflectionOptStruct2& P0)
  RVA=0x02D68B10  token=0x6000131  System.Double __Gen_Wrap_54(Beyond.Lua.LuaTestReflectionOptStruct2& P0)
  RVA=0x02D69750  token=0x6000132  System.Boolean __Gen_Wrap_55(Beyond.Lua.LuaTestReflectionOptStruct2& P0)
  RVA=0x02D6A390  token=0x6000133  System.Int32 __Gen_Wrap_56(Beyond.Lua.LuaTestReflectionOptStruct2& P0, System.Int32 P1)
  RVA=0x02D6AFE0  token=0x6000134  System.Void __Gen_Wrap_57(Beyond.Lua.LuaTestReflectionOptStruct2& P0)
  RVA=0x02D6BC10  token=0x6000135  System.Void __Gen_Wrap_58(Beyond.Lua.LuaTestReflectionOptStruct2& P0, Beyond.Lua.LuaTestReflectionOptStruct2 P1)
  RVA=0x02D6C950  token=0x6000136  System.Void __Gen_Wrap_59(Beyond.Lua.LuaTestReflectionOptStruct2& P0, Beyond.Lua.LuaTestReflectionOptStruct2& P1)
  RVA=0x02D6D8B0  token=0x6000137  System.Object __Gen_Wrap_60(Beyond.Lua.LuaTestReflectionOptStruct2& P0)
  RVA=0x02D6E7E0  token=0x6000138  System.Void __Gen_Wrap_61(Beyond.Lua.LuaTestReflectionOptStruct2& P0, System.Object P1)
  RVA=0x0135C3D0  token=0x6000139  System.Int32 __Gen_Wrap_62(System.Object P0, System.Int32 P1)
  RVA=0x02D6F500  token=0x600013A  System.Void __Gen_Wrap_63(System.Object P0, Beyond.Lua.LuaTestReflectionOptStruct P1)
  RVA=0x02D6FB90  token=0x600013B  System.Void __Gen_Wrap_64(System.Object P0, Beyond.Lua.LuaTestReflectionOptStruct2 P1)
  RVA=0x02D70220  token=0x600013C  System.Void __Gen_Wrap_65(System.Object P0, Beyond.Lua.LuaTestReflectionOptStruct& P1)
  RVA=0x012A0D50  token=0x600013D  System.Void __Gen_Wrap_66(System.Object P0, Beyond.Lua.ETestEnum P1)
  RVA=0x01298AE0  token=0x600013E  Beyond.Lua.ETestEnum __Gen_Wrap_67(System.Object P0)
  RVA=0x02D70F40  token=0x600013F  Beyond.Lua.LuaTestReflectionOptStruct __Gen_Wrap_68(System.Object P0, Beyond.Lua.LuaTestReflectionOptStruct P1)
  RVA=0x02D71BE0  token=0x6000140  UnityEngine.GameObject __Gen_Wrap_69(System.Object P0)
  RVA=0x02D72520  token=0x6000141  UnityEngine.GameObject __Gen_Wrap_70(System.Object P0, System.Object P1)
  RVA=0x01992E90  token=0x6000142  System.Void __Gen_Wrap_71(System.Object P0, System.Int32 P1, System.Int32 P2)
  RVA=0x02D72F50  token=0x6000143  System.Int32 __Gen_Wrap_72(System.Int32 P0, System.Object P1)
  RVA=0x02D73230  token=0x6000144  System.Boolean __Gen_Wrap_73(UnityEngine.RaycastHit& P0, UnityEngine.Ray P1, System.Single P2, System.Int32 P3, UnityEngine.QueryTriggerInteraction P4)
  RVA=0x02D73E00  token=0x6000145  System.Int32 __Gen_Wrap_74(UnityEngine.Ray P0, System.Object P1, System.Single P2, System.Int32 P3, UnityEngine.QueryTriggerInteraction P4)
  RVA=0x02D744E0  token=0x6000146  UnityEngine.Bounds __Gen_Wrap_75(System.Object P0)
  RVA=0x02D74DE0  token=0x6000147  UnityEngine.Rect __Gen_Wrap_76(System.Object P0, System.Object P1)
  RVA=0x02D757B0  token=0x6000148  UnityEngine.Vector3 __Gen_Wrap_77(System.Object P0)
  RVA=0x02D760B0  token=0x6000149  UnityEngine.Bounds __Gen_Wrap_78(System.Object P0, System.Object P1)
  RVA=0x02D76A80  token=0x600014A  UnityEngine.Vector2 __Gen_Wrap_79(System.Object P0, System.Object P1, UnityEngine.Vector2 P2)
  RVA=0x02D777B0  token=0x600014B  System.Collections.Generic.List<System.String> __Gen_Wrap_80()
  RVA=0x01298AE0  token=0x600014C  System.Int32 __Gen_Wrap_81(System.Object P0)
  RVA=0x02D77FF0  token=0x600014D  System.Single __Gen_Wrap_82(System.Object P0, System.Object P1)
  RVA=0x02D78380  token=0x600014E  System.Boolean __Gen_Wrap_83(System.Object P0, HG.Rendering.Runtime.HGCharacterVolume& P1)
  RVA=0x02837EF0  token=0x600014F  System.Single __Gen_Wrap_84(System.Object P0)
  RVA=0x011D1AC0  token=0x6000150  System.Single __Gen_Wrap_85()
  RVA=0x01494100  token=0x6000151  System.Void __Gen_Wrap_86(System.Single P0)
  RVA=0x02D78DA0  token=0x6000152  DG.Tweening.Tweener __Gen_Wrap_87(System.Single P0, System.Single P1, System.Single P2, System.Object P3)
  RVA=0x02D79740  token=0x6000153  Beyond.Lua.UtilsForLua.ImageContentParam __Gen_Wrap_88(System.Object P0)
  RVA=0x012F6920  token=0x6000154  System.Void __Gen_Wrap_89(System.Int32 P0)
  RVA=0x018C3AD0  token=0x6000155  System.Boolean __Gen_Wrap_90(System.Int32 P0)
  RVA=0x01488F50  token=0x6000156  System.Void __Gen_Wrap_91(System.Boolean P0)
  RVA=0x02D7A070  token=0x6000157  Cinemachine.CinemachineTransposer __Gen_Wrap_92(System.Object P0)
  RVA=0x02D7A9A0  token=0x6000158  Cinemachine.CinemachineComposer __Gen_Wrap_93(System.Object P0)
  RVA=0x02D7B2D0  token=0x6000159  CinemachineCameraOffset __Gen_Wrap_94(System.Object P0)
  RVA=0x04D37330  token=0x600015A  System.Void .cctor()
END_CLASS

CLASS: IFix.ILFixInterfaceBridge
TYPE:  class
TOKEN: 0x200002E
SIZE:  0x40
EXTENDS: IFix.Core.AnonymousStorey
FIELDS:
METHODS:
  RVA=0x09EB675C  token=0x600015B  System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine)
  RVA=0x041E1670  token=0x600015C  System.Void RefAsyncBuilderStartMethod()
END_CLASS

CLASS: IFix.WrappersManagerImpl
TYPE:  class
TOKEN: 0x200002F
SIZE:  0x18
IMPLEMENTS: IFix.Core.WrappersManager
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
METHODS:
  RVA=0x053908C0  token=0x600015D  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine)
  RVA=0x09EBE5EC  token=0x600015E  IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id)
  RVA=0x0301FCF0  token=0x600015F  System.Boolean IsPatched(System.Int32 id)
  RVA=0x09EBE4A8  token=0x6000160  System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon)
  RVA=0x09EBE574  token=0x6000161  System.Object CreateWrapper(System.Int32 id)
  RVA=0x09EBE650  token=0x6000162  System.Object InitWrapperArray(System.Int32 len)
  RVA=0x09EBE404  token=0x6000163  IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine)
END_CLASS

CLASS: IFix.IDMAP0
TYPE:  sealed struct
TOKEN: 0x2000030
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaEventSystem-PushEventName0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-IsLuaGCing0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-AddOnceLuaEventAfterGCDo0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaEventSystem-DispatchEvent0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaEventSystem-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-CSharpCallLuaRegistration-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-CSharpCallLuaRegistration-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaCustomConfig-get_itemDict0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaCustomConfig-OnBeforeSerialize0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaCustomConfig-OnAfterDeserialize0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaCustomConfig-Item-MarkLuaValueProcessed0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaCustomConfig-ConvertLuaTableToJson0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaCustomConfig-ConvertLuaCodeToJson0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaCustomConfig-_ConvertJValueToObjectForValidation0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaCustomConfig-_PopulateLuaTableFromJTokenForValidation0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaCustomConfig-_JsonToLuaTableForValidation0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaCustomConfig-_IsNumericType0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaCustomConfig-_CompareValues0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaCustomConfig-_CompareLuaTables0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaCustomConfig-_ValidateLuaTableConsistency0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaCustomConfig-_ProcessLuaTableItem0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaCustomConfig-ShouldProcessEnumItem0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaCustomConfig-_ProcessEnumItem0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaCustomConfig-_ConvertJValueToObject0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaCustomConfig-_PopulateLuaTableFromJToken0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaCustomConfig-_JsonToLuaTable0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaCustomConfig-Item-GetEnumType0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaCustomConfig-Item-GetEnumValueObject0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaCustomConfig-InitConfigTable0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaCustomConfig-Item-HasLuaValueChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaCustomConfig-Item-ShouldSerializeLuaTableJson0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaHotFix-LuaHotFixTest0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaHotFix-HotFixLua0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaHotFix-HotFixLua1  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaHotFix-HotFixLua2  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LoadLuaProxy-_RunOnStart0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LoadLuaProxy-LoadLua0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaMethodPointerModule-_RunOnStart0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-CreateInstance0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-DestroyInstance0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-IsEnableMultithreadGC0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-_OnRenderDone0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-_OnRenderDone1  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-GetLuaStack0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-_HyperLuaLogCallback0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-_LuaUtilsLogCallback0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-_Register3rdPartyLibraries0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-LoadLuaBytes0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-_OnEnvLangChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-LuaCalculateABPathHash0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-CasterStringPathHash0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-CheckStringPathHash0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-_RegisterCSharpCallLua0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-OnInit0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-get_isQuittingApplication0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-_ReleaseDelegateInAnotherStackFrame0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-OnRelease0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-ReStartLuaProfiler0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-set_isQuittingApplication0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-TailLateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-GetLuaFileRealPath0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-IsLuaFileExist0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-LoadLuaByteByFileRealPath0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-LoadLua0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-LoadLuaBytePtr0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-DoString0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-GetLuaState0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-ClearAllUnityObjectUserdata0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-AddLuaEventAfterGCDo0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-RemoveLuaEventAfterGCDo0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaManager-get_frameTickGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaReference-Reset0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaReference-_UnBindFromXluaRef0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaReference-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaReference-_BindSelfReferences0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaReference-BindToLua0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaReference-_RefreshSelfCustomUIStyleBindings0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaReference-RefreshCustomUIStyleBindings0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaReference-ContainsGameObject0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOptStruct-get_a10  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOptStruct-get_b10  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOptStruct-get_l10  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOptStruct-get_ul10  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOptStruct-get_f10  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOptStruct-get_d10  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOptStruct-get_bl10  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOptStruct-TestInt0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOptStruct-TestVoid0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOptStruct-TestStructParam0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOptStruct-TestStructParam20  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOptStruct-TestReturnObj0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOptStruct-TestObjParam0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOptStruct2-get_a10  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOptStruct2-get_b10  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOptStruct2-get_l10  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOptStruct2-get_ul10  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOptStruct2-get_f10  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOptStruct2-get_d10  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOptStruct2-get_bl10  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOptStruct2-TestInt0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOptStruct2-TestVoid0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOptStruct2-TestStructParam0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOptStruct2-TestStructParam20  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOptStruct2-TestReturnObj0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOptStruct2-TestObjParam0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOpt2-TestVoid0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOpt2-TestInt0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOpt2-TestStructParam0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOpt2-TestStructParam1  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOpt2-TestStructParam20  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOpt2-TestReturnObj0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOpt2-TestObjParam0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOpt2-TestEnum0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOpt2-TestReturnEnum0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOpt2-TestStruct0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOpt-TestVoid0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOpt-TestInt0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOpt-TestStructParam0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOpt-TestStructParam1  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOpt-TestStructParam20  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOpt-TestReturnObj0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOpt-TestObjParam0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOpt-TestEnum0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOpt-TestReturnEnum0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaTestReflectionOpt-TestStruct0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaUIWidget-Reset0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaUIWidget-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaUIWidget-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaUIWidget-ClearComponent0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-LuaUIWidget-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-CreateObject0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-CreateObject1  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-CreateObject2  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-UIContainerResize0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-TurnOffLayer0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-TurnOnLayer0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-ToggleLayer0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-SetPosX0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-SetPosY0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-SetPosZ0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-SetRotX0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-SetRotY0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-SetRotZ0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-SetScaleX0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-SetScaleY0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-SetScaleZ0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-ClearUIComponents0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-RayCast0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-RaycastNonAlloc0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-GetRectTransformBounds0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-RectTransformToScreenRect0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-GetRectTransformCenterPosition0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-CalcBoundOfRectTransform0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-ClampPointInRectTransform0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-GetStringList0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-GetStringHash0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-EntityDistance0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-OpenAccountCenter0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-ExitGame0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-GetCurrentUID0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-StartPayment0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-TryGetCharacterVolume0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-GetAnimationCurveLength0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-TweenTo0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-IsNull0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-ParseImageContent0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-ReturnToLogin0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-QuitGame0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-CheckExtremeDangerousByLevel0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-ToggleCharInfoInUpgradePanelOption0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-ToggleWeaponInUpgradePanelOption0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-GetCinemachineTransposer0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-GetCinemachineComposer0  // const
  public    static  IFix.IDMAP0                     Beyond-Lua-UtilsForLua-GetOrAddCinemachineCameraOffset0  // const
METHODS:
END_CLASS

