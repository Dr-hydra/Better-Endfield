// ========================================================
// Dumped by @desirepro
// Assembly: Unity.VisualScripting.Core.dll
// Classes:  260
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
TOKEN: 0x2000008
SIZE:  0x10
FIELDS:
  public    static readonly Unity.VisualScripting.FakeSerializationCloner.<>c<>9  // static @ 0x0
  public    static  System.Func<Unity.VisualScripting.FullSerializer.fsMetaProperty,System.Reflection.MemberInfo><>9__6_0  // static @ 0x8
METHODS:
  RVA=0x0B36BBC4  token=0x600001F  System.Void .cctor()
  RVA=0x041E1670  token=0x6000020  System.Void .ctor()
  RVA=0x06C08920  token=0x6000021  System.Reflection.MemberInfo <GetMembers>b__6_0(Unity.VisualScripting.FullSerializer.fsMetaProperty p)
END_CLASS

CLASS: <>c__DisplayClass35_0
TYPE:  sealed class
TOKEN: 0x200000E
SIZE:  0x18
FIELDS:
  public            System.Type                     type  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000045  System.Void .ctor()
  RVA=0x0B36B81C  token=0x6000046  System.Boolean <GetCloner>b__0(Unity.VisualScripting.ICloner cloner)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000010
SIZE:  0x10
FIELDS:
  public    static readonly Unity.VisualScripting.CloningContext.<>c<>9  // static @ 0x0
  public    static  System.Func<Unity.VisualScripting.CloningContext><>9__15_0  // static @ 0x8
METHODS:
  RVA=0x04D454F0  token=0x6000051  System.Void .cctor()
  RVA=0x041E1670  token=0x6000052  System.Void .ctor()
  RVA=0x039E80E0  token=0x6000053  Unity.VisualScripting.CloningContext <New>b__15_0()
END_CLASS

CLASS: Enumerator
TYPE:  sealed struct
TOKEN: 0x2000018
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Collections.Generic.Dictionary.Enumerator<System.Type,Unity.VisualScripting.IKeyedCollection<TKey,TItem>>collectionsEnumerator  // 0x0
  private           TItem                           currentItem  // 0x0
  private           Unity.VisualScripting.IKeyedCollection<TKey,TItem>currentCollection  // 0x0
  private           System.Int32                    indexInCurrentCollection  // 0x0
  private           System.Boolean                  exceeded  // 0x0
PROPERTIES:
  Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600006A  System.Void .ctor(Unity.VisualScripting.MergedKeyedCollection<TKey,TItem> merged)
  RVA=-1  // not resolved  token=0x600006B  System.Void Dispose()
  RVA=-1  // not resolved  token=0x600006C  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x600006F  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000022
SIZE:  0x10
FIELDS:
  public    static readonly Unity.VisualScripting.EditorTimeBinding.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x0B36BC28  token=0x6000090  System.Void .cctor()
  RVA=0x041E1670  token=0x6000091  System.Void .ctor()
  RVA=0x0B36B490  token=0x6000092  System.Int32 <.cctor>b__6_0()
  RVA=0x0B36B498  token=0x6000093  System.Single <.cctor>b__6_1()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200002A
SIZE:  0x10
FIELDS:
  public    static readonly Unity.VisualScripting.XString.<>c<>9  // static @ 0x0
  public    static  System.Func<System.String,System.Object><>9__1_0  // static @ 0x8
METHODS:
  RVA=0x0B36B96C  token=0x60000A6  System.Void .cctor()
  RVA=0x041E1670  token=0x60000A7  System.Void .ctor()
  RVA=0x04D88C30  token=0x60000A8  System.Object <Inject>b__1_0(System.String a)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000030
SIZE:  0x10
FIELDS:
  public    static readonly Unity.VisualScripting.Graph.<>c <>9  // static @ 0x0
  public    static  System.Func<Unity.VisualScripting.IGraphElement,System.Collections.Generic.IEnumerable<Unity.VisualScripting.ISerializationDependency>><>9__27_0  // static @ 0x8
  public    static  System.Comparison<Unity.VisualScripting.IGraphElement><>9__30_0  // static @ 0x10
METHODS:
  RVA=0x0B36BA98  token=0x60000B5  System.Void .cctor()
  RVA=0x041E1670  token=0x60000B6  System.Void .ctor()
  RVA=0x0B36B5A8  token=0x60000B7  System.Collections.Generic.IEnumerable<Unity.VisualScripting.ISerializationDependency> <get_deserializationDependencies>b__27_0(Unity.VisualScripting.IGraphElement e)
  RVA=0x0B36B2BC  token=0x60000B8  System.Int32 <OnAfterDependenciesDeserialized>b__30_0(Unity.VisualScripting.IGraphElement a, Unity.VisualScripting.IGraphElement b)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000038
SIZE:  0x10
FIELDS:
  public    static readonly Unity.VisualScripting.GraphStack.<>c<>9  // static @ 0x0
  public    static  System.Func<Unity.VisualScripting.GraphStack><>9__3_0  // static @ 0x8
METHODS:
  RVA=0x0B36BA34  token=0x60000F7  System.Void .cctor()
  RVA=0x041E1670  token=0x60000F8  System.Void .ctor()
  RVA=0x0B36B26C  token=0x60000F9  Unity.VisualScripting.GraphStack <New>b__3_0()
END_CLASS

CLASS: AttributeCache
TYPE:  class
TOKEN: 0x200004E
SIZE:  0x20
FIELDS:
  private   readonly System.Collections.Generic.List<System.Attribute><inheritedAttributes>k__BackingField  // 0x10
  private   readonly System.Collections.Generic.List<System.Attribute><definedAttributes>k__BackingField  // 0x18
PROPERTIES:
  inheritedAttributes  get=0x02B2ECC0
  definedAttributes  get=0x0385B100
METHODS:
  RVA=0x0B352A74  token=0x6000124  System.Void .ctor(System.Reflection.MemberInfo element)
  RVA=0x0B352888  token=0x6000125  System.Void Cache(System.Attribute[] attributeObjects, System.Collections.Generic.List<System.Attribute> cache)
  RVA=0x0B3529E0  token=0x6000126  System.Boolean HasAttribute(System.Type attributeType, System.Collections.Generic.List<System.Attribute> cache)
  RVA=0x0B352920  token=0x6000127  System.Attribute GetAttribute(System.Type attributeType, System.Collections.Generic.List<System.Attribute> cache)
  RVA=0x0B3529C4  token=0x6000128  System.Boolean HasAttribute(System.Type attributeType, System.Boolean inherit)
  RVA=0x0B352904  token=0x6000129  System.Attribute GetAttribute(System.Type attributeType, System.Boolean inherit)
  RVA=-1  // generic def  token=0x600012A  System.Boolean HasAttribute(System.Boolean inherit)
  RVA=-1  // generic def  token=0x600012B  TAttribute GetAttribute(System.Boolean inherit)
END_CLASS

CLASS: ConversionType
TYPE:  sealed struct
TOKEN: 0x2000050
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Unity.VisualScripting.ConversionUtility.ConversionTypeImpossible  // const
  public    static  Unity.VisualScripting.ConversionUtility.ConversionTypeIdentity  // const
  public    static  Unity.VisualScripting.ConversionUtility.ConversionTypeUpcast  // const
  public    static  Unity.VisualScripting.ConversionUtility.ConversionTypeDowncast  // const
  public    static  Unity.VisualScripting.ConversionUtility.ConversionTypeNumericImplicit  // const
  public    static  Unity.VisualScripting.ConversionUtility.ConversionTypeNumericExplicit  // const
  public    static  Unity.VisualScripting.ConversionUtility.ConversionTypeUserDefinedImplicit  // const
  public    static  Unity.VisualScripting.ConversionUtility.ConversionTypeUserDefinedExplicit  // const
  public    static  Unity.VisualScripting.ConversionUtility.ConversionTypeUserDefinedThenNumericImplicit  // const
  public    static  Unity.VisualScripting.ConversionUtility.ConversionTypeUserDefinedThenNumericExplicit  // const
  public    static  Unity.VisualScripting.ConversionUtility.ConversionTypeUnityHierarchy  // const
  public    static  Unity.VisualScripting.ConversionUtility.ConversionTypeEnumerableToArray  // const
  public    static  Unity.VisualScripting.ConversionUtility.ConversionTypeEnumerableToList  // const
  public    static  Unity.VisualScripting.ConversionUtility.ConversionTypeToString  // const
METHODS:
END_CLASS

CLASS: ConversionQuery
TYPE:  sealed struct
TOKEN: 0x2000051
SIZE:  0x20
IMPLEMENTS: System.IEquatable`1
FIELDS:
  public    readonly System.Type                     source  // 0x10
  public    readonly System.Type                     destination  // 0x18
METHODS:
  RVA=0x039CF630  token=0x6000148  System.Void .ctor(System.Type source, System.Type destination)
  RVA=0x0B35363C  token=0x6000149  System.Boolean Equals(Unity.VisualScripting.ConversionUtility.ConversionQuery other)
  RVA=0x0B3536AC  token=0x600014A  System.Boolean Equals(System.Object obj)
  RVA=0x0B35371C  token=0x600014B  System.Int32 GetHashCode()
END_CLASS

CLASS: ConversionQueryComparer
TYPE:  sealed struct
TOKEN: 0x2000052
SIZE:  0x11
IMPLEMENTS: System.Collections.Generic.IEqualityComparer`1
FIELDS:
METHODS:
  RVA=0x0B353604  token=0x600014C  System.Boolean Equals(Unity.VisualScripting.ConversionUtility.ConversionQuery x, Unity.VisualScripting.ConversionUtility.ConversionQuery y)
  RVA=0x0B353628  token=0x600014D  System.Int32 GetHashCode(Unity.VisualScripting.ConversionUtility.ConversionQuery obj)
END_CLASS

CLASS: <>c__DisplayClass11_0
TYPE:  sealed class
TOKEN: 0x2000053
SIZE:  0x18
FIELDS:
  public            System.Type                     source  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600014E  System.Void .ctor()
  RVA=0x0B36B5C4  token=0x600014F  System.Boolean <FindUserDefinedConversionMethods>b__2(System.Reflection.MethodInfo m)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000054
SIZE:  0x10
FIELDS:
  public    static readonly Unity.VisualScripting.ConversionUtility.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Reflection.MethodInfo,System.Boolean><>9__11_0  // static @ 0x8
  public    static  System.Func<System.Reflection.MethodInfo,System.Boolean><>9__11_1  // static @ 0x10
METHODS:
  RVA=0x0B36BCF0  token=0x6000150  System.Void .cctor()
  RVA=0x041E1670  token=0x6000151  System.Void .ctor()
  RVA=0x0B36B144  token=0x6000152  System.Boolean <FindUserDefinedConversionMethods>b__11_0(System.Reflection.MethodInfo m)
  RVA=0x0B36B190  token=0x6000153  System.Boolean <FindUserDefinedConversionMethods>b__11_1(System.Reflection.MethodInfo m)
END_CLASS

CLASS: <>c__DisplayClass13_0
TYPE:  sealed class
TOKEN: 0x2000055
SIZE:  0x18
FIELDS:
  public            System.Type                     destination  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000154  System.Void .ctor()
  RVA=0x0B36B66C  token=0x6000155  System.Boolean <GetUserDefinedConversionType>b__0(System.Reflection.MethodInfo m)
  RVA=0x0B36B6E0  token=0x6000156  System.Boolean <GetUserDefinedConversionType>b__1(System.Reflection.MethodInfo m)
  RVA=0x0B36B75C  token=0x6000157  System.Boolean <GetUserDefinedConversionType>b__2(System.Reflection.MethodInfo m)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000058
SIZE:  0x10
FIELDS:
  public    static readonly Unity.VisualScripting.MemberUtility.<>c<>9  // static @ 0x0
METHODS:
  RVA=0x0B36B908  token=0x6000162  System.Void .cctor()
  RVA=0x041E1670  token=0x6000163  System.Void .ctor()
  RVA=0x0B36B380  token=0x6000164  Unity.VisualScripting.ExtensionMethodCache <.cctor>b__0_0()
  RVA=0x0B36B3D0  token=0x6000165  System.Collections.Generic.Dictionary<System.Type,System.Reflection.MethodInfo[]> <.cctor>b__0_1()
  RVA=0x0B36B430  token=0x6000166  System.Collections.Generic.HashSet<System.Reflection.MethodInfo> <.cctor>b__0_2()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200005A
SIZE:  0x10
FIELDS:
  public    static readonly Unity.VisualScripting.ExtensionMethodCache.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Type,System.Boolean><>9__0_0  // static @ 0x8
  public    static  System.Func<System.Type,System.Collections.Generic.IEnumerable<System.Reflection.MethodInfo>><>9__0_1  // static @ 0x10
  public    static  System.Func<System.Reflection.MethodInfo,System.Boolean><>9__0_2  // static @ 0x18
METHODS:
  RVA=0x0B36BAFC  token=0x6000168  System.Void .cctor()
  RVA=0x041E1670  token=0x6000169  System.Void .ctor()
  RVA=0x0B36B4A0  token=0x600016A  System.Boolean <.ctor>b__0_0(System.Type type)
  RVA=0x0B36B514  token=0x600016B  System.Collections.Generic.IEnumerable<System.Reflection.MethodInfo> <.ctor>b__0_1(System.Type type)
  RVA=0x0B36B534  token=0x600016C  System.Boolean <.ctor>b__0_2(System.Reflection.MethodInfo method)
END_CLASS

CLASS: Collection
TYPE:  class
TOKEN: 0x200005C
SIZE:  0x30
EXTENDS: System.Collections.ObjectModel.KeyedCollection`2
IMPLEMENTS: Unity.VisualScripting.IKeyedCollection`2 System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
METHODS:
  RVA=0x09B24654  token=0x6000177  System.String GetKeyForItem(Unity.VisualScripting.Namespace item)
  RVA=0x0B3534C0  token=0x6000178  System.Boolean TryGetValue(System.String key, Unity.VisualScripting.Namespace& value)
  RVA=0x0B3535C8  token=0x6000179  System.Void .ctor()
  RVA=0x0B353580  token=0x600017A  Unity.VisualScripting.Namespace Unity.VisualScripting.IKeyedCollection<System.String,Unity.VisualScripting.Namespace>.get_Item(System.String key)
  RVA=0x0B353538  token=0x600017B  System.Boolean Unity.VisualScripting.IKeyedCollection<System.String,Unity.VisualScripting.Namespace>.Contains(System.String key)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000076
SIZE:  0x10
FIELDS:
  public    static readonly Unity.VisualScripting.OptimizedReflection.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Reflection.ParameterInfo,System.Boolean><>9__39_0  // static @ 0x8
METHODS:
  RVA=0x0B36BC8C  token=0x60001F0  System.Void .cctor()
  RVA=0x041E1670  token=0x60001F1  System.Void .ctor()
  RVA=0x0B36B358  token=0x60001F2  System.Boolean <SupportsOptimization>b__39_0(System.Reflection.ParameterInfo parameter)
END_CLASS

CLASS: <>c__DisplayClass5_0
TYPE:  sealed class
TOKEN: 0x2000082
FIELDS:
  public            TField                          constant  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000228  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000229  TField <Compile>b__2()
END_CLASS

CLASS: <GetAssemblyAttributes>d__15
TYPE:  sealed class
TOKEN: 0x2000090
SIZE:  0x58
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Attribute                <>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x20
  private           System.Type                     attributeType  // 0x28
  public            System.Type                     <>3__attributeType  // 0x30
  private           System.Collections.Generic.IEnumerable<System.Reflection.Assembly>assemblies  // 0x38
  public            System.Collections.Generic.IEnumerable<System.Reflection.Assembly><>3__assemblies  // 0x40
  private           System.Collections.Generic.IEnumerator<System.Reflection.Assembly><>7__wrap1  // 0x48
  private           System.Collections.Generic.IEnumerator<System.Attribute><>7__wrap2  // 0x50
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Attribute>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x03A11010  token=0x600026E  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x0B36AA34  token=0x600026F  System.Void System.IDisposable.Dispose()
  RVA=0x0B36A458  token=0x6000270  System.Boolean MoveNext()
  RVA=0x0B36AB20  token=0x6000271  System.Void <>m__Finally1()
  RVA=0x0B36AB68  token=0x6000272  System.Void <>m__Finally2()
  RVA=0x0B36A9E8  token=0x6000274  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x0B36A93C  token=0x6000276  System.Collections.Generic.IEnumerator<System.Attribute> System.Collections.Generic.IEnumerable<System.Attribute>.GetEnumerator()
  RVA=0x0B36A9E0  token=0x6000277  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <>c__DisplayClass25_0
TYPE:  sealed class
TOKEN: 0x2000091
SIZE:  0x20
FIELDS:
  public            Unity.VisualScripting.TypeName  typeName  // 0x10
  public            System.Func<System.Reflection.Assembly,System.Boolean><>9__0  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000278  System.Void .ctor()
  RVA=0x0B36B7D8  token=0x6000279  System.Boolean <TrySystemTypeLookup>b__0(System.Reflection.Assembly a)
END_CLASS

CLASS: ParseState
TYPE:  sealed struct
TOKEN: 0x2000093
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Unity.VisualScripting.TypeName.ParseStateName  // const
  public    static  Unity.VisualScripting.TypeName.ParseStateArray  // const
  public    static  Unity.VisualScripting.TypeName.ParseStateGenerics  // const
  public    static  Unity.VisualScripting.TypeName.ParseStateAssembly  // const
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000094
SIZE:  0x10
FIELDS:
  public    static readonly Unity.VisualScripting.TypeName.<>c<>9  // static @ 0x0
  public    static  System.Func<System.String,System.String><>9__35_0  // static @ 0x8
METHODS:
  RVA=0x0B36B9D0  token=0x6000293  System.Void .cctor()
  RVA=0x041E1670  token=0x6000294  System.Void .ctor()
  RVA=0x0B36B580  token=0x6000295  System.String <.ctor>b__35_0(System.String x)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000097
SIZE:  0x10
FIELDS:
  public    static readonly Unity.VisualScripting.TypeUtility.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Type,System.Boolean><>9__35_0  // static @ 0x8
  public    static  System.Func<System.Type,System.Boolean><>9__35_1  // static @ 0x10
METHODS:
  RVA=0x0B36BB60  token=0x600029D  System.Void .cctor()
  RVA=0x041E1670  token=0x600029E  System.Void .ctor()
  RVA=0x0B36B1DC  token=0x600029F  System.Boolean <GetTypesSafely>b__35_0(System.Type t)
  RVA=0x0B36B224  token=0x60002A0  System.Boolean <GetTypesSafely>b__35_1(System.Type t)
END_CLASS

CLASS: <GetTypesSafely>d__35
TYPE:  sealed class
TOKEN: 0x2000098
SIZE:  0x48
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Type                     <>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x20
  private           System.Reflection.Assembly      assembly  // 0x28
  public            System.Reflection.Assembly      <>3__assembly  // 0x30
  private           System.Type[]                   <>7__wrap1  // 0x38
  private           System.Int32                    <>7__wrap2  // 0x40
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Type>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x03A11010  token=0x60002A1  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60002A2  System.Void System.IDisposable.Dispose()
  RVA=0x0B36ABB4  token=0x60002A3  System.Boolean MoveNext()
  RVA=0x0B36B0F8  token=0x60002A5  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x0B36B05C  token=0x60002A7  System.Collections.Generic.IEnumerator<System.Type> System.Collections.Generic.IEnumerable<System.Type>.GetEnumerator()
  RVA=0x0B36B0F0  token=0x60002A8  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <Concat>d__0`1
TYPE:  sealed class
TOKEN: 0x20000B0
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           T                               <>2__current  // 0x0
  private           System.Int32                    <>l__initialThreadId  // 0x0
  private           System.Collections.IEnumerable[]enumerables  // 0x0
  public            System.Collections.IEnumerable[]<>3__enumerables  // 0x0
  private           System.Collections.Generic.IEnumerator<System.Collections.IEnumerable><>7__wrap1  // 0x0
  private           System.Collections.Generic.IEnumerator<T><>7__wrap2  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<T>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000312  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x6000313  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x6000314  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x6000315  System.Void <>m__Finally1()
  RVA=-1  // not resolved  token=0x6000316  System.Void <>m__Finally2()
  RVA=-1  // not resolved  token=0x6000318  System.Void System.Collections.IEnumerator.Reset()
  RVA=-1  // not resolved  token=0x600031A  System.Collections.Generic.IEnumerator<T> System.Collections.Generic.IEnumerable<T>.GetEnumerator()
  RVA=-1  // not resolved  token=0x600031B  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <>c__2`1
TYPE:  sealed class
TOKEN: 0x20000B1
FIELDS:
  public    static readonly Unity.VisualScripting.LinqUtility.<>c__2<T><>9  // static @ 0x0
  public    static  System.Func<T,System.Boolean>   <>9__2_0  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600031C  System.Void .cctor()
  RVA=-1  // not resolved  token=0x600031D  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600031E  System.Boolean <NotNull>b__2_0(T i)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000B3
FIELDS:
  public    static readonly Unity.VisualScripting.Recursion.<>c<T><>9  // static @ 0x0
  public    static  System.Func<Unity.VisualScripting.Recursion<T>><>9__13_0  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000329  System.Void .cctor()
  RVA=-1  // not resolved  token=0x600032A  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600032B  Unity.VisualScripting.Recursion<T> <New>b__13_0()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000B6
SIZE:  0x10
FIELDS:
  public    static readonly Unity.VisualScripting.ReferenceCollector.<>c<>9  // static @ 0x0
  public    static  UnityEngine.Events.UnityAction<UnityEngine.SceneManagement.Scene><>9__3_0  // static @ 0x8
METHODS:
  RVA=0x04D3FCD0  token=0x6000334  System.Void .cctor()
  RVA=0x041E1670  token=0x6000335  System.Void .ctor()
  RVA=0x04D54B20  token=0x6000336  System.Void <Initialize>b__3_0(UnityEngine.SceneManagement.Scene scene)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000BF
SIZE:  0x10
FIELDS:
  public    static readonly Unity.VisualScripting.SavedVariables.<>c<>9  // static @ 0x0
  public    static  System.Action                   <>9__8_0  // static @ 0x8
  public    static  System.Func<Unity.VisualScripting.VariableDeclaration,System.String><>9__24_0  // static @ 0x10
METHODS:
  RVA=0x04D4B0D0  token=0x600035F  System.Void .cctor()
  RVA=0x041E1670  token=0x6000360  System.Void .ctor()
  RVA=0x0B36B300  token=0x6000361  System.Void <OnEnterPlayMode>b__8_0()
  RVA=0x06C08920  token=0x6000362  System.String <MergeInitialAndSavedDeclarations>b__24_0(Unity.VisualScripting.VariableDeclaration vd)
END_CLASS

CLASS: <>c__DisplayClass5_0
TYPE:  sealed class
TOKEN: 0x20000CB
SIZE:  0x18
FIELDS:
  public            System.Enum                     x  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60003A2  System.Void .ctor()
  RVA=0x0B36CA88  token=0x60003A3  System.ValueTuple<System.Enum,System.String> <TryDeserialize>b__3(Unity.VisualScripting.RenamedFromAttribute attr)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000CC
SIZE:  0x10
FIELDS:
  public    static readonly Unity.VisualScripting.FullSerializer.fsEnumConverter.<>c<>9  // static @ 0x0
  public    static  System.Func<System.Enum,System.Collections.Generic.IEnumerable<System.ValueTuple<System.Enum,System.String>>><>9__5_0  // static @ 0x8
  public    static  System.Func<System.ValueTuple<System.Enum,System.String>,System.String><>9__5_1  // static @ 0x10
  public    static  System.Func<System.ValueTuple<System.Enum,System.String>,System.Enum><>9__5_2  // static @ 0x18
METHODS:
  RVA=0x0B36CAF8  token=0x60003A4  System.Void .cctor()
  RVA=0x041E1670  token=0x60003A5  System.Void .ctor()
  RVA=0x0B36C7D0  token=0x60003A6  System.Collections.Generic.IEnumerable<System.ValueTuple<System.Enum,System.String>> <TryDeserialize>b__5_0(System.Enum x)
  RVA=0x04D94CB0  token=0x60003A7  System.String <TryDeserialize>b__5_1(System.ValueTuple<System.Enum,System.String> x)
  RVA=0x02A55C30  token=0x60003A8  System.Enum <TryDeserialize>b__5_2(System.ValueTuple<System.Enum,System.String> x)
END_CLASS

CLASS: AotCompilation
TYPE:  sealed struct
TOKEN: 0x20000D9
SIZE:  0x28
FIELDS:
  public            System.Type                     Type  // 0x10
  public            Unity.VisualScripting.FullSerializer.fsMetaProperty[]Members  // 0x18
  public            System.Boolean                  IsConstructorPublic  // 0x20
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000DB
SIZE:  0x10
FIELDS:
  public    static readonly Unity.VisualScripting.FullSerializer.fsBaseConverter.<>c<>9  // static @ 0x0
  public    static  System.Func<Unity.VisualScripting.FullSerializer.fsDataType,System.String><>9__6_0  // static @ 0x8
METHODS:
  RVA=0x0B36CB5C  token=0x60003F7  System.Void .cctor()
  RVA=0x041E1670  token=0x60003F8  System.Void .ctor()
  RVA=0x0B36C780  token=0x60003F9  System.String <FailExpectedType>b__6_0(Unity.VisualScripting.FullSerializer.fsDataType t)
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x20000DE
SIZE:  0x10
FIELDS:
  public    static readonly Unity.VisualScripting.FullSerializer.fsConfig.<>c<>9  // static @ 0x0
  public    static  System.Func<System.String,System.Reflection.MemberInfo,System.String><>9__10_0  // static @ 0x8
METHODS:
  RVA=0x04D448F0  token=0x60003FC  System.Void .cctor()
  RVA=0x041E1670  token=0x60003FD  System.Void .ctor()
  RVA=0x04D88C30  token=0x60003FE  System.String <.ctor>b__10_0(System.String name, System.Reflection.MemberInfo info)
END_CLASS

CLASS: fsLazyCycleDefinitionWriter
TYPE:  class
TOKEN: 0x20000F3
SIZE:  0x20
FIELDS:
  private           System.Collections.Generic.Dictionary<System.Int32,Unity.VisualScripting.FullSerializer.fsData>_pendingDefinitions  // 0x10
  private           System.Collections.Generic.HashSet<System.Int32>_references  // 0x18
METHODS:
  RVA=0x0B377570  token=0x6000499  System.Void WriteDefinition(System.Int32 id, Unity.VisualScripting.FullSerializer.fsData data)
  RVA=0x0B3776C0  token=0x600049A  System.Void WriteReference(System.Int32 id, System.Collections.Generic.Dictionary<System.String,Unity.VisualScripting.FullSerializer.fsData> dict)
  RVA=0x0B377508  token=0x600049B  System.Void Clear()
  RVA=0x0B3778D4  token=0x600049C  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass16_0
TYPE:  sealed class
TOKEN: 0x20000F6
SIZE:  0x18
FIELDS:
  public            System.Reflection.MemberInfo    member  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60004BE  System.Void .ctor()
  RVA=0x0B36C8D0  token=0x60004BF  System.Boolean <CollectProperties>b__0(System.Type t)
  RVA=0x0B36C924  token=0x60004C0  System.Boolean <CollectProperties>b__1(System.Type t)
  RVA=0x0B36C978  token=0x60004C1  System.Boolean <CollectProperties>b__2(System.Type t)
END_CLASS

CLASS: <>c__DisplayClass18_0
TYPE:  sealed class
TOKEN: 0x20000F7
SIZE:  0x18
FIELDS:
  public            System.Reflection.PropertyInfo  property  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60004C2  System.Void .ctor()
  RVA=0x0B36C9CC  token=0x60004C3  System.Boolean <CanSerializeProperty>b__0(System.Type t)
END_CLASS

CLASS: <>c__DisplayClass19_0
TYPE:  sealed class
TOKEN: 0x20000F8
SIZE:  0x18
FIELDS:
  public            System.Reflection.FieldInfo     field  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60004C4  System.Void .ctor()
  RVA=0x0B36CA20  token=0x60004C5  System.Boolean <CanSerializeField>b__0(System.Type t)
END_CLASS

CLASS: ObjectReferenceEqualityComparator
TYPE:  class
TOKEN: 0x20000FB
SIZE:  0x10
IMPLEMENTS: System.Collections.Generic.IEqualityComparer`1
FIELDS:
  public    static readonly System.Collections.Generic.IEqualityComparer<System.Object>Instance  // static @ 0x0
METHODS:
  RVA=0x0829C278  token=0x60004CF  System.Boolean System.Collections.Generic.IEqualityComparer<System.Object>.Equals(System.Object x, System.Object y)
  RVA=0x04DA3AC0  token=0x60004D0  System.Int32 System.Collections.Generic.IEqualityComparer<System.Object>.GetHashCode(System.Object obj)
  RVA=0x041E1670  token=0x60004D1  System.Void .ctor()
  RVA=0x0B36C71C  token=0x60004D2  System.Void .cctor()
END_CLASS

CLASS: AttributeQuery
TYPE:  sealed struct
TOKEN: 0x20000FF
SIZE:  0x20
FIELDS:
  public            System.Reflection.MemberInfo    MemberInfo  // 0x10
  public            System.Type                     AttributeType  // 0x18
METHODS:
END_CLASS

CLASS: AttributeQueryComparator
TYPE:  class
TOKEN: 0x2000100
SIZE:  0x10
IMPLEMENTS: System.Collections.Generic.IEqualityComparer`1
FIELDS:
METHODS:
  RVA=0x0B36C66C  token=0x60004E8  System.Boolean Equals(Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.AttributeQuery x, Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.AttributeQuery y)
  RVA=0x0B36C6D0  token=0x60004E9  System.Int32 GetHashCode(Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.AttributeQuery obj)
  RVA=0x041E1670  token=0x60004EA  System.Void .ctor()
END_CLASS

CLASS: <>c__DisplayClass2_0
TYPE:  sealed class
TOKEN: 0x2000102
SIZE:  0x18
FIELDS:
  public            System.Boolean                  includeNamespace  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60004ED  System.Void .ctor()
  RVA=0x0B36CA74  token=0x60004EE  System.String <CSharpName>b__0(System.Type t)
END_CLASS

CLASS: Unity.VisualScripting.Cloner`1
TYPE:  abstract class
TOKEN: 0x2000002
IMPLEMENTS: Unity.VisualScripting.ICloner
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000001  System.Void .ctor()
  RVA=-1  // abstract  token=0x6000002  System.Boolean Handles(System.Type type)
  RVA=-1  // not resolved  token=0x6000003  System.Void Unity.VisualScripting.ICloner.BeforeClone(System.Type type, System.Object original)
  RVA=-1  // not resolved  token=0x6000004  System.Void BeforeClone(System.Type type, T original)
  RVA=-1  // not resolved  token=0x6000005  System.Object Unity.VisualScripting.ICloner.ConstructClone(System.Type type, System.Object original)
  RVA=-1  // not resolved  token=0x6000006  T ConstructClone(System.Type type, T original)
  RVA=-1  // not resolved  token=0x6000007  System.Void Unity.VisualScripting.ICloner.FillClone(System.Type type, System.Object& clone, System.Object original, Unity.VisualScripting.CloningContext context)
  RVA=-1  // not resolved  token=0x6000008  System.Void FillClone(System.Type type, T& clone, T original, Unity.VisualScripting.CloningContext context)
  RVA=-1  // not resolved  token=0x6000009  System.Void Unity.VisualScripting.ICloner.AfterClone(System.Type type, System.Object clone)
  RVA=-1  // not resolved  token=0x600000A  System.Void AfterClone(System.Type type, T clone)
END_CLASS

CLASS: Unity.VisualScripting.AnimationCurveCloner
TYPE:  sealed class
TOKEN: 0x2000003
SIZE:  0x10
EXTENDS: Unity.VisualScripting.Cloner`1
FIELDS:
METHODS:
  RVA=0x0B352534  token=0x600000B  System.Boolean Handles(System.Type type)
  RVA=0x0B352424  token=0x600000C  UnityEngine.AnimationCurve ConstructClone(System.Type type, UnityEngine.AnimationCurve original)
  RVA=0x0B352474  token=0x600000D  System.Void FillClone(System.Type type, UnityEngine.AnimationCurve& clone, UnityEngine.AnimationCurve original, Unity.VisualScripting.CloningContext context)
  RVA=0x045BFD30  token=0x600000E  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.ArrayCloner
TYPE:  sealed class
TOKEN: 0x2000004
SIZE:  0x10
EXTENDS: Unity.VisualScripting.Cloner`1
FIELDS:
METHODS:
  RVA=0x0B352868  token=0x600000F  System.Boolean Handles(System.Type type)
  RVA=0x0B35274C  token=0x6000010  System.Array ConstructClone(System.Type type, System.Array original)
  RVA=0x0B352778  token=0x6000011  System.Void FillClone(System.Type type, System.Array& clone, System.Array original, Unity.VisualScripting.CloningContext context)
  RVA=0x045BFD00  token=0x6000012  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.DictionaryCloner
TYPE:  sealed class
TOKEN: 0x2000005
SIZE:  0x10
EXTENDS: Unity.VisualScripting.Cloner`1
FIELDS:
METHODS:
  RVA=0x0B35706C  token=0x6000013  System.Boolean Handles(System.Type type)
  RVA=0x0B356F80  token=0x6000014  System.Void FillClone(System.Type type, System.Collections.IDictionary& clone, System.Collections.IDictionary original, Unity.VisualScripting.CloningContext context)
  RVA=0x045BFCD0  token=0x6000015  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.EnumerableCloner
TYPE:  sealed class
TOKEN: 0x2000006
SIZE:  0x18
EXTENDS: Unity.VisualScripting.Cloner`1
FIELDS:
  private   readonly System.Collections.Generic.Dictionary<System.Type,Unity.VisualScripting.IOptimizedInvoker>addMethods  // 0x10
METHODS:
  RVA=0x0B3578E8  token=0x6000016  System.Boolean Handles(System.Type type)
  RVA=0x0B3574D4  token=0x6000017  System.Void FillClone(System.Type type, System.Collections.IEnumerable& clone, System.Collections.IEnumerable original, Unity.VisualScripting.CloningContext context)
  RVA=0x0B3576EC  token=0x6000018  Unity.VisualScripting.IOptimizedInvoker GetAddMethod(System.Type type)
  RVA=0x045BFDD0  token=0x6000019  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.FakeSerializationCloner
TYPE:  sealed class
TOKEN: 0x2000007
SIZE:  0x28
EXTENDS: Unity.VisualScripting.ReflectedCloner
FIELDS:
  private           Unity.VisualScripting.FullSerializer.fsConfig<config>k__BackingField  // 0x20
PROPERTIES:
  config  get=0x04D862C0
METHODS:
  RVA=0x0B358610  token=0x600001B  System.Void BeforeClone(System.Type type, System.Object original)
  RVA=0x0B3585C8  token=0x600001C  System.Void AfterClone(System.Type type, System.Object clone)
  RVA=0x0B358658  token=0x600001D  System.Collections.Generic.IEnumerable<System.Reflection.MemberInfo> GetMembers(System.Type type)
  RVA=0x045BFD60  token=0x600001E  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.FieldsCloner
TYPE:  sealed class
TOKEN: 0x2000009
SIZE:  0x20
EXTENDS: Unity.VisualScripting.ReflectedCloner
FIELDS:
METHODS:
  RVA=0x02FFF600  token=0x6000022  System.Boolean IncludeField(System.Reflection.FieldInfo field)
  RVA=0x012081B0  token=0x6000023  System.Boolean IncludeProperty(System.Reflection.PropertyInfo property)
  RVA=0x0539798C  token=0x6000024  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.GradientCloner
TYPE:  sealed class
TOKEN: 0x200000A
SIZE:  0x10
EXTENDS: Unity.VisualScripting.Cloner`1
FIELDS:
METHODS:
  RVA=0x0B358868  token=0x6000025  System.Boolean Handles(System.Type type)
  RVA=0x0B358798  token=0x6000026  UnityEngine.Gradient ConstructClone(System.Type type, UnityEngine.Gradient original)
  RVA=0x0B3587E8  token=0x6000027  System.Void FillClone(System.Type type, UnityEngine.Gradient& clone, UnityEngine.Gradient original, Unity.VisualScripting.CloningContext context)
  RVA=0x045BFCA0  token=0x6000028  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.ListCloner
TYPE:  sealed class
TOKEN: 0x200000B
SIZE:  0x10
EXTENDS: Unity.VisualScripting.Cloner`1
FIELDS:
METHODS:
  RVA=0x0B35C140  token=0x6000029  System.Boolean Handles(System.Type type)
  RVA=0x0B35BF9C  token=0x600002A  System.Void FillClone(System.Type type, System.Collections.IList& clone, System.Collections.IList original, Unity.VisualScripting.CloningContext context)
  RVA=0x045BFC70  token=0x600002B  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.ReflectedCloner
TYPE:  abstract class
TOKEN: 0x200000C
SIZE:  0x20
EXTENDS: Unity.VisualScripting.Cloner`1
FIELDS:
  private   readonly System.Collections.Generic.Dictionary<System.Type,System.Reflection.MemberInfo[]>accessors  // 0x10
  private   readonly System.Collections.Generic.Dictionary<System.Type,Unity.VisualScripting.IOptimizedAccessor[]>optimizedAccessors  // 0x18
METHODS:
  RVA=0x012081B0  token=0x600002C  System.Boolean Handles(System.Type type)
  RVA=0x0B361738  token=0x600002D  System.Void FillClone(System.Type type, System.Object& clone, System.Object original, Unity.VisualScripting.CloningContext context)
  RVA=0x0B361AB8  token=0x600002E  System.Reflection.MemberInfo[] GetAccessors(System.Type type)
  RVA=0x0B361D48  token=0x600002F  Unity.VisualScripting.IOptimizedAccessor[] GetOptimizedAccessors(System.Type type)
  RVA=0x0B361B94  token=0x6000030  System.Collections.Generic.IEnumerable<System.Reflection.MemberInfo> GetMembers(System.Type type)
  RVA=0x012081B0  token=0x6000031  System.Boolean IncludeField(System.Reflection.FieldInfo field)
  RVA=0x012081B0  token=0x6000032  System.Boolean IncludeProperty(System.Reflection.PropertyInfo property)
  RVA=0x045BFE60  token=0x6000033  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.Cloning
TYPE:  static class
TOKEN: 0x200000D
SIZE:  0x10
FIELDS:
  private   static readonly System.Collections.Generic.Dictionary<System.Type,System.Boolean>skippable  // static @ 0x0
  private   static readonly System.Collections.Generic.HashSet<Unity.VisualScripting.ICloner><cloners>k__BackingField  // static @ 0x8
  private   static readonly Unity.VisualScripting.ArrayCloner<arrayCloner>k__BackingField  // static @ 0x10
  private   static readonly Unity.VisualScripting.DictionaryCloner<dictionaryCloner>k__BackingField  // static @ 0x18
  private   static readonly Unity.VisualScripting.EnumerableCloner<enumerableCloner>k__BackingField  // static @ 0x20
  private   static readonly Unity.VisualScripting.ListCloner<listCloner>k__BackingField  // static @ 0x28
  private   static readonly Unity.VisualScripting.AnimationCurveCloner<animationCurveCloner>k__BackingField  // static @ 0x30
  private   static readonly Unity.VisualScripting.GradientCloner<gradientCloner>k__BackingField  // static @ 0x38
  private   static readonly Unity.VisualScripting.FieldsCloner<fieldsCloner>k__BackingField  // static @ 0x40
  private   static readonly Unity.VisualScripting.FakeSerializationCloner<fakeSerializationCloner>k__BackingField  // static @ 0x48
PROPERTIES:
  cloners  get=0x0B3532E0
  arrayCloner  get=0x0B353290
  dictionaryCloner  get=0x0B353330
  enumerableCloner  get=0x0B353380
  listCloner  get=0x0B353470
  animationCurveCloner  get=0x0B353240
  gradientCloner  get=0x0B353420
  fakeSerializationCloner  get=0x0B3533D0
METHODS:
  RVA=0x045BF5F0  token=0x6000034  System.Void .cctor()
  RVA=0x039E8440  token=0x600003D  System.Object Clone(System.Object original, Unity.VisualScripting.ICloner fallbackCloner, System.Boolean tryPreserveInstances)
  RVA=0x039E83B0  token=0x600003E  System.Object CloneViaFakeSerialization(System.Object original)
  RVA=-1  // generic def  token=0x600003F  T CloneViaFakeSerialization(T original)
  RVA=0x039E8530  token=0x6000040  System.Object Clone(Unity.VisualScripting.CloningContext context, System.Object original)
  RVA=0x039E8870  token=0x6000041  System.Void CloneInto(Unity.VisualScripting.CloningContext context, System.Object& clone, System.Object original)
  RVA=0x039E8B80  token=0x6000042  Unity.VisualScripting.ICloner GetCloner(System.Object original, System.Type type)
  RVA=0x039E8AE0  token=0x6000043  Unity.VisualScripting.ICloner GetCloner(System.Object original, System.Type type, Unity.VisualScripting.ICloner fallbackCloner)
  RVA=0x039E9F30  token=0x6000044  System.Boolean Skippable(System.Type type)
END_CLASS

CLASS: Unity.VisualScripting.CloningContext
TYPE:  sealed class
TOKEN: 0x200000F
SIZE:  0x28
IMPLEMENTS: Unity.VisualScripting.IPoolable System.IDisposable
FIELDS:
  private   readonly System.Collections.Generic.Dictionary<System.Object,System.Object><clonings>k__BackingField  // 0x10
  private           Unity.VisualScripting.ICloner   <fallbackCloner>k__BackingField  // 0x18
  private           System.Boolean                  <tryPreserveInstances>k__BackingField  // 0x20
  private           System.Boolean                  disposed  // 0x21
PROPERTIES:
  clonings  get=0x02B2ECC0
  fallbackCloner  get=0x0385B100  set=0x05392C40
  tryPreserveInstances  get=0x04D866B0  set=0x04D866D0
METHODS:
  RVA=0x04DBA710  token=0x600004C  System.Void Unity.VisualScripting.IPoolable.New()
  RVA=0x03B7DA20  token=0x600004D  System.Void Unity.VisualScripting.IPoolable.Free()
  RVA=0x04CF4DD0  token=0x600004E  System.Void Dispose()
  RVA=0x039E85A0  token=0x600004F  Unity.VisualScripting.CloningContext New(Unity.VisualScripting.ICloner fallbackCloner, System.Boolean tryPreserveInstances)
  RVA=0x039E8130  token=0x6000050  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.ICloner
TYPE:  interface
TOKEN: 0x2000011
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000054  System.Boolean Handles(System.Type type)
  RVA=-1  // abstract  token=0x6000055  System.Object ConstructClone(System.Type type, System.Object original)
  RVA=-1  // abstract  token=0x6000056  System.Void BeforeClone(System.Type type, System.Object original)
  RVA=-1  // abstract  token=0x6000057  System.Void FillClone(System.Type type, System.Object& clone, System.Object original, Unity.VisualScripting.CloningContext context)
  RVA=-1  // abstract  token=0x6000058  System.Void AfterClone(System.Type type, System.Object clone)
END_CLASS

CLASS: Unity.VisualScripting.ISpecifiesCloner
TYPE:  interface
TOKEN: 0x2000012
FIELDS:
PROPERTIES:
  cloner  get=-1  // abstract
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.AotDictionary
TYPE:  sealed class
TOKEN: 0x2000013
SIZE:  0x48
EXTENDS: System.Collections.Specialized.OrderedDictionary
FIELDS:
METHODS:
  RVA=0x0AFF6D1C  token=0x600005A  System.Void .ctor()
  RVA=0x0B352598  token=0x600005B  System.Void AotStubs()
END_CLASS

CLASS: Unity.VisualScripting.AotList
TYPE:  sealed class
TOKEN: 0x2000014
SIZE:  0x28
EXTENDS: System.Collections.ArrayList
FIELDS:
METHODS:
  RVA=0x0AF96728  token=0x600005C  System.Void .ctor()
  RVA=0x0B352640  token=0x600005D  System.Void AotStubs()
END_CLASS

CLASS: Unity.VisualScripting.GuidCollection`1
TYPE:  class
TOKEN: 0x2000015
EXTENDS: System.Collections.ObjectModel.KeyedCollection`2
IMPLEMENTS: System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600005E  System.Guid GetKeyForItem(T item)
END_CLASS

CLASS: Unity.VisualScripting.IKeyedCollection`2
TYPE:  interface
TOKEN: 0x2000016
IMPLEMENTS: System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
PROPERTIES:
  Item  get=-1  // abstract
  Item  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000061  System.Boolean Contains(TKey key)
END_CLASS

CLASS: Unity.VisualScripting.MergedKeyedCollection`2
TYPE:  class
TOKEN: 0x2000017
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  protected readonly System.Collections.Generic.Dictionary<System.Type,Unity.VisualScripting.IKeyedCollection<TKey,TItem>>collections  // 0x0
  protected readonly System.Collections.Generic.Dictionary<System.Type,Unity.VisualScripting.IKeyedCollection<TKey,TItem>>collectionsLookup  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000062  Unity.VisualScripting.IKeyedCollection<TKey,TItem> GetCollectionForItem(TItem item)
  RVA=-1  // not resolved  token=0x6000063  Unity.VisualScripting.IKeyedCollection<TKey,TItem> GetCollectionForType(System.Type type, System.Boolean throwOnFail)
  RVA=-1  // not resolved  token=0x6000064  System.Void Add(TItem item)
  RVA=-1  // not resolved  token=0x6000065  System.Void Clear()
  RVA=-1  // not resolved  token=0x6000066  System.Boolean Contains(TItem item)
  RVA=-1  // not resolved  token=0x6000067  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000068  System.Collections.Generic.IEnumerator<TItem> System.Collections.Generic.IEnumerable<TItem>.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000069  Unity.VisualScripting.MergedKeyedCollection.Enumerator<TKey,TItem> GetEnumerator()
END_CLASS

CLASS: Unity.VisualScripting.ConnectionCollection`3
TYPE:  class
TOKEN: 0x2000019
EXTENDS: Unity.VisualScripting.ConnectionCollectionBase`4
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000070  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.ConnectionCollectionBase`4
TYPE:  class
TOKEN: 0x200001A
IMPLEMENTS: Unity.VisualScripting.IConnectionCollection`3 System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly System.Collections.Generic.Dictionary<TDestination,System.Collections.Generic.List<TConnection>>byDestination  // 0x0
  private   readonly System.Collections.Generic.Dictionary<TSource,System.Collections.Generic.List<TConnection>>bySource  // 0x0
  protected readonly TCollection                     collection  // 0x0
PROPERTIES:
  Count  get=-1  // not resolved
  IsReadOnly  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000071  System.Void .ctor(TCollection collection)
  RVA=-1  // not resolved  token=0x6000074  System.Collections.Generic.IEnumerator<TConnection> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000075  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000076  System.Collections.Generic.IEnumerable<TConnection> WithSource(TSource source)
  RVA=-1  // not resolved  token=0x6000077  System.Collections.Generic.List<TConnection> WithSourceNoAlloc(TSource source)
  RVA=-1  // not resolved  token=0x6000078  TConnection SingleOrDefaultWithSource(TSource source)
  RVA=-1  // not resolved  token=0x6000079  System.Collections.Generic.IEnumerable<TConnection> WithDestination(TDestination destination)
  RVA=-1  // not resolved  token=0x600007A  System.Collections.Generic.List<TConnection> WithDestinationNoAlloc(TDestination destination)
  RVA=-1  // not resolved  token=0x600007B  TConnection SingleOrDefaultWithDestination(TDestination destination)
  RVA=-1  // not resolved  token=0x600007C  System.Void Add(TConnection item)
  RVA=-1  // not resolved  token=0x600007D  System.Void Clear()
  RVA=-1  // not resolved  token=0x600007E  System.Boolean Contains(TConnection item)
  RVA=-1  // not resolved  token=0x600007F  System.Void CopyTo(TConnection[] array, System.Int32 arrayIndex)
  RVA=-1  // not resolved  token=0x6000080  System.Boolean Remove(TConnection item)
  RVA=-1  // not resolved  token=0x6000081  System.Void BeforeAdd(TConnection item)
  RVA=-1  // not resolved  token=0x6000082  System.Void AfterAdd(TConnection item)
  RVA=-1  // not resolved  token=0x6000083  System.Void BeforeRemove(TConnection item)
  RVA=-1  // not resolved  token=0x6000084  System.Void AfterRemove(TConnection item)
  RVA=-1  // not resolved  token=0x6000085  System.Void AddToDictionaries(TConnection item)
  RVA=-1  // not resolved  token=0x6000086  System.Void RemoveFromDictionaries(TConnection item)
END_CLASS

CLASS: Unity.VisualScripting.GraphConnectionCollection`3
TYPE:  class
TOKEN: 0x200001B
EXTENDS: Unity.VisualScripting.ConnectionCollectionBase`4
IMPLEMENTS: System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.IConnection`2
TYPE:  interface
TOKEN: 0x200001C
FIELDS:
PROPERTIES:
  source  get=-1  // abstract
  destination  get=-1  // abstract
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.IConnectionCollection`3
TYPE:  interface
TOKEN: 0x200001D
IMPLEMENTS: System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.InvalidConnectionException
TYPE:  class
TOKEN: 0x200001E
SIZE:  0x90
EXTENDS: System.Exception
FIELDS:
METHODS:
  RVA=0x0B35BAB4  token=0x6000089  System.Void .ctor()
  RVA=0x0B35BB08  token=0x600008A  System.Void .ctor(System.String message)
END_CLASS

CLASS: Unity.VisualScripting.ValueAttribute
TYPE:  class
TOKEN: 0x200001F
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600008B  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.DisableAnnotationAttribute
TYPE:  class
TOKEN: 0x2000020
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  private           System.Boolean                  <disableIcon>k__BackingField  // 0x10
METHODS:
  RVA=0x015EFCD0  token=0x600008C  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.EditorTimeBinding
TYPE:  static class
TOKEN: 0x2000021
SIZE:  0x10
FIELDS:
  public    static  System.Func<System.Int32>       frameBinding  // static @ 0x0
  public    static  System.Func<System.Single>      timeBinding  // static @ 0x8
PROPERTIES:
  frame  get=0x0B357228
  time  get=0x0B3572C8
METHODS:
  RVA=0x0B3570E4  token=0x600008F  System.Void .cctor()
END_CLASS

CLASS: Unity.VisualScripting.IncludeInSettingsAttribute
TYPE:  sealed class
TOKEN: 0x2000023
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  private           System.Boolean                  <include>k__BackingField  // 0x10
PROPERTIES:
  include  set=0x04D86130
METHODS:
  RVA=0x04D86130  token=0x6000094  System.Void .ctor(System.Boolean include)
END_CLASS

CLASS: Unity.VisualScripting.InspectableAttribute
TYPE:  sealed class
TOKEN: 0x2000024
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000096  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.InspectorWideAttribute
TYPE:  sealed class
TOKEN: 0x2000025
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  private           System.Boolean                  <toEdge>k__BackingField  // 0x10
PROPERTIES:
  toEdge  set=0x04D86130
METHODS:
  RVA=0x04D86130  token=0x6000097  System.Void .ctor(System.Boolean toEdge)
END_CLASS

CLASS: Unity.VisualScripting.Ensure
TYPE:  static class
TOKEN: 0x2000026
SIZE:  0x10
FIELDS:
  private   static readonly Unity.VisualScripting.EnsureThatinstance  // static @ 0x0
  private   static  System.Boolean                  <IsActive>k__BackingField  // static @ 0x8
PROPERTIES:
  IsActive  get=0x0B357430  set=0x0B357480
METHODS:
  RVA=0x04727780  token=0x600009B  Unity.VisualScripting.EnsureThat That(System.String paramName)
  RVA=0x03FCE6A0  token=0x600009C  System.Void OnRuntimeMethodLoad()
  RVA=0x04D32510  token=0x600009D  System.Void .cctor()
END_CLASS

CLASS: Unity.VisualScripting.EnsureThat
TYPE:  class
TOKEN: 0x2000027
SIZE:  0x18
FIELDS:
  private           System.String                   paramName  // 0x10
METHODS:
  RVA=-1  // generic def  token=0x600009E  System.Void IsNotNull(T value)
  RVA=0x0B357368  token=0x600009F  System.Void IsNotNull(System.String value)
  RVA=-1  // generic def  token=0x60000A0  System.Void IsOfType(T param, System.Type expectedType)
  RVA=0x041E1670  token=0x60000A1  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.ExceptionMessages
TYPE:  static class
TOKEN: 0x2000028
SIZE:  0x10
FIELDS:
  private   static readonly System.String                   <Common_IsNull_Failed>k__BackingField  // static @ 0x0
  private   static readonly System.String                   <Common_IsNotNull_Failed>k__BackingField  // static @ 0x8
  private   static readonly System.String                   <Booleans_IsTrueFailed>k__BackingField  // static @ 0x10
  private   static readonly System.String                   <Booleans_IsFalseFailed>k__BackingField  // static @ 0x18
  private   static readonly System.String                   <Collections_Any_Failed>k__BackingField  // static @ 0x20
  private   static readonly System.String                   <Collections_ContainsKey_Failed>k__BackingField  // static @ 0x28
  private   static readonly System.String                   <Collections_HasItemsFailed>k__BackingField  // static @ 0x30
  private   static readonly System.String                   <Collections_HasNoNullItemFailed>k__BackingField  // static @ 0x38
  private   static readonly System.String                   <Collections_SizeIs_Failed>k__BackingField  // static @ 0x40
  private   static readonly System.String                   <Comp_Is_Failed>k__BackingField  // static @ 0x48
  private   static readonly System.String                   <Comp_IsNot_Failed>k__BackingField  // static @ 0x50
  private   static readonly System.String                   <Comp_IsNotLt>k__BackingField  // static @ 0x58
  private   static readonly System.String                   <Comp_IsNotLte>k__BackingField  // static @ 0x60
  private   static readonly System.String                   <Comp_IsNotGt>k__BackingField  // static @ 0x68
  private   static readonly System.String                   <Comp_IsNotGte>k__BackingField  // static @ 0x70
  private   static readonly System.String                   <Comp_IsNotInRange_ToLow>k__BackingField  // static @ 0x78
  private   static readonly System.String                   <Comp_IsNotInRange_ToHigh>k__BackingField  // static @ 0x80
  private   static readonly System.String                   <Guids_IsNotEmpty_Failed>k__BackingField  // static @ 0x88
  private   static readonly System.String                   <Strings_IsEqualTo_Failed>k__BackingField  // static @ 0x90
  private   static readonly System.String                   <Strings_IsNotEqualTo_Failed>k__BackingField  // static @ 0x98
  private   static readonly System.String                   <Strings_SizeIs_Failed>k__BackingField  // static @ 0xa0
  private   static readonly System.String                   <Strings_IsNotNullOrWhiteSpace_Failed>k__BackingField  // static @ 0xa8
  private   static readonly System.String                   <Strings_IsNotNullOrEmpty_Failed>k__BackingField  // static @ 0xb0
  private   static readonly System.String                   <Strings_HasLengthBetween_Failed_ToShort>k__BackingField  // static @ 0xb8
  private   static readonly System.String                   <Strings_HasLengthBetween_Failed_ToLong>k__BackingField  // static @ 0xc0
  private   static readonly System.String                   <Strings_Matches_Failed>k__BackingField  // static @ 0xc8
  private   static readonly System.String                   <Strings_IsNotEmpty_Failed>k__BackingField  // static @ 0xd0
  private   static readonly System.String                   <Strings_IsGuid_Failed>k__BackingField  // static @ 0xd8
  private   static readonly System.String                   <Types_IsOfType_Failed>k__BackingField  // static @ 0xe0
  private   static readonly System.String                   <Reflection_HasAttribute_Failed>k__BackingField  // static @ 0xe8
  private   static readonly System.String                   <Reflection_HasConstructor_Failed>k__BackingField  // static @ 0xf0
  private   static readonly System.String                   <Reflection_HasPublicConstructor_Failed>k__BackingField  // static @ 0xf8
  private   static readonly System.String                   <ValueTypes_IsNotDefault_Failed>k__BackingField  // static @ 0x100
PROPERTIES:
  Common_IsNotNull_Failed  get=0x0B3581F8
  Types_IsOfType_Failed  get=0x0B358248
METHODS:
  RVA=0x0B3579CC  token=0x60000A4  System.Void .cctor()
END_CLASS

CLASS: Unity.VisualScripting.XString
TYPE:  static class
TOKEN: 0x2000029
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B36C530  token=0x60000A5  System.String Inject(System.String format, System.String[] formattingArgs)
END_CLASS

CLASS: Unity.VisualScripting.EmptyEventArgs
TYPE:  sealed struct
TOKEN: 0x200002B
SIZE:  0x11
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.InvalidConversionException
TYPE:  class
TOKEN: 0x200002C
SIZE:  0x90
EXTENDS: System.InvalidCastException
FIELDS:
METHODS:
  RVA=0x0B35BB64  token=0x60000A9  System.Void .ctor()
  RVA=0x0A843454  token=0x60000AA  System.Void .ctor(System.String message)
  RVA=0x0B35BB6C  token=0x60000AB  System.Void .ctor(System.String message, System.Exception innerException)
END_CLASS

CLASS: Unity.VisualScripting.InvalidImplementationException
TYPE:  class
TOKEN: 0x200002D
SIZE:  0x90
EXTENDS: System.Exception
FIELDS:
METHODS:
  RVA=0x0B35BB74  token=0x60000AC  System.Void .ctor(System.String message)
END_CLASS

CLASS: Unity.VisualScripting.UnexpectedEnumValueException`1
TYPE:  class
TOKEN: 0x200002E
EXTENDS: System.Exception
FIELDS:
  private           T                               <Value>k__BackingField  // 0x0
PROPERTIES:
  Value  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60000AD  System.Void .ctor(T value)
END_CLASS

CLASS: Unity.VisualScripting.Graph
TYPE:  abstract class
TOKEN: 0x200002F
SIZE:  0x20
IMPLEMENTS: Unity.VisualScripting.IGraph
FIELDS:
  private           System.Collections.Generic.List<Unity.VisualScripting.IGraphElement>_elements  // 0x10
  private   readonly Unity.VisualScripting.MergedGraphElementCollection<elements>k__BackingField  // 0x18
PROPERTIES:
  elements  get=0x0385B100
  deserializationDependencies  get=0x0B35B574
METHODS:
  RVA=0x0B35B508  token=0x60000B1  System.Void OnBeforeSerialize()
  RVA=0x0B35B4BC  token=0x60000B2  System.Void OnAfterDeserialize()
  RVA=0x0B35AFD4  token=0x60000B3  System.Void OnAfterDependenciesDeserialized()
  RVA=0x0B35AE88  token=0x60000B4  System.Void Dispose()
END_CLASS

CLASS: Unity.VisualScripting.GraphElement`1
TYPE:  abstract class
TOKEN: 0x2000031
IMPLEMENTS: Unity.VisualScripting.IGraphElement System.IDisposable Unity.VisualScripting.IIdentifiable
FIELDS:
  private           System.Guid                     <guid>k__BackingField  // 0x0
  private           TGraph                          <graph>k__BackingField  // 0x0
PROPERTIES:
  guid  get=-1  // not resolved
  dependencyOrder  get=-1  // not resolved
  graph  get=-1  // not resolved
  deserializationDependencies  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60000BA  System.Void Dispose()
  RVA=-1  // not resolved  token=0x60000BC  System.Boolean HandleDependencies()
  RVA=-1  // not resolved  token=0x60000BF  System.String ToString()
  RVA=-1  // not resolved  token=0x60000C0  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.GraphElementCollection`1
TYPE:  sealed class
TOKEN: 0x2000032
EXTENDS: Unity.VisualScripting.GuidCollection`1
IMPLEMENTS: Unity.VisualScripting.IKeyedCollection`2 System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60000C1  TElement Unity.VisualScripting.IKeyedCollection<System.Guid,TElement>.get_Item(System.Guid key)
  RVA=-1  // not resolved  token=0x60000C2  System.Boolean Unity.VisualScripting.IKeyedCollection<System.Guid,TElement>.Contains(System.Guid key)
END_CLASS

CLASS: Unity.VisualScripting.GraphPointer
TYPE:  abstract class
TOKEN: 0x2000033
SIZE:  0x48
FIELDS:
  private           Unity.VisualScripting.IGraphRoot<root>k__BackingField  // 0x10
  private           UnityEngine.GameObject          <gameObject>k__BackingField  // 0x18
  protected readonly System.Collections.Generic.List<Unity.VisualScripting.IGraphParent>parentStack  // 0x20
  protected readonly System.Collections.Generic.List<Unity.VisualScripting.IGraphParentElement>parentElementStack  // 0x28
  protected readonly System.Collections.Generic.List<Unity.VisualScripting.IGraph>graphStack  // 0x30
  protected readonly System.Collections.Generic.List<Unity.VisualScripting.IGraphData>dataStack  // 0x38
  protected readonly System.Collections.Generic.List<Unity.VisualScripting.IGraphDebugData>debugDataStack  // 0x40
PROPERTIES:
  root  get=0x02B2ECC0  set=0x053908C0
  rootObject  get=0x0B359E88
  gameObject  get=0x0385B100  set=0x05392C40
  self  get=0x0385B100
  serializedObject  get=0x0B359EC4
  depth  get=0x0B359A0C
  isRoot  get=0x0B359A7C
  isChild  get=0x0B359A64
  parent  get=0x0B359DE4
  parentElement  get=0x0B359D80
  rootGraph  get=0x0B359E40
  hasDebugData  get=0x0B359A4C
  _debugData  get=0x0B359990
  debugData  get=0x0B3599EC
  isValid  get=0x0B359A94
METHODS:
  RVA=0x0B3597EC  token=0x60000C3  System.Void .ctor()
  RVA=-1  // abstract  token=0x60000C4  Unity.VisualScripting.GraphReference AsReference()
  RVA=0x0B358AB4  token=0x60000C5  System.Void CopyFrom(Unity.VisualScripting.GraphPointer other)
  RVA=0x0B3592D4  token=0x60000D0  System.Void EnsureChild()
  RVA=0x0B359350  token=0x60000D5  System.Void EnsureDebugDataAvailable()
  RVA=-1  // generic def  token=0x60000D8  T GetElementDebugData(Unity.VisualScripting.IGraphElementWithDebugData element)
  RVA=0x0B3593CC  token=0x60000D9  System.Void ExitParentElement()
  RVA=0x0B359574  token=0x60000DB  System.Boolean InstanceEquals(Unity.VisualScripting.GraphPointer other)
  RVA=0x0B3591EC  token=0x60000DC  System.Boolean DefinitionEquals(Unity.VisualScripting.GraphPointer other)
  RVA=0x0B358990  token=0x60000DD  System.Int32 ComputeHashCode()
  RVA=0x0B359674  token=0x60000DE  System.String ToString()
END_CLASS

CLASS: Unity.VisualScripting.GraphPointerException
TYPE:  sealed class
TOKEN: 0x2000034
SIZE:  0x98
EXTENDS: System.Exception
FIELDS:
  private   readonly Unity.VisualScripting.GraphPointer<pointer>k__BackingField  // 0x90
METHODS:
  RVA=0x0B3588CC  token=0x60000DF  System.Void .ctor(System.String message, Unity.VisualScripting.GraphPointer pointer)
END_CLASS

CLASS: Unity.VisualScripting.GraphReference
TYPE:  sealed class
TOKEN: 0x2000035
SIZE:  0x50
EXTENDS: Unity.VisualScripting.GraphPointer
FIELDS:
  private           System.Int32                    hashCode  // 0x48
  private   static readonly System.Collections.Generic.Dictionary<System.Int32,System.Collections.Generic.List<Unity.VisualScripting.GraphReference>>internPool  // static @ 0x0
METHODS:
  RVA=0x0B35AAF8  token=0x60000E0  System.Void .cctor()
  RVA=0x0B35ABD0  token=0x60000E1  System.Void .ctor()
  RVA=0x0B35A9D8  token=0x60000E2  Unity.VisualScripting.GraphReference New(Unity.VisualScripting.GraphPointer model)
  RVA=0x0B35A014  token=0x60000E3  System.Void CopyFrom(Unity.VisualScripting.GraphPointer other)
  RVA=0x0B359FC8  token=0x60000E4  Unity.VisualScripting.GraphReference Clone()
  RVA=0x0464E630  token=0x60000E5  Unity.VisualScripting.GraphReference AsReference()
  RVA=0x0B35AAF0  token=0x60000E6  Unity.VisualScripting.GraphStack ToStackPooled()
  RVA=0x0B35A07C  token=0x60000E7  System.Boolean Equals(System.Object obj)
  RVA=0x0B35A6E8  token=0x60000E8  System.Void Hash()
  RVA=0x04D863D0  token=0x60000E9  System.Int32 GetHashCode()
  RVA=0x0B35AA40  token=0x60000EA  Unity.VisualScripting.GraphReference ParentReference(System.Boolean ensureValid)
  RVA=0x0B35A704  token=0x60000EB  Unity.VisualScripting.GraphReference Intern(Unity.VisualScripting.GraphPointer pointer)
  RVA=0x0B35A0D8  token=0x60000EC  System.Void FreeInvalidInterns()
END_CLASS

CLASS: Unity.VisualScripting.GraphsExceptionUtility
TYPE:  static class
TOKEN: 0x2000036
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B35BA0C  token=0x60000ED  System.Void SetException(Unity.VisualScripting.IGraphElementWithDebugData element, Unity.VisualScripting.GraphPointer pointer, System.Exception ex)
  RVA=0x0B35B67C  token=0x60000EE  System.Void HandleException(Unity.VisualScripting.IGraphElementWithDebugData element, Unity.VisualScripting.GraphPointer pointer, System.Exception ex)
  RVA=0x0B35B848  token=0x60000EF  System.Boolean HandledIn(System.Exception ex, Unity.VisualScripting.GraphReference reference)
END_CLASS

CLASS: Unity.VisualScripting.GraphStack
TYPE:  sealed class
TOKEN: 0x2000037
SIZE:  0x48
EXTENDS: Unity.VisualScripting.GraphPointer
IMPLEMENTS: Unity.VisualScripting.IPoolable System.IDisposable
FIELDS:
METHODS:
  RVA=0x0B35ABD0  token=0x60000F0  System.Void .ctor()
  RVA=0x0B35AC34  token=0x60000F1  Unity.VisualScripting.GraphStack New(Unity.VisualScripting.GraphPointer model)
  RVA=0x0B35ABE0  token=0x60000F2  System.Void Dispose()
  RVA=0x041E1670  token=0x60000F3  System.Void Unity.VisualScripting.IPoolable.New()
  RVA=0x0B35ADB0  token=0x60000F4  System.Void Unity.VisualScripting.IPoolable.Free()
  RVA=0x0B35ABD8  token=0x60000F5  Unity.VisualScripting.GraphReference AsReference()
  RVA=0x0B35AD64  token=0x60000F6  Unity.VisualScripting.GraphReference ToReference()
END_CLASS

CLASS: Unity.VisualScripting.IGraph
TYPE:  interface
TOKEN: 0x2000039
IMPLEMENTS: System.IDisposable Unity.VisualScripting.ISerializationDepender UnityEngine.ISerializationCallbackReceiver
FIELDS:
PROPERTIES:
  elements  get=-1  // abstract
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.IGraphData
TYPE:  interface
TOKEN: 0x200003A
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.IGraphDebugData
TYPE:  interface
TOKEN: 0x200003B
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60000FB  Unity.VisualScripting.IGraphElementDebugData GetOrCreateElementData(Unity.VisualScripting.IGraphElementWithDebugData element)
END_CLASS

CLASS: Unity.VisualScripting.IGraphElement
TYPE:  interface
TOKEN: 0x200003C
IMPLEMENTS: System.IDisposable Unity.VisualScripting.IIdentifiable
FIELDS:
PROPERTIES:
  dependencyOrder  get=-1  // abstract
  guid  get=-1  // abstract
  deserializationDependencies  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x60000FC  System.Boolean HandleDependencies()
END_CLASS

CLASS: Unity.VisualScripting.IGraphElementDebugData
TYPE:  interface
TOKEN: 0x200003D
FIELDS:
PROPERTIES:
  runtimeException  set=-1  // abstract
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.IGraphElementWithDebugData
TYPE:  interface
TOKEN: 0x200003E
IMPLEMENTS: Unity.VisualScripting.IGraphElement System.IDisposable Unity.VisualScripting.IIdentifiable
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.IGraphParent
TYPE:  interface
TOKEN: 0x200003F
FIELDS:
PROPERTIES:
  childGraph  get=-1  // abstract
  isSerializationRoot  get=-1  // abstract
  serializedObject  get=-1  // abstract
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.IGraphParentElement
TYPE:  interface
TOKEN: 0x2000040
IMPLEMENTS: Unity.VisualScripting.IGraphElement System.IDisposable Unity.VisualScripting.IIdentifiable Unity.VisualScripting.IGraphParent
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.IGraphRoot
TYPE:  interface
TOKEN: 0x2000041
IMPLEMENTS: Unity.VisualScripting.IGraphParent
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.MergedGraphElementCollection
TYPE:  sealed class
TOKEN: 0x2000042
SIZE:  0x20
EXTENDS: Unity.VisualScripting.MergedKeyedCollection`2
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.PlatformUtility
TYPE:  static class
TOKEN: 0x2000043
SIZE:  0x10
FIELDS:
  public    static readonly System.Boolean                  supportsJit  // static @ 0x0
METHODS:
  RVA=0x04D79D90  token=0x6000104  System.Void .cctor()
  RVA=0x012081B0  token=0x6000105  System.Boolean CheckJitSupport()
END_CLASS

CLASS: Unity.VisualScripting.GenericPool`1
TYPE:  static class
TOKEN: 0x2000044
FIELDS:
  private   static readonly System.Object                   lock  // static @ 0x0
  private   static readonly System.Collections.Generic.Stack<T>free  // static @ 0x0
  private   static readonly System.Collections.Generic.HashSet<T>busy  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000106  T New(System.Func<T> constructor)
  RVA=-1  // not resolved  token=0x6000107  System.Void Free(T item)
  RVA=-1  // not resolved  token=0x6000108  System.Void .cctor()
END_CLASS

CLASS: Unity.VisualScripting.IPoolable
TYPE:  interface
TOKEN: 0x2000045
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000109  System.Void New()
  RVA=-1  // abstract  token=0x600010A  System.Void Free()
END_CLASS

CLASS: Unity.VisualScripting.ListPool`1
TYPE:  static class
TOKEN: 0x2000046
FIELDS:
  private   static readonly System.Object                   lock  // static @ 0x0
  private   static readonly System.Collections.Generic.Stack<System.Collections.Generic.List<T>>free  // static @ 0x0
  private   static readonly System.Collections.Generic.HashSet<System.Collections.Generic.List<T>>busy  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600010B  System.Collections.Generic.List<T> New()
  RVA=-1  // not resolved  token=0x600010C  System.Void Free(System.Collections.Generic.List<T> list)
  RVA=-1  // not resolved  token=0x600010D  System.Void .cctor()
END_CLASS

CLASS: Unity.VisualScripting.XListPool
TYPE:  static class
TOKEN: 0x2000047
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x600010E  System.Void Free(System.Collections.Generic.List<T> list)
END_CLASS

CLASS: Unity.VisualScripting.ProfiledSegment
TYPE:  class
TOKEN: 0x2000048
SIZE:  0x30
FIELDS:
  private           System.String                   <name>k__BackingField  // 0x10
  private           System.Diagnostics.Stopwatch    <stopwatch>k__BackingField  // 0x18
  private           Unity.VisualScripting.ProfiledSegment<parent>k__BackingField  // 0x20
  private           Unity.VisualScripting.ProfiledSegmentCollection<children>k__BackingField  // 0x28
PROPERTIES:
  name  get=0x02B2ECC0  set=0x053908C0
  stopwatch  set=0x05392C40
  parent  set=0x05390F40
  children  set=0x02FDB880
METHODS:
  RVA=0x0B360B80  token=0x600010F  System.Void .ctor(Unity.VisualScripting.ProfiledSegment parent, System.String name)
END_CLASS

CLASS: Unity.VisualScripting.ProfiledSegmentCollection
TYPE:  class
TOKEN: 0x2000049
SIZE:  0x30
EXTENDS: System.Collections.ObjectModel.KeyedCollection`2
FIELDS:
METHODS:
  RVA=0x06C08920  token=0x6000115  System.String GetKeyForItem(Unity.VisualScripting.ProfiledSegment item)
  RVA=0x0B360B44  token=0x6000116  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.ProfilingScope
TYPE:  sealed struct
TOKEN: 0x200004A
SIZE:  0x11
IMPLEMENTS: System.IDisposable
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000117  System.Void .ctor(System.String name)
  RVA=0x041E1670  token=0x6000118  System.Void Dispose()
END_CLASS

CLASS: Unity.VisualScripting.ProfilingUtility
TYPE:  static class
TOKEN: 0x200004B
SIZE:  0x10
FIELDS:
  private   static readonly System.Object                   lock  // static @ 0x0
  private   static  Unity.VisualScripting.ProfiledSegment<rootSegment>k__BackingField  // static @ 0x8
  private   static  Unity.VisualScripting.ProfiledSegment<currentSegment>k__BackingField  // static @ 0x10
PROPERTIES:
  rootSegment  set=0x0B360DF0
  currentSegment  set=0x0B360D90
METHODS:
  RVA=0x0B360C38  token=0x6000119  System.Void .cctor()
  RVA=0x012081B0  token=0x600011C  Unity.VisualScripting.ProfilingScope SampleBlock(System.String name)
END_CLASS

CLASS: Unity.VisualScripting.ActionDirection
TYPE:  sealed struct
TOKEN: 0x200004C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Unity.VisualScripting.ActionDirectionAny  // const
  public    static  Unity.VisualScripting.ActionDirectionGet  // const
  public    static  Unity.VisualScripting.ActionDirectionSet  // const
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.AttributeUtility
TYPE:  static class
TOKEN: 0x200004D
SIZE:  0x10
FIELDS:
  private   static readonly System.Collections.Generic.Dictionary<System.Object,Unity.VisualScripting.AttributeUtility.AttributeCache>optimizedCaches  // static @ 0x0
METHODS:
  RVA=0x0B352F6C  token=0x600011D  Unity.VisualScripting.AttributeUtility.AttributeCache GetAttributeCache(System.Reflection.MemberInfo element)
  RVA=-1  // generic def  token=0x600011E  System.Collections.Generic.IEnumerable<T> GetAttributeOfEnumMember(System.Enum enumVal)
  RVA=-1  // generic def  token=0x600011F  System.Boolean HasAttribute(System.Reflection.MemberInfo element, System.Boolean inherit)
  RVA=-1  // generic def  token=0x6000120  TAttribute GetAttribute(System.Reflection.MemberInfo element, System.Boolean inherit)
  RVA=0x0B3531B4  token=0x6000121  System.Void .cctor()
END_CLASS

CLASS: Unity.VisualScripting.ConversionUtility
TYPE:  static class
TOKEN: 0x200004F
SIZE:  0x10
FIELDS:
  private   static readonly System.Collections.Generic.Dictionary<Unity.VisualScripting.ConversionUtility.ConversionQuery,Unity.VisualScripting.ConversionUtility.ConversionType>conversionTypesCache  // static @ 0x0
  private   static readonly System.Collections.Generic.Dictionary<Unity.VisualScripting.ConversionUtility.ConversionQuery,System.Reflection.MethodInfo[]>userConversionMethodsCache  // static @ 0x8
  private   static readonly System.Collections.Generic.Dictionary<System.Type,System.Collections.Generic.HashSet<System.Type>>implicitNumericConversions  // static @ 0x10
  private   static readonly System.Collections.Generic.Dictionary<System.Type,System.Collections.Generic.HashSet<System.Type>>explicitNumericConversions  // static @ 0x18
METHODS:
  RVA=0x0B35517C  token=0x600012C  System.Boolean RespectsIdentity(System.Type source, System.Type destination)
  RVA=0x0AD1AECC  token=0x600012D  System.Boolean IsUpcast(System.Type source, System.Type destination)
  RVA=0x0AC76A90  token=0x600012E  System.Boolean IsDowncast(System.Type source, System.Type destination)
  RVA=0x0B354DD0  token=0x600012F  System.Boolean HasImplicitNumericConversion(System.Type source, System.Type destination)
  RVA=0x0B354CEC  token=0x6000130  System.Boolean HasExplicitNumericConversion(System.Type source, System.Type destination)
  RVA=0x0B354EB4  token=0x6000131  System.Boolean HasNumericConversion(System.Type source, System.Type destination)
  RVA=0x0B354190  token=0x6000132  System.Collections.Generic.IEnumerable<System.Reflection.MethodInfo> FindUserDefinedConversionMethods(Unity.VisualScripting.ConversionUtility.ConversionQuery query)
  RVA=0x0B35462C  token=0x6000133  System.Reflection.MethodInfo[] GetUserDefinedConversionMethods(System.Type source, System.Type destination)
  RVA=0x0B3547AC  token=0x6000134  Unity.VisualScripting.ConversionUtility.ConversionType GetUserDefinedConversionType(System.Type source, System.Type destination)
  RVA=0x0B354AF4  token=0x6000135  System.Boolean HasEnumerableToArrayConversion(System.Type source, System.Type destination)
  RVA=0x0B354BD8  token=0x6000136  System.Boolean HasEnumerableToListConversion(System.Type source, System.Type destination)
  RVA=0x0B354F28  token=0x6000137  System.Boolean HasUnityHierarchyConversion(System.Type source, System.Type destination)
  RVA=0x0B35510C  token=0x6000138  System.Boolean IsValidConversion(Unity.VisualScripting.ConversionUtility.ConversionType conversionType, System.Boolean guaranteed)
  RVA=0x0B3537C8  token=0x6000139  System.Boolean CanConvert(System.Object value, System.Type type, System.Boolean guaranteed)
  RVA=0x0B35375C  token=0x600013A  System.Boolean CanConvert(System.Type source, System.Type destination, System.Boolean guaranteed)
  RVA=0x0B353834  token=0x600013B  System.Object Convert(System.Object value, System.Type type)
  RVA=0x0B3550AC  token=0x600013C  System.Boolean IsConvertibleTo(System.Type source, System.Type destination, System.Boolean guaranteed)
  RVA=0x0B35504C  token=0x600013D  System.Boolean IsConvertibleTo(System.Object source, System.Type type, System.Boolean guaranteed)
  RVA=0x0B354500  token=0x600013E  Unity.VisualScripting.ConversionUtility.ConversionType GetRequiredConversion(System.Type source, System.Type destination)
  RVA=0x0B353C8C  token=0x600013F  Unity.VisualScripting.ConversionUtility.ConversionType DetermineConversionType(Unity.VisualScripting.ConversionUtility.ConversionQuery query)
  RVA=0x0B354438  token=0x6000140  Unity.VisualScripting.ConversionUtility.ConversionType GetRequiredConversion(System.Object value, System.Type type)
  RVA=0x0B355120  token=0x6000141  System.Object NumericConversion(System.Object value, System.Type type)
  RVA=0x0B3553C0  token=0x6000142  System.Object UserDefinedConversion(Unity.VisualScripting.ConversionUtility.ConversionType conversion, System.Object value, System.Type type)
  RVA=0x0B353ECC  token=0x6000143  System.Object EnumerableToArrayConversion(System.Object value, System.Type arrayType)
  RVA=0x0B354004  token=0x6000144  System.Object EnumerableToListConversion(System.Object value, System.Type listType)
  RVA=0x0B3551D0  token=0x6000145  System.Object UnityHierarchyConversion(System.Object value, System.Type type)
  RVA=0x0B353898  token=0x6000146  System.Object Convert(System.Object value, System.Type type, Unity.VisualScripting.ConversionUtility.ConversionType conversionType)
  RVA=0x0B355574  token=0x6000147  System.Void .cctor()
END_CLASS

CLASS: Unity.VisualScripting.LooseAssemblyName
TYPE:  sealed struct
TOKEN: 0x2000056
SIZE:  0x18
FIELDS:
  public    readonly System.String                   name  // 0x10
METHODS:
  RVA=0x0B35C4DC  token=0x6000158  System.Void .ctor(System.String name)
  RVA=0x0B35C43C  token=0x6000159  System.Boolean Equals(System.Object obj)
  RVA=0x0B35C4A0  token=0x600015A  System.Int32 GetHashCode()
  RVA=0x02B76770  token=0x600015B  System.String ToString()
END_CLASS

CLASS: Unity.VisualScripting.MemberUtility
TYPE:  static class
TOKEN: 0x2000057
SIZE:  0x10
FIELDS:
  private   static readonly System.Lazy<Unity.VisualScripting.ExtensionMethodCache>ExtensionMethodsCache  // static @ 0x0
  private   static readonly System.Lazy<System.Collections.Generic.Dictionary<System.Type,System.Reflection.MethodInfo[]>>InheritedExtensionMethodsCache  // static @ 0x8
  private   static readonly System.Lazy<System.Collections.Generic.HashSet<System.Reflection.MethodInfo>>GenericExtensionMethods  // static @ 0x10
METHODS:
  RVA=0x0B35CCB0  token=0x600015C  System.Void .cctor()
  RVA=0x0B35CC10  token=0x600015D  System.Boolean IsUserDefinedConversion(System.Reflection.MethodInfo method)
  RVA=0x0B35C9F4  token=0x600015E  System.Boolean IsExtension(System.Reflection.MethodInfo methodInfo)
  RVA=0x0B35CA48  token=0x600015F  System.Boolean IsStatic(System.Reflection.PropertyInfo propertyInfo)
  RVA=0x0B35CAB0  token=0x6000160  System.Boolean IsStatic(System.Reflection.MemberInfo memberInfo)
  RVA=0x0B35C9BC  token=0x6000161  System.Boolean CanWrite(System.Reflection.FieldInfo fieldInfo)
END_CLASS

CLASS: Unity.VisualScripting.ExtensionMethodCache
TYPE:  class
TOKEN: 0x2000059
SIZE:  0x18
FIELDS:
  private   readonly System.Reflection.MethodInfo[]  Cache  // 0x10
METHODS:
  RVA=0x0B35829C  token=0x6000167  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.Namespace
TYPE:  sealed class
TOKEN: 0x200005B
SIZE:  0x38
FIELDS:
  private   readonly Unity.VisualScripting.Namespace <Root>k__BackingField  // 0x10
  private   readonly Unity.VisualScripting.Namespace <Parent>k__BackingField  // 0x18
  private   readonly System.String                   <FullName>k__BackingField  // 0x20
  private   readonly System.String                   <Name>k__BackingField  // 0x28
  private   readonly System.Boolean                  <IsRoot>k__BackingField  // 0x30
  private   readonly System.Boolean                  <IsGlobal>k__BackingField  // 0x31
  private   static readonly Unity.VisualScripting.Namespace.Collectioncollection  // static @ 0x0
  private   static readonly Unity.VisualScripting.Namespace <Global>k__BackingField  // static @ 0x8
PROPERTIES:
  FullName  get=0x04D862C0
  Global  get=0x0B35D57C
METHODS:
  RVA=0x0B35D438  token=0x600016D  System.Void .ctor(System.String fullName)
  RVA=0x0B35D348  token=0x600016F  System.Int32 GetHashCode()
  RVA=0x04D862C0  token=0x6000170  System.String ToString()
  RVA=0x0B35D370  token=0x6000171  System.Void .cctor()
  RVA=0x0B35D254  token=0x6000173  Unity.VisualScripting.Namespace FromFullName(System.String fullName)
  RVA=0x0B35D1C8  token=0x6000174  System.Boolean Equals(System.Object obj)
  RVA=0x0B35D5CC  token=0x6000175  Unity.VisualScripting.Namespace op_Implicit(System.String fullName)
  RVA=0x0A7BED8C  token=0x6000176  System.Boolean op_Equality(Unity.VisualScripting.Namespace a, Unity.VisualScripting.Namespace b)
END_CLASS

CLASS: Unity.VisualScripting.Action`5
TYPE:  sealed class
TOKEN: 0x200005D
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x600017C  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x600017D  System.Void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
END_CLASS

CLASS: Unity.VisualScripting.Action`6
TYPE:  sealed class
TOKEN: 0x200005E
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x600017E  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x600017F  System.Void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
END_CLASS

CLASS: Unity.VisualScripting.Func`6
TYPE:  sealed class
TOKEN: 0x200005F
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000180  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000181  TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
END_CLASS

CLASS: Unity.VisualScripting.Func`7
TYPE:  sealed class
TOKEN: 0x2000060
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000182  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000183  TResult Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6)
END_CLASS

CLASS: Unity.VisualScripting.InstanceActionInvokerBase`1
TYPE:  abstract class
TOKEN: 0x2000061
EXTENDS: Unity.VisualScripting.InstanceInvokerBase`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x6000184  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
END_CLASS

CLASS: Unity.VisualScripting.InstanceActionInvoker`1
TYPE:  sealed class
TOKEN: 0x2000062
EXTENDS: Unity.VisualScripting.InstanceActionInvokerBase`1
FIELDS:
  private           System.Action<TTarget>          invoke  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000185  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
  RVA=-1  // not resolved  token=0x6000186  System.Type[] GetParameterTypes()
  RVA=-1  // not resolved  token=0x6000187  System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions)
  RVA=-1  // not resolved  token=0x6000188  System.Void CreateDelegate()
END_CLASS

CLASS: Unity.VisualScripting.InstanceActionInvoker`2
TYPE:  sealed class
TOKEN: 0x2000063
EXTENDS: Unity.VisualScripting.InstanceActionInvokerBase`1
FIELDS:
  private           System.Action<TTarget,TParam0>  invoke  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000189  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
  RVA=-1  // not resolved  token=0x600018A  System.Object Invoke(System.Object target, System.Object arg0)
  RVA=-1  // not resolved  token=0x600018B  System.Object InvokeUnsafe(System.Object target, System.Object arg0)
  RVA=-1  // not resolved  token=0x600018C  System.Type[] GetParameterTypes()
  RVA=-1  // not resolved  token=0x600018D  System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions)
  RVA=-1  // not resolved  token=0x600018E  System.Void CreateDelegate()
END_CLASS

CLASS: Unity.VisualScripting.InstanceActionInvoker`3
TYPE:  sealed class
TOKEN: 0x2000064
EXTENDS: Unity.VisualScripting.InstanceActionInvokerBase`1
FIELDS:
  private           System.Action<TTarget,TParam0,TParam1>invoke  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600018F  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
  RVA=-1  // not resolved  token=0x6000190  System.Type[] GetParameterTypes()
  RVA=-1  // not resolved  token=0x6000191  System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions)
  RVA=-1  // not resolved  token=0x6000192  System.Void CreateDelegate()
END_CLASS

CLASS: Unity.VisualScripting.InstanceActionInvoker`4
TYPE:  sealed class
TOKEN: 0x2000065
EXTENDS: Unity.VisualScripting.InstanceActionInvokerBase`1
FIELDS:
  private           System.Action<TTarget,TParam0,TParam1,TParam2>invoke  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000193  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
  RVA=-1  // not resolved  token=0x6000194  System.Type[] GetParameterTypes()
  RVA=-1  // not resolved  token=0x6000195  System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions)
  RVA=-1  // not resolved  token=0x6000196  System.Void CreateDelegate()
END_CLASS

CLASS: Unity.VisualScripting.InstanceActionInvoker`5
TYPE:  sealed class
TOKEN: 0x2000066
EXTENDS: Unity.VisualScripting.InstanceActionInvokerBase`1
FIELDS:
  private           Unity.VisualScripting.Action<TTarget,TParam0,TParam1,TParam2,TParam3>invoke  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000197  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
  RVA=-1  // not resolved  token=0x6000198  System.Type[] GetParameterTypes()
  RVA=-1  // not resolved  token=0x6000199  System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions)
  RVA=-1  // not resolved  token=0x600019A  System.Void CreateDelegate()
END_CLASS

CLASS: Unity.VisualScripting.InstanceActionInvoker`6
TYPE:  sealed class
TOKEN: 0x2000067
EXTENDS: Unity.VisualScripting.InstanceActionInvokerBase`1
FIELDS:
  private           Unity.VisualScripting.Action<TTarget,TParam0,TParam1,TParam2,TParam3,TParam4>invoke  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600019B  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
  RVA=-1  // not resolved  token=0x600019C  System.Type[] GetParameterTypes()
  RVA=-1  // not resolved  token=0x600019D  System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions)
  RVA=-1  // not resolved  token=0x600019E  System.Void CreateDelegate()
END_CLASS

CLASS: Unity.VisualScripting.InstanceFieldAccessor`2
TYPE:  class
TOKEN: 0x2000068
IMPLEMENTS: Unity.VisualScripting.IOptimizedAccessor
FIELDS:
  private   readonly System.Reflection.FieldInfo     fieldInfo  // 0x0
  private           System.Func<TTarget,TField>     getter  // 0x0
  private           System.Action<TTarget,TField>   setter  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600019F  System.Void .ctor(System.Reflection.FieldInfo fieldInfo)
  RVA=-1  // not resolved  token=0x60001A0  System.Void Compile()
  RVA=-1  // not resolved  token=0x60001A1  System.Object GetValue(System.Object target)
  RVA=-1  // not resolved  token=0x60001A2  System.Object GetValueUnsafe(System.Object target)
  RVA=-1  // not resolved  token=0x60001A3  System.Void SetValue(System.Object target, System.Object value)
  RVA=-1  // not resolved  token=0x60001A4  System.Void SetValueUnsafe(System.Object target, System.Object value)
  RVA=-1  // not resolved  token=0x60001A5  TField <Compile>b__4_0(TTarget instance)
  RVA=-1  // not resolved  token=0x60001A6  System.Void <Compile>b__4_1(TTarget instance, TField value)
END_CLASS

CLASS: Unity.VisualScripting.InstanceFunctionInvokerBase`2
TYPE:  abstract class
TOKEN: 0x2000069
EXTENDS: Unity.VisualScripting.InstanceInvokerBase`1
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60001A7  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
END_CLASS

CLASS: Unity.VisualScripting.InstanceFunctionInvoker`2
TYPE:  sealed class
TOKEN: 0x200006A
EXTENDS: Unity.VisualScripting.InstanceFunctionInvokerBase`2
FIELDS:
  private           System.Func<TTarget,TResult>    invoke  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60001A8  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
  RVA=-1  // not resolved  token=0x60001A9  System.Type[] GetParameterTypes()
  RVA=-1  // not resolved  token=0x60001AA  System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions)
  RVA=-1  // not resolved  token=0x60001AB  System.Void CreateDelegate()
END_CLASS

CLASS: Unity.VisualScripting.InstanceFunctionInvoker`3
TYPE:  sealed class
TOKEN: 0x200006B
EXTENDS: Unity.VisualScripting.InstanceFunctionInvokerBase`2
FIELDS:
  private           System.Func<TTarget,TParam0,TResult>invoke  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60001AC  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
  RVA=-1  // not resolved  token=0x60001AD  System.Object Invoke(System.Object target, System.Object arg0)
  RVA=-1  // not resolved  token=0x60001AE  System.Object InvokeUnsafe(System.Object target, System.Object arg0)
  RVA=-1  // not resolved  token=0x60001AF  System.Type[] GetParameterTypes()
  RVA=-1  // not resolved  token=0x60001B0  System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions)
  RVA=-1  // not resolved  token=0x60001B1  System.Void CreateDelegate()
END_CLASS

CLASS: Unity.VisualScripting.InstanceFunctionInvoker`4
TYPE:  sealed class
TOKEN: 0x200006C
EXTENDS: Unity.VisualScripting.InstanceFunctionInvokerBase`2
FIELDS:
  private           System.Func<TTarget,TParam0,TParam1,TResult>invoke  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60001B2  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
  RVA=-1  // not resolved  token=0x60001B3  System.Type[] GetParameterTypes()
  RVA=-1  // not resolved  token=0x60001B4  System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions)
  RVA=-1  // not resolved  token=0x60001B5  System.Void CreateDelegate()
END_CLASS

CLASS: Unity.VisualScripting.InstanceFunctionInvoker`5
TYPE:  sealed class
TOKEN: 0x200006D
EXTENDS: Unity.VisualScripting.InstanceFunctionInvokerBase`2
FIELDS:
  private           System.Func<TTarget,TParam0,TParam1,TParam2,TResult>invoke  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60001B6  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
  RVA=-1  // not resolved  token=0x60001B7  System.Type[] GetParameterTypes()
  RVA=-1  // not resolved  token=0x60001B8  System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions)
  RVA=-1  // not resolved  token=0x60001B9  System.Void CreateDelegate()
END_CLASS

CLASS: Unity.VisualScripting.InstanceFunctionInvoker`6
TYPE:  sealed class
TOKEN: 0x200006E
EXTENDS: Unity.VisualScripting.InstanceFunctionInvokerBase`2
FIELDS:
  private           Unity.VisualScripting.Func<TTarget,TParam0,TParam1,TParam2,TParam3,TResult>invoke  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60001BA  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
  RVA=-1  // not resolved  token=0x60001BB  System.Type[] GetParameterTypes()
  RVA=-1  // not resolved  token=0x60001BC  System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions)
  RVA=-1  // not resolved  token=0x60001BD  System.Void CreateDelegate()
END_CLASS

CLASS: Unity.VisualScripting.InstanceFunctionInvoker`7
TYPE:  sealed class
TOKEN: 0x200006F
EXTENDS: Unity.VisualScripting.InstanceFunctionInvokerBase`2
FIELDS:
  private           Unity.VisualScripting.Func<TTarget,TParam0,TParam1,TParam2,TParam3,TParam4,TResult>invoke  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60001BE  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
  RVA=-1  // not resolved  token=0x60001BF  System.Type[] GetParameterTypes()
  RVA=-1  // not resolved  token=0x60001C0  System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions)
  RVA=-1  // not resolved  token=0x60001C1  System.Void CreateDelegate()
END_CLASS

CLASS: Unity.VisualScripting.InstanceInvokerBase`1
TYPE:  abstract class
TOKEN: 0x2000070
EXTENDS: Unity.VisualScripting.InvokerBase
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60001C2  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
  RVA=-1  // not resolved  token=0x60001C3  System.Void CompileExpression()
  RVA=-1  // abstract  token=0x60001C4  System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions)
  RVA=-1  // not resolved  token=0x60001C5  System.Void VerifyTarget(System.Object target)
END_CLASS

CLASS: Unity.VisualScripting.InstancePropertyAccessor`2
TYPE:  class
TOKEN: 0x2000071
IMPLEMENTS: Unity.VisualScripting.IOptimizedAccessor
FIELDS:
  private   readonly System.Reflection.PropertyInfo  propertyInfo  // 0x0
  private           System.Func<TTarget,TProperty>  getter  // 0x0
  private           System.Action<TTarget,TProperty>setter  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60001C6  System.Void .ctor(System.Reflection.PropertyInfo propertyInfo)
  RVA=-1  // not resolved  token=0x60001C7  System.Void Compile()
  RVA=-1  // not resolved  token=0x60001C8  System.Object GetValue(System.Object target)
  RVA=-1  // not resolved  token=0x60001C9  System.Object GetValueUnsafe(System.Object target)
  RVA=-1  // not resolved  token=0x60001CA  System.Void SetValue(System.Object target, System.Object value)
  RVA=-1  // not resolved  token=0x60001CB  System.Void SetValueUnsafe(System.Object target, System.Object value)
END_CLASS

CLASS: Unity.VisualScripting.InvokerBase
TYPE:  abstract class
TOKEN: 0x2000072
SIZE:  0x20
IMPLEMENTS: Unity.VisualScripting.IOptimizedInvoker
FIELDS:
  protected readonly System.Type                     targetType  // 0x10
  protected readonly System.Reflection.MethodInfo    methodInfo  // 0x18
METHODS:
  RVA=0x0B35BECC  token=0x60001CC  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
  RVA=-1  // generic def  token=0x60001CD  System.Void VerifyArgument(System.Reflection.MethodInfo methodInfo, System.Int32 argIndex, System.Object arg)
  RVA=0x0B35BBD0  token=0x60001CE  System.Void Compile()
  RVA=0x0B35BC20  token=0x60001CF  System.Linq.Expressions.ParameterExpression[] GetParameterExpressions()
  RVA=-1  // abstract  token=0x60001D0  System.Type[] GetParameterTypes()
  RVA=0x0B35BE80  token=0x60001D1  System.Object Invoke(System.Object target, System.Object arg0)
  RVA=-1  // abstract  token=0x60001D2  System.Void CompileExpression()
  RVA=-1  // abstract  token=0x60001D3  System.Void CreateDelegate()
  RVA=-1  // abstract  token=0x60001D4  System.Void VerifyTarget(System.Object target)
END_CLASS

CLASS: Unity.VisualScripting.IOptimizedAccessor
TYPE:  interface
TOKEN: 0x2000073
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60001D5  System.Void Compile()
  RVA=-1  // abstract  token=0x60001D6  System.Object GetValue(System.Object target)
  RVA=-1  // abstract  token=0x60001D7  System.Void SetValue(System.Object target, System.Object value)
END_CLASS

CLASS: Unity.VisualScripting.IOptimizedInvoker
TYPE:  interface
TOKEN: 0x2000074
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x60001D8  System.Void Compile()
  RVA=-1  // abstract  token=0x60001D9  System.Object Invoke(System.Object target, System.Object arg0)
END_CLASS

CLASS: Unity.VisualScripting.OptimizedReflection
TYPE:  static class
TOKEN: 0x2000075
SIZE:  0x10
FIELDS:
  private   static readonly System.Collections.Generic.Dictionary<System.Reflection.FieldInfo,Unity.VisualScripting.IOptimizedAccessor>fieldAccessors  // static @ 0x0
  private   static readonly System.Collections.Generic.Dictionary<System.Reflection.PropertyInfo,Unity.VisualScripting.IOptimizedAccessor>propertyAccessors  // static @ 0x8
  private   static readonly System.Collections.Generic.Dictionary<System.Reflection.MethodInfo,Unity.VisualScripting.IOptimizedInvoker>methodInvokers  // static @ 0x10
  public    static readonly System.Boolean                  jitAvailable  // static @ 0x18
  private   static  System.Boolean                  _useJitIfAvailable  // static @ 0x19
  private   static  System.Boolean                  <safeMode>k__BackingField  // static @ 0x1a
PROPERTIES:
  useJit  get=0x0B360A68
  useJitIfAvailable  get=0x0B360A18
  safeMode  get=0x0B3609C8  set=0x0B360AF0
METHODS:
  RVA=0x04A2CBC0  token=0x60001DA  System.Void .cctor()
  RVA=0x03FCE770  token=0x60001DF  System.Void OnRuntimeMethodLoad()
  RVA=0x0B360764  token=0x60001E0  System.Void VerifyStaticTarget(System.Type targetType, System.Object target)
  RVA=-1  // generic def  token=0x60001E1  System.Void VerifyInstanceTarget(System.Object target)
  RVA=0x0B3607B8  token=0x60001E2  System.Void VerifyTarget(System.Type targetType, System.Object target, System.Boolean static)
  RVA=0x0B360498  token=0x60001E3  System.Boolean SupportsOptimization(System.Reflection.MemberInfo memberInfo)
  RVA=0x0B3602CC  token=0x60001E4  Unity.VisualScripting.IOptimizedAccessor Prewarm(System.Reflection.FieldInfo fieldInfo)
  RVA=0x0B3603B0  token=0x60001E5  System.Void SetValueOptimized(System.Reflection.FieldInfo fieldInfo, System.Object target, System.Object value)
  RVA=0x0B36071C  token=0x60001E6  System.Boolean SupportsOptimization(System.Reflection.FieldInfo fieldInfo)
  RVA=0x0B35D618  token=0x60001E7  Unity.VisualScripting.IOptimizedAccessor GetFieldAccessor(System.Reflection.FieldInfo fieldInfo)
  RVA=0x0B360318  token=0x60001E8  Unity.VisualScripting.IOptimizedAccessor Prewarm(System.Reflection.PropertyInfo propertyInfo)
  RVA=0x0B360424  token=0x60001E9  System.Void SetValueOptimized(System.Reflection.PropertyInfo propertyInfo, System.Object target, System.Object value)
  RVA=0x0B3606D4  token=0x60001EA  System.Boolean SupportsOptimization(System.Reflection.PropertyInfo propertyInfo)
  RVA=0x0B35FD80  token=0x60001EB  Unity.VisualScripting.IOptimizedAccessor GetPropertyAccessor(System.Reflection.PropertyInfo propertyInfo)
  RVA=0x0B360364  token=0x60001EC  Unity.VisualScripting.IOptimizedInvoker Prewarm(System.Reflection.MethodInfo methodInfo)
  RVA=0x0B360258  token=0x60001ED  System.Object InvokeOptimized(System.Reflection.MethodInfo methodInfo, System.Object target, System.Object arg0)
  RVA=0x0B360510  token=0x60001EE  System.Boolean SupportsOptimization(System.Reflection.MethodInfo methodInfo)
  RVA=0x0B35DAC8  token=0x60001EF  Unity.VisualScripting.IOptimizedInvoker GetMethodInvoker(System.Reflection.MethodInfo methodInfo)
END_CLASS

CLASS: Unity.VisualScripting.ReflectionFieldAccessor
TYPE:  sealed class
TOKEN: 0x2000077
SIZE:  0x18
IMPLEMENTS: Unity.VisualScripting.IOptimizedAccessor
FIELDS:
  private   readonly System.Reflection.FieldInfo     fieldInfo  // 0x10
METHODS:
  RVA=0x0B362078  token=0x60001F3  System.Void .ctor(System.Reflection.FieldInfo fieldInfo)
  RVA=0x041E1670  token=0x60001F4  System.Void Compile()
  RVA=0x08B984D4  token=0x60001F5  System.Object GetValue(System.Object target)
  RVA=0x07C27D38  token=0x60001F6  System.Void SetValue(System.Object target, System.Object value)
END_CLASS

CLASS: Unity.VisualScripting.ReflectionInvoker
TYPE:  class
TOKEN: 0x2000078
SIZE:  0x18
IMPLEMENTS: Unity.VisualScripting.IOptimizedInvoker
FIELDS:
  private   readonly System.Reflection.MethodInfo    methodInfo  // 0x10
  private   static readonly System.Object[]                 EmptyObjects  // static @ 0x0
METHODS:
  RVA=0x0B362230  token=0x60001F7  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
  RVA=0x041E1670  token=0x60001F8  System.Void Compile()
  RVA=0x0B36212C  token=0x60001F9  System.Object Invoke(System.Object target, System.Object arg0)
  RVA=0x0B3621CC  token=0x60001FA  System.Void .cctor()
END_CLASS

CLASS: Unity.VisualScripting.ReflectionPropertyAccessor
TYPE:  sealed class
TOKEN: 0x2000079
SIZE:  0x18
IMPLEMENTS: Unity.VisualScripting.IOptimizedAccessor
FIELDS:
  private   readonly System.Reflection.PropertyInfo  propertyInfo  // 0x10
METHODS:
  RVA=0x0B3622E4  token=0x60001FB  System.Void .ctor(System.Reflection.PropertyInfo propertyInfo)
  RVA=0x041E1670  token=0x60001FC  System.Void Compile()
  RVA=0x08B95960  token=0x60001FD  System.Object GetValue(System.Object target)
  RVA=0x09171000  token=0x60001FE  System.Void SetValue(System.Object target, System.Object value)
END_CLASS

CLASS: Unity.VisualScripting.StaticActionInvokerBase
TYPE:  abstract class
TOKEN: 0x200007A
SIZE:  0x20
EXTENDS: Unity.VisualScripting.StaticInvokerBase
FIELDS:
METHODS:
  RVA=0x092E798C  token=0x60001FF  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
END_CLASS

CLASS: Unity.VisualScripting.StaticActionInvoker
TYPE:  sealed class
TOKEN: 0x200007B
SIZE:  0x28
EXTENDS: Unity.VisualScripting.StaticActionInvokerBase
FIELDS:
  private           System.Action                   invoke  // 0x20
METHODS:
  RVA=0x092E798C  token=0x6000200  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
  RVA=0x0B367890  token=0x6000201  System.Type[] GetParameterTypes()
  RVA=0x0B367774  token=0x6000202  System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions)
  RVA=0x0B367814  token=0x6000203  System.Void CreateDelegate()
  RVA=0x0B3678E0  token=0x6000204  System.Void <CreateDelegate>b__7_0()
END_CLASS

CLASS: Unity.VisualScripting.StaticActionInvoker`1
TYPE:  sealed class
TOKEN: 0x200007C
EXTENDS: Unity.VisualScripting.StaticActionInvokerBase
FIELDS:
  private           System.Action<TParam0>          invoke  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000205  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
  RVA=-1  // not resolved  token=0x6000206  System.Object Invoke(System.Object target, System.Object arg0)
  RVA=-1  // not resolved  token=0x6000207  System.Object InvokeUnsafe(System.Object target, System.Object arg0)
  RVA=-1  // not resolved  token=0x6000208  System.Type[] GetParameterTypes()
  RVA=-1  // not resolved  token=0x6000209  System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions)
  RVA=-1  // not resolved  token=0x600020A  System.Void CreateDelegate()
  RVA=-1  // not resolved  token=0x600020B  System.Void <CreateDelegate>b__7_0(TParam0 param0)
END_CLASS

CLASS: Unity.VisualScripting.StaticActionInvoker`2
TYPE:  sealed class
TOKEN: 0x200007D
EXTENDS: Unity.VisualScripting.StaticActionInvokerBase
FIELDS:
  private           System.Action<TParam0,TParam1>  invoke  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600020C  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
  RVA=-1  // not resolved  token=0x600020D  System.Type[] GetParameterTypes()
  RVA=-1  // not resolved  token=0x600020E  System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions)
  RVA=-1  // not resolved  token=0x600020F  System.Void CreateDelegate()
  RVA=-1  // not resolved  token=0x6000210  System.Void <CreateDelegate>b__7_0(TParam0 param0, TParam1 param1)
END_CLASS

CLASS: Unity.VisualScripting.StaticActionInvoker`3
TYPE:  sealed class
TOKEN: 0x200007E
EXTENDS: Unity.VisualScripting.StaticActionInvokerBase
FIELDS:
  private           System.Action<TParam0,TParam1,TParam2>invoke  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000211  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
  RVA=-1  // not resolved  token=0x6000212  System.Type[] GetParameterTypes()
  RVA=-1  // not resolved  token=0x6000213  System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions)
  RVA=-1  // not resolved  token=0x6000214  System.Void CreateDelegate()
  RVA=-1  // not resolved  token=0x6000215  System.Void <CreateDelegate>b__7_0(TParam0 param0, TParam1 param1, TParam2 param2)
END_CLASS

CLASS: Unity.VisualScripting.StaticActionInvoker`4
TYPE:  sealed class
TOKEN: 0x200007F
EXTENDS: Unity.VisualScripting.StaticActionInvokerBase
FIELDS:
  private           System.Action<TParam0,TParam1,TParam2,TParam3>invoke  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000216  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
  RVA=-1  // not resolved  token=0x6000217  System.Type[] GetParameterTypes()
  RVA=-1  // not resolved  token=0x6000218  System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions)
  RVA=-1  // not resolved  token=0x6000219  System.Void CreateDelegate()
  RVA=-1  // not resolved  token=0x600021A  System.Void <CreateDelegate>b__7_0(TParam0 param0, TParam1 param1, TParam2 param2, TParam3 param3)
END_CLASS

CLASS: Unity.VisualScripting.StaticActionInvoker`5
TYPE:  sealed class
TOKEN: 0x2000080
EXTENDS: Unity.VisualScripting.StaticActionInvokerBase
FIELDS:
  private           Unity.VisualScripting.Action<TParam0,TParam1,TParam2,TParam3,TParam4>invoke  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600021B  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
  RVA=-1  // not resolved  token=0x600021C  System.Type[] GetParameterTypes()
  RVA=-1  // not resolved  token=0x600021D  System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions)
  RVA=-1  // not resolved  token=0x600021E  System.Void CreateDelegate()
  RVA=-1  // not resolved  token=0x600021F  System.Void <CreateDelegate>b__7_0(TParam0 param0, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
END_CLASS

CLASS: Unity.VisualScripting.StaticFieldAccessor`1
TYPE:  class
TOKEN: 0x2000081
IMPLEMENTS: Unity.VisualScripting.IOptimizedAccessor
FIELDS:
  private   readonly System.Reflection.FieldInfo     fieldInfo  // 0x0
  private           System.Func<TField>             getter  // 0x0
  private           System.Action<TField>           setter  // 0x0
  private           System.Type                     targetType  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000220  System.Void .ctor(System.Reflection.FieldInfo fieldInfo)
  RVA=-1  // not resolved  token=0x6000221  System.Void Compile()
  RVA=-1  // not resolved  token=0x6000222  System.Object GetValue(System.Object target)
  RVA=-1  // not resolved  token=0x6000223  System.Object GetValueUnsafe(System.Object target)
  RVA=-1  // not resolved  token=0x6000224  System.Void SetValue(System.Object target, System.Object value)
  RVA=-1  // not resolved  token=0x6000225  System.Void SetValueUnsafe(System.Object target, System.Object value)
  RVA=-1  // not resolved  token=0x6000226  TField <Compile>b__5_0()
  RVA=-1  // not resolved  token=0x6000227  System.Void <Compile>b__5_1(TField value)
END_CLASS

CLASS: Unity.VisualScripting.StaticFunctionInvokerBase`1
TYPE:  abstract class
TOKEN: 0x2000083
EXTENDS: Unity.VisualScripting.StaticInvokerBase
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600022A  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
END_CLASS

CLASS: Unity.VisualScripting.StaticFunctionInvoker`1
TYPE:  sealed class
TOKEN: 0x2000084
EXTENDS: Unity.VisualScripting.StaticFunctionInvokerBase`1
FIELDS:
  private           System.Func<TResult>            invoke  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600022B  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
  RVA=-1  // not resolved  token=0x600022C  System.Type[] GetParameterTypes()
  RVA=-1  // not resolved  token=0x600022D  System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions)
  RVA=-1  // not resolved  token=0x600022E  System.Void CreateDelegate()
  RVA=-1  // not resolved  token=0x600022F  TResult <CreateDelegate>b__7_0()
END_CLASS

CLASS: Unity.VisualScripting.StaticFunctionInvoker`2
TYPE:  sealed class
TOKEN: 0x2000085
EXTENDS: Unity.VisualScripting.StaticFunctionInvokerBase`1
FIELDS:
  private           System.Func<TParam0,TResult>    invoke  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000230  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
  RVA=-1  // not resolved  token=0x6000231  System.Object Invoke(System.Object target, System.Object arg0)
  RVA=-1  // not resolved  token=0x6000232  System.Object InvokeUnsafe(System.Object target, System.Object arg0)
  RVA=-1  // not resolved  token=0x6000233  System.Type[] GetParameterTypes()
  RVA=-1  // not resolved  token=0x6000234  System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions)
  RVA=-1  // not resolved  token=0x6000235  System.Void CreateDelegate()
  RVA=-1  // not resolved  token=0x6000236  TResult <CreateDelegate>b__7_0(TParam0 param0)
END_CLASS

CLASS: Unity.VisualScripting.StaticFunctionInvoker`3
TYPE:  sealed class
TOKEN: 0x2000086
EXTENDS: Unity.VisualScripting.StaticFunctionInvokerBase`1
FIELDS:
  private           System.Func<TParam0,TParam1,TResult>invoke  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000237  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
  RVA=-1  // not resolved  token=0x6000238  System.Type[] GetParameterTypes()
  RVA=-1  // not resolved  token=0x6000239  System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions)
  RVA=-1  // not resolved  token=0x600023A  System.Void CreateDelegate()
  RVA=-1  // not resolved  token=0x600023B  TResult <CreateDelegate>b__7_0(TParam0 param0, TParam1 param1)
END_CLASS

CLASS: Unity.VisualScripting.StaticFunctionInvoker`4
TYPE:  sealed class
TOKEN: 0x2000087
EXTENDS: Unity.VisualScripting.StaticFunctionInvokerBase`1
FIELDS:
  private           System.Func<TParam0,TParam1,TParam2,TResult>invoke  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600023C  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
  RVA=-1  // not resolved  token=0x600023D  System.Type[] GetParameterTypes()
  RVA=-1  // not resolved  token=0x600023E  System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions)
  RVA=-1  // not resolved  token=0x600023F  System.Void CreateDelegate()
  RVA=-1  // not resolved  token=0x6000240  TResult <CreateDelegate>b__7_0(TParam0 param0, TParam1 param1, TParam2 param2)
END_CLASS

CLASS: Unity.VisualScripting.StaticFunctionInvoker`5
TYPE:  sealed class
TOKEN: 0x2000088
EXTENDS: Unity.VisualScripting.StaticFunctionInvokerBase`1
FIELDS:
  private           System.Func<TParam0,TParam1,TParam2,TParam3,TResult>invoke  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000241  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
  RVA=-1  // not resolved  token=0x6000242  System.Type[] GetParameterTypes()
  RVA=-1  // not resolved  token=0x6000243  System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions)
  RVA=-1  // not resolved  token=0x6000244  System.Void CreateDelegate()
  RVA=-1  // not resolved  token=0x6000245  TResult <CreateDelegate>b__7_0(TParam0 param0, TParam1 param1, TParam2 param2, TParam3 param3)
END_CLASS

CLASS: Unity.VisualScripting.StaticFunctionInvoker`6
TYPE:  sealed class
TOKEN: 0x2000089
EXTENDS: Unity.VisualScripting.StaticFunctionInvokerBase`1
FIELDS:
  private           Unity.VisualScripting.Func<TParam0,TParam1,TParam2,TParam3,TParam4,TResult>invoke  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000246  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
  RVA=-1  // not resolved  token=0x6000247  System.Type[] GetParameterTypes()
  RVA=-1  // not resolved  token=0x6000248  System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions)
  RVA=-1  // not resolved  token=0x6000249  System.Void CreateDelegate()
  RVA=-1  // not resolved  token=0x600024A  TResult <CreateDelegate>b__7_0(TParam0 param0, TParam1 param1, TParam2 param2, TParam3 param3, TParam4 param4)
END_CLASS

CLASS: Unity.VisualScripting.StaticInvokerBase
TYPE:  abstract class
TOKEN: 0x200008A
SIZE:  0x20
EXTENDS: Unity.VisualScripting.InvokerBase
FIELDS:
METHODS:
  RVA=0x0B367A54  token=0x600024B  System.Void .ctor(System.Reflection.MethodInfo methodInfo)
  RVA=0x0B367988  token=0x600024C  System.Void CompileExpression()
  RVA=-1  // abstract  token=0x600024D  System.Void CompileExpression(System.Linq.Expressions.MethodCallExpression callExpression, System.Linq.Expressions.ParameterExpression[] parameterExpressions)
  RVA=0x0B367A00  token=0x600024E  System.Void VerifyTarget(System.Object target)
END_CLASS

CLASS: Unity.VisualScripting.StaticPropertyAccessor`1
TYPE:  class
TOKEN: 0x200008B
IMPLEMENTS: Unity.VisualScripting.IOptimizedAccessor
FIELDS:
  private   readonly System.Reflection.PropertyInfo  propertyInfo  // 0x0
  private           System.Func<TProperty>          getter  // 0x0
  private           System.Action<TProperty>        setter  // 0x0
  private           System.Type                     targetType  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600024F  System.Void .ctor(System.Reflection.PropertyInfo propertyInfo)
  RVA=-1  // not resolved  token=0x6000250  System.Void Compile()
  RVA=-1  // not resolved  token=0x6000251  System.Object GetValue(System.Object target)
  RVA=-1  // not resolved  token=0x6000252  System.Object GetValueUnsafe(System.Object target)
  RVA=-1  // not resolved  token=0x6000253  System.Void SetValue(System.Object target, System.Object value)
  RVA=-1  // not resolved  token=0x6000254  System.Void SetValueUnsafe(System.Object target, System.Object value)
END_CLASS

CLASS: Unity.VisualScripting.RenamedAssemblyAttribute
TYPE:  sealed class
TOKEN: 0x200008C
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  private   readonly System.String                   <previousName>k__BackingField  // 0x10
  private   readonly System.String                   <newName>k__BackingField  // 0x18
PROPERTIES:
  previousName  get=0x02B2ECC0
  newName  get=0x0385B100
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.RenamedFromAttribute
TYPE:  sealed class
TOKEN: 0x200008D
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  private   readonly System.String                   <previousName>k__BackingField  // 0x10
PROPERTIES:
  previousName  get=0x02B2ECC0
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.RenamedNamespaceAttribute
TYPE:  sealed class
TOKEN: 0x200008E
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  private   readonly System.String                   <previousName>k__BackingField  // 0x10
  private   readonly System.String                   <newName>k__BackingField  // 0x18
PROPERTIES:
  previousName  get=0x02B2ECC0
  newName  get=0x0385B100
METHODS:
  RVA=0x032AF420  token=0x6000258  System.Void .ctor(System.String previousName, System.String newName)
END_CLASS

CLASS: Unity.VisualScripting.RuntimeCodebase
TYPE:  static class
TOKEN: 0x200008F
SIZE:  0x10
FIELDS:
  private   static readonly System.Object                   lock  // static @ 0x0
  private   static readonly System.Collections.Generic.List<System.Type>_types  // static @ 0x8
  private   static readonly System.Collections.Generic.List<System.Reflection.Assembly>_assemblies  // static @ 0x10
  public    static  System.Collections.Generic.HashSet<System.String>disallowedAssemblies  // static @ 0x18
  private   static readonly System.Collections.Generic.Dictionary<System.String,System.Type>typeSerializations  // static @ 0x20
  private   static  System.Collections.Generic.Dictionary<System.String,System.Type>_renamedTypes  // static @ 0x28
  private   static  System.Collections.Generic.Dictionary<System.String,System.String>_renamedNamespaces  // static @ 0x30
  private   static  System.Collections.Generic.Dictionary<System.String,System.String>_renamedAssemblies  // static @ 0x38
  private   static readonly System.Collections.Generic.Dictionary<System.Type,System.Collections.Generic.Dictionary<System.String,System.String>>_renamedMembers  // static @ 0x40
PROPERTIES:
  types  get=0x0B364CF4
  assemblies  get=0x0B364AD0
  renamedNamespaces  get=0x0B364BBC
  renamedAssemblies  get=0x0B364B20
  renamedTypes  get=0x0B364C58
METHODS:
  RVA=0x0B3644EC  token=0x600025D  System.Void .cctor()
  RVA=0x0B363420  token=0x600025E  System.Collections.Generic.IEnumerable<System.Attribute> GetAssemblyAttributes(System.Type attributeType)
  RVA=0x0B363398  token=0x600025F  System.Collections.Generic.IEnumerable<System.Attribute> GetAssemblyAttributes(System.Type attributeType, System.Collections.Generic.IEnumerable<System.Reflection.Assembly> assemblies)
  RVA=-1  // generic def  token=0x6000260  System.Collections.Generic.IEnumerable<TAttribute> GetAssemblyAttributes()
  RVA=0x0B363474  token=0x6000261  System.String SerializeType(System.Type type)
  RVA=0x0B363588  token=0x6000262  System.Boolean TryDeserializeType(System.String typeName, System.Type& type)
  RVA=0x0B362398  token=0x6000263  System.Type DeserializeType(System.String typeName)
  RVA=0x0B363508  token=0x6000264  System.Boolean TryCachedTypeLookup(System.String typeName, System.Type& type)
  RVA=0x0B363C38  token=0x6000265  System.Boolean TrySystemTypeLookup(System.String typeName, System.Type& type)
  RVA=0x0B364060  token=0x6000266  System.Boolean TrySystemTypeLookup(Unity.VisualScripting.TypeName typeName, System.Type& type)
  RVA=0x0B363764  token=0x6000267  System.Boolean TryRenamedTypeLookup(System.String previousTypeName, System.Type& type)
  RVA=0x0B3627FC  token=0x600026B  System.Collections.Generic.Dictionary<System.String,System.String> FetchRenamedNamespaces()
  RVA=0x0B362484  token=0x600026C  System.Collections.Generic.Dictionary<System.String,System.String> FetchRenamedAssemblies()
  RVA=0x0B362B74  token=0x600026D  System.Collections.Generic.Dictionary<System.String,System.Type> FetchRenamedTypes()
END_CLASS

CLASS: Unity.VisualScripting.TypeName
TYPE:  class
TOKEN: 0x2000092
SIZE:  0x58
FIELDS:
  private           System.String                   <AssemblyDescription>k__BackingField  // 0x10
  private           System.String                   <AssemblyName>k__BackingField  // 0x18
  private           System.String                   <AssemblyVersion>k__BackingField  // 0x20
  private           System.String                   <AssemblyCulture>k__BackingField  // 0x28
  private           System.String                   <AssemblyPublicKeyToken>k__BackingField  // 0x30
  private   readonly System.Collections.Generic.List<Unity.VisualScripting.TypeName><GenericParameters>k__BackingField  // 0x38
  private   readonly System.Collections.Generic.List<System.String>names  // 0x40
  private   readonly System.Collections.Generic.List<System.Int32>genericarities  // 0x48
  private           System.String                   <Name>k__BackingField  // 0x50
PROPERTIES:
  AssemblyDescription  get=0x02B2ECC0  set=0x053908C0
  AssemblyName  get=0x0385B100  set=0x05392C40
  AssemblyVersion  set=0x05390F40
  AssemblyCulture  set=0x02FDB880
  AssemblyPublicKeyToken  set=0x05396200
  GenericParameters  get=0x04D85A50
  Name  get=0x04D86270  set=0x02FDAEA0
  IsArray  get=0x0B36976C
METHODS:
  RVA=0x0B367EB0  token=0x6000285  Unity.VisualScripting.TypeName Parse(System.String s)
  RVA=0x0B368DD0  token=0x6000286  System.Void .ctor(System.String s, System.Int32& index)
  RVA=0x0B367DC8  token=0x6000287  System.String LookForPairThenRemove(System.Collections.Generic.List<System.String> strings, System.String Name)
  RVA=0x0B368404  token=0x6000288  System.Void ReplaceNamespace(System.String oldNamespace, System.String newNamespace)
  RVA=0x0B367F1C  token=0x6000289  System.Void ReplaceAssembly(System.String oldAssembly, System.String newAssembly)
  RVA=0x0B36837C  token=0x600028A  System.Void ReplaceName(System.String oldTypeName, System.Type newType)
  RVA=0x0B368124  token=0x600028B  System.Void ReplaceName(System.String oldTypeName, System.String newTypeName, System.Reflection.AssemblyName newAssemblyName)
  RVA=0x0B368858  token=0x600028C  System.String ToElementTypeName(System.String s)
  RVA=0x0B3687E4  token=0x600028D  System.String ToArrayOrType(System.String oldType, System.String newType)
  RVA=0x0B3686F0  token=0x600028E  System.Void SetAssemblyName(System.Reflection.AssemblyName newAssemblyName)
  RVA=0x0B368C80  token=0x600028F  System.Void UpdateName()
  RVA=0x0B3688F0  token=0x6000290  System.String ToString(Unity.VisualScripting.TypeNameDetail specification, Unity.VisualScripting.TypeNameDetail genericsSpecification)
  RVA=0x0B368C70  token=0x6000291  System.String ToString()
  RVA=0x0B3688E0  token=0x6000292  System.String ToLooseString()
END_CLASS

CLASS: Unity.VisualScripting.TypeNameDetail
TYPE:  sealed struct
TOKEN: 0x2000095
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Unity.VisualScripting.TypeNameDetailName  // const
  public    static  Unity.VisualScripting.TypeNameDetailNameAndAssembly  // const
  public    static  Unity.VisualScripting.TypeNameDetailFull  // const
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.TypeUtility
TYPE:  static class
TOKEN: 0x2000096
SIZE:  0x10
FIELDS:
  private   static readonly System.Collections.Generic.HashSet<System.Type>_numericTypes  // static @ 0x0
  private   static readonly System.Collections.Generic.HashSet<System.Type>_numericConstructTypes  // static @ 0x8
  private   static readonly System.Collections.Generic.HashSet<System.Type>typesWithShortStrings  // static @ 0x10
  private   static readonly System.Collections.Generic.Dictionary<System.Type,System.Object>defaultPrimitives  // static @ 0x18
METHODS:
  RVA=0x0B369898  token=0x6000296  System.Boolean IsBasic(System.Type type)
  RVA=0x0B369AAC  token=0x6000297  System.Boolean IsStatic(System.Type type)
  RVA=0x0B369A14  token=0x6000298  System.Boolean IsNullable(System.Type type)
  RVA=0x0B369A88  token=0x6000299  System.Boolean IsReferenceType(System.Type type)
  RVA=0x0B369828  token=0x600029A  System.Boolean IsAssignableFrom(System.Type type, System.Object value)
  RVA=0x0B3697B8  token=0x600029B  System.Collections.Generic.IEnumerable<System.Type> GetTypesSafely(System.Reflection.Assembly assembly)
  RVA=0x0B369AE4  token=0x600029C  System.Void .cctor()
END_CLASS

CLASS: Unity.VisualScripting.LooseAssemblyNameConverter
TYPE:  class
TOKEN: 0x2000099
SIZE:  0x18
EXTENDS: Unity.VisualScripting.FullSerializer.fsDirectConverter
FIELDS:
PROPERTIES:
  ModelType  get=0x0B35C3EC
METHODS:
  RVA=0x0B35C1B8  token=0x60002AA  System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType)
  RVA=0x0B35C31C  token=0x60002AB  Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType)
  RVA=0x0B35C1F4  token=0x60002AC  Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance, System.Type storageType)
  RVA=0x041E1670  token=0x60002AD  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.NamespaceConverter
TYPE:  class
TOKEN: 0x200009A
SIZE:  0x18
EXTENDS: Unity.VisualScripting.FullSerializer.fsDirectConverter
FIELDS:
PROPERTIES:
  ModelType  get=0x0B35D178
METHODS:
  RVA=0x0B35CF44  token=0x60002AF  System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType)
  RVA=0x0B35D09C  token=0x60002B0  Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType)
  RVA=0x0B35CF80  token=0x60002B1  Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance, System.Type storageType)
  RVA=0x041E1670  token=0x60002B2  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.Ray2DConverter
TYPE:  class
TOKEN: 0x200009B
SIZE:  0x18
EXTENDS: Unity.VisualScripting.FullSerializer.fsDirectConverter`1
FIELDS:
METHODS:
  RVA=0x0B361000  token=0x60002B3  Unity.VisualScripting.FullSerializer.fsResult DoSerialize(UnityEngine.Ray2D model, System.Collections.Generic.Dictionary<System.String,Unity.VisualScripting.FullSerializer.fsData> serialized)
  RVA=0x0B360E90  token=0x60002B4  Unity.VisualScripting.FullSerializer.fsResult DoDeserialize(System.Collections.Generic.Dictionary<System.String,Unity.VisualScripting.FullSerializer.fsData> data, UnityEngine.Ray2D& model)
  RVA=0x0B360E50  token=0x60002B5  System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType)
  RVA=0x0B361154  token=0x60002B6  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.RayConverter
TYPE:  class
TOKEN: 0x200009C
SIZE:  0x18
EXTENDS: Unity.VisualScripting.FullSerializer.fsDirectConverter`1
FIELDS:
METHODS:
  RVA=0x0B361364  token=0x60002B7  Unity.VisualScripting.FullSerializer.fsResult DoSerialize(UnityEngine.Ray model, System.Collections.Generic.Dictionary<System.String,Unity.VisualScripting.FullSerializer.fsData> serialized)
  RVA=0x0B3611D0  token=0x60002B8  Unity.VisualScripting.FullSerializer.fsResult DoDeserialize(System.Collections.Generic.Dictionary<System.String,Unity.VisualScripting.FullSerializer.fsData> data, UnityEngine.Ray& model)
  RVA=0x0B36117C  token=0x60002B9  System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType)
  RVA=0x0B3614CC  token=0x60002BA  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.UnityObjectConverter
TYPE:  class
TOKEN: 0x200009D
SIZE:  0x18
EXTENDS: Unity.VisualScripting.FullSerializer.fsConverter
FIELDS:
PROPERTIES:
  objectReferences  get=0x0B36C10C
METHODS:
  RVA=0x0B36BD54  token=0x60002BC  System.Boolean CanProcess(System.Type type)
  RVA=0x012081B0  token=0x60002BD  System.Boolean RequestCycleSupport(System.Type storageType)
  RVA=0x012081B0  token=0x60002BE  System.Boolean RequestInheritanceSupport(System.Type storageType)
  RVA=0x0B36BFE0  token=0x60002BF  Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType)
  RVA=0x0B36BDCC  token=0x60002C0  Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData storage, System.Object& instance, System.Type storageType)
  RVA=0x04D8C7E0  token=0x60002C1  System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType)
  RVA=0x041E1670  token=0x60002C2  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.DoNotSerializeAttribute
TYPE:  sealed class
TOKEN: 0x200009E
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60002C3  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.ISerializationDependency
TYPE:  interface
TOKEN: 0x200009F
IMPLEMENTS: UnityEngine.ISerializationCallbackReceiver
FIELDS:
PROPERTIES:
  IsDeserialized  get=-1  // abstract
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.ISerializationDepender
TYPE:  interface
TOKEN: 0x20000A0
IMPLEMENTS: UnityEngine.ISerializationCallbackReceiver
FIELDS:
PROPERTIES:
  deserializationDependencies  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x60002C6  System.Void OnAfterDependenciesDeserialized()
END_CLASS

CLASS: Unity.VisualScripting.Serialization
TYPE:  static class
TOKEN: 0x20000A1
SIZE:  0x10
FIELDS:
  private   static readonly System.Collections.Generic.HashSet<Unity.VisualScripting.SerializationOperation>freeOperations  // static @ 0x0
  private   static readonly System.Collections.Generic.HashSet<Unity.VisualScripting.SerializationOperation>busyOperations  // static @ 0x8
  private   static readonly System.Object                   lock  // static @ 0x10
  private   static  System.Boolean                  <isUnitySerializing>k__BackingField  // static @ 0x18
  private   static readonly System.Collections.Generic.HashSet<Unity.VisualScripting.ISerializationDepender>awaitingDependers  // static @ 0x20
PROPERTIES:
  isUnitySerializing  get=0x0B3676D0  set=0x0B367720
  isCustomSerializing  get=0x0B36766C
METHODS:
  RVA=0x0B3674DC  token=0x60002C7  System.Void .cctor()
  RVA=0x0B3672A0  token=0x60002CB  Unity.VisualScripting.SerializationOperation StartOperation()
  RVA=0x0B3667BC  token=0x60002CC  System.Void EndOperation(Unity.VisualScripting.SerializationOperation operation)
  RVA=0x0B366F18  token=0x60002CD  Unity.VisualScripting.SerializationData Serialize(System.Object value, System.Boolean forceReflected)
  RVA=0x0B365F38  token=0x60002CE  System.Void DeserializeInto(Unity.VisualScripting.SerializationData data, System.Object& instance, System.Boolean forceReflected)
  RVA=0x0B366750  token=0x60002CF  System.Object Deserialize(Unity.VisualScripting.SerializationData data, System.Boolean forceReflected)
  RVA=0x0B366CEC  token=0x60002D0  System.String SerializeJson(Unity.VisualScripting.FullSerializer.fsSerializer serializer, System.Object instance, System.Boolean forceReflected)
  RVA=0x0B3664D0  token=0x60002D1  Unity.VisualScripting.FullSerializer.fsResult DeserializeJsonUtil(Unity.VisualScripting.FullSerializer.fsSerializer serializer, System.String json, System.Object& instance, System.Boolean forceReflected)
  RVA=0x0B3665F8  token=0x60002D2  System.Void DeserializeJson(Unity.VisualScripting.FullSerializer.fsSerializer serializer, System.String json, System.Object& instance, System.Boolean forceReflected)
  RVA=0x0B3669C8  token=0x60002D3  System.Void HandleResult(System.String label, Unity.VisualScripting.FullSerializer.fsResult result, UnityEngine.Object context)
  RVA=0x0B366CD0  token=0x60002D4  System.String PrettyPrint(System.String json)
  RVA=0x0B365AB4  token=0x60002D5  System.Void AwaitDependencies(Unity.VisualScripting.ISerializationDepender depender)
  RVA=0x0B365B2C  token=0x60002D6  System.Void CheckIfDependenciesMet(Unity.VisualScripting.ISerializationDepender depender)
END_CLASS

CLASS: Unity.VisualScripting.SerializationData
TYPE:  sealed struct
TOKEN: 0x20000A2
SIZE:  0x20
FIELDS:
  private           System.String                   _json  // 0x10
  private           UnityEngine.Object[]            _objectReferences  // 0x18
PROPERTIES:
  json  get=0x02B76770
  objectReferences  get=0x04D88320
METHODS:
  RVA=0x0B3657A0  token=0x60002D9  System.Void .ctor(System.String json, System.Collections.Generic.IEnumerable<UnityEngine.Object> objectReferences)
  RVA=0x0B365798  token=0x60002DA  System.Void .ctor(System.String json, UnityEngine.Object[] objectReferences)
  RVA=0x09ACC1AC  token=0x60002DB  System.Void Clear()
  RVA=0x0B3650D0  token=0x60002DC  System.String ToString(System.String title)
  RVA=0x0B36578C  token=0x60002DD  System.String ToString()
  RVA=0x0B365004  token=0x60002DE  System.Void ShowString(System.String title)
END_CLASS

CLASS: Unity.VisualScripting.SerializationOperation
TYPE:  class
TOKEN: 0x20000A3
SIZE:  0x20
FIELDS:
  private           Unity.VisualScripting.FullSerializer.fsSerializer<serializer>k__BackingField  // 0x10
  private           System.Collections.Generic.List<UnityEngine.Object><objectReferences>k__BackingField  // 0x18
PROPERTIES:
  serializer  get=0x02B2ECC0  set=0x053908C0
  objectReferences  get=0x0385B100  set=0x05392C40
METHODS:
  RVA=0x0B365888  token=0x60002DF  System.Void .ctor()
  RVA=0x0B365840  token=0x60002E4  System.Void Reset()
END_CLASS

CLASS: Unity.VisualScripting.SerializationVersionAttribute
TYPE:  class
TOKEN: 0x20000A4
SIZE:  0x38
EXTENDS: Unity.VisualScripting.FullSerializer.fsObjectAttribute
FIELDS:
METHODS:
  RVA=0x0B365AAC  token=0x60002E5  System.Void .ctor(System.String versionString, System.Type[] previousModels)
END_CLASS

CLASS: Unity.VisualScripting.SerializeAsAttribute
TYPE:  class
TOKEN: 0x20000A5
SIZE:  0x20
EXTENDS: Unity.VisualScripting.FullSerializer.fsPropertyAttribute
FIELDS:
METHODS:
  RVA=0x053908C0  token=0x60002E6  System.Void .ctor(System.String name)
END_CLASS

CLASS: Unity.VisualScripting.SerializeAttribute
TYPE:  class
TOKEN: 0x20000A6
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x60002E7  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.ISingleton
TYPE:  interface
TOKEN: 0x20000A7
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.LudiqScriptableObject
TYPE:  abstract class
TOKEN: 0x20000A8
SIZE:  0x30
EXTENDS: UnityEngine.ScriptableObject
IMPLEMENTS: UnityEngine.ISerializationCallbackReceiver
FIELDS:
  protected         Unity.VisualScripting.SerializationData_data  // 0x18
  private           System.Action                   OnDestroyActions  // 0x28
EVENTS:
  OnDestroyActions  add=add_OnDestroyActions  remove=remove_OnDestroyActions
METHODS:
  RVA=0x0B35C748  token=0x60002EA  System.Void UnityEngine.ISerializationCallbackReceiver.OnBeforeSerialize()
  RVA=0x0B35C5E4  token=0x60002EB  System.Void UnityEngine.ISerializationCallbackReceiver.OnAfterDeserialize()
  RVA=0x041E1670  token=0x60002EC  System.Void OnBeforeSerialize()
  RVA=0x041E1670  token=0x60002ED  System.Void OnAfterSerialize()
  RVA=0x041E1670  token=0x60002EE  System.Void OnBeforeDeserialize()
  RVA=0x041E1670  token=0x60002EF  System.Void OnAfterDeserialize()
  RVA=0x041E1670  token=0x60002F0  System.Void OnPostDeserializeInEditor()
  RVA=0x0B35C548  token=0x60002F1  System.Void OnDestroy()
  RVA=0x0B35C558  token=0x60002F2  System.Void ShowData()
  RVA=0x0B35C5DC  token=0x60002F3  System.String ToString()
  RVA=0x03573370  token=0x60002F4  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.Singleton`1
TYPE:  static class
TOKEN: 0x20000A9
FIELDS:
  private   static readonly Unity.VisualScripting.SingletonAttributeattribute  // static @ 0x0
  private   static readonly System.Object                   _lock  // static @ 0x0
  private   static readonly System.Collections.Generic.HashSet<T>awoken  // static @ 0x0
  private   static  T                               _instance  // static @ 0x0
PROPERTIES:
  persistent  get=-1  // not resolved
  automatic  get=-1  // not resolved
  name  get=-1  // not resolved
  hideFlags  get=-1  // not resolved
  instance  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60002F5  System.Void .cctor()
  RVA=-1  // not resolved  token=0x60002FB  T[] FindObjectsOfType()
  RVA=-1  // not resolved  token=0x60002FC  T[] FindInstances()
  RVA=-1  // not resolved  token=0x60002FD  T Instantiate()
  RVA=-1  // not resolved  token=0x60002FE  System.Void Awake(T instance)
  RVA=-1  // not resolved  token=0x60002FF  System.Void OnDestroy(T instance)
END_CLASS

CLASS: Unity.VisualScripting.SingletonAttribute
TYPE:  sealed class
TOKEN: 0x20000AA
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  private           System.Boolean                  <Persistent>k__BackingField  // 0x10
  private           System.Boolean                  <Automatic>k__BackingField  // 0x11
  private           UnityEngine.HideFlags           <HideFlags>k__BackingField  // 0x14
  private           System.String                   <Name>k__BackingField  // 0x18
PROPERTIES:
  Persistent  get=0x015EFCE0  set=0x04D86130
  Automatic  get=0x04D88380  set=0x04D88460
  HideFlags  get=0x04D882A0  set=0x04D8F540
  Name  get=0x0385B100  set=0x05392C40
METHODS:
  RVA=0x04D9DD80  token=0x6000300  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.UnityThread
TYPE:  static class
TOKEN: 0x20000AB
SIZE:  0x10
FIELDS:
  public    static  System.Threading.Thread         thread  // static @ 0x0
  public    static  System.Collections.Concurrent.ConcurrentQueue<System.Action>pendingQueue  // static @ 0x8
PROPERTIES:
  allowsAPI  get=0x0B36C484
METHODS:
  RVA=0x03FCE110  token=0x600030A  System.Void RuntimeInitialize()
  RVA=0x04A1D420  token=0x600030B  System.Void .cctor()
END_CLASS

CLASS: Unity.VisualScripting.Empty`1
TYPE:  static class
TOKEN: 0x20000AC
FIELDS:
  public    static readonly T[]                             array  // static @ 0x0
  public    static readonly System.Collections.Generic.List<T>list  // static @ 0x0
  public    static readonly System.Collections.Generic.HashSet<T>hashSet  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600030C  System.Void .cctor()
END_CLASS

CLASS: Unity.VisualScripting.HashUtility
TYPE:  static class
TOKEN: 0x20000AD
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x600030D  System.Int32 GetHashCode(T a)
  RVA=-1  // generic def  token=0x600030E  System.Int32 GetHashCode(T1 a, T2 b)
END_CLASS

CLASS: Unity.VisualScripting.IIdentifiable
TYPE:  interface
TOKEN: 0x20000AE
FIELDS:
PROPERTIES:
  guid  get=-1  // abstract
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.LinqUtility
TYPE:  static class
TOKEN: 0x20000AF
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000310  System.Collections.Generic.IEnumerable<T> Concat(System.Collections.IEnumerable[] enumerables)
  RVA=-1  // generic def  token=0x6000311  System.Collections.Generic.IEnumerable<T> NotNull(System.Collections.Generic.IEnumerable<T> enumerable)
END_CLASS

CLASS: Unity.VisualScripting.Recursion`1
TYPE:  class
TOKEN: 0x20000B2
IMPLEMENTS: Unity.VisualScripting.IPoolable System.IDisposable
FIELDS:
  private   readonly System.Collections.Generic.Stack<T>traversedOrder  // 0x0
  private   readonly System.Collections.Generic.Dictionary<T,System.Int32>traversedCount  // 0x0
  private           System.Boolean                  disposed  // 0x0
  protected         System.Int32                    maxDepth  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600031F  System.Void .ctor()
  RVA=-1  // not resolved  token=0x6000320  System.Void Enter(T o)
  RVA=-1  // not resolved  token=0x6000321  System.Boolean TryEnter(T o)
  RVA=-1  // not resolved  token=0x6000322  System.Void Exit(T o)
  RVA=-1  // not resolved  token=0x6000323  System.Void Dispose()
  RVA=-1  // not resolved  token=0x6000324  System.Void Free()
  RVA=-1  // not resolved  token=0x6000325  System.Void Unity.VisualScripting.IPoolable.New()
  RVA=-1  // not resolved  token=0x6000326  System.Void Unity.VisualScripting.IPoolable.Free()
  RVA=-1  // not resolved  token=0x6000327  Unity.VisualScripting.Recursion<T> New()
  RVA=-1  // not resolved  token=0x6000328  Unity.VisualScripting.Recursion<T> New(System.Int32 maxDepth)
END_CLASS

CLASS: Unity.VisualScripting.Recursion
TYPE:  sealed class
TOKEN: 0x20000B4
SIZE:  0x28
EXTENDS: Unity.VisualScripting.Recursion`1
FIELDS:
  private   static  System.Int32                    <defaultMaxDepth>k__BackingField  // static @ 0x0
  private   static  System.Boolean                  <safeMode>k__BackingField  // static @ 0x4
PROPERTIES:
  defaultMaxDepth  get=0x0B3614F4
  safeMode  get=0x0B361544  set=0x0B361594
METHODS:
  RVA=0x03FCE840  token=0x600032F  System.Void OnRuntimeMethodLoad()
  RVA=0x04D79CD0  token=0x6000330  System.Void .cctor()
END_CLASS

CLASS: Unity.VisualScripting.ReferenceCollector
TYPE:  static class
TOKEN: 0x20000B5
SIZE:  0x10
FIELDS:
  private   static  System.Action                   onSceneUnloaded  // static @ 0x0
EVENTS:
  onSceneUnloaded  add=add_onSceneUnloaded  remove=remove_onSceneUnloaded
METHODS:
  RVA=0x03FCEDC0  token=0x6000333  System.Void Initialize()
END_CLASS

CLASS: Unity.VisualScripting.ReferenceEqualityComparer
TYPE:  class
TOKEN: 0x20000B7
SIZE:  0x10
IMPLEMENTS: System.Collections.Generic.IEqualityComparer`1
FIELDS:
  public    static readonly Unity.VisualScripting.ReferenceEqualityComparerInstance  // static @ 0x0
METHODS:
  RVA=0x041E1670  token=0x6000337  System.Void .ctor()
  RVA=0x04D7F860  token=0x6000338  System.Boolean System.Collections.Generic.IEqualityComparer<System.Object>.Equals(System.Object a, System.Object b)
  RVA=0x04CC8900  token=0x6000339  System.Int32 System.Collections.Generic.IEqualityComparer<System.Object>.GetHashCode(System.Object a)
  RVA=0x04D87100  token=0x600033A  System.Int32 GetHashCode(System.Object a)
  RVA=0x04D4DE30  token=0x600033B  System.Void .cctor()
END_CLASS

CLASS: Unity.VisualScripting.ReferenceEqualityComparer`1
TYPE:  class
TOKEN: 0x20000B8
IMPLEMENTS: System.Collections.Generic.IEqualityComparer`1
FIELDS:
  public    static readonly Unity.VisualScripting.ReferenceEqualityComparer<T>Instance  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600033C  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600033D  System.Boolean System.Collections.Generic.IEqualityComparer<T>.Equals(T a, T b)
  RVA=-1  // not resolved  token=0x600033E  System.Int32 System.Collections.Generic.IEqualityComparer<T>.GetHashCode(T a)
  RVA=-1  // not resolved  token=0x600033F  System.Int32 GetHashCode(T a)
  RVA=-1  // not resolved  token=0x6000340  System.Void .cctor()
END_CLASS

CLASS: Unity.VisualScripting.RuntimeVSUsageUtility
TYPE:  static class
TOKEN: 0x20000B9
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x03FCE040  token=0x6000341  System.Void RuntimeInitializeOnLoadBeforeSceneLoad()
END_CLASS

CLASS: Unity.VisualScripting.StringUtility
TYPE:  static class
TOKEN: 0x20000BA
SIZE:  0x10
FIELDS:
  private   static readonly System.Text.RegularExpressions.RegexguidRegex  // static @ 0x0
METHODS:
  RVA=0x0B367C98  token=0x6000342  System.String TrimStart(System.String source, System.String value)
  RVA=0x0B367B28  token=0x6000343  System.Void PartsAround(System.String s, System.Char c, System.String& before, System.String& after)
  RVA=0x0B367C1C  token=0x6000344  System.String ToHexString(System.Byte[] bytes)
  RVA=0x0B367D38  token=0x6000345  System.Void .cctor()
END_CLASS

CLASS: Unity.VisualScripting.UnityObjectUtility
TYPE:  static class
TOKEN: 0x20000BB
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B36C15C  token=0x6000346  System.Boolean IsUnityNull(System.Object obj)
  RVA=0x0B36C1D8  token=0x6000347  System.String ToSafeString(UnityEngine.Object uo)
  RVA=-1  // generic def  token=0x6000348  T AsUnityNull(T obj)
  RVA=0x0B36C3FC  token=0x6000349  System.Boolean TrulyEqual(UnityEngine.Object a, UnityEngine.Object b)
END_CLASS

CLASS: Unity.VisualScripting.ApplicationVariables
TYPE:  static class
TOKEN: 0x20000BC
SIZE:  0x10
FIELDS:
  private   static  Unity.VisualScripting.VariablesAsset_asset  // static @ 0x0
  private   static  Unity.VisualScripting.VariableDeclarations<runtime>k__BackingField  // static @ 0x8
PROPERTIES:
  asset  get=0x03C91D50
  runtime  set=0x0B3526F8
METHODS:
  RVA=0x03C91E00  token=0x600034B  System.Void Load()
  RVA=0x05396C5C  token=0x600034D  System.Void OnEnterPlayMode()
  RVA=0x041E1670  token=0x600034E  System.Void OnExitPlayMode()
  RVA=0x03C91350  token=0x600034F  System.Void CreateRuntimeDeclarations()
END_CLASS

CLASS: Unity.VisualScripting.InspectorVariableNameAttribute
TYPE:  sealed class
TOKEN: 0x20000BD
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  private           Unity.VisualScripting.ActionDirection<direction>k__BackingField  // 0x10
PROPERTIES:
  direction  set=0x014F51F0
METHODS:
  RVA=0x014F51F0  token=0x6000350  System.Void .ctor(Unity.VisualScripting.ActionDirection direction)
END_CLASS

CLASS: Unity.VisualScripting.SavedVariables
TYPE:  static class
TOKEN: 0x20000BE
SIZE:  0x10
FIELDS:
  private   static  Unity.VisualScripting.VariablesAsset_asset  // static @ 0x0
  private   static  Unity.VisualScripting.VariableDeclarations<saved>k__BackingField  // static @ 0x8
  private   static  Unity.VisualScripting.VariableDeclarations<merged>k__BackingField  // static @ 0x10
PROPERTIES:
  asset  get=0x03C91CA0
  initial  get=0x03C91C00
  saved  get=0x0B364F24  set=0x0B364FB0
  merged  get=0x0B364EEC  set=0x0B364F5C
METHODS:
  RVA=0x03C91E90  token=0x6000353  System.Void Load()
  RVA=0x03C911E0  token=0x6000354  System.Void OnEnterPlayMode()
  RVA=0x0B364D44  token=0x6000355  System.Void OnExitPlayMode()
  RVA=0x0B364D5C  token=0x600035B  System.Void SaveDeclarations(Unity.VisualScripting.VariableDeclarations declarations)
  RVA=0x03C92510  token=0x600035C  System.Void FetchSavedDeclarations()
  RVA=0x03C91410  token=0x600035D  System.Void MergeInitialAndSavedDeclarations()
  RVA=0x04727250  token=0x600035E  System.Void WarnAndNullifyUnityObjectReferences(Unity.VisualScripting.VariableDeclarations declarations)
END_CLASS

CLASS: Unity.VisualScripting.VariableDeclaration
TYPE:  sealed class
TOKEN: 0x20000C0
SIZE:  0x20
FIELDS:
  private           System.String                   <name>k__BackingField  // 0x10
  private           System.Object                   <value>k__BackingField  // 0x18
PROPERTIES:
  name  get=0x02B2ECC0  set=0x053908C0
  value  get=0x0385B100  set=0x05392C40
METHODS:
  RVA=0x032AF420  token=0x6000363  System.Void .ctor(System.String name, System.Object value)
END_CLASS

CLASS: Unity.VisualScripting.VariableDeclarationCollection
TYPE:  sealed class
TOKEN: 0x20000C1
SIZE:  0x30
EXTENDS: System.Collections.ObjectModel.KeyedCollection`2
IMPLEMENTS: Unity.VisualScripting.IKeyedCollection`2 System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
METHODS:
  RVA=0x06C08920  token=0x6000368  System.String GetKeyForItem(Unity.VisualScripting.VariableDeclaration item)
  RVA=0x0B36CBC0  token=0x6000369  System.Boolean TryGetValue(System.String key, Unity.VisualScripting.VariableDeclaration& value)
  RVA=0x03C93A80  token=0x600036A  System.Void .ctor()
  RVA=0x0B36CC80  token=0x600036B  Unity.VisualScripting.VariableDeclaration Unity.VisualScripting.IKeyedCollection<System.String,Unity.VisualScripting.VariableDeclaration>.get_Item(System.String key)
  RVA=0x0B36CC38  token=0x600036C  System.Boolean Unity.VisualScripting.IKeyedCollection<System.String,Unity.VisualScripting.VariableDeclaration>.Contains(System.String key)
END_CLASS

CLASS: Unity.VisualScripting.VariableDeclarations
TYPE:  sealed class
TOKEN: 0x20000C2
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable Unity.VisualScripting.ISpecifiesCloner
FIELDS:
  public            Unity.VisualScripting.VariableKindKind  // 0x10
  private           Unity.VisualScripting.VariableDeclarationCollectioncollection  // 0x18
  private           System.Action                   OnVariableChanged  // 0x20
PROPERTIES:
  Item  get=0x0B36D0FC  set=0x0B36D104
  Unity.VisualScripting.ISpecifiesCloner.cloner  get=0x0B36D0AC
METHODS:
  RVA=0x03C93A10  token=0x600036D  System.Void .ctor()
  RVA=0x0B36CF68  token=0x6000370  System.Void Set(System.String variable, System.Object value)
  RVA=0x0B36CD74  token=0x6000371  System.Object Get(System.String variable)
  RVA=0x0B36CD2C  token=0x6000372  System.Void Clear()
  RVA=0x0B36CEA8  token=0x6000373  System.Boolean IsDefined(System.String variable)
  RVA=0x04727730  token=0x6000374  System.Collections.Generic.IEnumerator<Unity.VisualScripting.VariableDeclaration> GetEnumerator()
  RVA=0x0B36D064  token=0x6000375  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Unity.VisualScripting.VariableDeclarationsCloner
TYPE:  sealed class
TOKEN: 0x20000C3
SIZE:  0x10
EXTENDS: Unity.VisualScripting.Cloner`1
FIELDS:
  public    static readonly Unity.VisualScripting.VariableDeclarationsClonerinstance  // static @ 0x0
METHODS:
  RVA=0x0B36CCC8  token=0x6000377  System.Boolean Handles(System.Type type)
  RVA=0x03C93950  token=0x6000378  Unity.VisualScripting.VariableDeclarations ConstructClone(System.Type type, Unity.VisualScripting.VariableDeclarations original)
  RVA=0x04727540  token=0x6000379  System.Void FillClone(System.Type type, Unity.VisualScripting.VariableDeclarations& clone, Unity.VisualScripting.VariableDeclarations original, Unity.VisualScripting.CloningContext context)
  RVA=0x04D19370  token=0x600037A  System.Void .ctor()
  RVA=0x04D19300  token=0x600037B  System.Void .cctor()
END_CLASS

CLASS: Unity.VisualScripting.VariableKind
TYPE:  sealed struct
TOKEN: 0x20000C4
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Unity.VisualScripting.VariableKindFlow  // const
  public    static  Unity.VisualScripting.VariableKindGraph  // const
  public    static  Unity.VisualScripting.VariableKindObject  // const
  public    static  Unity.VisualScripting.VariableKindScene  // const
  public    static  Unity.VisualScripting.VariableKindApplication  // const
  public    static  Unity.VisualScripting.VariableKindSaved  // const
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.VariablesAsset
TYPE:  sealed class
TOKEN: 0x20000C5
SIZE:  0x38
EXTENDS: Unity.VisualScripting.LudiqScriptableObject
FIELDS:
  private           Unity.VisualScripting.VariableDeclarations<declarations>k__BackingField  // 0x30
PROPERTIES:
  declarations  get=0x011F36E0  set=0x05396200
METHODS:
  RVA=0x0B36D10C  token=0x600037E  System.Void ShowData()
  RVA=0x03C939A0  token=0x600037F  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.VariablesSaver
TYPE:  class
TOKEN: 0x20000C6
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: Unity.VisualScripting.ISingleton
FIELDS:
PROPERTIES:
  instance  get=0x0B36D224
METHODS:
  RVA=0x0B36D114  token=0x6000380  System.Void Awake()
  RVA=0x0B36D1D0  token=0x6000381  System.Void OnDestroy()
  RVA=0x0B36D1C8  token=0x6000382  System.Void OnApplicationQuit()
  RVA=0x0B36D1BC  token=0x6000383  System.Void OnApplicationPause(System.Boolean isPaused)
  RVA=0x0B36D168  token=0x6000385  System.Void Instantiate()
  RVA=0x05393520  token=0x6000386  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsArrayConverter
TYPE:  class
TOKEN: 0x20000C7
SIZE:  0x18
EXTENDS: Unity.VisualScripting.FullSerializer.fsConverter
FIELDS:
METHODS:
  RVA=0x0B352868  token=0x6000387  System.Boolean CanProcess(System.Type type)
  RVA=0x012081B0  token=0x6000388  System.Boolean RequestCycleSupport(System.Type storageType)
  RVA=0x012081B0  token=0x6000389  System.Boolean RequestInheritanceSupport(System.Type storageType)
  RVA=0x0B36D718  token=0x600038A  Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType)
  RVA=0x0B36D4B0  token=0x600038B  Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance, System.Type storageType)
  RVA=0x0B36D43C  token=0x600038C  System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType)
  RVA=0x041E1670  token=0x600038D  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsDateConverter
TYPE:  class
TOKEN: 0x20000C8
SIZE:  0x18
EXTENDS: Unity.VisualScripting.FullSerializer.fsConverter
FIELDS:
PROPERTIES:
  DateTimeFormatString  get=0x0B370498
METHODS:
  RVA=0x0B36FC48  token=0x600038F  System.Boolean CanProcess(System.Type type)
  RVA=0x0B370290  token=0x6000390  Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType)
  RVA=0x0B36FD0C  token=0x6000391  Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance, System.Type storageType)
  RVA=0x041E1670  token=0x6000392  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsDictionaryConverter
TYPE:  class
TOKEN: 0x20000C9
SIZE:  0x18
EXTENDS: Unity.VisualScripting.FullSerializer.fsConverter
FIELDS:
METHODS:
  RVA=0x0B37078C  token=0x6000393  System.Boolean CanProcess(System.Type type)
  RVA=0x0B370804  token=0x6000394  System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType)
  RVA=0x0B370994  token=0x6000395  Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance_, System.Type storageType)
  RVA=0x0B371198  token=0x6000396  Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance_, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType)
  RVA=0x0B3704EC  token=0x6000397  Unity.VisualScripting.FullSerializer.fsResult AddItemToDictionary(System.Collections.IDictionary dictionary, System.Object key, System.Object value)
  RVA=0x0B370878  token=0x6000398  System.Void GetKeyValueTypes(System.Type dictionaryType, System.Type& keyStorageType, System.Type& valueStorageType)
  RVA=0x041E1670  token=0x6000399  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsEnumConverter
TYPE:  class
TOKEN: 0x20000CA
SIZE:  0x18
EXTENDS: Unity.VisualScripting.FullSerializer.fsConverter
FIELDS:
METHODS:
  RVA=0x0B371838  token=0x600039A  System.Boolean CanProcess(System.Type type)
  RVA=0x012081B0  token=0x600039B  System.Boolean RequestCycleSupport(System.Type storageType)
  RVA=0x012081B0  token=0x600039C  System.Boolean RequestInheritanceSupport(System.Type storageType)
  RVA=0x0B371890  token=0x600039D  System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType)
  RVA=0x0B372070  token=0x600039E  Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType)
  RVA=0x0B371904  token=0x600039F  Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance, System.Type storageType)
  RVA=-1  // generic def  token=0x60003A0  System.Boolean ArrayContains(T[] values, T value)
  RVA=0x041E1670  token=0x60003A1  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsForwardAttribute
TYPE:  sealed class
TOKEN: 0x20000CD
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public            System.String                   MemberName  // 0x10
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsForwardConverter
TYPE:  class
TOKEN: 0x20000CE
SIZE:  0x20
EXTENDS: Unity.VisualScripting.FullSerializer.fsConverter
FIELDS:
  private           System.String                   _memberName  // 0x18
METHODS:
  RVA=0x0A9A6604  token=0x60003A9  System.Void .ctor(Unity.VisualScripting.FullSerializer.fsForwardAttribute attribute)
  RVA=0x0B372544  token=0x60003AA  System.Boolean CanProcess(System.Type type)
  RVA=0x0B372614  token=0x60003AB  Unity.VisualScripting.FullSerializer.fsResult GetProperty(System.Object instance, Unity.VisualScripting.FullSerializer.fsMetaProperty& property)
  RVA=0x0B372960  token=0x60003AC  Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType)
  RVA=0x0B3727DC  token=0x60003AD  Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance, System.Type storageType)
  RVA=0x0B3725A0  token=0x60003AE  System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType)
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsGuidConverter
TYPE:  class
TOKEN: 0x20000CF
SIZE:  0x18
EXTENDS: Unity.VisualScripting.FullSerializer.fsConverter
FIELDS:
METHODS:
  RVA=0x0B372B40  token=0x60003AF  System.Boolean CanProcess(System.Type type)
  RVA=0x012081B0  token=0x60003B0  System.Boolean RequestCycleSupport(System.Type storageType)
  RVA=0x012081B0  token=0x60003B1  System.Boolean RequestInheritanceSupport(System.Type storageType)
  RVA=0x0B372CEC  token=0x60003B2  Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType)
  RVA=0x0B372BE4  token=0x60003B3  Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance, System.Type storageType)
  RVA=0x0B372BA4  token=0x60003B4  System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType)
  RVA=0x041E1670  token=0x60003B5  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsIEnumerableConverter
TYPE:  class
TOKEN: 0x20000D0
SIZE:  0x18
EXTENDS: Unity.VisualScripting.FullSerializer.fsConverter
FIELDS:
METHODS:
  RVA=0x0B372DDC  token=0x60003B6  System.Boolean CanProcess(System.Type type)
  RVA=0x0B372E74  token=0x60003B7  System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType)
  RVA=0x0B3735BC  token=0x60003B8  Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance_, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType)
  RVA=0x0B37318C  token=0x60003B9  System.Boolean IsStack(System.Type type)
  RVA=0x0B3732D8  token=0x60003BA  Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance_, System.Type storageType)
  RVA=0x0B373114  token=0x60003BB  System.Int32 HintSize(System.Collections.IEnumerable collection)
  RVA=0x0B373028  token=0x60003BC  System.Type GetElementType(System.Type objectType)
  RVA=0x0B373244  token=0x60003BD  System.Void TryClear(System.Type type, System.Object instance)
  RVA=0x0B372EE8  token=0x60003BE  System.Reflection.MethodInfo GetAddMethod(System.Type type)
  RVA=0x041E1670  token=0x60003BF  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsKeyValuePairConverter
TYPE:  class
TOKEN: 0x20000D1
SIZE:  0x18
EXTENDS: Unity.VisualScripting.FullSerializer.fsConverter
FIELDS:
METHODS:
  RVA=0x0B376DB0  token=0x60003C0  System.Boolean CanProcess(System.Type type)
  RVA=0x012081B0  token=0x60003C1  System.Boolean RequestCycleSupport(System.Type storageType)
  RVA=0x012081B0  token=0x60003C2  System.Boolean RequestInheritanceSupport(System.Type storageType)
  RVA=0x0B376E5C  token=0x60003C3  Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance, System.Type storageType)
  RVA=0x0B377250  token=0x60003C4  Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType)
  RVA=0x041E1670  token=0x60003C5  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsNullableConverter
TYPE:  class
TOKEN: 0x20000D2
SIZE:  0x18
EXTENDS: Unity.VisualScripting.FullSerializer.fsConverter
FIELDS:
METHODS:
  RVA=0x0B379524  token=0x60003C6  System.Boolean CanProcess(System.Type type)
  RVA=0x0B379648  token=0x60003C7  Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType)
  RVA=0x0B3795D0  token=0x60003C8  Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance, System.Type storageType)
  RVA=0x04D8C7E0  token=0x60003C9  System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType)
  RVA=0x041E1670  token=0x60003CA  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsPrimitiveConverter
TYPE:  class
TOKEN: 0x20000D3
SIZE:  0x18
EXTENDS: Unity.VisualScripting.FullSerializer.fsConverter
FIELDS:
METHODS:
  RVA=0x0B379F4C  token=0x60003CB  System.Boolean CanProcess(System.Type type)
  RVA=0x012081B0  token=0x60003CC  System.Boolean RequestCycleSupport(System.Type storageType)
  RVA=0x012081B0  token=0x60003CD  System.Boolean RequestInheritanceSupport(System.Type storageType)
  RVA=0x0B37A5B8  token=0x60003CE  Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType)
  RVA=0x0B37A014  token=0x60003CF  Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData storage, System.Object& instance, System.Type storageType)
  RVA=0x0B37AB64  token=0x60003D0  System.Boolean UseBool(System.Type type)
  RVA=0x0B37AC8C  token=0x60003D1  System.Boolean UseInt64(System.Type type)
  RVA=0x0B37ABC8  token=0x60003D2  System.Boolean UseDouble(System.Type type)
  RVA=0x0B37AE48  token=0x60003D3  System.Boolean UseString(System.Type type)
  RVA=0x041E1670  token=0x60003D4  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsReflectedConverter
TYPE:  class
TOKEN: 0x20000D4
SIZE:  0x18
EXTENDS: Unity.VisualScripting.FullSerializer.fsConverter
FIELDS:
METHODS:
  RVA=0x0B37AEE0  token=0x60003D5  System.Boolean CanProcess(System.Type type)
  RVA=0x0B37B260  token=0x60003D6  Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType)
  RVA=0x0B37B000  token=0x60003D7  Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance, System.Type storageType)
  RVA=0x0B37AF8C  token=0x60003D8  System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType)
  RVA=0x041E1670  token=0x60003D9  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsTypeConverter
TYPE:  class
TOKEN: 0x20000D5
SIZE:  0x18
EXTENDS: Unity.VisualScripting.FullSerializer.fsConverter
FIELDS:
METHODS:
  RVA=0x0B3815DC  token=0x60003DA  System.Boolean CanProcess(System.Type type)
  RVA=0x012081B0  token=0x60003DB  System.Boolean RequestCycleSupport(System.Type type)
  RVA=0x012081B0  token=0x60003DC  System.Boolean RequestInheritanceSupport(System.Type type)
  RVA=0x0B3817D0  token=0x60003DD  Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType)
  RVA=0x0B381654  token=0x60003DE  Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance, System.Type storageType)
  RVA=0x04D8C7E0  token=0x60003DF  System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType)
  RVA=0x041E1670  token=0x60003E0  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsWeakReferenceConverter
TYPE:  class
TOKEN: 0x20000D6
SIZE:  0x18
EXTENDS: Unity.VisualScripting.FullSerializer.fsConverter
FIELDS:
METHODS:
  RVA=0x0B382CDC  token=0x60003E1  System.Boolean CanProcess(System.Type type)
  RVA=0x012081B0  token=0x60003E2  System.Boolean RequestCycleSupport(System.Type storageType)
  RVA=0x012081B0  token=0x60003E3  System.Boolean RequestInheritanceSupport(System.Type storageType)
  RVA=0x0B3830B8  token=0x60003E4  Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType)
  RVA=0x0B382D98  token=0x60003E5  Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance, System.Type storageType)
  RVA=0x0B382D40  token=0x60003E6  System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType)
  RVA=0x041E1670  token=0x60003E7  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsConverterRegistrar
TYPE:  class
TOKEN: 0x20000D7
SIZE:  0x10
FIELDS:
  public    static  System.Collections.Generic.List<System.Type>Converters  // static @ 0x0
METHODS:
  RVA=0x0B36E330  token=0x60003E8  System.Void .cctor()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsAotCompilationManager
TYPE:  class
TOKEN: 0x20000D8
SIZE:  0x10
FIELDS:
  private   static  System.Collections.Generic.Dictionary<System.Type,System.String>_computedAotCompilations  // static @ 0x0
  private   static  System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsAotCompilationManager.AotCompilation>_uncomputedAotCompilations  // static @ 0x8
METHODS:
  RVA=0x0B36D278  token=0x60003E9  System.Void AddAotCompilation(System.Type type, Unity.VisualScripting.FullSerializer.fsMetaProperty[] members, System.Boolean isConstructorPublic)
  RVA=0x0B36D348  token=0x60003EA  System.Void .cctor()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsBaseConverter
TYPE:  abstract class
TOKEN: 0x20000DA
SIZE:  0x18
FIELDS:
  public            Unity.VisualScripting.FullSerializer.fsSerializerSerializer  // 0x10
METHODS:
  RVA=0x0B36DD20  token=0x60003EB  System.Object CreateInstance(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType)
  RVA=0x0B36E1B4  token=0x60003EC  System.Boolean RequestCycleSupport(System.Type storageType)
  RVA=0x0B36E268  token=0x60003ED  System.Boolean RequestInheritanceSupport(System.Type storageType)
  RVA=-1  // abstract  token=0x60003EE  Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType)
  RVA=-1  // abstract  token=0x60003EF  Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance, System.Type storageType)
  RVA=0x0B36DE74  token=0x60003F0  Unity.VisualScripting.FullSerializer.fsResult FailExpectedType(Unity.VisualScripting.FullSerializer.fsData data, Unity.VisualScripting.FullSerializer.fsDataType[] types)
  RVA=0x0B36DB08  token=0x60003F1  Unity.VisualScripting.FullSerializer.fsResult CheckType(Unity.VisualScripting.FullSerializer.fsData data, Unity.VisualScripting.FullSerializer.fsDataType type)
  RVA=0x0B36D8F4  token=0x60003F2  Unity.VisualScripting.FullSerializer.fsResult CheckKey(Unity.VisualScripting.FullSerializer.fsData data, System.String key, Unity.VisualScripting.FullSerializer.fsData& subitem)
  RVA=0x0B36D960  token=0x60003F3  Unity.VisualScripting.FullSerializer.fsResult CheckKey(System.Collections.Generic.Dictionary<System.String,Unity.VisualScripting.FullSerializer.fsData> data, System.String key, Unity.VisualScripting.FullSerializer.fsData& subitem)
  RVA=-1  // generic def  token=0x60003F4  Unity.VisualScripting.FullSerializer.fsResult SerializeMember(System.Collections.Generic.Dictionary<System.String,Unity.VisualScripting.FullSerializer.fsData> data, System.Type overrideConverterType, System.String name, T value)
  RVA=-1  // generic def  token=0x60003F5  Unity.VisualScripting.FullSerializer.fsResult DeserializeMember(System.Collections.Generic.Dictionary<System.String,Unity.VisualScripting.FullSerializer.fsData> data, System.Type overrideConverterType, System.String name, T& value)
  RVA=0x041E1670  token=0x60003F6  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsGlobalConfig
TYPE:  static class
TOKEN: 0x20000DC
SIZE:  0x10
FIELDS:
  public    static  System.Boolean                  IsCaseSensitive  // static @ 0x0
  public    static  System.Boolean                  AllowInternalExceptions  // static @ 0x1
  public    static  System.String                   InternalFieldPrefix  // static @ 0x8
METHODS:
  RVA=0x0B372AC0  token=0x60003FA  System.Void .cctor()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsConfig
TYPE:  class
TOKEN: 0x20000DD
SIZE:  0x48
FIELDS:
  public            System.Type[]                   SerializeAttributes  // 0x10
  public            System.Type[]                   IgnoreSerializeAttributes  // 0x18
  public            Unity.VisualScripting.FullSerializer.fsMemberSerializationDefaultMemberSerialization  // 0x20
  public            System.Func<System.String,System.Reflection.MemberInfo,System.String>GetJsonNameFromMemberName  // 0x28
  public            System.Boolean                  EnablePropertySerialization  // 0x30
  public            System.Boolean                  SerializeNonAutoProperties  // 0x31
  public            System.Boolean                  SerializeNonPublicSetProperties  // 0x32
  public            System.String                   CustomDateTimeFormatString  // 0x38
  public            System.Boolean                  Serialize64BitIntegerAsString  // 0x40
  public            System.Boolean                  SerializeEnumsAsInteger  // 0x41
METHODS:
  RVA=0x045BFF30  token=0x60003FB  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsContext
TYPE:  sealed class
TOKEN: 0x20000DF
SIZE:  0x18
FIELDS:
  private   readonly System.Collections.Generic.Dictionary<System.Type,System.Object>_contextObjects  // 0x10
METHODS:
  RVA=-1  // generic def  token=0x60003FF  System.Void Set(T obj)
  RVA=-1  // generic def  token=0x6000400  T Get()
  RVA=0x0B36E2B8  token=0x6000401  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsConverter
TYPE:  abstract class
TOKEN: 0x20000E0
SIZE:  0x18
EXTENDS: Unity.VisualScripting.FullSerializer.fsBaseConverter
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000402  System.Boolean CanProcess(System.Type type)
  RVA=0x041E1670  token=0x6000403  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsDataType
TYPE:  sealed struct
TOKEN: 0x20000E1
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Unity.VisualScripting.FullSerializer.fsDataTypeArray  // const
  public    static  Unity.VisualScripting.FullSerializer.fsDataTypeObject  // const
  public    static  Unity.VisualScripting.FullSerializer.fsDataTypeDouble  // const
  public    static  Unity.VisualScripting.FullSerializer.fsDataTypeInt64  // const
  public    static  Unity.VisualScripting.FullSerializer.fsDataTypeBoolean  // const
  public    static  Unity.VisualScripting.FullSerializer.fsDataTypeString  // const
  public    static  Unity.VisualScripting.FullSerializer.fsDataTypeNull  // const
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsData
TYPE:  sealed class
TOKEN: 0x20000E2
SIZE:  0x18
FIELDS:
  private           System.Object                   _value  // 0x10
  public    static readonly Unity.VisualScripting.FullSerializer.fsDataTrue  // static @ 0x0
  public    static readonly Unity.VisualScripting.FullSerializer.fsDataFalse  // static @ 0x8
  public    static readonly Unity.VisualScripting.FullSerializer.fsDataNull  // static @ 0x10
PROPERTIES:
  Type  get=0x0B36F9C8
  IsNull  get=0x03BA32C0
  IsDouble  get=0x0B36F8A0
  IsInt64  get=0x0B36F8EC
  IsBool  get=0x0B36F810
  IsString  get=0x0B36F97C
  IsDictionary  get=0x0B36F85C
  IsList  get=0x0B36F938
  AsDouble  get=0x0B36F720
  AsInt64  get=0x0B36F75C
  AsBool  get=0x0B36F6A8
  AsString  get=0x0B36F7D4
  AsDictionary  get=0x0B36F6E4
  AsList  get=0x0B36F798
METHODS:
  RVA=0x0B36F480  token=0x6000404  System.String ToString()
  RVA=0x040E20A0  token=0x6000405  System.Void .ctor()
  RVA=0x0B36F594  token=0x6000406  System.Void .ctor(System.Boolean boolean)
  RVA=0x0B36F5F0  token=0x6000407  System.Void .ctor(System.Double f)
  RVA=0x0B36F64C  token=0x6000408  System.Void .ctor(System.Int64 i)
  RVA=0x053908C0  token=0x6000409  System.Void .ctor(System.String str)
  RVA=0x053908C0  token=0x600040A  System.Void .ctor(System.Collections.Generic.Dictionary<System.String,Unity.VisualScripting.FullSerializer.fsData> dict)
  RVA=0x053908C0  token=0x600040B  System.Void .ctor(System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsData> list)
  RVA=0x0B36ED34  token=0x600040C  Unity.VisualScripting.FullSerializer.fsData CreateDictionary()
  RVA=0x0B36EE28  token=0x600040D  Unity.VisualScripting.FullSerializer.fsData CreateList(System.Int32 capacity)
  RVA=0x0B36EC50  token=0x600040E  System.Void BecomeDictionary()
  RVA=0x0B36ECC8  token=0x600040F  Unity.VisualScripting.FullSerializer.fsData Clone()
  RVA=-1  // generic def  token=0x600041E  T Cast()
  RVA=0x0B36EEC4  token=0x600041F  System.Boolean Equals(System.Object obj)
  RVA=0x0B36EF28  token=0x6000420  System.Boolean Equals(Unity.VisualScripting.FullSerializer.fsData other)
  RVA=0x0B36FB24  token=0x6000421  System.Boolean op_Equality(Unity.VisualScripting.FullSerializer.fsData a, Unity.VisualScripting.FullSerializer.fsData b)
  RVA=0x0B36FBF4  token=0x6000422  System.Boolean op_Inequality(Unity.VisualScripting.FullSerializer.fsData a, Unity.VisualScripting.FullSerializer.fsData b)
  RVA=0x08479504  token=0x6000423  System.Int32 GetHashCode()
  RVA=0x0B36F488  token=0x6000424  System.Void .cctor()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsDirectConverter
TYPE:  abstract class
TOKEN: 0x20000E3
SIZE:  0x18
EXTENDS: Unity.VisualScripting.FullSerializer.fsBaseConverter
FIELDS:
PROPERTIES:
  ModelType  get=-1  // abstract
METHODS:
  RVA=0x041E1670  token=0x6000426  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsDirectConverter`1
TYPE:  abstract class
TOKEN: 0x20000E4
EXTENDS: Unity.VisualScripting.FullSerializer.fsDirectConverter
FIELDS:
PROPERTIES:
  ModelType  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000428  Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Object instance, Unity.VisualScripting.FullSerializer.fsData& serialized, System.Type storageType)
  RVA=-1  // not resolved  token=0x6000429  Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Object& instance, System.Type storageType)
  RVA=-1  // abstract  token=0x600042A  Unity.VisualScripting.FullSerializer.fsResult DoSerialize(TModel model, System.Collections.Generic.Dictionary<System.String,Unity.VisualScripting.FullSerializer.fsData> serialized)
  RVA=-1  // abstract  token=0x600042B  Unity.VisualScripting.FullSerializer.fsResult DoDeserialize(System.Collections.Generic.Dictionary<System.String,Unity.VisualScripting.FullSerializer.fsData> data, TModel& model)
  RVA=-1  // not resolved  token=0x600042C  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsMissingVersionConstructorException
TYPE:  sealed class
TOKEN: 0x20000E5
SIZE:  0x90
EXTENDS: System.Exception
FIELDS:
METHODS:
  RVA=0x0B379458  token=0x600042D  System.Void .ctor(System.Type versionedType, System.Type constructorType)
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsDuplicateVersionNameException
TYPE:  sealed class
TOKEN: 0x20000E6
SIZE:  0x90
EXTENDS: System.Exception
FIELDS:
METHODS:
  RVA=0x0B3716D4  token=0x600042E  System.Void .ctor(System.Type typeA, System.Type typeB, System.String version)
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsIgnoreAttribute
TYPE:  sealed class
TOKEN: 0x20000E7
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsISerializationCallbacks
TYPE:  interface
TOKEN: 0x20000E8
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600042F  System.Void OnBeforeSerialize(System.Type storageType)
  RVA=-1  // abstract  token=0x6000430  System.Void OnAfterSerialize(System.Type storageType, Unity.VisualScripting.FullSerializer.fsData& data)
  RVA=-1  // abstract  token=0x6000431  System.Void OnBeforeDeserialize(System.Type storageType, Unity.VisualScripting.FullSerializer.fsData& data)
  RVA=-1  // abstract  token=0x6000432  System.Void OnAfterDeserialize(System.Type storageType)
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsSerializationCallbackProcessor
TYPE:  class
TOKEN: 0x20000E9
SIZE:  0x10
EXTENDS: Unity.VisualScripting.FullSerializer.fsObjectProcessor
FIELDS:
METHODS:
  RVA=0x0B37BE50  token=0x6000433  System.Boolean CanProcess(System.Type type)
  RVA=0x0B37C150  token=0x6000434  System.Void OnBeforeSerialize(System.Type storageType, System.Object instance)
  RVA=0x0B37BF44  token=0x6000435  System.Void OnAfterSerialize(System.Type storageType, System.Object instance, Unity.VisualScripting.FullSerializer.fsData& data)
  RVA=0x0B37BFD4  token=0x6000436  System.Void OnBeforeDeserializeAfterInstanceCreation(System.Type storageType, System.Object instance, Unity.VisualScripting.FullSerializer.fsData& data)
  RVA=0x0B37BEC8  token=0x6000437  System.Void OnAfterDeserialize(System.Type storageType, System.Object instance)
  RVA=0x041E1670  token=0x6000438  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsSerializationCallbackReceiverProcessor
TYPE:  class
TOKEN: 0x20000EA
SIZE:  0x10
EXTENDS: Unity.VisualScripting.FullSerializer.fsObjectProcessor
FIELDS:
METHODS:
  RVA=0x0B37C1CC  token=0x6000439  System.Boolean CanProcess(System.Type type)
  RVA=0x0B37C2C8  token=0x600043A  System.Void OnBeforeSerialize(System.Type storageType, System.Object instance)
  RVA=0x0B37C244  token=0x600043B  System.Void OnAfterDeserialize(System.Type storageType, System.Object instance)
  RVA=0x041E1670  token=0x600043C  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsJsonParser
TYPE:  class
TOKEN: 0x20000EB
SIZE:  0x28
FIELDS:
  private   readonly System.Text.StringBuilder       _cachedStringBuilder  // 0x10
  private           System.Int32                    _start  // 0x18
  private           System.String                   _input  // 0x20
METHODS:
  RVA=0x0B3758EC  token=0x600043D  System.Void .ctor(System.String input)
  RVA=0x0B373A2C  token=0x600043E  Unity.VisualScripting.FullSerializer.fsResult MakeFailure(System.String message)
  RVA=0x0B3742FC  token=0x600043F  System.Boolean TryMoveNext()
  RVA=0x0B373968  token=0x6000440  System.Boolean HasValue()
  RVA=0x0B373974  token=0x6000441  System.Boolean HasValue(System.Int32 offset)
  RVA=0x0B373930  token=0x6000442  System.Char Character()
  RVA=0x0B37393C  token=0x6000443  System.Char Character(System.Int32 offset)
  RVA=0x0B374120  token=0x6000444  System.Void SkipSpace()
  RVA=0x0B3745E0  token=0x6000445  Unity.VisualScripting.FullSerializer.fsResult TryParseExact(System.String content)
  RVA=0x0B3752F4  token=0x6000446  Unity.VisualScripting.FullSerializer.fsResult TryParseTrue(Unity.VisualScripting.FullSerializer.fsData& data)
  RVA=0x0B374724  token=0x6000447  Unity.VisualScripting.FullSerializer.fsResult TryParseFalse(Unity.VisualScripting.FullSerializer.fsData& data)
  RVA=0x0B374824  token=0x6000448  Unity.VisualScripting.FullSerializer.fsResult TryParseNull(Unity.VisualScripting.FullSerializer.fsData& data)
  RVA=0x0B3739D0  token=0x6000449  System.Boolean IsSeparator(System.Char c)
  RVA=0x0B374920  token=0x600044A  Unity.VisualScripting.FullSerializer.fsResult TryParseNumber(Unity.VisualScripting.FullSerializer.fsData& data)
  RVA=0x0B37503C  token=0x600044B  Unity.VisualScripting.FullSerializer.fsResult TryParseString(System.String& str)
  RVA=0x0B374328  token=0x600044C  Unity.VisualScripting.FullSerializer.fsResult TryParseArray(Unity.VisualScripting.FullSerializer.fsData& arr)
  RVA=0x0B374C34  token=0x600044D  Unity.VisualScripting.FullSerializer.fsResult TryParseObject(Unity.VisualScripting.FullSerializer.fsData& obj)
  RVA=0x0B373DDC  token=0x600044E  Unity.VisualScripting.FullSerializer.fsResult RunParse(Unity.VisualScripting.FullSerializer.fsData& data)
  RVA=0x0B373CF4  token=0x600044F  Unity.VisualScripting.FullSerializer.fsResult Parse(System.String input, Unity.VisualScripting.FullSerializer.fsData& data)
  RVA=0x0B373C80  token=0x6000450  Unity.VisualScripting.FullSerializer.fsData Parse(System.String input)
  RVA=0x0B3739A8  token=0x6000451  System.Boolean IsHex(System.Char c)
  RVA=0x0B373BB8  token=0x6000452  System.UInt32 ParseSingleChar(System.Char c1, System.UInt32 multipliyer)
  RVA=0x0B373BFC  token=0x6000453  System.UInt32 ParseUnicode(System.Char c1, System.Char c2, System.Char c3, System.Char c4)
  RVA=0x0B3753F4  token=0x6000454  Unity.VisualScripting.FullSerializer.fsResult TryUnescapeChar(System.Char& escaped)
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsJsonPrinter
TYPE:  static class
TOKEN: 0x20000EC
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B376BE4  token=0x6000455  System.Void InsertSpacing(System.IO.TextWriter stream, System.Int32 count)
  RVA=0x0B376928  token=0x6000456  System.String EscapeString(System.String str)
  RVA=0x0B375978  token=0x6000457  System.Void BuildCompressedString(Unity.VisualScripting.FullSerializer.fsData data, System.IO.TextWriter stream)
  RVA=0x0B375F50  token=0x6000458  System.Void BuildPrettyString(Unity.VisualScripting.FullSerializer.fsData data, System.IO.TextWriter stream, System.Int32 depth)
  RVA=0x0B376C50  token=0x6000459  System.String PrettyJson(Unity.VisualScripting.FullSerializer.fsData data)
  RVA=0x0B376690  token=0x600045A  System.String CompressedJson(Unity.VisualScripting.FullSerializer.fsData data)
  RVA=0x0B3767EC  token=0x600045B  System.String ConvertDoubleToString(System.Double d)
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsMemberSerialization
TYPE:  sealed struct
TOKEN: 0x20000ED
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  Unity.VisualScripting.FullSerializer.fsMemberSerializationOptIn  // const
  public    static  Unity.VisualScripting.FullSerializer.fsMemberSerializationOptOut  // const
  public    static  Unity.VisualScripting.FullSerializer.fsMemberSerializationDefault  // const
METHODS:
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsObjectAttribute
TYPE:  class
TOKEN: 0x20000EE
SIZE:  0x38
EXTENDS: System.Attribute
FIELDS:
  public            System.Type[]                   PreviousModels  // 0x10
  public            System.String                   VersionString  // 0x18
  public            Unity.VisualScripting.FullSerializer.fsMemberSerializationMemberSerialization  // 0x20
  public            System.Type                     Converter  // 0x28
  public            System.Type                     Processor  // 0x30
METHODS:
  RVA=0x04D91810  token=0x600045C  System.Void .ctor()
  RVA=0x0B3796C0  token=0x600045D  System.Void .ctor(System.String versionString, System.Type[] previousModels)
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsObjectProcessor
TYPE:  abstract class
TOKEN: 0x20000EF
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B3796F0  token=0x600045E  System.Boolean CanProcess(System.Type type)
  RVA=0x041E1670  token=0x600045F  System.Void OnBeforeSerialize(System.Type storageType, System.Object instance)
  RVA=0x041E1670  token=0x6000460  System.Void OnAfterSerialize(System.Type storageType, System.Object instance, Unity.VisualScripting.FullSerializer.fsData& data)
  RVA=0x041E1670  token=0x6000461  System.Void OnBeforeDeserialize(System.Type storageType, Unity.VisualScripting.FullSerializer.fsData& data)
  RVA=0x041E1670  token=0x6000462  System.Void OnBeforeDeserializeAfterInstanceCreation(System.Type storageType, System.Object instance, Unity.VisualScripting.FullSerializer.fsData& data)
  RVA=0x041E1670  token=0x6000463  System.Void OnAfterDeserialize(System.Type storageType, System.Object instance)
  RVA=0x041E1670  token=0x6000464  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsPropertyAttribute
TYPE:  class
TOKEN: 0x20000F0
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  public            System.String                   Name  // 0x10
  public            System.Type                     Converter  // 0x18
METHODS:
  RVA=0x053908C0  token=0x6000465  System.Void .ctor(System.String name)
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsResult
TYPE:  sealed struct
TOKEN: 0x20000F1
SIZE:  0x20
FIELDS:
  private   static readonly System.String[]                 EmptyStringArray  // static @ 0x0
  private           System.Boolean                  _success  // 0x10
  private           System.Collections.Generic.List<System.String>_messages  // 0x18
  public    static  Unity.VisualScripting.FullSerializer.fsResultSuccess  // static @ 0x8
PROPERTIES:
  Failed  get=0x0961F480
  Succeeded  get=0x04D883E0
  HasWarnings  get=0x0B37BD34
  AsException  get=0x0B37BB3C
  RawMessages  get=0x0B37BD80
  FormattedMessages  get=0x0B37BC54
METHODS:
  RVA=0x0B37B668  token=0x6000466  System.Void AddMessage(System.String message)
  RVA=0x0B37B718  token=0x6000467  System.Void AddMessages(Unity.VisualScripting.FullSerializer.fsResult result)
  RVA=0x0B37B908  token=0x6000468  Unity.VisualScripting.FullSerializer.fsResult Merge(Unity.VisualScripting.FullSerializer.fsResult other)
  RVA=0x0B37B9EC  token=0x6000469  Unity.VisualScripting.FullSerializer.fsResult Warn(System.String warning)
  RVA=0x0B37B85C  token=0x600046A  Unity.VisualScripting.FullSerializer.fsResult Fail(System.String warning)
  RVA=0x0B37BDD8  token=0x600046B  Unity.VisualScripting.FullSerializer.fsResult op_Addition(Unity.VisualScripting.FullSerializer.fsResult a, Unity.VisualScripting.FullSerializer.fsResult b)
  RVA=0x0B37B7D0  token=0x600046F  Unity.VisualScripting.FullSerializer.fsResult AssertSuccess()
  RVA=0x0B37BA9C  token=0x6000473  System.Void .cctor()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsSerializer
TYPE:  class
TOKEN: 0x20000F2
SIZE:  0x68
FIELDS:
  private   readonly System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsConverter>_availableConverters  // 0x10
  private   readonly System.Collections.Generic.Dictionary<System.Type,Unity.VisualScripting.FullSerializer.fsDirectConverter>_availableDirectConverters  // 0x18
  private   readonly System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsObjectProcessor>_processors  // 0x20
  private   readonly Unity.VisualScripting.FullSerializer.Internal.fsCyclicReferenceManager_references  // 0x28
  private   readonly Unity.VisualScripting.FullSerializer.fsSerializer.fsLazyCycleDefinitionWriter_lazyReferenceWriter  // 0x30
  private   readonly System.Collections.Generic.Dictionary<System.Type,System.Type>_abstractTypeRemap  // 0x38
  private           System.Collections.Generic.Dictionary<System.Type,Unity.VisualScripting.FullSerializer.fsBaseConverter>_cachedConverterTypeInstances  // 0x40
  private           System.Collections.Generic.Dictionary<System.Type,Unity.VisualScripting.FullSerializer.fsBaseConverter>_cachedConverters  // 0x48
  private           System.Collections.Generic.Dictionary<System.Type,System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsObjectProcessor>>_cachedProcessors  // 0x50
  public            Unity.VisualScripting.FullSerializer.fsContextContext  // 0x58
  public            Unity.VisualScripting.FullSerializer.fsConfigConfig  // 0x60
  private   static  System.Collections.Generic.HashSet<System.String>_reservedKeywords  // static @ 0x0
  private   static readonly System.String                   Key_ObjectReference  // static @ 0x8
  private   static readonly System.String                   Key_ObjectDefinition  // static @ 0x10
  private   static readonly System.String                   Key_InstanceType  // static @ 0x18
  private   static readonly System.String                   Key_Version  // static @ 0x20
  private   static readonly System.String                   Key_Content  // static @ 0x28
  private   static readonly System.String                   Key_UnitDefault  // static @ 0x30
  private   static readonly System.String                   Key_UnitPosition  // static @ 0x38
  private   static readonly System.String                   Key_UnitGuid  // static @ 0x40
  private   static readonly System.String                   Key_UnitFormerType  // static @ 0x48
  private   static readonly System.String                   Key_UnitFormerValue  // static @ 0x50
  private   static readonly System.String                   TypeName_Unit  // static @ 0x58
  private   static readonly System.Type                     Type_Unit  // static @ 0x60
  private   static readonly System.String                   TypeName_MissingType  // static @ 0x68
  private   static readonly System.Type                     Type_MissingType  // static @ 0x70
METHODS:
  RVA=0x0B380B84  token=0x6000474  System.Void .ctor()
  RVA=0x0B37FC30  token=0x6000475  System.Void RemapAbstractStorageTypeToDefaultType(System.Type& storageType)
  RVA=0x0B37FDC0  token=0x6000476  System.Void SetDefaultStorageType(System.Type abstractType, System.Type defaultStorageType)
  RVA=0x0B37DB94  token=0x6000477  System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsObjectProcessor> GetProcessors(System.Type type)
  RVA=0x0B37C34C  token=0x6000478  System.Void AddConverter(Unity.VisualScripting.FullSerializer.fsBaseConverter converter)
  RVA=0x0B37C9A8  token=0x6000479  Unity.VisualScripting.FullSerializer.fsBaseConverter GetConverter(System.Type type, System.Type overrideConverterType)
  RVA=-1  // generic def  token=0x600047A  Unity.VisualScripting.FullSerializer.fsResult TrySerialize(T instance, Unity.VisualScripting.FullSerializer.fsData& data)
  RVA=-1  // generic def  token=0x600047B  Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, T& instance)
  RVA=0x0B3805F8  token=0x600047C  Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Type storageType, System.Object instance, Unity.VisualScripting.FullSerializer.fsData& data)
  RVA=0x0B38025C  token=0x600047D  Unity.VisualScripting.FullSerializer.fsResult TrySerialize(System.Type storageType, System.Type overrideConverterType, System.Object instance, Unity.VisualScripting.FullSerializer.fsData& data)
  RVA=0x0B37EAC0  token=0x600047E  Unity.VisualScripting.FullSerializer.fsResult InternalSerialize_1_ProcessCycles(System.Type storageType, System.Type overrideConverterType, System.Object instance, Unity.VisualScripting.FullSerializer.fsData& data)
  RVA=0x0B37EE7C  token=0x600047F  Unity.VisualScripting.FullSerializer.fsResult InternalSerialize_2_Inheritance(System.Type storageType, System.Type overrideConverterType, System.Object instance, Unity.VisualScripting.FullSerializer.fsData& data)
  RVA=0x0B37F118  token=0x6000480  Unity.VisualScripting.FullSerializer.fsResult InternalSerialize_3_ProcessVersioning(System.Type overrideConverterType, System.Object instance, Unity.VisualScripting.FullSerializer.fsData& data)
  RVA=0x0B37F328  token=0x6000481  Unity.VisualScripting.FullSerializer.fsResult InternalSerialize_4_Converter(System.Type overrideConverterType, System.Object instance, Unity.VisualScripting.FullSerializer.fsData& data)
  RVA=0x0B380220  token=0x6000482  Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType, System.Object& result)
  RVA=0x0B37FEBC  token=0x6000483  Unity.VisualScripting.FullSerializer.fsResult TryDeserialize(Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType, System.Type overrideConverterType, System.Object& result)
  RVA=0x0B37DDF0  token=0x6000484  Unity.VisualScripting.FullSerializer.fsResult InternalDeserialize_1_CycleReference(System.Type overrideConverterType, Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType, System.Object& result, System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsObjectProcessor>& processors)
  RVA=0x0B37DFA4  token=0x6000485  Unity.VisualScripting.FullSerializer.fsResult InternalDeserialize_2_Version(System.Type overrideConverterType, Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType, System.Object& result, System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsObjectProcessor>& processors)
  RVA=0x0B37E418  token=0x6000486  Unity.VisualScripting.FullSerializer.fsResult InternalDeserialize_3_Inheritance(System.Type overrideConverterType, Unity.VisualScripting.FullSerializer.fsData data, System.Type storageType, System.Object& result, System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsObjectProcessor>& processors)
  RVA=0x0B37E850  token=0x6000487  Unity.VisualScripting.FullSerializer.fsResult InternalDeserialize_4_Cycles(System.Type overrideConverterType, Unity.VisualScripting.FullSerializer.fsData data, System.Type resultType, System.Object& result)
  RVA=0x0B37E990  token=0x6000488  Unity.VisualScripting.FullSerializer.fsResult InternalDeserialize_5_Converter(System.Type overrideConverterType, Unity.VisualScripting.FullSerializer.fsData data, System.Type resultType, System.Object& result)
  RVA=0x0B37CD98  token=0x6000489  System.Type GetDataType(Unity.VisualScripting.FullSerializer.fsData& data, System.Type defaultType, Unity.VisualScripting.FullSerializer.fsResult& deserializeResult)
  RVA=0x0B37C8F4  token=0x600048A  System.Void EnsureDictionary(Unity.VisualScripting.FullSerializer.fsData data)
  RVA=0x0B380638  token=0x600048B  System.Void .cctor()
  RVA=0x0B37F808  token=0x600048C  System.Boolean IsReservedKeyword(System.String key)
  RVA=0x0B37F770  token=0x600048D  System.Boolean IsObjectReference(Unity.VisualScripting.FullSerializer.fsData data)
  RVA=0x0B37F6D8  token=0x600048E  System.Boolean IsObjectDefinition(Unity.VisualScripting.FullSerializer.fsData data)
  RVA=0x0B37F910  token=0x600048F  System.Boolean IsVersioned(Unity.VisualScripting.FullSerializer.fsData data)
  RVA=0x0B37F878  token=0x6000490  System.Boolean IsTypeSpecified(Unity.VisualScripting.FullSerializer.fsData data)
  RVA=0x0B37FB98  token=0x6000491  System.Boolean IsWrappedData(Unity.VisualScripting.FullSerializer.fsData data)
  RVA=0x0B37F9A8  token=0x6000492  System.Boolean IsVisualScriptingUnit(Unity.VisualScripting.FullSerializer.fsData data)
  RVA=0x0B37C5D0  token=0x6000493  System.Void ConvertLegacyData(Unity.VisualScripting.FullSerializer.fsData& data)
  RVA=0x0B37F63C  token=0x6000494  System.Void Invoke_OnBeforeSerialize(System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsObjectProcessor> processors, System.Type storageType, System.Object instance)
  RVA=0x0B37F478  token=0x6000495  System.Void Invoke_OnAfterSerialize(System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsObjectProcessor> processors, System.Type storageType, System.Object instance, Unity.VisualScripting.FullSerializer.fsData& data)
  RVA=0x0B37F5D0  token=0x6000496  System.Void Invoke_OnBeforeDeserialize(System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsObjectProcessor> processors, System.Type storageType, Unity.VisualScripting.FullSerializer.fsData& data)
  RVA=0x0B37F524  token=0x6000497  System.Void Invoke_OnBeforeDeserializeAfterInstanceCreation(System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsObjectProcessor> processors, System.Type storageType, System.Object instance, Unity.VisualScripting.FullSerializer.fsData& data)
  RVA=0x0B37F3E0  token=0x6000498  System.Void Invoke_OnAfterDeserialize(System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsObjectProcessor> processors, System.Type storageType, System.Object instance)
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsMetaProperty
TYPE:  class
TOKEN: 0x20000F4
SIZE:  0x48
FIELDS:
  private           System.Reflection.MemberInfo    _memberInfo  // 0x10
  private           System.Type                     <StorageType>k__BackingField  // 0x18
  private           System.Type                     <OverrideConverterType>k__BackingField  // 0x20
  private           System.Boolean                  <CanRead>k__BackingField  // 0x28
  private           System.Boolean                  <CanWrite>k__BackingField  // 0x29
  private           System.String                   <JsonName>k__BackingField  // 0x30
  private           System.String                   <MemberName>k__BackingField  // 0x38
  private           System.Boolean                  <IsPublic>k__BackingField  // 0x40
  private           System.Boolean                  <IsReadOnly>k__BackingField  // 0x41
PROPERTIES:
  StorageType  get=0x0385B100  set=0x05392C40
  OverrideConverterType  get=0x04D862C0  set=0x05390F40
  CanRead  get=0x011F33C0  set=0x011F33D0
  CanWrite  get=0x04D867B0  set=0x04D867C0
  JsonName  get=0x011F36E0  set=0x05396200
  MemberName  get=0x04D85A50  set=0x046A91B0
  IsPublic  get=0x02D480A0  set=0x02D480B0
  IsReadOnly  get=0x04D86F40  set=0x04D86F60
METHODS:
  RVA=0x0B377E4C  token=0x600049D  System.Void .ctor(Unity.VisualScripting.FullSerializer.fsConfig config, System.Reflection.FieldInfo field)
  RVA=0x0B377D2C  token=0x600049E  System.Void .ctor(Unity.VisualScripting.FullSerializer.fsConfig config, System.Reflection.PropertyInfo property)
  RVA=0x0B377994  token=0x60004AF  System.Void CommonInitialize(Unity.VisualScripting.FullSerializer.fsConfig config)
  RVA=0x0B377B34  token=0x60004B0  System.Void Write(System.Object context, System.Object value)
  RVA=0x0B377A6C  token=0x60004B1  System.Object Read(System.Object context)
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsMetaType
TYPE:  class
TOKEN: 0x20000F5
SIZE:  0x28
FIELDS:
  public            System.Type                     ReflectedType  // 0x10
  private           System.Boolean                  _hasEmittedAotData  // 0x18
  private           System.Nullable<System.Boolean> _hasDefaultConstructorCache  // 0x19
  private           System.Boolean                  _isDefaultConstructorPublic  // 0x1b
  private           Unity.VisualScripting.FullSerializer.fsMetaProperty[]<Properties>k__BackingField  // 0x20
  private   static  System.Collections.Generic.Dictionary<Unity.VisualScripting.FullSerializer.fsConfig,System.Collections.Generic.Dictionary<System.Type,Unity.VisualScripting.FullSerializer.fsMetaType>>_configMetaTypes  // static @ 0x0
PROPERTIES:
  Properties  get=0x04D862C0  set=0x05390F40
  HasDefaultConstructor  get=0x0B3792EC
METHODS:
  RVA=0x0B37920C  token=0x60004B2  System.Void .ctor(Unity.VisualScripting.FullSerializer.fsConfig config, System.Type reflectedType)
  RVA=0x0B378D38  token=0x60004B6  System.Boolean EmitAotData()
  RVA=0x0B37882C  token=0x60004B7  System.Object CreateInstance()
  RVA=0x0B378E10  token=0x60004B8  Unity.VisualScripting.FullSerializer.fsMetaType Get(Unity.VisualScripting.FullSerializer.fsConfig config, System.Type type)
  RVA=0x0B3783A4  token=0x60004B9  System.Void CollectProperties(Unity.VisualScripting.FullSerializer.fsConfig config, System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.fsMetaProperty> properties, System.Type reflectedType)
  RVA=0x0B3790B8  token=0x60004BA  System.Boolean IsAutoProperty(System.Reflection.PropertyInfo property, System.Reflection.MemberInfo[] members)
  RVA=0x0B3780D0  token=0x60004BB  System.Boolean CanSerializeProperty(Unity.VisualScripting.FullSerializer.fsConfig config, System.Reflection.PropertyInfo property, System.Reflection.MemberInfo[] members, System.Boolean annotationFreeValue)
  RVA=0x0B377EF0  token=0x60004BC  System.Boolean CanSerializeField(Unity.VisualScripting.FullSerializer.fsConfig config, System.Reflection.FieldInfo field, System.Boolean annotationFreeValue)
  RVA=0x0B379180  token=0x60004BD  System.Void .cctor()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.fsReflectionUtility
TYPE:  static class
TOKEN: 0x20000F9
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B37B4A0  token=0x60004C6  System.Type GetInterface(System.Type type, System.Type interfaceType)
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.Internal.fsCyclicReferenceManager
TYPE:  class
TOKEN: 0x20000FA
SIZE:  0x30
FIELDS:
  private           System.Collections.Generic.Dictionary<System.Object,System.Int32>_objectIds  // 0x10
  private           System.Int32                    _nextId  // 0x18
  private           System.Collections.Generic.Dictionary<System.Int32,System.Object>_marked  // 0x20
  private           System.Int32                    _depth  // 0x28
METHODS:
  RVA=0x04D9EBD0  token=0x60004C7  System.Void Enter()
  RVA=0x0B36E6A8  token=0x60004C8  System.Boolean Exit()
  RVA=0x0B36E8B4  token=0x60004C9  System.Object GetReferenceObject(System.Int32 id)
  RVA=0x0B36E644  token=0x60004CA  System.Void AddReferenceWithId(System.Int32 id, System.Object reference)
  RVA=0x0B36E80C  token=0x60004CB  System.Int32 GetReferenceId(System.Object item)
  RVA=0x0B36E9BC  token=0x60004CC  System.Boolean IsReference(System.Object item)
  RVA=0x0B36EA2C  token=0x60004CD  System.Void MarkSerialized(System.Object item)
  RVA=0x0B36EB5C  token=0x60004CE  System.Void .ctor()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.Internal.fsOption`1
TYPE:  sealed struct
TOKEN: 0x20000FC
FIELDS:
  private           System.Boolean                  _hasValue  // 0x0
  private           T                               _value  // 0x0
PROPERTIES:
  HasValue  get=-1  // not resolved
  IsEmpty  get=-1  // not resolved
  Value  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60004D6  System.Void .ctor(T value)
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.Internal.fsOption
TYPE:  static class
TOKEN: 0x20000FD
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x60004D7  Unity.VisualScripting.FullSerializer.Internal.fsOption<T> Just(T value)
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection
TYPE:  static class
TOKEN: 0x20000FE
SIZE:  0x10
FIELDS:
  public    static  System.Type[]                   EmptyTypes  // static @ 0x0
  private   static  System.Collections.Generic.IDictionary<Unity.VisualScripting.FullSerializer.Internal.fsPortableReflection.AttributeQuery,System.Attribute>_cachedAttributeQueries  // static @ 0x8
  private   static  System.Reflection.BindingFlags  DeclaredFlags  // static @ 0x10
METHODS:
  RVA=0x0B379DE4  token=0x60004D8  System.Boolean HasAttribute(System.Reflection.MemberInfo element, System.Type attributeType)
  RVA=0x0539131C  token=0x60004D9  System.Boolean HasAttribute(System.Reflection.MemberInfo element, System.Type attributeType, System.Boolean shouldCache)
  RVA=0x0B37973C  token=0x60004DA  System.Attribute GetAttribute(System.Reflection.MemberInfo element, System.Type attributeType, System.Boolean shouldCache)
  RVA=-1  // generic def  token=0x60004DB  TAttribute GetAttribute(System.Reflection.MemberInfo element, System.Boolean shouldCache)
  RVA=-1  // generic def  token=0x60004DC  TAttribute GetAttribute(System.Reflection.MemberInfo element)
  RVA=0x0B379C30  token=0x60004DD  System.Reflection.PropertyInfo GetDeclaredProperty(System.Type type, System.String propertyName)
  RVA=0x0B379AC4  token=0x60004DE  System.Reflection.MethodInfo GetDeclaredMethod(System.Type type, System.String methodName)
  RVA=0x0B3798A4  token=0x60004DF  System.Reflection.ConstructorInfo GetDeclaredConstructor(System.Type type, System.Type[] parameters)
  RVA=0x0B3799A8  token=0x60004E0  System.Reflection.ConstructorInfo[] GetDeclaredConstructors(System.Type type)
  RVA=0x0B379CE4  token=0x60004E1  System.Reflection.MethodInfo GetFlattenedMethod(System.Type type, System.String methodName)
  RVA=0x0B379B78  token=0x60004E2  System.Reflection.MethodInfo[] GetDeclaredMethods(System.Type type)
  RVA=0x0B379BD4  token=0x60004E3  System.Reflection.PropertyInfo[] GetDeclaredProperties(System.Type type)
  RVA=0x0B379A0C  token=0x60004E4  System.Reflection.FieldInfo[] GetDeclaredFields(System.Type type)
  RVA=0x0B379A68  token=0x60004E5  System.Reflection.MemberInfo[] GetDeclaredMembers(System.Type type)
  RVA=0x0464E630  token=0x60004E6  System.Type Resolve(System.Type type)
  RVA=0x0B379E38  token=0x60004E7  System.Void .cctor()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.Internal.fsTypeExtensions
TYPE:  static class
TOKEN: 0x2000101
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B381E24  token=0x60004EB  System.String CSharpName(System.Type type)
  RVA=0x0B3818C8  token=0x60004EC  System.String CSharpName(System.Type type, System.Boolean includeNamespace)
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.Internal.fsVersionedType
TYPE:  sealed struct
TOKEN: 0x2000103
SIZE:  0x28
FIELDS:
  public            Unity.VisualScripting.FullSerializer.Internal.fsVersionedType[]Ancestors  // 0x10
  public            System.String                   VersionString  // 0x18
  public            System.Type                     ModelType  // 0x20
METHODS:
  RVA=0x0B382B10  token=0x60004EF  System.Object Migrate(System.Object ancestorInstance)
  RVA=0x0B382B9C  token=0x60004F0  System.String ToString()
  RVA=0x0B382A94  token=0x60004F1  System.Boolean Equals(System.Object obj)
  RVA=0x08479504  token=0x60004F2  System.Int32 GetHashCode()
END_CLASS

CLASS: Unity.VisualScripting.FullSerializer.Internal.fsVersionManager
TYPE:  static class
TOKEN: 0x2000104
SIZE:  0x10
FIELDS:
  private   static readonly System.Collections.Generic.Dictionary<System.Type,Unity.VisualScripting.FullSerializer.Internal.fsOption<Unity.VisualScripting.FullSerializer.Internal.fsVersionedType>>_cache  // static @ 0x0
METHODS:
  RVA=0x0B381F58  token=0x60004F3  Unity.VisualScripting.FullSerializer.fsResult GetVersionImportPath(System.String currentVersion, Unity.VisualScripting.FullSerializer.Internal.fsVersionedType targetVersion, System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.Internal.fsVersionedType>& path)
  RVA=0x0B381E30  token=0x60004F4  System.Boolean GetVersionImportPathRecursive(System.Collections.Generic.List<Unity.VisualScripting.FullSerializer.Internal.fsVersionedType> path, System.String currentVersion, Unity.VisualScripting.FullSerializer.Internal.fsVersionedType current)
  RVA=0x0B382190  token=0x60004F5  Unity.VisualScripting.FullSerializer.Internal.fsOption<Unity.VisualScripting.FullSerializer.Internal.fsVersionedType> GetVersionedType(System.Type type)
  RVA=0x0B382614  token=0x60004F6  System.Void VerifyConstructors(Unity.VisualScripting.FullSerializer.Internal.fsVersionedType type)
  RVA=0x0B38279C  token=0x60004F7  System.Void VerifyUniqueVersionStrings(Unity.VisualScripting.FullSerializer.Internal.fsVersionedType type)
  RVA=0x0B382A08  token=0x60004F8  System.Void .cctor()
END_CLASS

