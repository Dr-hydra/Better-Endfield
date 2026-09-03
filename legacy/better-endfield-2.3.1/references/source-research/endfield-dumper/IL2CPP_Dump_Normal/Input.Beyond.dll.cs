// ========================================================
// Dumped by @desirepro
// Assembly: Input.Beyond.dll
// Classes:  80
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000004  // size: 0x14
    public sealed struct CursorMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.InputSystem.UI.DebugVirtualMouseInput.CursorMode SoftwareCursor;  // const
        public static UnityEngine.InputSystem.UI.DebugVirtualMouseInput.CursorMode HardwareCursorIfAvailable;  // const

    }

    // TypeToken: 0x2000014  // size: 0x30
    public class Binding
    {
        // Fields
        public Beyond.Input.InputBindingInfo info;  // 0x10
        public System.Single pressDownTime;  // 0x18
        public System.Boolean isUsedForBlock;  // 0x1c
        public System.Boolean isCommonOpenUIAction;  // 0x1d
        public System.Int32 longPressTimerId;  // 0x20
        public System.Single longPressTime;  // 0x24
        public System.Boolean enableContinuousTrigger;  // 0x28
        public Beyond.Input.InputContinuousTriggerType continuousTriggerType;  // 0x2c

        // Methods
        // RVA: 0x037D0550  token: 0x6000077
        public System.Void .ctor(Beyond.Input.GamepadInput input, Beyond.Input.InputBindingInfo info, System.Boolean isUsedForBlock) { }

    }

    // TypeToken: 0x2000015  // size: 0x14
    public sealed struct TriggerType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Input.GamepadCheckerBase.TriggerType NORMAL;  // const
        public static Beyond.Input.GamepadCheckerBase.TriggerType CONTINUOUS;  // const
        public static Beyond.Input.GamepadCheckerBase.TriggerType STOP_CONTINUOUS;  // const

    }

    // TypeToken: 0x2000019  // size: 0x28
    public sealed class <>c__DisplayClass3_0
    {
        // Fields
        public Beyond.Input.GamepadOnLongPressChecker <>4__this;  // 0x10
        public Beyond.Input.GamepadCheckerBase.Binding binding;  // 0x18
        public Beyond.Input.GamepadKeyCode keyCode;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x600008C
        public System.Void .ctor() { }
        // RVA: 0x09E3ABB0  token: 0x600008D
        private System.Void <CreateTriggerBindingAction>b__0() { }

    }

    // TypeToken: 0x200001C  // size: 0x28
    public class CursorSpeedPhase
    {
        // Fields
        public System.Single maxJsValue;  // 0x10
        public System.Single minSpeed;  // 0x14
        public System.Single maxSpeed;  // 0x18
        public System.Single acceleration;  // 0x1c
        public System.Single accelerationSpeed;  // 0x20
        public System.Single maxAcceleration;  // 0x24

        // Methods
        // RVA: 0x041E1670  token: 0x60000BF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001D  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Beyond.Input.HGVirtualMouse.<>c <>9;  // static @ 0x0
        public static System.Predicate<UnityEngine.UI.Selectable> <>9__78_0;  // static @ 0x8

        // Methods
        // RVA: 0x09E3ABDC  token: 0x60000C0
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60000C1
        public System.Void .ctor() { }
        // RVA: 0x09E3AB64  token: 0x60000C2
        private System.Boolean <ChangeHoverState>b__78_0(UnityEngine.UI.Selectable o) { }

    }

    // TypeToken: 0x2000028  // size: 0x20
    public class KeyCaches
    {
        // Fields
        public System.Boolean escapeKeyDown;  // 0x10
        public System.Boolean mouse0KeyDown;  // 0x11
        public System.Boolean mouse1KeyDown;  // 0x12
        public System.Boolean escapeKeyUp;  // 0x13
        public System.Boolean mouse0KeyUp;  // 0x14
        public System.Boolean mouse1KeyUp;  // 0x15
        public System.Boolean escapeKey;  // 0x16
        public System.Boolean mouse0Key;  // 0x17
        public System.Boolean mouse1Key;  // 0x18

        // Methods
        // RVA: 0x03304DD0  token: 0x60001FF
        public System.Void UpdateKeyStat() { }
        // RVA: 0x041E1670  token: 0x6000200
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000029  // size: 0x14
    public sealed struct KeyCodeState
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Input.InputManager.KeyCodeState Pressed;  // const
        public static Beyond.Input.InputManager.KeyCodeState OnPress;  // const
        public static Beyond.Input.InputManager.KeyCodeState OnRelease;  // const

    }

    // TypeToken: 0x200002A  // size: 0x30
    public class ControllerHintInfo : System.IComparable`1, Beyond.PoolCore.IReusable
    {
        // Fields
        public System.String actionId;  // 0x10
        public System.Int32 priority;  // 0x18
        public Beyond.Input.InputBindingInfo bind;  // 0x20
        public Beyond.Input.IBindingView hintView;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x6000201
        public System.Void .ctor() { }
        // RVA: 0x09E30098  token: 0x6000202
        public System.Void .ctor(Beyond.Input.IBindingView hintView, Beyond.Input.InputBindingInfo bind) { }
        // RVA: 0x09E300A0  token: 0x6000203
        public System.Void .ctor(System.String actionId) { }
        // RVA: 0x09E2FF38  token: 0x6000204
        public System.Void Reset(Beyond.Input.IBindingView newHintView, Beyond.Input.InputBindingInfo newBind) { }
        // RVA: 0x09E2FFE4  token: 0x6000205
        public System.Void Reset(System.String resetActionId) { }
        // RVA: 0x09E2FE1C  token: 0x6000206
        public virtual System.Int32 CompareTo(Beyond.Input.InputManager.ControllerHintInfo other) { }
        // RVA: 0x09E2FE7C  token: 0x6000207
        public virtual System.Void OnAllocate() { }
        // RVA: 0x09E2FEC0  token: 0x6000208
        public virtual System.Void OnRecycle() { }

    }

    // TypeToken: 0x200002B  // size: 0x14
    public sealed struct DelayedComboKind
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Input.InputManager.DelayedComboKind ModifierTapThenTrigger;  // const
        public static Beyond.Input.InputManager.DelayedComboKind TriggerThenModifier;  // const

    }

    // TypeToken: 0x200002C  // size: 0x48
    public class DelayedComboBinding
    {
        // Fields
        public readonly System.String modifierActionId;  // 0x10
        public readonly System.String triggerActionId;  // 0x18
        public readonly Beyond.Input.InputManager.DelayedComboKind kind;  // 0x20
        public readonly System.Single maxModifierHoldTime;  // 0x24
        public readonly System.Action onTriggered;  // 0x28
        private Beyond.Input.GamepadKeyCode modifierKey;  // 0x30
        private Beyond.Input.GamepadKeyCode triggerKey;  // 0x34
        private System.Single modifierHoldTime;  // 0x38
        private System.Int32 modifierReleaseFrame;  // 0x3c
        private System.Boolean <triggerJustPressedLastFrame>k__BackingField;  // 0x40

        // Properties
        System.Boolean triggerJustPressedLastFrame { get; /* RVA: 0x02D480A0 */ set; /* RVA: 0x02D480B0 */ }

        // Methods
        // RVA: 0x04490470  token: 0x600020B
        private System.Void .ctor(System.String modifierActionId, System.String triggerActionId, Beyond.Input.InputManager.DelayedComboKind kind, System.Single maxModifierHoldTime, System.Action onTriggered) { }

    }

    // TypeToken: 0x200002D  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Beyond.Input.InputManager.<>c <>9;  // static @ 0x0
        public static System.Action <>9__100_0;  // static @ 0x8
        public static System.Action <>9__239_0;  // static @ 0x10
        public static System.Text.RegularExpressions.MatchEvaluator <>9__297_0;  // static @ 0x18

        // Methods
        // RVA: 0x04D4A230  token: 0x600020C
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600020D
        public System.Void .ctor() { }
        // RVA: 0x041E1670  token: 0x600020E
        private System.Void <_InitDeviceChangeBinding>b__100_0() { }
        // RVA: 0x041E1670  token: 0x600020F
        private System.Void <_CreateVirtualMouse>b__239_0() { }
        // RVA: 0x02D480C0  token: 0x6000210
        private System.String <ParseTextActionId>b__297_0(System.Text.RegularExpressions.Match match) { }

    }

    // TypeToken: 0x2000034  // size: 0x30
    public class Binding
    {
        // Fields
        public Beyond.Input.InputBindingInfo info;  // 0x10
        public System.Single pressDownTime;  // 0x18
        public System.Boolean isUsedForBlock;  // 0x1c
        public System.Int32 longPressTimerId;  // 0x20
        public System.Single longPressTime;  // 0x24
        public System.Boolean enableContinuousTrigger;  // 0x28
        public System.Boolean isCommonOpenUIAction;  // 0x29
        public Beyond.Input.InputContinuousTriggerType continuousTriggerType;  // 0x2c

        // Methods
        // RVA: 0x037D0D30  token: 0x6000220
        public System.Void .ctor(Beyond.Input.KeyboardInput input, Beyond.Input.InputBindingInfo info, System.Boolean isUsedForBlock) { }

    }

    // TypeToken: 0x2000035  // size: 0x14
    public sealed struct TriggerType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Input.KeyboardCheckerBase.TriggerType NORMAL;  // const
        public static Beyond.Input.KeyboardCheckerBase.TriggerType CONTINUOUS;  // const
        public static Beyond.Input.KeyboardCheckerBase.TriggerType STOP_CONTINUOUS;  // const

    }

    // TypeToken: 0x2000039  // size: 0x30
    public sealed class <>c__DisplayClass4_0
    {
        // Fields
        public System.ValueTuple<Beyond.Input.KeyboardKeyCode,System.Collections.Generic.HashSet<Beyond.Input.KeyboardCheckerBase.Binding>> key;  // 0x10
        public Beyond.Input.KeyboardCheckerBase.Binding targetBinding;  // 0x20
        public Beyond.Input.KeyboardOnLongPressChecker <>4__this;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x6000238
        public System.Void .ctor() { }
        // RVA: 0x09E40C1C  token: 0x6000239
        private System.Void <CheckKeyboardInput>b__0() { }

    }

    // TypeToken: 0x200004C  // size: 0x30
    public sealed struct __StaticArrayInitTypeSize=32
    {
    }

    // TypeToken: 0x200004B  // size: 0x10
    public sealed class <PrivateImplementationDetails>
    {
        // Fields
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=32 E680B0AF74F1D11AFCE59DFB222E2F946D48C66AE24640EE35784361EEA2E0C4;  // static @ 0x0

    }

namespace Beyond.Input
{

    // TypeToken: 0x200000A  // size: 0x58
    public class ControllerNaviManager
    {
        // Fields
        public System.Collections.Generic.LinkedList<UnityEngine.UI.SelectableNaviGroup> layerStackList;  // 0x10
        public UnityEngine.UI.Selectable curTarget;  // 0x18
        private readonly System.Collections.Generic.HashSet<System.String> m_naviLockSet;  // 0x20
        private System.Int32 m_targetInputBindingGroupId;  // 0x28
        private System.Int32 m_naviInputBindingGroupId;  // 0x2c
        private UnityEngine.UI.Selectable m_lastEnableNaviTarget;  // 0x30
        private UnityEngine.UI.NavigationBindingType <navigationBindingType>k__BackingField;  // 0x38
        private System.Collections.Generic.Dictionary<UnityEngine.UI.NavigationBindingType,System.Int32> m_naviBindingGroupIds;  // 0x40
        private Beyond.Input.InputManager m_inputManager;  // 0x48
        private static System.Single SCORE_OFFSET;  // const
        private System.Boolean <forceEnableNaviForGuide>k__BackingField;  // 0x50

        // Properties
        UnityEngine.UI.NavigationBindingType navigationBindingType { get; /* RVA: 0x04D863B0 */ set; /* RVA: 0x04D86430 */ }
        System.Boolean forceEnableNaviForGuide { get; /* RVA: 0x04D86800 */ set; /* RVA: 0x04D86840 */ }

        // Methods
        // RVA: 0x044BBBD0  token: 0x600002D
        public System.Void .ctor(Beyond.Input.InputManager inputManager) { }
        // RVA: 0x044BCD10  token: 0x600002E
        public System.Void SetNaviBindingType(UnityEngine.UI.NavigationBindingType bindingType) { }
        // RVA: 0x03304A00  token: 0x600002F
        public System.Void Tick() { }
        // RVA: 0x037D1060  token: 0x6000030
        private System.Boolean _RemoveLayer(UnityEngine.UI.SelectableNaviGroup group, UnityEngine.UI.SelectableNaviGroup curLayer) { }
        // RVA: 0x037D1460  token: 0x6000031
        private System.Void _RemoveLayerFromStack(UnityEngine.UI.SelectableNaviGroup group, System.Boolean silentMode) { }
        // RVA: 0x039D0480  token: 0x6000032
        private System.Void _SetTarget(UnityEngine.UI.Selectable target) { }
        // RVA: 0x09E31810  token: 0x6000033
        private System.Void _OnLayerSelected(UnityEngine.UI.SelectableNaviGroup topLayer) { }
        // RVA: 0x09E30ABC  token: 0x6000034
        private System.Boolean _DefaultNavigate(UnityEngine.UI.NaviDirection dir) { }
        // RVA: 0x09E31A3C  token: 0x6000035
        private UnityEngine.UI.Selectable _TryFindBestNaviTargetInGroup(UnityEngine.UI.SelectableNaviGroup group, UnityEngine.Vector3 pos, UnityEngine.Vector3 dir, System.Double curMaxScore, System.Double& maxScore) { }
        // RVA: 0x09E31DE0  token: 0x6000036
        private UnityEngine.UI.Selectable _TryFindBestNaviTarget(System.Collections.Generic.HashSet<UnityEngine.UI.Selectable> targets, UnityEngine.Vector3 curPos, UnityEngine.Vector3 dir, UnityEngine.UI.NaviStrategy strategy, System.Double curMaxScore, System.Double& maxScore) { }
        // RVA: 0x09E31608  token: 0x6000037
        private static UnityEngine.Vector3 _GetPointOnRectEdge(UnityEngine.RectTransform rectTransform, UnityEngine.Vector2 dir) { }
        // RVA: 0x09E314CC  token: 0x6000038
        private System.Collections.Generic.LinkedListNode<UnityEngine.UI.SelectableNaviGroup> _GetDummyLayerLastGroupInStack(UnityEngine.UI.SelectableNaviGroup dummyLayer) { }
        // RVA: 0x09E307C8  token: 0x600003B
        public System.Void SetForceEnableNavi(System.Boolean enable) { }
        // RVA: 0x03794810  token: 0x600003C
        public System.Void UpdateNaviInputBindingState() { }
        // RVA: 0x03794A30  token: 0x600003D
        public System.Boolean IsNavigationBindingEnabled() { }
        // RVA: 0x039D0120  token: 0x600003E
        public System.Void TryRemoveLayer(UnityEngine.UI.SelectableNaviGroup group) { }
        // RVA: 0x037D1410  token: 0x600003F
        public System.Boolean IsLayerInStack(UnityEngine.UI.SelectableNaviGroup group) { }
        // RVA: 0x09E30414  token: 0x6000040
        public System.Boolean IsTopLayer(UnityEngine.UI.SelectableNaviGroup group) { }
        // RVA: 0x044F3C80  token: 0x6000041
        public System.Void ChangeAndTrySetNaviBindingType(UnityEngine.UI.SelectableNaviGroup naviGroup, UnityEngine.UI.NavigationBindingType naviBindingType) { }
        // RVA: 0x039CFAE0  token: 0x6000042
        public System.Void TryAutoSetTarget() { }
        // RVA: 0x09E302CC  token: 0x6000043
        public UnityEngine.UI.SelectableNaviGroup GetTopDummyLayerGroup() { }
        // RVA: 0x09E30918  token: 0x6000044
        public System.Void SetTarget(UnityEngine.UI.Selectable target) { }
        // RVA: 0x09E30708  token: 0x6000045
        public System.Void ResetStateForUIDispose() { }
        // RVA: 0x04B6AF20  token: 0x6000046
        public System.Void SetTargetInSilentModeIfNecessary(UnityEngine.UI.Selectable selectable, UnityEngine.UI.SelectableNaviGroup needTopDummyGroup) { }
        // RVA: 0x09E300A8  token: 0x6000047
        public System.Void ClearTargetIfNecessary(UnityEngine.UI.SelectableNaviGroup needTopDummyGroup) { }
        // RVA: 0x09E3084C  token: 0x6000048
        public System.Void SetTargetInSilentModeIfNecessary(UnityEngine.UI.SelectableNaviGroup naviGroup, UnityEngine.UI.Selectable selectable, UnityEngine.UI.SelectableNaviGroup needTopDummyGroup) { }
        // RVA: 0x09E30388  token: 0x6000049
        public System.Boolean IsNaviTarget(UnityEngine.UI.Selectable target) { }
        // RVA: 0x09E30178  token: 0x600004A
        public UnityEngine.Vector3 GetNaviVector(UnityEngine.UI.NaviDirection dir) { }
        // RVA: 0x09E30510  token: 0x600004B
        public System.Void Navigate(UnityEngine.UI.NaviDirection dir) { }
        // RVA: 0x09E30984  token: 0x600004C
        public System.Void ToggleNavigation(System.String key, System.Boolean active) { }
        // RVA: 0x09E30A7C  token: 0x600004D
        private System.Void <.ctor>b__12_0() { }
        // RVA: 0x09E30AAC  token: 0x600004E
        private System.Void <.ctor>b__12_1() { }
        // RVA: 0x09E30A8C  token: 0x600004F
        private System.Void <.ctor>b__12_2() { }
        // RVA: 0x09E30A9C  token: 0x6000050
        private System.Void <.ctor>b__12_3() { }
        // RVA: 0x09E30A8C  token: 0x6000051
        private System.Void <.ctor>b__12_4() { }
        // RVA: 0x09E30A9C  token: 0x6000052
        private System.Void <.ctor>b__12_5() { }
        // RVA: 0x09E30A7C  token: 0x6000053
        private System.Void <.ctor>b__12_6() { }
        // RVA: 0x09E30AAC  token: 0x6000054
        private System.Void <.ctor>b__12_7() { }
        // RVA: 0x09E30A7C  token: 0x6000055
        private System.Void <.ctor>b__12_8() { }
        // RVA: 0x09E30AAC  token: 0x6000056
        private System.Void <.ctor>b__12_9() { }
        // RVA: 0x09E30A8C  token: 0x6000057
        private System.Void <.ctor>b__12_10() { }
        // RVA: 0x09E30A9C  token: 0x6000058
        private System.Void <.ctor>b__12_11() { }
        // RVA: 0x09E30A8C  token: 0x6000059
        private System.Void <.ctor>b__12_12() { }
        // RVA: 0x09E30A9C  token: 0x600005A
        private System.Void <.ctor>b__12_13() { }
        // RVA: 0x09E30A7C  token: 0x600005B
        private System.Void <.ctor>b__12_14() { }
        // RVA: 0x09E30AAC  token: 0x600005C
        private System.Void <.ctor>b__12_15() { }
        // RVA: 0x09E30A7C  token: 0x600005D
        private System.Void <.ctor>b__12_16() { }
        // RVA: 0x09E30AAC  token: 0x600005E
        private System.Void <.ctor>b__12_17() { }
        // RVA: 0x09E30A8C  token: 0x600005F
        private System.Void <.ctor>b__12_18() { }
        // RVA: 0x09E30A9C  token: 0x6000060
        private System.Void <.ctor>b__12_19() { }
        // RVA: 0x09E30A8C  token: 0x6000061
        private System.Void <.ctor>b__12_20() { }
        // RVA: 0x09E30A9C  token: 0x6000062
        private System.Void <.ctor>b__12_21() { }
        // RVA: 0x09E30A7C  token: 0x6000063
        private System.Void <.ctor>b__12_22() { }
        // RVA: 0x09E30AAC  token: 0x6000064
        private System.Void <.ctor>b__12_23() { }
        // RVA: 0x09E30A7C  token: 0x6000065
        private System.Void <.ctor>b__12_24() { }
        // RVA: 0x09E30AAC  token: 0x6000066
        private System.Void <.ctor>b__12_25() { }
        // RVA: 0x09E30A8C  token: 0x6000067
        private System.Void <.ctor>b__12_26() { }
        // RVA: 0x09E30A9C  token: 0x6000068
        private System.Void <.ctor>b__12_27() { }
        // RVA: 0x09E30A8C  token: 0x6000069
        private System.Void <.ctor>b__12_28() { }
        // RVA: 0x09E30A9C  token: 0x600006A
        private System.Void <.ctor>b__12_29() { }
        // RVA: 0x09E30A7C  token: 0x600006B
        private System.Void <.ctor>b__12_30() { }
        // RVA: 0x09E30AAC  token: 0x600006C
        private System.Void <.ctor>b__12_31() { }

    }

    // TypeToken: 0x200000B  // size: 0x14
    public sealed struct ActionOnSetNaviTarget
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Input.ActionOnSetNaviTarget None;  // const
        public static Beyond.Input.ActionOnSetNaviTarget AutoTriggerOnClick;  // const
        public static Beyond.Input.ActionOnSetNaviTarget PressConfirmTriggerOnClick;  // const

    }

    // TypeToken: 0x200000C  // size: 0x10
    public static class ControllerNaviUtils
    {
    }

    // TypeToken: 0x200000D  // size: 0x20
    public class PSTriggerEffectCfg : UnityEngine.MonoBehaviour
    {
        // Fields
        public System.Collections.Generic.List<Plugins.LibScePad.TriggerEffectCommandUnion> commands;  // 0x18

        // Methods
        // RVA: 0x05393520  token: 0x600006D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000E  // size: 0x14
    public sealed struct KeyboardKeyCode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Input.KeyboardKeyCode None;  // const
        public static Beyond.Input.KeyboardKeyCode Backspace;  // const
        public static Beyond.Input.KeyboardKeyCode Tab;  // const
        public static Beyond.Input.KeyboardKeyCode Clear;  // const
        public static Beyond.Input.KeyboardKeyCode Return;  // const
        public static Beyond.Input.KeyboardKeyCode Pause;  // const
        public static Beyond.Input.KeyboardKeyCode EscapeOnly;  // const
        public static Beyond.Input.KeyboardKeyCode Space;  // const
        public static Beyond.Input.KeyboardKeyCode Exclaim;  // const
        public static Beyond.Input.KeyboardKeyCode DoubleQuote;  // const
        public static Beyond.Input.KeyboardKeyCode Hash;  // const
        public static Beyond.Input.KeyboardKeyCode Dollar;  // const
        public static Beyond.Input.KeyboardKeyCode Percent;  // const
        public static Beyond.Input.KeyboardKeyCode Ampersand;  // const
        public static Beyond.Input.KeyboardKeyCode Quote;  // const
        public static Beyond.Input.KeyboardKeyCode LeftParen;  // const
        public static Beyond.Input.KeyboardKeyCode RightParen;  // const
        public static Beyond.Input.KeyboardKeyCode Asterisk;  // const
        public static Beyond.Input.KeyboardKeyCode Plus;  // const
        public static Beyond.Input.KeyboardKeyCode Comma;  // const
        public static Beyond.Input.KeyboardKeyCode Minus;  // const
        public static Beyond.Input.KeyboardKeyCode Period;  // const
        public static Beyond.Input.KeyboardKeyCode Slash;  // const
        public static Beyond.Input.KeyboardKeyCode Alpha0;  // const
        public static Beyond.Input.KeyboardKeyCode Alpha1;  // const
        public static Beyond.Input.KeyboardKeyCode Alpha2;  // const
        public static Beyond.Input.KeyboardKeyCode Alpha3;  // const
        public static Beyond.Input.KeyboardKeyCode Alpha4;  // const
        public static Beyond.Input.KeyboardKeyCode Alpha5;  // const
        public static Beyond.Input.KeyboardKeyCode Alpha6;  // const
        public static Beyond.Input.KeyboardKeyCode Alpha7;  // const
        public static Beyond.Input.KeyboardKeyCode Alpha8;  // const
        public static Beyond.Input.KeyboardKeyCode Alpha9;  // const
        public static Beyond.Input.KeyboardKeyCode Colon;  // const
        public static Beyond.Input.KeyboardKeyCode Semicolon;  // const
        public static Beyond.Input.KeyboardKeyCode Less;  // const
        public static Beyond.Input.KeyboardKeyCode Equals;  // const
        public static Beyond.Input.KeyboardKeyCode Greater;  // const
        public static Beyond.Input.KeyboardKeyCode Question;  // const
        public static Beyond.Input.KeyboardKeyCode At;  // const
        public static Beyond.Input.KeyboardKeyCode LeftBracket;  // const
        public static Beyond.Input.KeyboardKeyCode Backslash;  // const
        public static Beyond.Input.KeyboardKeyCode RightBracket;  // const
        public static Beyond.Input.KeyboardKeyCode Caret;  // const
        public static Beyond.Input.KeyboardKeyCode Underscore;  // const
        public static Beyond.Input.KeyboardKeyCode BackQuote;  // const
        public static Beyond.Input.KeyboardKeyCode A;  // const
        public static Beyond.Input.KeyboardKeyCode B;  // const
        public static Beyond.Input.KeyboardKeyCode C;  // const
        public static Beyond.Input.KeyboardKeyCode D;  // const
        public static Beyond.Input.KeyboardKeyCode E;  // const
        public static Beyond.Input.KeyboardKeyCode F;  // const
        public static Beyond.Input.KeyboardKeyCode G;  // const
        public static Beyond.Input.KeyboardKeyCode H;  // const
        public static Beyond.Input.KeyboardKeyCode I;  // const
        public static Beyond.Input.KeyboardKeyCode J;  // const
        public static Beyond.Input.KeyboardKeyCode K;  // const
        public static Beyond.Input.KeyboardKeyCode L;  // const
        public static Beyond.Input.KeyboardKeyCode M;  // const
        public static Beyond.Input.KeyboardKeyCode N;  // const
        public static Beyond.Input.KeyboardKeyCode O;  // const
        public static Beyond.Input.KeyboardKeyCode P;  // const
        public static Beyond.Input.KeyboardKeyCode Q;  // const
        public static Beyond.Input.KeyboardKeyCode R;  // const
        public static Beyond.Input.KeyboardKeyCode S;  // const
        public static Beyond.Input.KeyboardKeyCode T;  // const
        public static Beyond.Input.KeyboardKeyCode U;  // const
        public static Beyond.Input.KeyboardKeyCode V;  // const
        public static Beyond.Input.KeyboardKeyCode W;  // const
        public static Beyond.Input.KeyboardKeyCode X;  // const
        public static Beyond.Input.KeyboardKeyCode Y;  // const
        public static Beyond.Input.KeyboardKeyCode Z;  // const
        public static Beyond.Input.KeyboardKeyCode LeftCurlyBracket;  // const
        public static Beyond.Input.KeyboardKeyCode Pipe;  // const
        public static Beyond.Input.KeyboardKeyCode RightCurlyBracket;  // const
        public static Beyond.Input.KeyboardKeyCode Tilde;  // const
        public static Beyond.Input.KeyboardKeyCode Delete;  // const
        public static Beyond.Input.KeyboardKeyCode Keypad0;  // const
        public static Beyond.Input.KeyboardKeyCode Keypad1;  // const
        public static Beyond.Input.KeyboardKeyCode Keypad2;  // const
        public static Beyond.Input.KeyboardKeyCode Keypad3;  // const
        public static Beyond.Input.KeyboardKeyCode Keypad4;  // const
        public static Beyond.Input.KeyboardKeyCode Keypad5;  // const
        public static Beyond.Input.KeyboardKeyCode Keypad6;  // const
        public static Beyond.Input.KeyboardKeyCode Keypad7;  // const
        public static Beyond.Input.KeyboardKeyCode Keypad8;  // const
        public static Beyond.Input.KeyboardKeyCode Keypad9;  // const
        public static Beyond.Input.KeyboardKeyCode KeypadPeriod;  // const
        public static Beyond.Input.KeyboardKeyCode KeypadDivide;  // const
        public static Beyond.Input.KeyboardKeyCode KeypadMultiply;  // const
        public static Beyond.Input.KeyboardKeyCode KeypadMinus;  // const
        public static Beyond.Input.KeyboardKeyCode KeypadPlus;  // const
        public static Beyond.Input.KeyboardKeyCode KeypadEnter;  // const
        public static Beyond.Input.KeyboardKeyCode KeypadEquals;  // const
        public static Beyond.Input.KeyboardKeyCode UpArrow;  // const
        public static Beyond.Input.KeyboardKeyCode DownArrow;  // const
        public static Beyond.Input.KeyboardKeyCode RightArrow;  // const
        public static Beyond.Input.KeyboardKeyCode LeftArrow;  // const
        public static Beyond.Input.KeyboardKeyCode Insert;  // const
        public static Beyond.Input.KeyboardKeyCode Home;  // const
        public static Beyond.Input.KeyboardKeyCode End;  // const
        public static Beyond.Input.KeyboardKeyCode PageUp;  // const
        public static Beyond.Input.KeyboardKeyCode PageDown;  // const
        public static Beyond.Input.KeyboardKeyCode F1;  // const
        public static Beyond.Input.KeyboardKeyCode F2;  // const
        public static Beyond.Input.KeyboardKeyCode F3;  // const
        public static Beyond.Input.KeyboardKeyCode F4;  // const
        public static Beyond.Input.KeyboardKeyCode F5;  // const
        public static Beyond.Input.KeyboardKeyCode F6;  // const
        public static Beyond.Input.KeyboardKeyCode F7;  // const
        public static Beyond.Input.KeyboardKeyCode F8;  // const
        public static Beyond.Input.KeyboardKeyCode F9;  // const
        public static Beyond.Input.KeyboardKeyCode F10;  // const
        public static Beyond.Input.KeyboardKeyCode F11;  // const
        public static Beyond.Input.KeyboardKeyCode F12;  // const
        public static Beyond.Input.KeyboardKeyCode F13;  // const
        public static Beyond.Input.KeyboardKeyCode F14;  // const
        public static Beyond.Input.KeyboardKeyCode F15;  // const
        public static Beyond.Input.KeyboardKeyCode Numlock;  // const
        public static Beyond.Input.KeyboardKeyCode CapsLock;  // const
        public static Beyond.Input.KeyboardKeyCode ScrollLock;  // const
        public static Beyond.Input.KeyboardKeyCode RightShift;  // const
        public static Beyond.Input.KeyboardKeyCode LeftShift;  // const
        public static Beyond.Input.KeyboardKeyCode RightControl;  // const
        public static Beyond.Input.KeyboardKeyCode LeftControl;  // const
        public static Beyond.Input.KeyboardKeyCode RightAlt;  // const
        public static Beyond.Input.KeyboardKeyCode LeftAlt;  // const
        public static Beyond.Input.KeyboardKeyCode RightApple;  // const
        public static Beyond.Input.KeyboardKeyCode RightCommand;  // const
        public static Beyond.Input.KeyboardKeyCode RightMeta;  // const
        public static Beyond.Input.KeyboardKeyCode LeftApple;  // const
        public static Beyond.Input.KeyboardKeyCode LeftCommand;  // const
        public static Beyond.Input.KeyboardKeyCode LeftMeta;  // const
        public static Beyond.Input.KeyboardKeyCode LeftWindows;  // const
        public static Beyond.Input.KeyboardKeyCode RightWindows;  // const
        public static Beyond.Input.KeyboardKeyCode AltGr;  // const
        public static Beyond.Input.KeyboardKeyCode Help;  // const
        public static Beyond.Input.KeyboardKeyCode Print;  // const
        public static Beyond.Input.KeyboardKeyCode SysReq;  // const
        public static Beyond.Input.KeyboardKeyCode Break;  // const
        public static Beyond.Input.KeyboardKeyCode Menu;  // const
        public static Beyond.Input.KeyboardKeyCode Mouse0;  // const
        public static Beyond.Input.KeyboardKeyCode Mouse1;  // const
        public static Beyond.Input.KeyboardKeyCode Mouse2;  // const
        public static Beyond.Input.KeyboardKeyCode Mouse3;  // const
        public static Beyond.Input.KeyboardKeyCode Mouse4;  // const
        public static Beyond.Input.KeyboardKeyCode Mouse5;  // const
        public static Beyond.Input.KeyboardKeyCode Mouse6;  // const
        public static Beyond.Input.KeyboardKeyCode EscapeWithRightMouse;  // const

    }

    // TypeToken: 0x200000F  // size: 0x14
    public sealed struct GamepadKeyCode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Input.GamepadKeyCode None;  // const
        public static Beyond.Input.GamepadKeyCode LeftStickX;  // const
        public static Beyond.Input.GamepadKeyCode LeftStickY;  // const
        public static Beyond.Input.GamepadKeyCode LeftStickBtn;  // const
        public static Beyond.Input.GamepadKeyCode RightStickX;  // const
        public static Beyond.Input.GamepadKeyCode RightStickY;  // const
        public static Beyond.Input.GamepadKeyCode RightStickBtn;  // const
        public static Beyond.Input.GamepadKeyCode ArrowUp;  // const
        public static Beyond.Input.GamepadKeyCode ArrowDown;  // const
        public static Beyond.Input.GamepadKeyCode ArrowLeft;  // const
        public static Beyond.Input.GamepadKeyCode ArrowRight;  // const
        public static Beyond.Input.GamepadKeyCode A;  // const
        public static Beyond.Input.GamepadKeyCode B;  // const
        public static Beyond.Input.GamepadKeyCode X;  // const
        public static Beyond.Input.GamepadKeyCode Y;  // const
        public static Beyond.Input.GamepadKeyCode LB;  // const
        public static Beyond.Input.GamepadKeyCode LT;  // const
        public static Beyond.Input.GamepadKeyCode RB;  // const
        public static Beyond.Input.GamepadKeyCode RT;  // const
        public static Beyond.Input.GamepadKeyCode LeftMenuBtn;  // const
        public static Beyond.Input.GamepadKeyCode RightMenuBtn;  // const
        public static Beyond.Input.GamepadKeyCode Home;  // const
        public static Beyond.Input.GamepadKeyCode TouchPanel;  // const
        public static Beyond.Input.GamepadKeyCode LeftStickUp;  // const
        public static Beyond.Input.GamepadKeyCode LeftStickDown;  // const
        public static Beyond.Input.GamepadKeyCode LeftStickLeft;  // const
        public static Beyond.Input.GamepadKeyCode LeftStickRight;  // const
        public static Beyond.Input.GamepadKeyCode RightStickUp;  // const
        public static Beyond.Input.GamepadKeyCode RightStickDown;  // const
        public static Beyond.Input.GamepadKeyCode RightStickLeft;  // const
        public static Beyond.Input.GamepadKeyCode RightStickRight;  // const

    }

    // TypeToken: 0x2000010  // size: 0x14
    public sealed struct InputTimingType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Input.InputTimingType OnPress;  // const
        public static Beyond.Input.InputTimingType OnRelease;  // const
        public static Beyond.Input.InputTimingType OnClick;  // const
        public static Beyond.Input.InputTimingType OnLongPress;  // const

    }

    // TypeToken: 0x2000011  // size: 0x14
    public sealed struct InputContinuousTriggerType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Input.InputContinuousTriggerType NormalDelayAndLoopTime;  // const
        public static Beyond.Input.InputContinuousTriggerType EveryTick;  // const

    }

    // TypeToken: 0x2000012  // size: 0x14
    public sealed struct KeyboardType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Input.KeyboardType None;  // const
        public static Beyond.Input.KeyboardType Default;  // const
        public static Beyond.Input.KeyboardType QWERTZ;  // const
        public static Beyond.Input.KeyboardType AZERTY;  // const

    }

    // TypeToken: 0x2000013  // size: 0x38
    public abstract class GamepadCheckerBase
    {
        // Fields
        protected Beyond.Input.InputManager m_inputManager;  // 0x10
        protected Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>> m_normalBindingInfos;  // 0x18
        protected Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>>> m_modifyBindingInfos;  // 0x20
        protected System.Collections.Generic.HashSet<Beyond.Input.GamepadKeyCode> m_usedKeyCache;  // 0x28
        protected System.Collections.Generic.List<System.ValueTuple<Beyond.Input.GamepadCheckerBase.Binding,Beyond.Input.GamepadCheckerBase.TriggerType>> m_tmpNeedTriggerCallbacks;  // 0x30

        // Properties
        Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>> normalBindingInfos { get; /* RVA: 0x09E340DC */ }
        Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>>> modifyBindingInfos { get; /* RVA: 0x09E34090 */ }

        // Methods
        // RVA: 0x042B6C40  token: 0x6000070
        private System.Void .ctor(Beyond.Input.InputManager inputManager) { }
        // RVA: 0x037D02F0  token: 0x6000071
        public virtual Beyond.Input.GamepadCheckerBase.Binding OnCreateBinding(Beyond.Input.GamepadInput input, Beyond.Input.InputBindingInfo info, System.Boolean isUsedForBlock) { }
        // RVA: 0x037CFCA0  token: 0x6000072
        public virtual Beyond.Input.GamepadCheckerBase.Binding OnDeleteBinding(Beyond.Input.GamepadInput input, Beyond.Input.InputBindingInfo info) { }
        // RVA: 0x037D0470  token: 0x6000073
        protected Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>> GetInputDic(Beyond.Input.GamepadInput input) { }
        // RVA: 0x09E33CEC  token: 0x6000074
        public virtual System.Void CheckGamepadInput() { }
        // RVA: 0x09E33C88  token: 0x6000075
        protected virtual System.Void CheckDic(Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>> dic, Beyond.Input.GamepadKeyCode modifyKey) { }
        // RVA: 0x09E33E30  token: 0x6000076
        public virtual System.Void TriggerActions() { }

    }

    // TypeToken: 0x2000016  // size: 0x48
    public class GamepadChecker : Beyond.Input.GamepadCheckerBase
    {
        // Fields
        private System.Func<Beyond.Input.GamepadKeyCode,System.Boolean> m_checkFunc;  // 0x38
        private System.Boolean <isPressChecker>k__BackingField;  // 0x40

        // Properties
        System.Boolean isPressChecker { get; /* RVA: 0x02D480A0 */ set; /* RVA: 0x02D480B0 */ }

        // Methods
        // RVA: 0x042B62F0  token: 0x600007A
        public System.Void .ctor(System.Func<Beyond.Input.GamepadKeyCode,System.Boolean> checkFunc, System.Boolean isPress, Beyond.Input.InputManager inputManager) { }
        // RVA: 0x09E34128  token: 0x600007B
        protected virtual System.Void CheckDic(Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>> dic, Beyond.Input.GamepadKeyCode modifyKey) { }
        // RVA: 0x09E34794  token: 0x600007C
        public System.Void <>iFixBaseProxy_CheckDic(Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>> P0, Beyond.Input.GamepadKeyCode P1) { }

    }

    // TypeToken: 0x2000017  // size: 0x38
    public class GamepadOnClickChecker : Beyond.Input.GamepadCheckerBase
    {
        // Methods
        // RVA: 0x053973D4  token: 0x600007D
        private System.Void .ctor(Beyond.Input.InputManager inputManager) { }
        // RVA: 0x09E3479C  token: 0x600007E
        protected virtual System.Void CheckDic(Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>> dic, Beyond.Input.GamepadKeyCode modifyKey) { }
        // RVA: 0x09E34F00  token: 0x600007F
        public System.Void ClearKeyPressed(Beyond.Input.GamepadKeyCode keyCode) { }
        // RVA: 0x09E34794  token: 0x6000080
        public System.Void <>iFixBaseProxy_CheckDic(Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>> P0, Beyond.Input.GamepadKeyCode P1) { }

    }

    // TypeToken: 0x2000018  // size: 0x48
    public class GamepadOnLongPressChecker : Beyond.Input.GamepadCheckerBase
    {
        // Fields
        private System.Action<Beyond.Input.GamepadKeyCode> m_onTriggerOnLongPress;  // 0x38
        private System.Collections.Generic.Dictionary<Beyond.Input.GamepadInput,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>> m_bindings;  // 0x40

        // Methods
        // RVA: 0x042B6600  token: 0x6000081
        public System.Void .ctor(System.Action<Beyond.Input.GamepadKeyCode> onTriggerOnLongPress, Beyond.Input.InputManager inputManager) { }
        // RVA: 0x09E35A50  token: 0x6000082
        public System.Action CreateTriggerBindingAction(Beyond.Input.GamepadCheckerBase.Binding binding, Beyond.Input.GamepadKeyCode keyCode) { }
        // RVA: 0x09E35328  token: 0x6000083
        protected virtual System.Void CheckDic(Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>> dic, Beyond.Input.GamepadKeyCode modifyKey) { }
        // RVA: 0x09E35D74  token: 0x6000084
        private System.Void _OnTriggerBinding(Beyond.Input.GamepadCheckerBase.Binding binding, Beyond.Input.GamepadKeyCode keyCode) { }
        // RVA: 0x04611F30  token: 0x6000085
        public virtual Beyond.Input.GamepadCheckerBase.Binding OnCreateBinding(Beyond.Input.GamepadInput input, Beyond.Input.InputBindingInfo info, System.Boolean isUsedForBlock) { }
        // RVA: 0x04611E50  token: 0x6000086
        public virtual Beyond.Input.GamepadCheckerBase.Binding OnDeleteBinding(Beyond.Input.GamepadInput input, Beyond.Input.InputBindingInfo info) { }
        // RVA: 0x037CFEE0  token: 0x6000087
        public System.Void CancelPendingLongPress(Beyond.Input.InputBindingInfo info) { }
        // RVA: 0x09E35B50  token: 0x6000088
        public System.Single GetInputLongPressStartTime(Beyond.Input.GamepadInput input, System.Int32 bindingId) { }
        // RVA: 0x09E34794  token: 0x6000089
        public System.Void <>iFixBaseProxy_CheckDic(Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>> P0, Beyond.Input.GamepadKeyCode P1) { }
        // RVA: 0x09E35D5C  token: 0x600008A
        public Beyond.Input.GamepadCheckerBase.Binding <>iFixBaseProxy_OnCreateBinding(Beyond.Input.GamepadInput P0, Beyond.Input.InputBindingInfo P1, System.Boolean P2) { }
        // RVA: 0x09E35D6C  token: 0x600008B
        public Beyond.Input.GamepadCheckerBase.Binding <>iFixBaseProxy_OnDeleteBinding(Beyond.Input.GamepadInput P0, Beyond.Input.InputBindingInfo P1) { }

    }

    // TypeToken: 0x200001A  // size: 0x48
    public class GamepadOnReleaseChecker : Beyond.Input.GamepadCheckerBase
    {
        // Fields
        private readonly System.Func<Beyond.Input.GamepadKeyCode,System.Boolean> m_checkFunc;  // 0x38
        private readonly System.Collections.Generic.Dictionary<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadKeyCode>> m_modifyBindingPressedDic;  // 0x40

        // Methods
        // RVA: 0x042B6550  token: 0x600008E
        public System.Void .ctor(System.Func<Beyond.Input.GamepadKeyCode,System.Boolean> checkFunc, Beyond.Input.InputManager inputManager) { }
        // RVA: 0x09E368B8  token: 0x600008F
        public virtual System.Void CheckGamepadInput() { }
        // RVA: 0x09E3622C  token: 0x6000090
        protected virtual System.Void CheckDic(Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>> dic, Beyond.Input.GamepadKeyCode modifyKey) { }
        // RVA: 0x09E36E50  token: 0x6000091
        private System.Boolean <CheckGamepadInput>b__3_0(Beyond.Input.GamepadKeyCode keyCode) { }
        // RVA: 0x09E36E78  token: 0x6000092
        public System.Void <>iFixBaseProxy_CheckGamepadInput() { }
        // RVA: 0x09E34794  token: 0x6000093
        public System.Void <>iFixBaseProxy_CheckDic(Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>> P0, Beyond.Input.GamepadKeyCode P1) { }

    }

    // TypeToken: 0x200001B  // size: 0x170
    public class HGVirtualMouse : UnityEngine.MonoBehaviour
    {
        // Fields
        private static System.Single STANDARD_SCREEN_WIDTH;  // const
        public System.Boolean keepMousePosOnEnable;  // 0x18
        private UnityEngine.RectTransform m_CursorTransform;  // 0x20
        private UnityEngine.Animator m_animator;  // 0x28
        public System.Single currentCursorSpeed;  // 0x30
        public System.Single currentCursorAcc;  // 0x34
        public System.Collections.Generic.List<Beyond.Input.HGVirtualMouse.CursorSpeedPhase> speedPhaseList;  // 0x38
        public System.Collections.Generic.List<Beyond.Input.HGVirtualMouse.CursorSpeedPhase> hoverSpeedPhaseList;  // 0x40
        public System.Collections.Generic.List<Beyond.Input.HGVirtualMouse.CursorSpeedPhase> secondHoverSpeedPhaseList;  // 0x48
        private System.Single m_ScrollSpeed;  // 0x50
        private UnityEngine.InputSystem.InputActionProperty m_StickAction;  // 0x58
        private UnityEngine.InputSystem.InputActionProperty m_LeftButtonAction;  // 0x70
        private UnityEngine.InputSystem.InputActionProperty m_MiddleButtonAction;  // 0x88
        private UnityEngine.InputSystem.InputActionProperty m_RightButtonAction;  // 0xa0
        private UnityEngine.InputSystem.InputActionProperty m_ForwardButtonAction;  // 0xb8
        private UnityEngine.InputSystem.InputActionProperty m_BackButtonAction;  // 0xd0
        private UnityEngine.InputSystem.InputActionProperty m_ScrollWheelAction;  // 0xe8
        private UnityEngine.RectTransform _canvasTransform;  // 0x100
        private UnityEngine.InputSystem.Mouse m_VirtualMouse;  // 0x108
        private UnityEngine.InputSystem.Mouse m_SystemMouse;  // 0x110
        private System.Action<UnityEngine.InputSystem.InputAction.CallbackContext> m_ButtonActionTriggeredDelegate;  // 0x118
        private System.Double m_LastTime;  // 0x120
        private UnityEngine.Vector2 m_LastStickValue;  // 0x128
        private System.Single m_lastDisabledTime;  // 0x130
        private System.Single _resetCursorPosMinInterval;  // 0x134
        public System.Int32 leftBtnBindingGroupId;  // 0x138
        public System.Int32 leftBtnPressBindingId;  // 0x13c
        public System.Int32 leftBtnReleaseBindingId;  // 0x140
        private System.Collections.Generic.HashSet<UnityEngine.UI.Selectable> m_hoverTargets;  // 0x148
        private System.Boolean <isHoverOn>k__BackingField;  // 0x150
        private System.Boolean m_usingHoverSpd;  // 0x151
        private System.Int32 m_encounterHoverCount;  // 0x154
        private System.Single m_animatorHoverValue;  // 0x158
        private System.Boolean m_isIdle;  // 0x15c
        private System.Single _hoverAnimationChangeSpeed;  // 0x160
        private System.Boolean <mouseIconVisible>k__BackingField;  // 0x164
        private UnityEngine.CanvasGroup m_iconCanvasGroup;  // 0x168

        // Properties
        UnityEngine.RectTransform cursorTransform { get; /* RVA: 0x09E39298 */ set; /* RVA: 0x09E3974C */ }
        System.Single scrollSpeed { get; /* RVA: 0x09E394C0 */ set; /* RVA: 0x09E39BC4 */ }
        UnityEngine.InputSystem.Mouse virtualMouse { get; /* RVA: 0x09E395F8 */ }
        UnityEngine.InputSystem.InputActionProperty stickAction { get; /* RVA: 0x09E39588 */ set; /* RVA: 0x09E39CB0 */ }
        UnityEngine.InputSystem.InputActionProperty leftButtonAction { get; /* RVA: 0x09E3935C */ set; /* RVA: 0x09E398B4 */ }
        UnityEngine.InputSystem.InputActionProperty rightButtonAction { get; /* RVA: 0x09E39448 */ set; /* RVA: 0x09E39ABC */ }
        UnityEngine.InputSystem.InputActionProperty middleButtonAction { get; /* RVA: 0x09E393D0 */ set; /* RVA: 0x09E399B4 */ }
        UnityEngine.InputSystem.InputActionProperty forwardButtonAction { get; /* RVA: 0x09E392E4 */ set; /* RVA: 0x09E397AC */ }
        UnityEngine.InputSystem.InputActionProperty backButtonAction { get; /* RVA: 0x09E39220 */ set; /* RVA: 0x09E39644 */ }
        UnityEngine.InputSystem.InputActionProperty scrollWheelAction { get; /* RVA: 0x09E3950C */ set; /* RVA: 0x09E39C1C */ }
        System.Boolean isHoverOn { get; /* RVA: 0x04DA2F60 */ set; /* RVA: 0x04DA2F80 */ }
        System.Boolean mouseIconVisible { get; /* RVA: 0x04DA2F70 */ set; /* RVA: 0x04DA2F90 */ }

        // Methods
        // RVA: 0x09E378E0  token: 0x60000A7
        protected System.Void OnEnable() { }
        // RVA: 0x09E37554  token: 0x60000A8
        protected System.Void OnDisable() { }
        // RVA: 0x09E38974  token: 0x60000A9
        private System.Void _UpdateMotion() { }
        // RVA: 0x09E383F0  token: 0x60000AA
        private System.Void _OnButtonActionTriggered(UnityEngine.InputSystem.InputAction.CallbackContext context) { }
        // RVA: 0x09E380F4  token: 0x60000AB
        public System.Void TriggerLeftButton(System.Boolean isPressed) { }
        // RVA: 0x09E37158  token: 0x60000AC
        public System.Void ClearLeftButtonState() { }
        // RVA: 0x09E38614  token: 0x60000AD
        private static System.Void _SetActionCallback(UnityEngine.InputSystem.InputActionProperty field, System.Action<UnityEngine.InputSystem.InputAction.CallbackContext> callback, System.Boolean install) { }
        // RVA: 0x09E386D8  token: 0x60000AE
        private static System.Void _SetAction(UnityEngine.InputSystem.InputActionProperty& field, UnityEngine.InputSystem.InputActionProperty value) { }
        // RVA: 0x09E373DC  token: 0x60000AF
        public System.Void OnAfterInputUpdate() { }
        // RVA: 0x09E38240  token: 0x60000B2
        private System.Void Update() { }
        // RVA: 0x09E36F14  token: 0x60000B3
        public System.Void ChangeHoverState(UnityEngine.UI.Selectable target, System.Boolean isHoverOn) { }
        // RVA: 0x09E38374  token: 0x60000B4
        private System.Void _ChangeHoverState(System.Boolean isHoverOn) { }
        // RVA: 0x09E3724C  token: 0x60000B5
        public UnityEngine.UI.Selectable GetCurHoverSelectable() { }
        // RVA: 0x09E3742C  token: 0x60000B6
        private System.Void OnDestroy() { }
        // RVA: 0x09E3882C  token: 0x60000B7
        private System.Void _SyncScale() { }
        // RVA: 0x09E36E80  token: 0x60000BA
        private System.Void Awake() { }
        // RVA: 0x09E38074  token: 0x60000BB
        public System.Void SetMouseIconVisible(System.Boolean active) { }
        // RVA: 0x040F9790  token: 0x60000BC
        public System.Void .ctor() { }
        // RVA: 0x09E38228  token: 0x60000BD
        private System.Void <OnEnable>b__32_0() { }
        // RVA: 0x09E38234  token: 0x60000BE
        private System.Void <OnEnable>b__32_1() { }

    }

    // TypeToken: 0x200001E  // size: 0x60
    public class InputBindingInfo
    {
        // Fields
        private static System.UInt32 s_nextEnabledPriority;  // static @ 0x0
        private readonly System.Int32 <id>k__BackingField;  // 0x10
        private System.Action m_callback;  // 0x18
        private Beyond.Input.InputBindingGroup <group>k__BackingField;  // 0x20
        public System.String playerActionId;  // 0x28
        private System.Boolean <shouldShowControllerHint>k__BackingField;  // 0x30
        private System.Int32 <controllerHintPriority>k__BackingField;  // 0x34
        private System.Boolean <isCommonOpenUIAction>k__BackingField;  // 0x38
        private System.Boolean <selfEnabled>k__BackingField;  // 0x39
        private System.Boolean <groupEnabled>k__BackingField;  // 0x3a
        private System.Boolean <parentIgnored>k__BackingField;  // 0x3b
        public System.Boolean forceKeyhintToGrayState;  // 0x3c
        private System.UInt32 <enabledPriority>k__BackingField;  // 0x40
        private System.Int32 <actionPriority>k__BackingField;  // 0x44
        private Beyond.Input.IBindingView m_bindingView;  // 0x48
        private System.String m_text;  // 0x50
        public static System.Single s_continuousTriggerFirstDelayTime;  // static @ 0x4
        public static System.Single s_continuousTriggerLoopDelayTime;  // static @ 0x8
        public static System.Single s_continuousTriggerLoopQuickDelayTime;  // static @ 0xc
        private System.Int32 m_continuousTriggerTimerId;  // 0x58

        // Properties
        System.Int32 id { get; /* RVA: 0x02B2E2D0 */ }
        Beyond.Input.InputBindingGroup group { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        System.Boolean shouldShowControllerHint { get; /* RVA: 0x04D865F0 */ set; /* RVA: 0x04D86620 */ }
        System.Int32 controllerHintPriority { get; /* RVA: 0x04D86730 */ set; /* RVA: 0x04D86760 */ }
        System.Boolean isCommonOpenUIAction { get; /* RVA: 0x011F2A70 */ set; /* RVA: 0x011F2A80 */ }
        System.Boolean selfEnabled { get; /* RVA: 0x04D86650 */ set; /* RVA: 0x04D86680 */ }
        System.Boolean groupEnabled { get; /* RVA: 0x04D86660 */ set; /* RVA: 0x04D86690 */ }
        System.Boolean enabled { get; /* RVA: 0x037CE190 */ }
        System.Boolean parentIgnored { get; /* RVA: 0x04D86D10 */ set; /* RVA: 0x04D86D20 */ }
        System.UInt32 enabledPriority { get; /* RVA: 0x04D865E0 */ set; /* RVA: 0x04D86610 */ }
        System.Int32 actionPriority { get; /* RVA: 0x04D86530 */ set; /* RVA: 0x04D86580 */ }
        Beyond.Input.IBindingView bindingView { get; /* RVA: 0x037D5340 */ set; /* RVA: 0x09E3A584 */ }
        System.String text { get; /* RVA: 0x09E3A49C */ set; /* RVA: 0x043EF260 */ }

        // Methods
        // RVA: 0x037D0130  token: 0x60000C3
        private static System.UInt32 _GetNextEnabledPriority() { }
        // RVA: 0x037CE370  token: 0x60000DC
        public System.Void .ctor(System.Int32 id, Beyond.Input.InputBindingGroup group, System.Action callback, System.String playerActionId, Beyond.Input.PlayerActionInfo actionInfo) { }
        // RVA: 0x09E39E70  token: 0x60000DD
        public System.Void ChangeGroup(Beyond.Input.InputBindingGroup newGroup) { }
        // RVA: 0x037D0F80  token: 0x60000DE
        public System.Void SetSelfEnabled(System.Boolean value) { }
        // RVA: 0x037CEA10  token: 0x60000DF
        public System.Void OnGroupEnabledChanged(System.Boolean value, System.Boolean silent) { }
        // RVA: 0x09E39F4C  token: 0x60000E0
        public System.Void SetParentIgnored(System.Boolean value) { }
        // RVA: 0x03629C00  token: 0x60000E1
        public System.Void Trigger() { }
        // RVA: 0x09E39FA8  token: 0x60000E2
        public System.Void StartContinuousTrigger(Beyond.Input.InputContinuousTriggerType continuousTriggerType) { }
        // RVA: 0x09E3A434  token: 0x60000E3
        private System.Void _TryContinuousTrigger() { }
        // RVA: 0x037CF850  token: 0x60000E4
        public System.Void StopContinuousTrigger() { }
        // RVA: 0x03629C40  token: 0x60000E5
        private System.Void _RealTriggerCallback() { }
        // RVA: 0x037D0610  token: 0x60000E6
        public System.Void OnDelete() { }
        // RVA: 0x09E39EF4  token: 0x60000E7
        public System.Object GetCallbackTarget() { }
        // RVA: 0x04D35BB0  token: 0x60000E8
        private static System.Void .cctor() { }
        // RVA: 0x09E3A154  token: 0x60000E9
        private System.Void <StartContinuousTrigger>b__64_0() { }
        // RVA: 0x09E3A248  token: 0x60000EA
        private System.Void <StartContinuousTrigger>b__64_1() { }
        // RVA: 0x09E3A33C  token: 0x60000EB
        private System.Void <StartContinuousTrigger>b__64_2() { }

    }

    // TypeToken: 0x200001F  // size: 0x40
    public class InputBindingGroup
    {
        // Fields
        private readonly System.Int32 <id>k__BackingField;  // 0x10
        private System.Boolean <selfEnabled>k__BackingField;  // 0x14
        private System.Boolean <parentEnabled>k__BackingField;  // 0x15
        private System.Boolean <parentIgnored>k__BackingField;  // 0x16
        private Beyond.Input.InputBindingGroup <parent>k__BackingField;  // 0x18
        private readonly System.Collections.Generic.LinkedList<Beyond.Input.InputBindingGroup> <subGroups>k__BackingField;  // 0x20
        private readonly System.Collections.Generic.LinkedList<Beyond.Input.InputBindingInfo> <subInfos>k__BackingField;  // 0x28
        private Beyond.Input.IBindingView m_groupView;  // 0x30
        private Beyond.Input.IBindingGroupTarget m_groupTarget;  // 0x38

        // Properties
        System.Int32 id { get; /* RVA: 0x02B2E2D0 */ }
        System.Boolean selfEnabled { get; /* RVA: 0x04D8EE50 */ set; /* RVA: 0x04D8EE70 */ }
        System.Boolean parentEnabled { get; /* RVA: 0x04D8EE60 */ set; /* RVA: 0x04D8EE80 */ }
        System.Boolean enabled { get; /* RVA: 0x03794D70 */ }
        System.Boolean parentIgnored { get; /* RVA: 0x04DA2FA0 */ set; /* RVA: 0x04DA2FB0 */ }
        Beyond.Input.InputBindingGroup parent { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.Collections.Generic.LinkedList<Beyond.Input.InputBindingGroup> subGroups { get; /* RVA: 0x04D862C0 */ }
        System.Collections.Generic.LinkedList<Beyond.Input.InputBindingInfo> subInfos { get; /* RVA: 0x04D86240 */ }
        Beyond.Input.IBindingView groupView { get; /* RVA: 0x037D53C0 */ set; /* RVA: 0x03EAD8D0 */ }
        Beyond.Input.IBindingGroupTarget groupTarget { get; /* RVA: 0x09E39E0C */ set; /* RVA: 0x037932F0 */ }

        // Methods
        // RVA: 0x03793360  token: 0x60000FC
        public System.Void .ctor(System.Int32 id, Beyond.Input.InputBindingGroup parent) { }
        // RVA: 0x042B5BC0  token: 0x60000FD
        public System.Void ChangeParent(Beyond.Input.InputBindingGroup newParent) { }
        // RVA: 0x03793CB0  token: 0x60000FE
        public System.Void SetSelfEnabled(System.Boolean value) { }
        // RVA: 0x09E39D8C  token: 0x60000FF
        public System.Void SetParentIgnored(System.Boolean value, System.Boolean silent) { }
        // RVA: 0x03794770  token: 0x6000100
        public System.Void OnParentEnabledChanged(System.Boolean value, System.Boolean silent) { }
        // RVA: 0x037943E0  token: 0x6000101
        private System.Void _OnBindingGroupStateChanged(System.Boolean silent) { }

    }

    // TypeToken: 0x2000020  // size: 0x28
    public class InputBindingGroupMonoTarget : UnityEngine.MonoBehaviour, Beyond.Input.IBindingGroupTarget
    {
        // Fields
        private System.Int32 m_groupId;  // 0x18
        private System.Boolean m_init;  // 0x1c
        private System.Boolean m_internalEnabled;  // 0x1d
        private Beyond.Input.InputBindingGroupMonoTarget <parent>k__BackingField;  // 0x20

        // Properties
        System.Int32 groupId { get; /* RVA: 0x037D42E0 */ }
        System.Boolean groupEnabled { get; /* RVA: 0x037D3480 */ }
        Beyond.Input.InputBindingGroupMonoTarget parent { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        Beyond.Input.IBindingGroupTarget parentTarget { get; /* RVA: 0x09E39D3C */ }
        System.Boolean internalEnabled { get; /* RVA: 0x03C73B10 */ set; /* RVA: 0x037D46C0 */ }

        // Methods
        // RVA: 0x037D4340  token: 0x6000109
        public System.Void Init() { }
        // RVA: 0x037D4790  token: 0x600010A
        public System.Void UpdateState() { }
        // RVA: 0x037D4180  token: 0x600010B
        public System.Void DeleteGroup() { }
        // RVA: 0x037D42A0  token: 0x600010C
        private System.Void Awake() { }
        // RVA: 0x037D4750  token: 0x600010D
        private System.Void OnEnable() { }
        // RVA: 0x037D4710  token: 0x600010E
        private System.Void OnDisable() { }
        // RVA: 0x037D3D00  token: 0x600010F
        private System.Void OnDestroy() { }
        // RVA: 0x044F3520  token: 0x6000110
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000021
    public interface IBindingGroupTarget
    {
        // Properties
        System.Int32 groupId { get; /* RVA: -1  // abstract */ }
        Beyond.Input.IBindingGroupTarget parentTarget { get; /* RVA: -1  // abstract */ }
        System.Boolean groupEnabled { get; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x2000022
    public interface IBindingTarget
    {
        // Properties
        System.Int32 bindingId { get; /* RVA: -1  // abstract */ }
        Beyond.Input.IBindingGroupTarget parentTarget { get; /* RVA: -1  // abstract */ }
        System.Boolean bindingEnabled { get; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x2000023  // size: 0x14
    public sealed struct BindingViewState
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Input.BindingViewState Normal;  // const
        public static Beyond.Input.BindingViewState Disabled;  // const
        public static Beyond.Input.BindingViewState Hide;  // const

    }

    // TypeToken: 0x2000024  // size: 0x14
    public sealed struct BindingViewActionType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Input.BindingViewActionType Default;  // const
        public static Beyond.Input.BindingViewActionType ButtonClick;  // const
        public static Beyond.Input.BindingViewActionType ButtonLongPress;  // const
        public static Beyond.Input.BindingViewActionType ButtonPressStart;  // const
        public static Beyond.Input.BindingViewActionType ButtonPressEnd;  // const
        public static Beyond.Input.BindingViewActionType ButtonDoubleClick;  // const
        public static Beyond.Input.BindingViewActionType SliderAdd;  // const
        public static Beyond.Input.BindingViewActionType SliderReduce;  // const
        public static Beyond.Input.BindingViewActionType SliderStick;  // const

    }

    // TypeToken: 0x2000025
    public interface IBindingView
    {
        // Properties
        System.String bindingViewActionId { get; /* RVA: -1  // abstract */ }
        System.String bindingViewLabelText { get; /* RVA: -1  // abstract */ }
        Beyond.DisposedUnityEvent bindingViewUpdateEvent { get; /* RVA: -1  // abstract */ }
        System.Boolean interactable { get; /* RVA: -1  // abstract */ }
        UnityEngine.RectTransform redDotTrans { get; /* RVA: -1  // abstract */ }
        System.Boolean bindingEnabled { get; /* RVA: -1  // abstract */ }
        Beyond.Input.BindingViewState bindingViewState { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x600011E
        public virtual System.String GetBindingViewActionId(Beyond.Input.BindingViewActionType actionType) { }
        // RVA: -1  // abstract  token: 0x600011F
        public virtual System.Void OnInputKeyDown(System.Boolean isActionTrigger) { }
        // RVA: -1  // abstract  token: 0x6000120
        public virtual System.Void OnInputKeyUp(System.Boolean isActionTrigger) { }

    }

    // TypeToken: 0x2000026  // size: 0x14
    public sealed struct InputSettingLevel
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Beyond.Input.InputSettingLevel Default;  // const
        public static Beyond.Input.InputSettingLevel Custom;  // const
        public static Beyond.Input.InputSettingLevel Pending;  // const

    }

    // TypeToken: 0x2000027  // size: 0x1E0
    public class InputManager : Beyond.Singleton`1, System.IDisposable
    {
        // Fields
        public static System.Single s_longPressTime;  // static @ 0x0
        private static System.Collections.Generic.Dictionary<Beyond.Input.KeyboardKeyCode,System.String> s_keyboardKeyCode2StringDict;  // static @ 0x8
        private static System.Collections.Generic.Dictionary<Beyond.Input.GamepadKeyCode,System.String> s_gamepadKeyCode2StringDict;  // static @ 0x10
        public Beyond.MultiAction onKeyboardTypeChange;  // 0x10
        public System.Boolean enableController;  // 0x18
        public System.Boolean enableMarketingCamera;  // 0x19
        private System.Int32 m_nextInputId;  // 0x1c
        private System.Int32 m_nextGroupId;  // 0x20
        private Beyond.Input.InputBindingGroup m_rootGroup;  // 0x28
        private System.Collections.Generic.Dictionary<System.Int32,Beyond.Input.InputBindingGroup> m_groups;  // 0x30
        private System.Collections.Generic.Dictionary<System.Int32,Beyond.Input.InputBindingInfo> m_bindingInfos;  // 0x38
        private System.Collections.Generic.Dictionary<System.String,System.Collections.Generic.HashSet<System.Int32>> m_action2BindingDic;  // 0x40
        private Beyond.Input.KeyboardChecker m_onPressKeyboardChecker;  // 0x48
        private Beyond.Input.KeyboardChecker m_onReleaseKeyboardChecker;  // 0x50
        private Beyond.Input.KeyboardOnClickChecker m_onClickKeyboardChecker;  // 0x58
        private Beyond.Input.KeyboardOnLongPressChecker m_onLongPressKeyboardChecker;  // 0x60
        private Beyond.Input.GamepadChecker m_onPressGamepadChecker;  // 0x68
        private Beyond.Input.GamepadOnReleaseChecker m_onReleaseGamepadChecker;  // 0x70
        private Beyond.Input.GamepadOnClickChecker m_onClickGamepadChecker;  // 0x78
        private Beyond.Input.GamepadOnLongPressChecker m_onLongPressGamepadChecker;  // 0x80
        private Beyond.Input.RealCursorManager m_realCursorManager;  // 0x88
        public static Beyond.MultiAction<Rewired.Joystick,Beyond.DeviceInfo.ControllerType> onJoystickChanged;  // static @ 0x18
        private Rewired.Joystick <joystick>k__BackingField;  // 0x90
        private Rewired.IGamepadTemplate <gamepadTemplate>k__BackingField;  // 0x98
        private Rewired.ControllerExtensions.IDualShock4Extension <gamepadDualShock4Extension>k__BackingField;  // 0xa0
        private System.Int32 m_touchPanelBtnIndex;  // 0xa8
        private System.Int32 m_leftMenuBtnIndex;  // 0xac
        private Beyond.Input.KeyboardHandler m_keyboardHandler;  // 0xb0
        private Beyond.Input.InputManager.KeyCaches m_keyCaches;  // 0xb8
        private Beyond.TickFunction m_lateTickFunction;  // 0xc0
        public Beyond.MultiAction onInputLateTick;  // 0xc8
        public System.Action beforeCheckInput;  // 0xd0
        public System.Action afterCheckInput;  // 0xd8
        public System.Boolean trimNonTargetInputChecks;  // 0xe0
        public System.Boolean commonOpenUIActionTriggered;  // 0xe1
        public System.Boolean disableChangeInputDeviceCheck;  // 0xe2
        public System.Boolean needProcessTryChange;  // 0xe3
        private System.Boolean <inChangingInputDevice>k__BackingField;  // 0xe4
        private System.Boolean m_suppressActionsForDeviceSwitch;  // 0xe5
        private System.Int32 m_suppressFramesRemaining;  // 0xe8
        private static System.Int32 DEVICE_SWITCH_SUPPRESS_FRAMES;  // const
        private System.Boolean m_lastAnyOtherDeviceKey;  // 0xec
        private System.Int32 m_controllerForbidClickBindingId;  // 0xf0
        private System.Boolean m_ignoreFirstMouseUpOnFocusBack;  // 0xf4
        private static readonly Beyond.Input.KeyboardKeyCode[] S_CACHED_KEYBOARD_KEY_CODES;  // static @ 0x20
        private static System.Boolean s_autoUpdateInputBindingMask;  // static @ 0x28
        private System.Boolean m_hasFocusedInputFiled;  // 0xf5
        private static System.Single STICK_DEAD_ZONE_MAGNITUDE;  // const
        private static System.Single STICK_MIN_VALUE;  // const
        private static System.Single STICK_DEAD_ZONE_SQRT_MAGNITUDE;  // const
        private Beyond.Resource.IAssetLoader m_assetLoader;  // 0xf8
        private Beyond.SerializeFieldDictionary<System.String,Beyond.Input.PlayerActionInfo> m_defaultSetting;  // 0x100
        private Beyond.SerializeFieldDictionary<System.String,Beyond.Input.PlayerActionInfo> m_customSetting;  // 0x108
        private Beyond.SerializeFieldDictionary<System.String,Beyond.Input.PlayerActionInfo> m_pendingSetting;  // 0x110
        private Beyond.Input.InputRebindConfig m_rebindConfig;  // 0x118
        private System.Collections.Generic.Dictionary<System.String,Beyond.Input.InputRebindActionScopeConfig> m_rebindActionScopeConfigs;  // 0x120
        private System.Collections.Generic.Dictionary<System.String,Beyond.Input.PlayerActionInfo> m_anonymousActionInfos;  // 0x128
        private System.Int32 m_nextAnonymousPlayerActionId;  // 0x130
        private static System.String INPUT_MANAGER_CANVAS_PREFAB_PATH;  // const
        private Beyond.Input.HGVirtualMouse <virtualMouse>k__BackingField;  // 0x138
        private UnityEngine.InputSystem.UI.DebugVirtualMouseInput <debugVirtualMouse>k__BackingField;  // 0x140
        private UnityEngine.Transform <canvasRoot>k__BackingField;  // 0x148
        private UnityEngine.Transform <controllerMask>k__BackingField;  // 0x150
        private System.Boolean <usingVirtualMouse>k__BackingField;  // 0x158
        private System.Int32 m_virtualMouseBindingGroupId;  // 0x15c
        private Beyond.Resource.FAssetProxyHandle m_canvasPrefabHandle;  // 0x160
        private UnityEngine.Transform <customControllerMouseTrans>k__BackingField;  // 0x178
        private UnityEngine.Camera <customControllerMouseUICamera>k__BackingField;  // 0x180
        private System.Collections.Generic.List<Beyond.Input.InputManager.ControllerHintInfo> m_tmpControllerHintInfoList;  // 0x188
        public readonly System.Collections.Generic.HashSet<System.String> guideUseActionIds;  // 0x190
        private static System.Single DEFAULT_VIBRATE_LEVEL;  // const
        private static System.Text.RegularExpressions.Regex s_actIdRegex;  // static @ 0x30
        private static System.String TEXT_SPRITE_FORMAT;  // const
        private static System.Single TEXT_SPRITE_SCALE;  // const
        public static System.String LONG_PRESS_KEY_ICON_POSTFIX;  // const
        private static System.String KEY_HINT_ICON_COMMON_FOLDER;  // const
        private static System.String KEYBOARD_KEY_HINT_ICON_PATH;  // const
        private static System.String GAMEPAD_KEY_HINT_ICON_PATH_PS5;  // const
        private static System.String GAMEPAD_KEY_HINT_ICON_PATH_PS4;  // const
        private static System.String GAMEPAD_KEY_HINT_ICON_PATH_XBOX;  // const
        private static System.String GAMEPAD_KEY_HINT_ICON_PATH_FALLBACK;  // const
        public static System.String s_virtualMouseClickHintActionId;  // static @ 0x38
        public static System.String s_virtualMouseLongPressHintActionId;  // static @ 0x40
        public Beyond.MultiAction<UnityEngine.Vector2> onStartSwipeTouchPanel;  // 0x198
        public Beyond.MultiAction<UnityEngine.Vector2,UnityEngine.Vector2> onSwipeTouchPanel;  // 0x1a0
        public Beyond.MultiAction onEndSwipeTouchPanel;  // 0x1a8
        private System.Boolean <isSwipingTouchPanel>k__BackingField;  // 0x1b0
        private UnityEngine.Vector2 m_lastTouchPanelPos;  // 0x1b4
        public Beyond.Input.ControllerNaviManager controllerNaviManager;  // 0x1c0
        public System.Boolean disableCheckInputForNetMask;  // 0x1c8
        private System.Collections.Generic.List<Beyond.Input.InputManager.DelayedComboBinding> m_delayedCombos;  // 0x1d0
        private System.Boolean m_delayedComboKeyUpdateSubscribed;  // 0x1d8
        private static Unity.Profiling.ProfilerMarker s_updateDelayedCombosMarker;  // static @ 0x48

        // Properties
        System.Boolean isDebugForceShow { get; /* RVA: 0x09E2FB84 */ }
        System.Boolean cursorVisible { get; /* RVA: 0x033FDFC0 */ }
        System.Boolean anyKeyDown { get; /* RVA: 0x037CF460 */ }
        UnityEngine.Vector2 mouseScrollDelta { get; /* RVA: 0x037CF710 */ }
        System.Boolean multiTouchEnabled { get; /* RVA: 0x0429EE70 */ set; /* RVA: 0x046DA760 */ }
        System.Boolean usingController { get; /* RVA: 0x033FD440 */ set; /* RVA: 0x09E2FD80 */ }
        System.Boolean virtualMouseIconVisible { get; /* RVA: 0x09E2FC40 */ }
        Beyond.Input.KeyboardType keyboardType { get; /* RVA: 0x09E2FBE4 */ set; /* RVA: 0x04731010 */ }
        System.Int32 invalidID { get; /* RVA: 0x037CE430 */ }
        Rewired.Joystick joystick { get; /* RVA: 0x04D86220 */ set; /* RVA: 0x06402330 */ }
        Rewired.IGamepadTemplate gamepadTemplate { get; /* RVA: 0x04D85EE0 */ set; /* RVA: 0x06352D0C */ }
        Rewired.ControllerExtensions.IDualShock4Extension gamepadDualShock4Extension { get; /* RVA: 0x04D86210 */ set; /* RVA: 0x05391334 */ }
        System.Collections.Generic.Dictionary<System.String,System.Collections.Generic.HashSet<System.Int32>> action2BindingDic { get; /* RVA: 0x037CE220 */ }
        System.Int32 rootGroupId { get; /* RVA: 0x04D2BAF0 */ }
        System.Boolean inChangingInputDevice { get; /* RVA: 0x04D91930 */ set; /* RVA: 0x04D919A0 */ }
        System.Boolean isInputSuppressedByDeviceSwitch { get; /* RVA: 0x033FE910 */ }
        System.Boolean autoUpdateInputBindingMask { get; /* RVA: 0x03DF9D10 */ set; /* RVA: 0x09E2FCAC */ }
        Beyond.Input.HGVirtualMouse virtualMouse { get; /* RVA: 0x04D8DC90 */ set; /* RVA: 0x05397184 */ }
        UnityEngine.InputSystem.UI.DebugVirtualMouseInput debugVirtualMouse { get; /* RVA: 0x04D8DA60 */ set; /* RVA: 0x06B8B404 */ }
        UnityEngine.Transform canvasRoot { get; /* RVA: 0x04D8DAC0 */ set; /* RVA: 0x06B8B468 */ }
        UnityEngine.Transform controllerMask { get; /* RVA: 0x04D8D890 */ set; /* RVA: 0x06B8B224 */ }
        System.Boolean usingVirtualMouse { get; /* RVA: 0x04D907E0 */ set; /* RVA: 0x04D907F0 */ }
        UnityEngine.Vector3 mousePosition { get; /* RVA: 0x034021D0 */ }
        UnityEngine.Transform customControllerMouseTrans { get; /* RVA: 0x04D8DEE0 */ set; /* RVA: 0x06B8B92C */ }
        UnityEngine.Camera customControllerMouseUICamera { get; /* RVA: 0x04D8DD60 */ set; /* RVA: 0x06B8B774 */ }
        System.Boolean isSwipingTouchPanel { get; /* RVA: 0x04D90970 */ set; /* RVA: 0x04D909A0 */ }

        // Methods
        // RVA: 0x047C6070  token: 0x6000135
        private System.Void .ctor() { }
        // RVA: 0x036E7D40  token: 0x6000136
        public System.Void LateTick(System.Single deltaTime) { }
        // RVA: 0x042B4D70  token: 0x6000137
        private System.Void _Init() { }
        // RVA: 0x09E2C000  token: 0x6000138
        public System.Void Release() { }
        // RVA: 0x09E2D720  token: 0x6000139
        private System.Void _ClearTickFunction() { }
        // RVA: 0x09E2ABCC  token: 0x600013A
        public System.Void OnApplicationQuit() { }
        // RVA: 0x09E2D4A4  token: 0x600013B
        private System.Void _ClearEvents() { }
        // RVA: 0x03304780  token: 0x600013C
        private System.Void _OnAfterInputUpdate() { }
        // RVA: 0x09E2ED94  token: 0x600013D
        private System.Void _OnControllerConnected(Rewired.ControllerStatusChangedEventArgs args) { }
        // RVA: 0x09E2EEB8  token: 0x600013E
        private System.Void _OnControllerDisconnected(Rewired.ControllerStatusChangedEventArgs args) { }
        // RVA: 0x04A318A0  token: 0x600013F
        private System.Void _TryFindGamepad() { }
        // RVA: 0x09E2ECB8  token: 0x6000140
        private System.Boolean _IsValidJoystick(Rewired.Joystick js) { }
        // RVA: 0x09E2F260  token: 0x6000141
        private System.Void _SetJoystick(Rewired.Joystick js) { }
        // RVA: 0x034DEF90  token: 0x6000142
        private System.Void _CheckInput() { }
        // RVA: 0x02D45EA0  token: 0x6000143
        private Beyond.DeviceInfo.ControllerType _GetControllerType(Rewired.Joystick js, System.Boolean& isNonSupportPsController) { }
        // RVA: 0x03A74530  token: 0x6000144
        public System.Void OnApplicationFocus(System.Boolean hasFocus) { }
        // RVA: 0x042B6930  token: 0x6000148
        private System.Void _InitDeviceChangeBinding() { }
        // RVA: 0x03DF9CB0  token: 0x6000149
        private System.Void _RefreshDeviceChangeBinding() { }
        // RVA: 0x034DEB50  token: 0x600014A
        public System.Void CheckUsingController() { }
        // RVA: 0x09E2D44C  token: 0x600014B
        private System.Boolean _CheckAnyKeyForTouchChangeDevice() { }
        // RVA: 0x09E2D1CC  token: 0x600014C
        private System.Boolean _CheckAnyKeyForKeyboardChangeDevice() { }
        // RVA: 0x09E2D048  token: 0x600014D
        private System.Boolean _AnyKeyDownForKeyboardChangeDevice() { }
        // RVA: 0x09E2F920  token: 0x600014E
        private System.Void _TryChangeInputDeviceType(Beyond.DeviceInfo.InputType inputType) { }
        // RVA: 0x09E2FA70  token: 0x600014F
        private System.Void _TryStopDeviceSwitchSuppression() { }
        // RVA: 0x03DF8DC0  token: 0x6000150
        private System.Void _SyncUsingControllerState() { }
        // RVA: 0x09E2FAD4  token: 0x6000153
        private static System.Void _UpdateInputBindingMask(Beyond.DeviceInfo.InputType inputType) { }
        // RVA: 0x09E2F04C  token: 0x6000154
        private static System.Void _SetInputBindingMask(System.String group) { }
        // RVA: 0x09E2CE14  token: 0x6000155
        public System.Void ToggleInputDeviceChangeMode(System.Boolean inChanging) { }
        // RVA: 0x09E2C74C  token: 0x6000156
        public System.Void SetCursorOverrideForDeviceChange(System.Boolean forceShow) { }
        // RVA: 0x09E279BC  token: 0x6000157
        public System.Void ChangeInputTypeAndSyncState(Beyond.DeviceInfo.InputType inputType) { }
        // RVA: 0x03CD4D00  token: 0x6000158
        private System.Void _OnFocusedInputFieldChanged(Beyond.EventData<System.Boolean>& v) { }
        // RVA: 0x042B6810  token: 0x6000159
        private System.Void _CreateTickFunction() { }
        // RVA: 0x037D0760  token: 0x600015A
        private System.Void _RemovePlayerActionCheck(Beyond.Input.InputBindingInfo info) { }
        // RVA: 0x037CE710  token: 0x600015B
        private System.Void _AddPlayerActionCheck(Beyond.Input.InputBindingInfo info) { }
        // RVA: 0x037CE7E0  token: 0x600015C
        private System.Void _AddPlayerActionCheck(Beyond.Input.KeyboardInput input, Beyond.Input.InputBindingInfo info) { }
        // RVA: 0x037D0190  token: 0x600015D
        private System.Void _AddPlayerActionCheck(Beyond.Input.GamepadInput input, Beyond.Input.InputBindingInfo info) { }
        // RVA: 0x037D0830  token: 0x600015E
        private System.Void _RemovePlayerActionCheck(Beyond.Input.KeyboardInput input, Beyond.Input.InputBindingInfo info) { }
        // RVA: 0x037CE900  token: 0x600015F
        private System.Void _RemovePlayerActionCheck(Beyond.Input.GamepadInput input, Beyond.Input.InputBindingInfo info) { }
        // RVA: 0x04774970  token: 0x6000160
        public System.Boolean GetKey(Beyond.Input.KeyboardKeyCode keyCode) { }
        // RVA: 0x09E2A080  token: 0x6000161
        public System.Boolean GetKeyOrDown(Beyond.Input.KeyboardKeyCode keyCode) { }
        // RVA: 0x03305AC0  token: 0x6000162
        public System.Boolean GetKeyDown(Beyond.Input.KeyboardKeyCode keyCode) { }
        // RVA: 0x03305B50  token: 0x6000163
        public System.Boolean GetKeyUp(Beyond.Input.KeyboardKeyCode keyCode) { }
        // RVA: 0x09E29CF8  token: 0x6000164
        public UnityEngine.Vector2 GetGamepadStickValue(System.Boolean isLeft, System.Boolean checkDeadZone) { }
        // RVA: 0x09E28160  token: 0x6000165
        public static System.Boolean CheckGamepadStickInDeadZone(UnityEngine.Vector2 stickValue) { }
        // RVA: 0x09E27BF8  token: 0x6000166
        public static System.Boolean CheckGamepadStickInDeadZone(System.Single stickAxisValue) { }
        // RVA: 0x09E29E14  token: 0x6000167
        public System.Single GetGamepadTriggerValue(System.Boolean isLeft) { }
        // RVA: 0x09E2A0C4  token: 0x6000168
        public System.Boolean GetKey(Beyond.Input.GamepadKeyCode keyCode) { }
        // RVA: 0x09E29F64  token: 0x6000169
        public System.Boolean GetKeyDown(Beyond.Input.GamepadKeyCode keyCode) { }
        // RVA: 0x09E2A0B4  token: 0x600016A
        public System.Boolean GetKeyUp(Beyond.Input.GamepadKeyCode keyCode) { }
        // RVA: 0x09E27608  token: 0x600016B
        public System.Boolean AnyKeyboardKey(System.String[] actionScopes, Beyond.Input.KeyboardKeyCode& keyCode, System.Boolean& isBlackList) { }
        // RVA: 0x09E2D118  token: 0x600016C
        private System.Boolean _AnyKeyboardKey(Beyond.Input.KeyboardKeyCode[] keyCodes, Beyond.Input.KeyboardKeyCode& keyCode) { }
        // RVA: 0x034DECC0  token: 0x600016D
        public System.Boolean AnyGamepadKey() { }
        // RVA: 0x037CF620  token: 0x600016E
        public System.Boolean AnyMouseKeyDown() { }
        // RVA: 0x09E27878  token: 0x600016F
        public System.Boolean AnyMouseKey() { }
        // RVA: 0x09E277F8  token: 0x6000170
        public System.Boolean AnyMouseKeyUp() { }
        // RVA: 0x037CF500  token: 0x6000171
        public static System.Boolean AnyKeyDown() { }
        // RVA: 0x09E278F8  token: 0x6000172
        public static System.Boolean AnyTouchBegan() { }
        // RVA: 0x02D45020  token: 0x6000173
        public static System.Boolean AnyTouchActive() { }
        // RVA: 0x03B72340  token: 0x6000174
        private System.Boolean _CheckGamepadKeyCode(Beyond.Input.GamepadKeyCode keyCode, Beyond.Input.InputManager.KeyCodeState state) { }
        // RVA: 0x09E28284  token: 0x6000175
        public System.Int32 CreateBinding(Beyond.Input.PlayerActionInfo actionInfo, System.Action callback, System.Int32 groupId, System.String& actionId) { }
        // RVA: 0x037D16F0  token: 0x6000176
        public System.Int32 CreateBinding(System.String actionId, System.Action callback, System.Int32 groupId) { }
        // RVA: 0x09E2833C  token: 0x6000177
        public System.Int32 CreateBinding(Beyond.Input.KeyboardKeyCode key, System.String modifyKeys, Beyond.Input.InputTimingType timing, System.Action callback, System.Int32 groupId) { }
        // RVA: 0x037CE450  token: 0x6000178
        public System.Int32 CreateBindingByActionId(System.String actionId, System.Action callback, System.Int32 groupId) { }
        // RVA: 0x039D03E0  token: 0x6000179
        public System.Void DeleteBinding(System.Int32 id) { }
        // RVA: 0x037D0EF0  token: 0x600017A
        public System.Void ToggleBinding(System.Int32 id, System.Boolean active) { }
        // RVA: 0x037CFA30  token: 0x600017B
        public System.Void CancelPendingLongPress(Beyond.Input.InputBindingInfo info) { }
        // RVA: 0x03793000  token: 0x600017C
        public System.Int32 CreateGroup(System.Int32 parentGroupId, Beyond.Input.IBindingGroupTarget bindingGroupTarget) { }
        // RVA: 0x0351DCF0  token: 0x600017D
        public System.Void DeleteGroup(System.Int32 id) { }
        // RVA: 0x0453B150  token: 0x600017E
        public System.Void DeleteInGroup(System.Int32 groupId) { }
        // RVA: 0x03793B30  token: 0x600017F
        public System.Void ToggleGroup(System.Int32 id, System.Boolean active) { }
        // RVA: 0x037CDD30  token: 0x6000180
        public System.Boolean IsBindingEnabled(System.Int32 id, System.Boolean needLogError) { }
        // RVA: 0x02FAA7F0  token: 0x6000181
        public System.Boolean IsGroupParentDisabled(System.Int32 groupId) { }
        // RVA: 0x09E29210  token: 0x6000182
        public System.Void ForceBindingKeyhintToGray(System.Int32 id, System.Boolean grayState) { }
        // RVA: 0x09E2A810  token: 0x6000183
        public System.Boolean IsBindingKeyhintGray(System.Int32 id) { }
        // RVA: 0x03794C00  token: 0x6000184
        public System.Boolean IsGroupEnabled(System.Int32 id) { }
        // RVA: 0x09E2CD58  token: 0x6000185
        public System.Void ToggleDebugCursorMode() { }
        // RVA: 0x03C11A30  token: 0x6000186
        public System.Void ClearAllCursorShowRequests() { }
        // RVA: 0x03A73590  token: 0x6000187
        public System.Void SetCursorShowRequest(System.String key, System.Boolean showCursor) { }
        // RVA: 0x049D6C20  token: 0x6000188
        public System.Void ToggleAllInput(System.Boolean active) { }
        // RVA: 0x09E2C68C  token: 0x6000189
        public System.Void SetBindingView(System.Int32 bindingId, Beyond.Input.IBindingView bindingView) { }
        // RVA: 0x03EAD820  token: 0x600018A
        public System.Void SetGroupView(System.Int32 groupId, Beyond.Input.IBindingView groupView) { }
        // RVA: 0x042B5A20  token: 0x600018B
        public System.Void ChangeParent(System.Boolean isGroup, System.Int32 id, System.Int32 newParentGroupId) { }
        // RVA: 0x042B5940  token: 0x600018C
        public System.Int32 GetGroupParentId(System.Boolean isGroup, System.Int32 id) { }
        // RVA: 0x09E2A5D8  token: 0x600018D
        public System.Void IgnoreBindingGroupParent(System.Int32 groupId, System.Boolean parentIgnored) { }
        // RVA: 0x09E2A6A0  token: 0x600018E
        public System.Void IgnoreBindingParent(System.Int32 bindingId, System.Boolean parentIgnored) { }
        // RVA: 0x035C6510  token: 0x600018F
        public Beyond.Input.KeyboardKeyCode GetActionKeyboardKeyCode(System.String actionId, System.Boolean isModifyKey, System.Boolean isPrimary, Beyond.Input.InputSettingLevel level) { }
        // RVA: 0x043D1B10  token: 0x6000190
        public Beyond.Input.GamepadKeyCode GetActionGamepadKeyCode(System.String actionId, System.Boolean isModifyKey, System.Boolean isPrimary, Beyond.Input.InputSettingLevel level) { }
        // RVA: 0x09E28A0C  token: 0x6000191
        public System.Void DeleteCustomBinding(System.String actionId, Beyond.Input.InputDeviceFlags deviceFlags) { }
        // RVA: 0x03C1A700  token: 0x6000192
        public System.Void DeleteAllCustomBindings(Beyond.Input.InputDeviceFlags deviceFlags) { }
        // RVA: 0x043EF1A0  token: 0x6000193
        public System.Void SetBindingText(System.Int32 id, System.String text) { }
        // RVA: 0x09E29464  token: 0x6000194
        public System.String GetBindingText(System.Int32 id) { }
        // RVA: 0x09E2A550  token: 0x6000195
        public System.Boolean HasBinding(System.Int32 id) { }
        // RVA: 0x03CDE140  token: 0x6000196
        public System.String GetActionText(System.String actionId) { }
        // RVA: 0x03CDE1B0  token: 0x6000197
        public static System.String GetActionText(Beyond.Input.PlayerActionInfo info, System.String actionId) { }
        // RVA: 0x09E27ADC  token: 0x6000198
        public System.Boolean CheckActionKeyCodeConflict(System.String actionId, Beyond.Input.KeyboardKeyCode keyCode, System.Boolean& isPrimary) { }
        // RVA: 0x03792F20  token: 0x6000199
        private System.Void _DeleteGroup(Beyond.Input.InputBindingGroup group, System.Boolean isRootCall) { }
        // RVA: 0x03793D30  token: 0x600019A
        private System.Void _DeleteInGroup(Beyond.Input.InputBindingGroup group) { }
        // RVA: 0x037D0680  token: 0x600019B
        private System.Void _RealRemoveBindingInfo(Beyond.Input.InputBindingInfo info) { }
        // RVA: 0x032FEEA0  token: 0x600019C
        public System.Single GetAxis(System.String name) { }
        // RVA: 0x037D5240  token: 0x600019D
        public System.Void GetBindingInfo(System.Int32 bindingId, System.String& actionId, Beyond.Input.IBindingView& bindingView) { }
        // RVA: 0x037D17D0  token: 0x600019E
        private System.Void _LoadSetting() { }
        // RVA: 0x037D22D0  token: 0x600019F
        public System.Void LoadSetting() { }
        // RVA: 0x037D2230  token: 0x60001A0
        public System.Boolean IsPlayerActionIdValid(System.String id) { }
        // RVA: 0x037CE640  token: 0x60001A1
        public Beyond.Input.PlayerActionInfo GetPlayerActionInfo(System.String id, Beyond.Input.InputSettingLevel level) { }
        // RVA: 0x09E27A74  token: 0x60001A2
        public System.Void ChangePlayerAction(System.String id, Beyond.Input.PlayerActionInfo actionInfo) { }
        // RVA: 0x09E2748C  token: 0x60001A3
        public System.String AddAnonymousPlayerAction(Beyond.Input.PlayerActionInfo actionInfo) { }
        // RVA: 0x04B0B910  token: 0x60001A4
        private System.Void _LoadCustomInputSetting() { }
        // RVA: 0x09E2E744  token: 0x60001A5
        private Beyond.Input.PlayerActionInfo _GetOrCreatePendingActionInfo(System.String actionId) { }
        // RVA: 0x09E287DC  token: 0x60001A6
        public System.Boolean CustomizeActionKey(System.String actionId, Beyond.Input.KeyboardKeyCode key, System.Boolean isPrimary, System.String overrideKeyIconName) { }
        // RVA: 0x09E284F4  token: 0x60001A7
        public System.Boolean CustomizeActionKey(System.String actionId, System.Nullable<Beyond.Input.GamepadKeyCode> modifyKey, System.Nullable<Beyond.Input.GamepadKeyCode> key, System.Boolean isPrimary, System.String overrideKeyIconName) { }
        // RVA: 0x09E28208  token: 0x60001A8
        public System.Void ClearPendingInputSetting() { }
        // RVA: 0x03C194D0  token: 0x60001A9
        public System.Void SaveCustomInputSetting() { }
        // RVA: 0x0479F7E0  token: 0x60001B4
        private System.Void _CreateCanvas() { }
        // RVA: 0x0479FA80  token: 0x60001B5
        private System.Void _CreateDebugVirtualMouse() { }
        // RVA: 0x0479FB20  token: 0x60001B6
        private System.Void _CreateVirtualMouse() { }
        // RVA: 0x03A736A0  token: 0x60001B7
        private System.Void _ToggleVirtualMouse(System.Boolean active, System.Boolean forceUpdate) { }
        // RVA: 0x03A73630  token: 0x60001B8
        private System.Void _CheckVirtualMouseState() { }
        // RVA: 0x03401A40  token: 0x60001BA
        public static UnityEngine.Vector3 GetMousePos() { }
        // RVA: 0x09E2CC48  token: 0x60001BB
        public System.Void SetOnlyVirtualMousePos(UnityEngine.Vector2 newPos) { }
        // RVA: 0x09E2CA5C  token: 0x60001BC
        public static System.Void SetMousePos(UnityEngine.Vector2 newPos, System.Boolean hideVirtualMouse) { }
        // RVA: 0x033FE1F0  token: 0x60001BD
        public static System.Boolean GetMouseButton(System.Int32 button) { }
        // RVA: 0x02D454E0  token: 0x60001BE
        public static System.Boolean IsLeftMouseDown(System.Boolean requireActive) { }
        // RVA: 0x03E50260  token: 0x60001BF
        public static System.Boolean GetMouseButtonDown(System.Int32 button) { }
        // RVA: 0x03E2F900  token: 0x60001C0
        public static System.Boolean GetMouseButtonUp(System.Int32 button) { }
        // RVA: 0x09E2C9CC  token: 0x60001C5
        public System.Void SetCustomControllerMouse(UnityEngine.Transform trans, UnityEngine.Camera uiCamera) { }
        // RVA: 0x09E29A94  token: 0x60001C6
        public System.Collections.Generic.List<Beyond.Input.InputManager.ControllerHintInfo> GetEmptyControllerHintInfoList() { }
        // RVA: 0x09E29608  token: 0x60001C7
        public System.Void GetControllerHintInfos(System.Int32 groupId, System.Boolean ignoreRootEnabled, System.Collections.Generic.List<Beyond.Input.InputManager.ControllerHintInfo>& infoList) { }
        // RVA: 0x09E29558  token: 0x60001C8
        public System.Void GetControllerHintInfos(System.Collections.Generic.List<System.String> actionIdList, System.Collections.Generic.List<Beyond.Input.InputManager.ControllerHintInfo>& infoList) { }
        // RVA: 0x09E2DD40  token: 0x60001C9
        private System.Void _GetControllerHintInfos(Beyond.Input.InputBindingGroup group, System.Boolean ignoreRootEnabled, System.Collections.Generic.List<Beyond.Input.InputManager.ControllerHintInfo>& infoList) { }
        // RVA: 0x09E2DB6C  token: 0x60001CA
        private System.Void _GetControllerHintInfos(System.Collections.Generic.List<System.String> actionIdList, System.Collections.Generic.List<Beyond.Input.InputManager.ControllerHintInfo>& infoList) { }
        // RVA: 0x09E2A1BC  token: 0x60001CB
        public System.Single GetPlayerActionLongPressStartTime(System.String actionId, System.Single& longPressTime) { }
        // RVA: 0x09E292C8  token: 0x60001CC
        public System.Single GetActionBindingLongPressStartTime(System.Int32 bindingId, System.Single& longPressTime) { }
        // RVA: 0x09E27564  token: 0x60001CD
        public System.Void AddGuideUseActionId(System.String actionId) { }
        // RVA: 0x09E2C050  token: 0x60001CE
        public System.Void RemoveGuideUseActionId(System.String actionId) { }
        // RVA: 0x09E2818C  token: 0x60001CF
        public System.Void ClearGuideUseActionIds() { }
        // RVA: 0x033FDC80  token: 0x60001D0
        public System.Boolean IsGuideUseAction(System.String actionId) { }
        // RVA: 0x09E2A764  token: 0x60001D1
        public System.Boolean IsAnyBindingInGroupGuideUse(System.Int32 groupId) { }
        // RVA: 0x09E2E9D0  token: 0x60001D2
        private System.Boolean _IsAnyBindingInGroupGuideUse(Beyond.Input.InputBindingGroup group) { }
        // RVA: 0x09E2CF94  token: 0x60001D3
        public System.Void VibrateController(System.Single time, System.Single level) { }
        // RVA: 0x09E2E4BC  token: 0x60001D4
        private static System.String _GetGamepadKeyIconFolderPath() { }
        // RVA: 0x036D48E0  token: 0x60001D5
        public static System.String GetStringByKeyboardKeyCode(Beyond.Input.KeyboardKeyCode code) { }
        // RVA: 0x09E2A368  token: 0x60001D6
        public static System.String GetStringByGamepadKeyCode(Beyond.Input.GamepadKeyCode code) { }
        // RVA: 0x09E2A0D0  token: 0x60001D7
        public static System.String GetKeyboardIconPath(System.String keyName, System.Boolean isLongPress, System.Boolean isFullPath) { }
        // RVA: 0x09E29F74  token: 0x60001D8
        public static System.String GetKeyIconPath(Beyond.Input.GamepadInput inputInfo, System.Boolean isModifyKey, System.Boolean isFullPath, System.Boolean ignoreOverrideKeyIcon) { }
        // RVA: 0x09E29C48  token: 0x60001D9
        public static System.String GetGamepadKeyIconPath(System.String keyStr, System.Boolean isLongPress, System.Boolean isFullPath) { }
        // RVA: 0x036D41E0  token: 0x60001DA
        public static System.String GetKeyIconPath(Beyond.Input.KeyboardInput inputInfo, System.Boolean isModifyKey, System.Boolean isFullPath) { }
        // RVA: 0x036D4280  token: 0x60001DB
        public static System.String GetKeyIconPath(Beyond.Input.KeyboardInput inputInfo, System.Boolean isModifyKey, System.Boolean isLongPress, System.Boolean isFullPath) { }
        // RVA: 0x036D4410  token: 0x60001DC
        private static System.String _GetKeyIconPath(System.String path, System.String keyStr, System.Boolean isLongPress, System.Boolean isFullPath) { }
        // RVA: 0x0489C040  token: 0x60001DD
        public static System.String ParseTextActionId(System.String text) { }
        // RVA: 0x09E2A8B8  token: 0x60001DE
        public System.Void MoveMouseTo(UnityEngine.RectTransform target, UnityEngine.Camera uiCamera) { }
        // RVA: 0x09E2AB24  token: 0x60001DF
        public System.Void MoveVirtualMouseTo(UnityEngine.RectTransform target, UnityEngine.Camera uiCamera, System.Boolean hideVirtualMouse) { }
        // RVA: 0x09E2CCE4  token: 0x60001E0
        public System.Void SetVirtualMouseIconVisible(System.Boolean active) { }
        // RVA: 0x09E2D790  token: 0x60001E3
        private System.Void _ControllerTouchPanelTick() { }
        // RVA: 0x03798D80  token: 0x60001E4
        public static System.Int32 GetTouchId(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x042EE640  token: 0x60001E5
        public static System.Boolean GetPressPosByTouchId(System.Int32 touchId, UnityEngine.Vector2& position) { }
        // RVA: 0x09E29168  token: 0x60001E6
        public virtual System.Void Dispose() { }
        // RVA: 0x09E2C0F4  token: 0x60001E7
        public static System.Void RemoveUnusedInput() { }
        // RVA: 0x03A73460  token: 0x60001E8
        private System.Void _OnNetMaskChanged(Beyond.EventData<System.Boolean>& v) { }
        // RVA: 0x09E296FC  token: 0x60001E9
        public System.String GetCurJsInfos() { }
        // RVA: 0x09E2A30C  token: 0x60001EA
        public System.String GetRealCursorState() { }
        // RVA: 0x09E2B134  token: 0x60001EB
        public System.String PrintKeycodeInfo(System.String keyCode, System.Boolean checkKeyboard, System.Boolean checkGamepad, System.Boolean isDetailed) { }
        // RVA: 0x09E2BAD0  token: 0x60001EC
        public System.String PrintKeycodeInfo(Beyond.Input.KeyboardKeyCode keyCode, System.Boolean isDetailed) { }
        // RVA: 0x09E2B5B8  token: 0x60001ED
        public System.String PrintKeycodeInfo(Beyond.Input.GamepadKeyCode keyCode, System.Boolean isDetailed) { }
        // RVA: 0x09E2AC1C  token: 0x60001EE
        public System.String PrintActionInfo(System.String actionId) { }
        // RVA: 0x09E2AE94  token: 0x60001EF
        public System.String PrintBindingInfo(Beyond.Input.InputBindingInfo bindingInfo) { }
        // RVA: 0x09E2D944  token: 0x60001F0
        private System.String _GetBindingName(System.Object item) { }
        // RVA: 0x09E2E660  token: 0x60001F1
        private UnityEngine.GameObject _GetInputBindingGroupGameObject(Beyond.Input.InputBindingGroup bindingGroup) { }
        // RVA: 0x09E2E590  token: 0x60001F2
        private UnityEngine.GameObject _GetInputBindingGameObject(Beyond.Input.InputBindingInfo bindingInfo) { }
        // RVA: 0x04490250  token: 0x60001F3
        public Beyond.Input.InputManager.DelayedComboBinding RegisterDelayedCombo(System.String modifierActionId, System.String triggerActionId, Beyond.Input.InputManager.DelayedComboKind kind, System.Single maxModifierHoldTime, System.Action onTriggered) { }
        // RVA: 0x09E2CEFC  token: 0x60001F4
        public System.Void UnregisterDelayedCombo(Beyond.Input.InputManager.DelayedComboBinding binding) { }
        // RVA: 0x09E2EF48  token: 0x60001F5
        private System.Void _OnDelayedComboActionKeyUpdated(Beyond.EventData<System.String>& data) { }
        // RVA: 0x044904D0  token: 0x60001F6
        private System.Void _RefreshDelayedComboKeys(Beyond.Input.InputManager.DelayedComboBinding binding) { }
        // RVA: 0x036E8640  token: 0x60001F7
        private System.Void _UpdateDelayedCombos(System.Single deltaTime) { }
        // RVA: 0x036E9110  token: 0x60001F8
        private System.Void _UpdateModifierTapThenTriggerCombo(Beyond.Input.InputManager.DelayedComboBinding binding, System.Single deltaTime) { }
        // RVA: 0x036E91E0  token: 0x60001F9
        private System.Void _UpdateTriggerThenModifierCombo(Beyond.Input.InputManager.DelayedComboBinding binding) { }
        // RVA: 0x0432BE50  token: 0x60001FA
        private static System.Void .cctor() { }
        // RVA: 0x09E2CEBC  token: 0x60001FB
        private System.Void <_Init>b__68_0(Beyond.Input.KeyboardKeyCode key) { }
        // RVA: 0x09E2CEDC  token: 0x60001FC
        private System.Void <_Init>b__68_1(Beyond.Input.GamepadKeyCode key) { }
        // RVA: 0x03A73530  token: 0x60001FD
        private System.Void <_Init>b__68_2() { }
        // RVA: 0x03A734E0  token: 0x60001FE
        private System.Void <_Init>b__68_3() { }

    }

    // TypeToken: 0x200002E  // size: 0x20
    public class InputRebindConfig : UnityEngine.ScriptableObject
    {
        // Fields
        public static System.String INPUT_REBIND_CONFIG_PATH;  // const
        public Beyond.Input.InputRebindActionScopeConfig[] actionScopeConfigs;  // 0x18

        // Methods
        // RVA: 0x03573370  token: 0x6000211
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002F  // size: 0x28
    public class InputRebindActionScopeConfig
    {
        // Fields
        public System.String name;  // 0x10
        public Beyond.Input.KeyboardKeyCode[] keyboardKeyCodeWhiteList;  // 0x18
        public Beyond.Input.KeyboardKeyCode[] keyboardKeyCodeBlackList;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x6000212
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000030  // size: 0x20
    public class InputSetting : UnityEngine.ScriptableObject
    {
        // Fields
        public static System.String CUSTOM_SETTING_DIRECTORY_NAME;  // const
        public static System.String CUSTOM_SETTING_JSON_FILE_NAME;  // const
        public Beyond.SerializeFieldDictionaryPaired<System.String,Beyond.Input.PlayerActionInfo> actionInfos;  // 0x18

        // Methods
        // RVA: 0x0454B380  token: 0x6000213
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000031  // size: 0x20
    public class InputSettingConfig : UnityEngine.ScriptableObject
    {
        // Fields
        public static System.String INPUT_SETTING_CONFIG_PATH;  // const
        public System.Collections.Generic.List<Beyond.Input.InputSetting> inputSettingList;  // 0x18

        // Methods
        // RVA: 0x04D22DA0  token: 0x6000214
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000032  // size: 0x10
    public static class InputUtils
    {
        // Methods
        // RVA: 0x09E3A6F0  token: 0x6000215
        public static System.Void IgnoreBindingParent(Beyond.Input.IBindingTarget target, System.Boolean active) { }
        // RVA: 0x09E3A644  token: 0x6000216
        public static System.Void IgnoreBindingGroupParent(Beyond.Input.IBindingGroupTarget target, System.Boolean active) { }
        // RVA: 0x09E3A5E8  token: 0x6000217
        public static System.Boolean HasFlag(Beyond.Input.InputDeviceFlags value, Beyond.Input.InputDeviceFlags target) { }

    }

    // TypeToken: 0x2000033  // size: 0x20
    public abstract class KeyboardCheckerBase
    {
        // Fields
        protected Beyond.Input.InputManager m_inputManager;  // 0x10
        protected static System.Int32 CTRL;  // const
        protected static System.Int32 SHIFT;  // const
        protected static System.Int32 ALT;  // const
        protected static System.Int32 MAX_INDEX;  // const
        protected static System.Int32[] s_checkPriority;  // static @ 0x0
        protected System.Collections.Generic.List<Beyond.DynamicFastLookupCollection<Beyond.Input.KeyboardKeyCode,System.Collections.Generic.HashSet<Beyond.Input.KeyboardCheckerBase.Binding>>> m_bindingInfos;  // 0x18

        // Properties
        System.Collections.Generic.List<Beyond.DynamicFastLookupCollection<Beyond.Input.KeyboardKeyCode,System.Collections.Generic.HashSet<Beyond.Input.KeyboardCheckerBase.Binding>>> bindingInfos { get; /* RVA: 0x09E3A824 */ }

        // Methods
        // RVA: 0x042B5820  token: 0x6000219
        protected System.Void .ctor(Beyond.Input.InputManager inputManager) { }
        // RVA: 0x037D0CD0  token: 0x600021A
        public static System.Int32 GetModifyIndex(Beyond.Input.KeyboardInput input) { }
        // RVA: 0x037D0B50  token: 0x600021B
        public virtual Beyond.Input.KeyboardCheckerBase.Binding OnCreateBinding(Beyond.Input.KeyboardInput input, Beyond.Input.InputBindingInfo info, System.Boolean isUsedForBlock) { }
        // RVA: 0x037D08E0  token: 0x600021C
        public virtual Beyond.Input.KeyboardCheckerBase.Binding OnDeleteBinding(Beyond.Input.KeyboardInput input, Beyond.Input.InputBindingInfo info) { }
        // RVA: 0x09E3A7E0  token: 0x600021D
        public virtual System.Void CheckKeyboardInput() { }
        // RVA: 0x04066A20  token: 0x600021E
        public virtual System.Void TriggerActions() { }
        // RVA: 0x04D17B50  token: 0x600021F
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000036  // size: 0x40
    public class KeyboardChecker : Beyond.Input.KeyboardCheckerBase
    {
        // Fields
        private System.Func<Beyond.Input.KeyboardKeyCode,System.Boolean> m_checkFunc;  // 0x20
        private System.Boolean <isPressChecker>k__BackingField;  // 0x28
        private System.Collections.Generic.HashSet<Beyond.Input.KeyboardKeyCode> m_usedKeyCache;  // 0x30
        private System.Collections.Generic.List<System.ValueTuple<Beyond.Input.KeyboardCheckerBase.Binding,Beyond.Input.KeyboardCheckerBase.TriggerType>> m_tmpNeedTriggerCallbacks;  // 0x38

        // Properties
        System.Boolean isPressChecker { get; /* RVA: 0x011F33C0 */ set; /* RVA: 0x011F33D0 */ }

        // Methods
        // RVA: 0x042B56C0  token: 0x6000223
        public System.Void .ctor(System.Func<Beyond.Input.KeyboardKeyCode,System.Boolean> checkFunc, System.Boolean isPress, Beyond.Input.InputManager inputManager) { }
        // RVA: 0x03307E80  token: 0x6000224
        public virtual System.Void CheckKeyboardInput() { }
        // RVA: 0x03629670  token: 0x6000225
        public virtual System.Void TriggerActions() { }
        // RVA: 0x09E3A874  token: 0x6000226
        public System.Void <>iFixBaseProxy_CheckKeyboardInput() { }
        // RVA: 0x09E3A87C  token: 0x6000227
        public System.Void <>iFixBaseProxy_TriggerActions() { }

    }

    // TypeToken: 0x2000037  // size: 0x38
    public class KeyboardOnClickChecker : Beyond.Input.KeyboardCheckerBase
    {
        // Fields
        private System.Collections.Generic.HashSet<Beyond.Input.KeyboardKeyCode> m_usedKeyCache;  // 0x20
        private System.Collections.Generic.List<System.ValueTuple<Beyond.Input.KeyboardCheckerBase.Binding,Beyond.Input.KeyboardCheckerBase.TriggerType>> m_tmpNeedTriggerCallbacks;  // 0x28
        private System.Collections.Generic.List<Beyond.ListSet<Beyond.Input.KeyboardKeyCode>> m_combinationKeyPressedInfo;  // 0x30

        // Methods
        // RVA: 0x042B5DB0  token: 0x6000228
        public System.Void .ctor(Beyond.Input.InputManager inputManager) { }
        // RVA: 0x03306AA0  token: 0x6000229
        public virtual System.Void CheckKeyboardInput() { }
        // RVA: 0x03629260  token: 0x600022A
        public virtual System.Void TriggerActions() { }
        // RVA: 0x09E3A884  token: 0x600022B
        public System.Void ClearKeyPressed(Beyond.Input.KeyboardKeyCode keyCode) { }
        // RVA: 0x09E3A874  token: 0x600022C
        public System.Void <>iFixBaseProxy_CheckKeyboardInput() { }
        // RVA: 0x09E3A87C  token: 0x600022D
        public System.Void <>iFixBaseProxy_TriggerActions() { }

    }

    // TypeToken: 0x2000038  // size: 0x38
    public class KeyboardOnLongPressChecker : Beyond.Input.KeyboardCheckerBase
    {
        // Fields
        private System.Action<Beyond.Input.KeyboardKeyCode> m_onTriggerOnLongPress;  // 0x20
        private System.Collections.Generic.Dictionary<Beyond.Input.KeyboardInput,System.Collections.Generic.HashSet<Beyond.Input.KeyboardCheckerBase.Binding>> m_bindings;  // 0x28
        private System.Collections.Generic.HashSet<Beyond.Input.KeyboardKeyCode> m_usedKeyCache;  // 0x30

        // Methods
        // RVA: 0x042B5FB0  token: 0x600022E
        public System.Void .ctor(System.Action<Beyond.Input.KeyboardKeyCode> onTriggerOnLongPress, Beyond.Input.InputManager inputManager) { }
        // RVA: 0x03304F20  token: 0x600022F
        public virtual System.Void CheckKeyboardInput() { }
        // RVA: 0x09E40240  token: 0x6000230
        private System.Void _OnTriggerBinding(Beyond.Input.KeyboardCheckerBase.Binding binding, Beyond.Input.KeyboardKeyCode keyCode) { }
        // RVA: 0x09E3FF90  token: 0x6000231
        public virtual Beyond.Input.KeyboardCheckerBase.Binding OnCreateBinding(Beyond.Input.KeyboardInput input, Beyond.Input.InputBindingInfo info, System.Boolean isUsedForBlock) { }
        // RVA: 0x09E40108  token: 0x6000232
        public virtual Beyond.Input.KeyboardCheckerBase.Binding OnDeleteBinding(Beyond.Input.KeyboardInput input, Beyond.Input.InputBindingInfo info) { }
        // RVA: 0x037CFA90  token: 0x6000233
        public System.Void CancelPendingLongPress(Beyond.Input.InputBindingInfo info) { }
        // RVA: 0x09E3FD80  token: 0x6000234
        public System.Single GetInputLongPressStartTime(Beyond.Input.KeyboardInput input, System.Int32 bindingId) { }
        // RVA: 0x09E3A874  token: 0x6000235
        public System.Void <>iFixBaseProxy_CheckKeyboardInput() { }
        // RVA: 0x09E40228  token: 0x6000236
        public Beyond.Input.KeyboardCheckerBase.Binding <>iFixBaseProxy_OnCreateBinding(Beyond.Input.KeyboardInput P0, Beyond.Input.InputBindingInfo P1, System.Boolean P2) { }
        // RVA: 0x09E40238  token: 0x6000237
        public Beyond.Input.KeyboardCheckerBase.Binding <>iFixBaseProxy_OnDeleteBinding(Beyond.Input.KeyboardInput P0, Beyond.Input.InputBindingInfo P1) { }

    }

    // TypeToken: 0x200003A  // size: 0x30
    public class KeyboardHandler
    {
        // Fields
        private static System.Collections.Generic.Dictionary<Beyond.Input.KeyboardKeyCode,UnityEngine.InputSystem.Key> s_keyMap;  // static @ 0x0
        private System.Collections.Generic.Dictionary<Beyond.Input.KeyboardKeyCode,System.String> m_localizedKeyName;  // 0x10
        private System.Collections.Generic.Dictionary<System.String,System.String> m_localizedIconName;  // 0x18
        private System.Collections.Generic.Dictionary<Beyond.Input.KeyboardType,Beyond.Input.KeyboardOverrideConfig> m_configs;  // 0x20
        private Beyond.Input.KeyboardType m_keyboardType;  // 0x28

        // Methods
        // RVA: 0x044B64B0  token: 0x600023A
        private static System.Void .cctor() { }
        // RVA: 0x09E3F604  token: 0x600023B
        public static System.Boolean GetKey(Beyond.Input.KeyboardKeyCode keyCode) { }
        // RVA: 0x09E3F474  token: 0x600023C
        public static System.Boolean GetKeyDown(Beyond.Input.KeyboardKeyCode keyCode) { }
        // RVA: 0x09E3F53C  token: 0x600023D
        public static System.Boolean GetKeyUp(Beyond.Input.KeyboardKeyCode keyCode) { }
        // RVA: 0x047310D0  token: 0x600023E
        public System.Void ParseConfig(Beyond.Input.KeyboardLocalizeConfig keyboardConfig) { }
        // RVA: 0x09E3F6CC  token: 0x600023F
        public Beyond.Input.KeyboardType GetKeyboardType() { }
        // RVA: 0x047311B0  token: 0x6000240
        public System.Void SetKeyboardType(Beyond.Input.KeyboardType keyboardType) { }
        // RVA: 0x09E3F314  token: 0x6000241
        public System.String GetEscIconName() { }
        // RVA: 0x09E3F350  token: 0x6000242
        public System.String GetIconNameByKeyName(System.String keyName) { }
        // RVA: 0x09E3F3D4  token: 0x6000243
        public System.String GetIconName(Beyond.Input.KeyboardKeyCode keyCode) { }
        // RVA: 0x0432BD40  token: 0x6000244
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003B  // size: 0x20
    public class KeyboardLocalizeConfig : UnityEngine.ScriptableObject
    {
        // Fields
        public static System.String KEYBOARD_CONFIG_PATH;  // const
        public Beyond.Input.KeyboardOverrideConfig[] keyboardConfigs;  // 0x18

        // Methods
        // RVA: 0x03573370  token: 0x6000245
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003C  // size: 0x20
    public class KeyboardOverrideConfig
    {
        // Fields
        public Beyond.Input.KeyboardType keyboardType;  // 0x10
        public Beyond.Input.KeyboardOverrideIconConfig[] overrideIconConfigs;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000246
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003D  // size: 0x28
    public class KeyboardOverrideIconConfig
    {
        // Fields
        public Beyond.Input.KeyboardKeyCode keyCode;  // 0x10
        public System.String keyName;  // 0x18
        public System.String iconName;  // 0x20

        // Methods
        // RVA: 0x041E1670  token: 0x6000247
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003E  // size: 0x10
    public class PlayerActionIdAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000248
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003F  // size: 0x11
    public sealed struct InputDeviceFlags
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static Beyond.Input.InputDeviceFlags None;  // const
        public static Beyond.Input.InputDeviceFlags Keyboard;  // const
        public static Beyond.Input.InputDeviceFlags Gamepad;  // const
        public static Beyond.Input.InputDeviceFlags All;  // const

    }

    // TypeToken: 0x2000040  // size: 0x58
    public class PlayerActionInfo
    {
        // Fields
        public System.Boolean isCommonOpenUIAction;  // 0x10
        public Beyond.Input.KeyboardInput primaryKeyboardInput;  // 0x18
        public Beyond.Input.GamepadInput primaryGamepadInput;  // 0x20
        public System.Int32 priority;  // 0x28
        public System.Boolean needShowControllerHint;  // 0x2c
        public System.Int32 controllerHintPriority;  // 0x30
        public System.String overrideTextId;  // 0x38
        public System.Boolean needSecond;  // 0x40
        public System.Boolean hideSecondInKeyHint;  // 0x41
        public Beyond.Input.KeyboardInput secondaryKeyboardInput;  // 0x48
        public Beyond.Input.GamepadInput secondaryGamepadInput;  // 0x50

        // Methods
        // RVA: 0x09E40644  token: 0x6000249
        public System.Void ResetHintProority() { }
        // RVA: 0x09E4053C  token: 0x600024A
        public System.Boolean DeviceInputEquals(Beyond.Input.PlayerActionInfo other, Beyond.Input.InputDeviceFlags deviceFlags) { }
        // RVA: 0x09E40460  token: 0x600024B
        public System.Void DeviceInputCopyFrom(Beyond.Input.PlayerActionInfo other, Beyond.Input.InputDeviceFlags deviceFlags) { }
        // RVA: 0x015EFCD0  token: 0x600024C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000041
    public abstract class DeviceInput`1 : System.IEquatable`1
    {
        // Fields
        public Beyond.Input.InputTimingType timingType;  // 0x0
        public System.Boolean blockOtherTiming;  // 0x0
        public System.Single longPressTime;  // 0x0
        public System.Boolean enableContinuousTrigger;  // 0x0
        public Beyond.Input.InputContinuousTriggerType continuousTriggerType;  // 0x0
        public System.String overrideKeyIconName;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600024D
        public virtual System.String GetModifyText() { }
        // RVA: -1  // not resolved  token: 0x600024E
        public virtual System.String GetKeyCodeText() { }
        // RVA: -1  // not resolved  token: 0x600024F
        public virtual System.Int32 GetKeyCodeEnumIntValue() { }
        // RVA: -1  // not resolved  token: 0x6000250
        public virtual System.Void SetKeyCodeEnumByIntValue(System.Int32 value) { }
        // RVA: -1  // not resolved  token: 0x6000251
        public virtual System.Void ClearKeyCode() { }
        // RVA: -1  // not resolved  token: 0x6000252
        public virtual System.Boolean IsValid() { }
        // RVA: -1  // not resolved  token: 0x6000253
        public virtual System.Void CopyFrom(T other) { }
        // RVA: -1  // abstract  token: 0x6000254
        public virtual System.Boolean Equals(T other) { }
        // RVA: -1  // not resolved  token: 0x6000255
        public virtual System.String ToString() { }
        // RVA: -1  // not resolved  token: 0x6000256
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000042  // size: 0x38
    public class KeyboardInput : Beyond.Input.DeviceInput`1
    {
        // Fields
        public Beyond.Input.KeyboardKeyCode key;  // 0x30
        public System.Boolean useCtrl;  // 0x34
        public System.Boolean useShift;  // 0x35
        public System.Boolean useAlt;  // 0x36

        // Properties
        System.String modifyString { get; /* RVA: 0x09E3FCA4 */ }

        // Methods
        // RVA: 0x09E3FB18  token: 0x6000257
        public virtual System.String GetModifyText() { }
        // RVA: 0x09E3F8C8  token: 0x6000258
        public virtual System.String GetKeyCodeText() { }
        // RVA: 0x09E3F87C  token: 0x6000259
        public virtual System.Int32 GetKeyCodeEnumIntValue() { }
        // RVA: 0x09E3FC1C  token: 0x600025A
        public virtual System.Void SetKeyCodeEnumByIntValue(System.Int32 value) { }
        // RVA: 0x09E3F718  token: 0x600025C
        public virtual System.Void ClearKeyCode() { }
        // RVA: 0x04CC5BB0  token: 0x600025D
        public virtual System.Boolean IsValid() { }
        // RVA: 0x09E3F774  token: 0x600025E
        public virtual System.Void CopyFrom(Beyond.Input.KeyboardInput other) { }
        // RVA: 0x09E3F814  token: 0x600025F
        public virtual System.Boolean Equals(Beyond.Input.KeyboardInput other) { }
        // RVA: 0x04DA0C20  token: 0x6000260
        public System.Void .ctor() { }
        // RVA: 0x09E3FC8C  token: 0x6000261
        public System.String <>iFixBaseProxy_GetModifyText() { }
        // RVA: 0x09E3FC84  token: 0x6000262
        public System.String <>iFixBaseProxy_GetKeyCodeText() { }
        // RVA: 0x09E3FC7C  token: 0x6000263
        public System.Int32 <>iFixBaseProxy_GetKeyCodeEnumIntValue() { }
        // RVA: 0x09E3FC9C  token: 0x6000264
        public System.Void <>iFixBaseProxy_SetKeyCodeEnumByIntValue(System.Int32 P0) { }
        // RVA: 0x09E3FC74  token: 0x6000265
        public System.Void <>iFixBaseProxy_ClearKeyCode() { }
        // RVA: 0x09E3FC94  token: 0x6000266
        public System.Boolean <>iFixBaseProxy_IsValid() { }

    }

    // TypeToken: 0x2000043  // size: 0x38
    public class GamepadInput : Beyond.Input.DeviceInput`1
    {
        // Fields
        public Beyond.Input.GamepadKeyCode key;  // 0x30
        public Beyond.Input.GamepadKeyCode modifyKey;  // 0x34

        // Methods
        // RVA: 0x09E3AEEC  token: 0x6000267
        public virtual System.String GetModifyText() { }
        // RVA: 0x09E3AE64  token: 0x6000268
        public virtual System.String GetKeyCodeText() { }
        // RVA: 0x09E3AE18  token: 0x6000269
        public virtual System.Int32 GetKeyCodeEnumIntValue() { }
        // RVA: 0x09E3AFE4  token: 0x600026A
        public virtual System.Void SetKeyCodeEnumByIntValue(System.Int32 value) { }
        // RVA: 0x09E3AD24  token: 0x600026B
        public virtual System.Void ClearKeyCode() { }
        // RVA: 0x09E3AF94  token: 0x600026C
        public virtual System.Boolean IsValid() { }
        // RVA: 0x09E3AD84  token: 0x600026D
        public virtual System.Void CopyFrom(Beyond.Input.GamepadInput other) { }
        // RVA: 0x04CB2580  token: 0x600026E
        public virtual System.Boolean Equals(Beyond.Input.GamepadInput other) { }
        // RVA: 0x04DA0C20  token: 0x600026F
        public System.Void .ctor() { }
        // RVA: 0x09E3B054  token: 0x6000270
        public System.String <>iFixBaseProxy_GetModifyText() { }
        // RVA: 0x09E3B04C  token: 0x6000271
        public System.String <>iFixBaseProxy_GetKeyCodeText() { }
        // RVA: 0x09E3B044  token: 0x6000272
        public System.Int32 <>iFixBaseProxy_GetKeyCodeEnumIntValue() { }
        // RVA: 0x09E3B064  token: 0x6000273
        public System.Void <>iFixBaseProxy_SetKeyCodeEnumByIntValue(System.Int32 P0) { }
        // RVA: 0x09E3B03C  token: 0x6000274
        public System.Void <>iFixBaseProxy_ClearKeyCode() { }
        // RVA: 0x09E3B05C  token: 0x6000275
        public System.Boolean <>iFixBaseProxy_IsValid() { }

    }

    // TypeToken: 0x2000044  // size: 0x20
    public sealed struct CursorRequest
    {
        // Fields
        public System.String key;  // 0x10
        public System.Int32 priority;  // 0x18
        public System.Boolean show;  // 0x1c

        // Methods
        // RVA: 0x09E3AC40  token: 0x6000276
        public virtual System.String ToString() { }
        // RVA: 0x09E3ACD4  token: 0x6000277
        public System.String <>iFixBaseProxy_ToString() { }

    }

    // TypeToken: 0x2000045  // size: 0x10
    public static class CursorPriority
    {
        // Fields
        public static System.Int32 DEVICE_CHANGE_OVERRIDE;  // const
        public static System.Int32 CONTROLLER_HIDE;  // const
        public static System.Int32 EDITOR_DEBUG_FORCE_SHOW;  // const
        public static System.Int32 SHOW_REQUEST;  // const

    }

    // TypeToken: 0x2000046  // size: 0x30
    public class RealCursorManager
    {
        // Fields
        private System.Collections.Generic.Dictionary<System.String,Beyond.Input.CursorRequest> m_requests;  // 0x10
        private Beyond.Input.CursorRequest m_activeHighest;  // 0x18
        private System.Boolean m_isUsingVirtualMouse;  // 0x28

        // Properties
        System.Boolean isDebugForceShow { get; /* RVA: 0x09E40B90 */ }

        // Methods
        // RVA: 0x042B64C0  token: 0x6000279
        public System.Void .ctor() { }
        // RVA: 0x09E40A10  token: 0x600027A
        public System.Boolean HasAnyShowRequestAtPriority(System.Int32 priority) { }
        // RVA: 0x03A73820  token: 0x600027B
        public System.Void SetRequest(System.String key, System.Int32 priority, System.Boolean show) { }
        // RVA: 0x03A73C70  token: 0x600027C
        public System.Void RemoveRequest(System.String key) { }
        // RVA: 0x0485D800  token: 0x600027D
        public System.Void RemoveAllRequestsAtPriority(System.Int32 priority) { }
        // RVA: 0x03DF9C70  token: 0x600027E
        public System.Void SetVirtualMouseActive(System.Boolean active) { }
        // RVA: 0x03A738E0  token: 0x600027F
        public System.Void CalcState(System.Boolean forceUpdate) { }
        // RVA: 0x03A73A90  token: 0x6000280
        private System.Void _RefreshActiveHighest() { }
        // RVA: 0x03A73950  token: 0x6000281
        private System.Void _ToggleCursorInternal(System.Boolean enable, System.Boolean forceUpdate) { }
        // RVA: 0x09E40730  token: 0x6000282
        public System.String DumpState() { }

    }

    // TypeToken: 0x2000047  // size: 0x10
    public class TextIdAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000283
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000048  // size: 0x78
    public class UIEvent : UnityEngine.Events.UnityEvent, Beyond.Input.IBindingTarget
    {
        // Fields
        private System.Boolean m_bindingInit;  // 0x30
        public System.Boolean useBindingAction;  // 0x31
        public System.String playerActionId;  // 0x38
        public Beyond.Input.PlayerActionInfo anonymousPlayerActionInfo;  // 0x40
        public System.String anonymousPlayerActionId;  // 0x48
        private System.Int32 <bindingId>k__BackingField;  // 0x50
        private Beyond.Input.IBindingGroupTarget <parentTarget>k__BackingField;  // 0x58
        private System.Action m_bindAct;  // 0x60
        private System.Action m_onUpdateKeyAct;  // 0x68
        private System.Collections.Generic.List<UnityEngine.Events.UnityAction> m_calls;  // 0x70

        // Properties
        System.Boolean bindingEnabled { get; /* RVA: 0x09E41418 */ }
        System.Int32 bindingId { get; /* RVA: 0x04D86550 */ set; /* RVA: 0x04D865A0 */ }
        Beyond.Input.IBindingGroupTarget parentTarget { get; /* RVA: 0x02E56440 */ set; /* RVA: 0x036E5590 */ }
        System.Int32 count { get; /* RVA: 0x09E414C0 */ }

        // Methods
        // RVA: 0x09E412D4  token: 0x6000289
        private System.Void _CreatePlayerActionBinding() { }
        // RVA: 0x09E40E04  token: 0x600028A
        public System.Void InitBinding(Beyond.Input.IBindingGroupTarget parent, System.Action bindAct, System.Action onUpdateKeyAct) { }
        // RVA: 0x09E40EDC  token: 0x600028B
        public System.Void Invoke() { }
        // RVA: 0x09E41234  token: 0x600028C
        public System.Void UpdateKeys() { }
        // RVA: 0x09E40D98  token: 0x600028D
        public System.Void ChangeBindingPlayerAction(System.String actionId) { }
        // RVA: 0x09E40C48  token: 0x600028F
        public System.Void AddListener(UnityEngine.Events.UnityAction call) { }
        // RVA: 0x09E41164  token: 0x6000290
        public System.Void RemoveListener(UnityEngine.Events.UnityAction call) { }
        // RVA: 0x09E40FAC  token: 0x6000291
        public System.Void RemoveAllListeners() { }
        // RVA: 0x09E41408  token: 0x6000292
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000049
    public class UIEvent`1 : UnityEngine.Events.UnityEvent`1, Beyond.Input.IBindingTarget
    {
        // Fields
        private System.Boolean m_bindingInit;  // 0x0
        public System.Boolean useBindingAction;  // 0x0
        public System.String playerActionId;  // 0x0
        public Beyond.Input.PlayerActionInfo anonymousPlayerActionInfo;  // 0x0
        public System.String anonymousPlayerActionId;  // 0x0
        public System.Boolean useAnonymousAction;  // 0x0
        public System.Boolean tempForceEnable;  // 0x0
        private System.Int32 <bindingId>k__BackingField;  // 0x0
        private Beyond.Input.IBindingGroupTarget <parentTarget>k__BackingField;  // 0x0
        private System.Action m_bindAct;  // 0x0
        private System.Action m_onUpdateKeyAct;  // 0x0
        private System.Collections.Generic.List<UnityEngine.Events.UnityAction<T0>> m_calls;  // 0x0

        // Properties
        System.Boolean bindingEnabled { get; /* RVA: -1  // not resolved */ }
        System.Int32 bindingId { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        Beyond.Input.IBindingGroupTarget parentTarget { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Int32 count { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000298
        private System.Void _CreatePlayerActionBinding() { }
        // RVA: -1  // not resolved  token: 0x6000299
        public System.Void InitBinding(Beyond.Input.IBindingGroupTarget parent, System.Action bindAct, System.Action onUpdateKeyAct) { }
        // RVA: -1  // not resolved  token: 0x600029A
        public System.Void Invoke(T0 arg0) { }
        // RVA: -1  // not resolved  token: 0x600029B
        public Beyond.Input.PlayerActionInfo GetEventPlayerActionInfo() { }
        // RVA: -1  // not resolved  token: 0x600029C
        public System.Void UpdateKeys() { }
        // RVA: -1  // not resolved  token: 0x600029D
        public System.Void ChangeBindingPlayerAction(System.String actionId) { }
        // RVA: -1  // not resolved  token: 0x600029E
        public System.Void StopUseBinding() { }
        // RVA: -1  // not resolved  token: 0x600029F
        public System.String GetActionId() { }
        // RVA: -1  // not resolved  token: 0x60002A1
        public System.Void AddListener(UnityEngine.Events.UnityAction<T0> call) { }
        // RVA: -1  // not resolved  token: 0x60002A2
        public System.Void RemoveListener(UnityEngine.Events.UnityAction<T0> call) { }
        // RVA: -1  // not resolved  token: 0x60002A3
        public System.Void RemoveAllListeners() { }
        // RVA: -1  // not resolved  token: 0x60002A4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004A
    public class UIEvent`2 : UnityEngine.Events.UnityEvent`2, Beyond.Input.IBindingTarget
    {
        // Fields
        private System.Boolean m_bindingInit;  // 0x0
        public System.Boolean useBindingAction;  // 0x0
        public System.String playerActionId;  // 0x0
        public Beyond.Input.PlayerActionInfo anonymousPlayerActionInfo;  // 0x0
        public System.String anonymousPlayerActionId;  // 0x0
        private System.Int32 <bindingId>k__BackingField;  // 0x0
        private Beyond.Input.IBindingGroupTarget <parentTarget>k__BackingField;  // 0x0
        private System.Action m_bindAct;  // 0x0
        private System.Action m_onUpdateKeyAct;  // 0x0
        private System.Collections.Generic.List<UnityEngine.Events.UnityAction<T0,T1>> m_calls;  // 0x0

        // Properties
        System.Boolean bindingEnabled { get; /* RVA: -1  // not resolved */ }
        System.Int32 bindingId { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        Beyond.Input.IBindingGroupTarget parentTarget { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Int32 count { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60002AA
        private System.Void _CreatePlayerActionBinding() { }
        // RVA: -1  // not resolved  token: 0x60002AB
        public System.Void InitBinding(Beyond.Input.IBindingGroupTarget parent, System.Action bindAct, System.Action onUpdateKeyAct) { }
        // RVA: -1  // not resolved  token: 0x60002AC
        public System.Void Invoke(T0 arg0, T1 arg1) { }
        // RVA: -1  // not resolved  token: 0x60002AD
        public System.Void UpdateKeys() { }
        // RVA: -1  // not resolved  token: 0x60002AE
        public System.Void ChangeBindingPlayerAction(System.String actionId) { }
        // RVA: -1  // not resolved  token: 0x60002B0
        public System.Void AddListener(UnityEngine.Events.UnityAction<T0,T1> call) { }
        // RVA: -1  // not resolved  token: 0x60002B1
        public System.Void RemoveListener(UnityEngine.Events.UnityAction<T0,T1> call) { }
        // RVA: -1  // not resolved  token: 0x60002B2
        public System.Void RemoveAllListeners() { }
        // RVA: -1  // not resolved  token: 0x60002B3
        public System.Void .ctor() { }

    }

}

namespace Beyond.SourceGenerator
{

    // TypeToken: 0x2000005  // size: 0x20
    public class DataNAttribute : System.Attribute
    {
        // Fields
        public System.Int32 capacity;  // 0x10
        public System.Int32 tSize;  // 0x14
        public System.Boolean ring;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000026
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000006  // size: 0x10
    public static class AnimatorBlackboardUtility
    {
        // Fields
        private static System.Boolean s_enableFastPathBuffer;  // static @ 0x0

        // Methods
        // RVA: -1  // generic def  token: 0x6000027
        private static System.Void ClearAndEnsureDictionaryCapacity(System.Collections.Generic.Dictionary<TKey,TValue>& dict, System.Int32 capacity) { }

    }

    // TypeToken: 0x2000007  // size: 0x10
    public class AnimatorBlackboardAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000028
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000008  // size: 0x10
    public class CameraControlConfigAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000029
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000009  // size: 0x18
    public class ECSComponentAttribute : System.Attribute
    {
        // Fields
        public System.Runtime.InteropServices.LayoutKind layoutKind;  // 0x10
        public System.Boolean isTag;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x600002A
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

namespace IFix
{

    // TypeToken: 0x200004D  // size: 0x28
    public class ILFixDynamicMethodWrapper
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10
        private System.Int32 methodId;  // 0x18
        private System.Object anonObj;  // 0x20
        public static IFix.ILFixDynamicMethodWrapper[] wrapperArray;  // static @ 0x0

        // Methods
        // RVA: 0x032B2C30  token: 0x60002B4
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj) { }
        // RVA: 0x063632E4  token: 0x60002B5
        public System.Boolean __Gen_Wrap_0(System.Object P0) { }
        // RVA: 0x063621C4  token: 0x60002B6
        public System.UInt32 __Gen_Wrap_1() { }
        // RVA: 0x063633B4  token: 0x60002B7
        public System.Void __Gen_Wrap_2(System.Object P0) { }
        // RVA: 0x06377B18  token: 0x60002B8
        public System.Void __Gen_Wrap_3(System.Object P0, System.Object P1) { }
        // RVA: 0x07199240  token: 0x60002B9
        public System.Void __Gen_Wrap_4(System.Object P0, System.Boolean P1, System.Boolean P2) { }
        // RVA: 0x066C2B40  token: 0x60002BA
        public System.Void __Gen_Wrap_5(System.Object P0, System.Boolean P1) { }
        // RVA: 0x066C44C0  token: 0x60002BB
        public System.Void __Gen_Wrap_6(System.Object P0, System.Int32 P1, System.Boolean P2) { }
        // RVA: 0x066F3838  token: 0x60002BC
        public System.Void __Gen_Wrap_7(System.Object P0, UnityEngine.UI.NavigationBindingType P1) { }
        // RVA: 0x066E0A9C  token: 0x60002BD
        public System.Boolean __Gen_Wrap_8(System.Object P0, System.Int32 P1) { }
        // RVA: 0x066C43C8  token: 0x60002BE
        public System.Void __Gen_Wrap_9(System.Object P0, System.Object P1, System.Boolean P2) { }
        // RVA: 0x07195DFC  token: 0x60002BF
        public System.Boolean __Gen_Wrap_10(System.Object P0, System.Object P1, System.Object P2) { }
        // RVA: 0x071BADB4  token: 0x60002C0
        public System.Int32 __Gen_Wrap_11(System.Object P0, System.Int32 P1, System.Object P2) { }
        // RVA: 0x06360E84  token: 0x60002C1
        public System.Int32 __Gen_Wrap_12(System.Object P0) { }
        // RVA: 0x09E3B710  token: 0x60002C2
        public UnityEngine.Vector3 __Gen_Wrap_13(System.Object P0, UnityEngine.UI.NaviDirection P1) { }
        // RVA: 0x09E3B82C  token: 0x60002C3
        public UnityEngine.Vector3 __Gen_Wrap_14(System.Object P0, UnityEngine.Vector2 P1) { }
        // RVA: 0x09E3B970  token: 0x60002C4
        public UnityEngine.UI.Selectable __Gen_Wrap_15(System.Object P0, System.Object P1, UnityEngine.Vector3 P2, UnityEngine.Vector3 P3, UnityEngine.UI.NaviStrategy P4, System.Double P5, System.Double& P6) { }
        // RVA: 0x09E3BB3C  token: 0x60002C5
        public UnityEngine.UI.Selectable __Gen_Wrap_16(System.Object P0, System.Object P1, UnityEngine.Vector3 P2, UnityEngine.Vector3 P3, System.Double P4, System.Double& P5) { }
        // RVA: 0x069AB898  token: 0x60002C6
        public System.Boolean __Gen_Wrap_17(System.Object P0, UnityEngine.UI.NaviDirection P1) { }
        // RVA: 0x09E3BCF8  token: 0x60002C7
        public System.Collections.Generic.LinkedListNode<UnityEngine.UI.SelectableNaviGroup> __Gen_Wrap_18(System.Object P0, System.Object P1) { }
        // RVA: 0x06366BC4  token: 0x60002C8
        public System.Boolean __Gen_Wrap_19(System.Object P0, System.Object P1) { }
        // RVA: 0x063778A4  token: 0x60002C9
        public System.Void __Gen_Wrap_20(System.Object P0, System.Object P1, UnityEngine.UI.NavigationBindingType P2) { }
        // RVA: 0x09E3BE04  token: 0x60002CA
        public UnityEngine.UI.SelectableNaviGroup __Gen_Wrap_21(System.Object P0) { }
        // RVA: 0x06364290  token: 0x60002CB
        public System.Void __Gen_Wrap_22(System.Object P0, System.Object P1, System.Object P2) { }
        // RVA: 0x06373E64  token: 0x60002CC
        public System.Void __Gen_Wrap_23(System.Object P0, System.Object P1, System.Object P2, System.Object P3) { }
        // RVA: 0x06A5FB54  token: 0x60002CD
        public System.Void __Gen_Wrap_24(System.Object P0, UnityEngine.UI.NaviDirection P1) { }
        // RVA: 0x09E3BEF4  token: 0x60002CE
        public Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>> __Gen_Wrap_25(System.Object P0) { }
        // RVA: 0x09E3BFD0  token: 0x60002CF
        public Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>>> __Gen_Wrap_26(System.Object P0) { }
        // RVA: 0x09E3C0AC  token: 0x60002D0
        public Beyond.DynamicFastLookupCollection<Beyond.Input.GamepadKeyCode,System.Collections.Generic.HashSet<Beyond.Input.GamepadCheckerBase.Binding>> __Gen_Wrap_27(System.Object P0, System.Object P1) { }
        // RVA: 0x09E3C1B8  token: 0x60002D1
        public Beyond.Input.GamepadCheckerBase.Binding __Gen_Wrap_28(System.Object P0, System.Object P1, System.Object P2, System.Boolean P3) { }
        // RVA: 0x09E3C2EC  token: 0x60002D2
        public Beyond.Input.GamepadCheckerBase.Binding __Gen_Wrap_29(System.Object P0, System.Object P1, System.Object P2) { }
        // RVA: 0x063778A4  token: 0x60002D3
        public System.Void __Gen_Wrap_30(System.Object P0, System.Object P1, Beyond.Input.GamepadKeyCode P2) { }
        // RVA: 0x063761FC  token: 0x60002D4
        public System.Void __Gen_Wrap_31() { }
        // RVA: 0x066F3838  token: 0x60002D5
        public System.Void __Gen_Wrap_32(System.Object P0, Beyond.Input.InputContinuousTriggerType P1) { }
        // RVA: 0x09E3C414  token: 0x60002D6
        public Beyond.Input.IBindingView __Gen_Wrap_33(System.Object P0) { }
        // RVA: 0x06A5FB54  token: 0x60002D7
        public System.Void __Gen_Wrap_34(System.Object P0, Beyond.Input.GamepadKeyCode P1) { }
        // RVA: 0x09E3C504  token: 0x60002D8
        public Beyond.Input.PlayerActionInfo __Gen_Wrap_35(System.Object P0, System.Object P1, Beyond.Input.InputSettingLevel P2) { }
        // RVA: 0x09E3C628  token: 0x60002D9
        public System.String __Gen_Wrap_36(System.Object P0, System.Object P1) { }
        // RVA: 0x09E3C734  token: 0x60002DA
        public System.String __Gen_Wrap_37(System.Object P0) { }
        // RVA: 0x09E3C824  token: 0x60002DB
        public System.Action __Gen_Wrap_38(System.Object P0, System.Object P1, Beyond.Input.GamepadKeyCode P2) { }
        // RVA: 0x09E3C938  token: 0x60002DC
        public System.Single __Gen_Wrap_39(System.Object P0, System.Object P1, System.Int32 P2) { }
        // RVA: 0x063655AC  token: 0x60002DD
        public System.Boolean __Gen_Wrap_40(Beyond.Input.GamepadKeyCode P0) { }
        // RVA: 0x09E3CA3C  token: 0x60002DE
        public UnityEngine.RectTransform __Gen_Wrap_41(System.Object P0) { }
        // RVA: 0x09E3CB2C  token: 0x60002DF
        public System.Single __Gen_Wrap_42(System.Object P0) { }
        // RVA: 0x066C288C  token: 0x60002E0
        public System.Void __Gen_Wrap_43(System.Object P0, System.Single P1) { }
        // RVA: 0x09E3CBE8  token: 0x60002E1
        public UnityEngine.InputSystem.Mouse __Gen_Wrap_44(System.Object P0) { }
        // RVA: 0x09E3CCD8  token: 0x60002E2
        public UnityEngine.InputSystem.InputActionProperty __Gen_Wrap_45(System.Object P0) { }
        // RVA: 0x09E3CDDC  token: 0x60002E3
        public System.Void __Gen_Wrap_46(UnityEngine.InputSystem.InputActionProperty& P0, UnityEngine.InputSystem.InputActionProperty P1) { }
        // RVA: 0x09E3CF54  token: 0x60002E4
        public System.Void __Gen_Wrap_47(System.Object P0, UnityEngine.InputSystem.InputActionProperty P1) { }
        // RVA: 0x09E3D05C  token: 0x60002E5
        public System.Void __Gen_Wrap_48(UnityEngine.InputSystem.InputActionProperty P0, System.Object P1, System.Boolean P2) { }
        // RVA: 0x09E3D180  token: 0x60002E6
        public System.Void __Gen_Wrap_49(System.Object P0, UnityEngine.InputSystem.InputAction.CallbackContext P1) { }
        // RVA: 0x063621C4  token: 0x60002E7
        public System.Int32 __Gen_Wrap_50() { }
        // RVA: 0x09E3D280  token: 0x60002E8
        public Beyond.Input.KeyboardCheckerBase.Binding __Gen_Wrap_51(System.Object P0, System.Object P1, System.Object P2, System.Boolean P3) { }
        // RVA: 0x09E3D3B4  token: 0x60002E9
        public System.Int32 __Gen_Wrap_52(System.Object P0, System.Object P1, System.Object P2, System.Int32 P3) { }
        // RVA: 0x066C53B4  token: 0x60002EA
        public System.Boolean __Gen_Wrap_53(UnityEngine.UI.Selectable P0) { }
        // RVA: 0x09E3D4C8  token: 0x60002EB
        public UnityEngine.UI.Selectable __Gen_Wrap_54(System.Object P0) { }
        // RVA: 0x09E3D5A4  token: 0x60002EC
        public Beyond.Input.KeyboardCheckerBase.Binding __Gen_Wrap_55(System.Object P0, System.Object P1, System.Object P2) { }
        // RVA: 0x066F3838  token: 0x60002ED
        public System.Void __Gen_Wrap_56(System.Object P0, System.Int32 P1) { }
        // RVA: 0x09E3D6CC  token: 0x60002EE
        public System.Object __Gen_Wrap_57(System.Object P0) { }
        // RVA: 0x09E3D7A8  token: 0x60002EF
        public Beyond.Input.IBindingGroupTarget __Gen_Wrap_58(System.Object P0) { }
        // RVA: 0x063623A8  token: 0x60002F0
        public System.Boolean __Gen_Wrap_59() { }
        // RVA: 0x09E3D884  token: 0x60002F1
        public UnityEngine.Vector2 __Gen_Wrap_60(System.Object P0, System.Boolean P1, System.Boolean P2) { }
        // RVA: 0x09E3D9AC  token: 0x60002F2
        public System.Single __Gen_Wrap_61(System.Object P0, System.Boolean P1) { }
        // RVA: 0x09E3DA98  token: 0x60002F3
        public UnityEngine.Vector2 __Gen_Wrap_62() { }
        // RVA: 0x06375088  token: 0x60002F4
        public System.Void __Gen_Wrap_63(System.Boolean P0) { }
        // RVA: 0x09E3DB64  token: 0x60002F5
        public System.Void __Gen_Wrap_64(System.Object P0, System.Object P1, System.Int32 P2, System.Boolean P3) { }
        // RVA: 0x0635FF24  token: 0x60002F6
        public System.Void __Gen_Wrap_65(Beyond.DeviceInfo.InputType P0) { }
        // RVA: 0x066F3838  token: 0x60002F7
        public System.Void __Gen_Wrap_66(System.Object P0, Beyond.DeviceInfo.InputType P1) { }
        // RVA: 0x06366E40  token: 0x60002F8
        public Beyond.Input.KeyboardType __Gen_Wrap_67(System.Object P0) { }
        // RVA: 0x066F3838  token: 0x60002F9
        public System.Void __Gen_Wrap_68(System.Object P0, Beyond.Input.KeyboardType P1) { }
        // RVA: 0x09E3DC6C  token: 0x60002FA
        public System.Collections.Generic.Dictionary<System.String,System.Collections.Generic.HashSet<System.Int32>> __Gen_Wrap_69(System.Object P0) { }
        // RVA: 0x071C15C8  token: 0x60002FB
        public System.Void __Gen_Wrap_70(System.Object P0, System.Object P1, System.Single P2) { }
        // RVA: 0x06A5FB54  token: 0x60002FC
        public System.Void __Gen_Wrap_71(System.Object P0, Beyond.Input.KeyboardKeyCode P1) { }
        // RVA: 0x06301ED4  token: 0x60002FD
        public System.Void __Gen_Wrap_72(Beyond.Input.KeyboardKeyCode P0) { }
        // RVA: 0x06301ED4  token: 0x60002FE
        public System.Void __Gen_Wrap_73(Beyond.Input.GamepadKeyCode P0) { }
        // RVA: 0x02D541E0  token: 0x60002FF
        public Beyond.DeviceInfo.ControllerType __Gen_Wrap_74(System.Object P0, System.Object P1, System.Boolean& P2) { }
        // RVA: 0x09E3DD5C  token: 0x6000300
        public System.String __Gen_Wrap_75(Beyond.Input.GamepadKeyCode P0) { }
        // RVA: 0x09E3DE48  token: 0x6000301
        public System.String __Gen_Wrap_76() { }
        // RVA: 0x09E3DF14  token: 0x6000302
        public System.String __Gen_Wrap_77(System.Object P0, System.Object P1, System.Boolean P2, System.Boolean P3) { }
        // RVA: 0x09E3E048  token: 0x6000303
        public System.String __Gen_Wrap_78(System.Object P0, System.Boolean P1, System.Boolean P2) { }
        // RVA: 0x09E3E170  token: 0x6000304
        public System.String __Gen_Wrap_79(System.Object P0, System.Boolean P1, System.Boolean P2, System.Boolean P3) { }
        // RVA: 0x09E3E2A4  token: 0x6000305
        public System.Void __Gen_Wrap_80(System.Object P0, Beyond.EventData<System.Boolean>& P1) { }
        // RVA: 0x09E3E3C4  token: 0x6000306
        public System.Boolean __Gen_Wrap_81(System.Object P0, System.Object P1, Beyond.Input.KeyboardKeyCode& P2) { }
        // RVA: 0x09E3E4DC  token: 0x6000307
        public System.Boolean __Gen_Wrap_82(System.Object P0, System.Object P1, Beyond.Input.KeyboardKeyCode& P2, System.Boolean& P3) { }
        // RVA: 0x06A2DAF8  token: 0x6000308
        public System.Boolean __Gen_Wrap_83(System.Object P0, Beyond.Input.GamepadKeyCode P1, Beyond.Input.InputManager.KeyCodeState P2) { }
        // RVA: 0x09E3E628  token: 0x6000309
        public System.Int32 __Gen_Wrap_84(System.Object P0, System.Object P1, System.Object P2, System.Int32 P3, System.String& P4) { }
        // RVA: 0x09E3E798  token: 0x600030A
        public System.Int32 __Gen_Wrap_85(System.Object P0, Beyond.Input.KeyboardKeyCode P1, System.Object P2, Beyond.Input.InputTimingType P3, System.Object P4, System.Int32 P5) { }
        // RVA: 0x09E3E8BC  token: 0x600030B
        public System.Boolean __Gen_Wrap_86(System.Object P0, System.Int32 P1, System.Boolean P2) { }
        // RVA: 0x066F4C88  token: 0x600030C
        public System.Void __Gen_Wrap_87(System.Object P0, System.Int32 P1, System.Object P2) { }
        // RVA: 0x09E3E9C0  token: 0x600030D
        public System.Void __Gen_Wrap_88(System.Object P0, System.Boolean P1, System.Int32 P2, System.Int32 P3) { }
        // RVA: 0x09E3EAC8  token: 0x600030E
        public System.Int32 __Gen_Wrap_89(System.Object P0, System.Boolean P1, System.Int32 P2) { }
        // RVA: 0x09E3EBCC  token: 0x600030F
        public Beyond.Input.KeyboardKeyCode __Gen_Wrap_90(System.Object P0, System.Object P1, System.Boolean P2, System.Boolean P3, Beyond.Input.InputSettingLevel P4) { }
        // RVA: 0x09E3EBCC  token: 0x6000310
        public Beyond.Input.GamepadKeyCode __Gen_Wrap_91(System.Object P0, System.Object P1, System.Boolean P2, System.Boolean P3, Beyond.Input.InputSettingLevel P4) { }
        // RVA: 0x06373694  token: 0x6000311
        public System.Boolean __Gen_Wrap_92(Beyond.Input.InputDeviceFlags P0, Beyond.Input.InputDeviceFlags P1) { }
        // RVA: 0x09E3ECF0  token: 0x6000312
        public System.Boolean __Gen_Wrap_93(System.Object P0, System.Object P1, Beyond.Input.InputDeviceFlags P2) { }
        // RVA: 0x071BBFCC  token: 0x6000313
        public System.Void __Gen_Wrap_94(System.Object P0, System.Object P1, Beyond.Input.InputDeviceFlags P2) { }
        // RVA: 0x09E3EDF8  token: 0x6000314
        public System.Void __Gen_Wrap_95(System.Object P0, Beyond.Input.InputDeviceFlags P1) { }
        // RVA: 0x09E3EED4  token: 0x6000315
        public System.String __Gen_Wrap_96(System.Object P0, System.Int32 P1) { }
        // RVA: 0x09E3EFE0  token: 0x6000316
        public System.Boolean __Gen_Wrap_97(System.Object P0, System.Object P1, Beyond.Input.KeyboardKeyCode P2, System.Boolean& P3) { }
        // RVA: 0x066C0800  token: 0x6000317
        public System.Single __Gen_Wrap_98(System.Object P0, System.Object P1) { }
        // RVA: 0x09E3F108  token: 0x6000318
        public System.Void __Gen_Wrap_99(System.Object P0, System.Int32 P1, System.String& P2, Beyond.Input.IBindingView& P3) { }
        // RVA: 0x09E3B06C  token: 0x6000319
        public Beyond.Input.PlayerActionInfo __Gen_Wrap_100(System.Object P0, System.Object P1) { }
        // RVA: 0x09E3B178  token: 0x600031A
        public System.Boolean __Gen_Wrap_101(System.Object P0, System.Object P1, Beyond.Input.KeyboardKeyCode P2, System.Boolean P3, System.Object P4) { }
        // RVA: 0x09E3B29C  token: 0x600031B
        public System.Boolean __Gen_Wrap_102(System.Object P0, System.Object P1, System.Nullable<Beyond.Input.GamepadKeyCode> P2, System.Nullable<Beyond.Input.GamepadKeyCode> P3, System.Boolean P4, System.Object P5) { }
        // RVA: 0x09E3B40C  token: 0x600031C
        public UnityEngine.Vector3 __Gen_Wrap_103() { }
        // RVA: 0x09E3B500  token: 0x600031D
        public System.Void __Gen_Wrap_104(UnityEngine.Vector2 P0, System.Boolean P1) { }
        // RVA: 0x09E3B608  token: 0x600031E
        public System.Void __Gen_Wrap_105(System.Object P0, UnityEngine.Vector2 P1) { }
        // RVA: 0x06361FA4  token: 0x600031F
        public System.Boolean __Gen_Wrap_106(System.Int32 P0) { }
        // RVA: 0x02D4A050  token: 0x6000320
        public System.Boolean __Gen_Wrap_107(System.Boolean P0) { }
        // RVA: 0x02D4A250  token: 0x6000321
        public System.Collections.Generic.List<Beyond.Input.InputManager.ControllerHintInfo> __Gen_Wrap_108(System.Object P0) { }
        // RVA: 0x02D4AB80  token: 0x6000322
        public System.Void __Gen_Wrap_109(System.Object P0, System.Object P1, System.Boolean P2, System.Collections.Generic.List<Beyond.Input.InputManager.ControllerHintInfo>& P3) { }
        // RVA: 0x02D4B730  token: 0x6000323
        public System.Void __Gen_Wrap_110(System.Object P0, System.Int32 P1, System.Boolean P2, System.Collections.Generic.List<Beyond.Input.InputManager.ControllerHintInfo>& P3) { }
        // RVA: 0x02D4C200  token: 0x6000324
        public System.Void __Gen_Wrap_111(System.Object P0, System.Object P1, System.Collections.Generic.List<Beyond.Input.InputManager.ControllerHintInfo>& P2) { }
        // RVA: 0x02D4CD90  token: 0x6000325
        public System.Single __Gen_Wrap_112(System.Object P0, System.Object P1, System.Single& P2) { }
        // RVA: 0x01774E60  token: 0x6000326
        public System.Single __Gen_Wrap_113(System.Object P0, System.Int32 P1, System.Single& P2) { }
        // RVA: 0x02D4D1A0  token: 0x6000327
        public System.Void __Gen_Wrap_114(System.Object P0, System.Single P1, System.Single P2) { }
        // RVA: 0x02D4D4A0  token: 0x6000328
        public System.String __Gen_Wrap_115(Beyond.Input.KeyboardKeyCode P0) { }
        // RVA: 0x02D4DD20  token: 0x6000329
        public System.String __Gen_Wrap_116(System.Text.RegularExpressions.Match P0) { }
        // RVA: 0x01527A20  token: 0x600032A
        public System.Void __Gen_Wrap_117(System.Object P0, System.Object P1, System.Object P2, System.Boolean P3) { }
        // RVA: 0x02D4E650  token: 0x600032B
        public System.Boolean __Gen_Wrap_118(System.Int32 P0, UnityEngine.Vector2& P1) { }
        // RVA: 0x02D4EF20  token: 0x600032C
        public UnityEngine.GameObject __Gen_Wrap_119(System.Object P0, System.Object P1) { }
        // RVA: 0x02D4F940  token: 0x600032D
        public System.String __Gen_Wrap_120(System.Object P0, Beyond.Input.KeyboardKeyCode P1, System.Boolean P2) { }
        // RVA: 0x02D50300  token: 0x600032E
        public System.String __Gen_Wrap_121(System.Object P0, Beyond.Input.GamepadKeyCode P1, System.Boolean P2) { }
        // RVA: 0x02D50CC0  token: 0x600032F
        public System.String __Gen_Wrap_122(System.Object P0, System.Object P1, System.Boolean P2, System.Boolean P3, System.Boolean P4) { }
        // RVA: 0x02D51750  token: 0x6000330
        public System.Void __Gen_Wrap_123(System.Object P0, Beyond.EventData<System.String>& P1) { }
        // RVA: 0x02D51DD0  token: 0x6000331
        public Beyond.Input.InputManager.DelayedComboBinding __Gen_Wrap_124(System.Object P0, System.Object P1, System.Object P2, Beyond.Input.InputManager.DelayedComboKind P3, System.Single P4, System.Object P5) { }
        // RVA: 0x01353F60  token: 0x6000332
        public System.Int32 __Gen_Wrap_125(System.Object P0, System.Object P1) { }
        // RVA: 0x02D529C0  token: 0x6000333
        public System.Collections.Generic.List<Beyond.DynamicFastLookupCollection<Beyond.Input.KeyboardKeyCode,System.Collections.Generic.HashSet<Beyond.Input.KeyboardCheckerBase.Binding>>> __Gen_Wrap_126(System.Object P0) { }
        // RVA: 0x01412530  token: 0x6000334
        public System.Void __Gen_Wrap_127(System.Object P0, System.Object P1, Beyond.Input.KeyboardKeyCode P2) { }
        // RVA: 0x02D532F0  token: 0x6000335
        public System.String __Gen_Wrap_128(Beyond.Input.CursorRequest& P0) { }
        // RVA: 0x04D37AA0  token: 0x6000336
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200004E  // size: 0x40
    public class ILFixInterfaceBridge : IFix.Core.AnonymousStorey
    {
        // Methods
        // RVA: 0x09E3F280  token: 0x6000337
        public System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x041E1670  token: 0x6000338
        public System.Void RefAsyncBuilderStartMethod() { }

    }

    // TypeToken: 0x200004F  // size: 0x18
    public class WrappersManagerImpl : IFix.Core.WrappersManager
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000339
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x09E41724  token: 0x600033A
        public static IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id) { }
        // RVA: 0x02FAA9F0  token: 0x600033B
        public static System.Boolean IsPatched(System.Int32 id) { }
        // RVA: 0x09E415E0  token: 0x600033C
        public virtual System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon) { }
        // RVA: 0x09E416AC  token: 0x600033D
        public virtual System.Object CreateWrapper(System.Int32 id) { }
        // RVA: 0x09E41788  token: 0x600033E
        public virtual System.Object InitWrapperArray(System.Int32 len) { }
        // RVA: 0x09E4153C  token: 0x600033F
        public virtual IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine) { }

    }

    // TypeToken: 0x2000050  // size: 0x14
    public sealed struct IDMAP0
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static IFix.IDMAP0 Beyond-Input-InputBindingGroup-get_enabled0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingInfo-_GetNextEnabledPriority0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingInfo-StopContinuousTrigger0;  // const
        public static IFix.IDMAP0 Beyond-Input-KeyboardOnLongPressChecker-CancelPendingLongPress0;  // const
        public static IFix.IDMAP0 Beyond-Input-GamepadOnLongPressChecker-CancelPendingLongPress0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-CancelPendingLongPress0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingInfo-OnGroupEnabledChanged0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingGroup-OnParentEnabledChanged0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingGroup-_OnBindingGroupStateChanged0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingGroup-SetSelfEnabled0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-ToggleGroup0;  // const
        public static IFix.IDMAP0 Beyond-Input-ControllerNaviManager-SetNaviBindingType0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-IsGroupEnabled0;  // const
        public static IFix.IDMAP0 Beyond-Input-ControllerNaviManager-IsNavigationBindingEnabled0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-get_usingController0;  // const
        public static IFix.IDMAP0 Beyond-Input-ControllerNaviManager-UpdateNaviInputBindingState0;  // const
        public static IFix.IDMAP0 Beyond-Input-ControllerNaviManager-Tick0;  // const
        public static IFix.IDMAP0 Beyond-Input-ControllerNaviManager-_RemoveLayerFromStack0;  // const
        public static IFix.IDMAP0 Beyond-Input-ControllerNaviManager-_RemoveLayer0;  // const
        public static IFix.IDMAP0 Beyond-Input-ControllerNaviManager-_OnLayerSelected0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingGroup-set_groupTarget0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-CreateGroup0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingGroupMonoTarget-UpdateState0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingGroupMonoTarget-Init0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingGroupMonoTarget-get_groupId0;  // const
        public static IFix.IDMAP0 Beyond-Input-ControllerNaviManager-_SetTarget0;  // const
        public static IFix.IDMAP0 Beyond-Input-ControllerNaviManager-GetNaviVector0;  // const
        public static IFix.IDMAP0 Beyond-Input-ControllerNaviManager-_GetPointOnRectEdge0;  // const
        public static IFix.IDMAP0 Beyond-Input-ControllerNaviManager-_TryFindBestNaviTarget0;  // const
        public static IFix.IDMAP0 Beyond-Input-ControllerNaviManager-_TryFindBestNaviTargetInGroup0;  // const
        public static IFix.IDMAP0 Beyond-Input-ControllerNaviManager-SetTarget0;  // const
        public static IFix.IDMAP0 Beyond-Input-ControllerNaviManager-_DefaultNavigate0;  // const
        public static IFix.IDMAP0 Beyond-Input-ControllerNaviManager-_GetDummyLayerLastGroupInStack0;  // const
        public static IFix.IDMAP0 Beyond-Input-ControllerNaviManager-SetForceEnableNavi0;  // const
        public static IFix.IDMAP0 Beyond-Input-ControllerNaviManager-TryAutoSetTarget0;  // const
        public static IFix.IDMAP0 Beyond-Input-ControllerNaviManager-TryRemoveLayer0;  // const
        public static IFix.IDMAP0 Beyond-Input-ControllerNaviManager-IsLayerInStack0;  // const
        public static IFix.IDMAP0 Beyond-Input-ControllerNaviManager-IsTopLayer0;  // const
        public static IFix.IDMAP0 Beyond-Input-ControllerNaviManager-ChangeAndTrySetNaviBindingType0;  // const
        public static IFix.IDMAP0 Beyond-Input-ControllerNaviManager-GetTopDummyLayerGroup0;  // const
        public static IFix.IDMAP0 Beyond-Input-ControllerNaviManager-ResetStateForUIDispose0;  // const
        public static IFix.IDMAP0 Beyond-Input-ControllerNaviManager-SetTargetInSilentModeIfNecessary0;  // const
        public static IFix.IDMAP0 Beyond-Input-ControllerNaviManager-ClearTargetIfNecessary0;  // const
        public static IFix.IDMAP0 Beyond-Input-ControllerNaviManager-SetTargetInSilentModeIfNecessary1;  // const
        public static IFix.IDMAP0 Beyond-Input-ControllerNaviManager-IsNaviTarget0;  // const
        public static IFix.IDMAP0 Beyond-Input-ControllerNaviManager-Navigate0;  // const
        public static IFix.IDMAP0 Beyond-Input-ControllerNaviManager-ToggleNavigation0;  // const
        public static IFix.IDMAP0 Beyond-Input-GamepadCheckerBase-get_normalBindingInfos0;  // const
        public static IFix.IDMAP0 Beyond-Input-GamepadCheckerBase-get_modifyBindingInfos0;  // const
        public static IFix.IDMAP0 Beyond-Input-GamepadCheckerBase-GetInputDic0;  // const
        public static IFix.IDMAP0 Beyond-Input-GamepadCheckerBase-OnCreateBinding0;  // const
        public static IFix.IDMAP0 Beyond-Input-GamepadCheckerBase-OnDeleteBinding0;  // const
        public static IFix.IDMAP0 Beyond-Input-GamepadCheckerBase-CheckDic0;  // const
        public static IFix.IDMAP0 Beyond-Input-GamepadCheckerBase-CheckGamepadInput0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingInfo-_RealTriggerCallback0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingInfo-Trigger0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-IsGuideUseAction0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingInfo-get_enabled0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingInfo-_TryContinuousTrigger0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingInfo-StartContinuousTrigger0;  // const
        public static IFix.IDMAP0 Beyond-Input-GamepadCheckerBase-TriggerActions0;  // const
        public static IFix.IDMAP0 Beyond-Input-GamepadChecker-CheckDic0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingGroup-get_groupView0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingInfo-get_bindingView0;  // const
        public static IFix.IDMAP0 Beyond-Input-GamepadOnClickChecker-CheckDic0;  // const
        public static IFix.IDMAP0 Beyond-Input-GamepadOnClickChecker-ClearKeyPressed0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetPlayerActionInfo0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetActionText0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetActionText1;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingInfo-get_text0;  // const
        public static IFix.IDMAP0 Beyond-Input-GamepadOnLongPressChecker-_OnTriggerBinding0;  // const
        public static IFix.IDMAP0 Beyond-Input-GamepadOnLongPressChecker-CreateTriggerBindingAction0;  // const
        public static IFix.IDMAP0 Beyond-Input-GamepadOnLongPressChecker-CheckDic0;  // const
        public static IFix.IDMAP0 Beyond-Input-GamepadOnLongPressChecker-OnCreateBinding0;  // const
        public static IFix.IDMAP0 Beyond-Input-GamepadOnLongPressChecker-OnDeleteBinding0;  // const
        public static IFix.IDMAP0 Beyond-Input-GamepadOnLongPressChecker-GetInputLongPressStartTime0;  // const
        public static IFix.IDMAP0 Beyond-Input-GamepadOnReleaseChecker-CheckGamepadInput0;  // const
        public static IFix.IDMAP0 Beyond-Input-GamepadOnReleaseChecker-CheckDic0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-get_cursorTransform0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-set_cursorTransform0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-get_scrollSpeed0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-set_scrollSpeed0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-get_virtualMouse0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-get_stickAction0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-_SetAction0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-set_stickAction0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-get_leftButtonAction0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-_SetActionCallback0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-set_leftButtonAction0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-get_rightButtonAction0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-set_rightButtonAction0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-get_middleButtonAction0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-set_middleButtonAction0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-get_forwardButtonAction0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-set_forwardButtonAction0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-get_backButtonAction0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-set_backButtonAction0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-get_scrollWheelAction0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-set_scrollWheelAction0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-SetMouseIconVisible0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-_OnButtonActionTriggered0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-TriggerLeftButton0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-get_invalidID0;  // const
        public static IFix.IDMAP0 Beyond-Input-KeyboardCheckerBase-GetModifyIndex0;  // const
        public static IFix.IDMAP0 Beyond-Input-KeyboardCheckerBase-OnCreateBinding0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_AddPlayerActionCheck0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_AddPlayerActionCheck1;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_AddPlayerActionCheck2;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-CreateBindingByActionId0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-_ChangeHoverState0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-ChangeHoverState0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-_SyncScale0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-OnEnable0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-OnDisable0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-_UpdateMotion0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-ClearLeftButtonState0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-OnAfterInputUpdate0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-Update0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-GetCurHoverSelectable0;  // const
        public static IFix.IDMAP0 Beyond-Input-KeyboardCheckerBase-OnDeleteBinding0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_RemovePlayerActionCheck0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_RemovePlayerActionCheck1;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_RemovePlayerActionCheck2;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingInfo-OnDelete0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_RealRemoveBindingInfo0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_DeleteInGroup0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_DeleteGroup0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-DeleteGroup0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-OnDestroy0;  // const
        public static IFix.IDMAP0 Beyond-Input-HGVirtualMouse-Awake0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingInfo-set_bindingView0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingInfo-set_text0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingInfo-ChangeGroup0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingInfo-SetSelfEnabled0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingInfo-SetParentIgnored0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingInfo-GetCallbackTarget0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingGroup-set_groupView0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingGroup-get_groupTarget0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingGroup-ChangeParent0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingGroup-SetParentIgnored0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingGroupMonoTarget-get_groupEnabled0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingGroupMonoTarget-get_parentTarget0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingGroupMonoTarget-get_internalEnabled0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingGroupMonoTarget-set_internalEnabled0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingGroupMonoTarget-DeleteGroup0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingGroupMonoTarget-Awake0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingGroupMonoTarget-OnEnable0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingGroupMonoTarget-OnDisable0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputBindingGroupMonoTarget-OnDestroy0;  // const
        public static IFix.IDMAP0 Beyond-Input-RealCursorManager-get_isDebugForceShow0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-get_isDebugForceShow0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-get_cursorVisible0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-get_isInputSuppressedByDeviceSwitch0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetGamepadStickValue0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetGamepadTriggerValue0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-AnyGamepadKey0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-AnyMouseKeyDown0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-AnyKeyDown0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-AnyTouchBegan0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-get_anyKeyDown0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-get_mouseScrollDelta0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-get_multiTouchEnabled0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-set_multiTouchEnabled0;  // const
        public static IFix.IDMAP0 Beyond-Input-RealCursorManager-HasAnyShowRequestAtPriority0;  // const
        public static IFix.IDMAP0 Beyond-Input-RealCursorManager-_ToggleCursorInternal0;  // const
        public static IFix.IDMAP0 Beyond-Input-RealCursorManager-CalcState0;  // const
        public static IFix.IDMAP0 Beyond-Input-RealCursorManager-SetVirtualMouseActive0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_ToggleVirtualMouse0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_CheckVirtualMouseState0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-ToggleBinding0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_RefreshDeviceChangeBinding0;  // const
        public static IFix.IDMAP0 Beyond-Input-RealCursorManager-_RefreshActiveHighest0;  // const
        public static IFix.IDMAP0 Beyond-Input-RealCursorManager-SetRequest0;  // const
        public static IFix.IDMAP0 Beyond-Input-RealCursorManager-RemoveRequest0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-get_autoUpdateInputBindingMask0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_SetInputBindingMask0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_UpdateInputBindingMask0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_SyncUsingControllerState0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-SetCursorShowRequest0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-ChangeInputTypeAndSyncState0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_TryChangeInputDeviceType0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-set_usingController0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-get_virtualMouseIconVisible0;  // const
        public static IFix.IDMAP0 Beyond-Input-KeyboardHandler-GetKeyboardType0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-get_keyboardType0;  // const
        public static IFix.IDMAP0 Beyond-Input-KeyboardHandler-SetKeyboardType0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-set_keyboardType0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-get_action2BindingDic0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-get_rootGroupId0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_UpdateModifierTapThenTriggerCombo0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_UpdateTriggerThenModifierCombo0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_UpdateDelayedCombos0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-LateTick0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_CreateTickFunction0;  // const
        public static IFix.IDMAP0 Beyond-Input-KeyboardOnClickChecker-ClearKeyPressed0;  // const
        public static IFix.IDMAP0 Beyond-Input-KeyboardHandler-ParseConfig0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_LoadCustomInputSetting0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_LoadSetting0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_IsValidJoystick0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_GetControllerType0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_SetJoystick0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_OnControllerConnected0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_TryFindGamepad0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_OnControllerDisconnected0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_CreateDebugVirtualMouse0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetStringByGamepadKeyCode0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_GetGamepadKeyIconFolderPath0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_GetKeyIconPath0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetGamepadKeyIconPath0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetKeyIconPath0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetKeyIconPath1;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetKeyIconPath2;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_CreateVirtualMouse0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_CreateCanvas0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_InitDeviceChangeBinding0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-KeyCaches-UpdateKeyStat0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_CheckAnyKeyForTouchChangeDevice0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-AnyMouseKeyUp0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_AnyKeyDownForKeyboardChangeDevice0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_CheckAnyKeyForKeyboardChangeDevice0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-CheckUsingController0;  // const
        public static IFix.IDMAP0 Beyond-Input-KeyboardCheckerBase-CheckKeyboardInput0;  // const
        public static IFix.IDMAP0 Beyond-Input-KeyboardCheckerBase-TriggerActions0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_ControllerTouchPanelTick0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_CheckInput0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_TryStopDeviceSwitchSuppression0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_OnAfterInputUpdate0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_OnNetMaskChanged0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_OnFocusedInputFieldChanged0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_Init0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_ClearTickFunction0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-Release0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_ClearEvents0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-OnApplicationQuit0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-OnApplicationFocus0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-set_autoUpdateInputBindingMask0;  // const
        public static IFix.IDMAP0 Beyond-Input-RealCursorManager-RemoveAllRequestsAtPriority0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-ClearAllCursorShowRequests0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-ToggleInputDeviceChangeMode0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-SetCursorOverrideForDeviceChange0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_AnyKeyboardKey0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-AnyKeyboardKey0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-AnyMouseKey0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-AnyTouchActive0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_CheckGamepadKeyCode0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-AddAnonymousPlayerAction0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-CreateBinding0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-CreateBinding1;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-CreateBinding2;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-DeleteBinding0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-DeleteInGroup0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-IsBindingEnabled0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-IsGroupParentDisabled0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-ForceBindingKeyhintToGray0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-IsBindingKeyhintGray0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-ToggleDebugCursorMode0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-ToggleAllInput0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-SetBindingView0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-SetGroupView0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-ChangeParent0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetGroupParentId0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-IgnoreBindingGroupParent0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-IgnoreBindingParent0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetActionKeyboardKeyCode0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetActionGamepadKeyCode0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputUtils-HasFlag0;  // const
        public static IFix.IDMAP0 Beyond-Input-PlayerActionInfo-DeviceInputEquals0;  // const
        public static IFix.IDMAP0 Beyond-Input-PlayerActionInfo-DeviceInputCopyFrom0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-DeleteCustomBinding0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-DeleteAllCustomBindings0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-SetBindingText0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetBindingText0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-HasBinding0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-CheckActionKeyCodeConflict0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetAxis0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetBindingInfo0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-LoadSetting0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-IsPlayerActionIdValid0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-ChangePlayerAction0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_GetOrCreatePendingActionInfo0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-CustomizeActionKey0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-CustomizeActionKey1;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-ClearPendingInputSetting0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-SaveCustomInputSetting0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetMousePos0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-get_mousePosition0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-SetVirtualMouseIconVisible0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-SetMousePos0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-SetOnlyVirtualMousePos0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetMouseButton0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-IsLeftMouseDown0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetMouseButtonDown0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetMouseButtonUp0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-SetCustomControllerMouse0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetEmptyControllerHintInfoList0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-ControllerHintInfo-Reset0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_GetControllerHintInfos0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetControllerHintInfos0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-ControllerHintInfo-Reset1;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_GetControllerHintInfos1;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetControllerHintInfos1;  // const
        public static IFix.IDMAP0 Beyond-Input-KeyboardOnLongPressChecker-GetInputLongPressStartTime0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetPlayerActionLongPressStartTime0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetActionBindingLongPressStartTime0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-AddGuideUseActionId0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-RemoveGuideUseActionId0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-ClearGuideUseActionIds0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_IsAnyBindingInGroupGuideUse0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-IsAnyBindingInGroupGuideUse0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-VibrateController0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetStringByKeyboardKeyCode0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetKeyboardIconPath0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-ParseTextActionId0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-MoveMouseTo0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-MoveVirtualMouseTo0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetTouchId0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetPressPosByTouchId0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-Dispose0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-RemoveUnusedInput0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetCurJsInfos0;  // const
        public static IFix.IDMAP0 Beyond-Input-RealCursorManager-DumpState0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-GetRealCursorState0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_GetInputBindingGroupGameObject0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_GetInputBindingGameObject0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_GetBindingName0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-PrintBindingInfo0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-PrintKeycodeInfo0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-PrintKeycodeInfo1;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-PrintKeycodeInfo2;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-PrintActionInfo0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_RefreshDelayedComboKeys0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-_OnDelayedComboActionKeyUpdated0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-RegisterDelayedCombo0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-UnregisterDelayedCombo0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-ControllerHintInfo-CompareTo0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-ControllerHintInfo-OnAllocate0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputManager-ControllerHintInfo-OnRecycle0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputUtils-IgnoreBindingParent0;  // const
        public static IFix.IDMAP0 Beyond-Input-InputUtils-IgnoreBindingGroupParent0;  // const
        public static IFix.IDMAP0 Beyond-Input-KeyboardCheckerBase-get_bindingInfos0;  // const
        public static IFix.IDMAP0 Beyond-Input-KeyboardChecker-CheckKeyboardInput0;  // const
        public static IFix.IDMAP0 Beyond-Input-KeyboardChecker-TriggerActions0;  // const
        public static IFix.IDMAP0 Beyond-Input-KeyboardOnClickChecker-CheckKeyboardInput0;  // const
        public static IFix.IDMAP0 Beyond-Input-KeyboardOnClickChecker-TriggerActions0;  // const
        public static IFix.IDMAP0 Beyond-Input-KeyboardOnLongPressChecker-_OnTriggerBinding0;  // const
        public static IFix.IDMAP0 Beyond-Input-KeyboardOnLongPressChecker-CheckKeyboardInput0;  // const
        public static IFix.IDMAP0 Beyond-Input-KeyboardOnLongPressChecker-OnCreateBinding0;  // const
        public static IFix.IDMAP0 Beyond-Input-KeyboardOnLongPressChecker-OnDeleteBinding0;  // const
        public static IFix.IDMAP0 Beyond-Input-PlayerActionInfo-ResetHintProority0;  // const
        public static IFix.IDMAP0 Beyond-Input-KeyboardInput-GetModifyText0;  // const
        public static IFix.IDMAP0 Beyond-Input-KeyboardInput-GetKeyCodeText0;  // const
        public static IFix.IDMAP0 Beyond-Input-KeyboardInput-GetKeyCodeEnumIntValue0;  // const
        public static IFix.IDMAP0 Beyond-Input-KeyboardInput-SetKeyCodeEnumByIntValue0;  // const
        public static IFix.IDMAP0 Beyond-Input-KeyboardInput-get_modifyString0;  // const
        public static IFix.IDMAP0 Beyond-Input-KeyboardInput-ClearKeyCode0;  // const
        public static IFix.IDMAP0 Beyond-Input-KeyboardInput-IsValid0;  // const
        public static IFix.IDMAP0 Beyond-Input-KeyboardInput-CopyFrom0;  // const
        public static IFix.IDMAP0 Beyond-Input-KeyboardInput-Equals0;  // const
        public static IFix.IDMAP0 Beyond-Input-GamepadInput-GetModifyText0;  // const
        public static IFix.IDMAP0 Beyond-Input-GamepadInput-GetKeyCodeText0;  // const
        public static IFix.IDMAP0 Beyond-Input-GamepadInput-GetKeyCodeEnumIntValue0;  // const
        public static IFix.IDMAP0 Beyond-Input-GamepadInput-SetKeyCodeEnumByIntValue0;  // const
        public static IFix.IDMAP0 Beyond-Input-GamepadInput-ClearKeyCode0;  // const
        public static IFix.IDMAP0 Beyond-Input-GamepadInput-IsValid0;  // const
        public static IFix.IDMAP0 Beyond-Input-GamepadInput-CopyFrom0;  // const
        public static IFix.IDMAP0 Beyond-Input-GamepadInput-Equals0;  // const
        public static IFix.IDMAP0 Beyond-Input-CursorRequest-ToString0;  // const
        public static IFix.IDMAP0 Beyond-Input-UIEvent-get_bindingEnabled0;  // const
        public static IFix.IDMAP0 Beyond-Input-UIEvent-_CreatePlayerActionBinding0;  // const
        public static IFix.IDMAP0 Beyond-Input-UIEvent-InitBinding0;  // const
        public static IFix.IDMAP0 Beyond-Input-UIEvent-Invoke0;  // const
        public static IFix.IDMAP0 Beyond-Input-UIEvent-UpdateKeys0;  // const
        public static IFix.IDMAP0 Beyond-Input-UIEvent-ChangeBindingPlayerAction0;  // const
        public static IFix.IDMAP0 Beyond-Input-UIEvent-get_count0;  // const
        public static IFix.IDMAP0 Beyond-Input-UIEvent-AddListener0;  // const
        public static IFix.IDMAP0 Beyond-Input-UIEvent-RemoveListener0;  // const
        public static IFix.IDMAP0 Beyond-Input-UIEvent-RemoveAllListeners0;  // const

    }

}

namespace UnityEngine.InputSystem.UI
{

    // TypeToken: 0x2000003  // size: 0x128
    public class DebugVirtualMouseInput : UnityEngine.MonoBehaviour
    {
        // Fields
        private UnityEngine.InputSystem.UI.DebugVirtualMouseInput.CursorMode m_CursorMode;  // 0x18
        private UnityEngine.UI.Graphic m_CursorGraphic;  // 0x20
        private UnityEngine.RectTransform m_CursorTransform;  // 0x28
        private UnityEngine.RectTransform _canvasTransform;  // 0x30
        private UnityEngine.Canvas m_Canvas;  // 0x38
        private System.Single m_CursorSpeed;  // 0x40
        private System.Single m_ScrollSpeed;  // 0x44
        private UnityEngine.InputSystem.InputActionProperty m_StickAction;  // 0x48
        private UnityEngine.InputSystem.InputActionProperty m_LeftButtonAction;  // 0x60
        private UnityEngine.InputSystem.InputActionProperty m_MiddleButtonAction;  // 0x78
        private UnityEngine.InputSystem.InputActionProperty m_RightButtonAction;  // 0x90
        private UnityEngine.InputSystem.InputActionProperty m_ForwardButtonAction;  // 0xa8
        private UnityEngine.InputSystem.InputActionProperty m_BackButtonAction;  // 0xc0
        private UnityEngine.InputSystem.InputActionProperty m_ScrollWheelAction;  // 0xd8
        private UnityEngine.InputSystem.Mouse m_VirtualMouse;  // 0xf0
        private UnityEngine.InputSystem.Mouse m_SystemMouse;  // 0xf8
        private System.Action m_AfterInputUpdateDelegate;  // 0x100
        private System.Action<UnityEngine.InputSystem.InputAction.CallbackContext> m_ButtonActionTriggeredDelegate;  // 0x108
        private System.Double m_LastTime;  // 0x110
        private UnityEngine.Vector2 m_LastStickValue;  // 0x118
        private System.Single m_canvasScale;  // 0x120

        // Properties
        UnityEngine.RectTransform cursorTransform { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }
        System.Single cursorSpeed { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        UnityEngine.InputSystem.UI.DebugVirtualMouseInput.CursorMode cursorMode { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x09E3387C */ }
        UnityEngine.UI.Graphic cursorGraphic { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x09E33858 */ }
        System.Single scrollSpeed { get; /* RVA: 0x04D86400 */ set; /* RVA: 0x04D86480 */ }
        UnityEngine.InputSystem.Mouse virtualMouse { get; /* RVA: 0x04D8D1A0 */ }
        UnityEngine.InputSystem.InputActionProperty stickAction { get; /* RVA: 0x04D92750 */ set; /* RVA: 0x09E33C58 */ }
        UnityEngine.InputSystem.InputActionProperty leftButtonAction { get; /* RVA: 0x04D92770 */ set; /* RVA: 0x09E33A18 */ }
        UnityEngine.InputSystem.InputActionProperty rightButtonAction { get; /* RVA: 0x04D9DCA0 */ set; /* RVA: 0x09E33B70 */ }
        UnityEngine.InputSystem.InputActionProperty middleButtonAction { get; /* RVA: 0x04DA2F20 */ set; /* RVA: 0x09E33AC0 */ }
        UnityEngine.InputSystem.InputActionProperty forwardButtonAction { get; /* RVA: 0x04DA2F00 */ set; /* RVA: 0x09E33960 */ }
        UnityEngine.InputSystem.InputActionProperty backButtonAction { get; /* RVA: 0x04DA2EE0 */ set; /* RVA: 0x09E337A0 */ }
        UnityEngine.InputSystem.InputActionProperty scrollWheelAction { get; /* RVA: 0x04DA2F40 */ set; /* RVA: 0x09E33C28 */ }

        // Methods
        // RVA: 0x09E327D4  token: 0x600001B
        protected System.Void OnEnable() { }
        // RVA: 0x09E32538  token: 0x600001C
        protected System.Void OnDisable() { }
        // RVA: 0x09E33290  token: 0x600001D
        private System.Void _TryFindCanvas() { }
        // RVA: 0x09E33098  token: 0x600001E
        private System.Void _TryEnableHardwareCursor() { }
        // RVA: 0x09E32F98  token: 0x600001F
        private System.Void _SyncScale() { }
        // RVA: 0x09E332EC  token: 0x6000020
        private System.Void _UpdateMotion() { }
        // RVA: 0x09E32C04  token: 0x6000021
        private System.Void _OnButtonActionTriggered(UnityEngine.InputSystem.InputAction.CallbackContext context) { }
        // RVA: 0x09E32E34  token: 0x6000022
        private static System.Void _SetActionCallback(UnityEngine.InputSystem.InputActionProperty field, System.Action<UnityEngine.InputSystem.InputAction.CallbackContext> callback, System.Boolean install) { }
        // RVA: 0x09E32EA0  token: 0x6000023
        private static System.Void _SetAction(UnityEngine.InputSystem.InputActionProperty& field, UnityEngine.InputSystem.InputActionProperty value) { }
        // RVA: 0x09E32BFC  token: 0x6000024
        private System.Void _OnAfterInputUpdate() { }
        // RVA: 0x04D77730  token: 0x6000025
        public System.Void .ctor() { }

    }

}

