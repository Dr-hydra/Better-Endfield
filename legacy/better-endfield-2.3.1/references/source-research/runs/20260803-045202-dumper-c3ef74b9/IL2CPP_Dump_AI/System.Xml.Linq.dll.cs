// ========================================================
// Dumped by @desirepro
// Assembly: System.Xml.Linq.dll
// Classes:  38
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

CLASS: SR
TYPE:  static class
TOKEN: 0x2000002
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0AE476F8  token=0x6000001  System.String Format(System.String resourceFormat, System.Object p1)
  RVA=0x0AE47688  token=0x6000002  System.String Format(System.String resourceFormat, System.Object p1, System.Object p2)
END_CLASS

CLASS: ContentReader
TYPE:  sealed class
TOKEN: 0x200000A
SIZE:  0x48
FIELDS:
  private   readonly System.Xml.Linq.NamespaceCache  _eCache  // 0x10
  private   readonly System.Xml.Linq.NamespaceCache  _aCache  // 0x20
  private   readonly System.Xml.IXmlLineInfo         _lineInfo  // 0x30
  private           System.Xml.Linq.XContainer      _currentContainer  // 0x38
  private           System.String                   _baseUri  // 0x40
METHODS:
  RVA=0x046A91B0  token=0x6000031  System.Void .ctor(System.Xml.Linq.XContainer rootContainer)
  RVA=0x0AE46C14  token=0x6000032  System.Void .ctor(System.Xml.Linq.XContainer rootContainer, System.Xml.XmlReader r, System.Xml.Linq.LoadOptions o)
  RVA=0x0AE46604  token=0x6000033  System.Boolean ReadContentFrom(System.Xml.Linq.XContainer rootContainer, System.Xml.XmlReader r)
  RVA=0x0AE45DA0  token=0x6000034  System.Boolean ReadContentFrom(System.Xml.Linq.XContainer rootContainer, System.Xml.XmlReader r, System.Xml.Linq.LoadOptions o)
END_CLASS

CLASS: <Nodes>d__18
TYPE:  sealed class
TOKEN: 0x200000B
SIZE:  0x38
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.IDisposable System.Collections.IEnumerator
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Xml.Linq.XNode           <>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x20
  public            System.Xml.Linq.XContainer      <>4__this  // 0x28
  private           System.Xml.Linq.XNode           <n>5__2  // 0x30
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Xml.Linq.XNode>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x03A11010  token=0x6000035  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000036  System.Void System.IDisposable.Dispose()
  RVA=0x0AE47AEC  token=0x6000037  System.Boolean MoveNext()
  RVA=0x0AE47C28  token=0x6000039  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x0AE47B94  token=0x600003B  System.Collections.Generic.IEnumerator<System.Xml.Linq.XNode> System.Collections.Generic.IEnumerable<System.Xml.Linq.XNode>.GetEnumerator()
  RVA=0x0AE47C20  token=0x600003C  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: <GetAttributes>d__116
TYPE:  sealed class
TOKEN: 0x2000010
SIZE:  0x48
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable System.Collections.Generic.IEnumerator`1 System.IDisposable System.Collections.IEnumerator
FIELDS:
  private           System.Int32                    <>1__state  // 0x10
  private           System.Xml.Linq.XAttribute      <>2__current  // 0x18
  private           System.Int32                    <>l__initialThreadId  // 0x20
  public            System.Xml.Linq.XElement        <>4__this  // 0x28
  private           System.Xml.Linq.XName           name  // 0x30
  public            System.Xml.Linq.XName           <>3__name  // 0x38
  private           System.Xml.Linq.XAttribute      <a>5__2  // 0x40
PROPERTIES:
  System.Collections.Generic.IEnumerator<System.Xml.Linq.XAttribute>.Current  get=0x0385B100
  System.Collections.IEnumerator.Current  get=0x0385B100
METHODS:
  RVA=0x03A11010  token=0x6000075  System.Void .ctor(System.Int32 <>1__state)
  RVA=0x041E1670  token=0x6000076  System.Void System.IDisposable.Dispose()
  RVA=0x0AE47934  token=0x6000077  System.Boolean MoveNext()
  RVA=0x0AE47AA0  token=0x6000079  System.Void System.Collections.IEnumerator.Reset()
  RVA=0x0AE479F4  token=0x600007B  System.Collections.Generic.IEnumerator<System.Xml.Linq.XAttribute> System.Collections.Generic.IEnumerable<System.Xml.Linq.XAttribute>.GetEnumerator()
  RVA=0x0AE47A98  token=0x600007C  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
END_CLASS

CLASS: ExtractKeyDelegate
TYPE:  sealed class
TOKEN: 0x2000012
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=-1  // runtime  token=0x6000080  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=-1  // runtime  token=0x6000081  System.String Invoke(TValue value)
END_CLASS

CLASS: Entry
TYPE:  sealed struct
TOKEN: 0x2000014
FIELDS:
  public            TValue                          Value  // 0x0
  public            System.Int32                    HashCode  // 0x0
  public            System.Int32                    Next  // 0x0
METHODS:
END_CLASS

CLASS: XHashtableState
TYPE:  sealed class
TOKEN: 0x2000013
FIELDS:
  private           System.Int32[]                  _buckets  // 0x0
  private           System.Xml.Linq.XHashtable.XHashtableState.Entry<TValue>[]_entries  // 0x0
  private           System.Int32                    _numEntries  // 0x0
  private           System.Xml.Linq.XHashtable.ExtractKeyDelegate<TValue>_extractKey  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000082  System.Void .ctor(System.Xml.Linq.XHashtable.ExtractKeyDelegate<TValue> extractKey, System.Int32 capacity)
  RVA=-1  // not resolved  token=0x6000083  System.Xml.Linq.XHashtable.XHashtableState<TValue> Resize()
  RVA=-1  // not resolved  token=0x6000084  System.Boolean TryGetValue(System.String key, System.Int32 index, System.Int32 count, TValue& value)
  RVA=-1  // not resolved  token=0x6000085  System.Boolean TryAdd(TValue value, TValue& newValue)
  RVA=-1  // not resolved  token=0x6000086  System.Boolean FindEntry(System.Int32 hashCode, System.String key, System.Int32 index, System.Int32 count, System.Int32& entryIndex)
  RVA=-1  // not resolved  token=0x6000087  System.Int32 ComputeHashCode(System.String key, System.Int32 index, System.Int32 count)
END_CLASS

CLASS: NamespaceDeclaration
TYPE:  class
TOKEN: 0x2000018
SIZE:  0x30
FIELDS:
  public            System.String                   prefix  // 0x10
  public            System.Xml.Linq.XNamespace      ns  // 0x18
  public            System.Int32                    scope  // 0x20
  public            System.Xml.Linq.NamespaceResolver.NamespaceDeclarationprev  // 0x28
METHODS:
  RVA=0x041E1670  token=0x6000096  System.Void .ctor()
END_CLASS

CLASS: System.Text.StringBuilderCache
TYPE:  static class
TOKEN: 0x2000025
SIZE:  0x10
FIELDS:
  private   static  System.Text.StringBuilder       t_cachedInstance  // static @ 0xffffffff
METHODS:
  RVA=0x0AE47754  token=0x60000E1  System.Text.StringBuilder Acquire(System.Int32 capacity)
  RVA=0x0AE47870  token=0x60000E2  System.Void Release(System.Text.StringBuilder sb)
  RVA=0x0AE4782C  token=0x60000E3  System.String GetStringAndRelease(System.Text.StringBuilder sb)
END_CLASS

CLASS: System.Xml.Linq.BaseUriAnnotation
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x18
FIELDS:
  private           System.String                   baseUri  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000003  System.Void .ctor(System.String baseUri)
END_CLASS

CLASS: System.Xml.Linq.LineInfoAnnotation
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x18
FIELDS:
  private           System.Int32                    lineNumber  // 0x10
  private           System.Int32                    linePosition  // 0x14
METHODS:
  RVA=0x04D88290  token=0x6000004  System.Void .ctor(System.Int32 lineNumber, System.Int32 linePosition)
END_CLASS

CLASS: System.Xml.Linq.LineInfoEndElementAnnotation
TYPE:  class
TOKEN: 0x2000005
SIZE:  0x18
EXTENDS: System.Xml.Linq.LineInfoAnnotation
FIELDS:
METHODS:
  RVA=0x04D88290  token=0x6000005  System.Void .ctor(System.Int32 lineNumber, System.Int32 linePosition)
END_CLASS

CLASS: System.Xml.Linq.XAttribute
TYPE:  class
TOKEN: 0x2000006
SIZE:  0x38
EXTENDS: System.Xml.Linq.XObject
FIELDS:
  private           System.Xml.Linq.XAttribute      next  // 0x20
  private           System.Xml.Linq.XName           name  // 0x28
  private           System.String                   value  // 0x30
PROPERTIES:
  IsNamespaceDeclaration  get=0x0AE48574
  Name  get=0x04D86240
  NodeType  get=0x02BBE540
  Value  get=0x011F36E0
METHODS:
  RVA=0x0AE483DC  token=0x6000006  System.Void .ctor(System.Xml.Linq.XName name, System.Object value)
  RVA=0x0AE484E8  token=0x6000007  System.Void .ctor(System.Xml.Linq.XAttribute other)
  RVA=0x0AE47D88  token=0x600000C  System.String ToString()
  RVA=0x0AE47C74  token=0x600000D  System.String GetPrefixOfNamespace(System.Xml.Linq.XNamespace ns)
  RVA=0x0AE48018  token=0x600000E  System.Void ValidateAttribute(System.Xml.Linq.XName name, System.String value)
END_CLASS

CLASS: System.Xml.Linq.XCData
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x30
EXTENDS: System.Xml.Linq.XText
FIELDS:
PROPERTIES:
  NodeType  get=0x04D882B0
METHODS:
  RVA=0x0AE486D0  token=0x600000F  System.Void .ctor(System.String value)
  RVA=0x0AE486D8  token=0x6000010  System.Void .ctor(System.Xml.Linq.XCData other)
  RVA=0x0AE48658  token=0x6000012  System.Void WriteTo(System.Xml.XmlWriter writer)
  RVA=0x0AE485F8  token=0x6000013  System.Xml.Linq.XNode CloneNode()
END_CLASS

CLASS: System.Xml.Linq.XComment
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x30
EXTENDS: System.Xml.Linq.XNode
FIELDS:
  private           System.String                   value  // 0x28
PROPERTIES:
  NodeType  get=0x04D8D060
  Value  get=0x04D86240
METHODS:
  RVA=0x0AE48830  token=0x6000014  System.Void .ctor(System.String value)
  RVA=0x0AE487B8  token=0x6000015  System.Void .ctor(System.Xml.Linq.XComment other)
  RVA=0x0AE48740  token=0x6000018  System.Void WriteTo(System.Xml.XmlWriter writer)
  RVA=0x0AE486E0  token=0x6000019  System.Xml.Linq.XNode CloneNode()
END_CLASS

CLASS: System.Xml.Linq.XContainer
TYPE:  abstract class
TOKEN: 0x2000009
SIZE:  0x30
EXTENDS: System.Xml.Linq.XNode
FIELDS:
  private           System.Object                   content  // 0x28
PROPERTIES:
  LastNode  get=0x0AE49F10
METHODS:
  RVA=0x041E1670  token=0x600001A  System.Void .ctor()
  RVA=0x0AE49E0C  token=0x600001B  System.Void .ctor(System.Xml.Linq.XContainer other)
  RVA=0x0AE48F94  token=0x600001D  System.Void Add(System.Object content)
  RVA=0x0AE498FC  token=0x600001E  System.Collections.Generic.IEnumerable<System.Xml.Linq.XNode> Nodes()
  RVA=0x041E1670  token=0x600001F  System.Void AddAttribute(System.Xml.Linq.XAttribute a)
  RVA=0x041E1670  token=0x6000020  System.Void AddAttributeSkipNotify(System.Xml.Linq.XAttribute a)
  RVA=0x0AE488A4  token=0x6000021  System.Void AddContentSkipNotify(System.Object content)
  RVA=0x0AE48BE4  token=0x6000022  System.Void AddNode(System.Xml.Linq.XNode n)
  RVA=0x0AE48B6C  token=0x6000023  System.Void AddNodeSkipNotify(System.Xml.Linq.XNode n)
  RVA=0x0AE48D8C  token=0x6000024  System.Void AddString(System.String s)
  RVA=0x0AE48C5C  token=0x6000025  System.Void AddStringSkipNotify(System.String s)
  RVA=0x0AE49358  token=0x6000026  System.Void AppendNode(System.Xml.Linq.XNode n)
  RVA=0x0AE4929C  token=0x6000027  System.Void AppendNodeSkipNotify(System.Xml.Linq.XNode n)
  RVA=0x0AE4946C  token=0x6000028  System.Void AppendText(System.Text.StringBuilder sb)
  RVA=0x0AE49520  token=0x6000029  System.Void ConvertTextToNode()
  RVA=0x0AE495D4  token=0x600002A  System.String GetStringValue(System.Object value)
  RVA=0x0AE4996C  token=0x600002B  System.Void ReadContentFrom(System.Xml.XmlReader r)
  RVA=0x0AE49A68  token=0x600002C  System.Void ReadContentFrom(System.Xml.XmlReader r, System.Xml.Linq.LoadOptions o)
  RVA=0x0AE49B9C  token=0x600002D  System.Void RemoveNode(System.Xml.Linq.XNode n)
  RVA=0x041E1670  token=0x600002E  System.Void ValidateNode(System.Xml.Linq.XNode node, System.Xml.Linq.XNode previous)
  RVA=0x041E1670  token=0x600002F  System.Void ValidateString(System.String s)
  RVA=0x0AE49D28  token=0x6000030  System.Void WriteContentTo(System.Xml.XmlWriter writer)
END_CLASS

CLASS: System.Xml.Linq.XDeclaration
TYPE:  class
TOKEN: 0x200000C
SIZE:  0x28
FIELDS:
  private           System.String                   _version  // 0x10
  private           System.String                   _encoding  // 0x18
  private           System.String                   _standalone  // 0x20
PROPERTIES:
  Encoding  get=0x0385B100
  Standalone  get=0x04D862C0
  Version  get=0x02B2ECC0
METHODS:
  RVA=0x031D14F0  token=0x600003D  System.Void .ctor(System.String version, System.String encoding, System.String standalone)
  RVA=0x0AE4A160  token=0x600003E  System.Void .ctor(System.Xml.Linq.XDeclaration other)
  RVA=0x0AE4A00C  token=0x6000042  System.String ToString()
END_CLASS

CLASS: System.Xml.Linq.XDocument
TYPE:  class
TOKEN: 0x200000D
SIZE:  0x38
EXTENDS: System.Xml.Linq.XContainer
FIELDS:
  private           System.Xml.Linq.XDeclaration    _declaration  // 0x30
PROPERTIES:
  Declaration  get=0x011F36E0  set=0x05396200
  NodeType  get=0x04DA4FC0
  Root  get=0x0AE4AAD8
METHODS:
  RVA=0x041E1670  token=0x6000043  System.Void .ctor()
  RVA=0x0AE4AA44  token=0x6000044  System.Void .ctor(System.Xml.Linq.XDocument other)
  RVA=0x0AE4A920  token=0x6000049  System.Void WriteTo(System.Xml.XmlWriter writer)
  RVA=0x0AE4A498  token=0x600004A  System.Void AddAttribute(System.Xml.Linq.XAttribute a)
  RVA=0x0AE4A43C  token=0x600004B  System.Void AddAttributeSkipNotify(System.Xml.Linq.XAttribute a)
  RVA=0x0AE4A4F4  token=0x600004C  System.Xml.Linq.XNode CloneNode()
  RVA=-1  // generic def  token=0x600004D  T GetFirstNode()
  RVA=0x0AE4A554  token=0x600004E  System.Boolean IsWhitespace(System.String s)
  RVA=0x0AE4A6B0  token=0x600004F  System.Void ValidateNode(System.Xml.Linq.XNode node, System.Xml.Linq.XNode previous)
  RVA=0x0AE4A5AC  token=0x6000050  System.Void ValidateDocument(System.Xml.Linq.XNode previous, System.Xml.XmlNodeType allowBefore, System.Xml.XmlNodeType allowAfter)
  RVA=0x0AE4A8B0  token=0x6000051  System.Void ValidateString(System.String s)
END_CLASS

CLASS: System.Xml.Linq.XDocumentType
TYPE:  class
TOKEN: 0x200000E
SIZE:  0x48
EXTENDS: System.Xml.Linq.XNode
FIELDS:
  private           System.String                   _name  // 0x28
  private           System.String                   _publicId  // 0x30
  private           System.String                   _systemId  // 0x38
  private           System.String                   _internalSubset  // 0x40
PROPERTIES:
  InternalSubset  get=0x04D85A60
  Name  get=0x04D86240
  NodeType  get=0x04DA5360
  PublicId  get=0x011F36E0
  SystemId  get=0x04D85A50
METHODS:
  RVA=0x0AE4A39C  token=0x6000052  System.Void .ctor(System.String name, System.String publicId, System.String systemId, System.String internalSubset)
  RVA=0x0AE4A2EC  token=0x6000053  System.Void .ctor(System.Xml.Linq.XDocumentType other)
  RVA=0x0AE4A260  token=0x6000059  System.Void WriteTo(System.Xml.XmlWriter writer)
  RVA=0x0AE4A200  token=0x600005A  System.Xml.Linq.XNode CloneNode()
END_CLASS

CLASS: System.Xml.Linq.XElement
TYPE:  class
TOKEN: 0x200000F
SIZE:  0x40
EXTENDS: System.Xml.Linq.XContainer
IMPLEMENTS: System.Xml.Serialization.IXmlSerializable
FIELDS:
  private           System.Xml.Linq.XName           name  // 0x30
  private           System.Xml.Linq.XAttribute      lastAttr  // 0x38
PROPERTIES:
  HasAttributes  get=0x09B0008C
  IsEmpty  get=0x0AE4BBDC
  Name  get=0x011F36E0
  NodeType  get=0x04D85B80
  Value  get=0x0AE4BBE8
METHODS:
  RVA=0x0AE4BB68  token=0x600005B  System.Void .ctor(System.Xml.Linq.XName name)
  RVA=0x0AE4BA1C  token=0x600005C  System.Void .ctor(System.Xml.Linq.XElement other)
  RVA=0x0AE4BADC  token=0x600005D  System.Void .ctor(System.Xml.Linq.XStreamingElement other)
  RVA=0x0AE4AE70  token=0x6000063  System.Xml.Linq.XAttribute Attribute(System.Xml.Linq.XName name)
  RVA=0x0AE4AEA4  token=0x6000064  System.Collections.Generic.IEnumerable<System.Xml.Linq.XAttribute> Attributes()
  RVA=0x0AE4B058  token=0x6000065  System.String GetPrefixOfNamespace(System.Xml.Linq.XNamespace ns)
  RVA=0x0AE4B974  token=0x6000066  System.Void WriteTo(System.Xml.XmlWriter writer)
  RVA=0x011EC580  token=0x6000067  System.Xml.Schema.XmlSchema System.Xml.Serialization.IXmlSerializable.GetSchema()
  RVA=0x0AE4B60C  token=0x6000068  System.Void System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader reader)
  RVA=0x0AE4B7E0  token=0x6000069  System.Void System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter writer)
  RVA=0x0AE4AC00  token=0x600006A  System.Void AddAttribute(System.Xml.Linq.XAttribute a)
  RVA=0x0AE4AB14  token=0x600006B  System.Void AddAttributeSkipNotify(System.Xml.Linq.XAttribute a)
  RVA=0x0AE4AD5C  token=0x600006C  System.Void AppendAttribute(System.Xml.Linq.XAttribute a)
  RVA=0x0AE4ACEC  token=0x600006D  System.Void AppendAttributeSkipNotify(System.Xml.Linq.XAttribute a)
  RVA=0x0AE4AEB0  token=0x600006E  System.Xml.Linq.XNode CloneNode()
  RVA=0x0AE4AF10  token=0x600006F  System.Collections.Generic.IEnumerable<System.Xml.Linq.XAttribute> GetAttributes(System.Xml.Linq.XName name)
  RVA=0x0AE4AF98  token=0x6000070  System.String GetNamespaceOfPrefixInScope(System.String prefix, System.Xml.Linq.XElement outOfScope)
  RVA=0x0AE4B524  token=0x6000071  System.Void ReadElementFrom(System.Xml.XmlReader r, System.Xml.Linq.LoadOptions o)
  RVA=0x0AE4B260  token=0x6000072  System.Void ReadElementFromImpl(System.Xml.XmlReader r, System.Xml.Linq.LoadOptions o)
  RVA=0x0AE4B59C  token=0x6000073  System.Void SetEndElementLineInfo(System.Int32 lineNumber, System.Int32 linePosition)
  RVA=0x0AE4B7F4  token=0x6000074  System.Void ValidateNode(System.Xml.Linq.XNode node, System.Xml.Linq.XNode previous)
END_CLASS

CLASS: System.Xml.Linq.XHashtable`1
TYPE:  sealed class
TOKEN: 0x2000011
FIELDS:
  private           System.Xml.Linq.XHashtable.XHashtableState<TValue>_state  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x600007D  System.Void .ctor(System.Xml.Linq.XHashtable.ExtractKeyDelegate<TValue> extractKey, System.Int32 capacity)
  RVA=-1  // not resolved  token=0x600007E  System.Boolean TryGetValue(System.String key, System.Int32 index, System.Int32 count, TValue& value)
  RVA=-1  // not resolved  token=0x600007F  TValue Add(TValue value)
END_CLASS

CLASS: System.Xml.Linq.NamespaceCache
TYPE:  sealed struct
TOKEN: 0x2000015
SIZE:  0x20
FIELDS:
  private           System.Xml.Linq.XNamespace      _ns  // 0x10
  private           System.String                   _namespaceName  // 0x18
METHODS:
  RVA=0x0AE47338  token=0x6000088  System.Xml.Linq.XNamespace Get(System.String namespaceName)
END_CLASS

CLASS: System.Xml.Linq.ElementWriter
TYPE:  sealed struct
TOKEN: 0x2000016
SIZE:  0x30
FIELDS:
  private           System.Xml.XmlWriter            _writer  // 0x10
  private           System.Xml.Linq.NamespaceResolver_resolver  // 0x18
METHODS:
  RVA=0x0AE47314  token=0x6000089  System.Void .ctor(System.Xml.XmlWriter writer)
  RVA=0x0AE46FB4  token=0x600008A  System.Void WriteElement(System.Xml.Linq.XElement e)
  RVA=0x0AE46CD4  token=0x600008B  System.String GetPrefixOfNamespace(System.Xml.Linq.XNamespace ns, System.Boolean allowDefaultNamespace)
  RVA=0x0AE46DC8  token=0x600008C  System.Void PushAncestors(System.Xml.Linq.XElement e)
  RVA=0x0AE46ED0  token=0x600008D  System.Void PushElement(System.Xml.Linq.XElement e)
  RVA=0x0AE4711C  token=0x600008E  System.Void WriteEndElement()
  RVA=0x0AE47150  token=0x600008F  System.Void WriteFullEndElement()
  RVA=0x0AE47184  token=0x6000090  System.Void WriteStartElement(System.Xml.Linq.XElement e)
END_CLASS

CLASS: System.Xml.Linq.NamespaceResolver
TYPE:  sealed struct
TOKEN: 0x2000017
SIZE:  0x28
FIELDS:
  private           System.Int32                    _scope  // 0x10
  private           System.Xml.Linq.NamespaceResolver.NamespaceDeclaration_declaration  // 0x18
  private           System.Xml.Linq.NamespaceResolver.NamespaceDeclaration_rover  // 0x20
METHODS:
  RVA=0x04DAA480  token=0x6000091  System.Void PushScope()
  RVA=0x0AE47614  token=0x6000092  System.Void PopScope()
  RVA=0x0AE47454  token=0x6000093  System.Void Add(System.String prefix, System.Xml.Linq.XNamespace ns)
  RVA=0x0AE4737C  token=0x6000094  System.Void AddFirst(System.String prefix, System.Xml.Linq.XNamespace ns)
  RVA=0x0AE4751C  token=0x6000095  System.String GetPrefixOfNamespace(System.Xml.Linq.XNamespace ns, System.Boolean allowDefaultNamespace)
END_CLASS

CLASS: System.Xml.Linq.XObjectChange
TYPE:  sealed struct
TOKEN: 0x2000019
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  System.Xml.Linq.XObjectChange   Add  // const
  public    static  System.Xml.Linq.XObjectChange   Remove  // const
  public    static  System.Xml.Linq.XObjectChange   Name  // const
  public    static  System.Xml.Linq.XObjectChange   Value  // const
METHODS:
END_CLASS

CLASS: System.Xml.Linq.LoadOptions
TYPE:  sealed struct
TOKEN: 0x200001A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  System.Xml.Linq.LoadOptions     None  // const
  public    static  System.Xml.Linq.LoadOptions     PreserveWhitespace  // const
  public    static  System.Xml.Linq.LoadOptions     SetBaseUri  // const
  public    static  System.Xml.Linq.LoadOptions     SetLineInfo  // const
METHODS:
END_CLASS

CLASS: System.Xml.Linq.SaveOptions
TYPE:  sealed struct
TOKEN: 0x200001B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  System.Xml.Linq.SaveOptions     None  // const
  public    static  System.Xml.Linq.SaveOptions     DisableFormatting  // const
  public    static  System.Xml.Linq.SaveOptions     OmitDuplicateNamespaces  // const
METHODS:
END_CLASS

CLASS: System.Xml.Linq.XName
TYPE:  sealed class
TOKEN: 0x200001C
SIZE:  0x28
IMPLEMENTS: System.IEquatable`1 System.Runtime.Serialization.ISerializable
FIELDS:
  private           System.Xml.Linq.XNamespace      _ns  // 0x10
  private           System.String                   _localName  // 0x18
  private           System.Int32                    _hashCode  // 0x20
PROPERTIES:
  LocalName  get=0x0385B100
  Namespace  get=0x02B2ECC0
  NamespaceName  get=0x0AE4C010
METHODS:
  RVA=0x0AE4BF38  token=0x6000097  System.Void .ctor(System.Xml.Linq.XNamespace ns, System.String localName)
  RVA=0x0AE4BEBC  token=0x600009B  System.String ToString()
  RVA=0x0AE4BC70  token=0x600009C  System.Xml.Linq.XName Get(System.String expandedName)
  RVA=0x0AE4BE3C  token=0x600009D  System.Xml.Linq.XName Get(System.String localName, System.String namespaceName)
  RVA=0x0AE4C02C  token=0x600009E  System.Xml.Linq.XName op_Implicit(System.String expandedName)
  RVA=0x02FB1840  token=0x600009F  System.Boolean Equals(System.Object obj)
  RVA=0x02B70F90  token=0x60000A0  System.Int32 GetHashCode()
  RVA=0x02FB1840  token=0x60000A1  System.Boolean op_Equality(System.Xml.Linq.XName left, System.Xml.Linq.XName right)
  RVA=0x02FB1840  token=0x60000A2  System.Boolean System.IEquatable<System.Xml.Linq.XName>.Equals(System.Xml.Linq.XName other)
  RVA=0x0AE4BE70  token=0x60000A3  System.Void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
  RVA=0x0AE4BFE0  token=0x60000A4  System.Void .ctor()
END_CLASS

CLASS: System.Xml.Linq.XNamespace
TYPE:  sealed class
TOKEN: 0x200001D
SIZE:  0x28
FIELDS:
  private   static  System.Xml.Linq.XHashtable<System.WeakReference>s_namespaces  // static @ 0x0
  private   static  System.WeakReference            s_refNone  // static @ 0x8
  private   static  System.WeakReference            s_refXml  // static @ 0x10
  private   static  System.WeakReference            s_refXmlns  // static @ 0x18
  private           System.String                   _namespaceName  // 0x10
  private           System.Int32                    _hashCode  // 0x18
  private           System.Xml.Linq.XHashtable<System.Xml.Linq.XName>_names  // 0x20
PROPERTIES:
  NamespaceName  get=0x02B2ECC0
  None  get=0x0AE4C780
  Xml  get=0x0AE4C7DC
  Xmlns  get=0x0AE4C830
METHODS:
  RVA=0x0AE4C68C  token=0x60000A5  System.Void .ctor(System.String namespaceName)
  RVA=0x0AE4C2A0  token=0x60000A7  System.Xml.Linq.XName GetName(System.String localName)
  RVA=0x02B2ECC0  token=0x60000A8  System.String ToString()
  RVA=0x0AE4C618  token=0x60000AC  System.Xml.Linq.XNamespace Get(System.String namespaceName)
  RVA=0x0AE4C884  token=0x60000AD  System.Xml.Linq.XNamespace op_Implicit(System.String namespaceName)
  RVA=0x02FB1840  token=0x60000AE  System.Boolean Equals(System.Object obj)
  RVA=0x011EF5B0  token=0x60000AF  System.Int32 GetHashCode()
  RVA=0x02FB1840  token=0x60000B0  System.Boolean op_Equality(System.Xml.Linq.XNamespace left, System.Xml.Linq.XNamespace right)
  RVA=0x02F019D0  token=0x60000B1  System.Boolean op_Inequality(System.Xml.Linq.XNamespace left, System.Xml.Linq.XNamespace right)
  RVA=0x0AE4C198  token=0x60000B2  System.Xml.Linq.XName GetName(System.String localName, System.Int32 index, System.Int32 count)
  RVA=0x0AE4C318  token=0x60000B3  System.Xml.Linq.XNamespace Get(System.String namespaceName, System.Int32 index, System.Int32 count)
  RVA=0x053E4DA0  token=0x60000B4  System.String ExtractLocalName(System.Xml.Linq.XName n)
  RVA=0x0AE4C13C  token=0x60000B5  System.String ExtractNamespace(System.WeakReference r)
  RVA=0x0AE4C048  token=0x60000B6  System.Xml.Linq.XNamespace EnsureNamespace(System.WeakReference& refNmsp, System.String namespaceName)
END_CLASS

CLASS: System.Xml.Linq.XNode
TYPE:  abstract class
TOKEN: 0x200001E
SIZE:  0x28
EXTENDS: System.Xml.Linq.XObject
FIELDS:
  private           System.Xml.Linq.XNode           next  // 0x20
METHODS:
  RVA=0x041E1670  token=0x60000B7  System.Void .ctor()
  RVA=0x0AE4CB40  token=0x60000B8  System.Void Remove()
  RVA=0x0AE4CBB8  token=0x60000B9  System.String ToString()
  RVA=-1  // abstract  token=0x60000BA  System.Void WriteTo(System.Xml.XmlWriter writer)
  RVA=0x041E1670  token=0x60000BB  System.Void AppendText(System.Text.StringBuilder sb)
  RVA=-1  // abstract  token=0x60000BC  System.Xml.Linq.XNode CloneNode()
  RVA=0x0AE4C8A0  token=0x60000BD  System.String GetXmlString(System.Xml.Linq.SaveOptions o)
END_CLASS

CLASS: System.Xml.Linq.XObject
TYPE:  abstract class
TOKEN: 0x200001F
SIZE:  0x20
IMPLEMENTS: System.Xml.IXmlLineInfo
FIELDS:
  private           System.Xml.Linq.XContainer      parent  // 0x10
  private           System.Object                   annotations  // 0x18
PROPERTIES:
  BaseUri  get=0x0AE4D460
  NodeType  get=-1  // abstract
  Parent  get=0x0AE4D514
  System.Xml.IXmlLineInfo.LineNumber  get=0x0AE4D3D8
  System.Xml.IXmlLineInfo.LinePosition  get=0x0AE4D41C
  HasBaseUri  get=0x0AE4D4D4
METHODS:
  RVA=0x041E1670  token=0x60000BE  System.Void .ctor()
  RVA=0x0AE4CD90  token=0x60000C2  System.Void AddAnnotation(System.Object annotation)
  RVA=0x0AE4CF80  token=0x60000C3  System.Object AnnotationForSealedType(System.Type type)
  RVA=-1  // generic def  token=0x60000C4  T Annotation()
  RVA=0x0AE4D398  token=0x60000C5  System.Boolean System.Xml.IXmlLineInfo.HasLineInfo()
  RVA=0x0AE4D120  token=0x60000C9  System.Boolean NotifyChanged(System.Object sender, System.Xml.Linq.XObjectChangeEventArgs e)
  RVA=0x0AE4D1BC  token=0x60000CA  System.Boolean NotifyChanging(System.Object sender, System.Xml.Linq.XObjectChangeEventArgs e)
  RVA=0x0AE4D258  token=0x60000CB  System.Void SetBaseUri(System.String baseUri)
  RVA=0x0AE4D2D0  token=0x60000CC  System.Void SetLineInfo(System.Int32 lineNumber, System.Int32 linePosition)
  RVA=0x0AE4D340  token=0x60000CD  System.Boolean SkipNotify()
  RVA=0x0AE4D07C  token=0x60000CE  System.Xml.Linq.SaveOptions GetSaveOptionsFromAnnotations()
END_CLASS

CLASS: System.Xml.Linq.XObjectChangeAnnotation
TYPE:  class
TOKEN: 0x2000020
SIZE:  0x20
FIELDS:
  private           System.EventHandler<System.Xml.Linq.XObjectChangeEventArgs>changing  // 0x10
  private           System.EventHandler<System.Xml.Linq.XObjectChangeEventArgs>changed  // 0x18
METHODS:
END_CLASS

CLASS: System.Xml.Linq.XObjectChangeEventArgs
TYPE:  class
TOKEN: 0x2000021
SIZE:  0x18
EXTENDS: System.EventArgs
FIELDS:
  private           System.Xml.Linq.XObjectChange   _objectChange  // 0x10
  public    static readonly System.Xml.Linq.XObjectChangeEventArgsAdd  // static @ 0x0
  public    static readonly System.Xml.Linq.XObjectChangeEventArgsRemove  // static @ 0x8
  public    static readonly System.Xml.Linq.XObjectChangeEventArgsName  // static @ 0x10
  public    static readonly System.Xml.Linq.XObjectChangeEventArgsValue  // static @ 0x18
METHODS:
  RVA=0x0AE4CD40  token=0x60000CF  System.Void .ctor(System.Xml.Linq.XObjectChange objectChange)
  RVA=0x0AE4CBDC  token=0x60000D0  System.Void .cctor()
END_CLASS

CLASS: System.Xml.Linq.XProcessingInstruction
TYPE:  class
TOKEN: 0x2000022
SIZE:  0x38
EXTENDS: System.Xml.Linq.XNode
FIELDS:
  private           System.String                   target  // 0x28
  private           System.String                   data  // 0x30
PROPERTIES:
  Data  get=0x011F36E0
  NodeType  get=0x04D9BE00
  Target  get=0x04D86240
METHODS:
  RVA=0x0AE4D704  token=0x60000D1  System.Void .ctor(System.String target, System.String data)
  RVA=0x0AE4D7A8  token=0x60000D2  System.Void .ctor(System.Xml.Linq.XProcessingInstruction other)
  RVA=0x0AE4D688  token=0x60000D6  System.Void WriteTo(System.Xml.XmlWriter writer)
  RVA=0x0AE4D550  token=0x60000D7  System.Xml.Linq.XNode CloneNode()
  RVA=0x0AE4D5B0  token=0x60000D8  System.Void ValidateName(System.String name)
END_CLASS

CLASS: System.Xml.Linq.XStreamingElement
TYPE:  class
TOKEN: 0x2000023
SIZE:  0x20
FIELDS:
  private           System.Xml.Linq.XName           name  // 0x10
  private           System.Object                   content  // 0x18
METHODS:
END_CLASS

CLASS: System.Xml.Linq.XText
TYPE:  class
TOKEN: 0x2000024
SIZE:  0x30
EXTENDS: System.Xml.Linq.XNode
FIELDS:
  private           System.String                   text  // 0x28
PROPERTIES:
  NodeType  get=0x04D8C9E0
  Value  get=0x04D86240  set=0x0AE4DA64
METHODS:
  RVA=0x0AE4D978  token=0x60000D9  System.Void .ctor(System.String value)
  RVA=0x0AE4D9EC  token=0x60000DA  System.Void .ctor(System.Xml.Linq.XText other)
  RVA=0x0AE4D8BC  token=0x60000DE  System.Void WriteTo(System.Xml.XmlWriter writer)
  RVA=0x0AE4D834  token=0x60000DF  System.Void AppendText(System.Text.StringBuilder sb)
  RVA=0x0AE4D85C  token=0x60000E0  System.Xml.Linq.XNode CloneNode()
END_CLASS

CLASS: Unity.ThrowStub
TYPE:  sealed class
TOKEN: 0x2000026
SIZE:  0x98
EXTENDS: System.ObjectDisposedException
FIELDS:
METHODS:
  RVA=0x0AE478E8  token=0x60000E4  System.Void ThrowNotSupportedException()
END_CLASS

