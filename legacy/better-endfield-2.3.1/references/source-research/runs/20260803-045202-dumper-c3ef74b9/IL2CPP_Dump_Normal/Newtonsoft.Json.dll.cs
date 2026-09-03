// ========================================================
// Dumped by @desirepro
// Assembly: Newtonsoft.Json.dll
// Classes:  309
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x200000D  // size: 0x28
    public class Entry
    {
        // Fields
        private readonly System.String Value;  // 0x10
        private readonly System.Int32 HashCode;  // 0x18
        private Newtonsoft.Json.DefaultJsonNameTable.Entry Next;  // 0x20

        // Methods
        // RVA: 0x032B2C30  token: 0x600000F
        private System.Void .ctor(System.String value, System.Int32 hashCode, Newtonsoft.Json.DefaultJsonNameTable.Entry next) { }

    }

    // TypeToken: 0x2000026  // size: 0x14
    public sealed struct State
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.JsonReader.State Start;  // const
        public static Newtonsoft.Json.JsonReader.State Complete;  // const
        public static Newtonsoft.Json.JsonReader.State Property;  // const
        public static Newtonsoft.Json.JsonReader.State ObjectStart;  // const
        public static Newtonsoft.Json.JsonReader.State Object;  // const
        public static Newtonsoft.Json.JsonReader.State ArrayStart;  // const
        public static Newtonsoft.Json.JsonReader.State Array;  // const
        public static Newtonsoft.Json.JsonReader.State Closed;  // const
        public static Newtonsoft.Json.JsonReader.State PostValue;  // const
        public static Newtonsoft.Json.JsonReader.State ConstructorStart;  // const
        public static Newtonsoft.Json.JsonReader.State Constructor;  // const
        public static Newtonsoft.Json.JsonReader.State Error;  // const
        public static Newtonsoft.Json.JsonReader.State Finished;  // const

    }

    // TypeToken: 0x2000031  // size: 0x14
    public sealed struct State
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.JsonWriter.State Start;  // const
        public static Newtonsoft.Json.JsonWriter.State Property;  // const
        public static Newtonsoft.Json.JsonWriter.State ObjectStart;  // const
        public static Newtonsoft.Json.JsonWriter.State Object;  // const
        public static Newtonsoft.Json.JsonWriter.State ArrayStart;  // const
        public static Newtonsoft.Json.JsonWriter.State Array;  // const
        public static Newtonsoft.Json.JsonWriter.State ConstructorStart;  // const
        public static Newtonsoft.Json.JsonWriter.State Constructor;  // const
        public static Newtonsoft.Json.JsonWriter.State Closed;  // const
        public static Newtonsoft.Json.JsonWriter.State Error;  // const

    }

    // TypeToken: 0x2000040
    public sealed class <>c__2`1
    {
        // Fields
        public static readonly Newtonsoft.Json.Utilities.AotHelper.<>c__2<T> <>9;  // static @ 0x0
        public static System.Action <>9__2_0;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600020B
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x600020C
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x600020D
        private System.Void <EnsureList>b__2_0() { }

    }

    // TypeToken: 0x2000045
    public static class EmptyArrayContainer`1
    {
        // Fields
        public static readonly T[] Empty;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600022D
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200004C  // size: 0x14
    public sealed struct ConvertResult
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult Success;  // const
        public static Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult CannotConvertNull;  // const
        public static Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult NotInstantiableType;  // const
        public static Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult NoValidConversion;  // const

    }

    // TypeToken: 0x200004D  // size: 0x18
    public sealed class <>c__DisplayClass8_0
    {
        // Fields
        public Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object> call;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000261
        public System.Void .ctor() { }
        // RVA: 0x0A8BACF4  token: 0x6000262
        private System.Object <CreateCastConverter>b__0(System.Object o) { }

    }

    // TypeToken: 0x2000053
    public sealed struct DictionaryEnumerator`2 : System.Collections.IDictionaryEnumerator, System.Collections.IEnumerator
    {
        // Fields
        private readonly System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TEnumeratorKey,TEnumeratorValue>> _e;  // 0x0

        // Properties
        System.Collections.DictionaryEntry Entry { get; /* RVA: -1  // not resolved */ }
        System.Object Key { get; /* RVA: -1  // not resolved */ }
        System.Object Value { get; /* RVA: -1  // not resolved */ }
        System.Object Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60002AD
        public System.Void .ctor(System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TEnumeratorKey,TEnumeratorValue>> e) { }
        // RVA: -1  // not resolved  token: 0x60002B2
        public virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x60002B3
        public virtual System.Void Reset() { }

    }

    // TypeToken: 0x2000054
    public sealed class <>c
    {
        // Fields
        public static readonly Newtonsoft.Json.Utilities.DictionaryWrapper.<>c<TKey,TValue> <>9;  // static @ 0x0
        public static System.Func<System.Collections.DictionaryEntry,System.Collections.Generic.KeyValuePair<TKey,TValue>> <>9__29_0;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60002B4
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x60002B5
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60002B6
        private System.Collections.Generic.KeyValuePair<TKey,TValue> <GetEnumerator>b__29_0(System.Collections.DictionaryEntry de) { }

    }

    // TypeToken: 0x2000057
    public sealed class Fallback : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x60002DE
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x60002DF
        public virtual System.Dynamic.DynamicMetaObject Invoke(System.Dynamic.DynamicMetaObject errorSuggestion) { }

    }

    // TypeToken: 0x2000058
    public sealed class GetBinderAdapter : System.Dynamic.GetMemberBinder
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60002E0
        private System.Void .ctor(System.Dynamic.InvokeMemberBinder binder) { }
        // RVA: -1  // not resolved  token: 0x60002E1
        public virtual System.Dynamic.DynamicMetaObject FallbackGetMember(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject errorSuggestion) { }

    }

    // TypeToken: 0x2000059
    public sealed class <>c
    {
        // Fields
        public static readonly Newtonsoft.Json.Utilities.DynamicProxyMetaObject.<>c<T> <>9;  // static @ 0x0
        public static System.Func<System.Dynamic.DynamicMetaObject,System.Linq.Expressions.Expression> <>9__18_0;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60002E2
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x60002E3
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60002E4
        private System.Linq.Expressions.Expression <GetArgs>b__18_0(System.Dynamic.DynamicMetaObject arg) { }

    }

    // TypeToken: 0x200005A
    public sealed class <>c__DisplayClass10_0
    {
        // Fields
        public System.Dynamic.BinaryOperationBinder binder;  // 0x0
        public Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T> <>4__this;  // 0x0
        public System.Dynamic.DynamicMetaObject arg;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60002E5
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60002E6
        private System.Dynamic.DynamicMetaObject <BindBinaryOperation>b__0(System.Dynamic.DynamicMetaObject e) { }

    }

    // TypeToken: 0x200005B
    public sealed class <>c__DisplayClass11_0
    {
        // Fields
        public System.Dynamic.UnaryOperationBinder binder;  // 0x0
        public Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T> <>4__this;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60002E7
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60002E8
        private System.Dynamic.DynamicMetaObject <BindUnaryOperation>b__0(System.Dynamic.DynamicMetaObject e) { }

    }

    // TypeToken: 0x200005C
    public sealed class <>c__DisplayClass12_0
    {
        // Fields
        public System.Dynamic.GetIndexBinder binder;  // 0x0
        public Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T> <>4__this;  // 0x0
        public System.Dynamic.DynamicMetaObject[] indexes;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60002E9
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60002EA
        private System.Dynamic.DynamicMetaObject <BindGetIndex>b__0(System.Dynamic.DynamicMetaObject e) { }

    }

    // TypeToken: 0x200005D
    public sealed class <>c__DisplayClass13_0
    {
        // Fields
        public System.Dynamic.SetIndexBinder binder;  // 0x0
        public Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T> <>4__this;  // 0x0
        public System.Dynamic.DynamicMetaObject[] indexes;  // 0x0
        public System.Dynamic.DynamicMetaObject value;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60002EB
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60002EC
        private System.Dynamic.DynamicMetaObject <BindSetIndex>b__0(System.Dynamic.DynamicMetaObject e) { }

    }

    // TypeToken: 0x200005E
    public sealed class <>c__DisplayClass14_0
    {
        // Fields
        public System.Dynamic.DeleteIndexBinder binder;  // 0x0
        public Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T> <>4__this;  // 0x0
        public System.Dynamic.DynamicMetaObject[] indexes;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60002ED
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60002EE
        private System.Dynamic.DynamicMetaObject <BindDeleteIndex>b__0(System.Dynamic.DynamicMetaObject e) { }

    }

    // TypeToken: 0x200005F
    public sealed class <>c__DisplayClass3_0
    {
        // Fields
        public System.Dynamic.GetMemberBinder binder;  // 0x0
        public Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T> <>4__this;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60002EF
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60002F0
        private System.Dynamic.DynamicMetaObject <BindGetMember>b__0(System.Dynamic.DynamicMetaObject e) { }

    }

    // TypeToken: 0x2000060
    public sealed class <>c__DisplayClass4_0
    {
        // Fields
        public System.Dynamic.SetMemberBinder binder;  // 0x0
        public Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T> <>4__this;  // 0x0
        public System.Dynamic.DynamicMetaObject value;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60002F1
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60002F2
        private System.Dynamic.DynamicMetaObject <BindSetMember>b__0(System.Dynamic.DynamicMetaObject e) { }

    }

    // TypeToken: 0x2000061
    public sealed class <>c__DisplayClass5_0
    {
        // Fields
        public System.Dynamic.DeleteMemberBinder binder;  // 0x0
        public Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T> <>4__this;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60002F3
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60002F4
        private System.Dynamic.DynamicMetaObject <BindDeleteMember>b__0(System.Dynamic.DynamicMetaObject e) { }

    }

    // TypeToken: 0x2000062
    public sealed class <>c__DisplayClass6_0
    {
        // Fields
        public System.Dynamic.ConvertBinder binder;  // 0x0
        public Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T> <>4__this;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60002F5
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60002F6
        private System.Dynamic.DynamicMetaObject <BindConvert>b__0(System.Dynamic.DynamicMetaObject e) { }

    }

    // TypeToken: 0x2000063
    public sealed class <>c__DisplayClass7_0
    {
        // Fields
        public System.Dynamic.InvokeMemberBinder binder;  // 0x0
        public Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T> <>4__this;  // 0x0
        public System.Dynamic.DynamicMetaObject[] args;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60002F7
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60002F8
        private System.Dynamic.DynamicMetaObject <BindInvokeMember>b__0(System.Dynamic.DynamicMetaObject e) { }
        // RVA: -1  // not resolved  token: 0x60002F9
        private System.Dynamic.DynamicMetaObject <BindInvokeMember>b__1(System.Dynamic.DynamicMetaObject e) { }

    }

    // TypeToken: 0x2000064
    public sealed class <>c__DisplayClass8_0
    {
        // Fields
        public System.Dynamic.CreateInstanceBinder binder;  // 0x0
        public Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T> <>4__this;  // 0x0
        public System.Dynamic.DynamicMetaObject[] args;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60002FA
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60002FB
        private System.Dynamic.DynamicMetaObject <BindCreateInstance>b__0(System.Dynamic.DynamicMetaObject e) { }

    }

    // TypeToken: 0x2000065
    public sealed class <>c__DisplayClass9_0
    {
        // Fields
        public System.Dynamic.InvokeBinder binder;  // 0x0
        public Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T> <>4__this;  // 0x0
        public System.Dynamic.DynamicMetaObject[] args;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60002FC
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x60002FD
        private System.Dynamic.DynamicMetaObject <BindInvoke>b__0(System.Dynamic.DynamicMetaObject e) { }

    }

    // TypeToken: 0x2000067  // size: 0x10
    public static class BinderWrapper
    {
        // Fields
        private static System.Object _getCSharpArgumentInfoArray;  // static @ 0x0
        private static System.Object _setCSharpArgumentInfoArray;  // static @ 0x8
        private static Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object> _getMemberCall;  // static @ 0x10
        private static Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object> _setMemberCall;  // static @ 0x18
        private static System.Boolean _init;  // static @ 0x20

        // Methods
        // RVA: 0x0A8AFA20  token: 0x60002FF
        private static System.Void Init() { }
        // RVA: 0x0A8AF684  token: 0x6000300
        private static System.Object CreateSharpArgumentInfoArray(System.Int32[] values) { }
        // RVA: 0x0A8AF2B8  token: 0x6000301
        private static System.Void CreateMemberCalls() { }
        // RVA: 0x0A8AF8C0  token: 0x6000302
        public static System.Runtime.CompilerServices.CallSiteBinder GetMember(System.String name, System.Type context) { }
        // RVA: 0x0A8AFC1C  token: 0x6000303
        public static System.Runtime.CompilerServices.CallSiteBinder SetMember(System.String name, System.Type context) { }

    }

    // TypeToken: 0x200006D  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Newtonsoft.Json.Utilities.EnumUtils.<>c <>9;  // static @ 0x0
        public static System.Func<System.Runtime.Serialization.EnumMemberAttribute,System.String> <>9__3_0;  // static @ 0x8

        // Methods
        // RVA: 0x04D4B250  token: 0x6000315
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000316
        public System.Void .ctor() { }
        // RVA: 0x06C08920  token: 0x6000317
        private System.String <InitializeValuesAndNames>b__3_0(System.Runtime.Serialization.EnumMemberAttribute a) { }

    }

    // TypeToken: 0x2000070
    public sealed class <>c__55`2
    {
        // Fields
        public static readonly Newtonsoft.Json.Utilities.FSharpUtils.<>c__55<TKey,TValue> <>9;  // static @ 0x0
        public static System.Func<System.Collections.Generic.KeyValuePair<TKey,TValue>,System.Tuple<TKey,TValue>> <>9__55_1;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000336
        private static System.Void .cctor() { }
        // RVA: -1  // not resolved  token: 0x6000337
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000338
        private System.Tuple<TKey,TValue> <BuildMapCreator>b__55_1(System.Collections.Generic.KeyValuePair<TKey,TValue> kv) { }

    }

    // TypeToken: 0x2000071  // size: 0x20
    public sealed class <>c__DisplayClass52_0
    {
        // Fields
        public Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object> call;  // 0x10
        public Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object> invoke;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000339
        public System.Void .ctor() { }
        // RVA: 0x0A8BAC5C  token: 0x600033A
        private System.Object <CreateFSharpFuncCall>b__0(System.Object target, System.Object[] args) { }

    }

    // TypeToken: 0x2000072
    public sealed class <>c__DisplayClass55_0`2
    {
        // Fields
        public Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> ctorDelegate;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600033B
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x600033C
        private System.Object <BuildMapCreator>b__0(System.Object[] args) { }

    }

    // TypeToken: 0x2000074  // size: 0x28
    public class ImmutableCollectionTypeInfo
    {
        // Fields
        private System.String <ContractTypeName>k__BackingField;  // 0x10
        private System.String <CreatedTypeName>k__BackingField;  // 0x18
        private System.String <BuilderTypeName>k__BackingField;  // 0x20

        // Properties
        System.String ContractTypeName { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        System.String CreatedTypeName { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.String BuilderTypeName { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }

        // Methods
        // RVA: 0x031D14F0  token: 0x6000340
        public System.Void .ctor(System.String contractTypeName, System.String createdTypeName, System.String builderTypeName) { }

    }

    // TypeToken: 0x2000075  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.<>c <>9;  // static @ 0x0
        public static System.Func<System.Reflection.MethodInfo,System.Boolean> <>9__24_1;  // static @ 0x8
        public static System.Func<System.Reflection.MethodInfo,System.Boolean> <>9__25_1;  // static @ 0x10

        // Methods
        // RVA: 0x0A8BAD88  token: 0x6000347
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000348
        public System.Void .ctor() { }
        // RVA: 0x0A8BA8D8  token: 0x6000349
        private System.Boolean <TryBuildImmutableForArrayContract>b__24_1(System.Reflection.MethodInfo m) { }
        // RVA: 0x0A8BA94C  token: 0x600034A
        private System.Boolean <TryBuildImmutableForDictionaryContract>b__25_1(System.Reflection.MethodInfo m) { }

    }

    // TypeToken: 0x2000076  // size: 0x18
    public sealed class <>c__DisplayClass24_0
    {
        // Fields
        public System.String name;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600034B
        public System.Void .ctor() { }
        // RVA: 0x042E3CE0  token: 0x600034C
        private System.Boolean <TryBuildImmutableForArrayContract>b__0(Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.ImmutableCollectionTypeInfo d) { }

    }

    // TypeToken: 0x2000077  // size: 0x18
    public sealed class <>c__DisplayClass25_0
    {
        // Fields
        public System.String name;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600034D
        public System.Void .ctor() { }
        // RVA: 0x042E3CE0  token: 0x600034E
        private System.Boolean <TryBuildImmutableForDictionaryContract>b__0(Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.ImmutableCollectionTypeInfo d) { }

    }

    // TypeToken: 0x200007C  // size: 0x20
    public sealed class <>c__DisplayClass3_0
    {
        // Fields
        public System.Reflection.ConstructorInfo c;  // 0x10
        public System.Reflection.MethodBase method;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000367
        public System.Void .ctor() { }
        // RVA: 0x04295360  token: 0x6000368
        private System.Object <CreateParameterizedConstructor>b__0(System.Object[] a) { }
        // RVA: 0x0A8BAB5C  token: 0x6000369
        private System.Object <CreateParameterizedConstructor>b__1(System.Object[] a) { }

    }

    // TypeToken: 0x200007D
    public sealed class <>c__DisplayClass4_0`1
    {
        // Fields
        public System.Reflection.ConstructorInfo c;  // 0x0
        public System.Reflection.MethodBase method;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600036A
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x600036B
        private System.Object <CreateMethodCall>b__0(T o, System.Object[] a) { }
        // RVA: -1  // not resolved  token: 0x600036C
        private System.Object <CreateMethodCall>b__1(T o, System.Object[] a) { }

    }

    // TypeToken: 0x200007E
    public sealed class <>c__DisplayClass5_0`1
    {
        // Fields
        public System.Type type;  // 0x0
        public System.Reflection.ConstructorInfo constructorInfo;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600036D
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x600036E
        private T <CreateDefaultConstructor>b__0() { }
        // RVA: -1  // not resolved  token: 0x600036F
        private T <CreateDefaultConstructor>b__1() { }

    }

    // TypeToken: 0x200007F
    public sealed class <>c__DisplayClass6_0`1
    {
        // Fields
        public System.Reflection.PropertyInfo propertyInfo;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000370
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000371
        private System.Object <CreateGet>b__0(T o) { }

    }

    // TypeToken: 0x2000080
    public sealed class <>c__DisplayClass7_0`1
    {
        // Fields
        public System.Reflection.FieldInfo fieldInfo;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000372
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000373
        private System.Object <CreateGet>b__0(T o) { }

    }

    // TypeToken: 0x2000081
    public sealed class <>c__DisplayClass8_0`1
    {
        // Fields
        public System.Reflection.FieldInfo fieldInfo;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000374
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000375
        private System.Void <CreateSet>b__0(T o, System.Object v) { }

    }

    // TypeToken: 0x2000082
    public sealed class <>c__DisplayClass9_0`1
    {
        // Fields
        public System.Reflection.PropertyInfo propertyInfo;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000376
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x6000377
        private System.Void <CreateSet>b__0(T o, System.Object v) { }

    }

    // TypeToken: 0x2000089  // size: 0x18
    public sealed class <>c__DisplayClass11_0
    {
        // Fields
        public System.Func<System.Object> ctor;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600039E
        public System.Void .ctor() { }
        // RVA: 0x09ADFE08  token: 0x600039F
        private System.Object <Create>b__0(System.Object[] args) { }

    }

    // TypeToken: 0x200008A  // size: 0x18
    public sealed class <>c__DisplayClass11_1
    {
        // Fields
        public Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object> call;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60003A0
        public System.Void .ctor() { }
        // RVA: 0x0A8BAA50  token: 0x60003A1
        private System.Object <Create>b__1(System.Object target) { }

    }

    // TypeToken: 0x200008B  // size: 0x18
    public sealed class <>c__DisplayClass11_2
    {
        // Fields
        public Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object> call;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60003A2
        public System.Void .ctor() { }
        // RVA: 0x0A8BAAB8  token: 0x60003A3
        private System.Void <Create>b__2(System.Object target, System.Object arg) { }

    }

    // TypeToken: 0x200008D  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Newtonsoft.Json.Utilities.ReflectionUtils.<>c <>9;  // static @ 0x0
        public static System.Func<System.Reflection.ConstructorInfo,System.Boolean> <>9__11_0;  // static @ 0x8
        public static System.Func<System.Reflection.MemberInfo,System.String> <>9__31_0;  // static @ 0x10
        public static System.Func<System.Reflection.ParameterInfo,System.Type> <>9__39_0;  // static @ 0x18
        public static System.Func<System.Reflection.FieldInfo,System.Boolean> <>9__41_0;  // static @ 0x20

        // Methods
        // RVA: 0x04D4C210  token: 0x60003D2
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60003D3
        public System.Void .ctor() { }
        // RVA: 0x02F2EF10  token: 0x60003D4
        private System.Boolean <GetDefaultConstructor>b__11_0(System.Reflection.ConstructorInfo c) { }
        // RVA: 0x03F48CC0  token: 0x60003D5
        private System.String <GetFieldsAndProperties>b__31_0(System.Reflection.MemberInfo m) { }
        // RVA: 0x06C08908  token: 0x60003D6
        private System.Type <GetMemberInfoFromType>b__39_0(System.Reflection.ParameterInfo p) { }
        // RVA: 0x02F2E180  token: 0x60003D7
        private System.Boolean <GetChildPrivateFields>b__41_0(System.Reflection.FieldInfo f) { }

    }

    // TypeToken: 0x200008E  // size: 0x18
    public sealed class <>c__DisplayClass31_0
    {
        // Fields
        public System.Reflection.MemberInfo memberInfo;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60003D8
        public System.Void .ctor() { }
        // RVA: 0x04B87670  token: 0x60003D9
        private System.Boolean <GetFieldsAndProperties>b__1(System.Reflection.MemberInfo m) { }

    }

    // TypeToken: 0x200008F  // size: 0x18
    public sealed class <>c__DisplayClass44_0
    {
        // Fields
        public System.Reflection.PropertyInfo subTypeProperty;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60003DA
        public System.Void .ctor() { }
        // RVA: 0x048F6D60  token: 0x60003DB
        private System.Boolean <GetChildPrivateProperties>b__0(System.Reflection.PropertyInfo p) { }
        // RVA: 0x02F2F7C0  token: 0x60003DC
        private System.Boolean <GetChildPrivateProperties>b__1(System.Reflection.PropertyInfo p) { }

    }

    // TypeToken: 0x2000090  // size: 0x20
    public sealed class <>c__DisplayClass44_1
    {
        // Fields
        public System.Type subTypePropertyDeclaringType;  // 0x10
        public Newtonsoft.Json.Utilities.ReflectionUtils.<>c__DisplayClass44_0 CS$<>8__locals1;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60003DD
        public System.Void .ctor() { }
        // RVA: 0x02F2F690  token: 0x60003DE
        private System.Boolean <GetChildPrivateProperties>b__2(System.Reflection.PropertyInfo p) { }

    }

    // TypeToken: 0x2000091  // size: 0x20
    public sealed class <>c__DisplayClass45_0
    {
        // Fields
        public System.String method;  // 0x10
        public System.Type methodDeclaringType;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60003DF
        public System.Void .ctor() { }
        // RVA: 0x0A8BAB80  token: 0x60003E0
        private System.Boolean <IsMethodOverridden>b__0(System.Reflection.MethodInfo info) { }

    }

    // TypeToken: 0x2000096  // size: 0x14
    public sealed struct SeparatedCaseState
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.Utilities.StringUtils.SeparatedCaseState Start;  // const
        public static Newtonsoft.Json.Utilities.StringUtils.SeparatedCaseState Lower;  // const
        public static Newtonsoft.Json.Utilities.StringUtils.SeparatedCaseState Upper;  // const
        public static Newtonsoft.Json.Utilities.StringUtils.SeparatedCaseState NewWord;  // const

    }

    // TypeToken: 0x2000097
    public sealed class <>c__DisplayClass14_0`1
    {
        // Fields
        public System.Func<TSource,System.String> valueSelector;  // 0x0
        public System.String testValue;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600040A
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x600040B
        private System.Boolean <ForgivingCaseSensitiveFind>b__0(TSource s) { }
        // RVA: -1  // not resolved  token: 0x600040C
        private System.Boolean <ForgivingCaseSensitiveFind>b__1(TSource s) { }

    }

    // TypeToken: 0x20000A0
    public sealed class <GetEnumerator>d__2 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private System.Collections.Generic.KeyValuePair<System.Object,System.Object> <>2__current;  // 0x0
        public Newtonsoft.Json.Serialization.DefaultContractResolver.EnumerableDictionaryWrapper<TEnumeratorKey,TEnumeratorValue> <>4__this;  // 0x0
        private System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TEnumeratorKey,TEnumeratorValue>> <>7__wrap1;  // 0x0

        // Properties
        System.Collections.Generic.KeyValuePair<System.Object,System.Object> System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Object,System.Object>>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000469
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x600046A
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x600046B
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x600046C
        private System.Void <>m__Finally1() { }
        // RVA: -1  // not resolved  token: 0x600046E
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200009F
    public class EnumerableDictionaryWrapper`2 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TEnumeratorKey,TEnumeratorValue>> _e;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000466
        public System.Void .ctor(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TEnumeratorKey,TEnumeratorValue>> e) { }
        // RVA: -1  // not resolved  token: 0x6000467
        public virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Object,System.Object>> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000468
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x20000A1  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Newtonsoft.Json.Serialization.DefaultContractResolver.<>c <>9;  // static @ 0x0
        public static System.Func<System.Reflection.MemberInfo,System.Boolean> <>9__40_0;  // static @ 0x8
        public static System.Func<System.Reflection.MemberInfo,System.Boolean> <>9__40_1;  // static @ 0x10
        public static System.Func<System.Type,System.Collections.Generic.IEnumerable<System.Reflection.MemberInfo>> <>9__44_0;  // static @ 0x18
        public static System.Func<System.Reflection.MemberInfo,System.Boolean> <>9__44_1;  // static @ 0x20
        public static System.Func<System.Reflection.ConstructorInfo,System.Boolean> <>9__47_0;  // static @ 0x28
        public static System.Func<Newtonsoft.Json.Serialization.JsonProperty,System.Int32> <>9__75_0;  // static @ 0x30

        // Methods
        // RVA: 0x04D42010  token: 0x6000470
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000471
        public System.Void .ctor() { }
        // RVA: 0x02F2EF40  token: 0x6000472
        private System.Boolean <GetSerializableMembers>b__40_0(System.Reflection.MemberInfo m) { }
        // RVA: 0x0A8C1F0C  token: 0x6000473
        private System.Boolean <GetSerializableMembers>b__40_1(System.Reflection.MemberInfo m) { }
        // RVA: 0x035D0740  token: 0x6000474
        private System.Collections.Generic.IEnumerable<System.Reflection.MemberInfo> <GetExtensionDataMemberForType>b__44_0(System.Type baseType) { }
        // RVA: 0x02F2F8A0  token: 0x6000475
        private System.Boolean <GetExtensionDataMemberForType>b__44_1(System.Reflection.MemberInfo m) { }
        // RVA: 0x02F2E1D0  token: 0x6000476
        private System.Boolean <GetAttributeConstructor>b__47_0(System.Reflection.ConstructorInfo c) { }
        // RVA: 0x03FF4820  token: 0x6000477
        private System.Int32 <CreateProperties>b__75_0(Newtonsoft.Json.Serialization.JsonProperty p) { }

    }

    // TypeToken: 0x20000A2  // size: 0x18
    public sealed class <>c__DisplayClass42_0
    {
        // Fields
        public Newtonsoft.Json.Serialization.NamingStrategy namingStrategy;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000478
        public System.Void .ctor() { }
        // RVA: 0x0A8C1F88  token: 0x6000479
        private System.String <CreateObjectContract>b__0(System.String s) { }

    }

    // TypeToken: 0x20000A3  // size: 0x20
    public sealed class <>c__DisplayClass45_0
    {
        // Fields
        public System.Func<System.Object,System.Object> getExtensionDataDictionary;  // 0x10
        public System.Reflection.MemberInfo member;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x600047A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000A4  // size: 0x30
    public sealed class <>c__DisplayClass45_1
    {
        // Fields
        public System.Action<System.Object,System.Object> setExtensionDataDictionary;  // 0x10
        public System.Func<System.Object> createExtensionDataDictionary;  // 0x18
        public Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object> setExtensionDataDictionaryValue;  // 0x20
        public Newtonsoft.Json.Serialization.DefaultContractResolver.<>c__DisplayClass45_0 CS$<>8__locals1;  // 0x28

        // Methods
        // RVA: 0x041E1670  token: 0x600047B
        public System.Void .ctor() { }
        // RVA: 0x0A8C1FA8  token: 0x600047C
        private System.Void <SetExtensionDataDelegates>b__0(System.Object o, System.String key, System.Object value) { }

    }

    // TypeToken: 0x20000A5  // size: 0x20
    public sealed class <>c__DisplayClass45_2
    {
        // Fields
        public Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> createEnumerableWrapper;  // 0x10
        public Newtonsoft.Json.Serialization.DefaultContractResolver.<>c__DisplayClass45_0 CS$<>8__locals2;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x600047D
        public System.Void .ctor() { }
        // RVA: 0x0A8C2184  token: 0x600047E
        private System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Object,System.Object>> <SetExtensionDataDelegates>b__1(System.Object o) { }

    }

    // TypeToken: 0x20000A6  // size: 0x18
    public sealed class <>c__DisplayClass62_0
    {
        // Fields
        public Newtonsoft.Json.Serialization.NamingStrategy namingStrategy;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600047F
        public System.Void .ctor() { }
        // RVA: 0x0A8C1F88  token: 0x6000480
        private System.String <CreateDictionaryContract>b__0(System.String s) { }

    }

    // TypeToken: 0x20000A7  // size: 0x18
    public sealed class <>c__DisplayClass67_0
    {
        // Fields
        public Newtonsoft.Json.Serialization.NamingStrategy namingStrategy;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000481
        public System.Void .ctor() { }
        // RVA: 0x0A8C1F88  token: 0x6000482
        private System.String <CreateDynamicContract>b__0(System.String s) { }

    }

    // TypeToken: 0x20000A8  // size: 0x18
    public sealed class <>c__DisplayClass80_0
    {
        // Fields
        public Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object> shouldSerializeCall;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000483
        public System.Void .ctor() { }
        // RVA: 0x0A8C2350  token: 0x6000484
        private System.Boolean <CreateShouldSerializeTest>b__0(System.Object o) { }

    }

    // TypeToken: 0x20000A9  // size: 0x18
    public sealed class <>c__DisplayClass81_0
    {
        // Fields
        public System.Func<System.Object,System.Object> specifiedPropertyGet;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000485
        public System.Void .ctor() { }
        // RVA: 0x0A8C23D4  token: 0x6000486
        private System.Boolean <SetIsSpecifiedActions>b__0(System.Object o) { }

    }

    // TypeToken: 0x20000BD  // size: 0x18
    public sealed class <>c__DisplayClass57_0
    {
        // Fields
        public System.Reflection.MethodInfo callbackMethodInfo;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60004E5
        public System.Void .ctor() { }
        // RVA: 0x03F4D850  token: 0x60004E6
        private System.Void <CreateSerializationCallback>b__0(System.Object o, System.Runtime.Serialization.StreamingContext context) { }

    }

    // TypeToken: 0x20000BE  // size: 0x18
    public sealed class <>c__DisplayClass58_0
    {
        // Fields
        public System.Reflection.MethodInfo callbackMethodInfo;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60004E7
        public System.Void .ctor() { }
        // RVA: 0x0A8C225C  token: 0x60004E8
        private System.Void <CreateSerializationErrorCallback>b__0(System.Object o, System.Runtime.Serialization.StreamingContext context, Newtonsoft.Json.Serialization.ErrorContext econtext) { }

    }

    // TypeToken: 0x20000C9  // size: 0x10
    public class ReferenceEqualsEqualityComparer : System.Collections.Generic.IEqualityComparer`1
    {
        // Methods
        // RVA: 0x0829C278  token: 0x6000570
        private virtual System.Boolean System.Collections.Generic.IEqualityComparer<System.Object>.Equals(System.Object x, System.Object y) { }
        // RVA: 0x04DA3AC0  token: 0x6000571
        private virtual System.Int32 System.Collections.Generic.IEqualityComparer<System.Object>.GetHashCode(System.Object obj) { }
        // RVA: 0x041E1670  token: 0x6000572
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000CB  // size: 0x14
    public sealed struct PropertyPresence
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence None;  // const
        public static Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence Null;  // const
        public static Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence Value;  // const

    }

    // TypeToken: 0x20000CC  // size: 0x40
    public class CreatorPropertyContext
    {
        // Fields
        public readonly System.String Name;  // 0x10
        public Newtonsoft.Json.Serialization.JsonProperty Property;  // 0x18
        public Newtonsoft.Json.Serialization.JsonProperty ConstructorProperty;  // 0x20
        public System.Nullable<Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence> Presence;  // 0x28
        public System.Object Value;  // 0x30
        public System.Boolean Used;  // 0x38

        // Methods
        // RVA: 0x053908C0  token: 0x60005A3
        public System.Void .ctor(System.String name) { }

    }

    // TypeToken: 0x20000CD  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Newtonsoft.Json.Serialization.JsonSerializerInternalReader.<>c <>9;  // static @ 0x0
        public static System.Func<Newtonsoft.Json.Serialization.JsonProperty,System.String> <>9__38_0;  // static @ 0x8
        public static System.Func<Newtonsoft.Json.Serialization.JsonProperty,System.String> <>9__38_2;  // static @ 0x10
        public static System.Func<Newtonsoft.Json.Serialization.JsonProperty,Newtonsoft.Json.Serialization.JsonProperty> <>9__42_0;  // static @ 0x18
        public static System.Func<Newtonsoft.Json.Serialization.JsonProperty,Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence> <>9__42_1;  // static @ 0x20

        // Methods
        // RVA: 0x04D3F730  token: 0x60005A4
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60005A5
        public System.Void .ctor() { }
        // RVA: 0x0A8C1EF4  token: 0x60005A6
        private System.String <CreateObjectUsingCreatorWithParameters>b__38_0(Newtonsoft.Json.Serialization.JsonProperty p) { }
        // RVA: 0x045B2270  token: 0x60005A7
        private System.String <CreateObjectUsingCreatorWithParameters>b__38_2(Newtonsoft.Json.Serialization.JsonProperty p) { }
        // RVA: 0x04D88C30  token: 0x60005A8
        private Newtonsoft.Json.Serialization.JsonProperty <PopulateObject>b__42_0(Newtonsoft.Json.Serialization.JsonProperty m) { }
        // RVA: 0x011EC580  token: 0x60005A9
        private Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence <PopulateObject>b__42_1(Newtonsoft.Json.Serialization.JsonProperty m) { }

    }

    // TypeToken: 0x20000CE  // size: 0x18
    public sealed class <>c__DisplayClass38_0
    {
        // Fields
        public Newtonsoft.Json.Serialization.JsonProperty property;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60005AA
        public System.Void .ctor() { }
        // RVA: 0x0A8C1F68  token: 0x60005AB
        private System.Boolean <CreateObjectUsingCreatorWithParameters>b__1(Newtonsoft.Json.Serialization.JsonSerializerInternalReader.CreatorPropertyContext p) { }

    }

    // TypeToken: 0x20000D3  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Newtonsoft.Json.Serialization.JsonTypeReflector.<>c <>9;  // static @ 0x0
        public static System.Func<System.Object,System.Type> <>9__22_1;  // static @ 0x8

        // Methods
        // RVA: 0x0A8D2AFC  token: 0x600060D
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600060E
        public System.Void .ctor() { }
        // RVA: 0x0A8D2A0C  token: 0x600060F
        private System.Type <GetCreator>b__22_1(System.Object param) { }

    }

    // TypeToken: 0x20000D4  // size: 0x20
    public sealed class <>c__DisplayClass22_0
    {
        // Fields
        public System.Type type;  // 0x10
        public System.Func<System.Object> defaultConstructor;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000610
        public System.Void .ctor() { }
        // RVA: 0x049B8240  token: 0x6000611
        private System.Object <GetCreator>b__0(System.Object[] parameters) { }

    }

    // TypeToken: 0x20000E7  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Newtonsoft.Json.Linq.JObject.JObjectDynamicProxy.<>c <>9;  // static @ 0x0
        public static System.Func<Newtonsoft.Json.Linq.JProperty,System.String> <>9__2_0;  // static @ 0x8

        // Methods
        // RVA: 0x0A8D2A98  token: 0x6000715
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000716
        public System.Void .ctor() { }
        // RVA: 0x0A8D2A80  token: 0x6000717
        private System.String <GetDynamicMemberNames>b__2_0(Newtonsoft.Json.Linq.JProperty p) { }

    }

    // TypeToken: 0x20000E6  // size: 0x10
    public class JObjectDynamicProxy : Newtonsoft.Json.Utilities.DynamicProxy`1
    {
        // Methods
        // RVA: 0x0A8C4C50  token: 0x6000711
        public virtual System.Boolean TryGetMember(Newtonsoft.Json.Linq.JObject instance, System.Dynamic.GetMemberBinder binder, System.Object& result) { }
        // RVA: 0x0A8C4C90  token: 0x6000712
        public virtual System.Boolean TrySetMember(Newtonsoft.Json.Linq.JObject instance, System.Dynamic.SetMemberBinder binder, System.Object value) { }
        // RVA: 0x0A8C4B30  token: 0x6000713
        public virtual System.Collections.Generic.IEnumerable<System.String> GetDynamicMemberNames(Newtonsoft.Json.Linq.JObject instance) { }
        // RVA: 0x0A8C4D48  token: 0x6000714
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000E8  // size: 0x38
    public sealed class <GetEnumerator>d__64 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private System.Collections.Generic.KeyValuePair<System.String,Newtonsoft.Json.Linq.JToken> <>2__current;  // 0x18
        public Newtonsoft.Json.Linq.JObject <>4__this;  // 0x28
        private System.Collections.Generic.IEnumerator<Newtonsoft.Json.Linq.JToken> <>7__wrap1;  // 0x30

        // Properties
        System.Collections.Generic.KeyValuePair<System.String,Newtonsoft.Json.Linq.JToken> System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,Newtonsoft.Json.Linq.JToken>>.Current { get; /* RVA: 0x04D890C0 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0A8D2904 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000718
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x0A8D294C  token: 0x6000719
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0A8D259C  token: 0x600071A
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0A8D29C4  token: 0x600071B
        private System.Void <>m__Finally1() { }
        // RVA: 0x0A8D28B8  token: 0x600071D
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000EB  // size: 0x28
    public sealed class <GetEnumerator>d__1 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private Newtonsoft.Json.Linq.JToken <>2__current;  // 0x18
        public Newtonsoft.Json.Linq.JProperty.JPropertyList <>4__this;  // 0x20

        // Properties
        Newtonsoft.Json.Linq.JToken System.Collections.Generic.IEnumerator<Newtonsoft.Json.Linq.JToken>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x014F51F0  token: 0x6000741
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x6000742
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0A8D24F8  token: 0x6000743
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0A8D2550  token: 0x6000745
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x20000EA  // size: 0x18
    public class JPropertyList : System.Collections.Generic.IList`1, System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private Newtonsoft.Json.Linq.JToken _token;  // 0x10

        // Properties
        System.Int32 Count { get; /* RVA: 0x040C1F50 */ }
        System.Boolean IsReadOnly { get; /* RVA: 0x012081B0 */ }
        Newtonsoft.Json.Linq.JToken Item { get; /* RVA: 0x04B48FB0 */ set; /* RVA: 0x04D83150 */ }

        // Methods
        // RVA: 0x0A8C61BC  token: 0x6000732
        public virtual System.Collections.Generic.IEnumerator<Newtonsoft.Json.Linq.JToken> GetEnumerator() { }
        // RVA: 0x0A8C6268  token: 0x6000733
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: 0x053908C0  token: 0x6000734
        public virtual System.Void Add(Newtonsoft.Json.Linq.JToken item) { }
        // RVA: 0x040E20A0  token: 0x6000735
        public virtual System.Void Clear() { }
        // RVA: 0x0A8C614C  token: 0x6000736
        public virtual System.Boolean Contains(Newtonsoft.Json.Linq.JToken item) { }
        // RVA: 0x0A8C6154  token: 0x6000737
        public virtual System.Void CopyTo(Newtonsoft.Json.Linq.JToken[] array, System.Int32 arrayIndex) { }
        // RVA: 0x0A8C6240  token: 0x6000738
        public virtual System.Boolean Remove(Newtonsoft.Json.Linq.JToken item) { }
        // RVA: 0x0A8C6218  token: 0x600073B
        public virtual System.Int32 IndexOf(Newtonsoft.Json.Linq.JToken item) { }
        // RVA: 0x0424C7C0  token: 0x600073C
        public virtual System.Void Insert(System.Int32 index, Newtonsoft.Json.Linq.JToken item) { }
        // RVA: 0x0A8C6228  token: 0x600073D
        public virtual System.Void RemoveAt(System.Int32 index) { }
        // RVA: 0x041E1670  token: 0x6000740
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000F2  // size: 0x18
    public class LineInfoAnnotation
    {
        // Fields
        private readonly System.Int32 LineNumber;  // 0x10
        private readonly System.Int32 LinePosition;  // 0x14

        // Methods
        // RVA: 0x04D88290  token: 0x60007C5
        public System.Void .ctor(System.Int32 lineNumber, System.Int32 linePosition) { }

    }

    // TypeToken: 0x20000F7  // size: 0x10
    public class JValueDynamicProxy : Newtonsoft.Json.Utilities.DynamicProxy`1
    {
        // Methods
        // RVA: 0x0A8CD574  token: 0x6000831
        public virtual System.Boolean TryConvert(Newtonsoft.Json.Linq.JValue instance, System.Dynamic.ConvertBinder binder, System.Object& result) { }
        // RVA: 0x0A8CD2C8  token: 0x6000832
        public virtual System.Boolean TryBinaryOperation(Newtonsoft.Json.Linq.JValue instance, System.Dynamic.BinaryOperationBinder binder, System.Object arg, System.Object& result) { }
        // RVA: 0x0A8CD6F0  token: 0x6000833
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000100  // size: 0x20
    public class Union
    {
        // Fields
        public readonly Newtonsoft.Json.Utilities.FSharpFunction TagReader;  // 0x10
        public readonly System.Collections.Generic.List<Newtonsoft.Json.Converters.DiscriminatedUnionConverter.UnionCase> Cases;  // 0x18

        // Methods
        // RVA: 0x032AF420  token: 0x6000858
        public System.Void .ctor(Newtonsoft.Json.Utilities.FSharpFunction tagReader, System.Collections.Generic.List<Newtonsoft.Json.Converters.DiscriminatedUnionConverter.UnionCase> cases) { }

    }

    // TypeToken: 0x2000101  // size: 0x38
    public class UnionCase
    {
        // Fields
        public readonly System.Int32 Tag;  // 0x10
        public readonly System.String Name;  // 0x18
        public readonly System.Reflection.PropertyInfo[] Fields;  // 0x20
        public readonly Newtonsoft.Json.Utilities.FSharpFunction FieldReader;  // 0x28
        public readonly Newtonsoft.Json.Utilities.FSharpFunction Constructor;  // 0x30

        // Methods
        // RVA: 0x0A8D912C  token: 0x6000859
        public System.Void .ctor(System.Int32 tag, System.String name, System.Reflection.PropertyInfo[] fields, Newtonsoft.Json.Utilities.FSharpFunction fieldReader, Newtonsoft.Json.Utilities.FSharpFunction constructor) { }

    }

    // TypeToken: 0x2000102  // size: 0x18
    public sealed class <>c__DisplayClass8_0
    {
        // Fields
        public System.Int32 tag;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600085A
        public System.Void .ctor() { }
        // RVA: 0x06A829FC  token: 0x600085B
        private System.Boolean <WriteJson>b__0(Newtonsoft.Json.Converters.DiscriminatedUnionConverter.UnionCase c) { }

    }

    // TypeToken: 0x2000103  // size: 0x20
    public sealed class <>c__DisplayClass9_0
    {
        // Fields
        public System.String caseName;  // 0x10
        public System.Func<Newtonsoft.Json.Converters.DiscriminatedUnionConverter.UnionCase,System.Boolean> <>9__0;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x600085C
        public System.Void .ctor() { }
        // RVA: 0x0A8D9104  token: 0x600085D
        private System.Boolean <ReadJson>b__0(Newtonsoft.Json.Converters.DiscriminatedUnionConverter.UnionCase c) { }

    }

    // TypeToken: 0x200012D  // size: 0x1A
    public sealed struct __StaticArrayInitTypeSize=10
    {
    }

    // TypeToken: 0x200012E  // size: 0x20
    public sealed struct __StaticArrayInitTypeSize=16
    {
    }

    // TypeToken: 0x200012F  // size: 0x24
    public sealed struct __StaticArrayInitTypeSize=20
    {
    }

    // TypeToken: 0x2000130  // size: 0x28
    public sealed struct __StaticArrayInitTypeSize=24
    {
    }

    // TypeToken: 0x2000131  // size: 0x2C
    public sealed struct __StaticArrayInitTypeSize=28
    {
    }

    // TypeToken: 0x2000132  // size: 0x34
    public sealed struct __StaticArrayInitTypeSize=36
    {
    }

    // TypeToken: 0x2000133  // size: 0x38
    public sealed struct __StaticArrayInitTypeSize=40
    {
    }

    // TypeToken: 0x2000134  // size: 0x3C
    public sealed struct __StaticArrayInitTypeSize=44
    {
    }

    // TypeToken: 0x2000135  // size: 0x44
    public sealed struct __StaticArrayInitTypeSize=52
    {
    }

    // TypeToken: 0x200012C  // size: 0x10
    public sealed class <PrivateImplementationDetails>
    {
        // Fields
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=44 0698228BF899CAEAB9A53E5E6C7099E846C44F56432050D234DDF03AD772F139;  // static @ 0x0
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=10 19AE20A57B073E3E8DD45C6F6A4E9AB1076EA3EBFFF28E4AEB58B411472CF994;  // static @ 0x2c
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=40 223D6CA32241C349E421A0164F2341E20CC5B65D5A04AA021CFF71D623895570;  // static @ 0x36
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=20 33350F5DA385CE1B8749AEC68BA060CD54EE981968522B5EDF62178537A1FEEE;  // static @ 0x5e
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=40 38809B9974198671140931F729415F3FD75DF68A6398E3486AE3B58554329A63;  // static @ 0x72
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=28 499E4F5C84E20C7347E10100E0EC90C1945EA21C7C80809E4F7F474179B39DF6;  // static @ 0x9a
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=16 4EDE3546F1189E450DF4D4A2739BE90BEB3B1708B3B9F406B02E0773A92A10FF;  // static @ 0xb6
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=52 5857EE4CE98BFABBD62B385C1098507DD0052FF3951043AAD6A1DABD495F18AA;  // static @ 0xc6
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=28 5ADB7CA81690556AB2A3201A849839FA3562604BB469382C7D6D78AB426283E2;  // static @ 0xfa
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=24 5DDF815AC046E7D4603FA586D1BDE42118AD4FE9875D64F716BC7D2740EE52C9;  // static @ 0x116
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=16 605A3F93AE7A97E00C156F977E942027EA532E263A5B440A4219984F803FDD04;  // static @ 0x12e
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=36 7367A65185E4F747AA29364AB199D01646A010A62129A6BA2E35E929D7294D62;  // static @ 0x13e
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=40 7439A4C9E30AC42BCC55AD1A2B617E29E7129B6DDAC79C886944B17819262CC1;  // static @ 0x162
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=40 990F3F1286CC3928725497B2745CFF7BC7C9803B4EB8271611540BA6BF6654B5;  // static @ 0x18a
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=16 A8636D08B42D058EFC34703DD37B6468FCE56138DF242B862C3F1CA138CB3B89;  // static @ 0x1b2
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=20 B1D1BCD1D06B4A563944BE3C67D51F63DF23702E5BE760D7897C6AD1F51C6122;  // static @ 0x1c2
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=20 CAA07D7573596B3356BD202533F0EAFDD05309981F270193A99E300D57587326;  // static @ 0x1d6
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=40 D4B3B8EBA0589FC38724A0D318B46104B07BC528744109ED69ED71604B7EEC1A;  // static @ 0x1ea
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=40 F6EDC1733B068F457C63E03BB041B9AB6BFAD5CD7673D3E0841968D3FBCB12C7;  // static @ 0x212
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=52 FADB218011E7702BB9575D0C32A685DA10B5C72EB809BD9A955DB1C76E4D8315;  // static @ 0x23a
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=40 FCA56C548368F7065472C8C8EE4D63921B4F16BB51181EC202A0C252D5209E6A;  // static @ 0x26e

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

namespace Newtonsoft.Json
{

    // TypeToken: 0x2000008  // size: 0x14
    public sealed struct ConstructorHandling
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.ConstructorHandling Default;  // const
        public static Newtonsoft.Json.ConstructorHandling AllowNonPublicDefaultConstructor;  // const

    }

    // TypeToken: 0x2000009  // size: 0x14
    public sealed struct DateFormatHandling
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.DateFormatHandling IsoDateFormat;  // const
        public static Newtonsoft.Json.DateFormatHandling MicrosoftDateFormat;  // const

    }

    // TypeToken: 0x200000A  // size: 0x14
    public sealed struct DateParseHandling
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.DateParseHandling None;  // const
        public static Newtonsoft.Json.DateParseHandling DateTime;  // const
        public static Newtonsoft.Json.DateParseHandling DateTimeOffset;  // const

    }

    // TypeToken: 0x200000B  // size: 0x14
    public sealed struct DateTimeZoneHandling
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.DateTimeZoneHandling Local;  // const
        public static Newtonsoft.Json.DateTimeZoneHandling Utc;  // const
        public static Newtonsoft.Json.DateTimeZoneHandling Unspecified;  // const
        public static Newtonsoft.Json.DateTimeZoneHandling RoundtripKind;  // const

    }

    // TypeToken: 0x200000C  // size: 0x28
    public class DefaultJsonNameTable : Newtonsoft.Json.JsonNameTable
    {
        // Fields
        private static readonly System.Int32 HashCodeRandomizer;  // static @ 0x0
        private System.Int32 _count;  // 0x10
        private Newtonsoft.Json.DefaultJsonNameTable.Entry[] _entries;  // 0x18
        private System.Int32 _mask;  // 0x20

        // Methods
        // RVA: 0x04D75A00  token: 0x6000008
        private static System.Void .cctor() { }
        // RVA: 0x0464D570  token: 0x6000009
        public System.Void .ctor() { }
        // RVA: 0x02F06660  token: 0x600000A
        public virtual System.String Get(System.Char[] key, System.Int32 start, System.Int32 length) { }
        // RVA: 0x032B3E00  token: 0x600000B
        public System.String Add(System.String key) { }
        // RVA: 0x032B2C60  token: 0x600000C
        private System.String AddEntry(System.String str, System.Int32 hashCode) { }
        // RVA: 0x032B3210  token: 0x600000D
        private System.Void Grow() { }
        // RVA: 0x02F065E0  token: 0x600000E
        private static System.Boolean TextEquals(System.String str1, System.Char[] str2, System.Int32 str2Start, System.Int32 str2Length) { }

    }

    // TypeToken: 0x200000E  // size: 0x14
    public sealed struct DefaultValueHandling
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.DefaultValueHandling Include;  // const
        public static Newtonsoft.Json.DefaultValueHandling Ignore;  // const
        public static Newtonsoft.Json.DefaultValueHandling Populate;  // const
        public static Newtonsoft.Json.DefaultValueHandling IgnoreAndPopulate;  // const

    }

    // TypeToken: 0x200000F  // size: 0x14
    public sealed struct FloatFormatHandling
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.FloatFormatHandling String;  // const
        public static Newtonsoft.Json.FloatFormatHandling Symbol;  // const
        public static Newtonsoft.Json.FloatFormatHandling DefaultValue;  // const

    }

    // TypeToken: 0x2000010  // size: 0x14
    public sealed struct FloatParseHandling
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.FloatParseHandling Double;  // const
        public static Newtonsoft.Json.FloatParseHandling Decimal;  // const

    }

    // TypeToken: 0x2000011  // size: 0x14
    public sealed struct Formatting
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.Formatting None;  // const
        public static Newtonsoft.Json.Formatting Indented;  // const

    }

    // TypeToken: 0x2000012
    public interface IArrayPool`1
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000010
        public virtual T[] Rent(System.Int32 minimumLength) { }
        // RVA: -1  // abstract  token: 0x6000011
        public virtual System.Void Return(T[] array) { }

    }

    // TypeToken: 0x2000013
    public interface IJsonLineInfo
    {
        // Properties
        System.Int32 LineNumber { get; /* RVA: -1  // abstract */ }
        System.Int32 LinePosition { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000012
        public virtual System.Boolean HasLineInfo() { }

    }

    // TypeToken: 0x2000014  // size: 0x50
    public sealed class JsonArrayAttribute : Newtonsoft.Json.JsonContainerAttribute
    {
    }

    // TypeToken: 0x2000015  // size: 0x10
    public sealed class JsonConstructorAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000015
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000016  // size: 0x50
    public abstract class JsonContainerAttribute : System.Attribute
    {
        // Fields
        private System.Type <ItemConverterType>k__BackingField;  // 0x10
        private System.Object[] <ItemConverterParameters>k__BackingField;  // 0x18
        private Newtonsoft.Json.Serialization.NamingStrategy <NamingStrategyInstance>k__BackingField;  // 0x20
        private System.Nullable<System.Boolean> _isReference;  // 0x28
        private System.Nullable<System.Boolean> _itemIsReference;  // 0x2a
        private System.Nullable<Newtonsoft.Json.ReferenceLoopHandling> _itemReferenceLoopHandling;  // 0x2c
        private System.Nullable<Newtonsoft.Json.TypeNameHandling> _itemTypeNameHandling;  // 0x34
        private System.Type _namingStrategyType;  // 0x40
        private System.Object[] _namingStrategyParameters;  // 0x48

        // Properties
        System.Type ItemConverterType { get; /* RVA: 0x02B2ECC0 */ }
        System.Object[] ItemConverterParameters { get; /* RVA: 0x0385B100 */ }
        System.Type NamingStrategyType { get; /* RVA: 0x04D85A60 */ }
        System.Object[] NamingStrategyParameters { get; /* RVA: 0x04D86200 */ }
        Newtonsoft.Json.Serialization.NamingStrategy NamingStrategyInstance { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }

    }

    // TypeToken: 0x2000017  // size: 0x10
    public static class JsonConvert
    {
        // Fields
        private static System.Func<Newtonsoft.Json.JsonSerializerSettings> <DefaultSettings>k__BackingField;  // static @ 0x0
        public static readonly System.String True;  // static @ 0x8
        public static readonly System.String False;  // static @ 0x10
        public static readonly System.String Null;  // static @ 0x18
        public static readonly System.String Undefined;  // static @ 0x20
        public static readonly System.String PositiveInfinity;  // static @ 0x28
        public static readonly System.String NegativeInfinity;  // static @ 0x30
        public static readonly System.String NaN;  // static @ 0x38

        // Properties
        System.Func<Newtonsoft.Json.JsonSerializerSettings> DefaultSettings { get; /* RVA: 0x0A8A6DEC */ }

        // Methods
        // RVA: 0x03778C50  token: 0x600001D
        public static System.String ToString(System.Boolean value) { }
        // RVA: 0x0A8A6A78  token: 0x600001E
        public static System.String ToString(System.Char value) { }
        // RVA: 0x0A8A6C60  token: 0x600001F
        private static System.String ToString(System.Single value, Newtonsoft.Json.FloatFormatHandling floatFormatHandling, System.Char quoteChar, System.Boolean nullable) { }
        // RVA: 0x0A8A6658  token: 0x6000020
        private static System.String EnsureFloatFormat(System.Double value, System.String text, Newtonsoft.Json.FloatFormatHandling floatFormatHandling, System.Char quoteChar, System.Boolean nullable) { }
        // RVA: 0x0A8A6D10  token: 0x6000021
        private static System.String ToString(System.Double value, Newtonsoft.Json.FloatFormatHandling floatFormatHandling, System.Char quoteChar, System.Boolean nullable) { }
        // RVA: 0x0A8A6580  token: 0x6000022
        private static System.String EnsureDecimalPlace(System.Double value, System.String text) { }
        // RVA: 0x0A8A65FC  token: 0x6000023
        private static System.String EnsureDecimalPlace(System.String text) { }
        // RVA: 0x0A8A69E0  token: 0x6000024
        public static System.String ToString(System.Decimal value) { }
        // RVA: 0x0A8A6C10  token: 0x6000025
        public static System.String ToString(System.String value) { }
        // RVA: 0x0A8A6BBC  token: 0x6000026
        public static System.String ToString(System.String value, System.Char delimiter) { }
        // RVA: 0x0A8A6AE0  token: 0x6000027
        public static System.String ToString(System.String value, System.Char delimiter, Newtonsoft.Json.StringEscapeHandling stringEscapeHandling) { }
        // RVA: 0x03423A10  token: 0x6000028
        public static System.String SerializeObject(System.Object value) { }
        // RVA: 0x0A8A6938  token: 0x6000029
        public static System.String SerializeObject(System.Object value, Newtonsoft.Json.Formatting formatting) { }
        // RVA: 0x0A8A698C  token: 0x600002A
        public static System.String SerializeObject(System.Object value, Newtonsoft.Json.JsonSerializerSettings settings) { }
        // RVA: 0x03423A70  token: 0x600002B
        public static System.String SerializeObject(System.Object value, System.Type type, Newtonsoft.Json.JsonSerializerSettings settings) { }
        // RVA: 0x035A6320  token: 0x600002C
        public static System.String SerializeObject(System.Object value, Newtonsoft.Json.Formatting formatting, Newtonsoft.Json.JsonSerializerSettings settings) { }
        // RVA: 0x035A63A0  token: 0x600002D
        public static System.String SerializeObject(System.Object value, System.Type type, Newtonsoft.Json.Formatting formatting, Newtonsoft.Json.JsonSerializerSettings settings) { }
        // RVA: 0x03425340  token: 0x600002E
        private static System.String SerializeObjectInternal(System.Object value, System.Type type, Newtonsoft.Json.JsonSerializer jsonSerializer) { }
        // RVA: 0x04CC4980  token: 0x600002F
        public static System.Object DeserializeObject(System.String value) { }
        // RVA: 0x035A6000  token: 0x6000030
        public static System.Object DeserializeObject(System.String value, System.Type type) { }
        // RVA: -1  // generic def  token: 0x6000031
        public static T DeserializeObject(System.String value) { }
        // RVA: -1  // generic def  token: 0x6000032
        public static T DeserializeObject(System.String value, Newtonsoft.Json.JsonSerializerSettings settings) { }
        // RVA: 0x035A6700  token: 0x6000033
        public static System.Object DeserializeObject(System.String value, System.Type type, Newtonsoft.Json.JsonSerializerSettings settings) { }
        // RVA: 0x0A8A6718  token: 0x6000034
        public static System.Void PopulateObject(System.String value, System.Object target, Newtonsoft.Json.JsonSerializerSettings settings) { }
        // RVA: 0x04A282D0  token: 0x6000035
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000018  // size: 0x10
    public abstract class JsonConverter
    {
        // Properties
        System.Boolean CanRead { get; /* RVA: 0x02FFF600 */ }
        System.Boolean CanWrite { get; /* RVA: 0x02FFF600 */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000036
        public virtual System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: -1  // abstract  token: 0x6000037
        public virtual System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: -1  // abstract  token: 0x6000038
        public virtual System.Boolean CanConvert(System.Type objectType) { }
        // RVA: 0x041E1670  token: 0x600003B
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000019
    public abstract class JsonConverter`1 : Newtonsoft.Json.JsonConverter
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x600003C
        public virtual System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: -1  // abstract  token: 0x600003D
        public virtual System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, T value, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: -1  // not resolved  token: 0x600003E
        public virtual System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: -1  // abstract  token: 0x600003F
        public virtual T ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, T existingValue, System.Boolean hasExistingValue, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: -1  // not resolved  token: 0x6000040
        public virtual System.Boolean CanConvert(System.Type objectType) { }
        // RVA: -1  // not resolved  token: 0x6000041
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200001A  // size: 0x20
    public sealed class JsonConverterAttribute : System.Attribute
    {
        // Fields
        private readonly System.Type _converterType;  // 0x10
        private readonly System.Object[] <ConverterParameters>k__BackingField;  // 0x18

        // Properties
        System.Type ConverterType { get; /* RVA: 0x02B2ECC0 */ }
        System.Object[] ConverterParameters { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x04B0E520  token: 0x6000044
        public System.Void .ctor(System.Type converterType) { }

    }

    // TypeToken: 0x200001B  // size: 0x18
    public class JsonConverterCollection : System.Collections.ObjectModel.Collection`1
    {
        // Methods
        // RVA: 0x0539649C  token: 0x6000045
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200001C  // size: 0x50
    public sealed class JsonDictionaryAttribute : Newtonsoft.Json.JsonContainerAttribute
    {
    }

    // TypeToken: 0x200001D  // size: 0x90
    public class JsonException : System.Exception
    {
        // Methods
        // RVA: 0x0A8A6E3C  token: 0x6000046
        public System.Void .ctor() { }
        // RVA: 0x0A8A6E88  token: 0x6000047
        public System.Void .ctor(System.String message) { }
        // RVA: 0x0A8A6EE4  token: 0x6000048
        public System.Void .ctor(System.String message, System.Exception innerException) { }
        // RVA: 0x0A8A6F44  token: 0x6000049
        public System.Void .ctor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }

    }

    // TypeToken: 0x200001E  // size: 0x18
    public class JsonExtensionDataAttribute : System.Attribute
    {
        // Fields
        private System.Boolean <WriteData>k__BackingField;  // 0x10
        private System.Boolean <ReadData>k__BackingField;  // 0x11

        // Properties
        System.Boolean WriteData { get; /* RVA: 0x015EFCE0 */ }
        System.Boolean ReadData { get; /* RVA: 0x04D88380 */ }

    }

    // TypeToken: 0x200001F  // size: 0x10
    public sealed class JsonIgnoreAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x600004C
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000020  // size: 0x10
    public abstract class JsonNameTable
    {
        // Methods
        // RVA: -1  // abstract  token: 0x600004D
        public virtual System.String Get(System.Char[] key, System.Int32 start, System.Int32 length) { }
        // RVA: 0x041E1670  token: 0x600004E
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000021  // size: 0x70
    public sealed class JsonObjectAttribute : Newtonsoft.Json.JsonContainerAttribute
    {
        // Fields
        private Newtonsoft.Json.MemberSerialization _memberSerialization;  // 0x50
        private System.Nullable<Newtonsoft.Json.MissingMemberHandling> _missingMemberHandling;  // 0x54
        private System.Nullable<Newtonsoft.Json.Required> _itemRequired;  // 0x5c
        private System.Nullable<Newtonsoft.Json.NullValueHandling> _itemNullValueHandling;  // 0x64

        // Properties
        Newtonsoft.Json.MemberSerialization MemberSerialization { get; /* RVA: 0x04D86550 */ }

    }

    // TypeToken: 0x2000022  // size: 0x14
    public sealed struct JsonContainerType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.JsonContainerType None;  // const
        public static Newtonsoft.Json.JsonContainerType Object;  // const
        public static Newtonsoft.Json.JsonContainerType Array;  // const
        public static Newtonsoft.Json.JsonContainerType Constructor;  // const

    }

    // TypeToken: 0x2000023  // size: 0x28
    public sealed struct JsonPosition
    {
        // Fields
        private static readonly System.Char[] SpecialCharacters;  // static @ 0x0
        private Newtonsoft.Json.JsonContainerType Type;  // 0x10
        private System.Int32 Position;  // 0x14
        private System.String PropertyName;  // 0x18
        private System.Boolean HasIndex;  // 0x20

        // Methods
        // RVA: 0x02FB6FF0  token: 0x6000050
        public System.Void .ctor(Newtonsoft.Json.JsonContainerType type) { }
        // RVA: 0x0A8A72E4  token: 0x6000051
        private System.Int32 CalculateLength() { }
        // RVA: 0x0A8A7550  token: 0x6000052
        private System.Void WriteTo(System.Text.StringBuilder sb, System.IO.StringWriter& writer, System.Char[]& buffer) { }
        // RVA: 0x02FB7090  token: 0x6000053
        private static System.Boolean TypeHasIndex(Newtonsoft.Json.JsonContainerType type) { }
        // RVA: 0x0A8A6FB0  token: 0x6000054
        private static System.String BuildPath(System.Collections.Generic.List<Newtonsoft.Json.JsonPosition> positions, System.Nullable<Newtonsoft.Json.JsonPosition> currentPosition) { }
        // RVA: 0x0A8A7374  token: 0x6000055
        private static System.String FormatMessage(Newtonsoft.Json.IJsonLineInfo lineInfo, System.String path, System.String message) { }
        // RVA: 0x04D17BE0  token: 0x6000056
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000024  // size: 0x88
    public sealed class JsonPropertyAttribute : System.Attribute
    {
        // Fields
        private System.Nullable<Newtonsoft.Json.NullValueHandling> _nullValueHandling;  // 0x10
        private System.Nullable<Newtonsoft.Json.DefaultValueHandling> _defaultValueHandling;  // 0x18
        private System.Nullable<Newtonsoft.Json.ReferenceLoopHandling> _referenceLoopHandling;  // 0x20
        private System.Nullable<Newtonsoft.Json.ObjectCreationHandling> _objectCreationHandling;  // 0x28
        private System.Nullable<Newtonsoft.Json.TypeNameHandling> _typeNameHandling;  // 0x30
        private System.Nullable<System.Boolean> _isReference;  // 0x38
        private System.Nullable<System.Int32> _order;  // 0x3c
        private System.Nullable<Newtonsoft.Json.Required> _required;  // 0x44
        private System.Nullable<System.Boolean> _itemIsReference;  // 0x4c
        private System.Nullable<Newtonsoft.Json.ReferenceLoopHandling> _itemReferenceLoopHandling;  // 0x50
        private System.Nullable<Newtonsoft.Json.TypeNameHandling> _itemTypeNameHandling;  // 0x58
        private System.Type <ItemConverterType>k__BackingField;  // 0x60
        private System.Object[] <ItemConverterParameters>k__BackingField;  // 0x68
        private System.Type <NamingStrategyType>k__BackingField;  // 0x70
        private System.Object[] <NamingStrategyParameters>k__BackingField;  // 0x78
        private System.String <PropertyName>k__BackingField;  // 0x80

        // Properties
        System.Type ItemConverterType { get; /* RVA: 0x04D86280 */ }
        System.Object[] ItemConverterParameters { get; /* RVA: 0x04D86230 */ }
        System.Type NamingStrategyType { get; /* RVA: 0x04D862B0 */ }
        System.Object[] NamingStrategyParameters { get; /* RVA: 0x04D862A0 */ }
        Newtonsoft.Json.DefaultValueHandling DefaultValueHandling { set; /* RVA: 0x047C8250 */ }
        System.Int32 Order { set; /* RVA: 0x03FC47A0 */ }
        System.String PropertyName { get; /* RVA: 0x04D86260 */ set; /* RVA: 0x04CD9970 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x600005F
        public System.Void .ctor() { }
        // RVA: 0x04CD9970  token: 0x6000060
        public System.Void .ctor(System.String propertyName) { }

    }

    // TypeToken: 0x2000025  // size: 0x78
    public abstract class JsonReader : System.IDisposable
    {
        // Fields
        private Newtonsoft.Json.JsonToken _tokenType;  // 0x10
        private System.Object _value;  // 0x18
        private System.Char _quoteChar;  // 0x20
        private Newtonsoft.Json.JsonReader.State _currentState;  // 0x24
        private Newtonsoft.Json.JsonPosition _currentPosition;  // 0x28
        private System.Globalization.CultureInfo _culture;  // 0x40
        private Newtonsoft.Json.DateTimeZoneHandling _dateTimeZoneHandling;  // 0x48
        private System.Nullable<System.Int32> _maxDepth;  // 0x4c
        private System.Boolean _hasExceededMaxDepth;  // 0x54
        private Newtonsoft.Json.DateParseHandling _dateParseHandling;  // 0x58
        private Newtonsoft.Json.FloatParseHandling _floatParseHandling;  // 0x5c
        private System.String _dateFormatString;  // 0x60
        private System.Collections.Generic.List<Newtonsoft.Json.JsonPosition> _stack;  // 0x68
        private System.Boolean <CloseInput>k__BackingField;  // 0x70
        private System.Boolean <SupportMultipleContent>k__BackingField;  // 0x71

        // Properties
        Newtonsoft.Json.JsonReader.State CurrentState { get; /* RVA: 0x011F0020 */ }
        System.Boolean CloseInput { get; /* RVA: 0x04D86A50 */ set; /* RVA: 0x04D86A80 */ }
        System.Boolean SupportMultipleContent { get; /* RVA: 0x04D89040 */ set; /* RVA: 0x04D89050 */ }
        Newtonsoft.Json.DateTimeZoneHandling DateTimeZoneHandling { get; /* RVA: 0x04D863D0 */ set; /* RVA: 0x0A8A9CD8 */ }
        Newtonsoft.Json.DateParseHandling DateParseHandling { get; /* RVA: 0x04D86570 */ set; /* RVA: 0x0A8A9C70 */ }
        Newtonsoft.Json.FloatParseHandling FloatParseHandling { get; /* RVA: 0x04D86560 */ set; /* RVA: 0x0A8A9D40 */ }
        System.String DateFormatString { get; /* RVA: 0x04D86280 */ set; /* RVA: 0x063CC164 */ }
        System.Nullable<System.Int32> MaxDepth { get; /* RVA: 0x04DA5BD0 */ set; /* RVA: 0x02FBED20 */ }
        Newtonsoft.Json.JsonToken TokenType { get; /* RVA: 0x02B2E2D0 */ }
        System.Object Value { get; /* RVA: 0x0385B100 */ }
        System.Type ValueType { get; /* RVA: 0x0A8A9C4C */ }
        System.Int32 Depth { get; /* RVA: 0x034252E0 */ }
        System.String Path { get; /* RVA: 0x0A8A9B58 */ }
        System.Globalization.CultureInfo Culture { get; /* RVA: 0x02FBB470 */ set; /* RVA: 0x042B4AE0 */ }

        // Methods
        // RVA: 0x0A8A7990  token: 0x6000077
        private Newtonsoft.Json.JsonPosition GetPosition(System.Int32 depth) { }
        // RVA: 0x035A6D00  token: 0x6000078
        protected System.Void .ctor() { }
        // RVA: 0x02FB70A0  token: 0x6000079
        private System.Void Push(Newtonsoft.Json.JsonContainerType value) { }
        // RVA: 0x03638300  token: 0x600007A
        private Newtonsoft.Json.JsonContainerType Pop() { }
        // RVA: 0x011F2A90  token: 0x600007B
        private Newtonsoft.Json.JsonContainerType Peek() { }
        // RVA: -1  // abstract  token: 0x600007C
        public virtual System.Boolean Read() { }
        // RVA: 0x0A8A8AE4  token: 0x600007D
        public virtual System.Nullable<System.Int32> ReadAsInt32() { }
        // RVA: 0x0A8A97C0  token: 0x600007E
        private System.Nullable<System.Int32> ReadInt32String(System.String s) { }
        // RVA: 0x0342A500  token: 0x600007F
        public virtual System.String ReadAsString() { }
        // RVA: 0x0A8A7CBC  token: 0x6000080
        public virtual System.Byte[] ReadAsBytes() { }
        // RVA: 0x0A8A7BE8  token: 0x6000081
        private System.Byte[] ReadArrayIntoByteArray() { }
        // RVA: 0x0A8A7A34  token: 0x6000082
        private System.Boolean ReadArrayElementIntoByteArrayReportDone(System.Collections.Generic.List<System.Byte> buffer) { }
        // RVA: 0x0A8A8840  token: 0x6000083
        public virtual System.Nullable<System.Double> ReadAsDouble() { }
        // RVA: 0x0A8A95E0  token: 0x6000084
        private System.Nullable<System.Double> ReadDoubleString(System.String s) { }
        // RVA: 0x0342A430  token: 0x6000085
        public virtual System.Nullable<System.Boolean> ReadAsBoolean() { }
        // RVA: 0x0A8A8E70  token: 0x6000086
        private System.Nullable<System.Boolean> ReadBooleanString(System.String s) { }
        // RVA: 0x0A8A8468  token: 0x6000087
        public virtual System.Nullable<System.Decimal> ReadAsDecimal() { }
        // RVA: 0x0A8A93D0  token: 0x6000088
        private System.Nullable<System.Decimal> ReadDecimalString(System.String s) { }
        // RVA: 0x0A8A8240  token: 0x6000089
        public virtual System.Nullable<System.DateTime> ReadAsDateTime() { }
        // RVA: 0x0A8A91E0  token: 0x600008A
        private System.Nullable<System.DateTime> ReadDateTimeString(System.String s) { }
        // RVA: 0x0A8A8008  token: 0x600008B
        public virtual System.Nullable<System.DateTimeOffset> ReadAsDateTimeOffset() { }
        // RVA: 0x0A8A8FD8  token: 0x600008C
        private System.Nullable<System.DateTimeOffset> ReadDateTimeOffsetString(System.String s) { }
        // RVA: 0x0A8A9B10  token: 0x600008D
        private System.Void ReaderReadAndAssert() { }
        // RVA: 0x0A8A7950  token: 0x600008E
        private Newtonsoft.Json.JsonReaderException CreateUnexpectedEndException() { }
        // RVA: 0x0A8A992C  token: 0x600008F
        private System.Void ReadIntoWrappedTypeObject() { }
        // RVA: 0x03424C90  token: 0x6000090
        public System.Void Skip() { }
        // RVA: 0x05390BEC  token: 0x6000091
        protected System.Void SetToken(Newtonsoft.Json.JsonToken newToken) { }
        // RVA: 0x0539086C  token: 0x6000092
        protected System.Void SetToken(Newtonsoft.Json.JsonToken newToken, System.Object value) { }
        // RVA: 0x02FB8CC0  token: 0x6000093
        protected System.Void SetToken(Newtonsoft.Json.JsonToken newToken, System.Object value, System.Boolean updateIndex) { }
        // RVA: 0x02FB8F20  token: 0x6000094
        private System.Void SetPostValueState(System.Boolean updateIndex) { }
        // RVA: 0x02FB8F60  token: 0x6000095
        private System.Void UpdateScopeWithFinishedValue() { }
        // RVA: 0x03637ED0  token: 0x6000096
        private System.Void ValidateEnd(Newtonsoft.Json.JsonToken endToken) { }
        // RVA: 0x038306D0  token: 0x6000097
        protected System.Void SetStateBasedOnCurrent() { }
        // RVA: 0x03638550  token: 0x6000098
        private System.Void SetFinished() { }
        // RVA: 0x036382C0  token: 0x6000099
        private Newtonsoft.Json.JsonContainerType GetTypeForCloseToken(Newtonsoft.Json.JsonToken token) { }
        // RVA: 0x03F608D0  token: 0x600009A
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x03F60930  token: 0x600009B
        protected virtual System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x037786B0  token: 0x600009C
        public virtual System.Void Close() { }
        // RVA: 0x032B61B0  token: 0x600009D
        private System.Void ReadAndAssert() { }
        // RVA: 0x0A8A976C  token: 0x600009E
        private System.Void ReadForTypeAndAssert(Newtonsoft.Json.Serialization.JsonContract contract, System.Boolean hasConverter) { }
        // RVA: 0x02FB4440  token: 0x600009F
        private System.Boolean ReadForType(Newtonsoft.Json.Serialization.JsonContract contract, System.Boolean hasConverter) { }
        // RVA: 0x03424BC0  token: 0x60000A0
        private System.Boolean ReadAndMoveToContent() { }
        // RVA: 0x03424C10  token: 0x60000A1
        private System.Boolean MoveToContent() { }
        // RVA: 0x0342A5B0  token: 0x60000A2
        private Newtonsoft.Json.JsonToken GetContentToken() { }

    }

    // TypeToken: 0x2000027  // size: 0xA0
    public class JsonReaderException : Newtonsoft.Json.JsonException
    {
        // Fields
        private readonly System.Int32 <LineNumber>k__BackingField;  // 0x90
        private readonly System.Int32 <LinePosition>k__BackingField;  // 0x94
        private readonly System.String <Path>k__BackingField;  // 0x98

        // Methods
        // RVA: 0x0A8A78C8  token: 0x60000A3
        public System.Void .ctor() { }
        // RVA: 0x0A8A7948  token: 0x60000A4
        public System.Void .ctor(System.String message) { }
        // RVA: 0x0A8A7928  token: 0x60000A5
        public System.Void .ctor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x0A8A78D0  token: 0x60000A6
        public System.Void .ctor(System.String message, System.String path, System.Int32 lineNumber, System.Int32 linePosition, System.Exception innerException) { }
        // RVA: 0x0A8A78BC  token: 0x60000A7
        private static Newtonsoft.Json.JsonReaderException Create(Newtonsoft.Json.JsonReader reader, System.String message) { }
        // RVA: 0x0A8A782C  token: 0x60000A8
        private static Newtonsoft.Json.JsonReaderException Create(Newtonsoft.Json.JsonReader reader, System.String message, System.Exception ex) { }
        // RVA: 0x0A8A773C  token: 0x60000A9
        private static Newtonsoft.Json.JsonReaderException Create(Newtonsoft.Json.IJsonLineInfo lineInfo, System.String path, System.String message, System.Exception ex) { }

    }

    // TypeToken: 0x2000028  // size: 0x10
    public sealed class JsonRequiredAttribute : System.Attribute
    {
    }

    // TypeToken: 0x2000029  // size: 0xA0
    public class JsonSerializationException : Newtonsoft.Json.JsonException
    {
        // Fields
        private readonly System.Int32 <LineNumber>k__BackingField;  // 0x90
        private readonly System.Int32 <LinePosition>k__BackingField;  // 0x94
        private readonly System.String <Path>k__BackingField;  // 0x98

        // Methods
        // RVA: 0x0A8A78C8  token: 0x60000AA
        public System.Void .ctor() { }
        // RVA: 0x0A8A7948  token: 0x60000AB
        public System.Void .ctor(System.String message) { }
        // RVA: 0x0A8A9F34  token: 0x60000AC
        public System.Void .ctor(System.String message, System.Exception innerException) { }
        // RVA: 0x0A8A7928  token: 0x60000AD
        public System.Void .ctor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x0A8A78D0  token: 0x60000AE
        public System.Void .ctor(System.String message, System.String path, System.Int32 lineNumber, System.Int32 linePosition, System.Exception innerException) { }
        // RVA: 0x0A8A9DA8  token: 0x60000AF
        private static Newtonsoft.Json.JsonSerializationException Create(Newtonsoft.Json.JsonReader reader, System.String message) { }
        // RVA: 0x0A8A9DB4  token: 0x60000B0
        private static Newtonsoft.Json.JsonSerializationException Create(Newtonsoft.Json.JsonReader reader, System.String message, System.Exception ex) { }
        // RVA: 0x0A8A9E44  token: 0x60000B1
        private static Newtonsoft.Json.JsonSerializationException Create(Newtonsoft.Json.IJsonLineInfo lineInfo, System.String path, System.String message, System.Exception ex) { }

    }

    // TypeToken: 0x200002A  // size: 0xE0
    public class JsonSerializer
    {
        // Fields
        private Newtonsoft.Json.TypeNameHandling _typeNameHandling;  // 0x10
        private Newtonsoft.Json.TypeNameAssemblyFormatHandling _typeNameAssemblyFormatHandling;  // 0x14
        private Newtonsoft.Json.PreserveReferencesHandling _preserveReferencesHandling;  // 0x18
        private Newtonsoft.Json.ReferenceLoopHandling _referenceLoopHandling;  // 0x1c
        private Newtonsoft.Json.MissingMemberHandling _missingMemberHandling;  // 0x20
        private Newtonsoft.Json.ObjectCreationHandling _objectCreationHandling;  // 0x24
        private Newtonsoft.Json.NullValueHandling _nullValueHandling;  // 0x28
        private Newtonsoft.Json.DefaultValueHandling _defaultValueHandling;  // 0x2c
        private Newtonsoft.Json.ConstructorHandling _constructorHandling;  // 0x30
        private Newtonsoft.Json.MetadataPropertyHandling _metadataPropertyHandling;  // 0x34
        private Newtonsoft.Json.JsonConverterCollection _converters;  // 0x38
        private Newtonsoft.Json.Serialization.IContractResolver _contractResolver;  // 0x40
        private Newtonsoft.Json.Serialization.ITraceWriter _traceWriter;  // 0x48
        private System.Collections.IEqualityComparer _equalityComparer;  // 0x50
        private Newtonsoft.Json.Serialization.ISerializationBinder _serializationBinder;  // 0x58
        private System.Runtime.Serialization.StreamingContext _context;  // 0x60
        private Newtonsoft.Json.Serialization.IReferenceResolver _referenceResolver;  // 0x70
        private System.Nullable<Newtonsoft.Json.Formatting> _formatting;  // 0x78
        private System.Nullable<Newtonsoft.Json.DateFormatHandling> _dateFormatHandling;  // 0x80
        private System.Nullable<Newtonsoft.Json.DateTimeZoneHandling> _dateTimeZoneHandling;  // 0x88
        private System.Nullable<Newtonsoft.Json.DateParseHandling> _dateParseHandling;  // 0x90
        private System.Nullable<Newtonsoft.Json.FloatFormatHandling> _floatFormatHandling;  // 0x98
        private System.Nullable<Newtonsoft.Json.FloatParseHandling> _floatParseHandling;  // 0xa0
        private System.Nullable<Newtonsoft.Json.StringEscapeHandling> _stringEscapeHandling;  // 0xa8
        private System.Globalization.CultureInfo _culture;  // 0xb0
        private System.Nullable<System.Int32> _maxDepth;  // 0xb8
        private System.Boolean _maxDepthSet;  // 0xc0
        private System.Nullable<System.Boolean> _checkAdditionalContent;  // 0xc1
        private System.String _dateFormatString;  // 0xc8
        private System.Boolean _dateFormatStringSet;  // 0xd0
        private System.EventHandler<Newtonsoft.Json.Serialization.ErrorEventArgs> Error;  // 0xd8

        // Properties
        Newtonsoft.Json.Serialization.IReferenceResolver ReferenceResolver { set; /* RVA: 0x0A8AA708 */ }
        Newtonsoft.Json.Serialization.ISerializationBinder SerializationBinder { get; /* RVA: 0x02E56440 */ set; /* RVA: 0x0A8AA794 */ }
        Newtonsoft.Json.Serialization.ITraceWriter TraceWriter { get; /* RVA: 0x04D86200 */ set; /* RVA: 0x035A41A0 */ }
        System.Collections.IEqualityComparer EqualityComparer { set; /* RVA: 0x02FDAEA0 */ }
        Newtonsoft.Json.TypeNameHandling TypeNameHandling { set; /* RVA: 0x0411B110 */ }
        Newtonsoft.Json.TypeNameAssemblyFormatHandling TypeNameAssemblyFormatHandling { set; /* RVA: 0x04D83110 */ }
        Newtonsoft.Json.PreserveReferencesHandling PreserveReferencesHandling { set; /* RVA: 0x0A8AA6A0 */ }
        Newtonsoft.Json.ReferenceLoopHandling ReferenceLoopHandling { set; /* RVA: 0x04D83130 */ }
        Newtonsoft.Json.MissingMemberHandling MissingMemberHandling { set; /* RVA: 0x0A8AA638 */ }
        Newtonsoft.Json.NullValueHandling NullValueHandling { get; /* RVA: 0x011F2A90 */ set; /* RVA: 0x0411B460 */ }
        Newtonsoft.Json.DefaultValueHandling DefaultValueHandling { set; /* RVA: 0x0411B480 */ }
        Newtonsoft.Json.ObjectCreationHandling ObjectCreationHandling { set; /* RVA: 0x04CB28E0 */ }
        Newtonsoft.Json.ConstructorHandling ConstructorHandling { set; /* RVA: 0x04CB2900 */ }
        Newtonsoft.Json.MetadataPropertyHandling MetadataPropertyHandling { get; /* RVA: 0x04D86730 */ set; /* RVA: 0x0A8AA5D0 */ }
        Newtonsoft.Json.JsonConverterCollection Converters { get; /* RVA: 0x03EA57F0 */ }
        Newtonsoft.Json.Serialization.IContractResolver ContractResolver { get; /* RVA: 0x04D85A60 */ set; /* RVA: 0x049DAC00 */ }
        System.Runtime.Serialization.StreamingContext Context { get; /* RVA: 0x04DA1280 */ set; /* RVA: 0x0A8AA5C0 */ }
        Newtonsoft.Json.Formatting Formatting { get; /* RVA: 0x04CD9280 */ set; /* RVA: 0x04B89F60 */ }
        System.Nullable<System.Int32> MaxDepth { get; /* RVA: 0x04D85EF0 */ }
        System.Boolean CheckAdditionalContent { get; /* RVA: 0x04DA5BF0 */ set; /* RVA: 0x04119FE0 */ }

        // Events
        event System.EventHandler<Newtonsoft.Json.Serialization.ErrorEventArgs> Error;

        // Methods
        // RVA: 0x04D9E3E0  token: 0x60000D0
        private System.Boolean IsCheckAdditionalContentSet() { }
        // RVA: 0x035A6AC0  token: 0x60000D1
        public System.Void .ctor() { }
        // RVA: 0x035A6A20  token: 0x60000D2
        public static Newtonsoft.Json.JsonSerializer Create() { }
        // RVA: 0x035A69F0  token: 0x60000D3
        public static Newtonsoft.Json.JsonSerializer Create(Newtonsoft.Json.JsonSerializerSettings settings) { }
        // RVA: 0x035A6980  token: 0x60000D4
        public static Newtonsoft.Json.JsonSerializer CreateDefault() { }
        // RVA: 0x035A6940  token: 0x60000D5
        public static Newtonsoft.Json.JsonSerializer CreateDefault(Newtonsoft.Json.JsonSerializerSettings settings) { }
        // RVA: 0x035A6D40  token: 0x60000D6
        private static System.Void ApplySerializerSettings(Newtonsoft.Json.JsonSerializer serializer, Newtonsoft.Json.JsonSerializerSettings settings) { }
        // RVA: 0x0A8AA454  token: 0x60000D7
        public System.Void Populate(Newtonsoft.Json.JsonReader reader, System.Object target) { }
        // RVA: 0x0A8AA1C0  token: 0x60000D8
        private virtual System.Void PopulateInternal(Newtonsoft.Json.JsonReader reader, System.Object target) { }
        // RVA: -1  // generic def  token: 0x60000D9
        public T Deserialize(Newtonsoft.Json.JsonReader reader) { }
        // RVA: 0x05392F9C  token: 0x60000DA
        public System.Object Deserialize(Newtonsoft.Json.JsonReader reader, System.Type objectType) { }
        // RVA: 0x02FBCD40  token: 0x60000DB
        private virtual System.Object DeserializeInternal(Newtonsoft.Json.JsonReader reader, System.Type objectType) { }
        // RVA: 0x02FBCB20  token: 0x60000DC
        private System.Void SetupReader(Newtonsoft.Json.JsonReader reader, System.Globalization.CultureInfo& previousCulture, System.Nullable<Newtonsoft.Json.DateTimeZoneHandling>& previousDateTimeZoneHandling, System.Nullable<Newtonsoft.Json.DateParseHandling>& previousDateParseHandling, System.Nullable<Newtonsoft.Json.FloatParseHandling>& previousFloatParseHandling, System.Nullable<System.Int32>& previousMaxDepth, System.String& previousDateFormatString) { }
        // RVA: 0x02FBEC20  token: 0x60000DD
        private System.Void ResetReader(Newtonsoft.Json.JsonReader reader, System.Globalization.CultureInfo previousCulture, System.Nullable<Newtonsoft.Json.DateTimeZoneHandling> previousDateTimeZoneHandling, System.Nullable<Newtonsoft.Json.DateParseHandling> previousDateParseHandling, System.Nullable<Newtonsoft.Json.FloatParseHandling> previousFloatParseHandling, System.Nullable<System.Int32> previousMaxDepth, System.String previousDateFormatString) { }
        // RVA: 0x053924E0  token: 0x60000DE
        public System.Void Serialize(Newtonsoft.Json.JsonWriter jsonWriter, System.Object value, System.Type objectType) { }
        // RVA: 0x05392EF0  token: 0x60000DF
        public System.Void Serialize(Newtonsoft.Json.JsonWriter jsonWriter, System.Object value) { }
        // RVA: 0x0A8AA0C4  token: 0x60000E0
        private Newtonsoft.Json.Serialization.TraceJsonReader CreateTraceJsonReader(Newtonsoft.Json.JsonReader reader) { }
        // RVA: 0x03F71C60  token: 0x60000E1
        private virtual System.Void SerializeInternal(Newtonsoft.Json.JsonWriter jsonWriter, System.Object value, System.Type objectType) { }
        // RVA: 0x0A8AA144  token: 0x60000E2
        private Newtonsoft.Json.Serialization.IReferenceResolver GetReferenceResolver() { }
        // RVA: 0x05390BBC  token: 0x60000E3
        private Newtonsoft.Json.JsonConverter GetMatchingConverter(System.Type type) { }
        // RVA: 0x02FAFF80  token: 0x60000E4
        private static Newtonsoft.Json.JsonConverter GetMatchingConverter(System.Collections.Generic.IList<Newtonsoft.Json.JsonConverter> converters, System.Type objectType) { }
        // RVA: 0x0A8AA1A0  token: 0x60000E5
        private System.Void OnError(Newtonsoft.Json.Serialization.ErrorEventArgs e) { }

    }

    // TypeToken: 0x200002B  // size: 0x110
    public class JsonSerializerSettings
    {
        // Fields
        private static Newtonsoft.Json.ReferenceLoopHandling DefaultReferenceLoopHandling;  // const
        private static Newtonsoft.Json.MissingMemberHandling DefaultMissingMemberHandling;  // const
        private static Newtonsoft.Json.NullValueHandling DefaultNullValueHandling;  // const
        private static Newtonsoft.Json.DefaultValueHandling DefaultDefaultValueHandling;  // const
        private static Newtonsoft.Json.ObjectCreationHandling DefaultObjectCreationHandling;  // const
        private static Newtonsoft.Json.PreserveReferencesHandling DefaultPreserveReferencesHandling;  // const
        private static Newtonsoft.Json.ConstructorHandling DefaultConstructorHandling;  // const
        private static Newtonsoft.Json.TypeNameHandling DefaultTypeNameHandling;  // const
        private static Newtonsoft.Json.MetadataPropertyHandling DefaultMetadataPropertyHandling;  // const
        private static readonly System.Runtime.Serialization.StreamingContext DefaultContext;  // static @ 0x0
        private static Newtonsoft.Json.Formatting DefaultFormatting;  // const
        private static Newtonsoft.Json.DateFormatHandling DefaultDateFormatHandling;  // const
        private static Newtonsoft.Json.DateTimeZoneHandling DefaultDateTimeZoneHandling;  // const
        private static Newtonsoft.Json.DateParseHandling DefaultDateParseHandling;  // const
        private static Newtonsoft.Json.FloatParseHandling DefaultFloatParseHandling;  // const
        private static Newtonsoft.Json.FloatFormatHandling DefaultFloatFormatHandling;  // const
        private static Newtonsoft.Json.StringEscapeHandling DefaultStringEscapeHandling;  // const
        private static Newtonsoft.Json.TypeNameAssemblyFormatHandling DefaultTypeNameAssemblyFormatHandling;  // const
        private static readonly System.Globalization.CultureInfo DefaultCulture;  // static @ 0x10
        private static System.Boolean DefaultCheckAdditionalContent;  // const
        private static System.String DefaultDateFormatString;  // const
        private static System.Int32 DefaultMaxDepth;  // const
        private System.Nullable<Newtonsoft.Json.Formatting> _formatting;  // 0x10
        private System.Nullable<Newtonsoft.Json.DateFormatHandling> _dateFormatHandling;  // 0x18
        private System.Nullable<Newtonsoft.Json.DateTimeZoneHandling> _dateTimeZoneHandling;  // 0x20
        private System.Nullable<Newtonsoft.Json.DateParseHandling> _dateParseHandling;  // 0x28
        private System.Nullable<Newtonsoft.Json.FloatFormatHandling> _floatFormatHandling;  // 0x30
        private System.Nullable<Newtonsoft.Json.FloatParseHandling> _floatParseHandling;  // 0x38
        private System.Nullable<Newtonsoft.Json.StringEscapeHandling> _stringEscapeHandling;  // 0x40
        private System.Globalization.CultureInfo _culture;  // 0x48
        private System.Nullable<System.Boolean> _checkAdditionalContent;  // 0x50
        private System.Nullable<System.Int32> _maxDepth;  // 0x54
        private System.Boolean _maxDepthSet;  // 0x5c
        private System.String _dateFormatString;  // 0x60
        private System.Boolean _dateFormatStringSet;  // 0x68
        private System.Nullable<Newtonsoft.Json.TypeNameAssemblyFormatHandling> _typeNameAssemblyFormatHandling;  // 0x6c
        private System.Nullable<Newtonsoft.Json.DefaultValueHandling> _defaultValueHandling;  // 0x74
        private System.Nullable<Newtonsoft.Json.PreserveReferencesHandling> _preserveReferencesHandling;  // 0x7c
        private System.Nullable<Newtonsoft.Json.NullValueHandling> _nullValueHandling;  // 0x84
        private System.Nullable<Newtonsoft.Json.ObjectCreationHandling> _objectCreationHandling;  // 0x8c
        private System.Nullable<Newtonsoft.Json.MissingMemberHandling> _missingMemberHandling;  // 0x94
        private System.Nullable<Newtonsoft.Json.ReferenceLoopHandling> _referenceLoopHandling;  // 0x9c
        private System.Nullable<System.Runtime.Serialization.StreamingContext> _context;  // 0xa8
        private System.Nullable<Newtonsoft.Json.ConstructorHandling> _constructorHandling;  // 0xc0
        private System.Nullable<Newtonsoft.Json.TypeNameHandling> _typeNameHandling;  // 0xc8
        private System.Nullable<Newtonsoft.Json.MetadataPropertyHandling> _metadataPropertyHandling;  // 0xd0
        private System.Collections.Generic.IList<Newtonsoft.Json.JsonConverter> <Converters>k__BackingField;  // 0xd8
        private Newtonsoft.Json.Serialization.IContractResolver <ContractResolver>k__BackingField;  // 0xe0
        private System.Collections.IEqualityComparer <EqualityComparer>k__BackingField;  // 0xe8
        private System.Func<Newtonsoft.Json.Serialization.IReferenceResolver> <ReferenceResolverProvider>k__BackingField;  // 0xf0
        private Newtonsoft.Json.Serialization.ITraceWriter <TraceWriter>k__BackingField;  // 0xf8
        private Newtonsoft.Json.Serialization.ISerializationBinder <SerializationBinder>k__BackingField;  // 0x100
        private System.EventHandler<Newtonsoft.Json.Serialization.ErrorEventArgs> <Error>k__BackingField;  // 0x108

        // Properties
        Newtonsoft.Json.ReferenceLoopHandling ReferenceLoopHandling { get; /* RVA: 0x04D7C0B0 */ set; /* RVA: 0x03581CC0 */ }
        Newtonsoft.Json.MissingMemberHandling MissingMemberHandling { get; /* RVA: 0x0A8AA04C */ }
        Newtonsoft.Json.ObjectCreationHandling ObjectCreationHandling { get; /* RVA: 0x035A7100 */ set; /* RVA: 0x03581D80 */ }
        Newtonsoft.Json.NullValueHandling NullValueHandling { get; /* RVA: 0x04D86890 */ set; /* RVA: 0x04D2B120 */ }
        Newtonsoft.Json.DefaultValueHandling DefaultValueHandling { get; /* RVA: 0x04D863C0 */ set; /* RVA: 0x04D2B180 */ }
        System.Collections.Generic.IList<Newtonsoft.Json.JsonConverter> Converters { get; /* RVA: 0x04D88EF0 */ set; /* RVA: 0x06902698 */ }
        Newtonsoft.Json.PreserveReferencesHandling PreserveReferencesHandling { get; /* RVA: 0x0A8AA088 */ }
        Newtonsoft.Json.TypeNameHandling TypeNameHandling { get; /* RVA: 0x04DA5BE0 */ set; /* RVA: 0x03581D20 */ }
        Newtonsoft.Json.MetadataPropertyHandling MetadataPropertyHandling { get; /* RVA: 0x0A8AA010 */ }
        Newtonsoft.Json.TypeNameAssemblyFormatHandling TypeNameAssemblyFormatHandling { get; /* RVA: 0x04D7C070 */ set; /* RVA: 0x03581C60 */ }
        Newtonsoft.Json.ConstructorHandling ConstructorHandling { get; /* RVA: 0x035A70C0 */ set; /* RVA: 0x03581DE0 */ }
        Newtonsoft.Json.Serialization.IContractResolver ContractResolver { get; /* RVA: 0x04D8D1C0 */ set; /* RVA: 0x06AC5B08 */ }
        System.Collections.IEqualityComparer EqualityComparer { get; /* RVA: 0x04D88590 */ set; /* RVA: 0x06AC5B1C */ }
        System.Func<Newtonsoft.Json.Serialization.IReferenceResolver> ReferenceResolverProvider { get; /* RVA: 0x04D8D1A0 */ set; /* RVA: 0x06AC5AE0 */ }
        Newtonsoft.Json.Serialization.ITraceWriter TraceWriter { get; /* RVA: 0x04D877C0 */ set; /* RVA: 0x06790298 */ }
        Newtonsoft.Json.Serialization.ISerializationBinder SerializationBinder { get; /* RVA: 0x04D87D50 */ set; /* RVA: 0x067A5EA0 */ }
        System.EventHandler<Newtonsoft.Json.Serialization.ErrorEventArgs> Error { get; /* RVA: 0x04D85F90 */ set; /* RVA: 0x068C2E50 */ }
        System.Runtime.Serialization.StreamingContext Context { get; /* RVA: 0x0A8A9F74 */ }
        System.Nullable<System.Int32> MaxDepth { set; /* RVA: 0x04CAB710 */ }
        Newtonsoft.Json.Formatting Formatting { set; /* RVA: 0x03581E40 */ }
        System.Boolean CheckAdditionalContent { get; /* RVA: 0x0A8A9F3C */ }

        // Methods
        // RVA: 0x04D21F60  token: 0x6000109
        private static System.Void .cctor() { }
        // RVA: 0x035A41B0  token: 0x600010A
        public System.Void .ctor() { }
        // RVA: 0x035A4230  token: 0x600010B
        public System.Void .ctor(Newtonsoft.Json.JsonSerializerSettings original) { }

    }

    // TypeToken: 0x200002C  // size: 0xD8
    public class JsonTextReader : Newtonsoft.Json.JsonReader, Newtonsoft.Json.IJsonLineInfo
    {
        // Fields
        private readonly System.Boolean _safeAsync;  // 0x78
        private readonly System.IO.TextReader _reader;  // 0x80
        private System.Char[] _chars;  // 0x88
        private System.Int32 _charsUsed;  // 0x90
        private System.Int32 _charPos;  // 0x94
        private System.Int32 _lineStartPos;  // 0x98
        private System.Int32 _lineNumber;  // 0x9c
        private System.Boolean _isEndOfFile;  // 0xa0
        private Newtonsoft.Json.Utilities.StringBuffer _stringBuffer;  // 0xa8
        private Newtonsoft.Json.Utilities.StringReference _stringReference;  // 0xb8
        private Newtonsoft.Json.IArrayPool<System.Char> _arrayPool;  // 0xc8
        private Newtonsoft.Json.JsonNameTable <PropertyNameTable>k__BackingField;  // 0xd0

        // Properties
        Newtonsoft.Json.JsonNameTable PropertyNameTable { get; /* RVA: 0x04D876B0 */ set; /* RVA: 0x0678D7BC */ }
        System.Int32 LineNumber { get; /* RVA: 0x03427620 */ }
        System.Int32 LinePosition { get; /* RVA: 0x04DA5C50 */ }

        // Methods
        // RVA: 0x035A6C60  token: 0x600010C
        public System.Void .ctor(System.IO.TextReader reader) { }
        // RVA: 0x03778750  token: 0x600010F
        private System.Void EnsureBufferNotEmpty() { }
        // RVA: 0x02F054D0  token: 0x6000110
        private System.Void SetNewLine(System.Boolean hasNextChar) { }
        // RVA: 0x04DA5C20  token: 0x6000111
        private System.Void OnNewLine(System.Int32 pos) { }
        // RVA: 0x02FBB340  token: 0x6000112
        private System.Void ParseString(System.Char quote, Newtonsoft.Json.ReadType readType) { }
        // RVA: 0x02FBAF00  token: 0x6000113
        private System.Void ParseReadString(System.Char quote, Newtonsoft.Json.ReadType readType) { }
        // RVA: 0x032A9100  token: 0x6000114
        private static System.Void BlockCopyChars(System.Char[] src, System.Int32 srcOffset, System.Char[] dst, System.Int32 dstOffset, System.Int32 count) { }
        // RVA: 0x02F04780  token: 0x6000115
        private System.Void ShiftBufferIfNeeded() { }
        // RVA: 0x05390C28  token: 0x6000116
        private System.Int32 ReadData(System.Boolean append) { }
        // RVA: 0x03777AF0  token: 0x6000117
        private System.Void PrepareBufferForReadData(System.Boolean append, System.Int32 charsRequired) { }
        // RVA: 0x03777960  token: 0x6000118
        private System.Int32 ReadData(System.Boolean append, System.Int32 charsRequired) { }
        // RVA: 0x02F051B0  token: 0x6000119
        private System.Boolean EnsureChars(System.Int32 relativePosition, System.Boolean append) { }
        // RVA: 0x03777810  token: 0x600011A
        private System.Boolean ReadChars(System.Int32 relativePosition, System.Boolean append) { }
        // RVA: 0x02F04D70  token: 0x600011B
        public virtual System.Boolean Read() { }
        // RVA: 0x02FBC490  token: 0x600011C
        public virtual System.Nullable<System.Int32> ReadAsInt32() { }
        // RVA: 0x0A8AC234  token: 0x600011D
        public virtual System.Nullable<System.DateTime> ReadAsDateTime() { }
        // RVA: 0x02FB86A0  token: 0x600011E
        public virtual System.String ReadAsString() { }
        // RVA: 0x0A8ABCEC  token: 0x600011F
        public virtual System.Byte[] ReadAsBytes() { }
        // RVA: 0x02FB7970  token: 0x6000120
        private System.Object ReadStringValue(Newtonsoft.Json.ReadType readType) { }
        // RVA: 0x03A6E710  token: 0x6000121
        private System.Object FinishReadQuotedStringValue(Newtonsoft.Json.ReadType readType) { }
        // RVA: 0x0A8AAA20  token: 0x6000122
        private Newtonsoft.Json.JsonReaderException CreateUnexpectedCharacterException(System.Char c) { }
        // RVA: 0x034288E0  token: 0x6000123
        public virtual System.Nullable<System.Boolean> ReadAsBoolean() { }
        // RVA: 0x0A8ABC84  token: 0x6000124
        private System.Void ProcessValueComma() { }
        // RVA: 0x02FBC540  token: 0x6000125
        private System.Object ReadNumberValue(Newtonsoft.Json.ReadType readType) { }
        // RVA: 0x0A8AAB08  token: 0x6000126
        private System.Object FinishReadQuotedNumber(Newtonsoft.Json.ReadType readType) { }
        // RVA: 0x0A8AC1A8  token: 0x6000127
        public virtual System.Nullable<System.DateTimeOffset> ReadAsDateTimeOffset() { }
        // RVA: 0x0A8AC2B8  token: 0x6000128
        public virtual System.Nullable<System.Decimal> ReadAsDecimal() { }
        // RVA: 0x02FBC3D0  token: 0x6000129
        public virtual System.Nullable<System.Double> ReadAsDouble() { }
        // RVA: 0x0A8AAC68  token: 0x600012A
        private System.Void HandleNull() { }
        // RVA: 0x0A8AC344  token: 0x600012B
        private System.Void ReadFinished() { }
        // RVA: 0x03429DB0  token: 0x600012C
        private System.Boolean ReadNullChar() { }
        // RVA: 0x02F04F00  token: 0x600012D
        private System.Void EnsureBuffer() { }
        // RVA: 0x02F06BF0  token: 0x600012E
        private System.Void ReadStringIntoBuffer(System.Char quote) { }
        // RVA: 0x02F04F80  token: 0x600012F
        private System.Void FinishReadStringIntoBuffer(System.Int32 charPos, System.Int32 initialPosition, System.Int32 lastWritePosition) { }
        // RVA: 0x04208C00  token: 0x6000130
        private System.Void WriteCharToBuffer(System.Char writeChar, System.Int32 lastWritePosition, System.Int32 writeToPosition) { }
        // RVA: 0x0A8AA890  token: 0x6000131
        private System.Char ConvertUnicode(System.Boolean enoughChars) { }
        // RVA: 0x0A8ABB48  token: 0x6000132
        private System.Char ParseUnicode() { }
        // RVA: 0x02F05CF0  token: 0x6000133
        private System.Void ReadNumberIntoBuffer() { }
        // RVA: 0x02F07660  token: 0x6000134
        private System.Boolean ReadNumberCharIntoBuffer(System.Char currentChar, System.Int32 charPos) { }
        // RVA: 0x04DA5C00  token: 0x6000135
        private System.Void ClearRecentString() { }
        // RVA: 0x02F05E20  token: 0x6000136
        private System.Boolean ParsePostValue(System.Boolean ignoreComments) { }
        // RVA: 0x02F06810  token: 0x6000137
        private System.Boolean ParseObject() { }
        // RVA: 0x02F05530  token: 0x6000138
        private System.Boolean ParseProperty() { }
        // RVA: 0x0A8AC644  token: 0x6000139
        private System.Boolean ValidIdentifierChar(System.Char value) { }
        // RVA: 0x0A8ABB74  token: 0x600013A
        private System.Void ParseUnquotedProperty() { }
        // RVA: 0x0A8AC494  token: 0x600013B
        private System.Boolean ReadUnquotedPropertyReportIfDone(System.Char currentChar, System.Int32 initialPosition) { }
        // RVA: 0x02FBB980  token: 0x600013C
        private System.Boolean ParseValue() { }
        // RVA: 0x04DA5C30  token: 0x600013D
        private System.Void ProcessLineFeed() { }
        // RVA: 0x02F05330  token: 0x600013E
        private System.Void ProcessCarriageReturn(System.Boolean append) { }
        // RVA: 0x02F03EE0  token: 0x600013F
        private System.Void EatWhitespace() { }
        // RVA: 0x0A8AB05C  token: 0x6000140
        private System.Void ParseConstructor() { }
        // RVA: 0x02FB86E0  token: 0x6000141
        private System.Void ParseNumber(Newtonsoft.Json.ReadType readType) { }
        // RVA: 0x02FB8F70  token: 0x6000142
        private System.Void ParseReadNumber(Newtonsoft.Json.ReadType readType, System.Char firstChar, System.Int32 initialPosition) { }
        // RVA: 0x0A8AC5F0  token: 0x6000143
        private Newtonsoft.Json.JsonReaderException ThrowReaderError(System.String message, System.Exception ex) { }
        // RVA: 0x0A8AA820  token: 0x6000144
        private static System.Object BigIntegerParse(System.String number, System.Globalization.CultureInfo culture) { }
        // RVA: 0x0A8AAD44  token: 0x6000145
        private System.Void ParseComment(System.Boolean setToken) { }
        // RVA: 0x0A8AAACC  token: 0x6000146
        private System.Void EndComment(System.Boolean setToken, System.Int32 initialPosition, System.Int32 endPosition) { }
        // RVA: 0x03429B30  token: 0x6000147
        private System.Boolean MatchValue(System.String value) { }
        // RVA: 0x03429D20  token: 0x6000148
        private System.Boolean MatchValue(System.Boolean enoughChars, System.String value) { }
        // RVA: 0x03429790  token: 0x6000149
        private System.Boolean MatchValueWithTrailingSeparator(System.String value) { }
        // RVA: 0x03429AE0  token: 0x600014A
        private System.Boolean IsSeparator(System.Char c) { }
        // RVA: 0x0A8AB9E0  token: 0x600014B
        private System.Void ParseTrue() { }
        // RVA: 0x0342A3B0  token: 0x600014C
        private System.Void ParseNull() { }
        // RVA: 0x0A8ABAA8  token: 0x600014D
        private System.Void ParseUndefined() { }
        // RVA: 0x03428810  token: 0x600014E
        private System.Void ParseFalse() { }
        // RVA: 0x0A8AB788  token: 0x600014F
        private System.Object ParseNumberNegativeInfinity(Newtonsoft.Json.ReadType readType) { }
        // RVA: 0x0A8AB610  token: 0x6000150
        private System.Object ParseNumberNegativeInfinity(Newtonsoft.Json.ReadType readType, System.Boolean matched) { }
        // RVA: 0x0A8AB970  token: 0x6000151
        private System.Object ParseNumberPositiveInfinity(Newtonsoft.Json.ReadType readType) { }
        // RVA: 0x0A8AB7F8  token: 0x6000152
        private System.Object ParseNumberPositiveInfinity(Newtonsoft.Json.ReadType readType, System.Boolean matched) { }
        // RVA: 0x0A8AB428  token: 0x6000153
        private System.Object ParseNumberNaN(Newtonsoft.Json.ReadType readType) { }
        // RVA: 0x0A8AB498  token: 0x6000154
        private System.Object ParseNumberNaN(Newtonsoft.Json.ReadType readType, System.Boolean matched) { }
        // RVA: 0x037785D0  token: 0x6000155
        public virtual System.Void Close() { }
        // RVA: 0x02FFF600  token: 0x6000156
        public virtual System.Boolean HasLineInfo() { }

    }

    // TypeToken: 0x200002D  // size: 0x14
    public sealed struct ReadType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.ReadType Read;  // const
        public static Newtonsoft.Json.ReadType ReadAsInt32;  // const
        public static Newtonsoft.Json.ReadType ReadAsInt64;  // const
        public static Newtonsoft.Json.ReadType ReadAsBytes;  // const
        public static Newtonsoft.Json.ReadType ReadAsString;  // const
        public static Newtonsoft.Json.ReadType ReadAsDecimal;  // const
        public static Newtonsoft.Json.ReadType ReadAsDateTime;  // const
        public static Newtonsoft.Json.ReadType ReadAsDateTimeOffset;  // const
        public static Newtonsoft.Json.ReadType ReadAsDouble;  // const
        public static Newtonsoft.Json.ReadType ReadAsBoolean;  // const

    }

    // TypeToken: 0x200002E  // size: 0xA8
    public class JsonTextWriter : Newtonsoft.Json.JsonWriter
    {
        // Fields
        private readonly System.Boolean _safeAsync;  // 0x60
        private readonly System.IO.TextWriter _writer;  // 0x68
        private Newtonsoft.Json.Utilities.Base64Encoder _base64Encoder;  // 0x70
        private System.Char _indentChar;  // 0x78
        private System.Int32 _indentation;  // 0x7c
        private System.Char _quoteChar;  // 0x80
        private System.Boolean _quoteName;  // 0x82
        private System.Boolean[] _charEscapeFlags;  // 0x88
        private System.Char[] _writeBuffer;  // 0x90
        private Newtonsoft.Json.IArrayPool<System.Char> _arrayPool;  // 0x98
        private System.Char[] _indentChars;  // 0xa0

        // Properties
        Newtonsoft.Json.Utilities.Base64Encoder Base64Encoder { get; /* RVA: 0x0A8AD770 */ }
        System.Char QuoteChar { get; /* RVA: 0x04DA5C60 */ }

        // Methods
        // RVA: 0x03425CE0  token: 0x600015B
        public System.Void .ctor(System.IO.TextWriter textWriter) { }
        // RVA: 0x03778720  token: 0x600015C
        public virtual System.Void Close() { }
        // RVA: 0x03778560  token: 0x600015D
        private System.Void CloseBufferAndWriter() { }
        // RVA: 0x03778CB0  token: 0x600015E
        public virtual System.Void WriteStartObject() { }
        // RVA: 0x03777750  token: 0x600015F
        public virtual System.Void WriteStartArray() { }
        // RVA: 0x0A8AC894  token: 0x6000160
        public virtual System.Void WriteStartConstructor(System.String name) { }
        // RVA: 0x03778CF0  token: 0x6000161
        protected virtual System.Void WriteEnd(Newtonsoft.Json.JsonToken token) { }
        // RVA: 0x03778B30  token: 0x6000162
        public virtual System.Void WritePropertyName(System.String name) { }
        // RVA: 0x03779260  token: 0x6000163
        public virtual System.Void WritePropertyName(System.String name, System.Boolean escape) { }
        // RVA: 0x0A8AC69C  token: 0x6000164
        private virtual System.Void OnStringEscapeHandlingChanged() { }
        // RVA: 0x03425DF0  token: 0x6000165
        private System.Void UpdateCharEscapeFlags() { }
        // RVA: 0x03779EA0  token: 0x6000166
        protected virtual System.Void WriteIndent() { }
        // RVA: 0x03779FD0  token: 0x6000167
        private System.Int32 SetIndentChars() { }
        // RVA: 0x03778E60  token: 0x6000168
        protected virtual System.Void WriteValueDelimiter() { }
        // RVA: 0x03778ED0  token: 0x6000169
        protected virtual System.Void WriteIndentSpace() { }
        // RVA: 0x03779620  token: 0x600016A
        private System.Void WriteValueInternal(System.String value, Newtonsoft.Json.JsonToken token) { }
        // RVA: 0x0A8ACCD8  token: 0x600016B
        public virtual System.Void WriteValue(System.Object value) { }
        // RVA: 0x0A8AC808  token: 0x600016C
        public virtual System.Void WriteNull() { }
        // RVA: 0x0A8AC92C  token: 0x600016D
        public virtual System.Void WriteUndefined() { }
        // RVA: 0x0A8AC870  token: 0x600016E
        public virtual System.Void WriteRaw(System.String json) { }
        // RVA: 0x03778B90  token: 0x600016F
        public virtual System.Void WriteValue(System.String value) { }
        // RVA: 0x03778D50  token: 0x6000170
        private System.Void WriteEscapedString(System.String value, System.Boolean quote) { }
        // RVA: 0x0A8ACDA0  token: 0x6000171
        public virtual System.Void WriteValue(System.Int32 value) { }
        // RVA: 0x0A8ACFD4  token: 0x6000172
        public virtual System.Void WriteValue(System.UInt32 value) { }
        // RVA: 0x03778AF0  token: 0x6000173
        public virtual System.Void WriteValue(System.Int64 value) { }
        // RVA: 0x0A8AD5F8  token: 0x6000174
        public virtual System.Void WriteValue(System.UInt64 value) { }
        // RVA: 0x0A8AD630  token: 0x6000175
        public virtual System.Void WriteValue(System.Single value) { }
        // RVA: 0x0A8AD6AC  token: 0x6000176
        public virtual System.Void WriteValue(System.Nullable<System.Single> value) { }
        // RVA: 0x0A8AD560  token: 0x6000177
        public virtual System.Void WriteValue(System.Double value) { }
        // RVA: 0x0A8ACDD4  token: 0x6000178
        public virtual System.Void WriteValue(System.Nullable<System.Double> value) { }
        // RVA: 0x03778BE0  token: 0x6000179
        public virtual System.Void WriteValue(System.Boolean value) { }
        // RVA: 0x0A8ACE9C  token: 0x600017A
        public virtual System.Void WriteValue(System.Int16 value) { }
        // RVA: 0x0A8ACFA0  token: 0x600017B
        public virtual System.Void WriteValue(System.UInt16 value) { }
        // RVA: 0x0A8AD4F0  token: 0x600017C
        public virtual System.Void WriteValue(System.Char value) { }
        // RVA: 0x0A8AD4BC  token: 0x600017D
        public virtual System.Void WriteValue(System.Byte value) { }
        // RVA: 0x0A8AD254  token: 0x600017E
        public virtual System.Void WriteValue(System.SByte value) { }
        // RVA: 0x0A8AD144  token: 0x600017F
        public virtual System.Void WriteValue(System.Decimal value) { }
        // RVA: 0x0A8AD288  token: 0x6000180
        public virtual System.Void WriteValue(System.DateTime value) { }
        // RVA: 0x0A8ACAF8  token: 0x6000181
        private System.Int32 WriteValueToBuffer(System.DateTime value) { }
        // RVA: 0x0A8ACC1C  token: 0x6000182
        public virtual System.Void WriteValue(System.Byte[] value) { }
        // RVA: 0x0A8AD008  token: 0x6000183
        public virtual System.Void WriteValue(System.DateTimeOffset value) { }
        // RVA: 0x0A8AC994  token: 0x6000184
        private System.Int32 WriteValueToBuffer(System.DateTimeOffset value) { }
        // RVA: 0x0A8ACED0  token: 0x6000185
        public virtual System.Void WriteValue(System.Guid value) { }
        // RVA: 0x0A8AD3E0  token: 0x6000186
        public virtual System.Void WriteValue(System.TimeSpan value) { }
        // RVA: 0x0A8AD1BC  token: 0x6000187
        public virtual System.Void WriteValue(System.Uri value) { }
        // RVA: 0x0A8AC6A4  token: 0x6000188
        public virtual System.Void WriteComment(System.String text) { }
        // RVA: 0x03778E10  token: 0x6000189
        private System.Void EnsureWriteBuffer() { }
        // RVA: 0x03779650  token: 0x600018A
        private System.Void WriteIntegerValue(System.Int64 value) { }
        // RVA: 0x03778940  token: 0x600018B
        private System.Void WriteIntegerValue(System.UInt64 value, System.Boolean negative) { }
        // RVA: 0x037789D0  token: 0x600018C
        private System.Int32 WriteNumberToBuffer(System.UInt64 value, System.Boolean negative) { }
        // RVA: 0x0A8AC7B4  token: 0x600018D
        private System.Void WriteIntegerValue(System.Int32 value) { }
        // RVA: 0x0A8AC740  token: 0x600018E
        private System.Void WriteIntegerValue(System.UInt32 value, System.Boolean negative) { }
        // RVA: 0x03778A10  token: 0x600018F
        private System.Int32 WriteNumberToBuffer(System.UInt32 value, System.Boolean negative) { }

    }

    // TypeToken: 0x200002F  // size: 0x14
    public sealed struct JsonToken
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.JsonToken None;  // const
        public static Newtonsoft.Json.JsonToken StartObject;  // const
        public static Newtonsoft.Json.JsonToken StartArray;  // const
        public static Newtonsoft.Json.JsonToken StartConstructor;  // const
        public static Newtonsoft.Json.JsonToken PropertyName;  // const
        public static Newtonsoft.Json.JsonToken Comment;  // const
        public static Newtonsoft.Json.JsonToken Raw;  // const
        public static Newtonsoft.Json.JsonToken Integer;  // const
        public static Newtonsoft.Json.JsonToken Float;  // const
        public static Newtonsoft.Json.JsonToken String;  // const
        public static Newtonsoft.Json.JsonToken Boolean;  // const
        public static Newtonsoft.Json.JsonToken Null;  // const
        public static Newtonsoft.Json.JsonToken Undefined;  // const
        public static Newtonsoft.Json.JsonToken EndObject;  // const
        public static Newtonsoft.Json.JsonToken EndArray;  // const
        public static Newtonsoft.Json.JsonToken EndConstructor;  // const
        public static Newtonsoft.Json.JsonToken Date;  // const
        public static Newtonsoft.Json.JsonToken Bytes;  // const

    }

    // TypeToken: 0x2000030  // size: 0x60
    public abstract class JsonWriter : System.IDisposable
    {
        // Fields
        private static readonly Newtonsoft.Json.JsonWriter.State[][] StateArray;  // static @ 0x0
        private static readonly Newtonsoft.Json.JsonWriter.State[][] StateArrayTemplate;  // static @ 0x8
        private System.Collections.Generic.List<Newtonsoft.Json.JsonPosition> _stack;  // 0x10
        private Newtonsoft.Json.JsonPosition _currentPosition;  // 0x18
        private Newtonsoft.Json.JsonWriter.State _currentState;  // 0x30
        private Newtonsoft.Json.Formatting _formatting;  // 0x34
        private System.Boolean <CloseOutput>k__BackingField;  // 0x38
        private System.Boolean <AutoCompleteOnClose>k__BackingField;  // 0x39
        private Newtonsoft.Json.DateFormatHandling _dateFormatHandling;  // 0x3c
        private Newtonsoft.Json.DateTimeZoneHandling _dateTimeZoneHandling;  // 0x40
        private Newtonsoft.Json.StringEscapeHandling _stringEscapeHandling;  // 0x44
        private Newtonsoft.Json.FloatFormatHandling _floatFormatHandling;  // 0x48
        private System.String _dateFormatString;  // 0x50
        private System.Globalization.CultureInfo _culture;  // 0x58

        // Properties
        System.Boolean CloseOutput { get; /* RVA: 0x011F2A70 */ set; /* RVA: 0x011F2A80 */ }
        System.Boolean AutoCompleteOnClose { get; /* RVA: 0x04D86650 */ set; /* RVA: 0x04D86680 */ }
        System.Int32 Top { get; /* RVA: 0x03779FA0 */ }
        Newtonsoft.Json.WriteState WriteState { get; /* RVA: 0x0A8AE7E8 */ }
        System.String ContainerPath { get; /* RVA: 0x0A8AE670 */ }
        System.String Path { get; /* RVA: 0x0A8AE6F4 */ }
        Newtonsoft.Json.Formatting Formatting { get; /* RVA: 0x04D86730 */ set; /* RVA: 0x03425B10 */ }
        Newtonsoft.Json.DateFormatHandling DateFormatHandling { get; /* RVA: 0x04D86640 */ set; /* RVA: 0x0A8AE8EC */ }
        Newtonsoft.Json.DateTimeZoneHandling DateTimeZoneHandling { get; /* RVA: 0x04D865E0 */ set; /* RVA: 0x0A8AE954 */ }
        Newtonsoft.Json.StringEscapeHandling StringEscapeHandling { get; /* RVA: 0x04D86530 */ set; /* RVA: 0x0A8AEA24 */ }
        Newtonsoft.Json.FloatFormatHandling FloatFormatHandling { get; /* RVA: 0x04D863D0 */ set; /* RVA: 0x0A8AE9BC */ }
        System.String DateFormatString { get; /* RVA: 0x04D86270 */ set; /* RVA: 0x02FDAEA0 */ }
        System.Globalization.CultureInfo Culture { get; /* RVA: 0x03F71FB0 */ set; /* RVA: 0x036E5590 */ }

        // Methods
        // RVA: 0x037D88D0  token: 0x6000190
        private static Newtonsoft.Json.JsonWriter.State[][] BuildStateArray() { }
        // RVA: 0x037D9EE0  token: 0x6000191
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x60001A2
        private virtual System.Void OnStringEscapeHandlingChanged() { }
        // RVA: 0x04DA5C70  token: 0x60001A9
        protected System.Void .ctor() { }
        // RVA: 0x03779390  token: 0x60001AA
        private System.Void UpdateScopeWithFinishedValue() { }
        // RVA: 0x037794C0  token: 0x60001AB
        private System.Void Push(Newtonsoft.Json.JsonContainerType value) { }
        // RVA: 0x03637DA0  token: 0x60001AC
        private Newtonsoft.Json.JsonContainerType Pop() { }
        // RVA: 0x011EF5B0  token: 0x60001AD
        private Newtonsoft.Json.JsonContainerType Peek() { }
        // RVA: 0x03779F50  token: 0x60001AE
        public virtual System.Void Close() { }
        // RVA: 0x053924C4  token: 0x60001AF
        public virtual System.Void WriteStartObject() { }
        // RVA: 0x047A0930  token: 0x60001B0
        public virtual System.Void WriteEndObject() { }
        // RVA: 0x053924FC  token: 0x60001B1
        public virtual System.Void WriteStartArray() { }
        // RVA: 0x047A0920  token: 0x60001B2
        public virtual System.Void WriteEndArray() { }
        // RVA: 0x0A8ADD7C  token: 0x60001B3
        public virtual System.Void WriteStartConstructor(System.String name) { }
        // RVA: 0x0A8ADC50  token: 0x60001B4
        public virtual System.Void WriteEndConstructor() { }
        // RVA: 0x05392558  token: 0x60001B5
        public virtual System.Void WritePropertyName(System.String name) { }
        // RVA: 0x04CA3170  token: 0x60001B6
        public virtual System.Void WritePropertyName(System.String name, System.Boolean escape) { }
        // RVA: 0x0A8ADC60  token: 0x60001B7
        public virtual System.Void WriteEnd() { }
        // RVA: 0x05392F1C  token: 0x60001B8
        public System.Void WriteToken(Newtonsoft.Json.JsonReader reader) { }
        // RVA: 0x03424D10  token: 0x60001B9
        public System.Void WriteToken(Newtonsoft.Json.JsonReader reader, System.Boolean writeChildren) { }
        // RVA: 0x03424FF0  token: 0x60001BA
        public System.Void WriteToken(Newtonsoft.Json.JsonToken token, System.Object value) { }
        // RVA: 0x03424E90  token: 0x60001BB
        private virtual System.Void WriteToken(Newtonsoft.Json.JsonReader reader, System.Boolean writeChildren, System.Boolean writeDateConstructorAsDate, System.Boolean writeComments) { }
        // RVA: 0x03424DE0  token: 0x60001BC
        private System.Boolean IsWriteTokenIncomplete(Newtonsoft.Json.JsonReader reader, System.Boolean writeChildren, System.Int32 initialDepth) { }
        // RVA: 0x03425290  token: 0x60001BD
        private System.Int32 CalculateWriteTokenInitialDepth(Newtonsoft.Json.JsonReader reader) { }
        // RVA: 0x03424E30  token: 0x60001BE
        private System.Int32 CalculateWriteTokenFinalDepth(Newtonsoft.Json.JsonReader reader) { }
        // RVA: 0x0A8ADBA8  token: 0x60001BF
        private System.Void WriteConstructorDate(Newtonsoft.Json.JsonReader reader) { }
        // RVA: 0x0A8ADC6C  token: 0x60001C0
        private System.Void WriteEnd(Newtonsoft.Json.JsonContainerType type) { }
        // RVA: 0x03779F60  token: 0x60001C1
        private System.Void AutoCompleteAll() { }
        // RVA: 0x03637D60  token: 0x60001C2
        private Newtonsoft.Json.JsonToken GetCloseTokenForType(Newtonsoft.Json.JsonContainerType type) { }
        // RVA: 0x03637C80  token: 0x60001C3
        private System.Void AutoCompleteClose(Newtonsoft.Json.JsonContainerType type) { }
        // RVA: 0x03637D20  token: 0x60001C4
        private System.Int32 CalculateLevelsToComplete(Newtonsoft.Json.JsonContainerType type) { }
        // RVA: 0x03637E70  token: 0x60001C5
        private System.Void UpdateCurrentState() { }
        // RVA: 0x041E1670  token: 0x60001C6
        protected virtual System.Void WriteEnd(Newtonsoft.Json.JsonToken token) { }
        // RVA: 0x041E1670  token: 0x60001C7
        protected virtual System.Void WriteIndent() { }
        // RVA: 0x041E1670  token: 0x60001C8
        protected virtual System.Void WriteValueDelimiter() { }
        // RVA: 0x041E1670  token: 0x60001C9
        protected virtual System.Void WriteIndentSpace() { }
        // RVA: 0x037793A0  token: 0x60001CA
        private System.Void AutoComplete(Newtonsoft.Json.JsonToken tokenBeingWritten) { }
        // RVA: 0x0A8ADD2C  token: 0x60001CB
        public virtual System.Void WriteNull() { }
        // RVA: 0x0A8ADD8C  token: 0x60001CC
        public virtual System.Void WriteUndefined() { }
        // RVA: 0x041E1670  token: 0x60001CD
        public virtual System.Void WriteRaw(System.String json) { }
        // RVA: 0x0A8ADD3C  token: 0x60001CE
        public virtual System.Void WriteRawValue(System.String json) { }
        // RVA: 0x05392548  token: 0x60001CF
        public virtual System.Void WriteValue(System.String value) { }
        // RVA: 0x05392528  token: 0x60001D0
        public virtual System.Void WriteValue(System.Int32 value) { }
        // RVA: 0x05392528  token: 0x60001D1
        public virtual System.Void WriteValue(System.UInt32 value) { }
        // RVA: 0x05392528  token: 0x60001D2
        public virtual System.Void WriteValue(System.Int64 value) { }
        // RVA: 0x05392528  token: 0x60001D3
        public virtual System.Void WriteValue(System.UInt64 value) { }
        // RVA: 0x0A8ADF58  token: 0x60001D4
        public virtual System.Void WriteValue(System.Single value) { }
        // RVA: 0x0A8ADF58  token: 0x60001D5
        public virtual System.Void WriteValue(System.Double value) { }
        // RVA: 0x05392560  token: 0x60001D6
        public virtual System.Void WriteValue(System.Boolean value) { }
        // RVA: 0x05392528  token: 0x60001D7
        public virtual System.Void WriteValue(System.Int16 value) { }
        // RVA: 0x05392528  token: 0x60001D8
        public virtual System.Void WriteValue(System.UInt16 value) { }
        // RVA: 0x05392548  token: 0x60001D9
        public virtual System.Void WriteValue(System.Char value) { }
        // RVA: 0x05392528  token: 0x60001DA
        public virtual System.Void WriteValue(System.Byte value) { }
        // RVA: 0x05392528  token: 0x60001DB
        public virtual System.Void WriteValue(System.SByte value) { }
        // RVA: 0x0A8ADF58  token: 0x60001DC
        public virtual System.Void WriteValue(System.Decimal value) { }
        // RVA: 0x0A8ADE08  token: 0x60001DD
        public virtual System.Void WriteValue(System.DateTime value) { }
        // RVA: 0x0A8ADE08  token: 0x60001DE
        public virtual System.Void WriteValue(System.DateTimeOffset value) { }
        // RVA: 0x05392548  token: 0x60001DF
        public virtual System.Void WriteValue(System.Guid value) { }
        // RVA: 0x05392548  token: 0x60001E0
        public virtual System.Void WriteValue(System.TimeSpan value) { }
        // RVA: 0x0A8AE52C  token: 0x60001E1
        public virtual System.Void WriteValue(System.Nullable<System.Int32> value) { }
        // RVA: 0x0A8AE034  token: 0x60001E2
        public virtual System.Void WriteValue(System.Nullable<System.UInt32> value) { }
        // RVA: 0x0A8AE2B4  token: 0x60001E3
        public virtual System.Void WriteValue(System.Nullable<System.Int64> value) { }
        // RVA: 0x0A8AE174  token: 0x60001E4
        public virtual System.Void WriteValue(System.Nullable<System.UInt64> value) { }
        // RVA: 0x0A8ADF68  token: 0x60001E5
        public virtual System.Void WriteValue(System.Nullable<System.Single> value) { }
        // RVA: 0x0A8AE0A0  token: 0x60001E6
        public virtual System.Void WriteValue(System.Nullable<System.Double> value) { }
        // RVA: 0x0A8AE48C  token: 0x60001E7
        public virtual System.Void WriteValue(System.Nullable<System.Boolean> value) { }
        // RVA: 0x0A8AE1DC  token: 0x60001E8
        public virtual System.Void WriteValue(System.Nullable<System.Int16> value) { }
        // RVA: 0x0A8AE604  token: 0x60001E9
        public virtual System.Void WriteValue(System.Nullable<System.UInt16> value) { }
        // RVA: 0x0A8ADD9C  token: 0x60001EA
        public virtual System.Void WriteValue(System.Nullable<System.Char> value) { }
        // RVA: 0x0A8AE108  token: 0x60001EB
        public virtual System.Void WriteValue(System.Nullable<System.Byte> value) { }
        // RVA: 0x0A8ADEF0  token: 0x60001EC
        public virtual System.Void WriteValue(System.Nullable<System.SByte> value) { }
        // RVA: 0x0A8ADE80  token: 0x60001ED
        public virtual System.Void WriteValue(System.Nullable<System.Decimal> value) { }
        // RVA: 0x0A8ADE18  token: 0x60001EE
        public virtual System.Void WriteValue(System.Nullable<System.DateTime> value) { }
        // RVA: 0x0A8AE248  token: 0x60001EF
        public virtual System.Void WriteValue(System.Nullable<System.DateTimeOffset> value) { }
        // RVA: 0x0A8AE598  token: 0x60001F0
        public virtual System.Void WriteValue(System.Nullable<System.Guid> value) { }
        // RVA: 0x0A8ADFD4  token: 0x60001F1
        public virtual System.Void WriteValue(System.Nullable<System.TimeSpan> value) { }
        // RVA: 0x0A8AE4F8  token: 0x60001F2
        public virtual System.Void WriteValue(System.Byte[] value) { }
        // RVA: 0x0A8AE31C  token: 0x60001F3
        public virtual System.Void WriteValue(System.Uri value) { }
        // RVA: 0x0A8AE390  token: 0x60001F4
        public virtual System.Void WriteValue(System.Object value) { }
        // RVA: 0x0A8AD894  token: 0x60001F5
        public virtual System.Void WriteComment(System.String text) { }
        // RVA: 0x042EF170  token: 0x60001F6
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x042EF1B0  token: 0x60001F7
        protected virtual System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x0377F170  token: 0x60001F8
        private static System.Void WriteValue(Newtonsoft.Json.JsonWriter writer, Newtonsoft.Json.Utilities.PrimitiveTypeCode typeCode, System.Object value) { }
        // RVA: 0x0A8AD8A4  token: 0x60001F9
        private static System.Void ResolveConvertibleValue(System.IConvertible convertible, Newtonsoft.Json.Utilities.PrimitiveTypeCode& typeCode, System.Object& value) { }
        // RVA: 0x0A8AD7F4  token: 0x60001FA
        private static Newtonsoft.Json.JsonWriterException CreateUnsupportedTypeException(Newtonsoft.Json.JsonWriter writer, System.Object value) { }
        // RVA: 0x0A8AD99C  token: 0x60001FB
        protected System.Void SetWriteState(Newtonsoft.Json.JsonToken token, System.Object value) { }
        // RVA: 0x05397C18  token: 0x60001FC
        private System.Void InternalWriteEnd(Newtonsoft.Json.JsonContainerType container) { }
        // RVA: 0x03779340  token: 0x60001FD
        private System.Void InternalWritePropertyName(System.String name) { }
        // RVA: 0x041E1670  token: 0x60001FE
        private System.Void InternalWriteRaw() { }
        // RVA: 0x03778E90  token: 0x60001FF
        private System.Void InternalWriteStart(Newtonsoft.Json.JsonToken token, Newtonsoft.Json.JsonContainerType container) { }
        // RVA: 0x03779370  token: 0x6000200
        private System.Void InternalWriteValue(Newtonsoft.Json.JsonToken token) { }
        // RVA: 0x0A8AD894  token: 0x6000201
        private System.Void InternalWriteComment() { }

    }

    // TypeToken: 0x2000032  // size: 0x98
    public class JsonWriterException : Newtonsoft.Json.JsonException
    {
        // Fields
        private readonly System.String <Path>k__BackingField;  // 0x90

        // Methods
        // RVA: 0x0A8A78C8  token: 0x6000202
        public System.Void .ctor() { }
        // RVA: 0x0A8A7928  token: 0x6000203
        public System.Void .ctor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x0A8B7E20  token: 0x6000204
        public System.Void .ctor(System.String message, System.String path, System.Exception innerException) { }
        // RVA: 0x0A8B7DE0  token: 0x6000205
        private static Newtonsoft.Json.JsonWriterException Create(Newtonsoft.Json.JsonWriter writer, System.String message, System.Exception ex) { }
        // RVA: 0x0A8B7D30  token: 0x6000206
        private static Newtonsoft.Json.JsonWriterException Create(System.String path, System.String message, System.Exception ex) { }

    }

    // TypeToken: 0x2000033  // size: 0x14
    public sealed struct MemberSerialization
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.MemberSerialization OptOut;  // const
        public static Newtonsoft.Json.MemberSerialization OptIn;  // const
        public static Newtonsoft.Json.MemberSerialization Fields;  // const

    }

    // TypeToken: 0x2000034  // size: 0x14
    public sealed struct MetadataPropertyHandling
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.MetadataPropertyHandling Default;  // const
        public static Newtonsoft.Json.MetadataPropertyHandling ReadAhead;  // const
        public static Newtonsoft.Json.MetadataPropertyHandling Ignore;  // const

    }

    // TypeToken: 0x2000035  // size: 0x14
    public sealed struct MissingMemberHandling
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.MissingMemberHandling Ignore;  // const
        public static Newtonsoft.Json.MissingMemberHandling Error;  // const

    }

    // TypeToken: 0x2000036  // size: 0x14
    public sealed struct NullValueHandling
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.NullValueHandling Include;  // const
        public static Newtonsoft.Json.NullValueHandling Ignore;  // const

    }

    // TypeToken: 0x2000037  // size: 0x14
    public sealed struct ObjectCreationHandling
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.ObjectCreationHandling Auto;  // const
        public static Newtonsoft.Json.ObjectCreationHandling Reuse;  // const
        public static Newtonsoft.Json.ObjectCreationHandling Replace;  // const

    }

    // TypeToken: 0x2000038  // size: 0x14
    public sealed struct PreserveReferencesHandling
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.PreserveReferencesHandling None;  // const
        public static Newtonsoft.Json.PreserveReferencesHandling Objects;  // const
        public static Newtonsoft.Json.PreserveReferencesHandling Arrays;  // const
        public static Newtonsoft.Json.PreserveReferencesHandling All;  // const

    }

    // TypeToken: 0x2000039  // size: 0x14
    public sealed struct ReferenceLoopHandling
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.ReferenceLoopHandling Error;  // const
        public static Newtonsoft.Json.ReferenceLoopHandling Ignore;  // const
        public static Newtonsoft.Json.ReferenceLoopHandling Serialize;  // const

    }

    // TypeToken: 0x200003A  // size: 0x14
    public sealed struct Required
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.Required Default;  // const
        public static Newtonsoft.Json.Required AllowNull;  // const
        public static Newtonsoft.Json.Required Always;  // const
        public static Newtonsoft.Json.Required DisallowNull;  // const

    }

    // TypeToken: 0x200003B  // size: 0x14
    public sealed struct StringEscapeHandling
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.StringEscapeHandling Default;  // const
        public static Newtonsoft.Json.StringEscapeHandling EscapeNonAscii;  // const
        public static Newtonsoft.Json.StringEscapeHandling EscapeHtml;  // const

    }

    // TypeToken: 0x200003C  // size: 0x14
    public sealed struct TypeNameAssemblyFormatHandling
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.TypeNameAssemblyFormatHandling Simple;  // const
        public static Newtonsoft.Json.TypeNameAssemblyFormatHandling Full;  // const

    }

    // TypeToken: 0x200003D  // size: 0x14
    public sealed struct TypeNameHandling
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.TypeNameHandling None;  // const
        public static Newtonsoft.Json.TypeNameHandling Objects;  // const
        public static Newtonsoft.Json.TypeNameHandling Arrays;  // const
        public static Newtonsoft.Json.TypeNameHandling All;  // const
        public static Newtonsoft.Json.TypeNameHandling Auto;  // const

    }

    // TypeToken: 0x200003E  // size: 0x14
    public sealed struct WriteState
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.WriteState Error;  // const
        public static Newtonsoft.Json.WriteState Closed;  // const
        public static Newtonsoft.Json.WriteState Object;  // const
        public static Newtonsoft.Json.WriteState Array;  // const
        public static Newtonsoft.Json.WriteState Constructor;  // const
        public static Newtonsoft.Json.WriteState Property;  // const
        public static Newtonsoft.Json.WriteState Start;  // const

    }

}

namespace Newtonsoft.Json.Bson
{

    // TypeToken: 0x2000122  // size: 0x18
    public class BsonObjectId
    {
        // Fields
        private readonly System.Byte[] <Value>k__BackingField;  // 0x10

        // Properties
        System.Byte[] Value { get; /* RVA: 0x02B2ECC0 */ }

        // Methods
        // RVA: 0x0A8D36E0  token: 0x6000960
        public System.Void .ctor(System.Byte[] value) { }

    }

    // TypeToken: 0x2000123  // size: 0x18
    public abstract class BsonToken
    {
        // Fields
        private Newtonsoft.Json.Bson.BsonToken <Parent>k__BackingField;  // 0x10

        // Properties
        Newtonsoft.Json.Bson.BsonType Type { get; /* RVA: -1  // abstract */ }
        Newtonsoft.Json.Bson.BsonToken Parent { set; /* RVA: 0x053908C0 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000963
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000124  // size: 0x20
    public class BsonObject : Newtonsoft.Json.Bson.BsonToken
    {
        // Fields
        private readonly System.Collections.Generic.List<Newtonsoft.Json.Bson.BsonProperty> _children;  // 0x18

        // Properties
        Newtonsoft.Json.Bson.BsonType Type { get; /* RVA: 0x04D91FD0 */ }

        // Methods
        // RVA: 0x0A8D37E0  token: 0x6000964
        public System.Void Add(System.String name, Newtonsoft.Json.Bson.BsonToken token) { }

    }

    // TypeToken: 0x2000125  // size: 0x20
    public class BsonArray : Newtonsoft.Json.Bson.BsonToken
    {
        // Fields
        private readonly System.Collections.Generic.List<Newtonsoft.Json.Bson.BsonToken> _children;  // 0x18

        // Properties
        Newtonsoft.Json.Bson.BsonType Type { get; /* RVA: 0x04D92120 */ }

        // Methods
        // RVA: 0x0A8D3484  token: 0x6000966
        public System.Void Add(Newtonsoft.Json.Bson.BsonToken token) { }

    }

    // TypeToken: 0x2000126  // size: 0x28
    public class BsonValue : Newtonsoft.Json.Bson.BsonToken
    {
        // Fields
        private readonly System.Object _value;  // 0x18
        private readonly Newtonsoft.Json.Bson.BsonType _type;  // 0x20

        // Properties
        Newtonsoft.Json.Bson.BsonType Type { get; /* RVA: 0x04D866B0 */ }

        // Methods
        // RVA: 0x094E5F8C  token: 0x6000968
        public System.Void .ctor(System.Object value, Newtonsoft.Json.Bson.BsonType type) { }

    }

    // TypeToken: 0x2000127  // size: 0x30
    public class BsonString : Newtonsoft.Json.Bson.BsonValue
    {
        // Fields
        private readonly System.Boolean <IncludeLength>k__BackingField;  // 0x28

        // Methods
        // RVA: 0x0A8D3990  token: 0x600096A
        public System.Void .ctor(System.Object value, System.Boolean includeLength) { }

    }

    // TypeToken: 0x2000128  // size: 0x28
    public class BsonRegex : Newtonsoft.Json.Bson.BsonToken
    {
        // Fields
        private Newtonsoft.Json.Bson.BsonString <Pattern>k__BackingField;  // 0x18
        private Newtonsoft.Json.Bson.BsonString <Options>k__BackingField;  // 0x20

        // Properties
        Newtonsoft.Json.Bson.BsonString Pattern { set; /* RVA: 0x05392C40 */ }
        Newtonsoft.Json.Bson.BsonString Options { set; /* RVA: 0x05390F40 */ }
        Newtonsoft.Json.Bson.BsonType Type { get; /* RVA: 0x04DA5E70 */ }

        // Methods
        // RVA: 0x0A8D38D8  token: 0x600096D
        public System.Void .ctor(System.String pattern, System.String options) { }

    }

    // TypeToken: 0x2000129  // size: 0x20
    public class BsonProperty
    {
        // Fields
        private Newtonsoft.Json.Bson.BsonString <Name>k__BackingField;  // 0x10
        private Newtonsoft.Json.Bson.BsonToken <Value>k__BackingField;  // 0x18

        // Properties
        Newtonsoft.Json.Bson.BsonString Name { set; /* RVA: 0x053908C0 */ }
        Newtonsoft.Json.Bson.BsonToken Value { set; /* RVA: 0x05392C40 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000971
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200012A  // size: 0x11
    public sealed struct BsonType
    {
        // Fields
        public System.SByte value__;  // 0x10
        public static Newtonsoft.Json.Bson.BsonType Number;  // const
        public static Newtonsoft.Json.Bson.BsonType String;  // const
        public static Newtonsoft.Json.Bson.BsonType Object;  // const
        public static Newtonsoft.Json.Bson.BsonType Array;  // const
        public static Newtonsoft.Json.Bson.BsonType Binary;  // const
        public static Newtonsoft.Json.Bson.BsonType Undefined;  // const
        public static Newtonsoft.Json.Bson.BsonType Oid;  // const
        public static Newtonsoft.Json.Bson.BsonType Boolean;  // const
        public static Newtonsoft.Json.Bson.BsonType Date;  // const
        public static Newtonsoft.Json.Bson.BsonType Null;  // const
        public static Newtonsoft.Json.Bson.BsonType Regex;  // const
        public static Newtonsoft.Json.Bson.BsonType Reference;  // const
        public static Newtonsoft.Json.Bson.BsonType Code;  // const
        public static Newtonsoft.Json.Bson.BsonType Symbol;  // const
        public static Newtonsoft.Json.Bson.BsonType CodeWScope;  // const
        public static Newtonsoft.Json.Bson.BsonType Integer;  // const
        public static Newtonsoft.Json.Bson.BsonType TimeStamp;  // const
        public static Newtonsoft.Json.Bson.BsonType Long;  // const
        public static Newtonsoft.Json.Bson.BsonType MinKey;  // const
        public static Newtonsoft.Json.Bson.BsonType MaxKey;  // const

    }

    // TypeToken: 0x200012B  // size: 0x78
    public class BsonWriter : Newtonsoft.Json.JsonWriter
    {
        // Fields
        private Newtonsoft.Json.Bson.BsonToken _root;  // 0x60
        private Newtonsoft.Json.Bson.BsonToken _parent;  // 0x68
        private System.String _propertyName;  // 0x70

        // Methods
        // RVA: 0x0A8D3B54  token: 0x6000972
        private System.Void AddValue(System.Object value, Newtonsoft.Json.Bson.BsonType type) { }
        // RVA: 0x0A8D39B8  token: 0x6000973
        private System.Void AddToken(Newtonsoft.Json.Bson.BsonToken token) { }
        // RVA: 0x0A8D3BDC  token: 0x6000974
        public System.Void WriteObjectId(System.Byte[] value) { }
        // RVA: 0x0A8D3CC8  token: 0x6000975
        public System.Void WriteRegex(System.String pattern, System.String options) { }

    }

}

namespace Newtonsoft.Json.Converters
{

    // TypeToken: 0x20000F9  // size: 0x10
    public class BinaryConverter : Newtonsoft.Json.JsonConverter
    {
        // Fields
        private static System.String BinaryTypeName;  // const
        private static System.String BinaryToArrayName;  // const
        private static Newtonsoft.Json.Utilities.ReflectionObject _reflectionObject;  // static @ 0x0

        // Methods
        // RVA: 0x0A8C2EAC  token: 0x6000834
        public virtual System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x0A8C26EC  token: 0x6000835
        private System.Byte[] GetByteArray(System.Object value) { }
        // RVA: 0x0A8C2584  token: 0x6000836
        private static System.Void EnsureReflectionObject(System.Type t) { }
        // RVA: 0x0A8C2AC8  token: 0x6000837
        public virtual System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x0A8C28B0  token: 0x6000838
        private System.Byte[] ReadByteArray(Newtonsoft.Json.JsonReader reader) { }
        // RVA: 0x04048480  token: 0x6000839
        public virtual System.Boolean CanConvert(System.Type objectType) { }
        // RVA: 0x041E1670  token: 0x600083A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000FA  // size: 0x10
    public class BsonObjectIdConverter : Newtonsoft.Json.JsonConverter
    {
        // Methods
        // RVA: 0x0A8D3640  token: 0x600083B
        public virtual System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x0A8D34EC  token: 0x600083C
        public virtual System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x04239130  token: 0x600083D
        public virtual System.Boolean CanConvert(System.Type objectType) { }
        // RVA: 0x041E1670  token: 0x600083E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000FB
    public abstract class CustomCreationConverter`1 : Newtonsoft.Json.JsonConverter
    {
        // Properties
        System.Boolean CanWrite { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600083F
        public virtual System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: -1  // not resolved  token: 0x6000840
        public virtual System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: -1  // abstract  token: 0x6000841
        public virtual T Create(System.Type objectType) { }
        // RVA: -1  // not resolved  token: 0x6000842
        public virtual System.Boolean CanConvert(System.Type objectType) { }
        // RVA: -1  // not resolved  token: 0x6000844
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000FC  // size: 0x10
    public class DataSetConverter : Newtonsoft.Json.JsonConverter
    {
        // Methods
        // RVA: 0x0A8D3FDC  token: 0x6000845
        public virtual System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x0A8D3DBC  token: 0x6000846
        public virtual System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x041D0660  token: 0x6000847
        public virtual System.Boolean CanConvert(System.Type valueType) { }
        // RVA: 0x041E1670  token: 0x6000848
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000FD  // size: 0x10
    public class DataTableConverter : Newtonsoft.Json.JsonConverter
    {
        // Methods
        // RVA: 0x0A8D4AF0  token: 0x6000849
        public virtual System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x0A8D4898  token: 0x600084A
        public virtual System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x0A8D42D8  token: 0x600084B
        private static System.Void CreateRow(Newtonsoft.Json.JsonReader reader, System.Data.DataTable dt, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x0A8D46C4  token: 0x600084C
        private static System.Type GetColumnDataType(Newtonsoft.Json.JsonReader reader) { }
        // RVA: 0x041D0600  token: 0x600084D
        public virtual System.Boolean CanConvert(System.Type valueType) { }
        // RVA: 0x041E1670  token: 0x600084E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000FE  // size: 0x10
    public abstract class DateTimeConverterBase : Newtonsoft.Json.JsonConverter
    {
        // Methods
        // RVA: 0x0A8D4FA0  token: 0x600084F
        public virtual System.Boolean CanConvert(System.Type objectType) { }
        // RVA: 0x041E1670  token: 0x6000850
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000FF  // size: 0x10
    public class DiscriminatedUnionConverter : Newtonsoft.Json.JsonConverter
    {
        // Fields
        private static System.String CasePropertyName;  // const
        private static System.String FieldsPropertyName;  // const
        private static readonly Newtonsoft.Json.Utilities.ThreadSafeStore<System.Type,Newtonsoft.Json.Converters.DiscriminatedUnionConverter.Union> UnionCache;  // static @ 0x0
        private static readonly Newtonsoft.Json.Utilities.ThreadSafeStore<System.Type,System.Type> UnionTypeLookupCache;  // static @ 0x8

        // Methods
        // RVA: 0x0A8D5094  token: 0x6000851
        private static System.Type CreateUnionTypeLookup(System.Type t) { }
        // RVA: 0x0A8D51B8  token: 0x6000852
        private static Newtonsoft.Json.Converters.DiscriminatedUnionConverter.Union CreateUnion(System.Type t) { }
        // RVA: 0x0A8D5CD8  token: 0x6000853
        public virtual System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x0A8D5640  token: 0x6000854
        public virtual System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x03FAC670  token: 0x6000855
        public virtual System.Boolean CanConvert(System.Type objectType) { }
        // RVA: 0x041E1670  token: 0x6000856
        public System.Void .ctor() { }
        // RVA: 0x0489B2C0  token: 0x6000857
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000104  // size: 0x10
    public class EntityKeyMemberConverter : Newtonsoft.Json.JsonConverter
    {
        // Fields
        private static System.String EntityKeyMemberFullTypeName;  // const
        private static System.String KeyPropertyName;  // const
        private static System.String TypePropertyName;  // const
        private static System.String ValuePropertyName;  // const
        private static Newtonsoft.Json.Utilities.ReflectionObject _reflectionObject;  // static @ 0x0

        // Methods
        // RVA: 0x0A8D64C8  token: 0x600085E
        public virtual System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x0A8D6188  token: 0x600085F
        private static System.Void ReadAndAssertProperty(Newtonsoft.Json.JsonReader reader, System.String propertyName) { }
        // RVA: 0x0A8D6284  token: 0x6000860
        public virtual System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x0A8D60A4  token: 0x6000861
        private static System.Void EnsureReflectionObject(System.Type objectType) { }
        // RVA: 0x040B9A90  token: 0x6000862
        public virtual System.Boolean CanConvert(System.Type objectType) { }
        // RVA: 0x041E1670  token: 0x6000863
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000105  // size: 0x10
    public class ExpandoObjectConverter : Newtonsoft.Json.JsonConverter
    {
        // Properties
        System.Boolean CanWrite { get; /* RVA: 0x012081B0 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000864
        public virtual System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x0A8D6788  token: 0x6000865
        public virtual System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x0A8D6A44  token: 0x6000866
        private System.Object ReadValue(Newtonsoft.Json.JsonReader reader) { }
        // RVA: 0x0A8D6790  token: 0x6000867
        private System.Object ReadList(Newtonsoft.Json.JsonReader reader) { }
        // RVA: 0x0A8D68AC  token: 0x6000868
        private System.Object ReadObject(Newtonsoft.Json.JsonReader reader) { }
        // RVA: 0x042390E0  token: 0x6000869
        public virtual System.Boolean CanConvert(System.Type objectType) { }
        // RVA: 0x041E1670  token: 0x600086B
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000106  // size: 0x28
    public class IsoDateTimeConverter : Newtonsoft.Json.Converters.DateTimeConverterBase
    {
        // Fields
        private static System.String DefaultDateTimeFormat;  // const
        private System.Globalization.DateTimeStyles _dateTimeStyles;  // 0x10
        private System.String _dateTimeFormat;  // 0x18
        private System.Globalization.CultureInfo _culture;  // 0x20

        // Properties
        System.Globalization.DateTimeStyles DateTimeStyles { get; /* RVA: 0x02B2E2D0 */ set; /* RVA: 0x014F51F0 */ }
        System.String DateTimeFormat { get; /* RVA: 0x0A8D727C */ set; /* RVA: 0x0A8D72C4 */ }
        System.Globalization.CultureInfo Culture { get; /* RVA: 0x0A8D7228 */ set; /* RVA: 0x05390F40 */ }

        // Methods
        // RVA: 0x0A8D6FEC  token: 0x6000872
        public virtual System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x0A8D6BB4  token: 0x6000873
        public virtual System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x04DA5E80  token: 0x6000874
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000107  // size: 0x10
    public class JavaScriptDateTimeConverter : Newtonsoft.Json.Converters.DateTimeConverterBase
    {
        // Methods
        // RVA: 0x0A8D7628  token: 0x6000875
        public virtual System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x0A8D730C  token: 0x6000876
        public virtual System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x041E1670  token: 0x6000877
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000108  // size: 0x10
    public class KeyValuePairConverter : Newtonsoft.Json.JsonConverter
    {
        // Fields
        private static System.String KeyName;  // const
        private static System.String ValueName;  // const
        private static readonly Newtonsoft.Json.Utilities.ThreadSafeStore<System.Type,Newtonsoft.Json.Utilities.ReflectionObject> ReflectionObjectPerType;  // static @ 0x0

        // Methods
        // RVA: 0x0A8D77C0  token: 0x6000878
        private static Newtonsoft.Json.Utilities.ReflectionObject InitializeReflectionObject(System.Type t) { }
        // RVA: 0x0A8D7C9C  token: 0x6000879
        public virtual System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x0A8D7940  token: 0x600087A
        public virtual System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x035CF760  token: 0x600087B
        public virtual System.Boolean CanConvert(System.Type objectType) { }
        // RVA: 0x041E1670  token: 0x600087C
        public System.Void .ctor() { }
        // RVA: 0x04CAEFE0  token: 0x600087D
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000109  // size: 0x10
    public class RegexConverter : Newtonsoft.Json.JsonConverter
    {
        // Fields
        private static System.String PatternName;  // const
        private static System.String OptionsName;  // const

        // Methods
        // RVA: 0x0A8D862C  token: 0x600087E
        public virtual System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x02FB41C0  token: 0x600087F
        private System.Boolean HasFlag(System.Text.RegularExpressions.RegexOptions options, System.Text.RegularExpressions.RegexOptions flag) { }
        // RVA: 0x0A8D839C  token: 0x6000880
        private System.Void WriteBson(Newtonsoft.Json.Bson.BsonWriter writer, System.Text.RegularExpressions.Regex regex) { }
        // RVA: 0x0A8D84B8  token: 0x6000881
        private System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Text.RegularExpressions.Regex regex, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x0A8D7F1C  token: 0x6000882
        public virtual System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x0A8D824C  token: 0x6000883
        private System.Object ReadRegexString(Newtonsoft.Json.JsonReader reader) { }
        // RVA: 0x0A8D7FBC  token: 0x6000884
        private System.Text.RegularExpressions.Regex ReadRegexObject(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x041D06C0  token: 0x6000885
        public virtual System.Boolean CanConvert(System.Type objectType) { }
        // RVA: 0x0A8D7EB8  token: 0x6000886
        private System.Boolean IsRegex(System.Type objectType) { }
        // RVA: 0x041E1670  token: 0x6000887
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200010A  // size: 0x20
    public class StringEnumConverter : Newtonsoft.Json.JsonConverter
    {
        // Fields
        private Newtonsoft.Json.Serialization.NamingStrategy <NamingStrategy>k__BackingField;  // 0x10
        private System.Boolean <AllowIntegerValues>k__BackingField;  // 0x18

        // Properties
        System.Boolean CamelCaseText { get; /* RVA: 0x0A8D903C */ set; /* RVA: 0x0A8D9080 */ }
        Newtonsoft.Json.Serialization.NamingStrategy NamingStrategy { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        System.Boolean AllowIntegerValues { get; /* RVA: 0x04D86340 */ set; /* RVA: 0x04D86350 */ }

        // Methods
        // RVA: 0x04D8EB60  token: 0x600088E
        public System.Void .ctor() { }
        // RVA: 0x0A8D8D68  token: 0x600088F
        public System.Void .ctor(System.Boolean camelCaseText) { }
        // RVA: 0x0A8D8D40  token: 0x6000890
        public System.Void .ctor(Newtonsoft.Json.Serialization.NamingStrategy namingStrategy, System.Boolean allowIntegerValues) { }
        // RVA: 0x0A8D8DCC  token: 0x6000891
        public System.Void .ctor(System.Type namingStrategyType) { }
        // RVA: 0x0A8D8E8C  token: 0x6000892
        public System.Void .ctor(System.Type namingStrategyType, System.Object[] namingStrategyParameters) { }
        // RVA: 0x0A8D8F5C  token: 0x6000893
        public System.Void .ctor(System.Type namingStrategyType, System.Object[] namingStrategyParameters, System.Boolean allowIntegerValues) { }
        // RVA: 0x0A8D8B84  token: 0x6000894
        public virtual System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x0A8D8750  token: 0x6000895
        public virtual System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x0A8D86F4  token: 0x6000896
        public virtual System.Boolean CanConvert(System.Type objectType) { }

    }

    // TypeToken: 0x200010B  // size: 0x18
    public class UnixDateTimeConverter : Newtonsoft.Json.Converters.DateTimeConverterBase
    {
        // Fields
        private static readonly System.DateTime UnixEpoch;  // static @ 0x0
        private System.Boolean <AllowPreEpoch>k__BackingField;  // 0x10

        // Properties
        System.Boolean AllowPreEpoch { get; /* RVA: 0x015EFCE0 */ set; /* RVA: 0x04D86130 */ }

        // Methods
        // RVA: 0x04D8CCB0  token: 0x6000899
        public System.Void .ctor() { }
        // RVA: 0x04D86130  token: 0x600089A
        public System.Void .ctor(System.Boolean allowPreEpoch) { }
        // RVA: 0x0A8D95A4  token: 0x600089B
        public virtual System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x0A8D9178  token: 0x600089C
        public virtual System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x0A8D97F8  token: 0x600089D
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200010C  // size: 0x10
    public class VersionConverter : Newtonsoft.Json.JsonConverter
    {
        // Methods
        // RVA: 0x0A8D9B4C  token: 0x600089E
        public virtual System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x0A8D98D4  token: 0x600089F
        public virtual System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x0A8D9870  token: 0x60008A0
        public virtual System.Boolean CanConvert(System.Type objectType) { }
        // RVA: 0x041E1670  token: 0x60008A1
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200010D  // size: 0x30
    public class XmlDocumentWrapper : Newtonsoft.Json.Converters.XmlNodeWrapper, Newtonsoft.Json.Converters.IXmlDocument, Newtonsoft.Json.Converters.IXmlNode
    {
        // Fields
        private readonly System.Xml.XmlDocument _document;  // 0x28

        // Properties
        Newtonsoft.Json.Converters.IXmlElement DocumentElement { get; /* RVA: 0x0A8DC138 */ }

        // Methods
        // RVA: 0x0A8DB9D8  token: 0x60008A2
        public System.Void .ctor(System.Xml.XmlDocument document) { }
        // RVA: 0x0A8DBC74  token: 0x60008A3
        public virtual Newtonsoft.Json.Converters.IXmlNode CreateComment(System.String data) { }
        // RVA: 0x0A8DBF00  token: 0x60008A4
        public virtual Newtonsoft.Json.Converters.IXmlNode CreateTextNode(System.String text) { }
        // RVA: 0x0A8DBBF8  token: 0x60008A5
        public virtual Newtonsoft.Json.Converters.IXmlNode CreateCDataSection(System.String data) { }
        // RVA: 0x0A8DBF7C  token: 0x60008A6
        public virtual Newtonsoft.Json.Converters.IXmlNode CreateWhitespace(System.String text) { }
        // RVA: 0x0A8DBE84  token: 0x60008A7
        public virtual Newtonsoft.Json.Converters.IXmlNode CreateSignificantWhitespace(System.String text) { }
        // RVA: 0x0A8DBFF8  token: 0x60008A8
        public virtual Newtonsoft.Json.Converters.IXmlNode CreateXmlDeclaration(System.String version, System.String encoding, System.String standalone) { }
        // RVA: 0x0A8DC094  token: 0x60008A9
        public virtual Newtonsoft.Json.Converters.IXmlNode CreateXmlDocumentType(System.String name, System.String publicId, System.String systemId, System.String internalSubset) { }
        // RVA: 0x0A8DBDF8  token: 0x60008AA
        public virtual Newtonsoft.Json.Converters.IXmlNode CreateProcessingInstruction(System.String target, System.String data) { }
        // RVA: 0x0A8DBCF0  token: 0x60008AB
        public virtual Newtonsoft.Json.Converters.IXmlElement CreateElement(System.String elementName) { }
        // RVA: 0x0A8DBD6C  token: 0x60008AC
        public virtual Newtonsoft.Json.Converters.IXmlElement CreateElement(System.String qualifiedName, System.String namespaceUri) { }
        // RVA: 0x0A8DBAC0  token: 0x60008AD
        public virtual Newtonsoft.Json.Converters.IXmlNode CreateAttribute(System.String name, System.String value) { }
        // RVA: 0x0A8DBB54  token: 0x60008AE
        public virtual Newtonsoft.Json.Converters.IXmlNode CreateAttribute(System.String qualifiedName, System.String namespaceUri, System.String value) { }

    }

    // TypeToken: 0x200010E  // size: 0x30
    public class XmlElementWrapper : Newtonsoft.Json.Converters.XmlNodeWrapper, Newtonsoft.Json.Converters.IXmlElement, Newtonsoft.Json.Converters.IXmlNode
    {
        // Fields
        private readonly System.Xml.XmlElement _element;  // 0x28

        // Properties
        System.Boolean IsEmpty { get; /* RVA: 0x0A8DC268 */ }

        // Methods
        // RVA: 0x0A8DB9D8  token: 0x60008B0
        public System.Void .ctor(System.Xml.XmlElement element) { }
        // RVA: 0x0A8DC1E4  token: 0x60008B1
        public virtual System.Void SetAttributeNode(Newtonsoft.Json.Converters.IXmlNode attribute) { }
        // RVA: 0x0A8DC1C4  token: 0x60008B2
        public virtual System.String GetPrefixOfNamespace(System.String namespaceUri) { }

    }

    // TypeToken: 0x200010F  // size: 0x30
    public class XmlDeclarationWrapper : Newtonsoft.Json.Converters.XmlNodeWrapper, Newtonsoft.Json.Converters.IXmlDeclaration, Newtonsoft.Json.Converters.IXmlNode
    {
        // Fields
        private readonly System.Xml.XmlDeclaration _declaration;  // 0x28

        // Properties
        System.String Version { get; /* RVA: 0x0A8DBA38 */ }
        System.String Encoding { get; /* RVA: 0x0A8DBA00 */ }
        System.String Standalone { get; /* RVA: 0x0A8DBA1C */ }

        // Methods
        // RVA: 0x0A8DB9D8  token: 0x60008B4
        public System.Void .ctor(System.Xml.XmlDeclaration declaration) { }

    }

    // TypeToken: 0x2000110  // size: 0x30
    public class XmlDocumentTypeWrapper : Newtonsoft.Json.Converters.XmlNodeWrapper, Newtonsoft.Json.Converters.IXmlDocumentType, Newtonsoft.Json.Converters.IXmlNode
    {
        // Fields
        private readonly System.Xml.XmlDocumentType _documentType;  // 0x28

        // Properties
        System.String Name { get; /* RVA: 0x0A8DBA9C */ }
        System.String System { get; /* RVA: 0x0A8DBA1C */ }
        System.String Public { get; /* RVA: 0x0A8DBA00 */ }
        System.String InternalSubset { get; /* RVA: 0x0A8DBA54 */ }
        System.String LocalName { get; /* RVA: 0x0A8DBA70 */ }

        // Methods
        // RVA: 0x0A8DB9D8  token: 0x60008B8
        public System.Void .ctor(System.Xml.XmlDocumentType documentType) { }

    }

    // TypeToken: 0x2000111  // size: 0x28
    public class XmlNodeWrapper : Newtonsoft.Json.Converters.IXmlNode
    {
        // Fields
        private readonly System.Xml.XmlNode _node;  // 0x10
        private System.Collections.Generic.List<Newtonsoft.Json.Converters.IXmlNode> _childNodes;  // 0x18
        private System.Collections.Generic.List<Newtonsoft.Json.Converters.IXmlNode> _attributes;  // 0x20

        // Properties
        System.Object WrappedNode { get; /* RVA: 0x02B2ECC0 */ }
        System.Xml.XmlNodeType NodeType { get; /* RVA: 0x0A8E29E4 */ }
        System.String LocalName { get; /* RVA: 0x0A8E299C */ }
        System.Collections.Generic.List<Newtonsoft.Json.Converters.IXmlNode> ChildNodes { get; /* RVA: 0x0A8E2664 */ }
        System.Collections.Generic.List<Newtonsoft.Json.Converters.IXmlNode> Attributes { get; /* RVA: 0x0A8E23C8 */ }
        System.Boolean HasAttributes { get; /* RVA: 0x0A8E291C */ }
        Newtonsoft.Json.Converters.IXmlNode ParentNode { get; /* RVA: 0x0A8E2A08 */ }
        System.String Value { get; /* RVA: 0x0A8E2A84 */ set; /* RVA: 0x0A8E2AA8 */ }
        System.String NamespaceUri { get; /* RVA: 0x0A8E29C0 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x60008BE
        public System.Void .ctor(System.Xml.XmlNode node) { }
        // RVA: 0x0A8E225C  token: 0x60008C3
        private static Newtonsoft.Json.Converters.IXmlNode WrapNode(System.Xml.XmlNode node) { }
        // RVA: 0x0A8E21D4  token: 0x60008C9
        public virtual Newtonsoft.Json.Converters.IXmlNode AppendChild(Newtonsoft.Json.Converters.IXmlNode newChild) { }

    }

    // TypeToken: 0x2000112
    public interface IXmlDocument : Newtonsoft.Json.Converters.IXmlNode
    {
        // Properties
        Newtonsoft.Json.Converters.IXmlElement DocumentElement { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x60008CB
        public virtual Newtonsoft.Json.Converters.IXmlNode CreateComment(System.String text) { }
        // RVA: -1  // abstract  token: 0x60008CC
        public virtual Newtonsoft.Json.Converters.IXmlNode CreateTextNode(System.String text) { }
        // RVA: -1  // abstract  token: 0x60008CD
        public virtual Newtonsoft.Json.Converters.IXmlNode CreateCDataSection(System.String data) { }
        // RVA: -1  // abstract  token: 0x60008CE
        public virtual Newtonsoft.Json.Converters.IXmlNode CreateWhitespace(System.String text) { }
        // RVA: -1  // abstract  token: 0x60008CF
        public virtual Newtonsoft.Json.Converters.IXmlNode CreateSignificantWhitespace(System.String text) { }
        // RVA: -1  // abstract  token: 0x60008D0
        public virtual Newtonsoft.Json.Converters.IXmlNode CreateXmlDeclaration(System.String version, System.String encoding, System.String standalone) { }
        // RVA: -1  // abstract  token: 0x60008D1
        public virtual Newtonsoft.Json.Converters.IXmlNode CreateXmlDocumentType(System.String name, System.String publicId, System.String systemId, System.String internalSubset) { }
        // RVA: -1  // abstract  token: 0x60008D2
        public virtual Newtonsoft.Json.Converters.IXmlNode CreateProcessingInstruction(System.String target, System.String data) { }
        // RVA: -1  // abstract  token: 0x60008D3
        public virtual Newtonsoft.Json.Converters.IXmlElement CreateElement(System.String elementName) { }
        // RVA: -1  // abstract  token: 0x60008D4
        public virtual Newtonsoft.Json.Converters.IXmlElement CreateElement(System.String qualifiedName, System.String namespaceUri) { }
        // RVA: -1  // abstract  token: 0x60008D5
        public virtual Newtonsoft.Json.Converters.IXmlNode CreateAttribute(System.String name, System.String value) { }
        // RVA: -1  // abstract  token: 0x60008D6
        public virtual Newtonsoft.Json.Converters.IXmlNode CreateAttribute(System.String qualifiedName, System.String namespaceUri, System.String value) { }

    }

    // TypeToken: 0x2000113
    public interface IXmlDeclaration : Newtonsoft.Json.Converters.IXmlNode
    {
        // Properties
        System.String Version { get; /* RVA: -1  // abstract */ }
        System.String Encoding { get; /* RVA: -1  // abstract */ }
        System.String Standalone { get; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x2000114
    public interface IXmlDocumentType : Newtonsoft.Json.Converters.IXmlNode
    {
        // Properties
        System.String Name { get; /* RVA: -1  // abstract */ }
        System.String System { get; /* RVA: -1  // abstract */ }
        System.String Public { get; /* RVA: -1  // abstract */ }
        System.String InternalSubset { get; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x2000115
    public interface IXmlElement : Newtonsoft.Json.Converters.IXmlNode
    {
        // Properties
        System.Boolean IsEmpty { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x60008DF
        public virtual System.Void SetAttributeNode(Newtonsoft.Json.Converters.IXmlNode attribute) { }
        // RVA: -1  // abstract  token: 0x60008E0
        public virtual System.String GetPrefixOfNamespace(System.String namespaceUri) { }

    }

    // TypeToken: 0x2000116
    public interface IXmlNode
    {
        // Properties
        System.Xml.XmlNodeType NodeType { get; /* RVA: -1  // abstract */ }
        System.String LocalName { get; /* RVA: -1  // abstract */ }
        System.Collections.Generic.List<Newtonsoft.Json.Converters.IXmlNode> ChildNodes { get; /* RVA: -1  // abstract */ }
        System.Collections.Generic.List<Newtonsoft.Json.Converters.IXmlNode> Attributes { get; /* RVA: -1  // abstract */ }
        Newtonsoft.Json.Converters.IXmlNode ParentNode { get; /* RVA: -1  // abstract */ }
        System.String Value { get; /* RVA: -1  // abstract */ }
        System.String NamespaceUri { get; /* RVA: -1  // abstract */ }
        System.Object WrappedNode { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x60008E8
        public virtual Newtonsoft.Json.Converters.IXmlNode AppendChild(Newtonsoft.Json.Converters.IXmlNode newChild) { }

    }

    // TypeToken: 0x2000117  // size: 0x20
    public class XDeclarationWrapper : Newtonsoft.Json.Converters.XObjectWrapper, Newtonsoft.Json.Converters.IXmlDeclaration, Newtonsoft.Json.Converters.IXmlNode
    {
        // Fields
        private readonly System.Xml.Linq.XDeclaration <Declaration>k__BackingField;  // 0x18

        // Properties
        System.Xml.Linq.XDeclaration Declaration { get; /* RVA: 0x0385B100 */ }
        System.Xml.XmlNodeType NodeType { get; /* RVA: 0x04DA5E90 */ }
        System.String Version { get; /* RVA: 0x0A8DA4D0 */ }
        System.String Encoding { get; /* RVA: 0x0A8DA498 */ }
        System.String Standalone { get; /* RVA: 0x0A8DA4B4 */ }

        // Methods
        // RVA: 0x0A8DA46C  token: 0x60008EC
        public System.Void .ctor(System.Xml.Linq.XDeclaration declaration) { }

    }

    // TypeToken: 0x2000118  // size: 0x20
    public class XDocumentTypeWrapper : Newtonsoft.Json.Converters.XObjectWrapper, Newtonsoft.Json.Converters.IXmlDocumentType, Newtonsoft.Json.Converters.IXmlNode
    {
        // Fields
        private readonly System.Xml.Linq.XDocumentType _documentType;  // 0x18

        // Properties
        System.String Name { get; /* RVA: 0x0A8DA55C */ }
        System.String System { get; /* RVA: 0x0A8DA594 */ }
        System.String Public { get; /* RVA: 0x0A8DA578 */ }
        System.String InternalSubset { get; /* RVA: 0x0A8DA514 */ }
        System.String LocalName { get; /* RVA: 0x0A8DA530 */ }

        // Methods
        // RVA: 0x0A8DA4EC  token: 0x60008F1
        public System.Void .ctor(System.Xml.Linq.XDocumentType documentType) { }

    }

    // TypeToken: 0x2000119  // size: 0x20
    public class XDocumentWrapper : Newtonsoft.Json.Converters.XContainerWrapper, Newtonsoft.Json.Converters.IXmlDocument, Newtonsoft.Json.Converters.IXmlNode
    {
        // Properties
        System.Xml.Linq.XDocument Document { get; /* RVA: 0x0A8DAF78 */ }
        System.Collections.Generic.List<Newtonsoft.Json.Converters.IXmlNode> ChildNodes { get; /* RVA: 0x0A8DADCC */ }
        System.Boolean HasChildNodes { get; /* RVA: 0x0A8DAFB4 */ }
        Newtonsoft.Json.Converters.IXmlElement DocumentElement { get; /* RVA: 0x0A8DAEDC */ }

        // Methods
        // RVA: 0x053908C0  token: 0x60008F8
        public System.Void .ctor(System.Xml.Linq.XDocument document) { }
        // RVA: 0x0A8DA838  token: 0x60008FB
        public virtual Newtonsoft.Json.Converters.IXmlNode CreateComment(System.String text) { }
        // RVA: 0x0A8DAB40  token: 0x60008FC
        public virtual Newtonsoft.Json.Converters.IXmlNode CreateTextNode(System.String text) { }
        // RVA: 0x0A8DA7A8  token: 0x60008FD
        public virtual Newtonsoft.Json.Converters.IXmlNode CreateCDataSection(System.String data) { }
        // RVA: 0x0A8DABD0  token: 0x60008FE
        public virtual Newtonsoft.Json.Converters.IXmlNode CreateWhitespace(System.String text) { }
        // RVA: 0x0A8DAAB0  token: 0x60008FF
        public virtual Newtonsoft.Json.Converters.IXmlNode CreateSignificantWhitespace(System.String text) { }
        // RVA: 0x0A8DAC60  token: 0x6000900
        public virtual Newtonsoft.Json.Converters.IXmlNode CreateXmlDeclaration(System.String version, System.String encoding, System.String standalone) { }
        // RVA: 0x0A8DAD10  token: 0x6000901
        public virtual Newtonsoft.Json.Converters.IXmlNode CreateXmlDocumentType(System.String name, System.String publicId, System.String systemId, System.String internalSubset) { }
        // RVA: 0x0A8DAA10  token: 0x6000902
        public virtual Newtonsoft.Json.Converters.IXmlNode CreateProcessingInstruction(System.String target, System.String data) { }
        // RVA: 0x0A8DA8C8  token: 0x6000903
        public virtual Newtonsoft.Json.Converters.IXmlElement CreateElement(System.String elementName) { }
        // RVA: 0x0A8DA964  token: 0x6000904
        public virtual Newtonsoft.Json.Converters.IXmlElement CreateElement(System.String qualifiedName, System.String namespaceUri) { }
        // RVA: 0x0A8DA640  token: 0x6000905
        public virtual Newtonsoft.Json.Converters.IXmlNode CreateAttribute(System.String name, System.String value) { }
        // RVA: 0x0A8DA6EC  token: 0x6000906
        public virtual Newtonsoft.Json.Converters.IXmlNode CreateAttribute(System.String qualifiedName, System.String namespaceUri, System.String value) { }
        // RVA: 0x0A8DA5B0  token: 0x6000908
        public virtual Newtonsoft.Json.Converters.IXmlNode AppendChild(Newtonsoft.Json.Converters.IXmlNode newChild) { }

    }

    // TypeToken: 0x200011A  // size: 0x18
    public class XTextWrapper : Newtonsoft.Json.Converters.XObjectWrapper
    {
        // Properties
        System.Xml.Linq.XText Text { get; /* RVA: 0x0A8DB97C */ }
        System.String Value { get; /* RVA: 0x0A8DB9B8 */ }
        Newtonsoft.Json.Converters.IXmlNode ParentNode { get; /* RVA: 0x0A8DB924 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x600090A
        public System.Void .ctor(System.Xml.Linq.XText text) { }

    }

    // TypeToken: 0x200011B  // size: 0x18
    public class XCommentWrapper : Newtonsoft.Json.Converters.XObjectWrapper
    {
        // Properties
        System.Xml.Linq.XComment Text { get; /* RVA: 0x0A8D9D80 */ }
        System.String Value { get; /* RVA: 0x0A8D9DBC */ }
        Newtonsoft.Json.Converters.IXmlNode ParentNode { get; /* RVA: 0x0A8D9D28 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x600090E
        public System.Void .ctor(System.Xml.Linq.XComment text) { }

    }

    // TypeToken: 0x200011C  // size: 0x18
    public class XProcessingInstructionWrapper : Newtonsoft.Json.Converters.XObjectWrapper
    {
        // Properties
        System.Xml.Linq.XProcessingInstruction ProcessingInstruction { get; /* RVA: 0x0A8DB8C8 */ }
        System.String LocalName { get; /* RVA: 0x0A8DB8A8 */ }
        System.String Value { get; /* RVA: 0x0A8DB904 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x6000912
        public System.Void .ctor(System.Xml.Linq.XProcessingInstruction processingInstruction) { }

    }

    // TypeToken: 0x200011D  // size: 0x20
    public class XContainerWrapper : Newtonsoft.Json.Converters.XObjectWrapper
    {
        // Fields
        private System.Collections.Generic.List<Newtonsoft.Json.Converters.IXmlNode> _childNodes;  // 0x18

        // Properties
        System.Xml.Linq.XContainer Container { get; /* RVA: 0x0A8DA3AC */ }
        System.Collections.Generic.List<Newtonsoft.Json.Converters.IXmlNode> ChildNodes { get; /* RVA: 0x0A8DA180 */ }
        System.Boolean HasChildNodes { get; /* RVA: 0x0A8DA3E8 */ }
        Newtonsoft.Json.Converters.IXmlNode ParentNode { get; /* RVA: 0x0A8DA414 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x6000916
        public System.Void .ctor(System.Xml.Linq.XContainer container) { }
        // RVA: 0x0A8D9E44  token: 0x600091A
        private static Newtonsoft.Json.Converters.IXmlNode WrapNode(System.Xml.Linq.XObject node) { }
        // RVA: 0x0A8D9DDC  token: 0x600091B
        public virtual Newtonsoft.Json.Converters.IXmlNode AppendChild(Newtonsoft.Json.Converters.IXmlNode newChild) { }

    }

    // TypeToken: 0x200011E  // size: 0x18
    public class XObjectWrapper : Newtonsoft.Json.Converters.IXmlNode
    {
        // Fields
        private readonly System.Xml.Linq.XObject _xmlObject;  // 0x10

        // Properties
        System.Object WrappedNode { get; /* RVA: 0x02B2ECC0 */ }
        System.Xml.XmlNodeType NodeType { get; /* RVA: 0x0A8DB888 */ }
        System.String LocalName { get; /* RVA: 0x011EC580 */ }
        System.Collections.Generic.List<Newtonsoft.Json.Converters.IXmlNode> ChildNodes { get; /* RVA: 0x0A8DB838 */ }
        System.Collections.Generic.List<Newtonsoft.Json.Converters.IXmlNode> Attributes { get; /* RVA: 0x0A8DB7E8 */ }
        Newtonsoft.Json.Converters.IXmlNode ParentNode { get; /* RVA: 0x011EC580 */ }
        System.String Value { get; /* RVA: 0x011EC580 */ }
        System.String NamespaceUri { get; /* RVA: 0x011EC580 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x600091C
        public System.Void .ctor(System.Xml.Linq.XObject xmlObject) { }
        // RVA: 0x0A8DB79C  token: 0x6000924
        public virtual Newtonsoft.Json.Converters.IXmlNode AppendChild(Newtonsoft.Json.Converters.IXmlNode newChild) { }

    }

    // TypeToken: 0x200011F  // size: 0x18
    public class XAttributeWrapper : Newtonsoft.Json.Converters.XObjectWrapper
    {
        // Properties
        System.Xml.Linq.XAttribute Attribute { get; /* RVA: 0x0A8D9C20 */ }
        System.String Value { get; /* RVA: 0x0A8D9D08 */ }
        System.String LocalName { get; /* RVA: 0x0A8D9C5C */ }
        System.String NamespaceUri { get; /* RVA: 0x0A8D9C84 */ }
        Newtonsoft.Json.Converters.IXmlNode ParentNode { get; /* RVA: 0x0A8D9CB0 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x6000927
        public System.Void .ctor(System.Xml.Linq.XAttribute attribute) { }

    }

    // TypeToken: 0x2000120  // size: 0x28
    public class XElementWrapper : Newtonsoft.Json.Converters.XContainerWrapper, Newtonsoft.Json.Converters.IXmlElement, Newtonsoft.Json.Converters.IXmlNode
    {
        // Fields
        private System.Collections.Generic.List<Newtonsoft.Json.Converters.IXmlNode> _attributes;  // 0x20

        // Properties
        System.Xml.Linq.XElement Element { get; /* RVA: 0x0A8DB6C4 */ }
        System.Collections.Generic.List<Newtonsoft.Json.Converters.IXmlNode> Attributes { get; /* RVA: 0x0A8DB35C */ }
        System.String Value { get; /* RVA: 0x0A8DB778 */ }
        System.String LocalName { get; /* RVA: 0x0A8DB724 */ }
        System.String NamespaceUri { get; /* RVA: 0x0A8DB74C */ }
        System.Boolean IsEmpty { get; /* RVA: 0x0A8DB700 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x600092D
        public System.Void .ctor(System.Xml.Linq.XElement element) { }
        // RVA: 0x0A8DB2DC  token: 0x600092E
        public virtual System.Void SetAttributeNode(Newtonsoft.Json.Converters.IXmlNode attribute) { }
        // RVA: 0x0A8DB064  token: 0x6000930
        private System.Boolean HasImplicitNamespaceAttribute(System.String namespaceUri) { }
        // RVA: 0x0A8DAFF0  token: 0x6000931
        public virtual Newtonsoft.Json.Converters.IXmlNode AppendChild(Newtonsoft.Json.Converters.IXmlNode newChild) { }
        // RVA: 0x0A8DB020  token: 0x6000935
        public virtual System.String GetPrefixOfNamespace(System.String namespaceUri) { }

    }

    // TypeToken: 0x2000121  // size: 0x20
    public class XmlNodeConverter : Newtonsoft.Json.JsonConverter
    {
        // Fields
        private static readonly System.Collections.Generic.List<Newtonsoft.Json.Converters.IXmlNode> EmptyChildNodes;  // static @ 0x0
        private static System.String TextName;  // const
        private static System.String CommentName;  // const
        private static System.String CDataName;  // const
        private static System.String WhitespaceName;  // const
        private static System.String SignificantWhitespaceName;  // const
        private static System.String DeclarationName;  // const
        private static System.String JsonNamespaceUri;  // const
        private System.String <DeserializeRootElementName>k__BackingField;  // 0x10
        private System.Boolean <WriteArrayAttribute>k__BackingField;  // 0x18
        private System.Boolean <OmitRootObject>k__BackingField;  // 0x19
        private System.Boolean <EncodeSpecialCharacters>k__BackingField;  // 0x1a

        // Properties
        System.String DeserializeRootElementName { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        System.Boolean WriteArrayAttribute { get; /* RVA: 0x04D86340 */ set; /* RVA: 0x04D86350 */ }
        System.Boolean OmitRootObject { get; /* RVA: 0x04D866A0 */ set; /* RVA: 0x04D866C0 */ }
        System.Boolean EncodeSpecialCharacters { get; /* RVA: 0x015AF940 */ set; /* RVA: 0x04D866E0 */ }

        // Methods
        // RVA: 0x0A8E209C  token: 0x600093F
        public virtual System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x0A8E1DA0  token: 0x6000940
        private Newtonsoft.Json.Converters.IXmlNode WrapXml(System.Object value) { }
        // RVA: 0x0A8DE8DC  token: 0x6000941
        private System.Void PushParentNamespaces(Newtonsoft.Json.Converters.IXmlNode node, System.Xml.XmlNamespaceManager manager) { }
        // RVA: 0x0A8DFDB0  token: 0x6000942
        private System.String ResolveFullName(Newtonsoft.Json.Converters.IXmlNode node, System.Xml.XmlNamespaceManager manager) { }
        // RVA: 0x0A8DE288  token: 0x6000943
        private System.String GetPropertyName(Newtonsoft.Json.Converters.IXmlNode node, System.Xml.XmlNamespaceManager manager) { }
        // RVA: 0x0A8DE568  token: 0x6000944
        private System.Boolean IsArray(Newtonsoft.Json.Converters.IXmlNode node) { }
        // RVA: 0x0A8DFEF0  token: 0x6000945
        private System.Void SerializeGroupedNodes(Newtonsoft.Json.JsonWriter writer, Newtonsoft.Json.Converters.IXmlNode node, System.Xml.XmlNamespaceManager manager, System.Boolean writePropertyName) { }
        // RVA: 0x0A8E1E88  token: 0x6000946
        private System.Void WriteGroupedNodes(Newtonsoft.Json.JsonWriter writer, System.Xml.XmlNamespaceManager manager, System.Boolean writePropertyName, System.Collections.Generic.List<Newtonsoft.Json.Converters.IXmlNode> groupedNodes, System.String elementNames) { }
        // RVA: 0x0A8E1FD4  token: 0x6000947
        private System.Void WriteGroupedNodes(Newtonsoft.Json.JsonWriter writer, System.Xml.XmlNamespaceManager manager, System.Boolean writePropertyName, Newtonsoft.Json.Converters.IXmlNode node, System.String elementNames) { }
        // RVA: 0x0A8E068C  token: 0x6000948
        private System.Void SerializeNode(Newtonsoft.Json.JsonWriter writer, Newtonsoft.Json.Converters.IXmlNode node, System.Xml.XmlNamespaceManager manager, System.Boolean writePropertyName) { }
        // RVA: 0x0A8DC58C  token: 0x6000949
        private static System.Boolean AllSameName(Newtonsoft.Json.Converters.IXmlNode node) { }
        // RVA: 0x0A8DF7D4  token: 0x600094A
        public virtual System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x0A8DDF28  token: 0x600094B
        private System.Void DeserializeValue(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Converters.IXmlDocument document, System.Xml.XmlNamespaceManager manager, System.String propertyName, Newtonsoft.Json.Converters.IXmlNode currentNode) { }
        // RVA: 0x0A8DF4C8  token: 0x600094C
        private System.Void ReadElement(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Converters.IXmlDocument document, Newtonsoft.Json.Converters.IXmlNode currentNode, System.String propertyName, System.Xml.XmlNamespaceManager manager) { }
        // RVA: 0x0A8DCF2C  token: 0x600094D
        private System.Void CreateElement(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Converters.IXmlDocument document, Newtonsoft.Json.Converters.IXmlNode currentNode, System.String elementName, System.Xml.XmlNamespaceManager manager, System.String elementPrefix, System.Collections.Generic.Dictionary<System.String,System.String> attributeNameValues) { }
        // RVA: 0x0A8DC288  token: 0x600094E
        private static System.Void AddAttribute(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Converters.IXmlDocument document, Newtonsoft.Json.Converters.IXmlNode currentNode, System.String propertyName, System.String attributeName, System.Xml.XmlNamespaceManager manager, System.String attributePrefix) { }
        // RVA: 0x0A8DC6E8  token: 0x600094F
        private static System.String ConvertTokenToXmlValue(Newtonsoft.Json.JsonReader reader) { }
        // RVA: 0x0A8DEC5C  token: 0x6000950
        private System.Void ReadArrayElements(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Converters.IXmlDocument document, System.String propertyName, Newtonsoft.Json.Converters.IXmlNode currentNode, System.Xml.XmlNamespaceManager manager) { }
        // RVA: 0x0A8DC440  token: 0x6000951
        private System.Void AddJsonArrayAttribute(Newtonsoft.Json.Converters.IXmlElement element, Newtonsoft.Json.Converters.IXmlDocument document) { }
        // RVA: 0x0A8E1BA0  token: 0x6000952
        private System.Boolean ShouldReadInto(Newtonsoft.Json.JsonReader reader) { }
        // RVA: 0x0A8DEEE8  token: 0x6000953
        private System.Collections.Generic.Dictionary<System.String,System.String> ReadAttributeElements(Newtonsoft.Json.JsonReader reader, System.Xml.XmlNamespaceManager manager) { }
        // RVA: 0x0A8DD518  token: 0x6000954
        private System.Void CreateInstruction(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Converters.IXmlDocument document, Newtonsoft.Json.Converters.IXmlNode currentNode, System.String propertyName) { }
        // RVA: 0x0A8DCBF0  token: 0x6000955
        private System.Void CreateDocumentType(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Converters.IXmlDocument document, Newtonsoft.Json.Converters.IXmlNode currentNode) { }
        // RVA: 0x0A8DD380  token: 0x6000956
        private Newtonsoft.Json.Converters.IXmlElement CreateElement(System.String elementName, Newtonsoft.Json.Converters.IXmlDocument document, System.String elementPrefix, System.Xml.XmlNamespaceManager manager) { }
        // RVA: 0x0A8DD8C4  token: 0x6000957
        private System.Void DeserializeNode(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Converters.IXmlDocument document, System.Xml.XmlNamespaceManager manager, Newtonsoft.Json.Converters.IXmlNode currentNode) { }
        // RVA: 0x0A8DE720  token: 0x6000958
        private System.Boolean IsNamespaceAttribute(System.String attributeName, System.String& prefix) { }
        // RVA: 0x0A8E1C0C  token: 0x6000959
        private System.Boolean ValueAttributes(System.Collections.Generic.List<Newtonsoft.Json.Converters.IXmlNode> c) { }
        // RVA: 0x040B9AC0  token: 0x600095A
        public virtual System.Boolean CanConvert(System.Type valueType) { }
        // RVA: 0x0A8DE7EC  token: 0x600095B
        private System.Boolean IsXObject(System.Type valueType) { }
        // RVA: 0x0A8DE864  token: 0x600095C
        private System.Boolean IsXmlNode(System.Type valueType) { }
        // RVA: 0x041E1670  token: 0x600095D
        public System.Void .ctor() { }
        // RVA: 0x04D165F0  token: 0x600095E
        private static System.Void .cctor() { }

    }

}

namespace Newtonsoft.Json.Linq
{

    // TypeToken: 0x20000DE  // size: 0x14
    public sealed struct CommentHandling
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.Linq.CommentHandling Ignore;  // const
        public static Newtonsoft.Json.Linq.CommentHandling Load;  // const

    }

    // TypeToken: 0x20000DF  // size: 0x14
    public sealed struct DuplicatePropertyNameHandling
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.Linq.DuplicatePropertyNameHandling Replace;  // const
        public static Newtonsoft.Json.Linq.DuplicatePropertyNameHandling Ignore;  // const
        public static Newtonsoft.Json.Linq.DuplicatePropertyNameHandling Error;  // const

    }

    // TypeToken: 0x20000E0  // size: 0x10
    public static class Extensions
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000679
        public static U Value(System.Collections.Generic.IEnumerable<Newtonsoft.Json.Linq.JToken> value) { }
        // RVA: -1  // generic def  token: 0x600067A
        public static U Value(System.Collections.Generic.IEnumerable<T> value) { }
        // RVA: -1  // generic def  token: 0x600067B
        private static U Convert(T token) { }

    }

    // TypeToken: 0x20000E1  // size: 0x58
    public class JArray : Newtonsoft.Json.Linq.JContainer, System.Collections.Generic.IList`1, System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly System.Collections.Generic.List<Newtonsoft.Json.Linq.JToken> _values;  // 0x50

        // Properties
        System.Collections.Generic.IList<Newtonsoft.Json.Linq.JToken> ChildrenTokens { get; /* RVA: 0x04D86270 */ }
        Newtonsoft.Json.Linq.JTokenType Type { get; /* RVA: 0x02BBE540 */ }
        Newtonsoft.Json.Linq.JToken Item { get; /* RVA: 0x0A8C3240 */ }
        Newtonsoft.Json.Linq.JToken Item { get; /* RVA: 0x0A8C3240 */ set; /* RVA: 0x0A8C3388 */ }
        System.Boolean IsReadOnly { get; /* RVA: 0x012081B0 */ }

        // Methods
        // RVA: 0x03427EE0  token: 0x600067E
        public System.Void .ctor() { }
        // RVA: 0x0A8C31A0  token: 0x600067F
        private System.Void .ctor(Newtonsoft.Json.Linq.JArray other, Newtonsoft.Json.Linq.JsonCloneSettings settings) { }
        // RVA: 0x0A8C3104  token: 0x6000680
        public System.Void .ctor(System.Object content) { }
        // RVA: 0x0A8C2F14  token: 0x6000681
        private virtual Newtonsoft.Json.Linq.JToken CloneToken(Newtonsoft.Json.Linq.JsonCloneSettings settings) { }
        // RVA: 0x05396950  token: 0x6000682
        public static Newtonsoft.Json.Linq.JArray Load(Newtonsoft.Json.JsonReader reader) { }
        // RVA: 0x03423920  token: 0x6000683
        public static Newtonsoft.Json.Linq.JArray Load(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Linq.JsonLoadSettings settings) { }
        // RVA: 0x0A8C304C  token: 0x6000684
        public virtual System.Void WriteTo(Newtonsoft.Json.JsonWriter writer, Newtonsoft.Json.JsonConverter[] converters) { }
        // RVA: 0x0A8C2FA0  token: 0x6000688
        private virtual System.Int32 IndexOfItem(Newtonsoft.Json.Linq.JToken item) { }
        // RVA: 0x0A8C2FF4  token: 0x6000689
        public virtual System.Int32 IndexOf(Newtonsoft.Json.Linq.JToken item) { }
        // RVA: 0x0A8C3000  token: 0x600068A
        public virtual System.Void Insert(System.Int32 index, Newtonsoft.Json.Linq.JToken item) { }
        // RVA: 0x0A8C3024  token: 0x600068B
        public virtual System.Void RemoveAt(System.Int32 index) { }
        // RVA: 0x03F4DDF0  token: 0x600068C
        public virtual System.Collections.Generic.IEnumerator<Newtonsoft.Json.Linq.JToken> GetEnumerator() { }
        // RVA: 0x0A8C2EFC  token: 0x600068D
        public virtual System.Void Add(Newtonsoft.Json.Linq.JToken item) { }
        // RVA: 0x0A8C2F04  token: 0x600068E
        public virtual System.Void Clear() { }
        // RVA: 0x0A8C2F84  token: 0x600068F
        public virtual System.Boolean Contains(Newtonsoft.Json.Linq.JToken item) { }
        // RVA: 0x0A8C2F98  token: 0x6000690
        public virtual System.Void CopyTo(Newtonsoft.Json.Linq.JToken[] array, System.Int32 arrayIndex) { }
        // RVA: 0x0A8C3038  token: 0x6000692
        public virtual System.Boolean Remove(Newtonsoft.Json.Linq.JToken item) { }

    }

    // TypeToken: 0x20000E2  // size: 0x60
    public class JConstructor : Newtonsoft.Json.Linq.JContainer
    {
        // Fields
        private System.String _name;  // 0x50
        private readonly System.Collections.Generic.List<Newtonsoft.Json.Linq.JToken> _values;  // 0x58

        // Properties
        System.Collections.Generic.IList<Newtonsoft.Json.Linq.JToken> ChildrenTokens { get; /* RVA: 0x02E56440 */ }
        System.String Name { get; /* RVA: 0x04D86270 */ }
        Newtonsoft.Json.Linq.JTokenType Type { get; /* RVA: 0x04D8C9E0 */ }
        Newtonsoft.Json.Linq.JToken Item { get; /* RVA: 0x0A8C3940 */ }

        // Methods
        // RVA: 0x0A8C3408  token: 0x6000694
        private virtual System.Int32 IndexOfItem(Newtonsoft.Json.Linq.JToken item) { }
        // RVA: 0x0A8C3888  token: 0x6000697
        private System.Void .ctor(Newtonsoft.Json.Linq.JConstructor other, Newtonsoft.Json.Linq.JsonCloneSettings settings) { }
        // RVA: 0x0A8C3728  token: 0x6000698
        public System.Void .ctor(System.String name) { }
        // RVA: 0x0A8C3398  token: 0x6000699
        private virtual Newtonsoft.Json.Linq.JToken CloneToken(Newtonsoft.Json.Linq.JsonCloneSettings settings) { }
        // RVA: 0x0A8C3658  token: 0x600069A
        public virtual System.Void WriteTo(Newtonsoft.Json.JsonWriter writer, Newtonsoft.Json.JsonConverter[] converters) { }
        // RVA: 0x0A8C345C  token: 0x600069C
        public static Newtonsoft.Json.Linq.JConstructor Load(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Linq.JsonLoadSettings settings) { }

    }

    // TypeToken: 0x20000E3  // size: 0x50
    public abstract class JContainer : Newtonsoft.Json.Linq.JToken, System.Collections.Generic.IList`1, System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.ComponentModel.IBindingList, System.Collections.ICollection, System.Collections.IList, System.Collections.Specialized.INotifyCollectionChanged
    {
        // Fields
        private System.ComponentModel.ListChangedEventHandler _listChanged;  // 0x30
        private System.Collections.Specialized.NotifyCollectionChangedEventHandler _collectionChanged;  // 0x38
        private System.Object _syncRoot;  // 0x40
        private System.Boolean _busy;  // 0x48

        // Properties
        System.Collections.Generic.IList<Newtonsoft.Json.Linq.JToken> ChildrenTokens { get; /* RVA: -1  // abstract */ }
        System.Boolean HasValues { get; /* RVA: 0x040464C0 */ }
        Newtonsoft.Json.Linq.JToken First { get; /* RVA: 0x049E74E0 */ }
        Newtonsoft.Json.Linq.JToken Last { get; /* RVA: 0x03426800 */ }
        Newtonsoft.Json.Linq.JToken System.Collections.Generic.IList<Newtonsoft.Json.Linq.JToken>.Item { get; /* RVA: 0x053924D4 */ set; /* RVA: 0x0A8C3388 */ }
        System.Boolean System.Collections.Generic.ICollection<Newtonsoft.Json.Linq.JToken>.IsReadOnly { get; /* RVA: 0x012081B0 */ }
        System.Boolean System.Collections.IList.IsFixedSize { get; /* RVA: 0x012081B0 */ }
        System.Boolean System.Collections.IList.IsReadOnly { get; /* RVA: 0x012081B0 */ }
        System.Object System.Collections.IList.Item { get; /* RVA: 0x053924D4 */ set; /* RVA: 0x0A8C47DC */ }
        System.Int32 Count { get; /* RVA: 0x03424A40 */ }
        System.Boolean System.Collections.ICollection.IsSynchronized { get; /* RVA: 0x012081B0 */ }
        System.Object System.Collections.ICollection.SyncRoot { get; /* RVA: 0x0A8C4694 */ }

        // Events
        event System.Collections.Specialized.NotifyCollectionChangedEventHandler CollectionChanged;

        // Methods
        // RVA: 0x03427C80  token: 0x60006A0
        private System.Void .ctor() { }
        // RVA: 0x0A8C4814  token: 0x60006A1
        private System.Void .ctor(Newtonsoft.Json.Linq.JContainer other, Newtonsoft.Json.Linq.JsonCloneSettings settings) { }
        // RVA: 0x034279A0  token: 0x60006A2
        private System.Void CheckReentrancy() { }
        // RVA: 0x0A8C42A0  token: 0x60006A3
        protected virtual System.Void OnListChanged(System.ComponentModel.ListChangedEventArgs e) { }
        // RVA: 0x0A8C4204  token: 0x60006A4
        protected virtual System.Void OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e) { }
        // RVA: 0x04791E50  token: 0x60006A8
        public virtual Newtonsoft.Json.Linq.JEnumerable<Newtonsoft.Json.Linq.JToken> Children() { }
        // RVA: 0x03427480  token: 0x60006A9
        private System.Boolean IsMultiContent(System.Object content) { }
        // RVA: 0x03427930  token: 0x60006AA
        private Newtonsoft.Json.Linq.JToken EnsureParentToken(Newtonsoft.Json.Linq.JToken item, System.Boolean skipParentCheck, System.Boolean copyAnnotations) { }
        // RVA: -1  // abstract  token: 0x60006AB
        private virtual System.Int32 IndexOfItem(Newtonsoft.Json.Linq.JToken item) { }
        // RVA: 0x03427750  token: 0x60006AC
        private virtual System.Boolean InsertItem(System.Int32 index, Newtonsoft.Json.Linq.JToken item, System.Boolean skipParentCheck, System.Boolean copyAnnotations) { }
        // RVA: 0x0A8C433C  token: 0x60006AD
        private virtual System.Void RemoveItemAt(System.Int32 index) { }
        // RVA: 0x0A8C4618  token: 0x60006AE
        private virtual System.Boolean RemoveItem(Newtonsoft.Json.Linq.JToken item) { }
        // RVA: 0x03428730  token: 0x60006AF
        private virtual Newtonsoft.Json.Linq.JToken GetItem(System.Int32 index) { }
        // RVA: 0x047C9BB0  token: 0x60006B0
        private virtual System.Void SetItem(System.Int32 index, Newtonsoft.Json.Linq.JToken item) { }
        // RVA: 0x0A8C3A8C  token: 0x60006B1
        private virtual System.Void ClearItems() { }
        // RVA: 0x0A8C4654  token: 0x60006B2
        private virtual System.Void ReplaceItem(Newtonsoft.Json.Linq.JToken existing, Newtonsoft.Json.Linq.JToken replacement) { }
        // RVA: 0x0A8C3DB0  token: 0x60006B3
        private virtual System.Boolean ContainsItem(Newtonsoft.Json.Linq.JToken item) { }
        // RVA: 0x0A8C3DCC  token: 0x60006B4
        private virtual System.Void CopyItemsTo(System.Array array, System.Int32 arrayIndex) { }
        // RVA: 0x047C9EA0  token: 0x60006B5
        private static System.Boolean IsTokenUnchanged(Newtonsoft.Json.Linq.JToken currentValue, Newtonsoft.Json.Linq.JToken newValue) { }
        // RVA: 0x040B6120  token: 0x60006B6
        private virtual System.Void ValidateToken(Newtonsoft.Json.Linq.JToken o, Newtonsoft.Json.Linq.JToken existing) { }
        // RVA: 0x03426FD0  token: 0x60006B7
        public virtual System.Void Add(System.Object content) { }
        // RVA: 0x034285D0  token: 0x60006B8
        private System.Boolean TryAdd(System.Object content) { }
        // RVA: 0x03426110  token: 0x60006B9
        private System.Void AddAndSkipParentCheck(Newtonsoft.Json.Linq.JToken token) { }
        // RVA: 0x03427040  token: 0x60006BA
        private System.Boolean TryAddInternal(System.Int32 index, System.Object content, System.Boolean skipParentCheck, System.Boolean copyAnnotations) { }
        // RVA: 0x03427440  token: 0x60006BB
        private static Newtonsoft.Json.Linq.JToken CreateFromContent(System.Object content) { }
        // RVA: 0x0A8C2F04  token: 0x60006BC
        public System.Void RemoveAll() { }
        // RVA: 0x03425FA0  token: 0x60006BD
        private System.Void ReadTokenFrom(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Linq.JsonLoadSettings options) { }
        // RVA: 0x03426CF0  token: 0x60006BE
        private System.Void ReadContentFrom(Newtonsoft.Json.JsonReader r, Newtonsoft.Json.Linq.JsonLoadSettings settings) { }
        // RVA: 0x03426A00  token: 0x60006BF
        private static Newtonsoft.Json.Linq.JProperty ReadProperty(Newtonsoft.Json.JsonReader r, Newtonsoft.Json.Linq.JsonLoadSettings settings, Newtonsoft.Json.IJsonLineInfo lineInfo, Newtonsoft.Json.Linq.JContainer parent) { }
        // RVA: 0x0A8C2FF4  token: 0x60006C0
        private virtual System.Int32 System.Collections.Generic.IList<Newtonsoft.Json.Linq.JToken>.IndexOf(Newtonsoft.Json.Linq.JToken item) { }
        // RVA: 0x0A8C3000  token: 0x60006C1
        private virtual System.Void System.Collections.Generic.IList<Newtonsoft.Json.Linq.JToken>.Insert(System.Int32 index, Newtonsoft.Json.Linq.JToken item) { }
        // RVA: 0x0A8C3024  token: 0x60006C2
        private virtual System.Void System.Collections.Generic.IList<Newtonsoft.Json.Linq.JToken>.RemoveAt(System.Int32 index) { }
        // RVA: 0x0A8C2EFC  token: 0x60006C5
        private virtual System.Void System.Collections.Generic.ICollection<Newtonsoft.Json.Linq.JToken>.Add(Newtonsoft.Json.Linq.JToken item) { }
        // RVA: 0x0A8C2F04  token: 0x60006C6
        private virtual System.Void System.Collections.Generic.ICollection<Newtonsoft.Json.Linq.JToken>.Clear() { }
        // RVA: 0x0A8C2F84  token: 0x60006C7
        private virtual System.Boolean System.Collections.Generic.ICollection<Newtonsoft.Json.Linq.JToken>.Contains(Newtonsoft.Json.Linq.JToken item) { }
        // RVA: 0x0A8C2F98  token: 0x60006C8
        private virtual System.Void System.Collections.Generic.ICollection<Newtonsoft.Json.Linq.JToken>.CopyTo(Newtonsoft.Json.Linq.JToken[] array, System.Int32 arrayIndex) { }
        // RVA: 0x0A8C3038  token: 0x60006CA
        private virtual System.Boolean System.Collections.Generic.ICollection<Newtonsoft.Json.Linq.JToken>.Remove(Newtonsoft.Json.Linq.JToken item) { }
        // RVA: 0x0A8C4168  token: 0x60006CB
        private Newtonsoft.Json.Linq.JToken EnsureValue(System.Object value) { }
        // RVA: 0x0A8C46F4  token: 0x60006CC
        private virtual System.Int32 System.Collections.IList.Add(System.Object value) { }
        // RVA: 0x0A8C2F04  token: 0x60006CD
        private virtual System.Void System.Collections.IList.Clear() { }
        // RVA: 0x0A8C4728  token: 0x60006CE
        private virtual System.Boolean System.Collections.IList.Contains(System.Object value) { }
        // RVA: 0x0A8C4750  token: 0x60006CF
        private virtual System.Int32 System.Collections.IList.IndexOf(System.Object value) { }
        // RVA: 0x0A8C4774  token: 0x60006D0
        private virtual System.Void System.Collections.IList.Insert(System.Int32 index, System.Object value) { }
        // RVA: 0x0A8C47B4  token: 0x60006D3
        private virtual System.Void System.Collections.IList.Remove(System.Object value) { }
        // RVA: 0x0A8C3024  token: 0x60006D4
        private virtual System.Void System.Collections.IList.RemoveAt(System.Int32 index) { }
        // RVA: 0x0A8C2F98  token: 0x60006D7
        private virtual System.Void System.Collections.ICollection.CopyTo(System.Array array, System.Int32 index) { }

    }

    // TypeToken: 0x20000E4
    public sealed struct JEnumerable`1 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.IEquatable`1
    {
        // Fields
        public static readonly Newtonsoft.Json.Linq.JEnumerable<T> Empty;  // static @ 0x0
        private readonly System.Collections.Generic.IEnumerable<T> _enumerable;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x60006DB
        public System.Void .ctor(System.Collections.Generic.IEnumerable<T> enumerable) { }
        // RVA: -1  // not resolved  token: 0x60006DC
        public virtual System.Collections.Generic.IEnumerator<T> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x60006DD
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x60006DE
        public virtual System.Boolean Equals(Newtonsoft.Json.Linq.JEnumerable<T> other) { }
        // RVA: -1  // not resolved  token: 0x60006DF
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: -1  // not resolved  token: 0x60006E0
        public virtual System.Int32 GetHashCode() { }
        // RVA: -1  // not resolved  token: 0x60006E1
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000E5  // size: 0x68
    public class JObject : Newtonsoft.Json.Linq.JContainer, System.Collections.Generic.IDictionary`2, System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.ComponentModel.INotifyPropertyChanged, System.ComponentModel.ICustomTypeDescriptor
    {
        // Fields
        private readonly Newtonsoft.Json.Linq.JPropertyKeyedCollection _properties;  // 0x50
        private System.ComponentModel.PropertyChangedEventHandler PropertyChanged;  // 0x58
        private System.ComponentModel.PropertyChangingEventHandler PropertyChanging;  // 0x60

        // Properties
        System.Collections.Generic.IList<Newtonsoft.Json.Linq.JToken> ChildrenTokens { get; /* RVA: 0x04D86270 */ }
        Newtonsoft.Json.Linq.JTokenType Type { get; /* RVA: 0x04D85B80 */ }
        Newtonsoft.Json.Linq.JToken Item { get; /* RVA: 0x034268F0 */ }
        Newtonsoft.Json.Linq.JToken Item { get; /* RVA: 0x034268F0 */ set; /* RVA: 0x04CB6A90 */ }
        System.Collections.Generic.ICollection<System.String> System.Collections.Generic.IDictionary<System.String,Newtonsoft.Json.Linq.JToken>.Keys { get; /* RVA: 0x0A8C553C */ }
        System.Collections.Generic.ICollection<Newtonsoft.Json.Linq.JToken> System.Collections.Generic.IDictionary<System.String,Newtonsoft.Json.Linq.JToken>.Values { get; /* RVA: 0x0A8C555C */ }
        System.Boolean System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,Newtonsoft.Json.Linq.JToken>>.IsReadOnly { get; /* RVA: 0x012081B0 */ }

        // Events
        event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        // Methods
        // RVA: 0x03426890  token: 0x60006E5
        public System.Void .ctor() { }
        // RVA: 0x0A8C5A24  token: 0x60006E6
        private System.Void .ctor(Newtonsoft.Json.Linq.JObject other, Newtonsoft.Json.Linq.JsonCloneSettings settings) { }
        // RVA: 0x0A8C4FBC  token: 0x60006E7
        private virtual System.Int32 IndexOfItem(Newtonsoft.Json.Linq.JToken item) { }
        // RVA: 0x03426BF0  token: 0x60006E8
        private virtual System.Boolean InsertItem(System.Int32 index, Newtonsoft.Json.Linq.JToken item, System.Boolean skipParentCheck, System.Boolean copyAnnotations) { }
        // RVA: 0x034279C0  token: 0x60006E9
        private virtual System.Void ValidateToken(Newtonsoft.Json.Linq.JToken o, Newtonsoft.Json.Linq.JToken existing) { }
        // RVA: 0x047C9F40  token: 0x60006EA
        private System.Void InternalPropertyChanged(Newtonsoft.Json.Linq.JProperty childProperty) { }
        // RVA: 0x047C9E80  token: 0x60006EB
        private System.Void InternalPropertyChanging(Newtonsoft.Json.Linq.JProperty childProperty) { }
        // RVA: 0x0A8C4DF8  token: 0x60006EC
        private virtual Newtonsoft.Json.Linq.JToken CloneToken(Newtonsoft.Json.Linq.JsonCloneSettings settings) { }
        // RVA: 0x03F4DBE0  token: 0x60006EE
        public System.Collections.Generic.IEnumerable<Newtonsoft.Json.Linq.JProperty> Properties() { }
        // RVA: 0x03427A60  token: 0x60006EF
        public Newtonsoft.Json.Linq.JProperty Property(System.String name, System.StringComparison comparison) { }
        // RVA: 0x05396944  token: 0x60006F3
        public static Newtonsoft.Json.Linq.JObject Load(Newtonsoft.Json.JsonReader reader) { }
        // RVA: 0x03424AA0  token: 0x60006F4
        public static Newtonsoft.Json.Linq.JObject Load(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Linq.JsonLoadSettings settings) { }
        // RVA: 0x05396938  token: 0x60006F5
        public static Newtonsoft.Json.Linq.JObject Parse(System.String json) { }
        // RVA: 0x03423680  token: 0x60006F6
        public static Newtonsoft.Json.Linq.JObject Parse(System.String json, Newtonsoft.Json.Linq.JsonLoadSettings settings) { }
        // RVA: 0x035A5E50  token: 0x60006F7
        public static Newtonsoft.Json.Linq.JObject FromObject(System.Object o) { }
        // RVA: 0x035A4FB0  token: 0x60006F8
        public static Newtonsoft.Json.Linq.JObject FromObject(System.Object o, Newtonsoft.Json.JsonSerializer jsonSerializer) { }
        // RVA: 0x034266B0  token: 0x60006F9
        public virtual System.Void WriteTo(Newtonsoft.Json.JsonWriter writer, Newtonsoft.Json.JsonConverter[] converters) { }
        // RVA: 0x0A8C4FAC  token: 0x60006FA
        public Newtonsoft.Json.Linq.JToken GetValue(System.String propertyName) { }
        // RVA: 0x0A8C4F80  token: 0x60006FB
        public Newtonsoft.Json.Linq.JToken GetValue(System.String propertyName, System.StringComparison comparison) { }
        // RVA: 0x0A8C4D70  token: 0x60006FC
        public virtual System.Void Add(System.String propertyName, Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x03425800  token: 0x60006FD
        public virtual System.Boolean ContainsKey(System.String propertyName) { }
        // RVA: 0x03428650  token: 0x60006FF
        public virtual System.Boolean Remove(System.String propertyName) { }
        // RVA: 0x03426660  token: 0x6000700
        public virtual System.Boolean TryGetValue(System.String propertyName, Newtonsoft.Json.Linq.JToken& value) { }
        // RVA: 0x0A8C4FE8  token: 0x6000702
        private virtual System.Void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,Newtonsoft.Json.Linq.JToken>>.Add(System.Collections.Generic.KeyValuePair<System.String,Newtonsoft.Json.Linq.JToken> item) { }
        // RVA: 0x0A8C2F04  token: 0x6000703
        private virtual System.Void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,Newtonsoft.Json.Linq.JToken>>.Clear() { }
        // RVA: 0x0A8C507C  token: 0x6000704
        private virtual System.Boolean System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,Newtonsoft.Json.Linq.JToken>>.Contains(System.Collections.Generic.KeyValuePair<System.String,Newtonsoft.Json.Linq.JToken> item) { }
        // RVA: 0x0A8C50E8  token: 0x6000705
        private virtual System.Void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,Newtonsoft.Json.Linq.JToken>>.CopyTo(System.Collections.Generic.KeyValuePair<System.String,Newtonsoft.Json.Linq.JToken>[] array, System.Int32 arrayIndex) { }
        // RVA: 0x0A8C54B4  token: 0x6000707
        private virtual System.Boolean System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,Newtonsoft.Json.Linq.JToken>>.Remove(System.Collections.Generic.KeyValuePair<System.String,Newtonsoft.Json.Linq.JToken> item) { }
        // RVA: 0x0A8C4E68  token: 0x6000708
        public virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,Newtonsoft.Json.Linq.JToken>> GetEnumerator() { }
        // RVA: 0x047C9FD0  token: 0x6000709
        protected virtual System.Void OnPropertyChanged(System.String propertyName) { }
        // RVA: 0x047C9EF0  token: 0x600070A
        protected virtual System.Void OnPropertyChanging(System.String propertyName) { }
        // RVA: 0x0A8C59CC  token: 0x600070B
        private virtual System.ComponentModel.PropertyDescriptorCollection System.ComponentModel.ICustomTypeDescriptor.GetProperties() { }
        // RVA: 0x0A8C5634  token: 0x600070C
        private virtual System.ComponentModel.PropertyDescriptorCollection System.ComponentModel.ICustomTypeDescriptor.GetProperties(System.Attribute[] attributes) { }
        // RVA: 0x0A8C55A8  token: 0x600070D
        private virtual System.ComponentModel.AttributeCollection System.ComponentModel.ICustomTypeDescriptor.GetAttributes() { }
        // RVA: 0x0A8C55F8  token: 0x600070E
        private virtual System.ComponentModel.TypeConverter System.ComponentModel.ICustomTypeDescriptor.GetConverter() { }
        // RVA: 0x0A8C59D4  token: 0x600070F
        private virtual System.Object System.ComponentModel.ICustomTypeDescriptor.GetPropertyOwner(System.ComponentModel.PropertyDescriptor pd) { }
        // RVA: 0x0A8C4EC4  token: 0x6000710
        protected virtual System.Dynamic.DynamicMetaObject GetMetaObject(System.Linq.Expressions.Expression parameter) { }

    }

    // TypeToken: 0x20000E9  // size: 0x60
    public class JProperty : Newtonsoft.Json.Linq.JContainer
    {
        // Fields
        private readonly Newtonsoft.Json.Linq.JProperty.JPropertyList _content;  // 0x50
        private readonly System.String _name;  // 0x58

        // Properties
        System.Collections.Generic.IList<Newtonsoft.Json.Linq.JToken> ChildrenTokens { get; /* RVA: 0x04D86270 */ }
        System.String Name { get; /* RVA: 0x02E56440 */ }
        Newtonsoft.Json.Linq.JToken Value { get; /* RVA: 0x0539250C */ set; /* RVA: 0x04CB6AE0 */ }
        Newtonsoft.Json.Linq.JTokenType Type { get; /* RVA: 0x04D882B0 */ }

        // Methods
        // RVA: 0x0A8C67C4  token: 0x6000723
        private System.Void .ctor(Newtonsoft.Json.Linq.JProperty other, Newtonsoft.Json.Linq.JsonCloneSettings settings) { }
        // RVA: 0x0A8C63BC  token: 0x6000724
        private virtual Newtonsoft.Json.Linq.JToken GetItem(System.Int32 index) { }
        // RVA: 0x047C9DC0  token: 0x6000725
        private virtual System.Void SetItem(System.Int32 index, Newtonsoft.Json.Linq.JToken item) { }
        // RVA: 0x0A8C6704  token: 0x6000726
        private virtual System.Boolean RemoveItem(Newtonsoft.Json.Linq.JToken item) { }
        // RVA: 0x0A8C6644  token: 0x6000727
        private virtual System.Void RemoveItemAt(System.Int32 index) { }
        // RVA: 0x0A8C6418  token: 0x6000728
        private virtual System.Int32 IndexOfItem(Newtonsoft.Json.Linq.JToken item) { }
        // RVA: 0x03426C70  token: 0x6000729
        private virtual System.Boolean InsertItem(System.Int32 index, Newtonsoft.Json.Linq.JToken item, System.Boolean skipParentCheck, System.Boolean copyAnnotations) { }
        // RVA: 0x0A8C63A0  token: 0x600072A
        private virtual System.Boolean ContainsItem(Newtonsoft.Json.Linq.JToken item) { }
        // RVA: 0x0A8C6270  token: 0x600072B
        private virtual System.Void ClearItems() { }
        // RVA: 0x0A8C6330  token: 0x600072C
        private virtual Newtonsoft.Json.Linq.JToken CloneToken(Newtonsoft.Json.Linq.JsonCloneSettings settings) { }
        // RVA: 0x03427C10  token: 0x600072E
        private System.Void .ctor(System.String name) { }
        // RVA: 0x0A8C6854  token: 0x600072F
        public System.Void .ctor(System.String name, System.Object content) { }
        // RVA: 0x03426790  token: 0x6000730
        public virtual System.Void WriteTo(Newtonsoft.Json.JsonWriter writer, Newtonsoft.Json.JsonConverter[] converters) { }
        // RVA: 0x0A8C6448  token: 0x6000731
        public static Newtonsoft.Json.Linq.JProperty Load(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Linq.JsonLoadSettings settings) { }

    }

    // TypeToken: 0x20000EC  // size: 0x88
    public class JPropertyDescriptor : System.ComponentModel.PropertyDescriptor
    {
        // Properties
        System.Type ComponentType { get; /* RVA: 0x0A8C5D28 */ }
        System.Boolean IsReadOnly { get; /* RVA: 0x012081B0 */ }
        System.Type PropertyType { get; /* RVA: 0x0A8C5D78 */ }
        System.Int32 NameHashCode { get; /* RVA: 0x02B70F90 */ }

        // Methods
        // RVA: 0x0A8C5D1C  token: 0x6000747
        public System.Void .ctor(System.String name) { }
        // RVA: 0x012081B0  token: 0x6000748
        public virtual System.Boolean CanResetValue(System.Object component) { }
        // RVA: 0x0A8C5BDC  token: 0x6000749
        public virtual System.Object GetValue(System.Object component) { }
        // RVA: 0x041E1670  token: 0x600074A
        public virtual System.Void ResetValue(System.Object component) { }
        // RVA: 0x0A8C5C44  token: 0x600074B
        public virtual System.Void SetValue(System.Object component, System.Object value) { }
        // RVA: 0x012081B0  token: 0x600074C
        public virtual System.Boolean ShouldSerializeValue(System.Object component) { }

    }

    // TypeToken: 0x20000ED  // size: 0x20
    public class JPropertyKeyedCollection : System.Collections.ObjectModel.Collection`1
    {
        // Fields
        private static readonly System.Collections.Generic.IEqualityComparer<System.String> Comparer;  // static @ 0x0
        private System.Collections.Generic.Dictionary<System.String,Newtonsoft.Json.Linq.JToken> _dictionary;  // 0x18

        // Properties
        System.Collections.Generic.ICollection<System.String> Keys { get; /* RVA: 0x0A8C60FC */ }

        // Methods
        // RVA: 0x03427E80  token: 0x6000751
        public System.Void .ctor() { }
        // RVA: 0x03F010B0  token: 0x6000752
        private System.Void AddKey(System.String key, Newtonsoft.Json.Linq.JToken item) { }
        // RVA: 0x0A8C5DC8  token: 0x6000753
        protected virtual System.Void ClearItems() { }
        // RVA: 0x03425860  token: 0x6000754
        public System.Boolean Contains(System.String key) { }
        // RVA: 0x03F01100  token: 0x6000755
        private System.Void EnsureDictionary() { }
        // RVA: 0x03F01070  token: 0x6000756
        private System.String GetKeyForItem(Newtonsoft.Json.Linq.JToken item) { }
        // RVA: 0x03F00FD0  token: 0x6000757
        protected virtual System.Void InsertItem(System.Int32 index, Newtonsoft.Json.Linq.JToken item) { }
        // RVA: 0x0A8C5EA4  token: 0x6000758
        protected virtual System.Void RemoveItem(System.Int32 index) { }
        // RVA: 0x0A8C5F3C  token: 0x6000759
        private System.Void RemoveKey(System.String key) { }
        // RVA: 0x0A8C5F8C  token: 0x600075A
        protected virtual System.Void SetItem(System.Int32 index, Newtonsoft.Json.Linq.JToken item) { }
        // RVA: 0x03427B20  token: 0x600075B
        public System.Boolean TryGetValue(System.String key, Newtonsoft.Json.Linq.JToken& value) { }
        // RVA: 0x0A8C5E24  token: 0x600075D
        public System.Int32 IndexOfReference(Newtonsoft.Json.Linq.JToken t) { }
        // RVA: 0x04D36ED0  token: 0x600075E
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000EE  // size: 0x40
    public class JRaw : Newtonsoft.Json.Linq.JValue
    {
        // Methods
        // RVA: 0x0A8C6CC8  token: 0x600075F
        private System.Void .ctor(Newtonsoft.Json.Linq.JRaw other, Newtonsoft.Json.Linq.JsonCloneSettings settings) { }
        // RVA: 0x0A8C6CB8  token: 0x6000760
        public System.Void .ctor(System.Object rawJson) { }
        // RVA: 0x0A8C6A04  token: 0x6000761
        public static Newtonsoft.Json.Linq.JRaw Create(Newtonsoft.Json.JsonReader reader) { }
        // RVA: 0x0A8C6994  token: 0x6000762
        private virtual Newtonsoft.Json.Linq.JToken CloneToken(Newtonsoft.Json.Linq.JsonCloneSettings settings) { }

    }

    // TypeToken: 0x20000EF  // size: 0x18
    public class JsonCloneSettings
    {
        // Fields
        private static readonly Newtonsoft.Json.Linq.JsonCloneSettings SkipCopyAnnotations;  // static @ 0x0
        private System.Boolean <CopyAnnotations>k__BackingField;  // 0x10

        // Properties
        System.Boolean CopyAnnotations { get; /* RVA: 0x015EFCE0 */ set; /* RVA: 0x04D86130 */ }

        // Methods
        // RVA: 0x015EFCD0  token: 0x6000763
        public System.Void .ctor() { }
        // RVA: 0x0A8CF7D8  token: 0x6000766
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000F0  // size: 0x20
    public class JsonLoadSettings
    {
        // Fields
        private Newtonsoft.Json.Linq.CommentHandling _commentHandling;  // 0x10
        private Newtonsoft.Json.Linq.LineInfoHandling _lineInfoHandling;  // 0x14
        private Newtonsoft.Json.Linq.DuplicatePropertyNameHandling _duplicatePropertyNameHandling;  // 0x18

        // Properties
        Newtonsoft.Json.Linq.CommentHandling CommentHandling { get; /* RVA: 0x02B2E2D0 */ }
        Newtonsoft.Json.Linq.LineInfoHandling LineInfoHandling { get; /* RVA: 0x04D882A0 */ }
        Newtonsoft.Json.Linq.DuplicatePropertyNameHandling DuplicatePropertyNameHandling { get; /* RVA: 0x011EF5B0 */ }

    }

    // TypeToken: 0x20000F1  // size: 0x30
    public abstract class JToken : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, Newtonsoft.Json.IJsonLineInfo, System.ICloneable, System.Dynamic.IDynamicMetaObjectProvider
    {
        // Fields
        private Newtonsoft.Json.Linq.JContainer _parent;  // 0x10
        private Newtonsoft.Json.Linq.JToken _previous;  // 0x18
        private Newtonsoft.Json.Linq.JToken _next;  // 0x20
        private System.Object _annotations;  // 0x28
        private static readonly Newtonsoft.Json.Linq.JTokenType[] BooleanTypes;  // static @ 0x0
        private static readonly Newtonsoft.Json.Linq.JTokenType[] NumberTypes;  // static @ 0x8
        private static readonly Newtonsoft.Json.Linq.JTokenType[] BigIntegerTypes;  // static @ 0x10
        private static readonly Newtonsoft.Json.Linq.JTokenType[] StringTypes;  // static @ 0x18
        private static readonly Newtonsoft.Json.Linq.JTokenType[] GuidTypes;  // static @ 0x20
        private static readonly Newtonsoft.Json.Linq.JTokenType[] TimeSpanTypes;  // static @ 0x28
        private static readonly Newtonsoft.Json.Linq.JTokenType[] UriTypes;  // static @ 0x30
        private static readonly Newtonsoft.Json.Linq.JTokenType[] CharTypes;  // static @ 0x38
        private static readonly Newtonsoft.Json.Linq.JTokenType[] DateTimeTypes;  // static @ 0x40
        private static readonly Newtonsoft.Json.Linq.JTokenType[] BytesTypes;  // static @ 0x48

        // Properties
        Newtonsoft.Json.Linq.JContainer Parent { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        Newtonsoft.Json.Linq.JToken Root { get; /* RVA: 0x04D7B030 */ }
        Newtonsoft.Json.Linq.JTokenType Type { get; /* RVA: -1  // abstract */ }
        System.Boolean HasValues { get; /* RVA: -1  // abstract */ }
        Newtonsoft.Json.Linq.JToken Next { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }
        Newtonsoft.Json.Linq.JToken Previous { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.String Path { get; /* RVA: 0x0A8C876C */ }
        Newtonsoft.Json.Linq.JToken Item { get; /* RVA: 0x0A8C862C */ }
        Newtonsoft.Json.Linq.JToken First { get; /* RVA: 0x0A8C858C */ }
        Newtonsoft.Json.Linq.JToken Last { get; /* RVA: 0x0A8C86CC */ }
        System.Int32 Newtonsoft.Json.IJsonLineInfo.LineNumber { get; /* RVA: 0x0A8C7F0C */ }
        System.Int32 Newtonsoft.Json.IJsonLineInfo.LinePosition { get; /* RVA: 0x0A8C7F50 */ }

        // Methods
        // RVA: -1  // abstract  token: 0x600076D
        private virtual Newtonsoft.Json.Linq.JToken CloneToken(Newtonsoft.Json.Linq.JsonCloneSettings settings) { }
        // RVA: 0x041E1670  token: 0x6000775
        private System.Void .ctor() { }
        // RVA: -1  // generic def  token: 0x6000777
        public virtual T Value(System.Object key) { }
        // RVA: 0x0A8C7AB0  token: 0x600077A
        public virtual Newtonsoft.Json.Linq.JEnumerable<Newtonsoft.Json.Linq.JToken> Children() { }
        // RVA: 0x0A8C7FE0  token: 0x600077B
        public System.Void Remove() { }
        // RVA: 0x0A8C805C  token: 0x600077C
        public System.Void Replace(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: -1  // abstract  token: 0x600077D
        public virtual System.Void WriteTo(Newtonsoft.Json.JsonWriter writer, Newtonsoft.Json.JsonConverter[] converters) { }
        // RVA: 0x034258C0  token: 0x600077E
        public virtual System.String ToString() { }
        // RVA: 0x03425910  token: 0x600077F
        public System.String ToString(Newtonsoft.Json.Formatting formatting, Newtonsoft.Json.JsonConverter[] converters) { }
        // RVA: 0x0A8C7BF8  token: 0x6000780
        private static Newtonsoft.Json.Linq.JValue EnsureValue(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8C7DD0  token: 0x6000781
        private static System.String GetType(Newtonsoft.Json.Linq.JToken token) { }
        // RVA: 0x0A8C84E8  token: 0x6000782
        private static System.Boolean ValidateToken(Newtonsoft.Json.Linq.JToken o, Newtonsoft.Json.Linq.JTokenType[] validTypes, System.Boolean nullable) { }
        // RVA: 0x0A8CA83C  token: 0x6000783
        public static System.Boolean op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8C99B0  token: 0x6000784
        public static System.DateTimeOffset op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8CBCBC  token: 0x6000785
        public static System.Nullable<System.Boolean> op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8CC690  token: 0x6000786
        public static System.Int64 op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8CB860  token: 0x6000787
        public static System.Nullable<System.DateTime> op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8C9718  token: 0x6000788
        public static System.Nullable<System.DateTimeOffset> op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8CB3F4  token: 0x6000789
        public static System.Nullable<System.Decimal> op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8CCE14  token: 0x600078A
        public static System.Nullable<System.Double> op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8C9DBC  token: 0x600078B
        public static System.Nullable<System.Char> op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8CA3D8  token: 0x600078C
        public static System.Int32 op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8CAC0C  token: 0x600078D
        public static System.Int16 op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8CBEDC  token: 0x600078E
        public static System.UInt16 op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8CC0AC  token: 0x600078F
        public static System.Char op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8CD038  token: 0x6000790
        public static System.Byte op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8CB000  token: 0x6000791
        public static System.SByte op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8CB654  token: 0x6000792
        public static System.Nullable<System.Int32> op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8C9FC4  token: 0x6000793
        public static System.Nullable<System.Int16> op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8CC27C  token: 0x6000794
        public static System.Nullable<System.UInt16> op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8C9134  token: 0x6000795
        public static System.Nullable<System.Byte> op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8CC484  token: 0x6000796
        public static System.Nullable<System.SByte> op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8C9554  token: 0x6000797
        public static System.DateTime op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8CB1D0  token: 0x6000798
        public static System.Nullable<System.Int64> op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8C8D58  token: 0x6000799
        public static System.Nullable<System.Single> op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8C8B64  token: 0x600079A
        public static System.Decimal op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8CA1CC  token: 0x600079B
        public static System.Nullable<System.UInt32> op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8CADDC  token: 0x600079C
        public static System.Nullable<System.UInt64> op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8CCA7C  token: 0x600079D
        public static System.Double op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8C9BEC  token: 0x600079E
        public static System.Single op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8CBA7C  token: 0x600079F
        public static System.String op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8C8F64  token: 0x60007A0
        public static System.UInt32 op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8C8994  token: 0x60007A1
        public static System.UInt64 op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8C9340  token: 0x60007A2
        public static System.Guid op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8CA5A8  token: 0x60007A3
        public static System.Nullable<System.Guid> op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8CCC4C  token: 0x60007A4
        public static System.TimeSpan op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8CC860  token: 0x60007A5
        public static System.Nullable<System.TimeSpan> op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8CAA20  token: 0x60007A6
        public static System.Uri op_Explicit(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8C837C  token: 0x60007A7
        private static System.Numerics.BigInteger ToBigInteger(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8C81B0  token: 0x60007A8
        private static System.Nullable<System.Numerics.BigInteger> ToBigIntegerNullable(Newtonsoft.Json.Linq.JToken value) { }
        // RVA: 0x0A8CD268  token: 0x60007A9
        public static Newtonsoft.Json.Linq.JToken op_Implicit(System.Int32 value) { }
        // RVA: 0x0A8CD208  token: 0x60007AA
        public static Newtonsoft.Json.Linq.JToken op_Implicit(System.String value) { }
        // RVA: 0x0A8C8138  token: 0x60007AB
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: 0x0A8C80D4  token: 0x60007AC
        private virtual System.Collections.Generic.IEnumerator<Newtonsoft.Json.Linq.JToken> System.Collections.Generic.IEnumerable<Newtonsoft.Json.Linq.JToken>.GetEnumerator() { }
        // RVA: 0x0A8C7B8C  token: 0x60007AD
        public Newtonsoft.Json.JsonReader CreateReader() { }
        // RVA: 0x035A4630  token: 0x60007AE
        private static Newtonsoft.Json.Linq.JToken FromObjectInternal(System.Object o, Newtonsoft.Json.JsonSerializer jsonSerializer) { }
        // RVA: 0x0A8C7CB8  token: 0x60007AF
        public static Newtonsoft.Json.Linq.JToken FromObject(System.Object o) { }
        // RVA: -1  // generic def  token: 0x60007B0
        public T ToObject() { }
        // RVA: 0x047915A0  token: 0x60007B1
        public System.Object ToObject(System.Type objectType) { }
        // RVA: 0x04791840  token: 0x60007B2
        public System.Object ToObject(System.Type objectType, Newtonsoft.Json.JsonSerializer jsonSerializer) { }
        // RVA: 0x0A8C7F94  token: 0x60007B3
        public static Newtonsoft.Json.Linq.JToken ReadFrom(Newtonsoft.Json.JsonReader reader) { }
        // RVA: 0x034246E0  token: 0x60007B4
        public static Newtonsoft.Json.Linq.JToken ReadFrom(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Linq.JsonLoadSettings settings) { }
        // RVA: 0x03423DA0  token: 0x60007B5
        public static Newtonsoft.Json.Linq.JToken Parse(System.String json) { }
        // RVA: 0x03423DF0  token: 0x60007B6
        public static Newtonsoft.Json.Linq.JToken Parse(System.String json, Newtonsoft.Json.Linq.JsonLoadSettings settings) { }
        // RVA: 0x03424680  token: 0x60007B7
        public static Newtonsoft.Json.Linq.JToken Load(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Linq.JsonLoadSettings settings) { }
        // RVA: 0x03427520  token: 0x60007B8
        private System.Void SetLineInfo(Newtonsoft.Json.IJsonLineInfo lineInfo, Newtonsoft.Json.Linq.JsonLoadSettings settings) { }
        // RVA: 0x03427670  token: 0x60007B9
        private System.Void SetLineInfo(System.Int32 lineNumber, System.Int32 linePosition) { }
        // RVA: 0x0A8C7ECC  token: 0x60007BA
        private virtual System.Boolean Newtonsoft.Json.IJsonLineInfo.HasLineInfo() { }
        // RVA: 0x0A8C7D10  token: 0x60007BD
        protected virtual System.Dynamic.DynamicMetaObject GetMetaObject(System.Linq.Expressions.Expression parameter) { }
        // RVA: 0x0A8C8174  token: 0x60007BE
        private virtual System.Dynamic.DynamicMetaObject System.Dynamic.IDynamicMetaObjectProvider.GetMetaObject(System.Linq.Expressions.Expression parameter) { }
        // RVA: 0x0A8C7BEC  token: 0x60007BF
        private virtual System.Object System.ICloneable.Clone() { }
        // RVA: 0x0A8C7BEC  token: 0x60007C0
        public Newtonsoft.Json.Linq.JToken DeepClone() { }
        // RVA: 0x034276D0  token: 0x60007C1
        public System.Void AddAnnotation(System.Object annotation) { }
        // RVA: -1  // generic def  token: 0x60007C2
        public T Annotation() { }
        // RVA: 0x0A8C7B00  token: 0x60007C3
        private System.Void CopyAnnotations(Newtonsoft.Json.Linq.JToken target, Newtonsoft.Json.Linq.JToken source) { }
        // RVA: 0x037DA1A0  token: 0x60007C4
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000F3  // size: 0x98
    public class JTokenReader : Newtonsoft.Json.JsonReader, Newtonsoft.Json.IJsonLineInfo
    {
        // Fields
        private readonly Newtonsoft.Json.Linq.JToken _root;  // 0x78
        private System.String _initialPath;  // 0x80
        private Newtonsoft.Json.Linq.JToken _parent;  // 0x88
        private Newtonsoft.Json.Linq.JToken _current;  // 0x90

        // Properties
        Newtonsoft.Json.Linq.JToken CurrentToken { get; /* RVA: 0x04D86220 */ }
        System.Int32 Newtonsoft.Json.IJsonLineInfo.LineNumber { get; /* RVA: 0x0A8C6CF8 */ }
        System.Int32 Newtonsoft.Json.IJsonLineInfo.LinePosition { get; /* RVA: 0x0A8C6D20 */ }
        System.String Path { get; /* RVA: 0x0A8C6D98 */ }

        // Methods
        // RVA: 0x04791CC0  token: 0x60007C7
        public System.Void .ctor(Newtonsoft.Json.Linq.JToken token) { }
        // RVA: 0x03429F10  token: 0x60007C8
        public virtual System.Boolean Read() { }
        // RVA: 0x0342A1D0  token: 0x60007C9
        private System.Boolean ReadOver(Newtonsoft.Json.Linq.JToken t) { }
        // RVA: 0x0A8C6D48  token: 0x60007CA
        private System.Boolean ReadToEnd() { }
        // RVA: 0x0342A330  token: 0x60007CB
        private System.Nullable<Newtonsoft.Json.JsonToken> GetEndToken(Newtonsoft.Json.Linq.JContainer c) { }
        // RVA: 0x03429E90  token: 0x60007CC
        private System.Boolean ReadInto(Newtonsoft.Json.Linq.JContainer c) { }
        // RVA: 0x0342A290  token: 0x60007CD
        private System.Boolean SetEnd(Newtonsoft.Json.Linq.JContainer c) { }
        // RVA: 0x03429FE0  token: 0x60007CE
        private System.Void SetToken(Newtonsoft.Json.Linq.JToken token) { }
        // RVA: 0x0A8C6D7C  token: 0x60007CF
        private System.String SafeToString(System.Object value) { }
        // RVA: 0x0A8C6CD0  token: 0x60007D0
        private virtual System.Boolean Newtonsoft.Json.IJsonLineInfo.HasLineInfo() { }

    }

    // TypeToken: 0x20000F4  // size: 0x14
    public sealed struct JTokenType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.Linq.JTokenType None;  // const
        public static Newtonsoft.Json.Linq.JTokenType Object;  // const
        public static Newtonsoft.Json.Linq.JTokenType Array;  // const
        public static Newtonsoft.Json.Linq.JTokenType Constructor;  // const
        public static Newtonsoft.Json.Linq.JTokenType Property;  // const
        public static Newtonsoft.Json.Linq.JTokenType Comment;  // const
        public static Newtonsoft.Json.Linq.JTokenType Integer;  // const
        public static Newtonsoft.Json.Linq.JTokenType Float;  // const
        public static Newtonsoft.Json.Linq.JTokenType String;  // const
        public static Newtonsoft.Json.Linq.JTokenType Boolean;  // const
        public static Newtonsoft.Json.Linq.JTokenType Null;  // const
        public static Newtonsoft.Json.Linq.JTokenType Undefined;  // const
        public static Newtonsoft.Json.Linq.JTokenType Date;  // const
        public static Newtonsoft.Json.Linq.JTokenType Raw;  // const
        public static Newtonsoft.Json.Linq.JTokenType Bytes;  // const
        public static Newtonsoft.Json.Linq.JTokenType Guid;  // const
        public static Newtonsoft.Json.Linq.JTokenType Uri;  // const
        public static Newtonsoft.Json.Linq.JTokenType TimeSpan;  // const

    }

    // TypeToken: 0x20000F5  // size: 0x80
    public class JTokenWriter : Newtonsoft.Json.JsonWriter
    {
        // Fields
        private Newtonsoft.Json.Linq.JContainer _token;  // 0x60
        private Newtonsoft.Json.Linq.JContainer _parent;  // 0x68
        private Newtonsoft.Json.Linq.JValue _value;  // 0x70
        private Newtonsoft.Json.Linq.JToken _current;  // 0x78

        // Properties
        Newtonsoft.Json.Linq.JToken Token { get; /* RVA: 0x035A4C40 */ }

        // Methods
        // RVA: 0x035A4C60  token: 0x60007D5
        public System.Void .ctor() { }
        // RVA: 0x03779F50  token: 0x60007D6
        public virtual System.Void Close() { }
        // RVA: 0x034247C0  token: 0x60007D7
        public virtual System.Void WriteStartObject() { }
        // RVA: 0x034260B0  token: 0x60007D8
        private System.Void AddParent(Newtonsoft.Json.Linq.JContainer container) { }
        // RVA: 0x04723710  token: 0x60007D9
        private System.Void RemoveParent() { }
        // RVA: 0x03426030  token: 0x60007DA
        public virtual System.Void WriteStartArray() { }
        // RVA: 0x0A8C700C  token: 0x60007DB
        public virtual System.Void WriteStartConstructor(System.String name) { }
        // RVA: 0x04723700  token: 0x60007DC
        protected virtual System.Void WriteEnd(Newtonsoft.Json.JsonToken token) { }
        // RVA: 0x03428670  token: 0x60007DD
        public virtual System.Void WritePropertyName(System.String name) { }
        // RVA: 0x0A8C6E78  token: 0x60007DE
        private System.Void AddRawValue(System.Object value, Newtonsoft.Json.Linq.JTokenType type, Newtonsoft.Json.JsonToken token) { }
        // RVA: 0x03428510  token: 0x60007DF
        private System.Void AddJValue(Newtonsoft.Json.Linq.JValue value, Newtonsoft.Json.JsonToken token) { }
        // RVA: 0x0A8C745C  token: 0x60007E0
        public virtual System.Void WriteValue(System.Object value) { }
        // RVA: 0x0A8C6F58  token: 0x60007E1
        public virtual System.Void WriteNull() { }
        // RVA: 0x0A8C7090  token: 0x60007E2
        public virtual System.Void WriteUndefined() { }
        // RVA: 0x0A8C6F90  token: 0x60007E3
        public virtual System.Void WriteRaw(System.String json) { }
        // RVA: 0x0A8C6F10  token: 0x60007E4
        public virtual System.Void WriteComment(System.String text) { }
        // RVA: 0x03428490  token: 0x60007E5
        public virtual System.Void WriteValue(System.String value) { }
        // RVA: 0x0A8C74E0  token: 0x60007E6
        public virtual System.Void WriteValue(System.Int32 value) { }
        // RVA: 0x0A8C71B0  token: 0x60007E7
        public virtual System.Void WriteValue(System.UInt32 value) { }
        // RVA: 0x034283F0  token: 0x60007E8
        public virtual System.Void WriteValue(System.Int64 value) { }
        // RVA: 0x0A8C734C  token: 0x60007E9
        public virtual System.Void WriteValue(System.UInt64 value) { }
        // RVA: 0x0A8C72C4  token: 0x60007EA
        public virtual System.Void WriteValue(System.Single value) { }
        // RVA: 0x0A8C7974  token: 0x60007EB
        public virtual System.Void WriteValue(System.Double value) { }
        // RVA: 0x03429DF0  token: 0x60007EC
        public virtual System.Void WriteValue(System.Boolean value) { }
        // RVA: 0x0A8C7820  token: 0x60007ED
        public virtual System.Void WriteValue(System.Int16 value) { }
        // RVA: 0x0A8C7720  token: 0x60007EE
        public virtual System.Void WriteValue(System.UInt16 value) { }
        // RVA: 0x0A8C7898  token: 0x60007EF
        public virtual System.Void WriteValue(System.Char value) { }
        // RVA: 0x0A8C713C  token: 0x60007F0
        public virtual System.Void WriteValue(System.Byte value) { }
        // RVA: 0x0A8C70C8  token: 0x60007F1
        public virtual System.Void WriteValue(System.SByte value) { }
        // RVA: 0x0A8C7224  token: 0x60007F2
        public virtual System.Void WriteValue(System.Decimal value) { }
        // RVA: 0x0A8C79FC  token: 0x60007F3
        public virtual System.Void WriteValue(System.DateTime value) { }
        // RVA: 0x0A8C7680  token: 0x60007F4
        public virtual System.Void WriteValue(System.DateTimeOffset value) { }
        // RVA: 0x0A8C7554  token: 0x60007F5
        public virtual System.Void WriteValue(System.Byte[] value) { }
        // RVA: 0x0A8C7798  token: 0x60007F6
        public virtual System.Void WriteValue(System.TimeSpan value) { }
        // RVA: 0x0A8C75E0  token: 0x60007F7
        public virtual System.Void WriteValue(System.Guid value) { }
        // RVA: 0x0A8C73D4  token: 0x60007F8
        public virtual System.Void WriteValue(System.Uri value) { }
        // RVA: 0x03424D40  token: 0x60007F9
        private virtual System.Void WriteToken(Newtonsoft.Json.JsonReader reader, System.Boolean writeChildren, System.Boolean writeDateConstructorAsDate, System.Boolean writeComments) { }

    }

    // TypeToken: 0x20000F6  // size: 0x40
    public class JValue : Newtonsoft.Json.Linq.JToken, System.IEquatable`1, System.IFormattable, System.IComparable, System.IComparable`1, System.IConvertible
    {
        // Fields
        private Newtonsoft.Json.Linq.JTokenType _valueType;  // 0x30
        private System.Object _value;  // 0x38

        // Properties
        System.Boolean HasValues { get; /* RVA: 0x012081B0 */ }
        Newtonsoft.Json.Linq.JTokenType Type { get; /* RVA: 0x04D864F0 */ }
        System.Object Value { get; /* RVA: 0x04D85A50 */ }

        // Methods
        // RVA: 0x03427E30  token: 0x60007FA
        private System.Void .ctor(System.Object value, Newtonsoft.Json.Linq.JTokenType type) { }
        // RVA: 0x0A8CF530  token: 0x60007FB
        private System.Void .ctor(Newtonsoft.Json.Linq.JValue other, Newtonsoft.Json.Linq.JsonCloneSettings settings) { }
        // RVA: 0x03427FC0  token: 0x60007FC
        public System.Void .ctor(System.Int64 value) { }
        // RVA: 0x0A8CF708  token: 0x60007FD
        public System.Void .ctor(System.Decimal value) { }
        // RVA: 0x0A8CF3B0  token: 0x60007FE
        public System.Void .ctor(System.UInt64 value) { }
        // RVA: 0x0A8CF410  token: 0x60007FF
        public System.Void .ctor(System.Double value) { }
        // RVA: 0x0A8CF5E0  token: 0x6000800
        public System.Void .ctor(System.Single value) { }
        // RVA: 0x0A8CF470  token: 0x6000801
        public System.Void .ctor(System.DateTime value) { }
        // RVA: 0x0A8CF774  token: 0x6000802
        public System.Void .ctor(System.DateTimeOffset value) { }
        // RVA: 0x034287A0  token: 0x6000803
        public System.Void .ctor(System.Boolean value) { }
        // RVA: 0x05392538  token: 0x6000804
        public System.Void .ctor(System.String value) { }
        // RVA: 0x0A8CF6A4  token: 0x6000805
        public System.Void .ctor(System.Guid value) { }
        // RVA: 0x0A8CF640  token: 0x6000806
        public System.Void .ctor(System.Uri value) { }
        // RVA: 0x0A8CF4D0  token: 0x6000807
        public System.Void .ctor(System.TimeSpan value) { }
        // RVA: 0x03427CA0  token: 0x6000808
        public System.Void .ctor(System.Object value) { }
        // RVA: 0x0A8CD788  token: 0x600080A
        private static System.Int32 CompareBigInteger(System.Numerics.BigInteger i1, System.Object i2) { }
        // RVA: 0x0A8CDAC0  token: 0x600080B
        private static System.Int32 Compare(Newtonsoft.Json.Linq.JTokenType valueType, System.Object objA, System.Object objB) { }
        // RVA: 0x0A8CD9B4  token: 0x600080C
        private static System.Int32 CompareFloat(System.Object objA, System.Object objB) { }
        // RVA: 0x0A8CE63C  token: 0x600080D
        private static System.Boolean Operation(System.Linq.Expressions.ExpressionType operation, System.Object objA, System.Object objB, System.Object& result) { }
        // RVA: 0x0A8CD718  token: 0x600080E
        private virtual Newtonsoft.Json.Linq.JToken CloneToken(Newtonsoft.Json.Linq.JsonCloneSettings settings) { }
        // RVA: 0x0A8CE3CC  token: 0x600080F
        public static Newtonsoft.Json.Linq.JValue CreateComment(System.String value) { }
        // RVA: 0x0A8CE430  token: 0x6000810
        public static Newtonsoft.Json.Linq.JValue CreateNull() { }
        // RVA: 0x0A8CE488  token: 0x6000811
        public static Newtonsoft.Json.Linq.JValue CreateUndefined() { }
        // RVA: 0x03427D00  token: 0x6000812
        private static Newtonsoft.Json.Linq.JTokenType GetValueType(System.Nullable<Newtonsoft.Json.Linq.JTokenType> current, System.Object value) { }
        // RVA: 0x03427F70  token: 0x6000813
        private static Newtonsoft.Json.Linq.JTokenType GetStringValueType(System.Nullable<Newtonsoft.Json.Linq.JTokenType> current) { }
        // RVA: 0x03426430  token: 0x6000816
        public virtual System.Void WriteTo(Newtonsoft.Json.JsonWriter writer, Newtonsoft.Json.JsonConverter[] converters) { }
        // RVA: 0x0A8CF368  token: 0x6000817
        private static System.Boolean ValuesEquals(Newtonsoft.Json.Linq.JValue v1, Newtonsoft.Json.Linq.JValue v2) { }
        // RVA: 0x0A8CE53C  token: 0x6000818
        public virtual System.Boolean Equals(Newtonsoft.Json.Linq.JValue other) { }
        // RVA: 0x0A8CE4E0  token: 0x6000819
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0A8CE558  token: 0x600081A
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x04D50B60  token: 0x600081B
        public virtual System.String ToString() { }
        // RVA: 0x0A8CF358  token: 0x600081C
        public virtual System.String ToString(System.IFormatProvider formatProvider) { }
        // RVA: 0x0A8CF2AC  token: 0x600081D
        public virtual System.String ToString(System.String format, System.IFormatProvider formatProvider) { }
        // RVA: 0x0A8CE580  token: 0x600081E
        protected virtual System.Dynamic.DynamicMetaObject GetMetaObject(System.Linq.Expressions.Expression parameter) { }
        // RVA: 0x0A8CED84  token: 0x600081F
        private virtual System.Int32 System.IComparable.CompareTo(System.Object obj) { }
        // RVA: 0x0A8CDA8C  token: 0x6000820
        public virtual System.Int32 CompareTo(Newtonsoft.Json.Linq.JValue obj) { }
        // RVA: 0x0A8CEE08  token: 0x6000821
        private virtual System.TypeCode System.IConvertible.GetTypeCode() { }
        // RVA: 0x0A8CEE68  token: 0x6000822
        private virtual System.Boolean System.IConvertible.ToBoolean(System.IFormatProvider provider) { }
        // RVA: 0x0A8CEF00  token: 0x6000823
        private virtual System.Char System.IConvertible.ToChar(System.IFormatProvider provider) { }
        // RVA: 0x0A8CF128  token: 0x6000824
        private virtual System.SByte System.IConvertible.ToSByte(System.IFormatProvider provider) { }
        // RVA: 0x0A8CEEB4  token: 0x6000825
        private virtual System.Byte System.IConvertible.ToByte(System.IFormatProvider provider) { }
        // RVA: 0x0A8CF044  token: 0x6000826
        private virtual System.Int16 System.IConvertible.ToInt16(System.IFormatProvider provider) { }
        // RVA: 0x0A8CF1C8  token: 0x6000827
        private virtual System.UInt16 System.IConvertible.ToUInt16(System.IFormatProvider provider) { }
        // RVA: 0x0A8CF090  token: 0x6000828
        private virtual System.Int32 System.IConvertible.ToInt32(System.IFormatProvider provider) { }
        // RVA: 0x0A8CF214  token: 0x6000829
        private virtual System.UInt32 System.IConvertible.ToUInt32(System.IFormatProvider provider) { }
        // RVA: 0x0A8CF0DC  token: 0x600082A
        private virtual System.Int64 System.IConvertible.ToInt64(System.IFormatProvider provider) { }
        // RVA: 0x0A8CF260  token: 0x600082B
        private virtual System.UInt64 System.IConvertible.ToUInt64(System.IFormatProvider provider) { }
        // RVA: 0x0A8CF174  token: 0x600082C
        private virtual System.Single System.IConvertible.ToSingle(System.IFormatProvider provider) { }
        // RVA: 0x0A8CEFF8  token: 0x600082D
        private virtual System.Double System.IConvertible.ToDouble(System.IFormatProvider provider) { }
        // RVA: 0x0A8CEF98  token: 0x600082E
        private virtual System.Decimal System.IConvertible.ToDecimal(System.IFormatProvider provider) { }
        // RVA: 0x0A8CEF4C  token: 0x600082F
        private virtual System.DateTime System.IConvertible.ToDateTime(System.IFormatProvider provider) { }
        // RVA: 0x0A8CF1C0  token: 0x6000830
        private virtual System.Object System.IConvertible.ToType(System.Type conversionType, System.IFormatProvider provider) { }

    }

    // TypeToken: 0x20000F8  // size: 0x14
    public sealed struct LineInfoHandling
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.Linq.LineInfoHandling Ignore;  // const
        public static Newtonsoft.Json.Linq.LineInfoHandling Load;  // const

    }

}

namespace Newtonsoft.Json.Serialization
{

    // TypeToken: 0x200009C
    public static class CachedAttributeGetter`1
    {
        // Fields
        private static readonly Newtonsoft.Json.Utilities.ThreadSafeStore<System.Object,T> TypeAttributeCache;  // static @ 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000424
        public static T GetAttribute(System.Object type) { }
        // RVA: -1  // not resolved  token: 0x6000425
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200009D  // size: 0x18
    public class CamelCaseNamingStrategy : Newtonsoft.Json.Serialization.NamingStrategy
    {
        // Methods
        // RVA: 0x04DA5CA0  token: 0x6000426
        public System.Void .ctor(System.Boolean processDictionaryKeys, System.Boolean overrideSpecifiedNames) { }
        // RVA: 0x04DA5C90  token: 0x6000427
        public System.Void .ctor(System.Boolean processDictionaryKeys, System.Boolean overrideSpecifiedNames, System.Boolean processExtensionDataNames) { }
        // RVA: 0x041E1670  token: 0x6000428
        public System.Void .ctor() { }
        // RVA: 0x0A8AFEB8  token: 0x6000429
        protected virtual System.String ResolvePropertyName(System.String name) { }

    }

    // TypeToken: 0x200009E  // size: 0x38
    public class DefaultContractResolver : Newtonsoft.Json.Serialization.IContractResolver
    {
        // Fields
        private static readonly Newtonsoft.Json.Serialization.IContractResolver _instance;  // static @ 0x0
        private static readonly System.String[] BlacklistedTypeNames;  // static @ 0x8
        private static readonly Newtonsoft.Json.JsonConverter[] BuiltInConverters;  // static @ 0x10
        private readonly Newtonsoft.Json.DefaultJsonNameTable _nameTable;  // 0x10
        private readonly Newtonsoft.Json.Utilities.ThreadSafeStore<System.Type,Newtonsoft.Json.Serialization.JsonContract> _contractCache;  // 0x18
        private System.Reflection.BindingFlags <DefaultMembersSearchFlags>k__BackingField;  // 0x20
        private System.Boolean <SerializeCompilerGeneratedMembers>k__BackingField;  // 0x24
        private System.Boolean <IgnoreSerializableInterface>k__BackingField;  // 0x25
        private System.Boolean <IgnoreSerializableAttribute>k__BackingField;  // 0x26
        private System.Boolean <IgnoreIsSpecifiedMembers>k__BackingField;  // 0x27
        private System.Boolean <IgnoreShouldSerializeMembers>k__BackingField;  // 0x28
        private Newtonsoft.Json.Serialization.NamingStrategy <NamingStrategy>k__BackingField;  // 0x30

        // Properties
        Newtonsoft.Json.Serialization.IContractResolver Instance { get; /* RVA: 0x0A8B67F8 */ }
        System.Reflection.BindingFlags DefaultMembersSearchFlags { get; /* RVA: 0x02B70F90 */ set; /* RVA: 0x04D86330 */ }
        System.Boolean SerializeCompilerGeneratedMembers { get; /* RVA: 0x04D86870 */ }
        System.Boolean IgnoreSerializableInterface { get; /* RVA: 0x04D86930 */ }
        System.Boolean IgnoreSerializableAttribute { get; /* RVA: 0x04D86940 */ set; /* RVA: 0x04D86960 */ }
        System.Boolean IgnoreIsSpecifiedMembers { get; /* RVA: 0x04DA5CB0 */ }
        System.Boolean IgnoreShouldSerializeMembers { get; /* RVA: 0x011F33C0 */ }
        Newtonsoft.Json.Serialization.NamingStrategy NamingStrategy { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }

        // Methods
        // RVA: 0x0464D460  token: 0x6000435
        public System.Void .ctor() { }
        // RVA: 0x02FBE740  token: 0x6000436
        public virtual Newtonsoft.Json.Serialization.JsonContract ResolveContract(System.Type type) { }
        // RVA: 0x032B3490  token: 0x6000437
        private static System.Boolean FilterMembers(System.Reflection.MemberInfo member) { }
        // RVA: 0x02F30F80  token: 0x6000438
        protected virtual System.Collections.Generic.List<System.Reflection.MemberInfo> GetSerializableMembers(System.Type objectType) { }
        // RVA: 0x0A8B6694  token: 0x6000439
        private System.Boolean ShouldSerializeEntityMember(System.Reflection.MemberInfo memberInfo) { }
        // RVA: 0x035CE720  token: 0x600043A
        protected virtual Newtonsoft.Json.Serialization.JsonObjectContract CreateObjectContract(System.Type objectType) { }
        // RVA: 0x0A8B6750  token: 0x600043B
        private static System.Void ThrowUnableToSerializeError(System.Object o, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x035CEB30  token: 0x600043C
        private System.Reflection.MemberInfo GetExtensionDataMemberForType(System.Type type) { }
        // RVA: 0x0A8B5E64  token: 0x600043D
        private static System.Void SetExtensionDataDelegates(Newtonsoft.Json.Serialization.JsonObjectContract contract, System.Reflection.MemberInfo member) { }
        // RVA: 0x035CE410  token: 0x600043E
        private System.Reflection.ConstructorInfo GetAttributeConstructor(System.Type objectType) { }
        // RVA: 0x043AA090  token: 0x600043F
        private System.Reflection.ConstructorInfo GetImmutableConstructor(System.Type objectType, Newtonsoft.Json.Serialization.JsonPropertyCollection memberProperties) { }
        // RVA: 0x04CDE4B0  token: 0x6000440
        private System.Reflection.ConstructorInfo GetParameterizedConstructor(System.Type objectType) { }
        // RVA: 0x043A9F60  token: 0x6000441
        protected virtual System.Collections.Generic.IList<Newtonsoft.Json.Serialization.JsonProperty> CreateConstructorParameters(System.Reflection.ConstructorInfo constructor, Newtonsoft.Json.Serialization.JsonPropertyCollection memberProperties) { }
        // RVA: 0x043AA210  token: 0x6000442
        private Newtonsoft.Json.Serialization.JsonProperty MatchProperty(Newtonsoft.Json.Serialization.JsonPropertyCollection properties, System.String name, System.Type type) { }
        // RVA: 0x043AA2C0  token: 0x6000443
        protected virtual Newtonsoft.Json.Serialization.JsonProperty CreatePropertyFromConstructorParameter(Newtonsoft.Json.Serialization.JsonProperty matchingMemberProperty, System.Reflection.ParameterInfo parameterInfo) { }
        // RVA: 0x035CF340  token: 0x6000444
        protected virtual Newtonsoft.Json.JsonConverter ResolveContractConverter(System.Type objectType) { }
        // RVA: 0x035CF4F0  token: 0x6000445
        private System.Func<System.Object> GetDefaultCreator(System.Type createdType) { }
        // RVA: 0x035CF130  token: 0x6000446
        private System.Void InitializeContract(Newtonsoft.Json.Serialization.JsonContract contract) { }
        // RVA: 0x035CF370  token: 0x6000447
        private System.Void ResolveCallbackMethods(Newtonsoft.Json.Serialization.JsonContract contract, System.Type t) { }
        // RVA: 0x02F2FB00  token: 0x6000448
        private System.Void GetCallbackMethodsForType(System.Type type, System.Collections.Generic.List<Newtonsoft.Json.Serialization.SerializationCallback>& onSerializing, System.Collections.Generic.List<Newtonsoft.Json.Serialization.SerializationCallback>& onSerialized, System.Collections.Generic.List<Newtonsoft.Json.Serialization.SerializationCallback>& onDeserializing, System.Collections.Generic.List<Newtonsoft.Json.Serialization.SerializationCallback>& onDeserialized, System.Collections.Generic.List<Newtonsoft.Json.Serialization.SerializationErrorCallback>& onError) { }
        // RVA: 0x035CFCB0  token: 0x6000449
        private static System.Boolean IsConcurrentOrObservableCollection(System.Type t) { }
        // RVA: 0x035CFA20  token: 0x600044A
        private static System.Boolean ShouldSkipDeserialized(System.Type t) { }
        // RVA: 0x035CFA20  token: 0x600044B
        private static System.Boolean ShouldSkipSerializing(System.Type t) { }
        // RVA: 0x02F2E080  token: 0x600044C
        private System.Collections.Generic.List<System.Type> GetClassHierarchyForType(System.Type type) { }
        // RVA: 0x035CDAA0  token: 0x600044D
        protected virtual Newtonsoft.Json.Serialization.JsonDictionaryContract CreateDictionaryContract(System.Type objectType) { }
        // RVA: 0x035CDCD0  token: 0x600044E
        protected virtual Newtonsoft.Json.Serialization.JsonArrayContract CreateArrayContract(System.Type objectType) { }
        // RVA: 0x035CDE00  token: 0x600044F
        protected virtual Newtonsoft.Json.Serialization.JsonPrimitiveContract CreatePrimitiveContract(System.Type objectType) { }
        // RVA: 0x04D26960  token: 0x6000450
        protected virtual Newtonsoft.Json.Serialization.JsonLinqContract CreateLinqContract(System.Type objectType) { }
        // RVA: 0x0A8B5B90  token: 0x6000451
        protected virtual Newtonsoft.Json.Serialization.JsonISerializableContract CreateISerializableContract(System.Type objectType) { }
        // RVA: 0x0A8B59A0  token: 0x6000452
        protected virtual Newtonsoft.Json.Serialization.JsonDynamicContract CreateDynamicContract(System.Type objectType) { }
        // RVA: 0x04CCFB90  token: 0x6000453
        protected virtual Newtonsoft.Json.Serialization.JsonStringContract CreateStringContract(System.Type objectType) { }
        // RVA: 0x035D07D0  token: 0x6000454
        protected virtual Newtonsoft.Json.Serialization.JsonContract CreateContract(System.Type objectType) { }
        // RVA: 0x035D0CC0  token: 0x6000455
        private static System.Boolean IsJsonPrimitiveType(System.Type t) { }
        // RVA: 0x035CE690  token: 0x6000456
        private static System.Boolean IsIConvertible(System.Type t) { }
        // RVA: 0x035CED80  token: 0x6000457
        private static System.Boolean CanConvertToString(System.Type type) { }
        // RVA: 0x02F30CB0  token: 0x6000458
        private static System.Boolean IsValidCallback(System.Reflection.MethodInfo method, System.Reflection.ParameterInfo[] parameters, System.Type attributeType, System.Reflection.MethodInfo currentCallback, System.Type& prevAttributeType) { }
        // RVA: 0x0A8B5D6C  token: 0x6000459
        private static System.String GetClrTypeFullName(System.Type type) { }
        // RVA: 0x032B3570  token: 0x600045A
        protected virtual System.Collections.Generic.IList<Newtonsoft.Json.Serialization.JsonProperty> CreateProperties(System.Type type, Newtonsoft.Json.MemberSerialization memberSerialization) { }
        // RVA: 0x02B2ECC0  token: 0x600045B
        private virtual Newtonsoft.Json.DefaultJsonNameTable GetNameTable() { }
        // RVA: 0x032203A0  token: 0x600045C
        protected virtual Newtonsoft.Json.Serialization.IValueProvider CreateMemberValueProvider(System.Reflection.MemberInfo member) { }
        // RVA: 0x03220060  token: 0x600045D
        protected virtual Newtonsoft.Json.Serialization.JsonProperty CreateProperty(System.Reflection.MemberInfo member, Newtonsoft.Json.MemberSerialization memberSerialization) { }
        // RVA: 0x032B41E0  token: 0x600045E
        private System.Void SetPropertySettingsFromAttributes(Newtonsoft.Json.Serialization.JsonProperty property, System.Object attributeProvider, System.String name, System.Type declaringType, Newtonsoft.Json.MemberSerialization memberSerialization, System.Boolean& allowNonPublicAccess) { }
        // RVA: 0x03220500  token: 0x600045F
        private System.Predicate<System.Object> CreateShouldSerializeTest(System.Reflection.MemberInfo member) { }
        // RVA: 0x032206A0  token: 0x6000460
        private System.Void SetIsSpecifiedActions(Newtonsoft.Json.Serialization.JsonProperty property, System.Reflection.MemberInfo member, System.Boolean allowNonPublicAccess) { }
        // RVA: 0x032B4800  token: 0x6000461
        protected virtual System.String ResolvePropertyName(System.String propertyName) { }
        // RVA: 0x0A8B5E48  token: 0x6000462
        protected virtual System.String ResolveExtensionDataName(System.String extensionDataName) { }
        // RVA: 0x048BDB70  token: 0x6000463
        protected virtual System.String ResolveDictionaryKey(System.String dictionaryKey) { }
        // RVA: 0x0A8B5E40  token: 0x6000464
        public System.String GetResolvedPropertyName(System.String propertyName) { }
        // RVA: 0x0464D020  token: 0x6000465
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000AA  // size: 0x18
    public class DefaultNamingStrategy : Newtonsoft.Json.Serialization.NamingStrategy
    {
        // Methods
        // RVA: 0x04D88C30  token: 0x6000487
        protected virtual System.String ResolvePropertyName(System.String name) { }
        // RVA: 0x041E1670  token: 0x6000488
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000AB  // size: 0x18
    public class DefaultReferenceResolver : Newtonsoft.Json.Serialization.IReferenceResolver
    {
        // Fields
        private System.Int32 _referenceCount;  // 0x10

        // Methods
        // RVA: 0x0A8BAF3C  token: 0x6000489
        private Newtonsoft.Json.Utilities.BidirectionalDictionary<System.String,System.Object> GetMappings(System.Object context) { }
        // RVA: 0x0A8BB164  token: 0x600048A
        public virtual System.Object ResolveReference(System.Object context, System.String reference) { }
        // RVA: 0x0A8BB008  token: 0x600048B
        public virtual System.String GetReference(System.Object context, System.Object value) { }
        // RVA: 0x0A8BAEBC  token: 0x600048C
        public virtual System.Void AddReference(System.Object context, System.String reference, System.Object value) { }
        // RVA: 0x0A8BB0E8  token: 0x600048D
        public virtual System.Boolean IsReferenced(System.Object context, System.Object value) { }
        // RVA: 0x041E1670  token: 0x600048E
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000AC  // size: 0x18
    public class DefaultSerializationBinder : System.Runtime.Serialization.SerializationBinder, Newtonsoft.Json.Serialization.ISerializationBinder
    {
        // Fields
        private static readonly Newtonsoft.Json.Serialization.DefaultSerializationBinder Instance;  // static @ 0x0
        private readonly Newtonsoft.Json.Utilities.ThreadSafeStore<Newtonsoft.Json.Utilities.StructMultiKey<System.String,System.String>,System.Type> _typeCache;  // 0x10

        // Methods
        // RVA: 0x04B460F0  token: 0x600048F
        public System.Void .ctor() { }
        // RVA: 0x03B110C0  token: 0x6000490
        private System.Type GetTypeFromTypeNameKey(Newtonsoft.Json.Utilities.StructMultiKey<System.String,System.String> typeNameKey) { }
        // RVA: 0x0A8BB258  token: 0x6000491
        private System.Type GetGenericTypeFromTypeName(System.String typeName, System.Reflection.Assembly assembly) { }
        // RVA: 0x02FBD550  token: 0x6000492
        private System.Type GetTypeByName(Newtonsoft.Json.Utilities.StructMultiKey<System.String,System.String> typeNameKey) { }
        // RVA: 0x02FBD4E0  token: 0x6000493
        public virtual System.Type BindToType(System.String assemblyName, System.String typeName) { }
        // RVA: 0x0A8BB1E4  token: 0x6000494
        public virtual System.Void BindToName(System.Type serializedType, System.String& assemblyName, System.String& typeName) { }
        // RVA: 0x04B46080  token: 0x6000495
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000AD  // size: 0x40
    public class ErrorContext
    {
        // Fields
        private System.Boolean <Traced>k__BackingField;  // 0x10
        private readonly System.Exception <Error>k__BackingField;  // 0x18
        private readonly System.Object <OriginalObject>k__BackingField;  // 0x20
        private readonly System.Object <Member>k__BackingField;  // 0x28
        private readonly System.String <Path>k__BackingField;  // 0x30
        private System.Boolean <Handled>k__BackingField;  // 0x38

        // Properties
        System.Boolean Traced { get; /* RVA: 0x015EFCE0 */ set; /* RVA: 0x04D86130 */ }
        System.Exception Error { get; /* RVA: 0x0385B100 */ }
        System.Boolean Handled { get; /* RVA: 0x011F2A70 */ }

        // Methods
        // RVA: 0x0A8BB468  token: 0x6000496
        private System.Void .ctor(System.Object originalObject, System.Object member, System.String path, System.Exception error) { }

    }

    // TypeToken: 0x20000AE  // size: 0x20
    public class ErrorEventArgs : System.EventArgs
    {
        // Fields
        private readonly System.Object <CurrentObject>k__BackingField;  // 0x10
        private readonly Newtonsoft.Json.Serialization.ErrorContext <ErrorContext>k__BackingField;  // 0x18

        // Methods
        // RVA: 0x0A8BB4B0  token: 0x600049B
        public System.Void .ctor(System.Object currentObject, Newtonsoft.Json.Serialization.ErrorContext errorContext) { }

    }

    // TypeToken: 0x20000AF
    public interface IAttributeProvider
    {
    }

    // TypeToken: 0x20000B0
    public interface IContractResolver
    {
        // Methods
        // RVA: -1  // abstract  token: 0x600049C
        public virtual Newtonsoft.Json.Serialization.JsonContract ResolveContract(System.Type type) { }

    }

    // TypeToken: 0x20000B1
    public interface IReferenceResolver
    {
        // Methods
        // RVA: -1  // abstract  token: 0x600049D
        public virtual System.Object ResolveReference(System.Object context, System.String reference) { }
        // RVA: -1  // abstract  token: 0x600049E
        public virtual System.String GetReference(System.Object context, System.Object value) { }
        // RVA: -1  // abstract  token: 0x600049F
        public virtual System.Boolean IsReferenced(System.Object context, System.Object value) { }
        // RVA: -1  // abstract  token: 0x60004A0
        public virtual System.Void AddReference(System.Object context, System.String reference, System.Object value) { }

    }

    // TypeToken: 0x20000B2
    public interface ISerializationBinder
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60004A1
        public virtual System.Type BindToType(System.String assemblyName, System.String typeName) { }
        // RVA: -1  // abstract  token: 0x60004A2
        public virtual System.Void BindToName(System.Type serializedType, System.String& assemblyName, System.String& typeName) { }

    }

    // TypeToken: 0x20000B3
    public interface ITraceWriter
    {
        // Properties
        System.Diagnostics.TraceLevel LevelFilter { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x60004A4
        public virtual System.Void Trace(System.Diagnostics.TraceLevel level, System.String message, System.Exception ex) { }

    }

    // TypeToken: 0x20000B4
    public interface IValueProvider
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60004A5
        public virtual System.Void SetValue(System.Object target, System.Object value) { }
        // RVA: -1  // abstract  token: 0x60004A6
        public virtual System.Object GetValue(System.Object target) { }

    }

    // TypeToken: 0x20000B5  // size: 0x118
    public class JsonArrayContract : Newtonsoft.Json.Serialization.JsonContainerContract
    {
        // Fields
        private readonly System.Type <CollectionItemType>k__BackingField;  // 0xc0
        private readonly System.Boolean <IsMultidimensionalArray>k__BackingField;  // 0xc8
        private readonly System.Type _genericCollectionDefinitionType;  // 0xd0
        private System.Type _genericWrapperType;  // 0xd8
        private Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> _genericWrapperCreator;  // 0xe0
        private System.Func<System.Object> _genericTemporaryCollectionCreator;  // 0xe8
        private readonly System.Boolean <IsArray>k__BackingField;  // 0xf0
        private readonly System.Boolean <ShouldCreateWrapper>k__BackingField;  // 0xf1
        private System.Boolean <CanDeserialize>k__BackingField;  // 0xf2
        private readonly System.Reflection.ConstructorInfo _parameterizedConstructor;  // 0xf8
        private Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> _parameterizedCreator;  // 0x100
        private Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> _overrideCreator;  // 0x108
        private System.Boolean <HasParameterizedCreator>k__BackingField;  // 0x110

        // Properties
        System.Type CollectionItemType { get; /* RVA: 0x04D892F0 */ }
        System.Boolean IsMultidimensionalArray { get; /* RVA: 0x04D8E270 */ }
        System.Boolean IsArray { get; /* RVA: 0x04D8E520 */ }
        System.Boolean ShouldCreateWrapper { get; /* RVA: 0x04D9B5F0 */ }
        System.Boolean CanDeserialize { get; /* RVA: 0x04DA5CC0 */ set; /* RVA: 0x04DA5CD0 */ }
        Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> ParameterizedCreator { get; /* RVA: 0x0A8BB71C */ }
        Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> OverrideCreator { get; /* RVA: 0x04D85F90 */ set; /* RVA: 0x0A8BB7D4 */ }
        System.Boolean HasParameterizedCreator { get; /* RVA: 0x04D90FE0 */ set; /* RVA: 0x04D91070 */ }
        System.Boolean HasParameterizedCreatorInternal { get; /* RVA: 0x032B5600 */ }

        // Methods
        // RVA: 0x035CD250  token: 0x60004B3
        public System.Void .ctor(System.Type underlyingType) { }
        // RVA: 0x03B2C5A0  token: 0x60004B4
        private Newtonsoft.Json.Utilities.IWrappedCollection CreateWrapper(System.Object list) { }
        // RVA: 0x032B5730  token: 0x60004B5
        private System.Collections.IList CreateTemporaryCollection() { }
        // RVA: 0x0A8BB644  token: 0x60004B6
        private System.Void StoreFSharpListCreatorIfNecessary(System.Type underlyingType) { }

    }

    // TypeToken: 0x20000B6  // size: 0xC0
    public class JsonContainerContract : Newtonsoft.Json.Serialization.JsonContract
    {
        // Fields
        private Newtonsoft.Json.Serialization.JsonContract _itemContract;  // 0x90
        private Newtonsoft.Json.Serialization.JsonContract _finalItemContract;  // 0x98
        private Newtonsoft.Json.JsonConverter <ItemConverter>k__BackingField;  // 0xa0
        private System.Nullable<System.Boolean> <ItemIsReference>k__BackingField;  // 0xa8
        private System.Nullable<Newtonsoft.Json.ReferenceLoopHandling> <ItemReferenceLoopHandling>k__BackingField;  // 0xac
        private System.Nullable<Newtonsoft.Json.TypeNameHandling> <ItemTypeNameHandling>k__BackingField;  // 0xb4

        // Properties
        Newtonsoft.Json.Serialization.JsonContract ItemContract { get; /* RVA: 0x04D86220 */ set; /* RVA: 0x043C1090 */ }
        Newtonsoft.Json.Serialization.JsonContract FinalItemContract { get; /* RVA: 0x04D85EE0 */ }
        Newtonsoft.Json.JsonConverter ItemConverter { get; /* RVA: 0x04D86210 */ set; /* RVA: 0x05391334 */ }
        System.Nullable<System.Boolean> ItemIsReference { get; /* RVA: 0x04DA5CE0 */ set; /* RVA: 0x04DA5D10 */ }
        System.Nullable<Newtonsoft.Json.ReferenceLoopHandling> ItemReferenceLoopHandling { get; /* RVA: 0x04DA5CF0 */ set; /* RVA: 0x04DA5D20 */ }
        System.Nullable<Newtonsoft.Json.TypeNameHandling> ItemTypeNameHandling { get; /* RVA: 0x04DA5D00 */ set; /* RVA: 0x04DA5D30 */ }

        // Methods
        // RVA: 0x035CF090  token: 0x60004C2
        private System.Void .ctor(System.Type underlyingType) { }

    }

    // TypeToken: 0x20000B7  // size: 0x14
    public sealed struct JsonContractType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.Serialization.JsonContractType None;  // const
        public static Newtonsoft.Json.Serialization.JsonContractType Object;  // const
        public static Newtonsoft.Json.Serialization.JsonContractType Array;  // const
        public static Newtonsoft.Json.Serialization.JsonContractType Primitive;  // const
        public static Newtonsoft.Json.Serialization.JsonContractType String;  // const
        public static Newtonsoft.Json.Serialization.JsonContractType Dictionary;  // const
        public static Newtonsoft.Json.Serialization.JsonContractType Dynamic;  // const
        public static Newtonsoft.Json.Serialization.JsonContractType Serializable;  // const
        public static Newtonsoft.Json.Serialization.JsonContractType Linq;  // const

    }

    // TypeToken: 0x20000B8  // size: 0x80
    public sealed class SerializationCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x045D8FF0  token: 0x60004C3
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x03A1B910  token: 0x60004C4
        public virtual System.Void Invoke(System.Object o, System.Runtime.Serialization.StreamingContext context) { }

    }

    // TypeToken: 0x20000B9  // size: 0x80
    public sealed class SerializationErrorCallback : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0966D3A4  token: 0x60004C5
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x0621CA6C  token: 0x60004C6
        public virtual System.Void Invoke(System.Object o, System.Runtime.Serialization.StreamingContext context, Newtonsoft.Json.Serialization.ErrorContext errorContext) { }

    }

    // TypeToken: 0x20000BA  // size: 0x80
    public sealed class ExtensionDataSetter : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0A8BB51C  token: 0x60004C7
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x01546280  token: 0x60004C8
        public virtual System.Void Invoke(System.Object o, System.String key, System.Object value) { }

    }

    // TypeToken: 0x20000BB  // size: 0x80
    public sealed class ExtensionDataGetter : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0832435C  token: 0x60004C9
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x014E2060  token: 0x60004CA
        public virtual System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Object,System.Object>> Invoke(System.Object o) { }

    }

    // TypeToken: 0x20000BC  // size: 0x90
    public abstract class JsonContract
    {
        // Fields
        private System.Boolean IsNullable;  // 0x10
        private System.Boolean IsConvertable;  // 0x11
        private System.Boolean IsEnum;  // 0x12
        private System.Type NonNullableUnderlyingType;  // 0x18
        private Newtonsoft.Json.ReadType InternalReadType;  // 0x20
        private Newtonsoft.Json.Serialization.JsonContractType ContractType;  // 0x24
        private System.Boolean IsReadOnlyOrFixedSize;  // 0x28
        private System.Boolean IsSealed;  // 0x29
        private System.Boolean IsInstantiable;  // 0x2a
        private System.Collections.Generic.List<Newtonsoft.Json.Serialization.SerializationCallback> _onDeserializedCallbacks;  // 0x30
        private System.Collections.Generic.List<Newtonsoft.Json.Serialization.SerializationCallback> _onDeserializingCallbacks;  // 0x38
        private System.Collections.Generic.List<Newtonsoft.Json.Serialization.SerializationCallback> _onSerializedCallbacks;  // 0x40
        private System.Collections.Generic.List<Newtonsoft.Json.Serialization.SerializationCallback> _onSerializingCallbacks;  // 0x48
        private System.Collections.Generic.List<Newtonsoft.Json.Serialization.SerializationErrorCallback> _onErrorCallbacks;  // 0x50
        private System.Type _createdType;  // 0x58
        private readonly System.Type <UnderlyingType>k__BackingField;  // 0x60
        private System.Nullable<System.Boolean> <IsReference>k__BackingField;  // 0x68
        private Newtonsoft.Json.JsonConverter <Converter>k__BackingField;  // 0x70
        private Newtonsoft.Json.JsonConverter <InternalConverter>k__BackingField;  // 0x78
        private System.Func<System.Object> <DefaultCreator>k__BackingField;  // 0x80
        private System.Boolean <DefaultCreatorNonPublic>k__BackingField;  // 0x88

        // Properties
        System.Type UnderlyingType { get; /* RVA: 0x04D86280 */ }
        System.Type CreatedType { get; /* RVA: 0x02E56440 */ set; /* RVA: 0x035CF820 */ }
        System.Nullable<System.Boolean> IsReference { get; /* RVA: 0x04DA5D40 */ set; /* RVA: 0x04DA5D50 */ }
        Newtonsoft.Json.JsonConverter Converter { get; /* RVA: 0x04D862B0 */ set; /* RVA: 0x063AD760 */ }
        Newtonsoft.Json.JsonConverter InternalConverter { get; /* RVA: 0x04D862A0 */ set; /* RVA: 0x06402320 */ }
        System.Collections.Generic.IList<Newtonsoft.Json.Serialization.SerializationCallback> OnDeserializedCallbacks { get; /* RVA: 0x0484C760 */ }
        System.Collections.Generic.IList<Newtonsoft.Json.Serialization.SerializationCallback> OnDeserializingCallbacks { get; /* RVA: 0x0A8BB9FC */ }
        System.Collections.Generic.IList<Newtonsoft.Json.Serialization.SerializationCallback> OnSerializedCallbacks { get; /* RVA: 0x0A8BBA80 */ }
        System.Collections.Generic.IList<Newtonsoft.Json.Serialization.SerializationCallback> OnSerializingCallbacks { get; /* RVA: 0x032B56D0 */ }
        System.Collections.Generic.IList<Newtonsoft.Json.Serialization.SerializationErrorCallback> OnErrorCallbacks { get; /* RVA: 0x032B5670 */ }
        System.Func<System.Object> DefaultCreator { get; /* RVA: 0x04D86260 */ set; /* RVA: 0x04CD9970 */ }
        System.Boolean DefaultCreatorNonPublic { get; /* RVA: 0x011F2750 */ set; /* RVA: 0x011F2760 */ }

        // Methods
        // RVA: 0x035D0B60  token: 0x60004DD
        private System.Void .ctor(System.Type underlyingType) { }
        // RVA: 0x04386DC0  token: 0x60004DE
        private System.Void InvokeOnSerializing(System.Object o, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x04386C90  token: 0x60004DF
        private System.Void InvokeOnSerialized(System.Object o, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x032B6CF0  token: 0x60004E0
        private System.Void InvokeOnDeserializing(System.Object o, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x032B6E30  token: 0x60004E1
        private System.Void InvokeOnDeserialized(System.Object o, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x0A8BB89C  token: 0x60004E2
        private System.Void InvokeOnError(System.Object o, System.Runtime.Serialization.StreamingContext context, Newtonsoft.Json.Serialization.ErrorContext errorContext) { }
        // RVA: 0x045D8F40  token: 0x60004E3
        private static Newtonsoft.Json.Serialization.SerializationCallback CreateSerializationCallback(System.Reflection.MethodInfo callbackMethodInfo) { }
        // RVA: 0x0A8BB7FC  token: 0x60004E4
        private static Newtonsoft.Json.Serialization.SerializationErrorCallback CreateSerializationErrorCallback(System.Reflection.MethodInfo callbackMethodInfo) { }

    }

    // TypeToken: 0x20000BF  // size: 0x128
    public class JsonDictionaryContract : Newtonsoft.Json.Serialization.JsonContainerContract
    {
        // Fields
        private System.Func<System.String,System.String> <DictionaryKeyResolver>k__BackingField;  // 0xc0
        private readonly System.Type <DictionaryKeyType>k__BackingField;  // 0xc8
        private readonly System.Type <DictionaryValueType>k__BackingField;  // 0xd0
        private Newtonsoft.Json.Serialization.JsonContract <KeyContract>k__BackingField;  // 0xd8
        private readonly System.Type _genericCollectionDefinitionType;  // 0xe0
        private System.Type _genericWrapperType;  // 0xe8
        private Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> _genericWrapperCreator;  // 0xf0
        private System.Func<System.Object> _genericTemporaryDictionaryCreator;  // 0xf8
        private readonly System.Boolean <ShouldCreateWrapper>k__BackingField;  // 0x100
        private readonly System.Reflection.ConstructorInfo _parameterizedConstructor;  // 0x108
        private Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> _overrideCreator;  // 0x110
        private Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> _parameterizedCreator;  // 0x118
        private System.Boolean <HasParameterizedCreator>k__BackingField;  // 0x120

        // Properties
        System.Func<System.String,System.String> DictionaryKeyResolver { get; /* RVA: 0x04D892F0 */ set; /* RVA: 0x06AC5ACC */ }
        System.Type DictionaryKeyType { get; /* RVA: 0x04D8D1D0 */ }
        System.Type DictionaryValueType { get; /* RVA: 0x04D876B0 */ }
        Newtonsoft.Json.Serialization.JsonContract KeyContract { get; /* RVA: 0x04D88EF0 */ set; /* RVA: 0x06902698 */ }
        System.Boolean ShouldCreateWrapper { get; /* RVA: 0x04DA5D60 */ }
        Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> ParameterizedCreator { get; /* RVA: 0x0A8BBF20 */ }
        Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> OverrideCreator { get; /* RVA: 0x04D8D1B0 */ set; /* RVA: 0x05397170 */ }
        System.Boolean HasParameterizedCreator { get; /* RVA: 0x04D8FE70 */ set; /* RVA: 0x04D8FEA0 */ }
        System.Boolean HasParameterizedCreatorInternal { get; /* RVA: 0x044FE340 */ }

        // Methods
        // RVA: 0x03897F20  token: 0x60004F6
        public System.Void .ctor(System.Type underlyingType) { }
        // RVA: 0x0A8BBCE0  token: 0x60004F7
        private Newtonsoft.Json.Utilities.IWrappedDictionary CreateWrapper(System.Object dictionary) { }
        // RVA: 0x0A8BBB04  token: 0x60004F8
        private System.Collections.IDictionary CreateTemporaryDictionary() { }

    }

    // TypeToken: 0x20000C0  // size: 0xE0
    public class JsonDynamicContract : Newtonsoft.Json.Serialization.JsonContainerContract
    {
        // Fields
        private readonly Newtonsoft.Json.Serialization.JsonPropertyCollection <Properties>k__BackingField;  // 0xc0
        private System.Func<System.String,System.String> <PropertyNameResolver>k__BackingField;  // 0xc8
        private readonly Newtonsoft.Json.Utilities.ThreadSafeStore<System.String,System.Runtime.CompilerServices.CallSite<System.Func<System.Runtime.CompilerServices.CallSite,System.Object,System.Object>>> _callSiteGetters;  // 0xd0
        private readonly Newtonsoft.Json.Utilities.ThreadSafeStore<System.String,System.Runtime.CompilerServices.CallSite<System.Func<System.Runtime.CompilerServices.CallSite,System.Object,System.Object,System.Object>>> _callSiteSetters;  // 0xd8

        // Properties
        Newtonsoft.Json.Serialization.JsonPropertyCollection Properties { get; /* RVA: 0x04D892F0 */ }
        System.Func<System.String,System.String> PropertyNameResolver { get; /* RVA: 0x04D8D1D0 */ set; /* RVA: 0x06AC5B30 */ }

        // Methods
        // RVA: 0x0A8BBFD8  token: 0x60004FC
        private static System.Runtime.CompilerServices.CallSite<System.Func<System.Runtime.CompilerServices.CallSite,System.Object,System.Object>> CreateCallSiteGetter(System.String name) { }
        // RVA: 0x0A8BC0B0  token: 0x60004FD
        private static System.Runtime.CompilerServices.CallSite<System.Func<System.Runtime.CompilerServices.CallSite,System.Object,System.Object,System.Object>> CreateCallSiteSetter(System.String name) { }
        // RVA: 0x0A8BC3E0  token: 0x60004FE
        public System.Void .ctor(System.Type underlyingType) { }
        // RVA: 0x0A8BC188  token: 0x60004FF
        private System.Boolean TryGetMember(System.Dynamic.IDynamicMetaObjectProvider dynamicProvider, System.String name, System.Object& value) { }
        // RVA: 0x0A8BC2BC  token: 0x6000500
        private System.Boolean TrySetMember(System.Dynamic.IDynamicMetaObjectProvider dynamicProvider, System.String name, System.Object value) { }

    }

    // TypeToken: 0x20000C1  // size: 0x28
    public class JsonFormatterConverter : System.Runtime.Serialization.IFormatterConverter
    {
        // Fields
        private readonly Newtonsoft.Json.Serialization.JsonSerializerInternalReader _reader;  // 0x10
        private readonly Newtonsoft.Json.Serialization.JsonISerializableContract _contract;  // 0x18
        private readonly Newtonsoft.Json.Serialization.JsonProperty _member;  // 0x20

        // Methods
        // RVA: 0x0A8BC85C  token: 0x6000501
        public System.Void .ctor(Newtonsoft.Json.Serialization.JsonSerializerInternalReader reader, Newtonsoft.Json.Serialization.JsonISerializableContract contract, Newtonsoft.Json.Serialization.JsonProperty member) { }
        // RVA: -1  // generic def  token: 0x6000502
        private T GetTokenValue(System.Object value) { }
        // RVA: 0x0A8BC5B0  token: 0x6000503
        public virtual System.Object Convert(System.Object value, System.Type type) { }
        // RVA: 0x0A8BC6F4  token: 0x6000504
        public virtual System.Boolean ToBoolean(System.Object value) { }
        // RVA: 0x0A8BC73C  token: 0x6000505
        public virtual System.Int32 ToInt32(System.Object value) { }
        // RVA: 0x0A8BC784  token: 0x6000506
        public virtual System.Int64 ToInt64(System.Object value) { }
        // RVA: 0x0A8BC7CC  token: 0x6000507
        public virtual System.Single ToSingle(System.Object value) { }
        // RVA: 0x0A8BC814  token: 0x6000508
        public virtual System.String ToString(System.Object value) { }

    }

    // TypeToken: 0x20000C2  // size: 0xC8
    public class JsonISerializableContract : Newtonsoft.Json.Serialization.JsonContainerContract
    {
        // Fields
        private Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> <ISerializableCreator>k__BackingField;  // 0xc0

        // Properties
        Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> ISerializableCreator { get; /* RVA: 0x04D892F0 */ set; /* RVA: 0x06AC5ACC */ }

        // Methods
        // RVA: 0x0A8BC984  token: 0x600050B
        public System.Void .ctor(System.Type underlyingType) { }

    }

    // TypeToken: 0x20000C3  // size: 0x90
    public class JsonLinqContract : Newtonsoft.Json.Serialization.JsonContract
    {
        // Methods
        // RVA: 0x0539821C  token: 0x600050C
        public System.Void .ctor(System.Type underlyingType) { }

    }

    // TypeToken: 0x20000C4  // size: 0x128
    public class JsonObjectContract : Newtonsoft.Json.Serialization.JsonContainerContract
    {
        // Fields
        private Newtonsoft.Json.MemberSerialization <MemberSerialization>k__BackingField;  // 0xc0
        private System.Nullable<Newtonsoft.Json.MissingMemberHandling> <MissingMemberHandling>k__BackingField;  // 0xc4
        private System.Nullable<Newtonsoft.Json.Required> <ItemRequired>k__BackingField;  // 0xcc
        private System.Nullable<Newtonsoft.Json.NullValueHandling> <ItemNullValueHandling>k__BackingField;  // 0xd4
        private readonly Newtonsoft.Json.Serialization.JsonPropertyCollection <Properties>k__BackingField;  // 0xe0
        private Newtonsoft.Json.Serialization.ExtensionDataSetter <ExtensionDataSetter>k__BackingField;  // 0xe8
        private Newtonsoft.Json.Serialization.ExtensionDataGetter <ExtensionDataGetter>k__BackingField;  // 0xf0
        private System.Func<System.String,System.String> <ExtensionDataNameResolver>k__BackingField;  // 0xf8
        private System.Boolean ExtensionDataIsJToken;  // 0x100
        private System.Nullable<System.Boolean> _hasRequiredOrDefaultValueProperties;  // 0x101
        private Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> _overrideCreator;  // 0x108
        private Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> _parameterizedCreator;  // 0x110
        private Newtonsoft.Json.Serialization.JsonPropertyCollection _creatorParameters;  // 0x118
        private System.Type _extensionDataValueType;  // 0x120

        // Properties
        Newtonsoft.Json.MemberSerialization MemberSerialization { get; /* RVA: 0x04DA4B70 */ set; /* RVA: 0x04DA5DC0 */ }
        System.Nullable<Newtonsoft.Json.MissingMemberHandling> MissingMemberHandling { get; /* RVA: 0x04DA5D90 */ set; /* RVA: 0x04D9E430 */ }
        System.Nullable<Newtonsoft.Json.Required> ItemRequired { get; /* RVA: 0x04DA5D80 */ set; /* RVA: 0x04DA5DB0 */ }
        System.Nullable<Newtonsoft.Json.NullValueHandling> ItemNullValueHandling { get; /* RVA: 0x04DA5D70 */ set; /* RVA: 0x04DA5DA0 */ }
        Newtonsoft.Json.Serialization.JsonPropertyCollection Properties { get; /* RVA: 0x04D8D1C0 */ }
        Newtonsoft.Json.Serialization.JsonPropertyCollection CreatorParameters { get; /* RVA: 0x03B2CBA0 */ }
        Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> OverrideCreator { get; /* RVA: 0x04D85F90 */ set; /* RVA: 0x068C2E50 */ }
        Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> ParameterizedCreator { get; /* RVA: 0x04D8D1B0 */ set; /* RVA: 0x05397170 */ }
        Newtonsoft.Json.Serialization.ExtensionDataSetter ExtensionDataSetter { get; /* RVA: 0x04D88590 */ set; /* RVA: 0x06AC5B1C */ }
        Newtonsoft.Json.Serialization.ExtensionDataGetter ExtensionDataGetter { get; /* RVA: 0x04D8D1A0 */ set; /* RVA: 0x06AC5AE0 */ }
        System.Type ExtensionDataValueType { set; /* RVA: 0x0A8BCAA0 */ }
        System.Func<System.String,System.String> ExtensionDataNameResolver { get; /* RVA: 0x04D877C0 */ set; /* RVA: 0x06790298 */ }
        System.Boolean HasRequiredOrDefaultValueProperties { get; /* RVA: 0x03B2C340 */ }

        // Methods
        // RVA: 0x035CECF0  token: 0x6000523
        public System.Void .ctor(System.Type underlyingType) { }
        // RVA: 0x0A8BC9A4  token: 0x6000524
        private System.Object GetUninitializedObject() { }

    }

    // TypeToken: 0x20000C5  // size: 0x98
    public class JsonPrimitiveContract : Newtonsoft.Json.Serialization.JsonContract
    {
        // Fields
        private Newtonsoft.Json.Utilities.PrimitiveTypeCode <TypeCode>k__BackingField;  // 0x90
        private static readonly System.Collections.Generic.Dictionary<System.Type,Newtonsoft.Json.ReadType> ReadTypeMap;  // static @ 0x0

        // Properties
        Newtonsoft.Json.Utilities.PrimitiveTypeCode TypeCode { get; /* RVA: 0x04D86B60 */ set; /* RVA: 0x04D86BC0 */ }

        // Methods
        // RVA: 0x035CDE60  token: 0x6000527
        public System.Void .ctor(System.Type underlyingType) { }
        // RVA: 0x04825860  token: 0x6000528
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000C6  // size: 0xF0
    public class JsonProperty
    {
        // Fields
        private System.Nullable<Newtonsoft.Json.Required> _required;  // 0x10
        private System.Boolean _hasExplicitDefaultValue;  // 0x18
        private System.Object _defaultValue;  // 0x20
        private System.Boolean _hasGeneratedDefaultValue;  // 0x28
        private System.String _propertyName;  // 0x30
        private System.Boolean _skipPropertyNameEscape;  // 0x38
        private System.Type _propertyType;  // 0x40
        private Newtonsoft.Json.Serialization.JsonContract <PropertyContract>k__BackingField;  // 0x48
        private System.Type <DeclaringType>k__BackingField;  // 0x50
        private System.Nullable<System.Int32> <Order>k__BackingField;  // 0x58
        private System.String <UnderlyingName>k__BackingField;  // 0x60
        private Newtonsoft.Json.Serialization.IValueProvider <ValueProvider>k__BackingField;  // 0x68
        private Newtonsoft.Json.Serialization.IAttributeProvider <AttributeProvider>k__BackingField;  // 0x70
        private Newtonsoft.Json.JsonConverter <Converter>k__BackingField;  // 0x78
        private System.Boolean <Ignored>k__BackingField;  // 0x80
        private System.Boolean <Readable>k__BackingField;  // 0x81
        private System.Boolean <Writable>k__BackingField;  // 0x82
        private System.Boolean <HasMemberAttribute>k__BackingField;  // 0x83
        private System.Nullable<System.Boolean> <IsReference>k__BackingField;  // 0x84
        private System.Nullable<Newtonsoft.Json.NullValueHandling> <NullValueHandling>k__BackingField;  // 0x88
        private System.Nullable<Newtonsoft.Json.DefaultValueHandling> <DefaultValueHandling>k__BackingField;  // 0x90
        private System.Nullable<Newtonsoft.Json.ReferenceLoopHandling> <ReferenceLoopHandling>k__BackingField;  // 0x98
        private System.Nullable<Newtonsoft.Json.ObjectCreationHandling> <ObjectCreationHandling>k__BackingField;  // 0xa0
        private System.Nullable<Newtonsoft.Json.TypeNameHandling> <TypeNameHandling>k__BackingField;  // 0xa8
        private System.Predicate<System.Object> <ShouldSerialize>k__BackingField;  // 0xb0
        private System.Predicate<System.Object> <ShouldDeserialize>k__BackingField;  // 0xb8
        private System.Predicate<System.Object> <GetIsSpecified>k__BackingField;  // 0xc0
        private System.Action<System.Object,System.Object> <SetIsSpecified>k__BackingField;  // 0xc8
        private Newtonsoft.Json.JsonConverter <ItemConverter>k__BackingField;  // 0xd0
        private System.Nullable<System.Boolean> <ItemIsReference>k__BackingField;  // 0xd8
        private System.Nullable<Newtonsoft.Json.TypeNameHandling> <ItemTypeNameHandling>k__BackingField;  // 0xdc
        private System.Nullable<Newtonsoft.Json.ReferenceLoopHandling> <ItemReferenceLoopHandling>k__BackingField;  // 0xe4

        // Properties
        Newtonsoft.Json.Serialization.JsonContract PropertyContract { get; /* RVA: 0x04D86200 */ set; /* RVA: 0x035A41A0 */ }
        System.String PropertyName { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x032B4000 */ }
        System.Type DeclaringType { get; /* RVA: 0x04D86270 */ set; /* RVA: 0x02FDAEA0 */ }
        System.Nullable<System.Int32> Order { get; /* RVA: 0x02E56440 */ set; /* RVA: 0x04D86A60 */ }
        System.String UnderlyingName { get; /* RVA: 0x04D86280 */ set; /* RVA: 0x063CC164 */ }
        Newtonsoft.Json.Serialization.IValueProvider ValueProvider { get; /* RVA: 0x04D86230 */ set; /* RVA: 0x0485F170 */ }
        Newtonsoft.Json.Serialization.IAttributeProvider AttributeProvider { set; /* RVA: 0x063AD760 */ }
        System.Type PropertyType { get; /* RVA: 0x04D85A60 */ set; /* RVA: 0x0321FFC0 */ }
        Newtonsoft.Json.JsonConverter Converter { get; /* RVA: 0x04D862A0 */ set; /* RVA: 0x06402320 */ }
        System.Boolean Ignored { get; /* RVA: 0x04D8D1F0 */ set; /* RVA: 0x04D8D200 */ }
        System.Boolean Readable { get; /* RVA: 0x04D8FEE0 */ set; /* RVA: 0x04D8FEF0 */ }
        System.Boolean Writable { get; /* RVA: 0x04DA5E30 */ set; /* RVA: 0x04DA5E60 */ }
        System.Boolean HasMemberAttribute { get; /* RVA: 0x04DA5DF0 */ set; /* RVA: 0x04DA5E40 */ }
        System.Object DefaultValue { get; /* RVA: 0x0A8BCBA8 */ set; /* RVA: 0x0A8BCBB8 */ }
        Newtonsoft.Json.Required Required { get; /* RVA: 0x04D882A0 */ }
        System.Nullable<System.Boolean> IsReference { get; /* RVA: 0x04D9CCF0 */ set; /* RVA: 0x04D9CD00 */ }
        System.Nullable<Newtonsoft.Json.NullValueHandling> NullValueHandling { get; /* RVA: 0x04D861F0 */ set; /* RVA: 0x04D85ED0 */ }
        System.Nullable<Newtonsoft.Json.DefaultValueHandling> DefaultValueHandling { get; /* RVA: 0x04D86220 */ set; /* RVA: 0x04D92A70 */ }
        System.Nullable<Newtonsoft.Json.ReferenceLoopHandling> ReferenceLoopHandling { get; /* RVA: 0x04D85EE0 */ set; /* RVA: 0x04D92710 */ }
        System.Nullable<Newtonsoft.Json.ObjectCreationHandling> ObjectCreationHandling { get; /* RVA: 0x04D86210 */ set; /* RVA: 0x04D927B0 */ }
        System.Nullable<Newtonsoft.Json.TypeNameHandling> TypeNameHandling { get; /* RVA: 0x04D86290 */ set; /* RVA: 0x04D862F0 */ }
        System.Predicate<System.Object> ShouldSerialize { get; /* RVA: 0x04D86250 */ set; /* RVA: 0x04D84CE0 */ }
        System.Predicate<System.Object> ShouldDeserialize { get; /* RVA: 0x04D85EF0 */ }
        System.Predicate<System.Object> GetIsSpecified { get; /* RVA: 0x04D892F0 */ set; /* RVA: 0x06AC5ACC */ }
        System.Action<System.Object,System.Object> SetIsSpecified { get; /* RVA: 0x04D8D1D0 */ set; /* RVA: 0x06AC5B30 */ }
        Newtonsoft.Json.JsonConverter ItemConverter { get; /* RVA: 0x04D876B0 */ set; /* RVA: 0x0678D7BC */ }
        System.Nullable<System.Boolean> ItemIsReference { get; /* RVA: 0x04DA5E00 */ set; /* RVA: 0x04DA5E50 */ }
        System.Nullable<Newtonsoft.Json.TypeNameHandling> ItemTypeNameHandling { get; /* RVA: 0x04DA5E20 */ set; /* RVA: 0x04D9E050 */ }
        System.Nullable<Newtonsoft.Json.ReferenceLoopHandling> ItemReferenceLoopHandling { get; /* RVA: 0x04DA5E10 */ set; /* RVA: 0x04D9E040 */ }

        // Methods
        // RVA: 0x02FBEF90  token: 0x6000544
        private System.Object GetResolvedDefaultValue() { }
        // RVA: 0x0A8BCB60  token: 0x6000559
        public virtual System.String ToString() { }
        // RVA: 0x0377CE50  token: 0x6000562
        private System.Void WritePropertyName(Newtonsoft.Json.JsonWriter writer) { }
        // RVA: 0x041E1670  token: 0x6000563
        public System.Void .ctor() { }

    }

    // TypeToken: 0x20000C7  // size: 0x40
    public class JsonPropertyCollection : System.Collections.ObjectModel.KeyedCollection`2
    {
        // Fields
        private readonly System.Type _type;  // 0x30
        private readonly System.Collections.Generic.List<Newtonsoft.Json.Serialization.JsonProperty> _list;  // 0x38

        // Methods
        // RVA: 0x035CE5A0  token: 0x6000564
        public System.Void .ctor(System.Type type) { }
        // RVA: 0x04DA5DD0  token: 0x6000565
        protected virtual System.String GetKeyForItem(Newtonsoft.Json.Serialization.JsonProperty item) { }
        // RVA: 0x032B3F40  token: 0x6000566
        public System.Void AddProperty(Newtonsoft.Json.Serialization.JsonProperty property) { }
        // RVA: 0x02FB6580  token: 0x6000567
        public Newtonsoft.Json.Serialization.JsonProperty GetClosestMatchProperty(System.String propertyName) { }
        // RVA: 0x02FB6D30  token: 0x6000568
        private System.Boolean TryGetProperty(System.String key, Newtonsoft.Json.Serialization.JsonProperty& item) { }
        // RVA: 0x02FB67E0  token: 0x6000569
        public Newtonsoft.Json.Serialization.JsonProperty GetProperty(System.String propertyName, System.StringComparison comparisonType) { }

    }

    // TypeToken: 0x20000C8  // size: 0x38
    public abstract class JsonSerializerInternalBase
    {
        // Fields
        private Newtonsoft.Json.Serialization.ErrorContext _currentErrorContext;  // 0x10
        private Newtonsoft.Json.Utilities.BidirectionalDictionary<System.String,System.Object> _mappings;  // 0x18
        private readonly Newtonsoft.Json.JsonSerializer Serializer;  // 0x20
        private readonly Newtonsoft.Json.Serialization.ITraceWriter TraceWriter;  // 0x28
        protected Newtonsoft.Json.Serialization.JsonSerializerProxy InternalSerializer;  // 0x30

        // Properties
        Newtonsoft.Json.Utilities.BidirectionalDictionary<System.String,System.Object> DefaultReferenceMappings { get; /* RVA: 0x0A8BD0A4 */ }

        // Methods
        // RVA: 0x03F720B0  token: 0x600056A
        protected System.Void .ctor(Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x0A8BD018  token: 0x600056C
        protected Newtonsoft.Json.NullValueHandling ResolvedNullValueHandling(Newtonsoft.Json.Serialization.JsonObjectContract containerContract, Newtonsoft.Json.Serialization.JsonProperty property) { }
        // RVA: 0x0A8BCC40  token: 0x600056D
        private Newtonsoft.Json.Serialization.ErrorContext GetErrorContext(System.Object currentObject, System.Object member, System.String path, System.Exception error) { }
        // RVA: 0x0A8BCBCC  token: 0x600056E
        protected System.Void ClearErrorContext() { }
        // RVA: 0x0A8BCD4C  token: 0x600056F
        protected System.Boolean IsErrorHandled(System.Object currentObject, Newtonsoft.Json.Serialization.JsonContract contract, System.Object keyValue, Newtonsoft.Json.IJsonLineInfo lineInfo, System.String path, System.Exception ex) { }

    }

    // TypeToken: 0x20000CA  // size: 0x38
    public class JsonSerializerInternalReader : Newtonsoft.Json.Serialization.JsonSerializerInternalBase
    {
        // Methods
        // RVA: 0x05390C34  token: 0x6000573
        public System.Void .ctor(Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x0A8BEAB8  token: 0x6000574
        public System.Void Populate(Newtonsoft.Json.JsonReader reader, System.Object target) { }
        // RVA: 0x02FBD0A0  token: 0x6000575
        private Newtonsoft.Json.Serialization.JsonContract GetContractSafe(System.Type type) { }
        // RVA: 0x02FBDED0  token: 0x6000576
        private Newtonsoft.Json.Serialization.JsonContract GetContract(System.Type type) { }
        // RVA: 0x02FBCF50  token: 0x6000577
        public System.Object Deserialize(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Boolean checkAdditionalContent) { }
        // RVA: 0x0312E380  token: 0x6000578
        private Newtonsoft.Json.Serialization.JsonSerializerProxy GetInternalSerializer() { }
        // RVA: 0x035A4840  token: 0x6000579
        private Newtonsoft.Json.Linq.JToken CreateJToken(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonContract contract) { }
        // RVA: 0x035A3A50  token: 0x600057A
        private Newtonsoft.Json.Linq.JToken CreateJObject(Newtonsoft.Json.JsonReader reader) { }
        // RVA: 0x02FB24C0  token: 0x600057B
        private System.Object CreateValueInternal(Newtonsoft.Json.JsonReader reader, System.Type objectType, Newtonsoft.Json.Serialization.JsonContract contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract containerContract, Newtonsoft.Json.Serialization.JsonProperty containerMember, System.Object existingValue) { }
        // RVA: 0x02FBC320  token: 0x600057C
        private static System.Boolean CoerceEmptyStringToNull(System.Type objectType, Newtonsoft.Json.Serialization.JsonContract contract, System.String s) { }
        // RVA: 0x0A8BE2A0  token: 0x600057D
        private System.String GetExpectedDescription(Newtonsoft.Json.Serialization.JsonContract contract) { }
        // RVA: 0x02FB4810  token: 0x600057E
        private Newtonsoft.Json.JsonConverter GetConverter(Newtonsoft.Json.Serialization.JsonContract contract, Newtonsoft.Json.JsonConverter memberConverter, Newtonsoft.Json.Serialization.JsonContainerContract containerContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty) { }
        // RVA: 0x032B5D10  token: 0x600057F
        private System.Object CreateObject(Newtonsoft.Json.JsonReader reader, System.Type objectType, Newtonsoft.Json.Serialization.JsonContract contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract containerContract, Newtonsoft.Json.Serialization.JsonProperty containerMember, System.Object existingValue) { }
        // RVA: 0x0A8BF040  token: 0x6000580
        private System.Boolean ReadMetadataPropertiesToken(Newtonsoft.Json.Linq.JTokenReader reader, System.Type& objectType, Newtonsoft.Json.Serialization.JsonContract& contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract containerContract, Newtonsoft.Json.Serialization.JsonProperty containerMember, System.Object existingValue, System.Object& newValue, System.String& id) { }
        // RVA: 0x032B62C0  token: 0x6000581
        private System.Boolean ReadMetadataProperties(Newtonsoft.Json.JsonReader reader, System.Type& objectType, Newtonsoft.Json.Serialization.JsonContract& contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract containerContract, Newtonsoft.Json.Serialization.JsonProperty containerMember, System.Object existingValue, System.Object& newValue, System.String& id) { }
        // RVA: 0x02FBD0F0  token: 0x6000582
        private System.Void ResolveTypeName(Newtonsoft.Json.JsonReader reader, System.Type& objectType, Newtonsoft.Json.Serialization.JsonContract& contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract containerContract, Newtonsoft.Json.Serialization.JsonProperty containerMember, System.String qualifiedTypeName) { }
        // RVA: 0x032B7360  token: 0x6000583
        private Newtonsoft.Json.Serialization.JsonArrayContract EnsureArrayContract(Newtonsoft.Json.JsonReader reader, System.Type objectType, Newtonsoft.Json.Serialization.JsonContract contract) { }
        // RVA: 0x032B6EF0  token: 0x6000584
        private System.Object CreateList(Newtonsoft.Json.JsonReader reader, System.Type objectType, Newtonsoft.Json.Serialization.JsonContract contract, Newtonsoft.Json.Serialization.JsonProperty member, System.Object existingValue, System.String id) { }
        // RVA: 0x032B61E0  token: 0x6000585
        private System.Boolean HasNoDefinedType(Newtonsoft.Json.Serialization.JsonContract contract) { }
        // RVA: 0x02FB1850  token: 0x6000586
        private System.Object EnsureType(Newtonsoft.Json.JsonReader reader, System.Object value, System.Globalization.CultureInfo culture, Newtonsoft.Json.Serialization.JsonContract contract, System.Type targetType) { }
        // RVA: 0x02FB41D0  token: 0x6000587
        private System.Boolean SetPropertyValue(Newtonsoft.Json.Serialization.JsonProperty property, Newtonsoft.Json.JsonConverter propertyConverter, Newtonsoft.Json.Serialization.JsonContainerContract containerContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty, Newtonsoft.Json.JsonReader reader, System.Object target) { }
        // RVA: 0x02FB3360  token: 0x6000588
        private System.Boolean CalculatePropertyDetails(Newtonsoft.Json.Serialization.JsonProperty property, Newtonsoft.Json.JsonConverter& propertyConverter, Newtonsoft.Json.Serialization.JsonContainerContract containerContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty, Newtonsoft.Json.JsonReader reader, System.Object target, System.Boolean& useExistingValue, System.Object& currentValue, Newtonsoft.Json.Serialization.JsonContract& propertyContract, System.Boolean& gottenCurrentValue, System.Boolean& ignoredValue) { }
        // RVA: 0x0A8BD1AC  token: 0x6000589
        private System.Void AddReference(Newtonsoft.Json.JsonReader reader, System.String id, System.Object value) { }
        // RVA: 0x02FB41C0  token: 0x600058A
        private System.Boolean HasFlag(Newtonsoft.Json.DefaultValueHandling value, Newtonsoft.Json.DefaultValueHandling flag) { }
        // RVA: 0x02FB32B0  token: 0x600058B
        private System.Boolean ShouldSetPropertyValue(Newtonsoft.Json.Serialization.JsonProperty property, Newtonsoft.Json.Serialization.JsonObjectContract contract, System.Object value) { }
        // RVA: 0x032B5940  token: 0x600058C
        private System.Collections.IList CreateNewList(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonArrayContract contract, System.Boolean& createdFromNonDefaultCreator) { }
        // RVA: 0x04021D20  token: 0x600058D
        private System.Collections.IDictionary CreateNewDictionary(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonDictionaryContract contract, System.Boolean& createdFromNonDefaultCreator) { }
        // RVA: 0x032B6C70  token: 0x600058E
        private System.Void OnDeserializing(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonContract contract, System.Object value) { }
        // RVA: 0x032B6DB0  token: 0x600058F
        private System.Void OnDeserialized(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonContract contract, System.Object value) { }
        // RVA: 0x035D2C10  token: 0x6000590
        private System.Object PopulateDictionary(System.Collections.IDictionary dictionary, Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonDictionaryContract contract, Newtonsoft.Json.Serialization.JsonProperty containerProperty, System.String id) { }
        // RVA: 0x0A8BE3F0  token: 0x6000591
        private System.Object PopulateMultidimensionalArray(System.Collections.IList list, Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonArrayContract contract, Newtonsoft.Json.Serialization.JsonProperty containerProperty, System.String id) { }
        // RVA: 0x0A8BF638  token: 0x6000592
        private System.Void ThrowUnexpectedEndException(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonContract contract, System.Object currentObject, System.String message) { }
        // RVA: 0x032B73B0  token: 0x6000593
        private System.Object PopulateList(System.Collections.IList list, Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonArrayContract contract, Newtonsoft.Json.Serialization.JsonProperty containerProperty, System.String id) { }
        // RVA: 0x0A8BDC54  token: 0x6000594
        private System.Object CreateISerializable(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonISerializableContract contract, Newtonsoft.Json.Serialization.JsonProperty member, System.String id) { }
        // RVA: 0x0A8BDB38  token: 0x6000595
        private System.Object CreateISerializableItem(Newtonsoft.Json.Linq.JToken token, System.Type type, Newtonsoft.Json.Serialization.JsonISerializableContract contract, Newtonsoft.Json.Serialization.JsonProperty member) { }
        // RVA: 0x0A8BD448  token: 0x6000596
        private System.Object CreateDynamic(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonDynamicContract contract, Newtonsoft.Json.Serialization.JsonProperty member, System.String id) { }
        // RVA: 0x03B2CC20  token: 0x6000597
        private System.Object CreateObjectUsingCreatorWithParameters(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonObjectContract contract, Newtonsoft.Json.Serialization.JsonProperty containerProperty, Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> creator, System.String id) { }
        // RVA: 0x0312E290  token: 0x6000598
        private System.Object DeserializeConvertable(Newtonsoft.Json.JsonConverter converter, Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue) { }
        // RVA: 0x03B2C810  token: 0x6000599
        private System.Collections.Generic.List<Newtonsoft.Json.Serialization.JsonSerializerInternalReader.CreatorPropertyContext> ResolvePropertyAndCreatorValues(Newtonsoft.Json.Serialization.JsonObjectContract contract, Newtonsoft.Json.Serialization.JsonProperty containerProperty, Newtonsoft.Json.JsonReader reader, System.Type objectType) { }
        // RVA: 0x032B6B80  token: 0x600059A
        public System.Object CreateNewObject(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonObjectContract objectContract, Newtonsoft.Json.Serialization.JsonProperty containerMember, Newtonsoft.Json.Serialization.JsonProperty containerProperty, System.String id, System.Boolean& createdFromNonDefaultCreator) { }
        // RVA: 0x02FB4CC0  token: 0x600059B
        private System.Object PopulateObject(System.Object newObject, Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonObjectContract contract, Newtonsoft.Json.Serialization.JsonProperty member, System.String id) { }
        // RVA: 0x02FB47C0  token: 0x600059C
        private System.Boolean ShouldDeserialize(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonProperty property, System.Object target) { }
        // RVA: 0x02FB4C80  token: 0x600059D
        private System.Boolean CheckPropertyName(Newtonsoft.Json.JsonReader reader, System.String memberName) { }
        // RVA: 0x03424C40  token: 0x600059E
        private System.Void SetExtensionData(Newtonsoft.Json.Serialization.JsonObjectContract contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.JsonReader reader, System.String memberName, System.Object o) { }
        // RVA: 0x0A8BEF94  token: 0x600059F
        private System.Object ReadExtensionDataValue(Newtonsoft.Json.Serialization.JsonObjectContract contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.JsonReader reader) { }
        // RVA: 0x02FBED50  token: 0x60005A0
        private System.Void EndProcessProperty(System.Object newObject, Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonObjectContract contract, System.Int32 initialDepth, Newtonsoft.Json.Serialization.JsonProperty property, Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence presence, System.Boolean setDefaultValue) { }
        // RVA: 0x02FB4660  token: 0x60005A1
        private System.Void SetPropertyPresence(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonProperty property, System.Collections.Generic.Dictionary<Newtonsoft.Json.Serialization.JsonProperty,Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence> requiredProperties) { }
        // RVA: 0x0A8BE384  token: 0x60005A2
        private System.Void HandleError(Newtonsoft.Json.JsonReader reader, System.Boolean readPastError, System.Int32 initialDepth) { }

    }

    // TypeToken: 0x20000CF  // size: 0x50
    public class JsonSerializerInternalWriter : Newtonsoft.Json.Serialization.JsonSerializerInternalBase
    {
        // Fields
        private System.Type _rootType;  // 0x38
        private System.Int32 _rootLevel;  // 0x40
        private readonly System.Collections.Generic.List<System.Object> _serializeStack;  // 0x48

        // Methods
        // RVA: 0x03F72010  token: 0x60005AC
        public System.Void .ctor(Newtonsoft.Json.JsonSerializer serializer) { }
        // RVA: 0x03F72100  token: 0x60005AD
        public System.Void Serialize(Newtonsoft.Json.JsonWriter jsonWriter, System.Object value, System.Type objectType) { }
        // RVA: 0x0A8BF780  token: 0x60005AE
        private Newtonsoft.Json.Serialization.JsonSerializerProxy GetInternalSerializer() { }
        // RVA: 0x02FBEFE0  token: 0x60005AF
        private Newtonsoft.Json.Serialization.JsonContract GetContractSafe(System.Object value) { }
        // RVA: 0x02FBF000  token: 0x60005B0
        private Newtonsoft.Json.Serialization.JsonContract GetContract(System.Object value) { }
        // RVA: 0x0377F0F0  token: 0x60005B1
        private System.Void SerializePrimitive(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.Serialization.JsonPrimitiveContract contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract containerContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty) { }
        // RVA: 0x0377D320  token: 0x60005B2
        private System.Void SerializeValue(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.Serialization.JsonContract valueContract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract containerContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty) { }
        // RVA: 0x0377BD10  token: 0x60005B3
        private System.Nullable<System.Boolean> ResolveIsReference(Newtonsoft.Json.Serialization.JsonContract contract, Newtonsoft.Json.Serialization.JsonProperty property, Newtonsoft.Json.Serialization.JsonContainerContract collectionContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty) { }
        // RVA: 0x0377D190  token: 0x60005B4
        private System.Boolean ShouldWriteReference(System.Object value, Newtonsoft.Json.Serialization.JsonProperty property, Newtonsoft.Json.Serialization.JsonContract valueContract, Newtonsoft.Json.Serialization.JsonContainerContract collectionContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty) { }
        // RVA: 0x0377D120  token: 0x60005B5
        private System.Boolean ShouldWriteProperty(System.Object memberValue, Newtonsoft.Json.Serialization.JsonObjectContract containerContract, Newtonsoft.Json.Serialization.JsonProperty property) { }
        // RVA: 0x0377D240  token: 0x60005B6
        private System.Boolean CheckForCircularReference(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.Serialization.JsonProperty property, Newtonsoft.Json.Serialization.JsonContract contract, Newtonsoft.Json.Serialization.JsonContainerContract containerContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty) { }
        // RVA: 0x0A8C1BA4  token: 0x60005B7
        private System.Void WriteReference(Newtonsoft.Json.JsonWriter writer, System.Object value) { }
        // RVA: 0x0A8BF7F4  token: 0x60005B8
        private System.String GetReference(Newtonsoft.Json.JsonWriter writer, System.Object value) { }
        // RVA: 0x0A8C1958  token: 0x60005B9
        private static System.Boolean TryConvertToString(System.Object value, System.Type type, System.String& s) { }
        // RVA: 0x0A8C1828  token: 0x60005BA
        private System.Void SerializeString(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.Serialization.JsonStringContract contract) { }
        // RVA: 0x04386D50  token: 0x60005BB
        private System.Void OnSerializing(Newtonsoft.Json.JsonWriter writer, Newtonsoft.Json.Serialization.JsonContract contract, System.Object value) { }
        // RVA: 0x04386C20  token: 0x60005BC
        private System.Void OnSerialized(Newtonsoft.Json.JsonWriter writer, Newtonsoft.Json.Serialization.JsonContract contract, System.Object value) { }
        // RVA: 0x0377BD80  token: 0x60005BD
        private System.Void SerializeObject(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.Serialization.JsonObjectContract contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract collectionContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty) { }
        // RVA: 0x0377CE90  token: 0x60005BE
        private System.Boolean CalculatePropertyValues(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.Serialization.JsonContainerContract contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonProperty property, Newtonsoft.Json.Serialization.JsonContract& memberContract, System.Object& memberValue) { }
        // RVA: 0x0377BC20  token: 0x60005BF
        private System.Void WriteObjectStart(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.Serialization.JsonContract contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract collectionContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty) { }
        // RVA: 0x0A8BF9D4  token: 0x60005C0
        private System.Boolean HasCreatorParameter(Newtonsoft.Json.Serialization.JsonContainerContract contract, Newtonsoft.Json.Serialization.JsonProperty property) { }
        // RVA: 0x0A8C1A28  token: 0x60005C1
        private System.Void WriteReferenceIdProperty(Newtonsoft.Json.JsonWriter writer, System.Type type, System.Object value) { }
        // RVA: 0x0A8C1D4C  token: 0x60005C2
        private System.Void WriteTypeProperty(Newtonsoft.Json.JsonWriter writer, System.Type type) { }
        // RVA: 0x0377BD70  token: 0x60005C3
        private System.Boolean HasFlag(Newtonsoft.Json.DefaultValueHandling value, Newtonsoft.Json.DefaultValueHandling flag) { }
        // RVA: 0x0377BD70  token: 0x60005C4
        private System.Boolean HasFlag(Newtonsoft.Json.PreserveReferencesHandling value, Newtonsoft.Json.PreserveReferencesHandling flag) { }
        // RVA: 0x02FB41C0  token: 0x60005C5
        private System.Boolean HasFlag(Newtonsoft.Json.TypeNameHandling value, Newtonsoft.Json.TypeNameHandling flag) { }
        // RVA: 0x0A8BFA5C  token: 0x60005C6
        private System.Void SerializeConvertable(Newtonsoft.Json.JsonWriter writer, Newtonsoft.Json.JsonConverter converter, System.Object value, Newtonsoft.Json.Serialization.JsonContract contract, Newtonsoft.Json.Serialization.JsonContainerContract collectionContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty) { }
        // RVA: 0x04386090  token: 0x60005C7
        private System.Void SerializeList(Newtonsoft.Json.JsonWriter writer, System.Collections.IEnumerable values, Newtonsoft.Json.Serialization.JsonArrayContract contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract collectionContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty) { }
        // RVA: 0x0A8C1698  token: 0x60005C8
        private System.Void SerializeMultidimensionalArray(Newtonsoft.Json.JsonWriter writer, System.Array values, Newtonsoft.Json.Serialization.JsonArrayContract contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract collectionContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty) { }
        // RVA: 0x0A8C12BC  token: 0x60005C9
        private System.Void SerializeMultidimensionalArray(Newtonsoft.Json.JsonWriter writer, System.Array values, Newtonsoft.Json.Serialization.JsonArrayContract contract, Newtonsoft.Json.Serialization.JsonProperty member, System.Int32 initialDepth, System.Int32[] indices) { }
        // RVA: 0x04386E80  token: 0x60005CA
        private System.Boolean WriteStartArray(Newtonsoft.Json.JsonWriter writer, System.Object values, Newtonsoft.Json.Serialization.JsonArrayContract contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract containerContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty) { }
        // RVA: 0x0A8C0EB8  token: 0x60005CB
        private System.Void SerializeISerializable(Newtonsoft.Json.JsonWriter writer, System.Runtime.Serialization.ISerializable value, Newtonsoft.Json.Serialization.JsonISerializableContract contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract collectionContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty) { }
        // RVA: 0x0A8BFD9C  token: 0x60005CC
        private System.Void SerializeDynamic(Newtonsoft.Json.JsonWriter writer, System.Dynamic.IDynamicMetaObjectProvider value, Newtonsoft.Json.Serialization.JsonDynamicContract contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract collectionContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty) { }
        // RVA: 0x0A8C18C0  token: 0x60005CD
        private System.Boolean ShouldWriteDynamicProperty(System.Object memberValue) { }
        // RVA: 0x0377BAB0  token: 0x60005CE
        private System.Boolean ShouldWriteType(Newtonsoft.Json.TypeNameHandling typeNameHandlingFlag, Newtonsoft.Json.Serialization.JsonContract contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract containerContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty) { }
        // RVA: 0x02FBF040  token: 0x60005CF
        private System.Void SerializeDictionary(Newtonsoft.Json.JsonWriter writer, System.Collections.IDictionary values, Newtonsoft.Json.Serialization.JsonDictionaryContract contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract collectionContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty) { }
        // RVA: 0x02FC0E90  token: 0x60005D0
        private System.String GetPropertyName(Newtonsoft.Json.JsonWriter writer, System.Object name, Newtonsoft.Json.Serialization.JsonContract contract, System.Boolean& escape) { }
        // RVA: 0x0A8BF96C  token: 0x60005D1
        private System.Void HandleError(Newtonsoft.Json.JsonWriter writer, System.Int32 initialDepth) { }
        // RVA: 0x0377D0D0  token: 0x60005D2
        private System.Boolean ShouldSerialize(Newtonsoft.Json.JsonWriter writer, Newtonsoft.Json.Serialization.JsonProperty property, System.Object target) { }
        // RVA: 0x0377D080  token: 0x60005D3
        private System.Boolean IsSpecified(Newtonsoft.Json.JsonWriter writer, Newtonsoft.Json.Serialization.JsonProperty property, System.Object target) { }

    }

    // TypeToken: 0x20000D0  // size: 0xF8
    public class JsonSerializerProxy : Newtonsoft.Json.JsonSerializer
    {
        // Fields
        private readonly Newtonsoft.Json.Serialization.JsonSerializerInternalReader _serializerReader;  // 0xe0
        private readonly Newtonsoft.Json.Serialization.JsonSerializerInternalWriter _serializerWriter;  // 0xe8
        private readonly Newtonsoft.Json.JsonSerializer _serializer;  // 0xf0

        // Properties
        Newtonsoft.Json.Serialization.IReferenceResolver ReferenceResolver { set; /* RVA: 0x0A8CFDAC */ }
        Newtonsoft.Json.Serialization.ITraceWriter TraceWriter { get; /* RVA: 0x0A8CFB48 */ set; /* RVA: 0x0A8CFDFC */ }
        System.Collections.IEqualityComparer EqualityComparer { set; /* RVA: 0x0A8CFC6C */ }
        Newtonsoft.Json.JsonConverterCollection Converters { get; /* RVA: 0x0A8CFA98 */ }
        Newtonsoft.Json.DefaultValueHandling DefaultValueHandling { set; /* RVA: 0x0A8CFC44 */ }
        Newtonsoft.Json.Serialization.IContractResolver ContractResolver { get; /* RVA: 0x0A8CFA74 */ set; /* RVA: 0x0A8CFC1C */ }
        Newtonsoft.Json.MissingMemberHandling MissingMemberHandling { set; /* RVA: 0x0A8CFCE4 */ }
        Newtonsoft.Json.NullValueHandling NullValueHandling { get; /* RVA: 0x0A8CFB24 */ set; /* RVA: 0x0A8CFD0C */ }
        Newtonsoft.Json.ObjectCreationHandling ObjectCreationHandling { set; /* RVA: 0x0A8CFD34 */ }
        Newtonsoft.Json.ReferenceLoopHandling ReferenceLoopHandling { set; /* RVA: 0x0A8CFD84 */ }
        Newtonsoft.Json.PreserveReferencesHandling PreserveReferencesHandling { set; /* RVA: 0x0A8CFD5C */ }
        Newtonsoft.Json.TypeNameHandling TypeNameHandling { set; /* RVA: 0x0A8CFE4C */ }
        Newtonsoft.Json.MetadataPropertyHandling MetadataPropertyHandling { get; /* RVA: 0x0A8CFB00 */ set; /* RVA: 0x0A8CFCBC */ }
        Newtonsoft.Json.TypeNameAssemblyFormatHandling TypeNameAssemblyFormatHandling { set; /* RVA: 0x0A8CFE24 */ }
        Newtonsoft.Json.ConstructorHandling ConstructorHandling { set; /* RVA: 0x0A8CFBBC */ }
        Newtonsoft.Json.Serialization.ISerializationBinder SerializationBinder { get; /* RVA: 0x02E56460 */ set; /* RVA: 0x0A8CFDD4 */ }
        System.Runtime.Serialization.StreamingContext Context { get; /* RVA: 0x0A8CFA40 */ set; /* RVA: 0x0A8CFBE4 */ }
        Newtonsoft.Json.Formatting Formatting { get; /* RVA: 0x0A8CFAB8 */ set; /* RVA: 0x0A8CFC94 */ }
        System.Nullable<System.Int32> MaxDepth { get; /* RVA: 0x0A8CFADC */ }
        System.Boolean CheckAdditionalContent { get; /* RVA: 0x0A8CFA1C */ set; /* RVA: 0x0A8CFB94 */ }

        // Events
        event System.EventHandler<Newtonsoft.Json.Serialization.ErrorEventArgs> Error;

        // Methods
        // RVA: 0x0A8CF88C  token: 0x60005F2
        private Newtonsoft.Json.Serialization.JsonSerializerInternalBase GetInternalSerializer() { }
        // RVA: 0x035A65F0  token: 0x60005F3
        public System.Void .ctor(Newtonsoft.Json.Serialization.JsonSerializerInternalReader serializerReader) { }
        // RVA: 0x0A8CF934  token: 0x60005F4
        public System.Void .ctor(Newtonsoft.Json.Serialization.JsonSerializerInternalWriter serializerWriter) { }
        // RVA: 0x0A8CF840  token: 0x60005F5
        private virtual System.Object DeserializeInternal(Newtonsoft.Json.JsonReader reader, System.Type objectType) { }
        // RVA: 0x0A8CF8A8  token: 0x60005F6
        private virtual System.Void PopulateInternal(Newtonsoft.Json.JsonReader reader, System.Object target) { }
        // RVA: 0x0A8CF8EC  token: 0x60005F7
        private virtual System.Void SerializeInternal(Newtonsoft.Json.JsonWriter jsonWriter, System.Object value, System.Type rootType) { }

    }

    // TypeToken: 0x20000D1  // size: 0x98
    public class JsonStringContract : Newtonsoft.Json.Serialization.JsonPrimitiveContract
    {
        // Methods
        // RVA: 0x04CCFC10  token: 0x60005F8
        public System.Void .ctor(System.Type underlyingType) { }

    }

    // TypeToken: 0x20000D2  // size: 0x10
    public static class JsonTypeReflector
    {
        // Fields
        private static System.Nullable<System.Boolean> _fullyTrusted;  // static @ 0x0
        private static readonly Newtonsoft.Json.Utilities.ThreadSafeStore<System.Type,System.Func<System.Object[],System.Object>> CreatorCache;  // static @ 0x8
        private static readonly Newtonsoft.Json.Utilities.ThreadSafeStore<System.Type,System.Type> AssociatedMetadataTypesCache;  // static @ 0x10
        private static Newtonsoft.Json.Utilities.ReflectionObject _metadataTypeAttributeReflectionObject;  // static @ 0x18

        // Properties
        System.Boolean FullyTrusted { get; /* RVA: 0x0A8D0288 */ }
        Newtonsoft.Json.Utilities.ReflectionDelegateFactory ReflectionDelegateFactory { get; /* RVA: 0x035CF6E0 */ }

        // Methods
        // RVA: -1  // generic def  token: 0x60005F9
        public static T GetCachedAttribute(System.Object attributeProvider) { }
        // RVA: 0x03591BB0  token: 0x60005FA
        public static System.Boolean CanTypeDescriptorConvertString(System.Type type, System.ComponentModel.TypeConverter& typeConverter) { }
        // RVA: 0x032B4DB0  token: 0x60005FB
        public static System.Runtime.Serialization.DataContractAttribute GetDataContractAttribute(System.Type type) { }
        // RVA: 0x0A8D006C  token: 0x60005FC
        public static System.Runtime.Serialization.DataMemberAttribute GetDataMemberAttribute(System.Reflection.MemberInfo memberInfo) { }
        // RVA: 0x035CEAB0  token: 0x60005FD
        public static Newtonsoft.Json.MemberSerialization GetObjectMemberSerialization(System.Type objectType, System.Boolean ignoreSerializableAttribute) { }
        // RVA: 0x032B40C0  token: 0x60005FE
        public static Newtonsoft.Json.JsonConverter GetJsonConverter(System.Object attributeProvider) { }
        // RVA: 0x0A8CFE74  token: 0x60005FF
        public static Newtonsoft.Json.JsonConverter CreateJsonConverterInstance(System.Type converterType, System.Object[] args) { }
        // RVA: 0x0A8CFF1C  token: 0x6000600
        public static Newtonsoft.Json.Serialization.NamingStrategy CreateNamingStrategyInstance(System.Type namingStrategyType, System.Object[] args) { }
        // RVA: 0x0A8CFFC4  token: 0x6000601
        public static Newtonsoft.Json.Serialization.NamingStrategy GetContainerNamingStrategy(Newtonsoft.Json.JsonContainerAttribute containerAttribute) { }
        // RVA: 0x0496F210  token: 0x6000602
        private static System.Func<System.Object[],System.Object> GetCreator(System.Type type) { }
        // RVA: 0x0312C240  token: 0x6000603
        private static System.Type GetAssociatedMetadataType(System.Type type) { }
        // RVA: 0x032B2B60  token: 0x6000604
        private static System.Type GetAssociateMetadataTypeFromAttribute(System.Type type) { }
        // RVA: -1  // generic def  token: 0x6000605
        private static T GetAttribute(System.Type type) { }
        // RVA: -1  // generic def  token: 0x6000606
        private static T GetAttribute(System.Reflection.MemberInfo memberInfo) { }
        // RVA: 0x032B3450  token: 0x6000607
        public static System.Boolean IsNonSerializable(System.Object provider) { }
        // RVA: 0x0A8D022C  token: 0x6000608
        public static System.Boolean IsSerializable(System.Object provider) { }
        // RVA: -1  // generic def  token: 0x6000609
        public static T GetAttribute(System.Object provider) { }
        // RVA: 0x0489B130  token: 0x600060C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000D5  // size: 0x18
    public class KebabCaseNamingStrategy : Newtonsoft.Json.Serialization.NamingStrategy
    {
        // Methods
        // RVA: 0x04DA5CA0  token: 0x6000612
        public System.Void .ctor(System.Boolean processDictionaryKeys, System.Boolean overrideSpecifiedNames) { }
        // RVA: 0x04DA5C90  token: 0x6000613
        public System.Void .ctor(System.Boolean processDictionaryKeys, System.Boolean overrideSpecifiedNames, System.Boolean processExtensionDataNames) { }
        // RVA: 0x041E1670  token: 0x6000614
        public System.Void .ctor() { }
        // RVA: 0x0A8D0348  token: 0x6000615
        protected virtual System.String ResolvePropertyName(System.String name) { }

    }

    // TypeToken: 0x20000D6  // size: 0x18
    public abstract class NamingStrategy
    {
        // Fields
        private System.Boolean <ProcessDictionaryKeys>k__BackingField;  // 0x10
        private System.Boolean <ProcessExtensionDataNames>k__BackingField;  // 0x11
        private System.Boolean <OverrideSpecifiedNames>k__BackingField;  // 0x12

        // Properties
        System.Boolean ProcessDictionaryKeys { get; /* RVA: 0x015EFCE0 */ set; /* RVA: 0x04D86130 */ }
        System.Boolean ProcessExtensionDataNames { get; /* RVA: 0x04D88380 */ set; /* RVA: 0x04D88460 */ }
        System.Boolean OverrideSpecifiedNames { get; /* RVA: 0x04D88390 */ set; /* RVA: 0x04DA0900 */ }

        // Methods
        // RVA: 0x0A8D0540  token: 0x600061C
        public virtual System.String GetPropertyName(System.String name, System.Boolean hasSpecifiedName) { }
        // RVA: 0x0A8D0468  token: 0x600061D
        public virtual System.String GetExtensionDataName(System.String name) { }
        // RVA: 0x0A8D0440  token: 0x600061E
        public virtual System.String GetDictionaryKey(System.String key) { }
        // RVA: -1  // abstract  token: 0x600061F
        protected virtual System.String ResolvePropertyName(System.String name) { }
        // RVA: 0x0A8D0490  token: 0x6000620
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0A8D03EC  token: 0x6000621
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0A8D0354  token: 0x6000622
        protected System.Boolean Equals(Newtonsoft.Json.Serialization.NamingStrategy other) { }
        // RVA: 0x041E1670  token: 0x6000623
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x20000D7
    public sealed class ObjectConstructor`1 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x6000624
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x6000625
        public virtual System.Object Invoke(System.Object[] args) { }

    }

    // TypeToken: 0x20000D8  // size: 0x10
    public sealed class OnErrorAttribute : System.Attribute
    {
    }

    // TypeToken: 0x20000D9  // size: 0x18
    public class ReflectionAttributeProvider : Newtonsoft.Json.Serialization.IAttributeProvider
    {
        // Fields
        private readonly System.Object _attributeProvider;  // 0x10

        // Methods
        // RVA: 0x03220020  token: 0x6000626
        public System.Void .ctor(System.Object attributeProvider) { }

    }

    // TypeToken: 0x20000DA  // size: 0x18
    public class ReflectionValueProvider : Newtonsoft.Json.Serialization.IValueProvider
    {
        // Fields
        private readonly System.Reflection.MemberInfo _memberInfo;  // 0x10

        // Methods
        // RVA: 0x03220040  token: 0x6000627
        public System.Void .ctor(System.Reflection.MemberInfo memberInfo) { }
        // RVA: 0x02FB2C00  token: 0x6000628
        public virtual System.Void SetValue(System.Object target, System.Object value) { }
        // RVA: 0x03594940  token: 0x6000629
        public virtual System.Object GetValue(System.Object target) { }

    }

    // TypeToken: 0x20000DB  // size: 0x18
    public class SnakeCaseNamingStrategy : Newtonsoft.Json.Serialization.NamingStrategy
    {
        // Methods
        // RVA: 0x04DA5CA0  token: 0x600062A
        public System.Void .ctor(System.Boolean processDictionaryKeys, System.Boolean overrideSpecifiedNames) { }
        // RVA: 0x04DA5C90  token: 0x600062B
        public System.Void .ctor(System.Boolean processDictionaryKeys, System.Boolean overrideSpecifiedNames, System.Boolean processExtensionDataNames) { }
        // RVA: 0x041E1670  token: 0x600062C
        public System.Void .ctor() { }
        // RVA: 0x0A8D0564  token: 0x600062D
        protected virtual System.String ResolvePropertyName(System.String name) { }

    }

    // TypeToken: 0x20000DC  // size: 0x90
    public class TraceJsonReader : Newtonsoft.Json.JsonReader, Newtonsoft.Json.IJsonLineInfo
    {
        // Fields
        private readonly Newtonsoft.Json.JsonReader _innerReader;  // 0x78
        private readonly Newtonsoft.Json.JsonTextWriter _textWriter;  // 0x80
        private readonly System.IO.StringWriter _sw;  // 0x88

        // Properties
        System.Int32 Depth { get; /* RVA: 0x0A8D0AC8 */ }
        System.String Path { get; /* RVA: 0x0A8D0AEC */ }
        Newtonsoft.Json.JsonToken TokenType { get; /* RVA: 0x0A8D0B10 */ }
        System.Object Value { get; /* RVA: 0x0A8D0B58 */ }
        System.Type ValueType { get; /* RVA: 0x0A8D0B34 */ }
        System.Int32 Newtonsoft.Json.IJsonLineInfo.LineNumber { get; /* RVA: 0x0A8D0608 */ }
        System.Int32 Newtonsoft.Json.IJsonLineInfo.LinePosition { get; /* RVA: 0x0A8D0658 */ }

        // Methods
        // RVA: 0x0A8D0970  token: 0x600062E
        public System.Void .ctor(Newtonsoft.Json.JsonReader innerReader) { }
        // RVA: 0x0A8D0594  token: 0x600062F
        public System.String GetDeserializedJsonMessage() { }
        // RVA: 0x0A8D08F8  token: 0x6000630
        public virtual System.Boolean Read() { }
        // RVA: 0x0A8D0870  token: 0x6000631
        public virtual System.Nullable<System.Int32> ReadAsInt32() { }
        // RVA: 0x0A8D08B4  token: 0x6000632
        public virtual System.String ReadAsString() { }
        // RVA: 0x0A8D06EC  token: 0x6000633
        public virtual System.Byte[] ReadAsBytes() { }
        // RVA: 0x0A8D07CC  token: 0x6000634
        public virtual System.Nullable<System.Decimal> ReadAsDecimal() { }
        // RVA: 0x0A8D0820  token: 0x6000635
        public virtual System.Nullable<System.Double> ReadAsDouble() { }
        // RVA: 0x0A8D06A8  token: 0x6000636
        public virtual System.Nullable<System.Boolean> ReadAsBoolean() { }
        // RVA: 0x0A8D0784  token: 0x6000637
        public virtual System.Nullable<System.DateTime> ReadAsDateTime() { }
        // RVA: 0x0A8D0730  token: 0x6000638
        public virtual System.Nullable<System.DateTimeOffset> ReadAsDateTimeOffset() { }
        // RVA: 0x0A8D093C  token: 0x6000639
        public System.Void WriteCurrentToken() { }
        // RVA: 0x0A8D0570  token: 0x600063F
        public virtual System.Void Close() { }
        // RVA: 0x0A8D05B8  token: 0x6000640
        private virtual System.Boolean Newtonsoft.Json.IJsonLineInfo.HasLineInfo() { }

    }

    // TypeToken: 0x20000DD  // size: 0x78
    public class TraceJsonWriter : Newtonsoft.Json.JsonWriter
    {
        // Fields
        private readonly Newtonsoft.Json.JsonWriter _innerWriter;  // 0x60
        private readonly Newtonsoft.Json.JsonTextWriter _textWriter;  // 0x68
        private readonly System.IO.StringWriter _sw;  // 0x70

        // Methods
        // RVA: 0x0A8D2300  token: 0x6000643
        public System.Void .ctor(Newtonsoft.Json.JsonWriter innerWriter) { }
        // RVA: 0x0A8D0BC0  token: 0x6000644
        public System.String GetSerializedJsonMessage() { }
        // RVA: 0x0A8D1344  token: 0x6000645
        public virtual System.Void WriteValue(System.Decimal value) { }
        // RVA: 0x0A8D1594  token: 0x6000646
        public virtual System.Void WriteValue(System.Nullable<System.Decimal> value) { }
        // RVA: 0x0A8D1538  token: 0x6000647
        public virtual System.Void WriteValue(System.Boolean value) { }
        // RVA: 0x0A8D1770  token: 0x6000648
        public virtual System.Void WriteValue(System.Nullable<System.Boolean> value) { }
        // RVA: 0x0A8D10D4  token: 0x6000649
        public virtual System.Void WriteValue(System.Byte value) { }
        // RVA: 0x0A8D1A48  token: 0x600064A
        public virtual System.Void WriteValue(System.Nullable<System.Byte> value) { }
        // RVA: 0x0A8D1714  token: 0x600064B
        public virtual System.Void WriteValue(System.Char value) { }
        // RVA: 0x0A8D1AD8  token: 0x600064C
        public virtual System.Void WriteValue(System.Nullable<System.Char> value) { }
        // RVA: 0x0A8D21A0  token: 0x600064D
        public virtual System.Void WriteValue(System.Byte[] value) { }
        // RVA: 0x0A8D1FA8  token: 0x600064E
        public virtual System.Void WriteValue(System.DateTime value) { }
        // RVA: 0x0A8D1130  token: 0x600064F
        public virtual System.Void WriteValue(System.Nullable<System.DateTime> value) { }
        // RVA: 0x0A8D13C0  token: 0x6000650
        public virtual System.Void WriteValue(System.DateTimeOffset value) { }
        // RVA: 0x0A8D1654  token: 0x6000651
        public virtual System.Void WriteValue(System.Nullable<System.DateTimeOffset> value) { }
        // RVA: 0x0A8D1434  token: 0x6000652
        public virtual System.Void WriteValue(System.Double value) { }
        // RVA: 0x0A8D18F0  token: 0x6000653
        public virtual System.Void WriteValue(System.Nullable<System.Double> value) { }
        // RVA: 0x0A8D0FA8  token: 0x6000654
        public virtual System.Void WriteUndefined() { }
        // RVA: 0x0A8D0D0C  token: 0x6000655
        public virtual System.Void WriteNull() { }
        // RVA: 0x0A8D1D0C  token: 0x6000656
        public virtual System.Void WriteValue(System.Single value) { }
        // RVA: 0x0A8D20AC  token: 0x6000657
        public virtual System.Void WriteValue(System.Nullable<System.Single> value) { }
        // RVA: 0x0A8D1C04  token: 0x6000658
        public virtual System.Void WriteValue(System.Guid value) { }
        // RVA: 0x0A8D1D68  token: 0x6000659
        public virtual System.Void WriteValue(System.Nullable<System.Guid> value) { }
        // RVA: 0x0A8D12EC  token: 0x600065A
        public virtual System.Void WriteValue(System.Int32 value) { }
        // RVA: 0x0A8D0FEC  token: 0x600065B
        public virtual System.Void WriteValue(System.Nullable<System.Int32> value) { }
        // RVA: 0x0A8D1E78  token: 0x600065C
        public virtual System.Void WriteValue(System.Int64 value) { }
        // RVA: 0x0A8D1490  token: 0x600065D
        public virtual System.Void WriteValue(System.Nullable<System.Int64> value) { }
        // RVA: 0x0A8D1ED4  token: 0x600065E
        public virtual System.Void WriteValue(System.Object value) { }
        // RVA: 0x0A8D1084  token: 0x600065F
        public virtual System.Void WriteValue(System.SByte value) { }
        // RVA: 0x0A8D11D0  token: 0x6000660
        public virtual System.Void WriteValue(System.Nullable<System.SByte> value) { }
        // RVA: 0x0A8D220C  token: 0x6000661
        public virtual System.Void WriteValue(System.Int16 value) { }
        // RVA: 0x0A8D1260  token: 0x6000662
        public virtual System.Void WriteValue(System.Nullable<System.Int16> value) { }
        // RVA: 0x0A8D1894  token: 0x6000663
        public virtual System.Void WriteValue(System.String value) { }
        // RVA: 0x0A8D199C  token: 0x6000664
        public virtual System.Void WriteValue(System.TimeSpan value) { }
        // RVA: 0x0A8D1B64  token: 0x6000665
        public virtual System.Void WriteValue(System.Nullable<System.TimeSpan> value) { }
        // RVA: 0x0A8D1E20  token: 0x6000666
        public virtual System.Void WriteValue(System.UInt32 value) { }
        // RVA: 0x0A8D2268  token: 0x6000667
        public virtual System.Void WriteValue(System.Nullable<System.UInt32> value) { }
        // RVA: 0x0A8D2144  token: 0x6000668
        public virtual System.Void WriteValue(System.UInt64 value) { }
        // RVA: 0x0A8D2004  token: 0x6000669
        public virtual System.Void WriteValue(System.Nullable<System.UInt64> value) { }
        // RVA: 0x0A8D1C78  token: 0x600066A
        public virtual System.Void WriteValue(System.Uri value) { }
        // RVA: 0x0A8D19EC  token: 0x600066B
        public virtual System.Void WriteValue(System.UInt16 value) { }
        // RVA: 0x0A8D1808  token: 0x600066C
        public virtual System.Void WriteValue(System.Nullable<System.UInt16> value) { }
        // RVA: 0x0A8D0BE4  token: 0x600066D
        public virtual System.Void WriteComment(System.String text) { }
        // RVA: 0x0A8D0EC4  token: 0x600066E
        public virtual System.Void WriteStartArray() { }
        // RVA: 0x0A8D0C40  token: 0x600066F
        public virtual System.Void WriteEndArray() { }
        // RVA: 0x0A8D0F08  token: 0x6000670
        public virtual System.Void WriteStartConstructor(System.String name) { }
        // RVA: 0x0A8D0C84  token: 0x6000671
        public virtual System.Void WriteEndConstructor() { }
        // RVA: 0x0A8D0D50  token: 0x6000672
        public virtual System.Void WritePropertyName(System.String name) { }
        // RVA: 0x0A8D0DAC  token: 0x6000673
        public virtual System.Void WritePropertyName(System.String name, System.Boolean escape) { }
        // RVA: 0x0A8D0F64  token: 0x6000674
        public virtual System.Void WriteStartObject() { }
        // RVA: 0x0A8D0CC8  token: 0x6000675
        public virtual System.Void WriteEndObject() { }
        // RVA: 0x0A8D0E1C  token: 0x6000676
        public virtual System.Void WriteRawValue(System.String json) { }
        // RVA: 0x0A8D0E78  token: 0x6000677
        public virtual System.Void WriteRaw(System.String json) { }
        // RVA: 0x0A8D0B7C  token: 0x6000678
        public virtual System.Void Close() { }

    }

}

namespace Newtonsoft.Json.Utilities
{

    // TypeToken: 0x200003F  // size: 0x10
    public static class AotHelper
    {
        // Fields
        private static System.Boolean s_alwaysFalse;  // static @ 0x0

        // Methods
        // RVA: 0x04A25330  token: 0x6000207
        public static System.Void Ensure(System.Action action) { }
        // RVA: -1  // generic def  token: 0x6000208
        public static System.Void EnsureList() { }
        // RVA: 0x0A8AEC7C  token: 0x6000209
        public static System.Boolean IsFalse() { }
        // RVA: 0x04D269F0  token: 0x600020A
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000041  // size: 0x30
    public class Base64Encoder
    {
        // Fields
        private readonly System.Char[] _charsLine;  // 0x10
        private readonly System.IO.TextWriter _writer;  // 0x18
        private System.Byte[] _leftOverBytes;  // 0x20
        private System.Int32 _leftOverBytesCount;  // 0x28

        // Methods
        // RVA: 0x0A8AF1F4  token: 0x600020E
        public System.Void .ctor(System.IO.TextWriter writer) { }
        // RVA: 0x0A8AF04C  token: 0x600020F
        private System.Void ValidateEncode(System.Byte[] buffer, System.Int32 index, System.Int32 count) { }
        // RVA: 0x0A8AECCC  token: 0x6000210
        public System.Void Encode(System.Byte[] buffer, System.Int32 index, System.Int32 count) { }
        // RVA: 0x0A8AEF64  token: 0x6000211
        private System.Void StoreLeftOverBytes(System.Byte[] buffer, System.Int32 index, System.Int32& count) { }
        // RVA: 0x0A8AEED4  token: 0x6000212
        private System.Boolean FulfillFromLeftover(System.Byte[] buffer, System.Int32 index, System.Int32& count) { }
        // RVA: 0x0A8AEE38  token: 0x6000213
        public System.Void Flush() { }
        // RVA: 0x0A8AF1C4  token: 0x6000214
        private System.Void WriteChars(System.Char[] chars, System.Int32 index, System.Int32 count) { }

    }

    // TypeToken: 0x2000042
    public class BidirectionalDictionary`2
    {
        // Fields
        private readonly System.Collections.Generic.IDictionary<TFirst,TSecond> _firstToSecond;  // 0x0
        private readonly System.Collections.Generic.IDictionary<TSecond,TFirst> _secondToFirst;  // 0x0
        private readonly System.String _duplicateFirstErrorMessage;  // 0x0
        private readonly System.String _duplicateSecondErrorMessage;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000215
        public System.Void .ctor(System.Collections.Generic.IEqualityComparer<TFirst> firstEqualityComparer, System.Collections.Generic.IEqualityComparer<TSecond> secondEqualityComparer, System.String duplicateFirstErrorMessage, System.String duplicateSecondErrorMessage) { }
        // RVA: -1  // not resolved  token: 0x6000216
        public System.Void Set(TFirst first, TSecond second) { }
        // RVA: -1  // not resolved  token: 0x6000217
        public System.Boolean TryGetByFirst(TFirst first, TSecond& second) { }
        // RVA: -1  // not resolved  token: 0x6000218
        public System.Boolean TryGetBySecond(TSecond second, TFirst& first) { }

    }

    // TypeToken: 0x2000043  // size: 0x10
    public static class BoxedPrimitives
    {
        // Fields
        private static readonly System.Object BooleanTrue;  // static @ 0x0
        private static readonly System.Object BooleanFalse;  // static @ 0x8
        private static readonly System.Object Int32_M1;  // static @ 0x10
        private static readonly System.Object Int32_0;  // static @ 0x18
        private static readonly System.Object Int32_1;  // static @ 0x20
        private static readonly System.Object Int32_2;  // static @ 0x28
        private static readonly System.Object Int32_3;  // static @ 0x30
        private static readonly System.Object Int32_4;  // static @ 0x38
        private static readonly System.Object Int32_5;  // static @ 0x40
        private static readonly System.Object Int32_6;  // static @ 0x48
        private static readonly System.Object Int32_7;  // static @ 0x50
        private static readonly System.Object Int32_8;  // static @ 0x58
        private static readonly System.Object Int64_M1;  // static @ 0x60
        private static readonly System.Object Int64_0;  // static @ 0x68
        private static readonly System.Object Int64_1;  // static @ 0x70
        private static readonly System.Object Int64_2;  // static @ 0x78
        private static readonly System.Object Int64_3;  // static @ 0x80
        private static readonly System.Object Int64_4;  // static @ 0x88
        private static readonly System.Object Int64_5;  // static @ 0x90
        private static readonly System.Object Int64_6;  // static @ 0x98
        private static readonly System.Object Int64_7;  // static @ 0xa0
        private static readonly System.Object Int64_8;  // static @ 0xa8
        private static readonly System.Object DecimalZero;  // static @ 0xb0
        private static readonly System.Object DoubleNaN;  // static @ 0xb8
        private static readonly System.Object DoublePositiveInfinity;  // static @ 0xc0
        private static readonly System.Object DoubleNegativeInfinity;  // static @ 0xc8
        private static readonly System.Object DoubleZero;  // static @ 0xd0

        // Methods
        // RVA: 0x03429720  token: 0x6000219
        private static System.Object Get(System.Boolean value) { }
        // RVA: 0x03B2F4D0  token: 0x600021A
        private static System.Object Get(System.Int32 value) { }
        // RVA: 0x03428030  token: 0x600021B
        private static System.Object Get(System.Int64 value) { }
        // RVA: 0x0A8AFD80  token: 0x600021C
        private static System.Object Get(System.Decimal value) { }
        // RVA: 0x039F6160  token: 0x600021D
        private static System.Object Get(System.Double value) { }
        // RVA: 0x04305D50  token: 0x600021E
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000044  // size: 0x10
    public static class CollectionUtils
    {
        // Methods
        // RVA: -1  // generic def  token: 0x600021F
        public static System.Boolean IsNullOrEmpty(System.Collections.Generic.ICollection<T> collection) { }
        // RVA: -1  // generic def  token: 0x6000220
        public static System.Void AddRange(System.Collections.Generic.IList<T> initial, System.Collections.Generic.IEnumerable<T> collection) { }
        // RVA: 0x035CF890  token: 0x6000221
        public static System.Boolean IsDictionaryType(System.Type type) { }
        // RVA: 0x03FD8F30  token: 0x6000222
        public static System.Reflection.ConstructorInfo ResolveEnumerableCollectionConstructor(System.Type collectionType, System.Type collectionItemType) { }
        // RVA: 0x03FD9030  token: 0x6000223
        public static System.Reflection.ConstructorInfo ResolveEnumerableCollectionConstructor(System.Type collectionType, System.Type collectionItemType, System.Type constructorArgumentType) { }
        // RVA: -1  // generic def  token: 0x6000224
        public static System.Int32 IndexOf(System.Collections.Generic.IEnumerable<T> collection, System.Func<T,System.Boolean> predicate) { }
        // RVA: -1  // generic def  token: 0x6000225
        public static System.Boolean Contains(System.Collections.Generic.List<T> list, T value, System.Collections.IEqualityComparer comparer) { }
        // RVA: -1  // generic def  token: 0x6000226
        public static System.Int32 IndexOfReference(System.Collections.Generic.List<T> list, T item) { }
        // RVA: -1  // generic def  token: 0x6000227
        public static System.Void FastReverse(System.Collections.Generic.List<T> list) { }
        // RVA: 0x0A8B0098  token: 0x6000228
        private static System.Collections.Generic.IList<System.Int32> GetDimensions(System.Collections.IList values, System.Int32 dimensionsCount) { }
        // RVA: 0x0A8AFEC4  token: 0x6000229
        private static System.Void CopyFromJaggedToMultidimensionalArray(System.Collections.IList values, System.Array multidimensionalArray, System.Int32[] indices) { }
        // RVA: 0x0A8B019C  token: 0x600022A
        private static System.Object JaggedArrayGetValue(System.Collections.IList values, System.Int32[] indices) { }
        // RVA: 0x0A8B0238  token: 0x600022B
        public static System.Array ToMultidimensionalArray(System.Collections.IList values, System.Type type, System.Int32 rank) { }
        // RVA: -1  // generic def  token: 0x600022C
        public static T[] ArrayEmpty() { }

    }

    // TypeToken: 0x2000046
    public interface IWrappedCollection : System.Collections.IList, System.Collections.ICollection, System.Collections.IEnumerable
    {
        // Properties
        System.Object UnderlyingCollection { get; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x2000047
    public class CollectionWrapper`1 : System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, Newtonsoft.Json.Utilities.IWrappedCollection, System.Collections.IList, System.Collections.ICollection
    {
        // Fields
        private readonly System.Collections.IList _list;  // 0x0
        private readonly System.Collections.Generic.ICollection<T> _genericCollection;  // 0x0
        private System.Object _syncRoot;  // 0x0

        // Properties
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        System.Boolean IsReadOnly { get; /* RVA: -1  // not resolved */ }
        System.Boolean System.Collections.IList.IsFixedSize { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IList.Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Boolean System.Collections.ICollection.IsSynchronized { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.ICollection.SyncRoot { get; /* RVA: -1  // not resolved */ }
        System.Object UnderlyingCollection { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600022F
        public System.Void .ctor(System.Collections.IList list) { }
        // RVA: -1  // not resolved  token: 0x6000230
        public System.Void .ctor(System.Collections.Generic.ICollection<T> list) { }
        // RVA: -1  // not resolved  token: 0x6000231
        public virtual System.Void Add(T item) { }
        // RVA: -1  // not resolved  token: 0x6000232
        public virtual System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x6000233
        public virtual System.Boolean Contains(T item) { }
        // RVA: -1  // not resolved  token: 0x6000234
        public virtual System.Void CopyTo(T[] array, System.Int32 arrayIndex) { }
        // RVA: -1  // not resolved  token: 0x6000237
        public virtual System.Boolean Remove(T item) { }
        // RVA: -1  // not resolved  token: 0x6000238
        public virtual System.Collections.Generic.IEnumerator<T> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000239
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x600023A
        private virtual System.Int32 System.Collections.IList.Add(System.Object value) { }
        // RVA: -1  // not resolved  token: 0x600023B
        private virtual System.Boolean System.Collections.IList.Contains(System.Object value) { }
        // RVA: -1  // not resolved  token: 0x600023C
        private virtual System.Int32 System.Collections.IList.IndexOf(System.Object value) { }
        // RVA: -1  // not resolved  token: 0x600023D
        private virtual System.Void System.Collections.IList.RemoveAt(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x600023E
        private virtual System.Void System.Collections.IList.Insert(System.Int32 index, System.Object value) { }
        // RVA: -1  // not resolved  token: 0x6000240
        private virtual System.Void System.Collections.IList.Remove(System.Object value) { }
        // RVA: -1  // not resolved  token: 0x6000243
        private virtual System.Void System.Collections.ICollection.CopyTo(System.Array array, System.Int32 arrayIndex) { }
        // RVA: -1  // not resolved  token: 0x6000246
        private static System.Void VerifyValueType(System.Object value) { }
        // RVA: -1  // not resolved  token: 0x6000247
        private static System.Boolean IsCompatibleObject(System.Object value) { }

    }

    // TypeToken: 0x2000048  // size: 0x14
    public sealed struct PrimitiveTypeCode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode Empty;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode Object;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode Char;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode CharNullable;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode Boolean;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode BooleanNullable;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode SByte;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode SByteNullable;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode Int16;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode Int16Nullable;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode UInt16;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode UInt16Nullable;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode Int32;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode Int32Nullable;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode Byte;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode ByteNullable;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode UInt32;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode UInt32Nullable;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode Int64;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode Int64Nullable;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode UInt64;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode UInt64Nullable;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode Single;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode SingleNullable;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode Double;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode DoubleNullable;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode DateTime;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode DateTimeNullable;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode DateTimeOffset;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode DateTimeOffsetNullable;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode Decimal;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode DecimalNullable;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode Guid;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode GuidNullable;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode TimeSpan;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode TimeSpanNullable;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode BigInteger;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode BigIntegerNullable;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode Uri;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode String;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode Bytes;  // const
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode DBNull;  // const

    }

    // TypeToken: 0x2000049  // size: 0x20
    public class TypeInformation
    {
        // Fields
        private readonly System.Type <Type>k__BackingField;  // 0x10
        private readonly Newtonsoft.Json.Utilities.PrimitiveTypeCode <TypeCode>k__BackingField;  // 0x18

        // Properties
        System.Type Type { get; /* RVA: 0x02B2ECC0 */ }
        Newtonsoft.Json.Utilities.PrimitiveTypeCode TypeCode { get; /* RVA: 0x011EF5B0 */ }

        // Methods
        // RVA: 0x03B46FF0  token: 0x600024B
        public System.Void .ctor(System.Type type, Newtonsoft.Json.Utilities.PrimitiveTypeCode typeCode) { }

    }

    // TypeToken: 0x200004A  // size: 0x14
    public sealed struct ParseResult
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.Utilities.ParseResult None;  // const
        public static Newtonsoft.Json.Utilities.ParseResult Success;  // const
        public static Newtonsoft.Json.Utilities.ParseResult Overflow;  // const
        public static Newtonsoft.Json.Utilities.ParseResult Invalid;  // const

    }

    // TypeToken: 0x200004B  // size: 0x10
    public static class ConvertUtils
    {
        // Fields
        private static readonly System.Collections.Generic.Dictionary<System.Type,Newtonsoft.Json.Utilities.PrimitiveTypeCode> TypeCodeMap;  // static @ 0x0
        private static readonly Newtonsoft.Json.Utilities.TypeInformation[] PrimitiveTypeCodes;  // static @ 0x8
        private static readonly Newtonsoft.Json.Utilities.ThreadSafeStore<Newtonsoft.Json.Utilities.StructMultiKey<System.Type,System.Type>,System.Func<System.Object,System.Object>> CastConverters;  // static @ 0x10

        // Methods
        // RVA: 0x035D0DC0  token: 0x600024C
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode GetTypeCode(System.Type t) { }
        // RVA: 0x035D0F90  token: 0x600024D
        public static Newtonsoft.Json.Utilities.PrimitiveTypeCode GetTypeCode(System.Type t, System.Boolean& isEnum) { }
        // RVA: 0x0A8B16FC  token: 0x600024E
        public static Newtonsoft.Json.Utilities.TypeInformation GetTypeInformation(System.IConvertible convertable) { }
        // RVA: 0x035D0020  token: 0x600024F
        public static System.Boolean IsConvertible(System.Type t) { }
        // RVA: 0x0A8B177C  token: 0x6000250
        public static System.TimeSpan ParseTimeSpan(System.String input) { }
        // RVA: 0x0A8B0710  token: 0x6000251
        private static System.Func<System.Object,System.Object> CreateCastConverter(Newtonsoft.Json.Utilities.StructMultiKey<System.Type,System.Type> t) { }
        // RVA: 0x0A8B17EC  token: 0x6000252
        private static System.Numerics.BigInteger ToBigInteger(System.Object value) { }
        // RVA: 0x0A8B12CC  token: 0x6000253
        public static System.Object FromBigInteger(System.Numerics.BigInteger i, System.Type targetType) { }
        // RVA: 0x0A8B0458  token: 0x6000254
        public static System.Object Convert(System.Object initialValue, System.Globalization.CultureInfo culture, System.Type targetType) { }
        // RVA: 0x0A8B23CC  token: 0x6000255
        private static System.Boolean TryConvert(System.Object initialValue, System.Globalization.CultureInfo culture, System.Type targetType, System.Object& value) { }
        // RVA: 0x0A8B1BA8  token: 0x6000256
        private static Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult TryConvertInternal(System.Object initialValue, System.Globalization.CultureInfo culture, System.Type targetType, System.Object& value) { }
        // RVA: 0x0A8B032C  token: 0x6000257
        public static System.Object ConvertOrCast(System.Object initialValue, System.Globalization.CultureInfo culture, System.Type targetType) { }
        // RVA: 0x0A8B10BC  token: 0x6000258
        private static System.Object EnsureTypeAssignable(System.Object value, System.Type initialType, System.Type targetType) { }
        // RVA: 0x0A8B2564  token: 0x6000259
        public static System.Boolean VersionTryParse(System.String input, System.Version& result) { }
        // RVA: 0x035D0D00  token: 0x600025A
        public static System.Boolean IsInteger(System.Object value) { }
        // RVA: 0x038C05A0  token: 0x600025B
        public static Newtonsoft.Json.Utilities.ParseResult Int32TryParse(System.Char[] chars, System.Int32 start, System.Int32 length, System.Int32& value) { }
        // RVA: 0x03824B40  token: 0x600025C
        public static Newtonsoft.Json.Utilities.ParseResult Int64TryParse(System.Char[] chars, System.Int32 start, System.Int32 length, System.Int64& value) { }
        // RVA: 0x0A8B08D0  token: 0x600025D
        public static Newtonsoft.Json.Utilities.ParseResult DecimalTryParse(System.Char[] chars, System.Int32 start, System.Int32 length, System.Decimal& value) { }
        // RVA: 0x0A8B1B5C  token: 0x600025E
        public static System.Boolean TryConvertGuid(System.String s, System.Guid& g) { }
        // RVA: 0x0A8B24CC  token: 0x600025F
        public static System.Boolean TryHexTextToInt(System.Char[] text, System.Int32 start, System.Int32 end, System.Int32& value) { }
        // RVA: 0x044E2290  token: 0x6000260
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200004E  // size: 0x14
    public sealed struct ParserTimeZone
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static Newtonsoft.Json.Utilities.ParserTimeZone Unspecified;  // const
        public static Newtonsoft.Json.Utilities.ParserTimeZone Utc;  // const
        public static Newtonsoft.Json.Utilities.ParserTimeZone LocalWestOfUtc;  // const
        public static Newtonsoft.Json.Utilities.ParserTimeZone LocalEastOfUtc;  // const

    }

    // TypeToken: 0x200004F  // size: 0x48
    public sealed struct DateTimeParser
    {
        // Fields
        public System.Int32 Year;  // 0x10
        public System.Int32 Month;  // 0x14
        public System.Int32 Day;  // 0x18
        public System.Int32 Hour;  // 0x1c
        public System.Int32 Minute;  // 0x20
        public System.Int32 Second;  // 0x24
        public System.Int32 Fraction;  // 0x28
        public System.Int32 ZoneHour;  // 0x2c
        public System.Int32 ZoneMinute;  // 0x30
        public Newtonsoft.Json.Utilities.ParserTimeZone Zone;  // 0x34
        private System.Char[] _text;  // 0x38
        private System.Int32 _end;  // 0x40
        private static readonly System.Int32[] Power10;  // static @ 0x0
        private static readonly System.Int32 Lzyyyy;  // static @ 0x8
        private static readonly System.Int32 Lzyyyy_;  // static @ 0xc
        private static readonly System.Int32 Lzyyyy_MM;  // static @ 0x10
        private static readonly System.Int32 Lzyyyy_MM_;  // static @ 0x14
        private static readonly System.Int32 Lzyyyy_MM_dd;  // static @ 0x18
        private static readonly System.Int32 Lzyyyy_MM_ddT;  // static @ 0x1c
        private static readonly System.Int32 LzHH;  // static @ 0x20
        private static readonly System.Int32 LzHH_;  // static @ 0x24
        private static readonly System.Int32 LzHH_mm;  // static @ 0x28
        private static readonly System.Int32 LzHH_mm_;  // static @ 0x2c
        private static readonly System.Int32 LzHH_mm_ss;  // static @ 0x30
        private static readonly System.Int32 Lz_;  // static @ 0x34
        private static readonly System.Int32 Lz_zz;  // static @ 0x38

        // Methods
        // RVA: 0x0A8B2DB8  token: 0x6000263
        private static System.Void .cctor() { }
        // RVA: 0x0A8B2CCC  token: 0x6000264
        public System.Boolean Parse(System.Char[] text, System.Int32 startIndex, System.Int32 length) { }
        // RVA: 0x0A8B26E0  token: 0x6000265
        private System.Boolean ParseDate(System.Int32 start) { }
        // RVA: 0x0A8B2870  token: 0x6000266
        private System.Boolean ParseTimeAndZoneAndWhitespace(System.Int32 start) { }
        // RVA: 0x0A8B28E0  token: 0x6000267
        private System.Boolean ParseTime(System.Int32& start) { }
        // RVA: 0x0A8B2B60  token: 0x6000268
        private System.Boolean ParseZone(System.Int32 start) { }
        // RVA: 0x0A8B25DC  token: 0x6000269
        private System.Boolean Parse4Digit(System.Int32 start, System.Int32& num) { }
        // RVA: 0x0A8B256C  token: 0x600026A
        private System.Boolean Parse2Digit(System.Int32 start, System.Int32& num) { }
        // RVA: 0x0A8B26A8  token: 0x600026B
        private System.Boolean ParseChar(System.Int32 start, System.Char ch) { }

    }

    // TypeToken: 0x2000050  // size: 0x10
    public static class DateTimeUtils
    {
        // Fields
        private static readonly System.Int64 InitialJavaScriptDateTicks;  // static @ 0x0
        private static readonly System.Int32[] DaysToMonth365;  // static @ 0x8
        private static readonly System.Int32[] DaysToMonth366;  // static @ 0x10

        // Methods
        // RVA: 0x04B886C0  token: 0x600026C
        private static System.Void .cctor() { }
        // RVA: 0x0A8B3700  token: 0x600026D
        public static System.TimeSpan GetUtcOffset(System.DateTime d) { }
        // RVA: 0x0A8B38B0  token: 0x600026E
        public static System.Xml.XmlDateTimeSerializationMode ToSerializationMode(System.DateTimeKind kind) { }
        // RVA: 0x0A8B3424  token: 0x600026F
        private static System.DateTime EnsureDateTime(System.DateTime value, Newtonsoft.Json.DateTimeZoneHandling timeZone) { }
        // RVA: 0x0A8B3758  token: 0x6000270
        private static System.DateTime SwitchToLocalTime(System.DateTime value) { }
        // RVA: 0x0A8B3804  token: 0x6000271
        private static System.DateTime SwitchToUtcTime(System.DateTime value) { }
        // RVA: 0x0A8B3A78  token: 0x6000272
        private static System.Int64 ToUniversalTicks(System.DateTime dateTime) { }
        // RVA: 0x0A8B394C  token: 0x6000273
        private static System.Int64 ToUniversalTicks(System.DateTime dateTime, System.TimeSpan offset) { }
        // RVA: 0x0A8B30B0  token: 0x6000274
        private static System.Int64 ConvertDateTimeToJavaScriptTicks(System.DateTime dateTime, System.TimeSpan offset) { }
        // RVA: 0x0A8B310C  token: 0x6000275
        private static System.Int64 ConvertDateTimeToJavaScriptTicks(System.DateTime dateTime) { }
        // RVA: 0x0A8B3158  token: 0x6000276
        private static System.Int64 ConvertDateTimeToJavaScriptTicks(System.DateTime dateTime, System.Boolean convertToUtc) { }
        // RVA: 0x0A8B4F00  token: 0x6000277
        private static System.Int64 UniversalTicksToJavaScriptTicks(System.Int64 universalTicks) { }
        // RVA: 0x0A8B31F0  token: 0x6000278
        private static System.DateTime ConvertJavaScriptTicksToDateTime(System.Int64 javaScriptTicks) { }
        // RVA: 0x0A8B3BE0  token: 0x6000279
        private static System.Boolean TryParseDateTimeIso(Newtonsoft.Json.Utilities.StringReference text, Newtonsoft.Json.DateTimeZoneHandling dateTimeZoneHandling, System.DateTime& dt) { }
        // RVA: 0x0A8B41C0  token: 0x600027A
        private static System.Boolean TryParseDateTimeOffsetIso(Newtonsoft.Json.Utilities.StringReference text, System.DateTimeOffset& dt) { }
        // RVA: 0x0A8B32E8  token: 0x600027B
        private static System.DateTime CreateDateTime(Newtonsoft.Json.Utilities.DateTimeParser dateTimeParser) { }
        // RVA: 0x02FBB4D0  token: 0x600027C
        private static System.Boolean TryParseDateTime(Newtonsoft.Json.Utilities.StringReference s, Newtonsoft.Json.DateTimeZoneHandling dateTimeZoneHandling, System.String dateFormatString, System.Globalization.CultureInfo culture, System.DateTime& dt) { }
        // RVA: 0x0A8B49C8  token: 0x600027D
        private static System.Boolean TryParseDateTime(System.String s, Newtonsoft.Json.DateTimeZoneHandling dateTimeZoneHandling, System.String dateFormatString, System.Globalization.CultureInfo culture, System.DateTime& dt) { }
        // RVA: 0x0A8B453C  token: 0x600027E
        private static System.Boolean TryParseDateTimeOffset(Newtonsoft.Json.Utilities.StringReference s, System.String dateFormatString, System.Globalization.CultureInfo culture, System.DateTimeOffset& dt) { }
        // RVA: 0x0A8B4720  token: 0x600027F
        private static System.Boolean TryParseDateTimeOffset(System.String s, System.String dateFormatString, System.Globalization.CultureInfo culture, System.DateTimeOffset& dt) { }
        // RVA: 0x0A8B4C4C  token: 0x6000280
        private static System.Boolean TryParseMicrosoftDate(Newtonsoft.Json.Utilities.StringReference text, System.Int64& ticks, System.TimeSpan& offset, System.DateTimeKind& kind) { }
        // RVA: 0x0A8B3FF0  token: 0x6000281
        private static System.Boolean TryParseDateTimeMicrosoft(Newtonsoft.Json.Utilities.StringReference text, Newtonsoft.Json.DateTimeZoneHandling dateTimeZoneHandling, System.DateTime& dt) { }
        // RVA: 0x0A8B3B14  token: 0x6000282
        private static System.Boolean TryParseDateTimeExact(System.String text, Newtonsoft.Json.DateTimeZoneHandling dateTimeZoneHandling, System.String dateFormatString, System.Globalization.CultureInfo culture, System.DateTime& dt) { }
        // RVA: 0x0A8B4414  token: 0x6000283
        private static System.Boolean TryParseDateTimeOffsetMicrosoft(Newtonsoft.Json.Utilities.StringReference text, System.DateTimeOffset& dt) { }
        // RVA: 0x0A8B4120  token: 0x6000284
        private static System.Boolean TryParseDateTimeOffsetExact(System.String text, System.String dateFormatString, System.Globalization.CultureInfo culture, System.DateTimeOffset& dt) { }
        // RVA: 0x0A8B4DBC  token: 0x6000285
        private static System.Boolean TryReadOffset(Newtonsoft.Json.Utilities.StringReference offsetText, System.Int32 startIndex, System.TimeSpan& offset) { }
        // RVA: 0x0A8B52C8  token: 0x6000286
        private static System.Void WriteDateTimeString(System.IO.TextWriter writer, System.DateTime value, Newtonsoft.Json.DateFormatHandling format, System.String formatString, System.Globalization.CultureInfo culture) { }
        // RVA: 0x0A8B541C  token: 0x6000287
        private static System.Int32 WriteDateTimeString(System.Char[] chars, System.Int32 start, System.DateTime value, System.Nullable<System.TimeSpan> offset, System.DateTimeKind kind, Newtonsoft.Json.DateFormatHandling format) { }
        // RVA: 0x0A8B570C  token: 0x6000288
        private static System.Int32 WriteDefaultIsoDate(System.Char[] chars, System.Int32 start, System.DateTime dt) { }
        // RVA: 0x0A8B325C  token: 0x6000289
        private static System.Void CopyIntToCharArray(System.Char[] chars, System.Int32 start, System.Int32 value, System.Int32 digits) { }
        // RVA: 0x0A8B50F4  token: 0x600028A
        private static System.Int32 WriteDateTimeOffset(System.Char[] chars, System.Int32 start, System.TimeSpan offset, Newtonsoft.Json.DateFormatHandling format) { }
        // RVA: 0x0A8B4F64  token: 0x600028B
        private static System.Void WriteDateTimeOffsetString(System.IO.TextWriter writer, System.DateTimeOffset value, Newtonsoft.Json.DateFormatHandling format, System.String formatString, System.Globalization.CultureInfo culture) { }
        // RVA: 0x0A8B3544  token: 0x600028C
        private static System.Void GetDateValues(System.DateTime td, System.Int32& year, System.Int32& month, System.Int32& day) { }

    }

    // TypeToken: 0x2000051
    public interface IWrappedDictionary : System.Collections.IDictionary, System.Collections.ICollection, System.Collections.IEnumerable
    {
        // Properties
        System.Object UnderlyingDictionary { get; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x2000052
    public class DictionaryWrapper`2 : System.Collections.Generic.IDictionary`2, System.Collections.Generic.ICollection`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, Newtonsoft.Json.Utilities.IWrappedDictionary, System.Collections.IDictionary, System.Collections.ICollection
    {
        // Fields
        private readonly System.Collections.IDictionary _dictionary;  // 0x0
        private readonly System.Collections.Generic.IDictionary<TKey,TValue> _genericDictionary;  // 0x0
        private readonly System.Collections.Generic.IReadOnlyDictionary<TKey,TValue> _readOnlyDictionary;  // 0x0
        private System.Object _syncRoot;  // 0x0

        // Properties
        System.Collections.Generic.IDictionary<TKey,TValue> GenericDictionary { get; /* RVA: -1  // not resolved */ }
        System.Collections.Generic.ICollection<TKey> Keys { get; /* RVA: -1  // not resolved */ }
        System.Collections.Generic.ICollection<TValue> Values { get; /* RVA: -1  // not resolved */ }
        TValue Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        System.Boolean IsReadOnly { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IDictionary.Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Boolean System.Collections.IDictionary.IsFixedSize { get; /* RVA: -1  // not resolved */ }
        System.Collections.ICollection System.Collections.IDictionary.Keys { get; /* RVA: -1  // not resolved */ }
        System.Collections.ICollection System.Collections.IDictionary.Values { get; /* RVA: -1  // not resolved */ }
        System.Boolean System.Collections.ICollection.IsSynchronized { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.ICollection.SyncRoot { get; /* RVA: -1  // not resolved */ }
        System.Object UnderlyingDictionary { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600028F
        public virtual System.Void Add(TKey key, TValue value) { }
        // RVA: -1  // not resolved  token: 0x6000290
        public virtual System.Boolean ContainsKey(TKey key) { }
        // RVA: -1  // not resolved  token: 0x6000292
        public virtual System.Boolean Remove(TKey key) { }
        // RVA: -1  // not resolved  token: 0x6000293
        public virtual System.Boolean TryGetValue(TKey key, TValue& value) { }
        // RVA: -1  // not resolved  token: 0x6000297
        public virtual System.Void Add(System.Collections.Generic.KeyValuePair<TKey,TValue> item) { }
        // RVA: -1  // not resolved  token: 0x6000298
        public virtual System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x6000299
        public virtual System.Boolean Contains(System.Collections.Generic.KeyValuePair<TKey,TValue> item) { }
        // RVA: -1  // not resolved  token: 0x600029A
        public virtual System.Void CopyTo(System.Collections.Generic.KeyValuePair<TKey,TValue>[] array, System.Int32 arrayIndex) { }
        // RVA: -1  // not resolved  token: 0x600029D
        public virtual System.Boolean Remove(System.Collections.Generic.KeyValuePair<TKey,TValue> item) { }
        // RVA: -1  // not resolved  token: 0x600029E
        public virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey,TValue>> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x600029F
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x60002A0
        private virtual System.Void System.Collections.IDictionary.Add(System.Object key, System.Object value) { }
        // RVA: -1  // not resolved  token: 0x60002A3
        private virtual System.Collections.IDictionaryEnumerator System.Collections.IDictionary.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x60002A4
        private virtual System.Boolean System.Collections.IDictionary.Contains(System.Object key) { }
        // RVA: -1  // not resolved  token: 0x60002A7
        public virtual System.Void Remove(System.Object key) { }
        // RVA: -1  // not resolved  token: 0x60002A9
        private virtual System.Void System.Collections.ICollection.CopyTo(System.Array array, System.Int32 index) { }

    }

    // TypeToken: 0x2000055
    public class DynamicProxy`1
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x60002B7
        public virtual System.Collections.Generic.IEnumerable<System.String> GetDynamicMemberNames(T instance) { }
        // RVA: -1  // not resolved  token: 0x60002B8
        public virtual System.Boolean TryBinaryOperation(T instance, System.Dynamic.BinaryOperationBinder binder, System.Object arg, System.Object& result) { }
        // RVA: -1  // not resolved  token: 0x60002B9
        public virtual System.Boolean TryConvert(T instance, System.Dynamic.ConvertBinder binder, System.Object& result) { }
        // RVA: -1  // not resolved  token: 0x60002BA
        public virtual System.Boolean TryCreateInstance(T instance, System.Dynamic.CreateInstanceBinder binder, System.Object[] args, System.Object& result) { }
        // RVA: -1  // not resolved  token: 0x60002BB
        public virtual System.Boolean TryDeleteIndex(T instance, System.Dynamic.DeleteIndexBinder binder, System.Object[] indexes) { }
        // RVA: -1  // not resolved  token: 0x60002BC
        public virtual System.Boolean TryDeleteMember(T instance, System.Dynamic.DeleteMemberBinder binder) { }
        // RVA: -1  // not resolved  token: 0x60002BD
        public virtual System.Boolean TryGetIndex(T instance, System.Dynamic.GetIndexBinder binder, System.Object[] indexes, System.Object& result) { }
        // RVA: -1  // not resolved  token: 0x60002BE
        public virtual System.Boolean TryGetMember(T instance, System.Dynamic.GetMemberBinder binder, System.Object& result) { }
        // RVA: -1  // not resolved  token: 0x60002BF
        public virtual System.Boolean TryInvoke(T instance, System.Dynamic.InvokeBinder binder, System.Object[] args, System.Object& result) { }
        // RVA: -1  // not resolved  token: 0x60002C0
        public virtual System.Boolean TryInvokeMember(T instance, System.Dynamic.InvokeMemberBinder binder, System.Object[] args, System.Object& result) { }
        // RVA: -1  // not resolved  token: 0x60002C1
        public virtual System.Boolean TrySetIndex(T instance, System.Dynamic.SetIndexBinder binder, System.Object[] indexes, System.Object value) { }
        // RVA: -1  // not resolved  token: 0x60002C2
        public virtual System.Boolean TrySetMember(T instance, System.Dynamic.SetMemberBinder binder, System.Object value) { }
        // RVA: -1  // not resolved  token: 0x60002C3
        public virtual System.Boolean TryUnaryOperation(T instance, System.Dynamic.UnaryOperationBinder binder, System.Object& result) { }
        // RVA: -1  // not resolved  token: 0x60002C4
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000056
    public sealed class DynamicProxyMetaObject`1 : System.Dynamic.DynamicMetaObject
    {
        // Fields
        private readonly Newtonsoft.Json.Utilities.DynamicProxy<T> _proxy;  // 0x0

        // Properties
        System.Linq.Expressions.Expression[] NoArgs { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60002C5
        private System.Void .ctor(System.Linq.Expressions.Expression expression, T value, Newtonsoft.Json.Utilities.DynamicProxy<T> proxy) { }
        // RVA: -1  // not resolved  token: 0x60002C6
        private System.Boolean IsOverridden(System.String method) { }
        // RVA: -1  // not resolved  token: 0x60002C7
        public virtual System.Dynamic.DynamicMetaObject BindGetMember(System.Dynamic.GetMemberBinder binder) { }
        // RVA: -1  // not resolved  token: 0x60002C8
        public virtual System.Dynamic.DynamicMetaObject BindSetMember(System.Dynamic.SetMemberBinder binder, System.Dynamic.DynamicMetaObject value) { }
        // RVA: -1  // not resolved  token: 0x60002C9
        public virtual System.Dynamic.DynamicMetaObject BindDeleteMember(System.Dynamic.DeleteMemberBinder binder) { }
        // RVA: -1  // not resolved  token: 0x60002CA
        public virtual System.Dynamic.DynamicMetaObject BindConvert(System.Dynamic.ConvertBinder binder) { }
        // RVA: -1  // not resolved  token: 0x60002CB
        public virtual System.Dynamic.DynamicMetaObject BindInvokeMember(System.Dynamic.InvokeMemberBinder binder, System.Dynamic.DynamicMetaObject[] args) { }
        // RVA: -1  // not resolved  token: 0x60002CC
        public virtual System.Dynamic.DynamicMetaObject BindCreateInstance(System.Dynamic.CreateInstanceBinder binder, System.Dynamic.DynamicMetaObject[] args) { }
        // RVA: -1  // not resolved  token: 0x60002CD
        public virtual System.Dynamic.DynamicMetaObject BindInvoke(System.Dynamic.InvokeBinder binder, System.Dynamic.DynamicMetaObject[] args) { }
        // RVA: -1  // not resolved  token: 0x60002CE
        public virtual System.Dynamic.DynamicMetaObject BindBinaryOperation(System.Dynamic.BinaryOperationBinder binder, System.Dynamic.DynamicMetaObject arg) { }
        // RVA: -1  // not resolved  token: 0x60002CF
        public virtual System.Dynamic.DynamicMetaObject BindUnaryOperation(System.Dynamic.UnaryOperationBinder binder) { }
        // RVA: -1  // not resolved  token: 0x60002D0
        public virtual System.Dynamic.DynamicMetaObject BindGetIndex(System.Dynamic.GetIndexBinder binder, System.Dynamic.DynamicMetaObject[] indexes) { }
        // RVA: -1  // not resolved  token: 0x60002D1
        public virtual System.Dynamic.DynamicMetaObject BindSetIndex(System.Dynamic.SetIndexBinder binder, System.Dynamic.DynamicMetaObject[] indexes, System.Dynamic.DynamicMetaObject value) { }
        // RVA: -1  // not resolved  token: 0x60002D2
        public virtual System.Dynamic.DynamicMetaObject BindDeleteIndex(System.Dynamic.DeleteIndexBinder binder, System.Dynamic.DynamicMetaObject[] indexes) { }
        // RVA: -1  // not resolved  token: 0x60002D4
        private static System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> GetArgs(System.Dynamic.DynamicMetaObject[] args) { }
        // RVA: -1  // not resolved  token: 0x60002D5
        private static System.Linq.Expressions.Expression[] GetArgArray(System.Dynamic.DynamicMetaObject[] args) { }
        // RVA: -1  // not resolved  token: 0x60002D6
        private static System.Linq.Expressions.Expression[] GetArgArray(System.Dynamic.DynamicMetaObject[] args, System.Dynamic.DynamicMetaObject value) { }
        // RVA: -1  // not resolved  token: 0x60002D7
        private static System.Linq.Expressions.ConstantExpression Constant(System.Dynamic.DynamicMetaObjectBinder binder) { }
        // RVA: -1  // not resolved  token: 0x60002D8
        private System.Dynamic.DynamicMetaObject CallMethodWithResult(System.String methodName, System.Dynamic.DynamicMetaObjectBinder binder, System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> args, Newtonsoft.Json.Utilities.DynamicProxyMetaObject.Fallback<T> fallback, Newtonsoft.Json.Utilities.DynamicProxyMetaObject.Fallback<T> fallbackInvoke) { }
        // RVA: -1  // not resolved  token: 0x60002D9
        private System.Dynamic.DynamicMetaObject BuildCallMethodWithResult(System.String methodName, System.Dynamic.DynamicMetaObjectBinder binder, System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> args, System.Dynamic.DynamicMetaObject fallbackResult, Newtonsoft.Json.Utilities.DynamicProxyMetaObject.Fallback<T> fallbackInvoke) { }
        // RVA: -1  // not resolved  token: 0x60002DA
        private System.Dynamic.DynamicMetaObject CallMethodReturnLast(System.String methodName, System.Dynamic.DynamicMetaObjectBinder binder, System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> args, Newtonsoft.Json.Utilities.DynamicProxyMetaObject.Fallback<T> fallback) { }
        // RVA: -1  // not resolved  token: 0x60002DB
        private System.Dynamic.DynamicMetaObject CallMethodNoResult(System.String methodName, System.Dynamic.DynamicMetaObjectBinder binder, System.Linq.Expressions.Expression[] args, Newtonsoft.Json.Utilities.DynamicProxyMetaObject.Fallback<T> fallback) { }
        // RVA: -1  // not resolved  token: 0x60002DC
        private System.Dynamic.BindingRestrictions GetRestrictions() { }
        // RVA: -1  // not resolved  token: 0x60002DD
        public virtual System.Collections.Generic.IEnumerable<System.String> GetDynamicMemberNames() { }

    }

    // TypeToken: 0x2000066  // size: 0x10
    public static class DynamicUtils
    {
        // Methods
        // RVA: 0x0A8B6848  token: 0x60002FE
        public static System.Collections.Generic.IEnumerable<System.String> GetDynamicMemberNames(System.Dynamic.IDynamicMetaObjectProvider dynamicProvider) { }

    }

    // TypeToken: 0x2000068  // size: 0x30
    public class NoThrowGetBinderMember : System.Dynamic.GetMemberBinder
    {
        // Fields
        private readonly System.Dynamic.GetMemberBinder _innerBinder;  // 0x28

        // Methods
        // RVA: 0x0A8B84E4  token: 0x6000304
        public System.Void .ctor(System.Dynamic.GetMemberBinder innerBinder) { }
        // RVA: 0x0A8B8408  token: 0x6000305
        public virtual System.Dynamic.DynamicMetaObject FallbackGetMember(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject errorSuggestion) { }

    }

    // TypeToken: 0x2000069  // size: 0x30
    public class NoThrowSetBinderMember : System.Dynamic.SetMemberBinder
    {
        // Fields
        private readonly System.Dynamic.SetMemberBinder _innerBinder;  // 0x28

        // Methods
        // RVA: 0x0A8B8638  token: 0x6000306
        public System.Void .ctor(System.Dynamic.SetMemberBinder innerBinder) { }
        // RVA: 0x0A8B8528  token: 0x6000307
        public virtual System.Dynamic.DynamicMetaObject FallbackSetMember(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject value, System.Dynamic.DynamicMetaObject errorSuggestion) { }

    }

    // TypeToken: 0x200006A  // size: 0x10
    public class NoThrowExpressionVisitor : System.Linq.Expressions.ExpressionVisitor
    {
        // Fields
        private static readonly System.Object ErrorResult;  // static @ 0x0

        // Methods
        // RVA: 0x0A8B82BC  token: 0x6000308
        protected virtual System.Linq.Expressions.Expression VisitConditional(System.Linq.Expressions.ConditionalExpression node) { }
        // RVA: 0x041E1670  token: 0x6000309
        public System.Void .ctor() { }
        // RVA: 0x0A8B8398  token: 0x600030A
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200006B  // size: 0x30
    public class EnumInfo
    {
        // Fields
        public readonly System.Boolean IsFlags;  // 0x10
        public readonly System.UInt64[] Values;  // 0x18
        public readonly System.String[] Names;  // 0x20
        public readonly System.String[] ResolvedNames;  // 0x28

        // Methods
        // RVA: 0x0311D4E0  token: 0x600030B
        public System.Void .ctor(System.Boolean isFlags, System.UInt64[] values, System.String[] names, System.String[] resolvedNames) { }

    }

    // TypeToken: 0x200006C  // size: 0x10
    public static class EnumUtils
    {
        // Fields
        private static readonly Newtonsoft.Json.Utilities.ThreadSafeStore<Newtonsoft.Json.Utilities.StructMultiKey<System.Type,Newtonsoft.Json.Serialization.NamingStrategy>,Newtonsoft.Json.Utilities.EnumInfo> ValuesAndNamesPerEnum;  // static @ 0x0
        private static Newtonsoft.Json.Serialization.CamelCaseNamingStrategy _camelCaseNamingStrategy;  // static @ 0x8

        // Methods
        // RVA: 0x036FE560  token: 0x600030C
        private static Newtonsoft.Json.Utilities.EnumInfo InitializeValuesAndNames(Newtonsoft.Json.Utilities.StructMultiKey<System.Type,Newtonsoft.Json.Serialization.NamingStrategy> key) { }
        // RVA: 0x0A8B6AEC  token: 0x600030D
        public static System.Boolean TryToString(System.Type enumType, System.Object value, Newtonsoft.Json.Serialization.NamingStrategy namingStrategy, System.String& name) { }
        // RVA: 0x0A8B68B0  token: 0x600030E
        private static System.String InternalFlagsFormat(Newtonsoft.Json.Utilities.EnumInfo entry, System.UInt64 result) { }
        // RVA: 0x037D8AB0  token: 0x600030F
        public static Newtonsoft.Json.Utilities.EnumInfo GetEnumValuesAndNames(System.Type enumType) { }
        // RVA: 0x036FE9A0  token: 0x6000310
        private static System.UInt64 ToUInt64(System.Object value) { }
        // RVA: 0x035D1500  token: 0x6000311
        public static System.Object ParseEnum(System.Type enumType, Newtonsoft.Json.Serialization.NamingStrategy namingStrategy, System.String value, System.Boolean disallowNumber) { }
        // RVA: 0x0A8B6A28  token: 0x6000312
        private static System.Nullable<System.Int32> MatchName(System.String value, System.String[] enumNames, System.String[] resolvedNames, System.Int32 valueIndex, System.Int32 valueSubstringLength, System.StringComparison comparison) { }
        // RVA: 0x035D1180  token: 0x6000313
        private static System.Nullable<System.Int32> FindIndexByName(System.String[] enumNames, System.String value, System.Int32 valueIndex, System.Int32 valueSubstringLength, System.StringComparison comparison) { }
        // RVA: 0x04B445E0  token: 0x6000314
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200006E  // size: 0x20
    public class FSharpFunction
    {
        // Fields
        private readonly System.Object _instance;  // 0x10
        private readonly Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object> _invoker;  // 0x18

        // Methods
        // RVA: 0x032AF420  token: 0x6000318
        public System.Void .ctor(System.Object instance, Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object> invoker) { }
        // RVA: 0x0A8B6C74  token: 0x6000319
        public System.Object Invoke(System.Object[] args) { }

    }

    // TypeToken: 0x200006F  // size: 0x70
    public class FSharpUtils
    {
        // Fields
        private static readonly System.Object Lock;  // static @ 0x0
        private static Newtonsoft.Json.Utilities.FSharpUtils _instance;  // static @ 0x8
        private System.Reflection.MethodInfo _ofSeq;  // 0x10
        private System.Type _mapType;  // 0x18
        private System.Reflection.Assembly <FSharpCoreAssembly>k__BackingField;  // 0x20
        private Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object> <IsUnion>k__BackingField;  // 0x28
        private Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object> <GetUnionCases>k__BackingField;  // 0x30
        private Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object> <PreComputeUnionTagReader>k__BackingField;  // 0x38
        private Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object> <PreComputeUnionReader>k__BackingField;  // 0x40
        private Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object> <PreComputeUnionConstructor>k__BackingField;  // 0x48
        private System.Func<System.Object,System.Object> <GetUnionCaseInfoDeclaringType>k__BackingField;  // 0x50
        private System.Func<System.Object,System.Object> <GetUnionCaseInfoName>k__BackingField;  // 0x58
        private System.Func<System.Object,System.Object> <GetUnionCaseInfoTag>k__BackingField;  // 0x60
        private Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object> <GetUnionCaseInfoFields>k__BackingField;  // 0x68

        // Properties
        Newtonsoft.Json.Utilities.FSharpUtils Instance { get; /* RVA: 0x0A8B7694 */ }
        System.Reflection.Assembly FSharpCoreAssembly { set; /* RVA: 0x05390F40 */ }
        Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object> IsUnion { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x02FDB880 */ }
        Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object> GetUnionCases { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x05396200 */ }
        Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object> PreComputeUnionTagReader { get; /* RVA: 0x04D85A50 */ set; /* RVA: 0x046A91B0 */ }
        Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object> PreComputeUnionReader { get; /* RVA: 0x04D85A60 */ set; /* RVA: 0x042B4AE0 */ }
        Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object> PreComputeUnionConstructor { get; /* RVA: 0x04D86200 */ set; /* RVA: 0x035A41A0 */ }
        System.Func<System.Object,System.Object> GetUnionCaseInfoDeclaringType { get; /* RVA: 0x04D86270 */ set; /* RVA: 0x02FDAEA0 */ }
        System.Func<System.Object,System.Object> GetUnionCaseInfoName { get; /* RVA: 0x02E56440 */ set; /* RVA: 0x036E5590 */ }
        System.Func<System.Object,System.Object> GetUnionCaseInfoTag { get; /* RVA: 0x04D86280 */ set; /* RVA: 0x063CC164 */ }
        Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object> GetUnionCaseInfoFields { get; /* RVA: 0x04D86230 */ set; /* RVA: 0x0485F170 */ }

        // Methods
        // RVA: 0x0A8B72B4  token: 0x600031A
        private System.Void .ctor(System.Reflection.Assembly fsharpCoreAssembly) { }
        // RVA: 0x0A8B7034  token: 0x600032F
        public static System.Void EnsureInitialized(System.Reflection.Assembly fsharpCoreAssembly) { }
        // RVA: 0x0A8B71E8  token: 0x6000330
        private static System.Reflection.MethodInfo GetMethodWithNonPublicFallback(System.Type type, System.String methodName, System.Reflection.BindingFlags bindingFlags) { }
        // RVA: 0x0A8B6CA4  token: 0x6000331
        private static Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object> CreateFSharpFuncCall(System.Type type, System.String methodName) { }
        // RVA: 0x0A8B6F74  token: 0x6000332
        public Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> CreateSeq(System.Type t) { }
        // RVA: 0x0A8B6E24  token: 0x6000333
        public Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> CreateMap(System.Type keyType, System.Type valueType) { }
        // RVA: -1  // generic def  token: 0x6000334
        public Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> BuildMapCreator() { }
        // RVA: 0x0A8B7244  token: 0x6000335
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000073  // size: 0x10
    public static class ImmutableCollectionsUtils
    {
        // Fields
        private static readonly System.Collections.Generic.IList<Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.ImmutableCollectionTypeInfo> ArrayContractImmutableCollectionDefinitions;  // static @ 0x0
        private static readonly System.Collections.Generic.IList<Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.ImmutableCollectionTypeInfo> DictionaryContractImmutableCollectionDefinitions;  // static @ 0x8

        // Methods
        // RVA: 0x042182D0  token: 0x600033D
        private static System.Boolean TryBuildImmutableForArrayContract(System.Type underlyingType, System.Type collectionItemType, System.Type& createdType, Newtonsoft.Json.Serialization.ObjectConstructor<System.Object>& parameterizedCreator) { }
        // RVA: 0x042180D0  token: 0x600033E
        private static System.Boolean TryBuildImmutableForDictionaryContract(System.Type underlyingType, System.Type keyItemType, System.Type valueItemType, System.Type& createdType, Newtonsoft.Json.Serialization.ObjectConstructor<System.Object>& parameterizedCreator) { }
        // RVA: 0x0464FD30  token: 0x600033F
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000078  // size: 0x10
    public static class BufferUtils
    {
        // Methods
        // RVA: 0x03778910  token: 0x600034F
        public static System.Char[] RentBuffer(Newtonsoft.Json.IArrayPool<System.Char> bufferPool, System.Int32 minSize) { }
        // RVA: 0x03778690  token: 0x6000350
        public static System.Void ReturnBuffer(Newtonsoft.Json.IArrayPool<System.Char> bufferPool, System.Char[] buffer) { }
        // RVA: 0x0A8AFE38  token: 0x6000351
        public static System.Char[] EnsureBufferSize(Newtonsoft.Json.IArrayPool<System.Char> bufferPool, System.Int32 size, System.Char[] buffer) { }

    }

    // TypeToken: 0x2000079  // size: 0x10
    public static class JavaScriptUtils
    {
        // Fields
        private static readonly System.Boolean[] SingleQuoteCharEscapeFlags;  // static @ 0x0
        private static readonly System.Boolean[] DoubleQuoteCharEscapeFlags;  // static @ 0x8
        private static readonly System.Boolean[] HtmlCharEscapeFlags;  // static @ 0x10

        // Methods
        // RVA: 0x04063150  token: 0x6000352
        private static System.Void .cctor() { }
        // RVA: 0x03425E70  token: 0x6000353
        public static System.Boolean[] GetCharEscapeFlags(Newtonsoft.Json.StringEscapeHandling stringEscapeHandling, System.Char quoteChar) { }
        // RVA: 0x030BCB50  token: 0x6000354
        public static System.Boolean ShouldEscapeJavaScriptString(System.String s, System.Boolean[] charEscapeFlags) { }
        // RVA: 0x03778F00  token: 0x6000355
        public static System.Void WriteEscapedJavaScriptString(System.IO.TextWriter writer, System.String s, System.Char delimiter, System.Boolean appendDelimiters, System.Boolean[] charEscapeFlags, Newtonsoft.Json.StringEscapeHandling stringEscapeHandling, Newtonsoft.Json.IArrayPool<System.Char> bufferPool, System.Char[]& writeBuffer) { }
        // RVA: 0x0A8B76E4  token: 0x6000356
        public static System.String ToEscapedJavaScriptString(System.String value, System.Char delimiter, System.Boolean appendDelimiters, Newtonsoft.Json.StringEscapeHandling stringEscapeHandling) { }
        // RVA: 0x037795A0  token: 0x6000357
        private static System.Int32 FirstCharToEscape(System.String s, System.Boolean[] charEscapeFlags, Newtonsoft.Json.StringEscapeHandling stringEscapeHandling) { }
        // RVA: 0x0A8B79C8  token: 0x6000358
        public static System.Boolean TryGetDateFromConstructorJson(Newtonsoft.Json.JsonReader reader, System.DateTime& dateTime, System.String& errorMessage) { }
        // RVA: 0x0A8B786C  token: 0x6000359
        private static System.Boolean TryGetDateConstructorValue(Newtonsoft.Json.JsonReader reader, System.Nullable<System.Int64>& integer, System.String& errorMessage) { }

    }

    // TypeToken: 0x200007A  // size: 0x10
    public static class JsonTokenUtils
    {
        // Methods
        // RVA: 0x03424FE0  token: 0x600035A
        private static System.Boolean IsEndToken(Newtonsoft.Json.JsonToken token) { }
        // RVA: 0x03425330  token: 0x600035B
        private static System.Boolean IsStartToken(Newtonsoft.Json.JsonToken token) { }
        // RVA: 0x045F7460  token: 0x600035C
        private static System.Boolean IsPrimitiveToken(Newtonsoft.Json.JsonToken token) { }

    }

    // TypeToken: 0x200007B  // size: 0x10
    public class LateBoundReflectionDelegateFactory : Newtonsoft.Json.Utilities.ReflectionDelegateFactory
    {
        // Fields
        private static readonly Newtonsoft.Json.Utilities.LateBoundReflectionDelegateFactory _instance;  // static @ 0x0

        // Properties
        Newtonsoft.Json.Utilities.ReflectionDelegateFactory Instance { get; /* RVA: 0x0A8B7E5C */ }

        // Methods
        // RVA: 0x04820B70  token: 0x600035E
        public virtual Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> CreateParameterizedConstructor(System.Reflection.MethodBase method) { }
        // RVA: -1  // generic def  token: 0x600035F
        public virtual Newtonsoft.Json.Utilities.MethodCall<T,System.Object> CreateMethodCall(System.Reflection.MethodBase method) { }
        // RVA: -1  // generic def  token: 0x6000360
        public virtual System.Func<T> CreateDefaultConstructor(System.Type type) { }
        // RVA: -1  // generic def  token: 0x6000361
        public virtual System.Func<T,System.Object> CreateGet(System.Reflection.PropertyInfo propertyInfo) { }
        // RVA: -1  // generic def  token: 0x6000362
        public virtual System.Func<T,System.Object> CreateGet(System.Reflection.FieldInfo fieldInfo) { }
        // RVA: -1  // generic def  token: 0x6000363
        public virtual System.Action<T,System.Object> CreateSet(System.Reflection.FieldInfo fieldInfo) { }
        // RVA: -1  // generic def  token: 0x6000364
        public virtual System.Action<T,System.Object> CreateSet(System.Reflection.PropertyInfo propertyInfo) { }
        // RVA: 0x041E1670  token: 0x6000365
        public System.Void .ctor() { }
        // RVA: 0x04D4E0D0  token: 0x6000366
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000083  // size: 0x10
    public static class MathUtils
    {
        // Methods
        // RVA: 0x03778AC0  token: 0x6000378
        public static System.Int32 IntLength(System.UInt64 i) { }
        // RVA: 0x0A8B7F4C  token: 0x6000379
        public static System.Char IntToHex(System.Int32 n) { }
        // RVA: 0x0A8B7EAC  token: 0x600037A
        public static System.Boolean ApproxEquals(System.Double d1, System.Double d2) { }

    }

    // TypeToken: 0x2000084
    public sealed class MethodCall`2 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x600037B
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x600037C
        public virtual TResult Invoke(T target, System.Object[] args) { }

    }

    // TypeToken: 0x2000085  // size: 0x10
    public static class MiscellaneousUtils
    {
        // Methods
        // RVA: 0x04164380  token: 0x600037D
        public static System.Boolean ValueEquals(System.Object objA, System.Object objB) { }
        // RVA: 0x0A8B7FE4  token: 0x600037E
        public static System.ArgumentOutOfRangeException CreateArgumentOutOfRangeException(System.String paramName, System.Object actualValue, System.String message) { }
        // RVA: 0x0A8B8238  token: 0x600037F
        public static System.String ToString(System.Object value) { }
        // RVA: 0x0A8B7F5C  token: 0x6000380
        public static System.Int32 ByteArrayCompare(System.Byte[] a1, System.Byte[] a2) { }
        // RVA: 0x0A8B80EC  token: 0x6000381
        public static System.String GetPrefix(System.String qualifiedName) { }
        // RVA: 0x0A8B80C0  token: 0x6000382
        public static System.String GetLocalName(System.String qualifiedName) { }
        // RVA: 0x0A8B8118  token: 0x6000383
        public static System.Void GetQualifiedNameParts(System.String qualifiedName, System.String& prefix, System.String& localName) { }
        // RVA: 0x0A8B81BC  token: 0x6000384
        private static System.Text.RegularExpressions.RegexOptions GetRegexOptions(System.String optionsText) { }

    }

    // TypeToken: 0x2000086  // size: 0x10
    public abstract class ReflectionDelegateFactory
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000385
        public System.Func<T,System.Object> CreateGet(System.Reflection.MemberInfo memberInfo) { }
        // RVA: -1  // generic def  token: 0x6000386
        public System.Action<T,System.Object> CreateSet(System.Reflection.MemberInfo memberInfo) { }
        // RVA: -1  // abstract  token: 0x6000387
        public virtual Newtonsoft.Json.Utilities.MethodCall<T,System.Object> CreateMethodCall(System.Reflection.MethodBase method) { }
        // RVA: -1  // abstract  token: 0x6000388
        public virtual Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> CreateParameterizedConstructor(System.Reflection.MethodBase method) { }
        // RVA: -1  // abstract  token: 0x6000389
        public virtual System.Func<T> CreateDefaultConstructor(System.Type type) { }
        // RVA: -1  // abstract  token: 0x600038A
        public virtual System.Func<T,System.Object> CreateGet(System.Reflection.PropertyInfo propertyInfo) { }
        // RVA: -1  // abstract  token: 0x600038B
        public virtual System.Func<T,System.Object> CreateGet(System.Reflection.FieldInfo fieldInfo) { }
        // RVA: -1  // abstract  token: 0x600038C
        public virtual System.Action<T,System.Object> CreateSet(System.Reflection.FieldInfo fieldInfo) { }
        // RVA: -1  // abstract  token: 0x600038D
        public virtual System.Action<T,System.Object> CreateSet(System.Reflection.PropertyInfo propertyInfo) { }
        // RVA: 0x041E1670  token: 0x600038E
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000087  // size: 0x28
    public class ReflectionMember
    {
        // Fields
        private System.Type <MemberType>k__BackingField;  // 0x10
        private System.Func<System.Object,System.Object> <Getter>k__BackingField;  // 0x18
        private System.Action<System.Object,System.Object> <Setter>k__BackingField;  // 0x20

        // Properties
        System.Type MemberType { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        System.Func<System.Object,System.Object> Getter { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x05392C40 */ }
        System.Action<System.Object,System.Object> Setter { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x6000395
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000088  // size: 0x20
    public class ReflectionObject
    {
        // Fields
        private readonly Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> <Creator>k__BackingField;  // 0x10
        private readonly System.Collections.Generic.IDictionary<System.String,Newtonsoft.Json.Utilities.ReflectionMember> <Members>k__BackingField;  // 0x18

        // Properties
        Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> Creator { get; /* RVA: 0x02B2ECC0 */ }
        System.Collections.Generic.IDictionary<System.String,Newtonsoft.Json.Utilities.ReflectionMember> Members { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x0A8B8ECC  token: 0x6000398
        private System.Void .ctor(Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> creator) { }
        // RVA: 0x0A8B8DD4  token: 0x6000399
        public System.Object GetValue(System.Object target, System.String member) { }
        // RVA: 0x0A8B8E48  token: 0x600039A
        public System.Void SetValue(System.Object target, System.String member, System.Object value) { }
        // RVA: 0x0A8B8D7C  token: 0x600039B
        public System.Type GetType(System.String member) { }
        // RVA: 0x0A8B867C  token: 0x600039C
        public static Newtonsoft.Json.Utilities.ReflectionObject Create(System.Type t, System.String[] memberNames) { }
        // RVA: 0x0A8B868C  token: 0x600039D
        public static Newtonsoft.Json.Utilities.ReflectionObject Create(System.Type t, System.Reflection.MethodBase creator, System.String[] memberNames) { }

    }

    // TypeToken: 0x200008C  // size: 0x10
    public static class ReflectionUtils
    {
        // Fields
        public static readonly System.Type[] EmptyTypes;  // static @ 0x0

        // Methods
        // RVA: 0x04D31570  token: 0x60003A4
        private static System.Void .cctor() { }
        // RVA: 0x02F34FC0  token: 0x60003A5
        public static System.Boolean IsVirtual(System.Reflection.PropertyInfo propertyInfo) { }
        // RVA: 0x02F34B10  token: 0x60003A6
        public static System.Reflection.MethodInfo GetBaseDefinition(System.Reflection.PropertyInfo propertyInfo) { }
        // RVA: 0x02F346B0  token: 0x60003A7
        public static System.Boolean IsPublic(System.Reflection.PropertyInfo property) { }
        // RVA: 0x02FB2150  token: 0x60003A8
        public static System.Type GetObjectType(System.Object v) { }
        // RVA: 0x0A8B95FC  token: 0x60003A9
        public static System.String GetTypeName(System.Type t, Newtonsoft.Json.TypeNameAssemblyFormatHandling assemblyFormat, Newtonsoft.Json.Serialization.ISerializationBinder binder) { }
        // RVA: 0x0A8B9508  token: 0x60003AA
        private static System.String GetFullyQualifiedTypeName(System.Type t, Newtonsoft.Json.Serialization.ISerializationBinder binder) { }
        // RVA: 0x0A8B97B8  token: 0x60003AB
        private static System.String RemoveAssemblyDetails(System.String fullyQualifiedTypeName) { }
        // RVA: 0x035CF460  token: 0x60003AC
        public static System.Boolean HasDefaultConstructor(System.Type t, System.Boolean nonPublic) { }
        // RVA: 0x035CEF40  token: 0x60003AD
        public static System.Reflection.ConstructorInfo GetDefaultConstructor(System.Type t) { }
        // RVA: 0x035CEF80  token: 0x60003AE
        public static System.Reflection.ConstructorInfo GetDefaultConstructor(System.Type t, System.Boolean nonPublic) { }
        // RVA: 0x035D0080  token: 0x60003AF
        public static System.Boolean IsNullable(System.Type t) { }
        // RVA: 0x035CFEC0  token: 0x60003B0
        public static System.Boolean IsNullableType(System.Type t) { }
        // RVA: 0x035CF0F0  token: 0x60003B1
        public static System.Type EnsureNotNullableType(System.Type t) { }
        // RVA: 0x035D0C80  token: 0x60003B2
        public static System.Type EnsureNotByRefType(System.Type t) { }
        // RVA: 0x044778A0  token: 0x60003B3
        public static System.Boolean IsGenericDefinition(System.Type type, System.Type genericInterfaceDefinition) { }
        // RVA: 0x035CF9D0  token: 0x60003B4
        public static System.Boolean ImplementsGenericDefinition(System.Type type, System.Type genericInterfaceDefinition) { }
        // RVA: 0x035CFD90  token: 0x60003B5
        public static System.Boolean ImplementsGenericDefinition(System.Type type, System.Type genericInterfaceDefinition, System.Type& implementingType) { }
        // RVA: 0x035CE250  token: 0x60003B6
        public static System.Boolean InheritsGenericDefinition(System.Type type, System.Type genericClassDefinition) { }
        // RVA: 0x035CE2A0  token: 0x60003B7
        public static System.Boolean InheritsGenericDefinition(System.Type type, System.Type genericClassDefinition, System.Type& implementingType) { }
        // RVA: 0x035CE350  token: 0x60003B8
        private static System.Boolean InheritsGenericDefinitionInternal(System.Type type, System.Type genericClassDefinition, System.Type& implementingType) { }
        // RVA: 0x049E6300  token: 0x60003B9
        public static System.Type GetCollectionItemType(System.Type type) { }
        // RVA: 0x0A8B9220  token: 0x60003BA
        public static System.Void GetDictionaryKeyValueTypes(System.Type dictionaryType, System.Type& keyType, System.Type& valueType) { }
        // RVA: 0x032203F0  token: 0x60003BB
        public static System.Type GetMemberUnderlyingType(System.Reflection.MemberInfo member) { }
        // RVA: 0x032B4820  token: 0x60003BC
        public static System.Boolean IsByRefLikeType(System.Type type) { }
        // RVA: 0x02F2EED0  token: 0x60003BD
        public static System.Boolean IsIndexedProperty(System.Reflection.PropertyInfo property) { }
        // RVA: 0x035949B0  token: 0x60003BE
        public static System.Object GetMemberValue(System.Reflection.MemberInfo member, System.Object target) { }
        // RVA: 0x02FB2E10  token: 0x60003BF
        public static System.Void SetMemberValue(System.Reflection.MemberInfo member, System.Object target, System.Object value) { }
        // RVA: 0x0321FED0  token: 0x60003C0
        public static System.Boolean CanReadMemberValue(System.Reflection.MemberInfo member, System.Boolean nonPublic) { }
        // RVA: 0x0321FDB0  token: 0x60003C1
        public static System.Boolean CanSetMemberValue(System.Reflection.MemberInfo member, System.Boolean nonPublic, System.Boolean canSetReadOnly) { }
        // RVA: 0x02F32150  token: 0x60003C2
        public static System.Collections.Generic.List<System.Reflection.MemberInfo> GetFieldsAndProperties(System.Type type, System.Reflection.BindingFlags bindingAttr) { }
        // RVA: 0x04CB1A30  token: 0x60003C3
        private static System.Boolean IsOverridenGenericMember(System.Reflection.MemberInfo memberInfo, System.Reflection.BindingFlags bindingAttr) { }
        // RVA: -1  // generic def  token: 0x60003C4
        public static T GetAttribute(System.Object attributeProvider) { }
        // RVA: -1  // generic def  token: 0x60003C5
        public static T GetAttribute(System.Object attributeProvider, System.Boolean inherit) { }
        // RVA: -1  // generic def  token: 0x60003C6
        public static T[] GetAttributes(System.Object attributeProvider, System.Boolean inherit) { }
        // RVA: 0x0312C910  token: 0x60003C7
        public static System.Attribute[] GetAttributes(System.Object attributeProvider, System.Type attributeType, System.Boolean inherit) { }
        // RVA: 0x02FBD590  token: 0x60003C8
        public static Newtonsoft.Json.Utilities.StructMultiKey<System.String,System.String> SplitFullyQualifiedTypeName(System.String fullyQualifiedTypeName) { }
        // RVA: 0x02FBEB90  token: 0x60003C9
        private static System.Nullable<System.Int32> GetAssemblyDelimiterIndex(System.String fullyQualifiedTypeName) { }
        // RVA: 0x0312D520  token: 0x60003CA
        public static System.Reflection.MemberInfo GetMemberInfoFromType(System.Type targetType, System.Reflection.MemberInfo memberInfo) { }
        // RVA: 0x02F32E60  token: 0x60003CB
        public static System.Collections.Generic.IEnumerable<System.Reflection.FieldInfo> GetFields(System.Type targetType, System.Reflection.BindingFlags bindingAttr) { }
        // RVA: 0x035D00D0  token: 0x60003CC
        private static System.Void GetChildPrivateFields(System.Collections.Generic.IList<System.Reflection.MemberInfo> initialFields, System.Type type, System.Reflection.BindingFlags bindingAttr) { }
        // RVA: 0x02F32F10  token: 0x60003CD
        public static System.Collections.Generic.IEnumerable<System.Reflection.PropertyInfo> GetProperties(System.Type targetType, System.Reflection.BindingFlags bindingAttr) { }
        // RVA: 0x035CED60  token: 0x60003CE
        public static System.Reflection.BindingFlags RemoveFlag(System.Reflection.BindingFlags bindingAttr, System.Reflection.BindingFlags flag) { }
        // RVA: 0x02F347C0  token: 0x60003CF
        private static System.Void GetChildPrivateProperties(System.Collections.Generic.IList<System.Reflection.PropertyInfo> initialProperties, System.Type type, System.Reflection.BindingFlags bindingAttr) { }
        // RVA: 0x0A8B96C0  token: 0x60003D0
        public static System.Boolean IsMethodOverridden(System.Type currentType, System.Type methodDeclaringType, System.String method) { }
        // RVA: 0x0A8B8F5C  token: 0x60003D1
        public static System.Object GetDefaultValue(System.Type type) { }

    }

    // TypeToken: 0x2000092  // size: 0x20
    public sealed struct StringBuffer
    {
        // Fields
        private System.Char[] _buffer;  // 0x10
        private System.Int32 _position;  // 0x18

        // Properties
        System.Int32 Position { get; /* RVA: 0x04D88B10 */ set; /* RVA: 0x04D88B40 */ }
        System.Boolean IsEmpty { get; /* RVA: 0x0314CD60 */ }
        System.Char[] InternalBuffer { get; /* RVA: 0x02B76770 */ }

        // Methods
        // RVA: 0x037787C0  token: 0x60003E4
        public System.Void .ctor(Newtonsoft.Json.IArrayPool<System.Char> bufferPool, System.Int32 initalSize) { }
        // RVA: 0x05393828  token: 0x60003E5
        private System.Void .ctor(System.Char[] buffer) { }
        // RVA: 0x04208C70  token: 0x60003E6
        public System.Void Append(Newtonsoft.Json.IArrayPool<System.Char> bufferPool, System.Char value) { }
        // RVA: 0x04208CD0  token: 0x60003E7
        public System.Void Append(Newtonsoft.Json.IArrayPool<System.Char> bufferPool, System.Char[] buffer, System.Int32 startIndex, System.Int32 count) { }
        // RVA: 0x037786D0  token: 0x60003E8
        public System.Void Clear(Newtonsoft.Json.IArrayPool<System.Char> bufferPool) { }
        // RVA: 0x0A8B98D4  token: 0x60003E9
        private System.Void EnsureSize(Newtonsoft.Json.IArrayPool<System.Char> bufferPool, System.Int32 appendLength) { }
        // RVA: 0x0A8B9940  token: 0x60003EA
        public virtual System.String ToString() { }
        // RVA: 0x0A8B9950  token: 0x60003EB
        public System.String ToString(System.Int32 start, System.Int32 length) { }

    }

    // TypeToken: 0x2000093  // size: 0x20
    public sealed struct StringReference
    {
        // Fields
        private readonly System.Char[] _chars;  // 0x10
        private readonly System.Int32 _startIndex;  // 0x18
        private readonly System.Int32 _length;  // 0x1c

        // Properties
        System.Char Item { get; /* RVA: 0x02FBB5E0 */ }
        System.Char[] Chars { get; /* RVA: 0x02B76770 */ }
        System.Int32 StartIndex { get; /* RVA: 0x04D88B10 */ }
        System.Int32 Length { get; /* RVA: 0x04D8CAD0 */ }

        // Methods
        // RVA: 0x05390898  token: 0x60003F1
        public System.Void .ctor(System.Char[] chars, System.Int32 startIndex, System.Int32 length) { }
        // RVA: 0x05390BC8  token: 0x60003F2
        public virtual System.String ToString() { }

    }

    // TypeToken: 0x2000094  // size: 0x10
    public static class StringReferenceExtensions
    {
        // Methods
        // RVA: 0x0A8B9A00  token: 0x60003F3
        public static System.Int32 IndexOf(Newtonsoft.Json.Utilities.StringReference s, System.Char c, System.Int32 startIndex, System.Int32 length) { }
        // RVA: 0x0A8B9A80  token: 0x60003F4
        public static System.Boolean StartsWith(Newtonsoft.Json.Utilities.StringReference s, System.String text) { }
        // RVA: 0x0A8B9974  token: 0x60003F5
        public static System.Boolean EndsWith(Newtonsoft.Json.Utilities.StringReference s, System.String text) { }

    }

    // TypeToken: 0x2000095  // size: 0x10
    public static class StringUtils
    {
        // Methods
        // RVA: 0x05390C14  token: 0x60003F6
        public static System.Boolean IsNullOrEmpty(System.String value) { }
        // RVA: 0x0A8B9E80  token: 0x60003F7
        public static System.String FormatWith(System.String format, System.IFormatProvider provider, System.Object arg0) { }
        // RVA: 0x0A8B9F18  token: 0x60003F8
        public static System.String FormatWith(System.String format, System.IFormatProvider provider, System.Object arg0, System.Object arg1) { }
        // RVA: 0x0A8B9DA0  token: 0x60003F9
        public static System.String FormatWith(System.String format, System.IFormatProvider provider, System.Object arg0, System.Object arg1, System.Object arg2) { }
        // RVA: 0x0A8B9CA4  token: 0x60003FA
        public static System.String FormatWith(System.String format, System.IFormatProvider provider, System.Object arg0, System.Object arg1, System.Object arg2, System.Object arg3) { }
        // RVA: 0x0A8B9BFC  token: 0x60003FB
        private static System.String FormatWith(System.String format, System.IFormatProvider provider, System.Object[] args) { }
        // RVA: 0x0A8B9B08  token: 0x60003FC
        public static System.IO.StringWriter CreateStringWriter(System.Int32 capacity) { }
        // RVA: 0x0A8BA26C  token: 0x60003FD
        public static System.Void ToCharAsUnicode(System.Char c, System.Char[] buffer) { }
        // RVA: -1  // generic def  token: 0x60003FE
        public static TSource ForgivingCaseSensitiveFind(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.String> valueSelector, System.String testValue) { }
        // RVA: 0x0A8BA0C0  token: 0x60003FF
        public static System.String ToCamelCase(System.String s) { }
        // RVA: 0x0A8BA34C  token: 0x6000400
        private static System.Char ToLower(System.Char c) { }
        // RVA: 0x0A8BA604  token: 0x6000401
        public static System.String ToSnakeCase(System.String s) { }
        // RVA: 0x0A8BA33C  token: 0x6000402
        public static System.String ToKebabCase(System.String s) { }
        // RVA: 0x0A8BA3BC  token: 0x6000403
        private static System.String ToSeparatedCase(System.String s, System.Char separator) { }
        // RVA: 0x0A8B9FF4  token: 0x6000404
        public static System.Boolean IsHighSurrogate(System.Char c) { }
        // RVA: 0x0A8BA040  token: 0x6000405
        public static System.Boolean IsLowSurrogate(System.Char c) { }
        // RVA: 0x0A8B9FD8  token: 0x6000406
        public static System.Int32 IndexOf(System.String s, System.Char c) { }
        // RVA: 0x0A8BA08C  token: 0x6000407
        public static System.Boolean StartsWith(System.String source, System.Char value) { }
        // RVA: 0x0A8B9BC4  token: 0x6000408
        public static System.Boolean EndsWith(System.String source, System.Char value) { }
        // RVA: 0x02FBD6B0  token: 0x6000409
        public static System.String Trim(System.String s, System.Int32 start, System.Int32 length) { }

    }

    // TypeToken: 0x2000098
    public sealed struct StructMultiKey`2 : System.IEquatable`1
    {
        // Fields
        public readonly T1 Value1;  // 0x0
        public readonly T2 Value2;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x600040D
        public System.Void .ctor(T1 v1, T2 v2) { }
        // RVA: -1  // not resolved  token: 0x600040E
        public virtual System.Int32 GetHashCode() { }
        // RVA: -1  // not resolved  token: 0x600040F
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: -1  // not resolved  token: 0x6000410
        public virtual System.Boolean Equals(Newtonsoft.Json.Utilities.StructMultiKey<T1,T2> other) { }

    }

    // TypeToken: 0x2000099
    public class ThreadSafeStore`2
    {
        // Fields
        private readonly System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue> _concurrentStore;  // 0x0
        private readonly System.Func<TKey,TValue> _creator;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000411
        public System.Void .ctor(System.Func<TKey,TValue> creator) { }
        // RVA: -1  // not resolved  token: 0x6000412
        public TValue Get(TKey key) { }

    }

    // TypeToken: 0x200009A  // size: 0x10
    public static class TypeExtensions
    {
        // Methods
        // RVA: 0x02FB3040  token: 0x6000413
        public static System.Reflection.MemberTypes MemberType(System.Reflection.MemberInfo memberInfo) { }
        // RVA: 0x0A8BA634  token: 0x6000414
        public static System.Boolean ContainsGenericParameters(System.Type type) { }
        // RVA: 0x035CFF60  token: 0x6000415
        public static System.Boolean IsInterface(System.Type type) { }
        // RVA: 0x035CFF40  token: 0x6000416
        public static System.Boolean IsGenericType(System.Type type) { }
        // RVA: 0x035CFF80  token: 0x6000417
        public static System.Boolean IsGenericTypeDefinition(System.Type type) { }
        // RVA: 0x032B4E20  token: 0x6000418
        public static System.Type BaseType(System.Type type) { }
        // RVA: 0x0A8BA614  token: 0x6000419
        public static System.Reflection.Assembly Assembly(System.Type type) { }
        // RVA: 0x02F3A240  token: 0x600041A
        public static System.Boolean IsEnum(System.Type type) { }
        // RVA: 0x035CE330  token: 0x600041B
        public static System.Boolean IsClass(System.Type type) { }
        // RVA: 0x035CFFD0  token: 0x600041C
        public static System.Boolean IsSealed(System.Type type) { }
        // RVA: 0x035CFFA0  token: 0x600041D
        public static System.Boolean IsAbstract(System.Type type) { }
        // RVA: 0x0A8BA8BC  token: 0x600041E
        public static System.Boolean IsVisible(System.Type type) { }
        // RVA: 0x03131F30  token: 0x600041F
        public static System.Boolean IsValueType(System.Type type) { }
        // RVA: 0x032B4CF0  token: 0x6000420
        public static System.Boolean AssignableToTypeName(System.Type type, System.String fullTypeName, System.Boolean searchInterfaces, System.Type& match) { }
        // RVA: 0x05396E6C  token: 0x6000421
        public static System.Boolean AssignableToTypeName(System.Type type, System.String fullTypeName, System.Boolean searchInterfaces) { }
        // RVA: 0x0A8BA654  token: 0x6000422
        public static System.Boolean ImplementInterface(System.Type type, System.Type interfaceType) { }

    }

    // TypeToken: 0x200009B  // size: 0x10
    public static class ValidationUtils
    {
        // Methods
        // RVA: 0x02FB3000  token: 0x6000423
        public static System.Void ArgumentNotNull(System.Object value, System.String parameterName) { }

    }

}

namespace System.Diagnostics.CodeAnalysis
{

    // TypeToken: 0x2000006  // size: 0x10
    public sealed class NotNullAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000006
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000007  // size: 0x18
    public sealed class NotNullWhenAttribute : System.Attribute
    {
        // Fields
        private readonly System.Boolean <ReturnValue>k__BackingField;  // 0x10

        // Methods
        // RVA: 0x04D86130  token: 0x6000007
        public System.Void .ctor(System.Boolean returnValue) { }

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

    // TypeToken: 0x2000004  // size: 0x18
    public sealed class NullableAttribute : System.Attribute
    {
        // Fields
        public readonly System.Byte[] NullableFlags;  // 0x10

        // Methods
        // RVA: 0x03F261D0  token: 0x6000003
        public System.Void .ctor(System.Byte ) { }
        // RVA: 0x053908C0  token: 0x6000004
        public System.Void .ctor(System.Byte[] ) { }

    }

    // TypeToken: 0x2000005  // size: 0x18
    public sealed class NullableContextAttribute : System.Attribute
    {
        // Fields
        public readonly System.Byte Flag;  // 0x10

        // Methods
        // RVA: 0x04D86130  token: 0x6000005
        public System.Void .ctor(System.Byte ) { }

    }

}

