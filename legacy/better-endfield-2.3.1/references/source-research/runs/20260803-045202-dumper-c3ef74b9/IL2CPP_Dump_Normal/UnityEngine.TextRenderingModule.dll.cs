// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.TextRenderingModule.dll
// Classes:  14
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x200000E  // size: 0x80
    public sealed class FontTextureRebuildCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x053DEDC8  token: 0x600002E
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x053DEDB8  token: 0x600002F
        public virtual System.Void Invoke() { }

    }

namespace UnityEngine
{

    // TypeToken: 0x2000002  // size: 0x14
    public sealed struct FontStyle
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.FontStyle Normal;  // const
        public static UnityEngine.FontStyle Bold;  // const
        public static UnityEngine.FontStyle Italic;  // const
        public static UnityEngine.FontStyle BoldAndItalic;  // const

    }

    // TypeToken: 0x2000003  // size: 0x14
    public sealed struct TextGenerationError
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.TextGenerationError None;  // const
        public static UnityEngine.TextGenerationError CustomSizeOnNonDynamicFont;  // const
        public static UnityEngine.TextGenerationError CustomStyleOnNonDynamicFont;  // const
        public static UnityEngine.TextGenerationError NoFont;  // const

    }

    // TypeToken: 0x2000004  // size: 0x70
    public sealed struct TextGenerationSettings
    {
        // Fields
        public UnityEngine.Font font;  // 0x10
        public UnityEngine.Color color;  // 0x18
        public System.Int32 fontSize;  // 0x28
        public System.Single lineSpacing;  // 0x2c
        public System.Boolean richText;  // 0x30
        public System.Single scaleFactor;  // 0x34
        public UnityEngine.FontStyle fontStyle;  // 0x38
        public UnityEngine.TextAnchor textAnchor;  // 0x3c
        public System.Boolean alignByGeometry;  // 0x40
        public System.Boolean resizeTextForBestFit;  // 0x41
        public System.Int32 resizeTextMinSize;  // 0x44
        public System.Int32 resizeTextMaxSize;  // 0x48
        public System.Boolean updateBounds;  // 0x4c
        public UnityEngine.VerticalWrapMode verticalOverflow;  // 0x50
        public UnityEngine.HorizontalWrapMode horizontalOverflow;  // 0x54
        public UnityEngine.Vector2 generationExtents;  // 0x58
        public UnityEngine.Vector2 pivot;  // 0x60
        public System.Boolean generateOutOfBounds;  // 0x68

        // Methods
        // RVA: 0x0B45D438  token: 0x6000001
        private System.Boolean CompareColors(UnityEngine.Color left, UnityEngine.Color right) { }
        // RVA: 0x0B45D538  token: 0x6000002
        private System.Boolean CompareVector2(UnityEngine.Vector2 left, UnityEngine.Vector2 right) { }
        // RVA: 0x0B45D5E0  token: 0x6000003
        public System.Boolean Equals(UnityEngine.TextGenerationSettings other) { }

    }

    // TypeToken: 0x2000005  // size: 0xA8
    public sealed class TextGenerator : System.IDisposable
    {
        // Fields
        private System.IntPtr m_Ptr;  // 0x10
        private System.String m_LastString;  // 0x18
        private UnityEngine.TextGenerationSettings m_LastSettings;  // 0x20
        private System.Boolean m_HasGenerated;  // 0x80
        private UnityEngine.TextGenerationError m_LastValid;  // 0x84
        private readonly System.Collections.Generic.List<UnityEngine.UIVertex> m_Verts;  // 0x88
        private readonly System.Collections.Generic.List<UnityEngine.UICharInfo> m_Characters;  // 0x90
        private readonly System.Collections.Generic.List<UnityEngine.UILineInfo> m_Lines;  // 0x98
        private System.Boolean m_CachedVerts;  // 0xa0
        private System.Boolean m_CachedCharacters;  // 0xa1
        private System.Boolean m_CachedLines;  // 0xa2

        // Properties
        System.Int32 characterCountVisible { get; /* RVA: 0x0B45E85C */ }
        System.Collections.Generic.IList<UnityEngine.UIVertex> verts { get; /* RVA: 0x0B45E9C8 */ }
        System.Collections.Generic.IList<UnityEngine.UICharInfo> characters { get; /* RVA: 0x0B45E8A4 */ }
        System.Collections.Generic.IList<UnityEngine.UILineInfo> lines { get; /* RVA: 0x0B45E918 */ }
        UnityEngine.Rect rectExtents { get; /* RVA: 0x0B45E99C */ }
        System.Int32 characterCount { get; /* RVA: 0x0B45E870 */ }
        System.Int32 lineCount { get; /* RVA: 0x0B45E8E4 */ }

        // Methods
        // RVA: 0x0B45E84C  token: 0x6000004
        public System.Void .ctor() { }
        // RVA: 0x0B45E708  token: 0x6000005
        public System.Void .ctor(System.Int32 initialCapacity) { }
        // RVA: 0x0B45D868  token: 0x6000006
        protected virtual System.Void Finalize() { }
        // RVA: 0x0B45E480  token: 0x6000007
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0B45E4AC  token: 0x6000009
        private UnityEngine.TextGenerationSettings ValidatedSettings(UnityEngine.TextGenerationSettings settings) { }
        // RVA: 0x04DBD6E0  token: 0x600000A
        public System.Void Invalidate() { }
        // RVA: 0x0B45D944  token: 0x600000B
        public System.Void GetCharacters(System.Collections.Generic.List<UnityEngine.UICharInfo> characters) { }
        // RVA: 0x0B45D990  token: 0x600000C
        public System.Void GetLines(System.Collections.Generic.List<UnityEngine.UILineInfo> lines) { }
        // RVA: 0x0B45DADC  token: 0x600000D
        public System.Void GetVertices(System.Collections.Generic.List<UnityEngine.UIVertex> vertices) { }
        // RVA: 0x0B45DA14  token: 0x600000E
        public System.Single GetPreferredWidth(System.String str, UnityEngine.TextGenerationSettings settings) { }
        // RVA: 0x0B45D998  token: 0x600000F
        public System.Single GetPreferredHeight(System.String str, UnityEngine.TextGenerationSettings settings) { }
        // RVA: 0x0B45DED0  token: 0x6000010
        public System.Boolean PopulateWithErrors(System.String str, UnityEngine.TextGenerationSettings settings, UnityEngine.GameObject context) { }
        // RVA: 0x0B45E428  token: 0x6000011
        public System.Boolean Populate(System.String str, UnityEngine.TextGenerationSettings settings) { }
        // RVA: 0x0B45DDE4  token: 0x6000012
        private UnityEngine.TextGenerationError PopulateWithError(System.String str, UnityEngine.TextGenerationSettings settings) { }
        // RVA: 0x0B45DB44  token: 0x6000013
        private UnityEngine.TextGenerationError PopulateAlways(System.String str, UnityEngine.TextGenerationSettings settings) { }
        // RVA: 0x0B45DAE4  token: 0x600001A
        private static System.IntPtr Internal_Create() { }
        // RVA: 0x0B45DB10  token: 0x600001B
        private static System.Void Internal_Destroy(System.IntPtr ptr) { }
        // RVA: 0x0B45E2B8  token: 0x600001C
        private System.Boolean Populate_Internal(System.String str, UnityEngine.Font font, UnityEngine.Color color, System.Int32 fontSize, System.Single scaleFactor, System.Single lineSpacing, UnityEngine.FontStyle style, System.Boolean richText, System.Boolean resizeTextForBestFit, System.Int32 resizeTextMinSize, System.Int32 resizeTextMaxSize, System.Int32 verticalOverFlow, System.Int32 horizontalOverflow, System.Boolean updateBounds, UnityEngine.TextAnchor anchor, System.Single extentsX, System.Single extentsY, System.Single pivotX, System.Single pivotY, System.Boolean generateOutOfBounds, System.Boolean alignByGeometry, System.UInt32& error) { }
        // RVA: 0x0B45E0E8  token: 0x600001D
        private System.Boolean Populate_Internal(System.String str, UnityEngine.Font font, UnityEngine.Color color, System.Int32 fontSize, System.Single scaleFactor, System.Single lineSpacing, UnityEngine.FontStyle style, System.Boolean richText, System.Boolean resizeTextForBestFit, System.Int32 resizeTextMinSize, System.Int32 resizeTextMaxSize, UnityEngine.VerticalWrapMode verticalOverFlow, UnityEngine.HorizontalWrapMode horizontalOverflow, System.Boolean updateBounds, UnityEngine.TextAnchor anchor, UnityEngine.Vector2 extents, UnityEngine.Vector2 pivot, System.Boolean generateOutOfBounds, System.Boolean alignByGeometry, UnityEngine.TextGenerationError& error) { }
        // RVA: 0x0B45DA98  token: 0x600001E
        private System.Void GetVerticesInternal(System.Object vertices) { }
        // RVA: 0x0B45D900  token: 0x600001F
        private System.Void GetCharactersInternal(System.Object characters) { }
        // RVA: 0x0B45D94C  token: 0x6000020
        private System.Void GetLinesInternal(System.Object lines) { }
        // RVA: 0x0B45E958  token: 0x6000021
        private System.Void get_rectExtents_Injected(UnityEngine.Rect& ret) { }
        // RVA: 0x0B45E07C  token: 0x6000022
        private System.Boolean Populate_Internal_Injected(System.String str, UnityEngine.Font font, UnityEngine.Color& color, System.Int32 fontSize, System.Single scaleFactor, System.Single lineSpacing, UnityEngine.FontStyle style, System.Boolean richText, System.Boolean resizeTextForBestFit, System.Int32 resizeTextMinSize, System.Int32 resizeTextMaxSize, System.Int32 verticalOverFlow, System.Int32 horizontalOverflow, System.Boolean updateBounds, UnityEngine.TextAnchor anchor, System.Single extentsX, System.Single extentsY, System.Single pivotX, System.Single pivotY, System.Boolean generateOutOfBounds, System.Boolean alignByGeometry, System.UInt32& error) { }

    }

    // TypeToken: 0x2000006  // size: 0x14
    public sealed struct TextAlignment
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.TextAlignment Left;  // const
        public static UnityEngine.TextAlignment Center;  // const
        public static UnityEngine.TextAlignment Right;  // const

    }

    // TypeToken: 0x2000007  // size: 0x14
    public sealed struct TextAnchor
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.TextAnchor UpperLeft;  // const
        public static UnityEngine.TextAnchor UpperCenter;  // const
        public static UnityEngine.TextAnchor UpperRight;  // const
        public static UnityEngine.TextAnchor MiddleLeft;  // const
        public static UnityEngine.TextAnchor MiddleCenter;  // const
        public static UnityEngine.TextAnchor MiddleRight;  // const
        public static UnityEngine.TextAnchor LowerLeft;  // const
        public static UnityEngine.TextAnchor LowerCenter;  // const
        public static UnityEngine.TextAnchor LowerRight;  // const

    }

    // TypeToken: 0x2000008  // size: 0x14
    public sealed struct HorizontalWrapMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.HorizontalWrapMode Wrap;  // const
        public static UnityEngine.HorizontalWrapMode Overflow;  // const

    }

    // TypeToken: 0x2000009  // size: 0x14
    public sealed struct VerticalWrapMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.VerticalWrapMode Truncate;  // const
        public static UnityEngine.VerticalWrapMode Overflow;  // const

    }

    // TypeToken: 0x200000A  // size: 0x1C
    public sealed struct UICharInfo
    {
        // Fields
        public UnityEngine.Vector2 cursorPos;  // 0x10
        public System.Single charWidth;  // 0x18

    }

    // TypeToken: 0x200000B  // size: 0x20
    public sealed struct UILineInfo
    {
        // Fields
        public System.Int32 startCharIdx;  // 0x10
        public System.Int32 height;  // 0x14
        public System.Single topY;  // 0x18
        public System.Single leading;  // 0x1c

    }

    // TypeToken: 0x200000C  // size: 0x7C
    public sealed struct UIVertex
    {
        // Fields
        public UnityEngine.Vector3 position;  // 0x10
        public UnityEngine.Vector3 normal;  // 0x1c
        public UnityEngine.Vector4 tangent;  // 0x28
        public UnityEngine.Color32 color;  // 0x38
        public UnityEngine.Vector4 uv0;  // 0x3c
        public UnityEngine.Vector4 uv1;  // 0x4c
        public UnityEngine.Vector4 uv2;  // 0x5c
        public UnityEngine.Vector4 uv3;  // 0x6c
        private static readonly UnityEngine.Color32 s_DefaultColor;  // static @ 0x0
        private static readonly UnityEngine.Vector4 s_DefaultTangent;  // static @ 0x4
        public static UnityEngine.UIVertex simpleVert;  // static @ 0x14

        // Methods
        // RVA: 0x04970360  token: 0x6000023
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000D  // size: 0x20
    public sealed class Font : UnityEngine.Object
    {
        // Fields
        private static System.Action<UnityEngine.Font> textureRebuilt;  // static @ 0x0
        private UnityEngine.Font.FontTextureRebuildCallback m_FontTextureRebuildCallback;  // 0x18

        // Properties
        UnityEngine.Material material { get; /* RVA: 0x0B45D368 */ }
        System.Boolean dynamic { get; /* RVA: 0x0B45D300 */ }
        System.Int32 fontSize { get; /* RVA: 0x0B45D334 */ }

        // Events
        event System.Action<UnityEngine.Font> textureRebuilt;

        // Methods
        // RVA: 0x0B45D218  token: 0x6000029
        public System.Void .ctor() { }
        // RVA: 0x03D9F240  token: 0x600002A
        private static System.Void InvokeTextureRebuilt_Internal(UnityEngine.Font font) { }
        // RVA: 0x0B45D184  token: 0x600002B
        public System.Boolean HasCharacter(System.Char c) { }
        // RVA: 0x0B45D194  token: 0x600002C
        private System.Boolean HasCharacter(System.Int32 c) { }
        // RVA: 0x0B45D1D4  token: 0x600002D
        private static System.Void Internal_CreateFont(UnityEngine.Font self, System.String name) { }

    }

}

