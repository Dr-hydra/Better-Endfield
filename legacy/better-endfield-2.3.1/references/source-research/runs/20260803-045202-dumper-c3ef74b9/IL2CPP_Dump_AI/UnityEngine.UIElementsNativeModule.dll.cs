// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.UIElementsNativeModule.dll
// Classes:  32
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

CLASS: GPUBufferType
TYPE:  sealed struct
TOKEN: 0x200001F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UIElements.UIR.Utility.GPUBufferTypeVertex  // const
  public    static  UnityEngine.UIElements.UIR.Utility.GPUBufferTypeIndex  // const
METHODS:
END_CLASS

CLASS: GPUBuffer`1
TYPE:  class
TOKEN: 0x2000020
IMPLEMENTS: System.IDisposable
FIELDS:
  private           System.IntPtr                   buffer  // 0x0
  private           System.Int32                    elemCount  // 0x0
  private           System.Int32                    elemStride  // 0x0
PROPERTIES:
  ElementStride  get=-1  // not resolved
  BufferPointer  get=-1  // not resolved
METHODS:
  RVA=-1  // not resolved  token=0x60000F1  System.Void .ctor(System.Int32 elementCount, UnityEngine.UIElements.UIR.Utility.GPUBufferType type)
  RVA=-1  // not resolved  token=0x60000F2  System.Void Dispose()
  RVA=-1  // not resolved  token=0x60000F3  System.Void UpdateRanges(Unity.Collections.NativeSlice<UnityEngine.UIElements.UIR.GfxUpdateBufferRange> ranges, System.Int32 rangesMin, System.Int32 rangesMax)
END_CLASS

CLASS: UnityEngine.UIElements.TextNativeSettings
TYPE:  sealed struct
TOKEN: 0x2000018
SIZE:  0x50
FIELDS:
  public            System.String                   text  // 0x10
  public            UnityEngine.Font                font  // 0x18
  public            System.Int32                    size  // 0x20
  public            System.Single                   scaling  // 0x24
  public            UnityEngine.FontStyle           style  // 0x28
  public            UnityEngine.Color               color  // 0x2c
  public            UnityEngine.TextAnchor          anchor  // 0x3c
  public            System.Boolean                  wordWrap  // 0x40
  public            System.Single                   wordWrapWidth  // 0x44
  public            System.Boolean                  richText  // 0x48
METHODS:
END_CLASS

CLASS: UnityEngine.UIElements.TextVertex
TYPE:  sealed struct
TOKEN: 0x2000019
SIZE:  0x28
FIELDS:
  public            UnityEngine.Vector3             position  // 0x10
  public            UnityEngine.Color32             color  // 0x1c
  public            UnityEngine.Vector2             uv0  // 0x20
METHODS:
END_CLASS

CLASS: UnityEngine.UIElements.TextNative
TYPE:  static class
TOKEN: 0x200001A
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B564BDC  token=0x60000AD  UnityEngine.Vector2 GetCursorPosition(UnityEngine.UIElements.TextNativeSettings settings, UnityEngine.Rect rect, System.Int32 cursorIndex)
  RVA=0x0B564954  token=0x60000AE  System.Single ComputeTextWidth(UnityEngine.UIElements.TextNativeSettings settings)
  RVA=0x0B564854  token=0x60000AF  System.Single ComputeTextHeight(UnityEngine.UIElements.TextNativeSettings settings)
  RVA=0x0B564ECC  token=0x60000B0  Unity.Collections.NativeArray<UnityEngine.UIElements.TextVertex> GetVertices(UnityEngine.UIElements.TextNativeSettings settings)
  RVA=0x0B564CF8  token=0x60000B1  UnityEngine.Vector2 GetOffset(UnityEngine.UIElements.TextNativeSettings settings, UnityEngine.Rect screenRect)
  RVA=0x0B558DE0  token=0x60000B2  System.Single ComputeTextScaling(UnityEngine.Matrix4x4 worldMatrix, System.Single pixelsPerPoint)
  RVA=0x0B564ACC  token=0x60000B3  System.Single DoComputeTextWidth(UnityEngine.UIElements.TextNativeSettings settings)
  RVA=0x0B564A88  token=0x60000B4  System.Single DoComputeTextHeight(UnityEngine.UIElements.TextNativeSettings settings)
  RVA=0x0B564B40  token=0x60000B5  UnityEngine.Vector2 DoGetCursorPosition(UnityEngine.UIElements.TextNativeSettings settings, UnityEngine.Rect rect, System.Int32 cursorPosition)
  RVA=0x0B564EB4  token=0x60000B6  System.Void GetVertices(UnityEngine.UIElements.TextNativeSettings settings, System.IntPtr buffer, System.Int32 vertexSize, System.Int32& vertexCount)
  RVA=0x0B564BB8  token=0x60000B7  UnityEngine.Vector2 DoGetOffset(UnityEngine.UIElements.TextNativeSettings settings, UnityEngine.Rect rect)
  RVA=0x0B564A98  token=0x60000B8  System.Single DoComputeTextWidth_Injected(UnityEngine.UIElements.TextNativeSettings& settings)
  RVA=0x0B564A54  token=0x60000B9  System.Single DoComputeTextHeight_Injected(UnityEngine.UIElements.TextNativeSettings& settings)
  RVA=0x0B564ADC  token=0x60000BA  System.Void DoGetCursorPosition_Injected(UnityEngine.UIElements.TextNativeSettings& settings, UnityEngine.Rect& rect, System.Int32 cursorPosition, UnityEngine.Vector2& ret)
  RVA=0x0B564E50  token=0x60000BB  System.Void GetVertices_Injected(UnityEngine.UIElements.TextNativeSettings& settings, System.IntPtr buffer, System.Int32 vertexSize, System.Int32& vertexCount)
  RVA=0x0B564B64  token=0x60000BC  System.Void DoGetOffset_Injected(UnityEngine.UIElements.TextNativeSettings& settings, UnityEngine.Rect& rect, UnityEngine.Vector2& ret)
END_CLASS

CLASS: UnityEngine.UIElements.UIElementsRuntimeUtilityNative
TYPE:  static class
TOKEN: 0x200001B
SIZE:  0x10
FIELDS:
  private   static  System.Action                   RepaintOverlayPanelsCallback  // static @ 0x0
  private   static  System.Action                   UpdateRuntimePanelsCallback  // static @ 0x8
  private   static  System.Action                   RepaintOffscreenPanelsCallback  // static @ 0x10
METHODS:
  RVA=0x0B565094  token=0x60000BD  System.Void RepaintOverlayPanels()
  RVA=0x0B565100  token=0x60000BE  System.Void UpdateRuntimePanels()
  RVA=0x0B565050  token=0x60000BF  System.Void RepaintOffscreenPanels()
  RVA=0x0B565024  token=0x60000C0  System.Void RegisterPlayerloopCallback()
  RVA=0x0B5650D4  token=0x60000C1  System.Void UnregisterPlayerloopCallback()
  RVA=0x0B565144  token=0x60000C2  System.Void VisualElementCreation()
END_CLASS

CLASS: UnityEngine.UIElements.UIR.GfxUpdateBufferRange
TYPE:  sealed struct
TOKEN: 0x200001C
SIZE:  0x20
FIELDS:
  public            System.UInt32                   offsetFromWriteStart  // 0x10
  public            System.UInt32                   size  // 0x14
  public            System.UIntPtr                  source  // 0x18
METHODS:
END_CLASS

CLASS: UnityEngine.UIElements.UIR.DrawBufferRange
TYPE:  sealed struct
TOKEN: 0x200001D
SIZE:  0x20
FIELDS:
  public            System.Int32                    firstIndex  // 0x10
  public            System.Int32                    indexCount  // 0x14
  public            System.Int32                    minIndexVal  // 0x18
  public            System.Int32                    vertsReferenced  // 0x1c
METHODS:
END_CLASS

CLASS: UnityEngine.UIElements.UIR.Utility
TYPE:  class
TOKEN: 0x200001E
SIZE:  0x10
FIELDS:
  private   static  System.Action<System.Boolean>   GraphicsResourcesRecreate  // static @ 0x0
  private   static  System.Action                   EngineUpdate  // static @ 0x8
  private   static  System.Action                   FlushPendingResources  // static @ 0x10
  private   static  System.Action<UnityEngine.Camera>RegisterIntermediateRenderers  // static @ 0x18
  private   static  System.Action<System.IntPtr>    RenderNodeAdd  // static @ 0x20
  private   static  System.Action<System.IntPtr>    RenderNodeExecute  // static @ 0x28
  private   static  System.Action<System.IntPtr>    RenderNodeCleanup  // static @ 0x30
  private   static  Unity.Profiling.ProfilerMarker  s_MarkerRaiseEngineUpdate  // static @ 0x38
EVENTS:
  GraphicsResourcesRecreate  add=add_GraphicsResourcesRecreate  remove=remove_GraphicsResourcesRecreate
  EngineUpdate  add=add_EngineUpdate  remove=remove_EngineUpdate
  FlushPendingResources  add=add_FlushPendingResources  remove=remove_FlushPendingResources
  RegisterIntermediateRenderers  add=add_RegisterIntermediateRenderers  remove=remove_RegisterIntermediateRenderers
  RenderNodeExecute  add=add_RenderNodeExecute  remove=remove_RenderNodeExecute
METHODS:
  RVA=-1  // generic def  token=0x60000C3  System.Void SetVectorArray(UnityEngine.MaterialPropertyBlock props, System.Int32 name, Unity.Collections.NativeSlice<T> vector4s)
  RVA=0x0B565618  token=0x60000CE  System.Void RaiseGraphicsResourcesRecreate(System.Boolean recreate)
  RVA=0x0B565558  token=0x60000CF  System.Void RaiseEngineUpdate()
  RVA=0x0B5655C8  token=0x60000D0  System.Void RaiseFlushPendingResources()
  RVA=0x0B565670  token=0x60000D1  System.Void RaiseRegisterIntermediateRenderers(UnityEngine.Camera camera)
  RVA=0x0B5656CC  token=0x60000D2  System.Void RaiseRenderNodeAdd(System.IntPtr userData)
  RVA=0x0B565784  token=0x60000D3  System.Void RaiseRenderNodeExecute(System.IntPtr userData)
  RVA=0x0B565728  token=0x60000D4  System.Void RaiseRenderNodeCleanup(System.IntPtr userData)
  RVA=0x0B565170  token=0x60000D5  System.IntPtr AllocateBuffer(System.Int32 elementCount, System.Int32 elementStride, System.Boolean vertexBuffer)
  RVA=0x0B565304  token=0x60000D6  System.Void FreeBuffer(System.IntPtr buffer)
  RVA=0x0B565A9C  token=0x60000D7  System.Void UpdateBufferRanges(System.IntPtr buffer, System.IntPtr ranges, System.Int32 rangeCount, System.Int32 writeRangeStart, System.Int32 writeRangeEnd)
  RVA=0x0B565A10  token=0x60000D8  System.Void SetVectorArray(UnityEngine.MaterialPropertyBlock props, System.Int32 name, System.IntPtr vector4s, System.Int32 count)
  RVA=0x0B565440  token=0x60000D9  System.IntPtr GetVertexDeclaration(UnityEngine.Rendering.VertexAttributeDescriptor[] vertexAttributes)
  RVA=0x0B565848  token=0x60000DA  System.Void RegisterIntermediateRenderer(UnityEngine.Camera camera, UnityEngine.Material material, UnityEngine.Matrix4x4 transform, UnityEngine.Bounds aabb, System.Int32 renderLayer, System.Int32 shadowCasting, System.Boolean receiveShadows, System.Int32 sameDistanceSortPriority, System.UInt64 sceneCullingMask, System.Int32 rendererCallbackFlags, System.IntPtr userData, System.Int32 userDataSize)
  RVA=0x0B5652A0  token=0x60000DB  System.Void DrawRanges(System.IntPtr ib, System.IntPtr* vertexStreams, System.Int32 streamCount, System.IntPtr ranges, System.Int32 rangeCount, System.IntPtr vertexDecl)
  RVA=0x0B56591C  token=0x60000DC  System.Void SetPropertyBlock(UnityEngine.MaterialPropertyBlock props)
  RVA=0x0B565984  token=0x60000DD  System.Void SetScissorRect(UnityEngine.RectInt scissorRect)
  RVA=0x0B565274  token=0x60000DE  System.Void DisableScissor()
  RVA=0x0B565228  token=0x60000DF  System.IntPtr CreateStencilState(UnityEngine.Rendering.StencilState stencilState)
  RVA=0x0B5659D0  token=0x60000E0  System.Void SetStencilState(System.IntPtr stencilState, System.Int32 stencilRef)
  RVA=0x0B565474  token=0x60000E1  System.Boolean HasMappedBufferRange()
  RVA=0x0B5654A0  token=0x60000E2  System.UInt32 InsertCPUFence()
  RVA=0x0B5651C0  token=0x60000E3  System.Boolean CPUFencePassed(System.UInt32 fence)
  RVA=0x0B565B00  token=0x60000E4  System.Void WaitForCPUFencePassed(System.UInt32 fence)
  RVA=0x0B565A70  token=0x60000E5  System.Void SyncRenderThread()
  RVA=0x0B56536C  token=0x60000E6  UnityEngine.RectInt GetActiveViewport()
  RVA=0x0B565500  token=0x60000E7  System.Void ProfileDrawChainBegin()
  RVA=0x0B56552C  token=0x60000E8  System.Void ProfileDrawChainEnd()
  RVA=0x0B5654CC  token=0x60000E9  System.Void NotifyOfUIREvents(System.Boolean subscribe)
  RVA=0x0B5653EC  token=0x60000EA  UnityEngine.Matrix4x4 GetUnityProjectionMatrix()
  RVA=0x0B565B34  token=0x60000EB  System.Void .cctor()
  RVA=0x0B5657E0  token=0x60000EC  System.Void RegisterIntermediateRenderer_Injected(UnityEngine.Camera camera, UnityEngine.Material material, UnityEngine.Matrix4x4& transform, UnityEngine.Bounds& aabb, System.Int32 renderLayer, System.Int32 shadowCasting, System.Boolean receiveShadows, System.Int32 sameDistanceSortPriority, System.UInt64 sceneCullingMask, System.Int32 rendererCallbackFlags, System.IntPtr userData, System.Int32 userDataSize)
  RVA=0x0B565950  token=0x60000ED  System.Void SetScissorRect_Injected(UnityEngine.RectInt& scissorRect)
  RVA=0x0B5651F4  token=0x60000EE  System.IntPtr CreateStencilState_Injected(UnityEngine.Rendering.StencilState& stencilState)
  RVA=0x0B565338  token=0x60000EF  System.Void GetActiveViewport_Injected(UnityEngine.RectInt& ret)
  RVA=0x0B5653B8  token=0x60000F0  System.Void GetUnityProjectionMatrix_Injected(UnityEngine.Matrix4x4& ret)
END_CLASS

CLASS: UnityEngine.Yoga.BaselineFunction
TYPE:  sealed class
TOKEN: 0x2000002
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0B5634DC  token=0x6000001  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x05FD0A80  token=0x6000002  System.Single Invoke(UnityEngine.Yoga.YogaNode node, System.Single width, System.Single height)
END_CLASS

CLASS: UnityEngine.Yoga.Logger
TYPE:  sealed class
TOKEN: 0x2000003
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x097100B0  token=0x6000003  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x015547C0  token=0x6000004  System.Void Invoke(UnityEngine.Yoga.YogaConfig config, UnityEngine.Yoga.YogaNode node, UnityEngine.Yoga.YogaLogLevel level, System.String message)
END_CLASS

CLASS: UnityEngine.Yoga.MeasureFunction
TYPE:  sealed class
TOKEN: 0x2000004
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0B563604  token=0x6000005  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x07EDD23C  token=0x6000006  UnityEngine.Yoga.YogaSize Invoke(UnityEngine.Yoga.YogaNode node, System.Single width, UnityEngine.Yoga.YogaMeasureMode widthMode, System.Single height, UnityEngine.Yoga.YogaMeasureMode heightMode)
END_CLASS

CLASS: UnityEngine.Yoga.MeasureOutput
TYPE:  class
TOKEN: 0x2000005
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x04DBEED0  token=0x6000007  UnityEngine.Yoga.YogaSize Make(System.Single width, System.Single height)
END_CLASS

CLASS: UnityEngine.Yoga.YogaAlign
TYPE:  sealed struct
TOKEN: 0x2000006
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Yoga.YogaAlign      Auto  // const
  public    static  UnityEngine.Yoga.YogaAlign      FlexStart  // const
  public    static  UnityEngine.Yoga.YogaAlign      Center  // const
  public    static  UnityEngine.Yoga.YogaAlign      FlexEnd  // const
  public    static  UnityEngine.Yoga.YogaAlign      Stretch  // const
  public    static  UnityEngine.Yoga.YogaAlign      Baseline  // const
  public    static  UnityEngine.Yoga.YogaAlign      SpaceBetween  // const
  public    static  UnityEngine.Yoga.YogaAlign      SpaceAround  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Yoga.YogaConfig
TYPE:  class
TOKEN: 0x2000007
SIZE:  0x20
FIELDS:
  private   static readonly UnityEngine.Yoga.YogaConfig     Default  // static @ 0x0
  private           System.IntPtr                   _ygConfig  // 0x10
  private           UnityEngine.Yoga.Logger         _logger  // 0x18
PROPERTIES:
  Handle  get=0x04D9B600
  UseWebDefaults  get=0x0B566570  set=0x0B56658C
  PointScaleFactor  set=0x0B566580
METHODS:
  RVA=0x0B566504  token=0x6000008  System.Void .ctor(System.IntPtr ygConfig)
  RVA=0x0B5664E0  token=0x6000009  System.Void .ctor()
  RVA=0x0B56638C  token=0x600000A  System.Void Finalize()
  RVA=0x0B566458  token=0x600000F  System.Void .cctor()
END_CLASS

CLASS: UnityEngine.Yoga.YogaConstants
TYPE:  static class
TOKEN: 0x2000008
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B566598  token=0x6000010  System.Boolean IsUndefined(System.Single value)
END_CLASS

CLASS: UnityEngine.Yoga.YogaDirection
TYPE:  sealed struct
TOKEN: 0x2000009
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Yoga.YogaDirection  Inherit  // const
  public    static  UnityEngine.Yoga.YogaDirection  LTR  // const
  public    static  UnityEngine.Yoga.YogaDirection  RTL  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Yoga.YogaDisplay
TYPE:  sealed struct
TOKEN: 0x200000A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Yoga.YogaDisplay    Flex  // const
  public    static  UnityEngine.Yoga.YogaDisplay    None  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Yoga.YogaEdge
TYPE:  sealed struct
TOKEN: 0x200000B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Yoga.YogaEdge       Left  // const
  public    static  UnityEngine.Yoga.YogaEdge       Top  // const
  public    static  UnityEngine.Yoga.YogaEdge       Right  // const
  public    static  UnityEngine.Yoga.YogaEdge       Bottom  // const
  public    static  UnityEngine.Yoga.YogaEdge       Start  // const
  public    static  UnityEngine.Yoga.YogaEdge       End  // const
  public    static  UnityEngine.Yoga.YogaEdge       Horizontal  // const
  public    static  UnityEngine.Yoga.YogaEdge       Vertical  // const
  public    static  UnityEngine.Yoga.YogaEdge       All  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Yoga.YogaFlexDirection
TYPE:  sealed struct
TOKEN: 0x200000C
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Yoga.YogaFlexDirectionColumn  // const
  public    static  UnityEngine.Yoga.YogaFlexDirectionColumnReverse  // const
  public    static  UnityEngine.Yoga.YogaFlexDirectionRow  // const
  public    static  UnityEngine.Yoga.YogaFlexDirectionRowReverse  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Yoga.YogaJustify
TYPE:  sealed struct
TOKEN: 0x200000D
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Yoga.YogaJustify    FlexStart  // const
  public    static  UnityEngine.Yoga.YogaJustify    Center  // const
  public    static  UnityEngine.Yoga.YogaJustify    FlexEnd  // const
  public    static  UnityEngine.Yoga.YogaJustify    SpaceBetween  // const
  public    static  UnityEngine.Yoga.YogaJustify    SpaceAround  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Yoga.YogaLogLevel
TYPE:  sealed struct
TOKEN: 0x200000E
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Yoga.YogaLogLevel   Error  // const
  public    static  UnityEngine.Yoga.YogaLogLevel   Warn  // const
  public    static  UnityEngine.Yoga.YogaLogLevel   Info  // const
  public    static  UnityEngine.Yoga.YogaLogLevel   Debug  // const
  public    static  UnityEngine.Yoga.YogaLogLevel   Verbose  // const
  public    static  UnityEngine.Yoga.YogaLogLevel   Fatal  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Yoga.YogaMeasureMode
TYPE:  sealed struct
TOKEN: 0x200000F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Yoga.YogaMeasureModeUndefined  // const
  public    static  UnityEngine.Yoga.YogaMeasureModeExactly  // const
  public    static  UnityEngine.Yoga.YogaMeasureModeAtMost  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Yoga.Native
TYPE:  static class
TOKEN: 0x2000010
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x0B563CA4  token=0x6000011  System.IntPtr YGNodeNewWithConfig(System.IntPtr config)
  RVA=0x0B56397C  token=0x6000012  System.Void YGNodeFree(System.IntPtr ygNode)
  RVA=0x0B563948  token=0x6000013  System.Void YGNodeFreeInternal(System.IntPtr ygNode)
  RVA=0x0B564810  token=0x6000014  System.Void YGSetManagedObject(System.IntPtr ygNode, UnityEngine.Yoga.YogaNode node)
  RVA=0x0B563D50  token=0x6000015  System.Void YGNodeSetConfig(System.IntPtr ygNode, System.IntPtr config)
  RVA=0x0B563770  token=0x6000016  System.IntPtr YGConfigGetDefault()
  RVA=0x0B5637D0  token=0x6000017  System.IntPtr YGConfigNew()
  RVA=0x0B563760  token=0x6000018  System.Void YGConfigFree(System.IntPtr config)
  RVA=0x0B56372C  token=0x6000019  System.Void YGConfigFreeInternal(System.IntPtr config)
  RVA=0x0B563840  token=0x600001A  System.Void YGConfigSetUseWebDefaults(System.IntPtr config, System.Boolean useWebDefaults)
  RVA=0x0B56379C  token=0x600001B  System.Boolean YGConfigGetUseWebDefaults(System.IntPtr config)
  RVA=0x0B5637FC  token=0x600001C  System.Void YGConfigSetPointScaleFactor(System.IntPtr config, System.Single pixelsInPoint)
  RVA=0x0B5639C0  token=0x600001D  System.Void YGNodeInsertChild(System.IntPtr node, System.IntPtr child, System.UInt32 index)
  RVA=0x0B563CD8  token=0x600001E  System.Void YGNodeRemoveChild(System.IntPtr node, System.IntPtr child)
  RVA=0x0B5638A0  token=0x600001F  System.Void YGNodeCalculateLayout(System.IntPtr node, System.Single availableWidth, System.Single availableHeight, UnityEngine.Yoga.YogaDirection parentDirection)
  RVA=0x0B563C40  token=0x6000020  System.Void YGNodeMarkDirty(System.IntPtr node)
  RVA=0x0B563A14  token=0x6000021  System.Boolean YGNodeIsDirty(System.IntPtr node)
  RVA=0x0B563904  token=0x6000022  System.Void YGNodeCopyStyle(System.IntPtr dstNode, System.IntPtr srcNode)
  RVA=0x0B563DD8  token=0x6000023  System.Void YGNodeSetMeasureFunc(System.IntPtr node)
  RVA=0x0B563D1C  token=0x6000024  System.Void YGNodeRemoveMeasureFunc(System.IntPtr node)
  RVA=0x0B563C74  token=0x6000025  System.Void YGNodeMeasureInvoke(UnityEngine.Yoga.YogaNode node, System.Single width, UnityEngine.Yoga.YogaMeasureMode widthMode, System.Single height, UnityEngine.Yoga.YogaMeasureMode heightMode, System.IntPtr returnValueAddress)
  RVA=0x0B563884  token=0x6000026  System.Void YGNodeBaselineInvoke(UnityEngine.Yoga.YogaNode node, System.Single width, System.Single height, System.IntPtr returnValueAddress)
  RVA=0x0B563D94  token=0x6000027  System.Void YGNodeSetHasNewLayout(System.IntPtr node, System.Boolean hasNewLayout)
  RVA=0x0B56398C  token=0x6000028  System.Boolean YGNodeGetHasNewLayout(System.IntPtr node)
  RVA=0x0B563E0C  token=0x6000029  UnityEngine.Yoga.YogaDirection YGNodeStyleGetDirection(System.IntPtr node)
  RVA=0x0B56404C  token=0x600002A  System.Void YGNodeStyleSetFlexDirection(System.IntPtr node, UnityEngine.Yoga.YogaFlexDirection flexDirection)
  RVA=0x0B564254  token=0x600002B  System.Void YGNodeStyleSetJustifyContent(System.IntPtr node, UnityEngine.Yoga.YogaJustify justifyContent)
  RVA=0x0B563E40  token=0x600002C  System.Void YGNodeStyleSetAlignContent(System.IntPtr node, UnityEngine.Yoga.YogaAlign alignContent)
  RVA=0x0B563E80  token=0x600002D  System.Void YGNodeStyleSetAlignItems(System.IntPtr node, UnityEngine.Yoga.YogaAlign alignItems)
  RVA=0x0B563EC0  token=0x600002E  System.Void YGNodeStyleSetAlignSelf(System.IntPtr node, UnityEngine.Yoga.YogaAlign alignSelf)
  RVA=0x0B5646C4  token=0x600002F  System.Void YGNodeStyleSetPositionType(System.IntPtr node, UnityEngine.Yoga.YogaPositionType positionType)
  RVA=0x0B564114  token=0x6000030  System.Void YGNodeStyleSetFlexWrap(System.IntPtr node, UnityEngine.Yoga.YogaWrap flexWrap)
  RVA=0x0B564594  token=0x6000031  System.Void YGNodeStyleSetOverflow(System.IntPtr node, UnityEngine.Yoga.YogaOverflow flexWrap)
  RVA=0x0B563F50  token=0x6000032  System.Void YGNodeStyleSetDisplay(System.IntPtr node, UnityEngine.Yoga.YogaDisplay display)
  RVA=0x0B564154  token=0x6000033  System.Void YGNodeStyleSetFlex(System.IntPtr node, System.Single flex)
  RVA=0x0B56408C  token=0x6000034  System.Void YGNodeStyleSetFlexGrow(System.IntPtr node, System.Single flexGrow)
  RVA=0x0B5640D0  token=0x6000035  System.Void YGNodeStyleSetFlexShrink(System.IntPtr node, System.Single flexShrink)
  RVA=0x0B564008  token=0x6000036  System.Void YGNodeStyleSetFlexBasis(System.IntPtr node, System.Single flexBasis)
  RVA=0x0B563FC4  token=0x6000037  System.Void YGNodeStyleSetFlexBasisPercent(System.IntPtr node, System.Single flexBasis)
  RVA=0x0B563F90  token=0x6000038  System.Void YGNodeStyleSetFlexBasisAuto(System.IntPtr node)
  RVA=0x0B5647CC  token=0x6000039  System.Void YGNodeStyleSetWidth(System.IntPtr node, System.Single width)
  RVA=0x0B564788  token=0x600003A  System.Void YGNodeStyleSetWidthPercent(System.IntPtr node, System.Single width)
  RVA=0x0B564754  token=0x600003B  System.Void YGNodeStyleSetWidthAuto(System.IntPtr node)
  RVA=0x0B564210  token=0x600003C  System.Void YGNodeStyleSetHeight(System.IntPtr node, System.Single height)
  RVA=0x0B5641CC  token=0x600003D  System.Void YGNodeStyleSetHeightPercent(System.IntPtr node, System.Single height)
  RVA=0x0B564198  token=0x600003E  System.Void YGNodeStyleSetHeightAuto(System.IntPtr node)
  RVA=0x0B564550  token=0x600003F  System.Void YGNodeStyleSetMinWidth(System.IntPtr node, System.Single minWidth)
  RVA=0x0B56450C  token=0x6000040  System.Void YGNodeStyleSetMinWidthPercent(System.IntPtr node, System.Single minWidth)
  RVA=0x0B5644C8  token=0x6000041  System.Void YGNodeStyleSetMinHeight(System.IntPtr node, System.Single minHeight)
  RVA=0x0B564484  token=0x6000042  System.Void YGNodeStyleSetMinHeightPercent(System.IntPtr node, System.Single minHeight)
  RVA=0x0B564440  token=0x6000043  System.Void YGNodeStyleSetMaxWidth(System.IntPtr node, System.Single maxWidth)
  RVA=0x0B5643FC  token=0x6000044  System.Void YGNodeStyleSetMaxWidthPercent(System.IntPtr node, System.Single maxWidth)
  RVA=0x0B5643B8  token=0x6000045  System.Void YGNodeStyleSetMaxHeight(System.IntPtr node, System.Single maxHeight)
  RVA=0x0B564374  token=0x6000046  System.Void YGNodeStyleSetMaxHeightPercent(System.IntPtr node, System.Single maxHeight)
  RVA=0x0B564704  token=0x6000047  System.Void YGNodeStyleSetPosition(System.IntPtr node, UnityEngine.Yoga.YogaEdge edge, System.Single position)
  RVA=0x0B564674  token=0x6000048  System.Void YGNodeStyleSetPositionPercent(System.IntPtr node, UnityEngine.Yoga.YogaEdge edge, System.Single position)
  RVA=0x0B564324  token=0x6000049  System.Void YGNodeStyleSetMargin(System.IntPtr node, UnityEngine.Yoga.YogaEdge edge, System.Single margin)
  RVA=0x0B5642D4  token=0x600004A  System.Void YGNodeStyleSetMarginPercent(System.IntPtr node, UnityEngine.Yoga.YogaEdge edge, System.Single margin)
  RVA=0x0B564294  token=0x600004B  System.Void YGNodeStyleSetMarginAuto(System.IntPtr node, UnityEngine.Yoga.YogaEdge edge)
  RVA=0x0B564624  token=0x600004C  System.Void YGNodeStyleSetPadding(System.IntPtr node, UnityEngine.Yoga.YogaEdge edge, System.Single padding)
  RVA=0x0B5645D4  token=0x600004D  System.Void YGNodeStyleSetPaddingPercent(System.IntPtr node, UnityEngine.Yoga.YogaEdge edge, System.Single padding)
  RVA=0x0B563F00  token=0x600004E  System.Void YGNodeStyleSetBorder(System.IntPtr node, UnityEngine.Yoga.YogaEdge edge, System.Single border)
  RVA=0x0B563AF0  token=0x600004F  System.Single YGNodeLayoutGetLeft(System.IntPtr node)
  RVA=0x0B563BD8  token=0x6000050  System.Single YGNodeLayoutGetTop(System.IntPtr node)
  RVA=0x0B563BA4  token=0x6000051  System.Single YGNodeLayoutGetRight(System.IntPtr node)
  RVA=0x0B563A88  token=0x6000052  System.Single YGNodeLayoutGetBottom(System.IntPtr node)
  RVA=0x0B563C0C  token=0x6000053  System.Single YGNodeLayoutGetWidth(System.IntPtr node)
  RVA=0x0B563ABC  token=0x6000054  System.Single YGNodeLayoutGetHeight(System.IntPtr node)
  RVA=0x0B563B24  token=0x6000055  System.Single YGNodeLayoutGetMargin(System.IntPtr node, UnityEngine.Yoga.YogaEdge edge)
  RVA=0x0B563B64  token=0x6000056  System.Single YGNodeLayoutGetPadding(System.IntPtr node, UnityEngine.Yoga.YogaEdge edge)
  RVA=0x0B563A48  token=0x6000057  System.Single YGNodeLayoutGetBorder(System.IntPtr node, UnityEngine.Yoga.YogaEdge edge)
END_CLASS

CLASS: UnityEngine.Yoga.YogaNode
TYPE:  class
TOKEN: 0x2000011
SIZE:  0x48
IMPLEMENTS: System.Collections.Generic.IEnumerable`1 System.Collections.IEnumerable
FIELDS:
  private           System.IntPtr                   _ygNode  // 0x10
  private           UnityEngine.Yoga.YogaConfig     _config  // 0x18
  private           System.WeakReference            _parent  // 0x20
  private           System.Collections.Generic.List<UnityEngine.Yoga.YogaNode>_children  // 0x28
  private           UnityEngine.Yoga.MeasureFunction_measureFunction  // 0x30
  private           UnityEngine.Yoga.BaselineFunction_baselineFunction  // 0x38
  private           System.Object                   _data  // 0x40
PROPERTIES:
  Config  set=0x0B566EEC
  IsDirty  get=0x0B566D14
  HasNewLayout  get=0x0B566D04
  IsMeasureDefined  get=0x09B11948
  IsBaselineDefined  get=0x09B0008C
  FlexDirection  set=0x0B566FD8
  JustifyContent  set=0x0B56705C
  Display  set=0x0B566F78
  AlignItems  set=0x0B566E74
  AlignSelf  set=0x0B566E80
  AlignContent  set=0x0B566E68
  PositionType  set=0x0B5671D4
  Wrap  set=0x0B567254
  Flex  set=0x0B566FFC
  FlexGrow  set=0x0B566FE4
  FlexShrink  set=0x0B566FF0
  FlexBasis  set=0x0B566F84
  Width  set=0x0B567200
  Height  set=0x0B567008
  MaxWidth  set=0x0B5670EC
  MaxHeight  set=0x0B5670B8
  MinWidth  set=0x0B567154
  MinHeight  set=0x0B567120
  LayoutX  get=0x0B566E48
  LayoutY  get=0x0B566E58
  LayoutRight  get=0x0B566E28
  LayoutBottom  get=0x0B566D70
  LayoutWidth  get=0x0B566E38
  LayoutHeight  get=0x0B566D80
  Overflow  set=0x0B567188
  Count  get=0x0B566CC0
  Left  set=0x0B567068
  Top  set=0x0B5671F0
  Right  set=0x0B5671E0
  Bottom  set=0x0B566EDC
  MarginLeft  set=0x0B567088
  MarginTop  set=0x0B5670A8
  MarginRight  set=0x0B567098
  MarginBottom  set=0x0B567078
  PaddingLeft  set=0x0B5671A4
  PaddingTop  set=0x0B5671C4
  PaddingRight  set=0x0B5671B4
  PaddingBottom  set=0x0B567194
  BorderLeftWidth  set=0x0B566EA0
  BorderTopWidth  set=0x0B566EC8
  BorderRightWidth  set=0x0B566EB4
  BorderBottomWidth  set=0x0B566E8C
  LayoutMarginLeft  get=0x0B566DA4
  LayoutMarginTop  get=0x0B566DC8
  LayoutMarginRight  get=0x0B566DB4
  LayoutMarginBottom  get=0x0B566D90
  LayoutPaddingLeft  get=0x0B566DF0
  LayoutPaddingTop  get=0x0B566E14
  LayoutPaddingRight  get=0x0B566E00
  LayoutPaddingBottom  get=0x0B566DDC
  LayoutBorderLeft  get=0x0B566D38
  LayoutBorderTop  get=0x0B566D5C
  LayoutBorderRight  get=0x0B566D48
  LayoutBorderBottom  get=0x0B566D24
METHODS:
  RVA=0x0B566BE4  token=0x6000058  System.Void .ctor(UnityEngine.Yoga.YogaConfig config)
  RVA=0x0B566700  token=0x6000059  System.Void Finalize()
  RVA=0x0B5668FC  token=0x600005C  System.Void MarkDirty()
  RVA=0x0B5666DC  token=0x6000060  System.Void CopyStyle(UnityEngine.Yoga.YogaNode srcNode)
  RVA=0x0B566908  token=0x600007B  System.Void MarkLayoutSeen()
  RVA=0x0B5667E0  token=0x600007C  System.Void Insert(System.Int32 index, UnityEngine.Yoga.YogaNode node)
  RVA=0x0B5669B0  token=0x600007D  System.Void RemoveAt(System.Int32 index)
  RVA=0x0B566680  token=0x600007E  System.Void Clear()
  RVA=0x0B566A58  token=0x600007F  System.Void SetMeasureFunction(UnityEngine.Yoga.MeasureFunction measureFunction)
  RVA=0x0B566630  token=0x6000080  System.Void CalculateLayout(System.Single width, System.Single height)
  RVA=0x0B566918  token=0x6000081  UnityEngine.Yoga.YogaSize MeasureInternal(UnityEngine.Yoga.YogaNode node, System.Single width, UnityEngine.Yoga.YogaMeasureMode widthMode, System.Single height, UnityEngine.Yoga.YogaMeasureMode heightMode)
  RVA=0x0B5665A0  token=0x6000082  System.Single BaselineInternal(UnityEngine.Yoga.YogaNode node, System.Single width, System.Single height)
  RVA=0x0B566774  token=0x6000083  System.Collections.Generic.IEnumerator<UnityEngine.Yoga.YogaNode> GetEnumerator()
  RVA=0x0B566B78  token=0x6000084  System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
  RVA=0x0B566B44  token=0x6000089  System.Void SetStylePosition(UnityEngine.Yoga.YogaEdge edge, UnityEngine.Yoga.YogaValue value)
  RVA=0x0B566ABC  token=0x600008E  System.Void SetStyleMargin(UnityEngine.Yoga.YogaEdge edge, UnityEngine.Yoga.YogaValue value)
  RVA=0x0B566B10  token=0x6000093  System.Void SetStylePadding(UnityEngine.Yoga.YogaEdge edge, UnityEngine.Yoga.YogaValue value)
END_CLASS

CLASS: UnityEngine.Yoga.YogaOverflow
TYPE:  sealed struct
TOKEN: 0x2000012
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Yoga.YogaOverflow   Visible  // const
  public    static  UnityEngine.Yoga.YogaOverflow   Hidden  // const
  public    static  UnityEngine.Yoga.YogaOverflow   Scroll  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Yoga.YogaPositionType
TYPE:  sealed struct
TOKEN: 0x2000013
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Yoga.YogaPositionTypeRelative  // const
  public    static  UnityEngine.Yoga.YogaPositionTypeAbsolute  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Yoga.YogaSize
TYPE:  sealed struct
TOKEN: 0x2000014
SIZE:  0x18
FIELDS:
  public            System.Single                   width  // 0x10
  public            System.Single                   height  // 0x14
METHODS:
END_CLASS

CLASS: UnityEngine.Yoga.YogaUnit
TYPE:  sealed struct
TOKEN: 0x2000015
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Yoga.YogaUnit       Undefined  // const
  public    static  UnityEngine.Yoga.YogaUnit       Point  // const
  public    static  UnityEngine.Yoga.YogaUnit       Percent  // const
  public    static  UnityEngine.Yoga.YogaUnit       Auto  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Yoga.YogaValue
TYPE:  sealed struct
TOKEN: 0x2000016
SIZE:  0x18
FIELDS:
  private           System.Single                   value  // 0x10
  private           UnityEngine.Yoga.YogaUnit       unit  // 0x14
PROPERTIES:
  Unit  get=0x04D9E980
  Value  get=0x04DBA9D0
METHODS:
  RVA=0x0B567378  token=0x60000A6  UnityEngine.Yoga.YogaValue Point(System.Single value)
  RVA=0x0B567260  token=0x60000A7  System.Boolean Equals(UnityEngine.Yoga.YogaValue other)
  RVA=0x0B5672B4  token=0x60000A8  System.Boolean Equals(System.Object obj)
  RVA=0x0B567324  token=0x60000A9  System.Int32 GetHashCode()
  RVA=0x04DBEEE0  token=0x60000AA  UnityEngine.Yoga.YogaValue Auto()
  RVA=0x0B567354  token=0x60000AB  UnityEngine.Yoga.YogaValue Percent(System.Single value)
  RVA=0x0B567378  token=0x60000AC  UnityEngine.Yoga.YogaValue op_Implicit(System.Single pointValue)
END_CLASS

CLASS: UnityEngine.Yoga.YogaWrap
TYPE:  sealed struct
TOKEN: 0x2000017
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.Yoga.YogaWrap       NoWrap  // const
  public    static  UnityEngine.Yoga.YogaWrap       Wrap  // const
  public    static  UnityEngine.Yoga.YogaWrap       WrapReverse  // const
METHODS:
END_CLASS

