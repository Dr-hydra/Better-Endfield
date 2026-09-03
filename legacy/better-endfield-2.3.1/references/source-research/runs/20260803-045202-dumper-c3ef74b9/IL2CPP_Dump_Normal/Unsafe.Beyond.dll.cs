// ========================================================
// Dumped by @desirepro
// Assembly: Unsafe.Beyond.dll
// Classes:  101
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x200000C  // size: 0x28
    public sealed struct UnionKey
    {
        // Fields
        public System.Int32 intKey;  // 0x10
        public System.Int64 longKey;  // 0x18
        public System.String stringKey;  // 0x20

        // Methods
        // RVA: 0x0B5A6F28  token: 0x60000AA
        public System.Void .ctor(System.Int32 key) { }
        // RVA: 0x0B5A6F58  token: 0x60000AB
        public System.Void .ctor(System.Int64 key) { }
        // RVA: 0x0B5A6F40  token: 0x60000AC
        public System.Void .ctor(System.String key) { }

    }

    // TypeToken: 0x200000D  // size: 0x70
    public sealed struct UnionKeyEnumerator : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_parent;  // 0x10
        private System.Int32 m_index;  // 0x58
        private System.Int32 m_currentSlotIndex;  // 0x5c
        private System.Int32 m_currentSlotStartIndex;  // 0x60
        private Beyond.SparkBuffer.Runtime.Wrapper.HashSlot m_currentSlot;  // 0x64

        // Properties
        Beyond.SparkBuffer.Runtime.Map.UnionKey Current { get; /* RVA: 0x0B5A6D48 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0B5A6C94 */ }

        // Methods
        // RVA: 0x0B5A6CF4  token: 0x60000AF
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map parent) { }
        // RVA: 0x0B5A6BA8  token: 0x60000B0
        public virtual System.Boolean MoveNext() { }
        // RVA: 0x041E1670  token: 0x60000B1
        public virtual System.Void Dispose() { }
        // RVA: 0x04DBF490  token: 0x60000B2
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200000F  // size: 0x38
    public sealed struct Options
    {
        // Fields
        public System.String rootName;  // 0x10
        public Beyond.SparkBuffer.SparkType rootType;  // 0x18
        public Beyond.SparkBuffer.SparkType subType1;  // 0x19
        public Beyond.SparkBuffer.SparkType subType2;  // 0x1a
        public Beyond.SparkBuffer.Runtime.BeanType beanType;  // 0x20
        public Beyond.SparkBuffer.Runtime.EnumType enumType;  // 0x28
        public Beyond.SparkBuffer.Runtime.EnumType mapKeyEnumType;  // 0x30

    }

    // TypeToken: 0x2000013  // size: 0x70
    public sealed struct ReservedField
    {
        // Fields
        public System.Int32 position;  // 0x10
        public Newtonsoft.Json.Linq.JToken token;  // 0x18
        public Beyond.SparkBuffer.SparkType type;  // 0x20
        public Beyond.SparkBuffer.Runtime.BeanType beanType;  // 0x28
        public Beyond.SparkBuffer.Runtime.ArrayType arrayType;  // 0x30
        public Beyond.SparkBuffer.Runtime.MapType mapType;  // 0x48

    }

    // TypeToken: 0x2000012  // size: 0x20
    public class JsonLoader : System.IDisposable
    {
        // Fields
        private Beyond.SparkBuffer.Runtime.ByteWriter m_writer;  // 0x10
        private System.Collections.Generic.Dictionary<System.String,System.Int32> m_stringMap;  // 0x18

        // Methods
        // RVA: 0x0B59B2AC  token: 0x60000DD
        private System.Void .ctor(Beyond.SparkBuffer.Runtime.ByteWriter writer) { }
        // RVA: 0x040E20A0  token: 0x60000DE
        public virtual System.Void Dispose() { }
        // RVA: 0x0B597A64  token: 0x60000DF
        private static System.Void PopulateJsonDataWithoutTypeAndRootDef(Beyond.SparkBuffer.Runtime.ByteWriter writer, Newtonsoft.Json.Linq.JToken rootObj, Beyond.SparkBuffer.Runtime.Root.Options rootOptions, System.Int32& dataOffset) { }
        // RVA: 0x0B59B218  token: 0x60000E0
        private System.Void _LoadJsonContentWithoutTypeAndRootDef(Newtonsoft.Json.Linq.JToken rootObj, Beyond.SparkBuffer.Runtime.Root.Options rootOptions, System.Int32& dataOffset) { }
        // RVA: 0x0B59AAEC  token: 0x60000E1
        private System.Void _ExportStrings(Newtonsoft.Json.Linq.JToken rootJson, Beyond.SparkBuffer.Runtime.Root.Options rootOptions) { }
        // RVA: 0x0B597DB4  token: 0x60000E2
        private System.Void _CollectBeanStrings(System.Collections.Generic.HashSet<System.String> stringSet, Newtonsoft.Json.Linq.JObject obj, Beyond.SparkBuffer.Runtime.BeanType beanType) { }
        // RVA: 0x0B597BA0  token: 0x60000E3
        private System.Void _CollectArrayStrings(System.Collections.Generic.HashSet<System.String> stringSet, Newtonsoft.Json.Linq.JArray obj, Beyond.SparkBuffer.Runtime.ArrayType arrayType) { }
        // RVA: 0x0B59827C  token: 0x60000E4
        private System.Void _CollectMapStrings(System.Collections.Generic.HashSet<System.String> stringSet, Newtonsoft.Json.Linq.JObject obj, Beyond.SparkBuffer.Runtime.MapType mapType) { }
        // RVA: 0x0B59A740  token: 0x60000E5
        private System.Int32 _ExportRootData(Newtonsoft.Json.Linq.JToken rootJson, Beyond.SparkBuffer.Runtime.Root.Options rootOptions) { }
        // RVA: 0x0B598D50  token: 0x60000E6
        private System.Int32 _ExportBeanData(Newtonsoft.Json.Linq.JObject beanObj, Beyond.SparkBuffer.Runtime.BeanType beanType) { }
        // RVA: 0x0B5988F0  token: 0x60000E7
        private System.Int32 _ExportArrayData(Newtonsoft.Json.Linq.JArray arrayObj, Beyond.SparkBuffer.Runtime.ArrayType arrayType) { }
        // RVA: 0x0B599978  token: 0x60000E8
        private System.Int32 _ExportMapData(Newtonsoft.Json.Linq.JObject mapObj, Beyond.SparkBuffer.Runtime.MapType mapType) { }
        // RVA: 0x0B5984F0  token: 0x60000E9
        private System.Collections.Generic.Dictionary<System.Int32,System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<System.String,Newtonsoft.Json.Linq.JToken>>> _CreateMapHashEntry(Newtonsoft.Json.Linq.JObject mapObj, Beyond.SparkBuffer.SparkType keyType) { }
        // RVA: 0x0B59AFD8  token: 0x60000EA
        private System.Int32 _GetMapKeyHash(System.String key, Beyond.SparkBuffer.SparkType keyType) { }
        // RVA: 0x0B599FC4  token: 0x60000EB
        private System.Void _ExportMapKey(System.String key, Beyond.SparkBuffer.SparkType keyType) { }
        // RVA: 0x0B59A194  token: 0x60000EC
        private System.Void _ExportMapValue(Newtonsoft.Json.Linq.JToken valueToken, Beyond.SparkBuffer.Runtime.MapType mapType, System.Collections.Generic.List<Beyond.SparkBuffer.Runtime.SparkManager.JsonLoader.ReservedField> reservedFields) { }
        // RVA: 0x0B59A334  token: 0x60000ED
        private System.Void _ExportReservedFields(System.Collections.Generic.List<Beyond.SparkBuffer.Runtime.SparkManager.JsonLoader.ReservedField> reservedFields) { }
        // RVA: 0x0B59A9E8  token: 0x60000EE
        private System.Void _ExportStringByLoc(System.String strVal) { }
        // RVA: 0x0B5997E4  token: 0x60000EF
        private System.Void _ExportEnumData(Newtonsoft.Json.Linq.JToken token, Beyond.SparkBuffer.Runtime.EnumType enumType) { }
        // RVA: 0x0B599540  token: 0x60000F0
        private System.Void _ExportCommonDataOrThrow(Newtonsoft.Json.Linq.JToken token, Beyond.SparkBuffer.SparkType type, System.Collections.Generic.List<Beyond.SparkBuffer.Runtime.SparkManager.JsonLoader.ReservedField> reservedFields) { }
        // RVA: 0x0B598800  token: 0x60000F1
        private static System.Void _EnsureJTokenType(Newtonsoft.Json.Linq.JToken token, Newtonsoft.Json.Linq.JTokenType requiredType) { }
        // RVA: 0x0B59B178  token: 0x60000F2
        private static System.Exception _InvalidRootTypeError(Beyond.SparkBuffer.SparkType rootType) { }

    }

    // TypeToken: 0x2000015  // size: 0x18
    public sealed class <>c__DisplayClass14_0
    {
        // Fields
        public System.IntPtr L;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x60000FE
        public System.Void .ctor() { }
        // RVA: 0x03FA9A90  token: 0x60000FF
        private System.Void <RegisterToLua>b__0(Beyond.SparkBuffer.Runtime.Root root) { }
        // RVA: 0x0B5A6B14  token: 0x6000100
        private System.Void <RegisterToLua>b__1(Beyond.SparkBuffer.Runtime.Root root) { }
        // RVA: 0x0B5A6B38  token: 0x6000101
        private System.Void <RegisterToLua>b__2() { }

    }

    // TypeToken: 0x2000020  // size: 0x20
    public sealed struct EnumItem
    {
        // Fields
        public System.String name;  // 0x10
        public System.Int32 value;  // 0x18

    }

    // TypeToken: 0x2000053  // size: 0x20
    public sealed struct ReflectResult
    {
        // Fields
        public Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType[] types;  // 0x10
        public Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject rootObj;  // 0x18

    }

    // TypeToken: 0x2000055  // size: 0x20
    public sealed struct FieldInfo
    {
        // Fields
        public System.String name;  // 0x10
        public Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType type;  // 0x18

    }

    // TypeToken: 0x2000054  // size: 0x38
    public class ReflectType
    {
        // Fields
        public Beyond.SparkBuffer.SparkType sparkType;  // 0x10
        public System.Type concreteType;  // 0x18
        public Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType subType1;  // 0x20
        public Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType subType2;  // 0x28
        public System.Collections.Generic.List<Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType.FieldInfo> beanFields;  // 0x30

        // Methods
        // RVA: 0x041E1670  token: 0x6000321
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000056  // size: 0x38
    public class ReflectObject
    {
        // Fields
        public System.Object originObj;  // 0x10
        public Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType reflectType;  // 0x18
        public System.Collections.Specialized.OrderedDictionary beanElements;  // 0x20
        public System.Collections.Generic.List<Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject> arrayElements;  // 0x28
        public System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject,Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject>> mapElements;  // 0x30

        // Methods
        // RVA: 0x041E1670  token: 0x6000322
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000057  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Beyond.SparkBuffer.Serialize.ObjectReflector.<>c <>9;  // static @ 0x0
        public static System.Comparison<Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType> <>9__7_0;  // static @ 0x8
        public static System.Comparison<Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType.FieldInfo> <>9__9_0;  // static @ 0x10
        public static System.Comparison<System.Collections.Generic.KeyValuePair<Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject,Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject>> <>9__13_0;  // static @ 0x18
        public static System.Func<System.Reflection.FieldInfo,System.Int32> <>9__14_0;  // static @ 0x20

        // Methods
        // RVA: 0x0B5A6B44  token: 0x6000323
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000324
        public System.Void .ctor() { }
        // RVA: 0x0B5A69E0  token: 0x6000325
        private System.Int32 <_ParseInternal>b__7_0(Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType lhs, Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType rhs) { }
        // RVA: 0x0B5A6AFC  token: 0x6000326
        private System.Int32 <_TryParseType>b__9_0(Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType.FieldInfo lhs, Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType.FieldInfo rhs) { }
        // RVA: 0x0B5A6A28  token: 0x6000327
        private System.Int32 <_ReflectMap>b__13_0(System.Collections.Generic.KeyValuePair<Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject,Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject> lhs, System.Collections.Generic.KeyValuePair<Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject,Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject> rhs) { }
        // RVA: 0x0B5A69C0  token: 0x6000328
        private System.Int32 <_GetFields>b__14_0(System.Reflection.FieldInfo field) { }

    }

    // TypeToken: 0x2000059  // size: 0x20
    public sealed struct ReservedField
    {
        // Fields
        public System.Int32 position;  // 0x10
        public Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject reflectObj;  // 0x18

    }

    // TypeToken: 0x200005A  // size: 0x20
    public sealed struct Options
    {
        // Fields
        public static readonly Beyond.SparkBuffer.Serialize.SparkSerializer.Options DEFAULT;  // static @ 0x0
        public System.String rootName;  // 0x10
        public System.String stripTypeNamePrefix;  // 0x18

        // Methods
        // RVA: 0x033928A0  token: 0x6000344
        public System.Void .ctor(System.String rootName) { }
        // RVA: 0x039CF630  token: 0x6000345
        public System.Void .ctor(System.String rootName, System.String stripTypeNamePrefix) { }
        // RVA: 0x0B5A7BB0  token: 0x6000346
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200005B  // size: 0x18
    public sealed struct HashSlot
    {
        // Fields
        public System.Int32 offset;  // 0x10
        public System.Int32 bucketsSize;  // 0x14

    }

    // TypeToken: 0x200005C  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly Beyond.SparkBuffer.Serialize.SparkSerializer.<>c <>9;  // static @ 0x0
        public static System.Comparison<System.String> <>9__23_0;  // static @ 0x8

        // Methods
        // RVA: 0x0B5A9B84  token: 0x6000347
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x6000348
        public System.Void .ctor() { }
        // RVA: 0x0A7BA4E0  token: 0x6000349
        private System.Int32 <_ExportStrings>b__23_0(System.String lhs, System.String rhs) { }

    }

namespace Beyond.Reflection
{

    // TypeToken: 0x200005D  // size: 0x10
    public class MethodPointerManager
    {
        // Methods
        // RVA: -1  // generic def  token: 0x600034A
        public static T2 CallPointer(System.IntPtr ptr, T1 param) { }
        // RVA: -1  // generic def  token: 0x600034B
        public static TRet CallDelegatePointer(System.IntPtr ptr, TParam1 target) { }
        // RVA: -1  // generic def  token: 0x600034C
        public static TRet CallDelegatePointer(System.IntPtr ptr, TParam1 target, TParam2 param) { }
        // RVA: -1  // generic def  token: 0x600034D
        public static System.ValueTuple<System.IntPtr,T1> GetDelegatePointer(System.Delegate d) { }
        // RVA: 0x0B5A7980  token: 0x600034E
        public static System.IntPtr GetDelegatePointer(System.Reflection.MethodInfo d) { }
        // RVA: 0x041E1670  token: 0x600034F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200005E  // size: 0x10
    public static class ObjectPointerHelper
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000350
        public static System.IntPtr GetIntPtr(T& value) { }
        // RVA: -1  // generic def  token: 0x6000351
        public static System.IntPtr GetArrayPtr(T[] value) { }
        // RVA: -1  // generic def  token: 0x6000352
        public static T GetStructByPointer(System.IntPtr ptr) { }
        // RVA: -1  // generic def  token: 0x6000353
        public static T GetValue(System.IntPtr ptr, System.Int32 offset) { }
        // RVA: -1  // generic def  token: 0x6000354
        public static T GetArrayElement(System.IntPtr array, System.Int32 index) { }
        // RVA: -1  // generic def  token: 0x6000355
        public static System.Void SetValue(System.IntPtr ptr, System.Int32 offset, T value) { }
        // RVA: -1  // generic def  token: 0x6000356
        public static System.Void SetArrayElement(System.IntPtr array, System.Int32 index, T value) { }
        // RVA: 0x0B5A7B9C  token: 0x6000357
        public static System.Void SetValue(System.IntPtr ptr, System.Int32 offset, System.Byte* data, System.Int32 size) { }
        // RVA: 0x0B5A7A3C  token: 0x6000358
        public static System.Object GetObj(System.IntPtr ptr, System.Int32 offset) { }
        // RVA: 0x0B5A7B4C  token: 0x6000359
        public static System.Void SetObj(System.IntPtr ptr, System.Int32 offset, System.Object value) { }
        // RVA: 0x0B5A7A34  token: 0x600035A
        public static System.Void CopyValue(System.IntPtr target, System.IntPtr source, System.Int32 size) { }
        // RVA: 0x0B5A7A34  token: 0x600035B
        public static System.Void CopyValue(System.Void* target, System.Void* source, System.Int32 size) { }
        // RVA: 0x0B5A79D0  token: 0x600035C
        public static System.Void CopyValue(System.Byte* to, System.Byte* from, System.Int32 size) { }
        // RVA: 0x0B5A7A78  token: 0x600035D
        public static System.ReadOnlySpan<System.Byte> GetReadOnlySpan(System.IntPtr ptr, System.Int32 maxLength) { }

    }

    // TypeToken: 0x200005F  // size: 0x10
    public class StructMemManager
    {
        // Fields
        private static System.Collections.Generic.Dictionary<System.Int32,System.ValueTuple<System.Collections.Generic.HashSet<System.Int64>,System.Collections.Generic.List<System.IntPtr>>> s_structMemDict;  // static @ 0x0

        // Methods
        // RVA: 0x0B5A84AC  token: 0x600035E
        public static System.IntPtr GetMemoryBySize(System.Int32 size) { }
        // RVA: 0x0B5A837C  token: 0x600035F
        public static System.Void FreeMemory(System.Int32 size, System.IntPtr ptr) { }
        // RVA: 0x0B5A7C44  token: 0x6000360
        public static System.Void ClearMemory() { }
        // RVA: 0x0B5A7F84  token: 0x6000361
        public static System.String DumpMemory() { }
        // RVA: 0x041E1670  token: 0x6000362
        public System.Void .ctor() { }
        // RVA: 0x0B5A870C  token: 0x6000363
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000060  // size: 0x10
    public class StructPtrRefrenceManager
    {
        // Fields
        private static System.Collections.Generic.Dictionary<System.Object,System.Object> s_refDic;  // static @ 0x0
        private static System.Collections.Generic.Dictionary<System.Type,System.Collections.Generic.List<System.Int32>> s_structPtrOffsetDic;  // static @ 0x8

        // Methods
        // RVA: 0x03B7D0C0  token: 0x6000364
        private static System.Void _RunOnStart() { }
        // RVA: 0x0B5A8798  token: 0x6000365
        public static System.Void AddObjInPtrRef(System.Type type, System.IntPtr ptr) { }
        // RVA: 0x0B5A8A64  token: 0x6000366
        public static System.Void RemoveObjInPtrRef(System.Type type, System.IntPtr ptr) { }
        // RVA: 0x041E1670  token: 0x6000367
        public System.Void .ctor() { }
        // RVA: 0x04CA3A20  token: 0x6000368
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000061  // size: 0x28
    public class StructPtrWrapper
    {
        // Fields
        public System.IntPtr ptr;  // 0x10
        public System.Int32 size;  // 0x18
        public System.Type type;  // 0x20
        private static UnityEngine.Pool.ObjectPool<Beyond.Reflection.StructPtrWrapper> s_pool;  // static @ 0x0
        private static System.IntPtr s_ptr;  // static @ 0x8
        private static System.Int32 s_size;  // static @ 0x10
        private static System.Type s_type;  // static @ 0x18
        private static System.Object s_structObj;  // static @ 0x20

        // Methods
        // RVA: 0x0B5A90EC  token: 0x6000369
        public static Beyond.Reflection.StructPtrWrapper GetStructPtrWrapper(System.IntPtr ptr, System.Int32 size, System.Type type) { }
        // RVA: 0x0B5A8FA0  token: 0x600036A
        public static Beyond.Reflection.StructPtrWrapper GetStructPtrWrapper(System.Object structObj) { }
        // RVA: 0x0B5A92D4  token: 0x600036B
        public static System.Void Release(Beyond.Reflection.StructPtrWrapper obj) { }
        // RVA: 0x0B5A9A18  token: 0x600036C
        public System.Void .ctor(System.IntPtr ptr, System.Int32 size, System.Type type) { }
        // RVA: 0x0B5A8EC0  token: 0x600036D
        public System.Object GetStructObj() { }
        // RVA: 0x0B5A8E1C  token: 0x600036E
        public static System.Object GetStructObj(System.IntPtr ptr, System.Type type, System.Int32 size) { }
        // RVA: 0x0B5A93B8  token: 0x600036F
        public System.Void SetStructObjValue(System.Object structObj) { }
        // RVA: 0x0B5A8DBC  token: 0x6000370
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B5A8CEC  token: 0x6000371
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0B5A943C  token: 0x6000372
        public virtual System.String ToString() { }
        // RVA: 0x0B5A9288  token: 0x6000373
        public System.Type GetType() { }
        // RVA: 0x0B5A8F4C  token: 0x6000374
        public static System.Object GetStructObj(Beyond.Reflection.StructPtrWrapper obj) { }
        // RVA: 0x0B5A9AA4  token: 0x6000375
        public System.Void .ctor(System.Object structObj) { }
        // RVA: 0x0B5A949C  token: 0x6000376
        private static Beyond.Reflection.StructPtrWrapper _OnCreate() { }
        // RVA: 0x0B5A95D0  token: 0x6000377
        private static System.Void _OnGet(Beyond.Reflection.StructPtrWrapper o) { }
        // RVA: 0x0B5A97D4  token: 0x6000378
        private static System.Void _OnRealse(Beyond.Reflection.StructPtrWrapper obj) { }
        // RVA: 0x0B5A9884  token: 0x6000379
        private static System.Void .cctor() { }
        // RVA: 0x04D87100  token: 0x600037A
        public System.Int32 <>iFixBaseProxy_GetHashCode() { }
        // RVA: 0x02FB1840  token: 0x600037B
        public System.Boolean <>iFixBaseProxy_Equals(System.Object P0) { }
        // RVA: 0x0669AD6C  token: 0x600037C
        public System.String <>iFixBaseProxy_ToString() { }

    }

}

namespace Beyond.SparkBuffer
{

    // TypeToken: 0x2000004  // size: 0x11
    public sealed struct SparkType
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static Beyond.SparkBuffer.SparkType Bool;  // const
        public static Beyond.SparkBuffer.SparkType Byte;  // const
        public static Beyond.SparkBuffer.SparkType Int;  // const
        public static Beyond.SparkBuffer.SparkType Long;  // const
        public static Beyond.SparkBuffer.SparkType Float;  // const
        public static Beyond.SparkBuffer.SparkType Double;  // const
        public static Beyond.SparkBuffer.SparkType Enum;  // const
        public static Beyond.SparkBuffer.SparkType String;  // const
        public static Beyond.SparkBuffer.SparkType Bean;  // const
        public static Beyond.SparkBuffer.SparkType Array;  // const
        public static Beyond.SparkBuffer.SparkType Map;  // const

    }

    // TypeToken: 0x2000005  // size: 0x10
    public static class Common
    {
        // Fields
        public static System.Int32 NULL_PLACEHOLDER;  // const
        public static System.Int32 BOOL_MEM_SIZE;  // const
        public static System.Int32 BYTE_MEM_SIZE;  // const
        public static System.Int32 INT_MEM_SIZE;  // const
        public static System.Int32 LONG_MEM_SIZE;  // const
        public static System.Int32 FLOAT_MEM_SIZE;  // const
        public static System.Int32 DOUBLE_MEM_SIZE;  // const

    }

    // TypeToken: 0x2000006  // size: 0x10
    public static class Utils
    {
        // Methods
        // RVA: 0x0B5A6F70  token: 0x6000003
        public static System.Int32 Align4Bytes(System.Int32 position) { }
        // RVA: 0x0B5A6F84  token: 0x6000004
        public static System.Int32 Align8Bytes(System.Int32 position) { }
        // RVA: 0x0B5A6F98  token: 0x6000005
        public static System.Int32 AlignBytesForSize(System.Int32 position, System.Int32 size) { }
        // RVA: 0x03293C50  token: 0x6000006
        public static System.Int32 GetMemSizeForType(Beyond.SparkBuffer.SparkType sparkType) { }
        // RVA: 0x03292FE0  token: 0x6000007
        public static System.Int32 GetFieldOffset(Beyond.SparkBuffer.SparkType sparkType, System.Int32& postion) { }
        // RVA: 0x0B5A6FF8  token: 0x6000008
        public static System.Boolean IsEnumOrBean(Beyond.SparkBuffer.SparkType sparkType) { }
        // RVA: 0x0B5A6FE8  token: 0x6000009
        public static System.Boolean IsArrayOrMap(Beyond.SparkBuffer.SparkType sparkType) { }
        // RVA: 0x0B5A6FC0  token: 0x600000A
        public static System.Boolean CheckValidMapKeyType(Beyond.SparkBuffer.SparkType sparkType) { }
        // RVA: 0x0B5A6FD4  token: 0x600000B
        public static System.Boolean CheckValidRootType(Beyond.SparkBuffer.SparkType sparkType) { }

    }

}

namespace Beyond.SparkBuffer.Runtime
{

    // TypeToken: 0x2000007  // size: 0x30
    public class ByteReader : System.IDisposable
    {
        // Fields
        private Unity.Collections.NativeArray<System.Byte> m_buffer;  // 0x10
        private System.Byte* m_bufferPtr;  // 0x20
        private System.Int32 m_position;  // 0x28
        private readonly System.Int32 m_length;  // 0x2c
        public static Beyond.SparkBuffer.Runtime.IStringPool s_stringPool;  // static @ 0x0
        public static System.Action<System.IntPtr> s_freeMemory;  // static @ 0x8
        public static System.Func<System.Byte[],System.IntPtr> s_mallocMemory;  // static @ 0x10

        // Properties
        System.Int32 position { get; /* RVA: 0x011F2A90 */ }
        Unity.Collections.NativeArray<System.Byte> buffer { get; /* RVA: 0x02B2D510 */ }

        // Methods
        // RVA: 0x02F218C0  token: 0x600000E
        public System.Void .ctor(System.Byte[] buffer) { }
        // RVA: 0x0B595A14  token: 0x600000F
        public virtual System.Void Dispose() { }
        // RVA: 0x011F2AA0  token: 0x6000010
        public System.Void SeekTo(System.Int32 pos) { }
        // RVA: 0x0B595A5C  token: 0x6000011
        public System.Boolean ReadBool() { }
        // RVA: 0x04DBF440  token: 0x6000012
        public System.Byte ReadByte() { }
        // RVA: 0x0B595ACC  token: 0x6000013
        public System.Int32 ReadInt() { }
        // RVA: 0x0B595AF8  token: 0x6000014
        public System.Int64 ReadLong() { }
        // RVA: 0x0B595AA0  token: 0x6000015
        public System.Single ReadFloat() { }
        // RVA: 0x0B595A74  token: 0x6000016
        public System.Double ReadDouble() { }
        // RVA: 0x0B595B28  token: 0x6000017
        public System.String ReadString() { }
        // RVA: 0x04DBF460  token: 0x6000018
        public Beyond.SparkBuffer.Runtime.Wrapper.HashSlot ReadHashSlot() { }
        // RVA: 0x04DBF440  token: 0x6000019
        public Beyond.SparkBuffer.SparkType ReadSparkType() { }
        // RVA: 0x0B595BF0  token: 0x600001A
        public System.Int32 ReadTypeHash() { }

    }

    // TypeToken: 0x2000008  // size: 0x20
    public class ByteWriter : System.IDisposable
    {
        // Fields
        private static readonly System.Byte[] BUFFER_0;  // static @ 0x0
        private readonly System.IO.BinaryWriter m_writer;  // 0x10
        private readonly System.IO.Stream m_stream;  // 0x18
        public static Beyond.SparkBuffer.Runtime.IStringPool s_stringPool;  // static @ 0x8

        // Properties
        System.Int32 position { get; /* RVA: 0x0B59625C */ }

        // Methods
        // RVA: 0x0B5961D8  token: 0x600001C
        public System.Void .ctor(System.IO.Stream stream) { }
        // RVA: 0x0B595BF8  token: 0x600001D
        public virtual System.Void Dispose() { }
        // RVA: 0x0B595C78  token: 0x600001E
        public System.Void SeekTo(System.Int32 pos) { }
        // RVA: 0x0B595CA4  token: 0x600001F
        public System.Void WriteBool(System.Boolean v) { }
        // RVA: 0x0B595CC4  token: 0x6000020
        public System.Void WriteByte(System.Byte v) { }
        // RVA: 0x0B595DF8  token: 0x6000021
        public System.Void WriteInt(System.Int32 v) { }
        // RVA: 0x0B595E34  token: 0x6000022
        public System.Void WriteLong(System.Int64 v) { }
        // RVA: 0x0B595DBC  token: 0x6000023
        public System.Void WriteFloat(System.Single v) { }
        // RVA: 0x0B595D80  token: 0x6000024
        public System.Void WriteDouble(System.Double v) { }
        // RVA: 0x0B595E70  token: 0x6000025
        public System.Void WriteStringAlignment(System.String v) { }
        // RVA: 0x0B595F5C  token: 0x6000026
        public System.Void WriteString(System.String v) { }
        // RVA: 0x0B595CE4  token: 0x6000027
        public System.Void WriteBytes(System.ReadOnlySpan<System.Byte> buffer, System.Int32 index, System.Int32 count) { }
        // RVA: 0x0B595CC4  token: 0x6000028
        public System.Void WriteSparkType(Beyond.SparkBuffer.SparkType sparkType) { }
        // RVA: 0x0B595C34  token: 0x6000029
        private System.Int32 FillAlignment(System.Int32 align) { }
        // RVA: 0x0B596038  token: 0x600002A
        private System.Void _FillAlignment4() { }
        // RVA: 0x0B5960D4  token: 0x600002B
        private System.Void _FillAlignment8() { }
        // RVA: 0x0B596170  token: 0x600002C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000009  // size: 0x38
    public sealed struct Array
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.ArrayType m_arrayType;  // 0x10
        private readonly Beyond.SparkBuffer.Runtime.Root m_root;  // 0x28
        private readonly System.Int32 m_dataOffset;  // 0x30
        private readonly System.Int32 m_length;  // 0x34

        // Properties
        System.Int32 length { get; /* RVA: 0x011F0020 */ }

        // Methods
        // RVA: 0x0305EE00  token: 0x600002E
        private System.Void .ctor(Beyond.SparkBuffer.Runtime.ArrayType arrayType, System.Int32 offset, Beyond.SparkBuffer.Runtime.Root root) { }
        // RVA: 0x0B594D2C  token: 0x600002F
        public System.Boolean GetBool(System.Int32 index) { }
        // RVA: 0x0B594B48  token: 0x6000030
        public System.Byte GetByte(System.Int32 index) { }
        // RVA: 0x0B594EE8  token: 0x6000031
        public System.Int32 GetInt(System.Int32 index) { }
        // RVA: 0x0B594BB4  token: 0x6000032
        public System.Int64 GetLong(System.Int32 index) { }
        // RVA: 0x0B594E78  token: 0x6000033
        public System.Single GetFloat(System.Int32 index) { }
        // RVA: 0x0B594D98  token: 0x6000034
        public System.Double GetDouble(System.Int32 index) { }
        // RVA: 0x0B594F58  token: 0x6000035
        public System.String GetString(System.Int32 index) { }
        // RVA: 0x0B594E08  token: 0x6000036
        public System.Int32 GetEnum(System.Int32 index) { }
        // RVA: 0x0B594C9C  token: 0x6000037
        public System.Nullable<Beyond.SparkBuffer.Runtime.Bean> GetBean(System.Int32 index) { }

    }

    // TypeToken: 0x200000A  // size: 0x28
    public sealed struct Bean
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.BeanType m_beanType;  // 0x10
        private readonly Beyond.SparkBuffer.Runtime.Root m_root;  // 0x18
        private readonly System.Int32 m_dataOffset;  // 0x20

        // Methods
        // RVA: 0x0305FE10  token: 0x6000038
        private System.Void .ctor(Beyond.SparkBuffer.Runtime.BeanType beanType, System.Int32 offset, Beyond.SparkBuffer.Runtime.Root root) { }
        // RVA: 0x0B595180  token: 0x6000039
        public System.Boolean GetBool(System.String key, System.Int32& offset) { }
        // RVA: 0x0B5951F0  token: 0x600003A
        public System.Boolean GetBool(System.Int32 offset) { }
        // RVA: 0x0B595218  token: 0x600003B
        public System.Byte GetByte(System.String key, System.Int32& offset) { }
        // RVA: 0x0B595288  token: 0x600003C
        public System.Byte GetByte(System.Int32 offset) { }
        // RVA: 0x0B595370  token: 0x600003D
        public System.Int32 GetInt(System.String key, System.Int32& offset) { }
        // RVA: 0x0B595348  token: 0x600003E
        public System.Int32 GetInt(System.Int32 offset) { }
        // RVA: 0x0B595478  token: 0x600003F
        public System.Int64 GetLong(System.String key, System.Int32& offset) { }
        // RVA: 0x0B5954E8  token: 0x6000040
        public System.Int64 GetLong(System.Int32 offset) { }
        // RVA: 0x0B595408  token: 0x6000041
        public System.Single GetFloat(System.String key, System.Int32& offset) { }
        // RVA: 0x0B5953E0  token: 0x6000042
        public System.Single GetFloat(System.Int32 offset) { }
        // RVA: 0x0B5952B0  token: 0x6000043
        public System.Double GetDouble(System.String key, System.Int32& offset) { }
        // RVA: 0x0B595320  token: 0x6000044
        public System.Double GetDouble(System.Int32 offset) { }
        // RVA: 0x0B595510  token: 0x6000045
        public System.String GetString(System.String key, System.Int32& offset) { }
        // RVA: 0x0B595580  token: 0x6000046
        public System.String GetString(System.Int32 offset) { }
        // RVA: 0x0B595370  token: 0x6000047
        public System.Int32 GetEnum(System.String key, System.Int32& offset) { }
        // RVA: 0x0B595348  token: 0x6000048
        public System.Int32 GetEnum(System.Int32 offset) { }
        // RVA: 0x0305F690  token: 0x6000049
        public System.Nullable<Beyond.SparkBuffer.Runtime.Bean> GetBean(System.String key) { }
        // RVA: 0x0305EF30  token: 0x600004A
        public System.Nullable<Beyond.SparkBuffer.Runtime.Array> GetArray(System.String key) { }
        // RVA: 0x03293520  token: 0x600004B
        public System.Nullable<Beyond.SparkBuffer.Runtime.Map> GetMap(System.String key) { }

    }

    // TypeToken: 0x200000B  // size: 0x58
    public sealed struct Map : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private static System.Int32 INT_KEY_MEM_SIZE;  // const
        private static System.Int32 LONG_KEY_MEM_SIZE;  // const
        private readonly Beyond.SparkBuffer.Runtime.MapType m_mapType;  // 0x10
        private readonly Beyond.SparkBuffer.Runtime.Root m_root;  // 0x38
        private readonly System.Int32 m_valueMemSize;  // 0x40
        private readonly System.Int32 m_keyMemSize;  // 0x44
        private readonly System.Int32 m_dataOffset;  // 0x48
        private readonly System.Int32 m_count;  // 0x4c
        private readonly System.Int32 m_slotsOffset;  // 0x50

        // Properties
        System.Int32 count { get; /* RVA: 0x04D86640 */ }

        // Methods
        // RVA: 0x03293AA0  token: 0x600004D
        private System.Void .ctor(Beyond.SparkBuffer.Runtime.MapType mapType, System.Int32 offset, Beyond.SparkBuffer.Runtime.Root root) { }
        // RVA: 0x0B59D430  token: 0x600004E
        public Beyond.SparkBuffer.Runtime.Map.UnionKeyEnumerator GetEnumerator() { }
        // RVA: 0x0B59DCA0  token: 0x600004F
        private virtual System.Collections.Generic.IEnumerator<Beyond.SparkBuffer.Runtime.Map.UnionKey> System.Collections.Generic.IEnumerable<Beyond.SparkBuffer.Runtime.Map.UnionKey>.GetEnumerator() { }
        // RVA: 0x0B59DD2C  token: 0x6000050
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: 0x0B59D604  token: 0x6000051
        private static System.Int32 GetHash(System.String str) { }
        // RVA: 0x04D88BE0  token: 0x6000052
        private static System.Int32 GetHash(System.Int32 value) { }
        // RVA: 0x04D88BE0  token: 0x6000053
        private static System.Int32 GetHash(System.Int64 value) { }
        // RVA: 0x0B59D588  token: 0x6000054
        private Beyond.SparkBuffer.Runtime.Wrapper.HashSlot GetHashSlot(System.Int32 slotIndex) { }
        // RVA: 0x0B59E35C  token: 0x6000055
        private System.Int32 _FindHashIndexByKey(System.Int32 intKey) { }
        // RVA: 0x0B59E280  token: 0x6000056
        private System.Int32 _FindHashIndexByKey(System.Int64 longKey) { }
        // RVA: 0x0B59E434  token: 0x6000057
        private System.Int32 _FindHashIndexByKey(System.String stringKey) { }
        // RVA: 0x0B59D624  token: 0x6000058
        private System.Int32 GetIntKeyByIndex(System.Int32 index) { }
        // RVA: 0x0B59D878  token: 0x6000059
        private System.Int64 GetLongKeyByIndex(System.Int32 index) { }
        // RVA: 0x0B59DA2C  token: 0x600005A
        private System.String GetStringKeyByIndex(System.Int32 index) { }
        // RVA: 0x0B59D720  token: 0x600005B
        private System.Int32 GetIntKeyBySlotAndLocalIndex(Beyond.SparkBuffer.Runtime.Wrapper.HashSlot slot, System.Int32 localIndex) { }
        // RVA: 0x086D4544  token: 0x600005C
        private System.Int64 GetLongKeyBySlotAndLocalIndex(Beyond.SparkBuffer.Runtime.Wrapper.HashSlot slot, System.Int32 localIndex) { }
        // RVA: 0x0B59DB28  token: 0x600005D
        private System.String GetStringKeyBySlotAndLocalIndex(Beyond.SparkBuffer.Runtime.Wrapper.HashSlot slot, System.Int32 localIndex) { }
        // RVA: 0x03291800  token: 0x600005E
        public System.Boolean ContainsKey(System.String key) { }
        // RVA: 0x0B59D11C  token: 0x600005F
        public System.Boolean GetBool(System.String key) { }
        // RVA: 0x037534D0  token: 0x6000060
        public System.Boolean TryGetBool(System.String key, System.Boolean& value) { }
        // RVA: 0x0B59D200  token: 0x6000061
        public System.Byte GetByte(System.String key) { }
        // RVA: 0x0B59DE90  token: 0x6000062
        public System.Boolean TryGetByte(System.String key, System.Byte& value) { }
        // RVA: 0x03BAFE50  token: 0x6000063
        public System.Int32 GetInt(System.String key) { }
        // RVA: 0x0353C320  token: 0x6000064
        public System.Boolean TryGetInt(System.String key, System.Int32& value) { }
        // RVA: 0x0B59D9A8  token: 0x6000065
        public System.Int64 GetLong(System.String key) { }
        // RVA: 0x0B59E238  token: 0x6000066
        public System.Boolean TryGetLong(System.String key, System.Int64& value) { }
        // RVA: 0x0B59D4EC  token: 0x6000067
        public System.Single GetFloat(System.String key) { }
        // RVA: 0x0B59E0D0  token: 0x6000068
        public System.Boolean TryGetFloat(System.String key, System.Single& value) { }
        // RVA: 0x0B59D2B0  token: 0x6000069
        public System.Double GetDouble(System.String key) { }
        // RVA: 0x0B59DF20  token: 0x600006A
        public System.Boolean TryGetDouble(System.String key, System.Double& value) { }
        // RVA: 0x0B59DC38  token: 0x600006B
        public System.String GetString(System.String key) { }
        // RVA: 0x041EF060  token: 0x600006C
        public System.Boolean TryGetString(System.String key, System.String& value) { }
        // RVA: 0x0B59D394  token: 0x600006D
        public System.Int32 GetEnum(System.String key) { }
        // RVA: 0x0B59E088  token: 0x600006E
        public System.Boolean TryGetEnum(System.String key, System.Int32& value) { }
        // RVA: 0x038C2C50  token: 0x600006F
        public System.Nullable<Beyond.SparkBuffer.Runtime.Bean> GetBean(System.String key) { }
        // RVA: 0x038C3170  token: 0x6000070
        public System.Boolean TryGetBean(System.String key, System.Nullable<Beyond.SparkBuffer.Runtime.Bean>& bean) { }
        // RVA: 0x0331BB50  token: 0x6000071
        public System.Boolean ContainsKey(System.Int32 key) { }
        // RVA: 0x0B59D150  token: 0x6000072
        public System.Boolean GetBool(System.Int32 key) { }
        // RVA: 0x0B59DDB8  token: 0x6000073
        public System.Boolean TryGetBool(System.Int32 key, System.Boolean& value) { }
        // RVA: 0x0B59D1CC  token: 0x6000074
        public System.Byte GetByte(System.Int32 key) { }
        // RVA: 0x0B59DE48  token: 0x6000075
        public System.Boolean TryGetByte(System.Int32 key, System.Byte& value) { }
        // RVA: 0x0B59D3FC  token: 0x6000076
        public System.Int32 GetInt(System.Int32 key) { }
        // RVA: 0x03EBB2B0  token: 0x6000077
        public System.Boolean TryGetInt(System.Int32 key, System.Int32& value) { }
        // RVA: 0x0B59D9DC  token: 0x6000078
        public System.Int64 GetLong(System.Int32 key) { }
        // RVA: 0x0B59E1F0  token: 0x6000079
        public System.Boolean TryGetLong(System.Int32 key, System.Int64& value) { }
        // RVA: 0x0B59D520  token: 0x600007A
        public System.Single GetFloat(System.Int32 key) { }
        // RVA: 0x0B59E160  token: 0x600007B
        public System.Boolean TryGetFloat(System.Int32 key, System.Single& value) { }
        // RVA: 0x0B59D2E4  token: 0x600007C
        public System.Double GetDouble(System.Int32 key) { }
        // RVA: 0x0B59DFB0  token: 0x600007D
        public System.Boolean TryGetDouble(System.Int32 key, System.Double& value) { }
        // RVA: 0x03062620  token: 0x600007E
        public System.String GetString(System.Int32 key) { }
        // RVA: 0x0397BAB0  token: 0x600007F
        public System.Boolean TryGetString(System.Int32 key, System.String& value) { }
        // RVA: 0x0B59D3FC  token: 0x6000080
        public System.Int32 GetEnum(System.Int32 key) { }
        // RVA: 0x0B59E040  token: 0x6000081
        public System.Boolean TryGetEnum(System.Int32 key, System.Int32& value) { }
        // RVA: 0x038C1150  token: 0x6000082
        public System.Nullable<Beyond.SparkBuffer.Runtime.Bean> GetBean(System.Int32 key) { }
        // RVA: 0x038C20D0  token: 0x6000083
        public System.Boolean TryGetBean(System.Int32 key, System.Nullable<Beyond.SparkBuffer.Runtime.Bean>& bean) { }
        // RVA: 0x0B59CFE4  token: 0x6000084
        public System.Boolean ContainsKey(System.Int64 key) { }
        // RVA: 0x0B59D0E8  token: 0x6000085
        public System.Boolean GetBool(System.Int64 key) { }
        // RVA: 0x0B59DE00  token: 0x6000086
        public System.Boolean TryGetBool(System.Int64 key, System.Boolean& value) { }
        // RVA: 0x0B59D234  token: 0x6000087
        public System.Byte GetByte(System.Int64 key) { }
        // RVA: 0x0B59DED8  token: 0x6000088
        public System.Boolean TryGetByte(System.Int64 key, System.Byte& value) { }
        // RVA: 0x0B59D3C8  token: 0x6000089
        public System.Int32 GetInt(System.Int64 key) { }
        // RVA: 0x0B59DFF8  token: 0x600008A
        public System.Boolean TryGetInt(System.Int64 key, System.Int32& value) { }
        // RVA: 0x0B59D974  token: 0x600008B
        public System.Int64 GetLong(System.Int64 key) { }
        // RVA: 0x0B59E1A8  token: 0x600008C
        public System.Boolean TryGetLong(System.Int64 key, System.Int64& value) { }
        // RVA: 0x0B59D554  token: 0x600008D
        public System.Single GetFloat(System.Int64 key) { }
        // RVA: 0x0B59E118  token: 0x600008E
        public System.Boolean TryGetFloat(System.Int64 key, System.Single& value) { }
        // RVA: 0x0B59D318  token: 0x600008F
        public System.Double GetDouble(System.Int64 key) { }
        // RVA: 0x0B59DF68  token: 0x6000090
        public System.Boolean TryGetDouble(System.Int64 key, System.Double& value) { }
        // RVA: 0x0B59DC6C  token: 0x6000091
        public System.String GetString(System.Int64 key) { }
        // RVA: 0x039CE830  token: 0x6000092
        public System.Boolean TryGetString(System.Int64 key, System.String& value) { }
        // RVA: 0x0B59D3C8  token: 0x6000093
        public System.Int32 GetEnum(System.Int64 key) { }
        // RVA: 0x0B59DFF8  token: 0x6000094
        public System.Boolean TryGetEnum(System.Int64 key, System.Int32& value) { }
        // RVA: 0x0B59D038  token: 0x6000095
        public System.Nullable<Beyond.SparkBuffer.Runtime.Bean> GetBean(System.Int64 key) { }
        // RVA: 0x039CE7F0  token: 0x6000096
        public System.Boolean TryGetBean(System.Int64 key, System.Nullable<Beyond.SparkBuffer.Runtime.Bean>& bean) { }
        // RVA: 0x0B59D0A0  token: 0x6000097
        private System.Boolean GetBoolByIndex(System.Int32 index) { }
        // RVA: 0x0B59D184  token: 0x6000098
        private System.Byte GetByteByIndex(System.Int32 index) { }
        // RVA: 0x0B59D34C  token: 0x6000099
        private System.Int32 GetIntByIndex(System.Int32 index) { }
        // RVA: 0x0B59D830  token: 0x600009A
        private System.Int64 GetLongByIndex(System.Int32 index) { }
        // RVA: 0x03E31D60  token: 0x600009B
        private System.Single GetFloatByIndex(System.Int32 index) { }
        // RVA: 0x0B59D268  token: 0x600009C
        private System.Double GetDoubleByIndex(System.Int32 index) { }
        // RVA: 0x036A33B0  token: 0x600009D
        private System.String GetStringByIndex(System.Int32 index) { }
        // RVA: 0x0B59D34C  token: 0x600009E
        private System.Int32 GetEnumByIndex(System.Int32 index) { }
        // RVA: 0x038C2650  token: 0x600009F
        private System.Nullable<Beyond.SparkBuffer.Runtime.Bean> GetBeanByIndex(System.Int32 index) { }
        // RVA: 0x0B59E524  token: 0x60000A0
        private System.Int32 _GetOffsetByIndex(System.Int32 index) { }
        // RVA: 0x0B59D0CC  token: 0x60000A1
        private System.Boolean GetBoolByOffset(System.Int32 offset) { }
        // RVA: 0x0B59D1B0  token: 0x60000A2
        private System.Byte GetByteByOffset(System.Int32 offset) { }
        // RVA: 0x0B59D378  token: 0x60000A3
        private System.Int32 GetIntByOffset(System.Int32 offset) { }
        // RVA: 0x0B59D85C  token: 0x60000A4
        private System.Int64 GetLongByOffset(System.Int32 offset) { }
        // RVA: 0x0B59D4D0  token: 0x60000A5
        private System.Single GetFloatByOffset(System.Int32 offset) { }
        // RVA: 0x0B59D294  token: 0x60000A6
        private System.Double GetDoubleByOffset(System.Int32 offset) { }
        // RVA: 0x0B59DA10  token: 0x60000A7
        private System.String GetStringByOffset(System.Int32 offset) { }
        // RVA: 0x0B59D378  token: 0x60000A8
        private System.Int32 GetEnumByOffset(System.Int32 offset) { }
        // RVA: 0x0B59CFF8  token: 0x60000A9
        private System.Nullable<Beyond.SparkBuffer.Runtime.Bean> GetBeanByOffset(System.Int32 offset) { }

    }

    // TypeToken: 0x200000E  // size: 0x50
    public class Root : System.IDisposable
    {
        // Fields
        private Beyond.SparkBuffer.Runtime.ByteReader m_reader;  // 0x10
        private readonly Beyond.SparkBuffer.Runtime.Root.Options m_options;  // 0x18
        private readonly System.Int32 m_dataOffset;  // 0x40
        private System.Collections.Generic.Dictionary<System.Int32,System.String> m_cachedString;  // 0x48

        // Properties
        System.String name { get; /* RVA: 0x0385B100 */ }
        Unity.Collections.NativeArray<System.Byte> bytes { get; /* RVA: 0x03FA9AE0 */ }

        // Methods
        // RVA: 0x02F20A00  token: 0x60000B5
        private System.Void .ctor(System.Int32 dataOffset, Beyond.SparkBuffer.Runtime.Root.Options options, Beyond.SparkBuffer.Runtime.ByteReader reader) { }
        // RVA: 0x0B5A1184  token: 0x60000B6
        protected virtual System.Void Finalize() { }
        // RVA: 0x0B5A180C  token: 0x60000B7
        public virtual System.String ToString() { }
        // RVA: 0x0B5A1154  token: 0x60000B8
        public virtual System.Void Dispose() { }
        // RVA: 0x0A77DD74  token: 0x60000B9
        public System.Boolean IsValid() { }
        // RVA: 0x0B5A0EEC  token: 0x60000BA
        public System.Nullable<Beyond.SparkBuffer.Runtime.Bean> AsBean() { }
        // RVA: 0x0B5A0DC8  token: 0x60000BB
        public System.Nullable<Beyond.SparkBuffer.Runtime.Array> AsArray() { }
        // RVA: 0x0B5A0FA8  token: 0x60000BC
        public System.Nullable<Beyond.SparkBuffer.Runtime.Map> AsMap() { }
        // RVA: 0x0B5A14F4  token: 0x60000BD
        private System.Int32 GetCurReaderPosition() { }
        // RVA: 0x0B5A14B8  token: 0x60000BE
        private System.Boolean GetBool(System.Int32 offset) { }
        // RVA: 0x0B594C64  token: 0x60000BF
        private System.Byte GetByte(System.Int32 offset) { }
        // RVA: 0x0B5A1538  token: 0x60000C0
        private System.Int32 GetInt(System.Int32 offset) { }
        // RVA: 0x039CEA70  token: 0x60000C1
        private System.Int64 GetLong(System.Int32 offset) { }
        // RVA: 0x03E31E30  token: 0x60000C2
        private System.Single GetFloat(System.Int32 offset) { }
        // RVA: 0x0B5A1510  token: 0x60000C3
        private System.Double GetDouble(System.Int32 offset) { }
        // RVA: 0x0B5A1538  token: 0x60000C4
        private System.Int32 GetEnum(System.Int32 offset) { }
        // RVA: 0x0B5A1704  token: 0x60000C5
        private System.String GetString(System.Int32 offset) { }
        // RVA: 0x0B5A1394  token: 0x60000C6
        private System.Nullable<Beyond.SparkBuffer.Runtime.Bean> GetBean(System.Int32 offset, Beyond.SparkBuffer.Runtime.BeanType beanType) { }
        // RVA: 0x0B5A124C  token: 0x60000C7
        private System.Nullable<Beyond.SparkBuffer.Runtime.Array> GetArray(System.Int32 offset, Beyond.SparkBuffer.Runtime.ArrayType arrayType) { }
        // RVA: 0x0B5A1560  token: 0x60000C8
        private System.Nullable<Beyond.SparkBuffer.Runtime.Map> GetMap(System.Int32 offset, Beyond.SparkBuffer.Runtime.MapType mapType) { }
        // RVA: 0x03060940  token: 0x60000C9
        private Beyond.SparkBuffer.Runtime.Wrapper.HashSlot GetHashSlot(System.Int32 offset) { }

    }

    // TypeToken: 0x2000010  // size: 0x10
    public static class Error
    {
        // Methods
        // RVA: 0x0B596830  token: 0x60000CA
        public static System.Exception TypeNotFoundError(System.Int32 typeHash, Beyond.SparkBuffer.SparkType sparkType) { }
        // RVA: 0x0B59668C  token: 0x60000CB
        public static System.Exception InvalidTypeError(Beyond.SparkBuffer.SparkType sparkType) { }
        // RVA: 0x0B5965EC  token: 0x60000CC
        public static System.Exception InvalidMapKeyError(Beyond.SparkBuffer.SparkType sparkType) { }
        // RVA: 0x0B59672C  token: 0x60000CD
        public static System.Exception NullTypeError(Beyond.SparkBuffer.SparkType sparkType) { }
        // RVA: 0x0B596588  token: 0x60000CE
        public static System.Exception GetEndOfFile() { }
        // RVA: 0x0B5967CC  token: 0x60000CF
        public static System.Exception SeekAheadOfFile() { }
        // RVA: 0x0B596524  token: 0x60000D0
        public static System.Exception DisposedException() { }

    }

    // TypeToken: 0x2000011  // size: 0x38
    public abstract class SparkManager
    {
        // Fields
        private readonly System.Collections.Generic.Dictionary<System.Int32,Beyond.SparkBuffer.Runtime.BeanType> m_beanTypeMap;  // 0x10
        private readonly System.Collections.Generic.Dictionary<System.Int32,Beyond.SparkBuffer.Runtime.EnumType> m_enumTypeMap;  // 0x18
        protected readonly System.Collections.Generic.Dictionary<System.String,Beyond.SparkBuffer.Runtime.Root> m_namedRoots;  // 0x20
        private readonly System.Collections.Generic.List<Beyond.SparkBuffer.Runtime.Root> m_anonymousRoots;  // 0x28
        private readonly System.Collections.Generic.Queue<Beyond.SparkBuffer.Runtime.BeanType> m_beanTypeQueue;  // 0x30

        // Methods
        // RVA: 0x04A1E470  token: 0x60000D1
        protected System.Void .ctor() { }
        // RVA: 0x02F212C0  token: 0x60000D2
        public Beyond.SparkBuffer.Runtime.Root Load(System.Byte[] bytes, System.String overrideRootName) { }
        // RVA: 0x0B5A2ED0  token: 0x60000D3
        public Beyond.SparkBuffer.Runtime.Root Load(System.Byte[] bytes) { }
        // RVA: 0x0B5A29F0  token: 0x60000D4
        public Beyond.SparkBuffer.Runtime.Root LoadFromJson_EditorOnly(System.Byte[] bytes, Newtonsoft.Json.Linq.JToken jsonData, System.String overrideRootName) { }
        // RVA: 0x0B5A29D4  token: 0x60000D5
        public Beyond.SparkBuffer.Runtime.Root LoadFromJson_EditorOnly(System.Byte[] bytes, Newtonsoft.Json.Linq.JToken jsonData) { }
        // RVA: 0x0B5A3098  token: 0x60000D6
        public virtual System.Boolean Unload(Beyond.SparkBuffer.Runtime.Root root) { }
        // RVA: 0x0B5A2EDC  token: 0x60000D7
        public virtual System.Void UnloadAndClearAll() { }
        // RVA: 0x03D6EB00  token: 0x60000D8
        private System.Boolean TryGetEnumType(System.Int32 typeHash, Beyond.SparkBuffer.Runtime.EnumType& enumType) { }
        // RVA: 0x03D6EA90  token: 0x60000D9
        private System.Boolean TryGetBeanType(System.Int32 typeHash, Beyond.SparkBuffer.Runtime.BeanType& beanType) { }
        // RVA: 0x0408E180  token: 0x60000DA
        protected virtual System.Boolean _AddRoot(Beyond.SparkBuffer.Runtime.Root root) { }
        // RVA: 0x03D6E700  token: 0x60000DB
        private System.Void _ReadTypeDefs(Beyond.SparkBuffer.Runtime.ByteReader reader) { }
        // RVA: 0x02F21400  token: 0x60000DC
        private Beyond.SparkBuffer.Runtime.Root.Options _ReadRootDef(Beyond.SparkBuffer.Runtime.ByteReader reader, System.String overrideRootName) { }

    }

    // TypeToken: 0x2000014  // size: 0x50
    public class RuntimeSparkManager : Beyond.SparkBuffer.Runtime.SparkManager
    {
        // Fields
        private static Beyond.SparkBuffer.Runtime.RuntimeSparkManager s_instance;  // static @ 0x0
        private System.Action<Beyond.SparkBuffer.Runtime.Root> m_onRootLoaded;  // 0x38
        private System.Action<Beyond.SparkBuffer.Runtime.Root> m_onRootUnloaded;  // 0x40
        private System.Action m_onUnloadAll;  // 0x48
        private static System.String LUADLL;  // const

        // Properties
        Beyond.SparkBuffer.Runtime.RuntimeSparkManager instance { get; /* RVA: 0x042D25E0 */ }

        // Methods
        // RVA: 0x0B5A1A3C  token: 0x60000F4
        private System.Void .ctor() { }
        // RVA: 0x0B5A1994  token: 0x60000F5
        public virtual System.Boolean Unload(Beyond.SparkBuffer.Runtime.Root root) { }
        // RVA: 0x0B5A18C4  token: 0x60000F6
        public System.Void ReloadTableToLua(System.String name) { }
        // RVA: 0x0B5A1944  token: 0x60000F7
        public virtual System.Void UnloadAndClearAll() { }
        // RVA: 0x03FA9B10  token: 0x60000F8
        private static System.Boolean LuaSpark_LoadRoot(System.IntPtr L, System.String rootName, System.IntPtr data, System.Int32 size) { }
        // RVA: 0x0B5A1884  token: 0x60000F9
        private static System.Boolean LuaSpark_UnloadRoot(System.IntPtr L, System.String rootName) { }
        // RVA: 0x0B5A187C  token: 0x60000FA
        private static System.Void LuaSpark_UnloadAll(System.IntPtr L) { }
        // RVA: 0x04175010  token: 0x60000FB
        public System.Void RegisterToLua(System.IntPtr L) { }
        // RVA: 0x0B5A19FC  token: 0x60000FC
        public System.Void UnregisterFromLua(System.IntPtr L) { }
        // RVA: 0x0408E110  token: 0x60000FD
        protected virtual System.Boolean _AddRoot(Beyond.SparkBuffer.Runtime.Root root) { }

    }

    // TypeToken: 0x2000016
    public interface IStringPool
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000102
        public virtual System.Int32 AddString(System.String str) { }
        // RVA: -1  // abstract  token: 0x6000103
        public virtual System.String GetString(System.Int32 index) { }
        // RVA: -1  // abstract  token: 0x6000104
        public virtual System.Void Clear() { }

    }

    // TypeToken: 0x2000017  // size: 0x20
    public abstract class Field
    {
        // Fields
        private readonly System.String <name>k__BackingField;  // 0x10
        private readonly System.Int32 <offset>k__BackingField;  // 0x18

        // Properties
        System.String name { get; /* RVA: 0x02B2ECC0 */ }
        System.Int32 offset { get; /* RVA: 0x011EF5B0 */ }
        Beyond.SparkBuffer.SparkType type { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: 0x03B46FF0  token: 0x6000108
        protected System.Void .ctor(System.String fieldName, System.Int32 fieldOffset) { }
        // RVA: -1  // abstract  token: 0x6000109
        public virtual System.Void Unmarshal(Beyond.SparkBuffer.Runtime.ByteReader reader) { }
        // RVA: -1  // abstract  token: 0x600010A
        public virtual System.Void Resolve(Beyond.SparkBuffer.Runtime.SparkManager manager) { }

    }

    // TypeToken: 0x2000018  // size: 0x28
    public class SimpleField : Beyond.SparkBuffer.Runtime.Field
    {
        // Fields
        private readonly Beyond.SparkBuffer.SparkType <type>k__BackingField;  // 0x20

        // Properties
        Beyond.SparkBuffer.SparkType type { get; /* RVA: 0x04D866B0 */ }

        // Methods
        // RVA: 0x05391C28  token: 0x600010C
        public System.Void .ctor(Beyond.SparkBuffer.SparkType type, System.String fieldName, System.Int32 fieldOffset) { }
        // RVA: 0x041E1670  token: 0x600010D
        public virtual System.Void Unmarshal(Beyond.SparkBuffer.Runtime.ByteReader reader) { }
        // RVA: 0x041E1670  token: 0x600010E
        public virtual System.Void Resolve(Beyond.SparkBuffer.Runtime.SparkManager manager) { }

    }

    // TypeToken: 0x2000019  // size: 0x30
    public class EnumField : Beyond.SparkBuffer.Runtime.Field
    {
        // Fields
        private System.Int32 m_typeHash;  // 0x20
        private Beyond.SparkBuffer.Runtime.EnumType m_enumType;  // 0x28

        // Properties
        Beyond.SparkBuffer.SparkType type { get; /* RVA: 0x04DBF470 */ }
        Beyond.SparkBuffer.Runtime.EnumType enumType { get; /* RVA: 0x04D86240 */ }

        // Methods
        // RVA: 0x03B46FF0  token: 0x6000111
        public System.Void .ctor(System.String fieldName, System.Int32 fieldOffset) { }
        // RVA: 0x0403C2E0  token: 0x6000112
        public virtual System.Void Unmarshal(Beyond.SparkBuffer.Runtime.ByteReader reader) { }
        // RVA: 0x03D6EAC0  token: 0x6000113
        public virtual System.Void Resolve(Beyond.SparkBuffer.Runtime.SparkManager manager) { }

    }

    // TypeToken: 0x200001A  // size: 0x30
    public class BeanField : Beyond.SparkBuffer.Runtime.Field
    {
        // Fields
        private System.Int32 m_typeHash;  // 0x20
        private Beyond.SparkBuffer.Runtime.BeanType m_beanType;  // 0x28

        // Properties
        Beyond.SparkBuffer.SparkType type { get; /* RVA: 0x04D920C0 */ }
        Beyond.SparkBuffer.Runtime.BeanType beanType { get; /* RVA: 0x04D86240 */ }

        // Methods
        // RVA: 0x03B46FF0  token: 0x6000116
        public System.Void .ctor(System.String fieldName, System.Int32 fieldOffset) { }
        // RVA: 0x0403C310  token: 0x6000117
        public virtual System.Void Unmarshal(Beyond.SparkBuffer.Runtime.ByteReader reader) { }
        // RVA: 0x03D6EA50  token: 0x6000118
        public virtual System.Void Resolve(Beyond.SparkBuffer.Runtime.SparkManager manager) { }

    }

    // TypeToken: 0x200001B  // size: 0x40
    public class ArrayField : Beyond.SparkBuffer.Runtime.Field
    {
        // Fields
        private Beyond.SparkBuffer.Runtime.ArrayType m_arrayType;  // 0x20
        private System.Int32 m_elementTypeHash;  // 0x38

        // Properties
        Beyond.SparkBuffer.SparkType type { get; /* RVA: 0x04DBF430 */ }
        Beyond.SparkBuffer.Runtime.ArrayType arrayType { get; /* RVA: 0x04D9E6A0 */ }

        // Methods
        // RVA: 0x03B46FF0  token: 0x600011B
        public System.Void .ctor(System.String fieldName, System.Int32 fieldOffset) { }
        // RVA: 0x0403C250  token: 0x600011C
        public virtual System.Void Unmarshal(Beyond.SparkBuffer.Runtime.ByteReader reader) { }
        // RVA: 0x03D6E9E0  token: 0x600011D
        public virtual System.Void Resolve(Beyond.SparkBuffer.Runtime.SparkManager manager) { }

    }

    // TypeToken: 0x200001C  // size: 0x50
    public class MapField : Beyond.SparkBuffer.Runtime.Field
    {
        // Fields
        private Beyond.SparkBuffer.Runtime.MapType m_mapInfo;  // 0x20
        private System.Int32 m_keyTypeHash;  // 0x48
        private System.Int32 m_valueTypeHash;  // 0x4c

        // Properties
        Beyond.SparkBuffer.SparkType type { get; /* RVA: 0x04DBF480 */ }
        Beyond.SparkBuffer.Runtime.MapType mapType { get; /* RVA: 0x04D8E8C0 */ }

        // Methods
        // RVA: 0x03B46FF0  token: 0x6000120
        public System.Void .ctor(System.String fieldName, System.Int32 fieldOffset) { }
        // RVA: 0x0403C1E0  token: 0x6000121
        public virtual System.Void Unmarshal(Beyond.SparkBuffer.Runtime.ByteReader reader) { }
        // RVA: 0x03D6E980  token: 0x6000122
        public virtual System.Void Resolve(Beyond.SparkBuffer.Runtime.SparkManager manager) { }

    }

    // TypeToken: 0x200001D  // size: 0x28
    public sealed struct ArrayType
    {
        // Fields
        public Beyond.SparkBuffer.SparkType elementType;  // 0x10
        public Beyond.SparkBuffer.Runtime.BeanType elementBeanType;  // 0x18
        public Beyond.SparkBuffer.Runtime.EnumType elementEnumType;  // 0x20

    }

    // TypeToken: 0x200001E  // size: 0x38
    public sealed struct MapType
    {
        // Fields
        public Beyond.SparkBuffer.SparkType keyType;  // 0x10
        public Beyond.SparkBuffer.Runtime.EnumType keyEnumType;  // 0x18
        public Beyond.SparkBuffer.SparkType valueType;  // 0x20
        public Beyond.SparkBuffer.Runtime.BeanType valueBeanType;  // 0x28
        public Beyond.SparkBuffer.Runtime.EnumType valueEnumType;  // 0x30

    }

    // TypeToken: 0x200001F  // size: 0x28
    public class EnumType
    {
        // Fields
        private System.Collections.Generic.List<Beyond.SparkBuffer.Runtime.EnumType.EnumItem> m_enums;  // 0x10
        private System.Int32 <typeHash>k__BackingField;  // 0x18
        private System.String <name>k__BackingField;  // 0x20

        // Properties
        System.Int32 typeHash { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x011EF9B0 */ }
        System.String name { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }

        // Methods
        // RVA: 0x03292CE0  token: 0x6000127
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.ByteReader reader) { }
        // RVA: 0x0B596458  token: 0x6000128
        public System.Boolean TryGetValue(System.String enumName, System.Int32& enumVal) { }

    }

    // TypeToken: 0x2000021  // size: 0x28
    public class BeanType : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private System.Collections.Generic.Dictionary<System.String,Beyond.SparkBuffer.Runtime.Field> m_fields;  // 0x10
        private System.Int32 <typeHash>k__BackingField;  // 0x18
        private System.String <name>k__BackingField;  // 0x20

        // Properties
        System.Int32 typeHash { get; /* RVA: 0x011EF5B0 */ set; /* RVA: 0x011EF9B0 */ }
        System.String name { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }

        // Methods
        // RVA: 0x03292A70  token: 0x600012D
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.ByteReader reader) { }
        // RVA: 0x0B595110  token: 0x600012E
        public virtual System.String ToString() { }
        // RVA: 0x03D6E890  token: 0x600012F
        public System.Void Resolve(Beyond.SparkBuffer.Runtime.SparkManager manager) { }
        // RVA: 0x0305FED0  token: 0x6000130
        public System.Boolean TryGetField(System.String fieldName, Beyond.SparkBuffer.Runtime.Field& field) { }
        // RVA: 0x0B594FC8  token: 0x6000131
        public System.Collections.Generic.Dictionary.Enumerator<System.String,Beyond.SparkBuffer.Runtime.Field> GetEnumerator() { }
        // RVA: 0x0B595040  token: 0x6000132
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,Beyond.SparkBuffer.Runtime.Field>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,Beyond.SparkBuffer.Runtime.Field>>.GetEnumerator() { }
        // RVA: 0x0B5950A8  token: 0x6000133
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: 0x03292ED0  token: 0x6000134
        private Beyond.SparkBuffer.Runtime.Field _NewField(System.String name, Beyond.SparkBuffer.SparkType type, System.Int32 offset) { }

    }

}

namespace Beyond.SparkBuffer.Runtime.Wrapper
{

    // TypeToken: 0x2000022
    public interface IArrayWrapper`1
    {
        // Properties
        System.Int32 length { get; /* RVA: -1  // abstract */ }
        System.Int32 Count { get; /* RVA: -1  // abstract */ }
        T Item { get; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x2000023
    public sealed struct ArrayEnumerator`2 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private readonly TArray m_parent;  // 0x0
        private System.Int32 m_index;  // 0x0

        // Properties
        TElement Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600013A
        public System.Void .ctor(TArray parent) { }
        // RVA: -1  // not resolved  token: 0x600013B
        public virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x600013C
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x600013D
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000024
    public sealed struct NullableBeanArray`1 : Beyond.SparkBuffer.Runtime.Wrapper.IArrayWrapper`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private Beyond.SparkBuffer.Runtime.Array m_array;  // 0x0
        private System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean> m_creator;  // 0x0

        // Properties
        System.Int32 length { get; /* RVA: -1  // not resolved */ }
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        System.Nullable<TBean> Item { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000140
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Array array, System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean> creator) { }
        // RVA: -1  // not resolved  token: 0x6000142
        public Beyond.SparkBuffer.Runtime.Wrapper.ArrayEnumerator<System.Nullable<TBean>,Beyond.SparkBuffer.Runtime.Wrapper.NullableBeanArray<TBean>> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000143
        private virtual System.Collections.Generic.IEnumerator<System.Nullable<TBean>> System.Collections.Generic.IEnumerable<TBean?>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000144
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000025
    public sealed struct BeanArray`1 : Beyond.SparkBuffer.Runtime.Wrapper.IArrayWrapper`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private Beyond.SparkBuffer.Runtime.Array m_array;  // 0x0
        private System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean> m_creator;  // 0x0

        // Properties
        System.Int32 length { get; /* RVA: -1  // not resolved */ }
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        TBean Item { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000147
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Array array, System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean> creator) { }
        // RVA: -1  // not resolved  token: 0x6000149
        public Beyond.SparkBuffer.Runtime.Wrapper.ArrayEnumerator<TBean,Beyond.SparkBuffer.Runtime.Wrapper.BeanArray<TBean>> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x600014A
        private virtual System.Collections.Generic.IEnumerator<TBean> System.Collections.Generic.IEnumerable<TBean>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x600014B
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000026
    public sealed struct EnumArray`1 : Beyond.SparkBuffer.Runtime.Wrapper.IArrayWrapper`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private Beyond.SparkBuffer.Runtime.Array m_array;  // 0x0
        private System.Func<System.Int32,TEnum> m_converter;  // 0x0

        // Properties
        System.Int32 length { get; /* RVA: -1  // not resolved */ }
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        TEnum Item { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600014E
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Array array, System.Func<System.Int32,TEnum> converter) { }
        // RVA: -1  // not resolved  token: 0x6000150
        public Beyond.SparkBuffer.Runtime.Wrapper.ArrayEnumerator<TEnum,Beyond.SparkBuffer.Runtime.Wrapper.EnumArray<TEnum>> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000151
        public TEnum[] ToArray() { }
        // RVA: -1  // not resolved  token: 0x6000152
        public System.Boolean Contains(TEnum item) { }
        // RVA: -1  // not resolved  token: 0x6000153
        private virtual System.Collections.Generic.IEnumerator<TEnum> System.Collections.Generic.IEnumerable<TEnum>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000154
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000027  // size: 0x38
    public sealed struct BoolArray : Beyond.SparkBuffer.Runtime.Wrapper.IArrayWrapper`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private Beyond.SparkBuffer.Runtime.Array m_array;  // 0x10

        // Properties
        System.Int32 length { get; /* RVA: 0x011F0020 */ }
        System.Int32 Count { get; /* RVA: 0x011F0020 */ }
        System.Boolean Item { get; /* RVA: 0x0B5957E8 */ }

        // Methods
        // RVA: 0x0B5957C4  token: 0x6000157
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Array array) { }
        // RVA: 0x0B5955F0  token: 0x6000159
        public Beyond.SparkBuffer.Runtime.Wrapper.ArrayEnumerator<System.Boolean,Beyond.SparkBuffer.Runtime.Wrapper.BoolArray> GetEnumerator() { }
        // RVA: 0x0B59573C  token: 0x600015A
        public System.Boolean[] ToArray() { }
        // RVA: 0x0B5955A8  token: 0x600015B
        public System.Boolean Contains(System.Boolean item) { }
        // RVA: 0x0B595664  token: 0x600015C
        private virtual System.Collections.Generic.IEnumerator<System.Boolean> System.Collections.Generic.IEnumerable<System.Boolean>.GetEnumerator() { }
        // RVA: 0x0B5956D0  token: 0x600015D
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000028  // size: 0x38
    public sealed struct ByteArray : Beyond.SparkBuffer.Runtime.Wrapper.IArrayWrapper`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private Beyond.SparkBuffer.Runtime.Array m_array;  // 0x10

        // Properties
        System.Int32 length { get; /* RVA: 0x011F0020 */ }
        System.Int32 Count { get; /* RVA: 0x011F0020 */ }
        System.Byte Item { get; /* RVA: 0x0B595A0C */ }

        // Methods
        // RVA: 0x0B5957C4  token: 0x6000160
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Array array) { }
        // RVA: 0x0B595838  token: 0x6000162
        public Beyond.SparkBuffer.Runtime.Wrapper.ArrayEnumerator<System.Byte,Beyond.SparkBuffer.Runtime.Wrapper.ByteArray> GetEnumerator() { }
        // RVA: 0x0B595984  token: 0x6000163
        public System.Byte[] ToArray() { }
        // RVA: 0x0B5957F0  token: 0x6000164
        public System.Boolean Contains(System.Byte item) { }
        // RVA: 0x0B5958AC  token: 0x6000165
        private virtual System.Collections.Generic.IEnumerator<System.Byte> System.Collections.Generic.IEnumerable<System.Byte>.GetEnumerator() { }
        // RVA: 0x0B595918  token: 0x6000166
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000029  // size: 0x38
    public sealed struct IntArray : Beyond.SparkBuffer.Runtime.Wrapper.IArrayWrapper`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private Beyond.SparkBuffer.Runtime.Array m_array;  // 0x10

        // Properties
        System.Int32 length { get; /* RVA: 0x011F0020 */ }
        System.Int32 Count { get; /* RVA: 0x011F0020 */ }
        System.Int32 Item { get; /* RVA: 0x0539316C */ }

        // Methods
        // RVA: 0x0B5957C4  token: 0x6000169
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Array array) { }
        // RVA: 0x046AB220  token: 0x600016B
        public Beyond.SparkBuffer.Runtime.Wrapper.ArrayEnumerator<System.Int32,Beyond.SparkBuffer.Runtime.Wrapper.IntArray> GetEnumerator() { }
        // RVA: 0x0B5979DC  token: 0x600016C
        public System.Int32[] ToArray() { }
        // RVA: 0x0B597928  token: 0x600016D
        public System.Boolean Contains(System.Int32 item) { }
        // RVA: 0x046AB1B0  token: 0x600016E
        private virtual System.Collections.Generic.IEnumerator<System.Int32> System.Collections.Generic.IEnumerable<System.Int32>.GetEnumerator() { }
        // RVA: 0x0B597970  token: 0x600016F
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200002A  // size: 0x38
    public sealed struct LongArray : Beyond.SparkBuffer.Runtime.Wrapper.IArrayWrapper`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private Beyond.SparkBuffer.Runtime.Array m_array;  // 0x10

        // Properties
        System.Int32 length { get; /* RVA: 0x011F0020 */ }
        System.Int32 Count { get; /* RVA: 0x011F0020 */ }
        System.Int64 Item { get; /* RVA: 0x0B59CFDC */ }

        // Methods
        // RVA: 0x0B5957C4  token: 0x6000172
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Array array) { }
        // RVA: 0x0B59CE04  token: 0x6000174
        public Beyond.SparkBuffer.Runtime.Wrapper.ArrayEnumerator<System.Int64,Beyond.SparkBuffer.Runtime.Wrapper.LongArray> GetEnumerator() { }
        // RVA: 0x0B59CF50  token: 0x6000175
        public System.Int64[] ToArray() { }
        // RVA: 0x0B59CDBC  token: 0x6000176
        public System.Boolean Contains(System.Int64 item) { }
        // RVA: 0x0B59CE78  token: 0x6000177
        private virtual System.Collections.Generic.IEnumerator<System.Int64> System.Collections.Generic.IEnumerable<System.Int64>.GetEnumerator() { }
        // RVA: 0x0B59CEE4  token: 0x6000178
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200002B  // size: 0x38
    public sealed struct FloatArray : Beyond.SparkBuffer.Runtime.Wrapper.IArrayWrapper`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private Beyond.SparkBuffer.Runtime.Array m_array;  // 0x10

        // Properties
        System.Int32 length { get; /* RVA: 0x011F0020 */ }
        System.Int32 Count { get; /* RVA: 0x011F0020 */ }
        System.Single Item { get; /* RVA: 0x05397784 */ }

        // Methods
        // RVA: 0x0B5957C4  token: 0x600017B
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Array array) { }
        // RVA: 0x045FA690  token: 0x600017D
        public Beyond.SparkBuffer.Runtime.Wrapper.ArrayEnumerator<System.Single,Beyond.SparkBuffer.Runtime.Wrapper.FloatArray> GetEnumerator() { }
        // RVA: 0x0B596988  token: 0x600017E
        public System.Single[] ToArray() { }
        // RVA: 0x045FA620  token: 0x600017F
        private virtual System.Collections.Generic.IEnumerator<System.Single> System.Collections.Generic.IEnumerable<System.Single>.GetEnumerator() { }
        // RVA: 0x0B59691C  token: 0x6000180
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200002C  // size: 0x38
    public sealed struct DoubleArray : Beyond.SparkBuffer.Runtime.Wrapper.IArrayWrapper`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private Beyond.SparkBuffer.Runtime.Array m_array;  // 0x10

        // Properties
        System.Int32 length { get; /* RVA: 0x011F0020 */ }
        System.Int32 Count { get; /* RVA: 0x011F0020 */ }
        System.Double Item { get; /* RVA: 0x053938D4 */ }

        // Methods
        // RVA: 0x0B5957C4  token: 0x6000183
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Array array) { }
        // RVA: 0x0B596280  token: 0x6000185
        public Beyond.SparkBuffer.Runtime.Wrapper.ArrayEnumerator<System.Double,Beyond.SparkBuffer.Runtime.Wrapper.DoubleArray> GetEnumerator() { }
        // RVA: 0x0B5963CC  token: 0x6000186
        public System.Double[] ToArray() { }
        // RVA: 0x0B5962F4  token: 0x6000187
        private virtual System.Collections.Generic.IEnumerator<System.Double> System.Collections.Generic.IEnumerable<System.Double>.GetEnumerator() { }
        // RVA: 0x0B596360  token: 0x6000188
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200002D  // size: 0x38
    public sealed struct StringArray : Beyond.SparkBuffer.Runtime.Wrapper.IArrayWrapper`1, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private Beyond.SparkBuffer.Runtime.Array m_array;  // 0x10

        // Properties
        System.Int32 length { get; /* RVA: 0x011F0020 */ }
        System.Int32 Count { get; /* RVA: 0x011F0020 */ }
        System.String Item { get; /* RVA: 0x0539378C */ }

        // Methods
        // RVA: 0x0B5957C4  token: 0x600018B
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Array array) { }
        // RVA: 0x032F23B0  token: 0x600018D
        public Beyond.SparkBuffer.Runtime.Wrapper.ArrayEnumerator<System.String,Beyond.SparkBuffer.Runtime.Wrapper.StringArray> GetEnumerator() { }
        // RVA: 0x03756AF0  token: 0x600018E
        public System.String[] ToArray() { }
        // RVA: 0x03756B80  token: 0x600018F
        public System.Boolean Contains(System.String item) { }
        // RVA: 0x046D98E0  token: 0x6000190
        private virtual System.Collections.Generic.IEnumerator<System.String> System.Collections.Generic.IEnumerable<System.String>.GetEnumerator() { }
        // RVA: 0x0B5A6954  token: 0x6000191
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200002E
    public interface IBeanWrapper
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000192
        public virtual System.Void SetBean(Beyond.SparkBuffer.Runtime.Bean& bean) { }

    }

    // TypeToken: 0x200002F  // size: 0x18
    public sealed struct HashSlot
    {
        // Fields
        public System.Int32 offset;  // 0x10
        public System.Int32 bucketsSize;  // 0x14
        public static readonly System.Int32 size;  // static @ 0x0

        // Methods
        // RVA: 0x04D7A490  token: 0x6000193
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000030
    public interface IMapWrapper`3
    {
        // Properties
        System.Int32 count { get; /* RVA: -1  // abstract */ }
        System.Int32 Count { get; /* RVA: -1  // abstract */ }
        TValue Item { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000197
        public virtual System.Boolean ContainsKey(TKey key) { }
        // RVA: -1  // abstract  token: 0x6000198
        public virtual TValue GetValue(TKey key) { }
        // RVA: -1  // abstract  token: 0x6000199
        public virtual System.Boolean TryGetValue(TKey key, TValue& value) { }
        // RVA: -1  // abstract  token: 0x600019A
        public virtual TKey GetKeyByIndex(System.Int32 index) { }
        // RVA: -1  // abstract  token: 0x600019B
        public virtual TValue GetValueByIndex(System.Int32 index) { }
        // RVA: -1  // abstract  token: 0x600019C
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<TKey,TValue,TMap> GetEnumerator() { }

    }

    // TypeToken: 0x2000031
    public sealed struct MapEnumerator`3 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private readonly TMap m_parent;  // 0x0
        private System.Int32 m_index;  // 0x0

        // Properties
        System.Collections.Generic.KeyValuePair<TKey,TValue> Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600019F
        public System.Void .ctor(TMap parent) { }
        // RVA: -1  // not resolved  token: 0x60001A0
        public virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x60001A1
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x60001A2
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000032
    public sealed struct S2NullableBeanMap`1 : Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_map;  // 0x0
        private readonly System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean> m_creator;  // 0x0

        // Properties
        System.Int32 count { get; /* RVA: -1  // not resolved */ }
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        System.Nullable<TBean> Item { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60001A6
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map, System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean> creator) { }
        // RVA: -1  // not resolved  token: 0x60001A7
        public virtual System.Boolean ContainsKey(System.String key) { }
        // RVA: -1  // not resolved  token: 0x60001A8
        public virtual System.String GetKeyByIndex(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x60001A9
        public virtual System.Nullable<TBean> GetValue(System.String key) { }
        // RVA: -1  // not resolved  token: 0x60001AA
        public virtual System.Boolean TryGetValue(System.String key, System.Nullable<TBean>& value) { }
        // RVA: -1  // not resolved  token: 0x60001AB
        public virtual System.Nullable<TBean> GetValueByIndex(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x60001AC
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.String,System.Nullable<TBean>,Beyond.SparkBuffer.Runtime.Wrapper.S2NullableBeanMap<TBean>> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x60001AD
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,System.Nullable<TBean>>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,TBean?>>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x60001AE
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000033
    public sealed struct S2BeanMap`1 : Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_map;  // 0x0
        private readonly System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean> m_creator;  // 0x0

        // Properties
        System.Int32 count { get; /* RVA: -1  // not resolved */ }
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        TBean Item { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60001B2
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map, System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean> creator) { }
        // RVA: -1  // not resolved  token: 0x60001B3
        public virtual System.Boolean ContainsKey(System.String key) { }
        // RVA: -1  // not resolved  token: 0x60001B4
        public virtual System.String GetKeyByIndex(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x60001B5
        public virtual TBean GetValue(System.String key) { }
        // RVA: -1  // not resolved  token: 0x60001B6
        public virtual System.Boolean TryGetValue(System.String key, TBean& value) { }
        // RVA: -1  // not resolved  token: 0x60001B7
        public virtual TBean GetValueByIndex(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x60001B8
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.String,TBean,Beyond.SparkBuffer.Runtime.Wrapper.S2BeanMap<TBean>> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x60001B9
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,TBean>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,TBean>>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x60001BA
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000034
    public sealed struct S2EnumMap`1 : Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_map;  // 0x0
        private System.Func<System.Int32,TEnum> m_converter;  // 0x0

        // Properties
        System.Int32 count { get; /* RVA: -1  // not resolved */ }
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        TEnum Item { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60001BE
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map, System.Func<System.Int32,TEnum> converter) { }
        // RVA: -1  // not resolved  token: 0x60001BF
        public virtual System.Boolean ContainsKey(System.String key) { }
        // RVA: -1  // not resolved  token: 0x60001C0
        public virtual System.String GetKeyByIndex(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x60001C1
        public virtual TEnum GetValue(System.String key) { }
        // RVA: -1  // not resolved  token: 0x60001C2
        public virtual System.Boolean TryGetValue(System.String key, TEnum& value) { }
        // RVA: -1  // not resolved  token: 0x60001C3
        public virtual TEnum GetValueByIndex(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x60001C4
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.String,TEnum,Beyond.SparkBuffer.Runtime.Wrapper.S2EnumMap<TEnum>> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x60001C5
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,TEnum>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,TEnum>>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x60001C6
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000035  // size: 0x58
    public sealed struct S2BoolMap : Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_map;  // 0x10

        // Properties
        System.Int32 count { get; /* RVA: 0x02BB2A50 */ }
        System.Int32 Count { get; /* RVA: 0x02BB2A50 */ }
        System.Boolean Item { get; /* RVA: 0x0B5A1B1C */ }

        // Methods
        // RVA: 0x09DC2B6C  token: 0x60001CA
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map) { }
        // RVA: 0x03290380  token: 0x60001CB
        public virtual System.Boolean ContainsKey(System.String key) { }
        // RVA: 0x04240900  token: 0x60001CC
        public virtual System.String GetKeyByIndex(System.Int32 index) { }
        // RVA: 0x0B5A1B1C  token: 0x60001CD
        public virtual System.Boolean GetValue(System.String key) { }
        // RVA: 0x03753440  token: 0x60001CE
        public virtual System.Boolean TryGetValue(System.String key, System.Boolean& value) { }
        // RVA: 0x0B596AE4  token: 0x60001CF
        public virtual System.Boolean GetValueByIndex(System.Int32 index) { }
        // RVA: 0x0B5A1A44  token: 0x60001D0
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.String,System.Boolean,Beyond.SparkBuffer.Runtime.Wrapper.S2BoolMap> GetEnumerator() { }
        // RVA: 0x0B5A1B60  token: 0x60001D1
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,System.Boolean>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,System.Boolean>>.GetEnumerator() { }
        // RVA: 0x0B5A1BE0  token: 0x60001D2
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000036  // size: 0x58
    public sealed struct S2ByteMap : Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_map;  // 0x10

        // Properties
        System.Int32 count { get; /* RVA: 0x02BB2A50 */ }
        System.Int32 Count { get; /* RVA: 0x02BB2A50 */ }
        System.Byte Item { get; /* RVA: 0x0B5A1D38 */ }

        // Methods
        // RVA: 0x09DC2B6C  token: 0x60001D6
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map) { }
        // RVA: 0x03290380  token: 0x60001D7
        public virtual System.Boolean ContainsKey(System.String key) { }
        // RVA: 0x04240900  token: 0x60001D8
        public virtual System.String GetKeyByIndex(System.Int32 index) { }
        // RVA: 0x0B5A1D38  token: 0x60001D9
        public virtual System.Byte GetValue(System.String key) { }
        // RVA: 0x0B5A1E7C  token: 0x60001DA
        public virtual System.Boolean TryGetValue(System.String key, System.Byte& value) { }
        // RVA: 0x0B596D80  token: 0x60001DB
        public virtual System.Byte GetValueByIndex(System.Int32 index) { }
        // RVA: 0x0B5A1C60  token: 0x60001DC
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.String,System.Byte,Beyond.SparkBuffer.Runtime.Wrapper.S2ByteMap> GetEnumerator() { }
        // RVA: 0x0B5A1D7C  token: 0x60001DD
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,System.Byte>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,System.Byte>>.GetEnumerator() { }
        // RVA: 0x0B5A1DFC  token: 0x60001DE
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000037  // size: 0x58
    public sealed struct S2IntMap : Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_map;  // 0x10

        // Properties
        System.Int32 count { get; /* RVA: 0x02BB2A50 */ }
        System.Int32 Count { get; /* RVA: 0x02BB2A50 */ }
        System.Int32 Item { get; /* RVA: 0x03BAFD90 */ }

        // Methods
        // RVA: 0x09DC2B6C  token: 0x60001E2
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map) { }
        // RVA: 0x03290380  token: 0x60001E3
        public virtual System.Boolean ContainsKey(System.String key) { }
        // RVA: 0x04240900  token: 0x60001E4
        public virtual System.String GetKeyByIndex(System.Int32 index) { }
        // RVA: 0x03BAFD90  token: 0x60001E5
        public virtual System.Int32 GetValue(System.String key) { }
        // RVA: 0x0353C290  token: 0x60001E6
        public virtual System.Boolean TryGetValue(System.String key, System.Int32& value) { }
        // RVA: 0x0B5973C0  token: 0x60001E7
        public virtual System.Int32 GetValueByIndex(System.Int32 index) { }
        // RVA: 0x0B5A2380  token: 0x60001E8
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.String,System.Int32,Beyond.SparkBuffer.Runtime.Wrapper.S2IntMap> GetEnumerator() { }
        // RVA: 0x0B5A2458  token: 0x60001E9
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,System.Int32>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,System.Int32>>.GetEnumerator() { }
        // RVA: 0x0B5A24D8  token: 0x60001EA
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000038  // size: 0x58
    public sealed struct S2LongMap : Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_map;  // 0x10

        // Properties
        System.Int32 count { get; /* RVA: 0x02BB2A50 */ }
        System.Int32 Count { get; /* RVA: 0x02BB2A50 */ }
        System.Int64 Item { get; /* RVA: 0x0B5A2630 */ }

        // Methods
        // RVA: 0x09DC2B6C  token: 0x60001EE
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map) { }
        // RVA: 0x03290380  token: 0x60001EF
        public virtual System.Boolean ContainsKey(System.String key) { }
        // RVA: 0x04240900  token: 0x60001F0
        public virtual System.String GetKeyByIndex(System.Int32 index) { }
        // RVA: 0x0B5A2630  token: 0x60001F1
        public virtual System.Int64 GetValue(System.String key) { }
        // RVA: 0x0B5A2774  token: 0x60001F2
        public virtual System.Boolean TryGetValue(System.String key, System.Int64& value) { }
        // RVA: 0x0B59765C  token: 0x60001F3
        public virtual System.Int64 GetValueByIndex(System.Int32 index) { }
        // RVA: 0x0B5A2558  token: 0x60001F4
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.String,System.Int64,Beyond.SparkBuffer.Runtime.Wrapper.S2LongMap> GetEnumerator() { }
        // RVA: 0x0B5A2674  token: 0x60001F5
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,System.Int64>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,System.Int64>>.GetEnumerator() { }
        // RVA: 0x0B5A26F4  token: 0x60001F6
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000039  // size: 0x58
    public sealed struct S2FloatMap : Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_map;  // 0x10

        // Properties
        System.Int32 count { get; /* RVA: 0x02BB2A50 */ }
        System.Int32 Count { get; /* RVA: 0x02BB2A50 */ }
        System.Single Item { get; /* RVA: 0x0B5A21F8 */ }

        // Methods
        // RVA: 0x09DC2B6C  token: 0x60001FA
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map) { }
        // RVA: 0x03290380  token: 0x60001FB
        public virtual System.Boolean ContainsKey(System.String key) { }
        // RVA: 0x04240900  token: 0x60001FC
        public virtual System.String GetKeyByIndex(System.Int32 index) { }
        // RVA: 0x0B5A21F8  token: 0x60001FD
        public virtual System.Single GetValue(System.String key) { }
        // RVA: 0x0B5A233C  token: 0x60001FE
        public virtual System.Boolean TryGetValue(System.String key, System.Single& value) { }
        // RVA: 0x0B59C568  token: 0x60001FF
        public virtual System.Single GetValueByIndex(System.Int32 index) { }
        // RVA: 0x0B5A2120  token: 0x6000200
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.String,System.Single,Beyond.SparkBuffer.Runtime.Wrapper.S2FloatMap> GetEnumerator() { }
        // RVA: 0x0B5A223C  token: 0x6000201
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,System.Single>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,System.Single>>.GetEnumerator() { }
        // RVA: 0x0B5A22BC  token: 0x6000202
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200003A  // size: 0x58
    public sealed struct S2DoubleMap : Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_map;  // 0x10

        // Properties
        System.Int32 count { get; /* RVA: 0x02BB2A50 */ }
        System.Int32 Count { get; /* RVA: 0x02BB2A50 */ }
        System.Double Item { get; /* RVA: 0x0B5A1F98 */ }

        // Methods
        // RVA: 0x09DC2B6C  token: 0x6000206
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map) { }
        // RVA: 0x03290380  token: 0x6000207
        public virtual System.Boolean ContainsKey(System.String key) { }
        // RVA: 0x04240900  token: 0x6000208
        public virtual System.String GetKeyByIndex(System.Int32 index) { }
        // RVA: 0x0B5A1F98  token: 0x6000209
        public virtual System.Double GetValue(System.String key) { }
        // RVA: 0x0B5A20DC  token: 0x600020A
        public virtual System.Boolean TryGetValue(System.String key, System.Double& value) { }
        // RVA: 0x0B59701C  token: 0x600020B
        public virtual System.Double GetValueByIndex(System.Int32 index) { }
        // RVA: 0x0B5A1EC0  token: 0x600020C
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.String,System.Double,Beyond.SparkBuffer.Runtime.Wrapper.S2DoubleMap> GetEnumerator() { }
        // RVA: 0x0B5A1FDC  token: 0x600020D
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,System.Double>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,System.Double>>.GetEnumerator() { }
        // RVA: 0x0B5A205C  token: 0x600020E
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200003B  // size: 0x58
    public sealed struct S2StringMap : Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_map;  // 0x10

        // Properties
        System.Int32 count { get; /* RVA: 0x02BB2A50 */ }
        System.Int32 Count { get; /* RVA: 0x02BB2A50 */ }
        System.String Item { get; /* RVA: 0x0B5A2890 */ }

        // Methods
        // RVA: 0x09DC2B6C  token: 0x6000212
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map) { }
        // RVA: 0x03290380  token: 0x6000213
        public virtual System.Boolean ContainsKey(System.String key) { }
        // RVA: 0x04240900  token: 0x6000214
        public virtual System.String GetKeyByIndex(System.Int32 index) { }
        // RVA: 0x0B5A2890  token: 0x6000215
        public virtual System.String GetValue(System.String key) { }
        // RVA: 0x041EEFC0  token: 0x6000216
        public virtual System.Boolean TryGetValue(System.String key, System.String& value) { }
        // RVA: 0x0B59CC34  token: 0x6000217
        public virtual System.String GetValueByIndex(System.Int32 index) { }
        // RVA: 0x0B5A27B8  token: 0x6000218
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.String,System.String,Beyond.SparkBuffer.Runtime.Wrapper.S2StringMap> GetEnumerator() { }
        // RVA: 0x0B5A28D4  token: 0x6000219
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,System.String>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,System.String>>.GetEnumerator() { }
        // RVA: 0x0B5A2954  token: 0x600021A
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200003C
    public sealed struct I2NullableBeanMap`1 : Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_map;  // 0x0
        private readonly System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean> m_creator;  // 0x0

        // Properties
        System.Int32 count { get; /* RVA: -1  // not resolved */ }
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        System.Nullable<TBean> Item { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600021E
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map, System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean> creator) { }
        // RVA: -1  // not resolved  token: 0x600021F
        public virtual System.Boolean ContainsKey(System.Int32 key) { }
        // RVA: -1  // not resolved  token: 0x6000220
        public virtual System.Int32 GetKeyByIndex(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x6000221
        public virtual System.Nullable<TBean> GetValue(System.Int32 key) { }
        // RVA: -1  // not resolved  token: 0x6000222
        public virtual System.Boolean TryGetValue(System.Int32 key, System.Nullable<TBean>& value) { }
        // RVA: -1  // not resolved  token: 0x6000223
        public virtual System.Nullable<TBean> GetValueByIndex(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x6000224
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int32,System.Nullable<TBean>,Beyond.SparkBuffer.Runtime.Wrapper.I2NullableBeanMap<TBean>> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000225
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int32,System.Nullable<TBean>>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int32,TBean?>>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000226
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200003D
    public sealed struct I2BeanMap`1 : Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_map;  // 0x0
        private readonly System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean> m_creator;  // 0x0

        // Properties
        System.Int32 count { get; /* RVA: -1  // not resolved */ }
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        TBean Item { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600022A
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map, System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean> creator) { }
        // RVA: -1  // not resolved  token: 0x600022B
        public virtual System.Boolean ContainsKey(System.Int32 key) { }
        // RVA: -1  // not resolved  token: 0x600022C
        public virtual System.Int32 GetKeyByIndex(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x600022D
        public virtual TBean GetValue(System.Int32 key) { }
        // RVA: -1  // not resolved  token: 0x600022E
        public virtual System.Boolean TryGetValue(System.Int32 key, TBean& value) { }
        // RVA: -1  // not resolved  token: 0x600022F
        public virtual TBean GetValueByIndex(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x6000230
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int32,TBean,Beyond.SparkBuffer.Runtime.Wrapper.I2BeanMap<TBean>> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000231
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int32,TBean>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int32,TBean>>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000232
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200003E
    public sealed struct I2EnumMap`1 : Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_map;  // 0x0
        private System.Func<System.Int32,TEnum> m_converter;  // 0x0

        // Properties
        System.Int32 count { get; /* RVA: -1  // not resolved */ }
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        TEnum Item { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000236
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map, System.Func<System.Int32,TEnum> converter) { }
        // RVA: -1  // not resolved  token: 0x6000237
        public virtual System.Boolean ContainsKey(System.Int32 key) { }
        // RVA: -1  // not resolved  token: 0x6000238
        public virtual System.Int32 GetKeyByIndex(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x6000239
        public virtual TEnum GetValue(System.Int32 key) { }
        // RVA: -1  // not resolved  token: 0x600023A
        public virtual System.Boolean TryGetValue(System.Int32 key, TEnum& value) { }
        // RVA: -1  // not resolved  token: 0x600023B
        public virtual TEnum GetValueByIndex(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x600023C
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int32,TEnum,Beyond.SparkBuffer.Runtime.Wrapper.I2EnumMap<TEnum>> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x600023D
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int32,TEnum>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int32,TEnum>>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x600023E
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200003F  // size: 0x58
    public sealed struct I2BoolMap : Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_map;  // 0x10

        // Properties
        System.Int32 count { get; /* RVA: 0x02BB2A50 */ }
        System.Int32 Count { get; /* RVA: 0x02BB2A50 */ }
        System.Boolean Item { get; /* RVA: 0x0B596B28 */ }

        // Methods
        // RVA: 0x09DC2B6C  token: 0x6000242
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map) { }
        // RVA: 0x0856A078  token: 0x6000243
        public virtual System.Boolean ContainsKey(System.Int32 key) { }
        // RVA: 0x036A33E0  token: 0x6000244
        public virtual System.Int32 GetKeyByIndex(System.Int32 index) { }
        // RVA: 0x0B596B28  token: 0x6000245
        public virtual System.Boolean GetValue(System.Int32 key) { }
        // RVA: 0x0B596C6C  token: 0x6000246
        public virtual System.Boolean TryGetValue(System.Int32 key, System.Boolean& value) { }
        // RVA: 0x0B596AE4  token: 0x6000247
        public virtual System.Boolean GetValueByIndex(System.Int32 index) { }
        // RVA: 0x0B596A14  token: 0x6000248
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int32,System.Boolean,Beyond.SparkBuffer.Runtime.Wrapper.I2BoolMap> GetEnumerator() { }
        // RVA: 0x0B596B6C  token: 0x6000249
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int32,System.Boolean>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int32,System.Boolean>>.GetEnumerator() { }
        // RVA: 0x0B596BEC  token: 0x600024A
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000040  // size: 0x58
    public sealed struct I2ByteMap : Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_map;  // 0x10

        // Properties
        System.Int32 count { get; /* RVA: 0x02BB2A50 */ }
        System.Int32 Count { get; /* RVA: 0x02BB2A50 */ }
        System.Byte Item { get; /* RVA: 0x0B596DC4 */ }

        // Methods
        // RVA: 0x09DC2B6C  token: 0x600024E
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map) { }
        // RVA: 0x0856A078  token: 0x600024F
        public virtual System.Boolean ContainsKey(System.Int32 key) { }
        // RVA: 0x036A33E0  token: 0x6000250
        public virtual System.Int32 GetKeyByIndex(System.Int32 index) { }
        // RVA: 0x0B596DC4  token: 0x6000251
        public virtual System.Byte GetValue(System.Int32 key) { }
        // RVA: 0x0B596F08  token: 0x6000252
        public virtual System.Boolean TryGetValue(System.Int32 key, System.Byte& value) { }
        // RVA: 0x0B596D80  token: 0x6000253
        public virtual System.Byte GetValueByIndex(System.Int32 index) { }
        // RVA: 0x0B596CB0  token: 0x6000254
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int32,System.Byte,Beyond.SparkBuffer.Runtime.Wrapper.I2ByteMap> GetEnumerator() { }
        // RVA: 0x0B596E08  token: 0x6000255
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int32,System.Byte>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int32,System.Byte>>.GetEnumerator() { }
        // RVA: 0x0B596E88  token: 0x6000256
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000041  // size: 0x58
    public sealed struct I2IntMap : Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_map;  // 0x10

        // Properties
        System.Int32 count { get; /* RVA: 0x02BB2A50 */ }
        System.Int32 Count { get; /* RVA: 0x02BB2A50 */ }
        System.Int32 Item { get; /* RVA: 0x0B597404 */ }

        // Methods
        // RVA: 0x09DC2B6C  token: 0x600025A
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map) { }
        // RVA: 0x0856A078  token: 0x600025B
        public virtual System.Boolean ContainsKey(System.Int32 key) { }
        // RVA: 0x036A33E0  token: 0x600025C
        public virtual System.Int32 GetKeyByIndex(System.Int32 index) { }
        // RVA: 0x0B597404  token: 0x600025D
        public virtual System.Int32 GetValue(System.Int32 key) { }
        // RVA: 0x0B597548  token: 0x600025E
        public virtual System.Boolean TryGetValue(System.Int32 key, System.Int32& value) { }
        // RVA: 0x0B5973C0  token: 0x600025F
        public virtual System.Int32 GetValueByIndex(System.Int32 index) { }
        // RVA: 0x0B5972F0  token: 0x6000260
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int32,System.Int32,Beyond.SparkBuffer.Runtime.Wrapper.I2IntMap> GetEnumerator() { }
        // RVA: 0x0B597448  token: 0x6000261
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int32,System.Int32>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int32,System.Int32>>.GetEnumerator() { }
        // RVA: 0x0B5974C8  token: 0x6000262
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000042  // size: 0x58
    public sealed struct I2LongMap : Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_map;  // 0x10

        // Properties
        System.Int32 count { get; /* RVA: 0x02BB2A50 */ }
        System.Int32 Count { get; /* RVA: 0x02BB2A50 */ }
        System.Int64 Item { get; /* RVA: 0x0B5976A0 */ }

        // Methods
        // RVA: 0x09DC2B6C  token: 0x6000266
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map) { }
        // RVA: 0x0856A078  token: 0x6000267
        public virtual System.Boolean ContainsKey(System.Int32 key) { }
        // RVA: 0x036A33E0  token: 0x6000268
        public virtual System.Int32 GetKeyByIndex(System.Int32 index) { }
        // RVA: 0x0B5976A0  token: 0x6000269
        public virtual System.Int64 GetValue(System.Int32 key) { }
        // RVA: 0x0B5977E4  token: 0x600026A
        public virtual System.Boolean TryGetValue(System.Int32 key, System.Int64& value) { }
        // RVA: 0x0B59765C  token: 0x600026B
        public virtual System.Int64 GetValueByIndex(System.Int32 index) { }
        // RVA: 0x0B59758C  token: 0x600026C
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int32,System.Int64,Beyond.SparkBuffer.Runtime.Wrapper.I2LongMap> GetEnumerator() { }
        // RVA: 0x0B5976E4  token: 0x600026D
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int32,System.Int64>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int32,System.Int64>>.GetEnumerator() { }
        // RVA: 0x0B597764  token: 0x600026E
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000043  // size: 0x58
    public sealed struct I2FloatMap : Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_map;  // 0x10

        // Properties
        System.Int32 count { get; /* RVA: 0x02BB2A50 */ }
        System.Int32 Count { get; /* RVA: 0x02BB2A50 */ }
        System.Single Item { get; /* RVA: 0x0B5971E8 */ }

        // Methods
        // RVA: 0x09DC2B6C  token: 0x6000272
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map) { }
        // RVA: 0x0856A078  token: 0x6000273
        public virtual System.Boolean ContainsKey(System.Int32 key) { }
        // RVA: 0x036A33E0  token: 0x6000274
        public virtual System.Int32 GetKeyByIndex(System.Int32 index) { }
        // RVA: 0x0B5971E8  token: 0x6000275
        public virtual System.Single GetValue(System.Int32 key) { }
        // RVA: 0x0B5972AC  token: 0x6000276
        public virtual System.Boolean TryGetValue(System.Int32 key, System.Single& value) { }
        // RVA: 0x03E31CF0  token: 0x6000277
        public virtual System.Single GetValueByIndex(System.Int32 index) { }
        // RVA: 0x04B880E0  token: 0x6000278
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int32,System.Single,Beyond.SparkBuffer.Runtime.Wrapper.I2FloatMap> GetEnumerator() { }
        // RVA: 0x04B88060  token: 0x6000279
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int32,System.Single>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int32,System.Single>>.GetEnumerator() { }
        // RVA: 0x0B59722C  token: 0x600027A
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000044  // size: 0x58
    public sealed struct I2DoubleMap : Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_map;  // 0x10

        // Properties
        System.Int32 count { get; /* RVA: 0x02BB2A50 */ }
        System.Int32 Count { get; /* RVA: 0x02BB2A50 */ }
        System.Double Item { get; /* RVA: 0x0B597060 */ }

        // Methods
        // RVA: 0x09DC2B6C  token: 0x600027E
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map) { }
        // RVA: 0x0856A078  token: 0x600027F
        public virtual System.Boolean ContainsKey(System.Int32 key) { }
        // RVA: 0x036A33E0  token: 0x6000280
        public virtual System.Int32 GetKeyByIndex(System.Int32 index) { }
        // RVA: 0x0B597060  token: 0x6000281
        public virtual System.Double GetValue(System.Int32 key) { }
        // RVA: 0x0B5971A4  token: 0x6000282
        public virtual System.Boolean TryGetValue(System.Int32 key, System.Double& value) { }
        // RVA: 0x0B59701C  token: 0x6000283
        public virtual System.Double GetValueByIndex(System.Int32 index) { }
        // RVA: 0x0B596F4C  token: 0x6000284
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int32,System.Double,Beyond.SparkBuffer.Runtime.Wrapper.I2DoubleMap> GetEnumerator() { }
        // RVA: 0x0B5970A4  token: 0x6000285
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int32,System.Double>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int32,System.Double>>.GetEnumerator() { }
        // RVA: 0x0B597124  token: 0x6000286
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000045  // size: 0x58
    public sealed struct I2StringMap : Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_map;  // 0x10

        // Properties
        System.Int32 count { get; /* RVA: 0x02BB2A50 */ }
        System.Int32 Count { get; /* RVA: 0x02BB2A50 */ }
        System.String Item { get; /* RVA: 0x03062660 */ }

        // Methods
        // RVA: 0x09DC2B6C  token: 0x600028A
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map) { }
        // RVA: 0x0331BB00  token: 0x600028B
        public virtual System.Boolean ContainsKey(System.Int32 key) { }
        // RVA: 0x036A33E0  token: 0x600028C
        public virtual System.Int32 GetKeyByIndex(System.Int32 index) { }
        // RVA: 0x04060770  token: 0x600028D
        public virtual System.String GetValue(System.Int32 key) { }
        // RVA: 0x0397BA10  token: 0x600028E
        public virtual System.Boolean TryGetValue(System.Int32 key, System.String& value) { }
        // RVA: 0x036A3340  token: 0x600028F
        public virtual System.String GetValueByIndex(System.Int32 index) { }
        // RVA: 0x047F9D50  token: 0x6000290
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int32,System.String,Beyond.SparkBuffer.Runtime.Wrapper.I2StringMap> GetEnumerator() { }
        // RVA: 0x0B597828  token: 0x6000291
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int32,System.String>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int32,System.String>>.GetEnumerator() { }
        // RVA: 0x0B5978A8  token: 0x6000292
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000046  // size: 0x58
    public sealed struct L2BoolMap : Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_map;  // 0x10

        // Properties
        System.Int32 count { get; /* RVA: 0x02BB2A50 */ }
        System.Int32 Count { get; /* RVA: 0x02BB2A50 */ }
        System.Boolean Item { get; /* RVA: 0x0B59BC08 */ }

        // Methods
        // RVA: 0x09DC2B6C  token: 0x6000296
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map) { }
        // RVA: 0x086D7B08  token: 0x6000297
        public virtual System.Boolean ContainsKey(System.Int64 key) { }
        // RVA: 0x086D7B4C  token: 0x6000298
        public virtual System.Int64 GetKeyByIndex(System.Int32 index) { }
        // RVA: 0x0B59BC08  token: 0x6000299
        public virtual System.Boolean GetValue(System.Int64 key) { }
        // RVA: 0x0B59BD4C  token: 0x600029A
        public virtual System.Boolean TryGetValue(System.Int64 key, System.Boolean& value) { }
        // RVA: 0x0B596AE4  token: 0x600029B
        public virtual System.Boolean GetValueByIndex(System.Int32 index) { }
        // RVA: 0x0B59BB38  token: 0x600029C
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int64,System.Boolean,Beyond.SparkBuffer.Runtime.Wrapper.L2BoolMap> GetEnumerator() { }
        // RVA: 0x0B59BC4C  token: 0x600029D
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int64,System.Boolean>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int64,System.Boolean>>.GetEnumerator() { }
        // RVA: 0x0B59BCCC  token: 0x600029E
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000047  // size: 0x58
    public sealed struct L2ByteMap : Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_map;  // 0x10

        // Properties
        System.Int32 count { get; /* RVA: 0x02BB2A50 */ }
        System.Int32 Count { get; /* RVA: 0x02BB2A50 */ }
        System.Byte Item { get; /* RVA: 0x0B59BE60 */ }

        // Methods
        // RVA: 0x09DC2B6C  token: 0x60002A2
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map) { }
        // RVA: 0x086D7B08  token: 0x60002A3
        public virtual System.Boolean ContainsKey(System.Int64 key) { }
        // RVA: 0x086D7B4C  token: 0x60002A4
        public virtual System.Int64 GetKeyByIndex(System.Int32 index) { }
        // RVA: 0x0B59BE60  token: 0x60002A5
        public virtual System.Byte GetValue(System.Int64 key) { }
        // RVA: 0x0B59BFA4  token: 0x60002A6
        public virtual System.Boolean TryGetValue(System.Int64 key, System.Byte& value) { }
        // RVA: 0x0B596D80  token: 0x60002A7
        public virtual System.Byte GetValueByIndex(System.Int32 index) { }
        // RVA: 0x0B59BD90  token: 0x60002A8
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int64,System.Byte,Beyond.SparkBuffer.Runtime.Wrapper.L2ByteMap> GetEnumerator() { }
        // RVA: 0x0B59BEA4  token: 0x60002A9
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int64,System.Byte>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int64,System.Byte>>.GetEnumerator() { }
        // RVA: 0x0B59BF24  token: 0x60002AA
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000048  // size: 0x58
    public sealed struct L2IntMap : Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_map;  // 0x10

        // Properties
        System.Int32 count { get; /* RVA: 0x02BB2A50 */ }
        System.Int32 Count { get; /* RVA: 0x02BB2A50 */ }
        System.Int32 Item { get; /* RVA: 0x0B59C310 */ }

        // Methods
        // RVA: 0x09DC2B6C  token: 0x60002AE
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map) { }
        // RVA: 0x086D7B08  token: 0x60002AF
        public virtual System.Boolean ContainsKey(System.Int64 key) { }
        // RVA: 0x086D7B4C  token: 0x60002B0
        public virtual System.Int64 GetKeyByIndex(System.Int32 index) { }
        // RVA: 0x0B59C310  token: 0x60002B1
        public virtual System.Int32 GetValue(System.Int64 key) { }
        // RVA: 0x0B59C454  token: 0x60002B2
        public virtual System.Boolean TryGetValue(System.Int64 key, System.Int32& value) { }
        // RVA: 0x0B5973C0  token: 0x60002B3
        public virtual System.Int32 GetValueByIndex(System.Int32 index) { }
        // RVA: 0x0B59C734  token: 0x60002B4
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int64,System.Int32,Beyond.SparkBuffer.Runtime.Wrapper.L2IntMap> GetEnumerator() { }
        // RVA: 0x0B59C804  token: 0x60002B5
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int64,System.Int32>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int64,System.Int32>>.GetEnumerator() { }
        // RVA: 0x0B59C884  token: 0x60002B6
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000049  // size: 0x58
    public sealed struct L2LongMap : Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_map;  // 0x10

        // Properties
        System.Int32 count { get; /* RVA: 0x02BB2A50 */ }
        System.Int32 Count { get; /* RVA: 0x02BB2A50 */ }
        System.Int64 Item { get; /* RVA: 0x0B59C9D4 */ }

        // Methods
        // RVA: 0x09DC2B6C  token: 0x60002BA
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map) { }
        // RVA: 0x086D7B08  token: 0x60002BB
        public virtual System.Boolean ContainsKey(System.Int64 key) { }
        // RVA: 0x086D7B4C  token: 0x60002BC
        public virtual System.Int64 GetKeyByIndex(System.Int32 index) { }
        // RVA: 0x0B59C9D4  token: 0x60002BD
        public virtual System.Int64 GetValue(System.Int64 key) { }
        // RVA: 0x0B59CB18  token: 0x60002BE
        public virtual System.Boolean TryGetValue(System.Int64 key, System.Int64& value) { }
        // RVA: 0x0B59765C  token: 0x60002BF
        public virtual System.Int64 GetValueByIndex(System.Int32 index) { }
        // RVA: 0x0B59C904  token: 0x60002C0
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int64,System.Int64,Beyond.SparkBuffer.Runtime.Wrapper.L2LongMap> GetEnumerator() { }
        // RVA: 0x0B59CA18  token: 0x60002C1
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int64,System.Int64>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int64,System.Int64>>.GetEnumerator() { }
        // RVA: 0x0B59CA98  token: 0x60002C2
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200004A  // size: 0x58
    public sealed struct L2FloatMap : Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_map;  // 0x10

        // Properties
        System.Int32 count { get; /* RVA: 0x02BB2A50 */ }
        System.Int32 Count { get; /* RVA: 0x02BB2A50 */ }
        System.Single Item { get; /* RVA: 0x0B59C5AC */ }

        // Methods
        // RVA: 0x09DC2B6C  token: 0x60002C6
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map) { }
        // RVA: 0x086D7B08  token: 0x60002C7
        public virtual System.Boolean ContainsKey(System.Int64 key) { }
        // RVA: 0x086D7B4C  token: 0x60002C8
        public virtual System.Int64 GetKeyByIndex(System.Int32 index) { }
        // RVA: 0x0B59C5AC  token: 0x60002C9
        public virtual System.Single GetValue(System.Int64 key) { }
        // RVA: 0x0B59C6F0  token: 0x60002CA
        public virtual System.Boolean TryGetValue(System.Int64 key, System.Single& value) { }
        // RVA: 0x0B59C568  token: 0x60002CB
        public virtual System.Single GetValueByIndex(System.Int32 index) { }
        // RVA: 0x0B59C498  token: 0x60002CC
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int64,System.Single,Beyond.SparkBuffer.Runtime.Wrapper.L2FloatMap> GetEnumerator() { }
        // RVA: 0x0B59C5F0  token: 0x60002CD
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int64,System.Single>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int64,System.Single>>.GetEnumerator() { }
        // RVA: 0x0B59C670  token: 0x60002CE
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200004B  // size: 0x58
    public sealed struct L2DoubleMap : Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_map;  // 0x10

        // Properties
        System.Int32 count { get; /* RVA: 0x02BB2A50 */ }
        System.Int32 Count { get; /* RVA: 0x02BB2A50 */ }
        System.Double Item { get; /* RVA: 0x0B59C0B8 */ }

        // Methods
        // RVA: 0x09DC2B6C  token: 0x60002D2
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map) { }
        // RVA: 0x086D7B08  token: 0x60002D3
        public virtual System.Boolean ContainsKey(System.Int64 key) { }
        // RVA: 0x086D7B4C  token: 0x60002D4
        public virtual System.Int64 GetKeyByIndex(System.Int32 index) { }
        // RVA: 0x0B59C0B8  token: 0x60002D5
        public virtual System.Double GetValue(System.Int64 key) { }
        // RVA: 0x0B59C1FC  token: 0x60002D6
        public virtual System.Boolean TryGetValue(System.Int64 key, System.Double& value) { }
        // RVA: 0x0B59701C  token: 0x60002D7
        public virtual System.Double GetValueByIndex(System.Int32 index) { }
        // RVA: 0x0B59BFE8  token: 0x60002D8
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int64,System.Double,Beyond.SparkBuffer.Runtime.Wrapper.L2DoubleMap> GetEnumerator() { }
        // RVA: 0x0B59C0FC  token: 0x60002D9
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int64,System.Double>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int64,System.Double>>.GetEnumerator() { }
        // RVA: 0x0B59C17C  token: 0x60002DA
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200004C  // size: 0x58
    public sealed struct L2StringMap : Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_map;  // 0x10

        // Properties
        System.Int32 count { get; /* RVA: 0x02BB2A50 */ }
        System.Int32 Count { get; /* RVA: 0x02BB2A50 */ }
        System.String Item { get; /* RVA: 0x0B59CC78 */ }

        // Methods
        // RVA: 0x09DC2B6C  token: 0x60002DE
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map) { }
        // RVA: 0x086D7B08  token: 0x60002DF
        public virtual System.Boolean ContainsKey(System.Int64 key) { }
        // RVA: 0x086D7B4C  token: 0x60002E0
        public virtual System.Int64 GetKeyByIndex(System.Int32 index) { }
        // RVA: 0x0B59CC78  token: 0x60002E1
        public virtual System.String GetValue(System.Int64 key) { }
        // RVA: 0x039CE6C0  token: 0x60002E2
        public virtual System.Boolean TryGetValue(System.Int64 key, System.String& value) { }
        // RVA: 0x0B59CC34  token: 0x60002E3
        public virtual System.String GetValueByIndex(System.Int32 index) { }
        // RVA: 0x0B59CB5C  token: 0x60002E4
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int64,System.String,Beyond.SparkBuffer.Runtime.Wrapper.L2StringMap> GetEnumerator() { }
        // RVA: 0x0B59CCBC  token: 0x60002E5
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int64,System.String>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int64,System.String>>.GetEnumerator() { }
        // RVA: 0x0B59CD3C  token: 0x60002E6
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200004D  // size: 0x58
    public sealed struct L2EnumMap : Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_map;  // 0x10

        // Properties
        System.Int32 count { get; /* RVA: 0x02BB2A50 */ }
        System.Int32 Count { get; /* RVA: 0x02BB2A50 */ }
        System.Int32 Item { get; /* RVA: 0x0B59C310 */ }

        // Methods
        // RVA: 0x09DC2B6C  token: 0x60002EA
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map) { }
        // RVA: 0x086D7B08  token: 0x60002EB
        public virtual System.Boolean ContainsKey(System.Int64 key) { }
        // RVA: 0x086D7B4C  token: 0x60002EC
        public virtual System.Int64 GetKeyByIndex(System.Int32 index) { }
        // RVA: 0x0B59C310  token: 0x60002ED
        public virtual System.Int32 GetValue(System.Int64 key) { }
        // RVA: 0x0B59C454  token: 0x60002EE
        public virtual System.Boolean TryGetValue(System.Int64 key, System.Int32& value) { }
        // RVA: 0x0B5973C0  token: 0x60002EF
        public virtual System.Int32 GetValueByIndex(System.Int32 index) { }
        // RVA: 0x0B59C240  token: 0x60002F0
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int64,System.Int32,Beyond.SparkBuffer.Runtime.Wrapper.L2EnumMap> GetEnumerator() { }
        // RVA: 0x0B59C354  token: 0x60002F1
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int64,System.Int32>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int64,System.Int32>>.GetEnumerator() { }
        // RVA: 0x0B59C3D4  token: 0x60002F2
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200004E
    public sealed struct L2NullableBeanMap`1 : Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_map;  // 0x0
        private readonly System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean> m_creator;  // 0x0

        // Properties
        System.Int32 count { get; /* RVA: -1  // not resolved */ }
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        System.Nullable<TBean> Item { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x60002F6
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map, System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean> creator) { }
        // RVA: -1  // not resolved  token: 0x60002F7
        public virtual System.Boolean ContainsKey(System.Int64 key) { }
        // RVA: -1  // not resolved  token: 0x60002F8
        public virtual System.Int64 GetKeyByIndex(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x60002F9
        public virtual System.Nullable<TBean> GetValue(System.Int64 key) { }
        // RVA: -1  // not resolved  token: 0x60002FA
        public virtual System.Boolean TryGetValue(System.Int64 key, System.Nullable<TBean>& value) { }
        // RVA: -1  // not resolved  token: 0x60002FB
        public virtual System.Nullable<TBean> GetValueByIndex(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x60002FC
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int64,System.Nullable<TBean>,Beyond.SparkBuffer.Runtime.Wrapper.L2NullableBeanMap<TBean>> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x60002FD
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int64,System.Nullable<TBean>>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int64,TBean?>>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x60002FE
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200004F
    public sealed struct L2BeanMap`1 : Beyond.SparkBuffer.Runtime.Wrapper.IMapWrapper`3, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private readonly Beyond.SparkBuffer.Runtime.Map m_map;  // 0x0
        private readonly System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean> m_creator;  // 0x0

        // Properties
        System.Int32 count { get; /* RVA: -1  // not resolved */ }
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        TBean Item { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000302
        public System.Void .ctor(Beyond.SparkBuffer.Runtime.Map map, System.Func<Beyond.SparkBuffer.Runtime.Bean,TBean> creator) { }
        // RVA: -1  // not resolved  token: 0x6000303
        public virtual System.Boolean ContainsKey(System.Int64 key) { }
        // RVA: -1  // not resolved  token: 0x6000304
        public virtual System.Int64 GetKeyByIndex(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x6000305
        public virtual TBean GetValue(System.Int64 key) { }
        // RVA: -1  // not resolved  token: 0x6000306
        public virtual System.Boolean TryGetValue(System.Int64 key, TBean& value) { }
        // RVA: -1  // not resolved  token: 0x6000307
        public virtual TBean GetValueByIndex(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x6000308
        public virtual Beyond.SparkBuffer.Runtime.Wrapper.MapEnumerator<System.Int64,TBean,Beyond.SparkBuffer.Runtime.Wrapper.L2BeanMap<TBean>> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000309
        private virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.Int64,TBean>> System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.Int64,TBean>>.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x600030A
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

}

namespace Beyond.SparkBuffer.Serialize
{

    // TypeToken: 0x2000050  // size: 0x10
    public static class Extensions
    {
        // Methods
        // RVA: -1  // generic def  token: 0x600030B
        public static System.Boolean Contains(System.Collections.Generic.IList<T> list, T element) { }
        // RVA: -1  // generic def  token: 0x600030C
        public static System.Boolean IsNullOrEmpty(System.Collections.Generic.IList<T> list) { }
        // RVA: -1  // generic def  token: 0x600030D
        public static System.Boolean IsNullOrEmpty(System.Collections.Generic.ICollection<T> list) { }
        // RVA: 0x0B5968FC  token: 0x600030E
        public static System.Boolean IsNullOrEmpty(System.Collections.Specialized.OrderedDictionary dict) { }
        // RVA: -1  // generic def  token: 0x600030F
        public static T[] ToArrayE(System.Collections.Generic.IEnumerable<T> enumerable) { }
        // RVA: -1  // generic def  token: 0x6000310
        public static T GetValue(System.Collections.DictionaryEntry entry) { }

    }

    // TypeToken: 0x2000051  // size: 0x10
    public class JsonSerializer
    {
        // Methods
        // RVA: -1  // generic def  token: 0x6000311
        public static System.String Serialize(T obj, System.Boolean indented) { }
        // RVA: 0x0B59B334  token: 0x6000312
        public static System.String Serialize(System.Object obj, System.Boolean indented) { }
        // RVA: 0x0B59B364  token: 0x6000313
        private static System.String _SerializeDirect(Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject reflectObj, System.Boolean indented) { }
        // RVA: 0x0B59B558  token: 0x6000314
        private static System.Void _WriteReflectObj(Newtonsoft.Json.JsonWriter writer, Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject reflectObj) { }
        // RVA: 0x041E1670  token: 0x6000315
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000052  // size: 0x20
    public class ObjectReflector
    {
        // Fields
        private System.Collections.Generic.List<System.Object> m_tempList;  // 0x10
        private System.Collections.Generic.Dictionary<System.Type,Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType> m_typeMap;  // 0x18

        // Methods
        // RVA: 0x0B5A0D08  token: 0x6000316
        private System.Void .ctor() { }
        // RVA: 0x0B59E684  token: 0x6000317
        public static Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectResult Parse(System.Object obj) { }
        // RVA: 0x0B59E9FC  token: 0x6000318
        private Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectResult _ParseInternal(System.Object obj) { }
        // RVA: 0x0B59ED08  token: 0x6000319
        private Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType _ParseTypeMap(System.Type rootType) { }
        // RVA: 0x0B59FD60  token: 0x600031A
        private System.Boolean _TryParseType(System.Type type, Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType& reflectType) { }
        // RVA: 0x0B59FBD0  token: 0x600031B
        private Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject _ReflectObject(System.Object obj, Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType reflectType) { }
        // RVA: 0x0B59F340  token: 0x600031C
        private Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject _ReflectBean(System.Object obj, Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType reflectType) { }
        // RVA: 0x0B59EDE0  token: 0x600031D
        private Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject _ReflectArray(System.Object obj, Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType reflectType) { }
        // RVA: 0x0B59F87C  token: 0x600031E
        private Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject _ReflectMap(System.Object obj, Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType reflectType) { }
        // RVA: 0x0B59E8D8  token: 0x600031F
        private System.Collections.Generic.IEnumerable<System.Reflection.FieldInfo> _GetFields(System.Type type) { }
        // RVA: 0x0B59E708  token: 0x6000320
        private System.Object[] _ConvertICollectionToArray(System.Collections.ICollection collection) { }

    }

    // TypeToken: 0x2000058  // size: 0x50
    public class SparkSerializer : System.IDisposable
    {
        // Fields
        private Beyond.SparkBuffer.Serialize.SparkSerializer.Options m_options;  // 0x10
        private System.IO.MemoryStream m_ms;  // 0x20
        private Beyond.SparkBuffer.Runtime.ByteWriter m_writer;  // 0x28
        private System.Collections.Generic.List<Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType> m_enumTypes;  // 0x30
        private System.Collections.Generic.Dictionary<System.Type,Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType> m_beanMap;  // 0x38
        private System.Collections.Generic.Dictionary<System.Type,System.Int32> m_typeHashMap;  // 0x40
        private System.Collections.Generic.Dictionary<System.String,System.Int32> m_stringMap;  // 0x48

        // Methods
        // RVA: -1  // generic def  token: 0x6000329
        public static System.Byte[] Serialize(T obj) { }
        // RVA: -1  // generic def  token: 0x600032A
        public static System.Byte[] Serialize(T obj, Beyond.SparkBuffer.Serialize.SparkSerializer.Options options) { }
        // RVA: 0x0B5A3138  token: 0x600032B
        public static System.Byte[] Serialize(System.Object obj) { }
        // RVA: 0x0B5A3198  token: 0x600032C
        public static System.Byte[] Serialize(System.Object obj, Beyond.SparkBuffer.Serialize.SparkSerializer.Options options) { }
        // RVA: 0x0B5A6760  token: 0x600032D
        private System.Void .ctor(Beyond.SparkBuffer.Serialize.SparkSerializer.Options options) { }
        // RVA: 0x0B5A30EC  token: 0x600032E
        public virtual System.Void Dispose() { }
        // RVA: 0x0B5A6514  token: 0x600032F
        private System.Byte[] _SerializeInternal(System.Object obj) { }
        // RVA: 0x0B5A6308  token: 0x6000330
        private System.Void _PreprocessTypes(Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType[] reflectTypes) { }
        // RVA: 0x0B5A5DA0  token: 0x6000331
        private System.Void _ExportTypeDefs() { }
        // RVA: 0x0B5A4C34  token: 0x6000332
        private System.Void _ExportEnumType(Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType enumType) { }
        // RVA: 0x0B5A442C  token: 0x6000333
        private System.Void _ExportBeanType(Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType beanType) { }
        // RVA: 0x0B5A629C  token: 0x6000334
        private System.String _ParseTypeName(System.Type reflectType) { }
        // RVA: 0x0B5A3338  token: 0x6000335
        private static System.Collections.Generic.HashSet<System.String> _CollectAllStrings(Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject rootObj) { }
        // RVA: 0x0B5A6028  token: 0x6000336
        private System.Collections.Generic.List<System.Collections.Generic.List<System.String>> _GenStringHashList(System.Collections.Generic.HashSet<System.String> strings) { }
        // RVA: 0x0B5A5B08  token: 0x6000337
        private System.Void _ExportStrings(Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject rootObj) { }
        // RVA: 0x0B5A485C  token: 0x6000338
        private System.Void _ExportElementData(Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject elementObj, System.Collections.Generic.List<Beyond.SparkBuffer.Serialize.SparkSerializer.ReservedField> reservedFields) { }
        // RVA: 0x0B5A4050  token: 0x6000339
        private System.Int32 _ExportBeanData(Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject beanObj) { }
        // RVA: 0x0B5A3DFC  token: 0x600033A
        private System.Int32 _ExportArrayData(Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject arrayObj) { }
        // RVA: 0x0B5A5088  token: 0x600033B
        private System.Int32 _ExportMapData(Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject mapObj) { }
        // RVA: 0x0B5A39F0  token: 0x600033C
        private System.Collections.Generic.Dictionary<System.Int32,System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject,Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject>>> _CreateMapHashEntry(System.Collections.Generic.List<System.Collections.Generic.KeyValuePair<Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject,Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject>> mapElements, Beyond.SparkBuffer.SparkType keyType) { }
        // RVA: 0x0B5A5928  token: 0x600033D
        private System.Void _ExportRootDef(Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType rootType) { }
        // RVA: 0x0B5A4768  token: 0x600033E
        private System.Int32 _ExportDataFromRoot(Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectObject rootObj) { }
        // RVA: 0x0B5A56BC  token: 0x600033F
        private System.Void _ExportReservedFields(System.Collections.Generic.List<Beyond.SparkBuffer.Serialize.SparkSerializer.ReservedField> reservedFields) { }
        // RVA: 0x02F05D70  token: 0x6000340
        public static System.Int32 GetStableStringHash(System.String str) { }
        // RVA: 0x0B5A310C  token: 0x6000341
        public static System.Int32 GetStableTypeHash(System.Type type) { }
        // RVA: 0x0B5A6684  token: 0x6000342
        private System.Void _WriteConcreteType(System.Type type) { }
        // RVA: 0x0B5A32AC  token: 0x6000343
        private System.Int32 <_ExportTypeDefs>b__17_0(Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType lhs, Beyond.SparkBuffer.Serialize.ObjectReflector.ReflectType rhs) { }

    }

}

namespace IFix
{

    // TypeToken: 0x2000062  // size: 0x28
    public class ILFixDynamicMethodWrapper
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10
        private System.Int32 methodId;  // 0x18
        private System.Object anonObj;  // 0x20
        public static IFix.ILFixDynamicMethodWrapper[] wrapperArray;  // static @ 0x0

        // Methods
        // RVA: 0x032B2C30  token: 0x600037D
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj) { }
        // RVA: 0x067CDB90  token: 0x600037E
        public System.IntPtr __Gen_Wrap_0(System.Object P0) { }
        // RVA: 0x0B5A7468  token: 0x600037F
        public System.ReadOnlySpan<System.Byte> __Gen_Wrap_1(System.IntPtr P0, System.Int32 P1) { }
        // RVA: 0x063606FC  token: 0x6000380
        public System.IntPtr __Gen_Wrap_2(System.Int32 P0) { }
        // RVA: 0x0B5A757C  token: 0x6000381
        public System.Void __Gen_Wrap_3(System.Int32 P0, System.IntPtr P1) { }
        // RVA: 0x063761FC  token: 0x6000382
        public System.Void __Gen_Wrap_4() { }
        // RVA: 0x0B5A7644  token: 0x6000383
        public System.String __Gen_Wrap_5() { }
        // RVA: 0x09CC7624  token: 0x6000384
        public System.Void __Gen_Wrap_6(System.Object P0, System.IntPtr P1) { }
        // RVA: 0x0B5A76FC  token: 0x6000385
        public Beyond.Reflection.StructPtrWrapper __Gen_Wrap_7(System.IntPtr P0, System.Int32 P1, System.Object P2) { }
        // RVA: 0x0B5A7810  token: 0x6000386
        public Beyond.Reflection.StructPtrWrapper __Gen_Wrap_8(System.Object P0) { }
        // RVA: 0x066C296C  token: 0x6000387
        public System.Void __Gen_Wrap_9(System.Object P0) { }
        // RVA: 0x0B5A7008  token: 0x6000388
        public System.Object __Gen_Wrap_10(System.Object P0) { }
        // RVA: 0x0B5A70E4  token: 0x6000389
        public System.Object __Gen_Wrap_11(System.IntPtr P0, System.Object P1, System.Int32 P2) { }
        // RVA: 0x09C8B45C  token: 0x600038A
        public System.Void __Gen_Wrap_12(System.Object P0, System.Object P1) { }
        // RVA: 0x06366E40  token: 0x600038B
        public System.Int32 __Gen_Wrap_13(System.Object P0) { }
        // RVA: 0x0A8A1EF4  token: 0x600038C
        public System.Boolean __Gen_Wrap_14(System.Object P0, System.Object P1) { }
        // RVA: 0x0B5A71F8  token: 0x600038D
        public System.String __Gen_Wrap_15(System.Object P0) { }
        // RVA: 0x0B5A72D4  token: 0x600038E
        public System.Type __Gen_Wrap_16(System.Object P0) { }
        // RVA: 0x0B5A73B0  token: 0x600038F
        public Beyond.Reflection.StructPtrWrapper __Gen_Wrap_17() { }
        // RVA: 0x04D372C0  token: 0x6000390
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000063  // size: 0x40
    public class ILFixInterfaceBridge : IFix.Core.AnonymousStorey
    {
        // Methods
        // RVA: 0x0B5A78EC  token: 0x6000391
        public System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x041E1670  token: 0x6000392
        public System.Void RefAsyncBuilderStartMethod() { }

    }

    // TypeToken: 0x2000064  // size: 0x18
    public class WrappersManagerImpl : IFix.Core.WrappersManager
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000393
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x0B5A9DD0  token: 0x6000394
        public static IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id) { }
        // RVA: 0x0328E430  token: 0x6000395
        public static System.Boolean IsPatched(System.Int32 id) { }
        // RVA: 0x0B5A9C8C  token: 0x6000396
        public virtual System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon) { }
        // RVA: 0x0B5A9D58  token: 0x6000397
        public virtual System.Object CreateWrapper(System.Int32 id) { }
        // RVA: 0x0B5A9E34  token: 0x6000398
        public virtual System.Object InitWrapperArray(System.Int32 len) { }
        // RVA: 0x0B5A9BE8  token: 0x6000399
        public virtual IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine) { }

    }

    // TypeToken: 0x2000065  // size: 0x14
    public sealed struct IDMAP0
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static IFix.IDMAP0 Beyond-Reflection-MethodPointerManager-GetDelegatePointer0;  // const
        public static IFix.IDMAP0 Beyond-Reflection-ObjectPointerHelper-GetReadOnlySpan0;  // const
        public static IFix.IDMAP0 Beyond-Reflection-StructMemManager-GetMemoryBySize0;  // const
        public static IFix.IDMAP0 Beyond-Reflection-StructMemManager-FreeMemory0;  // const
        public static IFix.IDMAP0 Beyond-Reflection-StructMemManager-ClearMemory0;  // const
        public static IFix.IDMAP0 Beyond-Reflection-StructMemManager-DumpMemory0;  // const
        public static IFix.IDMAP0 Beyond-Reflection-StructPtrRefrenceManager-_RunOnStart0;  // const
        public static IFix.IDMAP0 Beyond-Reflection-StructPtrRefrenceManager-AddObjInPtrRef0;  // const
        public static IFix.IDMAP0 Beyond-Reflection-StructPtrRefrenceManager-RemoveObjInPtrRef0;  // const
        public static IFix.IDMAP0 Beyond-Reflection-StructPtrWrapper-GetStructPtrWrapper0;  // const
        public static IFix.IDMAP0 Beyond-Reflection-StructPtrWrapper-GetStructPtrWrapper1;  // const
        public static IFix.IDMAP0 Beyond-Reflection-StructPtrWrapper-Release0;  // const
        public static IFix.IDMAP0 Beyond-Reflection-StructPtrWrapper-GetStructObj0;  // const
        public static IFix.IDMAP0 Beyond-Reflection-StructPtrWrapper-GetStructObj1;  // const
        public static IFix.IDMAP0 Beyond-Reflection-StructPtrWrapper-SetStructObjValue0;  // const
        public static IFix.IDMAP0 Beyond-Reflection-StructPtrWrapper-GetHashCode0;  // const
        public static IFix.IDMAP0 Beyond-Reflection-StructPtrWrapper-Equals0;  // const
        public static IFix.IDMAP0 Beyond-Reflection-StructPtrWrapper-ToString0;  // const
        public static IFix.IDMAP0 Beyond-Reflection-StructPtrWrapper-GetType0;  // const
        public static IFix.IDMAP0 Beyond-Reflection-StructPtrWrapper-GetStructObj2;  // const
        public static IFix.IDMAP0 Beyond-Reflection-StructPtrWrapper-_OnCreate0;  // const
        public static IFix.IDMAP0 Beyond-Reflection-StructPtrWrapper-_OnGet0;  // const
        public static IFix.IDMAP0 Beyond-Reflection-StructPtrWrapper-_OnRealse0;  // const

    }

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
    public sealed class IsUnmanagedAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000002
        public System.Void .ctor() { }

    }

}

