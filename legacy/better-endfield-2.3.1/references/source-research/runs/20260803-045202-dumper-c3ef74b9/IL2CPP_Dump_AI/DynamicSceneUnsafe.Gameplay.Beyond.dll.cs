// ========================================================
// Dumped by @desirepro
// Assembly: DynamicSceneUnsafe.Gameplay.Beyond.dll
// Classes:  34
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

CLASS: EvalBatchDelegate
TYPE:  sealed class
TOKEN: 0x200000E
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x02F28950  token=0x6000023  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x02C394B0  token=0x6000024  System.Void Invoke(System.Single* cx, System.Single* cz, System.Single* br, System.Int32 count, Beyond.Gameplay.Core.DynamicScene.DynamicSceneVisCtx* ctxs, System.Int32 centerCount, System.Byte* outResult)
  RVA=0x0698B604  token=0x6000025  System.IAsyncResult BeginInvoke(System.Single* cx, System.Single* cz, System.Single* br, System.Int32 count, Beyond.Gameplay.Core.DynamicScene.DynamicSceneVisCtx* ctxs, System.Int32 centerCount, System.Byte* outResult, System.AsyncCallback callback, System.Object object)
  RVA=0x053DE22C  token=0x6000026  System.Void EndInvoke(System.IAsyncResult result)
END_CLASS

CLASS: $BurstDirectCallInitializer
TYPE:  static class
TOKEN: 0x200001E
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x04D7B4F0  token=0x6000071  System.Void Initialize()
END_CLASS

CLASS: Beyond.Gameplay.Core.DynamicScene.CompEntityBridge
TYPE:  class
TOKEN: 0x200000A
SIZE:  0x60
IMPLEMENTS: System.IDisposable
FIELDS:
  private   readonly Beyond.Gameplay.Core.DynamicScene.EDynamicSystemm_systemType  // 0x10
  private   readonly System.Boolean                  m_isRoot  // 0x11
  private   readonly System.Int32                    m_selfStride  // 0x14
  private           Beyond.Gameplay.Core.NativeSegmentPoolm_selfPool  // 0x18
  private           Beyond.Gameplay.Core.DynamicScene.IndexedArray<System.Int32>m_selfGridStarts  // 0x20
  private           Beyond.Gameplay.Core.DynamicScene.IndexedArray<Beyond.Gameplay.Core.SegmentHandle>m_selfGridHandles  // 0x28
  private           Beyond.Gameplay.Core.DynamicScene.CompEntityLookupm_lookup  // 0x30
  private           System.Boolean                  m_disposed  // 0x38
  private   readonly Beyond.Gameplay.Core.DynamicScene.GridSlotManagerm_gridSlotManager  // 0x40
  private   readonly Beyond.Gameplay.Core.NativeSegmentPoolm_rootPoolInner  // 0x48
  private   readonly Beyond.Gameplay.Core.DynamicScene.IndexedArray<System.Int32>m_rootGridStarts  // 0x50
  private   readonly System.Int32                    m_rootStride  // 0x58
METHODS:
  RVA=0x034AFE40  token=0x6000009  System.Void .ctor(Beyond.Gameplay.Core.DynamicScene.EDynamicSystem systemType, System.Int32 selfStride, Beyond.Gameplay.Core.DynamicScene.GridSlotManager gridSlotManager, Beyond.Gameplay.Core.NativeSegmentPool<Beyond.Gameplay.Core.DynamicScene.RootCompData> rootPool, Beyond.Gameplay.Core.DynamicScene.IndexedArray<System.Int32> rootGridStarts)
  RVA=0x0698B010  token=0x600000A  System.UInt64 GetEntityId(System.UInt64 compId)
  RVA=0x0698AFF4  token=0x600000B  System.UInt64 GetCompId(System.UInt64 entityId)
  RVA=0x0698B0EC  token=0x600000C  System.Boolean HasRegisterComp(System.UInt64 compId)
  RVA=0x0698B138  token=0x600000D  System.Boolean HasRegisterEntity(System.UInt64 entityId)
  RVA=0x033637D0  token=0x600000E  System.Void RegisterEntityToComp(System.UInt64 entityId, System.UInt64 compId)
  RVA=0x03364220  token=0x600000F  System.Void UnRegisterCompToEntity(System.UInt64 entityId, System.UInt64 compId)
  RVA=0x0698AFEC  token=0x6000010  System.Void EnsureSelfPoolSegment(System.UInt16 hotSlot, System.Int32 minCapacity)
  RVA=-1  // generic def  token=0x6000011  T& GetSelfSlot(System.UInt64 compId)
  RVA=-1  // generic def  token=0x6000012  T& GetSelfSlotDirect(System.UInt16 hotSlot, System.UInt32 gridIndex)
  RVA=0x0698B02C  token=0x6000013  System.Int32 GetSelfPoolCapacity(System.UInt16 hotSlot)
  RVA=0x034B15F0  token=0x6000014  System.Void FreeSelfPoolSegment(System.UInt16 hotSlot)
  RVA=0x034B1860  token=0x6000015  System.Void _AllocSelfPoolSegment(System.UInt16 hotSlot, System.Int32 minCapacity)
  RVA=0x034B2240  token=0x6000016  System.Void _OnSelfSegmentRelocated(System.UInt32 gridId, System.Int32 newStart)
  RVA=0x0698AECC  token=0x6000017  System.Void Dispose()
END_CLASS

CLASS: Beyond.Gameplay.Core.DynamicScene.CompEntityLookup
TYPE:  class
TOKEN: 0x200000B
SIZE:  0x40
FIELDS:
  private   static  System.Boolean                  s_layoutVerified  // static @ 0x0
  private   readonly Beyond.Gameplay.Core.DynamicScene.EDynamicSystemm_systemType  // 0x10
  private   readonly System.Boolean                  m_isRoot  // 0x11
  private   readonly System.Int32                    m_stride  // 0x14
  private   readonly System.Int32                    m_rootStride  // 0x18
  private   readonly Beyond.Gameplay.Core.NativeSegmentPoolm_selfPool  // 0x20
  private   readonly Beyond.Gameplay.Core.DynamicScene.IndexedArray<System.Int32>m_selfGridStarts  // 0x28
  private   readonly Beyond.Gameplay.Core.NativeSegmentPoolm_rootPool  // 0x30
  private   readonly Beyond.Gameplay.Core.DynamicScene.IndexedArray<System.Int32>m_rootGridStarts  // 0x38
METHODS:
  RVA=0x034B0260  token=0x6000018  System.Void _VerifyFArray8Layout()
  RVA=0x034B01C0  token=0x6000019  System.Void .ctor(Beyond.Gameplay.Core.DynamicScene.EDynamicSystem systemType, System.Int32 stride, Beyond.Gameplay.Core.NativeSegmentPool selfPool, Beyond.Gameplay.Core.DynamicScene.IndexedArray<System.Int32> selfGridStarts, System.Int32 rootStride, Beyond.Gameplay.Core.NativeSegmentPool rootPool, Beyond.Gameplay.Core.DynamicScene.IndexedArray<System.Int32> rootGridStarts)
  RVA=0x0698B184  token=0x600001A  System.UInt64 GetCompId(System.UInt64 entityId)
  RVA=0x0698B228  token=0x600001B  System.UInt64 GetEntityId(System.UInt64 compId)
  RVA=0x0698B3E0  token=0x600001C  System.Boolean HasRegisterEntity(System.UInt64 entityId)
  RVA=0x0698B2F0  token=0x600001D  System.Boolean HasRegisterComp(System.UInt64 compId)
END_CLASS

CLASS: Beyond.Gameplay.Core.DynamicScene.DynamicSceneVisCtx
TYPE:  sealed struct
TOKEN: 0x200000C
SIZE:  0x34
FIELDS:
  public    readonly System.Single                   ccx  // 0x10
  public    readonly System.Single                   ccz  // 0x14
  public    readonly System.Single                   g  // 0x18
  public    readonly System.Single                   gf  // 0x1c
  public    readonly System.Single                   oneMinusF  // 0x20
  public    readonly System.Single                   d  // 0x24
  public    readonly System.Single                   h  // 0x28
  public    readonly System.Single                   marginMin  // 0x2c
  public    readonly System.Single                   marginMax  // 0x30
METHODS:
  RVA=0x04D8CB70  token=0x600001E  System.Void .ctor(System.Single ccx, System.Single ccz, System.Single g, System.Single gf, System.Single oneMinusF, System.Single d, System.Single h, System.Single marginMin, System.Single marginMax)
END_CLASS

CLASS: Beyond.Gameplay.Core.DynamicScene.EvalBatch_00000020$PostfixBurstDelegate
TYPE:  sealed class
TOKEN: 0x200000F
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0698B9E8  token=0x6000027  System.Void .ctor(System.Object , System.IntPtr )
  RVA=0x02C394B0  token=0x6000028  System.Void Invoke(System.Single* cx, System.Single* cz, System.Single* br, System.Int32 count, Beyond.Gameplay.Core.DynamicScene.DynamicSceneVisCtx* ctxs, System.Int32 centerCount, System.Byte* outResult)
  RVA=0x0698B918  token=0x6000029  System.IAsyncResult BeginInvoke(System.Single* cx, System.Single* cz, System.Single* br, System.Int32 count, Beyond.Gameplay.Core.DynamicScene.DynamicSceneVisCtx* ctxs, System.Int32 centerCount, System.Byte* outResult, System.AsyncCallback , System.Object )
  RVA=0x053DE22C  token=0x600002A  System.Void EndInvoke(System.IAsyncResult )
END_CLASS

CLASS: Beyond.Gameplay.Core.DynamicScene.EvalBatch_00000020$BurstDirectCall
TYPE:  static class
TOKEN: 0x2000010
SIZE:  0x10
FIELDS:
  private   static  System.IntPtr                   Pointer  // static @ 0x0
  private   static  System.IntPtr                   DeferredCompilation  // static @ 0x8
METHODS:
  RVA=0x0698B6D4  token=0x600002B  System.Void GetFunctionPointerDiscard(System.IntPtr& )
  RVA=0x0698B7D8  token=0x600002C  System.IntPtr GetFunctionPointer()
  RVA=0x03FB1850  token=0x600002D  System.Void Constructor()
  RVA=0x041E1670  token=0x600002E  System.Void Initialize()
  RVA=0x03FB2B60  token=0x600002F  System.Void .cctor()
  RVA=0x0698B82C  token=0x6000030  System.Void Invoke(System.Single* cx, System.Single* cz, System.Single* br, System.Int32 count, Beyond.Gameplay.Core.DynamicScene.DynamicSceneVisCtx* ctxs, System.Int32 centerCount, System.Byte* outResult)
END_CLASS

CLASS: Beyond.Gameplay.Core.DynamicScene.DynamicSceneVisibilityKernel
TYPE:  static class
TOKEN: 0x200000D
SIZE:  0x10
FIELDS:
  private   static  Unity.Burst.FunctionPointer<Beyond.Gameplay.Core.DynamicScene.DynamicSceneVisibilityKernel.EvalBatchDelegate>s_evalBatch  // static @ 0x0
  private   static  Beyond.Gameplay.Core.DynamicScene.DynamicSceneVisibilityKernel.EvalBatchDelegates_evalBatchInvoke  // static @ 0x8
  private   static  System.Boolean                  s_evalBatchCompiled  // static @ 0x10
METHODS:
  RVA=0x0698B578  token=0x600001F  System.Boolean EvalCore(System.Single cx, System.Single cz, System.Single br, Beyond.Gameplay.Core.DynamicScene.DynamicSceneVisCtx& ctx, System.Boolean useExpanded)
  RVA=0x0698B4E4  token=0x6000020  System.Void EvalBatch(System.Single* cx, System.Single* cz, System.Single* br, System.Int32 count, Beyond.Gameplay.Core.DynamicScene.DynamicSceneVisCtx* ctxs, System.Int32 centerCount, System.Byte* outResult)
  RVA=0x03E32EB0  token=0x6000021  System.Void RunBatch(Unity.Collections.NativeArray<System.Single> cx, Unity.Collections.NativeArray<System.Single> cz, Unity.Collections.NativeArray<System.Single> br, System.Int32 count, Unity.Collections.NativeArray<Beyond.Gameplay.Core.DynamicScene.DynamicSceneVisCtx> ctxs, System.Int32 centerCount, Unity.Collections.NativeArray<System.Byte> result)
  RVA=0x0698AD9C  token=0x6000022  System.Void EvalBatch$BurstManaged(System.Single* cx, System.Single* cz, System.Single* br, System.Int32 count, Beyond.Gameplay.Core.DynamicScene.DynamicSceneVisCtx* ctxs, System.Int32 centerCount, System.Byte* outResult)
END_CLASS

CLASS: Beyond.Gameplay.Core.DynamicScene.EDynamicSystem
TYPE:  sealed struct
TOKEN: 0x2000011
SIZE:  0x11
FIELDS:
  public            System.Byte                     value__  // 0x10
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemNone  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemEntity  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemDynamicEntity  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemHittable  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemDestruct  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemTrigger  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemSpaceShip  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemSeat  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemEnableLight  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemNatureResource  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemTree  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemPhysic  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemErosion  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemGlobalVarControl  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemMissionControl  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemDynamicEntityControl  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemFactoryRegionControl  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemSettlementControl  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemScriptControl  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemInteractiveControl  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemNavmeshModify  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemBlockFactory  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemResourceGroup  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemModel  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemEcs  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemEffect  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemModelEcs  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemModelViewController  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemId  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemConveyorBelt  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemConveyorBeltBox  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemConveyorBeltGroup  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemSludge  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemExtraScene  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemRemapScene  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemWaterPipe  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemStreamingArea  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemNavmeshObstacle  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemMapVarControl  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemActivityControl  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemPoiControl  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemBlightMiasma  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemPureSystem  // const
  public    static  Beyond.Gameplay.Core.DynamicScene.EDynamicSystemMax  // const
METHODS:
END_CLASS

CLASS: Beyond.Gameplay.Core.DynamicScene.FArray8UlongOps
TYPE:  static class
TOKEN: 0x2000012
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0698BABC  token=0x6000031  System.Void Add(System.Byte* entityDataPtr, System.UInt64 compId)
  RVA=0x0698BB84  token=0x6000032  System.Void Remove(System.Byte* entityDataPtr, System.UInt64 compId)
  RVA=0x0698BB58  token=0x6000033  System.UInt64 FindBySysType(System.Byte* entityDataPtr, System.Byte sysType)
END_CLASS

CLASS: Beyond.Gameplay.Core.DynamicScene.GridSlotIdHelper
TYPE:  static class
TOKEN: 0x2000013
SIZE:  0x10
FIELDS:
  private   static  System.Int32                    FREE_BIT_SHIFT_NUM  // const
  private   static  System.UInt32                   FREE_BIT_MASK  // const
METHODS:
  RVA=0x04D87570  token=0x6000034  System.UInt32 GetUniqueIdFromGridId(System.UInt32 gridId)
  RVA=0x04D8CBF0  token=0x6000035  System.UInt64 Encode(System.Byte uniqueId, System.Byte sysType, System.UInt16 hotSlot, System.UInt32 gridIndex)
  RVA=0x04D88BE0  token=0x6000036  System.UInt32 GetGridIndex(System.UInt64 compId)
  RVA=0x04D8CC20  token=0x6000037  System.UInt16 GetHotSlot(System.UInt64 compId)
  RVA=0x04D8CC30  token=0x6000038  System.Byte GetSysType(System.UInt64 compId)
  RVA=0x04D8CC40  token=0x6000039  System.Byte GetUniqueId(System.UInt64 compId)
  RVA=0x04D8CBD0  token=0x600003A  System.Void Decode(System.UInt64 compId, System.UInt16& hotSlot, System.Int32& gridIndex)
  RVA=0x04D8CBD0  token=0x600003B  System.Void DecodeColdSlotEncodeId(System.UInt64 coldSlotEncode, System.UInt16& coldSlot, System.Int32& gridIndex)
  RVA=0x04D8CBE0  token=0x600003C  System.UInt64 EncodeSlotIndex(System.UInt16 hotSlot, System.Int32 index)
  RVA=0x04D8CC50  token=0x600003D  System.UInt64 MakeEntityId(System.Byte uniqueId, System.UInt16 hotSlot, System.UInt32 rootGridIndex)
END_CLASS

CLASS: Beyond.Gameplay.Core.DynamicScene.ColdGridData
TYPE:  sealed struct
TOKEN: 0x2000014
SIZE:  0x1C
FIELDS:
  public            System.UInt32                   gridId  // 0x10
  public            System.UInt16                   hotSlot  // 0x14
  public            System.Byte                     uniqueId  // 0x16
  public            System.Int32                    refCount  // 0x18
METHODS:
END_CLASS

CLASS: Beyond.Gameplay.Core.DynamicScene.HotGridData
TYPE:  sealed struct
TOKEN: 0x2000015
SIZE:  0x1C
FIELDS:
  public            System.UInt32                   gridId  // 0x10
  public            System.UInt16                   coldSlot  // 0x14
  public            System.Byte                     uniqueId  // 0x16
  public            System.Int32                    capacity  // 0x18
METHODS:
END_CLASS

CLASS: Beyond.Gameplay.Core.DynamicScene.SlotPool`1
TYPE:  sealed class
TOKEN: 0x2000016
FIELDS:
  private           T[]                             m_pool  // 0x0
  private           System.UInt16[]                 m_freeStack  // 0x0
  private           System.Boolean[]                m_allocated  // 0x0
  private           System.Int32                    m_freeTop  // 0x0
  private           System.UInt16                   m_nextSlot  // 0x0
PROPERTIES:
  NextSlot  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600003E  System.Void .ctor(System.Int32 capacity)
  RVA=-1  // not resolved  token=0x6000040  System.UInt16 Alloc()
  RVA=-1  // not resolved  token=0x6000041  System.Void Free(System.UInt16 slot)
  RVA=-1  // not resolved  token=0x6000042  System.Boolean IsAllocated(System.UInt16 slot)
  RVA=-1  // not resolved  token=0x6000043  T& Get(System.UInt16 slot)
  RVA=-1  // not resolved  token=0x6000044  System.Boolean IsValidIndex(System.UInt16 slot)
  RVA=-1  // not resolved  token=0x6000045  System.Void Dispose()
END_CLASS

CLASS: Beyond.Gameplay.Core.DynamicScene.GridSlotManager
TYPE:  class
TOKEN: 0x2000017
SIZE:  0x28
IMPLEMENTS: System.IDisposable
FIELDS:
  private   readonly Beyond.Gameplay.Core.DynamicScene.SlotPool<Beyond.Gameplay.Core.DynamicScene.ColdGridData>m_coldSlots  // 0x10
  private   readonly Beyond.Gameplay.Core.DynamicScene.SlotPool<Beyond.Gameplay.Core.DynamicScene.HotGridData>m_hotSlots  // 0x18
  private   readonly System.Collections.Generic.Dictionary<System.UInt32,System.UInt32>m_gridToSlots  // 0x20
  private   static  System.Int32                    HOT_SHIFT  // const
  private   static  System.UInt32                   COLD_MASK  // const
METHODS:
  RVA=0x03FD58C0  token=0x6000046  System.Void .ctor(System.Int32 coldCapacity, System.Int32 hotCapacity)
  RVA=0x0328E930  token=0x6000047  System.UInt16 AllocCold(System.UInt32 gridId)
  RVA=0x03587210  token=0x6000048  System.Void FreeCold(System.UInt16 coldSlot)
  RVA=0x033643D0  token=0x6000049  System.UInt16 AddColdRef(System.UInt32 gridId)
  RVA=0x03587110  token=0x600004A  System.Void RemoveColdRef(System.UInt32 gridId)
  RVA=0x0698C21C  token=0x600004B  System.Boolean IsColdRefFree(System.UInt16 coldSlot)
  RVA=0x0698C320  token=0x600004C  System.UInt16 TryGetOrAllocColdSlot(System.UInt32 gridId)
  RVA=0x03589840  token=0x600004D  System.UInt16 AllocHot(System.UInt32 gridId, System.Int32 capacity)
  RVA=0x03586FF0  token=0x600004E  System.Void FreeHot(System.UInt16 hotSlot)
  RVA=0x0698C080  token=0x600004F  Beyond.Gameplay.Core.DynamicScene.HotGridData& GetHotData(System.UInt16 hotSlot)
  RVA=0x0698BD50  token=0x6000050  Beyond.Gameplay.Core.DynamicScene.ColdGridData& GetColdData(System.UInt16 coldSlot)
  RVA=0x0698BE74  token=0x6000051  System.UInt16 GetColdSlot(System.UInt32 gridId)
  RVA=0x0698C1A4  token=0x6000052  System.UInt16 GetHotSlot(System.UInt32 gridId)
  RVA=0x0698BCC0  token=0x6000053  System.Void GetBothSlotForGrid(System.UInt32 gridId, System.UInt16& coldSlot, System.UInt16& hotSlot)
  RVA=0x0698BEE8  token=0x6000054  System.UInt32 GetGridIdByCold(System.UInt16 coldSlot)
  RVA=0x0698C0D4  token=0x6000055  System.UInt16 GetHotSlotByCold(System.UInt16 coldSlot)
  RVA=0x0698BDA4  token=0x6000056  System.UInt16 GetColdSlotByHot(System.UInt16 hotSlot)
  RVA=0x0698BFB4  token=0x6000057  System.UInt32 GetGridIdByHot(System.UInt16 hotSlot)
  RVA=0x0698C278  token=0x6000058  System.Boolean IsColdValid(System.UInt16 coldSlot)
  RVA=0x0698C2CC  token=0x6000059  System.Boolean IsHotValid(System.UInt16 hotSlot)
  RVA=0x0698BC08  token=0x600005A  System.Void Dispose()
END_CLASS

CLASS: Beyond.Gameplay.Core.DynamicScene.IndexedArray`1
TYPE:  class
TOKEN: 0x2000018
IMPLEMENTS: System.IDisposable
FIELDS:
  private           T[]                             data  // 0x0
  private   readonly T                               m_fill  // 0x0
PROPERTIES:
  Item  get=-1  // not resolved  set=-1  // not resolved
  Length  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x600005B  System.Void .ctor(System.Int32 initialCapacity, T fillValue)
  RVA=-1  // not resolved  token=0x600005F  T& GetRef(System.Int32 index)
  RVA=-1  // not resolved  token=0x6000060  System.Void Dispose()
  RVA=-1  // not resolved  token=0x6000061  System.Void EnsureCapacity(System.Int32 minCapacity)
END_CLASS

CLASS: Beyond.Gameplay.Core.DynamicScene.RootCompData
TYPE:  sealed struct
TOKEN: 0x2000019
SIZE:  0x70
FIELDS:
  public            Beyond.FArray8<System.UInt64>   compIds  // 0x10
  public            System.UInt32                   currentLogicState  // 0x58
  public            Beyond.Gameplay.Core.DynamicScene.EDynamicSystemrootSystem  // 0x5c
  public            Beyond.Gameplay.Core.DynamicScene.EDynamicSystemcontrolSys  // 0x5d
  public            System.Byte                     currentLoaded  // 0x5e
  public            System.Byte                     needLazyDestroy  // 0x5f
  public            System.Int32                    level  // 0x60
  public            System.Byte                     occupied  // 0x64
  public            System.Byte                     stateEnable  // 0x65
  public            System.Byte                     areaEnable  // 0x66
  public            System.Byte                     _pad0  // 0x67
  public            System.Byte                     _pad1  // 0x68
  public            System.Byte                     _pad2  // 0x69
  public            System.Byte                     _pad3  // 0x6a
  public            System.Byte                     _pad4  // 0x6b
PROPERTIES:
  isSceneVisible  get=0x014F5220
METHODS:
END_CLASS

CLASS: Beyond.Gameplay.Core.DynamicScene.RootCompDataUtil
TYPE:  static class
TOKEN: 0x200001A
SIZE:  0x10
FIELDS:
  public    static readonly System.Int32                    STRIDE  // static @ 0x0
METHODS:
  RVA=0x043222F0  token=0x6000063  System.Boolean IsSlotOccupied(Beyond.Gameplay.Core.DynamicScene.RootCompData& data)
  RVA=0x041CD8C0  token=0x6000064  System.Void ClearSlot(Beyond.Gameplay.Core.DynamicScene.RootCompData& data)
  RVA=0x04D14A40  token=0x6000065  System.Void .cctor()
END_CLASS

CLASS: Beyond.Gameplay.Core.DynamicScene.RootPoolUnsafeHelpers
TYPE:  static class
TOKEN: 0x200001B
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x034B0780  token=0x6000066  System.Void ClearSegment(Beyond.Gameplay.Core.NativeSegmentPool<Beyond.Gameplay.Core.DynamicScene.RootCompData> pool, Beyond.Gameplay.Core.SegmentHandle handle, System.Int32 capacity)
END_CLASS

CLASS: Beyond.Gameplay.Core.DynamicScene.GridSegmentSlot
TYPE:  sealed struct
TOKEN: 0x200001C
SIZE:  0x18
FIELDS:
  public            System.Int32                    start  // 0x10
  public            Beyond.Gameplay.Core.SegmentHandlehandle  // 0x14
PROPERTIES:
  IsAllocated  get=0x0698BBC0
METHODS:
END_CLASS

CLASS: Beyond.Gameplay.Core.DynamicScene.SystemSegmentPool`1
TYPE:  class
TOKEN: 0x200001D
IMPLEMENTS: System.IDisposable
FIELDS:
  private   static readonly System.Int32                    STRIDE  // static @ 0x0
  private           Beyond.Gameplay.Core.NativeSegmentPoolm_pool  // 0x0
  private           Beyond.Gameplay.Core.DynamicScene.IndexedArray<Beyond.Gameplay.Core.DynamicScene.GridSegmentSlot>m_gridSlots  // 0x0
  private           Beyond.Gameplay.Core.DynamicScene.GridSlotManagerm_gridSlotManager  // 0x0
METHODS:
  RVA=-1  // not resolved  token=0x6000068  System.Void .ctor(Beyond.Gameplay.Core.DynamicScene.GridSlotManager gridSlotManager, System.Int32 initialElementCapacity)
  RVA=-1  // not resolved  token=0x6000069  System.Void EnsureAllocated(System.UInt32 gridId, System.UInt16 hotSlot, System.Int32 elementCount)
  RVA=-1  // not resolved  token=0x600006A  System.Void Free(System.UInt16 hotSlot)
  RVA=-1  // not resolved  token=0x600006B  T& GetSlot(System.UInt16 hotSlot, System.Int32 elementIndex)
  RVA=-1  // not resolved  token=0x600006C  System.Void SetSlot(System.UInt16 hotSlot, System.Int32 elementIndex, T data)
  RVA=-1  // not resolved  token=0x600006D  System.Boolean IsAllocated(System.UInt16 hotSlot)
  RVA=-1  // not resolved  token=0x600006E  System.Void _OnSegmentRelocated(System.UInt32 gridId, System.Int32 newStart)
  RVA=-1  // not resolved  token=0x600006F  System.Void Dispose()
  RVA=-1  // not resolved  token=0x6000070  System.Void .cctor()
END_CLASS

CLASS: Beyond.SourceGenerator.CameraControlConfigAttribute
TYPE:  class
TOKEN: 0x2000005
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000004  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.AnimatorBlackboardUtility
TYPE:  static class
TOKEN: 0x2000006
SIZE:  0x10
FIELDS:
  private   static  System.Boolean                  s_enableFastPathBuffer  // static @ 0x0
METHODS:
  RVA=-1  // generic def  token=0x6000005  System.Void ClearAndEnsureDictionaryCapacity(System.Collections.Generic.Dictionary<TKey,TValue>& dict, System.Int32 capacity)
END_CLASS

CLASS: Beyond.SourceGenerator.AnimatorBlackboardAttribute
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000006  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.DataNAttribute
TYPE:  class
TOKEN: 0x2000008
SIZE:  0x20
EXTENDS: System.Attribute
FIELDS:
  public            System.Int32                    capacity  // 0x10
  public            System.Int32                    tSize  // 0x14
  public            System.Boolean                  ring  // 0x18
METHODS:
  RVA=0x041E1670  token=0x6000007  System.Void .ctor()
END_CLASS

CLASS: Beyond.SourceGenerator.ECSComponentAttribute
TYPE:  class
TOKEN: 0x2000009
SIZE:  0x18
EXTENDS: System.Attribute
FIELDS:
  public            System.Runtime.InteropServices.LayoutKindlayoutKind  // 0x10
  public            System.Boolean                  isTag  // 0x14
METHODS:
  RVA=0x041E1670  token=0x6000008  System.Void .ctor()
END_CLASS

CLASS: HG.Rendering.Runtime.HGConstantBufferLayoutAttribute
TYPE:  class
TOKEN: 0x2000004
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000003  System.Void .ctor()
END_CLASS

CLASS: IFix.ILFixDynamicMethodWrapper
TYPE:  class
TOKEN: 0x200001F
SIZE:  0x28
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
  private           System.Int32                    methodId  // 0x18
  private           System.Object                   anonObj  // 0x20
  public    static  IFix.ILFixDynamicMethodWrapper[]wrapperArray  // static @ 0x0
METHODS:
  RVA=0x032B2C30  token=0x6000072  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj)
  RVA=0x0698C354  token=0x6000073  System.Void __Gen_Wrap_0(System.Object P0, System.UInt64 P1, System.UInt64 P2)
  RVA=0x0698C880  token=0x6000074  System.Void __Gen_Wrap_1(System.Object P0, System.UInt16 P1)
  RVA=0x0698C95C  token=0x6000075  System.Void __Gen_Wrap_2(System.Object P0, System.UInt16 P1, System.Int32 P2)
  RVA=0x066C2400  token=0x6000076  System.Void __Gen_Wrap_3(System.Object P0, System.UInt32 P1, System.Int32 P2)
  RVA=0x066C296C  token=0x6000077  System.Void __Gen_Wrap_4(System.Object P0)
  RVA=0x063625A4  token=0x6000078  System.Void __Gen_Wrap_5()
  RVA=0x0698CA54  token=0x6000079  System.Void __Gen_Wrap_6(Unity.Collections.NativeArray<System.Single> P0, Unity.Collections.NativeArray<System.Single> P1, Unity.Collections.NativeArray<System.Single> P2, System.Int32 P3, Unity.Collections.NativeArray<Beyond.Gameplay.Core.DynamicScene.DynamicSceneVisCtx> P4, System.Int32 P5, Unity.Collections.NativeArray<System.Byte> P6)
  RVA=0x066C13F0  token=0x600007A  System.UInt16 __Gen_Wrap_7(System.Object P0, System.UInt32 P1)
  RVA=0x066F3838  token=0x600007B  System.Void __Gen_Wrap_8(System.Object P0, System.UInt32 P1)
  RVA=0x0698CC40  token=0x600007C  System.UInt16 __Gen_Wrap_9(System.Object P0, System.UInt32 P1, System.Int32 P2)
  RVA=0x0698C44C  token=0x600007D  System.Boolean __Gen_Wrap_10(Beyond.Gameplay.Core.DynamicScene.RootCompData& P0)
  RVA=0x0698C5E0  token=0x600007E  System.Void __Gen_Wrap_11(Beyond.Gameplay.Core.DynamicScene.RootCompData& P0)
  RVA=0x0698C764  token=0x600007F  System.Void __Gen_Wrap_12(System.Object P0, Beyond.Gameplay.Core.SegmentHandle P1, System.Int32 P2)
  RVA=0x04D37B80  token=0x6000080  System.Void .cctor()
END_CLASS

CLASS: IFix.ILFixInterfaceBridge
TYPE:  class
TOKEN: 0x2000020
SIZE:  0x40
EXTENDS: IFix.Core.AnonymousStorey
FIELDS:
METHODS:
  RVA=0x0698CD44  token=0x6000081  System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine)
  RVA=0x041E1670  token=0x6000082  System.Void RefAsyncBuilderStartMethod()
END_CLASS

CLASS: IFix.WrappersManagerImpl
TYPE:  class
TOKEN: 0x2000021
SIZE:  0x18
IMPLEMENTS: IFix.Core.WrappersManager
FIELDS:
  private           IFix.Core.VirtualMachine        virtualMachine  // 0x10
METHODS:
  RVA=0x053908C0  token=0x6000083  System.Void .ctor(IFix.Core.VirtualMachine virtualMachine)
  RVA=0x0698CFC0  token=0x6000084  IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id)
  RVA=0x033641B0  token=0x6000085  System.Boolean IsPatched(System.Int32 id)
  RVA=0x0698CE7C  token=0x6000086  System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon)
  RVA=0x0698CF48  token=0x6000087  System.Object CreateWrapper(System.Int32 id)
  RVA=0x0698D024  token=0x6000088  System.Object InitWrapperArray(System.Int32 len)
  RVA=0x0698CDD8  token=0x6000089  IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine)
END_CLASS

CLASS: IFix.IDMAP0
TYPE:  sealed struct
TOKEN: 0x2000022
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-DynamicScene-CompEntityBridge-RegisterEntityToComp0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-DynamicScene-CompEntityBridge-UnRegisterCompToEntity0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-DynamicScene-CompEntityBridge-FreeSelfPoolSegment0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-DynamicScene-CompEntityBridge-_AllocSelfPoolSegment0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-DynamicScene-CompEntityBridge-_OnSelfSegmentRelocated0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-DynamicScene-CompEntityBridge-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-DynamicScene-CompEntityLookup-_VerifyFArray8Layout0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-DynamicScene-DynamicSceneVisibilityKernel-RunBatch0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-DynamicScene-GridSlotManager-AllocCold0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-DynamicScene-GridSlotManager-FreeCold0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-DynamicScene-GridSlotManager-AddColdRef0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-DynamicScene-GridSlotManager-RemoveColdRef0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-DynamicScene-GridSlotManager-AllocHot0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-DynamicScene-GridSlotManager-FreeHot0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-DynamicScene-GridSlotManager-Dispose0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-DynamicScene-RootCompData-get_isSceneVisible0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-DynamicScene-RootCompDataUtil-IsSlotOccupied0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-DynamicScene-RootCompDataUtil-ClearSlot0  // const
  public    static  IFix.IDMAP0                     Beyond-Gameplay-Core-DynamicScene-RootPoolUnsafeHelpers-ClearSegment0  // const
METHODS:
END_CLASS

CLASS: Microsoft.CodeAnalysis.EmbeddedAttribute
TYPE:  sealed class
TOKEN: 0x2000002
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000001  System.Void .ctor()
END_CLASS

CLASS: System.Runtime.CompilerServices.IsUnmanagedAttribute
TYPE:  sealed class
TOKEN: 0x2000003
SIZE:  0x10
EXTENDS: System.Attribute
FIELDS:
METHODS:
  RVA=0x041E1670  token=0x6000002  System.Void .ctor()
END_CLASS

