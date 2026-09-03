// ========================================================
// Dumped by @desirepro
// Assembly: Priority Queue.dll
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

    // TypeToken: 0x2000003
    public sealed class <GetEnumerator>d__22 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private T <>2__current;  // 0x0
        public Priority_Queue.FastPriorityQueue<T> <>4__this;  // 0x0
        private System.Int32 <i>5__2;  // 0x0

        // Properties
        T System.Collections.Generic.IEnumerator<T>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000015
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x6000016
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000017
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x6000019
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x2000006
    public sealed class <GetEnumerator>d__25 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private TItem <>2__current;  // 0x0
        public Priority_Queue.GenericPriorityQueue<TItem,TPriority> <>4__this;  // 0x0
        private System.Int32 <i>5__2;  // 0x0

        // Properties
        TItem System.Collections.Generic.IEnumerator<TItem>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000035
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x6000036
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000037
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x6000039
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

    // TypeToken: 0x200000B
    public class SimpleNode : Priority_Queue.GenericPriorityQueueNode`1
    {
        // Fields
        private TItem <Data>k__BackingField;  // 0x0

        // Properties
        TItem Data { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600006B
        public System.Void .ctor(TItem data) { }

    }

    // TypeToken: 0x200000E
    public sealed class <GetEnumerator>d__22 : System.Collections.Generic.IEnumerator`1, System.Collections.IEnumerator, System.IDisposable
    {
        // Fields
        private System.Int32 <>1__state;  // 0x0
        private T <>2__current;  // 0x0
        public Priority_Queue.StablePriorityQueue<T> <>4__this;  // 0x0
        private System.Int32 <i>5__2;  // 0x0

        // Properties
        T System.Collections.Generic.IEnumerator<T>.Current { get; /* RVA: -1  // not resolved */ }
        System.Object System.Collections.IEnumerator.Current { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000082
        public System.Void .ctor(System.Int32 <>1__state) { }
        // RVA: -1  // not resolved  token: 0x6000083
        private virtual System.Void System.IDisposable.Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000084
        private virtual System.Boolean MoveNext() { }
        // RVA: -1  // not resolved  token: 0x6000086
        private virtual System.Void System.Collections.IEnumerator.Reset() { }

    }

namespace Priority_Queue
{

    // TypeToken: 0x2000002
    public sealed class FastPriorityQueue`1 : Priority_Queue.IFixedSizePriorityQueue`2, Priority_Queue.IPriorityQueue`2, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private System.Int32 _numNodes;  // 0x0
        private T[] _nodes;  // 0x0

        // Properties
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        System.Int32 MaxSize { get; /* RVA: -1  // not resolved */ }
        T First { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000001
        public System.Void .ctor(System.Int32 maxNodes) { }
        // RVA: -1  // not resolved  token: 0x6000004
        public virtual System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x6000005
        public virtual System.Boolean Contains(T node) { }
        // RVA: -1  // not resolved  token: 0x6000006
        public virtual System.Void Enqueue(T node, System.Single priority) { }
        // RVA: -1  // not resolved  token: 0x6000007
        private System.Void CascadeUp(T node) { }
        // RVA: -1  // not resolved  token: 0x6000008
        private System.Void CascadeDown(T node) { }
        // RVA: -1  // not resolved  token: 0x6000009
        private System.Boolean HasHigherPriority(T higher, T lower) { }
        // RVA: -1  // not resolved  token: 0x600000A
        private System.Boolean HasHigherOrEqualPriority(T higher, T lower) { }
        // RVA: -1  // not resolved  token: 0x600000B
        public virtual T Dequeue() { }
        // RVA: -1  // not resolved  token: 0x600000C
        public virtual System.Void Resize(System.Int32 maxNodes) { }
        // RVA: -1  // not resolved  token: 0x600000E
        public virtual System.Void UpdatePriority(T node, System.Single priority) { }
        // RVA: -1  // not resolved  token: 0x600000F
        private System.Void OnNodeUpdated(T node) { }
        // RVA: -1  // not resolved  token: 0x6000010
        public virtual System.Void Remove(T node) { }
        // RVA: -1  // not resolved  token: 0x6000011
        public virtual System.Void ResetNode(T node) { }
        // RVA: -1  // not resolved  token: 0x6000012
        public virtual System.Collections.Generic.IEnumerator<T> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000013
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000014
        public System.Boolean IsValidQueue() { }

    }

    // TypeToken: 0x2000004  // size: 0x18
    public class FastPriorityQueueNode
    {
        // Fields
        private System.Single <Priority>k__BackingField;  // 0x10
        private System.Int32 <QueueIndex>k__BackingField;  // 0x14

        // Properties
        System.Single Priority { get; /* RVA: 0x04D85F70 */ set; /* RVA: 0x04D88280 */ }
        System.Int32 QueueIndex { get; /* RVA: 0x04D882A0 */ set; /* RVA: 0x04D8F540 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x600001F
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000005
    public sealed class GenericPriorityQueue`2 : Priority_Queue.IFixedSizePriorityQueue`2, Priority_Queue.IPriorityQueue`2, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private System.Int32 _numNodes;  // 0x0
        private TItem[] _nodes;  // 0x0
        private System.Int64 _numNodesEverEnqueued;  // 0x0
        private readonly System.Comparison<TPriority> _comparer;  // 0x0

        // Properties
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        System.Int32 MaxSize { get; /* RVA: -1  // not resolved */ }
        TItem First { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000020
        public System.Void .ctor(System.Int32 maxNodes) { }
        // RVA: -1  // not resolved  token: 0x6000021
        public System.Void .ctor(System.Int32 maxNodes, System.Collections.Generic.IComparer<TPriority> comparer) { }
        // RVA: -1  // not resolved  token: 0x6000022
        public System.Void .ctor(System.Int32 maxNodes, System.Comparison<TPriority> comparer) { }
        // RVA: -1  // not resolved  token: 0x6000025
        public virtual System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x6000026
        public virtual System.Boolean Contains(TItem node) { }
        // RVA: -1  // not resolved  token: 0x6000027
        public virtual System.Void Enqueue(TItem node, TPriority priority) { }
        // RVA: -1  // not resolved  token: 0x6000028
        private System.Void CascadeUp(TItem node) { }
        // RVA: -1  // not resolved  token: 0x6000029
        private System.Void CascadeDown(TItem node) { }
        // RVA: -1  // not resolved  token: 0x600002A
        private System.Boolean HasHigherPriority(TItem higher, TItem lower) { }
        // RVA: -1  // not resolved  token: 0x600002B
        public virtual TItem Dequeue() { }
        // RVA: -1  // not resolved  token: 0x600002C
        public virtual System.Void Resize(System.Int32 maxNodes) { }
        // RVA: -1  // not resolved  token: 0x600002E
        public virtual System.Void UpdatePriority(TItem node, TPriority priority) { }
        // RVA: -1  // not resolved  token: 0x600002F
        private System.Void OnNodeUpdated(TItem node) { }
        // RVA: -1  // not resolved  token: 0x6000030
        public virtual System.Void Remove(TItem node) { }
        // RVA: -1  // not resolved  token: 0x6000031
        public virtual System.Void ResetNode(TItem node) { }
        // RVA: -1  // not resolved  token: 0x6000032
        public virtual System.Collections.Generic.IEnumerator<TItem> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000033
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000034
        public System.Boolean IsValidQueue() { }

    }

    // TypeToken: 0x2000007
    public class GenericPriorityQueueNode`1
    {
        // Fields
        private TPriority <Priority>k__BackingField;  // 0x0
        private System.Int32 <QueueIndex>k__BackingField;  // 0x0
        private System.Int64 <InsertionIndex>k__BackingField;  // 0x0

        // Properties
        TPriority Priority { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Int32 QueueIndex { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Int64 InsertionIndex { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x6000041
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000008
    public interface IFixedSizePriorityQueue`2 : Priority_Queue.IPriorityQueue`2, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Properties
        System.Int32 MaxSize { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000042
        public virtual System.Void Resize(System.Int32 maxNodes) { }
        // RVA: -1  // abstract  token: 0x6000044
        public virtual System.Void ResetNode(TItem node) { }

    }

    // TypeToken: 0x2000009
    public interface IPriorityQueue`2 : System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Properties
        TItem First { get; /* RVA: -1  // abstract */ }
        System.Int32 Count { get; /* RVA: -1  // abstract */ }

        // Methods
        // RVA: -1  // abstract  token: 0x6000045
        public virtual System.Void Enqueue(TItem node, TPriority priority) { }
        // RVA: -1  // abstract  token: 0x6000046
        public virtual TItem Dequeue() { }
        // RVA: -1  // abstract  token: 0x6000047
        public virtual System.Void Clear() { }
        // RVA: -1  // abstract  token: 0x6000048
        public virtual System.Boolean Contains(TItem node) { }
        // RVA: -1  // abstract  token: 0x6000049
        public virtual System.Void Remove(TItem node) { }
        // RVA: -1  // abstract  token: 0x600004A
        public virtual System.Void UpdatePriority(TItem node, TPriority priority) { }

    }

    // TypeToken: 0x200000A
    public class SimplePriorityQueue`2 : Priority_Queue.IPriorityQueue`2, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private static System.Int32 INITIAL_QUEUE_SIZE;  // const
        private readonly Priority_Queue.GenericPriorityQueue<Priority_Queue.SimplePriorityQueue.SimpleNode<TItem,TPriority>,TPriority> _queue;  // 0x0
        private readonly System.Collections.Generic.Dictionary<TItem,System.Collections.Generic.IList<Priority_Queue.SimplePriorityQueue.SimpleNode<TItem,TPriority>>> _itemToNodesCache;  // 0x0
        private readonly System.Collections.Generic.IList<Priority_Queue.SimplePriorityQueue.SimpleNode<TItem,TPriority>> _nullNodesCache;  // 0x0

        // Properties
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        TItem First { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600004D
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x600004E
        public System.Void .ctor(System.Collections.Generic.IComparer<TPriority> priorityComparer) { }
        // RVA: -1  // not resolved  token: 0x600004F
        public System.Void .ctor(System.Comparison<TPriority> priorityComparer) { }
        // RVA: -1  // not resolved  token: 0x6000050
        public System.Void .ctor(System.Collections.Generic.IEqualityComparer<TItem> itemEquality) { }
        // RVA: -1  // not resolved  token: 0x6000051
        public System.Void .ctor(System.Collections.Generic.IComparer<TPriority> priorityComparer, System.Collections.Generic.IEqualityComparer<TItem> itemEquality) { }
        // RVA: -1  // not resolved  token: 0x6000052
        public System.Void .ctor(System.Comparison<TPriority> priorityComparer, System.Collections.Generic.IEqualityComparer<TItem> itemEquality) { }
        // RVA: -1  // not resolved  token: 0x6000053
        private Priority_Queue.SimplePriorityQueue.SimpleNode<TItem,TPriority> GetExistingNode(TItem item) { }
        // RVA: -1  // not resolved  token: 0x6000054
        private System.Void AddToNodeCache(Priority_Queue.SimplePriorityQueue.SimpleNode<TItem,TPriority> node) { }
        // RVA: -1  // not resolved  token: 0x6000055
        private System.Void RemoveFromNodeCache(Priority_Queue.SimplePriorityQueue.SimpleNode<TItem,TPriority> node) { }
        // RVA: -1  // not resolved  token: 0x6000058
        public virtual System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x6000059
        public virtual System.Boolean Contains(TItem item) { }
        // RVA: -1  // not resolved  token: 0x600005A
        public virtual TItem Dequeue() { }
        // RVA: -1  // not resolved  token: 0x600005B
        private Priority_Queue.SimplePriorityQueue.SimpleNode<TItem,TPriority> EnqueueNoLockOrCache(TItem item, TPriority priority) { }
        // RVA: -1  // not resolved  token: 0x600005C
        public virtual System.Void Enqueue(TItem item, TPriority priority) { }
        // RVA: -1  // not resolved  token: 0x600005D
        public System.Boolean EnqueueWithoutDuplicates(TItem item, TPriority priority) { }
        // RVA: -1  // not resolved  token: 0x600005E
        public virtual System.Void Remove(TItem item) { }
        // RVA: -1  // not resolved  token: 0x600005F
        public virtual System.Void UpdatePriority(TItem item, TPriority priority) { }
        // RVA: -1  // not resolved  token: 0x6000060
        public TPriority GetPriority(TItem item) { }
        // RVA: -1  // not resolved  token: 0x6000061
        public System.Boolean TryFirst(TItem& first) { }
        // RVA: -1  // not resolved  token: 0x6000062
        public System.Boolean TryDequeue(TItem& first) { }
        // RVA: -1  // not resolved  token: 0x6000063
        public System.Boolean TryRemove(TItem item) { }
        // RVA: -1  // not resolved  token: 0x6000064
        public System.Boolean TryUpdatePriority(TItem item, TPriority priority) { }
        // RVA: -1  // not resolved  token: 0x6000065
        public System.Boolean TryGetPriority(TItem item, TPriority& priority) { }
        // RVA: -1  // not resolved  token: 0x6000066
        public virtual System.Collections.Generic.IEnumerator<TItem> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000067
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000068
        public System.Boolean IsValidQueue() { }

    }

    // TypeToken: 0x200000C
    public class SimplePriorityQueue`1 : Priority_Queue.SimplePriorityQueue`2
    {
        // Methods
        // RVA: -1  // not resolved  token: 0x600006C
        public System.Void .ctor() { }
        // RVA: -1  // not resolved  token: 0x600006D
        public System.Void .ctor(System.Collections.Generic.IComparer<System.Single> comparer) { }
        // RVA: -1  // not resolved  token: 0x600006E
        public System.Void .ctor(System.Comparison<System.Single> comparer) { }

    }

    // TypeToken: 0x200000D
    public sealed class StablePriorityQueue`1 : Priority_Queue.IFixedSizePriorityQueue`2, Priority_Queue.IPriorityQueue`2, System.Collections.Generic.IEnumerable`1, System.Collections.IEnumerable
    {
        // Fields
        private System.Int32 _numNodes;  // 0x0
        private T[] _nodes;  // 0x0
        private System.Int64 _numNodesEverEnqueued;  // 0x0

        // Properties
        System.Int32 Count { get; /* RVA: -1  // not resolved */ }
        System.Int32 MaxSize { get; /* RVA: -1  // not resolved */ }
        T First { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600006F
        public System.Void .ctor(System.Int32 maxNodes) { }
        // RVA: -1  // not resolved  token: 0x6000072
        public virtual System.Void Clear() { }
        // RVA: -1  // not resolved  token: 0x6000073
        public virtual System.Boolean Contains(T node) { }
        // RVA: -1  // not resolved  token: 0x6000074
        public virtual System.Void Enqueue(T node, System.Single priority) { }
        // RVA: -1  // not resolved  token: 0x6000075
        private System.Void CascadeUp(T node) { }
        // RVA: -1  // not resolved  token: 0x6000076
        private System.Void CascadeDown(T node) { }
        // RVA: -1  // not resolved  token: 0x6000077
        private System.Boolean HasHigherPriority(T higher, T lower) { }
        // RVA: -1  // not resolved  token: 0x6000078
        public virtual T Dequeue() { }
        // RVA: -1  // not resolved  token: 0x6000079
        public virtual System.Void Resize(System.Int32 maxNodes) { }
        // RVA: -1  // not resolved  token: 0x600007B
        public virtual System.Void UpdatePriority(T node, System.Single priority) { }
        // RVA: -1  // not resolved  token: 0x600007C
        private System.Void OnNodeUpdated(T node) { }
        // RVA: -1  // not resolved  token: 0x600007D
        public virtual System.Void Remove(T node) { }
        // RVA: -1  // not resolved  token: 0x600007E
        public virtual System.Void ResetNode(T node) { }
        // RVA: -1  // not resolved  token: 0x600007F
        public virtual System.Collections.Generic.IEnumerator<T> GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000080
        private virtual System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() { }
        // RVA: -1  // not resolved  token: 0x6000081
        public System.Boolean IsValidQueue() { }

    }

    // TypeToken: 0x200000F  // size: 0x20
    public class StablePriorityQueueNode : Priority_Queue.FastPriorityQueueNode
    {
        // Fields
        private System.Int64 <InsertionIndex>k__BackingField;  // 0x18

        // Properties
        System.Int64 InsertionIndex { get; /* RVA: 0x0385B100 */ set; /* RVA: 0x011EC9D0 */ }

        // Methods
        // RVA: 0x041E1670  token: 0x600008A
        public System.Void .ctor() { }

    }

}

