// ========================================================
// Dumped by @desirepro
// Assembly: StompyRobot.SRF.dll
// Classes:  90
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

CLASS: SRDebugUtil
TYPE:  static class
TOKEN: 0x2000002
SIZE:  0x10
FIELDS:
  public    static  System.Int32                    LineBufferCount  // const
  private   static  System.Boolean                  <IsFixedUpdate>k__BackingField  // static @ 0x0
PROPERTIES:
  IsFixedUpdate  get=0x0ACA7DF4  set=0x0ACA7E2C
METHODS:
  RVA=0x0ACA7B68  token=0x6000003  System.Void AssertNotNull(System.Object value, System.String message, UnityEngine.MonoBehaviour instance)
  RVA=0x0ACA7CEC  token=0x6000004  System.Void Assert(System.Boolean condition, System.String message, UnityEngine.MonoBehaviour instance)
  RVA=0x0ACA7DE4  token=0x6000005  System.Void EditorAssertNotNull(System.Object value, System.String message, UnityEngine.MonoBehaviour instance)
  RVA=0x0ACA7DEC  token=0x6000006  System.Void EditorAssert(System.Boolean condition, System.String message, UnityEngine.MonoBehaviour instance)
END_CLASS

CLASS: SRFileUtil
TYPE:  static class
TOKEN: 0x2000003
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0ACA8484  token=0x6000007  System.Void DeleteDirectory(System.String path)
  RVA=0x041A7E60  token=0x6000008  System.String GetBytesReadable(System.Int64 i)
END_CLASS

CLASS: SRInstantiate
TYPE:  static class
TOKEN: 0x2000004
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x6000009  T Instantiate(T prefab)
  RVA=0x0ACA8518  token=0x600000A  UnityEngine.GameObject Instantiate(UnityEngine.GameObject prefab)
  RVA=-1  // generic def  token=0x600000B  T Instantiate(T prefab, UnityEngine.Vector3 position, UnityEngine.Quaternion rotation)
END_CLASS

CLASS: EaseType
TYPE:  sealed struct
TOKEN: 0x2000006
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  SRMath.EaseType                 Linear  // const
  public    static  SRMath.EaseType                 QuadEaseOut  // const
  public    static  SRMath.EaseType                 QuadEaseIn  // const
  public    static  SRMath.EaseType                 QuadEaseInOut  // const
  public    static  SRMath.EaseType                 QuadEaseOutIn  // const
  public    static  SRMath.EaseType                 ExpoEaseOut  // const
  public    static  SRMath.EaseType                 ExpoEaseIn  // const
  public    static  SRMath.EaseType                 ExpoEaseInOut  // const
  public    static  SRMath.EaseType                 ExpoEaseOutIn  // const
  public    static  SRMath.EaseType                 CubicEaseOut  // const
  public    static  SRMath.EaseType                 CubicEaseIn  // const
  public    static  SRMath.EaseType                 CubicEaseInOut  // const
  public    static  SRMath.EaseType                 CubicEaseOutIn  // const
  public    static  SRMath.EaseType                 QuartEaseOut  // const
  public    static  SRMath.EaseType                 QuartEaseIn  // const
  public    static  SRMath.EaseType                 QuartEaseInOut  // const
  public    static  SRMath.EaseType                 QuartEaseOutIn  // const
  public    static  SRMath.EaseType                 QuintEaseOut  // const
  public    static  SRMath.EaseType                 QuintEaseIn  // const
  public    static  SRMath.EaseType                 QuintEaseInOut  // const
  public    static  SRMath.EaseType                 QuintEaseOutIn  // const
  public    static  SRMath.EaseType                 CircEaseOut  // const
  public    static  SRMath.EaseType                 CircEaseIn  // const
  public    static  SRMath.EaseType                 CircEaseInOut  // const
  public    static  SRMath.EaseType                 CircEaseOutIn  // const
  public    static  SRMath.EaseType                 SineEaseOut  // const
  public    static  SRMath.EaseType                 SineEaseIn  // const
  public    static  SRMath.EaseType                 SineEaseInOut  // const
  public    static  SRMath.EaseType                 SineEaseOutIn  // const
  public    static  SRMath.EaseType                 ElasticEaseOut  // const
  public    static  SRMath.EaseType                 ElasticEaseIn  // const
  public    static  SRMath.EaseType                 ElasticEaseInOut  // const
  public    static  SRMath.EaseType                 ElasticEaseOutIn  // const
  public    static  SRMath.EaseType                 BounceEaseOut  // const
  public    static  SRMath.EaseType                 BounceEaseIn  // const
  public    static  SRMath.EaseType                 BounceEaseInOut  // const
  public    static  SRMath.EaseType                 BounceEaseOutIn  // const
  public    static  SRMath.EaseType                 BackEaseOut  // const
  public    static  SRMath.EaseType                 BackEaseIn  // const
  public    static  SRMath.EaseType                 BackEaseInOut  // const
  public    static  SRMath.EaseType                 BackEaseOutIn  // const
METHODS:
END_CLASS

CLASS: TweenFunctions
TYPE:  static class
TOKEN: 0x2000007
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0ACACD54  token=0x600001B  System.Single Linear(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACACCEC  token=0x600001C  System.Single ExpoEaseOut(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACACC2C  token=0x600001D  System.Single ExpoEaseIn(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACACB7C  token=0x600001E  System.Single ExpoEaseInOut(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACACC8C  token=0x600001F  System.Single ExpoEaseOutIn(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACAC6DC  token=0x6000020  System.Single CircEaseOut(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACAC5E4  token=0x6000021  System.Single CircEaseIn(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACAC550  token=0x6000022  System.Single CircEaseInOut(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACAC640  token=0x6000023  System.Single CircEaseOutIn(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACACE58  token=0x6000024  System.Single QuadEaseOut(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACACDC4  token=0x6000025  System.Single QuadEaseIn(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACACD64  token=0x6000026  System.Single QuadEaseInOut(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACACDDC  token=0x6000027  System.Single QuadEaseOutIn(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACAD2F8  token=0x6000028  System.Single SineEaseOut(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACAD200  token=0x6000029  System.Single SineEaseIn(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACAD16C  token=0x600002A  System.Single SineEaseInOut(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACAD24C  token=0x600002B  System.Single SineEaseOutIn(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACAC844  token=0x600002C  System.Single CubicEaseOut(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACAC798  token=0x600002D  System.Single CubicEaseIn(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACAC728  token=0x600002E  System.Single CubicEaseInOut(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACAC7B4  token=0x600002F  System.Single CubicEaseOutIn(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACACFBC  token=0x6000030  System.Single QuartEaseOut(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACACEFC  token=0x6000031  System.Single QuartEaseIn(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACACE7C  token=0x6000032  System.Single QuartEaseInOut(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACACF1C  token=0x6000033  System.Single QuartEaseOutIn(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACAD138  token=0x6000034  System.Single QuintEaseOut(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACAD074  token=0x6000035  System.Single QuintEaseIn(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACACFF4  token=0x6000036  System.Single QuintEaseInOut(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACAD098  token=0x6000037  System.Single QuintEaseOutIn(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACACABC  token=0x6000038  System.Single ElasticEaseOut(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACAC990  token=0x6000039  System.Single ElasticEaseIn(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACAC870  token=0x600003A  System.Single ElasticEaseInOut(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACACA5C  token=0x600003B  System.Single ElasticEaseOutIn(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACAC490  token=0x600003C  System.Single BounceEaseOut(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACAC3B4  token=0x600003D  System.Single BounceEaseIn(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACAC31C  token=0x600003E  System.Single BounceEaseInOut(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACAC400  token=0x600003F  System.Single BounceEaseOutIn(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACAC2E0  token=0x6000040  System.Single BackEaseOut(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACAC204  token=0x6000041  System.Single BackEaseIn(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACAC17C  token=0x6000042  System.Single BackEaseInOut(System.Single t, System.Single b, System.Single c, System.Single d)
  RVA=0x0ACAC230  token=0x6000043  System.Single BackEaseOutIn(System.Single t, System.Single b, System.Single c, System.Single d)
END_CLASS

CLASS: SRMath
TYPE:  static class
TOKEN: 0x2000005
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x04DA9EB0  token=0x600000C  System.Single LerpUnclamped(System.Single from, System.Single to, System.Single t)
  RVA=0x04DA9E50  token=0x600000D  UnityEngine.Vector3 LerpUnclamped(UnityEngine.Vector3 from, UnityEngine.Vector3 to, System.Single t)
  RVA=0x0ACA8CBC  token=0x600000E  System.Single FacingNormalized(UnityEngine.Vector3 dir1, UnityEngine.Vector3 dir2)
  RVA=0x0ACA9044  token=0x600000F  System.Single WrapAngle(System.Single angle)
  RVA=0x0ACA8D2C  token=0x6000010  System.Single NearestAngle(System.Single to, System.Single angle1, System.Single angle2)
  RVA=0x0ACA906C  token=0x6000011  System.Int32 Wrap(System.Int32 max, System.Int32 value)
  RVA=0x0ACA9100  token=0x6000012  System.Single Wrap(System.Single max, System.Single value)
  RVA=0x04DA9E40  token=0x6000013  System.Single Average(System.Single v1, System.Single v2)
  RVA=0x0ACA856C  token=0x6000014  System.Single Angle(UnityEngine.Vector2 direction)
  RVA=0x0ACA8630  token=0x6000015  System.Single Ease(System.Single from, System.Single to, System.Single t, SRMath.EaseType type)
  RVA=0x0ACA8F7C  token=0x6000016  System.Single SpringLerp(System.Single strength, System.Single deltaTime)
  RVA=0x0ACA8F3C  token=0x6000017  System.Single SpringLerp(System.Single from, System.Single to, System.Single strength, System.Single deltaTime)
  RVA=0x0ACA8EB4  token=0x6000018  UnityEngine.Vector3 SpringLerp(UnityEngine.Vector3 from, UnityEngine.Vector3 to, System.Single strength, System.Single deltaTime)
  RVA=0x0ACA8FCC  token=0x6000019  UnityEngine.Quaternion SpringLerp(UnityEngine.Quaternion from, UnityEngine.Quaternion to, System.Single strength, System.Single deltaTime)
  RVA=0x0ACA8D9C  token=0x600001A  System.Single SmoothClamp(System.Single value, System.Single min, System.Single max, System.Single scrollMax, SRMath.EaseType easeType)
END_CLASS

CLASS: TOKEN
TYPE:  sealed struct
TOKEN: 0x200000A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  SRF.Json.Parser.TOKEN           NONE  // const
  public    static  SRF.Json.Parser.TOKEN           CURLY_OPEN  // const
  public    static  SRF.Json.Parser.TOKEN           CURLY_CLOSE  // const
  public    static  SRF.Json.Parser.TOKEN           SQUARED_OPEN  // const
  public    static  SRF.Json.Parser.TOKEN           SQUARED_CLOSE  // const
  public    static  SRF.Json.Parser.TOKEN           COLON  // const
  public    static  SRF.Json.Parser.TOKEN           COMMA  // const
  public    static  SRF.Json.Parser.TOKEN           STRING  // const
  public    static  SRF.Json.Parser.TOKEN           NUMBER  // const
  public    static  SRF.Json.Parser.TOKEN           TRUE  // const
  public    static  SRF.Json.Parser.TOKEN           FALSE  // const
  public    static  SRF.Json.Parser.TOKEN           NULL  // const
METHODS:
END_CLASS

CLASS: Parser
TYPE:  sealed class
TOKEN: 0x2000009
SIZE:  0x18
IMPLEMENTS: System.IDisposable
FIELDS:
  private   static  System.String                   WORD_BREAK  // const
  private           System.IO.StringReader          json  // 0x10
PROPERTIES:
  PeekChar  get=0x0ACA6DB4
  NextChar  get=0x0ACA6A84
  NextWord  get=0x0ACA6D04
  NextToken  get=0x0ACA6AE0
METHODS:
  RVA=0x0ACA6A0C  token=0x6000046  System.Void .ctor(System.String jsonString)
  RVA=0x09D2CEB4  token=0x600004B  System.Void Dispose()
  RVA=0x0ACA6300  token=0x600004C  System.Boolean IsWordBreak(System.Char c)
  RVA=0x0ACA6914  token=0x600004D  System.Object Parse(System.String jsonString)
  RVA=0x0ACA65B8  token=0x600004E  System.Collections.Generic.Dictionary<System.String,System.Object> ParseObject()
  RVA=0x0ACA6378  token=0x600004F  System.Collections.Generic.List<System.Object> ParseArray()
  RVA=0x0ACA68F0  token=0x6000050  System.Object ParseValue()
  RVA=0x0ACA6440  token=0x6000051  System.Object ParseByToken(SRF.Json.Parser.TOKEN token)
  RVA=0x0ACA66C4  token=0x6000052  System.String ParseString()
  RVA=0x0ACA64F4  token=0x6000053  System.Object ParseNumber()
  RVA=0x0ACA6270  token=0x6000054  System.Void EatWhitespace()
END_CLASS

CLASS: Serializer
TYPE:  sealed class
TOKEN: 0x200000B
SIZE:  0x18
FIELDS:
  private           System.Text.StringBuilder       builder  // 0x10
METHODS:
  RVA=0x0ACAB470  token=0x6000055  System.Void .ctor()
  RVA=0x0ACAB3F8  token=0x6000056  System.String Serialize(System.Object obj)
  RVA=0x0ACAB274  token=0x6000057  System.Void SerializeValue(System.Object value)
  RVA=0x0ACAA994  token=0x6000058  System.Void SerializeObject(System.Collections.IDictionary obj)
  RVA=0x0ACAA610  token=0x6000059  System.Void SerializeArray(System.Collections.IList anArray)
  RVA=0x0ACAAFFC  token=0x600005A  System.Void SerializeString(System.String str)
  RVA=0x0ACAAD9C  token=0x600005B  System.Void SerializeOther(System.Object value)
END_CLASS

CLASS: <GetEnumerator>d__15
TYPE:  sealed class
TOKEN: 0x200000D
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           T                               <>2__current  // 0x0
  public            SRF.SRList<T>                   <>4__this  // 0x0
  private           System.Int32                    <i>5__2  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<T>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600007A  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x600007B  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x600007C  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x600007E  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: FieldInfo
TYPE:  sealed struct
TOKEN: 0x2000012
SIZE:  0x30
FIELDS:
  public            System.Boolean                  AutoCreate  // 0x10
  public            System.Boolean                  AutoSet  // 0x11
  public            System.Reflection.FieldInfo     Field  // 0x18
  public            System.Boolean                  Import  // 0x20
  public            System.Type                     ImportType  // 0x28
METHODS:
END_CLASS

CLASS: <WaitForSecondsRealTime>d__0
TYPE:  sealed class
TOKEN: 0x2000014
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Object                   <>2__current  // 0x18
  public            System.Single                   time  // 0x20
  private           System.Single                   <endTime>5__2  // 0x24
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x014F51F0  token=0x60000A2  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60000A3  System.Void System.IDisposable.Dispose()
  RVA=0x0ACAD688  token=0x60000A4  System.Boolean MoveNext()
  RVA=0x0ACAD6EC  token=0x60000A6  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <GetChildren>d__0
TYPE:  sealed class
TOKEN: 0x200001A
SIZE:  0x40
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           UnityEngine.Transform           <>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x20
  private           UnityEngine.Transform           t  // 0x28
  public            UnityEngine.Transform           <>3__t  // 0x30
  private           System.Int32                    <i>5__2  // 0x38
PROPERTIES:
  System.Collections.Generic.IEnumerator<UnityEngine.Transform>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x03A11010  token=0x60000BE  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60000BF  System.Void System.IDisposable.Dispose()
  RVA=0x0ACAD334  token=0x60000C0  System.Boolean MoveNext()
  RVA=0x0ACAD44C  token=0x60000C2  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x0ACAD3B0  token=0x60000C4  System.Collections.Generic.IEnumerator<UnityEngine.Transform> System.Collections.Generic.IEnumerable<UnityEngine.Transform>.GetEnumerator()
  RVA=0x0ACAD444  token=0x60000C5  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: Operations
TYPE:  sealed struct
TOKEN: 0x2000020
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  SRF.UI.CopyPreferredSizes.OperationsMax  // const
  public    static  SRF.UI.CopyPreferredSizes.OperationsMin  // const
METHODS:
END_CLASS

CLASS: CopySource
TYPE:  class
TOKEN: 0x2000021
SIZE:  0x20
FIELDS:
  public            UnityEngine.RectTransform       Rect  // 0x10
  public            System.Single                   PaddingHeight  // 0x18
  public            System.Single                   PaddingWidth  // 0x1c
METHODS:
  RVA=0x041E1670  token=0x60000EE  System.Void .ctor()
END_CLASS

CLASS: Modes
TYPE:  sealed struct
TOKEN: 0x2000029
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  SRF.UI.ResponsiveEnable.Modes   EnableAbove  // const
  public    static  SRF.UI.ResponsiveEnable.Modes   EnableBelow  // const
METHODS:
END_CLASS

CLASS: Entry
TYPE:  sealed struct
TOKEN: 0x200002A
SIZE:  0x30
FIELDS:
  public            UnityEngine.Behaviour[]         Components  // 0x10
  public            UnityEngine.GameObject[]        GameObjects  // 0x18
  public            SRF.UI.ResponsiveEnable.Modes   Mode  // 0x20
  public            System.Single                   ThresholdHeight  // 0x24
  public            System.Single                   ThresholdWidth  // 0x28
METHODS:
END_CLASS

CLASS: SizeDefinition
TYPE:  sealed struct
TOKEN: 0x200002D
SIZE:  0x18
FIELDS:
  public            System.Single                   ElementWidth  // 0x10
  public            System.Single                   ThresholdWidth  // 0x14
METHODS:
END_CLASS

CLASS: Element
TYPE:  sealed struct
TOKEN: 0x200002C
SIZE:  0x20
FIELDS:
  public            SRF.UI.ResponsiveResize.Element.SizeDefinition[]SizeDefinitions  // 0x10
  public            UnityEngine.RectTransform       Target  // 0x18
METHODS:
END_CLASS

CLASS: SpinEvent
TYPE:  class
TOKEN: 0x2000033
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent
FIELDS:
METHODS:
  RVA=0x05393538  token=0x6000140  System.Void .ctor()
END_CLASS

CLASS: SelectedItemChangedEvent
TYPE:  class
TOKEN: 0x200003D
SIZE:  0x30
EXTENDS: UnityEngine.Events.UnityEvent`1
FIELDS:
METHODS:
  RVA=0x0ACAA5D4  token=0x6000189  System.Void .ctor()
END_CLASS

CLASS: Row
TYPE:  class
TOKEN: 0x200003E
SIZE:  0x38
FIELDS:
  public            System.Object                   Data  // 0x10
  public            System.Int32                    Index  // 0x18
  public            UnityEngine.RectTransform       Rect  // 0x20
  public            SRF.UI.StyleRoot                Root  // 0x28
  public            SRF.UI.Layout.IVirtualView      View  // 0x30
METHODS:
  RVA=0x041E1670  token=0x600018A  System.Void .ctor()
END_CLASS

CLASS: <LoadDependencies>d__8
TYPE:  sealed class
TOKEN: 0x2000044
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           System.Object                   <>2__current  // 0x0
  public            SRF.Service.SRDependencyServiceBase<T><>4__this  // 0x0
  private           System.Type[]                   <>7__wrap1  // 0x0
  private           System.Int32                    <>7__wrap2  // 0x0
  private           SRF.Service.IAsyncService       <a>5__4  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600019C  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x600019D  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x600019E  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x60001A0  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <LoadCoroutine>d__11
TYPE:  sealed class
TOKEN: 0x2000046
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           System.Object                   <>2__current  // 0x0
  public            SRF.Service.SRSceneServiceBase<T,TImpl><>4__this  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Object>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60001AB  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x60001AC  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x60001AD  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x60001AF  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: Service
TYPE:  class
TOKEN: 0x2000049
SIZE:  0x20
FIELDS:
  public            System.Object                   Object  // 0x10
  public            System.Type                     Type  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60001CA  System.Void .ctor()
END_CLASS

CLASS: ServiceStub
TYPE:  class
TOKEN: 0x200004A
SIZE:  0x30
FIELDS:
  public            System.Func<System.Object>      Constructor  // 0x10
  public            System.Type                     InterfaceType  // 0x18
  public            System.Func<System.Type>        Selector  // 0x20
  public            System.Type                     Type  // 0x28
METHODS:
  RVA=0x0ACAB4D4  token=0x60001CB  System.String ToString()
  RVA=0x041E1670  token=0x60001CC  System.Void .ctor()
END_CLASS

CLASS: <>c
TYPE:  sealed class
TOKEN: 0x200004B
SIZE:  0x10
FIELDS:
  public    static readonly SRF.Service.SRServiceManager.<>c<>9  // static @ 0x0
  public    static  System.Func<SRF.Service.SRServiceManager.ServiceStub,System.String><>9__21_0  // static @ 0x8
METHODS:
  RVA=0x0ACAD624  token=0x60001CD  System.Void .cctor()
  RVA=0x041E1670  token=0x60001CE  System.Void .ctor()
  RVA=0x0ACAD498  token=0x60001CF  System.String <UpdateStubs>b__21_0(SRF.Service.SRServiceManager.ServiceStub p)
END_CLASS

CLASS: <>c__DisplayClass26_0
TYPE:  sealed class
TOKEN: 0x200004C
SIZE:  0x18
FIELDS:
  public            SRF.Service.ServiceSelectorAttributeattrib  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001D0  System.Void .ctor()
  RVA=0x04CCAA20  token=0x60001D1  System.Boolean <ScanTypeForSelectors>b__0(SRF.Service.SRServiceManager.ServiceStub p)
END_CLASS

CLASS: <>c__DisplayClass27_0
TYPE:  sealed class
TOKEN: 0x200004D
SIZE:  0x20
FIELDS:
  public            SRF.Service.ServiceConstructorAttributeattrib  // 0x10
  public            System.Reflection.MethodInfo    m  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60001D2  System.Void .ctor()
  RVA=0x0ACAD570  token=0x60001D3  System.Boolean <ScanTypeForConstructors>b__0(SRF.Service.SRServiceManager.ServiceStub p)
  RVA=0x0ACAD5E0  token=0x60001D4  System.Object <ScanTypeForConstructors>b__1()
END_CLASS

CLASS: <>c__DisplayClass1_0
TYPE:  sealed class
TOKEN: 0x2000050
SIZE:  0x20
FIELDS:
  public            System.Reflection.MethodInfo    method  // 0x10
  public            System.Object                   target  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60001D9  System.Void .ctor()
  RVA=0x0ACAD544  token=0x60001DA  System.Object <.ctor>b__0(System.Object[] o)
END_CLASS

CLASS: <>c__DisplayClass4_0
TYPE:  sealed class
TOKEN: 0x2000051
SIZE:  0x18
FIELDS:
  public            System.Action                   action  // 0x10
METHODS:
  RVA=0x041E1670  token=0x60001DB  System.Void .ctor()
  RVA=0x0ACAD604  token=0x60001DC  System.Object <op_Implicit>b__0(System.Object[] args)
END_CLASS

CLASS: <>c__DisplayClass9_0`1
TYPE:  sealed class
TOKEN: 0x2000054
FIELDS:
  public            System.Action<T>                setter  // 0x0
  public            System.Func<T>                  getter  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60001F0  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60001F1  System.Void <FromLambda>b__0(System.Object o)
  RVA=-1  // not resolved  token=0x60001F2  System.Object <FromLambda>b__1()
END_CLASS

CLASS: <>c__DisplayClass10_0
TYPE:  sealed class
TOKEN: 0x2000055
SIZE:  0x20
FIELDS:
  public            System.Object                   target  // 0x10
  public            System.Reflection.PropertyInfo  property  // 0x18
METHODS:
  RVA=0x041E1670  token=0x60001F3  System.Void .ctor()
  RVA=0x0ACAD520  token=0x60001F4  System.Object <.ctor>b__0()
  RVA=0x0ACAD530  token=0x60001F5  System.Void <.ctor>b__1(System.Object v)
END_CLASS

CLASS: <>c__23`1
TYPE:  sealed class
TOKEN: 0x2000056
FIELDS:
  public    static readonly SRF.Helpers.PropertyReference.<>c__23<T><>9  // static @ 0x0
  public    static  System.Func<System.Attribute,System.Boolean><>9__23_0  // static @ 0x0
METHODS:
  RVA=-1  // not resolved  token=0x60001F6  System.Void .cctor()
  RVA=-1  // not resolved  token=0x60001F7  System.Void .ctor()
  RVA=-1  // not resolved  token=0x60001F8  System.Boolean <GetAttribute>b__23_0(System.Attribute p)
END_CLASS

CLASS: SRF.Json
TYPE:  static class
TOKEN: 0x2000008
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0ACA5F0C  token=0x6000044  System.Object Deserialize(System.String json)
  RVA=0x0ACA5F28  token=0x6000045  System.String Serialize(System.Object obj)
END_CLASS

CLASS: SRF.SRList`1
TYPE:  class
TOKEN: 0x200000C
IMPLEMENTS: System.Collections.Generic.IList`1 System.Collections.Generic.ICollection`1 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable UnityEngine.ISerializationCallbackReceiver
FIELDS:
  private           T[]                             _buffer  // 0x0
  private           System.Int32                    _count  // 0x0
  private           System.Collections.Generic.EqualityComparer<T>_equalityComparer  // 0x0
  private           System.Collections.ObjectModel.ReadOnlyCollection<T>_readOnlyWrapper  // 0x0
PROPERTIES:
  Buffer  get=-1  // not resolved  set=-1  // not resolved
  EqualityComparer  get=-1  // not resolved
  Count  get=-1  // not resolved  set=-1  // not resolved
  IsReadOnly  get=-1  // not resolved
  Item  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600005C  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600005D  System.Void .ctor(System.Int32 capacity)
  RVA=-1  // not resolved  token=0x600005E  System.Void .ctor(System.Collections.Generic.IEnumerable<T> source)
  RVA=-1  // not resolved  token=0x6000064  System.Collections.Generic.IEnumerator<T> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000065  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000066  System.Void Add(T item)
  RVA=-1  // not resolved  token=0x6000067  System.Void Clear()
  RVA=-1  // not resolved  token=0x6000068  System.Boolean Contains(T item)
  RVA=-1  // not resolved  token=0x6000069  System.Void CopyTo(T[] array, System.Int32 arrayIndex)
  RVA=-1  // not resolved  token=0x600006A  System.Boolean Remove(T item)
  RVA=-1  // not resolved  token=0x600006C  System.Int32 IndexOf(T item)
  RVA=-1  // not resolved  token=0x600006D  System.Void Insert(System.Int32 index, T item)
  RVA=-1  // not resolved  token=0x600006E  System.Void RemoveAt(System.Int32 index)
  RVA=-1  // not resolved  token=0x6000071  System.Void OnBeforeSerialize()
  RVA=-1  // not resolved  token=0x6000072  System.Void OnAfterDeserialize()
  RVA=-1  // not resolved  token=0x6000073  System.Void AddRange(System.Collections.Generic.IEnumerable<T> range)
  RVA=-1  // not resolved  token=0x6000074  System.Void Clear(System.Boolean clean)
  RVA=-1  // not resolved  token=0x6000075  System.Void Clean()
  RVA=-1  // not resolved  token=0x6000076  System.Collections.ObjectModel.ReadOnlyCollection<T> AsReadOnly()
  RVA=-1  // not resolved  token=0x6000077  System.Void Expand()
  RVA=-1  // not resolved  token=0x6000078  System.Void Trim()
  RVA=-1  // not resolved  token=0x6000079  System.Void Sort(System.Comparison<T> comparer)
END_CLASS

CLASS: SRF.SRMonoBehaviour
TYPE:  abstract class
TOKEN: 0x200000E
SIZE:  0x48
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.Collider            _collider  // 0x18
  private           UnityEngine.Transform           _transform  // 0x20
  private           UnityEngine.Rigidbody           _rigidBody  // 0x28
  private           UnityEngine.GameObject          _gameObject  // 0x30
  private           UnityEngine.Rigidbody2D         _rigidbody2D  // 0x38
  private           UnityEngine.Collider2D          _collider2D  // 0x40
PROPERTIES:
  CachedTransform  get=0x03A47940
  CachedCollider  get=0x0ACA91DC
  CachedCollider2D  get=0x0ACA9158
  CachedRigidBody  get=0x0ACA92E4
  CachedRigidBody2D  get=0x0ACA9260
  CachedGameObject  get=0x03A47890
  transform  get=0x0ACA9368
METHODS:
  RVA=0x0ACA9124  token=0x6000087  System.Void AssertNotNull(System.Object value, System.String fieldName)
  RVA=0x0ACA913C  token=0x6000088  System.Void Assert(System.Boolean condition, System.String message)
  RVA=0x0ACA9124  token=0x6000089  System.Void EditorAssertNotNull(System.Object value, System.String fieldName)
  RVA=0x0ACA913C  token=0x600008A  System.Void EditorAssert(System.Boolean condition, System.String message)
  RVA=0x05393520  token=0x600008B  System.Void .ctor()
END_CLASS

CLASS: SRF.RequiredFieldAttribute
TYPE:  sealed class
TOKEN: 0x200000F
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  private           System.Boolean                  _autoCreate  // 0x10
  private           System.Boolean                  _autoSearch  // 0x11
  private           System.Boolean                  _editorOnly  // 0x12
PROPERTIES:
  AutoSearch  get=0x04D88380  set=0x04D88460
  AutoCreate  get=0x015EFCE0  set=0x04D86130
  EditorOnly  get=0x04D88390  set=0x04DA0900
METHODS:
  RVA=0x04DA9E30  token=0x600008C  System.Void .ctor(System.Boolean autoSearch)
  RVA=0x04D99700  token=0x600008D  System.Void .ctor()
END_CLASS

CLASS: SRF.ImportAttribute
TYPE:  class
TOKEN: 0x2000010
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public    readonly System.Type                     Service  // 0x10
METHODS:
  RVA=0x041E1670  token=0x6000094  System.Void .ctor()
  RVA=0x053908C0  token=0x6000095  System.Void .ctor(System.Type serviceType)
END_CLASS

CLASS: SRF.SRMonoBehaviourEx
TYPE:  abstract class
TOKEN: 0x2000011
SIZE:  0x48
EXTENDS: SRF.SRMonoBehaviour
FIELDS:
  private   static  System.Collections.Generic.Dictionary<System.Type,System.Collections.Generic.IList<SRF.SRMonoBehaviourEx.FieldInfo>>_checkedFields  // static @ 0x0
METHODS:
  RVA=0x04582280  token=0x6000096  System.Void CheckFields(SRF.SRMonoBehaviourEx instance, System.Boolean justSet)
  RVA=0x045825B0  token=0x6000097  System.Void PopulateObject(System.Collections.Generic.IList<SRF.SRMonoBehaviourEx.FieldInfo> cache, SRF.SRMonoBehaviourEx instance, System.Boolean justSet)
  RVA=0x04582420  token=0x6000098  System.Collections.Generic.List<SRF.SRMonoBehaviourEx.FieldInfo> ScanType(System.Type t)
  RVA=0x05398080  token=0x6000099  System.Void Awake()
  RVA=0x041E1670  token=0x600009A  System.Void Start()
  RVA=0x041E1670  token=0x600009B  System.Void Update()
  RVA=0x041E1670  token=0x600009C  System.Void FixedUpdate()
  RVA=0x041E1670  token=0x600009D  System.Void OnEnable()
  RVA=0x041E1670  token=0x600009E  System.Void OnDisable()
  RVA=0x041E1670  token=0x600009F  System.Void OnDestroy()
  RVA=0x05393520  token=0x60000A0  System.Void .ctor()
END_CLASS

CLASS: SRF.Coroutines
TYPE:  static class
TOKEN: 0x2000013
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0ACA49C8  token=0x60000A1  System.Collections.IEnumerator WaitForSecondsRealTime(System.Single time)
END_CLASS

CLASS: SRF.SRFFloatExtensions
TYPE:  static class
TOKEN: 0x2000015
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x04D886E0  token=0x60000A8  System.Single Sqr(System.Single f)
  RVA=0x04D88670  token=0x60000A9  System.Single SqrRt(System.Single f)
  RVA=0x0ACA7E68  token=0x60000AA  System.Boolean ApproxZero(System.Single f)
  RVA=0x02FAEEA0  token=0x60000AB  System.Boolean Approx(System.Single f, System.Single f2)
END_CLASS

CLASS: SRF.SRFGameObjectExtensions
TYPE:  static class
TOKEN: 0x2000016
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x60000AC  T GetIComponent(UnityEngine.GameObject t)
  RVA=-1  // generic def  token=0x60000AD  T GetComponentOrAdd(UnityEngine.GameObject obj)
  RVA=-1  // generic def  token=0x60000AE  System.Void RemoveComponentIfExists(UnityEngine.GameObject obj)
  RVA=-1  // generic def  token=0x60000AF  System.Void RemoveComponentsIfExists(UnityEngine.GameObject obj)
  RVA=-1  // generic def  token=0x60000B0  System.Boolean EnableComponentIfExists(UnityEngine.GameObject obj, System.Boolean enable)
  RVA=0x0ACA8050  token=0x60000B1  System.Void SetLayerRecursive(UnityEngine.GameObject o, System.Int32 layer)
  RVA=0x0ACA7EB8  token=0x60000B2  System.Void SetLayerInternal(UnityEngine.Transform t, System.Int32 layer)
END_CLASS

CLASS: SRF.SRFIListExtensions
TYPE:  static class
TOKEN: 0x2000017
SIZE:  0x10
FIELDS:
METHODS:
  RVA=-1  // generic def  token=0x60000B3  T Random(System.Collections.Generic.IList<T> list)
  RVA=-1  // generic def  token=0x60000B4  T RandomOrDefault(System.Collections.Generic.IList<T> list)
  RVA=-1  // generic def  token=0x60000B5  T PopLast(System.Collections.Generic.IList<T> list)
END_CLASS

CLASS: SRF.SRFStringExtensions
TYPE:  static class
TOKEN: 0x2000018
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x05397134  token=0x60000B6  System.String Fmt(System.String formatString, System.Object[] args)
END_CLASS

CLASS: SRF.SRFTransformExtensions
TYPE:  static class
TOKEN: 0x2000019
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0ACA82D8  token=0x60000B7  System.Collections.Generic.IEnumerable<UnityEngine.Transform> GetChildren(UnityEngine.Transform t)
  RVA=0x0ACA83D0  token=0x60000B8  System.Void ResetLocal(UnityEngine.Transform t)
  RVA=0x0ACA807C  token=0x60000B9  UnityEngine.GameObject CreateChild(UnityEngine.Transform t, System.String name)
  RVA=0x0ACA8464  token=0x60000BA  System.Void SetParentMaintainLocals(UnityEngine.Transform t, UnityEngine.Transform parent)
  RVA=0x0AC7A890  token=0x60000BB  System.Void SetLocals(UnityEngine.Transform t, UnityEngine.Transform from)
  RVA=0x0ACA8348  token=0x60000BC  System.Void Match(UnityEngine.Transform t, UnityEngine.Transform from)
  RVA=0x0ACA8144  token=0x60000BD  System.Void DestroyChildren(UnityEngine.Transform t)
END_CLASS

CLASS: SRF.Hierarchy
TYPE:  class
TOKEN: 0x200001B
SIZE:  0x10
FIELDS:
  private   static readonly System.Char[]                   Seperator  // static @ 0x0
  private   static readonly System.Collections.Generic.Dictionary<System.String,UnityEngine.Transform>Cache  // static @ 0x8
PROPERTIES:
  Item  get=0x0ACA5D84
METHODS:
  RVA=0x03A479F0  token=0x60000C7  UnityEngine.Transform Get(System.String key)
  RVA=0x04D345A0  token=0x60000C8  System.Void RuntimeInitialize()
  RVA=0x041E1670  token=0x60000C9  System.Void .ctor()
  RVA=0x04CA6460  token=0x60000CA  System.Void .cctor()
END_CLASS

CLASS: SRF.Components.SRAutoSingleton`1
TYPE:  abstract class
TOKEN: 0x2000058
EXTENDS: SRF.SRMonoBehaviour
FIELDS:
  private   static  T                               _instance  // static @ 0x0
PROPERTIES:
  Instance  get=-1  // not resolved
  HasInstance  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60001FE  System.Void Awake()
  RVA=-1  // not resolved  token=0x60001FF  System.Void OnEnable()
  RVA=-1  // not resolved  token=0x6000200  System.Void OnApplicationQuit()
  RVA=-1  // not resolved  token=0x6000201  System.Void .ctor()
END_CLASS

CLASS: SRF.Components.SRSingleton`1
TYPE:  abstract class
TOKEN: 0x2000059
EXTENDS: SRF.SRMonoBehaviour
FIELDS:
  private   static  T                               _instance  // static @ 0x0
PROPERTIES:
  Instance  get=-1  // not resolved
  HasInstance  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000204  System.Void Register()
  RVA=-1  // not resolved  token=0x6000205  System.Void Awake()
  RVA=-1  // not resolved  token=0x6000206  System.Void OnEnable()
  RVA=-1  // not resolved  token=0x6000207  System.Void OnApplicationQuit()
  RVA=-1  // not resolved  token=0x6000208  System.Void .ctor()
END_CLASS

CLASS: SRF.Helpers.AssetUtil
TYPE:  static class
TOKEN: 0x200004E
SIZE:  0x10
FIELDS:
METHODS:
END_CLASS

CLASS: SRF.Helpers.MethodReference
TYPE:  sealed class
TOKEN: 0x200004F
SIZE:  0x18
FIELDS:
  private   readonly System.Func<System.Object[],System.Object>_method  // 0x10
METHODS:
  RVA=0x0ACA60C0  token=0x60001D5  System.Void .ctor(System.Object target, System.Reflection.MethodInfo method)
  RVA=0x053908C0  token=0x60001D6  System.Void .ctor(System.Func<System.Object[],System.Object> method)
  RVA=0x09B1C05C  token=0x60001D7  System.Object Invoke(System.Object[] parameters)
  RVA=0x0ACA61A0  token=0x60001D8  SRF.Helpers.MethodReference op_Implicit(System.Action action)
END_CLASS

CLASS: SRF.Helpers.PropertyValueChangedHandler
TYPE:  sealed class
TOKEN: 0x2000052
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x082A4F84  token=0x60001DD  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02B46B90  token=0x60001DE  System.Void Invoke(SRF.Helpers.PropertyReference property)
  RVA=0x047FDD70  token=0x60001DF  System.IAsyncResult BeginInvoke(SRF.Helpers.PropertyReference property, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x60001E0  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: SRF.Helpers.PropertyReference
TYPE:  sealed class
TOKEN: 0x2000053
SIZE:  0x48
FIELDS:
  private   readonly System.Reflection.PropertyInfo  _property  // 0x10
  private   readonly System.Object                   _target  // 0x18
  private   readonly System.Attribute[]              _attributes  // 0x20
  private   readonly System.Func<System.Object>      _getter  // 0x28
  private   readonly System.Action<System.Object>    _setter  // 0x30
  private           System.Collections.Generic.List<SRF.Helpers.PropertyValueChangedHandler>_valueChangedListeners  // 0x38
  private           System.Type                     <PropertyType>k__BackingField  // 0x40
PROPERTIES:
  PropertyType  get=0x04D85A60  set=0x042B4AE0
  CanRead  get=0x068C60A8
  CanWrite  get=0x09B11948
EVENTS:
  ValueChanged  add=add_ValueChanged  remove=remove_ValueChanged
METHODS:
  RVA=-1  // generic def  token=0x60001E3  SRF.Helpers.PropertyReference FromLambda(System.Func<T> getter, System.Action<T> setter, System.Attribute[] attributes)
  RVA=0x0ACA7148  token=0x60001E4  System.Void .ctor(System.Object target, System.Reflection.PropertyInfo property)
  RVA=0x0ACA731C  token=0x60001E5  System.Void .ctor(System.Type type, System.Func<System.Object> getter, System.Action<System.Object> setter, System.Attribute[] attributes)
  RVA=0x0ACA6E28  token=0x60001EA  System.Void NotifyValueChanged()
  RVA=0x0ACA6E10  token=0x60001EB  System.Object GetValue()
  RVA=0x0ACA6FB4  token=0x60001EC  System.Void SetValue(System.Object value)
  RVA=-1  // generic def  token=0x60001ED  T GetAttribute()
  RVA=0x0ACA6F38  token=0x60001EE  System.Void OnTargetPropertyChanged(System.Object sender, System.ComponentModel.PropertyChangedEventArgs e)
  RVA=0x0ACA702C  token=0x60001EF  System.String ToString()
END_CLASS

CLASS: SRF.Helpers.SRReflection
TYPE:  static class
TOKEN: 0x2000057
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0ACA9B48  token=0x60001F9  System.Void SetPropertyValue(System.Object obj, System.Reflection.PropertyInfo p, System.Object value)
  RVA=0x0ACA9B0C  token=0x60001FA  System.Object GetPropertyValue(System.Object obj, System.Reflection.PropertyInfo p)
  RVA=-1  // generic def  token=0x60001FB  T GetAttribute(System.Reflection.MemberInfo t)
END_CLASS

CLASS: SRF.Internal.ComponentMenuPaths
TYPE:  static class
TOKEN: 0x200005A
SIZE:  0x10
FIELDS:
  public    static  System.String                   PathRoot  // const
  public    static  System.String                   SRServiceManager  // const
  public    static  System.String                   BehavioursRoot  // const
  public    static  System.String                   DestroyOnDisable  // const
  public    static  System.String                   DontDestroyOnLoad  // const
  public    static  System.String                   MatchTransform  // const
  public    static  System.String                   LookAt  // const
  public    static  System.String                   MatchForwardDirection  // const
  public    static  System.String                   MatchMainCameraForwardDirection  // const
  public    static  System.String                   RuntimePosition  // const
  public    static  System.String                   ScrollTexture  // const
  public    static  System.String                   SmoothFloatBehaviour  // const
  public    static  System.String                   SmoothFollow2D  // const
  public    static  System.String                   SpringFollow  // const
  public    static  System.String                   SmoothMatchTransform  // const
  public    static  System.String                   SpawnPrefab  // const
  public    static  System.String                   Velocity  // const
  public    static  System.String                   SmoothOscillate  // const
  public    static  System.String                   SRDebugCamera  // const
  public    static  System.String                   ComponentsRoot  // const
  public    static  System.String                   SRLineRenderer  // const
  public    static  System.String                   SelectionRoot  // const
  public    static  System.String                   SRSpriteFadeRenderer  // const
  public    static  System.String                   SRMaterialFadeRenderer  // const
  public    static  System.String                   SRCompositeFadeRenderer  // const
  public    static  System.String                   UIRoot  // const
  public    static  System.String                   TiltOnTouch  // const
  public    static  System.String                   ScaleOnTouch  // const
  public    static  System.String                   InheritColour  // const
  public    static  System.String                   FlashGraphic  // const
  public    static  System.String                   CopyPreferredSize  // const
  public    static  System.String                   CopyPreferredSizes  // const
  public    static  System.String                   CopyLayoutElement  // const
  public    static  System.String                   CopySizeIntoLayoutElement  // const
  public    static  System.String                   SRText  // const
  public    static  System.String                   Unselectable  // const
  public    static  System.String                   LongPressButton  // const
  public    static  System.String                   ScrollToBottom  // const
  public    static  System.String                   FlowLayoutGroup  // const
  public    static  System.String                   VirtualVerticalLayoutGroup  // const
  public    static  System.String                   StyleRoot  // const
  public    static  System.String                   StyleComponent  // const
  public    static  System.String                   ResponsiveEnable  // const
  public    static  System.String                   ResponsiveResize  // const
  public    static  System.String                   RetinaScaler  // const
  public    static  System.String                   NumberButton  // const
  public    static  System.String                   NumberSpinner  // const
  public    static  System.String                   SRSpinner  // const
  public    static  System.String                   ContentFitText  // const
METHODS:
END_CLASS

CLASS: SRF.Service.ServiceAttribute
TYPE:  sealed class
TOKEN: 0x200003F
SIZE:  0x18
EXTENDS: UnityEngine.Scripting.PreserveAttribute
FIELDS:
  private           System.Type                     <ServiceType>k__BackingField  // 0x10
PROPERTIES:
  ServiceType  get=0x02B2ECC0  set=0x053908C0
METHODS:
  RVA=0x053908C0  token=0x600018B  System.Void .ctor(System.Type serviceType)
END_CLASS

CLASS: SRF.Service.ServiceSelectorAttribute
TYPE:  sealed class
TOKEN: 0x2000040
SIZE:  0x18
EXTENDS: UnityEngine.Scripting.PreserveAttribute
FIELDS:
  private           System.Type                     <ServiceType>k__BackingField  // 0x10
PROPERTIES:
  ServiceType  get=0x02B2ECC0  set=0x053908C0
METHODS:
  RVA=0x053908C0  token=0x600018E  System.Void .ctor(System.Type serviceType)
END_CLASS

CLASS: SRF.Service.ServiceConstructorAttribute
TYPE:  sealed class
TOKEN: 0x2000041
SIZE:  0x18
EXTENDS: UnityEngine.Scripting.PreserveAttribute
FIELDS:
  private           System.Type                     <ServiceType>k__BackingField  // 0x10
PROPERTIES:
  ServiceType  get=0x02B2ECC0  set=0x053908C0
METHODS:
  RVA=0x053908C0  token=0x6000191  System.Void .ctor(System.Type serviceType)
END_CLASS

CLASS: SRF.Service.IAsyncService
TYPE:  interface
TOKEN: 0x2000042
FIELDS:
PROPERTIES:
  IsLoaded  get=-1  // abstract
METHODS:
END_CLASS

CLASS: SRF.Service.SRDependencyServiceBase`1
TYPE:  abstract class
TOKEN: 0x2000043
EXTENDS: SRF.Service.SRServiceBase`1
IMPLEMENTS: SRF.Service.IAsyncService
FIELDS:
  private           System.Boolean                  _isLoaded  // 0x0
PROPERTIES:
  Dependencies  get=-1  // abstract
  IsLoaded  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000197  System.Void Log(System.String msg, UnityEngine.Object target)
  RVA=-1  // not resolved  token=0x6000198  System.Void Start()
  RVA=-1  // not resolved  token=0x6000199  System.Void OnLoaded()
  RVA=-1  // not resolved  token=0x600019A  System.Collections.IEnumerator LoadDependencies()
  RVA=-1  // not resolved  token=0x600019B  System.Void .ctor()
END_CLASS

CLASS: SRF.Service.SRSceneServiceBase`2
TYPE:  abstract class
TOKEN: 0x2000045
EXTENDS: SRF.Service.SRServiceBase`1
IMPLEMENTS: SRF.Service.IAsyncService
FIELDS:
  private           TImpl                           _rootObject  // 0x0
PROPERTIES:
  SceneName  get=-1  // abstract
  RootObject  get=-1  // not resolved
  IsLoaded  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60001A5  System.Void Log(System.String msg, UnityEngine.Object target)
  RVA=-1  // not resolved  token=0x60001A6  System.Void Start()
  RVA=-1  // not resolved  token=0x60001A7  System.Void OnDestroy()
  RVA=-1  // not resolved  token=0x60001A8  System.Void OnLoaded()
  RVA=-1  // not resolved  token=0x60001A9  System.Collections.IEnumerator LoadCoroutine()
  RVA=-1  // not resolved  token=0x60001AA  System.Void .ctor()
END_CLASS

CLASS: SRF.Service.SRServiceBase`1
TYPE:  abstract class
TOKEN: 0x2000047
EXTENDS: SRF.SRMonoBehaviourEx
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x60001B1  System.Void Awake()
  RVA=-1  // not resolved  token=0x60001B2  System.Void OnDestroy()
  RVA=-1  // not resolved  token=0x60001B3  System.Void .ctor()
END_CLASS

CLASS: SRF.Service.SRServiceManager
TYPE:  class
TOKEN: 0x2000048
SIZE:  0x58
EXTENDS: SRF.Components.SRAutoSingleton`1
FIELDS:
  public    static  System.Boolean                  EnableLogging  // const
  public    static  System.Int32                    LoadingCount  // static @ 0x0
  private   static readonly System.Collections.Generic.List<System.Reflection.Assembly>_assemblies  // static @ 0x8
  private   readonly SRF.SRList<SRF.Service.SRServiceManager.Service>_services  // 0x48
  private           System.Collections.Generic.List<SRF.Service.SRServiceManager.ServiceStub>_serviceStubs  // 0x50
  private   static  System.Boolean                  _hasQuit  // static @ 0x10
PROPERTIES:
  IsLoading  get=0x0ACA9F28
METHODS:
  RVA=-1  // generic def  token=0x60001B4  System.Void RegisterAssembly()
  RVA=-1  // generic def  token=0x60001B6  T GetService()
  RVA=0x0ACA9D30  token=0x60001B7  System.Object GetService(System.Type t)
  RVA=0x03B0F400  token=0x60001B8  System.Object GetServiceInternal(System.Type t)
  RVA=-1  // generic def  token=0x60001B9  System.Boolean HasService()
  RVA=0x03B0F510  token=0x60001BA  System.Boolean HasService(System.Type t)
  RVA=-1  // generic def  token=0x60001BB  System.Void RegisterService(System.Object service)
  RVA=0x03B0F1A0  token=0x60001BC  System.Void RegisterService(System.Type t, System.Object service)
  RVA=-1  // generic def  token=0x60001BD  System.Void UnRegisterService()
  RVA=0x03B0FF10  token=0x60001BE  System.Void UnRegisterService(System.Type t)
  RVA=0x03A477B0  token=0x60001BF  System.Void Awake()
  RVA=0x03B10000  token=0x60001C0  System.Void UpdateStubs()
  RVA=0x03B0F640  token=0x60001C1  System.Object AutoCreateService(System.Type t)
  RVA=0x0ACA9EB0  token=0x60001C2  System.Void OnApplicationQuit()
  RVA=0x03B0FD60  token=0x60001C3  System.Object DefaultServiceConstructor(System.Type serviceIntType, System.Type implType)
  RVA=0x03B10CD0  token=0x60001C4  System.Void ScanType(System.Type type)
  RVA=0x03B10E80  token=0x60001C5  System.Void ScanTypeForSelectors(System.Type t, System.Collections.Generic.List<SRF.Service.SRServiceManager.ServiceStub> stubs)
  RVA=0x03B10DB0  token=0x60001C6  System.Void ScanTypeForConstructors(System.Type t, System.Collections.Generic.List<SRF.Service.SRServiceManager.ServiceStub> stubs)
  RVA=0x02F2E150  token=0x60001C7  System.Reflection.MethodInfo[] GetStaticMethods(System.Type t)
  RVA=0x04CCA760  token=0x60001C8  System.Void .ctor()
  RVA=0x04CE6D70  token=0x60001C9  System.Void .cctor()
END_CLASS

CLASS: SRF.UI.ContentFitText
TYPE:  class
TOKEN: 0x200001C
SIZE:  0x28
EXTENDS: UnityEngine.EventSystems.UIBehaviour
IMPLEMENTS: UnityEngine.UI.ILayoutElement
FIELDS:
  public            SRF.UI.SRText                   CopySource  // 0x18
  public            UnityEngine.Vector2             Padding  // 0x20
PROPERTIES:
  minWidth  get=0x0ACA3F9C
  preferredWidth  get=0x0ACA40A4
  flexibleWidth  get=0x0ACA3E98
  minHeight  get=0x0ACA3F18
  preferredHeight  get=0x0ACA4020
  flexibleHeight  get=0x0ACA3E18
  layoutPriority  get=0x011EC580
METHODS:
  RVA=0x0ACA3C38  token=0x60000D2  System.Void CalculateLayoutInputHorizontal()
  RVA=0x0ACA3C38  token=0x60000D3  System.Void CalculateLayoutInputVertical()
  RVA=0x0ACA3CF4  token=0x60000D4  System.Void OnEnable()
  RVA=0x0ACA3C50  token=0x60000D5  System.Void CopySourceOnLayoutDirty(SRF.UI.SRText srText)
  RVA=0x0ACA3C50  token=0x60000D6  System.Void OnTransformParentChanged()
  RVA=0x0ACA3C58  token=0x60000D7  System.Void OnDisable()
  RVA=0x0ACA3C50  token=0x60000D8  System.Void OnDidApplyAnimationProperties()
  RVA=0x0ACA3C50  token=0x60000D9  System.Void OnBeforeTransformParentChanged()
  RVA=0x0ACA3D90  token=0x60000DA  System.Void SetDirty()
  RVA=0x05393520  token=0x60000DB  System.Void .ctor()
END_CLASS

CLASS: SRF.UI.CopyLayoutElement
TYPE:  class
TOKEN: 0x200001D
SIZE:  0x38
EXTENDS: UnityEngine.EventSystems.UIBehaviour
IMPLEMENTS: UnityEngine.UI.ILayoutElement
FIELDS:
  public            System.Boolean                  CopyMinHeight  // 0x18
  public            System.Boolean                  CopyMinWidth  // 0x19
  public            System.Boolean                  CopyPreferredHeight  // 0x1a
  public            System.Boolean                  CopyPreferredWidth  // 0x1b
  public            UnityEngine.RectTransform       CopySource  // 0x20
  public            System.Single                   PaddingMinHeight  // 0x28
  public            System.Single                   PaddingMinWidth  // 0x2c
  public            System.Single                   PaddingPreferredHeight  // 0x30
  public            System.Single                   PaddingPreferredWidth  // 0x34
PROPERTIES:
  preferredWidth  get=0x0ACA42C0
  preferredHeight  get=0x0ACA4238
  minWidth  get=0x0ACA41B0
  minHeight  get=0x0ACA4128
  layoutPriority  get=0x02BBE540
  flexibleHeight  get=0x04DA9E10
  flexibleWidth  get=0x04DA9E10
METHODS:
  RVA=0x041E1670  token=0x60000E3  System.Void CalculateLayoutInputHorizontal()
  RVA=0x041E1670  token=0x60000E4  System.Void CalculateLayoutInputVertical()
  RVA=0x05393520  token=0x60000E5  System.Void .ctor()
END_CLASS

CLASS: SRF.UI.CopyPreferredSize
TYPE:  class
TOKEN: 0x200001E
SIZE:  0x48
EXTENDS: UnityEngine.UI.LayoutElement
FIELDS:
  public            UnityEngine.RectTransform       CopySource  // 0x38
  public            System.Single                   PaddingHeight  // 0x40
  public            System.Single                   PaddingWidth  // 0x44
PROPERTIES:
  preferredWidth  get=0x0ACA43C8
  preferredHeight  get=0x0ACA4348
  layoutPriority  get=0x02BBE540
METHODS:
  RVA=0x04D77950  token=0x60000E9  System.Void .ctor()
END_CLASS

CLASS: SRF.UI.CopyPreferredSizes
TYPE:  class
TOKEN: 0x200001F
SIZE:  0x48
EXTENDS: UnityEngine.UI.LayoutElement
FIELDS:
  public            SRF.UI.CopyPreferredSizes.CopySource[]CopySources  // 0x38
  public            SRF.UI.CopyPreferredSizes.OperationsOperation  // 0x40
PROPERTIES:
  preferredWidth  get=0x0ACA45D0
  preferredHeight  get=0x0ACA4448
  layoutPriority  get=0x02BBE540
METHODS:
  RVA=0x04D77950  token=0x60000ED  System.Void .ctor()
END_CLASS

CLASS: SRF.UI.CopySizeIntoLayoutElement
TYPE:  class
TOKEN: 0x2000022
SIZE:  0x50
EXTENDS: UnityEngine.UI.LayoutElement
FIELDS:
  public            UnityEngine.RectTransform       CopySource  // 0x38
  public            System.Single                   PaddingHeight  // 0x40
  public            System.Single                   PaddingWidth  // 0x44
  public            System.Boolean                  SetPreferredSize  // 0x48
  public            System.Boolean                  SetMinimumSize  // 0x49
PROPERTIES:
  preferredWidth  get=0x0ACA492C
  preferredHeight  get=0x0ACA4890
  minWidth  get=0x0ACA47F4
  minHeight  get=0x0ACA4758
  layoutPriority  get=0x02BBE540
METHODS:
  RVA=0x04D77950  token=0x60000F4  System.Void .ctor()
END_CLASS

CLASS: SRF.UI.DragHandle
TYPE:  class
TOKEN: 0x2000023
SIZE:  0x48
EXTENDS: UnityEngine.MonoBehaviour
IMPLEMENTS: UnityEngine.EventSystems.IBeginDragHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.IEndDragHandler UnityEngine.EventSystems.IDragHandler
FIELDS:
  private           UnityEngine.UI.CanvasScaler     _canvasScaler  // 0x18
  private           System.Single                   _delta  // 0x20
  private           System.Single                   _startValue  // 0x24
  public            UnityEngine.RectTransform.Axis  Axis  // 0x28
  public            System.Boolean                  Invert  // 0x2c
  public            System.Single                   MaxSize  // 0x30
  public            UnityEngine.UI.LayoutElement    TargetLayoutElement  // 0x38
  public            UnityEngine.RectTransform       TargetRectTransform  // 0x40
PROPERTIES:
  Mult  get=0x0ACA5108
METHODS:
  RVA=0x0ACA4CE0  token=0x60000F6  System.Void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0ACA4D10  token=0x60000F7  System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0ACA4E40  token=0x60000F8  System.Void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0ACA4FE0  token=0x60000F9  System.Void Start()
  RVA=0x0ACA5030  token=0x60000FA  System.Boolean Verify()
  RVA=0x0ACA4B1C  token=0x60000FB  System.Single GetCurrentValue()
  RVA=0x0ACA4E98  token=0x60000FC  System.Void SetCurrentValue(System.Single value)
  RVA=0x0ACA4A1C  token=0x60000FD  System.Void CommitCurrentValue()
  RVA=0x0ACA4C58  token=0x60000FE  System.Single GetMinSize()
  RVA=0x0ACA4C3C  token=0x60000FF  System.Single GetMaxSize()
  RVA=0x0ACA50EC  token=0x6000100  System.Void .ctor()
END_CLASS

CLASS: SRF.UI.FlashGraphic
TYPE:  class
TOKEN: 0x2000024
SIZE:  0x50
EXTENDS: UnityEngine.EventSystems.UIBehaviour
IMPLEMENTS: UnityEngine.EventSystems.IPointerDownHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.IPointerUpHandler
FIELDS:
  public            System.Single                   DecayTime  // 0x18
  public            UnityEngine.Color               DefaultColor  // 0x1c
  public            UnityEngine.Color               FlashColor  // 0x2c
  public            UnityEngine.UI.Graphic          Target  // 0x40
  private           System.Boolean                  _isHoldingUntilNextPress  // 0x48
METHODS:
  RVA=0x0ACA5228  token=0x6000101  System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0ACA5270  token=0x6000102  System.Void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0ACA51E4  token=0x6000103  System.Void OnEnable()
  RVA=0x0ACA516C  token=0x6000104  System.Void Flash()
  RVA=0x0ACA5124  token=0x6000105  System.Void FlashAndHoldUntilNextPress()
  RVA=0x0ACA52BC  token=0x6000106  System.Void .ctor()
END_CLASS

CLASS: SRF.UI.InheritColour
TYPE:  class
TOKEN: 0x2000025
SIZE:  0x58
EXTENDS: SRF.SRMonoBehaviour
FIELDS:
  private           UnityEngine.UI.Graphic          _graphic  // 0x48
  public            UnityEngine.UI.Graphic          From  // 0x50
PROPERTIES:
  Graphic  get=0x0ACA5E88
METHODS:
  RVA=0x0ACA5DD0  token=0x6000108  System.Void Refresh()
  RVA=0x0ACA5E80  token=0x6000109  System.Void Update()
  RVA=0x0ACA5E80  token=0x600010A  System.Void Start()
  RVA=0x05393520  token=0x600010B  System.Void .ctor()
END_CLASS

CLASS: SRF.UI.LongPressButton
TYPE:  class
TOKEN: 0x2000026
SIZE:  0x1B0
EXTENDS: UnityEngine.UI.Button
FIELDS:
  private           System.Boolean                  _handled  // 0x190
  private           UnityEngine.UI.Button.ButtonClickedEvent_onLongPress  // 0x198
  private           System.Boolean                  _pressed  // 0x1a0
  private           System.Single                   _pressedTime  // 0x1a4
  public            System.Single                   LongPressDuration  // 0x1a8
PROPERTIES:
  onLongPress  get=0x04D8DED0  set=0x05397218
METHODS:
  RVA=0x0ACA5F98  token=0x600010E  System.Void OnPointerExit(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0ACA5F44  token=0x600010F  System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0ACA5FB8  token=0x6000110  System.Void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0ACA5F30  token=0x6000111  System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0ACA5FE8  token=0x6000112  System.Void Update()
  RVA=0x0ACA6040  token=0x6000113  System.Void .ctor()
END_CLASS

CLASS: SRF.UI.ResponsiveBase
TYPE:  abstract class
TOKEN: 0x2000027
SIZE:  0x50
EXTENDS: SRF.SRMonoBehaviour
FIELDS:
  private           System.Boolean                  _queueRefresh  // 0x48
PROPERTIES:
  RectTransform  get=0x0ACA7650
METHODS:
  RVA=0x04D99870  token=0x6000115  System.Void OnEnable()
  RVA=0x04D99870  token=0x6000116  System.Void OnRectTransformDimensionsChange()
  RVA=0x0ACA7628  token=0x6000117  System.Void Update()
  RVA=-1  // abstract  token=0x6000118  System.Void Refresh()
  RVA=0x0ABD817C  token=0x6000119  System.Void DoRefresh()
  RVA=0x05393520  token=0x600011A  System.Void .ctor()
END_CLASS

CLASS: SRF.UI.ResponsiveEnable
TYPE:  class
TOKEN: 0x2000028
SIZE:  0x58
EXTENDS: SRF.UI.ResponsiveBase
FIELDS:
  public            SRF.UI.ResponsiveEnable.Entry[] Entries  // 0x50
METHODS:
  RVA=0x0ACA76A0  token=0x600011B  System.Void Refresh()
  RVA=0x0ACA791C  token=0x600011C  System.Void .ctor()
END_CLASS

CLASS: SRF.UI.ResponsiveResize
TYPE:  class
TOKEN: 0x200002B
SIZE:  0x58
EXTENDS: SRF.UI.ResponsiveBase
FIELDS:
  public            SRF.UI.ResponsiveResize.Element[]Elements  // 0x50
METHODS:
  RVA=0x0ACA796C  token=0x600011D  System.Void Refresh()
  RVA=0x0ACA7B18  token=0x600011E  System.Void .ctor()
END_CLASS

CLASS: SRF.UI.ScrollToBottomBehaviour
TYPE:  class
TOKEN: 0x200002E
SIZE:  0x30
EXTENDS: UnityEngine.MonoBehaviour
FIELDS:
  private           UnityEngine.UI.ScrollRect       _scrollRect  // 0x18
  private           UnityEngine.CanvasGroup         _canvasGroup  // 0x20
  private           System.Boolean                  _scrollToTop  // 0x28
METHODS:
  RVA=0x0ACAA438  token=0x600011F  System.Void Start()
  RVA=0x0ACAA300  token=0x6000120  System.Void OnEnable()
  RVA=0x0ACAA594  token=0x6000121  System.Void Trigger()
  RVA=0x0ACAA300  token=0x6000122  System.Void OnScrollRectValueChanged(UnityEngine.Vector2 position)
  RVA=0x0ACAA308  token=0x6000123  System.Void Refresh()
  RVA=0x0ACAA3AC  token=0x6000124  System.Void SetVisible(System.Boolean truth)
  RVA=0x05393520  token=0x6000125  System.Void .ctor()
END_CLASS

CLASS: SRF.UI.SRNumberButton
TYPE:  class
TOKEN: 0x200002F
SIZE:  0x1B0
EXTENDS: UnityEngine.UI.Button
IMPLEMENTS: UnityEngine.EventSystems.IPointerClickHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.IPointerDownHandler UnityEngine.EventSystems.IPointerUpHandler
FIELDS:
  private   static  System.Single                   ExtraThreshold  // const
  public    static  System.Single                   Delay  // const
  private           System.Single                   _delayTime  // 0x190
  private           System.Single                   _downTime  // 0x194
  private           System.Boolean                  _isDown  // 0x198
  public            System.Double                   Amount  // 0x1a0
  public            SRF.UI.SRNumberSpinner          TargetField  // 0x1a8
METHODS:
  RVA=0x0ACA9470  token=0x6000126  System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0ACA94C0  token=0x6000127  System.Void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0ACA94E0  token=0x6000128  System.Void Update()
  RVA=0x0ACA9370  token=0x6000129  System.Void Apply()
  RVA=0x0ACA956C  token=0x600012A  System.Void .ctor()
END_CLASS

CLASS: SRF.UI.SRNumberSpinner
TYPE:  class
TOKEN: 0x2000030
SIZE:  0x2D8
EXTENDS: UnityEngine.UI.InputField
FIELDS:
  private           System.Double                   _currentValue  // 0x2a8
  private           System.Double                   _dragStartAmount  // 0x2b0
  private           System.Double                   _dragStep  // 0x2b8
  public            System.Single                   DragSensitivity  // 0x2c0
  public            System.Double                   MaxValue  // 0x2c8
  public            System.Double                   MinValue  // 0x2d0
METHODS:
  RVA=0x0ACA9584  token=0x600012B  System.Void Awake()
  RVA=0x0ACA99B8  token=0x600012C  System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x041E1670  token=0x600012D  System.Void OnPointerDown(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x041E1670  token=0x600012E  System.Void OnPointerUp(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0ACA9600  token=0x600012F  System.Void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0ACA9804  token=0x6000130  System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0ACA996C  token=0x6000131  System.Void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0ACA9AA0  token=0x6000132  System.Void .ctor()
END_CLASS

CLASS: SRF.UI.SRRetinaScaler
TYPE:  class
TOKEN: 0x2000031
SIZE:  0x58
EXTENDS: SRF.SRMonoBehaviour
FIELDS:
  private           System.Boolean                  _disablePixelPerfect  // 0x48
  private           System.Int32                    _designDpi  // 0x4c
  private           System.Single                   _lastDpi  // 0x50
METHODS:
  RVA=0x0ACA9CDC  token=0x6000133  System.Void Start()
  RVA=0x0ACA9BF4  token=0x6000134  System.Void ApplyScaling()
  RVA=0x0ACA9CE4  token=0x6000135  System.Void Update()
  RVA=0x0ACA9D14  token=0x6000136  System.Void .ctor()
END_CLASS

CLASS: SRF.UI.SRSpinner
TYPE:  class
TOKEN: 0x2000032
SIZE:  0x1A8
EXTENDS: UnityEngine.UI.Selectable
IMPLEMENTS: UnityEngine.EventSystems.IDragHandler UnityEngine.EventSystems.IEventSystemHandler UnityEngine.EventSystems.IBeginDragHandler
FIELDS:
  private           System.Single                   _dragDelta  // 0x188
  private           SRF.UI.SRSpinner.SpinEvent      _onSpinDecrement  // 0x190
  private           SRF.UI.SRSpinner.SpinEvent      _onSpinIncrement  // 0x198
  public            System.Single                   DragThreshold  // 0x1a0
PROPERTIES:
  OnSpinIncrement  get=0x04D8DED0  set=0x05397218
  OnSpinDecrement  get=0x04D8DD50  set=0x0539722C
METHODS:
  RVA=0x04DA9ED0  token=0x600013B  System.Void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0ACA9FBC  token=0x600013C  System.Void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0ACAA08C  token=0x600013D  System.Void OnIncrement(System.Int32 amount)
  RVA=0x0ACA9F70  token=0x600013E  System.Void OnDecrement(System.Int32 amount)
  RVA=0x0ACAA0D8  token=0x600013F  System.Void .ctor()
END_CLASS

CLASS: SRF.UI.SRText
TYPE:  class
TOKEN: 0x2000034
SIZE:  0x120
EXTENDS: UnityEngine.UI.Text
FIELDS:
  private           System.Action<SRF.UI.SRText>    LayoutDirty  // 0x118
EVENTS:
  LayoutDirty  add=add_LayoutDirty  remove=remove_LayoutDirty
METHODS:
  RVA=0x0ACAA1A0  token=0x6000143  System.Void SetLayoutDirty()
  RVA=0x0ACAA1D8  token=0x6000144  System.Void .ctor()
END_CLASS

CLASS: SRF.UI.StyleComponent
TYPE:  class
TOKEN: 0x2000035
SIZE:  0x88
EXTENDS: SRF.SRMonoBehaviour
FIELDS:
  private           SRF.UI.Style                    _activeStyle  // 0x48
  private           SRF.UI.StyleRoot                _cachedRoot  // 0x50
  private           UnityEngine.UI.Graphic          _graphic  // 0x58
  private           System.Boolean                  _hasStarted  // 0x60
  private           UnityEngine.UI.Image            _image  // 0x68
  private           UnityEngine.UI.Selectable       _selectable  // 0x70
  private           System.String                   _styleKey  // 0x78
  public            System.Boolean                  IgnoreImage  // 0x80
PROPERTIES:
  StyleKey  get=0x04D862A0  set=0x0ACABC9C
METHODS:
  RVA=0x0ACABC7C  token=0x6000147  System.Void Start()
  RVA=0x0ACABA50  token=0x6000148  System.Void OnEnable()
  RVA=0x0ACABA64  token=0x6000149  System.Void Refresh(System.Boolean invalidateCache)
  RVA=0x0ACAB900  token=0x600014A  SRF.UI.StyleRoot GetStyleRoot()
  RVA=0x0ACAB614  token=0x600014B  System.Void ApplyStyle()
  RVA=0x0ACABC2C  token=0x600014C  System.Void SRStyleDirty()
  RVA=0x05393520  token=0x600014D  System.Void .ctor()
END_CLASS

CLASS: SRF.UI.StyleRoot
TYPE:  sealed class
TOKEN: 0x2000036
SIZE:  0x58
EXTENDS: SRF.SRMonoBehaviour
FIELDS:
  private           SRF.UI.StyleSheet               _activeStyleSheet  // 0x48
  public            SRF.UI.StyleSheet               StyleSheet  // 0x50
METHODS:
  RVA=0x0ACABCC4  token=0x600014E  SRF.UI.Style GetStyle(System.String key)
  RVA=0x0ACABD88  token=0x600014F  System.Void OnEnable()
  RVA=0x0ACABD80  token=0x6000150  System.Void OnDisable()
  RVA=0x0ACABE4C  token=0x6000151  System.Void Update()
  RVA=0x0ACABDF8  token=0x6000152  System.Void OnStyleSheetChanged()
  RVA=0x070A7CA8  token=0x6000153  System.Void SetDirty()
  RVA=0x05393520  token=0x6000154  System.Void .ctor()
END_CLASS

CLASS: SRF.UI.Style
TYPE:  class
TOKEN: 0x2000037
SIZE:  0x58
FIELDS:
  public            UnityEngine.Color               ActiveColor  // 0x10
  public            UnityEngine.Color               DisabledColor  // 0x20
  public            UnityEngine.Color               HoverColor  // 0x30
  public            UnityEngine.Sprite              Image  // 0x40
  public            UnityEngine.Color               NormalColor  // 0x48
METHODS:
  RVA=0x0ACAC0B8  token=0x6000155  SRF.UI.Style Copy()
  RVA=0x0ACAC05C  token=0x6000156  System.Void CopyFrom(SRF.UI.Style style)
  RVA=0x0ACAC124  token=0x6000157  System.Void .ctor()
END_CLASS

CLASS: SRF.UI.StyleSheet
TYPE:  class
TOKEN: 0x2000038
SIZE:  0x30
EXTENDS: UnityEngine.ScriptableObject
FIELDS:
  private           System.Collections.Generic.List<System.String>_keys  // 0x18
  private           System.Collections.Generic.List<SRF.UI.Style>_styles  // 0x20
  public            SRF.UI.StyleSheet               Parent  // 0x28
METHODS:
  RVA=0x0ACABEBC  token=0x6000158  SRF.UI.Style GetStyle(System.String key, System.Boolean searchParent)
  RVA=0x0ACABF94  token=0x6000159  System.Void .ctor()
END_CLASS

CLASS: SRF.UI.Unselectable
TYPE:  sealed class
TOKEN: 0x2000039
SIZE:  0x50
EXTENDS: SRF.SRMonoBehaviour
IMPLEMENTS: UnityEngine.EventSystems.ISelectHandler UnityEngine.EventSystems.IEventSystemHandler
FIELDS:
  private           System.Boolean                  _suspectedSelected  // 0x48
METHODS:
  RVA=0x04D99870  token=0x600015A  System.Void OnSelect(UnityEngine.EventSystems.BaseEventData eventData)
  RVA=0x0ACAD738  token=0x600015B  System.Void Update()
  RVA=0x05393520  token=0x600015C  System.Void .ctor()
END_CLASS

CLASS: SRF.UI.Layout.FlowLayoutGroup
TYPE:  class
TOKEN: 0x200003A
SIZE:  0x70
EXTENDS: UnityEngine.UI.LayoutGroup
FIELDS:
  private   readonly System.Collections.Generic.IList<UnityEngine.RectTransform>_rowList  // 0x58
  private           System.Single                   _layoutHeight  // 0x60
  public            System.Boolean                  ChildForceExpandHeight  // 0x64
  public            System.Boolean                  ChildForceExpandWidth  // 0x65
  public            System.Single                   Spacing  // 0x68
PROPERTIES:
  IsCenterAlign  get=0x0ACA5D24
  IsRightAlign  get=0x0ACA5D6C
  IsMiddleAlign  get=0x0ACA5D54
  IsLowerAlign  get=0x0ACA5D3C
METHODS:
  RVA=0x0ACA5308  token=0x6000161  System.Void CalculateLayoutInputHorizontal()
  RVA=0x0ACA57C4  token=0x6000162  System.Void SetLayoutHorizontal()
  RVA=0x0ACA5810  token=0x6000163  System.Void SetLayoutVertical()
  RVA=0x0ACA538C  token=0x6000164  System.Void CalculateLayoutInputVertical()
  RVA=0x0ACA585C  token=0x6000165  System.Single SetLayout(System.Single width, System.Int32 axis, System.Boolean layoutInput)
  RVA=0x0ACA53E0  token=0x6000166  System.Single CalculateRowVerticalOffset(System.Single groupHeight, System.Single yOffset, System.Single currentRowHeight)
  RVA=0x0ACA54F4  token=0x6000167  System.Void LayoutRow(System.Collections.Generic.IList<UnityEngine.RectTransform> contents, System.Single rowWidth, System.Single rowHeight, System.Single maxWidth, System.Single xOffset, System.Single yOffset, System.Int32 axis)
  RVA=0x0ACA5464  token=0x6000168  System.Single GetGreatestMinimumChildWidth()
  RVA=0x0ACA5CA4  token=0x6000169  System.Void .ctor()
END_CLASS

CLASS: SRF.UI.Layout.IVirtualView
TYPE:  interface
TOKEN: 0x200003B
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x600016A  System.Void SetDataContext(System.Object data)
END_CLASS

CLASS: SRF.UI.Layout.VirtualVerticalLayoutGroup
TYPE:  class
TOKEN: 0x200003C
SIZE:  0xE8
EXTENDS: UnityEngine.UI.LayoutGroup
IMPLEMENTS: UnityEngine.EventSystems.IPointerClickHandler UnityEngine.EventSystems.IEventSystemHandler
FIELDS:
  private   readonly SRF.SRList<System.Object>       _itemList  // 0x58
  private   readonly SRF.SRList<System.Int32>        _visibleItemList  // 0x60
  private           System.Boolean                  _isDirty  // 0x68
  private           SRF.SRList<SRF.UI.Layout.VirtualVerticalLayoutGroup.Row>_rowCache  // 0x70
  private           UnityEngine.UI.ScrollRect       _scrollRect  // 0x78
  private           System.Int32                    _selectedIndex  // 0x80
  private           System.Object                   _selectedItem  // 0x88
  private           SRF.UI.Layout.VirtualVerticalLayoutGroup.SelectedItemChangedEvent_selectedItemChanged  // 0x90
  private           System.Int32                    _visibleItemCount  // 0x98
  private           SRF.SRList<SRF.UI.Layout.VirtualVerticalLayoutGroup.Row>_visibleRows  // 0xa0
  public            SRF.UI.StyleSheet               AltRowStyleSheet  // 0xa8
  public            System.Boolean                  EnableSelection  // 0xb0
  public            UnityEngine.RectTransform       ItemPrefab  // 0xb8
  public            System.Int32                    RowPadding  // 0xc0
  public            SRF.UI.StyleSheet               RowStyleSheet  // 0xc8
  public            SRF.UI.StyleSheet               SelectedRowStyleSheet  // 0xd0
  public            System.Single                   Spacing  // 0xd8
  public            System.Boolean                  StickToBottom  // 0xdc
  private           System.Single                   _itemHeight  // 0xe0
PROPERTIES:
  SelectedItemChanged  get=0x04D86220  set=0x06402330
  SelectedItem  get=0x04D861F0  set=0x0ACAF114
  minHeight  get=0x0ACAF048
  ScrollRect  get=0x0ACAEFC4
  AlignBottom  get=0x0ACAEE48
  AlignTop  get=0x0ACAEE60
  ItemHeight  get=0x0ACAEE78
METHODS:
  RVA=0x0ACAE074  token=0x6000170  System.Void OnPointerClick(UnityEngine.EventSystems.PointerEventData eventData)
  RVA=0x0ACAD90C  token=0x6000171  System.Void Awake()
  RVA=0x0ACAE1E4  token=0x6000172  System.Void OnScrollRectValueChanged(UnityEngine.Vector2 d)
  RVA=0x0ACAEC44  token=0x6000173  System.Void Start()
  RVA=0x0ACAE054  token=0x6000174  System.Void OnEnable()
  RVA=0x0ACAEC4C  token=0x6000175  System.Void Update()
  RVA=0x0ACADF2C  token=0x6000176  System.Void InvalidateItem(System.Int32 itemIndex)
  RVA=0x0ACAE454  token=0x6000177  System.Void RefreshIndexCache()
  RVA=0x0ACAE5D8  token=0x6000178  System.Void ScrollUpdate()
  RVA=0x0ACADA74  token=0x6000179  System.Void CalculateLayoutInputVertical()
  RVA=0x0ACAE954  token=0x600017A  System.Void SetLayoutHorizontal()
  RVA=0x0ACAEB00  token=0x600017B  System.Void SetLayoutVertical()
  RVA=0x0ACAE928  token=0x600017C  System.Void SetDirty()
  RVA=0x0ACAD800  token=0x600017D  System.Void AddItem(System.Object item)
  RVA=0x0ACAE528  token=0x600017E  System.Void RemoveItem(System.Object item)
  RVA=0x0ACADABC  token=0x600017F  System.Void ClearItems()
  RVA=0x0ACADD70  token=0x6000184  SRF.UI.Layout.VirtualVerticalLayoutGroup.Row GetRow(System.Int32 forIndex)
  RVA=0x0ACAE400  token=0x6000185  System.Void RecycleRow(SRF.UI.Layout.VirtualVerticalLayoutGroup.Row row)
  RVA=0x0ACAE23C  token=0x6000186  System.Void PopulateRow(System.Int32 index, SRF.UI.Layout.VirtualVerticalLayoutGroup.Row row)
  RVA=0x0ACADB70  token=0x6000187  SRF.UI.Layout.VirtualVerticalLayoutGroup.Row CreateRow()
  RVA=0x0ACAED28  token=0x6000188  System.Void .ctor()
END_CLASS

