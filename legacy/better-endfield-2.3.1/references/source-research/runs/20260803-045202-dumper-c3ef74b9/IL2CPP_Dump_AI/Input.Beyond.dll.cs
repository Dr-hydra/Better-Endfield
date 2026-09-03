// ========================================================
// Dumped by @desirepro
// Assembly: Input.Beyond.dll
// Classes:  80
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

CLASS: CursorMode
TYPE:  sealed struct
TOKEN: 0x2000004
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.InputSystem.UI.DebugVirtualMouseInput.CursorModeSoftwareCursor  // const
  public    static  UnityEngine.InputSystem.UI.DebugVirtualMouseInput.CursorModeHardwareCursorIfAvailable  // const
METHODS:
END_CLASS

CLASS: Binding
TYPE:  class
TOKEN: 0x2000014
SIZE:  0x30
FIELDS:
  public            Beyond.Input.InputBindingInfo   info  // 0x10
  public            System.Single                   pressDownTime  // 0x18
  public            System.Boolean                  isUsedForBlock  // 0x1c
  public            System.Boolean                  isCommonOpenUIAction  // 0x1d
  public            System.Int32                    longPressTimerId  // 0x20
  public            System.Single                   longPressTime  // 0x24
  public            System.Boolean                  enableContinuousTrigger  // 0x28
  public            Beyond.Input.InputContinuousTriggerTypecontinuousTriggerType  // 0x2c
METHODS:
  RVA=0x037D0550  token=0x6000077  System.Void .ctor(Beyond.Input.GamepadInput input, Beyond.Input.InputBindingInfo info, System.Boolean isUsedForBlock)
END_CLASS

CLASS: TriggerType
TYPE:  sealed struct
TOKEN: 0x2000015
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.Input.GamepadCheckerBase.TriggerTypeNORMAL  // const
  public    static  Beyond.Input.GamepadCheckerBase.TriggerTypeCONTINUOUS  // const
  public    static  Beyond.Input.GamepadCheckerBase.TriggerTypeSTOP_CONTINUOUS  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x2000019
SIZE:  0x28
FIELDS:
  public            Beyond.Input.GamepadOnLongPressChecker<>4__this  // 0x10
  public            Beyond.Input.GamepadCheckerBase.Bindingbinding  // 0x18
  public            Beyond.Input.GamepadKeyCode     keyCode  // 0x20
METHODS:
  RVA=0x041E1670  token=0x600008C  System.Void .ctor()
  RVA=0x09E3ABB0  token=0x600008D  System.Void <CreateTriggerBindingAction>b__0()
END_CLASS

CLASS: CursorSpeedPhase
TYPE:  class
TOKEN: 0x200001C
SIZE:  0x28
FIELDS:
  public            System.Single                   maxJsValue  // 0x10
  public            System.Single                   minSpeed  // 0x14
  public            System.Single                   maxSpeed  // 0x18
  public            System.Single                   acceleration  // 0x1c
  public            System.Single                   accelerationSpeed  // 0x20
  public            System.Single                   maxAcceleration  // 0x24
METHODS:
  RVA=0x041E1670  token=0x60000BF  System.Void .ctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200001D
SIZE:  0x10
FIELDS:
  public    static readonly Beyond.Input.HGVirtualMouse.<>c <>9  // static @ 0x0
  public    static  System.Predicate<UnityEngine.UI.Selectable><>9__78_0  // static @ 0x8
METHODS:
  RVA=0x09E3ABDC  token=0x60000C0  System.Void .cctor()
  RVA=0x041E1670  token=0x60000C1  System.Void .ctor()
  RVA=0x09E3AB64  token=0x60000C2  System.Boolean <ChangeHoverState>b__78_0(UnityEngine.UI.Selectable o)
END_CLASS

CLASS: KeyCaches
TYPE:  class
TOKEN: 0x2000028
SIZE:  0x20
FIELDS:
  public            System.Boolean                  escapeKeyDown  // 0x10
  public            System.Boolean                  mouse0KeyDown  // 0x11
  public            System.Boolean                  mouse1KeyDown  // 0x12
  public            System.Boolean                  escapeKeyUp  // 0x13
  public            System.Boolean                  mouse0KeyUp  // 0x14
  public            System.Boolean                  mouse1KeyUp  // 0x15
  public            System.Boolean                  escapeKey  // 0x16
  public            System.Boolean                  mouse0Key  // 0x17
  public            System.Boolean                  mouse1Key  // 0x18
METHODS:
  RVA=0x03304DD0  token=0x60001FF  System.Void UpdateKeyStat()
  RVA=0x041E1670  token=0x6000200  System.Void .ctor()
END_CLASS

CLASS: KeyCodeState
TYPE:  sealed struct
TOKEN: 0x2000029
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.Input.InputManager.KeyCodeStatePressed  // const
  public    static  Beyond.Input.InputManager.KeyCodeStateOnPress  // const
  public    static  Beyond.Input.InputManager.KeyCodeStateOnRelease  // const
METHODS:
END_CLASS

CLASS: ControllerHintInfo
TYPE:  class
TOKEN: 0x200002A
SIZE:  0x30
IMPLEMENTS: System.IComparable`1 Beyond.PoolCore.IReusable
FIELDS:
  public            System.String                   actionId  // 0x10
  public            System.Int32                    priority  // 0x18
  public            Beyond.Input.InputBindingInfo   bind  // 0x20
  public            Beyond.Input.IBindingView       hintView  // 0x28
METHODS:
  RVA=0x041E1670  token=0x6000201  System.Void .ctor()
  RVA=0x09E30098  token=0x6000202  System.Void .ctor(Beyond.Input.IBindingView hintView, Beyond.Input.InputBindingInfo bind)
  RVA=0x09E300A0  token=0x6000203  System.Void .ctor(System.String actionId)
  RVA=0x09E2FF38  token=0x6000204  System.Void Reset(Beyond.Input.IBindingView newHintView, Beyond.Input.InputBindingInfo newBind)
  RVA=0x09E2FFE4  token=0x6000205  System.Void Reset(System.String resetActionId)
  RVA=0x09E2FE1C  token=0x6000206  System.Int32 CompareTo(Beyond.Input.InputManager.ControllerHintInfo other)
  RVA=0x09E2FE7C  token=0x6000207  System.Void OnAllocate()
  RVA=0x09E2FEC0  token=0x6000208  System.Void OnRecycle()
END_CLASS

CLASS: DelayedComboKind
TYPE:  sealed struct
TOKEN: 0x200002B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.Input.InputManager.DelayedComboKindModifierTapThenTrigger  // const
  public    static  Beyond.Input.InputManager.DelayedComboKindTriggerThenModifier  // const
METHODS:
END_CLASS

CLASS: DelayedComboBinding
TYPE:  class
TOKEN: 0x200002C
SIZE:  0x48
FIELDS:
  public    readonly System.String                   modifierActionId  // 0x10
  public    readonly System.String                   triggerActionId  // 0x18
  public    readonly Beyond.Input.InputManager.DelayedComboKindkind  // 0x20
  public    readonly System.Single                   maxModifierHoldTime  // 0x24
  public    readonly System.Action                   onTriggered  // 0x28
  private           Beyond.Input.GamepadKeyCode     modifierKey  // 0x30
  private           Beyond.Input.GamepadKeyCode     triggerKey  // 0x34
  private           System.Single                   modifierHoldTime  // 0x38
  private           System.Int32                    modifierReleaseFrame  // 0x3c
  private           System.Boolean                  <triggerJustPressedLastFrame>k__BackingField  // 0x40
PROPERTIES:
  triggerJustPressedLastFrame  get=0x02D480A0  set=0x02D480B0
METHODS:
  RVA=0x04490470  token=0x600020B  System.Void .ctor(System.String modifierActionId, System.String triggerActionId, Beyond.Input.InputManager.DelayedComboKind kind, System.Single maxModifierHoldTime, System.Action onTriggered)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200002D
SIZE:  0x10
FIELDS:
  public    static readonly Beyond.Input.InputManager.<>c   <>9  // static @ 0x0
  public    static  System.Action                   <>9__100_0  // static @ 0x8
  public    static  System.Action                   <>9__239_0  // static @ 0x10
  public    static  System.Text.RegularExpressions.MatchEvaluator<>9__297_0  // static @ 0x18
METHODS:
  RVA=0x04D4A230  token=0x600020C  System.Void .cctor()
  RVA=0x041E1670  token=0x600020D  System.Void .ctor()
  RVA=0x041E1670  token=0x600020E  System.Void <_InitDeviceChangeBinding>b__100_0()
  RVA=0x041E1670  token=0x600020F  System.Void <_CreateVirtualMouse>b__239_0()
  RVA=0x02D480C0  token=0x6000210  System.String <ParseTextActionId>b__297_0(System.Text.RegularExpressions.Match match)
END_CLASS

CLASS: Binding
TYPE:  class
TOKEN: 0x2000034
SIZE:  0x30
FIELDS:
  public            Beyond.Input.InputBindingInfo   info  // 0x10
  public            System.Single                   pressDownTime  // 0x18
  public            System.Boolean                  isUsedForBlock  // 0x1c
  public            System.Int32                    longPressTimerId  // 0x20
  public            System.Single                   longPressTime  // 0x24
  public            System.Boolean                  enableContinuousTrigger  // 0x28
  public            System.Boolean                  isCommonOpenUIAction  // 0x29
  public            Beyond.Input.InputContinuousTriggerTypecontinuousTriggerType  // 0x2c
METHODS:
  RVA=0x037D0D30  token=0x6000220  System.Void .ctor(Beyond.Input.KeyboardInput input, Beyond.Input.InputBindingInfo info, System.Boolean isUsedForBlock)
END_CLASS

CLASS: TriggerType
TYPE:  sealed struct
TOKEN: 0x2000035
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.Input.KeyboardCheckerBase.TriggerTypeNORMAL  // const
  public    static  Beyond.Input.KeyboardCheckerBase.TriggerTypeCONTINUOUS  // const
  public    static  Beyond.Input.KeyboardCheckerBase.TriggerTypeSTOP_CONTINUOUS  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x2000039
SIZE:  0x30
FIELDS:
  public            System.ValueTuple<Beyond.Input.KeyboardKeyCode,System.Collections.Generic.HashSet<Beyond.Input.KeyboardCheckerBase.Binding>>key  // 0x10
  public            Beyond.Input.KeyboardCheckerBase.BindingtargetBinding  // 0x20
  public            Beyond.Input.KeyboardOnLongPressChecker<>4__this  // 0x28
METHODS:
  RVA=0x041E1670  token=0x6000238  System.Void .ctor()
  RVA=0x09E40C1C  token=0x6000239  System.Void <CheckKeyboardInput>b__0()
END_CLASS

CLASS: __StaticArrayInitTypeSize=32
TYPE:  sealed struct
TOKEN: 0x200004C
SIZE:  0x30
FIELDS:
METHODS:
END_CLASS

CLASS: <PrivateImplementationDetails>
TYPE:  sealed class
TOKEN: 0x200004B
SIZE:  0x10
FIELDS:
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=32E680B0AF74F1D11AFCE59DFB222E2F946D48C66AE24640EE35784361EEA2E0C4  // static @ 0x0
METHODS:
END_CLASS

CLASS: Beyond.Input.ControllerNaviManager
TYPE:  class
TOKEN: 0x200000A
SIZE:  0x58
FIELDS:
  public            System.Collections.Generic.LinkedList<UnityEngine.UI.SelectableNaviGroup>layerStackList  // 0x10
  public            UnityEngine.UI.Selectable       curTarget  // 0x18
  private   readonly System.Collections.Generic.HashSet<System.String>m_naviLockSet  // 0x20
  private           System.Int32                    m_targetInputBindingGroupId  // 0x28
  private           System.Int32                    m_naviInputBindingGroupId  // 0x2c
  private           UnityEngine.UI.Selectable       m_lastEnableNaviTarget  // 0x30
  private           UnityEngine.UI.NavigationBindingType<navigationBindingType>k__BackingField  // 0x38
  private           System.Collections.Generic.Dictionary<UnityEngine.UI.NavigationBindingType,System.Int32>m_naviBindingGroupIds  // 0x40
  private           Beyond.Input.InputManager       m_inputManager  // 0x48
  private   static  System.Single                   SCORE_OFFSET  // const
  private           System.Boolean                  <forceEnableNaviForGuide>k__BackingField  // 0x50
PROPERTIES:
  navigationBindingType  get=0x04D863B0  set=0x04D86430
  forceEnableNaviForGuide  get=0x04D86800  set=0x04D86840
METHODS:
  RVA=0x044BBBD0  token=0x600002D  System.Void .ctor(Beyond.Input.InputManager inputManager)
  RVA=0x044BCD10  token=0x600002E  System.Void SetNaviBindingType(UnityEngine.UI.NavigationBindingType bindingType)
  RVA=0x03304A00  token=0x600002F  System.Void Tick()
  RVA=0x037D1060  token=0x6000030  System.Boolean _RemoveLayer(UnityEngine.UI.SelectableNaviGroup group, UnityEngine.UI.SelectableNaviGroup curLayer)
  RVA=0x037D1460  token=0x6000031  System.Void _RemoveLayerFromStack(UnityEngine.UI.SelectableNaviGroup group, System.Boolean silentMode)
  RVA=0x039D0480  token=0x6000032  System.Void _SetTarget(UnityEngine.UI.Selectable target)
  RVA=0x09E31810  token=0x6000033  System.Void _OnLayerSelected(UnityEngine.UI.SelectableNaviGroup topLayer)
  RVA=0x09E30ABC  token=0x6000034  System.Boolean _DefaultNavigate(UnityEngine.UI.NaviDirection dir)
  RVA=0x09E31A3C  token=0x6000035  UnityEngine.UI.Selectable _TryFindBestNaviTargetInGroup(UnityEngine.UI.SelectableNaviGroup group, UnityEngine.Vector3 pos, UnityEngine.Vector3 dir, System.Double curMaxScore, System.Double& maxScore)
  RVA=0x09E31DE0  token=0x6000036  UnityEngine.UI.Selectable _TryFindBestNaviTarget(System.Collections.Generic.HashSet<UnityEngine.UI.Selectable> targets, UnityEngine.Vector3 curPos, UnityEngine.Vector3 dir, UnityEngine.UI.NaviStrategy strategy, System.Double curMaxScore, System.Double& maxScore)
  RVA=0x09E31608  token=0x6000037  UnityEngine.Vector3 _GetPointOnRectEdge(UnityEngine.RectTransform rectTransform, UnityEngine.Vector2 dir)
  RVA=0x09E314CC  token=0x6000038  System.Collections.Generic.LinkedListNode<UnityEngine.UI.SelectableNaviGroup> _GetDummyLayerLastGroupInStack(UnityEngine.UI.SelectableNaviGroup dummyLayer)
  RVA=0x09E307C8  token=0x600003B  System.Void SetForceEnableNavi(System.Boolean enable)
  RVA=0x03794810  token=0x600003C  System.Void UpdateNaviInputBindingState()
  RVA=0x03794A30  token=0x600003D  System.Boolean IsNavigationBindingEnabled()
  RVA=0x039D0120  token=0x600003E  System.Void TryRemoveLayer(UnityEngine.UI.SelectableNaviGroup group)
  RVA=0x037D1410  token=0x600003F  System.Boolean IsLayerInStack(UnityEngine.UI.SelectableNaviGroup group)
  RVA=0x09E30414  token=0x6000040  System.Boolean IsTopLayer(UnityEngine.UI.SelectableNaviGroup group)
  RVA=0x044F3C80  token=0x6000041  System.Void ChangeAndTrySetNaviBindingType(UnityEngine.UI.SelectableNaviGroup naviGroup, UnityEngine.UI.NavigationBindingType naviBindingType)
  RVA=0x039CFAE0  token=0x6000042  System.Void TryAutoSetTarget()
  RVA=0x09E302CC  token=0x6000043  UnityEngine.UI.SelectableNaviGroup GetTopDummyLayerGroup()
  RVA=0x09E30918  token=0x6000044  System.Void SetTarget(UnityEngine.UI.Selectable target)
  RVA=0x09E30708  token=0x6000045  System.Void ResetStateForUIDispose()
  RVA=0x04B6AF20  token=0x6000046  System.Void SetTargetInSilentModeIfNecessary(UnityEngine.UI.Selectable selectable, UnityEngine.UI.SelectableNaviGroup needTopDummyGroup)
  RVA=0x09E300A8  token=0x6000047  System.Void ClearTargetIfNecessary(UnityEngine.UI.SelectableNaviGroup needTopDummyGroup)
  RVA=0x09E3084C  token=0x6000048  System.Void SetTargetInSilentModeIfNecessary(UnityEngine.UI.SelectableNaviGroup naviGroup, UnityEngine.UI.Selectable selectable, UnityEngine.UI.SelectableNaviGroup needTopDummyGroup)
  RVA=0x09E30388  token=0x6000049  System.Boolean IsNaviTarget(UnityEngine.UI.Selectable target)
  RVA=0x09E30178  token=0x600004A  UnityEngine.Vector3 GetNaviVector(UnityEngine.UI.NaviDirection dir)
  RVA=0x09E30510  token=0x600004B  System.Void Navigate(UnityEngine.UI.NaviDirection dir)
  RVA=0x09E30984  token=0x600004C  System.Void ToggleNavigation(System.String key, System.Boolean active)
  RVA=0x09E30A7C  token=0x600004D  System.Void <.ctor>b__12_0()
  RVA=0x09E30AAC  token=0x600004E  System.Void <.ctor>b__12_1()
  RVA=0x09E30A8C  token=0x600004F  System.Void <.ctor>b__12_2()
  RVA=0x09E30A9C  token=0x6000050  System.Void <.ctor>b__12_3()
  RVA=0x09E30A8C  token=0x6000051  System.Void <.ctor>b__12_4()
  RVA=0x09E30A9C  token=0x6000052  System.Void <.ctor>b__12_5()
  RVA=0x09E30A7C  token=0x6000053  System.Void <.ctor>b__12_6()
  RVA=0x09E30AAC  token=0x6000054  System.Void <.ctor>b__12_7()
  RVA=0x09E30A7C  token=0x6000055  System.Void <.ctor>b__12_8()
  RVA=0x09E30AAC  token=0x6000056  System.Void <.ctor>b__12_9()
  RVA=0x09E30A8C  token=0x6000057  System.Void <.ctor>b__12_10()
  RVA=0x09E30A9C  token=0x6000058  System.Void <.ctor>b__12_11()
  RVA=0x09E30A8C  token=0x6000059  System.Void <.ctor>b__12_12()
  RVA=0x09E30A9C  token=0x600005A  System.Void <.ctor>b__12_13()
  RVA=0x09E30A7C  token=0x600005B  System.Void <.ctor>b__12_14()
  RVA=0x09E30AAC  token=0x600005C  System.Void <.ctor>b__12_15()
  RVA=0x09E30A7C  token=0x600005D  System.Void <.ctor>b__12_16()
  RVA=0x09E30AAC  token=0x600005E  System.Void <.ctor>b__12_17()
  RVA=0x09E30A8C  token=0x600005F  System.Void <.ctor>b__12_18()
  RVA=0x09E30A9C  token=0x6000060  System.Void <.ctor>b__12_19()
  RVA=0x09E30A8C  token=0x6000061  System.Void <.ctor>b__12_20()
  RVA=0x09E30A9C  token=0x6000062  System.Void <.ctor>b__12_21()
  RVA=0x09E30A7C  token=0x6000063  System.Void <.ctor>b__12_22()
  RVA=0x09E30AAC  token=0x6000064  System.Void <.ctor>b__12_23()
  RVA=0x09E30A7C  token=0x6000065  System.Void <.ctor>b__12_24()
  RVA=0x09E30AAC  token=0x6000066  System.Void <.ctor>b__12_25()
  RVA=0x09E30A8C  token=0x6000067  System.Void <.ctor>b__12_26()
  RVA=0x09E30A9C  token=0x6000068  System.Void <.ctor>b__12_27()
  RVA=0x09E30A8C  token=0x6000069  System.Void <.ctor>b__12_28()
  RVA=0x09E30A9C  token=0x600006A  System.Void <.ctor>b__12_29()
  RVA=0x09E30A7C  token=0x600006B  System.Void <.ctor>b__12_30()
  RVA=0x09E30AAC  token=0x600006C  System.Void <.ctor>b__12_31()
END_CLASS

CLASS: Beyond.Input.ActionOnSetNaviTarget
TYPE:  sealed struct
TOKEN: 0x200000B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.Input.ActionOnSetNaviTargetNone  // const
  public    static  Beyond.Input.ActionOnSetNaviTargetAutoTriggerOnClick  // const
  public    static  Beyond.Input.ActionOnSetNaviTargetPressConfirmTriggerOnClick  // const
METHODS:
END_CLASS

CLASS: Beyond.Input.ControllerNaviUtils
TYPE:  static class
TOKEN: 0x200000C
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: Beyond.Input.PSTriggerEffectCfg
TYPE:  class
TOKEN: 0x200000D
SIZE:  0x20
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public            System.Collections.Generic.List<Plugins.LibScePad.TriggerEffectCommandUnion>commands  // 0x18
METHODS:
  RVA=0x05393520  token=0x600006D  System.Void .ctor()
END_CLASS

CLASS: Beyond.Input.KeyboardKeyCode
TYPE:  sealed struct
TOKEN: 0x200000E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.Input.KeyboardKeyCode    None  // const
  public    static  Beyond.Input.KeyboardKeyCode    Backspace  // const
  public    static  Beyond.Input.KeyboardKeyCode    Tab  // const
  public    static  Beyond.Input.KeyboardKeyCode    Clear  // const
  public    static  Beyond.Input.KeyboardKeyCode    Return  // const
  public    static  Beyond.Input.KeyboardKeyCode    Pause  // const
  public    static  Beyond.Input.KeyboardKeyCode    EscapeOnly  // const
  public    static  Beyond.Input.KeyboardKeyCode    Space  // const
  public    static  Beyond.Input.KeyboardKeyCode    Exclaim  // const
  public    static  Beyond.Input.KeyboardKeyCode    DoubleQuote  // const
  public    static  Beyond.Input.KeyboardKeyCode    Hash  // const
  public    static  Beyond.Input.KeyboardKeyCode    Dollar  // const
  public    static  Beyond.Input.KeyboardKeyCode    Percent  // const
  public    static  Beyond.Input.KeyboardKeyCode    Ampersand  // const
  public    static  Beyond.Input.KeyboardKeyCode    Quote  // const
  public    static  Beyond.Input.KeyboardKeyCode    LeftParen  // const
  public    static  Beyond.Input.KeyboardKeyCode    RightParen  // const
  public    static  Beyond.Input.KeyboardKeyCode    Asterisk  // const
  public    static  Beyond.Input.KeyboardKeyCode    Plus  // const
  public    static  Beyond.Input.KeyboardKeyCode    Comma  // const
  public    static  Beyond.Input.KeyboardKeyCode    Minus  // const
  public    static  Beyond.Input.KeyboardKeyCode    Period  // const
  public    static  Beyond.Input.KeyboardKeyCode    Slash  // const
  public    static  Beyond.Input.KeyboardKeyCode    Alpha0  // const
  public    static  Beyond.Input.KeyboardKeyCode    Alpha1  // const
  public    static  Beyond.Input.KeyboardKeyCode    Alpha2  // const
  public    static  Beyond.Input.KeyboardKeyCode    Alpha3  // const
  public    static  Beyond.Input.KeyboardKeyCode    Alpha4  // const
  public    static  Beyond.Input.KeyboardKeyCode    Alpha5  // const
  public    static  Beyond.Input.KeyboardKeyCode    Alpha6  // const
  public    static  Beyond.Input.KeyboardKeyCode    Alpha7  // const
  public    static  Beyond.Input.KeyboardKeyCode    Alpha8  // const
  public    static  Beyond.Input.KeyboardKeyCode    Alpha9  // const
  public    static  Beyond.Input.KeyboardKeyCode    Colon  // const
  public    static  Beyond.Input.KeyboardKeyCode    Semicolon  // const
  public    static  Beyond.Input.KeyboardKeyCode    Less  // const
  public    static  Beyond.Input.KeyboardKeyCode    Equals  // const
  public    static  Beyond.Input.KeyboardKeyCode    Greater  // const
  public    static  Beyond.Input.KeyboardKeyCode    Question  // const
  public    static  Beyond.Input.KeyboardKeyCode    At  // const
  public    static  Beyond.Input.KeyboardKeyCode    LeftBracket  // const
  public    static  Beyond.Input.KeyboardKeyCode    Backslash  // const
  public    static  Beyond.Input.KeyboardKeyCode    RightBracket  // const
  public    static  Beyond.Input.KeyboardKeyCode    Caret  // const
  public    static  Beyond.Input.KeyboardKeyCode    Underscore  // const
  public    static  Beyond.Input.KeyboardKeyCode    BackQuote  // const
  public    static  Beyond.Input.KeyboardKeyCode    A  // const
  public    static  Beyond.Input.KeyboardKeyCode    B  // const
  public    static  Beyond.Input.KeyboardKeyCode    C  // const
  public    static  Beyond.Input.KeyboardKeyCode    D  // const
  public    static  Beyond.Input.KeyboardKeyCode    E  // const
  public    static  Beyond.Input.KeyboardKeyCode    F  // const
  public    static  Beyond.Input.KeyboardKeyCode    G  // const
  public    static  Beyond.Input.KeyboardKeyCode    H  // const
  public    static  Beyond.Input.KeyboardKeyCode    I  // const
  public    static  Beyond.Input.KeyboardKeyCode    J  // const
  public    static  Beyond.Input.KeyboardKeyCode    K  // const
  public    static  Beyond.Input.KeyboardKeyCode    L  // const
  public    static  Beyond.Input.KeyboardKeyCode    M  // const
  public    static  Beyond.Input.KeyboardKeyCode    N  // const
  public    static  Beyond.Input.KeyboardKeyCode    O  // const
  public    static  Beyond.Input.KeyboardKeyCode    P  // const
  public    static  Beyond.Input.KeyboardKeyCode    Q  // const
  public    static  Beyond.Input.KeyboardKeyCode    R  // const
  public    static  Beyond.Input.KeyboardKeyCode    S  // const
  public    static  Beyond.Input.KeyboardKeyCode    T  // const
  public    static  Beyond.Input.KeyboardKeyCode    U  // const
  public    static  Beyond.Input.KeyboardKeyCode    V  // const
  public    static  Beyond.Input.KeyboardKeyCode    W  // const
  public    static  Beyond.Input.KeyboardKeyCode    X  // const
  public    static  Beyond.Input.KeyboardKeyCode    Y  // const
  public    static  Beyond.Input.KeyboardKeyCode    Z  // const
  public    static  Beyond.Input.KeyboardKeyCode    LeftCurlyBracket  // const
  public    static  Beyond.Input.KeyboardKeyCode    Pipe  // const
  public    static  Beyond.Input.KeyboardKeyCode    RightCurlyBracket  // const
  public    static  Beyond.Input.KeyboardKeyCode    Tilde  // const
  public    static  Beyond.Input.KeyboardKeyCode    Delete  // const
  public    static  Beyond.Input.KeyboardKeyCode    Keypad0  // const
  public    static  Beyond.Input.KeyboardKeyCode    Keypad1  // const
  public    static  Beyond.Input.KeyboardKeyCode    Keypad2  // const
  public    static  Beyond.Input.KeyboardKeyCode    Keypad3  // const
  public    static  Beyond.Input.KeyboardKeyCode    Keypad4  // const
  public    static  Beyond.Input.KeyboardKeyCode    Keypad5  // const
  public    static  Beyond.Input.KeyboardKeyCode    Keypad6  // const
  public    static  Beyond.Input.KeyboardKeyCode    Keypad7  // const
  public    static  Beyond.Input.KeyboardKeyCode    Keypad8  // const
  public    static  Beyond.Input.KeyboardKeyCode    Keypad9  // const
  public    static  Beyond.Input.KeyboardKeyCode    KeypadPeriod  // const
  public    static  Beyond.Input.KeyboardKeyCode    KeypadDivide  // const
  public    static  Beyond.Input.KeyboardKeyCode    KeypadMultiply  // const
  public    static  Beyond.Input.KeyboardKeyCode    KeypadMinus  // const
  public    static  Beyond.Input.KeyboardKeyCode    KeypadPlus  // const
  public    static  Beyond.Input.KeyboardKeyCode    KeypadEnter  // const
  public    static  Beyond.Input.KeyboardKeyCode    KeypadEquals  // const
  public    static  Beyond.Input.KeyboardKeyCode    UpArrow  // const
  public    static  Beyond.Input.KeyboardKeyCode    DownArrow  // const
  public    static  Beyond.Input.KeyboardKeyCode    RightArrow  // const
  public    static  Beyond.Input.KeyboardKeyCode    LeftArrow  // const
  public    static  Beyond.Input.KeyboardKeyCode    Insert  // const
  public    static  Beyond.Input.KeyboardKeyCode    Home  // const
  public    static  Beyond.Input.KeyboardKeyCode    End  // const
  public    static  Beyond.Input.KeyboardKeyCode    PageUp  // const
  public    static  Beyond.Input.KeyboardKeyCode    PageDown  // const
  public    static  Beyond.Input.KeyboardKeyCode    F1  // const
  public    static  Beyond.Input.KeyboardKeyCode    F2  // const
  public    static  Beyond.Input.KeyboardKeyCode    F3  // const
  public    static  Beyond.Input.KeyboardKeyCode    F4  // const
  public    static  Beyond.Input.KeyboardKeyCode    F5  // const
  public    static  Beyond.Input.KeyboardKeyCode    F6  // const
  public    static  Beyond.Input.KeyboardKeyCode    F7  // const
  public    static  Beyond.Input.KeyboardKeyCode    F8  // const
  public    static  Beyond.Input.KeyboardKeyCode    F9  // const
  public    static  Beyond.Input.KeyboardKeyCode    F10  // const
  public    static  Beyond.Input.KeyboardKeyCode    F11  // const
  public    static  Beyond.Input.KeyboardKeyCode    F12  // const
  public    static  Beyond.Input.KeyboardKeyCode    F13  // const
  public    static  Beyond.Input.KeyboardKeyCode    F14  // const
  public    static  Beyond.Input.KeyboardKeyCode    F15  // const
  public    static  Beyond.Input.KeyboardKeyCode    Numlock  // const
  public    static  Beyond.Input.KeyboardKeyCode    CapsLock  // const
  public    static  Beyond.Input.KeyboardKeyCode    ScrollLock  // const
  public    static  Beyond.Input.KeyboardKeyCode    RightShift  // const
  public    static  Beyond.Input.KeyboardKeyCode    LeftShift  // const
  public    static  Beyond.Input.KeyboardKeyCode    RightControl  // const
  public    static  Beyond.Input.KeyboardKeyCode    LeftControl  // const
  public    static  Beyond.Input.KeyboardKeyCode    RightAlt  // const
  public    static  Beyond.Input.KeyboardKeyCode    LeftAlt  // const
  public    static  Beyond.Input.KeyboardKeyCode    RightApple  // const
  public    static  Beyond.Input.KeyboardKeyCode    RightCommand  // const
  public    static  Beyond.Input.KeyboardKeyCode    RightMeta  // const
  public    static  Beyond.Input.KeyboardKeyCode    LeftApple  // const
  public    static  Beyond.Input.KeyboardKeyCode    LeftCommand  // const
  public    static  Beyond.Input.KeyboardKeyCode    LeftMeta  // const
  public    static  Beyond.Input.KeyboardKeyCode    LeftWindows  // const
  public    static  Beyond.Input.KeyboardKeyCode    RightWindows  // const
  public    static  Beyond.Input.KeyboardKeyCode    AltGr  // const
  public    static  Beyond.Input.KeyboardKeyCode    Help  // const
  public    static  Beyond.Input.KeyboardKeyCode    Print  // const
  public    static  Beyond.Input.KeyboardKeyCode    SysReq  // const
  public    static  Beyond.Input.KeyboardKeyCode    Break  // const
  public    static  Beyond.Input.KeyboardKeyCode    Menu  // const
  public    static  Beyond.Input.KeyboardKeyCode    Mouse0  // const
  public    static  Beyond.Input.KeyboardKeyCode    Mouse1  // const
  public    static  Beyond.Input.KeyboardKeyCode    Mouse2  // const
  public    static  Beyond.Input.KeyboardKeyCode    Mouse3  // const
  public    static  Beyond.Input.KeyboardKeyCode    Mouse4  // const
  public    static  Beyond.Input.KeyboardKeyCode    Mouse5  // const
  public    static  Beyond.Input.KeyboardKeyCode    Mouse6  // const
  public    static  Beyond.Input.KeyboardKeyCode    EscapeWithRightMouse  // const
METHODS:
END_CLASS

CLASS: Beyond.Input.GamepadKeyCode
TYPE:  sealed struct
TOKEN: 0x200000F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.Input.GamepadKeyCode     None  // const
  public    static  Beyond.Input.GamepadKeyCode     LeftStickX  // const
  public    static  Beyond.Input.GamepadKeyCode     LeftStickY  // const
  public    static  Beyond.Input.GamepadKeyCode     LeftStickBtn  // const
  public    static  Beyond.Input.GamepadKeyCode     RightStickX  // const
  public    static  Beyond.Input.GamepadKeyCode     RightStickY  // const
  public    static  Beyond.Input.GamepadKeyCode     RightStickBtn  // const
  public    static  Beyond.Input.GamepadKeyCode     ArrowUp  // const
  public    static  Beyond.Input.GamepadKeyCode     ArrowDown  // const
  public    static  Beyond.Input.GamepadKeyCode     ArrowLeft  // const
  public    static  Beyond.Input.GamepadKeyCode     ArrowRight  // const
  public    static  Beyond.Input.GamepadKeyCode     A  // const
  public    static  Beyond.Input.GamepadKeyCode     B  // const
  public    static  Beyond.Input.GamepadKeyCode     X  // const
  public    static  Beyond.Input.GamepadKeyCode     Y  // const
  public    static  Beyond.Input.GamepadKeyCode     LB  // const
  public    static  Beyond.Input.GamepadKeyCode     LT  // const
  public    static  Beyond.Input.GamepadKeyCode     RB  // const
  public    static  Beyond.Input.GamepadKeyCode     RT  // const
  public    static  Beyond.Input.GamepadKeyCode     LeftMenuBtn  // const
  public    static  Beyond.Input.GamepadKeyCode     RightMenuBtn  // const
  public    static  Beyond.Input.GamepadKeyCode     Home  // const
  public    static  Beyond.Input.GamepadKeyCode     TouchPanel  // const
  public    static  Beyond.Input.GamepadKeyCode     LeftStickUp  // const
  public    static  Beyond.Input.GamepadKeyCode     LeftStickDown  // const
  public    static  Beyond.Input.GamepadKeyCode     LeftStickLeft  // const
  public    static  Beyond.Input.GamepadKeyCode     LeftStickRight  // const
  public    static  Beyond.Input.GamepadKeyCode     RightStickUp  // const
  public    static  Beyond.Input.GamepadKeyCode     RightStickDown  // const
  public    static  Beyond.Input.GamepadKeyCode     RightStickLeft  // const
  public    static  Beyond.Input.GamepadKeyCode     RightStickRight  // const
METHODS:
END_CLASS

CLASS: Beyond.Input.InputTimingType
TYPE:  sealed struct
TOKEN: 0x2000010
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.Input.InputTimingType    OnPress  // const
  public    static  Beyond.Input.InputTimingType    OnRelease  // const
  public    static  Beyond.Input.InputTimingType    OnClick  // const
  public    static  Beyond.Input.InputTimingType    OnLongPress  // const
METHODS:
END_CLASS

CLASS: Beyond.Input.InputContinuousTriggerType
TYPE:  sealed struct
TOKEN: 0x2000011
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.Input.InputContinuousTriggerTypeNormalDelayAndLoopTime  // const
  public    static  Beyond.Input.InputContinuousTriggerTypeEveryTick  // const
METHODS:
END_CLASS

CLASS: Beyond.Input.KeyboardType
TYPE:  sealed struct
TOKEN: 0x2000012
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.Input.KeyboardType       None  // const
  public    static  Beyond.Input.KeyboardType       Default  // const
  public    static  Beyond.Input.KeyboardType       QWERTZ  // const
  public    static  Beyond.Input.KeyboardType       AZERTY  // const
METHODS:
END_CLASS

CLASS: Beyond.Input.GamepadCheckerBase
TYPE:  abstract class
TOKEN: 0x2000013
SIZE:  0x38
FIELDS:
  protected         Beyond.Input.InputManager       m_inputManager  // 0x10
  protected         Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>>m_normalBindingInfos  // 0x18
  protected         Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>>>m_modifyBindingInfos  // 0x20
  protected         System.Collections.Generic.HashSet<Beyond.Input.GamepadKeyCode>m_usedKeyCache  // 0x28
  protected         System.Collections.Generic.List<System.ValueTuple<Beyond.Input.GamepadCheckerBase.Binding,Beyond.Input.GamepadCheckerBase.TriggerType>>m_tmpNeedTriggerCallbacks  // 0x30
PROPERTIES:
  normalBindingInfos  get=0x09E340DC
  modifyBindingInfos  get=0x09E34090
METHODS:
  RVA=0x042B6C40  token=0x6000070  System.Void .ctor(Beyond.Input.InputManager inputManager)
  RVA=0x037D02F0  token=0x6000071  Beyond.Input.GamepadCheckerBase.Binding OnCreateBinding(Beyond.Input.GamepadInput input, Beyond.Input.InputBindingInfo info, System.Boolean isUsedForBlock)
  RVA=0x037CFCA0  token=0x6000072  Beyond.Input.GamepadCheckerBase.Binding OnDeleteBinding(Beyond.Input.GamepadInput input, Beyond.Input.InputBindingInfo info)
  RVA=0x037D0470  token=0x6000073  Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>> GetInputDic(Beyond.Input.GamepadInput input)
  RVA=0x09E33CEC  token=0x6000074  System.Void CheckGamepadInput()
  RVA=0x09E33C88  token=0x6000075  System.Void CheckDic(Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>> dic, Beyond.Input.GamepadKeyCode modifyKey)
  RVA=0x09E33E30  token=0x6000076  System.Void TriggerActions()
END_CLASS

CLASS: Beyond.Input.GamepadChecker
TYPE:  class
TOKEN: 0x2000016
SIZE:  0x48
EXTENDS: Beyond.Input.GamepadCheckerBase
FIELDS:
  private           System.Func<Beyond.Input.GamepadKeyCode,System.Boolean>m_checkFunc  // 0x38
  private           System.Boolean                  <isPressChecker>k__BackingField  // 0x40
PROPERTIES:
  isPressChecker  get=0x02D480A0  set=0x02D480B0
METHODS:
  RVA=0x042B62F0  token=0x600007A  System.Void .ctor(System.Func<Beyond.Input.GamepadKeyCode,System.Boolean> checkFunc, System.Boolean isPress, Beyond.Input.InputManager inputManager)
  RVA=0x09E34128  token=0x600007B  System.Void CheckDic(Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>> dic, Beyond.Input.GamepadKeyCode modifyKey)
  RVA=0x09E34794  token=0x600007C  System.Void <>iFixBaseProxy_CheckDic(Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>> P0, Beyond.Input.GamepadKeyCode P1)
END_CLASS

CLASS: Beyond.Input.GamepadOnClickChecker
TYPE:  class
TOKEN: 0x2000017
SIZE:  0x38
EXTENDS: Beyond.Input.GamepadCheckerBase
FIELDS:
METHODS:
  RVA=0x053973D4  token=0x600007D  System.Void .ctor(Beyond.Input.InputManager inputManager)
  RVA=0x09E3479C  token=0x600007E  System.Void CheckDic(Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>> dic, Beyond.Input.GamepadKeyCode modifyKey)
  RVA=0x09E34F00  token=0x600007F  System.Void ClearKeyPressed(Beyond.Input.GamepadKeyCode keyCode)
  RVA=0x09E34794  token=0x6000080  System.Void <>iFixBaseProxy_CheckDic(Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>> P0, Beyond.Input.GamepadKeyCode P1)
END_CLASS

CLASS: Beyond.Input.GamepadOnLongPressChecker
TYPE:  class
TOKEN: 0x2000018
SIZE:  0x48
EXTENDS: Beyond.Input.GamepadCheckerBase
FIELDS:
  private           System.Action<Beyond.Input.GamepadKeyCode>m_onTriggerOnLongPress  // 0x38
  private           System.Collections.Generic.Dictionary<Beyond.Input.GamepadInput,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>>m_bindings  // 0x40
METHODS:
  RVA=0x042B6600  token=0x6000081  System.Void .ctor(System.Action<Beyond.Input.GamepadKeyCode> onTriggerOnLongPress, Beyond.Input.InputManager inputManager)
  RVA=0x09E35A50  token=0x6000082  System.Action CreateTriggerBindingAction(Beyond.Input.GamepadCheckerBase.Binding binding, Beyond.Input.GamepadKeyCode keyCode)
  RVA=0x09E35328  token=0x6000083  System.Void CheckDic(Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>> dic, Beyond.Input.GamepadKeyCode modifyKey)
  RVA=0x09E35D74  token=0x6000084  System.Void _OnTriggerBinding(Beyond.Input.GamepadCheckerBase.Binding binding, Beyond.Input.GamepadKeyCode keyCode)
  RVA=0x04611F30  token=0x6000085  Beyond.Input.GamepadCheckerBase.Binding OnCreateBinding(Beyond.Input.GamepadInput input, Beyond.Input.InputBindingInfo info, System.Boolean isUsedForBlock)
  RVA=0x04611E50  token=0x6000086  Beyond.Input.GamepadCheckerBase.Binding OnDeleteBinding(Beyond.Input.GamepadInput input, Beyond.Input.InputBindingInfo info)
  RVA=0x037CFEE0  token=0x6000087  System.Void CancelPendingLongPress(Beyond.Input.InputBindingInfo info)
  RVA=0x09E35B50  token=0x6000088  System.Single GetInputLongPressStartTime(Beyond.Input.GamepadInput input, System.Int32 bindingId)
  RVA=0x09E34794  token=0x6000089  System.Void <>iFixBaseProxy_CheckDic(Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>> P0, Beyond.Input.GamepadKeyCode P1)
  RVA=0x09E35D5C  token=0x600008A  Beyond.Input.GamepadCheckerBase.Binding <>iFixBaseProxy_OnCreateBinding(Beyond.Input.GamepadInput P0, Beyond.Input.InputBindingInfo P1, System.Boolean P2)
  RVA=0x09E35D6C  token=0x600008B  Beyond.Input.GamepadCheckerBase.Binding <>iFixBaseProxy_OnDeleteBinding(Beyond.Input.GamepadInput P0, Beyond.Input.InputBindingInfo P1)
END_CLASS

CLASS: Beyond.Input.GamepadOnReleaseChecker
TYPE:  class
TOKEN: 0x200001A
SIZE:  0x48
EXTENDS: Beyond.Input.GamepadCheckerBase
FIELDS:
  private   readonly System.Func<Beyond.Input.GamepadKeyCode,System.Boolean>m_checkFunc  // 0x38
  private   readonly System.Collections.Generic.Dictionary<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadKeyCode>>m_modifyBindingPressedDic  // 0x40
METHODS:
  RVA=0x042B6550  token=0x600008E  System.Void .ctor(System.Func<Beyond.Input.GamepadKeyCode,System.Boolean> checkFunc, Beyond.Input.InputManager inputManager)
  RVA=0x09E368B8  token=0x600008F  System.Void CheckGamepadInput()
  RVA=0x09E3622C  token=0x6000090  System.Void CheckDic(Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>> dic, Beyond.Input.GamepadKeyCode modifyKey)
  RVA=0x09E36E50  token=0x6000091  System.Boolean <CheckGamepadInput>b__3_0(Beyond.Input.GamepadKeyCode keyCode)
  RVA=0x09E36E78  token=0x6000092  System.Void <>iFixBaseProxy_CheckGamepadInput()
  RVA=0x09E34794  token=0x6000093  System.Void <>iFixBaseProxy_CheckDic(Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>> P0, Beyond.Input.GamepadKeyCode P1)
END_CLASS

CLASS: Beyond.Input.HGVirtualMouse
TYPE:  class
TOKEN: 0x200001B
SIZE:  0x170
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private   static  System.Single                   STANDARD_SCREEN_WIDTH  // const
  public            System.Boolean                  keepMousePosOnEnable  // 0x18
  private           UnityEngine.RectTransform       m_CursorTransform  // 0x20
  private           UnityEngine.Animator            m_animator  // 0x28
  public            System.Single                   currentCursorSpeed  // 0x30
  public            System.Single                   currentCursorAcc  // 0x34
  public            System.Collections.Generic.List<Beyond.Input.HGVirtualMouse.CursorSpeedPhase>speedPhaseList  // 0x38
  public            System.Collections.Generic.List<Beyond.Input.HGVirtualMouse.CursorSpeedPhase>hoverSpeedPhaseList  // 0x40
  public            System.Collections.Generic.List<Beyond.Input.HGVirtualMouse.CursorSpeedPhase>secondHoverSpeedPhaseList  // 0x48
  private           System.Single                   m_ScrollSpeed  // 0x50
  private           UnityEngine.InputSystem.InputActionPropertym_StickAction  // 0x58
  private           UnityEngine.InputSystem.InputActionPropertym_LeftButtonAction  // 0x70
  private           UnityEngine.InputSystem.InputActionPropertym_MiddleButtonAction  // 0x88
  private           UnityEngine.InputSystem.InputActionPropertym_RightButtonAction  // 0xa0
  private           UnityEngine.InputSystem.InputActionPropertym_ForwardButtonAction  // 0xb8
  private           UnityEngine.InputSystem.InputActionPropertym_BackButtonAction  // 0xd0
  private           UnityEngine.InputSystem.InputActionPropertym_ScrollWheelAction  // 0xe8
  private           UnityEngine.RectTransform       _canvasTransform  // 0x100
  private           UnityEngine.InputSystem.Mouse   m_VirtualMouse  // 0x108
  private           UnityEngine.InputSystem.Mouse   m_SystemMouse  // 0x110
  private           System.Action<UnityEngine.InputSystem.InputAction.CallbackContext>m_ButtonActionTriggeredDelegate  // 0x118
  private           System.Double                   m_LastTime  // 0x120
  private           UnityEngine.Vector2             m_LastStickValue  // 0x128
  private           System.Single                   m_lastDisabledTime  // 0x130
  private           System.Single                   _resetCursorPosMinInterval  // 0x134
  public            System.Int32                    leftBtnBindingGroupId  // 0x138
  public            System.Int32                    leftBtnPressBindingId  // 0x13c
  public            System.Int32                    leftBtnReleaseBindingId  // 0x140
  private           System.Collections.Generic.HashSet<UnityEngine.UI.Selectable>m_hoverTargets  // 0x148
  private           System.Boolean                  <isHoverOn>k__BackingField  // 0x150
  private           System.Boolean                  m_usingHoverSpd  // 0x151
  private           System.Int32                    m_encounterHoverCount  // 0x154
  private           System.Single                   m_animatorHoverValue  // 0x158
  private           System.Boolean                  m_isIdle  // 0x15c
  private           System.Single                   _hoverAnimationChangeSpeed  // 0x160
  private           System.Boolean                  <mouseIconVisible>k__BackingField  // 0x164
  private           UnityEngine.CanvasGroup         m_iconCanvasGroup  // 0x168
PROPERTIES:
  cursorTransform  get=0x09E39298  set=0x09E3974C
  scrollSpeed  get=0x09E394C0  set=0x09E39BC4
  virtualMouse  get=0x09E395F8
  stickAction  get=0x09E39588  set=0x09E39CB0
  leftButtonAction  get=0x09E3935C  set=0x09E398B4
  rightButtonAction  get=0x09E39448  set=0x09E39ABC
  middleButtonAction  get=0x09E393D0  set=0x09E399B4
  forwardButtonAction  get=0x09E392E4  set=0x09E397AC
  backButtonAction  get=0x09E39220  set=0x09E39644
  scrollWheelAction  get=0x09E3950C  set=0x09E39C1C
  isHoverOn  get=0x04DA2F60  set=0x04DA2F80
  mouseIconVisible  get=0x04DA2F70  set=0x04DA2F90
METHODS:
  RVA=0x09E378E0  token=0x60000A7  System.Void OnEnable()
  RVA=0x09E37554  token=0x60000A8  System.Void OnDisable()
  RVA=0x09E38974  token=0x60000A9  System.Void _UpdateMotion()
  RVA=0x09E383F0  token=0x60000AA  System.Void _OnButtonActionTriggered(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=0x09E380F4  token=0x60000AB  System.Void TriggerLeftButton(System.Boolean isPressed)
  RVA=0x09E37158  token=0x60000AC  System.Void ClearLeftButtonState()
  RVA=0x09E38614  token=0x60000AD  System.Void _SetActionCallback(UnityEngine.InputSystem.InputActionProperty field, System.Action<UnityEngine.InputSystem.InputAction.CallbackContext> callback, System.Boolean install)
  RVA=0x09E386D8  token=0x60000AE  System.Void _SetAction(UnityEngine.InputSystem.InputActionProperty& field, UnityEngine.InputSystem.InputActionProperty value)
  RVA=0x09E373DC  token=0x60000AF  System.Void OnAfterInputUpdate()
  RVA=0x09E38240  token=0x60000B2  System.Void Update()
  RVA=0x09E36F14  token=0x60000B3  System.Void ChangeHoverState(UnityEngine.UI.Selectable target, System.Boolean isHoverOn)
  RVA=0x09E38374  token=0x60000B4  System.Void _ChangeHoverState(System.Boolean isHoverOn)
  RVA=0x09E3724C  token=0x60000B5  UnityEngine.UI.Selectable GetCurHoverSelectable()
  RVA=0x09E3742C  token=0x60000B6  System.Void OnDestroy()
  RVA=0x09E3882C  token=0x60000B7  System.Void _SyncScale()
  RVA=0x09E36E80  token=0x60000BA  System.Void Awake()
  RVA=0x09E38074  token=0x60000BB  System.Void SetMouseIconVisible(System.Boolean active)
  RVA=0x040F9790  token=0x60000BC  System.Void .ctor()
  RVA=0x09E38228  token=0x60000BD  System.Void <OnEnable>b__32_0()
  RVA=0x09E38234  token=0x60000BE  System.Void <OnEnable>b__32_1()
END_CLASS

CLASS: Beyond.Input.InputBindingInfo
TYPE:  class
TOKEN: 0x200001E
SIZE:  0x60
FIELDS:
  private   static  System.UInt32                   s_nextEnabledPriority  // static @ 0x0
  private   readonly System.Int32                    <id>k__BackingField  // 0x10
  private           System.Action                   m_callback  // 0x18
  private           Beyond.Input.InputBindingGroup  <group>k__BackingField  // 0x20
  public            System.String                   playerActionId  // 0x28
  private           System.Boolean                  <shouldShowControllerHint>k__BackingField  // 0x30
  private           System.Int32                    <controllerHintPriority>k__BackingField  // 0x34
  private           System.Boolean                  <isCommonOpenUIAction>k__BackingField  // 0x38
  private           System.Boolean                  <selfEnabled>k__BackingField  // 0x39
  private           System.Boolean                  <groupEnabled>k__BackingField  // 0x3a
  private           System.Boolean                  <parentIgnored>k__BackingField  // 0x3b
  public            System.Boolean                  forceKeyhintToGrayState  // 0x3c
  private           System.UInt32                   <enabledPriority>k__BackingField  // 0x40
  private           System.Int32                    <actionPriority>k__BackingField  // 0x44
  private           Beyond.Input.IBindingView       m_bindingView  // 0x48
  private           System.String                   m_text  // 0x50
  public    static  System.Single                   s_continuousTriggerFirstDelayTime  // static @ 0x4
  public    static  System.Single                   s_continuousTriggerLoopDelayTime  // static @ 0x8
  public    static  System.Single                   s_continuousTriggerLoopQuickDelayTime  // static @ 0xc
  private           System.Int32                    m_continuousTriggerTimerId  // 0x58
PROPERTIES:
  id  get=0x02B2E2D0
  group  get=0x04D862C0  set=0x05390F40
  shouldShowControllerHint  get=0x04D865F0  set=0x04D86620
  controllerHintPriority  get=0x04D86730  set=0x04D86760
  isCommonOpenUIAction  get=0x011F2A70  set=0x011F2A80
  selfEnabled  get=0x04D86650  set=0x04D86680
  groupEnabled  get=0x04D86660  set=0x04D86690
  enabled  get=0x037CE190
  parentIgnored  get=0x04D86D10  set=0x04D86D20
  enabledPriority  get=0x04D865E0  set=0x04D86610
  actionPriority  get=0x04D86530  set=0x04D86580
  bindingView  get=0x037D5340  set=0x09E3A584
  text  get=0x09E3A49C  set=0x043EF260
METHODS:
  RVA=0x037D0130  token=0x60000C3  System.UInt32 _GetNextEnabledPriority()
  RVA=0x037CE370  token=0x60000DC  System.Void .ctor(System.Int32 id, Beyond.Input.InputBindingGroup group, System.Action callback, System.String playerActionId, Beyond.Input.PlayerActionInfo actionInfo)
  RVA=0x09E39E70  token=0x60000DD  System.Void ChangeGroup(Beyond.Input.InputBindingGroup newGroup)
  RVA=0x037D0F80  token=0x60000DE  System.Void SetSelfEnabled(System.Boolean value)
  RVA=0x037CEA10  token=0x60000DF  System.Void OnGroupEnabledChanged(System.Boolean value, System.Boolean silent)
  RVA=0x09E39F4C  token=0x60000E0  System.Void SetParentIgnored(System.Boolean value)
  RVA=0x03629C00  token=0x60000E1  System.Void Trigger()
  RVA=0x09E39FA8  token=0x60000E2  System.Void StartContinuousTrigger(Beyond.Input.InputContinuousTriggerType continuousTriggerType)
  RVA=0x09E3A434  token=0x60000E3  System.Void _TryContinuousTrigger()
  RVA=0x037CF850  token=0x60000E4  System.Void StopContinuousTrigger()
  RVA=0x03629C40  token=0x60000E5  System.Void _RealTriggerCallback()
  RVA=0x037D0610  token=0x60000E6  System.Void OnDelete()
  RVA=0x09E39EF4  token=0x60000E7  System.Object GetCallbackTarget()
  RVA=0x04D35BB0  token=0x60000E8  System.Void .cctor()
  RVA=0x09E3A154  token=0x60000E9  System.Void <StartContinuousTrigger>b__64_0()
  RVA=0x09E3A248  token=0x60000EA  System.Void <StartContinuousTrigger>b__64_1()
  RVA=0x09E3A33C  token=0x60000EB  System.Void <StartContinuousTrigger>b__64_2()
END_CLASS

CLASS: Beyond.Input.InputBindingGroup
TYPE:  class
TOKEN: 0x200001F
SIZE:  0x40
FIELDS:
  private   readonly System.Int32                    <id>k__BackingField  // 0x10
  private           System.Boolean                  <selfEnabled>k__BackingField  // 0x14
  private           System.Boolean                  <parentEnabled>k__BackingField  // 0x15
  private           System.Boolean                  <parentIgnored>k__BackingField  // 0x16
  private           Beyond.Input.InputBindingGroup  <parent>k__BackingField  // 0x18
  private   readonly System.Collections.Generic.LinkedList<Beyond.Input.InputBindingGroup><subGroups>k__BackingField  // 0x20
  private   readonly System.Collections.Generic.LinkedList<Beyond.Input.InputBindingInfo><subInfos>k__BackingField  // 0x28
  private           Beyond.Input.IBindingView       m_groupView  // 0x30
  private           Beyond.Input.IBindingGroupTargetm_groupTarget  // 0x38
PROPERTIES:
  id  get=0x02B2E2D0
  selfEnabled  get=0x04D8EE50  set=0x04D8EE70
  parentEnabled  get=0x04D8EE60  set=0x04D8EE80
  enabled  get=0x03794D70
  parentIgnored  get=0x04DA2FA0  set=0x04DA2FB0
  parent  get=0x0385B100  set=0x05392C40
  subGroups  get=0x04D862C0
  subInfos  get=0x04D86240
  groupView  get=0x037D53C0  set=0x03EAD8D0
  groupTarget  get=0x09E39E0C  set=0x037932F0
METHODS:
  RVA=0x03793360  token=0x60000FC  System.Void .ctor(System.Int32 id, Beyond.Input.InputBindingGroup parent)
  RVA=0x042B5BC0  token=0x60000FD  System.Void ChangeParent(Beyond.Input.InputBindingGroup newParent)
  RVA=0x03793CB0  token=0x60000FE  System.Void SetSelfEnabled(System.Boolean value)
  RVA=0x09E39D8C  token=0x60000FF  System.Void SetParentIgnored(System.Boolean value, System.Boolean silent)
  RVA=0x03794770  token=0x6000100  System.Void OnParentEnabledChanged(System.Boolean value, System.Boolean silent)
  RVA=0x037943E0  token=0x6000101  System.Void _OnBindingGroupStateChanged(System.Boolean silent)
END_CLASS

CLASS: Beyond.Input.InputBindingGroupMonoTarget
TYPE:  class
TOKEN: 0x2000020
SIZE:  0x28
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Beyond.Input.IBindingGroupTarget
FIELDS:
  private           System.Int32                    m_groupId  // 0x18
  private           System.Boolean                  m_init  // 0x1c
  private           System.Boolean                  m_internalEnabled  // 0x1d
  private           Beyond.Input.InputBindingGroupMonoTarget<parent>k__BackingField  // 0x20
PROPERTIES:
  groupId  get=0x037D42E0
  groupEnabled  get=0x037D3480
  parent  get=0x04D862C0  set=0x05390F40
  parentTarget  get=0x09E39D3C
  internalEnabled  get=0x03C73B10  set=0x037D46C0
METHODS:
  RVA=0x037D4340  token=0x6000109  System.Void Init()
  RVA=0x037D4790  token=0x600010A  System.Void UpdateState()
  RVA=0x037D4180  token=0x600010B  System.Void DeleteGroup()
  RVA=0x037D42A0  token=0x600010C  System.Void Awake()
  RVA=0x037D4750  token=0x600010D  System.Void OnEnable()
  RVA=0x037D4710  token=0x600010E  System.Void OnDisable()
  RVA=0x037D3D00  token=0x600010F  System.Void OnDestroy()
  RVA=0x044F3520  token=0x6000110  System.Void .ctor()
END_CLASS

CLASS: Beyond.Input.IBindingGroupTarget
TYPE:  interface
TOKEN: 0x2000021
FIELDS:
PROPERTIES:
  groupId  get=-1  // abstract
  parentTarget  get=-1  // abstract
  groupEnabled  get=-1  // abstract
METHODS:
END_CLASS

CLASS: Beyond.Input.IBindingTarget
TYPE:  interface
TOKEN: 0x2000022
FIELDS:
PROPERTIES:
  bindingId  get=-1  // abstract
  parentTarget  get=-1  // abstract
  bindingEnabled  get=-1  // abstract
METHODS:
END_CLASS

CLASS: Beyond.Input.BindingViewState
TYPE:  sealed struct
TOKEN: 0x2000023
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.Input.BindingViewState   Normal  // const
  public    static  Beyond.Input.BindingViewState   Disabled  // const
  public    static  Beyond.Input.BindingViewState   Hide  // const
METHODS:
END_CLASS

CLASS: Beyond.Input.BindingViewActionType
TYPE:  sealed struct
TOKEN: 0x2000024
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.Input.BindingViewActionTypeDefault  // const
  public    static  Beyond.Input.BindingViewActionTypeButtonClick  // const
  public    static  Beyond.Input.BindingViewActionTypeButtonLongPress  // const
  public    static  Beyond.Input.BindingViewActionTypeButtonPressStart  // const
  public    static  Beyond.Input.BindingViewActionTypeButtonPressEnd  // const
  public    static  Beyond.Input.BindingViewActionTypeButtonDoubleClick  // const
  public    static  Beyond.Input.BindingViewActionTypeSliderAdd  // const
  public    static  Beyond.Input.BindingViewActionTypeSliderReduce  // const
  public    static  Beyond.Input.BindingViewActionTypeSliderStick  // const
METHODS:
END_CLASS

CLASS: Beyond.Input.IBindingView
TYPE:  interface
TOKEN: 0x2000025
FIELDS:
PROPERTIES:
  bindingViewActionId  get=-1  // abstract
  bindingViewLabelText  get=-1  // abstract
  bindingViewUpdateEvent  get=-1  // abstract
  interactable  get=-1  // abstract
  redDotTrans  get=-1  // abstract
  bindingEnabled  get=-1  // abstract
  bindingViewState  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x600011E  System.String GetBindingViewActionId(Beyond.Input.BindingViewActionType actionType)
  RVA=-1  // abstract  token=0x600011F  System.Void OnInputKeyDown(System.Boolean isActionTrigger)
  RVA=-1  // abstract  token=0x6000120  System.Void OnInputKeyUp(System.Boolean isActionTrigger)
END_CLASS

CLASS: Beyond.Input.InputSettingLevel
TYPE:  sealed struct
TOKEN: 0x2000026
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Beyond.Input.InputSettingLevel  Default  // const
  public    static  Beyond.Input.InputSettingLevel  Custom  // const
  public    static  Beyond.Input.InputSettingLevel  Pending  // const
METHODS:
END_CLASS

CLASS: Beyond.Input.InputManager
TYPE:  class
TOKEN: 0x2000027
SIZE:  0x1E0
EXTENDS: Beyond.Singleton`1
IMPLEMENTS: System.IDisposable
FIELDS:
  public    static  System.Single                   s_longPressTime  // static @ 0x0
  private   static  System.Collections.Generic.Dictionary<Beyond.Input.KeyboardKeyCode,System.String>s_keyboardKeyCode2StringDict  // static @ 0x8
  private   static  System.Collections.Generic.Dictionary<Beyond.Input.GamepadKeyCode,System.String>s_gamepadKeyCode2StringDict  // static @ 0x10
  public            Beyond.MultiAction              onKeyboardTypeChange  // 0x10
  public            System.Boolean                  enableController  // 0x18
  public            System.Boolean                  enableMarketingCamera  // 0x19
  private           System.Int32                    m_nextInputId  // 0x1c
  private           System.Int32                    m_nextGroupId  // 0x20
  private           Beyond.Input.InputBindingGroup  m_rootGroup  // 0x28
  private           System.Collections.Generic.Dictionary<System.Int32,Beyond.Input.InputBindingGroup>m_groups  // 0x30
  private           System.Collections.Generic.Dictionary<System.Int32,Beyond.Input.InputBindingInfo>m_bindingInfos  // 0x38
  private           System.Collections.Generic.Dictionary<System.String,System.Collections.Generic.HashSet<System.Int32>>m_action2BindingDic  // 0x40
  private           Beyond.Input.KeyboardChecker    m_onPressKeyboardChecker  // 0x48
  private           Beyond.Input.KeyboardChecker    m_onReleaseKeyboardChecker  // 0x50
  private           Beyond.Input.KeyboardOnClickCheckerm_onClickKeyboardChecker  // 0x58
  private           Beyond.Input.KeyboardOnLongPressCheckerm_onLongPressKeyboardChecker  // 0x60
  private           Beyond.Input.GamepadChecker     m_onPressGamepadChecker  // 0x68
  private           Beyond.Input.GamepadOnReleaseCheckerm_onReleaseGamepadChecker  // 0x70
  private           Beyond.Input.GamepadOnClickCheckerm_onClickGamepadChecker  // 0x78
  private           Beyond.Input.GamepadOnLongPressCheckerm_onLongPressGamepadChecker  // 0x80
  private           Beyond.Input.RealCursorManager  m_realCursorManager  // 0x88
  public    static  Beyond.MultiAction<Rewired.Joystick,Beyond.DeviceInfo.ControllerType>onJoystickChanged  // static @ 0x18
  private           Rewired.Joystick                <joystick>k__BackingField  // 0x90
  private           Rewired.IGamepadTemplate        <gamepadTemplate>k__BackingField  // 0x98
  private           Rewired.ControllerExtensions.IDualShock4Extension<gamepadDualShock4Extension>k__BackingField  // 0xa0
  private           System.Int32                    m_touchPanelBtnIndex  // 0xa8
  private           System.Int32                    m_leftMenuBtnIndex  // 0xac
  private           Beyond.Input.KeyboardHandler    m_keyboardHandler  // 0xb0
  private           Beyond.Input.InputManager.KeyCachesm_keyCaches  // 0xb8
  private           Beyond.TickFunction             m_lateTickFunction  // 0xc0
  public            Beyond.MultiAction              onInputLateTick  // 0xc8
  public            System.Action                   beforeCheckInput  // 0xd0
  public            System.Action                   afterCheckInput  // 0xd8
  public            System.Boolean                  trimNonTargetInputChecks  // 0xe0
  public            System.Boolean                  commonOpenUIActionTriggered  // 0xe1
  public            System.Boolean                  disableChangeInputDeviceCheck  // 0xe2
  public            System.Boolean                  needProcessTryChange  // 0xe3
  private           System.Boolean                  <inChangingInputDevice>k__BackingField  // 0xe4
  private           System.Boolean                  m_suppressActionsForDeviceSwitch  // 0xe5
  private           System.Int32                    m_suppressFramesRemaining  // 0xe8
  private   static  System.Int32                    DEVICE_SWITCH_SUPPRESS_FRAMES  // const
  private           System.Boolean                  m_lastAnyOtherDeviceKey  // 0xec
  private           System.Int32                    m_controllerForbidClickBindingId  // 0xf0
  private           System.Boolean                  m_ignoreFirstMouseUpOnFocusBack  // 0xf4
  private   static readonly Beyond.Input.KeyboardKeyCode[]  S_CACHED_KEYBOARD_KEY_CODES  // static @ 0x20
  private   static  System.Boolean                  s_autoUpdateInputBindingMask  // static @ 0x28
  private           System.Boolean                  m_hasFocusedInputFiled  // 0xf5
  private   static  System.Single                   STICK_DEAD_ZONE_MAGNITUDE  // const
  private   static  System.Single                   STICK_MIN_VALUE  // const
  private   static  System.Single                   STICK_DEAD_ZONE_SQRT_MAGNITUDE  // const
  private           Beyond.Resource.IAssetLoader    m_assetLoader  // 0xf8
  private           Beyond.SerializeFieldDictionary<System.String,Beyond.Input.PlayerActionInfo>m_defaultSetting  // 0x100
  private           Beyond.SerializeFieldDictionary<System.String,Beyond.Input.PlayerActionInfo>m_customSetting  // 0x108
  private           Beyond.SerializeFieldDictionary<System.String,Beyond.Input.PlayerActionInfo>m_pendingSetting  // 0x110
  private           Beyond.Input.InputRebindConfig  m_rebindConfig  // 0x118
  private           System.Collections.Generic.Dictionary<System.String,Beyond.Input.InputRebindActionScopeConfig>m_rebindActionScopeConfigs  // 0x120
  private           System.Collections.Generic.Dictionary<System.String,Beyond.Input.PlayerActionInfo>m_anonymousActionInfos  // 0x128
  private           System.Int32                    m_nextAnonymousPlayerActionId  // 0x130
  private   static  System.String                   INPUT_MANAGER_CANVAS_PREFAB_PATH  // const
  private           Beyond.Input.HGVirtualMouse     <virtualMouse>k__BackingField  // 0x138
  private           UnityEngine.InputSystem.UI.DebugVirtualMouseInput<debugVirtualMouse>k__BackingField  // 0x140
  private           UnityEngine.Transform           <canvasRoot>k__BackingField  // 0x148
  private           UnityEngine.Transform           <controllerMask>k__BackingField  // 0x150
  private           System.Boolean                  <usingVirtualMouse>k__BackingField  // 0x158
  private           System.Int32                    m_virtualMouseBindingGroupId  // 0x15c
  private           Beyond.Resource.FAssetProxyHandlem_canvasPrefabHandle  // 0x160
  private           UnityEngine.Transform           <customControllerMouseTrans>k__BackingField  // 0x178
  private           UnityEngine.Camera              <customControllerMouseUICamera>k__BackingField  // 0x180
  private           System.Collections.Generic.List<Beyond.Input.InputManager.ControllerHintInfo>m_tmpControllerHintInfoList  // 0x188
  public    readonly System.Collections.Generic.HashSet<System.String>guideUseActionIds  // 0x190
  private   static  System.Single                   DEFAULT_VIBRATE_LEVEL  // const
  private   static  System.Text.RegularExpressions.Regexs_actIdRegex  // static @ 0x30
  private   static  System.String                   TEXT_SPRITE_FORMAT  // const
  private   static  System.Single                   TEXT_SPRITE_SCALE  // const
  public    static  System.String                   LONG_PRESS_KEY_ICON_POSTFIX  // const
  private   static  System.String                   KEY_HINT_ICON_COMMON_FOLDER  // const
  private   static  System.String                   KEYBOARD_KEY_HINT_ICON_PATH  // const
  private   static  System.String                   GAMEPAD_KEY_HINT_ICON_PATH_PS5  // const
  private   static  System.String                   GAMEPAD_KEY_HINT_ICON_PATH_PS4  // const
  private   static  System.String                   GAMEPAD_KEY_HINT_ICON_PATH_XBOX  // const
  private   static  System.String                   GAMEPAD_KEY_HINT_ICON_PATH_FALLBACK  // const
  public    static  System.String                   s_virtualMouseClickHintActionId  // static @ 0x38
  public    static  System.String                   s_virtualMouseLongPressHintActionId  // static @ 0x40
  public            Beyond.MultiAction<UnityEngine.Vector2>onStartSwipeTouchPanel  // 0x198
  public            Beyond.MultiAction<UnityEngine.Vector2,UnityEngine.Vector2>onSwipeTouchPanel  // 0x1a0
  public            Beyond.MultiAction              onEndSwipeTouchPanel  // 0x1a8
  private           System.Boolean                  <isSwipingTouchPanel>k__BackingField  // 0x1b0
  private           UnityEngine.Vector2             m_lastTouchPanelPos  // 0x1b4
  public            Beyond.Input.ControllerNaviManagercontrollerNaviManager  // 0x1c0
  public            System.Boolean                  disableCheckInputForNetMask  // 0x1c8
  private           System.Collections.Generic.List<Beyond.Input.InputManager.DelayedComboBinding>m_delayedCombos  // 0x1d0
  private           System.Boolean                  m_delayedComboKeyUpdateSubscribed  // 0x1d8
  private   static  Unity.Profiling.ProfilerMarker  s_updateDelayedCombosMarker  // static @ 0x48
PROPERTIES:
  isDebugForceShow  get=0x09E2FB84
  cursorVisible  get=0x033FDFC0
  anyKeyDown  get=0x037CF460
  mouseScrollDelta  get=0x037CF710
  multiTouchEnabled  get=0x0429EE70  set=0x046DA760
  usingController  get=0x033FD440  set=0x09E2FD80
  virtualMouseIconVisible  get=0x09E2FC40
  keyboardType  get=0x09E2FBE4  set=0x04731010
  invalidID  get=0x037CE430
  joystick  get=0x04D86220  set=0x06402330
  gamepadTemplate  get=0x04D85EE0  set=0x06352D0C
  gamepadDualShock4Extension  get=0x04D86210  set=0x05391334
  action2BindingDic  get=0x037CE220
  rootGroupId  get=0x04D2BAF0
  inChangingInputDevice  get=0x04D91930  set=0x04D919A0
  isInputSuppressedByDeviceSwitch  get=0x033FE910
  autoUpdateInputBindingMask  get=0x03DF9D10  set=0x09E2FCAC
  virtualMouse  get=0x04D8DC90  set=0x05397184
  debugVirtualMouse  get=0x04D8DA60  set=0x06B8B404
  canvasRoot  get=0x04D8DAC0  set=0x06B8B468
  controllerMask  get=0x04D8D890  set=0x06B8B224
  usingVirtualMouse  get=0x04D907E0  set=0x04D907F0
  mousePosition  get=0x034021D0
  customControllerMouseTrans  get=0x04D8DEE0  set=0x06B8B92C
  customControllerMouseUICamera  get=0x04D8DD60  set=0x06B8B774
  isSwipingTouchPanel  get=0x04D90970  set=0x04D909A0
METHODS:
  RVA=0x047C6070  token=0x6000135  System.Void .ctor()
  RVA=0x036E7D40  token=0x6000136  System.Void LateTick(System.Single deltaTime)
  RVA=0x042B4D70  token=0x6000137  System.Void _Init()
  RVA=0x09E2C000  token=0x6000138  System.Void Release()
  RVA=0x09E2D720  token=0x6000139  System.Void _ClearTickFunction()
  RVA=0x09E2ABCC  token=0x600013A  System.Void OnApplicationQuit()
  RVA=0x09E2D4A4  token=0x600013B  System.Void _ClearEvents()
  RVA=0x03304780  token=0x600013C  System.Void _OnAfterInputUpdate()
  RVA=0x09E2ED94  token=0x600013D  System.Void _OnControllerConnected(Rewired.ControllerStatusChangedEventArgs args)
  RVA=0x09E2EEB8  token=0x600013E  System.Void _OnControllerDisconnected(Rewired.ControllerStatusChangedEventArgs args)
  RVA=0x04A318A0  token=0x600013F  System.Void _TryFindGamepad()
  RVA=0x09E2ECB8  token=0x6000140  System.Boolean _IsValidJoystick(Rewired.Joystick js)
  RVA=0x09E2F260  token=0x6000141  System.Void _SetJoystick(Rewired.Joystick js)
  RVA=0x034DEF90  token=0x6000142  System.Void _CheckInput()
  RVA=0x02D45EA0  token=0x6000143  Beyond.DeviceInfo.ControllerType _GetControllerType(Rewired.Joystick js, System.Boolean& isNonSupportPsController)
  RVA=0x03A74530  token=0x6000144  System.Void OnApplicationFocus(System.Boolean hasFocus)
  RVA=0x042B6930  token=0x6000148  System.Void _InitDeviceChangeBinding()
  RVA=0x03DF9CB0  token=0x6000149  System.Void _RefreshDeviceChangeBinding()
  RVA=0x034DEB50  token=0x600014A  System.Void CheckUsingController()
  RVA=0x09E2D44C  token=0x600014B  System.Boolean _CheckAnyKeyForTouchChangeDevice()
  RVA=0x09E2D1CC  token=0x600014C  System.Boolean _CheckAnyKeyForKeyboardChangeDevice()
  RVA=0x09E2D048  token=0x600014D  System.Boolean _AnyKeyDownForKeyboardChangeDevice()
  RVA=0x09E2F920  token=0x600014E  System.Void _TryChangeInputDeviceType(Beyond.DeviceInfo.InputType inputType)
  RVA=0x09E2FA70  token=0x600014F  System.Void _TryStopDeviceSwitchSuppression()
  RVA=0x03DF8DC0  token=0x6000150  System.Void _SyncUsingControllerState()
  RVA=0x09E2FAD4  token=0x6000153  System.Void _UpdateInputBindingMask(Beyond.DeviceInfo.InputType inputType)
  RVA=0x09E2F04C  token=0x6000154  System.Void _SetInputBindingMask(System.String group)
  RVA=0x09E2CE14  token=0x6000155  System.Void ToggleInputDeviceChangeMode(System.Boolean inChanging)
  RVA=0x09E2C74C  token=0x6000156  System.Void SetCursorOverrideForDeviceChange(System.Boolean forceShow)
  RVA=0x09E279BC  token=0x6000157  System.Void ChangeInputTypeAndSyncState(Beyond.DeviceInfo.InputType inputType)
  RVA=0x03CD4D00  token=0x6000158  System.Void _OnFocusedInputFieldChanged(Beyond.EventData<System.Boolean>& v)
  RVA=0x042B6810  token=0x6000159  System.Void _CreateTickFunction()
  RVA=0x037D0760  token=0x600015A  System.Void _RemovePlayerActionCheck(Beyond.Input.InputBindingInfo info)
  RVA=0x037CE710  token=0x600015B  System.Void _AddPlayerActionCheck(Beyond.Input.InputBindingInfo info)
  RVA=0x037CE7E0  token=0x600015C  System.Void _AddPlayerActionCheck(Beyond.Input.KeyboardInput input, Beyond.Input.InputBindingInfo info)
  RVA=0x037D0190  token=0x600015D  System.Void _AddPlayerActionCheck(Beyond.Input.GamepadInput input, Beyond.Input.InputBindingInfo info)
  RVA=0x037D0830  token=0x600015E  System.Void _RemovePlayerActionCheck(Beyond.Input.KeyboardInput input, Beyond.Input.InputBindingInfo info)
  RVA=0x037CE900  token=0x600015F  System.Void _RemovePlayerActionCheck(Beyond.Input.GamepadInput input, Beyond.Input.InputBindingInfo info)
  RVA=0x04774970  token=0x6000160  System.Boolean GetKey(Beyond.Input.KeyboardKeyCode keyCode)
  RVA=0x09E2A080  token=0x6000161  System.Boolean GetKeyOrDown(Beyond.Input.KeyboardKeyCode keyCode)
  RVA=0x03305AC0  token=0x6000162  System.Boolean GetKeyDown(Beyond.Input.KeyboardKeyCode keyCode)
  RVA=0x03305B50  token=0x6000163  System.Boolean GetKeyUp(Beyond.Input.KeyboardKeyCode keyCode)
  RVA=0x09E29CF8  token=0x6000164  UnityEngine.Vector2 GetGamepadStickValue(System.Boolean isLeft, System.Boolean checkDeadZone)
  RVA=0x09E28160  token=0x6000165  System.Boolean CheckGamepadStickInDeadZone(UnityEngine.Vector2 stickValue)
  RVA=0x09E27BF8  token=0x6000166  System.Boolean CheckGamepadStickInDeadZone(System.Single stickAxisValue)
  RVA=0x09E29E14  token=0x6000167  System.Single GetGamepadTriggerValue(System.Boolean isLeft)
  RVA=0x09E2A0C4  token=0x6000168  System.Boolean GetKey(Beyond.Input.GamepadKeyCode keyCode)
  RVA=0x09E29F64  token=0x6000169  System.Boolean GetKeyDown(Beyond.Input.GamepadKeyCode keyCode)
  RVA=0x09E2A0B4  token=0x600016A  System.Boolean GetKeyUp(Beyond.Input.GamepadKeyCode keyCode)
  RVA=0x09E27608  token=0x600016B  System.Boolean AnyKeyboardKey(System.String[] actionScopes, Beyond.Input.KeyboardKeyCode& keyCode, System.Boolean& isBlackList)
  RVA=0x09E2D118  token=0x600016C  System.Boolean _AnyKeyboardKey(Beyond.Input.KeyboardKeyCode[] keyCodes, Beyond.Input.KeyboardKeyCode& keyCode)
  RVA=0x034DECC0  token=0x600016D  System.Boolean AnyGamepadKey()
  RVA=0x037CF620  token=0x600016E  System.Boolean AnyMouseKeyDown()
  RVA=0x09E27878  token=0x600016F  System.Boolean AnyMouseKey()
  RVA=0x09E277F8  token=0x6000170  System.Boolean AnyMouseKeyUp()
  RVA=0x037CF500  token=0x6000171  System.Boolean AnyKeyDown()
  RVA=0x09E278F8  token=0x6000172  System.Boolean AnyTouchBegan()
  RVA=0x02D45020  token=0x6000173  System.Boolean AnyTouchActive()
  RVA=0x03B72340  token=0x6000174  System.Boolean _CheckGamepadKeyCode(Beyond.Input.GamepadKeyCode keyCode, Beyond.Input.InputManager.KeyCodeState state)
  RVA=0x09E28284  token=0x6000175  System.Int32 CreateBinding(Beyond.Input.PlayerActionInfo actionInfo, System.Action callback, System.Int32 groupId, System.String& actionId)
  RVA=0x037D16F0  token=0x6000176  System.Int32 CreateBinding(System.String actionId, System.Action callback, System.Int32 groupId)
  RVA=0x09E2833C  token=0x6000177  System.Int32 CreateBinding(Beyond.Input.KeyboardKeyCode key, System.String modifyKeys, Beyond.Input.InputTimingType timing, System.Action callback, System.Int32 groupId)
  RVA=0x037CE450  token=0x6000178  System.Int32 CreateBindingByActionId(System.String actionId, System.Action callback, System.Int32 groupId)
  RVA=0x039D03E0  token=0x6000179  System.Void DeleteBinding(System.Int32 id)
  RVA=0x037D0EF0  token=0x600017A  System.Void ToggleBinding(System.Int32 id, System.Boolean active)
  RVA=0x037CFA30  token=0x600017B  System.Void CancelPendingLongPress(Beyond.Input.InputBindingInfo info)
  RVA=0x03793000  token=0x600017C  System.Int32 CreateGroup(System.Int32 parentGroupId, Beyond.Input.IBindingGroupTarget bindingGroupTarget)
  RVA=0x0351DCF0  token=0x600017D  System.Void DeleteGroup(System.Int32 id)
  RVA=0x0453B150  token=0x600017E  System.Void DeleteInGroup(System.Int32 groupId)
  RVA=0x03793B30  token=0x600017F  System.Void ToggleGroup(System.Int32 id, System.Boolean active)
  RVA=0x037CDD30  token=0x6000180  System.Boolean IsBindingEnabled(System.Int32 id, System.Boolean needLogError)
  RVA=0x02FAA7F0  token=0x6000181  System.Boolean IsGroupParentDisabled(System.Int32 groupId)
  RVA=0x09E29210  token=0x6000182  System.Void ForceBindingKeyhintToGray(System.Int32 id, System.Boolean grayState)
  RVA=0x09E2A810  token=0x6000183  System.Boolean IsBindingKeyhintGray(System.Int32 id)
  RVA=0x03794C00  token=0x6000184  System.Boolean IsGroupEnabled(System.Int32 id)
  RVA=0x09E2CD58  token=0x6000185  System.Void ToggleDebugCursorMode()
  RVA=0x03C11A30  token=0x6000186  System.Void ClearAllCursorShowRequests()
  RVA=0x03A73590  token=0x6000187  System.Void SetCursorShowRequest(System.String key, System.Boolean showCursor)
  RVA=0x049D6C20  token=0x6000188  System.Void ToggleAllInput(System.Boolean active)
  RVA=0x09E2C68C  token=0x6000189  System.Void SetBindingView(System.Int32 bindingId, Beyond.Input.IBindingView bindingView)
  RVA=0x03EAD820  token=0x600018A  System.Void SetGroupView(System.Int32 groupId, Beyond.Input.IBindingView groupView)
  RVA=0x042B5A20  token=0x600018B  System.Void ChangeParent(System.Boolean isGroup, System.Int32 id, System.Int32 newParentGroupId)
  RVA=0x042B5940  token=0x600018C  System.Int32 GetGroupParentId(System.Boolean isGroup, System.Int32 id)
  RVA=0x09E2A5D8  token=0x600018D  System.Void IgnoreBindingGroupParent(System.Int32 groupId, System.Boolean parentIgnored)
  RVA=0x09E2A6A0  token=0x600018E  System.Void IgnoreBindingParent(System.Int32 bindingId, System.Boolean parentIgnored)
  RVA=0x035C6510  token=0x600018F  Beyond.Input.KeyboardKeyCode GetActionKeyboardKeyCode(System.String actionId, System.Boolean isModifyKey, System.Boolean isPrimary, Beyond.Input.InputSettingLevel level)
  RVA=0x043D1B10  token=0x6000190  Beyond.Input.GamepadKeyCode GetActionGamepadKeyCode(System.String actionId, System.Boolean isModifyKey, System.Boolean isPrimary, Beyond.Input.InputSettingLevel level)
  RVA=0x09E28A0C  token=0x6000191  System.Void DeleteCustomBinding(System.String actionId, Beyond.Input.InputDeviceFlags deviceFlags)
  RVA=0x03C1A700  token=0x6000192  System.Void DeleteAllCustomBindings(Beyond.Input.InputDeviceFlags deviceFlags)
  RVA=0x043EF1A0  token=0x6000193  System.Void SetBindingText(System.Int32 id, System.String text)
  RVA=0x09E29464  token=0x6000194  System.String GetBindingText(System.Int32 id)
  RVA=0x09E2A550  token=0x6000195  System.Boolean HasBinding(System.Int32 id)
  RVA=0x03CDE140  token=0x6000196  System.String GetActionText(System.String actionId)
  RVA=0x03CDE1B0  token=0x6000197  System.String GetActionText(Beyond.Input.PlayerActionInfo info, System.String actionId)
  RVA=0x09E27ADC  token=0x6000198  System.Boolean CheckActionKeyCodeConflict(System.String actionId, Beyond.Input.KeyboardKeyCode keyCode, System.Boolean& isPrimary)
  RVA=0x03792F20  token=0x6000199  System.Void _DeleteGroup(Beyond.Input.InputBindingGroup group, System.Boolean isRootCall)
  RVA=0x03793D30  token=0x600019A  System.Void _DeleteInGroup(Beyond.Input.InputBindingGroup group)
  RVA=0x037D0680  token=0x600019B  System.Void _RealRemoveBindingInfo(Beyond.Input.InputBindingInfo info)
  RVA=0x032FEEA0  token=0x600019C  System.Single GetAxis(System.String name)
  RVA=0x037D5240  token=0x600019D  System.Void GetBindingInfo(System.Int32 bindingId, System.String& actionId, Beyond.Input.IBindingView& bindingView)
  RVA=0x037D17D0  token=0x600019E  System.Void _LoadSetting()
  RVA=0x037D22D0  token=0x600019F  System.Void LoadSetting()
  RVA=0x037D2230  token=0x60001A0  System.Boolean IsPlayerActionIdValid(System.String id)
  RVA=0x037CE640  token=0x60001A1  Beyond.Input.PlayerActionInfo GetPlayerActionInfo(System.String id, Beyond.Input.InputSettingLevel level)
  RVA=0x09E27A74  token=0x60001A2  System.Void ChangePlayerAction(System.String id, Beyond.Input.PlayerActionInfo actionInfo)
  RVA=0x09E2748C  token=0x60001A3  System.String AddAnonymousPlayerAction(Beyond.Input.PlayerActionInfo actionInfo)
  RVA=0x04B0B910  token=0x60001A4  System.Void _LoadCustomInputSetting()
  RVA=0x09E2E744  token=0x60001A5  Beyond.Input.PlayerActionInfo _GetOrCreatePendingActionInfo(System.String actionId)
  RVA=0x09E287DC  token=0x60001A6  System.Boolean CustomizeActionKey(System.String actionId, Beyond.Input.KeyboardKeyCode key, System.Boolean isPrimary, System.String overrideKeyIconName)
  RVA=0x09E284F4  token=0x60001A7  System.Boolean CustomizeActionKey(System.String actionId, System.Nullable<Beyond.Input.GamepadKeyCode> modifyKey, System.Nullable<Beyond.Input.GamepadKeyCode> key, System.Boolean isPrimary, System.String overrideKeyIconName)
  RVA=0x09E28208  token=0x60001A8  System.Void ClearPendingInputSetting()
  RVA=0x03C194D0  token=0x60001A9  System.Void SaveCustomInputSetting()
  RVA=0x0479F7E0  token=0x60001B4  System.Void _CreateCanvas()
  RVA=0x0479FA80  token=0x60001B5  System.Void _CreateDebugVirtualMouse()
  RVA=0x0479FB20  token=0x60001B6  System.Void _CreateVirtualMouse()
  RVA=0x03A736A0  token=0x60001B7  System.Void _ToggleVirtualMouse(System.Boolean active, System.Boolean forceUpdate)
  RVA=0x03A73630  token=0x60001B8  System.Void _CheckVirtualMouseState()
  RVA=0x03401A40  token=0x60001BA  UnityEngine.Vector3 GetMousePos()
  RVA=0x09E2CC48  token=0x60001BB  System.Void SetOnlyVirtualMousePos(UnityEngine.Vector2 newPos)
  RVA=0x09E2CA5C  token=0x60001BC  System.Void SetMousePos(UnityEngine.Vector2 newPos, System.Boolean hideVirtualMouse)
  RVA=0x033FE1F0  token=0x60001BD  System.Boolean GetMouseButton(System.Int32 button)
  RVA=0x02D454E0  token=0x60001BE  System.Boolean IsLeftMouseDown(System.Boolean requireActive)
  RVA=0x03E50260  token=0x60001BF  System.Boolean GetMouseButtonDown(System.Int32 button)
  RVA=0x03E2F900  token=0x60001C0  System.Boolean GetMouseButtonUp(System.Int32 button)
  RVA=0x09E2C9CC  token=0x60001C5  System.Void SetCustomControllerMouse(UnityEngine.Transform trans, UnityEngine.Camera uiCamera)
  RVA=0x09E29A94  token=0x60001C6  System.Collections.Generic.List<Beyond.Input.InputManager.ControllerHintInfo> GetEmptyControllerHintInfoList()
  RVA=0x09E29608  token=0x60001C7  System.Void GetControllerHintInfos(System.Int32 groupId, System.Boolean ignoreRootEnabled, System.Collections.Generic.List<Beyond.Input.InputManager.ControllerHintInfo>& infoList)
  RVA=0x09E29558  token=0x60001C8  System.Void GetControllerHintInfos(System.Collections.Generic.List<System.String> actionIdList, System.Collections.Generic.List<Beyond.Input.InputManager.ControllerHintInfo>& infoList)
  RVA=0x09E2DD40  token=0x60001C9  System.Void _GetControllerHintInfos(Beyond.Input.InputBindingGroup group, System.Boolean ignoreRootEnabled, System.Collections.Generic.List<Beyond.Input.InputManager.ControllerHintInfo>& infoList)
  RVA=0x09E2DB6C  token=0x60001CA  System.Void _GetControllerHintInfos(System.Collections.Generic.List<System.String> actionIdList, System.Collections.Generic.List<Beyond.Input.InputManager.ControllerHintInfo>& infoList)
  RVA=0x09E2A1BC  token=0x60001CB  System.Single GetPlayerActionLongPressStartTime(System.String actionId, System.Single& longPressTime)
  RVA=0x09E292C8  token=0x60001CC  System.Single GetActionBindingLongPressStartTime(System.Int32 bindingId, System.Single& longPressTime)
  RVA=0x09E27564  token=0x60001CD  System.Void AddGuideUseActionId(System.String actionId)
  RVA=0x09E2C050  token=0x60001CE  System.Void RemoveGuideUseActionId(System.String actionId)
  RVA=0x09E2818C  token=0x60001CF  System.Void ClearGuideUseActionIds()
  RVA=0x033FDC80  token=0x60001D0  System.Boolean IsGuideUseAction(System.String actionId)
  RVA=0x09E2A764  token=0x60001D1  System.Boolean IsAnyBindingInGroupGuideUse(System.Int32 groupId)
  RVA=0x09E2E9D0  token=0x60001D2  System.Boolean _IsAnyBindingInGroupGuideUse(Beyond.Input.InputBindingGroup group)
  RVA=0x09E2CF94  token=0x60001D3  System.Void VibrateController(System.Single time, System.Single level)
  RVA=0x09E2E4BC  token=0x60001D4  System.String _GetGamepadKeyIconFolderPath()
  RVA=0x036D48E0  token=0x60001D5  System.String GetStringByKeyboardKeyCode(Beyond.Input.KeyboardKeyCode code)
  RVA=0x09E2A368  token=0x60001D6  System.String GetStringByGamepadKeyCode(Beyond.Input.GamepadKeyCode code)
  RVA=0x09E2A0D0  token=0x60001D7  System.String GetKeyboardIconPath(System.String keyName, System.Boolean isLongPress, System.Boolean isFullPath)
  RVA=0x09E29F74  token=0x60001D8  System.String GetKeyIconPath(Beyond.Input.GamepadInput inputInfo, System.Boolean isModifyKey, System.Boolean isFullPath, System.Boolean ignoreOverrideKeyIcon)
  RVA=0x09E29C48  token=0x60001D9  System.String GetGamepadKeyIconPath(System.String keyStr, System.Boolean isLongPress, System.Boolean isFullPath)
  RVA=0x036D41E0  token=0x60001DA  System.String GetKeyIconPath(Beyond.Input.KeyboardInput inputInfo, System.Boolean isModifyKey, System.Boolean isFullPath)
  RVA=0x036D4280  token=0x60001DB  System.String GetKeyIconPath(Beyond.Input.KeyboardInput inputInfo, System.Boolean isModifyKey, System.Boolean isLongPress, System.Boolean isFullPath)
  RVA=0x036D4410  token=0x60001DC  System.String _GetKeyIconPath(System.String path, System.String keyStr, System.Boolean isLongPress, System.Boolean isFullPath)
  RVA=0x0489C040  token=0x60001DD  System.String ParseTextActionId(System.String text)
  RVA=0x09E2A8B8  token=0x60001DE  System.Void MoveMouseTo(UnityEngine.RectTransform target, UnityEngine.Camera uiCamera)
  RVA=0x09E2AB24  token=0x60001DF  System.Void MoveVirtualMouseTo(UnityEngine.RectTransform target, UnityEngine.Camera uiCamera, System.Boolean hideVirtualMouse)
  RVA=0x09E2CCE4  token=0x60001E0  System.Void SetVirtualMouseIconVisible(System.Boolean active)
  RVA=0x09E2D790  token=0x60001E3  System.Void _ControllerTouchPanelTick()
  RVA=0x03798D80  token=0x60001E4  System.Int32 GetTouchId(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x042EE640  token=0x60001E5  System.Boolean GetPressPosByTouchId(System.Int32 touchId, UnityEngine.Vector2& position)
  RVA=0x09E29168  token=0x60001E6  System.Void Dispose()
  RVA=0x09E2C0F4  token=0x60001E7  System.Void RemoveUnusedInput()
  RVA=0x03A73460  token=0x60001E8  System.Void _OnNetMaskChanged(Beyond.EventData<System.Boolean>& v)
  RVA=0x09E296FC  token=0x60001E9  System.String GetCurJsInfos()
  RVA=0x09E2A30C  token=0x60001EA  System.String GetRealCursorState()
  RVA=0x09E2B134  token=0x60001EB  System.String PrintKeycodeInfo(System.String keyCode, System.Boolean checkKeyboard, System.Boolean checkGamepad, System.Boolean isDetailed)
  RVA=0x09E2BAD0  token=0x60001EC  System.String PrintKeycodeInfo(Beyond.Input.KeyboardKeyCode keyCode, System.Boolean isDetailed)
  RVA=0x09E2B5B8  token=0x60001ED  System.String PrintKeycodeInfo(Beyond.Input.GamepadKeyCode keyCode, System.Boolean isDetailed)
  RVA=0x09E2AC1C  token=0x60001EE  System.String PrintActionInfo(System.String actionId)
  RVA=0x09E2AE94  token=0x60001EF  System.String PrintBindingInfo(Beyond.Input.InputBindingInfo bindingInfo)
  RVA=0x09E2D944  token=0x60001F0  System.String _GetBindingName(System.Object item)
  RVA=0x09E2E660  token=0x60001F1  UnityEngine.GameObject _GetInputBindingGroupGameObject(Beyond.Input.InputBindingGroup bindingGroup)
  RVA=0x09E2E590  token=0x60001F2  UnityEngine.GameObject _GetInputBindingGameObject(Beyond.Input.InputBindingInfo bindingInfo)
  RVA=0x04490250  token=0x60001F3  Beyond.Input.InputManager.DelayedComboBinding RegisterDelayedCombo(System.String modifierActionId, System.String triggerActionId, Beyond.Input.InputManager.DelayedComboKind kind, System.Single maxModifierHoldTime, System.Action onTriggered)
  RVA=0x09E2CEFC  token=0x60001F4  System.Void UnregisterDelayedCombo(Beyond.Input.InputManager.DelayedComboBinding binding)
  RVA=0x09E2EF48  token=0x60001F5  System.Void _OnDelayedComboActionKeyUpdated(Beyond.EventData<System.String>& data)
  RVA=0x044904D0  token=0x60001F6  System.Void _RefreshDelayedComboKeys(Beyond.Input.InputManager.DelayedComboBinding binding)
  RVA=0x036E8640  token=0x60001F7  System.Void _UpdateDelayedCombos(System.Single deltaTime)
  RVA=0x036E9110  token=0x60001F8  System.Void _UpdateModifierTapThenTriggerCombo(Beyond.Input.InputManager.DelayedComboBinding binding, System.Single deltaTime)
  RVA=0x036E91E0  token=0x60001F9  System.Void _UpdateTriggerThenModifierCombo(Beyond.Input.InputManager.DelayedComboBinding binding)
  RVA=0x0432BE50  token=0x60001FA  System.Void .cctor()
  RVA=0x09E2CEBC  token=0x60001FB  System.Void <_Init>b__68_0(Beyond.Input.KeyboardKeyCode key)
  RVA=0x09E2CEDC  token=0x60001FC  System.Void <_Init>b__68_1(Beyond.Input.GamepadKeyCode key)
  RVA=0x03A73530  token=0x60001FD  System.Void <_Init>b__68_2()
  RVA=0x03A734E0  token=0x60001FE  System.Void <_Init>b__68_3()
END_CLASS

CLASS: Beyond.Input.InputRebindConfig
TYPE:  class
TOKEN: 0x200002E
SIZE:  0x20
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
  public    static  System.String                   INPUT_REBIND_CONFIG_PATH  // const
  public            Beyond.Input.InputRebindActionScopeConfig[]actionScopeConfigs  // 0x18
METHODS:
  RVA=0x03573370  token=0x6000211  System.Void .ctor()
END_CLASS

CLASS: Beyond.Input.InputRebindActionScopeConfig
TYPE:  class
TOKEN: 0x200002F
SIZE:  0x28
FIELDS:
  public            System.String                   name  // 0x10
  public            Beyond.Input.KeyboardKeyCode[]  keyboardKeyCodeWhiteList  // 0x18
  public            Beyond.Input.KeyboardKeyCode[]  keyboardKeyCodeBlackList  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000212  System.Void .ctor()
END_CLASS

CLASS: Beyond.Input.InputSetting
TYPE:  class
TOKEN: 0x2000030
SIZE:  0x20
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
  public    static  System.String                   CUSTOM_SETTING_DIRECTORY_NAME  // const
  public    static  System.String                   CUSTOM_SETTING_JSON_FILE_NAME  // const
  public            Beyond.SerializeFieldDictionaryPaired<System.String,Beyond.Input.PlayerActionInfo>actionInfos  // 0x18
METHODS:
  RVA=0x0454B380  token=0x6000213  System.Void .ctor()
END_CLASS

CLASS: Beyond.Input.InputSettingConfig
TYPE:  class
TOKEN: 0x2000031
SIZE:  0x20
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
  public    static  System.String                   INPUT_SETTING_CONFIG_PATH  // const
  public            System.Collections.Generic.List<Beyond.Input.InputSetting>inputSettingList  // 0x18
METHODS:
  RVA=0x04D22DA0  token=0x6000214  System.Void .ctor()
END_CLASS

CLASS: Beyond.Input.InputUtils
TYPE:  static class
TOKEN: 0x2000032
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x09E3A6F0  token=0x6000215  System.Void IgnoreBindingParent(Beyond.Input.IBindingTarget target, System.Boolean active)
  RVA=0x09E3A644  token=0x6000216  System.Void IgnoreBindingGroupParent(Beyond.Input.IBindingGroupTarget target, System.Boolean active)
  RVA=0x09E3A5E8  token=0x6000217  System.Boolean HasFlag(Beyond.Input.InputDeviceFlags value, Beyond.Input.InputDeviceFlags target)
END_CLASS

CLASS: Beyond.Input.KeyboardCheckerBase
TYPE:  abstract class
TOKEN: 0x2000033
SIZE:  0x20
FIELDS:
  protected         Beyond.Input.InputManager       m_inputManager  // 0x10
  protected static  System.Int32                    CTRL  // const
  protected static  System.Int32                    SHIFT  // const
  protected static  System.Int32                    ALT  // const
  protected static  System.Int32                    MAX_INDEX  // const
  protected static  System.Int32[]                  s_checkPriority  // static @ 0x0
  protected         System.Collections.Generic.List<Beyond.DynamicFastLookupCollection<Beyond.Input.KeyboardKeyCode,System.Collections.Generic.HashSet<Beyond.Input.KeyboardCheckerBase.Binding>>>m_bindingInfos  // 0x18
PROPERTIES:
  bindingInfos  get=0x09E3A824
METHODS:
  RVA=0x042B5820  token=0x6000219  System.Void .ctor(Beyond.Input.InputManager inputManager)
  RVA=0x037D0CD0  token=0x600021A  System.Int32 GetModifyIndex(Beyond.Input.KeyboardInput input)
  RVA=0x037D0B50  token=0x600021B  Beyond.Input.KeyboardCheckerBase.Binding OnCreateBinding(Beyond.Input.KeyboardInput input, Beyond.Input.InputBindingInfo info, System.Boolean isUsedForBlock)
  RVA=0x037D08E0  token=0x600021C  Beyond.Input.KeyboardCheckerBase.Binding OnDeleteBinding(Beyond.Input.KeyboardInput input, Beyond.Input.InputBindingInfo info)
  RVA=0x09E3A7E0  token=0x600021D  System.Void CheckKeyboardInput()
  RVA=0x04066A20  token=0x600021E  System.Void TriggerActions()
  RVA=0x04D17B50  token=0x600021F  System.Void .cctor()
END_CLASS

CLASS: Beyond.Input.KeyboardChecker
TYPE:  class
TOKEN: 0x2000036
SIZE:  0x40
EXTENDS: Beyond.Input.KeyboardCheckerBase
FIELDS:
  private           System.Func<Beyond.Input.KeyboardKeyCode,System.Boolean>m_checkFunc  // 0x20
  private           System.Boolean                  <isPressChecker>k__BackingField  // 0x28
  private           System.Collections.Generic.HashSet<Beyond.Input.KeyboardKeyCode>m_usedKeyCache  // 0x30
  private           System.Collections.Generic.List<System.ValueTuple<Beyond.Input.KeyboardCheckerBase.Binding,Beyond.Input.KeyboardCheckerBase.TriggerType>>m_tmpNeedTriggerCallbacks  // 0x38
PROPERTIES:
  isPressChecker  get=0x011F33C0  set=0x011F33D0
METHODS:
  RVA=0x042B56C0  token=0x6000223  System.Void .ctor(System.Func<Beyond.Input.KeyboardKeyCode,System.Boolean> checkFunc, System.Boolean isPress, Beyond.Input.InputManager inputManager)
  RVA=0x03307E80  token=0x6000224  System.Void CheckKeyboardInput()
  RVA=0x03629670  token=0x6000225  System.Void TriggerActions()
  RVA=0x09E3A874  token=0x6000226  System.Void <>iFixBaseProxy_CheckKeyboardInput()
  RVA=0x09E3A87C  token=0x6000227  System.Void <>iFixBaseProxy_TriggerActions()
END_CLASS

CLASS: Beyond.Input.KeyboardOnClickChecker
TYPE:  class
TOKEN: 0x2000037
SIZE:  0x38
EXTENDS: Beyond.Input.KeyboardCheckerBase
FIELDS:
  private           System.Collections.Generic.HashSet<Beyond.Input.KeyboardKeyCode>m_usedKeyCache  // 0x20
  private           System.Collections.Generic.List<System.ValueTuple<Beyond.Input.KeyboardCheckerBase.Binding,Beyond.Input.KeyboardCheckerBase.TriggerType>>m_tmpNeedTriggerCallbacks  // 0x28
  private           System.Collections.Generic.List<Beyond.ListSet<Beyond.Input.KeyboardKeyCode>>m_combinationKeyPressedInfo  // 0x30
METHODS:
  RVA=0x042B5DB0  token=0x6000228  System.Void .ctor(Beyond.Input.InputManager inputManager)
  RVA=0x03306AA0  token=0x6000229  System.Void CheckKeyboardInput()
  RVA=0x03629260  token=0x600022A  System.Void TriggerActions()
  RVA=0x09E3A884  token=0x600022B  System.Void ClearKeyPressed(Beyond.Input.KeyboardKeyCode keyCode)
  RVA=0x09E3A874  token=0x600022C  System.Void <>iFixBaseProxy_CheckKeyboardInput()
  RVA=0x09E3A87C  token=0x600022D  System.Void <>iFixBaseProxy_TriggerActions()
END_CLASS

CLASS: Beyond.Input.KeyboardOnLongPressChecker
TYPE:  class
TOKEN: 0x2000038
SIZE:  0x38
EXTENDS: Beyond.Input.KeyboardCheckerBase
FIELDS:
  private           System.Action<Beyond.Input.KeyboardKeyCode>m_onTriggerOnLongPress  // 0x20
  private           System.Collections.Generic.Dictionary<Beyond.Input.KeyboardInput,System.Collections.Generic.HashSet<Beyond.Input.KeyboardCheckerBase.Binding>>m_bindings  // 0x28
  private           System.Collections.Generic.HashSet<Beyond.Input.KeyboardKeyCode>m_usedKeyCache  // 0x30
METHODS:
  RVA=0x042B5FB0  token=0x600022E  System.Void .ctor(System.Action<Beyond.Input.KeyboardKeyCode> onTriggerOnLongPress, Beyond.Input.InputManager inputManager)
  RVA=0x03304F20  token=0x600022F  System.Void CheckKeyboardInput()
  RVA=0x09E40240  token=0x6000230  System.Void _OnTriggerBinding(Beyond.Input.KeyboardCheckerBase.Binding binding, Beyond.Input.KeyboardKeyCode keyCode)
  RVA=0x09E3FF90  token=0x6000231  Beyond.Input.KeyboardCheckerBase.Binding OnCreateBinding(Beyond.Input.KeyboardInput input, Beyond.Input.InputBindingInfo info, System.Boolean isUsedForBlock)
  RVA=0x09E40108  token=0x6000232  Beyond.Input.KeyboardCheckerBase.Binding OnDeleteBinding(Beyond.Input.KeyboardInput input, Beyond.Input.InputBindingInfo info)
  RVA=0x037CFA90  token=0x6000233  System.Void CancelPendingLongPress(Beyond.Input.InputBindingInfo info)
  RVA=0x09E3FD80  token=0x6000234  System.Single GetInputLongPressStartTime(Beyond.Input.KeyboardInput input, System.Int32 bindingId)
  RVA=0x09E3A874  token=0x6000235  System.Void <>iFixBaseProxy_CheckKeyboardInput()
  RVA=0x09E40228  token=0x6000236  Beyond.Input.KeyboardCheckerBase.Binding <>iFixBaseProxy_OnCreateBinding(Beyond.Input.KeyboardInput P0, Beyond.Input.InputBindingInfo P1, System.Boolean P2)
  RVA=0x09E40238  token=0x6000237  Beyond.Input.KeyboardCheckerBase.Binding <>iFixBaseProxy_OnDeleteBinding(Beyond.Input.KeyboardInput P0, Beyond.Input.InputBindingInfo P1)
END_CLASS

CLASS: Beyond.Input.KeyboardHandler
TYPE:  class
TOKEN: 0x200003A
SIZE:  0x30
FIELDS:
  private   static  System.Collections.Generic.Dictionary<Beyond.Input.KeyboardKeyCode,UnityEngine.InputSystem.Key>s_keyMap  // static @ 0x0
  private           System.Collections.Generic.Dictionary<Beyond.Input.KeyboardKeyCode,System.String>m_localizedKeyName  // 0x10
  private           System.Collections.Generic.Dictionary<System.String,System.String>m_localizedIconName  // 0x18
  private           System.Collections.Generic.Dictionary<Beyond.Input.KeyboardType,Beyond.Input.KeyboardOverrideConfig>m_configs  // 0x20
  private           Beyond.Input.KeyboardType       m_keyboardType  // 0x28
METHODS:
  RVA=0x044B64B0  token=0x600023A  System.Void .cctor()
  RVA=0x09E3F604  token=0x600023B  System.Boolean GetKey(Beyond.Input.KeyboardKeyCode keyCode)
  RVA=0x09E3F474  token=0x600023C  System.Boolean GetKeyDown(Beyond.Input.KeyboardKeyCode keyCode)
  RVA=0x09E3F53C  token=0x600023D  System.Boolean GetKeyUp(Beyond.Input.KeyboardKeyCode keyCode)
  RVA=0x047310D0  token=0x600023E  System.Void ParseConfig(Beyond.Input.KeyboardLocalizeConfig keyboardConfig)
  RVA=0x09E3F6CC  token=0x600023F  Beyond.Input.KeyboardType GetKeyboardType()
  RVA=0x047311B0  token=0x6000240  System.Void SetKeyboardType(Beyond.Input.KeyboardType keyboardType)
  RVA=0x09E3F314  token=0x6000241  System.String GetEscIconName()
  RVA=0x09E3F350  token=0x6000242  System.String GetIconNameByKeyName(System.String keyName)
  RVA=0x09E3F3D4  token=0x6000243  System.String GetIconName(Beyond.Input.KeyboardKeyCode keyCode)
  RVA=0x0432BD40  token=0x6000244  System.Void .ctor()
END_CLASS

CLASS: Beyond.Input.KeyboardLocalizeConfig
TYPE:  class
TOKEN: 0x200003B
SIZE:  0x20
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
  public    static  System.String                   KEYBOARD_CONFIG_PATH  // const
  public            Beyond.Input.KeyboardOverrideConfig[]keyboardConfigs  // 0x18
METHODS:
  RVA=0x03573370  token=0x6000245  System.Void .ctor()
END_CLASS

CLASS: Beyond.Input.KeyboardOverrideConfig
TYPE:  class
TOKEN: 0x200003C
SIZE:  0x20
FIELDS:
  public            Beyond.Input.KeyboardType       keyboardType  // 0x10
  public            Beyond.Input.KeyboardOverrideIconConfig[]overrideIconConfigs  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000246  System.Void .ctor()
END_CLASS

CLASS: Beyond.Input.KeyboardOverrideIconConfig
TYPE:  class
TOKEN: 0x200003D
SIZE:  0x28
FIELDS:
  public            Beyond.Input.KeyboardKeyCode    keyCode  // 0x10
  public            System.String                   keyName  // 0x18
  public            System.String                   iconName  // 0x20
METHODS:
  RVA=0x041E1670  token=0x6000247  System.Void .ctor()
END_CLASS

CLASS: Beyond.Input.PlayerActionIdAttribute
TYPE:  class
TOKEN: 0x200003E
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000248  System.Void .ctor()
END_CLASS

CLASS: Beyond.Input.InputDeviceFlags
TYPE:  sealed struct
TOKEN: 0x200003F
SIZE:  0x11
FIELDS:
  public            System.Byte                     value__  // 0x10
  public    static  Beyond.Input.InputDeviceFlags   None  // const
  public    static  Beyond.Input.InputDeviceFlags   Keyboard  // const
  public    static  Beyond.Input.InputDeviceFlags   Gamepad  // const
  public    static  Beyond.Input.InputDeviceFlags   All  // const
METHODS:
END_CLASS

CLASS: Beyond.Input.PlayerActionInfo
TYPE:  class
TOKEN: 0x2000040
SIZE:  0x58
FIELDS:
  public            System.Boolean                  isCommonOpenUIAction  // 0x10
  public            Beyond.Input.KeyboardInput      primaryKeyboardInput  // 0x18
  public            Beyond.Input.GamepadInput       primaryGamepadInput  // 0x20
  public            System.Int32                    priority  // 0x28
  public            System.Boolean                  needShowControllerHint  // 0x2c
  public            System.Int32                    controllerHintPriority  // 0x30
  public            System.String                   overrideTextId  // 0x38
  public            System.Boolean                  needSecond  // 0x40
  public            System.Boolean                  hideSecondInKeyHint  // 0x41
  public            Beyond.Input.KeyboardInput      secondaryKeyboardInput  // 0x48
  public            Beyond.Input.GamepadInput       secondaryGamepadInput  // 0x50
METHODS:
  RVA=0x09E40644  token=0x6000249  System.Void ResetHintProority()
  RVA=0x09E4053C  token=0x600024A  System.Boolean DeviceInputEquals(Beyond.Input.PlayerActionInfo other, Beyond.Input.InputDeviceFlags deviceFlags)
  RVA=0x09E40460  token=0x600024B  System.Void DeviceInputCopyFrom(Beyond.Input.PlayerActionInfo other, Beyond.Input.InputDeviceFlags deviceFlags)
  RVA=0x015EFCD0  token=0x600024C  System.Void .ctor()
END_CLASS

CLASS: Beyond.Input.DeviceInput`1
TYPE:  abstract class
TOKEN: 0x2000041
IMPLEMENTS: System.IEquatable`1
FIELDS:
  public            Beyond.Input.InputTimingType    timingType  // 0x0
  public            System.Boolean                  blockOtherTiming  // 0x0
  public            System.Single                   longPressTime  // 0x0
  public            System.Boolean                  enableContinuousTrigger  // 0x0
  public            Beyond.Input.InputContinuousTriggerTypecontinuousTriggerType  // 0x0
  public            System.String                   overrideKeyIconName  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600024D  System.String GetModifyText()
  RVA=-1  // not resolved  token=0x600024E  System.String GetKeyCodeText()
  RVA=-1  // not resolved  token=0x600024F  System.Int32 GetKeyCodeEnumIntValue()
  RVA=-1  // not resolved  token=0x6000250  System.Void SetKeyCodeEnumByIntValue(System.Int32 value)
  RVA=-1  // not resolved  token=0x6000251  System.Void ClearKeyCode()
  RVA=-1  // not resolved  token=0x6000252  System.Boolean IsValid()
  RVA=-1  // not resolved  token=0x6000253  System.Void CopyFrom(T other)
  RVA=-1  // abstract  token=0x6000254  System.Boolean Equals(T other)
  RVA=-1  // not resolved  token=0x6000255  System.String ToString()
  RVA=-1  // not resolved  token=0x6000256  System.Void .ctor()
END_CLASS

CLASS: Beyond.Input.KeyboardInput
TYPE:  class
TOKEN: 0x2000042
SIZE:  0x38
EXTENDS: Beyond.Input.DeviceInput`1
FIELDS:
  public            Beyond.Input.KeyboardKeyCode    key  // 0x30
  public            System.Boolean                  useCtrl  // 0x34
  public            System.Boolean                  useShift  // 0x35
  public            System.Boolean                  useAlt  // 0x36
PROPERTIES:
  modifyString  get=0x09E3FCA4
METHODS:
  RVA=0x09E3FB18  token=0x6000257  System.String GetModifyText()
  RVA=0x09E3F8C8  token=0x6000258  System.String GetKeyCodeText()
  RVA=0x09E3F87C  token=0x6000259  System.Int32 GetKeyCodeEnumIntValue()
  RVA=0x09E3FC1C  token=0x600025A  System.Void SetKeyCodeEnumByIntValue(System.Int32 value)
  RVA=0x09E3F718  token=0x600025C  System.Void ClearKeyCode()
  RVA=0x04CC5BB0  token=0x600025D  System.Boolean IsValid()
  RVA=0x09E3F774  token=0x600025E  System.Void CopyFrom(Beyond.Input.KeyboardInput other)
  RVA=0x09E3F814  token=0x600025F  System.Boolean Equals(Beyond.Input.KeyboardInput other)
  RVA=0x04DA0C20  token=0x6000260  System.Void .ctor()
  RVA=0x09E3FC8C  token=0x6000261  System.String <>iFixBaseProxy_GetModifyText()
  RVA=0x09E3FC84  token=0x6000262  System.String <>iFixBaseProxy_GetKeyCodeText()
  RVA=0x09E3FC7C  token=0x6000263  System.Int32 <>iFixBaseProxy_GetKeyCodeEnumIntValue()
  RVA=0x09E3FC9C  token=0x6000264  System.Void <>iFixBaseProxy_SetKeyCodeEnumByIntValue(System.Int32 P0)
  RVA=0x09E3FC74  token=0x6000265  System.Void <>iFixBaseProxy_ClearKeyCode()
  RVA=0x09E3FC94  token=0x6000266  System.Boolean <>iFixBaseProxy_IsValid()
END_CLASS

CLASS: Beyond.Input.GamepadInput
TYPE:  class
TOKEN: 0x2000043
SIZE:  0x38
EXTENDS: Beyond.Input.DeviceInput`1
FIELDS:
  public            Beyond.Input.GamepadKeyCode     key  // 0x30
  public            Beyond.Input.GamepadKeyCode     modifyKey  // 0x34
METHODS:
  RVA=0x09E3AEEC  token=0x6000267  System.String GetModifyText()
  RVA=0x09E3AE64  token=0x6000268  System.String GetKeyCodeText()
  RVA=0x09E3AE18  token=0x6000269  System.Int32 GetKeyCodeEnumIntValue()
  RVA=0x09E3AFE4  token=0x600026A  System.Void SetKeyCodeEnumByIntValue(System.Int32 value)
  RVA=0x09E3AD24  token=0x600026B  System.Void ClearKeyCode()
  RVA=0x09E3AF94  token=0x600026C  System.Boolean IsValid()
  RVA=0x09E3AD84  token=0x600026D  System.Void CopyFrom(Beyond.Input.GamepadInput other)
  RVA=0x04CB2580  token=0x600026E  System.Boolean Equals(Beyond.Input.GamepadInput other)
  RVA=0x04DA0C20  token=0x600026F  System.Void .ctor()
  RVA=0x09E3B054  token=0x6000270  System.String <>iFixBaseProxy_GetModifyText()
  RVA=0x09E3B04C  token=0x6000271  System.String <>iFixBaseProxy_GetKeyCodeText()
  RVA=0x09E3B044  token=0x6000272  System.Int32 <>iFixBaseProxy_GetKeyCodeEnumIntValue()
  RVA=0x09E3B064  token=0x6000273  System.Void <>iFixBaseProxy_SetKeyCodeEnumByIntValue(System.Int32 P0)
  RVA=0x09E3B03C  token=0x6000274  System.Void <>iFixBaseProxy_ClearKeyCode()
  RVA=0x09E3B05C  token=0x6000275  System.Boolean <>iFixBaseProxy_IsValid()
END_CLASS

CLASS: Beyond.Input.CursorRequest
TYPE:  sealed struct
TOKEN: 0x2000044
SIZE:  0x20
FIELDS:
  public            System.String                   key  // 0x10
  public            System.Int32                    priority  // 0x18
  public            System.Boolean                  show  // 0x1c
METHODS:
  RVA=0x09E3AC40  token=0x6000276  System.String ToString()
  RVA=0x09E3ACD4  token=0x6000277  System.String <>iFixBaseProxy_ToString()
END_CLASS

CLASS: Beyond.Input.CursorPriority
TYPE:  static class
TOKEN: 0x2000045
SIZE:  0x10
FIELDS:
  public    static  System.Int32                    DEVICE_CHANGE_OVERRIDE  // const
  public    static  System.Int32                    CONTROLLER_HIDE  // const
  public    static  System.Int32                    EDITOR_DEBUG_FORCE_SHOW  // const
  public    static  System.Int32                    SHOW_REQUEST  // const
METHODS:
END_CLASS

CLASS: Beyond.Input.RealCursorManager
TYPE:  class
TOKEN: 0x2000046
SIZE:  0x30
FIELDS:
  private           System.Collections.Generic.Dictionary<System.String,Beyond.Input.CursorRequest>m_requests  // 0x10
  private           Beyond.Input.CursorRequest      m_activeHighest  // 0x18
  private           System.Boolean                  m_isUsingVirtualMouse  // 0x28
PROPERTIES:
  isDebugForceShow  get=0x09E40B90
METHODS:
  RVA=0x042B64C0  token=0x6000279  System.Void .ctor()
  RVA=0x09E40A10  token=0x600027A  System.Boolean HasAnyShowRequestAtPriority(System.Int32 priority)
  RVA=0x03A73820  token=0x600027B  System.Void SetRequest(System.String key, System.Int32 priority, System.Boolean show)
  RVA=0x03A73C70  token=0x600027C  System.Void RemoveRequest(System.String key)
  RVA=0x0485D800  token=0x600027D  System.Void RemoveAllRequestsAtPriority(System.Int32 priority)
  RVA=0x03DF9C70  token=0x600027E  System.Void SetVirtualMouseActive(System.Boolean active)
  RVA=0x03A738E0  token=0x600027F  System.Void CalcState(System.Boolean forceUpdate)
  RVA=0x03A73A90  token=0x6000280  System.Void _RefreshActiveHighest()
  RVA=0x03A73950  token=0x6000281  System.Void _ToggleCursorInternal(System.Boolean enable, System.Boolean forceUpdate)
  RVA=0x09E40730  token=0x6000282  System.String DumpState()
END_CLASS

CLASS: Beyond.Input.TextIdAttribute
TYPE:  class
TOKEN: 0x2000047
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000283  System.Void .ctor()
END_CLASS

CLASS: Beyond.Input.UIEvent
TYPE:  class
TOKEN: 0x2000048
SIZE:  0x78
EXTENDS: UnityEngine.Events.UnityEvent
IMPLEMENTS: Beyond.Input.IBindingTarget
FIELDS:
  private           System.Boolean                  m_bindingInit  // 0x30
  public            System.Boolean                  useBindingAction  // 0x31
  public            System.String                   playerActionId  // 0x38
  public            Beyond.Input.PlayerActionInfo   anonymousPlayerActionInfo  // 0x40
  public            System.String                   anonymousPlayerActionId  // 0x48
  private           System.Int32                    <bindingId>k__BackingField  // 0x50
  private           Beyond.Input.IBindingGroupTarget<parentTarget>k__BackingField  // 0x58
  private           System.Action                   m_bindAct  // 0x60
  private           System.Action                   m_onUpdateKeyAct  // 0x68
  private           System.Collections.Generic.List<UnityEngine.Events.UnityAction>m_calls  // 0x70
PROPERTIES:
  bindingEnabled  get=0x09E41418
  bindingId  get=0x04D86550  set=0x04D865A0
  parentTarget  get=0x02E56440  set=0x036E5590
  count  get=0x09E414C0
METHODS:
  RVA=0x09E412D4  token=0x6000289  System.Void _CreatePlayerActionBinding()
  RVA=0x09E40E04  token=0x600028A  System.Void InitBinding(Beyond.Input.IBindingGroupTarget parent, System.Action bindAct, System.Action onUpdateKeyAct)
  RVA=0x09E40EDC  token=0x600028B  System.Void Invoke()
  RVA=0x09E41234  token=0x600028C  System.Void UpdateKeys()
  RVA=0x09E40D98  token=0x600028D  System.Void ChangeBindingPlayerAction(System.String actionId)
  RVA=0x09E40C48  token=0x600028F  System.Void AddListener(UnityEngine.Events.UnityAction call)
  RVA=0x09E41164  token=0x6000290  System.Void RemoveListener(UnityEngine.Events.UnityAction call)
  RVA=0x09E40FAC  token=0x6000291  System.Void RemoveAllListeners()
  RVA=0x09E41408  token=0x6000292  System.Void .ctor()
END_CLASS

CLASS: Beyond.Input.UIEvent`1
TYPE:  class
TOKEN: 0x2000049
EXTENDS: UnityEngine.Events.UnityEvent`1
IMPLEMENTS: Beyond.Input.IBindingTarget
FIELDS:
  private           System.Boolean                  m_bindingInit  // 0x0
  public            System.Boolean                  useBindingAction  // 0x0
  public            System.String                   playerActionId  // 0x0
  public            Beyond.Input.PlayerActionInfo   anonymousPlayerActionInfo  // 0x0
  public            System.String                   anonymousPlayerActionId  // 0x0
  public            System.Boolean                  useAnonymousAction  // 0x0
  public            System.Boolean                  tempForceEnable  // 0x0
  private           System.Int32                    <bindingId>k__BackingField  // 0x0
  private           Beyond.Input.IBindingGroupTarget<parentTarget>k__BackingField  // 0x0
  private           System.Action                   m_bindAct  // 0x0
  private           System.Action                   m_onUpdateKeyAct  // 0x0
  private           System.Collections.Generic.List<UnityEngine.Events.UnityAction<T0>>m_calls  // 0x0
PROPERTIES:
  bindingEnabled  get=-1  // not resolved
  bindingId  get=-1  // not resolved  set=-1  // not resolved
  parentTarget  get=-1  // not resolved  set=-1  // not resolved
  count  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000298  System.Void _CreatePlayerActionBinding()
  RVA=-1  // not resolved  token=0x6000299  System.Void InitBinding(Beyond.Input.IBindingGroupTarget parent, System.Action bindAct, System.Action onUpdateKeyAct)
  RVA=-1  // not resolved  token=0x600029A  System.Void Invoke(T0 arg0)
  RVA=-1  // not resolved  token=0x600029B  Beyond.Input.PlayerActionInfo GetEventPlayerActionInfo()
  RVA=-1  // not resolved  token=0x600029C  System.Void UpdateKeys()
  RVA=-1  // not resolved  token=0x600029D  System.Void ChangeBindingPlayerAction(System.String actionId)
  RVA=-1  // not resolved  token=0x600029E  System.Void StopUseBinding()
  RVA=-1  // not resolved  token=0x600029F  System.String GetActionId()
  RVA=-1  // not resolved  token=0x60002A1  System.Void AddListener(UnityEngine.Events.UnityAction<T0> call)
  RVA=-1  // not resolved  token=0x60002A2  System.Void RemoveListener(UnityEngine.Events.UnityAction<T0> call)
  RVA=-1  // not resolved  token=0x60002A3  System.Void RemoveAllListeners()
  RVA=-1  // not resolved  token=0x60002A4  System.Void .ctor()
END_CLASS

CLASS: Beyond.Input.UIEvent`2
TYPE:  class
TOKEN: 0x200004A
EXTENDS: UnityEngine.Events.UnityEvent`2
IMPLEMENTS: Beyond.Input.IBindingTarget
FIELDS:
  private           System.Boolean                  m_bindingInit  // 0x0
  public            System.Boolean                  useBindingAction  // 0x0
  public            System.String                   playerActionId  // 0x0
  public            Beyond.Input.PlayerActionInfo   anonymousPlayerActionInfo  // 0x0
  public            System.String                   anonymousPlayerActionId  // 0x0
  private           System.Int32                    <bindingId>k__BackingField  // 0x0
  private           Beyond.Input.IBindingGroupTarget<parentTarget>k__BackingField  // 0x0
  private           System.Action                   m_bindAct  // 0x0
  private           System.Action                   m_onUpdateKeyAct  // 0x0
  private           System.Collections.Generic.List<UnityEngine.Events.UnityAction<T0,T1>>m_calls  // 0x0
PROPERTIES:
  bindingEnabled  get=-1  // not resolved
  bindingId  get=-1  // not resolved  set=-1  // not resolved
  parentTarget  get=-1  // not resolved  set=-1  // not resolved
  count  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60002AA  System.Void _CreatePlayerActionBinding()
  RVA=-1  // not resolved  token=0x60002AB  System.Void InitBinding(Beyond.Input.IBindingGroupTarget parent, System.Action bindAct, System.Action onUpdateKeyAct)
  RVA=-1  // not resolved  token=0x60002AC  System.Void Invoke(T0 arg0, T1 arg1)
  RVA=-1  // not resolved  token=0x60002AD  System.Void UpdateKeys()
  RVA=-1  // not resolved  token=0x60002AE  System.Void ChangeBindingPlayerAction(System.String actionId)
  RVA=-1  // not resolved  token=0x60002B0  System.Void AddListener(UnityEngine.Events.UnityAction<T0,T1> call)
  RVA=-1  // not resolved  token=0x60002B1  System.Void RemoveListener(UnityEngine.Events.UnityAction<T0,T1> call)
  RVA=-1  // not resolved  token=0x60002B2  System.Void RemoveAllListeners()
  RVA=-1  // not resolved  token=0x60002B3  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.DataNAttribute
TYPE:  class
TOKEN: 0x2000005
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  public            System.Int32                    capacity  // 0x10
  public            System.Int32                    tSize  // 0x14
  public            System.Boolean                  ring  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000026  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.AnimatorBlackboardUtility
TYPE:  static class
TOKEN: 0x2000006
SIZE:  0x10
FIELDS:
  private   static  System.Boolean                  s_enableFastPathBuffer  // static @ 0x0
METHODS:
  RVA=-1  // generic def  token=0x6000027  System.Void ClearAndEnsureDictionaryCapacity(System.Collections.Generic.Dictionary<TKey,TValue>& dict, System.Int32 capacity)
END_CLASS

CLASS: Beyond.SourceGenerator.AnimatorBlackboardAttribute
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000028  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.CameraControlConfigAttribute
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000029  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.ECSComponentAttribute
TYPE:  class
TOKEN: 0x2000009
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public            System.Runtime.InteropServices.LayoutKindlayoutKind  // 0x10
  public            System.Boolean                  isTag  // 0x14
METHODS:
  RVA=0x041E1670  token=0x600002A  System.Void .ctor()
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
TOKEN: 0x200004D
SIZE:  0x28
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
  private           System.Int32                    methodId  // 0x18
  private           System.Object                   anonObj  // 0x20
  public    static  IFix.ILFixDynamicMethodWrapper[]wrapperArray  // static @ 0x0
METHODS:
  RVA=0x032B2C30  token=0x60002B4  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj)
  RVA=0x063632E4  token=0x60002B5  System.Boolean __Gen_Wrap_0(System.Object P0)
  RVA=0x063621C4  token=0x60002B6  System.UInt32 __Gen_Wrap_1()
  RVA=0x063633B4  token=0x60002B7  System.Void __Gen_Wrap_2(System.Object P0)
  RVA=0x06377B18  token=0x60002B8  System.Void __Gen_Wrap_3(System.Object P0, System.Object P1)
  RVA=0x07199240  token=0x60002B9  System.Void __Gen_Wrap_4(System.Object P0, System.Boolean P1, System.Boolean P2)
  RVA=0x066C2B40  token=0x60002BA  System.Void __Gen_Wrap_5(System.Object P0, System.Boolean P1)
  RVA=0x066C44C0  token=0x60002BB  System.Void __Gen_Wrap_6(System.Object P0, System.Int32 P1, System.Boolean P2)
  RVA=0x066F3838  token=0x60002BC  System.Void __Gen_Wrap_7(System.Object P0, UnityEngine.UI.NavigationBindingType P1)
  RVA=0x066E0A9C  token=0x60002BD  System.Boolean __Gen_Wrap_8(System.Object P0, System.Int32 P1)
  RVA=0x066C43C8  token=0x60002BE  System.Void __Gen_Wrap_9(System.Object P0, System.Object P1, System.Boolean P2)
  RVA=0x07195DFC  token=0x60002BF  System.Boolean __Gen_Wrap_10(System.Object P0, System.Object P1, System.Object P2)
  RVA=0x071BADB4  token=0x60002C0  System.Int32 __Gen_Wrap_11(System.Object P0, System.Int32 P1, System.Object P2)
  RVA=0x06360E84  token=0x60002C1  System.Int32 __Gen_Wrap_12(System.Object P0)
  RVA=0x09E3B710  token=0x60002C2  UnityEngine.Vector3 __Gen_Wrap_13(System.Object P0, UnityEngine.UI.NaviDirection P1)
  RVA=0x09E3B82C  token=0x60002C3  UnityEngine.Vector3 __Gen_Wrap_14(System.Object P0, UnityEngine.Vector2 P1)
  RVA=0x09E3B970  token=0x60002C4  UnityEngine.UI.Selectable __Gen_Wrap_15(System.Object P0, System.Object P1, UnityEngine.Vector3 P2, UnityEngine.Vector3 P3, UnityEngine.UI.NaviStrategy P4, System.Double P5, System.Double& P6)
  RVA=0x09E3BB3C  token=0x60002C5  UnityEngine.UI.Selectable __Gen_Wrap_16(System.Object P0, System.Object P1, UnityEngine.Vector3 P2, UnityEngine.Vector3 P3, System.Double P4, System.Double& P5)
  RVA=0x069AB898  token=0x60002C6  System.Boolean __Gen_Wrap_17(System.Object P0, UnityEngine.UI.NaviDirection P1)
  RVA=0x09E3BCF8  token=0x60002C7  System.Collections.Generic.LinkedListNode<UnityEngine.UI.SelectableNaviGroup> __Gen_Wrap_18(System.Object P0, System.Object P1)
  RVA=0x06366BC4  token=0x60002C8  System.Boolean __Gen_Wrap_19(System.Object P0, System.Object P1)
  RVA=0x063778A4  token=0x60002C9  System.Void __Gen_Wrap_20(System.Object P0, System.Object P1, UnityEngine.UI.NavigationBindingType P2)
  RVA=0x09E3BE04  token=0x60002CA  UnityEngine.UI.SelectableNaviGroup __Gen_Wrap_21(System.Object P0)
  RVA=0x06364290  token=0x60002CB  System.Void __Gen_Wrap_22(System.Object P0, System.Object P1, System.Object P2)
  RVA=0x06373E64  token=0x60002CC  System.Void __Gen_Wrap_23(System.Object P0, System.Object P1, System.Object P2, System.Object P3)
  RVA=0x06A5FB54  token=0x60002CD  System.Void __Gen_Wrap_24(System.Object P0, UnityEngine.UI.NaviDirection P1)
  RVA=0x09E3BEF4  token=0x60002CE  Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>> __Gen_Wrap_25(System.Object P0)
  RVA=0x09E3BFD0  token=0x60002CF  Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>>> __Gen_Wrap_26(System.Object P0)
  RVA=0x09E3C0AC  token=0x60002D0  Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>> __Gen_Wrap_27(System.Object P0, System.Object P1)
  RVA=0x09E3C1B8  token=0x60002D1  Beyond.Input.GamepadCheckerBase.Binding __Gen_Wrap_28(System.Object P0, System.Object P1, System.Object P2, System.Boolean P3)
  RVA=0x09E3C2EC  token=0x60002D2  Beyond.Input.GamepadCheckerBase.Binding __Gen_Wrap_29(System.Object P0, System.Object P1, System.Object P2)
  RVA=0x063778A4  token=0x60002D3  System.Void __Gen_Wrap_30(System.Object P0, System.Object P1, Beyond.Input.GamepadKeyCode P2)
  RVA=0x063761FC  token=0x60002D4  System.Void __Gen_Wrap_31()
  RVA=0x066F3838  token=0x60002D5  System.Void __Gen_Wrap_32(System.Object P0, Beyond.Input.InputContinuousTriggerType P1)
  RVA=0x09E3C414  token=0x60002D6  Beyond.Input.IBindingView __Gen_Wrap_33(System.Object P0)
  RVA=0x06A5FB54  token=0x60002D7  System.Void __Gen_Wrap_34(System.Object P0, Beyond.Input.GamepadKeyCode P1)
  RVA=0x09E3C504  token=0x60002D8  Beyond.Input.PlayerActionInfo __Gen_Wrap_35(System.Object P0, System.Object P1, Beyond.Input.InputSettingLevel P2)
  RVA=0x09E3C628  token=0x60002D9  System.String __Gen_Wrap_36(System.Object P0, System.Object P1)
  RVA=0x09E3C734  token=0x60002DA  System.String __Gen_Wrap_37(System.Object P0)
  RVA=0x09E3C824  token=0x60002DB  System.Action __Gen_Wrap_38(System.Object P0, System.Object P1, Beyond.Input.GamepadKeyCode P2)
  RVA=0x09E3C938  token=0x60002DC  System.Single __Gen_Wrap_39(System.Object P0, System.Object P1, System.Int32 P2)
  RVA=0x063655AC  token=0x60002DD  System.Boolean __Gen_Wrap_40(Beyond.Input.GamepadKeyCode P0)
  RVA=0x09E3CA3C  token=0x60002DE  UnityEngine.RectTransform __Gen_Wrap_41(System.Object P0)
  RVA=0x09E3CB2C  token=0x60002DF  System.Single __Gen_Wrap_42(System.Object P0)
  RVA=0x066C288C  token=0x60002E0  System.Void __Gen_Wrap_43(System.Object P0, System.Single P1)
  RVA=0x09E3CBE8  token=0x60002E1  UnityEngine.InputSystem.Mouse __Gen_Wrap_44(System.Object P0)
  RVA=0x09E3CCD8  token=0x60002E2  UnityEngine.InputSystem.InputActionProperty __Gen_Wrap_45(System.Object P0)
  RVA=0x09E3CDDC  token=0x60002E3  System.Void __Gen_Wrap_46(UnityEngine.InputSystem.InputActionProperty& P0, UnityEngine.InputSystem.InputActionProperty P1)
  RVA=0x09E3CF54  token=0x60002E4  System.Void __Gen_Wrap_47(System.Object P0, UnityEngine.InputSystem.InputActionProperty P1)
  RVA=0x09E3D05C  token=0x60002E5  System.Void __Gen_Wrap_48(UnityEngine.InputSystem.InputActionProperty P0, System.Object P1, System.Boolean P2)
  RVA=0x09E3D180  token=0x60002E6  System.Void __Gen_Wrap_49(System.Object P0, UnityEngine.InputSystem.InputAction.CallbackContext P1)
  RVA=0x063621C4  token=0x60002E7  System.Int32 __Gen_Wrap_50()
  RVA=0x09E3D280  token=0x60002E8  Beyond.Input.KeyboardCheckerBase.Binding __Gen_Wrap_51(System.Object P0, System.Object P1, System.Object P2, System.Boolean P3)
  RVA=0x09E3D3B4  token=0x60002E9  System.Int32 __Gen_Wrap_52(System.Object P0, System.Object P1, System.Object P2, System.Int32 P3)
  RVA=0x066C53B4  token=0x60002EA  System.Boolean __Gen_Wrap_53(UnityEngine.UI.Selectable P0)
  RVA=0x09E3D4C8  token=0x60002EB  UnityEngine.UI.Selectable __Gen_Wrap_54(System.Object P0)
  RVA=0x09E3D5A4  token=0x60002EC  Beyond.Input.KeyboardCheckerBase.Binding __Gen_Wrap_55(System.Object P0, System.Object P1, System.Object P2)
  RVA=0x066F3838  token=0x60002ED  System.Void __Gen_Wrap_56(System.Object P0, System.Int32 P1)
  RVA=0x09E3D6CC  token=0x60002EE  System.Object __Gen_Wrap_57(System.Object P0)
  RVA=0x09E3D7A8  token=0x60002EF  Beyond.Input.IBindingGroupTarget __Gen_Wrap_58(System.Object P0)
  RVA=0x063623A8  token=0x60002F0  System.Boolean __Gen_Wrap_59()
  RVA=0x09E3D884  token=0x60002F1  UnityEngine.Vector2 __Gen_Wrap_60(System.Object P0, System.Boolean P1, System.Boolean P2)
  RVA=0x09E3D9AC  token=0x60002F2  System.Single __Gen_Wrap_61(System.Object P0, System.Boolean P1)
  RVA=0x09E3DA98  token=0x60002F3  UnityEngine.Vector2 __Gen_Wrap_62()
  RVA=0x06375088  token=0x60002F4  System.Void __Gen_Wrap_63(System.Boolean P0)
  RVA=0x09E3DB64  token=0x60002F5  System.Void __Gen_Wrap_64(System.Object P0, System.Object P1, System.Int32 P2, System.Boolean P3)
  RVA=0x0635FF24  token=0x60002F6  System.Void __Gen_Wrap_65(Beyond.DeviceInfo.InputType P0)
  RVA=0x066F3838  token=0x60002F7  System.Void __Gen_Wrap_66(System.Object P0, Beyond.DeviceInfo.InputType P1)
  RVA=0x06366E40  token=0x60002F8  Beyond.Input.KeyboardType __Gen_Wrap_67(System.Object P0)
  RVA=0x066F3838  token=0x60002F9  System.Void __Gen_Wrap_68(System.Object P0, Beyond.Input.KeyboardType P1)
  RVA=0x09E3DC6C  token=0x60002FA  System.Collections.Generic.Dictionary<System.String,System.Collections.Generic.HashSet<System.Int32>> __Gen_Wrap_69(System.Object P0)
  RVA=0x071C15C8  token=0x60002FB  System.Void __Gen_Wrap_70(System.Object P0, System.Object P1, System.Single P2)
  RVA=0x06A5FB54  token=0x60002FC  System.Void __Gen_Wrap_71(System.Object P0, Beyond.Input.KeyboardKeyCode P1)
  RVA=0x06301ED4  token=0x60002FD  System.Void __Gen_Wrap_72(Beyond.Input.KeyboardKeyCode P0)
  RVA=0x06301ED4  token=0x60002FE  System.Void __Gen_Wrap_73(Beyond.Input.GamepadKeyCode P0)
  RVA=0x02D541E0  token=0x60002FF  Beyond.DeviceInfo.ControllerType __Gen_Wrap_74(System.Object P0, System.Object P1, System.Boolean& P2)
  RVA=0x09E3DD5C  token=0x6000300  System.String __Gen_Wrap_75(Beyond.Input.GamepadKeyCode P0)
  RVA=0x09E3DE48  token=0x6000301  System.String __Gen_Wrap_76()
  RVA=0x09E3DF14  token=0x6000302  System.String __Gen_Wrap_77(System.Object P0, System.Object P1, System.Boolean P2, System.Boolean P3)
  RVA=0x09E3E048  token=0x6000303  System.String __Gen_Wrap_78(System.Object P0, System.Boolean P1, System.Boolean P2)
  RVA=0x09E3E170  token=0x6000304  System.String __Gen_Wrap_79(System.Object P0, System.Boolean P1, System.Boolean P2, System.Boolean P3)
  RVA=0x09E3E2A4  token=0x6000305  System.Void __Gen_Wrap_80(System.Object P0, Beyond.EventData<System.Boolean>& P1)
  RVA=0x09E3E3C4  token=0x6000306  System.Boolean __Gen_Wrap_81(System.Object P0, System.Object P1, Beyond.Input.KeyboardKeyCode& P2)
  RVA=0x09E3E4DC  token=0x6000307  System.Boolean __Gen_Wrap_82(System.Object P0, System.Object P1, Beyond.Input.KeyboardKeyCode& P2, System.Boolean& P3)
  RVA=0x06A2DAF8  token=0x6000308  System.Boolean __Gen_Wrap_83(System.Object P0, Beyond.Input.GamepadKeyCode P1, Beyond.Input.InputManager.KeyCodeState P2)
  RVA=0x09E3E628  token=0x6000309  System.Int32 __Gen_Wrap_84(System.Object P0, System.Object P1, System.Object P2, System.Int32 P3, System.String& P4)
  RVA=0x09E3E798  token=0x600030A  System.Int32 __Gen_Wrap_85(System.Object P0, Beyond.Input.KeyboardKeyCode P1, System.Object P2, Beyond.Input.InputTimingType P3, System.Object P4, System.Int32 P5)
  RVA=0x09E3E8BC  token=0x600030B  System.Boolean __Gen_Wrap_86(System.Object P0, System.Int32 P1, System.Boolean P2)
  RVA=0x066F4C88  token=0x600030C  System.Void __Gen_Wrap_87(System.Object P0, System.Int32 P1, System.Object P2)
  RVA=0x09E3E9C0  token=0x600030D  System.Void __Gen_Wrap_88(System.Object P0, System.Boolean P1, System.Int32 P2, System.Int32 P3)
  RVA=0x09E3EAC8  token=0x600030E  System.Int32 __Gen_Wrap_89(System.Object P0, System.Boolean P1, System.Int32 P2)
  RVA=0x09E3EBCC  token=0x600030F  Beyond.Input.KeyboardKeyCode __Gen_Wrap_90(System.Object P0, System.Object P1, System.Boolean P2, System.Boolean P3, Beyond.Input.InputSettingLevel P4)
  RVA=0x09E3EBCC  token=0x6000310  Beyond.Input.GamepadKeyCode __Gen_Wrap_91(System.Object P0, System.Object P1, System.Boolean P2, System.Boolean P3, Beyond.Input.InputSettingLevel P4)
  RVA=0x06373694  token=0x6000311  System.Boolean __Gen_Wrap_92(Beyond.Input.InputDeviceFlags P0, Beyond.Input.InputDeviceFlags P1)
  RVA=0x09E3ECF0  token=0x6000312  System.Boolean __Gen_Wrap_93(System.Object P0, System.Object P1, Beyond.Input.InputDeviceFlags P2)
  RVA=0x071BBFCC  token=0x6000313  System.Void __Gen_Wrap_94(System.Object P0, System.Object P1, Beyond.Input.InputDeviceFlags P2)
  RVA=0x09E3EDF8  token=0x6000314  System.Void __Gen_Wrap_95(System.Object P0, Beyond.Input.InputDeviceFlags P1)
  RVA=0x09E3EED4  token=0x6000315  System.String __Gen_Wrap_96(System.Object P0, System.Int32 P1)
  RVA=0x09E3EFE0  token=0x6000316  System.Boolean __Gen_Wrap_97(System.Object P0, System.Object P1, Beyond.Input.KeyboardKeyCode P2, System.Boolean& P3)
  RVA=0x066C0800  token=0x6000317  System.Single __Gen_Wrap_98(System.Object P0, System.Object P1)
  RVA=0x09E3F108  token=0x6000318  System.Void __Gen_Wrap_99(System.Object P0, System.Int32 P1, System.String& P2, Beyond.Input.IBindingView& P3)
  RVA=0x09E3B06C  token=0x6000319  Beyond.Input.PlayerActionInfo __Gen_Wrap_100(System.Object P0, System.Object P1)
  RVA=0x09E3B178  token=0x600031A  System.Boolean __Gen_Wrap_101(System.Object P0, System.Object P1, Beyond.Input.KeyboardKeyCode P2, System.Boolean P3, System.Object P4)
  RVA=0x09E3B29C  token=0x600031B  System.Boolean __Gen_Wrap_102(System.Object P0, System.Object P1, System.Nullable<Beyond.Input.GamepadKeyCode> P2, System.Nullable<Beyond.Input.GamepadKeyCode> P3, System.Boolean P4, System.Object P5)
  RVA=0x09E3B40C  token=0x600031C  UnityEngine.Vector3 __Gen_Wrap_103()
  RVA=0x09E3B500  token=0x600031D  System.Void __Gen_Wrap_104(UnityEngine.Vector2 P0, System.Boolean P1)
  RVA=0x09E3B608  token=0x600031E  System.Void __Gen_Wrap_105(System.Object P0, UnityEngine.Vector2 P1)
  RVA=0x06361FA4  token=0x600031F  System.Boolean __Gen_Wrap_106(System.Int32 P0)
  RVA=0x02D4A050  token=0x6000320  System.Boolean __Gen_Wrap_107(System.Boolean P0)
  RVA=0x02D4A250  token=0x6000321  System.Collections.Generic.List<Beyond.Input.InputManager.ControllerHintInfo> __Gen_Wrap_108(System.Object P0)
  RVA=0x02D4AB80  token=0x6000322  System.Void __Gen_Wrap_109(System.Object P0, System.Object P1, System.Boolean P2, System.Collections.Generic.List<Beyond.Input.InputManager.ControllerHintInfo>& P3)
  RVA=0x02D4B730  token=0x6000323  System.Void __Gen_Wrap_110(System.Object P0, System.Int32 P1, System.Boolean P2, System.Collections.Generic.List<Beyond.Input.InputManager.ControllerHintInfo>& P3)
  RVA=0x02D4C200  token=0x6000324  System.Void __Gen_Wrap_111(System.Object P0, System.Object P1, System.Collections.Generic.List<Beyond.Input.InputManager.ControllerHintInfo>& P2)
  RVA=0x02D4CD90  token=0x6000325  System.Single __Gen_Wrap_112(System.Object P0, System.Object P1, System.Single& P2)
  RVA=0x01774E60  token=0x6000326  System.Single __Gen_Wrap_113(System.Object P0, System.Int32 P1, System.Single& P2)
  RVA=0x02D4D1A0  token=0x6000327  System.Void __Gen_Wrap_114(System.Object P0, System.Single P1, System.Single P2)
  RVA=0x02D4D4A0  token=0x6000328  System.String __Gen_Wrap_115(Beyond.Input.KeyboardKeyCode P0)
  RVA=0x02D4DD20  token=0x6000329  System.String __Gen_Wrap_116(System.Text.RegularExpressions.Match P0)
  RVA=0x01527A20  token=0x600032A  System.Void __Gen_Wrap_117(System.Object P0, System.Object P1, System.Object P2, System.Boolean P3)
  RVA=0x02D4E650  token=0x600032B  System.Boolean __Gen_Wrap_118(System.Int32 P0, UnityEngine.Vector2& P1)
  RVA=0x02D4EF20  token=0x600032C  UnityEngine.GameObject __Gen_Wrap_119(System.Object P0, System.Object P1)
  RVA=0x02D4F940  token=0x600032D  System.String __Gen_Wrap_120(System.Object P0, Beyond.Input.KeyboardKeyCode P1, System.Boolean P2)
  RVA=0x02D50300  token=0x600032E  System.String __Gen_Wrap_121(System.Object P0, Beyond.Input.GamepadKeyCode P1, System.Boolean P2)
  RVA=0x02D50CC0  token=0x600032F  System.String __Gen_Wrap_122(System.Object P0, System.Object P1, System.Boolean P2, System.Boolean P3, System.Boolean P4)
  RVA=0x02D51750  token=0x6000330  System.Void __Gen_Wrap_123(System.Object P0, Beyond.EventData<System.String>& P1)
  RVA=0x02D51DD0  token=0x6000331  Beyond.Input.InputManager.DelayedComboBinding __Gen_Wrap_124(System.Object P0, System.Object P1, System.Object P2, Beyond.Input.InputManager.DelayedComboKind P3, System.Single P4, System.Object P5)
  RVA=0x01353F60  token=0x6000332  System.Int32 __Gen_Wrap_125(System.Object P0, System.Object P1)
  RVA=0x02D529C0  token=0x6000333  System.Collections.Generic.List<Beyond.DynamicFastLookupCollection<Beyond.Input.KeyboardKeyCode,System.Collections.Generic.HashSet<Beyond.Input.KeyboardCheckerBase.Binding>>> __Gen_Wrap_126(System.Object P0)
  RVA=0x01412530  token=0x6000334  System.Void __Gen_Wrap_127(System.Object P0, System.Object P1, Beyond.Input.KeyboardKeyCode P2)
  RVA=0x02D532F0  token=0x6000335  System.String __Gen_Wrap_128(Beyond.Input.CursorRequest& P0)
  RVA=0x04D37AA0  token=0x6000336  System.Void .cctor()
END_CLASS

CLASS: IFix.ILFixInterfaceBridge
TYPE:  class
TOKEN: 0x200004E
SIZE:  0x40
EXTENDS: IFix.Core.AnonymousStorey
FIELDS:
METHODS:
  RVA=0x09E3F280  token=0x6000337  System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine)
  RVA=0x041E1670  token=0x6000338  System.Void RefAsyncBuilderStartMethod()
END_CLASS

CLASS: IFix.WrappersManagerImpl
TYPE:  class
TOKEN: 0x200004F
SIZE:  0x18
IMPLEMENTS: IFix.Core.WrappersManager
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000339  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine)
  RVA=0x09E41724  token=0x600033A  IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id)
  RVA=0x02FAA9F0  token=0x600033B  System.Boolean IsPatched(System.Int32 id)
  RVA=0x09E415E0  token=0x600033C  System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon)
  RVA=0x09E416AC  token=0x600033D  System.Object CreateWrapper(System.Int32 id)
  RVA=0x09E41788  token=0x600033E  System.Object InitWrapperArray(System.Int32 len)
  RVA=0x09E4153C  token=0x600033F  IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine)
END_CLASS

CLASS: IFix.IDMAP0
TYPE:  sealed struct
TOKEN: 0x2000050
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingGroup-get_enabled0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingInfo-_GetNextEnabledPriority0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingInfo-StopContinuousTrigger0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-KeyboardOnLongPressChecker-CancelPendingLongPress0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-GamepadOnLongPressChecker-CancelPendingLongPress0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-CancelPendingLongPress0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingInfo-OnGroupEnabledChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingGroup-OnParentEnabledChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingGroup-_OnBindingGroupStateChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingGroup-SetSelfEnabled0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-ToggleGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-ControllerNaviManager-SetNaviBindingType0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-IsGroupEnabled0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-ControllerNaviManager-IsNavigationBindingEnabled0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-get_usingController0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-ControllerNaviManager-UpdateNaviInputBindingState0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-ControllerNaviManager-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-ControllerNaviManager-_RemoveLayerFromStack0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-ControllerNaviManager-_RemoveLayer0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-ControllerNaviManager-_OnLayerSelected0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingGroup-set_groupTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-CreateGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingGroupMonoTarget-UpdateState0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingGroupMonoTarget-Init0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingGroupMonoTarget-get_groupId0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-ControllerNaviManager-_SetTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-ControllerNaviManager-GetNaviVector0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-ControllerNaviManager-_GetPointOnRectEdge0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-ControllerNaviManager-_TryFindBestNaviTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-ControllerNaviManager-_TryFindBestNaviTargetInGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-ControllerNaviManager-SetTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-ControllerNaviManager-_DefaultNavigate0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-ControllerNaviManager-_GetDummyLayerLastGroupInStack0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-ControllerNaviManager-SetForceEnableNavi0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-ControllerNaviManager-TryAutoSetTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-ControllerNaviManager-TryRemoveLayer0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-ControllerNaviManager-IsLayerInStack0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-ControllerNaviManager-IsTopLayer0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-ControllerNaviManager-ChangeAndTrySetNaviBindingType0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-ControllerNaviManager-GetTopDummyLayerGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-ControllerNaviManager-ResetStateForUIDispose0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-ControllerNaviManager-SetTargetInSilentModeIfNecessary0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-ControllerNaviManager-ClearTargetIfNecessary0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-ControllerNaviManager-SetTargetInSilentModeIfNecessary1  // const
  public    static  IFix.IDMAP0                     Beyond-Input-ControllerNaviManager-IsNaviTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-ControllerNaviManager-Navigate0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-ControllerNaviManager-ToggleNavigation0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-GamepadCheckerBase-get_normalBindingInfos0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-GamepadCheckerBase-get_modifyBindingInfos0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-GamepadCheckerBase-GetInputDic0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-GamepadCheckerBase-OnCreateBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-GamepadCheckerBase-OnDeleteBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-GamepadCheckerBase-CheckDic0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-GamepadCheckerBase-CheckGamepadInput0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingInfo-_RealTriggerCallback0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingInfo-Trigger0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-IsGuideUseAction0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingInfo-get_enabled0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingInfo-_TryContinuousTrigger0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingInfo-StartContinuousTrigger0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-GamepadCheckerBase-TriggerActions0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-GamepadChecker-CheckDic0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingGroup-get_groupView0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingInfo-get_bindingView0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-GamepadOnClickChecker-CheckDic0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-GamepadOnClickChecker-ClearKeyPressed0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetPlayerActionInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetActionText0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetActionText1  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingInfo-get_text0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-GamepadOnLongPressChecker-_OnTriggerBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-GamepadOnLongPressChecker-CreateTriggerBindingAction0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-GamepadOnLongPressChecker-CheckDic0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-GamepadOnLongPressChecker-OnCreateBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-GamepadOnLongPressChecker-OnDeleteBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-GamepadOnLongPressChecker-GetInputLongPressStartTime0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-GamepadOnReleaseChecker-CheckGamepadInput0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-GamepadOnReleaseChecker-CheckDic0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-get_cursorTransform0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-set_cursorTransform0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-get_scrollSpeed0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-set_scrollSpeed0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-get_virtualMouse0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-get_stickAction0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-_SetAction0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-set_stickAction0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-get_leftButtonAction0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-_SetActionCallback0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-set_leftButtonAction0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-get_rightButtonAction0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-set_rightButtonAction0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-get_middleButtonAction0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-set_middleButtonAction0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-get_forwardButtonAction0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-set_forwardButtonAction0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-get_backButtonAction0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-set_backButtonAction0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-get_scrollWheelAction0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-set_scrollWheelAction0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-SetMouseIconVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-_OnButtonActionTriggered0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-TriggerLeftButton0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-get_invalidID0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-KeyboardCheckerBase-GetModifyIndex0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-KeyboardCheckerBase-OnCreateBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_AddPlayerActionCheck0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_AddPlayerActionCheck1  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_AddPlayerActionCheck2  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-CreateBindingByActionId0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-_ChangeHoverState0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-ChangeHoverState0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-_SyncScale0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-_UpdateMotion0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-ClearLeftButtonState0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-OnAfterInputUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-Update0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-GetCurHoverSelectable0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-KeyboardCheckerBase-OnDeleteBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_RemovePlayerActionCheck0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_RemovePlayerActionCheck1  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_RemovePlayerActionCheck2  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingInfo-OnDelete0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_RealRemoveBindingInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_DeleteInGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_DeleteGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-DeleteGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-HGVirtualMouse-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingInfo-set_bindingView0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingInfo-set_text0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingInfo-ChangeGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingInfo-SetSelfEnabled0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingInfo-SetParentIgnored0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingInfo-GetCallbackTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingGroup-set_groupView0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingGroup-get_groupTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingGroup-ChangeParent0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingGroup-SetParentIgnored0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingGroupMonoTarget-get_groupEnabled0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingGroupMonoTarget-get_parentTarget0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingGroupMonoTarget-get_internalEnabled0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingGroupMonoTarget-set_internalEnabled0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingGroupMonoTarget-DeleteGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingGroupMonoTarget-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingGroupMonoTarget-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingGroupMonoTarget-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputBindingGroupMonoTarget-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-RealCursorManager-get_isDebugForceShow0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-get_isDebugForceShow0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-get_cursorVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-get_isInputSuppressedByDeviceSwitch0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetGamepadStickValue0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetGamepadTriggerValue0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-AnyGamepadKey0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-AnyMouseKeyDown0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-AnyKeyDown0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-AnyTouchBegan0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-get_anyKeyDown0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-get_mouseScrollDelta0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-get_multiTouchEnabled0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-set_multiTouchEnabled0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-RealCursorManager-HasAnyShowRequestAtPriority0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-RealCursorManager-_ToggleCursorInternal0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-RealCursorManager-CalcState0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-RealCursorManager-SetVirtualMouseActive0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_ToggleVirtualMouse0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_CheckVirtualMouseState0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-ToggleBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_RefreshDeviceChangeBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-RealCursorManager-_RefreshActiveHighest0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-RealCursorManager-SetRequest0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-RealCursorManager-RemoveRequest0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-get_autoUpdateInputBindingMask0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_SetInputBindingMask0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_UpdateInputBindingMask0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_SyncUsingControllerState0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-SetCursorShowRequest0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-ChangeInputTypeAndSyncState0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_TryChangeInputDeviceType0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-set_usingController0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-get_virtualMouseIconVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-KeyboardHandler-GetKeyboardType0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-get_keyboardType0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-KeyboardHandler-SetKeyboardType0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-set_keyboardType0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-get_action2BindingDic0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-get_rootGroupId0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_UpdateModifierTapThenTriggerCombo0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_UpdateTriggerThenModifierCombo0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_UpdateDelayedCombos0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-LateTick0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_CreateTickFunction0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-KeyboardOnClickChecker-ClearKeyPressed0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-KeyboardHandler-ParseConfig0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_LoadCustomInputSetting0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_LoadSetting0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_IsValidJoystick0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_GetControllerType0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_SetJoystick0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_OnControllerConnected0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_TryFindGamepad0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_OnControllerDisconnected0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_CreateDebugVirtualMouse0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetStringByGamepadKeyCode0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_GetGamepadKeyIconFolderPath0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_GetKeyIconPath0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetGamepadKeyIconPath0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetKeyIconPath0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetKeyIconPath1  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetKeyIconPath2  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_CreateVirtualMouse0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_CreateCanvas0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_InitDeviceChangeBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-KeyCaches-UpdateKeyStat0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_CheckAnyKeyForTouchChangeDevice0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-AnyMouseKeyUp0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_AnyKeyDownForKeyboardChangeDevice0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_CheckAnyKeyForKeyboardChangeDevice0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-CheckUsingController0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-KeyboardCheckerBase-CheckKeyboardInput0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-KeyboardCheckerBase-TriggerActions0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_ControllerTouchPanelTick0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_CheckInput0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_TryStopDeviceSwitchSuppression0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_OnAfterInputUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_OnNetMaskChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_OnFocusedInputFieldChanged0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_Init0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_ClearTickFunction0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-Release0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_ClearEvents0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-OnApplicationQuit0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-OnApplicationFocus0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-set_autoUpdateInputBindingMask0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-RealCursorManager-RemoveAllRequestsAtPriority0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-ClearAllCursorShowRequests0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-ToggleInputDeviceChangeMode0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-SetCursorOverrideForDeviceChange0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_AnyKeyboardKey0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-AnyKeyboardKey0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-AnyMouseKey0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-AnyTouchActive0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_CheckGamepadKeyCode0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-AddAnonymousPlayerAction0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-CreateBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-CreateBinding1  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-CreateBinding2  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-DeleteBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-DeleteInGroup0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-IsBindingEnabled0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-IsGroupParentDisabled0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-ForceBindingKeyhintToGray0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-IsBindingKeyhintGray0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-ToggleDebugCursorMode0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-ToggleAllInput0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-SetBindingView0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-SetGroupView0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-ChangeParent0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetGroupParentId0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-IgnoreBindingGroupParent0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-IgnoreBindingParent0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetActionKeyboardKeyCode0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetActionGamepadKeyCode0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputUtils-HasFlag0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-PlayerActionInfo-DeviceInputEquals0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-PlayerActionInfo-DeviceInputCopyFrom0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-DeleteCustomBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-DeleteAllCustomBindings0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-SetBindingText0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetBindingText0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-HasBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-CheckActionKeyCodeConflict0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetAxis0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetBindingInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-LoadSetting0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-IsPlayerActionIdValid0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-ChangePlayerAction0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_GetOrCreatePendingActionInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-CustomizeActionKey0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-CustomizeActionKey1  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-ClearPendingInputSetting0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-SaveCustomInputSetting0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetMousePos0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-get_mousePosition0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-SetVirtualMouseIconVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-SetMousePos0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-SetOnlyVirtualMousePos0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetMouseButton0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-IsLeftMouseDown0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetMouseButtonDown0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetMouseButtonUp0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-SetCustomControllerMouse0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetEmptyControllerHintInfoList0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-ControllerHintInfo-Reset0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_GetControllerHintInfos0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetControllerHintInfos0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-ControllerHintInfo-Reset1  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_GetControllerHintInfos1  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetControllerHintInfos1  // const
  public    static  IFix.IDMAP0                     Beyond-Input-KeyboardOnLongPressChecker-GetInputLongPressStartTime0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetPlayerActionLongPressStartTime0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetActionBindingLongPressStartTime0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-AddGuideUseActionId0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-RemoveGuideUseActionId0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-ClearGuideUseActionIds0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_IsAnyBindingInGroupGuideUse0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-IsAnyBindingInGroupGuideUse0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-VibrateController0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetStringByKeyboardKeyCode0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetKeyboardIconPath0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-ParseTextActionId0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-MoveMouseTo0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-MoveVirtualMouseTo0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetTouchId0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetPressPosByTouchId0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-RemoveUnusedInput0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetCurJsInfos0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-RealCursorManager-DumpState0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-GetRealCursorState0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_GetInputBindingGroupGameObject0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_GetInputBindingGameObject0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_GetBindingName0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-PrintBindingInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-PrintKeycodeInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-PrintKeycodeInfo1  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-PrintKeycodeInfo2  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-PrintActionInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_RefreshDelayedComboKeys0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-_OnDelayedComboActionKeyUpdated0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-RegisterDelayedCombo0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-UnregisterDelayedCombo0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-ControllerHintInfo-CompareTo0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-ControllerHintInfo-OnAllocate0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputManager-ControllerHintInfo-OnRecycle0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputUtils-IgnoreBindingParent0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-InputUtils-IgnoreBindingGroupParent0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-KeyboardCheckerBase-get_bindingInfos0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-KeyboardChecker-CheckKeyboardInput0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-KeyboardChecker-TriggerActions0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-KeyboardOnClickChecker-CheckKeyboardInput0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-KeyboardOnClickChecker-TriggerActions0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-KeyboardOnLongPressChecker-_OnTriggerBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-KeyboardOnLongPressChecker-CheckKeyboardInput0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-KeyboardOnLongPressChecker-OnCreateBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-KeyboardOnLongPressChecker-OnDeleteBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-PlayerActionInfo-ResetHintProority0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-KeyboardInput-GetModifyText0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-KeyboardInput-GetKeyCodeText0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-KeyboardInput-GetKeyCodeEnumIntValue0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-KeyboardInput-SetKeyCodeEnumByIntValue0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-KeyboardInput-get_modifyString0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-KeyboardInput-ClearKeyCode0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-KeyboardInput-IsValid0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-KeyboardInput-CopyFrom0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-KeyboardInput-Equals0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-GamepadInput-GetModifyText0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-GamepadInput-GetKeyCodeText0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-GamepadInput-GetKeyCodeEnumIntValue0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-GamepadInput-SetKeyCodeEnumByIntValue0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-GamepadInput-ClearKeyCode0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-GamepadInput-IsValid0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-GamepadInput-CopyFrom0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-GamepadInput-Equals0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-CursorRequest-ToString0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-UIEvent-get_bindingEnabled0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-UIEvent-_CreatePlayerActionBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-UIEvent-InitBinding0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-UIEvent-Invoke0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-UIEvent-UpdateKeys0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-UIEvent-ChangeBindingPlayerAction0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-UIEvent-get_count0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-UIEvent-AddListener0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-UIEvent-RemoveListener0  // const
  public    static  IFix.IDMAP0                     Beyond-Input-UIEvent-RemoveAllListeners0  // const
METHODS:
END_CLASS

CLASS: UnityEngine.InputSystem.UI.DebugVirtualMouseInput
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x128
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.InputSystem.UI.DebugVirtualMouseInput.CursorModem_CursorMode  // 0x18
  private           UnityEngine.UI.Graphic          m_CursorGraphic  // 0x20
  private           UnityEngine.RectTransform       m_CursorTransform  // 0x28
  private           UnityEngine.RectTransform       _canvasTransform  // 0x30
  private           UnityEngine.Canvas              m_Canvas  // 0x38
  private           System.Single                   m_CursorSpeed  // 0x40
  private           System.Single                   m_ScrollSpeed  // 0x44
  private           UnityEngine.InputSystem.InputActionPropertym_StickAction  // 0x48
  private           UnityEngine.InputSystem.InputActionPropertym_LeftButtonAction  // 0x60
  private           UnityEngine.InputSystem.InputActionPropertym_MiddleButtonAction  // 0x78
  private           UnityEngine.InputSystem.InputActionPropertym_RightButtonAction  // 0x90
  private           UnityEngine.InputSystem.InputActionPropertym_ForwardButtonAction  // 0xa8
  private           UnityEngine.InputSystem.InputActionPropertym_BackButtonAction  // 0xc0
  private           UnityEngine.InputSystem.InputActionPropertym_ScrollWheelAction  // 0xd8
  private           UnityEngine.InputSystem.Mouse   m_VirtualMouse  // 0xf0
  private           UnityEngine.InputSystem.Mouse   m_SystemMouse  // 0xf8
  private           System.Action                   m_AfterInputUpdateDelegate  // 0x100
  private           System.Action<UnityEngine.InputSystem.InputAction.CallbackContext>m_ButtonActionTriggeredDelegate  // 0x108
  private           System.Double                   m_LastTime  // 0x110
  private           UnityEngine.Vector2             m_LastStickValue  // 0x118
  private           System.Single                   m_canvasScale  // 0x120
PROPERTIES:
  cursorTransform  get=0x04D86240  set=0x02FDB880
  cursorSpeed  get=0x04D863F0  set=0x04D86470
  cursorMode  get=0x011EF5B0  set=0x09E3387C
  cursorGraphic  get=0x04D862C0  set=0x09E33858
  scrollSpeed  get=0x04D86400  set=0x04D86480
  virtualMouse  get=0x04D8D1A0
  stickAction  get=0x04D92750  set=0x09E33C58
  leftButtonAction  get=0x04D92770  set=0x09E33A18
  rightButtonAction  get=0x04D9DCA0  set=0x09E33B70
  middleButtonAction  get=0x04DA2F20  set=0x09E33AC0
  forwardButtonAction  get=0x04DA2F00  set=0x09E33960
  backButtonAction  get=0x04DA2EE0  set=0x09E337A0
  scrollWheelAction  get=0x04DA2F40  set=0x09E33C28
METHODS:
  RVA=0x09E327D4  token=0x600001B  System.Void OnEnable()
  RVA=0x09E32538  token=0x600001C  System.Void OnDisable()
  RVA=0x09E33290  token=0x600001D  System.Void _TryFindCanvas()
  RVA=0x09E33098  token=0x600001E  System.Void _TryEnableHardwareCursor()
  RVA=0x09E32F98  token=0x600001F  System.Void _SyncScale()
  RVA=0x09E332EC  token=0x6000020  System.Void _UpdateMotion()
  RVA=0x09E32C04  token=0x6000021  System.Void _OnButtonActionTriggered(UnityEngine.InputSystem.InputAction.CallbackContext context)
  RVA=0x09E32E34  token=0x6000022  System.Void _SetActionCallback(UnityEngine.InputSystem.InputActionProperty field, System.Action<UnityEngine.InputSystem.InputAction.CallbackContext> callback, System.Boolean install)
  RVA=0x09E32EA0  token=0x6000023  System.Void _SetAction(UnityEngine.InputSystem.InputActionProperty& field, UnityEngine.InputSystem.InputActionProperty value)
  RVA=0x09E32BFC  token=0x6000024  System.Void _OnAfterInputUpdate()
  RVA=0x04D77730  token=0x6000025  System.Void .ctor()
END_CLASS

