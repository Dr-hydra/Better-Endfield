// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.UI.dll
// Classes:  233
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000006  // size: 0x30
    public class ButtonClickedEvent : UnityEngine.Events.UnityEvent
    {
        // Methods
        // RVA: 0x05393538  token: 0x6000015
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000007  // size: 0x30
    public sealed class <OnFinishSubmit>d__9 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public UnityEngine.UI.Button <>4__this;  // 0x20
        private System.Single <fadeTime>5__2;  // 0x28
        private System.Single <elapsedTime>5__3;  // 0x2c

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000016
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000017
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0B46BFB4  token: 0x6000018
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0B46C0A8  token: 0x600001A
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000012
    public interface IFactoryControls
    {
        // Methods
        // RVA: -1  // abstract  token: 0x600006D
        public virtual UnityEngine.GameObject CreateGameObject(System.String name, System.Type[] components) { }

    }

    // TypeToken: 0x2000013  // size: 0x10
    public class DefaultRuntimeFactory : IFactoryControls
    {
        // Fields
        public static UnityEngine.UI.DefaultControls.IFactoryControls Default;  // static @ 0x0

        // Methods
        // RVA: 0x0B464498  token: 0x600006E
        public virtual UnityEngine.GameObject CreateGameObject(System.String name, System.Type[] components) { }
        // RVA: 0x041E1670  token: 0x600006F
        public System.Void .ctor() { }
        // RVA: 0x0B464508  token: 0x6000070
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000014  // size: 0x48
    public sealed struct Resources
    {
        // Fields
        public UnityEngine.Sprite standard;  // 0x10
        public UnityEngine.Sprite background;  // 0x18
        public UnityEngine.Sprite inputField;  // 0x20
        public UnityEngine.Sprite knob;  // 0x28
        public UnityEngine.Sprite checkmark;  // 0x30
        public UnityEngine.Sprite dropdown;  // 0x38
        public UnityEngine.Sprite mask;  // 0x40

    }

    // TypeToken: 0x2000016  // size: 0x38
    public class DropdownItem : UnityEngine.MonoBehaviour, UnityEngine.EventSystems.IPointerEnterHandler, UnityEngine.EventSystems.IEventSystemHandler, UnityEngine.EventSystems.ICancelHandler
    {
        // Fields
        private UnityEngine.UI.Text m_Text;  // 0x18
        private UnityEngine.UI.Image m_Image;  // 0x20
        private UnityEngine.RectTransform m_RectTransform;  // 0x28
        private UnityEngine.UI.Toggle m_Toggle;  // 0x30

        // Properties
        UnityEngine.UI.Text text { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        UnityEngine.UI.Image image { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        UnityEngine.RectTransform rectTransform { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }
        UnityEngine.UI.Toggle toggle { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }

        // Methods
        // RVA: 0x0B464620  token: 0x60000AB
        public virtual System.Void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B4645A8  token: 0x60000AC
        public virtual System.Void OnCancel(UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x05393520  token: 0x60000AD
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000017  // size: 0x20
    public class OptionData
    {
        // Fields
        private System.String m_Text;  // 0x10
        private UnityEngine.Sprite m_Image;  // 0x18

        // Properties
        System.String text { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        UnityEngine.Sprite image { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60000B2
        public System.Void .ctor() { }
        // RVA: 0x053908C0  token: 0x60000B3
        public System.Void .ctor(System.String text) { }
        // RVA: 0x05392C40  token: 0x60000B4
        public System.Void .ctor(UnityEngine.Sprite image) { }
        // RVA: 0x032AF420  token: 0x60000B5
        public System.Void .ctor(System.String text, UnityEngine.Sprite image) { }

    }

    // TypeToken: 0x2000018  // size: 0x18
    public class OptionDataList
    {
        // Fields
        private System.Collections.Generic.List<UnityEngine.UI.Dropdown.OptionData> m_Options;  // 0x10

        // Properties
        System.Collections.Generic.List<UnityEngine.UI.Dropdown.OptionData> options { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }

        // Methods
        // RVA: 0x0B46B9CC  token: 0x60000B8
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000019  // size: 0x30
    public class DropdownEvent : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x0B46456C  token: 0x60000B9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001A  // size: 0x20
    public sealed class <>c__DisplayClass63_0
    {
        // Fields
        public UnityEngine.UI.Dropdown.DropdownItem item;  // 0x10
        public UnityEngine.UI.Dropdown <>4__this;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60000BA
        public System.Void .ctor() { }
        // RVA: 0x0B46C0F4  token: 0x60000BB
        private System.Void <Show>b__0(System.Boolean x) { }

    }

    // TypeToken: 0x200001B  // size: 0x30
    public sealed class <DelayedDestroyDropdownList>d__75 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public System.Single delay;  // 0x20
        public UnityEngine.UI.Dropdown <>4__this;  // 0x28

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60000BC
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60000BD
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0B46BECC  token: 0x60000BE
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0B46BF68  token: 0x60000C0
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000020  // size: 0x14
    public sealed struct BlockingObjects
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.GraphicRaycaster.BlockingObjects None;  // const
        public static UnityEngine.UI.GraphicRaycaster.BlockingObjects TwoD;  // const
        public static UnityEngine.UI.GraphicRaycaster.BlockingObjects ThreeD;  // const
        public static UnityEngine.UI.GraphicRaycaster.BlockingObjects All;  // const

    }

    // TypeToken: 0x2000021  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly UnityEngine.UI.GraphicRaycaster.<>c <>9;  // static @ 0x0
        public static System.Comparison<UnityEngine.UI.Graphic> <>9__27_0;  // static @ 0x8

        // Methods
        // RVA: 0x04D470B0  token: 0x6000135
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000136
        public System.Void .ctor() { }
        // RVA: 0x0389F670  token: 0x6000137
        private System.Int32 <Raycast>b__27_0(UnityEngine.UI.Graphic g1, UnityEngine.UI.Graphic g2) { }

    }

    // TypeToken: 0x2000029  // size: 0x14
    public sealed struct Type
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.Image.Type Simple;  // const
        public static UnityEngine.UI.Image.Type Sliced;  // const
        public static UnityEngine.UI.Image.Type Tiled;  // const
        public static UnityEngine.UI.Image.Type Filled;  // const
        public static UnityEngine.UI.Image.Type Mirror;  // const
        public static UnityEngine.UI.Image.Type Quarter;  // const

    }

    // TypeToken: 0x200002A  // size: 0x14
    public sealed struct FillMethod
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.Image.FillMethod Horizontal;  // const
        public static UnityEngine.UI.Image.FillMethod Vertical;  // const
        public static UnityEngine.UI.Image.FillMethod Radial90;  // const
        public static UnityEngine.UI.Image.FillMethod Radial180;  // const
        public static UnityEngine.UI.Image.FillMethod Radial360;  // const

    }

    // TypeToken: 0x200002B  // size: 0x14
    public sealed struct OriginHorizontal
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.Image.OriginHorizontal Left;  // const
        public static UnityEngine.UI.Image.OriginHorizontal Right;  // const

    }

    // TypeToken: 0x200002C  // size: 0x14
    public sealed struct OriginVertical
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.Image.OriginVertical Bottom;  // const
        public static UnityEngine.UI.Image.OriginVertical Top;  // const

    }

    // TypeToken: 0x200002D  // size: 0x14
    public sealed struct Origin90
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.Image.Origin90 BottomLeft;  // const
        public static UnityEngine.UI.Image.Origin90 TopLeft;  // const
        public static UnityEngine.UI.Image.Origin90 TopRight;  // const
        public static UnityEngine.UI.Image.Origin90 BottomRight;  // const

    }

    // TypeToken: 0x200002E  // size: 0x14
    public sealed struct Origin180
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.Image.Origin180 Bottom;  // const
        public static UnityEngine.UI.Image.Origin180 Left;  // const
        public static UnityEngine.UI.Image.Origin180 Top;  // const
        public static UnityEngine.UI.Image.Origin180 Right;  // const

    }

    // TypeToken: 0x200002F  // size: 0x14
    public sealed struct Origin360
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.Image.Origin360 Bottom;  // const
        public static UnityEngine.UI.Image.Origin360 Right;  // const
        public static UnityEngine.UI.Image.Origin360 Top;  // const
        public static UnityEngine.UI.Image.Origin360 Left;  // const

    }

    // TypeToken: 0x2000030  // size: 0x34
    public sealed struct SlicedSpriteParams
    {
        // Fields
        public System.Boolean slicedParamNeedUpdate;  // 0x10
        public UnityEngine.Vector2 inner_PosMin;  // 0x14
        public UnityEngine.Vector2 inner_PosMax;  // 0x1c
        public UnityEngine.Vector2 inner_UVMin;  // 0x24
        public UnityEngine.Vector2 inner_UVMax;  // 0x2c

        // Methods
        // RVA: 0x04DBEFB0  token: 0x60001B8
        public System.Void .ctor(System.Boolean slicedParamNeedUpdate, UnityEngine.Vector2 inner_PosMin, UnityEngine.Vector2 inner_PosMax, UnityEngine.Vector2 inner_UVMin, UnityEngine.Vector2 inner_UVMax) { }

    }

    // TypeToken: 0x2000031  // size: 0x11
    public sealed struct EDynamicAtlasActiveMode
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static UnityEngine.UI.Image.EDynamicAtlasActiveMode ActiveOnAwake;  // const
        public static UnityEngine.UI.Image.EDynamicAtlasActiveMode ActiveOnEnable;  // const

    }

    // TypeToken: 0x2000035  // size: 0x14
    public sealed struct ContentType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.InputField.ContentType Standard;  // const
        public static UnityEngine.UI.InputField.ContentType Autocorrected;  // const
        public static UnityEngine.UI.InputField.ContentType IntegerNumber;  // const
        public static UnityEngine.UI.InputField.ContentType DecimalNumber;  // const
        public static UnityEngine.UI.InputField.ContentType Alphanumeric;  // const
        public static UnityEngine.UI.InputField.ContentType Name;  // const
        public static UnityEngine.UI.InputField.ContentType EmailAddress;  // const
        public static UnityEngine.UI.InputField.ContentType Password;  // const
        public static UnityEngine.UI.InputField.ContentType Pin;  // const
        public static UnityEngine.UI.InputField.ContentType Custom;  // const

    }

    // TypeToken: 0x2000036  // size: 0x14
    public sealed struct InputType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.InputField.InputType Standard;  // const
        public static UnityEngine.UI.InputField.InputType AutoCorrect;  // const
        public static UnityEngine.UI.InputField.InputType Password;  // const

    }

    // TypeToken: 0x2000037  // size: 0x14
    public sealed struct CharacterValidation
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.InputField.CharacterValidation None;  // const
        public static UnityEngine.UI.InputField.CharacterValidation Integer;  // const
        public static UnityEngine.UI.InputField.CharacterValidation Decimal;  // const
        public static UnityEngine.UI.InputField.CharacterValidation Alphanumeric;  // const
        public static UnityEngine.UI.InputField.CharacterValidation Name;  // const
        public static UnityEngine.UI.InputField.CharacterValidation EmailAddress;  // const

    }

    // TypeToken: 0x2000038  // size: 0x14
    public sealed struct LineType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.InputField.LineType SingleLine;  // const
        public static UnityEngine.UI.InputField.LineType MultiLineSubmit;  // const
        public static UnityEngine.UI.InputField.LineType MultiLineNewline;  // const

    }

    // TypeToken: 0x2000039  // size: 0x80
    public sealed class OnValidateInput : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0B31A110  token: 0x600025E
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x014F3120  token: 0x600025F
        public virtual System.Char Invoke(System.String text, System.Int32 charIndex, System.Char addedChar) { }
        // RVA: 0x0B57220C  token: 0x6000260
        public virtual System.IAsyncResult BeginInvoke(System.String text, System.Int32 charIndex, System.Char addedChar, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x09612FF8  token: 0x6000261
        public virtual System.Char EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x200003A  // size: 0x30
    public class SubmitEvent : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x0B5722A8  token: 0x6000262
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003B  // size: 0x30
    public class EndEditEvent : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x0B568D1C  token: 0x6000263
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003C  // size: 0x30
    public class OnChangeEvent : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x0B5721D0  token: 0x6000264
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003D  // size: 0x14
    public sealed struct EditState
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.InputField.EditState Continue;  // const
        public static UnityEngine.UI.InputField.EditState Finish;  // const

    }

    // TypeToken: 0x200003E  // size: 0x28
    public sealed class <CaretBlink>d__169 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public UnityEngine.UI.InputField <>4__this;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000265
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000266
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0B5722E4  token: 0x6000267
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0B572404  token: 0x6000269
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200003F  // size: 0x30
    public sealed class <MouseDragOutsideRect>d__191 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public UnityEngine.EventSystems.PointerEventData eventData;  // 0x20
        public UnityEngine.UI.InputField <>4__this;  // 0x28

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x600026B
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x600026C
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0B57249C  token: 0x600026D
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0B572724  token: 0x600026F
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000041  // size: 0x14
    public sealed struct AspectMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.AspectRatioFitter.AspectMode None;  // const
        public static UnityEngine.UI.AspectRatioFitter.AspectMode WidthControlsHeight;  // const
        public static UnityEngine.UI.AspectRatioFitter.AspectMode HeightControlsWidth;  // const
        public static UnityEngine.UI.AspectRatioFitter.AspectMode FitInParent;  // const
        public static UnityEngine.UI.AspectRatioFitter.AspectMode EnvelopeParent;  // const

    }

    // TypeToken: 0x2000043  // size: 0x14
    public sealed struct ScaleMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.CanvasScaler.ScaleMode ConstantPixelSize;  // const
        public static UnityEngine.UI.CanvasScaler.ScaleMode ScaleWithScreenSize;  // const
        public static UnityEngine.UI.CanvasScaler.ScaleMode ConstantPhysicalSize;  // const

    }

    // TypeToken: 0x2000044  // size: 0x14
    public sealed struct ScreenMatchMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.CanvasScaler.ScreenMatchMode MatchWidthOrHeight;  // const
        public static UnityEngine.UI.CanvasScaler.ScreenMatchMode Expand;  // const
        public static UnityEngine.UI.CanvasScaler.ScreenMatchMode Shrink;  // const

    }

    // TypeToken: 0x2000045  // size: 0x14
    public sealed struct Unit
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.CanvasScaler.Unit Centimeters;  // const
        public static UnityEngine.UI.CanvasScaler.Unit Millimeters;  // const
        public static UnityEngine.UI.CanvasScaler.Unit Inches;  // const
        public static UnityEngine.UI.CanvasScaler.Unit Points;  // const
        public static UnityEngine.UI.CanvasScaler.Unit Picas;  // const

    }

    // TypeToken: 0x2000047  // size: 0x14
    public sealed struct FitMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.ContentSizeFitter.FitMode Unconstrained;  // const
        public static UnityEngine.UI.ContentSizeFitter.FitMode MinSize;  // const
        public static UnityEngine.UI.ContentSizeFitter.FitMode PreferredSize;  // const

    }

    // TypeToken: 0x2000049  // size: 0x14
    public sealed struct Corner
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.GridLayoutGroup.Corner UpperLeft;  // const
        public static UnityEngine.UI.GridLayoutGroup.Corner UpperRight;  // const
        public static UnityEngine.UI.GridLayoutGroup.Corner LowerLeft;  // const
        public static UnityEngine.UI.GridLayoutGroup.Corner LowerRight;  // const

    }

    // TypeToken: 0x200004A  // size: 0x14
    public sealed struct Axis
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.GridLayoutGroup.Axis Horizontal;  // const
        public static UnityEngine.UI.GridLayoutGroup.Axis Vertical;  // const

    }

    // TypeToken: 0x200004B  // size: 0x14
    public sealed struct Constraint
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.GridLayoutGroup.Constraint Flexible;  // const
        public static UnityEngine.UI.GridLayoutGroup.Constraint FixedColumnCount;  // const
        public static UnityEngine.UI.GridLayoutGroup.Constraint FixedRowCount;  // const

    }

    // TypeToken: 0x2000055  // size: 0x28
    public sealed class <DelayedSetDirty>d__56 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public UnityEngine.RectTransform rectTransform;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000328
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000329
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x04294B40  token: 0x600032A
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0B572450  token: 0x600032C
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000057  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly UnityEngine.UI.LayoutRebuilder.<>c <>9;  // static @ 0x0
        public static System.Predicate<UnityEngine.Component> <>9__10_0;  // static @ 0x8
        public static UnityEngine.Events.UnityAction<UnityEngine.Component> <>9__12_0;  // static @ 0x10
        public static UnityEngine.Events.UnityAction<UnityEngine.Component> <>9__12_1;  // static @ 0x18
        public static UnityEngine.Events.UnityAction<UnityEngine.Component> <>9__12_2;  // static @ 0x20
        public static UnityEngine.Events.UnityAction<UnityEngine.Component> <>9__12_3;  // static @ 0x28

        // Methods
        // RVA: 0x04D4C990  token: 0x6000342
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000343
        public System.Void .ctor() { }
        // RVA: 0x0353E490  token: 0x6000344
        private UnityEngine.UI.LayoutRebuilder <.cctor>b__5_0() { }
        // RVA: 0x03E504C0  token: 0x6000345
        private System.Void <.cctor>b__5_1(UnityEngine.UI.LayoutRebuilder x) { }
        // RVA: 0x03A18CE0  token: 0x6000346
        private System.Boolean <StripDisabledBehavioursFromList>b__10_0(UnityEngine.Component e) { }
        // RVA: 0x03B91740  token: 0x6000347
        private System.Void <Rebuild>b__12_0(UnityEngine.Component e) { }
        // RVA: 0x031E8FA0  token: 0x6000348
        private System.Void <Rebuild>b__12_1(UnityEngine.Component e) { }
        // RVA: 0x03B91960  token: 0x6000349
        private System.Void <Rebuild>b__12_2(UnityEngine.Component e) { }
        // RVA: 0x03EF8680  token: 0x600034A
        private System.Void <Rebuild>b__12_3(UnityEngine.Component e) { }

    }

    // TypeToken: 0x2000059  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly UnityEngine.UI.LayoutUtility.<>c <>9;  // static @ 0x0
        public static System.Func<UnityEngine.UI.ILayoutElement,System.Single> <>9__3_0;  // static @ 0x8
        public static System.Func<UnityEngine.UI.ILayoutElement,System.Single> <>9__4_0;  // static @ 0x10
        public static System.Func<UnityEngine.UI.ILayoutElement,System.Single> <>9__4_1;  // static @ 0x18
        public static System.Func<UnityEngine.UI.ILayoutElement,System.Single> <>9__5_0;  // static @ 0x20
        public static System.Func<UnityEngine.UI.ILayoutElement,System.Single> <>9__6_0;  // static @ 0x28
        public static System.Func<UnityEngine.UI.ILayoutElement,System.Single> <>9__7_0;  // static @ 0x30
        public static System.Func<UnityEngine.UI.ILayoutElement,System.Single> <>9__7_1;  // static @ 0x38
        public static System.Func<UnityEngine.UI.ILayoutElement,System.Single> <>9__8_0;  // static @ 0x40

        // Methods
        // RVA: 0x04D4CB70  token: 0x6000356
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000357
        public System.Void .ctor() { }
        // RVA: 0x03C6D650  token: 0x6000358
        private System.Single <GetMinWidth>b__3_0(UnityEngine.UI.ILayoutElement e) { }
        // RVA: 0x03C6D670  token: 0x6000359
        private System.Single <GetPreferredWidth>b__4_0(UnityEngine.UI.ILayoutElement e) { }
        // RVA: 0x031E9B20  token: 0x600035A
        private System.Single <GetPreferredWidth>b__4_1(UnityEngine.UI.ILayoutElement e) { }
        // RVA: 0x031E95A0  token: 0x600035B
        private System.Single <GetFlexibleWidth>b__5_0(UnityEngine.UI.ILayoutElement e) { }
        // RVA: 0x031E97C0  token: 0x600035C
        private System.Single <GetMinHeight>b__6_0(UnityEngine.UI.ILayoutElement e) { }
        // RVA: 0x031E97C0  token: 0x600035D
        private System.Single <GetPreferredHeight>b__7_0(UnityEngine.UI.ILayoutElement e) { }
        // RVA: 0x031E96D0  token: 0x600035E
        private System.Single <GetPreferredHeight>b__7_1(UnityEngine.UI.ILayoutElement e) { }
        // RVA: 0x031E9890  token: 0x600035F
        private System.Single <GetFlexibleHeight>b__8_0(UnityEngine.UI.ILayoutElement e) { }

    }

    // TypeToken: 0x200005D  // size: 0x30
    public class CullStateChangedEvent : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x0539350C  token: 0x6000389
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000063  // size: 0x14
    public sealed struct Mode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.Navigation.Mode None;  // const
        public static UnityEngine.UI.Navigation.Mode Horizontal;  // const
        public static UnityEngine.UI.Navigation.Mode Vertical;  // const
        public static UnityEngine.UI.Navigation.Mode Automatic;  // const
        public static UnityEngine.UI.Navigation.Mode Explicit;  // const

    }

    // TypeToken: 0x2000067  // size: 0x14
    public sealed struct Direction
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.Scrollbar.Direction LeftToRight;  // const
        public static UnityEngine.UI.Scrollbar.Direction RightToLeft;  // const
        public static UnityEngine.UI.Scrollbar.Direction BottomToTop;  // const
        public static UnityEngine.UI.Scrollbar.Direction TopToBottom;  // const

    }

    // TypeToken: 0x2000068  // size: 0x30
    public class ScrollEvent : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x0453A600  token: 0x60003FA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000069  // size: 0x14
    public sealed struct Axis
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.Scrollbar.Axis Horizontal;  // const
        public static UnityEngine.UI.Scrollbar.Axis Vertical;  // const

    }

    // TypeToken: 0x200006A  // size: 0x38
    public sealed class <ClickRepeat>d__58 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public UnityEngine.UI.Scrollbar <>4__this;  // 0x20
        public UnityEngine.Vector2 screenPosition;  // 0x28
        public UnityEngine.Camera camera;  // 0x30

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60003FB
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60003FC
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0B57A574  token: 0x60003FD
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0B57A79C  token: 0x60003FF
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200006C  // size: 0x14
    public sealed struct MovementType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.ScrollRect.MovementType Unrestricted;  // const
        public static UnityEngine.UI.ScrollRect.MovementType Elastic;  // const
        public static UnityEngine.UI.ScrollRect.MovementType Clamped;  // const

    }

    // TypeToken: 0x200006D  // size: 0x14
    public sealed struct ScrollbarVisibility
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.ScrollRect.ScrollbarVisibility Permanent;  // const
        public static UnityEngine.UI.ScrollRect.ScrollbarVisibility AutoHide;  // const
        public static UnityEngine.UI.ScrollRect.ScrollbarVisibility AutoHideAndExpandViewport;  // const

    }

    // TypeToken: 0x200006E  // size: 0x30
    public class ScrollRectEvent : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x05396540  token: 0x600045E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000070  // size: 0x14
    public sealed struct Transition
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.Selectable.Transition None;  // const
        public static UnityEngine.UI.Selectable.Transition ColorTint;  // const
        public static UnityEngine.UI.Selectable.Transition SpriteSwap;  // const
        public static UnityEngine.UI.Selectable.Transition Animation;  // const

    }

    // TypeToken: 0x2000071  // size: 0x14
    public sealed struct SelectionState
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.Selectable.SelectionState Normal;  // const
        public static UnityEngine.UI.Selectable.SelectionState Highlighted;  // const
        public static UnityEngine.UI.Selectable.SelectionState Pressed;  // const
        public static UnityEngine.UI.Selectable.SelectionState Selected;  // const
        public static UnityEngine.UI.Selectable.SelectionState Disabled;  // const

    }

    // TypeToken: 0x2000078  // size: 0x14
    public sealed struct Direction
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.Slider.Direction LeftToRight;  // const
        public static UnityEngine.UI.Slider.Direction RightToLeft;  // const
        public static UnityEngine.UI.Slider.Direction BottomToTop;  // const
        public static UnityEngine.UI.Slider.Direction TopToBottom;  // const

    }

    // TypeToken: 0x2000079  // size: 0x30
    public class SliderEvent : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x05397550  token: 0x60004FF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200007A  // size: 0x14
    public sealed struct Axis
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.Slider.Axis Horizontal;  // const
        public static UnityEngine.UI.Slider.Axis Vertical;  // const

    }

    // TypeToken: 0x200007D  // size: 0x40
    public class MatEntry
    {
        // Fields
        public UnityEngine.Material baseMat;  // 0x10
        public UnityEngine.Material customMat;  // 0x18
        public System.Int32 count;  // 0x20
        public System.Int32 stencilId;  // 0x24
        public UnityEngine.Rendering.StencilOp operation;  // 0x28
        public UnityEngine.Rendering.CompareFunction compareFunction;  // 0x2c
        public System.Int32 readMask;  // 0x30
        public System.Int32 writeMask;  // 0x34
        public System.Boolean useAlphaClip;  // 0x38
        public UnityEngine.Rendering.ColorWriteMask colorMask;  // 0x3c

        // Methods
        // RVA: 0x04DBEFE0  token: 0x6000510
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000080  // size: 0x14
    public sealed struct ToggleTransition
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.Toggle.ToggleTransition None;  // const
        public static UnityEngine.UI.Toggle.ToggleTransition Fade;  // const

    }

    // TypeToken: 0x2000081  // size: 0x30
    public class ToggleEvent : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x0539350C  token: 0x6000559
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000083  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly UnityEngine.UI.ToggleGroup.<>c <>9;  // static @ 0x0
        public static System.Predicate<UnityEngine.UI.Toggle> <>9__13_0;  // static @ 0x8
        public static System.Func<UnityEngine.UI.Toggle,System.Boolean> <>9__14_0;  // static @ 0x10

        // Methods
        // RVA: 0x04D45970  token: 0x6000568
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000569
        public System.Void .ctor() { }
        // RVA: 0x0B57A7E8  token: 0x600056A
        private System.Boolean <AnyTogglesOn>b__13_0(UnityEngine.UI.Toggle x) { }
        // RVA: 0x0B57A7E8  token: 0x600056B
        private System.Boolean <ActiveToggles>b__14_0(UnityEngine.UI.Toggle x) { }

    }

    // TypeToken: 0x2000085  // size: 0x80
    public sealed class Raycast3DCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0B5732D8  token: 0x600056E
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x0B572AF0  token: 0x600056F
        public virtual System.Boolean Invoke(UnityEngine.Ray r, UnityEngine.RaycastHit& hit, System.Single f, System.Int32 i) { }
        // RVA: 0x0B5731F0  token: 0x6000570
        public virtual System.IAsyncResult BeginInvoke(UnityEngine.Ray r, UnityEngine.RaycastHit& hit, System.Single f, System.Int32 i, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x05FD1038  token: 0x6000571
        public virtual System.Boolean EndInvoke(UnityEngine.RaycastHit& hit, System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000086  // size: 0x80
    public sealed class RaycastAllCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0B572958  token: 0x6000572
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x0B572920  token: 0x6000573
        public virtual UnityEngine.RaycastHit[] Invoke(UnityEngine.Ray r, System.Single f, System.Int32 i) { }
        // RVA: 0x0B5733A0  token: 0x6000574
        public virtual System.IAsyncResult BeginInvoke(UnityEngine.Ray r, System.Single f, System.Int32 i, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x05397E84  token: 0x6000575
        public virtual UnityEngine.RaycastHit[] EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000087  // size: 0x80
    public sealed class GetRaycastNonAllocCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0B572B30  token: 0x6000576
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x0B572AF0  token: 0x6000577
        public virtual System.Int32 Invoke(UnityEngine.Ray r, UnityEngine.RaycastHit[] results, System.Single f, System.Int32 i) { }
        // RVA: 0x0B572BF8  token: 0x6000578
        public virtual System.IAsyncResult BeginInvoke(UnityEngine.Ray r, UnityEngine.RaycastHit[] results, System.Single f, System.Int32 i, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x0539AB64  token: 0x6000579
        public virtual System.Int32 EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000088  // size: 0x80
    public sealed class Raycast2DCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0B573128  token: 0x600057A
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x0B5730CC  token: 0x600057B
        public virtual UnityEngine.RaycastHit2D Invoke(UnityEngine.Vector2 p1, UnityEngine.Vector2 p2, System.Single f, System.Int32 i) { }
        // RVA: 0x0B573004  token: 0x600057C
        public virtual System.IAsyncResult BeginInvoke(UnityEngine.Vector2 p1, UnityEngine.Vector2 p2, System.Single f, System.Int32 i, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x08420754  token: 0x600057D
        public virtual UnityEngine.RaycastHit2D EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000089  // size: 0x80
    public sealed class GetRayIntersectionAllCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0B572958  token: 0x600057E
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x0B572920  token: 0x600057F
        public virtual UnityEngine.RaycastHit2D[] Invoke(UnityEngine.Ray r, System.Single f, System.Int32 i) { }
        // RVA: 0x0B572868  token: 0x6000580
        public virtual System.IAsyncResult BeginInvoke(UnityEngine.Ray r, System.Single f, System.Int32 i, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x05397E84  token: 0x6000581
        public virtual UnityEngine.RaycastHit2D[] EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x200008A  // size: 0x80
    public sealed class GetRayIntersectionAllNonAllocCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0B572B30  token: 0x6000582
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x0B572AF0  token: 0x6000583
        public virtual System.Int32 Invoke(UnityEngine.Ray r, UnityEngine.RaycastHit2D[] results, System.Single f, System.Int32 i) { }
        // RVA: 0x0B572A20  token: 0x6000584
        public virtual System.IAsyncResult BeginInvoke(UnityEngine.Ray r, UnityEngine.RaycastHit2D[] results, System.Single f, System.Int32 i, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x0539AB64  token: 0x6000585
        public virtual System.Int32 EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000096  // size: 0x14
    public sealed struct ColorTweenMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.CoroutineTween.ColorTween.ColorTweenMode All;  // const
        public static UnityEngine.UI.CoroutineTween.ColorTween.ColorTweenMode RGB;  // const
        public static UnityEngine.UI.CoroutineTween.ColorTween.ColorTweenMode Alpha;  // const

    }

    // TypeToken: 0x2000097  // size: 0x30
    public class ColorTweenCallback : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x05397350  token: 0x60005D9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000099  // size: 0x30
    public class FloatTweenCallback : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x0B57D574  token: 0x60005E7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200009B
    public sealed class <Start>d__2 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private System.Object <>2__current;  // 0x0
        public T tweenInfo;  // 0x0
        private System.Single <elapsedTime>5__2;  // 0x0

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60005ED
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x60005EE
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x60005EF
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x60005F1
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200009D  // size: 0x14
    public sealed struct PointerEventType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UIElements.PanelEventHandler.PointerEventType Default;  // const
        public static UnityEngine.UIElements.PanelEventHandler.PointerEventType Down;  // const
        public static UnityEngine.UIElements.PanelEventHandler.PointerEventType Up;  // const

    }

    // TypeToken: 0x200009E  // size: 0x80
    public class PointerEvent : UnityEngine.UIElements.IPointerEvent
    {
        // Fields
        private System.Int32 <pointerId>k__BackingField;  // 0x10
        private System.String <pointerType>k__BackingField;  // 0x18
        private System.Boolean <isPrimary>k__BackingField;  // 0x20
        private System.Int32 <button>k__BackingField;  // 0x24
        private System.Int32 <pressedButtons>k__BackingField;  // 0x28
        private UnityEngine.Vector3 <position>k__BackingField;  // 0x2c
        private UnityEngine.Vector3 <localPosition>k__BackingField;  // 0x38
        private UnityEngine.Vector3 <deltaPosition>k__BackingField;  // 0x44
        private System.Single <deltaTime>k__BackingField;  // 0x50
        private System.Int32 <clickCount>k__BackingField;  // 0x54
        private System.Single <pressure>k__BackingField;  // 0x58
        private System.Single <tangentialPressure>k__BackingField;  // 0x5c
        private System.Single <altitudeAngle>k__BackingField;  // 0x60
        private System.Single <azimuthAngle>k__BackingField;  // 0x64
        private System.Single <twist>k__BackingField;  // 0x68
        private UnityEngine.Vector2 <radius>k__BackingField;  // 0x6c
        private UnityEngine.Vector2 <radiusVariance>k__BackingField;  // 0x74
        private UnityEngine.EventModifiers <modifiers>k__BackingField;  // 0x7c

        // Properties
        System.Int32 pointerId { get; /* RVA: 0x02B2E2D0 */ set; /* RVA: 0x014F51F0 */ }
        System.String pointerType { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.Boolean isPrimary { get; /* RVA: 0x04D866B0 */ set; /* RVA: 0x04D866D0 */ }
        System.Int32 button { get; /* RVA: 0x011F0020 */ set; /* RVA: 0x011F0030 */ }
        System.Int32 pressedButtons { get; /* RVA: 0x011F2A90 */ set; /* RVA: 0x011F2AA0 */ }
        UnityEngine.Vector3 position { get; /* RVA: 0x04D90540 */ set; /* RVA: 0x04D90510 */ }
        UnityEngine.Vector3 localPosition { get; /* RVA: 0x04D9E700 */ set; /* RVA: 0x04D9DB00 */ }
        UnityEngine.Vector3 deltaPosition { get; /* RVA: 0x04D914A0 */ set; /* RVA: 0x04D914C0 */ }
        System.Single deltaTime { get; /* RVA: 0x04D885A0 */ set; /* RVA: 0x04D88FF0 */ }
        System.Int32 clickCount { get; /* RVA: 0x04D86540 */ set; /* RVA: 0x04D86590 */ }
        System.Single pressure { get; /* RVA: 0x04D88400 */ set; /* RVA: 0x04D88430 */ }
        System.Single tangentialPressure { get; /* RVA: 0x04D91500 */ set; /* RVA: 0x04D91510 */ }
        System.Single altitudeAngle { get; /* RVA: 0x04D88480 */ set; /* RVA: 0x04D884A0 */ }
        System.Single azimuthAngle { get; /* RVA: 0x04D88470 */ set; /* RVA: 0x04D88490 */ }
        System.Single twist { get; /* RVA: 0x04D8F060 */ set; /* RVA: 0x04D8F090 */ }
        UnityEngine.Vector2 radius { get; /* RVA: 0x04DBF340 */ set; /* RVA: 0x04DBF370 */ }
        UnityEngine.Vector2 radiusVariance { get; /* RVA: 0x04DBF320 */ set; /* RVA: 0x04DBF360 */ }
        UnityEngine.EventModifiers modifiers { get; /* RVA: 0x04D86410 */ set; /* RVA: 0x04D86490 */ }
        System.Boolean shiftKey { get; /* RVA: 0x0B5817D4 */ }
        System.Boolean ctrlKey { get; /* RVA: 0x0B5817CC */ }
        System.Boolean commandKey { get; /* RVA: 0x0B5817C4 */ }
        System.Boolean altKey { get; /* RVA: 0x0B5817BC */ }
        System.Boolean actionKey { get; /* RVA: 0x0B581788 */ }

        // Methods
        // RVA: 0x0B581260  token: 0x600063F
        public System.Void Read(UnityEngine.UIElements.PanelEventHandler self, UnityEngine.EventSystems.PointerEventData eventData, UnityEngine.UIElements.PanelEventHandler.PointerEventType eventType) { }
        // RVA: 0x04DBF2F0  token: 0x6000640
        public System.Void SetPosition(UnityEngine.Vector3 positionOverride, UnityEngine.Vector3 deltaOverride) { }
        // RVA: 0x041E1670  token: 0x6000641
        public System.Void .ctor() { }
        // RVA: 0x0B581774  token: 0x6000642
        private static System.Boolean <Read>g__InRange|82_0(System.Int32 i, System.Int32 start, System.Int32 count) { }

    }

    // TypeToken: 0x20000A0  // size: 0x14
    public sealed struct FloatIntBits
    {
        // Fields
        public System.Single f;  // 0x10
        public System.Int32 i;  // 0x10

    }

    // TypeToken: 0x20000A5  // size: 0x14
    public sealed struct InputButton
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.EventSystems.PointerEventData.InputButton Left;  // const
        public static UnityEngine.EventSystems.PointerEventData.InputButton Right;  // const
        public static UnityEngine.EventSystems.PointerEventData.InputButton Middle;  // const

    }

    // TypeToken: 0x20000A6  // size: 0x14
    public sealed struct FramePressState
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.EventSystems.PointerEventData.FramePressState Pressed;  // const
        public static UnityEngine.EventSystems.PointerEventData.FramePressState Released;  // const
        public static UnityEngine.EventSystems.PointerEventData.FramePressState PressedAndReleased;  // const
        public static UnityEngine.EventSystems.PointerEventData.FramePressState NotChanged;  // const

    }

    // TypeToken: 0x20000BC  // size: 0x10
    public static class ProfilerMarkers
    {
        // Fields
        public static readonly Unity.Profiling.ProfilerMarker Update;  // static @ 0x0
        public static readonly Unity.Profiling.ProfilerMarker TickModules;  // static @ 0x8
        public static readonly Unity.Profiling.ProfilerMarker CurrentModuleProcess;  // static @ 0x10
        public static readonly Unity.Profiling.ProfilerMarker RaycastAll;  // static @ 0x18
        public static readonly Unity.Profiling.ProfilerMarker RaycastAllSort;  // static @ 0x20

        // Methods
        // RVA: 0x04B1D7A0  token: 0x60006D5
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000BD  // size: 0x20
    public sealed struct UIToolkitOverrideConfig
    {
        // Fields
        public UnityEngine.EventSystems.EventSystem activeEventSystem;  // 0x10
        public System.Boolean sendEvents;  // 0x18
        public System.Boolean createPanelGameObjectsOnStart;  // 0x19

    }

    // TypeToken: 0x20000BE  // size: 0x18
    public sealed class <>c__DisplayClass53_0
    {
        // Fields
        public UnityEngine.GameObject go;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60006D6
        public System.Void .ctor() { }
        // RVA: 0x0B588900  token: 0x60006D7
        private System.Void <CreateUIToolkitPanelGameObject>b__0() { }

    }

    // TypeToken: 0x20000C0  // size: 0x30
    public class TriggerEvent : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x0B5888C4  token: 0x60006EF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C1  // size: 0x20
    public class Entry
    {
        // Fields
        public UnityEngine.EventSystems.EventTriggerType eventID;  // 0x10
        public UnityEngine.EventSystems.EventTrigger.TriggerEvent callback;  // 0x18

        // Methods
        // RVA: 0x0B57C194  token: 0x60006F0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C4
    public sealed class EventFunction`1 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x600071E
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x600071F
        public virtual System.Void Invoke(T1 handler, UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: -1  // runtime  token: 0x6000720
        public virtual System.IAsyncResult BeginInvoke(T1 handler, UnityEngine.EventSystems.BaseEventData eventData, System.AsyncCallback callback, System.Object object) { }
        // RVA: -1  // runtime  token: 0x6000721
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x20000C8  // size: 0x20
    public class ButtonState
    {
        // Fields
        private UnityEngine.EventSystems.PointerEventData.InputButton m_Button;  // 0x10
        private UnityEngine.EventSystems.PointerInputModule.MouseButtonEventData m_EventData;  // 0x18

        // Properties
        UnityEngine.EventSystems.PointerInputModule.MouseButtonEventData eventData { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        UnityEngine.EventSystems.PointerEventData.InputButton button { get; /* RVA: 0x02B2E2D0 */ set; /* RVA: 0x014F51F0 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x600075D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C9  // size: 0x18
    public class MouseState
    {
        // Fields
        private System.Collections.Generic.List<UnityEngine.EventSystems.PointerInputModule.ButtonState> m_TrackedButtons;  // 0x10

        // Methods
        // RVA: 0x0B57D6AC  token: 0x600075E
        public System.Boolean AnyPressesThisFrame() { }
        // RVA: 0x0B57D74C  token: 0x600075F
        public System.Boolean AnyReleasesThisFrame() { }
        // RVA: 0x0B57D7EC  token: 0x6000760
        public UnityEngine.EventSystems.PointerInputModule.ButtonState GetButtonState(UnityEngine.EventSystems.PointerEventData.InputButton button) { }
        // RVA: 0x0B57D924  token: 0x6000761
        public System.Void SetButtonState(UnityEngine.EventSystems.PointerEventData.InputButton button, UnityEngine.EventSystems.PointerEventData.FramePressState stateForMouseButton, UnityEngine.EventSystems.PointerEventData data) { }
        // RVA: 0x0B57D974  token: 0x6000762
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000CA  // size: 0x20
    public class MouseButtonEventData
    {
        // Fields
        public UnityEngine.EventSystems.PointerEventData.FramePressState buttonState;  // 0x10
        public UnityEngine.EventSystems.PointerEventData buttonData;  // 0x18

        // Methods
        // RVA: 0x0A9CB864  token: 0x6000763
        public System.Boolean PressedThisFrame() { }
        // RVA: 0x0863E6DC  token: 0x6000764
        public System.Boolean ReleasedThisFrame() { }
        // RVA: 0x041E1670  token: 0x6000765
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000CC  // size: 0x10
    public static class ProfilerMarkers
    {
        // Fields
        public static readonly Unity.Profiling.ProfilerMarker Process;  // static @ 0x0
        public static readonly Unity.Profiling.ProfilerMarker ProcessMouseEvent;  // static @ 0x8
        public static readonly Unity.Profiling.ProfilerMarker ProcessMousePress;  // static @ 0x10
        public static readonly Unity.Profiling.ProfilerMarker ProcessTouchEvents;  // static @ 0x18
        public static readonly Unity.Profiling.ProfilerMarker ProcessTouchPress;  // static @ 0x20

        // Methods
        // RVA: 0x0B583294  token: 0x600078A
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000CD  // size: 0x14
    public sealed struct InputMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.EventSystems.StandaloneInputModule.InputMode Mouse;  // const
        public static UnityEngine.EventSystems.StandaloneInputModule.InputMode Buttons;  // const

    }

    // TypeToken: 0x20000CF  // size: 0x10
    public static class ProfilerMarkers
    {
        // Fields
        public static readonly Unity.Profiling.ProfilerMarker Process;  // static @ 0x0
        public static readonly Unity.Profiling.ProfilerMarker ProcessTouchEvents;  // static @ 0x8
        public static readonly Unity.Profiling.ProfilerMarker ProcessTouchPress;  // static @ 0x10

        // Methods
        // RVA: 0x0B5831C0  token: 0x600079A
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000D5  // size: 0x10
    public class RaycastHitComparer : System.Collections.Generic.IComparer`1
    {
        // Fields
        public static UnityEngine.EventSystems.PhysicsRaycaster.RaycastHitComparer instance;  // static @ 0x0

        // Methods
        // RVA: 0x0B5834DC  token: 0x60007B7
        public virtual System.Int32 Compare(UnityEngine.RaycastHit x, UnityEngine.RaycastHit y) { }
        // RVA: 0x041E1670  token: 0x60007B8
        public System.Void .ctor() { }
        // RVA: 0x0B583508  token: 0x60007B9
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000DB  // size: 0x11
    public sealed struct AutoScope : System.IDisposable
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60007DB
        private System.Void .ctor(Unity.Profiling.ProfilerMarker<TP1> marker, TP1 p1) { }
        // RVA: -1  // not resolved  token: 0x60007DC
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x20000DD  // size: 0x11
    public sealed struct AutoScope : System.IDisposable
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60007E2
        private System.Void .ctor(Unity.Profiling.ProfilerMarker<TP1,TP2> marker, TP1 p1, TP2 p2) { }
        // RVA: -1  // not resolved  token: 0x60007E3
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x20000DF  // size: 0x11
    public sealed struct AutoScope : System.IDisposable
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60007E9
        private System.Void .ctor(Unity.Profiling.ProfilerMarker<TP1,TP2,TP3> marker, TP1 p1, TP2 p2, TP3 p3) { }
        // RVA: -1  // not resolved  token: 0x60007EA
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x20000E1  // size: 0x11
    public sealed struct ProfilerMarkerScope : System.IDisposable
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60007FE
        public System.Void .ctor(Unity.Profiling.ProfilerMarker& marker) { }
        // RVA: 0x041E1670  token: 0x60007FF
        public System.Void .ctor(Unity.Profiling.ProfilerMarker& marker, System.Int32 context) { }
        // RVA: 0x041E1670  token: 0x6000800
        public System.Void .ctor(Unity.Profiling.ProfilerMarker& marker, System.Int64 context) { }
        // RVA: 0x041E1670  token: 0x6000801
        public System.Void .ctor(Unity.Profiling.ProfilerMarker& marker, System.Single context) { }
        // RVA: 0x041E1670  token: 0x6000802
        public System.Void .ctor(Unity.Profiling.ProfilerMarker& marker, System.String context) { }
        // RVA: 0x041E1670  token: 0x6000803
        public System.Void .ctor(Unity.Profiling.ProfilerMarker& marker, System.String context, System.String context2) { }
        // RVA: 0x041E1670  token: 0x6000804
        public System.Void .ctor(Unity.Profiling.ProfilerMarker& marker, System.Int64 context, System.String context2) { }
        // RVA: 0x041E1670  token: 0x6000805
        public System.Void .ctor(Unity.Profiling.ProfilerMarker& marker, System.Int64 context, System.Int64 context2) { }
        // RVA: 0x041E1670  token: 0x6000806
        public System.Void .ctor(Unity.Profiling.ProfilerMarker& marker, UnityEngine.Object obj) { }
        // RVA: 0x041E1670  token: 0x6000807
        public System.Void .ctor(Unity.Profiling.ProfilerMarker& marker, UnityEngine.Transform transform, System.Boolean path) { }
        // RVA: 0x041E1670  token: 0x6000808
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x20000E9  // size: 0x1C
    public sealed struct __StaticArrayInitTypeSize=12
    {
    }

    // TypeToken: 0x20000E8  // size: 0x10
    public sealed class <PrivateImplementationDetails>
    {
        // Fields
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=12 1C3635C112D556F4C11A4FE6BDE6ED3F126C4B2B546811BDB64DE7BDED3A05CB;  // static @ 0x0

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
    public sealed class IsUnmanagedAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000002
        public System.Void .ctor() { }

    }

}

namespace Unity.Profiling
{

    // TypeToken: 0x20000D8
    public sealed struct ProfilerCounter`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60007CC
        public System.Void .ctor(Unity.Profiling.ProfilerCategory category, System.String name, Unity.Profiling.ProfilerMarkerDataUnit dataUnit) { }
        // RVA: -1  // not resolved  token: 0x60007CD
        public System.Void Sample(T value) { }

    }

    // TypeToken: 0x20000D9  // size: 0x11
    public sealed struct ProfilerCounterValue`1
    {
        // Properties
        T Value { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60007CE
        public System.Void .ctor(System.String name) { }
        // RVA: -1  // not resolved  token: 0x60007CF
        public System.Void .ctor(System.String name, Unity.Profiling.ProfilerMarkerDataUnit dataUnit) { }
        // RVA: -1  // not resolved  token: 0x60007D0
        public System.Void .ctor(System.String name, Unity.Profiling.ProfilerMarkerDataUnit dataUnit, Unity.Profiling.ProfilerCounterOptions counterOptions) { }
        // RVA: -1  // not resolved  token: 0x60007D1
        public System.Void .ctor(Unity.Profiling.ProfilerCategory category, System.String name, Unity.Profiling.ProfilerMarkerDataUnit dataUnit) { }
        // RVA: -1  // not resolved  token: 0x60007D2
        public System.Void .ctor(Unity.Profiling.ProfilerCategory category, System.String name, Unity.Profiling.ProfilerMarkerDataUnit dataUnit, Unity.Profiling.ProfilerCounterOptions counterOptions) { }
        // RVA: -1  // not resolved  token: 0x60007D5
        public System.Void Sample() { }

    }

    // TypeToken: 0x20000DA  // size: 0x11
    public sealed struct ProfilerMarker`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60007D6
        public System.Void .ctor(System.String name, System.String param1Name) { }
        // RVA: -1  // not resolved  token: 0x60007D7
        public System.Void .ctor(Unity.Profiling.ProfilerCategory category, System.String name, System.String param1Name) { }
        // RVA: -1  // not resolved  token: 0x60007D8
        public System.Void Begin(TP1 p1) { }
        // RVA: -1  // not resolved  token: 0x60007D9
        public System.Void End() { }
        // RVA: -1  // not resolved  token: 0x60007DA
        public Unity.Profiling.ProfilerMarker.AutoScope<TP1> Auto(TP1 p1) { }

    }

    // TypeToken: 0x20000DC  // size: 0x11
    public sealed struct ProfilerMarker`2
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60007DD
        public System.Void .ctor(System.String name, System.String param1Name, System.String param2Name) { }
        // RVA: -1  // not resolved  token: 0x60007DE
        public System.Void .ctor(Unity.Profiling.ProfilerCategory category, System.String name, System.String param1Name, System.String param2Name) { }
        // RVA: -1  // not resolved  token: 0x60007DF
        public System.Void Begin(TP1 p1, TP2 p2) { }
        // RVA: -1  // not resolved  token: 0x60007E0
        public System.Void End() { }
        // RVA: -1  // not resolved  token: 0x60007E1
        public Unity.Profiling.ProfilerMarker.AutoScope<TP1,TP2> Auto(TP1 p1, TP2 p2) { }

    }

    // TypeToken: 0x20000DE  // size: 0x11
    public sealed struct ProfilerMarker`3
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60007E4
        public System.Void .ctor(System.String name, System.String param1Name, System.String param2Name, System.String param3Name) { }
        // RVA: -1  // not resolved  token: 0x60007E5
        public System.Void .ctor(Unity.Profiling.ProfilerCategory category, System.String name, System.String param1Name, System.String param2Name, System.String param3Name) { }
        // RVA: -1  // not resolved  token: 0x60007E6
        public System.Void Begin(TP1 p1, TP2 p2, TP3 p3) { }
        // RVA: -1  // not resolved  token: 0x60007E7
        public System.Void End() { }
        // RVA: -1  // not resolved  token: 0x60007E8
        public Unity.Profiling.ProfilerMarker.AutoScope<TP1,TP2,TP3> Auto(TP1 p1, TP2 p2, TP3 p3) { }

    }

    // TypeToken: 0x20000E0  // size: 0x10
    public static class ProfilerMarkerExtension
    {
        // Methods
        // RVA: 0x0B582C80  token: 0x60007EB
        public static System.Void Begin(Unity.Profiling.ProfilerMarker marker, System.Int32 metadata) { }
        // RVA: 0x0B58314C  token: 0x60007EC
        public static System.Void Begin(Unity.Profiling.ProfilerMarker marker, System.UInt32 metadata) { }
        // RVA: 0x0B58305C  token: 0x60007ED
        public static System.Void Begin(Unity.Profiling.ProfilerMarker marker, System.Int64 metadata) { }
        // RVA: 0x0B5830D4  token: 0x60007EE
        public static System.Void Begin(Unity.Profiling.ProfilerMarker marker, System.UInt64 metadata) { }
        // RVA: 0x0B582E34  token: 0x60007EF
        public static System.Void Begin(Unity.Profiling.ProfilerMarker marker, System.Single metadata) { }
        // RVA: 0x0B582CF4  token: 0x60007F0
        public static System.Void Begin(Unity.Profiling.ProfilerMarker marker, System.Double metadata) { }
        // RVA: 0x0B582C2C  token: 0x60007F1
        public static System.Void Begin(Unity.Profiling.ProfilerMarker marker, System.String metadata) { }
        // RVA: 0x0B582EAC  token: 0x60007F2
        public static System.Void Begin(Unity.Profiling.ProfilerMarker marker, System.String metadata, System.String metadata2) { }
        // RVA: 0x0B582F84  token: 0x60007F3
        public static System.Void Begin(Unity.Profiling.ProfilerMarker marker, System.Int64 metadata, System.String metadata2) { }
        // RVA: 0x0B582D6C  token: 0x60007F4
        public static System.Void Begin(Unity.Profiling.ProfilerMarker marker, System.Int64 metadata, System.Int64 metadata2) { }
        // RVA: 0x012081B0  token: 0x60007F5
        public static Unity.Profiling.ProfilerMarkerExtension.ProfilerMarkerScope Scope(Unity.Profiling.ProfilerMarker marker) { }
        // RVA: 0x04DBF390  token: 0x60007F6
        public static Unity.Profiling.ProfilerMarkerExtension.ProfilerMarkerScope Scope(Unity.Profiling.ProfilerMarker marker, System.Int32 context) { }
        // RVA: 0x04DBF390  token: 0x60007F7
        public static Unity.Profiling.ProfilerMarkerExtension.ProfilerMarkerScope Scope(Unity.Profiling.ProfilerMarker marker, System.Int64 context) { }
        // RVA: 0x04DBF390  token: 0x60007F8
        public static Unity.Profiling.ProfilerMarkerExtension.ProfilerMarkerScope Scope(Unity.Profiling.ProfilerMarker marker, System.Single context) { }
        // RVA: 0x04DBF390  token: 0x60007F9
        public static Unity.Profiling.ProfilerMarkerExtension.ProfilerMarkerScope Scope(Unity.Profiling.ProfilerMarker marker, System.String context) { }
        // RVA: 0x04DBF380  token: 0x60007FA
        public static Unity.Profiling.ProfilerMarkerExtension.ProfilerMarkerScope Scope(Unity.Profiling.ProfilerMarker marker, System.String context, System.String context2) { }
        // RVA: 0x04DBF380  token: 0x60007FB
        public static Unity.Profiling.ProfilerMarkerExtension.ProfilerMarkerScope Scope(Unity.Profiling.ProfilerMarker marker, System.Int64 context, System.String context2) { }
        // RVA: 0x04DBF380  token: 0x60007FC
        public static Unity.Profiling.ProfilerMarkerExtension.ProfilerMarkerScope Scope(Unity.Profiling.ProfilerMarker marker, System.Int64 context, System.Int64 context2) { }
        // RVA: 0x04DBF380  token: 0x60007FD
        public static Unity.Profiling.ProfilerMarkerExtension.ProfilerMarkerScope Scope(Unity.Profiling.ProfilerMarker marker, UnityEngine.Transform transform, System.Boolean path) { }

    }

    // TypeToken: 0x20000E2  // size: 0x20
    public sealed struct MonoObject_64
    {
        // Fields
        public System.Void* p1;  // 0x10
        public System.Void* p2;  // 0x18

    }

    // TypeToken: 0x20000E3  // size: 0x20
    public sealed struct MonoObject_32
    {
        // Fields
        public System.Void* p1;  // 0x10
        public System.Void* p2;  // 0x14

    }

    // TypeToken: 0x20000E4  // size: 0x28
    public sealed struct MonoString_64
    {
        // Fields
        public Unity.Profiling.MonoObject_64 obj;  // 0x10
        public System.Int32 length;  // 0x20
        public System.Char firstChar;  // 0x24

    }

    // TypeToken: 0x20000E5  // size: 0x20
    public sealed struct MonoString_32
    {
        // Fields
        public Unity.Profiling.MonoObject_32 obj;  // 0x10
        public System.Int32 length;  // 0x18
        public System.Char firstChar;  // 0x1c

    }

    // TypeToken: 0x20000E6  // size: 0x30
    public class ProfilerString : System.IDisposable
    {
        // Fields
        private static System.Int32 MAX;  // const
        private System.String m_buffer;  // 0x10
        private System.Int32 m_length;  // 0x18
        private System.Int32* m_strCount;  // 0x20
        private System.UInt64 m_handler;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x6000809
        public System.Void .ctor(System.Int32 count) { }
        // RVA: 0x041E1670  token: 0x600080A
        public virtual System.Void Dispose() { }
        // RVA: 0x02B2ECC0  token: 0x600080B
        public virtual System.String ToString() { }
        // RVA: 0x0B5834BC  token: 0x600080C
        public static System.String op_Implicit(Unity.Profiling.ProfilerString handle) { }
        // RVA: 0x0B5833D4  token: 0x600080D
        public System.Void Append(System.String value) { }

    }

    // TypeToken: 0x20000E7  // size: 0x11
    public sealed struct ProfilerUtility
    {
        // Methods
        // RVA: -1  // generic def  token: 0x600080E
        public static System.Byte GetProfilerMarkerDataType() { }

    }

}

namespace UnityEngine.EventSystems
{

    // TypeToken: 0x20000A1  // size: 0x30
    public class AxisEventData : UnityEngine.EventSystems.BaseEventData
    {
        // Fields
        private UnityEngine.Vector2 <moveVector>k__BackingField;  // 0x20
        private UnityEngine.EventSystems.MoveDirection <moveDir>k__BackingField;  // 0x28

        // Properties
        UnityEngine.Vector2 moveVector { get; /* RVA: 0x04DA5EA0 */ set; /* RVA: 0x04D86500 */ }
        UnityEngine.EventSystems.MoveDirection moveDir { get; /* RVA: 0x011F2A90 */ set; /* RVA: 0x011F2AA0 */ }

        // Methods
        // RVA: 0x0B57B33C  token: 0x6000653
        public System.Void .ctor(UnityEngine.EventSystems.EventSystem eventSystem) { }

    }

    // TypeToken: 0x20000A2  // size: 0x18
    public abstract class AbstractEventData
    {
        // Fields
        protected System.Boolean m_Used;  // 0x10

        // Properties
        System.Boolean used { get; /* RVA: 0x015EFCE0 */ }

        // Methods
        // RVA: 0x04D8CCB0  token: 0x6000654
        public virtual System.Void Reset() { }
        // RVA: 0x015EFCD0  token: 0x6000655
        public virtual System.Void Use() { }
        // RVA: 0x041E1670  token: 0x6000657
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000A3  // size: 0x20
    public class BaseEventData : UnityEngine.EventSystems.AbstractEventData
    {
        // Fields
        private readonly UnityEngine.EventSystems.EventSystem m_EventSystem;  // 0x18

        // Properties
        UnityEngine.EventSystems.BaseInputModule currentInputModule { get; /* RVA: 0x0A8DA4B4 */ }
        UnityEngine.GameObject selectedObject { get; /* RVA: 0x0A8DA594 */ set; /* RVA: 0x0B57B364 */ }

        // Methods
        // RVA: 0x05392C40  token: 0x6000658
        public System.Void .ctor(UnityEngine.EventSystems.EventSystem eventSystem) { }

    }

    // TypeToken: 0x20000A4  // size: 0x160
    public class PointerEventData : UnityEngine.EventSystems.BaseEventData
    {
        // Fields
        private UnityEngine.GameObject <pointerEnter>k__BackingField;  // 0x20
        private UnityEngine.GameObject m_PointerPress;  // 0x28
        private UnityEngine.GameObject <lastPress>k__BackingField;  // 0x30
        private UnityEngine.GameObject <rawPointerPress>k__BackingField;  // 0x38
        private UnityEngine.GameObject <pointerDrag>k__BackingField;  // 0x40
        private UnityEngine.GameObject <pointerClick>k__BackingField;  // 0x48
        private UnityEngine.EventSystems.RaycastResult <pointerCurrentRaycast>k__BackingField;  // 0x50
        private UnityEngine.EventSystems.RaycastResult <pointerPressRaycast>k__BackingField;  // 0x98
        public System.Collections.Generic.List<UnityEngine.GameObject> hovered;  // 0xe0
        private System.Boolean <eligibleForClick>k__BackingField;  // 0xe8
        private System.Int32 <pointerId>k__BackingField;  // 0xec
        private UnityEngine.Vector2 <position>k__BackingField;  // 0xf0
        private UnityEngine.Vector2 <delta>k__BackingField;  // 0xf8
        private UnityEngine.Vector2 <pressPosition>k__BackingField;  // 0x100
        private UnityEngine.Vector3 <worldPosition>k__BackingField;  // 0x108
        private UnityEngine.Vector3 <worldNormal>k__BackingField;  // 0x114
        private System.Single <clickTime>k__BackingField;  // 0x120
        private System.Int32 <clickCount>k__BackingField;  // 0x124
        private UnityEngine.Vector2 <scrollDelta>k__BackingField;  // 0x128
        private System.Boolean <useDragThreshold>k__BackingField;  // 0x130
        private System.Boolean <dragging>k__BackingField;  // 0x131
        private UnityEngine.EventSystems.PointerEventData.InputButton <button>k__BackingField;  // 0x134
        private System.Single <pressure>k__BackingField;  // 0x138
        private System.Single <tangentialPressure>k__BackingField;  // 0x13c
        private System.Single <altitudeAngle>k__BackingField;  // 0x140
        private System.Single <azimuthAngle>k__BackingField;  // 0x144
        private System.Single <twist>k__BackingField;  // 0x148
        private UnityEngine.Vector2 <radius>k__BackingField;  // 0x14c
        private UnityEngine.Vector2 <radiusVariance>k__BackingField;  // 0x154
        private System.Boolean <fullyExited>k__BackingField;  // 0x15c
        private System.Boolean <reentered>k__BackingField;  // 0x15d

        // Properties
        UnityEngine.GameObject pointerEnter { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        UnityEngine.GameObject lastPress { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }
        UnityEngine.GameObject rawPointerPress { get; /* RVA: 0x04D85A50 */ set; /* RVA: 0x046A91B0 */ }
        UnityEngine.GameObject pointerDrag { get; /* RVA: 0x04D85A60 */ set; /* RVA: 0x042B4AE0 */ }
        UnityEngine.GameObject pointerClick { get; /* RVA: 0x04D86200 */ set; /* RVA: 0x035A41A0 */ }
        UnityEngine.EventSystems.RaycastResult pointerCurrentRaycast { get; /* RVA: 0x04DBF160 */ set; /* RVA: 0x0B5811E4 */ }
        UnityEngine.EventSystems.RaycastResult pointerPressRaycast { get; /* RVA: 0x04DBF1A0 */ set; /* RVA: 0x0B58121C */ }
        System.Boolean eligibleForClick { get; /* RVA: 0x04D8C8C0 */ set; /* RVA: 0x04D8C8F0 */ }
        System.Int32 pointerId { get; /* RVA: 0x04D9DF40 */ set; /* RVA: 0x04D9E020 */ }
        UnityEngine.Vector2 position { get; /* RVA: 0x04D8E090 */ set; /* RVA: 0x04D8E0B0 */ }
        UnityEngine.Vector2 delta { get; /* RVA: 0x04DBF130 */ set; /* RVA: 0x04DBF270 */ }
        UnityEngine.Vector2 pressPosition { get; /* RVA: 0x04DBF1E0 */ set; /* RVA: 0x04DBF2A0 */ }
        UnityEngine.Vector3 worldPosition { get; /* RVA: 0x04D91A30 */ set; /* RVA: 0x04D91A70 */ }
        UnityEngine.Vector3 worldNormal { get; /* RVA: 0x04D90F70 */ set; /* RVA: 0x04D90FF0 */ }
        System.Single clickTime { get; /* RVA: 0x04DBD7E0 */ set; /* RVA: 0x04DBD820 */ }
        System.Int32 clickCount { get; /* RVA: 0x04D90A80 */ set; /* RVA: 0x04D90AB0 */ }
        UnityEngine.Vector2 scrollDelta { get; /* RVA: 0x04DBF240 */ set; /* RVA: 0x04DA14B0 */ }
        System.Boolean useDragThreshold { get; /* RVA: 0x04D92620 */ set; /* RVA: 0x04D92640 */ }
        System.Boolean dragging { get; /* RVA: 0x04DBF150 */ set; /* RVA: 0x04DBF280 */ }
        UnityEngine.EventSystems.PointerEventData.InputButton button { get; /* RVA: 0x04DA3240 */ set; /* RVA: 0x04DBDA30 */ }
        System.Single pressure { get; /* RVA: 0x04DA3250 */ set; /* RVA: 0x04DBF2B0 */ }
        System.Single tangentialPressure { get; /* RVA: 0x04DA7940 */ set; /* RVA: 0x04DBC020 */ }
        System.Single altitudeAngle { get; /* RVA: 0x04D8E180 */ set; /* RVA: 0x04D8E1D0 */ }
        System.Single azimuthAngle { get; /* RVA: 0x04DBF120 */ set; /* RVA: 0x04DBF260 */ }
        System.Single twist { get; /* RVA: 0x04D90E70 */ set; /* RVA: 0x04D90E80 */ }
        UnityEngine.Vector2 radius { get; /* RVA: 0x04DBF220 */ set; /* RVA: 0x04DBF2D0 */ }
        UnityEngine.Vector2 radiusVariance { get; /* RVA: 0x04DBF200 */ set; /* RVA: 0x04DBF2C0 */ }
        System.Boolean fullyExited { get; /* RVA: 0x04DA7930 */ set; /* RVA: 0x04DBF290 */ }
        System.Boolean reentered { get; /* RVA: 0x04DA7C60 */ set; /* RVA: 0x04DBF2E0 */ }
        UnityEngine.Camera enterEventCamera { get; /* RVA: 0x0B5810D8 */ }
        UnityEngine.Camera pressEventCamera { get; /* RVA: 0x0B58115C */ }
        UnityEngine.GameObject pointerPress { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x033F30B0 */ }

        // Methods
        // RVA: 0x03E9B9B0  token: 0x6000696
        public System.Void .ctor(UnityEngine.EventSystems.EventSystem eventSystem) { }
        // RVA: 0x033F2A80  token: 0x6000697
        public System.Boolean IsPointerMoving() { }
        // RVA: 0x0B580B40  token: 0x6000698
        public System.Boolean IsScrolling() { }
        // RVA: 0x0B580B68  token: 0x600069D
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x20000A7  // size: 0x14
    public sealed struct EventHandle
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.EventSystems.EventHandle Unused;  // const
        public static UnityEngine.EventSystems.EventHandle Used;  // const

    }

    // TypeToken: 0x20000A8
    public interface IEventSystemHandler
    {
    }

    // TypeToken: 0x20000A9
    public interface IPointerMoveHandler : UnityEngine.EventSystems.IEventSystemHandler
    {
        // Methods
        // RVA: -1  // abstract  token: 0x600069E
        public virtual System.Void OnPointerMove(UnityEngine.EventSystems.PointerEventData eventData) { }

    }

    // TypeToken: 0x20000AA
    public interface IPointerEnterHandler : UnityEngine.EventSystems.IEventSystemHandler
    {
        // Methods
        // RVA: -1  // abstract  token: 0x600069F
        public virtual System.Void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData) { }

    }

    // TypeToken: 0x20000AB
    public interface IPointerExitHandler : UnityEngine.EventSystems.IEventSystemHandler
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60006A0
        public virtual System.Void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData) { }

    }

    // TypeToken: 0x20000AC
    public interface IPointerDownHandler : UnityEngine.EventSystems.IEventSystemHandler
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60006A1
        public virtual System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData) { }

    }

    // TypeToken: 0x20000AD
    public interface IPointerUpHandler : UnityEngine.EventSystems.IEventSystemHandler
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60006A2
        public virtual System.Void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData) { }

    }

    // TypeToken: 0x20000AE
    public interface IPointerClickHandler : UnityEngine.EventSystems.IEventSystemHandler
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60006A3
        public virtual System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData) { }

    }

    // TypeToken: 0x20000AF
    public interface IBeginDragHandler : UnityEngine.EventSystems.IEventSystemHandler
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60006A4
        public virtual System.Void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData) { }

    }

    // TypeToken: 0x20000B0
    public interface IInitializePotentialDragHandler : UnityEngine.EventSystems.IEventSystemHandler
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60006A5
        public virtual System.Void OnInitializePotentialDrag(UnityEngine.EventSystems.PointerEventData eventData) { }

    }

    // TypeToken: 0x20000B1
    public interface IDragHandler : UnityEngine.EventSystems.IEventSystemHandler
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60006A6
        public virtual System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData) { }

    }

    // TypeToken: 0x20000B2
    public interface IEndDragHandler : UnityEngine.EventSystems.IEventSystemHandler
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60006A7
        public virtual System.Void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData) { }

    }

    // TypeToken: 0x20000B3
    public interface IDropHandler : UnityEngine.EventSystems.IEventSystemHandler
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60006A8
        public virtual System.Void OnDrop(UnityEngine.EventSystems.PointerEventData eventData) { }

    }

    // TypeToken: 0x20000B4
    public interface IScrollHandler : UnityEngine.EventSystems.IEventSystemHandler
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60006A9
        public virtual System.Void OnScroll(UnityEngine.EventSystems.PointerEventData eventData) { }

    }

    // TypeToken: 0x20000B5
    public interface IUpdateSelectedHandler : UnityEngine.EventSystems.IEventSystemHandler
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60006AA
        public virtual System.Void OnUpdateSelected(UnityEngine.EventSystems.BaseEventData eventData) { }

    }

    // TypeToken: 0x20000B6
    public interface ISelectHandler : UnityEngine.EventSystems.IEventSystemHandler
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60006AB
        public virtual System.Void OnSelect(UnityEngine.EventSystems.BaseEventData eventData) { }

    }

    // TypeToken: 0x20000B7
    public interface IDeselectHandler : UnityEngine.EventSystems.IEventSystemHandler
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60006AC
        public virtual System.Void OnDeselect(UnityEngine.EventSystems.BaseEventData eventData) { }

    }

    // TypeToken: 0x20000B8
    public interface IMoveHandler : UnityEngine.EventSystems.IEventSystemHandler
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60006AD
        public virtual System.Void OnMove(UnityEngine.EventSystems.AxisEventData eventData) { }

    }

    // TypeToken: 0x20000B9
    public interface ISubmitHandler : UnityEngine.EventSystems.IEventSystemHandler
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60006AE
        public virtual System.Void OnSubmit(UnityEngine.EventSystems.BaseEventData eventData) { }

    }

    // TypeToken: 0x20000BA
    public interface ICancelHandler : UnityEngine.EventSystems.IEventSystemHandler
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60006AF
        public virtual System.Void OnCancel(UnityEngine.EventSystems.BaseEventData eventData) { }

    }

    // TypeToken: 0x20000BB  // size: 0x50
    public class EventSystem : UnityEngine.EventSystems.UIBehaviour
    {
        // Fields
        private System.Collections.Generic.List<UnityEngine.EventSystems.BaseInputModule> m_SystemInputModules;  // 0x18
        private UnityEngine.EventSystems.BaseInputModule m_CurrentInputModule;  // 0x20
        private static System.Collections.Generic.List<UnityEngine.EventSystems.EventSystem> m_EventSystems;  // static @ 0x0
        private UnityEngine.GameObject m_FirstSelected;  // 0x28
        private System.Boolean m_sendNavigationEvents;  // 0x30
        private System.Int32 m_DragThreshold;  // 0x34
        private UnityEngine.GameObject m_CurrentSelected;  // 0x38
        private System.Boolean m_HasFocus;  // 0x40
        private System.Boolean m_SelectionGuard;  // 0x41
        private UnityEngine.EventSystems.BaseEventData m_DummyData;  // 0x48
        private static readonly System.Comparison<UnityEngine.EventSystems.RaycastResult> s_RaycastComparer;  // static @ 0x8
        private static UnityEngine.EventSystems.EventSystem.UIToolkitOverrideConfig s_UIToolkitOverride;  // static @ 0x10

        // Properties
        UnityEngine.EventSystems.EventSystem current { get; /* RVA: 0x03795B00 */ set; /* RVA: 0x0B57C964 */ }
        System.Boolean sendNavigationEvents { get; /* RVA: 0x04D865F0 */ set; /* RVA: 0x04D86620 */ }
        System.Int32 pixelDragThreshold { get; /* RVA: 0x04D86730 */ set; /* RVA: 0x04D86760 */ }
        UnityEngine.EventSystems.BaseInputModule currentInputModule { get; /* RVA: 0x04D862C0 */ }
        UnityEngine.GameObject firstSelectedGameObject { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }
        UnityEngine.GameObject currentSelectedGameObject { get; /* RVA: 0x04D85A50 */ }
        UnityEngine.GameObject lastSelectedGameObject { get; /* RVA: 0x011EC580 */ }
        System.Boolean isFocused { get; /* RVA: 0x02D480A0 */ }
        System.Boolean alreadySelecting { get; /* RVA: 0x04D86F40 */ }
        UnityEngine.EventSystems.BaseEventData baseEventDataCache { get; /* RVA: 0x0B57C8F0 */ }
        System.Boolean isUIToolkitActiveEventSystem { get; /* RVA: 0x04648650 */ }
        System.Boolean sendUIToolkitEvents { get; /* RVA: 0x04647B00 */ }
        System.Boolean createUIToolkitPanelGameObjectsOnStart { get; /* RVA: 0x04648500 */ }

        // Methods
        // RVA: 0x04D12950  token: 0x60006BC
        protected System.Void .ctor() { }
        // RVA: 0x03DFA5D0  token: 0x60006BD
        public System.Void UpdateModules() { }
        // RVA: 0x03E9A200  token: 0x60006BF
        public System.Void SetSelectedGameObject(UnityEngine.GameObject selected, UnityEngine.EventSystems.BaseEventData pointer) { }
        // RVA: 0x0B57C648  token: 0x60006C1
        public System.Void SetSelectedGameObject(UnityEngine.GameObject selected) { }
        // RVA: 0x0389F8B0  token: 0x60006C2
        private static System.Int32 RaycastComparer(UnityEngine.EventSystems.RaycastResult lhs, UnityEngine.EventSystems.RaycastResult rhs) { }
        // RVA: 0x02FC7540  token: 0x60006C3
        public System.Void RaycastAll(UnityEngine.EventSystems.PointerEventData eventData, System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult> raycastResults) { }
        // RVA: 0x0B57C4C0  token: 0x60006C4
        public System.Boolean IsPointerOverGameObject() { }
        // RVA: 0x0B57C440  token: 0x60006C5
        public System.Boolean IsPointerOverGameObject(System.Int32 pointerId) { }
        // RVA: 0x0B57C67C  token: 0x60006C9
        public static System.Void SetUITookitEventSystemOverride(UnityEngine.EventSystems.EventSystem activeEventSystem, System.Boolean sendEvents, System.Boolean createPanelGameObjectsOnStart) { }
        // RVA: 0x0B57C200  token: 0x60006CA
        private System.Void CreateUIToolkitPanelGameObject(UnityEngine.UIElements.BaseRuntimePanel panel) { }
        // RVA: 0x04647B70  token: 0x60006CB
        protected virtual System.Void Start() { }
        // RVA: 0x0B57C4D0  token: 0x60006CC
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x04647A40  token: 0x60006CD
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0B57C560  token: 0x60006CE
        protected virtual System.Void OnDisable() { }
        // RVA: 0x03795770  token: 0x60006CF
        private System.Void TickModules() { }
        // RVA: 0x03F26150  token: 0x60006D0
        protected virtual System.Void OnApplicationFocus(System.Boolean hasFocus) { }
        // RVA: 0x037952F0  token: 0x60006D1
        protected virtual System.Void Update() { }
        // RVA: 0x04CB05B0  token: 0x60006D2
        private System.Void ChangeEventModule(UnityEngine.EventSystems.BaseInputModule module) { }
        // RVA: 0x0B57C7C0  token: 0x60006D3
        public virtual System.String ToString() { }
        // RVA: 0x04B20F90  token: 0x60006D4
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000BF  // size: 0x20
    public class EventTrigger : UnityEngine.MonoBehaviour, UnityEngine.EventSystems.IPointerEnterHandler, UnityEngine.EventSystems.IEventSystemHandler, UnityEngine.EventSystems.IPointerExitHandler, UnityEngine.EventSystems.IPointerDownHandler, UnityEngine.EventSystems.IPointerUpHandler, UnityEngine.EventSystems.IPointerClickHandler, UnityEngine.EventSystems.IInitializePotentialDragHandler, UnityEngine.EventSystems.IBeginDragHandler, UnityEngine.EventSystems.IDragHandler, UnityEngine.EventSystems.IEndDragHandler, UnityEngine.EventSystems.IDropHandler, UnityEngine.EventSystems.IScrollHandler, UnityEngine.EventSystems.IUpdateSelectedHandler, UnityEngine.EventSystems.ISelectHandler, UnityEngine.EventSystems.IDeselectHandler, UnityEngine.EventSystems.IMoveHandler, UnityEngine.EventSystems.ISubmitHandler, UnityEngine.EventSystems.ICancelHandler
    {
        // Fields
        private System.Collections.Generic.List<UnityEngine.EventSystems.EventTrigger.Entry> m_Delegates;  // 0x18

        // Properties
        System.Collections.Generic.List<UnityEngine.EventSystems.EventTrigger.Entry> delegates { get; /* RVA: 0x0B57CCB8 */ set; /* RVA: 0x05392C40 */ }
        System.Collections.Generic.List<UnityEngine.EventSystems.EventTrigger.Entry> triggers { get; /* RVA: 0x0B57CCC0 */ set; /* RVA: 0x05392C40 */ }

        // Methods
        // RVA: 0x05393520  token: 0x60006DA
        protected System.Void .ctor() { }
        // RVA: 0x0B57CAC0  token: 0x60006DD
        private System.Void Execute(UnityEngine.EventSystems.EventTriggerType id, UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0B57CC48  token: 0x60006DE
        public virtual System.Void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B57CC58  token: 0x60006DF
        public virtual System.Void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B57CBD8  token: 0x60006E0
        public virtual System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B57CBE8  token: 0x60006E1
        public virtual System.Void OnDrop(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B57CC38  token: 0x60006E2
        public virtual System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B57CC68  token: 0x60006E3
        public virtual System.Void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B57CC28  token: 0x60006E4
        public virtual System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B57CC88  token: 0x60006E5
        public virtual System.Void OnSelect(UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0B57CBC8  token: 0x60006E6
        public virtual System.Void OnDeselect(UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0B57CC78  token: 0x60006E7
        public virtual System.Void OnScroll(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B57CC18  token: 0x60006E8
        public virtual System.Void OnMove(UnityEngine.EventSystems.AxisEventData eventData) { }
        // RVA: 0x0B57CCA8  token: 0x60006E9
        public virtual System.Void OnUpdateSelected(UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0B57CC08  token: 0x60006EA
        public virtual System.Void OnInitializePotentialDrag(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B57CBA8  token: 0x60006EB
        public virtual System.Void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B57CBF8  token: 0x60006EC
        public virtual System.Void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B57CC98  token: 0x60006ED
        public virtual System.Void OnSubmit(UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0B57CBB8  token: 0x60006EE
        public virtual System.Void OnCancel(UnityEngine.EventSystems.BaseEventData eventData) { }

    }

    // TypeToken: 0x20000C2  // size: 0x14
    public sealed struct EventTriggerType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.EventSystems.EventTriggerType PointerEnter;  // const
        public static UnityEngine.EventSystems.EventTriggerType PointerExit;  // const
        public static UnityEngine.EventSystems.EventTriggerType PointerDown;  // const
        public static UnityEngine.EventSystems.EventTriggerType PointerUp;  // const
        public static UnityEngine.EventSystems.EventTriggerType PointerClick;  // const
        public static UnityEngine.EventSystems.EventTriggerType Drag;  // const
        public static UnityEngine.EventSystems.EventTriggerType Drop;  // const
        public static UnityEngine.EventSystems.EventTriggerType Scroll;  // const
        public static UnityEngine.EventSystems.EventTriggerType UpdateSelected;  // const
        public static UnityEngine.EventSystems.EventTriggerType Select;  // const
        public static UnityEngine.EventSystems.EventTriggerType Deselect;  // const
        public static UnityEngine.EventSystems.EventTriggerType Move;  // const
        public static UnityEngine.EventSystems.EventTriggerType InitializePotentialDrag;  // const
        public static UnityEngine.EventSystems.EventTriggerType BeginDrag;  // const
        public static UnityEngine.EventSystems.EventTriggerType EndDrag;  // const
        public static UnityEngine.EventSystems.EventTriggerType Submit;  // const
        public static UnityEngine.EventSystems.EventTriggerType Cancel;  // const

    }

    // TypeToken: 0x20000C3  // size: 0x10
    public static class ExecuteEvents
    {
        // Fields
        private static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IPointerMoveHandler> s_PointerMoveHandler;  // static @ 0x0
        private static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IPointerEnterHandler> s_PointerEnterHandler;  // static @ 0x8
        private static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IPointerExitHandler> s_PointerExitHandler;  // static @ 0x10
        private static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IPointerDownHandler> s_PointerDownHandler;  // static @ 0x18
        private static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IPointerUpHandler> s_PointerUpHandler;  // static @ 0x20
        private static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IPointerClickHandler> s_PointerClickHandler;  // static @ 0x28
        private static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IInitializePotentialDragHandler> s_InitializePotentialDragHandler;  // static @ 0x30
        private static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IBeginDragHandler> s_BeginDragHandler;  // static @ 0x38
        private static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IDragHandler> s_DragHandler;  // static @ 0x40
        private static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IEndDragHandler> s_EndDragHandler;  // static @ 0x48
        private static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IDropHandler> s_DropHandler;  // static @ 0x50
        private static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IScrollHandler> s_ScrollHandler;  // static @ 0x58
        private static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IUpdateSelectedHandler> s_UpdateSelectedHandler;  // static @ 0x60
        private static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.ISelectHandler> s_SelectHandler;  // static @ 0x68
        private static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IDeselectHandler> s_DeselectHandler;  // static @ 0x70
        private static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IMoveHandler> s_MoveHandler;  // static @ 0x78
        private static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.ISubmitHandler> s_SubmitHandler;  // static @ 0x80
        private static readonly UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.ICancelHandler> s_CancelHandler;  // static @ 0x88
        private static readonly System.Collections.Generic.List<UnityEngine.Transform> s_InternalTransformList;  // static @ 0x90

        // Properties
        UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IPointerMoveHandler> pointerMoveHandler { get; /* RVA: 0x0B57D3E4 */ }
        UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IPointerEnterHandler> pointerEnterHandler { get; /* RVA: 0x0B57D37C */ }
        UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IPointerExitHandler> pointerExitHandler { get; /* RVA: 0x0B57D3B0 */ }
        UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IPointerDownHandler> pointerDownHandler { get; /* RVA: 0x0B57D348 */ }
        UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IPointerUpHandler> pointerUpHandler { get; /* RVA: 0x0B57D418 */ }
        UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IPointerClickHandler> pointerClickHandler { get; /* RVA: 0x0B57D314 */ }
        UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IInitializePotentialDragHandler> initializePotentialDrag { get; /* RVA: 0x0B57D274 */ }
        UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IBeginDragHandler> beginDragHandler { get; /* RVA: 0x0B57D0AC */ }
        UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IDragHandler> dragHandler { get; /* RVA: 0x0B57D1A0 */ }
        UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IEndDragHandler> endDragHandler { get; /* RVA: 0x0B57D224 */ }
        UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IDropHandler> dropHandler { get; /* RVA: 0x0B57D1D4 */ }
        UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IScrollHandler> scrollHandler { get; /* RVA: 0x0B57D44C */ }
        UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IUpdateSelectedHandler> updateSelectedHandler { get; /* RVA: 0x0B57D540 */ }
        UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.ISelectHandler> selectHandler { get; /* RVA: 0x0B57D49C */ }
        UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IDeselectHandler> deselectHandler { get; /* RVA: 0x0B57D150 */ }
        UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.IMoveHandler> moveHandler { get; /* RVA: 0x0B57D2C4 */ }
        UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.ISubmitHandler> submitHandler { get; /* RVA: 0x0B57D4EC */ }
        UnityEngine.EventSystems.ExecuteEvents.EventFunction<UnityEngine.EventSystems.ICancelHandler> cancelHandler { get; /* RVA: 0x0B57D0FC */ }

        // Methods
        // RVA: -1  // generic def  token: 0x60006F1
        public static T ValidateEventData(UnityEngine.EventSystems.BaseEventData data) { }
        // RVA: 0x0B57CEA4  token: 0x60006F2
        private static System.Void Execute(UnityEngine.EventSystems.IPointerMoveHandler handler, UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0379AE30  token: 0x60006F3
        private static System.Void Execute(UnityEngine.EventSystems.IPointerEnterHandler handler, UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0379ADD0  token: 0x60006F4
        private static System.Void Execute(UnityEngine.EventSystems.IPointerExitHandler handler, UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0379AD10  token: 0x60006F5
        private static System.Void Execute(UnityEngine.EventSystems.IPointerDownHandler handler, UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0379AD70  token: 0x60006F6
        private static System.Void Execute(UnityEngine.EventSystems.IPointerUpHandler handler, UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0379ACB0  token: 0x60006F7
        private static System.Void Execute(UnityEngine.EventSystems.IPointerClickHandler handler, UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x04A26280  token: 0x60006F8
        private static System.Void Execute(UnityEngine.EventSystems.IInitializePotentialDragHandler handler, UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x037D64A0  token: 0x60006F9
        private static System.Void Execute(UnityEngine.EventSystems.IBeginDragHandler handler, UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x037D6750  token: 0x60006FA
        private static System.Void Execute(UnityEngine.EventSystems.IDragHandler handler, UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x037D6540  token: 0x60006FB
        private static System.Void Execute(UnityEngine.EventSystems.IEndDragHandler handler, UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0B57CF58  token: 0x60006FC
        private static System.Void Execute(UnityEngine.EventSystems.IDropHandler handler, UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0B57CD44  token: 0x60006FD
        private static System.Void Execute(UnityEngine.EventSystems.IScrollHandler handler, UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x03798760  token: 0x60006FE
        private static System.Void Execute(UnityEngine.EventSystems.IUpdateSelectedHandler handler, UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x04CA2A10  token: 0x60006FF
        private static System.Void Execute(UnityEngine.EventSystems.ISelectHandler handler, UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x04B6C870  token: 0x6000700
        private static System.Void Execute(UnityEngine.EventSystems.IDeselectHandler handler, UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0B57D038  token: 0x6000701
        private static System.Void Execute(UnityEngine.EventSystems.IMoveHandler handler, UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0B57CF40  token: 0x6000702
        private static System.Void Execute(UnityEngine.EventSystems.ISubmitHandler handler, UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0B57CDB8  token: 0x6000703
        private static System.Void Execute(UnityEngine.EventSystems.ICancelHandler handler, UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x041AEF30  token: 0x6000716
        private static System.Void GetEventChain(UnityEngine.GameObject root, System.Collections.Generic.IList<UnityEngine.Transform> eventChain) { }
        // RVA: -1  // generic def  token: 0x6000717
        public static System.Boolean Execute(UnityEngine.GameObject target, UnityEngine.EventSystems.BaseEventData eventData, UnityEngine.EventSystems.ExecuteEvents.EventFunction<T> functor) { }
        // RVA: -1  // generic def  token: 0x6000718
        public static UnityEngine.GameObject ExecuteHierarchy(UnityEngine.GameObject root, UnityEngine.EventSystems.BaseEventData eventData, UnityEngine.EventSystems.ExecuteEvents.EventFunction<T> callbackFunction) { }
        // RVA: -1  // generic def  token: 0x6000719
        private static System.Boolean ShouldSendToComponent(UnityEngine.Component component) { }
        // RVA: -1  // generic def  token: 0x600071A
        private static System.Void GetEventList(UnityEngine.GameObject go, System.Collections.Generic.IList<UnityEngine.EventSystems.IEventSystemHandler> results) { }
        // RVA: -1  // generic def  token: 0x600071B
        public static System.Boolean CanHandleEvent(UnityEngine.GameObject go) { }
        // RVA: -1  // generic def  token: 0x600071C
        public static UnityEngine.GameObject GetEventHandler(UnityEngine.GameObject root) { }
        // RVA: 0x0468AC20  token: 0x600071D
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000C5  // size: 0x18
    public class BaseInput : UnityEngine.EventSystems.UIBehaviour
    {
        // Properties
        System.String compositionString { get; /* RVA: 0x04118260 */ }
        UnityEngine.IMECompositionMode imeCompositionMode { get; /* RVA: 0x0B57BC94 */ set; /* RVA: 0x0539555C */ }
        UnityEngine.Vector2 compositionCursorPos { get; /* RVA: 0x0B57BC8C */ set; /* RVA: 0x0B57BCD4 */ }
        System.Boolean mousePresent { get; /* RVA: 0x0B47AE10 */ }
        UnityEngine.Vector2 mousePosition { get; /* RVA: 0x0B57BC9C */ }
        UnityEngine.Vector2 mouseScrollDelta { get; /* RVA: 0x0B57BCC4 */ }
        System.Boolean touchSupported { get; /* RVA: 0x0B57BCCC */ }
        System.Int32 touchCount { get; /* RVA: 0x09F48384 */ }

        // Methods
        // RVA: 0x0B57BC68  token: 0x6000728
        public virtual System.Boolean GetMouseButtonDown(System.Int32 button) { }
        // RVA: 0x0B57BC74  token: 0x6000729
        public virtual System.Boolean GetMouseButtonUp(System.Int32 button) { }
        // RVA: 0x0B57BC80  token: 0x600072A
        public virtual System.Boolean GetMouseButton(System.Int32 button) { }
        // RVA: 0x0AA1FDE0  token: 0x600072F
        public virtual UnityEngine.Touch GetTouch(System.Int32 index) { }
        // RVA: 0x0B57BC5C  token: 0x6000730
        public virtual System.Single GetAxisRaw(System.String axisName) { }
        // RVA: 0x0B47AE04  token: 0x6000731
        public virtual System.Boolean GetButtonDown(System.String buttonName) { }
        // RVA: 0x05393520  token: 0x6000732
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C6  // size: 0x48
    public abstract class BaseInputModule : UnityEngine.EventSystems.UIBehaviour
    {
        // Fields
        protected System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult> m_RaycastResultCache;  // 0x18
        private UnityEngine.EventSystems.AxisEventData m_AxisEventData;  // 0x20
        private UnityEngine.EventSystems.EventSystem m_EventSystem;  // 0x28
        private UnityEngine.EventSystems.BaseEventData m_BaseEventData;  // 0x30
        protected UnityEngine.EventSystems.BaseInput m_InputOverride;  // 0x38
        private UnityEngine.EventSystems.BaseInput m_DefaultInput;  // 0x40

        // Properties
        UnityEngine.EventSystems.BaseInput input { get; /* RVA: 0x03795BA0 */ }
        UnityEngine.EventSystems.BaseInput inputOverride { get; /* RVA: 0x04D85A50 */ set; /* RVA: 0x046A91B0 */ }
        UnityEngine.EventSystems.EventSystem eventSystem { get; /* RVA: 0x04D86240 */ }

        // Methods
        // RVA: 0x03DFA850  token: 0x6000737
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0B57BC3C  token: 0x6000738
        protected virtual System.Void OnDisable() { }
        // RVA: -1  // abstract  token: 0x6000739
        public virtual System.Void Process() { }
        // RVA: 0x033F34A0  token: 0x600073A
        protected static UnityEngine.EventSystems.RaycastResult FindFirstRaycast(System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult> candidates) { }
        // RVA: 0x0B57B478  token: 0x600073B
        protected static UnityEngine.EventSystems.MoveDirection DetermineMoveDirection(System.Single x, System.Single y) { }
        // RVA: 0x0B57B408  token: 0x600073C
        protected static UnityEngine.EventSystems.MoveDirection DetermineMoveDirection(System.Single x, System.Single y, System.Single deadZone) { }
        // RVA: 0x03F32D60  token: 0x600073D
        protected static UnityEngine.GameObject FindCommonRoot(UnityEngine.GameObject g1, UnityEngine.GameObject g2) { }
        // RVA: 0x0B57B570  token: 0x600073E
        protected System.Void HandlePointerExitAndEnter(UnityEngine.EventSystems.PointerEventData currentPointerData, UnityEngine.GameObject newEnterTarget) { }
        // RVA: 0x0B57B488  token: 0x600073F
        protected virtual UnityEngine.EventSystems.AxisEventData GetAxisEventData(System.Single x, System.Single y, System.Single moveDeadZone) { }
        // RVA: 0x03E9A450  token: 0x6000740
        protected virtual UnityEngine.EventSystems.BaseEventData GetBaseEventData() { }
        // RVA: 0x012081B0  token: 0x6000741
        public virtual System.Boolean IsPointerOverGameObject(System.Int32 pointerId) { }
        // RVA: 0x03E148A0  token: 0x6000742
        public virtual System.Boolean ShouldActivateModule() { }
        // RVA: 0x041E1670  token: 0x6000743
        public virtual System.Void DeactivateModule() { }
        // RVA: 0x041E1670  token: 0x6000744
        public virtual System.Void ActivateModule() { }
        // RVA: 0x041E1670  token: 0x6000745
        public virtual System.Void UpdateModule() { }
        // RVA: 0x02FFF600  token: 0x6000746
        public virtual System.Boolean IsModuleSupported() { }
        // RVA: 0x0B57B38C  token: 0x6000747
        public virtual System.Int32 ConvertUIToolkitPointerId(UnityEngine.EventSystems.PointerEventData sourcePointerData) { }
        // RVA: 0x0445D4D0  token: 0x6000748
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000C7  // size: 0x58
    public abstract class PointerInputModule : UnityEngine.EventSystems.BaseInputModule
    {
        // Fields
        public static System.Int32 kMouseLeftId;  // const
        public static System.Int32 kMouseRightId;  // const
        public static System.Int32 kMouseMiddleId;  // const
        public static System.Int32 kFakeTouchesId;  // const
        protected System.Collections.Generic.Dictionary<System.Int32,UnityEngine.EventSystems.PointerEventData> m_PointerData;  // 0x48
        private readonly UnityEngine.EventSystems.PointerInputModule.MouseState m_MouseState;  // 0x50

        // Methods
        // RVA: 0x0B581FF4  token: 0x6000749
        protected System.Boolean GetPointerData(System.Int32 id, UnityEngine.EventSystems.PointerEventData& data, System.Boolean create) { }
        // RVA: 0x0B582710  token: 0x600074A
        protected System.Void RemovePointerData(UnityEngine.EventSystems.PointerEventData data) { }
        // RVA: 0x0B5820E8  token: 0x600074B
        protected UnityEngine.EventSystems.PointerEventData GetTouchPointerEventData(UnityEngine.Touch input, System.Boolean& pressed, System.Boolean& released) { }
        // RVA: 0x0B58198C  token: 0x600074C
        protected System.Void CopyFromTo(UnityEngine.EventSystems.PointerEventData from, UnityEngine.EventSystems.PointerEventData to) { }
        // RVA: 0x0B58276C  token: 0x600074D
        protected UnityEngine.EventSystems.PointerEventData.FramePressState StateForMouseButton(System.Int32 buttonId) { }
        // RVA: 0x0B581C00  token: 0x600074E
        protected virtual UnityEngine.EventSystems.PointerInputModule.MouseState GetMousePointerEventData() { }
        // RVA: 0x0B581C0C  token: 0x600074F
        protected virtual UnityEngine.EventSystems.PointerInputModule.MouseState GetMousePointerEventData(System.Int32 id) { }
        // RVA: 0x0B581BD8  token: 0x6000750
        protected UnityEngine.EventSystems.PointerEventData GetLastPointerEventData(System.Int32 id) { }
        // RVA: 0x0A9CED1C  token: 0x6000751
        private static System.Boolean ShouldStartDrag(UnityEngine.Vector2 pressPos, UnityEngine.Vector2 currentPos, System.Single threshold, System.Boolean useDragThreshold) { }
        // RVA: 0x0B5826C4  token: 0x6000752
        protected virtual System.Void ProcessMove(UnityEngine.EventSystems.PointerEventData pointerEvent) { }
        // RVA: 0x0B5824A4  token: 0x6000753
        protected virtual System.Void ProcessDrag(UnityEngine.EventSystems.PointerEventData pointerEvent) { }
        // RVA: 0x0B58243C  token: 0x6000754
        public virtual System.Boolean IsPointerOverGameObject(System.Int32 pointerId) { }
        // RVA: 0x0B5817DC  token: 0x6000755
        protected System.Void ClearSelection() { }
        // RVA: 0x0B5827E4  token: 0x6000756
        public virtual System.String ToString() { }
        // RVA: 0x0B581B0C  token: 0x6000757
        protected System.Void DeselectIfSelectionChanged(UnityEngine.GameObject currentOverGo, UnityEngine.EventSystems.BaseEventData pointerEvent) { }
        // RVA: 0x0B582A78  token: 0x6000758
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000CB  // size: 0xB8
    public class StandaloneInputModule : UnityEngine.EventSystems.PointerInputModule
    {
        // Fields
        private System.Single m_PrevActionTime;  // 0x58
        private UnityEngine.Vector2 m_LastMoveVector;  // 0x5c
        private System.Int32 m_ConsecutiveMoveCount;  // 0x64
        private UnityEngine.Vector2 m_LastMousePosition;  // 0x68
        private UnityEngine.Vector2 m_MousePosition;  // 0x70
        private UnityEngine.GameObject m_CurrentFocusedGameObject;  // 0x78
        private UnityEngine.EventSystems.PointerEventData m_InputPointerEvent;  // 0x80
        private System.String m_HorizontalAxis;  // 0x88
        private System.String m_VerticalAxis;  // 0x90
        private System.String m_SubmitButton;  // 0x98
        private System.String m_CancelButton;  // 0xa0
        private System.Single m_InputActionsPerSecond;  // 0xa8
        private System.Single m_RepeatDelay;  // 0xac
        private System.Boolean m_ForceModuleActive;  // 0xb0

        // Properties
        UnityEngine.EventSystems.StandaloneInputModule.InputMode inputMode { get; /* RVA: 0x011EC580 */ }
        System.Boolean allowActivationOnMobileDevice { get; /* RVA: 0x04D88CC0 */ set; /* RVA: 0x04D88D20 */ }
        System.Boolean forceModuleActive { get; /* RVA: 0x04D88CC0 */ set; /* RVA: 0x04D88D20 */ }
        System.Single inputActionsPerSecond { get; /* RVA: 0x04D8E650 */ set; /* RVA: 0x04D8E680 */ }
        System.Single repeatDelay { get; /* RVA: 0x04D8E660 */ set; /* RVA: 0x04D8E690 */ }
        System.String horizontalAxis { get; /* RVA: 0x04D861F0 */ set; /* RVA: 0x05395D30 */ }
        System.String verticalAxis { get; /* RVA: 0x04D86220 */ set; /* RVA: 0x06402330 */ }
        System.String submitButton { get; /* RVA: 0x04D85EE0 */ set; /* RVA: 0x06352D0C */ }
        System.String cancelButton { get; /* RVA: 0x04D86210 */ set; /* RVA: 0x05391334 */ }

        // Methods
        // RVA: 0x0B586BD8  token: 0x6000766
        protected System.Void .ctor() { }
        // RVA: 0x02FFF600  token: 0x6000778
        private System.Boolean ShouldIgnoreEventsOnNoFocus() { }
        // RVA: 0x0B586AC4  token: 0x6000779
        public virtual System.Void UpdateModule() { }
        // RVA: 0x0B5861EC  token: 0x600077A
        private System.Void ReleaseMouse(UnityEngine.EventSystems.PointerEventData pointerEvent, UnityEngine.GameObject currentOverGo) { }
        // RVA: 0x0B5868A8  token: 0x600077B
        public virtual System.Boolean ShouldActivateModule() { }
        // RVA: 0x0B58407C  token: 0x600077C
        public virtual System.Void ActivateModule() { }
        // RVA: 0x0B5841A4  token: 0x600077D
        public virtual System.Void DeactivateModule() { }
        // RVA: 0x0B586094  token: 0x600077E
        public virtual System.Void Process() { }
        // RVA: 0x0B584ACC  token: 0x600077F
        private System.Boolean ProcessTouchEvents() { }
        // RVA: 0x0B584D08  token: 0x6000780
        protected System.Void ProcessTouchPress(UnityEngine.EventSystems.PointerEventData pointerEvent, System.Boolean pressed, System.Boolean released) { }
        // RVA: 0x0B586670  token: 0x6000781
        protected System.Boolean SendSubmitEventToSelectedObject() { }
        // RVA: 0x0B5841AC  token: 0x6000782
        private UnityEngine.Vector2 GetRawMoveVector() { }
        // RVA: 0x0B586468  token: 0x6000783
        protected System.Boolean SendMoveEventToSelectedObject() { }
        // RVA: 0x0B584518  token: 0x6000784
        protected System.Void ProcessMouseEvent() { }
        // RVA: 0x012081B0  token: 0x6000785
        protected virtual System.Boolean ForceAutoSelect() { }
        // RVA: 0x0B5842B0  token: 0x6000786
        protected System.Void ProcessMouseEvent(System.Int32 id) { }
        // RVA: 0x0B5867D4  token: 0x6000787
        protected System.Boolean SendUpdateEventToSelectedObject() { }
        // RVA: 0x0B5845E0  token: 0x6000788
        protected System.Void ProcessMousePress(UnityEngine.EventSystems.PointerInputModule.MouseButtonEventData data) { }
        // RVA: 0x04D862A0  token: 0x6000789
        protected UnityEngine.GameObject GetCurrentFocusedGameObject() { }

    }

    // TypeToken: 0x20000CE  // size: 0x78
    public class TouchInputModule : UnityEngine.EventSystems.PointerInputModule
    {
        // Fields
        private UnityEngine.Vector2 m_LastMousePosition;  // 0x58
        private UnityEngine.Vector2 m_MousePosition;  // 0x60
        private UnityEngine.EventSystems.PointerEventData m_InputPointerEvent;  // 0x68
        private System.Boolean m_ForceModuleActive;  // 0x70

        // Properties
        System.Boolean allowActivationOnStandalone { get; /* RVA: 0x04D86A50 */ set; /* RVA: 0x04D86A80 */ }
        System.Boolean forceModuleActive { get; /* RVA: 0x04D86A50 */ set; /* RVA: 0x04D86A80 */ }

        // Methods
        // RVA: 0x0B5888BC  token: 0x600078B
        protected System.Void .ctor() { }
        // RVA: 0x0B588764  token: 0x6000790
        public virtual System.Void UpdateModule() { }
        // RVA: 0x0B586D9C  token: 0x6000791
        public virtual System.Boolean IsModuleSupported() { }
        // RVA: 0x0B588410  token: 0x6000792
        public virtual System.Boolean ShouldActivateModule() { }
        // RVA: 0x0B588898  token: 0x6000793
        private System.Boolean UseFakeInput() { }
        // RVA: 0x0B58832C  token: 0x6000794
        public virtual System.Void Process() { }
        // RVA: 0x0B586CAC  token: 0x6000795
        private System.Void FakeTouches() { }
        // RVA: 0x0B586DCC  token: 0x6000796
        private System.Void ProcessTouchEvents() { }
        // RVA: 0x0B586FE0  token: 0x6000797
        protected System.Void ProcessTouchPress(UnityEngine.EventSystems.PointerEventData pointerEvent, System.Boolean pressed, System.Boolean released) { }
        // RVA: 0x0B5841A4  token: 0x6000798
        public virtual System.Void DeactivateModule() { }
        // RVA: 0x0B5884E8  token: 0x6000799
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x20000D0  // size: 0x14
    public sealed struct MoveDirection
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.EventSystems.MoveDirection Left;  // const
        public static UnityEngine.EventSystems.MoveDirection Up;  // const
        public static UnityEngine.EventSystems.MoveDirection Right;  // const
        public static UnityEngine.EventSystems.MoveDirection Down;  // const
        public static UnityEngine.EventSystems.MoveDirection None;  // const

    }

    // TypeToken: 0x20000D1  // size: 0x10
    public static class RaycasterManager
    {
        // Fields
        private static readonly System.Collections.Generic.List<UnityEngine.EventSystems.BaseRaycaster> s_Raycasters;  // static @ 0x0

        // Methods
        // RVA: 0x040DD360  token: 0x600079B
        public static System.Void AddRaycaster(UnityEngine.EventSystems.BaseRaycaster baseRaycaster) { }
        // RVA: 0x0B583A58  token: 0x600079C
        public static System.Collections.Generic.List<UnityEngine.EventSystems.BaseRaycaster> GetRaycasters() { }
        // RVA: 0x040DD290  token: 0x600079D
        public static System.Void RemoveRaycasters(UnityEngine.EventSystems.BaseRaycaster baseRaycaster) { }
        // RVA: 0x04D171C0  token: 0x600079E
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000D2  // size: 0x20
    public abstract class BaseRaycaster : UnityEngine.EventSystems.UIBehaviour
    {
        // Fields
        private UnityEngine.EventSystems.BaseRaycaster m_RootRaycaster;  // 0x18

        // Properties
        UnityEngine.Camera eventCamera { get; /* RVA: -1  // abstract */ }
        System.Int32 priority { get; /* RVA: 0x011EC580 */ }
        System.Int32 sortOrderPriority { get; /* RVA: 0x04DBF110 */ }
        System.Int32 renderOrderPriority { get; /* RVA: 0x04DBF110 */ }
        UnityEngine.EventSystems.BaseRaycaster rootRaycaster { get; /* RVA: 0x0389FF30 */ }

        // Methods
        // RVA: -1  // abstract  token: 0x600079F
        public virtual System.Void Raycast(UnityEngine.EventSystems.PointerEventData eventData, System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult> resultAppendList) { }
        // RVA: 0x0B57BFF8  token: 0x60007A5
        public virtual System.String ToString() { }
        // RVA: 0x040DD330  token: 0x60007A6
        protected virtual System.Void OnEnable() { }
        // RVA: 0x040DD260  token: 0x60007A7
        protected virtual System.Void OnDisable() { }
        // RVA: 0x05390C40  token: 0x60007A8
        protected virtual System.Void OnCanvasHierarchyChanged() { }
        // RVA: 0x05390C40  token: 0x60007A9
        protected virtual System.Void OnTransformParentChanged() { }
        // RVA: 0x05393520  token: 0x60007AA
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000D3  // size: 0x48
    public class Physics2DRaycaster : UnityEngine.EventSystems.PhysicsRaycaster
    {
        // Fields
        private UnityEngine.RaycastHit2D[] m_Hits;  // 0x40

        // Methods
        // RVA: 0x0B5802C0  token: 0x60007AB
        protected System.Void .ctor() { }
        // RVA: 0x0B57FD24  token: 0x60007AC
        public virtual System.Void Raycast(UnityEngine.EventSystems.PointerEventData eventData, System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult> resultAppendList) { }

    }

    // TypeToken: 0x20000D4  // size: 0x40
    public class PhysicsRaycaster : UnityEngine.EventSystems.BaseRaycaster
    {
        // Fields
        protected static System.Int32 kNoEventMaskSet;  // const
        protected UnityEngine.Camera m_EventCamera;  // 0x20
        protected UnityEngine.LayerMask m_EventMask;  // 0x28
        protected System.Int32 m_MaxRayIntersections;  // 0x2c
        protected System.Int32 m_LastMaxRayIntersections;  // 0x30
        private UnityEngine.RaycastHit[] m_Hits;  // 0x38

        // Properties
        UnityEngine.Camera eventCamera { get; /* RVA: 0x0B580A28 */ }
        System.Int32 depth { get; /* RVA: 0x0B5809A4 */ }
        System.Int32 finalEventMask { get; /* RVA: 0x0B580AB8 */ }
        UnityEngine.LayerMask eventMask { get; /* RVA: 0x011F2A90 */ set; /* RVA: 0x011F2AA0 */ }
        System.Int32 maxRayIntersections { get; /* RVA: 0x04D86710 */ set; /* RVA: 0x04D86720 */ }

        // Methods
        // RVA: 0x0B580984  token: 0x60007AD
        protected System.Void .ctor() { }
        // RVA: 0x0B5802C8  token: 0x60007B5
        protected System.Boolean ComputeRayAndDistance(UnityEngine.EventSystems.PointerEventData eventData, UnityEngine.Ray& ray, System.Int32& eventDisplayIndex, System.Single& distanceToClipPlane) { }
        // RVA: 0x0B580598  token: 0x60007B6
        public virtual System.Void Raycast(UnityEngine.EventSystems.PointerEventData eventData, System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult> resultAppendList) { }

    }

    // TypeToken: 0x20000D6  // size: 0x58
    public sealed struct RaycastResult
    {
        // Fields
        private UnityEngine.GameObject m_GameObject;  // 0x10
        public UnityEngine.EventSystems.BaseRaycaster module;  // 0x18
        public System.Single distance;  // 0x20
        public System.Single index;  // 0x24
        public System.Int32 depth;  // 0x28
        public System.Int32 sortingLayer;  // 0x2c
        public System.Int32 sortingOrder;  // 0x30
        public UnityEngine.Vector3 worldPosition;  // 0x34
        public UnityEngine.Vector3 worldNormal;  // 0x40
        public UnityEngine.Vector2 screenPosition;  // 0x4c
        public System.Int32 displayIndex;  // 0x54

        // Properties
        UnityEngine.GameObject gameObject { get; /* RVA: 0x02B76770 */ set; /* RVA: 0x05392320 */ }
        System.Boolean isValid { get; /* RVA: 0x0B5839E4 */ }

        // Methods
        // RVA: 0x0B58356C  token: 0x60007BD
        public System.Void Clear() { }
        // RVA: 0x0B583614  token: 0x60007BE
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x20000D7  // size: 0x18
    public abstract class UIBehaviour : UnityEngine.MonoBehaviour
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60007BF
        protected virtual System.Void Awake() { }
        // RVA: 0x041E1670  token: 0x60007C0
        protected virtual System.Void OnEnable() { }
        // RVA: 0x041E1670  token: 0x60007C1
        protected virtual System.Void Start() { }
        // RVA: 0x041E1670  token: 0x60007C2
        protected virtual System.Void OnDisable() { }
        // RVA: 0x041E1670  token: 0x60007C3
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x02FA7940  token: 0x60007C4
        public virtual System.Boolean IsActive() { }
        // RVA: 0x041E1670  token: 0x60007C5
        protected virtual System.Void OnRectTransformDimensionsChange() { }
        // RVA: 0x041E1670  token: 0x60007C6
        protected virtual System.Void OnBeforeTransformParentChanged() { }
        // RVA: 0x041E1670  token: 0x60007C7
        protected virtual System.Void OnTransformParentChanged() { }
        // RVA: 0x041E1670  token: 0x60007C8
        protected virtual System.Void OnDidApplyAnimationProperties() { }
        // RVA: 0x041E1670  token: 0x60007C9
        protected virtual System.Void OnCanvasHierarchyChanged() { }
        // RVA: 0x031BD4A0  token: 0x60007CA
        public virtual System.Boolean IsDestroyed() { }
        // RVA: 0x05393520  token: 0x60007CB
        protected System.Void .ctor() { }

    }

}

namespace UnityEngine.UI
{

    // TypeToken: 0x2000004  // size: 0x38
    public class AnimationTriggers
    {
        // Fields
        private static System.String kDefaultNormalAnimName;  // const
        private static System.String kDefaultHighlightedAnimName;  // const
        private static System.String kDefaultPressedAnimName;  // const
        private static System.String kDefaultSelectedAnimName;  // const
        private static System.String kDefaultDisabledAnimName;  // const
        private System.String m_NormalTrigger;  // 0x10
        private System.String m_HighlightedTrigger;  // 0x18
        private System.String m_PressedTrigger;  // 0x20
        private System.String m_SelectedTrigger;  // 0x28
        private System.String m_DisabledTrigger;  // 0x30

        // Properties
        System.String normalTrigger { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        System.String highlightedTrigger { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.String pressedTrigger { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        System.String selectedTrigger { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }
        System.String disabledTrigger { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }

        // Methods
        // RVA: 0x03697710  token: 0x600000D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000005  // size: 0x190
    public class Button : UnityEngine.UI.Selectable, UnityEngine.EventSystems.IPointerClickHandler, UnityEngine.EventSystems.IEventSystemHandler, UnityEngine.EventSystems.ISubmitHandler
    {
        // Fields
        private UnityEngine.UI.Button.ButtonClickedEvent m_OnClick;  // 0x188

        // Properties
        UnityEngine.UI.Button.ButtonClickedEvent onClick { get; /* RVA: 0x04D8DA20 */ set; /* RVA: 0x06B8B3B4 */ }

        // Methods
        // RVA: 0x0B45FEEC  token: 0x600000E
        protected System.Void .ctor() { }
        // RVA: 0x0B45FE74  token: 0x6000011
        private System.Void Press() { }
        // RVA: 0x0B45FDE8  token: 0x6000012
        public virtual System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B45FE10  token: 0x6000013
        public virtual System.Void OnSubmit(UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0B45FD8C  token: 0x6000014
        private System.Collections.IEnumerator OnFinishSubmit() { }

    }

    // TypeToken: 0x2000008  // size: 0x14
    public sealed struct CanvasUpdate
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.CanvasUpdate Prelayout;  // const
        public static UnityEngine.UI.CanvasUpdate Layout;  // const
        public static UnityEngine.UI.CanvasUpdate PostLayout;  // const
        public static UnityEngine.UI.CanvasUpdate PreRender;  // const
        public static UnityEngine.UI.CanvasUpdate LatePreRender;  // const
        public static UnityEngine.UI.CanvasUpdate MaxUpdateValue;  // const

    }

    // TypeToken: 0x2000009
    public interface ICanvasElement
    {
        // Properties
        UnityEngine.Transform transform { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x600001C
        public virtual System.Void Rebuild(UnityEngine.UI.CanvasUpdate executing) { }
        // RVA: -1  // abstract  token: 0x600001E
        public virtual System.Void LayoutComplete() { }
        // RVA: -1  // abstract  token: 0x600001F
        public virtual System.Void GraphicUpdateComplete() { }
        // RVA: -1  // abstract  token: 0x6000020
        public virtual System.Boolean IsDestroyed() { }

    }

    // TypeToken: 0x200000A  // size: 0x30
    public class CanvasUpdateRegistry
    {
        // Fields
        private static UnityEngine.UI.CanvasUpdateRegistry s_Instance;  // static @ 0x0
        private System.Boolean m_PerformingLayoutUpdate;  // 0x10
        private System.Boolean m_PerformingGraphicUpdate;  // 0x11
        private Unity.Profiling.ProfilerMarker[] m_CanvasUpdateProfilerStrings;  // 0x18
        private static System.String m_CullingUpdateProfilerString;  // const
        private readonly UnityEngine.UI.Collections.IndexedSet<UnityEngine.UI.ICanvasElement> m_LayoutRebuildQueue;  // 0x20
        private readonly UnityEngine.UI.Collections.IndexedSet<UnityEngine.UI.ICanvasElement> m_GraphicRebuildQueue;  // 0x28
        private static readonly System.Comparison<UnityEngine.UI.ICanvasElement> s_SortLayoutFunction;  // static @ 0x8

        // Properties
        UnityEngine.UI.CanvasUpdateRegistry instance { get; /* RVA: 0x0351BE00 */ }

        // Methods
        // RVA: 0x04821CD0  token: 0x6000021
        protected System.Void .ctor() { }
        // RVA: 0x031BE530  token: 0x6000023
        private System.Boolean ObjectValidForUpdate(UnityEngine.UI.ICanvasElement element) { }
        // RVA: 0x031BD690  token: 0x6000024
        private System.Void CleanInvalidItems() { }
        // RVA: 0x031BD940  token: 0x6000025
        private System.Void PerformUpdate() { }
        // RVA: 0x03B9A390  token: 0x6000026
        private static System.Int32 ParentCount(UnityEngine.Transform child) { }
        // RVA: 0x03B9A250  token: 0x6000027
        private static System.Int32 SortLayoutList(UnityEngine.UI.ICanvasElement x, UnityEngine.UI.ICanvasElement y) { }
        // RVA: 0x039D3C60  token: 0x6000028
        public static System.Void RegisterCanvasElementForLayoutRebuild(UnityEngine.UI.ICanvasElement element) { }
        // RVA: 0x0351B260  token: 0x6000029
        public static System.Boolean TryRegisterCanvasElementForLayoutRebuild(UnityEngine.UI.ICanvasElement element) { }
        // RVA: 0x0351C0B0  token: 0x600002A
        private System.Boolean InternalRegisterCanvasElementForLayoutRebuild(UnityEngine.UI.ICanvasElement element) { }
        // RVA: 0x0351BB50  token: 0x600002B
        public static System.Void RegisterCanvasElementForGraphicRebuild(UnityEngine.UI.ICanvasElement element) { }
        // RVA: 0x0B45FF78  token: 0x600002C
        public static System.Boolean TryRegisterCanvasElementForGraphicRebuild(UnityEngine.UI.ICanvasElement element) { }
        // RVA: 0x0351BEE0  token: 0x600002D
        private System.Boolean InternalRegisterCanvasElementForGraphicRebuild(UnityEngine.UI.ICanvasElement element) { }
        // RVA: 0x0351B370  token: 0x600002E
        public static System.Void UnRegisterCanvasElementForRebuild(UnityEngine.UI.ICanvasElement element) { }
        // RVA: 0x0351ABA0  token: 0x600002F
        private System.Void InternalUnRegisterCanvasElementForLayoutRebuild(UnityEngine.UI.ICanvasElement element) { }
        // RVA: 0x0351AD10  token: 0x6000030
        private System.Void InternalUnRegisterCanvasElementForGraphicRebuild(UnityEngine.UI.ICanvasElement element) { }
        // RVA: 0x0351AE80  token: 0x6000031
        public static System.Boolean IsRebuildingLayout() { }
        // RVA: 0x0351AEC0  token: 0x6000032
        public static System.Boolean IsRebuildingGraphics() { }
        // RVA: 0x04CED4F0  token: 0x6000033
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000B  // size: 0x68
    public sealed struct ColorBlock : System.IEquatable`1
    {
        // Fields
        private UnityEngine.Color m_NormalColor;  // 0x10
        private UnityEngine.Color m_HighlightedColor;  // 0x20
        private UnityEngine.Color m_PressedColor;  // 0x30
        private UnityEngine.Color m_SelectedColor;  // 0x40
        private UnityEngine.Color m_DisabledColor;  // 0x50
        private System.Single m_ColorMultiplier;  // 0x60
        private System.Single m_FadeDuration;  // 0x64
        public static UnityEngine.UI.ColorBlock defaultColorBlock;  // static @ 0x0

        // Properties
        UnityEngine.Color normalColor { get; /* RVA: 0x02B77630 */ set; /* RVA: 0x04D8D270 */ }
        UnityEngine.Color highlightedColor { get; /* RVA: 0x02B2D510 */ set; /* RVA: 0x04D8C980 */ }
        UnityEngine.Color pressedColor { get; /* RVA: 0x04D8C200 */ set; /* RVA: 0x04D8C210 */ }
        UnityEngine.Color selectedColor { get; /* RVA: 0x04D87480 */ set; /* RVA: 0x04D87490 */ }
        UnityEngine.Color disabledColor { get; /* RVA: 0x04D905C0 */ set; /* RVA: 0x04D926F0 */ }
        System.Single colorMultiplier { get; /* RVA: 0x04D885A0 */ set; /* RVA: 0x04D88FF0 */ }
        System.Single fadeDuration { get; /* RVA: 0x04D86E00 */ set; /* RVA: 0x04D86E10 */ }

        // Methods
        // RVA: 0x04B2FF10  token: 0x6000042
        private static System.Void .cctor() { }
        // RVA: 0x0B460300  token: 0x6000043
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0B4603B0  token: 0x6000044
        public virtual System.Boolean Equals(UnityEngine.UI.ColorBlock other) { }
        // RVA: 0x0B4605B8  token: 0x6000045
        public static System.Boolean op_Equality(UnityEngine.UI.ColorBlock point1, UnityEngine.UI.ColorBlock point2) { }
        // RVA: 0x0B46064C  token: 0x6000046
        public static System.Boolean op_Inequality(UnityEngine.UI.ColorBlock point1, UnityEngine.UI.ColorBlock point2) { }
        // RVA: 0x0B460534  token: 0x6000047
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x200000C  // size: 0x20
    public class ClipperRegistry
    {
        // Fields
        private static UnityEngine.UI.ClipperRegistry s_Instance;  // static @ 0x0
        private readonly UnityEngine.UI.Collections.IndexedSet<UnityEngine.UI.IClipper> m_Clippers;  // 0x10
        private readonly Unity.Profiling.ProfilerMarker clippersMarker;  // 0x18

        // Properties
        UnityEngine.UI.ClipperRegistry instance { get; /* RVA: 0x031BCA30 */ }

        // Methods
        // RVA: 0x04CE72F0  token: 0x6000048
        protected System.Void .ctor() { }
        // RVA: 0x031BC990  token: 0x600004A
        public System.Void Cull() { }
        // RVA: 0x0B45FFD0  token: 0x600004B
        private System.Void CullOriginal() { }
        // RVA: 0x04424750  token: 0x600004C
        public static System.Void Register(UnityEngine.UI.IClipper c) { }
        // RVA: 0x03D91BD0  token: 0x600004D
        public static System.Void Unregister(UnityEngine.UI.IClipper c) { }

    }

    // TypeToken: 0x200000D  // size: 0x10
    public static class Clipping
    {
        // Methods
        // RVA: 0x0B460068  token: 0x600004E
        public static UnityEngine.Rect FindCullAndClipWorldRect(System.Collections.Generic.List<UnityEngine.UI.RectMask2D> rectMaskParents, System.Boolean& validRect) { }

    }

    // TypeToken: 0x200000E
    public interface IClipper
    {
        // Methods
        // RVA: -1  // abstract  token: 0x600004F
        public virtual System.Void PerformClipping() { }

    }

    // TypeToken: 0x200000F
    public interface IClippable
    {
        // Properties
        UnityEngine.GameObject gameObject { get; /* RVA: -1  // abstract */ }
        UnityEngine.RectTransform rectTransform { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000051
        public virtual System.Void RecalculateClipping() { }
        // RVA: -1  // abstract  token: 0x6000053
        public virtual System.Void Cull(UnityEngine.Rect clipRect, System.Boolean validRect) { }
        // RVA: -1  // abstract  token: 0x6000054
        public virtual System.Void SetClipRect(UnityEngine.Rect value, System.Boolean validRect) { }
        // RVA: -1  // abstract  token: 0x6000055
        public virtual System.Void SetClipSoftness(UnityEngine.Vector2 clipSoftness) { }
        // RVA: -1  // abstract  token: 0x6000056
        public virtual System.Void SetHGClipSoftness(UnityEngine.Vector4 hgClipSoftness) { }

    }

    // TypeToken: 0x2000010  // size: 0x20
    public class RectangularVertexClipper
    {
        // Fields
        private readonly UnityEngine.Vector3[] m_WorldCorners;  // 0x10
        private readonly UnityEngine.Vector3[] m_CanvasCorners;  // 0x18

        // Methods
        // RVA: 0x0B46BC90  token: 0x6000057
        public UnityEngine.Rect GetCanvasRect(UnityEngine.RectTransform t, UnityEngine.Canvas c) { }
        // RVA: 0x0B46BA44  token: 0x6000058
        public UnityEngine.Rect GetCanvasRectAABB(UnityEngine.RectTransform t, UnityEngine.Canvas c) { }
        // RVA: 0x0404D5C0  token: 0x6000059
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000011  // size: 0x10
    public static class DefaultControls
    {
        // Fields
        private static UnityEngine.UI.DefaultControls.IFactoryControls m_CurrentFactory;  // static @ 0x0
        private static System.Single kWidth;  // const
        private static System.Single kThickHeight;  // const
        private static System.Single kThinHeight;  // const
        private static UnityEngine.Vector2 s_ThickElementSize;  // static @ 0x8
        private static UnityEngine.Vector2 s_ThinElementSize;  // static @ 0x10
        private static UnityEngine.Vector2 s_ImageElementSize;  // static @ 0x18
        private static UnityEngine.Color s_DefaultSelectableColor;  // static @ 0x20
        private static UnityEngine.Color s_PanelColor;  // static @ 0x30
        private static UnityEngine.Color s_TextColor;  // static @ 0x40

        // Properties
        UnityEngine.UI.DefaultControls.IFactoryControls factory { get; /* RVA: 0x0B464448 */ }

        // Methods
        // RVA: 0x0B463F70  token: 0x600005B
        private static UnityEngine.GameObject CreateUIElementRoot(System.String name, UnityEngine.Vector2 size, System.Type[] components) { }
        // RVA: 0x0B46401C  token: 0x600005C
        private static UnityEngine.GameObject CreateUIObject(System.String name, UnityEngine.GameObject parent, System.Type[] components) { }
        // RVA: 0x0B4640F0  token: 0x600005D
        private static System.Void SetDefaultTextValues(UnityEngine.UI.Text lbl) { }
        // RVA: 0x0B4640A0  token: 0x600005E
        private static System.Void SetDefaultColorTransitionValues(UnityEngine.UI.Selectable slider) { }
        // RVA: 0x0B464268  token: 0x600005F
        private static System.Void SetParentAndAlign(UnityEngine.GameObject child, UnityEngine.GameObject parent) { }
        // RVA: 0x0B4641A8  token: 0x6000060
        private static System.Void SetLayerRecursively(UnityEngine.GameObject go, System.Int32 layer) { }
        // RVA: 0x0B462214  token: 0x6000061
        public static UnityEngine.GameObject CreatePanel(UnityEngine.UI.DefaultControls.Resources resources) { }
        // RVA: 0x0B4606E4  token: 0x6000062
        public static UnityEngine.GameObject CreateButton(UnityEngine.UI.DefaultControls.Resources resources) { }
        // RVA: 0x0B46384C  token: 0x6000063
        public static UnityEngine.GameObject CreateText(UnityEngine.UI.DefaultControls.Resources resources) { }
        // RVA: 0x0B461B78  token: 0x6000064
        public static UnityEngine.GameObject CreateImage(UnityEngine.UI.DefaultControls.Resources resources) { }
        // RVA: 0x0B462444  token: 0x6000065
        public static UnityEngine.GameObject CreateRawImage(UnityEngine.UI.DefaultControls.Resources resources) { }
        // RVA: 0x0B463100  token: 0x6000066
        public static UnityEngine.GameObject CreateSlider(UnityEngine.UI.DefaultControls.Resources resources) { }
        // RVA: 0x0B462CAC  token: 0x6000067
        public static UnityEngine.GameObject CreateScrollbar(UnityEngine.UI.DefaultControls.Resources resources) { }
        // RVA: 0x0B4639B0  token: 0x6000068
        public static UnityEngine.GameObject CreateToggle(UnityEngine.UI.DefaultControls.Resources resources) { }
        // RVA: 0x0B461C84  token: 0x6000069
        public static UnityEngine.GameObject CreateInputField(UnityEngine.UI.DefaultControls.Resources resources) { }
        // RVA: 0x0B460A40  token: 0x600006A
        public static UnityEngine.GameObject CreateDropdown(UnityEngine.UI.DefaultControls.Resources resources) { }
        // RVA: 0x0B462550  token: 0x600006B
        public static UnityEngine.GameObject CreateScrollView(UnityEngine.UI.DefaultControls.Resources resources) { }
        // RVA: 0x0B464334  token: 0x600006C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000015  // size: 0x1F8
    public class Dropdown : UnityEngine.UI.Selectable, UnityEngine.EventSystems.IPointerClickHandler, UnityEngine.EventSystems.IEventSystemHandler, UnityEngine.EventSystems.ISubmitHandler, UnityEngine.EventSystems.ICancelHandler
    {
        // Fields
        private UnityEngine.RectTransform m_Template;  // 0x188
        private UnityEngine.UI.Text m_CaptionText;  // 0x190
        private UnityEngine.UI.Image m_CaptionImage;  // 0x198
        private UnityEngine.UI.Text m_ItemText;  // 0x1a0
        private UnityEngine.UI.Image m_ItemImage;  // 0x1a8
        private System.Int32 m_Value;  // 0x1b0
        private UnityEngine.UI.Dropdown.OptionDataList m_Options;  // 0x1b8
        private UnityEngine.UI.Dropdown.DropdownEvent m_OnValueChanged;  // 0x1c0
        private System.Single m_AlphaFadeSpeed;  // 0x1c8
        private UnityEngine.GameObject m_Dropdown;  // 0x1d0
        private UnityEngine.GameObject m_Blocker;  // 0x1d8
        private System.Collections.Generic.List<UnityEngine.UI.Dropdown.DropdownItem> m_Items;  // 0x1e0
        private UnityEngine.UI.CoroutineTween.TweenRunner<UnityEngine.UI.CoroutineTween.FloatTween> m_AlphaTweenRunner;  // 0x1e8
        private System.Boolean validTemplate;  // 0x1f0
        private static System.Int32 kHighSortingLayer;  // const
        private static UnityEngine.UI.Dropdown.OptionData s_NoOptionData;  // static @ 0x0

        // Properties
        UnityEngine.RectTransform template { get; /* RVA: 0x04D8DA20 */ set; /* RVA: 0x0B4673B8 */ }
        UnityEngine.UI.Text captionText { get; /* RVA: 0x04D8DD50 */ set; /* RVA: 0x0B46730C */ }
        UnityEngine.UI.Image captionImage { get; /* RVA: 0x04D8DED0 */ set; /* RVA: 0x0B4672E4 */ }
        UnityEngine.UI.Text itemText { get; /* RVA: 0x04D8DA10 */ set; /* RVA: 0x0B46735C */ }
        UnityEngine.UI.Image itemImage { get; /* RVA: 0x04D8D9C0 */ set; /* RVA: 0x0B467334 */ }
        System.Collections.Generic.List<UnityEngine.UI.Dropdown.OptionData> options { get; /* RVA: 0x0B4672C4 */ set; /* RVA: 0x0B467384 */ }
        UnityEngine.UI.Dropdown.DropdownEvent onValueChanged { get; /* RVA: 0x04D8DA80 */ set; /* RVA: 0x06B8B42C */ }
        System.Single alphaFadeSpeed { get; /* RVA: 0x04D8E980 */ set; /* RVA: 0x04D8EA30 */ }
        System.Int32 value { get; /* RVA: 0x04D8E930 */ set; /* RVA: 0x0B4673E0 */ }

        // Methods
        // RVA: 0x0B465B2C  token: 0x6000083
        public System.Void SetValueWithoutNotify(System.Int32 input) { }
        // RVA: 0x0B465B38  token: 0x6000084
        private System.Void Set(System.Int32 value, System.Boolean sendCallback) { }
        // RVA: 0x0B46719C  token: 0x6000085
        protected System.Void .ctor() { }
        // RVA: 0x0B464D90  token: 0x6000086
        protected virtual System.Void Awake() { }
        // RVA: 0x0B46709C  token: 0x6000087
        protected virtual System.Void Start() { }
        // RVA: 0x0B465704  token: 0x6000088
        protected virtual System.Void OnDisable() { }
        // RVA: 0x0B4658A8  token: 0x6000089
        public System.Void RefreshShownValue() { }
        // RVA: 0x0B464B20  token: 0x600008A
        public System.Void AddOptions(System.Collections.Generic.List<UnityEngine.UI.Dropdown.OptionData> options) { }
        // RVA: 0x0B464918  token: 0x600008B
        public System.Void AddOptions(System.Collections.Generic.List<System.String> options) { }
        // RVA: 0x0B464A1C  token: 0x600008C
        public System.Void AddOptions(System.Collections.Generic.List<UnityEngine.Sprite> options) { }
        // RVA: 0x0B464E78  token: 0x600008D
        public System.Void ClearOptions() { }
        // RVA: 0x0B465C28  token: 0x600008E
        private System.Void SetupTemplate(UnityEngine.Canvas rootCanvas) { }
        // RVA: -1  // generic def  token: 0x600008F
        private static T GetOrAddComponent(UnityEngine.GameObject go) { }
        // RVA: 0x0B465798  token: 0x6000090
        public virtual System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B465798  token: 0x6000091
        public virtual System.Void OnSubmit(UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0B4656FC  token: 0x6000092
        public virtual System.Void OnCancel(UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0B466270  token: 0x6000093
        public System.Void Show() { }
        // RVA: 0x0B464ED8  token: 0x6000094
        protected virtual UnityEngine.GameObject CreateBlocker(UnityEngine.Canvas rootCanvas) { }
        // RVA: 0x0B465420  token: 0x6000095
        protected virtual System.Void DestroyBlocker(UnityEngine.GameObject blocker) { }
        // RVA: 0x0B46530C  token: 0x6000096
        protected virtual UnityEngine.GameObject CreateDropdownList(UnityEngine.GameObject template) { }
        // RVA: 0x0B46546C  token: 0x6000097
        protected virtual System.Void DestroyDropdownList(UnityEngine.GameObject dropdownList) { }
        // RVA: 0x0B465360  token: 0x6000098
        protected virtual UnityEngine.UI.Dropdown.DropdownItem CreateItem(UnityEngine.UI.Dropdown.DropdownItem itemTemplate) { }
        // RVA: 0x041E1670  token: 0x6000099
        protected virtual System.Void DestroyItem(UnityEngine.UI.Dropdown.DropdownItem item) { }
        // RVA: 0x0B46468C  token: 0x600009A
        private UnityEngine.UI.Dropdown.DropdownItem AddItem(UnityEngine.UI.Dropdown.OptionData data, System.Boolean selected, UnityEngine.UI.Dropdown.DropdownItem itemTemplate, System.Collections.Generic.List<UnityEngine.UI.Dropdown.DropdownItem> items) { }
        // RVA: 0x0B464D04  token: 0x600009B
        private System.Void AlphaFadeList(System.Single duration, System.Single alpha) { }
        // RVA: 0x0B464B88  token: 0x600009C
        private System.Void AlphaFadeList(System.Single duration, System.Single start, System.Single end) { }
        // RVA: 0x0B465A8C  token: 0x600009D
        private System.Void SetAlpha(System.Single alpha) { }
        // RVA: 0x0B4654B8  token: 0x600009E
        public System.Void Hide() { }
        // RVA: 0x0B4653B4  token: 0x600009F
        private System.Collections.IEnumerator DelayedDestroyDropdownList(System.Single delay) { }
        // RVA: 0x0B4655AC  token: 0x60000A0
        private System.Void ImmediateDestroyDropdownList() { }
        // RVA: 0x0B4657A0  token: 0x60000A1
        private System.Void OnSelectItem(UnityEngine.UI.Toggle toggle) { }
        // RVA: 0x0B46712C  token: 0x60000A2
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200001C  // size: 0x40
    public class FontData : UnityEngine.ISerializationCallbackReceiver
    {
        // Fields
        private UnityEngine.Font m_Font;  // 0x10
        private System.Int32 m_FontSize;  // 0x18
        private UnityEngine.FontStyle m_FontStyle;  // 0x1c
        private System.Boolean m_BestFit;  // 0x20
        private System.Int32 m_MinSize;  // 0x24
        private System.Int32 m_MaxSize;  // 0x28
        private UnityEngine.TextAnchor m_Alignment;  // 0x2c
        private System.Boolean m_AlignByGeometry;  // 0x30
        private System.Boolean m_RichText;  // 0x31
        private UnityEngine.HorizontalWrapMode m_HorizontalOverflow;  // 0x34
        private UnityEngine.VerticalWrapMode m_VerticalOverflow;  // 0x38
        private System.Single m_LineSpacing;  // 0x3c

        // Properties
        UnityEngine.UI.FontData defaultFontData { get; /* RVA: 0x0B467438 */ }
        UnityEngine.Font font { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        System.Int32 fontSize { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x011EF9B0 */ }
        UnityEngine.FontStyle fontStyle { get; /* RVA: 0x04D86310 */ set; /* RVA: 0x04D86320 */ }
        System.Boolean bestFit { get; /* RVA: 0x04D866B0 */ set; /* RVA: 0x04D866D0 */ }
        System.Int32 minSize { get; /* RVA: 0x011F0020 */ set; /* RVA: 0x011F0030 */ }
        System.Int32 maxSize { get; /* RVA: 0x011F2A90 */ set; /* RVA: 0x011F2AA0 */ }
        UnityEngine.TextAnchor alignment { get; /* RVA: 0x04D86710 */ set; /* RVA: 0x04D86720 */ }
        System.Boolean alignByGeometry { get; /* RVA: 0x04D865F0 */ set; /* RVA: 0x04D86620 */ }
        System.Boolean richText { get; /* RVA: 0x04D86AB0 */ set; /* RVA: 0x04D86AC0 */ }
        UnityEngine.HorizontalWrapMode horizontalOverflow { get; /* RVA: 0x04D86730 */ set; /* RVA: 0x04D86760 */ }
        UnityEngine.VerticalWrapMode verticalOverflow { get; /* RVA: 0x04D863B0 */ set; /* RVA: 0x04D86430 */ }
        System.Single lineSpacing { get; /* RVA: 0x04D863E0 */ set; /* RVA: 0x04D86460 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60000DB
        private virtual System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize() { }
        // RVA: 0x0B4673EC  token: 0x60000DC
        private virtual System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize() { }
        // RVA: 0x041E1670  token: 0x60000DD
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001D  // size: 0x10
    public static class FontUpdateTracker
    {
        // Fields
        private static System.Collections.Generic.Dictionary<UnityEngine.Font,System.Collections.Generic.HashSet<UnityEngine.UI.Text>> m_Tracked;  // static @ 0x0

        // Methods
        // RVA: 0x0B46761C  token: 0x60000DE
        public static System.Void TrackText(UnityEngine.UI.Text t) { }
        // RVA: 0x0B4674AC  token: 0x60000DF
        private static System.Void RebuildForFont(UnityEngine.Font f) { }
        // RVA: 0x0B46783C  token: 0x60000E0
        public static System.Void UntrackText(UnityEngine.UI.Text t) { }
        // RVA: 0x0B467A20  token: 0x60000E1
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200001E  // size: 0xB0
    public abstract class Graphic : UnityEngine.EventSystems.UIBehaviour, UnityEngine.UI.ICanvasElement
    {
        // Fields
        protected static UnityEngine.Material s_DefaultUI;  // static @ 0x0
        protected static UnityEngine.Texture2D s_WhiteTexture;  // static @ 0x8
        private static System.String s_DefaultMaterialName;  // static @ 0x10
        private static System.String s_OverlayShaderKeyword;  // static @ 0x18
        protected UnityEngine.Material m_Material;  // 0x18
        private UnityEngine.Color m_Color;  // 0x20
        protected System.Boolean m_SkipLayoutUpdate;  // 0x30
        protected System.Boolean m_SkipMaterialUpdate;  // 0x31
        private System.Boolean m_RaycastTarget;  // 0x32
        private UnityEngine.Vector4 m_RaycastPadding;  // 0x34
        private System.Boolean m_RaycastIgnoreMask;  // 0x44
        private UnityEngine.RectTransform m_RectTransform;  // 0x48
        private UnityEngine.CanvasRenderer m_CanvasRenderer;  // 0x50
        private UnityEngine.Canvas m_Canvas;  // 0x58
        private System.Boolean m_VertsDirty;  // 0x60
        private System.Boolean m_MaterialDirty;  // 0x61
        protected UnityEngine.Events.UnityAction m_OnDirtyLayoutCallback;  // 0x68
        protected UnityEngine.Events.UnityAction m_OnDirtyVertsCallback;  // 0x70
        protected UnityEngine.Events.UnityAction m_OnDirtyMaterialCallback;  // 0x78
        protected static UnityEngine.Mesh s_Mesh;  // static @ 0x20
        private static readonly UnityEngine.UI.VertexHelper s_VertexHelper;  // static @ 0x28
        protected UnityEngine.Mesh m_CachedMesh;  // 0x80
        protected UnityEngine.Vector2[] m_CachedUvs;  // 0x88
        private UnityEngine.UI.CoroutineTween.TweenRunner<UnityEngine.UI.CoroutineTween.ColorTween> m_ColorTweenRunnerLazy;  // 0x90
        protected System.Boolean m_EnableRuntimeAtlas;  // 0x98
        protected UnityEngine.Texture m_RuntimeAtlasTexture;  // 0xa0
        private System.Boolean <useLegacyMeshGeneration>k__BackingField;  // 0xa8

        // Properties
        System.String defaultMaterialName { get; /* RVA: 0x0B467EC4 */ set; /* RVA: 0x0B467F64 */ }
        UnityEngine.Material defaultGraphicMaterial { get; /* RVA: 0x031BCDA0 */ }
        System.String overlayShaderKeyword { get; /* RVA: 0x0B467F14 */ set; /* RVA: 0x0B467FF4 */ }
        UnityEngine.Color color { get; /* RVA: 0x04D8C200 */ set; /* RVA: 0x03046910 */ }
        System.Boolean raycastTarget { get; /* RVA: 0x04D86DC0 */ set; /* RVA: 0x044550F0 */ }
        UnityEngine.Vector4 raycastPadding { get; /* RVA: 0x04D89180 */ set; /* RVA: 0x04D89190 */ }
        UnityEngine.UI.CoroutineTween.TweenRunner<UnityEngine.UI.CoroutineTween.ColorTween> m_ColorTweenRunner { get; /* RVA: 0x0343F2F0 */ }
        System.Boolean enableRuntimeAtlas { get; /* RVA: 0x04D86C50 */ set; /* RVA: 0x0B467FC4 */ }
        UnityEngine.Texture runtimeAtlasTexture { get; /* RVA: 0x04D86210 */ set; /* RVA: 0x0B468054 */ }
        System.Boolean useLegacyMeshGeneration { get; /* RVA: 0x04D90720 */ set; /* RVA: 0x04D90750 */ }
        System.Int32 depth { get; /* RVA: 0x031BF800 */ }
        UnityEngine.RectTransform rectTransform { get; /* RVA: 0x031C2170 */ }
        UnityEngine.Canvas canvas { get; /* RVA: 0x02FCB610 */ }
        UnityEngine.CanvasRenderer canvasRenderer { get; /* RVA: 0x031BFEC0 */ }
        UnityEngine.Material defaultMaterial { get; /* RVA: 0x031BCD70 */ }
        UnityEngine.Material material { get; /* RVA: 0x03E9D0E0 */ set; /* RVA: 0x0425D120 */ }
        UnityEngine.Material materialForRendering { get; /* RVA: 0x031EE800 */ }
        UnityEngine.Texture mainTexture { get; /* RVA: 0x04D394C0 */ }
        UnityEngine.Mesh workerMesh { get; /* RVA: 0x031F2480 */ }

        // Methods
        // RVA: 0x03697340  token: 0x60000F4
        protected System.Void .ctor() { }
        // RVA: 0x03D23600  token: 0x60000F5
        public virtual System.Void SetAllDirty() { }
        // RVA: 0x03C98E90  token: 0x60000F6
        public virtual System.Void SetLayoutDirty() { }
        // RVA: 0x0351B640  token: 0x60000F7
        public virtual System.Void SetVerticesDirty() { }
        // RVA: 0x0351B8D0  token: 0x60000F8
        public virtual System.Void SetMaterialDirty() { }
        // RVA: 0x0351AB00  token: 0x60000F9
        protected virtual System.Void OnRectTransformDimensionsChange() { }
        // RVA: 0x035197A0  token: 0x60000FA
        protected virtual System.Void OnBeforeTransformParentChanged() { }
        // RVA: 0x03F3AF00  token: 0x60000FB
        protected virtual System.Void OnTransformParentChanged() { }
        // RVA: 0x02FA9D50  token: 0x60000FF
        private System.Void CacheCanvas() { }
        // RVA: 0x031EBEA0  token: 0x6000106
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0351A480  token: 0x6000107
        protected virtual System.Void OnDisable() { }
        // RVA: 0x03094210  token: 0x6000108
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x031C3F80  token: 0x6000109
        protected virtual System.Void OnCanvasHierarchyChanged() { }
        // RVA: 0x031EAB40  token: 0x600010A
        public virtual System.Void OnCullingChanged() { }
        // RVA: 0x031BF970  token: 0x600010B
        public virtual System.Void Rebuild(UnityEngine.UI.CanvasUpdate update) { }
        // RVA: 0x041E1670  token: 0x600010C
        public virtual System.Void LayoutComplete() { }
        // RVA: 0x041E1670  token: 0x600010D
        public virtual System.Void GraphicUpdateComplete() { }
        // RVA: 0x031BEC70  token: 0x600010E
        protected virtual System.Void UpdateMaterial() { }
        // RVA: 0x031F2660  token: 0x600010F
        protected virtual System.Void UpdateGeometry() { }
        // RVA: 0x031F1FC0  token: 0x6000110
        private System.Void DoMeshGeneration() { }
        // RVA: 0x046923F0  token: 0x6000111
        private System.Void DoLegacyMeshGeneration() { }
        // RVA: 0x041E1670  token: 0x6000113
        protected virtual System.Void OnFillVBO(System.Collections.Generic.List<UnityEngine.UIVertex> vbo) { }
        // RVA: 0x04A27120  token: 0x6000114
        protected virtual System.Void OnPopulateMesh(UnityEngine.Mesh m) { }
        // RVA: 0x03240F40  token: 0x6000115
        protected virtual System.Void OnPopulateMesh(UnityEngine.UI.VertexHelper vh) { }
        // RVA: 0x0B467B50  token: 0x6000116
        protected virtual System.Void OnDidApplyAnimationProperties() { }
        // RVA: 0x041E1670  token: 0x6000117
        public virtual System.Void SetNativeSize() { }
        // RVA: 0x031F0020  token: 0x6000118
        public virtual System.Boolean Raycast(UnityEngine.Vector2 sp, UnityEngine.Camera eventCamera) { }
        // RVA: 0x0B467B60  token: 0x6000119
        public UnityEngine.Vector2 PixelAdjustPoint(UnityEngine.Vector2 point) { }
        // RVA: 0x031C2550  token: 0x600011A
        public UnityEngine.Rect GetPixelAdjustedRect() { }
        // RVA: 0x0343EE00  token: 0x600011B
        public virtual System.Void CrossFadeColor(UnityEngine.Color targetColor, System.Single duration, System.Boolean ignoreTimeScale, System.Boolean useAlpha) { }
        // RVA: 0x0343EE80  token: 0x600011C
        public virtual System.Void CrossFadeColor(UnityEngine.Color targetColor, System.Single duration, System.Boolean ignoreTimeScale, System.Boolean useAlpha, System.Boolean useRGB) { }
        // RVA: 0x04CC9850  token: 0x600011D
        private static UnityEngine.Color CreateColorFromAlpha(System.Single alpha) { }
        // RVA: 0x04CC9780  token: 0x600011E
        public virtual System.Void CrossFadeAlpha(System.Single alpha, System.Single duration, System.Boolean ignoreTimeScale) { }
        // RVA: 0x0B467C94  token: 0x600011F
        public System.Void RegisterDirtyLayoutCallback(UnityEngine.Events.UnityAction action) { }
        // RVA: 0x0B467DAC  token: 0x6000120
        public System.Void UnregisterDirtyLayoutCallback(UnityEngine.Events.UnityAction action) { }
        // RVA: 0x02EDDB70  token: 0x6000121
        public System.Void RegisterDirtyVerticesCallback(UnityEngine.Events.UnityAction action) { }
        // RVA: 0x02EDD170  token: 0x6000122
        public System.Void UnregisterDirtyVerticesCallback(UnityEngine.Events.UnityAction action) { }
        // RVA: 0x0B467D20  token: 0x6000123
        public System.Void RegisterDirtyMaterialCallback(UnityEngine.Events.UnityAction action) { }
        // RVA: 0x0B467E38  token: 0x6000124
        public System.Void UnregisterDirtyMaterialCallback(UnityEngine.Events.UnityAction action) { }
        // RVA: 0x04B473C0  token: 0x6000125
        private static System.Void .cctor() { }
        // RVA: 0x053953F4  token: 0x6000126
        private virtual UnityEngine.Transform UnityEngine.UI.ICanvasElement.get_transform() { }

    }

    // TypeToken: 0x200001F  // size: 0x40
    public class GraphicRaycaster : UnityEngine.EventSystems.BaseRaycaster
    {
        // Fields
        protected static System.Int32 kNoEventMaskSet;  // const
        private System.Boolean m_IgnoreReversedGraphics;  // 0x20
        private UnityEngine.UI.GraphicRaycaster.BlockingObjects m_BlockingObjects;  // 0x24
        protected UnityEngine.LayerMask m_BlockingMask;  // 0x28
        private UnityEngine.Canvas m_Canvas;  // 0x30
        private System.Collections.Generic.List<UnityEngine.UI.Graphic> m_RaycastResults;  // 0x38
        private static readonly System.Collections.Generic.List<UnityEngine.UI.Graphic> s_SortedGraphics;  // static @ 0x0

        // Properties
        System.Int32 sortOrderPriority { get; /* RVA: 0x03DC3FB0 */ }
        System.Int32 renderOrderPriority { get; /* RVA: 0x03DC4A00 */ }
        System.Boolean ignoreReversedGraphics { get; /* RVA: 0x04D866B0 */ set; /* RVA: 0x04D866D0 */ }
        UnityEngine.UI.GraphicRaycaster.BlockingObjects blockingObjects { get; /* RVA: 0x011F0020 */ set; /* RVA: 0x011F0030 */ }
        UnityEngine.LayerMask blockingMask { get; /* RVA: 0x011F2A90 */ set; /* RVA: 0x011F2AA0 */ }
        UnityEngine.Canvas canvas { get; /* RVA: 0x02FC99B0 */ }
        UnityEngine.Camera eventCamera { get; /* RVA: 0x03096CF0 */ }

        // Methods
        // RVA: 0x043B7CB0  token: 0x600012F
        protected System.Void .ctor() { }
        // RVA: 0x02FC7E70  token: 0x6000131
        public virtual System.Void Raycast(UnityEngine.EventSystems.PointerEventData eventData, System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult> resultAppendList) { }
        // RVA: 0x031BEDF0  token: 0x6000133
        private static System.Void Raycast(UnityEngine.Canvas canvas, UnityEngine.Camera eventCamera, UnityEngine.Vector2 pointerPosition, System.Collections.Generic.IList<UnityEngine.UI.Graphic> foundGraphics, System.Collections.Generic.List<UnityEngine.UI.Graphic> results) { }
        // RVA: 0x043B7B80  token: 0x6000134
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000022  // size: 0x20
    public class GraphicRegistry
    {
        // Fields
        private static UnityEngine.UI.GraphicRegistry s_Instance;  // static @ 0x0
        private readonly System.Collections.Generic.Dictionary<UnityEngine.Canvas,UnityEngine.UI.Collections.IndexedSet<UnityEngine.UI.Graphic>> m_Graphics;  // 0x10
        private readonly System.Collections.Generic.Dictionary<UnityEngine.Canvas,UnityEngine.UI.Collections.IndexedSet<UnityEngine.UI.Graphic>> m_RaycastableGraphics;  // 0x18
        private static readonly System.Collections.Generic.List<UnityEngine.UI.Graphic> s_EmptyList;  // static @ 0x8

        // Properties
        UnityEngine.UI.GraphicRegistry instance { get; /* RVA: 0x02FC7CD0 */ }

        // Methods
        // RVA: 0x04A20380  token: 0x6000138
        protected System.Void .ctor() { }
        // RVA: 0x031EBFF0  token: 0x600013A
        public static System.Void RegisterGraphicForCanvas(UnityEngine.Canvas c, UnityEngine.UI.Graphic graphic) { }
        // RVA: 0x031EC3A0  token: 0x600013B
        public static System.Void RegisterRaycastGraphicForCanvas(UnityEngine.Canvas c, UnityEngine.UI.Graphic graphic) { }
        // RVA: 0x0351A630  token: 0x600013C
        public static System.Void UnregisterGraphicForCanvas(UnityEngine.Canvas c, UnityEngine.UI.Graphic graphic) { }
        // RVA: 0x0351A8A0  token: 0x600013D
        public static System.Void UnregisterRaycastGraphicForCanvas(UnityEngine.Canvas c, UnityEngine.UI.Graphic graphic) { }
        // RVA: 0x0B467AAC  token: 0x600013E
        public static System.Collections.Generic.IList<UnityEngine.UI.Graphic> GetGraphicsForCanvas(UnityEngine.Canvas canvas) { }
        // RVA: 0x02FC7830  token: 0x600013F
        public static System.Collections.Generic.IList<UnityEngine.UI.Graphic> GetRaycastableGraphicsForCanvas(UnityEngine.Canvas canvas) { }
        // RVA: 0x043B7C10  token: 0x6000140
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000023  // size: 0x190
    public class HGImage : UnityEngine.UI.Image
    {
        // Methods
        // RVA: 0x031BB800  token: 0x6000141
        protected virtual System.Void UpdateGeometry() { }
        // RVA: 0x031C29F0  token: 0x6000142
        private System.Boolean CanUseBatchedPath() { }
        // RVA: 0x031E85B0  token: 0x6000143
        protected virtual System.Void OnPopulateMesh(UnityEngine.UI.VertexHelper toFill) { }
        // RVA: 0x041E1670  token: 0x6000144
        protected virtual System.Void OnPopulateMeshFallback(UnityEngine.UI.VertexHelper toFill) { }
        // RVA: 0x03696D70  token: 0x6000145
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000024  // size: 0x10
    public static class HGUIBatchInit
    {
        // Methods
        // RVA: 0x04821F30  token: 0x6000146
        public static System.Void Initialize() { }

    }

    // TypeToken: 0x2000025  // size: 0x10
    public static class HGUIBatchClipper
    {
        // Fields
        private static System.Boolean <Enabled>k__BackingField;  // static @ 0x0
        private static readonly Unity.Profiling.ProfilerMarker s_BatchPrepareMarker;  // static @ 0x8
        private static readonly Unity.Profiling.ProfilerMarker s_BatchComputeMarker;  // static @ 0x10
        private static readonly Unity.Profiling.ProfilerMarker s_BatchDispatchMarker;  // static @ 0x18
        private static readonly System.Collections.Generic.List<UnityEngine.UI.RectMask2D> s_BatchMasks;  // static @ 0x20
        private static readonly System.Collections.Generic.List<UnityEngine.UI.MaskableGraphic> s_FlatTargets;  // static @ 0x28

        // Properties
        System.Boolean Enabled { get; /* RVA: 0x0B468124 */ set; /* RVA: 0x0B468174 */ }

        // Methods
        // RVA: 0x031C0310  token: 0x6000149
        public static System.Void CullBatched(System.Collections.Generic.IList<UnityEngine.UI.IClipper> clippers) { }
        // RVA: 0x0404D2D0  token: 0x600014A
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000026  // size: 0x10
    public static class HGImageBatchHelper
    {
        // Fields
        private static System.Boolean <Enabled>k__BackingField;  // static @ 0x0
        private static System.Int32 kMaxBatchImageCount;  // const
        private static Unity.Collections.NativeArray<UnityEngine.UI.HGImageBatchBindings.HGBatchImageInput> s_BatchBuffer;  // static @ 0x8
        private static System.Int32 s_BatchCount;  // static @ 0x18
        private static readonly Unity.Profiling.ProfilerMarker s_BatchFlushMarker;  // static @ 0x20

        // Properties
        System.Boolean Enabled { get; /* RVA: 0x0B468080 */ set; /* RVA: 0x0B4680D0 */ }

        // Methods
        // RVA: 0x031BB3F0  token: 0x600014D
        public static System.Void Enqueue(UnityEngine.UI.Image image) { }
        // RVA: 0x031BC560  token: 0x600014E
        public static System.Void Flush() { }
        // RVA: 0x031BB740  token: 0x600014F
        private static System.Void EnsureBatchBuffer() { }
        // RVA: 0x031BB290  token: 0x6000150
        public static System.Void FillHGImageData(UnityEngine.UI.Image image, UnityEngine.CanvasRenderer.HGImageData& data) { }
        // RVA: 0x04D30690  token: 0x6000151
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000027
    public interface IGraphicEnabledDisabled
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000152
        public virtual System.Void OnSiblingGraphicEnabledDisabled() { }

    }

    // TypeToken: 0x2000028  // size: 0x190
    public class Image : UnityEngine.UI.MaskableGraphic, UnityEngine.ISerializationCallbackReceiver, UnityEngine.UI.ILayoutElement, UnityEngine.ICanvasRaycastFilter
    {
        // Fields
        protected static UnityEngine.Material s_ETC1DefaultUI;  // static @ 0x0
        private UnityEngine.Sprite m_Sprite;  // 0xe8
        protected UnityEngine.Rect m_RuntimeAtlasRect;  // 0xf0
        protected UnityEngine.Rect m_RuntimeAtlasTextureRect;  // 0x100
        protected UnityEngine.Vector4 m_RuntimeAtlasBorder;  // 0x110
        protected System.Single m_RuntimeAtlasPixelsPerUnit;  // 0x120
        private UnityEngine.Sprite m_OverrideSprite;  // 0x128
        private UnityEngine.UI.Image.Type m_Type;  // 0x130
        private System.Boolean m_PreserveAspect;  // 0x134
        private System.Boolean m_FillCenter;  // 0x135
        private UnityEngine.UI.Image.FillMethod m_FillMethod;  // 0x138
        private System.Single m_FillAmount;  // 0x13c
        private System.Boolean m_FillClockwise;  // 0x140
        private System.Int32 m_FillOrigin;  // 0x144
        private System.Single m_AlphaHitTestMinimumThreshold;  // 0x148
        private System.Boolean m_Tracked;  // 0x14c
        private System.Boolean m_UseSpriteMesh;  // 0x14d
        private System.Single m_PixelsPerUnitMultiplier;  // 0x150
        private System.Single m_CachedReferencePixelsPerUnit;  // 0x154
        public UnityEngine.UI.Image.SlicedSpriteParams slicedSpriteParams;  // 0x158
        private System.Boolean bAwakeOnce;  // 0x17c
        private static readonly UnityEngine.Vector2[] s_VertScratch;  // static @ 0x8
        private static readonly UnityEngine.Vector2[] s_UVScratch;  // static @ 0x10
        private static System.Int32 QuadCount;  // const
        private static readonly UnityEngine.Vector3[] s_Xy;  // static @ 0x18
        private static readonly UnityEngine.Vector3[] s_Uv;  // static @ 0x20
        private static System.Collections.Generic.List<UnityEngine.UI.Image> m_TrackedTexturelessImages;  // static @ 0x28
        private static System.Boolean s_Initialized;  // static @ 0x30
        protected static System.Int32 VALIDATE_MAX_VERT_COUNT;  // const
        private UnityEngine.UI.Image.EDynamicAtlasActiveMode m_DynamicAtlasActiveMode;  // 0x17d
        protected System.Boolean _packIntoRuntimeAtlas;  // 0x17e
        protected System.Int32 m_panelLevel;  // 0x180
        public System.Boolean keepOriginUV;  // 0x184
        private System.Action dynamicAtlasCallBack;  // 0x188
        private static System.Boolean _sDynamicAtlasInitialized;  // static @ 0x31
        private static readonly System.Collections.Generic.List<UnityEngine.UI.Image> s_FreeList;  // static @ 0x38
        private static readonly System.Collections.Generic.HashSet<UnityEngine.UI.Image> s_TrackedDynamicAtlasImages;  // static @ 0x40
        private static readonly System.Collections.Generic.List<UnityEngine.UI.Image> s_FreeListRevert;  // static @ 0x48
        private static readonly System.Collections.Generic.HashSet<UnityEngine.UI.Image> s_TrackedDynamicAtlasSpriteRevert;  // static @ 0x50
        public static System.Boolean s_AtlasV2;  // static @ 0x58

        // Properties
        System.Boolean hasCullOverride { get; /* RVA: 0x012081B0 */ }
        UnityEngine.Rect runtimeAtlasRect { get; /* RVA: 0x04DA1270 */ set; /* RVA: 0x04DA1320 */ }
        UnityEngine.Rect runtimeAtlasTextureRect { get; /* RVA: 0x04DA1250 */ set; /* RVA: 0x04DA12F0 */ }
        UnityEngine.Vector4 runtimeAtlasBorder { get; /* RVA: 0x04DA1240 */ set; /* RVA: 0x04DA12E0 */ }
        System.Single runtimeAtlasPixelsPerUnit { get; /* RVA: 0x04DBD7E0 */ set; /* RVA: 0x04DBD820 */ }
        UnityEngine.Sprite sprite { get; /* RVA: 0x04D88590 */ set; /* RVA: 0x031EA420 */ }
        System.Boolean enableRuntimeAtlasRaw { get; /* RVA: 0x04D86C50 */ }
        System.Boolean enableRuntimeAtlas { get; /* RVA: 0x03C04DB0 */ set; /* RVA: 0x0B467FC4 */ }
        UnityEngine.Sprite overrideSprite { get; /* RVA: 0x053914BC */ set; /* RVA: 0x0B46B824 */ }
        UnityEngine.Sprite activeSprite { get; /* RVA: 0x02FC95E0 */ }
        UnityEngine.UI.Image.Type type { get; /* RVA: 0x04D8D520 */ set; /* RVA: 0x0B46B910 */ }
        System.Boolean preserveAspect { get; /* RVA: 0x04DA7910 */ set; /* RVA: 0x0B46B8B0 */ }
        System.Boolean fillCenter { get; /* RVA: 0x04DBD7C0 */ set; /* RVA: 0x0B46B6A4 */ }
        UnityEngine.UI.Image.FillMethod fillMethod { get; /* RVA: 0x04D8D0B0 */ set; /* RVA: 0x0B46B764 */ }
        System.Single fillAmount { get; /* RVA: 0x04DA7940 */ set; /* RVA: 0x02F54FD0 */ }
        System.Boolean fillClockwise { get; /* RVA: 0x04D8E4D0 */ set; /* RVA: 0x0B46B704 */ }
        System.Int32 fillOrigin { get; /* RVA: 0x04DBD7D0 */ set; /* RVA: 0x0B46B7C8 */ }
        System.Single eventAlphaThreshold { get; /* RVA: 0x04DBD7A0 */ set; /* RVA: 0x04DBD800 */ }
        System.Single alphaHitTestMinimumThreshold { get; /* RVA: 0x04D90E70 */ set; /* RVA: 0x04D90E80 */ }
        System.Boolean useSpriteMesh { get; /* RVA: 0x04DBD7F0 */ set; /* RVA: 0x0B46B96C */ }
        UnityEngine.Material defaultETC1GraphicMaterial { get; /* RVA: 0x0B46B5DC */ }
        UnityEngine.Texture mainTexture { get; /* RVA: 0x031EDF80 */ }
        System.Boolean hasBorder { get; /* RVA: 0x031CE170 */ }
        System.Single pixelsPerUnitMultiplier { get; /* RVA: 0x04DA7AE0 */ set; /* RVA: 0x0B46B88C */ }
        System.Single pixelsPerUnit { get; /* RVA: 0x031C4370 */ }
        System.Single multipliedPixelsPerUnit { get; /* RVA: 0x031CD2B0 */ }
        UnityEngine.Material material { get; /* RVA: 0x031BCAB0 */ set; /* RVA: 0x0425D110 */ }
        System.Single minWidth { get; /* RVA: 0x04D879D0 */ }
        System.Single preferredWidth { get; /* RVA: 0x031E9C70 */ }
        System.Single flexibleWidth { get; /* RVA: 0x04DA9E10 */ }
        System.Single minHeight { get; /* RVA: 0x04D879D0 */ }
        System.Single preferredHeight { get; /* RVA: 0x031E99C0 */ }
        System.Single flexibleHeight { get; /* RVA: 0x04DA9E10 */ }
        System.Int32 layoutPriority { get; /* RVA: 0x011EC580 */ }
        UnityEngine.UI.Image.EDynamicAtlasActiveMode DynamicAtlasActiveMode { get; /* RVA: 0x04DBD790 */ }

        // Methods
        // RVA: 0x04695BF0  token: 0x6000161
        public virtual System.Void ReleaseSprite() { }
        // RVA: 0x03093960  token: 0x6000162
        protected virtual System.Void _OnSpriteChanged(UnityEngine.Sprite oldOne) { }
        // RVA: 0x04DBD780  token: 0x6000163
        public System.Void DisableSpriteOptimizations() { }
        // RVA: 0x03696E30  token: 0x600017B
        protected System.Void .ctor() { }
        // RVA: 0x041E1670  token: 0x6000185
        public virtual System.Void OnBeforeSerialize() { }
        // RVA: 0x03C82850  token: 0x6000186
        public virtual System.Void OnAfterDeserialize() { }
        // RVA: 0x0B46ACF0  token: 0x6000187
        private System.Void PreserveSpriteAspectRatio(UnityEngine.Rect& rect, UnityEngine.Vector2 spriteSize) { }
        // RVA: 0x031E8D40  token: 0x6000188
        private UnityEngine.Vector4 GetDrawingDimensions(System.Boolean shouldPreserveAspect) { }
        // RVA: 0x031EA1B0  token: 0x6000189
        public virtual System.Void SetNativeSize() { }
        // RVA: 0x031E85F0  token: 0x600018A
        protected virtual System.Void OnPopulateMesh(UnityEngine.UI.VertexHelper toFill) { }
        // RVA: 0x031ED7C0  token: 0x600018B
        private System.Void TrackSprite() { }
        // RVA: 0x031EBCB0  token: 0x600018C
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0351A2F0  token: 0x600018D
        protected virtual System.Void OnDisable() { }
        // RVA: 0x03093B40  token: 0x600018E
        protected virtual System.Void Awake() { }
        // RVA: 0x03094110  token: 0x600018F
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x031BD110  token: 0x6000190
        protected virtual System.Void UpdateMaterial() { }
        // RVA: 0x031C3A40  token: 0x6000191
        protected virtual System.Void OnCanvasHierarchyChanged() { }
        // RVA: 0x031E87D0  token: 0x6000192
        private System.Void GenerateSimpleSprite(UnityEngine.UI.VertexHelper vh, System.Boolean lPreserveAspect) { }
        // RVA: 0x0B46A364  token: 0x6000193
        private System.Void GenerateSprite(UnityEngine.UI.VertexHelper vh, System.Boolean lPreserveAspect) { }
        // RVA: 0x031CC8E0  token: 0x6000194
        private System.Void GenerateSlicedSprite(UnityEngine.UI.VertexHelper toFill) { }
        // RVA: 0x031CECC0  token: 0x6000195
        private System.Void GenerateTiledSprite(UnityEngine.UI.VertexHelper toFill) { }
        // RVA: 0x0B469DFC  token: 0x6000196
        private System.Void GenerateMirrorSprite(UnityEngine.UI.VertexHelper toFill) { }
        // RVA: 0x0B469468  token: 0x6000197
        private System.Void GenerateMirrorQuarter(UnityEngine.UI.VertexHelper toFill) { }
        // RVA: 0x0B4681C8  token: 0x6000198
        private static System.Void AddQuad(UnityEngine.UI.VertexHelper vertexHelper, UnityEngine.Vector3[] quadPositions, UnityEngine.Color32 color, UnityEngine.Vector3[] quadUVs) { }
        // RVA: 0x032407C0  token: 0x6000199
        private static System.Void AddQuad(UnityEngine.UI.VertexHelper vertexHelper, UnityEngine.Vector2 posMin, UnityEngine.Vector2 posMax, UnityEngine.Color32 color, UnityEngine.Vector2 uvMin, UnityEngine.Vector2 uvMax) { }
        // RVA: 0x031CE290  token: 0x600019A
        private UnityEngine.Vector4 GetAdjustedBorders(UnityEngine.Vector4 border, UnityEngine.Rect adjustedRect) { }
        // RVA: 0x0B4682EC  token: 0x600019B
        private System.Void GenerateFilledSprite(UnityEngine.UI.VertexHelper toFill, System.Boolean preserveAspect) { }
        // RVA: 0x0B46ADD8  token: 0x600019C
        private static System.Boolean RadialCut(UnityEngine.Vector3[] xy, UnityEngine.Vector3[] uv, System.Single fill, System.Boolean invert, System.Int32 corner) { }
        // RVA: 0x0B46AF08  token: 0x600019D
        private static System.Void RadialCut(UnityEngine.Vector3[] xy, System.Single cos, System.Single sin, System.Boolean invert, System.Int32 corner) { }
        // RVA: 0x041E1670  token: 0x600019E
        public virtual System.Void CalculateLayoutInputHorizontal() { }
        // RVA: 0x041E1670  token: 0x600019F
        public virtual System.Void CalculateLayoutInputVertical() { }
        // RVA: 0x03093020  token: 0x60001A7
        public virtual System.Boolean IsRaycastLocationValid(UnityEngine.Vector2 screenPoint, UnityEngine.Camera eventCamera) { }
        // RVA: 0x03093540  token: 0x60001A8
        private UnityEngine.Vector2 MapCoordinate(UnityEngine.Vector2 local, UnityEngine.Rect rect) { }
        // RVA: 0x0B46B2E4  token: 0x60001A9
        private static System.Void RebuildImage(UnityEngine.U2D.SpriteAtlas spriteAtlas) { }
        // RVA: 0x0B46B46C  token: 0x60001AA
        private static System.Void TrackImage(UnityEngine.UI.Image g) { }
        // RVA: 0x0B46B56C  token: 0x60001AB
        private static System.Void UnTrackImage(UnityEngine.UI.Image g) { }
        // RVA: 0x03E51A10  token: 0x60001AC
        protected virtual System.Void OnDidApplyAnimationProperties() { }
        // RVA: 0x0B46AC48  token: 0x60001AD
        private UnityEngine.Vector4 GetRuntimeAtlasSpritePadding() { }
        // RVA: 0x0B46AB3C  token: 0x60001AE
        private UnityEngine.Vector4 GetRuntimeAtlasSpriteOuterUV() { }
        // RVA: 0x0B46A910  token: 0x60001AF
        private UnityEngine.Vector4 GetRuntimeAtlasSpriteInnerUV() { }
        // RVA: 0x0B46A7E4  token: 0x60001B0
        public System.String GetParticlePath() { }
        // RVA: 0x03093BA0  token: 0x60001B2
        public System.Void RegisterDynamicAtlas() { }
        // RVA: 0x031BC430  token: 0x60001B3
        public System.Void UnRegisterDynamicAtlas(UnityEngine.Sprite _sprite) { }
        // RVA: 0x0309A1A0  token: 0x60001B4
        public System.Void RegisterDynamicAtlasCallBack(System.Action cb) { }
        // RVA: 0x0309C080  token: 0x60001B5
        public System.Void UnRegisterDynamicAtlasCallBack(System.Action cb) { }
        // RVA: 0x031BB860  token: 0x60001B6
        private static System.Void _CanvasRreWillRenderCanvases() { }
        // RVA: 0x048B4620  token: 0x60001B7
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000032
    public interface IMask
    {
        // Properties
        UnityEngine.RectTransform rectTransform { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x60001B9
        public virtual System.Boolean Enabled() { }

    }

    // TypeToken: 0x2000033
    public interface IMaskable
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60001BB
        public virtual System.Void RecalculateMasking() { }

    }

    // TypeToken: 0x2000034  // size: 0x2A8
    public class InputField : UnityEngine.UI.Selectable, UnityEngine.EventSystems.IUpdateSelectedHandler, UnityEngine.EventSystems.IEventSystemHandler, UnityEngine.EventSystems.IBeginDragHandler, UnityEngine.EventSystems.IDragHandler, UnityEngine.EventSystems.IEndDragHandler, UnityEngine.EventSystems.IPointerClickHandler, UnityEngine.EventSystems.ISubmitHandler, UnityEngine.UI.ICanvasElement, UnityEngine.UI.ILayoutElement
    {
        // Fields
        protected UnityEngine.TouchScreenKeyboard m_Keyboard;  // 0x188
        private static readonly System.Char[] kSeparators;  // static @ 0x0
        private static System.Boolean s_IsQuestDevice;  // static @ 0x8
        protected UnityEngine.UI.Text m_TextComponent;  // 0x190
        protected UnityEngine.UI.Graphic m_Placeholder;  // 0x198
        private UnityEngine.UI.InputField.ContentType m_ContentType;  // 0x1a0
        private UnityEngine.UI.InputField.InputType m_InputType;  // 0x1a4
        private System.Char m_AsteriskChar;  // 0x1a8
        private UnityEngine.TouchScreenKeyboardType m_KeyboardType;  // 0x1ac
        private UnityEngine.UI.InputField.LineType m_LineType;  // 0x1b0
        private System.Boolean m_HideMobileInput;  // 0x1b4
        private UnityEngine.UI.InputField.CharacterValidation m_CharacterValidation;  // 0x1b8
        private System.Int32 m_CharacterLimit;  // 0x1bc
        private UnityEngine.UI.InputField.SubmitEvent m_OnSubmit;  // 0x1c0
        private UnityEngine.UI.InputField.EndEditEvent m_OnDidEndEdit;  // 0x1c8
        private UnityEngine.UI.InputField.OnChangeEvent m_OnValueChanged;  // 0x1d0
        private UnityEngine.UI.InputField.OnValidateInput m_OnValidateInput;  // 0x1d8
        private UnityEngine.Color m_CaretColor;  // 0x1e0
        private System.Boolean m_CustomCaretColor;  // 0x1f0
        private UnityEngine.Color m_SelectionColor;  // 0x1f4
        protected System.String m_Text;  // 0x208
        private System.Single m_CaretBlinkRate;  // 0x210
        private System.Int32 m_CaretWidth;  // 0x214
        private System.Boolean m_ReadOnly;  // 0x218
        private System.Boolean m_ShouldActivateOnSelect;  // 0x219
        protected System.Int32 m_CaretPosition;  // 0x21c
        protected System.Int32 m_CaretSelectPosition;  // 0x220
        private UnityEngine.RectTransform caretRectTrans;  // 0x228
        protected UnityEngine.UIVertex[] m_CursorVerts;  // 0x230
        private UnityEngine.TextGenerator m_InputTextCache;  // 0x238
        private UnityEngine.CanvasRenderer m_CachedInputRenderer;  // 0x240
        private System.Boolean m_PreventFontCallback;  // 0x248
        protected UnityEngine.Mesh m_Mesh;  // 0x250
        private System.Boolean m_AllowInput;  // 0x258
        private System.Boolean m_ShouldActivateNextUpdate;  // 0x259
        private System.Boolean m_UpdateDrag;  // 0x25a
        private System.Boolean m_DragPositionOutOfBounds;  // 0x25b
        private static System.Single kHScrollSpeed;  // const
        private static System.Single kVScrollSpeed;  // const
        protected System.Boolean m_CaretVisible;  // 0x25c
        private UnityEngine.Coroutine m_BlinkCoroutine;  // 0x260
        private System.Single m_BlinkStartTime;  // 0x268
        protected System.Int32 m_DrawStart;  // 0x26c
        protected System.Int32 m_DrawEnd;  // 0x270
        private UnityEngine.Coroutine m_DragCoroutine;  // 0x278
        private System.String m_OriginalText;  // 0x280
        private System.Boolean m_WasCanceled;  // 0x288
        private System.Boolean m_HasDoneFocusTransition;  // 0x289
        private UnityEngine.WaitForSecondsRealtime m_WaitForSecondsRealtime;  // 0x290
        private System.Boolean m_TouchKeyboardAllowsInPlaceEditing;  // 0x298
        private System.Boolean m_IsCompositionActive;  // 0x299
        private static System.String kEmailSpecialCharacters;  // const
        private static System.String kOculusQuestDeviceModel;  // const
        private UnityEngine.Event m_ProcessingEvent;  // 0x2a0
        private static System.Int32 k_MaxTextLength;  // const

        // Properties
        UnityEngine.EventSystems.BaseInput input { get; /* RVA: 0x0B570BE8 */ }
        System.String compositionString { get; /* RVA: 0x0B570B2C */ }
        UnityEngine.Mesh mesh { get; /* RVA: 0x0B570CB8 */ }
        UnityEngine.TextGenerator cachedInputTextGenerator { get; /* RVA: 0x0B570A08 */ }
        System.Boolean shouldHideMobileInput { get; /* RVA: 0x0B5710A4 */ set; /* RVA: 0x0B5717B0 */ }
        System.Boolean shouldActivateOnSelect { get; /* RVA: 0x0B571080 */ set; /* RVA: 0x04DBEFA0 */ }
        System.String text { get; /* RVA: 0x04D8D8C0 */ set; /* RVA: 0x0B571AB8 */ }
        System.Boolean isFocused { get; /* RVA: 0x04D8FD90 */ }
        System.Single caretBlinkRate { get; /* RVA: 0x04D928F0 */ set; /* RVA: 0x0B571130 */ }
        System.Int32 caretWidth { get; /* RVA: 0x04DB9DC0 */ set; /* RVA: 0x0B571238 */ }
        UnityEngine.UI.Text textComponent { get; /* RVA: 0x04D8DD50 */ set; /* RVA: 0x0B5717FC */ }
        UnityEngine.UI.Graphic placeholder { get; /* RVA: 0x04D8DED0 */ set; /* RVA: 0x0B57168C */ }
        UnityEngine.Color caretColor { get; /* RVA: 0x0B570A84 */ set; /* RVA: 0x0B571198 */ }
        System.Boolean customCaretColor { get; /* RVA: 0x04DBEF70 */ set; /* RVA: 0x0B5713E0 */ }
        UnityEngine.Color selectionColor { get; /* RVA: 0x04DBEF80 */ set; /* RVA: 0x0B571724 */ }
        UnityEngine.UI.InputField.EndEditEvent onEndEdit { get; /* RVA: 0x04D8D790 */ set; /* RVA: 0x0B571554 */ }
        UnityEngine.UI.InputField.SubmitEvent onSubmit { get; /* RVA: 0x04D8DA80 */ set; /* RVA: 0x0B5715A0 */ }
        UnityEngine.UI.InputField.OnChangeEvent onValueChange { get; /* RVA: 0x04D8D7B0 */ set; /* RVA: 0x0B571638 */ }
        UnityEngine.UI.InputField.OnChangeEvent onValueChanged { get; /* RVA: 0x04D8D7B0 */ set; /* RVA: 0x0B571640 */ }
        UnityEngine.UI.InputField.OnValidateInput onValidateInput { get; /* RVA: 0x04D87870 */ set; /* RVA: 0x0B5715EC */ }
        System.Int32 characterLimit { get; /* RVA: 0x04D91080 */ set; /* RVA: 0x0B571294 */ }
        UnityEngine.UI.InputField.ContentType contentType { get; /* RVA: 0x04DA3200 */ set; /* RVA: 0x0B571384 */ }
        UnityEngine.UI.InputField.LineType lineType { get; /* RVA: 0x04D8E930 */ set; /* RVA: 0x0B5714B0 */ }
        UnityEngine.UI.InputField.InputType inputType { get; /* RVA: 0x04DA7C40 */ set; /* RVA: 0x0B5713F8 */ }
        UnityEngine.TouchScreenKeyboard touchScreenKeyboard { get; /* RVA: 0x04D8DA20 */ }
        UnityEngine.TouchScreenKeyboardType keyboardType { get; /* RVA: 0x04D90790 */ set; /* RVA: 0x0B571454 */ }
        UnityEngine.UI.InputField.CharacterValidation characterValidation { get; /* RVA: 0x04D8EAC0 */ set; /* RVA: 0x0B571328 */ }
        System.Boolean readOnly { get; /* RVA: 0x04D8EDA0 */ set; /* RVA: 0x04D8EDB0 */ }
        System.Boolean multiLine { get; /* RVA: 0x0B570D5C */ }
        System.Char asteriskChar { get; /* RVA: 0x04DBEF60 */ set; /* RVA: 0x0B5710D4 */ }
        System.Boolean wasCanceled { get; /* RVA: 0x04DBEF90 */ }
        System.Int32 caretPositionInternal { get; /* RVA: 0x0B570AD4 */ set; /* RVA: 0x0B5711D8 */ }
        System.Int32 caretSelectPositionInternal { get; /* RVA: 0x0B570B00 */ set; /* RVA: 0x0B571220 */ }
        System.Boolean hasSelection { get; /* RVA: 0x0B570BB8 */ }
        System.Int32 caretPosition { get; /* RVA: 0x0B570B00 */ set; /* RVA: 0x0B5711F0 */ }
        System.Int32 selectionAnchorPosition { get; /* RVA: 0x0B570AD4 */ set; /* RVA: 0x0B5716D8 */ }
        System.Int32 selectionFocusPosition { get; /* RVA: 0x0B570B00 */ set; /* RVA: 0x0B571764 */ }
        System.String clipboard { get; /* RVA: 0x0B31DF8C */ set; /* RVA: 0x0B31E494 */ }
        System.Single minWidth { get; /* RVA: 0x04D879D0 */ }
        System.Single preferredWidth { get; /* RVA: 0x0B570F20 */ }
        System.Single flexibleWidth { get; /* RVA: 0x04DA9E10 */ }
        System.Single minHeight { get; /* RVA: 0x04D879D0 */ }
        System.Single preferredHeight { get; /* RVA: 0x0B570D74 */ }
        System.Single flexibleHeight { get; /* RVA: 0x04DA9E10 */ }
        System.Int32 layoutPriority { get; /* RVA: 0x04D85B80 */ }

        // Methods
        // RVA: 0x0B570814  token: 0x60001BE
        protected System.Void .ctor() { }
        // RVA: 0x0B56F2F8  token: 0x60001C7
        public System.Void SetTextWithoutNotify(System.String input) { }
        // RVA: 0x0B56F304  token: 0x60001C8
        private System.Void SetText(System.String value, System.Boolean sendCallback) { }
        // RVA: 0x0B56A0E8  token: 0x60001F5
        protected System.Void ClampPos(System.Int32& pos) { }
        // RVA: 0x0B56DF84  token: 0x6000201
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0B56DB80  token: 0x6000202
        protected virtual System.Void OnDisable() { }
        // RVA: 0x0B56A08C  token: 0x6000203
        private System.Collections.IEnumerator CaretBlink() { }
        // RVA: 0x0B56ECE4  token: 0x6000204
        private System.Void SetCaretVisible() { }
        // RVA: 0x0B56EC7C  token: 0x6000205
        private System.Void SetCaretActive() { }
        // RVA: 0x0B56F7F0  token: 0x6000206
        private System.Void UpdateCaretMaterial() { }
        // RVA: 0x0B56E394  token: 0x6000207
        protected System.Void OnFocus() { }
        // RVA: 0x0B56EAD4  token: 0x6000208
        protected System.Void SelectAll() { }
        // RVA: 0x0B56D9C4  token: 0x6000209
        public System.Void MoveTextEnd(System.Boolean shift) { }
        // RVA: 0x0B56DA18  token: 0x600020A
        public System.Void MoveTextStart(System.Boolean shift) { }
        // RVA: 0x0B56F6E0  token: 0x600020D
        private System.Boolean TouchScreenKeyboardShouldBeUsed() { }
        // RVA: 0x0B56BEA0  token: 0x600020E
        private System.Boolean InPlaceEditing() { }
        // RVA: 0x0B56BE44  token: 0x600020F
        private System.Boolean InPlaceEditingChanged() { }
        // RVA: 0x0B56F740  token: 0x6000210
        private System.Void UpdateCaretFromKeyboard() { }
        // RVA: 0x0B56C46C  token: 0x6000211
        protected virtual System.Void LateUpdate() { }
        // RVA: 0x0B56E804  token: 0x6000212
        public UnityEngine.Vector2 ScreenToLocal(UnityEngine.Vector2 screen) { }
        // RVA: 0x0B56BCE0  token: 0x6000213
        private System.Int32 GetUnclampedCharacterLineFromPosition(UnityEngine.Vector2 pos, UnityEngine.TextGenerator generator) { }
        // RVA: 0x0B56B8F8  token: 0x6000214
        protected System.Int32 GetCharacterIndexFromPosition(UnityEngine.Vector2 pos) { }
        // RVA: 0x0B56D65C  token: 0x6000215
        private System.Boolean MayDrag(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B56DB34  token: 0x6000216
        public virtual System.Void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B56DDCC  token: 0x6000217
        public virtual System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B56D708  token: 0x6000218
        private System.Collections.IEnumerator MouseDragOutsideRect(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B56E1F4  token: 0x6000219
        public virtual System.Void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B56E3C4  token: 0x600021A
        public virtual System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B56C098  token: 0x600021B
        protected UnityEngine.UI.InputField.EditState KeyPressed(UnityEngine.Event evt) { }
        // RVA: 0x0B56C038  token: 0x600021C
        private System.Boolean IsValidChar(System.Char c) { }
        // RVA: 0x0B56E7EC  token: 0x600021D
        public System.Void ProcessEvent(UnityEngine.Event e) { }
        // RVA: 0x0B56E634  token: 0x600021E
        public virtual System.Void OnUpdateSelected(UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0B56BC54  token: 0x600021F
        private System.String GetSelectedString() { }
        // RVA: 0x0B56A874  token: 0x6000220
        private System.Int32 FindtNextWordBegin() { }
        // RVA: 0x0B56D918  token: 0x6000221
        private System.Void MoveRight(System.Boolean shift, System.Boolean ctrl) { }
        // RVA: 0x0B56A930  token: 0x6000222
        private System.Int32 FindtPrevWordBegin() { }
        // RVA: 0x0B56D86C  token: 0x6000223
        private System.Void MoveLeft(System.Boolean shift, System.Boolean ctrl) { }
        // RVA: 0x0B56A5F0  token: 0x6000224
        private System.Int32 DetermineCharacterLine(System.Int32 charPos, UnityEngine.TextGenerator generator) { }
        // RVA: 0x0B56D408  token: 0x6000225
        private System.Int32 LineUpCharacterPosition(System.Int32 originalPos, System.Boolean goToFirstChar) { }
        // RVA: 0x0B56D218  token: 0x6000226
        private System.Int32 LineDownCharacterPosition(System.Int32 originalPos, System.Boolean goToLastChar) { }
        // RVA: 0x0B56D860  token: 0x6000227
        private System.Void MoveDown(System.Boolean shift) { }
        // RVA: 0x0B56D77C  token: 0x6000228
        private System.Void MoveDown(System.Boolean shift, System.Boolean goToLastChar) { }
        // RVA: 0x0B56DA58  token: 0x6000229
        private System.Void MoveUp(System.Boolean shift) { }
        // RVA: 0x0B56DA64  token: 0x600022A
        private System.Void MoveUp(System.Boolean shift, System.Boolean goToFirstChar) { }
        // RVA: 0x0B56A418  token: 0x600022B
        private System.Void Delete() { }
        // RVA: 0x0B56A9EC  token: 0x600022C
        private System.Void ForwardSpace() { }
        // RVA: 0x0B569FAC  token: 0x600022D
        private System.Void Backspace() { }
        // RVA: 0x0B56BEC4  token: 0x600022E
        private System.Void Insert(System.Char c) { }
        // RVA: 0x0B5700F4  token: 0x600022F
        private System.Void UpdateTouchKeyboardFromEditChanges() { }
        // RVA: 0x0B56EBEC  token: 0x6000230
        private System.Void SendOnValueChangedAndUpdateLabel() { }
        // RVA: 0x0B56EC0C  token: 0x6000231
        private System.Void SendOnValueChanged() { }
        // RVA: 0x0B56EB0C  token: 0x6000232
        protected System.Void SendOnEndEdit() { }
        // RVA: 0x0B56EB7C  token: 0x6000233
        protected System.Void SendOnSubmit() { }
        // RVA: 0x0B569814  token: 0x6000234
        protected virtual System.Void Append(System.String input) { }
        // RVA: 0x0B569594  token: 0x6000235
        protected virtual System.Void Append(System.Char input) { }
        // RVA: 0x0B56FCC0  token: 0x6000236
        protected System.Void UpdateLabel() { }
        // RVA: 0x0B56BFCC  token: 0x6000237
        private System.Boolean IsSelectionVisible() { }
        // RVA: 0x0B56BBAC  token: 0x6000238
        private static System.Int32 GetLineStartPosition(UnityEngine.TextGenerator gen, System.Int32 line) { }
        // RVA: 0x0B56BAF0  token: 0x6000239
        private static System.Int32 GetLineEndPosition(UnityEngine.TextGenerator gen, System.Int32 line) { }
        // RVA: 0x0B56ED24  token: 0x600023A
        private System.Void SetDrawRangeToContainCaretPosition(System.Int32 caretPos) { }
        // RVA: 0x0B56A9E4  token: 0x600023B
        public System.Void ForceLabelUpdate() { }
        // RVA: 0x0B56D610  token: 0x600023C
        private System.Void MarkGeometryAsDirty() { }
        // RVA: 0x0B56E7F4  token: 0x600023D
        public virtual System.Void Rebuild(UnityEngine.UI.CanvasUpdate update) { }
        // RVA: 0x041E1670  token: 0x600023E
        public virtual System.Void LayoutComplete() { }
        // RVA: 0x041E1670  token: 0x600023F
        public virtual System.Void GraphicUpdateComplete() { }
        // RVA: 0x0B56F8D4  token: 0x6000240
        private System.Void UpdateGeometry() { }
        // RVA: 0x0B56989C  token: 0x6000241
        private System.Void AssignPositioningIfNeeded() { }
        // RVA: 0x0B56E218  token: 0x6000242
        private System.Void OnFillVBO(UnityEngine.Mesh vbo) { }
        // RVA: 0x0B56AA88  token: 0x6000243
        private System.Void GenerateCaret(UnityEngine.UI.VertexHelper vbo, UnityEngine.Vector2 roundingOffset) { }
        // RVA: 0x0B56A11C  token: 0x6000244
        private System.Void CreateCursorVerts() { }
        // RVA: 0x0B56B148  token: 0x6000245
        private System.Void GenerateHighlight(UnityEngine.UI.VertexHelper vbo, UnityEngine.Vector2 roundingOffset) { }
        // RVA: 0x0B57013C  token: 0x6000246
        protected System.Char Validate(System.String text, System.Int32 pos, System.Char ch) { }
        // RVA: 0x0B56947C  token: 0x6000247
        public System.Void ActivateInputField() { }
        // RVA: 0x0B569180  token: 0x6000248
        private System.Void ActivateInputFieldInternal() { }
        // RVA: 0x0B56E5B4  token: 0x6000249
        public virtual System.Void OnSelect(UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0B56E39C  token: 0x600024A
        public virtual System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B56A284  token: 0x600024B
        public System.Void DeactivateInputField() { }
        // RVA: 0x0B56DB58  token: 0x600024C
        public virtual System.Void OnDeselect(UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0B56E5F0  token: 0x600024D
        public virtual System.Void OnSubmit(UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0B56A6B0  token: 0x600024E
        private System.Void EnforceContentType() { }
        // RVA: 0x0B56A7E0  token: 0x600024F
        private System.Void EnforceTextHOverflow() { }
        // RVA: 0x0B56F678  token: 0x6000250
        private System.Void SetToCustomIfContentTypeIsNot(UnityEngine.UI.InputField.ContentType[] allowedContentTypes) { }
        // RVA: 0x0B56F6C8  token: 0x6000251
        private System.Void SetToCustom() { }
        // RVA: 0x0B56A68C  token: 0x6000252
        protected virtual System.Void DoStateTransition(UnityEngine.UI.Selectable.SelectionState state, System.Boolean instant) { }
        // RVA: 0x041E1670  token: 0x6000253
        public virtual System.Void CalculateLayoutInputHorizontal() { }
        // RVA: 0x041E1670  token: 0x6000254
        public virtual System.Void CalculateLayoutInputVertical() { }
        // RVA: 0x0B570778  token: 0x600025C
        private static System.Void .cctor() { }
        // RVA: 0x053953F4  token: 0x600025D
        private virtual UnityEngine.Transform UnityEngine.UI.ICanvasElement.get_transform() { }

    }

    // TypeToken: 0x2000040  // size: 0x30
    public class AspectRatioFitter : UnityEngine.EventSystems.UIBehaviour, UnityEngine.UI.ILayoutSelfController, UnityEngine.UI.ILayoutController
    {
        // Fields
        private UnityEngine.UI.AspectRatioFitter.AspectMode m_AspectMode;  // 0x18
        private System.Single m_AspectRatio;  // 0x1c
        private UnityEngine.RectTransform m_Rect;  // 0x20
        private System.Boolean m_DelayedSetDirty;  // 0x28
        private System.Boolean m_DoesParentExist;  // 0x29
        private UnityEngine.DrivenRectTransformTracker m_Tracker;  // 0x2a

        // Properties
        UnityEngine.UI.AspectRatioFitter.AspectMode aspectMode { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x0B568AEC */ }
        System.Single aspectRatio { get; /* RVA: 0x04D868F0 */ set; /* RVA: 0x0B568B44 */ }
        UnityEngine.RectTransform rectTransform { get; /* RVA: 0x0449D7D0 */ }

        // Methods
        // RVA: 0x04CC8BA0  token: 0x6000276
        protected System.Void .ctor() { }
        // RVA: 0x0449D220  token: 0x6000277
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0449D1E0  token: 0x6000278
        protected virtual System.Void Start() { }
        // RVA: 0x0449D2B0  token: 0x6000279
        protected virtual System.Void OnDisable() { }
        // RVA: 0x0B568A6C  token: 0x600027A
        protected virtual System.Void OnTransformParentChanged() { }
        // RVA: 0x04703A40  token: 0x600027B
        protected virtual System.Void Update() { }
        // RVA: 0x0449D4C0  token: 0x600027C
        protected virtual System.Void OnRectTransformDimensionsChange() { }
        // RVA: 0x0449D4D0  token: 0x600027D
        private System.Void UpdateRect() { }
        // RVA: 0x0449D2F0  token: 0x600027E
        private System.Single GetSizeDeltaToProduceSize(System.Single size, System.Int32 axis) { }
        // RVA: 0x0449D3F0  token: 0x600027F
        private UnityEngine.Vector2 GetParentSize() { }
        // RVA: 0x041E1670  token: 0x6000280
        public virtual System.Void SetLayoutHorizontal() { }
        // RVA: 0x041E1670  token: 0x6000281
        public virtual System.Void SetLayoutVertical() { }
        // RVA: 0x0449D4C0  token: 0x6000282
        protected System.Void SetDirty() { }
        // RVA: 0x0449D870  token: 0x6000283
        public System.Boolean IsComponentValidOnObject() { }
        // RVA: 0x0449D210  token: 0x6000284
        public System.Boolean IsAspectModeValid() { }
        // RVA: 0x04D867B0  token: 0x6000285
        private System.Boolean DoesParentExists() { }

    }

    // TypeToken: 0x2000042  // size: 0x60
    public class CanvasScaler : UnityEngine.EventSystems.UIBehaviour
    {
        // Fields
        private UnityEngine.UI.CanvasScaler.ScaleMode m_UiScaleMode;  // 0x18
        protected System.Single m_ReferencePixelsPerUnit;  // 0x1c
        protected System.Single m_ScaleFactor;  // 0x20
        protected UnityEngine.Vector2 m_ReferenceResolution;  // 0x24
        protected UnityEngine.UI.CanvasScaler.ScreenMatchMode m_ScreenMatchMode;  // 0x2c
        protected System.Single m_MatchWidthOrHeight;  // 0x30
        private static System.Single kLogBase;  // const
        protected UnityEngine.UI.CanvasScaler.Unit m_PhysicalUnit;  // 0x34
        protected System.Single m_FallbackScreenDPI;  // 0x38
        protected System.Single m_DefaultSpriteDPI;  // 0x3c
        protected System.Single m_DynamicPixelsPerUnit;  // 0x40
        private UnityEngine.Canvas m_Canvas;  // 0x48
        private System.Single m_PrevScaleFactor;  // 0x50
        private System.Single m_PrevReferencePixelsPerUnit;  // 0x54
        protected System.Boolean m_PresetInfoIsWorld;  // 0x58

        // Properties
        UnityEngine.UI.CanvasScaler.ScaleMode uiScaleMode { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x011EF9B0 */ }
        System.Single referencePixelsPerUnit { get; /* RVA: 0x04D868F0 */ set; /* RVA: 0x04D86900 */ }
        System.Single scaleFactor { get; /* RVA: 0x011EC8A0 */ set; /* RVA: 0x0B568C58 */ }
        UnityEngine.Vector2 referenceResolution { get; /* RVA: 0x04D88060 */ set; /* RVA: 0x03C00870 */ }
        UnityEngine.UI.CanvasScaler.ScreenMatchMode screenMatchMode { get; /* RVA: 0x04D86710 */ set; /* RVA: 0x04D86720 */ }
        System.Single matchWidthOrHeight { get; /* RVA: 0x04D86E20 */ set; /* RVA: 0x04D86E30 */ }
        UnityEngine.UI.CanvasScaler.Unit physicalUnit { get; /* RVA: 0x04D86730 */ set; /* RVA: 0x04D86760 */ }
        System.Single fallbackScreenDPI { get; /* RVA: 0x04D88360 */ set; /* RVA: 0x04D88370 */ }
        System.Single defaultSpriteDPI { get; /* RVA: 0x04D863E0 */ set; /* RVA: 0x0B568C44 */ }
        System.Single dynamicPixelsPerUnit { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }

        // Methods
        // RVA: 0x044165B0  token: 0x600029A
        protected System.Void .ctor() { }
        // RVA: 0x03099A20  token: 0x600029B
        protected virtual System.Void OnEnable() { }
        // RVA: 0x02FC71A0  token: 0x600029C
        private System.Void Canvas_preWillRenderCanvases() { }
        // RVA: 0x03099830  token: 0x600029D
        protected virtual System.Void OnDisable() { }
        // RVA: 0x03BFDC20  token: 0x600029E
        public System.Void HandleManually() { }
        // RVA: 0x02FC7330  token: 0x600029F
        protected virtual System.Void Handle() { }
        // RVA: 0x030998B0  token: 0x60002A0
        protected virtual System.Void HandleWorldCanvas() { }
        // RVA: 0x04D7EDB0  token: 0x60002A1
        protected virtual System.Void HandleConstantPixelSize() { }
        // RVA: 0x02F108F0  token: 0x60002A2
        protected virtual System.Void HandleScaleWithScreenSize() { }
        // RVA: 0x0B568BA0  token: 0x60002A3
        protected virtual System.Void HandleConstantPhysicalSize() { }
        // RVA: 0x03099940  token: 0x60002A4
        protected System.Void SetScaleFactor(System.Single scaleFactor) { }
        // RVA: 0x03099910  token: 0x60002A5
        protected System.Void SetReferencePixelsPerUnit(System.Single referencePixelsPerUnit) { }

    }

    // TypeToken: 0x2000046  // size: 0x30
    public class ContentSizeFitter : UnityEngine.EventSystems.UIBehaviour, UnityEngine.UI.ILayoutSelfController, UnityEngine.UI.ILayoutController
    {
        // Fields
        protected UnityEngine.UI.ContentSizeFitter.FitMode m_HorizontalFit;  // 0x18
        protected UnityEngine.UI.ContentSizeFitter.FitMode m_VerticalFit;  // 0x1c
        private UnityEngine.RectTransform m_Rect;  // 0x20
        private UnityEngine.DrivenRectTransformTracker m_Tracker;  // 0x28

        // Properties
        UnityEngine.UI.ContentSizeFitter.FitMode horizontalFit { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x0B568C6C */ }
        UnityEngine.UI.ContentSizeFitter.FitMode verticalFit { get; /* RVA: 0x04D86310 */ set; /* RVA: 0x0B568CC4 */ }
        UnityEngine.RectTransform rectTransform { get; /* RVA: 0x031E91F0 */ }

        // Methods
        // RVA: 0x03695570  token: 0x60002AB
        protected System.Void .ctor() { }
        // RVA: 0x031E8550  token: 0x60002AC
        protected virtual System.Void OnEnable() { }
        // RVA: 0x031E8510  token: 0x60002AD
        protected virtual System.Void OnDisable() { }
        // RVA: 0x031E8550  token: 0x60002AE
        protected virtual System.Void OnRectTransformDimensionsChange() { }
        // RVA: 0x031E9160  token: 0x60002AF
        private System.Void HandleSelfFittingAlongAxis(System.Int32 axis) { }
        // RVA: 0x031E90F0  token: 0x60002B0
        public virtual System.Void SetLayoutHorizontal() { }
        // RVA: 0x053965AC  token: 0x60002B1
        public virtual System.Void SetLayoutVertical() { }
        // RVA: 0x031E8560  token: 0x60002B2
        protected System.Void SetDirty() { }

    }

    // TypeToken: 0x2000048  // size: 0x78
    public class GridLayoutGroup : UnityEngine.UI.LayoutGroup
    {
        // Fields
        protected UnityEngine.UI.GridLayoutGroup.Corner m_StartCorner;  // 0x58
        protected UnityEngine.UI.GridLayoutGroup.Axis m_StartAxis;  // 0x5c
        protected UnityEngine.Vector2 m_CellSize;  // 0x60
        protected UnityEngine.Vector2 m_Spacing;  // 0x68
        protected UnityEngine.UI.GridLayoutGroup.Constraint m_Constraint;  // 0x70
        protected System.Int32 m_ConstraintCount;  // 0x74

        // Properties
        UnityEngine.UI.GridLayoutGroup.Corner startCorner { get; /* RVA: 0x04D86570 */ set; /* RVA: 0x0B568F04 */ }
        UnityEngine.UI.GridLayoutGroup.Axis startAxis { get; /* RVA: 0x04D86560 */ set; /* RVA: 0x0B568EB8 */ }
        UnityEngine.Vector2 cellSize { get; /* RVA: 0x04DBEF20 */ set; /* RVA: 0x0B568D64 */ }
        UnityEngine.Vector2 spacing { get; /* RVA: 0x04DBEF40 */ set; /* RVA: 0x0B568E64 */ }
        UnityEngine.UI.GridLayoutGroup.Constraint constraint { get; /* RVA: 0x04D868A0 */ set; /* RVA: 0x0B568E18 */ }
        System.Int32 constraintCount { get; /* RVA: 0x04D868B0 */ set; /* RVA: 0x0B568DB8 */ }

        // Methods
        // RVA: 0x049D3A50  token: 0x60002BF
        protected System.Void .ctor() { }
        // RVA: 0x03B92180  token: 0x60002C0
        public virtual System.Void CalculateLayoutInputHorizontal() { }
        // RVA: 0x03B91B00  token: 0x60002C1
        public virtual System.Void CalculateLayoutInputVertical() { }
        // RVA: 0x0B568D58  token: 0x60002C2
        public virtual System.Void SetLayoutHorizontal() { }
        // RVA: 0x053965BC  token: 0x60002C3
        public virtual System.Void SetLayoutVertical() { }
        // RVA: 0x03B91C30  token: 0x60002C4
        private System.Void SetCellsAlongAxis(System.Int32 axis) { }

    }

    // TypeToken: 0x200004C  // size: 0x68
    public class HorizontalLayoutGroup : UnityEngine.UI.HorizontalOrVerticalLayoutGroup
    {
        // Methods
        // RVA: 0x0416C540  token: 0x60002C5
        protected System.Void .ctor() { }
        // RVA: 0x03B91900  token: 0x60002C6
        public virtual System.Void CalculateLayoutInputHorizontal() { }
        // RVA: 0x053953A0  token: 0x60002C7
        public virtual System.Void CalculateLayoutInputVertical() { }
        // RVA: 0x053915EC  token: 0x60002C8
        public virtual System.Void SetLayoutHorizontal() { }
        // RVA: 0x0539658C  token: 0x60002C9
        public virtual System.Void SetLayoutVertical() { }

    }

    // TypeToken: 0x200004D  // size: 0x68
    public abstract class HorizontalOrVerticalLayoutGroup : UnityEngine.UI.LayoutGroup
    {
        // Fields
        protected System.Single m_Spacing;  // 0x58
        protected System.Boolean m_ChildForceExpandWidth;  // 0x5c
        protected System.Boolean m_ChildForceExpandHeight;  // 0x5d
        protected System.Boolean m_ChildControlWidth;  // 0x5e
        protected System.Boolean m_ChildControlHeight;  // 0x5f
        protected System.Boolean m_ChildScaleWidth;  // 0x60
        protected System.Boolean m_ChildScaleHeight;  // 0x61
        protected System.Boolean m_ReverseArrangement;  // 0x62

        // Properties
        System.Single spacing { get; /* RVA: 0x04D88400 */ set; /* RVA: 0x0B569130 */ }
        System.Boolean childForceExpandWidth { get; /* RVA: 0x04D86D90 */ set; /* RVA: 0x045531B0 */ }
        System.Boolean childForceExpandHeight { get; /* RVA: 0x04DA5B40 */ set; /* RVA: 0x0B568FF0 */ }
        System.Boolean childControlWidth { get; /* RVA: 0x04DA5B60 */ set; /* RVA: 0x0B568FA0 */ }
        System.Boolean childControlHeight { get; /* RVA: 0x04D8E420 */ set; /* RVA: 0x0B568F50 */ }
        System.Boolean childScaleWidth { get; /* RVA: 0x04D88340 */ set; /* RVA: 0x0B569090 */ }
        System.Boolean childScaleHeight { get; /* RVA: 0x04D892D0 */ set; /* RVA: 0x0B569040 */ }
        System.Boolean reverseArrangement { get; /* RVA: 0x04D8FF20 */ set; /* RVA: 0x0B5690E0 */ }

        // Methods
        // RVA: 0x033F8790  token: 0x60002DA
        protected System.Void CalcAlongAxis(System.Int32 axis, System.Boolean isVertical) { }
        // RVA: 0x033F7B20  token: 0x60002DB
        protected System.Void SetChildrenAlongAxis(System.Int32 axis, System.Boolean isVertical) { }
        // RVA: 0x033F8D20  token: 0x60002DC
        private System.Void GetChildSizes(UnityEngine.RectTransform child, System.Int32 axis, System.Boolean controlSize, System.Boolean childForceExpand, System.Single& min, System.Single& preferred, System.Single& flexible) { }
        // RVA: 0x0416C540  token: 0x60002DD
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200004E
    public interface ILayoutElement
    {
        // Properties
        System.Single minWidth { get; /* RVA: -1  // abstract */ }
        System.Single preferredWidth { get; /* RVA: -1  // abstract */ }
        System.Single flexibleWidth { get; /* RVA: -1  // abstract */ }
        System.Single minHeight { get; /* RVA: -1  // abstract */ }
        System.Single preferredHeight { get; /* RVA: -1  // abstract */ }
        System.Single flexibleHeight { get; /* RVA: -1  // abstract */ }
        System.Int32 layoutPriority { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x60002DE
        public virtual System.Void CalculateLayoutInputHorizontal() { }
        // RVA: -1  // abstract  token: 0x60002DF
        public virtual System.Void CalculateLayoutInputVertical() { }

    }

    // TypeToken: 0x200004F
    public interface ILayoutController
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60002E7
        public virtual System.Void SetLayoutHorizontal() { }
        // RVA: -1  // abstract  token: 0x60002E8
        public virtual System.Void SetLayoutVertical() { }

    }

    // TypeToken: 0x2000050
    public interface ILayoutGroup : UnityEngine.UI.ILayoutController
    {
    }

    // TypeToken: 0x2000051
    public interface ILayoutSelfController : UnityEngine.UI.ILayoutController
    {
    }

    // TypeToken: 0x2000052
    public interface ILayoutIgnorer
    {
        // Properties
        System.Boolean ignoreLayout { get; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x2000053  // size: 0x38
    public class LayoutElement : UnityEngine.EventSystems.UIBehaviour, UnityEngine.UI.ILayoutElement, UnityEngine.UI.ILayoutIgnorer
    {
        // Fields
        private System.Boolean m_IgnoreLayout;  // 0x18
        private System.Single m_MinWidth;  // 0x1c
        private System.Single m_MinHeight;  // 0x20
        private System.Single m_PreferredWidth;  // 0x24
        private System.Single m_PreferredHeight;  // 0x28
        private System.Single m_FlexibleWidth;  // 0x2c
        private System.Single m_FlexibleHeight;  // 0x30
        private System.Int32 m_LayoutPriority;  // 0x34

        // Properties
        System.Boolean ignoreLayout { get; /* RVA: 0x04D86340 */ set; /* RVA: 0x033F1220 */ }
        System.Single minWidth { get; /* RVA: 0x04D868F0 */ set; /* RVA: 0x0B571BD4 */ }
        System.Single minHeight { get; /* RVA: 0x011EC8A0 */ set; /* RVA: 0x0B571B78 */ }
        System.Single preferredWidth { get; /* RVA: 0x04D89850 */ set; /* RVA: 0x0344CDB0 */ }
        System.Single preferredHeight { get; /* RVA: 0x04D86BF0 */ set; /* RVA: 0x0344CB70 */ }
        System.Single flexibleWidth { get; /* RVA: 0x04D86AF0 */ set; /* RVA: 0x03CD5350 */ }
        System.Single flexibleHeight { get; /* RVA: 0x04D86E20 */ set; /* RVA: 0x0B571AC4 */ }
        System.Int32 layoutPriority { get; /* RVA: 0x04D86730 */ set; /* RVA: 0x0B571B20 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60002EC
        public virtual System.Void CalculateLayoutInputHorizontal() { }
        // RVA: 0x041E1670  token: 0x60002ED
        public virtual System.Void CalculateLayoutInputVertical() { }
        // RVA: 0x03695EF0  token: 0x60002FC
        protected System.Void .ctor() { }
        // RVA: 0x0344CBD0  token: 0x60002FD
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0344CBD0  token: 0x60002FE
        protected virtual System.Void OnTransformParentChanged() { }
        // RVA: 0x0344CBD0  token: 0x60002FF
        protected virtual System.Void OnDisable() { }
        // RVA: 0x0344CBD0  token: 0x6000300
        protected virtual System.Void OnDidApplyAnimationProperties() { }
        // RVA: 0x0344CBD0  token: 0x6000301
        protected virtual System.Void OnBeforeTransformParentChanged() { }
        // RVA: 0x0344CBE0  token: 0x6000302
        protected System.Void SetDirty() { }

    }

    // TypeToken: 0x2000054  // size: 0x58
    public abstract class LayoutGroup : UnityEngine.EventSystems.UIBehaviour, UnityEngine.UI.ILayoutElement, UnityEngine.UI.ILayoutGroup, UnityEngine.UI.ILayoutController
    {
        // Fields
        protected UnityEngine.RectOffset m_Padding;  // 0x18
        protected UnityEngine.TextAnchor m_ChildAlignment;  // 0x20
        private UnityEngine.RectTransform m_Rect;  // 0x28
        protected UnityEngine.DrivenRectTransformTracker m_Tracker;  // 0x30
        private UnityEngine.Vector2 m_TotalMinSize;  // 0x34
        private UnityEngine.Vector2 m_TotalPreferredSize;  // 0x3c
        private UnityEngine.Vector2 m_TotalFlexibleSize;  // 0x44
        private System.Collections.Generic.List<UnityEngine.RectTransform> m_RectChildren;  // 0x50

        // Properties
        UnityEngine.RectOffset padding { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x049956B0 */ }
        UnityEngine.TextAnchor childAlignment { get; /* RVA: 0x02B70F90 */ set; /* RVA: 0x0B571CDC */ }
        UnityEngine.RectTransform rectTransform { get; /* RVA: 0x031F1F20 */ }
        System.Collections.Generic.List<UnityEngine.RectTransform> rectChildren { get; /* RVA: 0x04D86270 */ }
        System.Single minWidth { get; /* RVA: 0x053957B0 */ }
        System.Single preferredWidth { get; /* RVA: 0x05391670 */ }
        System.Single flexibleWidth { get; /* RVA: 0x05391650 */ }
        System.Single minHeight { get; /* RVA: 0x05391630 */ }
        System.Single preferredHeight { get; /* RVA: 0x05391640 */ }
        System.Single flexibleHeight { get; /* RVA: 0x05391660 */ }
        System.Int32 layoutPriority { get; /* RVA: 0x011EC580 */ }
        System.Boolean isRootLayoutGroup { get; /* RVA: 0x03DC3580 */ }

        // Methods
        // RVA: 0x031F1AA0  token: 0x6000309
        public virtual System.Void CalculateLayoutInputHorizontal() { }
        // RVA: -1  // abstract  token: 0x600030A
        public virtual System.Void CalculateLayoutInputVertical() { }
        // RVA: -1  // abstract  token: 0x6000312
        public virtual System.Void SetLayoutHorizontal() { }
        // RVA: -1  // abstract  token: 0x6000313
        public virtual System.Void SetLayoutVertical() { }
        // RVA: 0x03695590  token: 0x6000314
        protected System.Void .ctor() { }
        // RVA: 0x044067D0  token: 0x6000315
        protected virtual System.Void OnEnable() { }
        // RVA: 0x042561A0  token: 0x6000316
        protected virtual System.Void OnDisable() { }
        // RVA: 0x044067D0  token: 0x6000317
        protected virtual System.Void OnDidApplyAnimationProperties() { }
        // RVA: 0x0B571C3C  token: 0x6000318
        protected System.Single GetTotalMinSize(System.Int32 axis) { }
        // RVA: 0x0539241C  token: 0x6000319
        protected System.Single GetTotalPreferredSize(System.Int32 axis) { }
        // RVA: 0x0B571C30  token: 0x600031A
        protected System.Single GetTotalFlexibleSize(System.Int32 axis) { }
        // RVA: 0x033F7010  token: 0x600031B
        protected System.Single GetStartOffset(System.Int32 axis, System.Single requiredSpaceWithoutPadding) { }
        // RVA: 0x033F7270  token: 0x600031C
        protected System.Single GetAlignmentOnAxis(System.Int32 axis) { }
        // RVA: 0x033F76D0  token: 0x600031D
        protected System.Void SetLayoutInputForAxis(System.Single totalMin, System.Single totalPreferred, System.Single totalFlexible, System.Int32 axis) { }
        // RVA: 0x0B571C48  token: 0x600031E
        protected System.Void SetChildAlongAxis(UnityEngine.RectTransform rect, System.Int32 axis, System.Single pos) { }
        // RVA: 0x03C5D6D0  token: 0x600031F
        protected System.Void SetChildAlongAxisWithScale(UnityEngine.RectTransform rect, System.Int32 axis, System.Single pos, System.Single scaleFactor) { }
        // RVA: 0x03B920B0  token: 0x6000320
        protected System.Void SetChildAlongAxis(UnityEngine.RectTransform rect, System.Int32 axis, System.Single pos, System.Single size) { }
        // RVA: 0x033F7400  token: 0x6000321
        protected System.Void SetChildAlongAxisWithScale(UnityEngine.RectTransform rect, System.Int32 axis, System.Single pos, System.Single size, System.Single scaleFactor) { }
        // RVA: 0x03DC3550  token: 0x6000323
        protected virtual System.Void OnRectTransformDimensionsChange() { }
        // RVA: 0x044067D0  token: 0x6000324
        protected virtual System.Void OnTransformChildrenChanged() { }
        // RVA: -1  // generic def  token: 0x6000325
        protected System.Void SetProperty(T& currentValue, T newValue) { }
        // RVA: 0x03519E40  token: 0x6000326
        protected System.Void SetDirty() { }
        // RVA: 0x043BB440  token: 0x6000327
        private System.Collections.IEnumerator DelayedSetDirty(UnityEngine.RectTransform rectTransform) { }

    }

    // TypeToken: 0x2000056  // size: 0x20
    public class LayoutRebuilder : UnityEngine.UI.ICanvasElement
    {
        // Fields
        private UnityEngine.RectTransform m_ToRebuild;  // 0x10
        private System.Int32 m_CachedHashFromTransform;  // 0x18
        private static UnityEngine.Pool.ObjectPool<UnityEngine.UI.LayoutRebuilder> s_Rebuilders;  // static @ 0x0

        // Properties
        UnityEngine.Transform transform { get; /* RVA: 0x02B2ECC0 */ }

        // Methods
        // RVA: 0x0351B220  token: 0x600032E
        private System.Void Initialize(UnityEngine.RectTransform controller) { }
        // RVA: 0x05396170  token: 0x600032F
        private System.Void Clear() { }
        // RVA: 0x048F3650  token: 0x6000330
        private static System.Void .cctor() { }
        // RVA: 0x03EA5880  token: 0x6000331
        private static System.Void ReapplyDrivenProperties(UnityEngine.RectTransform driven) { }
        // RVA: 0x041916B0  token: 0x6000333
        public virtual System.Boolean IsDestroyed() { }
        // RVA: 0x031F0E10  token: 0x6000334
        private static System.Void StripDisabledBehavioursFromList(System.Collections.Generic.List<UnityEngine.Component> components) { }
        // RVA: 0x03543F10  token: 0x6000335
        public static System.Void ForceRebuildLayoutImmediate(UnityEngine.RectTransform layoutRoot) { }
        // RVA: 0x03543FC0  token: 0x6000336
        public virtual System.Void Rebuild(UnityEngine.UI.CanvasUpdate executing) { }
        // RVA: 0x031F13B0  token: 0x6000337
        private System.Void PerformLayoutControl(UnityEngine.RectTransform rect, UnityEngine.Events.UnityAction<UnityEngine.Component> action) { }
        // RVA: 0x031F0680  token: 0x6000338
        private System.Void PerformLayoutCalculation(UnityEngine.RectTransform rect, UnityEngine.Events.UnityAction<UnityEngine.Component> action) { }
        // RVA: 0x02FCBF00  token: 0x6000339
        public static System.Void MarkLayoutForRebuild(UnityEngine.RectTransform rect) { }
        // RVA: 0x031F0F50  token: 0x600033A
        private static System.Boolean ValidController(UnityEngine.RectTransform layoutRoot, System.Collections.Generic.List<UnityEngine.Component> comps) { }
        // RVA: 0x0351AF00  token: 0x600033B
        private static System.Void MarkLayoutRootForRebuild(UnityEngine.RectTransform controller) { }
        // RVA: 0x040E03C0  token: 0x600033C
        public virtual System.Void LayoutComplete() { }
        // RVA: 0x041E1670  token: 0x600033D
        public virtual System.Void GraphicUpdateComplete() { }
        // RVA: 0x011EF5B0  token: 0x600033E
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x03D30750  token: 0x600033F
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0B571D28  token: 0x6000340
        public virtual System.String ToString() { }
        // RVA: 0x041E1670  token: 0x6000341
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000058  // size: 0x10
    public static class LayoutUtility
    {
        // Methods
        // RVA: 0x033F9120  token: 0x600034B
        public static System.Single GetMinSize(UnityEngine.RectTransform rect, System.Int32 axis) { }
        // RVA: 0x033F8FE0  token: 0x600034C
        public static System.Single GetPreferredSize(UnityEngine.RectTransform rect, System.Int32 axis) { }
        // RVA: 0x033F9010  token: 0x600034D
        public static System.Single GetFlexibleSize(UnityEngine.RectTransform rect, System.Int32 axis) { }
        // RVA: 0x033F7A30  token: 0x600034E
        public static System.Single GetMinWidth(UnityEngine.RectTransform rect) { }
        // RVA: 0x033F9410  token: 0x600034F
        public static System.Single GetPreferredWidth(UnityEngine.RectTransform rect) { }
        // RVA: 0x033F7940  token: 0x6000350
        public static System.Single GetFlexibleWidth(UnityEngine.RectTransform rect) { }
        // RVA: 0x03DB8680  token: 0x6000351
        public static System.Single GetMinHeight(UnityEngine.RectTransform rect) { }
        // RVA: 0x033F9230  token: 0x6000352
        public static System.Single GetPreferredHeight(UnityEngine.RectTransform rect) { }
        // RVA: 0x03DB8770  token: 0x6000353
        public static System.Single GetFlexibleHeight(UnityEngine.RectTransform rect) { }
        // RVA: 0x05392498  token: 0x6000354
        public static System.Single GetLayoutProperty(UnityEngine.RectTransform rect, System.Func<UnityEngine.UI.ILayoutElement,System.Single> property, System.Single defaultValue) { }
        // RVA: 0x031EF3A0  token: 0x6000355
        public static System.Single GetLayoutProperty(UnityEngine.RectTransform rect, System.Func<UnityEngine.UI.ILayoutElement,System.Single> property, System.Single defaultValue, UnityEngine.UI.ILayoutElement& source) { }

    }

    // TypeToken: 0x200005A  // size: 0x68
    public class VerticalLayoutGroup : UnityEngine.UI.HorizontalOrVerticalLayoutGroup
    {
        // Methods
        // RVA: 0x0416C540  token: 0x6000360
        protected System.Void .ctor() { }
        // RVA: 0x03B91930  token: 0x6000361
        public virtual System.Void CalculateLayoutInputHorizontal() { }
        // RVA: 0x053953B0  token: 0x6000362
        public virtual System.Void CalculateLayoutInputVertical() { }
        // RVA: 0x053915FC  token: 0x6000363
        public virtual System.Void SetLayoutHorizontal() { }
        // RVA: 0x0539659C  token: 0x6000364
        public virtual System.Void SetLayoutVertical() { }

    }

    // TypeToken: 0x200005B  // size: 0x40
    public class Mask : UnityEngine.EventSystems.UIBehaviour, UnityEngine.ICanvasRaycastFilter, UnityEngine.UI.IMaterialModifier
    {
        // Fields
        private UnityEngine.RectTransform m_RectTransform;  // 0x18
        private System.Boolean m_ShowMaskGraphic;  // 0x20
        private UnityEngine.UI.Graphic m_Graphic;  // 0x28
        private UnityEngine.Material m_MaskMaterial;  // 0x30
        private UnityEngine.Material m_UnmaskMaterial;  // 0x38

        // Properties
        UnityEngine.RectTransform rectTransform { get; /* RVA: 0x03091280 */ }
        System.Boolean showMaskGraphic { get; /* RVA: 0x04D866B0 */ set; /* RVA: 0x0B571D88 */ }
        UnityEngine.UI.Graphic graphic { get; /* RVA: 0x041D2210 */ }

        // Methods
        // RVA: 0x0489D040  token: 0x6000369
        protected System.Void .ctor() { }
        // RVA: 0x041D1CA0  token: 0x600036A
        public virtual System.Boolean MaskEnabled() { }
        // RVA: 0x041E1670  token: 0x600036B
        public virtual System.Void OnSiblingGraphicEnabledDisabled() { }
        // RVA: 0x041D20C0  token: 0x600036C
        protected virtual System.Void OnEnable() { }
        // RVA: 0x041D1D30  token: 0x600036D
        protected virtual System.Void OnDisable() { }
        // RVA: 0x030911F0  token: 0x600036E
        public virtual System.Boolean IsRaycastLocationValid(UnityEngine.Vector2 sp, UnityEngine.Camera eventCamera) { }
        // RVA: 0x041D1F10  token: 0x600036F
        public virtual UnityEngine.Material GetModifiedMaterial(UnityEngine.Material baseMaterial) { }

    }

    // TypeToken: 0x200005C  // size: 0xE8
    public abstract class MaskableGraphic : UnityEngine.UI.Graphic, UnityEngine.UI.IClippable, UnityEngine.UI.IMaskable, UnityEngine.UI.IMaterialModifier
    {
        // Fields
        protected System.Boolean m_ShouldRecalculateStencil;  // 0xb0
        protected UnityEngine.Material m_MaskMaterial;  // 0xb8
        private UnityEngine.UI.RectMask2D m_ParentMask;  // 0xc0
        private System.Boolean m_Maskable;  // 0xc8
        private System.Boolean m_IsMaskingGraphic;  // 0xc9
        protected System.Boolean m_IncludeForMasking;  // 0xca
        private UnityEngine.UI.MaskableGraphic.CullStateChangedEvent m_OnCullStateChanged;  // 0xd0
        protected System.Boolean m_ShouldRecalculate;  // 0xd8
        protected System.Int32 m_StencilValue;  // 0xdc
        private readonly UnityEngine.Vector3[] m_Corners;  // 0xe0

        // Properties
        UnityEngine.UI.MaskableGraphic.CullStateChangedEvent onCullStateChanged { get; /* RVA: 0x04D876B0 */ set; /* RVA: 0x0678D7BC */ }
        System.Boolean maskable { get; /* RVA: 0x04D8E270 */ set; /* RVA: 0x0B5721A8 */ }
        System.Boolean isMaskingGraphic { get; /* RVA: 0x04DA2350 */ set; /* RVA: 0x041D2380 */ }
        System.Boolean hasCullOverride { get; /* RVA: 0x02FFF600 */ }
        UnityEngine.Rect rootCanvasRect { get; /* RVA: 0x0B571E74 */ }

        // Methods
        // RVA: 0x031E9E20  token: 0x6000377
        private System.Void UpdateCullDirect(System.Boolean cull) { }
        // RVA: 0x031EE3E0  token: 0x6000378
        public virtual UnityEngine.Material GetModifiedMaterial(UnityEngine.Material baseMaterial) { }
        // RVA: 0x0B571E14  token: 0x6000379
        public virtual System.Void Cull(UnityEngine.Rect clipRect, System.Boolean validRect) { }
        // RVA: 0x031EB1D0  token: 0x600037A
        private System.Void UpdateCull(System.Boolean cull) { }
        // RVA: 0x031EB0E0  token: 0x600037B
        public virtual System.Void SetClipRect(UnityEngine.Rect clipRect, System.Boolean validRect) { }
        // RVA: 0x031BFD70  token: 0x600037C
        public virtual System.Void SetClipSoftness(UnityEngine.Vector2 clipSoftness) { }
        // RVA: 0x031BFC20  token: 0x600037D
        public virtual System.Void SetHGClipSoftness(UnityEngine.Vector4 hgClipSoftness) { }
        // RVA: 0x031EBD20  token: 0x600037E
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0351A3E0  token: 0x600037F
        protected virtual System.Void OnDisable() { }
        // RVA: 0x03F3AEA0  token: 0x6000380
        protected virtual System.Void OnTransformParentChanged() { }
        // RVA: 0x041E1670  token: 0x6000381
        public virtual System.Void ParentMaskStateChanged() { }
        // RVA: 0x031C3D80  token: 0x6000382
        protected virtual System.Void OnCanvasHierarchyChanged() { }
        // RVA: 0x031EC620  token: 0x6000384
        private System.Void UpdateClipParent() { }
        // RVA: 0x03F59BE0  token: 0x6000385
        public virtual System.Void RecalculateClipping() { }
        // RVA: 0x0351C410  token: 0x6000386
        public virtual System.Void RecalculateMasking() { }
        // RVA: 0x03696ED0  token: 0x6000387
        protected System.Void .ctor() { }
        // RVA: 0x053916BC  token: 0x6000388
        private virtual UnityEngine.GameObject UnityEngine.UI.IClippable.get_gameObject() { }

    }

    // TypeToken: 0x200005E  // size: 0x10
    public class MaskUtilities
    {
        // Methods
        // RVA: 0x031EF090  token: 0x600038A
        public static System.Void Notify2DMaskStateChanged(UnityEngine.Component mask) { }
        // RVA: 0x041D23E0  token: 0x600038B
        public static System.Void NotifyStencilStateChanged(UnityEngine.Component mask) { }
        // RVA: 0x031ED4E0  token: 0x600038C
        public static UnityEngine.Transform FindRootSortOverrideCanvas(UnityEngine.Transform start) { }
        // RVA: 0x031ED130  token: 0x600038D
        public static System.Int32 GetStencilDepth(UnityEngine.Transform transform, UnityEngine.Transform stopAfter) { }
        // RVA: 0x031EB530  token: 0x600038E
        public static System.Boolean IsDescendantOrSelf(UnityEngine.Transform father, UnityEngine.Transform child) { }
        // RVA: 0x031EC810  token: 0x600038F
        public static UnityEngine.UI.RectMask2D GetRectMaskForClippable(UnityEngine.UI.IClippable clippable) { }
        // RVA: 0x03D91980  token: 0x6000390
        public static System.Void GetRectMasksForClip(UnityEngine.UI.RectMask2D clipper, System.Collections.Generic.List<UnityEngine.UI.RectMask2D> masks) { }
        // RVA: 0x041E1670  token: 0x6000391
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200005F
    public interface IMaterialModifier
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000392
        public virtual UnityEngine.Material GetModifiedMaterial(UnityEngine.Material baseMaterial) { }

    }

    // TypeToken: 0x2000060  // size: 0x10
    public static class Misc
    {
        // Methods
        // RVA: 0x0B572CC8  token: 0x6000393
        public static System.Void Destroy(UnityEngine.Object obj) { }
        // RVA: 0x0485FD10  token: 0x6000394
        public static System.Void DestroyImmediate(UnityEngine.Object obj) { }

    }

    // TypeToken: 0x2000061  // size: 0x10
    public static class MultipleDisplayUtilities
    {
        // Methods
        // RVA: 0x0B572D94  token: 0x6000395
        public static System.Boolean GetRelativeMousePositionForDrag(UnityEngine.EventSystems.PointerEventData eventData, UnityEngine.Vector2& position) { }
        // RVA: 0x0324F2F0  token: 0x6000396
        public static UnityEngine.Vector3 RelativeMouseAtScaled(UnityEngine.Vector2 position) { }

    }

    // TypeToken: 0x2000062  // size: 0x38
    public sealed struct Navigation : System.IEquatable`1
    {
        // Fields
        private UnityEngine.UI.Navigation.Mode m_Mode;  // 0x10
        private System.Boolean m_WrapAround;  // 0x14
        private UnityEngine.UI.Selectable m_SelectOnUp;  // 0x18
        private UnityEngine.UI.Selectable m_SelectOnDown;  // 0x20
        private UnityEngine.UI.Selectable m_SelectOnLeft;  // 0x28
        private UnityEngine.UI.Selectable m_SelectOnRight;  // 0x30

        // Properties
        UnityEngine.UI.Navigation.Mode mode { get; /* RVA: 0x02B77620 */ set; /* RVA: 0x04D86140 */ }
        System.Boolean wrapAround { get; /* RVA: 0x04D90BB0 */ set; /* RVA: 0x04D90BC0 */ }
        UnityEngine.UI.Selectable selectOnUp { get; /* RVA: 0x04D88320 */ set; /* RVA: 0x067D5E9C */ }
        UnityEngine.UI.Selectable selectOnDown { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        UnityEngine.UI.Selectable selectOnLeft { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        UnityEngine.UI.Selectable selectOnRight { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        UnityEngine.UI.Navigation defaultNavigation { get; /* RVA: 0x04DBEFF0 */ }

        // Methods
        // RVA: 0x03E58120  token: 0x60003A4
        public virtual System.Boolean Equals(UnityEngine.UI.Navigation other) { }

    }

    // TypeToken: 0x2000064  // size: 0x100
    public class RawImage : UnityEngine.UI.MaskableGraphic
    {
        // Fields
        private UnityEngine.Texture m_Texture;  // 0xe8
        private UnityEngine.Rect m_UVRect;  // 0xf0

        // Properties
        System.Boolean hasCullOverride { get; /* RVA: 0x012081B0 */ }
        UnityEngine.Texture mainTexture { get; /* RVA: 0x0429BCE0 */ }
        UnityEngine.Texture texture { get; /* RVA: 0x04D88590 */ set; /* RVA: 0x037DC210 */ }
        UnityEngine.Rect uvRect { get; /* RVA: 0x04DA1270 */ set; /* RVA: 0x0B572FA4 */ }

        // Methods
        // RVA: 0x039C5080  token: 0x60003A6
        protected System.Void .ctor() { }
        // RVA: 0x0B572E6C  token: 0x60003AC
        public virtual System.Void SetNativeSize() { }
        // RVA: 0x04032A90  token: 0x60003AD
        protected virtual System.Void OnPopulateMesh(UnityEngine.UI.VertexHelper vh) { }
        // RVA: 0x03E51A10  token: 0x60003AE
        protected virtual System.Void OnDidApplyAnimationProperties() { }

    }

    // TypeToken: 0x2000065  // size: 0x98
    public class RectMask2D : UnityEngine.EventSystems.UIBehaviour, UnityEngine.UI.IClipper, UnityEngine.ICanvasRaycastFilter
    {
        // Fields
        private readonly UnityEngine.UI.RectangularVertexClipper m_VertexClipper;  // 0x18
        private UnityEngine.RectTransform m_RectTransform;  // 0x20
        private System.Collections.Generic.HashSet<UnityEngine.UI.MaskableGraphic> m_MaskableTargets;  // 0x28
        private System.Collections.Generic.HashSet<UnityEngine.UI.IClippable> m_ClipTargets;  // 0x30
        private System.Boolean m_ShouldRecalculateClipRects;  // 0x38
        private System.Collections.Generic.List<UnityEngine.UI.RectMask2D> m_Clippers;  // 0x40
        private UnityEngine.Rect m_LastClipRectCanvasSpace;  // 0x48
        private System.Boolean m_ForceClip;  // 0x58
        private UnityEngine.Vector4 m_Padding;  // 0x5c
        private UnityEngine.Vector2Int m_Softness;  // 0x6c
        private UnityEngine.Vector4 m_HGSoftness;  // 0x74
        private UnityEngine.Canvas m_Canvas;  // 0x88
        private UnityEngine.Vector3[] m_Corners;  // 0x90

        // Properties
        UnityEngine.Vector4 padding { get; /* RVA: 0x04DA8720 */ set; /* RVA: 0x0B573E70 */ }
        UnityEngine.Vector2Int softness { get; /* RVA: 0x04DBF020 */ set; /* RVA: 0x0B573E80 */ }
        UnityEngine.Vector4 hgSoftness { get; /* RVA: 0x04DBF010 */ set; /* RVA: 0x0B573E30 */ }
        UnityEngine.Canvas Canvas { get; /* RVA: 0x031C28A0 */ }
        UnityEngine.Rect canvasRect { get; /* RVA: 0x0B573B54 */ }
        UnityEngine.RectTransform rectTransform { get; /* RVA: 0x031BF930 */ }
        UnityEngine.Rect rootCanvasRect { get; /* RVA: 0x0B573BCC */ }
        System.Boolean shouldRecalculateClipRects { get; /* RVA: 0x011F2A70 */ set; /* RVA: 0x011F2A80 */ }
        System.Collections.Generic.List<UnityEngine.UI.RectMask2D> clippers { get; /* RVA: 0x04D85A60 */ }
        System.Collections.Generic.HashSet<UnityEngine.UI.MaskableGraphic> maskableTargets { get; /* RVA: 0x04D86240 */ }
        System.Collections.Generic.HashSet<UnityEngine.UI.IClippable> clipTargets { get; /* RVA: 0x011F36E0 */ }
        UnityEngine.Rect lastClipRectCanvasSpace { get; /* RVA: 0x04D8FD50 */ set; /* RVA: 0x04D91540 */ }
        System.Boolean forceClip { get; /* RVA: 0x04D86F70 */ set; /* RVA: 0x04D86F80 */ }
        UnityEngine.Canvas canvasRef { get; /* RVA: 0x053914B4 */ }

        // Methods
        // RVA: 0x0404D490  token: 0x60003B8
        protected System.Void .ctor() { }
        // RVA: 0x04424720  token: 0x60003B9
        protected virtual System.Void OnEnable() { }
        // RVA: 0x03D918E0  token: 0x60003BA
        protected virtual System.Void OnDisable() { }
        // RVA: 0x030933E0  token: 0x60003BB
        public virtual System.Boolean IsRaycastLocationValid(UnityEngine.Vector2 sp, UnityEngine.Camera eventCamera) { }
        // RVA: 0x0B573458  token: 0x60003BD
        public virtual System.Void PerformClipping() { }
        // RVA: 0x031C1D80  token: 0x60003BE
        public virtual System.Void UpdateClipSoftness() { }
        // RVA: 0x031EB2C0  token: 0x60003BF
        public System.Void AddClippable(UnityEngine.UI.IClippable clippable) { }
        // RVA: 0x031EAE20  token: 0x60003C0
        public System.Void RemoveClippable(UnityEngine.UI.IClippable clippable) { }
        // RVA: 0x04D919E0  token: 0x60003C1
        protected virtual System.Void OnTransformParentChanged() { }
        // RVA: 0x042ADE00  token: 0x60003C2
        protected virtual System.Void OnCanvasHierarchyChanged() { }
        // RVA: 0x03D91950  token: 0x60003CD
        private System.Void RecalculateClipperChain() { }

    }

    // TypeToken: 0x2000066  // size: 0x1D0
    public class Scrollbar : UnityEngine.UI.Selectable, UnityEngine.EventSystems.IBeginDragHandler, UnityEngine.EventSystems.IEventSystemHandler, UnityEngine.EventSystems.IDragHandler, UnityEngine.EventSystems.IInitializePotentialDragHandler, UnityEngine.UI.ICanvasElement
    {
        // Fields
        private UnityEngine.RectTransform m_HandleRect;  // 0x188
        private UnityEngine.UI.Scrollbar.Direction m_Direction;  // 0x190
        private System.Single m_Value;  // 0x194
        private System.Single m_Size;  // 0x198
        private System.Int32 m_NumberOfSteps;  // 0x19c
        private UnityEngine.UI.Scrollbar.ScrollEvent m_OnValueChanged;  // 0x1a0
        private UnityEngine.RectTransform m_ContainerRect;  // 0x1a8
        private UnityEngine.Vector2 m_Offset;  // 0x1b0
        private UnityEngine.DrivenRectTransformTracker m_Tracker;  // 0x1b8
        private UnityEngine.Coroutine m_PointerDownRepeat;  // 0x1c0
        private System.Boolean isPointerDownAndNotDragging;  // 0x1c8
        private System.Boolean m_DelayedUpdateVisuals;  // 0x1c9

        // Properties
        UnityEngine.RectTransform handleRect { get; /* RVA: 0x04D8DA20 */ set; /* RVA: 0x0B576148 */ }
        UnityEngine.UI.Scrollbar.Direction direction { get; /* RVA: 0x04DBF060 */ set; /* RVA: 0x0B5760EC */ }
        System.Single value { get; /* RVA: 0x04033F40 */ set; /* RVA: 0x05396DA0 */ }
        System.Single size { get; /* RVA: 0x04D8EDC0 */ set; /* RVA: 0x04033C10 */ }
        System.Int32 numberOfSteps { get; /* RVA: 0x04D8F920 */ set; /* RVA: 0x0B5761B0 */ }
        UnityEngine.UI.Scrollbar.ScrollEvent onValueChanged { get; /* RVA: 0x04D8DA10 */ set; /* RVA: 0x05397204 */ }
        System.Single stepSize { get; /* RVA: 0x0B5760BC */ }
        UnityEngine.UI.Scrollbar.Axis axis { get; /* RVA: 0x04033F20 */ }
        System.Boolean reverseValue { get; /* RVA: 0x04033F70 */ }

        // Methods
        // RVA: 0x0453A550  token: 0x60003D2
        protected System.Void .ctor() { }
        // RVA: 0x0B575E2C  token: 0x60003D5
        public virtual System.Void SetValueWithoutNotify(System.Single input) { }
        // RVA: 0x041E1670  token: 0x60003DD
        public virtual System.Void Rebuild(UnityEngine.UI.CanvasUpdate executing) { }
        // RVA: 0x041E1670  token: 0x60003DE
        public virtual System.Void LayoutComplete() { }
        // RVA: 0x041E1670  token: 0x60003DF
        public virtual System.Void GraphicUpdateComplete() { }
        // RVA: 0x04033C80  token: 0x60003E0
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0447EC90  token: 0x60003E1
        protected virtual System.Void OnDisable() { }
        // RVA: 0x04D265A0  token: 0x60003E2
        protected virtual System.Void Update() { }
        // RVA: 0x04033F90  token: 0x60003E3
        private System.Void UpdateCachedReferences() { }
        // RVA: 0x04033CE0  token: 0x60003E4
        private System.Void Set(System.Single input, System.Boolean sendCallback) { }
        // RVA: 0x04033C50  token: 0x60003E5
        protected virtual System.Void OnRectTransformDimensionsChange() { }
        // RVA: 0x04033D80  token: 0x60003E8
        private System.Void UpdateVisuals() { }
        // RVA: 0x0B575E38  token: 0x60003E9
        private System.Void UpdateDrag(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B5755D4  token: 0x60003EA
        private System.Void DoUpdateDrag(UnityEngine.Vector2 handleCorner, System.Single remainingSize) { }
        // RVA: 0x0B575710  token: 0x60003EB
        private System.Boolean MayDrag(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B575764  token: 0x60003EC
        public virtual System.Void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B575974  token: 0x60003ED
        public virtual System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B575C08  token: 0x60003EE
        public virtual System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B57555C  token: 0x60003EF
        protected System.Collections.IEnumerator ClickRepeat(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B5754CC  token: 0x60003F0
        protected System.Collections.IEnumerator ClickRepeat(UnityEngine.Vector2 screenPosition, UnityEngine.Camera camera) { }
        // RVA: 0x0B575CD0  token: 0x60003F1
        public virtual System.Void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B575A18  token: 0x60003F2
        public virtual System.Void OnMove(UnityEngine.EventSystems.AxisEventData eventData) { }
        // RVA: 0x0B575674  token: 0x60003F3
        public virtual UnityEngine.UI.Selectable FindSelectableOnLeft() { }
        // RVA: 0x0B5756A8  token: 0x60003F4
        public virtual UnityEngine.UI.Selectable FindSelectableOnRight() { }
        // RVA: 0x0B5756DC  token: 0x60003F5
        public virtual UnityEngine.UI.Selectable FindSelectableOnUp() { }
        // RVA: 0x0B575640  token: 0x60003F6
        public virtual UnityEngine.UI.Selectable FindSelectableOnDown() { }
        // RVA: 0x0B5759FC  token: 0x60003F7
        public virtual System.Void OnInitializePotentialDrag(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B575CF0  token: 0x60003F8
        public System.Void SetDirection(UnityEngine.UI.Scrollbar.Direction direction, System.Boolean includeRectLayouts) { }
        // RVA: 0x053953F4  token: 0x60003F9
        private virtual UnityEngine.Transform UnityEngine.UI.ICanvasElement.get_transform() { }

    }

    // TypeToken: 0x200006B  // size: 0x150
    public class ScrollRect : UnityEngine.EventSystems.UIBehaviour, UnityEngine.EventSystems.IInitializePotentialDragHandler, UnityEngine.EventSystems.IEventSystemHandler, UnityEngine.EventSystems.IBeginDragHandler, UnityEngine.EventSystems.IEndDragHandler, UnityEngine.EventSystems.IDragHandler, UnityEngine.EventSystems.IScrollHandler, UnityEngine.UI.ICanvasElement, UnityEngine.UI.ILayoutElement, UnityEngine.UI.ILayoutGroup, UnityEngine.UI.ILayoutController
    {
        // Fields
        private UnityEngine.RectTransform m_Content;  // 0x18
        private System.Boolean m_Horizontal;  // 0x20
        private System.Boolean m_Vertical;  // 0x21
        protected UnityEngine.GameObject m_scrollEnableNode;  // 0x28
        private UnityEngine.UI.ScrollRect.MovementType m_MovementType;  // 0x30
        private System.Single m_Elasticity;  // 0x34
        private System.Boolean m_Inertia;  // 0x38
        private System.Single m_DecelerationRate;  // 0x3c
        private System.Single m_ScrollSensitivity;  // 0x40
        private UnityEngine.RectTransform m_Viewport;  // 0x48
        private UnityEngine.UI.Scrollbar m_HorizontalScrollbar;  // 0x50
        private UnityEngine.UI.Scrollbar m_VerticalScrollbar;  // 0x58
        private UnityEngine.UI.ScrollRect.ScrollbarVisibility m_HorizontalScrollbarVisibility;  // 0x60
        private UnityEngine.UI.ScrollRect.ScrollbarVisibility m_VerticalScrollbarVisibility;  // 0x64
        private System.Single m_HorizontalScrollbarSpacing;  // 0x68
        private System.Single m_VerticalScrollbarSpacing;  // 0x6c
        private UnityEngine.UI.ScrollRect.ScrollRectEvent m_OnValueChanged;  // 0x70
        public System.Boolean disableScroll;  // 0x78
        public UnityEngine.UI.ScrollRect.ScrollRectEvent OnScrollStart;  // 0x80
        public UnityEngine.UI.ScrollRect.ScrollRectEvent OnScrollEnd;  // 0x88
        private UnityEngine.Vector2 m_PointerStartLocalCursor;  // 0x90
        protected UnityEngine.Vector2 m_ContentStartPosition;  // 0x98
        private UnityEngine.RectTransform m_ViewRect;  // 0xa0
        protected UnityEngine.Bounds m_ContentBounds;  // 0xa8
        private UnityEngine.Bounds m_ViewBounds;  // 0xc0
        private UnityEngine.Vector2 m_Velocity;  // 0xd8
        protected System.Boolean m_Dragging;  // 0xe0
        protected System.Boolean m_onScrollEndCbFlag;  // 0xe1
        protected System.Boolean m_Scrolling;  // 0xe2
        private UnityEngine.Vector2 m_PrevPosition;  // 0xe4
        private UnityEngine.Bounds m_PrevContentBounds;  // 0xec
        private UnityEngine.Bounds m_PrevViewBounds;  // 0x104
        private System.Boolean m_HasRebuiltLayout;  // 0x11c
        private System.Boolean m_HSliderExpand;  // 0x11d
        private System.Boolean m_VSliderExpand;  // 0x11e
        private System.Single m_HSliderHeight;  // 0x120
        private System.Single m_VSliderWidth;  // 0x124
        private UnityEngine.RectTransform m_Rect;  // 0x128
        private UnityEngine.RectTransform m_HorizontalScrollbarRect;  // 0x130
        private UnityEngine.RectTransform m_VerticalScrollbarRect;  // 0x138
        private UnityEngine.DrivenRectTransformTracker m_Tracker;  // 0x140
        private readonly UnityEngine.Vector3[] m_Corners;  // 0x148

        // Properties
        UnityEngine.RectTransform content { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.Boolean horizontal { get; /* RVA: 0x04D866B0 */ set; /* RVA: 0x04D866D0 */ }
        System.Boolean vertical { get; /* RVA: 0x04D86850 */ set; /* RVA: 0x04D86860 */ }
        UnityEngine.UI.ScrollRect.MovementType movementType { get; /* RVA: 0x04D864F0 */ set; /* RVA: 0x04D86740 */ }
        System.Single elasticity { get; /* RVA: 0x04D8E050 */ set; /* RVA: 0x04D8E060 */ }
        System.Boolean inertia { get; /* RVA: 0x011F2A70 */ set; /* RVA: 0x011F2A80 */ }
        System.Single decelerationRate { get; /* RVA: 0x04D863E0 */ set; /* RVA: 0x04D86460 */ }
        System.Single scrollSensitivity { get; /* RVA: 0x04D863F0 */ set; /* RVA: 0x04D86470 */ }
        UnityEngine.RectTransform viewport { get; /* RVA: 0x04D86200 */ set; /* RVA: 0x0B5754A8 */ }
        UnityEngine.UI.Scrollbar horizontalScrollbar { get; /* RVA: 0x04D86270 */ set; /* RVA: 0x0B575144 */ }
        UnityEngine.UI.Scrollbar verticalScrollbar { get; /* RVA: 0x02E56440 */ set; /* RVA: 0x0B575320 */ }
        UnityEngine.UI.ScrollRect.ScrollbarVisibility horizontalScrollbarVisibility { get; /* RVA: 0x04D86790 */ set; /* RVA: 0x0B575138 */ }
        UnityEngine.UI.ScrollRect.ScrollbarVisibility verticalScrollbarVisibility { get; /* RVA: 0x04D88110 */ set; /* RVA: 0x0B575314 */ }
        System.Single horizontalScrollbarSpacing { get; /* RVA: 0x04D8F060 */ set; /* RVA: 0x0B57512C */ }
        System.Single verticalScrollbarSpacing { get; /* RVA: 0x04DA3230 */ set; /* RVA: 0x0B575308 */ }
        UnityEngine.UI.ScrollRect.ScrollRectEvent onValueChanged { get; /* RVA: 0x04D862B0 */ set; /* RVA: 0x063AD760 */ }
        UnityEngine.RectTransform viewRect { get; /* RVA: 0x033F6EF0 */ }
        UnityEngine.Vector2 velocity { get; /* RVA: 0x04DBF030 */ set; /* RVA: 0x04DBF050 */ }
        UnityEngine.RectTransform rectTransform { get; /* RVA: 0x039D3BC0 */ }
        UnityEngine.Vector2 normalizedPosition { get; /* RVA: 0x0396F070 */ set; /* RVA: 0x0B5752CC */ }
        System.Single horizontalNormalizedPosition { get; /* RVA: 0x0396F0B0 */ set; /* RVA: 0x05397FA8 */ }
        System.Single verticalNormalizedPosition { get; /* RVA: 0x0396F250 */ set; /* RVA: 0x053945E8 */ }
        System.Boolean hScrollingNeeded { get; /* RVA: 0x03E5E6B0 */ }
        System.Boolean vScrollingNeeded { get; /* RVA: 0x03E5E600 */ }
        System.Single minWidth { get; /* RVA: 0x04DA9E10 */ }
        System.Single preferredWidth { get; /* RVA: 0x04DA9E10 */ }
        System.Single flexibleWidth { get; /* RVA: 0x04DA9E10 */ }
        System.Single minHeight { get; /* RVA: 0x04DA9E10 */ }
        System.Single preferredHeight { get; /* RVA: 0x04DA9E10 */ }
        System.Single flexibleHeight { get; /* RVA: 0x04DA9E10 */ }
        System.Int32 layoutPriority { get; /* RVA: 0x04DA2BB0 */ }

        // Methods
        // RVA: 0x03EB2B60  token: 0x6000425
        protected System.Void .ctor() { }
        // RVA: 0x04033360  token: 0x6000426
        public virtual System.Void Rebuild(UnityEngine.UI.CanvasUpdate executing) { }
        // RVA: 0x041E1670  token: 0x6000427
        public virtual System.Void LayoutComplete() { }
        // RVA: 0x041E1670  token: 0x6000428
        public virtual System.Void GraphicUpdateComplete() { }
        // RVA: 0x040337D0  token: 0x6000429
        private System.Void UpdateCachedData() { }
        // RVA: 0x039D39F0  token: 0x600042A
        protected virtual System.Void OnEnable() { }
        // RVA: 0x039D37F0  token: 0x600042B
        protected virtual System.Void OnDisable() { }
        // RVA: 0x045E1060  token: 0x600042C
        public virtual System.Boolean IsActive() { }
        // RVA: 0x033F6720  token: 0x600042D
        public System.Void EnsureLayoutHasRebuilt() { }
        // RVA: 0x0B575100  token: 0x600042E
        public virtual System.Void StopMovement() { }
        // RVA: 0x0B574E34  token: 0x600042F
        public virtual System.Void OnScroll(UnityEngine.EventSystems.PointerEventData data) { }
        // RVA: 0x0B57510C  token: 0x6000430
        protected virtual System.Void _CustomSetPosOnScroll(UnityEngine.Vector2 position) { }
        // RVA: 0x04A26320  token: 0x6000431
        public virtual System.Void OnInitializePotentialDrag(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B5749F4  token: 0x6000432
        public virtual System.Void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B574E10  token: 0x6000433
        public virtual System.Void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B574B20  token: 0x6000434
        public virtual System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x039722E0  token: 0x6000435
        protected virtual System.Void SetContentAnchoredPosition(UnityEngine.Vector2 position) { }
        // RVA: 0x033F4C20  token: 0x6000436
        protected virtual System.Void LateUpdate() { }
        // RVA: 0x04033630  token: 0x6000437
        protected System.Void UpdatePrevData() { }
        // RVA: 0x04033480  token: 0x6000438
        protected virtual System.Void UpdateScrollbars(UnityEngine.Vector2 offset) { }
        // RVA: 0x05397FA8  token: 0x600043F
        protected virtual System.Void SetHorizontalNormalizedPosition(System.Single value) { }
        // RVA: 0x04573010  token: 0x6000440
        protected virtual System.Void SetVerticalNormalizedPosition(System.Single value) { }
        // RVA: 0x04573020  token: 0x6000441
        protected virtual System.Void SetNormalizedPosition(System.Single value, System.Int32 axis) { }
        // RVA: 0x0B575018  token: 0x6000442
        private static System.Single RubberDelta(System.Single overStretching, System.Single viewSize) { }
        // RVA: 0x039D3B60  token: 0x6000443
        protected virtual System.Void OnRectTransformDimensionsChange() { }
        // RVA: 0x041E1670  token: 0x6000446
        public virtual System.Void CalculateLayoutInputHorizontal() { }
        // RVA: 0x041E1670  token: 0x6000447
        public virtual System.Void CalculateLayoutInputVertical() { }
        // RVA: 0x04033740  token: 0x600044F
        public virtual System.Void SetLayoutHorizontal() { }
        // RVA: 0x03EF8850  token: 0x6000450
        public virtual System.Void SetLayoutVertical() { }
        // RVA: 0x03270630  token: 0x6000451
        private System.Void UpdateScrollbarVisibility() { }
        // RVA: 0x03270900  token: 0x6000452
        private static System.Void UpdateOneScrollbarVisibility(System.Boolean xScrollingNeeded, System.Boolean xAxisEnabled, UnityEngine.UI.ScrollRect.ScrollbarVisibility scrollbarVisibility, UnityEngine.UI.Scrollbar scrollbar) { }
        // RVA: 0x03EF89B0  token: 0x6000453
        private System.Void UpdateScrollbarLayout() { }
        // RVA: 0x03E0B730  token: 0x6000454
        protected virtual System.Void UpdateScrollEnableNode() { }
        // RVA: 0x033F6990  token: 0x6000455
        protected System.Void UpdateBounds() { }
        // RVA: 0x033F6850  token: 0x6000456
        private static System.Void AdjustBounds(UnityEngine.Bounds& viewBounds, UnityEngine.Vector2& contentPivot, UnityEngine.Vector3& contentSize, UnityEngine.Vector3& contentPos) { }
        // RVA: 0x02F3C280  token: 0x6000457
        private UnityEngine.Bounds GetBounds() { }
        // RVA: 0x02F3C5E0  token: 0x6000458
        private static UnityEngine.Bounds InternalGetBounds(UnityEngine.Vector3[] corners, UnityEngine.Matrix4x4& viewWorldToLocalMatrix) { }
        // RVA: 0x033F6440  token: 0x6000459
        protected UnityEngine.Vector2 CalculateOffset(UnityEngine.Vector2 delta) { }
        // RVA: 0x033F6490  token: 0x600045A
        private static UnityEngine.Vector2 InternalCalculateOffset(UnityEngine.Bounds& viewBounds, UnityEngine.Bounds& contentBounds, System.Boolean horizontal, System.Boolean vertical, UnityEngine.UI.ScrollRect.MovementType movementType, UnityEngine.Vector2& delta) { }
        // RVA: 0x039D3B70  token: 0x600045B
        protected System.Void SetDirty() { }
        // RVA: 0x0B575060  token: 0x600045C
        protected System.Void SetDirtyCaching() { }
        // RVA: 0x053953F4  token: 0x600045D
        private virtual UnityEngine.Transform UnityEngine.UI.ICanvasElement.get_transform() { }

    }

    // TypeToken: 0x200006F  // size: 0x188
    public class Selectable : UnityEngine.EventSystems.UIBehaviour, UnityEngine.EventSystems.IMoveHandler, UnityEngine.EventSystems.IEventSystemHandler, UnityEngine.EventSystems.IPointerDownHandler, UnityEngine.EventSystems.IPointerUpHandler, UnityEngine.EventSystems.IPointerEnterHandler, UnityEngine.EventSystems.IPointerExitHandler, UnityEngine.EventSystems.ISelectHandler, UnityEngine.EventSystems.IDeselectHandler
    {
        // Fields
        protected static UnityEngine.UI.Selectable[] s_Selectables;  // static @ 0x0
        protected static System.Int32 s_SelectableCount;  // static @ 0x8
        private System.Boolean m_EnableCalled;  // 0x18
        private UnityEngine.UI.Navigation m_Navigation;  // 0x20
        private UnityEngine.UI.Selectable.Transition m_Transition;  // 0x48
        private UnityEngine.UI.ColorBlock m_Colors;  // 0x4c
        private UnityEngine.UI.SpriteState m_SpriteState;  // 0xa8
        private UnityEngine.UI.AnimationTriggers m_AnimationTriggers;  // 0xc8
        private System.Boolean m_Interactable;  // 0xd0
        private UnityEngine.UI.Graphic m_TargetGraphic;  // 0xd8
        protected System.Boolean m_NotifyHover;  // 0xe0
        protected UnityEngine.GameObject m_HoverEnableNode;  // 0xe8
        protected System.String m_HoverAudioKey;  // 0xf0
        protected System.Boolean _enableUnityNavi;  // 0xf8
        private System.Boolean m_GroupsAllowInteraction;  // 0xf9
        protected System.Int32 m_CurrentIndex;  // 0xfc
        private System.Boolean <isPointerInside>k__BackingField;  // 0x100
        private System.Boolean <isPointerDown>k__BackingField;  // 0x101
        private System.Boolean <hasSelection>k__BackingField;  // 0x102
        private readonly System.Collections.Generic.List<UnityEngine.CanvasGroup> m_CanvasGroupCache;  // 0x108
        public static UnityEngine.Events.UnityEvent<UnityEngine.UI.Selectable,System.Boolean> onAnyHoverChange;  // static @ 0x10
        private readonly UnityEngine.Events.UnityEvent<System.Boolean> <onHoverChange>k__BackingField;  // 0x110
        public System.Boolean enableControllerNavi;  // 0x118
        public UnityEngine.Transform overrideNaviHintRectTransform;  // 0x120
        public System.Single overrideNaviHintRectScale;  // 0x128
        public System.Boolean changeNaviHintParent;  // 0x12c
        public System.Boolean hideNaviHint;  // 0x12d
        public System.Boolean needNaviConfirmKeyHint;  // 0x12e
        public System.Boolean useExplicitNaviSelect;  // 0x12f
        public System.Boolean banExplicitOnLeft;  // 0x130
        public System.Boolean banExplicitOnRight;  // 0x131
        public System.Boolean banExplicitOnUp;  // 0x132
        public System.Boolean banExplicitOnDown;  // 0x133
        public System.Func<UnityEngine.UI.NaviDirection,UnityEngine.UI.Selectable> customNaviTargetInDirFunc;  // 0x138
        private UnityEngine.UI.Selectable m_ExplicitSelectOnLeft;  // 0x140
        private UnityEngine.UI.Selectable m_ExplicitSelectOnRight;  // 0x148
        private UnityEngine.UI.Selectable m_ExplicitSelectOnUp;  // 0x150
        private UnityEngine.UI.Selectable m_ExplicitSelectOnDown;  // 0x158
        private System.Boolean <isNaviTarget>k__BackingField;  // 0x160
        public System.Action<System.Boolean,System.Boolean,System.Boolean> onIsNaviTargetChanged;  // 0x168
        private System.Boolean <isNavigationSilent>k__BackingField;  // 0x170
        public UnityEngine.UI.SelectableNaviGroup naviGroup;  // 0x178
        private System.Boolean m_naviInited;  // 0x180

        // Properties
        UnityEngine.UI.Selectable[] allSelectablesArray { get; /* RVA: 0x0B577454 */ }
        System.Int32 allSelectableCount { get; /* RVA: 0x0B577404 */ }
        System.Collections.Generic.List<UnityEngine.UI.Selectable> allSelectables { get; /* RVA: 0x0B5774D8 */ }
        System.String hoverAudioKey { get; /* RVA: 0x04D8D1A0 */ }
        UnityEngine.UI.Navigation navigation { get; /* RVA: 0x04D8E8C0 */ set; /* RVA: 0x037D4B20 */ }
        UnityEngine.UI.Selectable.Transition transition { get; /* RVA: 0x04D863D0 */ set; /* RVA: 0x0B57776C */ }
        UnityEngine.UI.ColorBlock colors { get; /* RVA: 0x04DBF070 */ set; /* RVA: 0x0B577604 */ }
        UnityEngine.UI.SpriteState spriteState { get; /* RVA: 0x04DBF0B0 */ set; /* RVA: 0x0B5776A0 */ }
        UnityEngine.UI.AnimationTriggers animationTriggers { get; /* RVA: 0x04D8D1D0 */ set; /* RVA: 0x0B5775A8 */ }
        UnityEngine.UI.Graphic targetGraphic { get; /* RVA: 0x04D88EF0 */ set; /* RVA: 0x0B577710 */ }
        System.Boolean interactable { get; /* RVA: 0x04D90CF0 */ set; /* RVA: 0x03409A80 */ }
        System.Boolean isPointerInside { get; /* RVA: 0x04DA5D60 */ set; /* RVA: 0x04DBF0E0 */ }
        System.Boolean isPointerDown { get; /* RVA: 0x04DAA4D0 */ set; /* RVA: 0x04DBF0D0 */ }
        System.Boolean hasSelection { get; /* RVA: 0x04D8C8D0 */ set; /* RVA: 0x04D8C900 */ }
        UnityEngine.UI.Image image { get; /* RVA: 0x0B577568 */ set; /* RVA: 0x06902698 */ }
        UnityEngine.Animator animator { get; /* RVA: 0x05393874 */ }
        UnityEngine.UI.Selectable.SelectionState currentSelectionState { get; /* RVA: 0x03795D60 */ }
        UnityEngine.Events.UnityEvent<System.Boolean> onHoverChange { get; /* RVA: 0x04D8D1B0 */ }
        System.Boolean isNaviTarget { get; /* RVA: 0x04D8E6A0 */ set; /* RVA: 0x04D90350 */ }
        System.Boolean isNavigationSilent { get; /* RVA: 0x04D905A0 */ set; /* RVA: 0x04D905B0 */ }

        // Methods
        // RVA: 0x0B576240  token: 0x6000462
        public static System.Int32 AllSelectablesNoAlloc(UnityEngine.UI.Selectable[] selectables) { }
        // RVA: 0x03697410  token: 0x6000478
        protected System.Void .ctor() { }
        // RVA: 0x037D4960  token: 0x600047C
        protected virtual System.Void Awake() { }
        // RVA: 0x02FC9670  token: 0x600047D
        protected System.Void OnCanvasGroupChanged() { }
        // RVA: 0x03795DB0  token: 0x600047E
        public virtual System.Boolean IsInteractable() { }
        // RVA: 0x0B576E4C  token: 0x600047F
        protected virtual System.Void OnDidApplyAnimationProperties() { }
        // RVA: 0x03794EC0  token: 0x6000480
        protected virtual System.Void OnEnable() { }
        // RVA: 0x04892DC0  token: 0x6000481
        protected virtual System.Void OnTransformParentChanged() { }
        // RVA: 0x037D5010  token: 0x6000482
        private System.Void OnSetProperty() { }
        // RVA: 0x03795DD0  token: 0x6000483
        protected virtual System.Void OnDisable() { }
        // RVA: 0x037974D0  token: 0x6000484
        private System.Void OnApplicationFocus(System.Boolean hasFocus) { }
        // RVA: 0x037962A0  token: 0x6000486
        protected virtual System.Void InstantClearState() { }
        // RVA: 0x03796050  token: 0x6000487
        protected virtual System.Void DoStateTransition(UnityEngine.UI.Selectable.SelectionState state, System.Boolean instant) { }
        // RVA: 0x0B576638  token: 0x6000488
        public UnityEngine.UI.Selectable FindSelectable(UnityEngine.Vector3 dir) { }
        // RVA: 0x0B576B98  token: 0x6000489
        private static UnityEngine.Vector3 GetPointOnRectEdge(UnityEngine.RectTransform rect, UnityEngine.Vector2 dir) { }
        // RVA: 0x0B576DBC  token: 0x600048A
        private System.Void Navigate(UnityEngine.EventSystems.AxisEventData eventData, UnityEngine.UI.Selectable sel) { }
        // RVA: 0x0B576428  token: 0x600048B
        public virtual UnityEngine.UI.Selectable FindSelectableOnLeft() { }
        // RVA: 0x0B5764D8  token: 0x600048C
        public virtual UnityEngine.UI.Selectable FindSelectableOnRight() { }
        // RVA: 0x0B576588  token: 0x600048D
        public virtual UnityEngine.UI.Selectable FindSelectableOnUp() { }
        // RVA: 0x0B576378  token: 0x600048E
        public virtual UnityEngine.UI.Selectable FindSelectableOnDown() { }
        // RVA: 0x0B576E54  token: 0x600048F
        public virtual System.Void OnMove(UnityEngine.EventSystems.AxisEventData eventData) { }
        // RVA: 0x03796550  token: 0x6000490
        private System.Void StartColorTween(UnityEngine.Color targetColor, System.Boolean instant) { }
        // RVA: 0x0B5762E8  token: 0x6000491
        private System.Void DoSpriteSwap(UnityEngine.Sprite newSprite) { }
        // RVA: 0x037966B0  token: 0x6000492
        private System.Void TriggerAnimation(System.String triggername) { }
        // RVA: 0x0B576D6C  token: 0x6000493
        protected System.Boolean IsHighlighted() { }
        // RVA: 0x03797500  token: 0x6000494
        protected System.Boolean IsPressed() { }
        // RVA: 0x0379B190  token: 0x6000495
        private System.Void EvaluateAndTransitionToSelectionState() { }
        // RVA: 0x0379AC80  token: 0x6000496
        public virtual System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0379AC50  token: 0x6000497
        public virtual System.Void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0379B050  token: 0x6000498
        public virtual System.Void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0379B160  token: 0x6000499
        public virtual System.Void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x04CA2B20  token: 0x600049A
        public virtual System.Void OnSelect(UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x04B6C980  token: 0x600049B
        public virtual System.Void OnDeselect(UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0B576F30  token: 0x600049C
        public virtual System.Void Select() { }
        // RVA: 0x03793A20  token: 0x600049D
        protected virtual System.Void _RefreshHoverEnableNode(System.Boolean active) { }
        // RVA: 0x041E1670  token: 0x600049E
        public virtual System.Void OnInteractableChanged() { }
        // RVA: 0x031EAD80  token: 0x60004A0
        protected virtual System.Void _NotifyIsHover(System.Boolean isHover) { }
        // RVA: 0x0351DB80  token: 0x60004A1
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x0B576B70  token: 0x60004A2
        public virtual System.Boolean GetMouseActionHints(System.String& clickHintTextId, System.String& longPressHintTextId) { }
        // RVA: 0x0B576FFC  token: 0x60004A7
        public System.Void SetAsNaviTarget(System.Boolean isTarget, System.Boolean silentMode, System.Boolean isGroupChanged) { }
        // RVA: 0x0B576EC4  token: 0x60004A8
        public virtual System.Void OnNaviTargetEnabledAgain() { }
        // RVA: 0x041E1670  token: 0x60004A9
        protected virtual System.Void _OnSetAsNaviTarget(System.Boolean isTarget, System.Boolean silentMode) { }
        // RVA: 0x037D4B70  token: 0x60004AA
        public System.Void TryFindNaviGroup() { }
        // RVA: 0x0B576AF8  token: 0x60004AB
        public UnityEngine.UI.SelectableNaviGroup GetLayerGroup() { }
        // RVA: 0x0B5771A4  token: 0x60004AC
        public System.Boolean TryFindExplicitNaviTarget(UnityEngine.UI.NaviDirection dir, UnityEngine.UI.Selectable& target) { }
        // RVA: 0x0B57732C  token: 0x60004AD
        public System.Boolean banExplicitNavi(UnityEngine.UI.NaviDirection dir) { }
        // RVA: 0x03795F90  token: 0x60004AE
        private System.Void _TryRegisterOnNaviGroup() { }
        // RVA: 0x0351DBC0  token: 0x60004AF
        private System.Void _TryUnRegisterOnNaviGroup() { }
        // RVA: 0x06B8B404  token: 0x60004B0
        public System.Void SetExplicitSelectOnLeft(UnityEngine.UI.Selectable selectable) { }
        // RVA: 0x06B8B468  token: 0x60004B1
        public System.Void SetExplicitSelectOnRight(UnityEngine.UI.Selectable selectable) { }
        // RVA: 0x06B8B224  token: 0x60004B2
        public System.Void SetExplicitSelectOnUp(UnityEngine.UI.Selectable selectable) { }
        // RVA: 0x06B8B274  token: 0x60004B3
        public System.Void SetExplicitSelectOnDown(UnityEngine.UI.Selectable selectable) { }
        // RVA: 0x0B577144  token: 0x60004B4
        public System.Void SetExplicitSelect(UnityEngine.UI.Selectable left, UnityEngine.UI.Selectable right, UnityEngine.UI.Selectable up, UnityEngine.UI.Selectable down) { }
        // RVA: 0x04CB0DE0  token: 0x60004B5
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000072  // size: 0x14
    public sealed struct NaviDirection
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.NaviDirection None;  // const
        public static UnityEngine.UI.NaviDirection Up;  // const
        public static UnityEngine.UI.NaviDirection Down;  // const
        public static UnityEngine.UI.NaviDirection Left;  // const
        public static UnityEngine.UI.NaviDirection Right;  // const

    }

    // TypeToken: 0x2000073  // size: 0x14
    public sealed struct NaviStrategy
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.NaviStrategy Normal;  // const
        public static UnityEngine.UI.NaviStrategy Strict;  // const
        public static UnityEngine.UI.NaviStrategy NotInternalFirst;  // const
        public static UnityEngine.UI.NaviStrategy VerticalOnlyWithInternalWrap;  // const
        public static UnityEngine.UI.NaviStrategy HorizontalOnlyWithInternalWrap;  // const

    }

    // TypeToken: 0x2000074  // size: 0x14
    public sealed struct NavigationBindingType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UI.NavigationBindingType InValid;  // const
        public static UnityEngine.UI.NavigationBindingType AllDirections;  // const
        public static UnityEngine.UI.NavigationBindingType HorizontalOnly;  // const
        public static UnityEngine.UI.NavigationBindingType VerticalOnly;  // const
        public static UnityEngine.UI.NavigationBindingType ArrowAllDirections;  // const
        public static UnityEngine.UI.NavigationBindingType ArrowHorizontalOnly;  // const
        public static UnityEngine.UI.NavigationBindingType ArrowVerticalOnly;  // const
        public static UnityEngine.UI.NavigationBindingType RightJsAllDirections;  // const
        public static UnityEngine.UI.NavigationBindingType RightJsHorizontalOnly;  // const
        public static UnityEngine.UI.NavigationBindingType RightJsVerticalOnly;  // const
        public static UnityEngine.UI.NavigationBindingType RightJsOnlyAllDirections;  // const
        public static UnityEngine.UI.NavigationBindingType RightJsOnlyHorizontalOnly;  // const
        public static UnityEngine.UI.NavigationBindingType RightJsOnlyVerticalOnly;  // const

    }

    // TypeToken: 0x2000075  // size: 0xA0
    public abstract class SelectableNaviGroup : UnityEngine.MonoBehaviour
    {
        // Fields
        public System.Boolean isIsolate;  // 0x18
        public System.Boolean isLayer;  // 0x19
        public System.Boolean removeLayerOnDisable;  // 0x1a
        public System.Boolean isDummy;  // 0x1b
        public UnityEngine.UI.SelectableNaviGroup parentNaviGroup;  // 0x20
        public UnityEngine.UI.NaviStrategy internalNaviStrategy;  // 0x28
        public UnityEngine.UI.NavigationBindingType navigationBindingType;  // 0x2c
        public System.Boolean enablePartner;  // 0x30
        public System.Collections.Generic.List<UnityEngine.UI.SelectableNaviGroup> naviPartnerOnUp;  // 0x38
        public System.Collections.Generic.List<UnityEngine.UI.SelectableNaviGroup> naviPartnerOnDown;  // 0x40
        public System.Collections.Generic.List<UnityEngine.UI.SelectableNaviGroup> naviPartnerOnLeft;  // 0x48
        public System.Collections.Generic.List<UnityEngine.UI.SelectableNaviGroup> naviPartnerOnRight;  // 0x50
        protected System.Boolean m_inited;  // 0x58
        protected UnityEngine.UI.Selectable m_layerSelectedTarget;  // 0x60
        public System.Collections.Generic.HashSet<UnityEngine.UI.Selectable> targets;  // 0x68
        public System.Collections.Generic.HashSet<UnityEngine.UI.SelectableNaviGroup> subGroups;  // 0x70
        public UnityEngine.Events.UnityEvent<UnityEngine.UI.Selectable> onSetLayerSelectedTarget;  // 0x78
        public UnityEngine.Events.UnityEvent<System.Boolean> onIsTopLayerChanged;  // 0x80
        public UnityEngine.Events.UnityEvent<UnityEngine.UI.NaviDirection> onDefaultNaviFailed;  // 0x88
        public UnityEngine.Events.UnityEvent onRemoveFromLayerStack;  // 0x90
        protected System.Boolean m_isTopLayer;  // 0x98

        // Properties
        UnityEngine.UI.Selectable LayerSelectedTarget { get; /* RVA: 0x037D2DD0 */ }
        System.Boolean IsTopLayer { get; /* RVA: 0x04D86C50 */ set; /* RVA: 0x0B576228 */ }

        // Methods
        // RVA: 0x0B576220  token: 0x60004B7
        protected virtual System.Void Reset() { }
        // RVA: 0x037D3D90  token: 0x60004B8
        protected virtual System.Void OnEnable() { }
        // RVA: 0x053979C0  token: 0x60004B9
        protected virtual System.Void OnDisable() { }
        // RVA: 0x039CFBF0  token: 0x60004BA
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x037D3E20  token: 0x60004BB
        protected virtual System.Void _Init() { }
        // RVA: 0x037D4820  token: 0x60004BC
        public System.Void TryFindParentNaviGroup() { }
        // RVA: 0x037D1390  token: 0x60004BD
        public UnityEngine.UI.SelectableNaviGroup GetLayerGroup() { }
        // RVA: 0x037D1280  token: 0x60004C0
        public System.Void SetLayerSelectedTarget(UnityEngine.UI.Selectable value, System.Boolean silentMode) { }
        // RVA: 0x037D14D0  token: 0x60004C1
        public System.Void OnRemoveFromLayerStack() { }
        // RVA: 0x037D3FE0  token: 0x60004C2
        protected virtual System.Void _OnIsTopLayerChanged() { }
        // RVA: 0x041E1670  token: 0x60004C3
        protected virtual System.Void _OnSelectableNaviGroupInited() { }
        // RVA: 0x041E1670  token: 0x60004C4
        protected virtual System.Void _BeforeSelectableNaviGroupDestroy() { }
        // RVA: 0x041E1670  token: 0x60004C5
        protected virtual System.Void _OnSelectableNaviGroupDisabled() { }
        // RVA: 0x041E1670  token: 0x60004C6
        protected virtual System.Void _OnSetLayerSelectedTarget(UnityEngine.UI.Selectable value) { }
        // RVA: 0x041E1670  token: 0x60004C7
        protected virtual System.Void _OnRemoveFromLayerStack() { }
        // RVA: 0x040F99E0  token: 0x60004C8
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000076  // size: 0x10
    public static class SetPropertyUtility
    {
        // Methods
        // RVA: 0x03046980  token: 0x60004C9
        public static System.Boolean SetColor(UnityEngine.Color& currentValue, UnityEngine.Color newValue) { }
        // RVA: -1  // generic def  token: 0x60004CA
        public static System.Boolean SetStruct(T& currentValue, T newValue) { }
        // RVA: -1  // generic def  token: 0x60004CB
        public static System.Boolean SetClass(T& currentValue, T newValue) { }

    }

    // TypeToken: 0x2000077  // size: 0x1F0
    public class Slider : UnityEngine.UI.Selectable, UnityEngine.EventSystems.IDragHandler, UnityEngine.EventSystems.IEventSystemHandler, UnityEngine.EventSystems.IInitializePotentialDragHandler, UnityEngine.UI.ICanvasElement
    {
        // Fields
        private UnityEngine.RectTransform m_FillRect;  // 0x188
        private UnityEngine.RectTransform m_HandleRect;  // 0x190
        private UnityEngine.UI.Slider.Direction m_Direction;  // 0x198
        private System.Single m_MinValue;  // 0x19c
        private System.Single m_MaxValue;  // 0x1a0
        private System.Boolean m_WholeNumbers;  // 0x1a4
        protected System.Single m_Value;  // 0x1a8
        private UnityEngine.UI.Slider.SliderEvent m_OnValueChanged;  // 0x1b0
        private UnityEngine.UI.Image m_FillImage;  // 0x1b8
        private UnityEngine.Transform m_FillTransform;  // 0x1c0
        private UnityEngine.RectTransform m_FillContainerRect;  // 0x1c8
        private UnityEngine.Transform m_HandleTransform;  // 0x1d0
        private UnityEngine.RectTransform m_HandleContainerRect;  // 0x1d8
        private UnityEngine.Vector2 m_Offset;  // 0x1e0
        private UnityEngine.DrivenRectTransformTracker m_Tracker;  // 0x1e8
        private System.Boolean m_DelayedUpdateVisuals;  // 0x1e9

        // Properties
        UnityEngine.RectTransform fillRect { get; /* RVA: 0x04D8DA20 */ set; /* RVA: 0x0B5782F8 */ }
        UnityEngine.RectTransform handleRect { get; /* RVA: 0x04D8DD50 */ set; /* RVA: 0x0B578360 */ }
        UnityEngine.UI.Slider.Direction direction { get; /* RVA: 0x04D8F930 */ set; /* RVA: 0x0B57829C */ }
        System.Single minValue { get; /* RVA: 0x04DBF0F0 */ set; /* RVA: 0x046FF340 */ }
        System.Single maxValue { get; /* RVA: 0x04D8E940 */ set; /* RVA: 0x046FF500 */ }
        System.Boolean wholeNumbers { get; /* RVA: 0x04DBF100 */ set; /* RVA: 0x0B5783F8 */ }
        System.Single value { get; /* RVA: 0x03936F60 */ set; /* RVA: 0x05390984 */ }
        System.Single normalizedValue { get; /* RVA: 0x03936EB0 */ set; /* RVA: 0x0B5783C8 */ }
        UnityEngine.UI.Slider.SliderEvent onValueChanged { get; /* RVA: 0x04D8D960 */ set; /* RVA: 0x053971DC */ }
        System.Single stepSize { get; /* RVA: 0x0B578270 */ }
        UnityEngine.UI.Slider.Axis axis { get; /* RVA: 0x03936E90 */ }
        System.Boolean reverseValue { get; /* RVA: 0x03936F90 */ }

        // Methods
        // RVA: 0x0B578034  token: 0x60004DA
        public virtual System.Void SetValueWithoutNotify(System.Single input) { }
        // RVA: 0x0435C2D0  token: 0x60004E0
        protected System.Void .ctor() { }
        // RVA: 0x041E1670  token: 0x60004E1
        public virtual System.Void Rebuild(UnityEngine.UI.CanvasUpdate executing) { }
        // RVA: 0x041E1670  token: 0x60004E2
        public virtual System.Void LayoutComplete() { }
        // RVA: 0x041E1670  token: 0x60004E3
        public virtual System.Void GraphicUpdateComplete() { }
        // RVA: 0x03935610  token: 0x60004E4
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0447EC90  token: 0x60004E5
        protected virtual System.Void OnDisable() { }
        // RVA: 0x0419EE40  token: 0x60004E6
        protected virtual System.Void Update() { }
        // RVA: 0x0B5778C8  token: 0x60004E7
        protected virtual System.Void OnDidApplyAnimationProperties() { }
        // RVA: 0x03937030  token: 0x60004E8
        private System.Void UpdateCachedReferences() { }
        // RVA: 0x0B5777C4  token: 0x60004E9
        protected virtual System.Single ClampValue(System.Single input) { }
        // RVA: 0x03936A10  token: 0x60004EA
        protected virtual System.Void Set(System.Single input, System.Boolean sendCallback) { }
        // RVA: 0x041E1670  token: 0x60004EB
        protected virtual System.Void OnValueChanged() { }
        // RVA: 0x041E1670  token: 0x60004EC
        protected virtual System.Void OnClickSliderBar() { }
        // RVA: 0x041E1670  token: 0x60004ED
        protected virtual System.Void OnClickSliderHandle() { }
        // RVA: 0x04736930  token: 0x60004EE
        protected virtual System.Void OnRectTransformDimensionsChange() { }
        // RVA: 0x03936AE0  token: 0x60004F1
        private System.Void UpdateVisuals() { }
        // RVA: 0x0B578040  token: 0x60004F2
        private System.Void UpdateDrag(UnityEngine.EventSystems.PointerEventData eventData, UnityEngine.Camera cam) { }
        // RVA: 0x04D9A920  token: 0x60004F3
        protected virtual System.Single CustomProcessNormalizedValue(System.Single val) { }
        // RVA: 0x0B575710  token: 0x60004F4
        private System.Boolean MayDrag(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B577D1C  token: 0x60004F5
        public virtual System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B577AE4  token: 0x60004F6
        public virtual System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B577B34  token: 0x60004F7
        public virtual System.Void OnMove(UnityEngine.EventSystems.AxisEventData eventData) { }
        // RVA: 0x0B57782C  token: 0x60004F8
        public virtual UnityEngine.UI.Selectable FindSelectableOnLeft() { }
        // RVA: 0x0B577860  token: 0x60004F9
        public virtual UnityEngine.UI.Selectable FindSelectableOnRight() { }
        // RVA: 0x0B577894  token: 0x60004FA
        public virtual UnityEngine.UI.Selectable FindSelectableOnUp() { }
        // RVA: 0x0B5777F8  token: 0x60004FB
        public virtual UnityEngine.UI.Selectable FindSelectableOnDown() { }
        // RVA: 0x0B5759FC  token: 0x60004FC
        public virtual System.Void OnInitializePotentialDrag(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B577EF8  token: 0x60004FD
        public System.Void SetDirection(UnityEngine.UI.Slider.Direction direction, System.Boolean includeRectLayouts) { }
        // RVA: 0x053953F4  token: 0x60004FE
        private virtual UnityEngine.Transform UnityEngine.UI.ICanvasElement.get_transform() { }

    }

    // TypeToken: 0x200007B  // size: 0x30
    public sealed struct SpriteState : System.IEquatable`1
    {
        // Fields
        private UnityEngine.Sprite m_HighlightedSprite;  // 0x10
        private UnityEngine.Sprite m_PressedSprite;  // 0x18
        private UnityEngine.Sprite m_SelectedSprite;  // 0x20
        private UnityEngine.Sprite m_DisabledSprite;  // 0x28

        // Properties
        UnityEngine.Sprite highlightedSprite { get; /* RVA: 0x02B76770 */ set; /* RVA: 0x05392320 */ }
        UnityEngine.Sprite pressedSprite { get; /* RVA: 0x04D88320 */ set; /* RVA: 0x067D5E9C */ }
        UnityEngine.Sprite selectedSprite { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        UnityEngine.Sprite disabledSprite { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }

        // Methods
        // RVA: 0x0B57846C  token: 0x6000508
        public virtual System.Boolean Equals(UnityEngine.UI.SpriteState other) { }

    }

    // TypeToken: 0x200007C  // size: 0x10
    public static class StencilMaterial
    {
        // Fields
        private static System.Collections.Generic.List<UnityEngine.UI.StencilMaterial.MatEntry> m_List;  // static @ 0x0
        private static System.Boolean s_EnableStencilMaterialWarning;  // static @ 0x8

        // Methods
        // RVA: 0x011EC580  token: 0x6000509
        public static UnityEngine.Material Add(UnityEngine.Material baseMat, System.Int32 stencilID) { }
        // RVA: 0x041D2250  token: 0x600050A
        public static UnityEngine.Material Add(UnityEngine.Material baseMat, System.Int32 stencilID, UnityEngine.Rendering.StencilOp operation, UnityEngine.Rendering.CompareFunction compareFunction, UnityEngine.Rendering.ColorWriteMask colorWriteMask) { }
        // RVA: 0x0B57864C  token: 0x600050B
        private static System.Void LogWarningWhenNotInBatchmode(System.String warning, UnityEngine.Object context) { }
        // RVA: 0x031DEDB0  token: 0x600050C
        public static UnityEngine.Material Add(UnityEngine.Material baseMat, System.Int32 stencilID, UnityEngine.Rendering.StencilOp operation, UnityEngine.Rendering.CompareFunction compareFunction, UnityEngine.Rendering.ColorWriteMask colorWriteMask, System.Int32 readMask, System.Int32 writeMask) { }
        // RVA: 0x0351C220  token: 0x600050D
        public static System.Void Remove(UnityEngine.Material customMat) { }
        // RVA: 0x0B578538  token: 0x600050E
        public static System.Void ClearAll() { }
        // RVA: 0x04CECE10  token: 0x600050F
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200007E  // size: 0x118
    public class Text : UnityEngine.UI.MaskableGraphic, UnityEngine.UI.ILayoutElement
    {
        // Fields
        private UnityEngine.UI.FontData m_FontData;  // 0xe8
        protected System.String m_Text;  // 0xf0
        private UnityEngine.TextGenerator m_TextCache;  // 0xf8
        private UnityEngine.TextGenerator m_TextCacheForLayout;  // 0x100
        protected static UnityEngine.Material s_DefaultText;  // static @ 0x0
        protected System.Boolean m_DisableFontTextureRebuiltCallback;  // 0x108
        private readonly UnityEngine.UIVertex[] m_TempVerts;  // 0x110

        // Properties
        System.Boolean hasCullOverride { get; /* RVA: 0x012081B0 */ }
        UnityEngine.TextGenerator cachedTextGenerator { get; /* RVA: 0x0B579360 */ }
        UnityEngine.TextGenerator cachedTextGeneratorForLayout { get; /* RVA: 0x0B5792E4 */ }
        UnityEngine.Texture mainTexture { get; /* RVA: 0x0B5794B4 */ }
        UnityEngine.Font font { get; /* RVA: 0x0B579454 */ set; /* RVA: 0x0B579B38 */ }
        System.String text { get; /* RVA: 0x04D8D1A0 */ set; /* RVA: 0x0B579DCC */ }
        System.Boolean supportRichText { get; /* RVA: 0x0B5799F0 */ set; /* RVA: 0x0B579D88 */ }
        System.Boolean resizeTextForBestFit { get; /* RVA: 0x0B579990 */ set; /* RVA: 0x0B579CBC */ }
        System.Int32 resizeTextMinSize { get; /* RVA: 0x0B5799D0 */ set; /* RVA: 0x0B579D44 */ }
        System.Int32 resizeTextMaxSize { get; /* RVA: 0x0B5799B0 */ set; /* RVA: 0x0B579D00 */ }
        UnityEngine.TextAnchor alignment { get; /* RVA: 0x0B5792C4 */ set; /* RVA: 0x0B579A6C */ }
        System.Boolean alignByGeometry { get; /* RVA: 0x0B5792A4 */ set; /* RVA: 0x0B579A30 */ }
        System.Int32 fontSize { get; /* RVA: 0x0B579414 */ set; /* RVA: 0x0B579AB0 */ }
        UnityEngine.HorizontalWrapMode horizontalOverflow { get; /* RVA: 0x0B579474 */ set; /* RVA: 0x0B579C2C */ }
        UnityEngine.VerticalWrapMode verticalOverflow { get; /* RVA: 0x0B579A10 */ set; /* RVA: 0x0B579E98 */ }
        System.Single lineSpacing { get; /* RVA: 0x0B579494 */ set; /* RVA: 0x0B579C70 */ }
        UnityEngine.FontStyle fontStyle { get; /* RVA: 0x0B579434 */ set; /* RVA: 0x0B579AF4 */ }
        System.Single pixelsPerUnit { get; /* RVA: 0x0B57963C */ }
        System.Single minWidth { get; /* RVA: 0x04D879D0 */ }
        System.Single preferredWidth { get; /* RVA: 0x0B5798A0 */ }
        System.Single flexibleWidth { get; /* RVA: 0x04DA9E10 */ }
        System.Single minHeight { get; /* RVA: 0x04D879D0 */ }
        System.Single preferredHeight { get; /* RVA: 0x0B579778 */ }
        System.Single flexibleHeight { get; /* RVA: 0x04DA9E10 */ }
        System.Int32 layoutPriority { get; /* RVA: 0x011EC580 */ }

        // Methods
        // RVA: 0x0B5791F8  token: 0x6000512
        protected System.Void .ctor() { }
        // RVA: 0x0B5787C4  token: 0x6000516
        public System.Void FontTextureChanged() { }
        // RVA: 0x0B578B70  token: 0x6000532
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0B578B24  token: 0x6000533
        protected virtual System.Void OnDisable() { }
        // RVA: 0x0B579190  token: 0x6000534
        protected virtual System.Void UpdateGeometry() { }
        // RVA: 0x0B57876C  token: 0x6000535
        public System.Void AssignDefaultFont() { }
        // RVA: 0x0B5786D4  token: 0x6000536
        private System.Void AssignDefaultFontIfNecessary() { }
        // RVA: 0x0B57888C  token: 0x6000537
        public UnityEngine.TextGenerationSettings GetGenerationSettings(UnityEngine.Vector2 extents) { }
        // RVA: 0x0B578A98  token: 0x6000538
        public static UnityEngine.Vector2 GetTextAnchorPivot(UnityEngine.TextAnchor anchor) { }
        // RVA: 0x0B578BD8  token: 0x6000539
        protected virtual System.Void OnPopulateMesh(UnityEngine.UI.VertexHelper toFill) { }
        // RVA: 0x041E1670  token: 0x600053A
        public virtual System.Void CalculateLayoutInputHorizontal() { }
        // RVA: 0x041E1670  token: 0x600053B
        public virtual System.Void CalculateLayoutInputVertical() { }

    }

    // TypeToken: 0x200007F  // size: 0x1B0
    public class Toggle : UnityEngine.UI.Selectable, UnityEngine.EventSystems.IPointerClickHandler, UnityEngine.EventSystems.IEventSystemHandler, UnityEngine.EventSystems.ISubmitHandler, UnityEngine.UI.ICanvasElement
    {
        // Fields
        public UnityEngine.UI.Toggle.ToggleTransition toggleTransition;  // 0x188
        public UnityEngine.UI.Graphic graphic;  // 0x190
        private UnityEngine.UI.ToggleGroup m_Group;  // 0x198
        public UnityEngine.UI.Toggle.ToggleEvent onValueChanged;  // 0x1a0
        private System.Boolean m_IsOn;  // 0x1a8

        // Properties
        UnityEngine.UI.ToggleGroup group { get; /* RVA: 0x04D8DED0 */ set; /* RVA: 0x0B57A54C */ }
        System.Boolean isOn { get; /* RVA: 0x04DA3310 */ set; /* RVA: 0x0378E5A0 */ }

        // Methods
        // RVA: 0x0435DEF0  token: 0x6000545
        protected System.Void .ctor() { }
        // RVA: 0x041E1670  token: 0x6000546
        public virtual System.Void Rebuild(UnityEngine.UI.CanvasUpdate executing) { }
        // RVA: 0x041E1670  token: 0x6000547
        public virtual System.Void LayoutComplete() { }
        // RVA: 0x041E1670  token: 0x6000548
        public virtual System.Void GraphicUpdateComplete() { }
        // RVA: 0x0B57A390  token: 0x6000549
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x0378E820  token: 0x600054A
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0378E750  token: 0x600054B
        protected virtual System.Void OnDisable() { }
        // RVA: 0x0B57A40C  token: 0x600054C
        protected virtual System.Void OnDidApplyAnimationProperties() { }
        // RVA: 0x0378E9F0  token: 0x600054D
        private System.Void SetToggleGroup(UnityEngine.UI.ToggleGroup newGroup, System.Boolean setMemberValue) { }
        // RVA: 0x053979D0  token: 0x6000550
        public System.Void SetIsOnWithoutNotify(System.Boolean value) { }
        // RVA: 0x0378E5B0  token: 0x6000551
        private System.Void Set(System.Boolean value, System.Boolean sendCallback) { }
        // RVA: 0x0378E920  token: 0x6000552
        private System.Void PlayEffect(System.Boolean instant) { }
        // RVA: 0x05393868  token: 0x6000553
        protected virtual System.Void Start() { }
        // RVA: 0x0B57A348  token: 0x6000554
        protected virtual System.Void InternalToggle() { }
        // RVA: 0x0B57A518  token: 0x6000555
        public virtual System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0AC6E908  token: 0x6000556
        public virtual System.Void OnSubmit(UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x041E1670  token: 0x6000557
        protected virtual System.Void OnValueChange() { }
        // RVA: 0x053953F4  token: 0x6000558
        private virtual UnityEngine.Transform UnityEngine.UI.ICanvasElement.get_transform() { }

    }

    // TypeToken: 0x2000082  // size: 0x28
    public class ToggleGroup : UnityEngine.EventSystems.UIBehaviour
    {
        // Fields
        private System.Boolean m_AllowSwitchOff;  // 0x18
        protected System.Collections.Generic.List<UnityEngine.UI.Toggle> m_Toggles;  // 0x20

        // Properties
        System.Boolean allowSwitchOff { get; /* RVA: 0x04D86340 */ set; /* RVA: 0x04D86350 */ }

        // Methods
        // RVA: 0x045C5E70  token: 0x600055C
        protected System.Void .ctor() { }
        // RVA: 0x0481BFF0  token: 0x600055D
        protected virtual System.Void Start() { }
        // RVA: 0x0481BFF0  token: 0x600055E
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0B57A1FC  token: 0x600055F
        private System.Void ValidateToggleIsInGroup(UnityEngine.UI.Toggle toggle) { }
        // RVA: 0x0B579F4C  token: 0x6000560
        public System.Void NotifyToggleOn(UnityEngine.UI.Toggle toggle, System.Boolean sendCallback) { }
        // RVA: 0x0B57A1A8  token: 0x6000561
        public System.Void UnregisterToggle(UnityEngine.UI.Toggle toggle) { }
        // RVA: 0x0B57A07C  token: 0x6000562
        public System.Void RegisterToggle(UnityEngine.UI.Toggle toggle) { }
        // RVA: 0x0481C000  token: 0x6000563
        public System.Void EnsureValidState() { }
        // RVA: 0x0481C340  token: 0x6000564
        public System.Boolean AnyTogglesOn() { }
        // RVA: 0x0481C260  token: 0x6000565
        public System.Collections.Generic.IEnumerable<UnityEngine.UI.Toggle> ActiveToggles() { }
        // RVA: 0x0B579EDC  token: 0x6000566
        public UnityEngine.UI.Toggle GetFirstActiveToggle() { }
        // RVA: 0x0B57A0D0  token: 0x6000567
        public System.Void SetAllTogglesOff(System.Boolean sendCallback) { }

    }

    // TypeToken: 0x2000084  // size: 0x40
    public class ReflectionMethodsCache
    {
        // Fields
        public UnityEngine.UI.ReflectionMethodsCache.Raycast3DCallback raycast3D;  // 0x10
        public UnityEngine.UI.ReflectionMethodsCache.RaycastAllCallback raycast3DAll;  // 0x18
        public UnityEngine.UI.ReflectionMethodsCache.GetRaycastNonAllocCallback getRaycastNonAlloc;  // 0x20
        public UnityEngine.UI.ReflectionMethodsCache.Raycast2DCallback raycast2D;  // 0x28
        public UnityEngine.UI.ReflectionMethodsCache.GetRayIntersectionAllCallback getRayIntersectionAll;  // 0x30
        public UnityEngine.UI.ReflectionMethodsCache.GetRayIntersectionAllNonAllocCallback getRayIntersectionAllNonAlloc;  // 0x38
        private static UnityEngine.UI.ReflectionMethodsCache s_ReflectionMethodsCache;  // static @ 0x0

        // Properties
        UnityEngine.UI.ReflectionMethodsCache Singleton { get; /* RVA: 0x0B574964 */ }

        // Methods
        // RVA: 0x0B573EA8  token: 0x600056C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200008B  // size: 0x60
    public class VertexHelper : System.IDisposable
    {
        // Fields
        private System.Collections.Generic.List<UnityEngine.Vector3> m_Positions;  // 0x10
        private System.Collections.Generic.List<UnityEngine.Color32> m_Colors;  // 0x18
        private System.Collections.Generic.List<UnityEngine.Vector4> m_Uv0S;  // 0x20
        private System.Collections.Generic.List<UnityEngine.Vector4> m_Uv1S;  // 0x28
        private System.Collections.Generic.List<UnityEngine.Vector4> m_Uv2S;  // 0x30
        private System.Collections.Generic.List<UnityEngine.Vector4> m_Uv3S;  // 0x38
        private System.Collections.Generic.List<UnityEngine.Vector3> m_Normals;  // 0x40
        private System.Collections.Generic.List<UnityEngine.Vector4> m_Tangents;  // 0x48
        private System.Collections.Generic.List<System.Int32> m_Indices;  // 0x50
        private static readonly UnityEngine.Vector4 s_DefaultTangent;  // static @ 0x0
        private static readonly UnityEngine.Vector3 s_DefaultNormal;  // static @ 0x10
        private System.Boolean m_ListsInitalized;  // 0x58

        // Properties
        System.Int32 currentVertCount { get; /* RVA: 0x03141B90 */ }
        System.Int32 currentIndexCount { get; /* RVA: 0x0B57B0F0 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000586
        public System.Void .ctor() { }
        // RVA: 0x0B57AEB8  token: 0x6000587
        public System.Void .ctor(UnityEngine.Mesh m) { }
        // RVA: 0x03242670  token: 0x6000588
        private System.Void InitializeListIfRequired() { }
        // RVA: 0x04028CD0  token: 0x6000589
        public virtual System.Void Dispose() { }
        // RVA: 0x031F9AA0  token: 0x600058A
        public System.Void Clear() { }
        // RVA: 0x0B57A9B4  token: 0x600058D
        public System.Void PopulateUIVertex(UnityEngine.UIVertex& vertex, System.Int32 i) { }
        // RVA: 0x0B57AB70  token: 0x600058E
        public System.Void SetUIVertex(UnityEngine.UIVertex vertex, System.Int32 i) { }
        // RVA: 0x031F3240  token: 0x600058F
        public System.Void FillMesh(UnityEngine.Mesh mesh) { }
        // RVA: 0x03241FE0  token: 0x6000590
        public System.Void AddVert(UnityEngine.Vector3 position, UnityEngine.Color32 color, UnityEngine.Vector4 uv0, UnityEngine.Vector4 uv1, UnityEngine.Vector4 uv2, UnityEngine.Vector4 uv3, UnityEngine.Vector3 normal, UnityEngine.Vector4 tangent) { }
        // RVA: 0x03241930  token: 0x6000591
        public System.Void AddVert(UnityEngine.Vector3 position, UnityEngine.Color32 color, UnityEngine.Vector4 uv0, UnityEngine.Vector4 uv1, UnityEngine.Vector3 normal, UnityEngine.Vector4 tangent) { }
        // RVA: 0x03241250  token: 0x6000592
        public System.Void AddVert(UnityEngine.Vector3 position, UnityEngine.Color32 color, UnityEngine.Vector4 uv0) { }
        // RVA: 0x03BA6F90  token: 0x6000593
        public System.Void AddVert(UnityEngine.UIVertex v) { }
        // RVA: 0x03131840  token: 0x6000594
        public System.Void AddTriangle(System.Int32 idx0, System.Int32 idx1, System.Int32 idx2) { }
        // RVA: 0x037972C0  token: 0x6000595
        public System.Void AddUIVertexQuad(UnityEngine.UIVertex[] verts) { }
        // RVA: 0x0B57A804  token: 0x6000596
        public System.Void AddUIVertexStream(System.Collections.Generic.List<UnityEngine.UIVertex> verts, System.Collections.Generic.List<System.Int32> indices) { }
        // RVA: 0x0B57A8C4  token: 0x6000597
        public System.Void AddUIVertexTriangleStream(System.Collections.Generic.List<UnityEngine.UIVertex> verts) { }
        // RVA: 0x0B57A93C  token: 0x6000598
        public System.Void GetUIVertexStream(System.Collections.Generic.List<UnityEngine.UIVertex> stream) { }
        // RVA: 0x049704A0  token: 0x6000599
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200008C  // size: 0x10
    public abstract class BaseVertexEffect
    {
        // Methods
        // RVA: -1  // abstract  token: 0x600059A
        public virtual System.Void ModifyVertices(System.Collections.Generic.List<UnityEngine.UIVertex> vertices) { }
        // RVA: 0x041E1670  token: 0x600059B
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200008D  // size: 0x20
    public abstract class BaseMeshEffect : UnityEngine.EventSystems.UIBehaviour, UnityEngine.UI.IMeshModifier
    {
        // Fields
        private UnityEngine.UI.Graphic m_Graphic;  // 0x18

        // Properties
        UnityEngine.UI.Graphic graphic { get; /* RVA: 0x0B57BF74 */ }

        // Methods
        // RVA: 0x0B57BEF4  token: 0x600059D
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0B57BE74  token: 0x600059E
        protected virtual System.Void OnDisable() { }
        // RVA: 0x0B57BDF4  token: 0x600059F
        protected virtual System.Void OnDidApplyAnimationProperties() { }
        // RVA: 0x0B57BCF0  token: 0x60005A0
        public virtual System.Void ModifyMesh(UnityEngine.Mesh mesh) { }
        // RVA: -1  // abstract  token: 0x60005A1
        public virtual System.Void ModifyMesh(UnityEngine.UI.VertexHelper vh) { }
        // RVA: 0x05393520  token: 0x60005A2
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200008E
    public interface IVertexModifier
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60005A3
        public virtual System.Void ModifyVertices(System.Collections.Generic.List<UnityEngine.UIVertex> verts) { }

    }

    // TypeToken: 0x200008F
    public interface IMeshModifier
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60005A4
        public virtual System.Void ModifyMesh(UnityEngine.Mesh mesh) { }
        // RVA: -1  // abstract  token: 0x60005A5
        public virtual System.Void ModifyMesh(UnityEngine.UI.VertexHelper verts) { }

    }

    // TypeToken: 0x2000090  // size: 0x40
    public class Outline : UnityEngine.UI.Shadow
    {
        // Methods
        // RVA: 0x0B57DC84  token: 0x60005A6
        protected System.Void .ctor() { }
        // RVA: 0x0B57D9EC  token: 0x60005A7
        public virtual System.Void ModifyMesh(UnityEngine.UI.VertexHelper vh) { }

    }

    // TypeToken: 0x2000091  // size: 0x20
    public class PositionAsUV1 : UnityEngine.UI.BaseMeshEffect
    {
        // Methods
        // RVA: 0x05393520  token: 0x60005A8
        protected System.Void .ctor() { }
        // RVA: 0x0B582B30  token: 0x60005A9
        public virtual System.Void ModifyMesh(UnityEngine.UI.VertexHelper vh) { }

    }

    // TypeToken: 0x2000092  // size: 0x40
    public class Shadow : UnityEngine.UI.BaseMeshEffect
    {
        // Fields
        private UnityEngine.Color m_EffectColor;  // 0x20
        private UnityEngine.Vector2 m_EffectDistance;  // 0x30
        private System.Boolean m_UseGraphicAlpha;  // 0x38
        private static System.Single kMaxEffectDistance;  // const

        // Properties
        UnityEngine.Color effectColor { get; /* RVA: 0x04D8C200 */ set; /* RVA: 0x0B583E40 */ }
        UnityEngine.Vector2 effectDistance { get; /* RVA: 0x04D9E6C0 */ set; /* RVA: 0x0B583ECC */ }
        System.Boolean useGraphicAlpha { get; /* RVA: 0x011F2A70 */ set; /* RVA: 0x0B583FF4 */ }

        // Methods
        // RVA: 0x04D79B90  token: 0x60005AA
        protected System.Void .ctor() { }
        // RVA: 0x0B583A8C  token: 0x60005B1
        protected System.Void ApplyShadowZeroAlloc(System.Collections.Generic.List<UnityEngine.UIVertex> verts, UnityEngine.Color32 color, System.Int32 start, System.Int32 end, System.Single x, System.Single y) { }
        // RVA: 0x0B583D08  token: 0x60005B2
        protected System.Void ApplyShadow(System.Collections.Generic.List<UnityEngine.UIVertex> verts, UnityEngine.Color32 color, System.Int32 start, System.Int32 end, System.Single x, System.Single y) { }
        // RVA: 0x0B583D18  token: 0x60005B3
        public virtual System.Void ModifyMesh(UnityEngine.UI.VertexHelper vh) { }

    }

}

namespace UnityEngine.UI.Collections
{

    // TypeToken: 0x2000093
    public class IndexedSet`1 : System.Collections.Generic.IList`1, System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly System.Collections.Generic.List<T> m_List;  // 0x0
        private System.Collections.Generic.Dictionary<T,System.Int32> m_Dictionary;  // 0x0

        // Properties
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        System.Boolean IsReadOnly { get; /* RVA: -1  // not resolved */ }
        T Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60005B4
        public virtual System.Void Add(T item) { }
        // RVA: -1  // not resolved  token: 0x60005B5
        public System.Boolean AddUnique(T item) { }
        // RVA: -1  // not resolved  token: 0x60005B6
        public virtual System.Boolean Remove(T item) { }
        // RVA: -1  // not resolved  token: 0x60005B7
        public virtual System.Collections.Generic.IEnumerator<T> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x60005B8
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x60005B9
        public virtual System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x60005BA
        public virtual System.Boolean Contains(T item) { }
        // RVA: -1  // not resolved  token: 0x60005BB
        public virtual System.Void CopyTo(T[] array, System.Int32 arrayIndex) { }
        // RVA: -1  // not resolved  token: 0x60005BE
        public virtual System.Int32 IndexOf(T item) { }
        // RVA: -1  // not resolved  token: 0x60005BF
        public virtual System.Void Insert(System.Int32 index, T item) { }
        // RVA: -1  // not resolved  token: 0x60005C0
        public virtual System.Void RemoveAt(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x60005C3
        public System.Void RemoveAll(System.Predicate<T> match) { }
        // RVA: -1  // not resolved  token: 0x60005C4
        public System.Void Sort(System.Comparison<T> sortLayoutFunction) { }
        // RVA: -1  // not resolved  token: 0x60005C5
        public System.Void .ctor() { }

    }

}

namespace UnityEngine.UI.CoroutineTween
{

    // TypeToken: 0x2000094
    public interface ITweenValue
    {
        // Properties
        System.Boolean ignoreTimeScale { get; /* RVA: -1  // abstract */ }
        System.Single duration { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x60005C6
        public virtual System.Void TweenValue(System.Single floatPercentage) { }
        // RVA: -1  // abstract  token: 0x60005C9
        public virtual System.Boolean ValidTarget() { }

    }

    // TypeToken: 0x2000095  // size: 0x48
    public sealed struct ColorTween : UnityEngine.UI.CoroutineTween.ITweenValue
    {
        // Fields
        private UnityEngine.UI.CoroutineTween.ColorTween.ColorTweenCallback m_Target;  // 0x10
        private UnityEngine.Color m_StartColor;  // 0x18
        private UnityEngine.Color m_TargetColor;  // 0x28
        private UnityEngine.UI.CoroutineTween.ColorTween.ColorTweenMode m_TweenMode;  // 0x38
        private System.Single m_Duration;  // 0x3c
        private System.Boolean m_IgnoreTimeScale;  // 0x40

        // Properties
        UnityEngine.Color startColor { get; /* RVA: 0x015AD730 */ set; /* RVA: 0x04DA4E20 */ }
        UnityEngine.Color targetColor { get; /* RVA: 0x04D890C0 */ set; /* RVA: 0x04D890D0 */ }
        UnityEngine.UI.CoroutineTween.ColorTween.ColorTweenMode tweenMode { get; /* RVA: 0x011F2A90 */ set; /* RVA: 0x011F2AA0 */ }
        System.Single duration { get; /* RVA: 0x04D86AF0 */ set; /* RVA: 0x04D86B00 */ }
        System.Boolean ignoreTimeScale { get; /* RVA: 0x04D865F0 */ set; /* RVA: 0x04D86620 */ }

        // Methods
        // RVA: 0x0374AE50  token: 0x60005D4
        public virtual System.Void TweenValue(System.Single floatPercentage) { }
        // RVA: 0x04259CB0  token: 0x60005D5
        public System.Void AddOnChangedCallback(UnityEngine.Events.UnityAction<UnityEngine.Color> callback) { }
        // RVA: 0x04D865F0  token: 0x60005D6
        public System.Boolean GetIgnoreTimescale() { }
        // RVA: 0x04D86AF0  token: 0x60005D7
        public System.Single GetDuration() { }
        // RVA: 0x0374AFD0  token: 0x60005D8
        public virtual System.Boolean ValidTarget() { }

    }

    // TypeToken: 0x2000098  // size: 0x28
    public sealed struct FloatTween : UnityEngine.UI.CoroutineTween.ITweenValue
    {
        // Fields
        private UnityEngine.UI.CoroutineTween.FloatTween.FloatTweenCallback m_Target;  // 0x10
        private System.Single m_StartValue;  // 0x18
        private System.Single m_TargetValue;  // 0x1c
        private System.Single m_Duration;  // 0x20
        private System.Boolean m_IgnoreTimeScale;  // 0x24

        // Properties
        System.Single startValue { get; /* RVA: 0x04D9C280 */ set; /* RVA: 0x04DAC1D0 */ }
        System.Single targetValue { get; /* RVA: 0x04D9CC80 */ set; /* RVA: 0x04DACD70 */ }
        System.Single duration { get; /* RVA: 0x04D85F70 */ set; /* RVA: 0x04D88280 */ }
        System.Boolean ignoreTimeScale { get; /* RVA: 0x04D8EE50 */ set; /* RVA: 0x04D8EE70 */ }

        // Methods
        // RVA: 0x0B57D648  token: 0x60005E2
        public virtual System.Void TweenValue(System.Single floatPercentage) { }
        // RVA: 0x0B57D5B0  token: 0x60005E3
        public System.Void AddOnChangedCallback(UnityEngine.Events.UnityAction<System.Single> callback) { }
        // RVA: 0x04D8EE50  token: 0x60005E4
        public System.Boolean GetIgnoreTimescale() { }
        // RVA: 0x04D85F70  token: 0x60005E5
        public System.Single GetDuration() { }
        // RVA: 0x03A7A090  token: 0x60005E6
        public virtual System.Boolean ValidTarget() { }

    }

    // TypeToken: 0x200009A
    public class TweenRunner`1
    {
        // Fields
        protected UnityEngine.MonoBehaviour m_CoroutineContainer;  // 0x0
        protected System.Collections.IEnumerator m_Tween;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60005E8
        private static System.Collections.IEnumerator Start(T tweenInfo) { }
        // RVA: -1  // not resolved  token: 0x60005E9
        public System.Void Init(UnityEngine.MonoBehaviour coroutineContainer) { }
        // RVA: -1  // not resolved  token: 0x60005EA
        public System.Void StartTween(T info) { }
        // RVA: -1  // not resolved  token: 0x60005EB
        public System.Void StopTween() { }
        // RVA: -1  // not resolved  token: 0x60005EC
        public System.Void .ctor() { }

    }

}

namespace UnityEngine.UIElements
{

    // TypeToken: 0x200009C  // size: 0x38
    public class PanelEventHandler : UnityEngine.EventSystems.UIBehaviour, UnityEngine.EventSystems.IPointerMoveHandler, UnityEngine.EventSystems.IEventSystemHandler, UnityEngine.EventSystems.IPointerUpHandler, UnityEngine.EventSystems.IPointerDownHandler, UnityEngine.EventSystems.ISubmitHandler, UnityEngine.EventSystems.ICancelHandler, UnityEngine.EventSystems.IMoveHandler, UnityEngine.EventSystems.IScrollHandler, UnityEngine.EventSystems.ISelectHandler, UnityEngine.EventSystems.IDeselectHandler, UnityEngine.EventSystems.IPointerExitHandler, UnityEngine.EventSystems.IPointerEnterHandler, UnityEngine.UIElements.IRuntimePanelComponent
    {
        // Fields
        private UnityEngine.UIElements.BaseRuntimePanel m_Panel;  // 0x18
        private readonly UnityEngine.UIElements.PanelEventHandler.PointerEvent m_PointerEvent;  // 0x20
        private System.Boolean m_Selecting;  // 0x28
        private UnityEngine.Event m_Event;  // 0x30
        private static UnityEngine.EventModifiers s_Modifiers;  // static @ 0x0

        // Properties
        UnityEngine.UIElements.IPanel panel { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x0B57F5F0 */ }
        UnityEngine.GameObject selectableGameObject { get; /* RVA: 0x0B57F5DC */ }
        UnityEngine.EventSystems.EventSystem eventSystem { get; /* RVA: 0x0B57F58C */ }

        // Methods
        // RVA: 0x0B57DE14  token: 0x60005F7
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0B57DD78  token: 0x60005F8
        protected virtual System.Void OnDisable() { }
        // RVA: 0x0B57ECC8  token: 0x60005F9
        private System.Void RegisterCallbacks() { }
        // RVA: 0x0B57F28C  token: 0x60005FA
        private System.Void UnregisterCallbacks() { }
        // RVA: 0x0B57DEF8  token: 0x60005FB
        private System.Void OnPanelDestroyed() { }
        // RVA: 0x0B57DD80  token: 0x60005FC
        private System.Void OnElementFocus(UnityEngine.UIElements.FocusEvent e) { }
        // RVA: 0x041E1670  token: 0x60005FD
        private System.Void OnElementBlur(UnityEngine.UIElements.BlurEvent e) { }
        // RVA: 0x0B57E6F8  token: 0x60005FE
        public virtual System.Void OnSelect(UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0B57DD64  token: 0x60005FF
        public virtual System.Void OnDeselect(UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0B57E378  token: 0x6000600
        public virtual System.Void OnPointerMove(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B57E464  token: 0x6000601
        public virtual System.Void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B57DF04  token: 0x6000602
        public virtual System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B57E13C  token: 0x6000603
        public virtual System.Void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B57E0C8  token: 0x6000604
        public virtual System.Void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B57E790  token: 0x6000605
        public virtual System.Void OnSubmit(UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0B57DC98  token: 0x6000606
        public virtual System.Void OnCancel(UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0B57DE1C  token: 0x6000607
        public virtual System.Void OnMove(UnityEngine.EventSystems.AxisEventData eventData) { }
        // RVA: 0x0B57E5B8  token: 0x6000608
        public virtual System.Void OnScroll(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B57EE70  token: 0x6000609
        private System.Void SendEvent(UnityEngine.UIElements.EventBase e, UnityEngine.EventSystems.BaseEventData sourceEventData) { }
        // RVA: 0x0B57EEBC  token: 0x600060A
        private System.Void SendEvent(UnityEngine.UIElements.EventBase e, UnityEngine.Event sourceEvent) { }
        // RVA: 0x0B57F434  token: 0x600060B
        private System.Void Update() { }
        // RVA: 0x0B57DC8C  token: 0x600060C
        private System.Void LateUpdate() { }
        // RVA: 0x0B57E85C  token: 0x600060D
        private System.Void ProcessImguiEvents(System.Boolean isSelected) { }
        // RVA: 0x0B57E9A8  token: 0x600060E
        private System.Void ProcessKeyboardEvent(UnityEngine.Event e) { }
        // RVA: 0x0B57EAA4  token: 0x600060F
        private System.Void ProcessTabEvent(UnityEngine.Event e) { }
        // RVA: 0x0B57F0DC  token: 0x6000610
        private System.Void SendTabEvent(UnityEngine.Event e, System.Int32 direction) { }
        // RVA: 0x0B57EFF4  token: 0x6000611
        private System.Void SendKeyUpEvent(UnityEngine.Event e, UnityEngine.KeyCode keyCode, UnityEngine.EventModifiers modifiers) { }
        // RVA: 0x0B57EF0C  token: 0x6000612
        private System.Void SendKeyDownEvent(UnityEngine.Event e, UnityEngine.KeyCode keyCode, UnityEngine.EventModifiers modifiers) { }
        // RVA: 0x0B57F1A0  token: 0x6000613
        private System.Void SendTextEvent(UnityEngine.Event e, System.Char c, UnityEngine.EventModifiers modifiers) { }
        // RVA: 0x0B57EB18  token: 0x6000614
        private System.Boolean ReadPointerData(UnityEngine.UIElements.PanelEventHandler.PointerEvent pe, UnityEngine.EventSystems.PointerEventData eventData, UnityEngine.UIElements.PanelEventHandler.PointerEventType eventType) { }
        // RVA: 0x0B57F4F4  token: 0x6000615
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200009F  // size: 0x28
    public class PanelRaycaster : UnityEngine.EventSystems.BaseRaycaster, UnityEngine.UIElements.IRuntimePanelComponent
    {
        // Fields
        private UnityEngine.UIElements.BaseRuntimePanel m_Panel;  // 0x20

        // Properties
        UnityEngine.UIElements.IPanel panel { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x0B57FCB4 */ }
        UnityEngine.GameObject selectableGameObject { get; /* RVA: 0x0B57FC84 */ }
        System.Int32 sortOrderPriority { get; /* RVA: 0x0B57FC98 */ }
        System.Int32 renderOrderPriority { get; /* RVA: 0x0B57FC6C */ }
        UnityEngine.Camera eventCamera { get; /* RVA: 0x011EC580 */ }

        // Methods
        // RVA: 0x0B57FB44  token: 0x6000645
        private System.Void RegisterCallbacks() { }
        // RVA: 0x0B57FBD8  token: 0x6000646
        private System.Void UnregisterCallbacks() { }
        // RVA: 0x0B57F660  token: 0x6000647
        private System.Void OnPanelDestroyed() { }
        // RVA: 0x0B57F66C  token: 0x600064B
        public virtual System.Void Raycast(UnityEngine.EventSystems.PointerEventData eventData, System.Collections.Generic.List<UnityEngine.EventSystems.RaycastResult> resultAppendList) { }
        // RVA: 0x04D94B90  token: 0x600064D
        private static System.Int32 ConvertFloatBitsToInt(System.Single f) { }
        // RVA: 0x05393520  token: 0x600064E
        public System.Void .ctor() { }

    }

}

