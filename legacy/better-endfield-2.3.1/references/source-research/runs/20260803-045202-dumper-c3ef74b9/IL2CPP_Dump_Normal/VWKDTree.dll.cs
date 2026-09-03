// ========================================================
// Dumped by @desirepro
// Assembly: VWKDTree.dll
// Classes:  13
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

namespace DataStructures.ViliWonka.Heap
{

    // TypeToken: 0x2000007  // size: 0x28
    public abstract class BaseHeap
    {
        // Fields
        protected System.Int32 nodesCount;  // 0x10
        protected System.Int32 maxSize;  // 0x14
        protected System.Single[] heap;  // 0x18
        protected System.Single tempHeap;  // 0x20

        // Properties
        System.Int32 Count { get; /* RVA: 0x02B2E2D0 */ }
        System.Single HeadValue { get; /* RVA: 0x0B89B3BC */ }

        // Methods
        // RVA: 0x0B89B368  token: 0x600002A
        protected System.Void .ctor(System.Int32 initialSize) { }
        // RVA: 0x04D91EC0  token: 0x600002D
        public System.Void Clear() { }
        // RVA: 0x04DBF650  token: 0x600002E
        protected System.Int32 Parent(System.Int32 index) { }
        // RVA: 0x04DBF640  token: 0x600002F
        protected System.Int32 Left(System.Int32 index) { }
        // RVA: 0x04DBF660  token: 0x6000030
        protected System.Int32 Right(System.Int32 index) { }
        // RVA: 0x0B89AE18  token: 0x6000031
        protected System.Void BubbleDownMax(System.Int32 index) { }
        // RVA: 0x0B89B0A8  token: 0x6000032
        protected System.Void BubbleUpMax(System.Int32 index) { }
        // RVA: 0x0B89AF60  token: 0x6000033
        protected System.Void BubbleDownMin(System.Int32 index) { }
        // RVA: 0x0B89B114  token: 0x6000034
        protected System.Void BubbleUpMin(System.Int32 index) { }
        // RVA: 0x0B89B2A4  token: 0x6000035
        protected virtual System.Void Swap(System.Int32 A, System.Int32 B) { }
        // RVA: 0x0B89B320  token: 0x6000036
        protected virtual System.Void UpsizeHeap() { }
        // RVA: 0x0B89B258  token: 0x6000037
        public virtual System.Void PushValue(System.Single h) { }
        // RVA: 0x0B89B20C  token: 0x6000038
        public virtual System.Single PopValue() { }
        // RVA: 0x0B89B180  token: 0x6000039
        public System.Void FlushHeapResult(System.Collections.Generic.List<System.Single> heapList) { }

    }

    // TypeToken: 0x2000008  // size: 0x28
    public class KSmallestHeap : DataStructures.ViliWonka.Heap.BaseHeap
    {
        // Properties
        System.Boolean Full { get; /* RVA: 0x0B89E0C0 */ }

        // Methods
        // RVA: 0x0B89E0B8  token: 0x600003A
        public System.Void .ctor(System.Int32 maxEntries) { }
        // RVA: 0x0B89E024  token: 0x600003C
        public virtual System.Void PushValue(System.Single h) { }
        // RVA: 0x0B89DED0  token: 0x600003D
        public virtual System.Single PopValue() { }
        // RVA: 0x0B89DF88  token: 0x600003E
        public System.Void Print() { }
        // RVA: 0x0B89DC5C  token: 0x600003F
        public System.Boolean HeapPropertyHolds(System.Int32 index, System.Int32 depth) { }

    }

    // TypeToken: 0x2000009
    public class KSmallestHeap`1 : DataStructures.ViliWonka.Heap.KSmallestHeap
    {
        // Fields
        private T[] objs;  // 0x0
        private T tempObjs;  // 0x0

        // Properties
        T HeadHeapObject { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000040
        public System.Void .ctor(System.Int32 maxEntries) { }
        // RVA: -1  // not resolved  token: 0x6000042
        protected virtual System.Void Swap(System.Int32 A, System.Int32 B) { }
        // RVA: -1  // not resolved  token: 0x6000043
        public virtual System.Void PushValue(System.Single h) { }
        // RVA: -1  // not resolved  token: 0x6000044
        public System.Void PushObj(T obj, System.Single h) { }
        // RVA: -1  // not resolved  token: 0x6000045
        public virtual System.Single PopValue() { }
        // RVA: -1  // not resolved  token: 0x6000046
        public T PopObj() { }
        // RVA: -1  // not resolved  token: 0x6000047
        public T PopObj(System.Single& heapValue) { }
        // RVA: -1  // not resolved  token: 0x6000048
        public System.Void FlushResult(System.Collections.Generic.List<T> resultList, System.Collections.Generic.List<System.Single> heapList) { }

    }

    // TypeToken: 0x200000A  // size: 0x28
    public class MaxHeap : DataStructures.ViliWonka.Heap.BaseHeap
    {
        // Methods
        // RVA: 0x0B89E0B8  token: 0x6000049
        public System.Void .ctor(System.Int32 initialSize) { }
        // RVA: 0x0B89E184  token: 0x600004A
        public virtual System.Void PushValue(System.Single h) { }
        // RVA: 0x0B89E0CC  token: 0x600004B
        public virtual System.Single PopValue() { }

    }

    // TypeToken: 0x200000B
    public class MaxHeap`1 : DataStructures.ViliWonka.Heap.MaxHeap
    {
        // Fields
        private T[] objs;  // 0x0
        private T tempObjs;  // 0x0

        // Properties
        T HeadHeapObject { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600004C
        public System.Void .ctor(System.Int32 maxNodes) { }
        // RVA: -1  // not resolved  token: 0x600004E
        protected virtual System.Void Swap(System.Int32 A, System.Int32 B) { }
        // RVA: -1  // not resolved  token: 0x600004F
        public virtual System.Void PushValue(System.Single h) { }
        // RVA: -1  // not resolved  token: 0x6000050
        public virtual System.Single PopValue() { }
        // RVA: -1  // not resolved  token: 0x6000051
        public System.Void PushObj(T obj, System.Single h) { }
        // RVA: -1  // not resolved  token: 0x6000052
        public T PopObj() { }
        // RVA: -1  // not resolved  token: 0x6000053
        public T PopObj(System.Single& heapValue) { }
        // RVA: -1  // not resolved  token: 0x6000054
        protected virtual System.Void UpsizeHeap() { }
        // RVA: -1  // not resolved  token: 0x6000055
        public System.Void FlushResult(System.Collections.Generic.List<T> resultList, System.Collections.Generic.List<System.Single> heapList) { }

    }

    // TypeToken: 0x200000C  // size: 0x28
    public class MinHeap : DataStructures.ViliWonka.Heap.BaseHeap
    {
        // Methods
        // RVA: 0x0B89E0B8  token: 0x6000056
        public System.Void .ctor(System.Int32 initialSize) { }
        // RVA: 0x0B89E2A8  token: 0x6000057
        public virtual System.Void PushValue(System.Single h) { }
        // RVA: 0x0B89E1F0  token: 0x6000058
        public virtual System.Single PopValue() { }

    }

    // TypeToken: 0x200000D
    public class MinHeap`1 : DataStructures.ViliWonka.Heap.MinHeap
    {
        // Fields
        private T[] objs;  // 0x0
        private T tempObjs;  // 0x0

        // Properties
        T HeadHeapObject { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000059
        public System.Void .ctor(System.Int32 maxNodes) { }
        // RVA: -1  // not resolved  token: 0x600005B
        protected virtual System.Void Swap(System.Int32 A, System.Int32 B) { }
        // RVA: -1  // not resolved  token: 0x600005C
        public virtual System.Void PushValue(System.Single h) { }
        // RVA: -1  // not resolved  token: 0x600005D
        public virtual System.Single PopValue() { }
        // RVA: -1  // not resolved  token: 0x600005E
        public System.Void PushObj(T obj, System.Single h) { }
        // RVA: -1  // not resolved  token: 0x600005F
        public T PopObj() { }
        // RVA: -1  // not resolved  token: 0x6000060
        public T PopObj(System.Single& heapValue) { }
        // RVA: -1  // not resolved  token: 0x6000061
        protected virtual System.Void UpsizeHeap() { }
        // RVA: -1  // not resolved  token: 0x6000062
        public System.Void FlushResult(System.Collections.Generic.List<T> resultList, System.Collections.Generic.List<System.Single> heapList) { }

    }

}

namespace DataStructures.ViliWonka.KDTree
{

    // TypeToken: 0x2000002  // size: 0x28
    public sealed struct KDBounds
    {
        // Fields
        public UnityEngine.Vector3 min;  // 0x10
        public UnityEngine.Vector3 max;  // 0x1c

        // Properties
        UnityEngine.Vector3 size { get; /* RVA: 0x0B89B550 */ }
        UnityEngine.Bounds Bounds { get; /* RVA: 0x0B89B464 */ }

        // Methods
        // RVA: 0x0B89B3E8  token: 0x6000003
        public UnityEngine.Vector3 ClosestPoint(UnityEngine.Vector3 point) { }

    }

    // TypeToken: 0x2000003  // size: 0x48
    public class KDNode
    {
        // Fields
        public System.Single partitionCoordinate;  // 0x10
        public System.Int32 partitionAxis;  // 0x14
        public DataStructures.ViliWonka.KDTree.KDNode negativeChild;  // 0x18
        public DataStructures.ViliWonka.KDTree.KDNode positiveChild;  // 0x20
        public System.Int32 start;  // 0x28
        public System.Int32 end;  // 0x2c
        public DataStructures.ViliWonka.KDTree.KDBounds bounds;  // 0x30

        // Properties
        System.Int32 Count { get; /* RVA: 0x04DBF670 */ }
        System.Boolean Leaf { get; /* RVA: 0x09300170 */ }

        // Methods
        // RVA: 0x04D8CCE0  token: 0x6000006
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000004  // size: 0x30
    public class KDQuery
    {
        // Fields
        protected DataStructures.ViliWonka.KDTree.KDQueryNode[] queueArray;  // 0x10
        protected DataStructures.ViliWonka.Heap.MinHeap<DataStructures.ViliWonka.KDTree.KDQueryNode> minHeap;  // 0x18
        protected System.Int32 count;  // 0x20
        protected System.Int32 queryIndex;  // 0x24
        private System.Collections.Generic.SortedList<System.Int32,DataStructures.ViliWonka.Heap.KSmallestHeap<System.Int32>> _heaps;  // 0x28

        // Properties
        System.Int32 LeftToProcess { get; /* RVA: 0x04DBF680 */ }

        // Methods
        // RVA: 0x0B89C4A4  token: 0x6000007
        private DataStructures.ViliWonka.KDTree.KDQueryNode PushGetQueue() { }
        // RVA: 0x0B89C698  token: 0x6000008
        protected System.Void PushToQueue(DataStructures.ViliWonka.KDTree.KDNode node, UnityEngine.Vector3 tempClosestPoint) { }
        // RVA: 0x0B89C59C  token: 0x6000009
        protected System.Void PushToHeap(DataStructures.ViliWonka.KDTree.KDNode node, UnityEngine.Vector3 tempClosestPoint, UnityEngine.Vector3 queryPosition) { }
        // RVA: 0x0B89C470  token: 0x600000B
        protected DataStructures.ViliWonka.KDTree.KDQueryNode PopFromQueue() { }
        // RVA: 0x0B89C3DC  token: 0x600000C
        protected DataStructures.ViliWonka.KDTree.KDQueryNode PopFromHeap() { }
        // RVA: 0x0B89CAB8  token: 0x600000D
        protected System.Void Reset() { }
        // RVA: 0x0B89CADC  token: 0x600000E
        public System.Void .ctor(System.Int32 queryNodesContainersInitialSize) { }
        // RVA: 0x0B89B990  token: 0x600000F
        public System.Void DrawLastQuery() { }
        // RVA: 0x0B89B5A8  token: 0x6000010
        public System.Void ClosestPoint(DataStructures.ViliWonka.KDTree.KDTree tree, UnityEngine.Vector3 queryPosition, System.Collections.Generic.List<System.Int32> resultIndices, System.Collections.Generic.List<System.Single> resultDistances) { }
        // RVA: 0x0B89BB0C  token: 0x6000011
        public System.Void Interval(DataStructures.ViliWonka.KDTree.KDTree tree, UnityEngine.Vector3 min, UnityEngine.Vector3 max, System.Collections.Generic.List<System.Int32> resultIndices) { }
        // RVA: 0x0B89BF20  token: 0x6000012
        public System.Void KNearest(DataStructures.ViliWonka.KDTree.KDTree tree, UnityEngine.Vector3 queryPosition, System.Int32 k, System.Collections.Generic.List<System.Int32> resultIndices, System.Collections.Generic.List<System.Single> resultDistances) { }
        // RVA: 0x0B89C6E8  token: 0x6000013
        public System.Void Radius(DataStructures.ViliWonka.KDTree.KDTree tree, UnityEngine.Vector3 queryPosition, System.Single queryRadius, System.Collections.Generic.List<System.Int32> resultIndices) { }

    }

    // TypeToken: 0x2000005  // size: 0x28
    public class KDQueryNode
    {
        // Fields
        public DataStructures.ViliWonka.KDTree.KDNode node;  // 0x10
        public UnityEngine.Vector3 tempClosestPoint;  // 0x18
        public System.Single distance;  // 0x24

        // Methods
        // RVA: 0x041E1670  token: 0x6000014
        public System.Void .ctor() { }
        // RVA: 0x067B70A8  token: 0x6000015
        public System.Void .ctor(DataStructures.ViliWonka.KDTree.KDNode node, UnityEngine.Vector3 tempClosestPoint) { }

    }

    // TypeToken: 0x2000006  // size: 0x40
    public class KDTree
    {
        // Fields
        private DataStructures.ViliWonka.KDTree.KDNode <RootNode>k__BackingField;  // 0x10
        private UnityEngine.Vector3[] points;  // 0x18
        private System.Int32[] permutation;  // 0x20
        private System.Int32 <Count>k__BackingField;  // 0x28
        private System.Int32 maxPointsPerLeafNode;  // 0x2c
        private DataStructures.ViliWonka.KDTree.KDNode[] kdNodesStack;  // 0x30
        private System.Int32 kdNodesCount;  // 0x38

        // Properties
        DataStructures.ViliWonka.KDTree.KDNode RootNode { get; /* RVA: 0x02B2ECC0 */ set; /* RVA: 0x053908C0 */ }
        UnityEngine.Vector3[] Points { get; /* RVA: 0x0385B100 */ }
        System.Int32[] Permutation { get; /* RVA: 0x04D862C0 */ }
        System.Int32 Count { get; /* RVA: 0x011F2A90 */ set; /* RVA: 0x011F2AA0 */ }

        // Methods
        // RVA: 0x0B89DAE8  token: 0x600001C
        public System.Void .ctor(System.Int32 maxPointsPerLeafNode) { }
        // RVA: 0x0B89DB98  token: 0x600001D
        public System.Void .ctor(UnityEngine.Vector3[] points, System.Int32 maxPointsPerLeafNode) { }
        // RVA: 0x0B89CD3C  token: 0x600001E
        public System.Void Build(UnityEngine.Vector3[] newPoints, System.Int32 maxPointsPerLeafNode) { }
        // RVA: 0x0B89CC5C  token: 0x600001F
        public System.Void Build(System.Collections.Generic.List<UnityEngine.Vector3> newPoints, System.Int32 maxPointsPerLeafNode) { }
        // RVA: 0x0B89D7E0  token: 0x6000020
        public System.Void Rebuild(System.Int32 maxPointsPerLeafNode) { }
        // RVA: 0x0B89D82C  token: 0x6000021
        public System.Void SetCount(System.Int32 newSize) { }
        // RVA: 0x0B89CBD0  token: 0x6000022
        private System.Void BuildTree() { }
        // RVA: 0x0B89D038  token: 0x6000023
        private DataStructures.ViliWonka.KDTree.KDNode GetKDNode() { }
        // RVA: 0x04DBF690  token: 0x6000024
        private System.Void ResetKDNodeStack() { }
        // RVA: 0x0B89D13C  token: 0x6000025
        private DataStructures.ViliWonka.KDTree.KDBounds MakeBounds() { }
        // RVA: 0x0B89D8AC  token: 0x6000026
        private System.Void SplitNode(DataStructures.ViliWonka.KDTree.KDNode parent) { }
        // RVA: 0x0B89CDE8  token: 0x6000027
        private System.Single CalculatePivot(System.Int32 start, System.Int32 end, System.Single boundsStart, System.Single boundsEnd, System.Int32 axis) { }
        // RVA: 0x0B89D6CC  token: 0x6000028
        private System.Int32 Partition(System.Int32 start, System.Int32 end, System.Single partitionPivot, System.Int32 axis) { }
        // RVA: 0x0B89D018  token: 0x6000029
        private System.Boolean ContinueSplit(DataStructures.ViliWonka.KDTree.KDNode node) { }

    }

}

