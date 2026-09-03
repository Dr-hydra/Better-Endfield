// ========================================================
// Dumped by @desirepro
// Assembly: Sirenix.Utilities.dll
// Classes:  13
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

namespace Sirenix.Utilities
{

    // TypeToken: 0x2000002  // size: 0x10
    public static class LinqExtensions
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000001
        public static System.Void Sort(System.Collections.Generic.IList<T> list, System.Comparison<T> comparison) { }

    }

    // TypeToken: 0x2000003  // size: 0x10
    public static class TypeExtensions
    {
        // Fields
        private static readonly System.Func<System.Single,System.Single,System.Boolean> FloatEqualityComparerFunc;  // static @ 0x0
        private static readonly System.Func<System.Double,System.Double,System.Boolean> DoubleEqualityComparerFunc;  // static @ 0x8
        private static readonly System.Func<UnityEngine.Quaternion,UnityEngine.Quaternion,System.Boolean> QuaternionEqualityComparerFunc;  // static @ 0x10
        private static readonly System.Object GenericConstraintsSatisfaction_LOCK;  // static @ 0x18
        private static readonly System.Collections.Generic.Dictionary<System.Type,System.Type> GenericConstraintsSatisfactionInferredParameters;  // static @ 0x20
        private static readonly System.Collections.Generic.Dictionary<System.Type,System.Type> GenericConstraintsSatisfactionResolvedMap;  // static @ 0x28
        private static readonly System.Collections.Generic.HashSet<System.Type> GenericConstraintsSatisfactionProcessedParams;  // static @ 0x30
        private static readonly System.Collections.Generic.HashSet<System.Type> GenericConstraintsSatisfactionTypesToCheck;  // static @ 0x38
        private static readonly System.Collections.Generic.List<System.Type> GenericConstraintsSatisfactionTypesToCheck_ToAdd;  // static @ 0x40
        private static readonly System.Type GenericListInterface;  // static @ 0x48
        private static readonly System.Type GenericCollectionInterface;  // static @ 0x50
        private static readonly System.Object WeaklyTypedTypeCastDelegates_LOCK;  // static @ 0x58
        private static readonly System.Object StronglyTypedTypeCastDelegates_LOCK;  // static @ 0x60
        private static readonly Sirenix.Utilities.DoubleLookupDictionary<System.Type,System.Type,System.Func<System.Object,System.Object>> WeaklyTypedTypeCastDelegates;  // static @ 0x68
        private static readonly Sirenix.Utilities.DoubleLookupDictionary<System.Type,System.Type,System.Delegate> StronglyTypedTypeCastDelegates;  // static @ 0x70
        private static readonly System.Type[] TwoLengthTypeArray_Cached;  // static @ 0x78
        private static readonly System.Collections.Generic.Stack<System.Type> GenericArgumentsContainsTypes_ArgsToCheckCached;  // static @ 0x80
        private static System.Collections.Generic.HashSet<System.String> ReservedCSharpKeywords;  // static @ 0x88
        public static readonly System.Collections.Generic.Dictionary<System.Type,System.String> TypeNameAlternatives;  // static @ 0x90
        private static readonly System.Object CachedNiceNames_LOCK;  // static @ 0x98
        private static readonly System.Collections.Generic.Dictionary<System.Type,System.String> CachedNiceNames;  // static @ 0xa0
        private static readonly System.Type VoidPointerType;  // static @ 0xa8
        private static readonly System.Collections.Generic.Dictionary<System.Type,System.Collections.Generic.HashSet<System.Type>> PrimitiveImplicitCasts;  // static @ 0xb0
        private static readonly System.Collections.Generic.HashSet<System.Type> ExplicitCastIntegrals;  // static @ 0xb8

        // Methods
        // RVA: 0x0AC47D40  token: 0x6000002
        private static System.String GetCachedNiceName(System.Type type) { }
        // RVA: 0x0AC479AC  token: 0x6000003
        private static System.String CreateNiceName(System.Type type) { }
        // RVA: 0x0AC47D1C  token: 0x6000004
        private static System.Boolean FloatEqualityComparer(System.Single a, System.Single b) { }
        // RVA: 0x0AC47CF4  token: 0x6000005
        private static System.Boolean DoubleEqualityComparer(System.Double a, System.Double b) { }
        // RVA: 0x0415FC30  token: 0x6000006
        private static System.Boolean QuaternionEqualityComparer(UnityEngine.Quaternion a, UnityEngine.Quaternion b) { }
        // RVA: 0x0AC47EB4  token: 0x6000007
        private static System.String GetMaybeSimplifiedTypeName(System.Type type) { }
        // RVA: 0x0AC47F50  token: 0x6000008
        public static System.String GetNiceName(System.Type type) { }
        // RVA: -1  // generic def  token: 0x6000009
        public static T GetCustomAttribute(System.Type type, System.Boolean inherit) { }
        // RVA: -1  // generic def  token: 0x600000A
        public static T GetCustomAttribute(System.Type type) { }
        // RVA: 0x0AC47FF0  token: 0x600000B
        public static System.Boolean InheritsFrom(System.Type type, System.Type baseType) { }
        // RVA: 0x0AC4813C  token: 0x600000C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000004
    public class DoubleLookupDictionary`3 : System.Collections.Generic.Dictionary`2
    {
        // Fields
        private readonly System.Collections.Generic.IEqualityComparer<TSecondKey> secondKeyComparer;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600000D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000005
    public interface IGlobalConfigEvents
    {
        // Methods
        // RVA: -1  // abstract  token: 0x600000E
        public virtual System.Void OnConfigAutoCreated() { }
        // RVA: -1  // abstract  token: 0x600000F
        public virtual System.Void OnConfigInstanceFirstAccessed() { }

    }

    // TypeToken: 0x2000006
    public abstract class GlobalConfig`1 : UnityEngine.ScriptableObject, Sirenix.Utilities.IGlobalConfigEvents
    {
        // Fields
        private static Sirenix.Utilities.GlobalConfigAttribute configAttribute;  // static @ 0x0
        private static T instance;  // static @ 0x0

        // Properties
        Sirenix.Utilities.GlobalConfigAttribute ConfigAttribute { get; /* RVA: -1  // not resolved */ }
        System.Boolean HasInstanceLoaded { get; /* RVA: -1  // not resolved */ }
        T Instance { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000013
        public static System.Void LoadInstanceIfAssetExists() { }
        // RVA: -1  // not resolved  token: 0x6000014
        public System.Void OpenInEditor() { }
        // RVA: -1  // not resolved  token: 0x6000015
        protected virtual System.Void OnConfigInstanceFirstAccessed() { }
        // RVA: -1  // not resolved  token: 0x6000016
        protected virtual System.Void OnConfigAutoCreated() { }
        // RVA: -1  // not resolved  token: 0x6000017
        private virtual System.Void Sirenix.Utilities.IGlobalConfigEvents.OnConfigAutoCreated() { }
        // RVA: -1  // not resolved  token: 0x6000018
        private virtual System.Void Sirenix.Utilities.IGlobalConfigEvents.OnConfigInstanceFirstAccessed() { }
        // RVA: -1  // not resolved  token: 0x6000019
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000007
    public static class GlobalConfigUtility`1
    {
        // Fields
        private static T instance;  // static @ 0x0

        // Properties
        System.Boolean HasInstanceLoaded { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600001B
        public static T GetInstance(System.String defaultAssetFolderPath, System.String defaultFileNameWithoutExtension) { }
        // RVA: -1  // not resolved  token: 0x600001C
        private static System.Void LoadInstanceIfAssetExists(System.String assetPath, System.String defaultFileNameWithoutExtension) { }

    }

    // TypeToken: 0x2000008  // size: 0x20
    public class GlobalConfigAttribute : System.Attribute
    {
        // Fields
        private System.String assetPath;  // 0x10
        private System.Boolean <UseAsset>k__BackingField;  // 0x18

        // Properties
        System.String AssetPath { get; /* RVA: 0x02B2ECC0 */ }

        // Methods
        // RVA: 0x0AC475D8  token: 0x600001E
        public System.Void .ctor(System.String assetPath) { }

    }

    // TypeToken: 0x2000009  // size: 0x10
    public class PersistentAssemblyAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600001F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000A  // size: 0x18
    public class ProjectPathFinder : UnityEngine.ScriptableObject
    {
        // Methods
        // RVA: 0x03573370  token: 0x6000020
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000B  // size: 0x10
    public static class SirenixAssetPaths
    {
        // Fields
        public static readonly System.String OdinPath;  // static @ 0x0
        public static readonly System.String SirenixAssetsPath;  // static @ 0x8
        public static readonly System.String SirenixPluginPath;  // static @ 0x10
        public static readonly System.String SirenixAssembliesPath;  // static @ 0x18
        public static readonly System.String OdinResourcesPath;  // static @ 0x20
        public static readonly System.String OdinEditorConfigsPath;  // static @ 0x28
        public static readonly System.String OdinResourcesConfigsPath;  // static @ 0x30

        // Methods
        // RVA: 0x0AC47720  token: 0x6000021
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000C  // size: 0x20
    public class SirenixGlobalConfigAttribute : Sirenix.Utilities.GlobalConfigAttribute
    {
        // Methods
        // RVA: 0x0AC47958  token: 0x6000022
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000D  // size: 0x10
    public static class UnityVersion
    {
        // Fields
        public static readonly System.Int32 Major;  // static @ 0x0
        public static readonly System.Int32 Minor;  // static @ 0x4

        // Methods
        // RVA: 0x03B0CEC0  token: 0x6000023
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000024
        private static System.Void EnsureLoaded() { }

    }

}

