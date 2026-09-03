// ========================================================
// Dumped by @desirepro
// Assembly: Unity.TextMeshPro.dll
// Classes:  186
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000010  // size: 0x28
    public sealed class <DelayedGraphicRebuild>d__18 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public TMPro.TextMeshProUGUI <>4__this;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60000EB
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60000EC
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0B30B618  token: 0x60000ED
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0B30B6C4  token: 0x60000EF
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000011  // size: 0x28
    public sealed class <DelayedMaterialRebuild>d__19 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public TMPro.TextMeshProUGUI <>4__this;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60000F1
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60000F2
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0B319CF0  token: 0x60000F3
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0B319DA4  token: 0x60000F5
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000023
    public sealed struct PoolOptions
    {
        // Fields
        public static readonly TMPro.TMP_ArrayObjectPool.PoolOptions<T> Default;  // static @ 0x0
        public readonly System.Boolean restrictArraySize;  // 0x0
        public readonly System.Collections.Generic.IReadOnlyList<System.Int32> allowedSizes;  // 0x0
        public readonly System.Int32 maxTotalCachedCount;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600012D
        public System.Void .ctor(System.Boolean restrictArraySize, System.Collections.Generic.IReadOnlyList<System.Int32> allowedSizes, System.Int32 maxTotalCachedCount) { }
        // RVA: -1  // not resolved  token: 0x600012E
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000025
    public sealed class <>c
    {
        // Fields
        public static readonly TMPro.TMP_ArrayPool.<>c<T> <>9;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000135
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x6000136
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000137
        private System.Void <.cctor>b__2_0(T[] arr) { }

    }

    // TypeToken: 0x2000033  // size: 0x14
    public sealed struct AnchorPositions
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.TMP_Compatibility.AnchorPositions TopLeft;  // const
        public static TMPro.TMP_Compatibility.AnchorPositions Top;  // const
        public static TMPro.TMP_Compatibility.AnchorPositions TopRight;  // const
        public static TMPro.TMP_Compatibility.AnchorPositions Left;  // const
        public static TMPro.TMP_Compatibility.AnchorPositions Center;  // const
        public static TMPro.TMP_Compatibility.AnchorPositions Right;  // const
        public static TMPro.TMP_Compatibility.AnchorPositions BottomLeft;  // const
        public static TMPro.TMP_Compatibility.AnchorPositions Bottom;  // const
        public static TMPro.TMP_Compatibility.AnchorPositions BottomRight;  // const
        public static TMPro.TMP_Compatibility.AnchorPositions BaseLine;  // const
        public static TMPro.TMP_Compatibility.AnchorPositions None;  // const

    }

    // TypeToken: 0x2000036  // size: 0x14
    public sealed struct ColorTweenMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.ColorTween.ColorTweenMode All;  // const
        public static TMPro.ColorTween.ColorTweenMode RGB;  // const
        public static TMPro.ColorTween.ColorTweenMode Alpha;  // const

    }

    // TypeToken: 0x2000037  // size: 0x30
    public class ColorTweenCallback : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x0B30B760  token: 0x600017C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000039  // size: 0x30
    public class FloatTweenCallback : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x0B30BC08  token: 0x600018A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003B
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
        // RVA: -1  // not resolved  token: 0x6000190
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x6000191
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000192
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x6000194
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200003D  // size: 0x48
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

    // TypeToken: 0x200003F  // size: 0x38
    public class DropdownItem : UnityEngine.MonoBehaviour, UnityEngine.EventSystems.IPointerEnterHandler, UnityEngine.EventSystems.IEventSystemHandler, UnityEngine.EventSystems.ICancelHandler
    {
        // Fields
        private TMPro.TMP_Text m_Text;  // 0x18
        private UnityEngine.UI.Image m_Image;  // 0x20
        private UnityEngine.RectTransform m_RectTransform;  // 0x28
        private UnityEngine.UI.Toggle m_Toggle;  // 0x30

        // Properties
        TMPro.TMP_Text text { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        UnityEngine.UI.Image image { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        UnityEngine.RectTransform rectTransform { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }
        UnityEngine.UI.Toggle toggle { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }

        // Methods
        // RVA: 0x0B30B9C0  token: 0x60001DF
        public virtual System.Void OnPointerEnter(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B30B948  token: 0x60001E0
        public virtual System.Void OnCancel(UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x05393520  token: 0x60001E1
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000040  // size: 0x20
    public class OptionData
    {
        // Fields
        private System.String m_Text;  // 0x10
        private UnityEngine.Sprite m_Image;  // 0x18

        // Properties
        System.String text { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        UnityEngine.Sprite image { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60001E6
        public System.Void .ctor() { }
        // RVA: 0x053908C0  token: 0x60001E7
        public System.Void .ctor(System.String text) { }
        // RVA: 0x05392C40  token: 0x60001E8
        public System.Void .ctor(UnityEngine.Sprite image) { }
        // RVA: 0x032AF420  token: 0x60001E9
        public System.Void .ctor(System.String text, UnityEngine.Sprite image) { }

    }

    // TypeToken: 0x2000041  // size: 0x18
    public class OptionDataList
    {
        // Fields
        private System.Collections.Generic.List<TMPro.TMP_Dropdown.OptionData> m_Options;  // 0x10

        // Properties
        System.Collections.Generic.List<TMPro.TMP_Dropdown.OptionData> options { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }

        // Methods
        // RVA: 0x0B30C96C  token: 0x60001EC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000042  // size: 0x30
    public class DropdownEvent : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x0B30B90C  token: 0x60001ED
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000043  // size: 0x20
    public sealed class <>c__DisplayClass69_0
    {
        // Fields
        public TMPro.TMP_Dropdown.DropdownItem item;  // 0x10
        public TMPro.TMP_Dropdown <>4__this;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60001EE
        public System.Void .ctor() { }
        // RVA: 0x0B319E80  token: 0x60001EF
        private System.Void <Show>b__0(System.Boolean x) { }

    }

    // TypeToken: 0x2000044  // size: 0x30
    public sealed class <DelayedDestroyDropdownList>d__81 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public System.Single delay;  // 0x20
        public TMPro.TMP_Dropdown <>4__this;  // 0x28

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60001F0
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60001F1
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0B319C08  token: 0x60001F2
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0B319CA4  token: 0x60001F4
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000047  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly TMPro.TMP_FontAsset.<>c <>9;  // static @ 0x0
        public static System.Func<TMPro.TMP_Character,System.UInt32> <>9__133_0;  // static @ 0x8
        public static System.Func<UnityEngine.TextCore.Glyph,System.UInt32> <>9__134_0;  // static @ 0x10

        // Methods
        // RVA: 0x0B319EB0  token: 0x600025D
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600025E
        public System.Void .ctor() { }
        // RVA: 0x0B319DF0  token: 0x600025F
        private System.UInt32 <SortCharacterTable>b__133_0(TMPro.TMP_Character c) { }
        // RVA: 0x0AA421AC  token: 0x6000260
        private System.UInt32 <SortGlyphTable>b__134_0(UnityEngine.TextCore.Glyph c) { }

    }

    // TypeToken: 0x2000050  // size: 0x18
    public sealed class <>c__DisplayClass3_0
    {
        // Fields
        public System.UInt32 first;  // 0x10
        public System.UInt32 second;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x600027B
        public System.Void .ctor() { }
        // RVA: 0x0B319E20  token: 0x600027C
        private System.Boolean <AddKerningPair>b__0(TMPro.KerningPair item) { }

    }

    // TypeToken: 0x2000051  // size: 0x18
    public sealed class <>c__DisplayClass4_0
    {
        // Fields
        public System.UInt32 first;  // 0x10
        public System.UInt32 second;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x600027D
        public System.Void .ctor() { }
        // RVA: 0x0B319E20  token: 0x600027E
        private System.Boolean <AddGlyphPairAdjustmentRecord>b__0(TMPro.KerningPair item) { }

    }

    // TypeToken: 0x2000052  // size: 0x18
    public sealed class <>c__DisplayClass5_0
    {
        // Fields
        public System.Int32 left;  // 0x10
        public System.Int32 right;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x600027F
        public System.Void .ctor() { }
        // RVA: 0x0B319E4C  token: 0x6000280
        private System.Boolean <RemoveKerningPair>b__0(TMPro.KerningPair item) { }

    }

    // TypeToken: 0x2000053  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly TMPro.KerningTable.<>c <>9;  // static @ 0x0
        public static System.Func<TMPro.KerningPair,System.UInt32> <>9__7_0;  // static @ 0x8
        public static System.Func<TMPro.KerningPair,System.UInt32> <>9__7_1;  // static @ 0x10

        // Methods
        // RVA: 0x0B319F14  token: 0x6000281
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000282
        public System.Void .ctor() { }
        // RVA: 0x0AA421AC  token: 0x6000283
        private System.UInt32 <SortKerningPairs>b__7_0(TMPro.KerningPair s) { }
        // RVA: 0x0B319E08  token: 0x6000284
        private System.UInt32 <SortKerningPairs>b__7_1(TMPro.KerningPair s) { }

    }

    // TypeToken: 0x200005C  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly TMPro.TMP_FontFeatureTable.<>c <>9;  // static @ 0x0
        public static System.Func<TMPro.TMP_GlyphPairAdjustmentRecord,System.UInt32> <>9__6_0;  // static @ 0x8
        public static System.Func<TMPro.TMP_GlyphPairAdjustmentRecord,System.UInt32> <>9__6_1;  // static @ 0x10

        // Methods
        // RVA: 0x0B321904  token: 0x60002B1
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60002B2
        public System.Void .ctor() { }
        // RVA: 0x0B3218D4  token: 0x60002B3
        private System.UInt32 <SortGlyphPairAdjustmentRecords>b__6_0(TMPro.TMP_GlyphPairAdjustmentRecord s) { }
        // RVA: 0x0B3218EC  token: 0x60002B4
        private System.UInt32 <SortGlyphPairAdjustmentRecords>b__6_1(TMPro.TMP_GlyphPairAdjustmentRecord s) { }

    }

    // TypeToken: 0x200005E  // size: 0x14
    public sealed struct ContentType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.TMP_InputField.ContentType Standard;  // const
        public static TMPro.TMP_InputField.ContentType Autocorrected;  // const
        public static TMPro.TMP_InputField.ContentType IntegerNumber;  // const
        public static TMPro.TMP_InputField.ContentType DecimalNumber;  // const
        public static TMPro.TMP_InputField.ContentType Alphanumeric;  // const
        public static TMPro.TMP_InputField.ContentType Name;  // const
        public static TMPro.TMP_InputField.ContentType EmailAddress;  // const
        public static TMPro.TMP_InputField.ContentType Password;  // const
        public static TMPro.TMP_InputField.ContentType Pin;  // const
        public static TMPro.TMP_InputField.ContentType Custom;  // const

    }

    // TypeToken: 0x200005F  // size: 0x14
    public sealed struct InputType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.TMP_InputField.InputType Standard;  // const
        public static TMPro.TMP_InputField.InputType AutoCorrect;  // const
        public static TMPro.TMP_InputField.InputType Password;  // const

    }

    // TypeToken: 0x2000060  // size: 0x14
    public sealed struct CharacterValidation
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.TMP_InputField.CharacterValidation None;  // const
        public static TMPro.TMP_InputField.CharacterValidation Digit;  // const
        public static TMPro.TMP_InputField.CharacterValidation Integer;  // const
        public static TMPro.TMP_InputField.CharacterValidation Decimal;  // const
        public static TMPro.TMP_InputField.CharacterValidation Alphanumeric;  // const
        public static TMPro.TMP_InputField.CharacterValidation Name;  // const
        public static TMPro.TMP_InputField.CharacterValidation Regex;  // const
        public static TMPro.TMP_InputField.CharacterValidation EmailAddress;  // const
        public static TMPro.TMP_InputField.CharacterValidation CustomValidator;  // const

    }

    // TypeToken: 0x2000061  // size: 0x14
    public sealed struct LineType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.TMP_InputField.LineType SingleLine;  // const
        public static TMPro.TMP_InputField.LineType MultiLineSubmit;  // const
        public static TMPro.TMP_InputField.LineType MultiLineNewline;  // const

    }

    // TypeToken: 0x2000062  // size: 0x80
    public sealed class OnValidateInput : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0B31A110  token: 0x600039E
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x014F3120  token: 0x600039F
        public virtual System.Char Invoke(System.String text, System.Int32 charIndex, System.Char addedChar) { }
        // RVA: 0x0B31A074  token: 0x60003A0
        public virtual System.IAsyncResult BeginInvoke(System.String text, System.Int32 charIndex, System.Char addedChar, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x09612FF8  token: 0x60003A1
        public virtual System.Char EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x2000063  // size: 0x30
    public class SubmitEvent : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x0405B7E0  token: 0x60003A2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000064  // size: 0x30
    public class OnChangeEvent : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x0405B3D0  token: 0x60003A3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000065  // size: 0x30
    public class SelectionEvent : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x0405B820  token: 0x60003A4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000066  // size: 0x30
    public class TextSelectionEvent : UnityEngine.Events.UnityEvent`3
    {
        // Methods
        // RVA: 0x0405B860  token: 0x60003A5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000067  // size: 0x30
    public class TouchScreenKeyboardEvent : UnityEngine.Events.UnityEvent`1
    {
        // Methods
        // RVA: 0x0405B900  token: 0x60003A6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000068  // size: 0x14
    public sealed struct EditState
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.TMP_InputField.EditState Continue;  // const
        public static TMPro.TMP_InputField.EditState Finish;  // const

    }

    // TypeToken: 0x2000069  // size: 0x28
    public sealed class <CaretBlink>d__291 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public TMPro.TMP_InputField <>4__this;  // 0x20

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60003A7
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60003A8
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x03797D20  token: 0x60003A9
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0B3215E0  token: 0x60003AB
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200006A  // size: 0x30
    public sealed class <MouseDragOutsideRect>d__309 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public TMPro.TMP_InputField <>4__this;  // 0x20
        public UnityEngine.EventSystems.PointerEventData eventData;  // 0x28

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x60003AD
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60003AE
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0B32162C  token: 0x60003AF
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0B321888  token: 0x60003B1
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200006F
    public sealed class <>c
    {
        // Fields
        public static readonly TMPro.TMP_ListPool.<>c<T> <>9;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60003BC
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x60003BD
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60003BE
        private System.Void <.cctor>b__3_0(System.Collections.Generic.List<T> l) { }

    }

    // TypeToken: 0x2000071  // size: 0x38
    public class FallbackMaterial
    {
        // Fields
        public System.Int64 fallbackID;  // 0x10
        public UnityEngine.Material sourceMaterial;  // 0x18
        private System.Int32 sourceMaterialCRC;  // 0x20
        public UnityEngine.Material fallbackMaterial;  // 0x28
        public System.Int32 count;  // 0x30

        // Methods
        // RVA: 0x041E1670  token: 0x60003D5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000072  // size: 0x28
    public class MaskingMaterial
    {
        // Fields
        public UnityEngine.Material baseMaterial;  // 0x10
        public UnityEngine.Material stencilMaterial;  // 0x18
        public System.Int32 count;  // 0x20
        public System.Int32 stencilID;  // 0x24

        // Methods
        // RVA: 0x041E1670  token: 0x60003D6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000073  // size: 0x18
    public sealed class <>c__DisplayClass13_0
    {
        // Fields
        public UnityEngine.Material stencilMaterial;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60003D7
        public System.Void .ctor() { }
        // RVA: 0x0B339194  token: 0x60003D8
        private System.Boolean <GetBaseMaterial>b__0(TMPro.TMP_MaterialManager.MaskingMaterial item) { }

    }

    // TypeToken: 0x2000074  // size: 0x18
    public sealed class <>c__DisplayClass15_0
    {
        // Fields
        public UnityEngine.Material stencilMaterial;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60003D9
        public System.Void .ctor() { }
        // RVA: 0x0B3391F8  token: 0x60003DA
        private System.Boolean <AddMaskingMaterial>b__0(TMPro.TMP_MaterialManager.MaskingMaterial item) { }

    }

    // TypeToken: 0x2000075  // size: 0x18
    public sealed class <>c__DisplayClass16_0
    {
        // Fields
        public UnityEngine.Material stencilMaterial;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60003DB
        public System.Void .ctor() { }
        // RVA: 0x0B33925C  token: 0x60003DC
        private System.Boolean <RemoveStencilMaterial>b__0(TMPro.TMP_MaterialManager.MaskingMaterial item) { }

    }

    // TypeToken: 0x2000076  // size: 0x18
    public sealed class <>c__DisplayClass17_0
    {
        // Fields
        public UnityEngine.Material baseMaterial;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60003DD
        public System.Void .ctor() { }
        // RVA: 0x0B3392C0  token: 0x60003DE
        private System.Boolean <ReleaseBaseMaterial>b__0(TMPro.TMP_MaterialManager.MaskingMaterial item) { }

    }

    // TypeToken: 0x2000082  // size: 0x20
    public class LineBreakingTable
    {
        // Fields
        public System.Collections.Generic.Dictionary<System.Int32,System.Char> leadingCharacters;  // 0x10
        public System.Collections.Generic.Dictionary<System.Int32,System.Char> followingCharacters;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000431
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000086  // size: 0x228
    public sealed class <DoSpriteAnimationInternal>d__7 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Object <>2__current;  // 0x18
        public TMPro.TMP_SpriteAnimator <>4__this;  // 0x20
        public System.Int32 start;  // 0x28
        public System.Int32 end;  // 0x2c
        public TMPro.TMP_SpriteAsset spriteAsset;  // 0x30
        public System.Int32 currentCharacter;  // 0x38
        public System.Int32 framerate;  // 0x3c
        private System.Int32 <currentFrame>5__2;  // 0x40
        private TMPro.TMP_CharacterInfo <charInfo>5__3;  // 0x48
        private System.Int32 <materialIndex>5__4;  // 0x1c0
        private System.Int32 <vertexIndex>5__5;  // 0x1c4
        private TMPro.TMP_MeshInfo <meshInfo>5__6;  // 0x1c8
        private System.Single <baseSpriteScale>5__7;  // 0x218
        private System.Single <elapsedTime>5__8;  // 0x21c
        private System.Single <targetTime>5__9;  // 0x220

        // Properties
        System.Object System.Collections.Generic.IEnumerator<System.Object>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000443
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000444
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0B33883C  token: 0x6000445
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0B339148  token: 0x6000447
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000088  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly TMPro.TMP_SpriteAsset.<>c <>9;  // static @ 0x0
        public static System.Func<TMPro.TMP_SpriteGlyph,System.UInt32> <>9__40_0;  // static @ 0x8
        public static System.Func<TMPro.TMP_SpriteCharacter,System.UInt32> <>9__41_0;  // static @ 0x10

        // Methods
        // RVA: 0x0B339324  token: 0x6000464
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000465
        public System.Void .ctor() { }
        // RVA: 0x0AA421AC  token: 0x6000466
        private System.UInt32 <SortGlyphTable>b__40_0(TMPro.TMP_SpriteGlyph item) { }
        // RVA: 0x0B319DF0  token: 0x6000467
        private System.UInt32 <SortCharacterTable>b__41_0(TMPro.TMP_SpriteCharacter c) { }

    }

    // TypeToken: 0x200009E  // size: 0x18
    public sealed struct CharacterSubstitution
    {
        // Fields
        public System.Int32 index;  // 0x10
        public System.UInt32 unicode;  // 0x14

        // Methods
        // RVA: 0x04D85C40  token: 0x6000623
        public System.Void .ctor(System.Int32 index, System.UInt32 unicode) { }

    }

    // TypeToken: 0x200009F  // size: 0x14
    public sealed struct TextInputSources
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.TMP_Text.TextInputSources TextInputBox;  // const
        public static TMPro.TMP_Text.TextInputSources SetText;  // const
        public static TMPro.TMP_Text.TextInputSources SetTextArray;  // const
        public static TMPro.TMP_Text.TextInputSources TextString;  // const

    }

    // TypeToken: 0x20000A0  // size: 0x1C
    public sealed struct UnicodeChar
    {
        // Fields
        public System.Int32 unicode;  // 0x10
        public System.Int32 stringIndex;  // 0x14
        public System.Int32 length;  // 0x18

    }

    // TypeToken: 0x20000A1  // size: 0x30
    public sealed struct SpecialCharacter
    {
        // Fields
        public TMPro.TMP_Character character;  // 0x10
        public TMPro.TMP_FontAsset fontAsset;  // 0x18
        public UnityEngine.Material material;  // 0x20
        public System.Int32 materialIndex;  // 0x28

        // Methods
        // RVA: 0x0333AD10  token: 0x6000624
        public System.Void .ctor(TMPro.TMP_Character character, System.Int32 materialIndex) { }

    }

    // TypeToken: 0x20000A2  // size: 0x20
    public sealed struct TextBackingContainer : System.IDisposable
    {
        // Fields
        private System.UInt32[] m_Array;  // 0x10
        private System.Int32 m_Count;  // 0x18

        // Properties
        System.Int32 Capacity { get; /* RVA: 0x0539210C */ }
        System.Int32 Count { get; /* RVA: 0x04D88B10 */ set; /* RVA: 0x04D88B40 */ }
        System.UInt32 Item { get; /* RVA: 0x0333E610 */ set; /* RVA: 0x0333BC20 */ }

        // Methods
        // RVA: 0x03518980  token: 0x600062A
        public System.Void .ctor(System.Int32 size) { }
        // RVA: 0x03339F40  token: 0x600062B
        public System.Void Resize(System.Int32 size) { }
        // RVA: 0x035194D0  token: 0x600062C
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x20000A3  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly TMPro.TMP_Text.<>c <>9;  // static @ 0x0
        public static System.Action<TMPro.TMP_TextInfo> <>9__689_0;  // static @ 0x8

        // Methods
        // RVA: 0x04D49A50  token: 0x600062D
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600062E
        public System.Void .ctor() { }
        // RVA: 0x041E1670  token: 0x600062F
        private System.Void <.ctor>b__689_0(TMPro.TMP_TextInfo <p0>) { }

    }

    // TypeToken: 0x20000AF  // size: 0x28
    public sealed struct LineSegment
    {
        // Fields
        public UnityEngine.Vector3 Point1;  // 0x10
        public UnityEngine.Vector3 Point2;  // 0x1c

        // Methods
        // RVA: 0x04D871E0  token: 0x600069C
        public System.Void .ctor(UnityEngine.Vector3 p1, UnityEngine.Vector3 p2) { }

    }

    // TypeToken: 0x20000B4  // size: 0x20
    public sealed struct SpriteFrame
    {
        // Fields
        public System.Single x;  // 0x10
        public System.Single y;  // 0x14
        public System.Single w;  // 0x18
        public System.Single h;  // 0x1c

        // Methods
        // RVA: 0x0B339388  token: 0x60006BB
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x20000B5  // size: 0x18
    public sealed struct SpriteSize
    {
        // Fields
        public System.Single w;  // 0x10
        public System.Single h;  // 0x14

        // Methods
        // RVA: 0x0B3394F8  token: 0x60006BC
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x20000B6  // size: 0x50
    public sealed struct Frame
    {
        // Fields
        public System.String filename;  // 0x10
        public TMPro.SpriteAssetUtilities.TexturePacker_JsonArray.SpriteFrame frame;  // 0x18
        public System.Boolean rotated;  // 0x28
        public System.Boolean trimmed;  // 0x29
        public TMPro.SpriteAssetUtilities.TexturePacker_JsonArray.SpriteFrame spriteSourceSize;  // 0x2c
        public TMPro.SpriteAssetUtilities.TexturePacker_JsonArray.SpriteSize sourceSize;  // 0x3c
        public UnityEngine.Vector2 pivot;  // 0x44

    }

    // TypeToken: 0x20000B7  // size: 0x48
    public sealed struct Meta
    {
        // Fields
        public System.String app;  // 0x10
        public System.String version;  // 0x18
        public System.String image;  // 0x20
        public System.String format;  // 0x28
        public TMPro.SpriteAssetUtilities.TexturePacker_JsonArray.SpriteSize size;  // 0x30
        public System.Single scale;  // 0x38
        public System.String smartupdate;  // 0x40

    }

    // TypeToken: 0x20000B8  // size: 0x50
    public class SpriteDataObject
    {
        // Fields
        public System.Collections.Generic.List<TMPro.SpriteAssetUtilities.TexturePacker_JsonArray.Frame> frames;  // 0x10
        public TMPro.SpriteAssetUtilities.TexturePacker_JsonArray.Meta meta;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60006BD
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000BA  // size: 0x1C
    public sealed struct __StaticArrayInitTypeSize=12
    {
    }

    // TypeToken: 0x20000B9  // size: 0x10
    public sealed class <PrivateImplementationDetails>
    {
        // Fields
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=12 1C3635C112D556F4C11A4FE6BDE6ED3F126C4B2B546811BDB64DE7BDED3A05CB;  // static @ 0x0

    }

namespace TMPro
{

    // TypeToken: 0x2000002  // size: 0x20
    public class FastAction
    {
        // Fields
        private System.Collections.Generic.LinkedList<System.Action> delegates;  // 0x10
        private System.Collections.Generic.Dictionary<System.Action,System.Collections.Generic.LinkedListNode<System.Action>> lookup;  // 0x18

        // Methods
        // RVA: 0x0B3008BC  token: 0x6000001
        public System.Void Add(System.Action rhs) { }
        // RVA: 0x0B3009F0  token: 0x6000002
        public System.Void Remove(System.Action rhs) { }
        // RVA: 0x0B300970  token: 0x6000003
        public System.Void Call() { }
        // RVA: 0x0478EC20  token: 0x6000004
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000003
    public class FastAction`1
    {
        // Fields
        private System.Collections.Generic.LinkedList<System.Action<A>> delegates;  // 0x0
        private System.Collections.Generic.Dictionary<System.Action<A>,System.Collections.Generic.LinkedListNode<System.Action<A>>> lookup;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000005
        public System.Void Add(System.Action<A> rhs) { }
        // RVA: -1  // not resolved  token: 0x6000006
        public System.Void Remove(System.Action<A> rhs) { }
        // RVA: -1  // not resolved  token: 0x6000007
        public System.Void Call(A a) { }
        // RVA: -1  // not resolved  token: 0x6000008
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000004
    public class FastAction`2
    {
        // Fields
        private System.Collections.Generic.LinkedList<System.Action<A,B>> delegates;  // 0x0
        private System.Collections.Generic.Dictionary<System.Action<A,B>,System.Collections.Generic.LinkedListNode<System.Action<A,B>>> lookup;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000009
        public System.Void Add(System.Action<A,B> rhs) { }
        // RVA: -1  // not resolved  token: 0x600000A
        public System.Void Remove(System.Action<A,B> rhs) { }
        // RVA: -1  // not resolved  token: 0x600000B
        public System.Void Call(A a, B b) { }
        // RVA: -1  // not resolved  token: 0x600000C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000005
    public class FastAction`3
    {
        // Fields
        private System.Collections.Generic.LinkedList<System.Action<A,B,C>> delegates;  // 0x0
        private System.Collections.Generic.Dictionary<System.Action<A,B,C>,System.Collections.Generic.LinkedListNode<System.Action<A,B,C>>> lookup;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600000D
        public System.Void Add(System.Action<A,B,C> rhs) { }
        // RVA: -1  // not resolved  token: 0x600000E
        public System.Void Remove(System.Action<A,B,C> rhs) { }
        // RVA: -1  // not resolved  token: 0x600000F
        public System.Void Call(A a, B b, C c) { }
        // RVA: -1  // not resolved  token: 0x6000010
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000006
    public interface IFontAssetLoader
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000011
        public virtual System.Boolean TryGetFontAsset(TMPro.TMP_Text refText, System.UInt32 unicode, TMPro.TMP_FontAsset& fontAsset) { }
        // RVA: -1  // abstract  token: 0x6000012
        public virtual System.Void TryRemoveRefText(TMPro.TMP_Text refText) { }

    }

    // TypeToken: 0x2000007
    public interface ITextPreprocessor
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000013
        public virtual System.String PreprocessText(System.String text) { }

    }

    // TypeToken: 0x2000008  // size: 0x10
    public class LocalizedTextPreprocessor : TMPro.ITextPreprocessor
    {
        // Fields
        private static TMPro.LocalizedTextPreprocessor _instance;  // static @ 0x0

        // Properties
        TMPro.LocalizedTextPreprocessor Instance { get; /* RVA: 0x0B300AA0 */ }

        // Methods
        // RVA: 0x0333C4E0  token: 0x6000014
        public virtual System.String PreprocessText(System.String text) { }
        // RVA: 0x041E1670  token: 0x6000016
        public System.Void .ctor() { }
        // RVA: 0x04D4E010  token: 0x6000017
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000009  // size: 0x30
    public class MaterialReferenceManager
    {
        // Fields
        private static TMPro.MaterialReferenceManager s_Instance;  // static @ 0x0
        private System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Material> m_FontMaterialReferenceLookup;  // 0x10
        private System.Collections.Generic.Dictionary<System.Int32,TMPro.TMP_FontAsset> m_FontAssetReferenceLookup;  // 0x18
        private System.Collections.Generic.Dictionary<System.Int32,TMPro.TMP_SpriteAsset> m_SpriteAssetReferenceLookup;  // 0x20
        private System.Collections.Generic.Dictionary<System.Int32,TMPro.TMP_ColorGradient> m_ColorGradientReferenceLookup;  // 0x28

        // Properties
        TMPro.MaterialReferenceManager instance { get; /* RVA: 0x0B3013D4 */ }

        // Methods
        // RVA: 0x0B300C68  token: 0x6000019
        public static System.Void AddFontAsset(TMPro.TMP_FontAsset fontAsset) { }
        // RVA: 0x0B300BB8  token: 0x600001A
        private System.Void AddFontAssetInternal(TMPro.TMP_FontAsset fontAsset) { }
        // RVA: 0x0B300EA8  token: 0x600001B
        public static System.Void AddSpriteAsset(TMPro.TMP_SpriteAsset spriteAsset) { }
        // RVA: 0x0B300D38  token: 0x600001C
        private System.Void AddSpriteAssetInternal(TMPro.TMP_SpriteAsset spriteAsset) { }
        // RVA: 0x0B300ED8  token: 0x600001D
        public static System.Void AddSpriteAsset(System.Int32 hashCode, TMPro.TMP_SpriteAsset spriteAsset) { }
        // RVA: 0x0B300DE8  token: 0x600001E
        private System.Void AddSpriteAssetInternal(System.Int32 hashCode, TMPro.TMP_SpriteAsset spriteAsset) { }
        // RVA: 0x0B300CFC  token: 0x600001F
        public static System.Void AddFontMaterial(System.Int32 hashCode, UnityEngine.Material material) { }
        // RVA: 0x0B300C98  token: 0x6000020
        private System.Void AddFontMaterialInternal(System.Int32 hashCode, UnityEngine.Material material) { }
        // RVA: 0x0B300B7C  token: 0x6000021
        public static System.Void AddColorGradientPreset(System.Int32 hashCode, TMPro.TMP_ColorGradient spriteAsset) { }
        // RVA: 0x0B300AF0  token: 0x6000022
        private System.Void AddColorGradientPreset_Internal(System.Int32 hashCode, TMPro.TMP_ColorGradient spriteAsset) { }
        // RVA: 0x0B300F14  token: 0x6000023
        public System.Boolean Contains(TMPro.TMP_FontAsset font) { }
        // RVA: 0x0B300F70  token: 0x6000024
        public System.Boolean Contains(TMPro.TMP_SpriteAsset sprite) { }
        // RVA: 0x0B3010E8  token: 0x6000025
        public static System.Boolean TryGetFontAsset(System.Int32 hashCode, TMPro.TMP_FontAsset& fontAsset) { }
        // RVA: 0x0B301078  token: 0x6000026
        private System.Boolean TryGetFontAssetInternal(System.Int32 hashCode, TMPro.TMP_FontAsset& fontAsset) { }
        // RVA: 0x0B301240  token: 0x6000027
        public static System.Boolean TryGetSpriteAsset(System.Int32 hashCode, TMPro.TMP_SpriteAsset& spriteAsset) { }
        // RVA: 0x0B3011D0  token: 0x6000028
        private System.Boolean TryGetSpriteAssetInternal(System.Int32 hashCode, TMPro.TMP_SpriteAsset& spriteAsset) { }
        // RVA: 0x0B30103C  token: 0x6000029
        public static System.Boolean TryGetColorGradientPreset(System.Int32 hashCode, TMPro.TMP_ColorGradient& gradientPreset) { }
        // RVA: 0x0B300FCC  token: 0x600002A
        private System.Boolean TryGetColorGradientPresetInternal(System.Int32 hashCode, TMPro.TMP_ColorGradient& gradientPreset) { }
        // RVA: 0x0B301194  token: 0x600002B
        public static System.Boolean TryGetMaterial(System.Int32 hashCode, UnityEngine.Material& material) { }
        // RVA: 0x0B301124  token: 0x600002C
        private System.Boolean TryGetMaterialInternal(System.Int32 hashCode, UnityEngine.Material& material) { }
        // RVA: 0x0B30127C  token: 0x600002D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000A  // size: 0x20
    public sealed struct TMP_MaterialReference
    {
        // Fields
        public UnityEngine.Material material;  // 0x10
        public System.Int32 referenceCount;  // 0x18

    }

    // TypeToken: 0x200000B  // size: 0x48
    public sealed struct MaterialReference
    {
        // Fields
        public System.Int32 index;  // 0x10
        public TMPro.TMP_FontAsset fontAsset;  // 0x18
        public TMPro.TMP_SpriteAsset spriteAsset;  // 0x20
        public UnityEngine.Material material;  // 0x28
        public System.Boolean isDefaultMaterial;  // 0x30
        public System.Boolean isFallbackMaterial;  // 0x31
        public UnityEngine.Material fallbackMaterial;  // 0x38
        public System.Single padding;  // 0x40
        public System.Int32 referenceCount;  // 0x44

        // Methods
        // RVA: 0x0333BC80  token: 0x600002E
        public System.Void .ctor(System.Int32 index, TMPro.TMP_FontAsset fontAsset, TMPro.TMP_SpriteAsset spriteAsset, UnityEngine.Material material, System.Single padding) { }
        // RVA: 0x0B301650  token: 0x600002F
        public static System.Boolean Contains(TMPro.MaterialReference[] materialReferences, TMPro.TMP_FontAsset fontAsset) { }
        // RVA: 0x0333CE80  token: 0x6000030
        public static System.Int32 AddMaterialReference(UnityEngine.Material material, TMPro.TMP_FontAsset fontAsset, TMPro.MaterialReference[]& materialReferences, System.Collections.Generic.Dictionary<System.Int32,System.Int32> materialReferenceIndexLookup) { }
        // RVA: 0x0B301464  token: 0x6000031
        public static System.Int32 AddMaterialReference(UnityEngine.Material material, TMPro.TMP_SpriteAsset spriteAsset, TMPro.MaterialReference[]& materialReferences, System.Collections.Generic.Dictionary<System.Int32,System.Int32> materialReferenceIndexLookup) { }

    }

    // TypeToken: 0x200000C  // size: 0x14
    public sealed struct TextContainerAnchors
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.TextContainerAnchors TopLeft;  // const
        public static TMPro.TextContainerAnchors Top;  // const
        public static TMPro.TextContainerAnchors TopRight;  // const
        public static TMPro.TextContainerAnchors Left;  // const
        public static TMPro.TextContainerAnchors Middle;  // const
        public static TMPro.TextContainerAnchors Right;  // const
        public static TMPro.TextContainerAnchors BottomLeft;  // const
        public static TMPro.TextContainerAnchors Bottom;  // const
        public static TMPro.TextContainerAnchors BottomRight;  // const
        public static TMPro.TextContainerAnchors Custom;  // const

    }

    // TypeToken: 0x200000D  // size: 0x70
    public class TextContainer : UnityEngine.EventSystems.UIBehaviour
    {
        // Fields
        private System.Boolean m_hasChanged;  // 0x18
        private UnityEngine.Vector2 m_pivot;  // 0x1c
        private TMPro.TextContainerAnchors m_anchorPosition;  // 0x24
        private UnityEngine.Rect m_rect;  // 0x28
        private System.Boolean m_isDefaultWidth;  // 0x38
        private System.Boolean m_isDefaultHeight;  // 0x39
        private System.Boolean m_isAutoFitting;  // 0x3a
        private UnityEngine.Vector3[] m_corners;  // 0x40
        private UnityEngine.Vector3[] m_worldCorners;  // 0x48
        private UnityEngine.Vector4 m_margins;  // 0x50
        private UnityEngine.RectTransform m_rectTransform;  // 0x60
        private static UnityEngine.Vector2 k_defaultSize;  // static @ 0x0
        private TMPro.TextMeshPro m_textMeshPro;  // 0x68

        // Properties
        System.Boolean hasChanged { get; /* RVA: 0x04D86340 */ set; /* RVA: 0x04D86350 */ }
        UnityEngine.Vector2 pivot { get; /* RVA: 0x04DB9A00 */ set; /* RVA: 0x0B3020A8 */ }
        TMPro.TextContainerAnchors anchorPosition { get; /* RVA: 0x011F0020 */ set; /* RVA: 0x0B301FC8 */ }
        UnityEngine.Rect rect { get; /* RVA: 0x04D8FAD0 */ set; /* RVA: 0x0B302118 */ }
        UnityEngine.Vector2 size { get; /* RVA: 0x04DB9A20 */ set; /* RVA: 0x0B302170 */ }
        System.Single width { get; /* RVA: 0x04DB9A40 */ set; /* RVA: 0x0B3021D0 */ }
        System.Single height { get; /* RVA: 0x053957B0 */ set; /* RVA: 0x0B302010 */ }
        System.Boolean isDefaultWidth { get; /* RVA: 0x011F2A70 */ }
        System.Boolean isDefaultHeight { get; /* RVA: 0x04D86650 */ }
        System.Boolean isAutoFitting { get; /* RVA: 0x04D86660 */ set; /* RVA: 0x04D86690 */ }
        UnityEngine.Vector3[] corners { get; /* RVA: 0x04D85A60 */ }
        UnityEngine.Vector3[] worldCorners { get; /* RVA: 0x04D86200 */ }
        UnityEngine.Vector4 margins { get; /* RVA: 0x04D8CE10 */ set; /* RVA: 0x0B302058 */ }
        UnityEngine.RectTransform rectTransform { get; /* RVA: 0x0B301EC0 */ }
        TMPro.TextMeshPro textMeshPro { get; /* RVA: 0x0B301F44 */ }

        // Methods
        // RVA: 0x0B301718  token: 0x600004A
        protected virtual System.Void Awake() { }
        // RVA: 0x0B301AE8  token: 0x600004B
        protected virtual System.Void OnEnable() { }
        // RVA: 0x041E1670  token: 0x600004C
        protected virtual System.Void OnDisable() { }
        // RVA: 0x0B3019EC  token: 0x600004D
        private System.Void OnContainerChanged() { }
        // RVA: 0x0B301AF0  token: 0x600004E
        protected virtual System.Void OnRectTransformDimensionsChange() { }
        // RVA: 0x04DB99B0  token: 0x600004F
        private System.Void SetRect(UnityEngine.Vector2 size) { }
        // RVA: 0x0B301C48  token: 0x6000050
        private System.Void UpdateCorners() { }
        // RVA: 0x0B301960  token: 0x6000051
        private UnityEngine.Vector2 GetPivot(TMPro.TextContainerAnchors anchor) { }
        // RVA: 0x0B3017B8  token: 0x6000052
        private TMPro.TextContainerAnchors GetAnchorPosition(UnityEngine.Vector2 pivot) { }
        // RVA: 0x0B301E48  token: 0x6000053
        public System.Void .ctor() { }
        // RVA: 0x0B301E08  token: 0x6000054
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000E  // size: 0xB98
    public class TextMeshPro : TMPro.TMP_Text, UnityEngine.UI.ILayoutElement
    {
        // Fields
        private System.Int32 _SortingLayer;  // 0xaf8
        private System.Int32 _SortingLayerID;  // 0xafc
        private System.Int32 _SortingOrder;  // 0xb00
        private System.Action<TMPro.TMP_TextInfo> OnPreRenderText;  // 0xb08
        private System.Boolean m_currentAutoSizeMode;  // 0xb10
        private System.Boolean m_hasFontAssetChanged;  // 0xb11
        private System.Single m_previousLossyScaleY;  // 0xb14
        private UnityEngine.Renderer m_renderer;  // 0xb18
        private UnityEngine.MeshFilter m_meshFilter;  // 0xb20
        private System.Boolean m_isFirstAllocation;  // 0xb28
        private System.Int32 m_max_characters;  // 0xb2c
        private System.Int32 m_max_numberOfLines;  // 0xb30
        private TMPro.TMP_SubMesh[] m_subTextObjects;  // 0xb38
        private TMPro.MaskingTypes m_maskType;  // 0xb40
        private UnityEngine.Matrix4x4 m_EnvMapMatrix;  // 0xb44
        private UnityEngine.Vector3[] m_RectTransformCorners;  // 0xb88
        private System.Boolean m_isRegisteredForEvents;  // 0xb90
        private static Unity.Profiling.ProfilerMarker k_GenerateTextMarker;  // static @ 0x0
        private static Unity.Profiling.ProfilerMarker k_SetArraySizesMarker;  // static @ 0x8
        private static Unity.Profiling.ProfilerMarker k_GenerateTextPhaseIMarker;  // static @ 0x10
        private static Unity.Profiling.ProfilerMarker k_ParseMarkupTextMarker;  // static @ 0x18
        private static Unity.Profiling.ProfilerMarker k_CharacterLookupMarker;  // static @ 0x20
        private static Unity.Profiling.ProfilerMarker k_HandleGPOSFeaturesMarker;  // static @ 0x28
        private static Unity.Profiling.ProfilerMarker k_CalculateVerticesPositionMarker;  // static @ 0x30
        private static Unity.Profiling.ProfilerMarker k_ComputeTextMetricsMarker;  // static @ 0x38
        private static Unity.Profiling.ProfilerMarker k_HandleVisibleCharacterMarker;  // static @ 0x40
        private static Unity.Profiling.ProfilerMarker k_HandleWhiteSpacesMarker;  // static @ 0x48
        private static Unity.Profiling.ProfilerMarker k_HandleHorizontalLineBreakingMarker;  // static @ 0x50
        private static Unity.Profiling.ProfilerMarker k_HandleVerticalLineBreakingMarker;  // static @ 0x58
        private static Unity.Profiling.ProfilerMarker k_SaveGlyphVertexDataMarker;  // static @ 0x60
        private static Unity.Profiling.ProfilerMarker k_ComputeCharacterAdvanceMarker;  // static @ 0x68
        private static Unity.Profiling.ProfilerMarker k_HandleCarriageReturnMarker;  // static @ 0x70
        private static Unity.Profiling.ProfilerMarker k_HandleLineTerminationMarker;  // static @ 0x78
        private static Unity.Profiling.ProfilerMarker k_SavePageInfoMarker;  // static @ 0x80
        private static Unity.Profiling.ProfilerMarker k_SaveProcessingStatesMarker;  // static @ 0x88
        private static Unity.Profiling.ProfilerMarker k_GenerateTextPhaseIIMarker;  // static @ 0x90
        private static Unity.Profiling.ProfilerMarker k_GenerateTextPhaseIIIMarker;  // static @ 0x98

        // Properties
        System.Int32 sortingLayerID { get; /* RVA: 0x0B30B248 */ set; /* RVA: 0x0B30B4E0 */ }
        System.Int32 sortingOrder { get; /* RVA: 0x0B30B2C4 */ set; /* RVA: 0x0B30B57C */ }
        System.Boolean autoSizeTextContainer { get; /* RVA: 0x04DB9A50 */ set; /* RVA: 0x0B30B460 */ }
        TMPro.TextContainer textContainer { get; /* RVA: 0x011EC580 */ }
        UnityEngine.Transform transform { get; /* RVA: 0x0B30B340 */ }
        UnityEngine.Renderer renderer { get; /* RVA: 0x0B30B1B8 */ }
        UnityEngine.Mesh mesh { get; /* RVA: 0x0B30B0FC */ }
        UnityEngine.MeshFilter meshFilter { get; /* RVA: 0x0B30AFEC */ }
        TMPro.MaskingTypes maskType { get; /* RVA: 0x04DB9A80 */ set; /* RVA: 0x0B30B4D0 */ }

        // Events
        event System.Action<TMPro.TMP_TextInfo> OnPreRenderText;

        // Methods
        // RVA: 0x0B309128  token: 0x6000062
        public System.Void SetMask(TMPro.MaskingTypes type, UnityEngine.Vector4 maskCoords) { }
        // RVA: 0x0B3092EC  token: 0x6000063
        public System.Void SetMask(TMPro.MaskingTypes type, UnityEngine.Vector4 maskCoords, System.Single softnessX, System.Single softnessY) { }
        // RVA: 0x0B309D8C  token: 0x6000064
        public virtual System.Void SetVerticesDirty() { }
        // RVA: 0x0B308EFC  token: 0x6000065
        public virtual System.Void SetLayoutDirty() { }
        // RVA: 0x0A973704  token: 0x6000066
        public virtual System.Void SetMaterialDirty() { }
        // RVA: 0x04056D70  token: 0x6000067
        public virtual System.Void SetAllDirty() { }
        // RVA: 0x0B306654  token: 0x6000068
        public virtual System.Void Rebuild(UnityEngine.UI.CanvasUpdate update) { }
        // RVA: 0x0B30A0D4  token: 0x6000069
        protected virtual System.Void UpdateMaterial() { }
        // RVA: 0x0B30A1F4  token: 0x600006A
        public virtual System.Void UpdateMeshPadding() { }
        // RVA: 0x0B305044  token: 0x600006B
        public virtual System.Void ForceMeshUpdate(System.Boolean ignoreActiveState, System.Boolean forceTextReparsing) { }
        // RVA: 0x0B305880  token: 0x600006C
        public virtual TMPro.TMP_TextInfo GetTextInfo(System.String text) { }
        // RVA: 0x0B304A44  token: 0x600006D
        public virtual System.Void ClearMesh(System.Boolean updateMesh) { }
        // RVA: 0x0B30A044  token: 0x6000070
        public virtual System.Void UpdateGeometry(UnityEngine.Mesh mesh, System.Int32 index) { }
        // RVA: 0x0B30A690  token: 0x6000071
        public virtual System.Void UpdateVertexData(TMPro.TMP_VertexDataUpdateFlags flags) { }
        // RVA: 0x0B30A854  token: 0x6000072
        public virtual System.Void UpdateVertexData() { }
        // RVA: 0x0B303B60  token: 0x6000073
        public System.Void UpdateFontAsset() { }
        // RVA: 0x041E1670  token: 0x6000074
        public virtual System.Void CalculateLayoutInputHorizontal() { }
        // RVA: 0x041E1670  token: 0x6000075
        public virtual System.Void CalculateLayoutInputVertical() { }
        // RVA: 0x0B30461C  token: 0x6000076
        protected virtual System.Void Awake() { }
        // RVA: 0x0B306178  token: 0x6000077
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0B3060DC  token: 0x6000078
        protected virtual System.Void OnDisable() { }
        // RVA: 0x0B306018  token: 0x6000079
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x0B305990  token: 0x600007A
        protected virtual System.Void LoadFontAsset() { }
        // RVA: 0x0B309E08  token: 0x600007B
        private System.Void UpdateEnvMapMatrix() { }
        // RVA: 0x0B309164  token: 0x600007C
        private System.Void SetMask(TMPro.MaskingTypes maskType) { }
        // RVA: 0x0B3090A4  token: 0x600007D
        private System.Void SetMaskCoordinates(UnityEngine.Vector4 coords) { }
        // RVA: 0x0B308FA4  token: 0x600007E
        private System.Void SetMaskCoordinates(UnityEngine.Vector4 coords, System.Single softX, System.Single softY) { }
        // RVA: 0x0B304F28  token: 0x600007F
        private System.Void EnableMasking() { }
        // RVA: 0x0B304E0C  token: 0x6000080
        private System.Void DisableMasking() { }
        // RVA: 0x0B30A068  token: 0x6000081
        private System.Void UpdateMask() { }
        // RVA: 0x0B305408  token: 0x6000082
        protected virtual UnityEngine.Material GetMaterial(UnityEngine.Material mat) { }
        // RVA: 0x0B305514  token: 0x6000083
        protected virtual UnityEngine.Material[] GetMaterials(UnityEngine.Material[] mats) { }
        // RVA: 0x0B3034A0  token: 0x6000084
        protected virtual System.Void SetSharedMaterial(UnityEngine.Material mat) { }
        // RVA: 0x0B305690  token: 0x6000085
        protected virtual UnityEngine.Material[] GetSharedMaterials() { }
        // RVA: 0x0B309A10  token: 0x6000086
        protected virtual System.Void SetSharedMaterials(UnityEngine.Material[] materials) { }
        // RVA: 0x0B309680  token: 0x6000087
        protected virtual System.Void SetOutlineThickness(System.Single thickness) { }
        // RVA: 0x0B308DB4  token: 0x6000088
        protected virtual System.Void SetFaceColor(UnityEngine.Color32 color) { }
        // RVA: 0x0B309538  token: 0x6000089
        protected virtual System.Void SetOutlineColor(UnityEngine.Color32 color) { }
        // RVA: 0x0B304C74  token: 0x600008A
        private System.Void CreateMaterialInstance() { }
        // RVA: 0x0B309890  token: 0x600008B
        protected virtual System.Void SetShaderDepth() { }
        // RVA: 0x0B308A60  token: 0x600008C
        protected virtual System.Void SetCulling() { }
        // RVA: 0x0B3097F0  token: 0x600008D
        private System.Void SetPerspectiveCorrection() { }
        // RVA: 0x0B306928  token: 0x600008E
        protected virtual System.Int32 SetArraySizes(TMPro.TMP_Text.UnicodeChar[] unicodeChars) { }
        // RVA: 0x0B304B2C  token: 0x600008F
        public virtual System.Void ComputeMarginSize() { }
        // RVA: 0x0B3060C0  token: 0x6000090
        protected virtual System.Void OnDidApplyAnimationProperties() { }
        // RVA: 0x0B30662C  token: 0x6000091
        protected virtual System.Void OnTransformParentChanged() { }
        // RVA: 0x0B3064AC  token: 0x6000092
        protected virtual System.Void OnRectTransformDimensionsChange() { }
        // RVA: 0x0B3058D8  token: 0x6000093
        private virtual System.Void InternalUpdate() { }
        // RVA: 0x0B306254  token: 0x6000094
        private System.Void OnPreRenderObject() { }
        // RVA: 0x0B2F4F6C  token: 0x6000095
        protected virtual System.Void GenerateTextMesh() { }
        // RVA: 0x0B3057E0  token: 0x6000096
        protected virtual UnityEngine.Vector3[] GetTextContainerLocalCorners() { }
        // RVA: 0x0B309348  token: 0x6000097
        private System.Void SetMeshFilters(System.Boolean state) { }
        // RVA: 0x0B306718  token: 0x6000098
        protected virtual System.Void SetActiveSubMeshes(System.Boolean state) { }
        // RVA: 0x0B306814  token: 0x6000099
        protected System.Void SetActiveSubTextObjectRenderers(System.Boolean state) { }
        // RVA: 0x0B304D4C  token: 0x600009A
        protected virtual System.Void DestroySubMeshObjects() { }
        // RVA: 0x0B30A490  token: 0x600009B
        private System.Void UpdateSubMeshSortingLayerID(System.Int32 id) { }
        // RVA: 0x0B30A590  token: 0x600009C
        private System.Void UpdateSubMeshSortingOrder(System.Int32 order) { }
        // RVA: 0x0B305058  token: 0x600009D
        protected virtual UnityEngine.Bounds GetCompoundBounds() { }
        // RVA: 0x0B30A2FC  token: 0x600009E
        private System.Void UpdateSDFScale(System.Single scaleDelta) { }
        // RVA: 0x0B30AEA0  token: 0x600009F
        public System.Void .ctor() { }
        // RVA: 0x0B30AA24  token: 0x60000A0
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000F  // size: 0xBC8
    public class TextMeshProUGUI : TMPro.TMP_Text, UnityEngine.UI.ILayoutElement
    {
        // Fields
        private System.Boolean m_isRebuildingLayout;  // 0xaf8
        private UnityEngine.Coroutine m_DelayedGraphicRebuild;  // 0xb00
        private UnityEngine.Coroutine m_DelayedMaterialRebuild;  // 0xb08
        private UnityEngine.Rect m_ClipRect;  // 0xb10
        private System.Boolean m_ValidRect;  // 0xb20
        private System.Action<TMPro.TMP_TextInfo> OnPreRenderText;  // 0xb28
        private System.Boolean m_hasFontAssetChanged;  // 0xb30
        protected TMPro.TMP_SubMeshUI[] m_subTextObjects;  // 0xb38
        protected UnityEngine.Vector3[] m_RectTransformCorners;  // 0xb40
        protected UnityEngine.CanvasRenderer m_canvasRenderer;  // 0xb48
        protected UnityEngine.Canvas m_canvas;  // 0xb50
        protected System.Single m_CanvasScaleFactor;  // 0xb58
        protected System.Single m_previousLossyScaleY;  // 0xb5c
        private System.Boolean m_isFirstAllocation;  // 0xb60
        private System.Int32 m_max_characters;  // 0xb64
        private UnityEngine.Material m_baseMaterial;  // 0xb68
        private System.Boolean m_isScrollRegionSet;  // 0xb70
        private UnityEngine.Vector4 m_maskOffset;  // 0xb74
        private UnityEngine.Matrix4x4 m_EnvMapMatrix;  // 0xb84
        private System.Boolean m_isRegisteredForEvents;  // 0xbc4
        private static Unity.Profiling.ProfilerMarker k_GenerateTextMarker;  // static @ 0x0
        private static Unity.Profiling.ProfilerMarker k_SetArraySizesMarker;  // static @ 0x8
        private static Unity.Profiling.ProfilerMarker k_GenerateTextPhaseIMarker;  // static @ 0x10
        private static Unity.Profiling.ProfilerMarker k_ParseMarkupTextMarker;  // static @ 0x18
        private static Unity.Profiling.ProfilerMarker k_CharacterLookupMarker;  // static @ 0x20
        private static Unity.Profiling.ProfilerMarker k_HandleGPOSFeaturesMarker;  // static @ 0x28
        private static Unity.Profiling.ProfilerMarker k_CalculateVerticesPositionMarker;  // static @ 0x30
        private static Unity.Profiling.ProfilerMarker k_ComputeTextMetricsMarker;  // static @ 0x38
        private static Unity.Profiling.ProfilerMarker k_HandleVisibleCharacterMarker;  // static @ 0x40
        private static Unity.Profiling.ProfilerMarker k_HandleWhiteSpacesMarker;  // static @ 0x48
        private static Unity.Profiling.ProfilerMarker k_HandleHorizontalLineBreakingMarker;  // static @ 0x50
        private static Unity.Profiling.ProfilerMarker k_HandleVerticalLineBreakingMarker;  // static @ 0x58
        private static Unity.Profiling.ProfilerMarker k_SaveGlyphVertexDataMarker;  // static @ 0x60
        private static Unity.Profiling.ProfilerMarker k_ComputeCharacterAdvanceMarker;  // static @ 0x68
        private static Unity.Profiling.ProfilerMarker k_HandleCarriageReturnMarker;  // static @ 0x70
        private static Unity.Profiling.ProfilerMarker k_HandleLineTerminationMarker;  // static @ 0x78
        private static Unity.Profiling.ProfilerMarker k_SavePageInfoMarker;  // static @ 0x80
        private static Unity.Profiling.ProfilerMarker k_SaveProcessingStatesMarker;  // static @ 0x88
        private static Unity.Profiling.ProfilerMarker k_GenerateTextPhaseIIMarker;  // static @ 0x90
        private static Unity.Profiling.ProfilerMarker k_GenerateTextPhaseIIIMarker;  // static @ 0x98

        // Properties
        UnityEngine.Material materialForRendering { get; /* RVA: 0x03E32420 */ }
        System.Boolean autoSizeTextContainer { get; /* RVA: 0x04DB9A50 */ set; /* RVA: 0x0B304584 */ }
        UnityEngine.Mesh mesh { get; /* RVA: 0x04DB9A70 */ }
        UnityEngine.CanvasRenderer canvasRenderer { get; /* RVA: 0x03E57CB0 */ }
        UnityEngine.Vector4 maskOffset { get; /* RVA: 0x04DB9A60 */ set; /* RVA: 0x0B3045F4 */ }

        // Events
        event System.Action<TMPro.TMP_TextInfo> OnPreRenderText;

        // Methods
        // RVA: 0x041E1670  token: 0x60000A6
        public virtual System.Void CalculateLayoutInputHorizontal() { }
        // RVA: 0x041E1670  token: 0x60000A7
        public virtual System.Void CalculateLayoutInputVertical() { }
        // RVA: 0x03519FC0  token: 0x60000A8
        public virtual System.Void SetVerticesDirty() { }
        // RVA: 0x031E8280  token: 0x60000A9
        public virtual System.Void SetLayoutDirty() { }
        // RVA: 0x0351A210  token: 0x60000AA
        public virtual System.Void SetMaterialDirty() { }
        // RVA: 0x04056D70  token: 0x60000AB
        public virtual System.Void SetAllDirty() { }
        // RVA: 0x0B30225C  token: 0x60000AC
        private System.Collections.IEnumerator DelayedGraphicRebuild() { }
        // RVA: 0x0B3022B8  token: 0x60000AD
        private System.Collections.IEnumerator DelayedMaterialRebuild() { }
        // RVA: 0x03CF2490  token: 0x60000AE
        public virtual System.Void Rebuild(UnityEngine.UI.CanvasUpdate update) { }
        // RVA: 0x031E7FA0  token: 0x60000AF
        private System.Void UpdateSubObjectPivot() { }
        // RVA: 0x02EDE7E0  token: 0x60000B0
        public virtual UnityEngine.Material GetModifiedMaterial(UnityEngine.Material baseMaterial) { }
        // RVA: 0x03E57B40  token: 0x60000B1
        protected virtual System.Void UpdateMaterial() { }
        // RVA: 0x03F59BE0  token: 0x60000B4
        public virtual System.Void RecalculateClipping() { }
        // RVA: 0x0323F3B0  token: 0x60000B5
        public virtual System.Void Cull(UnityEngine.Rect clipRect, System.Boolean validRect) { }
        // RVA: 0x03F28900  token: 0x60000B6
        private virtual System.Void UpdateCulling() { }
        // RVA: 0x0B30403C  token: 0x60000B7
        public virtual System.Void UpdateMeshPadding() { }
        // RVA: 0x04731510  token: 0x60000B8
        protected virtual System.Void InternalCrossFadeColor(UnityEngine.Color targetColor, System.Single duration, System.Boolean ignoreTimeScale, System.Boolean useAlpha) { }
        // RVA: 0x0B302AA8  token: 0x60000B9
        protected virtual System.Void InternalCrossFadeAlpha(System.Single alpha, System.Single duration, System.Boolean ignoreTimeScale) { }
        // RVA: 0x03CF23E0  token: 0x60000BA
        public virtual System.Void ForceMeshUpdate(System.Boolean ignoreActiveState, System.Boolean forceTextReparsing) { }
        // RVA: 0x0B3029DC  token: 0x60000BB
        public virtual TMPro.TMP_TextInfo GetTextInfo(System.String text) { }
        // RVA: 0x04404550  token: 0x60000BC
        public virtual System.Void ClearMesh() { }
        // RVA: 0x0B303B70  token: 0x60000BF
        public virtual System.Void UpdateGeometry(UnityEngine.Mesh mesh, System.Int32 index) { }
        // RVA: 0x0B304144  token: 0x60000C0
        public virtual System.Void UpdateVertexData(TMPro.TMP_VertexDataUpdateFlags flags) { }
        // RVA: 0x0B30435C  token: 0x60000C1
        public virtual System.Void UpdateVertexData() { }
        // RVA: 0x0B303B60  token: 0x60000C2
        public System.Void UpdateFontAsset() { }
        // RVA: 0x0351ED70  token: 0x60000C3
        protected virtual System.Void Awake() { }
        // RVA: 0x0351C480  token: 0x60000C4
        protected virtual System.Void OnEnable() { }
        // RVA: 0x03519C60  token: 0x60000C5
        protected virtual System.Void OnDisable() { }
        // RVA: 0x03519510  token: 0x60000C6
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x0333C620  token: 0x60000C7
        protected virtual System.Void LoadFontAsset() { }
        // RVA: 0x0351C5B0  token: 0x60000C8
        private UnityEngine.Canvas GetCanvas() { }
        // RVA: 0x0B303924  token: 0x60000C9
        private System.Void UpdateEnvMapMatrix() { }
        // RVA: 0x0B3023D4  token: 0x60000CA
        private System.Void EnableMasking() { }
        // RVA: 0x041E1670  token: 0x60000CB
        private System.Void DisableMasking() { }
        // RVA: 0x0B303BFC  token: 0x60000CC
        private System.Void UpdateMask() { }
        // RVA: 0x0B3025D8  token: 0x60000CD
        protected virtual UnityEngine.Material GetMaterial(UnityEngine.Material mat) { }
        // RVA: 0x0B302708  token: 0x60000CE
        protected virtual UnityEngine.Material[] GetMaterials(UnityEngine.Material[] mats) { }
        // RVA: 0x0B3034A0  token: 0x60000CF
        protected virtual System.Void SetSharedMaterial(UnityEngine.Material mat) { }
        // RVA: 0x0B302888  token: 0x60000D0
        protected virtual UnityEngine.Material[] GetSharedMaterials() { }
        // RVA: 0x0B3034E0  token: 0x60000D1
        protected virtual System.Void SetSharedMaterials(UnityEngine.Material[] materials) { }
        // RVA: 0x0B303104  token: 0x60000D2
        protected virtual System.Void SetOutlineThickness(System.Single thickness) { }
        // RVA: 0x0B302E18  token: 0x60000D3
        protected virtual System.Void SetFaceColor(UnityEngine.Color32 color) { }
        // RVA: 0x0B302FE8  token: 0x60000D4
        protected virtual System.Void SetOutlineColor(UnityEngine.Color32 color) { }
        // RVA: 0x0B30337C  token: 0x60000D5
        protected virtual System.Void SetShaderDepth() { }
        // RVA: 0x0B302B54  token: 0x60000D6
        protected virtual System.Void SetCulling() { }
        // RVA: 0x0B3032DC  token: 0x60000D7
        private System.Void SetPerspectiveCorrection() { }
        // RVA: 0x0B302F34  token: 0x60000D8
        private System.Void SetMeshArrays(System.Int32 size) { }
        // RVA: 0x0333D430  token: 0x60000D9
        protected virtual System.Int32 SetArraySizes(TMPro.TMP_Text.UnicodeChar[] unicodeChars) { }
        // RVA: 0x031E8050  token: 0x60000DA
        public virtual System.Void ComputeMarginSize() { }
        // RVA: 0x043EE460  token: 0x60000DB
        protected virtual System.Void OnDidApplyAnimationProperties() { }
        // RVA: 0x03B914B0  token: 0x60000DC
        protected virtual System.Void OnCanvasHierarchyChanged() { }
        // RVA: 0x03F3AE50  token: 0x60000DD
        protected virtual System.Void OnTransformParentChanged() { }
        // RVA: 0x031E7C60  token: 0x60000DE
        protected virtual System.Void OnRectTransformDimensionsChange() { }
        // RVA: 0x02FAB6B0  token: 0x60000DF
        private virtual System.Void InternalUpdate() { }
        // RVA: 0x03CF2540  token: 0x60000E0
        private System.Void OnPreRenderCanvas() { }
        // RVA: 0x0B302218  token: 0x60000E1
        protected virtual TMPro.TMP_SubMeshUI AddSubTextObject(TMPro.TextMeshProUGUI textComponent, TMPro.MaterialReference materialReference) { }
        // RVA: 0x03254CE0  token: 0x60000E2
        protected virtual System.Void GenerateTextMesh() { }
        // RVA: 0x03EADEF0  token: 0x60000E3
        protected virtual UnityEngine.Vector3[] GetTextContainerLocalCorners() { }
        // RVA: 0x03E996F0  token: 0x60000E4
        protected virtual System.Void SetActiveSubMeshes(System.Boolean state) { }
        // RVA: 0x0B302314  token: 0x60000E5
        protected virtual System.Void DestroySubMeshObjects() { }
        // RVA: 0x0323FC30  token: 0x60000E6
        protected virtual UnityEngine.Bounds GetCompoundBounds() { }
        // RVA: 0x0323F6B0  token: 0x60000E7
        private virtual UnityEngine.Rect GetCanvasSpaceClippingRect() { }
        // RVA: 0x031CC750  token: 0x60000E8
        private System.Void UpdateSDFScale(System.Single scaleDelta) { }
        // RVA: 0x03695970  token: 0x60000E9
        public System.Void .ctor() { }
        // RVA: 0x047CE1A0  token: 0x60000EA
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000012  // size: 0x14
    public sealed struct Compute_DistanceTransform_EventTypes
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.Compute_DistanceTransform_EventTypes Processing;  // const
        public static TMPro.Compute_DistanceTransform_EventTypes Completed;  // const

    }

    // TypeToken: 0x2000013  // size: 0x10
    public static class TMPro_EventManager
    {
        // Fields
        public static readonly TMPro.FastAction<System.Object,TMPro.Compute_DT_EventArgs> COMPUTE_DT_EVENT;  // static @ 0x0
        public static readonly TMPro.FastAction<System.Boolean,UnityEngine.Material> MATERIAL_PROPERTY_EVENT;  // static @ 0x8
        public static readonly TMPro.FastAction<System.Boolean,UnityEngine.Object> FONT_PROPERTY_EVENT;  // static @ 0x10
        public static readonly TMPro.FastAction<System.Boolean,UnityEngine.Object> SPRITE_ASSET_PROPERTY_EVENT;  // static @ 0x18
        public static readonly TMPro.FastAction<System.Boolean,UnityEngine.Object> TEXTMESHPRO_PROPERTY_EVENT;  // static @ 0x20
        public static readonly TMPro.FastAction<UnityEngine.GameObject,UnityEngine.Material,UnityEngine.Material> DRAG_AND_DROP_MATERIAL_EVENT;  // static @ 0x28
        public static readonly TMPro.FastAction<System.Boolean> TEXT_STYLE_PROPERTY_EVENT;  // static @ 0x30
        public static readonly TMPro.FastAction<UnityEngine.Object> COLOR_GRADIENT_PROPERTY_EVENT;  // static @ 0x38
        public static readonly TMPro.FastAction TMP_SETTINGS_PROPERTY_EVENT;  // static @ 0x40
        public static readonly TMPro.FastAction RESOURCE_LOAD_EVENT;  // static @ 0x48
        public static readonly TMPro.FastAction<System.Boolean,UnityEngine.Object> TEXTMESHPRO_UGUI_PROPERTY_EVENT;  // static @ 0x50
        public static readonly TMPro.FastAction<UnityEngine.Object> TEXT_CHANGED_EVENT;  // static @ 0x58

        // Methods
        // RVA: 0x0B31923C  token: 0x60000F7
        public static System.Void ON_MATERIAL_PROPERTY_CHANGED(System.Boolean isChanged, UnityEngine.Material mat) { }
        // RVA: 0x0B3191BC  token: 0x60000F8
        public static System.Void ON_FONT_PROPERTY_CHANGED(System.Boolean isChanged, UnityEngine.Object obj) { }
        // RVA: 0x0B319310  token: 0x60000F9
        public static System.Void ON_SPRITE_ASSET_PROPERTY_CHANGED(System.Boolean isChanged, UnityEngine.Object obj) { }
        // RVA: 0x0B319390  token: 0x60000FA
        public static System.Void ON_TEXTMESHPRO_PROPERTY_CHANGED(System.Boolean isChanged, UnityEngine.Object obj) { }
        // RVA: 0x0B319128  token: 0x60000FB
        public static System.Void ON_DRAG_AND_DROP_MATERIAL_CHANGED(UnityEngine.GameObject sender, UnityEngine.Material currentMaterial, UnityEngine.Material newMaterial) { }
        // RVA: 0x0B319490  token: 0x60000FC
        public static System.Void ON_TEXT_STYLE_PROPERTY_CHANGED(System.Boolean isChanged) { }
        // RVA: 0x0B31903C  token: 0x60000FD
        public static System.Void ON_COLOR_GRADIENT_PROPERTY_CHANGED(UnityEngine.Object obj) { }
        // RVA: 0x040736F0  token: 0x60000FE
        public static System.Void ON_TEXT_CHANGED(UnityEngine.Object obj) { }
        // RVA: 0x0B3194FC  token: 0x60000FF
        public static System.Void ON_TMP_SETTINGS_CHANGED() { }
        // RVA: 0x0B3192BC  token: 0x6000100
        public static System.Void ON_RESOURCES_LOADED() { }
        // RVA: 0x0B319410  token: 0x6000101
        public static System.Void ON_TEXTMESHPRO_UGUI_PROPERTY_CHANGED(System.Boolean isChanged, UnityEngine.Object obj) { }
        // RVA: 0x0B3190AC  token: 0x6000102
        public static System.Void ON_COMPUTE_DT_EVENT(System.Object Sender, TMPro.Compute_DT_EventArgs e) { }
        // RVA: 0x0478E7A0  token: 0x6000103
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000014  // size: 0x20
    public class Compute_DT_EventArgs
    {
        // Fields
        public TMPro.Compute_DistanceTransform_EventTypes EventType;  // 0x10
        public System.Single ProgressPercentage;  // 0x14
        public UnityEngine.Color[] Colors;  // 0x18

        // Methods
        // RVA: 0x04DB9A90  token: 0x6000104
        public System.Void .ctor(TMPro.Compute_DistanceTransform_EventTypes type, System.Single progress) { }
        // RVA: 0x05391D70  token: 0x6000105
        public System.Void .ctor(TMPro.Compute_DistanceTransform_EventTypes type, UnityEngine.Color[] colors) { }

    }

    // TypeToken: 0x2000015  // size: 0x10
    public static class TMPro_ExtensionMethods
    {
        // Methods
        // RVA: 0x0B319AC8  token: 0x6000106
        public static System.Int32[] ToIntArray(System.String text) { }
        // RVA: 0x0B319550  token: 0x6000107
        public static System.String ArrayToString(System.Char[] chars) { }
        // RVA: 0x0B31978C  token: 0x6000108
        public static System.String IntToString(System.Int32[] unicodes) { }
        // RVA: 0x0B319B58  token: 0x6000109
        private static System.String UintToString(System.Collections.Generic.List<System.UInt32> unicodes) { }
        // RVA: 0x0B31980C  token: 0x600010A
        public static System.String IntToString(System.Int32[] unicodes, System.Int32 start, System.Int32 length) { }
        // RVA: -1  // generic def  token: 0x600010B
        public static System.Int32 FindInstanceID(System.Collections.Generic.List<T> list, T target) { }
        // RVA: 0x04D333E0  token: 0x600010C
        public static System.Boolean Compare(UnityEngine.Color32 a, UnityEngine.Color32 b) { }
        // RVA: 0x0B319614  token: 0x600010D
        public static System.Boolean CompareRGB(UnityEngine.Color32 a, UnityEngine.Color32 b) { }
        // RVA: 0x0415FC30  token: 0x600010E
        public static System.Boolean Compare(UnityEngine.Color a, UnityEngine.Color b) { }
        // RVA: 0x030F9620  token: 0x600010F
        public static System.Boolean CompareRGB(UnityEngine.Color a, UnityEngine.Color b) { }
        // RVA: 0x0B319908  token: 0x6000110
        public static UnityEngine.Color32 Multiply(UnityEngine.Color32 c1, UnityEngine.Color32 c2) { }
        // RVA: 0x0B319908  token: 0x6000111
        public static UnityEngine.Color32 Tint(UnityEngine.Color32 c1, UnityEngine.Color32 c2) { }
        // RVA: 0x0B3199E8  token: 0x6000112
        public static UnityEngine.Color32 Tint(UnityEngine.Color32 c1, System.Single tint) { }
        // RVA: 0x0B3198E0  token: 0x6000113
        public static UnityEngine.Color MinAlpha(UnityEngine.Color c1, UnityEngine.Color c2) { }
        // RVA: 0x0B3196FC  token: 0x6000114
        public static System.Boolean Compare(UnityEngine.Vector3 v1, UnityEngine.Vector3 v2, System.Int32 accuracy) { }
        // RVA: 0x0B319638  token: 0x6000115
        public static System.Boolean Compare(UnityEngine.Quaternion q1, UnityEngine.Quaternion q2, System.Int32 accuracy) { }

    }

    // TypeToken: 0x2000016  // size: 0x10
    public static class TMP_Math
    {
        // Fields
        public static System.Single FLOAT_MAX;  // const
        public static System.Single FLOAT_MIN;  // const
        public static System.Int32 INT_MAX;  // const
        public static System.Int32 INT_MIN;  // const
        public static System.Single FLOAT_UNSET;  // const
        public static System.Int32 INT_UNSET;  // const
        public static UnityEngine.Vector2 MAX_16BIT;  // static @ 0x0
        public static UnityEngine.Vector2 MIN_16BIT;  // static @ 0x8

        // Methods
        // RVA: 0x0B318C88  token: 0x6000116
        public static System.Boolean Approximately(System.Single a, System.Single b) { }
        // RVA: 0x0B318CAC  token: 0x6000117
        public static System.Int32 Mod(System.Int32 a, System.Int32 b) { }
        // RVA: 0x0B318CC4  token: 0x6000118
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000017  // size: 0x14
    public sealed struct TMP_VertexDataUpdateFlags
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.TMP_VertexDataUpdateFlags None;  // const
        public static TMPro.TMP_VertexDataUpdateFlags Vertices;  // const
        public static TMPro.TMP_VertexDataUpdateFlags Uv0;  // const
        public static TMPro.TMP_VertexDataUpdateFlags Uv2;  // const
        public static TMPro.TMP_VertexDataUpdateFlags Uv4;  // const
        public static TMPro.TMP_VertexDataUpdateFlags Colors32;  // const
        public static TMPro.TMP_VertexDataUpdateFlags All;  // const

    }

    // TypeToken: 0x2000018  // size: 0x50
    public sealed struct VertexGradient
    {
        // Fields
        public UnityEngine.Color topLeft;  // 0x10
        public UnityEngine.Color topRight;  // 0x20
        public UnityEngine.Color bottomLeft;  // 0x30
        public UnityEngine.Color bottomRight;  // 0x40

        // Methods
        // RVA: 0x04DB9C80  token: 0x6000119
        public System.Void .ctor(UnityEngine.Color color) { }
        // RVA: 0x04DB3220  token: 0x600011A
        public System.Void .ctor(UnityEngine.Color color0, UnityEngine.Color color1, UnityEngine.Color color2, UnityEngine.Color color3) { }

    }

    // TypeToken: 0x2000019  // size: 0x24
    public sealed struct TMP_PageInfo
    {
        // Fields
        public System.Int32 firstCharacterIndex;  // 0x10
        public System.Int32 lastCharacterIndex;  // 0x14
        public System.Single ascender;  // 0x18
        public System.Single baseLine;  // 0x1c
        public System.Single descender;  // 0x20

    }

    // TypeToken: 0x200001A  // size: 0x38
    public sealed struct TMP_LinkInfo
    {
        // Fields
        public TMPro.TMP_Text textComponent;  // 0x10
        public System.Int32 hashCode;  // 0x18
        public System.Int32 linkIdFirstCharacterIndex;  // 0x1c
        public System.Int32 linkIdLength;  // 0x20
        public System.Int32 linkTextfirstCharacterIndex;  // 0x24
        public System.Int32 linkTextLength;  // 0x28
        private System.Char[] linkID;  // 0x30

        // Methods
        // RVA: 0x0B318BC4  token: 0x600011B
        private System.Void SetLinkID(System.Char[] text, System.Int32 startIndex, System.Int32 length) { }
        // RVA: 0x0B318AE4  token: 0x600011C
        public System.String GetLinkText() { }
        // RVA: 0x0B318A58  token: 0x600011D
        public System.String GetLinkID() { }

    }

    // TypeToken: 0x200001B  // size: 0x28
    public sealed struct TMP_WordInfo
    {
        // Fields
        public TMPro.TMP_Text textComponent;  // 0x10
        public System.Int32 firstCharacterIndex;  // 0x18
        public System.Int32 lastCharacterIndex;  // 0x1c
        public System.Int32 characterCount;  // 0x20

        // Methods
        // RVA: 0x0B318F58  token: 0x600011E
        public System.String GetWord() { }

    }

    // TypeToken: 0x200001C  // size: 0x1C
    public sealed struct TMP_SpriteInfo
    {
        // Fields
        public System.Int32 spriteIndex;  // 0x10
        public System.Int32 characterIndex;  // 0x14
        public System.Int32 vertexIndex;  // 0x18

    }

    // TypeToken: 0x200001D  // size: 0x20
    public sealed struct Extents
    {
        // Fields
        private static TMPro.Extents zero;  // static @ 0x0
        private static TMPro.Extents uninitialized;  // static @ 0x10
        public UnityEngine.Vector2 min;  // 0x10
        public UnityEngine.Vector2 max;  // 0x18

        // Methods
        // RVA: 0x04D861B0  token: 0x600011F
        public System.Void .ctor(UnityEngine.Vector2 min, UnityEngine.Vector2 max) { }
        // RVA: 0x0B30BA2C  token: 0x6000120
        public virtual System.String ToString() { }
        // RVA: 0x0B30BBB0  token: 0x6000121
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200001E  // size: 0x20
    public sealed struct Mesh_Extents
    {
        // Fields
        public UnityEngine.Vector2 min;  // 0x10
        public UnityEngine.Vector2 max;  // 0x18

        // Methods
        // RVA: 0x04D861B0  token: 0x6000122
        public System.Void .ctor(UnityEngine.Vector2 min, UnityEngine.Vector2 max) { }
        // RVA: 0x0B30C7E8  token: 0x6000123
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200001F  // size: 0x3A8
    public sealed struct WordWrapState
    {
        // Fields
        public System.Int32 previous_WordBreak;  // 0x10
        public System.Int32 total_CharacterCount;  // 0x14
        public System.Int32 visible_CharacterCount;  // 0x18
        public System.Int32 visible_SpriteCount;  // 0x1c
        public System.Int32 visible_LinkCount;  // 0x20
        public System.Int32 firstCharacterIndex;  // 0x24
        public System.Int32 firstVisibleCharacterIndex;  // 0x28
        public System.Int32 lastCharacterIndex;  // 0x2c
        public System.Int32 lastVisibleCharIndex;  // 0x30
        public System.Int32 lineNumber;  // 0x34
        public System.Single maxCapHeight;  // 0x38
        public System.Single maxAscender;  // 0x3c
        public System.Single maxLineAscenderWithOutImage;  // 0x40
        public System.Single maxDescender;  // 0x44
        public System.Single startOfLineAscender;  // 0x48
        public System.Single maxLineAscender;  // 0x4c
        public System.Single maxLineDescender;  // 0x50
        public System.Single pageAscender;  // 0x54
        public TMPro.HorizontalAlignmentOptions horizontalAlignment;  // 0x58
        public System.Single marginLeft;  // 0x5c
        public System.Single marginRight;  // 0x60
        public System.Single xAdvance;  // 0x64
        public System.Single preferredWidth;  // 0x68
        public System.Single preferredHeight;  // 0x6c
        public System.Single previousLineScale;  // 0x70
        public System.Int32 wordCount;  // 0x74
        public TMPro.FontStyles fontStyle;  // 0x78
        public System.Int32 italicAngle;  // 0x7c
        public System.Single fontScaleMultiplier;  // 0x80
        public System.Single currentFontSize;  // 0x84
        public System.Single baselineOffset;  // 0x88
        public System.Single lineOffset;  // 0x8c
        public System.Boolean isDrivenLineSpacing;  // 0x90
        public System.Single glyphHorizontalAdvanceAdjustment;  // 0x94
        public System.Single cSpace;  // 0x98
        public System.Single mSpace;  // 0x9c
        public TMPro.TMP_TextInfo textInfo;  // 0xa0
        public TMPro.TMP_LineInfo lineInfo;  // 0xa8
        public UnityEngine.Color32 vertexColor;  // 0x104
        public UnityEngine.Color32 underlineColor;  // 0x108
        public UnityEngine.Color32 strikethroughColor;  // 0x10c
        public UnityEngine.Color32 highlightColor;  // 0x110
        public TMPro.TMP_FontStyleStack basicStyleStack;  // 0x114
        public TMPro.TMP_TextProcessingStack<System.Int32> italicAngleStack;  // 0x120
        public TMPro.TMP_TextProcessingStack<UnityEngine.Color32> colorStack;  // 0x140
        public TMPro.TMP_TextProcessingStack<UnityEngine.Color32> underlineColorStack;  // 0x160
        public TMPro.TMP_TextProcessingStack<UnityEngine.Color32> strikethroughColorStack;  // 0x180
        public TMPro.TMP_TextProcessingStack<UnityEngine.Color32> highlightColorStack;  // 0x1a0
        public TMPro.TMP_TextProcessingStack<TMPro.HighlightState> highlightStateStack;  // 0x1c0
        public TMPro.TMP_TextProcessingStack<TMPro.TMP_ColorGradient> colorGradientStack;  // 0x1f0
        public TMPro.TMP_TextProcessingStack<System.Single> sizeStack;  // 0x218
        public TMPro.TMP_TextProcessingStack<System.Single> indentStack;  // 0x238
        public TMPro.TMP_TextProcessingStack<TMPro.FontWeight> fontWeightStack;  // 0x258
        public TMPro.TMP_TextProcessingStack<System.Int32> styleStack;  // 0x278
        public TMPro.TMP_TextProcessingStack<System.Single> baselineStack;  // 0x298
        public TMPro.TMP_TextProcessingStack<System.Int32> actionStack;  // 0x2b8
        public TMPro.TMP_TextProcessingStack<TMPro.MaterialReference> materialReferenceStack;  // 0x2d8
        public TMPro.TMP_TextProcessingStack<TMPro.HorizontalAlignmentOptions> lineJustificationStack;  // 0x330
        public TMPro.TMP_TextProcessingQueue<TMPro.PhoneticData> phoneticQueue;  // 0x350
        public System.Int32 spriteAnimationID;  // 0x370
        public TMPro.TMP_FontAsset currentFontAsset;  // 0x378
        public TMPro.TMP_SpriteAsset currentSpriteAsset;  // 0x380
        public UnityEngine.Material currentMaterial;  // 0x388
        public System.Int32 currentMaterialIndex;  // 0x390
        public TMPro.Extents meshExtents;  // 0x394
        public System.Boolean tagNoParsing;  // 0x3a4
        public System.Boolean isNonBreakingSpace;  // 0x3a5

    }

    // TypeToken: 0x2000020  // size: 0x1C
    public sealed struct TagAttribute
    {
        // Fields
        public System.Int32 startIndex;  // 0x10
        public System.Int32 length;  // 0x14
        public System.Int32 hashCode;  // 0x18

    }

    // TypeToken: 0x2000021  // size: 0x28
    public sealed struct RichTextTagAttribute
    {
        // Fields
        public System.Int32 nameHashCode;  // 0x10
        public System.Int32 valueHashCode;  // 0x14
        public TMPro.TagValueType valueType;  // 0x18
        public System.Int32 valueStartIndex;  // 0x1c
        public System.Int32 valueLength;  // 0x20
        public TMPro.TagUnitType unitType;  // 0x24

    }

    // TypeToken: 0x2000022
    public class TMP_ArrayObjectPool`1
    {
        // Fields
        private readonly System.Collections.Generic.Dictionary<System.Int32,System.Collections.Generic.Stack<T[]>> m_StackDic;  // 0x0
        private readonly UnityEngine.Events.UnityAction<T[]> m_ActionOnGet;  // 0x0
        private readonly UnityEngine.Events.UnityAction<T[]> m_ActionOnRelease;  // 0x0
        private readonly TMPro.TMP_ArrayObjectPool.PoolOptions<T> m_Options;  // 0x0
        private System.Int32 m_TotalCachedCount;  // 0x0
        private System.Int32 <countAll>k__BackingField;  // 0x0

        // Properties
        System.Int32 countAll { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Int32 countActive { get; /* RVA: -1  // not resolved */ }
        System.Int32 countInactive { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000128
        public System.Void .ctor(UnityEngine.Events.UnityAction<T[]> actionOnGet, UnityEngine.Events.UnityAction<T[]> actionOnRelease, TMPro.TMP_ArrayObjectPool.PoolOptions<T> options) { }
        // RVA: -1  // not resolved  token: 0x6000129
        public T[] Get(System.Int32 count) { }
        // RVA: -1  // not resolved  token: 0x600012A
        public System.Void Release(System.Int32 count, T[] element) { }
        // RVA: -1  // not resolved  token: 0x600012B
        public System.Void Shrink() { }
        // RVA: -1  // not resolved  token: 0x600012C
        private System.Boolean _CanUsePool(System.Int32 count) { }

    }

    // TypeToken: 0x2000024
    public static class TMP_ArrayPool`1
    {
        // Fields
        private static readonly TMPro.TMP_ArrayObjectPool<T> s_ArrayPool;  // static @ 0x0
        private static readonly TMPro.TMP_ArrayPoolWrapper<T> s_ArrayPoolWrapper;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600012F
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x6000130
        private static TMPro.TMP_ArrayObjectPool.PoolOptions<T> _CreateOptions() { }
        // RVA: -1  // not resolved  token: 0x6000131
        public static T[] Get(System.Int32 count) { }
        // RVA: -1  // not resolved  token: 0x6000132
        public static System.Void Release(T[] toRelease) { }
        // RVA: -1  // not resolved  token: 0x6000133
        public static System.Void Resize(T[]& array, System.Int32 newCount) { }
        // RVA: -1  // not resolved  token: 0x6000134
        public static System.Void Shrink() { }

    }

    // TypeToken: 0x2000026
    public interface IPoolClearable
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000138
        public virtual System.Void Clear() { }

    }

    // TypeToken: 0x2000027
    public interface ITMPArrayPoolRegistryItem
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000139
        public virtual System.Void Shrink() { }

    }

    // TypeToken: 0x2000028  // size: 0x10
    public static class TMP_ArrayPoolRegistry
    {
        // Fields
        private static readonly System.Collections.Generic.List<TMPro.ITMPArrayPoolRegistryItem> registeredPools;  // static @ 0x0

        // Methods
        // RVA: 0x04390C40  token: 0x600013A
        private static System.Void Register(TMPro.ITMPArrayPoolRegistryItem pool) { }
        // RVA: 0x0458CE10  token: 0x600013B
        public static System.Void ShrinkAll() { }
        // RVA: 0x04D16B00  token: 0x600013C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000029
    public class TMP_ArrayPoolWrapper`1 : TMPro.ITMPArrayPoolRegistryItem
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x600013D
        public virtual System.Void Shrink() { }
        // RVA: -1  // not resolved  token: 0x600013E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200002A  // size: 0x30
    public abstract class TMP_Asset : UnityEngine.ScriptableObject
    {
        // Fields
        private System.Int32 m_InstanceID;  // 0x18
        public System.Int32 hashCode;  // 0x1c
        public UnityEngine.Material material;  // 0x20
        public System.Int32 materialHashCode;  // 0x28

        // Properties
        System.Int32 instanceID { get; /* RVA: 0x0333E5E0 */ }

        // Methods
        // RVA: 0x03573370  token: 0x6000140
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200002B  // size: 0x30
    public class TMP_Character : TMPro.TMP_TextElement
    {
        // Methods
        // RVA: 0x04DB9B40  token: 0x6000141
        public System.Void .ctor() { }
        // RVA: 0x0B30CA24  token: 0x6000142
        public System.Void .ctor(System.UInt32 unicode, UnityEngine.TextCore.Glyph glyph) { }
        // RVA: 0x0393F5A0  token: 0x6000143
        public System.Void .ctor(System.UInt32 unicode, TMPro.TMP_FontAsset fontAsset, UnityEngine.TextCore.Glyph glyph) { }
        // RVA: 0x0B30C9E4  token: 0x6000144
        private System.Void .ctor(System.UInt32 unicode, System.UInt32 glyphIndex) { }

    }

    // TypeToken: 0x200002C  // size: 0x38
    public sealed struct TMP_Vertex
    {
        // Fields
        public UnityEngine.Vector3 position;  // 0x10
        public UnityEngine.Vector2 uv;  // 0x1c
        public UnityEngine.Vector2 uv2;  // 0x24
        public UnityEngine.Vector2 uv4;  // 0x2c
        public UnityEngine.Color32 color;  // 0x34
        private static readonly TMPro.TMP_Vertex k_Zero;  // static @ 0x0

        // Properties
        TMPro.TMP_Vertex zero { get; /* RVA: 0x0B318EF8 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000146
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200002D  // size: 0x20
    public sealed struct TMP_Offset
    {
        // Fields
        private System.Single m_Left;  // 0x10
        private System.Single m_Right;  // 0x14
        private System.Single m_Top;  // 0x18
        private System.Single m_Bottom;  // 0x1c
        private static readonly TMPro.TMP_Offset k_ZeroOffset;  // static @ 0x0

        // Properties
        System.Single left { get; /* RVA: 0x04D871D0 */ set; /* RVA: 0x04D932C0 */ }
        System.Single right { get; /* RVA: 0x04D9C560 */ set; /* RVA: 0x04DAB920 */ }
        System.Single top { get; /* RVA: 0x04D9C280 */ set; /* RVA: 0x04DAC1D0 */ }
        System.Single bottom { get; /* RVA: 0x04D9CC80 */ set; /* RVA: 0x04DACD70 */ }
        System.Single horizontal { get; /* RVA: 0x04D871D0 */ set; /* RVA: 0x04DB4640 */ }
        System.Single vertical { get; /* RVA: 0x04D9C280 */ set; /* RVA: 0x04DB9C70 */ }
        TMPro.TMP_Offset zero { get; /* RVA: 0x0B318E4C */ }

        // Methods
        // RVA: 0x04D88AD0  token: 0x6000154
        public System.Void .ctor(System.Single left, System.Single right, System.Single top, System.Single bottom) { }
        // RVA: 0x04DB9C50  token: 0x6000155
        public System.Void .ctor(System.Single horizontal, System.Single vertical) { }
        // RVA: 0x0415FC30  token: 0x6000156
        public static System.Boolean op_Equality(TMPro.TMP_Offset lhs, TMPro.TMP_Offset rhs) { }
        // RVA: 0x0B318E8C  token: 0x6000157
        public static System.Boolean op_Inequality(TMPro.TMP_Offset lhs, TMPro.TMP_Offset rhs) { }
        // RVA: 0x0308B480  token: 0x6000158
        public static TMPro.TMP_Offset op_Multiply(TMPro.TMP_Offset a, System.Single b) { }
        // RVA: 0x0B318DFC  token: 0x6000159
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B318D9C  token: 0x600015A
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0B318D20  token: 0x600015B
        public System.Boolean Equals(TMPro.TMP_Offset other) { }
        // RVA: 0x04D6F380  token: 0x600015C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200002E  // size: 0x24
    public sealed struct HighlightState
    {
        // Fields
        public UnityEngine.Color32 color;  // 0x10
        public TMPro.TMP_Offset padding;  // 0x14

        // Methods
        // RVA: 0x04D9C150  token: 0x600015D
        public System.Void .ctor(UnityEngine.Color32 color, TMPro.TMP_Offset padding) { }
        // RVA: 0x0B30BF8C  token: 0x600015E
        public static System.Boolean op_Equality(TMPro.HighlightState lhs, TMPro.HighlightState rhs) { }
        // RVA: 0x0B30C050  token: 0x600015F
        public static System.Boolean op_Inequality(TMPro.HighlightState lhs, TMPro.HighlightState rhs) { }
        // RVA: 0x0B30BF38  token: 0x6000160
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B30BE4C  token: 0x6000161
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0B30BEB0  token: 0x6000162
        public System.Boolean Equals(TMPro.HighlightState other) { }

    }

    // TypeToken: 0x200002F  // size: 0x188
    public sealed struct TMP_CharacterInfo : TMPro.IPoolClearable
    {
        // Fields
        public System.Char character;  // 0x10
        public System.Int32 index;  // 0x14
        public System.Int32 stringLength;  // 0x18
        public TMPro.TMP_TextElementType elementType;  // 0x1c
        public TMPro.TMP_TextElement textElement;  // 0x20
        public TMPro.TMP_FontAsset fontAsset;  // 0x28
        public TMPro.TMP_SpriteAsset spriteAsset;  // 0x30
        public System.Int32 spriteIndex;  // 0x38
        public UnityEngine.Material material;  // 0x40
        public System.Int32 materialReferenceIndex;  // 0x48
        public System.Boolean isUsingAlternateTypeface;  // 0x4c
        public System.Single pointSize;  // 0x50
        public System.Int32 lineNumber;  // 0x54
        public System.Int32 pageNumber;  // 0x58
        public System.Int32 vertexIndex;  // 0x5c
        public TMPro.TMP_Vertex vertex_BL;  // 0x60
        public TMPro.TMP_Vertex vertex_TL;  // 0x88
        public TMPro.TMP_Vertex vertex_TR;  // 0xb0
        public TMPro.TMP_Vertex vertex_BR;  // 0xd8
        public UnityEngine.Vector3 topLeft;  // 0x100
        public UnityEngine.Vector3 bottomLeft;  // 0x10c
        public UnityEngine.Vector3 topRight;  // 0x118
        public UnityEngine.Vector3 bottomRight;  // 0x124
        public System.Single origin;  // 0x130
        public System.Single xAdvance;  // 0x134
        public System.Single ascender;  // 0x138
        public System.Single baseLine;  // 0x13c
        public System.Single descender;  // 0x140
        public System.Single adjustedAscender;  // 0x144
        public System.Single adjustedDescender;  // 0x148
        public System.Single aspectRatio;  // 0x14c
        public System.Single scale;  // 0x150
        public UnityEngine.Color32 color;  // 0x154
        public UnityEngine.Color32 underlineColor;  // 0x158
        public System.Int32 underlineVertexIndex;  // 0x15c
        public UnityEngine.Color32 strikethroughColor;  // 0x160
        public System.Int32 strikethroughVertexIndex;  // 0x164
        public UnityEngine.Color32 highlightColor;  // 0x168
        public TMPro.HighlightState highlightState;  // 0x16c
        public TMPro.FontStyles style;  // 0x180
        public System.Boolean isVisible;  // 0x184

        // Methods
        // RVA: 0x03518F50  token: 0x6000163
        public virtual System.Void Clear() { }

    }

    // TypeToken: 0x2000030  // size: 0x14
    public sealed struct ColorMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.ColorMode Single;  // const
        public static TMPro.ColorMode HorizontalGradient;  // const
        public static TMPro.ColorMode VerticalGradient;  // const
        public static TMPro.ColorMode FourCornersGradient;  // const

    }

    // TypeToken: 0x2000031  // size: 0x60
    public class TMP_ColorGradient : UnityEngine.ScriptableObject
    {
        // Fields
        public TMPro.ColorMode colorMode;  // 0x18
        public UnityEngine.Color topLeft;  // 0x1c
        public UnityEngine.Color topRight;  // 0x2c
        public UnityEngine.Color bottomLeft;  // 0x3c
        public UnityEngine.Color bottomRight;  // 0x4c
        private static TMPro.ColorMode k_DefaultColorMode;  // const
        private static readonly UnityEngine.Color k_DefaultColor;  // static @ 0x0

        // Methods
        // RVA: 0x0B30CAB4  token: 0x6000164
        public System.Void .ctor() { }
        // RVA: 0x0B30CB60  token: 0x6000165
        public System.Void .ctor(UnityEngine.Color color) { }
        // RVA: 0x0B30CBA4  token: 0x6000166
        public System.Void .ctor(UnityEngine.Color color0, UnityEngine.Color color1, UnityEngine.Color color2, UnityEngine.Color color3) { }
        // RVA: 0x0B30CA70  token: 0x6000167
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000032  // size: 0x10
    public static class TMP_Compatibility
    {
        // Methods
        // RVA: 0x0B30CC14  token: 0x6000168
        public static TMPro.TextAlignmentOptions ConvertTextAlignmentEnumValues(TMPro.TextAlignmentOptions oldValue) { }

    }

    // TypeToken: 0x2000034
    public interface ITweenValue
    {
        // Properties
        System.Boolean ignoreTimeScale { get; /* RVA: -1  // abstract */ }
        System.Single duration { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000169
        public virtual System.Void TweenValue(System.Single floatPercentage) { }
        // RVA: -1  // abstract  token: 0x600016C
        public virtual System.Boolean ValidTarget() { }

    }

    // TypeToken: 0x2000035  // size: 0x48
    public sealed struct ColorTween : TMPro.ITweenValue
    {
        // Fields
        private TMPro.ColorTween.ColorTweenCallback m_Target;  // 0x10
        private UnityEngine.Color m_StartColor;  // 0x18
        private UnityEngine.Color m_TargetColor;  // 0x28
        private TMPro.ColorTween.ColorTweenMode m_TweenMode;  // 0x38
        private System.Single m_Duration;  // 0x3c
        private System.Boolean m_IgnoreTimeScale;  // 0x40

        // Properties
        UnityEngine.Color startColor { get; /* RVA: 0x015AD730 */ set; /* RVA: 0x04DA4E20 */ }
        UnityEngine.Color targetColor { get; /* RVA: 0x04D890C0 */ set; /* RVA: 0x04D890D0 */ }
        TMPro.ColorTween.ColorTweenMode tweenMode { get; /* RVA: 0x011F2A90 */ set; /* RVA: 0x011F2AA0 */ }
        System.Single duration { get; /* RVA: 0x04D86AF0 */ set; /* RVA: 0x04D86B00 */ }
        System.Boolean ignoreTimeScale { get; /* RVA: 0x04D865F0 */ set; /* RVA: 0x04D86620 */ }

        // Methods
        // RVA: 0x0B30B834  token: 0x6000177
        public virtual System.Void TweenValue(System.Single floatPercentage) { }
        // RVA: 0x0B30B79C  token: 0x6000178
        public System.Void AddOnChangedCallback(UnityEngine.Events.UnityAction<UnityEngine.Color> callback) { }
        // RVA: 0x04D865F0  token: 0x6000179
        public System.Boolean GetIgnoreTimescale() { }
        // RVA: 0x04D86AF0  token: 0x600017A
        public System.Single GetDuration() { }
        // RVA: 0x03A7A090  token: 0x600017B
        public virtual System.Boolean ValidTarget() { }

    }

    // TypeToken: 0x2000038  // size: 0x28
    public sealed struct FloatTween : TMPro.ITweenValue
    {
        // Fields
        private TMPro.FloatTween.FloatTweenCallback m_Target;  // 0x10
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
        // RVA: 0x0B30BCDC  token: 0x6000185
        public virtual System.Void TweenValue(System.Single floatPercentage) { }
        // RVA: 0x0B30BC44  token: 0x6000186
        public System.Void AddOnChangedCallback(UnityEngine.Events.UnityAction<System.Single> callback) { }
        // RVA: 0x04D8EE50  token: 0x6000187
        public System.Boolean GetIgnoreTimescale() { }
        // RVA: 0x04D85F70  token: 0x6000188
        public System.Single GetDuration() { }
        // RVA: 0x03A7A090  token: 0x6000189
        public virtual System.Boolean ValidTarget() { }

    }

    // TypeToken: 0x200003A
    public class TweenRunner`1
    {
        // Fields
        protected UnityEngine.MonoBehaviour m_CoroutineContainer;  // 0x0
        protected System.Collections.IEnumerator m_Tween;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600018B
        private static System.Collections.IEnumerator Start(T tweenInfo) { }
        // RVA: -1  // not resolved  token: 0x600018C
        public System.Void Init(UnityEngine.MonoBehaviour coroutineContainer) { }
        // RVA: -1  // not resolved  token: 0x600018D
        public System.Void StartTween(T info) { }
        // RVA: -1  // not resolved  token: 0x600018E
        public System.Void StopTween() { }
        // RVA: -1  // not resolved  token: 0x600018F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003C  // size: 0x10
    public static class TMP_DefaultControls
    {
        // Fields
        private static System.Single kWidth;  // const
        private static System.Single kThickHeight;  // const
        private static System.Single kThinHeight;  // const
        private static UnityEngine.Vector2 s_TextElementSize;  // static @ 0x0
        private static UnityEngine.Vector2 s_ThickElementSize;  // static @ 0x8
        private static UnityEngine.Vector2 s_ThinElementSize;  // static @ 0x10
        private static UnityEngine.Color s_DefaultSelectableColor;  // static @ 0x18
        private static UnityEngine.Color s_TextColor;  // static @ 0x28

        // Methods
        // RVA: 0x0B30E674  token: 0x6000196
        private static UnityEngine.GameObject CreateUIElementRoot(System.String name, UnityEngine.Vector2 size) { }
        // RVA: 0x0B30E714  token: 0x6000197
        private static UnityEngine.GameObject CreateUIObject(System.String name, UnityEngine.GameObject parent) { }
        // RVA: 0x0B30E814  token: 0x6000198
        private static System.Void SetDefaultTextValues(TMPro.TMP_Text lbl) { }
        // RVA: 0x0B30E7C4  token: 0x6000199
        private static System.Void SetDefaultColorTransitionValues(UnityEngine.UI.Selectable slider) { }
        // RVA: 0x0B30E954  token: 0x600019A
        private static System.Void SetParentAndAlign(UnityEngine.GameObject child, UnityEngine.GameObject parent) { }
        // RVA: 0x0B30E894  token: 0x600019B
        private static System.Void SetLayerRecursively(UnityEngine.GameObject go, System.Int32 layer) { }
        // RVA: 0x0B30E2FC  token: 0x600019C
        public static UnityEngine.GameObject CreateScrollbar(TMPro.TMP_DefaultControls.Resources resources) { }
        // RVA: 0x0B30CD30  token: 0x600019D
        public static UnityEngine.GameObject CreateButton(TMPro.TMP_DefaultControls.Resources resources) { }
        // RVA: 0x0B30E5D8  token: 0x600019E
        public static UnityEngine.GameObject CreateText(TMPro.TMP_DefaultControls.Resources resources) { }
        // RVA: 0x0B30DD54  token: 0x600019F
        public static UnityEngine.GameObject CreateInputField(TMPro.TMP_DefaultControls.Resources resources) { }
        // RVA: 0x0B30CFAC  token: 0x60001A0
        public static UnityEngine.GameObject CreateDropdown(TMPro.TMP_DefaultControls.Resources resources) { }
        // RVA: 0x0B30EA20  token: 0x60001A1
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200003E  // size: 0x208
    public class TMP_Dropdown : UnityEngine.UI.Selectable, UnityEngine.EventSystems.IPointerClickHandler, UnityEngine.EventSystems.IEventSystemHandler, UnityEngine.EventSystems.ISubmitHandler, UnityEngine.EventSystems.ICancelHandler
    {
        // Fields
        private UnityEngine.RectTransform m_Template;  // 0x188
        private TMPro.TMP_Text m_CaptionText;  // 0x190
        private UnityEngine.UI.Image m_CaptionImage;  // 0x198
        private UnityEngine.UI.Graphic m_Placeholder;  // 0x1a0
        private TMPro.TMP_Text m_ItemText;  // 0x1a8
        private UnityEngine.UI.Image m_ItemImage;  // 0x1b0
        private System.Int32 m_Value;  // 0x1b8
        private TMPro.TMP_Dropdown.OptionDataList m_Options;  // 0x1c0
        private TMPro.TMP_Dropdown.DropdownEvent m_OnValueChanged;  // 0x1c8
        private System.Single m_AlphaFadeSpeed;  // 0x1d0
        private UnityEngine.GameObject m_Dropdown;  // 0x1d8
        private UnityEngine.GameObject m_Blocker;  // 0x1e0
        private System.Collections.Generic.List<TMPro.TMP_Dropdown.DropdownItem> m_Items;  // 0x1e8
        private TMPro.TweenRunner<TMPro.FloatTween> m_AlphaTweenRunner;  // 0x1f0
        private System.Boolean validTemplate;  // 0x1f8
        private UnityEngine.Coroutine m_Coroutine;  // 0x200
        private static TMPro.TMP_Dropdown.OptionData s_NoOptionData;  // static @ 0x0

        // Properties
        UnityEngine.RectTransform template { get; /* RVA: 0x04D8DA20 */ set; /* RVA: 0x0B311974 */ }
        TMPro.TMP_Text captionText { get; /* RVA: 0x04D8DD50 */ set; /* RVA: 0x0B3118A0 */ }
        UnityEngine.UI.Image captionImage { get; /* RVA: 0x04D8DED0 */ set; /* RVA: 0x0B311878 */ }
        UnityEngine.UI.Graphic placeholder { get; /* RVA: 0x04D8DA10 */ set; /* RVA: 0x0B31194C */ }
        TMPro.TMP_Text itemText { get; /* RVA: 0x04D8D9C0 */ set; /* RVA: 0x0B3118F0 */ }
        UnityEngine.UI.Image itemImage { get; /* RVA: 0x04D8D960 */ set; /* RVA: 0x0B3118C8 */ }
        System.Collections.Generic.List<TMPro.TMP_Dropdown.OptionData> options { get; /* RVA: 0x0B311858 */ set; /* RVA: 0x0B311918 */ }
        TMPro.TMP_Dropdown.DropdownEvent onValueChanged { get; /* RVA: 0x04D8D790 */ set; /* RVA: 0x06B8B0D0 */ }
        System.Single alphaFadeSpeed { get; /* RVA: 0x04DB9B50 */ set; /* RVA: 0x04DB9B60 */ }
        System.Int32 value { get; /* RVA: 0x04D8EAC0 */ set; /* RVA: 0x0B31199C */ }
        System.Boolean IsExpanded { get; /* RVA: 0x0B311804 */ }

        // Methods
        // RVA: 0x0B310030  token: 0x60001B6
        public System.Void SetValueWithoutNotify(System.Int32 input) { }
        // RVA: 0x0B31003C  token: 0x60001B7
        private System.Void SetValue(System.Int32 value, System.Boolean sendCallback) { }
        // RVA: 0x0B3116DC  token: 0x60001B9
        protected System.Void .ctor() { }
        // RVA: 0x0B30F1A4  token: 0x60001BA
        protected virtual System.Void Awake() { }
        // RVA: 0x0B3115DC  token: 0x60001BB
        protected virtual System.Void Start() { }
        // RVA: 0x0B30FB88  token: 0x60001BC
        protected virtual System.Void OnDisable() { }
        // RVA: 0x0B30FD2C  token: 0x60001BD
        public System.Void RefreshShownValue() { }
        // RVA: 0x0B30ED54  token: 0x60001BE
        public System.Void AddOptions(System.Collections.Generic.List<TMPro.TMP_Dropdown.OptionData> options) { }
        // RVA: 0x0B30EDBC  token: 0x60001BF
        public System.Void AddOptions(System.Collections.Generic.List<System.String> options) { }
        // RVA: 0x0B30EEAC  token: 0x60001C0
        public System.Void AddOptions(System.Collections.Generic.List<UnityEngine.Sprite> options) { }
        // RVA: 0x0B30F28C  token: 0x60001C1
        public System.Void ClearOptions() { }
        // RVA: 0x0B310174  token: 0x60001C2
        private System.Void SetupTemplate() { }
        // RVA: -1  // generic def  token: 0x60001C3
        private static T GetOrAddComponent(UnityEngine.GameObject go) { }
        // RVA: 0x0B30FC1C  token: 0x60001C4
        public virtual System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B30FC1C  token: 0x60001C5
        public virtual System.Void OnSubmit(UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0B30FB80  token: 0x60001C6
        public virtual System.Void OnCancel(UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0B310778  token: 0x60001C7
        public System.Void Show() { }
        // RVA: 0x0B30F324  token: 0x60001C8
        protected virtual UnityEngine.GameObject CreateBlocker(UnityEngine.Canvas rootCanvas) { }
        // RVA: 0x0B30F86C  token: 0x60001C9
        protected virtual System.Void DestroyBlocker(UnityEngine.GameObject blocker) { }
        // RVA: 0x0B30F758  token: 0x60001CA
        protected virtual UnityEngine.GameObject CreateDropdownList(UnityEngine.GameObject template) { }
        // RVA: 0x0B30F8B8  token: 0x60001CB
        protected virtual System.Void DestroyDropdownList(UnityEngine.GameObject dropdownList) { }
        // RVA: 0x0B30F7AC  token: 0x60001CC
        protected virtual TMPro.TMP_Dropdown.DropdownItem CreateItem(TMPro.TMP_Dropdown.DropdownItem itemTemplate) { }
        // RVA: 0x041E1670  token: 0x60001CD
        protected virtual System.Void DestroyItem(TMPro.TMP_Dropdown.DropdownItem item) { }
        // RVA: 0x0B30EAC8  token: 0x60001CE
        private TMPro.TMP_Dropdown.DropdownItem AddItem(TMPro.TMP_Dropdown.OptionData data, System.Boolean selected, TMPro.TMP_Dropdown.DropdownItem itemTemplate, System.Collections.Generic.List<TMPro.TMP_Dropdown.DropdownItem> items) { }
        // RVA: 0x0B30F118  token: 0x60001CF
        private System.Void AlphaFadeList(System.Single duration, System.Single alpha) { }
        // RVA: 0x0B30EF9C  token: 0x60001D0
        private System.Void AlphaFadeList(System.Single duration, System.Single start, System.Single end) { }
        // RVA: 0x0B30FF90  token: 0x60001D1
        private System.Void SetAlpha(System.Single alpha) { }
        // RVA: 0x0B30F904  token: 0x60001D2
        public System.Void Hide() { }
        // RVA: 0x0B30F800  token: 0x60001D3
        private System.Collections.IEnumerator DelayedDestroyDropdownList(System.Single delay) { }
        // RVA: 0x0B30FA1C  token: 0x60001D4
        private System.Void ImmediateDestroyDropdownList() { }
        // RVA: 0x0B30FC24  token: 0x60001D5
        private System.Void OnSelectItem(UnityEngine.UI.Toggle toggle) { }
        // RVA: 0x0B31166C  token: 0x60001D6
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000045  // size: 0x14
    public sealed struct AtlasPopulationMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.AtlasPopulationMode Static;  // const
        public static TMPro.AtlasPopulationMode Dynamic;  // const

    }

    // TypeToken: 0x2000046  // size: 0x230
    public class TMP_FontAsset : TMPro.TMP_Asset
    {
        // Fields
        private System.String m_Version;  // 0x30
        private System.String m_SourceFontFileGUID;  // 0x38
        private UnityEngine.Font m_SourceFontFile;  // 0x40
        private System.Single m_ThaiBiasConsonantsValue;  // 0x48
        private System.String m_prefixMaterialSearchPatternValue;  // 0x50
        private TMPro.AtlasPopulationMode m_AtlasPopulationMode;  // 0x58
        public UnityEngine.TextCore.FaceInfo m_FaceInfo;  // 0x60
        private System.Collections.Generic.List<UnityEngine.TextCore.Glyph> m_GlyphTable;  // 0xc0
        private System.Collections.Generic.Dictionary<System.UInt32,UnityEngine.TextCore.Glyph> m_GlyphLookupDictionary;  // 0xc8
        private System.Collections.Generic.List<TMPro.TMP_Character> m_CharacterTable;  // 0xd0
        public System.Collections.Generic.Dictionary<System.UInt32,TMPro.TMP_Character> m_CharacterLookupDictionary;  // 0xd8
        private UnityEngine.Texture2D m_AtlasTexture;  // 0xe0
        private UnityEngine.Texture2D[] m_AtlasTextures;  // 0xe8
        private System.Int32 m_AtlasTextureIndex;  // 0xf0
        private System.Boolean m_IsMultiAtlasTexturesEnabled;  // 0xf4
        private System.Boolean m_ClearDynamicDataOnBuild;  // 0xf5
        private System.Collections.Generic.List<UnityEngine.TextCore.GlyphRect> m_UsedGlyphRects;  // 0xf8
        private System.Collections.Generic.List<UnityEngine.TextCore.GlyphRect> m_FreeGlyphRects;  // 0x100
        private TMPro.FaceInfo_Legacy m_fontInfo;  // 0x108
        public UnityEngine.Texture2D atlas;  // 0x110
        private System.Int32 m_AtlasWidth;  // 0x118
        private System.Int32 m_AtlasHeight;  // 0x11c
        private System.Int32 m_AtlasPadding;  // 0x120
        private UnityEngine.TextCore.LowLevel.GlyphRenderMode m_AtlasRenderMode;  // 0x124
        private System.Collections.Generic.List<TMPro.TMP_Glyph> m_glyphInfoList;  // 0x128
        private TMPro.KerningTable m_KerningTable;  // 0x130
        public TMPro.TMP_FontFeatureTable m_FontFeatureTable;  // 0x138
        private System.Collections.Generic.List<TMPro.TMP_FontAsset> fallbackFontAssets;  // 0x140
        private System.Collections.Generic.List<TMPro.TMP_FontAsset> m_FallbackFontAssetTable;  // 0x148
        private TMPro.FontAssetCreationSettings m_CreationSettings;  // 0x150
        private TMPro.TMP_FontWeightPair[] m_FontWeightTable;  // 0x1a8
        private TMPro.TMP_FontWeightPair[] fontWeights;  // 0x1b0
        public System.Single normalStyle;  // 0x1b8
        public System.Single normalSpacingOffset;  // 0x1bc
        public System.Single boldStyle;  // 0x1c0
        public System.Single boldSpacing;  // 0x1c4
        public System.Byte italicStyle;  // 0x1c8
        public System.Byte tabSize;  // 0x1c9
        public System.Boolean isUsedBySoftLink;  // 0x1ca
        private System.Boolean IsFontAssetLookupTablesDirty;  // 0x1cb
        private static Unity.Profiling.ProfilerMarker k_ReadFontAssetDefinitionMarker;  // static @ 0x0
        private static Unity.Profiling.ProfilerMarker k_AddSynthesizedCharactersMarker;  // static @ 0x8
        private static Unity.Profiling.ProfilerMarker<System.UInt32> k_TryAddCharacterMarker;  // static @ 0x10
        private static Unity.Profiling.ProfilerMarker k_TryAddCharactersMarker;  // static @ 0x18
        private static Unity.Profiling.ProfilerMarker k_UpdateGlyphAdjustmentRecordsMarker;  // static @ 0x20
        private static Unity.Profiling.ProfilerMarker k_ClearFontAssetDataMarker;  // static @ 0x28
        private static Unity.Profiling.ProfilerMarker k_UpdateFontAssetDataMarker;  // static @ 0x30
        private static System.String s_DefaultMaterialSuffix;  // static @ 0x38
        private System.Collections.Generic.HashSet<System.Int32> FallbackSearchQueryLookup;  // 0x1d0
        private static System.Collections.Generic.HashSet<System.Int32> k_SearchedFontAssetLookup;  // static @ 0x40
        private static System.Collections.Generic.List<TMPro.TMP_FontAsset> k_FontAssets_FontFeaturesUpdateQueue;  // static @ 0x48
        private static System.Collections.Generic.HashSet<System.Int32> k_FontAssets_FontFeaturesUpdateQueueLookup;  // static @ 0x50
        private static System.Collections.Generic.List<TMPro.TMP_FontAsset> k_FontAssets_AtlasTexturesUpdateQueue;  // static @ 0x58
        private static System.Collections.Generic.HashSet<System.Int32> k_FontAssets_AtlasTexturesUpdateQueueLookup;  // static @ 0x60
        private System.Collections.Generic.List<UnityEngine.TextCore.Glyph> m_GlyphsToRender;  // 0x1d8
        private System.Collections.Generic.List<UnityEngine.TextCore.Glyph> m_GlyphsRendered;  // 0x1e0
        private System.Collections.Generic.List<System.UInt32> m_GlyphIndexList;  // 0x1e8
        private System.Collections.Generic.List<System.UInt32> m_GlyphIndexListNewlyAdded;  // 0x1f0
        private System.Collections.Generic.List<System.UInt32> m_GlyphsToAdd;  // 0x1f8
        private System.Collections.Generic.HashSet<System.UInt32> m_GlyphsToAddLookup;  // 0x200
        private System.Collections.Generic.List<TMPro.TMP_Character> m_CharactersToAdd;  // 0x208
        private System.Collections.Generic.HashSet<System.UInt32> m_CharactersToAddLookup;  // 0x210
        private System.Collections.Generic.List<System.UInt32> s_MissingCharacterList;  // 0x218
        private System.Collections.Generic.HashSet<System.UInt32> m_MissingUnicodesFromFontFile;  // 0x220
        private static System.UInt32[] k_GlyphIndexArray;  // static @ 0x68
        private UnityEngine.RenderTexture[] m_DynamicRenderTextures;  // 0x228

        // Properties
        System.String version { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }
        UnityEngine.Font sourceFontFile { get; /* RVA: 0x04D85A60 */ set; /* RVA: 0x042B4AE0 */ }
        System.Single thaiBiasConsonantsValue { get; /* RVA: 0x04D8D350 */ set; /* RVA: 0x04D8D380 */ }
        System.String prefixMaterialSearchPatternValue { get; /* RVA: 0x04D86270 */ set; /* RVA: 0x02FDAEA0 */ }
        TMPro.AtlasPopulationMode atlasPopulationMode { get; /* RVA: 0x04D86570 */ set; /* RVA: 0x04D865D0 */ }
        UnityEngine.TextCore.FaceInfo faceInfo { get; /* RVA: 0x04DB9BE0 */ set; /* RVA: 0x0B318564 */ }
        System.Collections.Generic.List<UnityEngine.TextCore.Glyph> glyphTable { get; /* RVA: 0x04D892F0 */ set; /* RVA: 0x06AC5ACC */ }
        System.Collections.Generic.Dictionary<System.UInt32,UnityEngine.TextCore.Glyph> glyphLookupTable { get; /* RVA: 0x0B3184E4 */ }
        System.Collections.Generic.List<TMPro.TMP_Character> characterTable { get; /* RVA: 0x04D876B0 */ set; /* RVA: 0x0678D7BC */ }
        System.Collections.Generic.Dictionary<System.UInt32,TMPro.TMP_Character> characterLookupTable { get; /* RVA: 0x0333EEE0 */ }
        UnityEngine.Texture2D atlasTexture { get; /* RVA: 0x0351ECB0 */ }
        UnityEngine.Texture2D[] atlasTextures { get; /* RVA: 0x04D88590 */ set; /* RVA: 0x06AC5B1C */ }
        System.Int32 atlasTextureCount { get; /* RVA: 0x04DB9B80 */ }
        System.Boolean isMultiAtlasTexturesEnabled { get; /* RVA: 0x04DA7520 */ set; /* RVA: 0x04DA7530 */ }
        System.Boolean clearDynamicDataOnBuild { get; /* RVA: 0x04DA7540 */ set; /* RVA: 0x04DA7550 */ }
        System.Collections.Generic.List<UnityEngine.TextCore.GlyphRect> usedGlyphRects { get; /* RVA: 0x04D877C0 */ set; /* RVA: 0x06790298 */ }
        System.Collections.Generic.List<UnityEngine.TextCore.GlyphRect> freeGlyphRects { get; /* RVA: 0x04D87D50 */ set; /* RVA: 0x067A5EA0 */ }
        TMPro.FaceInfo_Legacy fontInfo { get; /* RVA: 0x04D85F90 */ }
        System.Int32 atlasWidth { get; /* RVA: 0x04D87FF0 */ set; /* RVA: 0x04D88000 */ }
        System.Int32 atlasHeight { get; /* RVA: 0x04DB9B70 */ set; /* RVA: 0x04DB9C20 */ }
        System.Int32 atlasPadding { get; /* RVA: 0x04D90A90 */ set; /* RVA: 0x04D90AC0 */ }
        UnityEngine.TextCore.LowLevel.GlyphRenderMode atlasRenderMode { get; /* RVA: 0x04D90A80 */ set; /* RVA: 0x04D90AB0 */ }
        TMPro.TMP_FontFeatureTable fontFeatureTable { get; /* RVA: 0x04D8DC90 */ set; /* RVA: 0x05397184 */ }
        System.Collections.Generic.List<TMPro.TMP_FontAsset> fallbackFontAssetTable { get; /* RVA: 0x04D8DAC0 */ set; /* RVA: 0x06B8B468 */ }
        TMPro.FontAssetCreationSettings creationSettings { get; /* RVA: 0x04DB9B90 */ set; /* RVA: 0x0B318514 */ }
        TMPro.TMP_FontWeightPair[] fontWeightTable { get; /* RVA: 0x04D8D9C0 */ set; /* RVA: 0x053971F0 */ }
        UnityEngine.RenderTexture[] dynamicRenderTextures { get; /* RVA: 0x04D8D440 */ }

        // Methods
        // RVA: 0x0B312AC8  token: 0x6000225
        public static TMPro.TMP_FontAsset CreateFontAsset(UnityEngine.Font font) { }
        // RVA: 0x0B312B38  token: 0x6000226
        public static TMPro.TMP_FontAsset CreateFontAsset(UnityEngine.Font font, System.Int32 samplingPointSize, System.Int32 atlasPadding, UnityEngine.TextCore.LowLevel.GlyphRenderMode renderMode, System.Int32 atlasWidth, System.Int32 atlasHeight, TMPro.AtlasPopulationMode atlasPopulationMode, System.Boolean enableMultiAtlasSupport) { }
        // RVA: 0x04A1E700  token: 0x6000227
        private System.Void Awake() { }
        // RVA: 0x0393FB50  token: 0x6000228
        public System.Void ReadFontAssetDefinition() { }
        // RVA: 0x0393FB20  token: 0x6000229
        private System.Void InitializeDictionaryLookupTables() { }
        // RVA: 0x0338C0B0  token: 0x600022A
        private System.Void InitializeGlyphLookupDictionary() { }
        // RVA: 0x0393F800  token: 0x600022B
        private System.Void InitializeCharacterLookupDictionary() { }
        // RVA: 0x0393FA00  token: 0x600022C
        private System.Void InitializeGlyphPaidAdjustmentRecordsLookupDictionary() { }
        // RVA: 0x0393F3D0  token: 0x600022D
        private System.Void AddSynthesizedCharactersAndFaceMetrics() { }
        // RVA: 0x0393F5F0  token: 0x600022E
        private System.Void AddSynthesizedCharacter(System.UInt32 unicode, System.Boolean isFontFaceLoaded, System.Boolean addImmediately) { }
        // RVA: 0x0B3120F4  token: 0x600022F
        private System.Void AddCharacterToLookupCache(System.UInt32 unicode, TMPro.TMP_Character character) { }
        // RVA: 0x0B3147CC  token: 0x6000230
        private System.Void SortCharacterTable() { }
        // RVA: 0x0B31495C  token: 0x6000231
        private System.Void SortGlyphTable() { }
        // RVA: 0x0B314938  token: 0x6000232
        private System.Void SortFontFeatureTable() { }
        // RVA: 0x0B3147A0  token: 0x6000233
        private System.Void SortAllTables() { }
        // RVA: 0x0B313530  token: 0x6000234
        public System.Boolean HasCharacter(System.Int32 character) { }
        // RVA: 0x0B31358C  token: 0x6000235
        public System.Boolean HasCharacter(System.Char character, System.Boolean searchFallbacks, System.Boolean tryAddCharacter) { }
        // RVA: 0x0B313318  token: 0x6000236
        private System.Boolean HasCharacter_Internal(System.UInt32 character, System.Boolean searchFallbacks, System.Boolean tryAddCharacter) { }
        // RVA: 0x0B313A6C  token: 0x6000237
        public System.Boolean HasCharacters(System.String text, System.Collections.Generic.List<System.Char>& missingCharacters) { }
        // RVA: 0x0B313BC0  token: 0x6000238
        public System.Boolean HasCharacters(System.String text, System.UInt32[]& missingCharacters, System.Boolean searchFallbacks, System.Boolean tryAddCharacter) { }
        // RVA: 0x0B31418C  token: 0x6000239
        public System.Boolean HasCharacters(System.String text) { }
        // RVA: 0x0B313154  token: 0x600023A
        public static System.String GetCharacters(TMPro.TMP_FontAsset fontAsset) { }
        // RVA: 0x0B313088  token: 0x600023B
        public static System.Int32[] GetCharactersArray(TMPro.TMP_FontAsset fontAsset) { }
        // RVA: 0x0B313234  token: 0x600023C
        private System.UInt32 GetGlyphIndex(System.UInt32 unicode) { }
        // RVA: 0x0B314348  token: 0x600023D
        private static System.Void RegisterFontAssetForFontFeatureUpdate(TMPro.TMP_FontAsset fontAsset) { }
        // RVA: 0x03CF2860  token: 0x600023E
        private static System.Void UpdateFontFeaturesForFontAssetsInQueue() { }
        // RVA: 0x0B31427C  token: 0x600023F
        private static System.Void RegisterFontAssetForAtlasTextureUpdate(TMPro.TMP_FontAsset fontAsset) { }
        // RVA: 0x0B31708C  token: 0x6000240
        private static System.Void UpdateAtlasTexturesForFontAssetsInQueue() { }
        // RVA: 0x0B31675C  token: 0x6000241
        public System.Boolean TryAddCharacters(System.UInt32[] unicodes, System.Boolean includeFontFeatures) { }
        // RVA: 0x0B315330  token: 0x6000242
        public System.Boolean TryAddCharacters(System.UInt32[] unicodes, System.UInt32[]& missingUnicodes, System.Boolean includeFontFeatures) { }
        // RVA: 0x0B315D48  token: 0x6000243
        public System.Boolean TryAddCharacters(System.String characters, System.Boolean includeFontFeatures) { }
        // RVA: 0x0B315D6C  token: 0x6000244
        public System.Boolean TryAddCharacters(System.String characters, System.String& missingCharacters, System.Boolean includeFontFeatures) { }
        // RVA: 0x0B312198  token: 0x6000245
        public System.Void AddCharacters(System.String characters) { }
        // RVA: 0x0B314AC8  token: 0x6000246
        private System.Boolean TryAddCharacterInternal(System.UInt32 unicode, TMPro.TMP_Character& character) { }
        // RVA: 0x0B316AF8  token: 0x6000247
        private System.Boolean TryGetCharacter_and_QueueRenderToTexture(System.UInt32 unicode, TMPro.TMP_Character& character) { }
        // RVA: 0x041E1670  token: 0x6000248
        private System.Void TryAddGlyphsToAtlasTextures() { }
        // RVA: 0x0B316780  token: 0x6000249
        private System.Boolean TryAddGlyphsToNewAtlasTexture() { }
        // RVA: 0x0B314414  token: 0x600024A
        private System.Void SetupNewAtlasTexture() { }
        // RVA: 0x0B316F08  token: 0x600024B
        private System.Void UpdateAtlasTexture() { }
        // RVA: 0x0B3172B8  token: 0x600024C
        private System.Void UpdateGlyphAdjustmentRecords() { }
        // RVA: 0x0B317518  token: 0x600024D
        private System.Void UpdateGlyphAdjustmentRecords(System.UInt32[] glyphIndexes) { }
        // RVA: 0x041E1670  token: 0x600024E
        private System.Void UpdateGlyphAdjustmentRecords(System.Collections.Generic.List<System.UInt32> glyphIndexes) { }
        // RVA: 0x041E1670  token: 0x600024F
        private System.Void UpdateGlyphAdjustmentRecords(System.Collections.Generic.List<System.UInt32> newGlyphIndexes, System.Collections.Generic.List<System.UInt32> allGlyphIndexes) { }
        // RVA: -1  // generic def  token: 0x6000250
        private System.Void CopyListDataToArray(System.Collections.Generic.List<T> srcList, T[]& dstArray) { }
        // RVA: 0x0B312904  token: 0x6000251
        public System.Void ClearFontAssetData(System.Boolean setAtlasSizeToZero) { }
        // RVA: 0x0B3128FC  token: 0x6000252
        public System.Void ClearFontAssetData() { }
        // RVA: 0x0B3128D8  token: 0x6000253
        private System.Void ClearFontAssetDataInternal() { }
        // RVA: 0x0B3171B0  token: 0x6000254
        private System.Void UpdateFontAssetData() { }
        // RVA: 0x0B312940  token: 0x6000255
        private System.Void ClearFontAssetTables() { }
        // RVA: 0x0B312248  token: 0x6000256
        private System.Void ClearAtlasTextures(System.Boolean setAtlasSizeToZero) { }
        // RVA: 0x0B317744  token: 0x6000257
        private System.Void UpgradeFontAsset() { }
        // RVA: 0x0B31814C  token: 0x6000258
        private System.Void UpgradeGlyphAdjustmentTableToFontFeatureTable() { }
        // RVA: 0x0B31422C  token: 0x600025A
        private System.Void OnDestroy() { }
        // RVA: 0x0450C370  token: 0x600025B
        public System.Void .ctor() { }
        // RVA: 0x04880740  token: 0x600025C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000048  // size: 0x68
    public class FaceInfo_Legacy
    {
        // Fields
        public System.String Name;  // 0x10
        public System.Single PointSize;  // 0x18
        public System.Single Scale;  // 0x1c
        public System.Int32 CharacterCount;  // 0x20
        public System.Single LineHeight;  // 0x24
        public System.Single Baseline;  // 0x28
        public System.Single Ascender;  // 0x2c
        public System.Single CapHeight;  // 0x30
        public System.Single Descender;  // 0x34
        public System.Single CenterLine;  // 0x38
        public System.Single SuperscriptOffset;  // 0x3c
        public System.Single SubscriptOffset;  // 0x40
        public System.Single SubSize;  // 0x44
        public System.Single Underline;  // 0x48
        public System.Single UnderlineThickness;  // 0x4c
        public System.Single strikethrough;  // 0x50
        public System.Single strikethroughThickness;  // 0x54
        public System.Single TabWidth;  // 0x58
        public System.Single Padding;  // 0x5c
        public System.Single AtlasWidth;  // 0x60
        public System.Single AtlasHeight;  // 0x64

        // Methods
        // RVA: 0x041E1670  token: 0x6000261
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000049  // size: 0x38
    public class TMP_Glyph : TMPro.TMP_TextElement_Legacy
    {
        // Methods
        // RVA: 0x0B3189B8  token: 0x6000262
        public static TMPro.TMP_Glyph Clone(TMPro.TMP_Glyph source) { }
        // RVA: 0x041E1670  token: 0x6000263
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004A  // size: 0x68
    public sealed struct FontAssetCreationSettings
    {
        // Fields
        public System.String sourceFontFileName;  // 0x10
        public System.String sourceFontFileGUID;  // 0x18
        public System.Int32 pointSizeSamplingMode;  // 0x20
        public System.Int32 pointSize;  // 0x24
        public System.Int32 padding;  // 0x28
        public System.Int32 packingMode;  // 0x2c
        public System.Int32 atlasWidth;  // 0x30
        public System.Int32 atlasHeight;  // 0x34
        public System.Int32 characterSetSelectionMode;  // 0x38
        public System.String characterSequence;  // 0x40
        public System.String referencedFontAssetGUID;  // 0x48
        public System.String referencedTextAssetGUID;  // 0x50
        public System.Int32 fontStyle;  // 0x58
        public System.Single fontStyleModifier;  // 0x5c
        public System.Int32 renderMode;  // 0x60
        public System.Boolean includeFontFeatures;  // 0x64

        // Methods
        // RVA: 0x0B30BD40  token: 0x6000264
        private System.Void .ctor(System.String sourceFontFileGUID, System.Int32 pointSize, System.Int32 pointSizeSamplingMode, System.Int32 padding, System.Int32 packingMode, System.Int32 atlasWidth, System.Int32 atlasHeight, System.Int32 characterSelectionMode, System.String characterSet, System.Int32 renderMode) { }

    }

    // TypeToken: 0x200004B  // size: 0x20
    public sealed struct TMP_FontWeightPair
    {
        // Fields
        public TMPro.TMP_FontAsset regularTypeface;  // 0x10
        public TMPro.TMP_FontAsset italicTypeface;  // 0x18

    }

    // TypeToken: 0x200004C  // size: 0x1C
    public sealed struct KerningPairKey
    {
        // Fields
        public System.UInt32 ascii_Left;  // 0x10
        public System.UInt32 ascii_Right;  // 0x14
        public System.UInt32 key;  // 0x18

        // Methods
        // RVA: 0x04DB9AC0  token: 0x6000265
        public System.Void .ctor(System.UInt32 ascii_left, System.UInt32 ascii_right) { }

    }

    // TypeToken: 0x200004D  // size: 0x20
    public sealed struct GlyphValueRecord_Legacy
    {
        // Fields
        public System.Single xPlacement;  // 0x10
        public System.Single yPlacement;  // 0x14
        public System.Single xAdvance;  // 0x18
        public System.Single yAdvance;  // 0x1c

        // Methods
        // RVA: 0x04DB9AA0  token: 0x6000266
        private System.Void .ctor(UnityEngine.TextCore.LowLevel.GlyphValueRecord valueRecord) { }
        // RVA: 0x030E87A0  token: 0x6000267
        public static TMPro.GlyphValueRecord_Legacy op_Addition(TMPro.GlyphValueRecord_Legacy a, TMPro.GlyphValueRecord_Legacy b) { }

    }

    // TypeToken: 0x200004E  // size: 0x40
    public class KerningPair
    {
        // Fields
        private System.UInt32 m_FirstGlyph;  // 0x10
        private TMPro.GlyphValueRecord_Legacy m_FirstGlyphAdjustments;  // 0x14
        private System.UInt32 m_SecondGlyph;  // 0x24
        private TMPro.GlyphValueRecord_Legacy m_SecondGlyphAdjustments;  // 0x28
        public System.Single xOffset;  // 0x38
        private static TMPro.KerningPair empty;  // static @ 0x0
        private System.Boolean m_IgnoreSpacingAdjustments;  // 0x3c

        // Properties
        System.UInt32 firstGlyph { get; /* RVA: 0x02B2E2D0 */ set; /* RVA: 0x014F51F0 */ }
        TMPro.GlyphValueRecord_Legacy firstGlyphAdjustments { get; /* RVA: 0x04D95480 */ }
        System.UInt32 secondGlyph { get; /* RVA: 0x011F0020 */ set; /* RVA: 0x011F0030 */ }
        TMPro.GlyphValueRecord_Legacy secondGlyphAdjustments { get; /* RVA: 0x04D8FAD0 */ }
        System.Boolean ignoreSpacingAdjustments { get; /* RVA: 0x04D86BD0 */ }

        // Methods
        // RVA: 0x04DB9B20  token: 0x600026F
        public System.Void .ctor() { }
        // RVA: 0x04DB9AF0  token: 0x6000270
        public System.Void .ctor(System.UInt32 left, System.UInt32 right, System.Single offset) { }
        // RVA: 0x04DB9B00  token: 0x6000271
        public System.Void .ctor(System.UInt32 firstGlyph, TMPro.GlyphValueRecord_Legacy firstGlyphAdjustments, System.UInt32 secondGlyph, TMPro.GlyphValueRecord_Legacy secondGlyphAdjustments) { }
        // RVA: 0x04DB9AE0  token: 0x6000272
        private System.Void ConvertLegacyKerningData() { }
        // RVA: 0x04D22C20  token: 0x6000273
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200004F  // size: 0x18
    public class KerningTable
    {
        // Fields
        public System.Collections.Generic.List<TMPro.KerningPair> kerningPairs;  // 0x10

        // Methods
        // RVA: 0x0450C7F0  token: 0x6000274
        public System.Void .ctor() { }
        // RVA: 0x0B30C33C  token: 0x6000275
        public System.Void AddKerningPair() { }
        // RVA: 0x0B30C1F0  token: 0x6000276
        public System.Int32 AddKerningPair(System.UInt32 first, System.UInt32 second, System.Single offset) { }
        // RVA: 0x0B30C08C  token: 0x6000277
        public System.Int32 AddGlyphPairAdjustmentRecord(System.UInt32 first, TMPro.GlyphValueRecord_Legacy firstAdjustments, System.UInt32 second, TMPro.GlyphValueRecord_Legacy secondAdjustments) { }
        // RVA: 0x0B30C474  token: 0x6000278
        public System.Void RemoveKerningPair(System.Int32 left, System.Int32 right) { }
        // RVA: 0x0B30C574  token: 0x6000279
        public System.Void RemoveKerningPair(System.Int32 index) { }
        // RVA: 0x0B30C5C8  token: 0x600027A
        public System.Void SortKerningPairs() { }

    }

    // TypeToken: 0x2000054  // size: 0x10
    public static class TMP_FontUtilities
    {
        // Fields
        private static System.Collections.Generic.List<System.Int32> k_searchedFontAssets;  // static @ 0x0

        // Methods
        // RVA: 0x0B3188B8  token: 0x6000285
        public static TMPro.TMP_FontAsset SearchForCharacter(TMPro.TMP_FontAsset font, System.UInt32 unicode, TMPro.TMP_Character& character) { }
        // RVA: 0x0B3188B0  token: 0x6000286
        public static TMPro.TMP_FontAsset SearchForCharacter(System.Collections.Generic.List<TMPro.TMP_FontAsset> fonts, System.UInt32 unicode, TMPro.TMP_Character& character) { }
        // RVA: 0x0B3185A8  token: 0x6000287
        private static TMPro.TMP_FontAsset SearchForCharacterInternal(TMPro.TMP_FontAsset font, System.UInt32 unicode, TMPro.TMP_Character& character) { }
        // RVA: 0x0B3187D4  token: 0x6000288
        private static TMPro.TMP_FontAsset SearchForCharacterInternal(System.Collections.Generic.List<TMPro.TMP_FontAsset> fonts, System.UInt32 unicode, TMPro.TMP_Character& character) { }

    }

    // TypeToken: 0x2000055  // size: 0x10
    public class TMP_FontAssetUtilities
    {
        // Fields
        private static readonly TMPro.TMP_FontAssetUtilities s_Instance;  // static @ 0x0
        private static System.Collections.Generic.HashSet<System.Int32> k_SearchedAssets;  // static @ 0x8
        private static System.Boolean k_IsFontEngineInitialized;  // static @ 0x10

        // Properties
        TMPro.TMP_FontAssetUtilities instance { get; /* RVA: 0x0B3120A4 */ }

        // Methods
        // RVA: 0x04D4DA10  token: 0x6000289
        private static System.Void .cctor() { }
        // RVA: 0x0333E700  token: 0x600028B
        public static TMPro.TMP_Character GetCharacterFromFontAsset(System.UInt32 unicode, TMPro.TMP_FontAsset sourceFontAsset, System.Boolean includeFallbacks, TMPro.FontStyles fontStyle, TMPro.FontWeight fontWeight, System.Boolean& isAlternativeTypeface) { }
        // RVA: 0x0333E780  token: 0x600028C
        private static TMPro.TMP_Character GetCharacterFromFontAsset_Internal(System.UInt32 unicode, TMPro.TMP_FontAsset sourceFontAsset, System.Boolean includeFallbacks, TMPro.FontStyles fontStyle, TMPro.FontWeight fontWeight, System.Boolean& isAlternativeTypeface) { }
        // RVA: 0x0B3119A8  token: 0x600028D
        public static TMPro.TMP_Character GetCharacterFromFontAssets(System.UInt32 unicode, TMPro.TMP_FontAsset sourceFontAsset, System.Collections.Generic.List<TMPro.TMP_FontAsset> fontAssets, System.Boolean includeFallbacks, TMPro.FontStyles fontStyle, TMPro.FontWeight fontWeight, System.Boolean& isAlternativeTypeface) { }
        // RVA: 0x0B311DB8  token: 0x600028E
        public static TMPro.TMP_SpriteCharacter GetSpriteCharacterFromSpriteAsset(System.UInt32 unicode, TMPro.TMP_SpriteAsset spriteAsset, System.Boolean includeFallbacks) { }
        // RVA: 0x0B311C00  token: 0x600028F
        private static TMPro.TMP_SpriteCharacter GetSpriteCharacterFromSpriteAsset_Internal(System.UInt32 unicode, TMPro.TMP_SpriteAsset spriteAsset, System.Boolean includeFallbacks) { }
        // RVA: 0x041E1670  token: 0x6000290
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000056  // size: 0x14
    public sealed struct FontFeatureLookupFlags
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.FontFeatureLookupFlags None;  // const
        public static TMPro.FontFeatureLookupFlags IgnoreLigatures;  // const
        public static TMPro.FontFeatureLookupFlags IgnoreSpacingAdjustments;  // const

    }

    // TypeToken: 0x2000057  // size: 0x20
    public sealed struct TMP_GlyphValueRecord
    {
        // Fields
        private System.Single m_XPlacement;  // 0x10
        private System.Single m_YPlacement;  // 0x14
        private System.Single m_XAdvance;  // 0x18
        private System.Single m_YAdvance;  // 0x1c

        // Properties
        System.Single xPlacement { get; /* RVA: 0x04D871D0 */ set; /* RVA: 0x04D932C0 */ }
        System.Single yPlacement { get; /* RVA: 0x04D9C560 */ set; /* RVA: 0x04DAB920 */ }
        System.Single xAdvance { get; /* RVA: 0x04D9C280 */ set; /* RVA: 0x04DAC1D0 */ }
        System.Single yAdvance { get; /* RVA: 0x04D9CC80 */ set; /* RVA: 0x04DACD70 */ }

        // Methods
        // RVA: 0x04D88AD0  token: 0x6000299
        public System.Void .ctor(System.Single xPlacement, System.Single yPlacement, System.Single xAdvance, System.Single yAdvance) { }
        // RVA: 0x04D92B90  token: 0x600029A
        private System.Void .ctor(TMPro.GlyphValueRecord_Legacy valueRecord) { }
        // RVA: 0x04DB9AA0  token: 0x600029B
        private System.Void .ctor(UnityEngine.TextCore.LowLevel.GlyphValueRecord valueRecord) { }
        // RVA: 0x04DB9C30  token: 0x600029C
        public static TMPro.TMP_GlyphValueRecord op_Addition(TMPro.TMP_GlyphValueRecord a, TMPro.TMP_GlyphValueRecord b) { }

    }

    // TypeToken: 0x2000058  // size: 0x24
    public sealed struct TMP_GlyphAdjustmentRecord
    {
        // Fields
        private System.UInt32 m_GlyphIndex;  // 0x10
        private TMPro.TMP_GlyphValueRecord m_GlyphValueRecord;  // 0x14

        // Properties
        System.UInt32 glyphIndex { get; /* RVA: 0x02B77620 */ set; /* RVA: 0x04D86140 */ }
        TMPro.TMP_GlyphValueRecord glyphValueRecord { get; /* RVA: 0x04D9C2B0 */ set; /* RVA: 0x04DB9CC0 */ }

        // Methods
        // RVA: 0x04D9C150  token: 0x60002A1
        public System.Void .ctor(System.UInt32 glyphIndex, TMPro.TMP_GlyphValueRecord glyphValueRecord) { }
        // RVA: 0x0B31A444  token: 0x60002A2
        private System.Void .ctor(UnityEngine.TextCore.LowLevel.GlyphAdjustmentRecord adjustmentRecord) { }

    }

    // TypeToken: 0x2000059  // size: 0x40
    public class TMP_GlyphPairAdjustmentRecord
    {
        // Fields
        public TMPro.TMP_GlyphAdjustmentRecord m_FirstAdjustmentRecord;  // 0x10
        public TMPro.TMP_GlyphAdjustmentRecord m_SecondAdjustmentRecord;  // 0x24
        public TMPro.FontFeatureLookupFlags m_FeatureLookupFlags;  // 0x38

        // Properties
        TMPro.TMP_GlyphAdjustmentRecord firstAdjustmentRecord { get; /* RVA: 0x04D8FE30 */ set; /* RVA: 0x04DA39A0 */ }
        TMPro.TMP_GlyphAdjustmentRecord secondAdjustmentRecord { get; /* RVA: 0x04DB9D00 */ set; /* RVA: 0x04DB9D20 */ }
        TMPro.FontFeatureLookupFlags featureLookupFlags { get; /* RVA: 0x04D863B0 */ set; /* RVA: 0x04D86430 */ }

        // Methods
        // RVA: 0x04DB9CD0  token: 0x60002A9
        public System.Void .ctor(TMPro.TMP_GlyphAdjustmentRecord firstAdjustmentRecord, TMPro.TMP_GlyphAdjustmentRecord secondAdjustmentRecord) { }
        // RVA: 0x0B31A48C  token: 0x60002AA
        private System.Void .ctor(UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord glyphPairAdjustmentRecord) { }

    }

    // TypeToken: 0x200005A  // size: 0x1C
    public sealed struct GlyphPairKey
    {
        // Fields
        public System.UInt32 firstGlyphIndex;  // 0x10
        public System.UInt32 secondGlyphIndex;  // 0x14
        public System.UInt32 key;  // 0x18

        // Methods
        // RVA: 0x04DB9CA0  token: 0x60002AB
        public System.Void .ctor(System.UInt32 firstGlyphIndex, System.UInt32 secondGlyphIndex) { }
        // RVA: 0x0B31A044  token: 0x60002AC
        private System.Void .ctor(TMPro.TMP_GlyphPairAdjustmentRecord record) { }

    }

    // TypeToken: 0x200005B  // size: 0x20
    public class TMP_FontFeatureTable
    {
        // Fields
        private System.Collections.Generic.List<TMPro.TMP_GlyphPairAdjustmentRecord> m_GlyphPairAdjustmentRecords;  // 0x10
        public System.Collections.Generic.Dictionary<System.UInt32,TMPro.TMP_GlyphPairAdjustmentRecord> m_GlyphPairAdjustmentRecordLookupDictionary;  // 0x18

        // Properties
        System.Collections.Generic.List<TMPro.TMP_GlyphPairAdjustmentRecord> glyphPairAdjustmentRecords { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }

        // Methods
        // RVA: 0x0450C870  token: 0x60002AF
        public System.Void .ctor() { }
        // RVA: 0x0B31A224  token: 0x60002B0
        public System.Void SortGlyphPairAdjustmentRecords() { }

    }

    // TypeToken: 0x200005D  // size: 0x390
    public class TMP_InputField : UnityEngine.UI.Selectable, UnityEngine.EventSystems.IUpdateSelectedHandler, UnityEngine.EventSystems.IEventSystemHandler, UnityEngine.EventSystems.IBeginDragHandler, UnityEngine.EventSystems.IDragHandler, UnityEngine.EventSystems.IEndDragHandler, UnityEngine.EventSystems.IPointerClickHandler, UnityEngine.EventSystems.ISubmitHandler, UnityEngine.UI.ICanvasElement, UnityEngine.UI.ILayoutElement, UnityEngine.EventSystems.IScrollHandler
    {
        // Fields
        protected UnityEngine.TouchScreenKeyboard m_SoftKeyboard;  // 0x188
        private static readonly System.Char[] kSeparators;  // static @ 0x0
        protected UnityEngine.RectTransform m_RectTransform;  // 0x190
        protected UnityEngine.RectTransform m_TextViewport;  // 0x198
        protected UnityEngine.UI.RectMask2D m_TextComponentRectMask;  // 0x1a0
        protected UnityEngine.UI.RectMask2D m_TextViewportRectMask;  // 0x1a8
        private UnityEngine.Rect m_CachedViewportRect;  // 0x1b0
        protected TMPro.TMP_Text m_TextComponent;  // 0x1c0
        protected UnityEngine.RectTransform m_TextComponentRectTransform;  // 0x1c8
        protected UnityEngine.UI.Graphic m_Placeholder;  // 0x1d0
        protected UnityEngine.UI.Scrollbar m_VerticalScrollbar;  // 0x1d8
        protected TMPro.TMP_ScrollbarEventHandler m_VerticalScrollbarEventHandler;  // 0x1e0
        private System.Boolean m_IsDrivenByLayoutComponents;  // 0x1e8
        private UnityEngine.UI.LayoutGroup m_LayoutGroup;  // 0x1f0
        private UnityEngine.EventSystems.IScrollHandler m_IScrollHandlerParent;  // 0x1f8
        private System.Single m_ScrollPosition;  // 0x200
        protected System.Single m_ScrollSensitivity;  // 0x204
        private TMPro.TMP_InputField.ContentType m_ContentType;  // 0x208
        private TMPro.TMP_InputField.InputType m_InputType;  // 0x20c
        private System.Char m_AsteriskChar;  // 0x210
        private UnityEngine.TouchScreenKeyboardType m_KeyboardType;  // 0x214
        private TMPro.TMP_InputField.LineType m_LineType;  // 0x218
        private System.Boolean m_HideMobileInput;  // 0x21c
        private System.Boolean m_HideSoftKeyboard;  // 0x21d
        private TMPro.TMP_InputField.CharacterValidation m_CharacterValidation;  // 0x220
        private System.String m_RegexValue;  // 0x228
        private System.Single m_GlobalPointSize;  // 0x230
        private System.Int32 m_CharacterLimit;  // 0x234
        private TMPro.TMP_InputField.SubmitEvent m_OnEndEdit;  // 0x238
        private TMPro.TMP_InputField.SubmitEvent m_OnSubmit;  // 0x240
        private TMPro.TMP_InputField.SelectionEvent m_OnSelect;  // 0x248
        private TMPro.TMP_InputField.SelectionEvent m_OnDeselect;  // 0x250
        private TMPro.TMP_InputField.TextSelectionEvent m_OnTextSelection;  // 0x258
        private TMPro.TMP_InputField.TextSelectionEvent m_OnEndTextSelection;  // 0x260
        private TMPro.TMP_InputField.OnChangeEvent m_OnValueChanged;  // 0x268
        private TMPro.TMP_InputField.TouchScreenKeyboardEvent m_OnTouchScreenKeyboardStatusChanged;  // 0x270
        private TMPro.TMP_InputField.OnValidateInput m_OnValidateInput;  // 0x278
        private UnityEngine.Events.UnityEvent m_OnFocused;  // 0x280
        private UnityEngine.Color m_CaretColor;  // 0x288
        private System.Boolean m_CustomCaretColor;  // 0x298
        private UnityEngine.Color m_SelectionColor;  // 0x29c
        protected System.String m_Text;  // 0x2b0
        private System.Single m_CaretBlinkRate;  // 0x2b8
        private System.Int32 m_CaretWidth;  // 0x2bc
        private System.Boolean m_ReadOnly;  // 0x2c0
        private System.Boolean m_RichText;  // 0x2c1
        private System.Boolean m_disableUpDownArrowKeyMovingCaret;  // 0x2c2
        private System.Boolean m_disableLeftRightArrowKeyMovingCaret;  // 0x2c3
        protected System.Int32 m_StringPosition;  // 0x2c4
        protected System.Int32 m_StringSelectPosition;  // 0x2c8
        protected System.Int32 m_CaretPosition;  // 0x2cc
        protected System.Int32 m_CaretSelectPosition;  // 0x2d0
        private UnityEngine.RectTransform caretRectTrans;  // 0x2d8
        protected UnityEngine.UIVertex[] m_CursorVerts;  // 0x2e0
        private UnityEngine.CanvasRenderer m_CachedInputRenderer;  // 0x2e8
        private UnityEngine.Vector2 m_LastPosition;  // 0x2f0
        protected UnityEngine.Mesh m_Mesh;  // 0x2f8
        private System.Boolean m_AllowInput;  // 0x300
        private System.Boolean m_ShouldActivateNextUpdate;  // 0x301
        private System.Boolean m_UpdateDrag;  // 0x302
        private System.Boolean m_DragPositionOutOfBounds;  // 0x303
        private static System.Single kHScrollSpeed;  // const
        private static System.Single kVScrollSpeed;  // const
        protected System.Boolean m_CaretVisible;  // 0x304
        private UnityEngine.Coroutine m_BlinkCoroutine;  // 0x308
        private System.Single m_BlinkStartTime;  // 0x310
        private UnityEngine.Coroutine m_DragCoroutine;  // 0x318
        private System.String m_OriginalText;  // 0x320
        private System.Boolean m_WasCanceled;  // 0x328
        private System.Boolean m_HasDoneFocusTransition;  // 0x329
        private UnityEngine.WaitForSecondsRealtime m_WaitForSecondsRealtime;  // 0x330
        private System.Boolean m_PreventCallback;  // 0x338
        private System.Boolean m_TouchKeyboardAllowsInPlaceEditing;  // 0x339
        private System.Boolean m_IsTextComponentUpdateRequired;  // 0x33a
        private System.Boolean m_isLastKeyBackspace;  // 0x33b
        private System.Single m_PointerDownClickStartTime;  // 0x33c
        private System.Single m_KeyDownStartTime;  // 0x340
        private System.Single m_DoubleClickDelay;  // 0x344
        private static System.String kEmailSpecialCharacters;  // const
        private System.Boolean m_IsCompositionActive;  // 0x348
        private System.Boolean m_ShouldUpdateIMEWindowPosition;  // 0x349
        private System.Int32 m_PreviousIMEInsertionLine;  // 0x34c
        public System.Func<System.String,System.Int32,System.String> onValidateCharacterLimit;  // 0x350
        public System.Func<System.String,System.Int32> onGetTextLength;  // 0x358
        protected System.Boolean m_OnFocusSelectAll;  // 0x360
        protected System.Boolean m_isSelectAll;  // 0x361
        protected System.Boolean m_ResetOnDeActivation;  // 0x362
        private System.Boolean m_SelectionStillActive;  // 0x363
        private System.Boolean m_ReleaseSelection;  // 0x364
        private UnityEngine.GameObject m_PreviouslySelectedObject;  // 0x368
        private System.Boolean m_RestoreOriginalTextOnEscape;  // 0x370
        private System.Boolean m_WontReleaseSelectionOnEscape;  // 0x371
        private System.Boolean m_tempClearCompositionString;  // 0x372
        protected System.Boolean m_isRichTextEditingAllowed;  // 0x373
        protected System.Int32 m_LineLimit;  // 0x374
        protected TMPro.TMP_InputValidator m_InputValidator;  // 0x378
        private System.Boolean m_isSelected;  // 0x380
        private System.Boolean m_IsStringPositionDirty;  // 0x381
        private System.Boolean m_IsCaretPositionDirty;  // 0x382
        private System.Boolean m_forceRectTransformAdjustment;  // 0x383
        private UnityEngine.Event m_ProcessingEvent;  // 0x388

        // Properties
        System.Boolean isLastKeyBackspace { get; /* RVA: 0x04DB9DA0 */ set; /* RVA: 0x04DB9E70 */ }
        UnityEngine.EventSystems.BaseInput inputSystem { get; /* RVA: 0x03795980 */ }
        System.String compositionString { get; /* RVA: 0x03795240 */ }
        System.Int32 compositionLength { get; /* RVA: 0x03794E90 */ }
        UnityEngine.Mesh mesh { get; /* RVA: 0x03797620 */ }
        System.Boolean shouldHideMobileInput { get; /* RVA: 0x0B31E21C */ set; /* RVA: 0x0B31EC58 */ }
        System.Boolean shouldHideSoftKeyboard { get; /* RVA: 0x0B31E24C */ set; /* RVA: 0x0B31ECC4 */ }
        System.String text { get; /* RVA: 0x04D8DEA0 */ set; /* RVA: 0x0B31EEB8 */ }
        System.Boolean isFocused { get; /* RVA: 0x04D92940 */ }
        System.Single caretBlinkRate { get; /* RVA: 0x04DB9D40 */ set; /* RVA: 0x0B31E2FC */ }
        System.Int32 caretWidth { get; /* RVA: 0x04DB9D50 */ set; /* RVA: 0x0B31E3DC */ }
        UnityEngine.RectTransform textViewport { get; /* RVA: 0x04D8DED0 */ set; /* RVA: 0x0B31EE6C */ }
        TMPro.TMP_Text textComponent { get; /* RVA: 0x04D8DA80 */ set; /* RVA: 0x0B31EE10 */ }
        UnityEngine.UI.Graphic placeholder { get; /* RVA: 0x04D8D7B0 */ set; /* RVA: 0x0B31E9D8 */ }
        UnityEngine.UI.Scrollbar verticalScrollbar { get; /* RVA: 0x04D87870 */ set; /* RVA: 0x0B31EEC4 */ }
        System.Single scrollSensitivity { get; /* RVA: 0x04DA2420 */ set; /* RVA: 0x0B31EACC */ }
        UnityEngine.Color caretColor { get; /* RVA: 0x03797430 */ set; /* RVA: 0x0B31E364 */ }
        System.Boolean customCaretColor { get; /* RVA: 0x04D90190 */ set; /* RVA: 0x0B31E4F8 */ }
        UnityEngine.Color selectionColor { get; /* RVA: 0x04DB9E30 */ set; /* RVA: 0x0B31EB64 */ }
        TMPro.TMP_InputField.SubmitEvent onEndEdit { get; /* RVA: 0x04D8DAD0 */ set; /* RVA: 0x0B31E778 */ }
        TMPro.TMP_InputField.SubmitEvent onSubmit { get; /* RVA: 0x04D8DE80 */ set; /* RVA: 0x0B31E8A8 */ }
        TMPro.TMP_InputField.SelectionEvent onSelect { get; /* RVA: 0x04D8D840 */ set; /* RVA: 0x0B31E85C */ }
        TMPro.TMP_InputField.SelectionEvent onDeselect { get; /* RVA: 0x04D8DCE0 */ set; /* RVA: 0x0B31E72C */ }
        TMPro.TMP_InputField.TextSelectionEvent onTextSelection { get; /* RVA: 0x04D8DE70 */ set; /* RVA: 0x0B31E8F4 */ }
        TMPro.TMP_InputField.TextSelectionEvent onEndTextSelection { get; /* RVA: 0x04D8D810 */ set; /* RVA: 0x0B31E7C4 */ }
        TMPro.TMP_InputField.OnChangeEvent onValueChanged { get; /* RVA: 0x04D8DE90 */ set; /* RVA: 0x0B31E98C */ }
        TMPro.TMP_InputField.TouchScreenKeyboardEvent onTouchScreenKeyboardStatusChanged { get; /* RVA: 0x04D8DF10 */ set; /* RVA: 0x0B31E940 */ }
        TMPro.TMP_InputField.OnValidateInput onValidateInput { get; /* RVA: 0x04D8DB90 */ set; /* RVA: 0x047A98F0 */ }
        UnityEngine.Events.UnityEvent onFocused { get; /* RVA: 0x04D8DD90 */ set; /* RVA: 0x0B31E810 */ }
        System.Int32 characterLimit { get; /* RVA: 0x04DB9D60 */ set; /* RVA: 0x0461D360 */ }
        System.Single pointSize { get; /* RVA: 0x04DB9DF0 */ set; /* RVA: 0x0B31EA24 */ }
        System.Boolean onFocusSelectAll { get; /* RVA: 0x04DB9DE0 */ set; /* RVA: 0x04DB9E90 */ }
        System.Boolean resetOnDeActivation { get; /* RVA: 0x04DB9E10 */ set; /* RVA: 0x04DB9EB0 */ }
        System.Boolean restoreOriginalTextOnEscape { get; /* RVA: 0x04DB9E20 */ set; /* RVA: 0x04DB9EC0 */ }
        System.Boolean wontReleaseSelectionOnEscape { get; /* RVA: 0x04DB9E50 */ set; /* RVA: 0x04DB9ED0 */ }
        System.Boolean disableArrowKeyMoveCaret { get; /* RVA: 0x04DB9D80 */ set; /* RVA: 0x04DB9E60 */ }
        System.Boolean isRichTextEditingAllowed { get; /* RVA: 0x04DB9DB0 */ set; /* RVA: 0x04DB9E80 */ }
        TMPro.TMP_InputField.ContentType contentType { get; /* RVA: 0x04DB9D70 */ set; /* RVA: 0x0B31E49C */ }
        TMPro.TMP_InputField.LineType lineType { get; /* RVA: 0x04DAA150 */ set; /* RVA: 0x0B31E688 */ }
        System.Int32 lineLimit { get; /* RVA: 0x04DB9DD0 */ set; /* RVA: 0x0B31E628 */ }
        TMPro.TMP_InputField.InputType inputType { get; /* RVA: 0x04DB9D90 */ set; /* RVA: 0x0B31E510 */ }
        UnityEngine.TouchScreenKeyboardType keyboardType { get; /* RVA: 0x04DB9DC0 */ set; /* RVA: 0x0B31E5CC */ }
        TMPro.TMP_InputField.CharacterValidation characterValidation { get; /* RVA: 0x04D902A0 */ set; /* RVA: 0x0B31E438 */ }
        TMPro.TMP_InputValidator inputValidator { get; /* RVA: 0x04D8DD00 */ set; /* RVA: 0x0B31E56C */ }
        System.Boolean readOnly { get; /* RVA: 0x04DB9E00 */ set; /* RVA: 0x04DB9EA0 */ }
        System.Boolean richText { get; /* RVA: 0x04D8FB10 */ set; /* RVA: 0x0B31EABC */ }
        System.Boolean multiLine { get; /* RVA: 0x03798D60 */ }
        System.Char asteriskChar { get; /* RVA: 0x04DB9D30 */ set; /* RVA: 0x0B31E2A0 */ }
        System.Boolean wasCanceled { get; /* RVA: 0x04DB9E40 */ }
        System.Int32 caretPositionInternal { get; /* RVA: 0x03796960 */ set; /* RVA: 0x0539550C */ }
        System.Int32 stringPositionInternal { get; /* RVA: 0x037969E0 */ set; /* RVA: 0x05395524 */ }
        System.Int32 caretSelectPositionInternal { get; /* RVA: 0x04CD9910 */ set; /* RVA: 0x053954F4 */ }
        System.Int32 stringSelectPositionInternal { get; /* RVA: 0x037969A0 */ set; /* RVA: 0x053954DC */ }
        System.Boolean hasSelection { get; /* RVA: 0x037974A0 */ }
        System.Int32 caretPosition { get; /* RVA: 0x053954D4 */ set; /* RVA: 0x0B31E3A4 */ }
        System.Int32 selectionAnchorPosition { get; /* RVA: 0x053954CC */ set; /* RVA: 0x0B31EB28 */ }
        System.Int32 selectionFocusPosition { get; /* RVA: 0x053954D4 */ set; /* RVA: 0x0B31EBA4 */ }
        System.Int32 stringPosition { get; /* RVA: 0x0B31E214 */ set; /* RVA: 0x0B31EDD8 */ }
        System.Int32 selectionStringAnchorPosition { get; /* RVA: 0x0B31E20C */ set; /* RVA: 0x0B31EBE0 */ }
        System.Int32 selectionStringFocusPosition { get; /* RVA: 0x0B31E214 */ set; /* RVA: 0x0B31EC1C */ }
        System.String clipboard { get; /* RVA: 0x0B31DF8C */ set; /* RVA: 0x0B31E494 */ }
        System.Single minWidth { get; /* RVA: 0x04D879D0 */ }
        System.Single preferredWidth { get; /* RVA: 0x0B31E0D0 */ }
        System.Single flexibleWidth { get; /* RVA: 0x04DA9E10 */ }
        System.Single minHeight { get; /* RVA: 0x04D879D0 */ }
        System.Single preferredHeight { get; /* RVA: 0x0B31DF94 */ }
        System.Single flexibleHeight { get; /* RVA: 0x04DA9E10 */ }
        System.Int32 layoutPriority { get; /* RVA: 0x04D85B80 */ }

        // Methods
        // RVA: 0x0405B410  token: 0x60002BA
        protected System.Void .ctor() { }
        // RVA: 0x037977F0  token: 0x60002C0
        private System.Boolean isKeyboardUsingEvents() { }
        // RVA: 0x0B31D620  token: 0x60002C3
        public System.Void SetTextWithoutNotify(System.String input) { }
        // RVA: 0x04B387E0  token: 0x60002C4
        private System.Void SetText(System.String value, System.Boolean sendCallback) { }
        // RVA: 0x049B1550  token: 0x6000314
        protected System.Void ClampStringPos(System.Int32& pos) { }
        // RVA: 0x0456AF70  token: 0x6000315
        protected System.Void ClampCaretPos(System.Int32& pos) { }
        // RVA: 0x02EDD330  token: 0x600032B
        protected virtual System.Void OnEnable() { }
        // RVA: 0x02EDCD20  token: 0x600032C
        protected virtual System.Void OnDisable() { }
        // RVA: 0x03BA6410  token: 0x600032D
        private System.Void ON_TEXT_CHANGED(UnityEngine.Object obj) { }
        // RVA: 0x04D35060  token: 0x600032E
        private System.Collections.IEnumerator CaretBlink() { }
        // RVA: 0x037986B0  token: 0x600032F
        private System.Void SetCaretVisible() { }
        // RVA: 0x037986F0  token: 0x6000330
        private System.Void SetCaretActive() { }
        // RVA: 0x03BA8AB0  token: 0x6000331
        protected System.Void OnFocus() { }
        // RVA: 0x03BA7DF0  token: 0x6000332
        protected System.Void SelectAll() { }
        // RVA: 0x0B31C210  token: 0x6000333
        public System.Void MoveTextEnd(System.Boolean shift) { }
        // RVA: 0x0B31C2E0  token: 0x6000334
        public System.Void MoveTextStart(System.Boolean shift) { }
        // RVA: 0x0B31C384  token: 0x6000335
        public System.Void MoveToEndOfLine(System.Boolean shift, System.Boolean ctrl) { }
        // RVA: 0x0B31C4DC  token: 0x6000336
        public System.Void MoveToStartOfLine(System.Boolean shift, System.Boolean ctrl) { }
        // RVA: 0x03797860  token: 0x6000339
        private System.Boolean InPlaceEditing() { }
        // RVA: 0x0B31D770  token: 0x600033A
        private System.Void UpdateStringPositionFromKeyboard() { }
        // RVA: 0x037976E0  token: 0x600033B
        protected virtual System.Void LateUpdate() { }
        // RVA: 0x03E9A110  token: 0x600033C
        private System.Boolean MayDrag(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B31C790  token: 0x600033D
        public virtual System.Void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B31C7B4  token: 0x600033E
        public virtual System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B31B4F4  token: 0x600033F
        private System.Collections.IEnumerator MouseDragOutsideRect(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B31CB0C  token: 0x6000340
        public virtual System.Void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x03E99F40  token: 0x6000341
        public virtual System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x03798A90  token: 0x6000342
        protected TMPro.TMP_InputField.EditState KeyPressed(UnityEngine.Event evt) { }
        // RVA: 0x03798D40  token: 0x6000343
        protected virtual System.Boolean IsValidChar(System.Char c) { }
        // RVA: 0x0B31D380  token: 0x6000344
        public System.Void ProcessEvent(UnityEngine.Event e) { }
        // RVA: 0x03798820  token: 0x6000345
        public virtual System.Void OnUpdateSelected(UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0B31CB30  token: 0x6000346
        public virtual System.Void OnScroll(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B31AE40  token: 0x6000347
        private System.Single GetScrollPositionRelativeToViewport() { }
        // RVA: 0x0B31AF48  token: 0x6000348
        private System.String GetSelectedString() { }
        // RVA: 0x0B31AB64  token: 0x6000349
        private System.Int32 FindNextWordBegin() { }
        // RVA: 0x0B31BF04  token: 0x600034A
        private System.Void MoveRight(System.Boolean shift, System.Boolean ctrl) { }
        // RVA: 0x0B31AC20  token: 0x600034B
        private System.Int32 FindPrevWordBegin() { }
        // RVA: 0x0B31B6C0  token: 0x600034C
        private System.Void MoveLeft(System.Boolean shift, System.Boolean ctrl) { }
        // RVA: 0x0B31B2A0  token: 0x600034D
        private System.Int32 LineUpCharacterPosition(System.Int32 originalPos, System.Boolean goToFirstChar) { }
        // RVA: 0x0B31B028  token: 0x600034E
        private System.Int32 LineDownCharacterPosition(System.Int32 originalPos, System.Boolean goToLastChar) { }
        // RVA: 0x0B31D078  token: 0x600034F
        private System.Int32 PageUpCharacterPosition(System.Int32 originalPos, System.Boolean goToFirstChar) { }
        // RVA: 0x0B31CD38  token: 0x6000350
        private System.Int32 PageDownCharacterPosition(System.Int32 originalPos, System.Boolean goToLastChar) { }
        // RVA: 0x0B31B6B4  token: 0x6000351
        private System.Void MoveDown(System.Boolean shift) { }
        // RVA: 0x0B31B568  token: 0x6000352
        private System.Void MoveDown(System.Boolean shift, System.Boolean goToLastChar) { }
        // RVA: 0x0B31C660  token: 0x6000353
        private System.Void MoveUp(System.Boolean shift) { }
        // RVA: 0x0B31C66C  token: 0x6000354
        private System.Void MoveUp(System.Boolean shift, System.Boolean goToFirstChar) { }
        // RVA: 0x0B31BEF8  token: 0x6000355
        private System.Void MovePageUp(System.Boolean shift) { }
        // RVA: 0x0B31BC40  token: 0x6000356
        private System.Void MovePageUp(System.Boolean shift, System.Boolean goToFirstChar) { }
        // RVA: 0x0B31BC34  token: 0x6000357
        private System.Void MovePageDown(System.Boolean shift) { }
        // RVA: 0x0B31B964  token: 0x6000358
        private System.Void MovePageDown(System.Boolean shift, System.Boolean goToLastChar) { }
        // RVA: 0x03BA5E90  token: 0x6000359
        private System.Void Delete() { }
        // RVA: 0x0B31A80C  token: 0x600035A
        private System.Void DeleteKey() { }
        // RVA: 0x03BA6110  token: 0x600035B
        private System.Void Backspace() { }
        // RVA: 0x0B31A784  token: 0x600035C
        protected virtual System.Void Append(System.String input) { }
        // RVA: 0x03BA5F50  token: 0x600035D
        protected virtual System.Void Append(System.Char input) { }
        // RVA: 0x03BA5BB0  token: 0x600035E
        private System.Void Insert(System.Char c) { }
        // RVA: 0x03BA5E10  token: 0x600035F
        private System.Void UpdateTouchKeyboardFromEditChanges() { }
        // RVA: 0x03BA63F0  token: 0x6000360
        private System.Void SendOnValueChangedAndUpdateLabel() { }
        // RVA: 0x03BA5E30  token: 0x6000361
        private System.Void SendOnValueChanged() { }
        // RVA: 0x03BA8B90  token: 0x6000362
        protected System.Void SendOnEndEdit() { }
        // RVA: 0x0B31D388  token: 0x6000363
        protected System.Void SendOnSubmit() { }
        // RVA: 0x04B47D60  token: 0x6000364
        protected virtual System.Void SendOnFocus() { }
        // RVA: 0x04CB19D0  token: 0x6000365
        protected virtual System.Void SendOnFocusLost() { }
        // RVA: 0x04CECD70  token: 0x6000366
        protected System.Void SendOnTextSelection() { }
        // RVA: 0x03797C90  token: 0x6000367
        protected System.Void SendOnEndTextSelection() { }
        // RVA: 0x0B31D3D8  token: 0x6000368
        protected System.Void SendTouchScreenKeyboardStatusChanged() { }
        // RVA: 0x03798250  token: 0x6000369
        protected System.Void UpdateLabel() { }
        // RVA: 0x0B31D694  token: 0x600036A
        private System.Void UpdateScrollbar() { }
        // RVA: 0x0B31CCB0  token: 0x600036B
        private System.Void OnScrollbarValueChange(System.Single value) { }
        // RVA: 0x041E1670  token: 0x600036C
        private System.Void UpdateMaskRegions() { }
        // RVA: 0x0B31A55C  token: 0x600036D
        private System.Void AdjustTextPositionRelativeToViewport(System.Single relativePosition) { }
        // RVA: 0x03BA6510  token: 0x600036E
        private System.Int32 GetCaretPositionFromStringIndex(System.Int32 stringIndex) { }
        // RVA: 0x0B31AD6C  token: 0x600036F
        private System.Int32 GetMinCaretPositionFromStringIndex(System.Int32 stringIndex) { }
        // RVA: 0x0B31ACDC  token: 0x6000370
        private System.Int32 GetMaxCaretPositionFromStringIndex(System.Int32 stringIndex) { }
        // RVA: 0x0B31AFD4  token: 0x6000371
        private System.Int32 GetStringIndexFromCaretPosition(System.Int32 caretPosition) { }
        // RVA: 0x0B31ACD4  token: 0x6000372
        public System.Void ForceLabelUpdate() { }
        // RVA: 0x03797E70  token: 0x6000373
        private System.Void MarkGeometryAsDirty() { }
        // RVA: 0x03797540  token: 0x6000374
        public virtual System.Void Rebuild(UnityEngine.UI.CanvasUpdate update) { }
        // RVA: 0x041E1670  token: 0x6000375
        public virtual System.Void LayoutComplete() { }
        // RVA: 0x041E1670  token: 0x6000376
        public virtual System.Void GraphicUpdateComplete() { }
        // RVA: 0x03797550  token: 0x6000377
        private System.Void UpdateGeometry() { }
        // RVA: 0x036CD400  token: 0x6000378
        private System.Void AssignPositioningIfNeeded() { }
        // RVA: 0x03797900  token: 0x6000379
        private System.Void OnFillVBO(UnityEngine.Mesh vbo) { }
        // RVA: 0x03796A20  token: 0x600037A
        private System.Void GenerateCaret(UnityEngine.UI.VertexHelper vbo, UnityEngine.Vector2 roundingOffset) { }
        // RVA: 0x04753660  token: 0x600037B
        private System.Void CreateCursorVerts() { }
        // RVA: 0x03BA6900  token: 0x600037C
        private System.Void GenerateHightlight(UnityEngine.UI.VertexHelper vbo, UnityEngine.Vector2 roundingOffset) { }
        // RVA: 0x04419E00  token: 0x600037D
        private System.Void AdjustRectTransformRelativeToViewport(UnityEngine.Vector2 startPosition, System.Single height, System.Boolean isCharVisible) { }
        // RVA: 0x0B31D868  token: 0x600037E
        protected System.Char Validate(System.String text, System.Int32 pos, System.Char ch) { }
        // RVA: 0x03BA85A0  token: 0x600037F
        public virtual System.Void ActivateInputField() { }
        // RVA: 0x012081B0  token: 0x6000380
        protected virtual System.Boolean TryActivateInputFieldCustom() { }
        // RVA: 0x03BA8AF0  token: 0x6000381
        protected System.Void CompleteActivation() { }
        // RVA: 0x03BA88C0  token: 0x6000382
        private System.Void ActivateInputFieldInternal() { }
        // RVA: 0x04CA2AE0  token: 0x6000383
        public virtual System.Void OnSelect(UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x04D799D0  token: 0x6000384
        public virtual System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x041E1670  token: 0x6000385
        public System.Void OnControlClick() { }
        // RVA: 0x03BA8B40  token: 0x6000386
        public System.Void ReleaseSelection() { }
        // RVA: 0x03BA81C0  token: 0x6000387
        public virtual System.Void DeactivateInputField(System.Boolean clearSelection) { }
        // RVA: 0x04B6C940  token: 0x6000388
        public virtual System.Void OnDeselect(UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0B31CCF0  token: 0x6000389
        public virtual System.Void OnSubmit(UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0B31AA30  token: 0x600038A
        private System.Void EnforceContentType() { }
        // RVA: 0x0405B940  token: 0x600038B
        private System.Void SetTextComponentWrapMode() { }
        // RVA: 0x0B31D5A8  token: 0x600038C
        private System.Void SetTextComponentRichTextMode() { }
        // RVA: 0x0B31D62C  token: 0x600038D
        private System.Void SetToCustomIfContentTypeIsNot(TMPro.TMP_InputField.ContentType[] allowedContentTypes) { }
        // RVA: 0x0B31D67C  token: 0x600038E
        private System.Void SetToCustom() { }
        // RVA: 0x0B31D67C  token: 0x600038F
        private System.Void SetToCustom(TMPro.TMP_InputField.CharacterValidation characterValidation) { }
        // RVA: 0x049E42C0  token: 0x6000390
        protected virtual System.Void DoStateTransition(UnityEngine.UI.Selectable.SelectionState state, System.Boolean instant) { }
        // RVA: 0x041E1670  token: 0x6000391
        public virtual System.Void CalculateLayoutInputHorizontal() { }
        // RVA: 0x041E1670  token: 0x6000392
        public virtual System.Void CalculateLayoutInputVertical() { }
        // RVA: 0x0B31D4F8  token: 0x600039A
        public System.Void SetGlobalPointSize(System.Single pointSize) { }
        // RVA: 0x0B31D448  token: 0x600039B
        public System.Void SetGlobalFontAsset(TMPro.TMP_FontAsset fontAsset) { }
        // RVA: 0x04D16A70  token: 0x600039C
        private static System.Void .cctor() { }
        // RVA: 0x053953F4  token: 0x600039D
        private virtual UnityEngine.Transform UnityEngine.UI.ICanvasElement.get_transform() { }

    }

    // TypeToken: 0x200006B  // size: 0x10
    public static class SetPropertyUtility
    {
        // Methods
        // RVA: 0x0B31A1E4  token: 0x60003B3
        public static System.Boolean SetColor(UnityEngine.Color& currentValue, UnityEngine.Color newValue) { }
        // RVA: -1  // generic def  token: 0x60003B4
        public static System.Boolean SetEquatableStruct(T& currentValue, T newValue) { }
        // RVA: -1  // generic def  token: 0x60003B5
        public static System.Boolean SetStruct(T& currentValue, T newValue) { }
        // RVA: -1  // generic def  token: 0x60003B6
        public static System.Boolean SetClass(T& currentValue, T newValue) { }

    }

    // TypeToken: 0x200006C  // size: 0x18
    public abstract class TMP_InputValidator : UnityEngine.ScriptableObject
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60003B7
        public virtual System.Char Validate(System.String& text, System.Int32& pos, System.Char ch) { }
        // RVA: 0x03573370  token: 0x60003B8
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200006D  // size: 0x6C
    public sealed struct TMP_LineInfo
    {
        // Fields
        public System.Int32 controlCharacterCount;  // 0x10
        public System.Int32 characterCount;  // 0x14
        public System.Int32 visibleCharacterCount;  // 0x18
        public System.Int32 spaceCount;  // 0x1c
        public System.Int32 wordCount;  // 0x20
        public System.Int32 firstCharacterIndex;  // 0x24
        public System.Int32 firstVisibleCharacterIndex;  // 0x28
        public System.Int32 lastCharacterIndex;  // 0x2c
        public System.Int32 lastVisibleCharacterIndex;  // 0x30
        public System.Single length;  // 0x34
        public System.Single lineHeight;  // 0x38
        public System.Single ascender;  // 0x3c
        public System.Single baseline;  // 0x40
        public System.Single descender;  // 0x44
        public System.Single maxAdvance;  // 0x48
        public System.Single width;  // 0x4c
        public System.Single marginLeft;  // 0x50
        public System.Single marginRight;  // 0x54
        public TMPro.HorizontalAlignmentOptions alignment;  // 0x58
        public TMPro.Extents lineExtents;  // 0x5c

    }

    // TypeToken: 0x200006E
    public static class TMP_ListPool`1
    {
        // Fields
        private static readonly TMPro.TMP_ObjectPool<System.Collections.Generic.List<T>> s_ListPool;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60003B9
        public static System.Collections.Generic.List<T> Get() { }
        // RVA: -1  // not resolved  token: 0x60003BA
        public static System.Void Release(System.Collections.Generic.List<T> toRelease) { }
        // RVA: -1  // not resolved  token: 0x60003BB
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000070  // size: 0x10
    public static class TMP_MaterialManager
    {
        // Fields
        private static System.Collections.Generic.List<TMPro.TMP_MaterialManager.MaskingMaterial> m_materialList;  // static @ 0x0
        private static System.Collections.Generic.Dictionary<System.Int64,TMPro.TMP_MaterialManager.FallbackMaterial> m_fallbackMaterials;  // static @ 0x8
        private static System.Collections.Generic.Dictionary<System.Int32,System.Int64> m_fallbackMaterialLookup;  // static @ 0x10
        private static System.Collections.Generic.List<TMPro.TMP_MaterialManager.FallbackMaterial> m_fallbackCleanupList;  // static @ 0x18
        private static System.Boolean isFallbackListDirty;  // static @ 0x20
        private static System.String s_overlayShaderKeyword;  // static @ 0x28

        // Properties
        System.String overlayShaderKeyword { get; /* RVA: 0x0B321530 */ set; /* RVA: 0x0B321580 */ }

        // Methods
        // RVA: 0x049B2240  token: 0x60003BF
        private static System.Void .cctor() { }
        // RVA: 0x03F64E20  token: 0x60003C0
        private static System.Void OnPreRender() { }
        // RVA: 0x0B32061C  token: 0x60003C3
        public static UnityEngine.Material GetStencilMaterial(UnityEngine.Material baseMaterial, System.Int32 stencilID) { }
        // RVA: 0x0B320FD0  token: 0x60003C4
        public static System.Void ReleaseStencilMaterial(UnityEngine.Material stencilMaterial) { }
        // RVA: 0x0B31FAF4  token: 0x60003C5
        public static UnityEngine.Material GetBaseMaterial(UnityEngine.Material stencilMaterial) { }
        // RVA: 0x0B321478  token: 0x60003C6
        public static UnityEngine.Material SetStencil(UnityEngine.Material material, System.Int32 stencilID) { }
        // RVA: 0x0B31F17C  token: 0x60003C7
        public static System.Void AddMaskingMaterial(UnityEngine.Material baseMaterial, UnityEngine.Material stencilMaterial, System.Int32 stencilID) { }
        // RVA: 0x0B32133C  token: 0x60003C8
        public static System.Void RemoveStencilMaterial(UnityEngine.Material stencilMaterial) { }
        // RVA: 0x0B320994  token: 0x60003C9
        public static System.Void ReleaseBaseMaterial(UnityEngine.Material baseMaterial) { }
        // RVA: 0x0B31F5A0  token: 0x60003CA
        public static System.Void ClearMaterials() { }
        // RVA: 0x0B3203C8  token: 0x60003CB
        public static System.Int32 GetStencilID(UnityEngine.GameObject obj) { }
        // RVA: 0x03E32470  token: 0x60003CC
        public static UnityEngine.Material GetMaterialForRendering(UnityEngine.UI.MaskableGraphic graphic, UnityEngine.Material baseMaterial) { }
        // RVA: 0x0B31F9A8  token: 0x60003CD
        private static UnityEngine.Transform FindRootSortOverrideCanvas(UnityEngine.Transform start) { }
        // RVA: 0x0B3200A8  token: 0x60003CE
        private static UnityEngine.Material GetFallbackMaterial(TMPro.TMP_FontAsset fontAsset, UnityEngine.Material sourceMaterial, System.Int32 atlasIndex) { }
        // RVA: 0x0B31FC40  token: 0x60003CF
        public static UnityEngine.Material GetFallbackMaterial(UnityEngine.Material sourceMaterial, UnityEngine.Material targetMaterial) { }
        // RVA: 0x0B31F060  token: 0x60003D0
        public static System.Void AddFallbackMaterialReference(UnityEngine.Material targetMaterial) { }
        // RVA: 0x0B3211C4  token: 0x60003D1
        public static System.Void RemoveFallbackMaterialReference(UnityEngine.Material targetMaterial) { }
        // RVA: 0x0B31F3B8  token: 0x60003D2
        public static System.Void CleanupFallbackMaterials() { }
        // RVA: 0x0B320E38  token: 0x60003D3
        public static System.Void ReleaseFallbackMaterial(UnityEngine.Material fallbackMaterial) { }
        // RVA: 0x0B31F710  token: 0x60003D4
        public static System.Void CopyMaterialPresetProperties(UnityEngine.Material source, UnityEngine.Material destination) { }

    }

    // TypeToken: 0x2000077  // size: 0x14
    public sealed struct VertexSortingOrder
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.VertexSortingOrder Normal;  // const
        public static TMPro.VertexSortingOrder Reverse;  // const

    }

    // TypeToken: 0x2000078  // size: 0x60
    public sealed struct TMP_MeshInfo : System.IDisposable
    {
        // Fields
        private static readonly UnityEngine.Color32 s_DefaultColor;  // static @ 0x0
        private static readonly UnityEngine.Vector3 s_DefaultNormal;  // static @ 0x4
        private static readonly UnityEngine.Vector4 s_DefaultTangent;  // static @ 0x10
        private static readonly UnityEngine.Bounds s_DefaultBounds;  // static @ 0x20
        public UnityEngine.Mesh mesh;  // 0x10
        public System.Int32 vertexCount;  // 0x18
        public UnityEngine.Vector3[] vertices;  // 0x20
        public UnityEngine.Vector3[] normals;  // 0x28
        public UnityEngine.Vector4[] tangents;  // 0x30
        public UnityEngine.Vector2[] uvs0;  // 0x38
        public UnityEngine.Vector2[] uvs2;  // 0x40
        public UnityEngine.Color32[] colors32;  // 0x48
        public System.Int32[] triangles;  // 0x50
        public UnityEngine.Material material;  // 0x58

        // Methods
        // RVA: 0x032537A0  token: 0x60003DF
        public System.Void .ctor(UnityEngine.Mesh mesh, System.Int32 size) { }
        // RVA: 0x0B32DC78  token: 0x60003E0
        public System.Void .ctor(UnityEngine.Mesh mesh, System.Int32 size, System.Boolean isVolumetric) { }
        // RVA: 0x031CF8D0  token: 0x60003E1
        public System.Void ResizeMeshInfo(System.Int32 size) { }
        // RVA: 0x0B32C724  token: 0x60003E2
        public System.Void ResizeMeshInfo(System.Int32 size, System.Boolean isVolumetric) { }
        // RVA: 0x0B32C694  token: 0x60003E3
        public System.Void Clear() { }
        // RVA: 0x03BEB310  token: 0x60003E4
        public System.Void Clear(System.Boolean uploadChanges) { }
        // RVA: 0x0B32C65C  token: 0x60003E5
        public System.Void ClearUnusedVertices() { }
        // RVA: 0x0B32C580  token: 0x60003E6
        public System.Void ClearUnusedVertices(System.Int32 startIndex) { }
        // RVA: 0x0B32C5B4  token: 0x60003E7
        public System.Void ClearUnusedVertices(System.Int32 startIndex, System.Boolean updateMesh) { }
        // RVA: 0x0B32D288  token: 0x60003E8
        public System.Void SortGeometry(TMPro.VertexSortingOrder order) { }
        // RVA: 0x0B32D32C  token: 0x60003E9
        public System.Void SortGeometry(System.Collections.Generic.IList<System.Int32> sortingOrder) { }
        // RVA: 0x0B32D428  token: 0x60003EA
        public System.Void SwapVertexData(System.Int32 src, System.Int32 dst) { }
        // RVA: 0x035190A0  token: 0x60003EB
        public virtual System.Void Dispose() { }
        // RVA: 0x04B529D0  token: 0x60003EC
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000079
    public class TMP_ObjectPool`1
    {
        // Fields
        private readonly System.Collections.Generic.Stack<T> m_Stack;  // 0x0
        private readonly UnityEngine.Events.UnityAction<T> m_ActionOnGet;  // 0x0
        private readonly UnityEngine.Events.UnityAction<T> m_ActionOnRelease;  // 0x0
        private System.Int32 <countAll>k__BackingField;  // 0x0

        // Properties
        System.Int32 countAll { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Int32 countActive { get; /* RVA: -1  // not resolved */ }
        System.Int32 countInactive { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60003F1
        public System.Void .ctor(UnityEngine.Events.UnityAction<T> actionOnGet, UnityEngine.Events.UnityAction<T> actionOnRelease) { }
        // RVA: -1  // not resolved  token: 0x60003F2
        public T Get() { }
        // RVA: -1  // not resolved  token: 0x60003F3
        public System.Void Release(T element) { }

    }

    // TypeToken: 0x200007A  // size: 0x10
    public class TMP_ResourceManager
    {
        // Fields
        private static readonly TMPro.TMP_ResourceManager s_instance;  // static @ 0x0
        private static TMPro.TMP_Settings s_TextSettings;  // static @ 0x8
        private static readonly System.Collections.Generic.List<TMPro.TMP_FontAsset> s_FontAssetReferences;  // static @ 0x10
        private static readonly System.Collections.Generic.Dictionary<System.Int32,TMPro.TMP_FontAsset> s_FontAssetReferenceLookup;  // static @ 0x18

        // Methods
        // RVA: 0x0B32E9A8  token: 0x60003F4
        private static System.Void .cctor() { }
        // RVA: 0x0B32E73C  token: 0x60003F5
        private static TMPro.TMP_Settings GetTextSettings() { }
        // RVA: 0x0B32E640  token: 0x60003F6
        public static System.Void AddFontAsset(TMPro.TMP_FontAsset fontAsset) { }
        // RVA: 0x0B32E920  token: 0x60003F7
        public static System.Boolean TryGetFontAsset(System.Int32 hashcode, TMPro.TMP_FontAsset& fontAsset) { }
        // RVA: 0x0B32E82C  token: 0x60003F8
        private static System.Void RebuildFontAssetCache(System.Int32 instanceID) { }
        // RVA: 0x041E1670  token: 0x60003F9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200007B  // size: 0x14
    public sealed struct MarkupTag
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.MarkupTag BOLD;  // const
        public static TMPro.MarkupTag SLASH_BOLD;  // const
        public static TMPro.MarkupTag ITALIC;  // const
        public static TMPro.MarkupTag SLASH_ITALIC;  // const
        public static TMPro.MarkupTag UNDERLINE;  // const
        public static TMPro.MarkupTag SLASH_UNDERLINE;  // const
        public static TMPro.MarkupTag STRIKETHROUGH;  // const
        public static TMPro.MarkupTag SLASH_STRIKETHROUGH;  // const
        public static TMPro.MarkupTag MARK;  // const
        public static TMPro.MarkupTag SLASH_MARK;  // const
        public static TMPro.MarkupTag SUBSCRIPT;  // const
        public static TMPro.MarkupTag SLASH_SUBSCRIPT;  // const
        public static TMPro.MarkupTag SUPERSCRIPT;  // const
        public static TMPro.MarkupTag SLASH_SUPERSCRIPT;  // const
        public static TMPro.MarkupTag COLOR;  // const
        public static TMPro.MarkupTag SLASH_COLOR;  // const
        public static TMPro.MarkupTag ALPHA;  // const
        public static TMPro.MarkupTag A;  // const
        public static TMPro.MarkupTag SLASH_A;  // const
        public static TMPro.MarkupTag SIZE;  // const
        public static TMPro.MarkupTag SLASH_SIZE;  // const
        public static TMPro.MarkupTag SPRITE;  // const
        public static TMPro.MarkupTag NO_BREAK;  // const
        public static TMPro.MarkupTag SLASH_NO_BREAK;  // const
        public static TMPro.MarkupTag STYLE;  // const
        public static TMPro.MarkupTag SLASH_STYLE;  // const
        public static TMPro.MarkupTag FONT;  // const
        public static TMPro.MarkupTag SLASH_FONT;  // const
        public static TMPro.MarkupTag SLASH_MATERIAL;  // const
        public static TMPro.MarkupTag LINK;  // const
        public static TMPro.MarkupTag SLASH_LINK;  // const
        public static TMPro.MarkupTag FONT_WEIGHT;  // const
        public static TMPro.MarkupTag SLASH_FONT_WEIGHT;  // const
        public static TMPro.MarkupTag NO_PARSE;  // const
        public static TMPro.MarkupTag SLASH_NO_PARSE;  // const
        public static TMPro.MarkupTag POSITION;  // const
        public static TMPro.MarkupTag SLASH_POSITION;  // const
        public static TMPro.MarkupTag VERTICAL_OFFSET;  // const
        public static TMPro.MarkupTag SLASH_VERTICAL_OFFSET;  // const
        public static TMPro.MarkupTag SPACE;  // const
        public static TMPro.MarkupTag SLASH_SPACE;  // const
        public static TMPro.MarkupTag PAGE;  // const
        public static TMPro.MarkupTag SLASH_PAGE;  // const
        public static TMPro.MarkupTag ALIGN;  // const
        public static TMPro.MarkupTag SLASH_ALIGN;  // const
        public static TMPro.MarkupTag WIDTH;  // const
        public static TMPro.MarkupTag SLASH_WIDTH;  // const
        public static TMPro.MarkupTag GRADIENT;  // const
        public static TMPro.MarkupTag SLASH_GRADIENT;  // const
        public static TMPro.MarkupTag CHARACTER_SPACE;  // const
        public static TMPro.MarkupTag SLASH_CHARACTER_SPACE;  // const
        public static TMPro.MarkupTag MONOSPACE;  // const
        public static TMPro.MarkupTag SLASH_MONOSPACE;  // const
        public static TMPro.MarkupTag CLASS;  // const
        public static TMPro.MarkupTag INDENT;  // const
        public static TMPro.MarkupTag SLASH_INDENT;  // const
        public static TMPro.MarkupTag LINE_INDENT;  // const
        public static TMPro.MarkupTag SLASH_LINE_INDENT;  // const
        public static TMPro.MarkupTag MARGIN;  // const
        public static TMPro.MarkupTag SLASH_MARGIN;  // const
        public static TMPro.MarkupTag MARGIN_LEFT;  // const
        public static TMPro.MarkupTag MARGIN_RIGHT;  // const
        public static TMPro.MarkupTag LINE_HEIGHT;  // const
        public static TMPro.MarkupTag SLASH_LINE_HEIGHT;  // const
        public static TMPro.MarkupTag ACTION;  // const
        public static TMPro.MarkupTag SLASH_ACTION;  // const
        public static TMPro.MarkupTag SCALE;  // const
        public static TMPro.MarkupTag SLASH_SCALE;  // const
        public static TMPro.MarkupTag ROTATE;  // const
        public static TMPro.MarkupTag SLASH_ROTATE;  // const
        public static TMPro.MarkupTag LOWERCASE;  // const
        public static TMPro.MarkupTag SLASH_LOWERCASE;  // const
        public static TMPro.MarkupTag ALLCAPS;  // const
        public static TMPro.MarkupTag SLASH_ALLCAPS;  // const
        public static TMPro.MarkupTag UPPERCASE;  // const
        public static TMPro.MarkupTag SLASH_UPPERCASE;  // const
        public static TMPro.MarkupTag SMALLCAPS;  // const
        public static TMPro.MarkupTag SLASH_SMALLCAPS;  // const
        public static TMPro.MarkupTag LIGA;  // const
        public static TMPro.MarkupTag SLASH_LIGA;  // const
        public static TMPro.MarkupTag FRAC;  // const
        public static TMPro.MarkupTag SLASH_FRAC;  // const
        public static TMPro.MarkupTag NAME;  // const
        public static TMPro.MarkupTag INDEX;  // const
        public static TMPro.MarkupTag TINT;  // const
        public static TMPro.MarkupTag ANIM;  // const
        public static TMPro.MarkupTag MATERIAL;  // const
        public static TMPro.MarkupTag HREF;  // const
        public static TMPro.MarkupTag ANGLE;  // const
        public static TMPro.MarkupTag RED;  // const
        public static TMPro.MarkupTag GREEN;  // const
        public static TMPro.MarkupTag BLUE;  // const
        public static TMPro.MarkupTag YELLOW;  // const
        public static TMPro.MarkupTag ORANGE;  // const
        public static TMPro.MarkupTag BLACK;  // const
        public static TMPro.MarkupTag WHITE;  // const
        public static TMPro.MarkupTag PURPLE;  // const
        public static TMPro.MarkupTag BR;  // const
        public static TMPro.MarkupTag ZWSP;  // const
        public static TMPro.MarkupTag NBSP;  // const
        public static TMPro.MarkupTag SHY;  // const
        public static TMPro.MarkupTag LEFT;  // const
        public static TMPro.MarkupTag RIGHT;  // const
        public static TMPro.MarkupTag CENTER;  // const
        public static TMPro.MarkupTag JUSTIFIED;  // const
        public static TMPro.MarkupTag FLUSH;  // const
        public static TMPro.MarkupTag NONE;  // const
        public static TMPro.MarkupTag PLUS;  // const
        public static TMPro.MarkupTag MINUS;  // const
        public static TMPro.MarkupTag PX;  // const
        public static TMPro.MarkupTag PLUS_PX;  // const
        public static TMPro.MarkupTag MINUS_PX;  // const
        public static TMPro.MarkupTag EM;  // const
        public static TMPro.MarkupTag PLUS_EM;  // const
        public static TMPro.MarkupTag MINUS_EM;  // const
        public static TMPro.MarkupTag PCT;  // const
        public static TMPro.MarkupTag PLUS_PCT;  // const
        public static TMPro.MarkupTag MINUS_PCT;  // const
        public static TMPro.MarkupTag PERCENTAGE;  // const
        public static TMPro.MarkupTag PLUS_PERCENTAGE;  // const
        public static TMPro.MarkupTag MINUS_PERCENTAGE;  // const
        public static TMPro.MarkupTag TRUE;  // const
        public static TMPro.MarkupTag FALSE;  // const
        public static TMPro.MarkupTag INVALID;  // const
        public static TMPro.MarkupTag NORMAL;  // const
        public static TMPro.MarkupTag DEFAULT;  // const

    }

    // TypeToken: 0x200007C  // size: 0x14
    public sealed struct TagValueType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.TagValueType None;  // const
        public static TMPro.TagValueType NumericalValue;  // const
        public static TMPro.TagValueType StringValue;  // const
        public static TMPro.TagValueType ColorValue;  // const

    }

    // TypeToken: 0x200007D  // size: 0x14
    public sealed struct TagUnitType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.TagUnitType Pixels;  // const
        public static TMPro.TagUnitType FontUnits;  // const
        public static TMPro.TagUnitType Percentage;  // const

    }

    // TypeToken: 0x200007E  // size: 0x10
    public static class CodePoint
    {
        // Fields
        public static System.UInt32 SPACE;  // const
        public static System.UInt32 DOUBLE_QUOTE;  // const
        public static System.UInt32 NUMBER_SIGN;  // const
        public static System.UInt32 PERCENTAGE;  // const
        public static System.UInt32 PLUS;  // const
        public static System.UInt32 MINUS;  // const
        public static System.UInt32 PERIOD;  // const
        public static System.UInt32 HYPHEN_MINUS;  // const
        public static System.UInt32 SOFT_HYPHEN;  // const
        public static System.UInt32 HYPHEN;  // const
        public static System.UInt32 NON_BREAKING_HYPHEN;  // const
        public static System.UInt32 ZERO_WIDTH_SPACE;  // const
        public static System.UInt32 RIGHT_SINGLE_QUOTATION;  // const
        public static System.UInt32 APOSTROPHE;  // const
        public static System.UInt32 WORD_JOINER;  // const
        public static System.UInt32 HIGH_SURROGATE_START;  // const
        public static System.UInt32 HIGH_SURROGATE_END;  // const
        public static System.UInt32 LOW_SURROGATE_START;  // const
        public static System.UInt32 LOW_SURROGATE_END;  // const
        public static System.UInt32 UNICODE_PLANE01_START;  // const

    }

    // TypeToken: 0x200007F  // size: 0x20
    public class TMP_ScrollbarEventHandler : UnityEngine.MonoBehaviour, UnityEngine.EventSystems.IPointerClickHandler, UnityEngine.EventSystems.IEventSystemHandler, UnityEngine.EventSystems.ISelectHandler, UnityEngine.EventSystems.IDeselectHandler
    {
        // Fields
        public System.Boolean isSelected;  // 0x18

        // Methods
        // RVA: 0x0B32EB34  token: 0x60003FA
        public virtual System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData) { }
        // RVA: 0x0B32EB88  token: 0x60003FB
        public virtual System.Void OnSelect(UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x0B32EADC  token: 0x60003FC
        public virtual System.Void OnDeselect(UnityEngine.EventSystems.BaseEventData eventData) { }
        // RVA: 0x05393520  token: 0x60003FD
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000080  // size: 0xE8
    public class TMP_SelectionCaret : UnityEngine.UI.MaskableGraphic
    {
        // Methods
        // RVA: 0x03519EE0  token: 0x60003FE
        public virtual System.Void Cull(UnityEngine.Rect clipRect, System.Boolean validRect) { }
        // RVA: 0x041E1670  token: 0x60003FF
        protected virtual System.Void UpdateGeometry() { }
        // RVA: 0x04D84AE0  token: 0x6000400
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000081  // size: 0xC0
    public class TMP_Settings : UnityEngine.ScriptableObject
    {
        // Fields
        private static TMPro.TMP_Settings s_Instance;  // static @ 0x0
        private System.Boolean m_enableWordWrapping;  // 0x18
        private System.Boolean m_enableKerning;  // 0x19
        private System.Boolean m_enableExtraPadding;  // 0x1a
        private System.Boolean m_enableTintAllSprites;  // 0x1b
        private System.Boolean m_enableParseEscapeCharacters;  // 0x1c
        private System.Boolean m_EnableRaycastTarget;  // 0x1d
        private System.Boolean m_GetFontFeaturesAtRuntime;  // 0x1e
        private System.Int32 m_missingGlyphCharacter;  // 0x20
        private System.Boolean m_warningsDisabled;  // 0x24
        private TMPro.TMP_FontAsset m_defaultFontAsset;  // 0x28
        private System.String m_defaultFontAssetPath;  // 0x30
        private System.Single m_defaultFontSize;  // 0x38
        private System.Single m_defaultAutoSizeMinRatio;  // 0x3c
        private System.Single m_defaultAutoSizeMaxRatio;  // 0x40
        private UnityEngine.Vector2 m_defaultTextMeshProTextContainerSize;  // 0x44
        private UnityEngine.Vector2 m_defaultTextMeshProUITextContainerSize;  // 0x4c
        private System.Boolean m_autoSizeTextContainer;  // 0x54
        private System.Boolean m_IsTextObjectScaleStatic;  // 0x55
        private System.Collections.Generic.List<TMPro.TMP_FontAsset> m_fallbackFontAssets;  // 0x58
        private System.Boolean m_matchMaterialPreset;  // 0x60
        private TMPro.TMP_SpriteAsset m_defaultSpriteAsset;  // 0x68
        private System.String m_defaultSpriteAssetPath;  // 0x70
        private System.Boolean m_enableEmojiSupport;  // 0x78
        private System.UInt32 m_MissingCharacterSpriteUnicode;  // 0x7c
        private System.String m_defaultColorGradientPresetsPath;  // 0x80
        private TMPro.TMP_StyleSheet m_defaultStyleSheet;  // 0x88
        private System.String m_StyleSheetsResourcePath;  // 0x90
        private UnityEngine.TextAsset m_leadingCharacters;  // 0x98
        private UnityEngine.TextAsset m_followingCharacters;  // 0xa0
        private TMPro.TMP_Settings.LineBreakingTable m_linebreakingRules;  // 0xa8
        private System.Boolean m_UseModernHangulLineBreakingRules;  // 0xb0
        public TMPro.IFontAssetLoader dynamicFontAssetLoader;  // 0xb8

        // Properties
        System.String version { get; /* RVA: 0x0B32F324 */ }
        System.Boolean enableWordWrapping { get; /* RVA: 0x0B32F1B4 */ }
        System.Boolean enableKerning { get; /* RVA: 0x0B32F134 */ }
        System.Boolean enableExtraPadding { get; /* RVA: 0x0B32F114 */ }
        System.Boolean enableTintAllSprites { get; /* RVA: 0x0B32F194 */ }
        System.Boolean enableParseEscapeCharacters { get; /* RVA: 0x0B32F154 */ }
        System.Boolean enableRaycastTarget { get; /* RVA: 0x0B32F174 */ }
        System.Boolean getFontFeaturesAtRuntime { get; /* RVA: 0x0B32F218 */ }
        System.Int32 missingGlyphCharacter { get; /* RVA: 0x0B32F2BC */ set; /* RVA: 0x0B32F3DC */ }
        System.Boolean warningsDisabled { get; /* RVA: 0x0B32F350 */ }
        TMPro.TMP_FontAsset defaultFontAsset { get; /* RVA: 0x0B32EFD4 */ }
        System.String defaultFontAssetPath { get; /* RVA: 0x0B32EFB4 */ }
        System.Single defaultFontSize { get; /* RVA: 0x0B32EFF4 */ }
        System.Single defaultTextAutoSizingMinRatio { get; /* RVA: 0x0B32F074 */ }
        System.Single defaultTextAutoSizingMaxRatio { get; /* RVA: 0x0B32F054 */ }
        UnityEngine.Vector2 defaultTextMeshProTextContainerSize { get; /* RVA: 0x0B32F094 */ }
        UnityEngine.Vector2 defaultTextMeshProUITextContainerSize { get; /* RVA: 0x0B32F0C4 */ }
        System.Boolean autoSizeTextContainer { get; /* RVA: 0x0B32EF70 */ }
        System.Boolean isTextObjectScaleStatic { get; /* RVA: 0x0B32F238 */ set; /* RVA: 0x0B32F394 */ }
        System.Collections.Generic.List<TMPro.TMP_FontAsset> fallbackFontAssets { get; /* RVA: 0x0B32F1D4 */ }
        System.Boolean matchMaterialPreset { get; /* RVA: 0x0B32F27C */ }
        TMPro.TMP_SpriteAsset defaultSpriteAsset { get; /* RVA: 0x0B32F034 */ }
        System.String defaultSpriteAssetPath { get; /* RVA: 0x0B32F014 */ }
        System.Boolean enableEmojiSupport { get; /* RVA: 0x0B32F0F4 */ set; /* RVA: 0x0B32F370 */ }
        System.UInt32 missingCharacterSpriteUnicode { get; /* RVA: 0x0B32F29C */ set; /* RVA: 0x0B32F3B8 */ }
        System.String defaultColorGradientPresetsPath { get; /* RVA: 0x0B32EF90 */ }
        TMPro.TMP_StyleSheet defaultStyleSheet { get; /* RVA: 0x0333CC00 */ }
        System.String styleSheetsResourcePath { get; /* RVA: 0x0B32F2DC */ }
        UnityEngine.TextAsset leadingCharacters { get; /* RVA: 0x0B32F258 */ }
        UnityEngine.TextAsset followingCharacters { get; /* RVA: 0x0B32F1F4 */ }
        TMPro.TMP_Settings.LineBreakingTable linebreakingRules { get; /* RVA: 0x0333CC30 */ }
        System.Boolean useModernHangulLineBreakingRules { get; /* RVA: 0x0B32F300 */ set; /* RVA: 0x0B32F400 */ }
        TMPro.TMP_Settings instance { get; /* RVA: 0x0333CC80 */ }

        // Methods
        // RVA: 0x0B32EE8C  token: 0x6000427
        public static TMPro.TMP_Settings LoadDefaultSettings() { }
        // RVA: 0x0B32ED54  token: 0x6000428
        public static TMPro.TMP_Settings GetSettings() { }
        // RVA: 0x0B32ECE8  token: 0x6000429
        public static TMPro.TMP_FontAsset GetFontAsset() { }
        // RVA: 0x0B32EBE0  token: 0x600042A
        public static System.Void ClearDefaultFontAsset() { }
        // RVA: 0x0B32EDB4  token: 0x600042B
        public static TMPro.TMP_SpriteAsset GetSpriteAsset() { }
        // RVA: 0x0B32EE20  token: 0x600042C
        public static TMPro.TMP_StyleSheet GetStyleSheet() { }
        // RVA: 0x04378C70  token: 0x600042D
        public static System.Void LoadLinebreakingRules() { }
        // RVA: 0x0B32EC7C  token: 0x600042E
        public static TMPro.IFontAssetLoader GetDynamicFallbackFontAssetLoader() { }
        // RVA: 0x04378DF0  token: 0x600042F
        private static System.Collections.Generic.Dictionary<System.Int32,System.Char> GetCharacters(UnityEngine.TextAsset file) { }
        // RVA: 0x04D84AD0  token: 0x6000430
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000083  // size: 0x10
    public static class ShaderUtilities
    {
        // Fields
        public static System.Int32 ID_MainTex;  // static @ 0x0
        public static System.Int32 ID_FaceTex;  // static @ 0x4
        public static System.Int32 ID_FaceColor;  // static @ 0x8
        public static System.Int32 ID_FaceDilate;  // static @ 0xc
        public static System.Int32 ID_Shininess;  // static @ 0x10
        public static System.Int32 ID_UnderlayColor;  // static @ 0x14
        public static System.Int32 ID_UnderlayOffsetX;  // static @ 0x18
        public static System.Int32 ID_UnderlayOffsetY;  // static @ 0x1c
        public static System.Int32 ID_UnderlayDilate;  // static @ 0x20
        public static System.Int32 ID_UnderlaySoftness;  // static @ 0x24
        public static System.Int32 ID_UnderlayOffset;  // static @ 0x28
        public static System.Int32 ID_UnderlayIsoPerimeter;  // static @ 0x2c
        public static System.Int32 ID_WeightNormal;  // static @ 0x30
        public static System.Int32 ID_WeightBold;  // static @ 0x34
        public static System.Int32 ID_OutlineTex;  // static @ 0x38
        public static System.Int32 ID_OutlineWidth;  // static @ 0x3c
        public static System.Int32 ID_OutlineSoftness;  // static @ 0x40
        public static System.Int32 ID_OutlineColor;  // static @ 0x44
        public static System.Int32 ID_Outline2Color;  // static @ 0x48
        public static System.Int32 ID_Outline2Width;  // static @ 0x4c
        public static System.Int32 ID_Padding;  // static @ 0x50
        public static System.Int32 ID_GradientScale;  // static @ 0x54
        public static System.Int32 ID_ScaleX;  // static @ 0x58
        public static System.Int32 ID_ScaleY;  // static @ 0x5c
        public static System.Int32 ID_PerspectiveFilter;  // static @ 0x60
        public static System.Int32 ID_Sharpness;  // static @ 0x64
        public static System.Int32 ID_TextureWidth;  // static @ 0x68
        public static System.Int32 ID_TextureHeight;  // static @ 0x6c
        public static System.Int32 ID_BevelAmount;  // static @ 0x70
        public static System.Int32 ID_GlowColor;  // static @ 0x74
        public static System.Int32 ID_GlowOffset;  // static @ 0x78
        public static System.Int32 ID_GlowPower;  // static @ 0x7c
        public static System.Int32 ID_GlowOuter;  // static @ 0x80
        public static System.Int32 ID_GlowInner;  // static @ 0x84
        public static System.Int32 ID_LightAngle;  // static @ 0x88
        public static System.Int32 ID_EnvMap;  // static @ 0x8c
        public static System.Int32 ID_EnvMatrix;  // static @ 0x90
        public static System.Int32 ID_EnvMatrixRotation;  // static @ 0x94
        public static System.Int32 ID_MaskCoord;  // static @ 0x98
        public static System.Int32 ID_ClipRect;  // static @ 0x9c
        public static System.Int32 ID_MaskSoftnessX;  // static @ 0xa0
        public static System.Int32 ID_MaskSoftnessY;  // static @ 0xa4
        public static System.Int32 ID_VertexOffsetX;  // static @ 0xa8
        public static System.Int32 ID_VertexOffsetY;  // static @ 0xac
        public static System.Int32 ID_UseClipRect;  // static @ 0xb0
        public static System.Int32 ID_StencilID;  // static @ 0xb4
        public static System.Int32 ID_StencilOp;  // static @ 0xb8
        public static System.Int32 ID_StencilComp;  // static @ 0xbc
        public static System.Int32 ID_StencilReadMask;  // static @ 0xc0
        public static System.Int32 ID_StencilWriteMask;  // static @ 0xc4
        public static System.Int32 ID_ShaderFlags;  // static @ 0xc8
        public static System.Int32 ID_ScaleRatio_A;  // static @ 0xcc
        public static System.Int32 ID_ScaleRatio_B;  // static @ 0xd0
        public static System.Int32 ID_ScaleRatio_C;  // static @ 0xd4
        public static System.Int32 ID_UseClipSceneDepth;  // static @ 0xd8
        public static System.Int32 ID_UsedForWorldUI;  // static @ 0xdc
        public static System.String Keyword_ClipSceneDepth;  // static @ 0xe0
        public static System.String Keyword_UsedForWorldUI;  // static @ 0xe8
        public static System.String Keyword_Bevel;  // static @ 0xf0
        public static System.String Keyword_Glow;  // static @ 0xf8
        public static System.String Keyword_Underlay;  // static @ 0x100
        public static System.String Keyword_Ratios;  // static @ 0x108
        public static System.String Keyword_MASK_SOFT;  // static @ 0x110
        public static System.String Keyword_MASK_HARD;  // static @ 0x118
        public static System.String Keyword_MASK_TEX;  // static @ 0x120
        public static System.String Keyword_Outline;  // static @ 0x128
        public static System.String ShaderTag_ZTestMode;  // static @ 0x130
        public static System.String ShaderTag_CullMode;  // static @ 0x138
        private static System.Single m_clamp;  // static @ 0x140
        public static System.Boolean isInitialized;  // static @ 0x144
        private static UnityEngine.Shader k_ShaderRef_MobileSDF;  // static @ 0x148
        private static UnityEngine.Shader k_ShaderRef_MobileBitmap;  // static @ 0x150

        // Properties
        UnityEngine.Shader ShaderRef_MobileSDF { get; /* RVA: 0x0B32C494 */ }
        UnityEngine.Shader ShaderRef_MobileBitmap { get; /* RVA: 0x0B32C3A8 */ }

        // Methods
        // RVA: 0x048E34D0  token: 0x6000434
        private static System.Void .cctor() { }
        // RVA: 0x0333A000  token: 0x6000435
        public static System.Void GetShaderPropertyIDs() { }
        // RVA: 0x0339F6A0  token: 0x6000436
        public static System.Void UpdateShaderRatios(UnityEngine.Material mat) { }
        // RVA: 0x0B32B8FC  token: 0x6000437
        public static UnityEngine.Vector4 GetFontExtent(UnityEngine.Material material) { }
        // RVA: 0x0333A910  token: 0x6000438
        public static System.Boolean IsMaskingEnabled(UnityEngine.Material material) { }
        // RVA: 0x0339E950  token: 0x6000439
        public static System.Single GetPadding(UnityEngine.Material material, System.Boolean enableExtraPadding, System.Boolean isBold) { }
        // RVA: 0x0B32B91C  token: 0x600043A
        public static System.Single GetPadding(UnityEngine.Material[] materials, System.Boolean enableExtraPadding, System.Boolean isBold) { }

    }

    // TypeToken: 0x2000084  // size: 0x58
    public class TMP_Sprite : TMPro.TMP_TextElement_Legacy
    {
        // Fields
        public System.String name;  // 0x38
        public System.Int32 hashCode;  // 0x40
        public System.Int32 unicode;  // 0x44
        public UnityEngine.Vector2 pivot;  // 0x48
        public UnityEngine.Sprite sprite;  // 0x50

        // Methods
        // RVA: 0x041E1670  token: 0x600043B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000085  // size: 0x28
    public class TMP_SpriteAnimator : UnityEngine.MonoBehaviour
    {
        // Fields
        private System.Collections.Generic.Dictionary<System.Int32,System.Boolean> m_animations;  // 0x18
        private TMPro.TMP_Text m_TextComponent;  // 0x20

        // Methods
        // RVA: 0x0B32F428  token: 0x600043C
        private System.Void Awake() { }
        // RVA: 0x041E1670  token: 0x600043D
        private System.Void OnEnable() { }
        // RVA: 0x041E1670  token: 0x600043E
        private System.Void OnDisable() { }
        // RVA: 0x0B32F5EC  token: 0x600043F
        public System.Void StopAllAnimations() { }
        // RVA: 0x0B32F514  token: 0x6000440
        public System.Void DoSpriteAnimation(System.Int32 currentCharacter, TMPro.TMP_SpriteAsset spriteAsset, System.Int32 start, System.Int32 end, System.Int32 framerate) { }
        // RVA: 0x0B32F470  token: 0x6000441
        private System.Collections.IEnumerator DoSpriteAnimationInternal(System.Int32 currentCharacter, TMPro.TMP_SpriteAsset spriteAsset, System.Int32 start, System.Int32 end, System.Int32 framerate) { }
        // RVA: 0x0B32F63C  token: 0x6000442
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000087  // size: 0xE8
    public class TMP_SpriteAsset : TMPro.TMP_Asset
    {
        // Fields
        private System.Collections.Generic.Dictionary<System.Int32,System.Int32> m_NameLookup;  // 0x30
        private System.Collections.Generic.Dictionary<System.UInt32,System.Int32> m_GlyphIndexLookup;  // 0x38
        private System.String m_Version;  // 0x40
        private UnityEngine.TextCore.FaceInfo m_FaceInfo;  // 0x48
        public UnityEngine.Texture spriteSheet;  // 0xa8
        private System.Collections.Generic.List<TMPro.TMP_SpriteCharacter> m_SpriteCharacterTable;  // 0xb0
        private System.Collections.Generic.Dictionary<System.UInt32,TMPro.TMP_SpriteCharacter> m_SpriteCharacterLookup;  // 0xb8
        private System.Collections.Generic.List<TMPro.TMP_SpriteGlyph> m_SpriteGlyphTable;  // 0xc0
        private System.Collections.Generic.Dictionary<System.UInt32,TMPro.TMP_SpriteGlyph> m_SpriteGlyphLookup;  // 0xc8
        public System.Collections.Generic.List<TMPro.TMP_Sprite> spriteInfoList;  // 0xd0
        public System.Collections.Generic.List<TMPro.TMP_SpriteAsset> fallbackSpriteAssets;  // 0xd8
        private System.Boolean m_IsSpriteAssetLookupTablesDirty;  // 0xe0
        private static System.Collections.Generic.HashSet<System.Int32> k_searchedSpriteAssets;  // static @ 0x0

        // Properties
        System.String version { get; /* RVA: 0x04D85A60 */ set; /* RVA: 0x042B4AE0 */ }
        UnityEngine.TextCore.FaceInfo faceInfo { get; /* RVA: 0x04DBA360 */ set; /* RVA: 0x0B330F50 */ }
        System.Collections.Generic.List<TMPro.TMP_SpriteCharacter> spriteCharacterTable { get; /* RVA: 0x0B330F20 */ set; /* RVA: 0x04D84CE0 */ }
        System.Collections.Generic.Dictionary<System.UInt32,TMPro.TMP_SpriteCharacter> spriteCharacterLookupTable { get; /* RVA: 0x0B330EF0 */ set; /* RVA: 0x06AC5AF4 */ }
        System.Collections.Generic.List<TMPro.TMP_SpriteGlyph> spriteGlyphTable { get; /* RVA: 0x04D892F0 */ set; /* RVA: 0x06AC5ACC */ }

        // Methods
        // RVA: 0x0B32F6C0  token: 0x6000453
        private System.Void Awake() { }
        // RVA: 0x0B32F730  token: 0x6000454
        private UnityEngine.Material GetDefaultSpriteMaterial() { }
        // RVA: 0x0B3305B0  token: 0x6000455
        public System.Void UpdateLookupTables() { }
        // RVA: 0x0B32F808  token: 0x6000456
        public System.Int32 GetSpriteIndexFromHashcode(System.Int32 hashCode) { }
        // RVA: 0x0B32F8F0  token: 0x6000457
        public System.Int32 GetSpriteIndexFromUnicode(System.UInt32 unicode) { }
        // RVA: 0x0B32F884  token: 0x6000458
        public System.Int32 GetSpriteIndexFromName(System.String name) { }
        // RVA: 0x0B3300A0  token: 0x6000459
        public static TMPro.TMP_SpriteAsset SearchForSpriteByUnicode(TMPro.TMP_SpriteAsset spriteAsset, System.UInt32 unicode, System.Boolean includeFallbacks, System.Int32& spriteIndex) { }
        // RVA: 0x0B32FE98  token: 0x600045A
        private static TMPro.TMP_SpriteAsset SearchForSpriteByUnicodeInternal(System.Collections.Generic.List<TMPro.TMP_SpriteAsset> spriteAssets, System.UInt32 unicode, System.Boolean includeFallbacks, System.Int32& spriteIndex) { }
        // RVA: 0x0B32FFF0  token: 0x600045B
        private static TMPro.TMP_SpriteAsset SearchForSpriteByUnicodeInternal(TMPro.TMP_SpriteAsset spriteAsset, System.UInt32 unicode, System.Boolean includeFallbacks, System.Int32& spriteIndex) { }
        // RVA: 0x0B32FB84  token: 0x600045C
        public static TMPro.TMP_SpriteAsset SearchForSpriteByHashCode(TMPro.TMP_SpriteAsset spriteAsset, System.Int32 hashCode, System.Boolean includeFallbacks, System.Int32& spriteIndex) { }
        // RVA: 0x0B32FA2C  token: 0x600045D
        private static TMPro.TMP_SpriteAsset SearchForSpriteByHashCodeInternal(System.Collections.Generic.List<TMPro.TMP_SpriteAsset> spriteAssets, System.Int32 hashCode, System.Boolean searchFallbacks, System.Int32& spriteIndex) { }
        // RVA: 0x0B32F97C  token: 0x600045E
        private static TMPro.TMP_SpriteAsset SearchForSpriteByHashCodeInternal(TMPro.TMP_SpriteAsset spriteAsset, System.Int32 hashCode, System.Boolean searchFallbacks, System.Int32& spriteIndex) { }
        // RVA: 0x0B330444  token: 0x600045F
        public System.Void SortGlyphTable() { }
        // RVA: 0x0B3302B8  token: 0x6000460
        private System.Void SortCharacterTable() { }
        // RVA: 0x0B330424  token: 0x6000461
        private System.Void SortGlyphAndCharacterTables() { }
        // RVA: 0x0B330AEC  token: 0x6000462
        private System.Void UpgradeSpriteAsset() { }
        // RVA: 0x0B330E1C  token: 0x6000463
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000089  // size: 0x40
    public class TMP_SpriteCharacter : TMPro.TMP_TextElement
    {
        // Fields
        private System.String m_Name;  // 0x30
        private System.Int32 m_HashCode;  // 0x38

        // Properties
        System.String name { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x0B331054 */ }
        System.Int32 hashCode { get; /* RVA: 0x04D863B0 */ }

        // Methods
        // RVA: 0x04DBA3A0  token: 0x600046B
        public System.Void .ctor() { }
        // RVA: 0x0B330FD0  token: 0x600046C
        public System.Void .ctor(System.UInt32 unicode, TMPro.TMP_SpriteGlyph glyph) { }
        // RVA: 0x0B33100C  token: 0x600046D
        public System.Void .ctor(System.UInt32 unicode, TMPro.TMP_SpriteAsset spriteAsset, TMPro.TMP_SpriteGlyph glyph) { }
        // RVA: 0x0B330F90  token: 0x600046E
        private System.Void .ctor(System.UInt32 unicode, System.UInt32 glyphIndex) { }

    }

    // TypeToken: 0x200008A  // size: 0x50
    public class TMP_SpriteGlyph : UnityEngine.TextCore.Glyph
    {
        // Fields
        public UnityEngine.Sprite sprite;  // 0x48

        // Methods
        // RVA: 0x0B3311A8  token: 0x600046F
        public System.Void .ctor() { }
        // RVA: 0x0B331140  token: 0x6000470
        public System.Void .ctor(System.UInt32 index, UnityEngine.TextCore.GlyphMetrics metrics, UnityEngine.TextCore.GlyphRect glyphRect, System.Single scale, System.Int32 atlasIndex) { }
        // RVA: 0x0B3310C8  token: 0x6000471
        public System.Void .ctor(System.UInt32 index, UnityEngine.TextCore.GlyphMetrics metrics, UnityEngine.TextCore.GlyphRect glyphRect, System.Single scale, System.Int32 atlasIndex, UnityEngine.Sprite sprite) { }

    }

    // TypeToken: 0x200008B  // size: 0x50
    public class TMP_Style
    {
        // Fields
        private static TMPro.TMP_Style k_NormalStyle;  // static @ 0x0
        private System.String m_Name;  // 0x10
        private System.Int32 m_HashCode;  // 0x18
        private System.String m_OpeningDefinition;  // 0x20
        private System.String m_ClosingDefinition;  // 0x28
        private System.Int32[] m_OpeningTagArray;  // 0x30
        private System.Int32[] m_ClosingTagArray;  // 0x38
        private System.UInt32[] m_OpeningTagUnicodeArray;  // 0x40
        private System.UInt32[] m_ClosingTagUnicodeArray;  // 0x48

        // Properties
        TMPro.TMP_Style NormalStyle { get; /* RVA: 0x0B331254 */ }
        System.String name { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x0B33133C */ }
        System.Int32 hashCode { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x0B331330 */ }
        System.String styleOpeningDefinition { get; /* RVA: 0x04D862C0 */ }
        System.String styleClosingDefinition { get; /* RVA: 0x04D86240 */ }
        System.Int32[] styleOpeningTagArray { get; /* RVA: 0x011F36E0 */ }
        System.Int32[] styleClosingTagArray { get; /* RVA: 0x04D85A50 */ }

        // Methods
        // RVA: 0x04139E70  token: 0x600047B
        private System.Void .ctor(System.String styleName, System.String styleOpeningDefinition, System.String styleClosingDefinition) { }
        // RVA: 0x0413A1E0  token: 0x600047C
        public System.Void RefreshStyle() { }

    }

    // TypeToken: 0x200008C  // size: 0x28
    public class TMP_StyleSheet : UnityEngine.ScriptableObject
    {
        // Fields
        private System.Collections.Generic.List<TMPro.TMP_Style> m_StyleList;  // 0x18
        private System.Collections.Generic.Dictionary<System.Int32,TMPro.TMP_Style> m_StyleLookupDictionary;  // 0x20

        // Properties
        System.Collections.Generic.List<TMPro.TMP_Style> styles { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x0B33124C  token: 0x600047E
        private System.Void Reset() { }
        // RVA: 0x0333CD60  token: 0x600047F
        public TMPro.TMP_Style GetStyle(System.Int32 hashCode) { }
        // RVA: 0x0B3311B0  token: 0x6000480
        public TMPro.TMP_Style GetStyle(System.String name) { }
        // RVA: 0x0B33124C  token: 0x6000481
        public System.Void RefreshStyles() { }
        // RVA: 0x04139F20  token: 0x6000482
        private System.Void LoadStyleDictionaryInternal() { }
        // RVA: 0x04D1E550  token: 0x6000483
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200008D  // size: 0x78
    public class TMP_SubMesh : UnityEngine.MonoBehaviour
    {
        // Fields
        private TMPro.TMP_FontAsset m_fontAsset;  // 0x18
        private TMPro.TMP_SpriteAsset m_spriteAsset;  // 0x20
        private UnityEngine.Material m_material;  // 0x28
        private UnityEngine.Material m_sharedMaterial;  // 0x30
        private UnityEngine.Material m_fallbackMaterial;  // 0x38
        private UnityEngine.Material m_fallbackSourceMaterial;  // 0x40
        private System.Boolean m_isDefaultMaterial;  // 0x48
        private System.Single m_padding;  // 0x4c
        private UnityEngine.Renderer m_renderer;  // 0x50
        private UnityEngine.MeshFilter m_meshFilter;  // 0x58
        private UnityEngine.Mesh m_mesh;  // 0x60
        private TMPro.TextMeshPro m_TextComponent;  // 0x68
        private System.Boolean m_isRegisteredForEvents;  // 0x70

        // Properties
        TMPro.TMP_FontAsset fontAsset { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        TMPro.TMP_SpriteAsset spriteAsset { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        UnityEngine.Material material { get; /* RVA: 0x0B333250 */ set; /* RVA: 0x0B33360C */ }
        UnityEngine.Material sharedMaterial { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x0B33369C */ }
        UnityEngine.Material fallbackMaterial { get; /* RVA: 0x04D85A50 */ set; /* RVA: 0x0B333504 */ }
        UnityEngine.Material fallbackSourceMaterial { get; /* RVA: 0x04D85A60 */ set; /* RVA: 0x042B4AE0 */ }
        System.Boolean isDefaultMaterial { get; /* RVA: 0x04D867D0 */ set; /* RVA: 0x04D86810 */ }
        System.Single padding { get; /* RVA: 0x04D87860 */ set; /* RVA: 0x04D91480 */ }
        UnityEngine.Renderer renderer { get; /* RVA: 0x0B3333FC */ }
        UnityEngine.MeshFilter meshFilter { get; /* RVA: 0x0B33325C */ }
        UnityEngine.Mesh mesh { get; /* RVA: 0x0B333350 */ set; /* RVA: 0x063CC164 */ }
        TMPro.TMP_Text textComponent { get; /* RVA: 0x0B333480 */ }

        // Methods
        // RVA: 0x0B33263C  token: 0x6000499
        public static TMPro.TMP_SubMesh AddSubTextObject(TMPro.TextMeshPro textComponent, TMPro.MaterialReference materialReference) { }
        // RVA: 0x0B332EA4  token: 0x600049A
        private System.Void OnEnable() { }
        // RVA: 0x0B332E00  token: 0x600049B
        private System.Void OnDisable() { }
        // RVA: 0x0B332CE8  token: 0x600049C
        private System.Void OnDestroy() { }
        // RVA: 0x0B332A50  token: 0x600049D
        public System.Void DestroySelf() { }
        // RVA: 0x0B332AA8  token: 0x600049E
        private UnityEngine.Material GetMaterial(UnityEngine.Material mat) { }
        // RVA: 0x0B33299C  token: 0x600049F
        private UnityEngine.Material CreateMaterialInstance(UnityEngine.Material source) { }
        // RVA: 0x0B332C54  token: 0x60004A0
        private UnityEngine.Material GetSharedMaterial() { }
        // RVA: 0x0B332FB0  token: 0x60004A1
        private System.Void SetSharedMaterial(UnityEngine.Material mat) { }
        // RVA: 0x0B332BD4  token: 0x60004A2
        public System.Single GetPaddingForMaterial() { }
        // RVA: 0x0B3331DC  token: 0x60004A3
        public System.Void UpdateMeshPadding(System.Boolean isExtraPadding, System.Boolean isUsingBold) { }
        // RVA: 0x0B332FE4  token: 0x60004A4
        public System.Void SetVerticesDirty() { }
        // RVA: 0x0B332FA8  token: 0x60004A5
        public System.Void SetMaterialDirty() { }
        // RVA: 0x0B333074  token: 0x60004A6
        protected System.Void UpdateMaterial() { }
        // RVA: 0x05393520  token: 0x60004A7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200008E  // size: 0x140
    public class TMP_SubMeshUI : UnityEngine.UI.MaskableGraphic
    {
        // Fields
        private TMPro.TMP_FontAsset m_fontAsset;  // 0xe8
        private TMPro.TMP_SpriteAsset m_spriteAsset;  // 0xf0
        private UnityEngine.Material m_material;  // 0xf8
        private UnityEngine.Material m_sharedMaterial;  // 0x100
        private UnityEngine.Material m_fallbackMaterial;  // 0x108
        private UnityEngine.Material m_fallbackSourceMaterial;  // 0x110
        private System.Boolean m_isDefaultMaterial;  // 0x118
        private System.Single m_padding;  // 0x11c
        private UnityEngine.Mesh m_mesh;  // 0x120
        private TMPro.TextMeshProUGUI m_TextComponent;  // 0x128
        private System.Boolean m_isRegisteredForEvents;  // 0x130
        private System.Boolean m_materialDirty;  // 0x131
        private System.Int32 m_materialReferenceIndex;  // 0x134
        private UnityEngine.Transform m_RootCanvasTransform;  // 0x138

        // Properties
        TMPro.TMP_FontAsset fontAsset { get; /* RVA: 0x04D88590 */ set; /* RVA: 0x06AC5B1C */ }
        TMPro.TMP_SpriteAsset spriteAsset { get; /* RVA: 0x04D8D1A0 */ set; /* RVA: 0x06AC5AE0 */ }
        UnityEngine.Texture mainTexture { get; /* RVA: 0x0B3321F4 */ }
        UnityEngine.Material material { get; /* RVA: 0x0B3322F8 */ set; /* RVA: 0x0B33254C */ }
        UnityEngine.Material sharedMaterial { get; /* RVA: 0x04D87D50 */ set; /* RVA: 0x0B332634 */ }
        UnityEngine.Material fallbackMaterial { get; /* RVA: 0x04D85F90 */ set; /* RVA: 0x0B33242C */ }
        UnityEngine.Material fallbackSourceMaterial { get; /* RVA: 0x04D8D1B0 */ set; /* RVA: 0x05397170 */ }
        UnityEngine.Material materialForRendering { get; /* RVA: 0x0B332298 */ }
        System.Boolean isDefaultMaterial { get; /* RVA: 0x04D92E30 */ set; /* RVA: 0x04D92E40 */ }
        System.Single padding { get; /* RVA: 0x04DBA3B0 */ set; /* RVA: 0x04DBA3C0 */ }
        UnityEngine.Mesh mesh { get; /* RVA: 0x0B332308 */ set; /* RVA: 0x053971C0 */ }
        TMPro.TMP_Text textComponent { get; /* RVA: 0x0B33239C */ }

        // Methods
        // RVA: 0x0B331378  token: 0x60004BD
        public static TMPro.TMP_SubMeshUI AddSubTextObject(TMPro.TextMeshProUGUI textComponent, TMPro.MaterialReference materialReference) { }
        // RVA: 0x0B331DB4  token: 0x60004BE
        protected virtual System.Void OnEnable() { }
        // RVA: 0x0B331D14  token: 0x60004BF
        protected virtual System.Void OnDisable() { }
        // RVA: 0x0B331B90  token: 0x60004C0
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x0B331E0C  token: 0x60004C1
        protected virtual System.Void OnTransformParentChanged() { }
        // RVA: 0x0B33187C  token: 0x60004C2
        public virtual UnityEngine.Material GetModifiedMaterial(UnityEngine.Material baseMaterial) { }
        // RVA: 0x0B331A34  token: 0x60004C3
        public System.Single GetPaddingForMaterial() { }
        // RVA: 0x0B3319B0  token: 0x60004C4
        public System.Single GetPaddingForMaterial(UnityEngine.Material mat) { }
        // RVA: 0x0B332178  token: 0x60004C5
        public System.Void UpdateMeshPadding(System.Boolean isExtraPadding, System.Boolean isUsingBold) { }
        // RVA: 0x041E1670  token: 0x60004C6
        public virtual System.Void SetAllDirty() { }
        // RVA: 0x0B331F80  token: 0x60004C7
        public virtual System.Void SetVerticesDirty() { }
        // RVA: 0x041E1670  token: 0x60004C8
        public virtual System.Void SetLayoutDirty() { }
        // RVA: 0x0B331E80  token: 0x60004C9
        public virtual System.Void SetMaterialDirty() { }
        // RVA: 0x0B331EB8  token: 0x60004CA
        public System.Void SetPivotDirty() { }
        // RVA: 0x0B331ABC  token: 0x60004CB
        private UnityEngine.Transform GetRootCanvasTransform() { }
        // RVA: 0x041E1670  token: 0x60004CC
        public virtual System.Void Cull(UnityEngine.Rect clipRect, System.Boolean validRect) { }
        // RVA: 0x041E1670  token: 0x60004CD
        protected virtual System.Void UpdateGeometry() { }
        // RVA: 0x0B331E50  token: 0x60004CE
        public virtual System.Void Rebuild(UnityEngine.UI.CanvasUpdate update) { }
        // RVA: 0x0A973704  token: 0x60004CF
        public System.Void RefreshMaterial() { }
        // RVA: 0x0B33201C  token: 0x60004D0
        protected virtual System.Void UpdateMaterial() { }
        // RVA: 0x03F59BE0  token: 0x60004D1
        public virtual System.Void RecalculateClipping() { }
        // RVA: 0x04D87D50  token: 0x60004D2
        private UnityEngine.Material GetMaterial() { }
        // RVA: 0x0B331770  token: 0x60004D3
        private UnityEngine.Material GetMaterial(UnityEngine.Material mat) { }
        // RVA: 0x0B3316BC  token: 0x60004D4
        private UnityEngine.Material CreateMaterialInstance(UnityEngine.Material source) { }
        // RVA: 0x0B331B68  token: 0x60004D5
        private UnityEngine.Material GetSharedMaterial() { }
        // RVA: 0x0B331F2C  token: 0x60004D6
        private System.Void SetSharedMaterial(UnityEngine.Material mat) { }
        // RVA: 0x04D84AE0  token: 0x60004D7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200008F
    public interface ITextElement
    {
        // Properties
        UnityEngine.Material sharedMaterial { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x60004D9
        public virtual System.Void Rebuild(UnityEngine.UI.CanvasUpdate update) { }
        // RVA: -1  // abstract  token: 0x60004DA
        public virtual System.Int32 GetInstanceID() { }

    }

    // TypeToken: 0x2000090  // size: 0x14
    public sealed struct TextAlignmentOptions
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.TextAlignmentOptions TopLeft;  // const
        public static TMPro.TextAlignmentOptions Top;  // const
        public static TMPro.TextAlignmentOptions TopRight;  // const
        public static TMPro.TextAlignmentOptions TopJustified;  // const
        public static TMPro.TextAlignmentOptions TopFlush;  // const
        public static TMPro.TextAlignmentOptions TopGeoAligned;  // const
        public static TMPro.TextAlignmentOptions Left;  // const
        public static TMPro.TextAlignmentOptions Center;  // const
        public static TMPro.TextAlignmentOptions Right;  // const
        public static TMPro.TextAlignmentOptions Justified;  // const
        public static TMPro.TextAlignmentOptions Flush;  // const
        public static TMPro.TextAlignmentOptions CenterGeoAligned;  // const
        public static TMPro.TextAlignmentOptions BottomLeft;  // const
        public static TMPro.TextAlignmentOptions Bottom;  // const
        public static TMPro.TextAlignmentOptions BottomRight;  // const
        public static TMPro.TextAlignmentOptions BottomJustified;  // const
        public static TMPro.TextAlignmentOptions BottomFlush;  // const
        public static TMPro.TextAlignmentOptions BottomGeoAligned;  // const
        public static TMPro.TextAlignmentOptions BaselineLeft;  // const
        public static TMPro.TextAlignmentOptions Baseline;  // const
        public static TMPro.TextAlignmentOptions BaselineRight;  // const
        public static TMPro.TextAlignmentOptions BaselineJustified;  // const
        public static TMPro.TextAlignmentOptions BaselineFlush;  // const
        public static TMPro.TextAlignmentOptions BaselineGeoAligned;  // const
        public static TMPro.TextAlignmentOptions MidlineLeft;  // const
        public static TMPro.TextAlignmentOptions Midline;  // const
        public static TMPro.TextAlignmentOptions MidlineRight;  // const
        public static TMPro.TextAlignmentOptions MidlineJustified;  // const
        public static TMPro.TextAlignmentOptions MidlineFlush;  // const
        public static TMPro.TextAlignmentOptions MidlineGeoAligned;  // const
        public static TMPro.TextAlignmentOptions CaplineLeft;  // const
        public static TMPro.TextAlignmentOptions Capline;  // const
        public static TMPro.TextAlignmentOptions CaplineRight;  // const
        public static TMPro.TextAlignmentOptions CaplineJustified;  // const
        public static TMPro.TextAlignmentOptions CaplineFlush;  // const
        public static TMPro.TextAlignmentOptions CaplineGeoAligned;  // const
        public static TMPro.TextAlignmentOptions Converted;  // const

    }

    // TypeToken: 0x2000091  // size: 0x14
    public sealed struct HorizontalAlignmentOptions
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.HorizontalAlignmentOptions Left;  // const
        public static TMPro.HorizontalAlignmentOptions Center;  // const
        public static TMPro.HorizontalAlignmentOptions Right;  // const
        public static TMPro.HorizontalAlignmentOptions Justified;  // const
        public static TMPro.HorizontalAlignmentOptions Flush;  // const
        public static TMPro.HorizontalAlignmentOptions Geometry;  // const

    }

    // TypeToken: 0x2000092  // size: 0x14
    public sealed struct VerticalAlignmentOptions
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.VerticalAlignmentOptions Top;  // const
        public static TMPro.VerticalAlignmentOptions Middle;  // const
        public static TMPro.VerticalAlignmentOptions Bottom;  // const
        public static TMPro.VerticalAlignmentOptions Baseline;  // const
        public static TMPro.VerticalAlignmentOptions Geometry;  // const
        public static TMPro.VerticalAlignmentOptions Capline;  // const

    }

    // TypeToken: 0x2000093  // size: 0x14
    public sealed struct TextRenderFlags
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.TextRenderFlags DontRender;  // const
        public static TMPro.TextRenderFlags Render;  // const

    }

    // TypeToken: 0x2000094  // size: 0x14
    public sealed struct TMP_TextElementType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.TMP_TextElementType Character;  // const
        public static TMPro.TMP_TextElementType Sprite;  // const
        public static TMPro.TMP_TextElementType Image;  // const

    }

    // TypeToken: 0x2000095  // size: 0x14
    public sealed struct MaskingTypes
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.MaskingTypes MaskOff;  // const
        public static TMPro.MaskingTypes MaskHard;  // const
        public static TMPro.MaskingTypes MaskSoft;  // const

    }

    // TypeToken: 0x2000096  // size: 0x14
    public sealed struct TextOverflowModes
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.TextOverflowModes Overflow;  // const
        public static TMPro.TextOverflowModes Ellipsis;  // const
        public static TMPro.TextOverflowModes Masking;  // const
        public static TMPro.TextOverflowModes Truncate;  // const
        public static TMPro.TextOverflowModes ScrollRect;  // const
        public static TMPro.TextOverflowModes Page;  // const
        public static TMPro.TextOverflowModes Linked;  // const

    }

    // TypeToken: 0x2000097  // size: 0x14
    public sealed struct MaskingOffsetMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.MaskingOffsetMode Percentage;  // const
        public static TMPro.MaskingOffsetMode Pixel;  // const

    }

    // TypeToken: 0x2000098  // size: 0x14
    public sealed struct TextureMappingOptions
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.TextureMappingOptions Character;  // const
        public static TMPro.TextureMappingOptions Line;  // const
        public static TMPro.TextureMappingOptions Paragraph;  // const
        public static TMPro.TextureMappingOptions MatchAspect;  // const

    }

    // TypeToken: 0x2000099  // size: 0x14
    public sealed struct FontStyles
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.FontStyles Normal;  // const
        public static TMPro.FontStyles Bold;  // const
        public static TMPro.FontStyles Italic;  // const
        public static TMPro.FontStyles Underline;  // const
        public static TMPro.FontStyles LowerCase;  // const
        public static TMPro.FontStyles UpperCase;  // const
        public static TMPro.FontStyles SmallCaps;  // const
        public static TMPro.FontStyles Strikethrough;  // const
        public static TMPro.FontStyles Superscript;  // const
        public static TMPro.FontStyles Subscript;  // const
        public static TMPro.FontStyles Highlight;  // const
        public static TMPro.FontStyles Phonetic;  // const

    }

    // TypeToken: 0x200009A  // size: 0x14
    public sealed struct FontWeight
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.FontWeight Thin;  // const
        public static TMPro.FontWeight ExtraLight;  // const
        public static TMPro.FontWeight Light;  // const
        public static TMPro.FontWeight Regular;  // const
        public static TMPro.FontWeight Medium;  // const
        public static TMPro.FontWeight SemiBold;  // const
        public static TMPro.FontWeight Bold;  // const
        public static TMPro.FontWeight Heavy;  // const
        public static TMPro.FontWeight Black;  // const

    }

    // TypeToken: 0x200009B  // size: 0x28
    public sealed struct PhoneticCharSlot
    {
        // Fields
        public TMPro.TMP_FontAsset fontAsset;  // 0x10
        public System.Int32 materialIndex;  // 0x18
        public UnityEngine.TextCore.Glyph glyph;  // 0x20

    }

    // TypeToken: 0x200009C  // size: 0x30
    public class PhoneticData
    {
        // Fields
        public static TMPro.PhoneticData Default;  // static @ 0x0
        public System.String m_phonetic;  // 0x10
        public System.Boolean m_padding;  // 0x18
        public System.Boolean m_justify;  // 0x19
        public System.Int32 m_length;  // 0x1c
        public System.Int32 m_startIndex;  // 0x20
        public System.Int32 m_startCharCount;  // 0x24
        public System.Nullable<TMPro.PhoneticCharSlot>[] m_charSlots;  // 0x28

        // Methods
        // RVA: 0x04CD06D0  token: 0x60004DB
        public System.Void .ctor() { }
        // RVA: 0x04CD0660  token: 0x60004DC
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200009D  // size: 0xAF8
    public abstract class TMP_Text : UnityEngine.UI.MaskableGraphic
    {
        // Fields
        protected System.String m_textForPopulate;  // 0xe8
        protected System.String m_text;  // 0xf0
        private System.Boolean m_IsTextBackingStringDirty;  // 0xf8
        protected TMPro.ITextPreprocessor m_TextPreprocessor;  // 0x100
        protected System.Boolean m_isRightToLeft;  // 0x108
        protected TMPro.TMP_FontAsset m_serializedFontAsset;  // 0x110
        protected TMPro.TMP_FontAsset m_currentFontAsset;  // 0x118
        protected System.Boolean m_isSDFShader;  // 0x120
        protected UnityEngine.Material m_serializedSharedMaterial;  // 0x128
        protected UnityEngine.Material m_currentMaterial;  // 0x130
        protected static TMPro.MaterialReference[] m_materialReferences;  // static @ 0x0
        protected static System.Collections.Generic.Dictionary<System.Int32,System.Int32> m_materialReferenceIndexLookup;  // static @ 0x8
        protected static TMPro.TMP_TextProcessingStack<TMPro.MaterialReference> m_materialReferenceStack;  // static @ 0x10
        protected System.Int32 m_currentMaterialIndex;  // 0x138
        protected UnityEngine.Material[] m_fontSharedMaterials;  // 0x140
        protected UnityEngine.Material m_fontMaterial;  // 0x148
        protected UnityEngine.Material[] m_fontMaterials;  // 0x150
        protected System.Boolean m_isMaterialDirty;  // 0x158
        protected UnityEngine.Color32 m_fontColor32;  // 0x15c
        protected UnityEngine.Color m_fontColor;  // 0x160
        protected static UnityEngine.Color32 s_colorWhite;  // static @ 0x68
        protected UnityEngine.Color32 m_underlineColor;  // 0x170
        protected UnityEngine.Color32 m_strikethroughColor;  // 0x174
        protected System.Boolean m_enableVertexGradient;  // 0x178
        protected TMPro.ColorMode m_colorMode;  // 0x17c
        protected TMPro.VertexGradient m_fontColorGradient;  // 0x180
        protected TMPro.TMP_ColorGradient m_fontColorGradientPreset;  // 0x1c0
        protected TMPro.TMP_SpriteAsset m_spriteAsset;  // 0x1c8
        protected System.Boolean m_tintAllSprites;  // 0x1d0
        protected System.Boolean m_tintSprite;  // 0x1d1
        protected UnityEngine.Color32 m_spriteColor;  // 0x1d4
        protected TMPro.TMP_StyleSheet m_StyleSheet;  // 0x1d8
        private TMPro.TMP_Style m_TextStyle;  // 0x1e0
        protected System.Int32 m_TextStyleHashCode;  // 0x1e8
        protected System.Boolean m_overrideHtmlColors;  // 0x1ec
        protected UnityEngine.Color32 m_faceColor;  // 0x1f0
        protected UnityEngine.Color32 m_outlineColor;  // 0x1f4
        protected System.Single m_outlineWidth;  // 0x1f8
        protected System.Single m_fontSize;  // 0x1fc
        protected System.Single m_currentFontSize;  // 0x200
        protected System.Single m_fontSizeBase;  // 0x204
        protected TMPro.TMP_TextProcessingStack<System.Single> m_sizeStack;  // 0x208
        protected System.Single m_imageWidth;  // 0x228
        protected System.Single m_imageHeight;  // 0x22c
        protected System.Single m_maxLineAscenderWithImage;  // 0x230
        protected static System.Single m_basePhoneticElementScale;  // const
        protected TMPro.FontStyles m_phoneticFontStyle;  // 0x234
        protected TMPro.FontWeight m_phoneticFontWeight;  // 0x238
        protected TMPro.WordWrapState m_phoneticWordWarpState;  // 0x240
        protected System.Int32 m_phoneticStartCharIndex;  // 0x5d8
        protected System.Int32 m_phoneticStartIndex;  // 0x5dc
        protected System.Int32 m_phoneticEndIndex;  // 0x5e0
        protected System.Boolean m_isPhoneticOverLength;  // 0x5e4
        protected TMPro.TMP_TextProcessingQueue<TMPro.PhoneticData> m_phoneticDataQueue;  // 0x5e8
        protected TMPro.PhoneticData m_currentPhoneticData;  // 0x608
        protected System.Int32 m_phoneticCount;  // 0x610
        protected System.Single m_phoneticFontSize;  // 0x614
        protected System.Boolean m_showPhonetic;  // 0x618
        protected System.Int32 m_phoneticIndex;  // 0x61c
        protected System.Boolean m_isInSetArraySizes;  // 0x620
        protected System.Boolean m_needParseActionId;  // 0x621
        protected TMPro.FontWeight m_fontWeight;  // 0x624
        protected TMPro.FontWeight m_FontWeightInternal;  // 0x628
        protected TMPro.TMP_TextProcessingStack<TMPro.FontWeight> m_FontWeightStack;  // 0x630
        protected System.Boolean m_enableAutoSizing;  // 0x650
        protected System.Single m_maxFontSize;  // 0x654
        protected System.Single m_minFontSize;  // 0x658
        protected System.Int32 m_AutoSizeIterationCount;  // 0x65c
        protected System.Int32 m_AutoSizeMaxIterationCount;  // 0x660
        protected System.Boolean m_IsAutoSizePointSizeSet;  // 0x664
        protected System.Single m_fontSizeMin;  // 0x668
        protected System.Single m_fontSizeMax;  // 0x66c
        protected TMPro.FontStyles m_fontStyle;  // 0x670
        protected TMPro.FontStyles m_FontStyleInternal;  // 0x674
        protected TMPro.TMP_FontStyleStack m_fontStyleStack;  // 0x678
        protected System.Boolean m_isUsingBold;  // 0x682
        protected TMPro.HorizontalAlignmentOptions m_HorizontalAlignment;  // 0x684
        protected TMPro.VerticalAlignmentOptions m_VerticalAlignment;  // 0x688
        protected TMPro.TextAlignmentOptions m_textAlignment;  // 0x68c
        protected TMPro.HorizontalAlignmentOptions m_lineJustification;  // 0x690
        protected TMPro.TMP_TextProcessingStack<TMPro.HorizontalAlignmentOptions> m_lineJustificationStack;  // 0x698
        protected UnityEngine.Vector3[] m_textContainerLocalCorners;  // 0x6b8
        protected System.Single m_characterSpacing;  // 0x6c0
        protected System.Single m_cSpacing;  // 0x6c4
        protected System.Single m_monoSpacing;  // 0x6c8
        protected System.Single m_wordSpacing;  // 0x6cc
        protected System.Single m_lineSpacing;  // 0x6d0
        protected System.Single m_lineSpacingDelta;  // 0x6d4
        protected System.Single m_lineHeight;  // 0x6d8
        protected System.Boolean m_IsDrivenLineSpacing;  // 0x6dc
        protected System.Single m_lineSpacingMax;  // 0x6e0
        protected System.Single m_paragraphSpacing;  // 0x6e4
        protected System.Single m_charWidthMaxAdj;  // 0x6e8
        protected System.Single m_charWidthAdjDelta;  // 0x6ec
        protected System.Boolean m_enableWordWrapping;  // 0x6f0
        protected System.Boolean m_isCharacterWrappingEnabled;  // 0x6f1
        protected System.Boolean m_isNonBreakingSpace;  // 0x6f2
        protected System.Boolean m_isIgnoringAlignment;  // 0x6f3
        protected System.Single m_wordWrappingRatios;  // 0x6f4
        protected TMPro.TextOverflowModes m_overflowMode;  // 0x6f8
        protected System.Int32 m_firstOverflowCharacterIndex;  // 0x6fc
        protected TMPro.TMP_Text m_linkedTextComponent;  // 0x700
        private TMPro.TMP_Text parentLinkedComponent;  // 0x708
        protected System.Boolean m_isTextTruncated;  // 0x710
        protected System.Boolean m_enableKerning;  // 0x711
        protected System.Single m_GlyphHorizontalAdvanceAdjustment;  // 0x714
        protected System.Boolean m_enableExtraPadding;  // 0x718
        protected System.Boolean checkPaddingRequired;  // 0x719
        protected System.Boolean m_isRichText;  // 0x71a
        protected System.Boolean m_enableHyperlink;  // 0x71b
        protected UnityEngine.Vector2 m_linkExtraPadding;  // 0x71c
        protected System.Single m_richTextColorBlend;  // 0x724
        protected System.Boolean m_parseCtrlCharacters;  // 0x728
        protected System.Boolean m_isOverlay;  // 0x729
        protected System.Boolean m_isOrthographic;  // 0x72a
        protected System.Boolean m_isCullingEnabled;  // 0x72b
        protected System.Boolean m_isMaskingEnabled;  // 0x72c
        protected System.Boolean isMaskUpdateRequired;  // 0x72d
        protected System.Boolean m_ignoreCulling;  // 0x72e
        protected TMPro.TextureMappingOptions m_horizontalMapping;  // 0x730
        protected TMPro.TextureMappingOptions m_verticalMapping;  // 0x734
        protected System.Single m_uvLineOffset;  // 0x738
        protected TMPro.TextRenderFlags m_renderMode;  // 0x73c
        protected TMPro.VertexSortingOrder m_geometrySortingOrder;  // 0x740
        protected System.Boolean m_IsTextObjectScaleStatic;  // 0x744
        protected System.Boolean m_VertexBufferAutoSizeReduction;  // 0x745
        protected System.Int32 m_firstVisibleCharacter;  // 0x748
        protected System.Int32 m_maxVisibleCharacters;  // 0x74c
        protected System.Int32 m_maxVisibleWords;  // 0x750
        protected System.Int32 m_maxVisibleLines;  // 0x754
        protected System.Boolean m_useMaxVisibleDescender;  // 0x758
        protected System.Int32 m_pageToDisplay;  // 0x75c
        protected System.Boolean m_isNewPage;  // 0x760
        protected UnityEngine.Vector4 m_margin;  // 0x764
        protected System.Single m_marginLeft;  // 0x774
        protected System.Single m_marginRight;  // 0x778
        protected System.Single m_marginWidth;  // 0x77c
        protected System.Single m_marginHeight;  // 0x780
        protected System.Single m_width;  // 0x784
        protected TMPro.TMP_TextInfo m_textInfo;  // 0x788
        protected System.Boolean m_havePropertiesChanged;  // 0x790
        protected System.Boolean m_isUsingLegacyAnimationComponent;  // 0x791
        protected UnityEngine.Transform m_transform;  // 0x798
        protected UnityEngine.RectTransform m_rectTransform;  // 0x7a0
        protected UnityEngine.Vector2 m_PreviousRectTransformSize;  // 0x7a8
        protected UnityEngine.Vector2 m_PreviousPivotPosition;  // 0x7b0
        private System.Boolean <autoSizeTextContainer>k__BackingField;  // 0x7b8
        protected System.Boolean m_autoSizeTextContainer;  // 0x7b9
        protected UnityEngine.Mesh m_mesh;  // 0x7c0
        protected System.Boolean m_isVolumetricText;  // 0x7c8
        private static System.Func<System.Int32,System.String,TMPro.TMP_FontAsset> OnFontAssetRequest;  // static @ 0x70
        private static System.Func<System.Int32,System.String,TMPro.TMP_SpriteAsset> OnSpriteAssetRequest;  // static @ 0x78
        private System.Action<TMPro.TMP_TextInfo> OnPreRenderText;  // 0x7d0
        protected TMPro.TMP_SpriteAnimator m_spriteAnimator;  // 0x7d8
        protected System.Single m_flexibleHeight;  // 0x7e0
        protected System.Single m_flexibleWidth;  // 0x7e4
        protected System.Single m_minWidth;  // 0x7e8
        protected System.Single m_minHeight;  // 0x7ec
        protected System.Single m_maxWidth;  // 0x7f0
        protected System.Single m_maxHeight;  // 0x7f4
        protected UnityEngine.UI.LayoutElement m_LayoutElement;  // 0x7f8
        protected System.Single m_preferredWidth;  // 0x800
        protected System.Single m_renderedWidth;  // 0x804
        protected System.Boolean m_isPreferredWidthDirty;  // 0x808
        protected System.Single m_preferredHeight;  // 0x80c
        protected System.Single m_renderedHeight;  // 0x810
        protected System.Boolean m_isPreferredHeightDirty;  // 0x814
        protected System.Boolean m_isCalculatingPreferredValues;  // 0x815
        protected System.Int32 m_layoutPriority;  // 0x818
        protected System.Boolean m_isLayoutDirty;  // 0x81c
        protected System.Boolean m_isAwake;  // 0x81d
        private System.Boolean m_isWaitingOnResourceLoad;  // 0x81e
        protected System.Boolean m_isInit;  // 0x81f
        private TMPro.TMP_Text.TextInputSources m_inputSource;  // 0x820
        protected System.Single m_fontScaleMultiplier;  // 0x824
        private static System.Char[] m_htmlTag;  // static @ 0x80
        protected static TMPro.RichTextTagAttribute[] m_xmlAttribute;  // static @ 0x88
        private static System.Single[] m_attributeParameterValues;  // static @ 0x90
        protected System.Single tag_LineIndent;  // 0x828
        protected System.Single tag_Indent;  // 0x82c
        protected TMPro.TMP_TextProcessingStack<System.Single> m_indentStack;  // 0x830
        protected System.Boolean tag_NoParsing;  // 0x850
        protected System.Boolean m_isParsingText;  // 0x851
        protected UnityEngine.Matrix4x4 m_FXMatrix;  // 0x854
        protected System.Boolean m_isFXMatrixSet;  // 0x894
        protected TMPro.TMP_Text.UnicodeChar[] m_TextProcessingArray;  // 0x898
        private System.Int32 m_InternalTextProcessingArraySize;  // 0x8a0
        private TMPro.TMP_CharacterInfo[] m_internalCharacterInfo;  // 0x8a8
        protected System.Int32 m_totalCharacterCount;  // 0x8b0
        protected static TMPro.WordWrapState m_SavedWordWrapState;  // static @ 0x98
        protected static TMPro.WordWrapState m_SavedLineState;  // static @ 0x430
        protected static TMPro.WordWrapState m_SavedEllipsisState;  // static @ 0x7c8
        protected static TMPro.WordWrapState m_SavedLastValidState;  // static @ 0xb60
        protected static TMPro.WordWrapState m_SavedSoftLineBreakState;  // static @ 0xef8
        protected static TMPro.TMP_TextProcessingStack<TMPro.WordWrapState> m_EllipsisInsertionCandidateStack;  // static @ 0x1290
        protected System.Int32 m_characterCount;  // 0x8b4
        protected System.Int32 m_firstCharacterOfLine;  // 0x8b8
        protected System.Int32 m_firstVisibleCharacterOfLine;  // 0x8bc
        protected System.Int32 m_lastCharacterOfLine;  // 0x8c0
        protected System.Int32 m_lastVisibleCharacterOfLine;  // 0x8c4
        protected System.Int32 m_lineNumber;  // 0x8c8
        protected System.Int32 m_lineVisibleCharacterCount;  // 0x8cc
        protected System.Int32 m_pageNumber;  // 0x8d0
        protected System.Single m_PageAscender;  // 0x8d4
        protected System.Single m_maxTextAscender;  // 0x8d8
        protected System.Single m_maxCapHeight;  // 0x8dc
        protected System.Single m_ElementAscender;  // 0x8e0
        protected System.Single m_ElementDescender;  // 0x8e4
        protected System.Single m_maxLineAscender;  // 0x8e8
        protected System.Single m_maxLineDescender;  // 0x8ec
        protected System.Single m_startOfLineAscender;  // 0x8f0
        protected System.Single m_startOfLineDescender;  // 0x8f4
        protected System.Single m_lineOffset;  // 0x8f8
        protected TMPro.Extents m_meshExtents;  // 0x8fc
        protected UnityEngine.Color32 m_htmlColor;  // 0x90c
        protected TMPro.TMP_TextProcessingStack<UnityEngine.Color32> m_colorStack;  // 0x910
        protected TMPro.TMP_TextProcessingStack<UnityEngine.Color32> m_underlineColorStack;  // 0x930
        protected TMPro.TMP_TextProcessingStack<UnityEngine.Color32> m_strikethroughColorStack;  // 0x950
        protected TMPro.TMP_TextProcessingStack<TMPro.HighlightState> m_HighlightStateStack;  // 0x970
        protected TMPro.TMP_TextProcessingStack<TMPro.TMP_ColorGradient> m_colorGradientStack;  // 0x9a0
        protected TMPro.TMP_ColorGradient m_colorGradientPreset;  // 0x9c8
        protected System.Boolean m_colorGradientPresetIsTinted;  // 0x9d0
        protected System.Single m_tabSpacing;  // 0x9d4
        protected System.Single m_spacing;  // 0x9d8
        protected TMPro.TMP_TextProcessingStack<System.Int32>[] m_TextStyleStacks;  // 0x9e0
        protected TMPro.TMP_TextProcessingStack<System.Int32> m_ItalicAngleStack;  // 0x9e8
        protected TMPro.TMP_TextProcessingStack<System.Int32> m_actionStack;  // 0xa08
        protected TMPro.TMP_TextProcessingStack<System.Single> m_baselineOffsetStack;  // 0xa28
        protected System.Int32 m_TextStyleStackDepth;  // 0xa48
        protected System.Int32 m_ItalicAngle;  // 0xa4c
        protected System.Single m_padding;  // 0xa50
        protected System.Single m_baselineOffset;  // 0xa54
        protected System.Single m_xAdvance;  // 0xa58
        protected System.Single m_lastCharAdvance;  // 0xa5c
        protected System.Boolean m_lastIsUpVowels;  // 0xa60
        protected System.Boolean m_lastIsAscender;  // 0xa61
        protected System.Boolean m_lastIsDescender;  // 0xa62
        protected System.Single m_lastHeight;  // 0xa64
        protected static System.Boolean s_forceLeftAlign;  // static @ 0x1648
        protected TMPro.TMP_TextElementType m_textElementType;  // 0xa68
        protected TMPro.TMP_TextElement m_cached_TextElement;  // 0xa70
        protected TMPro.TMP_Text.SpecialCharacter m_Ellipsis;  // 0xa78
        protected TMPro.TMP_Text.SpecialCharacter m_Underline;  // 0xa98
        protected TMPro.TMP_SpriteAsset m_defaultSpriteAsset;  // 0xab8
        protected TMPro.TMP_SpriteAsset m_currentSpriteAsset;  // 0xac0
        protected System.Int32 m_spriteCount;  // 0xac8
        protected System.Int32 m_spriteIndex;  // 0xacc
        protected System.Int32 m_spriteAnimationID;  // 0xad0
        private static Unity.Profiling.ProfilerMarker k_ParseTextMarker;  // static @ 0x1650
        private static Unity.Profiling.ProfilerMarker k_InsertNewLineMarker;  // static @ 0x1658
        protected System.Boolean m_ignoreActiveState;  // 0xad4
        private TMPro.TMP_Text.TextBackingContainer m_TextBackingArray;  // 0xad8
        private readonly System.Decimal[] k_Power;  // 0xae8
        protected static UnityEngine.Vector2 k_LargePositiveVector2;  // static @ 0x1660
        protected static UnityEngine.Vector2 k_LargeNegativeVector2;  // static @ 0x1668
        protected static System.Single k_LargePositiveFloat;  // static @ 0x1670
        protected static System.Single k_LargeNegativeFloat;  // static @ 0x1674
        protected static System.Int32 k_LargePositiveInt;  // static @ 0x1678
        protected static System.Int32 k_LargeNegativeInt;  // static @ 0x167c
        private System.Boolean m_dynamicFontAssetUsed;  // 0xaf0

        // Properties
        System.String text { get; /* RVA: 0x03E60F20 */ set; /* RVA: 0x03E86700 */ }
        System.String textForPopulate { get; /* RVA: 0x0333C4A0 */ }
        TMPro.ITextPreprocessor textPreprocessor { get; /* RVA: 0x04D87D50 */ set; /* RVA: 0x067A5EA0 */ }
        System.Boolean isRightToLeftText { get; /* RVA: 0x04DBA120 */ set; /* RVA: 0x0B32B074 */ }
        TMPro.TMP_FontAsset font { get; /* RVA: 0x05393890 */ set; /* RVA: 0x0B32AEBC */ }
        TMPro.TMP_FontAsset m_fontAsset { get; /* RVA: 0x04D8D1B0 */ set; /* RVA: 0x05397170 */ }
        UnityEngine.Material fontSharedMaterial { get; /* RVA: 0x04D8D910 */ set; /* RVA: 0x0B32AD0C */ }
        UnityEngine.Material m_sharedMaterial { get; /* RVA: 0x04D8D910 */ set; /* RVA: 0x053971AC */ }
        UnityEngine.Material[] fontSharedMaterials { get; /* RVA: 0x0B32A0DC */ set; /* RVA: 0x0B32ACD0 */ }
        UnityEngine.Material fontMaterial { get; /* RVA: 0x0B32A088 */ set; /* RVA: 0x0B32ABF4 */ }
        UnityEngine.Material[] fontMaterials { get; /* RVA: 0x0B32A09C */ set; /* RVA: 0x0B32ACD0 */ }
        UnityEngine.Color color { get; /* RVA: 0x043E92E0 */ set; /* RVA: 0x04369F20 */ }
        System.Single alpha { get; /* RVA: 0x04DA7C50 */ set; /* RVA: 0x0B32A8B0 */ }
        System.Boolean enableVertexGradient { get; /* RVA: 0x04D8FF90 */ set; /* RVA: 0x0B32AAB8 */ }
        TMPro.VertexGradient colorGradient { get; /* RVA: 0x04DB9FA0 */ set; /* RVA: 0x0B32A9A8 */ }
        TMPro.TMP_ColorGradient colorGradientPreset { get; /* RVA: 0x04D8DA80 */ set; /* RVA: 0x0B32A974 */ }
        TMPro.TMP_SpriteAsset spriteAsset { get; /* RVA: 0x04D8D790 */ set; /* RVA: 0x0B32B6BC */ }
        System.Boolean tintAllSprites { get; /* RVA: 0x04D900F0 */ set; /* RVA: 0x0B32B7A4 */ }
        TMPro.TMP_StyleSheet styleSheet { get; /* RVA: 0x04D87870 */ set; /* RVA: 0x0B32B700 */ }
        TMPro.TMP_Style textStyle { get; /* RVA: 0x0333CE30 */ set; /* RVA: 0x0B32B744 */ }
        System.Boolean overrideColorTags { get; /* RVA: 0x04D90680 */ set; /* RVA: 0x0B32B560 */ }
        UnityEngine.Color32 faceColor { get; /* RVA: 0x0B329FC0 */ set; /* RVA: 0x0B32AB68 */ }
        UnityEngine.Color32 outlineColor { get; /* RVA: 0x0B32A270 */ set; /* RVA: 0x0B32B46C */ }
        System.Single outlineWidth { get; /* RVA: 0x0B32A338 */ set; /* RVA: 0x0B32B4C4 */ }
        System.Single fontSize { get; /* RVA: 0x04DBA090 */ set; /* RVA: 0x04D83710 */ }
        System.Single PhoneticElementScale { get; /* RVA: 0x0B329EFC */ }
        System.String m_currentPhonetic { get; /* RVA: 0x0B32A20C */ }
        System.Boolean m_currentEnablePhoneticAutoPadding { get; /* RVA: 0x0496E820 */ }
        System.Boolean m_currentEnablePhoneticAutoJustify { get; /* RVA: 0x0B32A1A8 */ }
        TMPro.FontWeight fontWeight { get; /* RVA: 0x04DBA0A0 */ set; /* RVA: 0x0B32AE78 */ }
        System.Single pixelsPerUnit { get; /* RVA: 0x0B32A3E8 */ }
        System.Boolean enableAutoSizing { get; /* RVA: 0x04DB9FD0 */ set; /* RVA: 0x0B32A9EC */ }
        System.Single fontSizeMin { get; /* RVA: 0x04DBA080 */ set; /* RVA: 0x0B32ADEC */ }
        System.Single fontSizeMax { get; /* RVA: 0x04DBA070 */ set; /* RVA: 0x0B32ADA4 */ }
        TMPro.FontStyles fontStyle { get; /* RVA: 0x04DA15F0 */ set; /* RVA: 0x0B32AE34 */ }
        System.Boolean isUsingBold { get; /* RVA: 0x04DBA150 */ }
        TMPro.HorizontalAlignmentOptions horizontalAlignment { get; /* RVA: 0x04DBA0D0 */ set; /* RVA: 0x0B32AFA0 */ }
        TMPro.VerticalAlignmentOptions verticalAlignment { get; /* RVA: 0x04DA1650 */ set; /* RVA: 0x0B32B814 */ }
        TMPro.TextAlignmentOptions alignment { get; /* RVA: 0x04DB9F60 */ set; /* RVA: 0x0B32A874 */ }
        System.Single characterSpacing { get; /* RVA: 0x04DB9F80 */ set; /* RVA: 0x0B32A8DC */ }
        System.Single wordSpacing { get; /* RVA: 0x04DBA320 */ set; /* RVA: 0x0B32B864 */ }
        System.Single lineSpacing { get; /* RVA: 0x04DBA1A0 */ set; /* RVA: 0x0B32B1B8 */ }
        System.Single lineSpacingAdjustment { get; /* RVA: 0x04DBA190 */ set; /* RVA: 0x0B32B16C */ }
        System.Single paragraphSpacing { get; /* RVA: 0x04DBA290 */ set; /* RVA: 0x0B32B5B0 */ }
        System.Single characterWidthAdjustment { get; /* RVA: 0x04DB9F90 */ set; /* RVA: 0x0B32A928 */ }
        System.Boolean enableWordWrapping { get; /* RVA: 0x04DBA010 */ set; /* RVA: 0x0B32AAE0 */ }
        System.Single wordWrappingRatios { get; /* RVA: 0x04DBA330 */ set; /* RVA: 0x0B32B8B0 */ }
        TMPro.TextOverflowModes overflowMode { get; /* RVA: 0x04DBA270 */ set; /* RVA: 0x0B32B51C */ }
        System.Boolean isTextOverflowing { get; /* RVA: 0x0B32A10C */ }
        System.Int32 firstOverflowCharacterIndex { get; /* RVA: 0x04DBA030 */ }
        TMPro.TMP_Text linkedTextComponent { get; /* RVA: 0x04DBA1D0 */ set; /* RVA: 0x0B32B254 */ }
        System.Boolean isTextTruncated { get; /* RVA: 0x04DBA140 */ }
        System.Boolean enableKerning { get; /* RVA: 0x04DBA000 */ set; /* RVA: 0x0B32AA74 */ }
        System.Boolean extraPadding { get; /* RVA: 0x04DBA020 */ set; /* RVA: 0x0B32AB24 */ }
        System.Boolean richText { get; /* RVA: 0x04DBA2D0 */ set; /* RVA: 0x0B32B678 */ }
        System.Boolean enableHyperlink { get; /* RVA: 0x04DB9FF0 */ set; /* RVA: 0x0B32AA5C */ }
        UnityEngine.Vector2 linkExtraPadding { get; /* RVA: 0x04DBA1B0 */ set; /* RVA: 0x0B32B204 */ }
        System.Single richTextColorBlend { get; /* RVA: 0x04DBA2C0 */ set; /* RVA: 0x0B32B658 */ }
        System.Boolean parseCtrlCharacters { get; /* RVA: 0x04DBA2A0 */ set; /* RVA: 0x0B32B5FC */ }
        System.Boolean isOverlay { get; /* RVA: 0x04DBA110 */ set; /* RVA: 0x0B32B030 */ }
        System.Boolean isOrthographic { get; /* RVA: 0x04DBA100 */ set; /* RVA: 0x0B32B008 */ }
        System.Boolean enableCulling { get; /* RVA: 0x04DB9FE0 */ set; /* RVA: 0x0B32AA28 */ }
        System.Boolean ignoreVisibility { get; /* RVA: 0x04DBA0F0 */ set; /* RVA: 0x0B32AFF0 */ }
        TMPro.TextureMappingOptions horizontalMapping { get; /* RVA: 0x04DBA0E0 */ set; /* RVA: 0x0B32AFC8 */ }
        TMPro.TextureMappingOptions verticalMapping { get; /* RVA: 0x04DBA310 */ set; /* RVA: 0x0B32B83C */ }
        System.Single mappingUvLineOffset { get; /* RVA: 0x04DBA1E0 */ set; /* RVA: 0x0B32B348 */ }
        TMPro.TextRenderFlags renderMode { get; /* RVA: 0x04DBA2B0 */ set; /* RVA: 0x0B32B640 */ }
        TMPro.VertexSortingOrder geometrySortingOrder { get; /* RVA: 0x04DBA0B0 */ set; /* RVA: 0x0B32AF60 */ }
        System.Boolean isTextObjectScaleStatic { get; /* RVA: 0x04DBA130 */ set; /* RVA: 0x0B32B0B8 */ }
        System.Boolean vertexBufferAutoSizeReduction { get; /* RVA: 0x04DBA300 */ set; /* RVA: 0x0B32B7F4 */ }
        System.Int32 firstVisibleCharacter { get; /* RVA: 0x04DBA040 */ set; /* RVA: 0x0B32ABCC */ }
        System.Int32 maxVisibleCharacters { get; /* RVA: 0x04DBA210 */ set; /* RVA: 0x04B4A810 */ }
        System.Int32 maxVisibleWords { get; /* RVA: 0x04DBA230 */ set; /* RVA: 0x0B32B444 */ }
        System.Int32 maxVisibleLines { get; /* RVA: 0x04DBA220 */ set; /* RVA: 0x0B32B41C */ }
        System.Boolean useMaxVisibleDescender { get; /* RVA: 0x04DBA2F0 */ set; /* RVA: 0x0B32B7CC */ }
        System.Int32 pageToDisplay { get; /* RVA: 0x04DBA280 */ set; /* RVA: 0x0B32B588 */ }
        UnityEngine.Vector4 margin { get; /* RVA: 0x04DBA1F0 */ set; /* RVA: 0x0B32B374 */ }
        TMPro.TMP_TextInfo textInfo { get; /* RVA: 0x04DBA2E0 */ }
        System.Boolean havePropertiesChanged { get; /* RVA: 0x04DBA0C0 */ set; /* RVA: 0x0B32AF80 */ }
        System.Boolean isUsingLegacyAnimationComponent { get; /* RVA: 0x04DBA160 */ set; /* RVA: 0x04DBA350 */ }
        UnityEngine.Transform transform { get; /* RVA: 0x02EDE870 */ }
        UnityEngine.RectTransform rectTransform { get; /* RVA: 0x031E83F0 */ }
        System.Boolean autoSizeTextContainer { get; /* RVA: 0x04DB9F70 */ set; /* RVA: 0x04DBA340 */ }
        UnityEngine.Mesh mesh { get; /* RVA: 0x04DB9A70 */ }
        System.Boolean isVolumetricText { get; /* RVA: 0x04DBA170 */ set; /* RVA: 0x0B32B118 */ }
        UnityEngine.Bounds bounds { get; /* RVA: 0x0B329F1C */ }
        UnityEngine.Bounds textBounds { get; /* RVA: 0x0B32A600 */ }
        TMPro.TMP_SpriteAnimator spriteAnimator { get; /* RVA: 0x0B32A50C */ }
        System.Single flexibleHeight { get; /* RVA: 0x04DBA050 */ }
        System.Single flexibleWidth { get; /* RVA: 0x04DBA060 */ }
        System.Single minWidth { get; /* RVA: 0x04DBA260 */ }
        System.Single minHeight { get; /* RVA: 0x04DBA250 */ }
        System.Single maxWidth { get; /* RVA: 0x04DBA240 */ }
        System.Single maxHeight { get; /* RVA: 0x04DBA200 */ }
        UnityEngine.UI.LayoutElement layoutElement { get; /* RVA: 0x0B32A118 */ }
        System.Single preferredWidth { get; /* RVA: 0x05391678 */ }
        System.Single preferredHeight { get; /* RVA: 0x0539160C */ }
        System.Single renderedWidth { get; /* RVA: 0x0B325694 */ }
        System.Single renderedHeight { get; /* RVA: 0x0B3255A4 */ }
        System.Int32 layoutPriority { get; /* RVA: 0x04DBA180 */ }

        // Events
        event System.Func<System.Int32,System.String,TMPro.TMP_FontAsset> OnFontAssetRequest;
        event System.Func<System.Int32,System.String,TMPro.TMP_SpriteAsset> OnSpriteAssetRequest;
        event System.Action<TMPro.TMP_TextInfo> OnPreRenderText;

        // Methods
        // RVA: 0x0B328DA4  token: 0x60004E0
        public System.Void ResetTextForPopulate() { }
        // RVA: 0x0B327DB4  token: 0x60004E1
        public virtual System.Void RefreshPopulateText() { }
        // RVA: 0x011EC580  token: 0x60004E8
        protected virtual UnityEngine.Material LoadMaterial(System.String path) { }
        // RVA: 0x05392D94  token: 0x6000591
        protected virtual System.Void Awake() { }
        // RVA: 0x035181E0  token: 0x6000592
        public System.Void InitDataFromPool() { }
        // RVA: 0x03519220  token: 0x6000593
        protected virtual System.Void OnDestroy() { }
        // RVA: 0x041E1670  token: 0x6000594
        protected virtual System.Void LoadFontAsset() { }
        // RVA: 0x041E1670  token: 0x6000595
        protected virtual System.Void SetSharedMaterial(UnityEngine.Material mat) { }
        // RVA: 0x011EC580  token: 0x6000596
        protected virtual UnityEngine.Material GetMaterial(UnityEngine.Material mat) { }
        // RVA: 0x041E1670  token: 0x6000597
        protected virtual System.Void SetFontBaseMaterial(UnityEngine.Material mat) { }
        // RVA: 0x011EC580  token: 0x6000598
        protected virtual UnityEngine.Material[] GetSharedMaterials() { }
        // RVA: 0x041E1670  token: 0x6000599
        protected virtual System.Void SetSharedMaterials(UnityEngine.Material[] materials) { }
        // RVA: 0x011EC580  token: 0x600059A
        protected virtual UnityEngine.Material[] GetMaterials(UnityEngine.Material[] mats) { }
        // RVA: 0x0B3222D0  token: 0x600059B
        protected virtual UnityEngine.Material CreateMaterialInstance(UnityEngine.Material source) { }
        // RVA: 0x0B329C3C  token: 0x600059C
        protected System.Void SetVertexColorGradient(TMPro.TMP_ColorGradient gradient) { }
        // RVA: 0x041E1670  token: 0x600059D
        protected System.Void SetTextSortingOrder(TMPro.VertexSortingOrder order) { }
        // RVA: 0x041E1670  token: 0x600059E
        protected System.Void SetTextSortingOrder(System.Int32[] order) { }
        // RVA: 0x041E1670  token: 0x600059F
        protected virtual System.Void SetFaceColor(UnityEngine.Color32 color) { }
        // RVA: 0x041E1670  token: 0x60005A0
        protected virtual System.Void SetOutlineColor(UnityEngine.Color32 color) { }
        // RVA: 0x041E1670  token: 0x60005A1
        protected virtual System.Void SetOutlineThickness(System.Single thickness) { }
        // RVA: 0x041E1670  token: 0x60005A2
        protected virtual System.Void SetShaderDepth() { }
        // RVA: 0x041E1670  token: 0x60005A3
        protected virtual System.Void SetCulling() { }
        // RVA: 0x041E1670  token: 0x60005A4
        private virtual System.Void UpdateCulling() { }
        // RVA: 0x0333AE70  token: 0x60005A5
        protected virtual System.Single GetPaddingForMaterial() { }
        // RVA: 0x0B325078  token: 0x60005A6
        protected virtual System.Single GetPaddingForMaterial(UnityEngine.Material mat) { }
        // RVA: 0x011EC580  token: 0x60005A7
        protected virtual UnityEngine.Vector3[] GetTextContainerLocalCorners() { }
        // RVA: 0x041E1670  token: 0x60005A8
        public virtual System.Void ForceMeshUpdate(System.Boolean ignoreActiveState, System.Boolean forceTextReparsing) { }
        // RVA: 0x041E1670  token: 0x60005A9
        public virtual System.Void UpdateGeometry(UnityEngine.Mesh mesh, System.Int32 index) { }
        // RVA: 0x041E1670  token: 0x60005AA
        public virtual System.Void UpdateVertexData(TMPro.TMP_VertexDataUpdateFlags flags) { }
        // RVA: 0x041E1670  token: 0x60005AB
        public virtual System.Void UpdateVertexData() { }
        // RVA: 0x041E1670  token: 0x60005AC
        public virtual System.Void SetVertices(UnityEngine.Vector3[] vertices) { }
        // RVA: 0x041E1670  token: 0x60005AD
        public virtual System.Void UpdateMeshPadding() { }
        // RVA: 0x045F3E40  token: 0x60005AE
        public virtual System.Void CrossFadeColor(UnityEngine.Color targetColor, System.Single duration, System.Boolean ignoreTimeScale, System.Boolean useAlpha) { }
        // RVA: 0x0B322384  token: 0x60005AF
        public virtual System.Void CrossFadeAlpha(System.Single alpha, System.Single duration, System.Boolean ignoreTimeScale) { }
        // RVA: 0x041E1670  token: 0x60005B0
        protected virtual System.Void InternalCrossFadeColor(UnityEngine.Color targetColor, System.Single duration, System.Boolean ignoreTimeScale, System.Boolean useAlpha) { }
        // RVA: 0x041E1670  token: 0x60005B1
        protected virtual System.Void InternalCrossFadeAlpha(System.Single alpha, System.Single duration, System.Boolean ignoreTimeScale) { }
        // RVA: 0x0333C3B0  token: 0x60005B2
        protected System.Void ParseInputText() { }
        // RVA: 0x0333BE20  token: 0x60005B3
        private System.Void PopulateTextBackingArray(System.String sourceText) { }
        // RVA: 0x0333BA70  token: 0x60005B4
        private System.Void PopulateTextBackingArray(System.String sourceText, System.Int32 start, System.Int32 length) { }
        // RVA: 0x0B327668  token: 0x60005B5
        private System.Void PopulateTextBackingArray(System.Text.StringBuilder sourceText, System.Int32 start, System.Int32 length) { }
        // RVA: 0x0B327768  token: 0x60005B6
        private System.Void PopulateTextBackingArray(System.Char[] sourceText, System.Int32 start, System.Int32 length) { }
        // RVA: 0x0333D0F0  token: 0x60005B7
        private System.Void PopulateTextProcessingArray() { }
        // RVA: 0x0B3296E0  token: 0x60005B8
        private System.Void SetTextInternal(System.String sourceText) { }
        // RVA: 0x03639B30  token: 0x60005B9
        public System.Void SetText(System.String sourceText, System.Boolean syncTextInputBox) { }
        // RVA: 0x0B329B98  token: 0x60005BA
        public System.Void SetText(System.String sourceText, System.Single arg0) { }
        // RVA: 0x0B329734  token: 0x60005BB
        public System.Void SetText(System.String sourceText, System.Single arg0, System.Single arg1) { }
        // RVA: 0x0B329B08  token: 0x60005BC
        public System.Void SetText(System.String sourceText, System.Single arg0, System.Single arg1, System.Single arg2) { }
        // RVA: 0x0B329B4C  token: 0x60005BD
        public System.Void SetText(System.String sourceText, System.Single arg0, System.Single arg1, System.Single arg2, System.Single arg3) { }
        // RVA: 0x0B329AB0  token: 0x60005BE
        public System.Void SetText(System.String sourceText, System.Single arg0, System.Single arg1, System.Single arg2, System.Single arg3, System.Single arg4) { }
        // RVA: 0x0B3299E8  token: 0x60005BF
        public System.Void SetText(System.String sourceText, System.Single arg0, System.Single arg1, System.Single arg2, System.Single arg3, System.Single arg4, System.Single arg5) { }
        // RVA: 0x0B329A48  token: 0x60005C0
        public System.Void SetText(System.String sourceText, System.Single arg0, System.Single arg1, System.Single arg2, System.Single arg3, System.Single arg4, System.Single arg5, System.Single arg6) { }
        // RVA: 0x0B32979C  token: 0x60005C1
        public System.Void SetText(System.String sourceText, System.Single arg0, System.Single arg1, System.Single arg2, System.Single arg3, System.Single arg4, System.Single arg5, System.Single arg6, System.Single arg7) { }
        // RVA: 0x0B329770  token: 0x60005C2
        public System.Void SetText(System.Text.StringBuilder sourceText) { }
        // RVA: 0x0B329BE4  token: 0x60005C3
        private System.Void SetText(System.Text.StringBuilder sourceText, System.Int32 start, System.Int32 length) { }
        // RVA: 0x0B3296B8  token: 0x60005C4
        public System.Void SetText(System.Char[] sourceText) { }
        // RVA: 0x0B329BD4  token: 0x60005C5
        public System.Void SetText(System.Char[] sourceText, System.Int32 start, System.Int32 length) { }
        // RVA: 0x0B3296B8  token: 0x60005C6
        public System.Void SetCharArray(System.Char[] sourceText) { }
        // RVA: 0x0B329620  token: 0x60005C7
        public System.Void SetCharArray(System.Char[] sourceText, System.Int32 start, System.Int32 length) { }
        // RVA: 0x0333CAC0  token: 0x60005C8
        private TMPro.TMP_Style GetStyle(System.Int32 hashCode) { }
        // RVA: 0x0B3285DC  token: 0x60005C9
        private System.Boolean ReplaceOpeningStyleTag(TMPro.TMP_Text.TextBackingContainer& sourceText, System.Int32 srcIndex, System.Int32& srcOffset, TMPro.TMP_Text.UnicodeChar[]& charBuffer, System.Int32& writeIndex) { }
        // RVA: 0x0B3289A4  token: 0x60005CA
        private System.Boolean ReplaceOpeningStyleTag(System.Int32[]& sourceText, System.Int32 srcIndex, System.Int32& srcOffset, TMPro.TMP_Text.UnicodeChar[]& charBuffer, System.Int32& writeIndex) { }
        // RVA: 0x0B327EDC  token: 0x60005CB
        private System.Void ReplaceClosingStyleTag(TMPro.TMP_Text.TextBackingContainer& sourceText, System.Int32 srcIndex, TMPro.TMP_Text.UnicodeChar[]& charBuffer, System.Int32& writeIndex) { }
        // RVA: 0x0B328264  token: 0x60005CC
        private System.Void ReplaceClosingStyleTag(System.Int32[]& sourceText, System.Int32 srcIndex, TMPro.TMP_Text.UnicodeChar[]& charBuffer, System.Int32& writeIndex) { }
        // RVA: 0x0B327024  token: 0x60005CD
        private System.Boolean InsertOpeningStyleTag(TMPro.TMP_Style style, System.Int32 srcIndex, TMPro.TMP_Text.UnicodeChar[]& charBuffer, System.Int32& writeIndex) { }
        // RVA: 0x0B326CA8  token: 0x60005CE
        private System.Void InsertClosingStyleTag(TMPro.TMP_Text.UnicodeChar[]& charBuffer, System.Int32& writeIndex) { }
        // RVA: 0x0B324FC4  token: 0x60005CF
        private System.Int32 GetMarkupTagHashCode(System.Int32[] tagDefinition, System.Int32 readIndex) { }
        // RVA: 0x04111440  token: 0x60005D0
        private System.Int32 GetMarkupTagHashCode(TMPro.TMP_Text.TextBackingContainer tagDefinition, System.Int32 readIndex) { }
        // RVA: 0x0B3256B0  token: 0x60005D1
        private System.Int32 GetStyleHashCode(System.Int32[]& text, System.Int32 index, System.Int32& closeIndex) { }
        // RVA: 0x0B325770  token: 0x60005D2
        private System.Int32 GetStyleHashCode(TMPro.TMP_Text.TextBackingContainer& text, System.Int32 index, System.Int32& closeIndex) { }
        // RVA: -1  // generic def  token: 0x60005D3
        private System.Void ResizeInternalArray(T[]& array) { }
        // RVA: -1  // generic def  token: 0x60005D4
        private System.Void ResizeInternalArray(T[]& array, System.Int32 size) { }
        // RVA: 0x0B321968  token: 0x60005D5
        private System.Void AddFloatToInternalTextBackingArray(System.Single value, System.Int32 padding, System.Int32 precision, System.Int32& writeIndex) { }
        // RVA: 0x0B321C4C  token: 0x60005D6
        private System.Void AddIntegerToInternalTextBackingArray(System.Double number, System.Int32 padding, System.Int32& writeIndex) { }
        // RVA: 0x0B3273A0  token: 0x60005D7
        private System.String InternalTextBackingArrayToString() { }
        // RVA: 0x011EC580  token: 0x60005D8
        protected virtual System.Int32 SetArraySizes(TMPro.TMP_Text.UnicodeChar[] unicodeChars) { }
        // RVA: 0x0B3254B0  token: 0x60005D9
        public UnityEngine.Vector2 GetPreferredValues() { }
        // RVA: 0x0B325308  token: 0x60005DA
        public UnityEngine.Vector2 GetPreferredValues(System.Single width, System.Single height) { }
        // RVA: 0x0B325370  token: 0x60005DB
        public UnityEngine.Vector2 GetPreferredValues(System.String text) { }
        // RVA: 0x0B325438  token: 0x60005DC
        public UnityEngine.Vector2 GetPreferredValues(System.String text, System.Single width, System.Single height) { }
        // RVA: 0x03E641C0  token: 0x60005DD
        protected System.Single GetPreferredWidth() { }
        // RVA: 0x0B325504  token: 0x60005DE
        private System.Single GetPreferredWidth(UnityEngine.Vector2 margin) { }
        // RVA: 0x03E63FA0  token: 0x60005DF
        protected System.Single GetPreferredHeight() { }
        // RVA: 0x0B325254  token: 0x60005E0
        private System.Single GetPreferredHeight(UnityEngine.Vector2 margin) { }
        // RVA: 0x0B32561C  token: 0x60005E1
        public UnityEngine.Vector2 GetRenderedValues() { }
        // RVA: 0x0B3255C0  token: 0x60005E2
        public UnityEngine.Vector2 GetRenderedValues(System.Boolean onlyVisibleCharacters) { }
        // RVA: 0x0B325694  token: 0x60005E3
        private System.Single GetRenderedWidth() { }
        // RVA: 0x0B325678  token: 0x60005E4
        protected System.Single GetRenderedWidth(System.Boolean onlyVisibleCharacters) { }
        // RVA: 0x0B3255A4  token: 0x60005E5
        private System.Single GetRenderedHeight() { }
        // RVA: 0x0B325588  token: 0x60005E6
        protected System.Single GetRenderedHeight(System.Boolean onlyVisibleCharacters) { }
        // RVA: 0x0333EF20  token: 0x60005E7
        protected virtual UnityEngine.Vector2 CalculatePreferredValues(System.Single& fontSize, UnityEngine.Vector2 marginSize, System.Boolean isTextAutoSizingEnabled, System.Boolean isWordWrappingEnabled) { }
        // RVA: 0x04D8F590  token: 0x60005E8
        protected virtual UnityEngine.Bounds GetCompoundBounds() { }
        // RVA: 0x04D94BD0  token: 0x60005E9
        private virtual UnityEngine.Rect GetCanvasSpaceClippingRect() { }
        // RVA: 0x0B325B70  token: 0x60005EA
        protected UnityEngine.Bounds GetTextBounds() { }
        // RVA: 0x0B325838  token: 0x60005EB
        protected UnityEngine.Bounds GetTextBounds(System.Boolean onlyVisibleCharacters) { }
        // RVA: 0x0B321D64  token: 0x60005EC
        protected System.Void AdjustLineOffset(System.Int32 startIndex, System.Int32 endIndex, System.Single offset) { }
        // RVA: 0x03516A20  token: 0x60005ED
        protected System.Void ResizeLineExtents(System.Int32 size) { }
        // RVA: 0x011EC580  token: 0x60005EE
        public virtual TMPro.TMP_TextInfo GetTextInfo(System.String text) { }
        // RVA: 0x041E1670  token: 0x60005EF
        public virtual System.Void ComputeMarginSize() { }
        // RVA: 0x03517750  token: 0x60005F0
        protected System.Void InsertNewLine(System.Int32 i, System.Single baseScale, System.Single currentElementScale, System.Single currentEmScale, System.Single glyphAdjustment, System.Single boldSpacingAdjustment, System.Single characterSpacingAdjustment, System.Single width, System.Single lineGap, System.Boolean& isMaxVisibleDescenderSet, System.Single& maxVisibleDescender) { }
        // RVA: 0x03516D20  token: 0x60005F1
        protected System.Void SaveWordWrappingState(TMPro.WordWrapState& state, System.Int32 index, System.Int32 count) { }
        // RVA: 0x03516280  token: 0x60005F2
        protected System.Int32 RestoreWordWrappingState(TMPro.WordWrapState& state) { }
        // RVA: 0x032546F0  token: 0x60005F3
        protected virtual System.Void SaveGlyphVertexInfo(System.Single padding, System.Single style_padding, UnityEngine.Color32 vertexColor) { }
        // RVA: 0x0B328DB4  token: 0x60005F4
        protected virtual System.Void SaveSpriteVertexInfo(UnityEngine.Color32 vertexColor) { }
        // RVA: 0x03253E80  token: 0x60005F5
        protected virtual System.Void FillCharacterVertexBuffers(System.Int32 i, System.Int32 index_X4) { }
        // RVA: 0x0B323AF8  token: 0x60005F6
        protected virtual System.Void FillCharacterVertexBuffers(System.Int32 i, System.Int32 index_X4, System.Boolean isVolumetric) { }
        // RVA: 0x0B3246E4  token: 0x60005F7
        protected virtual System.Void FillSpriteVertexBuffers(System.Int32 i, System.Int32 index_X4) { }
        // RVA: 0x043C9F40  token: 0x60005F8
        protected virtual System.Void DrawUnderlineMesh(UnityEngine.Vector3 start, UnityEngine.Vector3 end, System.Int32& index, System.Single startScale, System.Single endScale, System.Single maxScale, System.Single sdfScale, UnityEngine.Color32 underlineColor) { }
        // RVA: 0x0B327864  token: 0x60005F9
        protected System.Void PrepareCharSlots(TMPro.PhoneticData data) { }
        // RVA: 0x0B327540  token: 0x60005FA
        protected virtual System.Single PhoneticWidth(TMPro.PhoneticData data) { }
        // RVA: 0x0B3223E4  token: 0x60005FB
        protected virtual System.Void DrawPhoneticMesh(UnityEngine.Rect rect, TMPro.PhoneticData data, System.Single padding, System.Single style_padding, System.Boolean justify, System.Single lossyScale) { }
        // RVA: 0x0B323618  token: 0x60005FC
        protected virtual System.Void DrawTextHighlight(UnityEngine.Vector3 start, UnityEngine.Vector3 end, System.Int32& index, UnityEngine.Color32 highlightColor) { }
        // RVA: 0x0351F1C0  token: 0x60005FD
        protected System.Void LoadDefaultSettings() { }
        // RVA: 0x0333AE40  token: 0x60005FE
        protected System.Void GetSpecialCharacters(TMPro.TMP_FontAsset fontAsset) { }
        // RVA: 0x0333AC00  token: 0x60005FF
        protected System.Void GetEllipsisSpecialCharacter(TMPro.TMP_FontAsset fontAsset) { }
        // RVA: 0x0333AB20  token: 0x6000600
        protected System.Void GetUnderlineSpecialCharacter(TMPro.TMP_FontAsset fontAsset) { }
        // RVA: 0x0B328D4C  token: 0x6000601
        protected System.Void ReplaceTagWithCharacter(System.Int32[] chars, System.Int32 insertionIndex, System.Int32 tagLength, System.Char c) { }
        // RVA: 0x0B324F40  token: 0x6000602
        protected TMPro.TMP_FontAsset GetFontAssetForWeight(System.Int32 fontWeight) { }
        // RVA: 0x0333E640  token: 0x6000603
        private TMPro.TMP_TextElement GetTextElement(System.UInt32 unicode, TMPro.TMP_FontAsset fontAsset, TMPro.FontStyles fontStyle, TMPro.FontWeight fontWeight, System.Boolean& isUsingAlternativeTypeface) { }
        // RVA: 0x03519E20  token: 0x6000604
        public System.Void TryRemoveDynamicFallbackFontAssetTextRef() { }
        // RVA: 0x041E1670  token: 0x6000605
        protected virtual System.Void SetActiveSubMeshes(System.Boolean state) { }
        // RVA: 0x041E1670  token: 0x6000606
        protected virtual System.Void DestroySubMeshObjects() { }
        // RVA: 0x041E1670  token: 0x6000607
        public virtual System.Void ClearMesh() { }
        // RVA: 0x041E1670  token: 0x6000608
        public virtual System.Void ClearMesh(System.Boolean uploadGeometry) { }
        // RVA: 0x0B32516C  token: 0x6000609
        public virtual System.String GetParsedText() { }
        // RVA: 0x0B327478  token: 0x600060A
        private System.Boolean IsSelfOrLinkedAncestor(TMPro.TMP_Text targetTextComponent) { }
        // RVA: 0x0B327DE4  token: 0x600060B
        private System.Void ReleaseLinkedTextComponent(TMPro.TMP_Text targetTextComponent) { }
        // RVA: 0x04DB9F20  token: 0x600060C
        protected UnityEngine.Vector2 PackUV(System.Single x, System.Single y, System.Single scale) { }
        // RVA: 0x04DB9EE0  token: 0x600060D
        protected System.Single PackUV(System.Single x, System.Single y) { }
        // RVA: 0x041E1670  token: 0x600060E
        private virtual System.Void InternalUpdate() { }
        // RVA: 0x03A278A0  token: 0x600060F
        protected static System.Int32 HexToInt(System.Char hex) { }
        // RVA: 0x0B326048  token: 0x6000610
        protected System.Int32 GetUTF16(System.String text, System.Int32 i) { }
        // RVA: 0x0B325F68  token: 0x6000611
        protected System.Int32 GetUTF16(System.Int32[] text, System.Int32 i) { }
        // RVA: 0x0B326208  token: 0x6000612
        private System.Int32 GetUTF16(System.UInt32[] text, System.Int32 i) { }
        // RVA: 0x0B325E80  token: 0x6000613
        protected System.Int32 GetUTF16(System.Text.StringBuilder text, System.Int32 i) { }
        // RVA: 0x0B326130  token: 0x6000614
        private System.Int32 GetUTF16(TMPro.TMP_Text.TextBackingContainer text, System.Int32 i) { }
        // RVA: 0x0B3262E8  token: 0x6000615
        protected System.Int32 GetUTF32(System.String text, System.Int32 i) { }
        // RVA: 0x0B3265D4  token: 0x6000616
        protected System.Int32 GetUTF32(System.Int32[] text, System.Int32 i) { }
        // RVA: 0x0B326460  token: 0x6000617
        private System.Int32 GetUTF32(System.UInt32[] text, System.Int32 i) { }
        // RVA: 0x0B3268A8  token: 0x6000618
        protected System.Int32 GetUTF32(System.Text.StringBuilder text, System.Int32 i) { }
        // RVA: 0x0B326748  token: 0x6000619
        private System.Int32 GetUTF32(TMPro.TMP_Text.TextBackingContainer text, System.Int32 i) { }
        // RVA: 0x03A276C0  token: 0x600061A
        protected static UnityEngine.Color32 HexCharsToColor(System.ReadOnlySpan<System.Char> hexChars, System.Int32 tagCount) { }
        // RVA: 0x0B326A20  token: 0x600061B
        protected static UnityEngine.Color32 HexCharsToColor(System.ReadOnlySpan<System.Char> hexChars, System.Int32 startIndex, System.Int32 length) { }
        // RVA: 0x0B324E94  token: 0x600061C
        private System.Int32 GetAttributeParameters(System.Char[] chars, System.Int32 startIndex, System.Int32 length, System.Single[]& parameters) { }
        // RVA: 0x053948A0  token: 0x600061D
        protected System.Single ConvertToFloat(System.Char[] chars, System.Int32 startIndex, System.Int32 length) { }
        // RVA: 0x044F4280  token: 0x600061E
        protected System.Single ConvertToFloat(System.Char[] chars, System.Int32 startIndex, System.Int32 length, System.Int32& lastIndex) { }
        // RVA: 0x0B322248  token: 0x600061F
        protected System.Boolean CheckIfEndPhonetic(TMPro.TMP_Text.UnicodeChar[] chars, System.Int32 startIndex) { }
        // RVA: 0x03A26780  token: 0x6000620
        protected System.Boolean ValidateHtmlTag(TMPro.TMP_Text.UnicodeChar[] chars, System.Int32 startIndex, System.Int32& endIndex) { }
        // RVA: 0x03695A00  token: 0x6000621
        protected System.Void .ctor() { }
        // RVA: 0x0478E1B0  token: 0x6000622
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000A4  // size: 0x11
    public sealed struct TextElementType
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static TMPro.TextElementType Character;  // const
        public static TMPro.TextElementType Sprite;  // const

    }

    // TypeToken: 0x20000A5  // size: 0x30
    public class TMP_TextElement
    {
        // Fields
        protected TMPro.TextElementType m_ElementType;  // 0x10
        private System.UInt32 m_Unicode;  // 0x14
        private TMPro.TMP_Asset m_TextAsset;  // 0x18
        private UnityEngine.TextCore.Glyph m_Glyph;  // 0x20
        private System.UInt32 m_GlyphIndex;  // 0x28
        private System.Single m_Scale;  // 0x2c

        // Properties
        TMPro.TextElementType elementType { get; /* RVA: 0x015EFCE0 */ }
        System.UInt32 unicode { get; /* RVA: 0x04D882A0 */ set; /* RVA: 0x04D8F540 */ }
        TMPro.TMP_Asset textAsset { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        UnityEngine.TextCore.Glyph glyph { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        System.UInt32 glyphIndex { get; /* RVA: 0x011F2A90 */ set; /* RVA: 0x011F2AA0 */ }
        System.Single scale { get; /* RVA: 0x04D86AF0 */ set; /* RVA: 0x04D86B00 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x600063B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000A6  // size: 0x38
    public class TMP_TextElement_Legacy
    {
        // Fields
        public System.Int32 id;  // 0x10
        public System.Single x;  // 0x14
        public System.Single y;  // 0x18
        public System.Single width;  // 0x1c
        public System.Single height;  // 0x20
        public System.Single xOffset;  // 0x24
        public System.Single yOffset;  // 0x28
        public System.Single xAdvance;  // 0x2c
        public System.Single scale;  // 0x30

        // Methods
        // RVA: 0x041E1670  token: 0x600063C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000A7  // size: 0x78
    public class TMP_TextInfo : System.IDisposable
    {
        // Fields
        private static UnityEngine.Vector2 k_InfinityVectorPositive;  // static @ 0x0
        private static UnityEngine.Vector2 k_InfinityVectorNegative;  // static @ 0x8
        public TMPro.TMP_Text textComponent;  // 0x10
        public System.Int32 characterCount;  // 0x18
        public System.Int32 spriteCount;  // 0x1c
        public System.Int32 spaceCount;  // 0x20
        public System.Int32 wordCount;  // 0x24
        public System.Int32 linkCount;  // 0x28
        public System.Int32 lineCount;  // 0x2c
        public System.Int32 pageCount;  // 0x30
        public System.Int32 materialCount;  // 0x34
        public System.Int32 characterToSubstituteIndex;  // 0x38
        public TMPro.TMP_CharacterInfo[] characterInfo;  // 0x40
        public TMPro.TMP_WordInfo[] wordInfo;  // 0x48
        public TMPro.TMP_LinkInfo[] linkInfo;  // 0x50
        public TMPro.TMP_LineInfo[] lineInfo;  // 0x58
        public TMPro.TMP_PageInfo[] pageInfo;  // 0x60
        public TMPro.TMP_MeshInfo[] meshInfo;  // 0x68
        private TMPro.TMP_MeshInfo[] m_CachedMeshInfo;  // 0x70

        // Methods
        // RVA: 0x0B333EEC  token: 0x600063D
        public System.Void .ctor() { }
        // RVA: 0x0B33409C  token: 0x600063E
        private System.Void .ctor(System.Int32 characterCount) { }
        // RVA: 0x0351F3D0  token: 0x600063F
        public System.Void .ctor(TMPro.TMP_Text textComponent) { }
        // RVA: 0x03EE0820  token: 0x6000640
        public System.Void Clear() { }
        // RVA: 0x0B3336A4  token: 0x6000641
        private System.Void ClearAllData() { }
        // RVA: 0x0B333864  token: 0x6000642
        public System.Void ClearMeshInfo(System.Boolean updateMesh) { }
        // RVA: 0x0B3337E4  token: 0x6000643
        public System.Void ClearAllMeshInfo() { }
        // RVA: 0x0B333E5C  token: 0x6000644
        public System.Void ResetVertexLayout(System.Boolean isVolumetric) { }
        // RVA: 0x0B3338F4  token: 0x6000645
        public System.Void ClearUnusedVertices(TMPro.MaterialReference[] materials) { }
        // RVA: 0x032527E0  token: 0x6000646
        public System.Void ClearLineInfo() { }
        // RVA: 0x03BB6530  token: 0x6000647
        public System.Void ClearPageInfo() { }
        // RVA: 0x0B333974  token: 0x6000648
        public TMPro.TMP_MeshInfo[] CopyMeshInfoVertexData() { }
        // RVA: -1  // generic def  token: 0x6000649
        public static System.Void Resize(T[]& array, System.Int32 size) { }
        // RVA: -1  // generic def  token: 0x600064A
        public static System.Void Resize(T[]& array, System.Int32 size, System.Boolean isBlockAllocated) { }
        // RVA: 0x03518C30  token: 0x600064B
        public virtual System.Void Dispose() { }
        // RVA: 0x04D561D0  token: 0x600064C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000A8  // size: 0x10
    public class TMP_TextParsingUtilities
    {
        // Fields
        private static readonly TMPro.TMP_TextParsingUtilities s_Instance;  // static @ 0x0
        private static System.String k_LookupStringL;  // const
        private static System.String k_LookupStringU;  // const

        // Properties
        TMPro.TMP_TextParsingUtilities instance { get; /* RVA: 0x0B3343E8 */ }

        // Methods
        // RVA: 0x04D4D9B0  token: 0x600064D
        private static System.Void .cctor() { }
        // RVA: 0x0413A410  token: 0x600064F
        public static System.Int32 GetHashCode(System.String s) { }
        // RVA: 0x0B334254  token: 0x6000650
        public static System.Int32 GetHashCodeCaseSensitive(System.String s) { }
        // RVA: 0x0B3342D8  token: 0x6000651
        public static System.Char ToLowerASCIIFast(System.Char c) { }
        // RVA: 0x0413A4C0  token: 0x6000652
        public static System.Char ToUpperASCIIFast(System.Char c) { }
        // RVA: 0x0B33438C  token: 0x6000653
        public static System.UInt32 ToUpperASCIIFast(System.UInt32 c) { }
        // RVA: 0x0B334330  token: 0x6000654
        public static System.UInt32 ToLowerASCIIFast(System.UInt32 c) { }
        // RVA: 0x0B3342A8  token: 0x6000655
        public static System.Boolean IsHighSurrogate(System.UInt32 c) { }
        // RVA: 0x0B3342C0  token: 0x6000656
        public static System.Boolean IsLowSurrogate(System.UInt32 c) { }
        // RVA: 0x04DBA3D0  token: 0x6000657
        private static System.UInt32 ConvertToUTF32(System.UInt32 highSurrogate, System.UInt32 lowSurrogate) { }
        // RVA: 0x041E1670  token: 0x6000658
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000A9  // size: 0x1A
    public sealed struct TMP_FontStyleStack
    {
        // Fields
        public System.Byte bold;  // 0x10
        public System.Byte italic;  // 0x11
        public System.Byte underline;  // 0x12
        public System.Byte strikethrough;  // 0x13
        public System.Byte highlight;  // 0x14
        public System.Byte superscript;  // 0x15
        public System.Byte subscript;  // 0x16
        public System.Byte uppercase;  // 0x17
        public System.Byte lowercase;  // 0x18
        public System.Byte smallcaps;  // 0x19

        // Methods
        // RVA: 0x0333C3A0  token: 0x6000659
        public System.Void Clear() { }
        // RVA: 0x03A279C0  token: 0x600065A
        public System.Byte Add(TMPro.FontStyles style) { }
        // RVA: 0x03A279F0  token: 0x600065B
        public System.Byte Remove(TMPro.FontStyles style) { }

    }

    // TypeToken: 0x20000AA
    public sealed struct TMP_TextProcessingStack`1 : System.IDisposable
    {
        // Fields
        public T[] itemStack;  // 0x0
        public System.Int32 index;  // 0x0
        private T m_DefaultItem;  // 0x0
        private System.Int32 m_Capacity;  // 0x0
        private System.Int32 m_RolloverSize;  // 0x0
        private System.Int32 m_Count;  // 0x0
        private static System.Int32 k_DefaultCapacity;  // const

        // Properties
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        T current { get; /* RVA: -1  // not resolved */ }
        System.Int32 rolloverSize { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600065C
        public System.Void .ctor(T[] stack) { }
        // RVA: -1  // not resolved  token: 0x600065D
        public System.Void .ctor(System.Int32 capacity) { }
        // RVA: -1  // not resolved  token: 0x600065E
        public System.Void .ctor(System.Int32 capacity, System.Int32 rolloverSize) { }
        // RVA: -1  // not resolved  token: 0x6000663
        private static System.Void SetDefault(TMPro.TMP_TextProcessingStack<T>[] stack, T item) { }
        // RVA: -1  // not resolved  token: 0x6000664
        public System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x6000665
        public System.Void SetDefault(T item) { }
        // RVA: -1  // not resolved  token: 0x6000666
        public System.Void Add(T item) { }
        // RVA: -1  // not resolved  token: 0x6000667
        public T Remove() { }
        // RVA: -1  // not resolved  token: 0x6000668
        public System.Void Push(T item) { }
        // RVA: -1  // not resolved  token: 0x6000669
        public T Pop() { }
        // RVA: -1  // not resolved  token: 0x600066A
        public T Peek() { }
        // RVA: -1  // not resolved  token: 0x600066B
        public T CurrentItem() { }
        // RVA: -1  // not resolved  token: 0x600066C
        public T PreviousItem() { }
        // RVA: -1  // not resolved  token: 0x600066D
        public System.Void RestoreFrom(TMPro.TMP_TextProcessingStack<T> other) { }
        // RVA: -1  // not resolved  token: 0x600066E
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x20000AB
    public sealed struct TMP_TextProcessingQueue`1 : System.IDisposable
    {
        // Fields
        public T[] itemQueue;  // 0x0
        private System.Int32 m_Head;  // 0x0
        private System.Int32 m_Tail;  // 0x0
        private System.Int32 m_Capacity;  // 0x0
        private System.Int32 m_Count;  // 0x0
        private T m_DefaultItem;  // 0x0
        private static System.Int32 k_DefaultCapacity;  // const

        // Properties
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600066F
        public System.Void .ctor(T[] queue) { }
        // RVA: -1  // not resolved  token: 0x6000670
        public System.Void .ctor(System.Int32 capacity) { }
        // RVA: -1  // not resolved  token: 0x6000672
        public System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x6000673
        public System.Void SetDefault(T item) { }
        // RVA: -1  // not resolved  token: 0x6000674
        public System.Void Enqueue(T item) { }
        // RVA: -1  // not resolved  token: 0x6000675
        public T Dequeue() { }
        // RVA: -1  // not resolved  token: 0x6000676
        public T Peek() { }
        // RVA: -1  // not resolved  token: 0x6000677
        private System.Void Resize(System.Int32 newCapacity) { }
        // RVA: -1  // not resolved  token: 0x6000678
        public System.Void RestoreFrom(TMPro.TMP_TextProcessingQueue<T> other) { }
        // RVA: -1  // not resolved  token: 0x6000679
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x20000AC  // size: 0x14
    public sealed struct CaretPosition
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.CaretPosition None;  // const
        public static TMPro.CaretPosition Left;  // const
        public static TMPro.CaretPosition Right;  // const

    }

    // TypeToken: 0x20000AD  // size: 0x18
    public sealed struct CaretInfo
    {
        // Fields
        public System.Int32 index;  // 0x10
        public TMPro.CaretPosition position;  // 0x14

        // Methods
        // RVA: 0x04D85C40  token: 0x600067A
        public System.Void .ctor(System.Int32 index, TMPro.CaretPosition position) { }

    }

    // TypeToken: 0x20000AE  // size: 0x10
    public static class TMP_TextUtilities
    {
        // Fields
        private static UnityEngine.Vector3[] m_rectWorldCorners;  // static @ 0x0
        private static System.String k_lookupStringL;  // const
        private static System.String k_lookupStringU;  // const

        // Methods
        // RVA: 0x0B337934  token: 0x600067B
        public static System.Int32 GetCursorIndexFromPosition(TMPro.TMP_Text textComponent, UnityEngine.Vector3 position, UnityEngine.Camera camera) { }
        // RVA: 0x0B337AE8  token: 0x600067C
        public static System.Int32 GetCursorIndexFromPosition(TMPro.TMP_Text textComponent, UnityEngine.Vector3 position, UnityEngine.Camera camera, TMPro.CaretPosition& cursor) { }
        // RVA: 0x0B3361E8  token: 0x600067D
        public static System.Int32 FindNearestLine(TMPro.TMP_Text text, UnityEngine.Vector3 position, UnityEngine.Camera camera) { }
        // RVA: 0x0B335900  token: 0x600067E
        public static System.Int32 FindNearestCharacterOnLine(TMPro.TMP_Text text, UnityEngine.Vector3 position, System.Int32 line, UnityEngine.Camera camera, System.Boolean visibleOnly) { }
        // RVA: 0x0B338164  token: 0x600067F
        public static System.Boolean IsIntersectingRectTransform(UnityEngine.RectTransform rectTransform, UnityEngine.Vector3 position, UnityEngine.Camera camera) { }
        // RVA: 0x0B334610  token: 0x6000680
        public static System.Int32 FindIntersectingCharacter(TMPro.TMP_Text text, UnityEngine.Vector3 position, UnityEngine.Camera camera, System.Boolean visibleOnly) { }
        // RVA: 0x0B335D9C  token: 0x6000681
        public static System.Int32 FindNearestCharacter(TMPro.TMP_Text text, UnityEngine.Vector3 position, UnityEngine.Camera camera, System.Boolean visibleOnly) { }
        // RVA: 0x0B3351DC  token: 0x6000682
        public static System.Int32 FindIntersectingWord(TMPro.TMP_Text text, UnityEngine.Vector3 position, UnityEngine.Camera camera) { }
        // RVA: 0x0B336E88  token: 0x6000683
        public static System.Int32 FindNearestWord(TMPro.TMP_Text text, UnityEngine.Vector3 position, UnityEngine.Camera camera) { }
        // RVA: 0x0B3348E0  token: 0x6000684
        public static System.Int32 FindIntersectingLine(TMPro.TMP_Text text, UnityEngine.Vector3 position, UnityEngine.Camera camera) { }
        // RVA: 0x0B33514C  token: 0x6000685
        public static System.Int32 FindIntersectingLink(TMPro.TMP_Text text, UnityEngine.Vector3 position, UnityEngine.Camera camera) { }
        // RVA: 0x0B334A70  token: 0x6000686
        public static System.Int32 FindIntersectingLink(TMPro.TMP_Text text, UnityEngine.Vector3 position, UnityEngine.Camera camera, UnityEngine.Vector2 linkExtraPadding) { }
        // RVA: 0x04DBA3E0  token: 0x6000687
        private static System.Single CalculateTouchLinkDistanceScore(UnityEngine.Vector2 touchPos, UnityEngine.Vector2 linkCenter) { }
        // RVA: 0x0B3363E4  token: 0x6000688
        public static System.Int32 FindNearestLink(TMPro.TMP_Text text, UnityEngine.Vector3 position, UnityEngine.Camera camera) { }
        // RVA: 0x0B33834C  token: 0x6000689
        private static System.Boolean PointIntersectRectangle(UnityEngine.Vector3 m, UnityEngine.Vector3 a, UnityEngine.Vector3 b, UnityEngine.Vector3 c, UnityEngine.Vector3 d) { }
        // RVA: 0x0B338518  token: 0x600068A
        public static System.Boolean ScreenPointToWorldPointInRectangle(UnityEngine.Transform transform, UnityEngine.Vector2 screenPoint, UnityEngine.Camera cam, UnityEngine.Vector3& worldPoint) { }
        // RVA: 0x0B337F40  token: 0x600068B
        private static System.Boolean IntersectLinePlane(TMPro.TMP_TextUtilities.LineSegment line, UnityEngine.Vector3 point, UnityEngine.Vector3 normal, UnityEngine.Vector3& intersectingPoint) { }
        // RVA: 0x0B334438  token: 0x600068C
        public static System.Single DistanceToLine(UnityEngine.Vector3 a, UnityEngine.Vector3 b, UnityEngine.Vector3 point) { }
        // RVA: 0x0B33878C  token: 0x600068D
        public static System.Char ToLowerFast(System.Char c) { }
        // RVA: 0x0B3387E4  token: 0x600068E
        public static System.Char ToUpperFast(System.Char c) { }
        // RVA: 0x041114E0  token: 0x600068F
        private static System.UInt32 ToUpperASCIIFast(System.UInt32 c) { }
        // RVA: 0x0B337D28  token: 0x6000690
        public static System.Int32 GetHashCode(System.String s) { }
        // RVA: 0x039403C0  token: 0x6000691
        public static System.Int32 GetSimpleHashCode(System.String s) { }
        // RVA: 0x0B337DB8  token: 0x6000692
        public static System.UInt32 GetSimpleHashCodeLowercase(System.String s) { }
        // RVA: 0x0B337E4C  token: 0x6000693
        public static System.Int32 HexToInt(System.Char hex) { }
        // RVA: 0x0B3386DC  token: 0x6000694
        public static System.Int32 StringHexToInt(System.String s) { }
        // RVA: 0x03A22940  token: 0x6000695
        public static System.Boolean IsThaiZeroWidthCharacter(System.Char c) { }
        // RVA: 0x03A22A30  token: 0x6000696
        public static System.Boolean IsUpVowel(System.Char c) { }
        // RVA: 0x03A229D0  token: 0x6000697
        public static System.Boolean IsLowVowel(System.Char c) { }
        // RVA: 0x03A22A00  token: 0x6000698
        public static System.Boolean IsToneMark(System.Char c) { }
        // RVA: 0x03CF12C0  token: 0x6000699
        public static System.Boolean IsAscenderThai(System.Char c) { }
        // RVA: 0x03E14980  token: 0x600069A
        public static System.Boolean IsDescenderThai(System.Char c) { }
        // RVA: 0x04D36370  token: 0x600069B
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000B0  // size: 0x50
    public class TMP_UpdateManager
    {
        // Fields
        private static TMPro.TMP_UpdateManager s_Instance;  // static @ 0x0
        private readonly System.Collections.Generic.HashSet<System.Int32> m_LayoutQueueLookup;  // 0x10
        private readonly System.Collections.Generic.List<TMPro.TMP_Text> m_LayoutRebuildQueue;  // 0x18
        private readonly System.Collections.Generic.HashSet<System.Int32> m_GraphicQueueLookup;  // 0x20
        private readonly System.Collections.Generic.List<TMPro.TMP_Text> m_GraphicRebuildQueue;  // 0x28
        private readonly System.Collections.Generic.HashSet<System.Int32> m_InternalUpdateLookup;  // 0x30
        private readonly System.Collections.Generic.List<TMPro.TMP_Text> m_InternalUpdateQueue;  // 0x38
        private readonly System.Collections.Generic.HashSet<System.Int32> m_CullingUpdateLookup;  // 0x40
        private readonly System.Collections.Generic.List<TMPro.TMP_Text> m_CullingUpdateQueue;  // 0x48
        private static Unity.Profiling.ProfilerMarker k_RegisterTextObjectForUpdateMarker;  // static @ 0x8
        private static Unity.Profiling.ProfilerMarker k_RegisterTextElementForGraphicRebuildMarker;  // static @ 0x10
        private static Unity.Profiling.ProfilerMarker k_RegisterTextElementForCullingUpdateMarker;  // static @ 0x18
        private static Unity.Profiling.ProfilerMarker k_UnregisterTextObjectForUpdateMarker;  // static @ 0x20
        private static Unity.Profiling.ProfilerMarker k_UnregisterTextElementForGraphicRebuildMarker;  // static @ 0x28

        // Properties
        TMPro.TMP_UpdateManager instance { get; /* RVA: 0x0351C7D0 */ }

        // Methods
        // RVA: 0x04992D70  token: 0x600069E
        private System.Void .ctor() { }
        // RVA: 0x0351C6F0  token: 0x600069F
        private static System.Void RegisterTextObjectForUpdate(TMPro.TMP_Text textObject) { }
        // RVA: 0x0351C920  token: 0x60006A0
        private System.Void InternalRegisterTextObjectForUpdate(TMPro.TMP_Text textObject) { }
        // RVA: 0x0B33988C  token: 0x60006A1
        public static System.Void RegisterTextElementForLayoutRebuild(TMPro.TMP_Text element) { }
        // RVA: 0x0B339648  token: 0x60006A2
        private System.Void InternalRegisterTextElementForLayoutRebuild(TMPro.TMP_Text element) { }
        // RVA: 0x0B339834  token: 0x60006A3
        public static System.Void RegisterTextElementForGraphicRebuild(TMPro.TMP_Text element) { }
        // RVA: 0x0B33958C  token: 0x60006A4
        private System.Void InternalRegisterTextElementForGraphicRebuild(TMPro.TMP_Text element) { }
        // RVA: 0x03EFA6E0  token: 0x60006A5
        public static System.Void RegisterTextElementForCullingUpdate(TMPro.TMP_Text element) { }
        // RVA: 0x03EFA730  token: 0x60006A6
        private System.Void InternalRegisterTextElementForCullingUpdate(TMPro.TMP_Text element) { }
        // RVA: 0x0B33982C  token: 0x60006A7
        private System.Void OnCameraPreCull() { }
        // RVA: 0x031BC2C0  token: 0x60006A8
        private System.Void DoRebuilds() { }
        // RVA: 0x0351C740  token: 0x60006A9
        private static System.Void UnRegisterTextObjectForUpdate(TMPro.TMP_Text textObject) { }
        // RVA: 0x0B3398E4  token: 0x60006AA
        public static System.Void UnRegisterTextElementForRebuild(TMPro.TMP_Text element) { }
        // RVA: 0x0B339704  token: 0x60006AB
        private System.Void InternalUnRegisterTextElementForGraphicRebuild(TMPro.TMP_Text element) { }
        // RVA: 0x0B339798  token: 0x60006AC
        private System.Void InternalUnRegisterTextElementForLayoutRebuild(TMPro.TMP_Text element) { }
        // RVA: 0x0351C8B0  token: 0x60006AD
        private System.Void InternalUnRegisterTextObjectForUpdate(TMPro.TMP_Text textObject) { }
        // RVA: 0x04B1D650  token: 0x60006AE
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000B1  // size: 0x30
    public class TMP_UpdateRegistry
    {
        // Fields
        private static TMPro.TMP_UpdateRegistry s_Instance;  // static @ 0x0
        private readonly System.Collections.Generic.List<UnityEngine.UI.ICanvasElement> m_LayoutRebuildQueue;  // 0x10
        private System.Collections.Generic.HashSet<System.Int32> m_LayoutQueueLookup;  // 0x18
        private readonly System.Collections.Generic.List<UnityEngine.UI.ICanvasElement> m_GraphicRebuildQueue;  // 0x20
        private System.Collections.Generic.HashSet<System.Int32> m_GraphicQueueLookup;  // 0x28

        // Properties
        TMPro.TMP_UpdateRegistry instance { get; /* RVA: 0x0B33A0AC */ }

        // Methods
        // RVA: 0x0B339F38  token: 0x60006B0
        protected System.Void .ctor() { }
        // RVA: 0x0B339EC0  token: 0x60006B1
        public static System.Void RegisterCanvasElementForLayoutRebuild(UnityEngine.UI.ICanvasElement element) { }
        // RVA: 0x0B339A5C  token: 0x60006B2
        private System.Boolean InternalRegisterCanvasElementForLayoutRebuild(UnityEngine.UI.ICanvasElement element) { }
        // RVA: 0x0B339E90  token: 0x60006B3
        public static System.Void RegisterCanvasElementForGraphicRebuild(UnityEngine.UI.ICanvasElement element) { }
        // RVA: 0x0B339970  token: 0x60006B4
        private System.Boolean InternalRegisterCanvasElementForGraphicRebuild(UnityEngine.UI.ICanvasElement element) { }
        // RVA: 0x0B339CD8  token: 0x60006B5
        private System.Void PerformUpdateForCanvasRendererObjects() { }
        // RVA: 0x0B339E3C  token: 0x60006B6
        private System.Void PerformUpdateForMeshRendererObjects() { }
        // RVA: 0x0B339EF0  token: 0x60006B7
        public static System.Void UnRegisterCanvasElementForRebuild(UnityEngine.UI.ICanvasElement element) { }
        // RVA: 0x0B339C10  token: 0x60006B8
        private System.Void InternalUnRegisterCanvasElementForLayoutRebuild(UnityEngine.UI.ICanvasElement element) { }
        // RVA: 0x0B339B48  token: 0x60006B9
        private System.Void InternalUnRegisterCanvasElementForGraphicRebuild(UnityEngine.UI.ICanvasElement element) { }

    }

}

namespace TMPro.SpriteAssetUtilities
{

    // TypeToken: 0x20000B2  // size: 0x14
    public sealed struct SpriteAssetImportFormats
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static TMPro.SpriteAssetUtilities.SpriteAssetImportFormats None;  // const
        public static TMPro.SpriteAssetUtilities.SpriteAssetImportFormats TexturePackerJsonArray;  // const

    }

    // TypeToken: 0x20000B3  // size: 0x10
    public class TexturePacker_JsonArray
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60006BA
        public System.Void .ctor() { }

    }

}

