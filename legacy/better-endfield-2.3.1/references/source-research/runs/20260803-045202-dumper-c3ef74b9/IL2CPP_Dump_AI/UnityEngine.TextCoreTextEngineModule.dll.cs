// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.TextCoreTextEngineModule.dll
// Classes:  59
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

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000009
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.TextCore.Text.FontAsset.<>c<>9  // static @ 0x0
  public    static  System.Func<UnityEngine.TextCore.Text.Character,System.UInt32><>9__144_0  // static @ 0x8
  public    static  System.Func<UnityEngine.TextCore.Glyph,System.UInt32><>9__145_0  // static @ 0x10
METHODS:
  RVA=0x0B459278  token=0x6000078  System.Void .cctor()
  RVA=0x041E1670  token=0x6000079  System.Void .ctor()
  RVA=0x0B319DF0  token=0x600007A  System.UInt32 <SortCharacterTable>b__144_0(UnityEngine.TextCore.Text.Character c)
  RVA=0x0AA421AC  token=0x600007B  System.UInt32 <SortGlyphTable>b__145_0(UnityEngine.TextCore.Glyph c)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200000C
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.TextCore.Text.FontFeatureTable.<>c<>9  // static @ 0x0
  public    static  System.Func<UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord,System.UInt32><>9__6_0  // static @ 0x8
  public    static  System.Func<UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord,System.UInt32><>9__6_1  // static @ 0x10
METHODS:
  RVA=0x0B4591B0  token=0x6000084  System.Void .cctor()
  RVA=0x041E1670  token=0x6000085  System.Void .ctor()
  RVA=0x04DBD6C0  token=0x6000086  System.UInt32 <SortGlyphPairAdjustmentRecords>b__6_0(UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord s)
  RVA=0x04DBD6D0  token=0x6000087  System.UInt32 <SortGlyphPairAdjustmentRecords>b__6_1(UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord s)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000016
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.TextCore.Text.SpriteAsset.<>c<>9  // static @ 0x0
  public    static  System.Func<UnityEngine.TextCore.Text.SpriteGlyph,System.UInt32><>9__37_0  // static @ 0x8
  public    static  System.Func<UnityEngine.TextCore.Text.SpriteCharacter,System.UInt32><>9__38_0  // static @ 0x10
METHODS:
  RVA=0x0B459214  token=0x60000C2  System.Void .cctor()
  RVA=0x041E1670  token=0x60000C3  System.Void .ctor()
  RVA=0x0AA421AC  token=0x60000C4  System.UInt32 <SortGlyphTable>b__37_0(UnityEngine.TextCore.Text.SpriteGlyph item)
  RVA=0x0B319DF0  token=0x60000C5  System.UInt32 <SortCharacterTable>b__38_0(UnityEngine.TextCore.Text.SpriteCharacter c)
END_CLASS

CLASS: SpecialCharacter
TYPE:  sealed struct
TOKEN: 0x2000024
SIZE:  0x30
FIELDS:
  public            UnityEngine.TextCore.Text.Charactercharacter  // 0x10
  public            UnityEngine.TextCore.Text.FontAssetfontAsset  // 0x18
  public            UnityEngine.Material            material  // 0x20
  public            System.Int32                    materialIndex  // 0x28
METHODS:
  RVA=0x0B4523E0  token=0x6000102  System.Void .ctor(UnityEngine.TextCore.Text.Character character, System.Int32 materialIndex)
END_CLASS

CLASS: FontAssetRef
TYPE:  sealed struct
TOKEN: 0x2000034
SIZE:  0x30
FIELDS:
  public            System.Int32                    nameHashCode  // 0x10
  public            System.Int32                    familyNameHashCode  // 0x14
  public            System.Int32                    styleNameHashCode  // 0x18
  public            System.Int64                    familyNameAndStyleHashCode  // 0x20
  public    readonly UnityEngine.TextCore.Text.FontAssetfontAsset  // 0x28
METHODS:
  RVA=0x0B4592DC  token=0x6000133  System.Void .ctor(System.Int32 nameHashCode, System.Int32 familyNameHashCode, System.Int32 styleNameHashCode, UnityEngine.TextCore.Text.FontAsset fontAsset)
END_CLASS

CLASS: FontReferenceMap
TYPE:  sealed struct
TOKEN: 0x2000036
SIZE:  0x20
FIELDS:
  public            UnityEngine.Font                font  // 0x10
  public            UnityEngine.TextCore.Text.FontAssetfontAsset  // 0x18
METHODS:
  RVA=0x039CF630  token=0x6000157  System.Void .ctor(UnityEngine.Font font, UnityEngine.TextCore.Text.FontAsset fontAsset)
END_CLASS

CLASS: UnityEngine.TextCore.Text.Character
TYPE:  class
TOKEN: 0x2000002
SIZE:  0x30
EXTENDS: UnityEngine.TextCore.Text.TextElement
FIELDS:
METHODS:
  RVA=0x04DB9B40  token=0x6000001  System.Void .ctor()
  RVA=0x0B448900  token=0x6000002  System.Void .ctor(System.UInt32 unicode, UnityEngine.TextCore.Text.FontAsset fontAsset, UnityEngine.TextCore.Glyph glyph)
  RVA=0x0B30C9E4  token=0x6000003  System.Void .ctor(System.UInt32 unicode, System.UInt32 glyphIndex)
END_CLASS

CLASS: UnityEngine.TextCore.Text.ColorUtilities
TYPE:  static class
TOKEN: 0x2000003
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B448958  token=0x6000004  System.Boolean CompareColors(UnityEngine.Color32 a, UnityEngine.Color32 b)
  RVA=0x0B448990  token=0x6000005  UnityEngine.Color32 MultiplyColors(UnityEngine.Color32 c1, UnityEngine.Color32 c2)
END_CLASS

CLASS: UnityEngine.TextCore.Text.TextFontWeight
TYPE:  sealed struct
TOKEN: 0x2000004
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.TextCore.Text.TextFontWeightThin  // const
  public    static  UnityEngine.TextCore.Text.TextFontWeightExtraLight  // const
  public    static  UnityEngine.TextCore.Text.TextFontWeightLight  // const
  public    static  UnityEngine.TextCore.Text.TextFontWeightRegular  // const
  public    static  UnityEngine.TextCore.Text.TextFontWeightMedium  // const
  public    static  UnityEngine.TextCore.Text.TextFontWeightSemiBold  // const
  public    static  UnityEngine.TextCore.Text.TextFontWeightBold  // const
  public    static  UnityEngine.TextCore.Text.TextFontWeightHeavy  // const
  public    static  UnityEngine.TextCore.Text.TextFontWeightBlack  // const
METHODS:
END_CLASS

CLASS: UnityEngine.TextCore.Text.FontWeightPair
TYPE:  sealed struct
TOKEN: 0x2000005
SIZE:  0x20
FIELDS:
  public            UnityEngine.TextCore.Text.FontAssetregularTypeface  // 0x10
  public            UnityEngine.TextCore.Text.FontAssetitalicTypeface  // 0x18
METHODS:
END_CLASS

CLASS: UnityEngine.TextCore.Text.FontAssetCreationEditorSettings
TYPE:  sealed struct
TOKEN: 0x2000006
SIZE:  0x60
FIELDS:
  public            System.String                   sourceFontFileGUID  // 0x10
  public            System.Int32                    faceIndex  // 0x18
  public            System.Int32                    pointSizeSamplingMode  // 0x1c
  public            System.Int32                    pointSize  // 0x20
  public            System.Int32                    padding  // 0x24
  public            System.Int32                    packingMode  // 0x28
  public            System.Int32                    atlasWidth  // 0x2c
  public            System.Int32                    atlasHeight  // 0x30
  public            System.Int32                    characterSetSelectionMode  // 0x34
  public            System.String                   characterSequence  // 0x38
  public            System.String                   referencedFontAssetGUID  // 0x40
  public            System.String                   referencedTextAssetGUID  // 0x48
  public            System.Int32                    fontStyle  // 0x50
  public            System.Single                   fontStyleModifier  // 0x54
  public            System.Int32                    renderMode  // 0x58
  public            System.Boolean                  includeFontFeatures  // 0x5c
METHODS:
END_CLASS

CLASS: UnityEngine.TextCore.Text.AtlasPopulationMode
TYPE:  sealed struct
TOKEN: 0x2000007
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.TextCore.Text.AtlasPopulationModeStatic  // const
  public    static  UnityEngine.TextCore.Text.AtlasPopulationModeDynamic  // const
  public    static  UnityEngine.TextCore.Text.AtlasPopulationModeDynamicOS  // const
METHODS:
END_CLASS

CLASS: UnityEngine.TextCore.Text.FontAsset
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x1E0
EXTENDS: UnityEngine.TextCore.Text.TextAsset
FIELDS:
  private           System.String                   m_SourceFontFileGUID  // 0x38
  private           UnityEngine.Font                m_SourceFontFile  // 0x40
  private           UnityEngine.TextCore.Text.AtlasPopulationModem_AtlasPopulationMode  // 0x48
  private           System.Boolean                  InternalDynamicOS  // 0x4c
  private           UnityEngine.TextCore.FaceInfo   m_FaceInfo  // 0x50
  private           System.Int32                    m_FamilyNameHashCode  // 0xb0
  private           System.Int32                    m_StyleNameHashCode  // 0xb4
  private           UnityEngine.TextCore.Text.FontWeightPair[]m_FontWeightTable  // 0xb8
  private           System.Collections.Generic.List<UnityEngine.TextCore.Glyph>m_GlyphTable  // 0xc0
  private           System.Collections.Generic.Dictionary<System.UInt32,UnityEngine.TextCore.Glyph>m_GlyphLookupDictionary  // 0xc8
  private           System.Collections.Generic.List<UnityEngine.TextCore.Text.Character>m_CharacterTable  // 0xd0
  private           System.Collections.Generic.Dictionary<System.UInt32,UnityEngine.TextCore.Text.Character>m_CharacterLookupDictionary  // 0xd8
  private           UnityEngine.Texture2D           m_AtlasTexture  // 0xe0
  private           UnityEngine.Texture2D[]         m_AtlasTextures  // 0xe8
  private           System.Int32                    m_AtlasTextureIndex  // 0xf0
  private           System.Boolean                  m_IsMultiAtlasTexturesEnabled  // 0xf4
  private           System.Boolean                  m_ClearDynamicDataOnBuild  // 0xf5
  private           System.Int32                    m_AtlasWidth  // 0xf8
  private           System.Int32                    m_AtlasHeight  // 0xfc
  private           System.Int32                    m_AtlasPadding  // 0x100
  private           UnityEngine.TextCore.LowLevel.GlyphRenderModem_AtlasRenderMode  // 0x104
  private           System.Collections.Generic.List<UnityEngine.TextCore.GlyphRect>m_UsedGlyphRects  // 0x108
  private           System.Collections.Generic.List<UnityEngine.TextCore.GlyphRect>m_FreeGlyphRects  // 0x110
  private           UnityEngine.TextCore.Text.FontFeatureTablem_FontFeatureTable  // 0x118
  private           System.Collections.Generic.List<UnityEngine.TextCore.Text.FontAsset>m_FallbackFontAssetTable  // 0x120
  private           UnityEngine.TextCore.Text.FontAssetCreationEditorSettingsm_fontAssetCreationEditorSettings  // 0x128
  private           System.Single                   m_RegularStyleWeight  // 0x178
  private           System.Single                   m_RegularStyleSpacing  // 0x17c
  private           System.Single                   m_BoldStyleWeight  // 0x180
  private           System.Single                   m_BoldStyleSpacing  // 0x184
  private           System.Byte                     m_ItalicStyleSlant  // 0x188
  private           System.Byte                     m_TabMultiple  // 0x189
  private           System.Boolean                  IsFontAssetLookupTablesDirty  // 0x18a
  private   static  Unity.Profiling.ProfilerMarker  k_ReadFontAssetDefinitionMarker  // static @ 0x0
  private   static  Unity.Profiling.ProfilerMarker  k_AddSynthesizedCharactersMarker  // static @ 0x8
  private   static  Unity.Profiling.ProfilerMarker  k_TryAddCharacterMarker  // static @ 0x10
  private   static  Unity.Profiling.ProfilerMarker  k_TryAddCharactersMarker  // static @ 0x18
  private   static  Unity.Profiling.ProfilerMarker  k_UpdateGlyphAdjustmentRecordsMarker  // static @ 0x20
  private   static  Unity.Profiling.ProfilerMarker  k_ClearFontAssetDataMarker  // static @ 0x28
  private   static  Unity.Profiling.ProfilerMarker  k_UpdateFontAssetDataMarker  // static @ 0x30
  private   static  System.String                   s_DefaultMaterialSuffix  // static @ 0x38
  private   static  System.Collections.Generic.HashSet<System.Int32>k_SearchedFontAssetLookup  // static @ 0x40
  private   static  System.Collections.Generic.List<UnityEngine.TextCore.Text.FontAsset>k_FontAssets_FontFeaturesUpdateQueue  // static @ 0x48
  private   static  System.Collections.Generic.HashSet<System.Int32>k_FontAssets_FontFeaturesUpdateQueueLookup  // static @ 0x50
  private   static  System.Collections.Generic.List<UnityEngine.Texture2D>k_FontAssets_AtlasTexturesUpdateQueue  // static @ 0x58
  private   static  System.Collections.Generic.HashSet<System.Int32>k_FontAssets_AtlasTexturesUpdateQueueLookup  // static @ 0x60
  private           System.Collections.Generic.List<UnityEngine.TextCore.Glyph>m_GlyphsToRender  // 0x190
  private           System.Collections.Generic.List<UnityEngine.TextCore.Glyph>m_GlyphsRendered  // 0x198
  private           System.Collections.Generic.List<System.UInt32>m_GlyphIndexList  // 0x1a0
  private           System.Collections.Generic.List<System.UInt32>m_GlyphIndexListNewlyAdded  // 0x1a8
  private           System.Collections.Generic.List<System.UInt32>m_GlyphsToAdd  // 0x1b0
  private           System.Collections.Generic.HashSet<System.UInt32>m_GlyphsToAddLookup  // 0x1b8
  private           System.Collections.Generic.List<UnityEngine.TextCore.Text.Character>m_CharactersToAdd  // 0x1c0
  private           System.Collections.Generic.HashSet<System.UInt32>m_CharactersToAddLookup  // 0x1c8
  private           System.Collections.Generic.List<System.UInt32>s_MissingCharacterList  // 0x1d0
  private           System.Collections.Generic.HashSet<System.UInt32>m_MissingUnicodesFromFontFile  // 0x1d8
  private   static  System.UInt32[]                 k_GlyphIndexArray  // static @ 0x68
PROPERTIES:
  sourceFontFile  get=0x04DBCF90  set=0x042B4AE0
  atlasPopulationMode  get=0x04DBC0F0  set=0x04D86450
  faceInfo  get=0x04DBD500  set=0x0B44FECC
  familyNameHashCode  get=0x0B44FE34  set=0x04D8F8F0
  styleNameHashCode  get=0x0B44FE98  set=0x04D89330
  fontWeightTable  get=0x04DBD0A0  set=0x06AC5AF4
  glyphTable  get=0x04DBD0B0  set=0x06AC5ACC
  glyphLookupTable  get=0x0B44FE68
  characterTable  get=0x04DBD050  set=0x0678D7BC
  characterLookupTable  get=0x0B44FE04
  atlasTexture  get=0x0B44FD64
  atlasTextures  get=0x0B44FDF8  set=0x06AC5B1C
  atlasTextureCount  get=0x04DBD4B0
  isMultiAtlasTexturesEnabled  get=0x04DBD5B0  set=0x04DA7530
  clearDynamicDataOnBuild  get=0x04DBD4F0  set=0x04DA7550
  atlasWidth  get=0x04DBD4C0  set=0x04DA6220
  atlasHeight  get=0x04DBD480  set=0x04D8F9D0
  atlasPadding  get=0x04DBD490  set=0x04D927A0
  atlasRenderMode  get=0x04DBD4A0  set=0x04DBD610
  usedGlyphRects  get=0x04DBD600  set=0x068C2E50
  freeGlyphRects  get=0x04DBD5A0  set=0x05397170
  fontFeatureTable  get=0x04DBD590  set=0x0539715C
  fallbackFontAssetTable  get=0x04DBD540  set=0x053971C0
  fontAssetCreationEditorSettings  get=0x04DBD550  set=0x0B44FF10
  regularStyleWeight  get=0x04DBD5E0  set=0x04D90320
  regularStyleSpacing  get=0x04DBD5D0  set=0x04D90400
  boldStyleWeight  get=0x04DBD4E0  set=0x04DBD620
  boldStyleSpacing  get=0x04DBD4D0  set=0x04D8EE30
  italicStyleSlant  get=0x04DBD5C0  set=0x04D90100
  tabMultiple  get=0x04DBD5F0  set=0x04DBD630
METHODS:
  RVA=0x0B44A4CC  token=0x600003E  UnityEngine.TextCore.Text.FontAsset CreateFontAsset(System.String familyName, System.String styleName, System.Int32 pointSize)
  RVA=0x0B44A32C  token=0x600003F  UnityEngine.TextCore.Text.FontAsset CreateFontAsset(System.String fontFilePath, System.Int32 faceIndex, System.Int32 samplingPointSize, System.Int32 atlasPadding, UnityEngine.TextCore.LowLevel.GlyphRenderMode renderMode, System.Int32 atlasWidth, System.Int32 atlasHeight, UnityEngine.TextCore.Text.AtlasPopulationMode atlasPopulationMode, System.Boolean enableMultiAtlasSupport)
  RVA=0x0B44A45C  token=0x6000040  UnityEngine.TextCore.Text.FontAsset CreateFontAsset(UnityEngine.Font font)
  RVA=0x0B44A6B0  token=0x6000041  UnityEngine.TextCore.Text.FontAsset CreateFontAsset(UnityEngine.Font font, System.Int32 samplingPointSize, System.Int32 atlasPadding, UnityEngine.TextCore.LowLevel.GlyphRenderMode renderMode, System.Int32 atlasWidth, System.Int32 atlasHeight, UnityEngine.TextCore.Text.AtlasPopulationMode atlasPopulationMode, System.Boolean enableMultiAtlasSupport)
  RVA=0x0B44A754  token=0x6000042  UnityEngine.TextCore.Text.FontAsset CreateFontAsset(UnityEngine.Font font, System.Int32 faceIndex, System.Int32 samplingPointSize, System.Int32 atlasPadding, UnityEngine.TextCore.LowLevel.GlyphRenderMode renderMode, System.Int32 atlasWidth, System.Int32 atlasHeight, UnityEngine.TextCore.Text.AtlasPopulationMode atlasPopulationMode, System.Boolean enableMultiAtlasSupport)
  RVA=0x0B449E84  token=0x6000043  UnityEngine.TextCore.Text.FontAsset CreateFontAssetInstance(UnityEngine.Font font, System.Int32 atlasPadding, UnityEngine.TextCore.LowLevel.GlyphRenderMode renderMode, System.Int32 atlasWidth, System.Int32 atlasHeight, UnityEngine.TextCore.Text.AtlasPopulationMode atlasPopulationMode, System.Boolean enableMultiAtlasSupport)
  RVA=0x041E1670  token=0x6000044  System.Void Awake()
  RVA=0x0B44BEE8  token=0x6000045  System.Void OnDestroy()
  RVA=0x0B44BF38  token=0x6000046  System.Void ReadFontAssetDefinition()
  RVA=0x0B44B9E8  token=0x6000047  System.Void InitializeDictionaryLookupTables()
  RVA=0x0B44BA14  token=0x6000048  System.Void InitializeGlyphLookupDictionary()
  RVA=0x0B44B828  token=0x6000049  System.Void InitializeCharacterLookupDictionary()
  RVA=0x0B44BC8C  token=0x600004A  System.Void InitializeGlyphPaidAdjustmentRecordsLookupDictionary()
  RVA=0x0B4498F8  token=0x600004B  System.Void AddSynthesizedCharactersAndFaceMetrics()
  RVA=0x0B4496DC  token=0x600004C  System.Void AddSynthesizedCharacter(System.UInt32 unicode, System.Boolean isFontFaceLoaded, System.Boolean addImmediately)
  RVA=0x0B449674  token=0x600004D  System.Void AddCharacterToLookupCache(System.UInt32 unicode, UnityEngine.TextCore.Text.Character character)
  RVA=0x0B44BE58  token=0x600004E  UnityEngine.TextCore.LowLevel.FontEngineError LoadFontFace()
  RVA=0x0B44C5F8  token=0x600004F  System.Void SortCharacterTable()
  RVA=0x0B44C788  token=0x6000050  System.Void SortGlyphTable()
  RVA=0x0B44C764  token=0x6000051  System.Void SortFontFeatureTable()
  RVA=0x0B44C5CC  token=0x6000052  System.Void SortAllTables()
  RVA=0x0B44AE50  token=0x6000053  System.Boolean HasCharacter(System.Int32 character)
  RVA=0x0B44AEB4  token=0x6000054  System.Boolean HasCharacter(System.Char character, System.Boolean searchFallbacks, System.Boolean tryAddCharacter)
  RVA=0x0B44AC20  token=0x6000055  System.Boolean HasCharacter_Internal(System.UInt32 character, System.Boolean searchFallbacks, System.Boolean tryAddCharacter)
  RVA=0x0B44B6D4  token=0x6000056  System.Boolean HasCharacters(System.String text, System.Collections.Generic.List<System.Char>& missingCharacters)
  RVA=0x0B44B2A4  token=0x6000057  System.Boolean HasCharacters(System.String text, System.UInt32[]& missingCharacters, System.Boolean searchFallbacks, System.Boolean tryAddCharacter)
  RVA=0x0B44B1F8  token=0x6000058  System.Boolean HasCharacters(System.String text)
  RVA=0x0B44AA78  token=0x6000059  System.String GetCharacters(UnityEngine.TextCore.Text.FontAsset fontAsset)
  RVA=0x0B44A9AC  token=0x600005A  System.Int32[] GetCharactersArray(UnityEngine.TextCore.Text.FontAsset fontAsset)
  RVA=0x0B44AB58  token=0x600005B  System.UInt32 GetGlyphIndex(System.UInt32 unicode)
  RVA=0x0B44C31C  token=0x600005C  System.Void RegisterFontAssetForFontFeatureUpdate(UnityEngine.TextCore.Text.FontAsset fontAsset)
  RVA=0x0B44F064  token=0x600005D  System.Void UpdateFontFeaturesForFontAssetsInQueue()
  RVA=0x0B44C250  token=0x600005E  System.Void RegisterAtlasTextureForApply(UnityEngine.Texture2D texture)
  RVA=0x0B44EDE0  token=0x600005F  System.Void UpdateAtlasTexturesInQueue()
  RVA=0x0B44F01C  token=0x6000060  System.Void UpdateFontAssetInUpdateQueue()
  RVA=0x0B44D20C  token=0x6000061  System.Boolean TryAddCharacters(System.UInt32[] unicodes, System.Boolean includeFontFeatures)
  RVA=0x0B44D230  token=0x6000062  System.Boolean TryAddCharacters(System.UInt32[] unicodes, System.UInt32[]& missingUnicodes, System.Boolean includeFontFeatures)
  RVA=0x0B44D1E8  token=0x6000063  System.Boolean TryAddCharacters(System.String characters, System.Boolean includeFontFeatures)
  RVA=0x0B44DBAC  token=0x6000064  System.Boolean TryAddCharacters(System.String characters, System.String& missingCharacters, System.Boolean includeFontFeatures)
  RVA=0x0B44C8F4  token=0x6000065  System.Boolean TryAddCharacterInternal(System.UInt32 unicode, UnityEngine.TextCore.Text.Character& character, System.Boolean shouldGetFontFeatures)
  RVA=0x0B44E864  token=0x6000066  System.Boolean TryGetCharacter_and_QueueRenderToTexture(System.UInt32 unicode, UnityEngine.TextCore.Text.Character& character, System.Boolean shouldGetFontFeatures)
  RVA=0x041E1670  token=0x6000067  System.Void TryAddGlyphsToAtlasTextures()
  RVA=0x0B44E504  token=0x6000068  System.Boolean TryAddGlyphsToNewAtlasTexture()
  RVA=0x0B44C3E8  token=0x6000069  System.Void SetupNewAtlasTexture()
  RVA=0x0B44EC54  token=0x600006A  System.Void UpdateAtlasTexture()
  RVA=0x0B44F3B0  token=0x600006B  System.Void UpdateGlyphAdjustmentRecords()
  RVA=0x0B44F194  token=0x600006C  System.Void UpdateGlyphAdjustmentRecords(System.UInt32[] glyphIndexes)
  RVA=0x041E1670  token=0x600006D  System.Void UpdateGlyphAdjustmentRecords(System.Collections.Generic.List<System.UInt32> glyphIndexes)
  RVA=0x041E1670  token=0x600006E  System.Void UpdateGlyphAdjustmentRecords(System.Collections.Generic.List<System.UInt32> newGlyphIndexes, System.Collections.Generic.List<System.UInt32> allGlyphIndexes)
  RVA=-1  // generic def  token=0x600006F  System.Void CopyListDataToArray(System.Collections.Generic.List<T> srcList, T[]& dstArray)
  RVA=0x0B449CCC  token=0x6000070  System.Void ClearFontAssetData(System.Boolean setAtlasSizeToZero)
  RVA=0x0B449CA8  token=0x6000071  System.Void ClearFontAssetDataInternal()
  RVA=0x0B44EF14  token=0x6000072  System.Void UpdateFontAssetData()
  RVA=0x0B449D08  token=0x6000073  System.Void ClearFontAssetTables()
  RVA=0x0B449AE0  token=0x6000074  System.Void ClearAtlasTextures(System.Boolean setAtlasSizeToZero)
  RVA=0x0B44A900  token=0x6000075  System.Void DestroyAtlasTextures()
  RVA=0x0B44F980  token=0x6000076  System.Void .ctor()
  RVA=0x0B44F600  token=0x6000077  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.TextCore.Text.FontAssetUtilities
TYPE:  static class
TOKEN: 0x200000A
SIZE:  0x10
FIELDS:
  private   static  System.Collections.Generic.HashSet<System.Int32>k_SearchedAssets  // static @ 0x0
METHODS:
  RVA=0x0B448F8C  token=0x600007C  UnityEngine.TextCore.Text.Character GetCharacterFromFontAsset(System.UInt32 unicode, UnityEngine.TextCore.Text.FontAsset sourceFontAsset, System.Boolean includeFallbacks, UnityEngine.TextCore.Text.FontStyles fontStyle, UnityEngine.TextCore.Text.TextFontWeight fontWeight, System.Boolean& isAlternativeTypeface)
  RVA=0x0B448BF0  token=0x600007D  UnityEngine.TextCore.Text.Character GetCharacterFromFontAsset_Internal(System.UInt32 unicode, UnityEngine.TextCore.Text.FontAsset sourceFontAsset, System.Boolean includeFallbacks, UnityEngine.TextCore.Text.FontStyles fontStyle, UnityEngine.TextCore.Text.TextFontWeight fontWeight, System.Boolean& isAlternativeTypeface)
  RVA=0x0B4490AC  token=0x600007E  UnityEngine.TextCore.Text.Character GetCharacterFromFontAssets(System.UInt32 unicode, UnityEngine.TextCore.Text.FontAsset sourceFontAsset, System.Collections.Generic.List<UnityEngine.TextCore.Text.FontAsset> fontAssets, System.Boolean includeFallbacks, UnityEngine.TextCore.Text.FontStyles fontStyle, UnityEngine.TextCore.Text.TextFontWeight fontWeight, System.Boolean& isAlternativeTypeface)
  RVA=0x0B4493F8  token=0x600007F  UnityEngine.TextCore.Text.SpriteCharacter GetSpriteCharacterFromSpriteAsset(System.UInt32 unicode, UnityEngine.TextCore.Text.SpriteAsset spriteAsset, System.Boolean includeFallbacks)
  RVA=0x0B449268  token=0x6000080  UnityEngine.TextCore.Text.SpriteCharacter GetSpriteCharacterFromSpriteAsset_Internal(System.UInt32 unicode, UnityEngine.TextCore.Text.SpriteAsset spriteAsset, System.Boolean includeFallbacks)
END_CLASS

CLASS: UnityEngine.TextCore.Text.FontFeatureTable
TYPE:  class
TOKEN: 0x200000B
SIZE:  0x20
FIELDS:
  private           System.Collections.Generic.List<UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord>m_GlyphPairAdjustmentRecords  // 0x10
  private           System.Collections.Generic.Dictionary<System.UInt32,UnityEngine.TextCore.LowLevel.GlyphPairAdjustmentRecord>m_GlyphPairAdjustmentRecordLookup  // 0x18
PROPERTIES:
  glyphPairAdjustmentRecords  get=0x04D9B600
METHODS:
  RVA=0x0B450174  token=0x6000082  System.Void .ctor()
  RVA=0x0B44FF54  token=0x6000083  System.Void SortGlyphPairAdjustmentRecords()
END_CLASS

CLASS: UnityEngine.TextCore.Text.Extents
TYPE:  sealed struct
TOKEN: 0x200000D
SIZE:  0x20
FIELDS:
  public            UnityEngine.Vector2             min  // 0x10
  public            UnityEngine.Vector2             max  // 0x18
METHODS:
  RVA=0x0B448A6C  token=0x6000088  System.String ToString()
END_CLASS

CLASS: UnityEngine.TextCore.Text.LineInfo
TYPE:  sealed struct
TOKEN: 0x200000E
SIZE:  0x70
FIELDS:
  private           System.Int32                    controlCharacterCount  // 0x10
  public            System.Int32                    characterCount  // 0x14
  public            System.Int32                    visibleCharacterCount  // 0x18
  public            System.Int32                    spaceCount  // 0x1c
  public            System.Int32                    visibleSpaceCount  // 0x20
  public            System.Int32                    wordCount  // 0x24
  public            System.Int32                    firstCharacterIndex  // 0x28
  public            System.Int32                    firstVisibleCharacterIndex  // 0x2c
  public            System.Int32                    lastCharacterIndex  // 0x30
  public            System.Int32                    lastVisibleCharacterIndex  // 0x34
  public            System.Single                   length  // 0x38
  public            System.Single                   lineHeight  // 0x3c
  public            System.Single                   ascender  // 0x40
  public            System.Single                   baseline  // 0x44
  public            System.Single                   descender  // 0x48
  public            System.Single                   maxAdvance  // 0x4c
  public            System.Single                   width  // 0x50
  public            System.Single                   marginLeft  // 0x54
  public            System.Single                   marginRight  // 0x58
  public            UnityEngine.TextCore.Text.TextAlignmentalignment  // 0x5c
  public            UnityEngine.TextCore.Text.ExtentslineExtents  // 0x60
METHODS:
END_CLASS

CLASS: UnityEngine.TextCore.Text.LinkInfo
TYPE:  sealed struct
TOKEN: 0x200000F
SIZE:  0x30
FIELDS:
  public            System.Int32                    hashCode  // 0x10
  public            System.Int32                    linkIdFirstCharacterIndex  // 0x14
  public            System.Int32                    linkIdLength  // 0x18
  public            System.Int32                    linkTextfirstCharacterIndex  // 0x1c
  public            System.Int32                    linkTextLength  // 0x20
  private           System.Char[]                   linkId  // 0x28
METHODS:
  RVA=0x0B450234  token=0x6000089  System.Void SetLinkId(System.Char[] text, System.Int32 startIndex, System.Int32 length)
END_CLASS

CLASS: UnityEngine.TextCore.Text.MaterialManager
TYPE:  static class
TOKEN: 0x2000010
SIZE:  0x10
FIELDS:
  private   static  System.Collections.Generic.Dictionary<System.Int64,UnityEngine.Material>s_FallbackMaterials  // static @ 0x0
METHODS:
  RVA=0x0B4502F8  token=0x600008A  UnityEngine.Material GetFallbackMaterial(UnityEngine.Material sourceMaterial, UnityEngine.Material targetMaterial)
  RVA=0x0B450688  token=0x600008B  UnityEngine.Material GetFallbackMaterial(UnityEngine.TextCore.Text.FontAsset fontAsset, UnityEngine.Material sourceMaterial, System.Int32 atlasIndex)
  RVA=0x0B45084C  token=0x600008C  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.TextCore.Text.MaterialReference
TYPE:  sealed struct
TOKEN: 0x2000011
SIZE:  0x48
FIELDS:
  public            System.Int32                    index  // 0x10
  public            UnityEngine.TextCore.Text.FontAssetfontAsset  // 0x18
  public            UnityEngine.TextCore.Text.SpriteAssetspriteAsset  // 0x20
  public            UnityEngine.Material            material  // 0x28
  public            System.Boolean                  isDefaultMaterial  // 0x30
  public            System.Boolean                  isFallbackMaterial  // 0x31
  public            UnityEngine.Material            fallbackMaterial  // 0x38
  public            System.Single                   padding  // 0x40
  public            System.Int32                    referenceCount  // 0x44
METHODS:
  RVA=0x0B4514C8  token=0x600008D  System.Void .ctor(System.Int32 index, UnityEngine.TextCore.Text.FontAsset fontAsset, UnityEngine.TextCore.Text.SpriteAsset spriteAsset, UnityEngine.Material material, System.Single padding)
  RVA=0x0B4510DC  token=0x600008E  System.Int32 AddMaterialReference(UnityEngine.Material material, UnityEngine.TextCore.Text.FontAsset fontAsset, UnityEngine.TextCore.Text.MaterialReference[]& materialReferences, System.Collections.Generic.Dictionary<System.Int32,System.Int32> materialReferenceIndexLookup)
  RVA=0x0B4512DC  token=0x600008F  System.Int32 AddMaterialReference(UnityEngine.Material material, UnityEngine.TextCore.Text.SpriteAsset spriteAsset, UnityEngine.TextCore.Text.MaterialReference[]& materialReferences, System.Collections.Generic.Dictionary<System.Int32,System.Int32> materialReferenceIndexLookup)
END_CLASS

CLASS: UnityEngine.TextCore.Text.MaterialReferenceManager
TYPE:  class
TOKEN: 0x2000012
SIZE:  0x30
FIELDS:
  private   static  UnityEngine.TextCore.Text.MaterialReferenceManagers_Instance  // static @ 0x0
  private           System.Collections.Generic.Dictionary<System.Int32,UnityEngine.Material>m_FontMaterialReferenceLookup  // 0x10
  private           System.Collections.Generic.Dictionary<System.Int32,UnityEngine.TextCore.Text.FontAsset>m_FontAssetReferenceLookup  // 0x18
  private           System.Collections.Generic.Dictionary<System.Int32,UnityEngine.TextCore.Text.SpriteAsset>m_SpriteAssetReferenceLookup  // 0x20
  private           System.Collections.Generic.Dictionary<System.Int32,UnityEngine.TextCore.Text.TextColorGradient>m_ColorGradientReferenceLookup  // 0x28
PROPERTIES:
  instance  get=0x0B45104C
METHODS:
  RVA=0x0B450A80  token=0x6000091  System.Void AddFontAsset(UnityEngine.TextCore.Text.FontAsset fontAsset)
  RVA=0x0B4509A0  token=0x6000092  System.Void AddFontAssetInternal(UnityEngine.TextCore.Text.FontAsset fontAsset)
  RVA=0x0B450C08  token=0x6000093  System.Void AddSpriteAsset(System.Int32 hashCode, UnityEngine.TextCore.Text.SpriteAsset spriteAsset)
  RVA=0x0B450B50  token=0x6000094  System.Void AddSpriteAssetInternal(System.Int32 hashCode, UnityEngine.TextCore.Text.SpriteAsset spriteAsset)
  RVA=0x0B450B14  token=0x6000095  System.Void AddFontMaterial(System.Int32 hashCode, UnityEngine.Material material)
  RVA=0x0B450AB0  token=0x6000096  System.Void AddFontMaterialInternal(System.Int32 hashCode, UnityEngine.Material material)
  RVA=0x0B450964  token=0x6000097  System.Void AddColorGradientPreset(System.Int32 hashCode, UnityEngine.TextCore.Text.TextColorGradient spriteAsset)
  RVA=0x0B4508D8  token=0x6000098  System.Void AddColorGradientPreset_Internal(System.Int32 hashCode, UnityEngine.TextCore.Text.TextColorGradient spriteAsset)
  RVA=0x0B450D60  token=0x6000099  System.Boolean TryGetFontAsset(System.Int32 hashCode, UnityEngine.TextCore.Text.FontAsset& fontAsset)
  RVA=0x0B450CF0  token=0x600009A  System.Boolean TryGetFontAssetInternal(System.Int32 hashCode, UnityEngine.TextCore.Text.FontAsset& fontAsset)
  RVA=0x0B450EB8  token=0x600009B  System.Boolean TryGetSpriteAsset(System.Int32 hashCode, UnityEngine.TextCore.Text.SpriteAsset& spriteAsset)
  RVA=0x0B450E48  token=0x600009C  System.Boolean TryGetSpriteAssetInternal(System.Int32 hashCode, UnityEngine.TextCore.Text.SpriteAsset& spriteAsset)
  RVA=0x0B450CB4  token=0x600009D  System.Boolean TryGetColorGradientPreset(System.Int32 hashCode, UnityEngine.TextCore.Text.TextColorGradient& gradientPreset)
  RVA=0x0B450C44  token=0x600009E  System.Boolean TryGetColorGradientPresetInternal(System.Int32 hashCode, UnityEngine.TextCore.Text.TextColorGradient& gradientPreset)
  RVA=0x0B450E0C  token=0x600009F  System.Boolean TryGetMaterial(System.Int32 hashCode, UnityEngine.Material& material)
  RVA=0x0B450D9C  token=0x60000A0  System.Boolean TryGetMaterialInternal(System.Int32 hashCode, UnityEngine.Material& material)
  RVA=0x0B450EF4  token=0x60000A1  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.TextCore.Text.VertexSortingOrder
TYPE:  sealed struct
TOKEN: 0x2000013
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.TextCore.Text.VertexSortingOrderNormal  // const
  public    static  UnityEngine.TextCore.Text.VertexSortingOrderReverse  // const
METHODS:
END_CLASS

CLASS: UnityEngine.TextCore.Text.MeshInfo
TYPE:  sealed struct
TOKEN: 0x2000014
SIZE:  0x48
FIELDS:
  private   static readonly UnityEngine.Color32             k_DefaultColor  // static @ 0x0
  public            System.Int32                    vertexCount  // 0x10
  public            UnityEngine.Vector3[]           vertices  // 0x18
  public            UnityEngine.Vector2[]           uvs0  // 0x20
  public            UnityEngine.Vector2[]           uvs2  // 0x28
  public            UnityEngine.Color32[]           colors32  // 0x30
  public            System.Int32[]                  triangles  // 0x38
  public            UnityEngine.Material            material  // 0x40
METHODS:
  RVA=0x0B4520EC  token=0x60000A2  System.Void .ctor(System.Int32 size)
  RVA=0x0B4515D4  token=0x60000A3  System.Void ResizeMeshInfo(System.Int32 size)
  RVA=0x0B4515A4  token=0x60000A4  System.Void Clear(System.Boolean uploadChanges)
  RVA=0x0B451570  token=0x60000A5  System.Void ClearUnusedVertices()
  RVA=0x0B4517C0  token=0x60000A6  System.Void SortGeometry(UnityEngine.TextCore.Text.VertexSortingOrder order)
  RVA=0x0B45185C  token=0x60000A7  System.Void SwapVertexData(System.Int32 src, System.Int32 dst)
  RVA=0x0B4520AC  token=0x60000A8  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.TextCore.Text.SpriteAsset
TYPE:  class
TOKEN: 0x2000015
SIZE:  0xE0
EXTENDS: UnityEngine.TextCore.Text.TextAsset
FIELDS:
  private           System.Collections.Generic.Dictionary<System.Int32,System.Int32>m_NameLookup  // 0x38
  private           System.Collections.Generic.Dictionary<System.UInt32,System.Int32>m_GlyphIndexLookup  // 0x40
  private           UnityEngine.TextCore.FaceInfo   m_FaceInfo  // 0x48
  private           UnityEngine.Texture             m_SpriteAtlasTexture  // 0xa8
  private           System.Collections.Generic.List<UnityEngine.TextCore.Text.SpriteCharacter>m_SpriteCharacterTable  // 0xb0
  private           System.Collections.Generic.Dictionary<System.UInt32,UnityEngine.TextCore.Text.SpriteCharacter>m_SpriteCharacterLookup  // 0xb8
  private           System.Collections.Generic.List<UnityEngine.TextCore.Text.SpriteGlyph>m_SpriteGlyphTable  // 0xc0
  private           System.Collections.Generic.Dictionary<System.UInt32,UnityEngine.TextCore.Text.SpriteGlyph>m_SpriteGlyphLookup  // 0xc8
  public            System.Collections.Generic.List<UnityEngine.TextCore.Text.SpriteAsset>fallbackSpriteAssets  // 0xd0
  private           System.Boolean                  m_IsSpriteAssetLookupTablesDirty  // 0xd8
  private   static  System.Collections.Generic.HashSet<System.Int32>k_searchedSpriteAssets  // static @ 0x0
PROPERTIES:
  faceInfo  get=0x04DBD640  set=0x0B330F50
  spriteSheet  get=0x04DBCFF0  set=0x04D84CC0
  spriteCharacterTable  get=0x0B453848  set=0x04D84CE0
  spriteCharacterLookupTable  get=0x0B453818  set=0x06AC5AF4
  spriteGlyphTable  get=0x04DBD0B0  set=0x06AC5ACC
METHODS:
  RVA=0x041E1670  token=0x60000B3  System.Void Awake()
  RVA=0x0B45323C  token=0x60000B4  System.Void UpdateLookupTables()
  RVA=0x0B4524C4  token=0x60000B5  System.Int32 GetSpriteIndexFromHashcode(System.Int32 hashCode)
  RVA=0x0B452580  token=0x60000B6  System.Int32 GetSpriteIndexFromUnicode(System.UInt32 unicode)
  RVA=0x0B452540  token=0x60000B7  System.Int32 GetSpriteIndexFromName(System.String name)
  RVA=0x0B452D7C  token=0x60000B8  UnityEngine.TextCore.Text.SpriteAsset SearchForSpriteByUnicode(UnityEngine.TextCore.Text.SpriteAsset spriteAsset, System.UInt32 unicode, System.Boolean includeFallbacks, System.Int32& spriteIndex)
  RVA=0x0B452B64  token=0x60000B9  UnityEngine.TextCore.Text.SpriteAsset SearchForSpriteByUnicodeInternal(System.Collections.Generic.List<UnityEngine.TextCore.Text.SpriteAsset> spriteAssets, System.UInt32 unicode, System.Boolean includeFallbacks, System.Int32& spriteIndex)
  RVA=0x0B452CBC  token=0x60000BA  UnityEngine.TextCore.Text.SpriteAsset SearchForSpriteByUnicodeInternal(UnityEngine.TextCore.Text.SpriteAsset spriteAsset, System.UInt32 unicode, System.Boolean includeFallbacks, System.Int32& spriteIndex)
  RVA=0x0B452824  token=0x60000BB  UnityEngine.TextCore.Text.SpriteAsset SearchForSpriteByHashCode(UnityEngine.TextCore.Text.SpriteAsset spriteAsset, System.Int32 hashCode, System.Boolean includeFallbacks, System.Int32& spriteIndex, UnityEngine.TextCore.Text.TextSettings textSettings)
  RVA=0x0B45260C  token=0x60000BC  UnityEngine.TextCore.Text.SpriteAsset SearchForSpriteByHashCodeInternal(System.Collections.Generic.List<UnityEngine.TextCore.Text.SpriteAsset> spriteAssets, System.Int32 hashCode, System.Boolean searchFallbacks, System.Int32& spriteIndex)
  RVA=0x0B452764  token=0x60000BD  UnityEngine.TextCore.Text.SpriteAsset SearchForSpriteByHashCodeInternal(UnityEngine.TextCore.Text.SpriteAsset spriteAsset, System.Int32 hashCode, System.Boolean searchFallbacks, System.Int32& spriteIndex)
  RVA=0x0B4530D0  token=0x60000BE  System.Void SortGlyphTable()
  RVA=0x0B452F44  token=0x60000BF  System.Void SortCharacterTable()
  RVA=0x0B4530B0  token=0x60000C0  System.Void SortGlyphAndCharacterTables()
  RVA=0x0B45373C  token=0x60000C1  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.TextCore.Text.SpriteCharacter
TYPE:  class
TOKEN: 0x2000017
SIZE:  0x40
EXTENDS: UnityEngine.TextCore.Text.TextElement
FIELDS:
  private           System.String                   m_Name  // 0x30
  private           System.Int32                    m_HashCode  // 0x38
PROPERTIES:
  name  get=0x04DBBEB0
METHODS:
  RVA=0x04DBA3A0  token=0x60000C7  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.TextCore.Text.SpriteGlyph
TYPE:  class
TOKEN: 0x2000018
SIZE:  0x50
EXTENDS: UnityEngine.TextCore.Glyph
FIELDS:
  public            UnityEngine.Sprite              sprite  // 0x48
METHODS:
  RVA=0x0B3311A8  token=0x60000C8  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.TextCore.Text.TextAsset
TYPE:  abstract class
TOKEN: 0x2000019
SIZE:  0x38
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
  private           System.String                   m_Version  // 0x18
  private           System.Int32                    m_InstanceID  // 0x20
  private           System.Int32                    m_HashCode  // 0x24
  private           UnityEngine.Material            m_Material  // 0x28
  private           System.Int32                    m_MaterialHashCode  // 0x30
PROPERTIES:
  version  get=0x04D96330  set=0x05392C40
  instanceID  get=0x0B4538A4
  hashCode  get=0x0B453878  set=0x011F0030
  material  get=0x04D86240  set=0x02FDB880
  materialHashCode  get=0x0B4538C8  set=0x04D86740
METHODS:
  RVA=0x03573370  token=0x60000D2  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.TextCore.Text.ColorGradientMode
TYPE:  sealed struct
TOKEN: 0x200001A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.TextCore.Text.ColorGradientModeSingle  // const
  public    static  UnityEngine.TextCore.Text.ColorGradientModeHorizontalGradient  // const
  public    static  UnityEngine.TextCore.Text.ColorGradientModeVerticalGradient  // const
  public    static  UnityEngine.TextCore.Text.ColorGradientModeFourCornersGradient  // const
METHODS:
END_CLASS

CLASS: UnityEngine.TextCore.Text.TextColorGradient
TYPE:  class
TOKEN: 0x200001B
SIZE:  0x60
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
  public            UnityEngine.TextCore.Text.ColorGradientModecolorMode  // 0x18
  public            UnityEngine.Color               topLeft  // 0x1c
  public            UnityEngine.Color               topRight  // 0x2c
  public            UnityEngine.Color               bottomLeft  // 0x3c
  public            UnityEngine.Color               bottomRight  // 0x4c
  private   static  UnityEngine.TextCore.Text.ColorGradientModek_DefaultColorMode  // const
  private   static readonly UnityEngine.Color               k_DefaultColor  // static @ 0x0
METHODS:
  RVA=0x0B45399C  token=0x60000D3  System.Void .ctor()
  RVA=0x0B30CB60  token=0x60000D4  System.Void .ctor(UnityEngine.Color color)
  RVA=0x0B30CBA4  token=0x60000D5  System.Void .ctor(UnityEngine.Color color0, UnityEngine.Color color1, UnityEngine.Color color2, UnityEngine.Color color3)
  RVA=0x0B453958  token=0x60000D6  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.TextCore.Text.TextElementType
TYPE:  sealed struct
TOKEN: 0x200001C
SIZE:  0x11
FIELDS:
  public            System.Byte                     value__  // 0x10
  public    static  UnityEngine.TextCore.Text.TextElementTypeCharacter  // const
  public    static  UnityEngine.TextCore.Text.TextElementTypeSprite  // const
METHODS:
END_CLASS

CLASS: UnityEngine.TextCore.Text.TextElement
TYPE:  abstract class
TOKEN: 0x200001D
SIZE:  0x30
FIELDS:
  protected         UnityEngine.TextCore.Text.TextElementTypem_ElementType  // 0x10
  private           System.UInt32                   m_Unicode  // 0x14
  private           UnityEngine.TextCore.Text.TextAssetm_TextAsset  // 0x18
  private           UnityEngine.TextCore.Glyph      m_Glyph  // 0x20
  private           System.UInt32                   m_GlyphIndex  // 0x28
  private           System.Single                   m_Scale  // 0x2c
PROPERTIES:
  elementType  get=0x04DBCFA0
  unicode  get=0x04D872D0  set=0x04D8F540
  textAsset  get=0x04D96330  set=0x05392C40
  glyph  get=0x04DBA910  set=0x05390F40
  glyphIndex  get=0x04DBBEC0  set=0x011F2AA0
  scale  get=0x04DBD1B0  set=0x04D86B00
METHODS:
  RVA=0x041E1670  token=0x60000E2  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.TextCore.Text.TextVertex
TYPE:  sealed struct
TOKEN: 0x200001E
SIZE:  0x38
FIELDS:
  public            UnityEngine.Vector3             position  // 0x10
  public            UnityEngine.Vector2             uv  // 0x1c
  public            UnityEngine.Vector2             uv2  // 0x24
  public            UnityEngine.Vector2             uv4  // 0x2c
  public            UnityEngine.Color32             color  // 0x34
METHODS:
END_CLASS

CLASS: UnityEngine.TextCore.Text.TextElementInfo
TYPE:  sealed struct
TOKEN: 0x200001F
SIZE:  0x168
FIELDS:
  public            System.Char                     character  // 0x10
  public            System.Int32                    index  // 0x14
  public            UnityEngine.TextCore.Text.TextElementTypeelementType  // 0x18
  public            UnityEngine.TextCore.Text.TextElementtextElement  // 0x20
  public            UnityEngine.TextCore.Text.FontAssetfontAsset  // 0x28
  public            UnityEngine.TextCore.Text.SpriteAssetspriteAsset  // 0x30
  public            System.Int32                    spriteIndex  // 0x38
  public            UnityEngine.Material            material  // 0x40
  public            System.Int32                    materialReferenceIndex  // 0x48
  public            System.Boolean                  isUsingAlternateTypeface  // 0x4c
  public            System.Single                   pointSize  // 0x50
  public            System.Int32                    lineNumber  // 0x54
  public            System.Int32                    pageNumber  // 0x58
  public            System.Int32                    vertexIndex  // 0x5c
  public            UnityEngine.TextCore.Text.TextVertexvertexTopLeft  // 0x60
  public            UnityEngine.TextCore.Text.TextVertexvertexBottomLeft  // 0x88
  public            UnityEngine.TextCore.Text.TextVertexvertexTopRight  // 0xb0
  public            UnityEngine.TextCore.Text.TextVertexvertexBottomRight  // 0xd8
  public            UnityEngine.Vector3             topLeft  // 0x100
  public            UnityEngine.Vector3             bottomLeft  // 0x10c
  public            UnityEngine.Vector3             topRight  // 0x118
  public            UnityEngine.Vector3             bottomRight  // 0x124
  public            System.Single                   origin  // 0x130
  public            System.Single                   ascender  // 0x134
  public            System.Single                   baseLine  // 0x138
  public            System.Single                   descender  // 0x13c
  public            System.Single                   xAdvance  // 0x140
  public            System.Single                   aspectRatio  // 0x144
  public            System.Single                   scale  // 0x148
  public            UnityEngine.Color32             color  // 0x14c
  public            UnityEngine.Color32             underlineColor  // 0x150
  public            UnityEngine.Color32             strikethroughColor  // 0x154
  public            UnityEngine.Color32             highlightColor  // 0x158
  public            UnityEngine.TextCore.Text.FontStylesstyle  // 0x15c
  public            System.Boolean                  isVisible  // 0x160
METHODS:
END_CLASS

CLASS: UnityEngine.TextCore.Text.TextOverflowMode
TYPE:  sealed struct
TOKEN: 0x2000020
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.TextCore.Text.TextOverflowModeOverflow  // const
  public    static  UnityEngine.TextCore.Text.TextOverflowModeEllipsis  // const
  public    static  UnityEngine.TextCore.Text.TextOverflowModeMasking  // const
  public    static  UnityEngine.TextCore.Text.TextOverflowModeTruncate  // const
  public    static  UnityEngine.TextCore.Text.TextOverflowModeScrollRect  // const
  public    static  UnityEngine.TextCore.Text.TextOverflowModePage  // const
  public    static  UnityEngine.TextCore.Text.TextOverflowModeLinked  // const
METHODS:
END_CLASS

CLASS: UnityEngine.TextCore.Text.TextureMapping
TYPE:  sealed struct
TOKEN: 0x2000021
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.TextCore.Text.TextureMappingCharacter  // const
  public    static  UnityEngine.TextCore.Text.TextureMappingLine  // const
  public    static  UnityEngine.TextCore.Text.TextureMappingParagraph  // const
  public    static  UnityEngine.TextCore.Text.TextureMappingMatchAspect  // const
METHODS:
END_CLASS

CLASS: UnityEngine.TextCore.Text.TextGenerationSettings
TYPE:  class
TOKEN: 0x2000022
SIZE:  0x100
IMPLEMENTS: System.IEquatable`1
FIELDS:
  public            System.String                   text  // 0x10
  public            UnityEngine.Rect                screenRect  // 0x18
  public            UnityEngine.Vector4             margins  // 0x28
  public            System.Single                   scale  // 0x38
  public            UnityEngine.TextCore.Text.FontAssetfontAsset  // 0x40
  public            UnityEngine.Material            material  // 0x48
  public            UnityEngine.TextCore.Text.SpriteAssetspriteAsset  // 0x50
  public            UnityEngine.TextCore.Text.TextStyleSheetstyleSheet  // 0x58
  public            UnityEngine.TextCore.Text.FontStylesfontStyle  // 0x60
  public            UnityEngine.TextCore.Text.TextSettingstextSettings  // 0x68
  public            UnityEngine.TextCore.Text.TextAlignmenttextAlignment  // 0x70
  public            UnityEngine.TextCore.Text.TextOverflowModeoverflowMode  // 0x74
  public            System.Boolean                  wordWrap  // 0x78
  public            System.Single                   wordWrappingRatio  // 0x7c
  public            UnityEngine.Color               color  // 0x80
  public            UnityEngine.TextCore.Text.TextColorGradientfontColorGradient  // 0x90
  public            System.Boolean                  tintSprites  // 0x98
  public            System.Boolean                  overrideRichTextColors  // 0x99
  public            System.Single                   fontSize  // 0x9c
  public            System.Boolean                  autoSize  // 0xa0
  public            System.Single                   fontSizeMin  // 0xa4
  public            System.Single                   fontSizeMax  // 0xa8
  public            System.Boolean                  enableKerning  // 0xac
  public            System.Boolean                  richText  // 0xad
  public            System.Boolean                  isRightToLeft  // 0xae
  public            System.Boolean                  extraPadding  // 0xaf
  public            System.Boolean                  parseControlCharacters  // 0xb0
  public            System.Single                   characterSpacing  // 0xb4
  public            System.Single                   wordSpacing  // 0xb8
  public            System.Single                   lineSpacing  // 0xbc
  public            System.Single                   paragraphSpacing  // 0xc0
  public            System.Single                   lineSpacingMax  // 0xc4
  public            System.Int32                    maxVisibleCharacters  // 0xc8
  public            System.Int32                    maxVisibleWords  // 0xcc
  public            System.Int32                    maxVisibleLines  // 0xd0
  public            System.Int32                    firstVisibleCharacter  // 0xd4
  public            System.Boolean                  useMaxVisibleDescender  // 0xd8
  public            UnityEngine.TextCore.Text.TextFontWeightfontWeight  // 0xdc
  public            System.Int32                    pageToDisplay  // 0xe0
  public            UnityEngine.TextCore.Text.TextureMappinghorizontalMapping  // 0xe4
  public            UnityEngine.TextCore.Text.TextureMappingverticalMapping  // 0xe8
  public            System.Single                   uvLineOffset  // 0xec
  public            UnityEngine.TextCore.Text.VertexSortingOrdergeometrySortingOrder  // 0xf0
  public            System.Boolean                  inverseYAxis  // 0xf4
  public            System.Single                   charWidthMaxAdj  // 0xf8
METHODS:
  RVA=0x0B453A48  token=0x60000E3  System.Boolean Equals(UnityEngine.TextCore.Text.TextGenerationSettings other)
  RVA=0x0B453E74  token=0x60000E4  System.Boolean Equals(System.Object obj)
  RVA=0x0B453F10  token=0x60000E5  System.Int32 GetHashCode()
  RVA=0x0B45441C  token=0x60000E6  System.String ToString()
  RVA=0x0B4556A0  token=0x60000E7  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.TextCore.Text.TextGenerator
TYPE:  class
TOKEN: 0x2000023
SIZE:  0xA58
FIELDS:
  private   static  UnityEngine.TextCore.Text.TextGenerators_TextGenerator  // static @ 0x0
  private           UnityEngine.Vector3[]           m_RectTransformCorners  // 0x10
  private           System.Single                   m_MarginWidth  // 0x18
  private           System.Single                   m_MarginHeight  // 0x1c
  private           System.Int32[]                  m_CharBuffer  // 0x20
  private           System.Single                   m_PreferredWidth  // 0x28
  private           System.Single                   m_PreferredHeight  // 0x2c
  private           UnityEngine.TextCore.Text.FontAssetm_CurrentFontAsset  // 0x30
  private           UnityEngine.Material            m_CurrentMaterial  // 0x38
  private           System.Int32                    m_CurrentMaterialIndex  // 0x40
  private           UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.TextCore.Text.MaterialReference>m_MaterialReferenceStack  // 0x48
  private           System.Single                   m_Padding  // 0xa0
  private           UnityEngine.TextCore.Text.SpriteAssetm_CurrentSpriteAsset  // 0xa8
  private           System.Int32                    m_TotalCharacterCount  // 0xb0
  private           System.Single                   m_FontScale  // 0xb4
  private           System.Single                   m_FontSize  // 0xb8
  private           System.Single                   m_FontScaleMultiplier  // 0xbc
  private           System.Single                   m_CurrentFontSize  // 0xc0
  private           UnityEngine.TextCore.Text.TextProcessingStack<System.Single>m_SizeStack  // 0xc8
  private           UnityEngine.TextCore.Text.FontStylesm_FontStyleInternal  // 0xe8
  private           UnityEngine.TextCore.Text.FontStyleStackm_FontStyleStack  // 0xec
  private           UnityEngine.TextCore.Text.TextFontWeightm_FontWeightInternal  // 0xf8
  private           UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.TextCore.Text.TextFontWeight>m_FontWeightStack  // 0x100
  private           UnityEngine.TextCore.Text.TextAlignmentm_LineJustification  // 0x120
  private           UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.TextCore.Text.TextAlignment>m_LineJustificationStack  // 0x128
  private           System.Single                   m_BaselineOffset  // 0x148
  private           UnityEngine.TextCore.Text.TextProcessingStack<System.Single>m_BaselineOffsetStack  // 0x150
  private           UnityEngine.Color32             m_FontColor32  // 0x170
  private           UnityEngine.Color32             m_HtmlColor  // 0x174
  private           UnityEngine.Color32             m_UnderlineColor  // 0x178
  private           UnityEngine.Color32             m_StrikethroughColor  // 0x17c
  private           UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.Color32>m_ColorStack  // 0x180
  private           UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.Color32>m_UnderlineColorStack  // 0x1a0
  private           UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.Color32>m_StrikethroughColorStack  // 0x1c0
  private           UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.Color32>m_HighlightColorStack  // 0x1e0
  private           UnityEngine.TextCore.Text.TextColorGradientm_ColorGradientPreset  // 0x200
  private           UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.TextCore.Text.TextColorGradient>m_ColorGradientStack  // 0x208
  private           UnityEngine.TextCore.Text.TextProcessingStack<System.Int32>m_ActionStack  // 0x230
  private           System.Boolean                  m_IsFxMatrixSet  // 0x250
  private           System.Single                   m_LineOffset  // 0x254
  private           System.Single                   m_LineHeight  // 0x258
  private           System.Single                   m_CSpacing  // 0x25c
  private           System.Single                   m_MonoSpacing  // 0x260
  private           System.Single                   m_XAdvance  // 0x264
  private           System.Single                   m_TagLineIndent  // 0x268
  private           System.Single                   m_TagIndent  // 0x26c
  private           UnityEngine.TextCore.Text.TextProcessingStack<System.Single>m_IndentStack  // 0x270
  private           System.Boolean                  m_TagNoParsing  // 0x290
  private           System.Int32                    m_CharacterCount  // 0x294
  private           System.Int32                    m_FirstCharacterOfLine  // 0x298
  private           System.Int32                    m_LastCharacterOfLine  // 0x29c
  private           System.Int32                    m_FirstVisibleCharacterOfLine  // 0x2a0
  private           System.Int32                    m_LastVisibleCharacterOfLine  // 0x2a4
  private           System.Single                   m_MaxLineAscender  // 0x2a8
  private           System.Single                   m_MaxLineDescender  // 0x2ac
  private           System.Int32                    m_LineNumber  // 0x2b0
  private           System.Int32                    m_LineVisibleCharacterCount  // 0x2b4
  private           System.Int32                    m_FirstOverflowCharacterIndex  // 0x2b8
  private           System.Int32                    m_PageNumber  // 0x2bc
  private           System.Single                   m_MarginLeft  // 0x2c0
  private           System.Single                   m_MarginRight  // 0x2c4
  private           System.Single                   m_Width  // 0x2c8
  private           UnityEngine.TextCore.Text.Extentsm_MeshExtents  // 0x2cc
  private           System.Single                   m_MaxCapHeight  // 0x2dc
  private           System.Single                   m_MaxAscender  // 0x2e0
  private           System.Single                   m_MaxDescender  // 0x2e4
  private           System.Boolean                  m_IsNewPage  // 0x2e8
  private           System.Boolean                  m_IsNonBreakingSpace  // 0x2e9
  private           UnityEngine.TextCore.Text.WordWrapStatem_SavedWordWrapState  // 0x2f0
  private           UnityEngine.TextCore.Text.WordWrapStatem_SavedLineState  // 0x600
  private           System.Int32                    m_LoopCountA  // 0x910
  private           UnityEngine.TextCore.Text.TextElementTypem_TextElementType  // 0x914
  private           System.Boolean                  m_IsParsingText  // 0x915
  private           System.Int32                    m_SpriteIndex  // 0x918
  private           UnityEngine.Color32             m_SpriteColor  // 0x91c
  private           UnityEngine.TextCore.Text.TextElementm_CachedTextElement  // 0x920
  private           UnityEngine.Color32             m_HighlightColor  // 0x928
  private           System.Single                   m_CharWidthAdjDelta  // 0x92c
  private           UnityEngine.Matrix4x4           m_FxMatrix  // 0x930
  private           System.Single                   m_MaxFontSize  // 0x970
  private           System.Single                   m_MinFontSize  // 0x974
  private           System.Boolean                  m_IsCharacterWrappingEnabled  // 0x978
  private           System.Single                   m_StartOfLineAscender  // 0x97c
  private           System.Single                   m_LineSpacingDelta  // 0x980
  private           System.Boolean                  m_IsMaskingEnabled  // 0x984
  private           UnityEngine.TextCore.Text.MaterialReference[]m_MaterialReferences  // 0x988
  private           System.Int32                    m_SpriteCount  // 0x990
  private           UnityEngine.TextCore.Text.TextProcessingStack<System.Int32>m_StyleStack  // 0x998
  private           System.Int32                    m_SpriteAnimationId  // 0x9b8
  private           System.UInt32[]                 m_InternalTextParsingBuffer  // 0x9c0
  private           UnityEngine.TextCore.Text.RichTextTagAttribute[]m_Attributes  // 0x9c8
  private           UnityEngine.TextCore.Text.XmlTagAttribute[]m_XmlAttribute  // 0x9d0
  private           System.Char[]                   m_RichTextTag  // 0x9d8
  private           System.Collections.Generic.Dictionary<System.Int32,System.Int32>m_MaterialReferenceIndexLookup  // 0x9e0
  private           System.Boolean                  m_IsCalculatingPreferredValues  // 0x9e8
  private           UnityEngine.TextCore.Text.SpriteAssetm_DefaultSpriteAsset  // 0x9f0
  private           System.Boolean                  m_TintSprite  // 0x9f8
  protected         UnityEngine.TextCore.Text.TextGenerator.SpecialCharacterm_Ellipsis  // 0xa00
  protected         UnityEngine.TextCore.Text.TextGenerator.SpecialCharacterm_Underline  // 0xa20
  private           System.Boolean                  m_IsUsingBold  // 0xa40
  private           System.Boolean                  m_IsSdfShader  // 0xa41
  private           UnityEngine.TextCore.Text.TextElementInfo[]m_InternalTextElementInfo  // 0xa48
  private           System.Int32                    m_RecursiveCount  // 0xa50
METHODS:
  RVA=0x0B4410F8  token=0x60000E8  UnityEngine.TextCore.Text.TextGenerator GetTextGenerator()
  RVA=0x0B4405A0  token=0x60000E9  System.Void GenerateText(UnityEngine.TextCore.Text.TextGenerationSettings settings, UnityEngine.TextCore.Text.TextInfo textInfo)
  RVA=0x0B4406E0  token=0x60000EA  UnityEngine.Vector2 GetCursorPosition(UnityEngine.TextCore.Text.TextInfo textInfo, UnityEngine.Rect screenRect, System.Int32 index, System.Boolean inverseYAxis)
  RVA=0x0B440C90  token=0x60000EB  UnityEngine.Vector2 GetPreferredValues(UnityEngine.TextCore.Text.TextGenerationSettings settings, UnityEngine.TextCore.Text.TextInfo textInfo)
  RVA=0x0B4412D0  token=0x60000EC  System.Void Prepare(UnityEngine.TextCore.Text.TextGenerationSettings generationSettings, UnityEngine.TextCore.Text.TextInfo textInfo)
  RVA=0x0B432DF0  token=0x60000ED  System.Void GenerateTextMesh(UnityEngine.TextCore.Text.TextGenerationSettings generationSettings, UnityEngine.TextCore.Text.TextInfo textInfo)
  RVA=0x0B4429B0  token=0x60000EE  System.Void SaveWordWrappingState(UnityEngine.TextCore.Text.WordWrapState& state, System.Int32 index, System.Int32 count, UnityEngine.TextCore.Text.TextInfo textInfo)
  RVA=0x0B441420  token=0x60000EF  System.Int32 RestoreWordWrappingState(UnityEngine.TextCore.Text.WordWrapState& state, UnityEngine.TextCore.Text.TextInfo textInfo)
  RVA=0x0B443F20  token=0x60000F0  System.Boolean ValidateHtmlTag(System.Int32[] chars, System.Int32 startIndex, System.Int32& endIndex, UnityEngine.TextCore.Text.TextGenerationSettings generationSettings, UnityEngine.TextCore.Text.TextInfo textInfo)
  RVA=0x0B441918  token=0x60000F1  System.Void SaveGlyphVertexInfo(System.Single padding, System.Single stylePadding, UnityEngine.Color32 vertexColor, UnityEngine.TextCore.Text.TextGenerationSettings generationSettings, UnityEngine.TextCore.Text.TextInfo textInfo)
  RVA=0x0B442218  token=0x60000F2  System.Void SaveSpriteVertexInfo(UnityEngine.Color32 vertexColor, UnityEngine.TextCore.Text.TextGenerationSettings generationSettings, UnityEngine.TextCore.Text.TextInfo textInfo)
  RVA=0x0B43F334  token=0x60000F3  System.Void DrawUnderlineMesh(UnityEngine.Vector3 start, UnityEngine.Vector3 end, System.Int32& index, System.Single startScale, System.Single endScale, System.Single maxScale, System.Single sdfScale, UnityEngine.Color32 underlineColor, UnityEngine.TextCore.Text.TextGenerationSettings generationSettings, UnityEngine.TextCore.Text.TextInfo textInfo)
  RVA=0x0B43ECE8  token=0x60000F4  System.Void DrawTextHighlight(UnityEngine.Vector3 start, UnityEngine.Vector3 end, System.Int32& index, UnityEngine.Color32 highlightColor, UnityEngine.TextCore.Text.TextGenerationSettings generationSettings, UnityEngine.TextCore.Text.TextInfo textInfo)
  RVA=0x0B43EBAC  token=0x60000F5  System.Void ClearMesh(System.Boolean updateMesh, UnityEngine.TextCore.Text.TextInfo textInfo)
  RVA=0x04DBD470  token=0x60000F6  System.Void EnableMasking()
  RVA=0x04DBD460  token=0x60000F7  System.Void DisableMasking()
  RVA=0x0B442E44  token=0x60000F8  System.Void SetArraySizes(System.Int32[] chars, UnityEngine.TextCore.Text.TextGenerationSettings generationSettings, UnityEngine.TextCore.Text.TextInfo textInfo)
  RVA=0x0B440DC4  token=0x60000F9  UnityEngine.TextCore.Text.TextElement GetTextElement(UnityEngine.TextCore.Text.TextGenerationSettings generationSettings, System.UInt32 unicode, UnityEngine.TextCore.Text.FontAsset fontAsset, UnityEngine.TextCore.Text.FontStyles fontStyle, UnityEngine.TextCore.Text.TextFontWeight fontWeight, System.Boolean& isUsingAlternativeTypeface)
  RVA=0x0B43EBD0  token=0x60000FA  System.Void ComputeMarginSize(UnityEngine.Rect rect, UnityEngine.Vector4 margins)
  RVA=0x0B440D94  token=0x60000FB  System.Void GetSpecialCharacters(UnityEngine.TextCore.Text.TextGenerationSettings generationSettings)
  RVA=0x0B440878  token=0x60000FC  System.Void GetEllipsisSpecialCharacter(UnityEngine.TextCore.Text.TextGenerationSettings generationSettings)
  RVA=0x0B441188  token=0x60000FD  System.Void GetUnderlineSpecialCharacter(UnityEngine.TextCore.Text.TextGenerationSettings generationSettings)
  RVA=0x0B440A74  token=0x60000FE  System.Single GetPaddingForMaterial(UnityEngine.Material material, System.Boolean extraPadding)
  RVA=0x0B440B74  token=0x60000FF  UnityEngine.Vector2 GetPreferredValuesInternal(UnityEngine.TextCore.Text.TextGenerationSettings generationSettings, UnityEngine.TextCore.Text.TextInfo textInfo)
  RVA=0x0B43CC48  token=0x6000100  UnityEngine.Vector2 CalculatePreferredValues(System.Single defaultFontSize, UnityEngine.Vector2 marginSize, System.Boolean ignoreTextAutoSizing, UnityEngine.TextCore.Text.TextGenerationSettings generationSettings, UnityEngine.TextCore.Text.TextInfo textInfo)
  RVA=0x0B448238  token=0x6000101  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.TextCore.Text.TextAlignment
TYPE:  sealed struct
TOKEN: 0x2000025
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.TextCore.Text.TextAlignmentTopLeft  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentTopCenter  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentTopRight  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentTopJustified  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentTopFlush  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentTopGeoAligned  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentMiddleLeft  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentMiddleCenter  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentMiddleRight  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentMiddleJustified  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentMiddleFlush  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentMiddleGeoAligned  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentBottomLeft  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentBottomCenter  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentBottomRight  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentBottomJustified  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentBottomFlush  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentBottomGeoAligned  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentBaselineLeft  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentBaselineCenter  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentBaselineRight  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentBaselineJustified  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentBaselineFlush  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentBaselineGeoAligned  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentMidlineLeft  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentMidlineCenter  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentMidlineRight  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentMidlineJustified  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentMidlineFlush  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentMidlineGeoAligned  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentCaplineLeft  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentCaplineCenter  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentCaplineRight  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentCaplineJustified  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentCaplineFlush  // const
  public    static  UnityEngine.TextCore.Text.TextAlignmentCaplineGeoAligned  // const
METHODS:
END_CLASS

CLASS: UnityEngine.TextCore.Text.FontStyles
TYPE:  sealed struct
TOKEN: 0x2000026
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.TextCore.Text.FontStylesNormal  // const
  public    static  UnityEngine.TextCore.Text.FontStylesBold  // const
  public    static  UnityEngine.TextCore.Text.FontStylesItalic  // const
  public    static  UnityEngine.TextCore.Text.FontStylesUnderline  // const
  public    static  UnityEngine.TextCore.Text.FontStylesLowerCase  // const
  public    static  UnityEngine.TextCore.Text.FontStylesUpperCase  // const
  public    static  UnityEngine.TextCore.Text.FontStylesSmallCaps  // const
  public    static  UnityEngine.TextCore.Text.FontStylesStrikethrough  // const
  public    static  UnityEngine.TextCore.Text.FontStylesSuperscript  // const
  public    static  UnityEngine.TextCore.Text.FontStylesSubscript  // const
  public    static  UnityEngine.TextCore.Text.FontStylesHighlight  // const
METHODS:
END_CLASS

CLASS: UnityEngine.TextCore.Text.XmlTagAttribute
TYPE:  sealed struct
TOKEN: 0x2000027
SIZE:  0x24
FIELDS:
  public            System.Int32                    nameHashCode  // 0x10
  public            UnityEngine.TextCore.Text.TagValueTypevalueType  // 0x14
  public            System.Int32                    valueStartIndex  // 0x18
  public            System.Int32                    valueLength  // 0x1c
  public            System.Int32                    valueHashCode  // 0x20
METHODS:
END_CLASS

CLASS: UnityEngine.TextCore.Text.RichTextTagAttribute
TYPE:  sealed struct
TOKEN: 0x2000028
SIZE:  0x24
FIELDS:
  public            System.Int32                    nameHashCode  // 0x10
  public            System.Int32                    valueHashCode  // 0x14
  public            UnityEngine.TextCore.Text.TagValueTypevalueType  // 0x18
  public            System.Int32                    valueStartIndex  // 0x1c
  public            System.Int32                    valueLength  // 0x20
METHODS:
END_CLASS

CLASS: UnityEngine.TextCore.Text.WordWrapState
TYPE:  sealed struct
TOKEN: 0x2000029
SIZE:  0x320
FIELDS:
  public            System.Int32                    previousWordBreak  // 0x10
  public            System.Int32                    totalCharacterCount  // 0x14
  public            System.Int32                    visibleCharacterCount  // 0x18
  public            System.Int32                    visibleSpriteCount  // 0x1c
  public            System.Int32                    visibleLinkCount  // 0x20
  public            System.Int32                    firstCharacterIndex  // 0x24
  public            System.Int32                    firstVisibleCharacterIndex  // 0x28
  public            System.Int32                    lastCharacterIndex  // 0x2c
  public            System.Int32                    lastVisibleCharIndex  // 0x30
  public            System.Int32                    lineNumber  // 0x34
  public            System.Single                   maxCapHeight  // 0x38
  public            System.Single                   maxAscender  // 0x3c
  public            System.Single                   maxDescender  // 0x40
  public            System.Single                   maxLineAscender  // 0x44
  public            System.Single                   maxLineDescender  // 0x48
  public            System.Single                   previousLineAscender  // 0x4c
  public            System.Single                   xAdvance  // 0x50
  public            System.Single                   preferredWidth  // 0x54
  public            System.Single                   preferredHeight  // 0x58
  public            System.Single                   previousLineScale  // 0x5c
  public            System.Int32                    wordCount  // 0x60
  public            UnityEngine.TextCore.Text.FontStylesfontStyle  // 0x64
  public            System.Single                   fontScale  // 0x68
  public            System.Single                   fontScaleMultiplier  // 0x6c
  public            System.Single                   currentFontSize  // 0x70
  public            System.Single                   baselineOffset  // 0x74
  public            System.Single                   lineOffset  // 0x78
  public            UnityEngine.TextCore.Text.TextInfotextInfo  // 0x80
  public            UnityEngine.TextCore.Text.LineInfolineInfo  // 0x88
  public            UnityEngine.Color32             vertexColor  // 0xe8
  public            UnityEngine.Color32             underlineColor  // 0xec
  public            UnityEngine.Color32             strikethroughColor  // 0xf0
  public            UnityEngine.Color32             highlightColor  // 0xf4
  public            UnityEngine.TextCore.Text.FontStyleStackbasicStyleStack  // 0xf8
  public            UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.Color32>colorStack  // 0x108
  public            UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.Color32>underlineColorStack  // 0x128
  public            UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.Color32>strikethroughColorStack  // 0x148
  public            UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.Color32>highlightColorStack  // 0x168
  public            UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.TextCore.Text.TextColorGradient>colorGradientStack  // 0x188
  public            UnityEngine.TextCore.Text.TextProcessingStack<System.Single>sizeStack  // 0x1b0
  public            UnityEngine.TextCore.Text.TextProcessingStack<System.Single>indentStack  // 0x1d0
  public            UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.TextCore.Text.TextFontWeight>fontWeightStack  // 0x1f0
  public            UnityEngine.TextCore.Text.TextProcessingStack<System.Int32>styleStack  // 0x210
  public            UnityEngine.TextCore.Text.TextProcessingStack<System.Single>baselineStack  // 0x230
  public            UnityEngine.TextCore.Text.TextProcessingStack<System.Int32>actionStack  // 0x250
  public            UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.TextCore.Text.MaterialReference>materialReferenceStack  // 0x270
  public            UnityEngine.TextCore.Text.TextProcessingStack<UnityEngine.TextCore.Text.TextAlignment>lineJustificationStack  // 0x2c8
  public            System.Int32                    spriteAnimationId  // 0x2e8
  public            UnityEngine.TextCore.Text.FontAssetcurrentFontAsset  // 0x2f0
  public            UnityEngine.TextCore.Text.SpriteAssetcurrentSpriteAsset  // 0x2f8
  public            UnityEngine.Material            currentMaterial  // 0x300
  public            System.Int32                    currentMaterialIndex  // 0x308
  public            UnityEngine.TextCore.Text.ExtentsmeshExtents  // 0x30c
  public            System.Boolean                  tagNoParsing  // 0x31c
  public            System.Boolean                  isNonBreakingSpace  // 0x31d
METHODS:
END_CLASS

CLASS: UnityEngine.TextCore.Text.TextGeneratorUtilities
TYPE:  static class
TOKEN: 0x200002A
SIZE:  0x10
FIELDS:
  public    static readonly UnityEngine.Vector2             largePositiveVector2  // static @ 0x0
  public    static readonly UnityEngine.Vector2             largeNegativeVector2  // static @ 0x8
METHODS:
  RVA=0x0B318C88  token=0x6000103  System.Boolean Approximately(System.Single a, System.Single b)
  RVA=0x0B4572C0  token=0x6000104  UnityEngine.Color32 HexCharsToColor(System.Char[] hexChars, System.Int32 tagCount)
  RVA=0x0B457920  token=0x6000105  UnityEngine.Color32 HexCharsToColor(System.Char[] hexChars, System.Int32 startIndex, System.Int32 length)
  RVA=0x0B457B88  token=0x6000106  System.Int32 HexToInt(System.Char hex)
  RVA=0x0B455CA4  token=0x6000107  System.Single ConvertToFloat(System.Char[] chars, System.Int32 startIndex, System.Int32 length)
  RVA=0x0B455B54  token=0x6000108  System.Single ConvertToFloat(System.Char[] chars, System.Int32 startIndex, System.Int32 length, System.Int32& lastIndex)
  RVA=0x04DBD680  token=0x6000109  UnityEngine.Vector2 PackUV(System.Single x, System.Single y, System.Single scale)
  RVA=0x0B458938  token=0x600010A  System.Void StringToCharArray(System.String sourceText, System.Int32[]& charBuffer, UnityEngine.TextCore.Text.TextProcessingStack<System.Int32>& styleStack, UnityEngine.TextCore.Text.TextGenerationSettings generationSettings)
  RVA=-1  // generic def  token=0x600010B  System.Void ResizeInternalArray(T[]& array)
  RVA=0x0B457CC0  token=0x600010C  System.Boolean IsTagName(System.String& text, System.String tag, System.Int32 index)
  RVA=0x0B457D64  token=0x600010D  System.Boolean IsTagName(System.Int32[]& text, System.String tag, System.Int32 index)
  RVA=0x0B45816C  token=0x600010E  System.Boolean ReplaceOpeningStyleTag(System.Int32[]& sourceText, System.Int32 srcIndex, System.Int32& srcOffset, System.Int32[]& charBuffer, System.Int32& writeIndex, UnityEngine.TextCore.Text.TextProcessingStack<System.Int32>& styleStack, UnityEngine.TextCore.Text.TextGenerationSettings& generationSettings)
  RVA=0x0B458464  token=0x600010F  System.Boolean ReplaceOpeningStyleTag(System.String& sourceText, System.Int32 srcIndex, System.Int32& srcOffset, System.Int32[]& charBuffer, System.Int32& writeIndex, UnityEngine.TextCore.Text.TextProcessingStack<System.Int32>& styleStack, UnityEngine.TextCore.Text.TextGenerationSettings& generationSettings)
  RVA=0x0B457E9C  token=0x6000110  System.Void ReplaceClosingStyleTag(System.Int32[]& charBuffer, System.Int32& writeIndex, UnityEngine.TextCore.Text.TextProcessingStack<System.Int32>& styleStack, UnityEngine.TextCore.Text.TextGenerationSettings& generationSettings)
  RVA=0x0B456E94  token=0x6000111  UnityEngine.TextCore.Text.TextStyle GetStyle(UnityEngine.TextCore.Text.TextGenerationSettings generationSetting, System.Int32 hashCode)
  RVA=0x0B45716C  token=0x6000112  System.Int32 GetUtf32(System.String text, System.Int32 i)
  RVA=0x0B457094  token=0x6000113  System.Int32 GetUtf16(System.String text, System.Int32 i)
  RVA=0x0B45700C  token=0x6000114  System.Int32 GetTagHashCode(System.Int32[]& text, System.Int32 index, System.Int32& closeIndex)
  RVA=0x0B456F6C  token=0x6000115  System.Int32 GetTagHashCode(System.String& text, System.Int32 index, System.Int32& closeIndex)
  RVA=0x0B455D10  token=0x6000116  System.Void FillCharacterVertexBuffers(System.Int32 i, UnityEngine.TextCore.Text.TextGenerationSettings generationSettings, UnityEngine.TextCore.Text.TextInfo textInfo)
  RVA=0x0B456618  token=0x6000117  System.Void FillSpriteVertexBuffers(System.Int32 i, UnityEngine.TextCore.Text.TextGenerationSettings generationSettings, UnityEngine.TextCore.Text.TextInfo textInfo)
  RVA=0x0B455730  token=0x6000118  System.Void AdjustLineOffset(System.Int32 startIndex, System.Int32 endIndex, System.Single offset, UnityEngine.TextCore.Text.TextInfo textInfo)
  RVA=0x0B458760  token=0x6000119  System.Void ResizeLineExtents(System.Int32 size, UnityEngine.TextCore.Text.TextInfo textInfo)
  RVA=0x0B457E78  token=0x600011A  UnityEngine.TextCore.Text.FontStyles LegacyStyleToNewStyle(UnityEngine.FontStyle fontStyle)
  RVA=0x0B457E14  token=0x600011B  UnityEngine.TextCore.Text.TextAlignment LegacyAlignmentToNewAlignment(UnityEngine.TextAnchor anchor)
  RVA=0x0B459154  token=0x600011C  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.TextCore.Text.PageInfo
TYPE:  sealed struct
TOKEN: 0x200002B
SIZE:  0x24
FIELDS:
  public            System.Int32                    firstCharacterIndex  // 0x10
  public            System.Int32                    lastCharacterIndex  // 0x14
  public            System.Single                   ascender  // 0x18
  public            System.Single                   baseLine  // 0x1c
  public            System.Single                   descender  // 0x20
METHODS:
END_CLASS

CLASS: UnityEngine.TextCore.Text.WordInfo
TYPE:  sealed struct
TOKEN: 0x200002C
SIZE:  0x1C
FIELDS:
  public            System.Int32                    firstCharacterIndex  // 0x10
  public            System.Int32                    lastCharacterIndex  // 0x14
  public            System.Int32                    characterCount  // 0x18
METHODS:
END_CLASS

CLASS: UnityEngine.TextCore.Text.TextInfo
TYPE:  class
TOKEN: 0x200002D
SIZE:  0x68
FIELDS:
  private   static  UnityEngine.Vector2             s_InfinityVectorPositive  // static @ 0x0
  private   static  UnityEngine.Vector2             s_InfinityVectorNegative  // static @ 0x8
  public            System.Int32                    characterCount  // 0x10
  public            System.Int32                    spriteCount  // 0x14
  public            System.Int32                    spaceCount  // 0x18
  public            System.Int32                    wordCount  // 0x1c
  public            System.Int32                    linkCount  // 0x20
  public            System.Int32                    lineCount  // 0x24
  public            System.Int32                    pageCount  // 0x28
  public            System.Int32                    materialCount  // 0x2c
  public            UnityEngine.TextCore.Text.TextElementInfo[]textElementInfo  // 0x30
  public            UnityEngine.TextCore.Text.WordInfo[]wordInfo  // 0x38
  public            UnityEngine.TextCore.Text.LinkInfo[]linkInfo  // 0x40
  public            UnityEngine.TextCore.Text.LineInfo[]lineInfo  // 0x48
  public            UnityEngine.TextCore.Text.PageInfo[]pageInfo  // 0x50
  public            UnityEngine.TextCore.Text.MeshInfo[]meshInfo  // 0x58
  public            System.Boolean                  isDirty  // 0x60
METHODS:
  RVA=0x0B459884  token=0x600011D  System.Void .ctor()
  RVA=0x0B4597CC  token=0x600011E  System.Void Clear()
  RVA=0x0B45973C  token=0x600011F  System.Void ClearMeshInfo(System.Boolean updateMesh)
  RVA=0x0B4594FC  token=0x6000120  System.Void ClearLineInfo()
  RVA=-1  // generic def  token=0x6000121  System.Void Resize(T[]& array, System.Int32 size)
  RVA=-1  // generic def  token=0x6000122  System.Void Resize(T[]& array, System.Int32 size, System.Boolean isBlockAllocated)
  RVA=0x0B459828  token=0x6000123  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.TextCore.Text.MarkupTag
TYPE:  sealed struct
TOKEN: 0x200002E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.TextCore.Text.MarkupTagBOLD  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_BOLD  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagITALIC  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_ITALIC  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagUNDERLINE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_UNDERLINE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSTRIKETHROUGH  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_STRIKETHROUGH  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagMARK  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_MARK  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSUBSCRIPT  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_SUBSCRIPT  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSUPERSCRIPT  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_SUPERSCRIPT  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagCOLOR  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_COLOR  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagALPHA  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagA  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_A  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSIZE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_SIZE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSPRITE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagNO_BREAK  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_NO_BREAK  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSTYLE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_STYLE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagFONT  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_FONT  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_MATERIAL  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagLINK  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_LINK  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagFONT_WEIGHT  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_FONT_WEIGHT  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagNO_PARSE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_NO_PARSE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagPOSITION  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_POSITION  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagVERTICAL_OFFSET  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_VERTICAL_OFFSET  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSPACE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_SPACE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagPAGE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_PAGE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagALIGN  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_ALIGN  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagWIDTH  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_WIDTH  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagGRADIENT  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_GRADIENT  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagCHARACTER_SPACE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_CHARACTER_SPACE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagMONOSPACE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_MONOSPACE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagCLASS  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagINDENT  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_INDENT  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagLINE_INDENT  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_LINE_INDENT  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagMARGIN  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_MARGIN  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagMARGIN_LEFT  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagMARGIN_RIGHT  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagLINE_HEIGHT  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_LINE_HEIGHT  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagACTION  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_ACTION  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSCALE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_SCALE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagROTATE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_ROTATE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagLOWERCASE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_LOWERCASE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagALLCAPS  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_ALLCAPS  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagUPPERCASE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_UPPERCASE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSMALLCAPS  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_SMALLCAPS  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagLIGA  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_LIGA  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagFRAC  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSLASH_FRAC  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagNAME  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagINDEX  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagTINT  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagANIM  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagMATERIAL  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagHREF  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagANGLE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagRED  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagGREEN  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagBLUE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagYELLOW  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagORANGE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagBLACK  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagWHITE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagPURPLE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagBR  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagZWSP  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagNBSP  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagSHY  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagLEFT  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagRIGHT  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagCENTER  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagJUSTIFIED  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagFLUSH  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagNONE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagPLUS  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagMINUS  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagPX  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagPLUS_PX  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagMINUS_PX  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagEM  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagPLUS_EM  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagMINUS_EM  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagPCT  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagPLUS_PCT  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagMINUS_PCT  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagPERCENTAGE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagPLUS_PERCENTAGE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagMINUS_PERCENTAGE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagTRUE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagFALSE  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagINVALID  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagNORMAL  // const
  public    static  UnityEngine.TextCore.Text.MarkupTagDEFAULT  // const
METHODS:
END_CLASS

CLASS: UnityEngine.TextCore.Text.TagValueType
TYPE:  sealed struct
TOKEN: 0x200002F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.TextCore.Text.TagValueTypeNone  // const
  public    static  UnityEngine.TextCore.Text.TagValueTypeNumericalValue  // const
  public    static  UnityEngine.TextCore.Text.TagValueTypeStringValue  // const
  public    static  UnityEngine.TextCore.Text.TagValueTypeColorValue  // const
METHODS:
END_CLASS

CLASS: UnityEngine.TextCore.Text.TagUnitType
TYPE:  sealed struct
TOKEN: 0x2000030
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.TextCore.Text.TagUnitTypePixels  // const
  public    static  UnityEngine.TextCore.Text.TagUnitTypeFontUnits  // const
  public    static  UnityEngine.TextCore.Text.TagUnitTypePercentage  // const
METHODS:
END_CLASS

CLASS: UnityEngine.TextCore.Text.FontStyleStack
TYPE:  sealed struct
TOKEN: 0x2000031
SIZE:  0x1A
FIELDS:
  public            System.Byte                     bold  // 0x10
  public            System.Byte                     italic  // 0x11
  public            System.Byte                     underline  // 0x12
  public            System.Byte                     strikethrough  // 0x13
  public            System.Byte                     highlight  // 0x14
  public            System.Byte                     superscript  // 0x15
  public            System.Byte                     subscript  // 0x16
  public            System.Byte                     uppercase  // 0x17
  public            System.Byte                     lowercase  // 0x18
  public            System.Byte                     smallcaps  // 0x19
METHODS:
  RVA=0x0333C3A0  token=0x6000124  System.Void Clear()
  RVA=0x0B45930C  token=0x6000125  System.Byte Add(UnityEngine.TextCore.Text.FontStyles style)
  RVA=0x0B4593D8  token=0x6000126  System.Byte Remove(UnityEngine.TextCore.Text.FontStyles style)
END_CLASS

CLASS: UnityEngine.TextCore.Text.TextProcessingStack`1
TYPE:  sealed struct
TOKEN: 0x2000032
FIELDS:
  public            T[]                             itemStack  // 0x0
  public            System.Int32                    index  // 0x0
  private           T                               m_DefaultItem  // 0x0
  private           System.Int32                    m_Capacity  // 0x0
  private           System.Int32                    m_RolloverSize  // 0x0
  private           System.Int32                    m_Count  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000127  System.Void .ctor(T[] stack)
  RVA=-1  // not resolved  token=0x6000128  System.Void .ctor(System.Int32 capacity)
  RVA=-1  // not resolved  token=0x6000129  System.Void Clear()
  RVA=-1  // not resolved  token=0x600012A  System.Void SetDefault(T item)
  RVA=-1  // not resolved  token=0x600012B  System.Void Add(T item)
  RVA=-1  // not resolved  token=0x600012C  T Remove()
  RVA=-1  // not resolved  token=0x600012D  System.Void Push(T item)
  RVA=-1  // not resolved  token=0x600012E  T Pop()
  RVA=-1  // not resolved  token=0x600012F  T Peek()
  RVA=-1  // not resolved  token=0x6000130  T CurrentItem()
END_CLASS

CLASS: UnityEngine.TextCore.Text.TextResourceManager
TYPE:  class
TOKEN: 0x2000033
SIZE:  0x10
FIELDS:
  private   static readonly System.Collections.Generic.Dictionary<System.Int32,UnityEngine.TextCore.Text.TextResourceManager.FontAssetRef>s_FontAssetReferences  // static @ 0x0
  private   static readonly System.Collections.Generic.Dictionary<System.Int32,UnityEngine.TextCore.Text.FontAsset>s_FontAssetNameReferenceLookup  // static @ 0x8
  private   static readonly System.Collections.Generic.Dictionary<System.Int64,UnityEngine.TextCore.Text.FontAsset>s_FontAssetFamilyNameAndStyleReferenceLookup  // static @ 0x10
  private   static readonly System.Collections.Generic.List<System.Int32>s_FontAssetRemovalList  // static @ 0x18
  private   static readonly System.Int32                    k_RegularStyleHashCode  // static @ 0x20
METHODS:
  RVA=0x0B4599AC  token=0x6000131  System.Void AddFontAsset(UnityEngine.TextCore.Text.FontAsset fontAsset)
  RVA=0x0B459F1C  token=0x6000132  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.TextCore.Text.TextSettings
TYPE:  class
TOKEN: 0x2000035
SIZE:  0xA0
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
  protected         System.String                   m_Version  // 0x18
  protected         UnityEngine.TextCore.Text.FontAssetm_DefaultFontAsset  // 0x20
  protected         System.String                   m_DefaultFontAssetPath  // 0x28
  protected         System.Collections.Generic.List<UnityEngine.TextCore.Text.FontAsset>m_FallbackFontAssets  // 0x30
  protected         System.Boolean                  m_MatchMaterialPreset  // 0x38
  protected         System.Int32                    m_MissingCharacterUnicode  // 0x3c
  protected         System.Boolean                  m_ClearDynamicDataOnBuild  // 0x40
  protected         UnityEngine.TextCore.Text.SpriteAssetm_DefaultSpriteAsset  // 0x48
  protected         System.String                   m_DefaultSpriteAssetPath  // 0x50
  protected         System.Collections.Generic.List<UnityEngine.TextCore.Text.SpriteAsset>m_FallbackSpriteAssets  // 0x58
  protected         System.UInt32                   m_MissingSpriteCharacterUnicode  // 0x60
  protected         UnityEngine.TextCore.Text.TextStyleSheetm_DefaultStyleSheet  // 0x68
  protected         System.String                   m_StyleSheetsResourcePath  // 0x70
  protected         System.String                   m_DefaultColorGradientPresetsPath  // 0x78
  protected         UnityEngine.TextCore.Text.UnicodeLineBreakingRulesm_UnicodeLineBreakingRules  // 0x80
  protected         System.Boolean                  m_DisplayWarnings  // 0x88
  private           System.Collections.Generic.Dictionary<System.Int32,UnityEngine.TextCore.Text.FontAsset>m_FontLookup  // 0x90
  private           System.Collections.Generic.List<UnityEngine.TextCore.Text.TextSettings.FontReferenceMap>m_FontReferences  // 0x98
PROPERTIES:
  version  get=0x0385B100  set=0x05392C40
  defaultFontAsset  get=0x04D862C0  set=0x05390F40
  defaultFontAssetPath  get=0x04D86240  set=0x02FDB880
  fallbackFontAssets  get=0x011F36E0  set=0x05396200
  matchMaterialPreset  get=0x011F2A70  set=0x011F2A80
  missingCharacterUnicode  get=0x04D86640  set=0x04D86670
  clearDynamicDataOnBuild  get=0x02D480A0  set=0x02D480B0
  defaultSpriteAsset  get=0x04D86200  set=0x035A41A0
  defaultSpriteAssetPath  get=0x04D86270  set=0x02FDAEA0
  fallbackSpriteAssets  get=0x02E56440  set=0x036E5590
  missingSpriteCharacterUnicode  get=0x04D86790  set=0x04D86D50
  defaultStyleSheet  get=0x04D86230  set=0x0485F170
  styleSheetsResourcePath  get=0x04D862B0  set=0x063AD760
  defaultColorGradientPresetsPath  get=0x04D862A0  set=0x06402320
  lineBreakingRules  get=0x0B45A71C  set=0x04CD9970
  displayWarnings  get=0x011F2750  set=0x011F2760
METHODS:
  RVA=0x0B45A414  token=0x6000154  System.Void InitializeFontReferenceLookup()
  RVA=0x0B45A114  token=0x6000155  UnityEngine.TextCore.Text.FontAsset GetCachedFontAssetInternal(UnityEngine.Font font)
  RVA=0x0B45A60C  token=0x6000156  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.TextCore.Text.TextShaderUtilities
TYPE:  static class
TOKEN: 0x2000037
SIZE:  0x10
FIELDS:
  public    static  System.Int32                    ID_MainTex  // static @ 0x0
  public    static  System.Int32                    ID_FaceTex  // static @ 0x4
  public    static  System.Int32                    ID_FaceColor  // static @ 0x8
  public    static  System.Int32                    ID_FaceDilate  // static @ 0xc
  public    static  System.Int32                    ID_Shininess  // static @ 0x10
  public    static  System.Int32                    ID_UnderlayColor  // static @ 0x14
  public    static  System.Int32                    ID_UnderlayOffsetX  // static @ 0x18
  public    static  System.Int32                    ID_UnderlayOffsetY  // static @ 0x1c
  public    static  System.Int32                    ID_UnderlayDilate  // static @ 0x20
  public    static  System.Int32                    ID_UnderlaySoftness  // static @ 0x24
  public    static  System.Int32                    ID_WeightNormal  // static @ 0x28
  public    static  System.Int32                    ID_WeightBold  // static @ 0x2c
  public    static  System.Int32                    ID_OutlineTex  // static @ 0x30
  public    static  System.Int32                    ID_OutlineWidth  // static @ 0x34
  public    static  System.Int32                    ID_OutlineSoftness  // static @ 0x38
  public    static  System.Int32                    ID_OutlineColor  // static @ 0x3c
  public    static  System.Int32                    ID_Outline2Color  // static @ 0x40
  public    static  System.Int32                    ID_Outline2Width  // static @ 0x44
  public    static  System.Int32                    ID_Padding  // static @ 0x48
  public    static  System.Int32                    ID_GradientScale  // static @ 0x4c
  public    static  System.Int32                    ID_ScaleX  // static @ 0x50
  public    static  System.Int32                    ID_ScaleY  // static @ 0x54
  public    static  System.Int32                    ID_PerspectiveFilter  // static @ 0x58
  public    static  System.Int32                    ID_Sharpness  // static @ 0x5c
  public    static  System.Int32                    ID_TextureWidth  // static @ 0x60
  public    static  System.Int32                    ID_TextureHeight  // static @ 0x64
  public    static  System.Int32                    ID_BevelAmount  // static @ 0x68
  public    static  System.Int32                    ID_GlowColor  // static @ 0x6c
  public    static  System.Int32                    ID_GlowOffset  // static @ 0x70
  public    static  System.Int32                    ID_GlowPower  // static @ 0x74
  public    static  System.Int32                    ID_GlowOuter  // static @ 0x78
  public    static  System.Int32                    ID_GlowInner  // static @ 0x7c
  public    static  System.Int32                    ID_LightAngle  // static @ 0x80
  public    static  System.Int32                    ID_EnvMap  // static @ 0x84
  public    static  System.Int32                    ID_EnvMatrix  // static @ 0x88
  public    static  System.Int32                    ID_EnvMatrixRotation  // static @ 0x8c
  public    static  System.Int32                    ID_MaskCoord  // static @ 0x90
  public    static  System.Int32                    ID_ClipRect  // static @ 0x94
  public    static  System.Int32                    ID_MaskSoftnessX  // static @ 0x98
  public    static  System.Int32                    ID_MaskSoftnessY  // static @ 0x9c
  public    static  System.Int32                    ID_VertexOffsetX  // static @ 0xa0
  public    static  System.Int32                    ID_VertexOffsetY  // static @ 0xa4
  public    static  System.Int32                    ID_UseClipRect  // static @ 0xa8
  public    static  System.Int32                    ID_StencilID  // static @ 0xac
  public    static  System.Int32                    ID_StencilOp  // static @ 0xb0
  public    static  System.Int32                    ID_StencilComp  // static @ 0xb4
  public    static  System.Int32                    ID_StencilReadMask  // static @ 0xb8
  public    static  System.Int32                    ID_StencilWriteMask  // static @ 0xbc
  public    static  System.Int32                    ID_ShaderFlags  // static @ 0xc0
  public    static  System.Int32                    ID_ScaleRatio_A  // static @ 0xc4
  public    static  System.Int32                    ID_ScaleRatio_B  // static @ 0xc8
  public    static  System.Int32                    ID_ScaleRatio_C  // static @ 0xcc
  public    static  System.String                   Keyword_Bevel  // static @ 0xd0
  public    static  System.String                   Keyword_Glow  // static @ 0xd8
  public    static  System.String                   Keyword_Underlay  // static @ 0xe0
  public    static  System.String                   Keyword_Ratios  // static @ 0xe8
  public    static  System.String                   Keyword_MASK_SOFT  // static @ 0xf0
  public    static  System.String                   Keyword_MASK_HARD  // static @ 0xf8
  public    static  System.String                   Keyword_MASK_TEX  // static @ 0x100
  public    static  System.String                   Keyword_Outline  // static @ 0x108
  public    static  System.String                   ShaderTag_ZTestMode  // static @ 0x110
  public    static  System.String                   ShaderTag_CullMode  // static @ 0x118
  private   static  System.Single                   m_clamp  // static @ 0x120
  public    static  System.Boolean                  isInitialized  // static @ 0x124
  private   static  UnityEngine.Shader              k_ShaderRef_MobileSDF  // static @ 0x128
  private   static  UnityEngine.Shader              k_ShaderRef_MobileBitmap  // static @ 0x130
PROPERTIES:
  ShaderRef_MobileSDF  get=0x0B45C3C8
  ShaderRef_MobileBitmap  get=0x0B45C24C
METHODS:
  RVA=0x0B45BF5C  token=0x600015A  System.Void .cctor()
  RVA=0x0B45AF50  token=0x600015B  System.Void GetShaderPropertyIDs()
  RVA=0x0B45BA3C  token=0x600015C  System.Void UpdateShaderRatios(UnityEngine.Material mat)
  RVA=0x0B45B8B4  token=0x600015D  System.Boolean IsMaskingEnabled(UnityEngine.Material material)
  RVA=0x0B45A798  token=0x600015E  System.Single GetPadding(UnityEngine.Material material, System.Boolean enableExtraPadding, System.Boolean isBold)
END_CLASS

CLASS: UnityEngine.TextCore.Text.TextStyle
TYPE:  class
TOKEN: 0x2000038
SIZE:  0x50
FIELDS:
  private   static  UnityEngine.TextCore.Text.TextStylek_NormalStyle  // static @ 0x0
  private           System.String                   m_Name  // 0x10
  private           System.Int32                    m_HashCode  // 0x18
  private           System.String                   m_OpeningDefinition  // 0x20
  private           System.String                   m_ClosingDefinition  // 0x28
  private           System.Int32[]                  m_OpeningTagArray  // 0x30
  private           System.Int32[]                  m_ClosingTagArray  // 0x38
  private           System.UInt32[]                 m_OpeningTagUnicodeArray  // 0x40
  private           System.UInt32[]                 m_ClosingTagUnicodeArray  // 0x48
PROPERTIES:
  hashCode  get=0x04D9D860
  styleOpeningTagArray  get=0x04DBBEB0
  styleClosingTagArray  get=0x04DBC0B0
METHODS:
  RVA=0x0B45C888  token=0x6000162  System.Void RefreshStyle()
END_CLASS

CLASS: UnityEngine.TextCore.Text.TextStyleSheet
TYPE:  class
TOKEN: 0x2000039
SIZE:  0x28
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
  private           System.Collections.Generic.List<UnityEngine.TextCore.Text.TextStyle>m_StyleList  // 0x18
  private           System.Collections.Generic.Dictionary<System.Int32,UnityEngine.TextCore.Text.TextStyle>m_StyleLookupDictionary  // 0x20
PROPERTIES:
  styles  get=0x04D96330
METHODS:
  RVA=0x0B45C544  token=0x6000164  UnityEngine.TextCore.Text.TextStyle GetStyle(System.Int32 hashCode)
  RVA=0x0B45C5C0  token=0x6000165  UnityEngine.TextCore.Text.TextStyle GetStyle(System.String name)
  RVA=0x0B45C7FC  token=0x6000166  System.Void RefreshStyles()
  RVA=0x0B45C644  token=0x6000167  System.Void LoadStyleDictionaryInternal()
  RVA=0x0B45C804  token=0x6000168  System.Void .ctor()
END_CLASS

CLASS: UnityEngine.TextCore.Text.TextUtilities
TYPE:  static class
TOKEN: 0x200003A
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B45CB28  token=0x6000169  System.Char ToUpperFast(System.Char c)
  RVA=0x0B45CAD0  token=0x600016A  System.UInt32 ToUpperASCIIFast(System.UInt32 c)
  RVA=0x0B45CA74  token=0x600016B  System.Int32 GetHashCodeCaseInSensitive(System.String s)
  RVA=0x0B45CB80  token=0x600016C  System.String UintToString(System.Collections.Generic.List<System.UInt32> unicodes)
END_CLASS

CLASS: UnityEngine.TextCore.Text.UnicodeLineBreakingRules
TYPE:  class
TOKEN: 0x200003B
SIZE:  0x30
FIELDS:
  private   static  UnityEngine.TextCore.Text.UnicodeLineBreakingRuless_Instance  // static @ 0x0
  private           UnityEngine.TextAsset           m_UnicodeLineBreakingRules  // 0x10
  private           UnityEngine.TextAsset           m_LeadingCharacters  // 0x18
  private           UnityEngine.TextAsset           m_FollowingCharacters  // 0x20
  private           System.Boolean                  m_UseModernHangulLineBreakingRules  // 0x28
  private   static  System.Collections.Generic.HashSet<System.UInt32>s_LeadingCharactersLookup  // static @ 0x8
  private   static  System.Collections.Generic.HashSet<System.UInt32>s_FollowingCharactersLookup  // static @ 0x10
PROPERTIES:
  leadingCharactersLookup  get=0x0B45D114
  followingCharactersLookup  get=0x0B45D0A4
METHODS:
  RVA=0x0B45CCF4  token=0x600016F  System.Void LoadLineBreakingRules()
  RVA=0x0B45CC30  token=0x6000170  System.Collections.Generic.HashSet<System.UInt32> GetCharacters(UnityEngine.TextAsset file)
  RVA=0x041E1670  token=0x6000171  System.Void .ctor()
  RVA=0x0B45D040  token=0x6000172  System.Void .cctor()
END_CLASS

