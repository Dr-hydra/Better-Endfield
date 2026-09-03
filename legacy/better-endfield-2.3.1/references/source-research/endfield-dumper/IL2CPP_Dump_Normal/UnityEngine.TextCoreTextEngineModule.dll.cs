// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.TextCoreTextEngineModule.dll
// Classes:  59
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000009  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly UnityEngine.TextCore.Text.FontAsset.<>c <>9;  // static @ 0x0
        public static System.Func<UnityEngine.TextCore.Text.Character,System.UInt32> <>9__144_0;  // static @ 0x8
        public static System.Func<UnityEngine.TextCore.Glyph,System.UInt32> <>9__145_0;  // static @ 0x10

        // Methods
        // RVA: 0x0B459278  token: 0x6000078
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000079
        public System.Void .ctor() { }
        // RVA: 0x0B319DF0  token: 0x600007A
        private System.UInt32 <SortCharacterTable>b__144_0(UnityEngine.TextCore.Text.Character c) { }
        // RVA: 0x0AA421AC  token: 0x600007B
        private System.UInt32 <SortGlyphTable>b__145_0(UnityEngine.TextCore.Glyph c) { }

    }

    // TypeToken: 0x200000C  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly UnityEngine.TextCore.Text.FontFeatureTable.<>c <>9;  // static @ 0x0
        public static System.Func<UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord,System.UInt32> <>9__6_0;  // static @ 0x8
        public static System.Func<UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord,System.UInt32> <>9__6_1;  // static @ 0x10

        // Methods
        // RVA: 0x0B4591B0  token: 0x6000084
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000085
        public System.Void .ctor() { }
        // RVA: 0x04DBD6C0  token: 0x6000086
        private System.UInt32 <SortGlyphPairAdjustmentRecords>b__6_0(UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord s) { }
        // RVA: 0x04DBD6D0  token: 0x6000087
        private System.UInt32 <SortGlyphPairAdjustmentRecords>b__6_1(UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord s) { }

    }

    // TypeToken: 0x2000016  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly UnityEngine.TextCore.Text.SpriteAsset.<>c <>9;  // static @ 0x0
        public static System.Func<UnityEngine.TextCore.Text.SpriteGlyph,System.UInt32> <>9__37_0;  // static @ 0x8
        public static System.Func<UnityEngine.TextCore.Text.SpriteCharacter,System.UInt32> <>9__38_0;  // static @ 0x10

        // Methods
        // RVA: 0x0B459214  token: 0x60000C2
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60000C3
        public System.Void .ctor() { }
        // RVA: 0x0AA421AC  token: 0x60000C4
        private System.UInt32 <SortGlyphTable>b__37_0(UnityEngine.TextCore.Text.SpriteGlyph item) { }
        // RVA: 0x0B319DF0  token: 0x60000C5
        private System.UInt32 <SortCharacterTable>b__38_0(UnityEngine.TextCore.Text.SpriteCharacter c) { }

    }

    // TypeToken: 0x2000024  // size: 0x30
    public sealed struct SpecialCharacter
    {
        // Fields
        public UnityEngine.TextCore.Text.Character character;  // 0x10
        public UnityEngine.TextCore.Text.FontAsset fontAsset;  // 0x18
        public UnityEngine.Material material;  // 0x20
        public System.Int32 materialIndex;  // 0x28

        // Methods
        // RVA: 0x0B4523E0  token: 0x6000102
        public System.Void .ctor(UnityEngine.TextCore.Text.Character character, System.Int32 materialIndex) { }

    }

    // TypeToken: 0x2000034  // size: 0x30
    public sealed struct FontAssetRef
    {
        // Fields
        public System.Int32 nameHashCode;  // 0x10
        public System.Int32 familyNameHashCode;  // 0x14
        public System.Int32 styleNameHashCode;  // 0x18
        public System.Int64 familyNameAndStyleHashCode;  // 0x20
        public readonly UnityEngine.TextCore.Text.FontAsset fontAsset;  // 0x28

        // Methods
        // RVA: 0x0B4592DC  token: 0x6000133
        public System.Void .ctor(System.Int32 nameHashCode, System.Int32 familyNameHashCode, System.Int32 styleNameHashCode, UnityEngine.TextCore.Text.FontAsset fontAsset) { }

    }

    // TypeToken: 0x2000036  // size: 0x20
    public sealed struct FontReferenceMap
    {
        // Fields
        public UnityEngine.Font font;  // 0x10
        public UnityEngine.TextCore.Text.FontAsset fontAsset;  // 0x18

        // Methods
        // RVA: 0x039CF630  token: 0x6000157
        public System.Void .ctor(UnityEngine.Font font, UnityEngine.TextCore.Text.FontAsset fontAsset) { }

    }

namespace UnityEngine.TextCore.Text
{

    // TypeToken: 0x2000002  // size: 0x30
    public class Character : UnityEngine.TextCore.Text.TextElement
    {
        // Methods
        // RVA: 0x04DB9B40  token: 0x6000001
        public System.Void .ctor() { }
        // RVA: 0x0B448900  token: 0x6000002
        public System.Void .ctor(System.UInt32 unicode, UnityEngine.TextCore.Text.FontAsset fontAsset, UnityEngine.TextCore.Glyph glyph) { }
        // RVA: 0x0B30C9E4  token: 0x6000003
        private System.Void .ctor(System.UInt32 unicode, System.UInt32 glyphIndex) { }

    }

    // TypeToken: 0x2000003  // size: 0x10
    public static class ColorUtilities
    {
        // Methods
        // RVA: 0x0B448958  token: 0x6000004
        private static System.Boolean CompareColors(UnityEngine.Color32 a, UnityEngine.Color32 b) { }
        // RVA: 0x0B448990  token: 0x6000005
        private static UnityEngine.Color32 MultiplyColors(UnityEngine.Color32 c1, UnityEngine.Color32 c2) { }

    }

    // TypeToken: 0x2000004  // size: 0x14
    public sealed struct TextFontWeight
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.TextCore.Text.TextFontWeight Thin;  // const
        public static UnityEngine.TextCore.Text.TextFontWeight ExtraLight;  // const
        public static UnityEngine.TextCore.Text.TextFontWeight Light;  // const
        public static UnityEngine.TextCore.Text.TextFontWeight Regular;  // const
        public static UnityEngine.TextCore.Text.TextFontWeight Medium;  // const
        public static UnityEngine.TextCore.Text.TextFontWeight SemiBold;  // const
        public static UnityEngine.TextCore.Text.TextFontWeight Bold;  // const
        public static UnityEngine.TextCore.Text.TextFontWeight Heavy;  // const
        public static UnityEngine.TextCore.Text.TextFontWeight Black;  // const

    }

    // TypeToken: 0x2000005  // size: 0x20
    public sealed struct FontWeightPair
    {
        // Fields
        public UnityEngine.TextCore.Text.FontAsset regularTypeface;  // 0x10
        public UnityEngine.TextCore.Text.FontAsset italicTypeface;  // 0x18

    }

    // TypeToken: 0x2000006  // size: 0x60
    public sealed struct FontAssetCreationEditorSettings
    {
        // Fields
        public System.String sourceFontFileGUID;  // 0x10
        public System.Int32 faceIndex;  // 0x18
        public System.Int32 pointSizeSamplingMode;  // 0x1c
        public System.Int32 pointSize;  // 0x20
        public System.Int32 padding;  // 0x24
        public System.Int32 packingMode;  // 0x28
        public System.Int32 atlasWidth;  // 0x2c
        public System.Int32 atlasHeight;  // 0x30
        public System.Int32 characterSetSelectionMode;  // 0x34
        public System.String characterSequence;  // 0x38
        public System.String referencedFontAssetGUID;  // 0x40
        public System.String referencedTextAssetGUID;  // 0x48
        public System.Int32 fontStyle;  // 0x50
        public System.Single fontStyleModifier;  // 0x54
        public System.Int32 renderMode;  // 0x58
        public System.Boolean includeFontFeatures;  // 0x5c

    }

    // TypeToken: 0x2000007  // size: 0x14
    public sealed struct AtlasPopulationMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.TextCore.Text.AtlasPopulationMode Static;  // const
        public static UnityEngine.TextCore.Text.AtlasPopulationMode Dynamic;  // const
        public static UnityEngine.TextCore.Text.AtlasPopulationMode DynamicOS;  // const

    }

    // TypeToken: 0x2000008  // size: 0x1E0
    public class FontAsset : UnityEngine.TextCore.Text.TextAsset
    {
        // Fields
        private System.String m_SourceFontFileGUID;  // 0x38
        private UnityEngine.Font m_SourceFontFile;  // 0x40
        private UnityEngine.TextCore.Text.AtlasPopulationMode m_AtlasPopulationMode;  // 0x48
        private System.Boolean InternalDynamicOS;  // 0x4c
        private UnityEngine.TextCore.FaceInfo m_FaceInfo;  // 0x50
        private System.Int32 m_FamilyNameHashCode;  // 0xb0
        private System.Int32 m_StyleNameHashCode;  // 0xb4
        private UnityEngine.TextCore.Text.FontWeightPair[] m_FontWeightTable;  // 0xb8
        private System.Collections.Generic.List<UnityEngine.TextCore.Glyph> m_GlyphTable;  // 0xc0
        private System.Collections.Generic.Dictionary<System.UInt32,UnityEngine.TextCore.Glyph> m_GlyphLookupDictionary;  // 0xc8
        private System.Collections.Generic.List<UnityEngine.TextCore.Text.Character> m_CharacterTable;  // 0xd0
        private System.Collections.Generic.Dictionary<System.UInt32,UnityEngine.TextCore.Text.Character> m_CharacterLookupDictionary;  // 0xd8
        private UnityEngine.Texture2D m_AtlasTexture;  // 0xe0
        private UnityEngine.Texture2D[] m_AtlasTextures;  // 0xe8
        private System.Int32 m_AtlasTextureIndex;  // 0xf0
        private System.Boolean m_IsMultiAtlasTexturesEnabled;  // 0xf4
        private System.Boolean m_ClearDynamicDataOnBuild;  // 0xf5
        private System.Int32 m_AtlasWidth;  // 0xf8
        private System.Int32 m_AtlasHeight;  // 0xfc
        private System.Int32 m_AtlasPadding;  // 0x100
        private UnityEngine.TextCore.LowLevel.GlyphRenderMode m_AtlasRenderMode;  // 0x104
        private System.Collections.Generic.List<UnityEngine.TextCore.GlyphRect> m_UsedGlyphRects;  // 0x108
        private System.Collections.Generic.List<UnityEngine.TextCore.GlyphRect> m_FreeGlyphRects;  // 0x110
        private UnityEngine.TextCore.Text.FontFeatureTable m_FontFeatureTable;  // 0x118
        private System.Collections.Generic.List<UnityEngine.TextCore.Text.FontAsset> m_FallbackFontAssetTable;  // 0x120
        private UnityEngine.TextCore.Text.FontAssetCreationEditorSettings m_fontAssetCreationEditorSettings;  // 0x128
        private System.Single m_RegularStyleWeight;  // 0x178
        private System.Single m_RegularStyleSpacing;  // 0x17c
        private System.Single m_BoldStyleWeight;  // 0x180
        private System.Single m_BoldStyleSpacing;  // 0x184
        private System.Byte m_ItalicStyleSlant;  // 0x188
        private System.Byte m_TabMultiple;  // 0x189
        private System.Boolean IsFontAssetLookupTablesDirty;  // 0x18a
        private static Unity.Profiling.ProfilerMarker k_ReadFontAssetDefinitionMarker;  // static @ 0x0
        private static Unity.Profiling.ProfilerMarker k_AddSynthesizedCharactersMarker;  // static @ 0x8
        private static Unity.Profiling.ProfilerMarker k_TryAddCharacterMarker;  // static @ 0x10
        private static Unity.Profiling.ProfilerMarker k_TryAddCharactersMarker;  // static @ 0x18
        private static Unity.Profiling.ProfilerMarker k_UpdateGlyphAdjustmentRecordsMarker;  // static @ 0x20
        private static Unity.Profiling.ProfilerMarker k_ClearFontAssetDataMarker;  // static @ 0x28
        private static Unity.Profiling.ProfilerMarker k_UpdateFontAssetDataMarker;  // static @ 0x30
        private static System.String s_DefaultMaterialSuffix;  // static @ 0x38
        private static System.Collections.Generic.HashSet<System.Int32> k_SearchedFontAssetLookup;  // static @ 0x40
        private static System.Collections.Generic.List<UnityEngine.TextCore.Text.FontAsset> k_FontAssets_FontFeaturesUpdateQueue;  // static @ 0x48
        private static System.Collections.Generic.HashSet<System.Int32> k_FontAssets_FontFeaturesUpdateQueueLookup;  // static @ 0x50
        private static System.Collections.Generic.List<UnityEngine.Texture2D> k_FontAssets_AtlasTexturesUpdateQueue;  // static @ 0x58
        private static System.Collections.Generic.HashSet<System.Int32> k_FontAssets_AtlasTexturesUpdateQueueLookup;  // static @ 0x60
        private System.Collections.Generic.List<UnityEngine.TextCore.Glyph> m_GlyphsToRender;  // 0x190
        private System.Collections.Generic.List<UnityEngine.TextCore.Glyph> m_GlyphsRendered;  // 0x198
        private System.Collections.Generic.List<System.UInt32> m_GlyphIndexList;  // 0x1a0
        private System.Collections.Generic.List<System.UInt32> m_GlyphIndexListNewlyAdded;  // 0x1a8
        private System.Collections.Generic.List<System.UInt32> m_GlyphsToAdd;  // 0x1b0
        private System.Collections.Generic.HashSet<System.UInt32> m_GlyphsToAddLookup;  // 0x1b8
        private System.Collections.Generic.List<UnityEngine.TextCore.Text.Character> m_CharactersToAdd;  // 0x1c0
        private System.Collections.Generic.HashSet<System.UInt32> m_CharactersToAddLookup;  // 0x1c8
        private System.Collections.Generic.List<System.UInt32> s_MissingCharacterList;  // 0x1d0
        private System.Collections.Generic.HashSet<System.UInt32> m_MissingUnicodesFromFontFile;  // 0x1d8
        private static System.UInt32[] k_GlyphIndexArray;  // static @ 0x68

        // Properties
        UnityEngine.Font sourceFontFile { get; /* RVA: 0x04DBCF90 */ set; /* RVA: 0x042B4AE0 */ }
        UnityEngine.TextCore.Text.AtlasPopulationMode atlasPopulationMode { get; /* RVA: 0x04DBC0F0 */ set; /* RVA: 0x04D86450 */ }
        UnityEngine.TextCore.FaceInfo faceInfo { get; /* RVA: 0x04DBD500 */ set; /* RVA: 0x0B44FECC */ }
        System.Int32 familyNameHashCode { get; /* RVA: 0x0B44FE34 */ set; /* RVA: 0x04D8F8F0 */ }
        System.Int32 styleNameHashCode { get; /* RVA: 0x0B44FE98 */ set; /* RVA: 0x04D89330 */ }
        UnityEngine.TextCore.Text.FontWeightPair[] fontWeightTable { get; /* RVA: 0x04DBD0A0 */ set; /* RVA: 0x06AC5AF4 */ }
        System.Collections.Generic.List<UnityEngine.TextCore.Glyph> glyphTable { get; /* RVA: 0x04DBD0B0 */ set; /* RVA: 0x06AC5ACC */ }
        System.Collections.Generic.Dictionary<System.UInt32,UnityEngine.TextCore.Glyph> glyphLookupTable { get; /* RVA: 0x0B44FE68 */ }
        System.Collections.Generic.List<UnityEngine.TextCore.Text.Character> characterTable { get; /* RVA: 0x04DBD050 */ set; /* RVA: 0x0678D7BC */ }
        System.Collections.Generic.Dictionary<System.UInt32,UnityEngine.TextCore.Text.Character> characterLookupTable { get; /* RVA: 0x0B44FE04 */ }
        UnityEngine.Texture2D atlasTexture { get; /* RVA: 0x0B44FD64 */ }
        UnityEngine.Texture2D[] atlasTextures { get; /* RVA: 0x0B44FDF8 */ set; /* RVA: 0x06AC5B1C */ }
        System.Int32 atlasTextureCount { get; /* RVA: 0x04DBD4B0 */ }
        System.Boolean isMultiAtlasTexturesEnabled { get; /* RVA: 0x04DBD5B0 */ set; /* RVA: 0x04DA7530 */ }
        System.Boolean clearDynamicDataOnBuild { get; /* RVA: 0x04DBD4F0 */ set; /* RVA: 0x04DA7550 */ }
        System.Int32 atlasWidth { get; /* RVA: 0x04DBD4C0 */ set; /* RVA: 0x04DA6220 */ }
        System.Int32 atlasHeight { get; /* RVA: 0x04DBD480 */ set; /* RVA: 0x04D8F9D0 */ }
        System.Int32 atlasPadding { get; /* RVA: 0x04DBD490 */ set; /* RVA: 0x04D927A0 */ }
        UnityEngine.TextCore.LowLevel.GlyphRenderMode atlasRenderMode { get; /* RVA: 0x04DBD4A0 */ set; /* RVA: 0x04DBD610 */ }
        System.Collections.Generic.List<UnityEngine.TextCore.GlyphRect> usedGlyphRects { get; /* RVA: 0x04DBD600 */ set; /* RVA: 0x068C2E50 */ }
        System.Collections.Generic.List<UnityEngine.TextCore.GlyphRect> freeGlyphRects { get; /* RVA: 0x04DBD5A0 */ set; /* RVA: 0x05397170 */ }
        UnityEngine.TextCore.Text.FontFeatureTable fontFeatureTable { get; /* RVA: 0x04DBD590 */ set; /* RVA: 0x0539715C */ }
        System.Collections.Generic.List<UnityEngine.TextCore.Text.FontAsset> fallbackFontAssetTable { get; /* RVA: 0x04DBD540 */ set; /* RVA: 0x053971C0 */ }
        UnityEngine.TextCore.Text.FontAssetCreationEditorSettings fontAssetCreationEditorSettings { get; /* RVA: 0x04DBD550 */ set; /* RVA: 0x0B44FF10 */ }
        System.Single regularStyleWeight { get; /* RVA: 0x04DBD5E0 */ set; /* RVA: 0x04D90320 */ }
        System.Single regularStyleSpacing { get; /* RVA: 0x04DBD5D0 */ set; /* RVA: 0x04D90400 */ }
        System.Single boldStyleWeight { get; /* RVA: 0x04DBD4E0 */ set; /* RVA: 0x04DBD620 */ }
        System.Single boldStyleSpacing { get; /* RVA: 0x04DBD4D0 */ set; /* RVA: 0x04D8EE30 */ }
        System.Byte italicStyleSlant { get; /* RVA: 0x04DBD5C0 */ set; /* RVA: 0x04D90100 */ }
        System.Byte tabMultiple { get; /* RVA: 0x04DBD5F0 */ set; /* RVA: 0x04DBD630 */ }

        // Methods
        // RVA: 0x0B44A4CC  token: 0x600003E
        public static UnityEngine.TextCore.Text.FontAsset CreateFontAsset(System.String familyName, System.String styleName, System.Int32 pointSize) { }
        // RVA: 0x0B44A32C  token: 0x600003F
        private static UnityEngine.TextCore.Text.FontAsset CreateFontAsset(System.String fontFilePath, System.Int32 faceIndex, System.Int32 samplingPointSize, System.Int32 atlasPadding, UnityEngine.TextCore.LowLevel.GlyphRenderMode renderMode, System.Int32 atlasWidth, System.Int32 atlasHeight, UnityEngine.TextCore.Text.AtlasPopulationMode atlasPopulationMode, System.Boolean enableMultiAtlasSupport) { }
        // RVA: 0x0B44A45C  token: 0x6000040
        public static UnityEngine.TextCore.Text.FontAsset CreateFontAsset(UnityEngine.Font font) { }
        // RVA: 0x0B44A6B0  token: 0x6000041
        public static UnityEngine.TextCore.Text.FontAsset CreateFontAsset(UnityEngine.Font font, System.Int32 samplingPointSize, System.Int32 atlasPadding, UnityEngine.TextCore.LowLevel.GlyphRenderMode renderMode, System.Int32 atlasWidth, System.Int32 atlasHeight, UnityEngine.TextCore.Text.AtlasPopulationMode atlasPopulationMode, System.Boolean enableMultiAtlasSupport) { }
        // RVA: 0x0B44A754  token: 0x6000042
        private static UnityEngine.TextCore.Text.FontAsset CreateFontAsset(UnityEngine.Font font, System.Int32 faceIndex, System.Int32 samplingPointSize, System.Int32 atlasPadding, UnityEngine.TextCore.LowLevel.GlyphRenderMode renderMode, System.Int32 atlasWidth, System.Int32 atlasHeight, UnityEngine.TextCore.Text.AtlasPopulationMode atlasPopulationMode, System.Boolean enableMultiAtlasSupport) { }
        // RVA: 0x0B449E84  token: 0x6000043
        private static UnityEngine.TextCore.Text.FontAsset CreateFontAssetInstance(UnityEngine.Font font, System.Int32 atlasPadding, UnityEngine.TextCore.LowLevel.GlyphRenderMode renderMode, System.Int32 atlasWidth, System.Int32 atlasHeight, UnityEngine.TextCore.Text.AtlasPopulationMode atlasPopulationMode, System.Boolean enableMultiAtlasSupport) { }
        // RVA: 0x041E1670  token: 0x6000044
        private System.Void Awake() { }
        // RVA: 0x0B44BEE8  token: 0x6000045
        private System.Void OnDestroy() { }
        // RVA: 0x0B44BF38  token: 0x6000046
        public System.Void ReadFontAssetDefinition() { }
        // RVA: 0x0B44B9E8  token: 0x6000047
        private System.Void InitializeDictionaryLookupTables() { }
        // RVA: 0x0B44BA14  token: 0x6000048
        private System.Void InitializeGlyphLookupDictionary() { }
        // RVA: 0x0B44B828  token: 0x6000049
        private System.Void InitializeCharacterLookupDictionary() { }
        // RVA: 0x0B44BC8C  token: 0x600004A
        private System.Void InitializeGlyphPaidAdjustmentRecordsLookupDictionary() { }
        // RVA: 0x0B4498F8  token: 0x600004B
        private System.Void AddSynthesizedCharactersAndFaceMetrics() { }
        // RVA: 0x0B4496DC  token: 0x600004C
        private System.Void AddSynthesizedCharacter(System.UInt32 unicode, System.Boolean isFontFaceLoaded, System.Boolean addImmediately) { }
        // RVA: 0x0B449674  token: 0x600004D
        private System.Void AddCharacterToLookupCache(System.UInt32 unicode, UnityEngine.TextCore.Text.Character character) { }
        // RVA: 0x0B44BE58  token: 0x600004E
        private UnityEngine.TextCore.LowLevel.FontEngineError LoadFontFace() { }
        // RVA: 0x0B44C5F8  token: 0x600004F
        private System.Void SortCharacterTable() { }
        // RVA: 0x0B44C788  token: 0x6000050
        private System.Void SortGlyphTable() { }
        // RVA: 0x0B44C764  token: 0x6000051
        private System.Void SortFontFeatureTable() { }
        // RVA: 0x0B44C5CC  token: 0x6000052
        private System.Void SortAllTables() { }
        // RVA: 0x0B44AE50  token: 0x6000053
        public System.Boolean HasCharacter(System.Int32 character) { }
        // RVA: 0x0B44AEB4  token: 0x6000054
        public System.Boolean HasCharacter(System.Char character, System.Boolean searchFallbacks, System.Boolean tryAddCharacter) { }
        // RVA: 0x0B44AC20  token: 0x6000055
        private System.Boolean HasCharacter_Internal(System.UInt32 character, System.Boolean searchFallbacks, System.Boolean tryAddCharacter) { }
        // RVA: 0x0B44B6D4  token: 0x6000056
        public System.Boolean HasCharacters(System.String text, System.Collections.Generic.List<System.Char>& missingCharacters) { }
        // RVA: 0x0B44B2A4  token: 0x6000057
        public System.Boolean HasCharacters(System.String text, System.UInt32[]& missingCharacters, System.Boolean searchFallbacks, System.Boolean tryAddCharacter) { }
        // RVA: 0x0B44B1F8  token: 0x6000058
        public System.Boolean HasCharacters(System.String text) { }
        // RVA: 0x0B44AA78  token: 0x6000059
        public static System.String GetCharacters(UnityEngine.TextCore.Text.FontAsset fontAsset) { }
        // RVA: 0x0B44A9AC  token: 0x600005A
        public static System.Int32[] GetCharactersArray(UnityEngine.TextCore.Text.FontAsset fontAsset) { }
        // RVA: 0x0B44AB58  token: 0x600005B
        private System.UInt32 GetGlyphIndex(System.UInt32 unicode) { }
        // RVA: 0x0B44C31C  token: 0x600005C
        private static System.Void RegisterFontAssetForFontFeatureUpdate(UnityEngine.TextCore.Text.FontAsset fontAsset) { }
        // RVA: 0x0B44F064  token: 0x600005D
        private static System.Void UpdateFontFeaturesForFontAssetsInQueue() { }
        // RVA: 0x0B44C250  token: 0x600005E
        private static System.Void RegisterAtlasTextureForApply(UnityEngine.Texture2D texture) { }
        // RVA: 0x0B44EDE0  token: 0x600005F
        private static System.Void UpdateAtlasTexturesInQueue() { }
        // RVA: 0x0B44F01C  token: 0x6000060
        private static System.Void UpdateFontAssetInUpdateQueue() { }
        // RVA: 0x0B44D20C  token: 0x6000061
        public System.Boolean TryAddCharacters(System.UInt32[] unicodes, System.Boolean includeFontFeatures) { }
        // RVA: 0x0B44D230  token: 0x6000062
        public System.Boolean TryAddCharacters(System.UInt32[] unicodes, System.UInt32[]& missingUnicodes, System.Boolean includeFontFeatures) { }
        // RVA: 0x0B44D1E8  token: 0x6000063
        public System.Boolean TryAddCharacters(System.String characters, System.Boolean includeFontFeatures) { }
        // RVA: 0x0B44DBAC  token: 0x6000064
        public System.Boolean TryAddCharacters(System.String characters, System.String& missingCharacters, System.Boolean includeFontFeatures) { }
        // RVA: 0x0B44C8F4  token: 0x6000065
        private System.Boolean TryAddCharacterInternal(System.UInt32 unicode, UnityEngine.TextCore.Text.Character& character, System.Boolean shouldGetFontFeatures) { }
        // RVA: 0x0B44E864  token: 0x6000066
        private System.Boolean TryGetCharacter_and_QueueRenderToTexture(System.UInt32 unicode, UnityEngine.TextCore.Text.Character& character, System.Boolean shouldGetFontFeatures) { }
        // RVA: 0x041E1670  token: 0x6000067
        private System.Void TryAddGlyphsToAtlasTextures() { }
        // RVA: 0x0B44E504  token: 0x6000068
        private System.Boolean TryAddGlyphsToNewAtlasTexture() { }
        // RVA: 0x0B44C3E8  token: 0x6000069
        private System.Void SetupNewAtlasTexture() { }
        // RVA: 0x0B44EC54  token: 0x600006A
        private System.Void UpdateAtlasTexture() { }
        // RVA: 0x0B44F3B0  token: 0x600006B
        private System.Void UpdateGlyphAdjustmentRecords() { }
        // RVA: 0x0B44F194  token: 0x600006C
        private System.Void UpdateGlyphAdjustmentRecords(System.UInt32[] glyphIndexes) { }
        // RVA: 0x041E1670  token: 0x600006D
        private System.Void UpdateGlyphAdjustmentRecords(System.Collections.Generic.List<System.UInt32> glyphIndexes) { }
        // RVA: 0x041E1670  token: 0x600006E
        private System.Void UpdateGlyphAdjustmentRecords(System.Collections.Generic.List<System.UInt32> newGlyphIndexes, System.Collections.Generic.List<System.UInt32> allGlyphIndexes) { }
        // RVA: -1  // generic def  token: 0x600006F
        private System.Void CopyListDataToArray(System.Collections.Generic.List<T> srcList, T[]& dstArray) { }
        // RVA: 0x0B449CCC  token: 0x6000070
        public System.Void ClearFontAssetData(System.Boolean setAtlasSizeToZero) { }
        // RVA: 0x0B449CA8  token: 0x6000071
        private System.Void ClearFontAssetDataInternal() { }
        // RVA: 0x0B44EF14  token: 0x6000072
        private System.Void UpdateFontAssetData() { }
        // RVA: 0x0B449D08  token: 0x6000073
        private System.Void ClearFontAssetTables() { }
        // RVA: 0x0B449AE0  token: 0x6000074
        private System.Void ClearAtlasTextures(System.Boolean setAtlasSizeToZero) { }
        // RVA: 0x0B44A900  token: 0x6000075
        private System.Void DestroyAtlasTextures() { }
        // RVA: 0x0B44F980  token: 0x6000076
        public System.Void .ctor() { }
        // RVA: 0x0B44F600  token: 0x6000077
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000A  // size: 0x10
    public static class FontAssetUtilities
    {
        // Fields
        private static System.Collections.Generic.HashSet<System.Int32> k_SearchedAssets;  // static @ 0x0

        // Methods
        // RVA: 0x0B448F8C  token: 0x600007C
        private static UnityEngine.TextCore.Text.Character GetCharacterFromFontAsset(System.UInt32 unicode, UnityEngine.TextCore.Text.FontAsset sourceFontAsset, System.Boolean includeFallbacks, UnityEngine.TextCore.Text.FontStyles fontStyle, UnityEngine.TextCore.Text.TextFontWeight fontWeight, System.Boolean& isAlternativeTypeface) { }
        // RVA: 0x0B448BF0  token: 0x600007D
        private static UnityEngine.TextCore.Text.Character GetCharacterFromFontAsset_Internal(System.UInt32 unicode, UnityEngine.TextCore.Text.FontAsset sourceFontAsset, System.Boolean includeFallbacks, UnityEngine.TextCore.Text.FontStyles fontStyle, UnityEngine.TextCore.Text.TextFontWeight fontWeight, System.Boolean& isAlternativeTypeface) { }
        // RVA: 0x0B4490AC  token: 0x600007E
        public static UnityEngine.TextCore.Text.Character GetCharacterFromFontAssets(System.UInt32 unicode, UnityEngine.TextCore.Text.FontAsset sourceFontAsset, System.Collections.Generic.List<UnityEngine.TextCore.Text.FontAsset> fontAssets, System.Boolean includeFallbacks, UnityEngine.TextCore.Text.FontStyles fontStyle, UnityEngine.TextCore.Text.TextFontWeight fontWeight, System.Boolean& isAlternativeTypeface) { }
        // RVA: 0x0B4493F8  token: 0x600007F
        public static UnityEngine.TextCore.Text.SpriteCharacter GetSpriteCharacterFromSpriteAsset(System.UInt32 unicode, UnityEngine.TextCore.Text.SpriteAsset spriteAsset, System.Boolean includeFallbacks) { }
        // RVA: 0x0B449268  token: 0x6000080
        private static UnityEngine.TextCore.Text.SpriteCharacter GetSpriteCharacterFromSpriteAsset_Internal(System.UInt32 unicode, UnityEngine.TextCore.Text.SpriteAsset spriteAsset, System.Boolean includeFallbacks) { }

    }

    // TypeToken: 0x200000B  // size: 0x20
    public class FontFeatureTable
    {
        // Fields
        private System.Collections.Generic.List<UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord> m_GlyphPairAdjustmentRecords;  // 0x10
        private System.Collections.Generic.Dictionary<System.UInt32,UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord> m_GlyphPairAdjustmentRecordLookup;  // 0x18

        // Properties
        System.Collections.Generic.List<UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord> glyphPairAdjustmentRecords { get; /* RVA: 0x04D9B600 */ }

        // Methods
        // RVA: 0x0B450174  token: 0x6000082
        public System.Void .ctor() { }
        // RVA: 0x0B44FF54  token: 0x6000083
        public System.Void SortGlyphPairAdjustmentRecords() { }

    }

    // TypeToken: 0x200000D  // size: 0x20
    public sealed struct Extents
    {
        // Fields
        public UnityEngine.Vector2 min;  // 0x10
        public UnityEngine.Vector2 max;  // 0x18

        // Methods
        // RVA: 0x0B448A6C  token: 0x6000088
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x200000E  // size: 0x70
    public sealed struct LineInfo
    {
        // Fields
        private System.Int32 controlCharacterCount;  // 0x10
        public System.Int32 characterCount;  // 0x14
        public System.Int32 visibleCharacterCount;  // 0x18
        public System.Int32 spaceCount;  // 0x1c
        public System.Int32 visibleSpaceCount;  // 0x20
        public System.Int32 wordCount;  // 0x24
        public System.Int32 firstCharacterIndex;  // 0x28
        public System.Int32 firstVisibleCharacterIndex;  // 0x2c
        public System.Int32 lastCharacterIndex;  // 0x30
        public System.Int32 lastVisibleCharacterIndex;  // 0x34
        public System.Single length;  // 0x38
        public System.Single lineHeight;  // 0x3c
        public System.Single ascender;  // 0x40
        public System.Single baseline;  // 0x44
        public System.Single descender;  // 0x48
        public System.Single maxAdvance;  // 0x4c
        public System.Single width;  // 0x50
        public System.Single marginLeft;  // 0x54
        public System.Single marginRight;  // 0x58
        public UnityEngine.TextCore.Text.TextAlignment alignment;  // 0x5c
        public UnityEngine.TextCore.Text.Extents lineExtents;  // 0x60

    }

    // TypeToken: 0x200000F  // size: 0x30
    public sealed struct LinkInfo
    {
        // Fields
        public System.Int32 hashCode;  // 0x10
        public System.Int32 linkIdFirstCharacterIndex;  // 0x14
        public System.Int32 linkIdLength;  // 0x18
        public System.Int32 linkTextfirstCharacterIndex;  // 0x1c
        public System.Int32 linkTextLength;  // 0x20
        private System.Char[] linkId;  // 0x28

        // Methods
        // RVA: 0x0B450234  token: 0x6000089
        private System.Void SetLinkId(System.Char[] text, System.Int32 startIndex, System.Int32 length) { }

    }

    // TypeToken: 0x2000010  // size: 0x10
    public static class MaterialManager
    {
        // Fields
        private static System.Collections.Generic.Dictionary<System.Int64,UnityEngine.Material> s_FallbackMaterials;  // static @ 0x0

        // Methods
        // RVA: 0x0B4502F8  token: 0x600008A
        public static UnityEngine.Material GetFallbackMaterial(UnityEngine.Material sourceMaterial, UnityEngine.Material targetMaterial) { }
        // RVA: 0x0B450688  token: 0x600008B
        public static UnityEngine.Material GetFallbackMaterial(UnityEngine.TextCore.Text.FontAsset fontAsset, UnityEngine.Material sourceMaterial, System.Int32 atlasIndex) { }
        // RVA: 0x0B45084C  token: 0x600008C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000011  // size: 0x48
    public sealed struct MaterialReference
    {
        // Fields
        public System.Int32 index;  // 0x10
        public UnityEngine.TextCore.Text.FontAsset fontAsset;  // 0x18
        public UnityEngine.TextCore.Text.SpriteAsset spriteAsset;  // 0x20
        public UnityEngine.Material material;  // 0x28
        public System.Boolean isDefaultMaterial;  // 0x30
        public System.Boolean isFallbackMaterial;  // 0x31
        public UnityEngine.Material fallbackMaterial;  // 0x38
        public System.Single padding;  // 0x40
        public System.Int32 referenceCount;  // 0x44

        // Methods
        // RVA: 0x0B4514C8  token: 0x600008D
        public System.Void .ctor(System.Int32 index, UnityEngine.TextCore.Text.FontAsset fontAsset, UnityEngine.TextCore.Text.SpriteAsset spriteAsset, UnityEngine.Material material, System.Single padding) { }
        // RVA: 0x0B4510DC  token: 0x600008E
        public static System.Int32 AddMaterialReference(UnityEngine.Material material, UnityEngine.TextCore.Text.FontAsset fontAsset, UnityEngine.TextCore.Text.MaterialReference[]& materialReferences, System.Collections.Generic.Dictionary<System.Int32,System.Int32> materialReferenceIndexLookup) { }
        // RVA: 0x0B4512DC  token: 0x600008F
        public static System.Int32 AddMaterialReference(UnityEngine.Material material, UnityEngine.TextCore.Text.SpriteAsset spriteAsset, UnityEngine.TextCore.Text.MaterialReference[]& materialReferences, System.Collections.Generic.Dictionary<System.Int32,System.Int32> materialReferenceIndexLookup) { }

    }

    // TypeToken: 0x2000012  // size: 0x30
    public class MaterialReferenceManager
    {
        // Fields
        private static UnityEngine.TextCore.Text.MaterialReferenceManager s_Instance;  // static @ 0x0
        private System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Material> m_FontMaterialReferenceLookup;  // 0x10
        private System.Collections.Generic.Dictionary<System.Int32,UnityEngine.TextCore.Text.FontAsset> m_FontAssetReferenceLookup;  // 0x18
        private System.Collections.Generic.Dictionary<System.Int32,UnityEngine.TextCore.Text.SpriteAsset> m_SpriteAssetReferenceLookup;  // 0x20
        private System.Collections.Generic.Dictionary<System.Int32,UnityEngine.TextCore.Text.TextColorGradient> m_ColorGradientReferenceLookup;  // 0x28

        // Properties
        UnityEngine.TextCore.Text.MaterialReferenceManager instance { get; /* RVA: 0x0B45104C */ }

        // Methods
        // RVA: 0x0B450A80  token: 0x6000091
        public static System.Void AddFontAsset(UnityEngine.TextCore.Text.FontAsset fontAsset) { }
        // RVA: 0x0B4509A0  token: 0x6000092
        private System.Void AddFontAssetInternal(UnityEngine.TextCore.Text.FontAsset fontAsset) { }
        // RVA: 0x0B450C08  token: 0x6000093
        public static System.Void AddSpriteAsset(System.Int32 hashCode, UnityEngine.TextCore.Text.SpriteAsset spriteAsset) { }
        // RVA: 0x0B450B50  token: 0x6000094
        private System.Void AddSpriteAssetInternal(System.Int32 hashCode, UnityEngine.TextCore.Text.SpriteAsset spriteAsset) { }
        // RVA: 0x0B450B14  token: 0x6000095
        public static System.Void AddFontMaterial(System.Int32 hashCode, UnityEngine.Material material) { }
        // RVA: 0x0B450AB0  token: 0x6000096
        private System.Void AddFontMaterialInternal(System.Int32 hashCode, UnityEngine.Material material) { }
        // RVA: 0x0B450964  token: 0x6000097
        public static System.Void AddColorGradientPreset(System.Int32 hashCode, UnityEngine.TextCore.Text.TextColorGradient spriteAsset) { }
        // RVA: 0x0B4508D8  token: 0x6000098
        private System.Void AddColorGradientPreset_Internal(System.Int32 hashCode, UnityEngine.TextCore.Text.TextColorGradient spriteAsset) { }
        // RVA: 0x0B450D60  token: 0x6000099
        public static System.Boolean TryGetFontAsset(System.Int32 hashCode, UnityEngine.TextCore.Text.FontAsset& fontAsset) { }
        // RVA: 0x0B450CF0  token: 0x600009A
        private System.Boolean TryGetFontAssetInternal(System.Int32 hashCode, UnityEngine.TextCore.Text.FontAsset& fontAsset) { }
        // RVA: 0x0B450EB8  token: 0x600009B
        public static System.Boolean TryGetSpriteAsset(System.Int32 hashCode, UnityEngine.TextCore.Text.SpriteAsset& spriteAsset) { }
        // RVA: 0x0B450E48  token: 0x600009C
        private System.Boolean TryGetSpriteAssetInternal(System.Int32 hashCode, UnityEngine.TextCore.Text.SpriteAsset& spriteAsset) { }
        // RVA: 0x0B450CB4  token: 0x600009D
        public static System.Boolean TryGetColorGradientPreset(System.Int32 hashCode, UnityEngine.TextCore.Text.TextColorGradient& gradientPreset) { }
        // RVA: 0x0B450C44  token: 0x600009E
        private System.Boolean TryGetColorGradientPresetInternal(System.Int32 hashCode, UnityEngine.TextCore.Text.TextColorGradient& gradientPreset) { }
        // RVA: 0x0B450E0C  token: 0x600009F
        public static System.Boolean TryGetMaterial(System.Int32 hashCode, UnityEngine.Material& material) { }
        // RVA: 0x0B450D9C  token: 0x60000A0
        private System.Boolean TryGetMaterialInternal(System.Int32 hashCode, UnityEngine.Material& material) { }
        // RVA: 0x0B450EF4  token: 0x60000A1
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000013  // size: 0x14
    public sealed struct VertexSortingOrder
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.TextCore.Text.VertexSortingOrder Normal;  // const
        public static UnityEngine.TextCore.Text.VertexSortingOrder Reverse;  // const

    }

    // TypeToken: 0x2000014  // size: 0x48
    public sealed struct MeshInfo
    {
        // Fields
        private static readonly UnityEngine.Color32 k_DefaultColor;  // static @ 0x0
        public System.Int32 vertexCount;  // 0x10
        public UnityEngine.Vector3[] vertices;  // 0x18
        public UnityEngine.Vector2[] uvs0;  // 0x20
        public UnityEngine.Vector2[] uvs2;  // 0x28
        public UnityEngine.Color32[] colors32;  // 0x30
        public System.Int32[] triangles;  // 0x38
        public UnityEngine.Material material;  // 0x40

        // Methods
        // RVA: 0x0B4520EC  token: 0x60000A2
        public System.Void .ctor(System.Int32 size) { }
        // RVA: 0x0B4515D4  token: 0x60000A3
        private System.Void ResizeMeshInfo(System.Int32 size) { }
        // RVA: 0x0B4515A4  token: 0x60000A4
        private System.Void Clear(System.Boolean uploadChanges) { }
        // RVA: 0x0B451570  token: 0x60000A5
        private System.Void ClearUnusedVertices() { }
        // RVA: 0x0B4517C0  token: 0x60000A6
        private System.Void SortGeometry(UnityEngine.TextCore.Text.VertexSortingOrder order) { }
        // RVA: 0x0B45185C  token: 0x60000A7
        private System.Void SwapVertexData(System.Int32 src, System.Int32 dst) { }
        // RVA: 0x0B4520AC  token: 0x60000A8
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000015  // size: 0xE0
    public class SpriteAsset : UnityEngine.TextCore.Text.TextAsset
    {
        // Fields
        private System.Collections.Generic.Dictionary<System.Int32,System.Int32> m_NameLookup;  // 0x38
        private System.Collections.Generic.Dictionary<System.UInt32,System.Int32> m_GlyphIndexLookup;  // 0x40
        private UnityEngine.TextCore.FaceInfo m_FaceInfo;  // 0x48
        private UnityEngine.Texture m_SpriteAtlasTexture;  // 0xa8
        private System.Collections.Generic.List<UnityEngine.TextCore.Text.SpriteCharacter> m_SpriteCharacterTable;  // 0xb0
        private System.Collections.Generic.Dictionary<System.UInt32,UnityEngine.TextCore.Text.SpriteCharacter> m_SpriteCharacterLookup;  // 0xb8
        private System.Collections.Generic.List<UnityEngine.TextCore.Text.SpriteGlyph> m_SpriteGlyphTable;  // 0xc0
        private System.Collections.Generic.Dictionary<System.UInt32,UnityEngine.TextCore.Text.SpriteGlyph> m_SpriteGlyphLookup;  // 0xc8
        public System.Collections.Generic.List<UnityEngine.TextCore.Text.SpriteAsset> fallbackSpriteAssets;  // 0xd0
        private System.Boolean m_IsSpriteAssetLookupTablesDirty;  // 0xd8
        private static System.Collections.Generic.HashSet<System.Int32> k_searchedSpriteAssets;  // static @ 0x0

        // Properties
        UnityEngine.TextCore.FaceInfo faceInfo { get; /* RVA: 0x04DBD640 */ set; /* RVA: 0x0B330F50 */ }
        UnityEngine.Texture spriteSheet { get; /* RVA: 0x04DBCFF0 */ set; /* RVA: 0x04D84CC0 */ }
        System.Collections.Generic.List<UnityEngine.TextCore.Text.SpriteCharacter> spriteCharacterTable { get; /* RVA: 0x0B453848 */ set; /* RVA: 0x04D84CE0 */ }
        System.Collections.Generic.Dictionary<System.UInt32,UnityEngine.TextCore.Text.SpriteCharacter> spriteCharacterLookupTable { get; /* RVA: 0x0B453818 */ set; /* RVA: 0x06AC5AF4 */ }
        System.Collections.Generic.List<UnityEngine.TextCore.Text.SpriteGlyph> spriteGlyphTable { get; /* RVA: 0x04DBD0B0 */ set; /* RVA: 0x06AC5ACC */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60000B3
        private System.Void Awake() { }
        // RVA: 0x0B45323C  token: 0x60000B4
        public System.Void UpdateLookupTables() { }
        // RVA: 0x0B4524C4  token: 0x60000B5
        public System.Int32 GetSpriteIndexFromHashcode(System.Int32 hashCode) { }
        // RVA: 0x0B452580  token: 0x60000B6
        public System.Int32 GetSpriteIndexFromUnicode(System.UInt32 unicode) { }
        // RVA: 0x0B452540  token: 0x60000B7
        public System.Int32 GetSpriteIndexFromName(System.String name) { }
        // RVA: 0x0B452D7C  token: 0x60000B8
        public static UnityEngine.TextCore.Text.SpriteAsset SearchForSpriteByUnicode(UnityEngine.TextCore.Text.SpriteAsset spriteAsset, System.UInt32 unicode, System.Boolean includeFallbacks, System.Int32& spriteIndex) { }
        // RVA: 0x0B452B64  token: 0x60000B9
        private static UnityEngine.TextCore.Text.SpriteAsset SearchForSpriteByUnicodeInternal(System.Collections.Generic.List<UnityEngine.TextCore.Text.SpriteAsset> spriteAssets, System.UInt32 unicode, System.Boolean includeFallbacks, System.Int32& spriteIndex) { }
        // RVA: 0x0B452CBC  token: 0x60000BA
        private static UnityEngine.TextCore.Text.SpriteAsset SearchForSpriteByUnicodeInternal(UnityEngine.TextCore.Text.SpriteAsset spriteAsset, System.UInt32 unicode, System.Boolean includeFallbacks, System.Int32& spriteIndex) { }
        // RVA: 0x0B452824  token: 0x60000BB
        public static UnityEngine.TextCore.Text.SpriteAsset SearchForSpriteByHashCode(UnityEngine.TextCore.Text.SpriteAsset spriteAsset, System.Int32 hashCode, System.Boolean includeFallbacks, System.Int32& spriteIndex, UnityEngine.TextCore.Text.TextSettings textSettings) { }
        // RVA: 0x0B45260C  token: 0x60000BC
        private static UnityEngine.TextCore.Text.SpriteAsset SearchForSpriteByHashCodeInternal(System.Collections.Generic.List<UnityEngine.TextCore.Text.SpriteAsset> spriteAssets, System.Int32 hashCode, System.Boolean searchFallbacks, System.Int32& spriteIndex) { }
        // RVA: 0x0B452764  token: 0x60000BD
        private static UnityEngine.TextCore.Text.SpriteAsset SearchForSpriteByHashCodeInternal(UnityEngine.TextCore.Text.SpriteAsset spriteAsset, System.Int32 hashCode, System.Boolean searchFallbacks, System.Int32& spriteIndex) { }
        // RVA: 0x0B4530D0  token: 0x60000BE
        public System.Void SortGlyphTable() { }
        // RVA: 0x0B452F44  token: 0x60000BF
        private System.Void SortCharacterTable() { }
        // RVA: 0x0B4530B0  token: 0x60000C0
        private System.Void SortGlyphAndCharacterTables() { }
        // RVA: 0x0B45373C  token: 0x60000C1
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000017  // size: 0x40
    public class SpriteCharacter : UnityEngine.TextCore.Text.TextElement
    {
        // Fields
        private System.String m_Name;  // 0x30
        private System.Int32 m_HashCode;  // 0x38

        // Properties
        System.String name { get; /* RVA: 0x04DBBEB0 */ }

        // Methods
        // RVA: 0x04DBA3A0  token: 0x60000C7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000018  // size: 0x50
    public class SpriteGlyph : UnityEngine.TextCore.Glyph
    {
        // Fields
        public UnityEngine.Sprite sprite;  // 0x48

        // Methods
        // RVA: 0x0B3311A8  token: 0x60000C8
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000019  // size: 0x38
    public abstract class TextAsset : UnityEngine.ScriptableObject
    {
        // Fields
        private System.String m_Version;  // 0x18
        private System.Int32 m_InstanceID;  // 0x20
        private System.Int32 m_HashCode;  // 0x24
        private UnityEngine.Material m_Material;  // 0x28
        private System.Int32 m_MaterialHashCode;  // 0x30

        // Properties
        System.String version { get; /* RVA: 0x04D96330 */ set; /* RVA: 0x05392C40 */ }
        System.Int32 instanceID { get; /* RVA: 0x0B4538A4 */ }
        System.Int32 hashCode { get; /* RVA: 0x0B453878 */ set; /* RVA: 0x011F0030 */ }
        UnityEngine.Material material { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }
        System.Int32 materialHashCode { get; /* RVA: 0x0B4538C8 */ set; /* RVA: 0x04D86740 */ }

        // Methods
        // RVA: 0x03573370  token: 0x60000D2
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200001A  // size: 0x14
    public sealed struct ColorGradientMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.TextCore.Text.ColorGradientMode Single;  // const
        public static UnityEngine.TextCore.Text.ColorGradientMode HorizontalGradient;  // const
        public static UnityEngine.TextCore.Text.ColorGradientMode VerticalGradient;  // const
        public static UnityEngine.TextCore.Text.ColorGradientMode FourCornersGradient;  // const

    }

    // TypeToken: 0x200001B  // size: 0x60
    public class TextColorGradient : UnityEngine.ScriptableObject
    {
        // Fields
        public UnityEngine.TextCore.Text.ColorGradientMode colorMode;  // 0x18
        public UnityEngine.Color topLeft;  // 0x1c
        public UnityEngine.Color topRight;  // 0x2c
        public UnityEngine.Color bottomLeft;  // 0x3c
        public UnityEngine.Color bottomRight;  // 0x4c
        private static UnityEngine.TextCore.Text.ColorGradientMode k_DefaultColorMode;  // const
        private static readonly UnityEngine.Color k_DefaultColor;  // static @ 0x0

        // Methods
        // RVA: 0x0B45399C  token: 0x60000D3
        public System.Void .ctor() { }
        // RVA: 0x0B30CB60  token: 0x60000D4
        public System.Void .ctor(UnityEngine.Color color) { }
        // RVA: 0x0B30CBA4  token: 0x60000D5
        public System.Void .ctor(UnityEngine.Color color0, UnityEngine.Color color1, UnityEngine.Color color2, UnityEngine.Color color3) { }
        // RVA: 0x0B453958  token: 0x60000D6
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200001C  // size: 0x11
    public sealed struct TextElementType
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static UnityEngine.TextCore.Text.TextElementType Character;  // const
        public static UnityEngine.TextCore.Text.TextElementType Sprite;  // const

    }

    // TypeToken: 0x200001D  // size: 0x30
    public abstract class TextElement
    {
        // Fields
        protected UnityEngine.TextCore.Text.TextElementType m_ElementType;  // 0x10
        private System.UInt32 m_Unicode;  // 0x14
        private UnityEngine.TextCore.Text.TextAsset m_TextAsset;  // 0x18
        private UnityEngine.TextCore.Glyph m_Glyph;  // 0x20
        private System.UInt32 m_GlyphIndex;  // 0x28
        private System.Single m_Scale;  // 0x2c

        // Properties
        UnityEngine.TextCore.Text.TextElementType elementType { get; /* RVA: 0x04DBCFA0 */ }
        System.UInt32 unicode { get; /* RVA: 0x04D872D0 */ set; /* RVA: 0x04D8F540 */ }
        UnityEngine.TextCore.Text.TextAsset textAsset { get; /* RVA: 0x04D96330 */ set; /* RVA: 0x05392C40 */ }
        UnityEngine.TextCore.Glyph glyph { get; /* RVA: 0x04DBA910 */ set; /* RVA: 0x05390F40 */ }
        System.UInt32 glyphIndex { get; /* RVA: 0x04DBBEC0 */ set; /* RVA: 0x011F2AA0 */ }
        System.Single scale { get; /* RVA: 0x04DBD1B0 */ set; /* RVA: 0x04D86B00 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x60000E2
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200001E  // size: 0x38
    public sealed struct TextVertex
    {
        // Fields
        public UnityEngine.Vector3 position;  // 0x10
        public UnityEngine.Vector2 uv;  // 0x1c
        public UnityEngine.Vector2 uv2;  // 0x24
        public UnityEngine.Vector2 uv4;  // 0x2c
        public UnityEngine.Color32 color;  // 0x34

    }

    // TypeToken: 0x200001F  // size: 0x168
    public sealed struct TextElementInfo
    {
        // Fields
        public System.Char character;  // 0x10
        public System.Int32 index;  // 0x14
        public UnityEngine.TextCore.Text.TextElementType elementType;  // 0x18
        public UnityEngine.TextCore.Text.TextElement textElement;  // 0x20
        public UnityEngine.TextCore.Text.FontAsset fontAsset;  // 0x28
        public UnityEngine.TextCore.Text.SpriteAsset spriteAsset;  // 0x30
        public System.Int32 spriteIndex;  // 0x38
        public UnityEngine.Material material;  // 0x40
        public System.Int32 materialReferenceIndex;  // 0x48
        public System.Boolean isUsingAlternateTypeface;  // 0x4c
        public System.Single pointSize;  // 0x50
        public System.Int32 lineNumber;  // 0x54
        public System.Int32 pageNumber;  // 0x58
        public System.Int32 vertexIndex;  // 0x5c
        public UnityEngine.TextCore.Text.TextVertex vertexTopLeft;  // 0x60
        public UnityEngine.TextCore.Text.TextVertex vertexBottomLeft;  // 0x88
        public UnityEngine.TextCore.Text.TextVertex vertexTopRight;  // 0xb0
        public UnityEngine.TextCore.Text.TextVertex vertexBottomRight;  // 0xd8
        public UnityEngine.Vector3 topLeft;  // 0x100
        public UnityEngine.Vector3 bottomLeft;  // 0x10c
        public UnityEngine.Vector3 topRight;  // 0x118
        public UnityEngine.Vector3 bottomRight;  // 0x124
        public System.Single origin;  // 0x130
        public System.Single ascender;  // 0x134
        public System.Single baseLine;  // 0x138
        public System.Single descender;  // 0x13c
        public System.Single xAdvance;  // 0x140
        public System.Single aspectRatio;  // 0x144
        public System.Single scale;  // 0x148
        public UnityEngine.Color32 color;  // 0x14c
        public UnityEngine.Color32 underlineColor;  // 0x150
        public UnityEngine.Color32 strikethroughColor;  // 0x154
        public UnityEngine.Color32 highlightColor;  // 0x158
        public UnityEngine.TextCore.Text.FontStyles style;  // 0x15c
        public System.Boolean isVisible;  // 0x160

    }

    // TypeToken: 0x2000020  // size: 0x14
    public sealed struct TextOverflowMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.TextCore.Text.TextOverflowMode Overflow;  // const
        public static UnityEngine.TextCore.Text.TextOverflowMode Ellipsis;  // const
        public static UnityEngine.TextCore.Text.TextOverflowMode Masking;  // const
        public static UnityEngine.TextCore.Text.TextOverflowMode Truncate;  // const
        public static UnityEngine.TextCore.Text.TextOverflowMode ScrollRect;  // const
        public static UnityEngine.TextCore.Text.TextOverflowMode Page;  // const
        public static UnityEngine.TextCore.Text.TextOverflowMode Linked;  // const

    }

    // TypeToken: 0x2000021  // size: 0x14
    public sealed struct TextureMapping
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.TextCore.Text.TextureMapping Character;  // const
        public static UnityEngine.TextCore.Text.TextureMapping Line;  // const
        public static UnityEngine.TextCore.Text.TextureMapping Paragraph;  // const
        public static UnityEngine.TextCore.Text.TextureMapping MatchAspect;  // const

    }

    // TypeToken: 0x2000022  // size: 0x100
    public class TextGenerationSettings : System.IEquatable`1
    {
        // Fields
        public System.String text;  // 0x10
        public UnityEngine.Rect screenRect;  // 0x18
        public UnityEngine.Vector4 margins;  // 0x28
        public System.Single scale;  // 0x38
        public UnityEngine.TextCore.Text.FontAsset fontAsset;  // 0x40
        public UnityEngine.Material material;  // 0x48
        public UnityEngine.TextCore.Text.SpriteAsset spriteAsset;  // 0x50
        public UnityEngine.TextCore.Text.TextStyleSheet styleSheet;  // 0x58
        public UnityEngine.TextCore.Text.FontStyles fontStyle;  // 0x60
        public UnityEngine.TextCore.Text.TextSettings textSettings;  // 0x68
        public UnityEngine.TextCore.Text.TextAlignment textAlignment;  // 0x70
        public UnityEngine.TextCore.Text.TextOverflowMode overflowMode;  // 0x74
        public System.Boolean wordWrap;  // 0x78
        public System.Single wordWrappingRatio;  // 0x7c
        public UnityEngine.Color color;  // 0x80
        public UnityEngine.TextCore.Text.TextColorGradient fontColorGradient;  // 0x90
        public System.Boolean tintSprites;  // 0x98
        public System.Boolean overrideRichTextColors;  // 0x99
        public System.Single fontSize;  // 0x9c
        public System.Boolean autoSize;  // 0xa0
        public System.Single fontSizeMin;  // 0xa4
        public System.Single fontSizeMax;  // 0xa8
        public System.Boolean enableKerning;  // 0xac
        public System.Boolean richText;  // 0xad
        public System.Boolean isRightToLeft;  // 0xae
        public System.Boolean extraPadding;  // 0xaf
        public System.Boolean parseControlCharacters;  // 0xb0
        public System.Single characterSpacing;  // 0xb4
        public System.Single wordSpacing;  // 0xb8
        public System.Single lineSpacing;  // 0xbc
        public System.Single paragraphSpacing;  // 0xc0
        public System.Single lineSpacingMax;  // 0xc4
        public System.Int32 maxVisibleCharacters;  // 0xc8
        public System.Int32 maxVisibleWords;  // 0xcc
        public System.Int32 maxVisibleLines;  // 0xd0
        public System.Int32 firstVisibleCharacter;  // 0xd4
        public System.Boolean useMaxVisibleDescender;  // 0xd8
        public UnityEngine.TextCore.Text.TextFontWeight fontWeight;  // 0xdc
        public System.Int32 pageToDisplay;  // 0xe0
        public UnityEngine.TextCore.Text.TextureMapping horizontalMapping;  // 0xe4
        public UnityEngine.TextCore.Text.TextureMapping verticalMapping;  // 0xe8
        public System.Single uvLineOffset;  // 0xec
        public UnityEngine.TextCore.Text.VertexSortingOrder geometrySortingOrder;  // 0xf0
        public System.Boolean inverseYAxis;  // 0xf4
        public System.Single charWidthMaxAdj;  // 0xf8

        // Methods
        // RVA: 0x0B453A48  token: 0x60000E3
        public virtual System.Boolean Equals(UnityEngine.TextCore.Text.TextGenerationSettings other) { }
        // RVA: 0x0B453E74  token: 0x60000E4
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0B453F10  token: 0x60000E5
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B45441C  token: 0x60000E6
        public virtual System.String ToString() { }
        // RVA: 0x0B4556A0  token: 0x60000E7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000023  // size: 0xA58
    public class TextGenerator
    {
        // Fields
        private static UnityEngine.TextCore.Text.TextGenerator s_TextGenerator;  // static @ 0x0
        private UnityEngine.Vector3[] m_RectTransformCorners;  // 0x10
        private System.Single m_MarginWidth;  // 0x18
        private System.Single m_MarginHeight;  // 0x1c
        private System.Int32[] m_CharBuffer;  // 0x20
        private System.Single m_PreferredWidth;  // 0x28
        private System.Single m_PreferredHeight;  // 0x2c
        private UnityEngine.TextCore.Text.FontAsset m_CurrentFontAsset;  // 0x30
        private UnityEngine.Material m_CurrentMaterial;  // 0x38
        private System.Int32 m_CurrentMaterialIndex;  // 0x40
        private UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.TextCore.Text.MaterialReference> m_MaterialReferenceStack;  // 0x48
        private System.Single m_Padding;  // 0xa0
        private UnityEngine.TextCore.Text.SpriteAsset m_CurrentSpriteAsset;  // 0xa8
        private System.Int32 m_TotalCharacterCount;  // 0xb0
        private System.Single m_FontScale;  // 0xb4
        private System.Single m_FontSize;  // 0xb8
        private System.Single m_FontScaleMultiplier;  // 0xbc
        private System.Single m_CurrentFontSize;  // 0xc0
        private UnityEngine.TextCore.Text.TextProcessingStack<System.Single> m_SizeStack;  // 0xc8
        private UnityEngine.TextCore.Text.FontStyles m_FontStyleInternal;  // 0xe8
        private UnityEngine.TextCore.Text.FontStyleStack m_FontStyleStack;  // 0xec
        private UnityEngine.TextCore.Text.TextFontWeight m_FontWeightInternal;  // 0xf8
        private UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.TextCore.Text.TextFontWeight> m_FontWeightStack;  // 0x100
        private UnityEngine.TextCore.Text.TextAlignment m_LineJustification;  // 0x120
        private UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.TextCore.Text.TextAlignment> m_LineJustificationStack;  // 0x128
        private System.Single m_BaselineOffset;  // 0x148
        private UnityEngine.TextCore.Text.TextProcessingStack<System.Single> m_BaselineOffsetStack;  // 0x150
        private UnityEngine.Color32 m_FontColor32;  // 0x170
        private UnityEngine.Color32 m_HtmlColor;  // 0x174
        private UnityEngine.Color32 m_UnderlineColor;  // 0x178
        private UnityEngine.Color32 m_StrikethroughColor;  // 0x17c
        private UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.Color32> m_ColorStack;  // 0x180
        private UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.Color32> m_UnderlineColorStack;  // 0x1a0
        private UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.Color32> m_StrikethroughColorStack;  // 0x1c0
        private UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.Color32> m_HighlightColorStack;  // 0x1e0
        private UnityEngine.TextCore.Text.TextColorGradient m_ColorGradientPreset;  // 0x200
        private UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.TextCore.Text.TextColorGradient> m_ColorGradientStack;  // 0x208
        private UnityEngine.TextCore.Text.TextProcessingStack<System.Int32> m_ActionStack;  // 0x230
        private System.Boolean m_IsFxMatrixSet;  // 0x250
        private System.Single m_LineOffset;  // 0x254
        private System.Single m_LineHeight;  // 0x258
        private System.Single m_CSpacing;  // 0x25c
        private System.Single m_MonoSpacing;  // 0x260
        private System.Single m_XAdvance;  // 0x264
        private System.Single m_TagLineIndent;  // 0x268
        private System.Single m_TagIndent;  // 0x26c
        private UnityEngine.TextCore.Text.TextProcessingStack<System.Single> m_IndentStack;  // 0x270
        private System.Boolean m_TagNoParsing;  // 0x290
        private System.Int32 m_CharacterCount;  // 0x294
        private System.Int32 m_FirstCharacterOfLine;  // 0x298
        private System.Int32 m_LastCharacterOfLine;  // 0x29c
        private System.Int32 m_FirstVisibleCharacterOfLine;  // 0x2a0
        private System.Int32 m_LastVisibleCharacterOfLine;  // 0x2a4
        private System.Single m_MaxLineAscender;  // 0x2a8
        private System.Single m_MaxLineDescender;  // 0x2ac
        private System.Int32 m_LineNumber;  // 0x2b0
        private System.Int32 m_LineVisibleCharacterCount;  // 0x2b4
        private System.Int32 m_FirstOverflowCharacterIndex;  // 0x2b8
        private System.Int32 m_PageNumber;  // 0x2bc
        private System.Single m_MarginLeft;  // 0x2c0
        private System.Single m_MarginRight;  // 0x2c4
        private System.Single m_Width;  // 0x2c8
        private UnityEngine.TextCore.Text.Extents m_MeshExtents;  // 0x2cc
        private System.Single m_MaxCapHeight;  // 0x2dc
        private System.Single m_MaxAscender;  // 0x2e0
        private System.Single m_MaxDescender;  // 0x2e4
        private System.Boolean m_IsNewPage;  // 0x2e8
        private System.Boolean m_IsNonBreakingSpace;  // 0x2e9
        private UnityEngine.TextCore.Text.WordWrapState m_SavedWordWrapState;  // 0x2f0
        private UnityEngine.TextCore.Text.WordWrapState m_SavedLineState;  // 0x600
        private System.Int32 m_LoopCountA;  // 0x910
        private UnityEngine.TextCore.Text.TextElementType m_TextElementType;  // 0x914
        private System.Boolean m_IsParsingText;  // 0x915
        private System.Int32 m_SpriteIndex;  // 0x918
        private UnityEngine.Color32 m_SpriteColor;  // 0x91c
        private UnityEngine.TextCore.Text.TextElement m_CachedTextElement;  // 0x920
        private UnityEngine.Color32 m_HighlightColor;  // 0x928
        private System.Single m_CharWidthAdjDelta;  // 0x92c
        private UnityEngine.Matrix4x4 m_FxMatrix;  // 0x930
        private System.Single m_MaxFontSize;  // 0x970
        private System.Single m_MinFontSize;  // 0x974
        private System.Boolean m_IsCharacterWrappingEnabled;  // 0x978
        private System.Single m_StartOfLineAscender;  // 0x97c
        private System.Single m_LineSpacingDelta;  // 0x980
        private System.Boolean m_IsMaskingEnabled;  // 0x984
        private UnityEngine.TextCore.Text.MaterialReference[] m_MaterialReferences;  // 0x988
        private System.Int32 m_SpriteCount;  // 0x990
        private UnityEngine.TextCore.Text.TextProcessingStack<System.Int32> m_StyleStack;  // 0x998
        private System.Int32 m_SpriteAnimationId;  // 0x9b8
        private System.UInt32[] m_InternalTextParsingBuffer;  // 0x9c0
        private UnityEngine.TextCore.Text.RichTextTagAttribute[] m_Attributes;  // 0x9c8
        private UnityEngine.TextCore.Text.XmlTagAttribute[] m_XmlAttribute;  // 0x9d0
        private System.Char[] m_RichTextTag;  // 0x9d8
        private System.Collections.Generic.Dictionary<System.Int32,System.Int32> m_MaterialReferenceIndexLookup;  // 0x9e0
        private System.Boolean m_IsCalculatingPreferredValues;  // 0x9e8
        private UnityEngine.TextCore.Text.SpriteAsset m_DefaultSpriteAsset;  // 0x9f0
        private System.Boolean m_TintSprite;  // 0x9f8
        protected UnityEngine.TextCore.Text.TextGenerator.SpecialCharacter m_Ellipsis;  // 0xa00
        protected UnityEngine.TextCore.Text.TextGenerator.SpecialCharacter m_Underline;  // 0xa20
        private System.Boolean m_IsUsingBold;  // 0xa40
        private System.Boolean m_IsSdfShader;  // 0xa41
        private UnityEngine.TextCore.Text.TextElementInfo[] m_InternalTextElementInfo;  // 0xa48
        private System.Int32 m_RecursiveCount;  // 0xa50

        // Methods
        // RVA: 0x0B4410F8  token: 0x60000E8
        private static UnityEngine.TextCore.Text.TextGenerator GetTextGenerator() { }
        // RVA: 0x0B4405A0  token: 0x60000E9
        public static System.Void GenerateText(UnityEngine.TextCore.Text.TextGenerationSettings settings, UnityEngine.TextCore.Text.TextInfo textInfo) { }
        // RVA: 0x0B4406E0  token: 0x60000EA
        public static UnityEngine.Vector2 GetCursorPosition(UnityEngine.TextCore.Text.TextInfo textInfo, UnityEngine.Rect screenRect, System.Int32 index, System.Boolean inverseYAxis) { }
        // RVA: 0x0B440C90  token: 0x60000EB
        public static UnityEngine.Vector2 GetPreferredValues(UnityEngine.TextCore.Text.TextGenerationSettings settings, UnityEngine.TextCore.Text.TextInfo textInfo) { }
        // RVA: 0x0B4412D0  token: 0x60000EC
        private System.Void Prepare(UnityEngine.TextCore.Text.TextGenerationSettings generationSettings, UnityEngine.TextCore.Text.TextInfo textInfo) { }
        // RVA: 0x0B432DF0  token: 0x60000ED
        private System.Void GenerateTextMesh(UnityEngine.TextCore.Text.TextGenerationSettings generationSettings, UnityEngine.TextCore.Text.TextInfo textInfo) { }
        // RVA: 0x0B4429B0  token: 0x60000EE
        private System.Void SaveWordWrappingState(UnityEngine.TextCore.Text.WordWrapState& state, System.Int32 index, System.Int32 count, UnityEngine.TextCore.Text.TextInfo textInfo) { }
        // RVA: 0x0B441420  token: 0x60000EF
        protected System.Int32 RestoreWordWrappingState(UnityEngine.TextCore.Text.WordWrapState& state, UnityEngine.TextCore.Text.TextInfo textInfo) { }
        // RVA: 0x0B443F20  token: 0x60000F0
        protected System.Boolean ValidateHtmlTag(System.Int32[] chars, System.Int32 startIndex, System.Int32& endIndex, UnityEngine.TextCore.Text.TextGenerationSettings generationSettings, UnityEngine.TextCore.Text.TextInfo textInfo) { }
        // RVA: 0x0B441918  token: 0x60000F1
        private System.Void SaveGlyphVertexInfo(System.Single padding, System.Single stylePadding, UnityEngine.Color32 vertexColor, UnityEngine.TextCore.Text.TextGenerationSettings generationSettings, UnityEngine.TextCore.Text.TextInfo textInfo) { }
        // RVA: 0x0B442218  token: 0x60000F2
        private System.Void SaveSpriteVertexInfo(UnityEngine.Color32 vertexColor, UnityEngine.TextCore.Text.TextGenerationSettings generationSettings, UnityEngine.TextCore.Text.TextInfo textInfo) { }
        // RVA: 0x0B43F334  token: 0x60000F3
        private System.Void DrawUnderlineMesh(UnityEngine.Vector3 start, UnityEngine.Vector3 end, System.Int32& index, System.Single startScale, System.Single endScale, System.Single maxScale, System.Single sdfScale, UnityEngine.Color32 underlineColor, UnityEngine.TextCore.Text.TextGenerationSettings generationSettings, UnityEngine.TextCore.Text.TextInfo textInfo) { }
        // RVA: 0x0B43ECE8  token: 0x60000F4
        private System.Void DrawTextHighlight(UnityEngine.Vector3 start, UnityEngine.Vector3 end, System.Int32& index, UnityEngine.Color32 highlightColor, UnityEngine.TextCore.Text.TextGenerationSettings generationSettings, UnityEngine.TextCore.Text.TextInfo textInfo) { }
        // RVA: 0x0B43EBAC  token: 0x60000F5
        private static System.Void ClearMesh(System.Boolean updateMesh, UnityEngine.TextCore.Text.TextInfo textInfo) { }
        // RVA: 0x04DBD470  token: 0x60000F6
        private System.Void EnableMasking() { }
        // RVA: 0x04DBD460  token: 0x60000F7
        private System.Void DisableMasking() { }
        // RVA: 0x0B442E44  token: 0x60000F8
        private System.Void SetArraySizes(System.Int32[] chars, UnityEngine.TextCore.Text.TextGenerationSettings generationSettings, UnityEngine.TextCore.Text.TextInfo textInfo) { }
        // RVA: 0x0B440DC4  token: 0x60000F9
        private UnityEngine.TextCore.Text.TextElement GetTextElement(UnityEngine.TextCore.Text.TextGenerationSettings generationSettings, System.UInt32 unicode, UnityEngine.TextCore.Text.FontAsset fontAsset, UnityEngine.TextCore.Text.FontStyles fontStyle, UnityEngine.TextCore.Text.TextFontWeight fontWeight, System.Boolean& isUsingAlternativeTypeface) { }
        // RVA: 0x0B43EBD0  token: 0x60000FA
        private System.Void ComputeMarginSize(UnityEngine.Rect rect, UnityEngine.Vector4 margins) { }
        // RVA: 0x0B440D94  token: 0x60000FB
        protected System.Void GetSpecialCharacters(UnityEngine.TextCore.Text.TextGenerationSettings generationSettings) { }
        // RVA: 0x0B440878  token: 0x60000FC
        protected System.Void GetEllipsisSpecialCharacter(UnityEngine.TextCore.Text.TextGenerationSettings generationSettings) { }
        // RVA: 0x0B441188  token: 0x60000FD
        protected System.Void GetUnderlineSpecialCharacter(UnityEngine.TextCore.Text.TextGenerationSettings generationSettings) { }
        // RVA: 0x0B440A74  token: 0x60000FE
        private System.Single GetPaddingForMaterial(UnityEngine.Material material, System.Boolean extraPadding) { }
        // RVA: 0x0B440B74  token: 0x60000FF
        private UnityEngine.Vector2 GetPreferredValuesInternal(UnityEngine.TextCore.Text.TextGenerationSettings generationSettings, UnityEngine.TextCore.Text.TextInfo textInfo) { }
        // RVA: 0x0B43CC48  token: 0x6000100
        protected virtual UnityEngine.Vector2 CalculatePreferredValues(System.Single defaultFontSize, UnityEngine.Vector2 marginSize, System.Boolean ignoreTextAutoSizing, UnityEngine.TextCore.Text.TextGenerationSettings generationSettings, UnityEngine.TextCore.Text.TextInfo textInfo) { }
        // RVA: 0x0B448238  token: 0x6000101
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000025  // size: 0x14
    public sealed struct TextAlignment
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.TextCore.Text.TextAlignment TopLeft;  // const
        public static UnityEngine.TextCore.Text.TextAlignment TopCenter;  // const
        public static UnityEngine.TextCore.Text.TextAlignment TopRight;  // const
        public static UnityEngine.TextCore.Text.TextAlignment TopJustified;  // const
        public static UnityEngine.TextCore.Text.TextAlignment TopFlush;  // const
        public static UnityEngine.TextCore.Text.TextAlignment TopGeoAligned;  // const
        public static UnityEngine.TextCore.Text.TextAlignment MiddleLeft;  // const
        public static UnityEngine.TextCore.Text.TextAlignment MiddleCenter;  // const
        public static UnityEngine.TextCore.Text.TextAlignment MiddleRight;  // const
        public static UnityEngine.TextCore.Text.TextAlignment MiddleJustified;  // const
        public static UnityEngine.TextCore.Text.TextAlignment MiddleFlush;  // const
        public static UnityEngine.TextCore.Text.TextAlignment MiddleGeoAligned;  // const
        public static UnityEngine.TextCore.Text.TextAlignment BottomLeft;  // const
        public static UnityEngine.TextCore.Text.TextAlignment BottomCenter;  // const
        public static UnityEngine.TextCore.Text.TextAlignment BottomRight;  // const
        public static UnityEngine.TextCore.Text.TextAlignment BottomJustified;  // const
        public static UnityEngine.TextCore.Text.TextAlignment BottomFlush;  // const
        public static UnityEngine.TextCore.Text.TextAlignment BottomGeoAligned;  // const
        public static UnityEngine.TextCore.Text.TextAlignment BaselineLeft;  // const
        public static UnityEngine.TextCore.Text.TextAlignment BaselineCenter;  // const
        public static UnityEngine.TextCore.Text.TextAlignment BaselineRight;  // const
        public static UnityEngine.TextCore.Text.TextAlignment BaselineJustified;  // const
        public static UnityEngine.TextCore.Text.TextAlignment BaselineFlush;  // const
        public static UnityEngine.TextCore.Text.TextAlignment BaselineGeoAligned;  // const
        public static UnityEngine.TextCore.Text.TextAlignment MidlineLeft;  // const
        public static UnityEngine.TextCore.Text.TextAlignment MidlineCenter;  // const
        public static UnityEngine.TextCore.Text.TextAlignment MidlineRight;  // const
        public static UnityEngine.TextCore.Text.TextAlignment MidlineJustified;  // const
        public static UnityEngine.TextCore.Text.TextAlignment MidlineFlush;  // const
        public static UnityEngine.TextCore.Text.TextAlignment MidlineGeoAligned;  // const
        public static UnityEngine.TextCore.Text.TextAlignment CaplineLeft;  // const
        public static UnityEngine.TextCore.Text.TextAlignment CaplineCenter;  // const
        public static UnityEngine.TextCore.Text.TextAlignment CaplineRight;  // const
        public static UnityEngine.TextCore.Text.TextAlignment CaplineJustified;  // const
        public static UnityEngine.TextCore.Text.TextAlignment CaplineFlush;  // const
        public static UnityEngine.TextCore.Text.TextAlignment CaplineGeoAligned;  // const

    }

    // TypeToken: 0x2000026  // size: 0x14
    public sealed struct FontStyles
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.TextCore.Text.FontStyles Normal;  // const
        public static UnityEngine.TextCore.Text.FontStyles Bold;  // const
        public static UnityEngine.TextCore.Text.FontStyles Italic;  // const
        public static UnityEngine.TextCore.Text.FontStyles Underline;  // const
        public static UnityEngine.TextCore.Text.FontStyles LowerCase;  // const
        public static UnityEngine.TextCore.Text.FontStyles UpperCase;  // const
        public static UnityEngine.TextCore.Text.FontStyles SmallCaps;  // const
        public static UnityEngine.TextCore.Text.FontStyles Strikethrough;  // const
        public static UnityEngine.TextCore.Text.FontStyles Superscript;  // const
        public static UnityEngine.TextCore.Text.FontStyles Subscript;  // const
        public static UnityEngine.TextCore.Text.FontStyles Highlight;  // const

    }

    // TypeToken: 0x2000027  // size: 0x24
    public sealed struct XmlTagAttribute
    {
        // Fields
        public System.Int32 nameHashCode;  // 0x10
        public UnityEngine.TextCore.Text.TagValueType valueType;  // 0x14
        public System.Int32 valueStartIndex;  // 0x18
        public System.Int32 valueLength;  // 0x1c
        public System.Int32 valueHashCode;  // 0x20

    }

    // TypeToken: 0x2000028  // size: 0x24
    public sealed struct RichTextTagAttribute
    {
        // Fields
        public System.Int32 nameHashCode;  // 0x10
        public System.Int32 valueHashCode;  // 0x14
        public UnityEngine.TextCore.Text.TagValueType valueType;  // 0x18
        public System.Int32 valueStartIndex;  // 0x1c
        public System.Int32 valueLength;  // 0x20

    }

    // TypeToken: 0x2000029  // size: 0x320
    public sealed struct WordWrapState
    {
        // Fields
        public System.Int32 previousWordBreak;  // 0x10
        public System.Int32 totalCharacterCount;  // 0x14
        public System.Int32 visibleCharacterCount;  // 0x18
        public System.Int32 visibleSpriteCount;  // 0x1c
        public System.Int32 visibleLinkCount;  // 0x20
        public System.Int32 firstCharacterIndex;  // 0x24
        public System.Int32 firstVisibleCharacterIndex;  // 0x28
        public System.Int32 lastCharacterIndex;  // 0x2c
        public System.Int32 lastVisibleCharIndex;  // 0x30
        public System.Int32 lineNumber;  // 0x34
        public System.Single maxCapHeight;  // 0x38
        public System.Single maxAscender;  // 0x3c
        public System.Single maxDescender;  // 0x40
        public System.Single maxLineAscender;  // 0x44
        public System.Single maxLineDescender;  // 0x48
        public System.Single previousLineAscender;  // 0x4c
        public System.Single xAdvance;  // 0x50
        public System.Single preferredWidth;  // 0x54
        public System.Single preferredHeight;  // 0x58
        public System.Single previousLineScale;  // 0x5c
        public System.Int32 wordCount;  // 0x60
        public UnityEngine.TextCore.Text.FontStyles fontStyle;  // 0x64
        public System.Single fontScale;  // 0x68
        public System.Single fontScaleMultiplier;  // 0x6c
        public System.Single currentFontSize;  // 0x70
        public System.Single baselineOffset;  // 0x74
        public System.Single lineOffset;  // 0x78
        public UnityEngine.TextCore.Text.TextInfo textInfo;  // 0x80
        public UnityEngine.TextCore.Text.LineInfo lineInfo;  // 0x88
        public UnityEngine.Color32 vertexColor;  // 0xe8
        public UnityEngine.Color32 underlineColor;  // 0xec
        public UnityEngine.Color32 strikethroughColor;  // 0xf0
        public UnityEngine.Color32 highlightColor;  // 0xf4
        public UnityEngine.TextCore.Text.FontStyleStack basicStyleStack;  // 0xf8
        public UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.Color32> colorStack;  // 0x108
        public UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.Color32> underlineColorStack;  // 0x128
        public UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.Color32> strikethroughColorStack;  // 0x148
        public UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.Color32> highlightColorStack;  // 0x168
        public UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.TextCore.Text.TextColorGradient> colorGradientStack;  // 0x188
        public UnityEngine.TextCore.Text.TextProcessingStack<System.Single> sizeStack;  // 0x1b0
        public UnityEngine.TextCore.Text.TextProcessingStack<System.Single> indentStack;  // 0x1d0
        public UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.TextCore.Text.TextFontWeight> fontWeightStack;  // 0x1f0
        public UnityEngine.TextCore.Text.TextProcessingStack<System.Int32> styleStack;  // 0x210
        public UnityEngine.TextCore.Text.TextProcessingStack<System.Single> baselineStack;  // 0x230
        public UnityEngine.TextCore.Text.TextProcessingStack<System.Int32> actionStack;  // 0x250
        public UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.TextCore.Text.MaterialReference> materialReferenceStack;  // 0x270
        public UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.TextCore.Text.TextAlignment> lineJustificationStack;  // 0x2c8
        public System.Int32 spriteAnimationId;  // 0x2e8
        public UnityEngine.TextCore.Text.FontAsset currentFontAsset;  // 0x2f0
        public UnityEngine.TextCore.Text.SpriteAsset currentSpriteAsset;  // 0x2f8
        public UnityEngine.Material currentMaterial;  // 0x300
        public System.Int32 currentMaterialIndex;  // 0x308
        public UnityEngine.TextCore.Text.Extents meshExtents;  // 0x30c
        public System.Boolean tagNoParsing;  // 0x31c
        public System.Boolean isNonBreakingSpace;  // 0x31d

    }

    // TypeToken: 0x200002A  // size: 0x10
    public static class TextGeneratorUtilities
    {
        // Fields
        public static readonly UnityEngine.Vector2 largePositiveVector2;  // static @ 0x0
        public static readonly UnityEngine.Vector2 largeNegativeVector2;  // static @ 0x8

        // Methods
        // RVA: 0x0B318C88  token: 0x6000103
        public static System.Boolean Approximately(System.Single a, System.Single b) { }
        // RVA: 0x0B4572C0  token: 0x6000104
        public static UnityEngine.Color32 HexCharsToColor(System.Char[] hexChars, System.Int32 tagCount) { }
        // RVA: 0x0B457920  token: 0x6000105
        public static UnityEngine.Color32 HexCharsToColor(System.Char[] hexChars, System.Int32 startIndex, System.Int32 length) { }
        // RVA: 0x0B457B88  token: 0x6000106
        public static System.Int32 HexToInt(System.Char hex) { }
        // RVA: 0x0B455CA4  token: 0x6000107
        public static System.Single ConvertToFloat(System.Char[] chars, System.Int32 startIndex, System.Int32 length) { }
        // RVA: 0x0B455B54  token: 0x6000108
        public static System.Single ConvertToFloat(System.Char[] chars, System.Int32 startIndex, System.Int32 length, System.Int32& lastIndex) { }
        // RVA: 0x04DBD680  token: 0x6000109
        public static UnityEngine.Vector2 PackUV(System.Single x, System.Single y, System.Single scale) { }
        // RVA: 0x0B458938  token: 0x600010A
        public static System.Void StringToCharArray(System.String sourceText, System.Int32[]& charBuffer, UnityEngine.TextCore.Text.TextProcessingStack<System.Int32>& styleStack, UnityEngine.TextCore.Text.TextGenerationSettings generationSettings) { }
        // RVA: -1  // generic def  token: 0x600010B
        private static System.Void ResizeInternalArray(T[]& array) { }
        // RVA: 0x0B457CC0  token: 0x600010C
        private static System.Boolean IsTagName(System.String& text, System.String tag, System.Int32 index) { }
        // RVA: 0x0B457D64  token: 0x600010D
        private static System.Boolean IsTagName(System.Int32[]& text, System.String tag, System.Int32 index) { }
        // RVA: 0x0B45816C  token: 0x600010E
        private static System.Boolean ReplaceOpeningStyleTag(System.Int32[]& sourceText, System.Int32 srcIndex, System.Int32& srcOffset, System.Int32[]& charBuffer, System.Int32& writeIndex, UnityEngine.TextCore.Text.TextProcessingStack<System.Int32>& styleStack, UnityEngine.TextCore.Text.TextGenerationSettings& generationSettings) { }
        // RVA: 0x0B458464  token: 0x600010F
        private static System.Boolean ReplaceOpeningStyleTag(System.String& sourceText, System.Int32 srcIndex, System.Int32& srcOffset, System.Int32[]& charBuffer, System.Int32& writeIndex, UnityEngine.TextCore.Text.TextProcessingStack<System.Int32>& styleStack, UnityEngine.TextCore.Text.TextGenerationSettings& generationSettings) { }
        // RVA: 0x0B457E9C  token: 0x6000110
        private static System.Void ReplaceClosingStyleTag(System.Int32[]& charBuffer, System.Int32& writeIndex, UnityEngine.TextCore.Text.TextProcessingStack<System.Int32>& styleStack, UnityEngine.TextCore.Text.TextGenerationSettings& generationSettings) { }
        // RVA: 0x0B456E94  token: 0x6000111
        private static UnityEngine.TextCore.Text.TextStyle GetStyle(UnityEngine.TextCore.Text.TextGenerationSettings generationSetting, System.Int32 hashCode) { }
        // RVA: 0x0B45716C  token: 0x6000112
        private static System.Int32 GetUtf32(System.String text, System.Int32 i) { }
        // RVA: 0x0B457094  token: 0x6000113
        private static System.Int32 GetUtf16(System.String text, System.Int32 i) { }
        // RVA: 0x0B45700C  token: 0x6000114
        private static System.Int32 GetTagHashCode(System.Int32[]& text, System.Int32 index, System.Int32& closeIndex) { }
        // RVA: 0x0B456F6C  token: 0x6000115
        private static System.Int32 GetTagHashCode(System.String& text, System.Int32 index, System.Int32& closeIndex) { }
        // RVA: 0x0B455D10  token: 0x6000116
        public static System.Void FillCharacterVertexBuffers(System.Int32 i, UnityEngine.TextCore.Text.TextGenerationSettings generationSettings, UnityEngine.TextCore.Text.TextInfo textInfo) { }
        // RVA: 0x0B456618  token: 0x6000117
        public static System.Void FillSpriteVertexBuffers(System.Int32 i, UnityEngine.TextCore.Text.TextGenerationSettings generationSettings, UnityEngine.TextCore.Text.TextInfo textInfo) { }
        // RVA: 0x0B455730  token: 0x6000118
        public static System.Void AdjustLineOffset(System.Int32 startIndex, System.Int32 endIndex, System.Single offset, UnityEngine.TextCore.Text.TextInfo textInfo) { }
        // RVA: 0x0B458760  token: 0x6000119
        public static System.Void ResizeLineExtents(System.Int32 size, UnityEngine.TextCore.Text.TextInfo textInfo) { }
        // RVA: 0x0B457E78  token: 0x600011A
        public static UnityEngine.TextCore.Text.FontStyles LegacyStyleToNewStyle(UnityEngine.FontStyle fontStyle) { }
        // RVA: 0x0B457E14  token: 0x600011B
        public static UnityEngine.TextCore.Text.TextAlignment LegacyAlignmentToNewAlignment(UnityEngine.TextAnchor anchor) { }
        // RVA: 0x0B459154  token: 0x600011C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200002B  // size: 0x24
    public sealed struct PageInfo
    {
        // Fields
        public System.Int32 firstCharacterIndex;  // 0x10
        public System.Int32 lastCharacterIndex;  // 0x14
        public System.Single ascender;  // 0x18
        public System.Single baseLine;  // 0x1c
        public System.Single descender;  // 0x20

    }

    // TypeToken: 0x200002C  // size: 0x1C
    public sealed struct WordInfo
    {
        // Fields
        public System.Int32 firstCharacterIndex;  // 0x10
        public System.Int32 lastCharacterIndex;  // 0x14
        public System.Int32 characterCount;  // 0x18

    }

    // TypeToken: 0x200002D  // size: 0x68
    public class TextInfo
    {
        // Fields
        private static UnityEngine.Vector2 s_InfinityVectorPositive;  // static @ 0x0
        private static UnityEngine.Vector2 s_InfinityVectorNegative;  // static @ 0x8
        public System.Int32 characterCount;  // 0x10
        public System.Int32 spriteCount;  // 0x14
        public System.Int32 spaceCount;  // 0x18
        public System.Int32 wordCount;  // 0x1c
        public System.Int32 linkCount;  // 0x20
        public System.Int32 lineCount;  // 0x24
        public System.Int32 pageCount;  // 0x28
        public System.Int32 materialCount;  // 0x2c
        public UnityEngine.TextCore.Text.TextElementInfo[] textElementInfo;  // 0x30
        public UnityEngine.TextCore.Text.WordInfo[] wordInfo;  // 0x38
        public UnityEngine.TextCore.Text.LinkInfo[] linkInfo;  // 0x40
        public UnityEngine.TextCore.Text.LineInfo[] lineInfo;  // 0x48
        public UnityEngine.TextCore.Text.PageInfo[] pageInfo;  // 0x50
        public UnityEngine.TextCore.Text.MeshInfo[] meshInfo;  // 0x58
        public System.Boolean isDirty;  // 0x60

        // Methods
        // RVA: 0x0B459884  token: 0x600011D
        public System.Void .ctor() { }
        // RVA: 0x0B4597CC  token: 0x600011E
        private System.Void Clear() { }
        // RVA: 0x0B45973C  token: 0x600011F
        private System.Void ClearMeshInfo(System.Boolean updateMesh) { }
        // RVA: 0x0B4594FC  token: 0x6000120
        private System.Void ClearLineInfo() { }
        // RVA: -1  // generic def  token: 0x6000121
        private static System.Void Resize(T[]& array, System.Int32 size) { }
        // RVA: -1  // generic def  token: 0x6000122
        private static System.Void Resize(T[]& array, System.Int32 size, System.Boolean isBlockAllocated) { }
        // RVA: 0x0B459828  token: 0x6000123
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200002E  // size: 0x14
    public sealed struct MarkupTag
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.TextCore.Text.MarkupTag BOLD;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_BOLD;  // const
        public static UnityEngine.TextCore.Text.MarkupTag ITALIC;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_ITALIC;  // const
        public static UnityEngine.TextCore.Text.MarkupTag UNDERLINE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_UNDERLINE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag STRIKETHROUGH;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_STRIKETHROUGH;  // const
        public static UnityEngine.TextCore.Text.MarkupTag MARK;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_MARK;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SUBSCRIPT;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_SUBSCRIPT;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SUPERSCRIPT;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_SUPERSCRIPT;  // const
        public static UnityEngine.TextCore.Text.MarkupTag COLOR;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_COLOR;  // const
        public static UnityEngine.TextCore.Text.MarkupTag ALPHA;  // const
        public static UnityEngine.TextCore.Text.MarkupTag A;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_A;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SIZE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_SIZE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SPRITE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag NO_BREAK;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_NO_BREAK;  // const
        public static UnityEngine.TextCore.Text.MarkupTag STYLE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_STYLE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag FONT;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_FONT;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_MATERIAL;  // const
        public static UnityEngine.TextCore.Text.MarkupTag LINK;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_LINK;  // const
        public static UnityEngine.TextCore.Text.MarkupTag FONT_WEIGHT;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_FONT_WEIGHT;  // const
        public static UnityEngine.TextCore.Text.MarkupTag NO_PARSE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_NO_PARSE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag POSITION;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_POSITION;  // const
        public static UnityEngine.TextCore.Text.MarkupTag VERTICAL_OFFSET;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_VERTICAL_OFFSET;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SPACE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_SPACE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag PAGE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_PAGE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag ALIGN;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_ALIGN;  // const
        public static UnityEngine.TextCore.Text.MarkupTag WIDTH;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_WIDTH;  // const
        public static UnityEngine.TextCore.Text.MarkupTag GRADIENT;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_GRADIENT;  // const
        public static UnityEngine.TextCore.Text.MarkupTag CHARACTER_SPACE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_CHARACTER_SPACE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag MONOSPACE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_MONOSPACE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag CLASS;  // const
        public static UnityEngine.TextCore.Text.MarkupTag INDENT;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_INDENT;  // const
        public static UnityEngine.TextCore.Text.MarkupTag LINE_INDENT;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_LINE_INDENT;  // const
        public static UnityEngine.TextCore.Text.MarkupTag MARGIN;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_MARGIN;  // const
        public static UnityEngine.TextCore.Text.MarkupTag MARGIN_LEFT;  // const
        public static UnityEngine.TextCore.Text.MarkupTag MARGIN_RIGHT;  // const
        public static UnityEngine.TextCore.Text.MarkupTag LINE_HEIGHT;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_LINE_HEIGHT;  // const
        public static UnityEngine.TextCore.Text.MarkupTag ACTION;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_ACTION;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SCALE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_SCALE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag ROTATE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_ROTATE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag LOWERCASE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_LOWERCASE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag ALLCAPS;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_ALLCAPS;  // const
        public static UnityEngine.TextCore.Text.MarkupTag UPPERCASE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_UPPERCASE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SMALLCAPS;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_SMALLCAPS;  // const
        public static UnityEngine.TextCore.Text.MarkupTag LIGA;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_LIGA;  // const
        public static UnityEngine.TextCore.Text.MarkupTag FRAC;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SLASH_FRAC;  // const
        public static UnityEngine.TextCore.Text.MarkupTag NAME;  // const
        public static UnityEngine.TextCore.Text.MarkupTag INDEX;  // const
        public static UnityEngine.TextCore.Text.MarkupTag TINT;  // const
        public static UnityEngine.TextCore.Text.MarkupTag ANIM;  // const
        public static UnityEngine.TextCore.Text.MarkupTag MATERIAL;  // const
        public static UnityEngine.TextCore.Text.MarkupTag HREF;  // const
        public static UnityEngine.TextCore.Text.MarkupTag ANGLE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag RED;  // const
        public static UnityEngine.TextCore.Text.MarkupTag GREEN;  // const
        public static UnityEngine.TextCore.Text.MarkupTag BLUE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag YELLOW;  // const
        public static UnityEngine.TextCore.Text.MarkupTag ORANGE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag BLACK;  // const
        public static UnityEngine.TextCore.Text.MarkupTag WHITE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag PURPLE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag BR;  // const
        public static UnityEngine.TextCore.Text.MarkupTag ZWSP;  // const
        public static UnityEngine.TextCore.Text.MarkupTag NBSP;  // const
        public static UnityEngine.TextCore.Text.MarkupTag SHY;  // const
        public static UnityEngine.TextCore.Text.MarkupTag LEFT;  // const
        public static UnityEngine.TextCore.Text.MarkupTag RIGHT;  // const
        public static UnityEngine.TextCore.Text.MarkupTag CENTER;  // const
        public static UnityEngine.TextCore.Text.MarkupTag JUSTIFIED;  // const
        public static UnityEngine.TextCore.Text.MarkupTag FLUSH;  // const
        public static UnityEngine.TextCore.Text.MarkupTag NONE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag PLUS;  // const
        public static UnityEngine.TextCore.Text.MarkupTag MINUS;  // const
        public static UnityEngine.TextCore.Text.MarkupTag PX;  // const
        public static UnityEngine.TextCore.Text.MarkupTag PLUS_PX;  // const
        public static UnityEngine.TextCore.Text.MarkupTag MINUS_PX;  // const
        public static UnityEngine.TextCore.Text.MarkupTag EM;  // const
        public static UnityEngine.TextCore.Text.MarkupTag PLUS_EM;  // const
        public static UnityEngine.TextCore.Text.MarkupTag MINUS_EM;  // const
        public static UnityEngine.TextCore.Text.MarkupTag PCT;  // const
        public static UnityEngine.TextCore.Text.MarkupTag PLUS_PCT;  // const
        public static UnityEngine.TextCore.Text.MarkupTag MINUS_PCT;  // const
        public static UnityEngine.TextCore.Text.MarkupTag PERCENTAGE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag PLUS_PERCENTAGE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag MINUS_PERCENTAGE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag TRUE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag FALSE;  // const
        public static UnityEngine.TextCore.Text.MarkupTag INVALID;  // const
        public static UnityEngine.TextCore.Text.MarkupTag NORMAL;  // const
        public static UnityEngine.TextCore.Text.MarkupTag DEFAULT;  // const

    }

    // TypeToken: 0x200002F  // size: 0x14
    public sealed struct TagValueType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.TextCore.Text.TagValueType None;  // const
        public static UnityEngine.TextCore.Text.TagValueType NumericalValue;  // const
        public static UnityEngine.TextCore.Text.TagValueType StringValue;  // const
        public static UnityEngine.TextCore.Text.TagValueType ColorValue;  // const

    }

    // TypeToken: 0x2000030  // size: 0x14
    public sealed struct TagUnitType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.TextCore.Text.TagUnitType Pixels;  // const
        public static UnityEngine.TextCore.Text.TagUnitType FontUnits;  // const
        public static UnityEngine.TextCore.Text.TagUnitType Percentage;  // const

    }

    // TypeToken: 0x2000031  // size: 0x1A
    public sealed struct FontStyleStack
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
        // RVA: 0x0333C3A0  token: 0x6000124
        public System.Void Clear() { }
        // RVA: 0x0B45930C  token: 0x6000125
        public System.Byte Add(UnityEngine.TextCore.Text.FontStyles style) { }
        // RVA: 0x0B4593D8  token: 0x6000126
        public System.Byte Remove(UnityEngine.TextCore.Text.FontStyles style) { }

    }

    // TypeToken: 0x2000032
    public sealed struct TextProcessingStack`1
    {
        // Fields
        public T[] itemStack;  // 0x0
        public System.Int32 index;  // 0x0
        private T m_DefaultItem;  // 0x0
        private System.Int32 m_Capacity;  // 0x0
        private System.Int32 m_RolloverSize;  // 0x0
        private System.Int32 m_Count;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000127
        public System.Void .ctor(T[] stack) { }
        // RVA: -1  // not resolved  token: 0x6000128
        public System.Void .ctor(System.Int32 capacity) { }
        // RVA: -1  // not resolved  token: 0x6000129
        public System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x600012A
        public System.Void SetDefault(T item) { }
        // RVA: -1  // not resolved  token: 0x600012B
        public System.Void Add(T item) { }
        // RVA: -1  // not resolved  token: 0x600012C
        public T Remove() { }
        // RVA: -1  // not resolved  token: 0x600012D
        public System.Void Push(T item) { }
        // RVA: -1  // not resolved  token: 0x600012E
        public T Pop() { }
        // RVA: -1  // not resolved  token: 0x600012F
        public T Peek() { }
        // RVA: -1  // not resolved  token: 0x6000130
        public T CurrentItem() { }

    }

    // TypeToken: 0x2000033  // size: 0x10
    public class TextResourceManager
    {
        // Fields
        private static readonly System.Collections.Generic.Dictionary<System.Int32,UnityEngine.TextCore.Text.TextResourceManager.FontAssetRef> s_FontAssetReferences;  // static @ 0x0
        private static readonly System.Collections.Generic.Dictionary<System.Int32,UnityEngine.TextCore.Text.FontAsset> s_FontAssetNameReferenceLookup;  // static @ 0x8
        private static readonly System.Collections.Generic.Dictionary<System.Int64,UnityEngine.TextCore.Text.FontAsset> s_FontAssetFamilyNameAndStyleReferenceLookup;  // static @ 0x10
        private static readonly System.Collections.Generic.List<System.Int32> s_FontAssetRemovalList;  // static @ 0x18
        private static readonly System.Int32 k_RegularStyleHashCode;  // static @ 0x20

        // Methods
        // RVA: 0x0B4599AC  token: 0x6000131
        private static System.Void AddFontAsset(UnityEngine.TextCore.Text.FontAsset fontAsset) { }
        // RVA: 0x0B459F1C  token: 0x6000132
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000035  // size: 0xA0
    public class TextSettings : UnityEngine.ScriptableObject
    {
        // Fields
        protected System.String m_Version;  // 0x18
        protected UnityEngine.TextCore.Text.FontAsset m_DefaultFontAsset;  // 0x20
        protected System.String m_DefaultFontAssetPath;  // 0x28
        protected System.Collections.Generic.List<UnityEngine.TextCore.Text.FontAsset> m_FallbackFontAssets;  // 0x30
        protected System.Boolean m_MatchMaterialPreset;  // 0x38
        protected System.Int32 m_MissingCharacterUnicode;  // 0x3c
        protected System.Boolean m_ClearDynamicDataOnBuild;  // 0x40
        protected UnityEngine.TextCore.Text.SpriteAsset m_DefaultSpriteAsset;  // 0x48
        protected System.String m_DefaultSpriteAssetPath;  // 0x50
        protected System.Collections.Generic.List<UnityEngine.TextCore.Text.SpriteAsset> m_FallbackSpriteAssets;  // 0x58
        protected System.UInt32 m_MissingSpriteCharacterUnicode;  // 0x60
        protected UnityEngine.TextCore.Text.TextStyleSheet m_DefaultStyleSheet;  // 0x68
        protected System.String m_StyleSheetsResourcePath;  // 0x70
        protected System.String m_DefaultColorGradientPresetsPath;  // 0x78
        protected UnityEngine.TextCore.Text.UnicodeLineBreakingRules m_UnicodeLineBreakingRules;  // 0x80
        protected System.Boolean m_DisplayWarnings;  // 0x88
        private System.Collections.Generic.Dictionary<System.Int32,UnityEngine.TextCore.Text.FontAsset> m_FontLookup;  // 0x90
        private System.Collections.Generic.List<UnityEngine.TextCore.Text.TextSettings.FontReferenceMap> m_FontReferences;  // 0x98

        // Properties
        System.String version { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        UnityEngine.TextCore.Text.FontAsset defaultFontAsset { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        System.String defaultFontAssetPath { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }
        System.Collections.Generic.List<UnityEngine.TextCore.Text.FontAsset> fallbackFontAssets { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }
        System.Boolean matchMaterialPreset { get; /* RVA: 0x011F2A70 */ set; /* RVA: 0x011F2A80 */ }
        System.Int32 missingCharacterUnicode { get; /* RVA: 0x04D86640 */ set; /* RVA: 0x04D86670 */ }
        System.Boolean clearDynamicDataOnBuild { get; /* RVA: 0x02D480A0 */ set; /* RVA: 0x02D480B0 */ }
        UnityEngine.TextCore.Text.SpriteAsset defaultSpriteAsset { get; /* RVA: 0x04D86200 */ set; /* RVA: 0x035A41A0 */ }
        System.String defaultSpriteAssetPath { get; /* RVA: 0x04D86270 */ set; /* RVA: 0x02FDAEA0 */ }
        System.Collections.Generic.List<UnityEngine.TextCore.Text.SpriteAsset> fallbackSpriteAssets { get; /* RVA: 0x02E56440 */ set; /* RVA: 0x036E5590 */ }
        System.UInt32 missingSpriteCharacterUnicode { get; /* RVA: 0x04D86790 */ set; /* RVA: 0x04D86D50 */ }
        UnityEngine.TextCore.Text.TextStyleSheet defaultStyleSheet { get; /* RVA: 0x04D86230 */ set; /* RVA: 0x0485F170 */ }
        System.String styleSheetsResourcePath { get; /* RVA: 0x04D862B0 */ set; /* RVA: 0x063AD760 */ }
        System.String defaultColorGradientPresetsPath { get; /* RVA: 0x04D862A0 */ set; /* RVA: 0x06402320 */ }
        UnityEngine.TextCore.Text.UnicodeLineBreakingRules lineBreakingRules { get; /* RVA: 0x0B45A71C */ set; /* RVA: 0x04CD9970 */ }
        System.Boolean displayWarnings { get; /* RVA: 0x011F2750 */ set; /* RVA: 0x011F2760 */ }

        // Methods
        // RVA: 0x0B45A414  token: 0x6000154
        protected System.Void InitializeFontReferenceLookup() { }
        // RVA: 0x0B45A114  token: 0x6000155
        protected UnityEngine.TextCore.Text.FontAsset GetCachedFontAssetInternal(UnityEngine.Font font) { }
        // RVA: 0x0B45A60C  token: 0x6000156
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000037  // size: 0x10
    public static class TextShaderUtilities
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
        public static System.Int32 ID_WeightNormal;  // static @ 0x28
        public static System.Int32 ID_WeightBold;  // static @ 0x2c
        public static System.Int32 ID_OutlineTex;  // static @ 0x30
        public static System.Int32 ID_OutlineWidth;  // static @ 0x34
        public static System.Int32 ID_OutlineSoftness;  // static @ 0x38
        public static System.Int32 ID_OutlineColor;  // static @ 0x3c
        public static System.Int32 ID_Outline2Color;  // static @ 0x40
        public static System.Int32 ID_Outline2Width;  // static @ 0x44
        public static System.Int32 ID_Padding;  // static @ 0x48
        public static System.Int32 ID_GradientScale;  // static @ 0x4c
        public static System.Int32 ID_ScaleX;  // static @ 0x50
        public static System.Int32 ID_ScaleY;  // static @ 0x54
        public static System.Int32 ID_PerspectiveFilter;  // static @ 0x58
        public static System.Int32 ID_Sharpness;  // static @ 0x5c
        public static System.Int32 ID_TextureWidth;  // static @ 0x60
        public static System.Int32 ID_TextureHeight;  // static @ 0x64
        public static System.Int32 ID_BevelAmount;  // static @ 0x68
        public static System.Int32 ID_GlowColor;  // static @ 0x6c
        public static System.Int32 ID_GlowOffset;  // static @ 0x70
        public static System.Int32 ID_GlowPower;  // static @ 0x74
        public static System.Int32 ID_GlowOuter;  // static @ 0x78
        public static System.Int32 ID_GlowInner;  // static @ 0x7c
        public static System.Int32 ID_LightAngle;  // static @ 0x80
        public static System.Int32 ID_EnvMap;  // static @ 0x84
        public static System.Int32 ID_EnvMatrix;  // static @ 0x88
        public static System.Int32 ID_EnvMatrixRotation;  // static @ 0x8c
        public static System.Int32 ID_MaskCoord;  // static @ 0x90
        public static System.Int32 ID_ClipRect;  // static @ 0x94
        public static System.Int32 ID_MaskSoftnessX;  // static @ 0x98
        public static System.Int32 ID_MaskSoftnessY;  // static @ 0x9c
        public static System.Int32 ID_VertexOffsetX;  // static @ 0xa0
        public static System.Int32 ID_VertexOffsetY;  // static @ 0xa4
        public static System.Int32 ID_UseClipRect;  // static @ 0xa8
        public static System.Int32 ID_StencilID;  // static @ 0xac
        public static System.Int32 ID_StencilOp;  // static @ 0xb0
        public static System.Int32 ID_StencilComp;  // static @ 0xb4
        public static System.Int32 ID_StencilReadMask;  // static @ 0xb8
        public static System.Int32 ID_StencilWriteMask;  // static @ 0xbc
        public static System.Int32 ID_ShaderFlags;  // static @ 0xc0
        public static System.Int32 ID_ScaleRatio_A;  // static @ 0xc4
        public static System.Int32 ID_ScaleRatio_B;  // static @ 0xc8
        public static System.Int32 ID_ScaleRatio_C;  // static @ 0xcc
        public static System.String Keyword_Bevel;  // static @ 0xd0
        public static System.String Keyword_Glow;  // static @ 0xd8
        public static System.String Keyword_Underlay;  // static @ 0xe0
        public static System.String Keyword_Ratios;  // static @ 0xe8
        public static System.String Keyword_MASK_SOFT;  // static @ 0xf0
        public static System.String Keyword_MASK_HARD;  // static @ 0xf8
        public static System.String Keyword_MASK_TEX;  // static @ 0x100
        public static System.String Keyword_Outline;  // static @ 0x108
        public static System.String ShaderTag_ZTestMode;  // static @ 0x110
        public static System.String ShaderTag_CullMode;  // static @ 0x118
        private static System.Single m_clamp;  // static @ 0x120
        public static System.Boolean isInitialized;  // static @ 0x124
        private static UnityEngine.Shader k_ShaderRef_MobileSDF;  // static @ 0x128
        private static UnityEngine.Shader k_ShaderRef_MobileBitmap;  // static @ 0x130

        // Properties
        UnityEngine.Shader ShaderRef_MobileSDF { get; /* RVA: 0x0B45C3C8 */ }
        UnityEngine.Shader ShaderRef_MobileBitmap { get; /* RVA: 0x0B45C24C */ }

        // Methods
        // RVA: 0x0B45BF5C  token: 0x600015A
        private static System.Void .cctor() { }
        // RVA: 0x0B45AF50  token: 0x600015B
        private static System.Void GetShaderPropertyIDs() { }
        // RVA: 0x0B45BA3C  token: 0x600015C
        private static System.Void UpdateShaderRatios(UnityEngine.Material mat) { }
        // RVA: 0x0B45B8B4  token: 0x600015D
        private static System.Boolean IsMaskingEnabled(UnityEngine.Material material) { }
        // RVA: 0x0B45A798  token: 0x600015E
        private static System.Single GetPadding(UnityEngine.Material material, System.Boolean enableExtraPadding, System.Boolean isBold) { }

    }

    // TypeToken: 0x2000038  // size: 0x50
    public class TextStyle
    {
        // Fields
        private static UnityEngine.TextCore.Text.TextStyle k_NormalStyle;  // static @ 0x0
        private System.String m_Name;  // 0x10
        private System.Int32 m_HashCode;  // 0x18
        private System.String m_OpeningDefinition;  // 0x20
        private System.String m_ClosingDefinition;  // 0x28
        private System.Int32[] m_OpeningTagArray;  // 0x30
        private System.Int32[] m_ClosingTagArray;  // 0x38
        private System.UInt32[] m_OpeningTagUnicodeArray;  // 0x40
        private System.UInt32[] m_ClosingTagUnicodeArray;  // 0x48

        // Properties
        System.Int32 hashCode { get; /* RVA: 0x04D9D860 */ }
        System.Int32[] styleOpeningTagArray { get; /* RVA: 0x04DBBEB0 */ }
        System.Int32[] styleClosingTagArray { get; /* RVA: 0x04DBC0B0 */ }

        // Methods
        // RVA: 0x0B45C888  token: 0x6000162
        public System.Void RefreshStyle() { }

    }

    // TypeToken: 0x2000039  // size: 0x28
    public class TextStyleSheet : UnityEngine.ScriptableObject
    {
        // Fields
        private System.Collections.Generic.List<UnityEngine.TextCore.Text.TextStyle> m_StyleList;  // 0x18
        private System.Collections.Generic.Dictionary<System.Int32,UnityEngine.TextCore.Text.TextStyle> m_StyleLookupDictionary;  // 0x20

        // Properties
        System.Collections.Generic.List<UnityEngine.TextCore.Text.TextStyle> styles { get; /* RVA: 0x04D96330 */ }

        // Methods
        // RVA: 0x0B45C544  token: 0x6000164
        public UnityEngine.TextCore.Text.TextStyle GetStyle(System.Int32 hashCode) { }
        // RVA: 0x0B45C5C0  token: 0x6000165
        public UnityEngine.TextCore.Text.TextStyle GetStyle(System.String name) { }
        // RVA: 0x0B45C7FC  token: 0x6000166
        public System.Void RefreshStyles() { }
        // RVA: 0x0B45C644  token: 0x6000167
        private System.Void LoadStyleDictionaryInternal() { }
        // RVA: 0x0B45C804  token: 0x6000168
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200003A  // size: 0x10
    public static class TextUtilities
    {
        // Methods
        // RVA: 0x0B45CB28  token: 0x6000169
        private static System.Char ToUpperFast(System.Char c) { }
        // RVA: 0x0B45CAD0  token: 0x600016A
        private static System.UInt32 ToUpperASCIIFast(System.UInt32 c) { }
        // RVA: 0x0B45CA74  token: 0x600016B
        public static System.Int32 GetHashCodeCaseInSensitive(System.String s) { }
        // RVA: 0x0B45CB80  token: 0x600016C
        private static System.String UintToString(System.Collections.Generic.List<System.UInt32> unicodes) { }

    }

    // TypeToken: 0x200003B  // size: 0x30
    public class UnicodeLineBreakingRules
    {
        // Fields
        private static UnityEngine.TextCore.Text.UnicodeLineBreakingRules s_Instance;  // static @ 0x0
        private UnityEngine.TextAsset m_UnicodeLineBreakingRules;  // 0x10
        private UnityEngine.TextAsset m_LeadingCharacters;  // 0x18
        private UnityEngine.TextAsset m_FollowingCharacters;  // 0x20
        private System.Boolean m_UseModernHangulLineBreakingRules;  // 0x28
        private static System.Collections.Generic.HashSet<System.UInt32> s_LeadingCharactersLookup;  // static @ 0x8
        private static System.Collections.Generic.HashSet<System.UInt32> s_FollowingCharactersLookup;  // static @ 0x10

        // Properties
        System.Collections.Generic.HashSet<System.UInt32> leadingCharactersLookup { get; /* RVA: 0x0B45D114 */ }
        System.Collections.Generic.HashSet<System.UInt32> followingCharactersLookup { get; /* RVA: 0x0B45D0A4 */ }

        // Methods
        // RVA: 0x0B45CCF4  token: 0x600016F
        private static System.Void LoadLineBreakingRules() { }
        // RVA: 0x0B45CC30  token: 0x6000170
        private static System.Collections.Generic.HashSet<System.UInt32> GetCharacters(UnityEngine.TextAsset file) { }
        // RVA: 0x041E1670  token: 0x6000171
        public System.Void .ctor() { }
        // RVA: 0x0B45D040  token: 0x6000172
        private static System.Void .cctor() { }

    }

}

