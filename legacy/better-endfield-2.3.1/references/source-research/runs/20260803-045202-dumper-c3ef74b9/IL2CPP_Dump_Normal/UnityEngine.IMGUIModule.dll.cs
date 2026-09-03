// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.IMGUIModule.dll
// Classes:  45
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x200000A  // size: 0x80
    public sealed class WindowFunction : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x069095FC  token: 0x6000097
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02B46B90  token: 0x6000098
        public virtual System.Void Invoke(System.Int32 id) { }

    }

    // TypeToken: 0x200000C  // size: 0x11
    public sealed struct ParentClipScope : System.IDisposable
    {
        // Fields
        private System.Boolean m_Disposed;  // 0x10

        // Methods
        // RVA: 0x0B40EF8C  token: 0x60000B2
        public System.Void .ctor(UnityEngine.Matrix4x4 objectTransform, UnityEngine.Rect clipRect) { }
        // RVA: 0x0B40EF7C  token: 0x60000B3
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x2000012  // size: 0x14
    public sealed struct Type
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.GUILayoutOption.Type fixedWidth;  // const
        public static UnityEngine.GUILayoutOption.Type fixedHeight;  // const
        public static UnityEngine.GUILayoutOption.Type minWidth;  // const
        public static UnityEngine.GUILayoutOption.Type maxWidth;  // const
        public static UnityEngine.GUILayoutOption.Type minHeight;  // const
        public static UnityEngine.GUILayoutOption.Type maxHeight;  // const
        public static UnityEngine.GUILayoutOption.Type stretchWidth;  // const
        public static UnityEngine.GUILayoutOption.Type stretchHeight;  // const
        public static UnityEngine.GUILayoutOption.Type alignStart;  // const
        public static UnityEngine.GUILayoutOption.Type alignMiddle;  // const
        public static UnityEngine.GUILayoutOption.Type alignEnd;  // const
        public static UnityEngine.GUILayoutOption.Type alignJustify;  // const
        public static UnityEngine.GUILayoutOption.Type equalSize;  // const
        public static UnityEngine.GUILayoutOption.Type spacing;  // const

    }

    // TypeToken: 0x2000014  // size: 0x30
    public sealed class LayoutCache
    {
        // Fields
        private System.Int32 <id>k__BackingField;  // 0x10
        private UnityEngine.GUILayoutGroup topLevel;  // 0x18
        private UnityEngineInternal.GenericStack layoutGroups;  // 0x20
        private UnityEngine.GUILayoutGroup windows;  // 0x28

        // Properties
        System.Int32 id { set; /* RVA: 0x014F51F0 */ }

        // Methods
        // RVA: 0x0B40EE8C  token: 0x60000F9
        private System.Void .ctor(System.Int32 instanceID) { }
        // RVA: 0x0B40EBF0  token: 0x60000FA
        public System.Void ResetCursor() { }

    }

    // TypeToken: 0x2000017  // size: 0x80
    public sealed class SkinChangedDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x053DEDC8  token: 0x600013F
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x053DEDB8  token: 0x6000140
        public virtual System.Void Invoke() { }

    }

    // TypeToken: 0x2000029  // size: 0x11
    public sealed struct DblClickSnapping
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static UnityEngine.TextEditor.DblClickSnapping WORDS;  // const
        public static UnityEngine.TextEditor.DblClickSnapping PARAGRAPHS;  // const

    }

    // TypeToken: 0x200002A  // size: 0x14
    public sealed struct CharacterType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.TextEditor.CharacterType LetterLike;  // const
        public static UnityEngine.TextEditor.CharacterType Symbol;  // const
        public static UnityEngine.TextEditor.CharacterType Symbol2;  // const
        public static UnityEngine.TextEditor.CharacterType WhiteSpace;  // const

    }

    // TypeToken: 0x200002B  // size: 0x14
    public sealed struct Direction
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.TextEditor.Direction Forward;  // const
        public static UnityEngine.TextEditor.Direction Backward;  // const

    }

    // TypeToken: 0x200002C  // size: 0x14
    public sealed struct TextEditOp
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.TextEditor.TextEditOp MoveLeft;  // const
        public static UnityEngine.TextEditor.TextEditOp MoveRight;  // const
        public static UnityEngine.TextEditor.TextEditOp MoveUp;  // const
        public static UnityEngine.TextEditor.TextEditOp MoveDown;  // const
        public static UnityEngine.TextEditor.TextEditOp MoveLineStart;  // const
        public static UnityEngine.TextEditor.TextEditOp MoveLineEnd;  // const
        public static UnityEngine.TextEditor.TextEditOp MoveTextStart;  // const
        public static UnityEngine.TextEditor.TextEditOp MoveTextEnd;  // const
        public static UnityEngine.TextEditor.TextEditOp MovePageUp;  // const
        public static UnityEngine.TextEditor.TextEditOp MovePageDown;  // const
        public static UnityEngine.TextEditor.TextEditOp MoveGraphicalLineStart;  // const
        public static UnityEngine.TextEditor.TextEditOp MoveGraphicalLineEnd;  // const
        public static UnityEngine.TextEditor.TextEditOp MoveWordLeft;  // const
        public static UnityEngine.TextEditor.TextEditOp MoveWordRight;  // const
        public static UnityEngine.TextEditor.TextEditOp MoveParagraphForward;  // const
        public static UnityEngine.TextEditor.TextEditOp MoveParagraphBackward;  // const
        public static UnityEngine.TextEditor.TextEditOp MoveToStartOfNextWord;  // const
        public static UnityEngine.TextEditor.TextEditOp MoveToEndOfPreviousWord;  // const
        public static UnityEngine.TextEditor.TextEditOp SelectLeft;  // const
        public static UnityEngine.TextEditor.TextEditOp SelectRight;  // const
        public static UnityEngine.TextEditor.TextEditOp SelectUp;  // const
        public static UnityEngine.TextEditor.TextEditOp SelectDown;  // const
        public static UnityEngine.TextEditor.TextEditOp SelectTextStart;  // const
        public static UnityEngine.TextEditor.TextEditOp SelectTextEnd;  // const
        public static UnityEngine.TextEditor.TextEditOp SelectPageUp;  // const
        public static UnityEngine.TextEditor.TextEditOp SelectPageDown;  // const
        public static UnityEngine.TextEditor.TextEditOp ExpandSelectGraphicalLineStart;  // const
        public static UnityEngine.TextEditor.TextEditOp ExpandSelectGraphicalLineEnd;  // const
        public static UnityEngine.TextEditor.TextEditOp SelectGraphicalLineStart;  // const
        public static UnityEngine.TextEditor.TextEditOp SelectGraphicalLineEnd;  // const
        public static UnityEngine.TextEditor.TextEditOp SelectWordLeft;  // const
        public static UnityEngine.TextEditor.TextEditOp SelectWordRight;  // const
        public static UnityEngine.TextEditor.TextEditOp SelectToEndOfPreviousWord;  // const
        public static UnityEngine.TextEditor.TextEditOp SelectToStartOfNextWord;  // const
        public static UnityEngine.TextEditor.TextEditOp SelectParagraphBackward;  // const
        public static UnityEngine.TextEditor.TextEditOp SelectParagraphForward;  // const
        public static UnityEngine.TextEditor.TextEditOp Delete;  // const
        public static UnityEngine.TextEditor.TextEditOp Backspace;  // const
        public static UnityEngine.TextEditor.TextEditOp DeleteWordBack;  // const
        public static UnityEngine.TextEditor.TextEditOp DeleteWordForward;  // const
        public static UnityEngine.TextEditor.TextEditOp DeleteLineBack;  // const
        public static UnityEngine.TextEditor.TextEditOp Cut;  // const
        public static UnityEngine.TextEditor.TextEditOp Copy;  // const
        public static UnityEngine.TextEditor.TextEditOp Paste;  // const
        public static UnityEngine.TextEditor.TextEditOp SelectAll;  // const
        public static UnityEngine.TextEditor.TextEditOp SelectNone;  // const
        public static UnityEngine.TextEditor.TextEditOp ScrollStart;  // const
        public static UnityEngine.TextEditor.TextEditOp ScrollEnd;  // const
        public static UnityEngine.TextEditor.TextEditOp ScrollPageUp;  // const
        public static UnityEngine.TextEditor.TextEditOp ScrollPageDown;  // const

    }

    // TypeToken: 0x200002D  // size: 0x10
    public sealed class <PrivateImplementationDetails>
    {
        // Methods
        // RVA: 0x0687A850  token: 0x60002DA
        private static System.UInt32 ComputeStringHash(System.String s) { }

    }

namespace Microsoft.CodeAnalysis
{

    // TypeToken: 0x2000002  // size: 0x10
    public sealed class EmbeddedAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000001
        public System.Void .ctor() { }

    }

}

namespace System.Runtime.CompilerServices
{

    // TypeToken: 0x2000003  // size: 0x10
    public sealed class IsReadOnlyAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000002
        public System.Void .ctor() { }

    }

}

namespace UnityEngine
{

    // TypeToken: 0x2000004  // size: 0x18
    public sealed class Event
    {
        // Fields
        private System.IntPtr m_Ptr;  // 0x10
        private static UnityEngine.Event s_Current;  // static @ 0x0
        private static UnityEngine.Event s_MasterEvent;  // static @ 0x8

        // Properties
        UnityEngine.EventType rawType { get; /* RVA: 0x037989A0 */ }
        UnityEngine.Vector2 mousePosition { get; /* RVA: 0x0B3FE568 */ set; /* RVA: 0x0B3FE874 */ }
        UnityEngine.Vector2 delta { get; /* RVA: 0x0B3FE448 */ set; /* RVA: 0x0B3FE794 */ }
        UnityEngine.PointerType pointerType { get; /* RVA: 0x0B3FE58C */ }
        System.Int32 button { get; /* RVA: 0x0B3FE338 */ }
        UnityEngine.EventModifiers modifiers { get; /* RVA: 0x03798C20 */ set; /* RVA: 0x0B3FE7F0 */ }
        System.Single pressure { get; /* RVA: 0x0B3FE5C0 */ }
        System.Int32 clickCount { get; /* RVA: 0x0B3FE36C */ }
        System.Char character { get; /* RVA: 0x03798D00 */ set; /* RVA: 0x0B3FE640 */ }
        UnityEngine.KeyCode keyCode { get; /* RVA: 0x03798C60 */ set; /* RVA: 0x0B3FE7B0 */ }
        System.Int32 displayIndex { get; /* RVA: 0x0B3FE46C */ set; /* RVA: 0x03C2D390 */ }
        UnityEngine.EventType type { get; /* RVA: 0x0B3FE60C */ set; /* RVA: 0x0B3FE890 */ }
        System.String commandName { get; /* RVA: 0x0B3FE3A0 */ set; /* RVA: 0x0B3FE684 */ }
        System.Boolean shift { get; /* RVA: 0x0B3FE5F4 */ }
        System.Boolean control { get; /* RVA: 0x0B3FE3EC */ }
        System.Boolean alt { get; /* RVA: 0x0B3FE320 */ }
        System.Boolean command { get; /* RVA: 0x0B3FE3D4 */ }
        UnityEngine.Event current { get; /* RVA: 0x04DBCF50 */ set; /* RVA: 0x0B3FE6C8 */ }
        System.Boolean isKey { get; /* RVA: 0x0B3FE4D8 */ }
        System.Boolean isMouse { get; /* RVA: 0x0B3FE4F8 */ }
        System.Boolean isDirectManipulationDevice { get; /* RVA: 0x0B3FE4A0 */ }

        // Methods
        // RVA: 0x0B3FCD20  token: 0x6000018
        private System.Void Internal_Use() { }
        // RVA: 0x0405B8C0  token: 0x6000019
        private static System.IntPtr Internal_Create(System.Int32 displayIndex) { }
        // RVA: 0x04A399D0  token: 0x600001A
        private static System.Void Internal_Destroy(System.IntPtr ptr) { }
        // RVA: 0x0B3FCCE0  token: 0x600001B
        public UnityEngine.EventType GetTypeForControl(System.Int32 controlID) { }
        // RVA: 0x0B3FCA7C  token: 0x600001C
        private System.Void CopyFromPtr(System.IntPtr ptr) { }
        // RVA: 0x03798960  token: 0x600001D
        public static System.Boolean PopEvent(UnityEngine.Event outEvent) { }
        // RVA: 0x03C2D350  token: 0x600001E
        private static System.Void Internal_SetNativeEvent(System.IntPtr ptr) { }
        // RVA: 0x03C2D200  token: 0x600001F
        private static System.Void Internal_MakeMasterEventCurrent(System.Int32 displayIndex) { }
        // RVA: 0x0B3FCC44  token: 0x6000020
        private static System.Int32 GetDoubleClickTime() { }
        // RVA: 0x0405B8A0  token: 0x6000021
        public System.Void .ctor() { }
        // RVA: 0x0405AF50  token: 0x6000022
        public System.Void .ctor(System.Int32 displayIndex) { }
        // RVA: 0x04A39930  token: 0x6000023
        protected virtual System.Void Finalize() { }
        // RVA: 0x0B3FCAC0  token: 0x6000024
        private System.Void CopyFrom(UnityEngine.Event e) { }
        // RVA: 0x0B3FCD54  token: 0x600002E
        public static UnityEngine.Event KeyboardEvent(System.String key) { }
        // RVA: 0x0B3FCC70  token: 0x600002F
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B3FCAEC  token: 0x6000030
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0B3FDD00  token: 0x6000031
        public virtual System.String ToString() { }
        // RVA: 0x0B3FE20C  token: 0x6000032
        public System.Void Use() { }
        // RVA: 0x0B3FE524  token: 0x6000033
        private System.Void get_mousePosition_Injected(UnityEngine.Vector2& ret) { }
        // RVA: 0x0B3FE830  token: 0x6000034
        private System.Void set_mousePosition_Injected(UnityEngine.Vector2& value) { }
        // RVA: 0x0B3FE404  token: 0x6000035
        private System.Void get_delta_Injected(UnityEngine.Vector2& ret) { }
        // RVA: 0x0B3FE750  token: 0x6000036
        private System.Void set_delta_Injected(UnityEngine.Vector2& value) { }

    }

    // TypeToken: 0x2000005  // size: 0x14
    public sealed struct EventType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.EventType MouseDown;  // const
        public static UnityEngine.EventType MouseUp;  // const
        public static UnityEngine.EventType MouseMove;  // const
        public static UnityEngine.EventType MouseDrag;  // const
        public static UnityEngine.EventType KeyDown;  // const
        public static UnityEngine.EventType KeyUp;  // const
        public static UnityEngine.EventType ScrollWheel;  // const
        public static UnityEngine.EventType Repaint;  // const
        public static UnityEngine.EventType Layout;  // const
        public static UnityEngine.EventType DragUpdated;  // const
        public static UnityEngine.EventType DragPerform;  // const
        public static UnityEngine.EventType DragExited;  // const
        public static UnityEngine.EventType Ignore;  // const
        public static UnityEngine.EventType Used;  // const
        public static UnityEngine.EventType ValidateCommand;  // const
        public static UnityEngine.EventType ExecuteCommand;  // const
        public static UnityEngine.EventType ContextClick;  // const
        public static UnityEngine.EventType MouseEnterWindow;  // const
        public static UnityEngine.EventType MouseLeaveWindow;  // const
        public static UnityEngine.EventType TouchDown;  // const
        public static UnityEngine.EventType TouchUp;  // const
        public static UnityEngine.EventType TouchMove;  // const
        public static UnityEngine.EventType TouchEnter;  // const
        public static UnityEngine.EventType TouchLeave;  // const
        public static UnityEngine.EventType TouchStationary;  // const
        public static UnityEngine.EventType mouseDown;  // const
        public static UnityEngine.EventType mouseUp;  // const
        public static UnityEngine.EventType mouseMove;  // const
        public static UnityEngine.EventType mouseDrag;  // const
        public static UnityEngine.EventType keyDown;  // const
        public static UnityEngine.EventType keyUp;  // const
        public static UnityEngine.EventType scrollWheel;  // const
        public static UnityEngine.EventType repaint;  // const
        public static UnityEngine.EventType layout;  // const
        public static UnityEngine.EventType dragUpdated;  // const
        public static UnityEngine.EventType dragPerform;  // const
        public static UnityEngine.EventType ignore;  // const
        public static UnityEngine.EventType used;  // const

    }

    // TypeToken: 0x2000006  // size: 0x14
    public sealed struct EventModifiers
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.EventModifiers None;  // const
        public static UnityEngine.EventModifiers Shift;  // const
        public static UnityEngine.EventModifiers Control;  // const
        public static UnityEngine.EventModifiers Alt;  // const
        public static UnityEngine.EventModifiers Command;  // const
        public static UnityEngine.EventModifiers Numeric;  // const
        public static UnityEngine.EventModifiers CapsLock;  // const
        public static UnityEngine.EventModifiers FunctionKey;  // const

    }

    // TypeToken: 0x2000007  // size: 0x14
    public sealed struct PointerType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.PointerType Mouse;  // const
        public static UnityEngine.PointerType Touch;  // const
        public static UnityEngine.PointerType Pen;  // const

    }

    // TypeToken: 0x2000008  // size: 0x13
    public sealed struct EventInterests
    {
        // Fields
        private System.Boolean <wantsMouseMove>k__BackingField;  // 0x10
        private System.Boolean <wantsMouseEnterLeaveWindow>k__BackingField;  // 0x11
        private System.Boolean <wantsLessLayoutEvents>k__BackingField;  // 0x12

        // Properties
        System.Boolean wantsMouseMove { get; /* RVA: 0x04D883E0 */ set; /* RVA: 0x04D87180 */ }
        System.Boolean wantsMouseEnterLeaveWindow { get; /* RVA: 0x04D91120 */ set; /* RVA: 0x04D91150 */ }
        System.Boolean wantsLessLayoutEvents { get; /* RVA: 0x04D91110 */ }

        // Methods
        // RVA: 0x0B3FC9EC  token: 0x600003C
        public System.Boolean WantsEvent(UnityEngine.EventType type) { }
        // RVA: 0x0B3FCA08  token: 0x600003D
        public System.Boolean WantsLayoutPass(UnityEngine.EventType type) { }

    }

    // TypeToken: 0x2000009  // size: 0x10
    public class GUI
    {
        // Fields
        private static System.Int32 s_ScrollControlId;  // static @ 0x0
        private static System.Int32 s_HotTextField;  // static @ 0x4
        private static readonly System.Int32 s_BoxHash;  // static @ 0x8
        private static readonly System.Int32 s_ButonHash;  // static @ 0xc
        private static readonly System.Int32 s_RepeatButtonHash;  // static @ 0x10
        private static readonly System.Int32 s_ToggleHash;  // static @ 0x14
        private static readonly System.Int32 s_ButtonGridHash;  // static @ 0x18
        private static readonly System.Int32 s_SliderHash;  // static @ 0x1c
        private static readonly System.Int32 s_BeginGroupHash;  // static @ 0x20
        private static readonly System.Int32 s_ScrollviewHash;  // static @ 0x24
        private static System.Int32 <scrollTroughSide>k__BackingField;  // static @ 0x28
        private static System.DateTime <nextScrollStepTime>k__BackingField;  // static @ 0x30
        private static UnityEngine.GUISkin s_Skin;  // static @ 0x38
        private static UnityEngine.Rect s_ToolTipRect;  // static @ 0x40
        private static UnityEngineInternal.GenericStack <scrollViewStates>k__BackingField;  // static @ 0x50

        // Properties
        UnityEngine.Color color { get; /* RVA: 0x0B40E5E4 */ set; /* RVA: 0x0B40E9D8 */ }
        UnityEngine.Color backgroundColor { get; /* RVA: 0x0B40E4E0 */ set; /* RVA: 0x0B40E924 */ }
        UnityEngine.Color contentColor { get; /* RVA: 0x0B40E664 */ set; /* RVA: 0x0B40EA58 */ }
        System.Boolean changed { get; /* RVA: 0x0B40E584 */ set; /* RVA: 0x0B40E970 */ }
        System.Boolean enabled { get; /* RVA: 0x0B40E6B0 */ set; /* RVA: 0x0B40EAA4 */ }
        System.Boolean usePageScrollbars { get; /* RVA: 0x0B40E8C4 */ }
        UnityEngine.Material blendMaterial { get; /* RVA: 0x0B40E52C */ }
        UnityEngine.Material blitMaterial { get; /* RVA: 0x0B40E558 */ }
        UnityEngine.Material roundedRectMaterial { get; /* RVA: 0x0B40E774 */ }
        UnityEngine.Material roundedRectWithColorPerBorderMaterial { get; /* RVA: 0x0B40E7A0 */ }
        System.Int32 scrollTroughSide { get; /* RVA: 0x0B40E7CC */ set; /* RVA: 0x0B40EB68 */ }
        System.DateTime nextScrollStepTime { get; /* RVA: 0x0B40E724 */ set; /* RVA: 0x0B40EB10 */ }
        UnityEngine.GUISkin skin { get; /* RVA: 0x0B40E86C */ set; /* RVA: 0x0B40EBBC */ }
        UnityEngine.Matrix4x4 matrix { get; /* RVA: 0x0B40E6DC */ set; /* RVA: 0x0B40EAD8 */ }
        UnityEngineInternal.GenericStack scrollViewStates { get; /* RVA: 0x0B40E81C */ }

        // Methods
        // RVA: 0x0B40C4F4  token: 0x600004D
        public static System.Void GrabMouseControl(System.Int32 id) { }
        // RVA: 0x0B40D090  token: 0x600004E
        public static System.Boolean HasMouseControl(System.Int32 id) { }
        // RVA: 0x0B40D5F0  token: 0x600004F
        public static System.Void ReleaseMouseControl() { }
        // RVA: 0x0B40D294  token: 0x6000050
        private static System.Void InternalRepaintEditorWindow() { }
        // RVA: 0x0B40D320  token: 0x6000051
        private static UnityEngine.Rect Internal_DoWindow(System.Int32 id, System.Int32 instanceID, UnityEngine.Rect clientRect, UnityEngine.GUI.WindowFunction func, UnityEngine.GUIContent title, UnityEngine.GUIStyle style, System.Object skin, System.Boolean forceRectOnLayout) { }
        // RVA: 0x0B40B1C4  token: 0x6000052
        public static System.Void DragWindow(UnityEngine.Rect position) { }
        // RVA: 0x0B40E24C  token: 0x6000053
        private static System.Void .cctor() { }
        // RVA: 0x0B40AB68  token: 0x600005A
        private static System.Void DoSetSkin(UnityEngine.GUISkin newSkin) { }
        // RVA: 0x0B40D444  token: 0x600005D
        public static System.Void Label(UnityEngine.Rect position, System.String text) { }
        // RVA: 0x0B40D3D8  token: 0x600005E
        public static System.Void Label(UnityEngine.Rect position, System.String text, UnityEngine.GUIStyle style) { }
        // RVA: 0x0B40D4E8  token: 0x600005F
        public static System.Void Label(UnityEngine.Rect position, UnityEngine.GUIContent content, UnityEngine.GUIStyle style) { }
        // RVA: 0x0B40BBD8  token: 0x6000060
        public static System.Void DrawTexture(UnityEngine.Rect position, UnityEngine.Texture image) { }
        // RVA: 0x0B40BB64  token: 0x6000061
        public static System.Void DrawTexture(UnityEngine.Rect position, UnityEngine.Texture image, UnityEngine.ScaleMode scaleMode) { }
        // RVA: 0x0B40B210  token: 0x6000062
        public static System.Void DrawTexture(UnityEngine.Rect position, UnityEngine.Texture image, UnityEngine.ScaleMode scaleMode, System.Boolean alphaBlend) { }
        // RVA: 0x0B40B99C  token: 0x6000063
        public static System.Void DrawTexture(UnityEngine.Rect position, UnityEngine.Texture image, UnityEngine.ScaleMode scaleMode, System.Boolean alphaBlend, System.Single imageAspect) { }
        // RVA: 0x0B40BA5C  token: 0x6000064
        public static System.Void DrawTexture(UnityEngine.Rect position, UnityEngine.Texture image, UnityEngine.ScaleMode scaleMode, System.Boolean alphaBlend, System.Single imageAspect, UnityEngine.Color color, System.Single borderWidth, System.Single borderRadius) { }
        // RVA: 0x0B40B29C  token: 0x6000065
        public static System.Void DrawTexture(UnityEngine.Rect position, UnityEngine.Texture image, UnityEngine.ScaleMode scaleMode, System.Boolean alphaBlend, System.Single imageAspect, UnityEngine.Color color, UnityEngine.Vector4 borderWidths, System.Single borderRadius) { }
        // RVA: 0x0B40B8A8  token: 0x6000066
        public static System.Void DrawTexture(UnityEngine.Rect position, UnityEngine.Texture image, UnityEngine.ScaleMode scaleMode, System.Boolean alphaBlend, System.Single imageAspect, UnityEngine.Color color, UnityEngine.Vector4 borderWidths, UnityEngine.Vector4 borderRadiuses) { }
        // RVA: 0x0B40B79C  token: 0x6000067
        private static System.Void DrawTexture(UnityEngine.Rect position, UnityEngine.Texture image, UnityEngine.ScaleMode scaleMode, System.Boolean alphaBlend, System.Single imageAspect, UnityEngine.Color color, UnityEngine.Vector4 borderWidths, UnityEngine.Vector4 borderRadiuses, System.Boolean drawSmoothCorners) { }
        // RVA: 0x0B40B3A0  token: 0x6000068
        private static System.Void DrawTexture(UnityEngine.Rect position, UnityEngine.Texture image, UnityEngine.ScaleMode scaleMode, System.Boolean alphaBlend, System.Single imageAspect, UnityEngine.Color leftColor, UnityEngine.Color topColor, UnityEngine.Color rightColor, UnityEngine.Color bottomColor, UnityEngine.Vector4 borderWidths, UnityEngine.Vector4 borderRadiuses, System.Boolean drawSmoothCorners) { }
        // RVA: 0x0B40A0D8  token: 0x6000069
        private static System.Boolean CalculateScaledTextureRects(UnityEngine.Rect position, UnityEngine.ScaleMode scaleMode, System.Single imageAspect, UnityEngine.Rect& outScreenRect, UnityEngine.Rect& outSourceRect) { }
        // RVA: 0x0B409E38  token: 0x600006A
        public static System.Void Box(UnityEngine.Rect position, System.String text) { }
        // RVA: 0x0B409D50  token: 0x600006B
        public static System.Void Box(UnityEngine.Rect position, UnityEngine.GUIContent content, UnityEngine.GUIStyle style) { }
        // RVA: 0x0B409EDC  token: 0x600006C
        public static System.Boolean Button(UnityEngine.Rect position, System.String text) { }
        // RVA: 0x0B409F58  token: 0x600006D
        public static System.Boolean Button(UnityEngine.Rect position, System.String text, UnityEngine.GUIStyle style) { }
        // RVA: 0x0B409FEC  token: 0x600006E
        public static System.Boolean Button(UnityEngine.Rect position, UnityEngine.GUIContent content, UnityEngine.GUIStyle style) { }
        // RVA: 0x0B40A070  token: 0x600006F
        private static System.Boolean Button(UnityEngine.Rect position, System.Int32 id, UnityEngine.GUIContent content, UnityEngine.GUIStyle style) { }
        // RVA: 0x0B40A954  token: 0x6000070
        private static System.Boolean DoRepeatButton(UnityEngine.Rect position, UnityEngine.GUIContent content, UnityEngine.GUIStyle style, UnityEngine.FocusType focusType) { }
        // RVA: 0x0B40DD34  token: 0x6000071
        public static System.String TextField(UnityEngine.Rect position, System.String text) { }
        // RVA: 0x0B40D548  token: 0x6000072
        private static System.String PasswordFieldGetStrToShow(System.String password, System.Char maskChar) { }
        // RVA: 0x0B40ABE4  token: 0x6000073
        private static System.Void DoTextField(UnityEngine.Rect position, System.Int32 id, UnityEngine.GUIContent content, System.Boolean multiline, System.Int32 maxLength, UnityEngine.GUIStyle style) { }
        // RVA: 0x0B40AF10  token: 0x6000074
        private static System.Void DoTextField(UnityEngine.Rect position, System.Int32 id, UnityEngine.GUIContent content, System.Boolean multiline, System.Int32 maxLength, UnityEngine.GUIStyle style, System.String secureText) { }
        // RVA: 0x0B40AC80  token: 0x6000075
        private static System.Void DoTextField(UnityEngine.Rect position, System.Int32 id, UnityEngine.GUIContent content, System.Boolean multiline, System.Int32 maxLength, UnityEngine.GUIStyle style, System.String secureText, System.Char maskChar) { }
        // RVA: 0x0B40CD34  token: 0x6000076
        private static System.Void HandleTextFieldEventForTouchscreen(UnityEngine.Rect position, System.Int32 id, UnityEngine.GUIContent content, System.Boolean multiline, System.Int32 maxLength, UnityEngine.GUIStyle style, System.String secureText, System.Char maskChar, UnityEngine.TextEditor editor) { }
        // RVA: 0x0B40C7B0  token: 0x6000077
        private static System.Void HandleTextFieldEventForDesktop(UnityEngine.Rect position, System.Int32 id, UnityEngine.GUIContent content, System.Boolean multiline, System.Int32 maxLength, UnityEngine.GUIStyle style, UnityEngine.TextEditor editor) { }
        // RVA: 0x0B40C528  token: 0x6000078
        private static System.Void HandleTextFieldEventForDesktopWithForcedKeyboard(UnityEngine.Rect position, System.Int32 id, UnityEngine.GUIContent content, System.Boolean multiline, System.Int32 maxLength, UnityEngine.GUIStyle style, System.String secureText, UnityEngine.TextEditor editor) { }
        // RVA: 0x0B40DE0C  token: 0x6000079
        public static System.Boolean Toggle(UnityEngine.Rect position, System.Boolean value, System.String text) { }
        // RVA: 0x0B40DEC4  token: 0x600007A
        public static System.Boolean Toggle(UnityEngine.Rect position, System.Boolean value, UnityEngine.GUIContent content, UnityEngine.GUIStyle style) { }
        // RVA: 0x0B40A514  token: 0x600007B
        private static System.Boolean DoControl(UnityEngine.Rect position, System.Int32 id, System.Boolean on, System.Boolean hover, UnityEngine.GUIContent content, UnityEngine.GUIStyle style) { }
        // RVA: 0x0B40A770  token: 0x600007C
        private static System.Void DoLabel(UnityEngine.Rect position, UnityEngine.GUIContent content, UnityEngine.GUIStyle style) { }
        // RVA: 0x0B40AFC8  token: 0x600007D
        private static System.Boolean DoToggle(UnityEngine.Rect position, System.Int32 id, System.Boolean value, UnityEngine.GUIContent content, UnityEngine.GUIStyle style) { }
        // RVA: 0x0B40A464  token: 0x600007E
        private static System.Boolean DoButton(UnityEngine.Rect position, System.Int32 id, UnityEngine.GUIContent content, UnityEngine.GUIStyle style) { }
        // RVA: 0x0B40DBE4  token: 0x600007F
        public static System.Single Slider(UnityEngine.Rect position, System.Single value, System.Single size, System.Single start, System.Single end, UnityEngine.GUIStyle slider, UnityEngine.GUIStyle thumb, System.Boolean horiz, System.Int32 id, UnityEngine.GUIStyle thumbExtent) { }
        // RVA: 0x0B40D0C4  token: 0x6000080
        public static System.Single HorizontalScrollbar(UnityEngine.Rect position, System.Single value, System.Single size, System.Single leftValue, System.Single rightValue, UnityEngine.GUIStyle style) { }
        // RVA: 0x0B40D61C  token: 0x6000081
        private static System.Boolean ScrollerRepeatButton(System.Int32 scrollerID, UnityEngine.Rect rect, UnityEngine.GUIStyle style) { }
        // RVA: 0x0B40DF7C  token: 0x6000082
        public static System.Single VerticalScrollbar(UnityEngine.Rect position, System.Single value, System.Single size, System.Single topValue, System.Single bottomValue, UnityEngine.GUIStyle style) { }
        // RVA: 0x0B40D7D4  token: 0x6000083
        private static System.Single Scroller(UnityEngine.Rect position, System.Single value, System.Single size, System.Single leftValue, System.Single rightValue, UnityEngine.GUIStyle slider, UnityEngine.GUIStyle thumb, UnityEngine.GUIStyle leftButton, UnityEngine.GUIStyle rightButton, System.Boolean horiz) { }
        // RVA: 0x0B409294  token: 0x6000084
        public static System.Void BeginGroup(UnityEngine.Rect position, UnityEngine.GUIContent content, UnityEngine.GUIStyle style) { }
        // RVA: 0x0B4090F0  token: 0x6000085
        private static System.Void BeginGroup(UnityEngine.Rect position, UnityEngine.GUIContent content, UnityEngine.GUIStyle style, UnityEngine.Vector2 scrollOffset) { }
        // RVA: 0x0B40BC38  token: 0x6000086
        public static System.Void EndGroup() { }
        // RVA: 0x0B409C5C  token: 0x6000088
        public static UnityEngine.Vector2 BeginScrollView(UnityEngine.Rect position, UnityEngine.Vector2 scrollPosition, UnityEngine.Rect viewRect) { }
        // RVA: 0x0B40931C  token: 0x6000089
        private static UnityEngine.Vector2 BeginScrollView(UnityEngine.Rect position, UnityEngine.Vector2 scrollPosition, UnityEngine.Rect viewRect, System.Boolean alwaysShowHorizontal, System.Boolean alwaysShowVertical, UnityEngine.GUIStyle horizontalScrollbar, UnityEngine.GUIStyle verticalScrollbar, UnityEngine.GUIStyle background) { }
        // RVA: 0x0B40BC50  token: 0x600008A
        public static System.Void EndScrollView() { }
        // RVA: 0x0B40BC94  token: 0x600008B
        public static System.Void EndScrollView(System.Boolean handleScrollWheel) { }
        // RVA: 0x0B40E14C  token: 0x600008C
        public static UnityEngine.Rect Window(System.Int32 id, UnityEngine.Rect clientRect, UnityEngine.GUI.WindowFunction func, System.String text) { }
        // RVA: 0x0B40B09C  token: 0x600008D
        private static UnityEngine.Rect DoWindow(System.Int32 id, UnityEngine.Rect clientRect, UnityEngine.GUI.WindowFunction func, UnityEngine.GUIContent title, UnityEngine.GUIStyle style, UnityEngine.GUISkin skin, System.Boolean forceRectOnLayout) { }
        // RVA: 0x0B40A26C  token: 0x600008E
        private static System.Void CallWindowDelegate(UnityEngine.GUI.WindowFunction func, System.Int32 id, System.Int32 instanceID, UnityEngine.GUISkin _skin, System.Int32 forceRect, System.Single width, System.Single height, UnityEngine.GUIStyle style) { }
        // RVA: 0x0B40E5B0  token: 0x600008F
        private static System.Void get_color_Injected(UnityEngine.Color& ret) { }
        // RVA: 0x0B40E9A4  token: 0x6000090
        private static System.Void set_color_Injected(UnityEngine.Color& value) { }
        // RVA: 0x0B40E4AC  token: 0x6000091
        private static System.Void get_backgroundColor_Injected(UnityEngine.Color& ret) { }
        // RVA: 0x0B40E8F0  token: 0x6000092
        private static System.Void set_backgroundColor_Injected(UnityEngine.Color& value) { }
        // RVA: 0x0B40E630  token: 0x6000093
        private static System.Void get_contentColor_Injected(UnityEngine.Color& ret) { }
        // RVA: 0x0B40EA24  token: 0x6000094
        private static System.Void set_contentColor_Injected(UnityEngine.Color& value) { }
        // RVA: 0x0B40D2C0  token: 0x6000095
        private static System.Void Internal_DoWindow_Injected(System.Int32 id, System.Int32 instanceID, UnityEngine.Rect& clientRect, UnityEngine.GUI.WindowFunction func, UnityEngine.GUIContent title, UnityEngine.GUIStyle style, System.Object skin, System.Boolean forceRectOnLayout, UnityEngine.Rect& ret) { }
        // RVA: 0x0B40B190  token: 0x6000096
        private static System.Void DragWindow_Injected(UnityEngine.Rect& position) { }

    }

    // TypeToken: 0x200000B  // size: 0x10
    public sealed class GUIClip
    {
        // Properties
        UnityEngine.Rect visibleRect { get; /* RVA: 0x0B3FEDEC */ }

        // Methods
        // RVA: 0x0B3FEC1C  token: 0x600009A
        private static System.Void Internal_Push(UnityEngine.Rect screenRect, UnityEngine.Vector2 scrollOffset, UnityEngine.Vector2 renderOffset, System.Boolean resetOffset) { }
        // RVA: 0x0B3FEAB0  token: 0x600009B
        private static System.Void Internal_Pop() { }
        // RVA: 0x0B3FEA58  token: 0x600009C
        private static System.Int32 Internal_GetCount() { }
        // RVA: 0x0B3FED7C  token: 0x600009D
        private static UnityEngine.Vector2 Unclip_Vector2(UnityEngine.Vector2 pos) { }
        // RVA: 0x0B3FECFC  token: 0x600009E
        private static UnityEngine.Vector2 UnclipToWindow_Vector2(UnityEngine.Vector2 pos) { }
        // RVA: 0x0B3FE9BC  token: 0x600009F
        private static UnityEngine.Vector2 ClipToWindow_Vector2(UnityEngine.Vector2 absolutePos) { }
        // RVA: 0x0B3FEA2C  token: 0x60000A0
        private static UnityEngine.Matrix4x4 GetMatrix() { }
        // RVA: 0x0B3FECA8  token: 0x60000A1
        private static System.Void SetMatrix(UnityEngine.Matrix4x4 m) { }
        // RVA: 0x0B3FEB30  token: 0x60000A2
        private static System.Void Internal_PushParentClip(UnityEngine.Matrix4x4 objectTransform, UnityEngine.Rect clipRect) { }
        // RVA: 0x0B3FEBA4  token: 0x60000A3
        private static System.Void Internal_PushParentClip(UnityEngine.Matrix4x4 renderTransform, UnityEngine.Matrix4x4 inputTransform, UnityEngine.Rect clipRect) { }
        // RVA: 0x0B3FEA84  token: 0x60000A4
        private static System.Void Internal_PopParentClip() { }
        // RVA: 0x0B3FEC50  token: 0x60000A5
        private static System.Void Push(UnityEngine.Rect screenRect, UnityEngine.Vector2 scrollOffset, UnityEngine.Vector2 renderOffset, System.Boolean resetOffset) { }
        // RVA: 0x0B3FEC48  token: 0x60000A6
        private static System.Void Pop() { }
        // RVA: 0x0B3FEDA8  token: 0x60000A7
        public static UnityEngine.Vector2 Unclip(UnityEngine.Vector2 pos) { }
        // RVA: 0x0B3FED28  token: 0x60000A8
        public static UnityEngine.Vector2 UnclipToWindow(UnityEngine.Vector2 pos) { }
        // RVA: 0x0B3FE9E8  token: 0x60000A9
        public static UnityEngine.Vector2 ClipToWindow(UnityEngine.Vector2 absolutePos) { }
        // RVA: 0x0B3FEDB8  token: 0x60000AA
        private static System.Void get_visibleRect_Injected(UnityEngine.Rect& ret) { }
        // RVA: 0x0B3FEBB8  token: 0x60000AB
        private static System.Void Internal_Push_Injected(UnityEngine.Rect& screenRect, UnityEngine.Vector2& scrollOffset, UnityEngine.Vector2& renderOffset, System.Boolean resetOffset) { }
        // RVA: 0x0B3FED38  token: 0x60000AC
        private static System.Void Unclip_Vector2_Injected(UnityEngine.Vector2& pos, UnityEngine.Vector2& ret) { }
        // RVA: 0x0B3FECB8  token: 0x60000AD
        private static System.Void UnclipToWindow_Vector2_Injected(UnityEngine.Vector2& pos, UnityEngine.Vector2& ret) { }
        // RVA: 0x0B3FE978  token: 0x60000AE
        private static System.Void ClipToWindow_Vector2_Injected(UnityEngine.Vector2& absolutePos, UnityEngine.Vector2& ret) { }
        // RVA: 0x0B3FE9F8  token: 0x60000AF
        private static System.Void GetMatrix_Injected(UnityEngine.Matrix4x4& ret) { }
        // RVA: 0x0B3FEC74  token: 0x60000B0
        private static System.Void SetMatrix_Injected(UnityEngine.Matrix4x4& m) { }
        // RVA: 0x0B3FEADC  token: 0x60000B1
        private static System.Void Internal_PushParentClip_Injected(UnityEngine.Matrix4x4& renderTransform, UnityEngine.Matrix4x4& inputTransform, UnityEngine.Rect& clipRect) { }

    }

    // TypeToken: 0x200000D  // size: 0x28
    public class GUIContent
    {
        // Fields
        private System.String m_Text;  // 0x10
        private UnityEngine.Texture m_Image;  // 0x18
        private System.String m_Tooltip;  // 0x20
        private static readonly UnityEngine.GUIContent s_Text;  // static @ 0x0
        private static readonly UnityEngine.GUIContent s_Image;  // static @ 0x8
        private static readonly UnityEngine.GUIContent s_TextImage;  // static @ 0x10
        public static UnityEngine.GUIContent none;  // static @ 0x18

        // Properties
        System.String text { get; /* RVA: 0x04D9B600 */ set; /* RVA: 0x053908C0 */ }
        UnityEngine.Texture image { set; /* RVA: 0x05392C40 */ }
        System.String tooltip { get; /* RVA: 0x04DBA910 */ set; /* RVA: 0x05390F40 */ }
        System.Int32 hash { get; /* RVA: 0x0B3FF38C */ }

        // Methods
        // RVA: 0x0B3FF2C8  token: 0x60000B9
        public System.Void .ctor() { }
        // RVA: 0x0B3FF330  token: 0x60000BA
        public System.Void .ctor(System.String text) { }
        // RVA: 0x0B3FF1FC  token: 0x60000BB
        public System.Void .ctor(System.String text, System.String tooltip) { }
        // RVA: 0x0B3FF144  token: 0x60000BC
        public System.Void .ctor(System.String text, UnityEngine.Texture image, System.String tooltip) { }
        // RVA: 0x0B3FF21C  token: 0x60000BD
        public System.Void .ctor(UnityEngine.GUIContent src) { }
        // RVA: 0x0B3FEF30  token: 0x60000BF
        private static UnityEngine.GUIContent Temp(System.String t) { }
        // RVA: 0x0B3FEE0C  token: 0x60000C0
        private static System.Void ClearStaticCache() { }
        // RVA: 0x0B3FEFBC  token: 0x60000C1
        public virtual System.String ToString() { }
        // RVA: 0x0B3FEFE0  token: 0x60000C2
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000E  // size: 0x14
    public sealed struct ScaleMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.ScaleMode StretchToFill;  // const
        public static UnityEngine.ScaleMode ScaleAndCrop;  // const
        public static UnityEngine.ScaleMode ScaleToFit;  // const

    }

    // TypeToken: 0x200000F  // size: 0x14
    public sealed struct FocusType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.FocusType Native;  // const
        public static UnityEngine.FocusType Keyboard;  // const
        public static UnityEngine.FocusType Passive;  // const

    }

    // TypeToken: 0x2000010  // size: 0x10
    public class GUILayout
    {
        // Methods
        // RVA: 0x0B403054  token: 0x60000C3
        public static System.Void Label(System.String text, UnityEngine.GUILayoutOption[] options) { }
        // RVA: 0x0B4030E0  token: 0x60000C4
        public static System.Void Label(System.String text, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options) { }
        // RVA: 0x0B402958  token: 0x60000C5
        private static System.Void DoLabel(UnityEngine.GUIContent content, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options) { }
        // RVA: 0x0B403328  token: 0x60000C6
        public static System.Boolean Toggle(System.Boolean value, System.String text, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options) { }
        // RVA: 0x0B4033A8  token: 0x60000C7
        public static System.Boolean Toggle(System.Boolean value, UnityEngine.GUIContent content, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options) { }
        // RVA: 0x0B402A00  token: 0x60000C8
        private static System.Boolean DoToggle(System.Boolean value, UnityEngine.GUIContent content, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options) { }
        // RVA: 0x0B40314C  token: 0x60000C9
        public static System.Void Space(System.Single pixels) { }
        // RVA: 0x0B402DC8  token: 0x60000CA
        public static System.Void FlexibleSpace() { }
        // RVA: 0x0B40227C  token: 0x60000CB
        public static System.Void BeginHorizontal(UnityEngine.GUILayoutOption[] options) { }
        // RVA: 0x0B402300  token: 0x60000CC
        public static System.Void BeginHorizontal(UnityEngine.GUIContent content, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options) { }
        // RVA: 0x0B402BDC  token: 0x60000CD
        public static System.Void EndHorizontal() { }
        // RVA: 0x0B402870  token: 0x60000CE
        public static System.Void BeginVertical(UnityEngine.GUILayoutOption[] options) { }
        // RVA: 0x0B4028F4  token: 0x60000CF
        public static System.Void BeginVertical(UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options) { }
        // RVA: 0x0B40272C  token: 0x60000D0
        public static System.Void BeginVertical(UnityEngine.GUIContent content, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options) { }
        // RVA: 0x0B402C88  token: 0x60000D1
        public static System.Void EndVertical() { }
        // RVA: 0x0B402068  token: 0x60000D2
        public static System.Void BeginArea(UnityEngine.Rect screenRect) { }
        // RVA: 0x0B4020F8  token: 0x60000D3
        public static System.Void BeginArea(UnityEngine.Rect screenRect, UnityEngine.GUIContent content, UnityEngine.GUIStyle style) { }
        // RVA: 0x0B402ABC  token: 0x60000D4
        public static System.Void EndArea() { }
        // RVA: 0x0B402444  token: 0x60000D5
        public static UnityEngine.Vector2 BeginScrollView(UnityEngine.Vector2 scrollPosition, UnityEngine.GUILayoutOption[] options) { }
        // RVA: 0x0B402504  token: 0x60000D6
        public static UnityEngine.Vector2 BeginScrollView(UnityEngine.Vector2 scrollPosition, System.Boolean alwaysShowHorizontal, System.Boolean alwaysShowVertical, UnityEngine.GUIStyle horizontalScrollbar, UnityEngine.GUIStyle verticalScrollbar, UnityEngine.GUIStyle background, UnityEngine.GUILayoutOption[] options) { }
        // RVA: 0x0B402C1C  token: 0x60000D7
        public static System.Void EndScrollView() { }
        // RVA: 0x0B402C28  token: 0x60000D8
        private static System.Void EndScrollView(System.Boolean handleScrollWheel) { }
        // RVA: 0x0B4033B8  token: 0x60000D9
        public static UnityEngine.GUILayoutOption Width(System.Single width) { }
        // RVA: 0x0B402FC8  token: 0x60000DA
        public static UnityEngine.GUILayoutOption Height(System.Single height) { }
        // RVA: 0x0B402D48  token: 0x60000DB
        public static UnityEngine.GUILayoutOption ExpandWidth(System.Boolean expand) { }
        // RVA: 0x0B402CC8  token: 0x60000DC
        public static UnityEngine.GUILayoutOption ExpandHeight(System.Boolean expand) { }

    }

    // TypeToken: 0x2000011  // size: 0x20
    public sealed class GUILayoutOption
    {
        // Fields
        private UnityEngine.GUILayoutOption.Type type;  // 0x10
        private System.Object value;  // 0x18

        // Methods
        // RVA: 0x05391D70  token: 0x60000DD
        private System.Void .ctor(UnityEngine.GUILayoutOption.Type type, System.Object value) { }

    }

    // TypeToken: 0x2000013  // size: 0x10
    public class GUILayoutUtility
    {
        // Fields
        private static readonly System.Collections.Generic.Dictionary<System.Int32,UnityEngine.GUILayoutUtility.LayoutCache> s_StoredLayouts;  // static @ 0x0
        private static readonly System.Collections.Generic.Dictionary<System.Int32,UnityEngine.GUILayoutUtility.LayoutCache> s_StoredWindows;  // static @ 0x8
        private static UnityEngine.GUILayoutUtility.LayoutCache current;  // static @ 0x10
        private static readonly UnityEngine.Rect kDummyRect;  // static @ 0x18
        private static System.Int32 <unbalancedgroupscount>k__BackingField;  // static @ 0x28
        private static UnityEngine.GUIStyle s_SpaceStyle;  // static @ 0x30

        // Properties
        System.Int32 unbalancedgroupscount { get; /* RVA: 0x0B401FC4 */ set; /* RVA: 0x0B402014 */ }
        UnityEngine.GUIStyle spaceStyle { get; /* RVA: 0x0B401ED4 */ }

        // Methods
        // RVA: 0x0B401298  token: 0x60000DE
        private static UnityEngine.Rect Internal_GetWindowRect(System.Int32 windowID) { }
        // RVA: 0x0B401330  token: 0x60000DF
        private static System.Void Internal_MoveWindow(System.Int32 windowID, UnityEngine.Rect r) { }
        // RVA: 0x0B401C08  token: 0x60000E2
        private static UnityEngine.GUILayoutUtility.LayoutCache SelectIDList(System.Int32 instanceID, System.Boolean isWindow) { }
        // RVA: 0x0B4006EC  token: 0x60000E3
        private static System.Void Begin(System.Int32 instanceID) { }
        // RVA: 0x0B3FFD98  token: 0x60000E4
        private static System.Void BeginContainer(UnityEngine.GUILayoutUtility.LayoutCache cache) { }
        // RVA: 0x0B4003E4  token: 0x60000E5
        private static System.Void BeginWindow(System.Int32 windowID, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options) { }
        // RVA: 0x0B4019EC  token: 0x60000E6
        private static System.Void Layout() { }
        // RVA: 0x0B401694  token: 0x60000E7
        private static System.Void LayoutFromEditorWindow() { }
        // RVA: 0x0B4014FC  token: 0x60000E8
        private static System.Void LayoutFromContainer(System.Single w, System.Single h) { }
        // RVA: 0x0B401388  token: 0x60000E9
        private static System.Void LayoutFreeGroup(UnityEngine.GUILayoutGroup toplevel) { }
        // RVA: 0x0B401858  token: 0x60000EA
        private static System.Void LayoutSingleGroup(UnityEngine.GUILayoutGroup i) { }
        // RVA: 0x0B40092C  token: 0x60000EB
        private static UnityEngine.GUILayoutGroup CreateGUILayoutGroupInstanceOfType(System.Type LayoutType) { }
        // RVA: 0x0B400164  token: 0x60000EC
        private static UnityEngine.GUILayoutGroup BeginLayoutGroup(UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options, System.Type layoutType) { }
        // RVA: 0x0B400EA4  token: 0x60000ED
        private static System.Void EndLayoutGroup() { }
        // RVA: 0x0B3FFF24  token: 0x60000EE
        private static UnityEngine.GUILayoutGroup BeginLayoutArea(UnityEngine.GUIStyle style, System.Type layoutType) { }
        // RVA: 0x0B4011D0  token: 0x60000EF
        public static UnityEngine.Rect GetRect(UnityEngine.GUIContent content, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options) { }
        // RVA: 0x0B400BD0  token: 0x60000F0
        private static UnityEngine.Rect DoGetRect(UnityEngine.GUIContent content, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options) { }
        // RVA: 0x0B40112C  token: 0x60000F1
        public static UnityEngine.Rect GetRect(System.Single width, System.Single height, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options) { }
        // RVA: 0x0B400A30  token: 0x60000F2
        private static UnityEngine.Rect DoGetRect(System.Single minWidth, System.Single maxWidth, System.Single minHeight, System.Single maxHeight, UnityEngine.GUIStyle style, UnityEngine.GUILayoutOption[] options) { }
        // RVA: 0x0B401070  token: 0x60000F3
        public static UnityEngine.Rect GetLastRect() { }
        // RVA: 0x0B401D80  token: 0x60000F5
        private static System.Void .cctor() { }
        // RVA: 0x0B401258  token: 0x60000F6
        private static System.Void Internal_GetWindowRect_Injected(System.Int32 windowID, UnityEngine.Rect& ret) { }
        // RVA: 0x0B4012F0  token: 0x60000F7
        private static System.Void Internal_MoveWindow_Injected(System.Int32 windowID, UnityEngine.Rect& r) { }

    }

    // TypeToken: 0x2000015  // size: 0x38
    public sealed class GUISettings
    {
        // Fields
        private System.Boolean m_DoubleClickSelectsWord;  // 0x10
        private System.Boolean m_TripleClickSelectsLine;  // 0x11
        private UnityEngine.Color m_CursorColor;  // 0x14
        private System.Single m_CursorFlashSpeed;  // 0x24
        private UnityEngine.Color m_SelectionColor;  // 0x28

        // Properties
        System.Boolean doubleClickSelectsWord { get; /* RVA: 0x04DBCFA0 */ }
        System.Boolean tripleClickSelectsLine { get; /* RVA: 0x04DBCFC0 */ }
        UnityEngine.Color cursorColor { get; /* RVA: 0x04D95490 */ }
        System.Single cursorFlashSpeed { get; /* RVA: 0x0B4034A8 */ }
        UnityEngine.Color selectionColor { get; /* RVA: 0x04DBCFB0 */ }

        // Methods
        // RVA: 0x0B403444  token: 0x60000FB
        private static System.Single Internal_GetCursorFlashSpeed() { }
        // RVA: 0x0B403470  token: 0x6000101
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000016  // size: 0xF0
    public sealed class GUISkin : UnityEngine.ScriptableObject
    {
        // Fields
        private UnityEngine.Font m_Font;  // 0x18
        private UnityEngine.GUIStyle m_box;  // 0x20
        private UnityEngine.GUIStyle m_button;  // 0x28
        private UnityEngine.GUIStyle m_toggle;  // 0x30
        private UnityEngine.GUIStyle m_label;  // 0x38
        private UnityEngine.GUIStyle m_textField;  // 0x40
        private UnityEngine.GUIStyle m_textArea;  // 0x48
        private UnityEngine.GUIStyle m_window;  // 0x50
        private UnityEngine.GUIStyle m_horizontalSlider;  // 0x58
        private UnityEngine.GUIStyle m_horizontalSliderThumb;  // 0x60
        private UnityEngine.GUIStyle m_horizontalSliderThumbExtent;  // 0x68
        private UnityEngine.GUIStyle m_verticalSlider;  // 0x70
        private UnityEngine.GUIStyle m_verticalSliderThumb;  // 0x78
        private UnityEngine.GUIStyle m_verticalSliderThumbExtent;  // 0x80
        private UnityEngine.GUIStyle m_SliderMixed;  // 0x88
        private UnityEngine.GUIStyle m_horizontalScrollbar;  // 0x90
        private UnityEngine.GUIStyle m_horizontalScrollbarThumb;  // 0x98
        private UnityEngine.GUIStyle m_horizontalScrollbarLeftButton;  // 0xa0
        private UnityEngine.GUIStyle m_horizontalScrollbarRightButton;  // 0xa8
        private UnityEngine.GUIStyle m_verticalScrollbar;  // 0xb0
        private UnityEngine.GUIStyle m_verticalScrollbarThumb;  // 0xb8
        private UnityEngine.GUIStyle m_verticalScrollbarUpButton;  // 0xc0
        private UnityEngine.GUIStyle m_verticalScrollbarDownButton;  // 0xc8
        private UnityEngine.GUIStyle m_ScrollView;  // 0xd0
        private UnityEngine.GUIStyle[] m_CustomStyles;  // 0xd8
        private UnityEngine.GUISettings m_Settings;  // 0xe0
        private static UnityEngine.GUIStyle ms_Error;  // static @ 0x0
        private System.Collections.Generic.Dictionary<System.String,UnityEngine.GUIStyle> m_Styles;  // 0xe8
        private static UnityEngine.GUISkin.SkinChangedDelegate m_SkinChanged;  // static @ 0x8
        private static UnityEngine.GUISkin current;  // static @ 0x10

        // Properties
        UnityEngine.Font font { get; /* RVA: 0x04D96330 */ set; /* RVA: 0x0B4049DC */ }
        UnityEngine.GUIStyle box { get; /* RVA: 0x04DBA910 */ set; /* RVA: 0x0B40496C */ }
        UnityEngine.GUIStyle label { get; /* RVA: 0x04DBC0B0 */ set; /* RVA: 0x0B404B98 */ }
        UnityEngine.GUIStyle textField { get; /* RVA: 0x04DBCF90 */ set; /* RVA: 0x0B404C30 */ }
        UnityEngine.GUIStyle textArea { get; /* RVA: 0x04DBD080 */ set; /* RVA: 0x0B404C0C */ }
        UnityEngine.GUIStyle button { get; /* RVA: 0x04DBBE70 */ set; /* RVA: 0x0B404990 */ }
        UnityEngine.GUIStyle toggle { get; /* RVA: 0x04DBBEB0 */ set; /* RVA: 0x0B404C54 */ }
        UnityEngine.GUIStyle window { get; /* RVA: 0x04DBD100 */ set; /* RVA: 0x0B404D88 */ }
        UnityEngine.GUIStyle horizontalSlider { get; /* RVA: 0x04DBD040 */ set; /* RVA: 0x0B404B74 */ }
        UnityEngine.GUIStyle horizontalSliderThumb { get; /* RVA: 0x04DBD030 */ set; /* RVA: 0x0B404B50 */ }
        UnityEngine.GUIStyle horizontalSliderThumbExtent { get; /* RVA: 0x04DBD020 */ set; /* RVA: 0x0B404B2C */ }
        UnityEngine.GUIStyle sliderMixed { get; /* RVA: 0x04DBD070 */ set; /* RVA: 0x0B404BE4 */ }
        UnityEngine.GUIStyle verticalSlider { get; /* RVA: 0x04DBD0F0 */ set; /* RVA: 0x0B404D64 */ }
        UnityEngine.GUIStyle verticalSliderThumb { get; /* RVA: 0x04DBD0E0 */ set; /* RVA: 0x0B404D40 */ }
        UnityEngine.GUIStyle verticalSliderThumbExtent { get; /* RVA: 0x04DBD0D0 */ set; /* RVA: 0x0B404D18 */ }
        UnityEngine.GUIStyle horizontalScrollbar { get; /* RVA: 0x04DBD010 */ set; /* RVA: 0x0B404B04 */ }
        UnityEngine.GUIStyle horizontalScrollbarThumb { get; /* RVA: 0x04DBD000 */ set; /* RVA: 0x0B404ADC */ }
        UnityEngine.GUIStyle horizontalScrollbarLeftButton { get; /* RVA: 0x04DBCFE0 */ set; /* RVA: 0x0B404A8C */ }
        UnityEngine.GUIStyle horizontalScrollbarRightButton { get; /* RVA: 0x04DBCFF0 */ set; /* RVA: 0x0B404AB4 */ }
        UnityEngine.GUIStyle verticalScrollbar { get; /* RVA: 0x04DBD0C0 */ set; /* RVA: 0x0B404CF0 */ }
        UnityEngine.GUIStyle verticalScrollbarThumb { get; /* RVA: 0x04DBD0A0 */ set; /* RVA: 0x0B404CA0 */ }
        UnityEngine.GUIStyle verticalScrollbarUpButton { get; /* RVA: 0x04DBD0B0 */ set; /* RVA: 0x0B404CC8 */ }
        UnityEngine.GUIStyle verticalScrollbarDownButton { get; /* RVA: 0x04DBD090 */ set; /* RVA: 0x0B404C78 */ }
        UnityEngine.GUIStyle scrollView { get; /* RVA: 0x04DBD050 */ set; /* RVA: 0x0B404BBC */ }
        UnityEngine.GUIStyle[] customStyles { get; /* RVA: 0x04DBCFD0 */ set; /* RVA: 0x0B4049B4 */ }
        UnityEngine.GUISettings settings { get; /* RVA: 0x04DBD060 */ }
        UnityEngine.GUIStyle error { get; /* RVA: 0x0B404898 */ }

        // Methods
        // RVA: 0x0B4047E0  token: 0x6000102
        public System.Void .ctor() { }
        // RVA: 0x0B4047D8  token: 0x6000103
        private System.Void OnEnable() { }
        // RVA: 0x0B404404  token: 0x6000104
        private static System.Void CleanupRoots() { }
        // RVA: 0x0B4034C8  token: 0x6000139
        private System.Void Apply() { }
        // RVA: 0x0B40352C  token: 0x600013A
        private System.Void BuildStyleCache() { }
        // RVA: 0x0B4045B4  token: 0x600013B
        public UnityEngine.GUIStyle GetStyle(System.String styleName) { }
        // RVA: 0x0B404478  token: 0x600013C
        public UnityEngine.GUIStyle FindStyle(System.String styleName) { }
        // RVA: 0x0B404760  token: 0x600013D
        private System.Void MakeCurrent() { }
        // RVA: 0x0B4044FC  token: 0x600013E
        public System.Collections.IEnumerator GetEnumerator() { }

    }

    // TypeToken: 0x2000018  // size: 0x10
    public class GUIStateObjects
    {
        // Fields
        private static System.Collections.Generic.Dictionary<System.Int32,System.Object> s_StateCache;  // static @ 0x0

        // Methods
        // RVA: 0x0B404DAC  token: 0x6000141
        private static System.Object GetStateObject(System.Type t, System.Int32 controlID) { }
        // RVA: 0x0B404EA4  token: 0x6000142
        private static System.Object QueryStateObject(System.Type t, System.Int32 controlID) { }
        // RVA: 0x0B404F3C  token: 0x6000143
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000019  // size: 0x20
    public sealed class GUIStyleState
    {
        // Fields
        private System.IntPtr m_Ptr;  // 0x10
        private readonly UnityEngine.GUIStyle m_SourceStyle;  // 0x18

        // Properties
        UnityEngine.Texture2D background { get; /* RVA: 0x0B405138 */ set; /* RVA: 0x0B4051DC */ }
        UnityEngine.Color textColor { get; /* RVA: 0x0B4051B0 */ set; /* RVA: 0x0B405264 */ }

        // Methods
        // RVA: 0x0B4050F0  token: 0x6000148
        private static System.IntPtr Init() { }
        // RVA: 0x0B404FC8  token: 0x6000149
        private System.Void Cleanup() { }
        // RVA: 0x0B40511C  token: 0x600014A
        public System.Void .ctor() { }
        // RVA: 0x0B3B8428  token: 0x600014B
        private System.Void .ctor(UnityEngine.GUIStyle sourceStyle, System.IntPtr source) { }
        // RVA: 0x0B405080  token: 0x600014C
        private static UnityEngine.GUIStyleState GetGUIStyleState(UnityEngine.GUIStyle sourceStyle, System.IntPtr source) { }
        // RVA: 0x0B404FFC  token: 0x600014D
        protected virtual System.Void Finalize() { }
        // RVA: 0x0B40516C  token: 0x600014E
        private System.Void get_textColor_Injected(UnityEngine.Color& ret) { }
        // RVA: 0x0B405220  token: 0x600014F
        private System.Void set_textColor_Injected(UnityEngine.Color& value) { }

    }

    // TypeToken: 0x200001A  // size: 0x80
    public sealed class GUIStyle
    {
        // Fields
        private System.IntPtr m_Ptr;  // 0x10
        private UnityEngine.GUIStyleState m_Normal;  // 0x18
        private UnityEngine.GUIStyleState m_Hover;  // 0x20
        private UnityEngine.GUIStyleState m_Active;  // 0x28
        private UnityEngine.GUIStyleState m_Focused;  // 0x30
        private UnityEngine.GUIStyleState m_OnNormal;  // 0x38
        private UnityEngine.GUIStyleState m_OnHover;  // 0x40
        private UnityEngine.GUIStyleState m_OnActive;  // 0x48
        private UnityEngine.GUIStyleState m_OnFocused;  // 0x50
        private UnityEngine.RectOffset m_Border;  // 0x58
        private UnityEngine.RectOffset m_Padding;  // 0x60
        private UnityEngine.RectOffset m_Margin;  // 0x68
        private UnityEngine.RectOffset m_Overflow;  // 0x70
        private System.String m_Name;  // 0x78
        private static System.Boolean showKeyboardFocus;  // static @ 0x0
        private static UnityEngine.GUIStyle s_None;  // static @ 0x8

        // Properties
        System.String rawName { get; /* RVA: 0x0B406E80 */ set; /* RVA: 0x0B40742C */ }
        UnityEngine.Font font { get; /* RVA: 0x0B406958 */ set; /* RVA: 0x0B407238 */ }
        UnityEngine.ImagePosition imagePosition { get; /* RVA: 0x0B4069D4 */ set; /* RVA: 0x0B4072A0 */ }
        UnityEngine.TextAnchor alignment { get; /* RVA: 0x0B4066DC */ set; /* RVA: 0x0B407008 */ }
        System.Boolean wordWrap { get; /* RVA: 0x0B406F50 */ set; /* RVA: 0x0B40753C */ }
        UnityEngine.TextClipping clipping { get; /* RVA: 0x0B4067A4 */ set; /* RVA: 0x0B40706C */ }
        UnityEngine.Vector2 contentOffset { get; /* RVA: 0x0B40681C */ set; /* RVA: 0x0B4070F0 */ }
        System.Single fixedWidth { get; /* RVA: 0x0B406874 */ set; /* RVA: 0x0B407150 */ }
        System.Single fixedHeight { get; /* RVA: 0x0B406840 */ set; /* RVA: 0x0B40710C */ }
        System.Boolean stretchWidth { get; /* RVA: 0x0B406F1C */ set; /* RVA: 0x0B4074F8 */ }
        System.Boolean stretchHeight { get; /* RVA: 0x0B406EE8 */ set; /* RVA: 0x0B4074B4 */ }
        System.Int32 fontSize { get; /* RVA: 0x0B4068F0 */ set; /* RVA: 0x0B4071B8 */ }
        UnityEngine.FontStyle fontStyle { get; /* RVA: 0x0B406924 */ set; /* RVA: 0x0B4071F8 */ }
        System.Boolean richText { get; /* RVA: 0x0B406EB4 */ set; /* RVA: 0x0B407470 */ }
        UnityEngine.Vector2 Internal_clipOffset { set; /* RVA: 0x0B406FC8 */ }
        System.String name { get; /* RVA: 0x0B406B40 */ set; /* RVA: 0x0B407304 */ }
        UnityEngine.GUIStyleState normal { get; /* RVA: 0x0B406C14 */ set; /* RVA: 0x0B407330 */ }
        UnityEngine.GUIStyleState hover { get; /* RVA: 0x0B40698C */ set; /* RVA: 0x0B40727C */ }
        UnityEngine.GUIStyleState active { get; /* RVA: 0x0B406694 */ set; /* RVA: 0x0B406FE4 */ }
        UnityEngine.GUIStyleState onNormal { get; /* RVA: 0x0B406D30 */ set; /* RVA: 0x0B4073C0 */ }
        UnityEngine.GUIStyleState onHover { get; /* RVA: 0x0B406CE8 */ set; /* RVA: 0x0B40739C */ }
        UnityEngine.GUIStyleState onActive { get; /* RVA: 0x0B406C58 */ set; /* RVA: 0x0B407354 */ }
        UnityEngine.GUIStyleState focused { get; /* RVA: 0x0B4068A8 */ set; /* RVA: 0x0B407194 */ }
        UnityEngine.GUIStyleState onFocused { get; /* RVA: 0x0B406CA0 */ set; /* RVA: 0x0B407378 */ }
        UnityEngine.RectOffset border { get; /* RVA: 0x0B406710 */ set; /* RVA: 0x0B407048 */ }
        UnityEngine.RectOffset margin { get; /* RVA: 0x0B406AAC */ set; /* RVA: 0x0B4072E0 */ }
        UnityEngine.RectOffset padding { get; /* RVA: 0x0B406E0C */ set; /* RVA: 0x0B407408 */ }
        UnityEngine.RectOffset overflow { get; /* RVA: 0x0B406D78 */ set; /* RVA: 0x0B4073E4 */ }
        System.Single lineHeight { get; /* RVA: 0x0B406A58 */ }
        UnityEngine.GUIStyle none { get; /* RVA: 0x0B406B74 */ }
        System.Boolean isHeightDependantOnWidth { get; /* RVA: 0x0B406A08 */ }

        // Methods
        // RVA: 0x0B405EF0  token: 0x600016D
        private static System.IntPtr Internal_Create(UnityEngine.GUIStyle self) { }
        // RVA: 0x0B405EAC  token: 0x600016E
        private static System.IntPtr Internal_Copy(UnityEngine.GUIStyle self, UnityEngine.GUIStyle other) { }
        // RVA: 0x0B405F24  token: 0x600016F
        private static System.Void Internal_Destroy(System.IntPtr self) { }
        // RVA: 0x0B405C94  token: 0x6000170
        private System.IntPtr GetStyleStatePtr(System.Int32 idx) { }
        // RVA: 0x0B4052C8  token: 0x6000171
        private System.Void AssignStyleState(System.Int32 idx, System.IntPtr srcStyleState) { }
        // RVA: 0x0B405C54  token: 0x6000172
        private System.IntPtr GetRectOffsetPtr(System.Int32 idx) { }
        // RVA: 0x0B405278  token: 0x6000173
        private System.Void AssignRectOffset(System.Int32 idx, System.IntPtr srcRectOffset) { }
        // RVA: 0x0B40631C  token: 0x6000174
        private static System.Single Internal_GetLineHeight(System.IntPtr target) { }
        // RVA: 0x0B4061A0  token: 0x6000175
        private System.Void Internal_Draw(UnityEngine.Rect screenRect, UnityEngine.GUIContent content, System.Boolean isHover, System.Boolean isActive, System.Boolean on, System.Boolean hasKeyboardFocus) { }
        // RVA: 0x0B405FBC  token: 0x6000176
        private System.Void Internal_Draw2(UnityEngine.Rect position, UnityEngine.GUIContent content, System.Int32 controlID, System.Boolean on) { }
        // RVA: 0x0B406040  token: 0x6000177
        private System.Void Internal_DrawCursor(UnityEngine.Rect position, UnityEngine.GUIContent content, System.Int32 pos, UnityEngine.Color cursorColor) { }
        // RVA: 0x0B4060CC  token: 0x6000178
        private System.Void Internal_DrawWithTextSelection(UnityEngine.Rect screenRect, UnityEngine.GUIContent content, System.Boolean isHover, System.Boolean isActive, System.Boolean on, System.Boolean hasKeyboardFocus, System.Boolean drawSelectionAsComposition, System.Int32 cursorFirst, System.Int32 cursorLast, UnityEngine.Color cursorColor, UnityEngine.Color selectionColor) { }
        // RVA: 0x0B406268  token: 0x6000179
        private UnityEngine.Vector2 Internal_GetCursorPixelPosition(UnityEngine.Rect position, UnityEngine.GUIContent content, System.Int32 cursorStringIndex) { }
        // RVA: 0x0B4062F8  token: 0x600017A
        private System.Int32 Internal_GetCursorStringIndex(UnityEngine.Rect position, UnityEngine.GUIContent content, UnityEngine.Vector2 cursorPixelPosition) { }
        // RVA: 0x0B4063B4  token: 0x600017B
        private System.String Internal_GetSelectedRenderedText(UnityEngine.Rect localPosition, UnityEngine.GUIContent mContent, System.Int32 selectIndex, System.Int32 cursorIndex) { }
        // RVA: 0x0B405E88  token: 0x600017C
        private UnityEngine.Vector2 Internal_CalcSize(UnityEngine.GUIContent content) { }
        // RVA: 0x0B405E04  token: 0x600017D
        private UnityEngine.Vector2 Internal_CalcSizeWithConstraints(UnityEngine.GUIContent content, UnityEngine.Vector2 maxSize) { }
        // RVA: 0x0B405CD4  token: 0x600017E
        private System.Single Internal_CalcHeight(UnityEngine.GUIContent content, System.Single width) { }
        // RVA: 0x0B405D7C  token: 0x600017F
        private UnityEngine.Vector2 Internal_CalcMinMaxWidth(UnityEngine.GUIContent content) { }
        // RVA: 0x0B406480  token: 0x6000180
        private static System.Void SetMouseTooltip(System.String tooltip, UnityEngine.Rect screenRect) { }
        // RVA: 0x0B4063D4  token: 0x6000181
        private static System.Boolean IsTooltipActive(System.String tooltip) { }
        // RVA: 0x0B4061D8  token: 0x6000182
        private static System.Single Internal_GetCursorFlashOffset() { }
        // RVA: 0x0B406408  token: 0x6000183
        private static System.Void SetDefaultFont(UnityEngine.Font font) { }
        // RVA: 0x0B406644  token: 0x6000184
        public System.Void .ctor() { }
        // RVA: 0x0B4065AC  token: 0x6000185
        public System.Void .ctor(UnityEngine.GUIStyle other) { }
        // RVA: 0x0B405B54  token: 0x6000186
        protected virtual System.Void Finalize() { }
        // RVA: 0x0B4059E0  token: 0x60001A2
        public System.Void Draw(UnityEngine.Rect position, System.Boolean isHover, System.Boolean isActive, System.Boolean on, System.Boolean hasKeyboardFocus) { }
        // RVA: 0x0B405998  token: 0x60001A3
        public System.Void Draw(UnityEngine.Rect position, UnityEngine.GUIContent content, System.Boolean isHover, System.Boolean isActive, System.Boolean on, System.Boolean hasKeyboardFocus) { }
        // RVA: 0x0B405A88  token: 0x60001A4
        public System.Void Draw(UnityEngine.Rect position, UnityEngine.GUIContent content, System.Int32 controlID) { }
        // RVA: 0x0B4058EC  token: 0x60001A5
        public System.Void Draw(UnityEngine.Rect position, UnityEngine.GUIContent content, System.Int32 controlID, System.Boolean on) { }
        // RVA: 0x0B405ABC  token: 0x60001A6
        public System.Void Draw(UnityEngine.Rect position, UnityEngine.GUIContent content, System.Int32 controlID, System.Boolean on, System.Boolean hover) { }
        // RVA: 0x0B40592C  token: 0x60001A7
        private System.Void Draw(UnityEngine.Rect position, UnityEngine.GUIContent content, System.Int32 controlId, System.Boolean isHover, System.Boolean isActive, System.Boolean on, System.Boolean hasKeyboardFocus) { }
        // RVA: 0x0B405388  token: 0x60001A8
        public System.Void DrawCursor(UnityEngine.Rect position, UnityEngine.GUIContent content, System.Int32 controlID, System.Int32 character) { }
        // RVA: 0x0B40554C  token: 0x60001A9
        private System.Void DrawWithTextSelection(UnityEngine.Rect position, UnityEngine.GUIContent content, System.Boolean isActive, System.Boolean hasKeyboardFocus, System.Int32 firstSelectedCharacter, System.Int32 lastSelectedCharacter, System.Boolean drawSelectionAsComposition, UnityEngine.Color selectionColor) { }
        // RVA: 0x0B4057A0  token: 0x60001AA
        private System.Void DrawWithTextSelection(UnityEngine.Rect position, UnityEngine.GUIContent content, System.Int32 controlID, System.Int32 firstSelectedCharacter, System.Int32 lastSelectedCharacter, System.Boolean drawSelectionAsComposition) { }
        // RVA: 0x0B405760  token: 0x60001AB
        public System.Void DrawWithTextSelection(UnityEngine.Rect position, UnityEngine.GUIContent content, System.Int32 controlID, System.Int32 firstSelectedCharacter, System.Int32 lastSelectedCharacter) { }
        // RVA: 0x0B405C0C  token: 0x60001AD
        public UnityEngine.Vector2 GetCursorPixelPosition(UnityEngine.Rect position, UnityEngine.GUIContent content, System.Int32 cursorStringIndex) { }
        // RVA: 0x0B405C30  token: 0x60001AE
        public System.Int32 GetCursorStringIndex(UnityEngine.Rect position, UnityEngine.GUIContent content, UnityEngine.Vector2 cursorPixelPosition) { }
        // RVA: 0x0B405374  token: 0x60001AF
        public UnityEngine.Vector2 CalcSize(UnityEngine.GUIContent content) { }
        // RVA: 0x0B405360  token: 0x60001B0
        private UnityEngine.Vector2 CalcSizeWithConstraints(UnityEngine.GUIContent content, UnityEngine.Vector2 constraints) { }
        // RVA: 0x0B405318  token: 0x60001B1
        public System.Single CalcHeight(UnityEngine.GUIContent content, System.Single width) { }
        // RVA: 0x0B405324  token: 0x60001B3
        public System.Void CalcMinMaxWidth(UnityEngine.GUIContent content, System.Single& minWidth, System.Single& maxWidth) { }
        // RVA: 0x0B4064DC  token: 0x60001B4
        public virtual System.String ToString() { }
        // RVA: 0x0B406574  token: 0x60001B5
        private static System.Void .cctor() { }
        // RVA: 0x0B4067D8  token: 0x60001B6
        private System.Void get_contentOffset_Injected(UnityEngine.Vector2& ret) { }
        // RVA: 0x0B4070AC  token: 0x60001B7
        private System.Void set_contentOffset_Injected(UnityEngine.Vector2& value) { }
        // RVA: 0x0B406F84  token: 0x60001B8
        private System.Void set_Internal_clipOffset_Injected(UnityEngine.Vector2& value) { }
        // RVA: 0x0B40613C  token: 0x60001B9
        private System.Void Internal_Draw_Injected(UnityEngine.Rect& screenRect, UnityEngine.GUIContent content, System.Boolean isHover, System.Boolean isActive, System.Boolean on, System.Boolean hasKeyboardFocus) { }
        // RVA: 0x0B405F58  token: 0x60001BA
        private System.Void Internal_Draw2_Injected(UnityEngine.Rect& position, UnityEngine.GUIContent content, System.Int32 controlID, System.Boolean on) { }
        // RVA: 0x0B405FDC  token: 0x60001BB
        private System.Void Internal_DrawCursor_Injected(UnityEngine.Rect& position, UnityEngine.GUIContent content, System.Int32 pos, UnityEngine.Color& cursorColor) { }
        // RVA: 0x0B406064  token: 0x60001BC
        private System.Void Internal_DrawWithTextSelection_Injected(UnityEngine.Rect& screenRect, UnityEngine.GUIContent content, System.Boolean isHover, System.Boolean isActive, System.Boolean on, System.Boolean hasKeyboardFocus, System.Boolean drawSelectionAsComposition, System.Int32 cursorFirst, System.Int32 cursorLast, UnityEngine.Color& cursorColor, UnityEngine.Color& selectionColor) { }
        // RVA: 0x0B406204  token: 0x60001BD
        private System.Void Internal_GetCursorPixelPosition_Injected(UnityEngine.Rect& position, UnityEngine.GUIContent content, System.Int32 cursorStringIndex, UnityEngine.Vector2& ret) { }
        // RVA: 0x0B406294  token: 0x60001BE
        private System.Int32 Internal_GetCursorStringIndex_Injected(UnityEngine.Rect& position, UnityEngine.GUIContent content, UnityEngine.Vector2& cursorPixelPosition) { }
        // RVA: 0x0B406350  token: 0x60001BF
        private System.String Internal_GetSelectedRenderedText_Injected(UnityEngine.Rect& localPosition, UnityEngine.GUIContent mContent, System.Int32 selectIndex, System.Int32 cursorIndex) { }
        // RVA: 0x0B405E34  token: 0x60001C0
        private System.Void Internal_CalcSize_Injected(UnityEngine.GUIContent content, UnityEngine.Vector2& ret) { }
        // RVA: 0x0B405DA0  token: 0x60001C1
        private System.Void Internal_CalcSizeWithConstraints_Injected(UnityEngine.GUIContent content, UnityEngine.Vector2& maxSize, UnityEngine.Vector2& ret) { }
        // RVA: 0x0B405D28  token: 0x60001C2
        private System.Void Internal_CalcMinMaxWidth_Injected(UnityEngine.GUIContent content, UnityEngine.Vector2& ret) { }
        // RVA: 0x0B40643C  token: 0x60001C3
        private static System.Void SetMouseTooltip_Injected(System.String tooltip, UnityEngine.Rect& screenRect) { }

    }

    // TypeToken: 0x200001B  // size: 0x14
    public sealed struct ImagePosition
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.ImagePosition ImageLeft;  // const
        public static UnityEngine.ImagePosition ImageAbove;  // const
        public static UnityEngine.ImagePosition ImageOnly;  // const
        public static UnityEngine.ImagePosition TextOnly;  // const

    }

    // TypeToken: 0x200001C  // size: 0x14
    public sealed struct TextClipping
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.TextClipping Overflow;  // const
        public static UnityEngine.TextClipping Clip;  // const

    }

    // TypeToken: 0x200001D  // size: 0x18
    public class GUITargetAttribute : System.Attribute
    {
        // Fields
        private System.Int32 displayMask;  // 0x10

        // Methods
        // RVA: 0x0B407580  token: 0x60001C4
        private static System.Int32 GetGUITargetAttrValue(System.Type klass, System.String methodName) { }

    }

    // TypeToken: 0x200001E  // size: 0x10
    public class GUIUtility
    {
        // Fields
        private static System.Int32 s_ControlCount;  // static @ 0x0
        private static System.Int32 s_SkinMode;  // static @ 0x4
        private static System.Int32 s_OriginalID;  // static @ 0x8
        private static System.Action takeCapture;  // static @ 0x10
        private static System.Action releaseCapture;  // static @ 0x18
        private static System.Func<System.Int32,System.IntPtr,System.Boolean> processEvent;  // static @ 0x20
        private static System.Action cleanupRoots;  // static @ 0x28
        private static System.Func<System.Exception,System.Boolean> endContainerGUIFromException;  // static @ 0x30
        private static System.Action guiChanged;  // static @ 0x38
        private static System.Boolean <guiIsExiting>k__BackingField;  // static @ 0x40
        private static System.Func<System.Boolean> s_HasCurrentWindowKeyFocusFunc;  // static @ 0x48

        // Properties
        System.Boolean hasModalWindow { get; /* RVA: 0x0B408D5C */ }
        System.Single pixelsPerPoint { get; /* RVA: 0x0B408E24 */ }
        System.Int32 guiDepth { get; /* RVA: 0x0B408CF8 */ }
        UnityEngine.Vector2 s_EditorScreenPointOffset { get; /* RVA: 0x0B408E84 */ set; /* RVA: 0x0B40906C */ }
        System.Boolean mouseUsed { get; /* RVA: 0x0B408DF8 */ set; /* RVA: 0x0B409004 */ }
        System.Boolean textFieldInput { get; /* RVA: 0x0B408ED4 */ set; /* RVA: 0x0B4090BC */ }
        System.Boolean manualTex2SRGBEnabled { get; /* RVA: 0x0B408DCC */ set; /* RVA: 0x0B408FD0 */ }
        System.String systemCopyBuffer { get; /* RVA: 0x0B408EA8 */ set; /* RVA: 0x0B409088 */ }
        System.String compositionString { get; /* RVA: 0x0B408CCC */ }
        UnityEngine.IMECompositionMode imeCompositionMode { get; /* RVA: 0x0B408D94 */ set; /* RVA: 0x0B408F94 */ }
        UnityEngine.Vector2 compositionCursorPos { get; /* RVA: 0x0B408CA8 */ set; /* RVA: 0x0B408F34 */ }
        System.Boolean guiIsExiting { get; /* RVA: 0x0B408D24 */ set; /* RVA: 0x0B408F50 */ }
        System.Int32 hotControl { get; /* RVA: 0x0B408D88 */ set; /* RVA: 0x0B408F8C */ }
        System.Int32 keyboardControl { get; /* RVA: 0x0B408DC0 */ set; /* RVA: 0x0B408FC8 */ }

        // Methods
        // RVA: 0x0B40814C  token: 0x60001D2
        private static System.Int32 Internal_GetControlID(System.Int32 hint, UnityEngine.FocusType focusType, UnityEngine.Rect rect) { }
        // RVA: 0x0B407C50  token: 0x60001D3
        public static System.Int32 GetControlID(System.Int32 hint, UnityEngine.FocusType focusType, UnityEngine.Rect rect) { }
        // RVA: 0x0B407788  token: 0x60001D4
        private static System.Void BeginContainerFromOwner(UnityEngine.ScriptableObject owner) { }
        // RVA: 0x0B4077BC  token: 0x60001D5
        private static System.Void BeginContainer(UnityEngine.ObjectGUIState objectGUIState) { }
        // RVA: 0x0B408070  token: 0x60001D6
        private static System.Void Internal_EndContainer() { }
        // RVA: 0x0B407D98  token: 0x60001D7
        private static System.Int32 GetPermanentControlID() { }
        // RVA: 0x0B407850  token: 0x60001D8
        private static System.Int32 CheckForTabEvent(UnityEngine.Event evt) { }
        // RVA: 0x0B408BCC  token: 0x60001D9
        private static System.Void SetKeyboardControlToFirstControlId() { }
        // RVA: 0x0B408BF8  token: 0x60001DA
        private static System.Void SetKeyboardControlToLastControlId() { }
        // RVA: 0x0B407E1C  token: 0x60001DB
        private static System.Boolean HasFocusableControls() { }
        // RVA: 0x0B408394  token: 0x60001DC
        private static System.Boolean OwnsId(System.Int32 id) { }
        // RVA: 0x0B4076FC  token: 0x60001DD
        public static UnityEngine.Rect AlignRectToDevice(UnityEngine.Rect rect, System.Int32& widthInPixels, System.Int32& heightInPixels) { }
        // RVA: 0x0B408240  token: 0x60001E3
        private static UnityEngine.Vector3 Internal_MultiplyPoint(UnityEngine.Vector3 point, UnityEngine.Matrix4x4 transform) { }
        // RVA: 0x0B407BE0  token: 0x60001E4
        private static System.Boolean GetChanged() { }
        // RVA: 0x0B408B64  token: 0x60001E5
        private static System.Void SetChanged(System.Boolean changed) { }
        // RVA: 0x0B408B98  token: 0x60001E6
        private static System.Void SetDidGUIWindowsEatLastEvent(System.Boolean value) { }
        // RVA: 0x0B408194  token: 0x60001E7
        private static System.Int32 Internal_GetHotControl() { }
        // RVA: 0x0B4081C0  token: 0x60001E8
        private static System.Int32 Internal_GetKeyboardControl() { }
        // RVA: 0x0B408274  token: 0x60001E9
        private static System.Void Internal_SetHotControl(System.Int32 value) { }
        // RVA: 0x0B4082A8  token: 0x60001EA
        private static System.Void Internal_SetKeyboardControl(System.Int32 value) { }
        // RVA: 0x0B408160  token: 0x60001EB
        private static System.Object Internal_GetDefaultSkin(System.Int32 skinMode) { }
        // RVA: 0x0B4080C8  token: 0x60001EC
        private static UnityEngine.Object Internal_GetBuiltinSkin(System.Int32 skin) { }
        // RVA: 0x0B40809C  token: 0x60001ED
        private static System.Void Internal_ExitGUI() { }
        // RVA: 0x0B408044  token: 0x60001EE
        private static UnityEngine.Vector2 InternalWindowToScreenPoint(UnityEngine.Vector2 windowPoint) { }
        // RVA: 0x0B407FD4  token: 0x60001EF
        private static UnityEngine.Vector2 InternalScreenToWindowPoint(UnityEngine.Vector2 screenPoint) { }
        // RVA: 0x0B408350  token: 0x60001F0
        private static System.Void MarkGUIChanged() { }
        // RVA: 0x0B407C88  token: 0x60001F1
        public static System.Int32 GetControlID(UnityEngine.FocusType focus) { }
        // RVA: 0x0B407C98  token: 0x60001F2
        public static System.Int32 GetControlID(UnityEngine.GUIContent contents, UnityEngine.FocusType focus) { }
        // RVA: 0x0B407C0C  token: 0x60001F3
        public static System.Int32 GetControlID(UnityEngine.FocusType focus, UnityEngine.Rect position) { }
        // RVA: 0x0B407CC4  token: 0x60001F4
        public static System.Int32 GetControlID(UnityEngine.GUIContent contents, UnityEngine.FocusType focus, UnityEngine.Rect position) { }
        // RVA: 0x0B407C30  token: 0x60001F5
        public static System.Int32 GetControlID(System.Int32 hint, UnityEngine.FocusType focus) { }
        // RVA: 0x0B407DC4  token: 0x60001F6
        public static System.Object GetStateObject(System.Type t, System.Int32 controlID) { }
        // RVA: 0x0B40843C  token: 0x60001F7
        public static System.Object QueryStateObject(System.Type t, System.Int32 controlID) { }
        // RVA: 0x0B408C30  token: 0x60001FC
        private static System.Void TakeCapture() { }
        // RVA: 0x0B408494  token: 0x60001FD
        private static System.Void RemoveCapture() { }
        // RVA: 0x0B407E48  token: 0x6000200
        private static System.Boolean HasKeyFocus(System.Int32 controlID) { }
        // RVA: 0x0B407AC8  token: 0x6000201
        public static System.Void ExitGUI() { }
        // RVA: 0x0B407D0C  token: 0x6000202
        private static UnityEngine.GUISkin GetDefaultSkin(System.Int32 skinMode) { }
        // RVA: 0x0B407D5C  token: 0x6000203
        private static UnityEngine.GUISkin GetDefaultSkin() { }
        // RVA: 0x0B407B90  token: 0x6000204
        private static UnityEngine.GUISkin GetBuiltinSkin(System.Int32 skin) { }
        // RVA: 0x0B4083C8  token: 0x6000205
        private static System.Void ProcessEvent(System.Int32 instanceID, System.IntPtr nativeEventPtr, System.Boolean& result) { }
        // RVA: 0x0B407990  token: 0x6000206
        private static System.Void EndContainer() { }
        // RVA: 0x0B4078F0  token: 0x6000207
        private static System.Void CleanupRoots() { }
        // RVA: 0x0B4077F0  token: 0x6000208
        private static System.Void BeginGUI(System.Int32 skinMode, System.Int32 instanceID, System.Int32 useGUILayout) { }
        // RVA: 0x0B4079C8  token: 0x6000209
        private static System.Void EndGUI(System.Int32 layoutType) { }
        // RVA: 0x0B4079A8  token: 0x600020A
        private static System.Boolean EndGUIFromException(System.Exception exception) { }
        // RVA: 0x0B407934  token: 0x600020B
        private static System.Boolean EndContainerGUIFromException(System.Exception exception) { }
        // RVA: 0x0B4084D8  token: 0x600020C
        private static System.Void ResetGlobalState() { }
        // RVA: 0x0B4082DC  token: 0x600020D
        private static System.Boolean IsExitGUIException(System.Exception exception) { }
        // RVA: 0x0B408C24  token: 0x600020E
        private static System.Boolean ShouldRethrowException(System.Exception exception) { }
        // RVA: 0x0B407884  token: 0x600020F
        private static System.Void CheckOnGUI() { }
        // RVA: 0x0B408804  token: 0x6000210
        private static System.Single RoundToPixelGrid(System.Single v) { }
        // RVA: 0x0B407B14  token: 0x6000211
        public static UnityEngine.Vector2 GUIToScreenPoint(UnityEngine.Vector2 guiPoint) { }
        // RVA: 0x0B407B30  token: 0x6000212
        public static UnityEngine.Rect GUIToScreenRect(UnityEngine.Rect guiRect) { }
        // RVA: 0x0B408AE8  token: 0x6000213
        public static UnityEngine.Vector2 ScreenToGUIPoint(UnityEngine.Vector2 screenPoint) { }
        // RVA: 0x0B408B04  token: 0x6000214
        public static UnityEngine.Rect ScreenToGUIRect(UnityEngine.Rect screenRect) { }
        // RVA: 0x0B408520  token: 0x6000215
        public static System.Void RotateAroundPivot(System.Single angle, UnityEngine.Vector2 pivotPoint) { }
        // RVA: 0x0B408844  token: 0x6000216
        public static System.Void ScaleAroundPivot(UnityEngine.Vector2 scale, UnityEngine.Vector2 pivotPoint) { }
        // RVA: 0x0B407738  token: 0x6000217
        public static UnityEngine.Rect AlignRectToDevice(UnityEngine.Rect rect) { }
        // RVA: 0x0B407EF4  token: 0x6000218
        public static System.Boolean HitTest(UnityEngine.Rect rect, UnityEngine.Vector2 point, System.Int32 offset) { }
        // RVA: 0x0B407F6C  token: 0x6000219
        public static System.Boolean HitTest(UnityEngine.Rect rect, UnityEngine.Vector2 point, System.Boolean isDirectManipulationDevice) { }
        // RVA: 0x0B407E8C  token: 0x600021A
        public static System.Boolean HitTest(UnityEngine.Rect rect, UnityEngine.Event evt) { }
        // RVA: 0x041E1670  token: 0x600021B
        public System.Void .ctor() { }
        // RVA: 0x0B408E50  token: 0x600021C
        private static System.Void get_s_EditorScreenPointOffset_Injected(UnityEngine.Vector2& ret) { }
        // RVA: 0x0B409038  token: 0x600021D
        private static System.Void set_s_EditorScreenPointOffset_Injected(UnityEngine.Vector2& value) { }
        // RVA: 0x0B4080FC  token: 0x600021E
        private static System.Int32 Internal_GetControlID_Injected(System.Int32 hint, UnityEngine.FocusType focusType, UnityEngine.Rect& rect) { }
        // RVA: 0x0B407698  token: 0x600021F
        private static System.Void AlignRectToDevice_Injected(UnityEngine.Rect& rect, System.Int32& widthInPixels, System.Int32& heightInPixels, UnityEngine.Rect& ret) { }
        // RVA: 0x0B408C74  token: 0x6000220
        private static System.Void get_compositionCursorPos_Injected(UnityEngine.Vector2& ret) { }
        // RVA: 0x0B408F00  token: 0x6000221
        private static System.Void set_compositionCursorPos_Injected(UnityEngine.Vector2& value) { }
        // RVA: 0x0B4081EC  token: 0x6000222
        private static System.Void Internal_MultiplyPoint_Injected(UnityEngine.Vector3& point, UnityEngine.Matrix4x4& transform, UnityEngine.Vector3& ret) { }
        // RVA: 0x0B408000  token: 0x6000223
        private static System.Void InternalWindowToScreenPoint_Injected(UnityEngine.Vector2& windowPoint, UnityEngine.Vector2& ret) { }
        // RVA: 0x0B407F90  token: 0x6000224
        private static System.Void InternalScreenToWindowPoint_Injected(UnityEngine.Vector2& screenPoint, UnityEngine.Vector2& ret) { }

    }

    // TypeToken: 0x200001F  // size: 0x90
    public sealed class ExitGUIException : System.Exception
    {
        // Methods
        // RVA: 0x0B3FE8D0  token: 0x6000225
        public System.Void .ctor() { }
        // RVA: 0x0B3FE920  token: 0x6000226
        private System.Void .ctor(System.String message) { }

    }

    // TypeToken: 0x2000020  // size: 0x48
    public class GUILayoutEntry
    {
        // Fields
        public System.Single minWidth;  // 0x10
        public System.Single maxWidth;  // 0x14
        public System.Single minHeight;  // 0x18
        public System.Single maxHeight;  // 0x1c
        public UnityEngine.Rect rect;  // 0x20
        public System.Int32 stretchWidth;  // 0x30
        public System.Int32 stretchHeight;  // 0x34
        public System.Boolean consideredForMargin;  // 0x38
        private UnityEngine.GUIStyle m_Style;  // 0x40
        private static UnityEngine.Rect kDummyRect;  // static @ 0x0
        protected static System.Int32 indent;  // static @ 0x10

        // Properties
        UnityEngine.GUIStyle style { get; /* RVA: 0x04DBCF90 */ set; /* RVA: 0x0B3FFD50 */ }
        System.Int32 marginLeft { get; /* RVA: 0x0B3FFC88 */ }
        System.Int32 marginRight { get; /* RVA: 0x0B3FFCB8 */ }
        System.Int32 marginTop { get; /* RVA: 0x0B3FFCE8 */ }
        System.Int32 marginBottom { get; /* RVA: 0x0B3FFC20 */ }
        System.Int32 marginHorizontal { get; /* RVA: 0x0B3FFC50 */ }
        System.Int32 marginVertical { get; /* RVA: 0x0B3FFD18 */ }

        // Methods
        // RVA: 0x0B3FFA90  token: 0x600022F
        public System.Void .ctor(System.Single _minWidth, System.Single _maxWidth, System.Single _minHeight, System.Single _maxHeight, UnityEngine.GUIStyle _style) { }
        // RVA: 0x0B3FFB5C  token: 0x6000230
        public System.Void .ctor(System.Single _minWidth, System.Single _maxWidth, System.Single _minHeight, System.Single _maxHeight, UnityEngine.GUIStyle _style, UnityEngine.GUILayoutOption[] options) { }
        // RVA: 0x041E1670  token: 0x6000231
        public virtual System.Void CalcWidth() { }
        // RVA: 0x041E1670  token: 0x6000232
        public virtual System.Void CalcHeight() { }
        // RVA: 0x04DBCF70  token: 0x6000233
        public virtual System.Void SetHorizontal(System.Single x, System.Single width) { }
        // RVA: 0x04DBCF80  token: 0x6000234
        public virtual System.Void SetVertical(System.Single y, System.Single height) { }
        // RVA: 0x0B3FF5E0  token: 0x6000235
        protected virtual System.Void ApplyStyleSettings(UnityEngine.GUIStyle style) { }
        // RVA: 0x0B3FF3B0  token: 0x6000236
        public virtual System.Void ApplyOptions(UnityEngine.GUILayoutOption[] options) { }
        // RVA: 0x0B3FF67C  token: 0x6000237
        public virtual System.String ToString() { }
        // RVA: 0x0B3FFA3C  token: 0x6000238
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000021  // size: 0x58
    public sealed class GUIWordWrapSizer : UnityEngine.GUILayoutEntry
    {
        // Fields
        private readonly UnityEngine.GUIContent m_Content;  // 0x48
        private readonly System.Single m_ForcedMinHeight;  // 0x50
        private readonly System.Single m_ForcedMaxHeight;  // 0x54

        // Methods
        // RVA: 0x0B411F60  token: 0x6000239
        public System.Void .ctor(UnityEngine.GUIStyle style, UnityEngine.GUIContent content, UnityEngine.GUILayoutOption[] options) { }
        // RVA: 0x0B411EB4  token: 0x600023A
        public virtual System.Void CalcWidth() { }
        // RVA: 0x0B411E34  token: 0x600023B
        public virtual System.Void CalcHeight() { }

    }

    // TypeToken: 0x2000022  // size: 0x90
    public class GUILayoutGroup : UnityEngine.GUILayoutEntry
    {
        // Fields
        public System.Collections.Generic.List<UnityEngine.GUILayoutEntry> entries;  // 0x48
        public System.Boolean isVertical;  // 0x50
        public System.Boolean resetCoords;  // 0x51
        public System.Single spacing;  // 0x54
        public System.Boolean sameSize;  // 0x58
        public System.Boolean isWindow;  // 0x59
        public System.Int32 windowID;  // 0x5c
        private System.Int32 m_Cursor;  // 0x60
        protected System.Int32 m_StretchableCountX;  // 0x64
        protected System.Int32 m_StretchableCountY;  // 0x68
        protected System.Boolean m_UserSpecifiedWidth;  // 0x6c
        protected System.Boolean m_UserSpecifiedHeight;  // 0x6d
        protected System.Single m_ChildMinWidth;  // 0x70
        protected System.Single m_ChildMaxWidth;  // 0x74
        protected System.Single m_ChildMinHeight;  // 0x78
        protected System.Single m_ChildMaxHeight;  // 0x7c
        protected System.Int32 m_MarginLeft;  // 0x80
        protected System.Int32 m_MarginRight;  // 0x84
        protected System.Int32 m_MarginTop;  // 0x88
        protected System.Int32 m_MarginBottom;  // 0x8c
        private static readonly UnityEngine.GUILayoutEntry none;  // static @ 0x0

        // Properties
        System.Int32 marginLeft { get; /* RVA: 0x04D863A0 */ }
        System.Int32 marginRight { get; /* RVA: 0x04D8FCE0 */ }
        System.Int32 marginTop { get; /* RVA: 0x04D86890 */ }
        System.Int32 marginBottom { get; /* RVA: 0x04D90C40 */ }

        // Methods
        // RVA: 0x0B4117E4  token: 0x6000240
        public System.Void .ctor() { }
        // RVA: 0x0B40F0CC  token: 0x6000241
        public virtual System.Void ApplyOptions(UnityEngine.GUILayoutOption[] options) { }
        // RVA: 0x0B40F19C  token: 0x6000242
        protected virtual System.Void ApplyStyleSettings(UnityEngine.GUIStyle style) { }
        // RVA: 0x04DBD110  token: 0x6000243
        public System.Void ResetCursor() { }
        // RVA: 0x0B410220  token: 0x6000244
        public UnityEngine.GUILayoutEntry GetNext() { }
        // RVA: 0x0B40FF84  token: 0x6000245
        public UnityEngine.Rect GetLast() { }
        // RVA: 0x0B40F078  token: 0x6000246
        public System.Void Add(UnityEngine.GUILayoutEntry e) { }
        // RVA: 0x0B40F898  token: 0x6000247
        public virtual System.Void CalcWidth() { }
        // RVA: 0x0B410480  token: 0x6000248
        public virtual System.Void SetHorizontal(System.Single x, System.Single width) { }
        // RVA: 0x0B40F21C  token: 0x6000249
        public virtual System.Void CalcHeight() { }
        // RVA: 0x0B410BE8  token: 0x600024A
        public virtual System.Void SetVertical(System.Single y, System.Single height) { }
        // RVA: 0x0B4113C8  token: 0x600024B
        public virtual System.String ToString() { }
        // RVA: 0x0B411714  token: 0x600024C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000023  // size: 0xC0
    public sealed class GUIScrollGroup : UnityEngine.GUILayoutGroup
    {
        // Fields
        public System.Single calcMinWidth;  // 0x90
        public System.Single calcMaxWidth;  // 0x94
        public System.Single calcMinHeight;  // 0x98
        public System.Single calcMaxHeight;  // 0x9c
        public System.Single clientWidth;  // 0xa0
        public System.Single clientHeight;  // 0xa4
        public System.Boolean allowHorizontalScroll;  // 0xa8
        public System.Boolean allowVerticalScroll;  // 0xa9
        public System.Boolean needsHorizontalScrollbar;  // 0xaa
        public System.Boolean needsVerticalScrollbar;  // 0xab
        public UnityEngine.GUIStyle horizontalScrollbar;  // 0xb0
        public UnityEngine.GUIStyle verticalScrollbar;  // 0xb8

        // Methods
        // RVA: 0x0B411DE0  token: 0x600024D
        public System.Void .ctor() { }
        // RVA: 0x0B411A08  token: 0x600024E
        public virtual System.Void CalcWidth() { }
        // RVA: 0x0B411A9C  token: 0x600024F
        public virtual System.Void SetHorizontal(System.Single x, System.Single width) { }
        // RVA: 0x0B4118F0  token: 0x6000250
        public virtual System.Void CalcHeight() { }
        // RVA: 0x0B411BD4  token: 0x6000251
        public virtual System.Void SetVertical(System.Single y, System.Single height) { }

    }

    // TypeToken: 0x2000024  // size: 0x18
    public class ObjectGUIState : System.IDisposable
    {
        // Fields
        private System.IntPtr m_Ptr;  // 0x10

        // Methods
        // RVA: 0x0B412180  token: 0x6000252
        public System.Void .ctor() { }
        // RVA: 0x0B412064  token: 0x6000253
        public virtual System.Void Dispose() { }
        // RVA: 0x0B4120B0  token: 0x6000254
        protected virtual System.Void Finalize() { }
        // RVA: 0x0B412038  token: 0x6000255
        private System.Void Destroy() { }
        // RVA: 0x0B412120  token: 0x6000256
        private static System.IntPtr Internal_Create() { }
        // RVA: 0x0B41214C  token: 0x6000257
        private static System.Void Internal_Destroy(System.IntPtr ptr) { }

    }

    // TypeToken: 0x2000025  // size: 0x68
    public class ScrollViewState
    {
        // Fields
        public UnityEngine.Rect position;  // 0x10
        public UnityEngine.Rect visibleRect;  // 0x20
        public UnityEngine.Rect viewRect;  // 0x30
        public UnityEngine.Vector2 scrollPosition;  // 0x40
        public System.Boolean apply;  // 0x48
        public System.Boolean isDuringTouchScroll;  // 0x49
        public UnityEngine.Vector2 touchScrollStartMousePosition;  // 0x4c
        public UnityEngine.Vector2 touchScrollStartPosition;  // 0x54
        public UnityEngine.Vector2 velocity;  // 0x5c
        public System.Single previousTimeSinceStartup;  // 0x64

        // Methods
        // RVA: 0x041E1670  token: 0x6000258
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000026  // size: 0x20
    public class SliderState
    {
        // Fields
        public System.Single dragStartPos;  // 0x10
        public System.Single dragStartValue;  // 0x14
        public System.Boolean isDragging;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000259
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000027  // size: 0x50
    public sealed struct SliderHandler
    {
        // Fields
        private readonly UnityEngine.Rect position;  // 0x10
        private readonly System.Single currentValue;  // 0x20
        private readonly System.Single size;  // 0x24
        private readonly System.Single start;  // 0x28
        private readonly System.Single end;  // 0x2c
        private readonly UnityEngine.GUIStyle slider;  // 0x30
        private readonly UnityEngine.GUIStyle thumb;  // 0x38
        private readonly UnityEngine.GUIStyle thumbExtent;  // 0x40
        private readonly System.Boolean horiz;  // 0x48
        private readonly System.Int32 id;  // 0x4c

        // Methods
        // RVA: 0x0B413534  token: 0x600025A
        public System.Void .ctor(UnityEngine.Rect position, System.Single currentValue, System.Single size, System.Single start, System.Single end, UnityEngine.GUIStyle slider, UnityEngine.GUIStyle thumb, System.Boolean horiz, System.Int32 id, UnityEngine.GUIStyle thumbExtent) { }
        // RVA: 0x0B4122B8  token: 0x600025B
        public System.Single Handle() { }
        // RVA: 0x0B4125A4  token: 0x600025C
        private System.Single OnMouseDown() { }
        // RVA: 0x0B412910  token: 0x600025D
        private System.Single OnMouseDrag() { }
        // RVA: 0x0B4129F8  token: 0x600025E
        private System.Single OnMouseUp() { }
        // RVA: 0x0B412A44  token: 0x600025F
        private System.Single OnRepaint() { }
        // RVA: 0x0B4121DC  token: 0x6000260
        private UnityEngine.EventType CurrentEventType() { }
        // RVA: 0x0B412210  token: 0x6000261
        private System.Int32 CurrentScrollTroughSide() { }
        // RVA: 0x0B4124F4  token: 0x6000262
        private System.Boolean IsEmptySlider() { }
        // RVA: 0x0B412F98  token: 0x6000263
        private System.Boolean SupportsPageMovements() { }
        // RVA: 0x0B412DCC  token: 0x6000264
        private System.Single PageMovementValue() { }
        // RVA: 0x0B412E5C  token: 0x6000265
        private System.Single PageUpMovementBound() { }
        // RVA: 0x04DBCF50  token: 0x6000266
        private UnityEngine.Event CurrentEvent() { }
        // RVA: 0x0B4131F8  token: 0x6000267
        private System.Single ValueForCurrentMousePosition() { }
        // RVA: 0x0B41219C  token: 0x6000268
        private System.Single Clamp(System.Single value) { }
        // RVA: 0x0B413110  token: 0x6000269
        private UnityEngine.Rect ThumbSelectionRect() { }
        // RVA: 0x0B412F44  token: 0x600026A
        private System.Void StartDraggingWithValue(System.Single dragStartValue) { }
        // RVA: 0x0B412EBC  token: 0x600026B
        private UnityEngine.SliderState SliderState() { }
        // RVA: 0x0B412FF4  token: 0x600026C
        private UnityEngine.Rect ThumbExtRect() { }
        // RVA: 0x0B4130D4  token: 0x600026D
        private UnityEngine.Rect ThumbRect() { }
        // RVA: 0x0B413380  token: 0x600026E
        private UnityEngine.Rect VerticalThumbRect() { }
        // RVA: 0x0B412340  token: 0x600026F
        private UnityEngine.Rect HorizontalThumbRect() { }
        // RVA: 0x0B4121CC  token: 0x6000270
        private System.Single ClampedCurrentValue() { }
        // RVA: 0x0B41252C  token: 0x6000271
        private System.Single MousePosition() { }
        // RVA: 0x0B4132C8  token: 0x6000272
        private System.Single ValuesPerPixel() { }
        // RVA: 0x0B41313C  token: 0x6000273
        private System.Single ThumbSize() { }
        // RVA: 0x0B412508  token: 0x6000274
        private System.Single MaxValue() { }
        // RVA: 0x0B41251C  token: 0x6000275
        private System.Single MinValue() { }

    }

    // TypeToken: 0x2000028  // size: 0x90
    public class TextEditor
    {
        // Fields
        public UnityEngine.TouchScreenKeyboard keyboardOnScreen;  // 0x10
        public System.Int32 controlID;  // 0x18
        public UnityEngine.GUIStyle style;  // 0x20
        public System.Boolean multiline;  // 0x28
        public System.Boolean hasHorizontalCursorPos;  // 0x29
        public System.Boolean isPasswordField;  // 0x2a
        private System.Boolean m_HasFocus;  // 0x2b
        public UnityEngine.Vector2 scrollOffset;  // 0x2c
        private UnityEngine.GUIContent m_Content;  // 0x38
        private UnityEngine.Rect m_Position;  // 0x40
        private System.Int32 m_CursorIndex;  // 0x50
        private System.Int32 m_SelectIndex;  // 0x54
        private System.Boolean m_RevealCursor;  // 0x58
        public UnityEngine.Vector2 graphicalCursorPos;  // 0x5c
        public UnityEngine.Vector2 graphicalSelectCursorPos;  // 0x64
        private System.Boolean m_MouseDragSelectsWholeWords;  // 0x6c
        private System.Int32 m_DblClickInitPos;  // 0x70
        private UnityEngine.TextEditor.DblClickSnapping m_DblClickSnap;  // 0x74
        private System.Boolean m_bJustSelected;  // 0x75
        private System.Int32 m_iAltCursorPos;  // 0x78
        private System.String oldText;  // 0x80
        private System.Int32 oldPos;  // 0x88
        private System.Int32 oldSelectPos;  // 0x8c
        private static System.Collections.Generic.Dictionary<UnityEngine.Event,UnityEngine.TextEditor.TextEditOp> s_Keyactions;  // static @ 0x0

        // Properties
        System.String text { get; /* RVA: 0x0A97F198 */ set; /* RVA: 0x0B4176C4 */ }
        UnityEngine.Rect position { get; /* RVA: 0x04DBD160 */ set; /* RVA: 0x0B41762C */ }
        UnityEngine.Rect localPosition { get; /* RVA: 0x04DBD150 */ }
        System.Int32 cursorIndex { get; /* RVA: 0x04DBA760 */ set; /* RVA: 0x0B4175EC */ }
        System.Int32 selectIndex { get; /* RVA: 0x04DBD170 */ set; /* RVA: 0x0B417688 */ }
        System.Int32 altCursorPosition { get; /* RVA: 0x04DBD140 */ }
        System.Boolean hasSelection { get; /* RVA: 0x0B4175E0 */ }
        System.String SelectedText { get; /* RVA: 0x0B417558 */ }

        // Methods
        // RVA: 0x04DBD120  token: 0x600027F
        private System.Void ClearCursorPos() { }
        // RVA: 0x0B417488  token: 0x6000281
        public System.Void .ctor() { }
        // RVA: 0x0B415FCC  token: 0x6000282
        public System.Void OnFocus() { }
        // RVA: 0x0B416008  token: 0x6000283
        public System.Void OnLostFocus() { }
        // RVA: 0x0B414818  token: 0x6000284
        private System.Void GrabGraphicalCursorPos() { }
        // RVA: 0x0B414A1C  token: 0x6000285
        public System.Boolean HandleKeyEvent(UnityEngine.Event e) { }
        // RVA: 0x0B4148FC  token: 0x6000286
        private System.Boolean HandleKeyEvent(UnityEngine.Event e, System.Boolean textIsReadOnly) { }
        // RVA: 0x0B413854  token: 0x6000287
        public System.Boolean DeleteLineBack() { }
        // RVA: 0x0B413ADC  token: 0x6000288
        public System.Boolean DeleteWordBack() { }
        // RVA: 0x0B413B90  token: 0x6000289
        public System.Boolean DeleteWordForward() { }
        // RVA: 0x0B413C3C  token: 0x600028A
        public System.Boolean Delete() { }
        // RVA: 0x0B413670  token: 0x600028B
        public System.Boolean CanPaste() { }
        // RVA: 0x0B4135B4  token: 0x600028C
        public System.Boolean Backspace() { }
        // RVA: 0x0B41673C  token: 0x600028D
        public System.Void SelectAll() { }
        // RVA: 0x0B416A10  token: 0x600028E
        public System.Void SelectNone() { }
        // RVA: 0x0B413948  token: 0x6000291
        public System.Boolean DeleteSelection() { }
        // RVA: 0x0B416668  token: 0x6000292
        public System.Void ReplaceSelection(System.String replace) { }
        // RVA: 0x0B41536C  token: 0x6000293
        public System.Void Insert(System.Char c) { }
        // RVA: 0x0B415B3C  token: 0x6000294
        public System.Void MoveSelectionToAltCursor() { }
        // RVA: 0x0B415ADC  token: 0x6000295
        public System.Void MoveRight() { }
        // RVA: 0x0B41585C  token: 0x6000296
        public System.Void MoveLeft() { }
        // RVA: 0x0B415D3C  token: 0x6000297
        public System.Void MoveUp() { }
        // RVA: 0x0B4156C4  token: 0x6000298
        public System.Void MoveDown() { }
        // RVA: 0x0B415948  token: 0x6000299
        public System.Void MoveLineStart() { }
        // RVA: 0x0B4158B0  token: 0x600029A
        public System.Void MoveLineEnd() { }
        // RVA: 0x0B415810  token: 0x600029B
        public System.Void MoveGraphicalLineStart() { }
        // RVA: 0x0B4157C4  token: 0x600029C
        public System.Void MoveGraphicalLineEnd() { }
        // RVA: 0x0B415C64  token: 0x600029D
        public System.Void MoveTextStart() { }
        // RVA: 0x0B415C1C  token: 0x600029E
        public System.Void MoveTextEnd() { }
        // RVA: 0x0B414A2C  token: 0x600029F
        private System.Int32 IndexOfEndOfLine(System.Int32 startIndex) { }
        // RVA: 0x0B415A64  token: 0x60002A0
        public System.Void MoveParagraphForward() { }
        // RVA: 0x0B4159D4  token: 0x60002A1
        public System.Void MoveParagraphBackward() { }
        // RVA: 0x0B415670  token: 0x60002A2
        public System.Void MoveCursorToPosition(UnityEngine.Vector2 cursorPosition) { }
        // RVA: 0x0B4155B0  token: 0x60002A3
        protected internal System.Void MoveCursorToPosition_Internal(UnityEngine.Vector2 cursorPosition, System.Boolean shift) { }
        // RVA: 0x0B415500  token: 0x60002A4
        public System.Void MoveAltCursorToPosition(UnityEngine.Vector2 cursorPosition) { }
        // RVA: 0x0B416C30  token: 0x60002A5
        public System.Void SelectToPosition(UnityEngine.Vector2 cursorPosition) { }
        // RVA: 0x0B4169B0  token: 0x60002A6
        public System.Void SelectLeft() { }
        // RVA: 0x0B416B60  token: 0x60002A7
        public System.Void SelectRight() { }
        // RVA: 0x0B416F08  token: 0x60002A8
        public System.Void SelectUp() { }
        // RVA: 0x0B416894  token: 0x60002A9
        public System.Void SelectDown() { }
        // RVA: 0x0B416BC0  token: 0x60002AA
        public System.Void SelectTextEnd() { }
        // RVA: 0x0B416BF0  token: 0x60002AB
        public System.Void SelectTextStart() { }
        // RVA: 0x04DBD130  token: 0x60002AC
        public System.Void MouseDragSelectsWholeWords(System.Boolean on) { }
        // RVA: 0x0157A810  token: 0x60002AD
        public System.Void DblClickSnap(UnityEngine.TextEditor.DblClickSnapping snapping) { }
        // RVA: 0x0B41473C  token: 0x60002AE
        private System.Int32 GetGraphicalLineStart(System.Int32 p) { }
        // RVA: 0x0B41464C  token: 0x60002AF
        private System.Int32 GetGraphicalLineEnd(System.Int32 p) { }
        // RVA: 0x0B4143B0  token: 0x60002B0
        private System.Int32 FindNextSeperator(System.Int32 startPos) { }
        // RVA: 0x0B414438  token: 0x60002B1
        private System.Int32 FindPrevSeperator(System.Int32 startPos) { }
        // RVA: 0x0B415E5C  token: 0x60002B2
        public System.Void MoveWordRight() { }
        // RVA: 0x0B415CE4  token: 0x60002B3
        public System.Void MoveToStartOfNextWord() { }
        // RVA: 0x0B415C8C  token: 0x60002B4
        public System.Void MoveToEndOfPreviousWord() { }
        // RVA: 0x0B416ED4  token: 0x60002B5
        public System.Void SelectToStartOfNextWord() { }
        // RVA: 0x0B416BFC  token: 0x60002B6
        public System.Void SelectToEndOfPreviousWord() { }
        // RVA: 0x0B4136D4  token: 0x60002B7
        private UnityEngine.TextEditor.CharacterType ClassifyChar(System.Int32 index) { }
        // RVA: 0x0B4144D8  token: 0x60002B8
        public System.Int32 FindStartOfNextWord(System.Int32 p) { }
        // RVA: 0x0B4142FC  token: 0x60002B9
        private System.Int32 FindEndOfPreviousWord(System.Int32 p) { }
        // RVA: 0x0B415E0C  token: 0x60002BA
        public System.Void MoveWordLeft() { }
        // RVA: 0x0B41701C  token: 0x60002BB
        public System.Void SelectWordRight() { }
        // RVA: 0x0B416F98  token: 0x60002BC
        public System.Void SelectWordLeft() { }
        // RVA: 0x0B414164  token: 0x60002BD
        public System.Void ExpandSelectGraphicalLineStart() { }
        // RVA: 0x0B414100  token: 0x60002BE
        public System.Void ExpandSelectGraphicalLineEnd() { }
        // RVA: 0x0B41697C  token: 0x60002BF
        public System.Void SelectGraphicalLineStart() { }
        // RVA: 0x0B416948  token: 0x60002C0
        public System.Void SelectGraphicalLineEnd() { }
        // RVA: 0x0B416AD8  token: 0x60002C1
        public System.Void SelectParagraphForward() { }
        // RVA: 0x0B416A38  token: 0x60002C2
        public System.Void SelectParagraphBackward() { }
        // RVA: 0x0B416808  token: 0x60002C3
        public System.Void SelectCurrentWord() { }
        // RVA: 0x0B4141C8  token: 0x60002C4
        private System.Int32 FindEndOfClassification(System.Int32 p, UnityEngine.TextEditor.Direction dir) { }
        // RVA: 0x0B416780  token: 0x60002C5
        public System.Void SelectCurrentParagraph() { }
        // RVA: 0x0B4170A0  token: 0x60002C6
        public System.Void UpdateScrollOffsetIfNeeded(UnityEngine.Event evt) { }
        // RVA: 0x0B4170F0  token: 0x60002C7
        private System.Void UpdateScrollOffset() { }
        // RVA: 0x0B413CF4  token: 0x60002C8
        public System.Void DrawCursor(System.String newText) { }
        // RVA: 0x0B416098  token: 0x60002C9
        private System.Boolean PerformOperation(UnityEngine.TextEditor.TextEditOp operation, System.Boolean textIsReadOnly) { }
        // RVA: 0x0B416700  token: 0x60002CA
        public System.Void SaveBackup() { }
        // RVA: 0x0B413828  token: 0x60002CB
        public System.Boolean Cut() { }
        // RVA: 0x0B4137AC  token: 0x60002CC
        public System.Void Copy() { }
        // RVA: 0x0B4165DC  token: 0x60002CD
        private static System.String ReplaceNewlinesWithSpaces(System.String value) { }
        // RVA: 0x0B416014  token: 0x60002CE
        public System.Boolean Paste() { }
        // RVA: 0x0B415478  token: 0x60002CF
        private static System.Void MapKey(System.String key, UnityEngine.TextEditor.TextEditOp action) { }
        // RVA: 0x0B414A84  token: 0x60002D0
        private System.Void InitKeyActions() { }
        // RVA: 0x0ABD8608  token: 0x60002D1
        public System.Void DetectFocusChange() { }
        // RVA: 0x0B415F74  token: 0x60002D2
        private virtual System.Void OnDetectFocusChange() { }
        // RVA: 0x041E1670  token: 0x60002D3
        private virtual System.Void OnCursorIndexChange() { }
        // RVA: 0x041E1670  token: 0x60002D4
        private virtual System.Void OnSelectIndexChange() { }
        // RVA: 0x0B413694  token: 0x60002D5
        private System.Void ClampTextIndex(System.Int32& index) { }
        // RVA: 0x0B4140BC  token: 0x60002D6
        private System.Void EnsureValidCodePointIndex(System.Int32& index) { }
        // RVA: 0x0B4153C0  token: 0x60002D7
        private System.Boolean IsValidCodePointIndex(System.Int32 index) { }
        // RVA: 0x0B416548  token: 0x60002D8
        private System.Int32 PreviousCodePointIndex(System.Int32 index) { }
        // RVA: 0x0B415EC0  token: 0x60002D9
        private System.Int32 NextCodePointIndex(System.Int32 index) { }

    }

}

