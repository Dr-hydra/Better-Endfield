// ========================================================
// Dumped by @desirepro
// Assembly: Sirenix.Utilities.dll
// Classes:  13
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

CLASS: Sirenix.Utilities.LinqExtensions
TYPE:  static class
TOKEN: 0x2000002
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000001  System.Void Sort(System.Collections.Generic.IList<T> list, System.Comparison<T> comparison)
END_CLASS

CLASS: Sirenix.Utilities.TypeExtensions
TYPE:  static class
TOKEN: 0x2000003
SIZE:  0x10
FIELDS:
  private   static readonly System.Func<System.Single,System.Single,System.Boolean>FloatEqualityComparerFunc  // static @ 0x0
  private   static readonly System.Func<System.Double,System.Double,System.Boolean>DoubleEqualityComparerFunc  // static @ 0x8
  private   static readonly System.Func<UnityEngine.Quaternion,UnityEngine.Quaternion,System.Boolean>QuaternionEqualityComparerFunc  // static @ 0x10
  private   static readonly System.Object                   GenericConstraintsSatisfaction_LOCK  // static @ 0x18
  private   static readonly System.Collections.Generic.Dictionary<System.Type,System.Type>GenericConstraintsSatisfactionInferredParameters  // static @ 0x20
  private   static readonly System.Collections.Generic.Dictionary<System.Type,System.Type>GenericConstraintsSatisfactionResolvedMap  // static @ 0x28
  private   static readonly System.Collections.Generic.HashSet<System.Type>GenericConstraintsSatisfactionProcessedParams  // static @ 0x30
  private   static readonly System.Collections.Generic.HashSet<System.Type>GenericConstraintsSatisfactionTypesToCheck  // static @ 0x38
  private   static readonly System.Collections.Generic.List<System.Type>GenericConstraintsSatisfactionTypesToCheck_ToAdd  // static @ 0x40
  private   static readonly System.Type                     GenericListInterface  // static @ 0x48
  private   static readonly System.Type                     GenericCollectionInterface  // static @ 0x50
  private   static readonly System.Object                   WeaklyTypedTypeCastDelegates_LOCK  // static @ 0x58
  private   static readonly System.Object                   StronglyTypedTypeCastDelegates_LOCK  // static @ 0x60
  private   static readonly Sirenix.Utilities.DoubleLookupDictionary<System.Type,System.Type,System.Func<System.Object,System.Object>>WeaklyTypedTypeCastDelegates  // static @ 0x68
  private   static readonly Sirenix.Utilities.DoubleLookupDictionary<System.Type,System.Type,System.Delegate>StronglyTypedTypeCastDelegates  // static @ 0x70
  private   static readonly System.Type[]                   TwoLengthTypeArray_Cached  // static @ 0x78
  private   static readonly System.Collections.Generic.Stack<System.Type>GenericArgumentsContainsTypes_ArgsToCheckCached  // static @ 0x80
  private   static  System.Collections.Generic.HashSet<System.String>ReservedCSharpKeywords  // static @ 0x88
  public    static readonly System.Collections.Generic.Dictionary<System.Type,System.String>TypeNameAlternatives  // static @ 0x90
  private   static readonly System.Object                   CachedNiceNames_LOCK  // static @ 0x98
  private   static readonly System.Collections.Generic.Dictionary<System.Type,System.String>CachedNiceNames  // static @ 0xa0
  private   static readonly System.Type                     VoidPointerType  // static @ 0xa8
  private   static readonly System.Collections.Generic.Dictionary<System.Type,System.Collections.Generic.HashSet<System.Type>>PrimitiveImplicitCasts  // static @ 0xb0
  private   static readonly System.Collections.Generic.HashSet<System.Type>ExplicitCastIntegrals  // static @ 0xb8
METHODS:
  RVA=0x0AC47D40  token=0x6000002  System.String GetCachedNiceName(System.Type type)
  RVA=0x0AC479AC  token=0x6000003  System.String CreateNiceName(System.Type type)
  RVA=0x0AC47D1C  token=0x6000004  System.Boolean FloatEqualityComparer(System.Single a, System.Single b)
  RVA=0x0AC47CF4  token=0x6000005  System.Boolean DoubleEqualityComparer(System.Double a, System.Double b)
  RVA=0x0415FC30  token=0x6000006  System.Boolean QuaternionEqualityComparer(UnityEngine.Quaternion a, UnityEngine.Quaternion b)
  RVA=0x0AC47EB4  token=0x6000007  System.String GetMaybeSimplifiedTypeName(System.Type type)
  RVA=0x0AC47F50  token=0x6000008  System.String GetNiceName(System.Type type)
  RVA=-1  // generic def  token=0x6000009  T GetCustomAttribute(System.Type type, System.Boolean inherit)
  RVA=-1  // generic def  token=0x600000A  T GetCustomAttribute(System.Type type)
  RVA=0x0AC47FF0  token=0x600000B  System.Boolean InheritsFrom(System.Type type, System.Type baseType)
  RVA=0x0AC4813C  token=0x600000C  System.Void .cctor()
END_CLASS

CLASS: Sirenix.Utilities.DoubleLookupDictionary`3
TYPE:  class
TOKEN: 0x2000004
EXTENDS: System.Collections.Generic.Dictionary`2
FIELDS:
  private   readonly System.Collections.Generic.IEqualityComparer<TSecondKey>secondKeyComparer  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600000D  System.Void .ctor()
END_CLASS

CLASS: Sirenix.Utilities.IGlobalConfigEvents
TYPE:  interface
TOKEN: 0x2000005
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600000E  System.Void OnConfigAutoCreated()
  RVA=-1  // abstract  token=0x600000F  System.Void OnConfigInstanceFirstAccessed()
END_CLASS

CLASS: Sirenix.Utilities.GlobalConfig`1
TYPE:  abstract class
TOKEN: 0x2000006
EXTENDS: UnityEngine.ScriptableObject
IMPLEMENTS: Sirenix.Utilities.IGlobalConfigEvents
FIELDS:
  private   static  Sirenix.Utilities.GlobalConfigAttributeconfigAttribute  // static @ 0x0
  private   static  T                               instance  // static @ 0x0
PROPERTIES:
  ConfigAttribute  get=-1  // not resolved
  HasInstanceLoaded  get=-1  // not resolved
  Instance  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000013  System.Void LoadInstanceIfAssetExists()
  RVA=-1  // not resolved  token=0x6000014  System.Void OpenInEditor()
  RVA=-1  // not resolved  token=0x6000015  System.Void OnConfigInstanceFirstAccessed()
  RVA=-1  // not resolved  token=0x6000016  System.Void OnConfigAutoCreated()
  RVA=-1  // not resolved  token=0x6000017  System.Void Sirenix.Utilities.IGlobalConfigEvents.OnConfigAutoCreated()
  RVA=-1  // not resolved  token=0x6000018  System.Void Sirenix.Utilities.IGlobalConfigEvents.OnConfigInstanceFirstAccessed()
  RVA=-1  // not resolved  token=0x6000019  System.Void .ctor()
END_CLASS

CLASS: Sirenix.Utilities.GlobalConfigUtility`1
TYPE:  static class
TOKEN: 0x2000007
FIELDS:
  private   static  T                               instance  // static @ 0x0
PROPERTIES:
  HasInstanceLoaded  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600001B  T GetInstance(System.String defaultAssetFolderPath, System.String defaultFileNameWithoutExtension)
  RVA=-1  // not resolved  token=0x600001C  System.Void LoadInstanceIfAssetExists(System.String assetPath, System.String defaultFileNameWithoutExtension)
END_CLASS

CLASS: Sirenix.Utilities.GlobalConfigAttribute
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  private           System.String                   assetPath  // 0x10
  private           System.Boolean                  <UseAsset>k__BackingField  // 0x18
PROPERTIES:
  AssetPath  get=0x02B2ECC0
METHODS:
  RVA=0x0AC475D8  token=0x600001E  System.Void .ctor(System.String assetPath)
END_CLASS

CLASS: Sirenix.Utilities.PersistentAssemblyAttribute
TYPE:  class
TOKEN: 0x2000009
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600001F  System.Void .ctor()
END_CLASS

CLASS: Sirenix.Utilities.ProjectPathFinder
TYPE:  class
TOKEN: 0x200000A
SIZE:  0x18
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
METHODS:
  RVA=0x03573370  token=0x6000020  System.Void .ctor()
END_CLASS

CLASS: Sirenix.Utilities.SirenixAssetPaths
TYPE:  static class
TOKEN: 0x200000B
SIZE:  0x10
FIELDS:
  public    static readonly System.String                   OdinPath  // static @ 0x0
  public    static readonly System.String                   SirenixAssetsPath  // static @ 0x8
  public    static readonly System.String                   SirenixPluginPath  // static @ 0x10
  public    static readonly System.String                   SirenixAssembliesPath  // static @ 0x18
  public    static readonly System.String                   OdinResourcesPath  // static @ 0x20
  public    static readonly System.String                   OdinEditorConfigsPath  // static @ 0x28
  public    static readonly System.String                   OdinResourcesConfigsPath  // static @ 0x30
METHODS:
  RVA=0x0AC47720  token=0x6000021  System.Void .cctor()
END_CLASS

CLASS: Sirenix.Utilities.SirenixGlobalConfigAttribute
TYPE:  class
TOKEN: 0x200000C
SIZE:  0x20
EXTENDS: Sirenix.Utilities.GlobalConfigAttribute
FIELDS:
METHODS:
  RVA=0x0AC47958  token=0x6000022  System.Void .ctor()
END_CLASS

CLASS: Sirenix.Utilities.UnityVersion
TYPE:  static class
TOKEN: 0x200000D
SIZE:  0x10
FIELDS:
  public    static readonly System.Int32                    Major  // static @ 0x0
  public    static readonly System.Int32                    Minor  // static @ 0x4
METHODS:
  RVA=0x03B0CEC0  token=0x6000023  System.Void .cctor()
  RVA=0x041E1670  token=0x6000024  System.Void EnsureLoaded()
END_CLASS

