// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.TextCoreFontEngineModule.dll
// Classes:  20
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
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

namespace UnityEngine.TextCore
{

    // TypeToken: 0x2000004  // size: 0x70
    public sealed struct FaceInfo
    {
        // Fields
        private System.Int32 m_FaceIndex;  // 0x10
        private System.String m_FamilyName;  // 0x18
        private System.String m_StyleName;  // 0x20
        private System.Int32 m_PointSize;  // 0x28
        private System.Single m_Scale;  // 0x2c
        private System.Int32 m_UnitsPerEM;  // 0x30
        private System.Single m_LineHeight;  // 0x34
        private System.Single m_AscentLine;  // 0x38
        private System.Single m_CapLine;  // 0x3c
        private System.Single m_MeanLine;  // 0x40
        private System.Single m_Baseline;  // 0x44
        private System.Single m_DescentLine;  // 0x48
        private System.Single m_SuperscriptOffset;  // 0x4c
        private System.Single m_SuperscriptSize;  // 0x50
        private System.Single m_SubscriptOffset;  // 0x54
        private System.Single m_SubscriptSize;  // 0x58
        private System.Single m_UnderlineOffset;  // 0x5c
        private System.Single m_UnderlineThickness;  // 0x60
        private System.Single m_StrikethroughOffset;  // 0x64
        private System.Single m_StrikethroughThickness;  // 0x68
        private System.Single m_TabWidth;  // 0x6c

        // Properties
        System.Int32 faceIndex { get; /* RVA: 0x04DBA750 */ }
        System.String familyName { get; /* RVA: 0x04DBA980 */ set; /* RVA: 0x067D5E9C */ }
        System.String styleName { get; /* RVA: 0x04D9B600 */ set; /* RVA: 0x053908C0 */ }
        System.Int32 pointSize { get; /* RVA: 0x04D9D860 */ set; /* RVA: 0x011EF9B0 */ }
        System.Single scale { get; /* RVA: 0x04DBD180 */ set; /* RVA: 0x04D86900 */ }
        System.Single lineHeight { get; /* RVA: 0x04DBBE50 */ set; /* RVA: 0x04D89860 */ }
        System.Single ascentLine { get; /* RVA: 0x04DBD360 */ set; /* RVA: 0x04D86C00 */ }
        System.Single capLine { get; /* RVA: 0x04DBD1B0 */ set; /* RVA: 0x04D86B00 */ }
        System.Single meanLine { get; /* RVA: 0x04DB9A40 */ set; /* RVA: 0x04D86E30 */ }
        System.Single baseline { get; /* RVA: 0x053957B0 */ set; /* RVA: 0x04D8E060 */ }
        System.Single descentLine { get; /* RVA: 0x05391630 */ set; /* RVA: 0x04D88370 */ }
        System.Single superscriptOffset { get; /* RVA: 0x05391670 */ set; /* RVA: 0x04D86460 */ }
        System.Single superscriptSize { get; /* RVA: 0x05391640 */ set; /* RVA: 0x04D86470 */ }
        System.Single subscriptOffset { get; /* RVA: 0x05391650 */ set; /* RVA: 0x04D86480 */ }
        System.Single subscriptSize { get; /* RVA: 0x05391660 */ set; /* RVA: 0x04D8D380 */ }
        System.Single underlineOffset { get; /* RVA: 0x04DBD330 */ set; /* RVA: 0x04D91480 */ }
        System.Single underlineThickness { get; /* RVA: 0x04DBD380 */ set; /* RVA: 0x04D88FF0 */ }
        System.Single strikethroughOffset { get; /* RVA: 0x04DBD340 */ set; /* RVA: 0x04D86E10 */ }
        System.Single strikethroughThickness { set; /* RVA: 0x04D88430 */ }
        System.Single tabWidth { get; /* RVA: 0x04DBD370 */ set; /* RVA: 0x04D91510 */ }

    }

    // TypeToken: 0x2000005  // size: 0x14
    public sealed struct GlyphClassDefinitionType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.TextCore.GlyphClassDefinitionType Undefined;  // const
        public static UnityEngine.TextCore.GlyphClassDefinitionType Base;  // const
        public static UnityEngine.TextCore.GlyphClassDefinitionType Ligature;  // const
        public static UnityEngine.TextCore.GlyphClassDefinitionType Mark;  // const
        public static UnityEngine.TextCore.GlyphClassDefinitionType Component;  // const

    }

    // TypeToken: 0x2000006  // size: 0x20
    public sealed struct GlyphRect : System.IEquatable`1
    {
        // Fields
        private System.Int32 m_X;  // 0x10
        private System.Int32 m_Y;  // 0x14
        private System.Int32 m_Width;  // 0x18
        private System.Int32 m_Height;  // 0x1c
        private static readonly UnityEngine.TextCore.GlyphRect s_ZeroGlyphRect;  // static @ 0x0

        // Properties
        System.Int32 x { get; /* RVA: 0x04DBA750 */ }
        System.Int32 y { get; /* RVA: 0x04D9E980 */ }
        System.Int32 width { get; /* RVA: 0x04D9CF30 */ }
        System.Int32 height { get; /* RVA: 0x04D9D1B0 */ }
        UnityEngine.TextCore.GlyphRect zero { get; /* RVA: 0x0393F790 */ }

        // Methods
        // RVA: 0x04D88C10  token: 0x600002E
        public System.Void .ctor(System.Int32 x, System.Int32 y, System.Int32 width, System.Int32 height) { }
        // RVA: 0x0B432C74  token: 0x600002F
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B432C14  token: 0x6000030
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0B432B98  token: 0x6000031
        public virtual System.Boolean Equals(UnityEngine.TextCore.GlyphRect other) { }
        // RVA: 0x04D728C0  token: 0x6000032
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000007  // size: 0x24
    public sealed struct GlyphMetrics : System.IEquatable`1
    {
        // Fields
        private System.Single m_Width;  // 0x10
        private System.Single m_Height;  // 0x14
        private System.Single m_HorizontalBearingX;  // 0x18
        private System.Single m_HorizontalBearingY;  // 0x1c
        private System.Single m_HorizontalAdvance;  // 0x20

        // Properties
        System.Single width { get; /* RVA: 0x04DBA9D0 */ }
        System.Single height { get; /* RVA: 0x04DBA740 */ }
        System.Single horizontalBearingX { get; /* RVA: 0x04DBA730 */ }
        System.Single horizontalBearingY { get; /* RVA: 0x04DBA950 */ }
        System.Single horizontalAdvance { get; /* RVA: 0x04DBA720 */ }

        // Methods
        // RVA: 0x04DBD390  token: 0x6000038
        public System.Void .ctor(System.Single width, System.Single height, System.Single bearingX, System.Single bearingY, System.Single advance) { }
        // RVA: 0x0B4329B4  token: 0x6000039
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B432950  token: 0x600003A
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0B4328C8  token: 0x600003B
        public virtual System.Boolean Equals(UnityEngine.TextCore.GlyphMetrics other) { }

    }

    // TypeToken: 0x2000008  // size: 0x48
    public class Glyph
    {
        // Fields
        private System.UInt32 m_Index;  // 0x10
        private UnityEngine.TextCore.GlyphMetrics m_Metrics;  // 0x14
        private UnityEngine.TextCore.GlyphRect m_GlyphRect;  // 0x28
        private System.Single m_Scale;  // 0x38
        private System.Int32 m_AtlasIndex;  // 0x3c
        private UnityEngine.TextCore.GlyphClassDefinitionType m_ClassDefinitionType;  // 0x40

        // Properties
        System.UInt32 index { get; /* RVA: 0x04DBA9F0 */ set; /* RVA: 0x014F51F0 */ }
        UnityEngine.TextCore.GlyphMetrics metrics { get; /* RVA: 0x04D96D10 */ set; /* RVA: 0x04DBD450 */ }
        UnityEngine.TextCore.GlyphRect glyphRect { get; /* RVA: 0x04DBCFB0 */ set; /* RVA: 0x04D8FAE0 */ }
        System.Single scale { get; /* RVA: 0x05391630 */ set; /* RVA: 0x04D88370 */ }
        System.Int32 atlasIndex { get; /* RVA: 0x04DBA840 */ set; /* RVA: 0x04D86670 */ }
        UnityEngine.TextCore.GlyphClassDefinitionType classDefinitionType { get; /* RVA: 0x04DBA830 */ }

        // Methods
        // RVA: 0x03FC21D0  token: 0x6000047
        public System.Void .ctor() { }
        // RVA: 0x04DBD3F0  token: 0x6000048
        private System.Void .ctor(UnityEngine.TextCore.LowLevel.GlyphMarshallingStruct glyphStruct) { }
        // RVA: 0x04DBD420  token: 0x6000049
        public System.Void .ctor(System.UInt32 index, UnityEngine.TextCore.GlyphMetrics metrics, UnityEngine.TextCore.GlyphRect glyphRect, System.Single scale, System.Int32 atlasIndex) { }

    }

}

namespace UnityEngine.TextCore.LowLevel
{

    // TypeToken: 0x2000009  // size: 0x14
    public sealed struct GlyphLoadFlags
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.TextCore.LowLevel.GlyphLoadFlags LOAD_DEFAULT;  // const
        public static UnityEngine.TextCore.LowLevel.GlyphLoadFlags LOAD_NO_SCALE;  // const
        public static UnityEngine.TextCore.LowLevel.GlyphLoadFlags LOAD_NO_HINTING;  // const
        public static UnityEngine.TextCore.LowLevel.GlyphLoadFlags LOAD_RENDER;  // const
        public static UnityEngine.TextCore.LowLevel.GlyphLoadFlags LOAD_NO_BITMAP;  // const
        public static UnityEngine.TextCore.LowLevel.GlyphLoadFlags LOAD_FORCE_AUTOHINT;  // const
        public static UnityEngine.TextCore.LowLevel.GlyphLoadFlags LOAD_MONOCHROME;  // const
        public static UnityEngine.TextCore.LowLevel.GlyphLoadFlags LOAD_NO_AUTOHINT;  // const
        public static UnityEngine.TextCore.LowLevel.GlyphLoadFlags LOAD_COLOR;  // const
        public static UnityEngine.TextCore.LowLevel.GlyphLoadFlags LOAD_COMPUTE_METRICS;  // const
        public static UnityEngine.TextCore.LowLevel.GlyphLoadFlags LOAD_BITMAP_METRICS_ONLY;  // const

    }

    // TypeToken: 0x200000A  // size: 0x14
    public sealed struct FontEngineError
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.TextCore.LowLevel.FontEngineError Success;  // const
        public static UnityEngine.TextCore.LowLevel.FontEngineError Invalid_File_Path;  // const
        public static UnityEngine.TextCore.LowLevel.FontEngineError Invalid_File_Format;  // const
        public static UnityEngine.TextCore.LowLevel.FontEngineError Invalid_File_Structure;  // const
        public static UnityEngine.TextCore.LowLevel.FontEngineError Invalid_File;  // const
        public static UnityEngine.TextCore.LowLevel.FontEngineError Invalid_Table;  // const
        public static UnityEngine.TextCore.LowLevel.FontEngineError Invalid_Glyph_Index;  // const
        public static UnityEngine.TextCore.LowLevel.FontEngineError Invalid_Character_Code;  // const
        public static UnityEngine.TextCore.LowLevel.FontEngineError Invalid_Pixel_Size;  // const
        public static UnityEngine.TextCore.LowLevel.FontEngineError Invalid_Library;  // const
        public static UnityEngine.TextCore.LowLevel.FontEngineError Invalid_Face;  // const
        public static UnityEngine.TextCore.LowLevel.FontEngineError Invalid_Library_or_Face;  // const
        public static UnityEngine.TextCore.LowLevel.FontEngineError Atlas_Generation_Cancelled;  // const
        public static UnityEngine.TextCore.LowLevel.FontEngineError Invalid_SharedTextureData;  // const
        public static UnityEngine.TextCore.LowLevel.FontEngineError OpenTypeLayoutLookup_Mismatch;  // const

    }

    // TypeToken: 0x200000B  // size: 0x14
    public sealed struct GlyphRenderMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.TextCore.LowLevel.GlyphRenderMode SMOOTH_HINTED;  // const
        public static UnityEngine.TextCore.LowLevel.GlyphRenderMode SMOOTH;  // const
        public static UnityEngine.TextCore.LowLevel.GlyphRenderMode COLOR_HINTED;  // const
        public static UnityEngine.TextCore.LowLevel.GlyphRenderMode COLOR;  // const
        public static UnityEngine.TextCore.LowLevel.GlyphRenderMode RASTER_HINTED;  // const
        public static UnityEngine.TextCore.LowLevel.GlyphRenderMode RASTER;  // const
        public static UnityEngine.TextCore.LowLevel.GlyphRenderMode SDF;  // const
        public static UnityEngine.TextCore.LowLevel.GlyphRenderMode SDF8;  // const
        public static UnityEngine.TextCore.LowLevel.GlyphRenderMode SDF16;  // const
        public static UnityEngine.TextCore.LowLevel.GlyphRenderMode SDF32;  // const
        public static UnityEngine.TextCore.LowLevel.GlyphRenderMode SDFAA_HINTED;  // const
        public static UnityEngine.TextCore.LowLevel.GlyphRenderMode SDFAA;  // const
        public static UnityEngine.TextCore.LowLevel.GlyphRenderMode SDFBezier;  // const

    }

    // TypeToken: 0x200000C  // size: 0x14
    public sealed struct GlyphPackingMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.TextCore.LowLevel.GlyphPackingMode BestShortSideFit;  // const
        public static UnityEngine.TextCore.LowLevel.GlyphPackingMode BestLongSideFit;  // const
        public static UnityEngine.TextCore.LowLevel.GlyphPackingMode BestAreaFit;  // const
        public static UnityEngine.TextCore.LowLevel.GlyphPackingMode BottomLeftRule;  // const
        public static UnityEngine.TextCore.LowLevel.GlyphPackingMode ContactPointRule;  // const

    }

    // TypeToken: 0x200000D  // size: 0x30
    public sealed struct FontReference
    {
        // Fields
        public System.String familyName;  // 0x10
        public System.String styleName;  // 0x18
        public System.Int32 faceIndex;  // 0x20
        public System.String filePath;  // 0x28

    }

    // TypeToken: 0x200000E  // size: 0x10
    public sealed class FontEngine
    {
        // Fields
        private static UnityEngine.TextCore.Glyph[] s_Glyphs;  // static @ 0x0
        private static System.UInt32[] s_GlyphIndexes_MarshallingArray_A;  // static @ 0x8
        private static UnityEngine.TextCore.LowLevel.GlyphMarshallingStruct[] s_GlyphMarshallingStruct_IN;  // static @ 0x10
        private static UnityEngine.TextCore.LowLevel.GlyphMarshallingStruct[] s_GlyphMarshallingStruct_OUT;  // static @ 0x18
        private static UnityEngine.TextCore.GlyphRect[] s_FreeGlyphRects;  // static @ 0x20
        private static UnityEngine.TextCore.GlyphRect[] s_UsedGlyphRects;  // static @ 0x28
        private static UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord[] s_PairAdjustmentRecords_MarshallingArray;  // static @ 0x30
        private static System.Collections.Generic.Dictionary<System.UInt32,UnityEngine.TextCore.Glyph> s_GlyphLookupDictionary;  // static @ 0x38

        // Methods
        // RVA: 0x0B431060  token: 0x600004A
        public static UnityEngine.TextCore.LowLevel.FontEngineError InitializeFontEngine() { }
        // RVA: 0x0B431034  token: 0x600004B
        private static System.Int32 InitializeFontEngine_Internal() { }
        // RVA: 0x0B43128C  token: 0x600004C
        public static UnityEngine.TextCore.LowLevel.FontEngineError LoadFontFace(System.String filePath, System.Int32 pointSize, System.Int32 faceIndex) { }
        // RVA: 0x0B4310A0  token: 0x600004D
        private static System.Int32 LoadFontFace_With_Size_And_FaceIndex_Internal(System.String filePath, System.Int32 pointSize, System.Int32 faceIndex) { }
        // RVA: 0x0B431234  token: 0x600004E
        public static UnityEngine.TextCore.LowLevel.FontEngineError LoadFontFace(UnityEngine.Font font, System.Int32 pointSize) { }
        // RVA: 0x0B4310F0  token: 0x600004F
        private static System.Int32 LoadFontFace_With_Size_FromFont_Internal(UnityEngine.Font font, System.Int32 pointSize) { }
        // RVA: 0x0B4311D4  token: 0x6000050
        public static UnityEngine.TextCore.LowLevel.FontEngineError LoadFontFace(UnityEngine.Font font, System.Int32 pointSize, System.Int32 faceIndex) { }
        // RVA: 0x0B431130  token: 0x6000051
        private static System.Int32 LoadFontFace_With_Size_and_FaceIndex_FromFont_Internal(UnityEngine.Font font, System.Int32 pointSize, System.Int32 faceIndex) { }
        // RVA: 0x0B4312EC  token: 0x6000052
        public static UnityEngine.TextCore.LowLevel.FontEngineError LoadFontFace(System.String familyName, System.String styleName, System.Int32 pointSize) { }
        // RVA: 0x0B431180  token: 0x6000053
        private static System.Int32 LoadFontFace_With_Size_by_FamilyName_and_StyleName_Internal(System.String familyName, System.String styleName, System.Int32 pointSize) { }
        // RVA: 0x0B43251C  token: 0x6000054
        private static System.Boolean TryGetSystemFontReference(System.String familyName, System.String styleName, UnityEngine.TextCore.LowLevel.FontReference& fontRef) { }
        // RVA: 0x0B4324C8  token: 0x6000055
        private static System.Boolean TryGetSystemFontReference_Internal(System.String familyName, System.String styleName, UnityEngine.TextCore.LowLevel.FontReference& fontRef) { }
        // RVA: 0x0B430CF0  token: 0x6000056
        public static UnityEngine.TextCore.FaceInfo GetFaceInfo() { }
        // RVA: 0x0B430CBC  token: 0x6000057
        private static System.Int32 GetFaceInfo_Internal(UnityEngine.TextCore.FaceInfo& faceInfo) { }
        // RVA: 0x0B430D90  token: 0x6000058
        private static System.UInt32 GetGlyphIndex(System.UInt32 unicode) { }
        // RVA: 0x0B4323B0  token: 0x6000059
        public static System.Boolean TryGetGlyphWithUnicodeValue(System.UInt32 unicode, UnityEngine.TextCore.LowLevel.GlyphLoadFlags flags, UnityEngine.TextCore.Glyph& glyph) { }
        // RVA: 0x0B432360  token: 0x600005A
        private static System.Boolean TryGetGlyphWithUnicodeValue_Internal(System.UInt32 unicode, UnityEngine.TextCore.LowLevel.GlyphLoadFlags loadFlags, UnityEngine.TextCore.LowLevel.GlyphMarshallingStruct& glyphStruct) { }
        // RVA: 0x0B432248  token: 0x600005B
        public static System.Boolean TryGetGlyphWithIndexValue(System.UInt32 glyphIndex, UnityEngine.TextCore.LowLevel.GlyphLoadFlags flags, UnityEngine.TextCore.Glyph& glyph) { }
        // RVA: 0x0B4321F8  token: 0x600005C
        private static System.Boolean TryGetGlyphWithIndexValue_Internal(System.UInt32 glyphIndex, UnityEngine.TextCore.LowLevel.GlyphLoadFlags loadFlags, UnityEngine.TextCore.LowLevel.GlyphMarshallingStruct& glyphStruct) { }
        // RVA: 0x0B430C30  token: 0x600005D
        public static System.Void AddGlyphToTextureByGpu(UnityEngine.TextCore.Glyph& glyph, System.Int32 padding, UnityEngine.RenderTexture texture) { }
        // RVA: 0x0B430BE0  token: 0x600005E
        private static System.Boolean AddGlyphToTextureByGpu_Internal(UnityEngine.TextCore.LowLevel.GlyphMarshallingStruct& glyph, System.Int32 padding, UnityEngine.RenderTexture texture) { }
        // RVA: 0x0B4313E4  token: 0x600005F
        public static System.Void ProcessSDFGenerationRequest(UnityEngine.Material material, UnityEngine.Rendering.ScriptableRenderContext scriptableRenderContext, UnityEngine.Rendering.CommandBuffer cmd) { }
        // RVA: 0x0B431390  token: 0x6000060
        private static System.Void ProcessSDFGenerationRequest_Internal(UnityEngine.Material material, System.IntPtr scriptableRenderContext, UnityEngine.Rendering.CommandBuffer cmd) { }
        // RVA: 0x0B431490  token: 0x6000061
        private static System.Void SetTextureUploadMode(System.Boolean shouldUploadImmediately) { }
        // RVA: 0x0B431528  token: 0x6000062
        private static System.Boolean TryAddGlyphToTexture(System.UInt32 glyphIndex, System.Int32 padding, UnityEngine.TextCore.LowLevel.GlyphPackingMode packingMode, System.Collections.Generic.List<UnityEngine.TextCore.GlyphRect> freeGlyphRects, System.Collections.Generic.List<UnityEngine.TextCore.GlyphRect> usedGlyphRects, UnityEngine.TextCore.LowLevel.GlyphRenderMode renderMode, UnityEngine.Texture texture, UnityEngine.TextCore.Glyph& glyph) { }
        // RVA: 0x0B4314C4  token: 0x6000063
        private static System.Boolean TryAddGlyphToTexture_Internal(System.UInt32 glyphIndex, System.Int32 padding, UnityEngine.TextCore.LowLevel.GlyphPackingMode packingMode, UnityEngine.TextCore.GlyphRect[] freeGlyphRects, System.Int32& freeGlyphRectCount, UnityEngine.TextCore.GlyphRect[] usedGlyphRects, System.Int32& usedGlyphRectCount, UnityEngine.TextCore.LowLevel.GlyphRenderMode renderMode, UnityEngine.Texture texture, UnityEngine.TextCore.LowLevel.GlyphMarshallingStruct& glyph) { }
        // RVA: 0x0B431A40  token: 0x6000064
        private static System.Boolean TryAddGlyphsToTexture(System.Collections.Generic.List<System.UInt32> glyphIndexes, System.Int32 padding, UnityEngine.TextCore.LowLevel.GlyphPackingMode packingMode, System.Collections.Generic.List<UnityEngine.TextCore.GlyphRect> freeGlyphRects, System.Collections.Generic.List<UnityEngine.TextCore.GlyphRect> usedGlyphRects, UnityEngine.TextCore.LowLevel.GlyphRenderMode renderMode, UnityEngine.Texture texture, UnityEngine.TextCore.Glyph[]& glyphs) { }
        // RVA: 0x0B4319DC  token: 0x6000065
        private static System.Boolean TryAddGlyphsToTexture_Internal(System.UInt32[] glyphIndex, System.Int32 padding, UnityEngine.TextCore.LowLevel.GlyphPackingMode packingMode, UnityEngine.TextCore.GlyphRect[] freeGlyphRects, System.Int32& freeGlyphRectCount, UnityEngine.TextCore.GlyphRect[] usedGlyphRects, System.Int32& usedGlyphRectCount, UnityEngine.TextCore.LowLevel.GlyphRenderMode renderMode, UnityEngine.Texture texture, UnityEngine.TextCore.LowLevel.GlyphMarshallingStruct[] glyphs, System.Int32& glyphCount) { }
        // RVA: 0x0B430EFC  token: 0x6000066
        private static UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord[] GetGlyphPairAdjustmentTable(System.UInt32[] glyphIndexes) { }
        // RVA: 0x0B430DC4  token: 0x6000067
        private static UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord[] GetGlyphPairAdjustmentRecords(System.Collections.Generic.List<System.UInt32> glyphIndexes, System.Int32& recordCount) { }
        // RVA: 0x0B43134C  token: 0x6000068
        private static System.Int32 PopulatePairAdjustmentRecordMarshallingArray_from_KernTable(System.UInt32[] glyphIndexes, System.Int32& recordCount) { }
        // RVA: 0x0B431000  token: 0x6000069
        private static System.Int32 GetPairAdjustmentRecordsFromMarshallingArray(UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord[] glyphPairAdjustmentRecords) { }
        // RVA: -1  // generic def  token: 0x600006A
        private static System.Void GenericListToMarshallingArray(System.Collections.Generic.List<T>& srcList, T[]& dstArray) { }
        // RVA: -1  // generic def  token: 0x600006B
        private static System.Void SetMarshallingArraySize(T[]& marshallingArray, System.Int32 recordCount) { }
        // RVA: 0x0B43145C  token: 0x600006C
        private static System.Void ResetAtlasTexture(UnityEngine.Texture2D texture) { }
        // RVA: 0x0B43257C  token: 0x600006D
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000F  // size: 0x11
    public sealed struct FontEngineUtilities
    {
        // Methods
        // RVA: 0x0B430BC4  token: 0x600006E
        private static System.Int32 MaxValue(System.Int32 a, System.Int32 b, System.Int32 c) { }

    }

    // TypeToken: 0x2000010  // size: 0x44
    public sealed struct GlyphMarshallingStruct
    {
        // Fields
        public System.UInt32 index;  // 0x10
        public UnityEngine.TextCore.GlyphMetrics metrics;  // 0x14
        public UnityEngine.TextCore.GlyphRect glyphRect;  // 0x28
        public System.Single scale;  // 0x38
        public System.Int32 atlasIndex;  // 0x3c
        public UnityEngine.TextCore.GlyphClassDefinitionType classDefinitionType;  // 0x40

        // Methods
        // RVA: 0x0B432884  token: 0x600006F
        public System.Void .ctor(UnityEngine.TextCore.Glyph glyph) { }

    }

    // TypeToken: 0x2000011  // size: 0x14
    public sealed struct FontFeatureLookupFlags
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.TextCore.LowLevel.FontFeatureLookupFlags None;  // const
        public static UnityEngine.TextCore.LowLevel.FontFeatureLookupFlags IgnoreLigatures;  // const
        public static UnityEngine.TextCore.LowLevel.FontFeatureLookupFlags IgnoreSpacingAdjustments;  // const

    }

    // TypeToken: 0x2000012  // size: 0x20
    public sealed struct GlyphValueRecord : System.IEquatable`1
    {
        // Fields
        private System.Single m_XPlacement;  // 0x10
        private System.Single m_YPlacement;  // 0x14
        private System.Single m_XAdvance;  // 0x18
        private System.Single m_YAdvance;  // 0x1c

        // Properties
        System.Single xPlacement { get; /* RVA: 0x04DBA9D0 */ }
        System.Single yPlacement { get; /* RVA: 0x04DBA740 */ }
        System.Single xAdvance { get; /* RVA: 0x04DBA730 */ }
        System.Single yAdvance { get; /* RVA: 0x04DBA950 */ }

        // Methods
        // RVA: 0x04DBD3D0  token: 0x6000074
        public static UnityEngine.TextCore.LowLevel.GlyphValueRecord op_Addition(UnityEngine.TextCore.LowLevel.GlyphValueRecord a, UnityEngine.TextCore.LowLevel.GlyphValueRecord b) { }
        // RVA: 0x0B432DA0  token: 0x6000075
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B432CC4  token: 0x6000076
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0B432D24  token: 0x6000077
        public virtual System.Boolean Equals(UnityEngine.TextCore.LowLevel.GlyphValueRecord other) { }

    }

    // TypeToken: 0x2000013  // size: 0x24
    public sealed struct GlyphAdjustmentRecord : System.IEquatable`1
    {
        // Fields
        private System.UInt32 m_GlyphIndex;  // 0x10
        private UnityEngine.TextCore.LowLevel.GlyphValueRecord m_GlyphValueRecord;  // 0x14

        // Properties
        System.UInt32 glyphIndex { get; /* RVA: 0x04DBA750 */ }
        UnityEngine.TextCore.LowLevel.GlyphValueRecord glyphValueRecord { get; /* RVA: 0x04DBCC60 */ }

        // Methods
        // RVA: 0x0B432830  token: 0x600007A
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B4327CC  token: 0x600007B
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0B432744  token: 0x600007C
        public virtual System.Boolean Equals(UnityEngine.TextCore.LowLevel.GlyphAdjustmentRecord other) { }

    }

    // TypeToken: 0x2000014  // size: 0x3C
    public sealed struct GlyphPairAdjustmentRecord : System.IEquatable`1
    {
        // Fields
        private UnityEngine.TextCore.LowLevel.GlyphAdjustmentRecord m_FirstAdjustmentRecord;  // 0x10
        private UnityEngine.TextCore.LowLevel.GlyphAdjustmentRecord m_SecondAdjustmentRecord;  // 0x24
        private UnityEngine.TextCore.LowLevel.FontFeatureLookupFlags m_FeatureLookupFlags;  // 0x38

        // Properties
        UnityEngine.TextCore.LowLevel.GlyphAdjustmentRecord firstAdjustmentRecord { get; /* RVA: 0x04DBD3C0 */ }
        UnityEngine.TextCore.LowLevel.GlyphAdjustmentRecord secondAdjustmentRecord { get; /* RVA: 0x04D96D10 */ }

        // Methods
        // RVA: 0x0B432B30  token: 0x600007F
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B432A08  token: 0x6000080
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0B432A80  token: 0x6000081
        public virtual System.Boolean Equals(UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord other) { }

    }

}

