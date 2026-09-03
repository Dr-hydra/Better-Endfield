// ========================================================
// Dumped by @desirepro
// Assembly: Priority Queue.dll
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

CLASS: <GetEnumerator>d__22
TYPE:  sealed class
TOKEN: 0x2000003
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           T                               <>2__current  // 0x0
  public            Priority_Queue.FastPriorityQueue<T><>4__this  // 0x0
  private           System.Int32                    <i>5__2  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<T>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000015  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x6000016  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x6000017  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x6000019  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: <GetEnumerator>d__25
TYPE:  sealed class
TOKEN: 0x2000006
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           TItem                           <>2__current  // 0x0
  public            Priority_Queue.GenericPriorityQueue<TItem,TPriority><>4__this  // 0x0
  private           System.Int32                    <i>5__2  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<TItem>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000035  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x6000036  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x6000037  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x6000039  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: SimpleNode
TYPE:  class
TOKEN: 0x200000B
EXTENDS: Priority_Queue.GenericPriorityQueueNode`1
FIELDS:
  private           TItem                           <Data>k__BackingField  // 0x0
PROPERTIES:
  Data  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600006B  System.Void .ctor(TItem data)
END_CLASS

CLASS: <GetEnumerator>d__22
TYPE:  sealed class
TOKEN: 0x200000E
IMPLEMENTS: System.Collections.Generic.IEnumerator`1 System.Collections.IEnumerator System.IDisposable
FIELDS:
  private           System.Int32                    <>1__state  // 0x0
  private           T                               <>2__current  // 0x0
  public            Priority_Queue.StablePriorityQueue<T><>4__this  // 0x0
  private           System.Int32                    <i>5__2  // 0x0
PROPERTIES:
  System.Collections.Generic.IEnumerator<T>.Current  get=-1  // not resolved
  System.Collections.IEnumerator.Current  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000082  System.Void .ctor(System.Int32 <>1__state)
  RVA=-1  // not resolved  token=0x6000083  System.Void System.IDisposable.Dispose()
  RVA=-1  // not resolved  token=0x6000084  System.Boolean MoveNext()
  RVA=-1  // not resolved  token=0x6000086  System.Void System.Collections.IEnumerator.Reset()
END_CLASS

CLASS: Priority_Queue.FastPriorityQueue`1
TYPE:  sealed class
TOKEN: 0x2000002
IMPLEMENTS: Priority_Queue.IFixedSizePriorityQueue`2 Priority_Queue.IPriorityQueue`2 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           System.Int32                    _numNodes  // 0x0
  private           T[]                             _nodes  // 0x0
PROPERTIES:
  Count  get=-1  // not resolved
  MaxSize  get=-1  // not resolved
  First  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000001  System.Void .ctor(System.Int32 maxNodes)
  RVA=-1  // not resolved  token=0x6000004  System.Void Clear()
  RVA=-1  // not resolved  token=0x6000005  System.Boolean Contains(T node)
  RVA=-1  // not resolved  token=0x6000006  System.Void Enqueue(T node, System.Single priority)
  RVA=-1  // not resolved  token=0x6000007  System.Void CascadeUp(T node)
  RVA=-1  // not resolved  token=0x6000008  System.Void CascadeDown(T node)
  RVA=-1  // not resolved  token=0x6000009  System.Boolean HasHigherPriority(T higher, T lower)
  RVA=-1  // not resolved  token=0x600000A  System.Boolean HasHigherOrEqualPriority(T higher, T lower)
  RVA=-1  // not resolved  token=0x600000B  T Dequeue()
  RVA=-1  // not resolved  token=0x600000C  System.Void Resize(System.Int32 maxNodes)
  RVA=-1  // not resolved  token=0x600000E  System.Void UpdatePriority(T node, System.Single priority)
  RVA=-1  // not resolved  token=0x600000F  System.Void OnNodeUpdated(T node)
  RVA=-1  // not resolved  token=0x6000010  System.Void Remove(T node)
  RVA=-1  // not resolved  token=0x6000011  System.Void ResetNode(T node)
  RVA=-1  // not resolved  token=0x6000012  System.Collections.Generic.IEnumerator<T> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000013  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000014  System.Boolean IsValidQueue()
END_CLASS

CLASS: Priority_Queue.FastPriorityQueueNode
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x18
FIELDS:
  private           System.Single                   <Priority>k__BackingField  // 0x10
  private           System.Int32                    <QueueIndex>k__BackingField  // 0x14
PROPERTIES:
  Priority  get=0x04D85F70  set=0x04D88280
  QueueIndex  get=0x04D882A0  set=0x04D8F540
METHODS:
  RVA=0x041E1670  token=0x600001F  System.Void .ctor()
END_CLASS

CLASS: Priority_Queue.GenericPriorityQueue`2
TYPE:  sealed class
TOKEN: 0x2000005
IMPLEMENTS: Priority_Queue.IFixedSizePriorityQueue`2 Priority_Queue.IPriorityQueue`2 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           System.Int32                    _numNodes  // 0x0
  private           TItem[]                         _nodes  // 0x0
  private           System.Int64                    _numNodesEverEnqueued  // 0x0
  private   readonly System.Comparison<TPriority>    _comparer  // 0x0
PROPERTIES:
  Count  get=-1  // not resolved
  MaxSize  get=-1  // not resolved
  First  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000020  System.Void .ctor(System.Int32 maxNodes)
  RVA=-1  // not resolved  token=0x6000021  System.Void .ctor(System.Int32 maxNodes, System.Collections.Generic.IComparer<TPriority> comparer)
  RVA=-1  // not resolved  token=0x6000022  System.Void .ctor(System.Int32 maxNodes, System.Comparison<TPriority> comparer)
  RVA=-1  // not resolved  token=0x6000025  System.Void Clear()
  RVA=-1  // not resolved  token=0x6000026  System.Boolean Contains(TItem node)
  RVA=-1  // not resolved  token=0x6000027  System.Void Enqueue(TItem node, TPriority priority)
  RVA=-1  // not resolved  token=0x6000028  System.Void CascadeUp(TItem node)
  RVA=-1  // not resolved  token=0x6000029  System.Void CascadeDown(TItem node)
  RVA=-1  // not resolved  token=0x600002A  System.Boolean HasHigherPriority(TItem higher, TItem lower)
  RVA=-1  // not resolved  token=0x600002B  TItem Dequeue()
  RVA=-1  // not resolved  token=0x600002C  System.Void Resize(System.Int32 maxNodes)
  RVA=-1  // not resolved  token=0x600002E  System.Void UpdatePriority(TItem node, TPriority priority)
  RVA=-1  // not resolved  token=0x600002F  System.Void OnNodeUpdated(TItem node)
  RVA=-1  // not resolved  token=0x6000030  System.Void Remove(TItem node)
  RVA=-1  // not resolved  token=0x6000031  System.Void ResetNode(TItem node)
  RVA=-1  // not resolved  token=0x6000032  System.Collections.Generic.IEnumerator<TItem> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000033  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000034  System.Boolean IsValidQueue()
END_CLASS

CLASS: Priority_Queue.GenericPriorityQueueNode`1
TYPE:  class
TOKEN: 0x2000007
FIELDS:
  private           TPriority                       <Priority>k__BackingField  // 0x0
  private           System.Int32                    <QueueIndex>k__BackingField  // 0x0
  private           System.Int64                    <InsertionIndex>k__BackingField  // 0x0
PROPERTIES:
  Priority  get=-1  // not resolved  set=-1  // not resolved
  QueueIndex  get=-1  // not resolved  set=-1  // not resolved
  InsertionIndex  get=-1  // not resolved  set=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000041  System.Void .ctor()
END_CLASS

CLASS: Priority_Queue.IFixedSizePriorityQueue`2
TYPE:  interface
TOKEN: 0x2000008
IMPLEMENTS: Priority_Queue.IPriorityQueue`2 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
PROPERTIES:
  MaxSize  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000042  System.Void Resize(System.Int32 maxNodes)
  RVA=-1  // abstract  token=0x6000044  System.Void ResetNode(TItem node)
END_CLASS

CLASS: Priority_Queue.IPriorityQueue`2
TYPE:  interface
TOKEN: 0x2000009
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
PROPERTIES:
  First  get=-1  // abstract
  Count  get=-1  // abstract
METHODS:
  RVA=-1  // abstract  token=0x6000045  System.Void Enqueue(TItem node, TPriority priority)
  RVA=-1  // abstract  token=0x6000046  TItem Dequeue()
  RVA=-1  // abstract  token=0x6000047  System.Void Clear()
  RVA=-1  // abstract  token=0x6000048  System.Boolean Contains(TItem node)
  RVA=-1  // abstract  token=0x6000049  System.Void Remove(TItem node)
  RVA=-1  // abstract  token=0x600004A  System.Void UpdatePriority(TItem node, TPriority priority)
END_CLASS

CLASS: Priority_Queue.SimplePriorityQueue`2
TYPE:  class
TOKEN: 0x200000A
IMPLEMENTS: Priority_Queue.IPriorityQueue`2 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private   static  System.Int32                    INITIAL_QUEUE_SIZE  // const
  private   readonly Priority_Queue.GenericPriorityQueue<Priority_Queue.SimplePriorityQueue.SimpleNode<TItem,TPriority>,TPriority>_queue  // 0x0
  private   readonly System.Collections.Generic.Dictionary<TItem,System.Collections.Generic.IList<Priority_Queue.SimplePriorityQueue.SimpleNode<TItem,TPriority>>>_itemToNodesCache  // 0x0
  private   readonly System.Collections.Generic.IList<Priority_Queue.SimplePriorityQueue.SimpleNode<TItem,TPriority>>_nullNodesCache  // 0x0
PROPERTIES:
  Count  get=-1  // not resolved
  First  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600004D  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600004E  System.Void .ctor(System.Collections.Generic.IComparer<TPriority> priorityComparer)
  RVA=-1  // not resolved  token=0x600004F  System.Void .ctor(System.Comparison<TPriority> priorityComparer)
  RVA=-1  // not resolved  token=0x6000050  System.Void .ctor(System.Collections.Generic.IEqualityComparer<TItem> itemEquality)
  RVA=-1  // not resolved  token=0x6000051  System.Void .ctor(System.Collections.Generic.IComparer<TPriority> priorityComparer, System.Collections.Generic.IEqualityComparer<TItem> itemEquality)
  RVA=-1  // not resolved  token=0x6000052  System.Void .ctor(System.Comparison<TPriority> priorityComparer, System.Collections.Generic.IEqualityComparer<TItem> itemEquality)
  RVA=-1  // not resolved  token=0x6000053  Priority_Queue.SimplePriorityQueue.SimpleNode<TItem,TPriority> GetExistingNode(TItem item)
  RVA=-1  // not resolved  token=0x6000054  System.Void AddToNodeCache(Priority_Queue.SimplePriorityQueue.SimpleNode<TItem,TPriority> node)
  RVA=-1  // not resolved  token=0x6000055  System.Void RemoveFromNodeCache(Priority_Queue.SimplePriorityQueue.SimpleNode<TItem,TPriority> node)
  RVA=-1  // not resolved  token=0x6000058  System.Void Clear()
  RVA=-1  // not resolved  token=0x6000059  System.Boolean Contains(TItem item)
  RVA=-1  // not resolved  token=0x600005A  TItem Dequeue()
  RVA=-1  // not resolved  token=0x600005B  Priority_Queue.SimplePriorityQueue.SimpleNode<TItem,TPriority> EnqueueNoLockOrCache(TItem item, TPriority priority)
  RVA=-1  // not resolved  token=0x600005C  System.Void Enqueue(TItem item, TPriority priority)
  RVA=-1  // not resolved  token=0x600005D  System.Boolean EnqueueWithoutDuplicates(TItem item, TPriority priority)
  RVA=-1  // not resolved  token=0x600005E  System.Void Remove(TItem item)
  RVA=-1  // not resolved  token=0x600005F  System.Void UpdatePriority(TItem item, TPriority priority)
  RVA=-1  // not resolved  token=0x6000060  TPriority GetPriority(TItem item)
  RVA=-1  // not resolved  token=0x6000061  System.Boolean TryFirst(TItem& first)
  RVA=-1  // not resolved  token=0x6000062  System.Boolean TryDequeue(TItem& first)
  RVA=-1  // not resolved  token=0x6000063  System.Boolean TryRemove(TItem item)
  RVA=-1  // not resolved  token=0x6000064  System.Boolean TryUpdatePriority(TItem item, TPriority priority)
  RVA=-1  // not resolved  token=0x6000065  System.Boolean TryGetPriority(TItem item, TPriority& priority)
  RVA=-1  // not resolved  token=0x6000066  System.Collections.Generic.IEnumerator<TItem> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000067  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000068  System.Boolean IsValidQueue()
END_CLASS

CLASS: Priority_Queue.SimplePriorityQueue`1
TYPE:  class
TOKEN: 0x200000C
EXTENDS: Priority_Queue.SimplePriorityQueue`2
FIELDS:
METHODS:
  RVA=-1  // not resolved  token=0x600006C  System.Void .ctor()
  RVA=-1  // not resolved  token=0x600006D  System.Void .ctor(System.Collections.Generic.IComparer<System.Single> comparer)
  RVA=-1  // not resolved  token=0x600006E  System.Void .ctor(System.Comparison<System.Single> comparer)
END_CLASS

CLASS: Priority_Queue.StablePriorityQueue`1
TYPE:  sealed class
TOKEN: 0x200000D
IMPLEMENTS: Priority_Queue.IFixedSizePriorityQueue`2 Priority_Queue.IPriorityQueue`2 System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           System.Int32                    _numNodes  // 0x0
  private           T[]                             _nodes  // 0x0
  private           System.Int64                    _numNodesEverEnqueued  // 0x0
PROPERTIES:
  Count  get=-1  // not resolved
  MaxSize  get=-1  // not resolved
  First  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600006F  System.Void .ctor(System.Int32 maxNodes)
  RVA=-1  // not resolved  token=0x6000072  System.Void Clear()
  RVA=-1  // not resolved  token=0x6000073  System.Boolean Contains(T node)
  RVA=-1  // not resolved  token=0x6000074  System.Void Enqueue(T node, System.Single priority)
  RVA=-1  // not resolved  token=0x6000075  System.Void CascadeUp(T node)
  RVA=-1  // not resolved  token=0x6000076  System.Void CascadeDown(T node)
  RVA=-1  // not resolved  token=0x6000077  System.Boolean HasHigherPriority(T higher, T lower)
  RVA=-1  // not resolved  token=0x6000078  T Dequeue()
  RVA=-1  // not resolved  token=0x6000079  System.Void Resize(System.Int32 maxNodes)
  RVA=-1  // not resolved  token=0x600007B  System.Void UpdatePriority(T node, System.Single priority)
  RVA=-1  // not resolved  token=0x600007C  System.Void OnNodeUpdated(T node)
  RVA=-1  // not resolved  token=0x600007D  System.Void Remove(T node)
  RVA=-1  // not resolved  token=0x600007E  System.Void ResetNode(T node)
  RVA=-1  // not resolved  token=0x600007F  System.Collections.Generic.IEnumerator<T> GetEnumerator()
  RVA=-1  // not resolved  token=0x6000080  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=-1  // not resolved  token=0x6000081  System.Boolean IsValidQueue()
END_CLASS

CLASS: Priority_Queue.StablePriorityQueueNode
TYPE:  class
TOKEN: 0x200000F
SIZE:  0x20
EXTENDS: Priority_Queue.FastPriorityQueueNode
FIELDS:
  private           System.Int64                    <InsertionIndex>k__BackingField  // 0x18
PROPERTIES:
  InsertionIndex  get=0x0385B100  set=0x011EC9D0
METHODS:
  RVA=0x041E1670  token=0x600008A  System.Void .ctor()
END_CLASS

