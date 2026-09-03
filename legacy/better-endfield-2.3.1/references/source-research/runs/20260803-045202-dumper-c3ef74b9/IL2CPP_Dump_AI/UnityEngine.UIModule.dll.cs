// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.UIModule.dll
// Classes:  23
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

CLASS: HGFillMethod
TYPE:  sealed struct
TOKEN: 0x2000005
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.CanvasRenderer.HGFillMethodHorizontal  // const
  public    static  UnityEngine.CanvasRenderer.HGFillMethodVertical  // const
  public    static  UnityEngine.CanvasRenderer.HGFillMethodRadial90  // const
  public    static  UnityEngine.CanvasRenderer.HGFillMethodRadial180  // const
  public    static  UnityEngine.CanvasRenderer.HGFillMethodRadial360  // const
METHODS:
END_CLASS

CLASS: HGImageType
TYPE:  sealed struct
TOKEN: 0x2000006
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.CanvasRenderer.HGImageTypeSimple  // const
  public    static  UnityEngine.CanvasRenderer.HGImageTypeSliced  // const
  public    static  UnityEngine.CanvasRenderer.HGImageTypeTiled  // const
  public    static  UnityEngine.CanvasRenderer.HGImageTypeFilled  // const
  public    static  UnityEngine.CanvasRenderer.HGImageTypeMirror  // const
  public    static  UnityEngine.CanvasRenderer.HGImageTypeMirrorQuarter  // const
METHODS:
END_CLASS

CLASS: HGImageData
TYPE:  sealed struct
TOKEN: 0x2000007
SIZE:  0x30
FIELDS:
  public            UnityEngine.CanvasRenderer.HGImageTypetype  // 0x10
  public            UnityEngine.CanvasRenderer.HGFillMethodfillMethod  // 0x14
  public            System.Single                   fillAmount  // 0x18
  public            System.Int32                    fillOrigin  // 0x1c
  public            System.Single                   pixelsPerUnit  // 0x20
  public            System.Single                   pixelsPerUnitMultiplier  // 0x24
  public            UnityEngine.Color32             color  // 0x28
  public            System.Boolean                  preserveAspect  // 0x2c
  public            System.Boolean                  fillCenter  // 0x2d
  public            System.Boolean                  fillClockwise  // 0x2e
METHODS:
END_CLASS

CLASS: InstanceRange
TYPE:  sealed struct
TOKEN: 0x2000008
SIZE:  0x14
FIELDS:
  public            System.UInt16                   start  // 0x10
  public            System.UInt16                   end  // 0x12
METHODS:
END_CLASS

CLASS: WillRenderCanvases
TYPE:  sealed class
TOKEN: 0x200000D
SIZE:  0x80
EXTENDS: System.MulticastDelegate
FIELDS:
METHODS:
  RVA=0x0309A530  token=0x6000097  System.Void .ctor(System.Object object, System.IntPtr method)
  RVA=0x053DEDB8  token=0x6000098  System.Void Invoke()
END_CLASS

CLASS: SampleType
TYPE:  sealed struct
TOKEN: 0x200000F
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.UISystemProfilerApi.SampleTypeLayout  // const
  public    static  UnityEngine.UISystemProfilerApi.SampleTypeRender  // const
METHODS:
END_CLASS

CLASS: HGRectMaskInput
TYPE:  sealed struct
TOKEN: 0x2000011
SIZE:  0x40
FIELDS:
  public            System.Int32                    maskRTInstanceID  // 0x10
  public            System.Int32                    rootCanvasInstanceID  // 0x14
  public            System.Int32                    renderMode  // 0x18
  public            System.Int32                    clipperOffset  // 0x1c
  public            System.Int32                    clipperCount  // 0x20
  public            System.Int32                    targetOffset  // 0x24
  public            System.Int32                    targetCount  // 0x28
  public            UnityEngine.Rect                lastClipRect  // 0x2c
  public            System.Int32                    forceClip  // 0x3c
METHODS:
END_CLASS

CLASS: HGClipperInput
TYPE:  sealed struct
TOKEN: 0x2000012
SIZE:  0x28
FIELDS:
  public            System.Int32                    rtInstanceID  // 0x10
  public            System.Int32                    canvasInstanceID  // 0x14
  public            UnityEngine.Vector4             padding  // 0x18
METHODS:
END_CLASS

CLASS: HGClipTargetInput
TYPE:  sealed struct
TOKEN: 0x2000013
SIZE:  0x18
FIELDS:
  public            System.Int32                    rtInstanceID  // 0x10
  public            System.Int32                    canvasRendererInstanceID  // 0x14
METHODS:
END_CLASS

CLASS: HGRectMaskOutput
TYPE:  sealed struct
TOKEN: 0x2000014
SIZE:  0x28
FIELDS:
  public            UnityEngine.Rect                clipRect  // 0x10
  public            System.Int32                    validRect  // 0x20
  public            System.Int32                    maskIsCulled  // 0x24
METHODS:
END_CLASS

CLASS: HGClipTargetOutput
TYPE:  sealed struct
TOKEN: 0x2000015
SIZE:  0x18
FIELDS:
  public            System.Int32                    shouldCull  // 0x10
  public            System.Int32                    cullStateChanged  // 0x14
METHODS:
END_CLASS

CLASS: HGBatchImageInput
TYPE:  sealed struct
TOKEN: 0x2000017
SIZE:  0x48
FIELDS:
  public            System.Int32                    canvasRendererInstanceID  // 0x10
  public            System.Int32                    spriteInstanceID  // 0x14
  public            UnityEngine.Rect                adjustedRect  // 0x18
  public            UnityEngine.CanvasRenderer.HGImageDataimageData  // 0x28
METHODS:
END_CLASS

CLASS: UnityEngine.ICanvasRaycastFilter
TYPE:  interface
TOKEN: 0x2000002
FIELDS:
METHODS:
  RVA=-1  // abstract  token=0x6000001  System.Boolean IsRaycastLocationValid(UnityEngine.Vector2 sp, UnityEngine.Camera eventCamera)
END_CLASS

CLASS: UnityEngine.CanvasGroup
TYPE:  sealed class
TOKEN: 0x2000003
SIZE:  0x18
EXTENDS: UnityEngine.Behaviour
IMPLEMENTS: UnityEngine.ICanvasRaycastFilter
FIELDS:
PROPERTIES:
  color  get=0x02FA9CA0  set=0x05390B3C
  alpha  get=0x03966E10  set=0x03046300
  interactable  get=0x02FC7E30  set=0x039D4A20
  blocksRaycasts  get=0x030912E0  set=0x039D4B90
  ignoreParentGroups  get=0x02FC7DF0  set=0x0B567398
METHODS:
  RVA=0x05390FCC  token=0x600000C  System.Boolean IsRaycastLocationValid(UnityEngine.Vector2 sp, UnityEngine.Camera eventCamera)
  RVA=0x05393520  token=0x600000D  System.Void .ctor()
  RVA=0x02FA9D00  token=0x600000E  System.Void get_color_Injected(UnityEngine.Color& ret)
  RVA=0x03B143B0  token=0x600000F  System.Void set_color_Injected(UnityEngine.Color& value)
END_CLASS

CLASS: UnityEngine.CanvasRenderer
TYPE:  sealed class
TOKEN: 0x2000004
SIZE:  0x20
EXTENDS: UnityEngine.Component
FIELDS:
  private           System.Boolean                  <isMask>k__BackingField  // 0x18
PROPERTIES:
  hasPopInstruction  set=0x041D2390
  materialCount  get=0x02EDDCF0  set=0x031BD070
  popMaterialCount  set=0x041D22D0
  absoluteDepth  get=0x031BF7C0
  hasMoved  get=0x0B567684
  cullTransparentMesh  get=0x0418F2D0  set=0x0B5676B8
  cull  get=0x031BEDB0  set=0x03519F70
  clippingSoftness  set=0x05391498
  clippingHGSoftness  set=0x05391484
METHODS:
  RVA=0x043ED860  token=0x600001A  System.Void SetColor(UnityEngine.Color color)
  RVA=0x0343F240  token=0x600001B  UnityEngine.Color GetColor()
  RVA=0x053916A8  token=0x600001C  System.Void EnableRectClipping(UnityEngine.Rect rect)
  RVA=0x031EB190  token=0x600001F  System.Void DisableRectClipping()
  RVA=0x031BD010  token=0x6000020  System.Void SetMaterial(UnityEngine.Material material, System.Int32 index)
  RVA=0x0B5674C0  token=0x6000021  UnityEngine.Material GetMaterial(System.Int32 index)
  RVA=0x041D2320  token=0x6000022  System.Void SetPopMaterial(UnityEngine.Material material, System.Int32 index)
  RVA=0x031BD0C0  token=0x6000023  System.Void SetTexture(UnityEngine.Texture texture)
  RVA=0x03F20C10  token=0x6000024  System.Void SetAlphaTexture(UnityEngine.Texture texture)
  RVA=0x031F2610  token=0x6000025  System.Void SetMesh(UnityEngine.Mesh mesh)
  RVA=0x0351A5F0  token=0x6000026  System.Void Clear()
  RVA=0x0B567558  token=0x6000027  System.Void SetImageMeshRebuildData(UnityEngine.CanvasRenderer.HGImageData imageData, UnityEngine.Sprite sprite, UnityEngine.Rect adjustedRect)
  RVA=0x0B567500  token=0x6000028  System.Void SetAlpha(System.Single alpha)
  RVA=0x033A0AE0  token=0x6000029  System.Single GetInheritedAlpha()
  RVA=0x02EDDC40  token=0x600002A  System.Void SetMaterial(UnityEngine.Material material, UnityEngine.Texture texture)
  RVA=0x0B5674B4  token=0x600002B  UnityEngine.Material GetMaterial()
  RVA=0x0B567618  token=0x600002C  System.Void SplitUIVertexStreams(System.Collections.Generic.List<UnityEngine.UIVertex> verts, System.Collections.Generic.List<UnityEngine.Vector3> positions, System.Collections.Generic.List<UnityEngine.Color32> colors, System.Collections.Generic.List<UnityEngine.Vector4> uv0S, System.Collections.Generic.List<UnityEngine.Vector4> uv1S, System.Collections.Generic.List<UnityEngine.Vector4> uv2S, System.Collections.Generic.List<UnityEngine.Vector4> uv3S, System.Collections.Generic.List<UnityEngine.Vector3> normals, System.Collections.Generic.List<UnityEngine.Vector4> tangents, System.Collections.Generic.List<System.Int32> indices)
  RVA=0x0B567450  token=0x600002D  System.Void CreateUIVertexStream(System.Collections.Generic.List<UnityEngine.UIVertex> verts, System.Collections.Generic.List<UnityEngine.Vector3> positions, System.Collections.Generic.List<UnityEngine.Color32> colors, System.Collections.Generic.List<UnityEngine.Vector4> uv0S, System.Collections.Generic.List<UnityEngine.Vector4> uv1S, System.Collections.Generic.List<UnityEngine.Vector4> uv2S, System.Collections.Generic.List<UnityEngine.Vector4> uv3S, System.Collections.Generic.List<UnityEngine.Vector3> normals, System.Collections.Generic.List<UnityEngine.Vector4> tangents, System.Collections.Generic.List<System.Int32> indices)
  RVA=0x0B5673DC  token=0x600002E  System.Void AddUIVertexStream(System.Collections.Generic.List<UnityEngine.UIVertex> verts, System.Collections.Generic.List<UnityEngine.Vector3> positions, System.Collections.Generic.List<UnityEngine.Color32> colors, System.Collections.Generic.List<UnityEngine.Vector4> uv0S, System.Collections.Generic.List<UnityEngine.Vector4> uv1S, System.Collections.Generic.List<UnityEngine.Vector4> uv2S, System.Collections.Generic.List<UnityEngine.Vector4> uv3S, System.Collections.Generic.List<UnityEngine.Vector3> normals, System.Collections.Generic.List<UnityEngine.Vector4> tangents)
  RVA=0x0B567570  token=0x600002F  System.Void SplitIndicesStreamsInternal(System.Object verts, System.Object indices)
  RVA=0x0B5675B4  token=0x6000030  System.Void SplitUIVertexStreamsInternal(System.Object verts, System.Object positions, System.Object colors, System.Object uv0S, System.Object uv1S, System.Object uv2S, System.Object uv3S, System.Object normals, System.Object tangents)
  RVA=0x0B5673EC  token=0x6000031  System.Void CreateUIVertexStreamInternal(System.Object verts, System.Object positions, System.Object colors, System.Object uv0S, System.Object uv1S, System.Object uv2S, System.Object uv3S, System.Object normals, System.Object tangents, System.Object indices)
  RVA=0x03AA7CD0  token=0x6000032  System.Void UpdateInstanceRange(System.UInt32 rangeCount, System.Void* ranges)
  RVA=0x03AA7C70  token=0x6000033  System.Void UpdateInstanceRange(System.UInt32 rangeCount, Unity.Collections.NativeArray<UnityEngine.CanvasRenderer.InstanceRange> rangers)
  RVA=0x03D21200  token=0x6000034  System.Void* BeginInstanceBufferWrite(System.UInt32 count, System.UInt32 stride)
  RVA=0x044C5000  token=0x6000035  System.Void* BeginPrefabBufferWrite(System.UInt32 count, System.UInt32 stride)
  RVA=0x033C6CD0  token=0x6000036  System.Void EndInstanceBufferWrite()
  RVA=0x045A7E00  token=0x6000037  System.Void EndPrefabBufferWrite()
  RVA=-1  // generic def  token=0x6000038  Unity.Collections.NativeArray<T> BeginInstanceDataWrite(System.Int32 count)
  RVA=-1  // generic def  token=0x6000039  Unity.Collections.NativeArray<T> BeginPrefabBufferWrite(System.Int32 count)
  RVA=0x03941830  token=0x600003A  System.Void EnableInstance()
  RVA=0x043ED880  token=0x600003B  System.Void SetColor_Injected(UnityEngine.Color& color)
  RVA=0x0343F2A0  token=0x600003C  System.Void GetColor_Injected(UnityEngine.Color& ret)
  RVA=0x040F3870  token=0x600003D  System.Void EnableRectClipping_Injected(UnityEngine.Rect& rect)
  RVA=0x03AC9A30  token=0x600003E  System.Void set_clippingSoftness_Injected(UnityEngine.Vector2& value)
  RVA=0x03AC9A80  token=0x600003F  System.Void set_clippingHGSoftness_Injected(UnityEngine.Vector4& value)
  RVA=0x034AD380  token=0x6000040  System.Void SetImageMeshRebuildData_Injected(UnityEngine.CanvasRenderer.HGImageData& imageData, UnityEngine.Sprite sprite, UnityEngine.Rect& adjustedRect)
END_CLASS

CLASS: UnityEngine.RectTransformUtility
TYPE:  sealed class
TOKEN: 0x2000009
SIZE:  0x10
FIELDS:
  private   static readonly UnityEngine.Vector3[]           s_Corners  // static @ 0x0
METHODS:
  RVA=0x0B5688E4  token=0x6000041  UnityEngine.Vector2 PixelAdjustPoint(UnityEngine.Vector2 point, UnityEngine.Transform elementTransform, UnityEngine.Canvas canvas)
  RVA=0x0B5689A8  token=0x6000042  UnityEngine.Rect PixelAdjustRect(UnityEngine.RectTransform rectTransform, UnityEngine.Canvas canvas)
  RVA=0x031BEB80  token=0x6000043  System.Boolean PointInRectangle(UnityEngine.Vector2 screenPoint, UnityEngine.RectTransform rect, UnityEngine.Camera cam, UnityEngine.Vector4 offset)
  RVA=0x041E1670  token=0x6000044  System.Void .ctor()
  RVA=0x0B568A14  token=0x6000045  System.Boolean RectangleContainsScreenPoint(UnityEngine.RectTransform rect, UnityEngine.Vector2 screenPoint)
  RVA=0x03091150  token=0x6000046  System.Boolean RectangleContainsScreenPoint(UnityEngine.RectTransform rect, UnityEngine.Vector2 screenPoint, UnityEngine.Camera cam)
  RVA=0x031BEAD0  token=0x6000047  System.Boolean RectangleContainsScreenPoint(UnityEngine.RectTransform rect, UnityEngine.Vector2 screenPoint, UnityEngine.Camera cam, UnityEngine.Vector4 offset)
  RVA=0x03095B10  token=0x6000048  System.Boolean ScreenPointToWorldPointInRectangle(UnityEngine.RectTransform rect, UnityEngine.Vector2 screenPoint, UnityEngine.Camera cam, UnityEngine.Vector3& worldPoint)
  RVA=0x03095A10  token=0x6000049  System.Boolean ScreenPointToLocalPointInRectangle(UnityEngine.RectTransform rect, UnityEngine.Vector2 screenPoint, UnityEngine.Camera cam, UnityEngine.Vector2& localPoint)
  RVA=0x03095F50  token=0x600004A  UnityEngine.Ray ScreenPointToRay(UnityEngine.Camera cam, UnityEngine.Vector2 screenPos)
  RVA=0x03097530  token=0x600004B  UnityEngine.Vector2 WorldToScreenPoint(UnityEngine.Camera cam, UnityEngine.Vector3 worldPoint)
  RVA=0x0B568030  token=0x600004C  UnityEngine.Bounds CalculateRelativeRectTransformBounds(UnityEngine.Transform root, UnityEngine.Transform child)
  RVA=0x0B5683B0  token=0x600004D  UnityEngine.Bounds CalculateRelativeRectTransformBounds(UnityEngine.Transform trans)
  RVA=0x0B568640  token=0x600004E  System.Void FlipLayoutOnAxis(UnityEngine.RectTransform rect, System.Int32 axis, System.Boolean keepPositioning, System.Boolean recursive)
  RVA=0x0B568428  token=0x600004F  System.Void FlipLayoutAxes(UnityEngine.RectTransform rect, System.Boolean keepPositioning, System.Boolean recursive)
  RVA=0x04DBEF00  token=0x6000050  UnityEngine.Vector2 GetTransposed(UnityEngine.Vector2 input)
  RVA=0x04D36610  token=0x6000051  System.Void .cctor()
  RVA=0x0B568880  token=0x6000052  System.Void PixelAdjustPoint_Injected(UnityEngine.Vector2& point, UnityEngine.Transform elementTransform, UnityEngine.Canvas canvas, UnityEngine.Vector2& ret)
  RVA=0x0B568954  token=0x6000053  System.Void PixelAdjustRect_Injected(UnityEngine.RectTransform rectTransform, UnityEngine.Canvas canvas, UnityEngine.Rect& ret)
  RVA=0x031BEC00  token=0x6000054  System.Boolean PointInRectangle_Injected(UnityEngine.Vector2& screenPoint, UnityEngine.RectTransform rect, UnityEngine.Camera cam, UnityEngine.Vector4& offset)
END_CLASS

CLASS: UnityEngine.RenderMode
TYPE:  sealed struct
TOKEN: 0x200000A
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.RenderMode          ScreenSpaceOverlay  // const
  public    static  UnityEngine.RenderMode          ScreenSpaceCamera  // const
  public    static  UnityEngine.RenderMode          WorldSpace  // const
METHODS:
END_CLASS

CLASS: UnityEngine.AdditionalCanvasShaderChannels
TYPE:  sealed struct
TOKEN: 0x200000B
SIZE:  0x14
FIELDS:
  public            System.Int32                    value__  // 0x10
  public    static  UnityEngine.AdditionalCanvasShaderChannelsNone  // const
  public    static  UnityEngine.AdditionalCanvasShaderChannelsTexCoord1  // const
  public    static  UnityEngine.AdditionalCanvasShaderChannelsTexCoord2  // const
  public    static  UnityEngine.AdditionalCanvasShaderChannelsTexCoord3  // const
  public    static  UnityEngine.AdditionalCanvasShaderChannelsNormal  // const
  public    static  UnityEngine.AdditionalCanvasShaderChannelsTangent  // const
METHODS:
END_CLASS

CLASS: UnityEngine.Canvas
TYPE:  sealed class
TOKEN: 0x200000C
SIZE:  0x18
EXTENDS: UnityEngine.Behaviour
FIELDS:
  private   static  UnityEngine.Canvas.WillRenderCanvasespreWillRenderCanvases  // static @ 0x0
  private   static  UnityEngine.Canvas.WillRenderCanvaseswillRenderCanvases  // static @ 0x8
  private   static  System.Action<System.Int32>     <externBeginRenderOverlays>k__BackingField  // static @ 0x10
  private   static  System.Action<System.Int32,System.Int32><externRenderOverlaysBefore>k__BackingField  // static @ 0x18
  private   static  System.Action<System.Int32>     <externEndRenderOverlays>k__BackingField  // static @ 0x20
PROPERTIES:
  renderMode  get=0x02FC7500  set=0x0434F490
  isRootCanvas  get=0x02FC74C0
  pixelRect  get=0x0B567AE4
  scaleFactor  get=0x031C2F50  set=0x030997E0
  referencePixelsPerUnit  get=0x031C4660  set=0x0B567EA4
  overridePixelPerfect  get=0x0B567A6C  set=0x0B567E1C
  vertexColorAlwaysGammaSpace  get=0x0B567BE0  set=0x0B567FEC
  pixelPerfect  get=0x031C2F90  set=0x0B567E60
  planeDistance  get=0x0B567B10  set=0x0434BB80
  renderOrder  get=0x0B567B44
  overrideSorting  get=0x031ED780  set=0x037D4DF0
  sortingOrder  get=0x03296810  set=0x037D4F10
  targetDisplay  get=0x03B3BFE0  set=0x0B567FAC
  sortingLayerID  get=0x03F0A8C0  set=0x0B567F28
  cachedSortingLayerValue  get=0x0B5678F4
  additionalShaderChannels  get=0x0418F290  set=0x0B567C14
  sortingLayerName  get=0x0B567BAC  set=0x0B567F68
  rootCanvas  get=0x0323FB20
  renderingDisplaySize  get=0x034AD3F0
  doCPUFrameInterpolation  get=0x0B567928  set=0x0B567C54
  joinGPUFrameInterpolation  get=0x0B567A04  set=0x0B567D94
  externBeginRenderOverlays  get=0x0B56795C  set=0x0B567C98
  externRenderOverlaysBefore  get=0x0B5679CC  set=0x0B567D40
  externEndRenderOverlays  get=0x0B567994  set=0x0B567CEC
  worldCamera  get=0x03096EB0  set=0x041E54D0
  normalizedSortingGridSize  get=0x0B567A38  set=0x0B567DD8
  sortingGridNormalizedSize  get=0x0B567B78  set=0x0B567EE8
EVENTS:
  preWillRenderCanvases  add=add_preWillRenderCanvases  remove=remove_preWillRenderCanvases
  willRenderCanvases  add=add_willRenderCanvases  remove=remove_willRenderCanvases
METHODS:
  RVA=0x0B56787C  token=0x6000083  System.Void SetExternalCanvasEnabled(System.Boolean enabled)
  RVA=0x0B5677C8  token=0x600008A  UnityEngine.Material GetDefaultCanvasTextMaterial()
  RVA=0x0B56779C  token=0x600008B  UnityEngine.Material GetDefaultCanvasMaterial()
  RVA=0x0B5677F4  token=0x600008C  UnityEngine.Material GetETC1SupportedCanvasMaterial()
  RVA=0x0B5678B0  token=0x600008D  System.Void UpdateCanvasRectTransform(System.Boolean alignWithCamera)
  RVA=0x03BFD110  token=0x600008E  System.Void ForceUpdateCanvases()
  RVA=0x0314D620  token=0x600008F  System.Void SendPreWillRenderCanvases()
  RVA=0x0314D640  token=0x6000090  System.Void SendWillRenderCanvases()
  RVA=0x0B5676FC  token=0x6000091  System.Void BeginRenderExtraOverlays(System.Int32 displayIndex)
  RVA=0x0B567820  token=0x6000092  System.Void RenderExtraOverlaysBefore(System.Int32 displayIndex, System.Int32 sortingOrder)
  RVA=0x0B56774C  token=0x6000093  System.Void EndRenderExtraOverlays(System.Int32 displayIndex)
  RVA=0x05393520  token=0x6000094  System.Void .ctor()
  RVA=0x0B567AA0  token=0x6000095  System.Void get_pixelRect_Injected(UnityEngine.Rect& ret)
  RVA=0x034AD440  token=0x6000096  System.Void get_renderingDisplaySize_Injected(UnityEngine.Vector2& ret)
END_CLASS

CLASS: UnityEngine.UISystemProfilerApi
TYPE:  static class
TOKEN: 0x200000E
SIZE:  0x10
FIELDS:
METHODS:
  RVA=0x031BCF90  token=0x6000099  System.Void BeginSample(UnityEngine.UISystemProfilerApi.SampleType type)
  RVA=0x031BCF50  token=0x600009A  System.Void EndSample(UnityEngine.UISystemProfilerApi.SampleType type)
  RVA=0x031EABC0  token=0x600009B  System.Void AddMarker(System.String name, UnityEngine.Object obj)
END_CLASS

CLASS: UnityEngine.UI.HGRectMaskClippingBindings
TYPE:  static class
TOKEN: 0x2000010
SIZE:  0x10
FIELDS:
PROPERTIES:
  EnableRectMaskOpt  get=0x05397CB4
METHODS:
  RVA=0x03E4D4C0  token=0x600009C  System.Void BatchComputeRectMaskClipping(System.Void* maskInputs, System.Int32 maskCount, System.Void* clipperInputs, System.Int32 clipperCount, System.Void* targetInputs, System.Int32 targetCount, System.Void* maskOutputs, System.Void* targetOutputs)
  RVA=0x034AD490  token=0x600009E  System.Boolean GetEnableRectMaskOpt()
END_CLASS

CLASS: UnityEngine.UI.HGImageBatchBindings
TYPE:  static class
TOKEN: 0x2000016
SIZE:  0x10
FIELDS:
PROPERTIES:
  EnableImageOpt  get=0x05397CC0
METHODS:
  RVA=0x03F68610  token=0x600009F  System.Void BatchSetImageMeshRebuildData(System.Void* inputs, System.Int32 count)
  RVA=0x034AD4D0  token=0x60000A1  System.Boolean GetEnableImageOpt()
END_CLASS

