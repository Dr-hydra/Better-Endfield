// ========================================================
// Dumped by @desirepro
// Assembly: ZSimpleJSON.dll
// Classes:  23
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000004  // size: 0x14
    public sealed struct Type
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static ZSimpleJSON.JSONNode.Enumerator.Type None;  // const
        public static ZSimpleJSON.JSONNode.Enumerator.Type Array;  // const
        public static ZSimpleJSON.JSONNode.Enumerator.Type Object;  // const

    }

    // TypeToken: 0x2000003  // size: 0x58
    public sealed struct Enumerator
    {
        // Fields
        private ZSimpleJSON.JSONNode.Enumerator.Type type;  // 0x10
        private System.Collections.Generic.Dictionary.Enumerator<System.String,ZSimpleJSON.JSONNode> m_Object;  // 0x18
        private System.Collections.Generic.List.Enumerator<ZSimpleJSON.JSONNode> m_Array;  // 0x40

        // Properties
        System.Boolean IsValid { get; /* RVA: 0x068B8E74 */ }
        System.Collections.Generic.KeyValuePair<System.String,ZSimpleJSON.JSONNode> Current { get; /* RVA: 0x036D56A0 */ }

        // Methods
        // RVA: 0x09DB87E4  token: 0x60000C1
        public System.Void .ctor(System.Collections.Generic.List.Enumerator<ZSimpleJSON.JSONNode> aArrayEnum) { }
        // RVA: 0x04B0C3F0  token: 0x60000C2
        public System.Void .ctor(System.Collections.Generic.Dictionary.Enumerator<System.String,ZSimpleJSON.JSONNode> aDictEnum) { }
        // RVA: 0x036D5670  token: 0x60000C4
        public System.Boolean MoveNext() { }

    }

    // TypeToken: 0x2000005  // size: 0x58
    public sealed struct ValueEnumerator
    {
        // Fields
        private ZSimpleJSON.JSONNode.Enumerator m_Enumerator;  // 0x10

        // Properties
        ZSimpleJSON.JSONNode Current { get; /* RVA: 0x0B8BEE84 */ }

        // Methods
        // RVA: 0x09DC2BA0  token: 0x60000C5
        public System.Void .ctor(System.Collections.Generic.List.Enumerator<ZSimpleJSON.JSONNode> aArrayEnum) { }
        // RVA: 0x09DC2ACC  token: 0x60000C6
        public System.Void .ctor(System.Collections.Generic.Dictionary.Enumerator<System.String,ZSimpleJSON.JSONNode> aDictEnum) { }
        // RVA: 0x09DC2B6C  token: 0x60000C7
        public System.Void .ctor(ZSimpleJSON.JSONNode.Enumerator aEnumerator) { }
        // RVA: 0x0B8BDC20  token: 0x60000C9
        public System.Boolean MoveNext() { }
        // RVA: 0x04DA2D60  token: 0x60000CA
        public ZSimpleJSON.JSONNode.ValueEnumerator GetEnumerator() { }

    }

    // TypeToken: 0x2000006  // size: 0x58
    public sealed struct KeyEnumerator
    {
        // Fields
        private ZSimpleJSON.JSONNode.Enumerator m_Enumerator;  // 0x10

        // Properties
        System.String Current { get; /* RVA: 0x0B8BDC28 */ }

        // Methods
        // RVA: 0x09DC2BA0  token: 0x60000CB
        public System.Void .ctor(System.Collections.Generic.List.Enumerator<ZSimpleJSON.JSONNode> aArrayEnum) { }
        // RVA: 0x09DC2ACC  token: 0x60000CC
        public System.Void .ctor(System.Collections.Generic.Dictionary.Enumerator<System.String,ZSimpleJSON.JSONNode> aDictEnum) { }
        // RVA: 0x09DC2B6C  token: 0x60000CD
        public System.Void .ctor(ZSimpleJSON.JSONNode.Enumerator aEnumerator) { }
        // RVA: 0x0B8BDC20  token: 0x60000CF
        public System.Boolean MoveNext() { }
        // RVA: 0x04DA2D60  token: 0x60000D0
        public ZSimpleJSON.JSONNode.KeyEnumerator GetEnumerator() { }

    }

    // TypeToken: 0x2000007  // size: 0x60
    public class LinqEnumerator : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private ZSimpleJSON.JSONNode m_Node;  // 0x10
        private ZSimpleJSON.JSONNode.Enumerator m_Enumerator;  // 0x18

        // Properties
        System.Collections.Generic.KeyValuePair<System.String,ZSimpleJSON.JSONNode> Current { get; /* RVA: 0x0B8BDEEC */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0B8BDDE0 */ }

        // Methods
        // RVA: 0x0B8BDE38  token: 0x60000D1
        private System.Void .ctor(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x0B8BDCCC  token: 0x60000D4
        public virtual System.Boolean MoveNext() { }
        // RVA: 0x09DC2C78  token: 0x60000D5
        public virtual System.Void Dispose() { }
        // RVA: 0x0B8BDC68  token: 0x60000D6
        public virtual System.Collections.Generic.IEnumerator<System.Collections.Generic.KeyValuePair<System.String,ZSimpleJSON.JSONNode>> GetEnumerator() { }
        // RVA: 0x0B8BDCD8  token: 0x60000D7
        public virtual System.Void Reset() { }
        // RVA: 0x0B8BDD7C  token: 0x60000D8
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000008  // size: 0x28
    public sealed class <get_Children>d__181 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private ZSimpleJSON.JSONNode <>2__current;  // 0x18
        private System.Int32 <>l__initialThreadId;  // 0x20

        // Properties
        ZSimpleJSON.JSONNode System.Collections.Generic.IEnumerator<ZSimpleJSON.JSONNode>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x03A11010  token: 0x60000D9
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x041E1670  token: 0x60000DA
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x04D83CA0  token: 0x60000DB
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0B8BDFFC  token: 0x60000DD
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: 0x0B8BDF7C  token: 0x60000DF
        private virtual System.Collections.Generic.IEnumerator<ZSimpleJSON.JSONNode> System.Collections.Generic.IEnumerable<ZSimpleJSON.JSONNode>.GetEnumerator() { }
        // RVA: 0x0B8BDFF4  token: 0x60000E0
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000009  // size: 0x40
    public sealed class <get_DeepChildren>d__183 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private ZSimpleJSON.JSONNode <>2__current;  // 0x18
        private System.Int32 <>l__initialThreadId;  // 0x20
        public ZSimpleJSON.JSONNode <>4__this;  // 0x28
        private System.Collections.Generic.IEnumerator<ZSimpleJSON.JSONNode> <>7__wrap1;  // 0x30
        private System.Collections.Generic.IEnumerator<ZSimpleJSON.JSONNode> <>7__wrap2;  // 0x38

        // Properties
        ZSimpleJSON.JSONNode System.Collections.Generic.IEnumerator<ZSimpleJSON.JSONNode>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x03A11010  token: 0x60000E1
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x0B8BED04  token: 0x60000E2
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0B8BE82C  token: 0x60000E3
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0B8BEDF0  token: 0x60000E4
        private System.Void <>m__Finally1() { }
        // RVA: 0x0B8BEE38  token: 0x60000E5
        private System.Void <>m__Finally2() { }
        // RVA: 0x0B8BECB8  token: 0x60000E7
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: 0x0B8BEC24  token: 0x60000E9
        private virtual System.Collections.Generic.IEnumerator<ZSimpleJSON.JSONNode> System.Collections.Generic.IEnumerable<ZSimpleJSON.JSONNode>.GetEnumerator() { }
        // RVA: 0x0B8BECB0  token: 0x60000EA
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x200000E  // size: 0x48
    public sealed class <get_Children>d__24 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private ZSimpleJSON.JSONNode <>2__current;  // 0x18
        private System.Int32 <>l__initialThreadId;  // 0x20
        public ZSimpleJSON.JSONArray <>4__this;  // 0x28
        private System.Collections.Generic.List.Enumerator<ZSimpleJSON.JSONNode> <>7__wrap1;  // 0x30

        // Properties
        ZSimpleJSON.JSONNode System.Collections.Generic.IEnumerator<ZSimpleJSON.JSONNode>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x03A11010  token: 0x60000FD
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x0B8BE370  token: 0x60000FE
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0B8BE048  token: 0x60000FF
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0B8BE3E8  token: 0x6000100
        private System.Void <>m__Finally1() { }
        // RVA: 0x0B8BE324  token: 0x6000102
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: 0x0B8BE290  token: 0x6000104
        private virtual System.Collections.Generic.IEnumerator<ZSimpleJSON.JSONNode> System.Collections.Generic.IEnumerable<ZSimpleJSON.JSONNode>.GetEnumerator() { }
        // RVA: 0x0B8BE31C  token: 0x6000105
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

    // TypeToken: 0x2000010  // size: 0x18
    public sealed class <>c__DisplayClass21_0
    {
        // Fields
        public ZSimpleJSON.JSONNode aNode;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600011B
        public System.Void .ctor() { }
        // RVA: 0x0B8BDF18  token: 0x600011C
        private System.Boolean <Remove>b__0(System.Collections.Generic.KeyValuePair<System.String,ZSimpleJSON.JSONNode> k) { }

    }

    // TypeToken: 0x2000011  // size: 0x58
    public sealed class <get_Children>d__27 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable, System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x10
        private ZSimpleJSON.JSONNode <>2__current;  // 0x18
        private System.Int32 <>l__initialThreadId;  // 0x20
        public ZSimpleJSON.JSONObject <>4__this;  // 0x28
        private System.Collections.Generic.Dictionary.Enumerator<System.String,ZSimpleJSON.JSONNode> <>7__wrap1;  // 0x30

        // Properties
        ZSimpleJSON.JSONNode System.Collections.Generic.IEnumerator<ZSimpleJSON.JSONNode>.Current { get; /* RVA: 0x0385B100 */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x03A11010  token: 0x600011D
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: 0x0B8BE778  token: 0x600011E
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: 0x0B8BE424  token: 0x600011F
        private virtual System.Boolean MoveNext() { }
        // RVA: 0x0B8BE7F0  token: 0x6000120
        private System.Void <>m__Finally1() { }
        // RVA: 0x0B8BE72C  token: 0x6000122
        private virtual System.Void System.Collections.IEnumerator.Reset() { }
        // RVA: 0x0B8BE698  token: 0x6000124
        private virtual System.Collections.Generic.IEnumerator<ZSimpleJSON.JSONNode> System.Collections.Generic.IEnumerable<ZSimpleJSON.JSONNode>.GetEnumerator() { }
        // RVA: 0x0B8BE724  token: 0x6000125
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }

    }

namespace ZSimpleJSON
{

    // TypeToken: 0x2000002  // size: 0x10
    public abstract class JSONNode
    {
        // Fields
        public static System.Byte Color32DefaultAlpha;  // static @ 0x0
        public static System.Single ColorDefaultAlpha;  // static @ 0x4
        public static ZSimpleJSON.JSONContainerType VectorContainerType;  // static @ 0x8
        public static ZSimpleJSON.JSONContainerType QuaternionContainerType;  // static @ 0xc
        public static ZSimpleJSON.JSONContainerType RectContainerType;  // static @ 0x10
        public static ZSimpleJSON.JSONContainerType ColorContainerType;  // static @ 0x14
        public static System.Boolean forceASCII;  // static @ 0x18
        public static System.Boolean longAsString;  // static @ 0x19
        public static System.Boolean allowLineComments;  // static @ 0x1a
        private static System.Text.StringBuilder m_EscapeBuilder;  // static @ 0xffffffff

        // Properties
        System.Decimal AsDecimal { get; /* RVA: 0x0B8BA794 */ set; /* RVA: 0x0B8BCAB0 */ }
        System.Char AsChar { get; /* RVA: 0x0B8BA658 */ set; /* RVA: 0x0B8BC990 */ }
        System.UInt32 AsUInt { get; /* RVA: 0x0B8BAC3C */ set; /* RVA: 0x0B8BCE20 */ }
        System.Byte AsByte { get; /* RVA: 0x053912C0 */ set; /* RVA: 0x0B8BC97C */ }
        System.SByte AsSByte { get; /* RVA: 0x053912C0 */ set; /* RVA: 0x0B8BCC2C */ }
        System.Int16 AsShort { get; /* RVA: 0x053912C0 */ set; /* RVA: 0x0B8BCC40 */ }
        System.UInt16 AsUShort { get; /* RVA: 0x053912C0 */ set; /* RVA: 0x0B8BCEA0 */ }
        System.DateTime AsDateTime { get; /* RVA: 0x0B8BA6E0 */ set; /* RVA: 0x0B8BCA28 */ }
        System.TimeSpan AsTimeSpan { get; /* RVA: 0x0B8BAB8C */ set; /* RVA: 0x0B8BCDC8 */ }
        System.Guid AsGuid { get; /* RVA: 0x0B8BA8B0 */ set; /* RVA: 0x0B8BCB84 */ }
        System.Byte[] AsByteArray { get; /* RVA: 0x0B8BA484 */ set; /* RVA: 0x0B8BC810 */ }
        System.Collections.Generic.List<System.Byte> AsByteList { get; /* RVA: 0x0B8BA558 */ set; /* RVA: 0x0B8BC8B8 */ }
        System.UInt64[] AsUlongArray { get; /* RVA: 0x0B8BACD4 */ set; /* RVA: 0x0B8BCEB4 */ }
        System.String[] AsStringArray { get; /* RVA: 0x0B8BA9B4 */ set; /* RVA: 0x0B8BCC54 */ }
        System.Collections.Generic.List<System.String> AsStringList { get; /* RVA: 0x0B8BAA88 */ set; /* RVA: 0x0B8BCD00 */ }
        ZSimpleJSON.JSONNodeType Tag { get; /* RVA: -1  // abstract */ }
        ZSimpleJSON.JSONNode Item { get; /* RVA: 0x011EC580 */ set; /* RVA: 0x041E1670 */ }
        ZSimpleJSON.JSONNode Item { get; /* RVA: 0x011EC580 */ set; /* RVA: 0x041E1670 */ }
        System.String Value { get; /* RVA: 0x0B8BAF84 */ set; /* RVA: 0x041E1670 */ }
        System.Int32 Count { get; /* RVA: 0x011EC580 */ }
        System.Boolean IsNumber { get; /* RVA: 0x012081B0 */ }
        System.Boolean IsString { get; /* RVA: 0x012081B0 */ }
        System.Boolean IsBoolean { get; /* RVA: 0x012081B0 */ }
        System.Boolean IsNull { get; /* RVA: 0x012081B0 */ }
        System.Boolean IsArray { get; /* RVA: 0x012081B0 */ }
        System.Boolean IsObject { get; /* RVA: 0x012081B0 */ }
        System.Boolean Inline { get; /* RVA: 0x012081B0 */ set; /* RVA: 0x041E1670 */ }
        System.Collections.Generic.IEnumerable<ZSimpleJSON.JSONNode> Children { get; /* RVA: 0x0B8BADAC */ }
        System.Collections.Generic.IEnumerable<ZSimpleJSON.JSONNode> DeepChildren { get; /* RVA: 0x0B8BAE00 */ }
        System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<System.String,ZSimpleJSON.JSONNode>> Linq { get; /* RVA: 0x0B8BAF24 */ }
        ZSimpleJSON.JSONNode.KeyEnumerator Keys { get; /* RVA: 0x0B8BAE70 */ }
        ZSimpleJSON.JSONNode.ValueEnumerator Values { get; /* RVA: 0x0B8BAE70 */ }
        System.Double AsDouble { get; /* RVA: 0x0B8BA808 */ set; /* RVA: 0x0B8BCB0C */ }
        System.Int32 AsInt { get; /* RVA: 0x0B8BA8E0 */ set; /* RVA: 0x0B8BCBB0 */ }
        System.Single AsFloat { get; /* RVA: 0x0B8BA894 */ set; /* RVA: 0x0B8BCB70 */ }
        System.Boolean AsBool { get; /* RVA: 0x0B8BA3E8 */ set; /* RVA: 0x0B8BC7A4 */ }
        System.Int64 AsLong { get; /* RVA: 0x0B8BA8FC */ set; /* RVA: 0x0B8BCBC8 */ }
        System.UInt64 AsULong { get; /* RVA: 0x0B8BAC58 */ set; /* RVA: 0x0B8BCE3C */ }
        ZSimpleJSON.JSONArray AsArray { get; /* RVA: 0x0B8BA3AC */ }
        ZSimpleJSON.JSONObject AsObject { get; /* RVA: 0x0B8BA978 */ }
        System.Text.StringBuilder EscapeBuilder { get; /* RVA: 0x0360CBC0 */ }

        // Methods
        // RVA: 0x0B8BB710  token: 0x6000003
        public static ZSimpleJSON.JSONNode op_Implicit(System.Decimal aDecimal) { }
        // RVA: 0x0B8BBCD4  token: 0x6000004
        public static System.Decimal op_Implicit(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x0B8BC41C  token: 0x6000007
        public static ZSimpleJSON.JSONNode op_Implicit(System.Char aChar) { }
        // RVA: 0x0B8BC3E4  token: 0x6000008
        public static System.Char op_Implicit(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x049DD510  token: 0x600000B
        public static ZSimpleJSON.JSONNode op_Implicit(System.UInt32 aUInt) { }
        // RVA: 0x0B8BC344  token: 0x600000C
        public static System.UInt32 op_Implicit(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x0B8BBED4  token: 0x600000F
        public static ZSimpleJSON.JSONNode op_Implicit(System.Byte aByte) { }
        // RVA: 0x0B8BB430  token: 0x6000010
        public static System.Byte op_Implicit(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x0B8BC674  token: 0x6000013
        public static ZSimpleJSON.JSONNode op_Implicit(System.SByte aSByte) { }
        // RVA: 0x0B8BB430  token: 0x6000014
        public static System.SByte op_Implicit(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x0B8BB3CC  token: 0x6000017
        public static ZSimpleJSON.JSONNode op_Implicit(System.Int16 aShort) { }
        // RVA: 0x0B8BB430  token: 0x6000018
        public static System.Int16 op_Implicit(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x0B8BC228  token: 0x600001B
        public static ZSimpleJSON.JSONNode op_Implicit(System.UInt16 aUShort) { }
        // RVA: 0x0B8BB430  token: 0x600001C
        public static System.UInt16 op_Implicit(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x0B8BB1DC  token: 0x600001F
        public static ZSimpleJSON.JSONNode op_Implicit(System.DateTime aDateTime) { }
        // RVA: 0x0B8BBD6C  token: 0x6000020
        public static System.DateTime op_Implicit(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x0B8BB344  token: 0x6000023
        public static ZSimpleJSON.JSONNode op_Implicit(System.TimeSpan aTimeSpan) { }
        // RVA: 0x0B8BBC9C  token: 0x6000024
        public static System.TimeSpan op_Implicit(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x0B8BB170  token: 0x6000027
        public static ZSimpleJSON.JSONNode op_Implicit(System.Guid aGuid) { }
        // RVA: 0x0B8BBEA4  token: 0x6000028
        public static System.Guid op_Implicit(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x0B8BB6A4  token: 0x600002B
        public static ZSimpleJSON.JSONNode op_Implicit(System.Byte[] aByteArray) { }
        // RVA: 0x0B8BBE20  token: 0x600002C
        public static System.Byte[] op_Implicit(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x0B8BC1BC  token: 0x600002F
        public static ZSimpleJSON.JSONNode op_Implicit(System.Collections.Generic.List<System.Byte> aByteList) { }
        // RVA: 0x0B8BB44C  token: 0x6000030
        public static System.Collections.Generic.List<System.Byte> op_Implicit(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x0B8BC51C  token: 0x6000033
        public static ZSimpleJSON.JSONNode op_Implicit(System.UInt64[] aStringArray) { }
        // RVA: 0x0B8BBAE0  token: 0x6000034
        public static System.UInt64[] op_Implicit(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x0B8BB528  token: 0x6000037
        public static ZSimpleJSON.JSONNode op_Implicit(System.String[] aStringArray) { }
        // RVA: 0x0B8BBCB8  token: 0x6000038
        public static System.String[] op_Implicit(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x0B8BBFA0  token: 0x600003B
        public static ZSimpleJSON.JSONNode op_Implicit(System.Collections.Generic.List<System.String> aStringList) { }
        // RVA: 0x0B8BC0A0  token: 0x600003C
        public static System.Collections.Generic.List<System.String> op_Implicit(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x04950E40  token: 0x600003D
        public static ZSimpleJSON.JSONNode op_Implicit(System.Nullable<System.Int32> aValue) { }
        // RVA: 0x0B8BBD88  token: 0x600003E
        public static System.Nullable<System.Int32> op_Implicit(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x0B8BBA24  token: 0x600003F
        public static ZSimpleJSON.JSONNode op_Implicit(System.Nullable<System.Single> aValue) { }
        // RVA: 0x0B8BC124  token: 0x6000040
        public static System.Nullable<System.Single> op_Implicit(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x0B8BB974  token: 0x6000041
        public static ZSimpleJSON.JSONNode op_Implicit(System.Nullable<System.Double> aValue) { }
        // RVA: 0x0B8BB840  token: 0x6000042
        public static System.Nullable<System.Double> op_Implicit(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x0B8BB088  token: 0x6000043
        public static ZSimpleJSON.JSONNode op_Implicit(System.Nullable<System.Boolean> aValue) { }
        // RVA: 0x0B8BC28C  token: 0x6000044
        public static System.Nullable<System.Boolean> op_Implicit(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x0B8BBB64  token: 0x6000045
        public static ZSimpleJSON.JSONNode op_Implicit(System.Nullable<System.Int64> aValue) { }
        // RVA: 0x0B8BB794  token: 0x6000046
        public static System.Nullable<System.Int64> op_Implicit(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x0B8BB28C  token: 0x6000047
        public static ZSimpleJSON.JSONNode op_Implicit(System.Nullable<System.Int16> aValue) { }
        // RVA: 0x0B8BC00C  token: 0x6000048
        public static System.Nullable<System.Int16> op_Implicit(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x0B8B8250  token: 0x6000049
        private static ZSimpleJSON.JSONNode GetContainer(ZSimpleJSON.JSONContainerType aType) { }
        // RVA: 0x0B8BB594  token: 0x600004A
        public static ZSimpleJSON.JSONNode op_Implicit(UnityEngine.Vector2 aVec) { }
        // RVA: 0x0B8BC6D8  token: 0x600004B
        public static ZSimpleJSON.JSONNode op_Implicit(UnityEngine.Vector3 aVec) { }
        // RVA: 0x0B8BC588  token: 0x600004C
        public static ZSimpleJSON.JSONNode op_Implicit(UnityEngine.Vector4 aVec) { }
        // RVA: 0x0B8BBC18  token: 0x600004D
        public static ZSimpleJSON.JSONNode op_Implicit(UnityEngine.Color aCol) { }
        // RVA: 0x0B8BAFE0  token: 0x600004E
        public static ZSimpleJSON.JSONNode op_Implicit(UnityEngine.Color32 aCol) { }
        // RVA: 0x0B8BB8F0  token: 0x600004F
        public static ZSimpleJSON.JSONNode op_Implicit(UnityEngine.Quaternion aRot) { }
        // RVA: 0x0B8BC360  token: 0x6000050
        public static ZSimpleJSON.JSONNode op_Implicit(UnityEngine.Rect aRect) { }
        // RVA: 0x0B8BC4A4  token: 0x6000051
        public static ZSimpleJSON.JSONNode op_Implicit(UnityEngine.RectOffset aRect) { }
        // RVA: 0x0B8BC400  token: 0x6000052
        public static UnityEngine.Vector2 op_Implicit(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x0B8BB4F0  token: 0x6000053
        public static UnityEngine.Vector3 op_Implicit(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x0B8BBF38  token: 0x6000054
        public static UnityEngine.Vector4 op_Implicit(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x0B8BAFB0  token: 0x6000055
        public static UnityEngine.Color op_Implicit(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x0B8BB4D0  token: 0x6000056
        public static UnityEngine.Color32 op_Implicit(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x0B8BB058  token: 0x6000057
        public static UnityEngine.Quaternion op_Implicit(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x0B8BB140  token: 0x6000058
        public static UnityEngine.Rect op_Implicit(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x0B8BC328  token: 0x6000059
        public static UnityEngine.RectOffset op_Implicit(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x0B8B8F6C  token: 0x600005A
        public UnityEngine.Vector2 ReadVector2(UnityEngine.Vector2 aDefault) { }
        // RVA: 0x0B8B8ECC  token: 0x600005B
        public UnityEngine.Vector2 ReadVector2(System.String aXName, System.String aYName) { }
        // RVA: 0x0B8B8EB4  token: 0x600005C
        public UnityEngine.Vector2 ReadVector2() { }
        // RVA: 0x0B8B9FA8  token: 0x600005D
        public ZSimpleJSON.JSONNode WriteVector2(UnityEngine.Vector2 aVec, System.String aXName, System.String aYName) { }
        // RVA: 0x0B8B918C  token: 0x600005E
        public UnityEngine.Vector3 ReadVector3(UnityEngine.Vector3 aDefault) { }
        // RVA: 0x0B8B90A4  token: 0x600005F
        public UnityEngine.Vector3 ReadVector3(System.String aXName, System.String aYName, System.String aZName) { }
        // RVA: 0x0B8B9054  token: 0x6000060
        public UnityEngine.Vector3 ReadVector3() { }
        // RVA: 0x0B8BA0A0  token: 0x6000061
        public ZSimpleJSON.JSONNode WriteVector3(UnityEngine.Vector3 aVec, System.String aXName, System.String aYName, System.String aZName) { }
        // RVA: 0x0B8B9308  token: 0x6000062
        public UnityEngine.Vector4 ReadVector4(UnityEngine.Vector4 aDefault) { }
        // RVA: 0x0B8B94E4  token: 0x6000063
        public UnityEngine.Vector4 ReadVector4() { }
        // RVA: 0x0B8BA1E0  token: 0x6000064
        public ZSimpleJSON.JSONNode WriteVector4(UnityEngine.Vector4 aVec) { }
        // RVA: 0x0B8B84EC  token: 0x6000065
        public UnityEngine.Color ReadColor(UnityEngine.Color aDefault) { }
        // RVA: 0x0B8B8720  token: 0x6000066
        public UnityEngine.Color ReadColor() { }
        // RVA: 0x0B8B9778  token: 0x6000067
        public ZSimpleJSON.JSONNode WriteColor(UnityEngine.Color aCol) { }
        // RVA: 0x0B8B82E0  token: 0x6000068
        public UnityEngine.Color32 ReadColor32(UnityEngine.Color32 aDefault) { }
        // RVA: 0x0B8B82D4  token: 0x6000069
        public UnityEngine.Color32 ReadColor32() { }
        // RVA: 0x0B8B95B0  token: 0x600006A
        public ZSimpleJSON.JSONNode WriteColor32(UnityEngine.Color32 aCol) { }
        // RVA: 0x0B8B8810  token: 0x600006B
        public UnityEngine.Quaternion ReadQuaternion(UnityEngine.Quaternion aDefault) { }
        // RVA: 0x0B8B89EC  token: 0x600006C
        public UnityEngine.Quaternion ReadQuaternion() { }
        // RVA: 0x0B8B99D8  token: 0x600006D
        public ZSimpleJSON.JSONNode WriteQuaternion(UnityEngine.Quaternion aRot) { }
        // RVA: 0x0B8B8CA4  token: 0x600006E
        public UnityEngine.Rect ReadRect(UnityEngine.Rect aDefault) { }
        // RVA: 0x0B8B8E80  token: 0x600006F
        public UnityEngine.Rect ReadRect() { }
        // RVA: 0x0B8B9DDC  token: 0x6000070
        public ZSimpleJSON.JSONNode WriteRect(UnityEngine.Rect aRect) { }
        // RVA: 0x0B8B8A90  token: 0x6000071
        public UnityEngine.RectOffset ReadRectOffset(UnityEngine.RectOffset aDefault) { }
        // RVA: 0x0B8B8A28  token: 0x6000072
        public UnityEngine.RectOffset ReadRectOffset() { }
        // RVA: 0x0B8B9BA4  token: 0x6000073
        public ZSimpleJSON.JSONNode WriteRectOffset(UnityEngine.RectOffset aRect) { }
        // RVA: 0x0B8B875C  token: 0x6000074
        public UnityEngine.Matrix4x4 ReadMatrix() { }
        // RVA: 0x0B8B9944  token: 0x6000075
        public ZSimpleJSON.JSONNode WriteMatrix(UnityEngine.Matrix4x4 aMatrix) { }
        // RVA: 0x041E1670  token: 0x6000086
        public virtual System.Void Add(System.String aKey, ZSimpleJSON.JSONNode aItem) { }
        // RVA: 0x03B4AD90  token: 0x6000087
        public virtual System.Void Add(ZSimpleJSON.JSONNode aItem) { }
        // RVA: 0x011EC580  token: 0x6000088
        public virtual ZSimpleJSON.JSONNode Remove(System.String aKey) { }
        // RVA: 0x011EC580  token: 0x6000089
        public virtual ZSimpleJSON.JSONNode Remove(System.Int32 aIndex) { }
        // RVA: 0x04D88C30  token: 0x600008A
        public virtual ZSimpleJSON.JSONNode Remove(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x041E1670  token: 0x600008B
        public virtual System.Void Clear() { }
        // RVA: 0x011EC580  token: 0x600008C
        public virtual ZSimpleJSON.JSONNode Clone() { }
        // RVA: 0x012081B0  token: 0x600008F
        public virtual System.Boolean HasKey(System.String aKey) { }
        // RVA: 0x04D8C7E0  token: 0x6000090
        public virtual ZSimpleJSON.JSONNode GetValueOrDefault(System.String aKey, ZSimpleJSON.JSONNode aDefault) { }
        // RVA: 0x04610F80  token: 0x6000091
        public virtual System.String ToString() { }
        // RVA: 0x0B8B9520  token: 0x6000092
        public virtual System.String ToString(System.Int32 aIndent) { }
        // RVA: 0x03A756A0  token: 0x6000093
        public virtual System.Void ToStringBuild(System.Text.StringBuilder& sb) { }
        // RVA: -1  // abstract  token: 0x6000094
        private virtual System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, ZSimpleJSON.JSONTextMode aMode) { }
        // RVA: -1  // abstract  token: 0x6000095
        public virtual ZSimpleJSON.JSONNode.Enumerator GetEnumerator() { }
        // RVA: 0x03B4AEF0  token: 0x60000A7
        public static ZSimpleJSON.JSONNode op_Implicit(System.String s) { }
        // RVA: 0x0B8BB63C  token: 0x60000A8
        public static System.String op_Implicit(ZSimpleJSON.JSONNode d) { }
        // RVA: 0x03B4AA70  token: 0x60000A9
        public static ZSimpleJSON.JSONNode op_Implicit(System.Double n) { }
        // RVA: 0x0B8BBAFC  token: 0x60000AA
        public static System.Double op_Implicit(ZSimpleJSON.JSONNode d) { }
        // RVA: 0x03B4AC60  token: 0x60000AB
        public static ZSimpleJSON.JSONNode op_Implicit(System.Single n) { }
        // RVA: 0x0B8BBD04  token: 0x60000AC
        public static System.Single op_Implicit(ZSimpleJSON.JSONNode d) { }
        // RVA: 0x03B4ACB0  token: 0x60000AD
        public static ZSimpleJSON.JSONNode op_Implicit(System.Int32 n) { }
        // RVA: 0x0B8BBE3C  token: 0x60000AE
        public static System.Int32 op_Implicit(ZSimpleJSON.JSONNode d) { }
        // RVA: 0x03B4ABE0  token: 0x60000AF
        public static ZSimpleJSON.JSONNode op_Implicit(System.Int64 n) { }
        // RVA: 0x0B8BC0BC  token: 0x60000B0
        public static System.Int64 op_Implicit(ZSimpleJSON.JSONNode d) { }
        // RVA: 0x03A72870  token: 0x60000B1
        public static ZSimpleJSON.JSONNode op_Implicit(System.UInt64 n) { }
        // RVA: 0x0B8BB468  token: 0x60000B2
        public static System.UInt64 op_Implicit(ZSimpleJSON.JSONNode d) { }
        // RVA: 0x03A74420  token: 0x60000B3
        public static ZSimpleJSON.JSONNode op_Implicit(System.Boolean b) { }
        // RVA: 0x0B8BC60C  token: 0x60000B4
        public static System.Boolean op_Implicit(ZSimpleJSON.JSONNode d) { }
        // RVA: 0x0B8BBF68  token: 0x60000B5
        public static ZSimpleJSON.JSONNode op_Implicit(System.Collections.Generic.KeyValuePair<System.String,ZSimpleJSON.JSONNode> aKeyValue) { }
        // RVA: 0x03D53B10  token: 0x60000B6
        public static System.Boolean op_Equality(ZSimpleJSON.JSONNode a, System.Object b) { }
        // RVA: 0x03D539A0  token: 0x60000B7
        public static System.Boolean op_Inequality(ZSimpleJSON.JSONNode a, System.Object b) { }
        // RVA: 0x03D53990  token: 0x60000B8
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x04D87100  token: 0x60000B9
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0360C7B0  token: 0x60000BB
        private static System.String Escape(System.String aText) { }
        // RVA: 0x03B4A400  token: 0x60000BC
        private static ZSimpleJSON.JSONNode ParseElement(System.String token, System.Boolean quoted) { }
        // RVA: 0x03B49E20  token: 0x60000BD
        public static ZSimpleJSON.JSONNode Parse(System.String aJSON) { }
        // RVA: 0x041E1670  token: 0x60000BE
        protected System.Void .ctor() { }
        // RVA: 0x04CDC2C0  token: 0x60000BF
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200000A  // size: 0x14
    public sealed struct JSONContainerType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static ZSimpleJSON.JSONContainerType Array;  // const
        public static ZSimpleJSON.JSONContainerType Object;  // const

    }

    // TypeToken: 0x200000B  // size: 0x14
    public sealed struct JSONNodeType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static ZSimpleJSON.JSONNodeType Array;  // const
        public static ZSimpleJSON.JSONNodeType Object;  // const
        public static ZSimpleJSON.JSONNodeType String;  // const
        public static ZSimpleJSON.JSONNodeType Number;  // const
        public static ZSimpleJSON.JSONNodeType NullValue;  // const
        public static ZSimpleJSON.JSONNodeType Boolean;  // const
        public static ZSimpleJSON.JSONNodeType None;  // const
        public static ZSimpleJSON.JSONNodeType Custom;  // const

    }

    // TypeToken: 0x200000C  // size: 0x14
    public sealed struct JSONTextMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static ZSimpleJSON.JSONTextMode Compact;  // const
        public static ZSimpleJSON.JSONTextMode Indent;  // const

    }

    // TypeToken: 0x200000D  // size: 0x20
    public class JSONArray : ZSimpleJSON.JSONNode
    {
        // Fields
        private System.Collections.Generic.List<ZSimpleJSON.JSONNode> m_List;  // 0x10
        private System.Boolean inline;  // 0x18

        // Properties
        System.Boolean Inline { get; /* RVA: 0x04D86340 */ set; /* RVA: 0x04D86350 */ }
        ZSimpleJSON.JSONNodeType Tag { get; /* RVA: 0x04D85B80 */ }
        System.Boolean IsArray { get; /* RVA: 0x02FFF600 */ }
        ZSimpleJSON.JSONNode Item { get; /* RVA: 0x0B8B7168 */ set; /* RVA: 0x03D53860 */ }
        ZSimpleJSON.JSONNode Item { get; /* RVA: 0x0B8B7168 */ set; /* RVA: 0x03D53860 */ }
        System.Int32 Count { get; /* RVA: 0x0B8B70C8 */ }
        System.Collections.Generic.IEnumerable<ZSimpleJSON.JSONNode> Children { get; /* RVA: 0x0B8B7058 */ }

        // Methods
        // RVA: 0x0B8B6ECC  token: 0x60000EF
        public virtual ZSimpleJSON.JSONNode.Enumerator GetEnumerator() { }
        // RVA: 0x03D53730  token: 0x60000F5
        public virtual System.Void Add(System.String aKey, ZSimpleJSON.JSONNode aItem) { }
        // RVA: 0x0B8B6F5C  token: 0x60000F6
        public virtual ZSimpleJSON.JSONNode Remove(System.Int32 aIndex) { }
        // RVA: 0x0B8B7000  token: 0x60000F7
        public virtual ZSimpleJSON.JSONNode Remove(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x0B8B6C54  token: 0x60000F8
        public virtual System.Void Clear() { }
        // RVA: 0x0B8B6C9C  token: 0x60000F9
        public virtual ZSimpleJSON.JSONNode Clone() { }
        // RVA: 0x0360E340  token: 0x60000FB
        private virtual System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, ZSimpleJSON.JSONTextMode aMode) { }
        // RVA: 0x0355DBA0  token: 0x60000FC
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000F  // size: 0x20
    public class JSONObject : ZSimpleJSON.JSONNode
    {
        // Fields
        private System.Collections.Generic.Dictionary<System.String,ZSimpleJSON.JSONNode> m_Dict;  // 0x10
        private System.Boolean inline;  // 0x18

        // Properties
        System.Boolean Inline { get; /* RVA: 0x04D86340 */ set; /* RVA: 0x04D86350 */ }
        ZSimpleJSON.JSONNodeType Tag { get; /* RVA: 0x02BBE540 */ }
        System.Boolean IsObject { get; /* RVA: 0x02FFF600 */ }
        ZSimpleJSON.JSONNode Item { get; /* RVA: 0x0B8BD9B8 */ set; /* RVA: 0x03D539F0 */ }
        ZSimpleJSON.JSONNode Item { get; /* RVA: 0x0B8BD9B8 */ set; /* RVA: 0x03D539F0 */ }
        System.Int32 Count { get; /* RVA: 0x0B8BD8EC */ }
        System.Collections.Generic.IEnumerable<ZSimpleJSON.JSONNode> Children { get; /* RVA: 0x0B8BD87C */ }

        // Methods
        // RVA: 0x04B0C340  token: 0x600010A
        public virtual ZSimpleJSON.JSONNode.Enumerator GetEnumerator() { }
        // RVA: 0x03D537A0  token: 0x6000110
        public virtual System.Void Add(System.String aKey, ZSimpleJSON.JSONNode aItem) { }
        // RVA: 0x0B8BD558  token: 0x6000111
        public virtual ZSimpleJSON.JSONNode Remove(System.String aKey) { }
        // RVA: 0x0B8BD610  token: 0x6000112
        public virtual ZSimpleJSON.JSONNode Remove(System.Int32 aIndex) { }
        // RVA: 0x0B8BD6D0  token: 0x6000113
        public virtual ZSimpleJSON.JSONNode Remove(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x04553E10  token: 0x6000114
        public virtual System.Void Clear() { }
        // RVA: 0x0B8BD2BC  token: 0x6000115
        public virtual ZSimpleJSON.JSONNode Clone() { }
        // RVA: 0x0B8BD504  token: 0x6000116
        public virtual System.Boolean HasKey(System.String aKey) { }
        // RVA: 0x0B8BD488  token: 0x6000117
        public virtual ZSimpleJSON.JSONNode GetValueOrDefault(System.String aKey, ZSimpleJSON.JSONNode aDefault) { }
        // RVA: 0x0360E070  token: 0x6000119
        private virtual System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, ZSimpleJSON.JSONTextMode aMode) { }
        // RVA: 0x03B490B0  token: 0x600011A
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000012  // size: 0x18
    public class JSONString : ZSimpleJSON.JSONNode
    {
        // Fields
        private System.String m_Data;  // 0x10

        // Properties
        ZSimpleJSON.JSONNodeType Tag { get; /* RVA: 0x04D8C9E0 */ }
        System.Boolean IsString { get; /* RVA: 0x02FFF600 */ }
        System.String Value { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }

        // Methods
        // RVA: 0x04DA2D20  token: 0x6000128
        public virtual ZSimpleJSON.JSONNode.Enumerator GetEnumerator() { }
        // RVA: 0x03B4AF60  token: 0x600012B
        public System.Void .ctor(System.String aData) { }
        // RVA: 0x0B8BDBBC  token: 0x600012C
        public virtual ZSimpleJSON.JSONNode Clone() { }
        // RVA: 0x0360D970  token: 0x600012D
        private virtual System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, ZSimpleJSON.JSONTextMode aMode) { }
        // RVA: 0x03D53900  token: 0x600012E
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x08479504  token: 0x600012F
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B8BDB7C  token: 0x6000130
        public virtual System.Void Clear() { }

    }

    // TypeToken: 0x2000013  // size: 0x18
    public class JSONNumber : ZSimpleJSON.JSONNode
    {
        // Fields
        private System.Double m_Data;  // 0x10

        // Properties
        ZSimpleJSON.JSONNodeType Tag { get; /* RVA: 0x04D882B0 */ }
        System.Boolean IsNumber { get; /* RVA: 0x02FFF600 */ }
        System.String Value { get; /* RVA: 0x0382D9A0 */ set; /* RVA: 0x0B8BD23C */ }
        System.Double AsDouble { get; /* RVA: 0x04D96320 */ set; /* RVA: 0x04D85BA0 */ }
        System.Int64 AsLong { get; /* RVA: 0x04DA2D40 */ set; /* RVA: 0x04DA2D50 */ }
        System.UInt64 AsULong { get; /* RVA: 0x0B8BD230 */ set; /* RVA: 0x04DBF6A0 */ }

        // Methods
        // RVA: 0x04DA2D20  token: 0x6000133
        public virtual ZSimpleJSON.JSONNode.Enumerator GetEnumerator() { }
        // RVA: 0x03B4AD00  token: 0x600013C
        public System.Void .ctor(System.Double aData) { }
        // RVA: 0x0B8BD1D4  token: 0x600013D
        public System.Void .ctor(System.String aData) { }
        // RVA: 0x0B8BD024  token: 0x600013E
        public virtual ZSimpleJSON.JSONNode Clone() { }
        // RVA: 0x040717C0  token: 0x600013F
        private virtual System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, ZSimpleJSON.JSONTextMode aMode) { }
        // RVA: 0x0B8BD08C  token: 0x6000140
        private static System.Boolean IsNumeric(System.Object value) { }
        // RVA: 0x04232AD0  token: 0x6000141
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x09DC1AD4  token: 0x6000142
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x04DA8180  token: 0x6000143
        public virtual System.Void Clear() { }

    }

    // TypeToken: 0x2000014  // size: 0x18
    public class JSONBool : ZSimpleJSON.JSONNode
    {
        // Fields
        private System.Boolean m_Data;  // 0x10

        // Properties
        ZSimpleJSON.JSONNodeType Tag { get; /* RVA: 0x04D9EC60 */ }
        System.Boolean IsBoolean { get; /* RVA: 0x02FFF600 */ }
        System.String Value { get; /* RVA: 0x0B8B73B0 */ set; /* RVA: 0x0B8B7404 */ }
        System.Boolean AsBool { get; /* RVA: 0x015EFCE0 */ set; /* RVA: 0x04D86130 */ }

        // Methods
        // RVA: 0x04DA2D20  token: 0x6000146
        public virtual ZSimpleJSON.JSONNode.Enumerator GetEnumerator() { }
        // RVA: 0x03A74470  token: 0x600014B
        public System.Void .ctor(System.Boolean aData) { }
        // RVA: 0x0B8B7354  token: 0x600014C
        public System.Void .ctor(System.String aData) { }
        // RVA: 0x0B8B72A8  token: 0x600014D
        public virtual ZSimpleJSON.JSONNode Clone() { }
        // RVA: 0x04503B80  token: 0x600014E
        private virtual System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, ZSimpleJSON.JSONTextMode aMode) { }
        // RVA: 0x045F14B0  token: 0x600014F
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0B8B730C  token: 0x6000150
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x04D8CCB0  token: 0x6000151
        public virtual System.Void Clear() { }

    }

    // TypeToken: 0x2000015  // size: 0x10
    public class JSONNull : ZSimpleJSON.JSONNode
    {
        // Fields
        private static ZSimpleJSON.JSONNull m_StaticInstance;  // static @ 0x0
        public static System.Boolean reuseSameInstance;  // static @ 0x8

        // Properties
        ZSimpleJSON.JSONNodeType Tag { get; /* RVA: 0x04D9EC50 */ }
        System.Boolean IsNull { get; /* RVA: 0x02FFF600 */ }
        System.String Value { get; /* RVA: 0x0B8BCFF8 */ set; /* RVA: 0x041E1670 */ }
        System.Boolean AsBool { get; /* RVA: 0x012081B0 */ set; /* RVA: 0x041E1670 */ }

        // Methods
        // RVA: 0x03D53BC0  token: 0x6000152
        public static ZSimpleJSON.JSONNull CreateOrGet() { }
        // RVA: 0x04CE7B70  token: 0x6000153
        private System.Void .ctor() { }
        // RVA: 0x04DA2D20  token: 0x6000156
        public virtual ZSimpleJSON.JSONNode.Enumerator GetEnumerator() { }
        // RVA: 0x0B8BCF60  token: 0x600015B
        public virtual ZSimpleJSON.JSONNode Clone() { }
        // RVA: 0x0B8BCFA0  token: 0x600015C
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x011EC580  token: 0x600015D
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x045F0B90  token: 0x600015E
        private virtual System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, ZSimpleJSON.JSONTextMode aMode) { }
        // RVA: 0x04CE7AE0  token: 0x600015F
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000016  // size: 0x20
    public class JSONLazyCreator : ZSimpleJSON.JSONNode
    {
        // Fields
        private ZSimpleJSON.JSONNode m_Node;  // 0x10
        private System.String m_Key;  // 0x18

        // Properties
        ZSimpleJSON.JSONNodeType Tag { get; /* RVA: 0x04D9BE00 */ }
        ZSimpleJSON.JSONNode Item { get; /* RVA: 0x0B8B7B98 */ set; /* RVA: 0x0B8B81BC */ }
        ZSimpleJSON.JSONNode Item { get; /* RVA: 0x0B8B7B98 */ set; /* RVA: 0x0B8B81BC */ }
        System.Int32 AsInt { get; /* RVA: 0x0B8B78AC */ set; /* RVA: 0x0B8B7E04 */ }
        System.Single AsFloat { get; /* RVA: 0x0B8B782C */ set; /* RVA: 0x0B8B7D78 */ }
        System.Double AsDouble { get; /* RVA: 0x0B8B77AC */ set; /* RVA: 0x0B8B7CF0 */ }
        System.Int64 AsLong { get; /* RVA: 0x0B8B7928 */ set; /* RVA: 0x0B8B7E90 */ }
        System.UInt64 AsULong { get; /* RVA: 0x0B8B7A9C */ set; /* RVA: 0x0B8B7FC4 */ }
        System.Boolean AsBool { get; /* RVA: 0x0B8B7730 */ set; /* RVA: 0x0B8B7C68 */ }
        ZSimpleJSON.JSONArray AsArray { get; /* RVA: 0x0B8B76B8 */ }
        ZSimpleJSON.JSONObject AsObject { get; /* RVA: 0x0B8B7A24 */ }

        // Methods
        // RVA: 0x04DA2D20  token: 0x6000161
        public virtual ZSimpleJSON.JSONNode.Enumerator GetEnumerator() { }
        // RVA: 0x0B8B75EC  token: 0x6000162
        public System.Void .ctor(ZSimpleJSON.JSONNode aNode) { }
        // RVA: 0x0B8B764C  token: 0x6000163
        public System.Void .ctor(ZSimpleJSON.JSONNode aNode, System.String aKey) { }
        // RVA: -1  // generic def  token: 0x6000164
        private T Set(T aVal) { }
        // RVA: 0x0B8B7468  token: 0x6000169
        public virtual System.Void Add(ZSimpleJSON.JSONNode aItem) { }
        // RVA: 0x0B8B74FC  token: 0x600016A
        public virtual System.Void Add(System.String aKey, ZSimpleJSON.JSONNode aItem) { }
        // RVA: 0x09DC0F98  token: 0x600016B
        public static System.Boolean op_Equality(ZSimpleJSON.JSONLazyCreator a, System.Object b) { }
        // RVA: 0x09DC1438  token: 0x600016C
        public static System.Boolean op_Inequality(ZSimpleJSON.JSONLazyCreator a, System.Object b) { }
        // RVA: 0x09DC0F98  token: 0x600016D
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x011EC580  token: 0x600016E
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0B8B75A4  token: 0x600017D
        private virtual System.Void WriteToStringBuilder(System.Text.StringBuilder aSB, System.Int32 aIndent, System.Int32 aIndentInc, ZSimpleJSON.JSONTextMode aMode) { }

    }

    // TypeToken: 0x2000017  // size: 0x10
    public static class JSON
    {
        // Methods
        // RVA: 0x04D5DBE0  token: 0x600017E
        public static ZSimpleJSON.JSONNode Parse(System.String aJSON) { }

    }

}

