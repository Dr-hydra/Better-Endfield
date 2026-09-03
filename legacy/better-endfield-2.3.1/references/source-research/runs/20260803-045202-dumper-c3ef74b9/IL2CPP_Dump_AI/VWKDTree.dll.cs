// ========================================================
// Dumped by @desirepro
// Assembly: VWKDTree.dll
// Classes:  13
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

CLASS: DataStructures.ViliWonka.Heap.BaseHeap
TYPE:  abstract class
TOKEN: 0x2000007
SIZE:  0x28
FIELDS:
  protected         System.Int32                    nodesCount  // 0x10
  protected         System.Int32                    maxSize  // 0x14
  protected         System.Single[]                 heap  // 0x18
  protected         System.Single                   tempHeap  // 0x20
PROPERTIES:
  Count  get=0x02B2E2D0
  HeadValue  get=0x0B89B3BC
METHODS:
  RVA=0x0B89B368  token=0x600002A  System.Void .ctor(System.Int32 initialSize)
  RVA=0x04D91EC0  token=0x600002D  System.Void Clear()
  RVA=0x04DBF650  token=0x600002E  System.Int32 Parent(System.Int32 index)
  RVA=0x04DBF640  token=0x600002F  System.Int32 Left(System.Int32 index)
  RVA=0x04DBF660  token=0x6000030  System.Int32 Right(System.Int32 index)
  RVA=0x0B89AE18  token=0x6000031  System.Void BubbleDownMax(System.Int32 index)
  RVA=0x0B89B0A8  token=0x6000032  System.Void BubbleUpMax(System.Int32 index)
  RVA=0x0B89AF60  token=0x6000033  System.Void BubbleDownMin(System.Int32 index)
  RVA=0x0B89B114  token=0x6000034  System.Void BubbleUpMin(System.Int32 index)
  RVA=0x0B89B2A4  token=0x6000035  System.Void Swap(System.Int32 A, System.Int32 B)
  RVA=0x0B89B320  token=0x6000036  System.Void UpsizeHeap()
  RVA=0x0B89B258  token=0x6000037  System.Void PushValue(System.Single h)
  RVA=0x0B89B20C  token=0x6000038  System.Single PopValue()
  RVA=0x0B89B180  token=0x6000039  System.Void FlushHeapResult(System.Collections.Generic.List<System.Single> heapList)
END_CLASS

CLASS: DataStructures.ViliWonka.Heap.KSmallestHeap
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x28
EXTENDS: DataStructures.ViliWonka.Heap.BaseHeap
FIELDS:
PROPERTIES:
  Full  get=0x0B89E0C0
METHODS:
  RVA=0x0B89E0B8  token=0x600003A  System.Void .ctor(System.Int32 maxEntries)
  RVA=0x0B89E024  token=0x600003C  System.Void PushValue(System.Single h)
  RVA=0x0B89DED0  token=0x600003D  System.Single PopValue()
  RVA=0x0B89DF88  token=0x600003E  System.Void Print()
  RVA=0x0B89DC5C  token=0x600003F  System.Boolean HeapPropertyHolds(System.Int32 index, System.Int32 depth)
END_CLASS

CLASS: DataStructures.ViliWonka.Heap.KSmallestHeap`1
TYPE:  class
TOKEN: 0x2000009
EXTENDS: DataStructures.ViliWonka.Heap.KSmallestHeap
FIELDS:
  private           T[]                             objs  // 0x0
  private           T                               tempObjs  // 0x0
PROPERTIES:
  HeadHeapObject  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000040  System.Void .ctor(System.Int32 maxEntries)
  RVA=-1  // not resolved  token=0x6000042  System.Void Swap(System.Int32 A, System.Int32 B)
  RVA=-1  // not resolved  token=0x6000043  System.Void PushValue(System.Single h)
  RVA=-1  // not resolved  token=0x6000044  System.Void PushObj(T obj, System.Single h)
  RVA=-1  // not resolved  token=0x6000045  System.Single PopValue()
  RVA=-1  // not resolved  token=0x6000046  T PopObj()
  RVA=-1  // not resolved  token=0x6000047  T PopObj(System.Single& heapValue)
  RVA=-1  // not resolved  token=0x6000048  System.Void FlushResult(System.Collections.Generic.List<T> resultList, System.Collections.Generic.List<System.Single> heapList)
END_CLASS

CLASS: DataStructures.ViliWonka.Heap.MaxHeap
TYPE:  class
TOKEN: 0x200000A
SIZE:  0x28
EXTENDS: DataStructures.ViliWonka.Heap.BaseHeap
FIELDS:
METHODS:
  RVA=0x0B89E0B8  token=0x6000049  System.Void .ctor(System.Int32 initialSize)
  RVA=0x0B89E184  token=0x600004A  System.Void PushValue(System.Single h)
  RVA=0x0B89E0CC  token=0x600004B  System.Single PopValue()
END_CLASS

CLASS: DataStructures.ViliWonka.Heap.MaxHeap`1
TYPE:  class
TOKEN: 0x200000B
EXTENDS: DataStructures.ViliWonka.Heap.MaxHeap
FIELDS:
  private           T[]                             objs  // 0x0
  private           T                               tempObjs  // 0x0
PROPERTIES:
  HeadHeapObject  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600004C  System.Void .ctor(System.Int32 maxNodes)
  RVA=-1  // not resolved  token=0x600004E  System.Void Swap(System.Int32 A, System.Int32 B)
  RVA=-1  // not resolved  token=0x600004F  System.Void PushValue(System.Single h)
  RVA=-1  // not resolved  token=0x6000050  System.Single PopValue()
  RVA=-1  // not resolved  token=0x6000051  System.Void PushObj(T obj, System.Single h)
  RVA=-1  // not resolved  token=0x6000052  T PopObj()
  RVA=-1  // not resolved  token=0x6000053  T PopObj(System.Single& heapValue)
  RVA=-1  // not resolved  token=0x6000054  System.Void UpsizeHeap()
  RVA=-1  // not resolved  token=0x6000055  System.Void FlushResult(System.Collections.Generic.List<T> resultList, System.Collections.Generic.List<System.Single> heapList)
END_CLASS

CLASS: DataStructures.ViliWonka.Heap.MinHeap
TYPE:  class
TOKEN: 0x200000C
SIZE:  0x28
EXTENDS: DataStructures.ViliWonka.Heap.BaseHeap
FIELDS:
METHODS:
  RVA=0x0B89E0B8  token=0x6000056  System.Void .ctor(System.Int32 initialSize)
  RVA=0x0B89E2A8  token=0x6000057  System.Void PushValue(System.Single h)
  RVA=0x0B89E1F0  token=0x6000058  System.Single PopValue()
END_CLASS

CLASS: DataStructures.ViliWonka.Heap.MinHeap`1
TYPE:  class
TOKEN: 0x200000D
EXTENDS: DataStructures.ViliWonka.Heap.MinHeap
FIELDS:
  private           T[]                             objs  // 0x0
  private           T                               tempObjs  // 0x0
PROPERTIES:
  HeadHeapObject  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x6000059  System.Void .ctor(System.Int32 maxNodes)
  RVA=-1  // not resolved  token=0x600005B  System.Void Swap(System.Int32 A, System.Int32 B)
  RVA=-1  // not resolved  token=0x600005C  System.Void PushValue(System.Single h)
  RVA=-1  // not resolved  token=0x600005D  System.Single PopValue()
  RVA=-1  // not resolved  token=0x600005E  System.Void PushObj(T obj, System.Single h)
  RVA=-1  // not resolved  token=0x600005F  T PopObj()
  RVA=-1  // not resolved  token=0x6000060  T PopObj(System.Single& heapValue)
  RVA=-1  // not resolved  token=0x6000061  System.Void UpsizeHeap()
  RVA=-1  // not resolved  token=0x6000062  System.Void FlushResult(System.Collections.Generic.List<T> resultList, System.Collections.Generic.List<System.Single> heapList)
END_CLASS

CLASS: DataStructures.ViliWonka.KDTree.KDBounds
TYPE:  sealed struct
TOKEN: 0x2000002
SIZE:  0x28
FIELDS:
  public            UnityEngine.Vector3             min  // 0x10
  public            UnityEngine.Vector3             max  // 0x1c
PROPERTIES:
  size  get=0x0B89B550
  Bounds  get=0x0B89B464
METHODS:
  RVA=0x0B89B3E8  token=0x6000003  UnityEngine.Vector3 ClosestPoint(UnityEngine.Vector3 point)
END_CLASS

CLASS: DataStructures.ViliWonka.KDTree.KDNode
TYPE:  class
TOKEN: 0x2000003
SIZE:  0x48
FIELDS:
  public            System.Single                   partitionCoordinate  // 0x10
  public            System.Int32                    partitionAxis  // 0x14
  public            DataStructures.ViliWonka.KDTree.KDNodenegativeChild  // 0x18
  public            DataStructures.ViliWonka.KDTree.KDNodepositiveChild  // 0x20
  public            System.Int32                    start  // 0x28
  public            System.Int32                    end  // 0x2c
  public            DataStructures.ViliWonka.KDTree.KDBoundsbounds  // 0x30
PROPERTIES:
  Count  get=0x04DBF670
  Leaf  get=0x09300170
METHODS:
  RVA=0x04D8CCE0  token=0x6000006  System.Void .ctor()
END_CLASS

CLASS: DataStructures.ViliWonka.KDTree.KDQuery
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x30
FIELDS:
  protected         DataStructures.ViliWonka.KDTree.KDQueryNode[]queueArray  // 0x10
  protected         DataStructures.ViliWonka.Heap.MinHeap<DataStructures.ViliWonka.KDTree.KDQueryNode>minHeap  // 0x18
  protected         System.Int32                    count  // 0x20
  protected         System.Int32                    queryIndex  // 0x24
  private           System.Collections.Generic.SortedList<System.Int32,DataStructures.ViliWonka.Heap.KSmallestHeap<System.Int32>>_heaps  // 0x28
PROPERTIES:
  LeftToProcess  get=0x04DBF680
METHODS:
  RVA=0x0B89C4A4  token=0x6000007  DataStructures.ViliWonka.KDTree.KDQueryNode PushGetQueue()
  RVA=0x0B89C698  token=0x6000008  System.Void PushToQueue(DataStructures.ViliWonka.KDTree.KDNode node, UnityEngine.Vector3 tempClosestPoint)
  RVA=0x0B89C59C  token=0x6000009  System.Void PushToHeap(DataStructures.ViliWonka.KDTree.KDNode node, UnityEngine.Vector3 tempClosestPoint, UnityEngine.Vector3 queryPosition)
  RVA=0x0B89C470  token=0x600000B  DataStructures.ViliWonka.KDTree.KDQueryNode PopFromQueue()
  RVA=0x0B89C3DC  token=0x600000C  DataStructures.ViliWonka.KDTree.KDQueryNode PopFromHeap()
  RVA=0x0B89CAB8  token=0x600000D  System.Void Reset()
  RVA=0x0B89CADC  token=0x600000E  System.Void .ctor(System.Int32 queryNodesContainersInitialSize)
  RVA=0x0B89B990  token=0x600000F  System.Void DrawLastQuery()
  RVA=0x0B89B5A8  token=0x6000010  System.Void ClosestPoint(DataStructures.ViliWonka.KDTree.KDTree tree, UnityEngine.Vector3 queryPosition, System.Collections.Generic.List<System.Int32> resultIndices, System.Collections.Generic.List<System.Single> resultDistances)
  RVA=0x0B89BB0C  token=0x6000011  System.Void Interval(DataStructures.ViliWonka.KDTree.KDTree tree, UnityEngine.Vector3 min, UnityEngine.Vector3 max, System.Collections.Generic.List<System.Int32> resultIndices)
  RVA=0x0B89BF20  token=0x6000012  System.Void KNearest(DataStructures.ViliWonka.KDTree.KDTree tree, UnityEngine.Vector3 queryPosition, System.Int32 k, System.Collections.Generic.List<System.Int32> resultIndices, System.Collections.Generic.List<System.Single> resultDistances)
  RVA=0x0B89C6E8  token=0x6000013  System.Void Radius(DataStructures.ViliWonka.KDTree.KDTree tree, UnityEngine.Vector3 queryPosition, System.Single queryRadius, System.Collections.Generic.List<System.Int32> resultIndices)
END_CLASS

CLASS: DataStructures.ViliWonka.KDTree.KDQueryNode
TYPE:  class
TOKEN: 0x2000005
SIZE:  0x28
FIELDS:
  public            DataStructures.ViliWonka.KDTree.KDNodenode  // 0x10
  public            UnityEngine.Vector3             tempClosestPoint  // 0x18
  public            System.Single                   distance  // 0x24
METHODS:
  RVA=0x041E1670  token=0x6000014  System.Void .ctor()
  RVA=0x067B70A8  token=0x6000015  System.Void .ctor(DataStructures.ViliWonka.KDTree.KDNode node, UnityEngine.Vector3 tempClosestPoint)
END_CLASS

CLASS: DataStructures.ViliWonka.KDTree.KDTree
TYPE:  class
TOKEN: 0x2000006
SIZE:  0x40
FIELDS:
  private           DataStructures.ViliWonka.KDTree.KDNode<RootNode>k__BackingField  // 0x10
  private           UnityEngine.Vector3[]           points  // 0x18
  private           System.Int32[]                  permutation  // 0x20
  private           System.Int32                    <Count>k__BackingField  // 0x28
  private           System.Int32                    maxPointsPerLeafNode  // 0x2c
  private           DataStructures.ViliWonka.KDTree.KDNode[]kdNodesStack  // 0x30
  private           System.Int32                    kdNodesCount  // 0x38
PROPERTIES:
  RootNode  get=0x02B2ECC0  set=0x053908C0
  Points  get=0x0385B100
  Permutation  get=0x04D862C0
  Count  get=0x011F2A90  set=0x011F2AA0
METHODS:
  RVA=0x0B89DAE8  token=0x600001C  System.Void .ctor(System.Int32 maxPointsPerLeafNode)
  RVA=0x0B89DB98  token=0x600001D  System.Void .ctor(UnityEngine.Vector3[] points, System.Int32 maxPointsPerLeafNode)
  RVA=0x0B89CD3C  token=0x600001E  System.Void Build(UnityEngine.Vector3[] newPoints, System.Int32 maxPointsPerLeafNode)
  RVA=0x0B89CC5C  token=0x600001F  System.Void Build(System.Collections.Generic.List<UnityEngine.Vector3> newPoints, System.Int32 maxPointsPerLeafNode)
  RVA=0x0B89D7E0  token=0x6000020  System.Void Rebuild(System.Int32 maxPointsPerLeafNode)
  RVA=0x0B89D82C  token=0x6000021  System.Void SetCount(System.Int32 newSize)
  RVA=0x0B89CBD0  token=0x6000022  System.Void BuildTree()
  RVA=0x0B89D038  token=0x6000023  DataStructures.ViliWonka.KDTree.KDNode GetKDNode()
  RVA=0x04DBF690  token=0x6000024  System.Void ResetKDNodeStack()
  RVA=0x0B89D13C  token=0x6000025  DataStructures.ViliWonka.KDTree.KDBounds MakeBounds()
  RVA=0x0B89D8AC  token=0x6000026  System.Void SplitNode(DataStructures.ViliWonka.KDTree.KDNode parent)
  RVA=0x0B89CDE8  token=0x6000027  System.Single CalculatePivot(System.Int32 start, System.Int32 end, System.Single boundsStart, System.Single boundsEnd, System.Int32 axis)
  RVA=0x0B89D6CC  token=0x6000028  System.Int32 Partition(System.Int32 start, System.Int32 end, System.Single partitionPivot, System.Int32 axis)
  RVA=0x0B89D018  token=0x6000029  System.Boolean ContinueSplit(DataStructures.ViliWonka.KDTree.KDNode node)
END_CLASS

