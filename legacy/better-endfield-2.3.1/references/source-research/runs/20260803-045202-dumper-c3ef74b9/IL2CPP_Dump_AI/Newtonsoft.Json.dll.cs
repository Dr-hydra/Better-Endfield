// ========================================================
// Dumped by @desirepro
// Assembly: Newtonsoft.Json.dll
// Classes:  309
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

CLASS: Entry
TYPE:  class
TOKEN: 0x200000D
SIZE:  0x28
FIELDS:
  private   readonly System.String                   Value  // 0x10
  private   readonly System.Int32                    HashCode  // 0x18
  private           Newtonsoft.Json.DefaultJsonNameTable.EntryNext  // 0x20
METHODS:
  RVA=0x032B2C30  token=0x600000F  System.Void .ctor(System.String value, System.Int32 hashCode, Newtonsoft.Json.DefaultJsonNameTable.Entry next)
END_CLASS

CLASS: State
TYPE:  sealed struct
TOKEN: 0x2000026
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.JsonReader.StateStart  // const
  public    static  Newtonsoft.Json.JsonReader.StateComplete  // const
  public    static  Newtonsoft.Json.JsonReader.StateProperty  // const
  public    static  Newtonsoft.Json.JsonReader.StateObjectStart  // const
  public    static  Newtonsoft.Json.JsonReader.StateObject  // const
  public    static  Newtonsoft.Json.JsonReader.StateArrayStart  // const
  public    static  Newtonsoft.Json.JsonReader.StateArray  // const
  public    static  Newtonsoft.Json.JsonReader.StateClosed  // const
  public    static  Newtonsoft.Json.JsonReader.StatePostValue  // const
  public    static  Newtonsoft.Json.JsonReader.StateConstructorStart  // const
  public    static  Newtonsoft.Json.JsonReader.StateConstructor  // const
  public    static  Newtonsoft.Json.JsonReader.StateError  // const
  public    static  Newtonsoft.Json.JsonReader.StateFinished  // const
METHODS:
END_CLASS

CLASS: State
TYPE:  sealed struct
TOKEN: 0x2000031
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.JsonWriter.StateStart  // const
  public    static  Newtonsoft.Json.JsonWriter.StateProperty  // const
  public    static  Newtonsoft.Json.JsonWriter.StateObjectStart  // const
  public    static  Newtonsoft.Json.JsonWriter.StateObject  // const
  public    static  Newtonsoft.Json.JsonWriter.StateArrayStart  // const
  public    static  Newtonsoft.Json.JsonWriter.StateArray  // const
  public    static  Newtonsoft.Json.JsonWriter.StateConstructorStart  // const
  public    static  Newtonsoft.Json.JsonWriter.StateConstructor  // const
  public    static  Newtonsoft.Json.JsonWriter.StateClosed  // const
  public    static  Newtonsoft.Json.JsonWriter.StateError  // const
METHODS:
END_CLASS

CLASS: <>c__2`1
TYPE:  sealed class
TOKEN: 0x2000040
FIELDS:
  public    static readonly Newtonsoft.Json.Utilities.AotHelper.<>c__2<T><>9  // static @ 0x0
  public    static  System.Action                   <>9__2_0  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600020B  System.Void .cctor()
  RVA=-1  // not resolved  token=0x600020C  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600020D  System.Void <EnsureList>b__2_0()
END_CLASS

CLASS: EmptyArrayContainer`1
TYPE:  static class
TOKEN: 0x2000045
FIELDS:
  public    static readonly T[]                             Empty  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600022D  System.Void .cctor()
END_CLASS

CLASS: ConvertResult
TYPE:  sealed struct
TOKEN: 0x200004C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.Utilities.ConvertUtils.ConvertResultSuccess  // const
  public    static  Newtonsoft.Json.Utilities.ConvertUtils.ConvertResultCannotConvertNull  // const
  public    static  Newtonsoft.Json.Utilities.ConvertUtils.ConvertResultNotInstantiableType  // const
  public    static  Newtonsoft.Json.Utilities.ConvertUtils.ConvertResultNoValidConversion  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass8_0
TYPE:  sealed class
TOKEN: 0x200004D
SIZE:  0x18
FIELDS:
  public            Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object>call  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000261  System.Void .ctor()
  RVA=0x0A8BACF4  token=0x6000262  System.Object <CreateCastConverter>b__0(System.Object o)
END_CLASS

CLASS: DictionaryEnumerator`2
TYPE:  sealed struct
TOKEN: 0x2000053
IMPLEMENTS: System.Collections.IDictionaryEnumerator System.Collections.IEnumerator
FIELDS:
  private   readonly System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TEnumeratorKey,TEnumeratorValue>>_e  // 0x0
PROPERTIES:
  Entry  get=-1  // not resolved
  Key  get=-1  // not resolved
  Value  get=-1  // not resolved
  Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60002AD  System.Void .ctor(System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TEnumeratorKey,TEnumeratorValue>> e)
  RVA=-1  // not resolved  token=0x60002B2  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x60002B3  System.Void Reset()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000054
FIELDS:
  public    static readonly Newtonsoft.Json.Utilities.DictionaryWrapper.<>c<TKey,TValue><>9  // static @ 0x0
  public    static  System.Func<System.Collections.DictionaryEntry,System.Collections.Generic.KeyValuePair<TKey,TValue>><>9__29_0  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60002B4  System.Void .cctor()
  RVA=-1  // not resolved  token=0x60002B5  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60002B6  System.Collections.Generic.KeyValuePair<TKey,TValue> <GetEnumerator>b__29_0(System.Collections.DictionaryEntry de)
END_CLASS

CLASS: Fallback
TYPE:  sealed class
TOKEN: 0x2000057
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x60002DE  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x60002DF  System.Dynamic.DynamicMetaObject Invoke(System.Dynamic.DynamicMetaObject errorSuggestion)
END_CLASS

CLASS: GetBinderAdapter
TYPE:  sealed class
TOKEN: 0x2000058
EXTENDS: System.Dynamic.GetMemberBinder
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60002E0  System.Void .ctor(System.Dynamic.InvokeMemberBinder binder)
  RVA=-1  // not resolved  token=0x60002E1  System.Dynamic.DynamicMetaObject FallbackGetMember(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject errorSuggestion)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000059
FIELDS:
  public    static readonly Newtonsoft.Json.Utilities.DynamicProxyMetaObject.<>c<T><>9  // static @ 0x0
  public    static  System.Func<System.Dynamic.DynamicMetaObject,System.Linq.Expressions.Expression><>9__18_0  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60002E2  System.Void .cctor()
  RVA=-1  // not resolved  token=0x60002E3  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60002E4  System.Linq.Expressions.Expression <GetArgs>b__18_0(System.Dynamic.DynamicMetaObject arg)
END_CLASS

CLASS: <>c__DisplayClass10_0
TYPE:  sealed class
TOKEN: 0x200005A
FIELDS:
  public            System.Dynamic.BinaryOperationBinderbinder  // 0x0
  public            Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T><>4__this  // 0x0
  public            System.Dynamic.DynamicMetaObjectarg  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60002E5  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60002E6  System.Dynamic.DynamicMetaObject <BindBinaryOperation>b__0(System.Dynamic.DynamicMetaObject e)
END_CLASS

CLASS: <>c__DisplayClass11_0
TYPE:  sealed class
TOKEN: 0x200005B
FIELDS:
  public            System.Dynamic.UnaryOperationBinderbinder  // 0x0
  public            Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T><>4__this  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60002E7  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60002E8  System.Dynamic.DynamicMetaObject <BindUnaryOperation>b__0(System.Dynamic.DynamicMetaObject e)
END_CLASS

CLASS: <>c__DisplayClass12_0
TYPE:  sealed class
TOKEN: 0x200005C
FIELDS:
  public            System.Dynamic.GetIndexBinder   binder  // 0x0
  public            Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T><>4__this  // 0x0
  public            System.Dynamic.DynamicMetaObject[]indexes  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60002E9  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60002EA  System.Dynamic.DynamicMetaObject <BindGetIndex>b__0(System.Dynamic.DynamicMetaObject e)
END_CLASS

CLASS: <>c__DisplayClass13_0
TYPE:  sealed class
TOKEN: 0x200005D
FIELDS:
  public            System.Dynamic.SetIndexBinder   binder  // 0x0
  public            Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T><>4__this  // 0x0
  public            System.Dynamic.DynamicMetaObject[]indexes  // 0x0
  public            System.Dynamic.DynamicMetaObjectvalue  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60002EB  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60002EC  System.Dynamic.DynamicMetaObject <BindSetIndex>b__0(System.Dynamic.DynamicMetaObject e)
END_CLASS

CLASS: <>c__DisplayClass14_0
TYPE:  sealed class
TOKEN: 0x200005E
FIELDS:
  public            System.Dynamic.DeleteIndexBinderbinder  // 0x0
  public            Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T><>4__this  // 0x0
  public            System.Dynamic.DynamicMetaObject[]indexes  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60002ED  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60002EE  System.Dynamic.DynamicMetaObject <BindDeleteIndex>b__0(System.Dynamic.DynamicMetaObject e)
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x200005F
FIELDS:
  public            System.Dynamic.GetMemberBinder  binder  // 0x0
  public            Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T><>4__this  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60002EF  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60002F0  System.Dynamic.DynamicMetaObject <BindGetMember>b__0(System.Dynamic.DynamicMetaObject e)
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x2000060
FIELDS:
  public            System.Dynamic.SetMemberBinder  binder  // 0x0
  public            Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T><>4__this  // 0x0
  public            System.Dynamic.DynamicMetaObjectvalue  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60002F1  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60002F2  System.Dynamic.DynamicMetaObject <BindSetMember>b__0(System.Dynamic.DynamicMetaObject e)
END_CLASS

CLASS: <>c__DisplayClass5_0
TYPE:  sealed class
TOKEN: 0x2000061
FIELDS:
  public            System.Dynamic.DeleteMemberBinderbinder  // 0x0
  public            Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T><>4__this  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60002F3  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60002F4  System.Dynamic.DynamicMetaObject <BindDeleteMember>b__0(System.Dynamic.DynamicMetaObject e)
END_CLASS

CLASS: <>c__DisplayClass6_0
TYPE:  sealed class
TOKEN: 0x2000062
FIELDS:
  public            System.Dynamic.ConvertBinder    binder  // 0x0
  public            Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T><>4__this  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60002F5  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60002F6  System.Dynamic.DynamicMetaObject <BindConvert>b__0(System.Dynamic.DynamicMetaObject e)
END_CLASS

CLASS: <>c__DisplayClass7_0
TYPE:  sealed class
TOKEN: 0x2000063
FIELDS:
  public            System.Dynamic.InvokeMemberBinderbinder  // 0x0
  public            Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T><>4__this  // 0x0
  public            System.Dynamic.DynamicMetaObject[]args  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60002F7  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60002F8  System.Dynamic.DynamicMetaObject <BindInvokeMember>b__0(System.Dynamic.DynamicMetaObject e)
  RVA=-1  // not resolved  token=0x60002F9  System.Dynamic.DynamicMetaObject <BindInvokeMember>b__1(System.Dynamic.DynamicMetaObject e)
END_CLASS

CLASS: <>c__DisplayClass8_0
TYPE:  sealed class
TOKEN: 0x2000064
FIELDS:
  public            System.Dynamic.CreateInstanceBinderbinder  // 0x0
  public            Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T><>4__this  // 0x0
  public            System.Dynamic.DynamicMetaObject[]args  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60002FA  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60002FB  System.Dynamic.DynamicMetaObject <BindCreateInstance>b__0(System.Dynamic.DynamicMetaObject e)
END_CLASS

CLASS: <>c__DisplayClass9_0
TYPE:  sealed class
TOKEN: 0x2000065
FIELDS:
  public            System.Dynamic.InvokeBinder     binder  // 0x0
  public            Newtonsoft.Json.Utilities.DynamicProxyMetaObject<T><>4__this  // 0x0
  public            System.Dynamic.DynamicMetaObject[]args  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60002FC  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60002FD  System.Dynamic.DynamicMetaObject <BindInvoke>b__0(System.Dynamic.DynamicMetaObject e)
END_CLASS

CLASS: BinderWrapper
TYPE:  static class
TOKEN: 0x2000067
SIZE:  0x10
FIELDS:
  private   static  System.Object                   _getCSharpArgumentInfoArray  // static @ 0x0
  private   static  System.Object                   _setCSharpArgumentInfoArray  // static @ 0x8
  private   static  Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object>_getMemberCall  // static @ 0x10
  private   static  Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object>_setMemberCall  // static @ 0x18
  private   static  System.Boolean                  _init  // static @ 0x20
METHODS:
  RVA=0x0A8AFA20  token=0x60002FF  System.Void Init()
  RVA=0x0A8AF684  token=0x6000300  System.Object CreateSharpArgumentInfoArray(System.Int32[] values)
  RVA=0x0A8AF2B8  token=0x6000301  System.Void CreateMemberCalls()
  RVA=0x0A8AF8C0  token=0x6000302  System.Runtime.CompilerServices.CallSiteBinder GetMember(System.String name, System.Type context)
  RVA=0x0A8AFC1C  token=0x6000303  System.Runtime.CompilerServices.CallSiteBinder SetMember(System.String name, System.Type context)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200006D
SIZE:  0x10
FIELDS:
  public    static readonly Newtonsoft.Json.Utilities.EnumUtils.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Runtime.Serialization.EnumMemberAttribute,System.String><>9__3_0  // static @ 0x8
METHODS:
  RVA=0x04D4B250  token=0x6000315  System.Void .cctor()
  RVA=0x041E1670  token=0x6000316  System.Void .ctor()
  RVA=0x06C08920  token=0x6000317  System.String <InitializeValuesAndNames>b__3_0(System.Runtime.Serialization.EnumMemberAttribute a)
END_CLASS

CLASS: <>c__55`2
TYPE:  sealed class
TOKEN: 0x2000070
FIELDS:
  public    static readonly Newtonsoft.Json.Utilities.FSharpUtils.<>c__55<TKey,TValue><>9  // static @ 0x0
  public    static  System.Func<System.Collections.Generic.KeyValuePair<TKey,TValue>,System.Tuple<TKey,TValue>><>9__55_1  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000336  System.Void .cctor()
  RVA=-1  // not resolved  token=0x6000337  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000338  System.Tuple<TKey,TValue> <BuildMapCreator>b__55_1(System.Collections.Generic.KeyValuePair<TKey,TValue> kv)
END_CLASS

CLASS: <>c__DisplayClass52_0
TYPE:  sealed class
TOKEN: 0x2000071
SIZE:  0x20
FIELDS:
  public            Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object>call  // 0x10
  public            Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object>invoke  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000339  System.Void .ctor()
  RVA=0x0A8BAC5C  token=0x600033A  System.Object <CreateFSharpFuncCall>b__0(System.Object target, System.Object[] args)
END_CLASS

CLASS: <>c__DisplayClass55_0`2
TYPE:  sealed class
TOKEN: 0x2000072
FIELDS:
  public            Newtonsoft.Json.Serialization.ObjectConstructor<System.Object>ctorDelegate  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600033B  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600033C  System.Object <BuildMapCreator>b__0(System.Object[] args)
END_CLASS

CLASS: ImmutableCollectionTypeInfo
TYPE:  class
TOKEN: 0x2000074
SIZE:  0x28
FIELDS:
  private           System.String                   <ContractTypeName>k__BackingField  // 0x10
  private           System.String                   <CreatedTypeName>k__BackingField  // 0x18
  private           System.String                   <BuilderTypeName>k__BackingField  // 0x20
PROPERTIES:
  ContractTypeName  get=0x02B2ECC0  set=0x053908C0
  CreatedTypeName  get=0x0385B100  set=0x05392C40
  BuilderTypeName  get=0x04D862C0  set=0x05390F40
METHODS:
  RVA=0x031D14F0  token=0x6000340  System.Void .ctor(System.String contractTypeName, System.String createdTypeName, System.String builderTypeName)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000075
SIZE:  0x10
FIELDS:
  public    static readonly Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Reflection.MethodInfo,System.Boolean><>9__24_1  // static @ 0x8
  public    static  System.Func<System.Reflection.MethodInfo,System.Boolean><>9__25_1  // static @ 0x10
METHODS:
  RVA=0x0A8BAD88  token=0x6000347  System.Void .cctor()
  RVA=0x041E1670  token=0x6000348  System.Void .ctor()
  RVA=0x0A8BA8D8  token=0x6000349  System.Boolean <TryBuildImmutableForArrayContract>b__24_1(System.Reflection.MethodInfo m)
  RVA=0x0A8BA94C  token=0x600034A  System.Boolean <TryBuildImmutableForDictionaryContract>b__25_1(System.Reflection.MethodInfo m)
END_CLASS

CLASS: <>c__DisplayClass24_0
TYPE:  sealed class
TOKEN: 0x2000076
SIZE:  0x18
FIELDS:
  public            System.String                   name  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600034B  System.Void .ctor()
  RVA=0x042E3CE0  token=0x600034C  System.Boolean <TryBuildImmutableForArrayContract>b__0(Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.ImmutableCollectionTypeInfo d)
END_CLASS

CLASS: <>c__DisplayClass25_0
TYPE:  sealed class
TOKEN: 0x2000077
SIZE:  0x18
FIELDS:
  public            System.String                   name  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600034D  System.Void .ctor()
  RVA=0x042E3CE0  token=0x600034E  System.Boolean <TryBuildImmutableForDictionaryContract>b__0(Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.ImmutableCollectionTypeInfo d)
END_CLASS

CLASS: <>c__DisplayClass3_0
TYPE:  sealed class
TOKEN: 0x200007C
SIZE:  0x20
FIELDS:
  public            System.Reflection.ConstructorInfoc  // 0x10
  public            System.Reflection.MethodBase    method  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000367  System.Void .ctor()
  RVA=0x04295360  token=0x6000368  System.Object <CreateParameterizedConstructor>b__0(System.Object[] a)
  RVA=0x0A8BAB5C  token=0x6000369  System.Object <CreateParameterizedConstructor>b__1(System.Object[] a)
END_CLASS

CLASS: <>c__DisplayClass4_0`1
TYPE:  sealed class
TOKEN: 0x200007D
FIELDS:
  public            System.Reflection.ConstructorInfoc  // 0x0
  public            System.Reflection.MethodBase    method  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600036A  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600036B  System.Object <CreateMethodCall>b__0(T o, System.Object[] a)
  RVA=-1  // not resolved  token=0x600036C  System.Object <CreateMethodCall>b__1(T o, System.Object[] a)
END_CLASS

CLASS: <>c__DisplayClass5_0`1
TYPE:  sealed class
TOKEN: 0x200007E
FIELDS:
  public            System.Type                     type  // 0x0
  public            System.Reflection.ConstructorInfoconstructorInfo  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600036D  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600036E  T <CreateDefaultConstructor>b__0()
  RVA=-1  // not resolved  token=0x600036F  T <CreateDefaultConstructor>b__1()
END_CLASS

CLASS: <>c__DisplayClass6_0`1
TYPE:  sealed class
TOKEN: 0x200007F
FIELDS:
  public            System.Reflection.PropertyInfo  propertyInfo  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000370  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000371  System.Object <CreateGet>b__0(T o)
END_CLASS

CLASS: <>c__DisplayClass7_0`1
TYPE:  sealed class
TOKEN: 0x2000080
FIELDS:
  public            System.Reflection.FieldInfo     fieldInfo  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000372  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000373  System.Object <CreateGet>b__0(T o)
END_CLASS

CLASS: <>c__DisplayClass8_0`1
TYPE:  sealed class
TOKEN: 0x2000081
FIELDS:
  public            System.Reflection.FieldInfo     fieldInfo  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000374  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000375  System.Void <CreateSet>b__0(T o, System.Object v)
END_CLASS

CLASS: <>c__DisplayClass9_0`1
TYPE:  sealed class
TOKEN: 0x2000082
FIELDS:
  public            System.Reflection.PropertyInfo  propertyInfo  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000376  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000377  System.Void <CreateSet>b__0(T o, System.Object v)
END_CLASS

CLASS: <>c__DisplayClass11_0
TYPE:  sealed class
TOKEN: 0x2000089
SIZE:  0x18
FIELDS:
  public            System.Func<System.Object>      ctor  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600039E  System.Void .ctor()
  RVA=0x09ADFE08  token=0x600039F  System.Object <Create>b__0(System.Object[] args)
END_CLASS

CLASS: <>c__DisplayClass11_1
TYPE:  sealed class
TOKEN: 0x200008A
SIZE:  0x18
FIELDS:
  public            Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object>call  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60003A0  System.Void .ctor()
  RVA=0x0A8BAA50  token=0x60003A1  System.Object <Create>b__1(System.Object target)
END_CLASS

CLASS: <>c__DisplayClass11_2
TYPE:  sealed class
TOKEN: 0x200008B
SIZE:  0x18
FIELDS:
  public            Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object>call  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60003A2  System.Void .ctor()
  RVA=0x0A8BAAB8  token=0x60003A3  System.Void <Create>b__2(System.Object target, System.Object arg)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200008D
SIZE:  0x10
FIELDS:
  public    static readonly Newtonsoft.Json.Utilities.ReflectionUtils.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Reflection.ConstructorInfo,System.Boolean><>9__11_0  // static @ 0x8
  public    static  System.Func<System.Reflection.MemberInfo,System.String><>9__31_0  // static @ 0x10
  public    static  System.Func<System.Reflection.ParameterInfo,System.Type><>9__39_0  // static @ 0x18
  public    static  System.Func<System.Reflection.FieldInfo,System.Boolean><>9__41_0  // static @ 0x20
METHODS:
  RVA=0x04D4C210  token=0x60003D2  System.Void .cctor()
  RVA=0x041E1670  token=0x60003D3  System.Void .ctor()
  RVA=0x02F2EF10  token=0x60003D4  System.Boolean <GetDefaultConstructor>b__11_0(System.Reflection.ConstructorInfo c)
  RVA=0x03F48CC0  token=0x60003D5  System.String <GetFieldsAndProperties>b__31_0(System.Reflection.MemberInfo m)
  RVA=0x06C08908  token=0x60003D6  System.Type <GetMemberInfoFromType>b__39_0(System.Reflection.ParameterInfo p)
  RVA=0x02F2E180  token=0x60003D7  System.Boolean <GetChildPrivateFields>b__41_0(System.Reflection.FieldInfo f)
END_CLASS

CLASS: <>c__DisplayClass31_0
TYPE:  sealed class
TOKEN: 0x200008E
SIZE:  0x18
FIELDS:
  public            System.Reflection.MemberInfo    memberInfo  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60003D8  System.Void .ctor()
  RVA=0x04B87670  token=0x60003D9  System.Boolean <GetFieldsAndProperties>b__1(System.Reflection.MemberInfo m)
END_CLASS

CLASS: <>c__DisplayClass44_0
TYPE:  sealed class
TOKEN: 0x200008F
SIZE:  0x18
FIELDS:
  public            System.Reflection.PropertyInfo  subTypeProperty  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60003DA  System.Void .ctor()
  RVA=0x048F6D60  token=0x60003DB  System.Boolean <GetChildPrivateProperties>b__0(System.Reflection.PropertyInfo p)
  RVA=0x02F2F7C0  token=0x60003DC  System.Boolean <GetChildPrivateProperties>b__1(System.Reflection.PropertyInfo p)
END_CLASS

CLASS: <>c__DisplayClass44_1
TYPE:  sealed class
TOKEN: 0x2000090
SIZE:  0x20
FIELDS:
  public            System.Type                     subTypePropertyDeclaringType  // 0x10
  public            Newtonsoft.Json.Utilities.ReflectionUtils.<>c__DisplayClass44_0CS$<>8__locals1  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60003DD  System.Void .ctor()
  RVA=0x02F2F690  token=0x60003DE  System.Boolean <GetChildPrivateProperties>b__2(System.Reflection.PropertyInfo p)
END_CLASS

CLASS: <>c__DisplayClass45_0
TYPE:  sealed class
TOKEN: 0x2000091
SIZE:  0x20
FIELDS:
  public            System.String                   method  // 0x10
  public            System.Type                     methodDeclaringType  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60003DF  System.Void .ctor()
  RVA=0x0A8BAB80  token=0x60003E0  System.Boolean <IsMethodOverridden>b__0(System.Reflection.MethodInfo info)
END_CLASS

CLASS: SeparatedCaseState
TYPE:  sealed struct
TOKEN: 0x2000096
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.Utilities.StringUtils.SeparatedCaseStateStart  // const
  public    static  Newtonsoft.Json.Utilities.StringUtils.SeparatedCaseStateLower  // const
  public    static  Newtonsoft.Json.Utilities.StringUtils.SeparatedCaseStateUpper  // const
  public    static  Newtonsoft.Json.Utilities.StringUtils.SeparatedCaseStateNewWord  // const
METHODS:
END_CLASS

CLASS: <>c__DisplayClass14_0`1
TYPE:  sealed class
TOKEN: 0x2000097
FIELDS:
  public            System.Func<TSource,System.String>valueSelector  // 0x0
  public            System.String                   testValue  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600040A  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600040B  System.Boolean <ForgivingCaseSensitiveFind>b__0(TSource s)
  RVA=-1  // not resolved  token=0x600040C  System.Boolean <ForgivingCaseSensitiveFind>b__1(TSource s)
END_CLASS

CLASS: <GetEnumerator>d__2
TYPE:  sealed class
TOKEN: 0x20000A0
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           System.Collections.Generic.KeyValuePair<System.Object,System.Object><>2__current  // 0x0
  public            Newtonsoft.Json.Serialization.DefaultContractResolver.EnumerableDictionaryWrapper<TEnumeratorKey,TEnumeratorValue><>4__this  // 0x0
  private           System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TEnumeratorKey,TEnumeratorValue>><>7__wrap1  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Object,System.Object>>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000469  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x600046A  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x600046B  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x600046C  System.Void <>m__Finally1()
  RVA=-1  // not resolved  token=0x600046E  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: EnumerableDictionaryWrapper`2
TYPE:  class
TOKEN: 0x200009F
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TEnumeratorKey,TEnumeratorValue>>_e  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000466  System.Void .ctor(System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<TEnumeratorKey,TEnumeratorValue>> e)
  RVA=-1  // not resolved  token=0x6000467  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Object,System.Object>> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000468  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000A1
SIZE:  0x10
FIELDS:
  public    static readonly Newtonsoft.Json.Serialization.DefaultContractResolver.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Reflection.MemberInfo,System.Boolean><>9__40_0  // static @ 0x8
  public    static  System.Func<System.Reflection.MemberInfo,System.Boolean><>9__40_1  // static @ 0x10
  public    static  System.Func<System.Type,System.Collections.Generic.IEnumerable<System.Reflection.MemberInfo>><>9__44_0  // static @ 0x18
  public    static  System.Func<System.Reflection.MemberInfo,System.Boolean><>9__44_1  // static @ 0x20
  public    static  System.Func<System.Reflection.ConstructorInfo,System.Boolean><>9__47_0  // static @ 0x28
  public    static  System.Func<Newtonsoft.Json.Serialization.JsonProperty,System.Int32><>9__75_0  // static @ 0x30
METHODS:
  RVA=0x04D42010  token=0x6000470  System.Void .cctor()
  RVA=0x041E1670  token=0x6000471  System.Void .ctor()
  RVA=0x02F2EF40  token=0x6000472  System.Boolean <GetSerializableMembers>b__40_0(System.Reflection.MemberInfo m)
  RVA=0x0A8C1F0C  token=0x6000473  System.Boolean <GetSerializableMembers>b__40_1(System.Reflection.MemberInfo m)
  RVA=0x035D0740  token=0x6000474  System.Collections.Generic.IEnumerable<System.Reflection.MemberInfo> <GetExtensionDataMemberForType>b__44_0(System.Type baseType)
  RVA=0x02F2F8A0  token=0x6000475  System.Boolean <GetExtensionDataMemberForType>b__44_1(System.Reflection.MemberInfo m)
  RVA=0x02F2E1D0  token=0x6000476  System.Boolean <GetAttributeConstructor>b__47_0(System.Reflection.ConstructorInfo c)
  RVA=0x03FF4820  token=0x6000477  System.Int32 <CreateProperties>b__75_0(Newtonsoft.Json.Serialization.JsonProperty p)
END_CLASS

CLASS: <>c__DisplayClass42_0
TYPE:  sealed class
TOKEN: 0x20000A2
SIZE:  0x18
FIELDS:
  public            Newtonsoft.Json.Serialization.NamingStrategynamingStrategy  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000478  System.Void .ctor()
  RVA=0x0A8C1F88  token=0x6000479  System.String <CreateObjectContract>b__0(System.String s)
END_CLASS

CLASS: <>c__DisplayClass45_0
TYPE:  sealed class
TOKEN: 0x20000A3
SIZE:  0x20
FIELDS:
  public            System.Func<System.Object,System.Object>getExtensionDataDictionary  // 0x10
  public            System.Reflection.MemberInfo    member  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600047A  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass45_1
TYPE:  sealed class
TOKEN: 0x20000A4
SIZE:  0x30
FIELDS:
  public            System.Action<System.Object,System.Object>setExtensionDataDictionary  // 0x10
  public            System.Func<System.Object>      createExtensionDataDictionary  // 0x18
  public            Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object>setExtensionDataDictionaryValue  // 0x20
  public            Newtonsoft.Json.Serialization.DefaultContractResolver.<>c__DisplayClass45_0CS$<>8__locals1  // 0x28
METHODS:
  RVA=0x041E1670  token=0x600047B  System.Void .ctor()
  RVA=0x0A8C1FA8  token=0x600047C  System.Void <SetExtensionDataDelegates>b__0(System.Object o, System.String key, System.Object value)
END_CLASS

CLASS: <>c__DisplayClass45_2
TYPE:  sealed class
TOKEN: 0x20000A5
SIZE:  0x20
FIELDS:
  public            Newtonsoft.Json.Serialization.ObjectConstructor<System.Object>createEnumerableWrapper  // 0x10
  public            Newtonsoft.Json.Serialization.DefaultContractResolver.<>c__DisplayClass45_0CS$<>8__locals2  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600047D  System.Void .ctor()
  RVA=0x0A8C2184  token=0x600047E  System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Object,System.Object>> <SetExtensionDataDelegates>b__1(System.Object o)
END_CLASS

CLASS: <>c__DisplayClass62_0
TYPE:  sealed class
TOKEN: 0x20000A6
SIZE:  0x18
FIELDS:
  public            Newtonsoft.Json.Serialization.NamingStrategynamingStrategy  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600047F  System.Void .ctor()
  RVA=0x0A8C1F88  token=0x6000480  System.String <CreateDictionaryContract>b__0(System.String s)
END_CLASS

CLASS: <>c__DisplayClass67_0
TYPE:  sealed class
TOKEN: 0x20000A7
SIZE:  0x18
FIELDS:
  public            Newtonsoft.Json.Serialization.NamingStrategynamingStrategy  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000481  System.Void .ctor()
  RVA=0x0A8C1F88  token=0x6000482  System.String <CreateDynamicContract>b__0(System.String s)
END_CLASS

CLASS: <>c__DisplayClass80_0
TYPE:  sealed class
TOKEN: 0x20000A8
SIZE:  0x18
FIELDS:
  public            Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object>shouldSerializeCall  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000483  System.Void .ctor()
  RVA=0x0A8C2350  token=0x6000484  System.Boolean <CreateShouldSerializeTest>b__0(System.Object o)
END_CLASS

CLASS: <>c__DisplayClass81_0
TYPE:  sealed class
TOKEN: 0x20000A9
SIZE:  0x18
FIELDS:
  public            System.Func<System.Object,System.Object>specifiedPropertyGet  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000485  System.Void .ctor()
  RVA=0x0A8C23D4  token=0x6000486  System.Boolean <SetIsSpecifiedActions>b__0(System.Object o)
END_CLASS

CLASS: <>c__DisplayClass57_0
TYPE:  sealed class
TOKEN: 0x20000BD
SIZE:  0x18
FIELDS:
  public            System.Reflection.MethodInfo    callbackMethodInfo  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60004E5  System.Void .ctor()
  RVA=0x03F4D850  token=0x60004E6  System.Void <CreateSerializationCallback>b__0(System.Object o, System.Runtime.Serialization.StreamingContext context)
END_CLASS

CLASS: <>c__DisplayClass58_0
TYPE:  sealed class
TOKEN: 0x20000BE
SIZE:  0x18
FIELDS:
  public            System.Reflection.MethodInfo    callbackMethodInfo  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60004E7  System.Void .ctor()
  RVA=0x0A8C225C  token=0x60004E8  System.Void <CreateSerializationErrorCallback>b__0(System.Object o, System.Runtime.Serialization.StreamingContext context, Newtonsoft.Json.Serialization.ErrorContext econtext)
END_CLASS

CLASS: ReferenceEqualsEqualityComparer
TYPE:  class
TOKEN: 0x20000C9
SIZE:  0x10
IMPLEMENTS: System.Collections.Generic.IEqualityComparer`1
FIELDS:
METHODS:
  RVA=0x0829C278  token=0x6000570  System.Boolean System.Collections.Generic.IEqualityComparer<System.Object>.Equals(System.Object x, System.Object y)
  RVA=0x04DA3AC0  token=0x6000571  System.Int32 System.Collections.Generic.IEqualityComparer<System.Object>.GetHashCode(System.Object obj)
  RVA=0x041E1670  token=0x6000572  System.Void .ctor()
END_CLASS

CLASS: PropertyPresence
TYPE:  sealed struct
TOKEN: 0x20000CB
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresenceNone  // const
  public    static  Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresenceNull  // const
  public    static  Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresenceValue  // const
METHODS:
END_CLASS

CLASS: CreatorPropertyContext
TYPE:  class
TOKEN: 0x20000CC
SIZE:  0x40
FIELDS:
  public    readonly System.String                   Name  // 0x10
  public            Newtonsoft.Json.Serialization.JsonPropertyProperty  // 0x18
  public            Newtonsoft.Json.Serialization.JsonPropertyConstructorProperty  // 0x20
  public            System.Nullable<Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence>Presence  // 0x28
  public            System.Object                   Value  // 0x30
  public            System.Boolean                  Used  // 0x38
METHODS:
  RVA=0x053908C0  token=0x60005A3  System.Void .ctor(System.String name)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000CD
SIZE:  0x10
FIELDS:
  public    static readonly Newtonsoft.Json.Serialization.JsonSerializerInternalReader.<>c<>9  // static @ 0x0
  public    static  System.Func<Newtonsoft.Json.Serialization.JsonProperty,System.String><>9__38_0  // static @ 0x8
  public    static  System.Func<Newtonsoft.Json.Serialization.JsonProperty,System.String><>9__38_2  // static @ 0x10
  public    static  System.Func<Newtonsoft.Json.Serialization.JsonProperty,Newtonsoft.Json.Serialization.JsonProperty><>9__42_0  // static @ 0x18
  public    static  System.Func<Newtonsoft.Json.Serialization.JsonProperty,Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence><>9__42_1  // static @ 0x20
METHODS:
  RVA=0x04D3F730  token=0x60005A4  System.Void .cctor()
  RVA=0x041E1670  token=0x60005A5  System.Void .ctor()
  RVA=0x0A8C1EF4  token=0x60005A6  System.String <CreateObjectUsingCreatorWithParameters>b__38_0(Newtonsoft.Json.Serialization.JsonProperty p)
  RVA=0x045B2270  token=0x60005A7  System.String <CreateObjectUsingCreatorWithParameters>b__38_2(Newtonsoft.Json.Serialization.JsonProperty p)
  RVA=0x04D88C30  token=0x60005A8  Newtonsoft.Json.Serialization.JsonProperty <PopulateObject>b__42_0(Newtonsoft.Json.Serialization.JsonProperty m)
  RVA=0x011EC580  token=0x60005A9  Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence <PopulateObject>b__42_1(Newtonsoft.Json.Serialization.JsonProperty m)
END_CLASS

CLASS: <>c__DisplayClass38_0
TYPE:  sealed class
TOKEN: 0x20000CE
SIZE:  0x18
FIELDS:
  public            Newtonsoft.Json.Serialization.JsonPropertyproperty  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60005AA  System.Void .ctor()
  RVA=0x0A8C1F68  token=0x60005AB  System.Boolean <CreateObjectUsingCreatorWithParameters>b__1(Newtonsoft.Json.Serialization.JsonSerializerInternalReader.CreatorPropertyContext p)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000D3
SIZE:  0x10
FIELDS:
  public    static readonly Newtonsoft.Json.Serialization.JsonTypeReflector.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Object,System.Type><>9__22_1  // static @ 0x8
METHODS:
  RVA=0x0A8D2AFC  token=0x600060D  System.Void .cctor()
  RVA=0x041E1670  token=0x600060E  System.Void .ctor()
  RVA=0x0A8D2A0C  token=0x600060F  System.Type <GetCreator>b__22_1(System.Object param)
END_CLASS

CLASS: <>c__DisplayClass22_0
TYPE:  sealed class
TOKEN: 0x20000D4
SIZE:  0x20
FIELDS:
  public            System.Type                     type  // 0x10
  public            System.Func<System.Object>      defaultConstructor  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000610  System.Void .ctor()
  RVA=0x049B8240  token=0x6000611  System.Object <GetCreator>b__0(System.Object[] parameters)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000E7
SIZE:  0x10
FIELDS:
  public    static readonly Newtonsoft.Json.Linq.JObject.JObjectDynamicProxy.<>c<>9  // static @ 0x0
  public    static  System.Func<Newtonsoft.Json.Linq.JProperty,System.String><>9__2_0  // static @ 0x8
METHODS:
  RVA=0x0A8D2A98  token=0x6000715  System.Void .cctor()
  RVA=0x041E1670  token=0x6000716  System.Void .ctor()
  RVA=0x0A8D2A80  token=0x6000717  System.String <GetDynamicMemberNames>b__2_0(Newtonsoft.Json.Linq.JProperty p)
END_CLASS

CLASS: JObjectDynamicProxy
TYPE:  class
TOKEN: 0x20000E6
SIZE:  0x10
EXTENDS: Newtonsoft.Json.Utilities.DynamicProxy`1
FIELDS:
METHODS:
  RVA=0x0A8C4C50  token=0x6000711  System.Boolean TryGetMember(Newtonsoft.Json.Linq.JObject instance, System.Dynamic.GetMemberBinder binder, System.Object& result)
  RVA=0x0A8C4C90  token=0x6000712  System.Boolean TrySetMember(Newtonsoft.Json.Linq.JObject instance, System.Dynamic.SetMemberBinder binder, System.Object value)
  RVA=0x0A8C4B30  token=0x6000713  System.Collections.Generic.IEnumerable<System.String> GetDynamicMemberNames(Newtonsoft.Json.Linq.JObject instance)
  RVA=0x0A8C4D48  token=0x6000714  System.Void .ctor()
END_CLASS

CLASS: <GetEnumerator>d__64
TYPE:  sealed class
TOKEN: 0x20000E8
SIZE:  0x38
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Collections.Generic.KeyValuePair<System.String,Newtonsoft.Json.Linq.JToken><>2__current  // 0x18
  public            Newtonsoft.Json.Linq.JObject    <>4__this  // 0x28
  private           System.Collections.Generic.IEnumerator<Newtonsoft.Json.Linq.JToken><>7__wrap1  // 0x30
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,Newtonsoft.Json.Linq.JToken>>.Current  get=0x04D890C0
  System.Collections.IEnumerator.Current  get=0x0A8D2904
METHODS:
  RVA=0x014F51F0  token=0x6000718  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x0A8D294C  token=0x6000719  System.Void System.IDisposable.Dispose()
  RVA=0x0A8D259C  token=0x600071A  System.Boolean MoveNext()
  RVA=0x0A8D29C4  token=0x600071B  System.Void <>m__Finally1()
  RVA=0x0A8D28B8  token=0x600071D  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <GetEnumerator>d__1
TYPE:  sealed class
TOKEN: 0x20000EB
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           Newtonsoft.Json.Linq.JToken     <>2__current  // 0x18
  public            Newtonsoft.Json.Linq.JProperty.JPropertyList<>4__this  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<Newtonsoft.Json.Linq.JToken>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000741  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000742  System.Void System.IDisposable.Dispose()
  RVA=0x0A8D24F8  token=0x6000743  System.Boolean MoveNext()
  RVA=0x0A8D2550  token=0x6000745  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: JPropertyList
TYPE:  class
TOKEN: 0x20000EA
SIZE:  0x18
IMPLEMENTS: System.Collections.Generic.IList`1 System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           Newtonsoft.Json.Linq.JToken     _token  // 0x10
PROPERTIES:
  Count  get=0x040C1F50
  IsReadOnly  get=0x012081B0
  Item  get=0x04B48FB0  set=0x04D83150
METHODS:
  RVA=0x0A8C61BC  token=0x6000732  System.Collections.Generic.IEnumerator<Newtonsoft.Json.Linq.JToken> GetEnumerator()
  RVA=0x0A8C6268  token=0x6000733  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=0x053908C0  token=0x6000734  System.Void Add(Newtonsoft.Json.Linq.JToken item)
  RVA=0x040E20A0  token=0x6000735  System.Void Clear()
  RVA=0x0A8C614C  token=0x6000736  System.Boolean Contains(Newtonsoft.Json.Linq.JToken item)
  RVA=0x0A8C6154  token=0x6000737  System.Void CopyTo(Newtonsoft.Json.Linq.JToken[] array, System.Int32 arrayIndex)
  RVA=0x0A8C6240  token=0x6000738  System.Boolean Remove(Newtonsoft.Json.Linq.JToken item)
  RVA=0x0A8C6218  token=0x600073B  System.Int32 IndexOf(Newtonsoft.Json.Linq.JToken item)
  RVA=0x0424C7C0  token=0x600073C  System.Void Insert(System.Int32 index, Newtonsoft.Json.Linq.JToken item)
  RVA=0x0A8C6228  token=0x600073D  System.Void RemoveAt(System.Int32 index)
  RVA=0x041E1670  token=0x6000740  System.Void .ctor()
END_CLASS

CLASS: LineInfoAnnotation
TYPE:  class
TOKEN: 0x20000F2
SIZE:  0x18
FIELDS:
  private   readonly System.Int32                    LineNumber  // 0x10
  private   readonly System.Int32                    LinePosition  // 0x14
METHODS:
  RVA=0x04D88290  token=0x60007C5  System.Void .ctor(System.Int32 lineNumber, System.Int32 linePosition)
END_CLASS

CLASS: JValueDynamicProxy
TYPE:  class
TOKEN: 0x20000F7
SIZE:  0x10
EXTENDS: Newtonsoft.Json.Utilities.DynamicProxy`1
FIELDS:
METHODS:
  RVA=0x0A8CD574  token=0x6000831  System.Boolean TryConvert(Newtonsoft.Json.Linq.JValue instance, System.Dynamic.ConvertBinder binder, System.Object& result)
  RVA=0x0A8CD2C8  token=0x6000832  System.Boolean TryBinaryOperation(Newtonsoft.Json.Linq.JValue instance, System.Dynamic.BinaryOperationBinder binder, System.Object arg, System.Object& result)
  RVA=0x0A8CD6F0  token=0x6000833  System.Void .ctor()
END_CLASS

CLASS: Union
TYPE:  class
TOKEN: 0x2000100
SIZE:  0x20
FIELDS:
  public    readonly Newtonsoft.Json.Utilities.FSharpFunctionTagReader  // 0x10
  public    readonly System.Collections.Generic.List<Newtonsoft.Json.Converters.DiscriminatedUnionConverter.UnionCase>Cases  // 0x18
METHODS:
  RVA=0x032AF420  token=0x6000858  System.Void .ctor(Newtonsoft.Json.Utilities.FSharpFunction tagReader, System.Collections.Generic.List<Newtonsoft.Json.Converters.DiscriminatedUnionConverter.UnionCase> cases)
END_CLASS

CLASS: UnionCase
TYPE:  class
TOKEN: 0x2000101
SIZE:  0x38
FIELDS:
  public    readonly System.Int32                    Tag  // 0x10
  public    readonly System.String                   Name  // 0x18
  public    readonly System.Reflection.PropertyInfo[]Fields  // 0x20
  public    readonly Newtonsoft.Json.Utilities.FSharpFunctionFieldReader  // 0x28
  public    readonly Newtonsoft.Json.Utilities.FSharpFunctionConstructor  // 0x30
METHODS:
  RVA=0x0A8D912C  token=0x6000859  System.Void .ctor(System.Int32 tag, System.String name, System.Reflection.PropertyInfo[] fields, Newtonsoft.Json.Utilities.FSharpFunction fieldReader, Newtonsoft.Json.Utilities.FSharpFunction constructor)
END_CLASS

CLASS: <>c__DisplayClass8_0
TYPE:  sealed class
TOKEN: 0x2000102
SIZE:  0x18
FIELDS:
  public            System.Int32                    tag  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600085A  System.Void .ctor()
  RVA=0x06A829FC  token=0x600085B  System.Boolean <WriteJson>b__0(Newtonsoft.Json.Converters.DiscriminatedUnionConverter.UnionCase c)
END_CLASS

CLASS: <>c__DisplayClass9_0
TYPE:  sealed class
TOKEN: 0x2000103
SIZE:  0x20
FIELDS:
  public            System.String                   caseName  // 0x10
  public            System.Func<Newtonsoft.Json.Converters.DiscriminatedUnionConverter.UnionCase,System.Boolean><>9__0  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600085C  System.Void .ctor()
  RVA=0x0A8D9104  token=0x600085D  System.Boolean <ReadJson>b__0(Newtonsoft.Json.Converters.DiscriminatedUnionConverter.UnionCase c)
END_CLASS

CLASS: __StaticArrayInitTypeSize=10
TYPE:  sealed struct
TOKEN: 0x200012D
SIZE:  0x1A
FIELDS:
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=16
TYPE:  sealed struct
TOKEN: 0x200012E
SIZE:  0x20
FIELDS:
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=20
TYPE:  sealed struct
TOKEN: 0x200012F
SIZE:  0x24
FIELDS:
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=24
TYPE:  sealed struct
TOKEN: 0x2000130
SIZE:  0x28
FIELDS:
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=28
TYPE:  sealed struct
TOKEN: 0x2000131
SIZE:  0x2C
FIELDS:
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=36
TYPE:  sealed struct
TOKEN: 0x2000132
SIZE:  0x34
FIELDS:
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=40
TYPE:  sealed struct
TOKEN: 0x2000133
SIZE:  0x38
FIELDS:
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=44
TYPE:  sealed struct
TOKEN: 0x2000134
SIZE:  0x3C
FIELDS:
METHODS:
END_CLASS

CLASS: __StaticArrayInitTypeSize=52
TYPE:  sealed struct
TOKEN: 0x2000135
SIZE:  0x44
FIELDS:
METHODS:
END_CLASS

CLASS: <PrivateImplementationDetails>
TYPE:  sealed class
TOKEN: 0x200012C
SIZE:  0x10
FIELDS:
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=440698228BF899CAEAB9A53E5E6C7099E846C44F56432050D234DDF03AD772F139  // static @ 0x0
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=1019AE20A57B073E3E8DD45C6F6A4E9AB1076EA3EBFFF28E4AEB58B411472CF994  // static @ 0x2c
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=40223D6CA32241C349E421A0164F2341E20CC5B65D5A04AA021CFF71D623895570  // static @ 0x36
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=2033350F5DA385CE1B8749AEC68BA060CD54EE981968522B5EDF62178537A1FEEE  // static @ 0x5e
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=4038809B9974198671140931F729415F3FD75DF68A6398E3486AE3B58554329A63  // static @ 0x72
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=28499E4F5C84E20C7347E10100E0EC90C1945EA21C7C80809E4F7F474179B39DF6  // static @ 0x9a
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=164EDE3546F1189E450DF4D4A2739BE90BEB3B1708B3B9F406B02E0773A92A10FF  // static @ 0xb6
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=525857EE4CE98BFABBD62B385C1098507DD0052FF3951043AAD6A1DABD495F18AA  // static @ 0xc6
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=285ADB7CA81690556AB2A3201A849839FA3562604BB469382C7D6D78AB426283E2  // static @ 0xfa
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=245DDF815AC046E7D4603FA586D1BDE42118AD4FE9875D64F716BC7D2740EE52C9  // static @ 0x116
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=16605A3F93AE7A97E00C156F977E942027EA532E263A5B440A4219984F803FDD04  // static @ 0x12e
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=367367A65185E4F747AA29364AB199D01646A010A62129A6BA2E35E929D7294D62  // static @ 0x13e
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=407439A4C9E30AC42BCC55AD1A2B617E29E7129B6DDAC79C886944B17819262CC1  // static @ 0x162
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=40990F3F1286CC3928725497B2745CFF7BC7C9803B4EB8271611540BA6BF6654B5  // static @ 0x18a
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=16A8636D08B42D058EFC34703DD37B6468FCE56138DF242B862C3F1CA138CB3B89  // static @ 0x1b2
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=20B1D1BCD1D06B4A563944BE3C67D51F63DF23702E5BE760D7897C6AD1F51C6122  // static @ 0x1c2
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=20CAA07D7573596B3356BD202533F0EAFDD05309981F270193A99E300D57587326  // static @ 0x1d6
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=40D4B3B8EBA0589FC38724A0D318B46104B07BC528744109ED69ED71604B7EEC1A  // static @ 0x1ea
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=40F6EDC1733B068F457C63E03BB041B9AB6BFAD5CD7673D3E0841968D3FBCB12C7  // static @ 0x212
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=52FADB218011E7702BB9575D0C32A685DA10B5C72EB809BD9A955DB1C76E4D8315  // static @ 0x23a
  private   static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=40FCA56C548368F7065472C8C8EE4D63921B4F16BB51181EC202A0C252D5209E6A  // static @ 0x26e
METHODS:
END_CLASS

CLASS: Microsoft.CodeAnalysis.EmbeddedAttribute
TYPE:  sealed class
TOKEN: 0x2000002
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000001  System.Void .ctor()
END_CLASS

CLASS: Newtonsoft.Json.ConstructorHandling
TYPE:  sealed struct
TOKEN: 0x2000008
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.ConstructorHandlingDefault  // const
  public    static  Newtonsoft.Json.ConstructorHandlingAllowNonPublicDefaultConstructor  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.DateFormatHandling
TYPE:  sealed struct
TOKEN: 0x2000009
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.DateFormatHandlingIsoDateFormat  // const
  public    static  Newtonsoft.Json.DateFormatHandlingMicrosoftDateFormat  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.DateParseHandling
TYPE:  sealed struct
TOKEN: 0x200000A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.DateParseHandlingNone  // const
  public    static  Newtonsoft.Json.DateParseHandlingDateTime  // const
  public    static  Newtonsoft.Json.DateParseHandlingDateTimeOffset  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.DateTimeZoneHandling
TYPE:  sealed struct
TOKEN: 0x200000B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.DateTimeZoneHandlingLocal  // const
  public    static  Newtonsoft.Json.DateTimeZoneHandlingUtc  // const
  public    static  Newtonsoft.Json.DateTimeZoneHandlingUnspecified  // const
  public    static  Newtonsoft.Json.DateTimeZoneHandlingRoundtripKind  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.DefaultJsonNameTable
TYPE:  class
TOKEN: 0x200000C
SIZE:  0x28
EXTENDS: Newtonsoft.Json.JsonNameTable
FIELDS:
  private   static readonly System.Int32                    HashCodeRandomizer  // static @ 0x0
  private           System.Int32                    _count  // 0x10
  private           Newtonsoft.Json.DefaultJsonNameTable.Entry[]_entries  // 0x18
  private           System.Int32                    _mask  // 0x20
METHODS:
  RVA=0x04D75A00  token=0x6000008  System.Void .cctor()
  RVA=0x0464D570  token=0x6000009  System.Void .ctor()
  RVA=0x02F06660  token=0x600000A  System.String Get(System.Char[] key, System.Int32 start, System.Int32 length)
  RVA=0x032B3E00  token=0x600000B  System.String Add(System.String key)
  RVA=0x032B2C60  token=0x600000C  System.String AddEntry(System.String str, System.Int32 hashCode)
  RVA=0x032B3210  token=0x600000D  System.Void Grow()
  RVA=0x02F065E0  token=0x600000E  System.Boolean TextEquals(System.String str1, System.Char[] str2, System.Int32 str2Start, System.Int32 str2Length)
END_CLASS

CLASS: Newtonsoft.Json.DefaultValueHandling
TYPE:  sealed struct
TOKEN: 0x200000E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.DefaultValueHandlingInclude  // const
  public    static  Newtonsoft.Json.DefaultValueHandlingIgnore  // const
  public    static  Newtonsoft.Json.DefaultValueHandlingPopulate  // const
  public    static  Newtonsoft.Json.DefaultValueHandlingIgnoreAndPopulate  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.FloatFormatHandling
TYPE:  sealed struct
TOKEN: 0x200000F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.FloatFormatHandlingString  // const
  public    static  Newtonsoft.Json.FloatFormatHandlingSymbol  // const
  public    static  Newtonsoft.Json.FloatFormatHandlingDefaultValue  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.FloatParseHandling
TYPE:  sealed struct
TOKEN: 0x2000010
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.FloatParseHandlingDouble  // const
  public    static  Newtonsoft.Json.FloatParseHandlingDecimal  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.Formatting
TYPE:  sealed struct
TOKEN: 0x2000011
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.Formatting      None  // const
  public    static  Newtonsoft.Json.Formatting      Indented  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.IArrayPool`1
TYPE:  interface
TOKEN: 0x2000012
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000010  T[] Rent(System.Int32 minimumLength)
  RVA=-1  // abstract  token=0x6000011  System.Void Return(T[] array)
END_CLASS

CLASS: Newtonsoft.Json.IJsonLineInfo
TYPE:  interface
TOKEN: 0x2000013
FIELDS:
PROPERTIES:
  LineNumber  get=-1  // abstract
  LinePosition  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000012  System.Boolean HasLineInfo()
END_CLASS

CLASS: Newtonsoft.Json.JsonArrayAttribute
TYPE:  sealed class
TOKEN: 0x2000014
SIZE:  0x50
EXTENDS: Newtonsoft.Json.JsonContainerAttribute
FIELDS:
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.JsonConstructorAttribute
TYPE:  sealed class
TOKEN: 0x2000015
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000015  System.Void .ctor()
END_CLASS

CLASS: Newtonsoft.Json.JsonContainerAttribute
TYPE:  abstract class
TOKEN: 0x2000016
SIZE:  0x50
EXTENDS: System.Attribute
FIELDS:
  private           System.Type                     <ItemConverterType>k__BackingField  // 0x10
  private           System.Object[]                 <ItemConverterParameters>k__BackingField  // 0x18
  private           Newtonsoft.Json.Serialization.NamingStrategy<NamingStrategyInstance>k__BackingField  // 0x20
  private           System.Nullable<System.Boolean> _isReference  // 0x28
  private           System.Nullable<System.Boolean> _itemIsReference  // 0x2a
  private           System.Nullable<Newtonsoft.Json.ReferenceLoopHandling>_itemReferenceLoopHandling  // 0x2c
  private           System.Nullable<Newtonsoft.Json.TypeNameHandling>_itemTypeNameHandling  // 0x34
  private           System.Type                     _namingStrategyType  // 0x40
  private           System.Object[]                 _namingStrategyParameters  // 0x48
PROPERTIES:
  ItemConverterType  get=0x02B2ECC0
  ItemConverterParameters  get=0x0385B100
  NamingStrategyType  get=0x04D85A60
  NamingStrategyParameters  get=0x04D86200
  NamingStrategyInstance  get=0x04D862C0  set=0x05390F40
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.JsonConvert
TYPE:  static class
TOKEN: 0x2000017
SIZE:  0x10
FIELDS:
  private   static  System.Func<Newtonsoft.Json.JsonSerializerSettings><DefaultSettings>k__BackingField  // static @ 0x0
  public    static readonly System.String                   True  // static @ 0x8
  public    static readonly System.String                   False  // static @ 0x10
  public    static readonly System.String                   Null  // static @ 0x18
  public    static readonly System.String                   Undefined  // static @ 0x20
  public    static readonly System.String                   PositiveInfinity  // static @ 0x28
  public    static readonly System.String                   NegativeInfinity  // static @ 0x30
  public    static readonly System.String                   NaN  // static @ 0x38
PROPERTIES:
  DefaultSettings  get=0x0A8A6DEC
METHODS:
  RVA=0x03778C50  token=0x600001D  System.String ToString(System.Boolean value)
  RVA=0x0A8A6A78  token=0x600001E  System.String ToString(System.Char value)
  RVA=0x0A8A6C60  token=0x600001F  System.String ToString(System.Single value, Newtonsoft.Json.FloatFormatHandling floatFormatHandling, System.Char quoteChar, System.Boolean nullable)
  RVA=0x0A8A6658  token=0x6000020  System.String EnsureFloatFormat(System.Double value, System.String text, Newtonsoft.Json.FloatFormatHandling floatFormatHandling, System.Char quoteChar, System.Boolean nullable)
  RVA=0x0A8A6D10  token=0x6000021  System.String ToString(System.Double value, Newtonsoft.Json.FloatFormatHandling floatFormatHandling, System.Char quoteChar, System.Boolean nullable)
  RVA=0x0A8A6580  token=0x6000022  System.String EnsureDecimalPlace(System.Double value, System.String text)
  RVA=0x0A8A65FC  token=0x6000023  System.String EnsureDecimalPlace(System.String text)
  RVA=0x0A8A69E0  token=0x6000024  System.String ToString(System.Decimal value)
  RVA=0x0A8A6C10  token=0x6000025  System.String ToString(System.String value)
  RVA=0x0A8A6BBC  token=0x6000026  System.String ToString(System.String value, System.Char delimiter)
  RVA=0x0A8A6AE0  token=0x6000027  System.String ToString(System.String value, System.Char delimiter, Newtonsoft.Json.StringEscapeHandling stringEscapeHandling)
  RVA=0x03423A10  token=0x6000028  System.String SerializeObject(System.Object value)
  RVA=0x0A8A6938  token=0x6000029  System.String SerializeObject(System.Object value, Newtonsoft.Json.Formatting formatting)
  RVA=0x0A8A698C  token=0x600002A  System.String SerializeObject(System.Object value, Newtonsoft.Json.JsonSerializerSettings settings)
  RVA=0x03423A70  token=0x600002B  System.String SerializeObject(System.Object value, System.Type type, Newtonsoft.Json.JsonSerializerSettings settings)
  RVA=0x035A6320  token=0x600002C  System.String SerializeObject(System.Object value, Newtonsoft.Json.Formatting formatting, Newtonsoft.Json.JsonSerializerSettings settings)
  RVA=0x035A63A0  token=0x600002D  System.String SerializeObject(System.Object value, System.Type type, Newtonsoft.Json.Formatting formatting, Newtonsoft.Json.JsonSerializerSettings settings)
  RVA=0x03425340  token=0x600002E  System.String SerializeObjectInternal(System.Object value, System.Type type, Newtonsoft.Json.JsonSerializer jsonSerializer)
  RVA=0x04CC4980  token=0x600002F  System.Object DeserializeObject(System.String value)
  RVA=0x035A6000  token=0x6000030  System.Object DeserializeObject(System.String value, System.Type type)
  RVA=-1  // generic def  token=0x6000031  T DeserializeObject(System.String value)
  RVA=-1  // generic def  token=0x6000032  T DeserializeObject(System.String value, Newtonsoft.Json.JsonSerializerSettings settings)
  RVA=0x035A6700  token=0x6000033  System.Object DeserializeObject(System.String value, System.Type type, Newtonsoft.Json.JsonSerializerSettings settings)
  RVA=0x0A8A6718  token=0x6000034  System.Void PopulateObject(System.String value, System.Object target, Newtonsoft.Json.JsonSerializerSettings settings)
  RVA=0x04A282D0  token=0x6000035  System.Void .cctor()
END_CLASS

CLASS: Newtonsoft.Json.JsonConverter
TYPE:  abstract class
TOKEN: 0x2000018
SIZE:  0x10
FIELDS:
PROPERTIES:
  CanRead  get=0x02FFF600
  CanWrite  get=0x02FFF600
METHODS:
  RVA=-1  // abstract  token=0x6000036  System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer)
  RVA=-1  // abstract  token=0x6000037  System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer)
  RVA=-1  // abstract  token=0x6000038  System.Boolean CanConvert(System.Type objectType)
  RVA=0x041E1670  token=0x600003B  System.Void .ctor()
END_CLASS

CLASS: Newtonsoft.Json.JsonConverter`1
TYPE:  abstract class
TOKEN: 0x2000019
EXTENDS: Newtonsoft.Json.JsonConverter
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600003C  System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer)
  RVA=-1  // abstract  token=0x600003D  System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, T value, Newtonsoft.Json.JsonSerializer serializer)
  RVA=-1  // not resolved  token=0x600003E  System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer)
  RVA=-1  // abstract  token=0x600003F  T ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, T existingValue, System.Boolean hasExistingValue, Newtonsoft.Json.JsonSerializer serializer)
  RVA=-1  // not resolved  token=0x6000040  System.Boolean CanConvert(System.Type objectType)
  RVA=-1  // not resolved  token=0x6000041  System.Void .ctor()
END_CLASS

CLASS: Newtonsoft.Json.JsonConverterAttribute
TYPE:  sealed class
TOKEN: 0x200001A
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  private   readonly System.Type                     _converterType  // 0x10
  private   readonly System.Object[]                 <ConverterParameters>k__BackingField  // 0x18
PROPERTIES:
  ConverterType  get=0x02B2ECC0
  ConverterParameters  get=0x0385B100
METHODS:
  RVA=0x04B0E520  token=0x6000044  System.Void .ctor(System.Type converterType)
END_CLASS

CLASS: Newtonsoft.Json.JsonConverterCollection
TYPE:  class
TOKEN: 0x200001B
SIZE:  0x18
EXTENDS: System.Collections.ObjectModel.Collection`1
FIELDS:
METHODS:
  RVA=0x0539649C  token=0x6000045  System.Void .ctor()
END_CLASS

CLASS: Newtonsoft.Json.JsonDictionaryAttribute
TYPE:  sealed class
TOKEN: 0x200001C
SIZE:  0x50
EXTENDS: Newtonsoft.Json.JsonContainerAttribute
FIELDS:
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.JsonException
TYPE:  class
TOKEN: 0x200001D
SIZE:  0x90
EXTENDS: System.Exception
FIELDS:
METHODS:
  RVA=0x0A8A6E3C  token=0x6000046  System.Void .ctor()
  RVA=0x0A8A6E88  token=0x6000047  System.Void .ctor(System.String message)
  RVA=0x0A8A6EE4  token=0x6000048  System.Void .ctor(System.String message, System.Exception innerException)
  RVA=0x0A8A6F44  token=0x6000049  System.Void .ctor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
END_CLASS

CLASS: Newtonsoft.Json.JsonExtensionDataAttribute
TYPE:  class
TOKEN: 0x200001E
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  private           System.Boolean                  <WriteData>k__BackingField  // 0x10
  private           System.Boolean                  <ReadData>k__BackingField  // 0x11
PROPERTIES:
  WriteData  get=0x015EFCE0
  ReadData  get=0x04D88380
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.JsonIgnoreAttribute
TYPE:  sealed class
TOKEN: 0x200001F
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600004C  System.Void .ctor()
END_CLASS

CLASS: Newtonsoft.Json.JsonNameTable
TYPE:  abstract class
TOKEN: 0x2000020
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600004D  System.String Get(System.Char[] key, System.Int32 start, System.Int32 length)
  RVA=0x041E1670  token=0x600004E  System.Void .ctor()
END_CLASS

CLASS: Newtonsoft.Json.JsonObjectAttribute
TYPE:  sealed class
TOKEN: 0x2000021
SIZE:  0x70
EXTENDS: Newtonsoft.Json.JsonContainerAttribute
FIELDS:
  private           Newtonsoft.Json.MemberSerialization_memberSerialization  // 0x50
  private           System.Nullable<Newtonsoft.Json.MissingMemberHandling>_missingMemberHandling  // 0x54
  private           System.Nullable<Newtonsoft.Json.Required>_itemRequired  // 0x5c
  private           System.Nullable<Newtonsoft.Json.NullValueHandling>_itemNullValueHandling  // 0x64
PROPERTIES:
  MemberSerialization  get=0x04D86550
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.JsonContainerType
TYPE:  sealed struct
TOKEN: 0x2000022
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.JsonContainerTypeNone  // const
  public    static  Newtonsoft.Json.JsonContainerTypeObject  // const
  public    static  Newtonsoft.Json.JsonContainerTypeArray  // const
  public    static  Newtonsoft.Json.JsonContainerTypeConstructor  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.JsonPosition
TYPE:  sealed struct
TOKEN: 0x2000023
SIZE:  0x28
FIELDS:
  private   static readonly System.Char[]                   SpecialCharacters  // static @ 0x0
  private           Newtonsoft.Json.JsonContainerTypeType  // 0x10
  private           System.Int32                    Position  // 0x14
  private           System.String                   PropertyName  // 0x18
  private           System.Boolean                  HasIndex  // 0x20
METHODS:
  RVA=0x02FB6FF0  token=0x6000050  System.Void .ctor(Newtonsoft.Json.JsonContainerType type)
  RVA=0x0A8A72E4  token=0x6000051  System.Int32 CalculateLength()
  RVA=0x0A8A7550  token=0x6000052  System.Void WriteTo(System.Text.StringBuilder sb, System.IO.StringWriter& writer, System.Char[]& buffer)
  RVA=0x02FB7090  token=0x6000053  System.Boolean TypeHasIndex(Newtonsoft.Json.JsonContainerType type)
  RVA=0x0A8A6FB0  token=0x6000054  System.String BuildPath(System.Collections.Generic.List<Newtonsoft.Json.JsonPosition> positions, System.Nullable<Newtonsoft.Json.JsonPosition> currentPosition)
  RVA=0x0A8A7374  token=0x6000055  System.String FormatMessage(Newtonsoft.Json.IJsonLineInfo lineInfo, System.String path, System.String message)
  RVA=0x04D17BE0  token=0x6000056  System.Void .cctor()
END_CLASS

CLASS: Newtonsoft.Json.JsonPropertyAttribute
TYPE:  sealed class
TOKEN: 0x2000024
SIZE:  0x88
EXTENDS: System.Attribute
FIELDS:
  private           System.Nullable<Newtonsoft.Json.NullValueHandling>_nullValueHandling  // 0x10
  private           System.Nullable<Newtonsoft.Json.DefaultValueHandling>_defaultValueHandling  // 0x18
  private           System.Nullable<Newtonsoft.Json.ReferenceLoopHandling>_referenceLoopHandling  // 0x20
  private           System.Nullable<Newtonsoft.Json.ObjectCreationHandling>_objectCreationHandling  // 0x28
  private           System.Nullable<Newtonsoft.Json.TypeNameHandling>_typeNameHandling  // 0x30
  private           System.Nullable<System.Boolean> _isReference  // 0x38
  private           System.Nullable<System.Int32>   _order  // 0x3c
  private           System.Nullable<Newtonsoft.Json.Required>_required  // 0x44
  private           System.Nullable<System.Boolean> _itemIsReference  // 0x4c
  private           System.Nullable<Newtonsoft.Json.ReferenceLoopHandling>_itemReferenceLoopHandling  // 0x50
  private           System.Nullable<Newtonsoft.Json.TypeNameHandling>_itemTypeNameHandling  // 0x58
  private           System.Type                     <ItemConverterType>k__BackingField  // 0x60
  private           System.Object[]                 <ItemConverterParameters>k__BackingField  // 0x68
  private           System.Type                     <NamingStrategyType>k__BackingField  // 0x70
  private           System.Object[]                 <NamingStrategyParameters>k__BackingField  // 0x78
  private           System.String                   <PropertyName>k__BackingField  // 0x80
PROPERTIES:
  ItemConverterType  get=0x04D86280
  ItemConverterParameters  get=0x04D86230
  NamingStrategyType  get=0x04D862B0
  NamingStrategyParameters  get=0x04D862A0
  DefaultValueHandling  set=0x047C8250
  Order  set=0x03FC47A0
  PropertyName  get=0x04D86260  set=0x04CD9970
METHODS:
  RVA=0x041E1670  token=0x600005F  System.Void .ctor()
  RVA=0x04CD9970  token=0x6000060  System.Void .ctor(System.String propertyName)
END_CLASS

CLASS: Newtonsoft.Json.JsonReader
TYPE:  abstract class
TOKEN: 0x2000025
SIZE:  0x78
IMPLEMENTS: System.IDisposable
FIELDS:
  private           Newtonsoft.Json.JsonToken       _tokenType  // 0x10
  private           System.Object                   _value  // 0x18
  private           System.Char                     _quoteChar  // 0x20
  private           Newtonsoft.Json.JsonReader.State_currentState  // 0x24
  private           Newtonsoft.Json.JsonPosition    _currentPosition  // 0x28
  private           System.Globalization.CultureInfo_culture  // 0x40
  private           Newtonsoft.Json.DateTimeZoneHandling_dateTimeZoneHandling  // 0x48
  private           System.Nullable<System.Int32>   _maxDepth  // 0x4c
  private           System.Boolean                  _hasExceededMaxDepth  // 0x54
  private           Newtonsoft.Json.DateParseHandling_dateParseHandling  // 0x58
  private           Newtonsoft.Json.FloatParseHandling_floatParseHandling  // 0x5c
  private           System.String                   _dateFormatString  // 0x60
  private           System.Collections.Generic.List<Newtonsoft.Json.JsonPosition>_stack  // 0x68
  private           System.Boolean                  <CloseInput>k__BackingField  // 0x70
  private           System.Boolean                  <SupportMultipleContent>k__BackingField  // 0x71
PROPERTIES:
  CurrentState  get=0x011F0020
  CloseInput  get=0x04D86A50  set=0x04D86A80
  SupportMultipleContent  get=0x04D89040  set=0x04D89050
  DateTimeZoneHandling  get=0x04D863D0  set=0x0A8A9CD8
  DateParseHandling  get=0x04D86570  set=0x0A8A9C70
  FloatParseHandling  get=0x04D86560  set=0x0A8A9D40
  DateFormatString  get=0x04D86280  set=0x063CC164
  MaxDepth  get=0x04DA5BD0  set=0x02FBED20
  TokenType  get=0x02B2E2D0
  Value  get=0x0385B100
  ValueType  get=0x0A8A9C4C
  Depth  get=0x034252E0
  Path  get=0x0A8A9B58
  Culture  get=0x02FBB470  set=0x042B4AE0
METHODS:
  RVA=0x0A8A7990  token=0x6000077  Newtonsoft.Json.JsonPosition GetPosition(System.Int32 depth)
  RVA=0x035A6D00  token=0x6000078  System.Void .ctor()
  RVA=0x02FB70A0  token=0x6000079  System.Void Push(Newtonsoft.Json.JsonContainerType value)
  RVA=0x03638300  token=0x600007A  Newtonsoft.Json.JsonContainerType Pop()
  RVA=0x011F2A90  token=0x600007B  Newtonsoft.Json.JsonContainerType Peek()
  RVA=-1  // abstract  token=0x600007C  System.Boolean Read()
  RVA=0x0A8A8AE4  token=0x600007D  System.Nullable<System.Int32> ReadAsInt32()
  RVA=0x0A8A97C0  token=0x600007E  System.Nullable<System.Int32> ReadInt32String(System.String s)
  RVA=0x0342A500  token=0x600007F  System.String ReadAsString()
  RVA=0x0A8A7CBC  token=0x6000080  System.Byte[] ReadAsBytes()
  RVA=0x0A8A7BE8  token=0x6000081  System.Byte[] ReadArrayIntoByteArray()
  RVA=0x0A8A7A34  token=0x6000082  System.Boolean ReadArrayElementIntoByteArrayReportDone(System.Collections.Generic.List<System.Byte> buffer)
  RVA=0x0A8A8840  token=0x6000083  System.Nullable<System.Double> ReadAsDouble()
  RVA=0x0A8A95E0  token=0x6000084  System.Nullable<System.Double> ReadDoubleString(System.String s)
  RVA=0x0342A430  token=0x6000085  System.Nullable<System.Boolean> ReadAsBoolean()
  RVA=0x0A8A8E70  token=0x6000086  System.Nullable<System.Boolean> ReadBooleanString(System.String s)
  RVA=0x0A8A8468  token=0x6000087  System.Nullable<System.Decimal> ReadAsDecimal()
  RVA=0x0A8A93D0  token=0x6000088  System.Nullable<System.Decimal> ReadDecimalString(System.String s)
  RVA=0x0A8A8240  token=0x6000089  System.Nullable<System.DateTime> ReadAsDateTime()
  RVA=0x0A8A91E0  token=0x600008A  System.Nullable<System.DateTime> ReadDateTimeString(System.String s)
  RVA=0x0A8A8008  token=0x600008B  System.Nullable<System.DateTimeOffset> ReadAsDateTimeOffset()
  RVA=0x0A8A8FD8  token=0x600008C  System.Nullable<System.DateTimeOffset> ReadDateTimeOffsetString(System.String s)
  RVA=0x0A8A9B10  token=0x600008D  System.Void ReaderReadAndAssert()
  RVA=0x0A8A7950  token=0x600008E  Newtonsoft.Json.JsonReaderException CreateUnexpectedEndException()
  RVA=0x0A8A992C  token=0x600008F  System.Void ReadIntoWrappedTypeObject()
  RVA=0x03424C90  token=0x6000090  System.Void Skip()
  RVA=0x05390BEC  token=0x6000091  System.Void SetToken(Newtonsoft.Json.JsonToken newToken)
  RVA=0x0539086C  token=0x6000092  System.Void SetToken(Newtonsoft.Json.JsonToken newToken, System.Object value)
  RVA=0x02FB8CC0  token=0x6000093  System.Void SetToken(Newtonsoft.Json.JsonToken newToken, System.Object value, System.Boolean updateIndex)
  RVA=0x02FB8F20  token=0x6000094  System.Void SetPostValueState(System.Boolean updateIndex)
  RVA=0x02FB8F60  token=0x6000095  System.Void UpdateScopeWithFinishedValue()
  RVA=0x03637ED0  token=0x6000096  System.Void ValidateEnd(Newtonsoft.Json.JsonToken endToken)
  RVA=0x038306D0  token=0x6000097  System.Void SetStateBasedOnCurrent()
  RVA=0x03638550  token=0x6000098  System.Void SetFinished()
  RVA=0x036382C0  token=0x6000099  Newtonsoft.Json.JsonContainerType GetTypeForCloseToken(Newtonsoft.Json.JsonToken token)
  RVA=0x03F608D0  token=0x600009A  System.Void System.IDisposable.Dispose()
  RVA=0x03F60930  token=0x600009B  System.Void Dispose(System.Boolean disposing)
  RVA=0x037786B0  token=0x600009C  System.Void Close()
  RVA=0x032B61B0  token=0x600009D  System.Void ReadAndAssert()
  RVA=0x0A8A976C  token=0x600009E  System.Void ReadForTypeAndAssert(Newtonsoft.Json.Serialization.JsonContract contract, System.Boolean hasConverter)
  RVA=0x02FB4440  token=0x600009F  System.Boolean ReadForType(Newtonsoft.Json.Serialization.JsonContract contract, System.Boolean hasConverter)
  RVA=0x03424BC0  token=0x60000A0  System.Boolean ReadAndMoveToContent()
  RVA=0x03424C10  token=0x60000A1  System.Boolean MoveToContent()
  RVA=0x0342A5B0  token=0x60000A2  Newtonsoft.Json.JsonToken GetContentToken()
END_CLASS

CLASS: Newtonsoft.Json.JsonReaderException
TYPE:  class
TOKEN: 0x2000027
SIZE:  0xA0
EXTENDS: Newtonsoft.Json.JsonException
FIELDS:
  private   readonly System.Int32                    <LineNumber>k__BackingField  // 0x90
  private   readonly System.Int32                    <LinePosition>k__BackingField  // 0x94
  private   readonly System.String                   <Path>k__BackingField  // 0x98
METHODS:
  RVA=0x0A8A78C8  token=0x60000A3  System.Void .ctor()
  RVA=0x0A8A7948  token=0x60000A4  System.Void .ctor(System.String message)
  RVA=0x0A8A7928  token=0x60000A5  System.Void .ctor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
  RVA=0x0A8A78D0  token=0x60000A6  System.Void .ctor(System.String message, System.String path, System.Int32 lineNumber, System.Int32 linePosition, System.Exception innerException)
  RVA=0x0A8A78BC  token=0x60000A7  Newtonsoft.Json.JsonReaderException Create(Newtonsoft.Json.JsonReader reader, System.String message)
  RVA=0x0A8A782C  token=0x60000A8  Newtonsoft.Json.JsonReaderException Create(Newtonsoft.Json.JsonReader reader, System.String message, System.Exception ex)
  RVA=0x0A8A773C  token=0x60000A9  Newtonsoft.Json.JsonReaderException Create(Newtonsoft.Json.IJsonLineInfo lineInfo, System.String path, System.String message, System.Exception ex)
END_CLASS

CLASS: Newtonsoft.Json.JsonRequiredAttribute
TYPE:  sealed class
TOKEN: 0x2000028
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.JsonSerializationException
TYPE:  class
TOKEN: 0x2000029
SIZE:  0xA0
EXTENDS: Newtonsoft.Json.JsonException
FIELDS:
  private   readonly System.Int32                    <LineNumber>k__BackingField  // 0x90
  private   readonly System.Int32                    <LinePosition>k__BackingField  // 0x94
  private   readonly System.String                   <Path>k__BackingField  // 0x98
METHODS:
  RVA=0x0A8A78C8  token=0x60000AA  System.Void .ctor()
  RVA=0x0A8A7948  token=0x60000AB  System.Void .ctor(System.String message)
  RVA=0x0A8A9F34  token=0x60000AC  System.Void .ctor(System.String message, System.Exception innerException)
  RVA=0x0A8A7928  token=0x60000AD  System.Void .ctor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
  RVA=0x0A8A78D0  token=0x60000AE  System.Void .ctor(System.String message, System.String path, System.Int32 lineNumber, System.Int32 linePosition, System.Exception innerException)
  RVA=0x0A8A9DA8  token=0x60000AF  Newtonsoft.Json.JsonSerializationException Create(Newtonsoft.Json.JsonReader reader, System.String message)
  RVA=0x0A8A9DB4  token=0x60000B0  Newtonsoft.Json.JsonSerializationException Create(Newtonsoft.Json.JsonReader reader, System.String message, System.Exception ex)
  RVA=0x0A8A9E44  token=0x60000B1  Newtonsoft.Json.JsonSerializationException Create(Newtonsoft.Json.IJsonLineInfo lineInfo, System.String path, System.String message, System.Exception ex)
END_CLASS

CLASS: Newtonsoft.Json.JsonSerializer
TYPE:  class
TOKEN: 0x200002A
SIZE:  0xE0
FIELDS:
  private           Newtonsoft.Json.TypeNameHandling_typeNameHandling  // 0x10
  private           Newtonsoft.Json.TypeNameAssemblyFormatHandling_typeNameAssemblyFormatHandling  // 0x14
  private           Newtonsoft.Json.PreserveReferencesHandling_preserveReferencesHandling  // 0x18
  private           Newtonsoft.Json.ReferenceLoopHandling_referenceLoopHandling  // 0x1c
  private           Newtonsoft.Json.MissingMemberHandling_missingMemberHandling  // 0x20
  private           Newtonsoft.Json.ObjectCreationHandling_objectCreationHandling  // 0x24
  private           Newtonsoft.Json.NullValueHandling_nullValueHandling  // 0x28
  private           Newtonsoft.Json.DefaultValueHandling_defaultValueHandling  // 0x2c
  private           Newtonsoft.Json.ConstructorHandling_constructorHandling  // 0x30
  private           Newtonsoft.Json.MetadataPropertyHandling_metadataPropertyHandling  // 0x34
  private           Newtonsoft.Json.JsonConverterCollection_converters  // 0x38
  private           Newtonsoft.Json.Serialization.IContractResolver_contractResolver  // 0x40
  private           Newtonsoft.Json.Serialization.ITraceWriter_traceWriter  // 0x48
  private           System.Collections.IEqualityComparer_equalityComparer  // 0x50
  private           Newtonsoft.Json.Serialization.ISerializationBinder_serializationBinder  // 0x58
  private           System.Runtime.Serialization.StreamingContext_context  // 0x60
  private           Newtonsoft.Json.Serialization.IReferenceResolver_referenceResolver  // 0x70
  private           System.Nullable<Newtonsoft.Json.Formatting>_formatting  // 0x78
  private           System.Nullable<Newtonsoft.Json.DateFormatHandling>_dateFormatHandling  // 0x80
  private           System.Nullable<Newtonsoft.Json.DateTimeZoneHandling>_dateTimeZoneHandling  // 0x88
  private           System.Nullable<Newtonsoft.Json.DateParseHandling>_dateParseHandling  // 0x90
  private           System.Nullable<Newtonsoft.Json.FloatFormatHandling>_floatFormatHandling  // 0x98
  private           System.Nullable<Newtonsoft.Json.FloatParseHandling>_floatParseHandling  // 0xa0
  private           System.Nullable<Newtonsoft.Json.StringEscapeHandling>_stringEscapeHandling  // 0xa8
  private           System.Globalization.CultureInfo_culture  // 0xb0
  private           System.Nullable<System.Int32>   _maxDepth  // 0xb8
  private           System.Boolean                  _maxDepthSet  // 0xc0
  private           System.Nullable<System.Boolean> _checkAdditionalContent  // 0xc1
  private           System.String                   _dateFormatString  // 0xc8
  private           System.Boolean                  _dateFormatStringSet  // 0xd0
  private           System.EventHandler<Newtonsoft.Json.Serialization.ErrorEventArgs>Error  // 0xd8
PROPERTIES:
  ReferenceResolver  set=0x0A8AA708
  SerializationBinder  get=0x02E56440  set=0x0A8AA794
  TraceWriter  get=0x04D86200  set=0x035A41A0
  EqualityComparer  set=0x02FDAEA0
  TypeNameHandling  set=0x0411B110
  TypeNameAssemblyFormatHandling  set=0x04D83110
  PreserveReferencesHandling  set=0x0A8AA6A0
  ReferenceLoopHandling  set=0x04D83130
  MissingMemberHandling  set=0x0A8AA638
  NullValueHandling  get=0x011F2A90  set=0x0411B460
  DefaultValueHandling  set=0x0411B480
  ObjectCreationHandling  set=0x04CB28E0
  ConstructorHandling  set=0x04CB2900
  MetadataPropertyHandling  get=0x04D86730  set=0x0A8AA5D0
  Converters  get=0x03EA57F0
  ContractResolver  get=0x04D85A60  set=0x049DAC00
  Context  get=0x04DA1280  set=0x0A8AA5C0
  Formatting  get=0x04CD9280  set=0x04B89F60
  MaxDepth  get=0x04D85EF0
  CheckAdditionalContent  get=0x04DA5BF0  set=0x04119FE0
EVENTS:
  Error  add=add_Error  remove=remove_Error
METHODS:
  RVA=0x04D9E3E0  token=0x60000D0  System.Boolean IsCheckAdditionalContentSet()
  RVA=0x035A6AC0  token=0x60000D1  System.Void .ctor()
  RVA=0x035A6A20  token=0x60000D2  Newtonsoft.Json.JsonSerializer Create()
  RVA=0x035A69F0  token=0x60000D3  Newtonsoft.Json.JsonSerializer Create(Newtonsoft.Json.JsonSerializerSettings settings)
  RVA=0x035A6980  token=0x60000D4  Newtonsoft.Json.JsonSerializer CreateDefault()
  RVA=0x035A6940  token=0x60000D5  Newtonsoft.Json.JsonSerializer CreateDefault(Newtonsoft.Json.JsonSerializerSettings settings)
  RVA=0x035A6D40  token=0x60000D6  System.Void ApplySerializerSettings(Newtonsoft.Json.JsonSerializer serializer, Newtonsoft.Json.JsonSerializerSettings settings)
  RVA=0x0A8AA454  token=0x60000D7  System.Void Populate(Newtonsoft.Json.JsonReader reader, System.Object target)
  RVA=0x0A8AA1C0  token=0x60000D8  System.Void PopulateInternal(Newtonsoft.Json.JsonReader reader, System.Object target)
  RVA=-1  // generic def  token=0x60000D9  T Deserialize(Newtonsoft.Json.JsonReader reader)
  RVA=0x05392F9C  token=0x60000DA  System.Object Deserialize(Newtonsoft.Json.JsonReader reader, System.Type objectType)
  RVA=0x02FBCD40  token=0x60000DB  System.Object DeserializeInternal(Newtonsoft.Json.JsonReader reader, System.Type objectType)
  RVA=0x02FBCB20  token=0x60000DC  System.Void SetupReader(Newtonsoft.Json.JsonReader reader, System.Globalization.CultureInfo& previousCulture, System.Nullable<Newtonsoft.Json.DateTimeZoneHandling>& previousDateTimeZoneHandling, System.Nullable<Newtonsoft.Json.DateParseHandling>& previousDateParseHandling, System.Nullable<Newtonsoft.Json.FloatParseHandling>& previousFloatParseHandling, System.Nullable<System.Int32>& previousMaxDepth, System.String& previousDateFormatString)
  RVA=0x02FBEC20  token=0x60000DD  System.Void ResetReader(Newtonsoft.Json.JsonReader reader, System.Globalization.CultureInfo previousCulture, System.Nullable<Newtonsoft.Json.DateTimeZoneHandling> previousDateTimeZoneHandling, System.Nullable<Newtonsoft.Json.DateParseHandling> previousDateParseHandling, System.Nullable<Newtonsoft.Json.FloatParseHandling> previousFloatParseHandling, System.Nullable<System.Int32> previousMaxDepth, System.String previousDateFormatString)
  RVA=0x053924E0  token=0x60000DE  System.Void Serialize(Newtonsoft.Json.JsonWriter jsonWriter, System.Object value, System.Type objectType)
  RVA=0x05392EF0  token=0x60000DF  System.Void Serialize(Newtonsoft.Json.JsonWriter jsonWriter, System.Object value)
  RVA=0x0A8AA0C4  token=0x60000E0  Newtonsoft.Json.Serialization.TraceJsonReader CreateTraceJsonReader(Newtonsoft.Json.JsonReader reader)
  RVA=0x03F71C60  token=0x60000E1  System.Void SerializeInternal(Newtonsoft.Json.JsonWriter jsonWriter, System.Object value, System.Type objectType)
  RVA=0x0A8AA144  token=0x60000E2  Newtonsoft.Json.Serialization.IReferenceResolver GetReferenceResolver()
  RVA=0x05390BBC  token=0x60000E3  Newtonsoft.Json.JsonConverter GetMatchingConverter(System.Type type)
  RVA=0x02FAFF80  token=0x60000E4  Newtonsoft.Json.JsonConverter GetMatchingConverter(System.Collections.Generic.IList<Newtonsoft.Json.JsonConverter> converters, System.Type objectType)
  RVA=0x0A8AA1A0  token=0x60000E5  System.Void OnError(Newtonsoft.Json.Serialization.ErrorEventArgs e)
END_CLASS

CLASS: Newtonsoft.Json.JsonSerializerSettings
TYPE:  class
TOKEN: 0x200002B
SIZE:  0x110
FIELDS:
  private   static  Newtonsoft.Json.ReferenceLoopHandlingDefaultReferenceLoopHandling  // const
  private   static  Newtonsoft.Json.MissingMemberHandlingDefaultMissingMemberHandling  // const
  private   static  Newtonsoft.Json.NullValueHandlingDefaultNullValueHandling  // const
  private   static  Newtonsoft.Json.DefaultValueHandlingDefaultDefaultValueHandling  // const
  private   static  Newtonsoft.Json.ObjectCreationHandlingDefaultObjectCreationHandling  // const
  private   static  Newtonsoft.Json.PreserveReferencesHandlingDefaultPreserveReferencesHandling  // const
  private   static  Newtonsoft.Json.ConstructorHandlingDefaultConstructorHandling  // const
  private   static  Newtonsoft.Json.TypeNameHandlingDefaultTypeNameHandling  // const
  private   static  Newtonsoft.Json.MetadataPropertyHandlingDefaultMetadataPropertyHandling  // const
  private   static readonly System.Runtime.Serialization.StreamingContextDefaultContext  // static @ 0x0
  private   static  Newtonsoft.Json.Formatting      DefaultFormatting  // const
  private   static  Newtonsoft.Json.DateFormatHandlingDefaultDateFormatHandling  // const
  private   static  Newtonsoft.Json.DateTimeZoneHandlingDefaultDateTimeZoneHandling  // const
  private   static  Newtonsoft.Json.DateParseHandlingDefaultDateParseHandling  // const
  private   static  Newtonsoft.Json.FloatParseHandlingDefaultFloatParseHandling  // const
  private   static  Newtonsoft.Json.FloatFormatHandlingDefaultFloatFormatHandling  // const
  private   static  Newtonsoft.Json.StringEscapeHandlingDefaultStringEscapeHandling  // const
  private   static  Newtonsoft.Json.TypeNameAssemblyFormatHandlingDefaultTypeNameAssemblyFormatHandling  // const
  private   static readonly System.Globalization.CultureInfoDefaultCulture  // static @ 0x10
  private   static  System.Boolean                  DefaultCheckAdditionalContent  // const
  private   static  System.String                   DefaultDateFormatString  // const
  private   static  System.Int32                    DefaultMaxDepth  // const
  private           System.Nullable<Newtonsoft.Json.Formatting>_formatting  // 0x10
  private           System.Nullable<Newtonsoft.Json.DateFormatHandling>_dateFormatHandling  // 0x18
  private           System.Nullable<Newtonsoft.Json.DateTimeZoneHandling>_dateTimeZoneHandling  // 0x20
  private           System.Nullable<Newtonsoft.Json.DateParseHandling>_dateParseHandling  // 0x28
  private           System.Nullable<Newtonsoft.Json.FloatFormatHandling>_floatFormatHandling  // 0x30
  private           System.Nullable<Newtonsoft.Json.FloatParseHandling>_floatParseHandling  // 0x38
  private           System.Nullable<Newtonsoft.Json.StringEscapeHandling>_stringEscapeHandling  // 0x40
  private           System.Globalization.CultureInfo_culture  // 0x48
  private           System.Nullable<System.Boolean> _checkAdditionalContent  // 0x50
  private           System.Nullable<System.Int32>   _maxDepth  // 0x54
  private           System.Boolean                  _maxDepthSet  // 0x5c
  private           System.String                   _dateFormatString  // 0x60
  private           System.Boolean                  _dateFormatStringSet  // 0x68
  private           System.Nullable<Newtonsoft.Json.TypeNameAssemblyFormatHandling>_typeNameAssemblyFormatHandling  // 0x6c
  private           System.Nullable<Newtonsoft.Json.DefaultValueHandling>_defaultValueHandling  // 0x74
  private           System.Nullable<Newtonsoft.Json.PreserveReferencesHandling>_preserveReferencesHandling  // 0x7c
  private           System.Nullable<Newtonsoft.Json.NullValueHandling>_nullValueHandling  // 0x84
  private           System.Nullable<Newtonsoft.Json.ObjectCreationHandling>_objectCreationHandling  // 0x8c
  private           System.Nullable<Newtonsoft.Json.MissingMemberHandling>_missingMemberHandling  // 0x94
  private           System.Nullable<Newtonsoft.Json.ReferenceLoopHandling>_referenceLoopHandling  // 0x9c
  private           System.Nullable<System.Runtime.Serialization.StreamingContext>_context  // 0xa8
  private           System.Nullable<Newtonsoft.Json.ConstructorHandling>_constructorHandling  // 0xc0
  private           System.Nullable<Newtonsoft.Json.TypeNameHandling>_typeNameHandling  // 0xc8
  private           System.Nullable<Newtonsoft.Json.MetadataPropertyHandling>_metadataPropertyHandling  // 0xd0
  private           System.Collections.Generic.IList<Newtonsoft.Json.JsonConverter><Converters>k__BackingField  // 0xd8
  private           Newtonsoft.Json.Serialization.IContractResolver<ContractResolver>k__BackingField  // 0xe0
  private           System.Collections.IEqualityComparer<EqualityComparer>k__BackingField  // 0xe8
  private           System.Func<Newtonsoft.Json.Serialization.IReferenceResolver><ReferenceResolverProvider>k__BackingField  // 0xf0
  private           Newtonsoft.Json.Serialization.ITraceWriter<TraceWriter>k__BackingField  // 0xf8
  private           Newtonsoft.Json.Serialization.ISerializationBinder<SerializationBinder>k__BackingField  // 0x100
  private           System.EventHandler<Newtonsoft.Json.Serialization.ErrorEventArgs><Error>k__BackingField  // 0x108
PROPERTIES:
  ReferenceLoopHandling  get=0x04D7C0B0  set=0x03581CC0
  MissingMemberHandling  get=0x0A8AA04C
  ObjectCreationHandling  get=0x035A7100  set=0x03581D80
  NullValueHandling  get=0x04D86890  set=0x04D2B120
  DefaultValueHandling  get=0x04D863C0  set=0x04D2B180
  Converters  get=0x04D88EF0  set=0x06902698
  PreserveReferencesHandling  get=0x0A8AA088
  TypeNameHandling  get=0x04DA5BE0  set=0x03581D20
  MetadataPropertyHandling  get=0x0A8AA010
  TypeNameAssemblyFormatHandling  get=0x04D7C070  set=0x03581C60
  ConstructorHandling  get=0x035A70C0  set=0x03581DE0
  ContractResolver  get=0x04D8D1C0  set=0x06AC5B08
  EqualityComparer  get=0x04D88590  set=0x06AC5B1C
  ReferenceResolverProvider  get=0x04D8D1A0  set=0x06AC5AE0
  TraceWriter  get=0x04D877C0  set=0x06790298
  SerializationBinder  get=0x04D87D50  set=0x067A5EA0
  Error  get=0x04D85F90  set=0x068C2E50
  Context  get=0x0A8A9F74
  MaxDepth  set=0x04CAB710
  Formatting  set=0x03581E40
  CheckAdditionalContent  get=0x0A8A9F3C
METHODS:
  RVA=0x04D21F60  token=0x6000109  System.Void .cctor()
  RVA=0x035A41B0  token=0x600010A  System.Void .ctor()
  RVA=0x035A4230  token=0x600010B  System.Void .ctor(Newtonsoft.Json.JsonSerializerSettings original)
END_CLASS

CLASS: Newtonsoft.Json.JsonTextReader
TYPE:  class
TOKEN: 0x200002C
SIZE:  0xD8
EXTENDS: Newtonsoft.Json.JsonReader
IMPLEMENTS: Newtonsoft.Json.IJsonLineInfo
FIELDS:
  private   readonly System.Boolean                  _safeAsync  // 0x78
  private   readonly System.IO.TextReader            _reader  // 0x80
  private           System.Char[]                   _chars  // 0x88
  private           System.Int32                    _charsUsed  // 0x90
  private           System.Int32                    _charPos  // 0x94
  private           System.Int32                    _lineStartPos  // 0x98
  private           System.Int32                    _lineNumber  // 0x9c
  private           System.Boolean                  _isEndOfFile  // 0xa0
  private           Newtonsoft.Json.Utilities.StringBuffer_stringBuffer  // 0xa8
  private           Newtonsoft.Json.Utilities.StringReference_stringReference  // 0xb8
  private           Newtonsoft.Json.IArrayPool<System.Char>_arrayPool  // 0xc8
  private           Newtonsoft.Json.JsonNameTable   <PropertyNameTable>k__BackingField  // 0xd0
PROPERTIES:
  PropertyNameTable  get=0x04D876B0  set=0x0678D7BC
  LineNumber  get=0x03427620
  LinePosition  get=0x04DA5C50
METHODS:
  RVA=0x035A6C60  token=0x600010C  System.Void .ctor(System.IO.TextReader reader)
  RVA=0x03778750  token=0x600010F  System.Void EnsureBufferNotEmpty()
  RVA=0x02F054D0  token=0x6000110  System.Void SetNewLine(System.Boolean hasNextChar)
  RVA=0x04DA5C20  token=0x6000111  System.Void OnNewLine(System.Int32 pos)
  RVA=0x02FBB340  token=0x6000112  System.Void ParseString(System.Char quote, Newtonsoft.Json.ReadType readType)
  RVA=0x02FBAF00  token=0x6000113  System.Void ParseReadString(System.Char quote, Newtonsoft.Json.ReadType readType)
  RVA=0x032A9100  token=0x6000114  System.Void BlockCopyChars(System.Char[] src, System.Int32 srcOffset, System.Char[] dst, System.Int32 dstOffset, System.Int32 count)
  RVA=0x02F04780  token=0x6000115  System.Void ShiftBufferIfNeeded()
  RVA=0x05390C28  token=0x6000116  System.Int32 ReadData(System.Boolean append)
  RVA=0x03777AF0  token=0x6000117  System.Void PrepareBufferForReadData(System.Boolean append, System.Int32 charsRequired)
  RVA=0x03777960  token=0x6000118  System.Int32 ReadData(System.Boolean append, System.Int32 charsRequired)
  RVA=0x02F051B0  token=0x6000119  System.Boolean EnsureChars(System.Int32 relativePosition, System.Boolean append)
  RVA=0x03777810  token=0x600011A  System.Boolean ReadChars(System.Int32 relativePosition, System.Boolean append)
  RVA=0x02F04D70  token=0x600011B  System.Boolean Read()
  RVA=0x02FBC490  token=0x600011C  System.Nullable<System.Int32> ReadAsInt32()
  RVA=0x0A8AC234  token=0x600011D  System.Nullable<System.DateTime> ReadAsDateTime()
  RVA=0x02FB86A0  token=0x600011E  System.String ReadAsString()
  RVA=0x0A8ABCEC  token=0x600011F  System.Byte[] ReadAsBytes()
  RVA=0x02FB7970  token=0x6000120  System.Object ReadStringValue(Newtonsoft.Json.ReadType readType)
  RVA=0x03A6E710  token=0x6000121  System.Object FinishReadQuotedStringValue(Newtonsoft.Json.ReadType readType)
  RVA=0x0A8AAA20  token=0x6000122  Newtonsoft.Json.JsonReaderException CreateUnexpectedCharacterException(System.Char c)
  RVA=0x034288E0  token=0x6000123  System.Nullable<System.Boolean> ReadAsBoolean()
  RVA=0x0A8ABC84  token=0x6000124  System.Void ProcessValueComma()
  RVA=0x02FBC540  token=0x6000125  System.Object ReadNumberValue(Newtonsoft.Json.ReadType readType)
  RVA=0x0A8AAB08  token=0x6000126  System.Object FinishReadQuotedNumber(Newtonsoft.Json.ReadType readType)
  RVA=0x0A8AC1A8  token=0x6000127  System.Nullable<System.DateTimeOffset> ReadAsDateTimeOffset()
  RVA=0x0A8AC2B8  token=0x6000128  System.Nullable<System.Decimal> ReadAsDecimal()
  RVA=0x02FBC3D0  token=0x6000129  System.Nullable<System.Double> ReadAsDouble()
  RVA=0x0A8AAC68  token=0x600012A  System.Void HandleNull()
  RVA=0x0A8AC344  token=0x600012B  System.Void ReadFinished()
  RVA=0x03429DB0  token=0x600012C  System.Boolean ReadNullChar()
  RVA=0x02F04F00  token=0x600012D  System.Void EnsureBuffer()
  RVA=0x02F06BF0  token=0x600012E  System.Void ReadStringIntoBuffer(System.Char quote)
  RVA=0x02F04F80  token=0x600012F  System.Void FinishReadStringIntoBuffer(System.Int32 charPos, System.Int32 initialPosition, System.Int32 lastWritePosition)
  RVA=0x04208C00  token=0x6000130  System.Void WriteCharToBuffer(System.Char writeChar, System.Int32 lastWritePosition, System.Int32 writeToPosition)
  RVA=0x0A8AA890  token=0x6000131  System.Char ConvertUnicode(System.Boolean enoughChars)
  RVA=0x0A8ABB48  token=0x6000132  System.Char ParseUnicode()
  RVA=0x02F05CF0  token=0x6000133  System.Void ReadNumberIntoBuffer()
  RVA=0x02F07660  token=0x6000134  System.Boolean ReadNumberCharIntoBuffer(System.Char currentChar, System.Int32 charPos)
  RVA=0x04DA5C00  token=0x6000135  System.Void ClearRecentString()
  RVA=0x02F05E20  token=0x6000136  System.Boolean ParsePostValue(System.Boolean ignoreComments)
  RVA=0x02F06810  token=0x6000137  System.Boolean ParseObject()
  RVA=0x02F05530  token=0x6000138  System.Boolean ParseProperty()
  RVA=0x0A8AC644  token=0x6000139  System.Boolean ValidIdentifierChar(System.Char value)
  RVA=0x0A8ABB74  token=0x600013A  System.Void ParseUnquotedProperty()
  RVA=0x0A8AC494  token=0x600013B  System.Boolean ReadUnquotedPropertyReportIfDone(System.Char currentChar, System.Int32 initialPosition)
  RVA=0x02FBB980  token=0x600013C  System.Boolean ParseValue()
  RVA=0x04DA5C30  token=0x600013D  System.Void ProcessLineFeed()
  RVA=0x02F05330  token=0x600013E  System.Void ProcessCarriageReturn(System.Boolean append)
  RVA=0x02F03EE0  token=0x600013F  System.Void EatWhitespace()
  RVA=0x0A8AB05C  token=0x6000140  System.Void ParseConstructor()
  RVA=0x02FB86E0  token=0x6000141  System.Void ParseNumber(Newtonsoft.Json.ReadType readType)
  RVA=0x02FB8F70  token=0x6000142  System.Void ParseReadNumber(Newtonsoft.Json.ReadType readType, System.Char firstChar, System.Int32 initialPosition)
  RVA=0x0A8AC5F0  token=0x6000143  Newtonsoft.Json.JsonReaderException ThrowReaderError(System.String message, System.Exception ex)
  RVA=0x0A8AA820  token=0x6000144  System.Object BigIntegerParse(System.String number, System.Globalization.CultureInfo culture)
  RVA=0x0A8AAD44  token=0x6000145  System.Void ParseComment(System.Boolean setToken)
  RVA=0x0A8AAACC  token=0x6000146  System.Void EndComment(System.Boolean setToken, System.Int32 initialPosition, System.Int32 endPosition)
  RVA=0x03429B30  token=0x6000147  System.Boolean MatchValue(System.String value)
  RVA=0x03429D20  token=0x6000148  System.Boolean MatchValue(System.Boolean enoughChars, System.String value)
  RVA=0x03429790  token=0x6000149  System.Boolean MatchValueWithTrailingSeparator(System.String value)
  RVA=0x03429AE0  token=0x600014A  System.Boolean IsSeparator(System.Char c)
  RVA=0x0A8AB9E0  token=0x600014B  System.Void ParseTrue()
  RVA=0x0342A3B0  token=0x600014C  System.Void ParseNull()
  RVA=0x0A8ABAA8  token=0x600014D  System.Void ParseUndefined()
  RVA=0x03428810  token=0x600014E  System.Void ParseFalse()
  RVA=0x0A8AB788  token=0x600014F  System.Object ParseNumberNegativeInfinity(Newtonsoft.Json.ReadType readType)
  RVA=0x0A8AB610  token=0x6000150  System.Object ParseNumberNegativeInfinity(Newtonsoft.Json.ReadType readType, System.Boolean matched)
  RVA=0x0A8AB970  token=0x6000151  System.Object ParseNumberPositiveInfinity(Newtonsoft.Json.ReadType readType)
  RVA=0x0A8AB7F8  token=0x6000152  System.Object ParseNumberPositiveInfinity(Newtonsoft.Json.ReadType readType, System.Boolean matched)
  RVA=0x0A8AB428  token=0x6000153  System.Object ParseNumberNaN(Newtonsoft.Json.ReadType readType)
  RVA=0x0A8AB498  token=0x6000154  System.Object ParseNumberNaN(Newtonsoft.Json.ReadType readType, System.Boolean matched)
  RVA=0x037785D0  token=0x6000155  System.Void Close()
  RVA=0x02FFF600  token=0x6000156  System.Boolean HasLineInfo()
END_CLASS

CLASS: Newtonsoft.Json.ReadType
TYPE:  sealed struct
TOKEN: 0x200002D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.ReadType        Read  // const
  public    static  Newtonsoft.Json.ReadType        ReadAsInt32  // const
  public    static  Newtonsoft.Json.ReadType        ReadAsInt64  // const
  public    static  Newtonsoft.Json.ReadType        ReadAsBytes  // const
  public    static  Newtonsoft.Json.ReadType        ReadAsString  // const
  public    static  Newtonsoft.Json.ReadType        ReadAsDecimal  // const
  public    static  Newtonsoft.Json.ReadType        ReadAsDateTime  // const
  public    static  Newtonsoft.Json.ReadType        ReadAsDateTimeOffset  // const
  public    static  Newtonsoft.Json.ReadType        ReadAsDouble  // const
  public    static  Newtonsoft.Json.ReadType        ReadAsBoolean  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.JsonTextWriter
TYPE:  class
TOKEN: 0x200002E
SIZE:  0xA8
EXTENDS: Newtonsoft.Json.JsonWriter
FIELDS:
  private   readonly System.Boolean                  _safeAsync  // 0x60
  private   readonly System.IO.TextWriter            _writer  // 0x68
  private           Newtonsoft.Json.Utilities.Base64Encoder_base64Encoder  // 0x70
  private           System.Char                     _indentChar  // 0x78
  private           System.Int32                    _indentation  // 0x7c
  private           System.Char                     _quoteChar  // 0x80
  private           System.Boolean                  _quoteName  // 0x82
  private           System.Boolean[]                _charEscapeFlags  // 0x88
  private           System.Char[]                   _writeBuffer  // 0x90
  private           Newtonsoft.Json.IArrayPool<System.Char>_arrayPool  // 0x98
  private           System.Char[]                   _indentChars  // 0xa0
PROPERTIES:
  Base64Encoder  get=0x0A8AD770
  QuoteChar  get=0x04DA5C60
METHODS:
  RVA=0x03425CE0  token=0x600015B  System.Void .ctor(System.IO.TextWriter textWriter)
  RVA=0x03778720  token=0x600015C  System.Void Close()
  RVA=0x03778560  token=0x600015D  System.Void CloseBufferAndWriter()
  RVA=0x03778CB0  token=0x600015E  System.Void WriteStartObject()
  RVA=0x03777750  token=0x600015F  System.Void WriteStartArray()
  RVA=0x0A8AC894  token=0x6000160  System.Void WriteStartConstructor(System.String name)
  RVA=0x03778CF0  token=0x6000161  System.Void WriteEnd(Newtonsoft.Json.JsonToken token)
  RVA=0x03778B30  token=0x6000162  System.Void WritePropertyName(System.String name)
  RVA=0x03779260  token=0x6000163  System.Void WritePropertyName(System.String name, System.Boolean escape)
  RVA=0x0A8AC69C  token=0x6000164  System.Void OnStringEscapeHandlingChanged()
  RVA=0x03425DF0  token=0x6000165  System.Void UpdateCharEscapeFlags()
  RVA=0x03779EA0  token=0x6000166  System.Void WriteIndent()
  RVA=0x03779FD0  token=0x6000167  System.Int32 SetIndentChars()
  RVA=0x03778E60  token=0x6000168  System.Void WriteValueDelimiter()
  RVA=0x03778ED0  token=0x6000169  System.Void WriteIndentSpace()
  RVA=0x03779620  token=0x600016A  System.Void WriteValueInternal(System.String value, Newtonsoft.Json.JsonToken token)
  RVA=0x0A8ACCD8  token=0x600016B  System.Void WriteValue(System.Object value)
  RVA=0x0A8AC808  token=0x600016C  System.Void WriteNull()
  RVA=0x0A8AC92C  token=0x600016D  System.Void WriteUndefined()
  RVA=0x0A8AC870  token=0x600016E  System.Void WriteRaw(System.String json)
  RVA=0x03778B90  token=0x600016F  System.Void WriteValue(System.String value)
  RVA=0x03778D50  token=0x6000170  System.Void WriteEscapedString(System.String value, System.Boolean quote)
  RVA=0x0A8ACDA0  token=0x6000171  System.Void WriteValue(System.Int32 value)
  RVA=0x0A8ACFD4  token=0x6000172  System.Void WriteValue(System.UInt32 value)
  RVA=0x03778AF0  token=0x6000173  System.Void WriteValue(System.Int64 value)
  RVA=0x0A8AD5F8  token=0x6000174  System.Void WriteValue(System.UInt64 value)
  RVA=0x0A8AD630  token=0x6000175  System.Void WriteValue(System.Single value)
  RVA=0x0A8AD6AC  token=0x6000176  System.Void WriteValue(System.Nullable<System.Single> value)
  RVA=0x0A8AD560  token=0x6000177  System.Void WriteValue(System.Double value)
  RVA=0x0A8ACDD4  token=0x6000178  System.Void WriteValue(System.Nullable<System.Double> value)
  RVA=0x03778BE0  token=0x6000179  System.Void WriteValue(System.Boolean value)
  RVA=0x0A8ACE9C  token=0x600017A  System.Void WriteValue(System.Int16 value)
  RVA=0x0A8ACFA0  token=0x600017B  System.Void WriteValue(System.UInt16 value)
  RVA=0x0A8AD4F0  token=0x600017C  System.Void WriteValue(System.Char value)
  RVA=0x0A8AD4BC  token=0x600017D  System.Void WriteValue(System.Byte value)
  RVA=0x0A8AD254  token=0x600017E  System.Void WriteValue(System.SByte value)
  RVA=0x0A8AD144  token=0x600017F  System.Void WriteValue(System.Decimal value)
  RVA=0x0A8AD288  token=0x6000180  System.Void WriteValue(System.DateTime value)
  RVA=0x0A8ACAF8  token=0x6000181  System.Int32 WriteValueToBuffer(System.DateTime value)
  RVA=0x0A8ACC1C  token=0x6000182  System.Void WriteValue(System.Byte[] value)
  RVA=0x0A8AD008  token=0x6000183  System.Void WriteValue(System.DateTimeOffset value)
  RVA=0x0A8AC994  token=0x6000184  System.Int32 WriteValueToBuffer(System.DateTimeOffset value)
  RVA=0x0A8ACED0  token=0x6000185  System.Void WriteValue(System.Guid value)
  RVA=0x0A8AD3E0  token=0x6000186  System.Void WriteValue(System.TimeSpan value)
  RVA=0x0A8AD1BC  token=0x6000187  System.Void WriteValue(System.Uri value)
  RVA=0x0A8AC6A4  token=0x6000188  System.Void WriteComment(System.String text)
  RVA=0x03778E10  token=0x6000189  System.Void EnsureWriteBuffer()
  RVA=0x03779650  token=0x600018A  System.Void WriteIntegerValue(System.Int64 value)
  RVA=0x03778940  token=0x600018B  System.Void WriteIntegerValue(System.UInt64 value, System.Boolean negative)
  RVA=0x037789D0  token=0x600018C  System.Int32 WriteNumberToBuffer(System.UInt64 value, System.Boolean negative)
  RVA=0x0A8AC7B4  token=0x600018D  System.Void WriteIntegerValue(System.Int32 value)
  RVA=0x0A8AC740  token=0x600018E  System.Void WriteIntegerValue(System.UInt32 value, System.Boolean negative)
  RVA=0x03778A10  token=0x600018F  System.Int32 WriteNumberToBuffer(System.UInt32 value, System.Boolean negative)
END_CLASS

CLASS: Newtonsoft.Json.JsonToken
TYPE:  sealed struct
TOKEN: 0x200002F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.JsonToken       None  // const
  public    static  Newtonsoft.Json.JsonToken       StartObject  // const
  public    static  Newtonsoft.Json.JsonToken       StartArray  // const
  public    static  Newtonsoft.Json.JsonToken       StartConstructor  // const
  public    static  Newtonsoft.Json.JsonToken       PropertyName  // const
  public    static  Newtonsoft.Json.JsonToken       Comment  // const
  public    static  Newtonsoft.Json.JsonToken       Raw  // const
  public    static  Newtonsoft.Json.JsonToken       Integer  // const
  public    static  Newtonsoft.Json.JsonToken       Float  // const
  public    static  Newtonsoft.Json.JsonToken       String  // const
  public    static  Newtonsoft.Json.JsonToken       Boolean  // const
  public    static  Newtonsoft.Json.JsonToken       Null  // const
  public    static  Newtonsoft.Json.JsonToken       Undefined  // const
  public    static  Newtonsoft.Json.JsonToken       EndObject  // const
  public    static  Newtonsoft.Json.JsonToken       EndArray  // const
  public    static  Newtonsoft.Json.JsonToken       EndConstructor  // const
  public    static  Newtonsoft.Json.JsonToken       Date  // const
  public    static  Newtonsoft.Json.JsonToken       Bytes  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.JsonWriter
TYPE:  abstract class
TOKEN: 0x2000030
SIZE:  0x60
IMPLEMENTS: System.IDisposable
FIELDS:
  private   static readonly Newtonsoft.Json.JsonWriter.State[][]StateArray  // static @ 0x0
  private   static readonly Newtonsoft.Json.JsonWriter.State[][]StateArrayTemplate  // static @ 0x8
  private           System.Collections.Generic.List<Newtonsoft.Json.JsonPosition>_stack  // 0x10
  private           Newtonsoft.Json.JsonPosition    _currentPosition  // 0x18
  private           Newtonsoft.Json.JsonWriter.State_currentState  // 0x30
  private           Newtonsoft.Json.Formatting      _formatting  // 0x34
  private           System.Boolean                  <CloseOutput>k__BackingField  // 0x38
  private           System.Boolean                  <AutoCompleteOnClose>k__BackingField  // 0x39
  private           Newtonsoft.Json.DateFormatHandling_dateFormatHandling  // 0x3c
  private           Newtonsoft.Json.DateTimeZoneHandling_dateTimeZoneHandling  // 0x40
  private           Newtonsoft.Json.StringEscapeHandling_stringEscapeHandling  // 0x44
  private           Newtonsoft.Json.FloatFormatHandling_floatFormatHandling  // 0x48
  private           System.String                   _dateFormatString  // 0x50
  private           System.Globalization.CultureInfo_culture  // 0x58
PROPERTIES:
  CloseOutput  get=0x011F2A70  set=0x011F2A80
  AutoCompleteOnClose  get=0x04D86650  set=0x04D86680
  Top  get=0x03779FA0
  WriteState  get=0x0A8AE7E8
  ContainerPath  get=0x0A8AE670
  Path  get=0x0A8AE6F4
  Formatting  get=0x04D86730  set=0x03425B10
  DateFormatHandling  get=0x04D86640  set=0x0A8AE8EC
  DateTimeZoneHandling  get=0x04D865E0  set=0x0A8AE954
  StringEscapeHandling  get=0x04D86530  set=0x0A8AEA24
  FloatFormatHandling  get=0x04D863D0  set=0x0A8AE9BC
  DateFormatString  get=0x04D86270  set=0x02FDAEA0
  Culture  get=0x03F71FB0  set=0x036E5590
METHODS:
  RVA=0x037D88D0  token=0x6000190  Newtonsoft.Json.JsonWriter.State[][] BuildStateArray()
  RVA=0x037D9EE0  token=0x6000191  System.Void .cctor()
  RVA=0x041E1670  token=0x60001A2  System.Void OnStringEscapeHandlingChanged()
  RVA=0x04DA5C70  token=0x60001A9  System.Void .ctor()
  RVA=0x03779390  token=0x60001AA  System.Void UpdateScopeWithFinishedValue()
  RVA=0x037794C0  token=0x60001AB  System.Void Push(Newtonsoft.Json.JsonContainerType value)
  RVA=0x03637DA0  token=0x60001AC  Newtonsoft.Json.JsonContainerType Pop()
  RVA=0x011EF5B0  token=0x60001AD  Newtonsoft.Json.JsonContainerType Peek()
  RVA=0x03779F50  token=0x60001AE  System.Void Close()
  RVA=0x053924C4  token=0x60001AF  System.Void WriteStartObject()
  RVA=0x047A0930  token=0x60001B0  System.Void WriteEndObject()
  RVA=0x053924FC  token=0x60001B1  System.Void WriteStartArray()
  RVA=0x047A0920  token=0x60001B2  System.Void WriteEndArray()
  RVA=0x0A8ADD7C  token=0x60001B3  System.Void WriteStartConstructor(System.String name)
  RVA=0x0A8ADC50  token=0x60001B4  System.Void WriteEndConstructor()
  RVA=0x05392558  token=0x60001B5  System.Void WritePropertyName(System.String name)
  RVA=0x04CA3170  token=0x60001B6  System.Void WritePropertyName(System.String name, System.Boolean escape)
  RVA=0x0A8ADC60  token=0x60001B7  System.Void WriteEnd()
  RVA=0x05392F1C  token=0x60001B8  System.Void WriteToken(Newtonsoft.Json.JsonReader reader)
  RVA=0x03424D10  token=0x60001B9  System.Void WriteToken(Newtonsoft.Json.JsonReader reader, System.Boolean writeChildren)
  RVA=0x03424FF0  token=0x60001BA  System.Void WriteToken(Newtonsoft.Json.JsonToken token, System.Object value)
  RVA=0x03424E90  token=0x60001BB  System.Void WriteToken(Newtonsoft.Json.JsonReader reader, System.Boolean writeChildren, System.Boolean writeDateConstructorAsDate, System.Boolean writeComments)
  RVA=0x03424DE0  token=0x60001BC  System.Boolean IsWriteTokenIncomplete(Newtonsoft.Json.JsonReader reader, System.Boolean writeChildren, System.Int32 initialDepth)
  RVA=0x03425290  token=0x60001BD  System.Int32 CalculateWriteTokenInitialDepth(Newtonsoft.Json.JsonReader reader)
  RVA=0x03424E30  token=0x60001BE  System.Int32 CalculateWriteTokenFinalDepth(Newtonsoft.Json.JsonReader reader)
  RVA=0x0A8ADBA8  token=0x60001BF  System.Void WriteConstructorDate(Newtonsoft.Json.JsonReader reader)
  RVA=0x0A8ADC6C  token=0x60001C0  System.Void WriteEnd(Newtonsoft.Json.JsonContainerType type)
  RVA=0x03779F60  token=0x60001C1  System.Void AutoCompleteAll()
  RVA=0x03637D60  token=0x60001C2  Newtonsoft.Json.JsonToken GetCloseTokenForType(Newtonsoft.Json.JsonContainerType type)
  RVA=0x03637C80  token=0x60001C3  System.Void AutoCompleteClose(Newtonsoft.Json.JsonContainerType type)
  RVA=0x03637D20  token=0x60001C4  System.Int32 CalculateLevelsToComplete(Newtonsoft.Json.JsonContainerType type)
  RVA=0x03637E70  token=0x60001C5  System.Void UpdateCurrentState()
  RVA=0x041E1670  token=0x60001C6  System.Void WriteEnd(Newtonsoft.Json.JsonToken token)
  RVA=0x041E1670  token=0x60001C7  System.Void WriteIndent()
  RVA=0x041E1670  token=0x60001C8  System.Void WriteValueDelimiter()
  RVA=0x041E1670  token=0x60001C9  System.Void WriteIndentSpace()
  RVA=0x037793A0  token=0x60001CA  System.Void AutoComplete(Newtonsoft.Json.JsonToken tokenBeingWritten)
  RVA=0x0A8ADD2C  token=0x60001CB  System.Void WriteNull()
  RVA=0x0A8ADD8C  token=0x60001CC  System.Void WriteUndefined()
  RVA=0x041E1670  token=0x60001CD  System.Void WriteRaw(System.String json)
  RVA=0x0A8ADD3C  token=0x60001CE  System.Void WriteRawValue(System.String json)
  RVA=0x05392548  token=0x60001CF  System.Void WriteValue(System.String value)
  RVA=0x05392528  token=0x60001D0  System.Void WriteValue(System.Int32 value)
  RVA=0x05392528  token=0x60001D1  System.Void WriteValue(System.UInt32 value)
  RVA=0x05392528  token=0x60001D2  System.Void WriteValue(System.Int64 value)
  RVA=0x05392528  token=0x60001D3  System.Void WriteValue(System.UInt64 value)
  RVA=0x0A8ADF58  token=0x60001D4  System.Void WriteValue(System.Single value)
  RVA=0x0A8ADF58  token=0x60001D5  System.Void WriteValue(System.Double value)
  RVA=0x05392560  token=0x60001D6  System.Void WriteValue(System.Boolean value)
  RVA=0x05392528  token=0x60001D7  System.Void WriteValue(System.Int16 value)
  RVA=0x05392528  token=0x60001D8  System.Void WriteValue(System.UInt16 value)
  RVA=0x05392548  token=0x60001D9  System.Void WriteValue(System.Char value)
  RVA=0x05392528  token=0x60001DA  System.Void WriteValue(System.Byte value)
  RVA=0x05392528  token=0x60001DB  System.Void WriteValue(System.SByte value)
  RVA=0x0A8ADF58  token=0x60001DC  System.Void WriteValue(System.Decimal value)
  RVA=0x0A8ADE08  token=0x60001DD  System.Void WriteValue(System.DateTime value)
  RVA=0x0A8ADE08  token=0x60001DE  System.Void WriteValue(System.DateTimeOffset value)
  RVA=0x05392548  token=0x60001DF  System.Void WriteValue(System.Guid value)
  RVA=0x05392548  token=0x60001E0  System.Void WriteValue(System.TimeSpan value)
  RVA=0x0A8AE52C  token=0x60001E1  System.Void WriteValue(System.Nullable<System.Int32> value)
  RVA=0x0A8AE034  token=0x60001E2  System.Void WriteValue(System.Nullable<System.UInt32> value)
  RVA=0x0A8AE2B4  token=0x60001E3  System.Void WriteValue(System.Nullable<System.Int64> value)
  RVA=0x0A8AE174  token=0x60001E4  System.Void WriteValue(System.Nullable<System.UInt64> value)
  RVA=0x0A8ADF68  token=0x60001E5  System.Void WriteValue(System.Nullable<System.Single> value)
  RVA=0x0A8AE0A0  token=0x60001E6  System.Void WriteValue(System.Nullable<System.Double> value)
  RVA=0x0A8AE48C  token=0x60001E7  System.Void WriteValue(System.Nullable<System.Boolean> value)
  RVA=0x0A8AE1DC  token=0x60001E8  System.Void WriteValue(System.Nullable<System.Int16> value)
  RVA=0x0A8AE604  token=0x60001E9  System.Void WriteValue(System.Nullable<System.UInt16> value)
  RVA=0x0A8ADD9C  token=0x60001EA  System.Void WriteValue(System.Nullable<System.Char> value)
  RVA=0x0A8AE108  token=0x60001EB  System.Void WriteValue(System.Nullable<System.Byte> value)
  RVA=0x0A8ADEF0  token=0x60001EC  System.Void WriteValue(System.Nullable<System.SByte> value)
  RVA=0x0A8ADE80  token=0x60001ED  System.Void WriteValue(System.Nullable<System.Decimal> value)
  RVA=0x0A8ADE18  token=0x60001EE  System.Void WriteValue(System.Nullable<System.DateTime> value)
  RVA=0x0A8AE248  token=0x60001EF  System.Void WriteValue(System.Nullable<System.DateTimeOffset> value)
  RVA=0x0A8AE598  token=0x60001F0  System.Void WriteValue(System.Nullable<System.Guid> value)
  RVA=0x0A8ADFD4  token=0x60001F1  System.Void WriteValue(System.Nullable<System.TimeSpan> value)
  RVA=0x0A8AE4F8  token=0x60001F2  System.Void WriteValue(System.Byte[] value)
  RVA=0x0A8AE31C  token=0x60001F3  System.Void WriteValue(System.Uri value)
  RVA=0x0A8AE390  token=0x60001F4  System.Void WriteValue(System.Object value)
  RVA=0x0A8AD894  token=0x60001F5  System.Void WriteComment(System.String text)
  RVA=0x042EF170  token=0x60001F6  System.Void System.IDisposable.Dispose()
  RVA=0x042EF1B0  token=0x60001F7  System.Void Dispose(System.Boolean disposing)
  RVA=0x0377F170  token=0x60001F8  System.Void WriteValue(Newtonsoft.Json.JsonWriter writer, Newtonsoft.Json.Utilities.PrimitiveTypeCode typeCode, System.Object value)
  RVA=0x0A8AD8A4  token=0x60001F9  System.Void ResolveConvertibleValue(System.IConvertible convertible, Newtonsoft.Json.Utilities.PrimitiveTypeCode& typeCode, System.Object& value)
  RVA=0x0A8AD7F4  token=0x60001FA  Newtonsoft.Json.JsonWriterException CreateUnsupportedTypeException(Newtonsoft.Json.JsonWriter writer, System.Object value)
  RVA=0x0A8AD99C  token=0x60001FB  System.Void SetWriteState(Newtonsoft.Json.JsonToken token, System.Object value)
  RVA=0x05397C18  token=0x60001FC  System.Void InternalWriteEnd(Newtonsoft.Json.JsonContainerType container)
  RVA=0x03779340  token=0x60001FD  System.Void InternalWritePropertyName(System.String name)
  RVA=0x041E1670  token=0x60001FE  System.Void InternalWriteRaw()
  RVA=0x03778E90  token=0x60001FF  System.Void InternalWriteStart(Newtonsoft.Json.JsonToken token, Newtonsoft.Json.JsonContainerType container)
  RVA=0x03779370  token=0x6000200  System.Void InternalWriteValue(Newtonsoft.Json.JsonToken token)
  RVA=0x0A8AD894  token=0x6000201  System.Void InternalWriteComment()
END_CLASS

CLASS: Newtonsoft.Json.JsonWriterException
TYPE:  class
TOKEN: 0x2000032
SIZE:  0x98
EXTENDS: Newtonsoft.Json.JsonException
FIELDS:
  private   readonly System.String                   <Path>k__BackingField  // 0x90
METHODS:
  RVA=0x0A8A78C8  token=0x6000202  System.Void .ctor()
  RVA=0x0A8A7928  token=0x6000203  System.Void .ctor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
  RVA=0x0A8B7E20  token=0x6000204  System.Void .ctor(System.String message, System.String path, System.Exception innerException)
  RVA=0x0A8B7DE0  token=0x6000205  Newtonsoft.Json.JsonWriterException Create(Newtonsoft.Json.JsonWriter writer, System.String message, System.Exception ex)
  RVA=0x0A8B7D30  token=0x6000206  Newtonsoft.Json.JsonWriterException Create(System.String path, System.String message, System.Exception ex)
END_CLASS

CLASS: Newtonsoft.Json.MemberSerialization
TYPE:  sealed struct
TOKEN: 0x2000033
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.MemberSerializationOptOut  // const
  public    static  Newtonsoft.Json.MemberSerializationOptIn  // const
  public    static  Newtonsoft.Json.MemberSerializationFields  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.MetadataPropertyHandling
TYPE:  sealed struct
TOKEN: 0x2000034
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.MetadataPropertyHandlingDefault  // const
  public    static  Newtonsoft.Json.MetadataPropertyHandlingReadAhead  // const
  public    static  Newtonsoft.Json.MetadataPropertyHandlingIgnore  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.MissingMemberHandling
TYPE:  sealed struct
TOKEN: 0x2000035
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.MissingMemberHandlingIgnore  // const
  public    static  Newtonsoft.Json.MissingMemberHandlingError  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.NullValueHandling
TYPE:  sealed struct
TOKEN: 0x2000036
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.NullValueHandlingInclude  // const
  public    static  Newtonsoft.Json.NullValueHandlingIgnore  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.ObjectCreationHandling
TYPE:  sealed struct
TOKEN: 0x2000037
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.ObjectCreationHandlingAuto  // const
  public    static  Newtonsoft.Json.ObjectCreationHandlingReuse  // const
  public    static  Newtonsoft.Json.ObjectCreationHandlingReplace  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.PreserveReferencesHandling
TYPE:  sealed struct
TOKEN: 0x2000038
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.PreserveReferencesHandlingNone  // const
  public    static  Newtonsoft.Json.PreserveReferencesHandlingObjects  // const
  public    static  Newtonsoft.Json.PreserveReferencesHandlingArrays  // const
  public    static  Newtonsoft.Json.PreserveReferencesHandlingAll  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.ReferenceLoopHandling
TYPE:  sealed struct
TOKEN: 0x2000039
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.ReferenceLoopHandlingError  // const
  public    static  Newtonsoft.Json.ReferenceLoopHandlingIgnore  // const
  public    static  Newtonsoft.Json.ReferenceLoopHandlingSerialize  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.Required
TYPE:  sealed struct
TOKEN: 0x200003A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.Required        Default  // const
  public    static  Newtonsoft.Json.Required        AllowNull  // const
  public    static  Newtonsoft.Json.Required        Always  // const
  public    static  Newtonsoft.Json.Required        DisallowNull  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.StringEscapeHandling
TYPE:  sealed struct
TOKEN: 0x200003B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.StringEscapeHandlingDefault  // const
  public    static  Newtonsoft.Json.StringEscapeHandlingEscapeNonAscii  // const
  public    static  Newtonsoft.Json.StringEscapeHandlingEscapeHtml  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.TypeNameAssemblyFormatHandling
TYPE:  sealed struct
TOKEN: 0x200003C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.TypeNameAssemblyFormatHandlingSimple  // const
  public    static  Newtonsoft.Json.TypeNameAssemblyFormatHandlingFull  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.TypeNameHandling
TYPE:  sealed struct
TOKEN: 0x200003D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.TypeNameHandlingNone  // const
  public    static  Newtonsoft.Json.TypeNameHandlingObjects  // const
  public    static  Newtonsoft.Json.TypeNameHandlingArrays  // const
  public    static  Newtonsoft.Json.TypeNameHandlingAll  // const
  public    static  Newtonsoft.Json.TypeNameHandlingAuto  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.WriteState
TYPE:  sealed struct
TOKEN: 0x200003E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.WriteState      Error  // const
  public    static  Newtonsoft.Json.WriteState      Closed  // const
  public    static  Newtonsoft.Json.WriteState      Object  // const
  public    static  Newtonsoft.Json.WriteState      Array  // const
  public    static  Newtonsoft.Json.WriteState      Constructor  // const
  public    static  Newtonsoft.Json.WriteState      Property  // const
  public    static  Newtonsoft.Json.WriteState      Start  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.Bson.BsonObjectId
TYPE:  class
TOKEN: 0x2000122
SIZE:  0x18
FIELDS:
  private   readonly System.Byte[]                   <Value>k__BackingField  // 0x10
PROPERTIES:
  Value  get=0x02B2ECC0
METHODS:
  RVA=0x0A8D36E0  token=0x6000960  System.Void .ctor(System.Byte[] value)
END_CLASS

CLASS: Newtonsoft.Json.Bson.BsonToken
TYPE:  abstract class
TOKEN: 0x2000123
SIZE:  0x18
FIELDS:
  private           Newtonsoft.Json.Bson.BsonToken  <Parent>k__BackingField  // 0x10
PROPERTIES:
  Type  get=-1  // abstract
  Parent  set=0x053908C0
METHODS:
  RVA=0x041E1670  token=0x6000963  System.Void .ctor()
END_CLASS

CLASS: Newtonsoft.Json.Bson.BsonObject
TYPE:  class
TOKEN: 0x2000124
SIZE:  0x20
EXTENDS: Newtonsoft.Json.Bson.BsonToken
FIELDS:
  private   readonly System.Collections.Generic.List<Newtonsoft.Json.Bson.BsonProperty>_children  // 0x18
PROPERTIES:
  Type  get=0x04D91FD0
METHODS:
  RVA=0x0A8D37E0  token=0x6000964  System.Void Add(System.String name, Newtonsoft.Json.Bson.BsonToken token)
END_CLASS

CLASS: Newtonsoft.Json.Bson.BsonArray
TYPE:  class
TOKEN: 0x2000125
SIZE:  0x20
EXTENDS: Newtonsoft.Json.Bson.BsonToken
FIELDS:
  private   readonly System.Collections.Generic.List<Newtonsoft.Json.Bson.BsonToken>_children  // 0x18
PROPERTIES:
  Type  get=0x04D92120
METHODS:
  RVA=0x0A8D3484  token=0x6000966  System.Void Add(Newtonsoft.Json.Bson.BsonToken token)
END_CLASS

CLASS: Newtonsoft.Json.Bson.BsonValue
TYPE:  class
TOKEN: 0x2000126
SIZE:  0x28
EXTENDS: Newtonsoft.Json.Bson.BsonToken
FIELDS:
  private   readonly System.Object                   _value  // 0x18
  private   readonly Newtonsoft.Json.Bson.BsonType   _type  // 0x20
PROPERTIES:
  Type  get=0x04D866B0
METHODS:
  RVA=0x094E5F8C  token=0x6000968  System.Void .ctor(System.Object value, Newtonsoft.Json.Bson.BsonType type)
END_CLASS

CLASS: Newtonsoft.Json.Bson.BsonString
TYPE:  class
TOKEN: 0x2000127
SIZE:  0x30
EXTENDS: Newtonsoft.Json.Bson.BsonValue
FIELDS:
  private   readonly System.Boolean                  <IncludeLength>k__BackingField  // 0x28
METHODS:
  RVA=0x0A8D3990  token=0x600096A  System.Void .ctor(System.Object value, System.Boolean includeLength)
END_CLASS

CLASS: Newtonsoft.Json.Bson.BsonRegex
TYPE:  class
TOKEN: 0x2000128
SIZE:  0x28
EXTENDS: Newtonsoft.Json.Bson.BsonToken
FIELDS:
  private           Newtonsoft.Json.Bson.BsonString <Pattern>k__BackingField  // 0x18
  private           Newtonsoft.Json.Bson.BsonString <Options>k__BackingField  // 0x20
PROPERTIES:
  Pattern  set=0x05392C40
  Options  set=0x05390F40
  Type  get=0x04DA5E70
METHODS:
  RVA=0x0A8D38D8  token=0x600096D  System.Void .ctor(System.String pattern, System.String options)
END_CLASS

CLASS: Newtonsoft.Json.Bson.BsonProperty
TYPE:  class
TOKEN: 0x2000129
SIZE:  0x20
FIELDS:
  private           Newtonsoft.Json.Bson.BsonString <Name>k__BackingField  // 0x10
  private           Newtonsoft.Json.Bson.BsonToken  <Value>k__BackingField  // 0x18
PROPERTIES:
  Name  set=0x053908C0
  Value  set=0x05392C40
METHODS:
  RVA=0x041E1670  token=0x6000971  System.Void .ctor()
END_CLASS

CLASS: Newtonsoft.Json.Bson.BsonType
TYPE:  sealed struct
TOKEN: 0x200012A
SIZE:  0x11
FIELDS:
  public            System.SByte                    value__  // 0x10
  public    static  Newtonsoft.Json.Bson.BsonType   Number  // const
  public    static  Newtonsoft.Json.Bson.BsonType   String  // const
  public    static  Newtonsoft.Json.Bson.BsonType   Object  // const
  public    static  Newtonsoft.Json.Bson.BsonType   Array  // const
  public    static  Newtonsoft.Json.Bson.BsonType   Binary  // const
  public    static  Newtonsoft.Json.Bson.BsonType   Undefined  // const
  public    static  Newtonsoft.Json.Bson.BsonType   Oid  // const
  public    static  Newtonsoft.Json.Bson.BsonType   Boolean  // const
  public    static  Newtonsoft.Json.Bson.BsonType   Date  // const
  public    static  Newtonsoft.Json.Bson.BsonType   Null  // const
  public    static  Newtonsoft.Json.Bson.BsonType   Regex  // const
  public    static  Newtonsoft.Json.Bson.BsonType   Reference  // const
  public    static  Newtonsoft.Json.Bson.BsonType   Code  // const
  public    static  Newtonsoft.Json.Bson.BsonType   Symbol  // const
  public    static  Newtonsoft.Json.Bson.BsonType   CodeWScope  // const
  public    static  Newtonsoft.Json.Bson.BsonType   Integer  // const
  public    static  Newtonsoft.Json.Bson.BsonType   TimeStamp  // const
  public    static  Newtonsoft.Json.Bson.BsonType   Long  // const
  public    static  Newtonsoft.Json.Bson.BsonType   MinKey  // const
  public    static  Newtonsoft.Json.Bson.BsonType   MaxKey  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.Bson.BsonWriter
TYPE:  class
TOKEN: 0x200012B
SIZE:  0x78
EXTENDS: Newtonsoft.Json.JsonWriter
FIELDS:
  private           Newtonsoft.Json.Bson.BsonToken  _root  // 0x60
  private           Newtonsoft.Json.Bson.BsonToken  _parent  // 0x68
  private           System.String                   _propertyName  // 0x70
METHODS:
  RVA=0x0A8D3B54  token=0x6000972  System.Void AddValue(System.Object value, Newtonsoft.Json.Bson.BsonType type)
  RVA=0x0A8D39B8  token=0x6000973  System.Void AddToken(Newtonsoft.Json.Bson.BsonToken token)
  RVA=0x0A8D3BDC  token=0x6000974  System.Void WriteObjectId(System.Byte[] value)
  RVA=0x0A8D3CC8  token=0x6000975  System.Void WriteRegex(System.String pattern, System.String options)
END_CLASS

CLASS: Newtonsoft.Json.Converters.BinaryConverter
TYPE:  class
TOKEN: 0x20000F9
SIZE:  0x10
EXTENDS: Newtonsoft.Json.JsonConverter
FIELDS:
  private   static  System.String                   BinaryTypeName  // const
  private   static  System.String                   BinaryToArrayName  // const
  private   static  Newtonsoft.Json.Utilities.ReflectionObject_reflectionObject  // static @ 0x0
METHODS:
  RVA=0x0A8C2EAC  token=0x6000834  System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x0A8C26EC  token=0x6000835  System.Byte[] GetByteArray(System.Object value)
  RVA=0x0A8C2584  token=0x6000836  System.Void EnsureReflectionObject(System.Type t)
  RVA=0x0A8C2AC8  token=0x6000837  System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x0A8C28B0  token=0x6000838  System.Byte[] ReadByteArray(Newtonsoft.Json.JsonReader reader)
  RVA=0x04048480  token=0x6000839  System.Boolean CanConvert(System.Type objectType)
  RVA=0x041E1670  token=0x600083A  System.Void .ctor()
END_CLASS

CLASS: Newtonsoft.Json.Converters.BsonObjectIdConverter
TYPE:  class
TOKEN: 0x20000FA
SIZE:  0x10
EXTENDS: Newtonsoft.Json.JsonConverter
FIELDS:
METHODS:
  RVA=0x0A8D3640  token=0x600083B  System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x0A8D34EC  token=0x600083C  System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x04239130  token=0x600083D  System.Boolean CanConvert(System.Type objectType)
  RVA=0x041E1670  token=0x600083E  System.Void .ctor()
END_CLASS

CLASS: Newtonsoft.Json.Converters.CustomCreationConverter`1
TYPE:  abstract class
TOKEN: 0x20000FB
EXTENDS: Newtonsoft.Json.JsonConverter
FIELDS:
PROPERTIES:
  CanWrite  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600083F  System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer)
  RVA=-1  // not resolved  token=0x6000840  System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer)
  RVA=-1  // abstract  token=0x6000841  T Create(System.Type objectType)
  RVA=-1  // not resolved  token=0x6000842  System.Boolean CanConvert(System.Type objectType)
  RVA=-1  // not resolved  token=0x6000844  System.Void .ctor()
END_CLASS

CLASS: Newtonsoft.Json.Converters.DataSetConverter
TYPE:  class
TOKEN: 0x20000FC
SIZE:  0x10
EXTENDS: Newtonsoft.Json.JsonConverter
FIELDS:
METHODS:
  RVA=0x0A8D3FDC  token=0x6000845  System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x0A8D3DBC  token=0x6000846  System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x041D0660  token=0x6000847  System.Boolean CanConvert(System.Type valueType)
  RVA=0x041E1670  token=0x6000848  System.Void .ctor()
END_CLASS

CLASS: Newtonsoft.Json.Converters.DataTableConverter
TYPE:  class
TOKEN: 0x20000FD
SIZE:  0x10
EXTENDS: Newtonsoft.Json.JsonConverter
FIELDS:
METHODS:
  RVA=0x0A8D4AF0  token=0x6000849  System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x0A8D4898  token=0x600084A  System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x0A8D42D8  token=0x600084B  System.Void CreateRow(Newtonsoft.Json.JsonReader reader, System.Data.DataTable dt, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x0A8D46C4  token=0x600084C  System.Type GetColumnDataType(Newtonsoft.Json.JsonReader reader)
  RVA=0x041D0600  token=0x600084D  System.Boolean CanConvert(System.Type valueType)
  RVA=0x041E1670  token=0x600084E  System.Void .ctor()
END_CLASS

CLASS: Newtonsoft.Json.Converters.DateTimeConverterBase
TYPE:  abstract class
TOKEN: 0x20000FE
SIZE:  0x10
EXTENDS: Newtonsoft.Json.JsonConverter
FIELDS:
METHODS:
  RVA=0x0A8D4FA0  token=0x600084F  System.Boolean CanConvert(System.Type objectType)
  RVA=0x041E1670  token=0x6000850  System.Void .ctor()
END_CLASS

CLASS: Newtonsoft.Json.Converters.DiscriminatedUnionConverter
TYPE:  class
TOKEN: 0x20000FF
SIZE:  0x10
EXTENDS: Newtonsoft.Json.JsonConverter
FIELDS:
  private   static  System.String                   CasePropertyName  // const
  private   static  System.String                   FieldsPropertyName  // const
  private   static readonly Newtonsoft.Json.Utilities.ThreadSafeStore<System.Type,Newtonsoft.Json.Converters.DiscriminatedUnionConverter.Union>UnionCache  // static @ 0x0
  private   static readonly Newtonsoft.Json.Utilities.ThreadSafeStore<System.Type,System.Type>UnionTypeLookupCache  // static @ 0x8
METHODS:
  RVA=0x0A8D5094  token=0x6000851  System.Type CreateUnionTypeLookup(System.Type t)
  RVA=0x0A8D51B8  token=0x6000852  Newtonsoft.Json.Converters.DiscriminatedUnionConverter.Union CreateUnion(System.Type t)
  RVA=0x0A8D5CD8  token=0x6000853  System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x0A8D5640  token=0x6000854  System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x03FAC670  token=0x6000855  System.Boolean CanConvert(System.Type objectType)
  RVA=0x041E1670  token=0x6000856  System.Void .ctor()
  RVA=0x0489B2C0  token=0x6000857  System.Void .cctor()
END_CLASS

CLASS: Newtonsoft.Json.Converters.EntityKeyMemberConverter
TYPE:  class
TOKEN: 0x2000104
SIZE:  0x10
EXTENDS: Newtonsoft.Json.JsonConverter
FIELDS:
  private   static  System.String                   EntityKeyMemberFullTypeName  // const
  private   static  System.String                   KeyPropertyName  // const
  private   static  System.String                   TypePropertyName  // const
  private   static  System.String                   ValuePropertyName  // const
  private   static  Newtonsoft.Json.Utilities.ReflectionObject_reflectionObject  // static @ 0x0
METHODS:
  RVA=0x0A8D64C8  token=0x600085E  System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x0A8D6188  token=0x600085F  System.Void ReadAndAssertProperty(Newtonsoft.Json.JsonReader reader, System.String propertyName)
  RVA=0x0A8D6284  token=0x6000860  System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x0A8D60A4  token=0x6000861  System.Void EnsureReflectionObject(System.Type objectType)
  RVA=0x040B9A90  token=0x6000862  System.Boolean CanConvert(System.Type objectType)
  RVA=0x041E1670  token=0x6000863  System.Void .ctor()
END_CLASS

CLASS: Newtonsoft.Json.Converters.ExpandoObjectConverter
TYPE:  class
TOKEN: 0x2000105
SIZE:  0x10
EXTENDS: Newtonsoft.Json.JsonConverter
FIELDS:
PROPERTIES:
  CanWrite  get=0x012081B0
METHODS:
  RVA=0x041E1670  token=0x6000864  System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x0A8D6788  token=0x6000865  System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x0A8D6A44  token=0x6000866  System.Object ReadValue(Newtonsoft.Json.JsonReader reader)
  RVA=0x0A8D6790  token=0x6000867  System.Object ReadList(Newtonsoft.Json.JsonReader reader)
  RVA=0x0A8D68AC  token=0x6000868  System.Object ReadObject(Newtonsoft.Json.JsonReader reader)
  RVA=0x042390E0  token=0x6000869  System.Boolean CanConvert(System.Type objectType)
  RVA=0x041E1670  token=0x600086B  System.Void .ctor()
END_CLASS

CLASS: Newtonsoft.Json.Converters.IsoDateTimeConverter
TYPE:  class
TOKEN: 0x2000106
SIZE:  0x28
EXTENDS: Newtonsoft.Json.Converters.DateTimeConverterBase
FIELDS:
  private   static  System.String                   DefaultDateTimeFormat  // const
  private           System.Globalization.DateTimeStyles_dateTimeStyles  // 0x10
  private           System.String                   _dateTimeFormat  // 0x18
  private           System.Globalization.CultureInfo_culture  // 0x20
PROPERTIES:
  DateTimeStyles  get=0x02B2E2D0  set=0x014F51F0
  DateTimeFormat  get=0x0A8D727C  set=0x0A8D72C4
  Culture  get=0x0A8D7228  set=0x05390F40
METHODS:
  RVA=0x0A8D6FEC  token=0x6000872  System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x0A8D6BB4  token=0x6000873  System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x04DA5E80  token=0x6000874  System.Void .ctor()
END_CLASS

CLASS: Newtonsoft.Json.Converters.JavaScriptDateTimeConverter
TYPE:  class
TOKEN: 0x2000107
SIZE:  0x10
EXTENDS: Newtonsoft.Json.Converters.DateTimeConverterBase
FIELDS:
METHODS:
  RVA=0x0A8D7628  token=0x6000875  System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x0A8D730C  token=0x6000876  System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x041E1670  token=0x6000877  System.Void .ctor()
END_CLASS

CLASS: Newtonsoft.Json.Converters.KeyValuePairConverter
TYPE:  class
TOKEN: 0x2000108
SIZE:  0x10
EXTENDS: Newtonsoft.Json.JsonConverter
FIELDS:
  private   static  System.String                   KeyName  // const
  private   static  System.String                   ValueName  // const
  private   static readonly Newtonsoft.Json.Utilities.ThreadSafeStore<System.Type,Newtonsoft.Json.Utilities.ReflectionObject>ReflectionObjectPerType  // static @ 0x0
METHODS:
  RVA=0x0A8D77C0  token=0x6000878  Newtonsoft.Json.Utilities.ReflectionObject InitializeReflectionObject(System.Type t)
  RVA=0x0A8D7C9C  token=0x6000879  System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x0A8D7940  token=0x600087A  System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x035CF760  token=0x600087B  System.Boolean CanConvert(System.Type objectType)
  RVA=0x041E1670  token=0x600087C  System.Void .ctor()
  RVA=0x04CAEFE0  token=0x600087D  System.Void .cctor()
END_CLASS

CLASS: Newtonsoft.Json.Converters.RegexConverter
TYPE:  class
TOKEN: 0x2000109
SIZE:  0x10
EXTENDS: Newtonsoft.Json.JsonConverter
FIELDS:
  private   static  System.String                   PatternName  // const
  private   static  System.String                   OptionsName  // const
METHODS:
  RVA=0x0A8D862C  token=0x600087E  System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x02FB41C0  token=0x600087F  System.Boolean HasFlag(System.Text.RegularExpressions.RegexOptions options, System.Text.RegularExpressions.RegexOptions flag)
  RVA=0x0A8D839C  token=0x6000880  System.Void WriteBson(Newtonsoft.Json.Bson.BsonWriter writer, System.Text.RegularExpressions.Regex regex)
  RVA=0x0A8D84B8  token=0x6000881  System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Text.RegularExpressions.Regex regex, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x0A8D7F1C  token=0x6000882  System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x0A8D824C  token=0x6000883  System.Object ReadRegexString(Newtonsoft.Json.JsonReader reader)
  RVA=0x0A8D7FBC  token=0x6000884  System.Text.RegularExpressions.Regex ReadRegexObject(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x041D06C0  token=0x6000885  System.Boolean CanConvert(System.Type objectType)
  RVA=0x0A8D7EB8  token=0x6000886  System.Boolean IsRegex(System.Type objectType)
  RVA=0x041E1670  token=0x6000887  System.Void .ctor()
END_CLASS

CLASS: Newtonsoft.Json.Converters.StringEnumConverter
TYPE:  class
TOKEN: 0x200010A
SIZE:  0x20
EXTENDS: Newtonsoft.Json.JsonConverter
FIELDS:
  private           Newtonsoft.Json.Serialization.NamingStrategy<NamingStrategy>k__BackingField  // 0x10
  private           System.Boolean                  <AllowIntegerValues>k__BackingField  // 0x18
PROPERTIES:
  CamelCaseText  get=0x0A8D903C  set=0x0A8D9080
  NamingStrategy  get=0x02B2ECC0  set=0x053908C0
  AllowIntegerValues  get=0x04D86340  set=0x04D86350
METHODS:
  RVA=0x04D8EB60  token=0x600088E  System.Void .ctor()
  RVA=0x0A8D8D68  token=0x600088F  System.Void .ctor(System.Boolean camelCaseText)
  RVA=0x0A8D8D40  token=0x6000890  System.Void .ctor(Newtonsoft.Json.Serialization.NamingStrategy namingStrategy, System.Boolean allowIntegerValues)
  RVA=0x0A8D8DCC  token=0x6000891  System.Void .ctor(System.Type namingStrategyType)
  RVA=0x0A8D8E8C  token=0x6000892  System.Void .ctor(System.Type namingStrategyType, System.Object[] namingStrategyParameters)
  RVA=0x0A8D8F5C  token=0x6000893  System.Void .ctor(System.Type namingStrategyType, System.Object[] namingStrategyParameters, System.Boolean allowIntegerValues)
  RVA=0x0A8D8B84  token=0x6000894  System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x0A8D8750  token=0x6000895  System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x0A8D86F4  token=0x6000896  System.Boolean CanConvert(System.Type objectType)
END_CLASS

CLASS: Newtonsoft.Json.Converters.UnixDateTimeConverter
TYPE:  class
TOKEN: 0x200010B
SIZE:  0x18
EXTENDS: Newtonsoft.Json.Converters.DateTimeConverterBase
FIELDS:
  private   static readonly System.DateTime                 UnixEpoch  // static @ 0x0
  private           System.Boolean                  <AllowPreEpoch>k__BackingField  // 0x10
PROPERTIES:
  AllowPreEpoch  get=0x015EFCE0  set=0x04D86130
METHODS:
  RVA=0x04D8CCB0  token=0x6000899  System.Void .ctor()
  RVA=0x04D86130  token=0x600089A  System.Void .ctor(System.Boolean allowPreEpoch)
  RVA=0x0A8D95A4  token=0x600089B  System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x0A8D9178  token=0x600089C  System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x0A8D97F8  token=0x600089D  System.Void .cctor()
END_CLASS

CLASS: Newtonsoft.Json.Converters.VersionConverter
TYPE:  class
TOKEN: 0x200010C
SIZE:  0x10
EXTENDS: Newtonsoft.Json.JsonConverter
FIELDS:
METHODS:
  RVA=0x0A8D9B4C  token=0x600089E  System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x0A8D98D4  token=0x600089F  System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x0A8D9870  token=0x60008A0  System.Boolean CanConvert(System.Type objectType)
  RVA=0x041E1670  token=0x60008A1  System.Void .ctor()
END_CLASS

CLASS: Newtonsoft.Json.Converters.XmlDocumentWrapper
TYPE:  class
TOKEN: 0x200010D
SIZE:  0x30
EXTENDS: Newtonsoft.Json.Converters.XmlNodeWrapper
IMPLEMENTS: Newtonsoft.Json.Converters.IXmlDocument Newtonsoft.Json.Converters.IXmlNode
FIELDS:
  private   readonly System.Xml.XmlDocument          _document  // 0x28
PROPERTIES:
  DocumentElement  get=0x0A8DC138
METHODS:
  RVA=0x0A8DB9D8  token=0x60008A2  System.Void .ctor(System.Xml.XmlDocument document)
  RVA=0x0A8DBC74  token=0x60008A3  Newtonsoft.Json.Converters.IXmlNode CreateComment(System.String data)
  RVA=0x0A8DBF00  token=0x60008A4  Newtonsoft.Json.Converters.IXmlNode CreateTextNode(System.String text)
  RVA=0x0A8DBBF8  token=0x60008A5  Newtonsoft.Json.Converters.IXmlNode CreateCDataSection(System.String data)
  RVA=0x0A8DBF7C  token=0x60008A6  Newtonsoft.Json.Converters.IXmlNode CreateWhitespace(System.String text)
  RVA=0x0A8DBE84  token=0x60008A7  Newtonsoft.Json.Converters.IXmlNode CreateSignificantWhitespace(System.String text)
  RVA=0x0A8DBFF8  token=0x60008A8  Newtonsoft.Json.Converters.IXmlNode CreateXmlDeclaration(System.String version, System.String encoding, System.String standalone)
  RVA=0x0A8DC094  token=0x60008A9  Newtonsoft.Json.Converters.IXmlNode CreateXmlDocumentType(System.String name, System.String publicId, System.String systemId, System.String internalSubset)
  RVA=0x0A8DBDF8  token=0x60008AA  Newtonsoft.Json.Converters.IXmlNode CreateProcessingInstruction(System.String target, System.String data)
  RVA=0x0A8DBCF0  token=0x60008AB  Newtonsoft.Json.Converters.IXmlElement CreateElement(System.String elementName)
  RVA=0x0A8DBD6C  token=0x60008AC  Newtonsoft.Json.Converters.IXmlElement CreateElement(System.String qualifiedName, System.String namespaceUri)
  RVA=0x0A8DBAC0  token=0x60008AD  Newtonsoft.Json.Converters.IXmlNode CreateAttribute(System.String name, System.String value)
  RVA=0x0A8DBB54  token=0x60008AE  Newtonsoft.Json.Converters.IXmlNode CreateAttribute(System.String qualifiedName, System.String namespaceUri, System.String value)
END_CLASS

CLASS: Newtonsoft.Json.Converters.XmlElementWrapper
TYPE:  class
TOKEN: 0x200010E
SIZE:  0x30
EXTENDS: Newtonsoft.Json.Converters.XmlNodeWrapper
IMPLEMENTS: Newtonsoft.Json.Converters.IXmlElement Newtonsoft.Json.Converters.IXmlNode
FIELDS:
  private   readonly System.Xml.XmlElement           _element  // 0x28
PROPERTIES:
  IsEmpty  get=0x0A8DC268
METHODS:
  RVA=0x0A8DB9D8  token=0x60008B0  System.Void .ctor(System.Xml.XmlElement element)
  RVA=0x0A8DC1E4  token=0x60008B1  System.Void SetAttributeNode(Newtonsoft.Json.Converters.IXmlNode attribute)
  RVA=0x0A8DC1C4  token=0x60008B2  System.String GetPrefixOfNamespace(System.String namespaceUri)
END_CLASS

CLASS: Newtonsoft.Json.Converters.XmlDeclarationWrapper
TYPE:  class
TOKEN: 0x200010F
SIZE:  0x30
EXTENDS: Newtonsoft.Json.Converters.XmlNodeWrapper
IMPLEMENTS: Newtonsoft.Json.Converters.IXmlDeclaration Newtonsoft.Json.Converters.IXmlNode
FIELDS:
  private   readonly System.Xml.XmlDeclaration       _declaration  // 0x28
PROPERTIES:
  Version  get=0x0A8DBA38
  Encoding  get=0x0A8DBA00
  Standalone  get=0x0A8DBA1C
METHODS:
  RVA=0x0A8DB9D8  token=0x60008B4  System.Void .ctor(System.Xml.XmlDeclaration declaration)
END_CLASS

CLASS: Newtonsoft.Json.Converters.XmlDocumentTypeWrapper
TYPE:  class
TOKEN: 0x2000110
SIZE:  0x30
EXTENDS: Newtonsoft.Json.Converters.XmlNodeWrapper
IMPLEMENTS: Newtonsoft.Json.Converters.IXmlDocumentType Newtonsoft.Json.Converters.IXmlNode
FIELDS:
  private   readonly System.Xml.XmlDocumentType      _documentType  // 0x28
PROPERTIES:
  Name  get=0x0A8DBA9C
  System  get=0x0A8DBA1C
  Public  get=0x0A8DBA00
  InternalSubset  get=0x0A8DBA54
  LocalName  get=0x0A8DBA70
METHODS:
  RVA=0x0A8DB9D8  token=0x60008B8  System.Void .ctor(System.Xml.XmlDocumentType documentType)
END_CLASS

CLASS: Newtonsoft.Json.Converters.XmlNodeWrapper
TYPE:  class
TOKEN: 0x2000111
SIZE:  0x28
IMPLEMENTS: Newtonsoft.Json.Converters.IXmlNode
FIELDS:
  private   readonly System.Xml.XmlNode              _node  // 0x10
  private           System.Collections.Generic.List<Newtonsoft.Json.Converters.IXmlNode>_childNodes  // 0x18
  private           System.Collections.Generic.List<Newtonsoft.Json.Converters.IXmlNode>_attributes  // 0x20
PROPERTIES:
  WrappedNode  get=0x02B2ECC0
  NodeType  get=0x0A8E29E4
  LocalName  get=0x0A8E299C
  ChildNodes  get=0x0A8E2664
  Attributes  get=0x0A8E23C8
  HasAttributes  get=0x0A8E291C
  ParentNode  get=0x0A8E2A08
  Value  get=0x0A8E2A84  set=0x0A8E2AA8
  NamespaceUri  get=0x0A8E29C0
METHODS:
  RVA=0x053908C0  token=0x60008BE  System.Void .ctor(System.Xml.XmlNode node)
  RVA=0x0A8E225C  token=0x60008C3  Newtonsoft.Json.Converters.IXmlNode WrapNode(System.Xml.XmlNode node)
  RVA=0x0A8E21D4  token=0x60008C9  Newtonsoft.Json.Converters.IXmlNode AppendChild(Newtonsoft.Json.Converters.IXmlNode newChild)
END_CLASS

CLASS: Newtonsoft.Json.Converters.IXmlDocument
TYPE:  interface
TOKEN: 0x2000112
IMPLEMENTS: Newtonsoft.Json.Converters.IXmlNode
FIELDS:
PROPERTIES:
  DocumentElement  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x60008CB  Newtonsoft.Json.Converters.IXmlNode CreateComment(System.String text)
  RVA=-1  // abstract  token=0x60008CC  Newtonsoft.Json.Converters.IXmlNode CreateTextNode(System.String text)
  RVA=-1  // abstract  token=0x60008CD  Newtonsoft.Json.Converters.IXmlNode CreateCDataSection(System.String data)
  RVA=-1  // abstract  token=0x60008CE  Newtonsoft.Json.Converters.IXmlNode CreateWhitespace(System.String text)
  RVA=-1  // abstract  token=0x60008CF  Newtonsoft.Json.Converters.IXmlNode CreateSignificantWhitespace(System.String text)
  RVA=-1  // abstract  token=0x60008D0  Newtonsoft.Json.Converters.IXmlNode CreateXmlDeclaration(System.String version, System.String encoding, System.String standalone)
  RVA=-1  // abstract  token=0x60008D1  Newtonsoft.Json.Converters.IXmlNode CreateXmlDocumentType(System.String name, System.String publicId, System.String systemId, System.String internalSubset)
  RVA=-1  // abstract  token=0x60008D2  Newtonsoft.Json.Converters.IXmlNode CreateProcessingInstruction(System.String target, System.String data)
  RVA=-1  // abstract  token=0x60008D3  Newtonsoft.Json.Converters.IXmlElement CreateElement(System.String elementName)
  RVA=-1  // abstract  token=0x60008D4  Newtonsoft.Json.Converters.IXmlElement CreateElement(System.String qualifiedName, System.String namespaceUri)
  RVA=-1  // abstract  token=0x60008D5  Newtonsoft.Json.Converters.IXmlNode CreateAttribute(System.String name, System.String value)
  RVA=-1  // abstract  token=0x60008D6  Newtonsoft.Json.Converters.IXmlNode CreateAttribute(System.String qualifiedName, System.String namespaceUri, System.String value)
END_CLASS

CLASS: Newtonsoft.Json.Converters.IXmlDeclaration
TYPE:  interface
TOKEN: 0x2000113
IMPLEMENTS: Newtonsoft.Json.Converters.IXmlNode
FIELDS:
PROPERTIES:
  Version  get=-1  // abstract
  Encoding  get=-1  // abstract
  Standalone  get=-1  // abstract
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.Converters.IXmlDocumentType
TYPE:  interface
TOKEN: 0x2000114
IMPLEMENTS: Newtonsoft.Json.Converters.IXmlNode
FIELDS:
PROPERTIES:
  Name  get=-1  // abstract
  System  get=-1  // abstract
  Public  get=-1  // abstract
  InternalSubset  get=-1  // abstract
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.Converters.IXmlElement
TYPE:  interface
TOKEN: 0x2000115
IMPLEMENTS: Newtonsoft.Json.Converters.IXmlNode
FIELDS:
PROPERTIES:
  IsEmpty  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x60008DF  System.Void SetAttributeNode(Newtonsoft.Json.Converters.IXmlNode attribute)
  RVA=-1  // abstract  token=0x60008E0  System.String GetPrefixOfNamespace(System.String namespaceUri)
END_CLASS

CLASS: Newtonsoft.Json.Converters.IXmlNode
TYPE:  interface
TOKEN: 0x2000116
FIELDS:
PROPERTIES:
  NodeType  get=-1  // abstract
  LocalName  get=-1  // abstract
  ChildNodes  get=-1  // abstract
  Attributes  get=-1  // abstract
  ParentNode  get=-1  // abstract
  Value  get=-1  // abstract
  NamespaceUri  get=-1  // abstract
  WrappedNode  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x60008E8  Newtonsoft.Json.Converters.IXmlNode AppendChild(Newtonsoft.Json.Converters.IXmlNode newChild)
END_CLASS

CLASS: Newtonsoft.Json.Converters.XDeclarationWrapper
TYPE:  class
TOKEN: 0x2000117
SIZE:  0x20
EXTENDS: Newtonsoft.Json.Converters.XObjectWrapper
IMPLEMENTS: Newtonsoft.Json.Converters.IXmlDeclaration Newtonsoft.Json.Converters.IXmlNode
FIELDS:
  private   readonly System.Xml.Linq.XDeclaration    <Declaration>k__BackingField  // 0x18
PROPERTIES:
  Declaration  get=0x0385B100
  NodeType  get=0x04DA5E90
  Version  get=0x0A8DA4D0
  Encoding  get=0x0A8DA498
  Standalone  get=0x0A8DA4B4
METHODS:
  RVA=0x0A8DA46C  token=0x60008EC  System.Void .ctor(System.Xml.Linq.XDeclaration declaration)
END_CLASS

CLASS: Newtonsoft.Json.Converters.XDocumentTypeWrapper
TYPE:  class
TOKEN: 0x2000118
SIZE:  0x20
EXTENDS: Newtonsoft.Json.Converters.XObjectWrapper
IMPLEMENTS: Newtonsoft.Json.Converters.IXmlDocumentType Newtonsoft.Json.Converters.IXmlNode
FIELDS:
  private   readonly System.Xml.Linq.XDocumentType   _documentType  // 0x18
PROPERTIES:
  Name  get=0x0A8DA55C
  System  get=0x0A8DA594
  Public  get=0x0A8DA578
  InternalSubset  get=0x0A8DA514
  LocalName  get=0x0A8DA530
METHODS:
  RVA=0x0A8DA4EC  token=0x60008F1  System.Void .ctor(System.Xml.Linq.XDocumentType documentType)
END_CLASS

CLASS: Newtonsoft.Json.Converters.XDocumentWrapper
TYPE:  class
TOKEN: 0x2000119
SIZE:  0x20
EXTENDS: Newtonsoft.Json.Converters.XContainerWrapper
IMPLEMENTS: Newtonsoft.Json.Converters.IXmlDocument Newtonsoft.Json.Converters.IXmlNode
FIELDS:
PROPERTIES:
  Document  get=0x0A8DAF78
  ChildNodes  get=0x0A8DADCC
  HasChildNodes  get=0x0A8DAFB4
  DocumentElement  get=0x0A8DAEDC
METHODS:
  RVA=0x053908C0  token=0x60008F8  System.Void .ctor(System.Xml.Linq.XDocument document)
  RVA=0x0A8DA838  token=0x60008FB  Newtonsoft.Json.Converters.IXmlNode CreateComment(System.String text)
  RVA=0x0A8DAB40  token=0x60008FC  Newtonsoft.Json.Converters.IXmlNode CreateTextNode(System.String text)
  RVA=0x0A8DA7A8  token=0x60008FD  Newtonsoft.Json.Converters.IXmlNode CreateCDataSection(System.String data)
  RVA=0x0A8DABD0  token=0x60008FE  Newtonsoft.Json.Converters.IXmlNode CreateWhitespace(System.String text)
  RVA=0x0A8DAAB0  token=0x60008FF  Newtonsoft.Json.Converters.IXmlNode CreateSignificantWhitespace(System.String text)
  RVA=0x0A8DAC60  token=0x6000900  Newtonsoft.Json.Converters.IXmlNode CreateXmlDeclaration(System.String version, System.String encoding, System.String standalone)
  RVA=0x0A8DAD10  token=0x6000901  Newtonsoft.Json.Converters.IXmlNode CreateXmlDocumentType(System.String name, System.String publicId, System.String systemId, System.String internalSubset)
  RVA=0x0A8DAA10  token=0x6000902  Newtonsoft.Json.Converters.IXmlNode CreateProcessingInstruction(System.String target, System.String data)
  RVA=0x0A8DA8C8  token=0x6000903  Newtonsoft.Json.Converters.IXmlElement CreateElement(System.String elementName)
  RVA=0x0A8DA964  token=0x6000904  Newtonsoft.Json.Converters.IXmlElement CreateElement(System.String qualifiedName, System.String namespaceUri)
  RVA=0x0A8DA640  token=0x6000905  Newtonsoft.Json.Converters.IXmlNode CreateAttribute(System.String name, System.String value)
  RVA=0x0A8DA6EC  token=0x6000906  Newtonsoft.Json.Converters.IXmlNode CreateAttribute(System.String qualifiedName, System.String namespaceUri, System.String value)
  RVA=0x0A8DA5B0  token=0x6000908  Newtonsoft.Json.Converters.IXmlNode AppendChild(Newtonsoft.Json.Converters.IXmlNode newChild)
END_CLASS

CLASS: Newtonsoft.Json.Converters.XTextWrapper
TYPE:  class
TOKEN: 0x200011A
SIZE:  0x18
EXTENDS: Newtonsoft.Json.Converters.XObjectWrapper
FIELDS:
PROPERTIES:
  Text  get=0x0A8DB97C
  Value  get=0x0A8DB9B8
  ParentNode  get=0x0A8DB924
METHODS:
  RVA=0x053908C0  token=0x600090A  System.Void .ctor(System.Xml.Linq.XText text)
END_CLASS

CLASS: Newtonsoft.Json.Converters.XCommentWrapper
TYPE:  class
TOKEN: 0x200011B
SIZE:  0x18
EXTENDS: Newtonsoft.Json.Converters.XObjectWrapper
FIELDS:
PROPERTIES:
  Text  get=0x0A8D9D80
  Value  get=0x0A8D9DBC
  ParentNode  get=0x0A8D9D28
METHODS:
  RVA=0x053908C0  token=0x600090E  System.Void .ctor(System.Xml.Linq.XComment text)
END_CLASS

CLASS: Newtonsoft.Json.Converters.XProcessingInstructionWrapper
TYPE:  class
TOKEN: 0x200011C
SIZE:  0x18
EXTENDS: Newtonsoft.Json.Converters.XObjectWrapper
FIELDS:
PROPERTIES:
  ProcessingInstruction  get=0x0A8DB8C8
  LocalName  get=0x0A8DB8A8
  Value  get=0x0A8DB904
METHODS:
  RVA=0x053908C0  token=0x6000912  System.Void .ctor(System.Xml.Linq.XProcessingInstruction processingInstruction)
END_CLASS

CLASS: Newtonsoft.Json.Converters.XContainerWrapper
TYPE:  class
TOKEN: 0x200011D
SIZE:  0x20
EXTENDS: Newtonsoft.Json.Converters.XObjectWrapper
FIELDS:
  private           System.Collections.Generic.List<Newtonsoft.Json.Converters.IXmlNode>_childNodes  // 0x18
PROPERTIES:
  Container  get=0x0A8DA3AC
  ChildNodes  get=0x0A8DA180
  HasChildNodes  get=0x0A8DA3E8
  ParentNode  get=0x0A8DA414
METHODS:
  RVA=0x053908C0  token=0x6000916  System.Void .ctor(System.Xml.Linq.XContainer container)
  RVA=0x0A8D9E44  token=0x600091A  Newtonsoft.Json.Converters.IXmlNode WrapNode(System.Xml.Linq.XObject node)
  RVA=0x0A8D9DDC  token=0x600091B  Newtonsoft.Json.Converters.IXmlNode AppendChild(Newtonsoft.Json.Converters.IXmlNode newChild)
END_CLASS

CLASS: Newtonsoft.Json.Converters.XObjectWrapper
TYPE:  class
TOKEN: 0x200011E
SIZE:  0x18
IMPLEMENTS: Newtonsoft.Json.Converters.IXmlNode
FIELDS:
  private   readonly System.Xml.Linq.XObject         _xmlObject  // 0x10
PROPERTIES:
  WrappedNode  get=0x02B2ECC0
  NodeType  get=0x0A8DB888
  LocalName  get=0x011EC580
  ChildNodes  get=0x0A8DB838
  Attributes  get=0x0A8DB7E8
  ParentNode  get=0x011EC580
  Value  get=0x011EC580
  NamespaceUri  get=0x011EC580
METHODS:
  RVA=0x053908C0  token=0x600091C  System.Void .ctor(System.Xml.Linq.XObject xmlObject)
  RVA=0x0A8DB79C  token=0x6000924  Newtonsoft.Json.Converters.IXmlNode AppendChild(Newtonsoft.Json.Converters.IXmlNode newChild)
END_CLASS

CLASS: Newtonsoft.Json.Converters.XAttributeWrapper
TYPE:  class
TOKEN: 0x200011F
SIZE:  0x18
EXTENDS: Newtonsoft.Json.Converters.XObjectWrapper
FIELDS:
PROPERTIES:
  Attribute  get=0x0A8D9C20
  Value  get=0x0A8D9D08
  LocalName  get=0x0A8D9C5C
  NamespaceUri  get=0x0A8D9C84
  ParentNode  get=0x0A8D9CB0
METHODS:
  RVA=0x053908C0  token=0x6000927  System.Void .ctor(System.Xml.Linq.XAttribute attribute)
END_CLASS

CLASS: Newtonsoft.Json.Converters.XElementWrapper
TYPE:  class
TOKEN: 0x2000120
SIZE:  0x28
EXTENDS: Newtonsoft.Json.Converters.XContainerWrapper
IMPLEMENTS: Newtonsoft.Json.Converters.IXmlElement Newtonsoft.Json.Converters.IXmlNode
FIELDS:
  private           System.Collections.Generic.List<Newtonsoft.Json.Converters.IXmlNode>_attributes  // 0x20
PROPERTIES:
  Element  get=0x0A8DB6C4
  Attributes  get=0x0A8DB35C
  Value  get=0x0A8DB778
  LocalName  get=0x0A8DB724
  NamespaceUri  get=0x0A8DB74C
  IsEmpty  get=0x0A8DB700
METHODS:
  RVA=0x053908C0  token=0x600092D  System.Void .ctor(System.Xml.Linq.XElement element)
  RVA=0x0A8DB2DC  token=0x600092E  System.Void SetAttributeNode(Newtonsoft.Json.Converters.IXmlNode attribute)
  RVA=0x0A8DB064  token=0x6000930  System.Boolean HasImplicitNamespaceAttribute(System.String namespaceUri)
  RVA=0x0A8DAFF0  token=0x6000931  Newtonsoft.Json.Converters.IXmlNode AppendChild(Newtonsoft.Json.Converters.IXmlNode newChild)
  RVA=0x0A8DB020  token=0x6000935  System.String GetPrefixOfNamespace(System.String namespaceUri)
END_CLASS

CLASS: Newtonsoft.Json.Converters.XmlNodeConverter
TYPE:  class
TOKEN: 0x2000121
SIZE:  0x20
EXTENDS: Newtonsoft.Json.JsonConverter
FIELDS:
  private   static readonly System.Collections.Generic.List<Newtonsoft.Json.Converters.IXmlNode>EmptyChildNodes  // static @ 0x0
  private   static  System.String                   TextName  // const
  private   static  System.String                   CommentName  // const
  private   static  System.String                   CDataName  // const
  private   static  System.String                   WhitespaceName  // const
  private   static  System.String                   SignificantWhitespaceName  // const
  private   static  System.String                   DeclarationName  // const
  private   static  System.String                   JsonNamespaceUri  // const
  private           System.String                   <DeserializeRootElementName>k__BackingField  // 0x10
  private           System.Boolean                  <WriteArrayAttribute>k__BackingField  // 0x18
  private           System.Boolean                  <OmitRootObject>k__BackingField  // 0x19
  private           System.Boolean                  <EncodeSpecialCharacters>k__BackingField  // 0x1a
PROPERTIES:
  DeserializeRootElementName  get=0x02B2ECC0  set=0x053908C0
  WriteArrayAttribute  get=0x04D86340  set=0x04D86350
  OmitRootObject  get=0x04D866A0  set=0x04D866C0
  EncodeSpecialCharacters  get=0x015AF940  set=0x04D866E0
METHODS:
  RVA=0x0A8E209C  token=0x600093F  System.Void WriteJson(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x0A8E1DA0  token=0x6000940  Newtonsoft.Json.Converters.IXmlNode WrapXml(System.Object value)
  RVA=0x0A8DE8DC  token=0x6000941  System.Void PushParentNamespaces(Newtonsoft.Json.Converters.IXmlNode node, System.Xml.XmlNamespaceManager manager)
  RVA=0x0A8DFDB0  token=0x6000942  System.String ResolveFullName(Newtonsoft.Json.Converters.IXmlNode node, System.Xml.XmlNamespaceManager manager)
  RVA=0x0A8DE288  token=0x6000943  System.String GetPropertyName(Newtonsoft.Json.Converters.IXmlNode node, System.Xml.XmlNamespaceManager manager)
  RVA=0x0A8DE568  token=0x6000944  System.Boolean IsArray(Newtonsoft.Json.Converters.IXmlNode node)
  RVA=0x0A8DFEF0  token=0x6000945  System.Void SerializeGroupedNodes(Newtonsoft.Json.JsonWriter writer, Newtonsoft.Json.Converters.IXmlNode node, System.Xml.XmlNamespaceManager manager, System.Boolean writePropertyName)
  RVA=0x0A8E1E88  token=0x6000946  System.Void WriteGroupedNodes(Newtonsoft.Json.JsonWriter writer, System.Xml.XmlNamespaceManager manager, System.Boolean writePropertyName, System.Collections.Generic.List<Newtonsoft.Json.Converters.IXmlNode> groupedNodes, System.String elementNames)
  RVA=0x0A8E1FD4  token=0x6000947  System.Void WriteGroupedNodes(Newtonsoft.Json.JsonWriter writer, System.Xml.XmlNamespaceManager manager, System.Boolean writePropertyName, Newtonsoft.Json.Converters.IXmlNode node, System.String elementNames)
  RVA=0x0A8E068C  token=0x6000948  System.Void SerializeNode(Newtonsoft.Json.JsonWriter writer, Newtonsoft.Json.Converters.IXmlNode node, System.Xml.XmlNamespaceManager manager, System.Boolean writePropertyName)
  RVA=0x0A8DC58C  token=0x6000949  System.Boolean AllSameName(Newtonsoft.Json.Converters.IXmlNode node)
  RVA=0x0A8DF7D4  token=0x600094A  System.Object ReadJson(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue, Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x0A8DDF28  token=0x600094B  System.Void DeserializeValue(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Converters.IXmlDocument document, System.Xml.XmlNamespaceManager manager, System.String propertyName, Newtonsoft.Json.Converters.IXmlNode currentNode)
  RVA=0x0A8DF4C8  token=0x600094C  System.Void ReadElement(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Converters.IXmlDocument document, Newtonsoft.Json.Converters.IXmlNode currentNode, System.String propertyName, System.Xml.XmlNamespaceManager manager)
  RVA=0x0A8DCF2C  token=0x600094D  System.Void CreateElement(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Converters.IXmlDocument document, Newtonsoft.Json.Converters.IXmlNode currentNode, System.String elementName, System.Xml.XmlNamespaceManager manager, System.String elementPrefix, System.Collections.Generic.Dictionary<System.String,System.String> attributeNameValues)
  RVA=0x0A8DC288  token=0x600094E  System.Void AddAttribute(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Converters.IXmlDocument document, Newtonsoft.Json.Converters.IXmlNode currentNode, System.String propertyName, System.String attributeName, System.Xml.XmlNamespaceManager manager, System.String attributePrefix)
  RVA=0x0A8DC6E8  token=0x600094F  System.String ConvertTokenToXmlValue(Newtonsoft.Json.JsonReader reader)
  RVA=0x0A8DEC5C  token=0x6000950  System.Void ReadArrayElements(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Converters.IXmlDocument document, System.String propertyName, Newtonsoft.Json.Converters.IXmlNode currentNode, System.Xml.XmlNamespaceManager manager)
  RVA=0x0A8DC440  token=0x6000951  System.Void AddJsonArrayAttribute(Newtonsoft.Json.Converters.IXmlElement element, Newtonsoft.Json.Converters.IXmlDocument document)
  RVA=0x0A8E1BA0  token=0x6000952  System.Boolean ShouldReadInto(Newtonsoft.Json.JsonReader reader)
  RVA=0x0A8DEEE8  token=0x6000953  System.Collections.Generic.Dictionary<System.String,System.String> ReadAttributeElements(Newtonsoft.Json.JsonReader reader, System.Xml.XmlNamespaceManager manager)
  RVA=0x0A8DD518  token=0x6000954  System.Void CreateInstruction(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Converters.IXmlDocument document, Newtonsoft.Json.Converters.IXmlNode currentNode, System.String propertyName)
  RVA=0x0A8DCBF0  token=0x6000955  System.Void CreateDocumentType(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Converters.IXmlDocument document, Newtonsoft.Json.Converters.IXmlNode currentNode)
  RVA=0x0A8DD380  token=0x6000956  Newtonsoft.Json.Converters.IXmlElement CreateElement(System.String elementName, Newtonsoft.Json.Converters.IXmlDocument document, System.String elementPrefix, System.Xml.XmlNamespaceManager manager)
  RVA=0x0A8DD8C4  token=0x6000957  System.Void DeserializeNode(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Converters.IXmlDocument document, System.Xml.XmlNamespaceManager manager, Newtonsoft.Json.Converters.IXmlNode currentNode)
  RVA=0x0A8DE720  token=0x6000958  System.Boolean IsNamespaceAttribute(System.String attributeName, System.String& prefix)
  RVA=0x0A8E1C0C  token=0x6000959  System.Boolean ValueAttributes(System.Collections.Generic.List<Newtonsoft.Json.Converters.IXmlNode> c)
  RVA=0x040B9AC0  token=0x600095A  System.Boolean CanConvert(System.Type valueType)
  RVA=0x0A8DE7EC  token=0x600095B  System.Boolean IsXObject(System.Type valueType)
  RVA=0x0A8DE864  token=0x600095C  System.Boolean IsXmlNode(System.Type valueType)
  RVA=0x041E1670  token=0x600095D  System.Void .ctor()
  RVA=0x04D165F0  token=0x600095E  System.Void .cctor()
END_CLASS

CLASS: Newtonsoft.Json.Linq.CommentHandling
TYPE:  sealed struct
TOKEN: 0x20000DE
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.Linq.CommentHandlingIgnore  // const
  public    static  Newtonsoft.Json.Linq.CommentHandlingLoad  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.Linq.DuplicatePropertyNameHandling
TYPE:  sealed struct
TOKEN: 0x20000DF
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.Linq.DuplicatePropertyNameHandlingReplace  // const
  public    static  Newtonsoft.Json.Linq.DuplicatePropertyNameHandlingIgnore  // const
  public    static  Newtonsoft.Json.Linq.DuplicatePropertyNameHandlingError  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.Linq.Extensions
TYPE:  static class
TOKEN: 0x20000E0
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000679  U Value(System.Collections.Generic.IEnumerable<Newtonsoft.Json.Linq.JToken> value)
  RVA=-1  // generic def  token=0x600067A  U Value(System.Collections.Generic.IEnumerable<T> value)
  RVA=-1  // generic def  token=0x600067B  U Convert(T token)
END_CLASS

CLASS: Newtonsoft.Json.Linq.JArray
TYPE:  class
TOKEN: 0x20000E1
SIZE:  0x58
EXTENDS: Newtonsoft.Json.Linq.JContainer
IMPLEMENTS: System.Collections.Generic.IList`1 System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly System.Collections.Generic.List<Newtonsoft.Json.Linq.JToken>_values  // 0x50
PROPERTIES:
  ChildrenTokens  get=0x04D86270
  Type  get=0x02BBE540
  Item  get=0x0A8C3240
  Item  get=0x0A8C3240  set=0x0A8C3388
  IsReadOnly  get=0x012081B0
METHODS:
  RVA=0x03427EE0  token=0x600067E  System.Void .ctor()
  RVA=0x0A8C31A0  token=0x600067F  System.Void .ctor(Newtonsoft.Json.Linq.JArray other, Newtonsoft.Json.Linq.JsonCloneSettings settings)
  RVA=0x0A8C3104  token=0x6000680  System.Void .ctor(System.Object content)
  RVA=0x0A8C2F14  token=0x6000681  Newtonsoft.Json.Linq.JToken CloneToken(Newtonsoft.Json.Linq.JsonCloneSettings settings)
  RVA=0x05396950  token=0x6000682  Newtonsoft.Json.Linq.JArray Load(Newtonsoft.Json.JsonReader reader)
  RVA=0x03423920  token=0x6000683  Newtonsoft.Json.Linq.JArray Load(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Linq.JsonLoadSettings settings)
  RVA=0x0A8C304C  token=0x6000684  System.Void WriteTo(Newtonsoft.Json.JsonWriter writer, Newtonsoft.Json.JsonConverter[] converters)
  RVA=0x0A8C2FA0  token=0x6000688  System.Int32 IndexOfItem(Newtonsoft.Json.Linq.JToken item)
  RVA=0x0A8C2FF4  token=0x6000689  System.Int32 IndexOf(Newtonsoft.Json.Linq.JToken item)
  RVA=0x0A8C3000  token=0x600068A  System.Void Insert(System.Int32 index, Newtonsoft.Json.Linq.JToken item)
  RVA=0x0A8C3024  token=0x600068B  System.Void RemoveAt(System.Int32 index)
  RVA=0x03F4DDF0  token=0x600068C  System.Collections.Generic.IEnumerator<Newtonsoft.Json.Linq.JToken> GetEnumerator()
  RVA=0x0A8C2EFC  token=0x600068D  System.Void Add(Newtonsoft.Json.Linq.JToken item)
  RVA=0x0A8C2F04  token=0x600068E  System.Void Clear()
  RVA=0x0A8C2F84  token=0x600068F  System.Boolean Contains(Newtonsoft.Json.Linq.JToken item)
  RVA=0x0A8C2F98  token=0x6000690  System.Void CopyTo(Newtonsoft.Json.Linq.JToken[] array, System.Int32 arrayIndex)
  RVA=0x0A8C3038  token=0x6000692  System.Boolean Remove(Newtonsoft.Json.Linq.JToken item)
END_CLASS

CLASS: Newtonsoft.Json.Linq.JConstructor
TYPE:  class
TOKEN: 0x20000E2
SIZE:  0x60
EXTENDS: Newtonsoft.Json.Linq.JContainer
FIELDS:
  private           System.String                   _name  // 0x50
  private   readonly System.Collections.Generic.List<Newtonsoft.Json.Linq.JToken>_values  // 0x58
PROPERTIES:
  ChildrenTokens  get=0x02E56440
  Name  get=0x04D86270
  Type  get=0x04D8C9E0
  Item  get=0x0A8C3940
METHODS:
  RVA=0x0A8C3408  token=0x6000694  System.Int32 IndexOfItem(Newtonsoft.Json.Linq.JToken item)
  RVA=0x0A8C3888  token=0x6000697  System.Void .ctor(Newtonsoft.Json.Linq.JConstructor other, Newtonsoft.Json.Linq.JsonCloneSettings settings)
  RVA=0x0A8C3728  token=0x6000698  System.Void .ctor(System.String name)
  RVA=0x0A8C3398  token=0x6000699  Newtonsoft.Json.Linq.JToken CloneToken(Newtonsoft.Json.Linq.JsonCloneSettings settings)
  RVA=0x0A8C3658  token=0x600069A  System.Void WriteTo(Newtonsoft.Json.JsonWriter writer, Newtonsoft.Json.JsonConverter[] converters)
  RVA=0x0A8C345C  token=0x600069C  Newtonsoft.Json.Linq.JConstructor Load(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Linq.JsonLoadSettings settings)
END_CLASS

CLASS: Newtonsoft.Json.Linq.JContainer
TYPE:  abstract class
TOKEN: 0x20000E3
SIZE:  0x50
EXTENDS: Newtonsoft.Json.Linq.JToken
IMPLEMENTS: System.Collections.Generic.IList`1 System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.ComponentModel.IBindingList System.Collections.ICollection System.Collections.IList System.Collections.Specialized.INotifyCollectionChanged
FIELDS:
  private           System.ComponentModel.ListChangedEventHandler_listChanged  // 0x30
  private           System.Collections.Specialized.NotifyCollectionChangedEventHandler_collectionChanged  // 0x38
  private           System.Object                   _syncRoot  // 0x40
  private           System.Boolean                  _busy  // 0x48
PROPERTIES:
  ChildrenTokens  get=-1  // abstract
  HasValues  get=0x040464C0
  First  get=0x049E74E0
  Last  get=0x03426800
  System.Collections.Generic.IList<Newtonsoft.Json.Linq.JToken>.Item  get=0x053924D4  set=0x0A8C3388
  System.Collections.Generic.ICollection<Newtonsoft.Json.Linq.JToken>.IsReadOnly  get=0x012081B0
  System.Collections.IList.IsFixedSize  get=0x012081B0
  System.Collections.IList.IsReadOnly  get=0x012081B0
  System.Collections.IList.Item  get=0x053924D4  set=0x0A8C47DC
  Count  get=0x03424A40
  System.Collections.ICollection.IsSynchronized  get=0x012081B0
  System.Collections.ICollection.SyncRoot  get=0x0A8C4694
EVENTS:
  CollectionChanged  add=add_CollectionChanged  remove=remove_CollectionChanged
METHODS:
  RVA=0x03427C80  token=0x60006A0  System.Void .ctor()
  RVA=0x0A8C4814  token=0x60006A1  System.Void .ctor(Newtonsoft.Json.Linq.JContainer other, Newtonsoft.Json.Linq.JsonCloneSettings settings)
  RVA=0x034279A0  token=0x60006A2  System.Void CheckReentrancy()
  RVA=0x0A8C42A0  token=0x60006A3  System.Void OnListChanged(System.ComponentModel.ListChangedEventArgs e)
  RVA=0x0A8C4204  token=0x60006A4  System.Void OnCollectionChanged(System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
  RVA=0x04791E50  token=0x60006A8  Newtonsoft.Json.Linq.JEnumerable<Newtonsoft.Json.Linq.JToken> Children()
  RVA=0x03427480  token=0x60006A9  System.Boolean IsMultiContent(System.Object content)
  RVA=0x03427930  token=0x60006AA  Newtonsoft.Json.Linq.JToken EnsureParentToken(Newtonsoft.Json.Linq.JToken item, System.Boolean skipParentCheck, System.Boolean copyAnnotations)
  RVA=-1  // abstract  token=0x60006AB  System.Int32 IndexOfItem(Newtonsoft.Json.Linq.JToken item)
  RVA=0x03427750  token=0x60006AC  System.Boolean InsertItem(System.Int32 index, Newtonsoft.Json.Linq.JToken item, System.Boolean skipParentCheck, System.Boolean copyAnnotations)
  RVA=0x0A8C433C  token=0x60006AD  System.Void RemoveItemAt(System.Int32 index)
  RVA=0x0A8C4618  token=0x60006AE  System.Boolean RemoveItem(Newtonsoft.Json.Linq.JToken item)
  RVA=0x03428730  token=0x60006AF  Newtonsoft.Json.Linq.JToken GetItem(System.Int32 index)
  RVA=0x047C9BB0  token=0x60006B0  System.Void SetItem(System.Int32 index, Newtonsoft.Json.Linq.JToken item)
  RVA=0x0A8C3A8C  token=0x60006B1  System.Void ClearItems()
  RVA=0x0A8C4654  token=0x60006B2  System.Void ReplaceItem(Newtonsoft.Json.Linq.JToken existing, Newtonsoft.Json.Linq.JToken replacement)
  RVA=0x0A8C3DB0  token=0x60006B3  System.Boolean ContainsItem(Newtonsoft.Json.Linq.JToken item)
  RVA=0x0A8C3DCC  token=0x60006B4  System.Void CopyItemsTo(System.Array array, System.Int32 arrayIndex)
  RVA=0x047C9EA0  token=0x60006B5  System.Boolean IsTokenUnchanged(Newtonsoft.Json.Linq.JToken currentValue, Newtonsoft.Json.Linq.JToken newValue)
  RVA=0x040B6120  token=0x60006B6  System.Void ValidateToken(Newtonsoft.Json.Linq.JToken o, Newtonsoft.Json.Linq.JToken existing)
  RVA=0x03426FD0  token=0x60006B7  System.Void Add(System.Object content)
  RVA=0x034285D0  token=0x60006B8  System.Boolean TryAdd(System.Object content)
  RVA=0x03426110  token=0x60006B9  System.Void AddAndSkipParentCheck(Newtonsoft.Json.Linq.JToken token)
  RVA=0x03427040  token=0x60006BA  System.Boolean TryAddInternal(System.Int32 index, System.Object content, System.Boolean skipParentCheck, System.Boolean copyAnnotations)
  RVA=0x03427440  token=0x60006BB  Newtonsoft.Json.Linq.JToken CreateFromContent(System.Object content)
  RVA=0x0A8C2F04  token=0x60006BC  System.Void RemoveAll()
  RVA=0x03425FA0  token=0x60006BD  System.Void ReadTokenFrom(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Linq.JsonLoadSettings options)
  RVA=0x03426CF0  token=0x60006BE  System.Void ReadContentFrom(Newtonsoft.Json.JsonReader r, Newtonsoft.Json.Linq.JsonLoadSettings settings)
  RVA=0x03426A00  token=0x60006BF  Newtonsoft.Json.Linq.JProperty ReadProperty(Newtonsoft.Json.JsonReader r, Newtonsoft.Json.Linq.JsonLoadSettings settings, Newtonsoft.Json.IJsonLineInfo lineInfo, Newtonsoft.Json.Linq.JContainer parent)
  RVA=0x0A8C2FF4  token=0x60006C0  System.Int32 System.Collections.Generic.IList<Newtonsoft.Json.Linq.JToken>.IndexOf(Newtonsoft.Json.Linq.JToken item)
  RVA=0x0A8C3000  token=0x60006C1  System.Void System.Collections.Generic.IList<Newtonsoft.Json.Linq.JToken>.Insert(System.Int32 index, Newtonsoft.Json.Linq.JToken item)
  RVA=0x0A8C3024  token=0x60006C2  System.Void System.Collections.Generic.IList<Newtonsoft.Json.Linq.JToken>.RemoveAt(System.Int32 index)
  RVA=0x0A8C2EFC  token=0x60006C5  System.Void System.Collections.Generic.ICollection<Newtonsoft.Json.Linq.JToken>.Add(Newtonsoft.Json.Linq.JToken item)
  RVA=0x0A8C2F04  token=0x60006C6  System.Void System.Collections.Generic.ICollection<Newtonsoft.Json.Linq.JToken>.Clear()
  RVA=0x0A8C2F84  token=0x60006C7  System.Boolean System.Collections.Generic.ICollection<Newtonsoft.Json.Linq.JToken>.Contains(Newtonsoft.Json.Linq.JToken item)
  RVA=0x0A8C2F98  token=0x60006C8  System.Void System.Collections.Generic.ICollection<Newtonsoft.Json.Linq.JToken>.CopyTo(Newtonsoft.Json.Linq.JToken[] array, System.Int32 arrayIndex)
  RVA=0x0A8C3038  token=0x60006CA  System.Boolean System.Collections.Generic.ICollection<Newtonsoft.Json.Linq.JToken>.Remove(Newtonsoft.Json.Linq.JToken item)
  RVA=0x0A8C4168  token=0x60006CB  Newtonsoft.Json.Linq.JToken EnsureValue(System.Object value)
  RVA=0x0A8C46F4  token=0x60006CC  System.Int32 System.Collections.IList.Add(System.Object value)
  RVA=0x0A8C2F04  token=0x60006CD  System.Void System.Collections.IList.Clear()
  RVA=0x0A8C4728  token=0x60006CE  System.Boolean System.Collections.IList.Contains(System.Object value)
  RVA=0x0A8C4750  token=0x60006CF  System.Int32 System.Collections.IList.IndexOf(System.Object value)
  RVA=0x0A8C4774  token=0x60006D0  System.Void System.Collections.IList.Insert(System.Int32 index, System.Object value)
  RVA=0x0A8C47B4  token=0x60006D3  System.Void System.Collections.IList.Remove(System.Object value)
  RVA=0x0A8C3024  token=0x60006D4  System.Void System.Collections.IList.RemoveAt(System.Int32 index)
  RVA=0x0A8C2F98  token=0x60006D7  System.Void System.Collections.ICollection.CopyTo(System.Array array, System.Int32 index)
END_CLASS

CLASS: Newtonsoft.Json.Linq.JEnumerable`1
TYPE:  sealed struct
TOKEN: 0x20000E4
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.IEquatable`1
FIELDS:
  public    static readonly Newtonsoft.Json.Linq.JEnumerable<T>Empty  // static @ 0x0
  private   readonly System.Collections.Generic.IEnumerable<T>_enumerable  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60006DB  System.Void .ctor(System.Collections.Generic.IEnumerable<T> enumerable)
  RVA=-1  // not resolved  token=0x60006DC  System.Collections.Generic.IEnumerator<T> GetEnumerator()
  RVA=-1  // not resolved  token=0x60006DD  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x60006DE  System.Boolean Equals(Newtonsoft.Json.Linq.JEnumerable<T> other)
  RVA=-1  // not resolved  token=0x60006DF  System.Boolean Equals(System.Object obj)
  RVA=-1  // not resolved  token=0x60006E0  System.Int32 GetHashCode()
  RVA=-1  // not resolved  token=0x60006E1  System.Void .cctor()
END_CLASS

CLASS: Newtonsoft.Json.Linq.JObject
TYPE:  class
TOKEN: 0x20000E5
SIZE:  0x68
EXTENDS: Newtonsoft.Json.Linq.JContainer
IMPLEMENTS: System.Collections.Generic.IDictionary`2 System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.ComponentModel.INotifyPropertyChanged System.ComponentModel.ICustomTypeDescriptor
FIELDS:
  private   readonly Newtonsoft.Json.Linq.JPropertyKeyedCollection_properties  // 0x50
  private           System.ComponentModel.PropertyChangedEventHandlerPropertyChanged  // 0x58
  private           System.ComponentModel.PropertyChangingEventHandlerPropertyChanging  // 0x60
PROPERTIES:
  ChildrenTokens  get=0x04D86270
  Type  get=0x04D85B80
  Item  get=0x034268F0
  Item  get=0x034268F0  set=0x04CB6A90
  System.Collections.Generic.IDictionary<System.String,Newtonsoft.Json.Linq.JToken>.Keys  get=0x0A8C553C
  System.Collections.Generic.IDictionary<System.String,Newtonsoft.Json.Linq.JToken>.Values  get=0x0A8C555C
  System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,Newtonsoft.Json.Linq.JToken>>.IsReadOnly  get=0x012081B0
EVENTS:
  PropertyChanged  add=add_PropertyChanged  remove=remove_PropertyChanged
METHODS:
  RVA=0x03426890  token=0x60006E5  System.Void .ctor()
  RVA=0x0A8C5A24  token=0x60006E6  System.Void .ctor(Newtonsoft.Json.Linq.JObject other, Newtonsoft.Json.Linq.JsonCloneSettings settings)
  RVA=0x0A8C4FBC  token=0x60006E7  System.Int32 IndexOfItem(Newtonsoft.Json.Linq.JToken item)
  RVA=0x03426BF0  token=0x60006E8  System.Boolean InsertItem(System.Int32 index, Newtonsoft.Json.Linq.JToken item, System.Boolean skipParentCheck, System.Boolean copyAnnotations)
  RVA=0x034279C0  token=0x60006E9  System.Void ValidateToken(Newtonsoft.Json.Linq.JToken o, Newtonsoft.Json.Linq.JToken existing)
  RVA=0x047C9F40  token=0x60006EA  System.Void InternalPropertyChanged(Newtonsoft.Json.Linq.JProperty childProperty)
  RVA=0x047C9E80  token=0x60006EB  System.Void InternalPropertyChanging(Newtonsoft.Json.Linq.JProperty childProperty)
  RVA=0x0A8C4DF8  token=0x60006EC  Newtonsoft.Json.Linq.JToken CloneToken(Newtonsoft.Json.Linq.JsonCloneSettings settings)
  RVA=0x03F4DBE0  token=0x60006EE  System.Collections.Generic.IEnumerable<Newtonsoft.Json.Linq.JProperty> Properties()
  RVA=0x03427A60  token=0x60006EF  Newtonsoft.Json.Linq.JProperty Property(System.String name, System.StringComparison comparison)
  RVA=0x05396944  token=0x60006F3  Newtonsoft.Json.Linq.JObject Load(Newtonsoft.Json.JsonReader reader)
  RVA=0x03424AA0  token=0x60006F4  Newtonsoft.Json.Linq.JObject Load(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Linq.JsonLoadSettings settings)
  RVA=0x05396938  token=0x60006F5  Newtonsoft.Json.Linq.JObject Parse(System.String json)
  RVA=0x03423680  token=0x60006F6  Newtonsoft.Json.Linq.JObject Parse(System.String json, Newtonsoft.Json.Linq.JsonLoadSettings settings)
  RVA=0x035A5E50  token=0x60006F7  Newtonsoft.Json.Linq.JObject FromObject(System.Object o)
  RVA=0x035A4FB0  token=0x60006F8  Newtonsoft.Json.Linq.JObject FromObject(System.Object o, Newtonsoft.Json.JsonSerializer jsonSerializer)
  RVA=0x034266B0  token=0x60006F9  System.Void WriteTo(Newtonsoft.Json.JsonWriter writer, Newtonsoft.Json.JsonConverter[] converters)
  RVA=0x0A8C4FAC  token=0x60006FA  Newtonsoft.Json.Linq.JToken GetValue(System.String propertyName)
  RVA=0x0A8C4F80  token=0x60006FB  Newtonsoft.Json.Linq.JToken GetValue(System.String propertyName, System.StringComparison comparison)
  RVA=0x0A8C4D70  token=0x60006FC  System.Void Add(System.String propertyName, Newtonsoft.Json.Linq.JToken value)
  RVA=0x03425800  token=0x60006FD  System.Boolean ContainsKey(System.String propertyName)
  RVA=0x03428650  token=0x60006FF  System.Boolean Remove(System.String propertyName)
  RVA=0x03426660  token=0x6000700  System.Boolean TryGetValue(System.String propertyName, Newtonsoft.Json.Linq.JToken& value)
  RVA=0x0A8C4FE8  token=0x6000702  System.Void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,Newtonsoft.Json.Linq.JToken>>.Add(System.Collections.Generic.KeyValuePair<System.String,Newtonsoft.Json.Linq.JToken> item)
  RVA=0x0A8C2F04  token=0x6000703  System.Void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,Newtonsoft.Json.Linq.JToken>>.Clear()
  RVA=0x0A8C507C  token=0x6000704  System.Boolean System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,Newtonsoft.Json.Linq.JToken>>.Contains(System.Collections.Generic.KeyValuePair<System.String,Newtonsoft.Json.Linq.JToken> item)
  RVA=0x0A8C50E8  token=0x6000705  System.Void System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,Newtonsoft.Json.Linq.JToken>>.CopyTo(System.Collections.Generic.KeyValuePair<System.String,Newtonsoft.Json.Linq.JToken>[] array, System.Int32 arrayIndex)
  RVA=0x0A8C54B4  token=0x6000707  System.Boolean System.Collections.Generic.ICollection<System.Collections.Generic.KeyValuePair<System.String,Newtonsoft.Json.Linq.JToken>>.Remove(System.Collections.Generic.KeyValuePair<System.String,Newtonsoft.Json.Linq.JToken> item)
  RVA=0x0A8C4E68  token=0x6000708  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,Newtonsoft.Json.Linq.JToken>> GetEnumerator()
  RVA=0x047C9FD0  token=0x6000709  System.Void OnPropertyChanged(System.String propertyName)
  RVA=0x047C9EF0  token=0x600070A  System.Void OnPropertyChanging(System.String propertyName)
  RVA=0x0A8C59CC  token=0x600070B  System.ComponentModel.PropertyDescriptorCollection System.ComponentModel.ICustomTypeDescriptor.GetProperties()
  RVA=0x0A8C5634  token=0x600070C  System.ComponentModel.PropertyDescriptorCollection System.ComponentModel.ICustomTypeDescriptor.GetProperties(System.Attribute[] attributes)
  RVA=0x0A8C55A8  token=0x600070D  System.ComponentModel.AttributeCollection System.ComponentModel.ICustomTypeDescriptor.GetAttributes()
  RVA=0x0A8C55F8  token=0x600070E  System.ComponentModel.TypeConverter System.ComponentModel.ICustomTypeDescriptor.GetConverter()
  RVA=0x0A8C59D4  token=0x600070F  System.Object System.ComponentModel.ICustomTypeDescriptor.GetPropertyOwner(System.ComponentModel.PropertyDescriptor pd)
  RVA=0x0A8C4EC4  token=0x6000710  System.Dynamic.DynamicMetaObject GetMetaObject(System.Linq.Expressions.Expression parameter)
END_CLASS

CLASS: Newtonsoft.Json.Linq.JProperty
TYPE:  class
TOKEN: 0x20000E9
SIZE:  0x60
EXTENDS: Newtonsoft.Json.Linq.JContainer
FIELDS:
  private   readonly Newtonsoft.Json.Linq.JProperty.JPropertyList_content  // 0x50
  private   readonly System.String                   _name  // 0x58
PROPERTIES:
  ChildrenTokens  get=0x04D86270
  Name  get=0x02E56440
  Value  get=0x0539250C  set=0x04CB6AE0
  Type  get=0x04D882B0
METHODS:
  RVA=0x0A8C67C4  token=0x6000723  System.Void .ctor(Newtonsoft.Json.Linq.JProperty other, Newtonsoft.Json.Linq.JsonCloneSettings settings)
  RVA=0x0A8C63BC  token=0x6000724  Newtonsoft.Json.Linq.JToken GetItem(System.Int32 index)
  RVA=0x047C9DC0  token=0x6000725  System.Void SetItem(System.Int32 index, Newtonsoft.Json.Linq.JToken item)
  RVA=0x0A8C6704  token=0x6000726  System.Boolean RemoveItem(Newtonsoft.Json.Linq.JToken item)
  RVA=0x0A8C6644  token=0x6000727  System.Void RemoveItemAt(System.Int32 index)
  RVA=0x0A8C6418  token=0x6000728  System.Int32 IndexOfItem(Newtonsoft.Json.Linq.JToken item)
  RVA=0x03426C70  token=0x6000729  System.Boolean InsertItem(System.Int32 index, Newtonsoft.Json.Linq.JToken item, System.Boolean skipParentCheck, System.Boolean copyAnnotations)
  RVA=0x0A8C63A0  token=0x600072A  System.Boolean ContainsItem(Newtonsoft.Json.Linq.JToken item)
  RVA=0x0A8C6270  token=0x600072B  System.Void ClearItems()
  RVA=0x0A8C6330  token=0x600072C  Newtonsoft.Json.Linq.JToken CloneToken(Newtonsoft.Json.Linq.JsonCloneSettings settings)
  RVA=0x03427C10  token=0x600072E  System.Void .ctor(System.String name)
  RVA=0x0A8C6854  token=0x600072F  System.Void .ctor(System.String name, System.Object content)
  RVA=0x03426790  token=0x6000730  System.Void WriteTo(Newtonsoft.Json.JsonWriter writer, Newtonsoft.Json.JsonConverter[] converters)
  RVA=0x0A8C6448  token=0x6000731  Newtonsoft.Json.Linq.JProperty Load(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Linq.JsonLoadSettings settings)
END_CLASS

CLASS: Newtonsoft.Json.Linq.JPropertyDescriptor
TYPE:  class
TOKEN: 0x20000EC
SIZE:  0x88
EXTENDS: System.ComponentModel.PropertyDescriptor
FIELDS:
PROPERTIES:
  ComponentType  get=0x0A8C5D28
  IsReadOnly  get=0x012081B0
  PropertyType  get=0x0A8C5D78
  NameHashCode  get=0x02B70F90
METHODS:
  RVA=0x0A8C5D1C  token=0x6000747  System.Void .ctor(System.String name)
  RVA=0x012081B0  token=0x6000748  System.Boolean CanResetValue(System.Object component)
  RVA=0x0A8C5BDC  token=0x6000749  System.Object GetValue(System.Object component)
  RVA=0x041E1670  token=0x600074A  System.Void ResetValue(System.Object component)
  RVA=0x0A8C5C44  token=0x600074B  System.Void SetValue(System.Object component, System.Object value)
  RVA=0x012081B0  token=0x600074C  System.Boolean ShouldSerializeValue(System.Object component)
END_CLASS

CLASS: Newtonsoft.Json.Linq.JPropertyKeyedCollection
TYPE:  class
TOKEN: 0x20000ED
SIZE:  0x20
EXTENDS: System.Collections.ObjectModel.Collection`1
FIELDS:
  private   static readonly System.Collections.Generic.IEqualityComparer<System.String>Comparer  // static @ 0x0
  private           System.Collections.Generic.Dictionary<System.String,Newtonsoft.Json.Linq.JToken>_dictionary  // 0x18
PROPERTIES:
  Keys  get=0x0A8C60FC
METHODS:
  RVA=0x03427E80  token=0x6000751  System.Void .ctor()
  RVA=0x03F010B0  token=0x6000752  System.Void AddKey(System.String key, Newtonsoft.Json.Linq.JToken item)
  RVA=0x0A8C5DC8  token=0x6000753  System.Void ClearItems()
  RVA=0x03425860  token=0x6000754  System.Boolean Contains(System.String key)
  RVA=0x03F01100  token=0x6000755  System.Void EnsureDictionary()
  RVA=0x03F01070  token=0x6000756  System.String GetKeyForItem(Newtonsoft.Json.Linq.JToken item)
  RVA=0x03F00FD0  token=0x6000757  System.Void InsertItem(System.Int32 index, Newtonsoft.Json.Linq.JToken item)
  RVA=0x0A8C5EA4  token=0x6000758  System.Void RemoveItem(System.Int32 index)
  RVA=0x0A8C5F3C  token=0x6000759  System.Void RemoveKey(System.String key)
  RVA=0x0A8C5F8C  token=0x600075A  System.Void SetItem(System.Int32 index, Newtonsoft.Json.Linq.JToken item)
  RVA=0x03427B20  token=0x600075B  System.Boolean TryGetValue(System.String key, Newtonsoft.Json.Linq.JToken& value)
  RVA=0x0A8C5E24  token=0x600075D  System.Int32 IndexOfReference(Newtonsoft.Json.Linq.JToken t)
  RVA=0x04D36ED0  token=0x600075E  System.Void .cctor()
END_CLASS

CLASS: Newtonsoft.Json.Linq.JRaw
TYPE:  class
TOKEN: 0x20000EE
SIZE:  0x40
EXTENDS: Newtonsoft.Json.Linq.JValue
FIELDS:
METHODS:
  RVA=0x0A8C6CC8  token=0x600075F  System.Void .ctor(Newtonsoft.Json.Linq.JRaw other, Newtonsoft.Json.Linq.JsonCloneSettings settings)
  RVA=0x0A8C6CB8  token=0x6000760  System.Void .ctor(System.Object rawJson)
  RVA=0x0A8C6A04  token=0x6000761  Newtonsoft.Json.Linq.JRaw Create(Newtonsoft.Json.JsonReader reader)
  RVA=0x0A8C6994  token=0x6000762  Newtonsoft.Json.Linq.JToken CloneToken(Newtonsoft.Json.Linq.JsonCloneSettings settings)
END_CLASS

CLASS: Newtonsoft.Json.Linq.JsonCloneSettings
TYPE:  class
TOKEN: 0x20000EF
SIZE:  0x18
FIELDS:
  private   static readonly Newtonsoft.Json.Linq.JsonCloneSettingsSkipCopyAnnotations  // static @ 0x0
  private           System.Boolean                  <CopyAnnotations>k__BackingField  // 0x10
PROPERTIES:
  CopyAnnotations  get=0x015EFCE0  set=0x04D86130
METHODS:
  RVA=0x015EFCD0  token=0x6000763  System.Void .ctor()
  RVA=0x0A8CF7D8  token=0x6000766  System.Void .cctor()
END_CLASS

CLASS: Newtonsoft.Json.Linq.JsonLoadSettings
TYPE:  class
TOKEN: 0x20000F0
SIZE:  0x20
FIELDS:
  private           Newtonsoft.Json.Linq.CommentHandling_commentHandling  // 0x10
  private           Newtonsoft.Json.Linq.LineInfoHandling_lineInfoHandling  // 0x14
  private           Newtonsoft.Json.Linq.DuplicatePropertyNameHandling_duplicatePropertyNameHandling  // 0x18
PROPERTIES:
  CommentHandling  get=0x02B2E2D0
  LineInfoHandling  get=0x04D882A0
  DuplicatePropertyNameHandling  get=0x011EF5B0
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.Linq.JToken
TYPE:  abstract class
TOKEN: 0x20000F1
SIZE:  0x30
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable Newtonsoft.Json.IJsonLineInfo System.ICloneable System.Dynamic.IDynamicMetaObjectProvider
FIELDS:
  private           Newtonsoft.Json.Linq.JContainer _parent  // 0x10
  private           Newtonsoft.Json.Linq.JToken     _previous  // 0x18
  private           Newtonsoft.Json.Linq.JToken     _next  // 0x20
  private           System.Object                   _annotations  // 0x28
  private   static readonly Newtonsoft.Json.Linq.JTokenType[]BooleanTypes  // static @ 0x0
  private   static readonly Newtonsoft.Json.Linq.JTokenType[]NumberTypes  // static @ 0x8
  private   static readonly Newtonsoft.Json.Linq.JTokenType[]BigIntegerTypes  // static @ 0x10
  private   static readonly Newtonsoft.Json.Linq.JTokenType[]StringTypes  // static @ 0x18
  private   static readonly Newtonsoft.Json.Linq.JTokenType[]GuidTypes  // static @ 0x20
  private   static readonly Newtonsoft.Json.Linq.JTokenType[]TimeSpanTypes  // static @ 0x28
  private   static readonly Newtonsoft.Json.Linq.JTokenType[]UriTypes  // static @ 0x30
  private   static readonly Newtonsoft.Json.Linq.JTokenType[]CharTypes  // static @ 0x38
  private   static readonly Newtonsoft.Json.Linq.JTokenType[]DateTimeTypes  // static @ 0x40
  private   static readonly Newtonsoft.Json.Linq.JTokenType[]BytesTypes  // static @ 0x48
PROPERTIES:
  Parent  get=0x02B2ECC0  set=0x053908C0
  Root  get=0x04D7B030
  Type  get=-1  // abstract
  HasValues  get=-1  // abstract
  Next  get=0x04D862C0  set=0x05390F40
  Previous  get=0x0385B100  set=0x05392C40
  Path  get=0x0A8C876C
  Item  get=0x0A8C862C
  First  get=0x0A8C858C
  Last  get=0x0A8C86CC
  Newtonsoft.Json.IJsonLineInfo.LineNumber  get=0x0A8C7F0C
  Newtonsoft.Json.IJsonLineInfo.LinePosition  get=0x0A8C7F50
METHODS:
  RVA=-1  // abstract  token=0x600076D  Newtonsoft.Json.Linq.JToken CloneToken(Newtonsoft.Json.Linq.JsonCloneSettings settings)
  RVA=0x041E1670  token=0x6000775  System.Void .ctor()
  RVA=-1  // generic def  token=0x6000777  T Value(System.Object key)
  RVA=0x0A8C7AB0  token=0x600077A  Newtonsoft.Json.Linq.JEnumerable<Newtonsoft.Json.Linq.JToken> Children()
  RVA=0x0A8C7FE0  token=0x600077B  System.Void Remove()
  RVA=0x0A8C805C  token=0x600077C  System.Void Replace(Newtonsoft.Json.Linq.JToken value)
  RVA=-1  // abstract  token=0x600077D  System.Void WriteTo(Newtonsoft.Json.JsonWriter writer, Newtonsoft.Json.JsonConverter[] converters)
  RVA=0x034258C0  token=0x600077E  System.String ToString()
  RVA=0x03425910  token=0x600077F  System.String ToString(Newtonsoft.Json.Formatting formatting, Newtonsoft.Json.JsonConverter[] converters)
  RVA=0x0A8C7BF8  token=0x6000780  Newtonsoft.Json.Linq.JValue EnsureValue(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8C7DD0  token=0x6000781  System.String GetType(Newtonsoft.Json.Linq.JToken token)
  RVA=0x0A8C84E8  token=0x6000782  System.Boolean ValidateToken(Newtonsoft.Json.Linq.JToken o, Newtonsoft.Json.Linq.JTokenType[] validTypes, System.Boolean nullable)
  RVA=0x0A8CA83C  token=0x6000783  System.Boolean op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8C99B0  token=0x6000784  System.DateTimeOffset op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8CBCBC  token=0x6000785  System.Nullable<System.Boolean> op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8CC690  token=0x6000786  System.Int64 op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8CB860  token=0x6000787  System.Nullable<System.DateTime> op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8C9718  token=0x6000788  System.Nullable<System.DateTimeOffset> op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8CB3F4  token=0x6000789  System.Nullable<System.Decimal> op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8CCE14  token=0x600078A  System.Nullable<System.Double> op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8C9DBC  token=0x600078B  System.Nullable<System.Char> op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8CA3D8  token=0x600078C  System.Int32 op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8CAC0C  token=0x600078D  System.Int16 op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8CBEDC  token=0x600078E  System.UInt16 op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8CC0AC  token=0x600078F  System.Char op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8CD038  token=0x6000790  System.Byte op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8CB000  token=0x6000791  System.SByte op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8CB654  token=0x6000792  System.Nullable<System.Int32> op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8C9FC4  token=0x6000793  System.Nullable<System.Int16> op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8CC27C  token=0x6000794  System.Nullable<System.UInt16> op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8C9134  token=0x6000795  System.Nullable<System.Byte> op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8CC484  token=0x6000796  System.Nullable<System.SByte> op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8C9554  token=0x6000797  System.DateTime op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8CB1D0  token=0x6000798  System.Nullable<System.Int64> op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8C8D58  token=0x6000799  System.Nullable<System.Single> op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8C8B64  token=0x600079A  System.Decimal op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8CA1CC  token=0x600079B  System.Nullable<System.UInt32> op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8CADDC  token=0x600079C  System.Nullable<System.UInt64> op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8CCA7C  token=0x600079D  System.Double op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8C9BEC  token=0x600079E  System.Single op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8CBA7C  token=0x600079F  System.String op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8C8F64  token=0x60007A0  System.UInt32 op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8C8994  token=0x60007A1  System.UInt64 op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8C9340  token=0x60007A2  System.Guid op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8CA5A8  token=0x60007A3  System.Nullable<System.Guid> op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8CCC4C  token=0x60007A4  System.TimeSpan op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8CC860  token=0x60007A5  System.Nullable<System.TimeSpan> op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8CAA20  token=0x60007A6  System.Uri op_Explicit(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8C837C  token=0x60007A7  System.Numerics.BigInteger ToBigInteger(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8C81B0  token=0x60007A8  System.Nullable<System.Numerics.BigInteger> ToBigIntegerNullable(Newtonsoft.Json.Linq.JToken value)
  RVA=0x0A8CD268  token=0x60007A9  Newtonsoft.Json.Linq.JToken op_Implicit(System.Int32 value)
  RVA=0x0A8CD208  token=0x60007AA  Newtonsoft.Json.Linq.JToken op_Implicit(System.String value)
  RVA=0x0A8C8138  token=0x60007AB  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=0x0A8C80D4  token=0x60007AC  System.Collections.Generic.IEnumerator<Newtonsoft.Json.Linq.JToken> System.Collections.Generic.IEnumerable<Newtonsoft.Json.Linq.JToken>.GetEnumerator()
  RVA=0x0A8C7B8C  token=0x60007AD  Newtonsoft.Json.JsonReader CreateReader()
  RVA=0x035A4630  token=0x60007AE  Newtonsoft.Json.Linq.JToken FromObjectInternal(System.Object o, Newtonsoft.Json.JsonSerializer jsonSerializer)
  RVA=0x0A8C7CB8  token=0x60007AF  Newtonsoft.Json.Linq.JToken FromObject(System.Object o)
  RVA=-1  // generic def  token=0x60007B0  T ToObject()
  RVA=0x047915A0  token=0x60007B1  System.Object ToObject(System.Type objectType)
  RVA=0x04791840  token=0x60007B2  System.Object ToObject(System.Type objectType, Newtonsoft.Json.JsonSerializer jsonSerializer)
  RVA=0x0A8C7F94  token=0x60007B3  Newtonsoft.Json.Linq.JToken ReadFrom(Newtonsoft.Json.JsonReader reader)
  RVA=0x034246E0  token=0x60007B4  Newtonsoft.Json.Linq.JToken ReadFrom(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Linq.JsonLoadSettings settings)
  RVA=0x03423DA0  token=0x60007B5  Newtonsoft.Json.Linq.JToken Parse(System.String json)
  RVA=0x03423DF0  token=0x60007B6  Newtonsoft.Json.Linq.JToken Parse(System.String json, Newtonsoft.Json.Linq.JsonLoadSettings settings)
  RVA=0x03424680  token=0x60007B7  Newtonsoft.Json.Linq.JToken Load(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Linq.JsonLoadSettings settings)
  RVA=0x03427520  token=0x60007B8  System.Void SetLineInfo(Newtonsoft.Json.IJsonLineInfo lineInfo, Newtonsoft.Json.Linq.JsonLoadSettings settings)
  RVA=0x03427670  token=0x60007B9  System.Void SetLineInfo(System.Int32 lineNumber, System.Int32 linePosition)
  RVA=0x0A8C7ECC  token=0x60007BA  System.Boolean Newtonsoft.Json.IJsonLineInfo.HasLineInfo()
  RVA=0x0A8C7D10  token=0x60007BD  System.Dynamic.DynamicMetaObject GetMetaObject(System.Linq.Expressions.Expression parameter)
  RVA=0x0A8C8174  token=0x60007BE  System.Dynamic.DynamicMetaObject System.Dynamic.IDynamicMetaObjectProvider.GetMetaObject(System.Linq.Expressions.Expression parameter)
  RVA=0x0A8C7BEC  token=0x60007BF  System.Object System.ICloneable.Clone()
  RVA=0x0A8C7BEC  token=0x60007C0  Newtonsoft.Json.Linq.JToken DeepClone()
  RVA=0x034276D0  token=0x60007C1  System.Void AddAnnotation(System.Object annotation)
  RVA=-1  // generic def  token=0x60007C2  T Annotation()
  RVA=0x0A8C7B00  token=0x60007C3  System.Void CopyAnnotations(Newtonsoft.Json.Linq.JToken target, Newtonsoft.Json.Linq.JToken source)
  RVA=0x037DA1A0  token=0x60007C4  System.Void .cctor()
END_CLASS

CLASS: Newtonsoft.Json.Linq.JTokenReader
TYPE:  class
TOKEN: 0x20000F3
SIZE:  0x98
EXTENDS: Newtonsoft.Json.JsonReader
IMPLEMENTS: Newtonsoft.Json.IJsonLineInfo
FIELDS:
  private   readonly Newtonsoft.Json.Linq.JToken     _root  // 0x78
  private           System.String                   _initialPath  // 0x80
  private           Newtonsoft.Json.Linq.JToken     _parent  // 0x88
  private           Newtonsoft.Json.Linq.JToken     _current  // 0x90
PROPERTIES:
  CurrentToken  get=0x04D86220
  Newtonsoft.Json.IJsonLineInfo.LineNumber  get=0x0A8C6CF8
  Newtonsoft.Json.IJsonLineInfo.LinePosition  get=0x0A8C6D20
  Path  get=0x0A8C6D98
METHODS:
  RVA=0x04791CC0  token=0x60007C7  System.Void .ctor(Newtonsoft.Json.Linq.JToken token)
  RVA=0x03429F10  token=0x60007C8  System.Boolean Read()
  RVA=0x0342A1D0  token=0x60007C9  System.Boolean ReadOver(Newtonsoft.Json.Linq.JToken t)
  RVA=0x0A8C6D48  token=0x60007CA  System.Boolean ReadToEnd()
  RVA=0x0342A330  token=0x60007CB  System.Nullable<Newtonsoft.Json.JsonToken> GetEndToken(Newtonsoft.Json.Linq.JContainer c)
  RVA=0x03429E90  token=0x60007CC  System.Boolean ReadInto(Newtonsoft.Json.Linq.JContainer c)
  RVA=0x0342A290  token=0x60007CD  System.Boolean SetEnd(Newtonsoft.Json.Linq.JContainer c)
  RVA=0x03429FE0  token=0x60007CE  System.Void SetToken(Newtonsoft.Json.Linq.JToken token)
  RVA=0x0A8C6D7C  token=0x60007CF  System.String SafeToString(System.Object value)
  RVA=0x0A8C6CD0  token=0x60007D0  System.Boolean Newtonsoft.Json.IJsonLineInfo.HasLineInfo()
END_CLASS

CLASS: Newtonsoft.Json.Linq.JTokenType
TYPE:  sealed struct
TOKEN: 0x20000F4
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.Linq.JTokenType None  // const
  public    static  Newtonsoft.Json.Linq.JTokenType Object  // const
  public    static  Newtonsoft.Json.Linq.JTokenType Array  // const
  public    static  Newtonsoft.Json.Linq.JTokenType Constructor  // const
  public    static  Newtonsoft.Json.Linq.JTokenType Property  // const
  public    static  Newtonsoft.Json.Linq.JTokenType Comment  // const
  public    static  Newtonsoft.Json.Linq.JTokenType Integer  // const
  public    static  Newtonsoft.Json.Linq.JTokenType Float  // const
  public    static  Newtonsoft.Json.Linq.JTokenType String  // const
  public    static  Newtonsoft.Json.Linq.JTokenType Boolean  // const
  public    static  Newtonsoft.Json.Linq.JTokenType Null  // const
  public    static  Newtonsoft.Json.Linq.JTokenType Undefined  // const
  public    static  Newtonsoft.Json.Linq.JTokenType Date  // const
  public    static  Newtonsoft.Json.Linq.JTokenType Raw  // const
  public    static  Newtonsoft.Json.Linq.JTokenType Bytes  // const
  public    static  Newtonsoft.Json.Linq.JTokenType Guid  // const
  public    static  Newtonsoft.Json.Linq.JTokenType Uri  // const
  public    static  Newtonsoft.Json.Linq.JTokenType TimeSpan  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.Linq.JTokenWriter
TYPE:  class
TOKEN: 0x20000F5
SIZE:  0x80
EXTENDS: Newtonsoft.Json.JsonWriter
FIELDS:
  private           Newtonsoft.Json.Linq.JContainer _token  // 0x60
  private           Newtonsoft.Json.Linq.JContainer _parent  // 0x68
  private           Newtonsoft.Json.Linq.JValue     _value  // 0x70
  private           Newtonsoft.Json.Linq.JToken     _current  // 0x78
PROPERTIES:
  Token  get=0x035A4C40
METHODS:
  RVA=0x035A4C60  token=0x60007D5  System.Void .ctor()
  RVA=0x03779F50  token=0x60007D6  System.Void Close()
  RVA=0x034247C0  token=0x60007D7  System.Void WriteStartObject()
  RVA=0x034260B0  token=0x60007D8  System.Void AddParent(Newtonsoft.Json.Linq.JContainer container)
  RVA=0x04723710  token=0x60007D9  System.Void RemoveParent()
  RVA=0x03426030  token=0x60007DA  System.Void WriteStartArray()
  RVA=0x0A8C700C  token=0x60007DB  System.Void WriteStartConstructor(System.String name)
  RVA=0x04723700  token=0x60007DC  System.Void WriteEnd(Newtonsoft.Json.JsonToken token)
  RVA=0x03428670  token=0x60007DD  System.Void WritePropertyName(System.String name)
  RVA=0x0A8C6E78  token=0x60007DE  System.Void AddRawValue(System.Object value, Newtonsoft.Json.Linq.JTokenType type, Newtonsoft.Json.JsonToken token)
  RVA=0x03428510  token=0x60007DF  System.Void AddJValue(Newtonsoft.Json.Linq.JValue value, Newtonsoft.Json.JsonToken token)
  RVA=0x0A8C745C  token=0x60007E0  System.Void WriteValue(System.Object value)
  RVA=0x0A8C6F58  token=0x60007E1  System.Void WriteNull()
  RVA=0x0A8C7090  token=0x60007E2  System.Void WriteUndefined()
  RVA=0x0A8C6F90  token=0x60007E3  System.Void WriteRaw(System.String json)
  RVA=0x0A8C6F10  token=0x60007E4  System.Void WriteComment(System.String text)
  RVA=0x03428490  token=0x60007E5  System.Void WriteValue(System.String value)
  RVA=0x0A8C74E0  token=0x60007E6  System.Void WriteValue(System.Int32 value)
  RVA=0x0A8C71B0  token=0x60007E7  System.Void WriteValue(System.UInt32 value)
  RVA=0x034283F0  token=0x60007E8  System.Void WriteValue(System.Int64 value)
  RVA=0x0A8C734C  token=0x60007E9  System.Void WriteValue(System.UInt64 value)
  RVA=0x0A8C72C4  token=0x60007EA  System.Void WriteValue(System.Single value)
  RVA=0x0A8C7974  token=0x60007EB  System.Void WriteValue(System.Double value)
  RVA=0x03429DF0  token=0x60007EC  System.Void WriteValue(System.Boolean value)
  RVA=0x0A8C7820  token=0x60007ED  System.Void WriteValue(System.Int16 value)
  RVA=0x0A8C7720  token=0x60007EE  System.Void WriteValue(System.UInt16 value)
  RVA=0x0A8C7898  token=0x60007EF  System.Void WriteValue(System.Char value)
  RVA=0x0A8C713C  token=0x60007F0  System.Void WriteValue(System.Byte value)
  RVA=0x0A8C70C8  token=0x60007F1  System.Void WriteValue(System.SByte value)
  RVA=0x0A8C7224  token=0x60007F2  System.Void WriteValue(System.Decimal value)
  RVA=0x0A8C79FC  token=0x60007F3  System.Void WriteValue(System.DateTime value)
  RVA=0x0A8C7680  token=0x60007F4  System.Void WriteValue(System.DateTimeOffset value)
  RVA=0x0A8C7554  token=0x60007F5  System.Void WriteValue(System.Byte[] value)
  RVA=0x0A8C7798  token=0x60007F6  System.Void WriteValue(System.TimeSpan value)
  RVA=0x0A8C75E0  token=0x60007F7  System.Void WriteValue(System.Guid value)
  RVA=0x0A8C73D4  token=0x60007F8  System.Void WriteValue(System.Uri value)
  RVA=0x03424D40  token=0x60007F9  System.Void WriteToken(Newtonsoft.Json.JsonReader reader, System.Boolean writeChildren, System.Boolean writeDateConstructorAsDate, System.Boolean writeComments)
END_CLASS

CLASS: Newtonsoft.Json.Linq.JValue
TYPE:  class
TOKEN: 0x20000F6
SIZE:  0x40
EXTENDS: Newtonsoft.Json.Linq.JToken
IMPLEMENTS: System.IEquatable`1 System.IFormattable System.IComparable System.IComparable`1 System.IConvertible
FIELDS:
  private           Newtonsoft.Json.Linq.JTokenType _valueType  // 0x30
  private           System.Object                   _value  // 0x38
PROPERTIES:
  HasValues  get=0x012081B0
  Type  get=0x04D864F0
  Value  get=0x04D85A50
METHODS:
  RVA=0x03427E30  token=0x60007FA  System.Void .ctor(System.Object value, Newtonsoft.Json.Linq.JTokenType type)
  RVA=0x0A8CF530  token=0x60007FB  System.Void .ctor(Newtonsoft.Json.Linq.JValue other, Newtonsoft.Json.Linq.JsonCloneSettings settings)
  RVA=0x03427FC0  token=0x60007FC  System.Void .ctor(System.Int64 value)
  RVA=0x0A8CF708  token=0x60007FD  System.Void .ctor(System.Decimal value)
  RVA=0x0A8CF3B0  token=0x60007FE  System.Void .ctor(System.UInt64 value)
  RVA=0x0A8CF410  token=0x60007FF  System.Void .ctor(System.Double value)
  RVA=0x0A8CF5E0  token=0x6000800  System.Void .ctor(System.Single value)
  RVA=0x0A8CF470  token=0x6000801  System.Void .ctor(System.DateTime value)
  RVA=0x0A8CF774  token=0x6000802  System.Void .ctor(System.DateTimeOffset value)
  RVA=0x034287A0  token=0x6000803  System.Void .ctor(System.Boolean value)
  RVA=0x05392538  token=0x6000804  System.Void .ctor(System.String value)
  RVA=0x0A8CF6A4  token=0x6000805  System.Void .ctor(System.Guid value)
  RVA=0x0A8CF640  token=0x6000806  System.Void .ctor(System.Uri value)
  RVA=0x0A8CF4D0  token=0x6000807  System.Void .ctor(System.TimeSpan value)
  RVA=0x03427CA0  token=0x6000808  System.Void .ctor(System.Object value)
  RVA=0x0A8CD788  token=0x600080A  System.Int32 CompareBigInteger(System.Numerics.BigInteger i1, System.Object i2)
  RVA=0x0A8CDAC0  token=0x600080B  System.Int32 Compare(Newtonsoft.Json.Linq.JTokenType valueType, System.Object objA, System.Object objB)
  RVA=0x0A8CD9B4  token=0x600080C  System.Int32 CompareFloat(System.Object objA, System.Object objB)
  RVA=0x0A8CE63C  token=0x600080D  System.Boolean Operation(System.Linq.Expressions.ExpressionType operation, System.Object objA, System.Object objB, System.Object& result)
  RVA=0x0A8CD718  token=0x600080E  Newtonsoft.Json.Linq.JToken CloneToken(Newtonsoft.Json.Linq.JsonCloneSettings settings)
  RVA=0x0A8CE3CC  token=0x600080F  Newtonsoft.Json.Linq.JValue CreateComment(System.String value)
  RVA=0x0A8CE430  token=0x6000810  Newtonsoft.Json.Linq.JValue CreateNull()
  RVA=0x0A8CE488  token=0x6000811  Newtonsoft.Json.Linq.JValue CreateUndefined()
  RVA=0x03427D00  token=0x6000812  Newtonsoft.Json.Linq.JTokenType GetValueType(System.Nullable<Newtonsoft.Json.Linq.JTokenType> current, System.Object value)
  RVA=0x03427F70  token=0x6000813  Newtonsoft.Json.Linq.JTokenType GetStringValueType(System.Nullable<Newtonsoft.Json.Linq.JTokenType> current)
  RVA=0x03426430  token=0x6000816  System.Void WriteTo(Newtonsoft.Json.JsonWriter writer, Newtonsoft.Json.JsonConverter[] converters)
  RVA=0x0A8CF368  token=0x6000817  System.Boolean ValuesEquals(Newtonsoft.Json.Linq.JValue v1, Newtonsoft.Json.Linq.JValue v2)
  RVA=0x0A8CE53C  token=0x6000818  System.Boolean Equals(Newtonsoft.Json.Linq.JValue other)
  RVA=0x0A8CE4E0  token=0x6000819  System.Boolean Equals(System.Object obj)
  RVA=0x0A8CE558  token=0x600081A  System.Int32 GetHashCode()
  RVA=0x04D50B60  token=0x600081B  System.String ToString()
  RVA=0x0A8CF358  token=0x600081C  System.String ToString(System.IFormatProvider formatProvider)
  RVA=0x0A8CF2AC  token=0x600081D  System.String ToString(System.String format, System.IFormatProvider formatProvider)
  RVA=0x0A8CE580  token=0x600081E  System.Dynamic.DynamicMetaObject GetMetaObject(System.Linq.Expressions.Expression parameter)
  RVA=0x0A8CED84  token=0x600081F  System.Int32 System.IComparable.CompareTo(System.Object obj)
  RVA=0x0A8CDA8C  token=0x6000820  System.Int32 CompareTo(Newtonsoft.Json.Linq.JValue obj)
  RVA=0x0A8CEE08  token=0x6000821  System.TypeCode System.IConvertible.GetTypeCode()
  RVA=0x0A8CEE68  token=0x6000822  System.Boolean System.IConvertible.ToBoolean(System.IFormatProvider provider)
  RVA=0x0A8CEF00  token=0x6000823  System.Char System.IConvertible.ToChar(System.IFormatProvider provider)
  RVA=0x0A8CF128  token=0x6000824  System.SByte System.IConvertible.ToSByte(System.IFormatProvider provider)
  RVA=0x0A8CEEB4  token=0x6000825  System.Byte System.IConvertible.ToByte(System.IFormatProvider provider)
  RVA=0x0A8CF044  token=0x6000826  System.Int16 System.IConvertible.ToInt16(System.IFormatProvider provider)
  RVA=0x0A8CF1C8  token=0x6000827  System.UInt16 System.IConvertible.ToUInt16(System.IFormatProvider provider)
  RVA=0x0A8CF090  token=0x6000828  System.Int32 System.IConvertible.ToInt32(System.IFormatProvider provider)
  RVA=0x0A8CF214  token=0x6000829  System.UInt32 System.IConvertible.ToUInt32(System.IFormatProvider provider)
  RVA=0x0A8CF0DC  token=0x600082A  System.Int64 System.IConvertible.ToInt64(System.IFormatProvider provider)
  RVA=0x0A8CF260  token=0x600082B  System.UInt64 System.IConvertible.ToUInt64(System.IFormatProvider provider)
  RVA=0x0A8CF174  token=0x600082C  System.Single System.IConvertible.ToSingle(System.IFormatProvider provider)
  RVA=0x0A8CEFF8  token=0x600082D  System.Double System.IConvertible.ToDouble(System.IFormatProvider provider)
  RVA=0x0A8CEF98  token=0x600082E  System.Decimal System.IConvertible.ToDecimal(System.IFormatProvider provider)
  RVA=0x0A8CEF4C  token=0x600082F  System.DateTime System.IConvertible.ToDateTime(System.IFormatProvider provider)
  RVA=0x0A8CF1C0  token=0x6000830  System.Object System.IConvertible.ToType(System.Type conversionType, System.IFormatProvider provider)
END_CLASS

CLASS: Newtonsoft.Json.Linq.LineInfoHandling
TYPE:  sealed struct
TOKEN: 0x20000F8
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.Linq.LineInfoHandlingIgnore  // const
  public    static  Newtonsoft.Json.Linq.LineInfoHandlingLoad  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.Serialization.CachedAttributeGetter`1
TYPE:  static class
TOKEN: 0x200009C
FIELDS:
  private   static readonly Newtonsoft.Json.Utilities.ThreadSafeStore<System.Object,T>TypeAttributeCache  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000424  T GetAttribute(System.Object type)
  RVA=-1  // not resolved  token=0x6000425  System.Void .cctor()
END_CLASS

CLASS: Newtonsoft.Json.Serialization.CamelCaseNamingStrategy
TYPE:  class
TOKEN: 0x200009D
SIZE:  0x18
EXTENDS: Newtonsoft.Json.Serialization.NamingStrategy
FIELDS:
METHODS:
  RVA=0x04DA5CA0  token=0x6000426  System.Void .ctor(System.Boolean processDictionaryKeys, System.Boolean overrideSpecifiedNames)
  RVA=0x04DA5C90  token=0x6000427  System.Void .ctor(System.Boolean processDictionaryKeys, System.Boolean overrideSpecifiedNames, System.Boolean processExtensionDataNames)
  RVA=0x041E1670  token=0x6000428  System.Void .ctor()
  RVA=0x0A8AFEB8  token=0x6000429  System.String ResolvePropertyName(System.String name)
END_CLASS

CLASS: Newtonsoft.Json.Serialization.DefaultContractResolver
TYPE:  class
TOKEN: 0x200009E
SIZE:  0x38
IMPLEMENTS: Newtonsoft.Json.Serialization.IContractResolver
FIELDS:
  private   static readonly Newtonsoft.Json.Serialization.IContractResolver_instance  // static @ 0x0
  private   static readonly System.String[]                 BlacklistedTypeNames  // static @ 0x8
  private   static readonly Newtonsoft.Json.JsonConverter[] BuiltInConverters  // static @ 0x10
  private   readonly Newtonsoft.Json.DefaultJsonNameTable_nameTable  // 0x10
  private   readonly Newtonsoft.Json.Utilities.ThreadSafeStore<System.Type,Newtonsoft.Json.Serialization.JsonContract>_contractCache  // 0x18
  private           System.Reflection.BindingFlags  <DefaultMembersSearchFlags>k__BackingField  // 0x20
  private           System.Boolean                  <SerializeCompilerGeneratedMembers>k__BackingField  // 0x24
  private           System.Boolean                  <IgnoreSerializableInterface>k__BackingField  // 0x25
  private           System.Boolean                  <IgnoreSerializableAttribute>k__BackingField  // 0x26
  private           System.Boolean                  <IgnoreIsSpecifiedMembers>k__BackingField  // 0x27
  private           System.Boolean                  <IgnoreShouldSerializeMembers>k__BackingField  // 0x28
  private           Newtonsoft.Json.Serialization.NamingStrategy<NamingStrategy>k__BackingField  // 0x30
PROPERTIES:
  Instance  get=0x0A8B67F8
  DefaultMembersSearchFlags  get=0x02B70F90  set=0x04D86330
  SerializeCompilerGeneratedMembers  get=0x04D86870
  IgnoreSerializableInterface  get=0x04D86930
  IgnoreSerializableAttribute  get=0x04D86940  set=0x04D86960
  IgnoreIsSpecifiedMembers  get=0x04DA5CB0
  IgnoreShouldSerializeMembers  get=0x011F33C0
  NamingStrategy  get=0x011F36E0  set=0x05396200
METHODS:
  RVA=0x0464D460  token=0x6000435  System.Void .ctor()
  RVA=0x02FBE740  token=0x6000436  Newtonsoft.Json.Serialization.JsonContract ResolveContract(System.Type type)
  RVA=0x032B3490  token=0x6000437  System.Boolean FilterMembers(System.Reflection.MemberInfo member)
  RVA=0x02F30F80  token=0x6000438  System.Collections.Generic.List<System.Reflection.MemberInfo> GetSerializableMembers(System.Type objectType)
  RVA=0x0A8B6694  token=0x6000439  System.Boolean ShouldSerializeEntityMember(System.Reflection.MemberInfo memberInfo)
  RVA=0x035CE720  token=0x600043A  Newtonsoft.Json.Serialization.JsonObjectContract CreateObjectContract(System.Type objectType)
  RVA=0x0A8B6750  token=0x600043B  System.Void ThrowUnableToSerializeError(System.Object o, System.Runtime.Serialization.StreamingContext context)
  RVA=0x035CEB30  token=0x600043C  System.Reflection.MemberInfo GetExtensionDataMemberForType(System.Type type)
  RVA=0x0A8B5E64  token=0x600043D  System.Void SetExtensionDataDelegates(Newtonsoft.Json.Serialization.JsonObjectContract contract, System.Reflection.MemberInfo member)
  RVA=0x035CE410  token=0x600043E  System.Reflection.ConstructorInfo GetAttributeConstructor(System.Type objectType)
  RVA=0x043AA090  token=0x600043F  System.Reflection.ConstructorInfo GetImmutableConstructor(System.Type objectType, Newtonsoft.Json.Serialization.JsonPropertyCollection memberProperties)
  RVA=0x04CDE4B0  token=0x6000440  System.Reflection.ConstructorInfo GetParameterizedConstructor(System.Type objectType)
  RVA=0x043A9F60  token=0x6000441  System.Collections.Generic.IList<Newtonsoft.Json.Serialization.JsonProperty> CreateConstructorParameters(System.Reflection.ConstructorInfo constructor, Newtonsoft.Json.Serialization.JsonPropertyCollection memberProperties)
  RVA=0x043AA210  token=0x6000442  Newtonsoft.Json.Serialization.JsonProperty MatchProperty(Newtonsoft.Json.Serialization.JsonPropertyCollection properties, System.String name, System.Type type)
  RVA=0x043AA2C0  token=0x6000443  Newtonsoft.Json.Serialization.JsonProperty CreatePropertyFromConstructorParameter(Newtonsoft.Json.Serialization.JsonProperty matchingMemberProperty, System.Reflection.ParameterInfo parameterInfo)
  RVA=0x035CF340  token=0x6000444  Newtonsoft.Json.JsonConverter ResolveContractConverter(System.Type objectType)
  RVA=0x035CF4F0  token=0x6000445  System.Func<System.Object> GetDefaultCreator(System.Type createdType)
  RVA=0x035CF130  token=0x6000446  System.Void InitializeContract(Newtonsoft.Json.Serialization.JsonContract contract)
  RVA=0x035CF370  token=0x6000447  System.Void ResolveCallbackMethods(Newtonsoft.Json.Serialization.JsonContract contract, System.Type t)
  RVA=0x02F2FB00  token=0x6000448  System.Void GetCallbackMethodsForType(System.Type type, System.Collections.Generic.List<Newtonsoft.Json.Serialization.SerializationCallback>& onSerializing, System.Collections.Generic.List<Newtonsoft.Json.Serialization.SerializationCallback>& onSerialized, System.Collections.Generic.List<Newtonsoft.Json.Serialization.SerializationCallback>& onDeserializing, System.Collections.Generic.List<Newtonsoft.Json.Serialization.SerializationCallback>& onDeserialized, System.Collections.Generic.List<Newtonsoft.Json.Serialization.SerializationErrorCallback>& onError)
  RVA=0x035CFCB0  token=0x6000449  System.Boolean IsConcurrentOrObservableCollection(System.Type t)
  RVA=0x035CFA20  token=0x600044A  System.Boolean ShouldSkipDeserialized(System.Type t)
  RVA=0x035CFA20  token=0x600044B  System.Boolean ShouldSkipSerializing(System.Type t)
  RVA=0x02F2E080  token=0x600044C  System.Collections.Generic.List<System.Type> GetClassHierarchyForType(System.Type type)
  RVA=0x035CDAA0  token=0x600044D  Newtonsoft.Json.Serialization.JsonDictionaryContract CreateDictionaryContract(System.Type objectType)
  RVA=0x035CDCD0  token=0x600044E  Newtonsoft.Json.Serialization.JsonArrayContract CreateArrayContract(System.Type objectType)
  RVA=0x035CDE00  token=0x600044F  Newtonsoft.Json.Serialization.JsonPrimitiveContract CreatePrimitiveContract(System.Type objectType)
  RVA=0x04D26960  token=0x6000450  Newtonsoft.Json.Serialization.JsonLinqContract CreateLinqContract(System.Type objectType)
  RVA=0x0A8B5B90  token=0x6000451  Newtonsoft.Json.Serialization.JsonISerializableContract CreateISerializableContract(System.Type objectType)
  RVA=0x0A8B59A0  token=0x6000452  Newtonsoft.Json.Serialization.JsonDynamicContract CreateDynamicContract(System.Type objectType)
  RVA=0x04CCFB90  token=0x6000453  Newtonsoft.Json.Serialization.JsonStringContract CreateStringContract(System.Type objectType)
  RVA=0x035D07D0  token=0x6000454  Newtonsoft.Json.Serialization.JsonContract CreateContract(System.Type objectType)
  RVA=0x035D0CC0  token=0x6000455  System.Boolean IsJsonPrimitiveType(System.Type t)
  RVA=0x035CE690  token=0x6000456  System.Boolean IsIConvertible(System.Type t)
  RVA=0x035CED80  token=0x6000457  System.Boolean CanConvertToString(System.Type type)
  RVA=0x02F30CB0  token=0x6000458  System.Boolean IsValidCallback(System.Reflection.MethodInfo method, System.Reflection.ParameterInfo[] parameters, System.Type attributeType, System.Reflection.MethodInfo currentCallback, System.Type& prevAttributeType)
  RVA=0x0A8B5D6C  token=0x6000459  System.String GetClrTypeFullName(System.Type type)
  RVA=0x032B3570  token=0x600045A  System.Collections.Generic.IList<Newtonsoft.Json.Serialization.JsonProperty> CreateProperties(System.Type type, Newtonsoft.Json.MemberSerialization memberSerialization)
  RVA=0x02B2ECC0  token=0x600045B  Newtonsoft.Json.DefaultJsonNameTable GetNameTable()
  RVA=0x032203A0  token=0x600045C  Newtonsoft.Json.Serialization.IValueProvider CreateMemberValueProvider(System.Reflection.MemberInfo member)
  RVA=0x03220060  token=0x600045D  Newtonsoft.Json.Serialization.JsonProperty CreateProperty(System.Reflection.MemberInfo member, Newtonsoft.Json.MemberSerialization memberSerialization)
  RVA=0x032B41E0  token=0x600045E  System.Void SetPropertySettingsFromAttributes(Newtonsoft.Json.Serialization.JsonProperty property, System.Object attributeProvider, System.String name, System.Type declaringType, Newtonsoft.Json.MemberSerialization memberSerialization, System.Boolean& allowNonPublicAccess)
  RVA=0x03220500  token=0x600045F  System.Predicate<System.Object> CreateShouldSerializeTest(System.Reflection.MemberInfo member)
  RVA=0x032206A0  token=0x6000460  System.Void SetIsSpecifiedActions(Newtonsoft.Json.Serialization.JsonProperty property, System.Reflection.MemberInfo member, System.Boolean allowNonPublicAccess)
  RVA=0x032B4800  token=0x6000461  System.String ResolvePropertyName(System.String propertyName)
  RVA=0x0A8B5E48  token=0x6000462  System.String ResolveExtensionDataName(System.String extensionDataName)
  RVA=0x048BDB70  token=0x6000463  System.String ResolveDictionaryKey(System.String dictionaryKey)
  RVA=0x0A8B5E40  token=0x6000464  System.String GetResolvedPropertyName(System.String propertyName)
  RVA=0x0464D020  token=0x6000465  System.Void .cctor()
END_CLASS

CLASS: Newtonsoft.Json.Serialization.DefaultNamingStrategy
TYPE:  class
TOKEN: 0x20000AA
SIZE:  0x18
EXTENDS: Newtonsoft.Json.Serialization.NamingStrategy
FIELDS:
METHODS:
  RVA=0x04D88C30  token=0x6000487  System.String ResolvePropertyName(System.String name)
  RVA=0x041E1670  token=0x6000488  System.Void .ctor()
END_CLASS

CLASS: Newtonsoft.Json.Serialization.DefaultReferenceResolver
TYPE:  class
TOKEN: 0x20000AB
SIZE:  0x18
IMPLEMENTS: Newtonsoft.Json.Serialization.IReferenceResolver
FIELDS:
  private           System.Int32                    _referenceCount  // 0x10
METHODS:
  RVA=0x0A8BAF3C  token=0x6000489  Newtonsoft.Json.Utilities.BidirectionalDictionary<System.String,System.Object> GetMappings(System.Object context)
  RVA=0x0A8BB164  token=0x600048A  System.Object ResolveReference(System.Object context, System.String reference)
  RVA=0x0A8BB008  token=0x600048B  System.String GetReference(System.Object context, System.Object value)
  RVA=0x0A8BAEBC  token=0x600048C  System.Void AddReference(System.Object context, System.String reference, System.Object value)
  RVA=0x0A8BB0E8  token=0x600048D  System.Boolean IsReferenced(System.Object context, System.Object value)
  RVA=0x041E1670  token=0x600048E  System.Void .ctor()
END_CLASS

CLASS: Newtonsoft.Json.Serialization.DefaultSerializationBinder
TYPE:  class
TOKEN: 0x20000AC
SIZE:  0x18
EXTENDS: System.Runtime.Serialization.SerializationBinder
IMPLEMENTS: Newtonsoft.Json.Serialization.ISerializationBinder
FIELDS:
  private   static readonly Newtonsoft.Json.Serialization.DefaultSerializationBinderInstance  // static @ 0x0
  private   readonly Newtonsoft.Json.Utilities.ThreadSafeStore<Newtonsoft.Json.Utilities.StructMultiKey<System.String,System.String>,System.Type>_typeCache  // 0x10
METHODS:
  RVA=0x04B460F0  token=0x600048F  System.Void .ctor()
  RVA=0x03B110C0  token=0x6000490  System.Type GetTypeFromTypeNameKey(Newtonsoft.Json.Utilities.StructMultiKey<System.String,System.String> typeNameKey)
  RVA=0x0A8BB258  token=0x6000491  System.Type GetGenericTypeFromTypeName(System.String typeName, System.Reflection.Assembly assembly)
  RVA=0x02FBD550  token=0x6000492  System.Type GetTypeByName(Newtonsoft.Json.Utilities.StructMultiKey<System.String,System.String> typeNameKey)
  RVA=0x02FBD4E0  token=0x6000493  System.Type BindToType(System.String assemblyName, System.String typeName)
  RVA=0x0A8BB1E4  token=0x6000494  System.Void BindToName(System.Type serializedType, System.String& assemblyName, System.String& typeName)
  RVA=0x04B46080  token=0x6000495  System.Void .cctor()
END_CLASS

CLASS: Newtonsoft.Json.Serialization.ErrorContext
TYPE:  class
TOKEN: 0x20000AD
SIZE:  0x40
FIELDS:
  private           System.Boolean                  <Traced>k__BackingField  // 0x10
  private   readonly System.Exception                <Error>k__BackingField  // 0x18
  private   readonly System.Object                   <OriginalObject>k__BackingField  // 0x20
  private   readonly System.Object                   <Member>k__BackingField  // 0x28
  private   readonly System.String                   <Path>k__BackingField  // 0x30
  private           System.Boolean                  <Handled>k__BackingField  // 0x38
PROPERTIES:
  Traced  get=0x015EFCE0  set=0x04D86130
  Error  get=0x0385B100
  Handled  get=0x011F2A70
METHODS:
  RVA=0x0A8BB468  token=0x6000496  System.Void .ctor(System.Object originalObject, System.Object member, System.String path, System.Exception error)
END_CLASS

CLASS: Newtonsoft.Json.Serialization.ErrorEventArgs
TYPE:  class
TOKEN: 0x20000AE
SIZE:  0x20
EXTENDS: System.EventArgs
FIELDS:
  private   readonly System.Object                   <CurrentObject>k__BackingField  // 0x10
  private   readonly Newtonsoft.Json.Serialization.ErrorContext<ErrorContext>k__BackingField  // 0x18
METHODS:
  RVA=0x0A8BB4B0  token=0x600049B  System.Void .ctor(System.Object currentObject, Newtonsoft.Json.Serialization.ErrorContext errorContext)
END_CLASS

CLASS: Newtonsoft.Json.Serialization.IAttributeProvider
TYPE:  interface
TOKEN: 0x20000AF
FIELDS:
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.Serialization.IContractResolver
TYPE:  interface
TOKEN: 0x20000B0
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600049C  Newtonsoft.Json.Serialization.JsonContract ResolveContract(System.Type type)
END_CLASS

CLASS: Newtonsoft.Json.Serialization.IReferenceResolver
TYPE:  interface
TOKEN: 0x20000B1
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600049D  System.Object ResolveReference(System.Object context, System.String reference)
  RVA=-1  // abstract  token=0x600049E  System.String GetReference(System.Object context, System.Object value)
  RVA=-1  // abstract  token=0x600049F  System.Boolean IsReferenced(System.Object context, System.Object value)
  RVA=-1  // abstract  token=0x60004A0  System.Void AddReference(System.Object context, System.String reference, System.Object value)
END_CLASS

CLASS: Newtonsoft.Json.Serialization.ISerializationBinder
TYPE:  interface
TOKEN: 0x20000B2
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60004A1  System.Type BindToType(System.String assemblyName, System.String typeName)
  RVA=-1  // abstract  token=0x60004A2  System.Void BindToName(System.Type serializedType, System.String& assemblyName, System.String& typeName)
END_CLASS

CLASS: Newtonsoft.Json.Serialization.ITraceWriter
TYPE:  interface
TOKEN: 0x20000B3
FIELDS:
PROPERTIES:
  LevelFilter  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x60004A4  System.Void Trace(System.Diagnostics.TraceLevel level, System.String message, System.Exception ex)
END_CLASS

CLASS: Newtonsoft.Json.Serialization.IValueProvider
TYPE:  interface
TOKEN: 0x20000B4
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60004A5  System.Void SetValue(System.Object target, System.Object value)
  RVA=-1  // abstract  token=0x60004A6  System.Object GetValue(System.Object target)
END_CLASS

CLASS: Newtonsoft.Json.Serialization.JsonArrayContract
TYPE:  class
TOKEN: 0x20000B5
SIZE:  0x118
EXTENDS: Newtonsoft.Json.Serialization.JsonContainerContract
FIELDS:
  private   readonly System.Type                     <CollectionItemType>k__BackingField  // 0xc0
  private   readonly System.Boolean                  <IsMultidimensionalArray>k__BackingField  // 0xc8
  private   readonly System.Type                     _genericCollectionDefinitionType  // 0xd0
  private           System.Type                     _genericWrapperType  // 0xd8
  private           Newtonsoft.Json.Serialization.ObjectConstructor<System.Object>_genericWrapperCreator  // 0xe0
  private           System.Func<System.Object>      _genericTemporaryCollectionCreator  // 0xe8
  private   readonly System.Boolean                  <IsArray>k__BackingField  // 0xf0
  private   readonly System.Boolean                  <ShouldCreateWrapper>k__BackingField  // 0xf1
  private           System.Boolean                  <CanDeserialize>k__BackingField  // 0xf2
  private   readonly System.Reflection.ConstructorInfo_parameterizedConstructor  // 0xf8
  private           Newtonsoft.Json.Serialization.ObjectConstructor<System.Object>_parameterizedCreator  // 0x100
  private           Newtonsoft.Json.Serialization.ObjectConstructor<System.Object>_overrideCreator  // 0x108
  private           System.Boolean                  <HasParameterizedCreator>k__BackingField  // 0x110
PROPERTIES:
  CollectionItemType  get=0x04D892F0
  IsMultidimensionalArray  get=0x04D8E270
  IsArray  get=0x04D8E520
  ShouldCreateWrapper  get=0x04D9B5F0
  CanDeserialize  get=0x04DA5CC0  set=0x04DA5CD0
  ParameterizedCreator  get=0x0A8BB71C
  OverrideCreator  get=0x04D85F90  set=0x0A8BB7D4
  HasParameterizedCreator  get=0x04D90FE0  set=0x04D91070
  HasParameterizedCreatorInternal  get=0x032B5600
METHODS:
  RVA=0x035CD250  token=0x60004B3  System.Void .ctor(System.Type underlyingType)
  RVA=0x03B2C5A0  token=0x60004B4  Newtonsoft.Json.Utilities.IWrappedCollection CreateWrapper(System.Object list)
  RVA=0x032B5730  token=0x60004B5  System.Collections.IList CreateTemporaryCollection()
  RVA=0x0A8BB644  token=0x60004B6  System.Void StoreFSharpListCreatorIfNecessary(System.Type underlyingType)
END_CLASS

CLASS: Newtonsoft.Json.Serialization.JsonContainerContract
TYPE:  class
TOKEN: 0x20000B6
SIZE:  0xC0
EXTENDS: Newtonsoft.Json.Serialization.JsonContract
FIELDS:
  private           Newtonsoft.Json.Serialization.JsonContract_itemContract  // 0x90
  private           Newtonsoft.Json.Serialization.JsonContract_finalItemContract  // 0x98
  private           Newtonsoft.Json.JsonConverter   <ItemConverter>k__BackingField  // 0xa0
  private           System.Nullable<System.Boolean> <ItemIsReference>k__BackingField  // 0xa8
  private           System.Nullable<Newtonsoft.Json.ReferenceLoopHandling><ItemReferenceLoopHandling>k__BackingField  // 0xac
  private           System.Nullable<Newtonsoft.Json.TypeNameHandling><ItemTypeNameHandling>k__BackingField  // 0xb4
PROPERTIES:
  ItemContract  get=0x04D86220  set=0x043C1090
  FinalItemContract  get=0x04D85EE0
  ItemConverter  get=0x04D86210  set=0x05391334
  ItemIsReference  get=0x04DA5CE0  set=0x04DA5D10
  ItemReferenceLoopHandling  get=0x04DA5CF0  set=0x04DA5D20
  ItemTypeNameHandling  get=0x04DA5D00  set=0x04DA5D30
METHODS:
  RVA=0x035CF090  token=0x60004C2  System.Void .ctor(System.Type underlyingType)
END_CLASS

CLASS: Newtonsoft.Json.Serialization.JsonContractType
TYPE:  sealed struct
TOKEN: 0x20000B7
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.Serialization.JsonContractTypeNone  // const
  public    static  Newtonsoft.Json.Serialization.JsonContractTypeObject  // const
  public    static  Newtonsoft.Json.Serialization.JsonContractTypeArray  // const
  public    static  Newtonsoft.Json.Serialization.JsonContractTypePrimitive  // const
  public    static  Newtonsoft.Json.Serialization.JsonContractTypeString  // const
  public    static  Newtonsoft.Json.Serialization.JsonContractTypeDictionary  // const
  public    static  Newtonsoft.Json.Serialization.JsonContractTypeDynamic  // const
  public    static  Newtonsoft.Json.Serialization.JsonContractTypeSerializable  // const
  public    static  Newtonsoft.Json.Serialization.JsonContractTypeLinq  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.Serialization.SerializationCallback
TYPE:  sealed class
TOKEN: 0x20000B8
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x045D8FF0  token=0x60004C3  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x03A1B910  token=0x60004C4  System.Void Invoke(System.Object o, System.Runtime.Serialization.StreamingContext context)
END_CLASS

CLASS: Newtonsoft.Json.Serialization.SerializationErrorCallback
TYPE:  sealed class
TOKEN: 0x20000B9
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0966D3A4  token=0x60004C5  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x0621CA6C  token=0x60004C6  System.Void Invoke(System.Object o, System.Runtime.Serialization.StreamingContext context, Newtonsoft.Json.Serialization.ErrorContext errorContext)
END_CLASS

CLASS: Newtonsoft.Json.Serialization.ExtensionDataSetter
TYPE:  sealed class
TOKEN: 0x20000BA
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0A8BB51C  token=0x60004C7  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x01546280  token=0x60004C8  System.Void Invoke(System.Object o, System.String key, System.Object value)
END_CLASS

CLASS: Newtonsoft.Json.Serialization.ExtensionDataGetter
TYPE:  sealed class
TOKEN: 0x20000BB
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0832435C  token=0x60004C9  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x014E2060  token=0x60004CA  System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Object,System.Object>> Invoke(System.Object o)
END_CLASS

CLASS: Newtonsoft.Json.Serialization.JsonContract
TYPE:  abstract class
TOKEN: 0x20000BC
SIZE:  0x90
FIELDS:
  private           System.Boolean                  IsNullable  // 0x10
  private           System.Boolean                  IsConvertable  // 0x11
  private           System.Boolean                  IsEnum  // 0x12
  private           System.Type                     NonNullableUnderlyingType  // 0x18
  private           Newtonsoft.Json.ReadType        InternalReadType  // 0x20
  private           Newtonsoft.Json.Serialization.JsonContractTypeContractType  // 0x24
  private           System.Boolean                  IsReadOnlyOrFixedSize  // 0x28
  private           System.Boolean                  IsSealed  // 0x29
  private           System.Boolean                  IsInstantiable  // 0x2a
  private           System.Collections.Generic.List<Newtonsoft.Json.Serialization.SerializationCallback>_onDeserializedCallbacks  // 0x30
  private           System.Collections.Generic.List<Newtonsoft.Json.Serialization.SerializationCallback>_onDeserializingCallbacks  // 0x38
  private           System.Collections.Generic.List<Newtonsoft.Json.Serialization.SerializationCallback>_onSerializedCallbacks  // 0x40
  private           System.Collections.Generic.List<Newtonsoft.Json.Serialization.SerializationCallback>_onSerializingCallbacks  // 0x48
  private           System.Collections.Generic.List<Newtonsoft.Json.Serialization.SerializationErrorCallback>_onErrorCallbacks  // 0x50
  private           System.Type                     _createdType  // 0x58
  private   readonly System.Type                     <UnderlyingType>k__BackingField  // 0x60
  private           System.Nullable<System.Boolean> <IsReference>k__BackingField  // 0x68
  private           Newtonsoft.Json.JsonConverter   <Converter>k__BackingField  // 0x70
  private           Newtonsoft.Json.JsonConverter   <InternalConverter>k__BackingField  // 0x78
  private           System.Func<System.Object>      <DefaultCreator>k__BackingField  // 0x80
  private           System.Boolean                  <DefaultCreatorNonPublic>k__BackingField  // 0x88
PROPERTIES:
  UnderlyingType  get=0x04D86280
  CreatedType  get=0x02E56440  set=0x035CF820
  IsReference  get=0x04DA5D40  set=0x04DA5D50
  Converter  get=0x04D862B0  set=0x063AD760
  InternalConverter  get=0x04D862A0  set=0x06402320
  OnDeserializedCallbacks  get=0x0484C760
  OnDeserializingCallbacks  get=0x0A8BB9FC
  OnSerializedCallbacks  get=0x0A8BBA80
  OnSerializingCallbacks  get=0x032B56D0
  OnErrorCallbacks  get=0x032B5670
  DefaultCreator  get=0x04D86260  set=0x04CD9970
  DefaultCreatorNonPublic  get=0x011F2750  set=0x011F2760
METHODS:
  RVA=0x035D0B60  token=0x60004DD  System.Void .ctor(System.Type underlyingType)
  RVA=0x04386DC0  token=0x60004DE  System.Void InvokeOnSerializing(System.Object o, System.Runtime.Serialization.StreamingContext context)
  RVA=0x04386C90  token=0x60004DF  System.Void InvokeOnSerialized(System.Object o, System.Runtime.Serialization.StreamingContext context)
  RVA=0x032B6CF0  token=0x60004E0  System.Void InvokeOnDeserializing(System.Object o, System.Runtime.Serialization.StreamingContext context)
  RVA=0x032B6E30  token=0x60004E1  System.Void InvokeOnDeserialized(System.Object o, System.Runtime.Serialization.StreamingContext context)
  RVA=0x0A8BB89C  token=0x60004E2  System.Void InvokeOnError(System.Object o, System.Runtime.Serialization.StreamingContext context, Newtonsoft.Json.Serialization.ErrorContext errorContext)
  RVA=0x045D8F40  token=0x60004E3  Newtonsoft.Json.Serialization.SerializationCallback CreateSerializationCallback(System.Reflection.MethodInfo callbackMethodInfo)
  RVA=0x0A8BB7FC  token=0x60004E4  Newtonsoft.Json.Serialization.SerializationErrorCallback CreateSerializationErrorCallback(System.Reflection.MethodInfo callbackMethodInfo)
END_CLASS

CLASS: Newtonsoft.Json.Serialization.JsonDictionaryContract
TYPE:  class
TOKEN: 0x20000BF
SIZE:  0x128
EXTENDS: Newtonsoft.Json.Serialization.JsonContainerContract
FIELDS:
  private           System.Func<System.String,System.String><DictionaryKeyResolver>k__BackingField  // 0xc0
  private   readonly System.Type                     <DictionaryKeyType>k__BackingField  // 0xc8
  private   readonly System.Type                     <DictionaryValueType>k__BackingField  // 0xd0
  private           Newtonsoft.Json.Serialization.JsonContract<KeyContract>k__BackingField  // 0xd8
  private   readonly System.Type                     _genericCollectionDefinitionType  // 0xe0
  private           System.Type                     _genericWrapperType  // 0xe8
  private           Newtonsoft.Json.Serialization.ObjectConstructor<System.Object>_genericWrapperCreator  // 0xf0
  private           System.Func<System.Object>      _genericTemporaryDictionaryCreator  // 0xf8
  private   readonly System.Boolean                  <ShouldCreateWrapper>k__BackingField  // 0x100
  private   readonly System.Reflection.ConstructorInfo_parameterizedConstructor  // 0x108
  private           Newtonsoft.Json.Serialization.ObjectConstructor<System.Object>_overrideCreator  // 0x110
  private           Newtonsoft.Json.Serialization.ObjectConstructor<System.Object>_parameterizedCreator  // 0x118
  private           System.Boolean                  <HasParameterizedCreator>k__BackingField  // 0x120
PROPERTIES:
  DictionaryKeyResolver  get=0x04D892F0  set=0x06AC5ACC
  DictionaryKeyType  get=0x04D8D1D0
  DictionaryValueType  get=0x04D876B0
  KeyContract  get=0x04D88EF0  set=0x06902698
  ShouldCreateWrapper  get=0x04DA5D60
  ParameterizedCreator  get=0x0A8BBF20
  OverrideCreator  get=0x04D8D1B0  set=0x05397170
  HasParameterizedCreator  get=0x04D8FE70  set=0x04D8FEA0
  HasParameterizedCreatorInternal  get=0x044FE340
METHODS:
  RVA=0x03897F20  token=0x60004F6  System.Void .ctor(System.Type underlyingType)
  RVA=0x0A8BBCE0  token=0x60004F7  Newtonsoft.Json.Utilities.IWrappedDictionary CreateWrapper(System.Object dictionary)
  RVA=0x0A8BBB04  token=0x60004F8  System.Collections.IDictionary CreateTemporaryDictionary()
END_CLASS

CLASS: Newtonsoft.Json.Serialization.JsonDynamicContract
TYPE:  class
TOKEN: 0x20000C0
SIZE:  0xE0
EXTENDS: Newtonsoft.Json.Serialization.JsonContainerContract
FIELDS:
  private   readonly Newtonsoft.Json.Serialization.JsonPropertyCollection<Properties>k__BackingField  // 0xc0
  private           System.Func<System.String,System.String><PropertyNameResolver>k__BackingField  // 0xc8
  private   readonly Newtonsoft.Json.Utilities.ThreadSafeStore<System.String,System.Runtime.CompilerServices.CallSite<System.Func<System.Runtime.CompilerServices.CallSite,System.Object,System.Object>>>_callSiteGetters  // 0xd0
  private   readonly Newtonsoft.Json.Utilities.ThreadSafeStore<System.String,System.Runtime.CompilerServices.CallSite<System.Func<System.Runtime.CompilerServices.CallSite,System.Object,System.Object,System.Object>>>_callSiteSetters  // 0xd8
PROPERTIES:
  Properties  get=0x04D892F0
  PropertyNameResolver  get=0x04D8D1D0  set=0x06AC5B30
METHODS:
  RVA=0x0A8BBFD8  token=0x60004FC  System.Runtime.CompilerServices.CallSite<System.Func<System.Runtime.CompilerServices.CallSite,System.Object,System.Object>> CreateCallSiteGetter(System.String name)
  RVA=0x0A8BC0B0  token=0x60004FD  System.Runtime.CompilerServices.CallSite<System.Func<System.Runtime.CompilerServices.CallSite,System.Object,System.Object,System.Object>> CreateCallSiteSetter(System.String name)
  RVA=0x0A8BC3E0  token=0x60004FE  System.Void .ctor(System.Type underlyingType)
  RVA=0x0A8BC188  token=0x60004FF  System.Boolean TryGetMember(System.Dynamic.IDynamicMetaObjectProvider dynamicProvider, System.String name, System.Object& value)
  RVA=0x0A8BC2BC  token=0x6000500  System.Boolean TrySetMember(System.Dynamic.IDynamicMetaObjectProvider dynamicProvider, System.String name, System.Object value)
END_CLASS

CLASS: Newtonsoft.Json.Serialization.JsonFormatterConverter
TYPE:  class
TOKEN: 0x20000C1
SIZE:  0x28
IMPLEMENTS: System.Runtime.Serialization.IFormatterConverter
FIELDS:
  private   readonly Newtonsoft.Json.Serialization.JsonSerializerInternalReader_reader  // 0x10
  private   readonly Newtonsoft.Json.Serialization.JsonISerializableContract_contract  // 0x18
  private   readonly Newtonsoft.Json.Serialization.JsonProperty_member  // 0x20
METHODS:
  RVA=0x0A8BC85C  token=0x6000501  System.Void .ctor(Newtonsoft.Json.Serialization.JsonSerializerInternalReader reader, Newtonsoft.Json.Serialization.JsonISerializableContract contract, Newtonsoft.Json.Serialization.JsonProperty member)
  RVA=-1  // generic def  token=0x6000502  T GetTokenValue(System.Object value)
  RVA=0x0A8BC5B0  token=0x6000503  System.Object Convert(System.Object value, System.Type type)
  RVA=0x0A8BC6F4  token=0x6000504  System.Boolean ToBoolean(System.Object value)
  RVA=0x0A8BC73C  token=0x6000505  System.Int32 ToInt32(System.Object value)
  RVA=0x0A8BC784  token=0x6000506  System.Int64 ToInt64(System.Object value)
  RVA=0x0A8BC7CC  token=0x6000507  System.Single ToSingle(System.Object value)
  RVA=0x0A8BC814  token=0x6000508  System.String ToString(System.Object value)
END_CLASS

CLASS: Newtonsoft.Json.Serialization.JsonISerializableContract
TYPE:  class
TOKEN: 0x20000C2
SIZE:  0xC8
EXTENDS: Newtonsoft.Json.Serialization.JsonContainerContract
FIELDS:
  private           Newtonsoft.Json.Serialization.ObjectConstructor<System.Object><ISerializableCreator>k__BackingField  // 0xc0
PROPERTIES:
  ISerializableCreator  get=0x04D892F0  set=0x06AC5ACC
METHODS:
  RVA=0x0A8BC984  token=0x600050B  System.Void .ctor(System.Type underlyingType)
END_CLASS

CLASS: Newtonsoft.Json.Serialization.JsonLinqContract
TYPE:  class
TOKEN: 0x20000C3
SIZE:  0x90
EXTENDS: Newtonsoft.Json.Serialization.JsonContract
FIELDS:
METHODS:
  RVA=0x0539821C  token=0x600050C  System.Void .ctor(System.Type underlyingType)
END_CLASS

CLASS: Newtonsoft.Json.Serialization.JsonObjectContract
TYPE:  class
TOKEN: 0x20000C4
SIZE:  0x128
EXTENDS: Newtonsoft.Json.Serialization.JsonContainerContract
FIELDS:
  private           Newtonsoft.Json.MemberSerialization<MemberSerialization>k__BackingField  // 0xc0
  private           System.Nullable<Newtonsoft.Json.MissingMemberHandling><MissingMemberHandling>k__BackingField  // 0xc4
  private           System.Nullable<Newtonsoft.Json.Required><ItemRequired>k__BackingField  // 0xcc
  private           System.Nullable<Newtonsoft.Json.NullValueHandling><ItemNullValueHandling>k__BackingField  // 0xd4
  private   readonly Newtonsoft.Json.Serialization.JsonPropertyCollection<Properties>k__BackingField  // 0xe0
  private           Newtonsoft.Json.Serialization.ExtensionDataSetter<ExtensionDataSetter>k__BackingField  // 0xe8
  private           Newtonsoft.Json.Serialization.ExtensionDataGetter<ExtensionDataGetter>k__BackingField  // 0xf0
  private           System.Func<System.String,System.String><ExtensionDataNameResolver>k__BackingField  // 0xf8
  private           System.Boolean                  ExtensionDataIsJToken  // 0x100
  private           System.Nullable<System.Boolean> _hasRequiredOrDefaultValueProperties  // 0x101
  private           Newtonsoft.Json.Serialization.ObjectConstructor<System.Object>_overrideCreator  // 0x108
  private           Newtonsoft.Json.Serialization.ObjectConstructor<System.Object>_parameterizedCreator  // 0x110
  private           Newtonsoft.Json.Serialization.JsonPropertyCollection_creatorParameters  // 0x118
  private           System.Type                     _extensionDataValueType  // 0x120
PROPERTIES:
  MemberSerialization  get=0x04DA4B70  set=0x04DA5DC0
  MissingMemberHandling  get=0x04DA5D90  set=0x04D9E430
  ItemRequired  get=0x04DA5D80  set=0x04DA5DB0
  ItemNullValueHandling  get=0x04DA5D70  set=0x04DA5DA0
  Properties  get=0x04D8D1C0
  CreatorParameters  get=0x03B2CBA0
  OverrideCreator  get=0x04D85F90  set=0x068C2E50
  ParameterizedCreator  get=0x04D8D1B0  set=0x05397170
  ExtensionDataSetter  get=0x04D88590  set=0x06AC5B1C
  ExtensionDataGetter  get=0x04D8D1A0  set=0x06AC5AE0
  ExtensionDataValueType  set=0x0A8BCAA0
  ExtensionDataNameResolver  get=0x04D877C0  set=0x06790298
  HasRequiredOrDefaultValueProperties  get=0x03B2C340
METHODS:
  RVA=0x035CECF0  token=0x6000523  System.Void .ctor(System.Type underlyingType)
  RVA=0x0A8BC9A4  token=0x6000524  System.Object GetUninitializedObject()
END_CLASS

CLASS: Newtonsoft.Json.Serialization.JsonPrimitiveContract
TYPE:  class
TOKEN: 0x20000C5
SIZE:  0x98
EXTENDS: Newtonsoft.Json.Serialization.JsonContract
FIELDS:
  private           Newtonsoft.Json.Utilities.PrimitiveTypeCode<TypeCode>k__BackingField  // 0x90
  private   static readonly System.Collections.Generic.Dictionary<System.Type,Newtonsoft.Json.ReadType>ReadTypeMap  // static @ 0x0
PROPERTIES:
  TypeCode  get=0x04D86B60  set=0x04D86BC0
METHODS:
  RVA=0x035CDE60  token=0x6000527  System.Void .ctor(System.Type underlyingType)
  RVA=0x04825860  token=0x6000528  System.Void .cctor()
END_CLASS

CLASS: Newtonsoft.Json.Serialization.JsonProperty
TYPE:  class
TOKEN: 0x20000C6
SIZE:  0xF0
FIELDS:
  private           System.Nullable<Newtonsoft.Json.Required>_required  // 0x10
  private           System.Boolean                  _hasExplicitDefaultValue  // 0x18
  private           System.Object                   _defaultValue  // 0x20
  private           System.Boolean                  _hasGeneratedDefaultValue  // 0x28
  private           System.String                   _propertyName  // 0x30
  private           System.Boolean                  _skipPropertyNameEscape  // 0x38
  private           System.Type                     _propertyType  // 0x40
  private           Newtonsoft.Json.Serialization.JsonContract<PropertyContract>k__BackingField  // 0x48
  private           System.Type                     <DeclaringType>k__BackingField  // 0x50
  private           System.Nullable<System.Int32>   <Order>k__BackingField  // 0x58
  private           System.String                   <UnderlyingName>k__BackingField  // 0x60
  private           Newtonsoft.Json.Serialization.IValueProvider<ValueProvider>k__BackingField  // 0x68
  private           Newtonsoft.Json.Serialization.IAttributeProvider<AttributeProvider>k__BackingField  // 0x70
  private           Newtonsoft.Json.JsonConverter   <Converter>k__BackingField  // 0x78
  private           System.Boolean                  <Ignored>k__BackingField  // 0x80
  private           System.Boolean                  <Readable>k__BackingField  // 0x81
  private           System.Boolean                  <Writable>k__BackingField  // 0x82
  private           System.Boolean                  <HasMemberAttribute>k__BackingField  // 0x83
  private           System.Nullable<System.Boolean> <IsReference>k__BackingField  // 0x84
  private           System.Nullable<Newtonsoft.Json.NullValueHandling><NullValueHandling>k__BackingField  // 0x88
  private           System.Nullable<Newtonsoft.Json.DefaultValueHandling><DefaultValueHandling>k__BackingField  // 0x90
  private           System.Nullable<Newtonsoft.Json.ReferenceLoopHandling><ReferenceLoopHandling>k__BackingField  // 0x98
  private           System.Nullable<Newtonsoft.Json.ObjectCreationHandling><ObjectCreationHandling>k__BackingField  // 0xa0
  private           System.Nullable<Newtonsoft.Json.TypeNameHandling><TypeNameHandling>k__BackingField  // 0xa8
  private           System.Predicate<System.Object> <ShouldSerialize>k__BackingField  // 0xb0
  private           System.Predicate<System.Object> <ShouldDeserialize>k__BackingField  // 0xb8
  private           System.Predicate<System.Object> <GetIsSpecified>k__BackingField  // 0xc0
  private           System.Action<System.Object,System.Object><SetIsSpecified>k__BackingField  // 0xc8
  private           Newtonsoft.Json.JsonConverter   <ItemConverter>k__BackingField  // 0xd0
  private           System.Nullable<System.Boolean> <ItemIsReference>k__BackingField  // 0xd8
  private           System.Nullable<Newtonsoft.Json.TypeNameHandling><ItemTypeNameHandling>k__BackingField  // 0xdc
  private           System.Nullable<Newtonsoft.Json.ReferenceLoopHandling><ItemReferenceLoopHandling>k__BackingField  // 0xe4
PROPERTIES:
  PropertyContract  get=0x04D86200  set=0x035A41A0
  PropertyName  get=0x011F36E0  set=0x032B4000
  DeclaringType  get=0x04D86270  set=0x02FDAEA0
  Order  get=0x02E56440  set=0x04D86A60
  UnderlyingName  get=0x04D86280  set=0x063CC164
  ValueProvider  get=0x04D86230  set=0x0485F170
  AttributeProvider  set=0x063AD760
  PropertyType  get=0x04D85A60  set=0x0321FFC0
  Converter  get=0x04D862A0  set=0x06402320
  Ignored  get=0x04D8D1F0  set=0x04D8D200
  Readable  get=0x04D8FEE0  set=0x04D8FEF0
  Writable  get=0x04DA5E30  set=0x04DA5E60
  HasMemberAttribute  get=0x04DA5DF0  set=0x04DA5E40
  DefaultValue  get=0x0A8BCBA8  set=0x0A8BCBB8
  Required  get=0x04D882A0
  IsReference  get=0x04D9CCF0  set=0x04D9CD00
  NullValueHandling  get=0x04D861F0  set=0x04D85ED0
  DefaultValueHandling  get=0x04D86220  set=0x04D92A70
  ReferenceLoopHandling  get=0x04D85EE0  set=0x04D92710
  ObjectCreationHandling  get=0x04D86210  set=0x04D927B0
  TypeNameHandling  get=0x04D86290  set=0x04D862F0
  ShouldSerialize  get=0x04D86250  set=0x04D84CE0
  ShouldDeserialize  get=0x04D85EF0
  GetIsSpecified  get=0x04D892F0  set=0x06AC5ACC
  SetIsSpecified  get=0x04D8D1D0  set=0x06AC5B30
  ItemConverter  get=0x04D876B0  set=0x0678D7BC
  ItemIsReference  get=0x04DA5E00  set=0x04DA5E50
  ItemTypeNameHandling  get=0x04DA5E20  set=0x04D9E050
  ItemReferenceLoopHandling  get=0x04DA5E10  set=0x04D9E040
METHODS:
  RVA=0x02FBEF90  token=0x6000544  System.Object GetResolvedDefaultValue()
  RVA=0x0A8BCB60  token=0x6000559  System.String ToString()
  RVA=0x0377CE50  token=0x6000562  System.Void WritePropertyName(Newtonsoft.Json.JsonWriter writer)
  RVA=0x041E1670  token=0x6000563  System.Void .ctor()
END_CLASS

CLASS: Newtonsoft.Json.Serialization.JsonPropertyCollection
TYPE:  class
TOKEN: 0x20000C7
SIZE:  0x40
EXTENDS: System.Collections.ObjectModel.KeyedCollection`2
FIELDS:
  private   readonly System.Type                     _type  // 0x30
  private   readonly System.Collections.Generic.List<Newtonsoft.Json.Serialization.JsonProperty>_list  // 0x38
METHODS:
  RVA=0x035CE5A0  token=0x6000564  System.Void .ctor(System.Type type)
  RVA=0x04DA5DD0  token=0x6000565  System.String GetKeyForItem(Newtonsoft.Json.Serialization.JsonProperty item)
  RVA=0x032B3F40  token=0x6000566  System.Void AddProperty(Newtonsoft.Json.Serialization.JsonProperty property)
  RVA=0x02FB6580  token=0x6000567  Newtonsoft.Json.Serialization.JsonProperty GetClosestMatchProperty(System.String propertyName)
  RVA=0x02FB6D30  token=0x6000568  System.Boolean TryGetProperty(System.String key, Newtonsoft.Json.Serialization.JsonProperty& item)
  RVA=0x02FB67E0  token=0x6000569  Newtonsoft.Json.Serialization.JsonProperty GetProperty(System.String propertyName, System.StringComparison comparisonType)
END_CLASS

CLASS: Newtonsoft.Json.Serialization.JsonSerializerInternalBase
TYPE:  abstract class
TOKEN: 0x20000C8
SIZE:  0x38
FIELDS:
  private           Newtonsoft.Json.Serialization.ErrorContext_currentErrorContext  // 0x10
  private           Newtonsoft.Json.Utilities.BidirectionalDictionary<System.String,System.Object>_mappings  // 0x18
  private   readonly Newtonsoft.Json.JsonSerializer  Serializer  // 0x20
  private   readonly Newtonsoft.Json.Serialization.ITraceWriterTraceWriter  // 0x28
  protected         Newtonsoft.Json.Serialization.JsonSerializerProxyInternalSerializer  // 0x30
PROPERTIES:
  DefaultReferenceMappings  get=0x0A8BD0A4
METHODS:
  RVA=0x03F720B0  token=0x600056A  System.Void .ctor(Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x0A8BD018  token=0x600056C  Newtonsoft.Json.NullValueHandling ResolvedNullValueHandling(Newtonsoft.Json.Serialization.JsonObjectContract containerContract, Newtonsoft.Json.Serialization.JsonProperty property)
  RVA=0x0A8BCC40  token=0x600056D  Newtonsoft.Json.Serialization.ErrorContext GetErrorContext(System.Object currentObject, System.Object member, System.String path, System.Exception error)
  RVA=0x0A8BCBCC  token=0x600056E  System.Void ClearErrorContext()
  RVA=0x0A8BCD4C  token=0x600056F  System.Boolean IsErrorHandled(System.Object currentObject, Newtonsoft.Json.Serialization.JsonContract contract, System.Object keyValue, Newtonsoft.Json.IJsonLineInfo lineInfo, System.String path, System.Exception ex)
END_CLASS

CLASS: Newtonsoft.Json.Serialization.JsonSerializerInternalReader
TYPE:  class
TOKEN: 0x20000CA
SIZE:  0x38
EXTENDS: Newtonsoft.Json.Serialization.JsonSerializerInternalBase
FIELDS:
METHODS:
  RVA=0x05390C34  token=0x6000573  System.Void .ctor(Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x0A8BEAB8  token=0x6000574  System.Void Populate(Newtonsoft.Json.JsonReader reader, System.Object target)
  RVA=0x02FBD0A0  token=0x6000575  Newtonsoft.Json.Serialization.JsonContract GetContractSafe(System.Type type)
  RVA=0x02FBDED0  token=0x6000576  Newtonsoft.Json.Serialization.JsonContract GetContract(System.Type type)
  RVA=0x02FBCF50  token=0x6000577  System.Object Deserialize(Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Boolean checkAdditionalContent)
  RVA=0x0312E380  token=0x6000578  Newtonsoft.Json.Serialization.JsonSerializerProxy GetInternalSerializer()
  RVA=0x035A4840  token=0x6000579  Newtonsoft.Json.Linq.JToken CreateJToken(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonContract contract)
  RVA=0x035A3A50  token=0x600057A  Newtonsoft.Json.Linq.JToken CreateJObject(Newtonsoft.Json.JsonReader reader)
  RVA=0x02FB24C0  token=0x600057B  System.Object CreateValueInternal(Newtonsoft.Json.JsonReader reader, System.Type objectType, Newtonsoft.Json.Serialization.JsonContract contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract containerContract, Newtonsoft.Json.Serialization.JsonProperty containerMember, System.Object existingValue)
  RVA=0x02FBC320  token=0x600057C  System.Boolean CoerceEmptyStringToNull(System.Type objectType, Newtonsoft.Json.Serialization.JsonContract contract, System.String s)
  RVA=0x0A8BE2A0  token=0x600057D  System.String GetExpectedDescription(Newtonsoft.Json.Serialization.JsonContract contract)
  RVA=0x02FB4810  token=0x600057E  Newtonsoft.Json.JsonConverter GetConverter(Newtonsoft.Json.Serialization.JsonContract contract, Newtonsoft.Json.JsonConverter memberConverter, Newtonsoft.Json.Serialization.JsonContainerContract containerContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty)
  RVA=0x032B5D10  token=0x600057F  System.Object CreateObject(Newtonsoft.Json.JsonReader reader, System.Type objectType, Newtonsoft.Json.Serialization.JsonContract contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract containerContract, Newtonsoft.Json.Serialization.JsonProperty containerMember, System.Object existingValue)
  RVA=0x0A8BF040  token=0x6000580  System.Boolean ReadMetadataPropertiesToken(Newtonsoft.Json.Linq.JTokenReader reader, System.Type& objectType, Newtonsoft.Json.Serialization.JsonContract& contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract containerContract, Newtonsoft.Json.Serialization.JsonProperty containerMember, System.Object existingValue, System.Object& newValue, System.String& id)
  RVA=0x032B62C0  token=0x6000581  System.Boolean ReadMetadataProperties(Newtonsoft.Json.JsonReader reader, System.Type& objectType, Newtonsoft.Json.Serialization.JsonContract& contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract containerContract, Newtonsoft.Json.Serialization.JsonProperty containerMember, System.Object existingValue, System.Object& newValue, System.String& id)
  RVA=0x02FBD0F0  token=0x6000582  System.Void ResolveTypeName(Newtonsoft.Json.JsonReader reader, System.Type& objectType, Newtonsoft.Json.Serialization.JsonContract& contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract containerContract, Newtonsoft.Json.Serialization.JsonProperty containerMember, System.String qualifiedTypeName)
  RVA=0x032B7360  token=0x6000583  Newtonsoft.Json.Serialization.JsonArrayContract EnsureArrayContract(Newtonsoft.Json.JsonReader reader, System.Type objectType, Newtonsoft.Json.Serialization.JsonContract contract)
  RVA=0x032B6EF0  token=0x6000584  System.Object CreateList(Newtonsoft.Json.JsonReader reader, System.Type objectType, Newtonsoft.Json.Serialization.JsonContract contract, Newtonsoft.Json.Serialization.JsonProperty member, System.Object existingValue, System.String id)
  RVA=0x032B61E0  token=0x6000585  System.Boolean HasNoDefinedType(Newtonsoft.Json.Serialization.JsonContract contract)
  RVA=0x02FB1850  token=0x6000586  System.Object EnsureType(Newtonsoft.Json.JsonReader reader, System.Object value, System.Globalization.CultureInfo culture, Newtonsoft.Json.Serialization.JsonContract contract, System.Type targetType)
  RVA=0x02FB41D0  token=0x6000587  System.Boolean SetPropertyValue(Newtonsoft.Json.Serialization.JsonProperty property, Newtonsoft.Json.JsonConverter propertyConverter, Newtonsoft.Json.Serialization.JsonContainerContract containerContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty, Newtonsoft.Json.JsonReader reader, System.Object target)
  RVA=0x02FB3360  token=0x6000588  System.Boolean CalculatePropertyDetails(Newtonsoft.Json.Serialization.JsonProperty property, Newtonsoft.Json.JsonConverter& propertyConverter, Newtonsoft.Json.Serialization.JsonContainerContract containerContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty, Newtonsoft.Json.JsonReader reader, System.Object target, System.Boolean& useExistingValue, System.Object& currentValue, Newtonsoft.Json.Serialization.JsonContract& propertyContract, System.Boolean& gottenCurrentValue, System.Boolean& ignoredValue)
  RVA=0x0A8BD1AC  token=0x6000589  System.Void AddReference(Newtonsoft.Json.JsonReader reader, System.String id, System.Object value)
  RVA=0x02FB41C0  token=0x600058A  System.Boolean HasFlag(Newtonsoft.Json.DefaultValueHandling value, Newtonsoft.Json.DefaultValueHandling flag)
  RVA=0x02FB32B0  token=0x600058B  System.Boolean ShouldSetPropertyValue(Newtonsoft.Json.Serialization.JsonProperty property, Newtonsoft.Json.Serialization.JsonObjectContract contract, System.Object value)
  RVA=0x032B5940  token=0x600058C  System.Collections.IList CreateNewList(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonArrayContract contract, System.Boolean& createdFromNonDefaultCreator)
  RVA=0x04021D20  token=0x600058D  System.Collections.IDictionary CreateNewDictionary(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonDictionaryContract contract, System.Boolean& createdFromNonDefaultCreator)
  RVA=0x032B6C70  token=0x600058E  System.Void OnDeserializing(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonContract contract, System.Object value)
  RVA=0x032B6DB0  token=0x600058F  System.Void OnDeserialized(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonContract contract, System.Object value)
  RVA=0x035D2C10  token=0x6000590  System.Object PopulateDictionary(System.Collections.IDictionary dictionary, Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonDictionaryContract contract, Newtonsoft.Json.Serialization.JsonProperty containerProperty, System.String id)
  RVA=0x0A8BE3F0  token=0x6000591  System.Object PopulateMultidimensionalArray(System.Collections.IList list, Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonArrayContract contract, Newtonsoft.Json.Serialization.JsonProperty containerProperty, System.String id)
  RVA=0x0A8BF638  token=0x6000592  System.Void ThrowUnexpectedEndException(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonContract contract, System.Object currentObject, System.String message)
  RVA=0x032B73B0  token=0x6000593  System.Object PopulateList(System.Collections.IList list, Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonArrayContract contract, Newtonsoft.Json.Serialization.JsonProperty containerProperty, System.String id)
  RVA=0x0A8BDC54  token=0x6000594  System.Object CreateISerializable(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonISerializableContract contract, Newtonsoft.Json.Serialization.JsonProperty member, System.String id)
  RVA=0x0A8BDB38  token=0x6000595  System.Object CreateISerializableItem(Newtonsoft.Json.Linq.JToken token, System.Type type, Newtonsoft.Json.Serialization.JsonISerializableContract contract, Newtonsoft.Json.Serialization.JsonProperty member)
  RVA=0x0A8BD448  token=0x6000596  System.Object CreateDynamic(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonDynamicContract contract, Newtonsoft.Json.Serialization.JsonProperty member, System.String id)
  RVA=0x03B2CC20  token=0x6000597  System.Object CreateObjectUsingCreatorWithParameters(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonObjectContract contract, Newtonsoft.Json.Serialization.JsonProperty containerProperty, Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> creator, System.String id)
  RVA=0x0312E290  token=0x6000598  System.Object DeserializeConvertable(Newtonsoft.Json.JsonConverter converter, Newtonsoft.Json.JsonReader reader, System.Type objectType, System.Object existingValue)
  RVA=0x03B2C810  token=0x6000599  System.Collections.Generic.List<Newtonsoft.Json.Serialization.JsonSerializerInternalReader.CreatorPropertyContext> ResolvePropertyAndCreatorValues(Newtonsoft.Json.Serialization.JsonObjectContract contract, Newtonsoft.Json.Serialization.JsonProperty containerProperty, Newtonsoft.Json.JsonReader reader, System.Type objectType)
  RVA=0x032B6B80  token=0x600059A  System.Object CreateNewObject(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonObjectContract objectContract, Newtonsoft.Json.Serialization.JsonProperty containerMember, Newtonsoft.Json.Serialization.JsonProperty containerProperty, System.String id, System.Boolean& createdFromNonDefaultCreator)
  RVA=0x02FB4CC0  token=0x600059B  System.Object PopulateObject(System.Object newObject, Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonObjectContract contract, Newtonsoft.Json.Serialization.JsonProperty member, System.String id)
  RVA=0x02FB47C0  token=0x600059C  System.Boolean ShouldDeserialize(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonProperty property, System.Object target)
  RVA=0x02FB4C80  token=0x600059D  System.Boolean CheckPropertyName(Newtonsoft.Json.JsonReader reader, System.String memberName)
  RVA=0x03424C40  token=0x600059E  System.Void SetExtensionData(Newtonsoft.Json.Serialization.JsonObjectContract contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.JsonReader reader, System.String memberName, System.Object o)
  RVA=0x0A8BEF94  token=0x600059F  System.Object ReadExtensionDataValue(Newtonsoft.Json.Serialization.JsonObjectContract contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.JsonReader reader)
  RVA=0x02FBED50  token=0x60005A0  System.Void EndProcessProperty(System.Object newObject, Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonObjectContract contract, System.Int32 initialDepth, Newtonsoft.Json.Serialization.JsonProperty property, Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence presence, System.Boolean setDefaultValue)
  RVA=0x02FB4660  token=0x60005A1  System.Void SetPropertyPresence(Newtonsoft.Json.JsonReader reader, Newtonsoft.Json.Serialization.JsonProperty property, System.Collections.Generic.Dictionary<Newtonsoft.Json.Serialization.JsonProperty,Newtonsoft.Json.Serialization.JsonSerializerInternalReader.PropertyPresence> requiredProperties)
  RVA=0x0A8BE384  token=0x60005A2  System.Void HandleError(Newtonsoft.Json.JsonReader reader, System.Boolean readPastError, System.Int32 initialDepth)
END_CLASS

CLASS: Newtonsoft.Json.Serialization.JsonSerializerInternalWriter
TYPE:  class
TOKEN: 0x20000CF
SIZE:  0x50
EXTENDS: Newtonsoft.Json.Serialization.JsonSerializerInternalBase
FIELDS:
  private           System.Type                     _rootType  // 0x38
  private           System.Int32                    _rootLevel  // 0x40
  private   readonly System.Collections.Generic.List<System.Object>_serializeStack  // 0x48
METHODS:
  RVA=0x03F72010  token=0x60005AC  System.Void .ctor(Newtonsoft.Json.JsonSerializer serializer)
  RVA=0x03F72100  token=0x60005AD  System.Void Serialize(Newtonsoft.Json.JsonWriter jsonWriter, System.Object value, System.Type objectType)
  RVA=0x0A8BF780  token=0x60005AE  Newtonsoft.Json.Serialization.JsonSerializerProxy GetInternalSerializer()
  RVA=0x02FBEFE0  token=0x60005AF  Newtonsoft.Json.Serialization.JsonContract GetContractSafe(System.Object value)
  RVA=0x02FBF000  token=0x60005B0  Newtonsoft.Json.Serialization.JsonContract GetContract(System.Object value)
  RVA=0x0377F0F0  token=0x60005B1  System.Void SerializePrimitive(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.Serialization.JsonPrimitiveContract contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract containerContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty)
  RVA=0x0377D320  token=0x60005B2  System.Void SerializeValue(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.Serialization.JsonContract valueContract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract containerContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty)
  RVA=0x0377BD10  token=0x60005B3  System.Nullable<System.Boolean> ResolveIsReference(Newtonsoft.Json.Serialization.JsonContract contract, Newtonsoft.Json.Serialization.JsonProperty property, Newtonsoft.Json.Serialization.JsonContainerContract collectionContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty)
  RVA=0x0377D190  token=0x60005B4  System.Boolean ShouldWriteReference(System.Object value, Newtonsoft.Json.Serialization.JsonProperty property, Newtonsoft.Json.Serialization.JsonContract valueContract, Newtonsoft.Json.Serialization.JsonContainerContract collectionContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty)
  RVA=0x0377D120  token=0x60005B5  System.Boolean ShouldWriteProperty(System.Object memberValue, Newtonsoft.Json.Serialization.JsonObjectContract containerContract, Newtonsoft.Json.Serialization.JsonProperty property)
  RVA=0x0377D240  token=0x60005B6  System.Boolean CheckForCircularReference(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.Serialization.JsonProperty property, Newtonsoft.Json.Serialization.JsonContract contract, Newtonsoft.Json.Serialization.JsonContainerContract containerContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty)
  RVA=0x0A8C1BA4  token=0x60005B7  System.Void WriteReference(Newtonsoft.Json.JsonWriter writer, System.Object value)
  RVA=0x0A8BF7F4  token=0x60005B8  System.String GetReference(Newtonsoft.Json.JsonWriter writer, System.Object value)
  RVA=0x0A8C1958  token=0x60005B9  System.Boolean TryConvertToString(System.Object value, System.Type type, System.String& s)
  RVA=0x0A8C1828  token=0x60005BA  System.Void SerializeString(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.Serialization.JsonStringContract contract)
  RVA=0x04386D50  token=0x60005BB  System.Void OnSerializing(Newtonsoft.Json.JsonWriter writer, Newtonsoft.Json.Serialization.JsonContract contract, System.Object value)
  RVA=0x04386C20  token=0x60005BC  System.Void OnSerialized(Newtonsoft.Json.JsonWriter writer, Newtonsoft.Json.Serialization.JsonContract contract, System.Object value)
  RVA=0x0377BD80  token=0x60005BD  System.Void SerializeObject(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.Serialization.JsonObjectContract contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract collectionContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty)
  RVA=0x0377CE90  token=0x60005BE  System.Boolean CalculatePropertyValues(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.Serialization.JsonContainerContract contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonProperty property, Newtonsoft.Json.Serialization.JsonContract& memberContract, System.Object& memberValue)
  RVA=0x0377BC20  token=0x60005BF  System.Void WriteObjectStart(Newtonsoft.Json.JsonWriter writer, System.Object value, Newtonsoft.Json.Serialization.JsonContract contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract collectionContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty)
  RVA=0x0A8BF9D4  token=0x60005C0  System.Boolean HasCreatorParameter(Newtonsoft.Json.Serialization.JsonContainerContract contract, Newtonsoft.Json.Serialization.JsonProperty property)
  RVA=0x0A8C1A28  token=0x60005C1  System.Void WriteReferenceIdProperty(Newtonsoft.Json.JsonWriter writer, System.Type type, System.Object value)
  RVA=0x0A8C1D4C  token=0x60005C2  System.Void WriteTypeProperty(Newtonsoft.Json.JsonWriter writer, System.Type type)
  RVA=0x0377BD70  token=0x60005C3  System.Boolean HasFlag(Newtonsoft.Json.DefaultValueHandling value, Newtonsoft.Json.DefaultValueHandling flag)
  RVA=0x0377BD70  token=0x60005C4  System.Boolean HasFlag(Newtonsoft.Json.PreserveReferencesHandling value, Newtonsoft.Json.PreserveReferencesHandling flag)
  RVA=0x02FB41C0  token=0x60005C5  System.Boolean HasFlag(Newtonsoft.Json.TypeNameHandling value, Newtonsoft.Json.TypeNameHandling flag)
  RVA=0x0A8BFA5C  token=0x60005C6  System.Void SerializeConvertable(Newtonsoft.Json.JsonWriter writer, Newtonsoft.Json.JsonConverter converter, System.Object value, Newtonsoft.Json.Serialization.JsonContract contract, Newtonsoft.Json.Serialization.JsonContainerContract collectionContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty)
  RVA=0x04386090  token=0x60005C7  System.Void SerializeList(Newtonsoft.Json.JsonWriter writer, System.Collections.IEnumerable values, Newtonsoft.Json.Serialization.JsonArrayContract contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract collectionContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty)
  RVA=0x0A8C1698  token=0x60005C8  System.Void SerializeMultidimensionalArray(Newtonsoft.Json.JsonWriter writer, System.Array values, Newtonsoft.Json.Serialization.JsonArrayContract contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract collectionContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty)
  RVA=0x0A8C12BC  token=0x60005C9  System.Void SerializeMultidimensionalArray(Newtonsoft.Json.JsonWriter writer, System.Array values, Newtonsoft.Json.Serialization.JsonArrayContract contract, Newtonsoft.Json.Serialization.JsonProperty member, System.Int32 initialDepth, System.Int32[] indices)
  RVA=0x04386E80  token=0x60005CA  System.Boolean WriteStartArray(Newtonsoft.Json.JsonWriter writer, System.Object values, Newtonsoft.Json.Serialization.JsonArrayContract contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract containerContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty)
  RVA=0x0A8C0EB8  token=0x60005CB  System.Void SerializeISerializable(Newtonsoft.Json.JsonWriter writer, System.Runtime.Serialization.ISerializable value, Newtonsoft.Json.Serialization.JsonISerializableContract contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract collectionContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty)
  RVA=0x0A8BFD9C  token=0x60005CC  System.Void SerializeDynamic(Newtonsoft.Json.JsonWriter writer, System.Dynamic.IDynamicMetaObjectProvider value, Newtonsoft.Json.Serialization.JsonDynamicContract contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract collectionContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty)
  RVA=0x0A8C18C0  token=0x60005CD  System.Boolean ShouldWriteDynamicProperty(System.Object memberValue)
  RVA=0x0377BAB0  token=0x60005CE  System.Boolean ShouldWriteType(Newtonsoft.Json.TypeNameHandling typeNameHandlingFlag, Newtonsoft.Json.Serialization.JsonContract contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract containerContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty)
  RVA=0x02FBF040  token=0x60005CF  System.Void SerializeDictionary(Newtonsoft.Json.JsonWriter writer, System.Collections.IDictionary values, Newtonsoft.Json.Serialization.JsonDictionaryContract contract, Newtonsoft.Json.Serialization.JsonProperty member, Newtonsoft.Json.Serialization.JsonContainerContract collectionContract, Newtonsoft.Json.Serialization.JsonProperty containerProperty)
  RVA=0x02FC0E90  token=0x60005D0  System.String GetPropertyName(Newtonsoft.Json.JsonWriter writer, System.Object name, Newtonsoft.Json.Serialization.JsonContract contract, System.Boolean& escape)
  RVA=0x0A8BF96C  token=0x60005D1  System.Void HandleError(Newtonsoft.Json.JsonWriter writer, System.Int32 initialDepth)
  RVA=0x0377D0D0  token=0x60005D2  System.Boolean ShouldSerialize(Newtonsoft.Json.JsonWriter writer, Newtonsoft.Json.Serialization.JsonProperty property, System.Object target)
  RVA=0x0377D080  token=0x60005D3  System.Boolean IsSpecified(Newtonsoft.Json.JsonWriter writer, Newtonsoft.Json.Serialization.JsonProperty property, System.Object target)
END_CLASS

CLASS: Newtonsoft.Json.Serialization.JsonSerializerProxy
TYPE:  class
TOKEN: 0x20000D0
SIZE:  0xF8
EXTENDS: Newtonsoft.Json.JsonSerializer
FIELDS:
  private   readonly Newtonsoft.Json.Serialization.JsonSerializerInternalReader_serializerReader  // 0xe0
  private   readonly Newtonsoft.Json.Serialization.JsonSerializerInternalWriter_serializerWriter  // 0xe8
  private   readonly Newtonsoft.Json.JsonSerializer  _serializer  // 0xf0
PROPERTIES:
  ReferenceResolver  set=0x0A8CFDAC
  TraceWriter  get=0x0A8CFB48  set=0x0A8CFDFC
  EqualityComparer  set=0x0A8CFC6C
  Converters  get=0x0A8CFA98
  DefaultValueHandling  set=0x0A8CFC44
  ContractResolver  get=0x0A8CFA74  set=0x0A8CFC1C
  MissingMemberHandling  set=0x0A8CFCE4
  NullValueHandling  get=0x0A8CFB24  set=0x0A8CFD0C
  ObjectCreationHandling  set=0x0A8CFD34
  ReferenceLoopHandling  set=0x0A8CFD84
  PreserveReferencesHandling  set=0x0A8CFD5C
  TypeNameHandling  set=0x0A8CFE4C
  MetadataPropertyHandling  get=0x0A8CFB00  set=0x0A8CFCBC
  TypeNameAssemblyFormatHandling  set=0x0A8CFE24
  ConstructorHandling  set=0x0A8CFBBC
  SerializationBinder  get=0x02E56460  set=0x0A8CFDD4
  Context  get=0x0A8CFA40  set=0x0A8CFBE4
  Formatting  get=0x0A8CFAB8  set=0x0A8CFC94
  MaxDepth  get=0x0A8CFADC
  CheckAdditionalContent  get=0x0A8CFA1C  set=0x0A8CFB94
EVENTS:
  Error  add=add_Error  remove=remove_Error
METHODS:
  RVA=0x0A8CF88C  token=0x60005F2  Newtonsoft.Json.Serialization.JsonSerializerInternalBase GetInternalSerializer()
  RVA=0x035A65F0  token=0x60005F3  System.Void .ctor(Newtonsoft.Json.Serialization.JsonSerializerInternalReader serializerReader)
  RVA=0x0A8CF934  token=0x60005F4  System.Void .ctor(Newtonsoft.Json.Serialization.JsonSerializerInternalWriter serializerWriter)
  RVA=0x0A8CF840  token=0x60005F5  System.Object DeserializeInternal(Newtonsoft.Json.JsonReader reader, System.Type objectType)
  RVA=0x0A8CF8A8  token=0x60005F6  System.Void PopulateInternal(Newtonsoft.Json.JsonReader reader, System.Object target)
  RVA=0x0A8CF8EC  token=0x60005F7  System.Void SerializeInternal(Newtonsoft.Json.JsonWriter jsonWriter, System.Object value, System.Type rootType)
END_CLASS

CLASS: Newtonsoft.Json.Serialization.JsonStringContract
TYPE:  class
TOKEN: 0x20000D1
SIZE:  0x98
EXTENDS: Newtonsoft.Json.Serialization.JsonPrimitiveContract
FIELDS:
METHODS:
  RVA=0x04CCFC10  token=0x60005F8  System.Void .ctor(System.Type underlyingType)
END_CLASS

CLASS: Newtonsoft.Json.Serialization.JsonTypeReflector
TYPE:  static class
TOKEN: 0x20000D2
SIZE:  0x10
FIELDS:
  private   static  System.Nullable<System.Boolean> _fullyTrusted  // static @ 0x0
  private   static readonly Newtonsoft.Json.Utilities.ThreadSafeStore<System.Type,System.Func<System.Object[],System.Object>>CreatorCache  // static @ 0x8
  private   static readonly Newtonsoft.Json.Utilities.ThreadSafeStore<System.Type,System.Type>AssociatedMetadataTypesCache  // static @ 0x10
  private   static  Newtonsoft.Json.Utilities.ReflectionObject_metadataTypeAttributeReflectionObject  // static @ 0x18
PROPERTIES:
  FullyTrusted  get=0x0A8D0288
  ReflectionDelegateFactory  get=0x035CF6E0
METHODS:
  RVA=-1  // generic def  token=0x60005F9  T GetCachedAttribute(System.Object attributeProvider)
  RVA=0x03591BB0  token=0x60005FA  System.Boolean CanTypeDescriptorConvertString(System.Type type, System.ComponentModel.TypeConverter& typeConverter)
  RVA=0x032B4DB0  token=0x60005FB  System.Runtime.Serialization.DataContractAttribute GetDataContractAttribute(System.Type type)
  RVA=0x0A8D006C  token=0x60005FC  System.Runtime.Serialization.DataMemberAttribute GetDataMemberAttribute(System.Reflection.MemberInfo memberInfo)
  RVA=0x035CEAB0  token=0x60005FD  Newtonsoft.Json.MemberSerialization GetObjectMemberSerialization(System.Type objectType, System.Boolean ignoreSerializableAttribute)
  RVA=0x032B40C0  token=0x60005FE  Newtonsoft.Json.JsonConverter GetJsonConverter(System.Object attributeProvider)
  RVA=0x0A8CFE74  token=0x60005FF  Newtonsoft.Json.JsonConverter CreateJsonConverterInstance(System.Type converterType, System.Object[] args)
  RVA=0x0A8CFF1C  token=0x6000600  Newtonsoft.Json.Serialization.NamingStrategy CreateNamingStrategyInstance(System.Type namingStrategyType, System.Object[] args)
  RVA=0x0A8CFFC4  token=0x6000601  Newtonsoft.Json.Serialization.NamingStrategy GetContainerNamingStrategy(Newtonsoft.Json.JsonContainerAttribute containerAttribute)
  RVA=0x0496F210  token=0x6000602  System.Func<System.Object[],System.Object> GetCreator(System.Type type)
  RVA=0x0312C240  token=0x6000603  System.Type GetAssociatedMetadataType(System.Type type)
  RVA=0x032B2B60  token=0x6000604  System.Type GetAssociateMetadataTypeFromAttribute(System.Type type)
  RVA=-1  // generic def  token=0x6000605  T GetAttribute(System.Type type)
  RVA=-1  // generic def  token=0x6000606  T GetAttribute(System.Reflection.MemberInfo memberInfo)
  RVA=0x032B3450  token=0x6000607  System.Boolean IsNonSerializable(System.Object provider)
  RVA=0x0A8D022C  token=0x6000608  System.Boolean IsSerializable(System.Object provider)
  RVA=-1  // generic def  token=0x6000609  T GetAttribute(System.Object provider)
  RVA=0x0489B130  token=0x600060C  System.Void .cctor()
END_CLASS

CLASS: Newtonsoft.Json.Serialization.KebabCaseNamingStrategy
TYPE:  class
TOKEN: 0x20000D5
SIZE:  0x18
EXTENDS: Newtonsoft.Json.Serialization.NamingStrategy
FIELDS:
METHODS:
  RVA=0x04DA5CA0  token=0x6000612  System.Void .ctor(System.Boolean processDictionaryKeys, System.Boolean overrideSpecifiedNames)
  RVA=0x04DA5C90  token=0x6000613  System.Void .ctor(System.Boolean processDictionaryKeys, System.Boolean overrideSpecifiedNames, System.Boolean processExtensionDataNames)
  RVA=0x041E1670  token=0x6000614  System.Void .ctor()
  RVA=0x0A8D0348  token=0x6000615  System.String ResolvePropertyName(System.String name)
END_CLASS

CLASS: Newtonsoft.Json.Serialization.NamingStrategy
TYPE:  abstract class
TOKEN: 0x20000D6
SIZE:  0x18
FIELDS:
  private           System.Boolean                  <ProcessDictionaryKeys>k__BackingField  // 0x10
  private           System.Boolean                  <ProcessExtensionDataNames>k__BackingField  // 0x11
  private           System.Boolean                  <OverrideSpecifiedNames>k__BackingField  // 0x12
PROPERTIES:
  ProcessDictionaryKeys  get=0x015EFCE0  set=0x04D86130
  ProcessExtensionDataNames  get=0x04D88380  set=0x04D88460
  OverrideSpecifiedNames  get=0x04D88390  set=0x04DA0900
METHODS:
  RVA=0x0A8D0540  token=0x600061C  System.String GetPropertyName(System.String name, System.Boolean hasSpecifiedName)
  RVA=0x0A8D0468  token=0x600061D  System.String GetExtensionDataName(System.String name)
  RVA=0x0A8D0440  token=0x600061E  System.String GetDictionaryKey(System.String key)
  RVA=-1  // abstract  token=0x600061F  System.String ResolvePropertyName(System.String name)
  RVA=0x0A8D0490  token=0x6000620  System.Int32 GetHashCode()
  RVA=0x0A8D03EC  token=0x6000621  System.Boolean Equals(System.Object obj)
  RVA=0x0A8D0354  token=0x6000622  System.Boolean Equals(Newtonsoft.Json.Serialization.NamingStrategy other)
  RVA=0x041E1670  token=0x6000623  System.Void .ctor()
END_CLASS

CLASS: Newtonsoft.Json.Serialization.ObjectConstructor`1
TYPE:  sealed class
TOKEN: 0x20000D7
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000624  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000625  System.Object Invoke(System.Object[] args)
END_CLASS

CLASS: Newtonsoft.Json.Serialization.OnErrorAttribute
TYPE:  sealed class
TOKEN: 0x20000D8
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.Serialization.ReflectionAttributeProvider
TYPE:  class
TOKEN: 0x20000D9
SIZE:  0x18
IMPLEMENTS: Newtonsoft.Json.Serialization.IAttributeProvider
FIELDS:
  private   readonly System.Object                   _attributeProvider  // 0x10
METHODS:
  RVA=0x03220020  token=0x6000626  System.Void .ctor(System.Object attributeProvider)
END_CLASS

CLASS: Newtonsoft.Json.Serialization.ReflectionValueProvider
TYPE:  class
TOKEN: 0x20000DA
SIZE:  0x18
IMPLEMENTS: Newtonsoft.Json.Serialization.IValueProvider
FIELDS:
  private   readonly System.Reflection.MemberInfo    _memberInfo  // 0x10
METHODS:
  RVA=0x03220040  token=0x6000627  System.Void .ctor(System.Reflection.MemberInfo memberInfo)
  RVA=0x02FB2C00  token=0x6000628  System.Void SetValue(System.Object target, System.Object value)
  RVA=0x03594940  token=0x6000629  System.Object GetValue(System.Object target)
END_CLASS

CLASS: Newtonsoft.Json.Serialization.SnakeCaseNamingStrategy
TYPE:  class
TOKEN: 0x20000DB
SIZE:  0x18
EXTENDS: Newtonsoft.Json.Serialization.NamingStrategy
FIELDS:
METHODS:
  RVA=0x04DA5CA0  token=0x600062A  System.Void .ctor(System.Boolean processDictionaryKeys, System.Boolean overrideSpecifiedNames)
  RVA=0x04DA5C90  token=0x600062B  System.Void .ctor(System.Boolean processDictionaryKeys, System.Boolean overrideSpecifiedNames, System.Boolean processExtensionDataNames)
  RVA=0x041E1670  token=0x600062C  System.Void .ctor()
  RVA=0x0A8D0564  token=0x600062D  System.String ResolvePropertyName(System.String name)
END_CLASS

CLASS: Newtonsoft.Json.Serialization.TraceJsonReader
TYPE:  class
TOKEN: 0x20000DC
SIZE:  0x90
EXTENDS: Newtonsoft.Json.JsonReader
IMPLEMENTS: Newtonsoft.Json.IJsonLineInfo
FIELDS:
  private   readonly Newtonsoft.Json.JsonReader      _innerReader  // 0x78
  private   readonly Newtonsoft.Json.JsonTextWriter  _textWriter  // 0x80
  private   readonly System.IO.StringWriter          _sw  // 0x88
PROPERTIES:
  Depth  get=0x0A8D0AC8
  Path  get=0x0A8D0AEC
  TokenType  get=0x0A8D0B10
  Value  get=0x0A8D0B58
  ValueType  get=0x0A8D0B34
  Newtonsoft.Json.IJsonLineInfo.LineNumber  get=0x0A8D0608
  Newtonsoft.Json.IJsonLineInfo.LinePosition  get=0x0A8D0658
METHODS:
  RVA=0x0A8D0970  token=0x600062E  System.Void .ctor(Newtonsoft.Json.JsonReader innerReader)
  RVA=0x0A8D0594  token=0x600062F  System.String GetDeserializedJsonMessage()
  RVA=0x0A8D08F8  token=0x6000630  System.Boolean Read()
  RVA=0x0A8D0870  token=0x6000631  System.Nullable<System.Int32> ReadAsInt32()
  RVA=0x0A8D08B4  token=0x6000632  System.String ReadAsString()
  RVA=0x0A8D06EC  token=0x6000633  System.Byte[] ReadAsBytes()
  RVA=0x0A8D07CC  token=0x6000634  System.Nullable<System.Decimal> ReadAsDecimal()
  RVA=0x0A8D0820  token=0x6000635  System.Nullable<System.Double> ReadAsDouble()
  RVA=0x0A8D06A8  token=0x6000636  System.Nullable<System.Boolean> ReadAsBoolean()
  RVA=0x0A8D0784  token=0x6000637  System.Nullable<System.DateTime> ReadAsDateTime()
  RVA=0x0A8D0730  token=0x6000638  System.Nullable<System.DateTimeOffset> ReadAsDateTimeOffset()
  RVA=0x0A8D093C  token=0x6000639  System.Void WriteCurrentToken()
  RVA=0x0A8D0570  token=0x600063F  System.Void Close()
  RVA=0x0A8D05B8  token=0x6000640  System.Boolean Newtonsoft.Json.IJsonLineInfo.HasLineInfo()
END_CLASS

CLASS: Newtonsoft.Json.Serialization.TraceJsonWriter
TYPE:  class
TOKEN: 0x20000DD
SIZE:  0x78
EXTENDS: Newtonsoft.Json.JsonWriter
FIELDS:
  private   readonly Newtonsoft.Json.JsonWriter      _innerWriter  // 0x60
  private   readonly Newtonsoft.Json.JsonTextWriter  _textWriter  // 0x68
  private   readonly System.IO.StringWriter          _sw  // 0x70
METHODS:
  RVA=0x0A8D2300  token=0x6000643  System.Void .ctor(Newtonsoft.Json.JsonWriter innerWriter)
  RVA=0x0A8D0BC0  token=0x6000644  System.String GetSerializedJsonMessage()
  RVA=0x0A8D1344  token=0x6000645  System.Void WriteValue(System.Decimal value)
  RVA=0x0A8D1594  token=0x6000646  System.Void WriteValue(System.Nullable<System.Decimal> value)
  RVA=0x0A8D1538  token=0x6000647  System.Void WriteValue(System.Boolean value)
  RVA=0x0A8D1770  token=0x6000648  System.Void WriteValue(System.Nullable<System.Boolean> value)
  RVA=0x0A8D10D4  token=0x6000649  System.Void WriteValue(System.Byte value)
  RVA=0x0A8D1A48  token=0x600064A  System.Void WriteValue(System.Nullable<System.Byte> value)
  RVA=0x0A8D1714  token=0x600064B  System.Void WriteValue(System.Char value)
  RVA=0x0A8D1AD8  token=0x600064C  System.Void WriteValue(System.Nullable<System.Char> value)
  RVA=0x0A8D21A0  token=0x600064D  System.Void WriteValue(System.Byte[] value)
  RVA=0x0A8D1FA8  token=0x600064E  System.Void WriteValue(System.DateTime value)
  RVA=0x0A8D1130  token=0x600064F  System.Void WriteValue(System.Nullable<System.DateTime> value)
  RVA=0x0A8D13C0  token=0x6000650  System.Void WriteValue(System.DateTimeOffset value)
  RVA=0x0A8D1654  token=0x6000651  System.Void WriteValue(System.Nullable<System.DateTimeOffset> value)
  RVA=0x0A8D1434  token=0x6000652  System.Void WriteValue(System.Double value)
  RVA=0x0A8D18F0  token=0x6000653  System.Void WriteValue(System.Nullable<System.Double> value)
  RVA=0x0A8D0FA8  token=0x6000654  System.Void WriteUndefined()
  RVA=0x0A8D0D0C  token=0x6000655  System.Void WriteNull()
  RVA=0x0A8D1D0C  token=0x6000656  System.Void WriteValue(System.Single value)
  RVA=0x0A8D20AC  token=0x6000657  System.Void WriteValue(System.Nullable<System.Single> value)
  RVA=0x0A8D1C04  token=0x6000658  System.Void WriteValue(System.Guid value)
  RVA=0x0A8D1D68  token=0x6000659  System.Void WriteValue(System.Nullable<System.Guid> value)
  RVA=0x0A8D12EC  token=0x600065A  System.Void WriteValue(System.Int32 value)
  RVA=0x0A8D0FEC  token=0x600065B  System.Void WriteValue(System.Nullable<System.Int32> value)
  RVA=0x0A8D1E78  token=0x600065C  System.Void WriteValue(System.Int64 value)
  RVA=0x0A8D1490  token=0x600065D  System.Void WriteValue(System.Nullable<System.Int64> value)
  RVA=0x0A8D1ED4  token=0x600065E  System.Void WriteValue(System.Object value)
  RVA=0x0A8D1084  token=0x600065F  System.Void WriteValue(System.SByte value)
  RVA=0x0A8D11D0  token=0x6000660  System.Void WriteValue(System.Nullable<System.SByte> value)
  RVA=0x0A8D220C  token=0x6000661  System.Void WriteValue(System.Int16 value)
  RVA=0x0A8D1260  token=0x6000662  System.Void WriteValue(System.Nullable<System.Int16> value)
  RVA=0x0A8D1894  token=0x6000663  System.Void WriteValue(System.String value)
  RVA=0x0A8D199C  token=0x6000664  System.Void WriteValue(System.TimeSpan value)
  RVA=0x0A8D1B64  token=0x6000665  System.Void WriteValue(System.Nullable<System.TimeSpan> value)
  RVA=0x0A8D1E20  token=0x6000666  System.Void WriteValue(System.UInt32 value)
  RVA=0x0A8D2268  token=0x6000667  System.Void WriteValue(System.Nullable<System.UInt32> value)
  RVA=0x0A8D2144  token=0x6000668  System.Void WriteValue(System.UInt64 value)
  RVA=0x0A8D2004  token=0x6000669  System.Void WriteValue(System.Nullable<System.UInt64> value)
  RVA=0x0A8D1C78  token=0x600066A  System.Void WriteValue(System.Uri value)
  RVA=0x0A8D19EC  token=0x600066B  System.Void WriteValue(System.UInt16 value)
  RVA=0x0A8D1808  token=0x600066C  System.Void WriteValue(System.Nullable<System.UInt16> value)
  RVA=0x0A8D0BE4  token=0x600066D  System.Void WriteComment(System.String text)
  RVA=0x0A8D0EC4  token=0x600066E  System.Void WriteStartArray()
  RVA=0x0A8D0C40  token=0x600066F  System.Void WriteEndArray()
  RVA=0x0A8D0F08  token=0x6000670  System.Void WriteStartConstructor(System.String name)
  RVA=0x0A8D0C84  token=0x6000671  System.Void WriteEndConstructor()
  RVA=0x0A8D0D50  token=0x6000672  System.Void WritePropertyName(System.String name)
  RVA=0x0A8D0DAC  token=0x6000673  System.Void WritePropertyName(System.String name, System.Boolean escape)
  RVA=0x0A8D0F64  token=0x6000674  System.Void WriteStartObject()
  RVA=0x0A8D0CC8  token=0x6000675  System.Void WriteEndObject()
  RVA=0x0A8D0E1C  token=0x6000676  System.Void WriteRawValue(System.String json)
  RVA=0x0A8D0E78  token=0x6000677  System.Void WriteRaw(System.String json)
  RVA=0x0A8D0B7C  token=0x6000678  System.Void Close()
END_CLASS

CLASS: Newtonsoft.Json.Utilities.AotHelper
TYPE:  static class
TOKEN: 0x200003F
SIZE:  0x10
FIELDS:
  private   static  System.Boolean                  s_alwaysFalse  // static @ 0x0
METHODS:
  RVA=0x04A25330  token=0x6000207  System.Void Ensure(System.Action action)
  RVA=-1  // generic def  token=0x6000208  System.Void EnsureList()
  RVA=0x0A8AEC7C  token=0x6000209  System.Boolean IsFalse()
  RVA=0x04D269F0  token=0x600020A  System.Void .cctor()
END_CLASS

CLASS: Newtonsoft.Json.Utilities.Base64Encoder
TYPE:  class
TOKEN: 0x2000041
SIZE:  0x30
FIELDS:
  private   readonly System.Char[]                   _charsLine  // 0x10
  private   readonly System.IO.TextWriter            _writer  // 0x18
  private           System.Byte[]                   _leftOverBytes  // 0x20
  private           System.Int32                    _leftOverBytesCount  // 0x28
METHODS:
  RVA=0x0A8AF1F4  token=0x600020E  System.Void .ctor(System.IO.TextWriter writer)
  RVA=0x0A8AF04C  token=0x600020F  System.Void ValidateEncode(System.Byte[] buffer, System.Int32 index, System.Int32 count)
  RVA=0x0A8AECCC  token=0x6000210  System.Void Encode(System.Byte[] buffer, System.Int32 index, System.Int32 count)
  RVA=0x0A8AEF64  token=0x6000211  System.Void StoreLeftOverBytes(System.Byte[] buffer, System.Int32 index, System.Int32& count)
  RVA=0x0A8AEED4  token=0x6000212  System.Boolean FulfillFromLeftover(System.Byte[] buffer, System.Int32 index, System.Int32& count)
  RVA=0x0A8AEE38  token=0x6000213  System.Void Flush()
  RVA=0x0A8AF1C4  token=0x6000214  System.Void WriteChars(System.Char[] chars, System.Int32 index, System.Int32 count)
END_CLASS

CLASS: Newtonsoft.Json.Utilities.BidirectionalDictionary`2
TYPE:  class
TOKEN: 0x2000042
FIELDS:
  private   readonly System.Collections.Generic.IDictionary<TFirst,TSecond>_firstToSecond  // 0x0
  private   readonly System.Collections.Generic.IDictionary<TSecond,TFirst>_secondToFirst  // 0x0
  private   readonly System.String                   _duplicateFirstErrorMessage  // 0x0
  private   readonly System.String                   _duplicateSecondErrorMessage  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000215  System.Void .ctor(System.Collections.Generic.IEqualityComparer<TFirst> firstEqualityComparer, System.Collections.Generic.IEqualityComparer<TSecond> secondEqualityComparer, System.String duplicateFirstErrorMessage, System.String duplicateSecondErrorMessage)
  RVA=-1  // not resolved  token=0x6000216  System.Void Set(TFirst first, TSecond second)
  RVA=-1  // not resolved  token=0x6000217  System.Boolean TryGetByFirst(TFirst first, TSecond& second)
  RVA=-1  // not resolved  token=0x6000218  System.Boolean TryGetBySecond(TSecond second, TFirst& first)
END_CLASS

CLASS: Newtonsoft.Json.Utilities.BoxedPrimitives
TYPE:  static class
TOKEN: 0x2000043
SIZE:  0x10
FIELDS:
  private   static readonly System.Object                   BooleanTrue  // static @ 0x0
  private   static readonly System.Object                   BooleanFalse  // static @ 0x8
  private   static readonly System.Object                   Int32_M1  // static @ 0x10
  private   static readonly System.Object                   Int32_0  // static @ 0x18
  private   static readonly System.Object                   Int32_1  // static @ 0x20
  private   static readonly System.Object                   Int32_2  // static @ 0x28
  private   static readonly System.Object                   Int32_3  // static @ 0x30
  private   static readonly System.Object                   Int32_4  // static @ 0x38
  private   static readonly System.Object                   Int32_5  // static @ 0x40
  private   static readonly System.Object                   Int32_6  // static @ 0x48
  private   static readonly System.Object                   Int32_7  // static @ 0x50
  private   static readonly System.Object                   Int32_8  // static @ 0x58
  private   static readonly System.Object                   Int64_M1  // static @ 0x60
  private   static readonly System.Object                   Int64_0  // static @ 0x68
  private   static readonly System.Object                   Int64_1  // static @ 0x70
  private   static readonly System.Object                   Int64_2  // static @ 0x78
  private   static readonly System.Object                   Int64_3  // static @ 0x80
  private   static readonly System.Object                   Int64_4  // static @ 0x88
  private   static readonly System.Object                   Int64_5  // static @ 0x90
  private   static readonly System.Object                   Int64_6  // static @ 0x98
  private   static readonly System.Object                   Int64_7  // static @ 0xa0
  private   static readonly System.Object                   Int64_8  // static @ 0xa8
  private   static readonly System.Object                   DecimalZero  // static @ 0xb0
  private   static readonly System.Object                   DoubleNaN  // static @ 0xb8
  private   static readonly System.Object                   DoublePositiveInfinity  // static @ 0xc0
  private   static readonly System.Object                   DoubleNegativeInfinity  // static @ 0xc8
  private   static readonly System.Object                   DoubleZero  // static @ 0xd0
METHODS:
  RVA=0x03429720  token=0x6000219  System.Object Get(System.Boolean value)
  RVA=0x03B2F4D0  token=0x600021A  System.Object Get(System.Int32 value)
  RVA=0x03428030  token=0x600021B  System.Object Get(System.Int64 value)
  RVA=0x0A8AFD80  token=0x600021C  System.Object Get(System.Decimal value)
  RVA=0x039F6160  token=0x600021D  System.Object Get(System.Double value)
  RVA=0x04305D50  token=0x600021E  System.Void .cctor()
END_CLASS

CLASS: Newtonsoft.Json.Utilities.CollectionUtils
TYPE:  static class
TOKEN: 0x2000044
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x600021F  System.Boolean IsNullOrEmpty(System.Collections.Generic.ICollection<T> collection)
  RVA=-1  // generic def  token=0x6000220  System.Void AddRange(System.Collections.Generic.IList<T> initial, System.Collections.Generic.IEnumerable<T> collection)
  RVA=0x035CF890  token=0x6000221  System.Boolean IsDictionaryType(System.Type type)
  RVA=0x03FD8F30  token=0x6000222  System.Reflection.ConstructorInfo ResolveEnumerableCollectionConstructor(System.Type collectionType, System.Type collectionItemType)
  RVA=0x03FD9030  token=0x6000223  System.Reflection.ConstructorInfo ResolveEnumerableCollectionConstructor(System.Type collectionType, System.Type collectionItemType, System.Type constructorArgumentType)
  RVA=-1  // generic def  token=0x6000224  System.Int32 IndexOf(System.Collections.Generic.IEnumerable<T> collection, System.Func<T,System.Boolean> predicate)
  RVA=-1  // generic def  token=0x6000225  System.Boolean Contains(System.Collections.Generic.List<T> list, T value, System.Collections.IEqualityComparer comparer)
  RVA=-1  // generic def  token=0x6000226  System.Int32 IndexOfReference(System.Collections.Generic.List<T> list, T item)
  RVA=-1  // generic def  token=0x6000227  System.Void FastReverse(System.Collections.Generic.List<T> list)
  RVA=0x0A8B0098  token=0x6000228  System.Collections.Generic.IList<System.Int32> GetDimensions(System.Collections.IList values, System.Int32 dimensionsCount)
  RVA=0x0A8AFEC4  token=0x6000229  System.Void CopyFromJaggedToMultidimensionalArray(System.Collections.IList values, System.Array multidimensionalArray, System.Int32[] indices)
  RVA=0x0A8B019C  token=0x600022A  System.Object JaggedArrayGetValue(System.Collections.IList values, System.Int32[] indices)
  RVA=0x0A8B0238  token=0x600022B  System.Array ToMultidimensionalArray(System.Collections.IList values, System.Type type, System.Int32 rank)
  RVA=-1  // generic def  token=0x600022C  T[] ArrayEmpty()
END_CLASS

CLASS: Newtonsoft.Json.Utilities.IWrappedCollection
TYPE:  interface
TOKEN: 0x2000046
IMPLEMENTS: System.Collections.IList System.Collections.ICollection System.Collections.IEnumerable
FIELDS:
PROPERTIES:
  UnderlyingCollection  get=-1  // abstract
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.Utilities.CollectionWrapper`1
TYPE:  class
TOKEN: 0x2000047
IMPLEMENTS: System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable Newtonsoft.Json.Utilities.IWrappedCollection System.Collections.IList System.Collections.ICollection
FIELDS:
  private   readonly System.Collections.IList        _list  // 0x0
  private   readonly System.Collections.Generic.ICollection<T>_genericCollection  // 0x0
  private           System.Object                   _syncRoot  // 0x0
PROPERTIES:
  Count  get=-1  // not resolved
  IsReadOnly  get=-1  // not resolved
  System.Collections.IList.IsFixedSize  get=-1  // not resolved
  System.Collections.IList.Item  get=-1  // not resolved  set=-1  // not resolved
  System.Collections.ICollection.IsSynchronized  get=-1  // not resolved
  System.Collections.ICollection.SyncRoot  get=-1  // not resolved
  UnderlyingCollection  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600022F  System.Void .ctor(System.Collections.IList list)
  RVA=-1  // not resolved  token=0x6000230  System.Void .ctor(System.Collections.Generic.ICollection<T> list)
  RVA=-1  // not resolved  token=0x6000231  System.Void Add(T item)
  RVA=-1  // not resolved  token=0x6000232  System.Void Clear()
  RVA=-1  // not resolved  token=0x6000233  System.Boolean Contains(T item)
  RVA=-1  // not resolved  token=0x6000234  System.Void CopyTo(T[] array, System.Int32 arrayIndex)
  RVA=-1  // not resolved  token=0x6000237  System.Boolean Remove(T item)
  RVA=-1  // not resolved  token=0x6000238  System.Collections.Generic.IEnumerator<T> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000239  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x600023A  System.Int32 System.Collections.IList.Add(System.Object value)
  RVA=-1  // not resolved  token=0x600023B  System.Boolean System.Collections.IList.Contains(System.Object value)
  RVA=-1  // not resolved  token=0x600023C  System.Int32 System.Collections.IList.IndexOf(System.Object value)
  RVA=-1  // not resolved  token=0x600023D  System.Void System.Collections.IList.RemoveAt(System.Int32 index)
  RVA=-1  // not resolved  token=0x600023E  System.Void System.Collections.IList.Insert(System.Int32 index, System.Object value)
  RVA=-1  // not resolved  token=0x6000240  System.Void System.Collections.IList.Remove(System.Object value)
  RVA=-1  // not resolved  token=0x6000243  System.Void System.Collections.ICollection.CopyTo(System.Array array, System.Int32 arrayIndex)
  RVA=-1  // not resolved  token=0x6000246  System.Void VerifyValueType(System.Object value)
  RVA=-1  // not resolved  token=0x6000247  System.Boolean IsCompatibleObject(System.Object value)
END_CLASS

CLASS: Newtonsoft.Json.Utilities.PrimitiveTypeCode
TYPE:  sealed struct
TOKEN: 0x2000048
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeEmpty  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeObject  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeChar  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeCharNullable  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeBoolean  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeBooleanNullable  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeSByte  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeSByteNullable  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeInt16  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeInt16Nullable  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeUInt16  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeUInt16Nullable  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeInt32  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeInt32Nullable  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeByte  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeByteNullable  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeUInt32  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeUInt32Nullable  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeInt64  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeInt64Nullable  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeUInt64  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeUInt64Nullable  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeSingle  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeSingleNullable  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeDouble  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeDoubleNullable  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeDateTime  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeDateTimeNullable  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeDateTimeOffset  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeDateTimeOffsetNullable  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeDecimal  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeDecimalNullable  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeGuid  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeGuidNullable  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeTimeSpan  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeTimeSpanNullable  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeBigInteger  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeBigIntegerNullable  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeUri  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeString  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeBytes  // const
  public    static  Newtonsoft.Json.Utilities.PrimitiveTypeCodeDBNull  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.Utilities.TypeInformation
TYPE:  class
TOKEN: 0x2000049
SIZE:  0x20
FIELDS:
  private   readonly System.Type                     <Type>k__BackingField  // 0x10
  private   readonly Newtonsoft.Json.Utilities.PrimitiveTypeCode<TypeCode>k__BackingField  // 0x18
PROPERTIES:
  Type  get=0x02B2ECC0
  TypeCode  get=0x011EF5B0
METHODS:
  RVA=0x03B46FF0  token=0x600024B  System.Void .ctor(System.Type type, Newtonsoft.Json.Utilities.PrimitiveTypeCode typeCode)
END_CLASS

CLASS: Newtonsoft.Json.Utilities.ParseResult
TYPE:  sealed struct
TOKEN: 0x200004A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.Utilities.ParseResultNone  // const
  public    static  Newtonsoft.Json.Utilities.ParseResultSuccess  // const
  public    static  Newtonsoft.Json.Utilities.ParseResultOverflow  // const
  public    static  Newtonsoft.Json.Utilities.ParseResultInvalid  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.Utilities.ConvertUtils
TYPE:  static class
TOKEN: 0x200004B
SIZE:  0x10
FIELDS:
  private   static readonly System.Collections.Generic.Dictionary<System.Type,Newtonsoft.Json.Utilities.PrimitiveTypeCode>TypeCodeMap  // static @ 0x0
  private   static readonly Newtonsoft.Json.Utilities.TypeInformation[]PrimitiveTypeCodes  // static @ 0x8
  private   static readonly Newtonsoft.Json.Utilities.ThreadSafeStore<Newtonsoft.Json.Utilities.StructMultiKey<System.Type,System.Type>,System.Func<System.Object,System.Object>>CastConverters  // static @ 0x10
METHODS:
  RVA=0x035D0DC0  token=0x600024C  Newtonsoft.Json.Utilities.PrimitiveTypeCode GetTypeCode(System.Type t)
  RVA=0x035D0F90  token=0x600024D  Newtonsoft.Json.Utilities.PrimitiveTypeCode GetTypeCode(System.Type t, System.Boolean& isEnum)
  RVA=0x0A8B16FC  token=0x600024E  Newtonsoft.Json.Utilities.TypeInformation GetTypeInformation(System.IConvertible convertable)
  RVA=0x035D0020  token=0x600024F  System.Boolean IsConvertible(System.Type t)
  RVA=0x0A8B177C  token=0x6000250  System.TimeSpan ParseTimeSpan(System.String input)
  RVA=0x0A8B0710  token=0x6000251  System.Func<System.Object,System.Object> CreateCastConverter(Newtonsoft.Json.Utilities.StructMultiKey<System.Type,System.Type> t)
  RVA=0x0A8B17EC  token=0x6000252  System.Numerics.BigInteger ToBigInteger(System.Object value)
  RVA=0x0A8B12CC  token=0x6000253  System.Object FromBigInteger(System.Numerics.BigInteger i, System.Type targetType)
  RVA=0x0A8B0458  token=0x6000254  System.Object Convert(System.Object initialValue, System.Globalization.CultureInfo culture, System.Type targetType)
  RVA=0x0A8B23CC  token=0x6000255  System.Boolean TryConvert(System.Object initialValue, System.Globalization.CultureInfo culture, System.Type targetType, System.Object& value)
  RVA=0x0A8B1BA8  token=0x6000256  Newtonsoft.Json.Utilities.ConvertUtils.ConvertResult TryConvertInternal(System.Object initialValue, System.Globalization.CultureInfo culture, System.Type targetType, System.Object& value)
  RVA=0x0A8B032C  token=0x6000257  System.Object ConvertOrCast(System.Object initialValue, System.Globalization.CultureInfo culture, System.Type targetType)
  RVA=0x0A8B10BC  token=0x6000258  System.Object EnsureTypeAssignable(System.Object value, System.Type initialType, System.Type targetType)
  RVA=0x0A8B2564  token=0x6000259  System.Boolean VersionTryParse(System.String input, System.Version& result)
  RVA=0x035D0D00  token=0x600025A  System.Boolean IsInteger(System.Object value)
  RVA=0x038C05A0  token=0x600025B  Newtonsoft.Json.Utilities.ParseResult Int32TryParse(System.Char[] chars, System.Int32 start, System.Int32 length, System.Int32& value)
  RVA=0x03824B40  token=0x600025C  Newtonsoft.Json.Utilities.ParseResult Int64TryParse(System.Char[] chars, System.Int32 start, System.Int32 length, System.Int64& value)
  RVA=0x0A8B08D0  token=0x600025D  Newtonsoft.Json.Utilities.ParseResult DecimalTryParse(System.Char[] chars, System.Int32 start, System.Int32 length, System.Decimal& value)
  RVA=0x0A8B1B5C  token=0x600025E  System.Boolean TryConvertGuid(System.String s, System.Guid& g)
  RVA=0x0A8B24CC  token=0x600025F  System.Boolean TryHexTextToInt(System.Char[] text, System.Int32 start, System.Int32 end, System.Int32& value)
  RVA=0x044E2290  token=0x6000260  System.Void .cctor()
END_CLASS

CLASS: Newtonsoft.Json.Utilities.ParserTimeZone
TYPE:  sealed struct
TOKEN: 0x200004E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Newtonsoft.Json.Utilities.ParserTimeZoneUnspecified  // const
  public    static  Newtonsoft.Json.Utilities.ParserTimeZoneUtc  // const
  public    static  Newtonsoft.Json.Utilities.ParserTimeZoneLocalWestOfUtc  // const
  public    static  Newtonsoft.Json.Utilities.ParserTimeZoneLocalEastOfUtc  // const
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.Utilities.DateTimeParser
TYPE:  sealed struct
TOKEN: 0x200004F
SIZE:  0x48
FIELDS:
  public            System.Int32                    Year  // 0x10
  public            System.Int32                    Month  // 0x14
  public            System.Int32                    Day  // 0x18
  public            System.Int32                    Hour  // 0x1c
  public            System.Int32                    Minute  // 0x20
  public            System.Int32                    Second  // 0x24
  public            System.Int32                    Fraction  // 0x28
  public            System.Int32                    ZoneHour  // 0x2c
  public            System.Int32                    ZoneMinute  // 0x30
  public            Newtonsoft.Json.Utilities.ParserTimeZoneZone  // 0x34
  private           System.Char[]                   _text  // 0x38
  private           System.Int32                    _end  // 0x40
  private   static readonly System.Int32[]                  Power10  // static @ 0x0
  private   static readonly System.Int32                    Lzyyyy  // static @ 0x8
  private   static readonly System.Int32                    Lzyyyy_  // static @ 0xc
  private   static readonly System.Int32                    Lzyyyy_MM  // static @ 0x10
  private   static readonly System.Int32                    Lzyyyy_MM_  // static @ 0x14
  private   static readonly System.Int32                    Lzyyyy_MM_dd  // static @ 0x18
  private   static readonly System.Int32                    Lzyyyy_MM_ddT  // static @ 0x1c
  private   static readonly System.Int32                    LzHH  // static @ 0x20
  private   static readonly System.Int32                    LzHH_  // static @ 0x24
  private   static readonly System.Int32                    LzHH_mm  // static @ 0x28
  private   static readonly System.Int32                    LzHH_mm_  // static @ 0x2c
  private   static readonly System.Int32                    LzHH_mm_ss  // static @ 0x30
  private   static readonly System.Int32                    Lz_  // static @ 0x34
  private   static readonly System.Int32                    Lz_zz  // static @ 0x38
METHODS:
  RVA=0x0A8B2DB8  token=0x6000263  System.Void .cctor()
  RVA=0x0A8B2CCC  token=0x6000264  System.Boolean Parse(System.Char[] text, System.Int32 startIndex, System.Int32 length)
  RVA=0x0A8B26E0  token=0x6000265  System.Boolean ParseDate(System.Int32 start)
  RVA=0x0A8B2870  token=0x6000266  System.Boolean ParseTimeAndZoneAndWhitespace(System.Int32 start)
  RVA=0x0A8B28E0  token=0x6000267  System.Boolean ParseTime(System.Int32& start)
  RVA=0x0A8B2B60  token=0x6000268  System.Boolean ParseZone(System.Int32 start)
  RVA=0x0A8B25DC  token=0x6000269  System.Boolean Parse4Digit(System.Int32 start, System.Int32& num)
  RVA=0x0A8B256C  token=0x600026A  System.Boolean Parse2Digit(System.Int32 start, System.Int32& num)
  RVA=0x0A8B26A8  token=0x600026B  System.Boolean ParseChar(System.Int32 start, System.Char ch)
END_CLASS

CLASS: Newtonsoft.Json.Utilities.DateTimeUtils
TYPE:  static class
TOKEN: 0x2000050
SIZE:  0x10
FIELDS:
  private   static readonly System.Int64                    InitialJavaScriptDateTicks  // static @ 0x0
  private   static readonly System.Int32[]                  DaysToMonth365  // static @ 0x8
  private   static readonly System.Int32[]                  DaysToMonth366  // static @ 0x10
METHODS:
  RVA=0x04B886C0  token=0x600026C  System.Void .cctor()
  RVA=0x0A8B3700  token=0x600026D  System.TimeSpan GetUtcOffset(System.DateTime d)
  RVA=0x0A8B38B0  token=0x600026E  System.Xml.XmlDateTimeSerializationMode ToSerializationMode(System.DateTimeKind kind)
  RVA=0x0A8B3424  token=0x600026F  System.DateTime EnsureDateTime(System.DateTime value, Newtonsoft.Json.DateTimeZoneHandling timeZone)
  RVA=0x0A8B3758  token=0x6000270  System.DateTime SwitchToLocalTime(System.DateTime value)
  RVA=0x0A8B3804  token=0x6000271  System.DateTime SwitchToUtcTime(System.DateTime value)
  RVA=0x0A8B3A78  token=0x6000272  System.Int64 ToUniversalTicks(System.DateTime dateTime)
  RVA=0x0A8B394C  token=0x6000273  System.Int64 ToUniversalTicks(System.DateTime dateTime, System.TimeSpan offset)
  RVA=0x0A8B30B0  token=0x6000274  System.Int64 ConvertDateTimeToJavaScriptTicks(System.DateTime dateTime, System.TimeSpan offset)
  RVA=0x0A8B310C  token=0x6000275  System.Int64 ConvertDateTimeToJavaScriptTicks(System.DateTime dateTime)
  RVA=0x0A8B3158  token=0x6000276  System.Int64 ConvertDateTimeToJavaScriptTicks(System.DateTime dateTime, System.Boolean convertToUtc)
  RVA=0x0A8B4F00  token=0x6000277  System.Int64 UniversalTicksToJavaScriptTicks(System.Int64 universalTicks)
  RVA=0x0A8B31F0  token=0x6000278  System.DateTime ConvertJavaScriptTicksToDateTime(System.Int64 javaScriptTicks)
  RVA=0x0A8B3BE0  token=0x6000279  System.Boolean TryParseDateTimeIso(Newtonsoft.Json.Utilities.StringReference text, Newtonsoft.Json.DateTimeZoneHandling dateTimeZoneHandling, System.DateTime& dt)
  RVA=0x0A8B41C0  token=0x600027A  System.Boolean TryParseDateTimeOffsetIso(Newtonsoft.Json.Utilities.StringReference text, System.DateTimeOffset& dt)
  RVA=0x0A8B32E8  token=0x600027B  System.DateTime CreateDateTime(Newtonsoft.Json.Utilities.DateTimeParser dateTimeParser)
  RVA=0x02FBB4D0  token=0x600027C  System.Boolean TryParseDateTime(Newtonsoft.Json.Utilities.StringReference s, Newtonsoft.Json.DateTimeZoneHandling dateTimeZoneHandling, System.String dateFormatString, System.Globalization.CultureInfo culture, System.DateTime& dt)
  RVA=0x0A8B49C8  token=0x600027D  System.Boolean TryParseDateTime(System.String s, Newtonsoft.Json.DateTimeZoneHandling dateTimeZoneHandling, System.String dateFormatString, System.Globalization.CultureInfo culture, System.DateTime& dt)
  RVA=0x0A8B453C  token=0x600027E  System.Boolean TryParseDateTimeOffset(Newtonsoft.Json.Utilities.StringReference s, System.String dateFormatString, System.Globalization.CultureInfo culture, System.DateTimeOffset& dt)
  RVA=0x0A8B4720  token=0x600027F  System.Boolean TryParseDateTimeOffset(System.String s, System.String dateFormatString, System.Globalization.CultureInfo culture, System.DateTimeOffset& dt)
  RVA=0x0A8B4C4C  token=0x6000280  System.Boolean TryParseMicrosoftDate(Newtonsoft.Json.Utilities.StringReference text, System.Int64& ticks, System.TimeSpan& offset, System.DateTimeKind& kind)
  RVA=0x0A8B3FF0  token=0x6000281  System.Boolean TryParseDateTimeMicrosoft(Newtonsoft.Json.Utilities.StringReference text, Newtonsoft.Json.DateTimeZoneHandling dateTimeZoneHandling, System.DateTime& dt)
  RVA=0x0A8B3B14  token=0x6000282  System.Boolean TryParseDateTimeExact(System.String text, Newtonsoft.Json.DateTimeZoneHandling dateTimeZoneHandling, System.String dateFormatString, System.Globalization.CultureInfo culture, System.DateTime& dt)
  RVA=0x0A8B4414  token=0x6000283  System.Boolean TryParseDateTimeOffsetMicrosoft(Newtonsoft.Json.Utilities.StringReference text, System.DateTimeOffset& dt)
  RVA=0x0A8B4120  token=0x6000284  System.Boolean TryParseDateTimeOffsetExact(System.String text, System.String dateFormatString, System.Globalization.CultureInfo culture, System.DateTimeOffset& dt)
  RVA=0x0A8B4DBC  token=0x6000285  System.Boolean TryReadOffset(Newtonsoft.Json.Utilities.StringReference offsetText, System.Int32 startIndex, System.TimeSpan& offset)
  RVA=0x0A8B52C8  token=0x6000286  System.Void WriteDateTimeString(System.IO.TextWriter writer, System.DateTime value, Newtonsoft.Json.DateFormatHandling format, System.String formatString, System.Globalization.CultureInfo culture)
  RVA=0x0A8B541C  token=0x6000287  System.Int32 WriteDateTimeString(System.Char[] chars, System.Int32 start, System.DateTime value, System.Nullable<System.TimeSpan> offset, System.DateTimeKind kind, Newtonsoft.Json.DateFormatHandling format)
  RVA=0x0A8B570C  token=0x6000288  System.Int32 WriteDefaultIsoDate(System.Char[] chars, System.Int32 start, System.DateTime dt)
  RVA=0x0A8B325C  token=0x6000289  System.Void CopyIntToCharArray(System.Char[] chars, System.Int32 start, System.Int32 value, System.Int32 digits)
  RVA=0x0A8B50F4  token=0x600028A  System.Int32 WriteDateTimeOffset(System.Char[] chars, System.Int32 start, System.TimeSpan offset, Newtonsoft.Json.DateFormatHandling format)
  RVA=0x0A8B4F64  token=0x600028B  System.Void WriteDateTimeOffsetString(System.IO.TextWriter writer, System.DateTimeOffset value, Newtonsoft.Json.DateFormatHandling format, System.String formatString, System.Globalization.CultureInfo culture)
  RVA=0x0A8B3544  token=0x600028C  System.Void GetDateValues(System.DateTime td, System.Int32& year, System.Int32& month, System.Int32& day)
END_CLASS

CLASS: Newtonsoft.Json.Utilities.IWrappedDictionary
TYPE:  interface
TOKEN: 0x2000051
IMPLEMENTS: System.Collections.IDictionary System.Collections.ICollection System.Collections.IEnumerable
FIELDS:
PROPERTIES:
  UnderlyingDictionary  get=-1  // abstract
METHODS:
END_CLASS

CLASS: Newtonsoft.Json.Utilities.DictionaryWrapper`2
TYPE:  class
TOKEN: 0x2000052
IMPLEMENTS: System.Collections.Generic.IDictionary`2 System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable Newtonsoft.Json.Utilities.IWrappedDictionary System.Collections.IDictionary System.Collections.ICollection
FIELDS:
  private   readonly System.Collections.IDictionary  _dictionary  // 0x0
  private   readonly System.Collections.Generic.IDictionary<TKey,TValue>_genericDictionary  // 0x0
  private   readonly System.Collections.Generic.IReadOnlyDictionary<TKey,TValue>_readOnlyDictionary  // 0x0
  private           System.Object                   _syncRoot  // 0x0
PROPERTIES:
  GenericDictionary  get=-1  // not resolved
  Keys  get=-1  // not resolved
  Values  get=-1  // not resolved
  Item  get=-1  // not resolved  set=-1  // not resolved
  Count  get=-1  // not resolved
  IsReadOnly  get=-1  // not resolved
  System.Collections.IDictionary.Item  get=-1  // not resolved  set=-1  // not resolved
  System.Collections.IDictionary.IsFixedSize  get=-1  // not resolved
  System.Collections.IDictionary.Keys  get=-1  // not resolved
  System.Collections.IDictionary.Values  get=-1  // not resolved
  System.Collections.ICollection.IsSynchronized  get=-1  // not resolved
  System.Collections.ICollection.SyncRoot  get=-1  // not resolved
  UnderlyingDictionary  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600028F  System.Void Add(TKey key, TValue value)
  RVA=-1  // not resolved  token=0x6000290  System.Boolean ContainsKey(TKey key)
  RVA=-1  // not resolved  token=0x6000292  System.Boolean Remove(TKey key)
  RVA=-1  // not resolved  token=0x6000293  System.Boolean TryGetValue(TKey key, TValue& value)
  RVA=-1  // not resolved  token=0x6000297  System.Void Add(System.Collections.Generic.KeyValuePair<TKey,TValue> item)
  RVA=-1  // not resolved  token=0x6000298  System.Void Clear()
  RVA=-1  // not resolved  token=0x6000299  System.Boolean Contains(System.Collections.Generic.KeyValuePair<TKey,TValue> item)
  RVA=-1  // not resolved  token=0x600029A  System.Void CopyTo(System.Collections.Generic.KeyValuePair<TKey,TValue>[] array, System.Int32 arrayIndex)
  RVA=-1  // not resolved  token=0x600029D  System.Boolean Remove(System.Collections.Generic.KeyValuePair<TKey,TValue> item)
  RVA=-1  // not resolved  token=0x600029E  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<TKey,TValue>> GetEnumerator()
  RVA=-1  // not resolved  token=0x600029F  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x60002A0  System.Void System.Collections.IDictionary.Add(System.Object key, System.Object value)
  RVA=-1  // not resolved  token=0x60002A3  System.Collections.IDictionaryEnumerator System.Collections.IDictionary.GetEnumerator()
  RVA=-1  // not resolved  token=0x60002A4  System.Boolean System.Collections.IDictionary.Contains(System.Object key)
  RVA=-1  // not resolved  token=0x60002A7  System.Void Remove(System.Object key)
  RVA=-1  // not resolved  token=0x60002A9  System.Void System.Collections.ICollection.CopyTo(System.Array array, System.Int32 index)
END_CLASS

CLASS: Newtonsoft.Json.Utilities.DynamicProxy`1
TYPE:  class
TOKEN: 0x2000055
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60002B7  System.Collections.Generic.IEnumerable<System.String> GetDynamicMemberNames(T instance)
  RVA=-1  // not resolved  token=0x60002B8  System.Boolean TryBinaryOperation(T instance, System.Dynamic.BinaryOperationBinder binder, System.Object arg, System.Object& result)
  RVA=-1  // not resolved  token=0x60002B9  System.Boolean TryConvert(T instance, System.Dynamic.ConvertBinder binder, System.Object& result)
  RVA=-1  // not resolved  token=0x60002BA  System.Boolean TryCreateInstance(T instance, System.Dynamic.CreateInstanceBinder binder, System.Object[] args, System.Object& result)
  RVA=-1  // not resolved  token=0x60002BB  System.Boolean TryDeleteIndex(T instance, System.Dynamic.DeleteIndexBinder binder, System.Object[] indexes)
  RVA=-1  // not resolved  token=0x60002BC  System.Boolean TryDeleteMember(T instance, System.Dynamic.DeleteMemberBinder binder)
  RVA=-1  // not resolved  token=0x60002BD  System.Boolean TryGetIndex(T instance, System.Dynamic.GetIndexBinder binder, System.Object[] indexes, System.Object& result)
  RVA=-1  // not resolved  token=0x60002BE  System.Boolean TryGetMember(T instance, System.Dynamic.GetMemberBinder binder, System.Object& result)
  RVA=-1  // not resolved  token=0x60002BF  System.Boolean TryInvoke(T instance, System.Dynamic.InvokeBinder binder, System.Object[] args, System.Object& result)
  RVA=-1  // not resolved  token=0x60002C0  System.Boolean TryInvokeMember(T instance, System.Dynamic.InvokeMemberBinder binder, System.Object[] args, System.Object& result)
  RVA=-1  // not resolved  token=0x60002C1  System.Boolean TrySetIndex(T instance, System.Dynamic.SetIndexBinder binder, System.Object[] indexes, System.Object value)
  RVA=-1  // not resolved  token=0x60002C2  System.Boolean TrySetMember(T instance, System.Dynamic.SetMemberBinder binder, System.Object value)
  RVA=-1  // not resolved  token=0x60002C3  System.Boolean TryUnaryOperation(T instance, System.Dynamic.UnaryOperationBinder binder, System.Object& result)
  RVA=-1  // not resolved  token=0x60002C4  System.Void .ctor()
END_CLASS

CLASS: Newtonsoft.Json.Utilities.DynamicProxyMetaObject`1
TYPE:  sealed class
TOKEN: 0x2000056
EXTENDS: System.Dynamic.DynamicMetaObject
FIELDS:
  private   readonly Newtonsoft.Json.Utilities.DynamicProxy<T>_proxy  // 0x0
PROPERTIES:
  NoArgs  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60002C5  System.Void .ctor(System.Linq.Expressions.Expression expression, T value, Newtonsoft.Json.Utilities.DynamicProxy<T> proxy)
  RVA=-1  // not resolved  token=0x60002C6  System.Boolean IsOverridden(System.String method)
  RVA=-1  // not resolved  token=0x60002C7  System.Dynamic.DynamicMetaObject BindGetMember(System.Dynamic.GetMemberBinder binder)
  RVA=-1  // not resolved  token=0x60002C8  System.Dynamic.DynamicMetaObject BindSetMember(System.Dynamic.SetMemberBinder binder, System.Dynamic.DynamicMetaObject value)
  RVA=-1  // not resolved  token=0x60002C9  System.Dynamic.DynamicMetaObject BindDeleteMember(System.Dynamic.DeleteMemberBinder binder)
  RVA=-1  // not resolved  token=0x60002CA  System.Dynamic.DynamicMetaObject BindConvert(System.Dynamic.ConvertBinder binder)
  RVA=-1  // not resolved  token=0x60002CB  System.Dynamic.DynamicMetaObject BindInvokeMember(System.Dynamic.InvokeMemberBinder binder, System.Dynamic.DynamicMetaObject[] args)
  RVA=-1  // not resolved  token=0x60002CC  System.Dynamic.DynamicMetaObject BindCreateInstance(System.Dynamic.CreateInstanceBinder binder, System.Dynamic.DynamicMetaObject[] args)
  RVA=-1  // not resolved  token=0x60002CD  System.Dynamic.DynamicMetaObject BindInvoke(System.Dynamic.InvokeBinder binder, System.Dynamic.DynamicMetaObject[] args)
  RVA=-1  // not resolved  token=0x60002CE  System.Dynamic.DynamicMetaObject BindBinaryOperation(System.Dynamic.BinaryOperationBinder binder, System.Dynamic.DynamicMetaObject arg)
  RVA=-1  // not resolved  token=0x60002CF  System.Dynamic.DynamicMetaObject BindUnaryOperation(System.Dynamic.UnaryOperationBinder binder)
  RVA=-1  // not resolved  token=0x60002D0  System.Dynamic.DynamicMetaObject BindGetIndex(System.Dynamic.GetIndexBinder binder, System.Dynamic.DynamicMetaObject[] indexes)
  RVA=-1  // not resolved  token=0x60002D1  System.Dynamic.DynamicMetaObject BindSetIndex(System.Dynamic.SetIndexBinder binder, System.Dynamic.DynamicMetaObject[] indexes, System.Dynamic.DynamicMetaObject value)
  RVA=-1  // not resolved  token=0x60002D2  System.Dynamic.DynamicMetaObject BindDeleteIndex(System.Dynamic.DeleteIndexBinder binder, System.Dynamic.DynamicMetaObject[] indexes)
  RVA=-1  // not resolved  token=0x60002D4  System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> GetArgs(System.Dynamic.DynamicMetaObject[] args)
  RVA=-1  // not resolved  token=0x60002D5  System.Linq.Expressions.Expression[] GetArgArray(System.Dynamic.DynamicMetaObject[] args)
  RVA=-1  // not resolved  token=0x60002D6  System.Linq.Expressions.Expression[] GetArgArray(System.Dynamic.DynamicMetaObject[] args, System.Dynamic.DynamicMetaObject value)
  RVA=-1  // not resolved  token=0x60002D7  System.Linq.Expressions.ConstantExpression Constant(System.Dynamic.DynamicMetaObjectBinder binder)
  RVA=-1  // not resolved  token=0x60002D8  System.Dynamic.DynamicMetaObject CallMethodWithResult(System.String methodName, System.Dynamic.DynamicMetaObjectBinder binder, System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> args, Newtonsoft.Json.Utilities.DynamicProxyMetaObject.Fallback<T> fallback, Newtonsoft.Json.Utilities.DynamicProxyMetaObject.Fallback<T> fallbackInvoke)
  RVA=-1  // not resolved  token=0x60002D9  System.Dynamic.DynamicMetaObject BuildCallMethodWithResult(System.String methodName, System.Dynamic.DynamicMetaObjectBinder binder, System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> args, System.Dynamic.DynamicMetaObject fallbackResult, Newtonsoft.Json.Utilities.DynamicProxyMetaObject.Fallback<T> fallbackInvoke)
  RVA=-1  // not resolved  token=0x60002DA  System.Dynamic.DynamicMetaObject CallMethodReturnLast(System.String methodName, System.Dynamic.DynamicMetaObjectBinder binder, System.Collections.Generic.IEnumerable<System.Linq.Expressions.Expression> args, Newtonsoft.Json.Utilities.DynamicProxyMetaObject.Fallback<T> fallback)
  RVA=-1  // not resolved  token=0x60002DB  System.Dynamic.DynamicMetaObject CallMethodNoResult(System.String methodName, System.Dynamic.DynamicMetaObjectBinder binder, System.Linq.Expressions.Expression[] args, Newtonsoft.Json.Utilities.DynamicProxyMetaObject.Fallback<T> fallback)
  RVA=-1  // not resolved  token=0x60002DC  System.Dynamic.BindingRestrictions GetRestrictions()
  RVA=-1  // not resolved  token=0x60002DD  System.Collections.Generic.IEnumerable<System.String> GetDynamicMemberNames()
END_CLASS

CLASS: Newtonsoft.Json.Utilities.DynamicUtils
TYPE:  static class
TOKEN: 0x2000066
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0A8B6848  token=0x60002FE  System.Collections.Generic.IEnumerable<System.String> GetDynamicMemberNames(System.Dynamic.IDynamicMetaObjectProvider dynamicProvider)
END_CLASS

CLASS: Newtonsoft.Json.Utilities.NoThrowGetBinderMember
TYPE:  class
TOKEN: 0x2000068
SIZE:  0x30
EXTENDS: System.Dynamic.GetMemberBinder
FIELDS:
  private   readonly System.Dynamic.GetMemberBinder  _innerBinder  // 0x28
METHODS:
  RVA=0x0A8B84E4  token=0x6000304  System.Void .ctor(System.Dynamic.GetMemberBinder innerBinder)
  RVA=0x0A8B8408  token=0x6000305  System.Dynamic.DynamicMetaObject FallbackGetMember(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject errorSuggestion)
END_CLASS

CLASS: Newtonsoft.Json.Utilities.NoThrowSetBinderMember
TYPE:  class
TOKEN: 0x2000069
SIZE:  0x30
EXTENDS: System.Dynamic.SetMemberBinder
FIELDS:
  private   readonly System.Dynamic.SetMemberBinder  _innerBinder  // 0x28
METHODS:
  RVA=0x0A8B8638  token=0x6000306  System.Void .ctor(System.Dynamic.SetMemberBinder innerBinder)
  RVA=0x0A8B8528  token=0x6000307  System.Dynamic.DynamicMetaObject FallbackSetMember(System.Dynamic.DynamicMetaObject target, System.Dynamic.DynamicMetaObject value, System.Dynamic.DynamicMetaObject errorSuggestion)
END_CLASS

CLASS: Newtonsoft.Json.Utilities.NoThrowExpressionVisitor
TYPE:  class
TOKEN: 0x200006A
SIZE:  0x10
EXTENDS: System.Linq.Expressions.ExpressionVisitor
FIELDS:
  private   static readonly System.Object                   ErrorResult  // static @ 0x0
METHODS:
  RVA=0x0A8B82BC  token=0x6000308  System.Linq.Expressions.Expression VisitConditional(System.Linq.Expressions.ConditionalExpression node)
  RVA=0x041E1670  token=0x6000309  System.Void .ctor()
  RVA=0x0A8B8398  token=0x600030A  System.Void .cctor()
END_CLASS

CLASS: Newtonsoft.Json.Utilities.EnumInfo
TYPE:  class
TOKEN: 0x200006B
SIZE:  0x30
FIELDS:
  public    readonly System.Boolean                  IsFlags  // 0x10
  public    readonly System.UInt64[]                 Values  // 0x18
  public    readonly System.String[]                 Names  // 0x20
  public    readonly System.String[]                 ResolvedNames  // 0x28
METHODS:
  RVA=0x0311D4E0  token=0x600030B  System.Void .ctor(System.Boolean isFlags, System.UInt64[] values, System.String[] names, System.String[] resolvedNames)
END_CLASS

CLASS: Newtonsoft.Json.Utilities.EnumUtils
TYPE:  static class
TOKEN: 0x200006C
SIZE:  0x10
FIELDS:
  private   static readonly Newtonsoft.Json.Utilities.ThreadSafeStore<Newtonsoft.Json.Utilities.StructMultiKey<System.Type,Newtonsoft.Json.Serialization.NamingStrategy>,Newtonsoft.Json.Utilities.EnumInfo>ValuesAndNamesPerEnum  // static @ 0x0
  private   static  Newtonsoft.Json.Serialization.CamelCaseNamingStrategy_camelCaseNamingStrategy  // static @ 0x8
METHODS:
  RVA=0x036FE560  token=0x600030C  Newtonsoft.Json.Utilities.EnumInfo InitializeValuesAndNames(Newtonsoft.Json.Utilities.StructMultiKey<System.Type,Newtonsoft.Json.Serialization.NamingStrategy> key)
  RVA=0x0A8B6AEC  token=0x600030D  System.Boolean TryToString(System.Type enumType, System.Object value, Newtonsoft.Json.Serialization.NamingStrategy namingStrategy, System.String& name)
  RVA=0x0A8B68B0  token=0x600030E  System.String InternalFlagsFormat(Newtonsoft.Json.Utilities.EnumInfo entry, System.UInt64 result)
  RVA=0x037D8AB0  token=0x600030F  Newtonsoft.Json.Utilities.EnumInfo GetEnumValuesAndNames(System.Type enumType)
  RVA=0x036FE9A0  token=0x6000310  System.UInt64 ToUInt64(System.Object value)
  RVA=0x035D1500  token=0x6000311  System.Object ParseEnum(System.Type enumType, Newtonsoft.Json.Serialization.NamingStrategy namingStrategy, System.String value, System.Boolean disallowNumber)
  RVA=0x0A8B6A28  token=0x6000312  System.Nullable<System.Int32> MatchName(System.String value, System.String[] enumNames, System.String[] resolvedNames, System.Int32 valueIndex, System.Int32 valueSubstringLength, System.StringComparison comparison)
  RVA=0x035D1180  token=0x6000313  System.Nullable<System.Int32> FindIndexByName(System.String[] enumNames, System.String value, System.Int32 valueIndex, System.Int32 valueSubstringLength, System.StringComparison comparison)
  RVA=0x04B445E0  token=0x6000314  System.Void .cctor()
END_CLASS

CLASS: Newtonsoft.Json.Utilities.FSharpFunction
TYPE:  class
TOKEN: 0x200006E
SIZE:  0x20
FIELDS:
  private   readonly System.Object                   _instance  // 0x10
  private   readonly Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object>_invoker  // 0x18
METHODS:
  RVA=0x032AF420  token=0x6000318  System.Void .ctor(System.Object instance, Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object> invoker)
  RVA=0x0A8B6C74  token=0x6000319  System.Object Invoke(System.Object[] args)
END_CLASS

CLASS: Newtonsoft.Json.Utilities.FSharpUtils
TYPE:  class
TOKEN: 0x200006F
SIZE:  0x70
FIELDS:
  private   static readonly System.Object                   Lock  // static @ 0x0
  private   static  Newtonsoft.Json.Utilities.FSharpUtils_instance  // static @ 0x8
  private           System.Reflection.MethodInfo    _ofSeq  // 0x10
  private           System.Type                     _mapType  // 0x18
  private           System.Reflection.Assembly      <FSharpCoreAssembly>k__BackingField  // 0x20
  private           Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object><IsUnion>k__BackingField  // 0x28
  private           Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object><GetUnionCases>k__BackingField  // 0x30
  private           Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object><PreComputeUnionTagReader>k__BackingField  // 0x38
  private           Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object><PreComputeUnionReader>k__BackingField  // 0x40
  private           Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object><PreComputeUnionConstructor>k__BackingField  // 0x48
  private           System.Func<System.Object,System.Object><GetUnionCaseInfoDeclaringType>k__BackingField  // 0x50
  private           System.Func<System.Object,System.Object><GetUnionCaseInfoName>k__BackingField  // 0x58
  private           System.Func<System.Object,System.Object><GetUnionCaseInfoTag>k__BackingField  // 0x60
  private           Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object><GetUnionCaseInfoFields>k__BackingField  // 0x68
PROPERTIES:
  Instance  get=0x0A8B7694
  FSharpCoreAssembly  set=0x05390F40
  IsUnion  get=0x04D86240  set=0x02FDB880
  GetUnionCases  get=0x011F36E0  set=0x05396200
  PreComputeUnionTagReader  get=0x04D85A50  set=0x046A91B0
  PreComputeUnionReader  get=0x04D85A60  set=0x042B4AE0
  PreComputeUnionConstructor  get=0x04D86200  set=0x035A41A0
  GetUnionCaseInfoDeclaringType  get=0x04D86270  set=0x02FDAEA0
  GetUnionCaseInfoName  get=0x02E56440  set=0x036E5590
  GetUnionCaseInfoTag  get=0x04D86280  set=0x063CC164
  GetUnionCaseInfoFields  get=0x04D86230  set=0x0485F170
METHODS:
  RVA=0x0A8B72B4  token=0x600031A  System.Void .ctor(System.Reflection.Assembly fsharpCoreAssembly)
  RVA=0x0A8B7034  token=0x600032F  System.Void EnsureInitialized(System.Reflection.Assembly fsharpCoreAssembly)
  RVA=0x0A8B71E8  token=0x6000330  System.Reflection.MethodInfo GetMethodWithNonPublicFallback(System.Type type, System.String methodName, System.Reflection.BindingFlags bindingFlags)
  RVA=0x0A8B6CA4  token=0x6000331  Newtonsoft.Json.Utilities.MethodCall<System.Object,System.Object> CreateFSharpFuncCall(System.Type type, System.String methodName)
  RVA=0x0A8B6F74  token=0x6000332  Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> CreateSeq(System.Type t)
  RVA=0x0A8B6E24  token=0x6000333  Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> CreateMap(System.Type keyType, System.Type valueType)
  RVA=-1  // generic def  token=0x6000334  Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> BuildMapCreator()
  RVA=0x0A8B7244  token=0x6000335  System.Void .cctor()
END_CLASS

CLASS: Newtonsoft.Json.Utilities.ImmutableCollectionsUtils
TYPE:  static class
TOKEN: 0x2000073
SIZE:  0x10
FIELDS:
  private   static readonly System.Collections.Generic.IList<Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.ImmutableCollectionTypeInfo>ArrayContractImmutableCollectionDefinitions  // static @ 0x0
  private   static readonly System.Collections.Generic.IList<Newtonsoft.Json.Utilities.ImmutableCollectionsUtils.ImmutableCollectionTypeInfo>DictionaryContractImmutableCollectionDefinitions  // static @ 0x8
METHODS:
  RVA=0x042182D0  token=0x600033D  System.Boolean TryBuildImmutableForArrayContract(System.Type underlyingType, System.Type collectionItemType, System.Type& createdType, Newtonsoft.Json.Serialization.ObjectConstructor<System.Object>& parameterizedCreator)
  RVA=0x042180D0  token=0x600033E  System.Boolean TryBuildImmutableForDictionaryContract(System.Type underlyingType, System.Type keyItemType, System.Type valueItemType, System.Type& createdType, Newtonsoft.Json.Serialization.ObjectConstructor<System.Object>& parameterizedCreator)
  RVA=0x0464FD30  token=0x600033F  System.Void .cctor()
END_CLASS

CLASS: Newtonsoft.Json.Utilities.BufferUtils
TYPE:  static class
TOKEN: 0x2000078
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x03778910  token=0x600034F  System.Char[] RentBuffer(Newtonsoft.Json.IArrayPool<System.Char> bufferPool, System.Int32 minSize)
  RVA=0x03778690  token=0x6000350  System.Void ReturnBuffer(Newtonsoft.Json.IArrayPool<System.Char> bufferPool, System.Char[] buffer)
  RVA=0x0A8AFE38  token=0x6000351  System.Char[] EnsureBufferSize(Newtonsoft.Json.IArrayPool<System.Char> bufferPool, System.Int32 size, System.Char[] buffer)
END_CLASS

CLASS: Newtonsoft.Json.Utilities.JavaScriptUtils
TYPE:  static class
TOKEN: 0x2000079
SIZE:  0x10
FIELDS:
  private   static readonly System.Boolean[]                SingleQuoteCharEscapeFlags  // static @ 0x0
  private   static readonly System.Boolean[]                DoubleQuoteCharEscapeFlags  // static @ 0x8
  private   static readonly System.Boolean[]                HtmlCharEscapeFlags  // static @ 0x10
METHODS:
  RVA=0x04063150  token=0x6000352  System.Void .cctor()
  RVA=0x03425E70  token=0x6000353  System.Boolean[] GetCharEscapeFlags(Newtonsoft.Json.StringEscapeHandling stringEscapeHandling, System.Char quoteChar)
  RVA=0x030BCB50  token=0x6000354  System.Boolean ShouldEscapeJavaScriptString(System.String s, System.Boolean[] charEscapeFlags)
  RVA=0x03778F00  token=0x6000355  System.Void WriteEscapedJavaScriptString(System.IO.TextWriter writer, System.String s, System.Char delimiter, System.Boolean appendDelimiters, System.Boolean[] charEscapeFlags, Newtonsoft.Json.StringEscapeHandling stringEscapeHandling, Newtonsoft.Json.IArrayPool<System.Char> bufferPool, System.Char[]& writeBuffer)
  RVA=0x0A8B76E4  token=0x6000356  System.String ToEscapedJavaScriptString(System.String value, System.Char delimiter, System.Boolean appendDelimiters, Newtonsoft.Json.StringEscapeHandling stringEscapeHandling)
  RVA=0x037795A0  token=0x6000357  System.Int32 FirstCharToEscape(System.String s, System.Boolean[] charEscapeFlags, Newtonsoft.Json.StringEscapeHandling stringEscapeHandling)
  RVA=0x0A8B79C8  token=0x6000358  System.Boolean TryGetDateFromConstructorJson(Newtonsoft.Json.JsonReader reader, System.DateTime& dateTime, System.String& errorMessage)
  RVA=0x0A8B786C  token=0x6000359  System.Boolean TryGetDateConstructorValue(Newtonsoft.Json.JsonReader reader, System.Nullable<System.Int64>& integer, System.String& errorMessage)
END_CLASS

CLASS: Newtonsoft.Json.Utilities.JsonTokenUtils
TYPE:  static class
TOKEN: 0x200007A
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x03424FE0  token=0x600035A  System.Boolean IsEndToken(Newtonsoft.Json.JsonToken token)
  RVA=0x03425330  token=0x600035B  System.Boolean IsStartToken(Newtonsoft.Json.JsonToken token)
  RVA=0x045F7460  token=0x600035C  System.Boolean IsPrimitiveToken(Newtonsoft.Json.JsonToken token)
END_CLASS

CLASS: Newtonsoft.Json.Utilities.LateBoundReflectionDelegateFactory
TYPE:  class
TOKEN: 0x200007B
SIZE:  0x10
EXTENDS: Newtonsoft.Json.Utilities.ReflectionDelegateFactory
FIELDS:
  private   static readonly Newtonsoft.Json.Utilities.LateBoundReflectionDelegateFactory_instance  // static @ 0x0
PROPERTIES:
  Instance  get=0x0A8B7E5C
METHODS:
  RVA=0x04820B70  token=0x600035E  Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> CreateParameterizedConstructor(System.Reflection.MethodBase method)
  RVA=-1  // generic def  token=0x600035F  Newtonsoft.Json.Utilities.MethodCall<T,System.Object> CreateMethodCall(System.Reflection.MethodBase method)
  RVA=-1  // generic def  token=0x6000360  System.Func<T> CreateDefaultConstructor(System.Type type)
  RVA=-1  // generic def  token=0x6000361  System.Func<T,System.Object> CreateGet(System.Reflection.PropertyInfo propertyInfo)
  RVA=-1  // generic def  token=0x6000362  System.Func<T,System.Object> CreateGet(System.Reflection.FieldInfo fieldInfo)
  RVA=-1  // generic def  token=0x6000363  System.Action<T,System.Object> CreateSet(System.Reflection.FieldInfo fieldInfo)
  RVA=-1  // generic def  token=0x6000364  System.Action<T,System.Object> CreateSet(System.Reflection.PropertyInfo propertyInfo)
  RVA=0x041E1670  token=0x6000365  System.Void .ctor()
  RVA=0x04D4E0D0  token=0x6000366  System.Void .cctor()
END_CLASS

CLASS: Newtonsoft.Json.Utilities.MathUtils
TYPE:  static class
TOKEN: 0x2000083
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x03778AC0  token=0x6000378  System.Int32 IntLength(System.UInt64 i)
  RVA=0x0A8B7F4C  token=0x6000379  System.Char IntToHex(System.Int32 n)
  RVA=0x0A8B7EAC  token=0x600037A  System.Boolean ApproxEquals(System.Double d1, System.Double d2)
END_CLASS

CLASS: Newtonsoft.Json.Utilities.MethodCall`2
TYPE:  sealed class
TOKEN: 0x2000084
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x600037B  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x600037C  TResult Invoke(T target, System.Object[] args)
END_CLASS

CLASS: Newtonsoft.Json.Utilities.MiscellaneousUtils
TYPE:  static class
TOKEN: 0x2000085
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x04164380  token=0x600037D  System.Boolean ValueEquals(System.Object objA, System.Object objB)
  RVA=0x0A8B7FE4  token=0x600037E  System.ArgumentOutOfRangeException CreateArgumentOutOfRangeException(System.String paramName, System.Object actualValue, System.String message)
  RVA=0x0A8B8238  token=0x600037F  System.String ToString(System.Object value)
  RVA=0x0A8B7F5C  token=0x6000380  System.Int32 ByteArrayCompare(System.Byte[] a1, System.Byte[] a2)
  RVA=0x0A8B80EC  token=0x6000381  System.String GetPrefix(System.String qualifiedName)
  RVA=0x0A8B80C0  token=0x6000382  System.String GetLocalName(System.String qualifiedName)
  RVA=0x0A8B8118  token=0x6000383  System.Void GetQualifiedNameParts(System.String qualifiedName, System.String& prefix, System.String& localName)
  RVA=0x0A8B81BC  token=0x6000384  System.Text.RegularExpressions.RegexOptions GetRegexOptions(System.String optionsText)
END_CLASS

CLASS: Newtonsoft.Json.Utilities.ReflectionDelegateFactory
TYPE:  abstract class
TOKEN: 0x2000086
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000385  System.Func<T,System.Object> CreateGet(System.Reflection.MemberInfo memberInfo)
  RVA=-1  // generic def  token=0x6000386  System.Action<T,System.Object> CreateSet(System.Reflection.MemberInfo memberInfo)
  RVA=-1  // abstract  token=0x6000387  Newtonsoft.Json.Utilities.MethodCall<T,System.Object> CreateMethodCall(System.Reflection.MethodBase method)
  RVA=-1  // abstract  token=0x6000388  Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> CreateParameterizedConstructor(System.Reflection.MethodBase method)
  RVA=-1  // abstract  token=0x6000389  System.Func<T> CreateDefaultConstructor(System.Type type)
  RVA=-1  // abstract  token=0x600038A  System.Func<T,System.Object> CreateGet(System.Reflection.PropertyInfo propertyInfo)
  RVA=-1  // abstract  token=0x600038B  System.Func<T,System.Object> CreateGet(System.Reflection.FieldInfo fieldInfo)
  RVA=-1  // abstract  token=0x600038C  System.Action<T,System.Object> CreateSet(System.Reflection.FieldInfo fieldInfo)
  RVA=-1  // abstract  token=0x600038D  System.Action<T,System.Object> CreateSet(System.Reflection.PropertyInfo propertyInfo)
  RVA=0x041E1670  token=0x600038E  System.Void .ctor()
END_CLASS

CLASS: Newtonsoft.Json.Utilities.ReflectionMember
TYPE:  class
TOKEN: 0x2000087
SIZE:  0x28
FIELDS:
  private           System.Type                     <MemberType>k__BackingField  // 0x10
  private           System.Func<System.Object,System.Object><Getter>k__BackingField  // 0x18
  private           System.Action<System.Object,System.Object><Setter>k__BackingField  // 0x20
PROPERTIES:
  MemberType  get=0x02B2ECC0  set=0x053908C0
  Getter  get=0x0385B100  set=0x05392C40
  Setter  get=0x04D862C0  set=0x05390F40
METHODS:
  RVA=0x041E1670  token=0x6000395  System.Void .ctor()
END_CLASS

CLASS: Newtonsoft.Json.Utilities.ReflectionObject
TYPE:  class
TOKEN: 0x2000088
SIZE:  0x20
FIELDS:
  private   readonly Newtonsoft.Json.Serialization.ObjectConstructor<System.Object><Creator>k__BackingField  // 0x10
  private   readonly System.Collections.Generic.IDictionary<System.String,Newtonsoft.Json.Utilities.ReflectionMember><Members>k__BackingField  // 0x18
PROPERTIES:
  Creator  get=0x02B2ECC0
  Members  get=0x0385B100
METHODS:
  RVA=0x0A8B8ECC  token=0x6000398  System.Void .ctor(Newtonsoft.Json.Serialization.ObjectConstructor<System.Object> creator)
  RVA=0x0A8B8DD4  token=0x6000399  System.Object GetValue(System.Object target, System.String member)
  RVA=0x0A8B8E48  token=0x600039A  System.Void SetValue(System.Object target, System.String member, System.Object value)
  RVA=0x0A8B8D7C  token=0x600039B  System.Type GetType(System.String member)
  RVA=0x0A8B867C  token=0x600039C  Newtonsoft.Json.Utilities.ReflectionObject Create(System.Type t, System.String[] memberNames)
  RVA=0x0A8B868C  token=0x600039D  Newtonsoft.Json.Utilities.ReflectionObject Create(System.Type t, System.Reflection.MethodBase creator, System.String[] memberNames)
END_CLASS

CLASS: Newtonsoft.Json.Utilities.ReflectionUtils
TYPE:  static class
TOKEN: 0x200008C
SIZE:  0x10
FIELDS:
  public    static readonly System.Type[]                   EmptyTypes  // static @ 0x0
METHODS:
  RVA=0x04D31570  token=0x60003A4  System.Void .cctor()
  RVA=0x02F34FC0  token=0x60003A5  System.Boolean IsVirtual(System.Reflection.PropertyInfo propertyInfo)
  RVA=0x02F34B10  token=0x60003A6  System.Reflection.MethodInfo GetBaseDefinition(System.Reflection.PropertyInfo propertyInfo)
  RVA=0x02F346B0  token=0x60003A7  System.Boolean IsPublic(System.Reflection.PropertyInfo property)
  RVA=0x02FB2150  token=0x60003A8  System.Type GetObjectType(System.Object v)
  RVA=0x0A8B95FC  token=0x60003A9  System.String GetTypeName(System.Type t, Newtonsoft.Json.TypeNameAssemblyFormatHandling assemblyFormat, Newtonsoft.Json.Serialization.ISerializationBinder binder)
  RVA=0x0A8B9508  token=0x60003AA  System.String GetFullyQualifiedTypeName(System.Type t, Newtonsoft.Json.Serialization.ISerializationBinder binder)
  RVA=0x0A8B97B8  token=0x60003AB  System.String RemoveAssemblyDetails(System.String fullyQualifiedTypeName)
  RVA=0x035CF460  token=0x60003AC  System.Boolean HasDefaultConstructor(System.Type t, System.Boolean nonPublic)
  RVA=0x035CEF40  token=0x60003AD  System.Reflection.ConstructorInfo GetDefaultConstructor(System.Type t)
  RVA=0x035CEF80  token=0x60003AE  System.Reflection.ConstructorInfo GetDefaultConstructor(System.Type t, System.Boolean nonPublic)
  RVA=0x035D0080  token=0x60003AF  System.Boolean IsNullable(System.Type t)
  RVA=0x035CFEC0  token=0x60003B0  System.Boolean IsNullableType(System.Type t)
  RVA=0x035CF0F0  token=0x60003B1  System.Type EnsureNotNullableType(System.Type t)
  RVA=0x035D0C80  token=0x60003B2  System.Type EnsureNotByRefType(System.Type t)
  RVA=0x044778A0  token=0x60003B3  System.Boolean IsGenericDefinition(System.Type type, System.Type genericInterfaceDefinition)
  RVA=0x035CF9D0  token=0x60003B4  System.Boolean ImplementsGenericDefinition(System.Type type, System.Type genericInterfaceDefinition)
  RVA=0x035CFD90  token=0x60003B5  System.Boolean ImplementsGenericDefinition(System.Type type, System.Type genericInterfaceDefinition, System.Type& implementingType)
  RVA=0x035CE250  token=0x60003B6  System.Boolean InheritsGenericDefinition(System.Type type, System.Type genericClassDefinition)
  RVA=0x035CE2A0  token=0x60003B7  System.Boolean InheritsGenericDefinition(System.Type type, System.Type genericClassDefinition, System.Type& implementingType)
  RVA=0x035CE350  token=0x60003B8  System.Boolean InheritsGenericDefinitionInternal(System.Type type, System.Type genericClassDefinition, System.Type& implementingType)
  RVA=0x049E6300  token=0x60003B9  System.Type GetCollectionItemType(System.Type type)
  RVA=0x0A8B9220  token=0x60003BA  System.Void GetDictionaryKeyValueTypes(System.Type dictionaryType, System.Type& keyType, System.Type& valueType)
  RVA=0x032203F0  token=0x60003BB  System.Type GetMemberUnderlyingType(System.Reflection.MemberInfo member)
  RVA=0x032B4820  token=0x60003BC  System.Boolean IsByRefLikeType(System.Type type)
  RVA=0x02F2EED0  token=0x60003BD  System.Boolean IsIndexedProperty(System.Reflection.PropertyInfo property)
  RVA=0x035949B0  token=0x60003BE  System.Object GetMemberValue(System.Reflection.MemberInfo member, System.Object target)
  RVA=0x02FB2E10  token=0x60003BF  System.Void SetMemberValue(System.Reflection.MemberInfo member, System.Object target, System.Object value)
  RVA=0x0321FED0  token=0x60003C0  System.Boolean CanReadMemberValue(System.Reflection.MemberInfo member, System.Boolean nonPublic)
  RVA=0x0321FDB0  token=0x60003C1  System.Boolean CanSetMemberValue(System.Reflection.MemberInfo member, System.Boolean nonPublic, System.Boolean canSetReadOnly)
  RVA=0x02F32150  token=0x60003C2  System.Collections.Generic.List<System.Reflection.MemberInfo> GetFieldsAndProperties(System.Type type, System.Reflection.BindingFlags bindingAttr)
  RVA=0x04CB1A30  token=0x60003C3  System.Boolean IsOverridenGenericMember(System.Reflection.MemberInfo memberInfo, System.Reflection.BindingFlags bindingAttr)
  RVA=-1  // generic def  token=0x60003C4  T GetAttribute(System.Object attributeProvider)
  RVA=-1  // generic def  token=0x60003C5  T GetAttribute(System.Object attributeProvider, System.Boolean inherit)
  RVA=-1  // generic def  token=0x60003C6  T[] GetAttributes(System.Object attributeProvider, System.Boolean inherit)
  RVA=0x0312C910  token=0x60003C7  System.Attribute[] GetAttributes(System.Object attributeProvider, System.Type attributeType, System.Boolean inherit)
  RVA=0x02FBD590  token=0x60003C8  Newtonsoft.Json.Utilities.StructMultiKey<System.String,System.String> SplitFullyQualifiedTypeName(System.String fullyQualifiedTypeName)
  RVA=0x02FBEB90  token=0x60003C9  System.Nullable<System.Int32> GetAssemblyDelimiterIndex(System.String fullyQualifiedTypeName)
  RVA=0x0312D520  token=0x60003CA  System.Reflection.MemberInfo GetMemberInfoFromType(System.Type targetType, System.Reflection.MemberInfo memberInfo)
  RVA=0x02F32E60  token=0x60003CB  System.Collections.Generic.IEnumerable<System.Reflection.FieldInfo> GetFields(System.Type targetType, System.Reflection.BindingFlags bindingAttr)
  RVA=0x035D00D0  token=0x60003CC  System.Void GetChildPrivateFields(System.Collections.Generic.IList<System.Reflection.MemberInfo> initialFields, System.Type type, System.Reflection.BindingFlags bindingAttr)
  RVA=0x02F32F10  token=0x60003CD  System.Collections.Generic.IEnumerable<System.Reflection.PropertyInfo> GetProperties(System.Type targetType, System.Reflection.BindingFlags bindingAttr)
  RVA=0x035CED60  token=0x60003CE  System.Reflection.BindingFlags RemoveFlag(System.Reflection.BindingFlags bindingAttr, System.Reflection.BindingFlags flag)
  RVA=0x02F347C0  token=0x60003CF  System.Void GetChildPrivateProperties(System.Collections.Generic.IList<System.Reflection.PropertyInfo> initialProperties, System.Type type, System.Reflection.BindingFlags bindingAttr)
  RVA=0x0A8B96C0  token=0x60003D0  System.Boolean IsMethodOverridden(System.Type currentType, System.Type methodDeclaringType, System.String method)
  RVA=0x0A8B8F5C  token=0x60003D1  System.Object GetDefaultValue(System.Type type)
END_CLASS

CLASS: Newtonsoft.Json.Utilities.StringBuffer
TYPE:  sealed struct
TOKEN: 0x2000092
SIZE:  0x20
FIELDS:
  private           System.Char[]                   _buffer  // 0x10
  private           System.Int32                    _position  // 0x18
PROPERTIES:
  Position  get=0x04D88B10  set=0x04D88B40
  IsEmpty  get=0x0314CD60
  InternalBuffer  get=0x02B76770
METHODS:
  RVA=0x037787C0  token=0x60003E4  System.Void .ctor(Newtonsoft.Json.IArrayPool<System.Char> bufferPool, System.Int32 initalSize)
  RVA=0x05393828  token=0x60003E5  System.Void .ctor(System.Char[] buffer)
  RVA=0x04208C70  token=0x60003E6  System.Void Append(Newtonsoft.Json.IArrayPool<System.Char> bufferPool, System.Char value)
  RVA=0x04208CD0  token=0x60003E7  System.Void Append(Newtonsoft.Json.IArrayPool<System.Char> bufferPool, System.Char[] buffer, System.Int32 startIndex, System.Int32 count)
  RVA=0x037786D0  token=0x60003E8  System.Void Clear(Newtonsoft.Json.IArrayPool<System.Char> bufferPool)
  RVA=0x0A8B98D4  token=0x60003E9  System.Void EnsureSize(Newtonsoft.Json.IArrayPool<System.Char> bufferPool, System.Int32 appendLength)
  RVA=0x0A8B9940  token=0x60003EA  System.String ToString()
  RVA=0x0A8B9950  token=0x60003EB  System.String ToString(System.Int32 start, System.Int32 length)
END_CLASS

CLASS: Newtonsoft.Json.Utilities.StringReference
TYPE:  sealed struct
TOKEN: 0x2000093
SIZE:  0x20
FIELDS:
  private   readonly System.Char[]                   _chars  // 0x10
  private   readonly System.Int32                    _startIndex  // 0x18
  private   readonly System.Int32                    _length  // 0x1c
PROPERTIES:
  Item  get=0x02FBB5E0
  Chars  get=0x02B76770
  StartIndex  get=0x04D88B10
  Length  get=0x04D8CAD0
METHODS:
  RVA=0x05390898  token=0x60003F1  System.Void .ctor(System.Char[] chars, System.Int32 startIndex, System.Int32 length)
  RVA=0x05390BC8  token=0x60003F2  System.String ToString()
END_CLASS

CLASS: Newtonsoft.Json.Utilities.StringReferenceExtensions
TYPE:  static class
TOKEN: 0x2000094
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0A8B9A00  token=0x60003F3  System.Int32 IndexOf(Newtonsoft.Json.Utilities.StringReference s, System.Char c, System.Int32 startIndex, System.Int32 length)
  RVA=0x0A8B9A80  token=0x60003F4  System.Boolean StartsWith(Newtonsoft.Json.Utilities.StringReference s, System.String text)
  RVA=0x0A8B9974  token=0x60003F5  System.Boolean EndsWith(Newtonsoft.Json.Utilities.StringReference s, System.String text)
END_CLASS

CLASS: Newtonsoft.Json.Utilities.StringUtils
TYPE:  static class
TOKEN: 0x2000095
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x05390C14  token=0x60003F6  System.Boolean IsNullOrEmpty(System.String value)
  RVA=0x0A8B9E80  token=0x60003F7  System.String FormatWith(System.String format, System.IFormatProvider provider, System.Object arg0)
  RVA=0x0A8B9F18  token=0x60003F8  System.String FormatWith(System.String format, System.IFormatProvider provider, System.Object arg0, System.Object arg1)
  RVA=0x0A8B9DA0  token=0x60003F9  System.String FormatWith(System.String format, System.IFormatProvider provider, System.Object arg0, System.Object arg1, System.Object arg2)
  RVA=0x0A8B9CA4  token=0x60003FA  System.String FormatWith(System.String format, System.IFormatProvider provider, System.Object arg0, System.Object arg1, System.Object arg2, System.Object arg3)
  RVA=0x0A8B9BFC  token=0x60003FB  System.String FormatWith(System.String format, System.IFormatProvider provider, System.Object[] args)
  RVA=0x0A8B9B08  token=0x60003FC  System.IO.StringWriter CreateStringWriter(System.Int32 capacity)
  RVA=0x0A8BA26C  token=0x60003FD  System.Void ToCharAsUnicode(System.Char c, System.Char[] buffer)
  RVA=-1  // generic def  token=0x60003FE  TSource ForgivingCaseSensitiveFind(System.Collections.Generic.IEnumerable<TSource> source, System.Func<TSource,System.String> valueSelector, System.String testValue)
  RVA=0x0A8BA0C0  token=0x60003FF  System.String ToCamelCase(System.String s)
  RVA=0x0A8BA34C  token=0x6000400  System.Char ToLower(System.Char c)
  RVA=0x0A8BA604  token=0x6000401  System.String ToSnakeCase(System.String s)
  RVA=0x0A8BA33C  token=0x6000402  System.String ToKebabCase(System.String s)
  RVA=0x0A8BA3BC  token=0x6000403  System.String ToSeparatedCase(System.String s, System.Char separator)
  RVA=0x0A8B9FF4  token=0x6000404  System.Boolean IsHighSurrogate(System.Char c)
  RVA=0x0A8BA040  token=0x6000405  System.Boolean IsLowSurrogate(System.Char c)
  RVA=0x0A8B9FD8  token=0x6000406  System.Int32 IndexOf(System.String s, System.Char c)
  RVA=0x0A8BA08C  token=0x6000407  System.Boolean StartsWith(System.String source, System.Char value)
  RVA=0x0A8B9BC4  token=0x6000408  System.Boolean EndsWith(System.String source, System.Char value)
  RVA=0x02FBD6B0  token=0x6000409  System.String Trim(System.String s, System.Int32 start, System.Int32 length)
END_CLASS

CLASS: Newtonsoft.Json.Utilities.StructMultiKey`2
TYPE:  sealed struct
TOKEN: 0x2000098
IMPLEMENTS: System.IEquatable`1
FIELDS:
  public    readonly T1                              Value1  // 0x0
  public    readonly T2                              Value2  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600040D  System.Void .ctor(T1 v1, T2 v2)
  RVA=-1  // not resolved  token=0x600040E  System.Int32 GetHashCode()
  RVA=-1  // not resolved  token=0x600040F  System.Boolean Equals(System.Object obj)
  RVA=-1  // not resolved  token=0x6000410  System.Boolean Equals(Newtonsoft.Json.Utilities.StructMultiKey<T1,T2> other)
END_CLASS

CLASS: Newtonsoft.Json.Utilities.ThreadSafeStore`2
TYPE:  class
TOKEN: 0x2000099
FIELDS:
  private   readonly System.Collections.Concurrent.ConcurrentDictionary<TKey,TValue>_concurrentStore  // 0x0
  private   readonly System.Func<TKey,TValue>        _creator  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000411  System.Void .ctor(System.Func<TKey,TValue> creator)
  RVA=-1  // not resolved  token=0x6000412  TValue Get(TKey key)
END_CLASS

CLASS: Newtonsoft.Json.Utilities.TypeExtensions
TYPE:  static class
TOKEN: 0x200009A
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x02FB3040  token=0x6000413  System.Reflection.MemberTypes MemberType(System.Reflection.MemberInfo memberInfo)
  RVA=0x0A8BA634  token=0x6000414  System.Boolean ContainsGenericParameters(System.Type type)
  RVA=0x035CFF60  token=0x6000415  System.Boolean IsInterface(System.Type type)
  RVA=0x035CFF40  token=0x6000416  System.Boolean IsGenericType(System.Type type)
  RVA=0x035CFF80  token=0x6000417  System.Boolean IsGenericTypeDefinition(System.Type type)
  RVA=0x032B4E20  token=0x6000418  System.Type BaseType(System.Type type)
  RVA=0x0A8BA614  token=0x6000419  System.Reflection.Assembly Assembly(System.Type type)
  RVA=0x02F3A240  token=0x600041A  System.Boolean IsEnum(System.Type type)
  RVA=0x035CE330  token=0x600041B  System.Boolean IsClass(System.Type type)
  RVA=0x035CFFD0  token=0x600041C  System.Boolean IsSealed(System.Type type)
  RVA=0x035CFFA0  token=0x600041D  System.Boolean IsAbstract(System.Type type)
  RVA=0x0A8BA8BC  token=0x600041E  System.Boolean IsVisible(System.Type type)
  RVA=0x03131F30  token=0x600041F  System.Boolean IsValueType(System.Type type)
  RVA=0x032B4CF0  token=0x6000420  System.Boolean AssignableToTypeName(System.Type type, System.String fullTypeName, System.Boolean searchInterfaces, System.Type& match)
  RVA=0x05396E6C  token=0x6000421  System.Boolean AssignableToTypeName(System.Type type, System.String fullTypeName, System.Boolean searchInterfaces)
  RVA=0x0A8BA654  token=0x6000422  System.Boolean ImplementInterface(System.Type type, System.Type interfaceType)
END_CLASS

CLASS: Newtonsoft.Json.Utilities.ValidationUtils
TYPE:  static class
TOKEN: 0x200009B
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x02FB3000  token=0x6000423  System.Void ArgumentNotNull(System.Object value, System.String parameterName)
END_CLASS

CLASS: System.Diagnostics.CodeAnalysis.NotNullAttribute
TYPE:  sealed class
TOKEN: 0x2000006
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000006  System.Void .ctor()
END_CLASS

CLASS: System.Diagnostics.CodeAnalysis.NotNullWhenAttribute
TYPE:  sealed class
TOKEN: 0x2000007
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  private   readonly System.Boolean                  <ReturnValue>k__BackingField  // 0x10
METHODS:
  RVA=0x04D86130  token=0x6000007  System.Void .ctor(System.Boolean returnValue)
END_CLASS

CLASS: System.Runtime.CompilerServices.IsReadOnlyAttribute
TYPE:  sealed class
TOKEN: 0x2000003
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000002  System.Void .ctor()
END_CLASS

CLASS: System.Runtime.CompilerServices.NullableAttribute
TYPE:  sealed class
TOKEN: 0x2000004
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.Byte[]                   NullableFlags  // 0x10
METHODS:
  RVA=0x03F261D0  token=0x6000003  System.Void .ctor(System.Byte )
  RVA=0x053908C0  token=0x6000004  System.Void .ctor(System.Byte[] )
END_CLASS

CLASS: System.Runtime.CompilerServices.NullableContextAttribute
TYPE:  sealed class
TOKEN: 0x2000005
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.Byte                     Flag  // 0x10
METHODS:
  RVA=0x04D86130  token=0x6000005  System.Void .ctor(System.Byte )
END_CLASS

