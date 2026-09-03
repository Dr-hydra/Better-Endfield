// ========================================================
// Dumped by @desirepro
// Assembly: IFixRuntime.Beyond.dll
// Classes:  27
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

CLASS: WrapperArrayCandidate
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x40
FIELDS:
  public            System.String                   Source  // 0x10
  public            System.String                   OwnerTypeName  // 0x18
  public            System.String                   FieldName  // 0x20
  public            System.String                   ElementTypeName  // 0x28
  public            System.Int32                    Length  // 0x30
  public            System.Int32                    NonNullCount  // 0x34
  public            System.Func<System.Int32,System.Object>Getter  // 0x38
METHODS:
  RVA=0x09E26814  token=0x600001F  System.Object GetValue(System.Int32 index)
  RVA=0x041E1670  token=0x6000020  System.Void .ctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000005
SIZE:  0x10
FIELDS:
  public    static readonly IFix.IFixMethodPrinter.<>c      <>9  // static @ 0x0
  public    static  System.Func<System.Collections.Generic.KeyValuePair<System.Int32,System.String>,System.Int32><>9__7_0  // static @ 0x8
METHODS:
  RVA=0x09E266EC  token=0x6000021  System.Void .cctor()
  RVA=0x041E1670  token=0x6000022  System.Void .ctor()
  RVA=0x09E26678  token=0x6000023  System.Int32 <GetAllIFixMethodStrs>b__7_0(System.Collections.Generic.KeyValuePair<System.Int32,System.String> k)
END_CLASS

CLASS: <>c__DisplayClass17_0
TYPE:  sealed class
TOKEN: 0x2000006
SIZE:  0x18
FIELDS:
  public            System.Array                    arr  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000024  System.Void .ctor()
  RVA=0x09E266AC  token=0x6000025  System.Object <TryGetWrapperArrayDirect>b__0(System.Int32 i)
END_CLASS

CLASS: <>c__DisplayClass26_0
TYPE:  sealed class
TOKEN: 0x2000007
SIZE:  0x20
FIELDS:
  public            System.Array                    arr  // 0x10
  public            System.Collections.IList        listValue  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000026  System.Void .ctor()
  RVA=0x09E266AC  token=0x6000027  System.Object <AddWrapperCandidate>b__0(System.Int32 i)
  RVA=0x09E266CC  token=0x6000028  System.Object <AddWrapperCandidate>b__1(System.Int32 i)
END_CLASS

CLASS: <GetAllFieldsIncludingBase>d__29
TYPE:  sealed class
TOKEN: 0x2000008
SIZE:  0x58
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Reflection.FieldInfo     <>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x20
  private           System.Type                     type  // 0x28
  public            System.Type                     <>3__type  // 0x30
  private           System.Reflection.BindingFlags  flags  // 0x38
  public            System.Reflection.BindingFlags  <>3__flags  // 0x3c
  private           System.Type                     <t>5__2  // 0x40
  private           System.Reflection.FieldInfo[]   <>7__wrap2  // 0x48
  private           System.Int32                    <>7__wrap3  // 0x50
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Reflection.FieldInfo>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x03A11010  token=0x6000029  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x600002A  System.Void System.IDisposable.Dispose()
  RVA=0x09E26238  token=0x600002B  System.Boolean MoveNext()
  RVA=0x09E2640C  token=0x600002D  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x09E26368  token=0x600002F  System.Collections.Generic.IEnumerator<System.Reflection.FieldInfo> System.Collections.Generic.IEnumerable<System.Reflection.FieldInfo>.GetEnumerator()
  RVA=0x09E26404  token=0x6000030  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <GetAllPropertiesIncludingBase>d__30
TYPE:  sealed class
TOKEN: 0x2000009
SIZE:  0x58
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Reflection.PropertyInfo  <>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x20
  private           System.Type                     type  // 0x28
  public            System.Type                     <>3__type  // 0x30
  private           System.Reflection.BindingFlags  flags  // 0x38
  public            System.Reflection.BindingFlags  <>3__flags  // 0x3c
  private           System.Type                     <t>5__2  // 0x40
  private           System.Reflection.PropertyInfo[]<>7__wrap2  // 0x48
  private           System.Int32                    <>7__wrap3  // 0x50
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Reflection.PropertyInfo>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x03A11010  token=0x6000031  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000032  System.Void System.IDisposable.Dispose()
  RVA=0x09E26458  token=0x6000033  System.Boolean MoveNext()
  RVA=0x09E2662C  token=0x6000035  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x09E26588  token=0x6000037  System.Collections.Generic.IEnumerator<System.Reflection.PropertyInfo> System.Collections.Generic.IEnumerable<System.Reflection.PropertyInfo>.GetEnumerator()
  RVA=0x09E26624  token=0x6000038  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <_DownloadAndApplyCoroutine>d__11
TYPE:  sealed class
TOKEN: 0x2000011
SIZE:  0x48
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            System.String                   url  // 0x20
  private           System.String                   <text>5__2  // 0x28
  private           System.String                   <lastError>5__3  // 0x30
  private           UnityEngine.Networking.UnityWebRequest.Result<lastResult>5__4  // 0x38
  private           System.Int32                    <attempt>5__5  // 0x3c
  private           UnityEngine.Networking.UnityWebRequest<req>5__6  // 0x40
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x6000049  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x09E2679C  token=0x600004A  System.Void System.IDisposable.Dispose()
  RVA=0x03E35330  token=0x600004B  System.Boolean MoveNext()
  RVA=0x03E34B50  token=0x600004C  System.Void <>m__Finally1()
  RVA=0x09E26750  token=0x600004E  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x2000017
SIZE:  0x10
FIELDS:
  public    static readonly Beyond.HotFix.HotFixPatchManager.<>c<>9  // static @ 0x0
  public    static  System.Action<System.String>    <>9__14_0  // static @ 0x8
METHODS:
  RVA=0x04D435D0  token=0x60000C9  System.Void .cctor()
  RVA=0x041E1670  token=0x60000CA  System.Void .ctor()
  RVA=0x041E1670  token=0x60000CB  System.Void <PatchInitAssemble>b__14_0(System.String s)
END_CLASS

CLASS: Beyond.HotFix.HotfixDownload
TYPE:  static class
TOKEN: 0x2000010
SIZE:  0x10
FIELDS:
  private   static  System.String                   HOTFIX_SDK_TYPE  // const
  private   static  System.String                   SDK_MSG_TYPE_KEY  // const
  private   static  System.String                   SDK_MSG_CONTENT_KEY  // const
  private   static  System.Boolean                  s_registered  // static @ 0x0
METHODS:
  RVA=0x03A86260  token=0x600003F  System.Void RegisterSDKEvent()
  RVA=0x09E19500  token=0x6000040  System.Void UnregisterSDKEvent()
  RVA=0x044407B0  token=0x6000041  System.Boolean TryProcessSDKExtraInfo(System.Int32 code, System.Collections.Generic.Dictionary<System.String,System.Object> msg)
  RVA=0x044402A0  token=0x6000042  System.Void DownloadAndApply(System.String url)
  RVA=0x03E347F0  token=0x6000043  System.Boolean TryApplyDownloadText(System.String downloadText)
  RVA=0x03E34900  token=0x6000044  System.Boolean TryDecodeDownloadText(System.String downloadText, System.String& json)
  RVA=0x09E195FC  token=0x6000045  System.String _TryDecryptSignedAesField(System.String base64Data, System.String fieldName)
  RVA=0x04440360  token=0x6000046  System.Collections.IEnumerator _DownloadAndApplyCoroutine(System.String url)
  RVA=0x0443FF60  token=0x6000047  System.Boolean _IsValidHttpsUrl(System.String url)
  RVA=-1  // generic def  token=0x6000048  T _GetValueSafe(System.Collections.Generic.Dictionary<System.String,System.Object> dict, System.String key, T defaultValue)
END_CLASS

CLASS: Beyond.HotFix.HotFixMonobehaviour1
TYPE:  class
TOKEN: 0x2000012
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public    static  System.Int32                    paramInt1  // static @ 0x0
  public    static  System.Int32                    paramInt2  // static @ 0x4
  public    static  System.Int32                    paramInt3  // static @ 0x8
  public    static  System.Int32                    paramInt4  // static @ 0xc
  public    static  System.Int32                    paramInt5  // static @ 0x10
  public    static  System.Single                   paramFloat1  // static @ 0x14
  public    static  System.Single                   paramFloat2  // static @ 0x18
  public    static  System.Single                   paramFloat3  // static @ 0x1c
  public    static  System.Single                   paramFloat4  // static @ 0x20
  public    static  System.Single                   paramFloat5  // static @ 0x24
  public    static  System.Boolean                  paramBool1  // static @ 0x28
  public    static  System.Boolean                  paramBool2  // static @ 0x29
  public    static  System.Boolean                  paramBool3  // static @ 0x2a
  public    static  System.Boolean                  paramBool4  // static @ 0x2b
  public    static  System.Boolean                  paramBool5  // static @ 0x2c
  public    static  System.String                   paramString1  // static @ 0x30
  public    static  System.String                   paramString2  // static @ 0x38
  public    static  System.String                   paramString3  // static @ 0x40
  public    static  System.String                   paramString4  // static @ 0x48
  public    static  System.String                   paramString5  // static @ 0x50
  public    static  System.Object                   paramObject1  // static @ 0x58
  public    static  System.Object                   paramObject2  // static @ 0x60
  public    static  System.Object                   paramObject3  // static @ 0x68
  public    static  System.Object                   paramObject4  // static @ 0x70
  public    static  System.Object                   paramObject5  // static @ 0x78
METHODS:
  RVA=0x09E152FC  token=0x6000050  System.Void Awake()
  RVA=0x09E1580C  token=0x6000051  System.Void OnEnable()
  RVA=0x09E157CC  token=0x6000052  System.Void OnDisable()
  RVA=0x09E1593C  token=0x6000053  System.Void Start()
  RVA=0x09E1597C  token=0x6000054  System.Void Update()
  RVA=0x09E154FC  token=0x6000055  System.Void LateUpdate()
  RVA=0x09E1578C  token=0x6000056  System.Void OnDestroy()
  RVA=0x09E1537C  token=0x6000057  System.Void FixedUpdate()
  RVA=0x09E1569C  token=0x6000058  System.Void OnCollisionEnter(UnityEngine.Collision other)
  RVA=0x09E156EC  token=0x6000059  System.Void OnCollisionExit(UnityEngine.Collision other)
  RVA=0x09E1573C  token=0x600005A  System.Void OnCollisionStay(UnityEngine.Collision other)
  RVA=0x09E1584C  token=0x600005B  System.Void OnTriggerEnter(UnityEngine.Collider other)
  RVA=0x09E1589C  token=0x600005C  System.Void OnTriggerExit(UnityEngine.Collider other)
  RVA=0x09E158EC  token=0x600005D  System.Void OnTriggerStay(UnityEngine.Collider other)
  RVA=0x09E155DC  token=0x600005E  System.Void OnApplicationQuit()
  RVA=0x09E1553C  token=0x600005F  System.Void OnApplicationFocus(System.Boolean hasFocus)
  RVA=0x09E1558C  token=0x6000060  System.Void OnApplicationPause(System.Boolean pauseStatus)
  RVA=0x09E1561C  token=0x6000061  System.Void OnBecameInvisible()
  RVA=0x09E1565C  token=0x6000062  System.Void OnBecameVisible()
  RVA=0x09E1533C  token=0x6000063  System.Void Do()
  RVA=0x09E153BC  token=0x6000064  System.Void Func1()
  RVA=0x09E153FC  token=0x6000065  System.Void Func2()
  RVA=0x09E1543C  token=0x6000066  System.Void Func3()
  RVA=0x09E1547C  token=0x6000067  System.Void Func4()
  RVA=0x09E154BC  token=0x6000068  System.Void Func5()
  RVA=0x05393520  token=0x6000069  System.Void .ctor()
  RVA=0x09E159BC  token=0x600006A  System.Void .cctor()
END_CLASS

CLASS: Beyond.HotFix.HotFixMonobehaviour2
TYPE:  class
TOKEN: 0x2000013
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public    static  System.Int32                    paramInt1  // static @ 0x0
  public    static  System.Int32                    paramInt2  // static @ 0x4
  public    static  System.Int32                    paramInt3  // static @ 0x8
  public    static  System.Int32                    paramInt4  // static @ 0xc
  public    static  System.Int32                    paramInt5  // static @ 0x10
  public    static  System.Single                   paramFloat1  // static @ 0x14
  public    static  System.Single                   paramFloat2  // static @ 0x18
  public    static  System.Single                   paramFloat3  // static @ 0x1c
  public    static  System.Single                   paramFloat4  // static @ 0x20
  public    static  System.Single                   paramFloat5  // static @ 0x24
  public    static  System.Boolean                  paramBool1  // static @ 0x28
  public    static  System.Boolean                  paramBool2  // static @ 0x29
  public    static  System.Boolean                  paramBool3  // static @ 0x2a
  public    static  System.Boolean                  paramBool4  // static @ 0x2b
  public    static  System.Boolean                  paramBool5  // static @ 0x2c
  public    static  System.String                   paramString1  // static @ 0x30
  public    static  System.String                   paramString2  // static @ 0x38
  public    static  System.String                   paramString3  // static @ 0x40
  public    static  System.String                   paramString4  // static @ 0x48
  public    static  System.String                   paramString5  // static @ 0x50
  public    static  System.Object                   paramObject1  // static @ 0x58
  public    static  System.Object                   paramObject2  // static @ 0x60
  public    static  System.Object                   paramObject3  // static @ 0x68
  public    static  System.Object                   paramObject4  // static @ 0x70
  public    static  System.Object                   paramObject5  // static @ 0x78
METHODS:
  RVA=0x09E15CE0  token=0x600006B  System.Void Awake()
  RVA=0x09E161F0  token=0x600006C  System.Void OnEnable()
  RVA=0x09E161B0  token=0x600006D  System.Void OnDisable()
  RVA=0x09E16320  token=0x600006E  System.Void Start()
  RVA=0x09E16360  token=0x600006F  System.Void Update()
  RVA=0x09E15EE0  token=0x6000070  System.Void LateUpdate()
  RVA=0x09E16170  token=0x6000071  System.Void OnDestroy()
  RVA=0x09E15D60  token=0x6000072  System.Void FixedUpdate()
  RVA=0x09E16080  token=0x6000073  System.Void OnCollisionEnter(UnityEngine.Collision other)
  RVA=0x09E160D0  token=0x6000074  System.Void OnCollisionExit(UnityEngine.Collision other)
  RVA=0x09E16120  token=0x6000075  System.Void OnCollisionStay(UnityEngine.Collision other)
  RVA=0x09E16230  token=0x6000076  System.Void OnTriggerEnter(UnityEngine.Collider other)
  RVA=0x09E16280  token=0x6000077  System.Void OnTriggerExit(UnityEngine.Collider other)
  RVA=0x09E162D0  token=0x6000078  System.Void OnTriggerStay(UnityEngine.Collider other)
  RVA=0x09E15FC0  token=0x6000079  System.Void OnApplicationQuit()
  RVA=0x09E15F20  token=0x600007A  System.Void OnApplicationFocus(System.Boolean hasFocus)
  RVA=0x09E15F70  token=0x600007B  System.Void OnApplicationPause(System.Boolean pauseStatus)
  RVA=0x09E16000  token=0x600007C  System.Void OnBecameInvisible()
  RVA=0x09E16040  token=0x600007D  System.Void OnBecameVisible()
  RVA=0x09E15D20  token=0x600007E  System.Void Do()
  RVA=0x09E15DA0  token=0x600007F  System.Void Func1()
  RVA=0x09E15DE0  token=0x6000080  System.Void Func2()
  RVA=0x09E15E20  token=0x6000081  System.Void Func3()
  RVA=0x09E15E60  token=0x6000082  System.Void Func4()
  RVA=0x09E15EA0  token=0x6000083  System.Void Func5()
  RVA=0x05393520  token=0x6000084  System.Void .ctor()
  RVA=0x09E163A0  token=0x6000085  System.Void .cctor()
END_CLASS

CLASS: Beyond.HotFix.HotFixMonobehaviour3
TYPE:  class
TOKEN: 0x2000014
SIZE:  0x18
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  public    static  System.Int32                    paramInt1  // static @ 0x0
  public    static  System.Int32                    paramInt2  // static @ 0x4
  public    static  System.Int32                    paramInt3  // static @ 0x8
  public    static  System.Int32                    paramInt4  // static @ 0xc
  public    static  System.Int32                    paramInt5  // static @ 0x10
  public    static  System.Single                   paramFloat1  // static @ 0x14
  public    static  System.Single                   paramFloat2  // static @ 0x18
  public    static  System.Single                   paramFloat3  // static @ 0x1c
  public    static  System.Single                   paramFloat4  // static @ 0x20
  public    static  System.Single                   paramFloat5  // static @ 0x24
  public    static  System.Boolean                  paramBool1  // static @ 0x28
  public    static  System.Boolean                  paramBool2  // static @ 0x29
  public    static  System.Boolean                  paramBool3  // static @ 0x2a
  public    static  System.Boolean                  paramBool4  // static @ 0x2b
  public    static  System.Boolean                  paramBool5  // static @ 0x2c
  public    static  System.String                   paramString1  // static @ 0x30
  public    static  System.String                   paramString2  // static @ 0x38
  public    static  System.String                   paramString3  // static @ 0x40
  public    static  System.String                   paramString4  // static @ 0x48
  public    static  System.String                   paramString5  // static @ 0x50
  public    static  System.Object                   paramObject1  // static @ 0x58
  public    static  System.Object                   paramObject2  // static @ 0x60
  public    static  System.Object                   paramObject3  // static @ 0x68
  public    static  System.Object                   paramObject4  // static @ 0x70
  public    static  System.Object                   paramObject5  // static @ 0x78
METHODS:
  RVA=0x09E166C4  token=0x6000086  System.Void Awake()
  RVA=0x09E16BD4  token=0x6000087  System.Void OnEnable()
  RVA=0x09E16B94  token=0x6000088  System.Void OnDisable()
  RVA=0x09E16D04  token=0x6000089  System.Void Start()
  RVA=0x09E16D44  token=0x600008A  System.Void Update()
  RVA=0x09E168C4  token=0x600008B  System.Void LateUpdate()
  RVA=0x09E16B54  token=0x600008C  System.Void OnDestroy()
  RVA=0x09E16744  token=0x600008D  System.Void FixedUpdate()
  RVA=0x09E16A64  token=0x600008E  System.Void OnCollisionEnter(UnityEngine.Collision other)
  RVA=0x09E16AB4  token=0x600008F  System.Void OnCollisionExit(UnityEngine.Collision other)
  RVA=0x09E16B04  token=0x6000090  System.Void OnCollisionStay(UnityEngine.Collision other)
  RVA=0x09E16C14  token=0x6000091  System.Void OnTriggerEnter(UnityEngine.Collider other)
  RVA=0x09E16C64  token=0x6000092  System.Void OnTriggerExit(UnityEngine.Collider other)
  RVA=0x09E16CB4  token=0x6000093  System.Void OnTriggerStay(UnityEngine.Collider other)
  RVA=0x09E169A4  token=0x6000094  System.Void OnApplicationQuit()
  RVA=0x09E16904  token=0x6000095  System.Void OnApplicationFocus(System.Boolean hasFocus)
  RVA=0x09E16954  token=0x6000096  System.Void OnApplicationPause(System.Boolean pauseStatus)
  RVA=0x09E169E4  token=0x6000097  System.Void OnBecameInvisible()
  RVA=0x09E16A24  token=0x6000098  System.Void OnBecameVisible()
  RVA=0x09E16704  token=0x6000099  System.Void Do()
  RVA=0x09E16784  token=0x600009A  System.Void Func1()
  RVA=0x09E167C4  token=0x600009B  System.Void Func2()
  RVA=0x09E16804  token=0x600009C  System.Void Func3()
  RVA=0x09E16844  token=0x600009D  System.Void Func4()
  RVA=0x09E16884  token=0x600009E  System.Void Func5()
  RVA=0x05393520  token=0x600009F  System.Void .ctor()
  RVA=0x09E16D84  token=0x60000A0  System.Void .cctor()
END_CLASS

CLASS: Beyond.HotFix.HotFixPatchManagerProxy
TYPE:  class
TOKEN: 0x2000015
SIZE:  0x10
IMPLEMENTS: Beyond.IHotFixPatchManagerProxy Beyond.IHotFixCommon
FIELDS:
  private   static  Beyond.HotFix.HotfixDownloadPayloads_pendingDownloadPayload  // static @ 0x0
  private   static  System.Int32                    s_currentAppliedVersion  // static @ 0x8
METHODS:
  RVA=0x04CF5480  token=0x60000A1  System.Void _RunOnStart()
  RVA=0x04A1A540  token=0x60000A2  System.Void InitManager()
  RVA=0x09E170A8  token=0x60000A3  System.Void ReleaseManager()
  RVA=0x02D41A30  token=0x60000A4  System.Void OnReturnToLogin()
  RVA=0x04D52670  token=0x60000A5  System.Void RegisterHotfixSDKEvent()
  RVA=0x09E172A8  token=0x60000A6  System.Void UnregisterHotfixSDKEvent()
  RVA=0x04441730  token=0x60000A7  System.Boolean TryProcessHotfixSDKExtraInfo(System.Int32 code, System.Collections.Generic.Dictionary<System.String,System.Object> msg)
  RVA=0x03D9FDC0  token=0x60000A8  System.Void PatchInitAssemble()
  RVA=0x04CA5120  token=0x60000A9  System.Void PatchRemoteInitAssemble()
  RVA=0x04D4E8D0  token=0x60000AA  System.Void PatchRemoteInjectedLua()
  RVA=0x0455F320  token=0x60000AB  System.Void _OnHotfixDownloadReady(Beyond.HotFix.HotfixDownloadEventData& data)
  RVA=0x04560EB0  token=0x60000AC  System.Void _ApplyDownloadPayload(Beyond.HotFix.HotfixDownloadPayload payload)
  RVA=0x03E3DF90  token=0x60000AD  System.Void _PatchRemoteInjectedLua()
  RVA=0x03D9FE20  token=0x60000AE  System.Void PatchMainAssemble()
  RVA=0x04CA5060  token=0x60000AF  System.Void PatchRemoteMainAssemble()
  RVA=0x041E1670  token=0x60000B0  System.Void .ctor()
END_CLASS

CLASS: Beyond.HotFix.HotFixPatchManager
TYPE:  class
TOKEN: 0x2000016
SIZE:  0x18
EXTENDS: Beyond.Singleton`1
FIELDS:
  private           Beyond.TickFunction             m_tickFunction  // 0x10
  private   static  System.Int32                    s_currentPatchVersion  // static @ 0x0
  private   static  System.Collections.Generic.HashSet<System.Reflection.Assembly>s_patchedAssemble  // static @ 0x8
  private   static  System.Collections.Generic.List<IFix.Core.VirtualMachine>s_patchedVM  // static @ 0x10
  private   static  System.Collections.Generic.HashSet<System.Reflection.Assembly>s_downloadPatchedAssemble  // static @ 0x18
  private   static  System.Collections.Generic.List<IFix.Core.VirtualMachine>s_downloadPatchedVM  // static @ 0x20
PROPERTIES:
  currentPatchVersion  get=0x09E1948C
METHODS:
  RVA=0x04D33860  token=0x60000B2  System.Void .ctor()
  RVA=0x04D338A0  token=0x60000B3  System.Void _Init()
  RVA=0x09E1943C  token=0x60000B4  System.Void _UnInit()
  RVA=0x09E17468  token=0x60000B5  System.Void Release()
  RVA=0x0382A6D0  token=0x60000B6  System.Void _CreateTickFunction()
  RVA=0x09E174B8  token=0x60000B7  System.Void _ClearTickFunction()
  RVA=0x03DA1330  token=0x60000B8  System.Void PatchInitAssemble()
  RVA=0x03DA0240  token=0x60000B9  System.Void PatchMainAssemble()
  RVA=0x0301FB60  token=0x60000BA  System.Void Tick(System.Single deltaTime)
  RVA=0x09E1751C  token=0x60000BB  System.Void _ForceLocalUpdateIFix()
  RVA=0x09E17630  token=0x60000BC  System.Void _ForceLocalUpdateLua()
  RVA=0x09E17344  token=0x60000BD  System.Int32 InjectStringPatch(System.String value)
  RVA=0x04560E00  token=0x60000BE  System.Int32 InjectLuaStringPatch(System.String value)
  RVA=0x09E172F4  token=0x60000BF  System.Void InjectLuaStringPatchFromUrl(System.String url)
  RVA=0x09E1862C  token=0x60000C0  System.Int32 _InjectStringPatchLine(System.String value)
  RVA=0x09E17AC4  token=0x60000C1  System.Int32 _InjectStringPatchJson(System.String value)
  RVA=0x09E17744  token=0x60000C2  System.Int32 _InjectLuaStringPatchLine(System.String value)
  RVA=0x045609E0  token=0x60000C3  System.Int32 _InjectLuaStringPatchJson(System.String value)
  RVA=0x09E18DF0  token=0x60000C4  System.Boolean _TryApplyLuaPatchBase64(System.String base64Data, System.Int32 expectedSize, System.String md5, System.String patchName)
  RVA=0x02D43FE0  token=0x60000C5  System.Void PatchDebugAssemble(System.IO.Stream stream, System.Boolean isDownload)
  RVA=0x02D423C0  token=0x60000C6  System.Void ClearPatchesForRelogin()
  RVA=0x03DA2120  token=0x60000C7  System.Int32 PatchAssemble()
  RVA=0x03DA03A0  token=0x60000C8  System.Void .cctor()
END_CLASS

CLASS: Beyond.SourceGenerator.DataNAttribute
TYPE:  class
TOKEN: 0x200000B
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  public            System.Int32                    capacity  // 0x10
  public            System.Int32                    tSize  // 0x14
  public            System.Boolean                  ring  // 0x18
METHODS:
  RVA=0x041E1670  token=0x600003A  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.CameraControlConfigAttribute
TYPE:  class
TOKEN: 0x200000C
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600003B  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.ECSComponentAttribute
TYPE:  class
TOKEN: 0x200000D
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public            System.Runtime.InteropServices.LayoutKindlayoutKind  // 0x10
  public            System.Boolean                  isTag  // 0x14
METHODS:
  RVA=0x041E1670  token=0x600003C  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.AnimatorBlackboardUtility
TYPE:  static class
TOKEN: 0x200000E
SIZE:  0x10
FIELDS:
  private   static  System.Boolean                  s_enableFastPathBuffer  // static @ 0x0
METHODS:
  RVA=-1  // generic def  token=0x600003D  System.Void ClearAndEnsureDictionaryCapacity(System.Collections.Generic.Dictionary<TKey,TValue>& dict, System.Int32 capacity)
END_CLASS

CLASS: Beyond.SourceGenerator.AnimatorBlackboardAttribute
TYPE:  class
TOKEN: 0x200000F
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x600003E  System.Void .ctor()
END_CLASS

CLASS: HG.Rendering.Runtime.HGConstantBufferLayoutAttribute
TYPE:  class
TOKEN: 0x2000002
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000001  System.Void .ctor()
END_CLASS

CLASS: IFix.IFixMethodPrinter
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x28
FIELDS:
  private           System.Reflection.FieldInfo     unmanagedCodesField  // 0x10
  private           System.Reflection.PropertyInfo  exceptionHandlersProperty  // 0x18
  private           System.Reflection.PropertyInfo  externMethodsProperty  // 0x20
METHODS:
  RVA=0x09E1ED50  token=0x6000002  System.Void InitializeReflection()
  RVA=0x09E1E580  token=0x6000003  System.Int32 GetMethodCount(IFix.Core.VirtualMachine vm)
  RVA=0x09E1E600  token=0x6000004  IFix.Core.Instruction[] GetMethodInstructions(IFix.Core.VirtualMachine vm, System.Int32 methodIndex)
  RVA=0x09E1CB50  token=0x6000005  System.Int32 EstimateInstructionCount(IFix.Core.Instruction* codePtr)
  RVA=0x09E1D0BC  token=0x6000006  System.String GetAllIFixMethodStrs(IFix.Core.VirtualMachine vm)
  RVA=0x09E1C104  token=0x6000007  System.Void DumpRedirectToMappings(System.Text.StringBuilder sb, IFix.Core.WrappersManager wrappersManager)
  RVA=0x09E1A3C4  token=0x6000008  System.Collections.Generic.Dictionary<System.Int32,System.String> BuildIdMap(System.Reflection.Assembly preferredAssembly)
  RVA=0x09E1CBA4  token=0x6000009  System.Void ForceLoadIdMapTypes(System.Collections.Generic.Dictionary<System.Int32,System.String> result)
  RVA=0x09E19A60  token=0x600000A  System.Void AddIdMapFields(System.Collections.Generic.Dictionary<System.Int32,System.String> result, System.Type idMapType)
  RVA=0x09E1A734  token=0x600000B  System.String BuildMethodLabelFromIdMapField(System.Reflection.FieldInfo field)
  RVA=0x09E1EEA4  token=0x600000C  System.Boolean IsNumericField(System.Type type)
  RVA=0x09E1AD38  token=0x600000D  System.Collections.Generic.List<IFix.IFixMethodPrinter.WrapperArrayCandidate> CollectWrapperArrays(IFix.Core.WrappersManager wrappersManager)
  RVA=0x09E1A8BC  token=0x600000E  System.Collections.Generic.List<IFix.IFixMethodPrinter.WrapperArrayCandidate> CollectWrapperArraysFromAssembly(System.Reflection.Assembly asm)
  RVA=0x09E1F37C  token=0x600000F  IFix.IFixMethodPrinter.WrapperArrayCandidate SelectBestWrapperArray(System.Collections.Generic.List<IFix.IFixMethodPrinter.WrapperArrayCandidate> candidates, System.Collections.Generic.Dictionary<System.Int32,System.String> idMap)
  RVA=0x09E1F8A8  token=0x6000010  IFix.IFixMethodPrinter.WrapperArrayCandidate TryGetWrapperArrayDirect(System.Reflection.Assembly asm)
  RVA=0x09E1FFB8  token=0x6000011  System.String TryResolveWrapperTarget(System.Object wrapperObj)
  RVA=0x09E1E93C  token=0x6000012  System.String GetWrapperDebugInfo(System.Object wrapperObj)
  RVA=0x09E1F548  token=0x6000013  System.String SummarizeValue(System.Object val)
  RVA=0x09E1FC10  token=0x6000014  System.Nullable<System.Int32> TryGetWrapperMethodId(System.Object wrapperObj)
  RVA=0x09E1A018  token=0x6000015  System.Void AppendILSummary(System.Text.StringBuilder sb, IFix.Core.VirtualMachine vm, System.Int32 methodId, System.Reflection.MethodBase[] externMethods)
  RVA=0x09E1CE24  token=0x6000016  System.String FormatOperand(IFix.Core.VirtualMachine vm, IFix.Core.Instruction inst, System.Reflection.MethodBase[] externMethods)
  RVA=0x09E1F060  token=0x6000017  System.Int32 ScoreWrapperCandidate(IFix.IFixMethodPrinter.WrapperArrayCandidate candidate, System.Int32 minLen)
  RVA=0x09E19C3C  token=0x6000018  System.Void AddWrapperCandidate(System.Collections.Generic.List<IFix.IFixMethodPrinter.WrapperArrayCandidate> list, System.String source, System.String ownerTypeName, System.String name, System.Type valueType, System.Object value)
  RVA=0x09E1C470  token=0x6000019  System.Void DumpWrappersManagerMembers(System.Text.StringBuilder sb, IFix.Core.WrappersManager wrappersManager)
  RVA=0x09E1BA48  token=0x600001A  System.Void DumpAssemblyPotentialStores(System.Text.StringBuilder sb, System.Reflection.Assembly asm)
  RVA=0x09E1D03C  token=0x600001B  System.Collections.Generic.IEnumerable<System.Reflection.FieldInfo> GetAllFieldsIncludingBase(System.Type type, System.Reflection.BindingFlags flags)
  RVA=0x09E1E500  token=0x600001C  System.Collections.Generic.IEnumerable<System.Reflection.PropertyInfo> GetAllPropertiesIncludingBase(System.Type type, System.Reflection.BindingFlags flags)
  RVA=0x09E1B8FC  token=0x600001D  System.String DescribeValue(System.Object value)
  RVA=0x041E1670  token=0x600001E  System.Void .ctor()
END_CLASS

CLASS: IFix.ILFixDynamicMethodWrapper
TYPE:  class
TOKEN: 0x2000018
SIZE:  0x28
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
  private           System.Int32                    methodId  // 0x18
  private           System.Object                   anonObj  // 0x20
  public    static  IFix.ILFixDynamicMethodWrapper[]wrapperArray  // static @ 0x0
METHODS:
  RVA=0x032B2C30  token=0x60000CC  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj)
  RVA=0x063625A4  token=0x60000CD  System.Void __Gen_Wrap_0()
  RVA=0x063632E4  token=0x60000CE  System.Boolean __Gen_Wrap_1(System.Object P0)
  RVA=0x09E2248C  token=0x60000CF  System.String __Gen_Wrap_2(System.Object P0, System.Object P1)
  RVA=0x09E226BC  token=0x60000D0  System.Boolean __Gen_Wrap_3(System.Object P0, System.String& P1)
  RVA=0x09E22E98  token=0x60000D1  System.Collections.IEnumerator __Gen_Wrap_4(System.Object P0)
  RVA=0x063633B4  token=0x60000D2  System.Void __Gen_Wrap_5(System.Object P0)
  RVA=0x09E239F4  token=0x60000D3  System.Boolean __Gen_Wrap_6(System.Int32 P0, System.Object P1)
  RVA=0x09C8B45C  token=0x60000D4  System.Void __Gen_Wrap_7(System.Object P0, System.Object P1)
  RVA=0x066C2B40  token=0x60000D5  System.Void __Gen_Wrap_8(System.Object P0, System.Boolean P1)
  RVA=0x09E244B8  token=0x60000D6  System.Boolean __Gen_Wrap_9(System.Object P0, System.Int32 P1, System.Object P2, System.Object P3)
  RVA=0x06360E84  token=0x60000D7  System.Int32 __Gen_Wrap_10(System.Object P0)
  RVA=0x09E20DB8  token=0x60000D8  System.Void __Gen_Wrap_11(System.Object P0, Beyond.HotFix.HotfixDownloadEventData& P1)
  RVA=0x069ADDF8  token=0x60000D9  System.Boolean __Gen_Wrap_12(System.Object P0, System.Int32 P1, System.Object P2)
  RVA=0x066C296C  token=0x60000DA  System.Void __Gen_Wrap_13(System.String P0)
  RVA=0x063621C4  token=0x60000DB  System.Int32 __Gen_Wrap_14()
  RVA=0x066C288C  token=0x60000DC  System.Void __Gen_Wrap_15(System.Object P0, System.Single P1)
  RVA=0x09C8B45C  token=0x60000DD  System.Void __Gen_Wrap_16(System.Object P0, System.ComponentModel.PropertyChangedEventArgs P1)
  RVA=0x066C296C  token=0x60000DE  System.Void __Gen_Wrap_17(Beyond.Misc.ObjectRef P0)
  RVA=0x09E21C78  token=0x60000DF  System.Void __Gen_Wrap_18(U8.SDK.U8MockLogin P0)
  RVA=0x09E21D5C  token=0x60000E0  U8.SDK.SDKPromise<U8.SDK.U8PayResult> __Gen_Wrap_19(U8.SDK.U8PayParams P0)
  RVA=0x09E21E38  token=0x60000E1  System.Void __Gen_Wrap_20(U8.SDK.U8PayResult P0)
  RVA=0x09E21F24  token=0x60000E2  U8.SDK.SDKMeta __Gen_Wrap_21()
  RVA=0x066C296C  token=0x60000E3  System.Void __Gen_Wrap_22(System.Collections.Generic.Dictionary<System.Enum,Beyond.TickGroupMonitorItem> P0)
  RVA=0x06301ED4  token=0x60000E4  System.Void __Gen_Wrap_23(System.Int32 P0)
  RVA=0x066DCCF0  token=0x60000E5  System.Void __Gen_Wrap_24(System.Single P0)
  RVA=0x066DCC44  token=0x60000E6  System.Void __Gen_Wrap_25(System.Boolean P0)
  RVA=0x09E21FDC  token=0x60000E7  System.Void __Gen_Wrap_26(UnityEngine.HyperGryph.ECS.Entity P0, Unity.Mathematics.float3 P1, Unity.Mathematics.float3 P2, UnityEngine.Matrix4x4 P3)
  RVA=0x09E22184  token=0x60000E8  System.Void __Gen_Wrap_27(UnityEngine.HyperGryph.ECS.Entity P0)
  RVA=0x09E22268  token=0x60000E9  System.Void __Gen_Wrap_28(Unity.Collections.NativeArray<UnityEngine.HyperGryph.ECS.Entity> P0, System.Int32 P1)
  RVA=0x09E22368  token=0x60000EA  System.Void __Gen_Wrap_29(UnityEngine.HyperGryph.ECS.EntityManager P0, UnityEngine.HyperGryph.ECS.Entity P1)
  RVA=0x09E22598  token=0x60000EB  System.Void __Gen_Wrap_30(System.Object P0, Beyond.Audio.AudioCallbackType P1, Beyond.Audio.AudioCallbackInfo P2)
  RVA=0x066C296C  token=0x60000EC  System.Void __Gen_Wrap_31(TMPro.TMP_TextInfo P0)
  RVA=0x066C296C  token=0x60000ED  System.Void __Gen_Wrap_32(UnityEngine.GameObject P0)
  RVA=0x06365664  token=0x60000EE  System.Void __Gen_Wrap_33(System.Int32 P0, UnityEngine.GameObject P1)
  RVA=0x09C9FEFC  token=0x60000EF  System.Void __Gen_Wrap_34(System.Int32 P0, System.Boolean P1)
  RVA=0x069AE148  token=0x60000F0  System.Void __Gen_Wrap_35(System.Int32 P0, Beyond.UI.UIDropdownOption P1, System.Boolean P2)
  RVA=0x06365664  token=0x60000F1  System.Void __Gen_Wrap_36(System.Int32 P0, Beyond.UI.UIDropdown P1)
  RVA=0x06365664  token=0x60000F2  System.Void __Gen_Wrap_37(System.Int32 P0, Beyond.UI.UIMultiSelectDropdown P1)
  RVA=0x09C86674  token=0x60000F3  System.Void __Gen_Wrap_38(System.Boolean P0, System.String P1, System.String P2)
  RVA=0x066C296C  token=0x60000F4  System.Void __Gen_Wrap_39(Beyond.Rendering.HGTerrainHelper P0)
  RVA=0x09E227F4  token=0x60000F5  System.Boolean __Gen_Wrap_40(UnityEngine.Vector2Int P0)
  RVA=0x09E228E8  token=0x60000F6  System.Boolean __Gen_Wrap_41(UnityEngine.Vector2 P0)
  RVA=0x09E229E0  token=0x60000F7  System.Void __Gen_Wrap_42(UnityEngine.Vector2Int P0)
  RVA=0x066D3790  token=0x60000F8  System.Single __Gen_Wrap_43()
  RVA=0x09E22AC4  token=0x60000F9  System.Single __Gen_Wrap_44(UnityEngine.Light P0, UnityEngine.Camera P1, UnityEngine.Vector3 P2)
  RVA=0x09E22BF4  token=0x60000FA  UnityEngine.Rendering.RTHandle __Gen_Wrap_45(System.Int32 P0)
  RVA=0x09E22CCC  token=0x60000FB  System.Void __Gen_Wrap_46(UnityEngine.Rendering.RenderTargetIdentifier P0, UnityEngine.Rendering.CommandBuffer P1)
  RVA=0x066C53B4  token=0x60000FC  System.Boolean __Gen_Wrap_47(System.Reflection.FieldInfo P0)
  RVA=0x09E22DE0  token=0x60000FD  System.Object __Gen_Wrap_48()
  RVA=0x063770B0  token=0x60000FE  System.Boolean __Gen_Wrap_49()
  RVA=0x06377214  token=0x60000FF  System.UInt32 __Gen_Wrap_50()
  RVA=0x06301ED4  token=0x6000100  System.Void __Gen_Wrap_51(System.UInt32 P0)
  RVA=0x09E22F88  token=0x6000101  System.Enum __Gen_Wrap_52()
  RVA=0x066C296C  token=0x6000102  System.Void __Gen_Wrap_53(System.Enum P0)
  RVA=0x09E23040  token=0x6000103  UnityEngine.Color __Gen_Wrap_54()
  RVA=0x09E23118  token=0x6000104  System.Void __Gen_Wrap_55(UnityEngine.Color P0)
  RVA=0x09E231FC  token=0x6000105  UnityEngine.Vector2 __Gen_Wrap_56()
  RVA=0x09E232B4  token=0x6000106  System.Void __Gen_Wrap_57(UnityEngine.Vector2 P0)
  RVA=0x09E2339C  token=0x6000107  UnityEngine.Vector3 __Gen_Wrap_58()
  RVA=0x09E2347C  token=0x6000108  System.Void __Gen_Wrap_59(UnityEngine.Vector3 P0)
  RVA=0x09E23568  token=0x6000109  UnityEngine.Vector4 __Gen_Wrap_60()
  RVA=0x09E23640  token=0x600010A  System.Void __Gen_Wrap_61(UnityEngine.Vector4 P0)
  RVA=0x066C296C  token=0x600010B  System.Void __Gen_Wrap_62(UnityEngine.Rendering.DebugUI.Panel P0)
  RVA=0x066C296C  token=0x600010C  System.Void __Gen_Wrap_63(MagicaCloth.BaseMeshDeformer P0)
  RVA=0x06363EE4  token=0x600010D  System.UInt32 __Gen_Wrap_64(System.Int32 P0)
  RVA=0x09E23724  token=0x600010E  Unity.Mathematics.float3 __Gen_Wrap_65(System.Int32 P0)
  RVA=0x09E23824  token=0x600010F  Unity.Mathematics.quaternion __Gen_Wrap_66(System.Int32 P0)
  RVA=0x0636B508  token=0x6000110  System.Single __Gen_Wrap_67(System.Int32 P0)
  RVA=0x09E2391C  token=0x6000111  UnityEngine.Transform __Gen_Wrap_68(System.Int32 P0)
  RVA=0x09C8B45C  token=0x6000112  System.Void __Gen_Wrap_69(MagicaCloth.MagicaAvatar P0, MagicaCloth.MagicaAvatarParts P1)
  RVA=0x066C296C  token=0x6000113  System.Void __Gen_Wrap_70(MagicaCloth.MagicaAvatar P0)
  RVA=0x06374274  token=0x6000114  System.Void __Gen_Wrap_71(System.Collections.Generic.List<System.Int32> P0, System.Collections.Generic.List<System.Int32> P1, System.Collections.Generic.List<UnityEngine.Vector3> P2, System.Collections.Generic.List<UnityEngine.Vector3> P3, System.Collections.Generic.List<UnityEngine.Vector3> P4, System.Collections.Generic.List<System.Int32> P5, System.Collections.Generic.List<System.Int32> P6)
  RVA=0x066C296C  token=0x6000115  System.Void __Gen_Wrap_72(MagicaCloth.CoreComponent P0)
  RVA=0x066C296C  token=0x6000116  System.Void __Gen_Wrap_73(UnityEngine.Animator P0)
  RVA=0x066C296C  token=0x6000117  System.Void __Gen_Wrap_74(MagicaCloth.BaseCloth P0)
  RVA=0x09CB3104  token=0x6000118  System.Void __Gen_Wrap_75(System.Int32 P0, System.Int32 P1)
  RVA=0x09C8B45C  token=0x6000119  System.Void __Gen_Wrap_76(System.Object P0, System.Runtime.Serialization.SafeSerializationEventArgs P1)
  RVA=0x066C296C  token=0x600011A  System.Void __Gen_Wrap_77(System.IAsyncResult P0)
  RVA=0x09E23AE0  token=0x600011B  System.Void __Gen_Wrap_78(Beyond.Network.NetResponse& P0)
  RVA=0x09C8B45C  token=0x600011C  System.Void __Gen_Wrap_79(System.Type P0, System.Reflection.MethodInfo P1)
  RVA=0x09E23C18  token=0x600011D  System.Void __Gen_Wrap_80(UnityEngine.InputSystem.InputAction.CallbackContext P0)
  RVA=0x09E23CFC  token=0x600011E  System.Void __Gen_Wrap_81(HG.Rendering.Runtime.HGRenderPipeCallbackArgs P0)
  RVA=0x09E23DE0  token=0x600011F  System.Void __Gen_Wrap_82(UnityEngine.Rendering.ScriptableRenderContext P0, HG.Rendering.Runtime.HGCamera P1)
  RVA=0x09C8B45C  token=0x6000120  System.Void __Gen_Wrap_83(HG.Rendering.Runtime.HGCamera P0, UnityEngine.Rendering.RTHandle P1)
  RVA=0x09E23EE0  token=0x6000121  System.Void __Gen_Wrap_84(System.Boolean P0, System.Boolean P1)
  RVA=0x066C296C  token=0x6000122  System.Void __Gen_Wrap_85(System.Net.Http.HttpRequestMessage P0)
  RVA=0x09E23FA8  token=0x6000123  System.Boolean __Gen_Wrap_86(Beyond.Blackboard.DataPair P0)
  RVA=0x09E240A0  token=0x6000124  System.Void __Gen_Wrap_87(Beyond.Blackboard.DataPair P0)
  RVA=0x09E2418C  token=0x6000125  System.Int32 __Gen_Wrap_88(Beyond.Blackboard.DataPair P0, Beyond.Blackboard.DataPair P1)
  RVA=0x066C296C  token=0x6000126  System.Void __Gen_Wrap_89(UnityEngine.Transform P0)
  RVA=0x066C53B4  token=0x6000127  System.Boolean __Gen_Wrap_90(UnityEngine.Transform P0)
  RVA=0x066C296C  token=0x6000128  System.Void __Gen_Wrap_91(UnityEngine.Material P0)
  RVA=0x066C296C  token=0x6000129  System.Void __Gen_Wrap_92(UnityEngine.Texture P0)
  RVA=0x066C296C  token=0x600012A  System.Void __Gen_Wrap_93(UnityEngine.Shader P0)
  RVA=0x066C296C  token=0x600012B  System.Void __Gen_Wrap_94(UnityEngine.Mesh P0)
  RVA=0x066C296C  token=0x600012C  System.Void __Gen_Wrap_95(UnityEngine.Sprite P0)
  RVA=0x066C296C  token=0x600012D  System.Void __Gen_Wrap_96(UnityEngine.ScriptableObject P0)
  RVA=0x066C296C  token=0x600012E  System.Void __Gen_Wrap_97(UnityEngine.RuntimeAnimatorController P0)
  RVA=0x09E242C0  token=0x600012F  System.Boolean __Gen_Wrap_98(Beyond.StyledBlackboard.StyledDataPair P0)
  RVA=0x09E243C4  token=0x6000130  System.Void __Gen_Wrap_99(Beyond.StyledBlackboard.StyledDataPair P0)
  RVA=0x09E204B8  token=0x6000131  System.Int32 __Gen_Wrap_100(Beyond.StyledBlackboard.StyledDataPair P0, Beyond.StyledBlackboard.StyledDataPair P1)
  RVA=0x066C53B4  token=0x6000132  System.Boolean __Gen_Wrap_101(UnityEngine.GameObject P0)
  RVA=0x071A12AC  token=0x6000133  System.Void __Gen_Wrap_102(System.Boolean P0, Beyond.Resource.IAssetLoaderHandle P1)
  RVA=0x09E20600  token=0x6000134  System.Void __Gen_Wrap_103(System.Boolean P0, Beyond.Resource.FAssetProxyHandle P1)
  RVA=0x066C296C  token=0x6000135  System.Void __Gen_Wrap_104(Beyond.Resource.Runtime.IPreloadGroup P0)
  RVA=0x066C296C  token=0x6000136  System.Void __Gen_Wrap_105(Beyond.Resource.Runtime.Asset P0)
  RVA=0x066C296C  token=0x6000137  System.Void __Gen_Wrap_106(Beyond.Resource.Runtime.BundleLoader.AssetProxy P0)
  RVA=0x09CB3104  token=0x6000138  System.Void __Gen_Wrap_107(Beyond.SDK.HGRetCodeType P0, System.Int32 P1)
  RVA=0x06301ED4  token=0x6000139  System.Void __Gen_Wrap_108(Beyond.SDK.HGDownloader.PauseReason P0)
  RVA=0x09E20704  token=0x600013A  System.Boolean __Gen_Wrap_109(UnityEngine.HyperGryph.ECS.Entity P0, UnityEngine.HyperGryph.Streaming.FlatBufferConvertContextV2 P1, UnityEngine.HyperGryph.Streaming.EntityTransition P2)
  RVA=0x09E20850  token=0x600013B  System.Boolean __Gen_Wrap_110(System.IntPtr& P0, UnityEngine.HyperGryph.Streaming.FlatBufferConvertContextV2 P1, UnityEngine.HyperGryph.Streaming.EntityTransition P2)
  RVA=0x09E209D4  token=0x600013C  System.Boolean __Gen_Wrap_111(UnityEngine.Component P0, UnityEngine.HyperGryph.Streaming.FlatBufferConvertContextV2 P1, UnityEngine.HyperGryph.Streaming.EntityTransition P2)
  RVA=0x09E20B00  token=0x600013D  UnityEngine.Component __Gen_Wrap_112(UnityEngine.GameObject P0)
  RVA=0x09E20BDC  token=0x600013E  UnityEngine.Component __Gen_Wrap_113(UnityEngine.GameObject P0, System.Int32 P1)
  RVA=0x066C296C  token=0x600013F  System.Void __Gen_Wrap_114(Beyond.Network.ServerConfig P0)
  RVA=0x071A12AC  token=0x6000140  System.Void __Gen_Wrap_115(System.Boolean P0, Beyond.Gameplay.LevelScriptData P1)
  RVA=0x071A12AC  token=0x6000141  System.Void __Gen_Wrap_116(System.Boolean P0, Beyond.Gameplay.InteractiveTemplateData P1)
  RVA=0x09E20CFC  token=0x6000142  System.Single __Gen_Wrap_117(System.Single P0)
  RVA=0x066C53B4  token=0x6000143  System.Boolean __Gen_Wrap_118(Beyond.Gameplay.ClientSingleMapMarkData P0)
  RVA=0x066C296C  token=0x6000144  System.Void __Gen_Wrap_119(Beyond.Gameplay.ClientSingleMapMarkData P0)
  RVA=0x071AD678  token=0x6000145  System.Int32 __Gen_Wrap_120(Beyond.Gameplay.ClientSingleMapMarkData P0, Beyond.Gameplay.ClientSingleMapMarkData P1)
  RVA=0x066C296C  token=0x6000146  System.Void __Gen_Wrap_121(NodeCanvas.Framework.Graph P0)
  RVA=0x066C296C  token=0x6000147  System.Void __Gen_Wrap_122(Beyond.Gameplay.AICanvasGraphData P0)
  RVA=0x09E20EDC  token=0x6000148  System.Void __Gen_Wrap_123(System.UInt64 P0, System.UInt64 P1, Beyond.Resource.FAssetProxyHandle P2)
  RVA=0x09C8B45C  token=0x6000149  System.Void __Gen_Wrap_124(Beyond.Gameplay.ParamVariable P0, Beyond.Gameplay.ParamVariable P1)
  RVA=0x066C296C  token=0x600014A  System.Void __Gen_Wrap_125(Beyond.Gameplay.DialogTree P0)
  RVA=0x09C8B45C  token=0x600014B  System.Void __Gen_Wrap_126(Proto.CS_FACTORY_OP P0, Proto.SC_FACTORY_OP_RET P1)
  RVA=0x09E20FFC  token=0x600014C  System.Void __Gen_Wrap_127(Beyond.ObjectPtr<Beyond.Gameplay.Core.Entity> P0)
  RVA=0x071A12AC  token=0x600014D  System.Void __Gen_Wrap_128(System.Boolean P0, Beyond.Gameplay.EffectInstance P1)
  RVA=0x066C296C  token=0x600014E  System.Void __Gen_Wrap_129(Beyond.Rendering.EntityVFXControllerBase P0)
  RVA=0x09E210E0  token=0x600014F  System.Boolean __Gen_Wrap_130(System.Single P0)
  RVA=0x066C296C  token=0x6000150  System.Void __Gen_Wrap_131(Beyond.Gameplay.Factory.FactoryUtil.TopViewBuildingInfo P0)
  RVA=0x066C296C  token=0x6000151  System.Void __Gen_Wrap_132(UnityEngine.AnimatorOverrideController P0)
  RVA=0x09E2119C  token=0x6000152  System.Boolean __Gen_Wrap_133(Beyond.Gameplay.Factory.Core.Vector2IntData P0)
  RVA=0x09E21290  token=0x6000153  System.Boolean __Gen_Wrap_134(System.Int32 P0, System.Int32 P1)
  RVA=0x09C8B45C  token=0x6000154  System.Void __Gen_Wrap_135(System.String P0, UnityEngine.GameObject P1)
  RVA=0x09E21364  token=0x6000155  Beyond.Gameplay.RemoteFactory.PrefabMeshMaterialMatrixParser __Gen_Wrap_136()
  RVA=0x09E2141C  token=0x6000156  Beyond.Gameplay.RemoteFactory.IRemoteFactoryMessageSendable __Gen_Wrap_137()
  RVA=0x09E214D4  token=0x6000157  Beyond.Gameplay.RemoteFactory.IFactoryStateProvider __Gen_Wrap_138()
  RVA=0x066C53B4  token=0x6000158  System.Boolean __Gen_Wrap_139(Beyond.Gameplay.LevelDataBlackbox P0)
  RVA=0x09E2158C  token=0x6000159  System.Void __Gen_Wrap_140(Beyond.SparkBuffer.Runtime.Wrapper.BeanArray<Beyond.Cfg.DynamicParam> P0)
  RVA=0x09CD3FC4  token=0x600015A  System.Void __Gen_Wrap_141(System.UInt64 P0)
  RVA=0x066C296C  token=0x600015B  System.Void __Gen_Wrap_142(Beyond.Gameplay.Audio.PossibleResponses P0)
  RVA=0x066C296C  token=0x600015C  System.Void __Gen_Wrap_143(Beyond.Gameplay.Audio.ResponseContext P0)
  RVA=0x09E21680  token=0x600015D  System.Void __Gen_Wrap_144(System.UInt32 P0, System.UInt32 P1, Beyond.Gameplay.Audio.VoiceResponseProcessor.EResponseDecideReason P2)
  RVA=0x06301ED4  token=0x600015E  System.Void __Gen_Wrap_145(Beyond.I18n.AudioLang P0)
  RVA=0x06301ED4  token=0x600015F  System.Void __Gen_Wrap_146(Beyond.GEnums.AudioGameState P0)
  RVA=0x06301ED4  token=0x6000160  System.Void __Gen_Wrap_147(NodeCanvas.Framework.Status P0)
  RVA=0x09E21760  token=0x6000161  UnityEngine.Vector3 __Gen_Wrap_148(UnityEngine.Vector3 P0)
  RVA=0x09E21890  token=0x6000162  System.Void __Gen_Wrap_149(System.Int32 P0, System.String P1, UnityEngine.GameObject P2)
  RVA=0x071A12AC  token=0x6000163  System.Void __Gen_Wrap_150(System.Boolean P0, Beyond.Gameplay.View.BaseModelViewPart P1)
  RVA=0x09C8B45C  token=0x6000164  System.Void __Gen_Wrap_151(System.String P0, Beyond.Montage.AnimMontageData P1)
  RVA=0x09CB3104  token=0x6000165  System.Void __Gen_Wrap_152(Beyond.Gameplay.Core.Skill.FinishType P0, System.Int32 P1)
  RVA=0x066C296C  token=0x6000166  System.Void __Gen_Wrap_153(Beyond.Gameplay.Core.Skill P0)
  RVA=0x09E21974  token=0x6000167  Beyond.Gameplay.Core.Buff __Gen_Wrap_154(Beyond.Gameplay.Core.AbilitySystem P0, Beyond.Gameplay.Core.BuffData P1, System.Collections.Generic.List<Beyond.Blackboard.DataPair> P2, System.Nullable<System.UInt64> P3)
  RVA=0x071A12AC  token=0x6000168  System.Void __Gen_Wrap_155(System.Boolean P0, Beyond.Gameplay.Core.SkillData P1)
  RVA=0x066C296C  token=0x6000169  System.Void __Gen_Wrap_156(Beyond.Gameplay.Core.Buff P0)
  RVA=0x09D27228  token=0x600016A  System.Void __Gen_Wrap_157(Beyond.Gameplay.Core.CinematicTimelineManagerBase.TimelineHandle P0, System.Boolean P1)
  RVA=0x09C8B45C  token=0x600016B  System.Void __Gen_Wrap_158(System.String P0, System.String P1)
  RVA=0x066C296C  token=0x600016C  System.Void __Gen_Wrap_159(Beyond.Gameplay.Core.Entity P0)
  RVA=0x09E21AC0  token=0x600016D  Beyond.Gameplay.MovementSetting __Gen_Wrap_160(System.String P0)
  RVA=0x09E21B9C  token=0x600016E  Beyond.Gameplay.MovementSettingModifier __Gen_Wrap_161(System.String P0)
  RVA=0x066C296C  token=0x600016F  System.Void __Gen_Wrap_162(Slate.Section P0)
  RVA=0x09C8B45C  token=0x6000170  System.Void __Gen_Wrap_163(System.String P0, System.Object P1)
  RVA=0x066C296C  token=0x6000171  System.Void __Gen_Wrap_164(UnityEngine.Texture2D[] P0)
  RVA=0x04D376B0  token=0x6000172  System.Void .cctor()
END_CLASS

CLASS: IFix.ILFixInterfaceBridge
TYPE:  class
TOKEN: 0x2000019
SIZE:  0x58
EXTENDS: IFix.Core.AnonymousStorey
IMPLEMENTS: System.IDisposable System.Collections.IEnumerator System.Collections.Generic.IEnumerator`1
FIELDS:
  private           System.Int32                    methodId_0  // 0x40
  private           System.Int32                    methodId_1  // 0x44
  private           System.Int32                    methodId_2  // 0x48
  private           System.Int32                    methodId_3  // 0x4c
  private           System.Int32                    methodId_4  // 0x50
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x09E26838
  System.Collections.IEnumerator.Current  get=0x09E268E4
METHODS:
  RVA=0x06365FC0  token=0x6000173  System.Void System.IDisposable.Dispose()
  RVA=0x06365D60  token=0x6000174  System.Boolean MoveNext()
  RVA=0x06365E98  token=0x6000176  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x09E26990  token=0x6000178  System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine)
  RVA=0x041E1670  token=0x6000179  System.Void RefAsyncBuilderStartMethod()
END_CLASS

CLASS: IFix.WrappersManagerImpl
TYPE:  class
TOKEN: 0x200001A
SIZE:  0x18
IMPLEMENTS: IFix.Core.WrappersManager
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
METHODS:
  RVA=0x053908C0  token=0x600017A  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine)
  RVA=0x09E26C54  token=0x600017B  IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id)
  RVA=0x0301FC10  token=0x600017C  System.Boolean IsPatched(System.Int32 id)
  RVA=0x09E26B10  token=0x600017D  System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon)
  RVA=0x09E26BDC  token=0x600017E  System.Object CreateWrapper(System.Int32 id)
  RVA=0x09E26CB8  token=0x600017F  System.Object InitWrapperArray(System.Int32 len)
  RVA=0x09E26A6C  token=0x6000180  IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine)
END_CLASS

CLASS: IFix.IDMAP0
TYPE:  sealed struct
TOKEN: 0x200001B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotfixDownload-RegisterSDKEvent0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotfixDownload-UnregisterSDKEvent0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotfixDownload-_IsValidHttpsUrl0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotfixDownload-_TryDecryptSignedAesField0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotfixDownload-TryDecodeDownloadText0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotfixDownload-TryApplyDownloadText0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotfixDownload-_DownloadAndApplyCoroutine0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotfixDownload-DownloadAndApply0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotfixDownload-TryProcessSDKExtraInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour1-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour1-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour1-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour1-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour1-Update0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour1-LateUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour1-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour1-FixedUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour1-OnCollisionEnter0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour1-OnCollisionExit0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour1-OnCollisionStay0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour1-OnTriggerEnter0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour1-OnTriggerExit0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour1-OnTriggerStay0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour1-OnApplicationQuit0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour1-OnApplicationFocus0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour1-OnApplicationPause0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour1-OnBecameInvisible0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour1-OnBecameVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour1-Do0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour1-Func10  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour1-Func20  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour1-Func30  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour1-Func40  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour1-Func50  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour2-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour2-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour2-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour2-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour2-Update0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour2-LateUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour2-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour2-FixedUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour2-OnCollisionEnter0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour2-OnCollisionExit0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour2-OnCollisionStay0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour2-OnTriggerEnter0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour2-OnTriggerExit0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour2-OnTriggerStay0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour2-OnApplicationQuit0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour2-OnApplicationFocus0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour2-OnApplicationPause0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour2-OnBecameInvisible0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour2-OnBecameVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour2-Do0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour2-Func10  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour2-Func20  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour2-Func30  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour2-Func40  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour2-Func50  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour3-Awake0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour3-OnEnable0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour3-OnDisable0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour3-Start0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour3-Update0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour3-LateUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour3-OnDestroy0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour3-FixedUpdate0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour3-OnCollisionEnter0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour3-OnCollisionExit0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour3-OnCollisionStay0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour3-OnTriggerEnter0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour3-OnTriggerExit0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour3-OnTriggerStay0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour3-OnApplicationQuit0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour3-OnApplicationFocus0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour3-OnApplicationPause0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour3-OnBecameInvisible0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour3-OnBecameVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour3-Do0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour3-Func10  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour3-Func20  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour3-Func30  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour3-Func40  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixMonobehaviour3-Func50  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManagerProxy-_RunOnStart0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManager-_TryApplyLuaPatchBase640  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManager-_InjectLuaStringPatchJson0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManager-_InjectLuaStringPatchLine0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManager-InjectLuaStringPatch0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManager-PatchDebugAssemble0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManager-_InjectStringPatchJson0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManager-_InjectStringPatchLine0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManager-InjectStringPatch0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManagerProxy-_ApplyDownloadPayload0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManagerProxy-_OnHotfixDownloadReady0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManagerProxy-_PatchRemoteInjectedLua0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManagerProxy-InitManager0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManager-_ClearTickFunction0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManager-_UnInit0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManager-Release0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManagerProxy-ReleaseManager0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManager-ClearPatchesForRelogin0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManagerProxy-OnReturnToLogin0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManagerProxy-RegisterHotfixSDKEvent0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManagerProxy-UnregisterHotfixSDKEvent0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManagerProxy-TryProcessHotfixSDKExtraInfo0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManager-PatchAssemble0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManager-PatchInitAssemble0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManagerProxy-PatchInitAssemble0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManagerProxy-PatchRemoteInitAssemble0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManagerProxy-PatchRemoteInjectedLua0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManager-PatchMainAssemble0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManagerProxy-PatchMainAssemble0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManagerProxy-PatchRemoteMainAssemble0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManager-get_currentPatchVersion0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManager-_ForceLocalUpdateIFix0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManager-_ForceLocalUpdateLua0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManager-Tick0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManager-_CreateTickFunction0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManager-_Init0  // const
  public    static  IFix.IDMAP0                     Beyond-HotFix-HotFixPatchManager-InjectLuaStringPatchFromUrl0  // const
METHODS:
END_CLASS

CLASS: IFix.Bridge.ILFixWarpBridge
TYPE:  static class
TOKEN: 0x200000A
SIZE:  0x10
FIELDS:
  private   static  System.Collections.Generic.List<System.Type>bridge  // static @ 0x0
METHODS:
  RVA=0x09E245CC  token=0x6000039  System.Void .cctor()
END_CLASS

