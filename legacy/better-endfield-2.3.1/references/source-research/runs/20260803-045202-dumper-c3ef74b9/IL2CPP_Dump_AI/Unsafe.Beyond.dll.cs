// ========================================================
// Dumped by @desirepro
// Assembly: Unsafe.Beyond.dll
// Classes:  101
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

CLASS: UnionKey
TYPE:  sealed struct
TOKEN: 0x200000C
SIZE:  0x28
FIELDS:
  public            System.Int32                    intKey  // 0x10
  public            System.Int64                    longKey  // 0x18
  public            System.String                   stringKey  // 0x20
METHODS:
  RVA=0x0B5A6F28  token=0x60000AA  System.Void .ctor(System.Int32 key)
  RVA=0x0B5A6F58  token=0x60000AB  System.Void .ctor(System.Int64 key)
  RVA=0x0B5A6F40  token=0x60000AC  System.Void .ctor(System.String key)
END_CLASS

CLASS: UnionKeyEnumerator
TYPE:  sealed struct
TOKEN: 0x200000D
SIZE:  0x70
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_parent  // 0x10
  private           System.Int32                    m_index  // 0x58
  private           System.Int32                    m_currentSlotIndex  // 0x5c
  private           System.Int32                    m_currentSlotStartIndex  // 0x60
  private           Beyond.SparkBuffer.Runtime.Wrapper.HashSlotm_currentSlot  // 0x64
PROPERTIES:
  Current  get=0x0B5A6D48
  System.Collections.IEnumerator.Current  get=0x0B5A6C94
METHODS:
  RVA=0x0B5A6CF4  token=0x60000AF  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map parent)
  RVA=0x0B5A6BA8  token=0x60000B0  System.Boolean MoveNext()
  RVA=0x041E1670  token=0x60000B1  System.Void Dispose()
  RVA=0x04DBF490  token=0x60000B2  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: Options
TYPE:  sealed struct
TOKEN: 0x200000F
SIZE:  0x38
FIELDS:
  public            System.String                   rootName  // 0x10
  public            Beyond.SparkBuffer.SparkType    rootType  // 0x18
  public            Beyond.SparkBuffer.SparkType    subType1  // 0x19
  public            Beyond.SparkBuffer.SparkType    subType2  // 0x1a
  public            Beyond.SparkBuffer.Runtime.BeanTypebeanType  // 0x20
  public            Beyond.SparkBuffer.Runtime.EnumTypeenumType  // 0x28
  public            Beyond.SparkBuffer.Runtime.EnumTypemapKeyEnumType  // 0x30
METHODS:
END_CLASS

CLASS: ReservedField
TYPE:  sealed struct
TOKEN: 0x2000013
SIZE:  0x70
FIELDS:
  public            System.Int32                    position  // 0x10
  public            Newtonsoft.Json.Linq.JToken     token  // 0x18
  public            Beyond.SparkBuffer.SparkType    type  // 0x20
  public            Beyond.SparkBuffer.Runtime.BeanTypebeanType  // 0x28
  public            Beyond.SparkBuffer.Runtime.ArrayTypearrayType  // 0x30
  public            Beyond.SparkBuffer.Runtime.MapTypemapType  // 0x48
METHODS:
END_CLASS

CLASS: JsonLoader
TYPE:  class
TOKEN: 0x2000012
SIZE:  0x20
IMPLEMENTS: System.IDisposable
FIELDS:
  private           Beyond.SparkBuffer.Runtime.ByteWriterm_writer  // 0x10
  private           System.Collections.Generic.Dictionary<System.String,System.Int32>m_stringMap  // 0x18
METHODS:
  RVA=0x0B59B2AC  token=0x60000DD  System.Void .ctor(Beyond.SparkBuffer.Runtime.ByteWriter writer)
  RVA=0x040E20A0  token=0x60000DE  System.Void Dispose()
  RVA=0x0B597A64  token=0x60000DF  System.Void PopulateJsonDataWithoutTypeAndRootDef(Beyond.SparkBuffer.Runtime.ByteWriter writer, Newtonsoft.Json.Linq.JToken rootObj, Beyond.SparkBuffer.Runtime.Root.Options rootOptions, System.Int32& dataOffset)
  RVA=0x0B59B218  token=0x60000E0  System.Void _LoadJsonContentWithoutTypeAndRootDef(Newtonsoft.Json.Linq.JToken rootObj, Beyond.SparkBuffer.Runtime.Root.Options rootOptions, System.Int32& dataOffset)
  RVA=0x0B59AAEC  token=0x60000E1  System.Void _ExportStrings(Newtonsoft.Json.Linq.JToken rootJson, Beyond.SparkBuffer.Runtime.Root.Options rootOptions)
  RVA=0x0B597DB4  token=0x60000E2  System.Void _CollectBeanStrings(System.Collections.Generic.HashSet<System.String> stringSet, Newtonsoft.Json.Linq.JObject obj, Beyond.SparkBuffer.Runtime.BeanType beanType)
  RVA=0x0B597BA0  token=0x60000E3  System.Void _CollectArrayStrings(System.Collections.Generic.HashSet<System.String> stringSet, Newtonsoft.Json.Linq.JArray obj, Beyond.SparkBuffer.Runtime.ArrayType arrayType)
  RVA=0x0B59827C  token=0x60000E4  System.Void _CollectMapStrings(System.Collections.Generic.HashSet<System.String> stringSet, Newtonsoft.Json.Linq.JObject obj, Beyond.SparkBuffer.Runtime.MapType mapType)
  RVA=0x0B59A740  token=0x60000E5  System.Int32 _ExportRootData(Newtonsoft.Json.Linq.JToken rootJson, Beyond.SparkBuffer.Runtime.Root.Options rootOptions)
  RVA=0x0B598D50  token=0x60000E6  System.Int32 _ExportBeanData(Newtonsoft.Json.Linq.JObject beanObj, Beyond.SparkBuffer.Runtime.BeanType beanType)
  RVA=0x0B5988F0  token=0x60000E7  System.Int32 _ExportArrayData(Newtonsoft.Json.Linq.JArray arrayObj, Beyond.SparkBuffer.Runtime.ArrayType arrayType)
  RVA=0x0B599978  token=0x60000E8  System.Int32 _ExportMapData(Newtonsoft.Json.Linq.JObject mapObj, Beyond.SparkBuffer.Runtime.MapType mapType)
  RVA=0x0B5984F0  token=0x60000E9  System.Collections.Generic.Dictionary<System.Int32,System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<System.String,Newtonsoft.Json.Linq.JToken>>> _CreateMapHashEntry(Newtonsoft.Json.Linq.JObject mapObj, Beyond.SparkBuffer.SparkType keyType)
  RVA=0x0B59AFD8  token=0x60000EA  System.Int32 _GetMapKeyHash(System.String key, Beyond.SparkBuffer.SparkType keyType)
  RVA=0x0B599FC4  token=0x60000EB  System.Void _ExportMapKey(System.String key, Beyond.SparkBuffer.SparkType keyType)
  RVA=0x0B59A194  token=0x60000EC  System.Void _ExportMapValue(Newtonsoft.Json.Linq.JToken valueToken, Beyond.SparkBuffer.Runtime.MapType mapType, System.Collections.Generic.List<Beyond.SparkBuffer.Runtime.SparkManager.JsonLoader.ReservedField> reservedFields)
  RVA=0x0B59A334  token=0x60000ED  System.Void _ExportReservedFields(System.Collections.Generic.List<Beyond.SparkBuffer.Runtime.SparkManager.JsonLoader.ReservedField> reservedFields)
  RVA=0x0B59A9E8  token=0x60000EE  System.Void _ExportStringByLoc(System.String strVal)
  RVA=0x0B5997E4  token=0x60000EF  System.Void _ExportEnumData(Newtonsoft.Json.Linq.JToken token, Beyond.SparkBuffer.Runtime.EnumType enumType)
  RVA=0x0B599540  token=0x60000F0  System.Void _ExportCommonDataOrThrow(Newtonsoft.Json.Linq.JToken token, Beyond.SparkBuffer.SparkType type, System.Collections.Generic.List<Beyond.SparkBuffer.Runtime.SparkManager.JsonLoader.ReservedField> reservedFields)
  RVA=0x0B598800  token=0x60000F1  System.Void _EnsureJTokenType(Newtonsoft.Json.Linq.JToken token, Newtonsoft.Json.Linq.JTokenType requiredType)
  RVA=0x0B59B178  token=0x60000F2  System.Exception _InvalidRootTypeError(Beyond.SparkBuffer.SparkType rootType)
END_CLASS

CLASS: <>c__DisplayClass14_0
TYPE:  sealed class
TOKEN: 0x2000015
SIZE:  0x18
FIELDS:
  public            System.IntPtr                   L  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60000FE  System.Void .ctor()
  RVA=0x03FA9A90  token=0x60000FF  System.Void <RegisterToLua>b__0(Beyond.SparkBuffer.Runtime.Root root)
  RVA=0x0B5A6B14  token=0x6000100  System.Void <RegisterToLua>b__1(Beyond.SparkBuffer.Runtime.Root root)
  RVA=0x0B5A6B38  token=0x6000101  System.Void <RegisterToLua>b__2()
END_CLASS

CLASS: EnumItem
TYPE:  sealed struct
TOKEN: 0x2000020
SIZE:  0x20
FIELDS:
  public            System.String                   name  // 0x10
  public            System.Int32                    value  // 0x18
METHODS:
END_CLASS

CLASS: ReflectResult
TYPE:  sealed struct
TOKEN: 0x2000053
SIZE:  0x20
FIELDS:
  public            Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType[]types  // 0x10
  public            Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObjectrootObj  // 0x18
METHODS:
END_CLASS

CLASS: FieldInfo
TYPE:  sealed struct
TOKEN: 0x2000055
SIZE:  0x20
FIELDS:
  public            System.String                   name  // 0x10
  public            Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectTypetype  // 0x18
METHODS:
END_CLASS

CLASS: ReflectType
TYPE:  class
TOKEN: 0x2000054
SIZE:  0x38
FIELDS:
  public            Beyond.SparkBuffer.SparkType    sparkType  // 0x10
  public            System.Type                     concreteType  // 0x18
  public            Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectTypesubType1  // 0x20
  public            Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectTypesubType2  // 0x28
  public            System.Collections.Generic.List<Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType.FieldInfo>beanFields  // 0x30
METHODS:
  RVA=0x041E1670  token=0x6000321  System.Void .ctor()
END_CLASS

CLASS: ReflectObject
TYPE:  class
TOKEN: 0x2000056
SIZE:  0x38
FIELDS:
  public            System.Object                   originObj  // 0x10
  public            Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectTypereflectType  // 0x18
  public            System.Collections.Specialized.OrderedDictionarybeanElements  // 0x20
  public            System.Collections.Generic.List<Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject>arrayElements  // 0x28
  public            System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject,Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject>>mapElements  // 0x30
METHODS:
  RVA=0x041E1670  token=0x6000322  System.Void .ctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000057
SIZE:  0x10
FIELDS:
  public    static readonly Beyond.SparkBuffer.Serialize.ObjectReflector.<>c<>9  // static @ 0x0
  public    static  System.Comparison<Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType><>9__7_0  // static @ 0x8
  public    static  System.Comparison<Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType.FieldInfo><>9__9_0  // static @ 0x10
  public    static  System.Comparison<System.Collections.Generic.KeyValuePair<Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject,Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject>><>9__13_0  // static @ 0x18
  public    static  System.Func<System.Reflection.FieldInfo,System.Int32><>9__14_0  // static @ 0x20
METHODS:
  RVA=0x0B5A6B44  token=0x6000323  System.Void .cctor()
  RVA=0x041E1670  token=0x6000324  System.Void .ctor()
  RVA=0x0B5A69E0  token=0x6000325  System.Int32 <_ParseInternal>b__7_0(Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType lhs, Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType rhs)
  RVA=0x0B5A6AFC  token=0x6000326  System.Int32 <_TryParseType>b__9_0(Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType.FieldInfo lhs, Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType.FieldInfo rhs)
  RVA=0x0B5A6A28  token=0x6000327  System.Int32 <_ReflectMap>b__13_0(System.Collections.Generic.KeyValuePair<Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject,Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject> lhs, System.Collections.Generic.KeyValuePair<Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject,Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject> rhs)
  RVA=0x0B5A69C0  token=0x6000328  System.Int32 <_GetFields>b__14_0(System.Reflection.FieldInfo field)
END_CLASS

CLASS: ReservedField
TYPE:  sealed struct
TOKEN: 0x2000059
SIZE:  0x20
FIELDS:
  public            System.Int32                    position  // 0x10
  public            Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObjectreflectObj  // 0x18
METHODS:
END_CLASS

CLASS: Options
TYPE:  sealed struct
TOKEN: 0x200005A
SIZE:  0x20
FIELDS:
  public    static readonly Beyond.SparkBuffer.Serialize.SparkSerializer.OptionsDEFAULT  // static @ 0x0
  public            System.String                   rootName  // 0x10
  public            System.String                   stripTypeNamePrefix  // 0x18
METHODS:
  RVA=0x033928A0  token=0x6000344  System.Void .ctor(System.String rootName)
  RVA=0x039CF630  token=0x6000345  System.Void .ctor(System.String rootName, System.String stripTypeNamePrefix)
  RVA=0x0B5A7BB0  token=0x6000346  System.Void .cctor()
END_CLASS

CLASS: HashSlot
TYPE:  sealed struct
TOKEN: 0x200005B
SIZE:  0x18
FIELDS:
  public            System.Int32                    offset  // 0x10
  public            System.Int32                    bucketsSize  // 0x14
METHODS:
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200005C
SIZE:  0x10
FIELDS:
  public    static readonly Beyond.SparkBuffer.Serialize.SparkSerializer.<>c<>9  // static @ 0x0
  public    static  System.Comparison<System.String><>9__23_0  // static @ 0x8
METHODS:
  RVA=0x0B5A9B84  token=0x6000347  System.Void .cctor()
  RVA=0x041E1670  token=0x6000348  System.Void .ctor()
  RVA=0x0A7BA4E0  token=0x6000349  System.Int32 <_ExportStrings>b__23_0(System.String lhs, System.String rhs)
END_CLASS

CLASS: Beyond.Reflection.MethodPointerManager
TYPE:  class
TOKEN: 0x200005D
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x600034A  T2 CallPointer(System.IntPtr ptr, T1 param)
  RVA=-1  // generic def  token=0x600034B  TRet CallDelegatePointer(System.IntPtr ptr, TParam1 target)
  RVA=-1  // generic def  token=0x600034C  TRet CallDelegatePointer(System.IntPtr ptr, TParam1 target, TParam2 param)
  RVA=-1  // generic def  token=0x600034D  System.ValueTuple<System.IntPtr,T1> GetDelegatePointer(System.Delegate d)
  RVA=0x0B5A7980  token=0x600034E  System.IntPtr GetDelegatePointer(System.Reflection.MethodInfo d)
  RVA=0x041E1670  token=0x600034F  System.Void .ctor()
END_CLASS

CLASS: Beyond.Reflection.ObjectPointerHelper
TYPE:  static class
TOKEN: 0x200005E
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000350  System.IntPtr GetIntPtr(T& value)
  RVA=-1  // generic def  token=0x6000351  System.IntPtr GetArrayPtr(T[] value)
  RVA=-1  // generic def  token=0x6000352  T GetStructByPointer(System.IntPtr ptr)
  RVA=-1  // generic def  token=0x6000353  T GetValue(System.IntPtr ptr, System.Int32 offset)
  RVA=-1  // generic def  token=0x6000354  T GetArrayElement(System.IntPtr array, System.Int32 index)
  RVA=-1  // generic def  token=0x6000355  System.Void SetValue(System.IntPtr ptr, System.Int32 offset, T value)
  RVA=-1  // generic def  token=0x6000356  System.Void SetArrayElement(System.IntPtr array, System.Int32 index, T value)
  RVA=0x0B5A7B9C  token=0x6000357  System.Void SetValue(System.IntPtr ptr, System.Int32 offset, System.Byte* data, System.Int32 size)
  RVA=0x0B5A7A3C  token=0x6000358  System.Object GetObj(System.IntPtr ptr, System.Int32 offset)
  RVA=0x0B5A7B4C  token=0x6000359  System.Void SetObj(System.IntPtr ptr, System.Int32 offset, System.Object value)
  RVA=0x0B5A7A34  token=0x600035A  System.Void CopyValue(System.IntPtr target, System.IntPtr source, System.Int32 size)
  RVA=0x0B5A7A34  token=0x600035B  System.Void CopyValue(System.Void* target, System.Void* source, System.Int32 size)
  RVA=0x0B5A79D0  token=0x600035C  System.Void CopyValue(System.Byte* to, System.Byte* from, System.Int32 size)
  RVA=0x0B5A7A78  token=0x600035D  System.ReadOnlySpan<System.Byte> GetReadOnlySpan(System.IntPtr ptr, System.Int32 maxLength)
END_CLASS

CLASS: Beyond.Reflection.StructMemManager
TYPE:  class
TOKEN: 0x200005F
SIZE:  0x10
FIELDS:
  private   static  System.Collections.Generic.Dictionary<System.Int32,System.ValueTuple<System.Collections.Generic.HashSet<System.Int64>,System.Collections.Generic.List<System.IntPtr>>>s_structMemDict  // static @ 0x0
METHODS:
  RVA=0x0B5A84AC  token=0x600035E  System.IntPtr GetMemoryBySize(System.Int32 size)
  RVA=0x0B5A837C  token=0x600035F  System.Void FreeMemory(System.Int32 size, System.IntPtr ptr)
  RVA=0x0B5A7C44  token=0x6000360  System.Void ClearMemory()
  RVA=0x0B5A7F84  token=0x6000361  System.String DumpMemory()
  RVA=0x041E1670  token=0x6000362  System.Void .ctor()
  RVA=0x0B5A870C  token=0x6000363  System.Void .cctor()
END_CLASS

CLASS: Beyond.Reflection.StructPtrRefrenceManager
TYPE:  class
TOKEN: 0x2000060
SIZE:  0x10
FIELDS:
  private   static  System.Collections.Generic.Dictionary<System.Object,System.Object>s_refDic  // static @ 0x0
  private   static  System.Collections.Generic.Dictionary<System.Type,System.Collections.Generic.List<System.Int32>>s_structPtrOffsetDic  // static @ 0x8
METHODS:
  RVA=0x03B7D0C0  token=0x6000364  System.Void _RunOnStart()
  RVA=0x0B5A8798  token=0x6000365  System.Void AddObjInPtrRef(System.Type type, System.IntPtr ptr)
  RVA=0x0B5A8A64  token=0x6000366  System.Void RemoveObjInPtrRef(System.Type type, System.IntPtr ptr)
  RVA=0x041E1670  token=0x6000367  System.Void .ctor()
  RVA=0x04CA3A20  token=0x6000368  System.Void .cctor()
END_CLASS

CLASS: Beyond.Reflection.StructPtrWrapper
TYPE:  class
TOKEN: 0x2000061
SIZE:  0x28
FIELDS:
  public            System.IntPtr                   ptr  // 0x10
  public            System.Int32                    size  // 0x18
  public            System.Type                     type  // 0x20
  private   static  UnityEngine.Pool.ObjectPool<Beyond.Reflection.StructPtrWrapper>s_pool  // static @ 0x0
  private   static  System.IntPtr                   s_ptr  // static @ 0x8
  private   static  System.Int32                    s_size  // static @ 0x10
  private   static  System.Type                     s_type  // static @ 0x18
  private   static  System.Object                   s_structObj  // static @ 0x20
METHODS:
  RVA=0x0B5A90EC  token=0x6000369  Beyond.Reflection.StructPtrWrapper GetStructPtrWrapper(System.IntPtr ptr, System.Int32 size, System.Type type)
  RVA=0x0B5A8FA0  token=0x600036A  Beyond.Reflection.StructPtrWrapper GetStructPtrWrapper(System.Object structObj)
  RVA=0x0B5A92D4  token=0x600036B  System.Void Release(Beyond.Reflection.StructPtrWrapper obj)
  RVA=0x0B5A9A18  token=0x600036C  System.Void .ctor(System.IntPtr ptr, System.Int32 size, System.Type type)
  RVA=0x0B5A8EC0  token=0x600036D  System.Object GetStructObj()
  RVA=0x0B5A8E1C  token=0x600036E  System.Object GetStructObj(System.IntPtr ptr, System.Type type, System.Int32 size)
  RVA=0x0B5A93B8  token=0x600036F  System.Void SetStructObjValue(System.Object structObj)
  RVA=0x0B5A8DBC  token=0x6000370  System.Int32 GetHashCode()
  RVA=0x0B5A8CEC  token=0x6000371  System.Boolean Equals(System.Object obj)
  RVA=0x0B5A943C  token=0x6000372  System.String ToString()
  RVA=0x0B5A9288  token=0x6000373  System.Type GetType()
  RVA=0x0B5A8F4C  token=0x6000374  System.Object GetStructObj(Beyond.Reflection.StructPtrWrapper obj)
  RVA=0x0B5A9AA4  token=0x6000375  System.Void .ctor(System.Object structObj)
  RVA=0x0B5A949C  token=0x6000376  Beyond.Reflection.StructPtrWrapper _OnCreate()
  RVA=0x0B5A95D0  token=0x6000377  System.Void _OnGet(Beyond.Reflection.StructPtrWrapper o)
  RVA=0x0B5A97D4  token=0x6000378  System.Void _OnRealse(Beyond.Reflection.StructPtrWrapper obj)
  RVA=0x0B5A9884  token=0x6000379  System.Void .cctor()
  RVA=0x04D87100  token=0x600037A  System.Int32 <>iFixBaseProxy_GetHashCode()
  RVA=0x02FB1840  token=0x600037B  System.Boolean <>iFixBaseProxy_Equals(System.Object P0)
  RVA=0x0669AD6C  token=0x600037C  System.String <>iFixBaseProxy_ToString()
END_CLASS

CLASS: Beyond.SparkBuffer.SparkType
TYPE:  sealed struct
TOKEN: 0x2000004
SIZE:  0x11
FIELDS:
  public            System.Byte                     value__  // 0x10
  public    static  Beyond.SparkBuffer.SparkType    Bool  // const
  public    static  Beyond.SparkBuffer.SparkType    Byte  // const
  public    static  Beyond.SparkBuffer.SparkType    Int  // const
  public    static  Beyond.SparkBuffer.SparkType    Long  // const
  public    static  Beyond.SparkBuffer.SparkType    Float  // const
  public    static  Beyond.SparkBuffer.SparkType    Double  // const
  public    static  Beyond.SparkBuffer.SparkType    Enum  // const
  public    static  Beyond.SparkBuffer.SparkType    String  // const
  public    static  Beyond.SparkBuffer.SparkType    Bean  // const
  public    static  Beyond.SparkBuffer.SparkType    Array  // const
  public    static  Beyond.SparkBuffer.SparkType    Map  // const
METHODS:
END_CLASS

CLASS: Beyond.SparkBuffer.Common
TYPE:  static class
TOKEN: 0x2000005
SIZE:  0x10
FIELDS:
  public    static  System.Int32                    NULL_PLACEHOLDER  // const
  public    static  System.Int32                    BOOL_MEM_SIZE  // const
  public    static  System.Int32                    BYTE_MEM_SIZE  // const
  public    static  System.Int32                    INT_MEM_SIZE  // const
  public    static  System.Int32                    LONG_MEM_SIZE  // const
  public    static  System.Int32                    FLOAT_MEM_SIZE  // const
  public    static  System.Int32                    DOUBLE_MEM_SIZE  // const
METHODS:
END_CLASS

CLASS: Beyond.SparkBuffer.Utils
TYPE:  static class
TOKEN: 0x2000006
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B5A6F70  token=0x6000003  System.Int32 Align4Bytes(System.Int32 position)
  RVA=0x0B5A6F84  token=0x6000004  System.Int32 Align8Bytes(System.Int32 position)
  RVA=0x0B5A6F98  token=0x6000005  System.Int32 AlignBytesForSize(System.Int32 position, System.Int32 size)
  RVA=0x03293C50  token=0x6000006  System.Int32 GetMemSizeForType(Beyond.SparkBuffer.SparkType sparkType)
  RVA=0x03292FE0  token=0x6000007  System.Int32 GetFieldOffset(Beyond.SparkBuffer.SparkType sparkType, System.Int32& postion)
  RVA=0x0B5A6FF8  token=0x6000008  System.Boolean IsEnumOrBean(Beyond.SparkBuffer.SparkType sparkType)
  RVA=0x0B5A6FE8  token=0x6000009  System.Boolean IsArrayOrMap(Beyond.SparkBuffer.SparkType sparkType)
  RVA=0x0B5A6FC0  token=0x600000A  System.Boolean CheckValidMapKeyType(Beyond.SparkBuffer.SparkType sparkType)
  RVA=0x0B5A6FD4  token=0x600000B  System.Boolean CheckValidRootType(Beyond.SparkBuffer.SparkType sparkType)
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.ByteReader
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x30
IMPLEMENTS: System.IDisposable
FIELDS:
  private           Unity.Collections.NativeArray<System.Byte>m_buffer  // 0x10
  private           System.Byte*                    m_bufferPtr  // 0x20
  private           System.Int32                    m_position  // 0x28
  private   readonly System.Int32                    m_length  // 0x2c
  public    static  Beyond.SparkBuffer.Runtime.IStringPools_stringPool  // static @ 0x0
  public    static  System.Action<System.IntPtr>    s_freeMemory  // static @ 0x8
  public    static  System.Func<System.Byte[],System.IntPtr>s_mallocMemory  // static @ 0x10
PROPERTIES:
  position  get=0x011F2A90
  buffer  get=0x02B2D510
METHODS:
  RVA=0x02F218C0  token=0x600000E  System.Void .ctor(System.Byte[] buffer)
  RVA=0x0B595A14  token=0x600000F  System.Void Dispose()
  RVA=0x011F2AA0  token=0x6000010  System.Void SeekTo(System.Int32 pos)
  RVA=0x0B595A5C  token=0x6000011  System.Boolean ReadBool()
  RVA=0x04DBF440  token=0x6000012  System.Byte ReadByte()
  RVA=0x0B595ACC  token=0x6000013  System.Int32 ReadInt()
  RVA=0x0B595AF8  token=0x6000014  System.Int64 ReadLong()
  RVA=0x0B595AA0  token=0x6000015  System.Single ReadFloat()
  RVA=0x0B595A74  token=0x6000016  System.Double ReadDouble()
  RVA=0x0B595B28  token=0x6000017  System.String ReadString()
  RVA=0x04DBF460  token=0x6000018  Beyond.SparkBuffer.Runtime.Wrapper.HashSlot ReadHashSlot()
  RVA=0x04DBF440  token=0x6000019  Beyond.SparkBuffer.SparkType ReadSparkType()
  RVA=0x0B595BF0  token=0x600001A  System.Int32 ReadTypeHash()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.ByteWriter
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x20
IMPLEMENTS: System.IDisposable
FIELDS:
  private   static readonly System.Byte[]                   BUFFER_0  // static @ 0x0
  private   readonly System.IO.BinaryWriter          m_writer  // 0x10
  private   readonly System.IO.Stream                m_stream  // 0x18
  public    static  Beyond.SparkBuffer.Runtime.IStringPools_stringPool  // static @ 0x8
PROPERTIES:
  position  get=0x0B59625C
METHODS:
  RVA=0x0B5961D8  token=0x600001C  System.Void .ctor(System.IO.Stream stream)
  RVA=0x0B595BF8  token=0x600001D  System.Void Dispose()
  RVA=0x0B595C78  token=0x600001E  System.Void SeekTo(System.Int32 pos)
  RVA=0x0B595CA4  token=0x600001F  System.Void WriteBool(System.Boolean v)
  RVA=0x0B595CC4  token=0x6000020  System.Void WriteByte(System.Byte v)
  RVA=0x0B595DF8  token=0x6000021  System.Void WriteInt(System.Int32 v)
  RVA=0x0B595E34  token=0x6000022  System.Void WriteLong(System.Int64 v)
  RVA=0x0B595DBC  token=0x6000023  System.Void WriteFloat(System.Single v)
  RVA=0x0B595D80  token=0x6000024  System.Void WriteDouble(System.Double v)
  RVA=0x0B595E70  token=0x6000025  System.Void WriteStringAlignment(System.String v)
  RVA=0x0B595F5C  token=0x6000026  System.Void WriteString(System.String v)
  RVA=0x0B595CE4  token=0x6000027  System.Void WriteBytes(System.ReadOnlySpan<System.Byte> buffer, System.Int32 index, System.Int32 count)
  RVA=0x0B595CC4  token=0x6000028  System.Void WriteSparkType(Beyond.SparkBuffer.SparkType sparkType)
  RVA=0x0B595C34  token=0x6000029  System.Int32 FillAlignment(System.Int32 align)
  RVA=0x0B596038  token=0x600002A  System.Void _FillAlignment4()
  RVA=0x0B5960D4  token=0x600002B  System.Void _FillAlignment8()
  RVA=0x0B596170  token=0x600002C  System.Void .cctor()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Array
TYPE:  sealed struct
TOKEN: 0x2000009
SIZE:  0x38
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.ArrayTypem_arrayType  // 0x10
  private   readonly Beyond.SparkBuffer.Runtime.Root m_root  // 0x28
  private   readonly System.Int32                    m_dataOffset  // 0x30
  private   readonly System.Int32                    m_length  // 0x34
PROPERTIES:
  length  get=0x011F0020
METHODS:
  RVA=0x0305EE00  token=0x600002E  System.Void .ctor(Beyond.SparkBuffer.Runtime.ArrayType arrayType, System.Int32 offset, Beyond.SparkBuffer.Runtime.Root root)
  RVA=0x0B594D2C  token=0x600002F  System.Boolean GetBool(System.Int32 index)
  RVA=0x0B594B48  token=0x6000030  System.Byte GetByte(System.Int32 index)
  RVA=0x0B594EE8  token=0x6000031  System.Int32 GetInt(System.Int32 index)
  RVA=0x0B594BB4  token=0x6000032  System.Int64 GetLong(System.Int32 index)
  RVA=0x0B594E78  token=0x6000033  System.Single GetFloat(System.Int32 index)
  RVA=0x0B594D98  token=0x6000034  System.Double GetDouble(System.Int32 index)
  RVA=0x0B594F58  token=0x6000035  System.String GetString(System.Int32 index)
  RVA=0x0B594E08  token=0x6000036  System.Int32 GetEnum(System.Int32 index)
  RVA=0x0B594C9C  token=0x6000037  System.Nullable<Beyond.SparkBuffer.Runtime.Bean> GetBean(System.Int32 index)
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Bean
TYPE:  sealed struct
TOKEN: 0x200000A
SIZE:  0x28
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.BeanTypem_beanType  // 0x10
  private   readonly Beyond.SparkBuffer.Runtime.Root m_root  // 0x18
  private   readonly System.Int32                    m_dataOffset  // 0x20
METHODS:
  RVA=0x0305FE10  token=0x6000038  System.Void .ctor(Beyond.SparkBuffer.Runtime.BeanType beanType, System.Int32 offset, Beyond.SparkBuffer.Runtime.Root root)
  RVA=0x0B595180  token=0x6000039  System.Boolean GetBool(System.String key, System.Int32& offset)
  RVA=0x0B5951F0  token=0x600003A  System.Boolean GetBool(System.Int32 offset)
  RVA=0x0B595218  token=0x600003B  System.Byte GetByte(System.String key, System.Int32& offset)
  RVA=0x0B595288  token=0x600003C  System.Byte GetByte(System.Int32 offset)
  RVA=0x0B595370  token=0x600003D  System.Int32 GetInt(System.String key, System.Int32& offset)
  RVA=0x0B595348  token=0x600003E  System.Int32 GetInt(System.Int32 offset)
  RVA=0x0B595478  token=0x600003F  System.Int64 GetLong(System.String key, System.Int32& offset)
  RVA=0x0B5954E8  token=0x6000040  System.Int64 GetLong(System.Int32 offset)
  RVA=0x0B595408  token=0x6000041  System.Single GetFloat(System.String key, System.Int32& offset)
  RVA=0x0B5953E0  token=0x6000042  System.Single GetFloat(System.Int32 offset)
  RVA=0x0B5952B0  token=0x6000043  System.Double GetDouble(System.String key, System.Int32& offset)
  RVA=0x0B595320  token=0x6000044  System.Double GetDouble(System.Int32 offset)
  RVA=0x0B595510  token=0x6000045  System.String GetString(System.String key, System.Int32& offset)
  RVA=0x0B595580  token=0x6000046  System.String GetString(System.Int32 offset)
  RVA=0x0B595370  token=0x6000047  System.Int32 GetEnum(System.String key, System.Int32& offset)
  RVA=0x0B595348  token=0x6000048  System.Int32 GetEnum(System.Int32 offset)
  RVA=0x0305F690  token=0x6000049  System.Nullable<Beyond.SparkBuffer.Runtime.Bean> GetBean(System.String key)
  RVA=0x0305EF30  token=0x600004A  System.Nullable<Beyond.SparkBuffer.Runtime.Array> GetArray(System.String key)
  RVA=0x03293520  token=0x600004B  System.Nullable<Beyond.SparkBuffer.Runtime.Map> GetMap(System.String key)
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Map
TYPE:  sealed struct
TOKEN: 0x200000B
SIZE:  0x58
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   static  System.Int32                    INT_KEY_MEM_SIZE  // const
  private   static  System.Int32                    LONG_KEY_MEM_SIZE  // const
  private   readonly Beyond.SparkBuffer.Runtime.MapTypem_mapType  // 0x10
  private   readonly Beyond.SparkBuffer.Runtime.Root m_root  // 0x38
  private   readonly System.Int32                    m_valueMemSize  // 0x40
  private   readonly System.Int32                    m_keyMemSize  // 0x44
  private   readonly System.Int32                    m_dataOffset  // 0x48
  private   readonly System.Int32                    m_count  // 0x4c
  private   readonly System.Int32                    m_slotsOffset  // 0x50
PROPERTIES:
  count  get=0x04D86640
METHODS:
  RVA=0x03293AA0  token=0x600004D  System.Void .ctor(Beyond.SparkBuffer.Runtime.MapType mapType, System.Int32 offset, Beyond.SparkBuffer.Runtime.Root root)
  RVA=0x0B59D430  token=0x600004E  Beyond.SparkBuffer.Runtime.Map.UnionKeyEnumerator GetEnumerator()
  RVA=0x0B59DCA0  token=0x600004F  System.Collections.Generic.IEnumerator<Beyond.SparkBuffer.Runtime.Map.UnionKey> System.Collections.Generic.IEnumerable<Beyond.SparkBuffer.Runtime.Map.UnionKey>.GetEnumerator()
  RVA=0x0B59DD2C  token=0x6000050  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=0x0B59D604  token=0x6000051  System.Int32 GetHash(System.String str)
  RVA=0x04D88BE0  token=0x6000052  System.Int32 GetHash(System.Int32 value)
  RVA=0x04D88BE0  token=0x6000053  System.Int32 GetHash(System.Int64 value)
  RVA=0x0B59D588  token=0x6000054  Beyond.SparkBuffer.Runtime.Wrapper.HashSlot GetHashSlot(System.Int32 slotIndex)
  RVA=0x0B59E35C  token=0x6000055  System.Int32 _FindHashIndexByKey(System.Int32 intKey)
  RVA=0x0B59E280  token=0x6000056  System.Int32 _FindHashIndexByKey(System.Int64 longKey)
  RVA=0x0B59E434  token=0x6000057  System.Int32 _FindHashIndexByKey(System.String stringKey)
  RVA=0x0B59D624  token=0x6000058  System.Int32 GetIntKeyByIndex(System.Int32 index)
  RVA=0x0B59D878  token=0x6000059  System.Int64 GetLongKeyByIndex(System.Int32 index)
  RVA=0x0B59DA2C  token=0x600005A  System.String GetStringKeyByIndex(System.Int32 index)
  RVA=0x0B59D720  token=0x600005B  System.Int32 GetIntKeyBySlotAndLocalIndex(Beyond.SparkBuffer.Runtime.Wrapper.HashSlot slot, System.Int32 localIndex)
  RVA=0x086D4544  token=0x600005C  System.Int64 GetLongKeyBySlotAndLocalIndex(Beyond.SparkBuffer.Runtime.Wrapper.HashSlot slot, System.Int32 localIndex)
  RVA=0x0B59DB28  token=0x600005D  System.String GetStringKeyBySlotAndLocalIndex(Beyond.SparkBuffer.Runtime.Wrapper.HashSlot slot, System.Int32 localIndex)
  RVA=0x03291800  token=0x600005E  System.Boolean ContainsKey(System.String key)
  RVA=0x0B59D11C  token=0x600005F  System.Boolean GetBool(System.String key)
  RVA=0x037534D0  token=0x6000060  System.Boolean TryGetBool(System.String key, System.Boolean& value)
  RVA=0x0B59D200  token=0x6000061  System.Byte GetByte(System.String key)
  RVA=0x0B59DE90  token=0x6000062  System.Boolean TryGetByte(System.String key, System.Byte& value)
  RVA=0x03BAFE50  token=0x6000063  System.Int32 GetInt(System.String key)
  RVA=0x0353C320  token=0x6000064  System.Boolean TryGetInt(System.String key, System.Int32& value)
  RVA=0x0B59D9A8  token=0x6000065  System.Int64 GetLong(System.String key)
  RVA=0x0B59E238  token=0x6000066  System.Boolean TryGetLong(System.String key, System.Int64& value)
  RVA=0x0B59D4EC  token=0x6000067  System.Single GetFloat(System.String key)
  RVA=0x0B59E0D0  token=0x6000068  System.Boolean TryGetFloat(System.String key, System.Single& value)
  RVA=0x0B59D2B0  token=0x6000069  System.Double GetDouble(System.String key)
  RVA=0x0B59DF20  token=0x600006A  System.Boolean TryGetDouble(System.String key, System.Double& value)
  RVA=0x0B59DC38  token=0x600006B  System.String GetString(System.String key)
  RVA=0x041EF060  token=0x600006C  System.Boolean TryGetString(System.String key, System.String& value)
  RVA=0x0B59D394  token=0x600006D  System.Int32 GetEnum(System.String key)
  RVA=0x0B59E088  token=0x600006E  System.Boolean TryGetEnum(System.String key, System.Int32& value)
  RVA=0x038C2C50  token=0x600006F  System.Nullable<Beyond.SparkBuffer.Runtime.Bean> GetBean(System.String key)
  RVA=0x038C3170  token=0x6000070  System.Boolean TryGetBean(System.String key, System.Nullable<Beyond.SparkBuffer.Runtime.Bean>& bean)
  RVA=0x0331BB50  token=0x6000071  System.Boolean ContainsKey(System.Int32 key)
  RVA=0x0B59D150  token=0x6000072  System.Boolean GetBool(System.Int32 key)
  RVA=0x0B59DDB8  token=0x6000073  System.Boolean TryGetBool(System.Int32 key, System.Boolean& value)
  RVA=0x0B59D1CC  token=0x6000074  System.Byte GetByte(System.Int32 key)
  RVA=0x0B59DE48  token=0x6000075  System.Boolean TryGetByte(System.Int32 key, System.Byte& value)
  RVA=0x0B59D3FC  token=0x6000076  System.Int32 GetInt(System.Int32 key)
  RVA=0x03EBB2B0  token=0x6000077  System.Boolean TryGetInt(System.Int32 key, System.Int32& value)
  RVA=0x0B59D9DC  token=0x6000078  System.Int64 GetLong(System.Int32 key)
  RVA=0x0B59E1F0  token=0x6000079  System.Boolean TryGetLong(System.Int32 key, System.Int64& value)
  RVA=0x0B59D520  token=0x600007A  System.Single GetFloat(System.Int32 key)
  RVA=0x0B59E160  token=0x600007B  System.Boolean TryGetFloat(System.Int32 key, System.Single& value)
  RVA=0x0B59D2E4  token=0x600007C  System.Double GetDouble(System.Int32 key)
  RVA=0x0B59DFB0  token=0x600007D  System.Boolean TryGetDouble(System.Int32 key, System.Double& value)
  RVA=0x03062620  token=0x600007E  System.String GetString(System.Int32 key)
  RVA=0x0397BAB0  token=0x600007F  System.Boolean TryGetString(System.Int32 key, System.String& value)
  RVA=0x0B59D3FC  token=0x6000080  System.Int32 GetEnum(System.Int32 key)
  RVA=0x0B59E040  token=0x6000081  System.Boolean TryGetEnum(System.Int32 key, System.Int32& value)
  RVA=0x038C1150  token=0x6000082  System.Nullable<Beyond.SparkBuffer.Runtime.Bean> GetBean(System.Int32 key)
  RVA=0x038C20D0  token=0x6000083  System.Boolean TryGetBean(System.Int32 key, System.Nullable<Beyond.SparkBuffer.Runtime.Bean>& bean)
  RVA=0x0B59CFE4  token=0x6000084  System.Boolean ContainsKey(System.Int64 key)
  RVA=0x0B59D0E8  token=0x6000085  System.Boolean GetBool(System.Int64 key)
  RVA=0x0B59DE00  token=0x6000086  System.Boolean TryGetBool(System.Int64 key, System.Boolean& value)
  RVA=0x0B59D234  token=0x6000087  System.Byte GetByte(System.Int64 key)
  RVA=0x0B59DED8  token=0x6000088  System.Boolean TryGetByte(System.Int64 key, System.Byte& value)
  RVA=0x0B59D3C8  token=0x6000089  System.Int32 GetInt(System.Int64 key)
  RVA=0x0B59DFF8  token=0x600008A  System.Boolean TryGetInt(System.Int64 key, System.Int32& value)
  RVA=0x0B59D974  token=0x600008B  System.Int64 GetLong(System.Int64 key)
  RVA=0x0B59E1A8  token=0x600008C  System.Boolean TryGetLong(System.Int64 key, System.Int64& value)
  RVA=0x0B59D554  token=0x600008D  System.Single GetFloat(System.Int64 key)
  RVA=0x0B59E118  token=0x600008E  System.Boolean TryGetFloat(System.Int64 key, System.Single& value)
  RVA=0x0B59D318  token=0x600008F  System.Double GetDouble(System.Int64 key)
  RVA=0x0B59DF68  token=0x6000090  System.Boolean TryGetDouble(System.Int64 key, System.Double& value)
  RVA=0x0B59DC6C  token=0x6000091  System.String GetString(System.Int64 key)
  RVA=0x039CE830  token=0x6000092  System.Boolean TryGetString(System.Int64 key, System.String& value)
  RVA=0x0B59D3C8  token=0x6000093  System.Int32 GetEnum(System.Int64 key)
  RVA=0x0B59DFF8  token=0x6000094  System.Boolean TryGetEnum(System.Int64 key, System.Int32& value)
  RVA=0x0B59D038  token=0x6000095  System.Nullable<Beyond.SparkBuffer.Runtime.Bean> GetBean(System.Int64 key)
  RVA=0x039CE7F0  token=0x6000096  System.Boolean TryGetBean(System.Int64 key, System.Nullable<Beyond.SparkBuffer.Runtime.Bean>& bean)
  RVA=0x0B59D0A0  token=0x6000097  System.Boolean GetBoolByIndex(System.Int32 index)
  RVA=0x0B59D184  token=0x6000098  System.Byte GetByteByIndex(System.Int32 index)
  RVA=0x0B59D34C  token=0x6000099  System.Int32 GetIntByIndex(System.Int32 index)
  RVA=0x0B59D830  token=0x600009A  System.Int64 GetLongByIndex(System.Int32 index)
  RVA=0x03E31D60  token=0x600009B  System.Single GetFloatByIndex(System.Int32 index)
  RVA=0x0B59D268  token=0x600009C  System.Double GetDoubleByIndex(System.Int32 index)
  RVA=0x036A33B0  token=0x600009D  System.String GetStringByIndex(System.Int32 index)
  RVA=0x0B59D34C  token=0x600009E  System.Int32 GetEnumByIndex(System.Int32 index)
  RVA=0x038C2650  token=0x600009F  System.Nullable<Beyond.SparkBuffer.Runtime.Bean> GetBeanByIndex(System.Int32 index)
  RVA=0x0B59E524  token=0x60000A0  System.Int32 _GetOffsetByIndex(System.Int32 index)
  RVA=0x0B59D0CC  token=0x60000A1  System.Boolean GetBoolByOffset(System.Int32 offset)
  RVA=0x0B59D1B0  token=0x60000A2  System.Byte GetByteByOffset(System.Int32 offset)
  RVA=0x0B59D378  token=0x60000A3  System.Int32 GetIntByOffset(System.Int32 offset)
  RVA=0x0B59D85C  token=0x60000A4  System.Int64 GetLongByOffset(System.Int32 offset)
  RVA=0x0B59D4D0  token=0x60000A5  System.Single GetFloatByOffset(System.Int32 offset)
  RVA=0x0B59D294  token=0x60000A6  System.Double GetDoubleByOffset(System.Int32 offset)
  RVA=0x0B59DA10  token=0x60000A7  System.String GetStringByOffset(System.Int32 offset)
  RVA=0x0B59D378  token=0x60000A8  System.Int32 GetEnumByOffset(System.Int32 offset)
  RVA=0x0B59CFF8  token=0x60000A9  System.Nullable<Beyond.SparkBuffer.Runtime.Bean> GetBeanByOffset(System.Int32 offset)
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Root
TYPE:  class
TOKEN: 0x200000E
SIZE:  0x50
IMPLEMENTS: System.IDisposable
FIELDS:
  private           Beyond.SparkBuffer.Runtime.ByteReaderm_reader  // 0x10
  private   readonly Beyond.SparkBuffer.Runtime.Root.Optionsm_options  // 0x18
  private   readonly System.Int32                    m_dataOffset  // 0x40
  private           System.Collections.Generic.Dictionary<System.Int32,System.String>m_cachedString  // 0x48
PROPERTIES:
  name  get=0x0385B100
  bytes  get=0x03FA9AE0
METHODS:
  RVA=0x02F20A00  token=0x60000B5  System.Void .ctor(System.Int32 dataOffset, Beyond.SparkBuffer.Runtime.Root.Options options, Beyond.SparkBuffer.Runtime.ByteReader reader)
  RVA=0x0B5A1184  token=0x60000B6  System.Void Finalize()
  RVA=0x0B5A180C  token=0x60000B7  System.String ToString()
  RVA=0x0B5A1154  token=0x60000B8  System.Void Dispose()
  RVA=0x0A77DD74  token=0x60000B9  System.Boolean IsValid()
  RVA=0x0B5A0EEC  token=0x60000BA  System.Nullable<Beyond.SparkBuffer.Runtime.Bean> AsBean()
  RVA=0x0B5A0DC8  token=0x60000BB  System.Nullable<Beyond.SparkBuffer.Runtime.Array> AsArray()
  RVA=0x0B5A0FA8  token=0x60000BC  System.Nullable<Beyond.SparkBuffer.Runtime.Map> AsMap()
  RVA=0x0B5A14F4  token=0x60000BD  System.Int32 GetCurReaderPosition()
  RVA=0x0B5A14B8  token=0x60000BE  System.Boolean GetBool(System.Int32 offset)
  RVA=0x0B594C64  token=0x60000BF  System.Byte GetByte(System.Int32 offset)
  RVA=0x0B5A1538  token=0x60000C0  System.Int32 GetInt(System.Int32 offset)
  RVA=0x039CEA70  token=0x60000C1  System.Int64 GetLong(System.Int32 offset)
  RVA=0x03E31E30  token=0x60000C2  System.Single GetFloat(System.Int32 offset)
  RVA=0x0B5A1510  token=0x60000C3  System.Double GetDouble(System.Int32 offset)
  RVA=0x0B5A1538  token=0x60000C4  System.Int32 GetEnum(System.Int32 offset)
  RVA=0x0B5A1704  token=0x60000C5  System.String GetString(System.Int32 offset)
  RVA=0x0B5A1394  token=0x60000C6  System.Nullable<Beyond.SparkBuffer.Runtime.Bean> GetBean(System.Int32 offset, Beyond.SparkBuffer.Runtime.BeanType beanType)
  RVA=0x0B5A124C  token=0x60000C7  System.Nullable<Beyond.SparkBuffer.Runtime.Array> GetArray(System.Int32 offset, Beyond.SparkBuffer.Runtime.ArrayType arrayType)
  RVA=0x0B5A1560  token=0x60000C8  System.Nullable<Beyond.SparkBuffer.Runtime.Map> GetMap(System.Int32 offset, Beyond.SparkBuffer.Runtime.MapType mapType)
  RVA=0x03060940  token=0x60000C9  Beyond.SparkBuffer.Runtime.Wrapper.HashSlot GetHashSlot(System.Int32 offset)
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Error
TYPE:  static class
TOKEN: 0x2000010
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B596830  token=0x60000CA  System.Exception TypeNotFoundError(System.Int32 typeHash, Beyond.SparkBuffer.SparkType sparkType)
  RVA=0x0B59668C  token=0x60000CB  System.Exception InvalidTypeError(Beyond.SparkBuffer.SparkType sparkType)
  RVA=0x0B5965EC  token=0x60000CC  System.Exception InvalidMapKeyError(Beyond.SparkBuffer.SparkType sparkType)
  RVA=0x0B59672C  token=0x60000CD  System.Exception NullTypeError(Beyond.SparkBuffer.SparkType sparkType)
  RVA=0x0B596588  token=0x60000CE  System.Exception GetEndOfFile()
  RVA=0x0B5967CC  token=0x60000CF  System.Exception SeekAheadOfFile()
  RVA=0x0B596524  token=0x60000D0  System.Exception DisposedException()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.SparkManager
TYPE:  abstract class
TOKEN: 0x2000011
SIZE:  0x38
FIELDS:
  private   readonly System.Collections.Generic.Dictionary<System.Int32,Beyond.SparkBuffer.Runtime.BeanType>m_beanTypeMap  // 0x10
  private   readonly System.Collections.Generic.Dictionary<System.Int32,Beyond.SparkBuffer.Runtime.EnumType>m_enumTypeMap  // 0x18
  protected readonly System.Collections.Generic.Dictionary<System.String,Beyond.SparkBuffer.Runtime.Root>m_namedRoots  // 0x20
  private   readonly System.Collections.Generic.List<Beyond.SparkBuffer.Runtime.Root>m_anonymousRoots  // 0x28
  private   readonly System.Collections.Generic.Queue<Beyond.SparkBuffer.Runtime.BeanType>m_beanTypeQueue  // 0x30
METHODS:
  RVA=0x04A1E470  token=0x60000D1  System.Void .ctor()
  RVA=0x02F212C0  token=0x60000D2  Beyond.SparkBuffer.Runtime.Root Load(System.Byte[] bytes, System.String overrideRootName)
  RVA=0x0B5A2ED0  token=0x60000D3  Beyond.SparkBuffer.Runtime.Root Load(System.Byte[] bytes)
  RVA=0x0B5A29F0  token=0x60000D4  Beyond.SparkBuffer.Runtime.Root LoadFromJson_EditorOnly(System.Byte[] bytes, Newtonsoft.Json.Linq.JToken jsonData, System.String overrideRootName)
  RVA=0x0B5A29D4  token=0x60000D5  Beyond.SparkBuffer.Runtime.Root LoadFromJson_EditorOnly(System.Byte[] bytes, Newtonsoft.Json.Linq.JToken jsonData)
  RVA=0x0B5A3098  token=0x60000D6  System.Boolean Unload(Beyond.SparkBuffer.Runtime.Root root)
  RVA=0x0B5A2EDC  token=0x60000D7  System.Void UnloadAndClearAll()
  RVA=0x03D6EB00  token=0x60000D8  System.Boolean TryGetEnumType(System.Int32 typeHash, Beyond.SparkBuffer.Runtime.EnumType& enumType)
  RVA=0x03D6EA90  token=0x60000D9  System.Boolean TryGetBeanType(System.Int32 typeHash, Beyond.SparkBuffer.Runtime.BeanType& beanType)
  RVA=0x0408E180  token=0x60000DA  System.Boolean _AddRoot(Beyond.SparkBuffer.Runtime.Root root)
  RVA=0x03D6E700  token=0x60000DB  System.Void _ReadTypeDefs(Beyond.SparkBuffer.Runtime.ByteReader reader)
  RVA=0x02F21400  token=0x60000DC  Beyond.SparkBuffer.Runtime.Root.Options _ReadRootDef(Beyond.SparkBuffer.Runtime.ByteReader reader, System.String overrideRootName)
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.RuntimeSparkManager
TYPE:  class
TOKEN: 0x2000014
SIZE:  0x50
EXTENDS: Beyond.SparkBuffer.Runtime.SparkManager
FIELDS:
  private   static  Beyond.SparkBuffer.Runtime.RuntimeSparkManagers_instance  // static @ 0x0
  private           System.Action<Beyond.SparkBuffer.Runtime.Root>m_onRootLoaded  // 0x38
  private           System.Action<Beyond.SparkBuffer.Runtime.Root>m_onRootUnloaded  // 0x40
  private           System.Action                   m_onUnloadAll  // 0x48
  private   static  System.String                   LUADLL  // const
PROPERTIES:
  instance  get=0x042D25E0
METHODS:
  RVA=0x0B5A1A3C  token=0x60000F4  System.Void .ctor()
  RVA=0x0B5A1994  token=0x60000F5  System.Boolean Unload(Beyond.SparkBuffer.Runtime.Root root)
  RVA=0x0B5A18C4  token=0x60000F6  System.Void ReloadTableToLua(System.String name)
  RVA=0x0B5A1944  token=0x60000F7  System.Void UnloadAndClearAll()
  RVA=0x03FA9B10  token=0x60000F8  System.Boolean LuaSpark_LoadRoot(System.IntPtr L, System.String rootName, System.IntPtr data, System.Int32 size)
  RVA=0x0B5A1884  token=0x60000F9  System.Boolean LuaSpark_UnloadRoot(System.IntPtr L, System.String rootName)
  RVA=0x0B5A187C  token=0x60000FA  System.Void LuaSpark_UnloadAll(System.IntPtr L)
  RVA=0x04175010  token=0x60000FB  System.Void RegisterToLua(System.IntPtr L)
  RVA=0x0B5A19FC  token=0x60000FC  System.Void UnregisterFromLua(System.IntPtr L)
  RVA=0x0408E110  token=0x60000FD  System.Boolean _AddRoot(Beyond.SparkBuffer.Runtime.Root root)
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.IStringPool
TYPE:  interface
TOKEN: 0x2000016
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000102  System.Int32 AddString(System.String str)
  RVA=-1  // abstract  token=0x6000103  System.String GetString(System.Int32 index)
  RVA=-1  // abstract  token=0x6000104  System.Void Clear()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Field
TYPE:  abstract class
TOKEN: 0x2000017
SIZE:  0x20
FIELDS:
  private   readonly System.String                   <name>k__BackingField  // 0x10
  private   readonly System.Int32                    <offset>k__BackingField  // 0x18
PROPERTIES:
  name  get=0x02B2ECC0
  offset  get=0x011EF5B0
  type  get=-1  // abstract
METHODS:
  RVA=0x03B46FF0  token=0x6000108  System.Void .ctor(System.String fieldName, System.Int32 fieldOffset)
  RVA=-1  // abstract  token=0x6000109  System.Void Unmarshal(Beyond.SparkBuffer.Runtime.ByteReader reader)
  RVA=-1  // abstract  token=0x600010A  System.Void Resolve(Beyond.SparkBuffer.Runtime.SparkManager manager)
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.SimpleField
TYPE:  class
TOKEN: 0x2000018
SIZE:  0x28
EXTENDS: Beyond.SparkBuffer.Runtime.Field
FIELDS:
  private   readonly Beyond.SparkBuffer.SparkType    <type>k__BackingField  // 0x20
PROPERTIES:
  type  get=0x04D866B0
METHODS:
  RVA=0x05391C28  token=0x600010C  System.Void .ctor(Beyond.SparkBuffer.SparkType type, System.String fieldName, System.Int32 fieldOffset)
  RVA=0x041E1670  token=0x600010D  System.Void Unmarshal(Beyond.SparkBuffer.Runtime.ByteReader reader)
  RVA=0x041E1670  token=0x600010E  System.Void Resolve(Beyond.SparkBuffer.Runtime.SparkManager manager)
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.EnumField
TYPE:  class
TOKEN: 0x2000019
SIZE:  0x30
EXTENDS: Beyond.SparkBuffer.Runtime.Field
FIELDS:
  private           System.Int32                    m_typeHash  // 0x20
  private           Beyond.SparkBuffer.Runtime.EnumTypem_enumType  // 0x28
PROPERTIES:
  type  get=0x04DBF470
  enumType  get=0x04D86240
METHODS:
  RVA=0x03B46FF0  token=0x6000111  System.Void .ctor(System.String fieldName, System.Int32 fieldOffset)
  RVA=0x0403C2E0  token=0x6000112  System.Void Unmarshal(Beyond.SparkBuffer.Runtime.ByteReader reader)
  RVA=0x03D6EAC0  token=0x6000113  System.Void Resolve(Beyond.SparkBuffer.Runtime.SparkManager manager)
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.BeanField
TYPE:  class
TOKEN: 0x200001A
SIZE:  0x30
EXTENDS: Beyond.SparkBuffer.Runtime.Field
FIELDS:
  private           System.Int32                    m_typeHash  // 0x20
  private           Beyond.SparkBuffer.Runtime.BeanTypem_beanType  // 0x28
PROPERTIES:
  type  get=0x04D920C0
  beanType  get=0x04D86240
METHODS:
  RVA=0x03B46FF0  token=0x6000116  System.Void .ctor(System.String fieldName, System.Int32 fieldOffset)
  RVA=0x0403C310  token=0x6000117  System.Void Unmarshal(Beyond.SparkBuffer.Runtime.ByteReader reader)
  RVA=0x03D6EA50  token=0x6000118  System.Void Resolve(Beyond.SparkBuffer.Runtime.SparkManager manager)
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.ArrayField
TYPE:  class
TOKEN: 0x200001B
SIZE:  0x40
EXTENDS: Beyond.SparkBuffer.Runtime.Field
FIELDS:
  private           Beyond.SparkBuffer.Runtime.ArrayTypem_arrayType  // 0x20
  private           System.Int32                    m_elementTypeHash  // 0x38
PROPERTIES:
  type  get=0x04DBF430
  arrayType  get=0x04D9E6A0
METHODS:
  RVA=0x03B46FF0  token=0x600011B  System.Void .ctor(System.String fieldName, System.Int32 fieldOffset)
  RVA=0x0403C250  token=0x600011C  System.Void Unmarshal(Beyond.SparkBuffer.Runtime.ByteReader reader)
  RVA=0x03D6E9E0  token=0x600011D  System.Void Resolve(Beyond.SparkBuffer.Runtime.SparkManager manager)
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.MapField
TYPE:  class
TOKEN: 0x200001C
SIZE:  0x50
EXTENDS: Beyond.SparkBuffer.Runtime.Field
FIELDS:
  private           Beyond.SparkBuffer.Runtime.MapTypem_mapInfo  // 0x20
  private           System.Int32                    m_keyTypeHash  // 0x48
  private           System.Int32                    m_valueTypeHash  // 0x4c
PROPERTIES:
  type  get=0x04DBF480
  mapType  get=0x04D8E8C0
METHODS:
  RVA=0x03B46FF0  token=0x6000120  System.Void .ctor(System.String fieldName, System.Int32 fieldOffset)
  RVA=0x0403C1E0  token=0x6000121  System.Void Unmarshal(Beyond.SparkBuffer.Runtime.ByteReader reader)
  RVA=0x03D6E980  token=0x6000122  System.Void Resolve(Beyond.SparkBuffer.Runtime.SparkManager manager)
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.ArrayType
TYPE:  sealed struct
TOKEN: 0x200001D
SIZE:  0x28
FIELDS:
  public            Beyond.SparkBuffer.SparkType    elementType  // 0x10
  public            Beyond.SparkBuffer.Runtime.BeanTypeelementBeanType  // 0x18
  public            Beyond.SparkBuffer.Runtime.EnumTypeelementEnumType  // 0x20
METHODS:
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.MapType
TYPE:  sealed struct
TOKEN: 0x200001E
SIZE:  0x38
FIELDS:
  public            Beyond.SparkBuffer.SparkType    keyType  // 0x10
  public            Beyond.SparkBuffer.Runtime.EnumTypekeyEnumType  // 0x18
  public            Beyond.SparkBuffer.SparkType    valueType  // 0x20
  public            Beyond.SparkBuffer.Runtime.BeanTypevalueBeanType  // 0x28
  public            Beyond.SparkBuffer.Runtime.EnumTypevalueEnumType  // 0x30
METHODS:
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.EnumType
TYPE:  class
TOKEN: 0x200001F
SIZE:  0x28
FIELDS:
  private           System.Collections.Generic.List<Beyond.SparkBuffer.Runtime.EnumType.EnumItem>m_enums  // 0x10
  private           System.Int32                    <typeHash>k__BackingField  // 0x18
  private           System.String                   <name>k__BackingField  // 0x20
PROPERTIES:
  typeHash  get=0x011EF5B0  set=0x011EF9B0
  name  get=0x04D862C0  set=0x05390F40
METHODS:
  RVA=0x03292CE0  token=0x6000127  System.Void .ctor(Beyond.SparkBuffer.Runtime.ByteReader reader)
  RVA=0x0B596458  token=0x6000128  System.Boolean TryGetValue(System.String enumName, System.Int32& enumVal)
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.BeanType
TYPE:  class
TOKEN: 0x2000021
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           System.Collections.Generic.Dictionary<System.String,Beyond.SparkBuffer.Runtime.Field>m_fields  // 0x10
  private           System.Int32                    <typeHash>k__BackingField  // 0x18
  private           System.String                   <name>k__BackingField  // 0x20
PROPERTIES:
  typeHash  get=0x011EF5B0  set=0x011EF9B0
  name  get=0x04D862C0  set=0x05390F40
METHODS:
  RVA=0x03292A70  token=0x600012D  System.Void .ctor(Beyond.SparkBuffer.Runtime.ByteReader reader)
  RVA=0x0B595110  token=0x600012E  System.String ToString()
  RVA=0x03D6E890  token=0x600012F  System.Void Resolve(Beyond.SparkBuffer.Runtime.SparkManager manager)
  RVA=0x0305FED0  token=0x6000130  System.Boolean TryGetField(System.String fieldName, Beyond.SparkBuffer.Runtime.Field& field)
  RVA=0x0B594FC8  token=0x6000131  System.Collections.Generic.Dictionary.Enumerator<System.String,Beyond.SparkBuffer.Runtime.Field> GetEnumerator()
  RVA=0x0B595040  token=0x6000132  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,Beyond.SparkBuffer.Runtime.Field>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,Beyond.SparkBuffer.Runtime.Field>>.GetEnumerator()
  RVA=0x0B5950A8  token=0x6000133  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=0x03292ED0  token=0x6000134  Beyond.SparkBuffer.Runtime.Field _NewField(System.String name, Beyond.SparkBuffer.SparkType type, System.Int32 offset)
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.IArrayWrapper`1
TYPE:  interface
TOKEN: 0x2000022
FIELDS:
PROPERTIES:
  length  get=-1  // abstract
  Count  get=-1  // abstract
  Item  get=-1  // abstract
METHODS:
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.ArrayEnumerator`2
TYPE:  sealed struct
TOKEN: 0x2000023
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private   readonly TArray                          m_parent  // 0x0
  private           System.Int32                    m_index  // 0x0
PROPERTIES:
  Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600013A  System.Void .ctor(TArray parent)
  RVA=-1  // not resolved  token=0x600013B  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x600013C  System.Void Dispose()
  RVA=-1  // not resolved  token=0x600013D  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.NullableBeanArray`1
TYPE:  sealed struct
TOKEN: 0x2000024
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IArrayWrapper`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           Beyond.SparkBuffer.Runtime.Arraym_array  // 0x0
  private           System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean>m_creator  // 0x0
PROPERTIES:
  length  get=-1  // not resolved
  Count  get=-1  // not resolved
  Item  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000140  System.Void .ctor(Beyond.SparkBuffer.Runtime.Array array, System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean> creator)
  RVA=-1  // not resolved  token=0x6000142  Beyond.SparkBuffer.Runtime.Wrapper.ArrayEnumerator<System.Nullable<TBean>,Beyond.SparkBuffer.Runtime.Wrapper.NullableBeanArray<TBean>> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000143  System.Collections.Generic.IEnumerator<System.Nullable<TBean>> System.Collections.Generic.IEnumerable<TBean?>.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000144  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.BeanArray`1
TYPE:  sealed struct
TOKEN: 0x2000025
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IArrayWrapper`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           Beyond.SparkBuffer.Runtime.Arraym_array  // 0x0
  private           System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean>m_creator  // 0x0
PROPERTIES:
  length  get=-1  // not resolved
  Count  get=-1  // not resolved
  Item  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000147  System.Void .ctor(Beyond.SparkBuffer.Runtime.Array array, System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean> creator)
  RVA=-1  // not resolved  token=0x6000149  Beyond.SparkBuffer.Runtime.Wrapper.ArrayEnumerator<TBean,Beyond.SparkBuffer.Runtime.Wrapper.BeanArray<TBean>> GetEnumerator()
  RVA=-1  // not resolved  token=0x600014A  System.Collections.Generic.IEnumerator<TBean> System.Collections.Generic.IEnumerable<TBean>.GetEnumerator()
  RVA=-1  // not resolved  token=0x600014B  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.EnumArray`1
TYPE:  sealed struct
TOKEN: 0x2000026
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IArrayWrapper`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           Beyond.SparkBuffer.Runtime.Arraym_array  // 0x0
  private           System.Func<System.Int32,TEnum> m_converter  // 0x0
PROPERTIES:
  length  get=-1  // not resolved
  Count  get=-1  // not resolved
  Item  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600014E  System.Void .ctor(Beyond.SparkBuffer.Runtime.Array array, System.Func<System.Int32,TEnum> converter)
  RVA=-1  // not resolved  token=0x6000150  Beyond.SparkBuffer.Runtime.Wrapper.ArrayEnumerator<TEnum,Beyond.SparkBuffer.Runtime.Wrapper.EnumArray<TEnum>> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000151  TEnum[] ToArray()
  RVA=-1  // not resolved  token=0x6000152  System.Boolean Contains(TEnum item)
  RVA=-1  // not resolved  token=0x6000153  System.Collections.Generic.IEnumerator<TEnum> System.Collections.Generic.IEnumerable<TEnum>.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000154  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.BoolArray
TYPE:  sealed struct
TOKEN: 0x2000027
SIZE:  0x38
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IArrayWrapper`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           Beyond.SparkBuffer.Runtime.Arraym_array  // 0x10
PROPERTIES:
  length  get=0x011F0020
  Count  get=0x011F0020
  Item  get=0x0B5957E8
METHODS:
  RVA=0x0B5957C4  token=0x6000157  System.Void .ctor(Beyond.SparkBuffer.Runtime.Array array)
  RVA=0x0B5955F0  token=0x6000159  Beyond.SparkBuffer.Runtime.Wrapper.ArrayEnumerator<System.Boolean,Beyond.SparkBuffer.Runtime.Wrapper.BoolArray> GetEnumerator()
  RVA=0x0B59573C  token=0x600015A  System.Boolean[] ToArray()
  RVA=0x0B5955A8  token=0x600015B  System.Boolean Contains(System.Boolean item)
  RVA=0x0B595664  token=0x600015C  System.Collections.Generic.IEnumerator<System.Boolean> System.Collections.Generic.IEnumerable<System.Boolean>.GetEnumerator()
  RVA=0x0B5956D0  token=0x600015D  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.ByteArray
TYPE:  sealed struct
TOKEN: 0x2000028
SIZE:  0x38
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IArrayWrapper`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           Beyond.SparkBuffer.Runtime.Arraym_array  // 0x10
PROPERTIES:
  length  get=0x011F0020
  Count  get=0x011F0020
  Item  get=0x0B595A0C
METHODS:
  RVA=0x0B5957C4  token=0x6000160  System.Void .ctor(Beyond.SparkBuffer.Runtime.Array array)
  RVA=0x0B595838  token=0x6000162  Beyond.SparkBuffer.Runtime.Wrapper.ArrayEnumerator<System.Byte,Beyond.SparkBuffer.Runtime.Wrapper.ByteArray> GetEnumerator()
  RVA=0x0B595984  token=0x6000163  System.Byte[] ToArray()
  RVA=0x0B5957F0  token=0x6000164  System.Boolean Contains(System.Byte item)
  RVA=0x0B5958AC  token=0x6000165  System.Collections.Generic.IEnumerator<System.Byte> System.Collections.Generic.IEnumerable<System.Byte>.GetEnumerator()
  RVA=0x0B595918  token=0x6000166  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.IntArray
TYPE:  sealed struct
TOKEN: 0x2000029
SIZE:  0x38
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IArrayWrapper`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           Beyond.SparkBuffer.Runtime.Arraym_array  // 0x10
PROPERTIES:
  length  get=0x011F0020
  Count  get=0x011F0020
  Item  get=0x0539316C
METHODS:
  RVA=0x0B5957C4  token=0x6000169  System.Void .ctor(Beyond.SparkBuffer.Runtime.Array array)
  RVA=0x046AB220  token=0x600016B  Beyond.SparkBuffer.Runtime.Wrapper.ArrayEnumerator<System.Int32,Beyond.SparkBuffer.Runtime.Wrapper.IntArray> GetEnumerator()
  RVA=0x0B5979DC  token=0x600016C  System.Int32[] ToArray()
  RVA=0x0B597928  token=0x600016D  System.Boolean Contains(System.Int32 item)
  RVA=0x046AB1B0  token=0x600016E  System.Collections.Generic.IEnumerator<System.Int32> System.Collections.Generic.IEnumerable<System.Int32>.GetEnumerator()
  RVA=0x0B597970  token=0x600016F  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.LongArray
TYPE:  sealed struct
TOKEN: 0x200002A
SIZE:  0x38
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IArrayWrapper`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           Beyond.SparkBuffer.Runtime.Arraym_array  // 0x10
PROPERTIES:
  length  get=0x011F0020
  Count  get=0x011F0020
  Item  get=0x0B59CFDC
METHODS:
  RVA=0x0B5957C4  token=0x6000172  System.Void .ctor(Beyond.SparkBuffer.Runtime.Array array)
  RVA=0x0B59CE04  token=0x6000174  Beyond.SparkBuffer.Runtime.Wrapper.ArrayEnumerator<System.Int64,Beyond.SparkBuffer.Runtime.Wrapper.LongArray> GetEnumerator()
  RVA=0x0B59CF50  token=0x6000175  System.Int64[] ToArray()
  RVA=0x0B59CDBC  token=0x6000176  System.Boolean Contains(System.Int64 item)
  RVA=0x0B59CE78  token=0x6000177  System.Collections.Generic.IEnumerator<System.Int64> System.Collections.Generic.IEnumerable<System.Int64>.GetEnumerator()
  RVA=0x0B59CEE4  token=0x6000178  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.FloatArray
TYPE:  sealed struct
TOKEN: 0x200002B
SIZE:  0x38
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IArrayWrapper`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           Beyond.SparkBuffer.Runtime.Arraym_array  // 0x10
PROPERTIES:
  length  get=0x011F0020
  Count  get=0x011F0020
  Item  get=0x05397784
METHODS:
  RVA=0x0B5957C4  token=0x600017B  System.Void .ctor(Beyond.SparkBuffer.Runtime.Array array)
  RVA=0x045FA690  token=0x600017D  Beyond.SparkBuffer.Runtime.Wrapper.ArrayEnumerator<System.Single,Beyond.SparkBuffer.Runtime.Wrapper.FloatArray> GetEnumerator()
  RVA=0x0B596988  token=0x600017E  System.Single[] ToArray()
  RVA=0x045FA620  token=0x600017F  System.Collections.Generic.IEnumerator<System.Single> System.Collections.Generic.IEnumerable<System.Single>.GetEnumerator()
  RVA=0x0B59691C  token=0x6000180  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.DoubleArray
TYPE:  sealed struct
TOKEN: 0x200002C
SIZE:  0x38
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IArrayWrapper`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           Beyond.SparkBuffer.Runtime.Arraym_array  // 0x10
PROPERTIES:
  length  get=0x011F0020
  Count  get=0x011F0020
  Item  get=0x053938D4
METHODS:
  RVA=0x0B5957C4  token=0x6000183  System.Void .ctor(Beyond.SparkBuffer.Runtime.Array array)
  RVA=0x0B596280  token=0x6000185  Beyond.SparkBuffer.Runtime.Wrapper.ArrayEnumerator<System.Double,Beyond.SparkBuffer.Runtime.Wrapper.DoubleArray> GetEnumerator()
  RVA=0x0B5963CC  token=0x6000186  System.Double[] ToArray()
  RVA=0x0B5962F4  token=0x6000187  System.Collections.Generic.IEnumerator<System.Double> System.Collections.Generic.IEnumerable<System.Double>.GetEnumerator()
  RVA=0x0B596360  token=0x6000188  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.StringArray
TYPE:  sealed struct
TOKEN: 0x200002D
SIZE:  0x38
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IArrayWrapper`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           Beyond.SparkBuffer.Runtime.Arraym_array  // 0x10
PROPERTIES:
  length  get=0x011F0020
  Count  get=0x011F0020
  Item  get=0x0539378C
METHODS:
  RVA=0x0B5957C4  token=0x600018B  System.Void .ctor(Beyond.SparkBuffer.Runtime.Array array)
  RVA=0x032F23B0  token=0x600018D  Beyond.SparkBuffer.Runtime.Wrapper.ArrayEnumerator<System.String,Beyond.SparkBuffer.Runtime.Wrapper.StringArray> GetEnumerator()
  RVA=0x03756AF0  token=0x600018E  System.String[] ToArray()
  RVA=0x03756B80  token=0x600018F  System.Boolean Contains(System.String item)
  RVA=0x046D98E0  token=0x6000190  System.Collections.Generic.IEnumerator<System.String> System.Collections.Generic.IEnumerable<System.String>.GetEnumerator()
  RVA=0x0B5A6954  token=0x6000191  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.IBeanWrapper
TYPE:  interface
TOKEN: 0x200002E
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000192  System.Void SetBean(Beyond.SparkBuffer.Runtime.Bean& bean)
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.HashSlot
TYPE:  sealed struct
TOKEN: 0x200002F
SIZE:  0x18
FIELDS:
  public            System.Int32                    offset  // 0x10
  public            System.Int32                    bucketsSize  // 0x14
  public    static readonly System.Int32                    size  // static @ 0x0
METHODS:
  RVA=0x04D7A490  token=0x6000193  System.Void .cctor()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3
TYPE:  interface
TOKEN: 0x2000030
FIELDS:
PROPERTIES:
  count  get=-1  // abstract
  Count  get=-1  // abstract
  Item  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000197  System.Boolean ContainsKey(TKey key)
  RVA=-1  // abstract  token=0x6000198  TValue GetValue(TKey key)
  RVA=-1  // abstract  token=0x6000199  System.Boolean TryGetValue(TKey key, TValue& value)
  RVA=-1  // abstract  token=0x600019A  TKey GetKeyByIndex(System.Int32 index)
  RVA=-1  // abstract  token=0x600019B  TValue GetValueByIndex(System.Int32 index)
  RVA=-1  // abstract  token=0x600019C  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<TKey,TValue,TMap> GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator`3
TYPE:  sealed struct
TOKEN: 0x2000031
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private   readonly TMap                            m_parent  // 0x0
  private           System.Int32                    m_index  // 0x0
PROPERTIES:
  Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600019F  System.Void .ctor(TMap parent)
  RVA=-1  // not resolved  token=0x60001A0  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x60001A1  System.Void Dispose()
  RVA=-1  // not resolved  token=0x60001A2  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.S2NullableBeanMap`1
TYPE:  sealed struct
TOKEN: 0x2000032
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_map  // 0x0
  private   readonly System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean>m_creator  // 0x0
PROPERTIES:
  count  get=-1  // not resolved
  Count  get=-1  // not resolved
  Item  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60001A6  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map, System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean> creator)
  RVA=-1  // not resolved  token=0x60001A7  System.Boolean ContainsKey(System.String key)
  RVA=-1  // not resolved  token=0x60001A8  System.String GetKeyByIndex(System.Int32 index)
  RVA=-1  // not resolved  token=0x60001A9  System.Nullable<TBean> GetValue(System.String key)
  RVA=-1  // not resolved  token=0x60001AA  System.Boolean TryGetValue(System.String key, System.Nullable<TBean>& value)
  RVA=-1  // not resolved  token=0x60001AB  System.Nullable<TBean> GetValueByIndex(System.Int32 index)
  RVA=-1  // not resolved  token=0x60001AC  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.String,System.Nullable<TBean>,Beyond.SparkBuffer.Runtime.Wrapper.S2NullableBeanMap<TBean>> GetEnumerator()
  RVA=-1  // not resolved  token=0x60001AD  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,System.Nullable<TBean>>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,TBean?>>.GetEnumerator()
  RVA=-1  // not resolved  token=0x60001AE  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.S2BeanMap`1
TYPE:  sealed struct
TOKEN: 0x2000033
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_map  // 0x0
  private   readonly System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean>m_creator  // 0x0
PROPERTIES:
  count  get=-1  // not resolved
  Count  get=-1  // not resolved
  Item  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60001B2  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map, System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean> creator)
  RVA=-1  // not resolved  token=0x60001B3  System.Boolean ContainsKey(System.String key)
  RVA=-1  // not resolved  token=0x60001B4  System.String GetKeyByIndex(System.Int32 index)
  RVA=-1  // not resolved  token=0x60001B5  TBean GetValue(System.String key)
  RVA=-1  // not resolved  token=0x60001B6  System.Boolean TryGetValue(System.String key, TBean& value)
  RVA=-1  // not resolved  token=0x60001B7  TBean GetValueByIndex(System.Int32 index)
  RVA=-1  // not resolved  token=0x60001B8  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.String,TBean,Beyond.SparkBuffer.Runtime.Wrapper.S2BeanMap<TBean>> GetEnumerator()
  RVA=-1  // not resolved  token=0x60001B9  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,TBean>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,TBean>>.GetEnumerator()
  RVA=-1  // not resolved  token=0x60001BA  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.S2EnumMap`1
TYPE:  sealed struct
TOKEN: 0x2000034
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_map  // 0x0
  private           System.Func<System.Int32,TEnum> m_converter  // 0x0
PROPERTIES:
  count  get=-1  // not resolved
  Count  get=-1  // not resolved
  Item  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60001BE  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map, System.Func<System.Int32,TEnum> converter)
  RVA=-1  // not resolved  token=0x60001BF  System.Boolean ContainsKey(System.String key)
  RVA=-1  // not resolved  token=0x60001C0  System.String GetKeyByIndex(System.Int32 index)
  RVA=-1  // not resolved  token=0x60001C1  TEnum GetValue(System.String key)
  RVA=-1  // not resolved  token=0x60001C2  System.Boolean TryGetValue(System.String key, TEnum& value)
  RVA=-1  // not resolved  token=0x60001C3  TEnum GetValueByIndex(System.Int32 index)
  RVA=-1  // not resolved  token=0x60001C4  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.String,TEnum,Beyond.SparkBuffer.Runtime.Wrapper.S2EnumMap<TEnum>> GetEnumerator()
  RVA=-1  // not resolved  token=0x60001C5  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,TEnum>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,TEnum>>.GetEnumerator()
  RVA=-1  // not resolved  token=0x60001C6  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.S2BoolMap
TYPE:  sealed struct
TOKEN: 0x2000035
SIZE:  0x58
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_map  // 0x10
PROPERTIES:
  count  get=0x02BB2A50
  Count  get=0x02BB2A50
  Item  get=0x0B5A1B1C
METHODS:
  RVA=0x09DC2B6C  token=0x60001CA  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map)
  RVA=0x03290380  token=0x60001CB  System.Boolean ContainsKey(System.String key)
  RVA=0x04240900  token=0x60001CC  System.String GetKeyByIndex(System.Int32 index)
  RVA=0x0B5A1B1C  token=0x60001CD  System.Boolean GetValue(System.String key)
  RVA=0x03753440  token=0x60001CE  System.Boolean TryGetValue(System.String key, System.Boolean& value)
  RVA=0x0B596AE4  token=0x60001CF  System.Boolean GetValueByIndex(System.Int32 index)
  RVA=0x0B5A1A44  token=0x60001D0  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.String,System.Boolean,Beyond.SparkBuffer.Runtime.Wrapper.S2BoolMap> GetEnumerator()
  RVA=0x0B5A1B60  token=0x60001D1  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,System.Boolean>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,System.Boolean>>.GetEnumerator()
  RVA=0x0B5A1BE0  token=0x60001D2  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.S2ByteMap
TYPE:  sealed struct
TOKEN: 0x2000036
SIZE:  0x58
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_map  // 0x10
PROPERTIES:
  count  get=0x02BB2A50
  Count  get=0x02BB2A50
  Item  get=0x0B5A1D38
METHODS:
  RVA=0x09DC2B6C  token=0x60001D6  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map)
  RVA=0x03290380  token=0x60001D7  System.Boolean ContainsKey(System.String key)
  RVA=0x04240900  token=0x60001D8  System.String GetKeyByIndex(System.Int32 index)
  RVA=0x0B5A1D38  token=0x60001D9  System.Byte GetValue(System.String key)
  RVA=0x0B5A1E7C  token=0x60001DA  System.Boolean TryGetValue(System.String key, System.Byte& value)
  RVA=0x0B596D80  token=0x60001DB  System.Byte GetValueByIndex(System.Int32 index)
  RVA=0x0B5A1C60  token=0x60001DC  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.String,System.Byte,Beyond.SparkBuffer.Runtime.Wrapper.S2ByteMap> GetEnumerator()
  RVA=0x0B5A1D7C  token=0x60001DD  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,System.Byte>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,System.Byte>>.GetEnumerator()
  RVA=0x0B5A1DFC  token=0x60001DE  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.S2IntMap
TYPE:  sealed struct
TOKEN: 0x2000037
SIZE:  0x58
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_map  // 0x10
PROPERTIES:
  count  get=0x02BB2A50
  Count  get=0x02BB2A50
  Item  get=0x03BAFD90
METHODS:
  RVA=0x09DC2B6C  token=0x60001E2  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map)
  RVA=0x03290380  token=0x60001E3  System.Boolean ContainsKey(System.String key)
  RVA=0x04240900  token=0x60001E4  System.String GetKeyByIndex(System.Int32 index)
  RVA=0x03BAFD90  token=0x60001E5  System.Int32 GetValue(System.String key)
  RVA=0x0353C290  token=0x60001E6  System.Boolean TryGetValue(System.String key, System.Int32& value)
  RVA=0x0B5973C0  token=0x60001E7  System.Int32 GetValueByIndex(System.Int32 index)
  RVA=0x0B5A2380  token=0x60001E8  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.String,System.Int32,Beyond.SparkBuffer.Runtime.Wrapper.S2IntMap> GetEnumerator()
  RVA=0x0B5A2458  token=0x60001E9  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,System.Int32>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,System.Int32>>.GetEnumerator()
  RVA=0x0B5A24D8  token=0x60001EA  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.S2LongMap
TYPE:  sealed struct
TOKEN: 0x2000038
SIZE:  0x58
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_map  // 0x10
PROPERTIES:
  count  get=0x02BB2A50
  Count  get=0x02BB2A50
  Item  get=0x0B5A2630
METHODS:
  RVA=0x09DC2B6C  token=0x60001EE  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map)
  RVA=0x03290380  token=0x60001EF  System.Boolean ContainsKey(System.String key)
  RVA=0x04240900  token=0x60001F0  System.String GetKeyByIndex(System.Int32 index)
  RVA=0x0B5A2630  token=0x60001F1  System.Int64 GetValue(System.String key)
  RVA=0x0B5A2774  token=0x60001F2  System.Boolean TryGetValue(System.String key, System.Int64& value)
  RVA=0x0B59765C  token=0x60001F3  System.Int64 GetValueByIndex(System.Int32 index)
  RVA=0x0B5A2558  token=0x60001F4  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.String,System.Int64,Beyond.SparkBuffer.Runtime.Wrapper.S2LongMap> GetEnumerator()
  RVA=0x0B5A2674  token=0x60001F5  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,System.Int64>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,System.Int64>>.GetEnumerator()
  RVA=0x0B5A26F4  token=0x60001F6  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.S2FloatMap
TYPE:  sealed struct
TOKEN: 0x2000039
SIZE:  0x58
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_map  // 0x10
PROPERTIES:
  count  get=0x02BB2A50
  Count  get=0x02BB2A50
  Item  get=0x0B5A21F8
METHODS:
  RVA=0x09DC2B6C  token=0x60001FA  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map)
  RVA=0x03290380  token=0x60001FB  System.Boolean ContainsKey(System.String key)
  RVA=0x04240900  token=0x60001FC  System.String GetKeyByIndex(System.Int32 index)
  RVA=0x0B5A21F8  token=0x60001FD  System.Single GetValue(System.String key)
  RVA=0x0B5A233C  token=0x60001FE  System.Boolean TryGetValue(System.String key, System.Single& value)
  RVA=0x0B59C568  token=0x60001FF  System.Single GetValueByIndex(System.Int32 index)
  RVA=0x0B5A2120  token=0x6000200  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.String,System.Single,Beyond.SparkBuffer.Runtime.Wrapper.S2FloatMap> GetEnumerator()
  RVA=0x0B5A223C  token=0x6000201  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,System.Single>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,System.Single>>.GetEnumerator()
  RVA=0x0B5A22BC  token=0x6000202  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.S2DoubleMap
TYPE:  sealed struct
TOKEN: 0x200003A
SIZE:  0x58
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_map  // 0x10
PROPERTIES:
  count  get=0x02BB2A50
  Count  get=0x02BB2A50
  Item  get=0x0B5A1F98
METHODS:
  RVA=0x09DC2B6C  token=0x6000206  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map)
  RVA=0x03290380  token=0x6000207  System.Boolean ContainsKey(System.String key)
  RVA=0x04240900  token=0x6000208  System.String GetKeyByIndex(System.Int32 index)
  RVA=0x0B5A1F98  token=0x6000209  System.Double GetValue(System.String key)
  RVA=0x0B5A20DC  token=0x600020A  System.Boolean TryGetValue(System.String key, System.Double& value)
  RVA=0x0B59701C  token=0x600020B  System.Double GetValueByIndex(System.Int32 index)
  RVA=0x0B5A1EC0  token=0x600020C  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.String,System.Double,Beyond.SparkBuffer.Runtime.Wrapper.S2DoubleMap> GetEnumerator()
  RVA=0x0B5A1FDC  token=0x600020D  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,System.Double>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,System.Double>>.GetEnumerator()
  RVA=0x0B5A205C  token=0x600020E  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.S2StringMap
TYPE:  sealed struct
TOKEN: 0x200003B
SIZE:  0x58
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_map  // 0x10
PROPERTIES:
  count  get=0x02BB2A50
  Count  get=0x02BB2A50
  Item  get=0x0B5A2890
METHODS:
  RVA=0x09DC2B6C  token=0x6000212  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map)
  RVA=0x03290380  token=0x6000213  System.Boolean ContainsKey(System.String key)
  RVA=0x04240900  token=0x6000214  System.String GetKeyByIndex(System.Int32 index)
  RVA=0x0B5A2890  token=0x6000215  System.String GetValue(System.String key)
  RVA=0x041EEFC0  token=0x6000216  System.Boolean TryGetValue(System.String key, System.String& value)
  RVA=0x0B59CC34  token=0x6000217  System.String GetValueByIndex(System.Int32 index)
  RVA=0x0B5A27B8  token=0x6000218  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.String,System.String,Beyond.SparkBuffer.Runtime.Wrapper.S2StringMap> GetEnumerator()
  RVA=0x0B5A28D4  token=0x6000219  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,System.String>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,System.String>>.GetEnumerator()
  RVA=0x0B5A2954  token=0x600021A  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.I2NullableBeanMap`1
TYPE:  sealed struct
TOKEN: 0x200003C
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_map  // 0x0
  private   readonly System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean>m_creator  // 0x0
PROPERTIES:
  count  get=-1  // not resolved
  Count  get=-1  // not resolved
  Item  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600021E  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map, System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean> creator)
  RVA=-1  // not resolved  token=0x600021F  System.Boolean ContainsKey(System.Int32 key)
  RVA=-1  // not resolved  token=0x6000220  System.Int32 GetKeyByIndex(System.Int32 index)
  RVA=-1  // not resolved  token=0x6000221  System.Nullable<TBean> GetValue(System.Int32 key)
  RVA=-1  // not resolved  token=0x6000222  System.Boolean TryGetValue(System.Int32 key, System.Nullable<TBean>& value)
  RVA=-1  // not resolved  token=0x6000223  System.Nullable<TBean> GetValueByIndex(System.Int32 index)
  RVA=-1  // not resolved  token=0x6000224  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int32,System.Nullable<TBean>,Beyond.SparkBuffer.Runtime.Wrapper.I2NullableBeanMap<TBean>> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000225  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int32,System.Nullable<TBean>>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int32,TBean?>>.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000226  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.I2BeanMap`1
TYPE:  sealed struct
TOKEN: 0x200003D
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_map  // 0x0
  private   readonly System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean>m_creator  // 0x0
PROPERTIES:
  count  get=-1  // not resolved
  Count  get=-1  // not resolved
  Item  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600022A  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map, System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean> creator)
  RVA=-1  // not resolved  token=0x600022B  System.Boolean ContainsKey(System.Int32 key)
  RVA=-1  // not resolved  token=0x600022C  System.Int32 GetKeyByIndex(System.Int32 index)
  RVA=-1  // not resolved  token=0x600022D  TBean GetValue(System.Int32 key)
  RVA=-1  // not resolved  token=0x600022E  System.Boolean TryGetValue(System.Int32 key, TBean& value)
  RVA=-1  // not resolved  token=0x600022F  TBean GetValueByIndex(System.Int32 index)
  RVA=-1  // not resolved  token=0x6000230  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int32,TBean,Beyond.SparkBuffer.Runtime.Wrapper.I2BeanMap<TBean>> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000231  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int32,TBean>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int32,TBean>>.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000232  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.I2EnumMap`1
TYPE:  sealed struct
TOKEN: 0x200003E
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_map  // 0x0
  private           System.Func<System.Int32,TEnum> m_converter  // 0x0
PROPERTIES:
  count  get=-1  // not resolved
  Count  get=-1  // not resolved
  Item  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000236  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map, System.Func<System.Int32,TEnum> converter)
  RVA=-1  // not resolved  token=0x6000237  System.Boolean ContainsKey(System.Int32 key)
  RVA=-1  // not resolved  token=0x6000238  System.Int32 GetKeyByIndex(System.Int32 index)
  RVA=-1  // not resolved  token=0x6000239  TEnum GetValue(System.Int32 key)
  RVA=-1  // not resolved  token=0x600023A  System.Boolean TryGetValue(System.Int32 key, TEnum& value)
  RVA=-1  // not resolved  token=0x600023B  TEnum GetValueByIndex(System.Int32 index)
  RVA=-1  // not resolved  token=0x600023C  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int32,TEnum,Beyond.SparkBuffer.Runtime.Wrapper.I2EnumMap<TEnum>> GetEnumerator()
  RVA=-1  // not resolved  token=0x600023D  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int32,TEnum>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int32,TEnum>>.GetEnumerator()
  RVA=-1  // not resolved  token=0x600023E  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.I2BoolMap
TYPE:  sealed struct
TOKEN: 0x200003F
SIZE:  0x58
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_map  // 0x10
PROPERTIES:
  count  get=0x02BB2A50
  Count  get=0x02BB2A50
  Item  get=0x0B596B28
METHODS:
  RVA=0x09DC2B6C  token=0x6000242  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map)
  RVA=0x0856A078  token=0x6000243  System.Boolean ContainsKey(System.Int32 key)
  RVA=0x036A33E0  token=0x6000244  System.Int32 GetKeyByIndex(System.Int32 index)
  RVA=0x0B596B28  token=0x6000245  System.Boolean GetValue(System.Int32 key)
  RVA=0x0B596C6C  token=0x6000246  System.Boolean TryGetValue(System.Int32 key, System.Boolean& value)
  RVA=0x0B596AE4  token=0x6000247  System.Boolean GetValueByIndex(System.Int32 index)
  RVA=0x0B596A14  token=0x6000248  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int32,System.Boolean,Beyond.SparkBuffer.Runtime.Wrapper.I2BoolMap> GetEnumerator()
  RVA=0x0B596B6C  token=0x6000249  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int32,System.Boolean>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int32,System.Boolean>>.GetEnumerator()
  RVA=0x0B596BEC  token=0x600024A  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.I2ByteMap
TYPE:  sealed struct
TOKEN: 0x2000040
SIZE:  0x58
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_map  // 0x10
PROPERTIES:
  count  get=0x02BB2A50
  Count  get=0x02BB2A50
  Item  get=0x0B596DC4
METHODS:
  RVA=0x09DC2B6C  token=0x600024E  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map)
  RVA=0x0856A078  token=0x600024F  System.Boolean ContainsKey(System.Int32 key)
  RVA=0x036A33E0  token=0x6000250  System.Int32 GetKeyByIndex(System.Int32 index)
  RVA=0x0B596DC4  token=0x6000251  System.Byte GetValue(System.Int32 key)
  RVA=0x0B596F08  token=0x6000252  System.Boolean TryGetValue(System.Int32 key, System.Byte& value)
  RVA=0x0B596D80  token=0x6000253  System.Byte GetValueByIndex(System.Int32 index)
  RVA=0x0B596CB0  token=0x6000254  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int32,System.Byte,Beyond.SparkBuffer.Runtime.Wrapper.I2ByteMap> GetEnumerator()
  RVA=0x0B596E08  token=0x6000255  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int32,System.Byte>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int32,System.Byte>>.GetEnumerator()
  RVA=0x0B596E88  token=0x6000256  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.I2IntMap
TYPE:  sealed struct
TOKEN: 0x2000041
SIZE:  0x58
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_map  // 0x10
PROPERTIES:
  count  get=0x02BB2A50
  Count  get=0x02BB2A50
  Item  get=0x0B597404
METHODS:
  RVA=0x09DC2B6C  token=0x600025A  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map)
  RVA=0x0856A078  token=0x600025B  System.Boolean ContainsKey(System.Int32 key)
  RVA=0x036A33E0  token=0x600025C  System.Int32 GetKeyByIndex(System.Int32 index)
  RVA=0x0B597404  token=0x600025D  System.Int32 GetValue(System.Int32 key)
  RVA=0x0B597548  token=0x600025E  System.Boolean TryGetValue(System.Int32 key, System.Int32& value)
  RVA=0x0B5973C0  token=0x600025F  System.Int32 GetValueByIndex(System.Int32 index)
  RVA=0x0B5972F0  token=0x6000260  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int32,System.Int32,Beyond.SparkBuffer.Runtime.Wrapper.I2IntMap> GetEnumerator()
  RVA=0x0B597448  token=0x6000261  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int32,System.Int32>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int32,System.Int32>>.GetEnumerator()
  RVA=0x0B5974C8  token=0x6000262  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.I2LongMap
TYPE:  sealed struct
TOKEN: 0x2000042
SIZE:  0x58
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_map  // 0x10
PROPERTIES:
  count  get=0x02BB2A50
  Count  get=0x02BB2A50
  Item  get=0x0B5976A0
METHODS:
  RVA=0x09DC2B6C  token=0x6000266  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map)
  RVA=0x0856A078  token=0x6000267  System.Boolean ContainsKey(System.Int32 key)
  RVA=0x036A33E0  token=0x6000268  System.Int32 GetKeyByIndex(System.Int32 index)
  RVA=0x0B5976A0  token=0x6000269  System.Int64 GetValue(System.Int32 key)
  RVA=0x0B5977E4  token=0x600026A  System.Boolean TryGetValue(System.Int32 key, System.Int64& value)
  RVA=0x0B59765C  token=0x600026B  System.Int64 GetValueByIndex(System.Int32 index)
  RVA=0x0B59758C  token=0x600026C  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int32,System.Int64,Beyond.SparkBuffer.Runtime.Wrapper.I2LongMap> GetEnumerator()
  RVA=0x0B5976E4  token=0x600026D  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int32,System.Int64>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int32,System.Int64>>.GetEnumerator()
  RVA=0x0B597764  token=0x600026E  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.I2FloatMap
TYPE:  sealed struct
TOKEN: 0x2000043
SIZE:  0x58
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_map  // 0x10
PROPERTIES:
  count  get=0x02BB2A50
  Count  get=0x02BB2A50
  Item  get=0x0B5971E8
METHODS:
  RVA=0x09DC2B6C  token=0x6000272  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map)
  RVA=0x0856A078  token=0x6000273  System.Boolean ContainsKey(System.Int32 key)
  RVA=0x036A33E0  token=0x6000274  System.Int32 GetKeyByIndex(System.Int32 index)
  RVA=0x0B5971E8  token=0x6000275  System.Single GetValue(System.Int32 key)
  RVA=0x0B5972AC  token=0x6000276  System.Boolean TryGetValue(System.Int32 key, System.Single& value)
  RVA=0x03E31CF0  token=0x6000277  System.Single GetValueByIndex(System.Int32 index)
  RVA=0x04B880E0  token=0x6000278  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int32,System.Single,Beyond.SparkBuffer.Runtime.Wrapper.I2FloatMap> GetEnumerator()
  RVA=0x04B88060  token=0x6000279  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int32,System.Single>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int32,System.Single>>.GetEnumerator()
  RVA=0x0B59722C  token=0x600027A  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.I2DoubleMap
TYPE:  sealed struct
TOKEN: 0x2000044
SIZE:  0x58
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_map  // 0x10
PROPERTIES:
  count  get=0x02BB2A50
  Count  get=0x02BB2A50
  Item  get=0x0B597060
METHODS:
  RVA=0x09DC2B6C  token=0x600027E  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map)
  RVA=0x0856A078  token=0x600027F  System.Boolean ContainsKey(System.Int32 key)
  RVA=0x036A33E0  token=0x6000280  System.Int32 GetKeyByIndex(System.Int32 index)
  RVA=0x0B597060  token=0x6000281  System.Double GetValue(System.Int32 key)
  RVA=0x0B5971A4  token=0x6000282  System.Boolean TryGetValue(System.Int32 key, System.Double& value)
  RVA=0x0B59701C  token=0x6000283  System.Double GetValueByIndex(System.Int32 index)
  RVA=0x0B596F4C  token=0x6000284  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int32,System.Double,Beyond.SparkBuffer.Runtime.Wrapper.I2DoubleMap> GetEnumerator()
  RVA=0x0B5970A4  token=0x6000285  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int32,System.Double>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int32,System.Double>>.GetEnumerator()
  RVA=0x0B597124  token=0x6000286  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.I2StringMap
TYPE:  sealed struct
TOKEN: 0x2000045
SIZE:  0x58
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_map  // 0x10
PROPERTIES:
  count  get=0x02BB2A50
  Count  get=0x02BB2A50
  Item  get=0x03062660
METHODS:
  RVA=0x09DC2B6C  token=0x600028A  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map)
  RVA=0x0331BB00  token=0x600028B  System.Boolean ContainsKey(System.Int32 key)
  RVA=0x036A33E0  token=0x600028C  System.Int32 GetKeyByIndex(System.Int32 index)
  RVA=0x04060770  token=0x600028D  System.String GetValue(System.Int32 key)
  RVA=0x0397BA10  token=0x600028E  System.Boolean TryGetValue(System.Int32 key, System.String& value)
  RVA=0x036A3340  token=0x600028F  System.String GetValueByIndex(System.Int32 index)
  RVA=0x047F9D50  token=0x6000290  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int32,System.String,Beyond.SparkBuffer.Runtime.Wrapper.I2StringMap> GetEnumerator()
  RVA=0x0B597828  token=0x6000291  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int32,System.String>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int32,System.String>>.GetEnumerator()
  RVA=0x0B5978A8  token=0x6000292  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.L2BoolMap
TYPE:  sealed struct
TOKEN: 0x2000046
SIZE:  0x58
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_map  // 0x10
PROPERTIES:
  count  get=0x02BB2A50
  Count  get=0x02BB2A50
  Item  get=0x0B59BC08
METHODS:
  RVA=0x09DC2B6C  token=0x6000296  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map)
  RVA=0x086D7B08  token=0x6000297  System.Boolean ContainsKey(System.Int64 key)
  RVA=0x086D7B4C  token=0x6000298  System.Int64 GetKeyByIndex(System.Int32 index)
  RVA=0x0B59BC08  token=0x6000299  System.Boolean GetValue(System.Int64 key)
  RVA=0x0B59BD4C  token=0x600029A  System.Boolean TryGetValue(System.Int64 key, System.Boolean& value)
  RVA=0x0B596AE4  token=0x600029B  System.Boolean GetValueByIndex(System.Int32 index)
  RVA=0x0B59BB38  token=0x600029C  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int64,System.Boolean,Beyond.SparkBuffer.Runtime.Wrapper.L2BoolMap> GetEnumerator()
  RVA=0x0B59BC4C  token=0x600029D  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int64,System.Boolean>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int64,System.Boolean>>.GetEnumerator()
  RVA=0x0B59BCCC  token=0x600029E  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.L2ByteMap
TYPE:  sealed struct
TOKEN: 0x2000047
SIZE:  0x58
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_map  // 0x10
PROPERTIES:
  count  get=0x02BB2A50
  Count  get=0x02BB2A50
  Item  get=0x0B59BE60
METHODS:
  RVA=0x09DC2B6C  token=0x60002A2  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map)
  RVA=0x086D7B08  token=0x60002A3  System.Boolean ContainsKey(System.Int64 key)
  RVA=0x086D7B4C  token=0x60002A4  System.Int64 GetKeyByIndex(System.Int32 index)
  RVA=0x0B59BE60  token=0x60002A5  System.Byte GetValue(System.Int64 key)
  RVA=0x0B59BFA4  token=0x60002A6  System.Boolean TryGetValue(System.Int64 key, System.Byte& value)
  RVA=0x0B596D80  token=0x60002A7  System.Byte GetValueByIndex(System.Int32 index)
  RVA=0x0B59BD90  token=0x60002A8  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int64,System.Byte,Beyond.SparkBuffer.Runtime.Wrapper.L2ByteMap> GetEnumerator()
  RVA=0x0B59BEA4  token=0x60002A9  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int64,System.Byte>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int64,System.Byte>>.GetEnumerator()
  RVA=0x0B59BF24  token=0x60002AA  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.L2IntMap
TYPE:  sealed struct
TOKEN: 0x2000048
SIZE:  0x58
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_map  // 0x10
PROPERTIES:
  count  get=0x02BB2A50
  Count  get=0x02BB2A50
  Item  get=0x0B59C310
METHODS:
  RVA=0x09DC2B6C  token=0x60002AE  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map)
  RVA=0x086D7B08  token=0x60002AF  System.Boolean ContainsKey(System.Int64 key)
  RVA=0x086D7B4C  token=0x60002B0  System.Int64 GetKeyByIndex(System.Int32 index)
  RVA=0x0B59C310  token=0x60002B1  System.Int32 GetValue(System.Int64 key)
  RVA=0x0B59C454  token=0x60002B2  System.Boolean TryGetValue(System.Int64 key, System.Int32& value)
  RVA=0x0B5973C0  token=0x60002B3  System.Int32 GetValueByIndex(System.Int32 index)
  RVA=0x0B59C734  token=0x60002B4  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int64,System.Int32,Beyond.SparkBuffer.Runtime.Wrapper.L2IntMap> GetEnumerator()
  RVA=0x0B59C804  token=0x60002B5  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int64,System.Int32>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int64,System.Int32>>.GetEnumerator()
  RVA=0x0B59C884  token=0x60002B6  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.L2LongMap
TYPE:  sealed struct
TOKEN: 0x2000049
SIZE:  0x58
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_map  // 0x10
PROPERTIES:
  count  get=0x02BB2A50
  Count  get=0x02BB2A50
  Item  get=0x0B59C9D4
METHODS:
  RVA=0x09DC2B6C  token=0x60002BA  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map)
  RVA=0x086D7B08  token=0x60002BB  System.Boolean ContainsKey(System.Int64 key)
  RVA=0x086D7B4C  token=0x60002BC  System.Int64 GetKeyByIndex(System.Int32 index)
  RVA=0x0B59C9D4  token=0x60002BD  System.Int64 GetValue(System.Int64 key)
  RVA=0x0B59CB18  token=0x60002BE  System.Boolean TryGetValue(System.Int64 key, System.Int64& value)
  RVA=0x0B59765C  token=0x60002BF  System.Int64 GetValueByIndex(System.Int32 index)
  RVA=0x0B59C904  token=0x60002C0  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int64,System.Int64,Beyond.SparkBuffer.Runtime.Wrapper.L2LongMap> GetEnumerator()
  RVA=0x0B59CA18  token=0x60002C1  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int64,System.Int64>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int64,System.Int64>>.GetEnumerator()
  RVA=0x0B59CA98  token=0x60002C2  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.L2FloatMap
TYPE:  sealed struct
TOKEN: 0x200004A
SIZE:  0x58
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_map  // 0x10
PROPERTIES:
  count  get=0x02BB2A50
  Count  get=0x02BB2A50
  Item  get=0x0B59C5AC
METHODS:
  RVA=0x09DC2B6C  token=0x60002C6  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map)
  RVA=0x086D7B08  token=0x60002C7  System.Boolean ContainsKey(System.Int64 key)
  RVA=0x086D7B4C  token=0x60002C8  System.Int64 GetKeyByIndex(System.Int32 index)
  RVA=0x0B59C5AC  token=0x60002C9  System.Single GetValue(System.Int64 key)
  RVA=0x0B59C6F0  token=0x60002CA  System.Boolean TryGetValue(System.Int64 key, System.Single& value)
  RVA=0x0B59C568  token=0x60002CB  System.Single GetValueByIndex(System.Int32 index)
  RVA=0x0B59C498  token=0x60002CC  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int64,System.Single,Beyond.SparkBuffer.Runtime.Wrapper.L2FloatMap> GetEnumerator()
  RVA=0x0B59C5F0  token=0x60002CD  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int64,System.Single>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int64,System.Single>>.GetEnumerator()
  RVA=0x0B59C670  token=0x60002CE  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.L2DoubleMap
TYPE:  sealed struct
TOKEN: 0x200004B
SIZE:  0x58
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_map  // 0x10
PROPERTIES:
  count  get=0x02BB2A50
  Count  get=0x02BB2A50
  Item  get=0x0B59C0B8
METHODS:
  RVA=0x09DC2B6C  token=0x60002D2  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map)
  RVA=0x086D7B08  token=0x60002D3  System.Boolean ContainsKey(System.Int64 key)
  RVA=0x086D7B4C  token=0x60002D4  System.Int64 GetKeyByIndex(System.Int32 index)
  RVA=0x0B59C0B8  token=0x60002D5  System.Double GetValue(System.Int64 key)
  RVA=0x0B59C1FC  token=0x60002D6  System.Boolean TryGetValue(System.Int64 key, System.Double& value)
  RVA=0x0B59701C  token=0x60002D7  System.Double GetValueByIndex(System.Int32 index)
  RVA=0x0B59BFE8  token=0x60002D8  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int64,System.Double,Beyond.SparkBuffer.Runtime.Wrapper.L2DoubleMap> GetEnumerator()
  RVA=0x0B59C0FC  token=0x60002D9  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int64,System.Double>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int64,System.Double>>.GetEnumerator()
  RVA=0x0B59C17C  token=0x60002DA  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.L2StringMap
TYPE:  sealed struct
TOKEN: 0x200004C
SIZE:  0x58
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_map  // 0x10
PROPERTIES:
  count  get=0x02BB2A50
  Count  get=0x02BB2A50
  Item  get=0x0B59CC78
METHODS:
  RVA=0x09DC2B6C  token=0x60002DE  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map)
  RVA=0x086D7B08  token=0x60002DF  System.Boolean ContainsKey(System.Int64 key)
  RVA=0x086D7B4C  token=0x60002E0  System.Int64 GetKeyByIndex(System.Int32 index)
  RVA=0x0B59CC78  token=0x60002E1  System.String GetValue(System.Int64 key)
  RVA=0x039CE6C0  token=0x60002E2  System.Boolean TryGetValue(System.Int64 key, System.String& value)
  RVA=0x0B59CC34  token=0x60002E3  System.String GetValueByIndex(System.Int32 index)
  RVA=0x0B59CB5C  token=0x60002E4  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int64,System.String,Beyond.SparkBuffer.Runtime.Wrapper.L2StringMap> GetEnumerator()
  RVA=0x0B59CCBC  token=0x60002E5  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int64,System.String>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int64,System.String>>.GetEnumerator()
  RVA=0x0B59CD3C  token=0x60002E6  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.L2EnumMap
TYPE:  sealed struct
TOKEN: 0x200004D
SIZE:  0x58
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_map  // 0x10
PROPERTIES:
  count  get=0x02BB2A50
  Count  get=0x02BB2A50
  Item  get=0x0B59C310
METHODS:
  RVA=0x09DC2B6C  token=0x60002EA  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map)
  RVA=0x086D7B08  token=0x60002EB  System.Boolean ContainsKey(System.Int64 key)
  RVA=0x086D7B4C  token=0x60002EC  System.Int64 GetKeyByIndex(System.Int32 index)
  RVA=0x0B59C310  token=0x60002ED  System.Int32 GetValue(System.Int64 key)
  RVA=0x0B59C454  token=0x60002EE  System.Boolean TryGetValue(System.Int64 key, System.Int32& value)
  RVA=0x0B5973C0  token=0x60002EF  System.Int32 GetValueByIndex(System.Int32 index)
  RVA=0x0B59C240  token=0x60002F0  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int64,System.Int32,Beyond.SparkBuffer.Runtime.Wrapper.L2EnumMap> GetEnumerator()
  RVA=0x0B59C354  token=0x60002F1  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int64,System.Int32>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int64,System.Int32>>.GetEnumerator()
  RVA=0x0B59C3D4  token=0x60002F2  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.L2NullableBeanMap`1
TYPE:  sealed struct
TOKEN: 0x200004E
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_map  // 0x0
  private   readonly System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean>m_creator  // 0x0
PROPERTIES:
  count  get=-1  // not resolved
  Count  get=-1  // not resolved
  Item  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60002F6  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map, System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean> creator)
  RVA=-1  // not resolved  token=0x60002F7  System.Boolean ContainsKey(System.Int64 key)
  RVA=-1  // not resolved  token=0x60002F8  System.Int64 GetKeyByIndex(System.Int32 index)
  RVA=-1  // not resolved  token=0x60002F9  System.Nullable<TBean> GetValue(System.Int64 key)
  RVA=-1  // not resolved  token=0x60002FA  System.Boolean TryGetValue(System.Int64 key, System.Nullable<TBean>& value)
  RVA=-1  // not resolved  token=0x60002FB  System.Nullable<TBean> GetValueByIndex(System.Int32 index)
  RVA=-1  // not resolved  token=0x60002FC  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int64,System.Nullable<TBean>,Beyond.SparkBuffer.Runtime.Wrapper.L2NullableBeanMap<TBean>> GetEnumerator()
  RVA=-1  // not resolved  token=0x60002FD  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int64,System.Nullable<TBean>>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int64,TBean?>>.GetEnumerator()
  RVA=-1  // not resolved  token=0x60002FE  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Runtime.Wrapper.L2BeanMap`1
TYPE:  sealed struct
TOKEN: 0x200004F
IMPLEMENTS: Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   readonly Beyond.SparkBuffer.Runtime.Map  m_map  // 0x0
  private   readonly System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean>m_creator  // 0x0
PROPERTIES:
  count  get=-1  // not resolved
  Count  get=-1  // not resolved
  Item  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000302  System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map, System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean> creator)
  RVA=-1  // not resolved  token=0x6000303  System.Boolean ContainsKey(System.Int64 key)
  RVA=-1  // not resolved  token=0x6000304  System.Int64 GetKeyByIndex(System.Int32 index)
  RVA=-1  // not resolved  token=0x6000305  TBean GetValue(System.Int64 key)
  RVA=-1  // not resolved  token=0x6000306  System.Boolean TryGetValue(System.Int64 key, TBean& value)
  RVA=-1  // not resolved  token=0x6000307  TBean GetValueByIndex(System.Int32 index)
  RVA=-1  // not resolved  token=0x6000308  Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int64,TBean,Beyond.SparkBuffer.Runtime.Wrapper.L2BeanMap<TBean>> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000309  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int64,TBean>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int64,TBean>>.GetEnumerator()
  RVA=-1  // not resolved  token=0x600030A  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Beyond.SparkBuffer.Serialize.Extensions
TYPE:  static class
TOKEN: 0x2000050
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x600030B  System.Boolean Contains(System.Collections.Generic.IList<T> list, T element)
  RVA=-1  // generic def  token=0x600030C  System.Boolean IsNullOrEmpty(System.Collections.Generic.IList<T> list)
  RVA=-1  // generic def  token=0x600030D  System.Boolean IsNullOrEmpty(System.Collections.Generic.ICollection<T> list)
  RVA=0x0B5968FC  token=0x600030E  System.Boolean IsNullOrEmpty(System.Collections.Specialized.OrderedDictionary dict)
  RVA=-1  // generic def  token=0x600030F  T[] ToArrayE(System.Collections.Generic.IEnumerable<T> enumerable)
  RVA=-1  // generic def  token=0x6000310  T GetValue(System.Collections.DictionaryEntry entry)
END_CLASS

CLASS: Beyond.SparkBuffer.Serialize.JsonSerializer
TYPE:  class
TOKEN: 0x2000051
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000311  System.String Serialize(T obj, System.Boolean indented)
  RVA=0x0B59B334  token=0x6000312  System.String Serialize(System.Object obj, System.Boolean indented)
  RVA=0x0B59B364  token=0x6000313  System.String _SerializeDirect(Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject reflectObj, System.Boolean indented)
  RVA=0x0B59B558  token=0x6000314  System.Void _WriteReflectObj(Newtonsoft.Json.JsonWriter writer, Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject reflectObj)
  RVA=0x041E1670  token=0x6000315  System.Void .ctor()
END_CLASS

CLASS: Beyond.SparkBuffer.Serialize.ObjectReflector
TYPE:  class
TOKEN: 0x2000052
SIZE:  0x20
FIELDS:
  private           System.Collections.Generic.List<System.Object>m_tempList  // 0x10
  private           System.Collections.Generic.Dictionary<System.Type,Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType>m_typeMap  // 0x18
METHODS:
  RVA=0x0B5A0D08  token=0x6000316  System.Void .ctor()
  RVA=0x0B59E684  token=0x6000317  Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectResult Parse(System.Object obj)
  RVA=0x0B59E9FC  token=0x6000318  Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectResult _ParseInternal(System.Object obj)
  RVA=0x0B59ED08  token=0x6000319  Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType _ParseTypeMap(System.Type rootType)
  RVA=0x0B59FD60  token=0x600031A  System.Boolean _TryParseType(System.Type type, Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType& reflectType)
  RVA=0x0B59FBD0  token=0x600031B  Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject _ReflectObject(System.Object obj, Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType reflectType)
  RVA=0x0B59F340  token=0x600031C  Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject _ReflectBean(System.Object obj, Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType reflectType)
  RVA=0x0B59EDE0  token=0x600031D  Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject _ReflectArray(System.Object obj, Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType reflectType)
  RVA=0x0B59F87C  token=0x600031E  Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject _ReflectMap(System.Object obj, Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType reflectType)
  RVA=0x0B59E8D8  token=0x600031F  System.Collections.Generic.IEnumerable<System.Reflection.FieldInfo> _GetFields(System.Type type)
  RVA=0x0B59E708  token=0x6000320  System.Object[] _ConvertICollectionToArray(System.Collections.ICollection collection)
END_CLASS

CLASS: Beyond.SparkBuffer.Serialize.SparkSerializer
TYPE:  class
TOKEN: 0x2000058
SIZE:  0x50
IMPLEMENTS: System.IDisposable
FIELDS:
  private           Beyond.SparkBuffer.Serialize.SparkSerializer.Optionsm_options  // 0x10
  private           System.IO.MemoryStream          m_ms  // 0x20
  private           Beyond.SparkBuffer.Runtime.ByteWriterm_writer  // 0x28
  private           System.Collections.Generic.List<Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType>m_enumTypes  // 0x30
  private           System.Collections.Generic.Dictionary<System.Type,Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType>m_beanMap  // 0x38
  private           System.Collections.Generic.Dictionary<System.Type,System.Int32>m_typeHashMap  // 0x40
  private           System.Collections.Generic.Dictionary<System.String,System.Int32>m_stringMap  // 0x48
METHODS:
  RVA=-1  // generic def  token=0x6000329  System.Byte[] Serialize(T obj)
  RVA=-1  // generic def  token=0x600032A  System.Byte[] Serialize(T obj, Beyond.SparkBuffer.Serialize.SparkSerializer.Options options)
  RVA=0x0B5A3138  token=0x600032B  System.Byte[] Serialize(System.Object obj)
  RVA=0x0B5A3198  token=0x600032C  System.Byte[] Serialize(System.Object obj, Beyond.SparkBuffer.Serialize.SparkSerializer.Options options)
  RVA=0x0B5A6760  token=0x600032D  System.Void .ctor(Beyond.SparkBuffer.Serialize.SparkSerializer.Options options)
  RVA=0x0B5A30EC  token=0x600032E  System.Void Dispose()
  RVA=0x0B5A6514  token=0x600032F  System.Byte[] _SerializeInternal(System.Object obj)
  RVA=0x0B5A6308  token=0x6000330  System.Void _PreprocessTypes(Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType[] reflectTypes)
  RVA=0x0B5A5DA0  token=0x6000331  System.Void _ExportTypeDefs()
  RVA=0x0B5A4C34  token=0x6000332  System.Void _ExportEnumType(Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType enumType)
  RVA=0x0B5A442C  token=0x6000333  System.Void _ExportBeanType(Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType beanType)
  RVA=0x0B5A629C  token=0x6000334  System.String _ParseTypeName(System.Type reflectType)
  RVA=0x0B5A3338  token=0x6000335  System.Collections.Generic.HashSet<System.String> _CollectAllStrings(Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject rootObj)
  RVA=0x0B5A6028  token=0x6000336  System.Collections.Generic.List<System.Collections.Generic.List<System.String>> _GenStringHashList(System.Collections.Generic.HashSet<System.String> strings)
  RVA=0x0B5A5B08  token=0x6000337  System.Void _ExportStrings(Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject rootObj)
  RVA=0x0B5A485C  token=0x6000338  System.Void _ExportElementData(Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject elementObj, System.Collections.Generic.List<Beyond.SparkBuffer.Serialize.SparkSerializer.ReservedField> reservedFields)
  RVA=0x0B5A4050  token=0x6000339  System.Int32 _ExportBeanData(Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject beanObj)
  RVA=0x0B5A3DFC  token=0x600033A  System.Int32 _ExportArrayData(Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject arrayObj)
  RVA=0x0B5A5088  token=0x600033B  System.Int32 _ExportMapData(Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject mapObj)
  RVA=0x0B5A39F0  token=0x600033C  System.Collections.Generic.Dictionary<System.Int32,System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject,Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject>>> _CreateMapHashEntry(System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject,Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject>> mapElements, Beyond.SparkBuffer.SparkType keyType)
  RVA=0x0B5A5928  token=0x600033D  System.Void _ExportRootDef(Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType rootType)
  RVA=0x0B5A4768  token=0x600033E  System.Int32 _ExportDataFromRoot(Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject rootObj)
  RVA=0x0B5A56BC  token=0x600033F  System.Void _ExportReservedFields(System.Collections.Generic.List<Beyond.SparkBuffer.Serialize.SparkSerializer.ReservedField> reservedFields)
  RVA=0x02F05D70  token=0x6000340  System.Int32 GetStableStringHash(System.String str)
  RVA=0x0B5A310C  token=0x6000341  System.Int32 GetStableTypeHash(System.Type type)
  RVA=0x0B5A6684  token=0x6000342  System.Void _WriteConcreteType(System.Type type)
  RVA=0x0B5A32AC  token=0x6000343  System.Int32 <_ExportTypeDefs>b__17_0(Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType lhs, Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType rhs)
END_CLASS

CLASS: IFix.ILFixDynamicMethodWrapper
TYPE:  class
TOKEN: 0x2000062
SIZE:  0x28
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
  private           System.Int32                    methodId  // 0x18
  private           System.Object                   anonObj  // 0x20
  public    static  IFix.ILFixDynamicMethodWrapper[]wrapperArray  // static @ 0x0
METHODS:
  RVA=0x032B2C30  token=0x600037D  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj)
  RVA=0x067CDB90  token=0x600037E  System.IntPtr __Gen_Wrap_0(System.Object P0)
  RVA=0x0B5A7468  token=0x600037F  System.ReadOnlySpan<System.Byte> __Gen_Wrap_1(System.IntPtr P0, System.Int32 P1)
  RVA=0x063606FC  token=0x6000380  System.IntPtr __Gen_Wrap_2(System.Int32 P0)
  RVA=0x0B5A757C  token=0x6000381  System.Void __Gen_Wrap_3(System.Int32 P0, System.IntPtr P1)
  RVA=0x063761FC  token=0x6000382  System.Void __Gen_Wrap_4()
  RVA=0x0B5A7644  token=0x6000383  System.String __Gen_Wrap_5()
  RVA=0x09CC7624  token=0x6000384  System.Void __Gen_Wrap_6(System.Object P0, System.IntPtr P1)
  RVA=0x0B5A76FC  token=0x6000385  Beyond.Reflection.StructPtrWrapper __Gen_Wrap_7(System.IntPtr P0, System.Int32 P1, System.Object P2)
  RVA=0x0B5A7810  token=0x6000386  Beyond.Reflection.StructPtrWrapper __Gen_Wrap_8(System.Object P0)
  RVA=0x066C296C  token=0x6000387  System.Void __Gen_Wrap_9(System.Object P0)
  RVA=0x0B5A7008  token=0x6000388  System.Object __Gen_Wrap_10(System.Object P0)
  RVA=0x0B5A70E4  token=0x6000389  System.Object __Gen_Wrap_11(System.IntPtr P0, System.Object P1, System.Int32 P2)
  RVA=0x09C8B45C  token=0x600038A  System.Void __Gen_Wrap_12(System.Object P0, System.Object P1)
  RVA=0x06366E40  token=0x600038B  System.Int32 __Gen_Wrap_13(System.Object P0)
  RVA=0x0A8A1EF4  token=0x600038C  System.Boolean __Gen_Wrap_14(System.Object P0, System.Object P1)
  RVA=0x0B5A71F8  token=0x600038D  System.String __Gen_Wrap_15(System.Object P0)
  RVA=0x0B5A72D4  token=0x600038E  System.Type __Gen_Wrap_16(System.Object P0)
  RVA=0x0B5A73B0  token=0x600038F  Beyond.Reflection.StructPtrWrapper __Gen_Wrap_17()
  RVA=0x04D372C0  token=0x6000390  System.Void .cctor()
END_CLASS

CLASS: IFix.ILFixInterfaceBridge
TYPE:  class
TOKEN: 0x2000063
SIZE:  0x40
EXTENDS: IFix.Core.AnonymousStorey
FIELDS:
METHODS:
  RVA=0x0B5A78EC  token=0x6000391  System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine)
  RVA=0x041E1670  token=0x6000392  System.Void RefAsyncBuilderStartMethod()
END_CLASS

CLASS: IFix.WrappersManagerImpl
TYPE:  class
TOKEN: 0x2000064
SIZE:  0x18
IMPLEMENTS: IFix.Core.WrappersManager
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000393  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine)
  RVA=0x0B5A9DD0  token=0x6000394  IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id)
  RVA=0x0328E430  token=0x6000395  System.Boolean IsPatched(System.Int32 id)
  RVA=0x0B5A9C8C  token=0x6000396  System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon)
  RVA=0x0B5A9D58  token=0x6000397  System.Object CreateWrapper(System.Int32 id)
  RVA=0x0B5A9E34  token=0x6000398  System.Object InitWrapperArray(System.Int32 len)
  RVA=0x0B5A9BE8  token=0x6000399  IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine)
END_CLASS

CLASS: IFix.IDMAP0
TYPE:  sealed struct
TOKEN: 0x2000065
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  IFix.IDMAP0                     Beyond-Reflection-MethodPointerManager-GetDelegatePointer0  // const
  public    static  IFix.IDMAP0                     Beyond-Reflection-ObjectPointerHelper-GetReadOnlySpan0  // const
  public    static  IFix.IDMAP0                     Beyond-Reflection-StructMemManager-GetMemoryBySize0  // const
  public    static  IFix.IDMAP0                     Beyond-Reflection-StructMemManager-FreeMemory0  // const
  public    static  IFix.IDMAP0                     Beyond-Reflection-StructMemManager-ClearMemory0  // const
  public    static  IFix.IDMAP0                     Beyond-Reflection-StructMemManager-DumpMemory0  // const
  public    static  IFix.IDMAP0                     Beyond-Reflection-StructPtrRefrenceManager-_RunOnStart0  // const
  public    static  IFix.IDMAP0                     Beyond-Reflection-StructPtrRefrenceManager-AddObjInPtrRef0  // const
  public    static  IFix.IDMAP0                     Beyond-Reflection-StructPtrRefrenceManager-RemoveObjInPtrRef0  // const
  public    static  IFix.IDMAP0                     Beyond-Reflection-StructPtrWrapper-GetStructPtrWrapper0  // const
  public    static  IFix.IDMAP0                     Beyond-Reflection-StructPtrWrapper-GetStructPtrWrapper1  // const
  public    static  IFix.IDMAP0                     Beyond-Reflection-StructPtrWrapper-Release0  // const
  public    static  IFix.IDMAP0                     Beyond-Reflection-StructPtrWrapper-GetStructObj0  // const
  public    static  IFix.IDMAP0                     Beyond-Reflection-StructPtrWrapper-GetStructObj1  // const
  public    static  IFix.IDMAP0                     Beyond-Reflection-StructPtrWrapper-SetStructObjValue0  // const
  public    static  IFix.IDMAP0                     Beyond-Reflection-StructPtrWrapper-GetHashCode0  // const
  public    static  IFix.IDMAP0                     Beyond-Reflection-StructPtrWrapper-Equals0  // const
  public    static  IFix.IDMAP0                     Beyond-Reflection-StructPtrWrapper-ToString0  // const
  public    static  IFix.IDMAP0                     Beyond-Reflection-StructPtrWrapper-GetType0  // const
  public    static  IFix.IDMAP0                     Beyond-Reflection-StructPtrWrapper-GetStructObj2  // const
  public    static  IFix.IDMAP0                     Beyond-Reflection-StructPtrWrapper-_OnCreate0  // const
  public    static  IFix.IDMAP0                     Beyond-Reflection-StructPtrWrapper-_OnGet0  // const
  public    static  IFix.IDMAP0                     Beyond-Reflection-StructPtrWrapper-_OnRealse0  // const
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

CLASS: System.Runtime.CompilerServices.IsUnmanagedAttribute
TYPE:  sealed class
TOKEN: 0x2000003
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000002  System.Void .ctor()
END_CLASS

