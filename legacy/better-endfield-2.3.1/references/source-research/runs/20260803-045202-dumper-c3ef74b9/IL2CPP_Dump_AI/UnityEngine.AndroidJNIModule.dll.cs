// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.AndroidJNIModule.dll
// Classes:  15
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

CLASS: UnityEngine.AndroidJavaRunnable
TYPE:  sealed class
TOKEN: 0x2000002
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x053DEDC8  token=0x6000001  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x053DEDB8  token=0x6000002  System.Void Invoke()
END_CLASS

CLASS: UnityEngine.AndroidJavaException
TYPE:  sealed class
TOKEN: 0x2000003
SIZE:  0x98
EXTENDS: System.Exception
FIELDS:
  private           System.String                   mJavaStackTrace  // 0x90
PROPERTIES:
  StackTrace  get=0x0B38ED58
METHODS:
  RVA=0x0B38ECE8  token=0x6000003  System.Void .ctor(System.String message, System.String javaStackTrace)
END_CLASS

CLASS: UnityEngine.GlobalJavaObjectRef
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x20
FIELDS:
  private           System.Boolean                  m_disposed  // 0x10
  protected         System.IntPtr                   m_jobject  // 0x18
METHODS:
  RVA=0x0B390FBC  token=0x6000005  System.Void .ctor(System.IntPtr jobject)
  RVA=0x0B390F4C  token=0x6000006  System.Void Finalize()
  RVA=0x053E4DA0  token=0x6000007  System.IntPtr op_Implicit(UnityEngine.GlobalJavaObjectRef obj)
  RVA=0x0B390F24  token=0x6000008  System.Void Dispose()
END_CLASS

CLASS: UnityEngine.AndroidJavaRunnableProxy
TYPE:  class
TOKEN: 0x2000005
SIZE:  0x28
EXTENDS: UnityEngine.AndroidJavaProxy
FIELDS:
  private           UnityEngine.AndroidJavaRunnable mRunnable  // 0x20
METHODS:
  RVA=0x0B390134  token=0x6000009  System.Void .ctor(UnityEngine.AndroidJavaRunnable runnable)
END_CLASS

CLASS: UnityEngine.AndroidJavaProxy
TYPE:  class
TOKEN: 0x2000006
SIZE:  0x20
FIELDS:
  public    readonly UnityEngine.AndroidJavaClass    javaInterface  // 0x10
  private           System.IntPtr                   proxyObject  // 0x18
  private   static readonly UnityEngine.GlobalJavaObjectRef s_JavaLangSystemClass  // static @ 0x0
  private   static readonly System.IntPtr                   s_HashCodeMethodID  // static @ 0x8
METHODS:
  RVA=0x0B38FF64  token=0x600000A  System.Void .ctor(System.String javaInterface)
  RVA=0x0B38FFDC  token=0x600000B  System.Void .ctor(UnityEngine.AndroidJavaClass javaInterface)
  RVA=0x0B38F450  token=0x600000C  System.Void Finalize()
  RVA=0x0B38F540  token=0x600000D  UnityEngine.AndroidJavaObject Invoke(System.String methodName, System.Object[] args)
  RVA=0x0B38FD40  token=0x600000E  UnityEngine.AndroidJavaObject Invoke(System.String methodName, UnityEngine.AndroidJavaObject[] javaArgs)
  RVA=0x0B38FFF4  token=0x600000F  System.Boolean equals(UnityEngine.AndroidJavaObject obj)
  RVA=0x0B390028  token=0x6000010  System.Int32 hashCode()
  RVA=0x0B3900D8  token=0x6000011  System.String toString()
  RVA=0x0B38F4C4  token=0x6000012  UnityEngine.AndroidJavaObject GetProxyObject()
  RVA=0x0B38F4E0  token=0x6000013  System.IntPtr GetRawProxy()
  RVA=0x0B38FE58  token=0x6000014  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.AndroidJavaObject
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x20
IMPLEMENTS: System.IDisposable
FIELDS:
  private   static  System.Boolean                  enableDebugPrints  // static @ 0x0
  private           UnityEngine.GlobalJavaObjectRef m_jobject  // 0x10
  private           UnityEngine.GlobalJavaObjectRef m_jclass  // 0x18
METHODS:
  RVA=0x0B38F448  token=0x6000015  System.Void .ctor(System.String className, System.Object[] args)
  RVA=0x0B38EF8C  token=0x6000016  System.Void Dispose()
  RVA=-1  // generic def  token=0x6000017  FieldType Get(System.String fieldName)
  RVA=-1  // generic def  token=0x6000018  FieldType GetStatic(System.String fieldName)
  RVA=0x0B38F0AC  token=0x6000019  System.IntPtr GetRawObject()
  RVA=0x0B38F0A0  token=0x600001A  System.IntPtr GetRawClass()
  RVA=-1  // generic def  token=0x600001B  ReturnType Call(System.String methodName, System.Object[] args)
  RVA=-1  // generic def  token=0x600001C  ReturnType CallStatic(System.String methodName, System.Object[] args)
  RVA=0x0B38EF2C  token=0x600001D  System.Void DebugPrint(System.String msg)
  RVA=0x0B38F0B8  token=0x600001E  System.Void _AndroidJavaObject(System.String className, System.Object[] args)
  RVA=0x0B38F328  token=0x600001F  System.Void .ctor(System.IntPtr jobject)
  RVA=0x041E1670  token=0x6000020  System.Void .ctor()
  RVA=0x0B38F02C  token=0x6000021  System.Void Finalize()
  RVA=0x0B38EFDC  token=0x6000022  System.Void Dispose(System.Boolean disposing)
  RVA=-1  // generic def  token=0x6000023  ReturnType _Call(System.String methodName, System.Object[] args)
  RVA=-1  // generic def  token=0x6000024  FieldType _Get(System.String fieldName)
  RVA=-1  // generic def  token=0x6000025  ReturnType _CallStatic(System.String methodName, System.Object[] args)
  RVA=-1  // generic def  token=0x6000026  FieldType _GetStatic(System.String fieldName)
  RVA=0x0B38EE58  token=0x6000027  UnityEngine.AndroidJavaObject AndroidJavaObjectDeleteLocalRef(System.IntPtr jobject)
  RVA=0x0B38ED84  token=0x6000028  UnityEngine.AndroidJavaClass AndroidJavaClassDeleteLocalRef(System.IntPtr jclass)
  RVA=-1  // generic def  token=0x6000029  ReturnType FromJavaArrayDeleteLocalRef(System.IntPtr jobject)
  RVA=0x0B38F314  token=0x600002A  System.IntPtr _GetRawObject()
  RVA=0x0A8DA498  token=0x600002B  System.IntPtr _GetRawClass()
END_CLASS

CLASS: UnityEngine.AndroidJavaClass
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x20
EXTENDS: UnityEngine.AndroidJavaObject
FIELDS:
METHODS:
  RVA=0x0B38ECE0  token=0x600002C  System.Void .ctor(System.String className)
  RVA=0x0B38EB28  token=0x600002D  System.Void _AndroidJavaClass(System.String className)
  RVA=0x0B38EC08  token=0x600002E  System.Void .ctor(System.IntPtr jclass)
END_CLASS

CLASS: UnityEngine.AndroidReflection
TYPE:  class
TOKEN: 0x2000009
SIZE:  0x10
FIELDS:
  private   static readonly UnityEngine.GlobalJavaObjectRef s_ReflectionHelperClass  // static @ 0x0
  private   static readonly System.IntPtr                   s_ReflectionHelperGetConstructorID  // static @ 0x8
  private   static readonly System.IntPtr                   s_ReflectionHelperGetMethodID  // static @ 0x10
  private   static readonly System.IntPtr                   s_ReflectionHelperGetFieldID  // static @ 0x18
  private   static readonly System.IntPtr                   s_ReflectionHelperGetFieldSignature  // static @ 0x20
  private   static readonly System.IntPtr                   s_ReflectionHelperNewProxyInstance  // static @ 0x28
  private   static readonly System.IntPtr                   s_ReflectionHelperSetNativeExceptionOnProxy  // static @ 0x30
  private   static readonly System.IntPtr                   s_FieldGetDeclaringClass  // static @ 0x38
METHODS:
  RVA=0x0AAAB0A8  token=0x600002F  System.Boolean IsPrimitive(System.Type t)
  RVA=0x0AC76A90  token=0x6000030  System.Boolean IsAssignableFrom(System.Type t, System.Type from)
  RVA=0x0B390860  token=0x6000031  System.IntPtr GetStaticMethodID(System.String clazz, System.String methodName, System.String signature)
  RVA=0x0B3905E0  token=0x6000032  System.IntPtr GetMethodID(System.String clazz, System.String methodName, System.String signature)
  RVA=0x0B3901A4  token=0x6000033  System.IntPtr GetConstructorMember(System.IntPtr jclass, System.String signature)
  RVA=0x0B39068C  token=0x6000034  System.IntPtr GetMethodMember(System.IntPtr jclass, System.String methodName, System.String signature, System.Boolean isStatic)
  RVA=0x0B390364  token=0x6000035  System.IntPtr GetFieldMember(System.IntPtr jclass, System.String fieldName, System.String signature, System.Boolean isStatic)
  RVA=0x0B39030C  token=0x6000036  System.IntPtr GetFieldClass(System.IntPtr field)
  RVA=0x0B390538  token=0x6000037  System.String GetFieldSignature(System.IntPtr field)
  RVA=0x0B39090C  token=0x6000038  System.IntPtr NewProxyInstance(System.IntPtr player, System.IntPtr delegateHandle, System.IntPtr interfaze)
  RVA=0x0B3909E8  token=0x6000039  System.Void SetNativeExceptionOnProxy(System.IntPtr proxy, System.Exception e, System.Boolean methodNotFound)
  RVA=0x0B390AD4  token=0x600003A  System.Void .cctor()
END_CLASS

CLASS: UnityEngine._AndroidJNIHelper
TYPE:  sealed class
TOKEN: 0x200000A
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B392590  token=0x600003B  System.IntPtr CreateJavaProxy(System.IntPtr player, System.IntPtr delegateHandle, UnityEngine.AndroidJavaProxy proxy)
  RVA=0x0B392610  token=0x600003C  System.IntPtr CreateJavaRunnable(UnityEngine.AndroidJavaRunnable jrunnable)
  RVA=0x0B3939E4  token=0x600003D  System.IntPtr InvokeJavaProxyMethod(UnityEngine.AndroidJavaProxy proxy, System.IntPtr jmethodName, System.IntPtr jargs)
  RVA=0x0B391FFC  token=0x600003E  UnityEngine.jvalue[] CreateJNIArgArray(System.Object[] args)
  RVA=0x0B393C80  token=0x600003F  System.Object UnboxArray(UnityEngine.AndroidJavaObject obj)
  RVA=0x0B394268  token=0x6000040  System.Object Unbox(UnityEngine.AndroidJavaObject obj)
  RVA=0x0B390FE8  token=0x6000041  UnityEngine.AndroidJavaObject Box(System.Object obj)
  RVA=0x0B392678  token=0x6000042  System.Void DeleteJNIArgArray(System.Object[] args, UnityEngine.jvalue[] jniArgs)
  RVA=0x0B39192C  token=0x6000043  System.IntPtr ConvertToJNIArray(System.Array array)
  RVA=-1  // generic def  token=0x6000044  ArrayType ConvertFromJNIArray(System.IntPtr array)
  RVA=0x0B392918  token=0x6000045  System.IntPtr GetConstructorID(System.IntPtr jclass, System.Object[] args)
  RVA=-1  // generic def  token=0x6000046  System.IntPtr GetMethodID(System.IntPtr jclass, System.String methodName, System.Object[] args, System.Boolean isStatic)
  RVA=-1  // generic def  token=0x6000047  System.IntPtr GetFieldID(System.IntPtr jclass, System.String fieldName, System.Boolean isStatic)
  RVA=0x0B39276C  token=0x6000048  System.IntPtr GetConstructorID(System.IntPtr jclass, System.String signature)
  RVA=0x0B392C3C  token=0x6000049  System.IntPtr GetMethodID(System.IntPtr jclass, System.String methodName, System.String signature, System.Boolean isStatic)
  RVA=0x0B392BB4  token=0x600004A  System.IntPtr GetMethodIDFallback(System.IntPtr jclass, System.String methodName, System.String signature, System.Boolean isStatic)
  RVA=0x0B392944  token=0x600004B  System.IntPtr GetFieldID(System.IntPtr jclass, System.String fieldName, System.String signature, System.Boolean isStatic)
  RVA=0x0B392ECC  token=0x600004C  System.String GetSignature(System.Object obj)
  RVA=0x0B392DFC  token=0x600004D  System.String GetSignature(System.Object[] args)
  RVA=-1  // generic def  token=0x600004E  System.String GetSignature(System.Object[] args)
END_CLASS

CLASS: UnityEngine.jvalue
TYPE:  sealed struct
TOKEN: 0x200000B
SIZE:  0x18
FIELDS:
  public            System.Boolean                  z  // 0x10
  public            System.SByte                    b  // 0x10
  public            System.Char                     c  // 0x10
  public            System.Int16                    s  // 0x10
  public            System.Int32                    i  // 0x10
  public            System.Int64                    j  // 0x10
  public            System.Single                   f  // 0x10
  public            System.Double                   d  // 0x10
  public            System.IntPtr                   l  // 0x10
METHODS:
END_CLASS

CLASS: UnityEngine.AndroidJNIHelper
TYPE:  static class
TOKEN: 0x200000C
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B38ACA4  token=0x600004F  System.IntPtr GetConstructorID(System.IntPtr javaClass, System.String signature)
  RVA=0x0B38ACC0  token=0x6000050  System.IntPtr GetMethodID(System.IntPtr javaClass, System.String methodName, System.String signature, System.Boolean isStatic)
  RVA=0x0B38ACB0  token=0x6000051  System.IntPtr GetFieldID(System.IntPtr javaClass, System.String fieldName, System.String signature, System.Boolean isStatic)
  RVA=0x0B38AC84  token=0x6000052  System.IntPtr CreateJavaRunnable(UnityEngine.AndroidJavaRunnable jrunnable)
  RVA=0x0B38AB70  token=0x6000053  System.IntPtr CreateJavaProxy(UnityEngine.AndroidJavaProxy proxy)
  RVA=0x0B38AB64  token=0x6000054  UnityEngine.jvalue[] CreateJNIArgArray(System.Object[] args)
  RVA=0x0B38AC90  token=0x6000055  System.Void DeleteJNIArgArray(System.Object[] args, UnityEngine.jvalue[] jniArgs)
  RVA=0x0B38AC98  token=0x6000056  System.IntPtr GetConstructorID(System.IntPtr jclass, System.Object[] args)
  RVA=-1  // generic def  token=0x6000057  ArrayType ConvertFromJNIArray(System.IntPtr array)
  RVA=-1  // generic def  token=0x6000058  System.IntPtr GetMethodID(System.IntPtr jclass, System.String methodName, System.Object[] args, System.Boolean isStatic)
  RVA=-1  // generic def  token=0x6000059  System.IntPtr GetFieldID(System.IntPtr jclass, System.String fieldName, System.Boolean isStatic)
END_CLASS

CLASS: UnityEngine.AndroidJNI
TYPE:  static class
TOKEN: 0x200000D
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B38DCDC  token=0x600005A  System.IntPtr FindClass(System.String name)
  RVA=0x0B38DE7C  token=0x600005B  System.IntPtr FromReflectedMethod(System.IntPtr refMethod)
  RVA=0x0B38DCB0  token=0x600005C  System.IntPtr ExceptionOccurred()
  RVA=0x0B38DC84  token=0x600005D  System.Void ExceptionClear()
  RVA=0x0B38E88C  token=0x600005E  System.Int32 PushLocalFrame(System.Int32 capacity)
  RVA=0x0B38E858  token=0x600005F  System.IntPtr PopLocalFrame(System.IntPtr ptr)
  RVA=0x0B38E6D8  token=0x6000060  System.IntPtr NewGlobalRef(System.IntPtr obj)
  RVA=0x0B38DBE8  token=0x6000061  System.Void DeleteGlobalRef(System.IntPtr obj)
  RVA=0x0B38E824  token=0x6000062  System.IntPtr NewWeakGlobalRef(System.IntPtr obj)
  RVA=0x0B38DC50  token=0x6000063  System.Void DeleteWeakGlobalRef(System.IntPtr obj)
  RVA=0x0B38E70C  token=0x6000064  System.IntPtr NewLocalRef(System.IntPtr obj)
  RVA=0x0B38DC1C  token=0x6000065  System.Void DeleteLocalRef(System.IntPtr obj)
  RVA=0x0B38E694  token=0x6000066  System.Boolean IsSameObject(System.IntPtr obj1, System.IntPtr obj2)
  RVA=0x0B38E790  token=0x6000067  System.IntPtr NewObject(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38E1CC  token=0x6000068  System.IntPtr GetObjectClass(System.IntPtr obj)
  RVA=0x0B38E138  token=0x6000069  System.IntPtr GetMethodID(System.IntPtr clazz, System.String name, System.String sig)
  RVA=0x0B38E018  token=0x600006A  System.IntPtr GetFieldID(System.IntPtr clazz, System.String name, System.String sig)
  RVA=0x0B38E4B8  token=0x600006B  System.IntPtr GetStaticMethodID(System.IntPtr clazz, System.String name, System.String sig)
  RVA=0x0B38E398  token=0x600006C  System.IntPtr GetStaticFieldID(System.IntPtr clazz, System.String name, System.String sig)
  RVA=0x0B38E818  token=0x600006D  System.IntPtr NewString(System.String chars)
  RVA=0x0B38E7E4  token=0x600006E  System.IntPtr NewStringFromStr(System.String chars)
  RVA=0x0B38E61C  token=0x600006F  System.String GetStringChars(System.IntPtr str)
  RVA=0x0B38DB94  token=0x6000070  System.String CallStringMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38D6FC  token=0x6000071  System.IntPtr CallObjectMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38D654  token=0x6000072  System.Int32 CallIntMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38D504  token=0x6000073  System.Boolean CallBooleanMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38D7A4  token=0x6000074  System.Int16 CallShortMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38D750  token=0x6000075  System.SByte CallSByteMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38D558  token=0x6000076  System.Char CallCharMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38D600  token=0x6000077  System.Single CallFloatMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38D5AC  token=0x6000078  System.Double CallDoubleMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38D6A8  token=0x6000079  System.Int64 CallLongMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38E650  token=0x600007A  System.String GetStringField(System.IntPtr obj, System.IntPtr fieldID)
  RVA=0x0B38E200  token=0x600007B  System.IntPtr GetObjectField(System.IntPtr obj, System.IntPtr fieldID)
  RVA=0x0B38DF4C  token=0x600007C  System.Boolean GetBooleanField(System.IntPtr obj, System.IntPtr fieldID)
  RVA=0x0B38E244  token=0x600007D  System.SByte GetSByteField(System.IntPtr obj, System.IntPtr fieldID)
  RVA=0x0B38DF90  token=0x600007E  System.Char GetCharField(System.IntPtr obj, System.IntPtr fieldID)
  RVA=0x0B38E288  token=0x600007F  System.Int16 GetShortField(System.IntPtr obj, System.IntPtr fieldID)
  RVA=0x0B38E0B0  token=0x6000080  System.Int32 GetIntField(System.IntPtr obj, System.IntPtr fieldID)
  RVA=0x0B38E0F4  token=0x6000081  System.Int64 GetLongField(System.IntPtr obj, System.IntPtr fieldID)
  RVA=0x0B38E06C  token=0x6000082  System.Single GetFloatField(System.IntPtr obj, System.IntPtr fieldID)
  RVA=0x0B38DFD4  token=0x6000083  System.Double GetDoubleField(System.IntPtr obj, System.IntPtr fieldID)
  RVA=0x0B38DAEC  token=0x6000084  System.String CallStaticStringMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38D9F0  token=0x6000085  System.IntPtr CallStaticObjectMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38D948  token=0x6000086  System.Int32 CallStaticIntMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38D7F8  token=0x6000087  System.Boolean CallStaticBooleanMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38DA98  token=0x6000088  System.Int16 CallStaticShortMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38DA44  token=0x6000089  System.SByte CallStaticSByteMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38D84C  token=0x600008A  System.Char CallStaticCharMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38D8F4  token=0x600008B  System.Single CallStaticFloatMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38D8A0  token=0x600008C  System.Double CallStaticDoubleMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38D99C  token=0x600008D  System.Int64 CallStaticLongMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38DB40  token=0x600008E  System.Void CallStaticVoidMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38E5D8  token=0x600008F  System.String GetStaticStringField(System.IntPtr clazz, System.IntPtr fieldID)
  RVA=0x0B38E50C  token=0x6000090  System.IntPtr GetStaticObjectField(System.IntPtr clazz, System.IntPtr fieldID)
  RVA=0x0B38E2CC  token=0x6000091  System.Boolean GetStaticBooleanField(System.IntPtr clazz, System.IntPtr fieldID)
  RVA=0x0B38E550  token=0x6000092  System.SByte GetStaticSByteField(System.IntPtr clazz, System.IntPtr fieldID)
  RVA=0x0B38E310  token=0x6000093  System.Char GetStaticCharField(System.IntPtr clazz, System.IntPtr fieldID)
  RVA=0x0B38E594  token=0x6000094  System.Int16 GetStaticShortField(System.IntPtr clazz, System.IntPtr fieldID)
  RVA=0x0B38E430  token=0x6000095  System.Int32 GetStaticIntField(System.IntPtr clazz, System.IntPtr fieldID)
  RVA=0x0B38E474  token=0x6000096  System.Int64 GetStaticLongField(System.IntPtr clazz, System.IntPtr fieldID)
  RVA=0x0B38E3EC  token=0x6000097  System.Single GetStaticFloatField(System.IntPtr clazz, System.IntPtr fieldID)
  RVA=0x0B38E354  token=0x6000098  System.Double GetStaticDoubleField(System.IntPtr clazz, System.IntPtr fieldID)
  RVA=0x0B38E910  token=0x6000099  System.IntPtr ToBooleanArray(System.Boolean[] array)
  RVA=0x0B38E944  token=0x600009A  System.IntPtr ToByteArray(System.Byte[] array)
  RVA=0x0B38EAC0  token=0x600009B  System.IntPtr ToSByteArray(System.SByte[] array)
  RVA=0x0B38E978  token=0x600009C  System.IntPtr ToCharArray(System.Char[] array)
  RVA=0x0B38EAF4  token=0x600009D  System.IntPtr ToShortArray(System.Int16[] array)
  RVA=0x0B38EA14  token=0x600009E  System.IntPtr ToIntArray(System.Int32[] array)
  RVA=0x0B38EA48  token=0x600009F  System.IntPtr ToLongArray(System.Int64[] array)
  RVA=0x0B38E9E0  token=0x60000A0  System.IntPtr ToFloatArray(System.Single[] array)
  RVA=0x0B38E9AC  token=0x60000A1  System.IntPtr ToDoubleArray(System.Double[] array)
  RVA=0x0B38EA7C  token=0x60000A2  System.IntPtr ToObjectArray(System.IntPtr[] array, System.IntPtr arrayClass)
  RVA=0x0B38DD10  token=0x60000A3  System.Boolean[] FromBooleanArray(System.IntPtr array)
  RVA=0x0B38DD44  token=0x60000A4  System.Byte[] FromByteArray(System.IntPtr array)
  RVA=0x0B38DEB0  token=0x60000A5  System.SByte[] FromSByteArray(System.IntPtr array)
  RVA=0x0B38DD78  token=0x60000A6  System.Char[] FromCharArray(System.IntPtr array)
  RVA=0x0B38DEE4  token=0x60000A7  System.Int16[] FromShortArray(System.IntPtr array)
  RVA=0x0B38DE14  token=0x60000A8  System.Int32[] FromIntArray(System.IntPtr array)
  RVA=0x0B38DE48  token=0x60000A9  System.Int64[] FromLongArray(System.IntPtr array)
  RVA=0x0B38DDE0  token=0x60000AA  System.Single[] FromFloatArray(System.IntPtr array)
  RVA=0x0B38DDAC  token=0x60000AB  System.Double[] FromDoubleArray(System.IntPtr array)
  RVA=0x0B38DF18  token=0x60000AC  System.Int32 GetArrayLength(System.IntPtr array)
  RVA=0x0B38E740  token=0x60000AD  System.IntPtr NewObjectArray(System.Int32 size, System.IntPtr clazz, System.IntPtr obj)
  RVA=0x0B38E18C  token=0x60000AE  System.IntPtr GetObjectArrayElement(System.IntPtr array, System.Int32 index)
  RVA=0x0B38E8C0  token=0x60000AF  System.Void SetObjectArrayElement(System.IntPtr array, System.Int32 index, System.IntPtr obj)
END_CLASS

CLASS: UnityEngine.AndroidJNISafe
TYPE:  class
TOKEN: 0x200000E
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B38B7D8  token=0x60000B0  System.Void CheckException()
  RVA=0x0B38BA28  token=0x60000B1  System.Void DeleteGlobalRef(System.IntPtr globalref)
  RVA=0x0B38BA48  token=0x60000B2  System.Void DeleteWeakGlobalRef(System.IntPtr globalref)
  RVA=0x0B38BA38  token=0x60000B3  System.Void DeleteLocalRef(System.IntPtr localref)
  RVA=0x0B38CF54  token=0x60000B4  System.IntPtr NewString(System.String chars)
  RVA=0x0B38CDC0  token=0x60000B5  System.String GetStringChars(System.IntPtr str)
  RVA=0x0B38C550  token=0x60000B6  System.IntPtr GetObjectClass(System.IntPtr ptr)
  RVA=0x0B38CB20  token=0x60000B7  System.IntPtr GetStaticMethodID(System.IntPtr clazz, System.String name, System.String sig)
  RVA=0x0B38C440  token=0x60000B8  System.IntPtr GetMethodID(System.IntPtr obj, System.String name, System.String sig)
  RVA=0x0B38C220  token=0x60000B9  System.IntPtr GetFieldID(System.IntPtr clazz, System.String name, System.String sig)
  RVA=0x0B38C900  token=0x60000BA  System.IntPtr GetStaticFieldID(System.IntPtr clazz, System.String name, System.String sig)
  RVA=0x0B38BE78  token=0x60000BB  System.IntPtr FromReflectedMethod(System.IntPtr refMethod)
  RVA=0x0B38BA58  token=0x60000BC  System.IntPtr FindClass(System.String name)
  RVA=0x0B38CECC  token=0x60000BD  System.IntPtr NewObject(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38CBA8  token=0x60000BE  System.IntPtr GetStaticObjectField(System.IntPtr clazz, System.IntPtr fieldID)
  RVA=0x0B38CD38  token=0x60000BF  System.String GetStaticStringField(System.IntPtr clazz, System.IntPtr fieldID)
  RVA=0x0B38C7E4  token=0x60000C0  System.Char GetStaticCharField(System.IntPtr clazz, System.IntPtr fieldID)
  RVA=0x0B38C86C  token=0x60000C1  System.Double GetStaticDoubleField(System.IntPtr clazz, System.IntPtr fieldID)
  RVA=0x0B38C988  token=0x60000C2  System.Single GetStaticFloatField(System.IntPtr clazz, System.IntPtr fieldID)
  RVA=0x0B38CA98  token=0x60000C3  System.Int64 GetStaticLongField(System.IntPtr clazz, System.IntPtr fieldID)
  RVA=0x0B38CCB0  token=0x60000C4  System.Int16 GetStaticShortField(System.IntPtr clazz, System.IntPtr fieldID)
  RVA=0x0B38CC30  token=0x60000C5  System.SByte GetStaticSByteField(System.IntPtr clazz, System.IntPtr fieldID)
  RVA=0x0B38C764  token=0x60000C6  System.Boolean GetStaticBooleanField(System.IntPtr clazz, System.IntPtr fieldID)
  RVA=0x0B38CA18  token=0x60000C7  System.Int32 GetStaticIntField(System.IntPtr clazz, System.IntPtr fieldID)
  RVA=0x0B38B6E0  token=0x60000C8  System.Void CallStaticVoidMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38B4C8  token=0x60000C9  System.IntPtr CallStaticObjectMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38B658  token=0x60000CA  System.String CallStaticStringMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38B214  token=0x60000CB  System.Char CallStaticCharMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38B29C  token=0x60000CC  System.Double CallStaticDoubleMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38B330  token=0x60000CD  System.Single CallStaticFloatMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38B440  token=0x60000CE  System.Int64 CallStaticLongMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38B5D0  token=0x60000CF  System.Int16 CallStaticShortMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38B550  token=0x60000D0  System.SByte CallStaticSByteMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38B194  token=0x60000D1  System.Boolean CallStaticBooleanMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38B3C0  token=0x60000D2  System.Int32 CallStaticIntMethod(System.IntPtr clazz, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38C5D4  token=0x60000D3  System.IntPtr GetObjectField(System.IntPtr obj, System.IntPtr fieldID)
  RVA=0x0B38CE44  token=0x60000D4  System.String GetStringField(System.IntPtr obj, System.IntPtr fieldID)
  RVA=0x0B38C104  token=0x60000D5  System.Char GetCharField(System.IntPtr obj, System.IntPtr fieldID)
  RVA=0x0B38C18C  token=0x60000D6  System.Double GetDoubleField(System.IntPtr obj, System.IntPtr fieldID)
  RVA=0x0B38C2A8  token=0x60000D7  System.Single GetFloatField(System.IntPtr obj, System.IntPtr fieldID)
  RVA=0x0B38C3B8  token=0x60000D8  System.Int64 GetLongField(System.IntPtr obj, System.IntPtr fieldID)
  RVA=0x0B38C6DC  token=0x60000D9  System.Int16 GetShortField(System.IntPtr obj, System.IntPtr fieldID)
  RVA=0x0B38C65C  token=0x60000DA  System.SByte GetSByteField(System.IntPtr obj, System.IntPtr fieldID)
  RVA=0x0B38C084  token=0x60000DB  System.Boolean GetBooleanField(System.IntPtr obj, System.IntPtr fieldID)
  RVA=0x0B38C338  token=0x60000DC  System.Int32 GetIntField(System.IntPtr obj, System.IntPtr fieldID)
  RVA=0x0B38B004  token=0x60000DD  System.IntPtr CallObjectMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38B750  token=0x60000DE  System.String CallStringMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38AD50  token=0x60000DF  System.Char CallCharMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38ADD8  token=0x60000E0  System.Double CallDoubleMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38AE6C  token=0x60000E1  System.Single CallFloatMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38AF7C  token=0x60000E2  System.Int64 CallLongMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38B10C  token=0x60000E3  System.Int16 CallShortMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38B08C  token=0x60000E4  System.SByte CallSByteMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38ACD0  token=0x60000E5  System.Boolean CallBooleanMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38AEFC  token=0x60000E6  System.Int32 CallIntMethod(System.IntPtr obj, System.IntPtr methodID, UnityEngine.jvalue[] args)
  RVA=0x0B38BBE4  token=0x60000E7  System.Char[] FromCharArray(System.IntPtr array)
  RVA=0x0B38BC68  token=0x60000E8  System.Double[] FromDoubleArray(System.IntPtr array)
  RVA=0x0B38BCEC  token=0x60000E9  System.Single[] FromFloatArray(System.IntPtr array)
  RVA=0x0B38BDF4  token=0x60000EA  System.Int64[] FromLongArray(System.IntPtr array)
  RVA=0x0B38BF80  token=0x60000EB  System.Int16[] FromShortArray(System.IntPtr array)
  RVA=0x0B38BB60  token=0x60000EC  System.Byte[] FromByteArray(System.IntPtr array)
  RVA=0x0B38BEFC  token=0x60000ED  System.SByte[] FromSByteArray(System.IntPtr array)
  RVA=0x0B38BADC  token=0x60000EE  System.Boolean[] FromBooleanArray(System.IntPtr array)
  RVA=0x0B38BD70  token=0x60000EF  System.Int32[] FromIntArray(System.IntPtr array)
  RVA=0x0B38D374  token=0x60000F0  System.IntPtr ToObjectArray(System.IntPtr[] array, System.IntPtr type)
  RVA=0x0B38D0E0  token=0x60000F1  System.IntPtr ToCharArray(System.Char[] array)
  RVA=0x0B38D164  token=0x60000F2  System.IntPtr ToDoubleArray(System.Double[] array)
  RVA=0x0B38D1E8  token=0x60000F3  System.IntPtr ToFloatArray(System.Single[] array)
  RVA=0x0B38D2F0  token=0x60000F4  System.IntPtr ToLongArray(System.Int64[] array)
  RVA=0x0B38D480  token=0x60000F5  System.IntPtr ToShortArray(System.Int16[] array)
  RVA=0x0B38D05C  token=0x60000F6  System.IntPtr ToByteArray(System.Byte[] array)
  RVA=0x0B38D3FC  token=0x60000F7  System.IntPtr ToSByteArray(System.SByte[] array)
  RVA=0x0B38CFD8  token=0x60000F8  System.IntPtr ToBooleanArray(System.Boolean[] array)
  RVA=0x0B38D26C  token=0x60000F9  System.IntPtr ToIntArray(System.Int32[] array)
  RVA=0x0B38C4C8  token=0x60000FA  System.IntPtr GetObjectArrayElement(System.IntPtr array, System.Int32 index)
  RVA=0x0B38C004  token=0x60000FB  System.Int32 GetArrayLength(System.IntPtr array)
END_CLASS

CLASS: UnityEngine.Android.Common
TYPE:  static class
TOKEN: 0x200000F
SIZE:  0x10
FIELDS:
  private   static  UnityEngine.AndroidJavaObject   m_Activity  // static @ 0x0
METHODS:
  RVA=0x0B390D7C  token=0x60000FC  UnityEngine.AndroidJavaObject GetActivity()
END_CLASS

