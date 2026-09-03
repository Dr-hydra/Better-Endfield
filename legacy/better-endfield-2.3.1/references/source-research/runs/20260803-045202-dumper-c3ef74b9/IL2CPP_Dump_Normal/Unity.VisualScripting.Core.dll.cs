// ========================================================
// Dumped by @desirepro
// Assembly: Unity.VisualScripting.Core.dll
// Classes:  260
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000008  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Unity.VisualScripting.FakeSerializationCloner.<>c <>9;  // static @ 0x0
        public static System.Func<Unity.VisualScripting.FullSerializer.fsMetaProperty,System.Reflection.MemberInfo> <>9__6_0;  // static @ 0x8

        // Methods
        // RVA: 0x0B36BBC4  token: 0x600001F
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000020
        public System.Void .ctor() { }
        // RVA: 0x06C08920  token: 0x6000021
        private System.Reflection.MemberInfo <GetMembers>b__6_0(Unity.VisualScripting.FullSerializer.fsMetaProperty p) { }

    }

    // TypeToken: 0x200000E  // size: 0x18
    public sealed class <>c__DisplayClass35_0
    {
        // Fields
        public System.Type type;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000045
        public System.Void .ctor() { }
        // RVA: 0x0B36B81C  token: 0x6000046
        private System.Boolean <GetCloner>b__0(Unity.VisualScripting.ICloner cloner) { }

    }

    // TypeToken: 0x2000010  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Unity.VisualScripting.CloningContext.<>c <>9;  // static @ 0x0
        public static System.Func<Unity.VisualScripting.CloningContext> <>9__15_0;  // static @ 0x8

        // Methods
        // RVA: 0x04D454F0  token: 0x6000051
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000052
        public System.Void .ctor() { }
        // RVA: 0x039E80E0  token: 0x6000053
        private Unity.VisualScripting.CloningContext <New>b__15_0() { }

    }

    // TypeToken: 0x2000018
    public sealed struct Enumerator : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Collections.Generic.Dictionary.Enumerator<System.Type,Unity.VisualScripting.IKeyedCollection<TKey,TItem>> collectionsEnumerator;  // 0x0
        private TItem currentItem;  // 0x0
        private Unity.VisualScripting.IKeyedCollection<TKey,TItem> currentCollection;  // 0x0
        private System.Int32 indexInCurrentCollection;  // 0x0
        private System.Boolean exceeded;  // 0x0

        // Properties
        TItem Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600006A
        public System.Void .ctor(Unity.VisualScripting.MergedKeyedCollection<TKey,TItem> merged) { }
        // RVA: -1  // not resolved  token: 0x600006B
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x600006C
        public virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x600006F
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000022  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Unity.VisualScripting.EditorTimeBinding.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x0B36BC28  token: 0x6000090
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000091
        public System.Void .ctor() { }
        // RVA: 0x0B36B490  token: 0x6000092
        private System.Int32 <.cctor>b__6_0() { }
        // RVA: 0x0B36B498  token: 0x6000093
        private System.Single <.cctor>b__6_1() { }

    }

    // TypeToken: 0x200002A  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Unity.VisualScripting.XString.<>c <>9;  // static @ 0x0
        public static System.Func<System.String,System.Object> <>9__1_0;  // static @ 0x8

        // Methods
        // RVA: 0x0B36B96C  token: 0x60000A6
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60000A7
        public System.Void .ctor() { }
        // RVA: 0x04D88C30  token: 0x60000A8
        private System.Object <Inject>b__1_0(System.String a) { }

    }

    // TypeToken: 0x2000030  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Unity.VisualScripting.Graph.<>c <>9;  // static @ 0x0
        public static System.Func<Unity.VisualScripting.IGraphElement,System.Collections.Generic.IEnumerable<Unity.VisualScripting.ISerializationDependency>> <>9__27_0;  // static @ 0x8
        public static System.Comparison<Unity.VisualScripting.IGraphElement> <>9__30_0;  // static @ 0x10

        // Methods
        // RVA: 0x0B36BA98  token: 0x60000B5
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60000B6
        public System.Void .ctor() { }
        // RVA: 0x0B36B5A8  token: 0x60000B7
        private System.Collections.Generic.IEnumerable<Unity.VisualScripting.ISerializationDependency> <get_deserializationDependencies>b__27_0(Unity.VisualScripting.IGraphElement e) { }
        // RVA: 0x0B36B2BC  token: 0x60000B8
        private System.Int32 <OnAfterDependenciesDeserialized>b__30_0(Unity.VisualScripting.IGraphElement a, Unity.VisualScripting.IGraphElement b) { }

    }

    // TypeToken: 0x2000038  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Unity.VisualScripting.GraphStack.<>c <>9;  // static @ 0x0
        public static System.Func<Unity.VisualScripting.GraphStack> <>9__3_0;  // static @ 0x8

        // Methods
        // RVA: 0x0B36BA34  token: 0x60000F7
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60000F8
        public System.Void .ctor() { }
        // RVA: 0x0B36B26C  token: 0x60000F9
        private Unity.VisualScripting.GraphStack <New>b__3_0() { }

    }

    // TypeToken: 0x200004E  // size: 0x20
    public class AttributeCache
    {
        // Fields
        private readonly System.Collections.Generic.List<System.Attribute> <inheritedAttributes>k__BackingField;  // 0x10
        private readonly System.Collections.Generic.List<System.Attribute> <definedAttributes>k__BackingField;  // 0x18

        // Properties
        System.Collections.Generic.List<System.Attribute> inheritedAttributes { get; /* RVA: 0x02B2ECC0 */ }
        System.Collections.Generic.List<System.Attribute> definedAttributes { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x0B352A74  token: 0x6000124
        public System.Void .ctor(System.Reflection.MemberInfo element) { }
        // RVA: 0x0B352888  token: 0x6000125
        private System.Void Cache(System.Attribute[] attributeObjects, System.Collections.Generic.List<System.Attribute> cache) { }
        // RVA: 0x0B3529E0  token: 0x6000126
        private System.Boolean HasAttribute(System.Type attributeType, System.Collections.Generic.List<System.Attribute> cache) { }
        // RVA: 0x0B352920  token: 0x6000127
        private System.Attribute GetAttribute(System.Type attributeType, System.Collections.Generic.List<System.Attribute> cache) { }
        // RVA: 0x0B3529C4  token: 0x6000128
        public System.Boolean HasAttribute(System.Type attributeType, System.Boolean inherit) { }
        // RVA: 0x0B352904  token: 0x6000129
        public System.Attribute GetAttribute(System.Type attributeType, System.Boolean inherit) { }
        // RVA: -1  // generic def  token: 0x600012A
        public System.Boolean HasAttribute(System.Boolean inherit) { }
        // RVA: -1  // generic def  token: 0x600012B
        public TAttribute GetAttribute(System.Boolean inherit) { }

    }

    // TypeToken: 0x2000050  // size: 0x14
    public sealed struct ConversionType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Unity.VisualScripting.ConversionUtility.ConversionType Impossible;  // const
        public static Unity.VisualScripting.ConversionUtility.ConversionType Identity;  // const
        public static Unity.VisualScripting.ConversionUtility.ConversionType Upcast;  // const
        public static Unity.VisualScripting.ConversionUtility.ConversionType Downcast;  // const
        public static Unity.VisualScripting.ConversionUtility.ConversionType NumericImplicit;  // const
        public static Unity.VisualScripting.ConversionUtility.ConversionType NumericExplicit;  // const
        public static Unity.VisualScripting.ConversionUtility.ConversionType UserDefinedImplicit;  // const
        public static Unity.VisualScripting.ConversionUtility.ConversionType UserDefinedExplicit;  // const
        public static Unity.VisualScripting.ConversionUtility.ConversionType UserDefinedThenNumericImplicit;  // const
        public static Unity.VisualScripting.ConversionUtility.ConversionType UserDefinedThenNumericExplicit;  // const
        public static Unity.VisualScripting.ConversionUtility.ConversionType UnityHierarchy;  // const
        public static Unity.VisualScripting.ConversionUtility.ConversionType EnumerableToArray;  // const
        public static Unity.VisualScripting.ConversionUtility.ConversionType EnumerableToList;  // const
        public static Unity.VisualScripting.ConversionUtility.ConversionType ToString;  // const

    }

    // TypeToken: 0x2000051  // size: 0x20
    public sealed struct ConversionQuery : System.IEquatable`1
    {
        // Fields
        public readonly System.Type source;  // 0x10
        public readonly System.Type destination;  // 0x18

        // Methods
        // RVA: 0x039CF630  token: 0x6000148
        public System.Void .ctor(System.Type source, System.Type destination) { }
        // RVA: 0x0B35363C  token: 0x6000149
        public virtual System.Boolean Equals(Unity.VisualScripting.ConversionUtility.ConversionQuery other) { }
        // RVA: 0x0B3536AC  token: 0x600014A
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0B35371C  token: 0x600014B
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x2000052  // size: 0x11
    public sealed struct ConversionQueryComparer : System.Collections.Generic.IEqualityComparer`1
    {
        // Methods
        // RVA: 0x0B353604  token: 0x600014C
        public virtual System.Boolean Equals(Unity.VisualScripting.ConversionUtility.ConversionQuery x, Unity.VisualScripting.ConversionUtility.ConversionQuery y) { }
        // RVA: 0x0B353628  token: 0x600014D
        public virtual System.Int32 GetHashCode(Unity.VisualScripting.ConversionUtility.ConversionQuery obj) { }

    }

    // TypeToken: 0x2000053  // size: 0x18
    public sealed class <>c__DisplayClass11_0
    {
        // Fields
        public System.Type source;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600014E
        public System.Void .ctor() { }
        // RVA: 0x0B36B5C4  token: 0x600014F
        private System.Boolean <FindUserDefinedConversionMethods>b__2(System.Reflection.MethodInfo m) { }

    }

    // TypeToken: 0x2000054  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Unity.VisualScripting.ConversionUtility.<>c <>9;  // static @ 0x0
        public static System.Func<System.Reflection.MethodInfo,System.Boolean> <>9__11_0;  // static @ 0x8
        public static System.Func<System.Reflection.MethodInfo,System.Boolean> <>9__11_1;  // static @ 0x10

        // Methods
        // RVA: 0x0B36BCF0  token: 0x6000150
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000151
        public System.Void .ctor() { }
        // RVA: 0x0B36B144  token: 0x6000152
        private System.Boolean <FindUserDefinedConversionMethods>b__11_0(System.Reflection.MethodInfo m) { }
        // RVA: 0x0B36B190  token: 0x6000153
        private System.Boolean <FindUserDefinedConversionMethods>b__11_1(System.Reflection.MethodInfo m) { }

    }

    // TypeToken: 0x2000055  // size: 0x18
    public sealed class <>c__DisplayClass13_0
    {
        // Fields
        public System.Type destination;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000154
        public System.Void .ctor() { }
        // RVA: 0x0B36B66C  token: 0x6000155
        private System.Boolean <GetUserDefinedConversionType>b__0(System.Reflection.MethodInfo m) { }
        // RVA: 0x0B36B6E0  token: 0x6000156
        private System.Boolean <GetUserDefinedConversionType>b__1(System.Reflection.MethodInfo m) { }
        // RVA: 0x0B36B75C  token: 0x6000157
        private System.Boolean <GetUserDefinedConversionType>b__2(System.Reflection.MethodInfo m) { }

    }

    // TypeToken: 0x2000058  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Unity.VisualScripting.MemberUtility.<>c <>9;  // static @ 0x0

        // Methods
        // RVA: 0x0B36B908  token: 0x6000162
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000163
        public System.Void .ctor() { }
        // RVA: 0x0B36B380  token: 0x6000164
        private Unity.VisualScripting.ExtensionMethodCache <.cctor>b__0_0() { }
        // RVA: 0x0B36B3D0  token: 0x6000165
        private System.Collections.Generic.Dictionary<System.Type,System.Reflection.MethodInfo[]> <.cctor>b__0_1() { }
        // RVA: 0x0B36B430  token: 0x6000166
        private System.Collections.Generic.HashSet<System.Reflection.MethodInfo> <.cctor>b__0_2() { }

    }

    // TypeToken: 0x200005A  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Unity.VisualScripting.ExtensionMethodCache.<>c <>9;  // static @ 0x0
        public static System.Func<System.Type,System.Boolean> <>9__0_0;  // static @ 0x8
        public static System.Func<System.Type,System.Collections.Generic.IEnumerable<System.Reflection.MethodInfo>> <>9__0_1;  // static @ 0x10
        public static System.Func<System.Reflection.MethodInfo,System.Boolean> <>9__0_2;  // static @ 0x18

        // Methods
        // RVA: 0x0B36BAFC  token: 0x6000168
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000169
        public System.Void .ctor() { }
        // RVA: 0x0B36B4A0  token: 0x600016A
        private System.Boolean <.ctor>b__0_0(System.Type type) { }
        // RVA: 0x0B36B514  token: 0x600016B
        private System.Collections.Generic.IEnumerable<System.Reflection.MethodInfo> <.ctor>b__0_1(System.Type type) { }
        // RVA: 0x0B36B534  token: 0x600016C
        private System.Boolean <.ctor>b__0_2(System.Reflection.MethodInfo method) { }

    }

    // TypeToken: 0x200005C  // size: 0x30
    public class Collection : System.Collections.ObjectModel.KeyedCollection`2, Unity.VisualScripting.IKeyedCollection`2, System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Methods
        // RVA: 0x09B24654  token: 0x6000177
        protected virtual System.String GetKeyForItem(Unity.VisualScripting.Namespace item) { }
        // RVA: 0x0B3534C0  token: 0x6000178
        public virtual System.Boolean TryGetValue(System.String key, Unity.VisualScripting.Namespace& value) { }
        // RVA: 0x0B3535C8  token: 0x6000179
        public System.Void .ctor() { }
        // RVA: 0x0B353580  token: 0x600017A
        private virtual Unity.VisualScripting.Namespace Unity.VisualScripting.IKeyedCollection<System.String,Unity.VisualScripting.Namespace>.get_Item(System.String key) { }
        // RVA: 0x0B353538  token: 0x600017B
        private virtual System.Boolean Unity.VisualScripting.IKeyedCollection<System.String,Unity.VisualScripting.Namespace>.Contains(System.String key) { }

    }

    // TypeToken: 0x2000076  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Unity.VisualScripting.OptimizedReflection.<>c <>9;  // static @ 0x0
        public static System.Func<System.Reflection.ParameterInfo,System.Boolean> <>9__39_0;  // static @ 0x8

        // Methods
        // RVA: 0x0B36BC8C  token: 0x60001F0
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60001F1
        public System.Void .ctor() { }
        // RVA: 0x0B36B358  token: 0x60001F2
        private System.Boolean <SupportsOptimization>b__39_0(System.Reflection.ParameterInfo parameter) { }

    }

    // TypeToken: 0x2000082
    public sealed class <>c__DisplayClass5_0
    {
        // Fields
        public TField constant;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000228
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000229
        private TField <Compile>b__2() { }

    }

    // TypeToken: 0x2000090  // size: 0x58
    public sealed class <GetAssemblyAttributes>d__15 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Attribute <>2__current;  // 0x18
        private System.Int32 <>l__initialThreadId;  // 0x20
        private System.Type attributeType;  // 0x28
        public System.Type <>3__attributeType;  // 0x30
        private System.Collections.Generic.IEnumerable<System.Reflection.Assembly> assemblies;  // 0x38
        public System.Collections.Generic.IEnumerable<System.Reflection.Assembly> <>3__assemblies;  // 0x40
        private System.Collections.Generic.IEnumerator<System.Reflection.Assembly> <>7__wrap1;  // 0x48
        private System.Collections.Generic.IEnumerator<System.Attribute> <>7__wrap2;  // 0x50

        // Properties
        System.Attribute System.Collections.Generic.IEnumerator<System.Attribute>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x03A11010  token: 0x600026E
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x0B36AA34  token: 0x600026F
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0B36A458  token: 0x6000270
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0B36AB20  token: 0x6000271
        private System.Void <>m__Finally1() { }
        // RVA: 0x0B36AB68  token: 0x6000272
        private System.Void <>m__Finally2() { }
        // RVA: 0x0B36A9E8  token: 0x6000274
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: 0x0B36A93C  token: 0x6000276
        private virtual System.Collections.Generic.IEnumerator<System.Attribute> System.Collections.Generic.IEnumerable<System.Attribute>.GetEnumerator() { }
        // RVA: 0x0B36A9E0  token: 0x6000277
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000091  // size: 0x20
    public sealed class <>c__DisplayClass25_0
    {
        // Fields
        public Unity.VisualScripting.TypeName typeName;  // 0x10
        public System.Func<System.Reflection.Assembly,System.Boolean> <>9__0;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000278
        public System.Void .ctor() { }
        // RVA: 0x0B36B7D8  token: 0x6000279
        private System.Boolean <TrySystemTypeLookup>b__0(System.Reflection.Assembly a) { }

    }

    // TypeToken: 0x2000093  // size: 0x14
    public sealed struct ParseState
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Unity.VisualScripting.TypeName.ParseState Name;  // const
        public static Unity.VisualScripting.TypeName.ParseState Array;  // const
        public static Unity.VisualScripting.TypeName.ParseState Generics;  // const
        public static Unity.VisualScripting.TypeName.ParseState Assembly;  // const

    }

    // TypeToken: 0x2000094  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Unity.VisualScripting.TypeName.<>c <>9;  // static @ 0x0
        public static System.Func<System.String,System.String> <>9__35_0;  // static @ 0x8

        // Methods
        // RVA: 0x0B36B9D0  token: 0x6000293
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000294
        public System.Void .ctor() { }
        // RVA: 0x0B36B580  token: 0x6000295
        private System.String <.ctor>b__35_0(System.String x) { }

    }

    // TypeToken: 0x2000097  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Unity.VisualScripting.TypeUtility.<>c <>9;  // static @ 0x0
        public static System.Func<System.Type,System.Boolean> <>9__35_0;  // static @ 0x8
        public static System.Func<System.Type,System.Boolean> <>9__35_1;  // static @ 0x10

        // Methods
        // RVA: 0x0B36BB60  token: 0x600029D
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600029E
        public System.Void .ctor() { }
        // RVA: 0x0B36B1DC  token: 0x600029F
        private System.Boolean <GetTypesSafely>b__35_0(System.Type t) { }
        // RVA: 0x0B36B224  token: 0x60002A0
        private System.Boolean <GetTypesSafely>b__35_1(System.Type t) { }

    }

    // TypeToken: 0x2000098  // size: 0x48
    public sealed class <GetTypesSafely>d__35 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Type <>2__current;  // 0x18
        private System.Int32 <>l__initialThreadId;  // 0x20
        private System.Reflection.Assembly assembly;  // 0x28
        public System.Reflection.Assembly <>3__assembly;  // 0x30
        private System.Type[] <>7__wrap1;  // 0x38
        private System.Int32 <>7__wrap2;  // 0x40

        // Properties
        System.Type System.Collections.Generic.IEnumerator<System.Type>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x03A11010  token: 0x60002A1
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60002A2
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0B36ABB4  token: 0x60002A3
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0B36B0F8  token: 0x60002A5
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: 0x0B36B05C  token: 0x60002A7
        private virtual System.Collections.Generic.IEnumerator<System.Type> System.Collections.Generic.IEnumerable<System.Type>.GetEnumerator() { }
        // RVA: 0x0B36B0F0  token: 0x60002A8
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x20000B0
    public sealed class <Concat>d__0`1 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private T <>2__current;  // 0x0
        private System.Int32 <>l__initialThreadId;  // 0x0
        private System.Collections.IEnumerable[] enumerables;  // 0x0
        public System.Collections.IEnumerable[] <>3__enumerables;  // 0x0
        private System.Collections.Generic.IEnumerator<System.Collections.IEnumerable> <>7__wrap1;  // 0x0
        private System.Collections.Generic.IEnumerator<T> <>7__wrap2;  // 0x0

        // Properties
        T System.Collections.Generic.IEnumerator<T>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000312
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x6000313
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000314
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x6000315
        private System.Void <>m__Finally1() { }
        // RVA: -1  // not resolved  token: 0x6000316
        private System.Void <>m__Finally2() { }
        // RVA: -1  // not resolved  token: 0x6000318
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: -1  // not resolved  token: 0x600031A
        private virtual System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x600031B
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x20000B1
    public sealed class <>c__2`1
    {
        // Fields
        public static readonly Unity.VisualScripting.LinqUtility.<>c__2<T> <>9;  // static @ 0x0
        public static System.Func<T,System.Boolean> <>9__2_0;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600031C
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x600031D
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x600031E
        private System.Boolean <NotNull>b__2_0(T i) { }

    }

    // TypeToken: 0x20000B3
    public sealed class <>c
    {
        // Fields
        public static readonly Unity.VisualScripting.Recursion.<>c<T> <>9;  // static @ 0x0
        public static System.Func<Unity.VisualScripting.Recursion<T>> <>9__13_0;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000329
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x600032A
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x600032B
        private Unity.VisualScripting.Recursion<T> <New>b__13_0() { }

    }

    // TypeToken: 0x20000B6  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Unity.VisualScripting.ReferenceCollector.<>c <>9;  // static @ 0x0
        public static UnityEngine.Events.UnityAction<UnityEngine.SceneManagement.Scene> <>9__3_0;  // static @ 0x8

        // Methods
        // RVA: 0x04D3FCD0  token: 0x6000334
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000335
        public System.Void .ctor() { }
        // RVA: 0x04D54B20  token: 0x6000336
        private System.Void <Initialize>b__3_0(UnityEngine.SceneManagement.Scene scene) { }

    }

    // TypeToken: 0x20000BF  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Unity.VisualScripting.SavedVariables.<>c <>9;  // static @ 0x0
        public static System.Action <>9__8_0;  // static @ 0x8
        public static System.Func<Unity.VisualScripting.VariableDeclaration,System.String> <>9__24_0;  // static @ 0x10

        // Methods
        // RVA: 0x04D4B0D0  token: 0x600035F
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000360
        public System.Void .ctor() { }
        // RVA: 0x0B36B300  token: 0x6000361
        private System.Void <OnEnterPlayMode>b__8_0() { }
        // RVA: 0x06C08920  token: 0x6000362
        private System.String <MergeInitialAndSavedDeclarations>b__24_0(Unity.VisualScripting.VariableDeclaration vd) { }

    }

    // TypeToken: 0x20000CB  // size: 0x18
    public sealed class <>c__DisplayClass5_0
    {
        // Fields
        public System.Enum x;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60003A2
        public System.Void .ctor() { }
        // RVA: 0x0B36CA88  token: 0x60003A3
        private System.ValueTuple<System.Enum,System.String> <TryDeserialize>b__3(Unity.VisualScripting.RenamedFromAttribute attr) { }

    }

    // TypeToken: 0x20000CC  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Unity.VisualScripting.FullSerializer.fsEnumConverter.<>c <>9;  // static @ 0x0
        public static System.Func<System.Enum,System.Collections.Generic.IEnumerable<System.ValueTuple<System.Enum,System.String>>> <>9__5_0;  // static @ 0x8
        public static System.Func<System.ValueTuple<System.Enum,System.String>,System.String> <>9__5_1;  // static @ 0x10
        public static System.Func<System.ValueTuple<System.Enum,System.String>,System.Enum> <>9__5_2;  // static @ 0x18

        // Methods
        // RVA: 0x0B36CAF8  token: 0x60003A4
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60003A5
        public System.Void .ctor() { }
        // RVA: 0x0B36C7D0  token: 0x60003A6
        private System.Collections.Generic.IEnumerable<System.ValueTuple<System.Enum,System.String>> <TryDeserialize>b__5_0(System.Enum x) { }
        // RVA: 0x04D94CB0  token: 0x60003A7
        private System.String <TryDeserialize>b__5_1(System.ValueTuple<System.Enum,System.String> x) { }
        // RVA: 0x02A55C30  token: 0x60003A8
        private System.Enum <TryDeserialize>b__5_2(System.ValueTuple<System.Enum,System.String> x) { }

    }

    // TypeToken: 0x20000D9  // size: 0x28
    public sealed struct AotCompilation
    {
        // Fields
        public System.Type Type;  // 0x10
        public Unity.VisualScripting.FullSerializer.fsMetaProperty[] Members;  // 0x18
        public System.Boolean IsConstructorPublic;  // 0x20

    }

    // TypeToken: 0x20000DB  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Unity.VisualScripting.FullSerializer.fsBaseConverter.<>c <>9;  // static @ 0x0
        public static System.Func<Unity.VisualScripting.FullSerializer.fsDataType,System.String> <>9__6_0;  // static @ 0x8

        // Methods
        // RVA: 0x0B36CB5C  token: 0x60003F7
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60003F8
        public System.Void .ctor() { }
        // RVA: 0x0B36C780  token: 0x60003F9
        private System.String <FailExpectedType>b__6_0(Unity.VisualScripting.FullSerializer.fsDataType t) { }

    }

    // TypeToken: 0x20000DE  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Unity.VisualScripting.FullSerializer.fsConfig.<>c <>9;  // static @ 0x0
        public static System.Func<System.String,System.Reflection.MemberInfo,System.String> <>9__10_0;  // static @ 0x8

        // Methods
        // RVA: 0x04D448F0  token: 0x60003FC
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60003FD
        public System.Void .ctor() { }
        // RVA: 0x04D88C30  token: 0x60003FE
        private System.String <.ctor>b__10_0(System.String name, System.Reflection.MemberInfo info) { }

    }

    // TypeToken: 0x20000F3  // size: 0x20
    public class fsLazyCycleDefinitionWriter
    {
        // Fields
        private System.Collections.Generic.Dictionary<System.Int32,Unity.VisualScripting.FullSerializer.fsData> _pendingDefinitions;  // 0x10
        private System.Collections.Generic.HashSet<System.Int32> _references;  // 0x18

        // Methods
        // RVA: 0x0B377570  token: 0x6000499
        public System.Void WriteDefinition(System.Int32 id, Unity.VisualScripting.FullSerializer.fsData data) { }
        // RVA: 0x0B3776C0  token: 0x600049A
        public System.Void WriteReference(System.Int32 id, System.Collections.Generic.Dictionary<System.String,Unity.VisualScripting.FullSerializer.fsData> dict) { }
        // RVA: 0x0B377508  token: 0x600049B
        public System.Void Clear() { }
        // RVA: 0x0B3778D4  token: 0x600049C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000F6  // size: 0x18
    public sealed class <>c__DisplayClass16_0
    {
        // Fields
        public System.Reflection.MemberInfo member;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60004BE
        public System.Void .ctor() { }
        // RVA: 0x0B36C8D0  token: 0x60004BF
        private System.Boolean <CollectProperties>b__0(System.Type t) { }
        // RVA: 0x0B36C924  token: 0x60004C0
        private System.Boolean <CollectProperties>b__1(System.Type t) { }
        // RVA: 0x0B36C978  token: 0x60004C1
        private System.Boolean <CollectProperties>b__2(System.Type t) { }

    }

    // TypeToken: 0x20000F7  // size: 0x18
    public sealed class <>c__DisplayClass18_0
    {
        // Fields
        public System.Reflection.PropertyInfo property;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60004C2
        public System.Void .ctor() { }
        // RVA: 0x0B36C9CC  token: 0x60004C3
        private System.Boolean <CanSerializeProperty>b__0(System.Type t) { }

    }

    // TypeToken: 0x20000F8  // size: 0x18
    public sealed class <>c__DisplayClass19_0
    {
        // Fields
        public System.Reflection.FieldInfo field;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60004C4
        public System.Void .ctor() { }
        // RVA: 0x0B36CA20  token: 0x60004C5
        private System.Boolean <CanSerializeField>b__0(System.Type t) { }

    }

    // TypeToken: 0x20000FB  // size: 0x10
    public class ObjectReferenceEqualityComparator : System.Collections.Generic.IEqualityComparer`1
    {
        // Fields
        public static readonly System.Collections.Generic.IEqualityComparer<System.Object> Instance;  // static @ 0x0

        // Methods
        // RVA: 0x0829C278  token: 0x60004CF
        private virtual System.Boolean System.Collections.Generic.IEqualityComparer<System.Object>.Equals(System.Object x, System.Object y) { }
        // RVA: 0x04DA3AC0  token: 0x60004D0
        private virtual System.Int32 System.Collections.Generic.IEqualityComparer<System.Object>.GetHashCode(System.Object obj) { }
        // RVA: 0x041E1670  token: 0x60004D1
        public System.Void .ctor() { }
        // RVA: 0x0B36C71C  token: 0x60004D2
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000FF  // size: 0x20
    public sealed struct AttributeQuery
    {
        // Fields
        public System.Reflection.MemberInfo MemberInfo;  // 0x10
        public System.Type AttributeType;  // 0x18

    }

    // TypeToken: 0x2000100  // size: 0x10
    public class AttributeQueryComparator : System.Collections.Generic.IEqualityComparer`1
    {
        // Methods
        // RVA: 0x0B36C66C  token: 0x60004E8
        public virtual System.Boolean Equals(Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.AttributeQuery x, Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.AttributeQuery y) { }
        // RVA: 0x0B36C6D0  token: 0x60004E9
        public virtual System.Int32 GetHashCode(Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.AttributeQuery obj) { }
        // RVA: 0x041E1670  token: 0x60004EA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000102  // size: 0x18
    public sealed class <>c__DisplayClass2_0
    {
        // Fields
        public System.Boolean includeNamespace;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60004ED
        public System.Void .ctor() { }
        // RVA: 0x0B36CA74  token: 0x60004EE
        private System.String <CSharpName>b__0(System.Type t) { }

    }

namespace Unity.VisualScripting
{

    // TypeToken: 0x2000002
    public abstract class Cloner`1 : Unity.VisualScripting.ICloner
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000001
        protected System.Void .ctor() { }
        // RVA: -1  // abstract  token: 0x6000002
        public virtual System.Boolean Handles(System.Type type) { }
        // RVA: -1  // not resolved  token: 0x6000003
        private virtual System.Void Unity.VisualScripting.ICloner.BeforeClone(System.Type type, System.Object original) { }
        // RVA: -1  // not resolved  token: 0x6000004
        public virtual System.Void BeforeClone(System.Type type, T original) { }
        // RVA: -1  // not resolved  token: 0x6000005
        private virtual System.Object Unity.VisualScripting.ICloner.ConstructClone(System.Type type, System.Object original) { }
        // RVA: -1  // not resolved  token: 0x6000006
        public virtual T ConstructClone(System.Type type, T original) { }
        // RVA: -1  // not resolved  token: 0x6000007
        private virtual System.Void Unity.VisualScripting.ICloner.FillClone(System.Type type, System.Object& clone, System.Object original, Unity.VisualScripting.CloningContext context) { }
        // RVA: -1  // not resolved  token: 0x6000008
        public virtual System.Void FillClone(System.Type type, T& clone, T original, Unity.VisualScripting.CloningContext context) { }
        // RVA: -1  // not resolved  token: 0x6000009
        private virtual System.Void Unity.VisualScripting.ICloner.AfterClone(System.Type type, System.Object clone) { }
        // RVA: -1  // not resolved  token: 0x600000A
        public virtual System.Void AfterClone(System.Type type, T clone) { }

    }

    // TypeToken: 0x2000003  // size: 0x10
    public sealed class AnimationCurveCloner : Unity.VisualScripting.Cloner`1
    {
        // Methods
        // RVA: 0x0B352534  token: 0x600000B
        public virtual System.Boolean Handles(System.Type type) { }
        // RVA: 0x0B352424  token: 0x600000C
        public virtual UnityEngine.AnimationCurve ConstructClone(System.Type type, UnityEngine.AnimationCurve original) { }
        // RVA: 0x0B352474  token: 0x600000D
        public virtual System.Void FillClone(System.Type type, UnityEngine.AnimationCurve& clone, UnityEngine.AnimationCurve original, Unity.VisualScripting.CloningContext context) { }
        // RVA: 0x045BFD30  token: 0x600000E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000004  // size: 0x10
    public sealed class ArrayCloner : Unity.VisualScripting.Cloner`1
    {
        // Methods
        // RVA: 0x0B352868  token: 0x600000F
        public virtual System.Boolean Handles(System.Type type) { }
        // RVA: 0x0B35274C  token: 0x6000010
        public virtual System.Array ConstructClone(System.Type type, System.Array original) { }
        // RVA: 0x0B352778  token: 0x6000011
        public virtual System.Void FillClone(System.Type type, System.Array& clone, System.Array original, Unity.VisualScripting.CloningContext context) { }
        // RVA: 0x045BFD00  token: 0x6000012
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000005  // size: 0x10
    public sealed class DictionaryCloner : Unity.VisualScripting.Cloner`1
    {
        // Methods
        // RVA: 0x0B35706C  token: 0x6000013
        public virtual System.Boolean Handles(System.Type type) { }
        // RVA: 0x0B356F80  token: 0x6000014
        public virtual System.Void FillClone(System.Type type, System.Collections.IDictionary& clone, System.Collections.IDictionary original, Unity.VisualScripting.CloningContext context) { }
        // RVA: 0x045BFCD0  token: 0x6000015
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000006  // size: 0x18
    public sealed class EnumerableCloner : Unity.VisualScripting.Cloner`1
    {
        // Fields
        private readonly System.Collections.Generic.Dictionary<System.Type,Unity.VisualScripting.IOptimizedInvoker> addMethods;  // 0x10

        // Methods
        // RVA: 0x0B3578E8  token: 0x6000016
        public virtual System.Boolean Handles(System.Type type) { }
        // RVA: 0x0B3574D4  token: 0x6000017
        public virtual System.Void FillClone(System.Type type, System.Collections.IEnumerable& clone, System.Collections.IEnumerable original, Unity.VisualScripting.CloningContext context) { }
        // RVA: 0x0B3576EC  token: 0x6000018
        private Unity.VisualScripting.IOptimizedInvoker GetAddMethod(System.Type type) { }
        // RVA: 0x045BFDD0  token: 0x6000019
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000007  // size: 0x28
    public sealed class FakeSerializationCloner : Unity.VisualScripting.ReflectedCloner
    {
        // Fields
        private Unity.VisualScripting.FullSerializer.fsConfig <config>k__BackingField;  // 0x20

        // Properties
        Unity.VisualScripting.FullSerializer.fsConfig config { get; /* RVA: 0x04D862C0 */ }

        // Methods
        // RVA: 0x0B358610  token: 0x600001B
        public virtual System.Void BeforeClone(System.Type type, System.Object original) { }
        // RVA: 0x0B3585C8  token: 0x600001C
        public virtual System.Void AfterClone(System.Type type, System.Object clone) { }
        // RVA: 0x0B358658  token: 0x600001D
        protected virtual System.Collections.Generic.IEnumerable<System.Reflection.MemberInfo> GetMembers(System.Type type) { }
        // RVA: 0x045BFD60  token: 0x600001E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000009  // size: 0x20
    public sealed class FieldsCloner : Unity.VisualScripting.ReflectedCloner
    {
        // Methods
        // RVA: 0x02FFF600  token: 0x6000022
        protected virtual System.Boolean IncludeField(System.Reflection.FieldInfo field) { }
        // RVA: 0x012081B0  token: 0x6000023
        protected virtual System.Boolean IncludeProperty(System.Reflection.PropertyInfo property) { }
        // RVA: 0x0539798C  token: 0x6000024
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000A  // size: 0x10
    public sealed class GradientCloner : Unity.VisualScripting.Cloner`1
    {
        // Methods
        // RVA: 0x0B358868  token: 0x6000025
        public virtual System.Boolean Handles(System.Type type) { }
        // RVA: 0x0B358798  token: 0x6000026
        public virtual UnityEngine.Gradient ConstructClone(System.Type type, UnityEngine.Gradient original) { }
        // RVA: 0x0B3587E8  token: 0x6000027
        public virtual System.Void FillClone(System.Type type, UnityEngine.Gradient& clone, UnityEngine.Gradient original, Unity.VisualScripting.CloningContext context) { }
        // RVA: 0x045BFCA0  token: 0x6000028
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000B  // size: 0x10
    public sealed class ListCloner : Unity.VisualScripting.Cloner`1
    {
        // Methods
        // RVA: 0x0B35C140  token: 0x6000029
        public virtual System.Boolean Handles(System.Type type) { }
        // RVA: 0x0B35BF9C  token: 0x600002A
        public virtual System.Void FillClone(System.Type type, System.Collections.IList& clone, System.Collections.IList original, Unity.VisualScripting.CloningContext context) { }
        // RVA: 0x045BFC70  token: 0x600002B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000C  // size: 0x20
    public abstract class ReflectedCloner : Unity.VisualScripting.Cloner`1
    {
        // Fields
        private readonly System.Collections.Generic.Dictionary<System.Type,System.Reflection.MemberInfo[]> accessors;  // 0x10
        private readonly System.Collections.Generic.Dictionary<System.Type,Unity.VisualScripting.IOptimizedAccessor[]> optimizedAccessors;  // 0x18

        // Methods
        // RVA: 0x012081B0  token: 0x600002C
        public virtual System.Boolean Handles(System.Type type) { }
        // RVA: 0x0B361738  token: 0x600002D
        public virtual System.Void FillClone(System.Type type, System.Object& clone, System.Object original, Unity.VisualScripting.CloningContext context) { }
        // RVA: 0x0B361AB8  token: 0x600002E
        private System.Reflection.MemberInfo[] GetAccessors(System.Type type) { }
        // RVA: 0x0B361D48  token: 0x600002F
        private Unity.VisualScripting.IOptimizedAccessor[] GetOptimizedAccessors(System.Type type) { }
        // RVA: 0x0B361B94  token: 0x6000030
        protected virtual System.Collections.Generic.IEnumerable<System.Reflection.MemberInfo> GetMembers(System.Type type) { }
        // RVA: 0x012081B0  token: 0x6000031
        protected virtual System.Boolean IncludeField(System.Reflection.FieldInfo field) { }
        // RVA: 0x012081B0  token: 0x6000032
        protected virtual System.Boolean IncludeProperty(System.Reflection.PropertyInfo property) { }
        // RVA: 0x045BFE60  token: 0x6000033
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200000D  // size: 0x10
    public static class Cloning
    {
        // Fields
        private static readonly System.Collections.Generic.Dictionary<System.Type,System.Boolean> skippable;  // static @ 0x0
        private static readonly System.Collections.Generic.HashSet<Unity.VisualScripting.ICloner> <cloners>k__BackingField;  // static @ 0x8
        private static readonly Unity.VisualScripting.ArrayCloner <arrayCloner>k__BackingField;  // static @ 0x10
        private static readonly Unity.VisualScripting.DictionaryCloner <dictionaryCloner>k__BackingField;  // static @ 0x18
        private static readonly Unity.VisualScripting.EnumerableCloner <enumerableCloner>k__BackingField;  // static @ 0x20
        private static readonly Unity.VisualScripting.ListCloner <listCloner>k__BackingField;  // static @ 0x28
        private static readonly Unity.VisualScripting.AnimationCurveCloner <animationCurveCloner>k__BackingField;  // static @ 0x30
        private static readonly Unity.VisualScripting.GradientCloner <gradientCloner>k__BackingField;  // static @ 0x38
        private static readonly Unity.VisualScripting.FieldsCloner <fieldsCloner>k__BackingField;  // static @ 0x40
        private static readonly Unity.VisualScripting.FakeSerializationCloner <fakeSerializationCloner>k__BackingField;  // static @ 0x48

        // Properties
        System.Collections.Generic.HashSet<Unity.VisualScripting.ICloner> cloners { get; /* RVA: 0x0B3532E0 */ }
        Unity.VisualScripting.ArrayCloner arrayCloner { get; /* RVA: 0x0B353290 */ }
        Unity.VisualScripting.DictionaryCloner dictionaryCloner { get; /* RVA: 0x0B353330 */ }
        Unity.VisualScripting.EnumerableCloner enumerableCloner { get; /* RVA: 0x0B353380 */ }
        Unity.VisualScripting.ListCloner listCloner { get; /* RVA: 0x0B353470 */ }
        Unity.VisualScripting.AnimationCurveCloner animationCurveCloner { get; /* RVA: 0x0B353240 */ }
        Unity.VisualScripting.GradientCloner gradientCloner { get; /* RVA: 0x0B353420 */ }
        Unity.VisualScripting.FakeSerializationCloner fakeSerializationCloner { get; /* RVA: 0x0B3533D0 */ }

        // Methods
        // RVA: 0x045BF5F0  token: 0x6000034
        private static System.Void .cctor() { }
        // RVA: 0x039E8440  token: 0x600003D
        public static System.Object Clone(System.Object original, Unity.VisualScripting.ICloner fallbackCloner, System.Boolean tryPreserveInstances) { }
        // RVA: 0x039E83B0  token: 0x600003E
        public static System.Object CloneViaFakeSerialization(System.Object original) { }
        // RVA: -1  // generic def  token: 0x600003F
        public static T CloneViaFakeSerialization(T original) { }
        // RVA: 0x039E8530  token: 0x6000040
        private static System.Object Clone(Unity.VisualScripting.CloningContext context, System.Object original) { }
        // RVA: 0x039E8870  token: 0x6000041
        private static System.Void CloneInto(Unity.VisualScripting.CloningContext context, System.Object& clone, System.Object original) { }
        // RVA: 0x039E8B80  token: 0x6000042
        public static Unity.VisualScripting.ICloner GetCloner(System.Object original, System.Type type) { }
        // RVA: 0x039E8AE0  token: 0x6000043
        private static Unity.VisualScripting.ICloner GetCloner(System.Object original, System.Type type, Unity.VisualScripting.ICloner fallbackCloner) { }
        // RVA: 0x039E9F30  token: 0x6000044
        private static System.Boolean Skippable(System.Type type) { }

    }

    // TypeToken: 0x200000F  // size: 0x28
    public sealed class CloningContext : Unity.VisualScripting.IPoolable, System.IDisposable
    {
        // Fields
        private readonly System.Collections.Generic.Dictionary<System.Object,System.Object> <clonings>k__BackingField;  // 0x10
        private Unity.VisualScripting.ICloner <fallbackCloner>k__BackingField;  // 0x18
        private System.Boolean <tryPreserveInstances>k__BackingField;  // 0x20
        private System.Boolean disposed;  // 0x21

        // Properties
        System.Collections.Generic.Dictionary<System.Object,System.Object> clonings { get; /* RVA: 0x02B2ECC0 */ }
        Unity.VisualScripting.ICloner fallbackCloner { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.Boolean tryPreserveInstances { get; /* RVA: 0x04D866B0 */ set; /* RVA: 0x04D866D0 */ }

        // Methods
        // RVA: 0x04DBA710  token: 0x600004C
        private virtual System.Void Unity.VisualScripting.IPoolable.New() { }
        // RVA: 0x03B7DA20  token: 0x600004D
        private virtual System.Void Unity.VisualScripting.IPoolable.Free() { }
        // RVA: 0x04CF4DD0  token: 0x600004E
        public virtual System.Void Dispose() { }
        // RVA: 0x039E85A0  token: 0x600004F
        public static Unity.VisualScripting.CloningContext New(Unity.VisualScripting.ICloner fallbackCloner, System.Boolean tryPreserveInstances) { }
        // RVA: 0x039E8130  token: 0x6000050
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000011
    public interface ICloner
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000054
        public virtual System.Boolean Handles(System.Type type) { }
        // RVA: -1  // abstract  token: 0x6000055
        public virtual System.Object ConstructClone(System.Type type, System.Object original) { }
        // RVA: -1  // abstract  token: 0x6000056
        public virtual System.Void BeforeClone(System.Type type, System.Object original) { }
        // RVA: -1  // abstract  token: 0x6000057
        public virtual System.Void FillClone(System.Type type, System.Object& clone, System.Object original, Unity.VisualScripting.CloningContext context) { }
        // RVA: -1  // abstract  token: 0x6000058
        public virtual System.Void AfterClone(System.Type type, System.Object clone) { }

    }

    // TypeToken: 0x2000012
    public interface ISpecifiesCloner
    {
        // Properties
        Unity.VisualScripting.ICloner cloner { get; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x2000013  // size: 0x48
    public sealed class AotDictionary : System.Collections.Specialized.OrderedDictionary
    {
        // Methods
        // RVA: 0x0AFF6D1C  token: 0x600005A
        public System.Void .ctor() { }
        // RVA: 0x0B352598  token: 0x600005B
        public static System.Void AotStubs() { }

    }

    // TypeToken: 0x2000014  // size: 0x28
    public sealed class AotList : System.Collections.ArrayList
    {
        // Methods
        // RVA: 0x0AF96728  token: 0x600005C
        public System.Void .ctor() { }
        // RVA: 0x0B352640  token: 0x600005D
        public static System.Void AotStubs() { }

    }

    // TypeToken: 0x2000015
    public class GuidCollection`1 : System.Collections.ObjectModel.KeyedCollection`2, System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x600005E
        protected virtual System.Guid GetKeyForItem(T item) { }

    }

    // TypeToken: 0x2000016
    public interface IKeyedCollection`2 : System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Properties
        TItem Item { get; /* RVA: -1  // abstract */ }
        TItem Item { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000061
        public virtual System.Boolean Contains(TKey key) { }

    }

    // TypeToken: 0x2000017
    public class MergedKeyedCollection`2 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        protected readonly System.Collections.Generic.Dictionary<System.Type,Unity.VisualScripting.IKeyedCollection<TKey,TItem>> collections;  // 0x0
        protected readonly System.Collections.Generic.Dictionary<System.Type,Unity.VisualScripting.IKeyedCollection<TKey,TItem>> collectionsLookup;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000062
        protected Unity.VisualScripting.IKeyedCollection<TKey,TItem> GetCollectionForItem(TItem item) { }
        // RVA: -1  // not resolved  token: 0x6000063
        protected Unity.VisualScripting.IKeyedCollection<TKey,TItem> GetCollectionForType(System.Type type, System.Boolean throwOnFail) { }
        // RVA: -1  // not resolved  token: 0x6000064
        public virtual System.Void Add(TItem item) { }
        // RVA: -1  // not resolved  token: 0x6000065
        public virtual System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x6000066
        public virtual System.Boolean Contains(TItem item) { }
        // RVA: -1  // not resolved  token: 0x6000067
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000068
        private virtual System.Collections.Generic.IEnumerator<TItem> System.Collections.Generic.IEnumerable<TItem>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000069
        public Unity.VisualScripting.MergedKeyedCollection.Enumerator<TKey,TItem> GetEnumerator() { }

    }

    // TypeToken: 0x2000019
    public class ConnectionCollection`3 : Unity.VisualScripting.ConnectionCollectionBase`4
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000070
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001A
    public class ConnectionCollectionBase`4 : Unity.VisualScripting.IConnectionCollection`3, System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly System.Collections.Generic.Dictionary<TDestination,System.Collections.Generic.List<TConnection>> byDestination;  // 0x0
        private readonly System.Collections.Generic.Dictionary<TSource,System.Collections.Generic.List<TConnection>> bySource;  // 0x0
        protected readonly TCollection collection;  // 0x0

        // Properties
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        System.Boolean IsReadOnly { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000071
        public System.Void .ctor(TCollection collection) { }
        // RVA: -1  // not resolved  token: 0x6000074
        public virtual System.Collections.Generic.IEnumerator<TConnection> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000075
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000076
        public virtual System.Collections.Generic.IEnumerable<TConnection> WithSource(TSource source) { }
        // RVA: -1  // not resolved  token: 0x6000077
        public System.Collections.Generic.List<TConnection> WithSourceNoAlloc(TSource source) { }
        // RVA: -1  // not resolved  token: 0x6000078
        public TConnection SingleOrDefaultWithSource(TSource source) { }
        // RVA: -1  // not resolved  token: 0x6000079
        public virtual System.Collections.Generic.IEnumerable<TConnection> WithDestination(TDestination destination) { }
        // RVA: -1  // not resolved  token: 0x600007A
        public System.Collections.Generic.List<TConnection> WithDestinationNoAlloc(TDestination destination) { }
        // RVA: -1  // not resolved  token: 0x600007B
        public TConnection SingleOrDefaultWithDestination(TDestination destination) { }
        // RVA: -1  // not resolved  token: 0x600007C
        public virtual System.Void Add(TConnection item) { }
        // RVA: -1  // not resolved  token: 0x600007D
        public virtual System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x600007E
        public virtual System.Boolean Contains(TConnection item) { }
        // RVA: -1  // not resolved  token: 0x600007F
        public virtual System.Void CopyTo(TConnection[] array, System.Int32 arrayIndex) { }
        // RVA: -1  // not resolved  token: 0x6000080
        public virtual System.Boolean Remove(TConnection item) { }
        // RVA: -1  // not resolved  token: 0x6000081
        protected virtual System.Void BeforeAdd(TConnection item) { }
        // RVA: -1  // not resolved  token: 0x6000082
        protected virtual System.Void AfterAdd(TConnection item) { }
        // RVA: -1  // not resolved  token: 0x6000083
        protected virtual System.Void BeforeRemove(TConnection item) { }
        // RVA: -1  // not resolved  token: 0x6000084
        protected virtual System.Void AfterRemove(TConnection item) { }
        // RVA: -1  // not resolved  token: 0x6000085
        private System.Void AddToDictionaries(TConnection item) { }
        // RVA: -1  // not resolved  token: 0x6000086
        private System.Void RemoveFromDictionaries(TConnection item) { }

    }

    // TypeToken: 0x200001B
    public class GraphConnectionCollection`3 : Unity.VisualScripting.ConnectionCollectionBase`4, System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1
    {
    }

    // TypeToken: 0x200001C
    public interface IConnection`2
    {
        // Properties
        TSource source { get; /* RVA: -1  // abstract */ }
        TDestination destination { get; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x200001D
    public interface IConnectionCollection`3 : System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
    }

    // TypeToken: 0x200001E  // size: 0x90
    public class InvalidConnectionException : System.Exception
    {
        // Methods
        // RVA: 0x0B35BAB4  token: 0x6000089
        public System.Void .ctor() { }
        // RVA: 0x0B35BB08  token: 0x600008A
        public System.Void .ctor(System.String message) { }

    }

    // TypeToken: 0x200001F  // size: 0x10
    public class ValueAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600008B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000020  // size: 0x18
    public class DisableAnnotationAttribute : System.Attribute
    {
        // Fields
        private System.Boolean <disableIcon>k__BackingField;  // 0x10

        // Methods
        // RVA: 0x015EFCD0  token: 0x600008C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000021  // size: 0x10
    public static class EditorTimeBinding
    {
        // Fields
        public static System.Func<System.Int32> frameBinding;  // static @ 0x0
        public static System.Func<System.Single> timeBinding;  // static @ 0x8

        // Properties
        System.Int32 frame { get; /* RVA: 0x0B357228 */ }
        System.Single time { get; /* RVA: 0x0B3572C8 */ }

        // Methods
        // RVA: 0x0B3570E4  token: 0x600008F
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000023  // size: 0x18
    public sealed class IncludeInSettingsAttribute : System.Attribute
    {
        // Fields
        private System.Boolean <include>k__BackingField;  // 0x10

        // Properties
        System.Boolean include { set; /* RVA: 0x04D86130 */ }

        // Methods
        // RVA: 0x04D86130  token: 0x6000094
        public System.Void .ctor(System.Boolean include) { }

    }

    // TypeToken: 0x2000024  // size: 0x10
    public sealed class InspectableAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000096
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000025  // size: 0x18
    public sealed class InspectorWideAttribute : System.Attribute
    {
        // Fields
        private System.Boolean <toEdge>k__BackingField;  // 0x10

        // Properties
        System.Boolean toEdge { set; /* RVA: 0x04D86130 */ }

        // Methods
        // RVA: 0x04D86130  token: 0x6000097
        public System.Void .ctor(System.Boolean toEdge) { }

    }

    // TypeToken: 0x2000026  // size: 0x10
    public static class Ensure
    {
        // Fields
        private static readonly Unity.VisualScripting.EnsureThat instance;  // static @ 0x0
        private static System.Boolean <IsActive>k__BackingField;  // static @ 0x8

        // Properties
        System.Boolean IsActive { get; /* RVA: 0x0B357430 */ set; /* RVA: 0x0B357480 */ }

        // Methods
        // RVA: 0x04727780  token: 0x600009B
        public static Unity.VisualScripting.EnsureThat That(System.String paramName) { }
        // RVA: 0x03FCE6A0  token: 0x600009C
        private static System.Void OnRuntimeMethodLoad() { }
        // RVA: 0x04D32510  token: 0x600009D
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000027  // size: 0x18
    public class EnsureThat
    {
        // Fields
        private System.String paramName;  // 0x10

        // Methods
        // RVA: -1  // generic def  token: 0x600009E
        public System.Void IsNotNull(T value) { }
        // RVA: 0x0B357368  token: 0x600009F
        public System.Void IsNotNull(System.String value) { }
        // RVA: -1  // generic def  token: 0x60000A0
        public System.Void IsOfType(T param, System.Type expectedType) { }
        // RVA: 0x041E1670  token: 0x60000A1
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000028  // size: 0x10
    public static class ExceptionMessages
    {
        // Fields
        private static readonly System.String <Common_IsNull_Failed>k__BackingField;  // static @ 0x0
        private static readonly System.String <Common_IsNotNull_Failed>k__BackingField;  // static @ 0x8
        private static readonly System.String <Booleans_IsTrueFailed>k__BackingField;  // static @ 0x10
        private static readonly System.String <Booleans_IsFalseFailed>k__BackingField;  // static @ 0x18
        private static readonly System.String <Collections_Any_Failed>k__BackingField;  // static @ 0x20
        private static readonly System.String <Collections_ContainsKey_Failed>k__BackingField;  // static @ 0x28
        private static readonly System.String <Collections_HasItemsFailed>k__BackingField;  // static @ 0x30
        private static readonly System.String <Collections_HasNoNullItemFailed>k__BackingField;  // static @ 0x38
        private static readonly System.String <Collections_SizeIs_Failed>k__BackingField;  // static @ 0x40
        private static readonly System.String <Comp_Is_Failed>k__BackingField;  // static @ 0x48
        private static readonly System.String <Comp_IsNot_Failed>k__BackingField;  // static @ 0x50
        private static readonly System.String <Comp_IsNotLt>k__BackingField;  // static @ 0x58
        private static readonly System.String <Comp_IsNotLte>k__BackingField;  // static @ 0x60
        private static readonly System.String <Comp_IsNotGt>k__BackingField;  // static @ 0x68
        private static readonly System.String <Comp_IsNotGte>k__BackingField;  // static @ 0x70
        private static readonly System.String <Comp_IsNotInRange_ToLow>k__BackingField;  // static @ 0x78
        private static readonly System.String <Comp_IsNotInRange_ToHigh>k__BackingField;  // static @ 0x80
        private static readonly System.String <Guids_IsNotEmpty_Failed>k__BackingField;  // static @ 0x88
        private static readonly System.String <Strings_IsEqualTo_Failed>k__BackingField;  // static @ 0x90
        private static readonly System.String <Strings_IsNotEqualTo_Failed>k__BackingField;  // static @ 0x98
        private static readonly System.String <Strings_SizeIs_Failed>k__BackingField;  // static @ 0xa0
        private static readonly System.String <Strings_IsNotNullOrWhiteSpace_Failed>k__BackingField;  // static @ 0xa8
        private static readonly System.String <Strings_IsNotNullOrEmpty_Failed>k__BackingField;  // static @ 0xb0
        private static readonly System.String <Strings_HasLengthBetween_Failed_ToShort>k__BackingField;  // static @ 0xb8
        private static readonly System.String <Strings_HasLengthBetween_Failed_ToLong>k__BackingField;  // static @ 0xc0
        private static readonly System.String <Strings_Matches_Failed>k__BackingField;  // static @ 0xc8
        private static readonly System.String <Strings_IsNotEmpty_Failed>k__BackingField;  // static @ 0xd0
        private static readonly System.String <Strings_IsGuid_Failed>k__BackingField;  // static @ 0xd8
        private static readonly System.String <Types_IsOfType_Failed>k__BackingField;  // static @ 0xe0
        private static readonly System.String <Reflection_HasAttribute_Failed>k__BackingField;  // static @ 0xe8
        private static readonly System.String <Reflection_HasConstructor_Failed>k__BackingField;  // static @ 0xf0
        private static readonly System.String <Reflection_HasPublicConstructor_Failed>k__BackingField;  // static @ 0xf8
        private static readonly System.String <ValueTypes_IsNotDefault_Failed>k__BackingField;  // static @ 0x100

        // Properties
        System.String Common_IsNotNull_Failed { get; /* RVA: 0x0B3581F8 */ }
        System.String Types_IsOfType_Failed { get; /* RVA: 0x0B358248 */ }

        // Methods
        // RVA: 0x0B3579CC  token: 0x60000A4
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000029  // size: 0x10
    public static class XString
    {
        // Methods
        // RVA: 0x0B36C530  token: 0x60000A5
        private static System.String Inject(System.String format, System.String[] formattingArgs) { }

    }

    // TypeToken: 0x200002B  // size: 0x11
    public sealed struct EmptyEventArgs
    {
    }

    // TypeToken: 0x200002C  // size: 0x90
    public class InvalidConversionException : System.InvalidCastException
    {
        // Methods
        // RVA: 0x0B35BB64  token: 0x60000A9
        public System.Void .ctor() { }
        // RVA: 0x0A843454  token: 0x60000AA
        public System.Void .ctor(System.String message) { }
        // RVA: 0x0B35BB6C  token: 0x60000AB
        public System.Void .ctor(System.String message, System.Exception innerException) { }

    }

    // TypeToken: 0x200002D  // size: 0x90
    public class InvalidImplementationException : System.Exception
    {
        // Methods
        // RVA: 0x0B35BB74  token: 0x60000AC
        public System.Void .ctor(System.String message) { }

    }

    // TypeToken: 0x200002E
    public class UnexpectedEnumValueException`1 : System.Exception
    {
        // Fields
        private T <Value>k__BackingField;  // 0x0

        // Properties
        T Value { set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60000AD
        public System.Void .ctor(T value) { }

    }

    // TypeToken: 0x200002F  // size: 0x20
    public abstract class Graph : Unity.VisualScripting.IGraph
    {
        // Fields
        private System.Collections.Generic.List<Unity.VisualScripting.IGraphElement> _elements;  // 0x10
        private readonly Unity.VisualScripting.MergedGraphElementCollection <elements>k__BackingField;  // 0x18

        // Properties
        Unity.VisualScripting.MergedGraphElementCollection elements { get; /* RVA: 0x0385B100 */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.ISerializationDependency> deserializationDependencies { get; /* RVA: 0x0B35B574 */ }

        // Methods
        // RVA: 0x0B35B508  token: 0x60000B1
        public virtual System.Void OnBeforeSerialize() { }
        // RVA: 0x0B35B4BC  token: 0x60000B2
        public virtual System.Void OnAfterDeserialize() { }
        // RVA: 0x0B35AFD4  token: 0x60000B3
        public virtual System.Void OnAfterDependenciesDeserialized() { }
        // RVA: 0x0B35AE88  token: 0x60000B4
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x2000031
    public abstract class GraphElement`1 : Unity.VisualScripting.IGraphElement, System.IDisposable, Unity.VisualScripting.IIdentifiable
    {
        // Fields
        private System.Guid <guid>k__BackingField;  // 0x0
        private TGraph <graph>k__BackingField;  // 0x0

        // Properties
        System.Guid guid { get; /* RVA: -1  // not resolved */ }
        System.Int32 dependencyOrder { get; /* RVA: -1  // not resolved */ }
        TGraph graph { get; /* RVA: -1  // not resolved */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.ISerializationDependency> deserializationDependencies { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60000BA
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x60000BC
        public virtual System.Boolean HandleDependencies() { }
        // RVA: -1  // not resolved  token: 0x60000BF
        public virtual System.String ToString() { }
        // RVA: -1  // not resolved  token: 0x60000C0
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000032
    public sealed class GraphElementCollection`1 : Unity.VisualScripting.GuidCollection`1, Unity.VisualScripting.IKeyedCollection`2, System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60000C1
        private virtual TElement Unity.VisualScripting.IKeyedCollection<System.Guid,TElement>.get_Item(System.Guid key) { }
        // RVA: -1  // not resolved  token: 0x60000C2
        private virtual System.Boolean Unity.VisualScripting.IKeyedCollection<System.Guid,TElement>.Contains(System.Guid key) { }

    }

    // TypeToken: 0x2000033  // size: 0x48
    public abstract class GraphPointer
    {
        // Fields
        private Unity.VisualScripting.IGraphRoot <root>k__BackingField;  // 0x10
        private UnityEngine.GameObject <gameObject>k__BackingField;  // 0x18
        protected readonly System.Collections.Generic.List<Unity.VisualScripting.IGraphParent> parentStack;  // 0x20
        protected readonly System.Collections.Generic.List<Unity.VisualScripting.IGraphParentElement> parentElementStack;  // 0x28
        protected readonly System.Collections.Generic.List<Unity.VisualScripting.IGraph> graphStack;  // 0x30
        protected readonly System.Collections.Generic.List<Unity.VisualScripting.IGraphData> dataStack;  // 0x38
        protected readonly System.Collections.Generic.List<Unity.VisualScripting.IGraphDebugData> debugDataStack;  // 0x40

        // Properties
        Unity.VisualScripting.IGraphRoot root { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        UnityEngine.Object rootObject { get; /* RVA: 0x0B359E88 */ }
        UnityEngine.GameObject gameObject { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        UnityEngine.GameObject self { get; /* RVA: 0x0385B100 */ }
        UnityEngine.Object serializedObject { get; /* RVA: 0x0B359EC4 */ }
        System.Int32 depth { get; /* RVA: 0x0B359A0C */ }
        System.Boolean isRoot { get; /* RVA: 0x0B359A7C */ }
        System.Boolean isChild { get; /* RVA: 0x0B359A64 */ }
        Unity.VisualScripting.IGraphParent parent { get; /* RVA: 0x0B359DE4 */ }
        Unity.VisualScripting.IGraphParentElement parentElement { get; /* RVA: 0x0B359D80 */ }
        Unity.VisualScripting.IGraph rootGraph { get; /* RVA: 0x0B359E40 */ }
        System.Boolean hasDebugData { get; /* RVA: 0x0B359A4C */ }
        Unity.VisualScripting.IGraphDebugData _debugData { get; /* RVA: 0x0B359990 */ }
        Unity.VisualScripting.IGraphDebugData debugData { get; /* RVA: 0x0B3599EC */ }
        System.Boolean isValid { get; /* RVA: 0x0B359A94 */ }

        // Methods
        // RVA: 0x0B3597EC  token: 0x60000C3
        private System.Void .ctor() { }
        // RVA: -1  // abstract  token: 0x60000C4
        public virtual Unity.VisualScripting.GraphReference AsReference() { }
        // RVA: 0x0B358AB4  token: 0x60000C5
        public virtual System.Void CopyFrom(Unity.VisualScripting.GraphPointer other) { }
        // RVA: 0x0B3592D4  token: 0x60000D0
        public System.Void EnsureChild() { }
        // RVA: 0x0B359350  token: 0x60000D5
        public System.Void EnsureDebugDataAvailable() { }
        // RVA: -1  // generic def  token: 0x60000D8
        public T GetElementDebugData(Unity.VisualScripting.IGraphElementWithDebugData element) { }
        // RVA: 0x0B3593CC  token: 0x60000D9
        protected System.Void ExitParentElement() { }
        // RVA: 0x0B359574  token: 0x60000DB
        public System.Boolean InstanceEquals(Unity.VisualScripting.GraphPointer other) { }
        // RVA: 0x0B3591EC  token: 0x60000DC
        public System.Boolean DefinitionEquals(Unity.VisualScripting.GraphPointer other) { }
        // RVA: 0x0B358990  token: 0x60000DD
        public System.Int32 ComputeHashCode() { }
        // RVA: 0x0B359674  token: 0x60000DE
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000034  // size: 0x98
    public sealed class GraphPointerException : System.Exception
    {
        // Fields
        private readonly Unity.VisualScripting.GraphPointer <pointer>k__BackingField;  // 0x90

        // Methods
        // RVA: 0x0B3588CC  token: 0x60000DF
        public System.Void .ctor(System.String message, Unity.VisualScripting.GraphPointer pointer) { }

    }

    // TypeToken: 0x2000035  // size: 0x50
    public sealed class GraphReference : Unity.VisualScripting.GraphPointer
    {
        // Fields
        private System.Int32 hashCode;  // 0x48
        private static readonly System.Collections.Generic.Dictionary<System.Int32,System.Collections.Generic.List<Unity.VisualScripting.GraphReference>> internPool;  // static @ 0x0

        // Methods
        // RVA: 0x0B35AAF8  token: 0x60000E0
        private static System.Void .cctor() { }
        // RVA: 0x0B35ABD0  token: 0x60000E1
        private System.Void .ctor() { }
        // RVA: 0x0B35A9D8  token: 0x60000E2
        private static Unity.VisualScripting.GraphReference New(Unity.VisualScripting.GraphPointer model) { }
        // RVA: 0x0B35A014  token: 0x60000E3
        public virtual System.Void CopyFrom(Unity.VisualScripting.GraphPointer other) { }
        // RVA: 0x0B359FC8  token: 0x60000E4
        public Unity.VisualScripting.GraphReference Clone() { }
        // RVA: 0x0464E630  token: 0x60000E5
        public virtual Unity.VisualScripting.GraphReference AsReference() { }
        // RVA: 0x0B35AAF0  token: 0x60000E6
        public Unity.VisualScripting.GraphStack ToStackPooled() { }
        // RVA: 0x0B35A07C  token: 0x60000E7
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0B35A6E8  token: 0x60000E8
        private System.Void Hash() { }
        // RVA: 0x04D863D0  token: 0x60000E9
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B35AA40  token: 0x60000EA
        public Unity.VisualScripting.GraphReference ParentReference(System.Boolean ensureValid) { }
        // RVA: 0x0B35A704  token: 0x60000EB
        public static Unity.VisualScripting.GraphReference Intern(Unity.VisualScripting.GraphPointer pointer) { }
        // RVA: 0x0B35A0D8  token: 0x60000EC
        public static System.Void FreeInvalidInterns() { }

    }

    // TypeToken: 0x2000036  // size: 0x10
    public static class GraphsExceptionUtility
    {
        // Methods
        // RVA: 0x0B35BA0C  token: 0x60000ED
        public static System.Void SetException(Unity.VisualScripting.IGraphElementWithDebugData element, Unity.VisualScripting.GraphPointer pointer, System.Exception ex) { }
        // RVA: 0x0B35B67C  token: 0x60000EE
        public static System.Void HandleException(Unity.VisualScripting.IGraphElementWithDebugData element, Unity.VisualScripting.GraphPointer pointer, System.Exception ex) { }
        // RVA: 0x0B35B848  token: 0x60000EF
        private static System.Boolean HandledIn(System.Exception ex, Unity.VisualScripting.GraphReference reference) { }

    }

    // TypeToken: 0x2000037  // size: 0x48
    public sealed class GraphStack : Unity.VisualScripting.GraphPointer, Unity.VisualScripting.IPoolable, System.IDisposable
    {
        // Methods
        // RVA: 0x0B35ABD0  token: 0x60000F0
        private System.Void .ctor() { }
        // RVA: 0x0B35AC34  token: 0x60000F1
        private static Unity.VisualScripting.GraphStack New(Unity.VisualScripting.GraphPointer model) { }
        // RVA: 0x0B35ABE0  token: 0x60000F2
        public virtual System.Void Dispose() { }
        // RVA: 0x041E1670  token: 0x60000F3
        private virtual System.Void Unity.VisualScripting.IPoolable.New() { }
        // RVA: 0x0B35ADB0  token: 0x60000F4
        private virtual System.Void Unity.VisualScripting.IPoolable.Free() { }
        // RVA: 0x0B35ABD8  token: 0x60000F5
        public virtual Unity.VisualScripting.GraphReference AsReference() { }
        // RVA: 0x0B35AD64  token: 0x60000F6
        public Unity.VisualScripting.GraphReference ToReference() { }

    }

    // TypeToken: 0x2000039
    public interface IGraph : System.IDisposable, Unity.VisualScripting.ISerializationDepender, UnityEngine.ISerializationCallbackReceiver
    {
        // Properties
        Unity.VisualScripting.MergedGraphElementCollection elements { get; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x200003A
    public interface IGraphData
    {
    }

    // TypeToken: 0x200003B
    public interface IGraphDebugData
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60000FB
        public virtual Unity.VisualScripting.IGraphElementDebugData GetOrCreateElementData(Unity.VisualScripting.IGraphElementWithDebugData element) { }

    }

    // TypeToken: 0x200003C
    public interface IGraphElement : System.IDisposable, Unity.VisualScripting.IIdentifiable
    {
        // Properties
        System.Int32 dependencyOrder { get; /* RVA: -1  // abstract */ }
        System.Guid guid { get; /* RVA: -1  // abstract */ }
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.ISerializationDependency> deserializationDependencies { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x60000FC
        public virtual System.Boolean HandleDependencies() { }

    }

    // TypeToken: 0x200003D
    public interface IGraphElementDebugData
    {
        // Properties
        System.Exception runtimeException { set; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x200003E
    public interface IGraphElementWithDebugData : Unity.VisualScripting.IGraphElement, System.IDisposable, Unity.VisualScripting.IIdentifiable
    {
    }

    // TypeToken: 0x200003F
    public interface IGraphParent
    {
        // Properties
        Unity.VisualScripting.IGraph childGraph { get; /* RVA: -1  // abstract */ }
        System.Boolean isSerializationRoot { get; /* RVA: -1  // abstract */ }
        UnityEngine.Object serializedObject { get; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x2000040
    public interface IGraphParentElement : Unity.VisualScripting.IGraphElement, System.IDisposable, Unity.VisualScripting.IIdentifiable, Unity.VisualScripting.IGraphParent
    {
    }

    // TypeToken: 0x2000041
    public interface IGraphRoot : Unity.VisualScripting.IGraphParent
    {
    }

    // TypeToken: 0x2000042  // size: 0x20
    public sealed class MergedGraphElementCollection : Unity.VisualScripting.MergedKeyedCollection`2
    {
    }

    // TypeToken: 0x2000043  // size: 0x10
    public static class PlatformUtility
    {
        // Fields
        public static readonly System.Boolean supportsJit;  // static @ 0x0

        // Methods
        // RVA: 0x04D79D90  token: 0x6000104
        private static System.Void .cctor() { }
        // RVA: 0x012081B0  token: 0x6000105
        private static System.Boolean CheckJitSupport() { }

    }

    // TypeToken: 0x2000044
    public static class GenericPool`1
    {
        // Fields
        private static readonly System.Object lock;  // static @ 0x0
        private static readonly System.Collections.Generic.Stack<T> free;  // static @ 0x0
        private static readonly System.Collections.Generic.HashSet<T> busy;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000106
        public static T New(System.Func<T> constructor) { }
        // RVA: -1  // not resolved  token: 0x6000107
        public static System.Void Free(T item) { }
        // RVA: -1  // not resolved  token: 0x6000108
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000045
    public interface IPoolable
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000109
        public virtual System.Void New() { }
        // RVA: -1  // abstract  token: 0x600010A
        public virtual System.Void Free() { }

    }

    // TypeToken: 0x2000046
    public static class ListPool`1
    {
        // Fields
        private static readonly System.Object lock;  // static @ 0x0
        private static readonly System.Collections.Generic.Stack<System.Collections.Generic.List<T>> free;  // static @ 0x0
        private static readonly System.Collections.Generic.HashSet<System.Collections.Generic.List<T>> busy;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600010B
        public static System.Collections.Generic.List<T> New() { }
        // RVA: -1  // not resolved  token: 0x600010C
        public static System.Void Free(System.Collections.Generic.List<T> list) { }
        // RVA: -1  // not resolved  token: 0x600010D
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000047  // size: 0x10
    public static class XListPool
    {
        // Methods
        // RVA: -1  // generic def  token: 0x600010E
        public static System.Void Free(System.Collections.Generic.List<T> list) { }

    }

    // TypeToken: 0x2000048  // size: 0x30
    public class ProfiledSegment
    {
        // Fields
        private System.String <name>k__BackingField;  // 0x10
        private System.Diagnostics.Stopwatch <stopwatch>k__BackingField;  // 0x18
        private Unity.VisualScripting.ProfiledSegment <parent>k__BackingField;  // 0x20
        private Unity.VisualScripting.ProfiledSegmentCollection <children>k__BackingField;  // 0x28

        // Properties
        System.String name { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        System.Diagnostics.Stopwatch stopwatch { set; /* RVA: 0x05392C40 */ }
        Unity.VisualScripting.ProfiledSegment parent { set; /* RVA: 0x05390F40 */ }
        Unity.VisualScripting.ProfiledSegmentCollection children { set; /* RVA: 0x02FDB880 */ }

        // Methods
        // RVA: 0x0B360B80  token: 0x600010F
        public System.Void .ctor(Unity.VisualScripting.ProfiledSegment parent, System.String name) { }

    }

    // TypeToken: 0x2000049  // size: 0x30
    public class ProfiledSegmentCollection : System.Collections.ObjectModel.KeyedCollection`2
    {
        // Methods
        // RVA: 0x06C08920  token: 0x6000115
        protected virtual System.String GetKeyForItem(Unity.VisualScripting.ProfiledSegment item) { }
        // RVA: 0x0B360B44  token: 0x6000116
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200004A  // size: 0x11
    public sealed struct ProfilingScope : System.IDisposable
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000117
        public System.Void .ctor(System.String name) { }
        // RVA: 0x041E1670  token: 0x6000118
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x200004B  // size: 0x10
    public static class ProfilingUtility
    {
        // Fields
        private static readonly System.Object lock;  // static @ 0x0
        private static Unity.VisualScripting.ProfiledSegment <rootSegment>k__BackingField;  // static @ 0x8
        private static Unity.VisualScripting.ProfiledSegment <currentSegment>k__BackingField;  // static @ 0x10

        // Properties
        Unity.VisualScripting.ProfiledSegment rootSegment { set; /* RVA: 0x0B360DF0 */ }
        Unity.VisualScripting.ProfiledSegment currentSegment { set; /* RVA: 0x0B360D90 */ }

        // Methods
        // RVA: 0x0B360C38  token: 0x6000119
        private static System.Void .cctor() { }
        // RVA: 0x012081B0  token: 0x600011C
        public static Unity.VisualScripting.ProfilingScope SampleBlock(System.String name) { }

    }

    // TypeToken: 0x200004C  // size: 0x14
    public sealed struct ActionDirection
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Unity.VisualScripting.ActionDirection Any;  // const
        public static Unity.VisualScripting.ActionDirection Get;  // const
        public static Unity.VisualScripting.ActionDirection Set;  // const

    }

    // TypeToken: 0x200004D  // size: 0x10
    public static class AttributeUtility
    {
        // Fields
        private static readonly System.Collections.Generic.Dictionary<System.Object,Unity.VisualScripting.AttributeUtility.AttributeCache> optimizedCaches;  // static @ 0x0

        // Methods
        // RVA: 0x0B352F6C  token: 0x600011D
        private static Unity.VisualScripting.AttributeUtility.AttributeCache GetAttributeCache(System.Reflection.MemberInfo element) { }
        // RVA: -1  // generic def  token: 0x600011E
        private static System.Collections.Generic.IEnumerable<T> GetAttributeOfEnumMember(System.Enum enumVal) { }
        // RVA: -1  // generic def  token: 0x600011F
        public static System.Boolean HasAttribute(System.Reflection.MemberInfo element, System.Boolean inherit) { }
        // RVA: -1  // generic def  token: 0x6000120
        public static TAttribute GetAttribute(System.Reflection.MemberInfo element, System.Boolean inherit) { }
        // RVA: 0x0B3531B4  token: 0x6000121
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200004F  // size: 0x10
    public static class ConversionUtility
    {
        // Fields
        private static readonly System.Collections.Generic.Dictionary<Unity.VisualScripting.ConversionUtility.ConversionQuery,Unity.VisualScripting.ConversionUtility.ConversionType> conversionTypesCache;  // static @ 0x0
        private static readonly System.Collections.Generic.Dictionary<Unity.VisualScripting.ConversionUtility.ConversionQuery,System.Reflection.MethodInfo[]> userConversionMethodsCache;  // static @ 0x8
        private static readonly System.Collections.Generic.Dictionary<System.Type,System.Collections.Generic.HashSet<System.Type>> implicitNumericConversions;  // static @ 0x10
        private static readonly System.Collections.Generic.Dictionary<System.Type,System.Collections.Generic.HashSet<System.Type>> explicitNumericConversions;  // static @ 0x18

        // Methods
        // RVA: 0x0B35517C  token: 0x600012C
        private static System.Boolean RespectsIdentity(System.Type source, System.Type destination) { }
        // RVA: 0x0AD1AECC  token: 0x600012D
        private static System.Boolean IsUpcast(System.Type source, System.Type destination) { }
        // RVA: 0x0AC76A90  token: 0x600012E
        private static System.Boolean IsDowncast(System.Type source, System.Type destination) { }
        // RVA: 0x0B354DD0  token: 0x600012F
        public static System.Boolean HasImplicitNumericConversion(System.Type source, System.Type destination) { }
        // RVA: 0x0B354CEC  token: 0x6000130
        public static System.Boolean HasExplicitNumericConversion(System.Type source, System.Type destination) { }
        // RVA: 0x0B354EB4  token: 0x6000131
        public static System.Boolean HasNumericConversion(System.Type source, System.Type destination) { }
        // RVA: 0x0B354190  token: 0x6000132
        private static System.Collections.Generic.IEnumerable<System.Reflection.MethodInfo> FindUserDefinedConversionMethods(Unity.VisualScripting.ConversionUtility.ConversionQuery query) { }
        // RVA: 0x0B35462C  token: 0x6000133
        private static System.Reflection.MethodInfo[] GetUserDefinedConversionMethods(System.Type source, System.Type destination) { }
        // RVA: 0x0B3547AC  token: 0x6000134
        private static Unity.VisualScripting.ConversionUtility.ConversionType GetUserDefinedConversionType(System.Type source, System.Type destination) { }
        // RVA: 0x0B354AF4  token: 0x6000135
        private static System.Boolean HasEnumerableToArrayConversion(System.Type source, System.Type destination) { }
        // RVA: 0x0B354BD8  token: 0x6000136
        private static System.Boolean HasEnumerableToListConversion(System.Type source, System.Type destination) { }
        // RVA: 0x0B354F28  token: 0x6000137
        private static System.Boolean HasUnityHierarchyConversion(System.Type source, System.Type destination) { }
        // RVA: 0x0B35510C  token: 0x6000138
        private static System.Boolean IsValidConversion(Unity.VisualScripting.ConversionUtility.ConversionType conversionType, System.Boolean guaranteed) { }
        // RVA: 0x0B3537C8  token: 0x6000139
        public static System.Boolean CanConvert(System.Object value, System.Type type, System.Boolean guaranteed) { }
        // RVA: 0x0B35375C  token: 0x600013A
        public static System.Boolean CanConvert(System.Type source, System.Type destination, System.Boolean guaranteed) { }
        // RVA: 0x0B353834  token: 0x600013B
        public static System.Object Convert(System.Object value, System.Type type) { }
        // RVA: 0x0B3550AC  token: 0x600013C
        public static System.Boolean IsConvertibleTo(System.Type source, System.Type destination, System.Boolean guaranteed) { }
        // RVA: 0x0B35504C  token: 0x600013D
        public static System.Boolean IsConvertibleTo(System.Object source, System.Type type, System.Boolean guaranteed) { }
        // RVA: 0x0B354500  token: 0x600013E
        public static Unity.VisualScripting.ConversionUtility.ConversionType GetRequiredConversion(System.Type source, System.Type destination) { }
        // RVA: 0x0B353C8C  token: 0x600013F
        private static Unity.VisualScripting.ConversionUtility.ConversionType DetermineConversionType(Unity.VisualScripting.ConversionUtility.ConversionQuery query) { }
        // RVA: 0x0B354438  token: 0x6000140
        public static Unity.VisualScripting.ConversionUtility.ConversionType GetRequiredConversion(System.Object value, System.Type type) { }
        // RVA: 0x0B355120  token: 0x6000141
        private static System.Object NumericConversion(System.Object value, System.Type type) { }
        // RVA: 0x0B3553C0  token: 0x6000142
        private static System.Object UserDefinedConversion(Unity.VisualScripting.ConversionUtility.ConversionType conversion, System.Object value, System.Type type) { }
        // RVA: 0x0B353ECC  token: 0x6000143
        private static System.Object EnumerableToArrayConversion(System.Object value, System.Type arrayType) { }
        // RVA: 0x0B354004  token: 0x6000144
        private static System.Object EnumerableToListConversion(System.Object value, System.Type listType) { }
        // RVA: 0x0B3551D0  token: 0x6000145
        private static System.Object UnityHierarchyConversion(System.Object value, System.Type type) { }
        // RVA: 0x0B353898  token: 0x6000146
        private static System.Object Convert(System.Object value, System.Type type, Unity.VisualScripting.ConversionUtility.ConversionType conversionType) { }
        // RVA: 0x0B355574  token: 0x6000147
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000056  // size: 0x18
    public sealed struct LooseAssemblyName
    {
        // Fields
        public readonly System.String name;  // 0x10

        // Methods
        // RVA: 0x0B35C4DC  token: 0x6000158
        public System.Void .ctor(System.String name) { }
        // RVA: 0x0B35C43C  token: 0x6000159
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0B35C4A0  token: 0x600015A
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x02B76770  token: 0x600015B
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000057  // size: 0x10
    public static class MemberUtility
    {
        // Fields
        private static readonly System.Lazy<Unity.VisualScripting.ExtensionMethodCache> ExtensionMethodsCache;  // static @ 0x0
        private static readonly System.Lazy<System.Collections.Generic.Dictionary<System.Type,System.Reflection.MethodInfo[]>> InheritedExtensionMethodsCache;  // static @ 0x8
        private static readonly System.Lazy<System.Collections.Generic.HashSet<System.Reflection.MethodInfo>> GenericExtensionMethods;  // static @ 0x10

        // Methods
        // RVA: 0x0B35CCB0  token: 0x600015C
        private static System.Void .cctor() { }
        // RVA: 0x0B35CC10  token: 0x600015D
        public static System.Boolean IsUserDefinedConversion(System.Reflection.MethodInfo method) { }
        // RVA: 0x0B35C9F4  token: 0x600015E
        public static System.Boolean IsExtension(System.Reflection.MethodInfo methodInfo) { }
        // RVA: 0x0B35CA48  token: 0x600015F
        public static System.Boolean IsStatic(System.Reflection.PropertyInfo propertyInfo) { }
        // RVA: 0x0B35CAB0  token: 0x6000160
        public static System.Boolean IsStatic(System.Reflection.MemberInfo memberInfo) { }
        // RVA: 0x0B35C9BC  token: 0x6000161
        public static System.Boolean CanWrite(System.Reflection.FieldInfo fieldInfo) { }

    }

    // TypeToken: 0x2000059  // size: 0x18
    public class ExtensionMethodCache
    {
        // Fields
        private readonly System.Reflection.MethodInfo[] Cache;  // 0x10

        // Methods
        // RVA: 0x0B35829C  token: 0x6000167
        private System.Void .ctor() { }

    }

    // TypeToken: 0x200005B  // size: 0x38
    public sealed class Namespace
    {
        // Fields
        private readonly Unity.VisualScripting.Namespace <Root>k__BackingField;  // 0x10
        private readonly Unity.VisualScripting.Namespace <Parent>k__BackingField;  // 0x18
        private readonly System.String <FullName>k__BackingField;  // 0x20
        private readonly System.String <Name>k__BackingField;  // 0x28
        private readonly System.Boolean <IsRoot>k__BackingField;  // 0x30
        private readonly System.Boolean <IsGlobal>k__BackingField;  // 0x31
        private static readonly Unity.VisualScripting.Namespace.Collection collection;  // static @ 0x0
        private static readonly Unity.VisualScripting.Namespace <Global>k__BackingField;  // static @ 0x8

        // Properties
        System.String FullName { get; /* RVA: 0x04D862C0 */ }
        Unity.VisualScripting.Namespace Global { get; /* RVA: 0x0B35D57C */ }

        // Methods
        // RVA: 0x0B35D438  token: 0x600016D
        private System.Void .ctor(System.String fullName) { }
        // RVA: 0x0B35D348  token: 0x600016F
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x04D862C0  token: 0x6000170
        public virtual System.String ToString() { }
        // RVA: 0x0B35D370  token: 0x6000171
        private static System.Void .cctor() { }
        // RVA: 0x0B35D254  token: 0x6000173
        public static Unity.VisualScripting.Namespace FromFullName(System.String fullName) { }
        // RVA: 0x0B35D1C8  token: 0x6000174
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0B35D5CC  token: 0x6000175
        public static Unity.VisualScripting.Namespace op_Implicit(System.String fullName) { }
        // RVA: 0x0A7BED8C  token: 0x6000176
        public static System.Boolean op_Equality(Unity.VisualScripting.Namespace a, Unity.VisualScripting.Namespace b) { }

    }

    // TypeToken: 0x200005D
    public sealed class Action`5 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x600017C
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x600017D
        public virtual System.Void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) { }

    }

    // TypeToken: 0x200005E
    public sealed class Action`6 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x600017E
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x600017F
        public virtual System.Void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) { }

    }

    // TypeToken: 0x200005F
    public sealed class Func`6 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000180
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000181
        public virtual TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5) { }

    }

    // TypeToken: 0x2000060
    public sealed class Func`7 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000182
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000183
        public virtual TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) { }

    }

    // TypeToken: 0x2000061
    public abstract class InstanceActionInvokerBase`1 : Unity.VisualScripting.InstanceInvokerBase`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x6000184
        protected System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }

    }

    // TypeToken: 0x2000062
    public sealed class InstanceActionInvoker`1 : Unity.VisualScripting.InstanceActionInvokerBase`1
    {
        // Fields
        private System.Action<TTarget> invoke;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000185
        public System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }
        // RVA: -1  // not resolved  token: 0x6000186
        protected virtual System.Type[] GetParameterTypes() { }
        // RVA: -1  // not resolved  token: 0x6000187
        protected virtual System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions) { }
        // RVA: -1  // not resolved  token: 0x6000188
        protected virtual System.Void CreateDelegate() { }

    }

    // TypeToken: 0x2000063
    public sealed class InstanceActionInvoker`2 : Unity.VisualScripting.InstanceActionInvokerBase`1
    {
        // Fields
        private System.Action<TTarget,TParam0> invoke;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000189
        public System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }
        // RVA: -1  // not resolved  token: 0x600018A
        public virtual System.Object Invoke(System.Object target, System.Object arg0) { }
        // RVA: -1  // not resolved  token: 0x600018B
        private System.Object InvokeUnsafe(System.Object target, System.Object arg0) { }
        // RVA: -1  // not resolved  token: 0x600018C
        protected virtual System.Type[] GetParameterTypes() { }
        // RVA: -1  // not resolved  token: 0x600018D
        protected virtual System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions) { }
        // RVA: -1  // not resolved  token: 0x600018E
        protected virtual System.Void CreateDelegate() { }

    }

    // TypeToken: 0x2000064
    public sealed class InstanceActionInvoker`3 : Unity.VisualScripting.InstanceActionInvokerBase`1
    {
        // Fields
        private System.Action<TTarget,TParam0,TParam1> invoke;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600018F
        public System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }
        // RVA: -1  // not resolved  token: 0x6000190
        protected virtual System.Type[] GetParameterTypes() { }
        // RVA: -1  // not resolved  token: 0x6000191
        protected virtual System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions) { }
        // RVA: -1  // not resolved  token: 0x6000192
        protected virtual System.Void CreateDelegate() { }

    }

    // TypeToken: 0x2000065
    public sealed class InstanceActionInvoker`4 : Unity.VisualScripting.InstanceActionInvokerBase`1
    {
        // Fields
        private System.Action<TTarget,TParam0,TParam1,TParam2> invoke;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000193
        public System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }
        // RVA: -1  // not resolved  token: 0x6000194
        protected virtual System.Type[] GetParameterTypes() { }
        // RVA: -1  // not resolved  token: 0x6000195
        protected virtual System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions) { }
        // RVA: -1  // not resolved  token: 0x6000196
        protected virtual System.Void CreateDelegate() { }

    }

    // TypeToken: 0x2000066
    public sealed class InstanceActionInvoker`5 : Unity.VisualScripting.InstanceActionInvokerBase`1
    {
        // Fields
        private Unity.VisualScripting.Action<TTarget,TParam0,TParam1,TParam2,TParam3> invoke;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000197
        public System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }
        // RVA: -1  // not resolved  token: 0x6000198
        protected virtual System.Type[] GetParameterTypes() { }
        // RVA: -1  // not resolved  token: 0x6000199
        protected virtual System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions) { }
        // RVA: -1  // not resolved  token: 0x600019A
        protected virtual System.Void CreateDelegate() { }

    }

    // TypeToken: 0x2000067
    public sealed class InstanceActionInvoker`6 : Unity.VisualScripting.InstanceActionInvokerBase`1
    {
        // Fields
        private Unity.VisualScripting.Action<TTarget,TParam0,TParam1,TParam2,TParam3,TParam4> invoke;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600019B
        public System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }
        // RVA: -1  // not resolved  token: 0x600019C
        protected virtual System.Type[] GetParameterTypes() { }
        // RVA: -1  // not resolved  token: 0x600019D
        protected virtual System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions) { }
        // RVA: -1  // not resolved  token: 0x600019E
        protected virtual System.Void CreateDelegate() { }

    }

    // TypeToken: 0x2000068
    public class InstanceFieldAccessor`2 : Unity.VisualScripting.IOptimizedAccessor
    {
        // Fields
        private readonly System.Reflection.FieldInfo fieldInfo;  // 0x0
        private System.Func<TTarget,TField> getter;  // 0x0
        private System.Action<TTarget,TField> setter;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600019F
        public System.Void .ctor(System.Reflection.FieldInfo fieldInfo) { }
        // RVA: -1  // not resolved  token: 0x60001A0
        public virtual System.Void Compile() { }
        // RVA: -1  // not resolved  token: 0x60001A1
        public virtual System.Object GetValue(System.Object target) { }
        // RVA: -1  // not resolved  token: 0x60001A2
        private System.Object GetValueUnsafe(System.Object target) { }
        // RVA: -1  // not resolved  token: 0x60001A3
        public virtual System.Void SetValue(System.Object target, System.Object value) { }
        // RVA: -1  // not resolved  token: 0x60001A4
        private System.Void SetValueUnsafe(System.Object target, System.Object value) { }
        // RVA: -1  // not resolved  token: 0x60001A5
        private TField <Compile>b__4_0(TTarget instance) { }
        // RVA: -1  // not resolved  token: 0x60001A6
        private System.Void <Compile>b__4_1(TTarget instance, TField value) { }

    }

    // TypeToken: 0x2000069
    public abstract class InstanceFunctionInvokerBase`2 : Unity.VisualScripting.InstanceInvokerBase`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60001A7
        protected System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }

    }

    // TypeToken: 0x200006A
    public sealed class InstanceFunctionInvoker`2 : Unity.VisualScripting.InstanceFunctionInvokerBase`2
    {
        // Fields
        private System.Func<TTarget,TResult> invoke;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60001A8
        public System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }
        // RVA: -1  // not resolved  token: 0x60001A9
        protected virtual System.Type[] GetParameterTypes() { }
        // RVA: -1  // not resolved  token: 0x60001AA
        protected virtual System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions) { }
        // RVA: -1  // not resolved  token: 0x60001AB
        protected virtual System.Void CreateDelegate() { }

    }

    // TypeToken: 0x200006B
    public sealed class InstanceFunctionInvoker`3 : Unity.VisualScripting.InstanceFunctionInvokerBase`2
    {
        // Fields
        private System.Func<TTarget,TParam0,TResult> invoke;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60001AC
        public System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }
        // RVA: -1  // not resolved  token: 0x60001AD
        public virtual System.Object Invoke(System.Object target, System.Object arg0) { }
        // RVA: -1  // not resolved  token: 0x60001AE
        public System.Object InvokeUnsafe(System.Object target, System.Object arg0) { }
        // RVA: -1  // not resolved  token: 0x60001AF
        protected virtual System.Type[] GetParameterTypes() { }
        // RVA: -1  // not resolved  token: 0x60001B0
        protected virtual System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions) { }
        // RVA: -1  // not resolved  token: 0x60001B1
        protected virtual System.Void CreateDelegate() { }

    }

    // TypeToken: 0x200006C
    public sealed class InstanceFunctionInvoker`4 : Unity.VisualScripting.InstanceFunctionInvokerBase`2
    {
        // Fields
        private System.Func<TTarget,TParam0,TParam1,TResult> invoke;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60001B2
        public System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }
        // RVA: -1  // not resolved  token: 0x60001B3
        protected virtual System.Type[] GetParameterTypes() { }
        // RVA: -1  // not resolved  token: 0x60001B4
        protected virtual System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions) { }
        // RVA: -1  // not resolved  token: 0x60001B5
        protected virtual System.Void CreateDelegate() { }

    }

    // TypeToken: 0x200006D
    public sealed class InstanceFunctionInvoker`5 : Unity.VisualScripting.InstanceFunctionInvokerBase`2
    {
        // Fields
        private System.Func<TTarget,TParam0,TParam1,TParam2,TResult> invoke;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60001B6
        public System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }
        // RVA: -1  // not resolved  token: 0x60001B7
        protected virtual System.Type[] GetParameterTypes() { }
        // RVA: -1  // not resolved  token: 0x60001B8
        protected virtual System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions) { }
        // RVA: -1  // not resolved  token: 0x60001B9
        protected virtual System.Void CreateDelegate() { }

    }

    // TypeToken: 0x200006E
    public sealed class InstanceFunctionInvoker`6 : Unity.VisualScripting.InstanceFunctionInvokerBase`2
    {
        // Fields
        private Unity.VisualScripting.Func<TTarget,TParam0,TParam1,TParam2,TParam3,TResult> invoke;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60001BA
        public System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }
        // RVA: -1  // not resolved  token: 0x60001BB
        protected virtual System.Type[] GetParameterTypes() { }
        // RVA: -1  // not resolved  token: 0x60001BC
        protected virtual System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions) { }
        // RVA: -1  // not resolved  token: 0x60001BD
        protected virtual System.Void CreateDelegate() { }

    }

    // TypeToken: 0x200006F
    public sealed class InstanceFunctionInvoker`7 : Unity.VisualScripting.InstanceFunctionInvokerBase`2
    {
        // Fields
        private Unity.VisualScripting.Func<TTarget,TParam0,TParam1,TParam2,TParam3,TParam4,TResult> invoke;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60001BE
        public System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }
        // RVA: -1  // not resolved  token: 0x60001BF
        protected virtual System.Type[] GetParameterTypes() { }
        // RVA: -1  // not resolved  token: 0x60001C0
        protected virtual System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions) { }
        // RVA: -1  // not resolved  token: 0x60001C1
        protected virtual System.Void CreateDelegate() { }

    }

    // TypeToken: 0x2000070
    public abstract class InstanceInvokerBase`1 : Unity.VisualScripting.InvokerBase
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60001C2
        protected System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }
        // RVA: -1  // not resolved  token: 0x60001C3
        protected virtual System.Void CompileExpression() { }
        // RVA: -1  // abstract  token: 0x60001C4
        protected virtual System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions) { }
        // RVA: -1  // not resolved  token: 0x60001C5
        protected virtual System.Void VerifyTarget(System.Object target) { }

    }

    // TypeToken: 0x2000071
    public class InstancePropertyAccessor`2 : Unity.VisualScripting.IOptimizedAccessor
    {
        // Fields
        private readonly System.Reflection.PropertyInfo propertyInfo;  // 0x0
        private System.Func<TTarget,TProperty> getter;  // 0x0
        private System.Action<TTarget,TProperty> setter;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60001C6
        public System.Void .ctor(System.Reflection.PropertyInfo propertyInfo) { }
        // RVA: -1  // not resolved  token: 0x60001C7
        public virtual System.Void Compile() { }
        // RVA: -1  // not resolved  token: 0x60001C8
        public virtual System.Object GetValue(System.Object target) { }
        // RVA: -1  // not resolved  token: 0x60001C9
        private System.Object GetValueUnsafe(System.Object target) { }
        // RVA: -1  // not resolved  token: 0x60001CA
        public virtual System.Void SetValue(System.Object target, System.Object value) { }
        // RVA: -1  // not resolved  token: 0x60001CB
        private System.Void SetValueUnsafe(System.Object target, System.Object value) { }

    }

    // TypeToken: 0x2000072  // size: 0x20
    public abstract class InvokerBase : Unity.VisualScripting.IOptimizedInvoker
    {
        // Fields
        protected readonly System.Type targetType;  // 0x10
        protected readonly System.Reflection.MethodInfo methodInfo;  // 0x18

        // Methods
        // RVA: 0x0B35BECC  token: 0x60001CC
        protected System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }
        // RVA: -1  // generic def  token: 0x60001CD
        protected System.Void VerifyArgument(System.Reflection.MethodInfo methodInfo, System.Int32 argIndex, System.Object arg) { }
        // RVA: 0x0B35BBD0  token: 0x60001CE
        public virtual System.Void Compile() { }
        // RVA: 0x0B35BC20  token: 0x60001CF
        protected System.Linq.Expressions.ParameterExpression[] GetParameterExpressions() { }
        // RVA: -1  // abstract  token: 0x60001D0
        protected virtual System.Type[] GetParameterTypes() { }
        // RVA: 0x0B35BE80  token: 0x60001D1
        public virtual System.Object Invoke(System.Object target, System.Object arg0) { }
        // RVA: -1  // abstract  token: 0x60001D2
        protected virtual System.Void CompileExpression() { }
        // RVA: -1  // abstract  token: 0x60001D3
        protected virtual System.Void CreateDelegate() { }
        // RVA: -1  // abstract  token: 0x60001D4
        protected virtual System.Void VerifyTarget(System.Object target) { }

    }

    // TypeToken: 0x2000073
    public interface IOptimizedAccessor
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60001D5
        public virtual System.Void Compile() { }
        // RVA: -1  // abstract  token: 0x60001D6
        public virtual System.Object GetValue(System.Object target) { }
        // RVA: -1  // abstract  token: 0x60001D7
        public virtual System.Void SetValue(System.Object target, System.Object value) { }

    }

    // TypeToken: 0x2000074
    public interface IOptimizedInvoker
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60001D8
        public virtual System.Void Compile() { }
        // RVA: -1  // abstract  token: 0x60001D9
        public virtual System.Object Invoke(System.Object target, System.Object arg0) { }

    }

    // TypeToken: 0x2000075  // size: 0x10
    public static class OptimizedReflection
    {
        // Fields
        private static readonly System.Collections.Generic.Dictionary<System.Reflection.FieldInfo,Unity.VisualScripting.IOptimizedAccessor> fieldAccessors;  // static @ 0x0
        private static readonly System.Collections.Generic.Dictionary<System.Reflection.PropertyInfo,Unity.VisualScripting.IOptimizedAccessor> propertyAccessors;  // static @ 0x8
        private static readonly System.Collections.Generic.Dictionary<System.Reflection.MethodInfo,Unity.VisualScripting.IOptimizedInvoker> methodInvokers;  // static @ 0x10
        public static readonly System.Boolean jitAvailable;  // static @ 0x18
        private static System.Boolean _useJitIfAvailable;  // static @ 0x19
        private static System.Boolean <safeMode>k__BackingField;  // static @ 0x1a

        // Properties
        System.Boolean useJit { get; /* RVA: 0x0B360A68 */ }
        System.Boolean useJitIfAvailable { get; /* RVA: 0x0B360A18 */ }
        System.Boolean safeMode { get; /* RVA: 0x0B3609C8 */ set; /* RVA: 0x0B360AF0 */ }

        // Methods
        // RVA: 0x04A2CBC0  token: 0x60001DA
        private static System.Void .cctor() { }
        // RVA: 0x03FCE770  token: 0x60001DF
        private static System.Void OnRuntimeMethodLoad() { }
        // RVA: 0x0B360764  token: 0x60001E0
        private static System.Void VerifyStaticTarget(System.Type targetType, System.Object target) { }
        // RVA: -1  // generic def  token: 0x60001E1
        private static System.Void VerifyInstanceTarget(System.Object target) { }
        // RVA: 0x0B3607B8  token: 0x60001E2
        private static System.Void VerifyTarget(System.Type targetType, System.Object target, System.Boolean static) { }
        // RVA: 0x0B360498  token: 0x60001E3
        private static System.Boolean SupportsOptimization(System.Reflection.MemberInfo memberInfo) { }
        // RVA: 0x0B3602CC  token: 0x60001E4
        public static Unity.VisualScripting.IOptimizedAccessor Prewarm(System.Reflection.FieldInfo fieldInfo) { }
        // RVA: 0x0B3603B0  token: 0x60001E5
        public static System.Void SetValueOptimized(System.Reflection.FieldInfo fieldInfo, System.Object target, System.Object value) { }
        // RVA: 0x0B36071C  token: 0x60001E6
        public static System.Boolean SupportsOptimization(System.Reflection.FieldInfo fieldInfo) { }
        // RVA: 0x0B35D618  token: 0x60001E7
        private static Unity.VisualScripting.IOptimizedAccessor GetFieldAccessor(System.Reflection.FieldInfo fieldInfo) { }
        // RVA: 0x0B360318  token: 0x60001E8
        public static Unity.VisualScripting.IOptimizedAccessor Prewarm(System.Reflection.PropertyInfo propertyInfo) { }
        // RVA: 0x0B360424  token: 0x60001E9
        public static System.Void SetValueOptimized(System.Reflection.PropertyInfo propertyInfo, System.Object target, System.Object value) { }
        // RVA: 0x0B3606D4  token: 0x60001EA
        public static System.Boolean SupportsOptimization(System.Reflection.PropertyInfo propertyInfo) { }
        // RVA: 0x0B35FD80  token: 0x60001EB
        private static Unity.VisualScripting.IOptimizedAccessor GetPropertyAccessor(System.Reflection.PropertyInfo propertyInfo) { }
        // RVA: 0x0B360364  token: 0x60001EC
        public static Unity.VisualScripting.IOptimizedInvoker Prewarm(System.Reflection.MethodInfo methodInfo) { }
        // RVA: 0x0B360258  token: 0x60001ED
        public static System.Object InvokeOptimized(System.Reflection.MethodInfo methodInfo, System.Object target, System.Object arg0) { }
        // RVA: 0x0B360510  token: 0x60001EE
        public static System.Boolean SupportsOptimization(System.Reflection.MethodInfo methodInfo) { }
        // RVA: 0x0B35DAC8  token: 0x60001EF
        private static Unity.VisualScripting.IOptimizedInvoker GetMethodInvoker(System.Reflection.MethodInfo methodInfo) { }

    }

    // TypeToken: 0x2000077  // size: 0x18
    public sealed class ReflectionFieldAccessor : Unity.VisualScripting.IOptimizedAccessor
    {
        // Fields
        private readonly System.Reflection.FieldInfo fieldInfo;  // 0x10

        // Methods
        // RVA: 0x0B362078  token: 0x60001F3
        public System.Void .ctor(System.Reflection.FieldInfo fieldInfo) { }
        // RVA: 0x041E1670  token: 0x60001F4
        public virtual System.Void Compile() { }
        // RVA: 0x08B984D4  token: 0x60001F5
        public virtual System.Object GetValue(System.Object target) { }
        // RVA: 0x07C27D38  token: 0x60001F6
        public virtual System.Void SetValue(System.Object target, System.Object value) { }

    }

    // TypeToken: 0x2000078  // size: 0x18
    public class ReflectionInvoker : Unity.VisualScripting.IOptimizedInvoker
    {
        // Fields
        private readonly System.Reflection.MethodInfo methodInfo;  // 0x10
        private static readonly System.Object[] EmptyObjects;  // static @ 0x0

        // Methods
        // RVA: 0x0B362230  token: 0x60001F7
        public System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }
        // RVA: 0x041E1670  token: 0x60001F8
        public virtual System.Void Compile() { }
        // RVA: 0x0B36212C  token: 0x60001F9
        public virtual System.Object Invoke(System.Object target, System.Object arg0) { }
        // RVA: 0x0B3621CC  token: 0x60001FA
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000079  // size: 0x18
    public sealed class ReflectionPropertyAccessor : Unity.VisualScripting.IOptimizedAccessor
    {
        // Fields
        private readonly System.Reflection.PropertyInfo propertyInfo;  // 0x10

        // Methods
        // RVA: 0x0B3622E4  token: 0x60001FB
        public System.Void .ctor(System.Reflection.PropertyInfo propertyInfo) { }
        // RVA: 0x041E1670  token: 0x60001FC
        public virtual System.Void Compile() { }
        // RVA: 0x08B95960  token: 0x60001FD
        public virtual System.Object GetValue(System.Object target) { }
        // RVA: 0x09171000  token: 0x60001FE
        public virtual System.Void SetValue(System.Object target, System.Object value) { }

    }

    // TypeToken: 0x200007A  // size: 0x20
    public abstract class StaticActionInvokerBase : Unity.VisualScripting.StaticInvokerBase
    {
        // Methods
        // RVA: 0x092E798C  token: 0x60001FF
        protected System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }

    }

    // TypeToken: 0x200007B  // size: 0x28
    public sealed class StaticActionInvoker : Unity.VisualScripting.StaticActionInvokerBase
    {
        // Fields
        private System.Action invoke;  // 0x20

        // Methods
        // RVA: 0x092E798C  token: 0x6000200
        public System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }
        // RVA: 0x0B367890  token: 0x6000201
        protected virtual System.Type[] GetParameterTypes() { }
        // RVA: 0x0B367774  token: 0x6000202
        protected virtual System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions) { }
        // RVA: 0x0B367814  token: 0x6000203
        protected virtual System.Void CreateDelegate() { }
        // RVA: 0x0B3678E0  token: 0x6000204
        private System.Void <CreateDelegate>b__7_0() { }

    }

    // TypeToken: 0x200007C
    public sealed class StaticActionInvoker`1 : Unity.VisualScripting.StaticActionInvokerBase
    {
        // Fields
        private System.Action<TParam0> invoke;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000205
        public System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }
        // RVA: -1  // not resolved  token: 0x6000206
        public virtual System.Object Invoke(System.Object target, System.Object arg0) { }
        // RVA: -1  // not resolved  token: 0x6000207
        private System.Object InvokeUnsafe(System.Object target, System.Object arg0) { }
        // RVA: -1  // not resolved  token: 0x6000208
        protected virtual System.Type[] GetParameterTypes() { }
        // RVA: -1  // not resolved  token: 0x6000209
        protected virtual System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions) { }
        // RVA: -1  // not resolved  token: 0x600020A
        protected virtual System.Void CreateDelegate() { }
        // RVA: -1  // not resolved  token: 0x600020B
        private System.Void <CreateDelegate>b__7_0(TParam0 param0) { }

    }

    // TypeToken: 0x200007D
    public sealed class StaticActionInvoker`2 : Unity.VisualScripting.StaticActionInvokerBase
    {
        // Fields
        private System.Action<TParam0,TParam1> invoke;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600020C
        public System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }
        // RVA: -1  // not resolved  token: 0x600020D
        protected virtual System.Type[] GetParameterTypes() { }
        // RVA: -1  // not resolved  token: 0x600020E
        protected virtual System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions) { }
        // RVA: -1  // not resolved  token: 0x600020F
        protected virtual System.Void CreateDelegate() { }
        // RVA: -1  // not resolved  token: 0x6000210
        private System.Void <CreateDelegate>b__7_0(TParam0 param0, TParam1 param1) { }

    }

    // TypeToken: 0x200007E
    public sealed class StaticActionInvoker`3 : Unity.VisualScripting.StaticActionInvokerBase
    {
        // Fields
        private System.Action<TParam0,TParam1,TParam2> invoke;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000211
        public System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }
        // RVA: -1  // not resolved  token: 0x6000212
        protected virtual System.Type[] GetParameterTypes() { }
        // RVA: -1  // not resolved  token: 0x6000213
        protected virtual System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions) { }
        // RVA: -1  // not resolved  token: 0x6000214
        protected virtual System.Void CreateDelegate() { }
        // RVA: -1  // not resolved  token: 0x6000215
        private System.Void <CreateDelegate>b__7_0(TParam0 param0, TParam1 param1, TParam2 param2) { }

    }

    // TypeToken: 0x200007F
    public sealed class StaticActionInvoker`4 : Unity.VisualScripting.StaticActionInvokerBase
    {
        // Fields
        private System.Action<TParam0,TParam1,TParam2,TParam3> invoke;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000216
        public System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }
        // RVA: -1  // not resolved  token: 0x6000217
        protected virtual System.Type[] GetParameterTypes() { }
        // RVA: -1  // not resolved  token: 0x6000218
        protected virtual System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions) { }
        // RVA: -1  // not resolved  token: 0x6000219
        protected virtual System.Void CreateDelegate() { }
        // RVA: -1  // not resolved  token: 0x600021A
        private System.Void <CreateDelegate>b__7_0(TParam0 param0, TParam1 param1, TParam2 param2, TParam3 param3) { }

    }

    // TypeToken: 0x2000080
    public sealed class StaticActionInvoker`5 : Unity.VisualScripting.StaticActionInvokerBase
    {
        // Fields
        private Unity.VisualScripting.Action<TParam0,TParam1,TParam2,TParam3,TParam4> invoke;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600021B
        public System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }
        // RVA: -1  // not resolved  token: 0x600021C
        protected virtual System.Type[] GetParameterTypes() { }
        // RVA: -1  // not resolved  token: 0x600021D
        protected virtual System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions) { }
        // RVA: -1  // not resolved  token: 0x600021E
        protected virtual System.Void CreateDelegate() { }
        // RVA: -1  // not resolved  token: 0x600021F
        private System.Void <CreateDelegate>b__7_0(TParam0 param0, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4) { }

    }

    // TypeToken: 0x2000081
    public class StaticFieldAccessor`1 : Unity.VisualScripting.IOptimizedAccessor
    {
        // Fields
        private readonly System.Reflection.FieldInfo fieldInfo;  // 0x0
        private System.Func<TField> getter;  // 0x0
        private System.Action<TField> setter;  // 0x0
        private System.Type targetType;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000220
        public System.Void .ctor(System.Reflection.FieldInfo fieldInfo) { }
        // RVA: -1  // not resolved  token: 0x6000221
        public virtual System.Void Compile() { }
        // RVA: -1  // not resolved  token: 0x6000222
        public virtual System.Object GetValue(System.Object target) { }
        // RVA: -1  // not resolved  token: 0x6000223
        private System.Object GetValueUnsafe(System.Object target) { }
        // RVA: -1  // not resolved  token: 0x6000224
        public virtual System.Void SetValue(System.Object target, System.Object value) { }
        // RVA: -1  // not resolved  token: 0x6000225
        private System.Void SetValueUnsafe(System.Object target, System.Object value) { }
        // RVA: -1  // not resolved  token: 0x6000226
        private TField <Compile>b__5_0() { }
        // RVA: -1  // not resolved  token: 0x6000227
        private System.Void <Compile>b__5_1(TField value) { }

    }

    // TypeToken: 0x2000083
    public abstract class StaticFunctionInvokerBase`1 : Unity.VisualScripting.StaticInvokerBase
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x600022A
        protected System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }

    }

    // TypeToken: 0x2000084
    public sealed class StaticFunctionInvoker`1 : Unity.VisualScripting.StaticFunctionInvokerBase`1
    {
        // Fields
        private System.Func<TResult> invoke;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600022B
        public System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }
        // RVA: -1  // not resolved  token: 0x600022C
        protected virtual System.Type[] GetParameterTypes() { }
        // RVA: -1  // not resolved  token: 0x600022D
        protected virtual System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions) { }
        // RVA: -1  // not resolved  token: 0x600022E
        protected virtual System.Void CreateDelegate() { }
        // RVA: -1  // not resolved  token: 0x600022F
        private TResult <CreateDelegate>b__7_0() { }

    }

    // TypeToken: 0x2000085
    public sealed class StaticFunctionInvoker`2 : Unity.VisualScripting.StaticFunctionInvokerBase`1
    {
        // Fields
        private System.Func<TParam0,TResult> invoke;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000230
        public System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }
        // RVA: -1  // not resolved  token: 0x6000231
        public virtual System.Object Invoke(System.Object target, System.Object arg0) { }
        // RVA: -1  // not resolved  token: 0x6000232
        public System.Object InvokeUnsafe(System.Object target, System.Object arg0) { }
        // RVA: -1  // not resolved  token: 0x6000233
        protected virtual System.Type[] GetParameterTypes() { }
        // RVA: -1  // not resolved  token: 0x6000234
        protected virtual System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions) { }
        // RVA: -1  // not resolved  token: 0x6000235
        protected virtual System.Void CreateDelegate() { }
        // RVA: -1  // not resolved  token: 0x6000236
        private TResult <CreateDelegate>b__7_0(TParam0 param0) { }

    }

    // TypeToken: 0x2000086
    public sealed class StaticFunctionInvoker`3 : Unity.VisualScripting.StaticFunctionInvokerBase`1
    {
        // Fields
        private System.Func<TParam0,TParam1,TResult> invoke;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000237
        public System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }
        // RVA: -1  // not resolved  token: 0x6000238
        protected virtual System.Type[] GetParameterTypes() { }
        // RVA: -1  // not resolved  token: 0x6000239
        protected virtual System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions) { }
        // RVA: -1  // not resolved  token: 0x600023A
        protected virtual System.Void CreateDelegate() { }
        // RVA: -1  // not resolved  token: 0x600023B
        private TResult <CreateDelegate>b__7_0(TParam0 param0, TParam1 param1) { }

    }

    // TypeToken: 0x2000087
    public sealed class StaticFunctionInvoker`4 : Unity.VisualScripting.StaticFunctionInvokerBase`1
    {
        // Fields
        private System.Func<TParam0,TParam1,TParam2,TResult> invoke;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600023C
        public System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }
        // RVA: -1  // not resolved  token: 0x600023D
        protected virtual System.Type[] GetParameterTypes() { }
        // RVA: -1  // not resolved  token: 0x600023E
        protected virtual System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions) { }
        // RVA: -1  // not resolved  token: 0x600023F
        protected virtual System.Void CreateDelegate() { }
        // RVA: -1  // not resolved  token: 0x6000240
        private TResult <CreateDelegate>b__7_0(TParam0 param0, TParam1 param1, TParam2 param2) { }

    }

    // TypeToken: 0x2000088
    public sealed class StaticFunctionInvoker`5 : Unity.VisualScripting.StaticFunctionInvokerBase`1
    {
        // Fields
        private System.Func<TParam0,TParam1,TParam2,TParam3,TResult> invoke;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000241
        public System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }
        // RVA: -1  // not resolved  token: 0x6000242
        protected virtual System.Type[] GetParameterTypes() { }
        // RVA: -1  // not resolved  token: 0x6000243
        protected virtual System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions) { }
        // RVA: -1  // not resolved  token: 0x6000244
        protected virtual System.Void CreateDelegate() { }
        // RVA: -1  // not resolved  token: 0x6000245
        private TResult <CreateDelegate>b__7_0(TParam0 param0, TParam1 param1, TParam2 param2, TParam3 param3) { }

    }

    // TypeToken: 0x2000089
    public sealed class StaticFunctionInvoker`6 : Unity.VisualScripting.StaticFunctionInvokerBase`1
    {
        // Fields
        private Unity.VisualScripting.Func<TParam0,TParam1,TParam2,TParam3,TParam4,TResult> invoke;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000246
        public System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }
        // RVA: -1  // not resolved  token: 0x6000247
        protected virtual System.Type[] GetParameterTypes() { }
        // RVA: -1  // not resolved  token: 0x6000248
        protected virtual System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions) { }
        // RVA: -1  // not resolved  token: 0x6000249
        protected virtual System.Void CreateDelegate() { }
        // RVA: -1  // not resolved  token: 0x600024A
        private TResult <CreateDelegate>b__7_0(TParam0 param0, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4) { }

    }

    // TypeToken: 0x200008A  // size: 0x20
    public abstract class StaticInvokerBase : Unity.VisualScripting.InvokerBase
    {
        // Methods
        // RVA: 0x0B367A54  token: 0x600024B
        protected System.Void .ctor(System.Reflection.MethodInfo methodInfo) { }
        // RVA: 0x0B367988  token: 0x600024C
        protected virtual System.Void CompileExpression() { }
        // RVA: -1  // abstract  token: 0x600024D
        protected virtual System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions) { }
        // RVA: 0x0B367A00  token: 0x600024E
        protected virtual System.Void VerifyTarget(System.Object target) { }

    }

    // TypeToken: 0x200008B
    public class StaticPropertyAccessor`1 : Unity.VisualScripting.IOptimizedAccessor
    {
        // Fields
        private readonly System.Reflection.PropertyInfo propertyInfo;  // 0x0
        private System.Func<TProperty> getter;  // 0x0
        private System.Action<TProperty> setter;  // 0x0
        private System.Type targetType;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600024F
        public System.Void .ctor(System.Reflection.PropertyInfo propertyInfo) { }
        // RVA: -1  // not resolved  token: 0x6000250
        public virtual System.Void Compile() { }
        // RVA: -1  // not resolved  token: 0x6000251
        public virtual System.Object GetValue(System.Object target) { }
        // RVA: -1  // not resolved  token: 0x6000252
        private System.Object GetValueUnsafe(System.Object target) { }
        // RVA: -1  // not resolved  token: 0x6000253
        public virtual System.Void SetValue(System.Object target, System.Object value) { }
        // RVA: -1  // not resolved  token: 0x6000254
        private System.Void SetValueUnsafe(System.Object target, System.Object value) { }

    }

    // TypeToken: 0x200008C  // size: 0x20
    public sealed class RenamedAssemblyAttribute : System.Attribute
    {
        // Fields
        private readonly System.String <previousName>k__BackingField;  // 0x10
        private readonly System.String <newName>k__BackingField;  // 0x18

        // Properties
        System.String previousName { get; /* RVA: 0x02B2ECC0 */ }
        System.String newName { get; /* RVA: 0x0385B100 */ }

    }

    // TypeToken: 0x200008D  // size: 0x18
    public sealed class RenamedFromAttribute : System.Attribute
    {
        // Fields
        private readonly System.String <previousName>k__BackingField;  // 0x10

        // Properties
        System.String previousName { get; /* RVA: 0x02B2ECC0 */ }

    }

    // TypeToken: 0x200008E  // size: 0x20
    public sealed class RenamedNamespaceAttribute : System.Attribute
    {
        // Fields
        private readonly System.String <previousName>k__BackingField;  // 0x10
        private readonly System.String <newName>k__BackingField;  // 0x18

        // Properties
        System.String previousName { get; /* RVA: 0x02B2ECC0 */ }
        System.String newName { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x032AF420  token: 0x6000258
        public System.Void .ctor(System.String previousName, System.String newName) { }

    }

    // TypeToken: 0x200008F  // size: 0x10
    public static class RuntimeCodebase
    {
        // Fields
        private static readonly System.Object lock;  // static @ 0x0
        private static readonly System.Collections.Generic.List<System.Type> _types;  // static @ 0x8
        private static readonly System.Collections.Generic.List<System.Reflection.Assembly> _assemblies;  // static @ 0x10
        public static System.Collections.Generic.HashSet<System.String> disallowedAssemblies;  // static @ 0x18
        private static readonly System.Collections.Generic.Dictionary<System.String,System.Type> typeSerializations;  // static @ 0x20
        private static System.Collections.Generic.Dictionary<System.String,System.Type> _renamedTypes;  // static @ 0x28
        private static System.Collections.Generic.Dictionary<System.String,System.String> _renamedNamespaces;  // static @ 0x30
        private static System.Collections.Generic.Dictionary<System.String,System.String> _renamedAssemblies;  // static @ 0x38
        private static readonly System.Collections.Generic.Dictionary<System.Type,System.Collections.Generic.Dictionary<System.String,System.String>> _renamedMembers;  // static @ 0x40

        // Properties
        System.Collections.Generic.IEnumerable<System.Type> types { get; /* RVA: 0x0B364CF4 */ }
        System.Collections.Generic.IEnumerable<System.Reflection.Assembly> assemblies { get; /* RVA: 0x0B364AD0 */ }
        System.Collections.Generic.Dictionary<System.String,System.String> renamedNamespaces { get; /* RVA: 0x0B364BBC */ }
        System.Collections.Generic.Dictionary<System.String,System.String> renamedAssemblies { get; /* RVA: 0x0B364B20 */ }
        System.Collections.Generic.Dictionary<System.String,System.Type> renamedTypes { get; /* RVA: 0x0B364C58 */ }

        // Methods
        // RVA: 0x0B3644EC  token: 0x600025D
        private static System.Void .cctor() { }
        // RVA: 0x0B363420  token: 0x600025E
        public static System.Collections.Generic.IEnumerable<System.Attribute> GetAssemblyAttributes(System.Type attributeType) { }
        // RVA: 0x0B363398  token: 0x600025F
        public static System.Collections.Generic.IEnumerable<System.Attribute> GetAssemblyAttributes(System.Type attributeType, System.Collections.Generic.IEnumerable<System.Reflection.Assembly> assemblies) { }
        // RVA: -1  // generic def  token: 0x6000260
        public static System.Collections.Generic.IEnumerable<TAttribute> GetAssemblyAttributes() { }
        // RVA: 0x0B363474  token: 0x6000261
        public static System.String SerializeType(System.Type type) { }
        // RVA: 0x0B363588  token: 0x6000262
        public static System.Boolean TryDeserializeType(System.String typeName, System.Type& type) { }
        // RVA: 0x0B362398  token: 0x6000263
        public static System.Type DeserializeType(System.String typeName) { }
        // RVA: 0x0B363508  token: 0x6000264
        private static System.Boolean TryCachedTypeLookup(System.String typeName, System.Type& type) { }
        // RVA: 0x0B363C38  token: 0x6000265
        private static System.Boolean TrySystemTypeLookup(System.String typeName, System.Type& type) { }
        // RVA: 0x0B364060  token: 0x6000266
        private static System.Boolean TrySystemTypeLookup(Unity.VisualScripting.TypeName typeName, System.Type& type) { }
        // RVA: 0x0B363764  token: 0x6000267
        private static System.Boolean TryRenamedTypeLookup(System.String previousTypeName, System.Type& type) { }
        // RVA: 0x0B3627FC  token: 0x600026B
        private static System.Collections.Generic.Dictionary<System.String,System.String> FetchRenamedNamespaces() { }
        // RVA: 0x0B362484  token: 0x600026C
        private static System.Collections.Generic.Dictionary<System.String,System.String> FetchRenamedAssemblies() { }
        // RVA: 0x0B362B74  token: 0x600026D
        private static System.Collections.Generic.Dictionary<System.String,System.Type> FetchRenamedTypes() { }

    }

    // TypeToken: 0x2000092  // size: 0x58
    public class TypeName
    {
        // Fields
        private System.String <AssemblyDescription>k__BackingField;  // 0x10
        private System.String <AssemblyName>k__BackingField;  // 0x18
        private System.String <AssemblyVersion>k__BackingField;  // 0x20
        private System.String <AssemblyCulture>k__BackingField;  // 0x28
        private System.String <AssemblyPublicKeyToken>k__BackingField;  // 0x30
        private readonly System.Collections.Generic.List<Unity.VisualScripting.TypeName> <GenericParameters>k__BackingField;  // 0x38
        private readonly System.Collections.Generic.List<System.String> names;  // 0x40
        private readonly System.Collections.Generic.List<System.Int32> genericarities;  // 0x48
        private System.String <Name>k__BackingField;  // 0x50

        // Properties
        System.String AssemblyDescription { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        System.String AssemblyName { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.String AssemblyVersion { set; /* RVA: 0x05390F40 */ }
        System.String AssemblyCulture { set; /* RVA: 0x02FDB880 */ }
        System.String AssemblyPublicKeyToken { set; /* RVA: 0x05396200 */ }
        System.Collections.Generic.List<Unity.VisualScripting.TypeName> GenericParameters { get; /* RVA: 0x04D85A50 */ }
        System.String Name { get; /* RVA: 0x04D86270 */ set; /* RVA: 0x02FDAEA0 */ }
        System.Boolean IsArray { get; /* RVA: 0x0B36976C */ }

        // Methods
        // RVA: 0x0B367EB0  token: 0x6000285
        public static Unity.VisualScripting.TypeName Parse(System.String s) { }
        // RVA: 0x0B368DD0  token: 0x6000286
        private System.Void .ctor(System.String s, System.Int32& index) { }
        // RVA: 0x0B367DC8  token: 0x6000287
        private static System.String LookForPairThenRemove(System.Collections.Generic.List<System.String> strings, System.String Name) { }
        // RVA: 0x0B368404  token: 0x6000288
        public System.Void ReplaceNamespace(System.String oldNamespace, System.String newNamespace) { }
        // RVA: 0x0B367F1C  token: 0x6000289
        public System.Void ReplaceAssembly(System.String oldAssembly, System.String newAssembly) { }
        // RVA: 0x0B36837C  token: 0x600028A
        public System.Void ReplaceName(System.String oldTypeName, System.Type newType) { }
        // RVA: 0x0B368124  token: 0x600028B
        public System.Void ReplaceName(System.String oldTypeName, System.String newTypeName, System.Reflection.AssemblyName newAssemblyName) { }
        // RVA: 0x0B368858  token: 0x600028C
        private static System.String ToElementTypeName(System.String s) { }
        // RVA: 0x0B3687E4  token: 0x600028D
        private static System.String ToArrayOrType(System.String oldType, System.String newType) { }
        // RVA: 0x0B3686F0  token: 0x600028E
        public System.Void SetAssemblyName(System.Reflection.AssemblyName newAssemblyName) { }
        // RVA: 0x0B368C80  token: 0x600028F
        private System.Void UpdateName() { }
        // RVA: 0x0B3688F0  token: 0x6000290
        public System.String ToString(Unity.VisualScripting.TypeNameDetail specification, Unity.VisualScripting.TypeNameDetail genericsSpecification) { }
        // RVA: 0x0B368C70  token: 0x6000291
        public virtual System.String ToString() { }
        // RVA: 0x0B3688E0  token: 0x6000292
        public System.String ToLooseString() { }

    }

    // TypeToken: 0x2000095  // size: 0x14
    public sealed struct TypeNameDetail
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Unity.VisualScripting.TypeNameDetail Name;  // const
        public static Unity.VisualScripting.TypeNameDetail NameAndAssembly;  // const
        public static Unity.VisualScripting.TypeNameDetail Full;  // const

    }

    // TypeToken: 0x2000096  // size: 0x10
    public static class TypeUtility
    {
        // Fields
        private static readonly System.Collections.Generic.HashSet<System.Type> _numericTypes;  // static @ 0x0
        private static readonly System.Collections.Generic.HashSet<System.Type> _numericConstructTypes;  // static @ 0x8
        private static readonly System.Collections.Generic.HashSet<System.Type> typesWithShortStrings;  // static @ 0x10
        private static readonly System.Collections.Generic.Dictionary<System.Type,System.Object> defaultPrimitives;  // static @ 0x18

        // Methods
        // RVA: 0x0B369898  token: 0x6000296
        public static System.Boolean IsBasic(System.Type type) { }
        // RVA: 0x0B369AAC  token: 0x6000297
        public static System.Boolean IsStatic(System.Type type) { }
        // RVA: 0x0B369A14  token: 0x6000298
        public static System.Boolean IsNullable(System.Type type) { }
        // RVA: 0x0B369A88  token: 0x6000299
        public static System.Boolean IsReferenceType(System.Type type) { }
        // RVA: 0x0B369828  token: 0x600029A
        public static System.Boolean IsAssignableFrom(System.Type type, System.Object value) { }
        // RVA: 0x0B3697B8  token: 0x600029B
        public static System.Collections.Generic.IEnumerable<System.Type> GetTypesSafely(System.Reflection.Assembly assembly) { }
        // RVA: 0x0B369AE4  token: 0x600029C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000099  // size: 0x18
    public class LooseAssemblyNameConverter : Unity.VisualScripting.FullSerializer.fsDirectConverter
    {
        // Properties
        System.Type ModelType { get; /* RVA: 0x0B35C3EC */ }

        // Methods
        // RVA: 0x0B35C1B8  token: 0x60002AA
        public virtual System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType) { }
        // RVA: 0x0B35C31C  token: 0x60002AB
        public virtual Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType) { }
        // RVA: 0x0B35C1F4  token: 0x60002AC
        public virtual Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance, System.Type storageType) { }
        // RVA: 0x041E1670  token: 0x60002AD
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200009A  // size: 0x18
    public class NamespaceConverter : Unity.VisualScripting.FullSerializer.fsDirectConverter
    {
        // Properties
        System.Type ModelType { get; /* RVA: 0x0B35D178 */ }

        // Methods
        // RVA: 0x0B35CF44  token: 0x60002AF
        public virtual System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType) { }
        // RVA: 0x0B35D09C  token: 0x60002B0
        public virtual Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType) { }
        // RVA: 0x0B35CF80  token: 0x60002B1
        public virtual Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance, System.Type storageType) { }
        // RVA: 0x041E1670  token: 0x60002B2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200009B  // size: 0x18
    public class Ray2DConverter : Unity.VisualScripting.FullSerializer.fsDirectConverter`1
    {
        // Methods
        // RVA: 0x0B361000  token: 0x60002B3
        protected virtual Unity.VisualScripting.FullSerializer.fsResult DoSerialize(UnityEngine.Ray2D model, System.Collections.Generic.Dictionary<System.String,Unity.VisualScripting.FullSerializer.fsData> serialized) { }
        // RVA: 0x0B360E90  token: 0x60002B4
        protected virtual Unity.VisualScripting.FullSerializer.fsResult DoDeserialize(System.Collections.Generic.Dictionary<System.String,Unity.VisualScripting.FullSerializer.fsData> data, UnityEngine.Ray2D& model) { }
        // RVA: 0x0B360E50  token: 0x60002B5
        public virtual System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType) { }
        // RVA: 0x0B361154  token: 0x60002B6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200009C  // size: 0x18
    public class RayConverter : Unity.VisualScripting.FullSerializer.fsDirectConverter`1
    {
        // Methods
        // RVA: 0x0B361364  token: 0x60002B7
        protected virtual Unity.VisualScripting.FullSerializer.fsResult DoSerialize(UnityEngine.Ray model, System.Collections.Generic.Dictionary<System.String,Unity.VisualScripting.FullSerializer.fsData> serialized) { }
        // RVA: 0x0B3611D0  token: 0x60002B8
        protected virtual Unity.VisualScripting.FullSerializer.fsResult DoDeserialize(System.Collections.Generic.Dictionary<System.String,Unity.VisualScripting.FullSerializer.fsData> data, UnityEngine.Ray& model) { }
        // RVA: 0x0B36117C  token: 0x60002B9
        public virtual System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType) { }
        // RVA: 0x0B3614CC  token: 0x60002BA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200009D  // size: 0x18
    public class UnityObjectConverter : Unity.VisualScripting.FullSerializer.fsConverter
    {
        // Properties
        System.Collections.Generic.List<UnityEngine.Object> objectReferences { get; /* RVA: 0x0B36C10C */ }

        // Methods
        // RVA: 0x0B36BD54  token: 0x60002BC
        public virtual System.Boolean CanProcess(System.Type type) { }
        // RVA: 0x012081B0  token: 0x60002BD
        public virtual System.Boolean RequestCycleSupport(System.Type storageType) { }
        // RVA: 0x012081B0  token: 0x60002BE
        public virtual System.Boolean RequestInheritanceSupport(System.Type storageType) { }
        // RVA: 0x0B36BFE0  token: 0x60002BF
        public virtual Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType) { }
        // RVA: 0x0B36BDCC  token: 0x60002C0
        public virtual Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData storage, System.Object& instance, System.Type storageType) { }
        // RVA: 0x04D8C7E0  token: 0x60002C1
        public virtual System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType) { }
        // RVA: 0x041E1670  token: 0x60002C2
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200009E  // size: 0x10
    public sealed class DoNotSerializeAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60002C3
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200009F
    public interface ISerializationDependency : UnityEngine.ISerializationCallbackReceiver
    {
        // Properties
        System.Boolean IsDeserialized { get; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x20000A0
    public interface ISerializationDepender : UnityEngine.ISerializationCallbackReceiver
    {
        // Properties
        System.Collections.Generic.IEnumerable<Unity.VisualScripting.ISerializationDependency> deserializationDependencies { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x60002C6
        public virtual System.Void OnAfterDependenciesDeserialized() { }

    }

    // TypeToken: 0x20000A1  // size: 0x10
    public static class Serialization
    {
        // Fields
        private static readonly System.Collections.Generic.HashSet<Unity.VisualScripting.SerializationOperation> freeOperations;  // static @ 0x0
        private static readonly System.Collections.Generic.HashSet<Unity.VisualScripting.SerializationOperation> busyOperations;  // static @ 0x8
        private static readonly System.Object lock;  // static @ 0x10
        private static System.Boolean <isUnitySerializing>k__BackingField;  // static @ 0x18
        private static readonly System.Collections.Generic.HashSet<Unity.VisualScripting.ISerializationDepender> awaitingDependers;  // static @ 0x20

        // Properties
        System.Boolean isUnitySerializing { get; /* RVA: 0x0B3676D0 */ set; /* RVA: 0x0B367720 */ }
        System.Boolean isCustomSerializing { get; /* RVA: 0x0B36766C */ }

        // Methods
        // RVA: 0x0B3674DC  token: 0x60002C7
        private static System.Void .cctor() { }
        // RVA: 0x0B3672A0  token: 0x60002CB
        private static Unity.VisualScripting.SerializationOperation StartOperation() { }
        // RVA: 0x0B3667BC  token: 0x60002CC
        private static System.Void EndOperation(Unity.VisualScripting.SerializationOperation operation) { }
        // RVA: 0x0B366F18  token: 0x60002CD
        public static Unity.VisualScripting.SerializationData Serialize(System.Object value, System.Boolean forceReflected) { }
        // RVA: 0x0B365F38  token: 0x60002CE
        public static System.Void DeserializeInto(Unity.VisualScripting.SerializationData data, System.Object& instance, System.Boolean forceReflected) { }
        // RVA: 0x0B366750  token: 0x60002CF
        public static System.Object Deserialize(Unity.VisualScripting.SerializationData data, System.Boolean forceReflected) { }
        // RVA: 0x0B366CEC  token: 0x60002D0
        private static System.String SerializeJson(Unity.VisualScripting.FullSerializer.fsSerializer serializer, System.Object instance, System.Boolean forceReflected) { }
        // RVA: 0x0B3664D0  token: 0x60002D1
        private static Unity.VisualScripting.FullSerializer.fsResult DeserializeJsonUtil(Unity.VisualScripting.FullSerializer.fsSerializer serializer, System.String json, System.Object& instance, System.Boolean forceReflected) { }
        // RVA: 0x0B3665F8  token: 0x60002D2
        private static System.Void DeserializeJson(Unity.VisualScripting.FullSerializer.fsSerializer serializer, System.String json, System.Object& instance, System.Boolean forceReflected) { }
        // RVA: 0x0B3669C8  token: 0x60002D3
        private static System.Void HandleResult(System.String label, Unity.VisualScripting.FullSerializer.fsResult result, UnityEngine.Object context) { }
        // RVA: 0x0B366CD0  token: 0x60002D4
        public static System.String PrettyPrint(System.String json) { }
        // RVA: 0x0B365AB4  token: 0x60002D5
        public static System.Void AwaitDependencies(Unity.VisualScripting.ISerializationDepender depender) { }
        // RVA: 0x0B365B2C  token: 0x60002D6
        private static System.Void CheckIfDependenciesMet(Unity.VisualScripting.ISerializationDepender depender) { }

    }

    // TypeToken: 0x20000A2  // size: 0x20
    public sealed struct SerializationData
    {
        // Fields
        private System.String _json;  // 0x10
        private UnityEngine.Object[] _objectReferences;  // 0x18

        // Properties
        System.String json { get; /* RVA: 0x02B76770 */ }
        UnityEngine.Object[] objectReferences { get; /* RVA: 0x04D88320 */ }

        // Methods
        // RVA: 0x0B3657A0  token: 0x60002D9
        public System.Void .ctor(System.String json, System.Collections.Generic.IEnumerable<UnityEngine.Object> objectReferences) { }
        // RVA: 0x0B365798  token: 0x60002DA
        public System.Void .ctor(System.String json, UnityEngine.Object[] objectReferences) { }
        // RVA: 0x09ACC1AC  token: 0x60002DB
        private System.Void Clear() { }
        // RVA: 0x0B3650D0  token: 0x60002DC
        public System.String ToString(System.String title) { }
        // RVA: 0x0B36578C  token: 0x60002DD
        public virtual System.String ToString() { }
        // RVA: 0x0B365004  token: 0x60002DE
        public System.Void ShowString(System.String title) { }

    }

    // TypeToken: 0x20000A3  // size: 0x20
    public class SerializationOperation
    {
        // Fields
        private Unity.VisualScripting.FullSerializer.fsSerializer <serializer>k__BackingField;  // 0x10
        private System.Collections.Generic.List<UnityEngine.Object> <objectReferences>k__BackingField;  // 0x18

        // Properties
        Unity.VisualScripting.FullSerializer.fsSerializer serializer { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        System.Collections.Generic.List<UnityEngine.Object> objectReferences { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }

        // Methods
        // RVA: 0x0B365888  token: 0x60002DF
        public System.Void .ctor() { }
        // RVA: 0x0B365840  token: 0x60002E4
        public System.Void Reset() { }

    }

    // TypeToken: 0x20000A4  // size: 0x38
    public class SerializationVersionAttribute : Unity.VisualScripting.FullSerializer.fsObjectAttribute
    {
        // Methods
        // RVA: 0x0B365AAC  token: 0x60002E5
        public System.Void .ctor(System.String versionString, System.Type[] previousModels) { }

    }

    // TypeToken: 0x20000A5  // size: 0x20
    public class SerializeAsAttribute : Unity.VisualScripting.FullSerializer.fsPropertyAttribute
    {
        // Methods
        // RVA: 0x053908C0  token: 0x60002E6
        public System.Void .ctor(System.String name) { }

    }

    // TypeToken: 0x20000A6  // size: 0x10
    public class SerializeAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60002E7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000A7
    public interface ISingleton
    {
    }

    // TypeToken: 0x20000A8  // size: 0x30
    public abstract class LudiqScriptableObject : UnityEngine.ScriptableObject, UnityEngine.ISerializationCallbackReceiver
    {
        // Fields
        protected Unity.VisualScripting.SerializationData _data;  // 0x18
        private System.Action OnDestroyActions;  // 0x28

        // Events
        event System.Action OnDestroyActions;

        // Methods
        // RVA: 0x0B35C748  token: 0x60002EA
        private virtual System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize() { }
        // RVA: 0x0B35C5E4  token: 0x60002EB
        private virtual System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize() { }
        // RVA: 0x041E1670  token: 0x60002EC
        protected virtual System.Void OnBeforeSerialize() { }
        // RVA: 0x041E1670  token: 0x60002ED
        protected virtual System.Void OnAfterSerialize() { }
        // RVA: 0x041E1670  token: 0x60002EE
        protected virtual System.Void OnBeforeDeserialize() { }
        // RVA: 0x041E1670  token: 0x60002EF
        protected virtual System.Void OnAfterDeserialize() { }
        // RVA: 0x041E1670  token: 0x60002F0
        protected virtual System.Void OnPostDeserializeInEditor() { }
        // RVA: 0x0B35C548  token: 0x60002F1
        private System.Void OnDestroy() { }
        // RVA: 0x0B35C558  token: 0x60002F2
        protected virtual System.Void ShowData() { }
        // RVA: 0x0B35C5DC  token: 0x60002F3
        public virtual System.String ToString() { }
        // RVA: 0x03573370  token: 0x60002F4
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000A9
    public static class Singleton`1
    {
        // Fields
        private static readonly Unity.VisualScripting.SingletonAttribute attribute;  // static @ 0x0
        private static readonly System.Object _lock;  // static @ 0x0
        private static readonly System.Collections.Generic.HashSet<T> awoken;  // static @ 0x0
        private static T _instance;  // static @ 0x0

        // Properties
        System.Boolean persistent { get; /* RVA: -1  // not resolved */ }
        System.Boolean automatic { get; /* RVA: -1  // not resolved */ }
        System.String name { get; /* RVA: -1  // not resolved */ }
        UnityEngine.HideFlags hideFlags { get; /* RVA: -1  // not resolved */ }
        T instance { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60002F5
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x60002FB
        private static T[] FindObjectsOfType() { }
        // RVA: -1  // not resolved  token: 0x60002FC
        private static T[] FindInstances() { }
        // RVA: -1  // not resolved  token: 0x60002FD
        public static T Instantiate() { }
        // RVA: -1  // not resolved  token: 0x60002FE
        public static System.Void Awake(T instance) { }
        // RVA: -1  // not resolved  token: 0x60002FF
        public static System.Void OnDestroy(T instance) { }

    }

    // TypeToken: 0x20000AA  // size: 0x20
    public sealed class SingletonAttribute : System.Attribute
    {
        // Fields
        private System.Boolean <Persistent>k__BackingField;  // 0x10
        private System.Boolean <Automatic>k__BackingField;  // 0x11
        private UnityEngine.HideFlags <HideFlags>k__BackingField;  // 0x14
        private System.String <Name>k__BackingField;  // 0x18

        // Properties
        System.Boolean Persistent { get; /* RVA: 0x015EFCE0 */ set; /* RVA: 0x04D86130 */ }
        System.Boolean Automatic { get; /* RVA: 0x04D88380 */ set; /* RVA: 0x04D88460 */ }
        UnityEngine.HideFlags HideFlags { get; /* RVA: 0x04D882A0 */ set; /* RVA: 0x04D8F540 */ }
        System.String Name { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }

        // Methods
        // RVA: 0x04D9DD80  token: 0x6000300
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000AB  // size: 0x10
    public static class UnityThread
    {
        // Fields
        public static System.Threading.Thread thread;  // static @ 0x0
        public static System.Collections.Concurrent.ConcurrentQueue<System.Action> pendingQueue;  // static @ 0x8

        // Properties
        System.Boolean allowsAPI { get; /* RVA: 0x0B36C484 */ }

        // Methods
        // RVA: 0x03FCE110  token: 0x600030A
        private static System.Void RuntimeInitialize() { }
        // RVA: 0x04A1D420  token: 0x600030B
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000AC
    public static class Empty`1
    {
        // Fields
        public static readonly T[] array;  // static @ 0x0
        public static readonly System.Collections.Generic.List<T> list;  // static @ 0x0
        public static readonly System.Collections.Generic.HashSet<T> hashSet;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600030C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000AD  // size: 0x10
    public static class HashUtility
    {
        // Methods
        // RVA: -1  // generic def  token: 0x600030D
        public static System.Int32 GetHashCode(T a) { }
        // RVA: -1  // generic def  token: 0x600030E
        public static System.Int32 GetHashCode(T1 a, T2 b) { }

    }

    // TypeToken: 0x20000AE
    public interface IIdentifiable
    {
        // Properties
        System.Guid guid { get; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x20000AF  // size: 0x10
    public static class LinqUtility
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000310
        public static System.Collections.Generic.IEnumerable<T> Concat(System.Collections.IEnumerable[] enumerables) { }
        // RVA: -1  // generic def  token: 0x6000311
        public static System.Collections.Generic.IEnumerable<T> NotNull(System.Collections.Generic.IEnumerable<T> enumerable) { }

    }

    // TypeToken: 0x20000B2
    public class Recursion`1 : Unity.VisualScripting.IPoolable, System.IDisposable
    {
        // Fields
        private readonly System.Collections.Generic.Stack<T> traversedOrder;  // 0x0
        private readonly System.Collections.Generic.Dictionary<T,System.Int32> traversedCount;  // 0x0
        private System.Boolean disposed;  // 0x0
        protected System.Int32 maxDepth;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600031F
        protected System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000320
        public System.Void Enter(T o) { }
        // RVA: -1  // not resolved  token: 0x6000321
        public System.Boolean TryEnter(T o) { }
        // RVA: -1  // not resolved  token: 0x6000322
        public System.Void Exit(T o) { }
        // RVA: -1  // not resolved  token: 0x6000323
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000324
        protected virtual System.Void Free() { }
        // RVA: -1  // not resolved  token: 0x6000325
        private virtual System.Void Unity.VisualScripting.IPoolable.New() { }
        // RVA: -1  // not resolved  token: 0x6000326
        private virtual System.Void Unity.VisualScripting.IPoolable.Free() { }
        // RVA: -1  // not resolved  token: 0x6000327
        public static Unity.VisualScripting.Recursion<T> New() { }
        // RVA: -1  // not resolved  token: 0x6000328
        public static Unity.VisualScripting.Recursion<T> New(System.Int32 maxDepth) { }

    }

    // TypeToken: 0x20000B4  // size: 0x28
    public sealed class Recursion : Unity.VisualScripting.Recursion`1
    {
        // Fields
        private static System.Int32 <defaultMaxDepth>k__BackingField;  // static @ 0x0
        private static System.Boolean <safeMode>k__BackingField;  // static @ 0x4

        // Properties
        System.Int32 defaultMaxDepth { get; /* RVA: 0x0B3614F4 */ }
        System.Boolean safeMode { get; /* RVA: 0x0B361544 */ set; /* RVA: 0x0B361594 */ }

        // Methods
        // RVA: 0x03FCE840  token: 0x600032F
        private static System.Void OnRuntimeMethodLoad() { }
        // RVA: 0x04D79CD0  token: 0x6000330
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000B5  // size: 0x10
    public static class ReferenceCollector
    {
        // Fields
        private static System.Action onSceneUnloaded;  // static @ 0x0

        // Events
        event System.Action onSceneUnloaded;

        // Methods
        // RVA: 0x03FCEDC0  token: 0x6000333
        private static System.Void Initialize() { }

    }

    // TypeToken: 0x20000B7  // size: 0x10
    public class ReferenceEqualityComparer : System.Collections.Generic.IEqualityComparer`1
    {
        // Fields
        public static readonly Unity.VisualScripting.ReferenceEqualityComparer Instance;  // static @ 0x0

        // Methods
        // RVA: 0x041E1670  token: 0x6000337
        private System.Void .ctor() { }
        // RVA: 0x04D7F860  token: 0x6000338
        private virtual System.Boolean System.Collections.Generic.IEqualityComparer<System.Object>.Equals(System.Object a, System.Object b) { }
        // RVA: 0x04CC8900  token: 0x6000339
        private virtual System.Int32 System.Collections.Generic.IEqualityComparer<System.Object>.GetHashCode(System.Object a) { }
        // RVA: 0x04D87100  token: 0x600033A
        public static System.Int32 GetHashCode(System.Object a) { }
        // RVA: 0x04D4DE30  token: 0x600033B
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000B8
    public class ReferenceEqualityComparer`1 : System.Collections.Generic.IEqualityComparer`1
    {
        // Fields
        public static readonly Unity.VisualScripting.ReferenceEqualityComparer<T> Instance;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600033C
        private System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x600033D
        private virtual System.Boolean System.Collections.Generic.IEqualityComparer<T>.Equals(T a, T b) { }
        // RVA: -1  // not resolved  token: 0x600033E
        private virtual System.Int32 System.Collections.Generic.IEqualityComparer<T>.GetHashCode(T a) { }
        // RVA: -1  // not resolved  token: 0x600033F
        public static System.Int32 GetHashCode(T a) { }
        // RVA: -1  // not resolved  token: 0x6000340
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000B9  // size: 0x10
    public static class RuntimeVSUsageUtility
    {
        // Methods
        // RVA: 0x03FCE040  token: 0x6000341
        private static System.Void RuntimeInitializeOnLoadBeforeSceneLoad() { }

    }

    // TypeToken: 0x20000BA  // size: 0x10
    public static class StringUtility
    {
        // Fields
        private static readonly System.Text.RegularExpressions.Regex guidRegex;  // static @ 0x0

        // Methods
        // RVA: 0x0B367C98  token: 0x6000342
        public static System.String TrimStart(System.String source, System.String value) { }
        // RVA: 0x0B367B28  token: 0x6000343
        public static System.Void PartsAround(System.String s, System.Char c, System.String& before, System.String& after) { }
        // RVA: 0x0B367C1C  token: 0x6000344
        public static System.String ToHexString(System.Byte[] bytes) { }
        // RVA: 0x0B367D38  token: 0x6000345
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000BB  // size: 0x10
    public static class UnityObjectUtility
    {
        // Methods
        // RVA: 0x0B36C15C  token: 0x6000346
        public static System.Boolean IsUnityNull(System.Object obj) { }
        // RVA: 0x0B36C1D8  token: 0x6000347
        public static System.String ToSafeString(UnityEngine.Object uo) { }
        // RVA: -1  // generic def  token: 0x6000348
        public static T AsUnityNull(T obj) { }
        // RVA: 0x0B36C3FC  token: 0x6000349
        public static System.Boolean TrulyEqual(UnityEngine.Object a, UnityEngine.Object b) { }

    }

    // TypeToken: 0x20000BC  // size: 0x10
    public static class ApplicationVariables
    {
        // Fields
        private static Unity.VisualScripting.VariablesAsset _asset;  // static @ 0x0
        private static Unity.VisualScripting.VariableDeclarations <runtime>k__BackingField;  // static @ 0x8

        // Properties
        Unity.VisualScripting.VariablesAsset asset { get; /* RVA: 0x03C91D50 */ }
        Unity.VisualScripting.VariableDeclarations runtime { set; /* RVA: 0x0B3526F8 */ }

        // Methods
        // RVA: 0x03C91E00  token: 0x600034B
        public static System.Void Load() { }
        // RVA: 0x05396C5C  token: 0x600034D
        private static System.Void OnEnterPlayMode() { }
        // RVA: 0x041E1670  token: 0x600034E
        private static System.Void OnExitPlayMode() { }
        // RVA: 0x03C91350  token: 0x600034F
        private static System.Void CreateRuntimeDeclarations() { }

    }

    // TypeToken: 0x20000BD  // size: 0x18
    public sealed class InspectorVariableNameAttribute : System.Attribute
    {
        // Fields
        private Unity.VisualScripting.ActionDirection <direction>k__BackingField;  // 0x10

        // Properties
        Unity.VisualScripting.ActionDirection direction { set; /* RVA: 0x014F51F0 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000350
        public System.Void .ctor(Unity.VisualScripting.ActionDirection direction) { }

    }

    // TypeToken: 0x20000BE  // size: 0x10
    public static class SavedVariables
    {
        // Fields
        private static Unity.VisualScripting.VariablesAsset _asset;  // static @ 0x0
        private static Unity.VisualScripting.VariableDeclarations <saved>k__BackingField;  // static @ 0x8
        private static Unity.VisualScripting.VariableDeclarations <merged>k__BackingField;  // static @ 0x10

        // Properties
        Unity.VisualScripting.VariablesAsset asset { get; /* RVA: 0x03C91CA0 */ }
        Unity.VisualScripting.VariableDeclarations initial { get; /* RVA: 0x03C91C00 */ }
        Unity.VisualScripting.VariableDeclarations saved { get; /* RVA: 0x0B364F24 */ set; /* RVA: 0x0B364FB0 */ }
        Unity.VisualScripting.VariableDeclarations merged { get; /* RVA: 0x0B364EEC */ set; /* RVA: 0x0B364F5C */ }

        // Methods
        // RVA: 0x03C91E90  token: 0x6000353
        public static System.Void Load() { }
        // RVA: 0x03C911E0  token: 0x6000354
        private static System.Void OnEnterPlayMode() { }
        // RVA: 0x0B364D44  token: 0x6000355
        private static System.Void OnExitPlayMode() { }
        // RVA: 0x0B364D5C  token: 0x600035B
        public static System.Void SaveDeclarations(Unity.VisualScripting.VariableDeclarations declarations) { }
        // RVA: 0x03C92510  token: 0x600035C
        public static System.Void FetchSavedDeclarations() { }
        // RVA: 0x03C91410  token: 0x600035D
        private static System.Void MergeInitialAndSavedDeclarations() { }
        // RVA: 0x04727250  token: 0x600035E
        private static System.Void WarnAndNullifyUnityObjectReferences(Unity.VisualScripting.VariableDeclarations declarations) { }

    }

    // TypeToken: 0x20000C0  // size: 0x20
    public sealed class VariableDeclaration
    {
        // Fields
        private System.String <name>k__BackingField;  // 0x10
        private System.Object <value>k__BackingField;  // 0x18

        // Properties
        System.String name { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        System.Object value { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }

        // Methods
        // RVA: 0x032AF420  token: 0x6000363
        public System.Void .ctor(System.String name, System.Object value) { }

    }

    // TypeToken: 0x20000C1  // size: 0x30
    public sealed class VariableDeclarationCollection : System.Collections.ObjectModel.KeyedCollection`2, Unity.VisualScripting.IKeyedCollection`2, System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Methods
        // RVA: 0x06C08920  token: 0x6000368
        protected virtual System.String GetKeyForItem(Unity.VisualScripting.VariableDeclaration item) { }
        // RVA: 0x0B36CBC0  token: 0x6000369
        public virtual System.Boolean TryGetValue(System.String key, Unity.VisualScripting.VariableDeclaration& value) { }
        // RVA: 0x03C93A80  token: 0x600036A
        public System.Void .ctor() { }
        // RVA: 0x0B36CC80  token: 0x600036B
        private virtual Unity.VisualScripting.VariableDeclaration Unity.VisualScripting.IKeyedCollection<System.String,Unity.VisualScripting.VariableDeclaration>.get_Item(System.String key) { }
        // RVA: 0x0B36CC38  token: 0x600036C
        private virtual System.Boolean Unity.VisualScripting.IKeyedCollection<System.String,Unity.VisualScripting.VariableDeclaration>.Contains(System.String key) { }

    }

    // TypeToken: 0x20000C2  // size: 0x28
    public sealed class VariableDeclarations : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, Unity.VisualScripting.ISpecifiesCloner
    {
        // Fields
        public Unity.VisualScripting.VariableKind Kind;  // 0x10
        private Unity.VisualScripting.VariableDeclarationCollection collection;  // 0x18
        private System.Action OnVariableChanged;  // 0x20

        // Properties
        System.Object Item { get; /* RVA: 0x0B36D0FC */ set; /* RVA: 0x0B36D104 */ }
        Unity.VisualScripting.ICloner Unity.VisualScripting.ISpecifiesCloner.cloner { get; /* RVA: 0x0B36D0AC */ }

        // Methods
        // RVA: 0x03C93A10  token: 0x600036D
        public System.Void .ctor() { }
        // RVA: 0x0B36CF68  token: 0x6000370
        public System.Void Set(System.String variable, System.Object value) { }
        // RVA: 0x0B36CD74  token: 0x6000371
        public System.Object Get(System.String variable) { }
        // RVA: 0x0B36CD2C  token: 0x6000372
        public System.Void Clear() { }
        // RVA: 0x0B36CEA8  token: 0x6000373
        public System.Boolean IsDefined(System.String variable) { }
        // RVA: 0x04727730  token: 0x6000374
        public virtual System.Collections.Generic.IEnumerator<Unity.VisualScripting.VariableDeclaration> GetEnumerator() { }
        // RVA: 0x0B36D064  token: 0x6000375
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x20000C3  // size: 0x10
    public sealed class VariableDeclarationsCloner : Unity.VisualScripting.Cloner`1
    {
        // Fields
        public static readonly Unity.VisualScripting.VariableDeclarationsCloner instance;  // static @ 0x0

        // Methods
        // RVA: 0x0B36CCC8  token: 0x6000377
        public virtual System.Boolean Handles(System.Type type) { }
        // RVA: 0x03C93950  token: 0x6000378
        public virtual Unity.VisualScripting.VariableDeclarations ConstructClone(System.Type type, Unity.VisualScripting.VariableDeclarations original) { }
        // RVA: 0x04727540  token: 0x6000379
        public virtual System.Void FillClone(System.Type type, Unity.VisualScripting.VariableDeclarations& clone, Unity.VisualScripting.VariableDeclarations original, Unity.VisualScripting.CloningContext context) { }
        // RVA: 0x04D19370  token: 0x600037A
        public System.Void .ctor() { }
        // RVA: 0x04D19300  token: 0x600037B
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000C4  // size: 0x14
    public sealed struct VariableKind
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Unity.VisualScripting.VariableKind Flow;  // const
        public static Unity.VisualScripting.VariableKind Graph;  // const
        public static Unity.VisualScripting.VariableKind Object;  // const
        public static Unity.VisualScripting.VariableKind Scene;  // const
        public static Unity.VisualScripting.VariableKind Application;  // const
        public static Unity.VisualScripting.VariableKind Saved;  // const

    }

    // TypeToken: 0x20000C5  // size: 0x38
    public sealed class VariablesAsset : Unity.VisualScripting.LudiqScriptableObject
    {
        // Fields
        private Unity.VisualScripting.VariableDeclarations <declarations>k__BackingField;  // 0x30

        // Properties
        Unity.VisualScripting.VariableDeclarations declarations { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }

        // Methods
        // RVA: 0x0B36D10C  token: 0x600037E
        protected virtual System.Void ShowData() { }
        // RVA: 0x03C939A0  token: 0x600037F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C6  // size: 0x18
    public class VariablesSaver : UnityEngine.MonoBehaviour, Unity.VisualScripting.ISingleton
    {
        // Properties
        Unity.VisualScripting.VariablesSaver instance { get; /* RVA: 0x0B36D224 */ }

        // Methods
        // RVA: 0x0B36D114  token: 0x6000380
        private System.Void Awake() { }
        // RVA: 0x0B36D1D0  token: 0x6000381
        private System.Void OnDestroy() { }
        // RVA: 0x0B36D1C8  token: 0x6000382
        private System.Void OnApplicationQuit() { }
        // RVA: 0x0B36D1BC  token: 0x6000383
        private System.Void OnApplicationPause(System.Boolean isPaused) { }
        // RVA: 0x0B36D168  token: 0x6000385
        public static System.Void Instantiate() { }
        // RVA: 0x05393520  token: 0x6000386
        public System.Void .ctor() { }

    }

}

namespace Unity.VisualScripting.FullSerializer
{

    // TypeToken: 0x20000C7  // size: 0x18
    public class fsArrayConverter : Unity.VisualScripting.FullSerializer.fsConverter
    {
        // Methods
        // RVA: 0x0B352868  token: 0x6000387
        public virtual System.Boolean CanProcess(System.Type type) { }
        // RVA: 0x012081B0  token: 0x6000388
        public virtual System.Boolean RequestCycleSupport(System.Type storageType) { }
        // RVA: 0x012081B0  token: 0x6000389
        public virtual System.Boolean RequestInheritanceSupport(System.Type storageType) { }
        // RVA: 0x0B36D718  token: 0x600038A
        public virtual Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType) { }
        // RVA: 0x0B36D4B0  token: 0x600038B
        public virtual Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance, System.Type storageType) { }
        // RVA: 0x0B36D43C  token: 0x600038C
        public virtual System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType) { }
        // RVA: 0x041E1670  token: 0x600038D
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C8  // size: 0x18
    public class fsDateConverter : Unity.VisualScripting.FullSerializer.fsConverter
    {
        // Properties
        System.String DateTimeFormatString { get; /* RVA: 0x0B370498 */ }

        // Methods
        // RVA: 0x0B36FC48  token: 0x600038F
        public virtual System.Boolean CanProcess(System.Type type) { }
        // RVA: 0x0B370290  token: 0x6000390
        public virtual Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType) { }
        // RVA: 0x0B36FD0C  token: 0x6000391
        public virtual Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance, System.Type storageType) { }
        // RVA: 0x041E1670  token: 0x6000392
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C9  // size: 0x18
    public class fsDictionaryConverter : Unity.VisualScripting.FullSerializer.fsConverter
    {
        // Methods
        // RVA: 0x0B37078C  token: 0x6000393
        public virtual System.Boolean CanProcess(System.Type type) { }
        // RVA: 0x0B370804  token: 0x6000394
        public virtual System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType) { }
        // RVA: 0x0B370994  token: 0x6000395
        public virtual Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance_, System.Type storageType) { }
        // RVA: 0x0B371198  token: 0x6000396
        public virtual Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance_, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType) { }
        // RVA: 0x0B3704EC  token: 0x6000397
        private Unity.VisualScripting.FullSerializer.fsResult AddItemToDictionary(System.Collections.IDictionary dictionary, System.Object key, System.Object value) { }
        // RVA: 0x0B370878  token: 0x6000398
        private static System.Void GetKeyValueTypes(System.Type dictionaryType, System.Type& keyStorageType, System.Type& valueStorageType) { }
        // RVA: 0x041E1670  token: 0x6000399
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000CA  // size: 0x18
    public class fsEnumConverter : Unity.VisualScripting.FullSerializer.fsConverter
    {
        // Methods
        // RVA: 0x0B371838  token: 0x600039A
        public virtual System.Boolean CanProcess(System.Type type) { }
        // RVA: 0x012081B0  token: 0x600039B
        public virtual System.Boolean RequestCycleSupport(System.Type storageType) { }
        // RVA: 0x012081B0  token: 0x600039C
        public virtual System.Boolean RequestInheritanceSupport(System.Type storageType) { }
        // RVA: 0x0B371890  token: 0x600039D
        public virtual System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType) { }
        // RVA: 0x0B372070  token: 0x600039E
        public virtual Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType) { }
        // RVA: 0x0B371904  token: 0x600039F
        public virtual Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance, System.Type storageType) { }
        // RVA: -1  // generic def  token: 0x60003A0
        private static System.Boolean ArrayContains(T[] values, T value) { }
        // RVA: 0x041E1670  token: 0x60003A1
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000CD  // size: 0x18
    public sealed class fsForwardAttribute : System.Attribute
    {
        // Fields
        public System.String MemberName;  // 0x10

    }

    // TypeToken: 0x20000CE  // size: 0x20
    public class fsForwardConverter : Unity.VisualScripting.FullSerializer.fsConverter
    {
        // Fields
        private System.String _memberName;  // 0x18

        // Methods
        // RVA: 0x0A9A6604  token: 0x60003A9
        public System.Void .ctor(Unity.VisualScripting.FullSerializer.fsForwardAttribute attribute) { }
        // RVA: 0x0B372544  token: 0x60003AA
        public virtual System.Boolean CanProcess(System.Type type) { }
        // RVA: 0x0B372614  token: 0x60003AB
        private Unity.VisualScripting.FullSerializer.fsResult GetProperty(System.Object instance, Unity.VisualScripting.FullSerializer.fsMetaProperty& property) { }
        // RVA: 0x0B372960  token: 0x60003AC
        public virtual Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType) { }
        // RVA: 0x0B3727DC  token: 0x60003AD
        public virtual Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance, System.Type storageType) { }
        // RVA: 0x0B3725A0  token: 0x60003AE
        public virtual System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType) { }

    }

    // TypeToken: 0x20000CF  // size: 0x18
    public class fsGuidConverter : Unity.VisualScripting.FullSerializer.fsConverter
    {
        // Methods
        // RVA: 0x0B372B40  token: 0x60003AF
        public virtual System.Boolean CanProcess(System.Type type) { }
        // RVA: 0x012081B0  token: 0x60003B0
        public virtual System.Boolean RequestCycleSupport(System.Type storageType) { }
        // RVA: 0x012081B0  token: 0x60003B1
        public virtual System.Boolean RequestInheritanceSupport(System.Type storageType) { }
        // RVA: 0x0B372CEC  token: 0x60003B2
        public virtual Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType) { }
        // RVA: 0x0B372BE4  token: 0x60003B3
        public virtual Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance, System.Type storageType) { }
        // RVA: 0x0B372BA4  token: 0x60003B4
        public virtual System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType) { }
        // RVA: 0x041E1670  token: 0x60003B5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D0  // size: 0x18
    public class fsIEnumerableConverter : Unity.VisualScripting.FullSerializer.fsConverter
    {
        // Methods
        // RVA: 0x0B372DDC  token: 0x60003B6
        public virtual System.Boolean CanProcess(System.Type type) { }
        // RVA: 0x0B372E74  token: 0x60003B7
        public virtual System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType) { }
        // RVA: 0x0B3735BC  token: 0x60003B8
        public virtual Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance_, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType) { }
        // RVA: 0x0B37318C  token: 0x60003B9
        private System.Boolean IsStack(System.Type type) { }
        // RVA: 0x0B3732D8  token: 0x60003BA
        public virtual Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance_, System.Type storageType) { }
        // RVA: 0x0B373114  token: 0x60003BB
        private static System.Int32 HintSize(System.Collections.IEnumerable collection) { }
        // RVA: 0x0B373028  token: 0x60003BC
        private static System.Type GetElementType(System.Type objectType) { }
        // RVA: 0x0B373244  token: 0x60003BD
        private static System.Void TryClear(System.Type type, System.Object instance) { }
        // RVA: 0x0B372EE8  token: 0x60003BE
        private static System.Reflection.MethodInfo GetAddMethod(System.Type type) { }
        // RVA: 0x041E1670  token: 0x60003BF
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D1  // size: 0x18
    public class fsKeyValuePairConverter : Unity.VisualScripting.FullSerializer.fsConverter
    {
        // Methods
        // RVA: 0x0B376DB0  token: 0x60003C0
        public virtual System.Boolean CanProcess(System.Type type) { }
        // RVA: 0x012081B0  token: 0x60003C1
        public virtual System.Boolean RequestCycleSupport(System.Type storageType) { }
        // RVA: 0x012081B0  token: 0x60003C2
        public virtual System.Boolean RequestInheritanceSupport(System.Type storageType) { }
        // RVA: 0x0B376E5C  token: 0x60003C3
        public virtual Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance, System.Type storageType) { }
        // RVA: 0x0B377250  token: 0x60003C4
        public virtual Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType) { }
        // RVA: 0x041E1670  token: 0x60003C5
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D2  // size: 0x18
    public class fsNullableConverter : Unity.VisualScripting.FullSerializer.fsConverter
    {
        // Methods
        // RVA: 0x0B379524  token: 0x60003C6
        public virtual System.Boolean CanProcess(System.Type type) { }
        // RVA: 0x0B379648  token: 0x60003C7
        public virtual Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType) { }
        // RVA: 0x0B3795D0  token: 0x60003C8
        public virtual Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance, System.Type storageType) { }
        // RVA: 0x04D8C7E0  token: 0x60003C9
        public virtual System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType) { }
        // RVA: 0x041E1670  token: 0x60003CA
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D3  // size: 0x18
    public class fsPrimitiveConverter : Unity.VisualScripting.FullSerializer.fsConverter
    {
        // Methods
        // RVA: 0x0B379F4C  token: 0x60003CB
        public virtual System.Boolean CanProcess(System.Type type) { }
        // RVA: 0x012081B0  token: 0x60003CC
        public virtual System.Boolean RequestCycleSupport(System.Type storageType) { }
        // RVA: 0x012081B0  token: 0x60003CD
        public virtual System.Boolean RequestInheritanceSupport(System.Type storageType) { }
        // RVA: 0x0B37A5B8  token: 0x60003CE
        public virtual Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType) { }
        // RVA: 0x0B37A014  token: 0x60003CF
        public virtual Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData storage, System.Object& instance, System.Type storageType) { }
        // RVA: 0x0B37AB64  token: 0x60003D0
        private static System.Boolean UseBool(System.Type type) { }
        // RVA: 0x0B37AC8C  token: 0x60003D1
        private static System.Boolean UseInt64(System.Type type) { }
        // RVA: 0x0B37ABC8  token: 0x60003D2
        private static System.Boolean UseDouble(System.Type type) { }
        // RVA: 0x0B37AE48  token: 0x60003D3
        private static System.Boolean UseString(System.Type type) { }
        // RVA: 0x041E1670  token: 0x60003D4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D4  // size: 0x18
    public class fsReflectedConverter : Unity.VisualScripting.FullSerializer.fsConverter
    {
        // Methods
        // RVA: 0x0B37AEE0  token: 0x60003D5
        public virtual System.Boolean CanProcess(System.Type type) { }
        // RVA: 0x0B37B260  token: 0x60003D6
        public virtual Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType) { }
        // RVA: 0x0B37B000  token: 0x60003D7
        public virtual Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance, System.Type storageType) { }
        // RVA: 0x0B37AF8C  token: 0x60003D8
        public virtual System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType) { }
        // RVA: 0x041E1670  token: 0x60003D9
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D5  // size: 0x18
    public class fsTypeConverter : Unity.VisualScripting.FullSerializer.fsConverter
    {
        // Methods
        // RVA: 0x0B3815DC  token: 0x60003DA
        public virtual System.Boolean CanProcess(System.Type type) { }
        // RVA: 0x012081B0  token: 0x60003DB
        public virtual System.Boolean RequestCycleSupport(System.Type type) { }
        // RVA: 0x012081B0  token: 0x60003DC
        public virtual System.Boolean RequestInheritanceSupport(System.Type type) { }
        // RVA: 0x0B3817D0  token: 0x60003DD
        public virtual Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType) { }
        // RVA: 0x0B381654  token: 0x60003DE
        public virtual Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance, System.Type storageType) { }
        // RVA: 0x04D8C7E0  token: 0x60003DF
        public virtual System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType) { }
        // RVA: 0x041E1670  token: 0x60003E0
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D6  // size: 0x18
    public class fsWeakReferenceConverter : Unity.VisualScripting.FullSerializer.fsConverter
    {
        // Methods
        // RVA: 0x0B382CDC  token: 0x60003E1
        public virtual System.Boolean CanProcess(System.Type type) { }
        // RVA: 0x012081B0  token: 0x60003E2
        public virtual System.Boolean RequestCycleSupport(System.Type storageType) { }
        // RVA: 0x012081B0  token: 0x60003E3
        public virtual System.Boolean RequestInheritanceSupport(System.Type storageType) { }
        // RVA: 0x0B3830B8  token: 0x60003E4
        public virtual Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType) { }
        // RVA: 0x0B382D98  token: 0x60003E5
        public virtual Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance, System.Type storageType) { }
        // RVA: 0x0B382D40  token: 0x60003E6
        public virtual System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType) { }
        // RVA: 0x041E1670  token: 0x60003E7
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000D7  // size: 0x10
    public class fsConverterRegistrar
    {
        // Fields
        public static System.Collections.Generic.List<System.Type> Converters;  // static @ 0x0

        // Methods
        // RVA: 0x0B36E330  token: 0x60003E8
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000D8  // size: 0x10
    public class fsAotCompilationManager
    {
        // Fields
        private static System.Collections.Generic.Dictionary<System.Type,System.String> _computedAotCompilations;  // static @ 0x0
        private static System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsAotCompilationManager.AotCompilation> _uncomputedAotCompilations;  // static @ 0x8

        // Methods
        // RVA: 0x0B36D278  token: 0x60003E9
        public static System.Void AddAotCompilation(System.Type type, Unity.VisualScripting.FullSerializer.fsMetaProperty[] members, System.Boolean isConstructorPublic) { }
        // RVA: 0x0B36D348  token: 0x60003EA
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000DA  // size: 0x18
    public abstract class fsBaseConverter
    {
        // Fields
        public Unity.VisualScripting.FullSerializer.fsSerializer Serializer;  // 0x10

        // Methods
        // RVA: 0x0B36DD20  token: 0x60003EB
        public virtual System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType) { }
        // RVA: 0x0B36E1B4  token: 0x60003EC
        public virtual System.Boolean RequestCycleSupport(System.Type storageType) { }
        // RVA: 0x0B36E268  token: 0x60003ED
        public virtual System.Boolean RequestInheritanceSupport(System.Type storageType) { }
        // RVA: -1  // abstract  token: 0x60003EE
        public virtual Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType) { }
        // RVA: -1  // abstract  token: 0x60003EF
        public virtual Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance, System.Type storageType) { }
        // RVA: 0x0B36DE74  token: 0x60003F0
        protected Unity.VisualScripting.FullSerializer.fsResult FailExpectedType(Unity.VisualScripting.FullSerializer.fsData data, Unity.VisualScripting.FullSerializer.fsDataType[] types) { }
        // RVA: 0x0B36DB08  token: 0x60003F1
        protected Unity.VisualScripting.FullSerializer.fsResult CheckType(Unity.VisualScripting.FullSerializer.fsData data, Unity.VisualScripting.FullSerializer.fsDataType type) { }
        // RVA: 0x0B36D8F4  token: 0x60003F2
        protected Unity.VisualScripting.FullSerializer.fsResult CheckKey(Unity.VisualScripting.FullSerializer.fsData data, System.String key, Unity.VisualScripting.FullSerializer.fsData& subitem) { }
        // RVA: 0x0B36D960  token: 0x60003F3
        protected Unity.VisualScripting.FullSerializer.fsResult CheckKey(System.Collections.Generic.Dictionary<System.String,Unity.VisualScripting.FullSerializer.fsData> data, System.String key, Unity.VisualScripting.FullSerializer.fsData& subitem) { }
        // RVA: -1  // generic def  token: 0x60003F4
        protected Unity.VisualScripting.FullSerializer.fsResult SerializeMember(System.Collections.Generic.Dictionary<System.String,Unity.VisualScripting.FullSerializer.fsData> data, System.Type overrideConverterType, System.String name, T value) { }
        // RVA: -1  // generic def  token: 0x60003F5
        protected Unity.VisualScripting.FullSerializer.fsResult DeserializeMember(System.Collections.Generic.Dictionary<System.String,Unity.VisualScripting.FullSerializer.fsData> data, System.Type overrideConverterType, System.String name, T& value) { }
        // RVA: 0x041E1670  token: 0x60003F6
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000DC  // size: 0x10
    public static class fsGlobalConfig
    {
        // Fields
        public static System.Boolean IsCaseSensitive;  // static @ 0x0
        public static System.Boolean AllowInternalExceptions;  // static @ 0x1
        public static System.String InternalFieldPrefix;  // static @ 0x8

        // Methods
        // RVA: 0x0B372AC0  token: 0x60003FA
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000DD  // size: 0x48
    public class fsConfig
    {
        // Fields
        public System.Type[] SerializeAttributes;  // 0x10
        public System.Type[] IgnoreSerializeAttributes;  // 0x18
        public Unity.VisualScripting.FullSerializer.fsMemberSerialization DefaultMemberSerialization;  // 0x20
        public System.Func<System.String,System.Reflection.MemberInfo,System.String> GetJsonNameFromMemberName;  // 0x28
        public System.Boolean EnablePropertySerialization;  // 0x30
        public System.Boolean SerializeNonAutoProperties;  // 0x31
        public System.Boolean SerializeNonPublicSetProperties;  // 0x32
        public System.String CustomDateTimeFormatString;  // 0x38
        public System.Boolean Serialize64BitIntegerAsString;  // 0x40
        public System.Boolean SerializeEnumsAsInteger;  // 0x41

        // Methods
        // RVA: 0x045BFF30  token: 0x60003FB
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000DF  // size: 0x18
    public sealed class fsContext
    {
        // Fields
        private readonly System.Collections.Generic.Dictionary<System.Type,System.Object> _contextObjects;  // 0x10

        // Methods
        // RVA: -1  // generic def  token: 0x60003FF
        public System.Void Set(T obj) { }
        // RVA: -1  // generic def  token: 0x6000400
        public T Get() { }
        // RVA: 0x0B36E2B8  token: 0x6000401
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000E0  // size: 0x18
    public abstract class fsConverter : Unity.VisualScripting.FullSerializer.fsBaseConverter
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000402
        public virtual System.Boolean CanProcess(System.Type type) { }
        // RVA: 0x041E1670  token: 0x6000403
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000E1  // size: 0x14
    public sealed struct fsDataType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Unity.VisualScripting.FullSerializer.fsDataType Array;  // const
        public static Unity.VisualScripting.FullSerializer.fsDataType Object;  // const
        public static Unity.VisualScripting.FullSerializer.fsDataType Double;  // const
        public static Unity.VisualScripting.FullSerializer.fsDataType Int64;  // const
        public static Unity.VisualScripting.FullSerializer.fsDataType Boolean;  // const
        public static Unity.VisualScripting.FullSerializer.fsDataType String;  // const
        public static Unity.VisualScripting.FullSerializer.fsDataType Null;  // const

    }

    // TypeToken: 0x20000E2  // size: 0x18
    public sealed class fsData
    {
        // Fields
        private System.Object _value;  // 0x10
        public static readonly Unity.VisualScripting.FullSerializer.fsData True;  // static @ 0x0
        public static readonly Unity.VisualScripting.FullSerializer.fsData False;  // static @ 0x8
        public static readonly Unity.VisualScripting.FullSerializer.fsData Null;  // static @ 0x10

        // Properties
        Unity.VisualScripting.FullSerializer.fsDataType Type { get; /* RVA: 0x0B36F9C8 */ }
        System.Boolean IsNull { get; /* RVA: 0x03BA32C0 */ }
        System.Boolean IsDouble { get; /* RVA: 0x0B36F8A0 */ }
        System.Boolean IsInt64 { get; /* RVA: 0x0B36F8EC */ }
        System.Boolean IsBool { get; /* RVA: 0x0B36F810 */ }
        System.Boolean IsString { get; /* RVA: 0x0B36F97C */ }
        System.Boolean IsDictionary { get; /* RVA: 0x0B36F85C */ }
        System.Boolean IsList { get; /* RVA: 0x0B36F938 */ }
        System.Double AsDouble { get; /* RVA: 0x0B36F720 */ }
        System.Int64 AsInt64 { get; /* RVA: 0x0B36F75C */ }
        System.Boolean AsBool { get; /* RVA: 0x0B36F6A8 */ }
        System.String AsString { get; /* RVA: 0x0B36F7D4 */ }
        System.Collections.Generic.Dictionary<System.String,Unity.VisualScripting.FullSerializer.fsData> AsDictionary { get; /* RVA: 0x0B36F6E4 */ }
        System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsData> AsList { get; /* RVA: 0x0B36F798 */ }

        // Methods
        // RVA: 0x0B36F480  token: 0x6000404
        public virtual System.String ToString() { }
        // RVA: 0x040E20A0  token: 0x6000405
        public System.Void .ctor() { }
        // RVA: 0x0B36F594  token: 0x6000406
        public System.Void .ctor(System.Boolean boolean) { }
        // RVA: 0x0B36F5F0  token: 0x6000407
        public System.Void .ctor(System.Double f) { }
        // RVA: 0x0B36F64C  token: 0x6000408
        public System.Void .ctor(System.Int64 i) { }
        // RVA: 0x053908C0  token: 0x6000409
        public System.Void .ctor(System.String str) { }
        // RVA: 0x053908C0  token: 0x600040A
        public System.Void .ctor(System.Collections.Generic.Dictionary<System.String,Unity.VisualScripting.FullSerializer.fsData> dict) { }
        // RVA: 0x053908C0  token: 0x600040B
        public System.Void .ctor(System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsData> list) { }
        // RVA: 0x0B36ED34  token: 0x600040C
        public static Unity.VisualScripting.FullSerializer.fsData CreateDictionary() { }
        // RVA: 0x0B36EE28  token: 0x600040D
        public static Unity.VisualScripting.FullSerializer.fsData CreateList(System.Int32 capacity) { }
        // RVA: 0x0B36EC50  token: 0x600040E
        private System.Void BecomeDictionary() { }
        // RVA: 0x0B36ECC8  token: 0x600040F
        private Unity.VisualScripting.FullSerializer.fsData Clone() { }
        // RVA: -1  // generic def  token: 0x600041E
        private T Cast() { }
        // RVA: 0x0B36EEC4  token: 0x600041F
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0B36EF28  token: 0x6000420
        public System.Boolean Equals(Unity.VisualScripting.FullSerializer.fsData other) { }
        // RVA: 0x0B36FB24  token: 0x6000421
        public static System.Boolean op_Equality(Unity.VisualScripting.FullSerializer.fsData a, Unity.VisualScripting.FullSerializer.fsData b) { }
        // RVA: 0x0B36FBF4  token: 0x6000422
        public static System.Boolean op_Inequality(Unity.VisualScripting.FullSerializer.fsData a, Unity.VisualScripting.FullSerializer.fsData b) { }
        // RVA: 0x08479504  token: 0x6000423
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B36F488  token: 0x6000424
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000E3  // size: 0x18
    public abstract class fsDirectConverter : Unity.VisualScripting.FullSerializer.fsBaseConverter
    {
        // Properties
        System.Type ModelType { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000426
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000E4
    public abstract class fsDirectConverter`1 : Unity.VisualScripting.FullSerializer.fsDirectConverter
    {
        // Properties
        System.Type ModelType { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000428
        public virtual Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType) { }
        // RVA: -1  // not resolved  token: 0x6000429
        public virtual Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance, System.Type storageType) { }
        // RVA: -1  // abstract  token: 0x600042A
        protected virtual Unity.VisualScripting.FullSerializer.fsResult DoSerialize(TModel model, System.Collections.Generic.Dictionary<System.String,Unity.VisualScripting.FullSerializer.fsData> serialized) { }
        // RVA: -1  // abstract  token: 0x600042B
        protected virtual Unity.VisualScripting.FullSerializer.fsResult DoDeserialize(System.Collections.Generic.Dictionary<System.String,Unity.VisualScripting.FullSerializer.fsData> data, TModel& model) { }
        // RVA: -1  // not resolved  token: 0x600042C
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000E5  // size: 0x90
    public sealed class fsMissingVersionConstructorException : System.Exception
    {
        // Methods
        // RVA: 0x0B379458  token: 0x600042D
        public System.Void .ctor(System.Type versionedType, System.Type constructorType) { }

    }

    // TypeToken: 0x20000E6  // size: 0x90
    public sealed class fsDuplicateVersionNameException : System.Exception
    {
        // Methods
        // RVA: 0x0B3716D4  token: 0x600042E
        public System.Void .ctor(System.Type typeA, System.Type typeB, System.String version) { }

    }

    // TypeToken: 0x20000E7  // size: 0x10
    public sealed class fsIgnoreAttribute : System.Attribute
    {
    }

    // TypeToken: 0x20000E8
    public interface fsISerializationCallbacks
    {
        // Methods
        // RVA: -1  // abstract  token: 0x600042F
        public virtual System.Void OnBeforeSerialize(System.Type storageType) { }
        // RVA: -1  // abstract  token: 0x6000430
        public virtual System.Void OnAfterSerialize(System.Type storageType, Unity.VisualScripting.FullSerializer.fsData& data) { }
        // RVA: -1  // abstract  token: 0x6000431
        public virtual System.Void OnBeforeDeserialize(System.Type storageType, Unity.VisualScripting.FullSerializer.fsData& data) { }
        // RVA: -1  // abstract  token: 0x6000432
        public virtual System.Void OnAfterDeserialize(System.Type storageType) { }

    }

    // TypeToken: 0x20000E9  // size: 0x10
    public class fsSerializationCallbackProcessor : Unity.VisualScripting.FullSerializer.fsObjectProcessor
    {
        // Methods
        // RVA: 0x0B37BE50  token: 0x6000433
        public virtual System.Boolean CanProcess(System.Type type) { }
        // RVA: 0x0B37C150  token: 0x6000434
        public virtual System.Void OnBeforeSerialize(System.Type storageType, System.Object instance) { }
        // RVA: 0x0B37BF44  token: 0x6000435
        public virtual System.Void OnAfterSerialize(System.Type storageType, System.Object instance, Unity.VisualScripting.FullSerializer.fsData& data) { }
        // RVA: 0x0B37BFD4  token: 0x6000436
        public virtual System.Void OnBeforeDeserializeAfterInstanceCreation(System.Type storageType, System.Object instance, Unity.VisualScripting.FullSerializer.fsData& data) { }
        // RVA: 0x0B37BEC8  token: 0x6000437
        public virtual System.Void OnAfterDeserialize(System.Type storageType, System.Object instance) { }
        // RVA: 0x041E1670  token: 0x6000438
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000EA  // size: 0x10
    public class fsSerializationCallbackReceiverProcessor : Unity.VisualScripting.FullSerializer.fsObjectProcessor
    {
        // Methods
        // RVA: 0x0B37C1CC  token: 0x6000439
        public virtual System.Boolean CanProcess(System.Type type) { }
        // RVA: 0x0B37C2C8  token: 0x600043A
        public virtual System.Void OnBeforeSerialize(System.Type storageType, System.Object instance) { }
        // RVA: 0x0B37C244  token: 0x600043B
        public virtual System.Void OnAfterDeserialize(System.Type storageType, System.Object instance) { }
        // RVA: 0x041E1670  token: 0x600043C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000EB  // size: 0x28
    public class fsJsonParser
    {
        // Fields
        private readonly System.Text.StringBuilder _cachedStringBuilder;  // 0x10
        private System.Int32 _start;  // 0x18
        private System.String _input;  // 0x20

        // Methods
        // RVA: 0x0B3758EC  token: 0x600043D
        private System.Void .ctor(System.String input) { }
        // RVA: 0x0B373A2C  token: 0x600043E
        private Unity.VisualScripting.FullSerializer.fsResult MakeFailure(System.String message) { }
        // RVA: 0x0B3742FC  token: 0x600043F
        private System.Boolean TryMoveNext() { }
        // RVA: 0x0B373968  token: 0x6000440
        private System.Boolean HasValue() { }
        // RVA: 0x0B373974  token: 0x6000441
        private System.Boolean HasValue(System.Int32 offset) { }
        // RVA: 0x0B373930  token: 0x6000442
        private System.Char Character() { }
        // RVA: 0x0B37393C  token: 0x6000443
        private System.Char Character(System.Int32 offset) { }
        // RVA: 0x0B374120  token: 0x6000444
        private System.Void SkipSpace() { }
        // RVA: 0x0B3745E0  token: 0x6000445
        private Unity.VisualScripting.FullSerializer.fsResult TryParseExact(System.String content) { }
        // RVA: 0x0B3752F4  token: 0x6000446
        private Unity.VisualScripting.FullSerializer.fsResult TryParseTrue(Unity.VisualScripting.FullSerializer.fsData& data) { }
        // RVA: 0x0B374724  token: 0x6000447
        private Unity.VisualScripting.FullSerializer.fsResult TryParseFalse(Unity.VisualScripting.FullSerializer.fsData& data) { }
        // RVA: 0x0B374824  token: 0x6000448
        private Unity.VisualScripting.FullSerializer.fsResult TryParseNull(Unity.VisualScripting.FullSerializer.fsData& data) { }
        // RVA: 0x0B3739D0  token: 0x6000449
        private System.Boolean IsSeparator(System.Char c) { }
        // RVA: 0x0B374920  token: 0x600044A
        private Unity.VisualScripting.FullSerializer.fsResult TryParseNumber(Unity.VisualScripting.FullSerializer.fsData& data) { }
        // RVA: 0x0B37503C  token: 0x600044B
        private Unity.VisualScripting.FullSerializer.fsResult TryParseString(System.String& str) { }
        // RVA: 0x0B374328  token: 0x600044C
        private Unity.VisualScripting.FullSerializer.fsResult TryParseArray(Unity.VisualScripting.FullSerializer.fsData& arr) { }
        // RVA: 0x0B374C34  token: 0x600044D
        private Unity.VisualScripting.FullSerializer.fsResult TryParseObject(Unity.VisualScripting.FullSerializer.fsData& obj) { }
        // RVA: 0x0B373DDC  token: 0x600044E
        private Unity.VisualScripting.FullSerializer.fsResult RunParse(Unity.VisualScripting.FullSerializer.fsData& data) { }
        // RVA: 0x0B373CF4  token: 0x600044F
        public static Unity.VisualScripting.FullSerializer.fsResult Parse(System.String input, Unity.VisualScripting.FullSerializer.fsData& data) { }
        // RVA: 0x0B373C80  token: 0x6000450
        public static Unity.VisualScripting.FullSerializer.fsData Parse(System.String input) { }
        // RVA: 0x0B3739A8  token: 0x6000451
        private System.Boolean IsHex(System.Char c) { }
        // RVA: 0x0B373BB8  token: 0x6000452
        private System.UInt32 ParseSingleChar(System.Char c1, System.UInt32 multipliyer) { }
        // RVA: 0x0B373BFC  token: 0x6000453
        private System.UInt32 ParseUnicode(System.Char c1, System.Char c2, System.Char c3, System.Char c4) { }
        // RVA: 0x0B3753F4  token: 0x6000454
        private Unity.VisualScripting.FullSerializer.fsResult TryUnescapeChar(System.Char& escaped) { }

    }

    // TypeToken: 0x20000EC  // size: 0x10
    public static class fsJsonPrinter
    {
        // Methods
        // RVA: 0x0B376BE4  token: 0x6000455
        private static System.Void InsertSpacing(System.IO.TextWriter stream, System.Int32 count) { }
        // RVA: 0x0B376928  token: 0x6000456
        private static System.String EscapeString(System.String str) { }
        // RVA: 0x0B375978  token: 0x6000457
        private static System.Void BuildCompressedString(Unity.VisualScripting.FullSerializer.fsData data, System.IO.TextWriter stream) { }
        // RVA: 0x0B375F50  token: 0x6000458
        private static System.Void BuildPrettyString(Unity.VisualScripting.FullSerializer.fsData data, System.IO.TextWriter stream, System.Int32 depth) { }
        // RVA: 0x0B376C50  token: 0x6000459
        public static System.String PrettyJson(Unity.VisualScripting.FullSerializer.fsData data) { }
        // RVA: 0x0B376690  token: 0x600045A
        public static System.String CompressedJson(Unity.VisualScripting.FullSerializer.fsData data) { }
        // RVA: 0x0B3767EC  token: 0x600045B
        private static System.String ConvertDoubleToString(System.Double d) { }

    }

    // TypeToken: 0x20000ED  // size: 0x14
    public sealed struct fsMemberSerialization
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Unity.VisualScripting.FullSerializer.fsMemberSerialization OptIn;  // const
        public static Unity.VisualScripting.FullSerializer.fsMemberSerialization OptOut;  // const
        public static Unity.VisualScripting.FullSerializer.fsMemberSerialization Default;  // const

    }

    // TypeToken: 0x20000EE  // size: 0x38
    public class fsObjectAttribute : System.Attribute
    {
        // Fields
        public System.Type[] PreviousModels;  // 0x10
        public System.String VersionString;  // 0x18
        public Unity.VisualScripting.FullSerializer.fsMemberSerialization MemberSerialization;  // 0x20
        public System.Type Converter;  // 0x28
        public System.Type Processor;  // 0x30

        // Methods
        // RVA: 0x04D91810  token: 0x600045C
        public System.Void .ctor() { }
        // RVA: 0x0B3796C0  token: 0x600045D
        public System.Void .ctor(System.String versionString, System.Type[] previousModels) { }

    }

    // TypeToken: 0x20000EF  // size: 0x10
    public abstract class fsObjectProcessor
    {
        // Methods
        // RVA: 0x0B3796F0  token: 0x600045E
        public virtual System.Boolean CanProcess(System.Type type) { }
        // RVA: 0x041E1670  token: 0x600045F
        public virtual System.Void OnBeforeSerialize(System.Type storageType, System.Object instance) { }
        // RVA: 0x041E1670  token: 0x6000460
        public virtual System.Void OnAfterSerialize(System.Type storageType, System.Object instance, Unity.VisualScripting.FullSerializer.fsData& data) { }
        // RVA: 0x041E1670  token: 0x6000461
        public virtual System.Void OnBeforeDeserialize(System.Type storageType, Unity.VisualScripting.FullSerializer.fsData& data) { }
        // RVA: 0x041E1670  token: 0x6000462
        public virtual System.Void OnBeforeDeserializeAfterInstanceCreation(System.Type storageType, System.Object instance, Unity.VisualScripting.FullSerializer.fsData& data) { }
        // RVA: 0x041E1670  token: 0x6000463
        public virtual System.Void OnAfterDeserialize(System.Type storageType, System.Object instance) { }
        // RVA: 0x041E1670  token: 0x6000464
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000F0  // size: 0x20
    public class fsPropertyAttribute : System.Attribute
    {
        // Fields
        public System.String Name;  // 0x10
        public System.Type Converter;  // 0x18

        // Methods
        // RVA: 0x053908C0  token: 0x6000465
        public System.Void .ctor(System.String name) { }

    }

    // TypeToken: 0x20000F1  // size: 0x20
    public sealed struct fsResult
    {
        // Fields
        private static readonly System.String[] EmptyStringArray;  // static @ 0x0
        private System.Boolean _success;  // 0x10
        private System.Collections.Generic.List<System.String> _messages;  // 0x18
        public static Unity.VisualScripting.FullSerializer.fsResult Success;  // static @ 0x8

        // Properties
        System.Boolean Failed { get; /* RVA: 0x0961F480 */ }
        System.Boolean Succeeded { get; /* RVA: 0x04D883E0 */ }
        System.Boolean HasWarnings { get; /* RVA: 0x0B37BD34 */ }
        System.Exception AsException { get; /* RVA: 0x0B37BB3C */ }
        System.Collections.Generic.IEnumerable<System.String> RawMessages { get; /* RVA: 0x0B37BD80 */ }
        System.String FormattedMessages { get; /* RVA: 0x0B37BC54 */ }

        // Methods
        // RVA: 0x0B37B668  token: 0x6000466
        public System.Void AddMessage(System.String message) { }
        // RVA: 0x0B37B718  token: 0x6000467
        public System.Void AddMessages(Unity.VisualScripting.FullSerializer.fsResult result) { }
        // RVA: 0x0B37B908  token: 0x6000468
        public Unity.VisualScripting.FullSerializer.fsResult Merge(Unity.VisualScripting.FullSerializer.fsResult other) { }
        // RVA: 0x0B37B9EC  token: 0x6000469
        public static Unity.VisualScripting.FullSerializer.fsResult Warn(System.String warning) { }
        // RVA: 0x0B37B85C  token: 0x600046A
        public static Unity.VisualScripting.FullSerializer.fsResult Fail(System.String warning) { }
        // RVA: 0x0B37BDD8  token: 0x600046B
        public static Unity.VisualScripting.FullSerializer.fsResult op_Addition(Unity.VisualScripting.FullSerializer.fsResult a, Unity.VisualScripting.FullSerializer.fsResult b) { }
        // RVA: 0x0B37B7D0  token: 0x600046F
        public Unity.VisualScripting.FullSerializer.fsResult AssertSuccess() { }
        // RVA: 0x0B37BA9C  token: 0x6000473
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000F2  // size: 0x68
    public class fsSerializer
    {
        // Fields
        private readonly System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsConverter> _availableConverters;  // 0x10
        private readonly System.Collections.Generic.Dictionary<System.Type,Unity.VisualScripting.FullSerializer.fsDirectConverter> _availableDirectConverters;  // 0x18
        private readonly System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsObjectProcessor> _processors;  // 0x20
        private readonly Unity.VisualScripting.FullSerializer.Internal.fsCyclicReferenceManager _references;  // 0x28
        private readonly Unity.VisualScripting.FullSerializer.fsSerializer.fsLazyCycleDefinitionWriter _lazyReferenceWriter;  // 0x30
        private readonly System.Collections.Generic.Dictionary<System.Type,System.Type> _abstractTypeRemap;  // 0x38
        private System.Collections.Generic.Dictionary<System.Type,Unity.VisualScripting.FullSerializer.fsBaseConverter> _cachedConverterTypeInstances;  // 0x40
        private System.Collections.Generic.Dictionary<System.Type,Unity.VisualScripting.FullSerializer.fsBaseConverter> _cachedConverters;  // 0x48
        private System.Collections.Generic.Dictionary<System.Type,System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsObjectProcessor>> _cachedProcessors;  // 0x50
        public Unity.VisualScripting.FullSerializer.fsContext Context;  // 0x58
        public Unity.VisualScripting.FullSerializer.fsConfig Config;  // 0x60
        private static System.Collections.Generic.HashSet<System.String> _reservedKeywords;  // static @ 0x0
        private static readonly System.String Key_ObjectReference;  // static @ 0x8
        private static readonly System.String Key_ObjectDefinition;  // static @ 0x10
        private static readonly System.String Key_InstanceType;  // static @ 0x18
        private static readonly System.String Key_Version;  // static @ 0x20
        private static readonly System.String Key_Content;  // static @ 0x28
        private static readonly System.String Key_UnitDefault;  // static @ 0x30
        private static readonly System.String Key_UnitPosition;  // static @ 0x38
        private static readonly System.String Key_UnitGuid;  // static @ 0x40
        private static readonly System.String Key_UnitFormerType;  // static @ 0x48
        private static readonly System.String Key_UnitFormerValue;  // static @ 0x50
        private static readonly System.String TypeName_Unit;  // static @ 0x58
        private static readonly System.Type Type_Unit;  // static @ 0x60
        private static readonly System.String TypeName_MissingType;  // static @ 0x68
        private static readonly System.Type Type_MissingType;  // static @ 0x70

        // Methods
        // RVA: 0x0B380B84  token: 0x6000474
        public System.Void .ctor() { }
        // RVA: 0x0B37FC30  token: 0x6000475
        private System.Void RemapAbstractStorageTypeToDefaultType(System.Type& storageType) { }
        // RVA: 0x0B37FDC0  token: 0x6000476
        public System.Void SetDefaultStorageType(System.Type abstractType, System.Type defaultStorageType) { }
        // RVA: 0x0B37DB94  token: 0x6000477
        private System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsObjectProcessor> GetProcessors(System.Type type) { }
        // RVA: 0x0B37C34C  token: 0x6000478
        public System.Void AddConverter(Unity.VisualScripting.FullSerializer.fsBaseConverter converter) { }
        // RVA: 0x0B37C9A8  token: 0x6000479
        private Unity.VisualScripting.FullSerializer.fsBaseConverter GetConverter(System.Type type, System.Type overrideConverterType) { }
        // RVA: -1  // generic def  token: 0x600047A
        public Unity.VisualScripting.FullSerializer.fsResult TrySerialize(T instance, Unity.VisualScripting.FullSerializer.fsData& data) { }
        // RVA: -1  // generic def  token: 0x600047B
        public Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, T& instance) { }
        // RVA: 0x0B3805F8  token: 0x600047C
        public Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Type storageType, System.Object instance, Unity.VisualScripting.FullSerializer.fsData& data) { }
        // RVA: 0x0B38025C  token: 0x600047D
        public Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Type storageType, System.Type overrideConverterType, System.Object instance, Unity.VisualScripting.FullSerializer.fsData& data) { }
        // RVA: 0x0B37EAC0  token: 0x600047E
        private Unity.VisualScripting.FullSerializer.fsResult InternalSerialize_1_ProcessCycles(System.Type storageType, System.Type overrideConverterType, System.Object instance, Unity.VisualScripting.FullSerializer.fsData& data) { }
        // RVA: 0x0B37EE7C  token: 0x600047F
        private Unity.VisualScripting.FullSerializer.fsResult InternalSerialize_2_Inheritance(System.Type storageType, System.Type overrideConverterType, System.Object instance, Unity.VisualScripting.FullSerializer.fsData& data) { }
        // RVA: 0x0B37F118  token: 0x6000480
        private Unity.VisualScripting.FullSerializer.fsResult InternalSerialize_3_ProcessVersioning(System.Type overrideConverterType, System.Object instance, Unity.VisualScripting.FullSerializer.fsData& data) { }
        // RVA: 0x0B37F328  token: 0x6000481
        private Unity.VisualScripting.FullSerializer.fsResult InternalSerialize_4_Converter(System.Type overrideConverterType, System.Object instance, Unity.VisualScripting.FullSerializer.fsData& data) { }
        // RVA: 0x0B380220  token: 0x6000482
        public Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType, System.Object& result) { }
        // RVA: 0x0B37FEBC  token: 0x6000483
        public Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType, System.Type overrideConverterType, System.Object& result) { }
        // RVA: 0x0B37DDF0  token: 0x6000484
        private Unity.VisualScripting.FullSerializer.fsResult InternalDeserialize_1_CycleReference(System.Type overrideConverterType, Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType, System.Object& result, System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsObjectProcessor>& processors) { }
        // RVA: 0x0B37DFA4  token: 0x6000485
        private Unity.VisualScripting.FullSerializer.fsResult InternalDeserialize_2_Version(System.Type overrideConverterType, Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType, System.Object& result, System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsObjectProcessor>& processors) { }
        // RVA: 0x0B37E418  token: 0x6000486
        private Unity.VisualScripting.FullSerializer.fsResult InternalDeserialize_3_Inheritance(System.Type overrideConverterType, Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType, System.Object& result, System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsObjectProcessor>& processors) { }
        // RVA: 0x0B37E850  token: 0x6000487
        private Unity.VisualScripting.FullSerializer.fsResult InternalDeserialize_4_Cycles(System.Type overrideConverterType, Unity.VisualScripting.FullSerializer.fsData data, System.Type resultType, System.Object& result) { }
        // RVA: 0x0B37E990  token: 0x6000488
        private Unity.VisualScripting.FullSerializer.fsResult InternalDeserialize_5_Converter(System.Type overrideConverterType, Unity.VisualScripting.FullSerializer.fsData data, System.Type resultType, System.Object& result) { }
        // RVA: 0x0B37CD98  token: 0x6000489
        private static System.Type GetDataType(Unity.VisualScripting.FullSerializer.fsData& data, System.Type defaultType, Unity.VisualScripting.FullSerializer.fsResult& deserializeResult) { }
        // RVA: 0x0B37C8F4  token: 0x600048A
        private static System.Void EnsureDictionary(Unity.VisualScripting.FullSerializer.fsData data) { }
        // RVA: 0x0B380638  token: 0x600048B
        private static System.Void .cctor() { }
        // RVA: 0x0B37F808  token: 0x600048C
        public static System.Boolean IsReservedKeyword(System.String key) { }
        // RVA: 0x0B37F770  token: 0x600048D
        private static System.Boolean IsObjectReference(Unity.VisualScripting.FullSerializer.fsData data) { }
        // RVA: 0x0B37F6D8  token: 0x600048E
        private static System.Boolean IsObjectDefinition(Unity.VisualScripting.FullSerializer.fsData data) { }
        // RVA: 0x0B37F910  token: 0x600048F
        private static System.Boolean IsVersioned(Unity.VisualScripting.FullSerializer.fsData data) { }
        // RVA: 0x0B37F878  token: 0x6000490
        private static System.Boolean IsTypeSpecified(Unity.VisualScripting.FullSerializer.fsData data) { }
        // RVA: 0x0B37FB98  token: 0x6000491
        private static System.Boolean IsWrappedData(Unity.VisualScripting.FullSerializer.fsData data) { }
        // RVA: 0x0B37F9A8  token: 0x6000492
        private static System.Boolean IsVisualScriptingUnit(Unity.VisualScripting.FullSerializer.fsData data) { }
        // RVA: 0x0B37C5D0  token: 0x6000493
        private static System.Void ConvertLegacyData(Unity.VisualScripting.FullSerializer.fsData& data) { }
        // RVA: 0x0B37F63C  token: 0x6000494
        private static System.Void Invoke_OnBeforeSerialize(System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsObjectProcessor> processors, System.Type storageType, System.Object instance) { }
        // RVA: 0x0B37F478  token: 0x6000495
        private static System.Void Invoke_OnAfterSerialize(System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsObjectProcessor> processors, System.Type storageType, System.Object instance, Unity.VisualScripting.FullSerializer.fsData& data) { }
        // RVA: 0x0B37F5D0  token: 0x6000496
        private static System.Void Invoke_OnBeforeDeserialize(System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsObjectProcessor> processors, System.Type storageType, Unity.VisualScripting.FullSerializer.fsData& data) { }
        // RVA: 0x0B37F524  token: 0x6000497
        private static System.Void Invoke_OnBeforeDeserializeAfterInstanceCreation(System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsObjectProcessor> processors, System.Type storageType, System.Object instance, Unity.VisualScripting.FullSerializer.fsData& data) { }
        // RVA: 0x0B37F3E0  token: 0x6000498
        private static System.Void Invoke_OnAfterDeserialize(System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsObjectProcessor> processors, System.Type storageType, System.Object instance) { }

    }

    // TypeToken: 0x20000F4  // size: 0x48
    public class fsMetaProperty
    {
        // Fields
        private System.Reflection.MemberInfo _memberInfo;  // 0x10
        private System.Type <StorageType>k__BackingField;  // 0x18
        private System.Type <OverrideConverterType>k__BackingField;  // 0x20
        private System.Boolean <CanRead>k__BackingField;  // 0x28
        private System.Boolean <CanWrite>k__BackingField;  // 0x29
        private System.String <JsonName>k__BackingField;  // 0x30
        private System.String <MemberName>k__BackingField;  // 0x38
        private System.Boolean <IsPublic>k__BackingField;  // 0x40
        private System.Boolean <IsReadOnly>k__BackingField;  // 0x41

        // Properties
        System.Type StorageType { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.Type OverrideConverterType { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        System.Boolean CanRead { get; /* RVA: 0x011F33C0 */ set; /* RVA: 0x011F33D0 */ }
        System.Boolean CanWrite { get; /* RVA: 0x04D867B0 */ set; /* RVA: 0x04D867C0 */ }
        System.String JsonName { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }
        System.String MemberName { get; /* RVA: 0x04D85A50 */ set; /* RVA: 0x046A91B0 */ }
        System.Boolean IsPublic { get; /* RVA: 0x02D480A0 */ set; /* RVA: 0x02D480B0 */ }
        System.Boolean IsReadOnly { get; /* RVA: 0x04D86F40 */ set; /* RVA: 0x04D86F60 */ }

        // Methods
        // RVA: 0x0B377E4C  token: 0x600049D
        private System.Void .ctor(Unity.VisualScripting.FullSerializer.fsConfig config, System.Reflection.FieldInfo field) { }
        // RVA: 0x0B377D2C  token: 0x600049E
        private System.Void .ctor(Unity.VisualScripting.FullSerializer.fsConfig config, System.Reflection.PropertyInfo property) { }
        // RVA: 0x0B377994  token: 0x60004AF
        private System.Void CommonInitialize(Unity.VisualScripting.FullSerializer.fsConfig config) { }
        // RVA: 0x0B377B34  token: 0x60004B0
        public System.Void Write(System.Object context, System.Object value) { }
        // RVA: 0x0B377A6C  token: 0x60004B1
        public System.Object Read(System.Object context) { }

    }

    // TypeToken: 0x20000F5  // size: 0x28
    public class fsMetaType
    {
        // Fields
        public System.Type ReflectedType;  // 0x10
        private System.Boolean _hasEmittedAotData;  // 0x18
        private System.Nullable<System.Boolean> _hasDefaultConstructorCache;  // 0x19
        private System.Boolean _isDefaultConstructorPublic;  // 0x1b
        private Unity.VisualScripting.FullSerializer.fsMetaProperty[] <Properties>k__BackingField;  // 0x20
        private static System.Collections.Generic.Dictionary<Unity.VisualScripting.FullSerializer.fsConfig,System.Collections.Generic.Dictionary<System.Type,Unity.VisualScripting.FullSerializer.fsMetaType>> _configMetaTypes;  // static @ 0x0

        // Properties
        Unity.VisualScripting.FullSerializer.fsMetaProperty[] Properties { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        System.Boolean HasDefaultConstructor { get; /* RVA: 0x0B3792EC */ }

        // Methods
        // RVA: 0x0B37920C  token: 0x60004B2
        private System.Void .ctor(Unity.VisualScripting.FullSerializer.fsConfig config, System.Type reflectedType) { }
        // RVA: 0x0B378D38  token: 0x60004B6
        public System.Boolean EmitAotData() { }
        // RVA: 0x0B37882C  token: 0x60004B7
        public System.Object CreateInstance() { }
        // RVA: 0x0B378E10  token: 0x60004B8
        public static Unity.VisualScripting.FullSerializer.fsMetaType Get(Unity.VisualScripting.FullSerializer.fsConfig config, System.Type type) { }
        // RVA: 0x0B3783A4  token: 0x60004B9
        private static System.Void CollectProperties(Unity.VisualScripting.FullSerializer.fsConfig config, System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsMetaProperty> properties, System.Type reflectedType) { }
        // RVA: 0x0B3790B8  token: 0x60004BA
        private static System.Boolean IsAutoProperty(System.Reflection.PropertyInfo property, System.Reflection.MemberInfo[] members) { }
        // RVA: 0x0B3780D0  token: 0x60004BB
        private static System.Boolean CanSerializeProperty(Unity.VisualScripting.FullSerializer.fsConfig config, System.Reflection.PropertyInfo property, System.Reflection.MemberInfo[] members, System.Boolean annotationFreeValue) { }
        // RVA: 0x0B377EF0  token: 0x60004BC
        private static System.Boolean CanSerializeField(Unity.VisualScripting.FullSerializer.fsConfig config, System.Reflection.FieldInfo field, System.Boolean annotationFreeValue) { }
        // RVA: 0x0B379180  token: 0x60004BD
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000F9  // size: 0x10
    public static class fsReflectionUtility
    {
        // Methods
        // RVA: 0x0B37B4A0  token: 0x60004C6
        public static System.Type GetInterface(System.Type type, System.Type interfaceType) { }

    }

}

namespace Unity.VisualScripting.FullSerializer.Internal
{

    // TypeToken: 0x20000FA  // size: 0x30
    public class fsCyclicReferenceManager
    {
        // Fields
        private System.Collections.Generic.Dictionary<System.Object,System.Int32> _objectIds;  // 0x10
        private System.Int32 _nextId;  // 0x18
        private System.Collections.Generic.Dictionary<System.Int32,System.Object> _marked;  // 0x20
        private System.Int32 _depth;  // 0x28

        // Methods
        // RVA: 0x04D9EBD0  token: 0x60004C7
        public System.Void Enter() { }
        // RVA: 0x0B36E6A8  token: 0x60004C8
        public System.Boolean Exit() { }
        // RVA: 0x0B36E8B4  token: 0x60004C9
        public System.Object GetReferenceObject(System.Int32 id) { }
        // RVA: 0x0B36E644  token: 0x60004CA
        public System.Void AddReferenceWithId(System.Int32 id, System.Object reference) { }
        // RVA: 0x0B36E80C  token: 0x60004CB
        public System.Int32 GetReferenceId(System.Object item) { }
        // RVA: 0x0B36E9BC  token: 0x60004CC
        public System.Boolean IsReference(System.Object item) { }
        // RVA: 0x0B36EA2C  token: 0x60004CD
        public System.Void MarkSerialized(System.Object item) { }
        // RVA: 0x0B36EB5C  token: 0x60004CE
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000FC
    public sealed struct fsOption`1
    {
        // Fields
        private System.Boolean _hasValue;  // 0x0
        private T _value;  // 0x0

        // Properties
        System.Boolean HasValue { get; /* RVA: -1  // not resolved */ }
        System.Boolean IsEmpty { get; /* RVA: -1  // not resolved */ }
        T Value { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60004D6
        public System.Void .ctor(T value) { }

    }

    // TypeToken: 0x20000FD  // size: 0x10
    public static class fsOption
    {
        // Methods
        // RVA: -1  // generic def  token: 0x60004D7
        public static Unity.VisualScripting.FullSerializer.Internal.fsOption<T> Just(T value) { }

    }

    // TypeToken: 0x20000FE  // size: 0x10
    public static class fsPortableReflection
    {
        // Fields
        public static System.Type[] EmptyTypes;  // static @ 0x0
        private static System.Collections.Generic.IDictionary<Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.AttributeQuery,System.Attribute> _cachedAttributeQueries;  // static @ 0x8
        private static System.Reflection.BindingFlags DeclaredFlags;  // static @ 0x10

        // Methods
        // RVA: 0x0B379DE4  token: 0x60004D8
        public static System.Boolean HasAttribute(System.Reflection.MemberInfo element, System.Type attributeType) { }
        // RVA: 0x0539131C  token: 0x60004D9
        public static System.Boolean HasAttribute(System.Reflection.MemberInfo element, System.Type attributeType, System.Boolean shouldCache) { }
        // RVA: 0x0B37973C  token: 0x60004DA
        public static System.Attribute GetAttribute(System.Reflection.MemberInfo element, System.Type attributeType, System.Boolean shouldCache) { }
        // RVA: -1  // generic def  token: 0x60004DB
        public static TAttribute GetAttribute(System.Reflection.MemberInfo element, System.Boolean shouldCache) { }
        // RVA: -1  // generic def  token: 0x60004DC
        public static TAttribute GetAttribute(System.Reflection.MemberInfo element) { }
        // RVA: 0x0B379C30  token: 0x60004DD
        public static System.Reflection.PropertyInfo GetDeclaredProperty(System.Type type, System.String propertyName) { }
        // RVA: 0x0B379AC4  token: 0x60004DE
        public static System.Reflection.MethodInfo GetDeclaredMethod(System.Type type, System.String methodName) { }
        // RVA: 0x0B3798A4  token: 0x60004DF
        public static System.Reflection.ConstructorInfo GetDeclaredConstructor(System.Type type, System.Type[] parameters) { }
        // RVA: 0x0B3799A8  token: 0x60004E0
        public static System.Reflection.ConstructorInfo[] GetDeclaredConstructors(System.Type type) { }
        // RVA: 0x0B379CE4  token: 0x60004E1
        public static System.Reflection.MethodInfo GetFlattenedMethod(System.Type type, System.String methodName) { }
        // RVA: 0x0B379B78  token: 0x60004E2
        public static System.Reflection.MethodInfo[] GetDeclaredMethods(System.Type type) { }
        // RVA: 0x0B379BD4  token: 0x60004E3
        public static System.Reflection.PropertyInfo[] GetDeclaredProperties(System.Type type) { }
        // RVA: 0x0B379A0C  token: 0x60004E4
        public static System.Reflection.FieldInfo[] GetDeclaredFields(System.Type type) { }
        // RVA: 0x0B379A68  token: 0x60004E5
        public static System.Reflection.MemberInfo[] GetDeclaredMembers(System.Type type) { }
        // RVA: 0x0464E630  token: 0x60004E6
        public static System.Type Resolve(System.Type type) { }
        // RVA: 0x0B379E38  token: 0x60004E7
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000101  // size: 0x10
    public static class fsTypeExtensions
    {
        // Methods
        // RVA: 0x0B381E24  token: 0x60004EB
        public static System.String CSharpName(System.Type type) { }
        // RVA: 0x0B3818C8  token: 0x60004EC
        public static System.String CSharpName(System.Type type, System.Boolean includeNamespace) { }

    }

    // TypeToken: 0x2000103  // size: 0x28
    public sealed struct fsVersionedType
    {
        // Fields
        public Unity.VisualScripting.FullSerializer.Internal.fsVersionedType[] Ancestors;  // 0x10
        public System.String VersionString;  // 0x18
        public System.Type ModelType;  // 0x20

        // Methods
        // RVA: 0x0B382B10  token: 0x60004EF
        public System.Object Migrate(System.Object ancestorInstance) { }
        // RVA: 0x0B382B9C  token: 0x60004F0
        public virtual System.String ToString() { }
        // RVA: 0x0B382A94  token: 0x60004F1
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x08479504  token: 0x60004F2
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x2000104  // size: 0x10
    public static class fsVersionManager
    {
        // Fields
        private static readonly System.Collections.Generic.Dictionary<System.Type,Unity.VisualScripting.FullSerializer.Internal.fsOption<Unity.VisualScripting.FullSerializer.Internal.fsVersionedType>> _cache;  // static @ 0x0

        // Methods
        // RVA: 0x0B381F58  token: 0x60004F3
        public static Unity.VisualScripting.FullSerializer.fsResult GetVersionImportPath(System.String currentVersion, Unity.VisualScripting.FullSerializer.Internal.fsVersionedType targetVersion, System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.Internal.fsVersionedType>& path) { }
        // RVA: 0x0B381E30  token: 0x60004F4
        private static System.Boolean GetVersionImportPathRecursive(System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.Internal.fsVersionedType> path, System.String currentVersion, Unity.VisualScripting.FullSerializer.Internal.fsVersionedType current) { }
        // RVA: 0x0B382190  token: 0x60004F5
        public static Unity.VisualScripting.FullSerializer.Internal.fsOption<Unity.VisualScripting.FullSerializer.Internal.fsVersionedType> GetVersionedType(System.Type type) { }
        // RVA: 0x0B382614  token: 0x60004F6
        private static System.Void VerifyConstructors(Unity.VisualScripting.FullSerializer.Internal.fsVersionedType type) { }
        // RVA: 0x0B38279C  token: 0x60004F7
        private static System.Void VerifyUniqueVersionStrings(Unity.VisualScripting.FullSerializer.Internal.fsVersionedType type) { }
        // RVA: 0x0B382A08  token: 0x60004F8
        private static System.Void .cctor() { }

    }

}

