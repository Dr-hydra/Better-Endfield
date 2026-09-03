// ========================================================
// Dumped by @desirepro
// Assembly: DynamicSceneUnsafe.Gameplay.Beyond.dll
// Classes:  34
// Module:   GameAssembly.dll  base=0x7FFA73900000
// Date:     Aug  3 2026 04:44:03
// ========================================================

using System;
using System.Collections.Generic;

    // TypeToken: 0x2000001  // size: 0x1
    public class <Module>
    {
    }

    // TypeToken: 0x200000E  // size: 0x80
    public sealed class EvalBatchDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x02F28950  token: 0x6000023
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x02C394B0  token: 0x6000024
        public virtual System.Void Invoke(System.Single* cx, System.Single* cz, System.Single* br, System.Int32 count, Beyond.Gameplay.Core.DynamicScene.DynamicSceneVisCtx* ctxs, System.Int32 centerCount, System.Byte* outResult) { }
        // RVA: 0x0698B604  token: 0x6000025
        public virtual System.IAsyncResult BeginInvoke(System.Single* cx, System.Single* cz, System.Single* br, System.Int32 count, Beyond.Gameplay.Core.DynamicScene.DynamicSceneVisCtx* ctxs, System.Int32 centerCount, System.Byte* outResult, System.AsyncCallback callback, System.Object object) { }
        // RVA: 0x053DE22C  token: 0x6000026
        public virtual System.Void EndInvoke(System.IAsyncResult result) { }

    }

    // TypeToken: 0x200001E  // size: 0x10
    public static class $BurstDirectCallInitializer
    {
        // Methods
        // RVA: 0x04D7B4F0  token: 0x6000071
        private static System.Void Initialize() { }

    }

namespace Beyond.Gameplay.Core.DynamicScene
{

    // TypeToken: 0x200000A  // size: 0x60
    public class CompEntityBridge : System.IDisposable
    {
        // Fields
        private readonly Beyond.Gameplay.Core.DynamicScene.EDynamicSystem m_systemType;  // 0x10
        private readonly System.Boolean m_isRoot;  // 0x11
        private readonly System.Int32 m_selfStride;  // 0x14
        private Beyond.Gameplay.Core.NativeSegmentPool m_selfPool;  // 0x18
        private Beyond.Gameplay.Core.DynamicScene.IndexedArray<System.Int32> m_selfGridStarts;  // 0x20
        private Beyond.Gameplay.Core.DynamicScene.IndexedArray<Beyond.Gameplay.Core.SegmentHandle> m_selfGridHandles;  // 0x28
        private Beyond.Gameplay.Core.DynamicScene.CompEntityLookup m_lookup;  // 0x30
        private System.Boolean m_disposed;  // 0x38
        private readonly Beyond.Gameplay.Core.DynamicScene.GridSlotManager m_gridSlotManager;  // 0x40
        private readonly Beyond.Gameplay.Core.NativeSegmentPool m_rootPoolInner;  // 0x48
        private readonly Beyond.Gameplay.Core.DynamicScene.IndexedArray<System.Int32> m_rootGridStarts;  // 0x50
        private readonly System.Int32 m_rootStride;  // 0x58

        // Methods
        // RVA: 0x034AFE40  token: 0x6000009
        public System.Void .ctor(Beyond.Gameplay.Core.DynamicScene.EDynamicSystem systemType, System.Int32 selfStride, Beyond.Gameplay.Core.DynamicScene.GridSlotManager gridSlotManager, Beyond.Gameplay.Core.NativeSegmentPool<Beyond.Gameplay.Core.DynamicScene.RootCompData> rootPool, Beyond.Gameplay.Core.DynamicScene.IndexedArray<System.Int32> rootGridStarts) { }
        // RVA: 0x0698B010  token: 0x600000A
        public System.UInt64 GetEntityId(System.UInt64 compId) { }
        // RVA: 0x0698AFF4  token: 0x600000B
        public System.UInt64 GetCompId(System.UInt64 entityId) { }
        // RVA: 0x0698B0EC  token: 0x600000C
        public System.Boolean HasRegisterComp(System.UInt64 compId) { }
        // RVA: 0x0698B138  token: 0x600000D
        public System.Boolean HasRegisterEntity(System.UInt64 entityId) { }
        // RVA: 0x033637D0  token: 0x600000E
        public System.Void RegisterEntityToComp(System.UInt64 entityId, System.UInt64 compId) { }
        // RVA: 0x03364220  token: 0x600000F
        public System.Void UnRegisterCompToEntity(System.UInt64 entityId, System.UInt64 compId) { }
        // RVA: 0x0698AFEC  token: 0x6000010
        public System.Void EnsureSelfPoolSegment(System.UInt16 hotSlot, System.Int32 minCapacity) { }
        // RVA: -1  // generic def  token: 0x6000011
        public T& GetSelfSlot(System.UInt64 compId) { }
        // RVA: -1  // generic def  token: 0x6000012
        public T& GetSelfSlotDirect(System.UInt16 hotSlot, System.UInt32 gridIndex) { }
        // RVA: 0x0698B02C  token: 0x6000013
        public System.Int32 GetSelfPoolCapacity(System.UInt16 hotSlot) { }
        // RVA: 0x034B15F0  token: 0x6000014
        public System.Void FreeSelfPoolSegment(System.UInt16 hotSlot) { }
        // RVA: 0x034B1860  token: 0x6000015
        private System.Void _AllocSelfPoolSegment(System.UInt16 hotSlot, System.Int32 minCapacity) { }
        // RVA: 0x034B2240  token: 0x6000016
        private System.Void _OnSelfSegmentRelocated(System.UInt32 gridId, System.Int32 newStart) { }
        // RVA: 0x0698AECC  token: 0x6000017
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x200000B  // size: 0x40
    public class CompEntityLookup
    {
        // Fields
        private static System.Boolean s_layoutVerified;  // static @ 0x0
        private readonly Beyond.Gameplay.Core.DynamicScene.EDynamicSystem m_systemType;  // 0x10
        private readonly System.Boolean m_isRoot;  // 0x11
        private readonly System.Int32 m_stride;  // 0x14
        private readonly System.Int32 m_rootStride;  // 0x18
        private readonly Beyond.Gameplay.Core.NativeSegmentPool m_selfPool;  // 0x20
        private readonly Beyond.Gameplay.Core.DynamicScene.IndexedArray<System.Int32> m_selfGridStarts;  // 0x28
        private readonly Beyond.Gameplay.Core.NativeSegmentPool m_rootPool;  // 0x30
        private readonly Beyond.Gameplay.Core.DynamicScene.IndexedArray<System.Int32> m_rootGridStarts;  // 0x38

        // Methods
        // RVA: 0x034B0260  token: 0x6000018
        private static System.Void _VerifyFArray8Layout() { }
        // RVA: 0x034B01C0  token: 0x6000019
        public System.Void .ctor(Beyond.Gameplay.Core.DynamicScene.EDynamicSystem systemType, System.Int32 stride, Beyond.Gameplay.Core.NativeSegmentPool selfPool, Beyond.Gameplay.Core.DynamicScene.IndexedArray<System.Int32> selfGridStarts, System.Int32 rootStride, Beyond.Gameplay.Core.NativeSegmentPool rootPool, Beyond.Gameplay.Core.DynamicScene.IndexedArray<System.Int32> rootGridStarts) { }
        // RVA: 0x0698B184  token: 0x600001A
        public System.UInt64 GetCompId(System.UInt64 entityId) { }
        // RVA: 0x0698B228  token: 0x600001B
        public System.UInt64 GetEntityId(System.UInt64 compId) { }
        // RVA: 0x0698B3E0  token: 0x600001C
        public System.Boolean HasRegisterEntity(System.UInt64 entityId) { }
        // RVA: 0x0698B2F0  token: 0x600001D
        public System.Boolean HasRegisterComp(System.UInt64 compId) { }

    }

    // TypeToken: 0x200000C  // size: 0x34
    public sealed struct DynamicSceneVisCtx
    {
        // Fields
        public readonly System.Single ccx;  // 0x10
        public readonly System.Single ccz;  // 0x14
        public readonly System.Single g;  // 0x18
        public readonly System.Single gf;  // 0x1c
        public readonly System.Single oneMinusF;  // 0x20
        public readonly System.Single d;  // 0x24
        public readonly System.Single h;  // 0x28
        public readonly System.Single marginMin;  // 0x2c
        public readonly System.Single marginMax;  // 0x30

        // Methods
        // RVA: 0x04D8CB70  token: 0x600001E
        public System.Void .ctor(System.Single ccx, System.Single ccz, System.Single g, System.Single gf, System.Single oneMinusF, System.Single d, System.Single h, System.Single marginMin, System.Single marginMax) { }

    }

    // TypeToken: 0x200000F  // size: 0x80
    public sealed class EvalBatch_00000020$PostfixBurstDelegate : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0698B9E8  token: 0x6000027
        public System.Void .ctor(System.Object , System.IntPtr ) { }
        // RVA: 0x02C394B0  token: 0x6000028
        public virtual System.Void Invoke(System.Single* cx, System.Single* cz, System.Single* br, System.Int32 count, Beyond.Gameplay.Core.DynamicScene.DynamicSceneVisCtx* ctxs, System.Int32 centerCount, System.Byte* outResult) { }
        // RVA: 0x0698B918  token: 0x6000029
        public virtual System.IAsyncResult BeginInvoke(System.Single* cx, System.Single* cz, System.Single* br, System.Int32 count, Beyond.Gameplay.Core.DynamicScene.DynamicSceneVisCtx* ctxs, System.Int32 centerCount, System.Byte* outResult, System.AsyncCallback , System.Object ) { }
        // RVA: 0x053DE22C  token: 0x600002A
        public virtual System.Void EndInvoke(System.IAsyncResult ) { }

    }

    // TypeToken: 0x2000010  // size: 0x10
    public static class EvalBatch_00000020$BurstDirectCall
    {
        // Fields
        private static System.IntPtr Pointer;  // static @ 0x0
        private static System.IntPtr DeferredCompilation;  // static @ 0x8

        // Methods
        // RVA: 0x0698B6D4  token: 0x600002B
        private static System.Void GetFunctionPointerDiscard(System.IntPtr& ) { }
        // RVA: 0x0698B7D8  token: 0x600002C
        private static System.IntPtr GetFunctionPointer() { }
        // RVA: 0x03FB1850  token: 0x600002D
        public static System.Void Constructor() { }
        // RVA: 0x041E1670  token: 0x600002E
        public static System.Void Initialize() { }
        // RVA: 0x03FB2B60  token: 0x600002F
        private static System.Void .cctor() { }
        // RVA: 0x0698B82C  token: 0x6000030
        public static System.Void Invoke(System.Single* cx, System.Single* cz, System.Single* br, System.Int32 count, Beyond.Gameplay.Core.DynamicScene.DynamicSceneVisCtx* ctxs, System.Int32 centerCount, System.Byte* outResult) { }

    }

    // TypeToken: 0x200000D  // size: 0x10
    public static class DynamicSceneVisibilityKernel
    {
        // Fields
        private static Unity.Burst.FunctionPointer<Beyond.Gameplay.Core.DynamicScene.DynamicSceneVisibilityKernel.EvalBatchDelegate> s_evalBatch;  // static @ 0x0
        private static Beyond.Gameplay.Core.DynamicScene.DynamicSceneVisibilityKernel.EvalBatchDelegate s_evalBatchInvoke;  // static @ 0x8
        private static System.Boolean s_evalBatchCompiled;  // static @ 0x10

        // Methods
        // RVA: 0x0698B578  token: 0x600001F
        public static System.Boolean EvalCore(System.Single cx, System.Single cz, System.Single br, Beyond.Gameplay.Core.DynamicScene.DynamicSceneVisCtx& ctx, System.Boolean useExpanded) { }
        // RVA: 0x0698B4E4  token: 0x6000020
        public static System.Void EvalBatch(System.Single* cx, System.Single* cz, System.Single* br, System.Int32 count, Beyond.Gameplay.Core.DynamicScene.DynamicSceneVisCtx* ctxs, System.Int32 centerCount, System.Byte* outResult) { }
        // RVA: 0x03E32EB0  token: 0x6000021
        public static System.Void RunBatch(Unity.Collections.NativeArray<System.Single> cx, Unity.Collections.NativeArray<System.Single> cz, Unity.Collections.NativeArray<System.Single> br, System.Int32 count, Unity.Collections.NativeArray<Beyond.Gameplay.Core.DynamicScene.DynamicSceneVisCtx> ctxs, System.Int32 centerCount, Unity.Collections.NativeArray<System.Byte> result) { }
        // RVA: 0x0698AD9C  token: 0x6000022
        public static System.Void EvalBatch$BurstManaged(System.Single* cx, System.Single* cz, System.Single* br, System.Int32 count, Beyond.Gameplay.Core.DynamicScene.DynamicSceneVisCtx* ctxs, System.Int32 centerCount, System.Byte* outResult) { }

    }

    // TypeToken: 0x2000011  // size: 0x11
    public sealed struct EDynamicSystem
    {
        // Fields
        public System.Byte value__;  // 0x10
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem None;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem Entity;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem DynamicEntity;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem Hittable;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem Destruct;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem Trigger;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem SpaceShip;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem Seat;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem EnableLight;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem NatureResource;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem Tree;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem Physic;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem Erosion;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem GlobalVarControl;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem MissionControl;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem DynamicEntityControl;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem FactoryRegionControl;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem SettlementControl;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem ScriptControl;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem InteractiveControl;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem NavmeshModify;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem BlockFactory;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem ResourceGroup;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem Model;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem Ecs;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem Effect;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem ModelEcs;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem ModelViewController;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem Id;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem ConveyorBelt;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem ConveyorBeltBox;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem ConveyorBeltGroup;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem Sludge;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem ExtraScene;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem RemapScene;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem WaterPipe;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem StreamingArea;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem NavmeshObstacle;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem MapVarControl;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem ActivityControl;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem PoiControl;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem BlightMiasma;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem PureSystem;  // const
        public static Beyond.Gameplay.Core.DynamicScene.EDynamicSystem Max;  // const

    }

    // TypeToken: 0x2000012  // size: 0x10
    public static class FArray8UlongOps
    {
        // Methods
        // RVA: 0x0698BABC  token: 0x6000031
        public static System.Void Add(System.Byte* entityDataPtr, System.UInt64 compId) { }
        // RVA: 0x0698BB84  token: 0x6000032
        public static System.Void Remove(System.Byte* entityDataPtr, System.UInt64 compId) { }
        // RVA: 0x0698BB58  token: 0x6000033
        public static System.UInt64 FindBySysType(System.Byte* entityDataPtr, System.Byte sysType) { }

    }

    // TypeToken: 0x2000013  // size: 0x10
    public static class GridSlotIdHelper
    {
        // Fields
        private static System.Int32 FREE_BIT_SHIFT_NUM;  // const
        private static System.UInt32 FREE_BIT_MASK;  // const

        // Methods
        // RVA: 0x04D87570  token: 0x6000034
        public static System.UInt32 GetUniqueIdFromGridId(System.UInt32 gridId) { }
        // RVA: 0x04D8CBF0  token: 0x6000035
        public static System.UInt64 Encode(System.Byte uniqueId, System.Byte sysType, System.UInt16 hotSlot, System.UInt32 gridIndex) { }
        // RVA: 0x04D88BE0  token: 0x6000036
        public static System.UInt32 GetGridIndex(System.UInt64 compId) { }
        // RVA: 0x04D8CC20  token: 0x6000037
        public static System.UInt16 GetHotSlot(System.UInt64 compId) { }
        // RVA: 0x04D8CC30  token: 0x6000038
        public static System.Byte GetSysType(System.UInt64 compId) { }
        // RVA: 0x04D8CC40  token: 0x6000039
        public static System.Byte GetUniqueId(System.UInt64 compId) { }
        // RVA: 0x04D8CBD0  token: 0x600003A
        public static System.Void Decode(System.UInt64 compId, System.UInt16& hotSlot, System.Int32& gridIndex) { }
        // RVA: 0x04D8CBD0  token: 0x600003B
        public static System.Void DecodeColdSlotEncodeId(System.UInt64 coldSlotEncode, System.UInt16& coldSlot, System.Int32& gridIndex) { }
        // RVA: 0x04D8CBE0  token: 0x600003C
        public static System.UInt64 EncodeSlotIndex(System.UInt16 hotSlot, System.Int32 index) { }
        // RVA: 0x04D8CC50  token: 0x600003D
        public static System.UInt64 MakeEntityId(System.Byte uniqueId, System.UInt16 hotSlot, System.UInt32 rootGridIndex) { }

    }

    // TypeToken: 0x2000014  // size: 0x1C
    public sealed struct ColdGridData
    {
        // Fields
        public System.UInt32 gridId;  // 0x10
        public System.UInt16 hotSlot;  // 0x14
        public System.Byte uniqueId;  // 0x16
        public System.Int32 refCount;  // 0x18

    }

    // TypeToken: 0x2000015  // size: 0x1C
    public sealed struct HotGridData
    {
        // Fields
        public System.UInt32 gridId;  // 0x10
        public System.UInt16 coldSlot;  // 0x14
        public System.Byte uniqueId;  // 0x16
        public System.Int32 capacity;  // 0x18

    }

    // TypeToken: 0x2000016
    public sealed class SlotPool`1
    {
        // Fields
        private T[] m_pool;  // 0x0
        private System.UInt16[] m_freeStack;  // 0x0
        private System.Boolean[] m_allocated;  // 0x0
        private System.Int32 m_freeTop;  // 0x0
        private System.UInt16 m_nextSlot;  // 0x0

        // Properties
        System.UInt16 NextSlot { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600003E
        public System.Void .ctor(System.Int32 capacity) { }
        // RVA: -1  // not resolved  token: 0x6000040
        public System.UInt16 Alloc() { }
        // RVA: -1  // not resolved  token: 0x6000041
        public System.Void Free(System.UInt16 slot) { }
        // RVA: -1  // not resolved  token: 0x6000042
        public System.Boolean IsAllocated(System.UInt16 slot) { }
        // RVA: -1  // not resolved  token: 0x6000043
        public T& Get(System.UInt16 slot) { }
        // RVA: -1  // not resolved  token: 0x6000044
        public System.Boolean IsValidIndex(System.UInt16 slot) { }
        // RVA: -1  // not resolved  token: 0x6000045
        public System.Void Dispose() { }

    }

    // TypeToken: 0x2000017  // size: 0x28
    public class GridSlotManager : System.IDisposable
    {
        // Fields
        private readonly Beyond.Gameplay.Core.DynamicScene.SlotPool<Beyond.Gameplay.Core.DynamicScene.ColdGridData> m_coldSlots;  // 0x10
        private readonly Beyond.Gameplay.Core.DynamicScene.SlotPool<Beyond.Gameplay.Core.DynamicScene.HotGridData> m_hotSlots;  // 0x18
        private readonly System.Collections.Generic.Dictionary<System.UInt32,System.UInt32> m_gridToSlots;  // 0x20
        private static System.Int32 HOT_SHIFT;  // const
        private static System.UInt32 COLD_MASK;  // const

        // Methods
        // RVA: 0x03FD58C0  token: 0x6000046
        public System.Void .ctor(System.Int32 coldCapacity, System.Int32 hotCapacity) { }
        // RVA: 0x0328E930  token: 0x6000047
        public System.UInt16 AllocCold(System.UInt32 gridId) { }
        // RVA: 0x03587210  token: 0x6000048
        public System.Void FreeCold(System.UInt16 coldSlot) { }
        // RVA: 0x033643D0  token: 0x6000049
        public System.UInt16 AddColdRef(System.UInt32 gridId) { }
        // RVA: 0x03587110  token: 0x600004A
        public System.Void RemoveColdRef(System.UInt32 gridId) { }
        // RVA: 0x0698C21C  token: 0x600004B
        public System.Boolean IsColdRefFree(System.UInt16 coldSlot) { }
        // RVA: 0x0698C320  token: 0x600004C
        public System.UInt16 TryGetOrAllocColdSlot(System.UInt32 gridId) { }
        // RVA: 0x03589840  token: 0x600004D
        public System.UInt16 AllocHot(System.UInt32 gridId, System.Int32 capacity) { }
        // RVA: 0x03586FF0  token: 0x600004E
        public System.Void FreeHot(System.UInt16 hotSlot) { }
        // RVA: 0x0698C080  token: 0x600004F
        public Beyond.Gameplay.Core.DynamicScene.HotGridData& GetHotData(System.UInt16 hotSlot) { }
        // RVA: 0x0698BD50  token: 0x6000050
        public Beyond.Gameplay.Core.DynamicScene.ColdGridData& GetColdData(System.UInt16 coldSlot) { }
        // RVA: 0x0698BE74  token: 0x6000051
        public System.UInt16 GetColdSlot(System.UInt32 gridId) { }
        // RVA: 0x0698C1A4  token: 0x6000052
        public System.UInt16 GetHotSlot(System.UInt32 gridId) { }
        // RVA: 0x0698BCC0  token: 0x6000053
        public System.Void GetBothSlotForGrid(System.UInt32 gridId, System.UInt16& coldSlot, System.UInt16& hotSlot) { }
        // RVA: 0x0698BEE8  token: 0x6000054
        public System.UInt32 GetGridIdByCold(System.UInt16 coldSlot) { }
        // RVA: 0x0698C0D4  token: 0x6000055
        public System.UInt16 GetHotSlotByCold(System.UInt16 coldSlot) { }
        // RVA: 0x0698BDA4  token: 0x6000056
        public System.UInt16 GetColdSlotByHot(System.UInt16 hotSlot) { }
        // RVA: 0x0698BFB4  token: 0x6000057
        public System.UInt32 GetGridIdByHot(System.UInt16 hotSlot) { }
        // RVA: 0x0698C278  token: 0x6000058
        public System.Boolean IsColdValid(System.UInt16 coldSlot) { }
        // RVA: 0x0698C2CC  token: 0x6000059
        public System.Boolean IsHotValid(System.UInt16 hotSlot) { }
        // RVA: 0x0698BC08  token: 0x600005A
        public virtual System.Void Dispose() { }

    }

    // TypeToken: 0x2000018
    public class IndexedArray`1 : System.IDisposable
    {
        // Fields
        private T[] data;  // 0x0
        private readonly T m_fill;  // 0x0

        // Properties
        T Item { get; /* RVA: -1  // not resolved */ set; /* RVA: -1  // not resolved */ }
        System.Int32 Length { get; /* RVA: -1  // not resolved */ }

        // Methods
        // RVA: -1  // not resolved  token: 0x600005B
        public System.Void .ctor(System.Int32 initialCapacity, T fillValue) { }
        // RVA: -1  // not resolved  token: 0x600005F
        public T& GetRef(System.Int32 index) { }
        // RVA: -1  // not resolved  token: 0x6000060
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000061
        public System.Void EnsureCapacity(System.Int32 minCapacity) { }

    }

    // TypeToken: 0x2000019  // size: 0x70
    public sealed struct RootCompData
    {
        // Fields
        public Beyond.FArray8<System.UInt64> compIds;  // 0x10
        public System.UInt32 currentLogicState;  // 0x58
        public Beyond.Gameplay.Core.DynamicScene.EDynamicSystem rootSystem;  // 0x5c
        public Beyond.Gameplay.Core.DynamicScene.EDynamicSystem controlSys;  // 0x5d
        public System.Byte currentLoaded;  // 0x5e
        public System.Byte needLazyDestroy;  // 0x5f
        public System.Int32 level;  // 0x60
        public System.Byte occupied;  // 0x64
        public System.Byte stateEnable;  // 0x65
        public System.Byte areaEnable;  // 0x66
        public System.Byte _pad0;  // 0x67
        public System.Byte _pad1;  // 0x68
        public System.Byte _pad2;  // 0x69
        public System.Byte _pad3;  // 0x6a
        public System.Byte _pad4;  // 0x6b

        // Properties
        System.Boolean isSceneVisible { get; /* RVA: 0x014F5220 */ }

    }

    // TypeToken: 0x200001A  // size: 0x10
    public static class RootCompDataUtil
    {
        // Fields
        public static readonly System.Int32 STRIDE;  // static @ 0x0

        // Methods
        // RVA: 0x043222F0  token: 0x6000063
        public static System.Boolean IsSlotOccupied(Beyond.Gameplay.Core.DynamicScene.RootCompData& data) { }
        // RVA: 0x041CD8C0  token: 0x6000064
        public static System.Void ClearSlot(Beyond.Gameplay.Core.DynamicScene.RootCompData& data) { }
        // RVA: 0x04D14A40  token: 0x6000065
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x200001B  // size: 0x10
    public static class RootPoolUnsafeHelpers
    {
        // Methods
        // RVA: 0x034B0780  token: 0x6000066
        public static System.Void ClearSegment(Beyond.Gameplay.Core.NativeSegmentPool<Beyond.Gameplay.Core.DynamicScene.RootCompData> pool, Beyond.Gameplay.Core.SegmentHandle handle, System.Int32 capacity) { }

    }

    // TypeToken: 0x200001C  // size: 0x18
    public sealed struct GridSegmentSlot
    {
        // Fields
        public System.Int32 start;  // 0x10
        public Beyond.Gameplay.Core.SegmentHandle handle;  // 0x14

        // Properties
        System.Boolean IsAllocated { get; /* RVA: 0x0698BBC0 */ }

    }

    // TypeToken: 0x200001D
    public class SystemSegmentPool`1 : System.IDisposable
    {
        // Fields
        private static readonly System.Int32 STRIDE;  // static @ 0x0
        private Beyond.Gameplay.Core.NativeSegmentPool m_pool;  // 0x0
        private Beyond.Gameplay.Core.DynamicScene.IndexedArray<Beyond.Gameplay.Core.DynamicScene.GridSegmentSlot> m_gridSlots;  // 0x0
        private Beyond.Gameplay.Core.DynamicScene.GridSlotManager m_gridSlotManager;  // 0x0

        // Methods
        // RVA: -1  // not resolved  token: 0x6000068
        public System.Void .ctor(Beyond.Gameplay.Core.DynamicScene.GridSlotManager gridSlotManager, System.Int32 initialElementCapacity) { }
        // RVA: -1  // not resolved  token: 0x6000069
        public System.Void EnsureAllocated(System.UInt32 gridId, System.UInt16 hotSlot, System.Int32 elementCount) { }
        // RVA: -1  // not resolved  token: 0x600006A
        public System.Void Free(System.UInt16 hotSlot) { }
        // RVA: -1  // not resolved  token: 0x600006B
        public T& GetSlot(System.UInt16 hotSlot, System.Int32 elementIndex) { }
        // RVA: -1  // not resolved  token: 0x600006C
        public System.Void SetSlot(System.UInt16 hotSlot, System.Int32 elementIndex, T data) { }
        // RVA: -1  // not resolved  token: 0x600006D
        public System.Boolean IsAllocated(System.UInt16 hotSlot) { }
        // RVA: -1  // not resolved  token: 0x600006E
        private System.Void _OnSegmentRelocated(System.UInt32 gridId, System.Int32 newStart) { }
        // RVA: -1  // not resolved  token: 0x600006F
        public virtual System.Void Dispose() { }
        // RVA: -1  // not resolved  token: 0x6000070
        private static System.Void .cctor() { }

    }

}

namespace Beyond.SourceGenerator
{

    // TypeToken: 0x2000005  // size: 0x10
    public class CameraControlConfigAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000004
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000006  // size: 0x10
    public static class AnimatorBlackboardUtility
    {
        // Fields
        private static System.Boolean s_enableFastPathBuffer;  // static @ 0x0

        // Methods
        // RVA: -1  // generic def  token: 0x6000005
        private static System.Void ClearAndEnsureDictionaryCapacity(System.Collections.Generic.Dictionary<TKey,TValue>& dict, System.Int32 capacity) { }

    }

    // TypeToken: 0x2000007  // size: 0x10
    public class AnimatorBlackboardAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000006
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000008  // size: 0x20
    public class DataNAttribute : System.Attribute
    {
        // Fields
        public System.Int32 capacity;  // 0x10
        public System.Int32 tSize;  // 0x14
        public System.Boolean ring;  // 0x18

        // Methods
        // RVA: 0x041E1670  token: 0x6000007
        public System.Void .ctor() { }

    }

    // TypeToken: 0x2000009  // size: 0x18
    public class ECSComponentAttribute : System.Attribute
    {
        // Fields
        public System.Runtime.InteropServices.LayoutKind layoutKind;  // 0x10
        public System.Boolean isTag;  // 0x14

        // Methods
        // RVA: 0x041E1670  token: 0x6000008
        public System.Void .ctor() { }

    }

}

namespace HG.Rendering.Runtime
{

    // TypeToken: 0x2000004  // size: 0x10
    public class HGConstantBufferLayoutAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000003
        public System.Void .ctor() { }

    }

}

namespace IFix
{

    // TypeToken: 0x200001F  // size: 0x28
    public class ILFixDynamicMethodWrapper
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10
        private System.Int32 methodId;  // 0x18
        private System.Object anonObj;  // 0x20
        public static IFix.ILFixDynamicMethodWrapper[] wrapperArray;  // static @ 0x0

        // Methods
        // RVA: 0x032B2C30  token: 0x6000072
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine, System.Int32 methodId, System.Object anonObj) { }
        // RVA: 0x0698C354  token: 0x6000073
        public System.Void __Gen_Wrap_0(System.Object P0, System.UInt64 P1, System.UInt64 P2) { }
        // RVA: 0x0698C880  token: 0x6000074
        public System.Void __Gen_Wrap_1(System.Object P0, System.UInt16 P1) { }
        // RVA: 0x0698C95C  token: 0x6000075
        public System.Void __Gen_Wrap_2(System.Object P0, System.UInt16 P1, System.Int32 P2) { }
        // RVA: 0x066C2400  token: 0x6000076
        public System.Void __Gen_Wrap_3(System.Object P0, System.UInt32 P1, System.Int32 P2) { }
        // RVA: 0x066C296C  token: 0x6000077
        public System.Void __Gen_Wrap_4(System.Object P0) { }
        // RVA: 0x063625A4  token: 0x6000078
        public System.Void __Gen_Wrap_5() { }
        // RVA: 0x0698CA54  token: 0x6000079
        public System.Void __Gen_Wrap_6(Unity.Collections.NativeArray<System.Single> P0, Unity.Collections.NativeArray<System.Single> P1, Unity.Collections.NativeArray<System.Single> P2, System.Int32 P3, Unity.Collections.NativeArray<Beyond.Gameplay.Core.DynamicScene.DynamicSceneVisCtx> P4, System.Int32 P5, Unity.Collections.NativeArray<System.Byte> P6) { }
        // RVA: 0x066C13F0  token: 0x600007A
        public System.UInt16 __Gen_Wrap_7(System.Object P0, System.UInt32 P1) { }
        // RVA: 0x066F3838  token: 0x600007B
        public System.Void __Gen_Wrap_8(System.Object P0, System.UInt32 P1) { }
        // RVA: 0x0698CC40  token: 0x600007C
        public System.UInt16 __Gen_Wrap_9(System.Object P0, System.UInt32 P1, System.Int32 P2) { }
        // RVA: 0x0698C44C  token: 0x600007D
        public System.Boolean __Gen_Wrap_10(Beyond.Gameplay.Core.DynamicScene.RootCompData& P0) { }
        // RVA: 0x0698C5E0  token: 0x600007E
        public System.Void __Gen_Wrap_11(Beyond.Gameplay.Core.DynamicScene.RootCompData& P0) { }
        // RVA: 0x0698C764  token: 0x600007F
        public System.Void __Gen_Wrap_12(System.Object P0, Beyond.Gameplay.Core.SegmentHandle P1, System.Int32 P2) { }
        // RVA: 0x04D37B80  token: 0x6000080
        private static System.Void .cctor() { }

    }

    // TypeToken: 0x2000020  // size: 0x40
    public class ILFixInterfaceBridge : IFix.Core.AnonymousStorey
    {
        // Methods
        // RVA: 0x0698CD44  token: 0x6000081
        public System.Void .ctor(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] methodIdArray, IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x041E1670  token: 0x6000082
        public System.Void RefAsyncBuilderStartMethod() { }

    }

    // TypeToken: 0x2000021  // size: 0x18
    public class WrappersManagerImpl : IFix.Core.WrappersManager
    {
        // Fields
        private IFix.Core.VirtualMachine virtualMachine;  // 0x10

        // Methods
        // RVA: 0x053908C0  token: 0x6000083
        public System.Void .ctor(IFix.Core.VirtualMachine virtualMachine) { }
        // RVA: 0x0698CFC0  token: 0x6000084
        public static IFix.ILFixDynamicMethodWrapper GetPatch(System.Int32 id) { }
        // RVA: 0x033641B0  token: 0x6000085
        public static System.Boolean IsPatched(System.Int32 id) { }
        // RVA: 0x0698CE7C  token: 0x6000086
        public virtual System.Delegate CreateDelegate(System.Type type, System.Int32 id, System.Object anon) { }
        // RVA: 0x0698CF48  token: 0x6000087
        public virtual System.Object CreateWrapper(System.Int32 id) { }
        // RVA: 0x0698D024  token: 0x6000088
        public virtual System.Object InitWrapperArray(System.Int32 len) { }
        // RVA: 0x0698CDD8  token: 0x6000089
        public virtual IFix.Core.AnonymousStorey CreateBridge(System.Int32 fieldNum, System.Int32[] fieldTypes, System.Int32 typeIndex, System.Int32[] vTable, System.Int32[] slots, IFix.Core.VirtualMachine virtualMachine) { }

    }

    // TypeToken: 0x2000022  // size: 0x14
    public sealed struct IDMAP0
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static IFix.IDMAP0 Beyond-Gameplay-Core-DynamicScene-CompEntityBridge-RegisterEntityToComp0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Core-DynamicScene-CompEntityBridge-UnRegisterCompToEntity0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Core-DynamicScene-CompEntityBridge-FreeSelfPoolSegment0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Core-DynamicScene-CompEntityBridge-_AllocSelfPoolSegment0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Core-DynamicScene-CompEntityBridge-_OnSelfSegmentRelocated0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Core-DynamicScene-CompEntityBridge-Dispose0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Core-DynamicScene-CompEntityLookup-_VerifyFArray8Layout0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Core-DynamicScene-DynamicSceneVisibilityKernel-RunBatch0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Core-DynamicScene-GridSlotManager-AllocCold0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Core-DynamicScene-GridSlotManager-FreeCold0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Core-DynamicScene-GridSlotManager-AddColdRef0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Core-DynamicScene-GridSlotManager-RemoveColdRef0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Core-DynamicScene-GridSlotManager-AllocHot0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Core-DynamicScene-GridSlotManager-FreeHot0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Core-DynamicScene-GridSlotManager-Dispose0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Core-DynamicScene-RootCompData-get_isSceneVisible0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Core-DynamicScene-RootCompDataUtil-IsSlotOccupied0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Core-DynamicScene-RootCompDataUtil-ClearSlot0;  // const
        public static IFix.IDMAP0 Beyond-Gameplay-Core-DynamicScene-RootPoolUnsafeHelpers-ClearSegment0;  // const

    }

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

namespace System.Runtime.CompilerServices
{

    // TypeToken: 0x2000003  // size: 0x10
    public sealed class IsUnmanagedAttribute : System.Attribute
    {
        // Methods
        // RVA: 0x041E1670  token: 0x6000002
        public System.Void .ctor() { }

    }

}

