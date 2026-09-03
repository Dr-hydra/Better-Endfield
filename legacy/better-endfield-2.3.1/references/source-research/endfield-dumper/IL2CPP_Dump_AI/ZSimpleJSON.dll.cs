// ========================================================
// Dumped by @desirepro
// Assembly: ZSimpleJSON.dll
// Classes:  23
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

CLASS: Type
TYPE:  sealed struct
TOKEN: 0x2000004
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  ZSimpleJSON.JSONNode.Enumerator.TypeNone  // const
  public    static  ZSimpleJSON.JSONNode.Enumerator.TypeArray  // const
  public    static  ZSimpleJSON.JSONNode.Enumerator.TypeObject  // const
METHODS:
END_CLASS

CLASS: Enumerator
TYPE:  sealed struct
TOKEN: 0x2000003
SIZE:  0x58
FIELDS:
  private           ZSimpleJSON.JSONNode.Enumerator.Typetype  // 0x10
  private           System.Collections.Generic.Dictionary.Enumerator<System.String,ZSimpleJSON.JSONNode>m_Object  // 0x18
  private           System.Collections.Generic.List.Enumerator<ZSimpleJSON.JSONNode>m_Array  // 0x40
PROPERTIES:
  IsValid  get=0x068B8E74
  Current  get=0x036D56A0
METHODS:
  RVA=0x09DB87E4  token=0x60000C1  System.Void .ctor(System.Collections.Generic.List.Enumerator<ZSimpleJSON.JSONNode> aArrayEnum)
  RVA=0x04B0C3F0  token=0x60000C2  System.Void .ctor(System.Collections.Generic.Dictionary.Enumerator<System.String,ZSimpleJSON.JSONNode> aDictEnum)
  RVA=0x036D5670  token=0x60000C4  System.Boolean MoveNext()
END_CLASS

CLASS: ValueEnumerator
TYPE:  sealed struct
TOKEN: 0x2000005
SIZE:  0x58
FIELDS:
  private           ZSimpleJSON.JSONNode.Enumerator m_Enumerator  // 0x10
PROPERTIES:
  Current  get=0x0B8BEE84
METHODS:
  RVA=0x09DC2BA0  token=0x60000C5  System.Void .ctor(System.Collections.Generic.List.Enumerator<ZSimpleJSON.JSONNode> aArrayEnum)
  RVA=0x09DC2ACC  token=0x60000C6  System.Void .ctor(System.Collections.Generic.Dictionary.Enumerator<System.String,ZSimpleJSON.JSONNode> aDictEnum)
  RVA=0x09DC2B6C  token=0x60000C7  System.Void .ctor(ZSimpleJSON.JSONNode.Enumerator aEnumerator)
  RVA=0x0B8BDC20  token=0x60000C9  System.Boolean MoveNext()
  RVA=0x04DA2D60  token=0x60000CA  ZSimpleJSON.JSONNode.ValueEnumerator GetEnumerator()
END_CLASS

CLASS: KeyEnumerator
TYPE:  sealed struct
TOKEN: 0x2000006
SIZE:  0x58
FIELDS:
  private           ZSimpleJSON.JSONNode.Enumerator m_Enumerator  // 0x10
PROPERTIES:
  Current  get=0x0B8BDC28
METHODS:
  RVA=0x09DC2BA0  token=0x60000CB  System.Void .ctor(System.Collections.Generic.List.Enumerator<ZSimpleJSON.JSONNode> aArrayEnum)
  RVA=0x09DC2ACC  token=0x60000CC  System.Void .ctor(System.Collections.Generic.Dictionary.Enumerator<System.String,ZSimpleJSON.JSONNode> aDictEnum)
  RVA=0x09DC2B6C  token=0x60000CD  System.Void .ctor(ZSimpleJSON.JSONNode.Enumerator aEnumerator)
  RVA=0x0B8BDC20  token=0x60000CF  System.Boolean MoveNext()
  RVA=0x04DA2D60  token=0x60000D0  ZSimpleJSON.JSONNode.KeyEnumerator GetEnumerator()
END_CLASS

CLASS: LinqEnumerator
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x60
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           ZSimpleJSON.JSONNode            m_Node  // 0x10
  private           ZSimpleJSON.JSONNode.Enumerator m_Enumerator  // 0x18
PROPERTIES:
  Current  get=0x0B8BDEEC
  System.Collections.IEnumerator.Current  get=0x0B8BDDE0
METHODS:
  RVA=0x0B8BDE38  token=0x60000D1  System.Void .ctor(ZSimpleJSON.JSONNode aNode)
  RVA=0x0B8BDCCC  token=0x60000D4  System.Boolean MoveNext()
  RVA=0x09DC2C78  token=0x60000D5  System.Void Dispose()
  RVA=0x0B8BDC68  token=0x60000D6  System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,ZSimpleJSON.JSONNode>> GetEnumerator()
  RVA=0x0B8BDCD8  token=0x60000D7  System.Void Reset()
  RVA=0x0B8BDD7C  token=0x60000D8  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <get_Children>d__181
TYPE:  sealed class
TOKEN: 0x2000008
SIZE:  0x28
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           ZSimpleJSON.JSONNode            <>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x20
PROPERTIES:
  System.Collections.Generic.IEnumerator<ZSimpleJSON.JSONNode>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x03A11010  token=0x60000D9  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x60000DA  System.Void System.IDisposable.Dispose()
  RVA=0x04D83CA0  token=0x60000DB  System.Boolean MoveNext()
  RVA=0x0B8BDFFC  token=0x60000DD  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x0B8BDF7C  token=0x60000DF  System.Collections.Generic.IEnumerator<ZSimpleJSON.JSONNode> System.Collections.Generic.IEnumerable<ZSimpleJSON.JSONNode>.GetEnumerator()
  RVA=0x0B8BDFF4  token=0x60000E0  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <get_DeepChildren>d__183
TYPE:  sealed class
TOKEN: 0x2000009
SIZE:  0x40
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           ZSimpleJSON.JSONNode            <>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x20
  public            ZSimpleJSON.JSONNode            <>4__this  // 0x28
  private           System.Collections.Generic.IEnumerator<ZSimpleJSON.JSONNode><>7__wrap1  // 0x30
  private           System.Collections.Generic.IEnumerator<ZSimpleJSON.JSONNode><>7__wrap2  // 0x38
PROPERTIES:
  System.Collections.Generic.IEnumerator<ZSimpleJSON.JSONNode>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x03A11010  token=0x60000E1  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x0B8BED04  token=0x60000E2  System.Void System.IDisposable.Dispose()
  RVA=0x0B8BE82C  token=0x60000E3  System.Boolean MoveNext()
  RVA=0x0B8BEDF0  token=0x60000E4  System.Void <>m__Finally1()
  RVA=0x0B8BEE38  token=0x60000E5  System.Void <>m__Finally2()
  RVA=0x0B8BECB8  token=0x60000E7  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x0B8BEC24  token=0x60000E9  System.Collections.Generic.IEnumerator<ZSimpleJSON.JSONNode> System.Collections.Generic.IEnumerable<ZSimpleJSON.JSONNode>.GetEnumerator()
  RVA=0x0B8BECB0  token=0x60000EA  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <get_Children>d__24
TYPE:  sealed class
TOKEN: 0x200000E
SIZE:  0x48
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           ZSimpleJSON.JSONNode            <>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x20
  public            ZSimpleJSON.JSONArray           <>4__this  // 0x28
  private           System.Collections.Generic.List.Enumerator<ZSimpleJSON.JSONNode><>7__wrap1  // 0x30
PROPERTIES:
  System.Collections.Generic.IEnumerator<ZSimpleJSON.JSONNode>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x03A11010  token=0x60000FD  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x0B8BE370  token=0x60000FE  System.Void System.IDisposable.Dispose()
  RVA=0x0B8BE048  token=0x60000FF  System.Boolean MoveNext()
  RVA=0x0B8BE3E8  token=0x6000100  System.Void <>m__Finally1()
  RVA=0x0B8BE324  token=0x6000102  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x0B8BE290  token=0x6000104  System.Collections.Generic.IEnumerator<ZSimpleJSON.JSONNode> System.Collections.Generic.IEnumerable<ZSimpleJSON.JSONNode>.GetEnumerator()
  RVA=0x0B8BE31C  token=0x6000105  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <>c__DisplayClass21_0
TYPE:  sealed class
TOKEN: 0x2000010
SIZE:  0x18
FIELDS:
  public            ZSimpleJSON.JSONNode            aNode  // 0x10
METHODS:
  RVA=0x041E1670  token=0x600011B  System.Void .ctor()
  RVA=0x0B8BDF18  token=0x600011C  System.Boolean <Remove>b__0(System.Collections.Generic.KeyValuePair<System.String,ZSimpleJSON.JSONNode> k)
END_CLASS

CLASS: <get_Children>d__27
TYPE:  sealed class
TOKEN: 0x2000011
SIZE:  0x58
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           ZSimpleJSON.JSONNode            <>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x20
  public            ZSimpleJSON.JSONObject          <>4__this  // 0x28
  private           System.Collections.Generic.Dictionary.Enumerator<System.String,ZSimpleJSON.JSONNode><>7__wrap1  // 0x30
PROPERTIES:
  System.Collections.Generic.IEnumerator<ZSimpleJSON.JSONNode>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x03A11010  token=0x600011D  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x0B8BE778  token=0x600011E  System.Void System.IDisposable.Dispose()
  RVA=0x0B8BE424  token=0x600011F  System.Boolean MoveNext()
  RVA=0x0B8BE7F0  token=0x6000120  System.Void <>m__Finally1()
  RVA=0x0B8BE72C  token=0x6000122  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x0B8BE698  token=0x6000124  System.Collections.Generic.IEnumerator<ZSimpleJSON.JSONNode> System.Collections.Generic.IEnumerable<ZSimpleJSON.JSONNode>.GetEnumerator()
  RVA=0x0B8BE724  token=0x6000125  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: ZSimpleJSON.JSONNode
TYPE:  abstract class
TOKEN: 0x2000002
SIZE:  0x10
FIELDS:
  public    static  System.Byte                     Color32DefaultAlpha  // static @ 0x0
  public    static  System.Single                   ColorDefaultAlpha  // static @ 0x4
  public    static  ZSimpleJSON.JSONContainerType   VectorContainerType  // static @ 0x8
  public    static  ZSimpleJSON.JSONContainerType   QuaternionContainerType  // static @ 0xc
  public    static  ZSimpleJSON.JSONContainerType   RectContainerType  // static @ 0x10
  public    static  ZSimpleJSON.JSONContainerType   ColorContainerType  // static @ 0x14
  public    static  System.Boolean                  forceASCII  // static @ 0x18
  public    static  System.Boolean                  longAsString  // static @ 0x19
  public    static  System.Boolean                  allowLineComments  // static @ 0x1a
  private   static  System.Text.StringBuilder       m_EscapeBuilder  // static @ 0xffffffff
PROPERTIES:
  AsDecimal  get=0x0B8BA794  set=0x0B8BCAB0
  AsChar  get=0x0B8BA658  set=0x0B8BC990
  AsUInt  get=0x0B8BAC3C  set=0x0B8BCE20
  AsByte  get=0x053912C0  set=0x0B8BC97C
  AsSByte  get=0x053912C0  set=0x0B8BCC2C
  AsShort  get=0x053912C0  set=0x0B8BCC40
  AsUShort  get=0x053912C0  set=0x0B8BCEA0
  AsDateTime  get=0x0B8BA6E0  set=0x0B8BCA28
  AsTimeSpan  get=0x0B8BAB8C  set=0x0B8BCDC8
  AsGuid  get=0x0B8BA8B0  set=0x0B8BCB84
  AsByteArray  get=0x0B8BA484  set=0x0B8BC810
  AsByteList  get=0x0B8BA558  set=0x0B8BC8B8
  AsUlongArray  get=0x0B8BACD4  set=0x0B8BCEB4
  AsStringArray  get=0x0B8BA9B4  set=0x0B8BCC54
  AsStringList  get=0x0B8BAA88  set=0x0B8BCD00
  Tag  get=-1  // abstract
  Item  get=0x011EC580  set=0x041E1670
  Item  get=0x011EC580  set=0x041E1670
  Value  get=0x0B8BAF84  set=0x041E1670
  Count  get=0x011EC580
  IsNumber  get=0x012081B0
  IsString  get=0x012081B0
  IsBoolean  get=0x012081B0
  IsNull  get=0x012081B0
  IsArray  get=0x012081B0
  IsObject  get=0x012081B0
  Inline  get=0x012081B0  set=0x041E1670
  Children  get=0x0B8BADAC
  DeepChildren  get=0x0B8BAE00
  Linq  get=0x0B8BAF24
  Keys  get=0x0B8BAE70
  Values  get=0x0B8BAE70
  AsDouble  get=0x0B8BA808  set=0x0B8BCB0C
  AsInt  get=0x0B8BA8E0  set=0x0B8BCBB0
  AsFloat  get=0x0B8BA894  set=0x0B8BCB70
  AsBool  get=0x0B8BA3E8  set=0x0B8BC7A4
  AsLong  get=0x0B8BA8FC  set=0x0B8BCBC8
  AsULong  get=0x0B8BAC58  set=0x0B8BCE3C
  AsArray  get=0x0B8BA3AC
  AsObject  get=0x0B8BA978
  EscapeBuilder  get=0x0360CBC0
METHODS:
  RVA=0x0B8BB710  token=0x6000003  ZSimpleJSON.JSONNode op_Implicit(System.Decimal aDecimal)
  RVA=0x0B8BBCD4  token=0x6000004  System.Decimal op_Implicit(ZSimpleJSON.JSONNode aNode)
  RVA=0x0B8BC41C  token=0x6000007  ZSimpleJSON.JSONNode op_Implicit(System.Char aChar)
  RVA=0x0B8BC3E4  token=0x6000008  System.Char op_Implicit(ZSimpleJSON.JSONNode aNode)
  RVA=0x049DD510  token=0x600000B  ZSimpleJSON.JSONNode op_Implicit(System.UInt32 aUInt)
  RVA=0x0B8BC344  token=0x600000C  System.UInt32 op_Implicit(ZSimpleJSON.JSONNode aNode)
  RVA=0x0B8BBED4  token=0x600000F  ZSimpleJSON.JSONNode op_Implicit(System.Byte aByte)
  RVA=0x0B8BB430  token=0x6000010  System.Byte op_Implicit(ZSimpleJSON.JSONNode aNode)
  RVA=0x0B8BC674  token=0x6000013  ZSimpleJSON.JSONNode op_Implicit(System.SByte aSByte)
  RVA=0x0B8BB430  token=0x6000014  System.SByte op_Implicit(ZSimpleJSON.JSONNode aNode)
  RVA=0x0B8BB3CC  token=0x6000017  ZSimpleJSON.JSONNode op_Implicit(System.Int16 aShort)
  RVA=0x0B8BB430  token=0x6000018  System.Int16 op_Implicit(ZSimpleJSON.JSONNode aNode)
  RVA=0x0B8BC228  token=0x600001B  ZSimpleJSON.JSONNode op_Implicit(System.UInt16 aUShort)
  RVA=0x0B8BB430  token=0x600001C  System.UInt16 op_Implicit(ZSimpleJSON.JSONNode aNode)
  RVA=0x0B8BB1DC  token=0x600001F  ZSimpleJSON.JSONNode op_Implicit(System.DateTime aDateTime)
  RVA=0x0B8BBD6C  token=0x6000020  System.DateTime op_Implicit(ZSimpleJSON.JSONNode aNode)
  RVA=0x0B8BB344  token=0x6000023  ZSimpleJSON.JSONNode op_Implicit(System.TimeSpan aTimeSpan)
  RVA=0x0B8BBC9C  token=0x6000024  System.TimeSpan op_Implicit(ZSimpleJSON.JSONNode aNode)
  RVA=0x0B8BB170  token=0x6000027  ZSimpleJSON.JSONNode op_Implicit(System.Guid aGuid)
  RVA=0x0B8BBEA4  token=0x6000028  System.Guid op_Implicit(ZSimpleJSON.JSONNode aNode)
  RVA=0x0B8BB6A4  token=0x600002B  ZSimpleJSON.JSONNode op_Implicit(System.Byte[] aByteArray)
  RVA=0x0B8BBE20  token=0x600002C  System.Byte[] op_Implicit(ZSimpleJSON.JSONNode aNode)
  RVA=0x0B8BC1BC  token=0x600002F  ZSimpleJSON.JSONNode op_Implicit(System.Collections.Generic.List<System.Byte> aByteList)
  RVA=0x0B8BB44C  token=0x6000030  System.Collections.Generic.List<System.Byte> op_Implicit(ZSimpleJSON.JSONNode aNode)
  RVA=0x0B8BC51C  token=0x6000033  ZSimpleJSON.JSONNode op_Implicit(System.UInt64[] aStringArray)
  RVA=0x0B8BBAE0  token=0x6000034  System.UInt64[] op_Implicit(ZSimpleJSON.JSONNode aNode)
  RVA=0x0B8BB528  token=0x6000037  ZSimpleJSON.JSONNode op_Implicit(System.String[] aStringArray)
  RVA=0x0B8BBCB8  token=0x6000038  System.String[] op_Implicit(ZSimpleJSON.JSONNode aNode)
  RVA=0x0B8BBFA0  token=0x600003B  ZSimpleJSON.JSONNode op_Implicit(System.Collections.Generic.List<System.String> aStringList)
  RVA=0x0B8BC0A0  token=0x600003C  System.Collections.Generic.List<System.String> op_Implicit(ZSimpleJSON.JSONNode aNode)
  RVA=0x04950E40  token=0x600003D  ZSimpleJSON.JSONNode op_Implicit(System.Nullable<System.Int32> aValue)
  RVA=0x0B8BBD88  token=0x600003E  System.Nullable<System.Int32> op_Implicit(ZSimpleJSON.JSONNode aNode)
  RVA=0x0B8BBA24  token=0x600003F  ZSimpleJSON.JSONNode op_Implicit(System.Nullable<System.Single> aValue)
  RVA=0x0B8BC124  token=0x6000040  System.Nullable<System.Single> op_Implicit(ZSimpleJSON.JSONNode aNode)
  RVA=0x0B8BB974  token=0x6000041  ZSimpleJSON.JSONNode op_Implicit(System.Nullable<System.Double> aValue)
  RVA=0x0B8BB840  token=0x6000042  System.Nullable<System.Double> op_Implicit(ZSimpleJSON.JSONNode aNode)
  RVA=0x0B8BB088  token=0x6000043  ZSimpleJSON.JSONNode op_Implicit(System.Nullable<System.Boolean> aValue)
  RVA=0x0B8BC28C  token=0x6000044  System.Nullable<System.Boolean> op_Implicit(ZSimpleJSON.JSONNode aNode)
  RVA=0x0B8BBB64  token=0x6000045  ZSimpleJSON.JSONNode op_Implicit(System.Nullable<System.Int64> aValue)
  RVA=0x0B8BB794  token=0x6000046  System.Nullable<System.Int64> op_Implicit(ZSimpleJSON.JSONNode aNode)
  RVA=0x0B8BB28C  token=0x6000047  ZSimpleJSON.JSONNode op_Implicit(System.Nullable<System.Int16> aValue)
  RVA=0x0B8BC00C  token=0x6000048  System.Nullable<System.Int16> op_Implicit(ZSimpleJSON.JSONNode aNode)
  RVA=0x0B8B8250  token=0x6000049  ZSimpleJSON.JSONNode GetContainer(ZSimpleJSON.JSONContainerType aType)
  RVA=0x0B8BB594  token=0x600004A  ZSimpleJSON.JSONNode op_Implicit(UnityEngine.Vector2 aVec)
  RVA=0x0B8BC6D8  token=0x600004B  ZSimpleJSON.JSONNode op_Implicit(UnityEngine.Vector3 aVec)
  RVA=0x0B8BC588  token=0x600004C  ZSimpleJSON.JSONNode op_Implicit(UnityEngine.Vector4 aVec)
  RVA=0x0B8BBC18  token=0x600004D  ZSimpleJSON.JSONNode op_Implicit(UnityEngine.Color aCol)
  RVA=0x0B8BAFE0  token=0x600004E  ZSimpleJSON.JSONNode op_Implicit(UnityEngine.Color32 aCol)
  RVA=0x0B8BB8F0  token=0x600004F  ZSimpleJSON.JSONNode op_Implicit(UnityEngine.Quaternion aRot)
  RVA=0x0B8BC360  token=0x6000050  ZSimpleJSON.JSONNode op_Implicit(UnityEngine.Rect aRect)
  RVA=0x0B8BC4A4  token=0x6000051  ZSimpleJSON.JSONNode op_Implicit(UnityEngine.RectOffset aRect)
  RVA=0x0B8BC400  token=0x6000052  UnityEngine.Vector2 op_Implicit(ZSimpleJSON.JSONNode aNode)
  RVA=0x0B8BB4F0  token=0x6000053  UnityEngine.Vector3 op_Implicit(ZSimpleJSON.JSONNode aNode)
  RVA=0x0B8BBF38  token=0x6000054  UnityEngine.Vector4 op_Implicit(ZSimpleJSON.JSONNode aNode)
  RVA=0x0B8BAFB0  token=0x6000055  UnityEngine.Color op_Implicit(ZSimpleJSON.JSONNode aNode)
  RVA=0x0B8BB4D0  token=0x6000056  UnityEngine.Color32 op_Implicit(ZSimpleJSON.JSONNode aNode)
  RVA=0x0B8BB058  token=0x6000057  UnityEngine.Quaternion op_Implicit(ZSimpleJSON.JSONNode aNode)
  RVA=0x0B8BB140  token=0x6000058  UnityEngine.Rect op_Implicit(ZSimpleJSON.JSONNode aNode)
  RVA=0x0B8BC328  token=0x6000059  UnityEngine.RectOffset op_Implicit(ZSimpleJSON.JSONNode aNode)
  RVA=0x0B8B8F6C  token=0x600005A  UnityEngine.Vector2 ReadVector2(UnityEngine.Vector2 aDefault)
  RVA=0x0B8B8ECC  token=0x600005B  UnityEngine.Vector2 ReadVector2(System.String aXName, System.String aYName)
  RVA=0x0B8B8EB4  token=0x600005C  UnityEngine.Vector2 ReadVector2()
  RVA=0x0B8B9FA8  token=0x600005D  ZSimpleJSON.JSONNode WriteVector2(UnityEngine.Vector2 aVec, System.String aXName, System.String aYName)
  RVA=0x0B8B918C  token=0x600005E  UnityEngine.Vector3 ReadVector3(UnityEngine.Vector3 aDefault)
  RVA=0x0B8B90A4  token=0x600005F  UnityEngine.Vector3 ReadVector3(System.String aXName, System.String aYName, System.String aZName)
  RVA=0x0B8B9054  token=0x6000060  UnityEngine.Vector3 ReadVector3()
  RVA=0x0B8BA0A0  token=0x6000061  ZSimpleJSON.JSONNode WriteVector3(UnityEngine.Vector3 aVec, System.String aXName, System.String aYName, System.String aZName)
  RVA=0x0B8B9308  token=0x6000062  UnityEngine.Vector4 ReadVector4(UnityEngine.Vector4 aDefault)
  RVA=0x0B8B94E4  token=0x6000063  UnityEngine.Vector4 ReadVector4()
  RVA=0x0B8BA1E0  token=0x6000064  ZSimpleJSON.JSONNode WriteVector4(UnityEngine.Vector4 aVec)
  RVA=0x0B8B84EC  token=0x6000065  UnityEngine.Color ReadColor(UnityEngine.Color aDefault)
  RVA=0x0B8B8720  token=0x6000066  UnityEngine.Color ReadColor()
  RVA=0x0B8B9778  token=0x6000067  ZSimpleJSON.JSONNode WriteColor(UnityEngine.Color aCol)
  RVA=0x0B8B82E0  token=0x6000068  UnityEngine.Color32 ReadColor32(UnityEngine.Color32 aDefault)
  RVA=0x0B8B82D4  token=0x6000069  UnityEngine.Color32 ReadColor32()
  RVA=0x0B8B95B0  token=0x600006A  ZSimpleJSON.JSONNode WriteColor32(UnityEngine.Color32 aCol)
  RVA=0x0B8B8810  token=0x600006B  UnityEngine.Quaternion ReadQuaternion(UnityEngine.Quaternion aDefault)
  RVA=0x0B8B89EC  token=0x600006C  UnityEngine.Quaternion ReadQuaternion()
  RVA=0x0B8B99D8  token=0x600006D  ZSimpleJSON.JSONNode WriteQuaternion(UnityEngine.Quaternion aRot)
  RVA=0x0B8B8CA4  token=0x600006E  UnityEngine.Rect ReadRect(UnityEngine.Rect aDefault)
  RVA=0x0B8B8E80  token=0x600006F  UnityEngine.Rect ReadRect()
  RVA=0x0B8B9DDC  token=0x6000070  ZSimpleJSON.JSONNode WriteRect(UnityEngine.Rect aRect)
  RVA=0x0B8B8A90  token=0x6000071  UnityEngine.RectOffset ReadRectOffset(UnityEngine.RectOffset aDefault)
  RVA=0x0B8B8A28  token=0x6000072  UnityEngine.RectOffset ReadRectOffset()
  RVA=0x0B8B9BA4  token=0x6000073  ZSimpleJSON.JSONNode WriteRectOffset(UnityEngine.RectOffset aRect)
  RVA=0x0B8B875C  token=0x6000074  UnityEngine.Matrix4x4 ReadMatrix()
  RVA=0x0B8B9944  token=0x6000075  ZSimpleJSON.JSONNode WriteMatrix(UnityEngine.Matrix4x4 aMatrix)
  RVA=0x041E1670  token=0x6000086  System.Void Add(System.String aKey, ZSimpleJSON.JSONNode aItem)
  RVA=0x03B4AD90  token=0x6000087  System.Void Add(ZSimpleJSON.JSONNode aItem)
  RVA=0x011EC580  token=0x6000088  ZSimpleJSON.JSONNode Remove(System.String aKey)
  RVA=0x011EC580  token=0x6000089  ZSimpleJSON.JSONNode Remove(System.Int32 aIndex)
  RVA=0x04D88C30  token=0x600008A  ZSimpleJSON.JSONNode Remove(ZSimpleJSON.JSONNode aNode)
  RVA=0x041E1670  token=0x600008B  System.Void Clear()
  RVA=0x011EC580  token=0x600008C  ZSimpleJSON.JSONNode Clone()
  RVA=0x012081B0  token=0x600008F  System.Boolean HasKey(System.String aKey)
  RVA=0x04D8C7E0  token=0x6000090  ZSimpleJSON.JSONNode GetValueOrDefault(System.String aKey, ZSimpleJSON.JSONNode aDefault)
  RVA=0x04610F80  token=0x6000091  System.String ToString()
  RVA=0x0B8B9520  token=0x6000092  System.String ToString(System.Int32 aIndent)
  RVA=0x03A756A0  token=0x6000093  System.Void ToStringBuild(System.Text.StringBuilder& sb)
  RVA=-1  // abstract  token=0x6000094  System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, ZSimpleJSON.JSONTextMode aMode)
  RVA=-1  // abstract  token=0x6000095  ZSimpleJSON.JSONNode.Enumerator GetEnumerator()
  RVA=0x03B4AEF0  token=0x60000A7  ZSimpleJSON.JSONNode op_Implicit(System.String s)
  RVA=0x0B8BB63C  token=0x60000A8  System.String op_Implicit(ZSimpleJSON.JSONNode d)
  RVA=0x03B4AA70  token=0x60000A9  ZSimpleJSON.JSONNode op_Implicit(System.Double n)
  RVA=0x0B8BBAFC  token=0x60000AA  System.Double op_Implicit(ZSimpleJSON.JSONNode d)
  RVA=0x03B4AC60  token=0x60000AB  ZSimpleJSON.JSONNode op_Implicit(System.Single n)
  RVA=0x0B8BBD04  token=0x60000AC  System.Single op_Implicit(ZSimpleJSON.JSONNode d)
  RVA=0x03B4ACB0  token=0x60000AD  ZSimpleJSON.JSONNode op_Implicit(System.Int32 n)
  RVA=0x0B8BBE3C  token=0x60000AE  System.Int32 op_Implicit(ZSimpleJSON.JSONNode d)
  RVA=0x03B4ABE0  token=0x60000AF  ZSimpleJSON.JSONNode op_Implicit(System.Int64 n)
  RVA=0x0B8BC0BC  token=0x60000B0  System.Int64 op_Implicit(ZSimpleJSON.JSONNode d)
  RVA=0x03A72870  token=0x60000B1  ZSimpleJSON.JSONNode op_Implicit(System.UInt64 n)
  RVA=0x0B8BB468  token=0x60000B2  System.UInt64 op_Implicit(ZSimpleJSON.JSONNode d)
  RVA=0x03A74420  token=0x60000B3  ZSimpleJSON.JSONNode op_Implicit(System.Boolean b)
  RVA=0x0B8BC60C  token=0x60000B4  System.Boolean op_Implicit(ZSimpleJSON.JSONNode d)
  RVA=0x0B8BBF68  token=0x60000B5  ZSimpleJSON.JSONNode op_Implicit(System.Collections.Generic.KeyValuePair<System.String,ZSimpleJSON.JSONNode> aKeyValue)
  RVA=0x03D53B10  token=0x60000B6  System.Boolean op_Equality(ZSimpleJSON.JSONNode a, System.Object b)
  RVA=0x03D539A0  token=0x60000B7  System.Boolean op_Inequality(ZSimpleJSON.JSONNode a, System.Object b)
  RVA=0x03D53990  token=0x60000B8  System.Boolean Equals(System.Object obj)
  RVA=0x04D87100  token=0x60000B9  System.Int32 GetHashCode()
  RVA=0x0360C7B0  token=0x60000BB  System.String Escape(System.String aText)
  RVA=0x03B4A400  token=0x60000BC  ZSimpleJSON.JSONNode ParseElement(System.String token, System.Boolean quoted)
  RVA=0x03B49E20  token=0x60000BD  ZSimpleJSON.JSONNode Parse(System.String aJSON)
  RVA=0x041E1670  token=0x60000BE  System.Void .ctor()
  RVA=0x04CDC2C0  token=0x60000BF  System.Void .cctor()
END_CLASS

CLASS: ZSimpleJSON.JSONContainerType
TYPE:  sealed struct
TOKEN: 0x200000A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  ZSimpleJSON.JSONContainerType   Array  // const
  public    static  ZSimpleJSON.JSONContainerType   Object  // const
METHODS:
END_CLASS

CLASS: ZSimpleJSON.JSONNodeType
TYPE:  sealed struct
TOKEN: 0x200000B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  ZSimpleJSON.JSONNodeType        Array  // const
  public    static  ZSimpleJSON.JSONNodeType        Object  // const
  public    static  ZSimpleJSON.JSONNodeType        String  // const
  public    static  ZSimpleJSON.JSONNodeType        Number  // const
  public    static  ZSimpleJSON.JSONNodeType        NullValue  // const
  public    static  ZSimpleJSON.JSONNodeType        Boolean  // const
  public    static  ZSimpleJSON.JSONNodeType        None  // const
  public    static  ZSimpleJSON.JSONNodeType        Custom  // const
METHODS:
END_CLASS

CLASS: ZSimpleJSON.JSONTextMode
TYPE:  sealed struct
TOKEN: 0x200000C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  ZSimpleJSON.JSONTextMode        Compact  // const
  public    static  ZSimpleJSON.JSONTextMode        Indent  // const
METHODS:
END_CLASS

CLASS: ZSimpleJSON.JSONArray
TYPE:  class
TOKEN: 0x200000D
SIZE:  0x20
EXTENDS: ZSimpleJSON.JSONNode
FIELDS:
  private           System.Collections.Generic.List<ZSimpleJSON.JSONNode>m_List  // 0x10
  private           System.Boolean                  inline  // 0x18
PROPERTIES:
  Inline  get=0x04D86340  set=0x04D86350
  Tag  get=0x04D85B80
  IsArray  get=0x02FFF600
  Item  get=0x0B8B7168  set=0x03D53860
  Item  get=0x0B8B7168  set=0x03D53860
  Count  get=0x0B8B70C8
  Children  get=0x0B8B7058
METHODS:
  RVA=0x0B8B6ECC  token=0x60000EF  ZSimpleJSON.JSONNode.Enumerator GetEnumerator()
  RVA=0x03D53730  token=0x60000F5  System.Void Add(System.String aKey, ZSimpleJSON.JSONNode aItem)
  RVA=0x0B8B6F5C  token=0x60000F6  ZSimpleJSON.JSONNode Remove(System.Int32 aIndex)
  RVA=0x0B8B7000  token=0x60000F7  ZSimpleJSON.JSONNode Remove(ZSimpleJSON.JSONNode aNode)
  RVA=0x0B8B6C54  token=0x60000F8  System.Void Clear()
  RVA=0x0B8B6C9C  token=0x60000F9  ZSimpleJSON.JSONNode Clone()
  RVA=0x0360E340  token=0x60000FB  System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, ZSimpleJSON.JSONTextMode aMode)
  RVA=0x0355DBA0  token=0x60000FC  System.Void .ctor()
END_CLASS

CLASS: ZSimpleJSON.JSONObject
TYPE:  class
TOKEN: 0x200000F
SIZE:  0x20
EXTENDS: ZSimpleJSON.JSONNode
FIELDS:
  private           System.Collections.Generic.Dictionary<System.String,ZSimpleJSON.JSONNode>m_Dict  // 0x10
  private           System.Boolean                  inline  // 0x18
PROPERTIES:
  Inline  get=0x04D86340  set=0x04D86350
  Tag  get=0x02BBE540
  IsObject  get=0x02FFF600
  Item  get=0x0B8BD9B8  set=0x03D539F0
  Item  get=0x0B8BD9B8  set=0x03D539F0
  Count  get=0x0B8BD8EC
  Children  get=0x0B8BD87C
METHODS:
  RVA=0x04B0C340  token=0x600010A  ZSimpleJSON.JSONNode.Enumerator GetEnumerator()
  RVA=0x03D537A0  token=0x6000110  System.Void Add(System.String aKey, ZSimpleJSON.JSONNode aItem)
  RVA=0x0B8BD558  token=0x6000111  ZSimpleJSON.JSONNode Remove(System.String aKey)
  RVA=0x0B8BD610  token=0x6000112  ZSimpleJSON.JSONNode Remove(System.Int32 aIndex)
  RVA=0x0B8BD6D0  token=0x6000113  ZSimpleJSON.JSONNode Remove(ZSimpleJSON.JSONNode aNode)
  RVA=0x04553E10  token=0x6000114  System.Void Clear()
  RVA=0x0B8BD2BC  token=0x6000115  ZSimpleJSON.JSONNode Clone()
  RVA=0x0B8BD504  token=0x6000116  System.Boolean HasKey(System.String aKey)
  RVA=0x0B8BD488  token=0x6000117  ZSimpleJSON.JSONNode GetValueOrDefault(System.String aKey, ZSimpleJSON.JSONNode aDefault)
  RVA=0x0360E070  token=0x6000119  System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, ZSimpleJSON.JSONTextMode aMode)
  RVA=0x03B490B0  token=0x600011A  System.Void .ctor()
END_CLASS

CLASS: ZSimpleJSON.JSONString
TYPE:  class
TOKEN: 0x2000012
SIZE:  0x18
EXTENDS: ZSimpleJSON.JSONNode
FIELDS:
  private           System.String                   m_Data  // 0x10
PROPERTIES:
  Tag  get=0x04D8C9E0
  IsString  get=0x02FFF600
  Value  get=0x02B2ECC0  set=0x053908C0
METHODS:
  RVA=0x04DA2D20  token=0x6000128  ZSimpleJSON.JSONNode.Enumerator GetEnumerator()
  RVA=0x03B4AF60  token=0x600012B  System.Void .ctor(System.String aData)
  RVA=0x0B8BDBBC  token=0x600012C  ZSimpleJSON.JSONNode Clone()
  RVA=0x0360D970  token=0x600012D  System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, ZSimpleJSON.JSONTextMode aMode)
  RVA=0x03D53900  token=0x600012E  System.Boolean Equals(System.Object obj)
  RVA=0x08479504  token=0x600012F  System.Int32 GetHashCode()
  RVA=0x0B8BDB7C  token=0x6000130  System.Void Clear()
END_CLASS

CLASS: ZSimpleJSON.JSONNumber
TYPE:  class
TOKEN: 0x2000013
SIZE:  0x18
EXTENDS: ZSimpleJSON.JSONNode
FIELDS:
  private           System.Double                   m_Data  // 0x10
PROPERTIES:
  Tag  get=0x04D882B0
  IsNumber  get=0x02FFF600
  Value  get=0x0382D9A0  set=0x0B8BD23C
  AsDouble  get=0x04D96320  set=0x04D85BA0
  AsLong  get=0x04DA2D40  set=0x04DA2D50
  AsULong  get=0x0B8BD230  set=0x04DBF6A0
METHODS:
  RVA=0x04DA2D20  token=0x6000133  ZSimpleJSON.JSONNode.Enumerator GetEnumerator()
  RVA=0x03B4AD00  token=0x600013C  System.Void .ctor(System.Double aData)
  RVA=0x0B8BD1D4  token=0x600013D  System.Void .ctor(System.String aData)
  RVA=0x0B8BD024  token=0x600013E  ZSimpleJSON.JSONNode Clone()
  RVA=0x040717C0  token=0x600013F  System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, ZSimpleJSON.JSONTextMode aMode)
  RVA=0x0B8BD08C  token=0x6000140  System.Boolean IsNumeric(System.Object value)
  RVA=0x04232AD0  token=0x6000141  System.Boolean Equals(System.Object obj)
  RVA=0x09DC1AD4  token=0x6000142  System.Int32 GetHashCode()
  RVA=0x04DA8180  token=0x6000143  System.Void Clear()
END_CLASS

CLASS: ZSimpleJSON.JSONBool
TYPE:  class
TOKEN: 0x2000014
SIZE:  0x18
EXTENDS: ZSimpleJSON.JSONNode
FIELDS:
  private           System.Boolean                  m_Data  // 0x10
PROPERTIES:
  Tag  get=0x04D9EC60
  IsBoolean  get=0x02FFF600
  Value  get=0x0B8B73B0  set=0x0B8B7404
  AsBool  get=0x015EFCE0  set=0x04D86130
METHODS:
  RVA=0x04DA2D20  token=0x6000146  ZSimpleJSON.JSONNode.Enumerator GetEnumerator()
  RVA=0x03A74470  token=0x600014B  System.Void .ctor(System.Boolean aData)
  RVA=0x0B8B7354  token=0x600014C  System.Void .ctor(System.String aData)
  RVA=0x0B8B72A8  token=0x600014D  ZSimpleJSON.JSONNode Clone()
  RVA=0x04503B80  token=0x600014E  System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, ZSimpleJSON.JSONTextMode aMode)
  RVA=0x045F14B0  token=0x600014F  System.Boolean Equals(System.Object obj)
  RVA=0x0B8B730C  token=0x6000150  System.Int32 GetHashCode()
  RVA=0x04D8CCB0  token=0x6000151  System.Void Clear()
END_CLASS

CLASS: ZSimpleJSON.JSONNull
TYPE:  class
TOKEN: 0x2000015
SIZE:  0x10
EXTENDS: ZSimpleJSON.JSONNode
FIELDS:
  private   static  ZSimpleJSON.JSONNull            m_StaticInstance  // static @ 0x0
  public    static  System.Boolean                  reuseSameInstance  // static @ 0x8
PROPERTIES:
  Tag  get=0x04D9EC50
  IsNull  get=0x02FFF600
  Value  get=0x0B8BCFF8  set=0x041E1670
  AsBool  get=0x012081B0  set=0x041E1670
METHODS:
  RVA=0x03D53BC0  token=0x6000152  ZSimpleJSON.JSONNull CreateOrGet()
  RVA=0x04CE7B70  token=0x6000153  System.Void .ctor()
  RVA=0x04DA2D20  token=0x6000156  ZSimpleJSON.JSONNode.Enumerator GetEnumerator()
  RVA=0x0B8BCF60  token=0x600015B  ZSimpleJSON.JSONNode Clone()
  RVA=0x0B8BCFA0  token=0x600015C  System.Boolean Equals(System.Object obj)
  RVA=0x011EC580  token=0x600015D  System.Int32 GetHashCode()
  RVA=0x045F0B90  token=0x600015E  System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, ZSimpleJSON.JSONTextMode aMode)
  RVA=0x04CE7AE0  token=0x600015F  System.Void .cctor()
END_CLASS

CLASS: ZSimpleJSON.JSONLazyCreator
TYPE:  class
TOKEN: 0x2000016
SIZE:  0x20
EXTENDS: ZSimpleJSON.JSONNode
FIELDS:
  private           ZSimpleJSON.JSONNode            m_Node  // 0x10
  private           System.String                   m_Key  // 0x18
PROPERTIES:
  Tag  get=0x04D9BE00
  Item  get=0x0B8B7B98  set=0x0B8B81BC
  Item  get=0x0B8B7B98  set=0x0B8B81BC
  AsInt  get=0x0B8B78AC  set=0x0B8B7E04
  AsFloat  get=0x0B8B782C  set=0x0B8B7D78
  AsDouble  get=0x0B8B77AC  set=0x0B8B7CF0
  AsLong  get=0x0B8B7928  set=0x0B8B7E90
  AsULong  get=0x0B8B7A9C  set=0x0B8B7FC4
  AsBool  get=0x0B8B7730  set=0x0B8B7C68
  AsArray  get=0x0B8B76B8
  AsObject  get=0x0B8B7A24
METHODS:
  RVA=0x04DA2D20  token=0x6000161  ZSimpleJSON.JSONNode.Enumerator GetEnumerator()
  RVA=0x0B8B75EC  token=0x6000162  System.Void .ctor(ZSimpleJSON.JSONNode aNode)
  RVA=0x0B8B764C  token=0x6000163  System.Void .ctor(ZSimpleJSON.JSONNode aNode, System.String aKey)
  RVA=-1  // generic def  token=0x6000164  T Set(T aVal)
  RVA=0x0B8B7468  token=0x6000169  System.Void Add(ZSimpleJSON.JSONNode aItem)
  RVA=0x0B8B74FC  token=0x600016A  System.Void Add(System.String aKey, ZSimpleJSON.JSONNode aItem)
  RVA=0x09DC0F98  token=0x600016B  System.Boolean op_Equality(ZSimpleJSON.JSONLazyCreator a, System.Object b)
  RVA=0x09DC1438  token=0x600016C  System.Boolean op_Inequality(ZSimpleJSON.JSONLazyCreator a, System.Object b)
  RVA=0x09DC0F98  token=0x600016D  System.Boolean Equals(System.Object obj)
  RVA=0x011EC580  token=0x600016E  System.Int32 GetHashCode()
  RVA=0x0B8B75A4  token=0x600017D  System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, ZSimpleJSON.JSONTextMode aMode)
END_CLASS

CLASS: ZSimpleJSON.JSON
TYPE:  static class
TOKEN: 0x2000017
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x04D5DBE0  token=0x600017E  ZSimpleJSON.JSONNode Parse(System.String aJSON)
END_CLASS

