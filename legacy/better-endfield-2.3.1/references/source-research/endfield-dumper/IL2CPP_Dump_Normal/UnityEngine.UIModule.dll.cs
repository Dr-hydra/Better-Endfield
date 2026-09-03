// ========================================================
// Dumped by @desirepro
// Assembly: UnityEngine.UIModule.dll
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

    // TypeToken: 0x2000005  // size: 0x14
    public sealed struct HGFillMethod
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.CanvasRenderer.HGFillMethod Horizontal;  // const
        public static UnityEngine.CanvasRenderer.HGFillMethod Vertical;  // const
        public static UnityEngine.CanvasRenderer.HGFillMethod Radial90;  // const
        public static UnityEngine.CanvasRenderer.HGFillMethod Radial180;  // const
        public static UnityEngine.CanvasRenderer.HGFillMethod Radial360;  // const

    }

    // TypeToken: 0x2000006  // size: 0x14
    public sealed struct HGImageType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.CanvasRenderer.HGImageType Simple;  // const
        public static UnityEngine.CanvasRenderer.HGImageType Sliced;  // const
        public static UnityEngine.CanvasRenderer.HGImageType Tiled;  // const
        public static UnityEngine.CanvasRenderer.HGImageType Filled;  // const
        public static UnityEngine.CanvasRenderer.HGImageType Mirror;  // const
        public static UnityEngine.CanvasRenderer.HGImageType MirrorQuarter;  // const

    }

    // TypeToken: 0x2000007  // size: 0x30
    public sealed struct HGImageData
    {
        // Fields
        public UnityEngine.CanvasRenderer.HGImageType type;  // 0x10
        public UnityEngine.CanvasRenderer.HGFillMethod fillMethod;  // 0x14
        public System.Single fillAmount;  // 0x18
        public System.Int32 fillOrigin;  // 0x1c
        public System.Single pixelsPerUnit;  // 0x20
        public System.Single pixelsPerUnitMultiplier;  // 0x24
        public UnityEngine.Color32 color;  // 0x28
        public System.Boolean preserveAspect;  // 0x2c
        public System.Boolean fillCenter;  // 0x2d
        public System.Boolean fillClockwise;  // 0x2e

    }

    // TypeToken: 0x2000008  // size: 0x14
    public sealed struct InstanceRange
    {
        // Fields
        public System.UInt16 start;  // 0x10
        public System.UInt16 end;  // 0x12

    }

    // TypeToken: 0x200000D  // size: 0x80
    public sealed class WillRenderCanvases : System.MulticastDelegate
    {
        // Methods
        // RVA: 0x0309A530  token: 0x6000097
        public System.Void .ctor(System.Object object, System.IntPtr method) { }
        // RVA: 0x053DEDB8  token: 0x6000098
        public virtual System.Void Invoke() { }

    }

    // TypeToken: 0x200000F  // size: 0x14
    public sealed struct SampleType
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.UISystemProfilerApi.SampleType Layout;  // const
        public static UnityEngine.UISystemProfilerApi.SampleType Render;  // const

    }

    // TypeToken: 0x2000011  // size: 0x40
    public sealed struct HGRectMaskInput
    {
        // Fields
        public System.Int32 maskRTInstanceID;  // 0x10
        public System.Int32 rootCanvasInstanceID;  // 0x14
        public System.Int32 renderMode;  // 0x18
        public System.Int32 clipperOffset;  // 0x1c
        public System.Int32 clipperCount;  // 0x20
        public System.Int32 targetOffset;  // 0x24
        public System.Int32 targetCount;  // 0x28
        public UnityEngine.Rect lastClipRect;  // 0x2c
        public System.Int32 forceClip;  // 0x3c

    }

    // TypeToken: 0x2000012  // size: 0x28
    public sealed struct HGClipperInput
    {
        // Fields
        public System.Int32 rtInstanceID;  // 0x10
        public System.Int32 canvasInstanceID;  // 0x14
        public UnityEngine.Vector4 padding;  // 0x18

    }

    // TypeToken: 0x2000013  // size: 0x18
    public sealed struct HGClipTargetInput
    {
        // Fields
        public System.Int32 rtInstanceID;  // 0x10
        public System.Int32 canvasRendererInstanceID;  // 0x14

    }

    // TypeToken: 0x2000014  // size: 0x28
    public sealed struct HGRectMaskOutput
    {
        // Fields
        public UnityEngine.Rect clipRect;  // 0x10
        public System.Int32 validRect;  // 0x20
        public System.Int32 maskIsCulled;  // 0x24

    }

    // TypeToken: 0x2000015  // size: 0x18
    public sealed struct HGClipTargetOutput
    {
        // Fields
        public System.Int32 shouldCull;  // 0x10
        public System.Int32 cullStateChanged;  // 0x14

    }

    // TypeToken: 0x2000017  // size: 0x48
    public sealed struct HGBatchImageInput
    {
        // Fields
        public System.Int32 canvasRendererInstanceID;  // 0x10
        public System.Int32 spriteInstanceID;  // 0x14
        public UnityEngine.Rect adjustedRect;  // 0x18
        public UnityEngine.CanvasRenderer.HGImageData imageData;  // 0x28

    }

namespace UnityEngine
{

    // TypeToken: 0x2000002
    public interface ICanvasRaycastFilter
    {
        // Methods
        // RVA: -1  // abstract  token: 0x6000001
        public virtual System.Boolean IsRaycastLocationValid(UnityEngine.Vector2 sp, UnityEngine.Camera eventCamera) { }

    }

    // TypeToken: 0x2000003  // size: 0x18
    public sealed class CanvasGroup : UnityEngine.Behaviour, UnityEngine.ICanvasRaycastFilter
    {
        // Properties
        UnityEngine.Color color { get; /* RVA: 0x02FA9CA0 */ set; /* RVA: 0x05390B3C */ }
        System.Single alpha { get; /* RVA: 0x03966E10 */ set; /* RVA: 0x03046300 */ }
        System.Boolean interactable { get; /* RVA: 0x02FC7E30 */ set; /* RVA: 0x039D4A20 */ }
        System.Boolean blocksRaycasts { get; /* RVA: 0x030912E0 */ set; /* RVA: 0x039D4B90 */ }
        System.Boolean ignoreParentGroups { get; /* RVA: 0x02FC7DF0 */ set; /* RVA: 0x0B567398 */ }

        // Methods
        // RVA: 0x05390FCC  token: 0x600000C
        public virtual System.Boolean IsRaycastLocationValid(UnityEngine.Vector2 sp, UnityEngine.Camera eventCamera) { }
        // RVA: 0x05393520  token: 0x600000D
        public System.Void .ctor() { }
        // RVA: 0x02FA9D00  token: 0x600000E
        private System.Void get_color_Injected(UnityEngine.Color& ret) { }
        // RVA: 0x03B143B0  token: 0x600000F
        private System.Void set_color_Injected(UnityEngine.Color& value) { }

    }

    // TypeToken: 0x2000004  // size: 0x20
    public sealed class CanvasRenderer : UnityEngine.Component
    {
        // Fields
        private System.Boolean <isMask>k__BackingField;  // 0x18

        // Properties
        System.Boolean hasPopInstruction { set; /* RVA: 0x041D2390 */ }
        System.Int32 materialCount { get; /* RVA: 0x02EDDCF0 */ set; /* RVA: 0x031BD070 */ }
        System.Int32 popMaterialCount { set; /* RVA: 0x041D22D0 */ }
        System.Int32 absoluteDepth { get; /* RVA: 0x031BF7C0 */ }
        System.Boolean hasMoved { get; /* RVA: 0x0B567684 */ }
        System.Boolean cullTransparentMesh { get; /* RVA: 0x0418F2D0 */ set; /* RVA: 0x0B5676B8 */ }
        System.Boolean cull { get; /* RVA: 0x031BEDB0 */ set; /* RVA: 0x03519F70 */ }
        UnityEngine.Vector2 clippingSoftness { set; /* RVA: 0x05391498 */ }
        UnityEngine.Vector4 clippingHGSoftness { set; /* RVA: 0x05391484 */ }

        // Methods
        // RVA: 0x043ED860  token: 0x600001A
        public System.Void SetColor(UnityEngine.Color color) { }
        // RVA: 0x0343F240  token: 0x600001B
        public UnityEngine.Color GetColor() { }
        // RVA: 0x053916A8  token: 0x600001C
        public System.Void EnableRectClipping(UnityEngine.Rect rect) { }
        // RVA: 0x031EB190  token: 0x600001F
        public System.Void DisableRectClipping() { }
        // RVA: 0x031BD010  token: 0x6000020
        public System.Void SetMaterial(UnityEngine.Material material, System.Int32 index) { }
        // RVA: 0x0B5674C0  token: 0x6000021
        public UnityEngine.Material GetMaterial(System.Int32 index) { }
        // RVA: 0x041D2320  token: 0x6000022
        public System.Void SetPopMaterial(UnityEngine.Material material, System.Int32 index) { }
        // RVA: 0x031BD0C0  token: 0x6000023
        public System.Void SetTexture(UnityEngine.Texture texture) { }
        // RVA: 0x03F20C10  token: 0x6000024
        public System.Void SetAlphaTexture(UnityEngine.Texture texture) { }
        // RVA: 0x031F2610  token: 0x6000025
        public System.Void SetMesh(UnityEngine.Mesh mesh) { }
        // RVA: 0x0351A5F0  token: 0x6000026
        public System.Void Clear() { }
        // RVA: 0x0B567558  token: 0x6000027
        public System.Void SetImageMeshRebuildData(UnityEngine.CanvasRenderer.HGImageData imageData, UnityEngine.Sprite sprite, UnityEngine.Rect adjustedRect) { }
        // RVA: 0x0B567500  token: 0x6000028
        public System.Void SetAlpha(System.Single alpha) { }
        // RVA: 0x033A0AE0  token: 0x6000029
        public System.Single GetInheritedAlpha() { }
        // RVA: 0x02EDDC40  token: 0x600002A
        public System.Void SetMaterial(UnityEngine.Material material, UnityEngine.Texture texture) { }
        // RVA: 0x0B5674B4  token: 0x600002B
        public UnityEngine.Material GetMaterial() { }
        // RVA: 0x0B567618  token: 0x600002C
        public static System.Void SplitUIVertexStreams(System.Collections.Generic.List<UnityEngine.UIVertex> verts, System.Collections.Generic.List<UnityEngine.Vector3> positions, System.Collections.Generic.List<UnityEngine.Color32> colors, System.Collections.Generic.List<UnityEngine.Vector4> uv0S, System.Collections.Generic.List<UnityEngine.Vector4> uv1S, System.Collections.Generic.List<UnityEngine.Vector4> uv2S, System.Collections.Generic.List<UnityEngine.Vector4> uv3S, System.Collections.Generic.List<UnityEngine.Vector3> normals, System.Collections.Generic.List<UnityEngine.Vector4> tangents, System.Collections.Generic.List<System.Int32> indices) { }
        // RVA: 0x0B567450  token: 0x600002D
        public static System.Void CreateUIVertexStream(System.Collections.Generic.List<UnityEngine.UIVertex> verts, System.Collections.Generic.List<UnityEngine.Vector3> positions, System.Collections.Generic.List<UnityEngine.Color32> colors, System.Collections.Generic.List<UnityEngine.Vector4> uv0S, System.Collections.Generic.List<UnityEngine.Vector4> uv1S, System.Collections.Generic.List<UnityEngine.Vector4> uv2S, System.Collections.Generic.List<UnityEngine.Vector4> uv3S, System.Collections.Generic.List<UnityEngine.Vector3> normals, System.Collections.Generic.List<UnityEngine.Vector4> tangents, System.Collections.Generic.List<System.Int32> indices) { }
        // RVA: 0x0B5673DC  token: 0x600002E
        public static System.Void AddUIVertexStream(System.Collections.Generic.List<UnityEngine.UIVertex> verts, System.Collections.Generic.List<UnityEngine.Vector3> positions, System.Collections.Generic.List<UnityEngine.Color32> colors, System.Collections.Generic.List<UnityEngine.Vector4> uv0S, System.Collections.Generic.List<UnityEngine.Vector4> uv1S, System.Collections.Generic.List<UnityEngine.Vector4> uv2S, System.Collections.Generic.List<UnityEngine.Vector4> uv3S, System.Collections.Generic.List<UnityEngine.Vector3> normals, System.Collections.Generic.List<UnityEngine.Vector4> tangents) { }
        // RVA: 0x0B567570  token: 0x600002F
        private static System.Void SplitIndicesStreamsInternal(System.Object verts, System.Object indices) { }
        // RVA: 0x0B5675B4  token: 0x6000030
        private static System.Void SplitUIVertexStreamsInternal(System.Object verts, System.Object positions, System.Object colors, System.Object uv0S, System.Object uv1S, System.Object uv2S, System.Object uv3S, System.Object normals, System.Object tangents) { }
        // RVA: 0x0B5673EC  token: 0x6000031
        private static System.Void CreateUIVertexStreamInternal(System.Object verts, System.Object positions, System.Object colors, System.Object uv0S, System.Object uv1S, System.Object uv2S, System.Object uv3S, System.Object normals, System.Object tangents, System.Object indices) { }
        // RVA: 0x03AA7CD0  token: 0x6000032
        private System.Void UpdateInstanceRange(System.UInt32 rangeCount, System.Void* ranges) { }
        // RVA: 0x03AA7C70  token: 0x6000033
        public System.Void UpdateInstanceRange(System.UInt32 rangeCount, Unity.Collections.NativeArray<UnityEngine.CanvasRenderer.InstanceRange> rangers) { }
        // RVA: 0x03D21200  token: 0x6000034
        private System.Void* BeginInstanceBufferWrite(System.UInt32 count, System.UInt32 stride) { }
        // RVA: 0x044C5000  token: 0x6000035
        private System.Void* BeginPrefabBufferWrite(System.UInt32 count, System.UInt32 stride) { }
        // RVA: 0x033C6CD0  token: 0x6000036
        public System.Void EndInstanceBufferWrite() { }
        // RVA: 0x045A7E00  token: 0x6000037
        public System.Void EndPrefabBufferWrite() { }
        // RVA: -1  // generic def  token: 0x6000038
        public Unity.Collections.NativeArray<T> BeginInstanceDataWrite(System.Int32 count) { }
        // RVA: -1  // generic def  token: 0x6000039
        public Unity.Collections.NativeArray<T> BeginPrefabBufferWrite(System.Int32 count) { }
        // RVA: 0x03941830  token: 0x600003A
        public System.Void EnableInstance() { }
        // RVA: 0x043ED880  token: 0x600003B
        private System.Void SetColor_Injected(UnityEngine.Color& color) { }
        // RVA: 0x0343F2A0  token: 0x600003C
        private System.Void GetColor_Injected(UnityEngine.Color& ret) { }
        // RVA: 0x040F3870  token: 0x600003D
        private System.Void EnableRectClipping_Injected(UnityEngine.Rect& rect) { }
        // RVA: 0x03AC9A30  token: 0x600003E
        private System.Void set_clippingSoftness_Injected(UnityEngine.Vector2& value) { }
        // RVA: 0x03AC9A80  token: 0x600003F
        private System.Void set_clippingHGSoftness_Injected(UnityEngine.Vector4& value) { }
        // RVA: 0x034AD380  token: 0x6000040
        private System.Void SetImageMeshRebuildData_Injected(UnityEngine.CanvasRenderer.HGImageData& imageData, UnityEngine.Sprite sprite, UnityEngine.Rect& adjustedRect) { }

    }

    // TypeToken: 0x2000009  // size: 0x10
    public sealed class RectTransformUtility
    {
        // Fields
        private static readonly UnityEngine.Vector3[] s_Corners;  // static @ 0x0

        // Methods
        // RVA: 0x0B5688E4  token: 0x6000041
        public static UnityEngine.Vector2 PixelAdjustPoint(UnityEngine.Vector2 point, UnityEngine.Transform elementTransform, UnityEngine.Canvas canvas) { }
        // RVA: 0x0B5689A8  token: 0x6000042
        public static UnityEngine.Rect PixelAdjustRect(UnityEngine.RectTransform rectTransform, UnityEngine.Canvas canvas) { }
        // RVA: 0x031BEB80  token: 0x6000043
        private static System.Boolean PointInRectangle(UnityEngine.Vector2 screenPoint, UnityEngine.RectTransform rect, UnityEngine.Camera cam, UnityEngine.Vector4 offset) { }
        // RVA: 0x041E1670  token: 0x6000044
        private System.Void .ctor() { }
        // RVA: 0x0B568A14  token: 0x6000045
        public static System.Boolean RectangleContainsScreenPoint(UnityEngine.RectTransform rect, UnityEngine.Vector2 screenPoint) { }
        // RVA: 0x03091150  token: 0x6000046
        public static System.Boolean RectangleContainsScreenPoint(UnityEngine.RectTransform rect, UnityEngine.Vector2 screenPoint, UnityEngine.Camera cam) { }
        // RVA: 0x031BEAD0  token: 0x6000047
        public static System.Boolean RectangleContainsScreenPoint(UnityEngine.RectTransform rect, UnityEngine.Vector2 screenPoint, UnityEngine.Camera cam, UnityEngine.Vector4 offset) { }
        // RVA: 0x03095B10  token: 0x6000048
        public static System.Boolean ScreenPointToWorldPointInRectangle(UnityEngine.RectTransform rect, UnityEngine.Vector2 screenPoint, UnityEngine.Camera cam, UnityEngine.Vector3& worldPoint) { }
        // RVA: 0x03095A10  token: 0x6000049
        public static System.Boolean ScreenPointToLocalPointInRectangle(UnityEngine.RectTransform rect, UnityEngine.Vector2 screenPoint, UnityEngine.Camera cam, UnityEngine.Vector2& localPoint) { }
        // RVA: 0x03095F50  token: 0x600004A
        public static UnityEngine.Ray ScreenPointToRay(UnityEngine.Camera cam, UnityEngine.Vector2 screenPos) { }
        // RVA: 0x03097530  token: 0x600004B
        public static UnityEngine.Vector2 WorldToScreenPoint(UnityEngine.Camera cam, UnityEngine.Vector3 worldPoint) { }
        // RVA: 0x0B568030  token: 0x600004C
        public static UnityEngine.Bounds CalculateRelativeRectTransformBounds(UnityEngine.Transform root, UnityEngine.Transform child) { }
        // RVA: 0x0B5683B0  token: 0x600004D
        public static UnityEngine.Bounds CalculateRelativeRectTransformBounds(UnityEngine.Transform trans) { }
        // RVA: 0x0B568640  token: 0x600004E
        public static System.Void FlipLayoutOnAxis(UnityEngine.RectTransform rect, System.Int32 axis, System.Boolean keepPositioning, System.Boolean recursive) { }
        // RVA: 0x0B568428  token: 0x600004F
        public static System.Void FlipLayoutAxes(UnityEngine.RectTransform rect, System.Boolean keepPositioning, System.Boolean recursive) { }
        // RVA: 0x04DBEF00  token: 0x6000050
        private static UnityEngine.Vector2 GetTransposed(UnityEngine.Vector2 input) { }
        // RVA: 0x04D36610  token: 0x6000051
        private static System.Void .cctor() { }
        // RVA: 0x0B568880  token: 0x6000052
        private static System.Void PixelAdjustPoint_Injected(UnityEngine.Vector2& point, UnityEngine.Transform elementTransform, UnityEngine.Canvas canvas, UnityEngine.Vector2& ret) { }
        // RVA: 0x0B568954  token: 0x6000053
        private static System.Void PixelAdjustRect_Injected(UnityEngine.RectTransform rectTransform, UnityEngine.Canvas canvas, UnityEngine.Rect& ret) { }
        // RVA: 0x031BEC00  token: 0x6000054
        private static System.Boolean PointInRectangle_Injected(UnityEngine.Vector2& screenPoint, UnityEngine.RectTransform rect, UnityEngine.Camera cam, UnityEngine.Vector4& offset) { }

    }

    // TypeToken: 0x200000A  // size: 0x14
    public sealed struct RenderMode
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.RenderMode ScreenSpaceOverlay;  // const
        public static UnityEngine.RenderMode ScreenSpaceCamera;  // const
        public static UnityEngine.RenderMode WorldSpace;  // const

    }

    // TypeToken: 0x200000B  // size: 0x14
    public sealed struct AdditionalCanvasShaderChannels
    {
        // Fields
        public System.Int32 value__;  // 0x10
        public static UnityEngine.AdditionalCanvasShaderChannels None;  // const
        public static UnityEngine.AdditionalCanvasShaderChannels TexCoord1;  // const
        public static UnityEngine.AdditionalCanvasShaderChannels TexCoord2;  // const
        public static UnityEngine.AdditionalCanvasShaderChannels TexCoord3;  // const
        public static UnityEngine.AdditionalCanvasShaderChannels Normal;  // const
        public static UnityEngine.AdditionalCanvasShaderChannels Tangent;  // const

    }

    // TypeToken: 0x200000C  // size: 0x18
    public sealed class Canvas : UnityEngine.Behaviour
    {
        // Fields
        private static UnityEngine.Canvas.WillRenderCanvases preWillRenderCanvases;  // static @ 0x0
        private static UnityEngine.Canvas.WillRenderCanvases willRenderCanvases;  // static @ 0x8
        private static System.Action<System.Int32> <externBeginRenderOverlays>k__BackingField;  // static @ 0x10
        private static System.Action<System.Int32,System.Int32> <externRenderOverlaysBefore>k__BackingField;  // static @ 0x18
        private static System.Action<System.Int32> <externEndRenderOverlays>k__BackingField;  // static @ 0x20

        // Properties
        UnityEngine.RenderMode renderMode { get; /* RVA: 0x02FC7500 */ set; /* RVA: 0x0434F490 */ }
        System.Boolean isRootCanvas { get; /* RVA: 0x02FC74C0 */ }
        UnityEngine.Rect pixelRect { get; /* RVA: 0x0B567AE4 */ }
        System.Single scaleFactor { get; /* RVA: 0x031C2F50 */ set; /* RVA: 0x030997E0 */ }
        System.Single referencePixelsPerUnit { get; /* RVA: 0x031C4660 */ set; /* RVA: 0x0B567EA4 */ }
        System.Boolean overridePixelPerfect { get; /* RVA: 0x0B567A6C */ set; /* RVA: 0x0B567E1C */ }
        System.Boolean vertexColorAlwaysGammaSpace { get; /* RVA: 0x0B567BE0 */ set; /* RVA: 0x0B567FEC */ }
        System.Boolean pixelPerfect { get; /* RVA: 0x031C2F90 */ set; /* RVA: 0x0B567E60 */ }
        System.Single planeDistance { get; /* RVA: 0x0B567B10 */ set; /* RVA: 0x0434BB80 */ }
        System.Int32 renderOrder { get; /* RVA: 0x0B567B44 */ }
        System.Boolean overrideSorting { get; /* RVA: 0x031ED780 */ set; /* RVA: 0x037D4DF0 */ }
        System.Int32 sortingOrder { get; /* RVA: 0x03296810 */ set; /* RVA: 0x037D4F10 */ }
        System.Int32 targetDisplay { get; /* RVA: 0x03B3BFE0 */ set; /* RVA: 0x0B567FAC */ }
        System.Int32 sortingLayerID { get; /* RVA: 0x03F0A8C0 */ set; /* RVA: 0x0B567F28 */ }
        System.Int32 cachedSortingLayerValue { get; /* RVA: 0x0B5678F4 */ }
        UnityEngine.AdditionalCanvasShaderChannels additionalShaderChannels { get; /* RVA: 0x0418F290 */ set; /* RVA: 0x0B567C14 */ }
        System.String sortingLayerName { get; /* RVA: 0x0B567BAC */ set; /* RVA: 0x0B567F68 */ }
        UnityEngine.Canvas rootCanvas { get; /* RVA: 0x0323FB20 */ }
        UnityEngine.Vector2 renderingDisplaySize { get; /* RVA: 0x034AD3F0 */ }
        System.Boolean doCPUFrameInterpolation { get; /* RVA: 0x0B567928 */ set; /* RVA: 0x0B567C54 */ }
        System.Boolean joinGPUFrameInterpolation { get; /* RVA: 0x0B567A04 */ set; /* RVA: 0x0B567D94 */ }
        System.Action<System.Int32> externBeginRenderOverlays { get; /* RVA: 0x0B56795C */ set; /* RVA: 0x0B567C98 */ }
        System.Action<System.Int32,System.Int32> externRenderOverlaysBefore { get; /* RVA: 0x0B5679CC */ set; /* RVA: 0x0B567D40 */ }
        System.Action<System.Int32> externEndRenderOverlays { get; /* RVA: 0x0B567994 */ set; /* RVA: 0x0B567CEC */ }
        UnityEngine.Camera worldCamera { get; /* RVA: 0x03096EB0 */ set; /* RVA: 0x041E54D0 */ }
        System.Single normalizedSortingGridSize { get; /* RVA: 0x0B567A38 */ set; /* RVA: 0x0B567DD8 */ }
        System.Int32 sortingGridNormalizedSize { get; /* RVA: 0x0B567B78 */ set; /* RVA: 0x0B567EE8 */ }

        // Events
        event UnityEngine.Canvas.WillRenderCanvases preWillRenderCanvases;
        event UnityEngine.Canvas.WillRenderCanvases willRenderCanvases;

        // Methods
        // RVA: 0x0B56787C  token: 0x6000083
        private static System.Void SetExternalCanvasEnabled(System.Boolean enabled) { }
        // RVA: 0x0B5677C8  token: 0x600008A
        public static UnityEngine.Material GetDefaultCanvasTextMaterial() { }
        // RVA: 0x0B56779C  token: 0x600008B
        public static UnityEngine.Material GetDefaultCanvasMaterial() { }
        // RVA: 0x0B5677F4  token: 0x600008C
        public static UnityEngine.Material GetETC1SupportedCanvasMaterial() { }
        // RVA: 0x0B5678B0  token: 0x600008D
        private System.Void UpdateCanvasRectTransform(System.Boolean alignWithCamera) { }
        // RVA: 0x03BFD110  token: 0x600008E
        public static System.Void ForceUpdateCanvases() { }
        // RVA: 0x0314D620  token: 0x600008F
        private static System.Void SendPreWillRenderCanvases() { }
        // RVA: 0x0314D640  token: 0x6000090
        private static System.Void SendWillRenderCanvases() { }
        // RVA: 0x0B5676FC  token: 0x6000091
        private static System.Void BeginRenderExtraOverlays(System.Int32 displayIndex) { }
        // RVA: 0x0B567820  token: 0x6000092
        private static System.Void RenderExtraOverlaysBefore(System.Int32 displayIndex, System.Int32 sortingOrder) { }
        // RVA: 0x0B56774C  token: 0x6000093
        private static System.Void EndRenderExtraOverlays(System.Int32 displayIndex) { }
        // RVA: 0x05393520  token: 0x6000094
        public System.Void .ctor() { }
        // RVA: 0x0B567AA0  token: 0x6000095
        private System.Void get_pixelRect_Injected(UnityEngine.Rect& ret) { }
        // RVA: 0x034AD440  token: 0x6000096
        private System.Void get_renderingDisplaySize_Injected(UnityEngine.Vector2& ret) { }

    }

    // TypeToken: 0x200000E  // size: 0x10
    public static class UISystemProfilerApi
    {
        // Methods
        // RVA: 0x031BCF90  token: 0x6000099
        public static System.Void BeginSample(UnityEngine.UISystemProfilerApi.SampleType type) { }
        // RVA: 0x031BCF50  token: 0x600009A
        public static System.Void EndSample(UnityEngine.UISystemProfilerApi.SampleType type) { }
        // RVA: 0x031EABC0  token: 0x600009B
        public static System.Void AddMarker(System.String name, UnityEngine.Object obj) { }

    }

}

namespace UnityEngine.UI
{

    // TypeToken: 0x2000010  // size: 0x10
    public static class HGRectMaskClippingBindings
    {
        // Properties
        System.Boolean EnableRectMaskOpt { get; /* RVA: 0x05397CB4 */ }

        // Methods
        // RVA: 0x03E4D4C0  token: 0x600009C
        public static System.Void BatchComputeRectMaskClipping(System.Void* maskInputs, System.Int32 maskCount, System.Void* clipperInputs, System.Int32 clipperCount, System.Void* targetInputs, System.Int32 targetCount, System.Void* maskOutputs, System.Void* targetOutputs) { }
        // RVA: 0x034AD490  token: 0x600009E
        private static System.Boolean GetEnableRectMaskOpt() { }

    }

    // TypeToken: 0x2000016  // size: 0x10
    public static class HGImageBatchBindings
    {
        // Properties
        System.Boolean EnableImageOpt { get; /* RVA: 0x05397CC0 */ }

        // Methods
        // RVA: 0x03F68610  token: 0x600009F
        public static System.Void BatchSetImageMeshRebuildData(System.Void* inputs, System.Int32 count) { }
        // RVA: 0x034AD4D0  token: 0x60000A1
        private static System.Boolean GetEnableImageOpt() { }

    }

}

