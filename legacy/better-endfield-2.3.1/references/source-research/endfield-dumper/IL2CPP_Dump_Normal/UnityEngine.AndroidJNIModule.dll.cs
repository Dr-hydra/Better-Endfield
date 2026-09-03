// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.AndroidJNIModule.dll
// Classes:  15
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

namespace UnityEngine
{

    // TypeToken: 0x2000002  // size: 0x80
    public sealed class AndroidJavaRunnable : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x053DEDC8  token: 0x6000001
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x053DEDB8  token: 0x6000002
        public virtual System.Void Invoke() { }

    }

    // TypeToken: 0x2000003  // size: 0x98
    public sealed class AndroidJavaException : System.Exception
    {
        // Fields
        private System.String mJavaStackTrace;  // 0x90

        // Properties
        System.String StackTrace { get; /* RVA: 0x0B38ED58 */ }

        // Methods
        // RVA: 0x0B38ECE8  token: 0x6000003
        private System.Void .ctor(System.String message, System.String javaStackTrace) { }

    }

    // TypeToken: 0x2000004  // size: 0x20
    public class GlobalJavaObjectRef
    {
        // Fields
        private System.Boolean m_disposed;  // 0x10
        protected System.IntPtr m_jobject;  // 0x18

        // Methods
        // RVA: 0x0B390FBC  token: 0x6000005
        public System.Void .ctor(System.IntPtr jobject) { }
        // RVA: 0x0B390F4C  token: 0x6000006
        protected virtual System.Void Finalize() { }
        // RVA: 0x053E4DA0  token: 0x6000007
        public static System.IntPtr op_Implicit(UnityEngine.GlobalJavaObjectRef obj) { }
        // RVA: 0x0B390F24  token: 0x6000008
        public System.Void Dispose() { }

    }

    // TypeToken: 0x2000005  // size: 0x28
    public class AndroidJavaRunnableProxy : UnityEngine.AndroidJavaProxy
    {
        // Fields
        private UnityEngine.AndroidJavaRunnable mRunnable;  // 0x20

        // Methods
        // RVA: 0x0B390134  token: 0x6000009
        public System.Void .ctor(UnityEngine.AndroidJavaRunnable runnable) { }

    }

    // TypeToken: 0x2000006  // size: 0x20
    public class AndroidJavaProxy
    {
        // Fields
        public readonly UnityEngine.AndroidJavaClass javaInterface;  // 0x10
        private System.IntPtr proxyObject;  // 0x18
        private static readonly UnityEngine.GlobalJavaObjectRef s_JavaLangSystemClass;  // static @ 0x0
        private static readonly System.IntPtr s_HashCodeMethodID;  // static @ 0x8

        // Methods
        // RVA: 0x0B38FF64  token: 0x600000A
        public System.Void .ctor(System.String javaInterface) { }
        // RVA: 0x0B38FFDC  token: 0x600000B
        public System.Void .ctor(UnityEngine.AndroidJavaClass javaInterface) { }
        // RVA: 0x0B38F450  token: 0x600000C
        protected virtual System.Void Finalize() { }
        // RVA: 0x0B38F540  token: 0x600000D
        public virtual UnityEngine.AndroidJavaObject Invoke(System.String methodName, System.Object[] args) { }
        // RVA: 0x0B38FD40  token: 0x600000E
        public virtual UnityEngine.AndroidJavaObject Invoke(System.String methodName, UnityEngine.AndroidJavaObject[] javaArgs) { }
        // RVA: 0x0B38FFF4  token: 0x600000F
        public virtual System.Boolean equals(UnityEngine.AndroidJavaObject obj) { }
        // RVA: 0x0B390028  token: 0x6000010
        public virtual System.Int32 hashCode() { }
        // RVA: 0x0B3900D8  token: 0x6000011
        public virtual System.String toString() { }
        // RVA: 0x0B38F4C4  token: 0x6000012
        private UnityEngine.AndroidJavaObject GetProxyObject() { }
        // RVA: 0x0B38F4E0  token: 0x6000013
        private System.IntPtr GetRawProxy() { }
        // RVA: 0x0B38FE58  token: 0x6000014
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000007  // size: 0x20
    public class AndroidJavaObject : System.IDisposable
    {
        // Fields
        private static System.Boolean enableDebugPrints;  // static @ 0x0
        private UnityEngine.GlobalJavaObjectRef m_jobject;  // 0x10
        private UnityEngine.GlobalJavaObjectRef m_jclass;  // 0x18

        // Methods
        // RVA: 0x0B38F448  token: 0x6000015
        public System.Void .ctor(System.String className, System.Object[] args) { }
        // RVA: 0x0B38EF8C  token: 0x6000016
        public virtual System.Void Dispose() { }
        // RVA: -1  // generic def  token: 0x6000017
        public FieldType Get(System.String fieldName) { }
        // RVA: -1  // generic def  token: 0x6000018
        public FieldType GetStatic(System.String fieldName) { }
        // RVA: 0x0B38F0AC  token: 0x6000019
        public System.IntPtr GetRawObject() { }
        // RVA: 0x0B38F0A0  token: 0x600001A
        public System.IntPtr GetRawClass() { }
        // RVA: -1  // generic def  token: 0x600001B
        public ReturnType Call(System.String methodName, System.Object[] args) { }
        // RVA: -1  // generic def  token: 0x600001C
        public ReturnType CallStatic(System.String methodName, System.Object[] args) { }
        // RVA: 0x0B38EF2C  token: 0x600001D
        protected System.Void DebugPrint(System.String msg) { }
        // RVA: 0x0B38F0B8  token: 0x600001E
        private System.Void _AndroidJavaObject(System.String className, System.Object[] args) { }
        // RVA: 0x0B38F328  token: 0x600001F
        private System.Void .ctor(System.IntPtr jobject) { }
        // RVA: 0x041E1670  token: 0x6000020
        private System.Void .ctor() { }
        // RVA: 0x0B38F02C  token: 0x6000021
        protected virtual System.Void Finalize() { }
        // RVA: 0x0B38EFDC  token: 0x6000022
        protected virtual System.Void Dispose(System.Boolean disposing) { }
        // RVA: -1  // generic def  token: 0x6000023
        protected ReturnType _Call(System.String methodName, System.Object[] args) { }
        // RVA: -1  // generic def  token: 0x6000024
        protected FieldType _Get(System.String fieldName) { }
        // RVA: -1  // generic def  token: 0x6000025
        protected ReturnType _CallStatic(System.String methodName, System.Object[] args) { }
        // RVA: -1  // generic def  token: 0x6000026
        protected FieldType _GetStatic(System.String fieldName) { }
        // RVA: 0x0B38EE58  token: 0x6000027
        private static UnityEngine.AndroidJavaObject AndroidJavaObjectDeleteLocalRef(System.IntPtr jobject) { }
        // RVA: 0x0B38ED84  token: 0x6000028
        private static UnityEngine.AndroidJavaClass AndroidJavaClassDeleteLocalRef(System.IntPtr jclass) { }
        // RVA: -1  // generic def  token: 0x6000029
        private static ReturnType FromJavaArrayDeleteLocalRef(System.IntPtr jobject) { }
        // RVA: 0x0B38F314  token: 0x600002A
        protected System.IntPtr _GetRawObject() { }
        // RVA: 0x0A8DA498  token: 0x600002B
        protected System.IntPtr _GetRawClass() { }

    }

    // TypeToken: 0x2000008  // size: 0x20
    public class AndroidJavaClass : UnityEngine.AndroidJavaObject
    {
        // Methods
        // RVA: 0x0B38ECE0  token: 0x600002C
        public System.Void .ctor(System.String className) { }
        // RVA: 0x0B38EB28  token: 0x600002D
        private System.Void _AndroidJavaClass(System.String className) { }
        // RVA: 0x0B38EC08  token: 0x600002E
        private System.Void .ctor(System.IntPtr jclass) { }

    }

    // TypeToken: 0x2000009  // size: 0x10
    public class AndroidReflection
    {
        // Fields
        private static readonly UnityEngine.GlobalJavaObjectRef s_ReflectionHelperClass;  // static @ 0x0
        private static readonly System.IntPtr s_ReflectionHelperGetConstructorID;  // static @ 0x8
        private static readonly System.IntPtr s_ReflectionHelperGetMethodID;  // static @ 0x10
        private static readonly System.IntPtr s_ReflectionHelperGetFieldID;  // static @ 0x18
        private static readonly System.IntPtr s_ReflectionHelperGetFieldSignature;  // static @ 0x20
        private static readonly System.IntPtr s_ReflectionHelperNewProxyInstance;  // static @ 0x28
        private static readonly System.IntPtr s_ReflectionHelperSetNativeExceptionOnProxy;  // static @ 0x30
        private static readonly System.IntPtr s_FieldGetDeclaringClass;  // static @ 0x38

        // Methods
        // RVA: 0x0AAAB0A8  token: 0x600002F
        public static System.Boolean IsPrimitive(System.Type t) { }
        // RVA: 0x0AC76A90  token: 0x6000030
        public static System.Boolean IsAssignableFrom(System.Type t, System.Type from) { }
        // RVA: 0x0B390860  token: 0x6000031
        private static System.IntPtr GetStaticMethodID(System.String clazz, System.String methodName, System.String signature) { }
        // RVA: 0x0B3905E0  token: 0x6000032
        private static System.IntPtr GetMethodID(System.String clazz, System.String methodName, System.String signature) { }
        // RVA: 0x0B3901A4  token: 0x6000033
        public static System.IntPtr GetConstructorMember(System.IntPtr jclass, System.String signature) { }
        // RVA: 0x0B39068C  token: 0x6000034
        public static System.IntPtr GetMethodMember(System.IntPtr jclass, System.String methodName, System.String signature, System.Boolean isStatic) { }
        // RVA: 0x0B390364  token: 0x6000035
        public static System.IntPtr GetFieldMember(System.IntPtr jclass, System.String fieldName, System.String signature, System.Boolean isStatic) { }
        // RVA: 0x0B39030C  token: 0x6000036
        public static System.IntPtr GetFieldClass(System.IntPtr field) { }
        // RVA: 0x0B390538  token: 0x6000037
        public static System.String GetFieldSignature(System.IntPtr field) { }
        // RVA: 0x0B39090C  token: 0x6000038
        public static System.IntPtr NewProxyInstance(System.IntPtr player, System.IntPtr delegateHandle, System.IntPtr interfaze) { }
        // RVA: 0x0B3909E8  token: 0x6000039
        public static System.Void SetNativeExceptionOnProxy(System.IntPtr proxy, System.Exception e, System.Boolean methodNotFound) { }
        // RVA: 0x0B390AD4  token: 0x600003A
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000A  // size: 0x10
    public sealed class _AndroidJNIHelper
    {
        // Methods
        // RVA: 0x0B392590  token: 0x600003B
        public static System.IntPtr CreateJavaProxy(System.IntPtr player, System.IntPtr delegateHandle, UnityEngine.AndroidJavaProxy proxy) { }
        // RVA: 0x0B392610  token: 0x600003C
        public static System.IntPtr CreateJavaRunnable(UnityEngine.AndroidJavaRunnable jrunnable) { }
        // RVA: 0x0B3939E4  token: 0x600003D
        public static System.IntPtr InvokeJavaProxyMethod(UnityEngine.AndroidJavaProxy proxy, System.IntPtr jmethodName, System.IntPtr jargs) { }
        // RVA: 0x0B391FFC  token: 0x600003E
        public static UnityEngine.jvalue[] CreateJNIArgArray(System.Object[] args) { }
        // RVA: 0x0B393C80  token: 0x600003F
        public static System.Object UnboxArray(UnityEngine.AndroidJavaObject obj) { }
        // RVA: 0x0B394268  token: 0x6000040
        public static System.Object Unbox(UnityEngine.AndroidJavaObject obj) { }
        // RVA: 0x0B390FE8  token: 0x6000041
        public static UnityEngine.AndroidJavaObject Box(System.Object obj) { }
        // RVA: 0x0B392678  token: 0x6000042
        public static System.Void DeleteJNIArgArray(System.Object[] args, UnityEngine.jvalue[] jniArgs) { }
        // RVA: 0x0B39192C  token: 0x6000043
        public static System.IntPtr ConvertToJNIArray(System.Array array) { }
        // RVA: -1  // generic def  token: 0x6000044
        public static ArrayType ConvertFromJNIArray(System.IntPtr array) { }
        // RVA: 0x0B392918  token: 0x6000045
        public static System.IntPtr GetConstructorID(System.IntPtr jclass, System.Object[] args) { }
        // RVA: -1  // generic def  token: 0x6000046
        public static System.IntPtr GetMethodID(System.IntPtr jclass, System.String methodName, System.Object[] args, System.Boolean isStatic) { }
        // RVA: -1  // generic def  token: 0x6000047
        public static System.IntPtr GetFieldID(System.IntPtr jclass, System.String fieldName, System.Boolean isStatic) { }
        // RVA: 0x0B39276C  token: 0x6000048
        public static System.IntPtr GetConstructorID(System.IntPtr jclass, System.String signature) { }
        // RVA: 0x0B392C3C  token: 0x6000049
        public static System.IntPtr GetMethodID(System.IntPtr jclass, System.String methodName, System.String signature, System.Boolean isStatic) { }
        // RVA: 0x0B392BB4  token: 0x600004A
        private static System.IntPtr GetMethodIDFallback(System.IntPtr jclass, System.String methodName, System.String signature, System.Boolean isStatic) { }
        // RVA: 0x0B392944  token: 0x600004B
        public static System.IntPtr GetFieldID(System.IntPtr jclass, System.String fieldName, System.String signature, System.Boolean isStatic) { }
        // RVA: 0x0B392ECC  token: 0x600004C
        public static System.String GetSignature(System.Object obj) { }
        // RVA: 0x0B392DFC  token: 0x600004D
        public static System.String GetSignature(System.Object[] args) { }
        // RVA: -1  // generic def  token: 0x600004E
        public static System.String GetSignature(System.Object[] args) { }

    }

    // TypeToken: 0x200000B  // size: 0x18
    public sealed struct jvalue
    {
        // Fields
        public System.Boolean z;  // 0x10
        public System.SByte b;  // 0x10
        public System.Char c;  // 0x10
        public System.Int16 s;  // 0x10
        public System.Int32 i;  // 0x10
        public System.Int64 j;  // 0x10
        public System.Single f;  // 0x10
        public System.Double d;  // 0x10
        public System.IntPtr l;  // 0x10

    }

    // TypeToken: 0x200000C  // size: 0x10
    public static class AndroidJNIHelper
    {
        // Methods
        // RVA: 0x0B38ACA4  token: 0x600004F
        public static System.IntPtr GetConstructorID(System.IntPtr javaClass, System.String signature) { }
        // RVA: 0x0B38ACC0  token: 0x6000050
        public static System.IntPtr GetMethodID(System.IntPtr javaClass, System.String methodName, System.String signature, System.Boolean isStatic) { }
        // RVA: 0x0B38ACB0  token: 0x6000051
        public static System.IntPtr GetFieldID(System.IntPtr javaClass, System.String fieldName, System.String signature, System.Boolean isStatic) { }
        // RVA: 0x0B38AC84  token: 0x6000052
        public static System.IntPtr CreateJavaRunnable(UnityEngine.AndroidJavaRunnable jrunnable) { }
        // RVA: 0x0B38AB70  token: 0x6000053
        public static System.IntPtr CreateJavaProxy(UnityEngine.AndroidJavaProxy proxy) { }
        // RVA: 0x0B38AB64  token: 0x6000054
        public static UnityEngine.jvalue[] CreateJNIArgArray(System.Object[] args) { }
        // RVA: 0x0B38AC90  token: 0x6000055
        public static System.Void DeleteJNIArgArray(System.Object[] args, UnityEngine.jvalue[] jniArgs) { }
        // RVA: 0x0B38AC98  token: 0x6000056
        public static System.IntPtr GetConstructorID(System.IntPtr jclass, System.Object[] args) { }
        // RVA: -1  // generic def  token: 0x6000057
        public static ArrayType ConvertFromJNIArray(System.IntPtr array) { }
        // RVA: -1  // generic def  token: 0x6000058
        public static System.IntPtr GetMethodID(System.IntPtr jclass, System.String methodName, System.Object[] args, System.Boolean isStatic) { }
        // RVA: -1  // generic def  token: 0x6000059
        public static System.IntPtr GetFieldID(System.IntPtr jclass, System.String fieldName, System.Boolean isStatic) { }

    }

    // TypeToken: 0x200000D  // size: 0x10
    public static class AndroidJNI
    {
        // Methods
        // RVA: 0x0B38DCDC  token: 0x600005A
        public static System.IntPtr FindClass(System.String name) { }
        // RVA: 0x0B38DE7C  token: 0x600005B
        public static System.IntPtr FromReflectedMethod(System.IntPtr refMethod) { }
        // RVA: 0x0B38DCB0  token: 0x600005C
        public static System.IntPtr ExceptionOccurred() { }
        // RVA: 0x0B38DC84  token: 0x600005D
        public static System.Void ExceptionClear() { }
        // RVA: 0x0B38E88C  token: 0x600005E
        public static System.Int32 PushLocalFrame(System.Int32 capacity) { }
        // RVA: 0x0B38E858  token: 0x600005F
        public static System.IntPtr PopLocalFrame(System.IntPtr ptr) { }
        // RVA: 0x0B38E6D8  token: 0x6000060
        public static System.IntPtr NewGlobalRef(System.IntPtr obj) { }
        // RVA: 0x0B38DBE8  token: 0x6000061
        public static System.Void DeleteGlobalRef(System.IntPtr obj) { }
        // RVA: 0x0B38E824  token: 0x6000062
        public static System.IntPtr NewWeakGlobalRef(System.IntPtr obj) { }
        // RVA: 0x0B38DC50  token: 0x6000063
        public static System.Void DeleteWeakGlobalRef(System.IntPtr obj) { }
        // RVA: 0x0B38E70C  token: 0x6000064
        public static System.IntPtr NewLocalRef(System.IntPtr obj) { }
        // RVA: 0x0B38DC1C  token: 0x6000065
        public static System.Void DeleteLocalRef(System.IntPtr obj) { }
        // RVA: 0x0B38E694  token: 0x6000066
        public static System.Boolean IsSameObject(System.IntPtr obj1, System.IntPtr obj2) { }
        // RVA: 0x0B38E790  token: 0x6000067
        public static System.IntPtr NewObject(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38E1CC  token: 0x6000068
        public static System.IntPtr GetObjectClass(System.IntPtr obj) { }
        // RVA: 0x0B38E138  token: 0x6000069
        public static System.IntPtr GetMethodID(System.IntPtr clazz, System.String name, System.String sig) { }
        // RVA: 0x0B38E018  token: 0x600006A
        public static System.IntPtr GetFieldID(System.IntPtr clazz, System.String name, System.String sig) { }
        // RVA: 0x0B38E4B8  token: 0x600006B
        public static System.IntPtr GetStaticMethodID(System.IntPtr clazz, System.String name, System.String sig) { }
        // RVA: 0x0B38E398  token: 0x600006C
        public static System.IntPtr GetStaticFieldID(System.IntPtr clazz, System.String name, System.String sig) { }
        // RVA: 0x0B38E818  token: 0x600006D
        public static System.IntPtr NewString(System.String chars) { }
        // RVA: 0x0B38E7E4  token: 0x600006E
        private static System.IntPtr NewStringFromStr(System.String chars) { }
        // RVA: 0x0B38E61C  token: 0x600006F
        public static System.String GetStringChars(System.IntPtr str) { }
        // RVA: 0x0B38DB94  token: 0x6000070
        public static System.String CallStringMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38D6FC  token: 0x6000071
        public static System.IntPtr CallObjectMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38D654  token: 0x6000072
        public static System.Int32 CallIntMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38D504  token: 0x6000073
        public static System.Boolean CallBooleanMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38D7A4  token: 0x6000074
        public static System.Int16 CallShortMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38D750  token: 0x6000075
        public static System.SByte CallSByteMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38D558  token: 0x6000076
        public static System.Char CallCharMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38D600  token: 0x6000077
        public static System.Single CallFloatMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38D5AC  token: 0x6000078
        public static System.Double CallDoubleMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38D6A8  token: 0x6000079
        public static System.Int64 CallLongMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38E650  token: 0x600007A
        public static System.String GetStringField(System.IntPtr obj, System.IntPtr fieldID) { }
        // RVA: 0x0B38E200  token: 0x600007B
        public static System.IntPtr GetObjectField(System.IntPtr obj, System.IntPtr fieldID) { }
        // RVA: 0x0B38DF4C  token: 0x600007C
        public static System.Boolean GetBooleanField(System.IntPtr obj, System.IntPtr fieldID) { }
        // RVA: 0x0B38E244  token: 0x600007D
        public static System.SByte GetSByteField(System.IntPtr obj, System.IntPtr fieldID) { }
        // RVA: 0x0B38DF90  token: 0x600007E
        public static System.Char GetCharField(System.IntPtr obj, System.IntPtr fieldID) { }
        // RVA: 0x0B38E288  token: 0x600007F
        public static System.Int16 GetShortField(System.IntPtr obj, System.IntPtr fieldID) { }
        // RVA: 0x0B38E0B0  token: 0x6000080
        public static System.Int32 GetIntField(System.IntPtr obj, System.IntPtr fieldID) { }
        // RVA: 0x0B38E0F4  token: 0x6000081
        public static System.Int64 GetLongField(System.IntPtr obj, System.IntPtr fieldID) { }
        // RVA: 0x0B38E06C  token: 0x6000082
        public static System.Single GetFloatField(System.IntPtr obj, System.IntPtr fieldID) { }
        // RVA: 0x0B38DFD4  token: 0x6000083
        public static System.Double GetDoubleField(System.IntPtr obj, System.IntPtr fieldID) { }
        // RVA: 0x0B38DAEC  token: 0x6000084
        public static System.String CallStaticStringMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38D9F0  token: 0x6000085
        public static System.IntPtr CallStaticObjectMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38D948  token: 0x6000086
        public static System.Int32 CallStaticIntMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38D7F8  token: 0x6000087
        public static System.Boolean CallStaticBooleanMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38DA98  token: 0x6000088
        public static System.Int16 CallStaticShortMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38DA44  token: 0x6000089
        public static System.SByte CallStaticSByteMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38D84C  token: 0x600008A
        public static System.Char CallStaticCharMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38D8F4  token: 0x600008B
        public static System.Single CallStaticFloatMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38D8A0  token: 0x600008C
        public static System.Double CallStaticDoubleMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38D99C  token: 0x600008D
        public static System.Int64 CallStaticLongMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38DB40  token: 0x600008E
        public static System.Void CallStaticVoidMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38E5D8  token: 0x600008F
        public static System.String GetStaticStringField(System.IntPtr clazz, System.IntPtr fieldID) { }
        // RVA: 0x0B38E50C  token: 0x6000090
        public static System.IntPtr GetStaticObjectField(System.IntPtr clazz, System.IntPtr fieldID) { }
        // RVA: 0x0B38E2CC  token: 0x6000091
        public static System.Boolean GetStaticBooleanField(System.IntPtr clazz, System.IntPtr fieldID) { }
        // RVA: 0x0B38E550  token: 0x6000092
        public static System.SByte GetStaticSByteField(System.IntPtr clazz, System.IntPtr fieldID) { }
        // RVA: 0x0B38E310  token: 0x6000093
        public static System.Char GetStaticCharField(System.IntPtr clazz, System.IntPtr fieldID) { }
        // RVA: 0x0B38E594  token: 0x6000094
        public static System.Int16 GetStaticShortField(System.IntPtr clazz, System.IntPtr fieldID) { }
        // RVA: 0x0B38E430  token: 0x6000095
        public static System.Int32 GetStaticIntField(System.IntPtr clazz, System.IntPtr fieldID) { }
        // RVA: 0x0B38E474  token: 0x6000096
        public static System.Int64 GetStaticLongField(System.IntPtr clazz, System.IntPtr fieldID) { }
        // RVA: 0x0B38E3EC  token: 0x6000097
        public static System.Single GetStaticFloatField(System.IntPtr clazz, System.IntPtr fieldID) { }
        // RVA: 0x0B38E354  token: 0x6000098
        public static System.Double GetStaticDoubleField(System.IntPtr clazz, System.IntPtr fieldID) { }
        // RVA: 0x0B38E910  token: 0x6000099
        public static System.IntPtr ToBooleanArray(System.Boolean[] array) { }
        // RVA: 0x0B38E944  token: 0x600009A
        public static System.IntPtr ToByteArray(System.Byte[] array) { }
        // RVA: 0x0B38EAC0  token: 0x600009B
        public static System.IntPtr ToSByteArray(System.SByte[] array) { }
        // RVA: 0x0B38E978  token: 0x600009C
        public static System.IntPtr ToCharArray(System.Char[] array) { }
        // RVA: 0x0B38EAF4  token: 0x600009D
        public static System.IntPtr ToShortArray(System.Int16[] array) { }
        // RVA: 0x0B38EA14  token: 0x600009E
        public static System.IntPtr ToIntArray(System.Int32[] array) { }
        // RVA: 0x0B38EA48  token: 0x600009F
        public static System.IntPtr ToLongArray(System.Int64[] array) { }
        // RVA: 0x0B38E9E0  token: 0x60000A0
        public static System.IntPtr ToFloatArray(System.Single[] array) { }
        // RVA: 0x0B38E9AC  token: 0x60000A1
        public static System.IntPtr ToDoubleArray(System.Double[] array) { }
        // RVA: 0x0B38EA7C  token: 0x60000A2
        public static System.IntPtr ToObjectArray(System.IntPtr[] array, System.IntPtr arrayClass) { }
        // RVA: 0x0B38DD10  token: 0x60000A3
        public static System.Boolean[] FromBooleanArray(System.IntPtr array) { }
        // RVA: 0x0B38DD44  token: 0x60000A4
        public static System.Byte[] FromByteArray(System.IntPtr array) { }
        // RVA: 0x0B38DEB0  token: 0x60000A5
        public static System.SByte[] FromSByteArray(System.IntPtr array) { }
        // RVA: 0x0B38DD78  token: 0x60000A6
        public static System.Char[] FromCharArray(System.IntPtr array) { }
        // RVA: 0x0B38DEE4  token: 0x60000A7
        public static System.Int16[] FromShortArray(System.IntPtr array) { }
        // RVA: 0x0B38DE14  token: 0x60000A8
        public static System.Int32[] FromIntArray(System.IntPtr array) { }
        // RVA: 0x0B38DE48  token: 0x60000A9
        public static System.Int64[] FromLongArray(System.IntPtr array) { }
        // RVA: 0x0B38DDE0  token: 0x60000AA
        public static System.Single[] FromFloatArray(System.IntPtr array) { }
        // RVA: 0x0B38DDAC  token: 0x60000AB
        public static System.Double[] FromDoubleArray(System.IntPtr array) { }
        // RVA: 0x0B38DF18  token: 0x60000AC
        public static System.Int32 GetArrayLength(System.IntPtr array) { }
        // RVA: 0x0B38E740  token: 0x60000AD
        public static System.IntPtr NewObjectArray(System.Int32 size, System.IntPtr clazz, System.IntPtr obj) { }
        // RVA: 0x0B38E18C  token: 0x60000AE
        public static System.IntPtr GetObjectArrayElement(System.IntPtr array, System.Int32 index) { }
        // RVA: 0x0B38E8C0  token: 0x60000AF
        public static System.Void SetObjectArrayElement(System.IntPtr array, System.Int32 index, System.IntPtr obj) { }

    }

    // TypeToken: 0x200000E  // size: 0x10
    public class AndroidJNISafe
    {
        // Methods
        // RVA: 0x0B38B7D8  token: 0x60000B0
        public static System.Void CheckException() { }
        // RVA: 0x0B38BA28  token: 0x60000B1
        public static System.Void DeleteGlobalRef(System.IntPtr globalref) { }
        // RVA: 0x0B38BA48  token: 0x60000B2
        public static System.Void DeleteWeakGlobalRef(System.IntPtr globalref) { }
        // RVA: 0x0B38BA38  token: 0x60000B3
        public static System.Void DeleteLocalRef(System.IntPtr localref) { }
        // RVA: 0x0B38CF54  token: 0x60000B4
        public static System.IntPtr NewString(System.String chars) { }
        // RVA: 0x0B38CDC0  token: 0x60000B5
        public static System.String GetStringChars(System.IntPtr str) { }
        // RVA: 0x0B38C550  token: 0x60000B6
        public static System.IntPtr GetObjectClass(System.IntPtr ptr) { }
        // RVA: 0x0B38CB20  token: 0x60000B7
        public static System.IntPtr GetStaticMethodID(System.IntPtr clazz, System.String name, System.String sig) { }
        // RVA: 0x0B38C440  token: 0x60000B8
        public static System.IntPtr GetMethodID(System.IntPtr obj, System.String name, System.String sig) { }
        // RVA: 0x0B38C220  token: 0x60000B9
        public static System.IntPtr GetFieldID(System.IntPtr clazz, System.String name, System.String sig) { }
        // RVA: 0x0B38C900  token: 0x60000BA
        public static System.IntPtr GetStaticFieldID(System.IntPtr clazz, System.String name, System.String sig) { }
        // RVA: 0x0B38BE78  token: 0x60000BB
        public static System.IntPtr FromReflectedMethod(System.IntPtr refMethod) { }
        // RVA: 0x0B38BA58  token: 0x60000BC
        public static System.IntPtr FindClass(System.String name) { }
        // RVA: 0x0B38CECC  token: 0x60000BD
        public static System.IntPtr NewObject(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38CBA8  token: 0x60000BE
        public static System.IntPtr GetStaticObjectField(System.IntPtr clazz, System.IntPtr fieldID) { }
        // RVA: 0x0B38CD38  token: 0x60000BF
        public static System.String GetStaticStringField(System.IntPtr clazz, System.IntPtr fieldID) { }
        // RVA: 0x0B38C7E4  token: 0x60000C0
        public static System.Char GetStaticCharField(System.IntPtr clazz, System.IntPtr fieldID) { }
        // RVA: 0x0B38C86C  token: 0x60000C1
        public static System.Double GetStaticDoubleField(System.IntPtr clazz, System.IntPtr fieldID) { }
        // RVA: 0x0B38C988  token: 0x60000C2
        public static System.Single GetStaticFloatField(System.IntPtr clazz, System.IntPtr fieldID) { }
        // RVA: 0x0B38CA98  token: 0x60000C3
        public static System.Int64 GetStaticLongField(System.IntPtr clazz, System.IntPtr fieldID) { }
        // RVA: 0x0B38CCB0  token: 0x60000C4
        public static System.Int16 GetStaticShortField(System.IntPtr clazz, System.IntPtr fieldID) { }
        // RVA: 0x0B38CC30  token: 0x60000C5
        public static System.SByte GetStaticSByteField(System.IntPtr clazz, System.IntPtr fieldID) { }
        // RVA: 0x0B38C764  token: 0x60000C6
        public static System.Boolean GetStaticBooleanField(System.IntPtr clazz, System.IntPtr fieldID) { }
        // RVA: 0x0B38CA18  token: 0x60000C7
        public static System.Int32 GetStaticIntField(System.IntPtr clazz, System.IntPtr fieldID) { }
        // RVA: 0x0B38B6E0  token: 0x60000C8
        public static System.Void CallStaticVoidMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38B4C8  token: 0x60000C9
        public static System.IntPtr CallStaticObjectMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38B658  token: 0x60000CA
        public static System.String CallStaticStringMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38B214  token: 0x60000CB
        public static System.Char CallStaticCharMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38B29C  token: 0x60000CC
        public static System.Double CallStaticDoubleMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38B330  token: 0x60000CD
        public static System.Single CallStaticFloatMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38B440  token: 0x60000CE
        public static System.Int64 CallStaticLongMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38B5D0  token: 0x60000CF
        public static System.Int16 CallStaticShortMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38B550  token: 0x60000D0
        public static System.SByte CallStaticSByteMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38B194  token: 0x60000D1
        public static System.Boolean CallStaticBooleanMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38B3C0  token: 0x60000D2
        public static System.Int32 CallStaticIntMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38C5D4  token: 0x60000D3
        public static System.IntPtr GetObjectField(System.IntPtr obj, System.IntPtr fieldID) { }
        // RVA: 0x0B38CE44  token: 0x60000D4
        public static System.String GetStringField(System.IntPtr obj, System.IntPtr fieldID) { }
        // RVA: 0x0B38C104  token: 0x60000D5
        public static System.Char GetCharField(System.IntPtr obj, System.IntPtr fieldID) { }
        // RVA: 0x0B38C18C  token: 0x60000D6
        public static System.Double GetDoubleField(System.IntPtr obj, System.IntPtr fieldID) { }
        // RVA: 0x0B38C2A8  token: 0x60000D7
        public static System.Single GetFloatField(System.IntPtr obj, System.IntPtr fieldID) { }
        // RVA: 0x0B38C3B8  token: 0x60000D8
        public static System.Int64 GetLongField(System.IntPtr obj, System.IntPtr fieldID) { }
        // RVA: 0x0B38C6DC  token: 0x60000D9
        public static System.Int16 GetShortField(System.IntPtr obj, System.IntPtr fieldID) { }
        // RVA: 0x0B38C65C  token: 0x60000DA
        public static System.SByte GetSByteField(System.IntPtr obj, System.IntPtr fieldID) { }
        // RVA: 0x0B38C084  token: 0x60000DB
        public static System.Boolean GetBooleanField(System.IntPtr obj, System.IntPtr fieldID) { }
        // RVA: 0x0B38C338  token: 0x60000DC
        public static System.Int32 GetIntField(System.IntPtr obj, System.IntPtr fieldID) { }
        // RVA: 0x0B38B004  token: 0x60000DD
        public static System.IntPtr CallObjectMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38B750  token: 0x60000DE
        public static System.String CallStringMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38AD50  token: 0x60000DF
        public static System.Char CallCharMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38ADD8  token: 0x60000E0
        public static System.Double CallDoubleMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38AE6C  token: 0x60000E1
        public static System.Single CallFloatMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38AF7C  token: 0x60000E2
        public static System.Int64 CallLongMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38B10C  token: 0x60000E3
        public static System.Int16 CallShortMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38B08C  token: 0x60000E4
        public static System.SByte CallSByteMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38ACD0  token: 0x60000E5
        public static System.Boolean CallBooleanMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38AEFC  token: 0x60000E6
        public static System.Int32 CallIntMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args) { }
        // RVA: 0x0B38BBE4  token: 0x60000E7
        public static System.Char[] FromCharArray(System.IntPtr array) { }
        // RVA: 0x0B38BC68  token: 0x60000E8
        public static System.Double[] FromDoubleArray(System.IntPtr array) { }
        // RVA: 0x0B38BCEC  token: 0x60000E9
        public static System.Single[] FromFloatArray(System.IntPtr array) { }
        // RVA: 0x0B38BDF4  token: 0x60000EA
        public static System.Int64[] FromLongArray(System.IntPtr array) { }
        // RVA: 0x0B38BF80  token: 0x60000EB
        public static System.Int16[] FromShortArray(System.IntPtr array) { }
        // RVA: 0x0B38BB60  token: 0x60000EC
        public static System.Byte[] FromByteArray(System.IntPtr array) { }
        // RVA: 0x0B38BEFC  token: 0x60000ED
        public static System.SByte[] FromSByteArray(System.IntPtr array) { }
        // RVA: 0x0B38BADC  token: 0x60000EE
        public static System.Boolean[] FromBooleanArray(System.IntPtr array) { }
        // RVA: 0x0B38BD70  token: 0x60000EF
        public static System.Int32[] FromIntArray(System.IntPtr array) { }
        // RVA: 0x0B38D374  token: 0x60000F0
        public static System.IntPtr ToObjectArray(System.IntPtr[] array, System.IntPtr type) { }
        // RVA: 0x0B38D0E0  token: 0x60000F1
        public static System.IntPtr ToCharArray(System.Char[] array) { }
        // RVA: 0x0B38D164  token: 0x60000F2
        public static System.IntPtr ToDoubleArray(System.Double[] array) { }
        // RVA: 0x0B38D1E8  token: 0x60000F3
        public static System.IntPtr ToFloatArray(System.Single[] array) { }
        // RVA: 0x0B38D2F0  token: 0x60000F4
        public static System.IntPtr ToLongArray(System.Int64[] array) { }
        // RVA: 0x0B38D480  token: 0x60000F5
        public static System.IntPtr ToShortArray(System.Int16[] array) { }
        // RVA: 0x0B38D05C  token: 0x60000F6
        public static System.IntPtr ToByteArray(System.Byte[] array) { }
        // RVA: 0x0B38D3FC  token: 0x60000F7
        public static System.IntPtr ToSByteArray(System.SByte[] array) { }
        // RVA: 0x0B38CFD8  token: 0x60000F8
        public static System.IntPtr ToBooleanArray(System.Boolean[] array) { }
        // RVA: 0x0B38D26C  token: 0x60000F9
        public static System.IntPtr ToIntArray(System.Int32[] array) { }
        // RVA: 0x0B38C4C8  token: 0x60000FA
        public static System.IntPtr GetObjectArrayElement(System.IntPtr array, System.Int32 index) { }
        // RVA: 0x0B38C004  token: 0x60000FB
        public static System.Int32 GetArrayLength(System.IntPtr array) { }

    }

}

namespace UnityEngine.Android
{

    // TypeToken: 0x200000F  // size: 0x10
    public static class Common
    {
        // Fields
        private static UnityEngine.AndroidJavaObject m_Activity;  // static @ 0x0

        // Methods
        // RVA: 0x0B390D7C  token: 0x60000FC
        public static UnityEngine.AndroidJavaObject GetActivity() { }

    }

}

