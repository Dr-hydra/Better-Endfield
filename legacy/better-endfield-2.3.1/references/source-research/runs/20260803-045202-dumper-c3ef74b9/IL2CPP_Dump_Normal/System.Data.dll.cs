// ========================================================
// Dumped by @desirepro
// Assembly: System.Data.dll
// Classes:  240
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x2000005  // size: 0x10
    public static class SR
    {
        // Methods
        // RVA: 0x0464E630  token: 0x6000004
        private static System.String GetString(System.String name) { }
        // RVA: 0x0AD60544  token: 0x6000005
        private static System.String Format(System.String resourceFormat, System.Object p1) { }
        // RVA: 0x0AD604D4  token: 0x6000006
        private static System.String Format(System.String resourceFormat, System.Object p1, System.Object p2) { }
        // RVA: 0x0AD60454  token: 0x6000007
        private static System.String Format(System.String resourceFormat, System.Object p1, System.Object p2, System.Object p3) { }

    }

    // TypeToken: 0x2000008  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly System.LocalAppContext.<>c <>9;  // static @ 0x0
        public static System.Func<System.Boolean> <>9__6_0;  // static @ 0x8

        // Methods
        // RVA: 0x0AD605FC  token: 0x600000C
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600000D
        public System.Void .ctor() { }
        // RVA: 0x0AD605A0  token: 0x600000E
        private System.Boolean <get_DisableCaching>b__6_0() { }

    }

    // TypeToken: 0x200001A  // size: 0x18
    public sealed struct RowDiffIdUsageSection
    {
        // Fields
        private System.Data.DataTable _targetTable;  // 0x10

        // Methods
        // RVA: 0x0AD603B8  token: 0x60002E7
        private System.Void Prepare(System.Data.DataTable table) { }

    }

    // TypeToken: 0x200001B  // size: 0x18
    public sealed struct DSRowDiffIdUsageSection
    {
        // Fields
        private System.Data.DataSet _targetDS;  // 0x10

        // Methods
        // RVA: 0x0AD48150  token: 0x60002E8
        private System.Void Prepare(System.Data.DataSet ds) { }

    }

    // TypeToken: 0x2000021  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly System.Data.TypeLimiter.Scope.<>c <>9;  // static @ 0x0
        public static System.Func<System.Type,System.Boolean> <>9__3_0;  // static @ 0x8

        // Methods
        // RVA: 0x0AD75320  token: 0x600030A
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600030B
        public System.Void .ctor() { }
        // RVA: 0x0AD75274  token: 0x600030C
        private System.Boolean <.ctor>b__3_0(System.Type type) { }

    }

    // TypeToken: 0x2000020  // size: 0x20
    public sealed class Scope : System.IDisposable
    {
        // Fields
        private static readonly System.Collections.Generic.HashSet<System.Type> s_allowedTypes;  // static @ 0x0
        private System.Collections.Generic.HashSet<System.Type> m_allowedTypes;  // 0x10
        private readonly System.Data.TypeLimiter.Scope m_previousScope;  // 0x18

        // Methods
        // RVA: 0x0AD74B18  token: 0x6000305
        private System.Void .ctor(System.Data.TypeLimiter.Scope previousScope, System.Collections.Generic.IEnumerable<System.Type> allowedTypes) { }
        // RVA: 0x0AD73F54  token: 0x6000306
        public virtual System.Void Dispose() { }
        // RVA: 0x0AD74038  token: 0x6000307
        public System.Boolean IsAllowedType(System.Type type) { }
        // RVA: 0x0AD74160  token: 0x6000308
        private static System.Boolean IsTypeUnconditionallyAllowed(System.Type type) { }
        // RVA: 0x0AD742C8  token: 0x6000309
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000022  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly System.Data.TypeLimiter.<>c <>9;  // static @ 0x0
        public static System.Func<System.Data.DataColumn,System.Type> <>9__10_0;  // static @ 0x8
        public static System.Func<System.Data.DataTable,System.Collections.Generic.IEnumerable<System.Type>> <>9__11_0;  // static @ 0x10

        // Methods
        // RVA: 0x0AD752BC  token: 0x600030D
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600030E
        public System.Void .ctor() { }
        // RVA: 0x0AD75250  token: 0x600030F
        private System.Type <GetPreviouslyDeclaredDataTypes>b__10_0(System.Data.DataColumn column) { }
        // RVA: 0x0AD75268  token: 0x6000310
        private System.Collections.Generic.IEnumerable<System.Type> <GetPreviouslyDeclaredDataTypes>b__11_0(System.Data.DataTable table) { }

    }

    // TypeToken: 0x2000034  // size: 0x20
    public sealed struct ColumnError
    {
        // Fields
        private System.Data.DataColumn _column;  // 0x10
        private System.String _error;  // 0x18

    }

    // TypeToken: 0x2000038  // size: 0x58
    public sealed class DataTableRelationCollection : System.Data.DataRelationCollection
    {
        // Fields
        private readonly System.Data.DataTable _table;  // 0x38
        private readonly System.Collections.ArrayList _relations;  // 0x40
        private readonly System.Boolean _fParentCollection;  // 0x48
        private System.ComponentModel.CollectionChangeEventHandler RelationPropertyChanged;  // 0x50

        // Properties
        System.Collections.ArrayList List { get; /* RVA: 0x04D85A60 */ }
        System.Data.DataRelation Item { get; /* RVA: 0x0AD7347C */ }
        System.Data.DataRelation Item { get; /* RVA: 0x0AD7347C */ }

        // Events
        event System.ComponentModel.CollectionChangeEventHandler RelationPropertyChanged;

        // Methods
        // RVA: 0x0AD73278  token: 0x600040F
        private System.Void .ctor(System.Data.DataTable table, System.Boolean fParentCollection) { }
        // RVA: 0x0AD7308C  token: 0x6000411
        private System.Void EnsureDataSet() { }
        // RVA: 0x0AD730D8  token: 0x6000412
        protected virtual System.Data.DataSet GetDataSet() { }
        // RVA: 0x0AD72F8C  token: 0x6000417
        private System.Void AddCache(System.Data.DataRelation relation) { }
        // RVA: 0x0AD72FD0  token: 0x6000418
        protected virtual System.Void AddCore(System.Data.DataRelation relation) { }
        // RVA: 0x0AD73104  token: 0x6000419
        private System.Void RemoveCache(System.Data.DataRelation relation) { }
        // RVA: 0x0AD731BC  token: 0x600041A
        protected virtual System.Void RemoveCore(System.Data.DataRelation relation) { }

    }

    // TypeToken: 0x2000039  // size: 0x50
    public sealed class DataSetRelationCollection : System.Data.DataRelationCollection
    {
        // Fields
        private readonly System.Data.DataSet _dataSet;  // 0x38
        private readonly System.Collections.ArrayList _relations;  // 0x40
        private System.Data.DataRelation[] _delayLoadingRelations;  // 0x48

        // Properties
        System.Collections.ArrayList List { get; /* RVA: 0x04D85A60 */ }
        System.Data.DataRelation Item { get; /* RVA: 0x0AD70AE8 */ }
        System.Data.DataRelation Item { get; /* RVA: 0x0AD70AE8 */ }

        // Methods
        // RVA: 0x0AD70988  token: 0x600041B
        private System.Void .ctor(System.Data.DataSet dataSet) { }
        // RVA: 0x0AD70704  token: 0x600041D
        public virtual System.Void Clear() { }
        // RVA: 0x04D85A50  token: 0x600041E
        protected virtual System.Data.DataSet GetDataSet() { }
        // RVA: 0x0AD7018C  token: 0x6000421
        protected virtual System.Void AddCore(System.Data.DataRelation relation) { }
        // RVA: 0x0AD7074C  token: 0x6000422
        protected virtual System.Void RemoveCore(System.Data.DataRelation relation) { }

    }

    // TypeToken: 0x2000041  // size: 0x40
    public sealed class DataRowTree : System.Data.RBTree`1
    {
        // Methods
        // RVA: 0x0AD6CA0C  token: 0x6000483
        private System.Void .ctor() { }
        // RVA: 0x0AD6C9AC  token: 0x6000484
        protected virtual System.Int32 CompareNode(System.Data.DataRow record1, System.Data.DataRow record2) { }
        // RVA: 0x0AD6C9DC  token: 0x6000485
        protected virtual System.Int32 CompareSateliteTreeNode(System.Data.DataRow record1, System.Data.DataRow record2) { }

    }

    // TypeToken: 0x2000050  // size: 0x10
    public sealed class DataRowReferenceComparer : System.Collections.Generic.IEqualityComparer`1
    {
        // Fields
        private static readonly System.Data.DataView.DataRowReferenceComparer s_default;  // static @ 0x0

        // Methods
        // RVA: 0x041E1670  token: 0x6000504
        private System.Void .ctor() { }
        // RVA: 0x0829C278  token: 0x6000505
        public virtual System.Boolean Equals(System.Data.DataRow x, System.Data.DataRow y) { }
        // RVA: 0x0AD7CD04  token: 0x6000506
        public virtual System.Int32 GetHashCode(System.Data.DataRow obj) { }
        // RVA: 0x0AD7CD1C  token: 0x6000507
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200005A  // size: 0x14
    public sealed struct DataTypePrecedence
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Data.BinaryNode.DataTypePrecedence SqlDateTime;  // const
        public static System.Data.BinaryNode.DataTypePrecedence DateTimeOffset;  // const
        public static System.Data.BinaryNode.DataTypePrecedence DateTime;  // const
        public static System.Data.BinaryNode.DataTypePrecedence TimeSpan;  // const
        public static System.Data.BinaryNode.DataTypePrecedence SqlDouble;  // const
        public static System.Data.BinaryNode.DataTypePrecedence Double;  // const
        public static System.Data.BinaryNode.DataTypePrecedence SqlSingle;  // const
        public static System.Data.BinaryNode.DataTypePrecedence Single;  // const
        public static System.Data.BinaryNode.DataTypePrecedence SqlDecimal;  // const
        public static System.Data.BinaryNode.DataTypePrecedence Decimal;  // const
        public static System.Data.BinaryNode.DataTypePrecedence SqlMoney;  // const
        public static System.Data.BinaryNode.DataTypePrecedence UInt64;  // const
        public static System.Data.BinaryNode.DataTypePrecedence SqlInt64;  // const
        public static System.Data.BinaryNode.DataTypePrecedence Int64;  // const
        public static System.Data.BinaryNode.DataTypePrecedence UInt32;  // const
        public static System.Data.BinaryNode.DataTypePrecedence SqlInt32;  // const
        public static System.Data.BinaryNode.DataTypePrecedence Int32;  // const
        public static System.Data.BinaryNode.DataTypePrecedence UInt16;  // const
        public static System.Data.BinaryNode.DataTypePrecedence SqlInt16;  // const
        public static System.Data.BinaryNode.DataTypePrecedence Int16;  // const
        public static System.Data.BinaryNode.DataTypePrecedence Byte;  // const
        public static System.Data.BinaryNode.DataTypePrecedence SqlByte;  // const
        public static System.Data.BinaryNode.DataTypePrecedence SByte;  // const
        public static System.Data.BinaryNode.DataTypePrecedence Error;  // const
        public static System.Data.BinaryNode.DataTypePrecedence SqlBoolean;  // const
        public static System.Data.BinaryNode.DataTypePrecedence Boolean;  // const
        public static System.Data.BinaryNode.DataTypePrecedence SqlGuid;  // const
        public static System.Data.BinaryNode.DataTypePrecedence SqlString;  // const
        public static System.Data.BinaryNode.DataTypePrecedence String;  // const
        public static System.Data.BinaryNode.DataTypePrecedence SqlXml;  // const
        public static System.Data.BinaryNode.DataTypePrecedence SqlChars;  // const
        public static System.Data.BinaryNode.DataTypePrecedence Char;  // const
        public static System.Data.BinaryNode.DataTypePrecedence SqlBytes;  // const
        public static System.Data.BinaryNode.DataTypePrecedence SqlBinary;  // const

    }

    // TypeToken: 0x2000062  // size: 0x20
    public sealed struct ReservedWords
    {
        // Fields
        private readonly System.String _word;  // 0x10
        private readonly System.Data.Tokens _token;  // 0x18
        private readonly System.Int32 _op;  // 0x1c

        // Methods
        // RVA: 0x05390898  token: 0x600059C
        private System.Void .ctor(System.String word, System.Data.Tokens token, System.Int32 op) { }

    }

    // TypeToken: 0x200007B
    public sealed struct NodeColor
    {
        // Fields
        public System.Int32 value__;  // 0x0
        public static System.Data.RBTree.NodeColor<K> red;  // const
        public static System.Data.RBTree.NodeColor<K> black;  // const

    }

    // TypeToken: 0x200007C
    public sealed struct Node
    {
        // Fields
        private System.Int32 _selfId;  // 0x0
        private System.Int32 _leftId;  // 0x0
        private System.Int32 _rightId;  // 0x0
        private System.Int32 _parentId;  // 0x0
        private System.Int32 _nextId;  // 0x0
        private System.Int32 _subTreeSize;  // 0x0
        private K _keyOfNode;  // 0x0
        private System.Data.RBTree.NodeColor<K> _nodeColor;  // 0x0

    }

    // TypeToken: 0x200007D
    public sealed struct NodePath
    {
        // Fields
        private readonly System.Int32 _nodeID;  // 0x0
        private readonly System.Int32 _mainTreeNodeID;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000696
        private System.Void .ctor(System.Int32 nodeID, System.Int32 mainTreeNodeID) { }

    }

    // TypeToken: 0x200007E
    public sealed class TreePage
    {
        // Fields
        private readonly System.Data.RBTree.Node<K>[] _slots;  // 0x0
        private readonly System.Int32[] _slotMap;  // 0x0
        private System.Int32 _inUseCount;  // 0x0
        private System.Int32 _pageId;  // 0x0
        private System.Int32 _nextFreeSlotLine;  // 0x0

        // Properties
        System.Int32 InUseCount { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Int32 PageId { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000697
        private System.Void .ctor(System.Int32 size) { }
        // RVA: -1  // not resolved  token: 0x6000698
        private System.Int32 AllocSlot(System.Data.RBTree<K> tree) { }

    }

    // TypeToken: 0x200007F
    public sealed struct RBTreeEnumerator : System.Collections.Generic.IEnumerator`1, System.IDisposable, System.Collections.IEnumerator
    {
        // Fields
        private readonly System.Data.RBTree<K> _tree;  // 0x0
        private readonly System.Int32 _version;  // 0x0
        private System.Int32 _index;  // 0x0
        private System.Int32 _mainTreeNodeId;  // 0x0
        private K _current;  // 0x0

        // Properties
        K Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600069D
        private System.Void .ctor(System.Data.RBTree<K> tree) { }
        // RVA: -1  // not resolved  token: 0x600069E
        private System.Void .ctor(System.Data.RBTree<K> tree, System.Int32 position) { }
        // RVA: -1  // not resolved  token: 0x600069F
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x60006A0
        public virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x60006A3
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000086  // size: 0x20
    public sealed class ColumnInfo
    {
        // Fields
        public System.Boolean flag;  // 0x10
        public System.Boolean equalsOperator;  // 0x11
        public System.Data.BinaryNode expr;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x60006D6
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000089  // size: 0x48
    public sealed class IndexTree : System.Data.RBTree`1
    {
        // Fields
        private readonly System.Data.Index _index;  // 0x40

        // Methods
        // RVA: 0x0AD89020  token: 0x600071B
        private System.Void .ctor(System.Data.Index index) { }
        // RVA: 0x0AD88FE0  token: 0x600071C
        protected virtual System.Int32 CompareNode(System.Int32 record1, System.Int32 record2) { }
        // RVA: 0x0AD89000  token: 0x600071D
        protected virtual System.Int32 CompareSateliteTreeNode(System.Int32 record1, System.Int32 record2) { }

    }

    // TypeToken: 0x200008A  // size: 0x10
    public sealed class <>c
    {
        // Fields
        public static readonly System.Data.Index.<>c <>9;  // static @ 0x0
        public static System.Data.Listeners.Func<System.Data.DataViewListener,System.Data.DataViewListener,System.Boolean> <>9__22_0;  // static @ 0x8
        public static System.Data.Listeners.Action<System.Data.DataViewListener,System.Data.DataViewListener,System.ComponentModel.ListChangedEventArgs,System.Boolean,System.Boolean> <>9__85_0;  // static @ 0x10

        // Methods
        // RVA: 0x0AD944B0  token: 0x600071E
        private static System.Void .cctor() { }
        // RVA: 0x041E1670  token: 0x600071F
        public System.Void .ctor() { }
        // RVA: 0x0721682C  token: 0x6000720
        private System.Boolean <.ctor>b__22_0(System.Data.DataViewListener listener) { }
        // RVA: 0x0AD94450  token: 0x6000721
        private System.Void <OnListChanged>b__85_0(System.Data.DataViewListener listener, System.ComponentModel.ListChangedEventArgs args, System.Boolean arg2, System.Boolean arg3) { }

    }

    // TypeToken: 0x200008B  // size: 0x18
    public sealed class <>c__DisplayClass86_0
    {
        // Fields
        public System.ComponentModel.ListChangedType changedType;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000722
        public System.Void .ctor() { }
        // RVA: 0x0AD94478  token: 0x6000723
        private System.Void <MaintainDataView>b__0(System.Data.DataViewListener listener, System.ComponentModel.ListChangedType type, System.Data.DataRow row, System.Boolean track) { }

    }

    // TypeToken: 0x200008D
    public sealed class Action`4 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x600072B
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x600072C
        public virtual System.Void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4) { }

    }

    // TypeToken: 0x200008E
    public sealed class Func`2 : System.MulticastDelegate
    {
        // Methods
        // RVA: -1  // runtime  token: 0x600072D
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: -1  // runtime  token: 0x600072E
        public virtual TResult Invoke(T1 arg1) { }

    }

    // TypeToken: 0x2000092  // size: 0x20
    public sealed class NameType : System.IComparable
    {
        // Fields
        public System.String name;  // 0x10
        public System.Type type;  // 0x18

        // Methods
        // RVA: 0x032AF420  token: 0x6000773
        public System.Void .ctor(System.String n, System.Type t) { }
        // RVA: 0x0AD98808  token: 0x6000774
        public virtual System.Int32 CompareTo(System.Object obj) { }

    }

    // TypeToken: 0x2000097  // size: 0x20
    public sealed class NameType : System.IComparable
    {
        // Fields
        public readonly System.String name;  // 0x10
        public readonly System.Type type;  // 0x18

        // Methods
        // RVA: 0x032AF420  token: 0x60007BE
        public System.Void .ctor(System.String n, System.Type t) { }
        // RVA: 0x0AD9887C  token: 0x60007BF
        public virtual System.Int32 CompareTo(System.Object obj) { }

    }

    // TypeToken: 0x200009C  // size: 0x20
    public sealed class XmlNodeIdentety
    {
        // Fields
        public System.String LocalName;  // 0x10
        public System.String NamespaceURI;  // 0x18

        // Methods
        // RVA: 0x032AF420  token: 0x60007F2
        public System.Void .ctor(System.String localName, System.String namespaceURI) { }
        // RVA: 0x08479504  token: 0x60007F3
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0ADB80C4  token: 0x60007F4
        public virtual System.Boolean Equals(System.Object obj) { }

    }

    // TypeToken: 0x200009D  // size: 0x58
    public sealed class XmlNodeIdHashtable : System.Collections.Hashtable
    {
        // Fields
        private System.Data.XmlToDatasetMap.XmlNodeIdentety _id;  // 0x50

        // Properties
        System.Object Item { get; /* RVA: 0x0ADB7FBC */ }
        System.Object Item { get; /* RVA: 0x0ADB7FBC */ }
        System.Object Item { get; /* RVA: 0x0ADB7FBC */ }
        System.Object Item { get; /* RVA: 0x0ADB7FBC */ }

        // Methods
        // RVA: 0x0ADB7DF8  token: 0x60007F5
        public System.Void .ctor(System.Int32 capacity) { }

    }

    // TypeToken: 0x200009E  // size: 0x20
    public sealed class TableSchemaInfo
    {
        // Fields
        public System.Data.DataTable TableSchema;  // 0x10
        public System.Data.XmlToDatasetMap.XmlNodeIdHashtable ColumnsSchemaMap;  // 0x18

        // Methods
        // RVA: 0x0ADB4080  token: 0x60007FA
        public System.Void .ctor(System.Data.DataTable tableSchema) { }

    }

    // TypeToken: 0x20000C1  // size: 0x14
    public sealed struct Families
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Data.Common.ObjectStorage.Families DATETIME;  // const
        public static System.Data.Common.ObjectStorage.Families NUMBER;  // const
        public static System.Data.Common.ObjectStorage.Families STRING;  // const
        public static System.Data.Common.ObjectStorage.Families BOOLEAN;  // const
        public static System.Data.Common.ObjectStorage.Families ARRAY;  // const

    }

    // TypeToken: 0x20000C2  // size: 0x10
    public class TempAssemblyComparer : System.Collections.Generic.IEqualityComparer`1
    {
        // Fields
        private static readonly System.Collections.Generic.IEqualityComparer<System.Collections.Generic.KeyValuePair<System.Type,System.Xml.Serialization.XmlRootAttribute>> s_default;  // static @ 0x0

        // Methods
        // RVA: 0x041E1670  token: 0x6000AA4
        private System.Void .ctor() { }
        // RVA: 0x0ADF639C  token: 0x6000AA5
        public virtual System.Boolean Equals(System.Collections.Generic.KeyValuePair<System.Type,System.Xml.Serialization.XmlRootAttribute> x, System.Collections.Generic.KeyValuePair<System.Type,System.Xml.Serialization.XmlRootAttribute> y) { }
        // RVA: 0x0ADF64E0  token: 0x6000AA6
        public virtual System.Int32 GetHashCode(System.Collections.Generic.KeyValuePair<System.Type,System.Xml.Serialization.XmlRootAttribute> obj) { }
        // RVA: 0x0ADF6560  token: 0x6000AA7
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000E4  // size: 0x18
    public sealed class <>c__DisplayClass6_0
    {
        // Fields
        public System.Type type;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x6000CAA
        public System.Void .ctor() { }
        // RVA: 0x0AE07AF4  token: 0x6000CAB
        private System.Object <GetStaticNullForUdtType>b__0(System.Type t) { }

    }

    // TypeToken: 0x20000EB  // size: 0x34
    public sealed struct __StaticArrayInitTypeSize=36
    {
    }

    // TypeToken: 0x20000EC  // size: 0x36
    public sealed struct __StaticArrayInitTypeSize=38
    {
    }

    // TypeToken: 0x20000ED  // size: 0x44
    public sealed struct __StaticArrayInitTypeSize=52
    {
    }

    // TypeToken: 0x20000EE  // size: 0x50
    public sealed struct __StaticArrayInitTypeSize=64
    {
    }

    // TypeToken: 0x20000EF  // size: 0xA8
    public sealed struct __StaticArrayInitTypeSize=152
    {
    }

    // TypeToken: 0x20000F0  // size: 0xC0
    public sealed struct __StaticArrayInitTypeSize=176
    {
    }

    // TypeToken: 0x20000EA  // size: 0x10
    public sealed class <PrivateImplementationDetails>
    {
        // Fields
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=152 39A47A6A540EB845B37C85CE8C346359A0DB937D4AAF7A74A6C207205E0BC61E;  // static @ 0x0
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=38 540CD885F06B2760118EA5544E069ACE0C5D184B85D9D104417C14F1E536376D;  // static @ 0x98
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=52 5857EE4CE98BFABBD62B385C1098507DD0052FF3951043AAD6A1DABD495F18AA;  // static @ 0xbe
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=36 8D4DC488705859D6A837A660BDBA9E88D1BD229BC39DB97734072D04BD513ECD;  // static @ 0xf2
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=152 BA1E68F004F9EFDE72987E33682A8A5C579C4A609FBECE4F6EDBB844431D9226;  // static @ 0x116
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=152 C44E90B8C219817ECD3C403823D4770C0F744358EBF32A4282B3CE0338D4602E;  // static @ 0x1ae
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=152 D44900CF81FC3D53E7F8D2FCB6EF3B50B39ED1A857628FA737F5B4B7E0382939;  // static @ 0x246
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=176 E29424929B12EB1FDF4FD2E4911E09644CB58261C6033211F88022DDED785AE6;  // static @ 0x2de
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=64 F327BBE8D18E0318C5295B25F9A8BA9B3AFE1F44C3C244BB3921AFEB578F1591;  // static @ 0x38e
        private static readonly <PrivateImplementationDetails>.__StaticArrayInitTypeSize=52 FADB218011E7702BB9575D0C32A685DA10B5C72EB809BD9A955DB1C76E4D8315;  // static @ 0x3ce

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

namespace System
{

    // TypeToken: 0x2000006  // size: 0x10
    public static class LocalAppContextSwitches
    {
        // Fields
        private static System.Int32 s_allowArbitraryTypeInstantiation;  // static @ 0x0

        // Properties
        System.Boolean AllowArbitraryTypeInstantiation { get; /* RVA: 0x0AD60038 */ }

    }

    // TypeToken: 0x2000007  // size: 0x10
    public class LocalAppContext
    {
        // Fields
        private static System.Boolean s_isDisableCachingInitialized;  // static @ 0x0
        private static System.Boolean s_disableCaching;  // static @ 0x1
        private static System.Object s_syncObject;  // static @ 0x8

        // Properties
        System.Boolean DisableCaching { get; /* RVA: 0x0AD6012C */ }

        // Methods
        // RVA: 0x0AD6010C  token: 0x6000009
        private static System.Boolean GetCachedSwitchValue(System.String switchName, System.Int32& switchValue) { }
        // RVA: 0x0AD60098  token: 0x600000A
        private static System.Boolean GetCachedSwitchValueInternal(System.String switchName, System.Int32& switchValue) { }

    }

}

namespace System.Data
{

    // TypeToken: 0x2000009  // size: 0xF0
    public class DataColumn : System.ComponentModel.MarshalByValueComponent
    {
        // Fields
        private System.Boolean _allowNull;  // 0x20
        private System.String _caption;  // 0x28
        private System.String _columnName;  // 0x30
        private System.Type _dataType;  // 0x38
        private System.Data.Common.StorageType _storageType;  // 0x40
        private System.Object _defaultValue;  // 0x48
        private System.Data.DataSetDateTime _dateTimeMode;  // 0x50
        private System.Data.DataExpression _expression;  // 0x58
        private System.Int32 _maxLength;  // 0x60
        private System.Int32 _ordinal;  // 0x64
        private System.Boolean _readOnly;  // 0x68
        private System.Data.Index _sortIndex;  // 0x70
        private System.Data.DataTable _table;  // 0x78
        private System.Boolean _unique;  // 0x80
        private System.Data.MappingType _columnMapping;  // 0x84
        private System.Int32 _hashCode;  // 0x88
        private System.Int32 _errors;  // 0x8c
        private System.Boolean _isSqlType;  // 0x90
        private System.Boolean _implementsINullable;  // 0x91
        private System.Boolean _implementsIChangeTracking;  // 0x92
        private System.Boolean _implementsIRevertibleChangeTracking;  // 0x93
        private System.Boolean _implementsIXMLSerializable;  // 0x94
        private System.Boolean _defaultValueIsNull;  // 0x95
        private System.Collections.Generic.List<System.Data.DataColumn> _dependentColumns;  // 0x98
        private System.Data.PropertyCollection _extendedProperties;  // 0xa0
        private System.Data.Common.DataStorage _storage;  // 0xa8
        private System.Data.AutoIncrementValue _autoInc;  // 0xb0
        private System.String _columnUri;  // 0xb8
        private System.String _columnPrefix;  // 0xc0
        private System.String _encodedColumnName;  // 0xc8
        private System.Data.SimpleType _simpleType;  // 0xd0
        private static System.Int32 s_objectTypeCount;  // static @ 0x0
        private readonly System.Int32 _objectID;  // 0xd8
        private System.String <XmlDataType>k__BackingField;  // 0xe0
        private System.ComponentModel.PropertyChangedEventHandler PropertyChanging;  // 0xe8

        // Properties
        System.Boolean AllowDBNull { get; /* RVA: 0x04D866B0 */ set; /* RVA: 0x0AD4AE2C */ }
        System.Boolean AutoIncrement { get; /* RVA: 0x0AD4A904 */ set; /* RVA: 0x0AD4B204 */ }
        System.Object AutoIncrementCurrent { get; /* RVA: 0x0AD4A83C */ set; /* RVA: 0x0AD4AF84 */ }
        System.Data.AutoIncrementValue AutoInc { get; /* RVA: 0x0AD4A750 */ }
        System.Int64 AutoIncrementSeed { get; /* RVA: 0x0AD4A8A8 */ set; /* RVA: 0x0AD4B074 */ }
        System.Int64 AutoIncrementStep { get; /* RVA: 0x0AD4A8D4 */ set; /* RVA: 0x0AD4B13C */ }
        System.String Caption { get; /* RVA: 0x0AD4A920 */ set; /* RVA: 0x0AD4B3C8 */ }
        System.String ColumnName { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x0AD4B6D8 */ }
        System.String EncodedColumnName { get; /* RVA: 0x0AD4AA58 */ }
        System.IFormatProvider FormatProvider { get; /* RVA: 0x0AD4AB94 */ }
        System.Globalization.CultureInfo Locale { get; /* RVA: 0x0AD4AC90 */ }
        System.Int32 ObjectID { get; /* RVA: 0x04D8F100 */ }
        System.String Prefix { get; /* RVA: 0x04D892F0 */ set; /* RVA: 0x0AD4D598 */ }
        System.Boolean Computed { get; /* RVA: 0x09B00064 */ }
        System.Data.DataExpression DataExpression { get; /* RVA: 0x02E56440 */ }
        System.Type DataType { get; /* RVA: 0x04D85A50 */ set; /* RVA: 0x0AD4BB68 */ }
        System.Data.DataSetDateTime DateTimeMode { get; /* RVA: 0x04D86550 */ set; /* RVA: 0x0AD4C394 */ }
        System.Object DefaultValue { get; /* RVA: 0x0AD4A934 */ set; /* RVA: 0x0AD4C4F0 */ }
        System.Boolean DefaultValueIsNull { get; /* RVA: 0x04D893B0 */ }
        System.String Expression { get; /* RVA: 0x0AD4AACC */ set; /* RVA: 0x0AD4C7E4 */ }
        System.Data.PropertyCollection ExtendedProperties { get; /* RVA: 0x0AD4AB18 */ }
        System.Boolean HasData { get; /* RVA: 0x0709E904 */ }
        System.Boolean ImplementsINullable { get; /* RVA: 0x04D88160 */ }
        System.Boolean ImplementsIChangeTracking { get; /* RVA: 0x04D88170 */ }
        System.Boolean ImplementsIRevertibleChangeTracking { get; /* RVA: 0x04DAA1C0 */ }
        System.Boolean IsValueType { get; /* RVA: 0x0AD4AC48 */ }
        System.Boolean IsSqlType { get; /* RVA: 0x02EC6590 */ }
        System.Int32 MaxLength { get; /* RVA: 0x04D86790 */ set; /* RVA: 0x0AD4D1E8 */ }
        System.String Namespace { get; /* RVA: 0x0AD4ACE8 */ set; /* RVA: 0x0AD4D470 */ }
        System.Int32 Ordinal { get; /* RVA: 0x04D88110 */ }
        System.Boolean ReadOnly { get; /* RVA: 0x04D86B40 */ set; /* RVA: 0x0AD4D6E8 */ }
        System.Data.Index SortIndex { get; /* RVA: 0x0AD4AD58 */ }
        System.Data.DataTable Table { get; /* RVA: 0x04D862A0 */ }
        System.Object Item { get; /* RVA: 0x0AD4AC68 */ set; /* RVA: 0x0AD4D058 */ }
        System.Boolean Unique { get; /* RVA: 0x04D8D1F0 */ set; /* RVA: 0x0AD4D80C */ }
        System.String XmlDataType { get; /* RVA: 0x04D8D1C0 */ set; /* RVA: 0x06AC5B08 */ }
        System.Data.SimpleType SimpleType { get; /* RVA: 0x04D876B0 */ set; /* RVA: 0x0AD4D7BC */ }
        System.Data.MappingType ColumnMapping { get; /* RVA: 0x04D8FCE0 */ set; /* RVA: 0x0AD4B45C */ }
        System.Boolean IsCustomType { get; /* RVA: 0x0AD4ABE8 */ }
        System.Boolean ImplementsIXMLSerializable { get; /* RVA: 0x04D91BC0 */ }

        // Methods
        // RVA: 0x0AD4A3CC  token: 0x600000F
        public System.Void .ctor() { }
        // RVA: 0x0AD4A3A8  token: 0x6000010
        public System.Void .ctor(System.String columnName, System.Type dataType) { }
        // RVA: 0x0AD4A448  token: 0x6000011
        public System.Void .ctor(System.String columnName, System.Type dataType, System.String expr, System.Data.MappingType type) { }
        // RVA: 0x0AD4A2B8  token: 0x6000012
        private System.Void UpdateColumnType(System.Type type, System.Data.Common.StorageType typeCode) { }
        // RVA: 0x0AD49174  token: 0x6000028
        private System.String GetColumnValueAsString(System.Data.DataRow row, System.Data.DataRowVersion version) { }
        // RVA: 0x0AD49E38  token: 0x600003B
        private System.Void SetMaxLengthSimpleType() { }
        // RVA: 0x0AD49EF0  token: 0x6000041
        private System.Void SetOrdinalInternal(System.Int32 ordinal) { }
        // RVA: 0x0AD4A078  token: 0x6000046
        private System.Void SetTable(System.Data.DataTable table) { }
        // RVA: 0x0AD4920C  token: 0x6000047
        private System.Data.DataRow GetDataRow(System.Int32 index) { }
        // RVA: 0x0AD49454  token: 0x600004A
        private System.Void InitializeRecord(System.Int32 record) { }
        // RVA: 0x0AD4A144  token: 0x600004B
        private System.Void SetValue(System.Int32 record, System.Object value) { }
        // RVA: 0x0AD49080  token: 0x600004C
        private System.Void FreeRecord(System.Int32 record) { }
        // RVA: 0x04D8D200  token: 0x600004F
        private System.Void InternalUnique(System.Boolean value) { }
        // RVA: 0x0AD48294  token: 0x6000056
        private System.Void CheckColumnConstraint(System.Data.DataRow row, System.Data.DataRowAction action) { }
        // RVA: 0x0AD48358  token: 0x6000057
        private System.Boolean CheckMaxLength() { }
        // RVA: 0x0AD482E8  token: 0x6000058
        private System.Void CheckMaxLength(System.Data.DataRow dr) { }
        // RVA: 0x0AD48568  token: 0x6000059
        protected internal System.Void CheckNotAllowNull() { }
        // RVA: 0x0AD48850  token: 0x600005A
        private System.Void CheckNullable(System.Data.DataRow row) { }
        // RVA: 0x0AD488CC  token: 0x600005B
        protected System.Void CheckUnique() { }
        // RVA: 0x0AD48E78  token: 0x600005C
        private System.Int32 Compare(System.Int32 record1, System.Int32 record2) { }
        // RVA: 0x0AD48CC4  token: 0x600005D
        private System.Boolean CompareValueTo(System.Int32 record1, System.Object value, System.Boolean checkType) { }
        // RVA: 0x0AD48E4C  token: 0x600005E
        private System.Int32 CompareValueTo(System.Int32 record1, System.Object value) { }
        // RVA: 0x0AD48F5C  token: 0x600005F
        private System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0AD49054  token: 0x6000060
        private System.Void Copy(System.Int32 srcRecordNo, System.Int32 dstRecordNo) { }
        // RVA: 0x0AD48924  token: 0x6000061
        private System.Data.DataColumn Clone() { }
        // RVA: 0x0AD490B0  token: 0x6000062
        private System.Object GetAggregateValue(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0AD49270  token: 0x6000063
        private System.Int32 GetStringLength(System.Int32 record) { }
        // RVA: 0x0AD493C0  token: 0x6000064
        private System.Void Init(System.Int32 record) { }
        // RVA: 0x0AD49518  token: 0x6000065
        private static System.Boolean IsAutoIncrementType(System.Type dataType) { }
        // RVA: 0x0AD49C38  token: 0x6000067
        private System.Boolean IsValueCustomTypeInstance(System.Object value) { }
        // RVA: 0x0AD49704  token: 0x6000069
        private System.Boolean IsInRelation() { }
        // RVA: 0x0AD49804  token: 0x600006A
        private System.Boolean IsMaxLengthViolated() { }
        // RVA: 0x0AD49B18  token: 0x600006B
        private System.Boolean IsNotAllowDBNullViolated() { }
        // RVA: 0x0AD49CC4  token: 0x600006C
        protected virtual System.Void OnPropertyChanging(System.ComponentModel.PropertyChangedEventArgs pcevent) { }
        // RVA: 0x0AD49CE4  token: 0x600006D
        protected internal System.Void RaisePropertyChanging(System.String name) { }
        // RVA: 0x0AD49498  token: 0x600006E
        private System.Void InsureStorage() { }
        // RVA: 0x0AD49DF8  token: 0x600006F
        private System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x041E1670  token: 0x6000070
        private System.Void OnSetDataSet() { }
        // RVA: 0x0AD4A254  token: 0x6000071
        public virtual System.String ToString() { }
        // RVA: 0x0AD48F84  token: 0x6000072
        private System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0AD48FC4  token: 0x6000073
        private System.Object ConvertXmlToObject(System.Xml.XmlReader xmlReader, System.Xml.Serialization.XmlRootAttribute xmlAttrib) { }
        // RVA: 0x0AD48EA4  token: 0x6000074
        private System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0AD48EE4  token: 0x6000075
        private System.Void ConvertObjectToXml(System.Object value, System.Xml.XmlWriter xmlWriter, System.Xml.Serialization.XmlRootAttribute xmlAttrib) { }
        // RVA: 0x0AD49234  token: 0x6000076
        private System.Object GetEmptyColumnStore(System.Int32 recordCount) { }
        // RVA: 0x0AD4902C  token: 0x6000077
        private System.Void CopyValueIntoStore(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0AD4A028  token: 0x6000078
        private System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }
        // RVA: 0x0AD481C4  token: 0x6000079
        private System.Void AddDependentColumn(System.Data.DataColumn expressionColumn) { }
        // RVA: 0x0AD49D5C  token: 0x600007A
        private System.Void RemoveDependentColumn(System.Data.DataColumn expressionColumn) { }
        // RVA: 0x0AD49298  token: 0x600007B
        private System.Void HandleDependentColumnList(System.Data.DataExpression oldExpression, System.Data.DataExpression newExpression) { }

    }

    // TypeToken: 0x200000A  // size: 0x18
    public abstract class AutoIncrementValue
    {
        // Fields
        private System.Boolean <Auto>k__BackingField;  // 0x10

        // Properties
        System.Boolean Auto { get; /* RVA: 0x015EFCE0 */ set; /* RVA: 0x04D86130 */ }
        System.Object Current { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Int64 Seed { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Int64 Step { get; /* RVA: -1  // abstract */ set; /* RVA: -1  // abstract */ }
        System.Type DataType { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000085
        private virtual System.Void SetCurrent(System.Object value, System.IFormatProvider formatProvider) { }
        // RVA: -1  // abstract  token: 0x6000086
        private virtual System.Void SetCurrentAndIncrement(System.Object value) { }
        // RVA: -1  // abstract  token: 0x6000087
        private virtual System.Void MoveAfter() { }
        // RVA: 0x0AD47FFC  token: 0x6000088
        private System.Data.AutoIncrementValue Clone() { }
        // RVA: 0x041E1670  token: 0x6000089
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200000B  // size: 0x30
    public sealed class AutoIncrementInt64 : System.Data.AutoIncrementValue
    {
        // Fields
        private System.Int64 _current;  // 0x18
        private System.Int64 _seed;  // 0x20
        private System.Int64 _step;  // 0x28

        // Properties
        System.Object Current { get; /* RVA: 0x0AD47E14 */ set; /* RVA: 0x0AD47EAC */ }
        System.Type DataType { get; /* RVA: 0x0AD47E5C */ }
        System.Int64 Seed { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x0AD47EFC */ }
        System.Int64 Step { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x0AD47F80 */ }

        // Methods
        // RVA: 0x04DAA1A0  token: 0x6000091
        private virtual System.Void MoveAfter() { }
        // RVA: 0x0AD47DB0  token: 0x6000092
        private virtual System.Void SetCurrent(System.Object value, System.IFormatProvider formatProvider) { }
        // RVA: 0x0AD47C9C  token: 0x6000093
        private virtual System.Void SetCurrentAndIncrement(System.Object value) { }
        // RVA: 0x0AD47BEC  token: 0x6000094
        private System.Boolean BoundaryCheck(System.Numerics.BigInteger value) { }
        // RVA: 0x04DAA1B0  token: 0x6000095
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000C  // size: 0x40
    public sealed class AutoIncrementBigInteger : System.Data.AutoIncrementValue
    {
        // Fields
        private System.Numerics.BigInteger _current;  // 0x18
        private System.Int64 _seed;  // 0x28
        private System.Numerics.BigInteger _step;  // 0x30

        // Properties
        System.Object Current { get; /* RVA: 0x0AD47824 */ set; /* RVA: 0x0AD4791C */ }
        System.Type DataType { get; /* RVA: 0x0AD4786C */ }
        System.Int64 Seed { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x0AD47974 */ }
        System.Int64 Step { get; /* RVA: 0x0AD478BC */ set; /* RVA: 0x0AD47A58 */ }

        // Methods
        // RVA: 0x0AD47658  token: 0x600009D
        private virtual System.Void MoveAfter() { }
        // RVA: 0x0AD477A0  token: 0x600009E
        private virtual System.Void SetCurrent(System.Object value, System.IFormatProvider formatProvider) { }
        // RVA: 0x0AD476E0  token: 0x600009F
        private virtual System.Void SetCurrentAndIncrement(System.Object value) { }
        // RVA: 0x0AD47558  token: 0x60000A0
        private System.Boolean BoundaryCheck(System.Numerics.BigInteger value) { }
        // RVA: 0x0AD477CC  token: 0x60000A1
        public System.Void .ctor() { }

    }

    // TypeToken: 0x200000D  // size: 0x90
    public class DataException : System.SystemException
    {
        // Methods
        // RVA: 0x0A761450  token: 0x60000A2
        protected System.Void .ctor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x0AD4DB30  token: 0x60000A3
        public System.Void .ctor() { }
        // RVA: 0x0AD4DB10  token: 0x60000A4
        public System.Void .ctor(System.String s) { }
        // RVA: 0x0A79E114  token: 0x60000A5
        public System.Void .ctor(System.String s, System.Exception innerException) { }

    }

    // TypeToken: 0x200000E  // size: 0x90
    public class ConstraintException : System.Data.DataException
    {
        // Methods
        // RVA: 0x0A76172C  token: 0x60000A6
        protected System.Void .ctor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x0AD480EC  token: 0x60000A7
        public System.Void .ctor() { }
        // RVA: 0x0AD48130  token: 0x60000A8
        public System.Void .ctor(System.String s) { }

    }

    // TypeToken: 0x200000F  // size: 0x90
    public class DeletedRowInaccessibleException : System.Data.DataException
    {
        // Methods
        // RVA: 0x0A76172C  token: 0x60000A9
        protected System.Void .ctor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x0AD57A84  token: 0x60000AA
        public System.Void .ctor() { }
        // RVA: 0x0AD57AC8  token: 0x60000AB
        public System.Void .ctor(System.String s) { }

    }

    // TypeToken: 0x2000010  // size: 0x90
    public class DuplicateNameException : System.Data.DataException
    {
        // Methods
        // RVA: 0x0A76172C  token: 0x60000AC
        protected System.Void .ctor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x0AD57B08  token: 0x60000AD
        public System.Void .ctor() { }
        // RVA: 0x0AD57AE8  token: 0x60000AE
        public System.Void .ctor(System.String s) { }

    }

    // TypeToken: 0x2000011  // size: 0x90
    public class InRowChangingEventException : System.Data.DataException
    {
        // Methods
        // RVA: 0x0A76172C  token: 0x60000AF
        protected System.Void .ctor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x0AD5FF70  token: 0x60000B0
        public System.Void .ctor() { }
        // RVA: 0x0AD5FFB4  token: 0x60000B1
        public System.Void .ctor(System.String s) { }

    }

    // TypeToken: 0x2000012  // size: 0x90
    public class InvalidConstraintException : System.Data.DataException
    {
        // Methods
        // RVA: 0x0A76172C  token: 0x60000B2
        protected System.Void .ctor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x0AD5FFF4  token: 0x60000B3
        public System.Void .ctor() { }
        // RVA: 0x0AD5FFD4  token: 0x60000B4
        public System.Void .ctor(System.String s) { }

    }

    // TypeToken: 0x2000013  // size: 0x90
    public class NoNullAllowedException : System.Data.DataException
    {
        // Methods
        // RVA: 0x0A76172C  token: 0x60000B5
        protected System.Void .ctor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x0AD602A0  token: 0x60000B6
        public System.Void .ctor() { }
        // RVA: 0x0AD60280  token: 0x60000B7
        public System.Void .ctor(System.String s) { }

    }

    // TypeToken: 0x2000014  // size: 0x90
    public class ReadOnlyException : System.Data.DataException
    {
        // Methods
        // RVA: 0x0A76172C  token: 0x60000B8
        protected System.Void .ctor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x0AD60354  token: 0x60000B9
        public System.Void .ctor() { }
        // RVA: 0x0AD60398  token: 0x60000BA
        public System.Void .ctor(System.String s) { }

    }

    // TypeToken: 0x2000015  // size: 0x90
    public class RowNotInTableException : System.Data.DataException
    {
        // Methods
        // RVA: 0x0A76172C  token: 0x60000BB
        protected System.Void .ctor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x0AD603F0  token: 0x60000BC
        public System.Void .ctor() { }
        // RVA: 0x0AD60434  token: 0x60000BD
        public System.Void .ctor(System.String s) { }

    }

    // TypeToken: 0x2000016  // size: 0x90
    public class VersionNotFoundException : System.Data.DataException
    {
        // Methods
        // RVA: 0x0A76172C  token: 0x60000BE
        protected System.Void .ctor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x0AD60660  token: 0x60000BF
        public System.Void .ctor() { }
        // RVA: 0x0AD606A4  token: 0x60000C0
        public System.Void .ctor(System.String s) { }

    }

    // TypeToken: 0x2000017  // size: 0x10
    public static class ExceptionBuilder
    {
        // Methods
        // RVA: 0x0AD5BBF4  token: 0x60000C1
        private static System.Void TraceException(System.String trace, System.Exception e) { }
        // RVA: 0x0AD5BB34  token: 0x60000C2
        private static System.Exception TraceExceptionAsReturnValue(System.Exception e) { }
        // RVA: 0x0AD5BB74  token: 0x60000C3
        private static System.Exception TraceExceptionForCapture(System.Exception e) { }
        // RVA: 0x0AD5BBB4  token: 0x60000C4
        private static System.Exception TraceExceptionWithoutRethrow(System.Exception e) { }
        // RVA: 0x0AD5C0C8  token: 0x60000C5
        private static System.Exception _Argument(System.String error) { }
        // RVA: 0x0AD5C050  token: 0x60000C6
        private static System.Exception _Argument(System.String error, System.Exception innerException) { }
        // RVA: 0x0AD5BF60  token: 0x60000C7
        private static System.Exception _ArgumentNull(System.String paramName, System.String msg) { }
        // RVA: 0x0AD5BFD8  token: 0x60000C8
        private static System.Exception _ArgumentOutOfRange(System.String paramName, System.String msg) { }
        // RVA: 0x0AD5C338  token: 0x60000C9
        private static System.Exception _IndexOutOfRange(System.String error) { }
        // RVA: 0x0AD5C470  token: 0x60000CA
        private static System.Exception _InvalidOperation(System.String error) { }
        // RVA: 0x0AD5C408  token: 0x60000CB
        private static System.Exception _InvalidEnumArgumentException(System.String error) { }
        // RVA: -1  // generic def  token: 0x60000CC
        private static System.Exception _InvalidEnumArgumentException(T value) { }
        // RVA: 0x0AD5BA40  token: 0x60000CD
        private static System.Void ThrowDataException(System.String error, System.Exception innerException) { }
        // RVA: 0x0AD5C198  token: 0x60000CE
        private static System.Exception _Data(System.String error) { }
        // RVA: 0x0AD5C130  token: 0x60000CF
        private static System.Exception _Constraint(System.String error) { }
        // RVA: 0x0AD5C3A0  token: 0x60000D0
        private static System.Exception _InvalidConstraint(System.String error) { }
        // RVA: 0x0AD5C200  token: 0x60000D1
        private static System.Exception _DeletedRowInaccessible(System.String error) { }
        // RVA: 0x0AD5C268  token: 0x60000D2
        private static System.Exception _DuplicateName(System.String error) { }
        // RVA: 0x0AD5C2D0  token: 0x60000D3
        private static System.Exception _InRowChangingEvent(System.String error) { }
        // RVA: 0x0AD5C4D8  token: 0x60000D4
        private static System.Exception _NoNullAllowed(System.String error) { }
        // RVA: 0x0AD5C540  token: 0x60000D5
        private static System.Exception _ReadOnly(System.String error) { }
        // RVA: 0x0AD5C5A8  token: 0x60000D6
        private static System.Exception _RowNotInTable(System.String error) { }
        // RVA: 0x0AD5C610  token: 0x60000D7
        private static System.Exception _VersionNotFound(System.String error) { }
        // RVA: 0x0AD57C88  token: 0x60000D8
        public static System.Exception ArgumentNull(System.String paramName) { }
        // RVA: 0x0AD57CD4  token: 0x60000D9
        public static System.Exception ArgumentOutOfRange(System.String paramName) { }
        // RVA: 0x0AD57E6C  token: 0x60000DA
        public static System.Exception BadObjectPropertyAccess(System.String error) { }
        // RVA: 0x0AD5BC78  token: 0x60000DB
        public static System.Exception TypeNotAllowed(System.Type type) { }
        // RVA: 0x0AD5886C  token: 0x60000DC
        public static System.Exception CaseInsensitiveNameConflict(System.String name) { }
        // RVA: 0x0AD5A5DC  token: 0x60000DD
        public static System.Exception NamespaceNameConflict(System.String name) { }
        // RVA: 0x0AD59D3C  token: 0x60000DE
        public static System.Exception InvalidOffsetLength() { }
        // RVA: 0x0AD589CC  token: 0x60000DF
        public static System.Exception ColumnNotInTheTable(System.String column, System.String table) { }
        // RVA: 0x0AD58998  token: 0x60000E0
        public static System.Exception ColumnNotInAnyTable() { }
        // RVA: 0x0AD58A24  token: 0x60000E1
        public static System.Exception ColumnOutOfRange(System.Int32 index) { }
        // RVA: 0x0AD58A98  token: 0x60000E2
        public static System.Exception ColumnOutOfRange(System.String column) { }
        // RVA: 0x0AD580BC  token: 0x60000E3
        public static System.Exception CannotAddColumn1(System.String column) { }
        // RVA: 0x0AD58104  token: 0x60000E4
        public static System.Exception CannotAddColumn2(System.String column) { }
        // RVA: 0x0AD5814C  token: 0x60000E5
        public static System.Exception CannotAddColumn3() { }
        // RVA: 0x0AD58180  token: 0x60000E6
        public static System.Exception CannotAddColumn4(System.String column) { }
        // RVA: 0x0AD58258  token: 0x60000E7
        public static System.Exception CannotAddDuplicate(System.String column) { }
        // RVA: 0x0AD581C8  token: 0x60000E8
        public static System.Exception CannotAddDuplicate2(System.String table) { }
        // RVA: 0x0AD58210  token: 0x60000E9
        public static System.Exception CannotAddDuplicate3(System.String table) { }
        // RVA: 0x0AD58418  token: 0x60000EA
        public static System.Exception CannotRemoveColumn() { }
        // RVA: 0x0AD584FC  token: 0x60000EB
        public static System.Exception CannotRemovePrimaryKey() { }
        // RVA: 0x0AD583D0  token: 0x60000EC
        public static System.Exception CannotRemoveChildKey(System.String relation) { }
        // RVA: 0x0AD5844C  token: 0x60000ED
        public static System.Exception CannotRemoveConstraint(System.String constraint, System.String table) { }
        // RVA: 0x0AD584A4  token: 0x60000EE
        public static System.Exception CannotRemoveExpression(System.String column, System.String expression) { }
        // RVA: 0x0AD57BB4  token: 0x60000EF
        public static System.Exception AddPrimaryKeyConstraint() { }
        // RVA: 0x0AD5A708  token: 0x60000F0
        public static System.Exception NoConstraintName() { }
        // RVA: 0x0AD58D34  token: 0x60000F1
        public static System.Exception ConstraintViolation(System.String constraint) { }
        // RVA: 0x0AD5A1D0  token: 0x60000F2
        public static System.String KeysToString(System.Object[] keys) { }
        // RVA: 0x0AD5BD9C  token: 0x60000F3
        public static System.String UniqueConstraintViolationText(System.Data.DataColumn[] columns, System.Object[] values) { }
        // RVA: 0x0AD58D18  token: 0x60000F4
        public static System.Exception ConstraintViolation(System.Data.DataColumn[] columns, System.Object[] values) { }
        // RVA: 0x0AD58C3C  token: 0x60000F5
        public static System.Exception ConstraintOutOfRange(System.Int32 index) { }
        // RVA: 0x0AD59274  token: 0x60000F6
        public static System.Exception DuplicateConstraint(System.String constraint) { }
        // RVA: 0x0AD591E4  token: 0x60000F7
        public static System.Exception DuplicateConstraintName(System.String constraint) { }
        // RVA: 0x0AD5A624  token: 0x60000F8
        public static System.Exception NeededForForeignKeyConstraint(System.Data.UniqueConstraint key, System.Data.ForeignKeyConstraint fk) { }
        // RVA: 0x0AD5BEF8  token: 0x60000F9
        public static System.Exception UniqueConstraintViolation() { }
        // RVA: 0x0AD58C08  token: 0x60000FA
        public static System.Exception ConstraintForeignTable() { }
        // RVA: 0x0AD58CB0  token: 0x60000FB
        public static System.Exception ConstraintParentValues() { }
        // RVA: 0x0AD58BB0  token: 0x60000FC
        public static System.Exception ConstraintAddFailed(System.Data.DataTable table) { }
        // RVA: 0x0AD58CE4  token: 0x60000FD
        public static System.Exception ConstraintRemoveFailed() { }
        // RVA: 0x0AD5965C  token: 0x60000FE
        public static System.Exception FailedCascadeDelete(System.String constraint) { }
        // RVA: 0x0AD596A4  token: 0x60000FF
        public static System.Exception FailedCascadeUpdate(System.String constraint) { }
        // RVA: 0x0AD596EC  token: 0x6000100
        public static System.Exception FailedClearParentTable(System.String table, System.String constraint, System.String childTable) { }
        // RVA: 0x0AD59754  token: 0x6000101
        public static System.Exception ForeignKeyViolation(System.String constraint, System.Object[] keys) { }
        // RVA: 0x0AD5B0C8  token: 0x6000102
        public static System.Exception RemoveParentRow(System.Data.ForeignKeyConstraint constraint) { }
        // RVA: 0x0AD5A35C  token: 0x6000103
        public static System.String MaxLengthViolationText(System.String columnName) { }
        // RVA: 0x0AD5A854  token: 0x6000104
        public static System.String NotAllowDBNullViolationText(System.String columnName) { }
        // RVA: 0x0AD58748  token: 0x6000105
        public static System.Exception CantAddConstraintToMultipleNestedTable(System.String tableName) { }
        // RVA: 0x0AD57DBC  token: 0x6000106
        public static System.Exception AutoIncrementAndExpression() { }
        // RVA: 0x0AD57D88  token: 0x6000107
        public static System.Exception AutoIncrementAndDefaultValue() { }
        // RVA: 0x0AD57E38  token: 0x6000108
        public static System.Exception AutoIncrementSeed() { }
        // RVA: 0x0AD58790  token: 0x6000109
        public static System.Exception CantChangeDataType() { }
        // RVA: 0x0AD5A8C4  token: 0x600010A
        public static System.Exception NullDataType() { }
        // RVA: 0x0AD58964  token: 0x600010B
        public static System.Exception ColumnNameRequired() { }
        // RVA: 0x0AD58F2C  token: 0x600010C
        public static System.Exception DefaultValueAndAutoIncrement() { }
        // RVA: 0x0AD59008  token: 0x600010D
        public static System.Exception DefaultValueDataType(System.String column, System.Type defaultType, System.Type columnType, System.Exception inner) { }
        // RVA: 0x0AD58F60  token: 0x600010E
        public static System.Exception DefaultValueColumnDataType(System.String column, System.Type defaultType, System.Type columnType, System.Exception inner) { }
        // RVA: 0x0AD595A0  token: 0x600010F
        public static System.Exception ExpressionAndUnique() { }
        // RVA: 0x0AD5956C  token: 0x6000110
        public static System.Exception ExpressionAndReadOnly() { }
        // RVA: 0x0AD59504  token: 0x6000111
        public static System.Exception ExpressionAndConstraint(System.Data.DataColumn column, System.Data.Constraint constraint) { }
        // RVA: 0x0AD59608  token: 0x6000112
        public static System.Exception ExpressionInConstraint(System.Data.DataColumn column) { }
        // RVA: 0x0AD595D4  token: 0x6000113
        public static System.Exception ExpressionCircular() { }
        // RVA: 0x0AD5A80C  token: 0x6000114
        public static System.Exception NonUniqueValues(System.String column) { }
        // RVA: 0x0AD5A8F8  token: 0x6000115
        public static System.Exception NullKeyValues(System.String column) { }
        // RVA: 0x0AD5A974  token: 0x6000116
        public static System.Exception NullValues(System.String column) { }
        // RVA: 0x0AD5ABB0  token: 0x6000117
        public static System.Exception ReadOnlyAndExpression() { }
        // RVA: 0x0AD5ABE4  token: 0x6000118
        public static System.Exception ReadOnly(System.String column) { }
        // RVA: 0x0AD5BD68  token: 0x6000119
        public static System.Exception UniqueAndExpression() { }
        // RVA: 0x0AD5B530  token: 0x600011A
        public static System.Exception SetFailed(System.Object value, System.Data.DataColumn column, System.Type type, System.Exception innerException) { }
        // RVA: 0x0AD586F4  token: 0x600011B
        public static System.Exception CannotSetToNull(System.Data.DataColumn column) { }
        // RVA: 0x0AD5A2C0  token: 0x600011C
        public static System.Exception LongerThanMaxLength(System.Data.DataColumn column) { }
        // RVA: 0x0AD585B8  token: 0x600011D
        public static System.Exception CannotSetMaxLength(System.Data.DataColumn column, System.Int32 value) { }
        // RVA: 0x0AD58564  token: 0x600011E
        public static System.Exception CannotSetMaxLength2(System.Data.DataColumn column) { }
        // RVA: 0x0AD58644  token: 0x600011F
        public static System.Exception CannotSetSimpleContentType(System.String columnName, System.Type type) { }
        // RVA: 0x0AD5869C  token: 0x6000120
        public static System.Exception CannotSetSimpleContent(System.String columnName, System.Type type) { }
        // RVA: 0x0AD582E8  token: 0x6000121
        public static System.Exception CannotChangeNamespace(System.String columnName) { }
        // RVA: 0x0AD598E8  token: 0x6000122
        public static System.Exception HasToBeStringType(System.Data.DataColumn column) { }
        // RVA: 0x0AD57DF0  token: 0x6000123
        public static System.Exception AutoIncrementCannotSetIfHasData(System.String typeName) { }
        // RVA: 0x0AD59984  token: 0x6000124
        public static System.Exception INullableUDTwithoutStaticNull(System.String typeName) { }
        // RVA: 0x0AD5993C  token: 0x6000125
        public static System.Exception IComparableNotImplemented(System.String typeName) { }
        // RVA: 0x0AD5BCD8  token: 0x6000126
        public static System.Exception UDTImplementsIChangeTrackingButnotIRevertible(System.String typeName) { }
        // RVA: 0x0AD59BBC  token: 0x6000127
        public static System.Exception InvalidDataColumnMapping(System.Type type) { }
        // RVA: 0x0AD58530  token: 0x6000128
        public static System.Exception CannotSetDateTimeModeForNonDateTimeColumns() { }
        // RVA: 0x0AD59C1C  token: 0x6000129
        public static System.Exception InvalidDateTimeMode(System.Data.DataSetDateTime mode) { }
        // RVA: 0x0AD587C4  token: 0x600012A
        public static System.Exception CantChangeDateTimeMode(System.Data.DataSetDateTime oldValue, System.Data.DataSetDateTime newValue) { }
        // RVA: 0x0AD58B28  token: 0x600012B
        public static System.Exception ColumnTypeNotSupported() { }
        // RVA: 0x0AD5B614  token: 0x600012C
        public static System.Exception SetFailed(System.String name) { }
        // RVA: 0x0AD58054  token: 0x600012D
        public static System.Exception CanNotUse() { }
        // RVA: 0x0AD5B65C  token: 0x600012E
        public static System.Exception SetIListObject() { }
        // RVA: 0x0AD57B80  token: 0x600012F
        public static System.Exception AddNewNotAllowNull() { }
        // RVA: 0x0AD5A890  token: 0x6000130
        public static System.Exception NotOpen() { }
        // RVA: 0x0AD58E00  token: 0x6000131
        public static System.Exception CreateChildView() { }
        // RVA: 0x0AD57F1C  token: 0x6000132
        public static System.Exception CanNotDelete() { }
        // RVA: 0x0AD5981C  token: 0x6000133
        public static System.Exception GetElementIndex(System.Int32 index) { }
        // RVA: 0x0AD57B4C  token: 0x6000134
        public static System.Exception AddExternalObject() { }
        // RVA: 0x0AD57EE8  token: 0x6000135
        public static System.Exception CanNotClear() { }
        // RVA: 0x0AD59AC8  token: 0x6000136
        public static System.Exception InsertExternalObject() { }
        // RVA: 0x0AD5B094  token: 0x6000137
        public static System.Exception RemoveExternalObject() { }
        // RVA: 0x0AD5A128  token: 0x6000138
        public static System.Exception KeyTableMismatch() { }
        // RVA: 0x0AD5A0F4  token: 0x6000139
        public static System.Exception KeyNoColumns() { }
        // RVA: 0x0AD5A15C  token: 0x600013A
        public static System.Exception KeyTooManyColumns(System.Int32 cols) { }
        // RVA: 0x0AD5A044  token: 0x600013B
        public static System.Exception KeyDuplicateColumns(System.String columnName) { }
        // RVA: 0x0AD5AD8C  token: 0x600013C
        public static System.Exception RelationDataSetMismatch() { }
        // RVA: 0x0AD58B7C  token: 0x600013D
        public static System.Exception ColumnsTypeMismatch() { }
        // RVA: 0x0AD5A08C  token: 0x600013E
        public static System.Exception KeyLengthMismatch() { }
        // RVA: 0x0AD5A0C0  token: 0x600013F
        public static System.Exception KeyLengthZero() { }
        // RVA: 0x0AD597B4  token: 0x6000140
        public static System.Exception ForeignRelation() { }
        // RVA: 0x0AD5A010  token: 0x6000141
        public static System.Exception KeyColumnsIdentical() { }
        // RVA: 0x0AD5AE5C  token: 0x6000142
        public static System.Exception RelationForeignTable(System.String t1, System.String t2) { }
        // RVA: 0x0AD59890  token: 0x6000143
        public static System.Exception GetParentRowTableMismatch(System.String t1, System.String t2) { }
        // RVA: 0x0AD5B690  token: 0x6000144
        public static System.Exception SetParentRowTableMismatch(System.String t1, System.String t2) { }
        // RVA: 0x0AD5AE28  token: 0x6000145
        public static System.Exception RelationForeignRow() { }
        // RVA: 0x0AD5AEB4  token: 0x6000146
        public static System.Exception RelationNestedReadOnly() { }
        // RVA: 0x0AD5B7EC  token: 0x6000147
        public static System.Exception TableCantBeNestedInTwoTables(System.String tableName) { }
        // RVA: 0x0AD5A314  token: 0x6000148
        public static System.Exception LoopInNestedRelations(System.String tableName) { }
        // RVA: 0x0AD5ADF4  token: 0x6000149
        public static System.Exception RelationDoesNotExist() { }
        // RVA: 0x0AD5A9BC  token: 0x600014A
        public static System.Exception ParentOrChildColumnsDoNotHaveDataSet() { }
        // RVA: 0x0AD599CC  token: 0x600014B
        public static System.Exception InValidNestedRelation(System.String childTableName) { }
        // RVA: 0x0AD59D70  token: 0x600014C
        public static System.Exception InvalidParentNamespaceinNestedRelation(System.String childTableName) { }
        // RVA: 0x0AD5B35C  token: 0x600014D
        public static System.Exception RowNotInTheDataSet() { }
        // RVA: 0x0AD5B390  token: 0x600014E
        public static System.Exception RowNotInTheTable() { }
        // RVA: 0x0AD593EC  token: 0x600014F
        public static System.Exception EditInRowChanging() { }
        // RVA: 0x0AD59468  token: 0x6000150
        public static System.Exception EndEditInRowChanging() { }
        // RVA: 0x0AD57EB4  token: 0x6000151
        public static System.Exception BeginEditInRowChanging() { }
        // RVA: 0x0AD58088  token: 0x6000152
        public static System.Exception CancelEditInRowChanging() { }
        // RVA: 0x0AD59100  token: 0x6000153
        public static System.Exception DeleteInRowDeleting() { }
        // RVA: 0x0AD5BF2C  token: 0x6000154
        public static System.Exception ValueArrayLength() { }
        // RVA: 0x0AD5A73C  token: 0x6000155
        public static System.Exception NoCurrentData() { }
        // RVA: 0x0AD5A770  token: 0x6000156
        public static System.Exception NoOriginalData() { }
        // RVA: 0x0AD5A7A4  token: 0x6000157
        public static System.Exception NoProposedData() { }
        // RVA: 0x0AD5B438  token: 0x6000158
        public static System.Exception RowRemovedFromTheTable() { }
        // RVA: 0x0AD59134  token: 0x6000159
        public static System.Exception DeletedRowInaccessible() { }
        // RVA: 0x0AD5B194  token: 0x600015A
        public static System.Exception RowAlreadyDeleted() { }
        // RVA: 0x0AD5B264  token: 0x600015B
        public static System.Exception RowEmpty() { }
        // RVA: 0x0AD59EA4  token: 0x600015C
        public static System.Exception InvalidRowVersion() { }
        // RVA: 0x0AD5B3C4  token: 0x600015D
        public static System.Exception RowOutOfRange(System.Int32 index) { }
        // RVA: 0x0AD5B2E0  token: 0x600015E
        public static System.Exception RowInsertTwice(System.Int32 index, System.String tableName) { }
        // RVA: 0x0AD5B298  token: 0x600015F
        public static System.Exception RowInsertMissing(System.String tableName) { }
        // RVA: 0x0AD5B230  token: 0x6000160
        public static System.Exception RowAlreadyRemoved() { }
        // RVA: 0x0AD5A574  token: 0x6000161
        public static System.Exception MultipleParents() { }
        // RVA: 0x0AD59E6C  token: 0x6000162
        public static System.Exception InvalidRowState(System.Data.DataRowState state) { }
        // RVA: 0x0AD59E38  token: 0x6000163
        public static System.Exception InvalidRowBitPattern() { }
        // RVA: 0x0AD5B4FC  token: 0x6000164
        private static System.Exception SetDataSetNameToEmpty() { }
        // RVA: 0x0AD5B4B4  token: 0x6000165
        private static System.Exception SetDataSetNameConflicting(System.String name) { }
        // RVA: 0x0AD58E34  token: 0x6000166
        public static System.Exception DataSetUnsupportedSchema(System.String ns) { }
        // RVA: 0x0AD5A3A0  token: 0x6000167
        public static System.Exception MergeMissingDefinition(System.String obj) { }
        // RVA: 0x0AD5BA0C  token: 0x6000168
        public static System.Exception TablesInDifferentSets() { }
        // RVA: 0x0AD5AC60  token: 0x6000169
        public static System.Exception RelationAlreadyExists() { }
        // RVA: 0x0AD5B1C8  token: 0x600016A
        public static System.Exception RowAlreadyInOtherCollection() { }
        // RVA: 0x0AD5B1FC  token: 0x600016B
        public static System.Exception RowAlreadyInTheCollection() { }
        // RVA: 0x0AD5AC2C  token: 0x600016C
        public static System.Exception RecordStateRange() { }
        // RVA: 0x0AD59A14  token: 0x600016D
        public static System.Exception IndexKeyLength(System.Int32 length, System.Int32 keyLength) { }
        // RVA: 0x0AD5B11C  token: 0x600016E
        public static System.Exception RemovePrimaryKey(System.Data.DataTable table) { }
        // RVA: 0x0AD5AC94  token: 0x600016F
        public static System.Exception RelationAlreadyInOtherDataSet() { }
        // RVA: 0x0AD5ACC8  token: 0x6000170
        public static System.Exception RelationAlreadyInTheDataSet() { }
        // RVA: 0x0AD5AEE8  token: 0x6000171
        public static System.Exception RelationNotInTheDataSet(System.String relation) { }
        // RVA: 0x0AD5AF30  token: 0x6000172
        public static System.Exception RelationOutOfRange(System.Object index) { }
        // RVA: 0x0AD59304  token: 0x6000173
        public static System.Exception DuplicateRelation(System.String relation) { }
        // RVA: 0x0AD5B02C  token: 0x6000174
        public static System.Exception RelationTableNull() { }
        // RVA: 0x0AD5ADC0  token: 0x6000175
        public static System.Exception RelationDataSetNull() { }
        // RVA: 0x0AD5B060  token: 0x6000176
        public static System.Exception RelationTableWasRemoved() { }
        // RVA: 0x0AD5A9F0  token: 0x6000177
        public static System.Exception ParentTableMismatch() { }
        // RVA: 0x0AD588E8  token: 0x6000178
        public static System.Exception ChildTableMismatch() { }
        // RVA: 0x0AD5949C  token: 0x6000179
        public static System.Exception EnforceConstraint() { }
        // RVA: 0x0AD588B4  token: 0x600017A
        public static System.Exception CaseLocaleMismatch() { }
        // RVA: 0x0AD582A0  token: 0x600017B
        public static System.Exception CannotChangeCaseLocale() { }
        // RVA: 0x0AD582AC  token: 0x600017C
        public static System.Exception CannotChangeCaseLocale(System.Exception innerException) { }
        // RVA: 0x0AD59E00  token: 0x600017D
        public static System.Exception InvalidRemotingFormat(System.Data.SerializationFormat mode) { }
        // RVA: 0x0AD5B834  token: 0x600017E
        public static System.Exception TableForeignPrimaryKey() { }
        // RVA: 0x0AD5B7B8  token: 0x600017F
        public static System.Exception TableCannotAddToSimpleContent() { }
        // RVA: 0x0AD5A7D8  token: 0x6000180
        public static System.Exception NoTableName() { }
        // RVA: 0x0AD5A5A8  token: 0x6000181
        public static System.Exception MultipleTextOnlyColumns() { }
        // RVA: 0x0AD59F20  token: 0x6000182
        public static System.Exception InvalidSortString(System.String sort) { }
        // RVA: 0x0AD593A4  token: 0x6000183
        public static System.Exception DuplicateTableName(System.String table) { }
        // RVA: 0x0AD5934C  token: 0x6000184
        public static System.Exception DuplicateTableName2(System.String table, System.String ns) { }
        // RVA: 0x0AD5B46C  token: 0x6000185
        public static System.Exception SelfnestedDatasetConflictingName(System.String table) { }
        // RVA: 0x0AD58EB0  token: 0x6000186
        public static System.Exception DatasetConflictingName(System.String table) { }
        // RVA: 0x0AD5B750  token: 0x6000187
        public static System.Exception TableAlreadyInOtherDataSet() { }
        // RVA: 0x0AD5B784  token: 0x6000188
        public static System.Exception TableAlreadyInTheDataSet() { }
        // RVA: 0x0AD5B998  token: 0x6000189
        public static System.Exception TableOutOfRange(System.Int32 index) { }
        // RVA: 0x0AD5B950  token: 0x600018A
        public static System.Exception TableNotInTheDataSet(System.String table) { }
        // RVA: 0x0AD5B8D4  token: 0x600018B
        public static System.Exception TableInRelation() { }
        // RVA: 0x0AD5B868  token: 0x600018C
        public static System.Exception TableInConstraint(System.Data.DataTable table, System.Data.Constraint constraint) { }
        // RVA: 0x0AD57FB8  token: 0x600018D
        public static System.Exception CanNotSerializeDataTableHierarchy() { }
        // RVA: 0x0AD57F84  token: 0x600018E
        public static System.Exception CanNotRemoteDataTable() { }
        // RVA: 0x0AD58020  token: 0x600018F
        public static System.Exception CanNotSetRemotingFormat() { }
        // RVA: 0x0AD57FEC  token: 0x6000190
        public static System.Exception CanNotSerializeDataTableWithEmptyName() { }
        // RVA: 0x0AD5B908  token: 0x6000191
        public static System.Exception TableNotFound(System.String tableName) { }
        // RVA: 0x0AD57BE8  token: 0x6000192
        public static System.Exception AggregateException(System.Data.AggregateType aggregateType, System.Type type) { }
        // RVA: 0x0AD59F68  token: 0x6000193
        public static System.Exception InvalidStorageType(System.TypeCode typecode) { }
        // RVA: 0x0AD5AB18  token: 0x6000194
        public static System.Exception RangeArgument(System.Int32 min, System.Int32 max) { }
        // RVA: 0x0AD5A940  token: 0x6000195
        public static System.Exception NullRange() { }
        // RVA: 0x0AD5A68C  token: 0x6000196
        public static System.Exception NegativeMinimumCapacity() { }
        // RVA: 0x0AD5AA6C  token: 0x6000197
        public static System.Exception ProblematicChars(System.Char charValue) { }
        // RVA: 0x0AD5B71C  token: 0x6000198
        public static System.Exception StorageSetFailed() { }
        // RVA: 0x0AD5B6E8  token: 0x6000199
        public static System.Exception SimpleTypeNotSupported() { }
        // RVA: 0x0AD5A474  token: 0x600019A
        public static System.Exception MissingAttribute(System.String attribute) { }
        // RVA: 0x0AD5A41C  token: 0x600019B
        public static System.Exception MissingAttribute(System.String element, System.String attribute) { }
        // RVA: 0x0AD59B64  token: 0x600019C
        public static System.Exception InvalidAttributeValue(System.String name, System.String value) { }
        // RVA: 0x0AD57D20  token: 0x600019D
        public static System.Exception AttributeValues(System.String name, System.String value1, System.String value2) { }
        // RVA: 0x0AD59420  token: 0x600019E
        public static System.Exception ElementTypeNotFound(System.String name) { }
        // RVA: 0x0AD5AF9C  token: 0x600019F
        public static System.Exception RelationParentNameMissing(System.String rel) { }
        // RVA: 0x0AD5AD44  token: 0x60001A0
        public static System.Exception RelationChildNameMissing(System.String rel) { }
        // RVA: 0x0AD5AFE4  token: 0x60001A1
        public static System.Exception RelationTableKeyMissing(System.String rel) { }
        // RVA: 0x0AD5ACFC  token: 0x60001A2
        public static System.Exception RelationChildKeyMissing(System.String rel) { }
        // RVA: 0x0AD5BD20  token: 0x60001A3
        public static System.Exception UndefinedDatatype(System.String name) { }
        // RVA: 0x0AD58EF8  token: 0x60001A4
        public static System.Exception DatatypeNotDefined() { }
        // RVA: 0x0AD5A3E8  token: 0x60001A5
        public static System.Exception MismatchKeyLength() { }
        // RVA: 0x0AD59CAC  token: 0x60001A6
        public static System.Exception InvalidField(System.String name) { }
        // RVA: 0x0AD59ED8  token: 0x60001A7
        public static System.Exception InvalidSelector(System.String name) { }
        // RVA: 0x0AD5891C  token: 0x60001A8
        public static System.Exception CircularComplexType(System.String name) { }
        // RVA: 0x0AD58388  token: 0x60001A9
        public static System.Exception CannotInstantiateAbstract(System.String name) { }
        // RVA: 0x0AD59CF4  token: 0x60001AA
        public static System.Exception InvalidKey(System.String name) { }
        // RVA: 0x0AD5919C  token: 0x60001AB
        public static System.Exception DiffgramMissingTable(System.String name) { }
        // RVA: 0x0AD59168  token: 0x60001AC
        public static System.Exception DiffgramMissingSQL() { }
        // RVA: 0x0AD5922C  token: 0x60001AD
        public static System.Exception DuplicateConstraintRead(System.String str) { }
        // RVA: 0x0AD58AE0  token: 0x60001AE
        public static System.Exception ColumnTypeConflict(System.String name) { }
        // RVA: 0x0AD58330  token: 0x60001AF
        public static System.Exception CannotConvert(System.String name, System.String type) { }
        // RVA: 0x0AD5A4BC  token: 0x60001B0
        public static System.Exception MissingRefer(System.String name) { }
        // RVA: 0x0AD59DB8  token: 0x60001B1
        public static System.Exception InvalidPrefix(System.String name) { }
        // RVA: 0x0AD57F50  token: 0x60001B2
        public static System.Exception CanNotDeserializeObjectType() { }
        // RVA: 0x0AD59FDC  token: 0x60001B3
        public static System.Exception IsDataSetAttributeMissingInSchema() { }
        // RVA: 0x0AD5BB00  token: 0x60001B4
        public static System.Exception TooManyIsDataSetAtributeInSchema() { }
        // RVA: 0x0AD5A6C0  token: 0x60001B5
        public static System.Exception NestedCircular(System.String name) { }
        // RVA: 0x0AD5A52C  token: 0x60001B6
        public static System.Exception MultipleParentRows(System.String tableQName) { }
        // RVA: 0x0AD5AA24  token: 0x60001B7
        public static System.Exception PolymorphismNotSupported(System.String typeName) { }
        // RVA: 0x0AD58E7C  token: 0x60001B8
        public static System.Exception DataTableInferenceNotSupported() { }
        // RVA: 0x0AD5BAB0  token: 0x60001B9
        private static System.Void ThrowMultipleTargetConverter(System.Exception innerException) { }
        // RVA: 0x0AD592BC  token: 0x60001BA
        public static System.Exception DuplicateDeclaration(System.String name) { }
        // RVA: 0x0AD597E8  token: 0x60001BB
        public static System.Exception FoundEntity() { }
        // RVA: 0x0AD5A398  token: 0x60001BC
        public static System.Exception MergeFailed(System.String name) { }
        // RVA: 0x0AD58D7C  token: 0x60001BD
        public static System.Exception ConvertFailed(System.Type type1, System.Type type2) { }
        // RVA: 0x0AD59C54  token: 0x60001BE
        private static System.Exception InvalidDuplicateNamedSimpleTypeDelaration(System.String stName, System.String errorStr) { }
        // RVA: 0x0AD59AFC  token: 0x60001BF
        private static System.Exception InternalRBTreeError(System.Data.RBTreeError internalError) { }
        // RVA: 0x0AD594D0  token: 0x60001C0
        public static System.Exception EnumeratorModified() { }

    }

    // TypeToken: 0x2000018  // size: 0xB0
    public class DataSet : System.ComponentModel.MarshalByValueComponent, System.Xml.Serialization.IXmlSerializable, System.Runtime.Serialization.ISerializable
    {
        // Fields
        private System.Data.DataViewManager _defaultViewManager;  // 0x20
        private readonly System.Data.DataTableCollection _tableCollection;  // 0x28
        private readonly System.Data.DataRelationCollection _relationCollection;  // 0x30
        private System.Data.PropertyCollection _extendedProperties;  // 0x38
        private System.String _dataSetName;  // 0x40
        private System.String _datasetPrefix;  // 0x48
        private System.String _namespaceURI;  // 0x50
        private System.Boolean _enforceConstraints;  // 0x58
        private System.Boolean _caseSensitive;  // 0x59
        private System.Globalization.CultureInfo _culture;  // 0x60
        private System.Boolean _cultureUserSet;  // 0x68
        private System.Boolean _fInReadXml;  // 0x69
        private System.Boolean _fInLoadDiffgram;  // 0x6a
        private System.Boolean _fTopLevelTable;  // 0x6b
        private System.Boolean _fInitInProgress;  // 0x6c
        private System.Boolean _fEnableCascading;  // 0x6d
        private System.Boolean _fIsSchemaLoading;  // 0x6e
        private System.String _mainTableName;  // 0x70
        private System.Data.SerializationFormat _remotingFormat;  // 0x78
        private System.Object _defaultViewManagerLock;  // 0x80
        private static System.Int32 s_objectTypeCount;  // static @ 0x0
        private readonly System.Int32 _objectID;  // 0x88
        private static System.Xml.Schema.XmlSchemaComplexType s_schemaTypeForWSDL;  // static @ 0x8
        private System.Boolean _useDataSetSchemaOnly;  // 0x8c
        private System.Boolean _udtIsWrapped;  // 0x8d
        private System.ComponentModel.PropertyChangedEventHandler PropertyChanging;  // 0x90
        private System.Data.MergeFailedEventHandler MergeFailed;  // 0x98
        private System.Data.DataRowCreatedEventHandler DataRowCreated;  // 0xa0
        private System.Data.DataSetClearEventhandler ClearFunctionCalled;  // 0xa8

        // Properties
        System.Data.SerializationFormat RemotingFormat { get; /* RVA: 0x04D863C0 */ set; /* RVA: 0x0AD579EC */ }
        System.Data.SchemaSerializationMode SchemaSerializationMode { get; /* RVA: 0x04D85B80 */ }
        System.Boolean CaseSensitive { get; /* RVA: 0x04D8EF10 */ set; /* RVA: 0x0AD56E10 */ }
        System.Boolean EnforceConstraints { get; /* RVA: 0x04D86F70 */ set; /* RVA: 0x0AD5724C */ }
        System.String DataSetName { get; /* RVA: 0x04D85A60 */ set; /* RVA: 0x0AD570F4 */ }
        System.String Namespace { get; /* RVA: 0x04D86270 */ set; /* RVA: 0x0AD574EC */ }
        System.String Prefix { get; /* RVA: 0x04D86200 */ set; /* RVA: 0x0AD578E0 */ }
        System.Data.PropertyCollection ExtendedProperties { get; /* RVA: 0x0AD56D9C */ }
        System.Globalization.CultureInfo Locale { get; /* RVA: 0x04D86280 */ set; /* RVA: 0x0AD57390 */ }
        System.ComponentModel.ISite Site { get; /* RVA: 0x02B2ECC0 */ }
        System.Data.DataRelationCollection Relations { get; /* RVA: 0x011F36E0 */ }
        System.Data.DataTableCollection Tables { get; /* RVA: 0x04D86240 */ }
        System.String MainTableName { get; /* RVA: 0x04D862B0 */ set; /* RVA: 0x063AD760 */ }
        System.Int32 ObjectID { get; /* RVA: 0x04D86890 */ }

        // Methods
        // RVA: 0x0AD56970  token: 0x60001C1
        public System.Void .ctor() { }
        // RVA: 0x0AD56D6C  token: 0x60001C2
        public System.Void .ctor(System.String dataSetName) { }
        // RVA: 0x0AD56D44  token: 0x60001C6
        protected System.Void .ctor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x0AD56BD0  token: 0x60001C7
        protected System.Void .ctor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context, System.Boolean ConstructSchema) { }
        // RVA: 0x0AD4FF0C  token: 0x60001C8
        public virtual System.Void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x041E1670  token: 0x60001C9
        protected virtual System.Void InitializeDerivedDataSet() { }
        // RVA: 0x0AD54598  token: 0x60001CA
        private System.Void SerializeDataSet(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context, System.Data.SerializationFormat remotingFormat) { }
        // RVA: 0x0AD4F0A8  token: 0x60001CB
        private System.Void DeserializeDataSet(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context, System.Data.SerializationFormat remotingFormat, System.Data.SchemaSerializationMode schemaSerializationMode) { }
        // RVA: 0x0AD4EC58  token: 0x60001CC
        private System.Void DeserializeDataSetSchema(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context, System.Data.SerializationFormat remotingFormat, System.Data.SchemaSerializationMode schemaSerializationMode) { }
        // RVA: 0x0AD4E868  token: 0x60001CD
        private System.Void DeserializeDataSetData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context, System.Data.SerializationFormat remotingFormat) { }
        // RVA: 0x0AD5445C  token: 0x60001CE
        private System.Void SerializeDataSetProperties(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x0AD4EA14  token: 0x60001CF
        private System.Void DeserializeDataSetProperties(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x0AD54B08  token: 0x60001D0
        private System.Void SerializeRelations(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x0AD4F120  token: 0x60001D1
        private System.Void DeserializeRelations(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x0AD4FC5C  token: 0x60001D2
        private System.Void FailedEnableConstraints() { }
        // RVA: 0x04D86F80  token: 0x60001D7
        private System.Void RestoreEnforceConstraints(System.Boolean value) { }
        // RVA: 0x0AD4F6A8  token: 0x60001D8
        private System.Void EnableConstraints() { }
        // RVA: 0x0AD55104  token: 0x60001E2
        private System.Void SetLocaleValue(System.Globalization.CultureInfo value, System.Boolean userSet) { }
        // RVA: 0x04D86B40  token: 0x60001E3
        private System.Boolean ShouldSerializeLocale() { }
        // RVA: 0x0AD4DB74  token: 0x60001E7
        public System.Void Clear() { }
        // RVA: 0x0AD4DD0C  token: 0x60001E8
        public virtual System.Data.DataSet Clone() { }
        // RVA: 0x0AD4FB2C  token: 0x60001E9
        private System.Int32 EstimatedXmlStringSize() { }
        // RVA: 0x0AD4FF38  token: 0x60001EA
        private System.String GetRemotingDiffGram(System.Data.DataTable table) { }
        // RVA: 0x0AD50058  token: 0x60001EB
        private System.String GetXmlSchemaForRemoting(System.Data.DataTable table) { }
        // RVA: 0x0AD51C18  token: 0x60001EC
        public System.Void ReadXmlSchema(System.Xml.XmlReader reader) { }
        // RVA: 0x0AD51C24  token: 0x60001ED
        private System.Void ReadXmlSchema(System.Xml.XmlReader reader, System.Boolean denyResolving) { }
        // RVA: 0x0AD3BF38  token: 0x60001EE
        private System.Boolean MoveToElement(System.Xml.XmlReader reader, System.Int32 depth) { }
        // RVA: 0x0AD509C4  token: 0x60001EF
        private static System.Void MoveToElement(System.Xml.XmlReader reader) { }
        // RVA: 0x0AD3D628  token: 0x60001F0
        private System.Void ReadEndElement(System.Xml.XmlReader reader) { }
        // RVA: 0x0AD50D60  token: 0x60001F1
        private System.Void ReadXSDSchema(System.Xml.XmlReader reader, System.Boolean denyResolving) { }
        // RVA: 0x0AD50C44  token: 0x60001F2
        private System.Void ReadXDRSchema(System.Xml.XmlReader reader) { }
        // RVA: 0x0AD56518  token: 0x60001F3
        private System.Void WriteXmlSchema(System.Xml.XmlWriter writer, System.Data.SchemaFormat schemaFormat, System.Converter<System.Type,System.String> multipleTargetConverter) { }
        // RVA: 0x0AD5422C  token: 0x60001F4
        public System.Data.XmlReadMode ReadXml(System.Xml.XmlReader reader) { }
        // RVA: 0x0AD53270  token: 0x60001F5
        private System.Data.XmlReadMode ReadXml(System.Xml.XmlReader reader, System.Boolean denyResolving) { }
        // RVA: 0x0AD501B4  token: 0x60001F6
        private System.Void InferSchema(System.Xml.XmlDocument xdoc, System.String[] excludedNamespaces, System.Data.XmlReadMode mode) { }
        // RVA: 0x0AD504A0  token: 0x60001F7
        private System.Boolean IsEmpty() { }
        // RVA: 0x0AD50F90  token: 0x60001F8
        private System.Void ReadXmlDiffgram(System.Xml.XmlReader reader) { }
        // RVA: 0x0AD52634  token: 0x60001F9
        public System.Data.XmlReadMode ReadXml(System.Xml.XmlReader reader, System.Data.XmlReadMode mode) { }
        // RVA: 0x0AD52650  token: 0x60001FA
        private System.Data.XmlReadMode ReadXml(System.Xml.XmlReader reader, System.Data.XmlReadMode mode, System.Boolean denyResolving) { }
        // RVA: 0x0AD566B4  token: 0x60001FB
        public System.Void WriteXml(System.Xml.XmlWriter writer, System.Data.XmlWriteMode mode) { }
        // RVA: 0x0AD50630  token: 0x60001FC
        public System.Void Merge(System.Data.DataSet dataSet) { }
        // RVA: 0x0AD50790  token: 0x60001FD
        public System.Void Merge(System.Data.DataSet dataSet, System.Boolean preserveChanges, System.Data.MissingSchemaAction missingSchemaAction) { }
        // RVA: 0x0AD50AC8  token: 0x60001FE
        protected virtual System.Void OnPropertyChanging(System.ComponentModel.PropertyChangedEventArgs pcevent) { }
        // RVA: 0x0AD50A64  token: 0x60001FF
        private System.Void OnMergeFailed(System.Data.MergeFailedEventArgs mfevent) { }
        // RVA: 0x0AD50B18  token: 0x6000200
        private System.Void RaiseMergeFailed(System.Data.DataTable table, System.String conflict, System.Data.MissingSchemaAction missingSchemaAction) { }
        // RVA: 0x0AD50A44  token: 0x6000201
        private System.Void OnDataRowCreated(System.Data.DataRow row) { }
        // RVA: 0x0AD50A24  token: 0x6000202
        private System.Void OnClearFunctionCalled(System.Data.DataTable table) { }
        // RVA: 0x041E1670  token: 0x6000203
        protected internal virtual System.Void OnRemoveTable(System.Data.DataTable table) { }
        // RVA: 0x0AD50AE8  token: 0x6000204
        private System.Void OnRemovedTable(System.Data.DataTable table) { }
        // RVA: 0x041E1670  token: 0x6000205
        protected virtual System.Void OnRemoveRelation(System.Data.DataRelation relation) { }
        // RVA: 0x041E1670  token: 0x6000206
        private System.Void OnRemoveRelationHack(System.Data.DataRelation relation) { }
        // RVA: 0x0AD50BCC  token: 0x6000207
        protected internal System.Void RaisePropertyChanging(System.String name) { }
        // RVA: 0x0AD55C50  token: 0x6000208
        private System.Data.DataTable[] TopLevelTables() { }
        // RVA: 0x0AD55C5C  token: 0x6000209
        private System.Data.DataTable[] TopLevelTables(System.Boolean forSchema) { }
        // RVA: 0x0AD54238  token: 0x600020A
        public virtual System.Void Reset() { }
        // RVA: 0x0AD55E30  token: 0x600020B
        private System.Boolean ValidateCaseConstraint() { }
        // RVA: 0x0AD5617C  token: 0x600020C
        private System.Boolean ValidateLocaleConstraint() { }
        // RVA: 0x0AD52350  token: 0x600020D
        protected virtual System.Void ReadXmlSerializable(System.Xml.XmlReader reader) { }
        // RVA: 0x0AD4FC90  token: 0x600020E
        public static System.Xml.Schema.XmlSchemaComplexType GetDataSetSchema(System.Xml.Schema.XmlSchemaSet schemaSet) { }
        // RVA: 0x0AD55950  token: 0x600020F
        private virtual System.Xml.Schema.XmlSchema System.Xml.Serialization.IXmlSerializable.GetSchema() { }
        // RVA: 0x0AD55AEC  token: 0x6000210
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader reader) { }
        // RVA: 0x0AD55C08  token: 0x6000211
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter writer) { }

    }

    // TypeToken: 0x2000019  // size: 0x228
    public class DataTable : System.ComponentModel.MarshalByValueComponent, System.Runtime.Serialization.ISerializable, System.Xml.Serialization.IXmlSerializable
    {
        // Fields
        private System.Data.DataSet _dataSet;  // 0x20
        private System.Data.DataView _defaultView;  // 0x28
        private System.Int64 _nextRowID;  // 0x30
        private readonly System.Data.DataRowCollection _rowCollection;  // 0x38
        private readonly System.Data.DataColumnCollection _columnCollection;  // 0x40
        private readonly System.Data.ConstraintCollection _constraintCollection;  // 0x48
        private System.Int32 _elementColumnCount;  // 0x50
        private System.Data.DataRelationCollection _parentRelationsCollection;  // 0x58
        private System.Data.DataRelationCollection _childRelationsCollection;  // 0x60
        private readonly System.Data.RecordManager _recordManager;  // 0x68
        private readonly System.Collections.Generic.List<System.Data.Index> _indexes;  // 0x70
        private System.Collections.Generic.List<System.Data.Index> _shadowIndexes;  // 0x78
        private System.Int32 _shadowCount;  // 0x80
        private System.Data.PropertyCollection _extendedProperties;  // 0x88
        private System.String _tableName;  // 0x90
        private System.String _tableNamespace;  // 0x98
        private System.String _tablePrefix;  // 0xa0
        private System.Data.DataExpression _displayExpression;  // 0xa8
        private System.Boolean _fNestedInDataset;  // 0xb0
        private System.Globalization.CultureInfo _culture;  // 0xb8
        private System.Boolean _cultureUserSet;  // 0xc0
        private System.Globalization.CompareInfo _compareInfo;  // 0xc8
        private System.Globalization.CompareOptions _compareFlags;  // 0xd0
        private System.IFormatProvider _formatProvider;  // 0xd8
        private System.StringComparer _hashCodeProvider;  // 0xe0
        private System.Boolean _caseSensitive;  // 0xe8
        private System.Boolean _caseSensitiveUserSet;  // 0xe9
        private System.String _encodedTableName;  // 0xf0
        private System.Data.DataColumn _xmlText;  // 0xf8
        private System.Data.DataColumn _colUnique;  // 0x100
        private System.Decimal _minOccurs;  // 0x108
        private System.Decimal _maxOccurs;  // 0x118
        private System.Boolean _repeatableElement;  // 0x128
        private System.Object _typeName;  // 0x130
        private System.Data.UniqueConstraint _primaryKey;  // 0x138
        private System.Data.IndexField[] _primaryIndex;  // 0x140
        private System.Data.DataColumn[] _delayedSetPrimaryKey;  // 0x148
        private System.Data.Index _loadIndex;  // 0x150
        private System.Data.Index _loadIndexwithOriginalAdded;  // 0x158
        private System.Data.Index _loadIndexwithCurrentDeleted;  // 0x160
        private System.Int32 _suspendIndexEvents;  // 0x168
        private System.Boolean _inDataLoad;  // 0x16c
        private System.Boolean _schemaLoading;  // 0x16d
        private System.Boolean _enforceConstraints;  // 0x16e
        private System.Boolean _suspendEnforceConstraints;  // 0x16f
        protected internal System.Boolean fInitInProgress;  // 0x170
        private System.Boolean _inLoad;  // 0x171
        private System.Boolean _fInLoadDiffgram;  // 0x172
        private System.Byte _isTypedDataTable;  // 0x173
        private System.Data.DataRow[] _emptyDataRowArray;  // 0x178
        private System.ComponentModel.PropertyDescriptorCollection _propertyDescriptorCollectionCache;  // 0x180
        private System.Data.DataRelation[] _nestedParentRelations;  // 0x188
        private System.Collections.Generic.List<System.Data.DataColumn> _dependentColumns;  // 0x190
        private System.Boolean _mergingData;  // 0x198
        private System.Data.DataRowChangeEventHandler _onRowChangedDelegate;  // 0x1a0
        private System.Data.DataRowChangeEventHandler _onRowChangingDelegate;  // 0x1a8
        private System.Data.DataRowChangeEventHandler _onRowDeletingDelegate;  // 0x1b0
        private System.Data.DataRowChangeEventHandler _onRowDeletedDelegate;  // 0x1b8
        private System.Data.DataColumnChangeEventHandler _onColumnChangedDelegate;  // 0x1c0
        private System.Data.DataColumnChangeEventHandler _onColumnChangingDelegate;  // 0x1c8
        private System.Data.DataTableClearEventHandler _onTableClearingDelegate;  // 0x1d0
        private System.Data.DataTableClearEventHandler _onTableClearedDelegate;  // 0x1d8
        private System.Data.DataTableNewRowEventHandler _onTableNewRowDelegate;  // 0x1e0
        private System.ComponentModel.PropertyChangedEventHandler _onPropertyChangingDelegate;  // 0x1e8
        private readonly System.Data.DataRowBuilder _rowBuilder;  // 0x1f0
        private readonly System.Collections.Generic.List<System.Data.DataView> _delayedViews;  // 0x1f8
        private readonly System.Collections.Generic.List<System.Data.DataViewListener> _dataViewListeners;  // 0x200
        private System.Collections.Hashtable _rowDiffId;  // 0x208
        private readonly System.Threading.ReaderWriterLockSlim _indexesLock;  // 0x210
        private System.Int32 _ukColumnPositionForInference;  // 0x218
        private System.Data.SerializationFormat _remotingFormat;  // 0x21c
        private static System.Int32 s_objectTypeCount;  // static @ 0x0
        private readonly System.Int32 _objectID;  // 0x220

        // Properties
        System.Boolean CaseSensitive { get; /* RVA: 0x04D8C8C0 */ set; /* RVA: 0x0AD460C0 */ }
        System.Boolean AreIndexEventsSuspended { get; /* RVA: 0x0AD454FC */ }
        System.Boolean IsTypedDataTable { get; /* RVA: 0x0AD457B8 */ }
        System.Boolean SelfNested { get; /* RVA: 0x0AD45DAC */ }
        System.Collections.Generic.List<System.Data.Index> LiveIndexes { get; /* RVA: 0x0AD45820 */ }
        System.Data.SerializationFormat RemotingFormat { get; /* RVA: 0x04DAA120 */ set; /* RVA: 0x0AD46E34 */ }
        System.Int32 UKColumnPositionForInference { get; /* RVA: 0x04DAA150 */ set; /* RVA: 0x04DAA190 */ }
        System.Data.DataRelationCollection ChildRelations { get; /* RVA: 0x0AD45508 */ }
        System.Data.DataColumnCollection Columns { get; /* RVA: 0x04D85A60 */ }
        System.Globalization.CompareInfo CompareInfo { get; /* RVA: 0x0AD45580 */ }
        System.Data.ConstraintCollection Constraints { get; /* RVA: 0x04D86200 */ }
        System.Data.DataSet DataSet { get; /* RVA: 0x04D862C0 */ }
        System.String DisplayExpressionInternal { get; /* RVA: 0x0AD455D0 */ }
        System.Boolean EnforceConstraints { get; /* RVA: 0x0AD456A4 */ set; /* RVA: 0x0AD461C0 */ }
        System.Boolean SuspendEnforceConstraints { get; /* RVA: 0x04DAA140 */ set; /* RVA: 0x04DAA180 */ }
        System.Data.PropertyCollection ExtendedProperties { get; /* RVA: 0x0AD456C8 */ }
        System.IFormatProvider FormatProvider { get; /* RVA: 0x0AD45744 */ }
        System.Globalization.CultureInfo Locale { get; /* RVA: 0x04D85EF0 */ set; /* RVA: 0x0AD461FC */ }
        System.Int32 MinimumCapacity { get; /* RVA: 0x0AD458B4 */ set; /* RVA: 0x0AD46678 */ }
        System.Int32 RecordCapacity { get; /* RVA: 0x0AD45D14 */ }
        System.Int32 ElementColumnCount { get; /* RVA: 0x04D86550 */ set; /* RVA: 0x0AD46178 */ }
        System.Data.DataRelationCollection ParentRelations { get; /* RVA: 0x0AD45C3C */ }
        System.Boolean MergingData { get; /* RVA: 0x04D91350 */ set; /* RVA: 0x04D913A0 */ }
        System.Data.DataRelation[] NestedParentRelations { get; /* RVA: 0x04D8DA20 */ }
        System.Boolean SchemaLoading { get; /* RVA: 0x04DAA130 */ }
        System.Int32 NestedParentsCount { get; /* RVA: 0x0AD45988 */ }
        System.Data.DataColumn[] PrimaryKey { get; /* RVA: 0x0AD45CB4 */ set; /* RVA: 0x0AD46A9C */ }
        System.Data.DataRowCollection Rows { get; /* RVA: 0x04D85A50 */ }
        System.String TableName { get; /* RVA: 0x04D86220 */ set; /* RVA: 0x0AD46EA8 */ }
        System.String EncodedTableName { get; /* RVA: 0x0AD4562C */ }
        System.String Namespace { get; /* RVA: 0x0AD458D0 */ set; /* RVA: 0x0AD466A4 */ }
        System.String Prefix { get; /* RVA: 0x04D86210 */ set; /* RVA: 0x0AD4694C */ }
        System.Data.DataColumn XmlText { get; /* RVA: 0x04D877C0 */ set; /* RVA: 0x0AD4749C */ }
        System.Decimal MaxOccurs { get; /* RVA: 0x04DAA100 */ set; /* RVA: 0x04DAA160 */ }
        System.Decimal MinOccurs { get; /* RVA: 0x04DAA110 */ set; /* RVA: 0x04DAA170 */ }
        System.ComponentModel.ISite Site { get; /* RVA: 0x02B2ECC0 */ }
        System.Boolean NeedColumnChangeEvents { get; /* RVA: 0x0AD45954 */ }
        System.Xml.XmlQualifiedName TypeName { get; /* RVA: 0x0AD46054 */ set; /* RVA: 0x05397198 */ }
        System.Collections.Hashtable RowDiffId { get; /* RVA: 0x0AD45D30 */ }
        System.Int32 ObjectID { get; /* RVA: 0x04D902A0 */ }

        // Methods
        // RVA: 0x0AD44E78  token: 0x6000215
        public System.Void .ctor() { }
        // RVA: 0x0AD45334  token: 0x6000216
        public System.Void .ctor(System.String tableName) { }
        // RVA: 0x0AD453A0  token: 0x6000217
        protected System.Void .ctor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x0AD39F2C  token: 0x6000218
        public virtual System.Void GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x0AD4156C  token: 0x6000219
        private System.Void SerializeDataTable(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context, System.Boolean isSingleTable, System.Data.SerializationFormat remotingFormat) { }
        // RVA: 0x0AD35EEC  token: 0x600021A
        private System.Void DeserializeDataTable(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context, System.Boolean isSingleTable, System.Data.SerializationFormat remotingFormat) { }
        // RVA: 0x0AD41F18  token: 0x600021B
        private System.Void SerializeTableSchema(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context, System.Boolean isSingleTable) { }
        // RVA: 0x0AD36B94  token: 0x600021C
        private System.Void DeserializeTableSchema(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context, System.Boolean isSingleTable) { }
        // RVA: 0x0AD40FD8  token: 0x600021D
        private System.Void SerializeConstraints(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context, System.Int32 serIndex, System.Boolean allConstraints) { }
        // RVA: 0x0AD355D4  token: 0x600021E
        private System.Void DeserializeConstraints(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context, System.Int32 serIndex, System.Boolean allConstraints) { }
        // RVA: 0x0AD41848  token: 0x600021F
        private System.Void SerializeExpressionColumns(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context, System.Int32 serIndex) { }
        // RVA: 0x0AD36234  token: 0x6000220
        private System.Void DeserializeExpressionColumns(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context, System.Int32 serIndex) { }
        // RVA: 0x0AD41980  token: 0x6000221
        private System.Void SerializeTableData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context, System.Int32 serIndex) { }
        // RVA: 0x0AD3637C  token: 0x6000222
        private System.Void DeserializeTableData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context, System.Int32 serIndex) { }
        // RVA: 0x0AD34D04  token: 0x6000223
        private System.Data.DataRowState ConvertToRowState(System.Collections.BitArray bitStates, System.Int32 bitIndex) { }
        // RVA: 0x0AD3A200  token: 0x6000224
        private System.Void GetRowAndColumnErrors(System.Int32 rowIndex, System.Collections.Hashtable rowErrors, System.Collections.Hashtable colErrors) { }
        // RVA: 0x0AD34ADC  token: 0x6000225
        private System.Void ConvertToRowError(System.Int32 rowIndex, System.Collections.Hashtable rowErrors, System.Collections.Hashtable colErrors) { }
        // RVA: 0x0AD40B4C  token: 0x6000229
        private System.Void RestoreIndexEvents(System.Boolean forceReset) { }
        // RVA: 0x0AD4457C  token: 0x600022A
        private System.Void SuspendIndexEvents() { }
        // RVA: 0x0AD42B20  token: 0x600022C
        private System.Boolean SetCaseSensitiveValue(System.Boolean isCaseSensitive, System.Boolean userSet, System.Boolean resetIndexes) { }
        // RVA: 0x04DAA0F0  token: 0x600022D
        private System.Boolean ShouldSerializeCaseSensitive() { }
        // RVA: 0x0AD40780  token: 0x6000238
        private System.Void ResetConstraints() { }
        // RVA: 0x0AD42DF8  token: 0x600023A
        private System.Void SetDataSet(System.Data.DataSet dataSet) { }
        // RVA: 0x0AD37BA4  token: 0x6000240
        private System.Void EnableConstraints() { }
        // RVA: 0x0AD42F14  token: 0x6000245
        private System.Boolean SetLocaleValue(System.Globalization.CultureInfo culture, System.Boolean userSet, System.Boolean resetIndexes) { }
        // RVA: 0x04D91920  token: 0x6000246
        private System.Boolean ShouldSerializeLocale() { }
        // RVA: 0x0AD324DC  token: 0x6000251
        private System.Void CacheNestedParent() { }
        // RVA: 0x0AD3946C  token: 0x6000252
        private System.Data.DataRelation[] FindNestedParentRelations() { }
        // RVA: 0x0AD39D48  token: 0x600025A
        private System.String GetInheritedNamespace(System.Collections.Generic.List<System.Data.DataTable> visitedTables) { }
        // RVA: 0x0AD3B578  token: 0x600025D
        private System.Boolean IsNamespaceInherited() { }
        // RVA: 0x0AD325CC  token: 0x600025E
        private System.Void CheckCascadingNamespaceConflict(System.String realNamespace) { }
        // RVA: 0x0AD32F5C  token: 0x600025F
        private System.Void CheckNamespaceValidityForNestedRelations(System.String realNamespace) { }
        // RVA: 0x0AD32D54  token: 0x6000260
        private System.Void CheckNamespaceValidityForNestedParentRelations(System.String ns, System.Data.DataTable parentTable) { }
        // RVA: 0x0AD37864  token: 0x6000261
        private System.Void DoRaiseNamespaceChange() { }
        // RVA: 0x0AD42E98  token: 0x600026A
        private System.Void SetKeyValues(System.Data.DataKey key, System.Object[] keyValues, System.Int32 record) { }
        // RVA: 0x0AD39390  token: 0x600026B
        private System.Data.DataRow FindByIndex(System.Data.Index ndx, System.Object[] key) { }
        // RVA: 0x0AD3940C  token: 0x600026C
        private System.Data.DataRow FindMergeTarget(System.Data.DataRow row, System.Data.DataKey key, System.Data.Index ndx) { }
        // RVA: 0x0AD4347C  token: 0x600026D
        private System.Void SetMergeRecords(System.Data.DataRow row, System.Int32 newRecord, System.Int32 oldRecord, System.Data.DataRowAction action) { }
        // RVA: 0x0AD3B5DC  token: 0x600026E
        private System.Data.DataRow MergeRow(System.Data.DataRow row, System.Data.DataRow targetRow, System.Boolean preserveChanges, System.Data.Index idxSearch) { }
        // RVA: 0x0AD34FE0  token: 0x600026F
        protected virtual System.Data.DataTable CreateInstance() { }
        // RVA: 0x0AD3485C  token: 0x6000270
        public virtual System.Data.DataTable Clone() { }
        // RVA: 0x0AD346A0  token: 0x6000271
        private System.Data.DataTable Clone(System.Data.DataSet cloneDS) { }
        // RVA: 0x0AD3A6D0  token: 0x6000272
        private System.Data.DataTable IncrementalCloneTo(System.Data.DataTable sourceTable, System.Data.DataTable targetTable) { }
        // RVA: 0x0AD33834  token: 0x6000273
        private System.Data.DataTable CloneHierarchy(System.Data.DataTable sourceTable, System.Data.DataSet ds, System.Collections.Hashtable visitedMap) { }
        // RVA: 0x0AD33BC0  token: 0x6000274
        private System.Data.DataTable CloneTo(System.Data.DataTable clone, System.Data.DataSet cloneDS, System.Boolean skipExpressionColumns) { }
        // RVA: 0x0AD32234  token: 0x6000276
        private System.Void AddRow(System.Data.DataRow row, System.Int32 proposedID) { }
        // RVA: 0x0AD3B3B4  token: 0x6000277
        private System.Void InsertRow(System.Data.DataRow row, System.Int32 proposedID, System.Int32 pos) { }
        // RVA: 0x0AD3AF04  token: 0x6000278
        private System.Void InsertRow(System.Data.DataRow row, System.Int64 proposedID, System.Int32 pos, System.Boolean fireEvent) { }
        // RVA: 0x0AD331FC  token: 0x6000279
        private System.Void CheckNotModifying(System.Data.DataRow row) { }
        // RVA: 0x0AD33228  token: 0x600027A
        public System.Void Clear() { }
        // RVA: 0x0AD33234  token: 0x600027B
        private System.Void Clear(System.Boolean clearAll) { }
        // RVA: 0x0AD32504  token: 0x600027C
        private System.Void CascadeAll(System.Data.DataRow row, System.Data.DataRowAction action) { }
        // RVA: 0x0AD34868  token: 0x600027D
        private System.Void CommitRow(System.Data.DataRow row) { }
        // RVA: 0x0AD34A38  token: 0x600027E
        private System.Int32 Compare(System.String s1, System.String s2) { }
        // RVA: 0x0AD348F8  token: 0x600027F
        private System.Int32 Compare(System.String s1, System.String s2, System.Globalization.CompareInfo comparer) { }
        // RVA: 0x0AD3A8C4  token: 0x6000280
        private System.Int32 IndexOf(System.String s1, System.String s2) { }
        // RVA: 0x0AD3B584  token: 0x6000281
        private System.Boolean IsSuffix(System.String s1, System.String s2) { }
        // RVA: 0x0AD34A54  token: 0x6000282
        public System.Object Compute(System.String expression, System.String filter) { }
        // RVA: 0x0AD35570  token: 0x6000283
        private System.Void DeleteRow(System.Data.DataRow row) { }
        // RVA: 0x0AD396C8  token: 0x6000284
        private System.String FormatSortString(System.Data.IndexField[] indexDesc) { }
        // RVA: 0x0AD397C4  token: 0x6000285
        private System.Void FreeRecord(System.Int32& record) { }
        // RVA: 0x0AD39CFC  token: 0x6000286
        private System.Data.Index GetIndex(System.String sort, System.Data.DataViewRowState recordStates, System.Data.IFilter rowFilter) { }
        // RVA: 0x0AD39A00  token: 0x6000287
        private System.Data.Index GetIndex(System.Data.IndexField[] indexDesc, System.Data.DataViewRowState recordStates, System.Data.IFilter rowFilter) { }
        // RVA: 0x04D8DA40  token: 0x6000288
        private System.Collections.Generic.List<System.Data.DataViewListener> GetListeners() { }
        // RVA: 0x0AD3A5FC  token: 0x6000289
        private System.Int32 GetSpecialHashCode(System.String name) { }
        // RVA: 0x0AD3AA90  token: 0x600028A
        private System.Void InsertRow(System.Data.DataRow row, System.Int64 proposedID) { }
        // RVA: 0x0AD3C024  token: 0x600028B
        private System.Int32 NewRecord() { }
        // RVA: 0x0AD3C39C  token: 0x600028C
        private System.Int32 NewUninitializedRecord() { }
        // RVA: 0x0AD3C034  token: 0x600028D
        private System.Int32 NewRecord(System.Int32 sourceRecord) { }
        // RVA: 0x0AD3BFC8  token: 0x600028E
        private System.Data.DataRow NewEmptyRow() { }
        // RVA: 0x0AD3C3BC  token: 0x600028F
        private System.Data.DataRow NewUninitializedRow() { }
        // RVA: 0x0AD3C360  token: 0x6000290
        public System.Data.DataRow NewRow() { }
        // RVA: 0x0AD34DAC  token: 0x6000291
        private System.Data.DataRow CreateEmptyRow() { }
        // RVA: 0x0AD3C1EC  token: 0x6000292
        private System.Void NewRowCreated(System.Data.DataRow row) { }
        // RVA: 0x0AD3C2D0  token: 0x6000293
        private System.Data.DataRow NewRow(System.Int32 record) { }
        // RVA: 0x0AD3C270  token: 0x6000294
        protected virtual System.Data.DataRow NewRowFromBuilder(System.Data.DataRowBuilder builder) { }
        // RVA: 0x0AD3A410  token: 0x6000295
        protected virtual System.Type GetRowType() { }
        // RVA: 0x0AD3C114  token: 0x6000296
        protected internal System.Data.DataRow[] NewRowArray(System.Int32 size) { }
        // RVA: 0x0AD3C498  token: 0x6000298
        protected internal virtual System.Void OnColumnChanging(System.Data.DataColumnChangeEventArgs e) { }
        // RVA: 0x0AD3C3E0  token: 0x6000299
        protected internal virtual System.Void OnColumnChanged(System.Data.DataColumnChangeEventArgs e) { }
        // RVA: 0x0AD3C550  token: 0x600029A
        protected virtual System.Void OnPropertyChanging(System.ComponentModel.PropertyChangedEventArgs pcevent) { }
        // RVA: 0x041E1670  token: 0x600029B
        private System.Void OnRemoveColumnInternal(System.Data.DataColumn column) { }
        // RVA: 0x041E1670  token: 0x600029C
        protected virtual System.Void OnRemoveColumn(System.Data.DataColumn column) { }
        // RVA: 0x0AD3C608  token: 0x600029D
        private System.Data.DataRowChangeEventArgs OnRowChanged(System.Data.DataRowChangeEventArgs args, System.Data.DataRow eRow, System.Data.DataRowAction eAction) { }
        // RVA: 0x0AD3C76C  token: 0x600029E
        private System.Data.DataRowChangeEventArgs OnRowChanging(System.Data.DataRowChangeEventArgs args, System.Data.DataRow eRow, System.Data.DataRowAction eAction) { }
        // RVA: 0x0AD3C6B4  token: 0x600029F
        protected virtual System.Void OnRowChanged(System.Data.DataRowChangeEventArgs e) { }
        // RVA: 0x0AD3C818  token: 0x60002A0
        protected virtual System.Void OnRowChanging(System.Data.DataRowChangeEventArgs e) { }
        // RVA: 0x0AD3C988  token: 0x60002A1
        protected virtual System.Void OnRowDeleting(System.Data.DataRowChangeEventArgs e) { }
        // RVA: 0x0AD3C8D0  token: 0x60002A2
        protected virtual System.Void OnRowDeleted(System.Data.DataRowChangeEventArgs e) { }
        // RVA: 0x0AD3CA40  token: 0x60002A3
        protected virtual System.Void OnTableCleared(System.Data.DataTableClearEventArgs e) { }
        // RVA: 0x0AD3CAF8  token: 0x60002A4
        protected virtual System.Void OnTableClearing(System.Data.DataTableClearEventArgs e) { }
        // RVA: 0x0AD3CBB0  token: 0x60002A5
        protected virtual System.Void OnTableNewRow(System.Data.DataTableNewRowEventArgs e) { }
        // RVA: 0x0AD3CC68  token: 0x60002A6
        private System.Data.IndexField[] ParseSortString(System.String sortString) { }
        // RVA: 0x0AD3CF14  token: 0x60002A7
        private System.Void RaisePropertyChanging(System.String name) { }
        // RVA: 0x0AD3FE3C  token: 0x60002A8
        private System.Void RecordChanged(System.Int32 record) { }
        // RVA: 0x0AD3FF54  token: 0x60002A9
        private System.Void RecordChanged(System.Int32[] oldIndex, System.Int32[] newIndex) { }
        // RVA: 0x0AD40270  token: 0x60002AA
        private System.Void RecordStateChanged(System.Int32 record, System.Data.DataViewRowState oldState, System.Data.DataViewRowState newState) { }
        // RVA: 0x0AD400B8  token: 0x60002AB
        private System.Void RecordStateChanged(System.Int32 record1, System.Data.DataViewRowState oldState1, System.Data.DataViewRowState newState1, System.Int32 record2, System.Data.DataViewRowState oldState2, System.Data.DataViewRowState newState2) { }
        // RVA: 0x0AD40430  token: 0x60002AC
        private System.Int32[] RemoveRecordFromIndexes(System.Data.DataRow row, System.Data.DataRowVersion version) { }
        // RVA: 0x0AD3A91C  token: 0x60002AD
        private System.Int32[] InsertRecordToIndexes(System.Data.DataRow row, System.Data.DataRowVersion version) { }
        // RVA: 0x0AD443F0  token: 0x60002AE
        private System.Void SilentlySetValue(System.Data.DataRow dr, System.Data.DataColumn dc, System.Data.DataRowVersion version, System.Object newValue) { }
        // RVA: 0x0AD405E4  token: 0x60002AF
        private System.Void RemoveRow(System.Data.DataRow row, System.Boolean check) { }
        // RVA: 0x0AD40930  token: 0x60002B0
        public virtual System.Void Reset() { }
        // RVA: 0x0AD407A0  token: 0x60002B1
        private System.Void ResetIndexes() { }
        // RVA: 0x0AD407AC  token: 0x60002B2
        private System.Void ResetInternalIndexes(System.Data.DataColumn column) { }
        // RVA: 0x0AD40EB0  token: 0x60002B3
        private System.Void RollbackRow(System.Data.DataRow row) { }
        // RVA: 0x0AD3CF8C  token: 0x60002B4
        private System.Data.DataRowChangeEventArgs RaiseRowChanged(System.Data.DataRowChangeEventArgs args, System.Data.DataRow eRow, System.Data.DataRowAction eAction) { }
        // RVA: 0x0AD3D2F0  token: 0x60002B5
        private System.Data.DataRowChangeEventArgs RaiseRowChanging(System.Data.DataRowChangeEventArgs args, System.Data.DataRow eRow, System.Data.DataRowAction eAction) { }
        // RVA: 0x0AD3D164  token: 0x60002B6
        private System.Data.DataRowChangeEventArgs RaiseRowChanging(System.Data.DataRowChangeEventArgs args, System.Data.DataRow eRow, System.Data.DataRowAction eAction, System.Boolean fireEvent) { }
        // RVA: 0x0AD40F0C  token: 0x60002B7
        public System.Data.DataRow[] Select(System.String filterExpression, System.String sort, System.Data.DataViewRowState recordStates) { }
        // RVA: 0x0AD43FC8  token: 0x60002B8
        private System.Void SetNewRecord(System.Data.DataRow row, System.Int32 proposedRecord, System.Data.DataRowAction action, System.Boolean isInMerge, System.Boolean fireEvent, System.Boolean suppressEnsurePropertyChanged) { }
        // RVA: 0x0AD43524  token: 0x60002B9
        private System.Void SetNewRecordWorker(System.Data.DataRow row, System.Int32 proposedRecord, System.Data.DataRowAction action, System.Boolean isInMerge, System.Boolean suppressEnsurePropertyChanged, System.Int32 position, System.Boolean fireEvent, System.Exception& deferredException) { }
        // RVA: 0x0AD44038  token: 0x60002BA
        private System.Void SetOldRecord(System.Data.DataRow row, System.Int32 proposedRecord) { }
        // RVA: 0x0AD40E94  token: 0x60002BB
        private System.Void RestoreShadowIndexes() { }
        // RVA: 0x0AD44324  token: 0x60002BC
        private System.Void SetShadowIndexes() { }
        // RVA: 0x0AD44360  token: 0x60002BD
        private System.Void ShadowIndexCopy() { }
        // RVA: 0x0AD4471C  token: 0x60002BE
        public virtual System.String ToString() { }
        // RVA: 0x0AD44788  token: 0x60002BF
        private System.Boolean UpdatingCurrent(System.Data.DataRow row, System.Data.DataRowAction action) { }
        // RVA: 0x0AD32264  token: 0x60002C0
        private System.Data.DataColumn AddUniqueKey(System.Int32 position) { }
        // RVA: 0x0AD32254  token: 0x60002C1
        private System.Data.DataColumn AddUniqueKey() { }
        // RVA: 0x0AD3217C  token: 0x60002C2
        private System.Data.DataColumn AddForeignKey(System.Data.DataColumn parentKey) { }
        // RVA: 0x06ECA82C  token: 0x60002C3
        private System.Void UpdatePropertyDescriptorCollectionCache() { }
        // RVA: 0x0AD39FF8  token: 0x60002C4
        private System.ComponentModel.PropertyDescriptorCollection GetPropertyDescriptorCollection(System.Attribute[] attributes) { }
        // RVA: 0x0AD3BCE4  token: 0x60002C7
        public System.Void Merge(System.Data.DataTable table) { }
        // RVA: 0x0AD3BD04  token: 0x60002C8
        public System.Void Merge(System.Data.DataTable table, System.Boolean preserveChanges, System.Data.MissingSchemaAction missingSchemaAction) { }
        // RVA: 0x0AD44AD4  token: 0x60002C9
        public System.Void WriteXml(System.Xml.XmlWriter writer, System.Data.XmlWriteMode mode, System.Boolean writeHierarchy) { }
        // RVA: 0x0AD32C90  token: 0x60002CA
        private System.Boolean CheckForClosureOnExpressions(System.Data.DataTable dt, System.Boolean writeHierarchy) { }
        // RVA: 0x0AD3283C  token: 0x60002CB
        private System.Boolean CheckForClosureOnExpressionTables(System.Collections.Generic.List<System.Data.DataTable> tableList) { }
        // RVA: 0x0AD447AC  token: 0x60002CC
        public System.Void WriteXmlSchema(System.Xml.XmlWriter writer, System.Boolean writeHierarchy) { }
        // RVA: 0x0AD40B14  token: 0x60002CD
        private System.Void RestoreConstraint(System.Boolean originalEnforceConstraint) { }
        // RVA: 0x0AD3B3D4  token: 0x60002CE
        private System.Boolean IsEmptyXml(System.Xml.XmlReader reader) { }
        // RVA: 0x0AD3F1C8  token: 0x60002CF
        private System.Data.XmlReadMode ReadXml(System.Xml.XmlReader reader, System.Data.XmlReadMode mode, System.Boolean denyResolving) { }
        // RVA: 0x0AD3D628  token: 0x60002D0
        private System.Void ReadEndElement(System.Xml.XmlReader reader) { }
        // RVA: 0x0AD3D694  token: 0x60002D1
        private System.Void ReadXDRSchema(System.Xml.XmlReader reader) { }
        // RVA: 0x0AD3BF38  token: 0x60002D2
        private System.Boolean MoveToElement(System.Xml.XmlReader reader, System.Int32 depth) { }
        // RVA: 0x0AD3D848  token: 0x60002D3
        private System.Void ReadXmlDiffgram(System.Xml.XmlReader reader) { }
        // RVA: 0x0AD3D6FC  token: 0x60002D4
        private System.Void ReadXSDSchema(System.Xml.XmlReader reader, System.Boolean denyResolving) { }
        // RVA: 0x0AD3E1E0  token: 0x60002D5
        private System.Void ReadXmlSchema(System.Xml.XmlReader reader, System.Boolean denyResolving) { }
        // RVA: 0x0AD3535C  token: 0x60002D6
        private System.Void CreateTableList(System.Data.DataTable currentTable, System.Collections.Generic.List<System.Data.DataTable> tableList) { }
        // RVA: 0x0AD35034  token: 0x60002D7
        private System.Void CreateRelationList(System.Collections.Generic.List<System.Data.DataTable> tableList, System.Collections.Generic.List<System.Data.DataRelation> relationList) { }
        // RVA: 0x0AD397E4  token: 0x60002D8
        public static System.Xml.Schema.XmlSchemaComplexType GetDataTableSchema(System.Xml.Schema.XmlSchemaSet schemaSet) { }
        // RVA: 0x0AD44614  token: 0x60002D9
        private virtual System.Xml.Schema.XmlSchema System.Xml.Serialization.IXmlSerializable.GetSchema() { }
        // RVA: 0x0AD3A460  token: 0x60002DA
        protected virtual System.Xml.Schema.XmlSchema GetSchema() { }
        // RVA: 0x0AD4461C  token: 0x60002DB
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader reader) { }
        // RVA: 0x0AD446D8  token: 0x60002DC
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter writer) { }
        // RVA: 0x0AD3F1A8  token: 0x60002DD
        protected virtual System.Void ReadXmlSerializable(System.Xml.XmlReader reader) { }
        // RVA: 0x0AD32094  token: 0x60002E0
        private System.Void AddDependentColumn(System.Data.DataColumn expressionColumn) { }
        // RVA: 0x0AD403A8  token: 0x60002E1
        private System.Void RemoveDependentColumn(System.Data.DataColumn expressionColumn) { }
        // RVA: 0x0AD38B6C  token: 0x60002E2
        private System.Void EvaluateExpressions() { }
        // RVA: 0x0AD38D94  token: 0x60002E3
        private System.Void EvaluateExpressions(System.Data.DataRow row, System.Data.DataRowAction action, System.Collections.Generic.List<System.Data.DataRow> cachedRows) { }
        // RVA: 0x0AD38920  token: 0x60002E4
        private System.Void EvaluateExpressions(System.Data.DataColumn column) { }
        // RVA: 0x0AD387E4  token: 0x60002E5
        private System.Void EvaluateDependentExpressions(System.Data.DataColumn column) { }
        // RVA: 0x0AD37F38  token: 0x60002E6
        private System.Void EvaluateDependentExpressions(System.Collections.Generic.List<System.Data.DataColumn> columns, System.Data.DataRow row, System.Data.DataRowVersion version, System.Collections.Generic.List<System.Data.DataRow> cachedRows) { }

    }

    // TypeToken: 0x200001C  // size: 0x38
    public sealed class FunctionNode : System.Data.ExpressionNode
    {
        // Fields
        private readonly System.String _name;  // 0x18
        private readonly System.Int32 _info;  // 0x20
        private System.Int32 _argumentCount;  // 0x24
        private System.Data.ExpressionNode[] _arguments;  // 0x28
        private readonly System.Data.TypeLimiter _capturedLimiter;  // 0x30
        private static readonly System.Data.Function[] s_funcs;  // static @ 0x0

        // Properties
        System.Data.FunctionId Aggregate { get; /* RVA: 0x0AD5FCE8 */ }
        System.Boolean IsAggregate { get; /* RVA: 0x0AD5FD6C */ }

        // Methods
        // RVA: 0x0AD5FBC0  token: 0x60002E9
        private System.Void .ctor(System.Data.DataTable table, System.String name) { }
        // RVA: 0x0AD5C678  token: 0x60002EA
        private System.Void AddArgument(System.Data.ExpressionNode argument) { }
        // RVA: 0x0AD5C820  token: 0x60002EB
        private virtual System.Void Bind(System.Data.DataTable table, System.Collections.Generic.List<System.Data.DataColumn> list) { }
        // RVA: 0x0AD5EC2C  token: 0x60002EC
        private virtual System.Object Eval() { }
        // RVA: 0x0AD5E2E8  token: 0x60002ED
        private virtual System.Object Eval(System.Data.DataRow row, System.Data.DataRowVersion version) { }
        // RVA: 0x0AD5EBF4  token: 0x60002EE
        private virtual System.Object Eval(System.Int32[] recordNos) { }
        // RVA: 0x0AD5EF58  token: 0x60002EF
        private virtual System.Boolean IsConstant() { }
        // RVA: 0x0AD5EFB8  token: 0x60002F0
        private virtual System.Boolean IsTableConstant() { }
        // RVA: 0x0AD5EE90  token: 0x60002F1
        private virtual System.Boolean HasLocalAggregate() { }
        // RVA: 0x0AD5EEF4  token: 0x60002F2
        private virtual System.Boolean HasRemoteAggregate() { }
        // RVA: 0x0AD5CCE8  token: 0x60002F3
        private virtual System.Boolean DependsOn(System.Data.DataColumn column) { }
        // RVA: 0x0AD5F01C  token: 0x60002F4
        private virtual System.Data.ExpressionNode Optimize() { }
        // RVA: 0x0AD5EC40  token: 0x60002F5
        private System.Type GetDataType(System.Data.ExpressionNode node) { }
        // RVA: 0x0AD5CD5C  token: 0x60002F6
        private System.Object EvalFunction(System.Data.FunctionId id, System.Object[] argumentValues, System.Data.DataRow row, System.Data.DataRowVersion version) { }
        // RVA: 0x0AD5CAC0  token: 0x60002F9
        private System.Void Check() { }
        // RVA: 0x0AD5F19C  token: 0x60002FA
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200001D  // size: 0x14
    public sealed struct FunctionId
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Data.FunctionId none;  // const
        public static System.Data.FunctionId Ascii;  // const
        public static System.Data.FunctionId Char;  // const
        public static System.Data.FunctionId Charindex;  // const
        public static System.Data.FunctionId Difference;  // const
        public static System.Data.FunctionId Len;  // const
        public static System.Data.FunctionId Lower;  // const
        public static System.Data.FunctionId LTrim;  // const
        public static System.Data.FunctionId Patindex;  // const
        public static System.Data.FunctionId Replicate;  // const
        public static System.Data.FunctionId Reverse;  // const
        public static System.Data.FunctionId Right;  // const
        public static System.Data.FunctionId RTrim;  // const
        public static System.Data.FunctionId Soundex;  // const
        public static System.Data.FunctionId Space;  // const
        public static System.Data.FunctionId Str;  // const
        public static System.Data.FunctionId Stuff;  // const
        public static System.Data.FunctionId Substring;  // const
        public static System.Data.FunctionId Upper;  // const
        public static System.Data.FunctionId IsNull;  // const
        public static System.Data.FunctionId Iif;  // const
        public static System.Data.FunctionId Convert;  // const
        public static System.Data.FunctionId cInt;  // const
        public static System.Data.FunctionId cBool;  // const
        public static System.Data.FunctionId cDate;  // const
        public static System.Data.FunctionId cDbl;  // const
        public static System.Data.FunctionId cStr;  // const
        public static System.Data.FunctionId Abs;  // const
        public static System.Data.FunctionId Acos;  // const
        public static System.Data.FunctionId In;  // const
        public static System.Data.FunctionId Trim;  // const
        public static System.Data.FunctionId Sum;  // const
        public static System.Data.FunctionId Avg;  // const
        public static System.Data.FunctionId Min;  // const
        public static System.Data.FunctionId Max;  // const
        public static System.Data.FunctionId Count;  // const
        public static System.Data.FunctionId StDev;  // const
        public static System.Data.FunctionId Var;  // const
        public static System.Data.FunctionId DateTimeOffset;  // const

    }

    // TypeToken: 0x200001E  // size: 0x38
    public sealed class Function
    {
        // Fields
        private readonly System.String _name;  // 0x10
        private readonly System.Data.FunctionId _id;  // 0x18
        private readonly System.Type _result;  // 0x20
        private readonly System.Boolean _isValidateArguments;  // 0x28
        private readonly System.Boolean _isVariantArgumentList;  // 0x29
        private readonly System.Int32 _argumentCount;  // 0x2c
        private readonly System.Type[] _parameters;  // 0x30
        private static System.String[] s_functionName;  // static @ 0x0

        // Methods
        // RVA: 0x0AD73BAC  token: 0x60002FB
        private System.Void .ctor(System.String name, System.Data.FunctionId id, System.Type result, System.Boolean IsValidateArguments, System.Boolean IsVariantArgumentList, System.Int32 argumentCount, System.Type a1, System.Type a2, System.Type a3) { }
        // RVA: 0x0AD73654  token: 0x60002FC
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200001F  // size: 0x18
    public sealed class TypeLimiter
    {
        // Fields
        private static System.Data.TypeLimiter.Scope s_activeScope;  // static @ 0xffffffff
        private System.Data.TypeLimiter.Scope m_instanceScope;  // 0x10

        // Properties
        System.Boolean IsTypeLimitingDisabled { get; /* RVA: 0x0AD751F0 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x60002FD
        private System.Void .ctor(System.Data.TypeLimiter.Scope scope) { }
        // RVA: 0x0AD74C8C  token: 0x60002FF
        public static System.Data.TypeLimiter Capture() { }
        // RVA: 0x0AD74D08  token: 0x6000300
        public static System.Void EnsureTypeIsAllowed(System.Type type, System.Data.TypeLimiter capturedLimiter) { }
        // RVA: 0x0AD74DA0  token: 0x6000301
        public static System.IDisposable EnterRestrictedScope(System.Data.DataSet dataSet) { }
        // RVA: 0x0AD74E7C  token: 0x6000302
        public static System.IDisposable EnterRestrictedScope(System.Data.DataTable dataTable) { }
        // RVA: 0x0AD750A4  token: 0x6000303
        private static System.Collections.Generic.IEnumerable<System.Type> GetPreviouslyDeclaredDataTypes(System.Data.DataTable dataTable) { }
        // RVA: 0x0AD74F58  token: 0x6000304
        private static System.Collections.Generic.IEnumerable<System.Type> GetPreviouslyDeclaredDataTypes(System.Data.DataSet dataSet) { }

    }

    // TypeToken: 0x2000023  // size: 0x14
    public sealed struct AcceptRejectRule
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Data.AcceptRejectRule None;  // const
        public static System.Data.AcceptRejectRule Cascade;  // const

    }

    // TypeToken: 0x2000024  // size: 0x14
    public sealed struct AggregateType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Data.AggregateType None;  // const
        public static System.Data.AggregateType Sum;  // const
        public static System.Data.AggregateType Mean;  // const
        public static System.Data.AggregateType Min;  // const
        public static System.Data.AggregateType Max;  // const
        public static System.Data.AggregateType First;  // const
        public static System.Data.AggregateType Count;  // const
        public static System.Data.AggregateType Var;  // const
        public static System.Data.AggregateType StDev;  // const

    }

    // TypeToken: 0x2000025  // size: 0x10
    public class InternalDataCollectionBase : System.Collections.ICollection, System.Collections.IEnumerable
    {
        // Fields
        private static readonly System.ComponentModel.CollectionChangeEventArgs s_refreshEventArgs;  // static @ 0x0

        // Properties
        System.Int32 Count { get; /* RVA: 0x0AD73EA4 */ }
        System.Boolean IsSynchronized { get; /* RVA: 0x012081B0 */ }
        System.Object SyncRoot { get; /* RVA: 0x0464E630 */ }
        System.Collections.ArrayList List { get; /* RVA: 0x011EC580 */ }

        // Methods
        // RVA: 0x0AD73D0C  token: 0x6000312
        public virtual System.Void CopyTo(System.Array ar, System.Int32 index) { }
        // RVA: 0x0AD73D4C  token: 0x6000313
        public virtual System.Collections.IEnumerator GetEnumerator() { }
        // RVA: 0x0AD73D74  token: 0x6000315
        private System.Int32 NamesEqual(System.String s1, System.String s2, System.Boolean fCaseSensitive, System.Globalization.CultureInfo locale) { }
        // RVA: 0x041E1670  token: 0x6000318
        public System.Void .ctor() { }
        // RVA: 0x0AD73E20  token: 0x6000319
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000026  // size: 0x18
    public sealed class ColumnTypeConverter : System.ComponentModel.TypeConverter
    {
        // Fields
        private static readonly System.Type[] s_types;  // static @ 0x0
        private System.ComponentModel.TypeConverter.StandardValuesCollection _values;  // 0x10

        // Methods
        // RVA: 0x041E1670  token: 0x600031A
        public System.Void .ctor() { }
        // RVA: 0x0AD60810  token: 0x600031B
        public virtual System.Boolean CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Type destinationType) { }
        // RVA: 0x0AD60A50  token: 0x600031C
        public virtual System.Object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, System.Object value, System.Type destinationType) { }
        // RVA: 0x0AD6077C  token: 0x600031D
        public virtual System.Boolean CanConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Type sourceType) { }
        // RVA: 0x0AD608A4  token: 0x600031E
        public virtual System.Object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, System.Object value) { }
        // RVA: 0x0AD60E58  token: 0x600031F
        public virtual System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(System.ComponentModel.ITypeDescriptorContext context) { }
        // RVA: 0x02FFF600  token: 0x6000320
        public virtual System.Boolean GetStandardValuesExclusive(System.ComponentModel.ITypeDescriptorContext context) { }
        // RVA: 0x02FFF600  token: 0x6000321
        public virtual System.Boolean GetStandardValuesSupported(System.ComponentModel.ITypeDescriptorContext context) { }
        // RVA: 0x0AD60F7C  token: 0x6000322
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000027  // size: 0x18
    public class DataCommonEventSource : System.Diagnostics.Tracing.EventSource
    {
        // Fields
        private static readonly System.Data.DataCommonEventSource Log;  // static @ 0x0
        private static System.Int64 s_nextScopeId;  // static @ 0x8

        // Methods
        // RVA: 0x0AD66EF8  token: 0x6000323
        private System.Void Trace(System.String message) { }
        // RVA: -1  // generic def  token: 0x6000324
        private System.Void Trace(System.String format, T0 arg0) { }
        // RVA: -1  // generic def  token: 0x6000325
        private System.Void Trace(System.String format, T0 arg0, T1 arg1) { }
        // RVA: -1  // generic def  token: 0x6000326
        private System.Void Trace(System.String format, T0 arg0, T1 arg1, T2 arg2) { }
        // RVA: -1  // generic def  token: 0x6000327
        private System.Void Trace(System.String format, T0 arg0, T1 arg1, T2 arg2, T3 arg3) { }
        // RVA: -1  // generic def  token: 0x6000328
        private System.Void Trace(System.String format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4) { }
        // RVA: -1  // generic def  token: 0x6000329
        private System.Void Trace(System.String format, T0 arg0, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5, T6 arg6) { }
        // RVA: 0x0AD66E8C  token: 0x600032A
        private System.Int64 EnterScope(System.String message) { }
        // RVA: -1  // generic def  token: 0x600032B
        private System.Int64 EnterScope(System.String format, T1 arg1) { }
        // RVA: -1  // generic def  token: 0x600032C
        private System.Int64 EnterScope(System.String format, T1 arg1, T2 arg2) { }
        // RVA: -1  // generic def  token: 0x600032D
        private System.Int64 EnterScope(System.String format, T1 arg1, T2 arg2, T3 arg3) { }
        // RVA: -1  // generic def  token: 0x600032E
        private System.Int64 EnterScope(System.String format, T1 arg1, T2 arg2, T3 arg3, T4 arg4) { }
        // RVA: 0x0AD66EE8  token: 0x600032F
        private System.Void ExitScope(System.Int64 scopeId) { }
        // RVA: 0x05398244  token: 0x6000330
        public System.Void .ctor() { }
        // RVA: 0x0AD66F08  token: 0x6000331
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000028  // size: 0x38
    public abstract class Constraint
    {
        // Fields
        private System.String _schemaName;  // 0x10
        private System.Boolean _inCollection;  // 0x18
        private System.Data.DataSet _dataSet;  // 0x20
        private System.String _name;  // 0x28
        private System.Data.PropertyCollection _extendedProperties;  // 0x30

        // Properties
        System.String ConstraintName { get; /* RVA: 0x04D86240 */ set; /* RVA: 0x0AD63FDC */ }
        System.String SchemaName { get; /* RVA: 0x0AD63FC8 */ set; /* RVA: 0x0AD641B8 */ }
        System.Boolean InCollection { get; /* RVA: 0x04D86340 */ set; /* RVA: 0x0AD64174 */ }
        System.Data.DataTable Table { get; /* RVA: -1  // abstract */ }
        System.Data.PropertyCollection ExtendedProperties { get; /* RVA: 0x0AD63F54 */ }
        System.Data.DataSet _DataSet { get; /* RVA: 0x04D862C0 */ }

        // Methods
        // RVA: -1  // abstract  token: 0x600033A
        private virtual System.Boolean ContainsColumn(System.Data.DataColumn column) { }
        // RVA: -1  // abstract  token: 0x600033B
        private virtual System.Boolean CanEnableConstraint() { }
        // RVA: -1  // abstract  token: 0x600033C
        private virtual System.Data.Constraint Clone(System.Data.DataSet destination) { }
        // RVA: -1  // abstract  token: 0x600033D
        private virtual System.Data.Constraint Clone(System.Data.DataSet destination, System.Boolean ignoreNSforTableLookup) { }
        // RVA: 0x0AD63D88  token: 0x600033E
        private System.Void CheckConstraint() { }
        // RVA: -1  // abstract  token: 0x600033F
        private virtual System.Void CheckCanAddToCollection(System.Data.ConstraintCollection constraint) { }
        // RVA: -1  // abstract  token: 0x6000340
        private virtual System.Boolean CanBeRemovedFromCollection(System.Data.ConstraintCollection constraint, System.Boolean fThrowException) { }
        // RVA: -1  // abstract  token: 0x6000341
        private virtual System.Void CheckConstraint(System.Data.DataRow row, System.Data.DataRowAction action) { }
        // RVA: -1  // abstract  token: 0x6000342
        private virtual System.Void CheckState() { }
        // RVA: 0x0AD63DD0  token: 0x6000343
        protected System.Void CheckStateForProperty() { }
        // RVA: -1  // abstract  token: 0x6000345
        private virtual System.Boolean IsConstraintViolated() { }
        // RVA: 0x04D86240  token: 0x6000346
        public virtual System.String ToString() { }
        // RVA: 0x0AD63EEC  token: 0x6000347
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x2000029  // size: 0x40
    public sealed class ConstraintCollection : System.Data.InternalDataCollectionBase
    {
        // Fields
        private readonly System.Data.DataTable _table;  // 0x10
        private readonly System.Collections.ArrayList _list;  // 0x18
        private System.Int32 _defaultNameIndex;  // 0x20
        private System.ComponentModel.CollectionChangeEventHandler _onCollectionChanged;  // 0x28
        private System.Data.Constraint[] _delayLoadingConstraints;  // 0x30
        private System.Boolean _fLoadForeignKeyConstraintsOnly;  // 0x38

        // Properties
        System.Collections.ArrayList List { get; /* RVA: 0x0385B100 */ }
        System.Data.Constraint Item { get; /* RVA: 0x0AD6321C */ }
        System.Data.DataTable Table { get; /* RVA: 0x02B2ECC0 */ }
        System.Data.Constraint Item { get; /* RVA: 0x0AD6321C */ }

        // Methods
        // RVA: 0x0AD6317C  token: 0x6000348
        private System.Void .ctor(System.Data.DataTable table) { }
        // RVA: 0x0AD618F8  token: 0x600034D
        public System.Void Add(System.Data.Constraint constraint) { }
        // RVA: 0x0AD619B8  token: 0x600034E
        private System.Void Add(System.Data.Constraint constraint, System.Boolean addUniqueWhenAddingForeign) { }
        // RVA: 0x0AD61904  token: 0x600034F
        public System.Data.Constraint Add(System.String name, System.Data.DataColumn[] columns, System.Boolean primaryKey) { }
        // RVA: 0x0AD61834  token: 0x6000350
        private System.Void AddUniqueConstraint(System.Data.UniqueConstraint constraint) { }
        // RVA: 0x0AD617C0  token: 0x6000351
        private System.Void AddForeignKeyConstraint(System.Data.ForeignKeyConstraint constraint) { }
        // RVA: 0x0AD61E48  token: 0x6000352
        private System.Boolean AutoGenerated(System.Data.Constraint constraint) { }
        // RVA: 0x0AD61DC0  token: 0x6000353
        private System.Void ArrayAdd(System.Data.Constraint constraint) { }
        // RVA: 0x0AD61DF4  token: 0x6000354
        private System.Void ArrayRemove(System.Data.Constraint constraint) { }
        // RVA: 0x0AD61E28  token: 0x6000355
        private System.String AssignName() { }
        // RVA: 0x0AD61EC0  token: 0x6000356
        private System.Void BaseAdd(System.Data.Constraint constraint) { }
        // RVA: 0x0AD61F4C  token: 0x6000357
        private System.Void BaseGroupSwitch(System.Data.Constraint[] oldArray, System.Int32 oldLength, System.Data.Constraint[] newArray, System.Int32 newLength) { }
        // RVA: 0x0AD620C4  token: 0x6000358
        private System.Void BaseRemove(System.Data.Constraint constraint) { }
        // RVA: 0x0AD62314  token: 0x6000359
        private System.Boolean CanRemove(System.Data.Constraint constraint, System.Boolean fThrowException) { }
        // RVA: 0x0AD62370  token: 0x600035A
        public System.Void Clear() { }
        // RVA: 0x0AD62780  token: 0x600035B
        public System.Boolean Contains(System.String name) { }
        // RVA: 0x0AD62798  token: 0x600035C
        private System.Boolean Contains(System.String name, System.Boolean caseSensitive) { }
        // RVA: 0x0AD62858  token: 0x600035D
        private System.Data.Constraint FindConstraint(System.Data.Constraint constraint) { }
        // RVA: 0x0AD62A68  token: 0x600035E
        private System.Data.UniqueConstraint FindKeyConstraint(System.Data.DataColumn[] columns) { }
        // RVA: 0x0AD62B40  token: 0x600035F
        private System.Data.UniqueConstraint FindKeyConstraint(System.Data.DataColumn column) { }
        // RVA: 0x0AD62968  token: 0x6000360
        private System.Data.ForeignKeyConstraint FindForeignKeyConstraint(System.Data.DataColumn[] parentColumns, System.Data.DataColumn[] childColumns) { }
        // RVA: 0x0AD62710  token: 0x6000361
        private static System.Boolean CompareArrays(System.Data.DataColumn[] a1, System.Data.DataColumn[] a2) { }
        // RVA: 0x0AD62C04  token: 0x6000362
        private System.Int32 InternalIndexOf(System.String constraintName) { }
        // RVA: 0x0AD62D2C  token: 0x6000363
        private System.String MakeName(System.Int32 index) { }
        // RVA: 0x0AD62DB8  token: 0x6000364
        private System.Void OnCollectionChanged(System.ComponentModel.CollectionChangeEventArgs ccevent) { }
        // RVA: 0x0AD62DD4  token: 0x6000365
        private System.Void RegisterName(System.String name) { }
        // RVA: 0x0AD62FA4  token: 0x6000366
        public System.Void Remove(System.Data.Constraint constraint) { }
        // RVA: 0x0AD630F4  token: 0x6000367
        private System.Void UnregisterName(System.String name) { }

    }

    // TypeToken: 0x200002A  // size: 0x10
    public sealed class ConstraintConverter : System.ComponentModel.ExpandableObjectConverter
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000368
        public System.Void .ctor() { }
        // RVA: 0x0AD63384  token: 0x6000369
        public virtual System.Boolean CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Type destinationType) { }
        // RVA: 0x0AD63418  token: 0x600036A
        public virtual System.Object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, System.Object value, System.Type destinationType) { }

    }

    // TypeToken: 0x200002B  // size: 0x28
    public class ConstraintEnumerator
    {
        // Fields
        private System.Collections.IEnumerator _tables;  // 0x10
        private System.Collections.IEnumerator _constraints;  // 0x18
        private System.Data.Constraint _currentObject;  // 0x20

        // Properties
        System.Data.Constraint CurrentObject { get; /* RVA: 0x04D862C0 */ }

        // Methods
        // RVA: 0x0AD63D38  token: 0x600036B
        public System.Void .ctor(System.Data.DataSet dataSet) { }
        // RVA: 0x0AD63B78  token: 0x600036C
        public System.Boolean GetNext() { }
        // RVA: 0x04D862C0  token: 0x600036D
        public System.Data.Constraint GetConstraint() { }
        // RVA: 0x02FFF600  token: 0x600036E
        protected virtual System.Boolean IsValidCandidate(System.Data.Constraint constraint) { }

    }

    // TypeToken: 0x200002C  // size: 0x28
    public class ForeignKeyConstraintEnumerator : System.Data.ConstraintEnumerator
    {
        // Methods
        // RVA: 0x0AD7364C  token: 0x6000370
        public System.Void .ctor(System.Data.DataSet dataSet) { }
        // RVA: 0x0AD7360C  token: 0x6000371
        protected virtual System.Boolean IsValidCandidate(System.Data.Constraint constraint) { }
        // RVA: 0x0AD735D0  token: 0x6000372
        public System.Data.ForeignKeyConstraint GetForeignKeyConstraint() { }

    }

    // TypeToken: 0x200002D  // size: 0x30
    public sealed class ChildForeignKeyConstraintEnumerator : System.Data.ForeignKeyConstraintEnumerator
    {
        // Fields
        private readonly System.Data.DataTable _table;  // 0x28

        // Methods
        // RVA: 0x0AD6074C  token: 0x6000373
        public System.Void .ctor(System.Data.DataSet dataSet, System.Data.DataTable inTable) { }
        // RVA: 0x0AD606C4  token: 0x6000374
        protected virtual System.Boolean IsValidCandidate(System.Data.Constraint constraint) { }

    }

    // TypeToken: 0x200002E  // size: 0x30
    public sealed class ParentForeignKeyConstraintEnumerator : System.Data.ForeignKeyConstraintEnumerator
    {
        // Fields
        private readonly System.Data.DataTable _table;  // 0x28

        // Methods
        // RVA: 0x0AD6074C  token: 0x6000375
        public System.Void .ctor(System.Data.DataSet dataSet, System.Data.DataTable inTable) { }
        // RVA: 0x0AD73ECC  token: 0x6000376
        protected virtual System.Boolean IsValidCandidate(System.Data.Constraint constraint) { }

    }

    // TypeToken: 0x200002F  // size: 0x28
    public class DataColumnChangeEventArgs : System.EventArgs
    {
        // Fields
        private System.Data.DataColumn _column;  // 0x10
        private readonly System.Data.DataRow <Row>k__BackingField;  // 0x18
        private System.Object <ProposedValue>k__BackingField;  // 0x20

        // Properties
        System.Object ProposedValue { get; /* RVA: 0x04D862C0 */ set; /* RVA: 0x05390F40 */ }

        // Methods
        // RVA: 0x0AD641D4  token: 0x6000377
        private System.Void .ctor(System.Data.DataRow row) { }
        // RVA: 0x0AD6422C  token: 0x6000378
        public System.Void .ctor(System.Data.DataRow row, System.Data.DataColumn column, System.Object value) { }
        // RVA: 0x04D7D160  token: 0x600037B
        private System.Void InitializeColumnChangeEvent(System.Data.DataColumn column, System.Object value) { }

    }

    // TypeToken: 0x2000030  // size: 0x80
    public sealed class DataColumnChangeEventHandler : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x086FC23C  token: 0x600037C
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x038E1420  token: 0x600037D
        public virtual System.Void Invoke(System.Object sender, System.Data.DataColumnChangeEventArgs e) { }

    }

    // TypeToken: 0x2000031  // size: 0x68
    public sealed class DataColumnCollection : System.Data.InternalDataCollectionBase
    {
        // Fields
        private readonly System.Data.DataTable _table;  // 0x10
        private readonly System.Collections.ArrayList _list;  // 0x18
        private System.Int32 _defaultNameIndex;  // 0x20
        private System.Data.DataColumn[] _delayedAddRangeColumns;  // 0x28
        private readonly System.Collections.Generic.Dictionary<System.String,System.Data.DataColumn> _columnFromName;  // 0x30
        private System.Boolean _fInClear;  // 0x38
        private System.Data.DataColumn[] _columnsImplementingIChangeTracking;  // 0x40
        private System.Int32 _nColumnsImplementingIChangeTracking;  // 0x48
        private System.Int32 _nColumnsImplementingIRevertibleChangeTracking;  // 0x4c
        private System.ComponentModel.CollectionChangeEventHandler CollectionChanged;  // 0x50
        private System.ComponentModel.CollectionChangeEventHandler CollectionChanging;  // 0x58
        private System.ComponentModel.CollectionChangeEventHandler ColumnPropertyChanged;  // 0x60

        // Properties
        System.Collections.ArrayList List { get; /* RVA: 0x0385B100 */ }
        System.Data.DataColumn[] ColumnsImplementingIChangeTracking { get; /* RVA: 0x04D85A60 */ }
        System.Int32 ColumnsImplementingIChangeTrackingCount { get; /* RVA: 0x04D863D0 */ }
        System.Int32 ColumnsImplementingIRevertibleChangeTrackingCount { get; /* RVA: 0x04D86600 */ }
        System.Data.DataColumn Item { get; /* RVA: 0x0AD665CC */ }
        System.Data.DataColumn Item { get; /* RVA: 0x0AD665CC */ }
        System.Data.DataColumn Item { get; /* RVA: 0x0AD665CC */ }

        // Events
        event System.ComponentModel.CollectionChangeEventHandler CollectionChanged;
        event System.ComponentModel.CollectionChangeEventHandler ColumnPropertyChanged;

        // Methods
        // RVA: 0x0AD662F4  token: 0x600037E
        private System.Void .ctor(System.Data.DataTable table) { }
        // RVA: 0x0AD645B4  token: 0x6000386
        public System.Void Add(System.Data.DataColumn column) { }
        // RVA: 0x0AD642B0  token: 0x6000387
        private System.Void AddAt(System.Int32 index, System.Data.DataColumn column) { }
        // RVA: 0x0AD6460C  token: 0x600038C
        private System.Void ArrayAdd(System.Data.DataColumn column) { }
        // RVA: 0x0AD645C4  token: 0x600038D
        private System.Void ArrayAdd(System.Int32 index, System.Data.DataColumn column) { }
        // RVA: 0x0AD64678  token: 0x600038E
        private System.Void ArrayRemove(System.Data.DataColumn column) { }
        // RVA: 0x0AD6478C  token: 0x600038F
        private System.String AssignName() { }
        // RVA: 0x0AD647FC  token: 0x6000390
        private System.Void BaseAdd(System.Data.DataColumn column) { }
        // RVA: 0x0AD64ADC  token: 0x6000391
        private System.Void BaseGroupSwitch(System.Data.DataColumn[] oldArray, System.Int32 oldLength, System.Data.DataColumn[] newArray, System.Int32 newLength) { }
        // RVA: 0x0AD64CC8  token: 0x6000392
        private System.Void BaseRemove(System.Data.DataColumn column) { }
        // RVA: 0x0AD64DE4  token: 0x6000393
        private System.Boolean CanRemove(System.Data.DataColumn column, System.Boolean fThrowException) { }
        // RVA: 0x0AD656B4  token: 0x6000394
        private System.Void CheckIChangeTracking(System.Data.DataColumn column) { }
        // RVA: 0x0AD656EC  token: 0x6000395
        public System.Void Clear() { }
        // RVA: 0x0AD65A54  token: 0x6000396
        public System.Boolean Contains(System.String name) { }
        // RVA: 0x0AD659B8  token: 0x6000397
        private System.Boolean Contains(System.String name, System.Boolean caseSensitive) { }
        // RVA: 0x0AD65C10  token: 0x6000398
        public System.Int32 IndexOf(System.String columnName) { }
        // RVA: 0x0AD65AD8  token: 0x6000399
        private System.Int32 IndexOfCaseInsensitive(System.String name) { }
        // RVA: 0x0AD65D04  token: 0x600039A
        private System.String MakeName(System.Int32 index) { }
        // RVA: 0x0AD65D90  token: 0x600039B
        private System.Void OnCollectionChanged(System.ComponentModel.CollectionChangeEventArgs ccevent) { }
        // RVA: 0x0AD65E04  token: 0x600039C
        private System.Void OnCollectionChanging(System.ComponentModel.CollectionChangeEventArgs ccevent) { }
        // RVA: 0x0AD65E20  token: 0x600039D
        private System.Void OnColumnPropertyChanged(System.ComponentModel.CollectionChangeEventArgs ccevent) { }
        // RVA: 0x0AD65E6C  token: 0x600039E
        private System.Void RegisterColumnName(System.String name, System.Data.DataColumn column) { }
        // RVA: 0x0AD64D8C  token: 0x600039F
        private System.Boolean CanRegisterName(System.String name) { }
        // RVA: 0x0AD6613C  token: 0x60003A0
        public System.Void Remove(System.Data.DataColumn column) { }
        // RVA: 0x0AD66230  token: 0x60003A1
        private System.Void UnregisterName(System.String name) { }
        // RVA: 0x0AD64508  token: 0x60003A2
        private System.Void AddColumnsImplementingIChangeTrackingList(System.Data.DataColumn dataColumn) { }
        // RVA: 0x0AD66060  token: 0x60003A3
        private System.Void RemoveColumnsImplementingIChangeTrackingList(System.Data.DataColumn dataColumn) { }

    }

    // TypeToken: 0x2000032  // size: 0x90
    public sealed class DataColumnPropertyDescriptor : System.ComponentModel.PropertyDescriptor
    {
        // Fields
        private readonly System.Data.DataColumn <Column>k__BackingField;  // 0x88

        // Properties
        System.ComponentModel.AttributeCollection Attributes { get; /* RVA: 0x0AD66C8C */ }
        System.Data.DataColumn Column { get; /* RVA: 0x04D861F0 */ }
        System.Type ComponentType { get; /* RVA: 0x0AD66DFC */ }
        System.Boolean IsReadOnly { get; /* RVA: 0x0AD66E4C */ }
        System.Type PropertyType { get; /* RVA: 0x0AD66E6C */ }

        // Methods
        // RVA: 0x0AD66C44  token: 0x60003A4
        private System.Void .ctor(System.Data.DataColumn dataColumn) { }
        // RVA: 0x0AD669DC  token: 0x60003AA
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x0AD66A3C  token: 0x60003AB
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AD66900  token: 0x60003AC
        public virtual System.Boolean CanResetValue(System.Object component) { }
        // RVA: 0x0AD66A60  token: 0x60003AD
        public virtual System.Object GetValue(System.Object component) { }
        // RVA: 0x0AD66AD4  token: 0x60003AE
        public virtual System.Void ResetValue(System.Object component) { }
        // RVA: 0x0AD66B80  token: 0x60003AF
        public virtual System.Void SetValue(System.Object component, System.Object value) { }
        // RVA: 0x012081B0  token: 0x60003B0
        public virtual System.Boolean ShouldSerializeValue(System.Object component) { }

    }

    // TypeToken: 0x2000033  // size: 0x28
    public sealed class DataError
    {
        // Fields
        private System.String _rowError;  // 0x10
        private System.Int32 _count;  // 0x18
        private System.Data.DataError.ColumnError[] _errorList;  // 0x20

        // Properties
        System.String Text { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x0AD67554 */ }
        System.Boolean HasErrors { get; /* RVA: 0x0AD67528 */ }

        // Methods
        // RVA: 0x0AD674E0  token: 0x60003B1
        private System.Void .ctor() { }
        // RVA: 0x0AD6747C  token: 0x60003B2
        private System.Void .ctor(System.String rowError) { }
        // RVA: 0x0AD672FC  token: 0x60003B6
        private System.Void SetColumnError(System.Data.DataColumn column, System.String error) { }
        // RVA: 0x0AD6709C  token: 0x60003B7
        private System.String GetColumnError(System.Data.DataColumn column) { }
        // RVA: 0x0AD66F8C  token: 0x60003B8
        private System.Void Clear(System.Data.DataColumn column) { }
        // RVA: 0x0AD67014  token: 0x60003B9
        private System.Void Clear() { }
        // RVA: 0x0AD67130  token: 0x60003BA
        private System.Data.DataColumn[] GetColumnsInError() { }
        // RVA: 0x0AD67420  token: 0x60003BB
        private System.Void SetText(System.String errorText) { }
        // RVA: 0x0AD671D4  token: 0x60003BC
        private System.Int32 IndexOf(System.Data.DataColumn column) { }

    }

    // TypeToken: 0x2000035  // size: 0x18
    public sealed struct DataKey
    {
        // Fields
        private readonly System.Data.DataColumn[] _columns;  // 0x10

        // Properties
        System.Data.DataColumn[] ColumnsReference { get; /* RVA: 0x02B76770 */ }
        System.Boolean HasValue { get; /* RVA: 0x03A7A090 */ }
        System.Data.DataTable Table { get; /* RVA: 0x0AD67E5C */ }

        // Methods
        // RVA: 0x0AD67C34  token: 0x60003BD
        private System.Void .ctor(System.Data.DataColumn[] columns, System.Boolean copyColumns) { }
        // RVA: 0x0AD6755C  token: 0x60003C1
        private System.Void CheckState() { }
        // RVA: 0x0AD676E4  token: 0x60003C2
        private System.Boolean ColumnsEqual(System.Data.DataKey key) { }
        // RVA: 0x0AD6764C  token: 0x60003C3
        private static System.Boolean ColumnsEqual(System.Data.DataColumn[] column1, System.Data.DataColumn[] column2) { }
        // RVA: 0x0AD676F0  token: 0x60003C4
        private System.Boolean ContainsColumn(System.Data.DataColumn column) { }
        // RVA: 0x0AD678A8  token: 0x60003C5
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AD677B4  token: 0x60003C6
        public virtual System.Boolean Equals(System.Object value) { }
        // RVA: 0x0AD67734  token: 0x60003C7
        private System.Boolean Equals(System.Data.DataKey value) { }
        // RVA: 0x0AD67808  token: 0x60003C8
        private System.String[] GetColumnNames() { }
        // RVA: 0x0AD678F8  token: 0x60003C9
        private System.Data.IndexField[] GetIndexDesc() { }
        // RVA: 0x0AD679C0  token: 0x60003CA
        private System.Object[] GetKeyValues(System.Int32 record) { }
        // RVA: 0x0AD67AF8  token: 0x60003CB
        private System.Data.Index GetSortIndex() { }
        // RVA: 0x0AD67A94  token: 0x60003CC
        private System.Data.Index GetSortIndex(System.Data.DataViewRowState recordStates) { }
        // RVA: 0x0AD67B08  token: 0x60003CD
        private System.Boolean RecordsEqual(System.Int32 record1, System.Int32 record2) { }
        // RVA: 0x0AD67B84  token: 0x60003CE
        private System.Data.DataColumn[] ToArray() { }

    }

    // TypeToken: 0x2000036  // size: 0x88
    public class DataRelation
    {
        // Fields
        private System.Data.DataSet _dataSet;  // 0x10
        private System.Data.PropertyCollection _extendedProperties;  // 0x18
        private System.String _relationName;  // 0x20
        private System.Data.DataKey _childKey;  // 0x28
        private System.Data.DataKey _parentKey;  // 0x30
        private System.Data.UniqueConstraint _parentKeyConstraint;  // 0x38
        private System.Data.ForeignKeyConstraint _childKeyConstraint;  // 0x40
        private System.String[] _parentColumnNames;  // 0x48
        private System.String[] _childColumnNames;  // 0x50
        private System.String _parentTableName;  // 0x58
        private System.String _childTableName;  // 0x60
        private System.String _parentTableNamespace;  // 0x68
        private System.String _childTableNamespace;  // 0x70
        private System.Boolean _nested;  // 0x78
        private System.Boolean _createConstraints;  // 0x79
        private System.Boolean _checkMultipleNested;  // 0x7a
        private static System.Int32 s_objectTypeCount;  // static @ 0x0
        private readonly System.Int32 _objectID;  // 0x7c
        private System.ComponentModel.PropertyChangedEventHandler PropertyChanging;  // 0x80

        // Properties
        System.Data.DataColumn[] ChildColumns { get; /* RVA: 0x0AD6B4E8 */ }
        System.Data.DataColumn[] ChildColumnsReference { get; /* RVA: 0x0AD6B4CC */ }
        System.Data.DataKey ChildKey { get; /* RVA: 0x0AD6B4CC */ }
        System.Data.DataTable ChildTable { get; /* RVA: 0x0AD6B524 */ }
        System.Data.DataSet DataSet { get; /* RVA: 0x0AD6B544 */ }
        System.String[] ParentColumnNames { get; /* RVA: 0x0AD6B5F0 */ }
        System.String[] ChildColumnNames { get; /* RVA: 0x0AD6B4C0 */ }
        System.Data.DataColumn[] ParentColumns { get; /* RVA: 0x0AD6B5FC */ }
        System.Data.DataColumn[] ParentColumnsReference { get; /* RVA: 0x011F36E0 */ }
        System.Data.DataKey ParentKey { get; /* RVA: 0x0AD6B638 */ }
        System.Data.DataTable ParentTable { get; /* RVA: 0x0AD6B654 */ }
        System.String RelationName { get; /* RVA: 0x0AD6AC30 */ }
        System.Boolean Nested { get; /* RVA: 0x0AD6B5D4 */ set; /* RVA: 0x0AD6B674 */ }
        System.Data.UniqueConstraint ParentKeyConstraint { get; /* RVA: 0x0AD6B61C */ }
        System.Data.ForeignKeyConstraint ChildKeyConstraint { get; /* RVA: 0x0AD6B508 */ }
        System.Data.PropertyCollection ExtendedProperties { get; /* RVA: 0x0AD6B560 */ }
        System.Boolean CheckMultipleNested { get; /* RVA: 0x04D88E50 */ set; /* RVA: 0x04D88EA0 */ }
        System.Int32 ObjectID { get; /* RVA: 0x04D86410 */ }

        // Methods
        // RVA: 0x0AD6AFEC  token: 0x60003CF
        public System.Void .ctor(System.String relationName, System.Data.DataColumn parentColumn, System.Data.DataColumn childColumn, System.Boolean createConstraints) { }
        // RVA: 0x0AD6B288  token: 0x60003D0
        public System.Void .ctor(System.String relationName, System.Data.DataColumn[] parentColumns, System.Data.DataColumn[] childColumns) { }
        // RVA: 0x0AD6B1C4  token: 0x60003D1
        public System.Void .ctor(System.String relationName, System.Data.DataColumn[] parentColumns, System.Data.DataColumn[] childColumns, System.Boolean createConstraints) { }
        // RVA: 0x0AD6B3C4  token: 0x60003D2
        public System.Void .ctor(System.String relationName, System.String parentTableName, System.String childTableName, System.String[] parentColumnNames, System.String[] childColumnNames, System.Boolean nested) { }
        // RVA: 0x0AD6B2A4  token: 0x60003D3
        public System.Void .ctor(System.String relationName, System.String parentTableName, System.String parentTableNamespace, System.String childTableName, System.String childTableNamespace, System.String[] parentColumnNames, System.String[] childColumnNames, System.Boolean nested) { }
        // RVA: 0x0AD6AA68  token: 0x60003DB
        private static System.Boolean IsKeyNull(System.Object[] values) { }
        // RVA: 0x0AD6A638  token: 0x60003DC
        private static System.Data.DataRow[] GetChildRows(System.Data.DataKey parentKey, System.Data.DataKey childKey, System.Data.DataRow parentRow, System.Data.DataRowVersion version) { }
        // RVA: 0x0AD6A82C  token: 0x60003DD
        private static System.Data.DataRow[] GetParentRows(System.Data.DataKey parentKey, System.Data.DataKey childKey, System.Data.DataRow childRow, System.Data.DataRowVersion version) { }
        // RVA: 0x0AD6A6D4  token: 0x60003DE
        private static System.Data.DataRow GetParentRow(System.Data.DataKey parentKey, System.Data.DataKey childKey, System.Data.DataRow childRow, System.Data.DataRowVersion version) { }
        // RVA: 0x0AD6AC1C  token: 0x60003DF
        private System.Void SetDataSet(System.Data.DataSet dataSet) { }
        // RVA: 0x0AD693DC  token: 0x60003E5
        private System.Void CheckNamespaceValidityForNestedRelations(System.String ns) { }
        // RVA: 0x0AD69618  token: 0x60003E6
        private System.Void CheckNestedRelations() { }
        // RVA: 0x046A91B0  token: 0x60003EA
        private System.Void SetParentKeyConstraint(System.Data.UniqueConstraint value) { }
        // RVA: 0x042B4AE0  token: 0x60003EF
        private System.Void SetChildKeyConstraint(System.Data.ForeignKeyConstraint value) { }
        // RVA: 0x0AD69A60  token: 0x60003F0
        private System.Void CheckState() { }
        // RVA: 0x0AD69948  token: 0x60003F1
        protected System.Void CheckStateForProperty() { }
        // RVA: 0x0AD6A2A8  token: 0x60003F2
        private System.Void Create(System.String relationName, System.Data.DataColumn[] parentColumns, System.Data.DataColumn[] childColumns, System.Boolean createConstraints) { }
        // RVA: 0x0AD69CD4  token: 0x60003F3
        private System.Data.DataRelation Clone(System.Data.DataSet destination) { }
        // RVA: 0x0AD6AAEC  token: 0x60003F4
        protected internal System.Void OnPropertyChanging(System.ComponentModel.PropertyChangedEventArgs pcevent) { }
        // RVA: 0x0AD6ABA4  token: 0x60003F5
        protected internal System.Void RaisePropertyChanging(System.String name) { }
        // RVA: 0x0AD6AC30  token: 0x60003F6
        public virtual System.String ToString() { }
        // RVA: 0x0AD6AC4C  token: 0x60003F7
        private System.Void ValidateMultipleNestedRelations() { }
        // RVA: 0x0AD6A8C4  token: 0x60003F8
        private System.Boolean IsAutoGenerated(System.Data.DataColumn col) { }

    }

    // TypeToken: 0x2000037  // size: 0x38
    public abstract class DataRelationCollection : System.Data.InternalDataCollectionBase
    {
        // Fields
        private System.Data.DataRelation _inTransition;  // 0x10
        private System.Int32 _defaultNameIndex;  // 0x18
        private System.ComponentModel.CollectionChangeEventHandler _onCollectionChangedDelegate;  // 0x20
        private System.ComponentModel.CollectionChangeEventHandler _onCollectionChangingDelegate;  // 0x28
        private static System.Int32 s_objectTypeCount;  // static @ 0x0
        private readonly System.Int32 _objectID;  // 0x30

        // Properties
        System.Int32 ObjectID { get; /* RVA: 0x04D864F0 */ }
        System.Data.DataRelation Item { get; /* RVA: -1  // abstract */ }
        System.Data.DataRelation Item { get; /* RVA: -1  // abstract */ }

        // Events
        event System.ComponentModel.CollectionChangeEventHandler CollectionChanged;

        // Methods
        // RVA: 0x0AD680F8  token: 0x60003FD
        public System.Void Add(System.Data.DataRelation relation) { }
        // RVA: 0x0AD67E8C  token: 0x60003FE
        protected virtual System.Void AddCore(System.Data.DataRelation relation) { }
        // RVA: 0x0AD68398  token: 0x6000401
        private System.String AssignName() { }
        // RVA: 0x0AD683B8  token: 0x6000402
        public virtual System.Void Clear() { }
        // RVA: 0x0AD685F4  token: 0x6000403
        public virtual System.Boolean Contains(System.String name) { }
        // RVA: 0x0AD6860C  token: 0x6000404
        private System.Int32 InternalIndexOf(System.String name) { }
        // RVA: -1  // abstract  token: 0x6000405
        protected virtual System.Data.DataSet GetDataSet() { }
        // RVA: 0x0AD6873C  token: 0x6000406
        private System.String MakeName(System.Int32 index) { }
        // RVA: 0x0AD687C8  token: 0x6000407
        protected virtual System.Void OnCollectionChanged(System.ComponentModel.CollectionChangeEventArgs ccevent) { }
        // RVA: 0x0AD68878  token: 0x6000408
        protected virtual System.Void OnCollectionChanging(System.ComponentModel.CollectionChangeEventArgs ccevent) { }
        // RVA: 0x0AD68928  token: 0x6000409
        private System.Void RegisterName(System.String name) { }
        // RVA: 0x0AD68CD0  token: 0x600040A
        public System.Void Remove(System.Data.DataRelation relation) { }
        // RVA: 0x0AD68AC0  token: 0x600040B
        public System.Void RemoveAt(System.Int32 index) { }
        // RVA: 0x0AD68B3C  token: 0x600040C
        protected virtual System.Void RemoveCore(System.Data.DataRelation relation) { }
        // RVA: 0x0AD68EB0  token: 0x600040D
        private System.Void UnregisterName(System.String name) { }
        // RVA: 0x0AD68FC4  token: 0x600040E
        protected System.Void .ctor() { }

    }

    // TypeToken: 0x200003A  // size: 0x90
    public sealed class DataRelationPropertyDescriptor : System.ComponentModel.PropertyDescriptor
    {
        // Fields
        private readonly System.Data.DataRelation <Relation>k__BackingField;  // 0x88

        // Properties
        System.Data.DataRelation Relation { get; /* RVA: 0x04D861F0 */ }
        System.Type ComponentType { get; /* RVA: 0x0AD6933C */ }
        System.Boolean IsReadOnly { get; /* RVA: 0x012081B0 */ }
        System.Type PropertyType { get; /* RVA: 0x0AD6938C */ }

        // Methods
        // RVA: 0x0AD692E8  token: 0x6000423
        private System.Void .ctor(System.Data.DataRelation dataRelation) { }
        // RVA: 0x0AD69210  token: 0x6000428
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x0AD66A3C  token: 0x6000429
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x012081B0  token: 0x600042A
        public virtual System.Boolean CanResetValue(System.Object component) { }
        // RVA: 0x0AD69270  token: 0x600042B
        public virtual System.Object GetValue(System.Object component) { }
        // RVA: 0x041E1670  token: 0x600042C
        public virtual System.Void ResetValue(System.Object component) { }
        // RVA: 0x041E1670  token: 0x600042D
        public virtual System.Void SetValue(System.Object component, System.Object value) { }
        // RVA: 0x012081B0  token: 0x600042E
        public virtual System.Boolean ShouldSerializeValue(System.Object component) { }

    }

    // TypeToken: 0x200003B  // size: 0x60
    public class DataRow
    {
        // Fields
        private readonly System.Data.DataTable _table;  // 0x10
        private readonly System.Data.DataColumnCollection _columns;  // 0x18
        private System.Int32 _oldRecord;  // 0x20
        private System.Int32 _newRecord;  // 0x24
        private System.Int32 _tempRecord;  // 0x28
        private System.Int64 _rowID;  // 0x30
        private System.Data.DataRowAction _action;  // 0x38
        private System.Boolean _inChangingEvent;  // 0x3c
        private System.Boolean _inDeletingEvent;  // 0x3d
        private System.Boolean _inCascade;  // 0x3e
        private System.Data.DataColumn _lastChangedColumn;  // 0x40
        private System.Int32 _countColumnChange;  // 0x48
        private System.Data.DataError _error;  // 0x50
        private System.Int32 _rbTreeNodeId;  // 0x58
        private static System.Int32 s_objectTypeCount;  // static @ 0x0
        private readonly System.Int32 _objectID;  // 0x5c

        // Properties
        System.Data.DataColumn LastChangedColumn { get; /* RVA: 0x0AD6F624 */ set; /* RVA: 0x0AD6FF94 */ }
        System.Boolean HasPropertyChanged { get; /* RVA: 0x0AD6F584 */ }
        System.Int32 RBTreeNodeId { get; /* RVA: 0x04D86570 */ set; /* RVA: 0x0AD6FFA4 */ }
        System.String RowError { get; /* RVA: 0x0AD6F634 */ set; /* RVA: 0x0AD7003C */ }
        System.Int64 rowID { get; /* RVA: 0x011F36E0 */ set; /* RVA: 0x0AD70164 */ }
        System.Data.DataRowState RowState { get; /* RVA: 0x0AD6F684 */ }
        System.Data.DataTable Table { get; /* RVA: 0x02B2ECC0 */ }
        System.String Item { set; /* RVA: 0x0AD6FC14 */ }
        System.Object Item { get; /* RVA: 0x0AD6F5E0 */ set; /* RVA: 0x0AD6FC14 */ }
        System.Object Item { get; /* RVA: 0x0AD6F5E0 */ }
        System.Object[] ItemArray { set; /* RVA: 0x0AD6F7D0 */ }
        System.Boolean HasErrors { get; /* RVA: 0x0AD6F56C */ }

        // Methods
        // RVA: 0x0AD6F4C4  token: 0x600042F
        protected internal System.Void .ctor(System.Data.DataRowBuilder builder) { }
        // RVA: 0x0AD6EBB0  token: 0x6000437
        private System.Void RowErrorChanged() { }
        // RVA: 0x0AD6D5D0  token: 0x600043C
        private System.Void CheckForLoops(System.Data.DataRelation rel) { }
        // RVA: 0x0AD6DF4C  token: 0x600043D
        private System.Int32 GetNestedParentCount() { }
        // RVA: 0x0AD6D0DC  token: 0x6000443
        public System.Void AcceptChanges() { }
        // RVA: 0x0AD6D4E0  token: 0x6000444
        public System.Void BeginEdit() { }
        // RVA: 0x0AD6D424  token: 0x6000445
        private System.Boolean BeginEditInternal() { }
        // RVA: 0x0AD6D4E8  token: 0x6000446
        public System.Void CancelEdit() { }
        // RVA: 0x0AD6D544  token: 0x6000447
        private System.Void CheckColumn(System.Data.DataColumn column) { }
        // RVA: 0x0AD6D6A8  token: 0x6000448
        private System.Void CheckInTable() { }
        // RVA: 0x0AD6DA20  token: 0x6000449
        public System.Void Delete() { }
        // RVA: 0x0AD6DA78  token: 0x600044A
        public System.Void EndEdit() { }
        // RVA: 0x0AD6EE18  token: 0x600044B
        public System.Void SetColumnError(System.Int32 columnIndex, System.String error) { }
        // RVA: 0x0AD6EBFC  token: 0x600044C
        public System.Void SetColumnError(System.Data.DataColumn column, System.String error) { }
        // RVA: 0x0AD6DC88  token: 0x600044D
        public System.String GetColumnError(System.Data.DataColumn column) { }
        // RVA: 0x0AD6D718  token: 0x600044E
        public System.Void ClearErrors() { }
        // RVA: 0x0AD6D6E4  token: 0x600044F
        private System.Void ClearError(System.Data.DataColumn column) { }
        // RVA: 0x0AD6DD7C  token: 0x6000451
        public System.Data.DataColumn[] GetColumnsInError() { }
        // RVA: 0x0AD6DB4C  token: 0x6000452
        public System.Data.DataRow[] GetChildRows(System.Data.DataRelation relation) { }
        // RVA: 0x0AD6DB5C  token: 0x6000453
        public System.Data.DataRow[] GetChildRows(System.Data.DataRelation relation, System.Data.DataRowVersion version) { }
        // RVA: 0x0AD6DE14  token: 0x6000454
        private System.Data.DataColumn GetDataColumn(System.String columnName) { }
        // RVA: 0x0AD6E200  token: 0x6000455
        public System.Data.DataRow GetParentRow(System.Data.DataRelation relation) { }
        // RVA: 0x0AD6E0E4  token: 0x6000456
        public System.Data.DataRow GetParentRow(System.Data.DataRelation relation, System.Data.DataRowVersion version) { }
        // RVA: 0x0AD6DFF8  token: 0x6000457
        private System.Data.DataRow GetNestedParentRow(System.Data.DataRowVersion version) { }
        // RVA: 0x0AD6E33C  token: 0x6000458
        public System.Data.DataRow[] GetParentRows(System.Data.DataRelation relation) { }
        // RVA: 0x0AD6E210  token: 0x6000459
        public System.Data.DataRow[] GetParentRows(System.Data.DataRelation relation, System.Data.DataRowVersion version) { }
        // RVA: 0x0AD6DD6C  token: 0x600045A
        private System.Object[] GetColumnValues(System.Data.DataColumn[] columns) { }
        // RVA: 0x0AD6DD24  token: 0x600045B
        private System.Object[] GetColumnValues(System.Data.DataColumn[] columns, System.Data.DataRowVersion version) { }
        // RVA: 0x0AD6DF00  token: 0x600045C
        private System.Object[] GetKeyValues(System.Data.DataKey key) { }
        // RVA: 0x0AD6DF24  token: 0x600045D
        private System.Object[] GetKeyValues(System.Data.DataKey key, System.Data.DataRowVersion version) { }
        // RVA: 0x0AD6DDD4  token: 0x600045E
        private System.Int32 GetCurrentRecordNo() { }
        // RVA: 0x0AD6DE84  token: 0x600045F
        private System.Int32 GetDefaultRecord() { }
        // RVA: 0x0AD6E0A4  token: 0x6000460
        private System.Int32 GetOriginalRecordNo() { }
        // RVA: 0x0AD6E34C  token: 0x6000461
        private System.Int32 GetProposedRecordNo() { }
        // RVA: 0x0AD6E38C  token: 0x6000462
        private System.Int32 GetRecordFromVersion(System.Data.DataRowVersion version) { }
        // RVA: 0x0AD6DED8  token: 0x6000463
        private System.Data.DataRowVersion GetDefaultRowVersion(System.Data.DataViewRowState viewState) { }
        // RVA: 0x0AD6E40C  token: 0x6000464
        private System.Data.DataViewRowState GetRecordState(System.Int32 record) { }
        // RVA: 0x0AD6E450  token: 0x6000465
        private System.Boolean HasKeyChanged(System.Data.DataKey key) { }
        // RVA: 0x0AD6E474  token: 0x6000466
        private System.Boolean HasKeyChanged(System.Data.DataKey key, System.Data.DataRowVersion version1, System.Data.DataRowVersion version2) { }
        // RVA: 0x0AD6E4F4  token: 0x6000467
        public System.Boolean HasVersion(System.Data.DataRowVersion version) { }
        // RVA: 0x0AD6E564  token: 0x6000468
        private System.Boolean HaveValuesChanged(System.Data.DataColumn[] columns) { }
        // RVA: 0x0AD6E588  token: 0x6000469
        private System.Boolean HaveValuesChanged(System.Data.DataColumn[] columns, System.Data.DataRowVersion version1, System.Data.DataRowVersion version2) { }
        // RVA: 0x0AD6E61C  token: 0x600046A
        public System.Void RejectChanges() { }
        // RVA: 0x0AD6EB90  token: 0x600046B
        private System.Void ResetLastChangedColumn() { }
        // RVA: 0x0AD6EE90  token: 0x600046C
        private System.Void SetKeyValues(System.Data.DataKey key, System.Object[] keyValues) { }
        // RVA: 0x0AD6EF88  token: 0x600046D
        private System.Void SetNestedParentRow(System.Data.DataRow parentRow, System.Boolean setNonNested) { }
        // RVA: 0x0AD6F1F8  token: 0x600046E
        private System.Void SetParentRowToDBNull() { }
        // RVA: 0x0AD6F370  token: 0x600046F
        private System.Void SetParentRowToDBNull(System.Data.DataRelation relation) { }
        // RVA: 0x0AD6D748  token: 0x6000470
        private System.Int32 CopyValuesIntoStore(System.Collections.ArrayList storeList, System.Collections.ArrayList nullbitList, System.Int32 storeIndex) { }

    }

    // TypeToken: 0x200003C  // size: 0x20
    public sealed class DataRowBuilder
    {
        // Fields
        private readonly System.Data.DataTable _table;  // 0x10
        private System.Int32 _record;  // 0x18

        // Methods
        // RVA: 0x03B46FF0  token: 0x6000471
        private System.Void .ctor(System.Data.DataTable table, System.Int32 record) { }

    }

    // TypeToken: 0x200003D  // size: 0x14
    public sealed struct DataRowAction
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Data.DataRowAction Nothing;  // const
        public static System.Data.DataRowAction Delete;  // const
        public static System.Data.DataRowAction Change;  // const
        public static System.Data.DataRowAction Rollback;  // const
        public static System.Data.DataRowAction Commit;  // const
        public static System.Data.DataRowAction Add;  // const
        public static System.Data.DataRowAction ChangeOriginal;  // const
        public static System.Data.DataRowAction ChangeCurrentAndOriginal;  // const

    }

    // TypeToken: 0x200003E  // size: 0x20
    public class DataRowChangeEventArgs : System.EventArgs
    {
        // Fields
        private readonly System.Data.DataRow <Row>k__BackingField;  // 0x10
        private readonly System.Data.DataRowAction <Action>k__BackingField;  // 0x18

        // Methods
        // RVA: 0x0AD6C28C  token: 0x6000472
        public System.Void .ctor(System.Data.DataRow row, System.Data.DataRowAction action) { }

    }

    // TypeToken: 0x200003F  // size: 0x80
    public sealed class DataRowChangeEventHandler : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x086FC23C  token: 0x6000473
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x038E1420  token: 0x6000474
        public virtual System.Void Invoke(System.Object sender, System.Data.DataRowChangeEventArgs e) { }

    }

    // TypeToken: 0x2000040  // size: 0x28
    public sealed class DataRowCollection : System.Data.InternalDataCollectionBase
    {
        // Fields
        private readonly System.Data.DataTable _table;  // 0x10
        private readonly System.Data.DataRowCollection.DataRowTree _list;  // 0x18
        private System.Int32 _nullInList;  // 0x20

        // Properties
        System.Int32 Count { get; /* RVA: 0x0AD6C938 */ }
        System.Data.DataRow Item { get; /* RVA: 0x0AD6C958 */ }

        // Methods
        // RVA: 0x0AD6C8A0  token: 0x6000475
        private System.Void .ctor(System.Data.DataTable table) { }
        // RVA: 0x0AD6C35C  token: 0x6000478
        public System.Void Add(System.Data.DataRow row) { }
        // RVA: 0x0AD6C608  token: 0x6000479
        private System.Void DiffInsertAt(System.Data.DataRow row, System.Int32 pos) { }
        // RVA: 0x0AD6C81C  token: 0x600047A
        public System.Int32 IndexOf(System.Data.DataRow row) { }
        // RVA: 0x0AD6C2EC  token: 0x600047B
        private System.Data.DataRow AddWithColumnEvents(System.Object[] values) { }
        // RVA: 0x0AD6C384  token: 0x600047C
        private System.Void ArrayAdd(System.Data.DataRow row) { }
        // RVA: 0x0AD6C434  token: 0x600047D
        private System.Void ArrayInsert(System.Data.DataRow row, System.Int32 pos) { }
        // RVA: 0x0AD6C3EC  token: 0x600047E
        private System.Void ArrayClear() { }
        // RVA: 0x0AD6C4AC  token: 0x600047F
        private System.Void ArrayRemove(System.Data.DataRow row) { }
        // RVA: 0x0AD6C5A4  token: 0x6000480
        public virtual System.Void CopyTo(System.Array ar, System.Int32 index) { }
        // RVA: 0x0AD6C540  token: 0x6000481
        public System.Void CopyTo(System.Data.DataRow[] array, System.Int32 index) { }
        // RVA: 0x0AD6C7D4  token: 0x6000482
        public virtual System.Collections.IEnumerator GetEnumerator() { }

    }

    // TypeToken: 0x2000042  // size: 0x80
    public sealed class DataRowCreatedEventHandler : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x086FC23C  token: 0x6000486
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x038E1420  token: 0x6000487
        public virtual System.Void Invoke(System.Object sender, System.Data.DataRow r) { }

    }

    // TypeToken: 0x2000043  // size: 0x80
    public sealed class DataSetClearEventhandler : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x086FC23C  token: 0x6000488
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x038E1420  token: 0x6000489
        public virtual System.Void Invoke(System.Object sender, System.Data.DataTable table) { }

    }

    // TypeToken: 0x2000044  // size: 0x14
    public sealed struct DataRowState
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Data.DataRowState Detached;  // const
        public static System.Data.DataRowState Unchanged;  // const
        public static System.Data.DataRowState Added;  // const
        public static System.Data.DataRowState Deleted;  // const
        public static System.Data.DataRowState Modified;  // const

    }

    // TypeToken: 0x2000045  // size: 0x14
    public sealed struct DataRowVersion
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Data.DataRowVersion Original;  // const
        public static System.Data.DataRowVersion Current;  // const
        public static System.Data.DataRowVersion Proposed;  // const
        public static System.Data.DataRowVersion Default;  // const

    }

    // TypeToken: 0x2000046  // size: 0x30
    public class DataRowView : System.ComponentModel.ICustomTypeDescriptor, System.ComponentModel.INotifyPropertyChanged
    {
        // Fields
        private readonly System.Data.DataView _dataView;  // 0x10
        private readonly System.Data.DataRow _row;  // 0x18
        private System.Boolean _delayBeginEdit;  // 0x20
        private static readonly System.ComponentModel.PropertyDescriptorCollection s_zeroPropertyDescriptorCollection;  // static @ 0x0
        private System.ComponentModel.PropertyChangedEventHandler PropertyChanged;  // 0x28

        // Properties
        System.Data.DataView DataView { get; /* RVA: 0x02B2ECC0 */ }
        System.Data.DataRowVersion RowVersionDefault { get; /* RVA: 0x0AD6D018 */ }
        System.Data.DataRow Row { get; /* RVA: 0x0385B100 */ }
        System.Boolean IsNew { get; /* RVA: 0x0AD6CFF4 */ }

        // Events
        event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        // Methods
        // RVA: 0x032AF420  token: 0x600048A
        private System.Void .ctor(System.Data.DataView dataView, System.Data.DataRow row) { }
        // RVA: 0x02FB1840  token: 0x600048B
        public virtual System.Boolean Equals(System.Object other) { }
        // RVA: 0x04D80F90  token: 0x600048C
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AD6CC84  token: 0x600048F
        private System.Int32 GetRecord() { }
        // RVA: 0x0AD6CCB4  token: 0x6000490
        private System.Boolean HasRecord() { }
        // RVA: 0x0AD6CC44  token: 0x6000491
        private System.Object GetColumnValue(System.Data.DataColumn column) { }
        // RVA: 0x0AD6CD74  token: 0x6000492
        private System.Void SetColumnValue(System.Data.DataColumn column, System.Object value) { }
        // RVA: 0x0AD6CA4C  token: 0x6000493
        public System.Data.DataView CreateChildView(System.Data.DataRelation relation, System.Boolean followParent) { }
        // RVA: 0x0AD6CBEC  token: 0x6000494
        public System.Data.DataView CreateChildView(System.Data.DataRelation relation) { }
        // RVA: 0x0AD6CBF8  token: 0x6000496
        public virtual System.Void EndEdit() { }
        // RVA: 0x0AD6CCE4  token: 0x600049A
        private System.Void RaisePropertyChangedEvent(System.String propName) { }
        // RVA: 0x0AD6CE10  token: 0x600049B
        private virtual System.ComponentModel.AttributeCollection System.ComponentModel.ICustomTypeDescriptor.GetAttributes() { }
        // RVA: 0x011EC580  token: 0x600049C
        private virtual System.ComponentModel.TypeConverter System.ComponentModel.ICustomTypeDescriptor.GetConverter() { }
        // RVA: 0x0A8C59CC  token: 0x600049D
        private virtual System.ComponentModel.PropertyDescriptorCollection System.ComponentModel.ICustomTypeDescriptor.GetProperties() { }
        // RVA: 0x0AD6CE64  token: 0x600049E
        private virtual System.ComponentModel.PropertyDescriptorCollection System.ComponentModel.ICustomTypeDescriptor.GetProperties(System.Attribute[] attributes) { }
        // RVA: 0x0464E630  token: 0x600049F
        private virtual System.Object System.ComponentModel.ICustomTypeDescriptor.GetPropertyOwner(System.ComponentModel.PropertyDescriptor pd) { }
        // RVA: 0x0AD6CEE0  token: 0x60004A0
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000047  // size: 0x14
    public sealed struct SerializationFormat
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Data.SerializationFormat Xml;  // const
        public static System.Data.SerializationFormat Binary;  // const

    }

    // TypeToken: 0x2000048  // size: 0x14
    public sealed struct DataSetDateTime
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Data.DataSetDateTime Local;  // const
        public static System.Data.DataSetDateTime Unspecified;  // const
        public static System.Data.DataSetDateTime UnspecifiedLocal;  // const
        public static System.Data.DataSetDateTime Utc;  // const

    }

    // TypeToken: 0x2000049  // size: 0x18
    public sealed class DataTableClearEventArgs : System.EventArgs
    {
        // Fields
        private readonly System.Data.DataTable <Table>k__BackingField;  // 0x10

        // Methods
        // RVA: 0x0AD70BA8  token: 0x60004A1
        public System.Void .ctor(System.Data.DataTable dataTable) { }

    }

    // TypeToken: 0x200004A  // size: 0x80
    public sealed class DataTableClearEventHandler : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x086FC23C  token: 0x60004A2
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x038E1420  token: 0x60004A3
        public virtual System.Void Invoke(System.Object sender, System.Data.DataTableClearEventArgs e) { }

    }

    // TypeToken: 0x200004B  // size: 0x48
    public sealed class DataTableCollection : System.Data.InternalDataCollectionBase
    {
        // Fields
        private readonly System.Data.DataSet _dataSet;  // 0x10
        private readonly System.Collections.ArrayList _list;  // 0x18
        private System.Int32 _defaultNameIndex;  // 0x20
        private System.Data.DataTable[] _delayedAddRangeTables;  // 0x28
        private System.ComponentModel.CollectionChangeEventHandler _onCollectionChangedDelegate;  // 0x30
        private System.ComponentModel.CollectionChangeEventHandler _onCollectionChangingDelegate;  // 0x38
        private static System.Int32 s_objectTypeCount;  // static @ 0x0
        private readonly System.Int32 _objectID;  // 0x40

        // Properties
        System.Collections.ArrayList List { get; /* RVA: 0x0385B100 */ }
        System.Int32 ObjectID { get; /* RVA: 0x04D865E0 */ }
        System.Data.DataTable Item { get; /* RVA: 0x0AD72DB8 */ }
        System.Data.DataTable Item { get; /* RVA: 0x0AD72DB8 */ }
        System.Data.DataTable Item { get; /* RVA: 0x0AD72DB8 */ }

        // Methods
        // RVA: 0x0AD72BA8  token: 0x60004A4
        private System.Void .ctor(System.Data.DataSet dataSet) { }
        // RVA: 0x0AD71E54  token: 0x60004AA
        private System.Data.DataTable GetTable(System.String name, System.String ns) { }
        // RVA: 0x0AD71D48  token: 0x60004AB
        private System.Data.DataTable GetTableSmart(System.String name, System.String ns) { }
        // RVA: 0x0AD70C00  token: 0x60004AC
        public System.Void Add(System.Data.DataTable table) { }
        // RVA: 0x0AD70F34  token: 0x60004AD
        private System.Void ArrayAdd(System.Data.DataTable table) { }
        // RVA: 0x0AD70F58  token: 0x60004AE
        private System.String AssignName() { }
        // RVA: 0x0AD70FA0  token: 0x60004AF
        private System.Void BaseAdd(System.Data.DataTable table) { }
        // RVA: 0x0AD7112C  token: 0x60004B0
        private System.Void BaseGroupSwitch(System.Data.DataTable[] oldArray, System.Int32 oldLength, System.Data.DataTable[] newArray, System.Int32 newLength) { }
        // RVA: 0x0AD7129C  token: 0x60004B1
        private System.Void BaseRemove(System.Data.DataTable table) { }
        // RVA: 0x0AD7131C  token: 0x60004B2
        private System.Boolean CanRemove(System.Data.DataTable table, System.Boolean fThrowException) { }
        // RVA: 0x0AD71864  token: 0x60004B3
        public System.Void Clear() { }
        // RVA: 0x0AD71BE0  token: 0x60004B4
        public System.Boolean Contains(System.String name) { }
        // RVA: 0x0AD71BF8  token: 0x60004B5
        private System.Boolean Contains(System.String name, System.String tableNamespace, System.Boolean checkProperty, System.Boolean caseSensitive) { }
        // RVA: 0x0AD71AE8  token: 0x60004B6
        private System.Boolean Contains(System.String name, System.Boolean caseSensitive) { }
        // RVA: 0x0AD71FF0  token: 0x60004B7
        public System.Int32 IndexOf(System.Data.DataTable table) { }
        // RVA: 0x0AD71F44  token: 0x60004B8
        public System.Int32 IndexOf(System.String tableName) { }
        // RVA: 0x0AD71F60  token: 0x60004B9
        private System.Int32 IndexOf(System.String tableName, System.String tableNamespace, System.Boolean chekforNull) { }
        // RVA: 0x0AD72A58  token: 0x60004BA
        private System.Void ReplaceFromInference(System.Collections.Generic.List<System.Data.DataTable> tableList) { }
        // RVA: 0x0AD72094  token: 0x60004BB
        private System.Int32 InternalIndexOf(System.String tableName) { }
        // RVA: 0x0AD7222C  token: 0x60004BC
        private System.Int32 InternalIndexOf(System.String tableName, System.String tableNamespace) { }
        // RVA: 0x0AD72394  token: 0x60004BD
        private System.String MakeName(System.Int32 index) { }
        // RVA: 0x0AD72420  token: 0x60004BE
        private System.Void OnCollectionChanged(System.ComponentModel.CollectionChangeEventArgs ccevent) { }
        // RVA: 0x0AD724D0  token: 0x60004BF
        private System.Void OnCollectionChanging(System.ComponentModel.CollectionChangeEventArgs ccevent) { }
        // RVA: 0x0AD72580  token: 0x60004C0
        private System.Void RegisterName(System.String name, System.String tbNamespace) { }
        // RVA: 0x0AD727A8  token: 0x60004C1
        public System.Void Remove(System.Data.DataTable table) { }
        // RVA: 0x0AD72AA4  token: 0x60004C2
        private System.Void UnregisterName(System.String name) { }

    }

    // TypeToken: 0x200004C  // size: 0x18
    public sealed class DataTableNewRowEventArgs : System.EventArgs
    {
        // Fields
        private readonly System.Data.DataRow <Row>k__BackingField;  // 0x10

        // Methods
        // RVA: 0x0AD7CD80  token: 0x60004C3
        public System.Void .ctor(System.Data.DataRow dataRow) { }

    }

    // TypeToken: 0x200004D  // size: 0x80
    public sealed class DataTableNewRowEventHandler : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x086FC23C  token: 0x60004C4
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x038E1420  token: 0x60004C5
        public virtual System.Void Invoke(System.Object sender, System.Data.DataTableNewRowEventArgs e) { }

    }

    // TypeToken: 0x200004E  // size: 0x18
    public sealed class DataTableTypeConverter : System.ComponentModel.ReferenceConverter
    {
        // Methods
        // RVA: 0x0AD7CDD8  token: 0x60004C6
        public System.Void .ctor() { }
        // RVA: 0x012081B0  token: 0x60004C7
        public virtual System.Boolean GetPropertiesSupported(System.ComponentModel.ITypeDescriptorContext context) { }

    }

    // TypeToken: 0x200004F  // size: 0xB0
    public class DataView : System.ComponentModel.MarshalByValueComponent, System.ComponentModel.IBindingList, System.Collections.IList, System.Collections.ICollection, System.Collections.IEnumerable
    {
        // Fields
        private System.Data.DataViewManager _dataViewManager;  // 0x20
        private System.Data.DataTable _table;  // 0x28
        private System.Boolean _locked;  // 0x30
        private System.Data.Index _index;  // 0x38
        private System.Collections.Generic.Dictionary<System.String,System.Data.Index> _findIndexes;  // 0x40
        private System.String _sort;  // 0x48
        private System.Comparison<System.Data.DataRow> _comparison;  // 0x50
        private System.Data.IFilter _rowFilter;  // 0x58
        private System.Data.DataViewRowState _recordStates;  // 0x60
        private System.Boolean _shouldOpen;  // 0x64
        private System.Boolean _open;  // 0x65
        private System.Boolean _allowNew;  // 0x66
        private System.Boolean _allowEdit;  // 0x67
        private System.Boolean _allowDelete;  // 0x68
        private System.Boolean _applyDefaultSort;  // 0x69
        private System.Data.DataRow _addNewRow;  // 0x70
        private System.ComponentModel.ListChangedEventArgs _addNewMoved;  // 0x78
        private System.ComponentModel.ListChangedEventHandler _onListChanged;  // 0x80
        private static System.ComponentModel.ListChangedEventArgs s_resetEventArgs;  // static @ 0x0
        private System.Data.DataViewRowState _delayedRecordStates;  // 0x88
        private System.Boolean _fEndInitInProgress;  // 0x8c
        private System.Collections.Generic.Dictionary<System.Data.DataRow,System.Data.DataRowView> _rowViewCache;  // 0x90
        private readonly System.Collections.Generic.Dictionary<System.Data.DataRow,System.Data.DataRowView> _rowViewBuffer;  // 0x98
        private System.Data.DataViewListener _dvListener;  // 0xa0
        private static System.Int32 s_objectTypeCount;  // static @ 0x8
        private readonly System.Int32 _objectID;  // 0xa8

        // Properties
        System.Boolean AllowDelete { get; /* RVA: 0x04D86B40 */ }
        System.Boolean AllowNew { get; /* RVA: 0x04DAA1D0 */ }
        System.Int32 Count { get; /* RVA: 0x0AD80414 */ }
        System.Int32 CountFromIndex { get; /* RVA: 0x0AD803F0 */ }
        System.Data.DataViewManager DataViewManager { get; /* RVA: 0x04D862C0 */ }
        System.Boolean IsOpen { get; /* RVA: 0x04DAA1E0 */ }
        System.Boolean System.Collections.ICollection.IsSynchronized { get; /* RVA: 0x012081B0 */ }
        System.Data.DataViewRowState RowStateFilter { get; /* RVA: 0x04D86790 */ }
        System.String Sort { get; /* RVA: 0x0AD8047C */ }
        System.Comparison<System.Data.DataRow> SortComparison { get; /* RVA: 0x04D86270 */ }
        System.Object System.Collections.ICollection.SyncRoot { get; /* RVA: 0x0464E630 */ }
        System.Data.DataTable Table { get; /* RVA: 0x04D86240 */ }
        System.Object System.Collections.IList.Item { get; /* RVA: 0x0AD7FD38 */ set; /* RVA: 0x0AD7FD40 */ }
        System.Data.DataRowView Item { get; /* RVA: 0x0AD80458 */ }
        System.Boolean System.Collections.IList.IsReadOnly { get; /* RVA: 0x012081B0 */ }
        System.Boolean System.Collections.IList.IsFixedSize { get; /* RVA: 0x012081B0 */ }
        System.Int32 ObjectID { get; /* RVA: 0x04D88C80 */ }

        // Methods
        // RVA: 0x0AD8012C  token: 0x60004C8
        private System.Void .ctor(System.Data.DataTable table, System.Boolean locked) { }
        // RVA: 0x0AD7DCC8  token: 0x60004D8
        public virtual System.Data.DataRowView AddNew() { }
        // RVA: 0x0AD7DFA4  token: 0x60004D9
        private System.Void CheckOpen() { }
        // RVA: 0x0AD7E150  token: 0x60004DA
        protected System.Void Close() { }
        // RVA: 0x0AD7E304  token: 0x60004DB
        public virtual System.Void CopyTo(System.Array array, System.Int32 index) { }
        // RVA: 0x0AD7E43C  token: 0x60004DC
        private System.Void CopyTo(System.Data.DataRowView[] array, System.Int32 index) { }
        // RVA: 0x0AD7E728  token: 0x60004DD
        public System.Void Delete(System.Int32 index) { }
        // RVA: 0x0AD7E594  token: 0x60004DE
        private System.Void Delete(System.Data.DataRow row) { }
        // RVA: 0x0AD7E74C  token: 0x60004DF
        protected virtual System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x0AD7E780  token: 0x60004E0
        private System.Void FinishAddNew(System.Boolean success) { }
        // RVA: 0x0AD7E920  token: 0x60004E1
        public virtual System.Collections.IEnumerator GetEnumerator() { }
        // RVA: 0x0AD7FB48  token: 0x60004E4
        private virtual System.Int32 System.Collections.IList.Add(System.Object value) { }
        // RVA: 0x0AD7FBA8  token: 0x60004E5
        private virtual System.Void System.Collections.IList.Clear() { }
        // RVA: 0x0AD7FBD0  token: 0x60004E6
        private virtual System.Boolean System.Collections.IList.Contains(System.Object value) { }
        // RVA: 0x0AD7FC2C  token: 0x60004E7
        private virtual System.Int32 System.Collections.IList.IndexOf(System.Object value) { }
        // RVA: 0x0AD7ECBC  token: 0x60004E8
        private System.Int32 IndexOf(System.Data.DataRowView rowview) { }
        // RVA: 0x0AD7EC5C  token: 0x60004E9
        private System.Int32 IndexOfDataRowView(System.Data.DataRowView rowview) { }
        // RVA: 0x0AD7FC80  token: 0x60004EA
        private virtual System.Void System.Collections.IList.Insert(System.Int32 index, System.Object value) { }
        // RVA: 0x0AD7FCB0  token: 0x60004EB
        private virtual System.Void System.Collections.IList.Remove(System.Object value) { }
        // RVA: 0x0AD7FCA8  token: 0x60004EC
        private virtual System.Void System.Collections.IList.RemoveAt(System.Int32 index) { }
        // RVA: 0x02E56440  token: 0x60004ED
        private virtual System.Data.IFilter GetFilter() { }
        // RVA: 0x0AD7E994  token: 0x60004EE
        private System.Int32 GetRecord(System.Int32 recordIndex) { }
        // RVA: 0x0AD7EAAC  token: 0x60004EF
        private System.Data.DataRow GetRow(System.Int32 index) { }
        // RVA: 0x0AD7EA70  token: 0x60004F0
        private System.Data.DataRowView GetRowView(System.Int32 record) { }
        // RVA: 0x0AD7EA18  token: 0x60004F1
        private System.Data.DataRowView GetRowView(System.Data.DataRow dr) { }
        // RVA: 0x0AD7EBEC  token: 0x60004F2
        protected virtual System.Void IndexListChanged(System.Object sender, System.ComponentModel.ListChangedEventArgs e) { }
        // RVA: 0x0AD7EB44  token: 0x60004F3
        private System.Void IndexListChangedInternal(System.ComponentModel.ListChangedEventArgs e) { }
        // RVA: 0x0AD7ED78  token: 0x60004F4
        private System.Void MaintainDataView(System.ComponentModel.ListChangedType changedType, System.Data.DataRow row, System.Boolean trackAddRemove) { }
        // RVA: 0x0AD7F020  token: 0x60004F5
        protected virtual System.Void OnListChanged(System.ComponentModel.ListChangedEventArgs e) { }
        // RVA: 0x0AD7F6C8  token: 0x60004F6
        protected System.Void Reset() { }
        // RVA: 0x0AD7F498  token: 0x60004F7
        private System.Void ResetRowViewCache() { }
        // RVA: 0x0AD7F6F4  token: 0x60004F8
        private System.Void SetDataViewManager(System.Data.DataViewManager dataViewManager) { }
        // RVA: 0x0AD7FB2C  token: 0x60004F9
        private virtual System.Void SetIndex(System.String newSort, System.Data.DataViewRowState newRowStates, System.Data.IFilter newRowFilter) { }
        // RVA: 0x0AD7F900  token: 0x60004FA
        private System.Void SetIndex2(System.String newSort, System.Data.DataViewRowState newRowStates, System.Data.IFilter newRowFilter, System.Boolean fireEvent) { }
        // RVA: 0x0AD8008C  token: 0x60004FB
        protected System.Void UpdateIndex() { }
        // RVA: 0x0AD8009C  token: 0x60004FC
        protected virtual System.Void UpdateIndex(System.Boolean force) { }
        // RVA: 0x0AD7FD68  token: 0x60004FD
        private System.Void UpdateIndex(System.Boolean force, System.Boolean fireEvent) { }
        // RVA: 0x0AD7DFE0  token: 0x60004FE
        private System.Void ChildRelationCollectionChanged(System.Object sender, System.ComponentModel.CollectionChangeEventArgs e) { }
        // RVA: 0x0AD7F328  token: 0x60004FF
        private System.Void ParentRelationCollectionChanged(System.Object sender, System.ComponentModel.CollectionChangeEventArgs e) { }
        // RVA: 0x0AD7E194  token: 0x6000500
        protected virtual System.Void ColumnCollectionChanged(System.Object sender, System.ComponentModel.CollectionChangeEventArgs e) { }
        // RVA: 0x0AD7E18C  token: 0x6000501
        private System.Void ColumnCollectionChangedInternal(System.Object sender, System.ComponentModel.CollectionChangeEventArgs e) { }
        // RVA: 0x0AD800A8  token: 0x6000503
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000051  // size: 0x30
    public sealed class DataViewListener
    {
        // Fields
        private readonly System.WeakReference _dvWeak;  // 0x10
        private System.Data.DataTable _table;  // 0x18
        private System.Data.Index _index;  // 0x20
        private readonly System.Int32 _objectID;  // 0x28

        // Methods
        // RVA: 0x0AD7D9A0  token: 0x6000508
        private System.Void .ctor(System.Data.DataView dv) { }
        // RVA: 0x0AD7CE60  token: 0x6000509
        private System.Void ChildRelationCollectionChanged(System.Object sender, System.ComponentModel.CollectionChangeEventArgs e) { }
        // RVA: 0x0AD7D0BC  token: 0x600050A
        private System.Void ParentRelationCollectionChanged(System.Object sender, System.ComponentModel.CollectionChangeEventArgs e) { }
        // RVA: 0x0AD7CF10  token: 0x600050B
        private System.Void ColumnCollectionChanged(System.Object sender, System.ComponentModel.CollectionChangeEventArgs e) { }
        // RVA: 0x0AD7D01C  token: 0x600050C
        private System.Void MaintainDataView(System.ComponentModel.ListChangedType changedType, System.Data.DataRow row, System.Boolean trackAddRemove) { }
        // RVA: 0x0AD7CFA0  token: 0x600050D
        private System.Void IndexListChanged(System.ComponentModel.ListChangedEventArgs e) { }
        // RVA: 0x0AD7D3B0  token: 0x600050E
        private System.Void RegisterMetaDataEvents(System.Data.DataTable table) { }
        // RVA: 0x0AD7D994  token: 0x600050F
        private System.Void UnregisterMetaDataEvents() { }
        // RVA: 0x0AD7D684  token: 0x6000510
        private System.Void UnregisterMetaDataEvents(System.Boolean updateListeners) { }
        // RVA: 0x0AD7D14C  token: 0x6000511
        private System.Void RegisterListChangedEvent(System.Data.Index index) { }
        // RVA: 0x0AD7D5A0  token: 0x6000512
        private System.Void UnregisterListChangedEvent() { }
        // RVA: 0x0AD7CEF0  token: 0x6000513
        private System.Void CleanUp(System.Boolean updateListeners) { }
        // RVA: 0x0AD7D220  token: 0x6000514
        private System.Void RegisterListener(System.Data.DataTable table) { }

    }

    // TypeToken: 0x2000052  // size: 0x30
    public class DataViewManager : System.ComponentModel.MarshalByValueComponent
    {
        // Fields
        private System.Data.DataViewSettingCollection _dataViewSettingsCollection;  // 0x20
        private System.Int32 _nViews;  // 0x28
        private static System.NotSupportedException s_notSupported;  // static @ 0x0

        // Properties
        System.Data.DataViewSettingCollection DataViewSettings { get; /* RVA: 0x04D862C0 */ }

        // Methods
        // RVA: 0x0AD7DA28  token: 0x6000516
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000053  // size: 0x14
    public sealed struct DataViewRowState
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Data.DataViewRowState None;  // const
        public static System.Data.DataViewRowState Unchanged;  // const
        public static System.Data.DataViewRowState Added;  // const
        public static System.Data.DataViewRowState Deleted;  // const
        public static System.Data.DataViewRowState ModifiedCurrent;  // const
        public static System.Data.DataViewRowState ModifiedOriginal;  // const
        public static System.Data.DataViewRowState OriginalRows;  // const
        public static System.Data.DataViewRowState CurrentRows;  // const

    }

    // TypeToken: 0x2000054  // size: 0x38
    public class DataViewSetting
    {
        // Fields
        private System.Data.DataViewManager _dataViewManager;  // 0x10
        private System.Data.DataTable _table;  // 0x18
        private System.String _sort;  // 0x20
        private System.String _rowFilter;  // 0x28
        private System.Data.DataViewRowState _rowStateFilter;  // 0x30
        private System.Boolean _applyDefaultSort;  // 0x34

        // Properties
        System.Boolean ApplyDefaultSort { get; /* RVA: 0x04D86A90 */ }
        System.String RowFilter { get; /* RVA: 0x04D86240 */ }
        System.Data.DataViewRowState RowStateFilter { get; /* RVA: 0x04D864F0 */ }
        System.String Sort { get; /* RVA: 0x04D862C0 */ }

        // Methods
        // RVA: 0x0AD7DC58  token: 0x6000517
        private System.Void .ctor() { }
        // RVA: 0x0AD6AC1C  token: 0x6000519
        private System.Void SetDataViewManager(System.Data.DataViewManager dataViewManager) { }
        // RVA: 0x0AD7DC44  token: 0x600051A
        private System.Void SetDataTable(System.Data.DataTable table) { }

    }

    // TypeToken: 0x2000055  // size: 0x20
    public class DataViewSettingCollection
    {
        // Fields
        private readonly System.Data.DataViewManager _dataViewManager;  // 0x10
        private readonly System.Collections.Hashtable _list;  // 0x18

        // Properties
        System.Data.DataViewSetting Item { get; /* RVA: 0x0AD7DAC8 */ set; /* RVA: 0x0AD7DBA4 */ }

        // Methods
        // RVA: 0x0AD7DAA4  token: 0x6000520
        private System.Void Remove(System.Data.DataTable table) { }

    }

    // TypeToken: 0x2000056  // size: 0x10
    public sealed class DefaultValueTypeConverter : System.ComponentModel.StringConverter
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000521
        public System.Void .ctor() { }
        // RVA: 0x0AD80620  token: 0x6000522
        public virtual System.Object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, System.Object value, System.Type destinationType) { }
        // RVA: 0x0AD804D0  token: 0x6000523
        public virtual System.Object ConvertFrom(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, System.Object value) { }

    }

    // TypeToken: 0x2000057  // size: 0x14
    public sealed struct Aggregate
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Data.Aggregate None;  // const
        public static System.Data.Aggregate Sum;  // const
        public static System.Data.Aggregate Avg;  // const
        public static System.Data.Aggregate Min;  // const
        public static System.Data.Aggregate Max;  // const
        public static System.Data.Aggregate Count;  // const
        public static System.Data.Aggregate StDev;  // const
        public static System.Data.Aggregate Var;  // const

    }

    // TypeToken: 0x2000058  // size: 0x50
    public sealed class AggregateNode : System.Data.ExpressionNode
    {
        // Fields
        private readonly System.Data.AggregateType _type;  // 0x18
        private readonly System.Data.Aggregate _aggregate;  // 0x1c
        private readonly System.Boolean _local;  // 0x20
        private readonly System.String _relationName;  // 0x28
        private readonly System.String _columnName;  // 0x30
        private System.Data.DataTable _childTable;  // 0x38
        private System.Data.DataColumn _column;  // 0x40
        private System.Data.DataRelation _relation;  // 0x48

        // Methods
        // RVA: 0x0AD75C14  token: 0x6000524
        private System.Void .ctor(System.Data.DataTable table, System.Data.FunctionId aggregateType, System.String columnName) { }
        // RVA: 0x0AD75AE0  token: 0x6000525
        private System.Void .ctor(System.Data.DataTable table, System.Data.FunctionId aggregateType, System.String columnName, System.Boolean local, System.String relationName) { }
        // RVA: 0x0AD75384  token: 0x6000526
        private virtual System.Void Bind(System.Data.DataTable table, System.Collections.Generic.List<System.Data.DataColumn> list) { }
        // RVA: 0x0AD75604  token: 0x6000527
        private static System.Void Bind(System.Data.DataRelation relation, System.Collections.Generic.List<System.Data.DataColumn> list) { }
        // RVA: 0x0AD5EC2C  token: 0x6000528
        private virtual System.Object Eval() { }
        // RVA: 0x0AD75800  token: 0x6000529
        private virtual System.Object Eval(System.Data.DataRow row, System.Data.DataRowVersion version) { }
        // RVA: 0x0AD75764  token: 0x600052A
        private virtual System.Object Eval(System.Int32[] records) { }
        // RVA: 0x012081B0  token: 0x600052B
        private virtual System.Boolean IsConstant() { }
        // RVA: 0x04D866B0  token: 0x600052C
        private virtual System.Boolean IsTableConstant() { }
        // RVA: 0x04D866B0  token: 0x600052D
        private virtual System.Boolean HasLocalAggregate() { }
        // RVA: 0x0AD75AD8  token: 0x600052E
        private virtual System.Boolean HasRemoteAggregate() { }
        // RVA: 0x0AD75728  token: 0x600052F
        private virtual System.Boolean DependsOn(System.Data.DataColumn column) { }
        // RVA: 0x0464E630  token: 0x6000530
        private virtual System.Data.ExpressionNode Optimize() { }

    }

    // TypeToken: 0x2000059  // size: 0x30
    public class BinaryNode : System.Data.ExpressionNode
    {
        // Fields
        private System.Int32 _op;  // 0x18
        private System.Data.ExpressionNode _left;  // 0x20
        private System.Data.ExpressionNode _right;  // 0x28

        // Methods
        // RVA: 0x0AD7B814  token: 0x6000531
        private System.Void .ctor(System.Data.DataTable table, System.Int32 op, System.Data.ExpressionNode left, System.Data.ExpressionNode right) { }
        // RVA: 0x0AD76998  token: 0x6000532
        private virtual System.Void Bind(System.Data.DataTable table, System.Collections.Generic.List<System.Data.DataColumn> list) { }
        // RVA: 0x0AD5EC2C  token: 0x6000533
        private virtual System.Object Eval() { }
        // RVA: 0x0AD7A990  token: 0x6000534
        private virtual System.Object Eval(System.Data.DataRow row, System.Data.DataRowVersion version) { }
        // RVA: 0x0AD7A920  token: 0x6000535
        private virtual System.Object Eval(System.Int32[] recordNos) { }
        // RVA: 0x0AD7AD7C  token: 0x6000536
        private virtual System.Boolean IsConstant() { }
        // RVA: 0x0AD7AE60  token: 0x6000537
        private virtual System.Boolean IsTableConstant() { }
        // RVA: 0x0AD7ACEC  token: 0x6000538
        private virtual System.Boolean HasLocalAggregate() { }
        // RVA: 0x0AD7AD34  token: 0x6000539
        private virtual System.Boolean HasRemoteAggregate() { }
        // RVA: 0x0AD769FC  token: 0x600053A
        private virtual System.Boolean DependsOn(System.Data.DataColumn column) { }
        // RVA: 0x0AD7AEA4  token: 0x600053B
        private virtual System.Data.ExpressionNode Optimize() { }
        // RVA: 0x0AD7B6E4  token: 0x600053C
        private System.Void SetTypeMismatchError(System.Int32 op, System.Type left, System.Type right) { }
        // RVA: 0x0AD7A954  token: 0x600053D
        private static System.Object Eval(System.Data.ExpressionNode expr, System.Data.DataRow row, System.Data.DataRowVersion version, System.Int32[] recordNos) { }
        // RVA: 0x0AD75C34  token: 0x600053E
        private System.Int32 BinaryCompare(System.Object vLeft, System.Object vRight, System.Data.Common.StorageType resultType, System.Int32 op) { }
        // RVA: 0x0AD75C58  token: 0x600053F
        private System.Int32 BinaryCompare(System.Object vLeft, System.Object vRight, System.Data.Common.StorageType resultType, System.Int32 op, System.Globalization.CompareInfo comparer) { }
        // RVA: 0x0AD76A54  token: 0x6000540
        private System.Object EvalBinaryOp(System.Int32 op, System.Data.ExpressionNode left, System.Data.ExpressionNode right, System.Data.DataRow row, System.Data.DataRowVersion version, System.Int32[] recordNos) { }
        // RVA: 0x0AD7AB4C  token: 0x6000541
        private System.Data.BinaryNode.DataTypePrecedence GetPrecedence(System.Data.Common.StorageType storageType) { }
        // RVA: 0x0AD7A9C0  token: 0x6000542
        private static System.Data.Common.StorageType GetPrecedenceType(System.Data.BinaryNode.DataTypePrecedence code) { }
        // RVA: 0x0AD7AE10  token: 0x6000543
        private System.Boolean IsMixed(System.Data.Common.StorageType left, System.Data.Common.StorageType right) { }
        // RVA: 0x0AD7ADC0  token: 0x6000544
        private System.Boolean IsMixedSql(System.Data.Common.StorageType left, System.Data.Common.StorageType right) { }
        // RVA: 0x0AD7B424  token: 0x6000545
        private System.Data.Common.StorageType ResultType(System.Data.Common.StorageType left, System.Data.Common.StorageType right, System.Boolean lc, System.Boolean rc, System.Int32 op) { }
        // RVA: 0x0AD7B150  token: 0x6000546
        private System.Data.Common.StorageType ResultSqlType(System.Data.Common.StorageType left, System.Data.Common.StorageType right, System.Boolean lc, System.Boolean rc, System.Int32 op) { }
        // RVA: 0x0AD7B71C  token: 0x6000547
        private System.Int32 SqlResultType(System.Int32 typeCode) { }

    }

    // TypeToken: 0x200005B  // size: 0x40
    public sealed class LikeNode : System.Data.BinaryNode
    {
        // Fields
        private System.Int32 _kind;  // 0x30
        private System.String _pattern;  // 0x38

        // Methods
        // RVA: 0x0AD84990  token: 0x6000548
        private System.Void .ctor(System.Data.DataTable table, System.Int32 op, System.Data.ExpressionNode left, System.Data.ExpressionNode right) { }
        // RVA: 0x0AD844F0  token: 0x6000549
        private virtual System.Object Eval(System.Data.DataRow row, System.Data.DataRowVersion version) { }
        // RVA: 0x0AD841E0  token: 0x600054A
        private System.String AnalyzePattern(System.String pat) { }

    }

    // TypeToken: 0x200005C  // size: 0x20
    public sealed class ConstNode : System.Data.ExpressionNode
    {
        // Fields
        private readonly System.Object _val;  // 0x18

        // Methods
        // RVA: 0x0AD7C334  token: 0x600054B
        private System.Void .ctor(System.Data.DataTable table, System.Data.ValueType type, System.Object constant) { }
        // RVA: 0x0AD7C110  token: 0x600054C
        private System.Void .ctor(System.Data.DataTable table, System.Data.ValueType type, System.Object constant, System.Boolean fParseQuotes) { }
        // RVA: 0x053908C0  token: 0x600054D
        private virtual System.Void Bind(System.Data.DataTable table, System.Collections.Generic.List<System.Data.DataColumn> list) { }
        // RVA: 0x0385B100  token: 0x600054E
        private virtual System.Object Eval() { }
        // RVA: 0x0AD7B854  token: 0x600054F
        private virtual System.Object Eval(System.Data.DataRow row, System.Data.DataRowVersion version) { }
        // RVA: 0x0AD7B854  token: 0x6000550
        private virtual System.Object Eval(System.Int32[] recordNos) { }
        // RVA: 0x02FFF600  token: 0x6000551
        private virtual System.Boolean IsConstant() { }
        // RVA: 0x02FFF600  token: 0x6000552
        private virtual System.Boolean IsTableConstant() { }
        // RVA: 0x012081B0  token: 0x6000553
        private virtual System.Boolean HasLocalAggregate() { }
        // RVA: 0x012081B0  token: 0x6000554
        private virtual System.Boolean HasRemoteAggregate() { }
        // RVA: 0x0464E630  token: 0x6000555
        private virtual System.Data.ExpressionNode Optimize() { }
        // RVA: 0x0AD7B864  token: 0x6000556
        private System.Object SmallestDecimal(System.Object constant) { }
        // RVA: 0x0AD7BC18  token: 0x6000557
        private System.Object SmallestNumeric(System.Object constant) { }

    }

    // TypeToken: 0x200005D  // size: 0x48
    public sealed class DataExpression : System.Data.IFilter
    {
        // Fields
        private System.String _originalExpression;  // 0x10
        private System.Boolean _parsed;  // 0x18
        private System.Boolean _bound;  // 0x19
        private System.Data.ExpressionNode _expr;  // 0x20
        private System.Data.DataTable _table;  // 0x28
        private readonly System.Data.Common.StorageType _storageType;  // 0x30
        private readonly System.Type _dataType;  // 0x38
        private System.Data.DataColumn[] _dependency;  // 0x40

        // Properties
        System.String Expression { get; /* RVA: 0x0AD7CCC4 */ }
        System.Data.ExpressionNode ExpressionNode { get; /* RVA: 0x04D862C0 */ }
        System.Boolean HasValue { get; /* RVA: 0x09B00070 */ }

        // Methods
        // RVA: 0x0AD7CB6C  token: 0x6000558
        private System.Void .ctor(System.Data.DataTable table, System.String expression) { }
        // RVA: 0x0AD7CB88  token: 0x6000559
        private System.Void .ctor(System.Data.DataTable table, System.String expression, System.Type type) { }
        // RVA: 0x0AD7C350  token: 0x600055D
        private System.Void Bind(System.Data.DataTable table) { }
        // RVA: 0x0AD7C418  token: 0x600055E
        private System.Boolean DependsOn(System.Data.DataColumn column) { }
        // RVA: 0x0AD7C62C  token: 0x600055F
        private System.Object Evaluate() { }
        // RVA: 0x0AD7C438  token: 0x6000560
        private System.Object Evaluate(System.Data.DataRow row, System.Data.DataRowVersion version) { }
        // RVA: 0x0AD7C61C  token: 0x6000561
        private System.Object Evaluate(System.Data.DataRow[] rows) { }
        // RVA: 0x0AD7C63C  token: 0x6000562
        private System.Object Evaluate(System.Data.DataRow[] rows, System.Data.DataRowVersion version) { }
        // RVA: 0x0AD7C7A4  token: 0x6000563
        public virtual System.Boolean Invoke(System.Data.DataRow row, System.Data.DataRowVersion version) { }
        // RVA: 0x04D85A60  token: 0x6000564
        private System.Data.DataColumn[] GetDependency() { }
        // RVA: 0x0AD7C894  token: 0x6000565
        private System.Boolean IsTableAggregate() { }
        // RVA: 0x0AD7C8B4  token: 0x6000566
        private static System.Boolean IsUnknown(System.Object value) { }
        // RVA: 0x0AD7C764  token: 0x6000567
        private System.Boolean HasLocalAggregate() { }
        // RVA: 0x0AD7C784  token: 0x6000568
        private System.Boolean HasRemoteAggregate() { }
        // RVA: 0x0AD7C900  token: 0x6000569
        private static System.Boolean ToBoolean(System.Object value) { }

    }

    // TypeToken: 0x200005E  // size: 0x18
    public abstract class ExpressionNode
    {
        // Fields
        private System.Data.DataTable _table;  // 0x10

        // Properties
        System.IFormatProvider FormatProvider { get; /* RVA: 0x0AD81ABC */ }
        System.Boolean IsSqlColumn { get; /* RVA: 0x012081B0 */ }
        System.Data.DataTable table { get; /* RVA: 0x02B2ECC0 */ }

        // Methods
        // RVA: 0x053908C0  token: 0x600056A
        protected System.Void .ctor(System.Data.DataTable table) { }
        // RVA: 0x053908C0  token: 0x600056E
        protected System.Void BindTable(System.Data.DataTable table) { }
        // RVA: -1  // abstract  token: 0x600056F
        private virtual System.Void Bind(System.Data.DataTable table, System.Collections.Generic.List<System.Data.DataColumn> list) { }
        // RVA: -1  // abstract  token: 0x6000570
        private virtual System.Object Eval() { }
        // RVA: -1  // abstract  token: 0x6000571
        private virtual System.Object Eval(System.Data.DataRow row, System.Data.DataRowVersion version) { }
        // RVA: -1  // abstract  token: 0x6000572
        private virtual System.Object Eval(System.Int32[] recordNos) { }
        // RVA: -1  // abstract  token: 0x6000573
        private virtual System.Boolean IsConstant() { }
        // RVA: -1  // abstract  token: 0x6000574
        private virtual System.Boolean IsTableConstant() { }
        // RVA: -1  // abstract  token: 0x6000575
        private virtual System.Boolean HasLocalAggregate() { }
        // RVA: -1  // abstract  token: 0x6000576
        private virtual System.Boolean HasRemoteAggregate() { }
        // RVA: -1  // abstract  token: 0x6000577
        private virtual System.Data.ExpressionNode Optimize() { }
        // RVA: 0x012081B0  token: 0x6000578
        private virtual System.Boolean DependsOn(System.Data.DataColumn column) { }
        // RVA: 0x0AD819D8  token: 0x6000579
        private static System.Boolean IsInteger(System.Data.Common.StorageType type) { }
        // RVA: 0x0AD819BC  token: 0x600057A
        private static System.Boolean IsIntegerSql(System.Data.Common.StorageType type) { }
        // RVA: 0x0AD81A6C  token: 0x600057B
        private static System.Boolean IsSigned(System.Data.Common.StorageType type) { }
        // RVA: 0x0AD81A4C  token: 0x600057C
        private static System.Boolean IsSignedSql(System.Data.Common.StorageType type) { }
        // RVA: 0x0AD81AA0  token: 0x600057D
        private static System.Boolean IsUnsigned(System.Data.Common.StorageType type) { }
        // RVA: 0x0AD81A84  token: 0x600057E
        private static System.Boolean IsUnsignedSql(System.Data.Common.StorageType type) { }
        // RVA: 0x0AD81A20  token: 0x600057F
        private static System.Boolean IsNumeric(System.Data.Common.StorageType type) { }
        // RVA: 0x0AD819F4  token: 0x6000580
        private static System.Boolean IsNumericSql(System.Data.Common.StorageType type) { }
        // RVA: 0x0AD819A8  token: 0x6000581
        private static System.Boolean IsFloat(System.Data.Common.StorageType type) { }
        // RVA: 0x0AD81988  token: 0x6000582
        private static System.Boolean IsFloatSql(System.Data.Common.StorageType type) { }

    }

    // TypeToken: 0x200005F  // size: 0x14
    public sealed struct ValueType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Data.ValueType Unknown;  // const
        public static System.Data.ValueType Null;  // const
        public static System.Data.ValueType Bool;  // const
        public static System.Data.ValueType Numeric;  // const
        public static System.Data.ValueType Str;  // const
        public static System.Data.ValueType Float;  // const
        public static System.Data.ValueType Decimal;  // const
        public static System.Data.ValueType Object;  // const
        public static System.Data.ValueType Date;  // const

    }

    // TypeToken: 0x2000060  // size: 0x14
    public sealed struct Nodes
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Data.Nodes Noop;  // const
        public static System.Data.Nodes Unop;  // const
        public static System.Data.Nodes UnopSpec;  // const
        public static System.Data.Nodes Binop;  // const
        public static System.Data.Nodes BinopSpec;  // const
        public static System.Data.Nodes Zop;  // const
        public static System.Data.Nodes Call;  // const
        public static System.Data.Nodes Const;  // const
        public static System.Data.Nodes Name;  // const
        public static System.Data.Nodes Paren;  // const
        public static System.Data.Nodes Conv;  // const

    }

    // TypeToken: 0x2000061  // size: 0x68
    public sealed class ExpressionParser
    {
        // Fields
        private static readonly System.Data.ExpressionParser.ReservedWords[] s_reservedwords;  // static @ 0x0
        private System.Char _escape;  // 0x10
        private System.Char _decimalSeparator;  // 0x12
        private System.Char _listSeparator;  // 0x14
        private System.Char _exponentL;  // 0x16
        private System.Char _exponentU;  // 0x18
        private System.Char[] _text;  // 0x20
        private System.Int32 _pos;  // 0x28
        private System.Int32 _start;  // 0x2c
        private System.Data.Tokens _token;  // 0x30
        private System.Int32 _op;  // 0x34
        private System.Data.OperatorInfo[] _ops;  // 0x38
        private System.Int32 _topOperator;  // 0x40
        private System.Int32 _topNode;  // 0x44
        private readonly System.Data.DataTable _table;  // 0x48
        private System.Data.ExpressionNode[] _nodeStack;  // 0x50
        private System.Int32 _prevOperand;  // 0x58
        private System.Data.ExpressionNode _expression;  // 0x60

        // Methods
        // RVA: 0x0AD84168  token: 0x6000583
        private System.Void .ctor(System.Data.DataTable table) { }
        // RVA: 0x0AD81FAC  token: 0x6000584
        private System.Void LoadExpression(System.String data) { }
        // RVA: 0x0AD83CE8  token: 0x6000585
        private System.Void StartScan() { }
        // RVA: 0x0AD823F4  token: 0x6000586
        private System.Data.ExpressionNode Parse() { }
        // RVA: 0x0AD82164  token: 0x6000587
        private System.Data.ExpressionNode ParseAggregateArgument(System.Data.FunctionId aggregate) { }
        // RVA: 0x0AD820B4  token: 0x6000588
        private System.Data.ExpressionNode NodePop() { }
        // RVA: 0x0AD8207C  token: 0x6000589
        private System.Data.ExpressionNode NodePeek() { }
        // RVA: 0x0AD820EC  token: 0x600058A
        private System.Void NodePush(System.Data.ExpressionNode node) { }
        // RVA: 0x0AD81AFC  token: 0x600058B
        private System.Void BuildExpression(System.Int32 pri) { }
        // RVA: 0x0AD81D38  token: 0x600058C
        private System.Void CheckToken(System.Data.Tokens token) { }
        // RVA: 0x0AD8389C  token: 0x600058D
        private System.Data.Tokens Scan() { }
        // RVA: 0x0AD83418  token: 0x600058E
        private System.Void ScanNumeric() { }
        // RVA: 0x0AD832AC  token: 0x600058F
        private System.Void ScanName() { }
        // RVA: 0x0AD83304  token: 0x6000590
        private System.Void ScanName(System.Char chEnd, System.Char esc, System.String charsToEscape) { }
        // RVA: 0x0AD831EC  token: 0x6000591
        private System.Void ScanDate() { }
        // RVA: 0x041E1670  token: 0x6000592
        private System.Void ScanBinaryConstant() { }
        // RVA: 0x0AD8357C  token: 0x6000593
        private System.Void ScanReserved() { }
        // RVA: 0x0AD83760  token: 0x6000594
        private System.Void ScanString(System.Char escape) { }
        // RVA: 0x0AD83814  token: 0x6000595
        private System.Void ScanToken(System.Data.Tokens token) { }
        // RVA: 0x0AD83844  token: 0x6000596
        private System.Void ScanWhite() { }
        // RVA: 0x0AD81F9C  token: 0x6000597
        private System.Boolean IsWhiteSpace(System.Char ch) { }
        // RVA: 0x0AD81D78  token: 0x6000598
        private System.Boolean IsAlphaNumeric(System.Char ch) { }
        // RVA: 0x0AD81F6C  token: 0x6000599
        private System.Boolean IsDigit(System.Char ch) { }
        // RVA: 0x0AD81ECC  token: 0x600059A
        private System.Boolean IsAlpha(System.Char ch) { }
        // RVA: 0x0AD83D40  token: 0x600059B
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000063  // size: 0x14
    public sealed struct Tokens
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Data.Tokens None;  // const
        public static System.Data.Tokens Name;  // const
        public static System.Data.Tokens Numeric;  // const
        public static System.Data.Tokens Decimal;  // const
        public static System.Data.Tokens Float;  // const
        public static System.Data.Tokens BinaryConst;  // const
        public static System.Data.Tokens StringConst;  // const
        public static System.Data.Tokens Date;  // const
        public static System.Data.Tokens ListSeparator;  // const
        public static System.Data.Tokens LeftParen;  // const
        public static System.Data.Tokens RightParen;  // const
        public static System.Data.Tokens ZeroOp;  // const
        public static System.Data.Tokens UnaryOp;  // const
        public static System.Data.Tokens BinaryOp;  // const
        public static System.Data.Tokens Child;  // const
        public static System.Data.Tokens Parent;  // const
        public static System.Data.Tokens Dot;  // const
        public static System.Data.Tokens Unknown;  // const
        public static System.Data.Tokens EOS;  // const

    }

    // TypeToken: 0x2000064  // size: 0x20
    public sealed class OperatorInfo
    {
        // Fields
        private System.Data.Nodes _type;  // 0x10
        private System.Int32 _op;  // 0x14
        private System.Int32 _priority;  // 0x18

        // Methods
        // RVA: 0x04D88580  token: 0x600059D
        private System.Void .ctor(System.Data.Nodes type, System.Int32 op, System.Int32 pri) { }

    }

    // TypeToken: 0x2000065  // size: 0x90
    public class InvalidExpressionException : System.Data.DataException
    {
        // Methods
        // RVA: 0x0A76172C  token: 0x600059E
        protected System.Void .ctor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x0AD8078C  token: 0x600059F
        public System.Void .ctor() { }
        // RVA: 0x0AD4DB10  token: 0x60005A0
        public System.Void .ctor(System.String s) { }

    }

    // TypeToken: 0x2000066  // size: 0x90
    public class EvaluateException : System.Data.InvalidExpressionException
    {
        // Methods
        // RVA: 0x0AD80794  token: 0x60005A1
        protected System.Void .ctor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x0AD8078C  token: 0x60005A2
        public System.Void .ctor() { }
        // RVA: 0x0AD4DB10  token: 0x60005A3
        public System.Void .ctor(System.String s) { }

    }

    // TypeToken: 0x2000067  // size: 0x90
    public class SyntaxErrorException : System.Data.InvalidExpressionException
    {
        // Methods
        // RVA: 0x0AD80794  token: 0x60005A4
        protected System.Void .ctor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x0AD8078C  token: 0x60005A5
        public System.Void .ctor() { }
        // RVA: 0x0AD4DB10  token: 0x60005A6
        public System.Void .ctor(System.String s) { }

    }

    // TypeToken: 0x2000068  // size: 0x10
    public sealed class ExprException
    {
        // Methods
        // RVA: 0x0AD818B0  token: 0x60005A7
        private static System.OverflowException _Overflow(System.String error) { }
        // RVA: 0x0AD81844  token: 0x60005A8
        private static System.Data.InvalidExpressionException _Expr(System.String error) { }
        // RVA: 0x0AD8191C  token: 0x60005A9
        private static System.Data.SyntaxErrorException _Syntax(System.String error) { }
        // RVA: 0x0AD817D8  token: 0x60005AA
        private static System.Data.EvaluateException _Eval(System.String error) { }
        // RVA: 0x0AD8176C  token: 0x60005AB
        private static System.Data.EvaluateException _Eval(System.String error, System.Exception innerException) { }
        // RVA: 0x0AD81040  token: 0x60005AC
        public static System.Exception InvokeArgument() { }
        // RVA: 0x0AD81218  token: 0x60005AD
        public static System.Exception NYI(System.String moreinfo) { }
        // RVA: 0x0AD81124  token: 0x60005AE
        public static System.Exception MissingOperand(System.Data.OperatorInfo before) { }
        // RVA: 0x0AD8119C  token: 0x60005AF
        public static System.Exception MissingOperator(System.String token) { }
        // RVA: 0x0AD81414  token: 0x60005B0
        public static System.Exception TypeMismatch(System.String expr) { }
        // RVA: 0x0AD80D00  token: 0x60005B1
        public static System.Exception FunctionArgumentOutOfRange(System.String arg, System.String func) { }
        // RVA: 0x0AD80BF4  token: 0x60005B2
        public static System.Exception ExpressionTooComplex() { }
        // RVA: 0x0AD8145C  token: 0x60005B3
        public static System.Exception UnboundName(System.String name) { }
        // RVA: 0x0AD80F7C  token: 0x60005B4
        public static System.Exception InvalidString(System.String str) { }
        // RVA: 0x0AD814A4  token: 0x60005B5
        public static System.Exception UndefinedFunction(System.String name) { }
        // RVA: 0x0AD812F4  token: 0x60005B6
        public static System.Exception SyntaxError() { }
        // RVA: 0x0AD80CB8  token: 0x60005B7
        public static System.Exception FunctionArgumentCount(System.String name) { }
        // RVA: 0x0AD811E4  token: 0x60005B8
        public static System.Exception MissingRightParen() { }
        // RVA: 0x0AD815D0  token: 0x60005B9
        public static System.Exception UnknownToken(System.String token, System.Int32 position) { }
        // RVA: 0x0AD814EC  token: 0x60005BA
        public static System.Exception UnknownToken(System.Data.Tokens tokExpected, System.Data.Tokens tokCurr, System.Int32 position) { }
        // RVA: 0x0AD80AA8  token: 0x60005BB
        public static System.Exception DatatypeConvertion(System.Type type1, System.Type type2) { }
        // RVA: 0x0AD80B2C  token: 0x60005BC
        public static System.Exception DatavalueConvertion(System.Object value, System.Type type, System.Exception innerException) { }
        // RVA: 0x0AD80EEC  token: 0x60005BD
        public static System.Exception InvalidName(System.String name) { }
        // RVA: 0x0AD80DC0  token: 0x60005BE
        public static System.Exception InvalidDate(System.String date) { }
        // RVA: 0x0AD81260  token: 0x60005BF
        public static System.Exception NonConstantArgument() { }
        // RVA: 0x0AD80F34  token: 0x60005C0
        public static System.Exception InvalidPattern(System.String pat) { }
        // RVA: 0x0AD80D8C  token: 0x60005C1
        public static System.Exception InWithoutParentheses() { }
        // RVA: 0x0AD80D58  token: 0x60005C2
        public static System.Exception InWithoutList() { }
        // RVA: 0x0AD80E3C  token: 0x60005C3
        public static System.Exception InvalidIsSyntax() { }
        // RVA: 0x0AD81294  token: 0x60005C4
        public static System.Exception Overflow(System.Type type) { }
        // RVA: 0x0AD80964  token: 0x60005C5
        public static System.Exception ArgumentType(System.String function, System.Int32 arg, System.Type type) { }
        // RVA: 0x0AD808E8  token: 0x60005C6
        public static System.Exception ArgumentTypeInteger(System.String function, System.Int32 arg) { }
        // RVA: 0x0AD8135C  token: 0x60005C7
        public static System.Exception TypeMismatchInBinop(System.Int32 op, System.Type type1, System.Type type2) { }
        // RVA: 0x0AD80830  token: 0x60005C8
        public static System.Exception AmbiguousBinop(System.Int32 op, System.Type type1, System.Type type2) { }
        // RVA: 0x0AD81704  token: 0x60005C9
        public static System.Exception UnsupportedOperator(System.Int32 op) { }
        // RVA: 0x0AD80EA4  token: 0x60005CA
        public static System.Exception InvalidNameBracketing(System.String name) { }
        // RVA: 0x0AD810DC  token: 0x60005CB
        public static System.Exception MissingOperandBefore(System.String op) { }
        // RVA: 0x0AD81328  token: 0x60005CC
        public static System.Exception TooManyRightParentheses() { }
        // RVA: 0x0AD8164C  token: 0x60005CD
        public static System.Exception UnresolvedRelation(System.String name, System.String expr) { }
        // RVA: 0x0AD80A18  token: 0x60005CE
        private static System.Data.EvaluateException BindFailure(System.String relationName) { }
        // RVA: 0x0AD807B4  token: 0x60005CF
        public static System.Exception AggregateArgument() { }
        // RVA: 0x0AD807E8  token: 0x60005D0
        public static System.Exception AggregateUnbound(System.String expr) { }
        // RVA: 0x0AD80BC0  token: 0x60005D1
        public static System.Exception EvalNoContext() { }
        // RVA: 0x0AD80C28  token: 0x60005D2
        public static System.Exception ExpressionUnbound(System.String expr) { }
        // RVA: 0x0AD80A60  token: 0x60005D3
        public static System.Exception ComputeNotAggregate(System.String expr) { }
        // RVA: 0x0AD80C70  token: 0x60005D4
        public static System.Exception FilterConvertion(System.String expr) { }
        // RVA: 0x0AD81074  token: 0x60005D5
        public static System.Exception LookupArgument() { }
        // RVA: 0x0AD80FF8  token: 0x60005D6
        public static System.Exception InvalidType(System.String typeName) { }
        // RVA: 0x0AD80E08  token: 0x60005D7
        public static System.Exception InvalidHoursArgument() { }
        // RVA: 0x0AD80E70  token: 0x60005D8
        public static System.Exception InvalidMinutesArgument() { }
        // RVA: 0x0AD80FC4  token: 0x60005D9
        public static System.Exception InvalidTimeZoneRange() { }
        // RVA: 0x0AD810A8  token: 0x60005DA
        public static System.Exception MismatchKindandTimeSpan() { }
        // RVA: 0x0AD816A4  token: 0x60005DB
        public static System.Exception UnsupportedDataType(System.Type type) { }

    }

    // TypeToken: 0x2000069
    public interface IFilter
    {
        // Methods
        // RVA: -1  // abstract  token: 0x60005DC
        public virtual System.Boolean Invoke(System.Data.DataRow row, System.Data.DataRowVersion version) { }

    }

    // TypeToken: 0x200006A  // size: 0x38
    public sealed class LookupNode : System.Data.ExpressionNode
    {
        // Fields
        private readonly System.String _relationName;  // 0x18
        private readonly System.String _columnName;  // 0x20
        private System.Data.DataColumn _column;  // 0x28
        private System.Data.DataRelation _relation;  // 0x30

        // Methods
        // RVA: 0x0AD84D60  token: 0x60005DD
        private System.Void .ctor(System.Data.DataTable table, System.String columnName, System.String relationName) { }
        // RVA: 0x0AD849A0  token: 0x60005DE
        private virtual System.Void Bind(System.Data.DataTable table, System.Collections.Generic.List<System.Data.DataColumn> list) { }
        // RVA: 0x0AD84C04  token: 0x60005DF
        private virtual System.Object Eval() { }
        // RVA: 0x0AD84C64  token: 0x60005E0
        private virtual System.Object Eval(System.Data.DataRow row, System.Data.DataRowVersion version) { }
        // RVA: 0x0AD84C2C  token: 0x60005E1
        private virtual System.Object Eval(System.Int32[] recordNos) { }
        // RVA: 0x012081B0  token: 0x60005E2
        private virtual System.Boolean IsConstant() { }
        // RVA: 0x012081B0  token: 0x60005E3
        private virtual System.Boolean IsTableConstant() { }
        // RVA: 0x012081B0  token: 0x60005E4
        private virtual System.Boolean HasLocalAggregate() { }
        // RVA: 0x012081B0  token: 0x60005E5
        private virtual System.Boolean HasRemoteAggregate() { }
        // RVA: 0x0AD84BFC  token: 0x60005E6
        private virtual System.Boolean DependsOn(System.Data.DataColumn column) { }
        // RVA: 0x0464E630  token: 0x60005E7
        private virtual System.Data.ExpressionNode Optimize() { }

    }

    // TypeToken: 0x200006B  // size: 0x30
    public sealed class NameNode : System.Data.ExpressionNode
    {
        // Fields
        private System.String _name;  // 0x18
        private System.Boolean _found;  // 0x20
        private System.Data.DataColumn _column;  // 0x28

        // Properties
        System.Boolean IsSqlColumn { get; /* RVA: 0x0AD853F0 */ }

        // Methods
        // RVA: 0x0AD853AC  token: 0x60005E8
        private System.Void .ctor(System.Data.DataTable table, System.Char[] text, System.Int32 start, System.Int32 pos) { }
        // RVA: 0x032AF420  token: 0x60005E9
        private System.Void .ctor(System.Data.DataTable table, System.String name) { }
        // RVA: 0x0AD84D98  token: 0x60005EB
        private virtual System.Void Bind(System.Data.DataTable table, System.Collections.Generic.List<System.Data.DataColumn> list) { }
        // RVA: 0x0AD850E4  token: 0x60005EC
        private virtual System.Object Eval() { }
        // RVA: 0x0AD84FE4  token: 0x60005ED
        private virtual System.Object Eval(System.Data.DataRow row, System.Data.DataRowVersion version) { }
        // RVA: 0x0AD850AC  token: 0x60005EE
        private virtual System.Object Eval(System.Int32[] records) { }
        // RVA: 0x012081B0  token: 0x60005EF
        private virtual System.Boolean IsConstant() { }
        // RVA: 0x0AD8514C  token: 0x60005F0
        private virtual System.Boolean IsTableConstant() { }
        // RVA: 0x0AD8510C  token: 0x60005F1
        private virtual System.Boolean HasLocalAggregate() { }
        // RVA: 0x0AD8512C  token: 0x60005F2
        private virtual System.Boolean HasRemoteAggregate() { }
        // RVA: 0x0AD84FA8  token: 0x60005F3
        private virtual System.Boolean DependsOn(System.Data.DataColumn column) { }
        // RVA: 0x0464E630  token: 0x60005F4
        private virtual System.Data.ExpressionNode Optimize() { }
        // RVA: 0x0AD8516C  token: 0x60005F5
        private static System.String ParseName(System.Char[] text, System.Int32 start, System.Int32 pos) { }

    }

    // TypeToken: 0x200006C  // size: 0x10
    public sealed class Operators
    {
        // Fields
        private static readonly System.Int32[] s_priority;  // static @ 0x0
        private static readonly System.String[] s_looks;  // static @ 0x8

        // Methods
        // RVA: 0x0AD85410  token: 0x60005F6
        private static System.Boolean IsArithmetical(System.Int32 op) { }
        // RVA: 0x0AD85424  token: 0x60005F7
        private static System.Boolean IsLogical(System.Int32 op) { }
        // RVA: 0x0AD85440  token: 0x60005F8
        private static System.Boolean IsRelational(System.Int32 op) { }
        // RVA: 0x0AD85450  token: 0x60005F9
        private static System.Int32 Priority(System.Int32 op) { }
        // RVA: 0x0AD854C0  token: 0x60005FA
        private static System.String ToString(System.Int32 op) { }
        // RVA: 0x0AD85560  token: 0x60005FB
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200006D  // size: 0x28
    public sealed class UnaryNode : System.Data.ExpressionNode
    {
        // Fields
        private readonly System.Int32 _op;  // 0x18
        private System.Data.ExpressionNode _right;  // 0x20

        // Methods
        // RVA: 0x032B2C30  token: 0x60005FC
        private System.Void .ctor(System.Data.DataTable table, System.Int32 op, System.Data.ExpressionNode right) { }
        // RVA: 0x0AD85B1C  token: 0x60005FD
        private virtual System.Void Bind(System.Data.DataTable table, System.Collections.Generic.List<System.Data.DataColumn> list) { }
        // RVA: 0x0AD5EC2C  token: 0x60005FE
        private virtual System.Object Eval() { }
        // RVA: 0x0AD862B8  token: 0x60005FF
        private virtual System.Object Eval(System.Data.DataRow row, System.Data.DataRowVersion version) { }
        // RVA: 0x0AD86298  token: 0x6000600
        private virtual System.Object Eval(System.Int32[] recordNos) { }
        // RVA: 0x0AD85B78  token: 0x6000601
        private System.Object EvalUnaryOp(System.Int32 op, System.Object vl) { }
        // RVA: 0x0AD86348  token: 0x6000602
        private virtual System.Boolean IsConstant() { }
        // RVA: 0x0AD8636C  token: 0x6000603
        private virtual System.Boolean IsTableConstant() { }
        // RVA: 0x0AD86300  token: 0x6000604
        private virtual System.Boolean HasLocalAggregate() { }
        // RVA: 0x0AD86324  token: 0x6000605
        private virtual System.Boolean HasRemoteAggregate() { }
        // RVA: 0x0AD85B54  token: 0x6000606
        private virtual System.Boolean DependsOn(System.Data.DataColumn column) { }
        // RVA: 0x0AD86390  token: 0x6000607
        private virtual System.Data.ExpressionNode Optimize() { }

    }

    // TypeToken: 0x200006E  // size: 0x20
    public sealed class ZeroOpNode : System.Data.ExpressionNode
    {
        // Fields
        private readonly System.Int32 _op;  // 0x18

        // Methods
        // RVA: 0x0AD987E4  token: 0x6000608
        private System.Void .ctor(System.Int32 op) { }
        // RVA: 0x041E1670  token: 0x6000609
        private virtual System.Void Bind(System.Data.DataTable table, System.Collections.Generic.List<System.Data.DataColumn> list) { }
        // RVA: 0x0AD9875C  token: 0x600060A
        private virtual System.Object Eval() { }
        // RVA: 0x0AD7B854  token: 0x600060B
        private virtual System.Object Eval(System.Data.DataRow row, System.Data.DataRowVersion version) { }
        // RVA: 0x0AD7B854  token: 0x600060C
        private virtual System.Object Eval(System.Int32[] recordNos) { }
        // RVA: 0x02FFF600  token: 0x600060D
        private virtual System.Boolean IsConstant() { }
        // RVA: 0x02FFF600  token: 0x600060E
        private virtual System.Boolean IsTableConstant() { }
        // RVA: 0x012081B0  token: 0x600060F
        private virtual System.Boolean HasLocalAggregate() { }
        // RVA: 0x012081B0  token: 0x6000610
        private virtual System.Boolean HasRemoteAggregate() { }
        // RVA: 0x0464E630  token: 0x6000611
        private virtual System.Data.ExpressionNode Optimize() { }

    }

    // TypeToken: 0x200006F  // size: 0x78
    public class ForeignKeyConstraint : System.Data.Constraint
    {
        // Fields
        private System.Data.Rule _deleteRule;  // 0x38
        private System.Data.Rule _updateRule;  // 0x3c
        private System.Data.AcceptRejectRule _acceptRejectRule;  // 0x40
        private System.Data.DataKey _childKey;  // 0x48
        private System.Data.DataKey _parentKey;  // 0x50
        private System.String _constraintName;  // 0x58
        private System.String[] _parentColumnNames;  // 0x60
        private System.String[] _childColumnNames;  // 0x68
        private System.String _parentTableName;  // 0x70

        // Properties
        System.Data.DataKey ChildKey { get; /* RVA: 0x0AD88CE4 */ }
        System.Data.DataColumn[] Columns { get; /* RVA: 0x0AD88D00 */ }
        System.Data.DataTable Table { get; /* RVA: 0x0AD88DA4 */ }
        System.String[] ParentColumnNames { get; /* RVA: 0x0AD88D3C */ }
        System.String[] ChildColumnNames { get; /* RVA: 0x0AD88CD8 */ }
        System.Data.AcceptRejectRule AcceptRejectRule { get; /* RVA: 0x0AD88CBC */ set; /* RVA: 0x0AD88DE0 */ }
        System.Data.Rule DeleteRule { get; /* RVA: 0x0AD88D20 */ set; /* RVA: 0x0AD88E30 */ }
        System.Data.DataColumn[] RelatedColumns { get; /* RVA: 0x0AD88D64 */ }
        System.Data.DataColumn[] RelatedColumnsReference { get; /* RVA: 0x0AD88D48 */ }
        System.Data.DataKey ParentKey { get; /* RVA: 0x0AD88D48 */ }
        System.Data.DataTable RelatedTable { get; /* RVA: 0x0AD88D84 */ }
        System.Data.Rule UpdateRule { get; /* RVA: 0x0AD88DC4 */ set; /* RVA: 0x0AD88E80 */ }

        // Methods
        // RVA: 0x0AD88C38  token: 0x6000612
        public System.Void .ctor(System.Data.DataColumn[] parentColumns, System.Data.DataColumn[] childColumns) { }
        // RVA: 0x0AD88C58  token: 0x6000613
        public System.Void .ctor(System.String constraintName, System.Data.DataColumn[] parentColumns, System.Data.DataColumn[] childColumns) { }
        // RVA: 0x0AD88BA0  token: 0x6000614
        public System.Void .ctor(System.String constraintName, System.String parentTableName, System.String[] parentColumnNames, System.String[] childColumnNames, System.Data.AcceptRejectRule acceptRejectRule, System.Data.Rule deleteRule, System.Data.Rule updateRule) { }
        // RVA: 0x0AD87170  token: 0x600061A
        private virtual System.Void CheckCanAddToCollection(System.Data.ConstraintCollection constraints) { }
        // RVA: 0x02FFF600  token: 0x600061B
        private virtual System.Boolean CanBeRemovedFromCollection(System.Data.ConstraintCollection constraints, System.Boolean fThrowException) { }
        // RVA: 0x0AD888AC  token: 0x600061C
        private System.Boolean IsKeyNull(System.Object[] values) { }
        // RVA: 0x0AD8870C  token: 0x600061D
        private virtual System.Boolean IsConstraintViolated() { }
        // RVA: 0x0AD8649C  token: 0x600061E
        private virtual System.Boolean CanEnableConstraint() { }
        // RVA: 0x0AD865C8  token: 0x600061F
        private System.Void CascadeCommit(System.Data.DataRow row) { }
        // RVA: 0x0AD86720  token: 0x6000620
        private System.Void CascadeDelete(System.Data.DataRow row) { }
        // RVA: 0x0AD86B78  token: 0x6000621
        private System.Void CascadeRollback(System.Data.DataRow row) { }
        // RVA: 0x0AD86D68  token: 0x6000622
        private System.Void CascadeUpdate(System.Data.DataRow row) { }
        // RVA: 0x0AD87268  token: 0x6000623
        private System.Void CheckCanClearParentTable(System.Data.DataTable table) { }
        // RVA: 0x0AD87324  token: 0x6000624
        private System.Void CheckCanRemoveParentRow(System.Data.DataRow row) { }
        // RVA: 0x0AD873D0  token: 0x6000625
        private System.Void CheckCascade(System.Data.DataRow row, System.Data.DataRowAction action) { }
        // RVA: 0x0AD87540  token: 0x6000626
        private virtual System.Void CheckConstraint(System.Data.DataRow childRow, System.Data.DataRowAction action) { }
        // RVA: 0x0AD88930  token: 0x6000627
        private System.Void NonVirtualCheckState() { }
        // RVA: 0x0AD877E8  token: 0x6000628
        private virtual System.Void CheckState() { }
        // RVA: 0x0AD883B4  token: 0x600062B
        private virtual System.Boolean ContainsColumn(System.Data.DataColumn column) { }
        // RVA: 0x0AD883A4  token: 0x600062C
        private virtual System.Data.Constraint Clone(System.Data.DataSet destination) { }
        // RVA: 0x0AD877F0  token: 0x600062D
        private virtual System.Data.Constraint Clone(System.Data.DataSet destination, System.Boolean ignorNSforTableLookup) { }
        // RVA: 0x0AD87EC8  token: 0x600062E
        private System.Data.ForeignKeyConstraint Clone(System.Data.DataTable destination) { }
        // RVA: 0x0AD883F4  token: 0x600062F
        private System.Void Create(System.String relationName, System.Data.DataColumn[] parentColumns, System.Data.DataColumn[] childColumns) { }
        // RVA: 0x0AD885BC  token: 0x6000632
        public virtual System.Boolean Equals(System.Object key) { }
        // RVA: 0x04D87100  token: 0x6000633
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x0AD88680  token: 0x6000637
        private System.Data.DataRelation FindParentRelation() { }

    }

    // TypeToken: 0x2000070  // size: 0x14
    public sealed struct MappingType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Data.MappingType Element;  // const
        public static System.Data.MappingType Attribute;  // const
        public static System.Data.MappingType SimpleContent;  // const
        public static System.Data.MappingType Hidden;  // const

    }

    // TypeToken: 0x2000071  // size: 0x20
    public class MergeFailedEventArgs : System.EventArgs
    {
        // Fields
        private readonly System.Data.DataTable <Table>k__BackingField;  // 0x10
        private readonly System.String <Conflict>k__BackingField;  // 0x18

        // Properties
        System.String Conflict { get; /* RVA: 0x0385B100 */ }

        // Methods
        // RVA: 0x0AD8C04C  token: 0x600063B
        public System.Void .ctor(System.Data.DataTable table, System.String conflict) { }

    }

    // TypeToken: 0x2000072  // size: 0x80
    public sealed class MergeFailedEventHandler : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x086FC23C  token: 0x600063D
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x038E1420  token: 0x600063E
        public virtual System.Void Invoke(System.Object sender, System.Data.MergeFailedEventArgs e) { }

    }

    // TypeToken: 0x2000073  // size: 0x30
    public sealed class Merger
    {
        // Fields
        private System.Data.DataSet _dataSet;  // 0x10
        private System.Data.DataTable _dataTable;  // 0x18
        private System.Boolean _preserveChanges;  // 0x20
        private System.Data.MissingSchemaAction _missingSchemaAction;  // 0x24
        private System.Boolean _isStandAlonetable;  // 0x28
        private System.Boolean _IgnoreNSforTableLookup;  // 0x29

        // Methods
        // RVA: 0x0AD8E618  token: 0x600063F
        private System.Void .ctor(System.Data.DataSet dataSet, System.Boolean preserveChanges, System.Data.MissingSchemaAction missingSchemaAction) { }
        // RVA: 0x0AD8E5E0  token: 0x6000640
        private System.Void .ctor(System.Data.DataTable dataTable, System.Boolean preserveChanges, System.Data.MissingSchemaAction missingSchemaAction) { }
        // RVA: 0x0AD8C4F4  token: 0x6000641
        private System.Void MergeDataSet(System.Data.DataSet source) { }
        // RVA: 0x0AD8DF88  token: 0x6000642
        private System.Void MergeTable(System.Data.DataTable src) { }
        // RVA: 0x0AD8E224  token: 0x6000643
        private System.Void MergeTable(System.Data.DataTable src, System.Data.DataTable dst) { }
        // RVA: 0x0AD8D7A4  token: 0x6000644
        private System.Data.DataTable MergeSchema(System.Data.DataTable table) { }
        // RVA: 0x0AD8DEE4  token: 0x6000645
        private System.Void MergeTableData(System.Data.DataTable src) { }
        // RVA: 0x0AD8C1F0  token: 0x6000646
        private System.Void MergeConstraints(System.Data.DataSet source) { }
        // RVA: 0x0AD8C260  token: 0x6000647
        private System.Void MergeConstraints(System.Data.DataTable table) { }
        // RVA: 0x0AD8CFC4  token: 0x6000648
        private System.Void MergeRelation(System.Data.DataRelation relation) { }
        // RVA: 0x0AD8CEE0  token: 0x6000649
        private System.Void MergeExtendedProperties(System.Data.PropertyCollection src, System.Data.PropertyCollection dst) { }
        // RVA: 0x0AD8C0B8  token: 0x600064A
        private System.Data.DataKey GetSrcKey(System.Data.DataTable src, System.Data.DataTable dst) { }

    }

    // TypeToken: 0x2000074  // size: 0x14
    public sealed struct MissingSchemaAction
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Data.MissingSchemaAction Add;  // const
        public static System.Data.MissingSchemaAction Ignore;  // const
        public static System.Data.MissingSchemaAction Error;  // const
        public static System.Data.MissingSchemaAction AddWithKey;  // const

    }

    // TypeToken: 0x2000075  // size: 0x18
    public sealed class PrimaryKeyTypeConverter : System.ComponentModel.ReferenceConverter
    {
        // Methods
        // RVA: 0x0AD8E82C  token: 0x600064B
        public System.Void .ctor() { }
        // RVA: 0x012081B0  token: 0x600064C
        public virtual System.Boolean GetPropertiesSupported(System.ComponentModel.ITypeDescriptorContext context) { }
        // RVA: 0x0AD8E64C  token: 0x600064D
        public virtual System.Boolean CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Type destinationType) { }
        // RVA: 0x0AD8E6E0  token: 0x600064E
        public virtual System.Object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, System.Object value, System.Type destinationType) { }

    }

    // TypeToken: 0x2000076  // size: 0x50
    public class PropertyCollection : System.Collections.Hashtable, System.ICloneable
    {
        // Methods
        // RVA: 0x05395460  token: 0x600064F
        public System.Void .ctor() { }
        // RVA: 0x0AD8EA74  token: 0x6000650
        protected System.Void .ctor(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x0AD8E8B4  token: 0x6000651
        public virtual System.Object Clone() { }

    }

    // TypeToken: 0x2000077  // size: 0x1C
    public sealed struct Range
    {
        // Fields
        private System.Int32 _min;  // 0x10
        private System.Int32 _max;  // 0x14
        private System.Boolean _isNotNull;  // 0x18

        // Properties
        System.Int32 Count { get; /* RVA: 0x0AD8EB18 */ }
        System.Boolean IsNull { get; /* RVA: 0x0AD8EB2C */ }
        System.Int32 Min { get; /* RVA: 0x0AD8EB34 */ }

        // Methods
        // RVA: 0x0AD8EAD0  token: 0x6000652
        public System.Void .ctor(System.Int32 min, System.Int32 max) { }
        // RVA: 0x0AD8EA94  token: 0x6000656
        private System.Void CheckNull() { }

    }

    // TypeToken: 0x2000078  // size: 0x14
    public sealed struct RBTreeError
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Data.RBTreeError InvalidPageSize;  // const
        public static System.Data.RBTreeError PagePositionInSlotInUse;  // const
        public static System.Data.RBTreeError NoFreeSlots;  // const
        public static System.Data.RBTreeError InvalidStateinInsert;  // const
        public static System.Data.RBTreeError InvalidNextSizeInDelete;  // const
        public static System.Data.RBTreeError InvalidStateinDelete;  // const
        public static System.Data.RBTreeError InvalidNodeSizeinDelete;  // const
        public static System.Data.RBTreeError InvalidStateinEndDelete;  // const
        public static System.Data.RBTreeError CannotRotateInvalidsuccessorNodeinDelete;  // const
        public static System.Data.RBTreeError IndexOutOFRangeinGetNodeByIndex;  // const
        public static System.Data.RBTreeError RBDeleteFixup;  // const
        public static System.Data.RBTreeError UnsupportedAccessMethod1;  // const
        public static System.Data.RBTreeError UnsupportedAccessMethod2;  // const
        public static System.Data.RBTreeError UnsupportedAccessMethodInNonNillRootSubtree;  // const
        public static System.Data.RBTreeError AttachedNodeWithZerorbTreeNodeId;  // const
        public static System.Data.RBTreeError CompareNodeInDataRowTree;  // const
        public static System.Data.RBTreeError CompareSateliteTreeNodeInDataRowTree;  // const
        public static System.Data.RBTreeError NestedSatelliteTreeEnumerator;  // const

    }

    // TypeToken: 0x2000079  // size: 0x14
    public sealed struct TreeAccessMethod
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Data.TreeAccessMethod KEY_SEARCH_AND_INDEX;  // const
        public static System.Data.TreeAccessMethod INDEX_ONLY;  // const

    }

    // TypeToken: 0x200007A
    public abstract class RBTree`1 : System.Collections.IEnumerable
    {
        // Fields
        private System.Data.RBTree.TreePage<K>[] _pageTable;  // 0x0
        private System.Int32[] _pageTableMap;  // 0x0
        private System.Int32 _inUsePageCount;  // 0x0
        private System.Int32 _nextFreePageLine;  // 0x0
        public System.Int32 root;  // 0x0
        private System.Int32 _version;  // 0x0
        private System.Int32 _inUseNodeCount;  // 0x0
        private System.Int32 _inUseSatelliteTreeCount;  // 0x0
        private readonly System.Data.TreeAccessMethod _accessMethod;  // 0x0

        // Properties
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        System.Boolean HasDuplicates { get; /* RVA: -1  // not resolved */ }
        K Item { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000657
        protected virtual System.Int32 CompareNode(K record1, K record2) { }
        // RVA: -1  // abstract  token: 0x6000658
        protected virtual System.Int32 CompareSateliteTreeNode(K record1, K record2) { }
        // RVA: -1  // not resolved  token: 0x6000659
        protected System.Void .ctor(System.Data.TreeAccessMethod accessMethod) { }
        // RVA: -1  // not resolved  token: 0x600065A
        private System.Void InitTree() { }
        // RVA: -1  // not resolved  token: 0x600065B
        private System.Void FreePage(System.Data.RBTree.TreePage<K> page) { }
        // RVA: -1  // not resolved  token: 0x600065C
        private System.Data.RBTree.TreePage<K> AllocPage(System.Int32 size) { }
        // RVA: -1  // not resolved  token: 0x600065D
        private System.Void MarkPageFull(System.Data.RBTree.TreePage<K> page) { }
        // RVA: -1  // not resolved  token: 0x600065E
        private System.Void MarkPageFree(System.Data.RBTree.TreePage<K> page) { }
        // RVA: -1  // not resolved  token: 0x600065F
        private static System.Int32 GetIntValueFromBitMap(System.UInt32 bitMap) { }
        // RVA: -1  // not resolved  token: 0x6000660
        private System.Void FreeNode(System.Int32 nodeId) { }
        // RVA: -1  // not resolved  token: 0x6000661
        private System.Int32 GetIndexOfPageWithFreeSlot(System.Boolean allocatedPage) { }
        // RVA: -1  // not resolved  token: 0x6000664
        private System.Int32 GetNewNode(K key) { }
        // RVA: -1  // not resolved  token: 0x6000665
        private System.Int32 Successor(System.Int32 x_id) { }
        // RVA: -1  // not resolved  token: 0x6000666
        private System.Boolean Successor(System.Int32& nodeId, System.Int32& mainTreeNodeId) { }
        // RVA: -1  // not resolved  token: 0x6000667
        private System.Int32 Minimum(System.Int32 x_id) { }
        // RVA: -1  // not resolved  token: 0x6000668
        private System.Int32 LeftRotate(System.Int32 root_id, System.Int32 x_id, System.Int32 mainTreeNode) { }
        // RVA: -1  // not resolved  token: 0x6000669
        private System.Int32 RightRotate(System.Int32 root_id, System.Int32 x_id, System.Int32 mainTreeNode) { }
        // RVA: -1  // not resolved  token: 0x600066A
        private System.Int32 RBInsert(System.Int32 root_id, System.Int32 x_id, System.Int32 mainTreeNodeID, System.Int32 position, System.Boolean append) { }
        // RVA: -1  // not resolved  token: 0x600066B
        public System.Void UpdateNodeKey(K currentKey, K newKey) { }
        // RVA: -1  // not resolved  token: 0x600066C
        public K DeleteByIndex(System.Int32 i) { }
        // RVA: -1  // not resolved  token: 0x600066D
        public System.Int32 RBDelete(System.Int32 z_id) { }
        // RVA: -1  // not resolved  token: 0x600066E
        private System.Int32 RBDeleteX(System.Int32 root_id, System.Int32 z_id, System.Int32 mainTreeNodeID) { }
        // RVA: -1  // not resolved  token: 0x600066F
        private System.Int32 RBDeleteFixup(System.Int32 root_id, System.Int32 x_id, System.Int32 px_id, System.Int32 mainTreeNodeID) { }
        // RVA: -1  // not resolved  token: 0x6000670
        private System.Int32 SearchSubTree(System.Int32 root_id, K key) { }
        // RVA: -1  // not resolved  token: 0x6000672
        private System.Data.RBTree.NodePath<K> GetNodeByKey(K key) { }
        // RVA: -1  // not resolved  token: 0x6000673
        public System.Int32 GetIndexByKey(K key) { }
        // RVA: -1  // not resolved  token: 0x6000674
        public System.Int32 GetIndexByNode(System.Int32 node) { }
        // RVA: -1  // not resolved  token: 0x6000675
        private System.Int32 GetIndexByNodePath(System.Data.RBTree.NodePath<K> path) { }
        // RVA: -1  // not resolved  token: 0x6000676
        private System.Int32 ComputeIndexByNode(System.Int32 nodeId) { }
        // RVA: -1  // not resolved  token: 0x6000677
        private System.Int32 ComputeIndexWithSatelliteByNode(System.Int32 nodeId) { }
        // RVA: -1  // not resolved  token: 0x6000678
        private System.Data.RBTree.NodePath<K> GetNodeByIndex(System.Int32 userIndex) { }
        // RVA: -1  // not resolved  token: 0x6000679
        private System.Int32 ComputeNodeByIndex(System.Int32 index, System.Int32& satelliteRootId) { }
        // RVA: -1  // not resolved  token: 0x600067A
        private System.Int32 ComputeNodeByIndex(System.Int32 x_id, System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x600067B
        public System.Int32 Insert(K item) { }
        // RVA: -1  // not resolved  token: 0x600067C
        public System.Int32 Add(K item) { }
        // RVA: -1  // not resolved  token: 0x600067D
        public virtual System.Collections.IEnumerator GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x600067E
        public System.Int32 IndexOf(System.Int32 nodeId, K item) { }
        // RVA: -1  // not resolved  token: 0x600067F
        public System.Int32 Insert(System.Int32 position, K item) { }
        // RVA: -1  // not resolved  token: 0x6000680
        public System.Int32 InsertAt(System.Int32 position, K item, System.Boolean append) { }
        // RVA: -1  // not resolved  token: 0x6000681
        public System.Void RemoveAt(System.Int32 position) { }
        // RVA: -1  // not resolved  token: 0x6000682
        public System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x6000683
        public System.Void CopyTo(System.Array array, System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x6000684
        public System.Void CopyTo(K[] array, System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x6000685
        private System.Void SetRight(System.Int32 nodeId, System.Int32 rightNodeId) { }
        // RVA: -1  // not resolved  token: 0x6000686
        private System.Void SetLeft(System.Int32 nodeId, System.Int32 leftNodeId) { }
        // RVA: -1  // not resolved  token: 0x6000687
        private System.Void SetParent(System.Int32 nodeId, System.Int32 parentNodeId) { }
        // RVA: -1  // not resolved  token: 0x6000688
        private System.Void SetColor(System.Int32 nodeId, System.Data.RBTree.NodeColor<K> color) { }
        // RVA: -1  // not resolved  token: 0x6000689
        private System.Void SetKey(System.Int32 nodeId, K key) { }
        // RVA: -1  // not resolved  token: 0x600068A
        private System.Void SetNext(System.Int32 nodeId, System.Int32 nextNodeId) { }
        // RVA: -1  // not resolved  token: 0x600068B
        private System.Void SetSubTreeSize(System.Int32 nodeId, System.Int32 size) { }
        // RVA: -1  // not resolved  token: 0x600068C
        private System.Void IncreaseSize(System.Int32 nodeId) { }
        // RVA: -1  // not resolved  token: 0x600068D
        private System.Void RecomputeSize(System.Int32 nodeId) { }
        // RVA: -1  // not resolved  token: 0x600068E
        private System.Void DecreaseSize(System.Int32 nodeId) { }
        // RVA: -1  // not resolved  token: 0x600068F
        public System.Int32 Right(System.Int32 nodeId) { }
        // RVA: -1  // not resolved  token: 0x6000690
        public System.Int32 Left(System.Int32 nodeId) { }
        // RVA: -1  // not resolved  token: 0x6000691
        public System.Int32 Parent(System.Int32 nodeId) { }
        // RVA: -1  // not resolved  token: 0x6000692
        private System.Data.RBTree.NodeColor<K> color(System.Int32 nodeId) { }
        // RVA: -1  // not resolved  token: 0x6000693
        public System.Int32 Next(System.Int32 nodeId) { }
        // RVA: -1  // not resolved  token: 0x6000694
        public System.Int32 SubTreeSize(System.Int32 nodeId) { }
        // RVA: -1  // not resolved  token: 0x6000695
        public K Key(System.Int32 nodeId) { }

    }

    // TypeToken: 0x2000080  // size: 0x38
    public sealed class RecordManager
    {
        // Fields
        private readonly System.Data.DataTable _table;  // 0x10
        private System.Int32 _lastFreeRecord;  // 0x18
        private System.Int32 _minimumCapacity;  // 0x1c
        private System.Int32 _recordCapacity;  // 0x20
        private readonly System.Collections.Generic.List<System.Int32> _freeRecordList;  // 0x28
        private System.Data.DataRow[] _rows;  // 0x30

        // Properties
        System.Int32 LastFreeRecord { get; /* RVA: 0x011EF5B0 */ }
        System.Int32 MinimumCapacity { get; /* RVA: 0x04D86310 */ set; /* RVA: 0x0AD8F43C */ }
        System.Int32 RecordCapacity { get; /* RVA: 0x02B70F90 */ set; /* RVA: 0x0AD8F478 */ }
        System.Data.DataRow Item { get; /* RVA: 0x0AD8F3C4 */ set; /* RVA: 0x0AD8F3F0 */ }

        // Methods
        // RVA: 0x0AD8F2F8  token: 0x60006A4
        private System.Void .ctor(System.Data.DataTable table) { }
        // RVA: 0x0AD8F0A0  token: 0x60006A5
        private System.Void GrowRecordCapacity() { }
        // RVA: 0x0AD8F1C4  token: 0x60006AB
        private static System.Int32 NewCapacity(System.Int32 capacity) { }
        // RVA: 0x0AD8F28C  token: 0x60006AC
        private System.Int32 NormalizedMinimumCapacity(System.Int32 capacity) { }
        // RVA: 0x0AD8F1D4  token: 0x60006AD
        private System.Int32 NewRecordBase() { }
        // RVA: 0x0AD8EFA4  token: 0x60006AE
        private System.Void FreeRecord(System.Int32& record) { }
        // RVA: 0x0AD8EB4C  token: 0x60006AF
        private System.Void Clear(System.Boolean clearAll) { }
        // RVA: 0x0AD8F1A4  token: 0x60006B2
        private System.Int32 ImportRecord(System.Data.DataTable src, System.Int32 record) { }
        // RVA: 0x0AD8ED00  token: 0x60006B3
        private System.Int32 CopyRecord(System.Data.DataTable src, System.Int32 record, System.Int32 copy) { }
        // RVA: 0x0AD8F2C4  token: 0x60006B4
        private System.Void SetRowCache(System.Data.DataRow[] newRows) { }

    }

    // TypeToken: 0x2000081  // size: 0xD8
    public sealed class RelatedView : System.Data.DataView, System.Data.IFilter
    {
        // Fields
        private readonly System.Nullable<System.Data.DataKey> _parentKey;  // 0xb0
        private readonly System.Data.DataKey _childKey;  // 0xc0
        private readonly System.Data.DataRowView _parentRowView;  // 0xc8
        private readonly System.Object[] _filterValues;  // 0xd0

        // Methods
        // RVA: 0x0AD8F720  token: 0x60006B5
        public System.Void .ctor(System.Data.DataColumn[] columns, System.Object[] values) { }
        // RVA: 0x0AD8F86C  token: 0x60006B6
        public System.Void .ctor(System.Data.DataRowView parentRowView, System.Data.DataKey parentKey, System.Data.DataColumn[] childKeyColumns) { }
        // RVA: 0x0AD8F570  token: 0x60006B7
        private System.Object[] GetParentValues() { }
        // RVA: 0x0AD8F60C  token: 0x60006B8
        public virtual System.Boolean Invoke(System.Data.DataRow row, System.Data.DataRowVersion version) { }
        // RVA: 0x0464E630  token: 0x60006B9
        private virtual System.Data.IFilter GetFilter() { }
        // RVA: 0x0AD8F500  token: 0x60006BA
        public virtual System.Data.DataRowView AddNew() { }
        // RVA: 0x0AD8F6F4  token: 0x60006BB
        private virtual System.Void SetIndex(System.String newSort, System.Data.DataViewRowState newRowStates, System.Data.IFilter newRowFilter) { }

    }

    // TypeToken: 0x2000082  // size: 0x10
    public sealed class RelationshipConverter : System.ComponentModel.ExpandableObjectConverter
    {
        // Methods
        // RVA: 0x041E1670  token: 0x60006BC
        public System.Void .ctor() { }
        // RVA: 0x0AD8F9A8  token: 0x60006BD
        public virtual System.Boolean CanConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Type destinationType) { }
        // RVA: 0x0AD8FA3C  token: 0x60006BE
        public virtual System.Object ConvertTo(System.ComponentModel.ITypeDescriptorContext context, System.Globalization.CultureInfo culture, System.Object value, System.Type destinationType) { }

    }

    // TypeToken: 0x2000083  // size: 0x14
    public sealed struct Rule
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Data.Rule None;  // const
        public static System.Data.Rule Cascade;  // const
        public static System.Data.Rule SetNull;  // const
        public static System.Data.Rule SetDefault;  // const

    }

    // TypeToken: 0x2000084  // size: 0x14
    public sealed struct SchemaSerializationMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Data.SchemaSerializationMode IncludeSchema;  // const
        public static System.Data.SchemaSerializationMode ExcludeSchema;  // const

    }

    // TypeToken: 0x2000085  // size: 0x70
    public sealed class Select
    {
        // Fields
        private readonly System.Data.DataTable _table;  // 0x10
        private readonly System.Data.IndexField[] _indexFields;  // 0x18
        private System.Data.DataViewRowState _recordStates;  // 0x20
        private System.Data.DataExpression _rowFilter;  // 0x28
        private System.Data.ExpressionNode _expression;  // 0x30
        private System.Data.Index _index;  // 0x38
        private System.Int32[] _records;  // 0x40
        private System.Int32 _recordCount;  // 0x48
        private System.Data.ExpressionNode _linearExpression;  // 0x50
        private System.Boolean _candidatesForBinarySearch;  // 0x58
        private System.Data.Select.ColumnInfo[] _candidateColumns;  // 0x60
        private System.Int32 _nCandidates;  // 0x68
        private System.Int32 _matchedCandidates;  // 0x6c

        // Methods
        // RVA: 0x0AD92D84  token: 0x60006BF
        public System.Void .ctor(System.Data.DataTable table, System.String filterExpression, System.String sort, System.Data.DataViewRowState recordStates) { }
        // RVA: 0x0AD92960  token: 0x60006C0
        private System.Boolean IsSupportedOperator(System.Int32 op) { }
        // RVA: 0x0AD90468  token: 0x60006C1
        private System.Void AnalyzeExpression(System.Data.BinaryNode expr) { }
        // RVA: 0x0AD90D4C  token: 0x60006C2
        private System.Boolean CompareSortIndexDesc(System.Data.IndexField[] fields) { }
        // RVA: 0x0AD92084  token: 0x60006C3
        private System.Boolean FindSortIndex() { }
        // RVA: 0x0AD90A3C  token: 0x60006C4
        private System.Int32 CompareClosestCandidateIndexDesc(System.Data.IndexField[] fields) { }
        // RVA: 0x0AD91CC0  token: 0x60006C5
        private System.Boolean FindClosestCandidateIndex() { }
        // RVA: 0x0AD927DC  token: 0x60006C6
        private System.Void InitCandidateColumns() { }
        // RVA: 0x0AD90E80  token: 0x60006C7
        private System.Void CreateIndex() { }
        // RVA: 0x0AD928DC  token: 0x60006C8
        private System.Boolean IsOperatorIn(System.Data.ExpressionNode enode) { }
        // RVA: 0x0AD90880  token: 0x60006C9
        private System.Void BuildLinearExpression() { }
        // RVA: 0x0AD92978  token: 0x60006CA
        public System.Data.DataRow[] SelectRows() { }
        // RVA: 0x0AD92728  token: 0x60006CB
        public System.Data.DataRow[] GetRows() { }
        // RVA: 0x0AD902E0  token: 0x60006CC
        private System.Boolean AcceptRecord(System.Int32 record) { }
        // RVA: 0x0AD914D8  token: 0x60006CD
        private System.Int32 Eval(System.Data.BinaryNode expr, System.Data.DataRow row, System.Data.DataRowVersion version) { }
        // RVA: 0x0AD91B8C  token: 0x60006CE
        private System.Int32 Evaluate(System.Int32 record) { }
        // RVA: 0x0AD91F50  token: 0x60006CF
        private System.Int32 FindFirstMatchingRecord() { }
        // RVA: 0x0AD91FEC  token: 0x60006D0
        private System.Int32 FindLastMatchingRecord(System.Int32 lo) { }
        // RVA: 0x0AD92278  token: 0x60006D1
        private System.Data.Range GetBinaryFilteredRecords() { }
        // RVA: 0x0AD92328  token: 0x60006D2
        private System.Int32[] GetLinearFilteredRecords(System.Data.Range range) { }
        // RVA: 0x0AD92550  token: 0x60006D3
        private System.Data.DataRow[] GetLinearFilteredRows(System.Data.Range range) { }
        // RVA: 0x0AD90AE4  token: 0x60006D4
        private System.Int32 CompareRecords(System.Int32 record1, System.Int32 record2) { }
        // RVA: 0x0AD92C34  token: 0x60006D5
        private System.Void Sort(System.Int32 left, System.Int32 right) { }

    }

    // TypeToken: 0x2000087  // size: 0x20
    public sealed struct IndexField
    {
        // Fields
        public readonly System.Data.DataColumn Column;  // 0x10
        public readonly System.Boolean IsDescending;  // 0x18

        // Methods
        // RVA: 0x086D0120  token: 0x60006D7
        private System.Void .ctor(System.Data.DataColumn column, System.Boolean isDescending) { }
        // RVA: 0x0AD88FC8  token: 0x60006D8
        public static System.Boolean op_Equality(System.Data.IndexField if1, System.Data.IndexField if2) { }
        // RVA: 0x0AD88ED0  token: 0x60006D9
        public virtual System.Boolean Equals(System.Object obj) { }
        // RVA: 0x0AD88F54  token: 0x60006DA
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x2000088  // size: 0x58
    public sealed class Index
    {
        // Fields
        private readonly System.Data.DataTable _table;  // 0x10
        private readonly System.Data.IndexField[] _indexFields;  // 0x18
        private readonly System.Comparison<System.Data.DataRow> _comparison;  // 0x20
        private readonly System.Data.DataViewRowState _recordStates;  // 0x28
        private System.WeakReference _rowFilter;  // 0x30
        private System.Data.Index.IndexTree _records;  // 0x38
        private System.Int32 _recordCount;  // 0x40
        private System.Int32 _refCount;  // 0x44
        private System.Data.Listeners<System.Data.DataViewListener> _listeners;  // 0x48
        private System.Boolean _suspendEvents;  // 0x50
        private readonly System.Boolean _isSharable;  // 0x51
        private readonly System.Boolean _hasRemoteAggregate;  // 0x52
        private static System.Int32 s_objectTypeCount;  // static @ 0x0
        private readonly System.Int32 _objectID;  // 0x54

        // Properties
        System.Boolean HasRemoteAggregate { get; /* RVA: 0x04DAA1F0 */ }
        System.Int32 ObjectID { get; /* RVA: 0x04D86540 */ }
        System.Data.DataViewRowState RecordStates { get; /* RVA: 0x011F2A90 */ }
        System.Data.IFilter RowFilter { get; /* RVA: 0x0AD8BFF4 */ }
        System.Boolean HasDuplicates { get; /* RVA: 0x0AD8BFB0 */ }
        System.Int32 RecordCount { get; /* RVA: 0x04D865E0 */ }
        System.Boolean IsSharable { get; /* RVA: 0x04D867F0 */ }
        System.Int32 RefCount { get; /* RVA: 0x04D86530 */ }
        System.Boolean DoListChanged { get; /* RVA: 0x0AD8BF48 */ }
        System.Data.DataTable Table { get; /* RVA: 0x02B2ECC0 */ }

        // Methods
        // RVA: 0x0AD8BF1C  token: 0x60006DB
        public System.Void .ctor(System.Data.DataTable table, System.Data.IndexField[] indexFields, System.Data.DataViewRowState recordStates, System.Data.IFilter rowFilter) { }
        // RVA: 0x0AD8BBA0  token: 0x60006DC
        public System.Void .ctor(System.Data.DataTable table, System.Comparison<System.Data.DataRow> comparison, System.Data.DataViewRowState recordStates, System.Data.IFilter rowFilter) { }
        // RVA: 0x0AD8A000  token: 0x60006DD
        private static System.Data.IndexField[] GetAllFields(System.Data.DataColumnCollection columns) { }
        // RVA: 0x0AD8BC10  token: 0x60006DE
        private System.Void .ctor(System.Data.DataTable table, System.Data.IndexField[] indexFields, System.Comparison<System.Data.DataRow> comparison, System.Data.DataViewRowState recordStates, System.Data.IFilter rowFilter) { }
        // RVA: 0x0AD89928  token: 0x60006DF
        public System.Boolean Equal(System.Data.IndexField[] indexDesc, System.Data.DataViewRowState recordStates, System.Data.IFilter rowFilter) { }
        // RVA: 0x0AD8A488  token: 0x60006E4
        public System.Int32 GetRecord(System.Int32 recordIndex) { }
        // RVA: 0x0AD89180  token: 0x60006E8
        private System.Boolean AcceptRecord(System.Int32 record) { }
        // RVA: 0x0AD89078  token: 0x60006E9
        private System.Boolean AcceptRecord(System.Int32 record, System.Data.IFilter filter) { }
        // RVA: 0x0AD8AF04  token: 0x60006EA
        private System.Void ListChangedAdd(System.Data.DataViewListener listener) { }
        // RVA: 0x0AD8AF58  token: 0x60006EB
        private System.Void ListChangedRemove(System.Data.DataViewListener listener) { }
        // RVA: 0x0AD891B0  token: 0x60006ED
        public System.Void AddRef() { }
        // RVA: 0x0AD8B944  token: 0x60006EE
        public System.Int32 RemoveRef() { }
        // RVA: 0x0AD89344  token: 0x60006EF
        private System.Void ApplyChangeAction(System.Int32 record, System.Int32 action, System.Int32 changeRecord) { }
        // RVA: 0x0AD893BC  token: 0x60006F0
        public System.Boolean CheckUnique() { }
        // RVA: 0x0AD896B8  token: 0x60006F1
        private System.Int32 CompareRecords(System.Int32 record1, System.Int32 record2) { }
        // RVA: 0x0AD893D0  token: 0x60006F2
        private System.Int32 CompareDataRows(System.Int32 record1, System.Int32 record2) { }
        // RVA: 0x0AD89458  token: 0x60006F3
        private System.Int32 CompareDuplicateRecords(System.Int32 record1, System.Int32 record2) { }
        // RVA: 0x0AD895FC  token: 0x60006F4
        private System.Int32 CompareRecordToKey(System.Int32 record1, System.Object[] vals) { }
        // RVA: 0x0AD89804  token: 0x60006F5
        public System.Void DeleteRecordFromIndex(System.Int32 recordIndex) { }
        // RVA: 0x0AD8991C  token: 0x60006F6
        private System.Void DeleteRecord(System.Int32 recordIndex) { }
        // RVA: 0x0AD89810  token: 0x60006F7
        private System.Void DeleteRecord(System.Int32 recordIndex, System.Boolean fireEvent) { }
        // RVA: 0x0AD8A0DC  token: 0x60006F8
        public System.Data.RBTree.RBTreeEnumerator<System.Int32> GetEnumerator(System.Int32 startIndex) { }
        // RVA: 0x0AD8A148  token: 0x60006F9
        public System.Int32 GetIndex(System.Int32 record) { }
        // RVA: 0x0AD8A19C  token: 0x60006FA
        private System.Int32 GetIndex(System.Int32 record, System.Int32 changeRecord) { }
        // RVA: 0x0AD8A694  token: 0x60006FB
        public System.Object[] GetUniqueKeyValues() { }
        // RVA: 0x0AD89ABC  token: 0x60006FC
        private System.Int32 FindNodeByKey(System.Object originalKey) { }
        // RVA: 0x0AD89CB4  token: 0x60006FD
        private System.Int32 FindNodeByKeys(System.Object[] originalKey) { }
        // RVA: 0x0AD899E4  token: 0x60006FE
        private System.Int32 FindNodeByKeyRecord(System.Int32 record) { }
        // RVA: 0x0AD8A35C  token: 0x60006FF
        private System.Data.Range GetRangeFromNode(System.Int32 nodeId) { }
        // RVA: 0x0AD89EEC  token: 0x6000700
        public System.Data.Range FindRecords(System.Object key) { }
        // RVA: 0x0AD89E9C  token: 0x6000701
        public System.Data.Range FindRecords(System.Object[] key) { }
        // RVA: 0x0AD89F3C  token: 0x6000702
        private System.Void FireResetEvent() { }
        // RVA: 0x0AD8A0C0  token: 0x6000703
        private System.Int32 GetChangeAction(System.Data.DataViewRowState oldState, System.Data.DataViewRowState newState) { }
        // RVA: 0x0AD8A4DC  token: 0x6000704
        private static System.Int32 GetReplaceAction(System.Data.DataViewRowState oldState) { }
        // RVA: 0x0AD8A4F4  token: 0x6000705
        public System.Data.DataRow GetRow(System.Int32 i) { }
        // RVA: 0x0AD8A650  token: 0x6000706
        public System.Data.DataRow[] GetRows(System.Object[] values) { }
        // RVA: 0x0AD8A52C  token: 0x6000707
        public System.Data.DataRow[] GetRows(System.Data.Range range) { }
        // RVA: 0x0AD8A8F4  token: 0x6000708
        private System.Void InitRecords(System.Data.IFilter filter) { }
        // RVA: 0x0AD8AC84  token: 0x6000709
        public System.Int32 InsertRecordToIndex(System.Int32 record) { }
        // RVA: 0x0AD8ACC4  token: 0x600070A
        private System.Int32 InsertRecord(System.Int32 record, System.Boolean fireEvent) { }
        // RVA: 0x0AD8AEBC  token: 0x600070B
        public System.Boolean IsKeyInIndex(System.Object key) { }
        // RVA: 0x0AD8AED4  token: 0x600070C
        public System.Boolean IsKeyInIndex(System.Object[] key) { }
        // RVA: 0x0AD8AEEC  token: 0x600070D
        public System.Boolean IsKeyRecordInIndex(System.Int32 record) { }
        // RVA: 0x0AD8B160  token: 0x600070F
        private System.Void OnListChanged(System.ComponentModel.ListChangedType changedType, System.Int32 newIndex, System.Int32 oldIndex) { }
        // RVA: 0x0AD8B0CC  token: 0x6000710
        private System.Void OnListChanged(System.ComponentModel.ListChangedType changedType, System.Int32 index) { }
        // RVA: 0x0AD8B20C  token: 0x6000711
        private System.Void OnListChanged(System.ComponentModel.ListChangedEventArgs e) { }
        // RVA: 0x0AD8AFAC  token: 0x6000712
        private System.Void MaintainDataView(System.ComponentModel.ListChangedType changedType, System.Int32 record, System.Boolean trackAddRemove) { }
        // RVA: 0x0AD8BAE4  token: 0x6000713
        public System.Void Reset() { }
        // RVA: 0x0AD8B494  token: 0x6000714
        public System.Void RecordChanged(System.Int32 record) { }
        // RVA: 0x0AD8B398  token: 0x6000715
        public System.Void RecordChanged(System.Int32 oldIndex, System.Int32 newIndex) { }
        // RVA: 0x0AD8B854  token: 0x6000716
        public System.Void RecordStateChanged(System.Int32 record, System.Data.DataViewRowState oldState, System.Data.DataViewRowState newState) { }
        // RVA: 0x0AD8B55C  token: 0x6000717
        public System.Void RecordStateChanged(System.Int32 oldRecord, System.Data.DataViewRowState oldOldState, System.Data.DataViewRowState oldNewState, System.Int32 newRecord, System.Data.DataViewRowState newOldState, System.Data.DataViewRowState newNewState) { }
        // RVA: 0x0AD8A760  token: 0x6000719
        private System.Void GetUniqueKeyValues(System.Collections.Generic.List<System.Object[]> list, System.Int32 curNodeId) { }
        // RVA: -1  // generic def  token: 0x600071A
        private static System.Int32 IndexOfReference(System.Collections.Generic.List<T> list, T item) { }

    }

    // TypeToken: 0x200008C
    public sealed class Listeners`1
    {
        // Fields
        private readonly System.Collections.Generic.List<TElem> _listeners;  // 0x0
        private readonly System.Data.Listeners.Func<TElem,TElem,System.Boolean> _filter;  // 0x0
        private readonly System.Int32 _objectID;  // 0x0
        private System.Int32 _listenerReaderCount;  // 0x0

        // Properties
        System.Boolean HasListeners { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000724
        private System.Void .ctor(System.Int32 ObjectID, System.Data.Listeners.Func<TElem,TElem,System.Boolean> notifyFilter) { }
        // RVA: -1  // not resolved  token: 0x6000726
        private System.Void Add(TElem listener) { }
        // RVA: -1  // not resolved  token: 0x6000727
        private System.Int32 IndexOfReference(TElem listener) { }
        // RVA: -1  // not resolved  token: 0x6000728
        private System.Void Remove(TElem listener) { }
        // RVA: -1  // generic def  token: 0x6000729
        private System.Void Notify(T1 arg1, T2 arg2, T3 arg3, System.Data.Listeners.Action<TElem,TElem,T1,T2,T3> action) { }
        // RVA: -1  // not resolved  token: 0x600072A
        private System.Void RemoveNullListeners(System.Int32 nullIndex) { }

    }

    // TypeToken: 0x200008F  // size: 0x78
    public sealed class SimpleType : System.Runtime.Serialization.ISerializable
    {
        // Fields
        private System.String _baseType;  // 0x10
        private System.Data.SimpleType _baseSimpleType;  // 0x18
        private System.Xml.XmlQualifiedName _xmlBaseType;  // 0x20
        private System.String _name;  // 0x28
        private System.Int32 _length;  // 0x30
        private System.Int32 _minLength;  // 0x34
        private System.Int32 _maxLength;  // 0x38
        private System.String _pattern;  // 0x40
        private System.String _ns;  // 0x48
        private System.String _maxExclusive;  // 0x50
        private System.String _maxInclusive;  // 0x58
        private System.String _minExclusive;  // 0x60
        private System.String _minInclusive;  // 0x68
        private System.String _enumeration;  // 0x70

        // Properties
        System.String BaseType { get; /* RVA: 0x02B2ECC0 */ }
        System.Xml.XmlQualifiedName XmlBaseType { get; /* RVA: 0x04D862C0 */ }
        System.String Name { get; /* RVA: 0x04D86240 */ }
        System.String Namespace { get; /* RVA: 0x04D86200 */ }
        System.Int32 Length { get; /* RVA: 0x04D864F0 */ }
        System.Int32 MaxLength { get; /* RVA: 0x04D863B0 */ set; /* RVA: 0x04D86430 */ }
        System.Data.SimpleType BaseSimpleType { get; /* RVA: 0x0385B100 */ }
        System.String SimpleTypeQualifiedName { get; /* RVA: 0x0AD943F0 */ }

        // Methods
        // RVA: 0x0AD940D0  token: 0x600072F
        private System.Void .ctor(System.String baseType) { }
        // RVA: 0x0AD94214  token: 0x6000730
        private System.Void .ctor(System.Xml.Schema.XmlSchemaSimpleType node) { }
        // RVA: 0x0AD93CAC  token: 0x6000731
        private virtual System.Void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }
        // RVA: 0x0AD93358  token: 0x6000732
        private System.Void LoadTypeValues(System.Xml.Schema.XmlSchemaSimpleType node) { }
        // RVA: 0x0AD93250  token: 0x6000733
        private System.Boolean IsPlainString() { }
        // RVA: 0x0AD93C44  token: 0x600073D
        private System.String QualifiedName(System.String name) { }
        // RVA: 0x0AD93CF8  token: 0x600073E
        private System.Xml.XmlNode ToNode(System.Xml.XmlDocument dc, System.Collections.Hashtable prefixes, System.Boolean inRemoting) { }
        // RVA: 0x0AD92FB4  token: 0x600073F
        private static System.Data.SimpleType CreateEnumeratedType(System.String values) { }
        // RVA: 0x0AD92F50  token: 0x6000740
        private static System.Data.SimpleType CreateByteArrayType(System.String encoding) { }
        // RVA: 0x0AD93030  token: 0x6000741
        private static System.Data.SimpleType CreateLimitedStringType(System.Int32 length) { }
        // RVA: 0x0AD930A4  token: 0x6000742
        private static System.Data.SimpleType CreateSimpleType(System.Data.Common.StorageType typeCode, System.Type type) { }
        // RVA: 0x0AD93160  token: 0x6000743
        private System.String HasConflictingDefinition(System.Data.SimpleType otherSimpleType) { }
        // RVA: 0x0AD92E50  token: 0x6000744
        private System.Boolean CanHaveMaxLength() { }
        // RVA: 0x0AD92EAC  token: 0x6000745
        private System.Void ConvertToAnnonymousSimpleType() { }

    }

    // TypeToken: 0x2000090  // size: 0x60
    public class UniqueConstraint : System.Data.Constraint
    {
        // Fields
        private System.Data.DataKey _key;  // 0x38
        private System.Data.Index _constraintIndex;  // 0x40
        private System.Boolean _bPrimaryKey;  // 0x48
        private System.String _constraintName;  // 0x50
        private System.String[] _columnNames;  // 0x58

        // Properties
        System.String[] ColumnNames { get; /* RVA: 0x0AD9552C */ }
        System.Data.Index ConstraintIndex { get; /* RVA: 0x04D85A60 */ }
        System.Data.DataColumn[] Columns { get; /* RVA: 0x0AD95538 */ }
        System.Data.DataColumn[] ColumnsReference { get; /* RVA: 0x04D85A50 */ }
        System.Boolean IsPrimaryKey { get; /* RVA: 0x0AD95544 */ }
        System.Boolean InCollection { set; /* RVA: 0x0AD95598 */ }
        System.Data.DataKey Key { get; /* RVA: 0x04D85A50 */ }
        System.Data.DataTable Table { get; /* RVA: 0x0AD95580 */ }

        // Methods
        // RVA: 0x0AD953AC  token: 0x6000746
        public System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0AD95440  token: 0x6000747
        public System.Void .ctor(System.String name, System.Data.DataColumn[] columns) { }
        // RVA: 0x0AD95378  token: 0x6000748
        public System.Void .ctor(System.Data.DataColumn[] columns) { }
        // RVA: 0x0AD954D0  token: 0x6000749
        public System.Void .ctor(System.String name, System.String[] columnNames, System.Boolean isPrimaryKey) { }
        // RVA: 0x0AD95480  token: 0x600074A
        public System.Void .ctor(System.String name, System.Data.DataColumn[] columns, System.Boolean isPrimaryKey) { }
        // RVA: 0x0AD94FB8  token: 0x600074D
        private System.Void ConstraintIndexClear() { }
        // RVA: 0x0AD94FF0  token: 0x600074E
        private System.Void ConstraintIndexInitialize() { }
        // RVA: 0x0AD947B8  token: 0x600074F
        private virtual System.Void CheckState() { }
        // RVA: 0x0AD947B8  token: 0x6000750
        private System.Void NonVirtualCheckState() { }
        // RVA: 0x041E1670  token: 0x6000751
        private virtual System.Void CheckCanAddToCollection(System.Data.ConstraintCollection constraints) { }
        // RVA: 0x0AD94514  token: 0x6000752
        private virtual System.Boolean CanBeRemovedFromCollection(System.Data.ConstraintCollection constraints, System.Boolean fThrowException) { }
        // RVA: 0x0AD9468C  token: 0x6000753
        private virtual System.Boolean CanEnableConstraint() { }
        // RVA: 0x0AD95194  token: 0x6000754
        private virtual System.Boolean IsConstraintViolated() { }
        // RVA: 0x0AD946D4  token: 0x6000755
        private virtual System.Void CheckConstraint(System.Data.DataRow row, System.Data.DataRowAction action) { }
        // RVA: 0x0AD95034  token: 0x6000756
        private virtual System.Boolean ContainsColumn(System.Data.DataColumn column) { }
        // RVA: 0x0AD883A4  token: 0x6000757
        private virtual System.Data.Constraint Clone(System.Data.DataSet destination) { }
        // RVA: 0x0AD94B44  token: 0x6000758
        private virtual System.Data.Constraint Clone(System.Data.DataSet destination, System.Boolean ignorNSforTableLookup) { }
        // RVA: 0x0AD947C4  token: 0x6000759
        private System.Data.UniqueConstraint Clone(System.Data.DataTable table) { }
        // RVA: 0x0AD95040  token: 0x600075D
        private System.Void Create(System.String constraintName, System.Data.DataColumn[] columns) { }
        // RVA: 0x0AD9510C  token: 0x600075E
        public virtual System.Boolean Equals(System.Object key2) { }
        // RVA: 0x04D87100  token: 0x600075F
        public virtual System.Int32 GetHashCode() { }

    }

    // TypeToken: 0x2000091  // size: 0x30
    public sealed class XDRSchema : System.Data.XMLSchema
    {
        // Fields
        private System.String _schemaName;  // 0x10
        private System.String _schemaUri;  // 0x18
        private System.Xml.XmlElement _schemaRoot;  // 0x20
        private System.Data.DataSet _ds;  // 0x28
        private static readonly System.Char[] s_colonArray;  // static @ 0x0
        private static System.Data.XDRSchema.NameType[] s_mapNameTypeXdr;  // static @ 0x8
        private static System.Data.XDRSchema.NameType s_enumerationNameType;  // static @ 0x10

        // Methods
        // RVA: 0x0AD986CC  token: 0x6000763
        private System.Void .ctor(System.Data.DataSet ds, System.Boolean fInline) { }
        // RVA: 0x0AD974CC  token: 0x6000764
        private System.Void LoadSchema(System.Xml.XmlElement schemaRoot, System.Data.DataSet ds) { }
        // RVA: 0x0AD956A4  token: 0x6000765
        private System.Xml.XmlElement FindTypeNode(System.Xml.XmlElement node) { }
        // RVA: 0x0AD971F8  token: 0x6000766
        private System.Boolean IsTextOnlyContent(System.Xml.XmlElement node) { }
        // RVA: 0x0AD97398  token: 0x6000767
        private System.Boolean IsXDRField(System.Xml.XmlElement node, System.Xml.XmlElement typeNode) { }
        // RVA: 0x0AD966E8  token: 0x6000768
        private System.Data.DataTable HandleTable(System.Xml.XmlElement node) { }
        // RVA: 0x0AD955E0  token: 0x6000769
        private static System.Data.XDRSchema.NameType FindNameType(System.String name) { }
        // RVA: 0x0AD976C8  token: 0x600076A
        private System.Type ParseDataType(System.String dt, System.String dtValues) { }
        // RVA: 0x0AD9596C  token: 0x600076B
        private System.String GetInstanceName(System.Xml.XmlElement node) { }
        // RVA: 0x0AD95E98  token: 0x600076C
        private System.Void HandleColumn(System.Xml.XmlElement node, System.Data.DataTable table) { }
        // RVA: 0x0AD95E74  token: 0x600076D
        private System.Void GetMinMax(System.Xml.XmlElement elNode, System.Int32& minOccurs, System.Int32& maxOccurs) { }
        // RVA: 0x0AD95ADC  token: 0x600076E
        private System.Void GetMinMax(System.Xml.XmlElement elNode, System.Boolean isAttribute, System.Int32& minOccurs, System.Int32& maxOccurs) { }
        // RVA: 0x0AD9686C  token: 0x600076F
        private System.Void HandleTypeNode(System.Xml.XmlElement typeNode, System.Data.DataTable table, System.Collections.ArrayList tableChildren) { }
        // RVA: 0x0AD96C50  token: 0x6000770
        private System.Data.DataTable InstantiateTable(System.Data.DataSet dataSet, System.Xml.XmlElement node, System.Xml.XmlElement typeNode) { }
        // RVA: 0x0AD969E4  token: 0x6000771
        private System.Data.DataTable InstantiateSimpleTable(System.Data.DataSet dataSet, System.Xml.XmlElement node) { }
        // RVA: 0x0AD97808  token: 0x6000772
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000093  // size: 0x28
    public sealed class XMLDiffLoader
    {
        // Fields
        private System.Collections.ArrayList _tables;  // 0x10
        private System.Data.DataSet _dataSet;  // 0x18
        private System.Data.DataTable _dataTable;  // 0x20

        // Methods
        // RVA: 0x0AD98C5C  token: 0x6000775
        private System.Void LoadDiffGram(System.Data.DataSet ds, System.Xml.XmlReader dataTextReader) { }
        // RVA: 0x0AD988F0  token: 0x6000776
        private System.Void CreateTablesHierarchy(System.Data.DataTable dt) { }
        // RVA: 0x0AD98DB0  token: 0x6000777
        private System.Void LoadDiffGram(System.Data.DataTable dt, System.Xml.XmlReader dataTextReader) { }
        // RVA: 0x0AD98F6C  token: 0x6000778
        private System.Void ProcessDiffs(System.Data.DataSet ds, System.Xml.XmlReader ssync) { }
        // RVA: 0x0AD99218  token: 0x6000779
        private System.Void ProcessDiffs(System.Collections.ArrayList tableList, System.Xml.XmlReader ssync) { }
        // RVA: 0x0AD99848  token: 0x600077A
        private System.Void ProcessErrors(System.Data.DataSet ds, System.Xml.XmlReader ssync) { }
        // RVA: 0x0AD994B4  token: 0x600077B
        private System.Void ProcessErrors(System.Collections.ArrayList dt, System.Xml.XmlReader ssync) { }
        // RVA: 0x0AD98AF4  token: 0x600077C
        private System.Data.DataTable GetTable(System.String tableName, System.String ns) { }
        // RVA: 0x0AD99B28  token: 0x600077D
        private System.Int32 ReadOldRowData(System.Data.DataSet ds, System.Data.DataTable& table, System.Int32& pos, System.Xml.XmlReader row) { }
        // RVA: 0x0AD9AE30  token: 0x600077E
        private System.Void SkipWhitespaces(System.Xml.XmlReader reader) { }
        // RVA: 0x041E1670  token: 0x600077F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000094  // size: 0x10
    public class XMLSchema
    {
        // Methods
        // RVA: 0x0AD9B12C  token: 0x6000780
        private static System.ComponentModel.TypeConverter GetConverter(System.Type type) { }
        // RVA: 0x0AD9B178  token: 0x6000781
        private static System.Void SetProperties(System.Object instance, System.Xml.XmlAttributeCollection attrs) { }
        // RVA: 0x0AD9AE80  token: 0x6000782
        private static System.Boolean FEqualIdentity(System.Xml.XmlNode node, System.String name, System.String ns) { }
        // RVA: 0x0AD9AFF8  token: 0x6000783
        private static System.Boolean GetBooleanAttribute(System.Xml.XmlElement element, System.String attrName, System.String attrNS, System.Boolean defVal) { }
        // RVA: 0x0AD9AEF0  token: 0x6000784
        private static System.String GenUniqueColumnName(System.String proposedName, System.Data.DataTable table) { }
        // RVA: 0x041E1670  token: 0x6000785
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000095  // size: 0x20
    public sealed class ConstraintTable
    {
        // Fields
        public System.Data.DataTable table;  // 0x10
        public System.Xml.Schema.XmlSchemaIdentityConstraint constraint;  // 0x18

        // Methods
        // RVA: 0x032AF420  token: 0x6000786
        public System.Void .ctor(System.Data.DataTable t, System.Xml.Schema.XmlSchemaIdentityConstraint c) { }

    }

    // TypeToken: 0x2000096  // size: 0xA8
    public sealed class XSDSchema : System.Data.XMLSchema
    {
        // Fields
        private System.Xml.Schema.XmlSchemaSet _schemaSet;  // 0x10
        private System.Xml.Schema.XmlSchemaElement _dsElement;  // 0x18
        private System.Data.DataSet _ds;  // 0x20
        private System.String _schemaName;  // 0x28
        private System.Collections.ArrayList _columnExpressions;  // 0x30
        private System.Collections.Hashtable _constraintNodes;  // 0x38
        private System.Collections.ArrayList _refTables;  // 0x40
        private System.Collections.ArrayList _complexTypes;  // 0x48
        private System.Xml.Schema.XmlSchemaObjectCollection _annotations;  // 0x50
        private System.Xml.Schema.XmlSchemaObjectCollection _elements;  // 0x58
        private System.Collections.Hashtable _attributes;  // 0x60
        private System.Collections.Hashtable _elementsTable;  // 0x68
        private System.Collections.Hashtable _attributeGroups;  // 0x70
        private System.Collections.Hashtable _schemaTypes;  // 0x78
        private System.Collections.Hashtable _expressions;  // 0x80
        private System.Collections.Generic.Dictionary<System.Data.DataTable,System.Collections.Generic.List<System.Data.DataTable>> _tableDictionary;  // 0x88
        private System.Collections.Hashtable _udSimpleTypes;  // 0x90
        private System.Collections.Hashtable _existingSimpleTypeMap;  // 0x98
        private System.Boolean _fromInference;  // 0xa0
        private static readonly System.Data.XSDSchema.NameType[] s_mapNameTypeXsd;  // static @ 0x0

        // Properties
        System.Boolean FromInference { get; /* RVA: 0x04D88020 */ set; /* RVA: 0x04D88040 */ }

        // Methods
        // RVA: 0x0AD9C050  token: 0x6000789
        private System.Void CollectElementsAnnotations(System.Xml.Schema.XmlSchema schema) { }
        // RVA: 0x0AD9B8E0  token: 0x600078A
        private System.Void CollectElementsAnnotations(System.Xml.Schema.XmlSchema schema, System.Collections.ArrayList schemaList) { }
        // RVA: 0x0ADAB0E0  token: 0x600078B
        private static System.String QualifiedName(System.String name) { }
        // RVA: 0x0ADAB5C8  token: 0x600078C
        private static System.Void SetProperties(System.Object instance, System.Xml.XmlAttribute[] attrs) { }
        // RVA: 0x0ADAB30C  token: 0x600078D
        private static System.Void SetExtProperties(System.Object instance, System.Xml.XmlAttribute[] attrs) { }
        // RVA: 0x0AD9E744  token: 0x600078E
        private System.Void HandleColumnExpression(System.Object instance, System.Xml.XmlAttribute[] attrs) { }
        // RVA: 0x0AD9CCA4  token: 0x600078F
        private static System.String GetMsdataAttribute(System.Xml.Schema.XmlSchemaAnnotated node, System.String ln) { }
        // RVA: 0x0ADAB148  token: 0x6000790
        private static System.Void SetExtProperties(System.Object instance, System.Xml.XmlAttributeCollection attrs) { }
        // RVA: 0x0ADA3188  token: 0x6000791
        private System.Void HandleRefTableProperties(System.Collections.ArrayList RefTables, System.Xml.Schema.XmlSchemaElement element) { }
        // RVA: 0x0ADA326C  token: 0x6000792
        private System.Void HandleRelation(System.Xml.XmlElement node, System.Boolean fNested) { }
        // RVA: 0x0ADA4E60  token: 0x6000793
        private System.Boolean HasAttributes(System.Xml.Schema.XmlSchemaObjectCollection attributes) { }
        // RVA: 0x0ADA7970  token: 0x6000794
        private System.Boolean IsDatasetParticle(System.Xml.Schema.XmlSchemaParticle pt) { }
        // RVA: 0x0AD9C0D4  token: 0x6000795
        private System.Int32 DatasetElementCount(System.Xml.Schema.XmlSchemaObjectCollection elements) { }
        // RVA: 0x0AD9C264  token: 0x6000796
        private System.Xml.Schema.XmlSchemaElement FindDatasetElement(System.Xml.Schema.XmlSchemaObjectCollection elements) { }
        // RVA: 0x0ADAAF78  token: 0x6000797
        public System.Void LoadSchema(System.Xml.Schema.XmlSchemaSet schemaSet, System.Data.DataTable dt) { }
        // RVA: 0x0ADA8098  token: 0x6000798
        public System.Void LoadSchema(System.Xml.Schema.XmlSchemaSet schemaSet, System.Data.DataSet ds) { }
        // RVA: 0x0ADA3A80  token: 0x6000799
        private System.Void HandleRelations(System.Xml.Schema.XmlSchemaAnnotation ann, System.Boolean fNested) { }
        // RVA: 0x0AD9D004  token: 0x600079A
        private System.Xml.Schema.XmlSchemaObjectCollection GetParticleItems(System.Xml.Schema.XmlSchemaParticle pt) { }
        // RVA: 0x0ADA286C  token: 0x600079B
        private System.Void HandleParticle(System.Xml.Schema.XmlSchemaParticle pt, System.Data.DataTable table, System.Collections.ArrayList tableChildren, System.Boolean isBase) { }
        // RVA: 0x0AD9E530  token: 0x600079C
        private System.Void HandleAttributes(System.Xml.Schema.XmlSchemaObjectCollection attributes, System.Data.DataTable table, System.Boolean isBase) { }
        // RVA: 0x0AD9E24C  token: 0x600079D
        private System.Void HandleAttributeGroup(System.Xml.Schema.XmlSchemaAttributeGroup attributeGroup, System.Data.DataTable table, System.Boolean isBase) { }
        // RVA: 0x0AD9E904  token: 0x600079E
        private System.Void HandleComplexType(System.Xml.Schema.XmlSchemaComplexType ct, System.Data.DataTable table, System.Collections.ArrayList tableChildren, System.Boolean isNillable) { }
        // RVA: 0x0AD9D1E0  token: 0x600079F
        private System.Xml.Schema.XmlSchemaParticle GetParticle(System.Xml.Schema.XmlSchemaComplexType ct) { }
        // RVA: 0x0AD9C60C  token: 0x60007A0
        private System.Data.DataColumn FindField(System.Data.DataTable table, System.String field) { }
        // RVA: 0x0AD9B6B4  token: 0x60007A1
        private System.Data.DataColumn[] BuildKey(System.Xml.Schema.XmlSchemaIdentityConstraint keyNode, System.Data.DataTable table) { }
        // RVA: 0x0AD9CAA8  token: 0x60007A2
        private System.Boolean GetBooleanAttribute(System.Xml.Schema.XmlSchemaAnnotated element, System.String attrName, System.Boolean defVal) { }
        // RVA: 0x0AD9D548  token: 0x60007A3
        private System.String GetStringAttribute(System.Xml.Schema.XmlSchemaAnnotated element, System.String attrName, System.String defVal) { }
        // RVA: 0x0ADABA04  token: 0x60007A4
        private static System.Data.AcceptRejectRule TranslateAcceptRejectRule(System.String strRule) { }
        // RVA: 0x0ADABA6C  token: 0x60007A5
        private static System.Data.Rule TranslateRule(System.String strRule) { }
        // RVA: 0x0ADA20B0  token: 0x60007A6
        private System.Void HandleKeyref(System.Xml.Schema.XmlSchemaKeyref keyref) { }
        // RVA: 0x0AD9EED8  token: 0x60007A7
        private System.Void HandleConstraint(System.Xml.Schema.XmlSchemaIdentityConstraint keyNode) { }
        // RVA: 0x0ADA4FF4  token: 0x60007A8
        private System.Data.DataTable InstantiateSimpleTable(System.Xml.Schema.XmlSchemaElement node) { }
        // RVA: 0x0AD9CBD8  token: 0x60007A9
        private System.String GetInstanceName(System.Xml.Schema.XmlSchemaAnnotated node) { }
        // RVA: 0x0ADA5750  token: 0x60007AA
        private System.Data.DataTable InstantiateTable(System.Xml.Schema.XmlSchemaElement node, System.Xml.Schema.XmlSchemaComplexType typeNode, System.Boolean isRef) { }
        // RVA: 0x0ADABB2C  token: 0x60007AB
        public static System.Type XsdtoClr(System.String xsdTypeName) { }
        // RVA: 0x0AD9C78C  token: 0x60007AC
        private static System.Data.XSDSchema.NameType FindNameType(System.String name) { }
        // RVA: 0x0ADAAFA4  token: 0x60007AD
        private System.Type ParseDataType(System.String dt) { }
        // RVA: 0x0ADA8040  token: 0x60007AE
        private static System.Boolean IsXsdType(System.String name) { }
        // RVA: 0x0AD9C84C  token: 0x60007AF
        private System.Xml.Schema.XmlSchemaAnnotated FindTypeNode(System.Xml.Schema.XmlSchemaAnnotated node) { }
        // RVA: 0x0ADA43D8  token: 0x60007B0
        private System.Void HandleSimpleTypeSimpleContentColumn(System.Xml.Schema.XmlSchemaSimpleType typeNode, System.String strType, System.Data.DataTable table, System.Boolean isBase, System.Xml.XmlAttribute[] attrs, System.Boolean isNillable) { }
        // RVA: 0x0ADA3CB8  token: 0x60007B1
        private System.Void HandleSimpleContentColumn(System.String strType, System.Data.DataTable table, System.Boolean isBase, System.Xml.XmlAttribute[] attrs, System.Boolean isNillable) { }
        // RVA: 0x0AD9D844  token: 0x60007B2
        private System.Void HandleAttributeColumn(System.Xml.Schema.XmlSchemaAttribute attrib, System.Data.DataTable table, System.Boolean isBase) { }
        // RVA: 0x0ADA13C8  token: 0x60007B3
        private System.Void HandleElementColumn(System.Xml.Schema.XmlSchemaElement elem, System.Data.DataTable table, System.Boolean isBase) { }
        // RVA: 0x0AD9F324  token: 0x60007B4
        private System.Void HandleDataSet(System.Xml.Schema.XmlSchemaElement node, System.Boolean isNewDataSet) { }
        // RVA: 0x0AD9B4E8  token: 0x60007B5
        private System.Void AddTablesToList(System.Collections.Generic.List<System.Data.DataTable> tableList, System.Data.DataTable dt) { }
        // RVA: 0x0AD9D2EC  token: 0x60007B6
        private System.String GetPrefix(System.String ns) { }
        // RVA: 0x0AD9CD9C  token: 0x60007B7
        private System.String GetNamespaceFromPrefix(System.String prefix) { }
        // RVA: 0x0AD9D6C4  token: 0x60007B8
        private System.String GetTableNamespace(System.Xml.Schema.XmlSchemaIdentityConstraint key) { }
        // RVA: 0x0AD9D5B8  token: 0x60007B9
        private System.String GetTableName(System.Xml.Schema.XmlSchemaIdentityConstraint key) { }
        // RVA: 0x0ADA7DE0  token: 0x60007BA
        private System.Boolean IsTable(System.Xml.Schema.XmlSchemaElement node) { }
        // RVA: 0x0ADA4D0C  token: 0x60007BB
        private System.Data.DataTable HandleTable(System.Xml.Schema.XmlSchemaElement node) { }
        // RVA: 0x041E1670  token: 0x60007BC
        public System.Void .ctor() { }
        // RVA: 0x0ADABBF8  token: 0x60007BD
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000098  // size: 0x38
    public sealed class XmlIgnoreNamespaceReader : System.Xml.XmlNodeReader
    {
        // Fields
        private System.Collections.Generic.List<System.String> _namespacesToIgnore;  // 0x30

        // Methods
        // RVA: 0x0ADB1674  token: 0x60007C0
        private System.Void .ctor(System.Xml.XmlDocument xdoc, System.String[] namespacesToIgnore) { }
        // RVA: 0x0ADB149C  token: 0x60007C1
        public virtual System.Boolean MoveToFirstAttribute() { }
        // RVA: 0x0ADB1584  token: 0x60007C2
        public virtual System.Boolean MoveToNextAttribute() { }

    }

    // TypeToken: 0x2000099  // size: 0xC8
    public sealed class XmlDataLoader
    {
        // Fields
        private System.Data.DataSet _dataSet;  // 0x10
        private System.Data.XmlToDatasetMap _nodeToSchemaMap;  // 0x18
        private System.Collections.Hashtable _nodeToRowMap;  // 0x20
        private System.Collections.Stack _childRowsStack;  // 0x28
        private System.Collections.Hashtable _htableExcludedNS;  // 0x30
        private System.Boolean _fIsXdr;  // 0x38
        private System.Boolean _isDiffgram;  // 0x39
        private System.Xml.XmlElement _topMostNode;  // 0x40
        private System.Boolean _ignoreSchema;  // 0x48
        private System.Data.DataTable _dataTable;  // 0x50
        private System.Boolean _isTableLevel;  // 0x58
        private System.Boolean _fromInference;  // 0x59
        private System.Xml.XmlReader _dataReader;  // 0x60
        private System.Object _XSD_XMLNS_NS;  // 0x68
        private System.Object _XDR_SCHEMA;  // 0x70
        private System.Object _XDRNS;  // 0x78
        private System.Object _SQL_SYNC;  // 0x80
        private System.Object _UPDGNS;  // 0x88
        private System.Object _XSD_SCHEMA;  // 0x90
        private System.Object _XSDNS;  // 0x98
        private System.Object _DFFNS;  // 0xa0
        private System.Object _MSDNS;  // 0xa8
        private System.Object _DIFFID;  // 0xb0
        private System.Object _HASCHANGES;  // 0xb8
        private System.Object _ROWORDER;  // 0xc0

        // Properties
        System.Boolean FromInference { get; /* RVA: 0x04D8EF10 */ set; /* RVA: 0x04D8EF20 */ }

        // Methods
        // RVA: 0x0ADB1178  token: 0x60007C3
        private System.Void .ctor(System.Data.DataSet dataset, System.Boolean IsXdr, System.Boolean ignoreSchema) { }
        // RVA: 0x0ADB13B4  token: 0x60007C4
        private System.Void .ctor(System.Data.DataSet dataset, System.Boolean IsXdr, System.Xml.XmlElement topNode, System.Boolean ignoreSchema) { }
        // RVA: 0x0ADB1210  token: 0x60007C5
        private System.Void .ctor(System.Data.DataTable datatable, System.Boolean IsXdr, System.Boolean ignoreSchema) { }
        // RVA: 0x0ADB12BC  token: 0x60007C6
        private System.Void .ctor(System.Data.DataTable datatable, System.Boolean IsXdr, System.Xml.XmlElement topNode, System.Boolean ignoreSchema) { }
        // RVA: 0x0ADACCFC  token: 0x60007C9
        private System.Void AttachRows(System.Data.DataRow parentRow, System.Xml.XmlNode parentElement) { }
        // RVA: 0x0ADACE24  token: 0x60007CA
        private System.Int32 CountNonNSAttributes(System.Xml.XmlNode node) { }
        // RVA: 0x0ADAD474  token: 0x60007CB
        private System.String GetValueForTextOnlyColums(System.Xml.XmlNode n) { }
        // RVA: 0x0ADAD17C  token: 0x60007CC
        private System.String GetInitialTextFromNodes(System.Xml.XmlNode& n) { }
        // RVA: 0x0ADAD3DC  token: 0x60007CD
        private System.Data.DataColumn GetTextOnlyColumn(System.Data.DataRow row) { }
        // RVA: 0x0ADAD378  token: 0x60007CE
        private System.Data.DataRow GetRowFromElement(System.Xml.XmlElement e) { }
        // RVA: 0x0ADACEF0  token: 0x60007CF
        private System.Boolean FColumnElement(System.Xml.XmlElement e) { }
        // RVA: 0x0ADACFBC  token: 0x60007D0
        private System.Boolean FExcludedNamespace(System.String ns) { }
        // RVA: 0x0ADAD0B0  token: 0x60007D1
        private System.Boolean FIgnoreNamespace(System.Xml.XmlNode node) { }
        // RVA: 0x0ADAD034  token: 0x60007D2
        private System.Boolean FIgnoreNamespace(System.Xml.XmlReader node) { }
        // RVA: 0x0ADAD8B0  token: 0x60007D3
        private System.Boolean IsTextLikeNode(System.Xml.XmlNodeType n) { }
        // RVA: 0x0ADAD8F8  token: 0x60007D4
        private System.Boolean IsTextOnly(System.Data.DataColumn c) { }
        // RVA: 0x0ADAE154  token: 0x60007D5
        private System.Void LoadData(System.Xml.XmlDocument xdoc) { }
        // RVA: 0x0ADAE748  token: 0x60007D6
        private System.Void LoadRowData(System.Data.DataRow row, System.Xml.XmlElement rowElement) { }
        // RVA: 0x0ADAF0C0  token: 0x60007D7
        private System.Void LoadRows(System.Data.DataRow parentRow, System.Xml.XmlNode parentElement) { }
        // RVA: 0x0ADB112C  token: 0x60007D8
        private System.Void SetRowValueFromXmlText(System.Data.DataRow row, System.Data.DataColumn col, System.String xmlText) { }
        // RVA: 0x0ADAD61C  token: 0x60007D9
        private System.Void InitNameTable() { }
        // RVA: 0x0ADAE3F4  token: 0x60007DA
        private System.Void LoadData(System.Xml.XmlReader reader) { }
        // RVA: 0x0ADAFE8C  token: 0x60007DB
        private System.Void LoadTopMostTable(System.Data.DataTable table) { }
        // RVA: 0x0ADAF390  token: 0x60007DC
        private System.Void LoadTable(System.Data.DataTable table, System.Boolean isNested) { }
        // RVA: 0x0ADAD918  token: 0x60007DD
        private System.Void LoadColumn(System.Data.DataColumn column, System.Object[] foundColumns) { }
        // RVA: 0x0ADB0F38  token: 0x60007DE
        private System.Boolean ProcessXsdSchema() { }

    }

    // TypeToken: 0x200009A  // size: 0x14
    public sealed struct XmlReadMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Data.XmlReadMode Auto;  // const
        public static System.Data.XmlReadMode ReadSchema;  // const
        public static System.Data.XmlReadMode IgnoreSchema;  // const
        public static System.Data.XmlReadMode InferSchema;  // const
        public static System.Data.XmlReadMode DiffGram;  // const
        public static System.Data.XmlReadMode Fragment;  // const
        public static System.Data.XmlReadMode InferTypedSchema;  // const

    }

    // TypeToken: 0x200009B  // size: 0x20
    public sealed class XmlToDatasetMap
    {
        // Fields
        private System.Data.XmlToDatasetMap.XmlNodeIdHashtable _tableSchemaMap;  // 0x10
        private System.Data.XmlToDatasetMap.TableSchemaInfo _lastTableSchemaInfo;  // 0x18

        // Methods
        // RVA: 0x0ADBA288  token: 0x60007DF
        public System.Void .ctor(System.Data.DataSet dataSet, System.Xml.XmlNameTable nameTable) { }
        // RVA: 0x0ADBA278  token: 0x60007E0
        public System.Void .ctor(System.Xml.XmlNameTable nameTable, System.Data.DataSet dataSet) { }
        // RVA: 0x0ADBA290  token: 0x60007E1
        public System.Void .ctor(System.Data.DataTable dataTable, System.Xml.XmlNameTable nameTable) { }
        // RVA: 0x0ADBA280  token: 0x60007E2
        public System.Void .ctor(System.Xml.XmlNameTable nameTable, System.Data.DataTable dataTable) { }
        // RVA: 0x0ADBA258  token: 0x60007E3
        private static System.Boolean IsMappedColumn(System.Data.DataColumn c) { }
        // RVA: 0x0ADB845C  token: 0x60007E4
        private System.Data.XmlToDatasetMap.TableSchemaInfo AddTableSchema(System.Data.DataTable table, System.Xml.XmlNameTable nameTable) { }
        // RVA: 0x0ADB8588  token: 0x60007E5
        private System.Data.XmlToDatasetMap.TableSchemaInfo AddTableSchema(System.Xml.XmlNameTable nameTable, System.Data.DataTable table) { }
        // RVA: 0x0ADB8150  token: 0x60007E6
        private System.Boolean AddColumnSchema(System.Data.DataColumn col, System.Xml.XmlNameTable nameTable, System.Data.XmlToDatasetMap.XmlNodeIdHashtable columns) { }
        // RVA: 0x0ADB8298  token: 0x60007E7
        private System.Boolean AddColumnSchema(System.Xml.XmlNameTable nameTable, System.Data.DataColumn col, System.Data.XmlToDatasetMap.XmlNodeIdHashtable columns) { }
        // RVA: 0x0ADB8714  token: 0x60007E8
        private System.Void BuildIdentityMap(System.Data.DataSet dataSet, System.Xml.XmlNameTable nameTable) { }
        // RVA: 0x0ADB8C98  token: 0x60007E9
        private System.Void BuildIdentityMap(System.Xml.XmlNameTable nameTable, System.Data.DataSet dataSet) { }
        // RVA: 0x0ADB8A88  token: 0x60007EA
        private System.Void BuildIdentityMap(System.Data.DataTable dataTable, System.Xml.XmlNameTable nameTable) { }
        // RVA: 0x0ADB9348  token: 0x60007EB
        private System.Void BuildIdentityMap(System.Xml.XmlNameTable nameTable, System.Data.DataTable dataTable) { }
        // RVA: 0x0ADB9D78  token: 0x60007EC
        private System.Collections.ArrayList GetSelfAndDescendants(System.Data.DataTable dt) { }
        // RVA: 0x0ADB9998  token: 0x60007ED
        public System.Object GetColumnSchema(System.Xml.XmlNode node, System.Boolean fIgnoreNamespace) { }
        // RVA: 0x0ADB9B3C  token: 0x60007EE
        public System.Object GetColumnSchema(System.Data.DataTable table, System.Xml.XmlReader dataReader, System.Boolean fIgnoreNamespace) { }
        // RVA: 0x0ADB9C9C  token: 0x60007EF
        public System.Object GetSchemaForNode(System.Xml.XmlNode node, System.Boolean fIgnoreNamespace) { }
        // RVA: 0x0ADBA024  token: 0x60007F0
        public System.Data.DataTable GetTableForNode(System.Xml.XmlReader node, System.Boolean fIgnoreNamespace) { }
        // RVA: 0x0ADBA0F8  token: 0x60007F1
        private System.Void HandleSpecialColumn(System.Data.DataColumn col, System.Xml.XmlNameTable nameTable, System.Data.XmlToDatasetMap.XmlNodeIdHashtable columns) { }

    }

    // TypeToken: 0x200009F  // size: 0x14
    public sealed struct XmlWriteMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Data.XmlWriteMode WriteSchema;  // const
        public static System.Data.XmlWriteMode IgnoreSchema;  // const
        public static System.Data.XmlWriteMode DiffGram;  // const

    }

    // TypeToken: 0x20000A0  // size: 0x14
    public sealed struct SchemaFormat
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Data.SchemaFormat Public;  // const
        public static System.Data.SchemaFormat Remoting;  // const
        public static System.Data.SchemaFormat WebService;  // const
        public static System.Data.SchemaFormat RemotingSkipSchema;  // const
        public static System.Data.SchemaFormat WebServiceSkipSchema;  // const

    }

    // TypeToken: 0x20000A1  // size: 0x90
    public sealed class XmlTreeGen
    {
        // Fields
        private System.Collections.ArrayList _constraintNames;  // 0x10
        private System.Collections.Hashtable _namespaces;  // 0x18
        private System.Collections.Hashtable _autogenerated;  // 0x20
        private System.Collections.Hashtable _prefixes;  // 0x28
        private System.Data.DataSet _ds;  // 0x30
        private System.Collections.ArrayList _tables;  // 0x38
        private System.Collections.ArrayList _relations;  // 0x40
        private System.Xml.XmlDocument _dc;  // 0x48
        private System.Xml.XmlElement _sRoot;  // 0x50
        private System.Int32 _prefixCount;  // 0x58
        private System.Data.SchemaFormat _schFormat;  // 0x5c
        private System.String _filePath;  // 0x60
        private System.String _fileName;  // 0x68
        private System.String _fileExt;  // 0x70
        private System.Xml.XmlElement _dsElement;  // 0x78
        private System.Xml.XmlElement _constraintSeparator;  // 0x80
        private System.Converter<System.Type,System.String> _targetConverter;  // 0x88

        // Methods
        // RVA: 0x0ADC6E94  token: 0x60007FB
        private System.Void .ctor(System.Data.SchemaFormat format) { }
        // RVA: 0x0ADBA7F0  token: 0x60007FC
        private static System.Void AddExtendedProperties(System.Data.PropertyCollection props, System.Xml.XmlElement node) { }
        // RVA: 0x0ADBA7FC  token: 0x60007FD
        private static System.Void AddExtendedProperties(System.Data.PropertyCollection props, System.Xml.XmlElement node, System.Type type) { }
        // RVA: 0x0ADBADBC  token: 0x60007FE
        private System.Void AddXdoProperties(System.Object instance, System.Xml.XmlElement root, System.Xml.XmlDocument xd) { }
        // RVA: 0x0ADBAEF8  token: 0x60007FF
        private System.Void AddXdoProperty(System.ComponentModel.PropertyDescriptor pd, System.Object instance, System.Xml.XmlElement root, System.Xml.XmlDocument xd) { }
        // RVA: 0x0ADC6514  token: 0x6000800
        private static System.String XmlDataTypeName(System.Type type) { }
        // RVA: 0x0ADBD2A0  token: 0x6000801
        private System.Void GenerateConstraintNames(System.Data.DataTable table, System.Boolean fromTable) { }
        // RVA: 0x0ADBCF90  token: 0x6000802
        private System.Void GenerateConstraintNames(System.Collections.ArrayList tables) { }
        // RVA: 0x0ADBD024  token: 0x6000803
        private System.Void GenerateConstraintNames(System.Data.DataSet ds) { }
        // RVA: 0x0ADC6E70  token: 0x6000804
        private static System.Boolean _PropsNotEmpty(System.Data.PropertyCollection props) { }
        // RVA: 0x0ADC109C  token: 0x6000805
        private System.Boolean HaveExtendedProperties(System.Data.DataSet ds) { }
        // RVA: 0x0ADC633C  token: 0x6000806
        private System.Void WriteSchemaRoot(System.Xml.XmlDocument xd, System.Xml.XmlElement rootSchema, System.String targetNamespace) { }
        // RVA: 0x0ADC62D0  token: 0x6000807
        private static System.Void ValidateColumnMapping(System.Type columnType) { }
        // RVA: 0x0ADC5558  token: 0x6000808
        private System.Void SetupAutoGenerated(System.Data.DataSet ds) { }
        // RVA: 0x0ADC615C  token: 0x6000809
        private System.Void SetupAutoGenerated(System.Collections.ArrayList dt) { }
        // RVA: 0x0ADC57D0  token: 0x600080A
        private System.Void SetupAutoGenerated(System.Data.DataTable dt) { }
        // RVA: 0x0ADBC150  token: 0x600080B
        private System.Void CreateTablesHierarchy(System.Data.DataTable dt) { }
        // RVA: 0x0ADBBE40  token: 0x600080C
        private System.Void CreateRelations(System.Data.DataTable dt) { }
        // RVA: 0x0ADBC47C  token: 0x600080D
        private System.Data.DataTable[] CreateToplevelTables() { }
        // RVA: 0x0ADC190C  token: 0x600080E
        private System.Void SchemaTree(System.Xml.XmlDocument xd, System.Xml.XmlWriter xmlWriter, System.Data.DataSet ds, System.Data.DataTable dt, System.Boolean writeHierarchy) { }
        // RVA: 0x0ADC1484  token: 0x600080F
        private System.Xml.XmlElement SchemaTree(System.Xml.XmlDocument xd, System.Data.DataTable dt) { }
        // RVA: 0x0ADBC6A4  token: 0x6000810
        private System.Xml.XmlElement FillDataSetElement(System.Xml.XmlDocument xd, System.Data.DataSet ds, System.Data.DataTable dt) { }
        // RVA: 0x0ADC53F0  token: 0x6000811
        private System.Void SetPath(System.Xml.XmlWriter xw) { }
        // RVA: 0x0ADC13B0  token: 0x6000812
        private System.Void Save(System.Data.DataSet ds, System.Xml.XmlWriter xw) { }
        // RVA: 0x0ADC13D0  token: 0x6000813
        private System.Void Save(System.Data.DataTable dt, System.Xml.XmlWriter xw) { }
        // RVA: 0x0ADC1240  token: 0x6000814
        private System.Void Save(System.Data.DataSet ds, System.Data.DataTable dt, System.Xml.XmlWriter xw) { }
        // RVA: 0x0ADC121C  token: 0x6000815
        private System.Void Save(System.Data.DataSet ds, System.Data.DataTable dt, System.Xml.XmlWriter xw, System.Boolean writeHierarchy) { }
        // RVA: 0x0ADC125C  token: 0x6000816
        private System.Void Save(System.Data.DataSet ds, System.Data.DataTable dt, System.Xml.XmlWriter xw, System.Boolean writeHierarchy, System.Converter<System.Type,System.String> multipleTargetConverter) { }
        // RVA: 0x0ADBE5CC  token: 0x6000817
        private System.Xml.XmlElement HandleRelation(System.Data.DataRelation rel, System.Xml.XmlDocument dc) { }
        // RVA: 0x0ADBCC28  token: 0x6000818
        private static System.Xml.XmlElement FindSimpleType(System.Xml.XmlElement schema, System.String name) { }
        // RVA: 0x0ADBD760  token: 0x6000819
        private System.Xml.XmlElement GetSchema(System.String NamespaceURI) { }
        // RVA: 0x0ADBD978  token: 0x600081A
        private System.Void HandleColumnType(System.Data.DataColumn col, System.Xml.XmlDocument dc, System.Xml.XmlElement root, System.Xml.XmlElement schema) { }
        // RVA: 0x0ADBA298  token: 0x600081B
        private System.Void AddColumnProperties(System.Data.DataColumn col, System.Xml.XmlElement root) { }
        // RVA: 0x0ADBCCFC  token: 0x600081C
        private System.String FindTargetNamespace(System.Data.DataTable table) { }
        // RVA: 0x0ADBDD50  token: 0x600081D
        private System.Xml.XmlElement HandleColumn(System.Data.DataColumn col, System.Xml.XmlDocument dc, System.Xml.XmlElement schema, System.Boolean fWriteOrdinal) { }
        // RVA: 0x0ADC61EC  token: 0x600081E
        private static System.String TranslateAcceptRejectRule(System.Data.AcceptRejectRule rule) { }
        // RVA: 0x0ADC6248  token: 0x600081F
        private static System.String TranslateRule(System.Data.Rule rule) { }
        // RVA: 0x0ADBB640  token: 0x6000820
        private System.Void AppendChildWithoutRef(System.Xml.XmlElement node, System.String Namespace, System.Xml.XmlElement el, System.String refString) { }
        // RVA: 0x0ADBCE00  token: 0x6000821
        private System.Xml.XmlElement FindTypeNode(System.Xml.XmlElement node, System.String strType) { }
        // RVA: 0x0ADC0D98  token: 0x6000822
        private System.Xml.XmlElement HandleTable(System.Data.DataTable table, System.Xml.XmlDocument dc, System.Xml.XmlElement schema) { }
        // RVA: 0x0ADC0DB4  token: 0x6000823
        private System.Boolean HasMixedColumns(System.Data.DataTable table) { }
        // RVA: 0x0ADBB97C  token: 0x6000824
        private static System.Boolean AutoGenerated(System.Data.DataColumn col) { }
        // RVA: 0x0ADBB8A8  token: 0x6000825
        private static System.Boolean AutoGenerated(System.Data.DataRelation rel) { }
        // RVA: 0x0ADBB6DC  token: 0x6000826
        private static System.Boolean AutoGenerated(System.Data.UniqueConstraint unique) { }
        // RVA: 0x0ADBB788  token: 0x6000827
        private System.Boolean AutoGenerated(System.Data.ForeignKeyConstraint fk) { }
        // RVA: 0x0ADBB798  token: 0x6000828
        private static System.Boolean AutoGenerated(System.Data.ForeignKeyConstraint fk, System.Boolean checkRelation) { }
        // RVA: 0x0ADC11E4  token: 0x6000829
        private System.Boolean IsAutoGenerated(System.Object o) { }
        // RVA: 0x0ADBEB2C  token: 0x600082A
        private System.Xml.XmlElement HandleTable(System.Data.DataTable table, System.Xml.XmlDocument dc, System.Xml.XmlElement schema, System.Boolean genNested) { }
        // RVA: 0x0ADC524C  token: 0x600082B
        private System.Void SetMSDataAttribute(System.Xml.XmlElement root, System.Type type) { }

    }

    // TypeToken: 0x20000A2  // size: 0x50
    public sealed class NewDiffgramGen
    {
        // Fields
        private System.Xml.XmlDocument _doc;  // 0x10
        private System.Data.DataSet _ds;  // 0x18
        private System.Data.DataTable _dt;  // 0x20
        private System.Xml.XmlWriter _xmlw;  // 0x28
        private System.Boolean _fBefore;  // 0x30
        private System.Boolean _fErrors;  // 0x31
        private System.Collections.Hashtable _rowsOrder;  // 0x38
        private System.Collections.ArrayList _tables;  // 0x40
        private System.Boolean _writeHierarchy;  // 0x48

        // Methods
        // RVA: 0x0ADB3F4C  token: 0x600082C
        private System.Void .ctor(System.Data.DataSet ds) { }
        // RVA: 0x0ADB3E3C  token: 0x600082D
        private System.Void .ctor(System.Data.DataTable dt, System.Boolean writeHierarchy) { }
        // RVA: 0x0ADB210C  token: 0x600082E
        private System.Void CreateTableHierarchy(System.Data.DataTable dt) { }
        // RVA: 0x0ADB2438  token: 0x600082F
        private System.Void DoAssignments(System.Collections.ArrayList tables) { }
        // RVA: 0x0ADB2614  token: 0x6000830
        private System.Boolean EmptyData() { }
        // RVA: 0x0ADB3E30  token: 0x6000831
        private System.Void Save(System.Xml.XmlWriter xmlw) { }
        // RVA: 0x0ADB3AB0  token: 0x6000832
        private System.Void Save(System.Xml.XmlWriter xmlw, System.Data.DataTable table) { }
        // RVA: 0x0ADB39DC  token: 0x6000833
        private System.Void GenerateTable(System.Data.DataTable table) { }
        // RVA: 0x0ADB348C  token: 0x6000834
        private System.Void GenerateTableErrors(System.Data.DataTable table) { }
        // RVA: 0x0ADB2EE0  token: 0x6000835
        private System.Void GenerateRow(System.Data.DataRow row) { }
        // RVA: 0x0ADB26D0  token: 0x6000836
        private System.Void GenerateColumn(System.Data.DataRow row, System.Data.DataColumn col, System.Data.DataRowVersion version) { }
        // RVA: 0x0ADB3A58  token: 0x6000837
        private static System.String QualifiedName(System.String prefix, System.String name) { }

    }

    // TypeToken: 0x20000A3  // size: 0x50
    public sealed class XmlDataTreeWriter
    {
        // Fields
        private System.Xml.XmlWriter _xmlw;  // 0x10
        private System.Data.DataSet _ds;  // 0x18
        private System.Data.DataTable _dt;  // 0x20
        private System.Collections.ArrayList _dTables;  // 0x28
        private System.Data.DataTable[] _topLevelTables;  // 0x30
        private System.Boolean _fFromTable;  // 0x38
        private System.Boolean _isDiffgram;  // 0x39
        private System.Collections.Hashtable _rowsOrder;  // 0x40
        private System.Boolean _writeHierarchy;  // 0x48

        // Methods
        // RVA: 0x0ADB7AD4  token: 0x6000838
        private System.Void .ctor(System.Data.DataSet ds) { }
        // RVA: 0x0ADB7964  token: 0x6000839
        private System.Void .ctor(System.Data.DataTable dt, System.Boolean writeHierarchy) { }
        // RVA: 0x0ADB4448  token: 0x600083A
        private System.Data.DataTable[] CreateToplevelTables() { }
        // RVA: 0x0ADB411C  token: 0x600083B
        private System.Void CreateTablesHierarchy(System.Data.DataTable dt) { }
        // RVA: 0x0ADB4A18  token: 0x600083C
        private static System.Boolean RowHasErrors(System.Data.DataRow row) { }
        // RVA: 0x0ADB4ADC  token: 0x600083D
        private System.Void SaveDiffgramData(System.Xml.XmlWriter xw, System.Collections.Hashtable rowsOrder) { }
        // RVA: 0x0ADB514C  token: 0x600083E
        private System.Void Save(System.Xml.XmlWriter xw, System.Boolean writeSchema) { }
        // RVA: 0x0ADB4670  token: 0x600083F
        private System.Collections.ArrayList GetNestedChildRelations(System.Data.DataRow row) { }
        // RVA: 0x0ADB5A7C  token: 0x6000840
        private System.Void XmlDataRowWriter(System.Data.DataRow row, System.String encodedTableName) { }
        // RVA: 0x0ADB4990  token: 0x6000841
        private static System.Boolean PreserveSpace(System.Object value) { }

    }

    // TypeToken: 0x20000A4  // size: 0x20
    public sealed class DataTextWriter : System.Xml.XmlWriter
    {
        // Fields
        private System.Xml.XmlWriter _xmltextWriter;  // 0x18

        // Properties
        System.IO.Stream BaseStream { get; /* RVA: 0x0ADB2098 */ }
        System.Xml.WriteState WriteState { get; /* RVA: 0x0ADB20E8 */ }

        // Methods
        // RVA: 0x0ADB1C88  token: 0x6000842
        private static System.Xml.XmlWriter CreateWriter(System.Xml.XmlWriter xw) { }
        // RVA: 0x05392C40  token: 0x6000843
        private System.Void .ctor(System.Xml.XmlWriter w) { }
        // RVA: 0x0ADB1FAC  token: 0x6000845
        public virtual System.Void WriteStartDocument() { }
        // RVA: 0x0ADB1FD0  token: 0x6000846
        public virtual System.Void WriteStartDocument(System.Boolean standalone) { }
        // RVA: 0x0A8245D8  token: 0x6000847
        public virtual System.Void WriteEndDocument() { }
        // RVA: 0x0ADB1E30  token: 0x6000848
        public virtual System.Void WriteDocType(System.String name, System.String pubid, System.String sysid, System.String subset) { }
        // RVA: 0x0ADB1FF8  token: 0x6000849
        public virtual System.Void WriteStartElement(System.String prefix, System.String localName, System.String ns) { }
        // RVA: 0x0ADB1E90  token: 0x600084A
        public virtual System.Void WriteEndElement() { }
        // RVA: 0x0ADB1ED8  token: 0x600084B
        public virtual System.Void WriteFullEndElement() { }
        // RVA: 0x0ADB1F7C  token: 0x600084C
        public virtual System.Void WriteStartAttribute(System.String prefix, System.String localName, System.String ns) { }
        // RVA: 0x0ADB1E6C  token: 0x600084D
        public virtual System.Void WriteEndAttribute() { }
        // RVA: 0x0ADB1D90  token: 0x600084E
        public virtual System.Void WriteCData(System.String text) { }
        // RVA: 0x0ADB1E0C  token: 0x600084F
        public virtual System.Void WriteComment(System.String text) { }
        // RVA: 0x0ADB1EFC  token: 0x6000850
        public virtual System.Void WriteProcessingInstruction(System.String name, System.String text) { }
        // RVA: 0x0ADB1EB4  token: 0x6000851
        public virtual System.Void WriteEntityRef(System.String name) { }
        // RVA: 0x0ADB1DB4  token: 0x6000852
        public virtual System.Void WriteCharEntity(System.Char ch) { }
        // RVA: 0x0ADB2074  token: 0x6000853
        public virtual System.Void WriteWhitespace(System.String ws) { }
        // RVA: 0x0ADB2028  token: 0x6000854
        public virtual System.Void WriteString(System.String text) { }
        // RVA: 0x0ADB204C  token: 0x6000855
        public virtual System.Void WriteSurrogateCharEntity(System.Char lowChar, System.Char highChar) { }
        // RVA: 0x0ADB1DDC  token: 0x6000856
        public virtual System.Void WriteChars(System.Char[] buffer, System.Int32 index, System.Int32 count) { }
        // RVA: 0x0ADB1F28  token: 0x6000857
        public virtual System.Void WriteRaw(System.Char[] buffer, System.Int32 index, System.Int32 count) { }
        // RVA: 0x0ADB1F58  token: 0x6000858
        public virtual System.Void WriteRaw(System.String data) { }
        // RVA: 0x0ADB1D30  token: 0x6000859
        public virtual System.Void WriteBase64(System.Byte[] buffer, System.Int32 index, System.Int32 count) { }
        // RVA: 0x0ADB1D60  token: 0x600085A
        public virtual System.Void WriteBinHex(System.Byte[] buffer, System.Int32 index, System.Int32 count) { }
        // RVA: 0x0ADB1C64  token: 0x600085C
        public virtual System.Void Close() { }
        // RVA: 0x0ADB1CE8  token: 0x600085D
        public virtual System.Void Flush() { }
        // RVA: 0x0ADB1D0C  token: 0x600085E
        public virtual System.String LookupPrefix(System.String ns) { }

    }

    // TypeToken: 0x20000A5  // size: 0x18
    public sealed class DataTextReader : System.Xml.XmlReader
    {
        // Fields
        private System.Xml.XmlReader _xmlreader;  // 0x10

        // Properties
        System.Xml.XmlReaderSettings Settings { get; /* RVA: 0x0ADB1C00 */ }
        System.Xml.XmlNodeType NodeType { get; /* RVA: 0x0ADB1B70 */ }
        System.String Name { get; /* RVA: 0x0ADB1B4C */ }
        System.String LocalName { get; /* RVA: 0x0A99D9B8 */ }
        System.String NamespaceURI { get; /* RVA: 0x0A8E2A84 */ }
        System.String Prefix { get; /* RVA: 0x0ADB1B94 */ }
        System.Boolean HasValue { get; /* RVA: 0x0ADB1ABC */ }
        System.String Value { get; /* RVA: 0x0ADB1C1C */ }
        System.Int32 Depth { get; /* RVA: 0x0ADB1A74 */ }
        System.String BaseURI { get; /* RVA: 0x0ADB1A08 */ }
        System.Boolean IsEmptyElement { get; /* RVA: 0x0ADB1B04 */ }
        System.Boolean IsDefault { get; /* RVA: 0x0ADB1AE0 */ }
        System.Char QuoteChar { get; /* RVA: 0x0ADB1BB8 */ }
        System.Xml.XmlSpace XmlSpace { get; /* RVA: 0x0A7ABE30 */ }
        System.String XmlLang { get; /* RVA: 0x0ADB1C40 */ }
        System.Int32 AttributeCount { get; /* RVA: 0x0ADB19E4 */ }
        System.Boolean EOF { get; /* RVA: 0x0ADB1A98 */ }
        System.Xml.ReadState ReadState { get; /* RVA: 0x0ADB1BDC */ }
        System.Xml.XmlNameTable NameTable { get; /* RVA: 0x0ADB1B28 */ }
        System.Boolean CanResolveEntity { get; /* RVA: 0x0ADB1A50 */ }
        System.Boolean CanReadValueChunk { get; /* RVA: 0x0ADB1A2C */ }

        // Methods
        // RVA: 0x0ADB1730  token: 0x600085F
        private static System.Xml.XmlReader CreateReader(System.Xml.XmlReader xr) { }
        // RVA: 0x0ADB198C  token: 0x6000860
        private System.Void .ctor(System.Xml.XmlReader input) { }
        // RVA: 0x0ADB17B8  token: 0x6000871
        public virtual System.String GetAttribute(System.String name) { }
        // RVA: 0x0ADB1790  token: 0x6000872
        public virtual System.String GetAttribute(System.String localName, System.String namespaceURI) { }
        // RVA: 0x0ADB17DC  token: 0x6000873
        public virtual System.String GetAttribute(System.Int32 i) { }
        // RVA: 0x0ADB1824  token: 0x6000874
        public virtual System.Boolean MoveToAttribute(System.String name) { }
        // RVA: 0x0ADB1848  token: 0x6000875
        public virtual System.Void MoveToAttribute(System.Int32 i) { }
        // RVA: 0x0ADB1890  token: 0x6000876
        public virtual System.Boolean MoveToFirstAttribute() { }
        // RVA: 0x0ADB18B4  token: 0x6000877
        public virtual System.Boolean MoveToNextAttribute() { }
        // RVA: 0x0ADB186C  token: 0x6000878
        public virtual System.Boolean MoveToElement() { }
        // RVA: 0x0ADB18D8  token: 0x6000879
        public virtual System.Boolean ReadAttributeValue() { }
        // RVA: 0x0ADB1920  token: 0x600087A
        public virtual System.Boolean Read() { }
        // RVA: 0x0ADB170C  token: 0x600087C
        public virtual System.Void Close() { }
        // RVA: 0x0ADB1968  token: 0x600087E
        public virtual System.Void Skip() { }
        // RVA: 0x0ADB1800  token: 0x6000880
        public virtual System.String LookupNamespace(System.String prefix) { }
        // RVA: 0x0ADB1944  token: 0x6000882
        public virtual System.Void ResolveEntity() { }
        // RVA: 0x0ADB18FC  token: 0x6000884
        public virtual System.String ReadString() { }

    }

}

namespace System.Data.Common
{

    // TypeToken: 0x20000C0  // size: 0x60
    public sealed class ObjectStorage : System.Data.Common.DataStorage
    {
        // Fields
        private static readonly System.Object s_defaultValue;  // static @ 0x0
        private System.Object[] _values;  // 0x50
        private readonly System.Boolean _implementsIXmlSerializable;  // 0x58
        private static readonly System.Object s_tempAssemblyCacheLock;  // static @ 0x8
        private static System.Collections.Generic.Dictionary<System.Collections.Generic.KeyValuePair<System.Type,System.Xml.Serialization.XmlRootAttribute>,System.Xml.Serialization.XmlSerializer> s_tempAssemblyCache;  // static @ 0x10
        private static readonly System.Xml.Serialization.XmlSerializerFactory s_serializerFactory;  // static @ 0x18

        // Methods
        // RVA: 0x0ADEE4F8  token: 0x6000A8D
        private System.Void .ctor(System.Data.DataColumn column, System.Type type) { }
        // RVA: 0x0ADEB99C  token: 0x6000A8E
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0ADEBFE0  token: 0x6000A8F
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADEBB2C  token: 0x6000A90
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo1, System.Object value) { }
        // RVA: 0x0ADEB9D0  token: 0x6000A91
        private System.Int32 CompareTo(System.Object valueNo1, System.Object valueNo2) { }
        // RVA: 0x0ADEBC64  token: 0x6000A92
        private System.Int32 CompareWithFamilies(System.Object valueNo1, System.Object valueNo2) { }
        // RVA: 0x0ADED0AC  token: 0x6000A93
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADED9DC  token: 0x6000A94
        public virtual System.Object Get(System.Int32 recordNo) { }
        // RVA: 0x0ADED140  token: 0x6000A95
        private System.Data.Common.ObjectStorage.Families GetFamily(System.Type dataType) { }
        // RVA: 0x0ADEDA14  token: 0x6000A96
        public virtual System.Boolean IsNull(System.Int32 record) { }
        // RVA: 0x0ADEDC6C  token: 0x6000A97
        public virtual System.Void Set(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0ADEDA44  token: 0x6000A98
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0ADEC684  token: 0x6000A99
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0ADECA18  token: 0x6000A9A
        public virtual System.Object ConvertXmlToObject(System.Xml.XmlReader xmlReader, System.Xml.Serialization.XmlRootAttribute xmlAttrib) { }
        // RVA: 0x0ADEC228  token: 0x6000A9B
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0ADEC150  token: 0x6000A9C
        public virtual System.Void ConvertObjectToXml(System.Object value, System.Xml.XmlWriter xmlWriter, System.Xml.Serialization.XmlRootAttribute xmlAttrib) { }
        // RVA: 0x0ADED108  token: 0x6000A9D
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0ADECF08  token: 0x6000A9E
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0ADEDAFC  token: 0x6000A9F
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }
        // RVA: 0x0ADEE310  token: 0x6000AA0
        private static System.Void VerifyIDynamicMetaObjectProvider(System.Type type) { }
        // RVA: 0x0ADED250  token: 0x6000AA1
        private static System.Xml.Serialization.XmlSerializer GetXmlSerializer(System.Type type) { }
        // RVA: 0x0ADED2B8  token: 0x6000AA2
        private static System.Xml.Serialization.XmlSerializer GetXmlSerializer(System.Type type, System.Xml.Serialization.XmlRootAttribute attribute) { }
        // RVA: 0x0ADEE41C  token: 0x6000AA3
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000C3  // size: 0x10
    public static class ADP
    {
        // Fields
        private static readonly System.Type s_stackOverflowType;  // static @ 0x0
        private static readonly System.Type s_outOfMemoryType;  // static @ 0x8
        private static readonly System.Type s_threadAbortType;  // static @ 0x10
        private static readonly System.Type s_nullReferenceType;  // static @ 0x18
        private static readonly System.Type s_accessViolationType;  // static @ 0x20
        private static readonly System.Type s_securityType;  // static @ 0x28
        private static readonly System.String StrEmpty;  // static @ 0x30
        private static readonly System.String[] AzureSqlServerEndpoints;  // static @ 0x38
        private static readonly System.IntPtr PtrZero;  // static @ 0x40
        private static readonly System.Int32 PtrSize;  // static @ 0x48

        // Methods
        // RVA: 0x0ADDEE08  token: 0x6000AA8
        private static System.Void TraceException(System.String trace, System.Exception e) { }
        // RVA: 0x0ADDED60  token: 0x6000AA9
        private static System.Void TraceExceptionAsReturnValue(System.Exception e) { }
        // RVA: 0x0ADDEDB4  token: 0x6000AAA
        private static System.Void TraceExceptionWithoutRethrow(System.Exception e) { }
        // RVA: 0x0ADDE640  token: 0x6000AAB
        private static System.ArgumentException Argument(System.String error) { }
        // RVA: 0x0ADDE528  token: 0x6000AAC
        private static System.ArgumentOutOfRangeException ArgumentOutOfRange(System.String parameterName) { }
        // RVA: 0x0ADDE5AC  token: 0x6000AAD
        private static System.ArgumentOutOfRangeException ArgumentOutOfRange(System.String message, System.String parameterName) { }
        // RVA: 0x0ADDE8A8  token: 0x6000AAE
        private static System.InvalidOperationException InvalidOperation(System.String error) { }
        // RVA: 0x0ADDECDC  token: 0x6000AAF
        private static System.NotSupportedException NotSupported(System.String error) { }
        // RVA: 0x0ADDEA04  token: 0x6000AB0
        private static System.Boolean IsCatchableExceptionType(System.Exception e) { }
        // RVA: 0x0ADDEB8C  token: 0x6000AB1
        private static System.Boolean IsCatchableOrSecurityExceptionType(System.Exception e) { }
        // RVA: 0x0ADDE748  token: 0x6000AB2
        private static System.ArgumentOutOfRangeException InvalidEnumerationValue(System.Type type, System.Int32 value) { }
        // RVA: 0x0ADDE9B0  token: 0x6000AB3
        private static System.Exception InvalidSeekOrigin(System.String parameterName) { }
        // RVA: 0x0ADDE6C4  token: 0x6000AB4
        private static System.ArgumentOutOfRangeException InvalidAcceptRejectRule(System.Data.AcceptRejectRule value) { }
        // RVA: 0x0ADDE824  token: 0x6000AB5
        private static System.ArgumentOutOfRangeException InvalidMissingSchemaAction(System.Data.MissingSchemaAction value) { }
        // RVA: 0x0ADDE92C  token: 0x6000AB6
        private static System.ArgumentOutOfRangeException InvalidRule(System.Data.Rule value) { }
        // RVA: 0x0ADDEE8C  token: 0x6000AB7
        private static System.Exception WrongType(System.Type got, System.Type expected) { }
        // RVA: 0x0ADDEF2C  token: 0x6000AB8
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000C4  // size: 0x58
    public sealed class BigIntegerStorage : System.Data.Common.DataStorage
    {
        // Fields
        private System.Numerics.BigInteger[] _values;  // 0x50

        // Methods
        // RVA: 0x0ADE0600  token: 0x6000AB9
        private System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0ADDF238  token: 0x6000ABA
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0ADDF36C  token: 0x6000ABB
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADDF26C  token: 0x6000ABC
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0ADDFB28  token: 0x6000ABD
        private static System.Numerics.BigInteger ConvertToBigInteger(System.Object value, System.IFormatProvider formatProvider) { }
        // RVA: 0x0ADDF464  token: 0x6000ABE
        private static System.Object ConvertFromBigInteger(System.Numerics.BigInteger value, System.Type type, System.IFormatProvider formatProvider) { }
        // RVA: 0x0ADE00A0  token: 0x6000ABF
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0ADE0290  token: 0x6000AC0
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADE0344  token: 0x6000AC1
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0ADE051C  token: 0x6000AC2
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0ADE03D8  token: 0x6000AC3
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0ADE0128  token: 0x6000AC4
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0ADDFA94  token: 0x6000AC5
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0ADE030C  token: 0x6000AC6
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0ADE01B8  token: 0x6000AC7
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0ADE04A0  token: 0x6000AC8
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }

    }

    // TypeToken: 0x20000C5  // size: 0x58
    public sealed class BooleanStorage : System.Data.Common.DataStorage
    {
        // Fields
        private System.Boolean[] _values;  // 0x50

        // Methods
        // RVA: 0x0ADE10FC  token: 0x6000AC9
        private System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0ADE06F0  token: 0x6000ACA
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0ADE0AAC  token: 0x6000ACB
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADE09D0  token: 0x6000ACC
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0ADE0BD0  token: 0x6000ACD
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0ADE0DC4  token: 0x6000ACE
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADE0E5C  token: 0x6000ACF
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0ADE1020  token: 0x6000AD0
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0ADE0EDC  token: 0x6000AD1
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0ADE0C80  token: 0x6000AD2
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0ADE0B74  token: 0x6000AD3
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0ADE0E24  token: 0x6000AD4
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0ADE0CE4  token: 0x6000AD5
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0ADE0FA4  token: 0x6000AD6
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }

    }

    // TypeToken: 0x20000C6  // size: 0x58
    public sealed class ByteStorage : System.Data.Common.DataStorage
    {
        // Fields
        private System.Byte[] _values;  // 0x50

        // Methods
        // RVA: 0x0ADE1F80  token: 0x6000AD7
        private System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0ADE11D0  token: 0x6000AD8
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0ADE199C  token: 0x6000AD9
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADE18D0  token: 0x6000ADA
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0ADE1A5C  token: 0x6000ADB
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0ADE1C48  token: 0x6000ADC
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADE1CE0  token: 0x6000ADD
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0ADE1EA4  token: 0x6000ADE
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0ADE1D60  token: 0x6000ADF
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0ADE1B0C  token: 0x6000AE0
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0ADE1A00  token: 0x6000AE1
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0ADE1CA8  token: 0x6000AE2
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0ADE1B70  token: 0x6000AE3
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0ADE1E28  token: 0x6000AE4
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }

    }

    // TypeToken: 0x20000C7  // size: 0x58
    public sealed class CharStorage : System.Data.Common.DataStorage
    {
        // Fields
        private System.Char[] _values;  // 0x50

        // Methods
        // RVA: 0x0ADE2AFC  token: 0x6000AE5
        private System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0ADE2054  token: 0x6000AE6
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0ADE2450  token: 0x6000AE7
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADE2374  token: 0x6000AE8
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0ADE2564  token: 0x6000AE9
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0ADE2754  token: 0x6000AEA
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADE27EC  token: 0x6000AEB
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0ADE29C0  token: 0x6000AEC
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0ADE287C  token: 0x6000AED
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0ADE2618  token: 0x6000AEE
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0ADE2508  token: 0x6000AEF
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0ADE27B4  token: 0x6000AF0
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0ADE267C  token: 0x6000AF1
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0ADE2944  token: 0x6000AF2
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }

    }

    // TypeToken: 0x20000C8  // size: 0x14
    public sealed struct StorageType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Data.Common.StorageType Empty;  // const
        public static System.Data.Common.StorageType Object;  // const
        public static System.Data.Common.StorageType DBNull;  // const
        public static System.Data.Common.StorageType Boolean;  // const
        public static System.Data.Common.StorageType Char;  // const
        public static System.Data.Common.StorageType SByte;  // const
        public static System.Data.Common.StorageType Byte;  // const
        public static System.Data.Common.StorageType Int16;  // const
        public static System.Data.Common.StorageType UInt16;  // const
        public static System.Data.Common.StorageType Int32;  // const
        public static System.Data.Common.StorageType UInt32;  // const
        public static System.Data.Common.StorageType Int64;  // const
        public static System.Data.Common.StorageType UInt64;  // const
        public static System.Data.Common.StorageType Single;  // const
        public static System.Data.Common.StorageType Double;  // const
        public static System.Data.Common.StorageType Decimal;  // const
        public static System.Data.Common.StorageType DateTime;  // const
        public static System.Data.Common.StorageType TimeSpan;  // const
        public static System.Data.Common.StorageType String;  // const
        public static System.Data.Common.StorageType Guid;  // const
        public static System.Data.Common.StorageType ByteArray;  // const
        public static System.Data.Common.StorageType CharArray;  // const
        public static System.Data.Common.StorageType Type;  // const
        public static System.Data.Common.StorageType DateTimeOffset;  // const
        public static System.Data.Common.StorageType BigInteger;  // const
        public static System.Data.Common.StorageType Uri;  // const
        public static System.Data.Common.StorageType SqlBinary;  // const
        public static System.Data.Common.StorageType SqlBoolean;  // const
        public static System.Data.Common.StorageType SqlByte;  // const
        public static System.Data.Common.StorageType SqlBytes;  // const
        public static System.Data.Common.StorageType SqlChars;  // const
        public static System.Data.Common.StorageType SqlDateTime;  // const
        public static System.Data.Common.StorageType SqlDecimal;  // const
        public static System.Data.Common.StorageType SqlDouble;  // const
        public static System.Data.Common.StorageType SqlGuid;  // const
        public static System.Data.Common.StorageType SqlInt16;  // const
        public static System.Data.Common.StorageType SqlInt32;  // const
        public static System.Data.Common.StorageType SqlInt64;  // const
        public static System.Data.Common.StorageType SqlMoney;  // const
        public static System.Data.Common.StorageType SqlSingle;  // const
        public static System.Data.Common.StorageType SqlString;  // const

    }

    // TypeToken: 0x20000C9  // size: 0x50
    public abstract class DataStorage
    {
        // Fields
        private static readonly System.Type[] s_storageClassType;  // static @ 0x0
        private readonly System.Data.DataColumn _column;  // 0x10
        private readonly System.Data.DataTable _table;  // 0x18
        private readonly System.Type _dataType;  // 0x20
        private readonly System.Data.Common.StorageType _storageTypeCode;  // 0x28
        private System.Collections.BitArray _dbNullBits;  // 0x30
        private readonly System.Object _defaultValue;  // 0x38
        private readonly System.Object _nullValue;  // 0x40
        private readonly System.Boolean _isCloneable;  // 0x48
        private readonly System.Boolean _isCustomDefinedType;  // 0x49
        private readonly System.Boolean _isStringType;  // 0x4a
        private readonly System.Boolean _isValueType;  // 0x4b
        private static readonly System.Func<System.Type,System.Tuple<System.Boolean,System.Boolean,System.Boolean,System.Boolean>> s_inspectTypeForInterfaces;  // static @ 0x8
        private static readonly System.Collections.Concurrent.ConcurrentDictionary<System.Type,System.Tuple<System.Boolean,System.Boolean,System.Boolean,System.Boolean>> s_typeImplementsInterface;  // static @ 0x10

        // Properties
        System.Data.DataSetDateTime DateTimeMode { get; /* RVA: 0x0ADE4DEC */ }
        System.IFormatProvider FormatProvider { get; /* RVA: 0x0ADE4E08 */ }

        // Methods
        // RVA: 0x0ADE4D54  token: 0x6000AF3
        protected System.Void .ctor(System.Data.DataColumn column, System.Type type, System.Object defaultValue, System.Data.Common.StorageType storageType) { }
        // RVA: 0x0ADE4C10  token: 0x6000AF4
        protected System.Void .ctor(System.Data.DataColumn column, System.Type type, System.Object defaultValue, System.Object nullValue, System.Data.Common.StorageType storageType) { }
        // RVA: 0x0ADE4C40  token: 0x6000AF5
        protected System.Void .ctor(System.Data.DataColumn column, System.Type type, System.Object defaultValue, System.Object nullValue, System.Boolean isICloneable, System.Data.Common.StorageType storageType) { }
        // RVA: 0x0ADE2C74  token: 0x6000AF8
        public virtual System.Object Aggregate(System.Int32[] recordNos, System.Data.AggregateType kind) { }
        // RVA: 0x0ADE2BD0  token: 0x6000AF9
        public System.Object AggregateCount(System.Int32[] recordNos) { }
        // RVA: 0x0ADE2C88  token: 0x6000AFA
        protected System.Int32 CompareBits(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: -1  // abstract  token: 0x6000AFB
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: -1  // abstract  token: 0x6000AFC
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo1, System.Object value) { }
        // RVA: 0x04D88C30  token: 0x6000AFD
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0ADE2D5C  token: 0x6000AFE
        protected System.Void CopyBits(System.Int32 srcRecordNo, System.Int32 dstRecordNo) { }
        // RVA: -1  // abstract  token: 0x6000AFF
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: -1  // abstract  token: 0x6000B00
        public virtual System.Object Get(System.Int32 recordNo) { }
        // RVA: 0x0ADE38B4  token: 0x6000B01
        protected System.Object GetBits(System.Int32 recordNo) { }
        // RVA: 0x04DA5AE0  token: 0x6000B02
        public virtual System.Int32 GetStringLength(System.Int32 record) { }
        // RVA: 0x0ADE3B5C  token: 0x6000B03
        protected System.Boolean HasValue(System.Int32 recordNo) { }
        // RVA: 0x0ADE3ED0  token: 0x6000B04
        public virtual System.Boolean IsNull(System.Int32 recordNo) { }
        // RVA: -1  // abstract  token: 0x6000B05
        public virtual System.Void Set(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0ADE4170  token: 0x6000B06
        protected System.Void SetNullBit(System.Int32 recordNo, System.Boolean flag) { }
        // RVA: 0x0ADE40E0  token: 0x6000B07
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: -1  // abstract  token: 0x6000B08
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0ADE2D28  token: 0x6000B09
        public virtual System.Object ConvertXmlToObject(System.Xml.XmlReader xmlReader, System.Xml.Serialization.XmlRootAttribute xmlAttrib) { }
        // RVA: -1  // abstract  token: 0x6000B0A
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0ADE2CEC  token: 0x6000B0B
        public virtual System.Void ConvertObjectToXml(System.Object value, System.Xml.XmlWriter xmlWriter, System.Xml.Serialization.XmlRootAttribute xmlAttrib) { }
        // RVA: 0x0ADE2E00  token: 0x6000B0C
        public static System.Data.Common.DataStorage CreateStorage(System.Data.DataColumn column, System.Type dataType, System.Data.Common.StorageType typeCode) { }
        // RVA: 0x0ADE3954  token: 0x6000B0D
        private static System.Data.Common.StorageType GetStorageType(System.Type dataType) { }
        // RVA: 0x0ADE3A14  token: 0x6000B0E
        private static System.Type GetTypeStorage(System.Data.Common.StorageType storageType) { }
        // RVA: 0x0ADE4094  token: 0x6000B0F
        private static System.Boolean IsTypeCustomType(System.Type type) { }
        // RVA: 0x0ADE4084  token: 0x6000B10
        private static System.Boolean IsTypeCustomType(System.Data.Common.StorageType typeCode) { }
        // RVA: 0x0ADE407C  token: 0x6000B11
        private static System.Boolean IsSqlType(System.Data.Common.StorageType storageType) { }
        // RVA: 0x0ADE3FB4  token: 0x6000B12
        public static System.Boolean IsSqlType(System.Type dataType) { }
        // RVA: 0x0ADE37BC  token: 0x6000B13
        private static System.Boolean DetermineIfValueType(System.Data.Common.StorageType typeCode, System.Type dataType) { }
        // RVA: 0x0ADE3C1C  token: 0x6000B14
        private static System.Void ImplementsInterfaces(System.Data.Common.StorageType typeCode, System.Type dataType, System.Boolean& sqlType, System.Boolean& nullable, System.Boolean& xmlSerializable, System.Boolean& changeTracking, System.Boolean& revertibleChangeTracking) { }
        // RVA: 0x0ADE3D68  token: 0x6000B15
        private static System.Tuple<System.Boolean,System.Boolean,System.Boolean,System.Boolean> InspectTypeForInterfaces(System.Type dataType) { }
        // RVA: 0x0ADE3B80  token: 0x6000B16
        private static System.Boolean ImplementsINullableValue(System.Data.Common.StorageType typeCode, System.Type dataType) { }
        // RVA: 0x0ADE3EF0  token: 0x6000B17
        public static System.Boolean IsObjectNull(System.Object value) { }
        // RVA: 0x0ADE3F68  token: 0x6000B18
        public static System.Boolean IsObjectSqlNull(System.Object value) { }
        // RVA: 0x0ADE38E8  token: 0x6000B19
        private System.Object GetEmptyStorageInternal(System.Int32 recordCount) { }
        // RVA: 0x0ADE2DA0  token: 0x6000B1A
        private System.Void CopyValueInternal(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0ADE4190  token: 0x6000B1B
        private System.Void SetStorageInternal(System.Object store, System.Collections.BitArray nullbits) { }
        // RVA: -1  // abstract  token: 0x6000B1C
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: -1  // abstract  token: 0x6000B1D
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: -1  // abstract  token: 0x6000B1E
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }
        // RVA: 0x05396200  token: 0x6000B1F
        protected System.Void SetNullStorage(System.Collections.BitArray nullbits) { }
        // RVA: 0x0ADE3A78  token: 0x6000B20
        private static System.Type GetType(System.String value) { }
        // RVA: 0x0ADE38FC  token: 0x6000B21
        private static System.String GetQualifiedName(System.Type type) { }
        // RVA: 0x0ADE41DC  token: 0x6000B22
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000CA  // size: 0x58
    public sealed class DateTimeOffsetStorage : System.Data.Common.DataStorage
    {
        // Fields
        private static readonly System.DateTimeOffset s_defaultValue;  // static @ 0x0
        private System.DateTimeOffset[] _values;  // 0x50

        // Methods
        // RVA: 0x0ADE5B80  token: 0x6000B23
        private System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0ADE4E28  token: 0x6000B24
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0ADE53A8  token: 0x6000B25
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADE525C  token: 0x6000B26
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0ADE559C  token: 0x6000B27
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0ADE575C  token: 0x6000B28
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADE5810  token: 0x6000B29
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0ADE5A3C  token: 0x6000B2A
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0ADE58F8  token: 0x6000B2B
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0ADE5614  token: 0x6000B2C
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0ADE5534  token: 0x6000B2D
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0ADE57D8  token: 0x6000B2E
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0ADE5684  token: 0x6000B2F
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0ADE59C0  token: 0x6000B30
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }
        // RVA: 0x0ADE5B20  token: 0x6000B31
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000CB  // size: 0x58
    public sealed class DateTimeStorage : System.Data.Common.DataStorage
    {
        // Fields
        private static readonly System.DateTime s_defaultValue;  // static @ 0x0
        private System.DateTime[] _values;  // 0x50

        // Methods
        // RVA: 0x0ADE6C6C  token: 0x6000B32
        private System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0ADE5C80  token: 0x6000B33
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0ADE6188  token: 0x6000B34
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADE6074  token: 0x6000B35
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0ADE6360  token: 0x6000B36
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0ADE65C4  token: 0x6000B37
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADE6670  token: 0x6000B38
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0ADE69B0  token: 0x6000B39
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0ADE6748  token: 0x6000B3A
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0ADE6414  token: 0x6000B3B
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0ADE62C8  token: 0x6000B3C
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0ADE6638  token: 0x6000B3D
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0ADE64A4  token: 0x6000B3E
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0ADE6810  token: 0x6000B3F
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }
        // RVA: 0x0ADE6C0C  token: 0x6000B40
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000CC  // size: 0x58
    public sealed class DecimalStorage : System.Data.Common.DataStorage
    {
        // Fields
        private static readonly System.Decimal s_defaultValue;  // static @ 0x0
        private System.Decimal[] _values;  // 0x50

        // Methods
        // RVA: 0x0ADE7E90  token: 0x6000B41
        private System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0ADE6D68  token: 0x6000B42
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0ADE771C  token: 0x6000B43
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADE75DC  token: 0x6000B44
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0ADE78F8  token: 0x6000B45
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0ADE7AFC  token: 0x6000B46
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADE7BB0  token: 0x6000B47
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0ADE7D7C  token: 0x6000B48
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0ADE7C38  token: 0x6000B49
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0ADE79B4  token: 0x6000B4A
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0ADE7890  token: 0x6000B4B
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0ADE7B78  token: 0x6000B4C
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0ADE7A24  token: 0x6000B4D
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0ADE7D00  token: 0x6000B4E
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }

    }

    // TypeToken: 0x20000CD  // size: 0x58
    public sealed class DoubleStorage : System.Data.Common.DataStorage
    {
        // Fields
        private System.Double[] _values;  // 0x50

        // Methods
        // RVA: 0x0ADE8CA8  token: 0x6000B4F
        private System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0ADE7F84  token: 0x6000B50
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0ADE868C  token: 0x6000B51
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADE85B4  token: 0x6000B52
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0ADE8770  token: 0x6000B53
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0ADE8960  token: 0x6000B54
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADE89F8  token: 0x6000B55
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0ADE8BC8  token: 0x6000B56
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0ADE8A84  token: 0x6000B57
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0ADE8824  token: 0x6000B58
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0ADE8714  token: 0x6000B59
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0ADE89C0  token: 0x6000B5A
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0ADE8888  token: 0x6000B5B
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0ADE8B4C  token: 0x6000B5C
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }

    }

    // TypeToken: 0x20000CE  // size: 0x58
    public sealed class Int16Storage : System.Data.Common.DataStorage
    {
        // Fields
        private System.Int16[] _values;  // 0x50

        // Methods
        // RVA: 0x0ADE9B44  token: 0x6000B5D
        private System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0ADE8D80  token: 0x6000B5E
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0ADE95A0  token: 0x6000B5F
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADE94D8  token: 0x6000B60
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0ADE9668  token: 0x6000B61
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0ADE2754  token: 0x6000B62
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADE9890  token: 0x6000B63
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0ADE9A64  token: 0x6000B64
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0ADE9920  token: 0x6000B65
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0ADE971C  token: 0x6000B66
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0ADE960C  token: 0x6000B67
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0ADE9858  token: 0x6000B68
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0ADE9780  token: 0x6000B69
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0ADE99E8  token: 0x6000B6A
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }

    }

    // TypeToken: 0x20000CF  // size: 0x58
    public sealed class Int32Storage : System.Data.Common.DataStorage
    {
        // Fields
        private System.Int32[] _values;  // 0x50

        // Methods
        // RVA: 0x0ADEAA04  token: 0x6000B6B
        private System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0ADE9C18  token: 0x6000B6C
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0ADEA40C  token: 0x6000B6D
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADEA344  token: 0x6000B6E
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0ADEA4E0  token: 0x6000B6F
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0ADEA6CC  token: 0x6000B70
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADEA764  token: 0x6000B71
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0ADEA928  token: 0x6000B72
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0ADEA7E4  token: 0x6000B73
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0ADEA590  token: 0x6000B74
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0ADEA484  token: 0x6000B75
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0ADEA72C  token: 0x6000B76
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0ADEA5F4  token: 0x6000B77
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0ADEA8AC  token: 0x6000B78
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }

    }

    // TypeToken: 0x20000D0  // size: 0x58
    public sealed class Int64Storage : System.Data.Common.DataStorage
    {
        // Fields
        private System.Int64[] _values;  // 0x50

        // Methods
        // RVA: 0x0ADEB8C8  token: 0x6000B79
        private System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0ADEAAD8  token: 0x6000B7A
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0ADEB2C0  token: 0x6000B7B
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADEB1F4  token: 0x6000B7C
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0ADEB398  token: 0x6000B7D
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0ADEB588  token: 0x6000B7E
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADEB620  token: 0x6000B7F
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0ADEB7E8  token: 0x6000B80
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0ADEB6A4  token: 0x6000B81
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0ADEB44C  token: 0x6000B82
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0ADEB33C  token: 0x6000B83
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0ADEB5E8  token: 0x6000B84
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0ADEB4B0  token: 0x6000B85
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0ADEB76C  token: 0x6000B86
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }

    }

    // TypeToken: 0x20000D1  // size: 0x58
    public sealed class SByteStorage : System.Data.Common.DataStorage
    {
        // Fields
        private System.SByte[] _values;  // 0x50

        // Methods
        // RVA: 0x0ADEF3BC  token: 0x6000B87
        public System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0ADEE664  token: 0x6000B88
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0ADEEE38  token: 0x6000B89
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADEED6C  token: 0x6000B8A
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0ADEEEF8  token: 0x6000B8B
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0ADE1C48  token: 0x6000B8C
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADEF11C  token: 0x6000B8D
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0ADEF2E0  token: 0x6000B8E
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0ADEF19C  token: 0x6000B8F
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0ADEEFA8  token: 0x6000B90
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0ADEEE9C  token: 0x6000B91
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0ADEF0E4  token: 0x6000B92
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0ADEF00C  token: 0x6000B93
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0ADEF264  token: 0x6000B94
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }

    }

    // TypeToken: 0x20000D2  // size: 0x10
    public static class SqlConvert
    {
        // Methods
        // RVA: 0x0ADF3F3C  token: 0x6000B95
        public static System.Data.SqlTypes.SqlByte ConvertToSqlByte(System.Object value) { }
        // RVA: 0x0ADF507C  token: 0x6000B96
        public static System.Data.SqlTypes.SqlInt16 ConvertToSqlInt16(System.Object value) { }
        // RVA: 0x0ADF5264  token: 0x6000B97
        public static System.Data.SqlTypes.SqlInt32 ConvertToSqlInt32(System.Object value) { }
        // RVA: 0x0ADF550C  token: 0x6000B98
        public static System.Data.SqlTypes.SqlInt64 ConvertToSqlInt64(System.Object value) { }
        // RVA: 0x0ADF4974  token: 0x6000B99
        public static System.Data.SqlTypes.SqlDouble ConvertToSqlDouble(System.Object value) { }
        // RVA: 0x0ADF4470  token: 0x6000B9A
        public static System.Data.SqlTypes.SqlDecimal ConvertToSqlDecimal(System.Object value) { }
        // RVA: 0x0ADF5D2C  token: 0x6000B9B
        public static System.Data.SqlTypes.SqlSingle ConvertToSqlSingle(System.Object value) { }
        // RVA: 0x0ADF58B4  token: 0x6000B9C
        public static System.Data.SqlTypes.SqlMoney ConvertToSqlMoney(System.Object value) { }
        // RVA: 0x0ADF42E0  token: 0x6000B9D
        public static System.Data.SqlTypes.SqlDateTime ConvertToSqlDateTime(System.Object value) { }
        // RVA: 0x0ADF3DDC  token: 0x6000B9E
        public static System.Data.SqlTypes.SqlBoolean ConvertToSqlBoolean(System.Object value) { }
        // RVA: 0x0ADF4F0C  token: 0x6000B9F
        public static System.Data.SqlTypes.SqlGuid ConvertToSqlGuid(System.Object value) { }
        // RVA: 0x0ADF3C78  token: 0x6000BA0
        public static System.Data.SqlTypes.SqlBinary ConvertToSqlBinary(System.Object value) { }
        // RVA: 0x0ADF620C  token: 0x6000BA1
        public static System.Data.SqlTypes.SqlString ConvertToSqlString(System.Object value) { }
        // RVA: 0x0ADF41C4  token: 0x6000BA2
        public static System.Data.SqlTypes.SqlChars ConvertToSqlChars(System.Object value) { }
        // RVA: 0x0ADF40A8  token: 0x6000BA3
        public static System.Data.SqlTypes.SqlBytes ConvertToSqlBytes(System.Object value) { }
        // RVA: 0x0ADF3C08  token: 0x6000BA4
        public static System.DateTimeOffset ConvertStringToDateTimeOffset(System.String value, System.IFormatProvider formatProvider) { }
        // RVA: 0x0ADF23F0  token: 0x6000BA5
        public static System.Object ChangeTypeForDefaultValue(System.Object value, System.Type type, System.IFormatProvider formatProvider) { }
        // RVA: 0x0ADF14C4  token: 0x6000BA6
        public static System.Object ChangeType2(System.Object value, System.Data.Common.StorageType stype, System.Type type, System.IFormatProvider formatProvider) { }
        // RVA: 0x0ADF2564  token: 0x6000BA7
        public static System.Object ChangeTypeForXML(System.Object value, System.Type type) { }

    }

    // TypeToken: 0x20000D3  // size: 0x58
    public sealed class SqlBinaryStorage : System.Data.Common.DataStorage
    {
        // Fields
        private System.Data.SqlTypes.SqlBinary[] _values;  // 0x50

        // Methods
        // RVA: 0x0ADEFED0  token: 0x6000BA8
        public System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0ADEF490  token: 0x6000BA9
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0ADEF71C  token: 0x6000BAA
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADEF68C  token: 0x6000BAB
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0ADEF93C  token: 0x6000BAC
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0ADEFC1C  token: 0x6000BAD
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADEFC9C  token: 0x6000BAE
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0ADEFD0C  token: 0x6000BAF
        public virtual System.Boolean IsNull(System.Int32 record) { }
        // RVA: 0x0ADEFE90  token: 0x6000BB0
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0ADEFD74  token: 0x6000BB1
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0ADEF9A0  token: 0x6000BB2
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0ADEF7B4  token: 0x6000BB3
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0ADEFC64  token: 0x6000BB4
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0ADEFB54  token: 0x6000BB5
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0ADEFE2C  token: 0x6000BB6
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }

    }

    // TypeToken: 0x20000D4  // size: 0x58
    public sealed class SqlByteStorage : System.Data.Common.DataStorage
    {
        // Fields
        private System.Data.SqlTypes.SqlByte[] _values;  // 0x50

        // Methods
        // RVA: 0x0ADF13B0  token: 0x6000BB7
        public System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0ADEFFE0  token: 0x6000BB8
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0ADF0BE4  token: 0x6000BB9
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADF0B54  token: 0x6000BBA
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0ADF0E04  token: 0x6000BBB
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0ADF10F4  token: 0x6000BBC
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADF1178  token: 0x6000BBD
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0ADF11E8  token: 0x6000BBE
        public virtual System.Boolean IsNull(System.Int32 record) { }
        // RVA: 0x0ADF136C  token: 0x6000BBF
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0ADF1250  token: 0x6000BC0
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0ADF0E68  token: 0x6000BC1
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0ADF0C7C  token: 0x6000BC2
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0ADF1140  token: 0x6000BC3
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0ADF101C  token: 0x6000BC4
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0ADF1308  token: 0x6000BC5
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }

    }

    // TypeToken: 0x20000D5  // size: 0x58
    public sealed class SqlBytesStorage : System.Data.Common.DataStorage
    {
        // Fields
        private System.Data.SqlTypes.SqlBytes[] _values;  // 0x50

        // Methods
        // RVA: 0x0ADF89F4  token: 0x6000BC6
        public System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0ADF8138  token: 0x6000BC7
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x011EC580  token: 0x6000BC8
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x011EC580  token: 0x6000BC9
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0ADF8744  token: 0x6000BCA
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADF87B8  token: 0x6000BCB
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0ADF87E4  token: 0x6000BCC
        public virtual System.Boolean IsNull(System.Int32 record) { }
        // RVA: 0x0ADF8938  token: 0x6000BCD
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0ADF881C  token: 0x6000BCE
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0ADF8494  token: 0x6000BCF
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0ADF830C  token: 0x6000BD0
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0ADF8780  token: 0x6000BD1
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0ADF8668  token: 0x6000BD2
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0ADF88D4  token: 0x6000BD3
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }

    }

    // TypeToken: 0x20000D6  // size: 0x58
    public sealed class SqlCharsStorage : System.Data.Common.DataStorage
    {
        // Fields
        private System.Data.SqlTypes.SqlChars[] _values;  // 0x50

        // Methods
        // RVA: 0x0ADF9300  token: 0x6000BD4
        public System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0ADF8AC8  token: 0x6000BD5
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x011EC580  token: 0x6000BD6
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x011EC580  token: 0x6000BD7
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0ADF8744  token: 0x6000BD8
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADF87B8  token: 0x6000BD9
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0ADF87E4  token: 0x6000BDA
        public virtual System.Boolean IsNull(System.Int32 record) { }
        // RVA: 0x0ADF9244  token: 0x6000BDB
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0ADF9128  token: 0x6000BDC
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0ADF8E24  token: 0x6000BDD
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0ADF8C9C  token: 0x6000BDE
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0ADF90F0  token: 0x6000BDF
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0ADF9014  token: 0x6000BE0
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0ADF91E0  token: 0x6000BE1
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }

    }

    // TypeToken: 0x20000D7  // size: 0x58
    public sealed class SqlDateTimeStorage : System.Data.Common.DataStorage
    {
        // Fields
        private System.Data.SqlTypes.SqlDateTime[] _values;  // 0x50

        // Methods
        // RVA: 0x0ADFA190  token: 0x6000BE2
        public System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0ADF93D4  token: 0x6000BE3
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0ADF9954  token: 0x6000BE4
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADF98AC  token: 0x6000BE5
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0ADF9B84  token: 0x6000BE6
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0ADF9EA8  token: 0x6000BE7
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADF9F3C  token: 0x6000BE8
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0ADF9FB4  token: 0x6000BE9
        public virtual System.Boolean IsNull(System.Int32 record) { }
        // RVA: 0x0ADFA134  token: 0x6000BEA
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0ADFA018  token: 0x6000BEB
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0ADF9BF8  token: 0x6000BEC
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0ADF99FC  token: 0x6000BED
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0ADF9F04  token: 0x6000BEE
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0ADF9DC8  token: 0x6000BEF
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0ADFA0D0  token: 0x6000BF0
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }

    }

    // TypeToken: 0x20000D8  // size: 0x58
    public sealed class SqlDecimalStorage : System.Data.Common.DataStorage
    {
        // Fields
        private System.Data.SqlTypes.SqlDecimal[] _values;  // 0x50

        // Methods
        // RVA: 0x0ADFB794  token: 0x6000BF1
        public System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0ADFA2CC  token: 0x6000BF2
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0ADFAF58  token: 0x6000BF3
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADFAEB4  token: 0x6000BF4
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0ADFB188  token: 0x6000BF5
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0ADFB4A8  token: 0x6000BF6
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADFB53C  token: 0x6000BF7
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0ADFB5B4  token: 0x6000BF8
        public virtual System.Boolean IsNull(System.Int32 record) { }
        // RVA: 0x0ADFB738  token: 0x6000BF9
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0ADFB61C  token: 0x6000BFA
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0ADFB1FC  token: 0x6000BFB
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0ADFB000  token: 0x6000BFC
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0ADFB504  token: 0x6000BFD
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0ADFB3C0  token: 0x6000BFE
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0ADFB6D4  token: 0x6000BFF
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }

    }

    // TypeToken: 0x20000D9  // size: 0x58
    public sealed class SqlDoubleStorage : System.Data.Common.DataStorage
    {
        // Fields
        private System.Data.SqlTypes.SqlDouble[] _values;  // 0x50

        // Methods
        // RVA: 0x0ADFCC50  token: 0x6000C00
        public System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0ADFB8CC  token: 0x6000C01
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0ADFC450  token: 0x6000C02
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADFC3B4  token: 0x6000C03
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0ADFC678  token: 0x6000C04
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0ADFC980  token: 0x6000C05
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADFCA0C  token: 0x6000C06
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0ADFCA7C  token: 0x6000C07
        public virtual System.Boolean IsNull(System.Int32 record) { }
        // RVA: 0x0ADFCBFC  token: 0x6000C08
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0ADFCAE0  token: 0x6000C09
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0ADFC6E4  token: 0x6000C0A
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0ADFC4F0  token: 0x6000C0B
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0ADFC9D4  token: 0x6000C0C
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0ADFC8A8  token: 0x6000C0D
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0ADFCB98  token: 0x6000C0E
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }

    }

    // TypeToken: 0x20000DA  // size: 0x58
    public sealed class SqlGuidStorage : System.Data.Common.DataStorage
    {
        // Fields
        private System.Data.SqlTypes.SqlGuid[] _values;  // 0x50

        // Methods
        // RVA: 0x0ADFD768  token: 0x6000C0F
        public System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0ADFCD70  token: 0x6000C10
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0ADFCFFC  token: 0x6000C11
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADFCF6C  token: 0x6000C12
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0ADFD21C  token: 0x6000C13
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0ADEFC1C  token: 0x6000C14
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADFD534  token: 0x6000C15
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0ADFD5A4  token: 0x6000C16
        public virtual System.Boolean IsNull(System.Int32 record) { }
        // RVA: 0x0ADFD728  token: 0x6000C17
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0ADFD60C  token: 0x6000C18
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0ADFD280  token: 0x6000C19
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0ADFD094  token: 0x6000C1A
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0ADFD4FC  token: 0x6000C1B
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0ADFD434  token: 0x6000C1C
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0ADFD6C4  token: 0x6000C1D
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }

    }

    // TypeToken: 0x20000DB  // size: 0x58
    public sealed class SqlInt16Storage : System.Data.Common.DataStorage
    {
        // Fields
        private System.Data.SqlTypes.SqlInt16[] _values;  // 0x50

        // Methods
        // RVA: 0x0ADFEC2C  token: 0x6000C1E
        public System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0ADFD878  token: 0x6000C1F
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0ADFE46C  token: 0x6000C20
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADFE3DC  token: 0x6000C21
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0ADFE68C  token: 0x6000C22
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0ADFE978  token: 0x6000C23
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADFE9FC  token: 0x6000C24
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0ADFEA68  token: 0x6000C25
        public virtual System.Boolean IsNull(System.Int32 record) { }
        // RVA: 0x0ADFEBEC  token: 0x6000C26
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0ADFEAD0  token: 0x6000C27
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0ADFE6F0  token: 0x6000C28
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0ADFE504  token: 0x6000C29
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0ADFE9C4  token: 0x6000C2A
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0ADFE8A4  token: 0x6000C2B
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0ADFEB88  token: 0x6000C2C
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }

    }

    // TypeToken: 0x20000DC  // size: 0x58
    public sealed class SqlInt32Storage : System.Data.Common.DataStorage
    {
        // Fields
        private System.Data.SqlTypes.SqlInt32[] _values;  // 0x50

        // Methods
        // RVA: 0x0AE000DC  token: 0x6000C2D
        public System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0ADFED3C  token: 0x6000C2E
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0ADFF910  token: 0x6000C2F
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADFF880  token: 0x6000C30
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0ADFFB30  token: 0x6000C31
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0ADFFE20  token: 0x6000C32
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADFFEA4  token: 0x6000C33
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0ADFFF14  token: 0x6000C34
        public virtual System.Boolean IsNull(System.Int32 record) { }
        // RVA: 0x0AE00098  token: 0x6000C35
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0ADFFF7C  token: 0x6000C36
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0ADFFB94  token: 0x6000C37
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0ADFF9A8  token: 0x6000C38
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0ADFFE6C  token: 0x6000C39
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0ADFFD48  token: 0x6000C3A
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0AE00034  token: 0x6000C3B
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }

    }

    // TypeToken: 0x20000DD  // size: 0x58
    public sealed class SqlInt64Storage : System.Data.Common.DataStorage
    {
        // Fields
        private System.Data.SqlTypes.SqlInt64[] _values;  // 0x50

        // Methods
        // RVA: 0x0AE01608  token: 0x6000C3C
        public System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0AE001EC  token: 0x6000C3D
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0AE00E5C  token: 0x6000C3E
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0AE00DC0  token: 0x6000C3F
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0AE01084  token: 0x6000C40
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0ADFC980  token: 0x6000C41
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0AE013C4  token: 0x6000C42
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0AE01434  token: 0x6000C43
        public virtual System.Boolean IsNull(System.Int32 record) { }
        // RVA: 0x0AE015B4  token: 0x6000C44
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0AE01498  token: 0x6000C45
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0AE010F0  token: 0x6000C46
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0AE00EFC  token: 0x6000C47
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0AE0138C  token: 0x6000C48
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0AE012B4  token: 0x6000C49
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0AE01550  token: 0x6000C4A
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }

    }

    // TypeToken: 0x20000DE  // size: 0x58
    public sealed class SqlMoneyStorage : System.Data.Common.DataStorage
    {
        // Fields
        private System.Data.SqlTypes.SqlMoney[] _values;  // 0x50

        // Methods
        // RVA: 0x0AE02B8C  token: 0x6000C4B
        public System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0AE01728  token: 0x6000C4C
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0AE023E0  token: 0x6000C4D
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0AE02344  token: 0x6000C4E
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0AE02608  token: 0x6000C4F
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0ADFC980  token: 0x6000C50
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0AE02948  token: 0x6000C51
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0AE029B8  token: 0x6000C52
        public virtual System.Boolean IsNull(System.Int32 record) { }
        // RVA: 0x0AE02B38  token: 0x6000C53
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0AE02A1C  token: 0x6000C54
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0AE02674  token: 0x6000C55
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0AE02480  token: 0x6000C56
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0AE02910  token: 0x6000C57
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0AE02838  token: 0x6000C58
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0AE02AD4  token: 0x6000C59
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }

    }

    // TypeToken: 0x20000DF  // size: 0x58
    public sealed class SqlSingleStorage : System.Data.Common.DataStorage
    {
        // Fields
        private System.Data.SqlTypes.SqlSingle[] _values;  // 0x50

        // Methods
        // RVA: 0x0AE03FC4  token: 0x6000C5A
        public System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0AE02CAC  token: 0x6000C5B
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0AE03844  token: 0x6000C5C
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0AE037B4  token: 0x6000C5D
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0AE03A64  token: 0x6000C5E
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0ADFFE20  token: 0x6000C5F
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0AE03D8C  token: 0x6000C60
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0AE03DFC  token: 0x6000C61
        public virtual System.Boolean IsNull(System.Int32 record) { }
        // RVA: 0x0AE03F80  token: 0x6000C62
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0AE03E64  token: 0x6000C63
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0AE03AC8  token: 0x6000C64
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0AE038DC  token: 0x6000C65
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0AE03D54  token: 0x6000C66
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0AE03C7C  token: 0x6000C67
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0AE03F1C  token: 0x6000C68
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }

    }

    // TypeToken: 0x20000E0  // size: 0x58
    public sealed class SqlStringStorage : System.Data.Common.DataStorage
    {
        // Fields
        private System.Data.SqlTypes.SqlString[] _values;  // 0x50

        // Methods
        // RVA: 0x0AE04E78  token: 0x6000C69
        public System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0AE040D4  token: 0x6000C6A
        public virtual System.Object Aggregate(System.Int32[] recordNos, System.Data.AggregateType kind) { }
        // RVA: 0x0AE044C0  token: 0x6000C6B
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0AE04554  token: 0x6000C6C
        public System.Int32 Compare(System.Data.SqlTypes.SqlString valueNo1, System.Data.SqlTypes.SqlString valueNo2) { }
        // RVA: 0x0AE0440C  token: 0x6000C6D
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0AE047BC  token: 0x6000C6E
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0AE04ADC  token: 0x6000C6F
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0AE04C1C  token: 0x6000C70
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0AE04B70  token: 0x6000C71
        public virtual System.Int32 GetStringLength(System.Int32 record) { }
        // RVA: 0x0AE04C94  token: 0x6000C72
        public virtual System.Boolean IsNull(System.Int32 record) { }
        // RVA: 0x0AE04E1C  token: 0x6000C73
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0AE04D00  token: 0x6000C74
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0AE04830  token: 0x6000C75
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0AE04634  token: 0x6000C76
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0AE04B38  token: 0x6000C77
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0AE049F4  token: 0x6000C78
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0AE04DB8  token: 0x6000C79
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }

    }

    // TypeToken: 0x20000E1  // size: 0x58
    public sealed class SqlBooleanStorage : System.Data.Common.DataStorage
    {
        // Fields
        private System.Data.SqlTypes.SqlBoolean[] _values;  // 0x50

        // Methods
        // RVA: 0x0ADF8028  token: 0x6000C7A
        public System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0ADF7464  token: 0x6000C7B
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0ADF7868  token: 0x6000C7C
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADF77DC  token: 0x6000C7D
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0ADF7A88  token: 0x6000C7E
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0ADF7D74  token: 0x6000C7F
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADF7DF8  token: 0x6000C80
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0ADF7E64  token: 0x6000C81
        public virtual System.Boolean IsNull(System.Int32 record) { }
        // RVA: 0x0ADF7FE8  token: 0x6000C82
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0ADF7ECC  token: 0x6000C83
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0ADF7AEC  token: 0x6000C84
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0ADF7900  token: 0x6000C85
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0ADF7DC0  token: 0x6000C86
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0ADF7CA0  token: 0x6000C87
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0ADF7F84  token: 0x6000C88
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }

    }

    // TypeToken: 0x20000E2  // size: 0x58
    public sealed class SingleStorage : System.Data.Common.DataStorage
    {
        // Fields
        private System.Single[] _values;  // 0x50

        // Methods
        // RVA: 0x0ADF7390  token: 0x6000C89
        public System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0ADF6630  token: 0x6000C8A
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0ADF6D78  token: 0x6000C8B
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADF6CA0  token: 0x6000C8C
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0ADF6E58  token: 0x6000C8D
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0ADF7048  token: 0x6000C8E
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADF70E0  token: 0x6000C8F
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0ADF72B0  token: 0x6000C90
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0ADF716C  token: 0x6000C91
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0ADF6F0C  token: 0x6000C92
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0ADF6DFC  token: 0x6000C93
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0ADF70A8  token: 0x6000C94
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0ADF6F70  token: 0x6000C95
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0ADF7234  token: 0x6000C96
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }

    }

    // TypeToken: 0x20000E3  // size: 0x60
    public sealed class SqlUdtStorage : System.Data.Common.DataStorage
    {
        // Fields
        private System.Object[] _values;  // 0x50
        private readonly System.Boolean _implementsIXmlSerializable;  // 0x58
        private readonly System.Boolean _implementsIComparable;  // 0x59
        private static readonly System.Collections.Concurrent.ConcurrentDictionary<System.Type,System.Object> s_typeToNull;  // static @ 0x0

        // Methods
        // RVA: 0x0AE05FB4  token: 0x6000C97
        public System.Void .ctor(System.Data.DataColumn column, System.Type type) { }
        // RVA: 0x0AE05E64  token: 0x6000C98
        private System.Void .ctor(System.Data.DataColumn column, System.Type type, System.Object nullValue) { }
        // RVA: 0x0AE059E4  token: 0x6000C99
        private static System.Object GetStaticNullForUdtType(System.Type type) { }
        // RVA: 0x0AE05AEC  token: 0x6000C9A
        public virtual System.Boolean IsNull(System.Int32 record) { }
        // RVA: 0x0AE04FA8  token: 0x6000C9B
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0AE0514C  token: 0x6000C9C
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0AE04FDC  token: 0x6000C9D
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo1, System.Object value) { }
        // RVA: 0x0AE05934  token: 0x6000C9E
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADF87B8  token: 0x6000C9F
        public virtual System.Object Get(System.Int32 recordNo) { }
        // RVA: 0x0AE05C98  token: 0x6000CA0
        public virtual System.Void Set(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0AE05B6C  token: 0x6000CA1
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0AE0561C  token: 0x6000CA2
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0AE05448  token: 0x6000CA3
        public virtual System.Object ConvertXmlToObject(System.Xml.XmlReader xmlReader, System.Xml.Serialization.XmlRootAttribute xmlAttrib) { }
        // RVA: 0x0AE05258  token: 0x6000CA4
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0AE0518C  token: 0x6000CA5
        public virtual System.Void ConvertObjectToXml(System.Object value, System.Xml.XmlWriter xmlWriter, System.Xml.Serialization.XmlRootAttribute xmlAttrib) { }
        // RVA: 0x0AE059AC  token: 0x6000CA6
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0AE05840  token: 0x6000CA7
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0AE05C34  token: 0x6000CA8
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }
        // RVA: 0x0AE05DD8  token: 0x6000CA9
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000E5  // size: 0x58
    public sealed class StringStorage : System.Data.Common.DataStorage
    {
        // Fields
        private System.String[] _values;  // 0x50

        // Methods
        // RVA: 0x0AE06718  token: 0x6000CAC
        public System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0AE06028  token: 0x6000CAD
        public virtual System.Object Aggregate(System.Int32[] recordNos, System.Data.AggregateType kind) { }
        // RVA: 0x0AE06334  token: 0x6000CAE
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0AE06270  token: 0x6000CAF
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0AE063F4  token: 0x6000CB0
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0AE064FC  token: 0x6000CB1
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0ADED9DC  token: 0x6000CB2
        public virtual System.Object Get(System.Int32 recordNo) { }
        // RVA: 0x0AE06570  token: 0x6000CB3
        public virtual System.Int32 GetStringLength(System.Int32 record) { }
        // RVA: 0x0ADEDA14  token: 0x6000CB4
        public virtual System.Boolean IsNull(System.Int32 record) { }
        // RVA: 0x0AE066C0  token: 0x6000CB5
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0AE065A4  token: 0x6000CB6
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x04D88C30  token: 0x6000CB7
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0AE063AC  token: 0x6000CB8
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0AE06538  token: 0x6000CB9
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0AE06420  token: 0x6000CBA
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0AE0665C  token: 0x6000CBB
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }

    }

    // TypeToken: 0x20000E6  // size: 0x58
    public sealed class TimeSpanStorage : System.Data.Common.DataStorage
    {
        // Fields
        private static readonly System.TimeSpan s_defaultValue;  // static @ 0x0
        private System.TimeSpan[] _values;  // 0x50

        // Methods
        // RVA: 0x0AE079F8  token: 0x6000CBC
        public System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0AE067E4  token: 0x6000CBD
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0AE071E4  token: 0x6000CBE
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0AE070C4  token: 0x6000CBF
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0AE07368  token: 0x6000CC0
        private static System.TimeSpan ConvertToTimeSpan(System.Object value) { }
        // RVA: 0x0AE074D4  token: 0x6000CC1
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0ADE65C4  token: 0x6000CC2
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0AE076D0  token: 0x6000CC3
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0AE078D8  token: 0x6000CC4
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0AE07794  token: 0x6000CC5
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0AE0755C  token: 0x6000CC6
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0AE0730C  token: 0x6000CC7
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0AE07698  token: 0x6000CC8
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0AE075C0  token: 0x6000CC9
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0AE0785C  token: 0x6000CCA
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }
        // RVA: 0x0AE07998  token: 0x6000CCB
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000E7  // size: 0x58
    public sealed class UInt16Storage : System.Data.Common.DataStorage
    {
        // Fields
        private static readonly System.UInt16 s_defaultValue;  // static @ 0x0
        private System.UInt16[] _values;  // 0x50

        // Methods
        // RVA: 0x0AE08A74  token: 0x6000CCC
        public System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0AE07BFC  token: 0x6000CCD
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0AE0845C  token: 0x6000CCE
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0AE08374  token: 0x6000CCF
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0AE08570  token: 0x6000CD0
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0ADE2754  token: 0x6000CD1
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0AE08798  token: 0x6000CD2
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0AE08978  token: 0x6000CD3
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0AE08834  token: 0x6000CD4
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0AE08624  token: 0x6000CD5
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0AE08514  token: 0x6000CD6
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0AE08760  token: 0x6000CD7
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0AE08688  token: 0x6000CD8
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0AE088FC  token: 0x6000CD9
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }

    }

    // TypeToken: 0x20000E8  // size: 0x58
    public sealed class UInt32Storage : System.Data.Common.DataStorage
    {
        // Fields
        private static readonly System.UInt32 s_defaultValue;  // static @ 0x0
        private System.UInt32[] _values;  // 0x50

        // Methods
        // RVA: 0x0AE099B8  token: 0x6000CDA
        public System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0AE08B64  token: 0x6000CDB
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0AE093A4  token: 0x6000CDC
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0AE092C4  token: 0x6000CDD
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0AE094BC  token: 0x6000CDE
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0ADEA6CC  token: 0x6000CDF
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0AE096E0  token: 0x6000CE0
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0AE098C0  token: 0x6000CE1
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0AE0977C  token: 0x6000CE2
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0AE0956C  token: 0x6000CE3
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0AE09460  token: 0x6000CE4
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0AE096A8  token: 0x6000CE5
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0AE095D0  token: 0x6000CE6
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0AE09844  token: 0x6000CE7
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }

    }

    // TypeToken: 0x20000E9  // size: 0x58
    public sealed class UInt64Storage : System.Data.Common.DataStorage
    {
        // Fields
        private static readonly System.UInt64 s_defaultValue;  // static @ 0x0
        private System.UInt64[] _values;  // 0x50

        // Methods
        // RVA: 0x0AE0A970  token: 0x6000CE8
        public System.Void .ctor(System.Data.DataColumn column) { }
        // RVA: 0x0AE09AA8  token: 0x6000CE9
        public virtual System.Object Aggregate(System.Int32[] records, System.Data.AggregateType kind) { }
        // RVA: 0x0AE0A350  token: 0x6000CEA
        public virtual System.Int32 Compare(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0AE0A26C  token: 0x6000CEB
        public virtual System.Int32 CompareValueTo(System.Int32 recordNo, System.Object value) { }
        // RVA: 0x0AE0A46C  token: 0x6000CEC
        public virtual System.Object ConvertValue(System.Object value) { }
        // RVA: 0x0ADEB588  token: 0x6000CED
        public virtual System.Void Copy(System.Int32 recordNo1, System.Int32 recordNo2) { }
        // RVA: 0x0AE0A694  token: 0x6000CEE
        public virtual System.Object Get(System.Int32 record) { }
        // RVA: 0x0AE0A874  token: 0x6000CEF
        public virtual System.Void Set(System.Int32 record, System.Object value) { }
        // RVA: 0x0AE0A730  token: 0x6000CF0
        public virtual System.Void SetCapacity(System.Int32 capacity) { }
        // RVA: 0x0AE0A520  token: 0x6000CF1
        public virtual System.Object ConvertXmlToObject(System.String s) { }
        // RVA: 0x0AE0A410  token: 0x6000CF2
        public virtual System.String ConvertObjectToXml(System.Object value) { }
        // RVA: 0x0AE0A65C  token: 0x6000CF3
        protected virtual System.Object GetEmptyStorage(System.Int32 recordCount) { }
        // RVA: 0x0AE0A584  token: 0x6000CF4
        protected virtual System.Void CopyValue(System.Int32 record, System.Object store, System.Collections.BitArray nullbits, System.Int32 storeIndex) { }
        // RVA: 0x0AE0A7F8  token: 0x6000CF5
        protected virtual System.Void SetStorage(System.Object store, System.Collections.BitArray nullbits) { }

    }

}

namespace System.Data.SqlTypes
{

    // TypeToken: 0x20000A6  // size: 0x10
    public static class SQLResource
    {
        // Properties
        System.String NullString { get; /* RVA: 0x0ADC7184 */ }
        System.String ArithOverflowMessage { get; /* RVA: 0x0ADC7024 */ }
        System.String DivideByZeroMessage { get; /* RVA: 0x0ADC7100 */ }
        System.String NullValueMessage { get; /* RVA: 0x0ADC71B0 */ }
        System.String TruncationMessage { get; /* RVA: 0x0ADC7208 */ }
        System.String DateTimeOverflowMessage { get; /* RVA: 0x0ADC70D4 */ }
        System.String ConcatDiffCollationMessage { get; /* RVA: 0x0ADC707C */ }
        System.String CompareDiffCollationMessage { get; /* RVA: 0x0ADC7050 */ }
        System.String ConversionOverflowMessage { get; /* RVA: 0x0ADC70A8 */ }
        System.String TimeZoneSpecifiedMessage { get; /* RVA: 0x0ADC71DC */ }
        System.String InvalidPrecScaleMessage { get; /* RVA: 0x0ADC7158 */ }
        System.String FormatMessage { get; /* RVA: 0x0ADC712C */ }

        // Methods
        // RVA: 0x0ADC6F34  token: 0x6000891
        private static System.String InvalidOpStreamClosed(System.String method) { }
        // RVA: 0x0ADC6FE8  token: 0x6000892
        private static System.String InvalidOpStreamNonWritable(System.String method) { }
        // RVA: 0x0ADC6F70  token: 0x6000893
        private static System.String InvalidOpStreamNonReadable(System.String method) { }
        // RVA: 0x0ADC6FAC  token: 0x6000894
        private static System.String InvalidOpStreamNonSeekable(System.String method) { }

    }

    // TypeToken: 0x20000A7
    public interface INullable
    {
        // Properties
        System.Boolean IsNull { get; /* RVA: -1  // abstract */ }

    }

    // TypeToken: 0x20000A8  // size: 0x18
    public sealed struct SqlBinary : System.Data.SqlTypes.INullable, System.IComparable, System.Xml.Serialization.IXmlSerializable
    {
        // Fields
        private System.Byte[] _value;  // 0x10
        public static readonly System.Data.SqlTypes.SqlBinary Null;  // static @ 0x0

        // Properties
        System.Boolean IsNull { get; /* RVA: 0x0314CD60 */ }
        System.Byte[] Value { get; /* RVA: 0x0ADC7AF8 */ }

        // Methods
        // RVA: 0x05395148  token: 0x6000896
        private System.Void .ctor(System.Boolean fNull) { }
        // RVA: 0x0ADC7A80  token: 0x6000897
        public System.Void .ctor(System.Byte[] value) { }
        // RVA: 0x0ADC7D54  token: 0x600089A
        public static System.Data.SqlTypes.SqlBinary op_Implicit(System.Byte[] x) { }
        // RVA: 0x0ADC7960  token: 0x600089B
        public virtual System.String ToString() { }
        // RVA: 0x0ADC7680  token: 0x600089C
        private static System.Data.SqlTypes.EComparison PerformCompareByte(System.Byte[] x, System.Byte[] y) { }
        // RVA: 0x0ADC7BBC  token: 0x600089D
        public static System.Data.SqlTypes.SqlBoolean op_Equality(System.Data.SqlTypes.SqlBinary x, System.Data.SqlTypes.SqlBinary y) { }
        // RVA: 0x0ADC7D7C  token: 0x600089E
        public static System.Data.SqlTypes.SqlBoolean op_LessThan(System.Data.SqlTypes.SqlBinary x, System.Data.SqlTypes.SqlBinary y) { }
        // RVA: 0x0ADC7C88  token: 0x600089F
        public static System.Data.SqlTypes.SqlBoolean op_GreaterThan(System.Data.SqlTypes.SqlBinary x, System.Data.SqlTypes.SqlBinary y) { }
        // RVA: 0x0ADC7330  token: 0x60008A0
        public virtual System.Int32 CompareTo(System.Object value) { }
        // RVA: 0x0ADC7234  token: 0x60008A1
        public System.Int32 CompareTo(System.Data.SqlTypes.SqlBinary value) { }
        // RVA: 0x0ADC7420  token: 0x60008A2
        public virtual System.Boolean Equals(System.Object value) { }
        // RVA: 0x0ADC7624  token: 0x60008A3
        private static System.Int32 HashByteArray(System.Byte[] rgbValue, System.Int32 length) { }
        // RVA: 0x0ADC7518  token: 0x60008A4
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x011EC580  token: 0x60008A5
        private virtual System.Xml.Schema.XmlSchema System.Xml.Serialization.IXmlSerializable.GetSchema() { }
        // RVA: 0x0ADC774C  token: 0x60008A6
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader reader) { }
        // RVA: 0x0ADC7870  token: 0x60008A7
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter writer) { }
        // RVA: 0x0ADC75AC  token: 0x60008A8
        public static System.Xml.XmlQualifiedName GetXsdType(System.Xml.Schema.XmlSchemaSet schemaSet) { }
        // RVA: 0x0ADC7A20  token: 0x60008A9
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000A9  // size: 0x11
    public sealed struct SqlBoolean : System.Data.SqlTypes.INullable, System.IComparable, System.Xml.Serialization.IXmlSerializable
    {
        // Fields
        private System.Byte m_value;  // 0x10
        public static readonly System.Data.SqlTypes.SqlBoolean True;  // static @ 0x0
        public static readonly System.Data.SqlTypes.SqlBoolean False;  // static @ 0x1
        public static readonly System.Data.SqlTypes.SqlBoolean Null;  // static @ 0x2
        public static readonly System.Data.SqlTypes.SqlBoolean Zero;  // static @ 0x3
        public static readonly System.Data.SqlTypes.SqlBoolean One;  // static @ 0x4

        // Properties
        System.Boolean IsNull { get; /* RVA: 0x0961F480 */ }
        System.Boolean Value { get; /* RVA: 0x0ADC866C */ }
        System.Boolean IsTrue { get; /* RVA: 0x0ADC8664 */ }
        System.Boolean IsFalse { get; /* RVA: 0x0ADC865C */ }
        System.Byte ByteValue { get; /* RVA: 0x0ADC85D4 */ }

        // Methods
        // RVA: 0x0ADC85CC  token: 0x60008AA
        public System.Void .ctor(System.Boolean value) { }
        // RVA: 0x0ADC856C  token: 0x60008AB
        public System.Void .ctor(System.Int32 value) { }
        // RVA: 0x0ADC85B8  token: 0x60008AC
        private System.Void .ctor(System.Int32 value, System.Boolean fNull) { }
        // RVA: 0x0ADC889C  token: 0x60008B1
        public static System.Data.SqlTypes.SqlBoolean op_Implicit(System.Boolean x) { }
        // RVA: 0x0ADC88A4  token: 0x60008B2
        public static System.Boolean op_True(System.Data.SqlTypes.SqlBoolean x) { }
        // RVA: 0x0ADC86CC  token: 0x60008B3
        public static System.Data.SqlTypes.SqlBoolean op_BitwiseAnd(System.Data.SqlTypes.SqlBoolean x, System.Data.SqlTypes.SqlBoolean y) { }
        // RVA: 0x0ADC8774  token: 0x60008B4
        public static System.Data.SqlTypes.SqlBoolean op_BitwiseOr(System.Data.SqlTypes.SqlBoolean x, System.Data.SqlTypes.SqlBoolean y) { }
        // RVA: 0x0ADC8430  token: 0x60008B6
        public virtual System.String ToString() { }
        // RVA: 0x0ADC881C  token: 0x60008B7
        public static System.Data.SqlTypes.SqlBoolean op_Equality(System.Data.SqlTypes.SqlBoolean x, System.Data.SqlTypes.SqlBoolean y) { }
        // RVA: 0x0ADC7E48  token: 0x60008B8
        public static System.Data.SqlTypes.SqlBoolean And(System.Data.SqlTypes.SqlBoolean x, System.Data.SqlTypes.SqlBoolean y) { }
        // RVA: 0x0ADC8218  token: 0x60008B9
        public static System.Data.SqlTypes.SqlBoolean Or(System.Data.SqlTypes.SqlBoolean x, System.Data.SqlTypes.SqlBoolean y) { }
        // RVA: 0x0ADC7F68  token: 0x60008BA
        public virtual System.Int32 CompareTo(System.Object value) { }
        // RVA: 0x0ADC7EA4  token: 0x60008BB
        public System.Int32 CompareTo(System.Data.SqlTypes.SqlBoolean value) { }
        // RVA: 0x0ADC8054  token: 0x60008BC
        public virtual System.Boolean Equals(System.Object value) { }
        // RVA: 0x0ADC812C  token: 0x60008BD
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x011EC580  token: 0x60008BE
        private virtual System.Xml.Schema.XmlSchema System.Xml.Serialization.IXmlSerializable.GetSchema() { }
        // RVA: 0x0ADC8274  token: 0x60008BF
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader reader) { }
        // RVA: 0x0ADC834C  token: 0x60008C0
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter writer) { }
        // RVA: 0x0ADC81A0  token: 0x60008C1
        public static System.Xml.XmlQualifiedName GetXsdType(System.Xml.Schema.XmlSchemaSet schemaSet) { }
        // RVA: 0x0ADC84BC  token: 0x60008C2
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000AA  // size: 0x12
    public sealed struct SqlByte : System.Data.SqlTypes.INullable, System.IComparable, System.Xml.Serialization.IXmlSerializable
    {
        // Fields
        private System.Boolean m_fNotNull;  // 0x10
        private System.Byte m_value;  // 0x11
        private static readonly System.Int32 s_iBitNotByteMax;  // static @ 0x0
        public static readonly System.Data.SqlTypes.SqlByte Null;  // static @ 0x4
        public static readonly System.Data.SqlTypes.SqlByte Zero;  // static @ 0x6
        public static readonly System.Data.SqlTypes.SqlByte MinValue;  // static @ 0x8
        public static readonly System.Data.SqlTypes.SqlByte MaxValue;  // static @ 0xa

        // Properties
        System.Boolean IsNull { get; /* RVA: 0x0961F480 */ }
        System.Byte Value { get; /* RVA: 0x0ADC90F0 */ }

        // Methods
        // RVA: 0x015C3DA0  token: 0x60008C3
        private System.Void .ctor(System.Boolean fNull) { }
        // RVA: 0x04D9D260  token: 0x60008C4
        public System.Void .ctor(System.Byte value) { }
        // RVA: 0x04DAA200  token: 0x60008C7
        public static System.Data.SqlTypes.SqlByte op_Implicit(System.Byte x) { }
        // RVA: 0x0ADC8FE8  token: 0x60008C8
        public virtual System.String ToString() { }
        // RVA: 0x0ADC9148  token: 0x60008C9
        public static System.Data.SqlTypes.SqlByte op_Addition(System.Data.SqlTypes.SqlByte x, System.Data.SqlTypes.SqlByte y) { }
        // RVA: 0x0ADC9778  token: 0x60008CA
        public static System.Data.SqlTypes.SqlByte op_Subtraction(System.Data.SqlTypes.SqlByte x, System.Data.SqlTypes.SqlByte y) { }
        // RVA: 0x0ADC9660  token: 0x60008CB
        public static System.Data.SqlTypes.SqlByte op_Multiply(System.Data.SqlTypes.SqlByte x, System.Data.SqlTypes.SqlByte y) { }
        // RVA: 0x0ADC925C  token: 0x60008CC
        public static System.Data.SqlTypes.SqlByte op_Division(System.Data.SqlTypes.SqlByte x, System.Data.SqlTypes.SqlByte y) { }
        // RVA: 0x0ADC93F0  token: 0x60008CD
        public static System.Data.SqlTypes.SqlByte op_Explicit(System.Data.SqlTypes.SqlInt64 x) { }
        // RVA: 0x0ADC9354  token: 0x60008CE
        public static System.Data.SqlTypes.SqlBoolean op_Equality(System.Data.SqlTypes.SqlByte x, System.Data.SqlTypes.SqlByte y) { }
        // RVA: 0x0ADC95C4  token: 0x60008CF
        public static System.Data.SqlTypes.SqlBoolean op_LessThan(System.Data.SqlTypes.SqlByte x, System.Data.SqlTypes.SqlByte y) { }
        // RVA: 0x0ADC9528  token: 0x60008D0
        public static System.Data.SqlTypes.SqlBoolean op_GreaterThan(System.Data.SqlTypes.SqlByte x, System.Data.SqlTypes.SqlByte y) { }
        // RVA: 0x0ADC8D00  token: 0x60008D1
        public static System.Data.SqlTypes.SqlBoolean LessThan(System.Data.SqlTypes.SqlByte x, System.Data.SqlTypes.SqlByte y) { }
        // RVA: 0x0ADC8CA4  token: 0x60008D2
        public static System.Data.SqlTypes.SqlBoolean GreaterThan(System.Data.SqlTypes.SqlByte x, System.Data.SqlTypes.SqlByte y) { }
        // RVA: 0x0ADC8F28  token: 0x60008D3
        public System.Data.SqlTypes.SqlDouble ToSqlDouble() { }
        // RVA: 0x0ADC8F88  token: 0x60008D4
        public System.Data.SqlTypes.SqlInt64 ToSqlInt64() { }
        // RVA: 0x0ADC89E8  token: 0x60008D5
        public virtual System.Int32 CompareTo(System.Object value) { }
        // RVA: 0x0ADC88EC  token: 0x60008D6
        public System.Int32 CompareTo(System.Data.SqlTypes.SqlByte value) { }
        // RVA: 0x0ADC8AD8  token: 0x60008D7
        public virtual System.Boolean Equals(System.Object value) { }
        // RVA: 0x0ADC8BD0  token: 0x60008D8
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x011EC580  token: 0x60008D9
        private virtual System.Xml.Schema.XmlSchema System.Xml.Serialization.IXmlSerializable.GetSchema() { }
        // RVA: 0x0ADC8D5C  token: 0x60008DA
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader reader) { }
        // RVA: 0x0ADC8E38  token: 0x60008DB
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter writer) { }
        // RVA: 0x0ADC8C2C  token: 0x60008DC
        public static System.Xml.XmlQualifiedName GetXsdType(System.Xml.Schema.XmlSchemaSet schemaSet) { }
        // RVA: 0x0ADC9040  token: 0x60008DD
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000AB  // size: 0x14
    public sealed struct SqlBytesCharsState
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Data.SqlTypes.SqlBytesCharsState Null;  // const
        public static System.Data.SqlTypes.SqlBytesCharsState Buffer;  // const
        public static System.Data.SqlTypes.SqlBytesCharsState Stream;  // const

    }

    // TypeToken: 0x20000AC  // size: 0x38
    public sealed class SqlBytes : System.Data.SqlTypes.INullable, System.Xml.Serialization.IXmlSerializable, System.Runtime.Serialization.ISerializable
    {
        // Fields
        private System.Byte[] _rgbBuf;  // 0x10
        private System.Int64 _lCurLen;  // 0x18
        private System.IO.Stream _stream;  // 0x20
        private System.Data.SqlTypes.SqlBytesCharsState _state;  // 0x28
        private System.Byte[] _rgbWorkBuf;  // 0x30

        // Properties
        System.Boolean IsNull { get; /* RVA: 0x0ADC9E84 */ }
        System.Byte[] Buffer { get; /* RVA: 0x0ADC9E5C */ }
        System.Int64 Length { get; /* RVA: 0x0ADC9E8C */ }
        System.Byte[] Value { get; /* RVA: 0x0ADC9F58 */ }
        System.Data.SqlTypes.SqlBytes Null { get; /* RVA: 0x0ADC9F04 */ }

        // Methods
        // RVA: 0x0ADC9D6C  token: 0x60008DE
        public System.Void .ctor() { }
        // RVA: 0x0ADC9DF8  token: 0x60008DF
        public System.Void .ctor(System.Byte[] buffer) { }
        // RVA: 0x0ADC9D74  token: 0x60008E0
        public System.Void .ctor(System.Data.SqlTypes.SqlBinary value) { }
        // RVA: 0x0ADC9ABC  token: 0x60008E5
        public System.Void SetNull() { }
        // RVA: 0x0ADC988C  token: 0x60008E6
        private System.Void CopyStreamToBuffer() { }
        // RVA: 0x0ADC99EC  token: 0x60008E7
        private System.Boolean FStream() { }
        // RVA: 0x0ADC9A6C  token: 0x60008E8
        private System.Void SetBuffer(System.Byte[] buffer) { }
        // RVA: 0x011EC580  token: 0x60008E9
        private virtual System.Xml.Schema.XmlSchema System.Xml.Serialization.IXmlSerializable.GetSchema() { }
        // RVA: 0x0ADC9B30  token: 0x60008EA
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader r) { }
        // RVA: 0x0ADC9C6C  token: 0x60008EB
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter writer) { }
        // RVA: 0x0ADC99F4  token: 0x60008EC
        public static System.Xml.XmlQualifiedName GetXsdType(System.Xml.Schema.XmlSchemaSet schemaSet) { }
        // RVA: 0x0ADC9AE4  token: 0x60008ED
        private virtual System.Void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }

    }

    // TypeToken: 0x20000AD  // size: 0x38
    public sealed class SqlChars : System.Data.SqlTypes.INullable, System.Xml.Serialization.IXmlSerializable, System.Runtime.Serialization.ISerializable
    {
        // Fields
        private System.Char[] _rgchBuf;  // 0x10
        private System.Int64 _lCurLen;  // 0x18
        private System.Data.SqlTypes.SqlStreamChars _stream;  // 0x20
        private System.Data.SqlTypes.SqlBytesCharsState _state;  // 0x28
        private System.Char[] _rgchWorkBuf;  // 0x30

        // Properties
        System.Boolean IsNull { get; /* RVA: 0x0ADC9E84 */ }
        System.Char[] Buffer { get; /* RVA: 0x0ADCA5D8 */ }
        System.Int64 Length { get; /* RVA: 0x0ADCA600 */ }
        System.Char[] Value { get; /* RVA: 0x0ADCA6CC */ }
        System.Data.SqlTypes.SqlChars Null { get; /* RVA: 0x0ADCA678 */ }

        // Methods
        // RVA: 0x0ADC9D6C  token: 0x60008EF
        public System.Void .ctor() { }
        // RVA: 0x0ADC9DF8  token: 0x60008F0
        public System.Void .ctor(System.Char[] buffer) { }
        // RVA: 0x0ADCA548  token: 0x60008F1
        public System.Void .ctor(System.Data.SqlTypes.SqlString value) { }
        // RVA: 0x0ADC9ABC  token: 0x60008F6
        public System.Void SetNull() { }
        // RVA: 0x0ADC99EC  token: 0x60008F7
        private System.Boolean FStream() { }
        // RVA: 0x0ADCA154  token: 0x60008F8
        private System.Void CopyStreamToBuffer() { }
        // RVA: 0x0ADC9A6C  token: 0x60008F9
        private System.Void SetBuffer(System.Char[] buffer) { }
        // RVA: 0x011EC580  token: 0x60008FA
        private virtual System.Xml.Schema.XmlSchema System.Xml.Serialization.IXmlSerializable.GetSchema() { }
        // RVA: 0x0ADCA378  token: 0x60008FB
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader r) { }
        // RVA: 0x0ADCA45C  token: 0x60008FC
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter writer) { }
        // RVA: 0x0ADCA2B4  token: 0x60008FD
        public static System.Xml.XmlQualifiedName GetXsdType(System.Xml.Schema.XmlSchemaSet schemaSet) { }
        // RVA: 0x0ADCA32C  token: 0x60008FE
        private virtual System.Void System.Runtime.Serialization.ISerializable.GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) { }

    }

    // TypeToken: 0x20000AE  // size: 0x1C
    public sealed struct SqlDateTime : System.Data.SqlTypes.INullable, System.IComparable, System.Xml.Serialization.IXmlSerializable
    {
        // Fields
        private System.Boolean m_fNotNull;  // 0x10
        private System.Int32 m_day;  // 0x14
        private System.Int32 m_time;  // 0x18
        private static readonly System.Double s_SQLTicksPerMillisecond;  // static @ 0x0
        public static readonly System.Int32 SQLTicksPerSecond;  // static @ 0x8
        public static readonly System.Int32 SQLTicksPerMinute;  // static @ 0xc
        public static readonly System.Int32 SQLTicksPerHour;  // static @ 0x10
        private static readonly System.Int32 s_SQLTicksPerDay;  // static @ 0x14
        private static readonly System.Int64 s_ticksPerSecond;  // static @ 0x18
        private static readonly System.DateTime s_SQLBaseDate;  // static @ 0x20
        private static readonly System.Int64 s_SQLBaseDateTicks;  // static @ 0x28
        private static readonly System.Int32 s_minYear;  // static @ 0x30
        private static readonly System.Int32 s_maxYear;  // static @ 0x34
        private static readonly System.Int32 s_minDay;  // static @ 0x38
        private static readonly System.Int32 s_maxDay;  // static @ 0x3c
        private static readonly System.Int32 s_minTime;  // static @ 0x40
        private static readonly System.Int32 s_maxTime;  // static @ 0x44
        private static readonly System.Int32 s_dayBase;  // static @ 0x48
        private static readonly System.Int32[] s_daysToMonth365;  // static @ 0x50
        private static readonly System.Int32[] s_daysToMonth366;  // static @ 0x58
        private static readonly System.DateTime s_minDateTime;  // static @ 0x60
        private static readonly System.DateTime s_maxDateTime;  // static @ 0x68
        private static readonly System.TimeSpan s_minTimeSpan;  // static @ 0x70
        private static readonly System.TimeSpan s_maxTimeSpan;  // static @ 0x78
        private static readonly System.String s_ISO8601_DateTimeFormat;  // static @ 0x80
        private static readonly System.String[] s_dateTimeFormats;  // static @ 0x88
        public static readonly System.Data.SqlTypes.SqlDateTime MinValue;  // static @ 0x90
        public static readonly System.Data.SqlTypes.SqlDateTime MaxValue;  // static @ 0x9c
        public static readonly System.Data.SqlTypes.SqlDateTime Null;  // static @ 0xa8

        // Properties
        System.Boolean IsNull { get; /* RVA: 0x0961F480 */ }
        System.DateTime Value { get; /* RVA: 0x0ADCBD64 */ }
        System.Int32 DayTicks { get; /* RVA: 0x0ADCBCB4 */ }
        System.Int32 TimeTicks { get; /* RVA: 0x0ADCBD0C */ }

        // Methods
        // RVA: 0x04DAA210  token: 0x6000900
        private System.Void .ctor(System.Boolean fNull) { }
        // RVA: 0x0ADCBB48  token: 0x6000901
        public System.Void .ctor(System.DateTime value) { }
        // RVA: 0x0ADCBBAC  token: 0x6000902
        public System.Void .ctor(System.Int32 dayTicks, System.Int32 timeTicks) { }
        // RVA: 0x0ADCB524  token: 0x6000904
        private static System.TimeSpan ToTimeSpan(System.Data.SqlTypes.SqlDateTime value) { }
        // RVA: 0x0ADCB3EC  token: 0x6000905
        private static System.DateTime ToDateTime(System.Data.SqlTypes.SqlDateTime value) { }
        // RVA: 0x0ADCAD60  token: 0x6000906
        private static System.Data.SqlTypes.SqlDateTime FromTimeSpan(System.TimeSpan value) { }
        // RVA: 0x0ADCAC6C  token: 0x6000907
        private static System.Data.SqlTypes.SqlDateTime FromDateTime(System.DateTime value) { }
        // RVA: 0x0ADCBF5C  token: 0x600090B
        public static System.Data.SqlTypes.SqlDateTime op_Implicit(System.DateTime value) { }
        // RVA: 0x0ADCB478  token: 0x600090C
        public virtual System.String ToString() { }
        // RVA: 0x0ADCBE10  token: 0x600090D
        public static System.Data.SqlTypes.SqlBoolean op_Equality(System.Data.SqlTypes.SqlDateTime x, System.Data.SqlTypes.SqlDateTime y) { }
        // RVA: 0x0ADCBF80  token: 0x600090E
        public static System.Data.SqlTypes.SqlBoolean op_LessThan(System.Data.SqlTypes.SqlDateTime x, System.Data.SqlTypes.SqlDateTime y) { }
        // RVA: 0x0ADCBEB4  token: 0x600090F
        public static System.Data.SqlTypes.SqlBoolean op_GreaterThan(System.Data.SqlTypes.SqlDateTime x, System.Data.SqlTypes.SqlDateTime y) { }
        // RVA: 0x0ADCB090  token: 0x6000910
        public static System.Data.SqlTypes.SqlBoolean LessThan(System.Data.SqlTypes.SqlDateTime x, System.Data.SqlTypes.SqlDateTime y) { }
        // RVA: 0x0ADCB018  token: 0x6000911
        public static System.Data.SqlTypes.SqlBoolean GreaterThan(System.Data.SqlTypes.SqlDateTime x, System.Data.SqlTypes.SqlDateTime y) { }
        // RVA: 0x0ADCA8C8  token: 0x6000912
        public virtual System.Int32 CompareTo(System.Object value) { }
        // RVA: 0x0ADCA9D4  token: 0x6000913
        public System.Int32 CompareTo(System.Data.SqlTypes.SqlDateTime value) { }
        // RVA: 0x0ADCAB38  token: 0x6000914
        public virtual System.Boolean Equals(System.Object value) { }
        // RVA: 0x0ADCAF20  token: 0x6000915
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x011EC580  token: 0x6000916
        private virtual System.Xml.Schema.XmlSchema System.Xml.Serialization.IXmlSerializable.GetSchema() { }
        // RVA: 0x0ADCB108  token: 0x6000917
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader reader) { }
        // RVA: 0x0ADCB2C4  token: 0x6000918
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter writer) { }
        // RVA: 0x0ADCAFA0  token: 0x6000919
        public static System.Xml.XmlQualifiedName GetXsdType(System.Xml.Schema.XmlSchemaSet schemaSet) { }
        // RVA: 0x0ADCB598  token: 0x600091A
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000AF  // size: 0x24
    public sealed struct SqlDecimal : System.Data.SqlTypes.INullable, System.IComparable, System.Xml.Serialization.IXmlSerializable
    {
        // Fields
        private System.Byte _bStatus;  // 0x10
        private System.Byte _bLen;  // 0x11
        private System.Byte _bPrec;  // 0x12
        private System.Byte _bScale;  // 0x13
        private System.UInt32 _data1;  // 0x14
        private System.UInt32 _data2;  // 0x18
        private System.UInt32 _data3;  // 0x1c
        private System.UInt32 _data4;  // 0x20
        private static readonly System.Byte s_NUMERIC_MAX_PRECISION;  // static @ 0x0
        public static readonly System.Byte MaxPrecision;  // static @ 0x1
        public static readonly System.Byte MaxScale;  // static @ 0x2
        private static readonly System.Byte s_bNullMask;  // static @ 0x3
        private static readonly System.Byte s_bIsNull;  // static @ 0x4
        private static readonly System.Byte s_bNotNull;  // static @ 0x5
        private static readonly System.Byte s_bReverseNullMask;  // static @ 0x6
        private static readonly System.Byte s_bSignMask;  // static @ 0x7
        private static readonly System.Byte s_bPositive;  // static @ 0x8
        private static readonly System.Byte s_bNegative;  // static @ 0x9
        private static readonly System.Byte s_bReverseSignMask;  // static @ 0xa
        private static readonly System.UInt32 s_uiZero;  // static @ 0xc
        private static readonly System.Int32 s_cNumeMax;  // static @ 0x10
        private static readonly System.Int64 s_lInt32Base;  // static @ 0x18
        private static readonly System.UInt64 s_ulInt32Base;  // static @ 0x20
        private static readonly System.UInt64 s_ulInt32BaseForMod;  // static @ 0x28
        private static readonly System.UInt64 s_llMax;  // static @ 0x30
        private static readonly System.UInt32 s_ulBase10;  // static @ 0x38
        private static readonly System.Double s_DUINT_BASE;  // static @ 0x40
        private static readonly System.Double s_DUINT_BASE2;  // static @ 0x48
        private static readonly System.Double s_DUINT_BASE3;  // static @ 0x50
        private static readonly System.Double s_DMAX_NUME;  // static @ 0x58
        private static readonly System.UInt32 s_DBL_DIG;  // static @ 0x60
        private static readonly System.Byte s_cNumeDivScaleMin;  // static @ 0x64
        private static readonly System.UInt32[] s_rgulShiftBase;  // static @ 0x68
        private static readonly System.UInt32[] s_decimalHelpersLo;  // static @ 0x70
        private static readonly System.UInt32[] s_decimalHelpersMid;  // static @ 0x78
        private static readonly System.UInt32[] s_decimalHelpersHi;  // static @ 0x80
        private static readonly System.UInt32[] s_decimalHelpersHiHi;  // static @ 0x88
        private static readonly System.Byte[] s_rgCLenFromPrec;  // static @ 0x90
        private static readonly System.UInt32 s_ulT1;  // static @ 0x98
        private static readonly System.UInt32 s_ulT2;  // static @ 0x9c
        private static readonly System.UInt32 s_ulT3;  // static @ 0xa0
        private static readonly System.UInt32 s_ulT4;  // static @ 0xa4
        private static readonly System.UInt32 s_ulT5;  // static @ 0xa8
        private static readonly System.UInt32 s_ulT6;  // static @ 0xac
        private static readonly System.UInt32 s_ulT7;  // static @ 0xb0
        private static readonly System.UInt32 s_ulT8;  // static @ 0xb4
        private static readonly System.UInt32 s_ulT9;  // static @ 0xb8
        private static readonly System.UInt64 s_dwlT10;  // static @ 0xc0
        private static readonly System.UInt64 s_dwlT11;  // static @ 0xc8
        private static readonly System.UInt64 s_dwlT12;  // static @ 0xd0
        private static readonly System.UInt64 s_dwlT13;  // static @ 0xd8
        private static readonly System.UInt64 s_dwlT14;  // static @ 0xe0
        private static readonly System.UInt64 s_dwlT15;  // static @ 0xe8
        private static readonly System.UInt64 s_dwlT16;  // static @ 0xf0
        private static readonly System.UInt64 s_dwlT17;  // static @ 0xf8
        private static readonly System.UInt64 s_dwlT18;  // static @ 0x100
        private static readonly System.UInt64 s_dwlT19;  // static @ 0x108
        public static readonly System.Data.SqlTypes.SqlDecimal Null;  // static @ 0x110
        public static readonly System.Data.SqlTypes.SqlDecimal MinValue;  // static @ 0x124
        public static readonly System.Data.SqlTypes.SqlDecimal MaxValue;  // static @ 0x138

        // Properties
        System.Boolean IsNull { get; /* RVA: 0x0ADD0314 */ }
        System.Decimal Value { get; /* RVA: 0x0ADD04A4 */ }
        System.Boolean IsPositive { get; /* RVA: 0x0ADD0368 */ }
        System.Byte Scale { get; /* RVA: 0x0ADD0418 */ }
        System.Int32[] Data { get; /* RVA: 0x0ADD0230 */ }

        // Methods
        // RVA: 0x0ADCC8C0  token: 0x600091B
        private System.Byte CalculatePrecision() { }
        // RVA: 0x0ADCF3FC  token: 0x600091C
        private System.Boolean VerifyPrecision(System.Byte precision) { }
        // RVA: 0x0ADCFE6C  token: 0x600091D
        private System.Void .ctor(System.Boolean fNull) { }
        // RVA: 0x0ADD00DC  token: 0x600091E
        public System.Void .ctor(System.Decimal value) { }
        // RVA: 0x0ADCFD9C  token: 0x600091F
        public System.Void .ctor(System.Int32 value) { }
        // RVA: 0x0ADCFEC4  token: 0x6000920
        public System.Void .ctor(System.Int64 value) { }
        // RVA: 0x0ADCFF9C  token: 0x6000921
        private System.Void .ctor(System.UInt32[] rglData, System.Byte bLen, System.Byte bPrec, System.Byte bScale, System.Boolean fPositive) { }
        // RVA: 0x0ADCE958  token: 0x6000925
        private System.Void SetPositive() { }
        // RVA: 0x0ADCE9B0  token: 0x6000926
        private System.Void SetSignBit(System.Boolean fPositive) { }
        // RVA: 0x0ADCF160  token: 0x6000929
        public virtual System.String ToString() { }
        // RVA: 0x0ADCE3E4  token: 0x600092A
        public static System.Data.SqlTypes.SqlDecimal Parse(System.String s) { }
        // RVA: 0x0ADCEE78  token: 0x600092B
        public System.Double ToDouble() { }
        // RVA: 0x0ADCED1C  token: 0x600092C
        private System.Decimal ToDecimal() { }
        // RVA: 0x0ADD1164  token: 0x600092D
        public static System.Data.SqlTypes.SqlDecimal op_Implicit(System.Decimal x) { }
        // RVA: 0x0ADD1198  token: 0x600092E
        public static System.Data.SqlTypes.SqlDecimal op_Implicit(System.Int64 x) { }
        // RVA: 0x0ADD2078  token: 0x600092F
        public static System.Data.SqlTypes.SqlDecimal op_UnaryNegation(System.Data.SqlTypes.SqlDecimal x) { }
        // RVA: 0x0ADD0504  token: 0x6000930
        public static System.Data.SqlTypes.SqlDecimal op_Addition(System.Data.SqlTypes.SqlDecimal x, System.Data.SqlTypes.SqlDecimal y) { }
        // RVA: 0x0ADD1FB8  token: 0x6000931
        public static System.Data.SqlTypes.SqlDecimal op_Subtraction(System.Data.SqlTypes.SqlDecimal x, System.Data.SqlTypes.SqlDecimal y) { }
        // RVA: 0x0ADD1664  token: 0x6000932
        public static System.Data.SqlTypes.SqlDecimal op_Multiply(System.Data.SqlTypes.SqlDecimal x, System.Data.SqlTypes.SqlDecimal y) { }
        // RVA: 0x0ADD0B40  token: 0x6000933
        public static System.Data.SqlTypes.SqlDecimal op_Division(System.Data.SqlTypes.SqlDecimal x, System.Data.SqlTypes.SqlDecimal y) { }
        // RVA: 0x0ADD135C  token: 0x6000934
        public static System.Data.SqlTypes.SqlDecimal op_Implicit(System.Data.SqlTypes.SqlByte x) { }
        // RVA: 0x0ADD11E8  token: 0x6000935
        public static System.Data.SqlTypes.SqlDecimal op_Implicit(System.Data.SqlTypes.SqlInt16 x) { }
        // RVA: 0x0ADD14E0  token: 0x6000936
        public static System.Data.SqlTypes.SqlDecimal op_Implicit(System.Data.SqlTypes.SqlInt32 x) { }
        // RVA: 0x0ADD12A4  token: 0x6000937
        public static System.Data.SqlTypes.SqlDecimal op_Implicit(System.Data.SqlTypes.SqlInt64 x) { }
        // RVA: 0x0ADD1418  token: 0x6000938
        public static System.Data.SqlTypes.SqlDecimal op_Implicit(System.Data.SqlTypes.SqlMoney x) { }
        // RVA: 0x0ADCF5E4  token: 0x6000939
        private static System.Void ZeroToMaxLen(System.UInt32[] rgulData, System.Int32 cUI4sCur) { }
        // RVA: 0x0ADCD3D8  token: 0x600093A
        private System.Boolean FZero() { }
        // RVA: 0x0ADCD358  token: 0x600093B
        private System.Boolean FGt10_38() { }
        // RVA: 0x0ADCD38C  token: 0x600093C
        private System.Boolean FGt10_38(System.UInt32[] rglData) { }
        // RVA: 0x0ADCC4F0  token: 0x600093D
        private static System.Byte BGetPrecUI4(System.UInt32 value) { }
        // RVA: 0x0ADCC634  token: 0x600093E
        private static System.Byte BGetPrecUI8(System.UInt64 dwlVal) { }
        // RVA: 0x0ADCC028  token: 0x600093F
        private System.Void AddULong(System.UInt32 ulAdd) { }
        // RVA: 0x0ADCE1A8  token: 0x6000940
        private System.Void MultByULong(System.UInt32 uiMultiplier) { }
        // RVA: 0x0ADCD060  token: 0x6000941
        private System.UInt32 DivByULong(System.UInt32 iDivisor) { }
        // RVA: 0x0ADCC228  token: 0x6000942
        private System.Void AdjustScale(System.Int32 digits, System.Boolean fRound) { }
        // RVA: 0x0ADCD5E4  token: 0x6000943
        private System.Int32 LAbsCmp(System.Data.SqlTypes.SqlDecimal snumOp) { }
        // RVA: 0x0ADCE034  token: 0x6000944
        private static System.Void MpMove(System.UInt32[] rgulS, System.Int32 ciulS, System.UInt32[] rgulD, System.Int32& ciulD) { }
        // RVA: 0x0ADCE17C  token: 0x6000945
        private static System.Void MpSet(System.UInt32[] rgulD, System.Int32& ciulD, System.UInt32 iulN) { }
        // RVA: 0x0ADCE144  token: 0x6000946
        private static System.Void MpNormalize(System.UInt32[] rgulU, System.Int32& ciulU) { }
        // RVA: 0x0ADCE084  token: 0x6000947
        private static System.Void MpMul1(System.UInt32[] piulD, System.Int32& ciulD, System.UInt32 iulX) { }
        // RVA: 0x0ADCD7C8  token: 0x6000948
        private static System.Void MpDiv1(System.UInt32[] rgulU, System.Int32& ciulU, System.UInt32 iulD, System.UInt32& iulR) { }
        // RVA: 0x04DAA230  token: 0x6000949
        private static System.UInt64 DWL(System.UInt32 lo, System.UInt32 hi) { }
        // RVA: 0x04D9D8A0  token: 0x600094A
        private static System.UInt32 HI(System.UInt64 x) { }
        // RVA: 0x04D88BE0  token: 0x600094B
        private static System.UInt32 LO(System.UInt64 x) { }
        // RVA: 0x0ADCD8A0  token: 0x600094C
        private static System.Void MpDiv(System.UInt32[] rgulU, System.Int32 ciulU, System.UInt32[] rgulD, System.Int32 ciulD, System.UInt32[] rgulQ, System.Int32& ciulQ, System.UInt32[] rgulR, System.Int32& ciulR) { }
        // RVA: 0x0ADCCB70  token: 0x600094D
        private System.Data.SqlTypes.EComparison CompareNm(System.Data.SqlTypes.SqlDecimal snumOp) { }
        // RVA: 0x0ADCCAA0  token: 0x600094E
        private static System.Void CheckValidPrecScale(System.Byte bPrec, System.Byte bScale) { }
        // RVA: 0x0ADD0FCC  token: 0x600094F
        public static System.Data.SqlTypes.SqlBoolean op_Equality(System.Data.SqlTypes.SqlDecimal x, System.Data.SqlTypes.SqlDecimal y) { }
        // RVA: 0x0ADD159C  token: 0x6000950
        public static System.Data.SqlTypes.SqlBoolean op_LessThan(System.Data.SqlTypes.SqlDecimal x, System.Data.SqlTypes.SqlDecimal y) { }
        // RVA: 0x0ADD1098  token: 0x6000951
        public static System.Data.SqlTypes.SqlBoolean op_GreaterThan(System.Data.SqlTypes.SqlDecimal x, System.Data.SqlTypes.SqlDecimal y) { }
        // RVA: 0x0ADCD754  token: 0x6000952
        public static System.Data.SqlTypes.SqlBoolean LessThan(System.Data.SqlTypes.SqlDecimal x, System.Data.SqlTypes.SqlDecimal y) { }
        // RVA: 0x0ADCD570  token: 0x6000953
        public static System.Data.SqlTypes.SqlBoolean GreaterThan(System.Data.SqlTypes.SqlDecimal x, System.Data.SqlTypes.SqlDecimal y) { }
        // RVA: 0x0ADCEFF8  token: 0x6000954
        public System.Data.SqlTypes.SqlDouble ToSqlDouble() { }
        // RVA: 0x0ADCF070  token: 0x6000955
        public System.Data.SqlTypes.SqlInt64 ToSqlInt64() { }
        // RVA: 0x0ADCF0E8  token: 0x6000956
        public System.Data.SqlTypes.SqlMoney ToSqlMoney() { }
        // RVA: 0x04DAA220  token: 0x6000957
        private static System.Char ChFromDigit(System.UInt32 uiDigit) { }
        // RVA: 0x0ADCEA88  token: 0x6000958
        private System.Void StoreFromWorkingArray(System.UInt32[] rguiData) { }
        // RVA: 0x0ADCEA24  token: 0x6000959
        private System.Void SetToZero() { }
        // RVA: 0x0ADCCF58  token: 0x600095A
        public virtual System.Int32 CompareTo(System.Object value) { }
        // RVA: 0x0ADCCDE8  token: 0x600095B
        public System.Int32 CompareTo(System.Data.SqlTypes.SqlDecimal value) { }
        // RVA: 0x0ADCD208  token: 0x600095C
        public virtual System.Boolean Equals(System.Object value) { }
        // RVA: 0x0ADCD3EC  token: 0x600095D
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x011EC580  token: 0x600095E
        private virtual System.Xml.Schema.XmlSchema System.Xml.Serialization.IXmlSerializable.GetSchema() { }
        // RVA: 0x0ADCEAD4  token: 0x600095F
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader reader) { }
        // RVA: 0x0ADCEC34  token: 0x6000960
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter writer) { }
        // RVA: 0x0ADCD4F8  token: 0x6000961
        public static System.Xml.XmlQualifiedName GetXsdType(System.Xml.Schema.XmlSchemaSet schemaSet) { }
        // RVA: 0x0ADCF654  token: 0x6000962
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000B0  // size: 0x20
    public sealed struct SqlDouble : System.Data.SqlTypes.INullable, System.IComparable, System.Xml.Serialization.IXmlSerializable
    {
        // Fields
        private System.Boolean m_fNotNull;  // 0x10
        private System.Double m_value;  // 0x18
        public static readonly System.Data.SqlTypes.SqlDouble Null;  // static @ 0x0
        public static readonly System.Data.SqlTypes.SqlDouble Zero;  // static @ 0x10
        public static readonly System.Data.SqlTypes.SqlDouble MinValue;  // static @ 0x20
        public static readonly System.Data.SqlTypes.SqlDouble MaxValue;  // static @ 0x30

        // Properties
        System.Boolean IsNull { get; /* RVA: 0x0961F480 */ }
        System.Double Value { get; /* RVA: 0x0ADD2A58 */ }

        // Methods
        // RVA: 0x04DAA240  token: 0x6000963
        private System.Void .ctor(System.Boolean fNull) { }
        // RVA: 0x0ADD29CC  token: 0x6000964
        public System.Void .ctor(System.Double value) { }
        // RVA: 0x0ADD3080  token: 0x6000967
        public static System.Data.SqlTypes.SqlDouble op_Implicit(System.Double x) { }
        // RVA: 0x0ADD2894  token: 0x6000968
        public virtual System.String ToString() { }
        // RVA: 0x0ADD3628  token: 0x6000969
        public static System.Data.SqlTypes.SqlDouble op_UnaryNegation(System.Data.SqlTypes.SqlDouble x) { }
        // RVA: 0x0ADD2AB4  token: 0x600096A
        public static System.Data.SqlTypes.SqlDouble op_Addition(System.Data.SqlTypes.SqlDouble x, System.Data.SqlTypes.SqlDouble y) { }
        // RVA: 0x0ADD3510  token: 0x600096B
        public static System.Data.SqlTypes.SqlDouble op_Subtraction(System.Data.SqlTypes.SqlDouble x, System.Data.SqlTypes.SqlDouble y) { }
        // RVA: 0x0ADD33F8  token: 0x600096C
        public static System.Data.SqlTypes.SqlDouble op_Multiply(System.Data.SqlTypes.SqlDouble x, System.Data.SqlTypes.SqlDouble y) { }
        // RVA: 0x0ADD2BCC  token: 0x600096D
        public static System.Data.SqlTypes.SqlDouble op_Division(System.Data.SqlTypes.SqlDouble x, System.Data.SqlTypes.SqlDouble y) { }
        // RVA: 0x0ADD32B0  token: 0x600096E
        public static System.Data.SqlTypes.SqlDouble op_Implicit(System.Data.SqlTypes.SqlByte x) { }
        // RVA: 0x0ADD3200  token: 0x600096F
        public static System.Data.SqlTypes.SqlDouble op_Implicit(System.Data.SqlTypes.SqlInt16 x) { }
        // RVA: 0x0ADD30A0  token: 0x6000970
        public static System.Data.SqlTypes.SqlDouble op_Implicit(System.Data.SqlTypes.SqlInt32 x) { }
        // RVA: 0x0ADD2E80  token: 0x6000971
        public static System.Data.SqlTypes.SqlDouble op_Implicit(System.Data.SqlTypes.SqlInt64 x) { }
        // RVA: 0x0ADD3150  token: 0x6000972
        public static System.Data.SqlTypes.SqlDouble op_Implicit(System.Data.SqlTypes.SqlSingle x) { }
        // RVA: 0x0ADD2FDC  token: 0x6000973
        public static System.Data.SqlTypes.SqlDouble op_Implicit(System.Data.SqlTypes.SqlMoney x) { }
        // RVA: 0x0ADD2F2C  token: 0x6000974
        public static System.Data.SqlTypes.SqlDouble op_Implicit(System.Data.SqlTypes.SqlDecimal x) { }
        // RVA: 0x0ADD2D48  token: 0x6000975
        public static System.Data.SqlTypes.SqlBoolean op_Equality(System.Data.SqlTypes.SqlDouble x, System.Data.SqlTypes.SqlDouble y) { }
        // RVA: 0x0ADD3364  token: 0x6000976
        public static System.Data.SqlTypes.SqlBoolean op_LessThan(System.Data.SqlTypes.SqlDouble x, System.Data.SqlTypes.SqlDouble y) { }
        // RVA: 0x0ADD2DEC  token: 0x6000977
        public static System.Data.SqlTypes.SqlBoolean op_GreaterThan(System.Data.SqlTypes.SqlDouble x, System.Data.SqlTypes.SqlDouble y) { }
        // RVA: 0x0ADD25F4  token: 0x6000978
        public static System.Data.SqlTypes.SqlBoolean LessThan(System.Data.SqlTypes.SqlDouble x, System.Data.SqlTypes.SqlDouble y) { }
        // RVA: 0x0ADD258C  token: 0x6000979
        public static System.Data.SqlTypes.SqlBoolean GreaterThan(System.Data.SqlTypes.SqlDouble x, System.Data.SqlTypes.SqlDouble y) { }
        // RVA: 0x0ADD2834  token: 0x600097A
        public System.Data.SqlTypes.SqlSingle ToSqlSingle() { }
        // RVA: 0x0ADD2164  token: 0x600097B
        public virtual System.Int32 CompareTo(System.Object value) { }
        // RVA: 0x0ADD2264  token: 0x600097C
        public System.Int32 CompareTo(System.Data.SqlTypes.SqlDouble value) { }
        // RVA: 0x0ADD239C  token: 0x600097D
        public virtual System.Boolean Equals(System.Object value) { }
        // RVA: 0x0ADD24AC  token: 0x600097E
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x011EC580  token: 0x600097F
        private virtual System.Xml.Schema.XmlSchema System.Xml.Serialization.IXmlSerializable.GetSchema() { }
        // RVA: 0x0ADD265C  token: 0x6000980
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader reader) { }
        // RVA: 0x0ADD2738  token: 0x6000981
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter writer) { }
        // RVA: 0x0ADD2514  token: 0x6000982
        public static System.Xml.XmlQualifiedName GetXsdType(System.Xml.Schema.XmlSchemaSet schemaSet) { }
        // RVA: 0x0ADD28EC  token: 0x6000983
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000B1  // size: 0x18
    public sealed struct SqlGuid : System.Data.SqlTypes.INullable, System.IComparable, System.Xml.Serialization.IXmlSerializable
    {
        // Fields
        private static readonly System.Int32 s_sizeOfGuid;  // static @ 0x0
        private static readonly System.Int32[] s_rgiGuidOrder;  // static @ 0x8
        private System.Byte[] m_value;  // 0x10
        public static readonly System.Data.SqlTypes.SqlGuid Null;  // static @ 0x10

        // Properties
        System.Boolean IsNull { get; /* RVA: 0x0314CD60 */ }
        System.Guid Value { get; /* RVA: 0x0ADD3F10 */ }

        // Methods
        // RVA: 0x05395148  token: 0x6000984
        private System.Void .ctor(System.Boolean fNull) { }
        // RVA: 0x0ADD3EE8  token: 0x6000985
        public System.Void .ctor(System.Guid g) { }
        // RVA: 0x0ADD4104  token: 0x6000988
        public static System.Data.SqlTypes.SqlGuid op_Implicit(System.Guid x) { }
        // RVA: 0x0ADD3D98  token: 0x6000989
        public virtual System.String ToString() { }
        // RVA: 0x0ADD38A0  token: 0x600098A
        private static System.Data.SqlTypes.EComparison Compare(System.Data.SqlTypes.SqlGuid x, System.Data.SqlTypes.SqlGuid y) { }
        // RVA: 0x0ADD3FB4  token: 0x600098B
        public static System.Data.SqlTypes.SqlBoolean op_Equality(System.Data.SqlTypes.SqlGuid x, System.Data.SqlTypes.SqlGuid y) { }
        // RVA: 0x0ADD4138  token: 0x600098C
        public static System.Data.SqlTypes.SqlBoolean op_LessThan(System.Data.SqlTypes.SqlGuid x, System.Data.SqlTypes.SqlGuid y) { }
        // RVA: 0x0ADD405C  token: 0x600098D
        public static System.Data.SqlTypes.SqlBoolean op_GreaterThan(System.Data.SqlTypes.SqlGuid x, System.Data.SqlTypes.SqlGuid y) { }
        // RVA: 0x0ADD36B4  token: 0x600098E
        public virtual System.Int32 CompareTo(System.Object value) { }
        // RVA: 0x0ADD37A4  token: 0x600098F
        public System.Int32 CompareTo(System.Data.SqlTypes.SqlGuid value) { }
        // RVA: 0x0ADD3998  token: 0x6000990
        public virtual System.Boolean Equals(System.Object value) { }
        // RVA: 0x0ADD3A90  token: 0x6000991
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x011EC580  token: 0x6000992
        private virtual System.Xml.Schema.XmlSchema System.Xml.Serialization.IXmlSerializable.GetSchema() { }
        // RVA: 0x0ADD3B88  token: 0x6000993
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader reader) { }
        // RVA: 0x0ADD3C80  token: 0x6000994
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter writer) { }
        // RVA: 0x0ADD3B10  token: 0x6000995
        public static System.Xml.XmlQualifiedName GetXsdType(System.Xml.Schema.XmlSchemaSet schemaSet) { }
        // RVA: 0x0ADD3E04  token: 0x6000996
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000B2  // size: 0x14
    public sealed struct SqlInt16 : System.Data.SqlTypes.INullable, System.IComparable, System.Xml.Serialization.IXmlSerializable
    {
        // Fields
        private System.Boolean m_fNotNull;  // 0x10
        private System.Int16 m_value;  // 0x12
        private static readonly System.Int32 s_MASKI2;  // static @ 0x0
        public static readonly System.Data.SqlTypes.SqlInt16 Null;  // static @ 0x4
        public static readonly System.Data.SqlTypes.SqlInt16 Zero;  // static @ 0x8
        public static readonly System.Data.SqlTypes.SqlInt16 MinValue;  // static @ 0xc
        public static readonly System.Data.SqlTypes.SqlInt16 MaxValue;  // static @ 0x10

        // Properties
        System.Boolean IsNull { get; /* RVA: 0x0961F480 */ }
        System.Int16 Value { get; /* RVA: 0x0ADD49D4 */ }

        // Methods
        // RVA: 0x04DAA250  token: 0x6000997
        private System.Void .ctor(System.Boolean fNull) { }
        // RVA: 0x04D9D4B0  token: 0x6000998
        public System.Void .ctor(System.Int16 value) { }
        // RVA: 0x04DAA260  token: 0x600099B
        public static System.Data.SqlTypes.SqlInt16 op_Implicit(System.Int16 x) { }
        // RVA: 0x0ADD48C4  token: 0x600099C
        public virtual System.String ToString() { }
        // RVA: 0x0ADD521C  token: 0x600099D
        public static System.Data.SqlTypes.SqlInt16 op_UnaryNegation(System.Data.SqlTypes.SqlInt16 x) { }
        // RVA: 0x0ADD4A30  token: 0x600099E
        public static System.Data.SqlTypes.SqlInt16 op_Addition(System.Data.SqlTypes.SqlInt16 x, System.Data.SqlTypes.SqlInt16 y) { }
        // RVA: 0x0ADD511C  token: 0x600099F
        public static System.Data.SqlTypes.SqlInt16 op_Subtraction(System.Data.SqlTypes.SqlInt16 x, System.Data.SqlTypes.SqlInt16 y) { }
        // RVA: 0x0ADD4FEC  token: 0x60009A0
        public static System.Data.SqlTypes.SqlInt16 op_Multiply(System.Data.SqlTypes.SqlInt16 x, System.Data.SqlTypes.SqlInt16 y) { }
        // RVA: 0x0ADD4B30  token: 0x60009A1
        public static System.Data.SqlTypes.SqlInt16 op_Division(System.Data.SqlTypes.SqlInt16 x, System.Data.SqlTypes.SqlInt16 y) { }
        // RVA: 0x0ADD4EBC  token: 0x60009A2
        public static System.Data.SqlTypes.SqlInt16 op_Implicit(System.Data.SqlTypes.SqlByte x) { }
        // RVA: 0x0ADD4D24  token: 0x60009A3
        public static System.Data.SqlTypes.SqlInt16 op_Explicit(System.Data.SqlTypes.SqlInt64 x) { }
        // RVA: 0x0ADD4C8C  token: 0x60009A4
        public static System.Data.SqlTypes.SqlBoolean op_Equality(System.Data.SqlTypes.SqlInt16 x, System.Data.SqlTypes.SqlInt16 y) { }
        // RVA: 0x0ADD4F54  token: 0x60009A5
        public static System.Data.SqlTypes.SqlBoolean op_LessThan(System.Data.SqlTypes.SqlInt16 x, System.Data.SqlTypes.SqlInt16 y) { }
        // RVA: 0x0ADD4E24  token: 0x60009A6
        public static System.Data.SqlTypes.SqlBoolean op_GreaterThan(System.Data.SqlTypes.SqlInt16 x, System.Data.SqlTypes.SqlInt16 y) { }
        // RVA: 0x0ADD45E0  token: 0x60009A7
        public static System.Data.SqlTypes.SqlBoolean LessThan(System.Data.SqlTypes.SqlInt16 x, System.Data.SqlTypes.SqlInt16 y) { }
        // RVA: 0x0ADD4588  token: 0x60009A8
        public static System.Data.SqlTypes.SqlBoolean GreaterThan(System.Data.SqlTypes.SqlInt16 x, System.Data.SqlTypes.SqlInt16 y) { }
        // RVA: 0x0ADD4804  token: 0x60009A9
        public System.Data.SqlTypes.SqlDouble ToSqlDouble() { }
        // RVA: 0x0ADD4864  token: 0x60009AA
        public System.Data.SqlTypes.SqlInt64 ToSqlInt64() { }
        // RVA: 0x0ADD41DC  token: 0x60009AB
        public virtual System.Int32 CompareTo(System.Object value) { }
        // RVA: 0x0ADD42C8  token: 0x60009AC
        public System.Int32 CompareTo(System.Data.SqlTypes.SqlInt16 value) { }
        // RVA: 0x0ADD43B8  token: 0x60009AD
        public virtual System.Boolean Equals(System.Object value) { }
        // RVA: 0x0ADD44AC  token: 0x60009AE
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x011EC580  token: 0x60009AF
        private virtual System.Xml.Schema.XmlSchema System.Xml.Serialization.IXmlSerializable.GetSchema() { }
        // RVA: 0x0ADD4638  token: 0x60009B0
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader reader) { }
        // RVA: 0x0ADD4714  token: 0x60009B1
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter writer) { }
        // RVA: 0x0ADD4510  token: 0x60009B2
        public static System.Xml.XmlQualifiedName GetXsdType(System.Xml.Schema.XmlSchemaSet schemaSet) { }
        // RVA: 0x0ADD491C  token: 0x60009B3
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000B3  // size: 0x18
    public sealed struct SqlInt32 : System.Data.SqlTypes.INullable, System.IComparable, System.Xml.Serialization.IXmlSerializable
    {
        // Fields
        private System.Boolean m_fNotNull;  // 0x10
        private System.Int32 m_value;  // 0x14
        private static readonly System.Int64 s_iIntMin;  // static @ 0x0
        private static readonly System.Int64 s_lBitNotIntMax;  // static @ 0x8
        public static readonly System.Data.SqlTypes.SqlInt32 Null;  // static @ 0x10
        public static readonly System.Data.SqlTypes.SqlInt32 Zero;  // static @ 0x18
        public static readonly System.Data.SqlTypes.SqlInt32 MinValue;  // static @ 0x20
        public static readonly System.Data.SqlTypes.SqlInt32 MaxValue;  // static @ 0x28

        // Properties
        System.Boolean IsNull { get; /* RVA: 0x0961F480 */ }
        System.Int32 Value { get; /* RVA: 0x0ADD5ACC */ }

        // Methods
        // RVA: 0x04DAA280  token: 0x60009B4
        private System.Void .ctor(System.Boolean fNull) { }
        // RVA: 0x04D9D420  token: 0x60009B5
        public System.Void .ctor(System.Int32 value) { }
        // RVA: 0x04DAA290  token: 0x60009B8
        public static System.Data.SqlTypes.SqlInt32 op_Implicit(System.Int32 x) { }
        // RVA: 0x0ADD59A0  token: 0x60009B9
        public virtual System.String ToString() { }
        // RVA: 0x0ADD6450  token: 0x60009BA
        public static System.Data.SqlTypes.SqlInt32 op_UnaryNegation(System.Data.SqlTypes.SqlInt32 x) { }
        // RVA: 0x0ADD5B50  token: 0x60009BB
        public static System.Data.SqlTypes.SqlInt32 op_Addition(System.Data.SqlTypes.SqlInt32 x, System.Data.SqlTypes.SqlInt32 y) { }
        // RVA: 0x0ADD6330  token: 0x60009BC
        public static System.Data.SqlTypes.SqlInt32 op_Subtraction(System.Data.SqlTypes.SqlInt32 x, System.Data.SqlTypes.SqlInt32 y) { }
        // RVA: 0x0ADD61F4  token: 0x60009BD
        public static System.Data.SqlTypes.SqlInt32 op_Multiply(System.Data.SqlTypes.SqlInt32 x, System.Data.SqlTypes.SqlInt32 y) { }
        // RVA: 0x0ADD5C74  token: 0x60009BE
        public static System.Data.SqlTypes.SqlInt32 op_Division(System.Data.SqlTypes.SqlInt32 x, System.Data.SqlTypes.SqlInt32 y) { }
        // RVA: 0x0ADD6024  token: 0x60009BF
        public static System.Data.SqlTypes.SqlInt32 op_Implicit(System.Data.SqlTypes.SqlByte x) { }
        // RVA: 0x0ADD60C0  token: 0x60009C0
        public static System.Data.SqlTypes.SqlInt32 op_Implicit(System.Data.SqlTypes.SqlInt16 x) { }
        // RVA: 0x0ADD5E84  token: 0x60009C1
        public static System.Data.SqlTypes.SqlInt32 op_Explicit(System.Data.SqlTypes.SqlInt64 x) { }
        // RVA: 0x0ADD5704  token: 0x60009C2
        private static System.Boolean SameSignInt(System.Int32 x, System.Int32 y) { }
        // RVA: 0x0ADD5DE8  token: 0x60009C3
        public static System.Data.SqlTypes.SqlBoolean op_Equality(System.Data.SqlTypes.SqlInt32 x, System.Data.SqlTypes.SqlInt32 y) { }
        // RVA: 0x0ADD6158  token: 0x60009C4
        public static System.Data.SqlTypes.SqlBoolean op_LessThan(System.Data.SqlTypes.SqlInt32 x, System.Data.SqlTypes.SqlInt32 y) { }
        // RVA: 0x0ADD5F88  token: 0x60009C5
        public static System.Data.SqlTypes.SqlBoolean op_GreaterThan(System.Data.SqlTypes.SqlInt32 x, System.Data.SqlTypes.SqlInt32 y) { }
        // RVA: 0x0ADD56A8  token: 0x60009C6
        public static System.Data.SqlTypes.SqlBoolean LessThan(System.Data.SqlTypes.SqlInt32 x, System.Data.SqlTypes.SqlInt32 y) { }
        // RVA: 0x0ADD564C  token: 0x60009C7
        public static System.Data.SqlTypes.SqlBoolean GreaterThan(System.Data.SqlTypes.SqlInt32 x, System.Data.SqlTypes.SqlInt32 y) { }
        // RVA: 0x0ADD58E0  token: 0x60009C8
        public System.Data.SqlTypes.SqlDouble ToSqlDouble() { }
        // RVA: 0x0ADD5940  token: 0x60009C9
        public System.Data.SqlTypes.SqlInt64 ToSqlInt64() { }
        // RVA: 0x0ADD5390  token: 0x60009CA
        public virtual System.Int32 CompareTo(System.Object value) { }
        // RVA: 0x0ADD5294  token: 0x60009CB
        public System.Int32 CompareTo(System.Data.SqlTypes.SqlInt32 value) { }
        // RVA: 0x0ADD5480  token: 0x60009CC
        public virtual System.Boolean Equals(System.Object value) { }
        // RVA: 0x0ADD5578  token: 0x60009CD
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x011EC580  token: 0x60009CE
        private virtual System.Xml.Schema.XmlSchema System.Xml.Serialization.IXmlSerializable.GetSchema() { }
        // RVA: 0x0ADD5714  token: 0x60009CF
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader reader) { }
        // RVA: 0x0ADD57F0  token: 0x60009D0
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter writer) { }
        // RVA: 0x0ADD55D4  token: 0x60009D1
        public static System.Xml.XmlQualifiedName GetXsdType(System.Xml.Schema.XmlSchemaSet schemaSet) { }
        // RVA: 0x0ADD59F8  token: 0x60009D2
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000B4  // size: 0x20
    public sealed struct SqlInt64 : System.Data.SqlTypes.INullable, System.IComparable, System.Xml.Serialization.IXmlSerializable
    {
        // Fields
        private System.Boolean m_fNotNull;  // 0x10
        private System.Int64 m_value;  // 0x18
        private static readonly System.Int64 s_lLowIntMask;  // static @ 0x0
        private static readonly System.Int64 s_lHighIntMask;  // static @ 0x8
        public static readonly System.Data.SqlTypes.SqlInt64 Null;  // static @ 0x10
        public static readonly System.Data.SqlTypes.SqlInt64 Zero;  // static @ 0x20
        public static readonly System.Data.SqlTypes.SqlInt64 MinValue;  // static @ 0x30
        public static readonly System.Data.SqlTypes.SqlInt64 MaxValue;  // static @ 0x40

        // Properties
        System.Boolean IsNull { get; /* RVA: 0x0961F480 */ }
        System.Int64 Value { get; /* RVA: 0x0ADD6EF4 */ }

        // Methods
        // RVA: 0x04DAA240  token: 0x60009D3
        private System.Void .ctor(System.Boolean fNull) { }
        // RVA: 0x04D9D3E0  token: 0x60009D4
        public System.Void .ctor(System.Int64 value) { }
        // RVA: 0x04DAA2B0  token: 0x60009D7
        public static System.Data.SqlTypes.SqlInt64 op_Implicit(System.Int64 x) { }
        // RVA: 0x0ADD6DA8  token: 0x60009D8
        public virtual System.String ToString() { }
        // RVA: 0x0ADD7D80  token: 0x60009D9
        public static System.Data.SqlTypes.SqlInt64 op_UnaryNegation(System.Data.SqlTypes.SqlInt64 x) { }
        // RVA: 0x0ADD6F50  token: 0x60009DA
        public static System.Data.SqlTypes.SqlInt64 op_Addition(System.Data.SqlTypes.SqlInt64 x, System.Data.SqlTypes.SqlInt64 y) { }
        // RVA: 0x0ADD7C4C  token: 0x60009DB
        public static System.Data.SqlTypes.SqlInt64 op_Subtraction(System.Data.SqlTypes.SqlInt64 x, System.Data.SqlTypes.SqlInt64 y) { }
        // RVA: 0x0ADD793C  token: 0x60009DC
        public static System.Data.SqlTypes.SqlInt64 op_Multiply(System.Data.SqlTypes.SqlInt64 x, System.Data.SqlTypes.SqlInt64 y) { }
        // RVA: 0x0ADD7084  token: 0x60009DD
        public static System.Data.SqlTypes.SqlInt64 op_Division(System.Data.SqlTypes.SqlInt64 x, System.Data.SqlTypes.SqlInt64 y) { }
        // RVA: 0x0ADD77D4  token: 0x60009DE
        public static System.Data.SqlTypes.SqlInt64 op_Modulus(System.Data.SqlTypes.SqlInt64 x, System.Data.SqlTypes.SqlInt64 y) { }
        // RVA: 0x0ADD7698  token: 0x60009DF
        public static System.Data.SqlTypes.SqlInt64 op_Implicit(System.Data.SqlTypes.SqlByte x) { }
        // RVA: 0x0ADD75F0  token: 0x60009E0
        public static System.Data.SqlTypes.SqlInt64 op_Implicit(System.Data.SqlTypes.SqlInt16 x) { }
        // RVA: 0x0ADD7548  token: 0x60009E1
        public static System.Data.SqlTypes.SqlInt64 op_Implicit(System.Data.SqlTypes.SqlInt32 x) { }
        // RVA: 0x0ADD7280  token: 0x60009E2
        public static System.Data.SqlTypes.SqlInt64 op_Explicit(System.Data.SqlTypes.SqlDecimal x) { }
        // RVA: 0x0ADD69BC  token: 0x60009E3
        private static System.Boolean SameSignLong(System.Int64 x, System.Int64 y) { }
        // RVA: 0x0ADD71EC  token: 0x60009E4
        public static System.Data.SqlTypes.SqlBoolean op_Equality(System.Data.SqlTypes.SqlInt64 x, System.Data.SqlTypes.SqlInt64 y) { }
        // RVA: 0x0ADD7740  token: 0x60009E5
        public static System.Data.SqlTypes.SqlBoolean op_LessThan(System.Data.SqlTypes.SqlInt64 x, System.Data.SqlTypes.SqlInt64 y) { }
        // RVA: 0x0ADD74B4  token: 0x60009E6
        public static System.Data.SqlTypes.SqlBoolean op_GreaterThan(System.Data.SqlTypes.SqlInt64 x, System.Data.SqlTypes.SqlInt64 y) { }
        // RVA: 0x0ADD6954  token: 0x60009E7
        public static System.Data.SqlTypes.SqlBoolean LessThan(System.Data.SqlTypes.SqlInt64 x, System.Data.SqlTypes.SqlInt64 y) { }
        // RVA: 0x0ADD68EC  token: 0x60009E8
        public static System.Data.SqlTypes.SqlBoolean GreaterThan(System.Data.SqlTypes.SqlInt64 x, System.Data.SqlTypes.SqlInt64 y) { }
        // RVA: 0x0ADD6B9C  token: 0x60009E9
        public System.Data.SqlTypes.SqlByte ToSqlByte() { }
        // RVA: 0x0ADD6C74  token: 0x60009EA
        public System.Data.SqlTypes.SqlDouble ToSqlDouble() { }
        // RVA: 0x0ADD6CE8  token: 0x60009EB
        public System.Data.SqlTypes.SqlInt16 ToSqlInt16() { }
        // RVA: 0x0ADD6D48  token: 0x60009EC
        public System.Data.SqlTypes.SqlInt32 ToSqlInt32() { }
        // RVA: 0x0ADD6BFC  token: 0x60009ED
        public System.Data.SqlTypes.SqlDecimal ToSqlDecimal() { }
        // RVA: 0x0ADD6604  token: 0x60009EE
        public virtual System.Int32 CompareTo(System.Object value) { }
        // RVA: 0x0ADD64CC  token: 0x60009EF
        public System.Int32 CompareTo(System.Data.SqlTypes.SqlInt64 value) { }
        // RVA: 0x0ADD6704  token: 0x60009F0
        public virtual System.Boolean Equals(System.Object value) { }
        // RVA: 0x0ADD6814  token: 0x60009F1
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x011EC580  token: 0x60009F2
        private virtual System.Xml.Schema.XmlSchema System.Xml.Serialization.IXmlSerializable.GetSchema() { }
        // RVA: 0x0ADD69D0  token: 0x60009F3
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader reader) { }
        // RVA: 0x0ADD6AAC  token: 0x60009F4
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter writer) { }
        // RVA: 0x0ADD6874  token: 0x60009F5
        public static System.Xml.XmlQualifiedName GetXsdType(System.Xml.Schema.XmlSchemaSet schemaSet) { }
        // RVA: 0x0ADD6E00  token: 0x60009F6
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000B5  // size: 0x20
    public sealed struct SqlMoney : System.Data.SqlTypes.INullable, System.IComparable, System.Xml.Serialization.IXmlSerializable
    {
        // Fields
        private System.Boolean _fNotNull;  // 0x10
        private System.Int64 _value;  // 0x18
        private static readonly System.Int32 s_iMoneyScale;  // static @ 0x0
        private static readonly System.Int64 s_lTickBase;  // static @ 0x8
        private static readonly System.Double s_dTickBase;  // static @ 0x10
        private static readonly System.Int64 s_minLong;  // static @ 0x18
        private static readonly System.Int64 s_maxLong;  // static @ 0x20
        public static readonly System.Data.SqlTypes.SqlMoney Null;  // static @ 0x28
        public static readonly System.Data.SqlTypes.SqlMoney Zero;  // static @ 0x38
        public static readonly System.Data.SqlTypes.SqlMoney MinValue;  // static @ 0x48
        public static readonly System.Data.SqlTypes.SqlMoney MaxValue;  // static @ 0x58

        // Properties
        System.Boolean IsNull { get; /* RVA: 0x0961F480 */ }
        System.Decimal Value { get; /* RVA: 0x0ADD8CA0 */ }

        // Methods
        // RVA: 0x04DAA240  token: 0x60009F7
        private System.Void .ctor(System.Boolean fNull) { }
        // RVA: 0x04D9D3E0  token: 0x60009F8
        private System.Void .ctor(System.Int64 value, System.Int32 ignored) { }
        // RVA: 0x0ADD8A74  token: 0x60009F9
        public System.Void .ctor(System.Int32 value) { }
        // RVA: 0x0ADD8990  token: 0x60009FA
        public System.Void .ctor(System.Int64 value) { }
        // RVA: 0x0ADD8AD4  token: 0x60009FB
        public System.Void .ctor(System.Decimal value) { }
        // RVA: 0x0ADD852C  token: 0x60009FE
        public System.Decimal ToDecimal() { }
        // RVA: 0x0ADD8628  token: 0x60009FF
        public System.Double ToDouble() { }
        // RVA: 0x0ADD92AC  token: 0x6000A00
        public static System.Data.SqlTypes.SqlMoney op_Implicit(System.Decimal x) { }
        // RVA: 0x0ADD9434  token: 0x6000A01
        public static System.Data.SqlTypes.SqlMoney op_Implicit(System.Int64 x) { }
        // RVA: 0x0ADD8794  token: 0x6000A02
        public virtual System.String ToString() { }
        // RVA: 0x0ADD9884  token: 0x6000A03
        public static System.Data.SqlTypes.SqlMoney op_UnaryNegation(System.Data.SqlTypes.SqlMoney x) { }
        // RVA: 0x0ADD8D48  token: 0x6000A04
        public static System.Data.SqlTypes.SqlMoney op_Addition(System.Data.SqlTypes.SqlMoney x, System.Data.SqlTypes.SqlMoney y) { }
        // RVA: 0x0ADD96F0  token: 0x6000A05
        public static System.Data.SqlTypes.SqlMoney op_Subtraction(System.Data.SqlTypes.SqlMoney x, System.Data.SqlTypes.SqlMoney y) { }
        // RVA: 0x0ADD95B8  token: 0x6000A06
        public static System.Data.SqlTypes.SqlMoney op_Multiply(System.Data.SqlTypes.SqlMoney x, System.Data.SqlTypes.SqlMoney y) { }
        // RVA: 0x0ADD8EE0  token: 0x6000A07
        public static System.Data.SqlTypes.SqlMoney op_Division(System.Data.SqlTypes.SqlMoney x, System.Data.SqlTypes.SqlMoney y) { }
        // RVA: 0x0ADD9388  token: 0x6000A08
        public static System.Data.SqlTypes.SqlMoney op_Implicit(System.Data.SqlTypes.SqlByte x) { }
        // RVA: 0x0ADD9200  token: 0x6000A09
        public static System.Data.SqlTypes.SqlMoney op_Implicit(System.Data.SqlTypes.SqlInt16 x) { }
        // RVA: 0x0ADD92DC  token: 0x6000A0A
        public static System.Data.SqlTypes.SqlMoney op_Implicit(System.Data.SqlTypes.SqlInt32 x) { }
        // RVA: 0x0ADD947C  token: 0x6000A0B
        public static System.Data.SqlTypes.SqlMoney op_Implicit(System.Data.SqlTypes.SqlInt64 x) { }
        // RVA: 0x0ADD90AC  token: 0x6000A0C
        public static System.Data.SqlTypes.SqlMoney op_Explicit(System.Data.SqlTypes.SqlDecimal x) { }
        // RVA: 0x0ADD9018  token: 0x6000A0D
        public static System.Data.SqlTypes.SqlBoolean op_Equality(System.Data.SqlTypes.SqlMoney x, System.Data.SqlTypes.SqlMoney y) { }
        // RVA: 0x0ADD9524  token: 0x6000A0E
        public static System.Data.SqlTypes.SqlBoolean op_LessThan(System.Data.SqlTypes.SqlMoney x, System.Data.SqlTypes.SqlMoney y) { }
        // RVA: 0x0ADD916C  token: 0x6000A0F
        public static System.Data.SqlTypes.SqlBoolean op_GreaterThan(System.Data.SqlTypes.SqlMoney x, System.Data.SqlTypes.SqlMoney y) { }
        // RVA: 0x0ADD8278  token: 0x6000A10
        public static System.Data.SqlTypes.SqlBoolean LessThan(System.Data.SqlTypes.SqlMoney x, System.Data.SqlTypes.SqlMoney y) { }
        // RVA: 0x0ADD8210  token: 0x6000A11
        public static System.Data.SqlTypes.SqlBoolean GreaterThan(System.Data.SqlTypes.SqlMoney x, System.Data.SqlTypes.SqlMoney y) { }
        // RVA: 0x0ADD8720  token: 0x6000A12
        public System.Data.SqlTypes.SqlDouble ToSqlDouble() { }
        // RVA: 0x0ADD86A8  token: 0x6000A13
        public System.Data.SqlTypes.SqlDecimal ToSqlDecimal() { }
        // RVA: 0x0ADD7F3C  token: 0x6000A14
        public virtual System.Int32 CompareTo(System.Object value) { }
        // RVA: 0x0ADD7E04  token: 0x6000A15
        public System.Int32 CompareTo(System.Data.SqlTypes.SqlMoney value) { }
        // RVA: 0x0ADD803C  token: 0x6000A16
        public virtual System.Boolean Equals(System.Object value) { }
        // RVA: 0x0ADD814C  token: 0x6000A17
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x011EC580  token: 0x6000A18
        private virtual System.Xml.Schema.XmlSchema System.Xml.Serialization.IXmlSerializable.GetSchema() { }
        // RVA: 0x0ADD82E0  token: 0x6000A19
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader reader) { }
        // RVA: 0x0ADD8410  token: 0x6000A1A
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter writer) { }
        // RVA: 0x0ADD8198  token: 0x6000A1B
        public static System.Xml.XmlQualifiedName GetXsdType(System.Xml.Schema.XmlSchemaSet schemaSet) { }
        // RVA: 0x0ADD883C  token: 0x6000A1C
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000B6  // size: 0x18
    public sealed struct SqlSingle : System.Data.SqlTypes.INullable, System.IComparable, System.Xml.Serialization.IXmlSerializable
    {
        // Fields
        private System.Boolean _fNotNull;  // 0x10
        private System.Single _value;  // 0x14
        public static readonly System.Data.SqlTypes.SqlSingle Null;  // static @ 0x0
        public static readonly System.Data.SqlTypes.SqlSingle Zero;  // static @ 0x8
        public static readonly System.Data.SqlTypes.SqlSingle MinValue;  // static @ 0x10
        public static readonly System.Data.SqlTypes.SqlSingle MaxValue;  // static @ 0x18

        // Properties
        System.Boolean IsNull { get; /* RVA: 0x0961F480 */ }
        System.Single Value { get; /* RVA: 0x0ADDA398 */ }

        // Methods
        // RVA: 0x04DAA280  token: 0x6000A1D
        private System.Void .ctor(System.Boolean fNull) { }
        // RVA: 0x0ADDA30C  token: 0x6000A1E
        public System.Void .ctor(System.Single value) { }
        // RVA: 0x0ADDA2B4  token: 0x6000A1F
        public System.Void .ctor(System.Double value) { }
        // RVA: 0x0ADDAB88  token: 0x6000A22
        public static System.Data.SqlTypes.SqlSingle op_Implicit(System.Single x) { }
        // RVA: 0x0ADDA184  token: 0x6000A23
        public virtual System.String ToString() { }
        // RVA: 0x0ADDAF24  token: 0x6000A24
        public static System.Data.SqlTypes.SqlSingle op_UnaryNegation(System.Data.SqlTypes.SqlSingle x) { }
        // RVA: 0x0ADDA3F4  token: 0x6000A25
        public static System.Data.SqlTypes.SqlSingle op_Addition(System.Data.SqlTypes.SqlSingle x, System.Data.SqlTypes.SqlSingle y) { }
        // RVA: 0x0ADDAE0C  token: 0x6000A26
        public static System.Data.SqlTypes.SqlSingle op_Subtraction(System.Data.SqlTypes.SqlSingle x, System.Data.SqlTypes.SqlSingle y) { }
        // RVA: 0x0ADDACF4  token: 0x6000A27
        public static System.Data.SqlTypes.SqlSingle op_Multiply(System.Data.SqlTypes.SqlSingle x, System.Data.SqlTypes.SqlSingle y) { }
        // RVA: 0x0ADDA50C  token: 0x6000A28
        public static System.Data.SqlTypes.SqlSingle op_Division(System.Data.SqlTypes.SqlSingle x, System.Data.SqlTypes.SqlSingle y) { }
        // RVA: 0x0ADDAAE4  token: 0x6000A29
        public static System.Data.SqlTypes.SqlSingle op_Implicit(System.Data.SqlTypes.SqlByte x) { }
        // RVA: 0x0ADDAA44  token: 0x6000A2A
        public static System.Data.SqlTypes.SqlSingle op_Implicit(System.Data.SqlTypes.SqlInt16 x) { }
        // RVA: 0x0ADDABB0  token: 0x6000A2B
        public static System.Data.SqlTypes.SqlSingle op_Implicit(System.Data.SqlTypes.SqlInt32 x) { }
        // RVA: 0x0ADDA9A4  token: 0x6000A2C
        public static System.Data.SqlTypes.SqlSingle op_Implicit(System.Data.SqlTypes.SqlInt64 x) { }
        // RVA: 0x0ADDA868  token: 0x6000A2D
        public static System.Data.SqlTypes.SqlSingle op_Implicit(System.Data.SqlTypes.SqlMoney x) { }
        // RVA: 0x0ADDA900  token: 0x6000A2E
        public static System.Data.SqlTypes.SqlSingle op_Implicit(System.Data.SqlTypes.SqlDecimal x) { }
        // RVA: 0x0ADDA730  token: 0x6000A2F
        public static System.Data.SqlTypes.SqlSingle op_Explicit(System.Data.SqlTypes.SqlDouble x) { }
        // RVA: 0x0ADDA684  token: 0x6000A30
        public static System.Data.SqlTypes.SqlBoolean op_Equality(System.Data.SqlTypes.SqlSingle x, System.Data.SqlTypes.SqlSingle y) { }
        // RVA: 0x0ADDAC54  token: 0x6000A31
        public static System.Data.SqlTypes.SqlBoolean op_LessThan(System.Data.SqlTypes.SqlSingle x, System.Data.SqlTypes.SqlSingle y) { }
        // RVA: 0x0ADDA7C8  token: 0x6000A32
        public static System.Data.SqlTypes.SqlBoolean op_GreaterThan(System.Data.SqlTypes.SqlSingle x, System.Data.SqlTypes.SqlSingle y) { }
        // RVA: 0x0ADD9EF0  token: 0x6000A33
        public static System.Data.SqlTypes.SqlBoolean LessThan(System.Data.SqlTypes.SqlSingle x, System.Data.SqlTypes.SqlSingle y) { }
        // RVA: 0x0ADD9E94  token: 0x6000A34
        public static System.Data.SqlTypes.SqlBoolean GreaterThan(System.Data.SqlTypes.SqlSingle x, System.Data.SqlTypes.SqlSingle y) { }
        // RVA: 0x0ADDA124  token: 0x6000A35
        public System.Data.SqlTypes.SqlDouble ToSqlDouble() { }
        // RVA: 0x0ADD9BCC  token: 0x6000A36
        public virtual System.Int32 CompareTo(System.Object value) { }
        // RVA: 0x0ADD9AD0  token: 0x6000A37
        public System.Int32 CompareTo(System.Data.SqlTypes.SqlSingle value) { }
        // RVA: 0x0ADD9CBC  token: 0x6000A38
        public virtual System.Boolean Equals(System.Object value) { }
        // RVA: 0x0ADD9DB4  token: 0x6000A39
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x011EC580  token: 0x6000A3A
        private virtual System.Xml.Schema.XmlSchema System.Xml.Serialization.IXmlSerializable.GetSchema() { }
        // RVA: 0x0ADD9F4C  token: 0x6000A3B
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader reader) { }
        // RVA: 0x0ADDA028  token: 0x6000A3C
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter writer) { }
        // RVA: 0x0ADD9E1C  token: 0x6000A3D
        public static System.Xml.XmlQualifiedName GetXsdType(System.Xml.Schema.XmlSchemaSet schemaSet) { }
        // RVA: 0x0ADDA1DC  token: 0x6000A3E
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000B7  // size: 0x14
    public sealed struct SqlCompareOptions
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Data.SqlTypes.SqlCompareOptions None;  // const
        public static System.Data.SqlTypes.SqlCompareOptions IgnoreCase;  // const
        public static System.Data.SqlTypes.SqlCompareOptions IgnoreNonSpace;  // const
        public static System.Data.SqlTypes.SqlCompareOptions IgnoreKanaType;  // const
        public static System.Data.SqlTypes.SqlCompareOptions IgnoreWidth;  // const
        public static System.Data.SqlTypes.SqlCompareOptions BinarySort;  // const
        public static System.Data.SqlTypes.SqlCompareOptions BinarySort2;  // const

    }

    // TypeToken: 0x20000B8  // size: 0x30
    public sealed struct SqlString : System.Data.SqlTypes.INullable, System.IComparable, System.Xml.Serialization.IXmlSerializable
    {
        // Fields
        private System.String m_value;  // 0x10
        private System.Globalization.CompareInfo m_cmpInfo;  // 0x18
        private System.Int32 m_lcid;  // 0x20
        private System.Data.SqlTypes.SqlCompareOptions m_flag;  // 0x24
        private System.Boolean m_fNotNull;  // 0x28
        public static readonly System.Data.SqlTypes.SqlString Null;  // static @ 0x0
        private static readonly System.Text.UnicodeEncoding s_unicodeEncoding;  // static @ 0x20
        public static readonly System.Int32 IgnoreCase;  // static @ 0x28
        public static readonly System.Int32 IgnoreWidth;  // static @ 0x2c
        public static readonly System.Int32 IgnoreNonSpace;  // static @ 0x30
        public static readonly System.Int32 IgnoreKanaType;  // static @ 0x34
        public static readonly System.Int32 BinarySort;  // static @ 0x38
        public static readonly System.Int32 BinarySort2;  // static @ 0x3c
        private static readonly System.Data.SqlTypes.SqlCompareOptions s_iDefaultFlag;  // static @ 0x40
        private static readonly System.Globalization.CompareOptions s_iValidCompareOptionMask;  // static @ 0x44
        private static readonly System.Data.SqlTypes.SqlCompareOptions s_iValidSqlCompareOptionMask;  // static @ 0x48
        private static readonly System.Int32 s_lcidUSEnglish;  // static @ 0x4c
        private static readonly System.Int32 s_lcidBinary;  // static @ 0x50

        // Properties
        System.Boolean IsNull { get; /* RVA: 0x0AA1EEF0 */ }
        System.String Value { get; /* RVA: 0x0ADDC3F0 */ }

        // Methods
        // RVA: 0x0ADDC3C4  token: 0x6000A3F
        private System.Void .ctor(System.Boolean fNull) { }
        // RVA: 0x0ADDC1E4  token: 0x6000A40
        public System.Void .ctor(System.String data, System.Int32 lcid, System.Data.SqlTypes.SqlCompareOptions compareOptions) { }
        // RVA: 0x0ADDC274  token: 0x6000A41
        public System.Void .ctor(System.String data) { }
        // RVA: 0x0ADDC314  token: 0x6000A42
        private System.Void .ctor(System.Int32 lcid, System.Data.SqlTypes.SqlCompareOptions compareOptions, System.String data, System.Globalization.CompareInfo cmpInfo) { }
        // RVA: 0x0ADDBA68  token: 0x6000A45
        private System.Void SetCompareInfo() { }
        // RVA: 0x0ADDC668  token: 0x6000A46
        public static System.Data.SqlTypes.SqlString op_Implicit(System.String x) { }
        // RVA: 0x0ADDBF3C  token: 0x6000A47
        public virtual System.String ToString() { }
        // RVA: 0x0ADDC474  token: 0x6000A48
        public static System.Data.SqlTypes.SqlString op_Addition(System.Data.SqlTypes.SqlString x, System.Data.SqlTypes.SqlString y) { }
        // RVA: 0x0ADDBAE0  token: 0x6000A49
        private static System.Int32 StringCompare(System.Data.SqlTypes.SqlString x, System.Data.SqlTypes.SqlString y) { }
        // RVA: 0x0ADDB550  token: 0x6000A4A
        private static System.Data.SqlTypes.SqlBoolean Compare(System.Data.SqlTypes.SqlString x, System.Data.SqlTypes.SqlString y, System.Data.SqlTypes.EComparison ecExpectedResult) { }
        // RVA: 0x0ADDC5EC  token: 0x6000A4B
        public static System.Data.SqlTypes.SqlBoolean op_Equality(System.Data.SqlTypes.SqlString x, System.Data.SqlTypes.SqlString y) { }
        // RVA: 0x0ADDBF88  token: 0x6000A4C
        private static System.Void ValidateSqlCompareOptions(System.Data.SqlTypes.SqlCompareOptions compareOptions) { }
        // RVA: 0x0ADDB2B0  token: 0x6000A4D
        public static System.Globalization.CompareOptions CompareOptionsFromSqlCompareOptions(System.Data.SqlTypes.SqlCompareOptions compareOptions) { }
        // RVA: 0x0ADDB7B8  token: 0x6000A4E
        private System.Boolean FBinarySort() { }
        // RVA: 0x0ADDB0F0  token: 0x6000A4F
        private static System.Int32 CompareBinary(System.Data.SqlTypes.SqlString x, System.Data.SqlTypes.SqlString y) { }
        // RVA: 0x0ADDAFA8  token: 0x6000A50
        private static System.Int32 CompareBinary2(System.Data.SqlTypes.SqlString x, System.Data.SqlTypes.SqlString y) { }
        // RVA: 0x0ADDB364  token: 0x6000A51
        public virtual System.Int32 CompareTo(System.Object value) { }
        // RVA: 0x0ADDB478  token: 0x6000A52
        public System.Int32 CompareTo(System.Data.SqlTypes.SqlString value) { }
        // RVA: 0x0ADDB664  token: 0x6000A53
        public virtual System.Boolean Equals(System.Object value) { }
        // RVA: 0x0ADDB804  token: 0x6000A54
        public virtual System.Int32 GetHashCode() { }
        // RVA: 0x011EC580  token: 0x6000A55
        private virtual System.Xml.Schema.XmlSchema System.Xml.Serialization.IXmlSerializable.GetSchema() { }
        // RVA: 0x0ADDBDA4  token: 0x6000A56
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader reader) { }
        // RVA: 0x0ADDBE70  token: 0x6000A57
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter writer) { }
        // RVA: 0x0ADDB9F0  token: 0x6000A58
        public static System.Xml.XmlQualifiedName GetXsdType(System.Xml.Schema.XmlSchemaSet schemaSet) { }
        // RVA: 0x0ADDC02C  token: 0x6000A59
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000B9  // size: 0x14
    public sealed struct EComparison
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static System.Data.SqlTypes.EComparison LT;  // const
        public static System.Data.SqlTypes.EComparison LE;  // const
        public static System.Data.SqlTypes.EComparison EQ;  // const
        public static System.Data.SqlTypes.EComparison GE;  // const
        public static System.Data.SqlTypes.EComparison GT;  // const
        public static System.Data.SqlTypes.EComparison NE;  // const

    }

    // TypeToken: 0x20000BA  // size: 0x90
    public class SqlTypeException : System.SystemException
    {
        // Methods
        // RVA: 0x0ADDC8E8  token: 0x6000A5A
        public System.Void .ctor() { }
        // RVA: 0x0ADDC8DC  token: 0x6000A5B
        public System.Void .ctor(System.String message) { }
        // RVA: 0x0ADDC8BC  token: 0x6000A5C
        public System.Void .ctor(System.String message, System.Exception e) { }
        // RVA: 0x0ADDC928  token: 0x6000A5D
        protected System.Void .ctor(System.Runtime.Serialization.SerializationInfo si, System.Runtime.Serialization.StreamingContext sc) { }
        // RVA: 0x0ADDC7F8  token: 0x6000A5E
        private static System.Runtime.Serialization.SerializationInfo SqlTypeExceptionSerialization(System.Runtime.Serialization.SerializationInfo si, System.Runtime.Serialization.StreamingContext sc) { }

    }

    // TypeToken: 0x20000BB  // size: 0x90
    public sealed class SqlNullValueException : System.Data.SqlTypes.SqlTypeException
    {
        // Methods
        // RVA: 0x0ADD9A28  token: 0x6000A5F
        public System.Void .ctor() { }
        // RVA: 0x0ADD9AC4  token: 0x6000A60
        public System.Void .ctor(System.String message) { }
        // RVA: 0x0ADD9A50  token: 0x6000A61
        public System.Void .ctor(System.String message, System.Exception e) { }
        // RVA: 0x0ADD9A70  token: 0x6000A62
        private System.Void .ctor(System.Runtime.Serialization.SerializationInfo si, System.Runtime.Serialization.StreamingContext sc) { }
        // RVA: 0x0ADD9964  token: 0x6000A63
        private static System.Runtime.Serialization.SerializationInfo SqlNullValueExceptionSerialization(System.Runtime.Serialization.SerializationInfo si, System.Runtime.Serialization.StreamingContext sc) { }

    }

    // TypeToken: 0x20000BC  // size: 0x90
    public sealed class SqlTruncateException : System.Data.SqlTypes.SqlTypeException
    {
        // Methods
        // RVA: 0x0ADDC770  token: 0x6000A64
        public System.Void .ctor() { }
        // RVA: 0x0ADDC7EC  token: 0x6000A65
        public System.Void .ctor(System.String message) { }
        // RVA: 0x0ADDC750  token: 0x6000A66
        public System.Void .ctor(System.String message, System.Exception e) { }
        // RVA: 0x0ADDC798  token: 0x6000A67
        private System.Void .ctor(System.Runtime.Serialization.SerializationInfo si, System.Runtime.Serialization.StreamingContext sc) { }
        // RVA: 0x0ADDC68C  token: 0x6000A68
        private static System.Runtime.Serialization.SerializationInfo SqlTruncateExceptionSerialization(System.Runtime.Serialization.SerializationInfo si, System.Runtime.Serialization.StreamingContext sc) { }

    }

    // TypeToken: 0x20000BD  // size: 0x10
    public abstract class SqlStreamChars
    {
        // Properties
        System.Int64 Length { get; /* RVA: -1  // abstract */ }
        System.Int64 Position { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000A6B
        public virtual System.Int32 Read(System.Char[] buffer, System.Int32 offset, System.Int32 count) { }
        // RVA: -1  // abstract  token: 0x6000A6C
        public virtual System.Int64 Seek(System.Int64 offset, System.IO.SeekOrigin origin) { }

    }

    // TypeToken: 0x20000BE  // size: 0x30
    public sealed class SqlXml : System.Data.SqlTypes.INullable, System.Xml.Serialization.IXmlSerializable
    {
        // Fields
        private static readonly System.Func<System.IO.Stream,System.Xml.XmlReaderSettings,System.Xml.XmlParserContext,System.Xml.XmlReader> s_sqlReaderDelegate;  // static @ 0x0
        private static readonly System.Xml.XmlReaderSettings s_defaultXmlReaderSettings;  // static @ 0x8
        private static readonly System.Xml.XmlReaderSettings s_defaultXmlReaderSettingsCloseInput;  // static @ 0x10
        private static System.Reflection.MethodInfo s_createSqlReaderMethodInfo;  // static @ 0x18
        private System.Reflection.MethodInfo _createSqlReaderMethodInfo;  // 0x10
        private System.Boolean _fNotNull;  // 0x18
        private System.IO.Stream _stream;  // 0x20
        private System.Boolean _firstCreateReader;  // 0x28

        // Properties
        System.Reflection.MethodInfo CreateSqlReaderMethodInfo { get; /* RVA: 0x0ADDDFA8 */ }
        System.Boolean IsNull { get; /* RVA: 0x0AA1EEF0 */ }

        // Methods
        // RVA: 0x0ADDDFA0  token: 0x6000A6D
        public System.Void .ctor() { }
        // RVA: 0x0ADDD7AC  token: 0x6000A6E
        public System.Xml.XmlReader CreateReader() { }
        // RVA: 0x0ADDD9A8  token: 0x6000A6F
        private static System.Xml.XmlReader CreateSqlXmlReader(System.IO.Stream stream, System.Boolean closeInput, System.Boolean throwTargetInvocationExceptions) { }
        // RVA: 0x0ADDD8F4  token: 0x6000A70
        private static System.Func<System.IO.Stream,System.Xml.XmlReaderSettings,System.Xml.XmlParserContext,System.Xml.XmlReader> CreateSqlReaderDelegate() { }
        // RVA: 0x0ADDDBA8  token: 0x6000A73
        private System.Void SetNull() { }
        // RVA: 0x011EC580  token: 0x6000A74
        private virtual System.Xml.Schema.XmlSchema System.Xml.Serialization.IXmlSerializable.GetSchema() { }
        // RVA: 0x0ADDDBD0  token: 0x6000A75
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader r) { }
        // RVA: 0x0ADDDD6C  token: 0x6000A76
        private virtual System.Void System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter writer) { }
        // RVA: 0x0ADDDB30  token: 0x6000A77
        public static System.Xml.XmlQualifiedName GetXsdType(System.Xml.Schema.XmlSchemaSet schemaSet) { }
        // RVA: 0x0ADDDE78  token: 0x6000A78
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x20000BF  // size: 0x40
    public sealed class SqlXmlStreamWrapper : System.IO.Stream
    {
        // Fields
        private System.IO.Stream _stream;  // 0x28
        private System.Int64 _lPosition;  // 0x30
        private System.Boolean _isClosed;  // 0x38

        // Properties
        System.Boolean CanRead { get; /* RVA: 0x0ADDD578 */ }
        System.Boolean CanSeek { get; /* RVA: 0x0ADDD5B0 */ }
        System.Boolean CanWrite { get; /* RVA: 0x0ADDD5E8 */ }
        System.Int64 Length { get; /* RVA: 0x0ADDD620 */ }
        System.Int64 Position { get; /* RVA: 0x0ADDD688 */ set; /* RVA: 0x0ADDD6DC */ }

        // Methods
        // RVA: 0x0ADDD51C  token: 0x6000A79
        private System.Void .ctor(System.IO.Stream stream) { }
        // RVA: 0x0ADDCD70  token: 0x6000A80
        public virtual System.Int64 Seek(System.Int64 offset, System.IO.SeekOrigin origin) { }
        // RVA: 0x0ADDCB58  token: 0x6000A81
        public virtual System.Int32 Read(System.Byte[] buffer, System.Int32 offset, System.Int32 count) { }
        // RVA: 0x0ADDD308  token: 0x6000A82
        public virtual System.Void Write(System.Byte[] buffer, System.Int32 offset, System.Int32 count) { }
        // RVA: 0x0ADDCA58  token: 0x6000A83
        public virtual System.Int32 ReadByte() { }
        // RVA: 0x0ADDD240  token: 0x6000A84
        public virtual System.Void WriteByte(System.Byte value) { }
        // RVA: 0x0ADDCFB8  token: 0x6000A85
        public virtual System.Void SetLength(System.Int64 value) { }
        // RVA: 0x0ADDC9D0  token: 0x6000A86
        public virtual System.Void Flush() { }
        // RVA: 0x0ADDC97C  token: 0x6000A87
        protected virtual System.Void Dispose(System.Boolean disposing) { }
        // RVA: 0x0ADDD0C0  token: 0x6000A88
        private System.Void ThrowIfStreamCannotSeek(System.String method) { }
        // RVA: 0x0ADDD03C  token: 0x6000A89
        private System.Void ThrowIfStreamCannotRead(System.String method) { }
        // RVA: 0x0ADDD144  token: 0x6000A8A
        private System.Void ThrowIfStreamCannotWrite(System.String method) { }
        // RVA: 0x0ADDD1C8  token: 0x6000A8B
        private System.Void ThrowIfStreamClosed(System.String method) { }
        // RVA: 0x0ADDC9EC  token: 0x6000A8C
        private System.Boolean IsStreamClosed() { }

    }

}

namespace System.Runtime.CompilerServices
{

    // TypeToken: 0x2000003  // size: 0x18
    public sealed class NullableAttribute : System.Attribute
    {
        // Fields
        public readonly System.Byte[] NullableFlags;  // 0x10

        // Methods
        // RVA: 0x0AD602E4  token: 0x6000002
        public System.Void .ctor(System.Byte ) { }

    }

    // TypeToken: 0x2000004  // size: 0x18
    public sealed class NullableContextAttribute : System.Attribute
    {
        // Fields
        public readonly System.Byte Flag;  // 0x10

        // Methods
        // RVA: 0x04D86130  token: 0x6000003
        public System.Void .ctor(System.Byte ) { }

    }

}

