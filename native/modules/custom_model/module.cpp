// Resource-delivery implementation. See CUSTOM_MODEL_RESOURCE_RELEASE_CALL_CHAIN_20260916.md.
// Mesh, material and texture algorithms migrated from the 84b88bfb PoC;
// Build Jobs own unpublished objects across frames; published Mesh/Texture
// assets are only observed weakly (no idle retention), with receiver-local bindings.
#include "BetterEndfieldNext/ModuleApi.h"
#include "BetterEndfieldNext/CustomModelGeometry.h"
#include "bem.h"
#include "async_loading.h"
#include "model_content_identity.h"
#include "native_mesh_layout.h"
#include "mod_registry.h"
#include "resource_policy.h"
#include "generic_model_matcher.h"
#include "aurora_legacy_compat.h"
#include "texture_binding_policy.h"
#if defined(__ANDROID__)
#include "modules/custom_model/android_mesh_builder.h"
#include "android_lod_relations.generated.h"
#include "android_texture_budget_policy.h"
#endif
#if defined(_WIN32)
#include <Windows.h>
#include "model_overlay_host.h"
#include "runtime_ini_win32.h"
#else
#include "platform_compat.h"
#endif
#include "animation_driver.h"
#include "driver_audio.h"
#include <algorithm>
#include <array>
#include <atomic>
#include <cmath>
#include <condition_variable>
#include <cstring>
#include <chrono>
#include <charconv>
#include <filesystem>
#include <fstream>
#include <map>
#include <memory>
#include <mutex>
#include <set>
#include <string>
#include <string_view>
#include <unordered_map>
#include <unordered_set>
#include <vector>
#if defined(__ANDROID__)
namespace betterendfieldnext {
bool AndroidSetCustomModelDriver(const char* id, int state, int speed_millis);
std::string AndroidGetCustomModelDriversJson();
}
#endif
namespace BetterEndfieldNext::CustomModel {
namespace {
constexpr char kModuleId[]="betterendfieldnext.custom_model";
const BE_HostApiV1* g_host=nullptr;
std::atomic_bool g_hot_switch_runtime{false};
std::atomic_bool g_clone_support_runtime{false};
std::atomic_bool g_release_retired_resources_runtime{false};
void WatchModelCloneSource(void* asset);
std::atomic<DWORD> g_pump_thread{0};
uint64_t g_registry_generation = 0;
#if defined(_WIN32)
ModelOverlayHost g_model_overlay;
#endif
AnimationDriverHost g_animation_driver;
std::string PackagePathUtf8(const std::filesystem::path& path);
void EnsureAnimationDriverLoaded();
void UnloadAnimationDriver(const char* reason);
std::string g_driver_applied_overrides;
void MaterializeDriverSounds(const std::filesystem::path& package);
void SyncDriverSounds();
void PollAnimationDriverControls();
#if defined(_WIN32)
void PublishDriverStateIfChanged();
#endif

struct DynamicMeshRecord {
    int32_t component_id = -1;
    // Immutable morph samples owned by the loaded component. Shared because one
    // component can be built into more than one live mesh (world and UI).
    std::shared_ptr<const BemMorphSet> morph;
};
std::unordered_map<int32_t, DynamicMeshRecord> g_dynamic_meshes;
// Set while the previous frame pushed driver weights into the meshes, so the
// first frame carrying none restores the authored pose exactly once.
bool g_driver_morph_active = false;
// Meshes whose GPU position buffer currently holds a driver-written pose. A
// frame with zero deltas still has to re-upload those, because the device is
// holding the previous frame; meshes that were never touched are skipped, so
// this does not turn into a per-frame whole-channel upload.
std::unordered_set<void*> g_driver_dirty_meshes;

// Section 13 sound attachments. Voices are keyed by driver id and owned by the
// platform audio backend; the animation clock never waits for audio.
std::unordered_set<std::string> g_sounding_drivers;

#if defined(__ANDROID__)
void ReapplyAndroidDriverOverrides();
void ClearAndroidDriverOverrides();
#endif
BE_ResolvedClassV1 g_skinned_renderer_class{},g_mesh_class{},g_texture2d_class{},g_material_class{},g_vector3_class{};
using ArrayNewFn = void* (*)(void* elementTypeInfo, uintptr_t length);
using ArrayHeaderSizeFn = int32_t (*)();
ArrayNewFn g_il2cpp_array_new = nullptr;
ArrayHeaderSizeFn g_il2cpp_array_header_size = nullptr;
BE_ResolvedClassV1 g_renderer_class{},g_static_renderer_class{},g_mesh_filter_class{};
BE_ResolvedClassV1 g_game_object_class{};
using ObjectClassFn=void*(*)(void*);
using ArrayNewSpecificFn=void*(*)(void*,uintptr_t);
ObjectClassFn g_object_class=nullptr;
ArrayNewSpecificFn g_array_new_specific=nullptr;
NativeMeshLayout g_native_layout{};
std::atomic_bool g_process_terminating{false};
struct Float2 {
    float x;
    float y;
};

struct Float3 {
    float x;
    float y;
    float z;
};

struct Float4 {
    float x;
    float y;
    float z;
    float w;
};

// Unboxed UnityEngine.Bounds payload: Vector3 center then Vector3 extents.
struct BoundsRaw {
    Float3 center;
    Float3 extents;
};
static_assert(sizeof(BoundsRaw) == 24);

// UnityEngine.Matrix4x4 stores m00,m10,m20,m30,m01,... so consecutive groups
// of four floats in memory are matrix columns, not rows.
struct Matrix4x4Raw {
    float m[16];
};
static_assert(sizeof(Matrix4x4Raw) == 64);

struct MethodContract {
    const char* key;
    BE_MethodDescriptorV1 descriptor;
    bool required;
    void* pointer = nullptr;
    const void* method_info = nullptr;
    bool resolved = false;
};

using MeshQueryByAttributeFn = int32_t(*)(void* mesh, int32_t argument);
using MeshQueryFn = int32_t(*)(void* mesh);
using MeshSetVertexBufferParamsFn = void(*)(
    void* mesh, int32_t vertex_count, const void* attributes,
    int32_t attribute_count);
using MeshSetVertexBufferDataFn = void(*)(
    void* mesh, int32_t stream, const void* data, int32_t data_start,
    int32_t mesh_buffer_start, int32_t count, int32_t element_size,
    int32_t flags);
using MeshSetIndexBufferParamsFn = void(*)(
    void* mesh, int32_t index_count, int32_t index_format);
using MeshSetIndexBufferDataFn = void(*)(
    void* mesh, const void* data, int32_t data_start,
    int32_t mesh_buffer_start, int32_t count, int32_t element_size,
    int32_t flags);
using MeshSetSubMeshFn = void(*)(
    void* mesh, int32_t index, const void* descriptor, int32_t flags);
using MeshUploadDataFn = void(*)(void* mesh, bool mark_no_longer_readable);

struct EngineBindings {
    MeshQueryByAttributeFn get_vertex_buffer_stride = nullptr;
    MeshQueryFn get_vertex_buffer_count = nullptr;
    MeshSetVertexBufferParamsFn set_vertex_buffer_params = nullptr;
    MeshSetVertexBufferDataFn set_vertex_buffer_data = nullptr;
    MeshSetIndexBufferParamsFn set_index_buffer_params = nullptr;
    MeshSetIndexBufferDataFn set_index_buffer_data = nullptr;
    MeshSetSubMeshFn set_sub_mesh = nullptr;
    MeshUploadDataFn upload_mesh_data = nullptr;
    int32_t cached_ptr_offset = -1;
    bool resolved = false;
};
EngineBindings g_engine{};

// UnityEngine.Mesh::SetNativeArrayForChannelImpl is the icall behind
// Mesh.SetVertices. Unlike a raw write into the native vertex buffer it marks
// the channel dirty, refreshes the GPU copy and leaves every other channel
// alone, so normals, UVs and the skin streams are never touched. It exists on
// both Windows and Android, which makes it the portable morph upload path.
using MeshSetNativeArrayForChannelFn = void(*)(void* mesh, int32_t channel, int32_t format,
    int32_t dim, const void* values, int32_t array_size, int32_t values_start,
    int32_t values_count, int32_t flags);

struct ChannelBindings {
    MeshSetNativeArrayForChannelFn set_native_channel = nullptr;
};
ChannelBindings g_channel{};

// Desktop-only A/B switch for the morph upload path. Android always uses the
// channel setter because the byte-range setter is stripped there.
constexpr int kVertexUploadChannel = 0;   // SetNativeArrayForChannelImpl (default)
constexpr int kVertexUploadByteRange = 1; // InternalSetVertexBufferData
std::atomic<int> g_vertex_upload_mode{kVertexUploadChannel};
bool g_vertex_upload_available[2] = {false, false};

// Unboxed UnityEngine.Rendering.SubMeshDescriptor: Bounds, then six Int32.
struct SubMeshDescriptorRaw {
    BoundsRaw bounds;
    int32_t topology;
    int32_t index_start;
    int32_t index_count;
    int32_t base_vertex;
    int32_t first_vertex;
    int32_t vertex_count;
};
static_assert(sizeof(SubMeshDescriptorRaw) == 48);

constexpr int32_t kTopologyTriangles = 0;
constexpr int32_t kIndexFormatUInt16 = 0;
constexpr int32_t kIndexFormatUInt32 = 1;
// MeshUpdateFlags.Default keeps Unity's own validation and bounds bookkeeping.
constexpr int32_t kMeshUpdateDefault = 0;

MethodContract g_methods[]{
    {"material_cache.helper", {"UnityEngine.CoreModule.dll","UnityEngine","Component","GetComponentInParent","System.Type|System.Boolean","UnityEngine.Component",2},false},
    {"material_cache.apply", {"Rendering.Beyond.dll","Beyond.Rendering","EntityRenderHelperMaterialController/AddedMaterialController","ApplySharedMaterialArray","Beyond.Rendering.EntityRenderHelperMaterialController/RendererInfo","System.Void",1},false},
    {"game_object.get_transform", {"UnityEngine.CoreModule.dll","UnityEngine","GameObject","get_transform",nullptr,"UnityEngine.Transform",0},true},
    {"audit.active_self", {"UnityEngine.CoreModule.dll","UnityEngine","GameObject","get_activeSelf",nullptr,"System.Boolean",0},false},
    {"audit.active_hierarchy", {"UnityEngine.CoreModule.dll","UnityEngine","GameObject","get_activeInHierarchy",nullptr,"System.Boolean",0},false},
    {"time.frame_count", {"UnityEngine.CoreModule.dll","UnityEngine","Time","get_frameCount",nullptr,"System.Int32",0},true},
    // Clone observation for frame-sliced template delivery. Optional: only
    // installed when g_model_clone_hooks_requested (Host hook chaining), see
    // InstallModelCloneHooks. Unresolved/refused -> synchronous delivery.
    {"clone.single", {"UnityEngine.CoreModule.dll","UnityEngine","Object","Internal_CloneSingle","UnityEngine.Object","UnityEngine.Object",1},false},
    {"clone.with_parent", {"UnityEngine.CoreModule.dll","UnityEngine","Object","Internal_CloneSingleWithParent",
        "UnityEngine.Object|UnityEngine.Transform|System.Boolean","UnityEngine.Object",3},false},
    // Instantiate(original, position, rotation[, parent]). Absent when stripped.
    {"clone.instantiate", {"UnityEngine.CoreModule.dll","UnityEngine","Object","Internal_InstantiateSingle",
        "UnityEngine.Object|UnityEngine.Vector3|UnityEngine.Quaternion","UnityEngine.Object",3},false},
    {"clone.instantiate_with_parent", {"UnityEngine.CoreModule.dll","UnityEngine","Object","Internal_InstantiateSingleWithParent",
        "UnityEngine.Object|UnityEngine.Transform|UnityEngine.Vector3|UnityEngine.Quaternion","UnityEngine.Object",4},false},
    // Optional upload-order hint: the visible receiver nearest the main camera.
    {"camera.get_main", {"UnityEngine.CoreModule.dll","UnityEngine","Camera","get_main",nullptr,"UnityEngine.Camera",0},false},
    {"pump.canvas_will_render",
        {"UnityEngine.UIModule.dll", "UnityEngine", "Canvas",
            "SendWillRenderCanvases", nullptr, "System.Void", 0}, true},
    {"object.get_name",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Object", "get_name",
            nullptr, "System.String", 0}, true},
    {"object.set_name",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Object", "set_name",
            "System.String", "System.Void", 1}, true},
    {"object.destroy",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Object", "Destroy",
            "UnityEngine.Object", "System.Void", 1}, true},
    {"object.is_alive",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Object", "op_Implicit",
            "UnityEngine.Object", "System.Boolean", 1}, true},
    {"array.get_length",
        {"mscorlib.dll", "System", "Array", "GetLength",
            "System.Int32", "System.Int32", 1}, true},
    {"array.get_value",
        {"mscorlib.dll", "System", "Array", "GetValue",
            "System.Int32", "System.Object", 1}, true},
    {"component.get_transform",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Component",
            "get_transform", nullptr, "UnityEngine.Transform", 0}, true},
    {"transform.get_parent",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Transform",
            "get_parent", nullptr, "UnityEngine.Transform", 0}, true},
    {"transform.child_count", {"UnityEngine.CoreModule.dll","UnityEngine","Transform",
        "get_childCount",nullptr,"System.Int32",0},false},
    {"transform.get_child", {"UnityEngine.CoreModule.dll","UnityEngine","Transform",
        "GetChild","System.Int32","UnityEngine.Transform",1},false},
    {"renderer.get_enabled",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Renderer",
            "get_enabled", nullptr, "System.Boolean", 0}, true},
    {"renderer.is_visible", {"UnityEngine.CoreModule.dll","UnityEngine","Renderer","get_isVisible",nullptr,"System.Boolean",0},false},
    // Optional: one-shot discovery of scene instances after a hot switch.
    {"resources.find_all", {"UnityEngine.CoreModule.dll","UnityEngine","Resources","FindObjectsOfTypeAll","System.Type","UnityEngine.Object[]",1},false},
    {"component.get_game_object", {"UnityEngine.CoreModule.dll","UnityEngine","Component","get_gameObject",nullptr,"UnityEngine.GameObject",0},false},
    {"renderer.set_enabled",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Renderer",
            "set_enabled", "System.Boolean", "System.Void", 1}, true},
#if defined(__ANDROID__)
    // Read-only issue #25 diagnostics. Missing/stripped getters never prevent
    // model publication; these are separate from the existing LOD policy.
    {"android.streaming_active", {"UnityEngine.CoreModule.dll","UnityEngine","QualitySettings","get_streamingMipmapsActive",nullptr,"System.Boolean",0},false},
    {"android.streaming_budget", {"UnityEngine.CoreModule.dll","UnityEngine","QualitySettings","get_streamingMipmapsMemoryBudget",nullptr,"System.Single",0},false},
    {"android.streaming_budget_set", {"UnityEngine.CoreModule.dll","UnityEngine","QualitySettings","set_streamingMipmapsMemoryBudget","System.Single","System.Void",1},false},
    {"android.streaming_max_reduction", {"UnityEngine.CoreModule.dll","UnityEngine","QualitySettings","get_streamingMipmapsMaxLevelReduction",nullptr,"System.Int32",0},false},
    {"android.global_mip_limit", {"UnityEngine.CoreModule.dll","UnityEngine","QualitySettings","get_globalTextureMipmapLimit",nullptr,"System.Int32",0},false},
    {"android.master_texture_limit", {"UnityEngine.CoreModule.dll","UnityEngine","QualitySettings","get_masterTextureLimit",nullptr,"System.Int32",0},false},
    {"android.quality_max_lod", {"UnityEngine.CoreModule.dll","UnityEngine","QualitySettings","get_maximumLODLevel",nullptr,"System.Int32",0},false},
    {"android.quality_lod_offset", {"UnityEngine.CoreModule.dll","UnityEngine","QualitySettings","get_lodOffset",nullptr,"System.Int32",0},false},
    {"android.lod_streaming_active", {"UnityEngine.HGGraphicsModule.dll","UnityEngine.HyperGryph","HGLODStreamingSystem","get_enableLODStreaming",nullptr,"System.Boolean",0},false},
    {"android.lod_streaming_status", {"UnityEngine.HGGraphicsModule.dll","UnityEngine.HyperGryph","HGLODStreamingSystem","QueryLODStreamingStatus",nullptr,"UnityEngine.HyperGryph.LODStreamingStatus",0},false},
    {"android.lod_streaming_offset", {"UnityEngine.HGGraphicsModule.dll","UnityEngine.HyperGryph","HGLODStreamingSystem","SetArtTagLODStreamingOffset","System.UInt32|System.Int32","System.Void",2},false},
    {"android.texture_current", {"UnityEngine.CoreModule.dll","UnityEngine","Texture","get_currentTextureMemory",nullptr,"System.UInt64",0},false},
    {"android.texture_nonstreaming", {"UnityEngine.CoreModule.dll","UnityEngine","Texture","get_nonStreamingTextureMemory",nullptr,"System.UInt64",0},false},
    {"android.texture_desired", {"UnityEngine.CoreModule.dll","UnityEngine","Texture","get_desiredTextureMemory",nullptr,"System.UInt64",0},false},
    {"android.texture_target", {"UnityEngine.CoreModule.dll","UnityEngine","Texture","get_targetTextureMemory",nullptr,"System.UInt64",0},false},
    {"android.texture_pending", {"UnityEngine.CoreModule.dll","UnityEngine","Texture","get_streamingTexturePendingLoadCount",nullptr,"System.UInt64",0},false},
    {"android.texture_loading", {"UnityEngine.CoreModule.dll","UnityEngine","Texture","get_streamingTextureLoadingCount",nullptr,"System.UInt64",0},false},
    {"android.texture_streaming_count", {"UnityEngine.CoreModule.dll","UnityEngine","Texture","get_streamingTextureCount",nullptr,"System.UInt64",0},false},
    {"android.texture_nonstreaming_count", {"UnityEngine.CoreModule.dll","UnityEngine","Texture","get_nonStreamingTextureCount",nullptr,"System.UInt64",0},false},
    {"android.texture_is_streaming", {"UnityEngine.CoreModule.dll","UnityEngine","Texture2D","get_streamingMipmaps",nullptr,"System.Boolean",0},false},
    {"android.texture_loaded_mip", {"UnityEngine.CoreModule.dll","UnityEngine","Texture2D","get_loadedMipmapLevel",nullptr,"System.Int32",0},false},
    {"android.texture_desired_mip", {"UnityEngine.CoreModule.dll","UnityEngine","Texture2D","get_desiredMipmapLevel",nullptr,"System.Int32",0},false},
    {"android.texture_requested_mip", {"UnityEngine.CoreModule.dll","UnityEngine","Texture2D","get_requestedMipmapLevel",nullptr,"System.Int32",0},false},
    {"android.shadow_get", {"UnityEngine.CoreModule.dll","UnityEngine","Renderer","get_shadowCastingMode",nullptr,"UnityEngine.Rendering.ShadowCastingMode",0},true},
    {"android.all_renderers", {"UnityEngine.CoreModule.dll","UnityEngine","Resources","FindObjectsOfTypeAll","System.Type","UnityEngine.Object[]",1},true},
    {"android.renderer_visible", {"UnityEngine.CoreModule.dll","UnityEngine","Renderer","get_isVisible",nullptr,"System.Boolean",0},true},
    {"android.shadow_set", {"UnityEngine.CoreModule.dll","UnityEngine","Renderer","set_shadowCastingMode","UnityEngine.Rendering.ShadowCastingMode","System.Void",1},true},
    {"android.shadow_mesh_get", {"UnityEngine.CoreModule.dll","UnityEngine","Renderer","get_shadowProxyMesh",nullptr,"UnityEngine.Mesh",0},true},
    {"android.shadow_mesh_set", {"UnityEngine.CoreModule.dll","UnityEngine","Renderer","set_shadowProxyMesh","UnityEngine.Mesh","System.Void",1},true},
#endif
    {"skinned.get_shared_mesh",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer",
            "get_sharedMesh", nullptr, "UnityEngine.Mesh", 0}, true},
    {"component.get_component", {"UnityEngine.CoreModule.dll","UnityEngine","Component",
        "GetComponent","System.Type","UnityEngine.Component",1},true},
    {"filter.get_shared_mesh", {"UnityEngine.CoreModule.dll","UnityEngine","MeshFilter",
        "get_sharedMesh",nullptr,"UnityEngine.Mesh",0},true},
    {"filter.set_shared_mesh", {"UnityEngine.CoreModule.dll","UnityEngine","MeshFilter",
        "set_sharedMesh","UnityEngine.Mesh","System.Void",1},true},
    {"skinned.set_shared_mesh",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer",
            "set_sharedMesh", "UnityEngine.Mesh", "System.Void", 1}, true},
    {"mesh.add_blend_shape_frame",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh",
            "AddBlendShapeFrame", nullptr, "System.Void", 5}, false},
    {"mesh.get_blend_shape_index",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh",
            "GetBlendShapeIndex", nullptr, "System.Int32", 1}, false},
    {"mesh.get_blend_shape_count",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh",
            "get_blendShapeCount", nullptr, "System.Int32", 0}, false},
    {"mesh.get_blend_shape_name",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh",
            "GetBlendShapeName", nullptr, "System.String", 1}, false},
    {"skinned.set_blend_shape_weight",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer",
            "SetBlendShapeWeight", nullptr, "System.Void", 2}, false},
    {"skinned.get_blend_shape_weight",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer",
            "GetBlendShapeWeight", nullptr, "System.Single", 1}, false},
    {"skinned.get_bones",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer",
            "get_bones", nullptr, "UnityEngine.Transform[]", 0}, true},
    {"skinned.get_root_bone",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer",
            "get_rootBone", nullptr, "UnityEngine.Transform", 0}, false},
    {"mesh.ctor",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", ".ctor",
            nullptr, "System.Void", 0}, true},
    {"mesh.get_vertex_count",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "get_vertexCount",
            nullptr, "System.Int32", 0}, true},
    {"mesh.get_sub_mesh_count",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "get_subMeshCount",
            nullptr, "System.Int32", 0}, true},
    {"mesh.get_index_count",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "GetIndexCount",
            "System.Int32", "System.UInt32", 1}, true},
    {"mesh.get_bindposes",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "get_bindposes",
            nullptr, "UnityEngine.Matrix4x4[]", 0}, true},
    {"mesh.has_bone_weights",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "HasBoneWeights",
            nullptr, "System.Boolean", 0}, true},
    {"mesh.get_vertex_attribute_count",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh",
            "get_vertexAttributeCount", nullptr, "System.Int32", 0}, true},
    {"mesh.get_vertex_attribute",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "GetVertexAttribute",
            "System.Int32",
            "UnityEngine.Rendering.VertexAttributeDescriptor", 1}, true},
    {"renderer.get_shared_materials",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Renderer",
            "get_sharedMaterials", nullptr, "UnityEngine.Material[]", 0}, true},
    {"renderer.set_shared_materials",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Renderer",
            "set_sharedMaterials", "UnityEngine.Material[]", "System.Void", 1}, true},
    {"renderer.get_enabled",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Renderer",
            "get_enabled", nullptr, "System.Boolean", 0}, true},
    {"renderer.set_enabled",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Renderer",
            "set_enabled", "System.Boolean", "System.Void", 1}, true},
    {"material.get_texture_property_ids",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Material",
            "GetTexturePropertyNameIDs", nullptr, "System.Int32[]", 0}, true},
    {"array.clone",
        {"mscorlib.dll", "System", "Array", "Clone", nullptr, "System.Object", 0}, true},
    {"material.get_texture_by_id",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Material", "GetTexture",
            "System.Int32", "UnityEngine.Texture", 1}, true},
    {"texture.get_width",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Texture", "get_width",
            nullptr, "System.Int32", 0}, true},
#ifdef __ANDROID__
    {"android.texture_supported", {"UnityEngine.CoreModule.dll","UnityEngine","SystemInfo",
        "SupportsTextureFormat","UnityEngine.TextureFormat","System.Boolean",1},true},
#endif
    // Optional: with multithreaded rendering this synchronizes with the render
    // thread, so a large upload's readable CPU copy is consumed before the next.
    {"texture.get_native_texture_ptr",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Texture",
            "GetNativeTexturePtr", nullptr, "System.IntPtr", 0}, false},
    {"texture.get_height",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Texture", "get_height",
            nullptr, "System.Int32", 0}, true},
    {"texture.get_graphics_format",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Texture",
            "get_graphicsFormat", nullptr, nullptr, 0}, true},
    {"texture2d.ctor",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Texture2D", ".ctor",
            "System.Int32|System.Int32|UnityEngine.TextureFormat|"
            "System.Int32|System.Boolean",
            "System.Void", 5}, true},
    {"texture2d.load_raw_texture_data",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Texture2D",
            "LoadRawTextureData", "System.IntPtr|System.Int32",
            "System.Void", 2}, true},
    {"texture2d.apply",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Texture2D", "Apply",
            "System.Boolean|System.Boolean", "System.Void", 2}, true},
    {"material.set_texture_by_id",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Material", "SetTexture",
            "System.Int32|UnityEngine.Texture", "System.Void", 2}, true},
    {"graphics_format_utility.get_graphics_format",
        {"UnityEngine.CoreModule.dll", "UnityEngine.Experimental.Rendering",
            "GraphicsFormatUtility", "GetGraphicsFormat",
            "UnityEngine.TextureFormat|System.Boolean", nullptr, 2}, true},
    {"texture.get_wrap_mode",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Texture",
            "get_wrapMode", nullptr, nullptr, 0}, true},
    {"texture.get_wrap_u", {"UnityEngine.CoreModule.dll","UnityEngine","Texture","get_wrapModeU",nullptr,nullptr,0},false},
    {"texture.get_wrap_v", {"UnityEngine.CoreModule.dll","UnityEngine","Texture","get_wrapModeV",nullptr,nullptr,0},false},
    {"texture.get_wrap_w", {"UnityEngine.CoreModule.dll","UnityEngine","Texture","get_wrapModeW",nullptr,nullptr,0},false},
    {"texture.set_wrap_u", {"UnityEngine.CoreModule.dll","UnityEngine","Texture","set_wrapModeU",nullptr,"System.Void",1},false},
    {"texture.set_wrap_v", {"UnityEngine.CoreModule.dll","UnityEngine","Texture","set_wrapModeV",nullptr,"System.Void",1},false},
    {"texture.set_wrap_w", {"UnityEngine.CoreModule.dll","UnityEngine","Texture","set_wrapModeW",nullptr,"System.Void",1},false},
    {"texture.set_wrap_mode",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Texture",
            "set_wrapMode", nullptr, "System.Void", 1}, true},
    {"texture.get_filter_mode",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Texture",
            "get_filterMode", nullptr, nullptr, 0}, true},
    {"texture.set_filter_mode",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Texture",
            "set_filterMode", nullptr, "System.Void", 1}, true},
    {"texture.get_aniso_level",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Texture",
            "get_anisoLevel", nullptr, "System.Int32", 0}, true},
    {"texture.set_aniso_level",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Texture",
            "set_anisoLevel", "System.Int32", "System.Void", 1}, true},
    {"texture.get_mip_map_bias",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Texture",
            "get_mipMapBias", nullptr, "System.Single", 0}, true},
    {"texture.set_mip_map_bias",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Texture",
            "set_mipMapBias", "System.Single", "System.Void", 1}, true},
    {"mesh.set_bindposes",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "set_bindposes",
            "UnityEngine.Matrix4x4[]", "System.Void", 1}, true},
    {"mesh.set_sub_mesh_count",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "set_subMeshCount",
            "System.Int32", "System.Void", 1}, true},
    // Optional: invalidates Unity's cached mesh/skinning copy after the driver
    // rewrites vertex positions. A SkinnedMeshRenderer keeps its own uploaded
    // vertex copy, so writing the Mesh alone leaves the renderer drawing the old
    // pose (hazy prop plus a solid copy left at the previous position).
    {"mesh.mark_modified",
        {"UnityEngine.CoreModule.dll","UnityEngine","Mesh",
            "MarkModified",nullptr,"System.Void",0},false},
    {"skinned.mark_modified",
        {"UnityEngine.CoreModule.dll","UnityEngine","SkinnedMeshRenderer",
            "MarkModified",nullptr,"System.Void",0},false},
    {"mesh.recalculate_bounds",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "RecalculateBounds",
            nullptr, "System.Void", 0}, true},
    {"pipeline.current",
        {"UnityEngine.CoreModule.dll", "UnityEngine.Rendering", "RenderPipelineManager",
            "get_currentPipeline", nullptr, nullptr, 0}, true},
    {"pipeline.enable_force_lod0",
        {"HG.RenderPipelines.Runtime.dll", "HG.Rendering.Runtime", "HGRenderPipeline",
            "EnableForceLOD0", nullptr, nullptr, 0}, true},
    {"pipeline.disable_force_lod0",
        {"HG.RenderPipelines.Runtime.dll", "HG.Rendering.Runtime", "HGRenderPipeline",
            "DisableForceLOD0", nullptr, nullptr, 0}, true},
#if defined(__ANDROID__)
    {"pipeline.register_bias", {"HG.RenderPipelines.Runtime.dll","HG.Rendering.Runtime","HGRenderPipeline",
        "RegisterArtTagLODBias",nullptr,"System.Void",0},true},
#endif
    {"culling.set_parent_lod_bias",
        {"UnityEngine.CoreModule.dll", "UnityEngine.HyperGryph", "HGCullingSystem",
            "set_parentLODBias", "System.Single", "System.Void", 1}, true},
    {"culling.set_art_tag_lod_bias",
        {"UnityEngine.CoreModule.dll", "UnityEngine.HyperGryph", "HGCullingSystem",
            "SetArtTagLODBias", "System.UInt32|System.Single", "System.Void", 2}, true},
    {"quality.set_max_lod",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "QualitySettings",
            "set_maximumLODLevel", "System.Int32", "System.Void", 1}, true},
    {"array.set_value", {"mscorlib.dll","System","Array","SetValue","System.Object|System.Int32","System.Void",2},true},
    {"skinned.set_bones", {"UnityEngine.CoreModule.dll","UnityEngine","SkinnedMeshRenderer","set_bones","UnityEngine.Transform[]","System.Void",1},false},
    {"transform.local_to_world", {"UnityEngine.CoreModule.dll","UnityEngine","Transform","get_localToWorldMatrix",nullptr,"UnityEngine.Matrix4x4",0},false},
    {"transform.world_to_local", {"UnityEngine.CoreModule.dll","UnityEngine","Transform","get_worldToLocalMatrix",nullptr,"UnityEngine.Matrix4x4",0},false},
    {"probe.material_shader", {"UnityEngine.CoreModule.dll","UnityEngine","Material","get_shader",nullptr,"UnityEngine.Shader",0},false},
    {"material.set_float", {"UnityEngine.CoreModule.dll","UnityEngine","Material","SetFloat","System.String|System.Single","System.Void",2},false},
    {"material.set_color", {"UnityEngine.CoreModule.dll","UnityEngine","Material","SetColor","System.String|UnityEngine.Color","System.Void",2},false},
    {"material.set_vector", {"UnityEngine.CoreModule.dll","UnityEngine","Material","SetVector","System.String|UnityEngine.Vector4","System.Void",2},false},
    {"material.disable_keyword", {"UnityEngine.CoreModule.dll","UnityEngine","Material","DisableKeyword","System.String","System.Void",1},false},
    {"material.enable_keyword", {"UnityEngine.CoreModule.dll","UnityEngine","Material","EnableKeyword","System.String","System.Void",1},false},
    {"cache.has_property", {"UnityEngine.CoreModule.dll","UnityEngine","Material","HasProperty","System.Int32","System.Boolean",1},false},
    {"cache.get_float", {"UnityEngine.CoreModule.dll","UnityEngine","Material","GetFloat","System.Int32","System.Single",1},false},
    {"cache.set_float", {"UnityEngine.CoreModule.dll","UnityEngine","Material","SetFloat","System.Int32|System.Single","System.Void",2},false},
    {"cache.get_vector", {"UnityEngine.CoreModule.dll","UnityEngine","Material","GetVector","System.Int32","UnityEngine.Vector4",1},false},
    {"cache.get_color", {"UnityEngine.CoreModule.dll","UnityEngine","Material","GetColorImpl","System.Int32","UnityEngine.Color",1},false},
    {"cache.set_vector", {"UnityEngine.CoreModule.dll","UnityEngine","Material","SetVector","System.Int32|UnityEngine.Vector4","System.Void",2},false},
    {"cache.keyword_enabled", {"UnityEngine.CoreModule.dll","UnityEngine","Material","IsKeywordEnabled","System.String","System.Boolean",1},false},
    {"cache.get_pass", {"UnityEngine.CoreModule.dll","UnityEngine","Material","GetShaderPassEnabledAndExisted","System.String","System.Boolean",1},false},
    {"cache.set_pass", {"UnityEngine.CoreModule.dll","UnityEngine","Material","SetShaderPassEnabled","System.String|System.Boolean","System.Void",2},false},
    {"cache.property_id", {"UnityEngine.CoreModule.dll","UnityEngine","Shader","PropertyToID","System.String","System.Int32",1},false},
    {"visual.get_local_position", {"UnityEngine.CoreModule.dll","UnityEngine","Transform","get_localPosition",nullptr,"UnityEngine.Vector3",0},false},
    {"visual.set_local_position", {"UnityEngine.CoreModule.dll","UnityEngine","Transform","set_localPosition","UnityEngine.Vector3","System.Void",1},false},
    {"material.copy", {"UnityEngine.CoreModule.dll","UnityEngine","Material",".ctor","UnityEngine.Material","System.Void",1},true},
    {"material.get_shader", {"UnityEngine.CoreModule.dll","UnityEngine","Material","get_shader",nullptr,"UnityEngine.Shader",0},true},
    {"object.instance_id", {"UnityEngine.CoreModule.dll","UnityEngine","Object","GetInstanceID",nullptr,"System.Int32",0},true},
    // Hot switch only: DontUnloadUnusedAsset on a pinned Original Mesh.
    {"object.get_hide_flags", {"UnityEngine.CoreModule.dll","UnityEngine","Object","get_hideFlags",nullptr,"UnityEngine.HideFlags",0},false},
    {"object.set_hide_flags", {"UnityEngine.CoreModule.dll","UnityEngine","Object","set_hideFlags","UnityEngine.HideFlags","System.Void",1},false},
    {"resource.finish", {"Common.Beyond.dll","Beyond.Resource.Runtime","BundleLoader.AssetProxy","_FinishWithAsset","UnityEngine.Object","System.Void",1},true},
    {"game_object.renderers", {"UnityEngine.CoreModule.dll","UnityEngine","GameObject","GetComponentsInChildren","System.Type|System.Boolean","UnityEngine.Component[]",2},true},
    {"quality.get_max_lod", {"UnityEngine.CoreModule.dll","UnityEngine","QualitySettings","get_maximumLODLevel",nullptr,"System.Int32",0},true},
};
MethodContract* Contract(std::string_view key) {
    for (auto& method : g_methods) {
        if (method.key == key) return &method;
    }
    return nullptr;
}
void Log(const std::string& message) {
    if (g_host && g_host->log) {
        g_host->log(g_host->context, kModuleId, message.c_str());
    }
}

void DestroyUnityObject(void* object);
struct ConstructionScope;
thread_local ConstructionScope* g_construction = nullptr;
struct ConstructionScope {
    std::vector<std::pair<void*,uint32_t>> roots;
    std::unordered_set<void*> rooted_objects;
    std::vector<void*> assets;
    // Count API submissions, including assets later rolled back. These bytes
    // are payload sizes, not driver residency or a GPU peak measurement.
    std::string resource_name;
    uint64_t started_ms = GetTickCount64();
    uint64_t texture_constructed = 0, texture_submitted = 0;
    // Per-transaction texture census: (format, srgb) -> count. Replaces one
    // detail line per built texture; the transaction summary prints the census
    // in a single line instead.
    std::map<std::pair<int32_t,int32_t>,uint32_t> texture_formats;
    // Counts built textures separately from texture_constructed: the ctor
    // succeeds earlier than the upload, so a texture that fails in between
    // would otherwise consume the "first of this transaction" detail line.
    uint64_t texture_detailed = 0;
    uint64_t texture_payload_bytes = 0, largest_texture_payload_bytes = 0;
    uint64_t render_syncs = 0;
    uint64_t mesh_submitted = 0, mesh_payload_bytes = 0;
    // Peak diagnostics (2026-10-03). References = material texture bindings
    // requested; dedup = served by a texture built earlier in this transaction;
    // live = served by a texture still bound to a published receiver. Decoded
    // bytes are per-texture BEM payloads held by this module (CPU), not GPU.
    const char* delivery_mode = "synchronous";
    uint64_t texture_references = 0, texture_dedup_hits = 0, texture_dedup_bytes = 0;
    uint64_t texture_live_reuse = 0, texture_live_reuse_bytes = 0;
    uint64_t decoded_live_bytes = 0, decoded_peak_bytes = 0;
    uint64_t upload_frame = UINT64_MAX, frame_upload_bytes = 0, max_frame_upload_bytes = 0, upload_frames = 0;
    uint64_t current_frame = 0; // synchronous transactions run inside one frame
    // All frame-sliced Jobs together (shared budget): bytes submitted in one
    // observed frame; the pump status line reports and resets the maximum.
    static inline uint64_t global_frame = UINT64_MAX, global_frame_bytes = 0, global_max_frame_bytes = 0;
    void RecordUpload(uint64_t bytes) {
        if (upload_frame != current_frame) { upload_frame = current_frame; frame_upload_bytes = 0; ++upload_frames; }
        frame_upload_bytes += bytes; max_frame_upload_bytes = std::max(max_frame_upload_bytes, frame_upload_bytes);
        if (std::string_view(delivery_mode) == "frame-sliced") {
            if (global_frame != current_frame) { global_frame = current_frame; global_frame_bytes = 0; }
            global_frame_bytes += bytes; global_max_frame_bytes = std::max(global_max_frame_bytes, global_frame_bytes);
        }
    }
    void HoldDecoded(uint64_t bytes) { decoded_live_bytes += bytes; decoded_peak_bytes = std::max(decoded_peak_bytes, decoded_live_bytes); }
    void ReleaseDecoded(uint64_t bytes) { decoded_live_bytes -= std::min(decoded_live_bytes, bytes); }
    ConstructionScope* previous = g_construction;
    bool failed = false;
    bool published = false;
    bool attached=false;
    explicit ConstructionScope(bool attach=true) : attached(attach) { if (attach) g_construction = this; }
    ConstructionScope(const ConstructionScope&) = delete;
    ~ConstructionScope();
    void* Root(void* object) {
        if (!object) return nullptr;
        // Queries may return tens of thousands of objects in one scope. Keep
        // the handle release list, but avoid scanning it for every return.
        if (rooted_objects.contains(object)) return object;
        roots.emplace_back(object,0);
        roots.back().second = g_host->gchandle_new(g_host->context,object,0);
        if (!roots.back().second) { failed = true; return nullptr; }
        rooted_objects.insert(object);
        return object;
    }
};
// A Job owns its scope across frames. Only the pump temporarily installs it.
struct ConstructionActivation {
    ConstructionScope* previous=g_construction;
    explicit ConstructionActivation(ConstructionScope& scope) { g_construction=&scope; }
    ~ConstructionActivation() { g_construction=previous; }
};
void* RootTemporary(void* object) { return g_construction ? g_construction->Root(object) : object; }
void* NewAsset(const void* type) {
    if (!g_construction || !type) return nullptr;
    void* object = RootTemporary(g_host->object_new(g_host->context,type));
    if (object) g_construction->assets.push_back(object);
    return object;
}
void* NewString(const char* value) { return RootTemporary(g_host->string_new(g_host->context,value)); }
size_t MappedImageSize(HMODULE module) {
    if (!module) return 0;
#if defined(__ANDROID__)
    (void)module;
    return 0;
#else
    __try {
        auto* base = reinterpret_cast<const uint8_t*>(module);
        auto* dos = reinterpret_cast<const IMAGE_DOS_HEADER*>(base);
        if (dos->e_magic != IMAGE_DOS_SIGNATURE || dos->e_lfanew <= 0) return 0;
        auto* nt = reinterpret_cast<const IMAGE_NT_HEADERS64*>(base + dos->e_lfanew);
        return nt->Signature == IMAGE_NT_SIGNATURE ? nt->OptionalHeader.SizeOfImage : 0;
    } __except(EXCEPTION_EXECUTE_HANDLER) { return 0; }
#endif
}
void* Invoke(const MethodContract* method, void* instance, void** parameters,
    bool log_exception = true) {
    if (!method || !method->method_info || !g_host || !g_host->runtime_invoke) {
        return nullptr;
    }
    void* exception = nullptr;
    void* result = g_host->runtime_invoke(
        g_host->context, method->method_info, instance, parameters, &exception);
    if (exception && log_exception) {
        Log(std::string("Managed exception from ") + method->key);
    }
    return exception ? nullptr : RootTemporary(result);
}
bool ReadNullableObject(const MethodContract* method,void* instance,void*& value,void** parameters=nullptr) {
    value=nullptr;
    if (!method || !method->resolved || !method->method_info || !g_host || !g_host->runtime_invoke) return false;
    void* exception=nullptr;void* result=g_host->runtime_invoke(g_host->context,method->method_info,instance,parameters,&exception);
    if (exception) return false;
    value=RootTemporary(result);return !result || value;
}

template <typename T>
bool Unbox(void* boxed, T& value) {
    if (!boxed || !g_host || !g_host->object_unbox) return false;
    void* raw = g_host->object_unbox(g_host->context, boxed);
    if (!raw) return false;
    std::memcpy(&value, raw, sizeof(T));
    return true;
}

template <typename T>
bool InvokeValue(const MethodContract* method, void* instance, void** parameters,
    T& value) {
    return Unbox(Invoke(method, instance, parameters), value);
}

bool InvokeVoid(const MethodContract* method, void* instance, void** parameters) {
    if (!method || !method->resolved || !g_host || !g_host->runtime_invoke) {
        return false;
    }
    void* exception = nullptr;
    g_host->runtime_invoke(
        g_host->context, method->method_info, instance, parameters, &exception);
    if (exception) {
        Log(std::string("Managed exception from ") + method->key);
        return false;
    }
    return true;
}

std::string ManagedString(void* value) {
    if (!value || !g_host || !g_host->copy_managed_string) return {};
    std::array<char, 1024> buffer{};
    const int copied = g_host->copy_managed_string(
        g_host->context, value, buffer.data(), buffer.size());
    return copied > 0 ? std::string(buffer.data(), static_cast<size_t>(copied))
                      : std::string{};
}

std::string ObjectName(void* object) {
    return object
        ? ManagedString(Invoke(Contract("object.get_name"), object, nullptr, false))
        : std::string{};
}


// ENGINE_HELPERS
int ArrayLength(void* array) {
    int dimension = 0;
    int length = 0;
    void* parameters[1]{&dimension};
    return array &&
            InvokeValue(Contract("array.get_length"), array, parameters, length)
        ? length
        : 0;
}

void* ArrayValue(void* array, int index) {
    void* parameters[1]{&index};
    return array
        ? Invoke(Contract("array.get_value"), array, parameters, false)
        : nullptr;
}

std::string BuildTransformPath(void* component) {
    std::vector<std::string> names;
    void* transform = Invoke(Contract("component.get_transform"), component, nullptr, false);
    for (int depth = 0; transform && depth < 24; ++depth) {
        std::string name = ObjectName(transform);
        names.push_back(name.empty() ? "<unnamed>" : std::move(name));
        transform = Invoke(Contract("transform.get_parent"), transform, nullptr, false);
    }
    std::reverse(names.begin(), names.end());

    std::string path;
    for (const auto& name : names) {
        if (!path.empty()) path.push_back('/');
        path += name;
    }
    return path;
}

std::filesystem::path Utf8Path(std::string_view value) {
    if (value.empty()) return {};
    const int wide_count = MultiByteToWideChar(
        CP_UTF8, MB_ERR_INVALID_CHARS, value.data(),
        static_cast<int>(value.size()), nullptr, 0);
    if (wide_count <= 0) return std::filesystem::path(std::string(value));
    std::wstring wide(static_cast<size_t>(wide_count), L'\0');
    MultiByteToWideChar(
        CP_UTF8, MB_ERR_INVALID_CHARS, value.data(),
        static_cast<int>(value.size()), wide.data(), wide_count);
    return std::filesystem::path(wide);
}

bool IsNativeObjectAlive(void* object) {
    uint8_t alive = 0;
    void* parameters[1]{object};
    return object && InvokeValue(Contract("object.is_alive"), nullptr,
        parameters, alive) && alive != 0;
}

// AssetProxy delivers arbitrary UnityEngine.Objects. Weapons in particular
// share their prefab name with UI Sprites; invoking a GameObject instance
// method on that Sprite can fault inside Unity before a managed exception is
// raised. GameObject is sealed, so an exact IL2CPP class check proves this
// boundary for both prefabs and clones. Missing type contracts fail closed.
bool IsGameObjectResource(void* asset) {
    return asset && g_object_class && g_game_object_class.class_info &&
        g_object_class(asset)==g_game_object_class.class_info;
}

bool RendererMaterialsMatch(void* renderer, void* expected_materials) {
    if (!renderer || !expected_materials) return false;
    void* current_materials = Invoke(Contract("renderer.get_shared_materials"), renderer, nullptr, false);
    if (!current_materials) return false;
    const int count = ArrayLength(expected_materials);
    if (count != ArrayLength(current_materials)) return false;
    for (int i = 0; i < count; ++i) {
        if (ArrayValue(current_materials, i) != ArrayValue(expected_materials, i)) {
            return false;
        }
    }
    return true;
}

bool ApplyRendererMaterials(void* renderer, void* materials) {
    void* args[]{materials};
    return InvokeVoid(Contract("renderer.set_shared_materials"),renderer,args) && RendererMaterialsMatch(renderer,materials);
}
bool IsStaticRenderer(void* renderer) {
    return renderer && g_object_class && g_static_renderer_class.class_info &&
        g_object_class(renderer)==g_static_renderer_class.class_info;
}
bool IsModelRenderer(void* renderer);
void* RendererMeshHolder(void* renderer) {
    if (!IsModelRenderer(renderer)) return nullptr;
    if (!IsStaticRenderer(renderer)) return renderer;
    void* args[]{g_mesh_filter_class.type_object};
    void* filter=Invoke(Contract("component.get_component"),renderer,args);
    return filter && IsNativeObjectAlive(filter)?filter:nullptr;
}
void* GetRendererMesh(void* renderer) {
    void* holder=RendererMeshHolder(renderer);
    return holder?Invoke(Contract(IsStaticRenderer(renderer)?"filter.get_shared_mesh":"skinned.get_shared_mesh"),holder,nullptr):nullptr;
}
void* GetRendererBones(void* renderer) {
    return !IsModelRenderer(renderer) || IsStaticRenderer(renderer)?nullptr:Invoke(Contract("skinned.get_bones"),renderer,nullptr);
}
// Query only renderer types with a supported Mesh owner. Exact resource/path
// matching remains mandatory; discovering a nested weapon never authorizes it.
void* ModelRendererType() { return g_renderer_class.type_object?g_renderer_class.type_object:g_skinned_renderer_class.type_object; }
bool IsModelRenderer(void* renderer) {
    if (!renderer) return false;
    if (!g_renderer_class.type_object || !g_object_class) return true;
    const auto type=g_object_class(renderer);
    return type==g_skinned_renderer_class.class_info || type==g_static_renderer_class.class_info;
}
bool SetSharedMesh(void* renderer, void* mesh) {
    void* holder=RendererMeshHolder(renderer);
    void* args[]{mesh};
    return holder && InvokeVoid(Contract(IsStaticRenderer(renderer)?"filter.set_shared_mesh":"skinned.set_shared_mesh"),holder,args) &&
        GetRendererMesh(renderer)==mesh;
}

constexpr int32_t kVertexPosition = 0;
constexpr int32_t kVertexNormal = 1;
constexpr int32_t kVertexColor = 3;
constexpr int32_t kVertexTexCoord0 = 4;
constexpr int32_t kVertexBlendWeight = 12;
constexpr int32_t kVertexBlendIndices = 13;

// The packed character bytes EFMI reports as source semantic TEXCOORD4 land on
// UnityEngine.Rendering.VertexAttribute.TexCoord2 in the live client. The EFMI
// D3D semantic index and Unity's VertexAttribute index are separate numbering
// domains and must not be assumed equal: enumerating the live C9 declaration
// shows this channel as TexCoord2(6) SNorm8 x4 on stream 1, immediately after
// TexCoord0 Float32 x2 on the same stream. That matches the EFMI VB1 12-byte
// stride the converter reads, so only the destination channel differs.
constexpr int32_t kVertexPackedCharData = 6;

// UnityEngine.Rendering.VertexAttributeFormat values.
constexpr int32_t kFormatFloat32 = 0;
constexpr int32_t kFormatSNorm8 = 3;
constexpr int32_t kFormatUNorm16 = 4;
constexpr int32_t kFormatUInt8 = 6;

struct VertexChannelInfo {
    bool present = false;
    int32_t format = -1;
    int32_t dimension = -1;
    int32_t stream = -1;
};

// Unboxed payload of UnityEngine.Rendering.VertexAttributeDescriptor. The
// managed struct stores four Int32 backing fields in declaration order, so the
// boxed value the runtime hands back is exactly these 16 bytes.
struct VertexAttributeDescriptorRaw {
    int32_t attribute;
    int32_t format;
    int32_t dimension;
    int32_t stream;
};
static_assert(sizeof(VertexAttributeDescriptorRaw) == 16);

// A Mesh cannot declare more channels than the VertexAttribute enum has
// members. The bound only keeps a nonsense count from driving the read loop.
constexpr int32_t kMaxVertexAttributes = 14;

struct VertexDeclaration {
    std::array<VertexAttributeDescriptorRaw, kMaxVertexAttributes> entries{};
    int32_t count = 0;

    bool Find(int32_t attribute, VertexChannelInfo& info) const {
        info = {};
        for (int32_t i = 0; i < count; ++i) {
            if (entries[i].attribute != attribute) continue;
            info.present = true;
            info.format = entries[i].format;
            info.dimension = entries[i].dimension;
            info.stream = entries[i].stream;
            return true;
        }
        return false;
    }
};

const char* VertexFormatName(int32_t format) {
    switch (format) {
    case kFormatFloat32: return "Float32";
    case 1: return "Float16";
    case 2: return "UNorm8";
    case kFormatSNorm8: return "SNorm8";
    case kFormatUNorm16: return "UNorm16";
    case 5: return "SNorm16";
    case kFormatUInt8: return "UInt8";
    case 7: return "SInt8";
    case 8: return "UInt16";
    case 9: return "SInt16";
    case 10: return "UInt32";
    case 11: return "SInt32";
    default: return "other";
    }
}

const char* VertexAttributeName(int32_t attribute) {
    switch (attribute) {
    case kVertexPosition: return "Position";
    case kVertexNormal: return "Normal";
    case 2: return "Tangent";
    case kVertexColor: return "Color";
    case kVertexTexCoord0: return "TexCoord0";
    case 5: return "TexCoord1";
    case kVertexPackedCharData: return "TexCoord2";
    case 7: return "TexCoord3";
    case 8: return "TexCoord4";
    case 9: return "TexCoord5";
    case 10: return "TexCoord6";
    case 11: return "TexCoord7";
    case kVertexBlendWeight: return "BlendWeight";
    case kVertexBlendIndices: return "BlendIndices";
    default: return "other";
    }
}


bool RawChannelContractsAvailable() {
    return Contract("mesh.get_vertex_attribute_count")->resolved && Contract("mesh.get_vertex_attribute")->resolved;
}

bool ReadVertexDeclaration(void* mesh, VertexDeclaration& declaration) {
    declaration = {};
    if (!mesh || !RawChannelContractsAvailable()) return false;

    int32_t count = 0;
    if (!InvokeValue(Contract("mesh.get_vertex_attribute_count"),
            mesh, nullptr, count)) {
        Log("Failed to read vertexAttributeCount from the source Mesh.");
        return false;
    }
    if (count <= 0 || count > kMaxVertexAttributes) {
        Log("Implausible vertexAttributeCount=" + std::to_string(count));
        return false;
    }

    for (int32_t i = 0; i < count; ++i) {
        int32_t index = i;
        void* p_index[1]{&index};
        VertexAttributeDescriptorRaw entry{};
        if (!InvokeValue(Contract("mesh.get_vertex_attribute"),
                mesh, p_index, entry)) {
            Log("Failed to read vertex attribute " + std::to_string(i));
            return false;
        }
        declaration.entries[static_cast<size_t>(i)] = entry;
    }
    declaration.count = count;
    return true;
}


void LogVertexChannel(
    const char* name, const VertexChannelInfo& info) {
    if (!info.present) {
        Log(std::string("  ") + name + ": absent");
        return;
    }
    Log(std::string("  ") + name + ": format=" +
        VertexFormatName(info.format) + "(" + std::to_string(info.format) +
        ") dim=" + std::to_string(info.dimension) +
        " stream=" + std::to_string(info.stream));
}

void LogVertexDeclaration(const VertexDeclaration& declaration) {
    for (int32_t i = 0; i < declaration.count; ++i) {
        const VertexAttributeDescriptorRaw& entry =
            declaration.entries[static_cast<size_t>(i)];
        Log("  [" + std::to_string(i) + "] " +
            VertexAttributeName(entry.attribute) + "(" +
            std::to_string(entry.attribute) + ") format=" +
            VertexFormatName(entry.format) + "(" +
            std::to_string(entry.format) + ") dim=" +
            std::to_string(entry.dimension) + " stream=" +
            std::to_string(entry.stream));
    }
}

using ResolveIcallFn = void* (*)(const char*);

// Kept free of unwindable objects so __try is legal here.
bool SafeResolveIcall(
    ResolveIcallFn resolve, const char* name, void** output) {
    *output = nullptr;
    __try {
        *output = resolve(name);
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

// Each raw engine call gets its own __try wrapper. These stay free of
// unwindable objects so __try is legal, and a wrong ABI shows up as a caught
// access violation in the log rather than as a dead client.
bool SafeQueryByAttribute(
    MeshQueryByAttributeFn fn, void* mesh, int32_t argument, int32_t* output) {
    *output = -1;
    __try {
        *output = fn(mesh, argument);
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

bool SafeQuery(MeshQueryFn fn, void* mesh, int32_t* output) {
    *output = -1;
    __try {
        *output = fn(mesh);
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}



bool SafeSetVertexBufferParams(
    void* mesh, int32_t vertex_count, const void* attributes,
    int32_t attribute_count) {
    __try {
        g_engine.set_vertex_buffer_params(
            mesh, vertex_count, attributes, attribute_count);
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

bool SafeSetVertexBufferData(
    void* mesh, int32_t stream, const void* data, int32_t byte_count) {
    __try {
        // count/elementSize are expressed in bytes: the payload is opaque here
        // and the declaration already fixes the real element layout.
        g_engine.set_vertex_buffer_data(
            mesh, stream, data, 0, 0, byte_count, 1, kMeshUpdateDefault);
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

uintptr_t GetNativeObjectPointer(void* object);

// Replaces the whole Position channel from the captured base plus this frame's
// deltas, exactly as Unity's own Mesh.SetVertices does. Only positions move:
// normals, tangents, UVs and the skin streams keep their bytes.
bool UploadMeshPositions(
    void* mesh, const BemMorphSet& set, const std::vector<float>& accumulated,
    const std::vector<uint32_t>& touched) {
    if (!g_channel.set_native_channel || !mesh || set.stride <= 0) return false;
    const size_t vertex_count = set.base.size() / size_t(set.stride);
    if (!vertex_count || vertex_count > (size_t(1) << 24) || set.offset + 12 > set.stride) return false;

    thread_local std::vector<float> positions;
    positions.resize(vertex_count * 3);
    const uint8_t* base = set.base.data();
    for (size_t v = 0; v < vertex_count; ++v)
        std::memcpy(positions.data() + v * 3, base + v * size_t(set.stride) + set.offset, 12);

    for (size_t i = 0; i < touched.size(); ++i) {
        const size_t index = touched[i];
        if (index >= set.vertices.size()) continue;
        const size_t vertex = set.vertices[index];
        if (vertex >= vertex_count) continue;
        float* position = positions.data() + vertex * 3;
        const float* delta = accumulated.data() + index * 3;
        for (int axis = 0; axis < 3; ++axis) position[axis] += delta[axis];
    }
    g_channel.set_native_channel(mesh, 0 /*Position*/, 0 /*Float32*/, 3,
        positions.data(), static_cast<int32_t>(vertex_count), 0,
        static_cast<int32_t>(vertex_count), 0 /*MeshUpdateFlags.Default*/);
    if (g_engine.upload_mesh_data) g_engine.upload_mesh_data(mesh, false);
    return true;
}

#if !defined(__ANDROID__)
// Byte-range upload into one vertex stream. Windows keeps the raw range setter,
// so only the runs that actually moved are pushed and the rest of the stream
// (UV, tangent, skin) is never rewritten.
bool SafeUpdateSubVertexBuffer(
    void* mesh, int32_t stream, const void* data,
    int32_t byte_offset, int32_t byte_count) {
    if (!g_engine.set_vertex_buffer_data || !mesh || !data || byte_count <= 0) return false;
    __try {
        g_engine.set_vertex_buffer_data(
            mesh, stream, data, 0, byte_offset, byte_count, 1, kMeshUpdateDefault);
        if (g_engine.upload_mesh_data) {
            g_engine.upload_mesh_data(mesh, false);
        }
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

#endif

bool SafeSetIndexBufferParams(
    void* mesh, int32_t index_count, int32_t index_format) {
    __try {
        g_engine.set_index_buffer_params(mesh, index_count, index_format);
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

bool SafeSetIndexBufferData(
    void* mesh, const void* data, int32_t byte_count) {
    __try {
        g_engine.set_index_buffer_data(
            mesh, data, 0, 0, byte_count, 1, kMeshUpdateDefault);
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

bool UploadComponentIndices(void* mesh, const BemComponent& component) {
    const auto& info=component.info;
    if ((info.index_element_size!=2 && info.index_element_size!=4) ||
        info.index_count>INT32_MAX || component.indices.size()>INT32_MAX ||
        component.indices.size()!=static_cast<size_t>(info.index_count)*info.index_element_size) return false;
    return SafeSetIndexBufferParams(mesh,static_cast<int32_t>(info.index_count),
        info.index_element_size==4?kIndexFormatUInt32:kIndexFormatUInt16) &&
        SafeSetIndexBufferData(mesh,component.indices.data(),static_cast<int32_t>(component.indices.size()));
}

bool SafeSetSubMesh(
    void* mesh, int32_t index, const SubMeshDescriptorRaw* descriptor) {
    __try {
        g_engine.set_sub_mesh(mesh, index, descriptor, kMeshUpdateDefault);
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

bool SafeUploadMeshData(void* mesh) {
    __try {
        // Keep the CPU copy for declaration checks and subsequent discovery.
        g_engine.upload_mesh_data(mesh, false);
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}


using ResolveIcallFn = void* (*)(const char*);

bool ResolveEngineBindings() {
    if (g_engine.resolved) return true;

#if defined(__ANDROID__)
    if (!betterendfieldnext::AndroidMeshBuilderReady()) return false;
    BE_FieldDescriptorV1 descriptor{"UnityEngine.CoreModule.dll", "UnityEngine", "Object", "m_CachedPtr", "System.IntPtr"};
    BE_ResolvedFieldV1 field{};
    std::string error;
    if (!g_host || g_host->resolve_field(g_host->context,&descriptor,&field)!=BE_Result_Ok ||
        field.offset < 0 || !ResolveNativeMeshLayout({},g_native_layout,error)) return false;
    g_engine.cached_ptr_offset = field.offset;
    HMODULE game = GetModuleHandleW(L"GameAssembly.dll");
    auto resolve = reinterpret_cast<ResolveIcallFn>(GetProcAddress(game,"il2cpp_resolve_icall"));
    g_engine.upload_mesh_data = reinterpret_cast<MeshUploadDataFn>(resolve ? resolve("UnityEngine.Mesh::UploadMeshDataImpl") : nullptr);
    g_channel.set_native_channel = reinterpret_cast<MeshSetNativeArrayForChannelFn>(
        resolve ? resolve("UnityEngine.Mesh::SetNativeArrayForChannelImpl") : nullptr);
    g_vertex_upload_available[kVertexUploadChannel] = g_channel.set_native_channel != nullptr;
    g_vertex_upload_available[kVertexUploadByteRange] = false; // stripped on Android
    g_vertex_upload_mode.store(kVertexUploadChannel, std::memory_order_relaxed);
    Log("Android engine bindings: upload=" + std::to_string(g_engine.upload_mesh_data != nullptr) +
        " channel=" + std::to_string(g_vertex_upload_available[kVertexUploadChannel]));
    // Never gate model construction on the optional position channel setter: a
    // missing icall must degrade to a static (non-animated) model, not to a
    // refused replacement.
    g_engine.resolved = (g_engine.upload_mesh_data != nullptr);
    return g_engine.resolved;
#else
    HMODULE game = GetModuleHandleW(L"GameAssembly.dll");
    auto resolve = reinterpret_cast<ResolveIcallFn>(
        game ? reinterpret_cast<void*>(
                   GetProcAddress(game, "il2cpp_resolve_icall"))
             : nullptr);
    if (!resolve) {
        Log("il2cpp_resolve_icall unavailable; engine bindings cannot be bound.");
        return false;
    }

    struct Binding {
        const char* name;
        void** slot;
    };
    const std::array<Binding, 8> bindings{{
        {"UnityEngine.Mesh::GetVertexBufferStride",
            reinterpret_cast<void**>(&g_engine.get_vertex_buffer_stride)},
        {"UnityEngine.Mesh::get_vertexBufferCount",
            reinterpret_cast<void**>(&g_engine.get_vertex_buffer_count)},
        {"UnityEngine.Mesh::SetVertexBufferParamsFromPtr",
            reinterpret_cast<void**>(&g_engine.set_vertex_buffer_params)},
        {"UnityEngine.Mesh::InternalSetVertexBufferData",
            reinterpret_cast<void**>(&g_engine.set_vertex_buffer_data)},
        {"UnityEngine.Mesh::SetIndexBufferParams",
            reinterpret_cast<void**>(&g_engine.set_index_buffer_params)},
        {"UnityEngine.Mesh::InternalSetIndexBufferData",
            reinterpret_cast<void**>(&g_engine.set_index_buffer_data)},
        {"UnityEngine.Mesh::SetSubMesh_Injected",
            reinterpret_cast<void**>(&g_engine.set_sub_mesh)},
        {"UnityEngine.Mesh::UploadMeshDataImpl",
            reinterpret_cast<void**>(&g_engine.upload_mesh_data)},
    }};

    bool complete = true;
    for (const Binding& binding : bindings) {
        void* pointer = nullptr;
        if (!SafeResolveIcall(resolve, binding.name, &pointer) || !pointer) {
            Log(std::string("Engine binding unresolved: ") + binding.name);
            complete = false;
            continue;
        }
        *binding.slot = pointer;
    }

    // Optional on desktop: the morph upload path degrades to the byte-range
    // setter when this one is missing, so it never blocks model construction.
    {
        void* pointer = nullptr;
        if (SafeResolveIcall(resolve, "UnityEngine.Mesh::SetNativeArrayForChannelImpl", &pointer) && pointer)
            g_channel.set_native_channel = reinterpret_cast<MeshSetNativeArrayForChannelFn>(pointer);
        g_vertex_upload_available[kVertexUploadChannel] = g_channel.set_native_channel != nullptr;
        g_vertex_upload_available[kVertexUploadByteRange] = g_engine.set_vertex_buffer_data != nullptr;
        // Prefer the channel path, but only when it is actually present.
        g_vertex_upload_mode.store(g_vertex_upload_available[kVertexUploadChannel]
            ? kVertexUploadChannel : kVertexUploadByteRange, std::memory_order_relaxed);
        Log("Desktop morph upload: channel=" + std::to_string(g_vertex_upload_available[kVertexUploadChannel]) +
            " byte_range=" + std::to_string(g_vertex_upload_available[kVertexUploadByteRange]) +
            " default=" + (g_vertex_upload_mode.load() == kVertexUploadChannel ? "channel" : "byte_range"));
    }
    if (g_engine.cached_ptr_offset < 0) {
        if (g_host && g_host->resolve_field) {
            const BE_FieldDescriptorV1 cached_ptr{
                "UnityEngine.CoreModule.dll", "UnityEngine", "Object",
                "m_CachedPtr", "System.IntPtr"};
            BE_ResolvedFieldV1 field{};
            if (g_host->resolve_field(g_host->context, &cached_ptr, &field) == BE_Result_Ok &&
                field.field_info && field.offset >= 0) {
                g_engine.cached_ptr_offset = field.offset;
                Log(std::string("Engine binding resolved m_CachedPtr offset: ") + std::to_string(field.offset));
            }
        }
        if (g_engine.cached_ptr_offset < 0) {
            Log("m_CachedPtr metadata is unavailable; refusing native access.");
            return false;
        }
    }
    if (complete) {
        std::string error;
#if defined(__ANDROID__)
        // The Android resolver discovers ELF segments itself. A dlopen handle
        // is not an image address and must never be passed as a mapped span.
        complete = ResolveNativeMeshLayout({},g_native_layout,error);
#else
        HMODULE player = GetModuleHandleW(L"UnityPlayer.dll");
        size_t size = MappedImageSize(player);
        complete = size && ResolveNativeMeshLayout({reinterpret_cast<const uint8_t*>(player),size},g_native_layout,error);
#endif
        if (!complete) Log("Native mesh layout: " + error);
        else Log("Native mesh layout resolved: offset=" + std::to_string(g_native_layout.bones_per_vertex_offset) +
            " serializers=" + std::to_string(g_native_layout.agreeing_serializers));
    }
    g_engine.resolved = complete;
    return complete;
#endif
}

uintptr_t GetNativeObjectPointer(void* object) {
    if (!object || g_engine.cached_ptr_offset < 0) return 0;
    uintptr_t ptr = 0;
#if defined(__ANDROID__)
    if (!AndroidReadMemory(reinterpret_cast<uintptr_t>(object) +
            static_cast<uintptr_t>(g_engine.cached_ptr_offset), &ptr, sizeof(ptr))) return 0;
#else
    __try {
        ptr = *reinterpret_cast<const uintptr_t*>(
            reinterpret_cast<const uint8_t*>(object) + g_engine.cached_ptr_offset);
    } __except (EXCEPTION_EXECUTE_HANDLER) {
        ptr = 0;
    }
#endif
    return ptr;
}

bool TryReadNativeUInt32(uintptr_t address, uint32_t& value) {
    if (!address) return false;
#if defined(__ANDROID__)
    return AndroidReadMemory(address, &value, sizeof(value));
#else
    __try {
        value = *reinterpret_cast<const volatile uint32_t*>(address);
        return true;
    } __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
#endif
}

bool TryWriteNativeUInt32(uintptr_t address, uint32_t value) {
    if (!address) return false;
#if defined(__ANDROID__)
    uint32_t readback = 0;
    return AndroidWriteMemory(address, &value, sizeof(value)) &&
        AndroidReadMemory(address, &readback, sizeof(readback)) && readback == value;
#else
    __try {
        *reinterpret_cast<volatile uint32_t*>(address) = value;
        return (*reinterpret_cast<const volatile uint32_t*>(address) == value);
    } __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
#endif
}


bool DeclarationsEqual(
    const VertexDeclaration& left, const VertexDeclaration& right) {
    if (left.count != right.count) return false;
    for (int32_t i = 0; i < left.count; ++i) {
        const VertexAttributeDescriptorRaw& a =
            left.entries[static_cast<size_t>(i)];
        const VertexAttributeDescriptorRaw& b =
            right.entries[static_cast<size_t>(i)];
        if (a.attribute != b.attribute || a.format != b.format ||
            a.dimension != b.dimension || a.stream != b.stream) {
            return false;
        }
    }
    return true;
}

bool ReadMeshStrides(void* mesh, std::vector<int32_t>& strides) {
#if defined(__ANDROID__)
    return betterendfieldnext::AndroidReadMeshStrides(mesh,strides);
#else
    strides.clear();
    if (!ResolveEngineBindings()) return false;
    int32_t buffer_count = -1;
    if (!SafeQuery(g_engine.get_vertex_buffer_count, mesh, &buffer_count) ||
        buffer_count <= 0 || buffer_count > 8) {
        return false;
    }
    for (int32_t stream = 0; stream < buffer_count; ++stream) {
        int32_t stride = -1;
        if (!SafeQueryByAttribute(
                g_engine.get_vertex_buffer_stride, mesh, stream, &stride) ||
            stride <= 0) {
            return false;
        }
        strides.push_back(stride);
    }
    return true;
#endif
}

std::string StrideText(const std::vector<int32_t>& strides) {
    std::string text;
    for (int32_t stride : strides) {
        if (!text.empty()) text += "/";
        text += std::to_string(stride);
    }
    return text;
}

// Confirms a mesh reproduces the expected declaration and stream strides.
bool MatchesDeclaration(
    void* mesh, const VertexDeclaration& expected,
    const std::vector<int32_t>& expected_strides, const std::string& label) {
    VertexDeclaration actual;
    if (!ReadVertexDeclaration(mesh, actual)) {
        Log("  " + label + ": declaration unreadable");
        return false;
    }
    if (!DeclarationsEqual(actual, expected)) {
        Log("  " + label + ": declaration differs from the source");
        Log("    expected:");
        LogVertexDeclaration(expected);
        Log("    actual:");
        LogVertexDeclaration(actual);
        return false;
    }

    std::vector<int32_t> actual_strides;
    if (!ReadMeshStrides(mesh, actual_strides)) {
        Log("  " + label + ": strides unreadable");
        return false;
    }
    if (actual_strides != expected_strides) {
        Log("  " + label + ": strides " + StrideText(actual_strides) +
            " differ from the source's " + StrideText(expected_strides));
        return false;
    }
    return true;
}


struct BoneWeight1Raw {
    float weight;
    int32_t bone_index;
};
static_assert(sizeof(BoneWeight1Raw) == 8);

bool DecodeComponentSkin(const BemComponent& component,
    std::vector<uint8_t>& counts, std::vector<BoneWeight1Raw>& weights) {
    const auto& info = component.info;
    if ((info.stride2 != 4 && info.stride2 != 12 && info.stride2 != 32) ||
        component.streams[2].size() != static_cast<size_t>(info.vertex_count) * info.stride2)
        return false;
    counts.reserve(info.vertex_count);
    weights.reserve(static_cast<size_t>(info.vertex_count) * 4);
    for (uint32_t v = 0; v < info.vertex_count; ++v) {
        const uint8_t* data = component.streams[2].data() + static_cast<size_t>(v) * info.stride2;
        const uint8_t* indices = data + info.stride2 - 4;
        std::array<BoneWeight1Raw, 4> influences{};
        uint8_t count = 0;
        float sum = 0;
        for (int k = 0; k < (info.stride2 == 4 ? 1 : 4); ++k) {
            float weight = 1.0f; int32_t bone = 0;
            if (info.stride2 == 32) {
                // BEM 1.2 uncompressed layout: float32 weights, UInt32 indices.
                uint32_t index = 0;
                std::memcpy(&weight, data + k * 4, sizeof(weight));
                std::memcpy(&index, data + 16 + k * 4, sizeof(index));
                if (component.skip_validation ? weight==0.0f : !(weight > 0.0f)) continue;
                if (index>INT32_MAX || (!component.skip_validation && (!std::isfinite(weight) || index > 255))) return false;
                bone = static_cast<int32_t>(index);
            } else {
                uint16_t packed = 65535;
                if (info.stride2 == 12) std::memcpy(&packed, data + k * 2, sizeof(packed));
                if (!packed) continue;
                weight = static_cast<float>(packed) / 65535.0f;
                bone = indices[k];
            }
            influences[count++] = {weight, bone};
            sum += weight;
        }
        if (!component.skip_validation && (!count || std::abs(sum - 1.0f) > 0.01f)) {
            Log("C" + std::to_string(info.component_id) + " invalid skin weights at vertex " +
                std::to_string(v) + " sum=" + std::to_string(sum));
            return false;
        }
        if(!component.skip_validation) std::sort(influences.begin(), influences.begin() + count,
            [](const auto& a, const auto& b) { return a.weight > b.weight; });
        for (int k = 0; k < count; ++k) {
            if(!component.skip_validation) influences[k].weight /= sum;
            weights.push_back(influences[k]);
        }
        counts.push_back(count);
    }
    return true;
}


bool ValidateRendererSkin(const BemComponent& component, void* renderer, void* source_mesh) {
    if(component.skip_validation) return true;
    std::vector<uint8_t> counts;
    std::vector<BoneWeight1Raw> weights;
    if (!DecodeComponentSkin(component, counts, weights)) return false;
    void* bones = Invoke(Contract("skinned.get_bones"), renderer, nullptr, false);
    void* poses = Invoke(Contract("mesh.get_bindposes"), source_mesh, nullptr, false);
    const int bone_count = ArrayLength(bones), pose_count = ArrayLength(poses);

    std::array<bool, 256> checked{};
    for (const auto& weight : weights) {
        const int index = weight.bone_index;
        if (checked[index]) continue;
        checked[index] = true;
        Matrix4x4Raw matrix{};
        bool valid = index < bone_count && index < pose_count &&
            Unbox(ArrayValue(poses,index),matrix) &&
            IsNativeObjectAlive(ArrayValue(bones, index));
        if (valid) {
            const float* values = matrix.m;
            float magnitude = 0;
            for (int n = 0; n < 16; ++n) {
                valid = valid && std::isfinite(values[n]);
                magnitude += std::abs(values[n]);
            }
            valid = valid && magnitude > 0;
        }
        if (!valid) {
            Log("Skin palette incomplete: C" + std::to_string(component.info.component_id) +
                " bone=" + std::to_string(index) + " renderer=" + ObjectName(renderer));
            return false;
        }
    }
    return true;
}

uint32_t Crc32(std::string_view text);
void RememberCpuGeometry(void* mesh,const BemComponent& component);
bool BuildMeshFromComponent(
    const BemComponent& component, void* source_mesh, void*& new_mesh, void* prepared_bindposes=nullptr) {
    new_mesh = nullptr;
    const BemComponentHeaderRaw& info = component.info;
    const std::string label = "C" + std::to_string(info.component_id);

    if (!ResolveEngineBindings()) {
        Log(label + " needs the engine Mesh layout bindings; refusing.");
        return false;
    }

    VertexDeclaration declaration;
    // The replacement owns its vertex declaration. The donor supplies bones,
    // bindposes and materials, not the layout of the newly allocated buffers.
    // In particular, Android may compress a native mesh differently from the
    // Windows profile used to author an otherwise valid BEM package.
    if (component.attributes.empty() || component.attributes.size()>declaration.entries.size()) {
        Log(label + " replacement declaration is missing or too large; refusing.");
        return false;
    }
    declaration.count=static_cast<int32_t>(component.attributes.size());
    std::memcpy(declaration.entries.data(),component.attributes.data(),component.attributes.size()*16);
    VertexDeclaration source_declaration;
    if (ReadVertexDeclaration(source_mesh,source_declaration) &&
        (source_declaration.count!=declaration.count ||
        std::memcmp(source_declaration.entries.data(),declaration.entries.data(),component.attributes.size()*16)!=0)) {
        Log(label + " native vertex layout differs; rebuilding from the BEM declaration.");
    }

    std::vector<int32_t> payload_strides{
        static_cast<int32_t>(info.stride0),
        static_cast<int32_t>(info.stride1),
        static_cast<int32_t>(info.stride2)};
    while (!payload_strides.empty() && !payload_strides.back()) payload_strides.pop_back();
    if (component.static_mesh && (!component.bones.empty() || !component.bone_names.empty() ||
        std::any_of(component.attributes.begin(),component.attributes.end(),[](const auto& a){return a[0]==12 || a[0]==13;}))) {
        Log(label+" static Mesh carries a skin contract; refusing."); return false;
    }

    void* bindposes = component.static_mesh?nullptr:(prepared_bindposes ? prepared_bindposes : Invoke(
        Contract("mesh.get_bindposes"), source_mesh, nullptr));
    const int bindpose_count = ArrayLength(bindposes);
    if (!component.static_mesh && (!bindposes || (!component.skip_validation && bindpose_count <= static_cast<int>(info.max_bone)))) {
        Log(label + " bindpose palette too small: bindposes=" +
            std::to_string(bindpose_count) + " maxBone=" +
            std::to_string(info.max_bone));
        return false;
    }

    void* mesh = NewAsset(g_mesh_class.class_info);
    if (!mesh || !InvokeVoid(Contract("mesh.ctor"), mesh, nullptr)) {
        Log(label + " failed to construct UnityEngine.Mesh.");
        return false;
    }

    if (auto* set_name = Contract("object.set_name");
        set_name && set_name->resolved && g_host->string_new) {
        const std::string name =
            ObjectName(source_mesh);
        void* managed = NewString(name.c_str());
        void* parameters[1]{managed};
        InvokeVoid(set_name, mesh, parameters);
    }

    // Initialize native Mesh m_BonesPerVertex directly from the source mesh native value
    // as early as possible before any buffer, geometry or bindpose assignment.
    const uintptr_t source_native = GetNativeObjectPointer(source_mesh);
    const uintptr_t new_native = GetNativeObjectPointer(mesh);
    uint32_t source_native_influences = 0;
    if (component.static_mesh) {
        // A MeshRenderer has no skin palette. Do not read or manufacture HG's
        // private skin metadata from its donor.
    } else if (source_native && new_native) {
        const bool read_source_ok = TryReadNativeUInt32(source_native + g_native_layout.bones_per_vertex_offset, source_native_influences);
        if (read_source_ok &&
            (component.skip_validation || source_native_influences == 1 || source_native_influences == 2 || source_native_influences == 4)) {
            const bool write_ok = TryWriteNativeUInt32(new_native + g_native_layout.bones_per_vertex_offset, source_native_influences);
            if (!write_ok) {
                Log(label + " failed to write native Mesh m_BonesPerVertex; refusing.");
                DestroyUnityObject(mesh);
                return false;
            }
        } else {
            Log(label + " failed to read native Mesh m_BonesPerVertex from source; refusing.");
            DestroyUnityObject(mesh);
            return false;
        }
    } else {
        Log(label + " failed to obtain native Mesh pointers (source=" +
            std::to_string(source_native) + " new=" + std::to_string(new_native) + "); refusing.");
        DestroyUnityObject(mesh);
        return false;
    }

    if (!component.skip_validation && !component.bones.empty()) {
        std::vector<uint8_t> counts; std::vector<BoneWeight1Raw> weights;
        if (!DecodeComponentSkin(component,counts,weights) ||
            std::any_of(counts.begin(),counts.end(),[&](uint8_t count){ return count>source_native_influences; })) {
            Log(label+" source native skin field cannot represent replacement influences.");
            DestroyUnityObject(mesh); return false;
        }
    }
    const int32_t vertex_count = static_cast<int32_t>(info.vertex_count);
    const int32_t index_count = static_cast<int32_t>(info.index_count);
    int sub_mesh_count = component.draws.empty() ? 1 : static_cast<int>(component.draws.size());
#if defined(__ANDROID__)
    if (!betterendfieldnext::AndroidSubmitMesh(mesh,component)) {
        Log(label + " Android MeshData submission/readback failed.");
        DestroyUnityObject(mesh); return false;
    }
#else
    std::array<VertexAttributeDescriptorRaw, kMaxVertexAttributes> attributes{};
    for (int32_t i = 0; i < declaration.count; ++i) {
        attributes[static_cast<size_t>(i)] =
            declaration.entries[static_cast<size_t>(i)];
    }
    if (!SafeSetVertexBufferParams(
            mesh, vertex_count, attributes.data(), declaration.count)) {
        Log(label + " SetVertexBufferParams faulted.");
        DestroyUnityObject(mesh);
        return false;
    }

    // The BEM declaration already exposes the packed BlendWeight and
    // BlendIndices fields in stream 2. Keep that one authoritative storage:
    // InternalSetBoneWeights creates a second Unity skinning representation,
    // then the former second SetVertexBufferParams call replaced its layout
    // again. HG can retain either representation across a renderer refresh,
    // which leaves the draw in bind pose when they disagree. Upload the exact
    // source layout and its raw skin stream once instead.

    for (int32_t stream = 0; stream < static_cast<int32_t>(payload_strides.size());
            ++stream) {
        const std::vector<uint8_t>& data =
            component.streams[static_cast<size_t>(stream)];
        if (!SafeSetVertexBufferData(
                mesh, stream, data.data(), static_cast<int32_t>(data.size()))) {
            Log(label + " SetVertexBufferData faulted on stream " +
                std::to_string(stream));
            DestroyUnityObject(mesh);
            return false;
        }
    }

    if (!UploadComponentIndices(mesh,component)) {
        Log(label + " index buffer upload faulted.");
        DestroyUnityObject(mesh);
        return false;
    }

    // v25 stores the selected draw ranges consecutively; each has its own
    // private donor material. v24 retains the original single-submesh path.
    void* p_sub_mesh_count[1]{&sub_mesh_count};
    if (!InvokeVoid(
            Contract("mesh.set_sub_mesh_count"), mesh, p_sub_mesh_count)) {
        Log(label + " failed to set the submesh count.");
        DestroyUnityObject(mesh);
        return false;
    }

    SubMeshDescriptorRaw descriptor{};
    descriptor.topology = kTopologyTriangles;
    descriptor.index_start = 0;
    descriptor.index_count = index_count;
    descriptor.base_vertex = 0;
    descriptor.first_vertex = 0;
    descriptor.vertex_count = vertex_count;
    for (int sub=0; sub<sub_mesh_count; ++sub) {
        if (!component.draws.empty()) {
            descriptor.index_start=static_cast<int32_t>(component.draws[sub].start);
            descriptor.index_count=static_cast<int32_t>(component.draws[sub].count);
        }
        if (!SafeSetSubMesh(mesh, sub, &descriptor)) {
            Log(label + " SetSubMesh faulted."); DestroyUnityObject(mesh); return false;
        }
    }
#endif

    EnsureAnimationDriverLoaded();
    // Section 10 samples captured at load. Registering them here is what lets a
    // driver address this mesh later; without a record the tick has nothing to
    // write into.
    if (component.morph && !component.morph->channels.empty()) {
        int32_t mesh_id = 0;
        if (InvokeValue(Contract("object.instance_id"), mesh, nullptr, mesh_id) && mesh_id != 0) {
            DynamicMeshRecord rec{};
            rec.component_id = info.component_id;
            rec.morph = component.morph;
            auto channels = component.morph->channels.size();
            auto vertices = component.morph->vertices.size();
            g_dynamic_meshes[mesh_id] = std::move(rec);
            Log(label + " registered morph source: " + std::to_string(channels) + " channels, " +
                std::to_string(vertices) + " sampled vertices");
        }
    }
    void* p_bindposes[1]{bindposes};
    if (!component.static_mesh && !InvokeVoid(Contract("mesh.set_bindposes"), mesh, p_bindposes)) {
        Log(label + " failed to install the original bindpose palette.");
        DestroyUnityObject(mesh);
        return false;
    }
#if defined(__ANDROID__)
    void* read_poses = Invoke(Contract("mesh.get_bindposes"),mesh,nullptr);
    bool poses_equal = ArrayLength(read_poses) == bindpose_count;
    for (int i=0; poses_equal && i<bindpose_count; ++i) {
        Matrix4x4Raw expected{}, actual{};
        poses_equal = Unbox(ArrayValue(bindposes,i),expected) && Unbox(ArrayValue(read_poses,i),actual) &&
            std::memcmp(&expected,&actual,sizeof(expected)) == 0;
    }
    if (!component.skip_validation && !poses_equal) { Log(label+" bindpose readback differs."); DestroyUnityObject(mesh); return false; }
#endif

    // Do not RecalculateNormals/Tangents. Endfield packs normal, encoded
    // tangent and bitangent sign into a single channel the shader unpacks.
    if (!InvokeVoid(Contract("mesh.recalculate_bounds"), mesh, nullptr)) {
        Log(label + " RecalculateBounds failed.");
        DestroyUnityObject(mesh);
        return false;
    }

    int built_vertex_count = -1;
    uint32_t built_index_count = 0;
    int32_t submesh = 0;
    void* index_parameters[1]{&submesh};
    InvokeValue(
        Contract("mesh.get_vertex_count"), mesh, nullptr, built_vertex_count);
    for (submesh=0; submesh<sub_mesh_count; ++submesh) {
        uint32_t size=0;
        if (!InvokeValue(Contract("mesh.get_index_count"),mesh,index_parameters,size)) {
            DestroyUnityObject(mesh); return false;
        }
        built_index_count += size;
    }
    if (!component.skip_validation && (built_vertex_count != vertex_count ||
        built_index_count != static_cast<uint32_t>(index_count))) {
        Log(label + " built geometry counts disagree with the payload: vtx=" +
            std::to_string(built_vertex_count) + " idx=" +
            std::to_string(built_index_count));
        DestroyUnityObject(mesh);
        return false;
    }

    // Verify before assigning. The writer signatures are inferred from Unity's
    // published bindings, so a wrong one shows up here as a refusal rather
    // than as a corrupted renderer.
    if (!component.skip_validation && !MatchesDeclaration(mesh, declaration, payload_strides, label +
            " built")) {
        Log(label + " does not reproduce the BEM declaration; refusing.");
        DestroyUnityObject(mesh);
        return false;
    }

    // Submit the completed mesh before any renderer can consume it. The raw
    // setters populate CPU mesh storage; otherwise upload is left until a
    // later render after the resource has already been delivered.
    uint8_t has_skin = 0;
    const bool skin_read = InvokeValue(Contract("mesh.has_bone_weights"), mesh, nullptr, has_skin);
    if (!component.skip_validation && (!skin_read || static_cast<bool>(has_skin)==component.static_mesh)) {
        Log(label + (component.static_mesh?" static Mesh skin metadata is unexpected or unreadable; original mesh retained.":
            " packed stream did not activate skin metadata; original mesh retained."));
        DestroyUnityObject(mesh);
        return false;
    }
    if (!SafeUploadMeshData(mesh)) {
        Log(label + " UploadMeshData(false) faulted; original mesh retained.");
        DestroyUnityObject(mesh);
        return false;
    }
    if (g_construction) {
        ++g_construction->mesh_submitted;
        uint64_t mesh_bytes=component.indices.size();
        for (const auto& stream:component.streams) mesh_bytes+=stream.size();
        g_construction->mesh_payload_bytes+=mesh_bytes;
        g_construction->RecordUpload(mesh_bytes);
    }

    uint32_t final_influences = 0;
    if (!component.static_mesh && (!TryReadNativeUInt32(new_native + g_native_layout.bones_per_vertex_offset,final_influences) ||
        final_influences != source_native_influences)) {
        Log(label + " native skin field changed after upload; refusing.");
        DestroyUnityObject(mesh); return false;
    }

    new_mesh = mesh;
    RememberCpuGeometry(mesh,component);
    return true;
}

const char* TextureFormatName(int32_t format) {
    switch (format) {
    case 4: return "RGBA32";
    case 63: return "R8";
    case 10: return "DXT1";
    case 12: return "DXT5";
    case 25: return "BC7";
    case 26: return "BC4";
    case 27: return "BC5";
    case 48: return "ASTC4x4";
    case 49: return "ASTC5x5";
    case 50: return "ASTC6x6";
    default: return "?";
    }
}

struct MaterialTextureSlot {
    int32_t slot_id = 0;
    void* texture = nullptr;
    int32_t width = -1;
    int32_t height = -1;
    int32_t graphics_format = -1;
};


std::vector<MaterialTextureSlot> ReadMaterialTextureSlots(void* material) {
    std::vector<MaterialTextureSlot> slots;
    void* ids = Invoke(Contract("material.get_texture_property_ids"),
        material, nullptr);
    const int id_count = ArrayLength(ids);
    if (!ids || id_count <= 0) return slots;

    MethodContract* get_texture = Contract("material.get_texture_by_id");
    for (int i = 0; i < id_count; ++i) {
        MaterialTextureSlot slot;
        if (!Unbox(ArrayValue(ids,i),slot.slot_id)) return {};
        int32_t id_argument = slot.slot_id;
        void* parameters[1]{&id_argument};
        slot.texture = Invoke(get_texture, material, parameters, false);
        if (!slot.texture) continue;
        InvokeValue(Contract("texture.get_width"), slot.texture, nullptr,
            slot.width);
        InvokeValue(Contract("texture.get_height"), slot.texture, nullptr,
            slot.height);
        InvokeValue(Contract("texture.get_graphics_format"), slot.texture,
            nullptr, slot.graphics_format);
        slots.push_back(slot);
    }
    return slots;
}

bool SlotGraphicsFormat(int32_t texture_format, int32_t srgb, int32_t& output) {
    int32_t format_argument = texture_format;
    uint8_t srgb_argument = srgb != 0 ? 1 : 0;
    void* parameters[2]{&format_argument, &srgb_argument};
    return InvokeValue(
        Contract("graphics_format_utility.get_graphics_format"), nullptr,
        parameters, output);
}

bool CopySamplerState(void* source, void* destination) {
    const auto copy_enum = [&](const char* getter, const char* setter) {
        int32_t value=0,read=0;
        if (!InvokeValue(Contract(getter),source,nullptr,value)) return false;
        void* args[]{&value};
        return InvokeVoid(Contract(setter),destination,args) &&
            InvokeValue(Contract(getter),destination,nullptr,read) && read==value;
    };
    if (!copy_enum("texture.get_wrap_mode","texture.set_wrap_mode") ||
        !copy_enum("texture.get_filter_mode","texture.set_filter_mode") ||
        !copy_enum("texture.get_aniso_level","texture.set_aniso_level")) return false;
    for (const auto axis : {"u","v","w"}) {
        const auto getter=std::string("texture.get_wrap_")+axis,setter=std::string("texture.set_wrap_")+axis;
        if (Contract(getter)->resolved && Contract(setter)->resolved && !copy_enum(getter.c_str(),setter.c_str())) return false;
    }
    float bias=0,read=0;
    if (!InvokeValue(Contract("texture.get_mip_map_bias"),source,nullptr,bias) || !std::isfinite(bias)) return false;
    void* args[]{&bias};
    return InvokeVoid(Contract("texture.set_mip_map_bias"),destination,args) &&
        InvokeValue(Contract("texture.get_mip_map_bias"),destination,nullptr,read) && read==bias;
}

// Exact texture sampler identity: two donors with the same value produce the
// same CopySamplerState result, so their replacements may share one upload.
bool TextureSamplerIdentity(void* texture,std::string& key) {
    const auto add=[&](const auto& value) { key.append(reinterpret_cast<const char*>(&value),sizeof(value)); };
    for (const auto* getter:{"texture.get_wrap_mode","texture.get_filter_mode","texture.get_aniso_level"}) {
        int32_t value=0; if (!InvokeValue(Contract(getter),texture,nullptr,value)) return false; add(value);
    }
    for (const auto* getter:{"texture.get_wrap_u","texture.get_wrap_v","texture.get_wrap_w"}) {
        auto* method=Contract(getter); if (!method || !method->resolved) continue;
        int32_t value=0; if (!InvokeValue(method,texture,nullptr,value)) return false; add(value);
    }
    float bias=0; if (!InvokeValue(Contract("texture.get_mip_map_bias"),texture,nullptr,bias) || !std::isfinite(bias)) return false;
    add(bias); return true;
}
// Immutable content identity of a deferred BEM texture entry: package
// generation + payload extent + texture description. Empty when the payload
// was decoded inline (no package generation is known for those bytes).
std::string BemTextureContentIdentity(const BemPocData& bem,size_t index) {
    if (index>=bem.textures.size() || !bem.payload_source || bem.textures[index].payload_id==UINT32_MAX) return {};
    const auto& t=bem.textures[index];const auto& source=*bem.payload_source;
    std::string key="bem-texture-v2\n"+source.path.lexically_normal().generic_string()+"\n";
    const auto add=[&](const auto& value) { key.append(reinterpret_cast<const char*>(&value),sizeof(value)); };
    add(source.file_size);add(source.write_time);add(t.payload_id);add(t.payload_codec);add(t.payload_offset);add(t.payload_stored);
    add(t.info);key+=t.original_name;return key;
}
// Generated texture instance ID -> content identity (metadata only, bounded).
// A texture is only ever looked up through a live published material, so a
// stale entry never keeps or resurrects anything.
std::unordered_map<int32_t,std::string> g_generated_texture_identity;
std::vector<int32_t> g_generated_texture_order;
void RememberGeneratedTexture(void* texture,const std::string& identity) {
    int32_t id=0;
    if (identity.empty() || !InvokeValue(Contract("object.instance_id"),texture,nullptr,id)) return;
    if (g_generated_texture_identity.insert_or_assign(id,identity).second) g_generated_texture_order.push_back(id);
    while (g_generated_texture_order.size()>4096) {
        g_generated_texture_identity.erase(g_generated_texture_order.front());
        g_generated_texture_order.erase(g_generated_texture_order.begin());
    }
}
#if defined(__ANDROID__)
#include "android_lod_streaming_observer.inc"
#include "android_texture_streaming_diagnostics.inc"
#endif
constexpr uint64_t kRenderSyncBytes=4ull*1024*1024;
constexpr uint64_t kFastLoadingSyncBytes=128ull*1024*1024;
bool g_fast_loading=false; // [CustomModel] fast_loading, fixed for the session
uint64_t g_unsynced_upload_bytes=0;
void* CreateTextureFromBem(const BemPocData& bem,size_t index) {
    const BemTexture& texture=bem.textures.at(index);
    const BemTextureEntryRaw& info = texture.info;
#ifdef __ANDROID__
    ObserveAndroidTextureStreamingBeforeUpload();
    // Installation probes EGL; confirm against the game's actual graphics backend as well.
    int32_t supported_format=info.create_format; uint8_t supported=0;
    void* supported_args[]{&supported_format};
    if(!InvokeValue(Contract("android.texture_supported"),nullptr,supported_args,supported) || !supported) {
        Log("Texture format unsupported by game backend: "+texture.name);return nullptr;
    }
#endif
    void* object =
        NewAsset(g_texture2d_class.class_info);
    if (!object) {
        Log("  failed to allocate a Texture2D for t=" + texture.name);
        return nullptr;
    }

    int32_t width = static_cast<int32_t>(info.width);
    int32_t height = static_cast<int32_t>(info.height);
    int32_t format = info.create_format;
    int32_t mip_count = static_cast<int32_t>(info.mip_count);
    uint8_t linear = info.create_srgb != 0 ? 0 : 1;
    void* ctor_parameters[5]{&width, &height, &format, &mip_count, &linear};
    if (!InvokeVoid(Contract("texture2d.ctor"), object, ctor_parameters)) {
        Log("  Texture2D construction failed for t=" + texture.name);
        return nullptr;
    }
    if (g_construction) ++g_construction->texture_constructed;

    // Deferred entries are decoded here, one texture at a time, and released
    // as soon as Unity copied them; the package is never decoded whole.
    std::vector<uint8_t> decoded;
    const std::vector<uint8_t>* bytes=&texture.data;
    if (texture.Deferred()) {
        std::string error;
        if (!bem.payload_source || !DecodeBemTexturePayload(*bem.payload_source,texture,decoded,error)) {
            Log("  texture payload decode failed for t=" + texture.name + ": " + error);
            DestroyUnityObject(object);
            return nullptr;
        }
        bytes=&decoded;
        if (g_construction) g_construction->HoldDecoded(decoded.size());
    }
    const uint64_t payload_bytes=bytes->size();
    if (bytes->empty() || payload_bytes>INT32_MAX || payload_bytes!=info.data_size) {
        Log("  texture payload size mismatch for t=" + texture.name);
        if (g_construction) g_construction->ReleaseDecoded(decoded.size());
        DestroyUnityObject(object);
        return nullptr;
    }
    void* data_pointer = const_cast<uint8_t*>(bytes->data());
    int32_t data_size = static_cast<int32_t>(payload_bytes);
    void* upload_parameters[2]{&data_pointer, &data_size};
    const bool loaded=InvokeVoid(Contract("texture2d.load_raw_texture_data"), object,
            upload_parameters);
    if (g_construction) g_construction->ReleaseDecoded(decoded.size());
    std::vector<uint8_t>().swap(decoded); // Unity holds its own copy now.
    if (!loaded) {
        Log("  LoadRawTextureData failed for t=" + texture.name);
        DestroyUnityObject(object);
        return nullptr;
    }

    // Do not regenerate mipmaps: the payload already carries whatever chain the
    // source had. Release the CPU copy, which is several megabytes per surface.
    uint8_t update_mipmaps = 0;
    uint8_t no_longer_readable = 1;
    void* apply_parameters[2]{&update_mipmaps, &no_longer_readable};
    if (!InvokeVoid(Contract("texture2d.apply"), object, apply_parameters)) {
        Log("  Apply failed for t=" + texture.name);
        DestroyUnityObject(object);
        return nullptr;
    }
    // Synchronous delivery uploads a whole character in one frame. Without a
    // render-thread sync every Unity CPU copy stays alive until the frame ends,
    // so the transient peak grows with the package instead of one texture.
    // Default syncs about every 4MiB (lowest peak, for small-memory devices);
    // the optional fast mode syncs every 128MiB (shorter stall, higher peak).
    g_unsynced_upload_bytes+=payload_bytes;
    if (g_unsynced_upload_bytes>=(g_fast_loading?kFastLoadingSyncBytes:kRenderSyncBytes)) {
        const auto* sync=Contract("texture.get_native_texture_ptr");
        void* native_texture=nullptr;
        if (sync && sync->resolved) {
            InvokeValue(sync,object,nullptr,native_texture);
            if (g_construction) ++g_construction->render_syncs;
        }
        g_unsynced_upload_bytes=0;
    }
    if (g_construction) {
        ++g_construction->texture_submitted;
        g_construction->texture_payload_bytes+=payload_bytes;
        g_construction->largest_texture_payload_bytes=std::max(
            g_construction->largest_texture_payload_bytes,payload_bytes);
        g_construction->RecordUpload(payload_bytes);
    }

    int32_t built_width = -1;
    int32_t built_height = -1;
    int32_t built_graphics_format = -1;
    InvokeValue(Contract("texture.get_width"), object, nullptr, built_width);
    InvokeValue(Contract("texture.get_height"), object, nullptr, built_height);
    InvokeValue(Contract("texture.get_graphics_format"), object, nullptr,
        built_graphics_format);
    int32_t expected_graphics_format=-1;
    if (!SlotGraphicsFormat(info.create_format,info.create_srgb,expected_graphics_format) ||
        built_graphics_format!=expected_graphics_format || built_width != width || built_height != height) {
        Log("  built texture t=" + texture.name + " is " +
            std::to_string(built_width) + "x" + std::to_string(built_height) +
            ", expected " + std::to_string(width) + "x" +
            std::to_string(height) + "; refusing it.");
        DestroyUnityObject(object);
        return nullptr;
    }

    if (auto* set_name = Contract("object.set_name");
        set_name && set_name->resolved && g_host->string_new) {
        const std::string name = "BetterEndfieldNext.T." + texture.name;
        void* managed = NewString(name.c_str());
        void* parameters[1]{managed};
        InvokeVoid(set_name, object, parameters);
    }

    // One detail line per transaction (the first texture) plus a census that the
    // transaction summary prints. A 30-texture material package used to write 30
    // near-identical lines, each one a log-file open before the logger held its
    // handle; the format/size of the first build is what actually gets read.
    if (g_construction) {
        ++g_construction->texture_formats[{info.create_format, info.create_srgb != 0}];
        if (g_construction->texture_detailed++ == 0)
            Log("  built t=" + texture.name + " " + std::to_string(built_width) + "x" +
                std::to_string(built_height) + " mips=" +
                std::to_string(info.mip_count) + " " +
                TextureFormatName(info.create_format) +
                (info.create_srgb != 0 ? " sRGB" : " linear") + " graphicsFormat=" +
                std::to_string(built_graphics_format) + " (first of this transaction)");
    } else {
        Log("  built t=" + texture.name + " " + std::to_string(built_width) + "x" +
            std::to_string(built_height) + " mips=" +
            std::to_string(info.mip_count) + " " +
            TextureFormatName(info.create_format) +
            (info.create_srgb != 0 ? " sRGB" : " linear") + " graphicsFormat=" +
            std::to_string(built_graphics_format));
    }
    RememberGeneratedTexture(object,BemTextureContentIdentity(bem,index));
    return object;
}

// CRC-32 (the IEEE polynomial), matching zlib.crc32 that the converter uses.
uint32_t Crc32(std::string_view text) {
    uint32_t crc = 0xFFFFFFFFu;
    for (char character : text) {
        crc ^= static_cast<unsigned char>(character);
        for (int bit = 0; bit < 8; ++bit) {
            crc = (crc >> 1) ^ (0xEDB88320u & (0u - (crc & 1u)));
        }
    }
    return ~crc;
}


bool SetRendererEnabled(void* renderer, bool enabled) {
    MethodContract* setter = Contract("renderer.set_enabled");
    if (!setter || !setter->resolved) return false;
    uint8_t value = enabled ? 1 : 0;
    void* parameters[1]{&value};
    return InvokeVoid(setter, renderer, parameters);
}

bool GetRendererEnabled(void* renderer, bool& enabled) {
    uint8_t value = 0;
    if (!InvokeValue(Contract("renderer.get_enabled"), renderer, nullptr,
            value)) {
        return false;
    }
    enabled = value != 0;
    return true;
}

void DestroyUnityObject(void* object) {
    if (!object) return;
    int32_t destroy_id = 0;
    if (InvokeValue(Contract("object.instance_id"), object, nullptr, destroy_id) && destroy_id != 0) {
        g_dynamic_meshes.erase(destroy_id);
    }
    if (g_construction) {
        auto& assets = g_construction->assets;
        auto it = std::find(assets.begin(),assets.end(),object);
        if (it == assets.end()) return;
        *it = nullptr;
    }
    if (IsNativeObjectAlive(object)) {
        void* args[]{object};
        InvokeVoid(Contract("object.destroy"),nullptr,args);
    }
}
ConstructionScope::~ConstructionScope() {
    if (texture_constructed || mesh_submitted || texture_live_reuse || texture_dedup_hits) {
        try {
            // "BC7 sRGB x8,RGBA32 linear x2" carries the same information the
            // per-texture detail lines used to, in one field.
            std::string formats;
            for (const auto& [key,count] : texture_formats) {
                if (!formats.empty()) formats += ',';
                formats += TextureFormatName(key.first);
                formats += key.second ? " sRGB x" : " linear x";
                formats += std::to_string(count);
            }
            Log("Model upload transaction resource="+resource_name+
                " mode="+delivery_mode+
                " published="+(published?"true":"false")+
                " textureRefs="+std::to_string(texture_references)+
                " textureDedupHits="+std::to_string(texture_dedup_hits)+
                " textureDedupSavedBytes="+std::to_string(texture_dedup_bytes)+
                " textureLiveReuse="+std::to_string(texture_live_reuse)+
                " textureLiveReuseBytes="+std::to_string(texture_live_reuse_bytes)+
                " decodedPeakBytes="+std::to_string(decoded_peak_bytes)+
                " uploadFrames="+std::to_string(upload_frames)+
                " maxFrameUploadBytes="+std::to_string(max_frame_upload_bytes)+
                " textureConstructed="+std::to_string(texture_constructed)+
                " textureSubmitted="+std::to_string(texture_submitted)+
                " texturePayloadBytes="+std::to_string(texture_payload_bytes)+
                " largestTexturePayloadBytes="+std::to_string(largest_texture_payload_bytes)+
                " renderSyncs="+std::to_string(render_syncs)+
                " meshSubmitted="+std::to_string(mesh_submitted)+
                " meshPayloadBytes="+std::to_string(mesh_payload_bytes)+
                " textureFormats="+(formats.empty()?std::string("-"):formats)+
                " elapsedMs="+std::to_string(GetTickCount64()-started_ms));
        } catch (...) { /* Diagnostics must not interrupt rollback/unwinding. */ }
    }
#if defined(__ANDROID__)
    if (texture_constructed) ObserveAndroidTextureStreamingAfterUpload(
        published,texture_constructed,texture_payload_bytes,texture_dedup_bytes,texture_live_reuse_bytes);
#endif
    // No long-lived GC roots or DontUnloadUnusedAsset flags after delivery.
    auto* restore=attached?previous:g_construction;
    g_construction = nullptr; // Cleanup must not allocate more temporary roots.
    if (!published) {
        for (size_t i=assets.size();i>0;--i) {
            try { if (assets[i-1]) DestroyUnityObject(assets[i-1]); }
            catch (...) { /* Preserve shutdown/exception unwinding. */ }
        }
    }
    for (const auto& root : roots) if (root.second) g_host->gchandle_free(g_host->context,root.second);
    g_construction = restore;
}
struct SavedOriginalBinding;
struct RendererMaterialCacheSnapshot;
struct PreparedBinding {
    mutable std::shared_ptr<RendererMaterialCacheSnapshot> material_cache;
    GenericMatching::ReceiverKey receiver_key;
    uint32_t component_id=0;
    void* renderer=nullptr;
    void* original_mesh=nullptr;
    void* custom_mesh=nullptr;
    void* original_materials=nullptr;
    void* custom_materials=nullptr;
    bool copied_donor_materials=false;
    void* original_bones=nullptr;
    void* custom_bones=nullptr;
    // Rollback fields above describe the currently bound package. Construction
    // always uses the pristine donor retained by experimental hot switching.
    void* donor_mesh=nullptr;
    void* donor_materials=nullptr;
    void* donor_bones=nullptr;
    bool donor_enabled=true;
    std::shared_ptr<SavedOriginalBinding> saved_original;
    std::vector<std::string> bone_names;
    std::vector<BemMaterialOverrides> material_overrides;
    void* visual_root=nullptr;
    std::string visual_root_path;
    Float3 visual_original{},visual_custom{},visual_pristine{};
#if defined(_WIN32)
    // Set only after the actual pristine donor passes the complete, bounded
    // Snowshine 1.6.3 contract; it also applies when this part is kept/hidden.
    bool aurora_legacy_163=false;
#endif
    bool original_enabled=true;
    bool custom_enabled=true;
#if defined(__ANDROID__)
    bool change_shadow=false;
    int32_t original_shadow=0,custom_shadow=0;
    void* original_shadow_mesh=nullptr;
    void* custom_shadow_mesh=nullptr;
    struct SiblingRenderer {
        void* renderer=nullptr;
        void* original_mesh=nullptr;
        void* original_materials=nullptr;
        void* original_bones=nullptr;
    };
    std::vector<SiblingRenderer> sibling_renderers;
#endif
};
void* DonorMesh(const PreparedBinding& b) { return b.donor_mesh?b.donor_mesh:b.original_mesh; }
void* DonorMaterials(const PreparedBinding& b) { return b.donor_materials?b.donor_materials:b.original_materials; }
void* DonorBones(const PreparedBinding& b) { return b.donor_bones?b.donor_bones:b.original_bones; }
bool ResolveMaterialCopyDonor(const PreparedBinding& binding,void*& donor);
std::string DescribeBindingObject(void* object) {
    if (!object) return "null";
    if (!IsNativeObjectAlive(object)) return "dead";
    int32_t id=0;
    InvokeValue(Contract("object.instance_id"),object,nullptr,id);
    return std::to_string(id)+":"+ObjectName(object).substr(0,120);
}
std::string DescribeRendererVisibility(void* renderer) {
    bool enabled=false;
    std::string state=" enabled=";
    state+=GetRendererEnabled(renderer,enabled)?(enabled?"1":"0"):"unavailable";
    void* object=Invoke(Contract("component.get_game_object"),renderer,nullptr,false);
    for (const auto& entry:{std::pair{" activeSelf=","audit.active_self"},
            std::pair{" activeHierarchy=","audit.active_hierarchy"}}) {
        uint8_t active=0;
        state+=entry.first;
        state+=object && InvokeValue(Contract(entry.second),object,nullptr,active)?
            (active?"1":"0"):"unavailable";
    }
    return state;
}
bool UseSavedOriginal(void* asset,PreparedBinding& binding,bool require_materials=true,bool restoring_original=false);
struct GenericRendererCandidate;
bool ReadGenericPristineMesh(const CharacterAdapter&,void*,const GenericRendererCandidate&,GenericMatching::MeshIdentity&);
#include "generic_model_matcher.inc"
#if defined(_WIN32)
bool ValidateAuroraLegacyParts(void* asset,const GenericMatching::ReceiverKey& key,uint32_t component,
    void* mesh,void* material,std::span<void* const> bones) {
    GenericMatching::MeshIdentity identity;
    int32_t vertices=0;
    if (!AuroraLegacy::IsLegacyTarget(key.character,key.resource,component,ObjectName(mesh),AuroraLegacy::kLegacyIndices,false) ||
        !ReadGenericMeshIdentity(mesh,GenericMatching::DonorOrigin::Pristine,identity) ||
        !InvokeValue(Contract("mesh.get_vertex_count"),mesh,nullptr,vertices) || vertices<0) return false;
    if (!IsNativeObjectAlive(material) || ObjectName(material)!=AuroraLegacy::kMaterialName ||
        !IsNativeObjectAlive(Invoke(Contract("material.get_shader"),material,nullptr))) return false;
    void* poses=Invoke(Contract("mesh.get_bindposes"),mesh,nullptr);
    if (bones.size()!=AuroraLegacy::kBoneCount || ArrayLength(poses)!=int(AuroraLegacy::kBoneCount)) return false;
    std::array<std::string,AuroraLegacy::kBoneCount> names,paths;
    std::array<AuroraLegacy::BoneObservation,AuroraLegacy::kBoneCount> observed;
    for (size_t i=0;i<AuroraLegacy::kBoneCount;++i) {
        void* bone=bones[i];Matrix4x4Raw matrix{};
        if (!IsNativeObjectAlive(bone) || !ResourceRelativePath(asset,bone,paths[i]) ||
            !Unbox(ArrayValue(poses,int(i)),matrix)) return false;
        names[i]=ObjectName(bone);
        observed[i].name=names[i];observed[i].path=paths[i];
        std::copy(std::begin(matrix.m),std::end(matrix.m),observed[i].bindpose.begin());
    }
    return AuroraLegacy::ValidateDonor(identity.name,key.path,identity.indices,uint32_t(vertices),observed);
}
bool ValidateAuroraLegacyDonor(void* asset,PreparedBinding& binding) {
    binding.aurora_legacy_163=false;
    void* source_bones=DonorBones(binding);void* materials=DonorMaterials(binding);
    if (ArrayLength(source_bones)!=int(AuroraLegacy::kBoneCount) || ArrayLength(materials)!=1) return false;
    std::array<void*,AuroraLegacy::kBoneCount> bones;
    for (size_t i=0;i<bones.size();++i) bones[i]=ArrayValue(source_bones,int(i));
    binding.aurora_legacy_163=ValidateAuroraLegacyParts(asset,binding.receiver_key,binding.component_id,
        DonorMesh(binding),ArrayValue(materials,0),bones);
    return binding.aurora_legacy_163;
}
#endif
bool MakeReceiverKey(const CharacterAdapter& adapter,void* asset,void* renderer,GenericMatching::ReceiverKey& key) {
    std::string resource=ObjectName(asset),path;
    resource=std::string(ResourceBaseName(resource));
    if (!IsModelRenderer(renderer) || (!adapter.explicit_resource && IsStaticRenderer(renderer)) ||
        (resource!=adapter.world_resource && resource!=adapter.ui_resource) || !ResourceRelativePath(asset,renderer,path)) return false;
    const auto region=GenericMatching::ClassifyReceiver(path);
    const bool declared=std::any_of(adapter.components.begin(),adapter.components.end(),[&](const auto& identity){
        if (identity.static_mesh == IsStaticRenderer(renderer)) {
            std::string_view id_path(identity.receiver_path ? identity.receiver_path : "");
            if (path == id_path) return true;
#if defined(__ANDROID__)
            if (GenericMatching::SiblingLodPathMatches(id_path, path)) return true;
#endif
        }
        return false;
    });
    if (adapter.explicit_resource && !declared && region!=GenericMatching::Region::MobileProxy) return false;
    key={adapter.id,std::move(resource),path,adapter.explicit_resource?GenericMatching::Region::Explicit:region};
    return (adapter.explicit_resource || !path.empty()) && key.region!=GenericMatching::Region::Unknown;
}
bool MaterialCopyCompatible(void* source,void* copy) {
    void* shader=Invoke(Contract("material.get_shader"),source,nullptr);
    return shader && IsNativeObjectAlive(shader) && Invoke(Contract("material.get_shader"),copy,nullptr)==shader;
}
// Property total for the per-component summary. Section 12 quotas allow up to
// 352 properties on one draw under skip_validation, and a component can hold
// 256 draws, so the summary reports one line per component instead of one per
// material: every extra line used to cost a log-file open (see
// worktree/audit/bem15-performance-2026-10-25.md section 11).
size_t OverridePropertyCount(const BemMaterialOverrides& p) {
    return p.floats.size() + p.colors.size() + p.vectors.size() +
           p.disable_keywords.size() + p.enable_keywords.size();
}
bool ApplyMaterialOverrides(void* material, const BemMaterialOverrides& p, bool skip_validation = false) {
    if (!material) return false;
    for (const auto& [name, value] : p.floats) {
        void* key = NewString(name.c_str());
        void* args[]{key, (void*)&value};
        if (!InvokeVoid(Contract("material.set_float"), material, args)) {
            if (!skip_validation) return false;
        }
    }
    for (const auto& [name, value] : p.colors) {
        void* key = NewString(name.c_str());
        void* args[]{key, (void*)value.data()};
        if (!InvokeVoid(Contract("material.set_color"), material, args)) {
            if (!skip_validation) return false;
        }
    }
    for (const auto& [name, value] : p.vectors) {
        void* key = NewString(name.c_str());
        void* args[]{key, (void*)value.data()};
        if (!InvokeVoid(Contract("material.set_vector"), material, args)) {
            if (!skip_validation) return false;
        }
    }
    for (const auto& name : p.disable_keywords) {
        void* key = NewString(name.c_str());
        void* args[]{key};
        if (!InvokeVoid(Contract("material.disable_keyword"), material, args)) {
            if (!skip_validation) return false;
        }
    }
    for (const auto& name : p.enable_keywords) {
        void* key = NewString(name.c_str());
        void* args[]{key};
        if (!InvokeVoid(Contract("material.enable_keyword"), material, args)) {
            if (!skip_validation) return false;
        }
    }
    return true;
}
bool CopyMaterials(PreparedBinding& binding,bool skip_validation=false) {
    if (!binding.original_materials) binding.original_materials=Invoke(Contract("renderer.get_shared_materials"),binding.renderer,nullptr);
    void* donor=nullptr;
    if (!ResolveMaterialCopyDonor(binding,donor)) return false;
    const int count=ArrayLength(donor);
    if (count<=0 || (!skip_validation && count>256)) return false;
    binding.custom_materials=Invoke(Contract("array.clone"),donor,nullptr);
    if (!binding.custom_materials || ArrayLength(binding.custom_materials)!=count) return false;
    for (int i=0;i<count;++i) {
        void* source=ArrayValue(donor,i);
        void* copy=source?NewAsset(g_material_class.class_info):nullptr;
        void* ctor[]{source}; void* slot[]{copy,&i};
        if (!copy || !InvokeVoid(Contract("material.copy"),copy,ctor) || !MaterialCopyCompatible(source,copy)) return false;
        if (i < static_cast<int>(binding.material_overrides.size()) && !binding.material_overrides[i].empty()) {
            if (!ApplyMaterialOverrides(copy, binding.material_overrides[i], skip_validation)) {
                if (!skip_validation) return false;
            }
        }
        if (!InvokeVoid(Contract("array.set_value"),binding.custom_materials,slot) ||
            ArrayValue(binding.custom_materials,i)!=copy) return false;
    }
    binding.copied_donor_materials=true;
    return true;
}
void* NewArrayLike(void* original, int count,bool skip_validation=false) {
    if (!original || count<=0 || (!skip_validation && count>256) || !g_object_class || !g_array_new_specific) return nullptr;
    void* type=g_object_class(original);
    void* result=type?RootTemporary(g_array_new_specific(type,static_cast<uintptr_t>(count))):nullptr;
    return ArrayLength(result)==count?result:nullptr;
}
bool SetArrayValue(void* array, int index, void* object) {
    void* args[]{object,&index};
    return array && object && InvokeVoid(Contract("array.set_value"),array,args);
}
const PreparedBinding* FindPrepared(const std::vector<PreparedBinding>& bindings,uint32_t id) {
    for (const auto& binding:bindings) if (binding.component_id==id) return &binding;
    return nullptr;
}
thread_local std::array<void*,2> g_verified_mesh_space_roots{};
bool ReceiverLocalMeshMatrix(void* renderer,Matrix4x4Raw& matrix) {
    void* transform=Invoke(Contract("component.get_transform"),renderer,nullptr);
    if (!transform || !InvokeValue(Contract("transform.local_to_world"),transform,nullptr,matrix)) return false;
    if (!g_verified_mesh_space_roots[0]) return true;
    for (void* root:g_verified_mesh_space_roots) {
        void* root_transform=Invoke(Contract("game_object.get_transform"),root,nullptr);
        void* parent=transform;std::unordered_set<void*> visited;
        for (unsigned depth=0;parent && depth<128 && visited.insert(parent).second;++depth) {
            if (parent==root_transform) {
                Matrix4x4Raw inverse{},local{};
                if (!InvokeValue(Contract("transform.world_to_local"),root_transform,nullptr,inverse)) return false;
                for(int col=0;col<4;++col) for(int row=0;row<4;++row) for(int k=0;k<4;++k)
                    local.m[col*4+row]+=inverse.m[k*4+row]*matrix.m[col*4+k];
                matrix=local;return true;
            }
            parent=Invoke(Contract("transform.get_parent"),parent,nullptr);
        }
    }
    return false;
}
// Set while a hot switch rebinds a scene instance cloned from a committed template.
thread_local bool g_instance_rebind_active=false;
void LogMeshSpaceDifference(void* ui,void* world) {
    static unsigned logged=0;
    if (logged>=8) return;
    ++logged;
    Matrix4x4Raw a{},b{};
    const bool ra=ReceiverLocalMeshMatrix(ui,a),rb=ReceiverLocalMeshMatrix(world,b);
    std::string text="Mesh space diff "+ObjectName(world)+" uiOk="+std::to_string(ra)+" worldOk="+std::to_string(rb)+" ui=[";
    for (int i=0;i<16;++i) text+=(i?",":"")+std::to_string(a.m[i]);
    text+="] world=[";
    for (int i=0;i<16;++i) text+=(i?",":"")+std::to_string(b.m[i]);
    Log(text+"]");
}
bool SameMeshSpace(void* a,void* b) {
    if (a==b) return true;
    Matrix4x4Raw ma{},mb{};
    if (!ReceiverLocalMeshMatrix(a,ma) || !ReceiverLocalMeshMatrix(b,mb)) return false;
    for (int i=0;i<16;++i) if (!std::isfinite(ma.m[i]) || !std::isfinite(mb.m[i]) ||
        std::abs(ma.m[i]-mb.m[i])>0.0001f) return false;
    return true;
}
// Use the actual donor Transform/bindpose pair. Numeric ranges alone do not
// prove bone identity. Unequal mesh spaces need an explicit geometry-space
// conversion, which this bounded importer deliberately does not infer.
// Single Mesh construction entry for the synchronous transaction and the
// frame-budgeted Job. Offline tests substitute it; production never changes it.
using ModelMeshBuildFn=bool(*)(const BemComponent&,void*,void*&,void*);
bool BuildModelMesh(const BemComponent& component,void* source_mesh,void*& new_mesh,void* poses) {
    return BuildMeshFromComponent(component,source_mesh,new_mesh,poses);
}
ModelMeshBuildFn g_model_build_mesh=&BuildModelMesh;
// validate_skin=false is only for a metadata-only Job plan whose vertex streams
// were intentionally dropped. That caller must validate the skin stream from
// the decoded payload before it builds a Mesh (see AdvanceModelJob Mesh phase).
bool PreparePalette(const BemComponent& component,PreparedBinding& target,
    const std::vector<PreparedBinding>& bindings,void*& poses,bool skip_validation=false,bool validate_skin=true) {
    if (component.static_mesh) {
        poses=nullptr; target.custom_bones=nullptr;
        return IsStaticRenderer(target.renderer) && component.bones.empty() && component.bone_names.empty();
    }
    void* template_poses=Invoke(Contract("mesh.get_bindposes"),DonorMesh(target),nullptr);
    target.custom_bones=NewArrayLike(DonorBones(target),static_cast<int>(component.bones.size()),skip_validation);
    poses=NewArrayLike(template_poses,static_cast<int>(component.bones.size()),skip_validation);
    if (!target.custom_bones || !poses) return false;
    for (size_t i=0;i<component.bones.size();++i) {
        const auto& ref=component.bones[i]; const auto* donor=FindPrepared(bindings,ref.component);
        if (!donor || (!skip_validation && !SameMeshSpace(target.renderer,donor->renderer))) {
            Log("Merged palette donor missing or mesh spaces differ."); return false;
        }
        void* donor_poses=Invoke(Contract("mesh.get_bindposes"),DonorMesh(*donor),nullptr);
        uint32_t donor_index=ref.index;
#if defined(_WIN32)
        if (donor->aurora_legacy_163) {
            if (donor->component_id!=1 || i>=component.bone_names.size()) return false;
            const auto mapped=AuroraLegacy::RemapBone(ref.index,component.bone_names[i]);
            if (!mapped) {Log("Aurora legacy palette source does not match the verified old bone slot.");return false;}
            donor_index=*mapped;
        }
#endif
        if (donor_index>=static_cast<uint32_t>(ArrayLength(DonorBones(*donor))) ||
            donor_index>=static_cast<uint32_t>(ArrayLength(donor_poses))) return false;
        void* bone=ArrayValue(DonorBones(*donor),donor_index);
        void* pose=ArrayValue(donor_poses,donor_index); Matrix4x4Raw matrix{};
        // BEM 1.2 aliases cover a bone this resource names differently (e.g. a
        // world-skeleton typo); the palette still binds the donor's bone object.
        if (!IsNativeObjectAlive(bone) || (!skip_validation && !component.BoneNameMatches(i,ObjectName(bone))) || !Unbox(pose,matrix)) return false;
        float magnitude=0;
        for (float f:matrix.m) { if (!std::isfinite(f)) return false; magnitude+=std::abs(f); }
        if (!magnitude || !SetArrayValue(target.custom_bones,static_cast<int>(i),bone) ||
            !SetArrayValue(poses,static_cast<int>(i),pose)) return false;
        target.bone_names.push_back(ObjectName(bone));
    }
    if (!validate_skin) return true;
    std::vector<uint8_t> counts; std::vector<BoneWeight1Raw> weights;
    return DecodeComponentSkin(component,counts,weights);
}
void LogTexturePinFailure(const char* reason,void* material,
    const std::vector<MaterialTextureSlot>& slots,const std::string& expected) {
    size_t matches=0; void* first=nullptr; bool same_object=true;
    for (const auto& slot:slots) if (ObjectName(slot.texture)==expected) {
        if (!first) first=slot.texture;
        else if (first!=slot.texture) same_object=false;
        ++matches;
    }
    std::string message=std::string(reason)+" material="+ObjectName(material)+
        " texture="+expected+" matchingSlots="+std::to_string(matches)+
        " sameTextureObject="+(matches?(same_object?"true":"false"):"n/a")+" slots=";
    size_t described=0;
    for (const auto& slot:slots) {
        const auto name=ObjectName(slot.texture);
        if (matches && name!=expected) continue;
        if (described++>=12) { message+=" ..."; break; }
        message+=" ["+std::to_string(slot.slot_id)+":"+name+" "+
            std::to_string(slot.width)+"x"+std::to_string(slot.height)+
            " format="+std::to_string(slot.graphics_format)+"]";
    }
    Log(message);
}
using DeferredTextureFn=bool(*)(void*,size_t,void*,const std::vector<int32_t>&);
thread_local DeferredTextureFn g_deferred_texture=nullptr;
// One construction transaction's texture uploads. The same selected BEM entry
// with the same donor sampler state is built and uploaded ONCE and shared by
// every material slot/component (world and UI of one load included). Before a
// build, a texture with the same immutable content that is still bound to a
// live published receiver of this role is reused; nothing idle is retained.
struct TextureTransactionCache {
    std::map<std::pair<size_t,std::string>,void*> built;
    const CharacterAdapter* adapter=nullptr;
    bool live_indexed=false;
    std::unordered_map<std::string,void*> live;
};
bool SameAdapter(const CharacterAdapter& a,const CharacterAdapter& b);
void IndexLivePublishedTextures(const CharacterAdapter& adapter,std::unordered_map<std::string,void*>& live);
// A live candidate must still match the expected size/format and the donor's
// exact sampler state; otherwise it is ignored and a new texture is built.
bool LiveTextureMatches(void* candidate,const BemTextureEntryRaw& info,const std::string& sampler) {
    int32_t width=0,height=0,format=0,expected=0;std::string candidate_sampler;
    return IsNativeObjectAlive(candidate) &&
        InvokeValue(Contract("texture.get_width"),candidate,nullptr,width) && width==int32_t(info.width) &&
        InvokeValue(Contract("texture.get_height"),candidate,nullptr,height) && height==int32_t(info.height) &&
        InvokeValue(Contract("texture.get_graphics_format"),candidate,nullptr,format) &&
        SlotGraphicsFormat(info.create_format,info.create_srgb,expected) && format==expected &&
        TextureSamplerIdentity(candidate,candidate_sampler) && candidate_sampler==sampler;
}
void* FindLivePublishedTexture(TextureTransactionCache& cache,const BemPocData& bem,size_t index,const std::string& sampler) {
    if (!cache.adapter || g_generated_texture_identity.empty()) return nullptr;
    const auto identity=BemTextureContentIdentity(bem,index);
    if (identity.empty()) return nullptr;
    if (!cache.live_indexed) { cache.live_indexed=true; IndexLivePublishedTextures(*cache.adapter,cache.live); }
    const auto found=cache.live.find(identity);
    if (found==cache.live.end() || !LiveTextureMatches(found->second,bem.textures[index].info,sampler)) return nullptr;
    return RootTemporary(found->second); // Borrowed: never in this scope's assets[].
}
bool ApplyTextureMask(void* copy,uint64_t mask,const BemPocData& bem,
    TextureTransactionCache& texture_cache) {
        const auto slots=ReadMaterialTextureSlots(copy);
        std::vector<int32_t> assigned;
        for (size_t t=0;t<bem.textures.size();++t) if (mask&(uint64_t{1}<<t)) {
            const auto& tex=bem.textures[t];
            const auto pins=MatchTexturePins(slots,tex.original_name,bem.skip_validation,ObjectName);
            if (pins.status==TexturePinStatus::Ambiguous) {
                LogTexturePinFailure("Ambiguous v25 texture name pin.",copy,slots,tex.original_name); return false;
            }
            if (pins.status==TexturePinStatus::Missing) {
                LogTexturePinFailure(bem.skip_validation?"Developer mode: unmatched texture left unchanged.":"Texture name pin missing.",copy,slots,tex.original_name);
                if(bem.skip_validation) continue;
                return false;
            }
            const auto& matches=pins.slots;
            const auto* match=matches.front();
            for (const auto* matched:matches) {
                if (!bem.skip_validation && std::find(assigned.begin(),assigned.end(),matched->slot_id)!=assigned.end()) {
                    LogTexturePinFailure("Duplicate texture slot assignment.",copy,slots,tex.original_name); return false;
                }
                assigned.push_back(matched->slot_id);
            }
            if (g_deferred_texture) {
                std::vector<int32_t> ids;
                for (const auto* matched:matches) ids.push_back(matched->slot_id);
                if (!g_deferred_texture(copy,t,match->texture,ids)) return false;
                continue;
            }
            // Unreadable sampler state: keep the source Texture identity instead.
            std::string sampler;
            if (!TextureSamplerIdentity(match->texture,sampler)) {
                sampler="source:"; const auto source=reinterpret_cast<uintptr_t>(match->texture);
                sampler.append(reinterpret_cast<const char*>(&source),sizeof(source));
            }
            if (g_construction) ++g_construction->texture_references;
            void*& texture=texture_cache.built[{t,sampler}];
            if (texture) {
                if (g_construction) { ++g_construction->texture_dedup_hits; g_construction->texture_dedup_bytes+=tex.info.data_size; }
            } else if ((texture=FindLivePublishedTexture(texture_cache,bem,t,sampler))) {
                if (g_construction) { ++g_construction->texture_live_reuse; g_construction->texture_live_reuse_bytes+=tex.info.data_size; }
            } else {
                texture=CreateTextureFromBem(bem,t);
                if (!texture || !CopySamplerState(match->texture,texture)) return false;
#if defined(__ANDROID__)
                betterendfieldnext::AndroidAuditTextureColorSpace(match->texture,texture,tex.original_name);
                betterendfieldnext::AndroidAuditNormalTexture(match->texture,tex.original_name);
#endif
            }
            for (const auto* matched:matches) {
                int32_t slot=matched->slot_id; void* args[]{&slot,texture}; void* read[]{&slot};
                if (!InvokeVoid(Contract("material.set_texture_by_id"),copy,args) ||
                    Invoke(Contract("material.get_texture_by_id"),copy,read)!=texture) return false;
            }
        }
    return true;
}
bool PrepareDrawMaterials(const BemComponent& component,PreparedBinding& target,
    const std::vector<PreparedBinding>& bindings,const BemPocData& bem,
    TextureTransactionCache* transaction_textures=nullptr) {
    TextureTransactionCache local_textures;
    auto& texture_cache=transaction_textures?*transaction_textures:local_textures;
    auto fail=[&](const char* reason,size_t draw_index,const PreparedBinding* =nullptr,
            void* =nullptr,void* =nullptr) {
        Log(std::string("Material preparation failed: reason=")+reason+
            " component="+std::to_string(component.info.component_id)+" draw="+std::to_string(draw_index));
        return false;
    };
    void* target_base=nullptr;
    if (!ResolveMaterialCopyDonor(target,target_base)) return fail("donor-base",component.draws.size());
    target.custom_materials=NewArrayLike(target_base,static_cast<int>(component.draws.size()),bem.skip_validation);
    if (!target.custom_materials) return fail("new-array",component.draws.size());
    std::unordered_map<const PreparedBinding*,void*> donor_bases;
    size_t override_draws=0,override_properties=0;
    for (size_t i=0;i<component.draws.size();++i) {
        const auto& draw=component.draws[i]; const auto* donor=FindPrepared(bindings,draw.material_component);
        if (!donor) return fail("donor-component",i);
        void* donor_base=nullptr;
        if (const auto cached=donor_bases.find(donor);cached!=donor_bases.end()) donor_base=cached->second;
        else {
            if (!ResolveMaterialCopyDonor(*donor,donor_base)) return fail("donor-base",i,donor);
            donor_bases.emplace(donor,donor_base);
        }
        if (draw.material_slot>=static_cast<uint32_t>(ArrayLength(donor_base)))
            return fail("donor-slot",i,donor);
        void* material=ArrayValue(donor_base,draw.material_slot);
        if (!material) return fail("null-material",i,donor);
        if (!bem.skip_validation && ObjectName(material)!=component.material_names[i])
            return fail("material-name",i,donor,material);
        void* copy=NewAsset(g_material_class.class_info); void* ctor[]{material};
        if (!copy) return fail("new-material",i,donor,material);
        if (!InvokeVoid(Contract("material.copy"),copy,ctor)) return fail("copy-constructor",i,donor,material,copy);
        if (!MaterialCopyCompatible(material,copy)) return fail("copy-shader",i,donor,material,copy);
        if (!SetArrayValue(target.custom_materials,static_cast<int>(i),copy))
            return fail("set-array",i,donor,material,copy);
#if defined(__ANDROID__)
        if (!betterendfieldnext::AndroidAuditMaterialCopy(material,copy)) return fail("android-copy",i,donor,material,copy);
#endif
        if (!ApplyTextureMask(copy,draw.textures,bem,texture_cache)) return fail("texture-mask",i,donor,material,copy);
        if (i < component.draw_material_overrides.size() && !component.draw_material_overrides[i].empty()) {
            const auto& overrides=component.draw_material_overrides[i];
            if (!ApplyMaterialOverrides(copy, overrides, bem.skip_validation)) {
                if (!bem.skip_validation) return fail("material-overrides",i,donor,material,copy);
            }
            ++override_draws;
            override_properties+=OverridePropertyCount(overrides);
        }
    }
    if (override_draws)
        Log("Applied BEM 1.5 material overrides: C"+std::to_string(component.info.component_id)+
            " draws="+std::to_string(override_draws)+
            " properties="+std::to_string(override_properties));
    return true;
}
bool PrepareKeepMaterials(const BemComponent& component,PreparedBinding& target,const BemPocData& bem,
    TextureTransactionCache* transaction_textures=nullptr) {
    if (!CopyMaterials(target,bem.skip_validation)) return false;
    void* donor_base=nullptr;
    if (!ResolveMaterialCopyDonor(target,donor_base)) return false;
    TextureTransactionCache local_textures;
    auto& texture_cache=transaction_textures?*transaction_textures:local_textures;
    for (size_t i=0;i<component.keep_material_overrides.size();++i) {
        const auto& override=component.keep_material_overrides[i];
        if (override.material_slot>=static_cast<uint32_t>(ArrayLength(donor_base)) ||
            override.material_slot>=static_cast<uint32_t>(ArrayLength(target.custom_materials))) return false;
        void* source=ArrayValue(donor_base,override.material_slot);
        void* copy=ArrayValue(target.custom_materials,override.material_slot);
        if (!source || !copy || (!bem.skip_validation && ObjectName(source)!=component.keep_material_names[i]) ||
            !ApplyTextureMask(copy,override.textures,bem,texture_cache)) return false;
    }
    // CopyMaterials applied the authored overrides slot by slot; report them as
    // one line for this component instead of one per material.
    size_t override_slots=0,override_properties=0;
    for (const auto& overrides:target.material_overrides) if (!overrides.empty()) {
        ++override_slots;
        override_properties+=OverridePropertyCount(overrides);
    }
    if (override_slots)
        Log("Applied BEM 1.5 material overrides: C"+std::to_string(component.info.component_id)+
            " slots="+std::to_string(override_slots)+
            " properties="+std::to_string(override_properties));
    return true;
}
bool SetRendererBones(void* renderer,void* bones) {
    if (!bones) return true; // v24 never changes the renderer palette.
    void* args[]{bones};
    if (!InvokeVoid(Contract("skinned.set_bones"),renderer,args)) return false;
    void* read=Invoke(Contract("skinned.get_bones"),renderer,nullptr);
    if (ArrayLength(read)!=ArrayLength(bones)) return false;
    for (int i=0;i<ArrayLength(bones);++i) if (ArrayValue(read,i)!=ArrayValue(bones,i)) return false;
    return true;
}
bool SetVisualPosition(void* root,const Float3& position);
bool PrepareVisualRoot(void* asset,const BemPocData& bem,std::vector<PreparedBinding>& bindings);
bool PrepareRendererMaterialCache(const PreparedBinding& binding,bool inspect_only=false);
bool ApplyRendererMaterialCache(const PreparedBinding& binding);
bool RestoreRendererMaterialCache(const PreparedBinding& binding);
bool ApplyPreparedBinding(PreparedBinding& binding) {
#if defined(__ANDROID__)
    if (binding.change_shadow) {
        void* mode[]{&binding.custom_shadow}; void* mesh[]{binding.custom_shadow_mesh}; int32_t read=-1;void* read_mesh=nullptr;
        if (!InvokeVoid(Contract("android.shadow_set"),binding.renderer,mode) ||
            !InvokeValue(Contract("android.shadow_get"),binding.renderer,nullptr,read) || read!=binding.custom_shadow ||
            !InvokeVoid(Contract("android.shadow_mesh_set"),binding.renderer,mesh) ||
            !ReadNullableObject(Contract("android.shadow_mesh_get"),binding.renderer,read_mesh) || read_mesh!=binding.custom_shadow_mesh) return false;
    }
#endif
    if (binding.custom_bones && !SetRendererBones(binding.renderer,binding.custom_bones)) return false;
    if (binding.custom_mesh!=binding.original_mesh && !SetSharedMesh(binding.renderer,binding.custom_mesh)) return false;
    if (!ApplyRendererMaterials(binding.renderer,binding.custom_materials)) return false;
    if (!ApplyRendererMaterialCache(binding)) return false;
    if (!SetVisualPosition(binding.visual_root,binding.visual_custom)) return false;
#if defined(__ANDROID__)
    for (const auto& sibling:binding.sibling_renderers) {
        if (sibling.renderer) {
            if (binding.custom_bones && !IsStaticRenderer(sibling.renderer)) {
                SetRendererBones(sibling.renderer,binding.custom_bones);
            }
            SetSharedMesh(sibling.renderer,binding.custom_mesh);
            ApplyRendererMaterials(sibling.renderer,binding.custom_materials);
            SetRendererEnabled(sibling.renderer,binding.custom_enabled);
        }
    }
#endif
    bool read=false;
    return SetRendererEnabled(binding.renderer,binding.custom_enabled) &&
        GetRendererEnabled(binding.renderer,read) && read==binding.custom_enabled;
}
bool RestorePreparedBinding(PreparedBinding& binding) {
    // No short-circuit: independently restore every field even if another fails.
    const bool mesh=SetSharedMesh(binding.renderer,binding.original_mesh);
    const bool bones=!binding.custom_bones || SetRendererBones(binding.renderer,binding.original_bones);
    const bool materials=ApplyRendererMaterials(binding.renderer,binding.original_materials);
    const bool material_cache=RestoreRendererMaterialCache(binding);
    bool read=false;
    const bool enabled=SetRendererEnabled(binding.renderer,binding.original_enabled) &&
        GetRendererEnabled(binding.renderer,read) && read==binding.original_enabled;
    bool shadow=true;
#if defined(__ANDROID__)
    if (binding.change_shadow) {
        void* mode[]{&binding.original_shadow}; void* proxy[]{binding.original_shadow_mesh}; int32_t read=-1;void* read_mesh=nullptr;
        const bool restored_mode=InvokeVoid(Contract("android.shadow_set"),binding.renderer,mode) &&
            InvokeValue(Contract("android.shadow_get"),binding.renderer,nullptr,read) && read==binding.original_shadow;
        const bool restored_proxy=InvokeVoid(Contract("android.shadow_mesh_set"),binding.renderer,proxy) &&
            ReadNullableObject(Contract("android.shadow_mesh_get"),binding.renderer,read_mesh) && read_mesh==binding.original_shadow_mesh;
        shadow=restored_mode && restored_proxy;
    }
    for (const auto& sibling:binding.sibling_renderers) {
        if (sibling.renderer) {
            if (sibling.original_bones && !IsStaticRenderer(sibling.renderer)) {
                SetRendererBones(sibling.renderer,sibling.original_bones);
            }
            SetSharedMesh(sibling.renderer,sibling.original_mesh);
            ApplyRendererMaterials(sibling.renderer,sibling.original_materials);
            SetRendererEnabled(sibling.renderer,binding.original_enabled);
        }
    }
#endif
    const bool visual=SetVisualPosition(binding.visual_root,binding.visual_original);
    return mesh && bones && materials && material_cache && enabled && shadow && visual;
}
bool ValidatePayloadAdapter(const CharacterAdapter& adapter,const BemPocData& bem) {
    if (adapter.explicit_resource && std::any_of(adapter.components.begin(),adapter.components.end(),[&](const auto& c){
        return !GenericMatching::ReceiverLodAgrees(c.receiver_path,adapter.receiver_lod);
    })) {
        Log("Explicit receiver path disagrees with resource LOD: "+std::string(adapter.world_resource));return false;
    }
#if !defined(__ANDROID__)
    if (adapter.explicit_resource && adapter.receiver_lod!=0) {
        Log("Explicit Windows resource requires LOD0: "+std::string(adapter.world_resource)); return false;
    }
#endif
    if (bem.components.size()!=adapter.components.size()) return false;
    for (const auto& component:bem.components) {
        if (component.info.component_id>=adapter.components.size() ||
            component.static_mesh!=adapter.components[component.info.component_id].static_mesh ||
            (!bem.skip_validation && adapter.components[component.info.component_id].indices!=component.info.original_index_count)) return false;
    }
    for (const auto& texture:bem.textures) if (texture.original_name.empty()) return false;
    return true;
}
thread_local void* g_ready_resource=nullptr;
thread_local const std::vector<PreparedBinding>* g_ready_bindings=nullptr;
bool CaptureGenericResourceBindings(const CharacterAdapter&,const BemPocData&,void*,std::vector<PreparedBinding>&);
#if defined(__ANDROID__)
bool PrepareExplicitAndroidShadows(const CharacterAdapter&,const BemPocData&,void*,std::vector<PreparedBinding>&);
#endif
bool PrepareResource(const CharacterAdapter& adapter,const BemPocData& bem,void* asset,
    std::vector<PreparedBinding>& bindings,bool capture_only=false) {
    if (!capture_only && asset==g_ready_resource && g_ready_bindings) { bindings=*g_ready_bindings; return true; }
    if (capture_only) return CaptureGenericResourceBindings(adapter,bem,asset,bindings);
    if (!CaptureGenericResourceBindings(adapter,bem,asset,bindings)) return false;
    // One upload per selected texture entry and donor sampler state for the
    // whole transaction (independent of loading_optimization). ConstructionScope
    // owns each created asset once, including on rollback; live published
    // textures are borrowed and never destroyed by this transaction.
    TextureTransactionCache transaction_textures; transaction_textures.adapter=&adapter;
    for (size_t i=0;i<bem.components.size();++i) {
        const auto& component=bem.components[i]; auto& binding=bindings[i];
        if (!(component.info.flags&kComponentFlagNoGeometry)) {
            void* poses=nullptr;
            if (!PreparePalette(component,binding,bindings,poses,bem.skip_validation)) {
                Log("PrepareResource: PreparePalette failed for C" + std::to_string(i));
                return false;
            }
            if (!g_model_build_mesh(component,DonorMesh(binding),binding.custom_mesh,poses)) {
                Log("PrepareResource: g_model_build_mesh failed for C" + std::to_string(i));
                return false;
            }
            if (!PrepareDrawMaterials(component,binding,bindings,bem,&transaction_textures)) {
                Log("PrepareResource: PrepareDrawMaterials failed for C" + std::to_string(i));
                return false;
            }
        } else {
            if (!PrepareKeepMaterials(component,binding,bem,&transaction_textures)) {
                Log("PrepareResource: PrepareKeepMaterials failed for C" + std::to_string(i));
                return false;
            }
        }
    }
    #if defined(__ANDROID__)
    if (!PrepareExplicitAndroidShadows(adapter,bem,asset,bindings)) return false;
    #endif
    return !g_construction->failed && PrepareVisualRoot(asset,bem,bindings);
}
#if defined(__ANDROID__)
bool ReadCompletedAndroidDonor(const CharacterAdapter&,const BemPocData&,void*,std::vector<PreparedBinding>&);
#include "modules/custom_model/world_resource_adapter.inc"
#endif
using WeakNewFn=uint32_t(*)(void*,bool);
using WeakTargetFn=void*(*)(uint32_t);
WeakNewFn g_weak_new=nullptr;
WeakTargetFn g_weak_target=nullptr;
struct WeakObject {
    uint32_t handle=0;
    int32_t instance_id=0;
    WeakObject()=default;
    WeakObject(const WeakObject&)=delete;
    WeakObject& operator=(const WeakObject&)=delete;
    WeakObject(WeakObject&& other) noexcept { std::swap(handle,other.handle); std::swap(instance_id,other.instance_id); }
    WeakObject& operator=(WeakObject&& other) noexcept {
        if (this!=&other) { Reset(); std::swap(handle,other.handle); std::swap(instance_id,other.instance_id); }
        return *this;
    }
    ~WeakObject() { Reset(); }
    void Reset() {
        if (handle && g_host && !g_process_terminating.load()) g_host->gchandle_free(g_host->context,handle);
        handle=0; instance_id=0;
    }
    bool Set(void* object) {
        Reset();
        if (!object || !g_weak_new || !InvokeValue(Contract("object.instance_id"),object,nullptr,instance_id)) return false;
        handle=g_weak_new(object,false); return handle!=0;
    }
    void* Get() const {
        void* object=handle && g_weak_target?RootTemporary(g_weak_target(handle)):nullptr;
        int32_t id=0;
        return object && IsNativeObjectAlive(object) &&
            InvokeValue(Contract("object.instance_id"),object,nullptr,id) && id==instance_id?object:nullptr;
    }
    // Native identity of a live object read back from the engine. A recycled
    // managed wrapper of the same Unity object (dead weak handle) still
    // matches its unique instance ID; a reused address of another object
    // does not. Lineage/completion comparisons use this, never Get()==object.
    bool Is(void* object) const {
        int32_t id=0;
        return object && instance_id!=0 && IsNativeObjectAlive(object) &&
            InvokeValue(Contract("object.instance_id"),object,nullptr,id) && id==instance_id;
    }
    // Same identity, own handle (the target may already be gone).
    void Clone(const WeakObject& other) {
        Reset(); instance_id=other.instance_id;
        if (void* object=other.Get(); object && g_weak_new) handle=g_weak_new(object,false);
    }
};
struct CpuGeometry {
    WeakObject mesh;
    std::vector<float> positions;
    std::vector<uint8_t> skin;
    std::vector<uint32_t> indices;
    std::vector<BE_CustomModelDrawV1> draws;
    uint32_t vertices=0,stride=0;
    uint64_t used=0;
    size_t Bytes() const {return positions.size()*4+skin.size()+indices.size()*4+draws.size()*sizeof(BE_CustomModelDrawV1);}
};
constexpr size_t kCpuGeometryBudget=64u*1024u*1024u;
std::vector<std::unique_ptr<CpuGeometry>> g_cpu_geometry;
size_t g_cpu_geometry_bytes=0;
uint64_t g_cpu_geometry_serial=0;
void PruneCpuGeometry() {
    std::erase_if(g_cpu_geometry,[](const auto& entry){
        if(entry->mesh.Get()) return false;
        g_cpu_geometry_bytes-=entry->Bytes();return true;
    });
}
void RememberCpuGeometry(void* mesh,const BemComponent& component) {
    // Optional bounded cache for camera masking. Cache failure never rejects a
    // valid replacement. No textures, TBN streams or strong engine references.
    try {
        const auto& info=component.info;
        const uint64_t bytes=uint64_t(info.vertex_count)*(12+info.stride2)+uint64_t(info.index_count)*4+
            std::max<size_t>(component.draws.size(),1)*sizeof(BE_CustomModelDrawV1);
        if(bytes>kCpuGeometryBudget || !info.vertex_count || (info.stride2!=4 && info.stride2!=12 && info.stride2!=32) ||
            component.streams[2].size()!=uint64_t(info.vertex_count)*info.stride2 ||
            (info.index_element_size!=2 && info.index_element_size!=4) ||
            component.indices.size()!=uint64_t(info.index_count)*info.index_element_size) return;
        constexpr uint32_t sizes[]{4,2,1,1,2,2,1,1,2,2,4,4};
        uint32_t positionStream=0,positionOffset=0; bool found=false;std::array<uint32_t,3> offsets{};
        for(const auto& attribute:component.attributes) {
            if(attribute[1]<0 || attribute[1]>=12 || attribute[2]<=0 || attribute[2]>4 || attribute[3]<0 || attribute[3]>=3) return;
            const auto stream=static_cast<uint32_t>(attribute[3]);
            if(attribute[0]==0) {
                if(found || attribute[1]!=0 || attribute[2]!=3) return;
                found=true;positionStream=stream;positionOffset=offsets[stream];
            }
            offsets[stream]+=sizes[attribute[1]]*attribute[2];
        }
        const uint32_t strides[]{info.stride0,info.stride1,info.stride2};
        if(!found || positionOffset>strides[positionStream] || 12>strides[positionStream]-positionOffset ||
            component.streams[positionStream].size()!=uint64_t(info.vertex_count)*strides[positionStream]) return;
        auto entry=std::make_unique<CpuGeometry>();
        if(!entry->mesh.Set(mesh)) return;
        PruneCpuGeometry();
        std::erase_if(g_cpu_geometry,[&](const auto& previous){
            if(previous->mesh.Get()!=mesh) return false;
            g_cpu_geometry_bytes-=previous->Bytes();return true;
        });
        while(g_cpu_geometry_bytes+bytes>kCpuGeometryBudget && !g_cpu_geometry.empty()) {
            auto oldest=std::min_element(g_cpu_geometry.begin(),g_cpu_geometry.end(),[](const auto& a,const auto& b){return a->used<b->used;});
            g_cpu_geometry_bytes-=(*oldest)->Bytes();g_cpu_geometry.erase(oldest);
        }
        entry->vertices=info.vertex_count;entry->stride=info.stride2;entry->positions.resize(size_t(info.vertex_count)*3);
        for(uint32_t vertex=0;vertex<info.vertex_count;++vertex) {
            std::memcpy(entry->positions.data()+size_t(vertex)*3,component.streams[positionStream].data()+size_t(vertex)*strides[positionStream]+positionOffset,12);
            for(size_t axis=0;axis<3;++axis) if(!std::isfinite(entry->positions[size_t(vertex)*3+axis])) return;
        }
        entry->skin=component.streams[2];entry->indices.resize(info.index_count);
        for(uint32_t index=0;index<info.index_count;++index) {
            entry->indices[index]=0;
            std::memcpy(&entry->indices[index],component.indices.data()+size_t(index)*info.index_element_size,info.index_element_size);
            if(entry->indices[index]>=info.vertex_count) return;
        }
        if(component.draws.empty()) entry->draws.push_back({0,info.index_count});
        else for(const auto& draw:component.draws) {
            if(draw.start>info.index_count || draw.count>info.index_count-draw.start) return;
            entry->draws.push_back({draw.start,draw.count});
        }
        entry->used=++g_cpu_geometry_serial;g_cpu_geometry_bytes+=entry->Bytes();g_cpu_geometry.push_back(std::move(entry));
    } catch(...) { /* The camera retains its generic path when memory is unavailable. */ }
}
struct StrongReference {
    uint32_t handle=0;
    StrongReference()=default;
    StrongReference(const StrongReference&)=delete;
    StrongReference& operator=(const StrongReference&)=delete;
    StrongReference(StrongReference&& other) noexcept { std::swap(handle,other.handle); }
    StrongReference& operator=(StrongReference&& other) noexcept {
        if (this!=&other) { Set(nullptr);std::swap(handle,other.handle); }
        return *this;
    }
    ~StrongReference() { if (handle && g_host && !g_process_terminating.load()) g_host->gchandle_free(g_host->context,handle); }
    bool Set(void* object) {
        if (handle && g_host && !g_process_terminating.load()) g_host->gchandle_free(g_host->context,handle);
        handle=object && g_host && g_host->gchandle_new?g_host->gchandle_new(g_host->context,object,0):0;
        return !object || handle!=0;
    }
    void* Get() const { return handle && g_weak_target?RootTemporary(g_weak_target(handle)):nullptr; }
};
// A weak managed array plus receiver-relative paths never retains a UI skeleton.
struct WeakBoneArray {
    uint32_t handle=0;
    ~WeakBoneArray() { if (handle && g_host && !g_process_terminating.load()) g_host->gchandle_free(g_host->context,handle); }
    bool Set(void* object) {
        if (handle && g_host) g_host->gchandle_free(g_host->context,handle);
        handle=object && g_weak_new?g_weak_new(object,false):0; return !object || handle!=0;
    }
    void* Get() const { return handle && g_weak_target?RootTemporary(g_weak_target(handle)):nullptr; }
};
// Hot switch keeps exactly one asset alive beyond the game's own references:
// the Original Mesh of a receiver that currently shows generated geometry.
// Unity's unused-asset sweep destroyed such a Mesh on device even while a
// managed GC handle held its wrapper (diag6: "saved Original Mesh is no longer
// alive"), so the pin also sets DontUnloadUnusedAsset and clears it again when
// the last pin of that Mesh is released (Original restored, record replaced or
// pruned). Original materials are never pinned: that would keep the game's
// original textures resident.
constexpr int32_t kHideFlagDontUnloadUnusedAsset=32;
struct OriginalMeshPin {
    StrongReference handle;
    int32_t instance_id=0;
    bool flag_added=false;
    OriginalMeshPin()=default;
    OriginalMeshPin(const OriginalMeshPin&)=delete;
    ~OriginalMeshPin();
};
std::unordered_map<int32_t,std::weak_ptr<OriginalMeshPin>> g_original_mesh_pins;
std::shared_ptr<OriginalMeshPin> AcquireOriginalMeshPin(void* mesh) {
    int32_t id=0;
    if (!mesh || !IsNativeObjectAlive(mesh) || !InvokeValue(Contract("object.instance_id"),mesh,nullptr,id) || !id) return {};
    if (const auto found=g_original_mesh_pins.find(id);found!=g_original_mesh_pins.end())
        if (auto pin=found->second.lock()) return pin;
    auto pin=std::make_shared<OriginalMeshPin>();
    if (!pin->handle.Set(mesh)) return {};
    pin->instance_id=id;
    const auto* get=Contract("object.get_hide_flags");const auto* set=Contract("object.set_hide_flags");
    int32_t flags=0;
    if (get && set && get->resolved && set->resolved && InvokeValue(get,mesh,nullptr,flags)) {
        if (!(flags&kHideFlagDontUnloadUnusedAsset)) {
            int32_t next=flags|kHideFlagDontUnloadUnusedAsset,read=0;void* args[]{&next};
            pin->flag_added=InvokeVoid(set,mesh,args) && InvokeValue(get,mesh,nullptr,read) &&
                (read&kHideFlagDontUnloadUnusedAsset);
        }
    } else {
        static bool logged=false;
        if (!logged) { logged=true; Log("Original Mesh pin: hideFlags unavailable; GC handle only (may not survive unloading)"); }
    }
    g_original_mesh_pins[id]=pin;
    return pin;
}
struct SavedOriginalBinding {
    // Updating the game's base-material cache replaces its references to the
    // pristine materials. Retain those references for this Original's lifetime.
    StrongReference material_cache_materials;
    WeakObject mesh;                              // identity
    std::shared_ptr<OriginalMeshPin> mesh_pin;    // only while generated geometry is shown
    std::vector<WeakObject> materials;            // weak; never pins original textures
    std::vector<bool> material_present;           // false: the original slot was null
    std::vector<std::string> material_names;      // diagnostics only
    WeakBoneArray bones;
    std::vector<std::string> bone_paths;
    bool enabled=true;
    std::string visual_root_path;
    Float3 visual_pristine{};
#if defined(__ANDROID__)
    bool change_shadow=false;
    int32_t shadow=0;
    StrongReference shadow_mesh;
    std::shared_ptr<OriginalMeshPin> shadow_mesh_pin;
#endif
    // The live Original Mesh. `current` (a receiver's Mesh) resolves it when
    // only its identity survives (unpinned and the managed wrapper recycled).
    void* ResolveMesh(void* current=nullptr) const {
        if (mesh_pin) if (void* pinned=mesh_pin->handle.Get(); pinned && IsNativeObjectAlive(pinned)) return pinned;
        if (void* object=mesh.Get()) return object;
        return current && mesh.Is(current)?current:nullptr;
    }
    void EnsurePinned(void* current=nullptr) { if (!mesh_pin) mesh_pin=AcquireOriginalMeshPin(ResolveMesh(current)); }
    bool SetMaterials(void* array) {
        materials.clear(); material_present.clear(); material_names.clear();
        const int count=ArrayLength(array);
        if (count<0 || count>256) return false;
        for (int i=0;i<count;++i) {
            void* material=ArrayValue(array,i); WeakObject weak;
            if (material && !weak.Set(material)) return false;
            materials.push_back(std::move(weak)); material_present.push_back(material!=nullptr);
            material_names.push_back(material?ObjectName(material):std::string{});
        }
        return true;
    }
    void CopyIdentity(const SavedOriginalBinding& other) {
        mesh.Clone(other.mesh); mesh_pin=other.mesh_pin;
        materials.clear(); materials.resize(other.materials.size());
        for (size_t i=0;i<other.materials.size();++i) materials[i].Clone(other.materials[i]);
        material_present=other.material_present; material_names=other.material_names;
        enabled=other.enabled; bone_paths=other.bone_paths;
        visual_root_path=other.visual_root_path; visual_pristine=other.visual_pristine;
#if defined(__ANDROID__)
        change_shadow=other.change_shadow; shadow=other.shadow;
        shadow_mesh_pin=other.shadow_mesh_pin;
#endif
    }
};
bool SetVisualPosition(void* root,const Float3& position) {
    if(!root) return true;
    void* args[]{const_cast<Float3*>(&position)};Float3 read{};
    return InvokeVoid(Contract("visual.set_local_position"),root,args) &&
        InvokeValue(Contract("visual.get_local_position"),root,nullptr,read) &&
        std::isfinite(read.x) && std::isfinite(read.y) && std::isfinite(read.z) &&
        std::abs(read.x-position.x)<1e-4f && std::abs(read.y-position.y)<1e-4f && std::abs(read.z-position.z)<1e-4f;
}
bool PrepareVisualRoot(void* asset,const BemPocData& bem,std::vector<PreparedBinding>& bindings) {
    std::string path=bem.root_offset.path;PreparedBinding* owner=nullptr;
    for(auto& b:bindings) if(b.saved_original && !b.saved_original->visual_root_path.empty()) {owner=&b;if(path.empty())path=b.saved_original->visual_root_path;break;}
    if(path.empty()) return true;
    if(owner && owner->saved_original && owner->saved_original->visual_root_path!=path) return false;
    if(bindings.empty() || !ValidVisualPath(path)) return false;
    if(!owner) owner=&bindings.front();
    void* node=Invoke(Contract("game_object.get_transform"),asset,nullptr);size_t begin=0;
    while(begin<path.size()) {
        auto end=path.find('/',begin);if(end==path.npos)end=path.size();auto part=path.substr(begin,end-begin);
        int32_t count=0;if(!node || !InvokeValue(Contract("transform.child_count"),node,nullptr,count) || count<0 || count>8192)return false;
        void* next=nullptr;for(int32_t i=0;i<count;++i) {void* args[]{&i};void* child=Invoke(Contract("transform.get_child"),node,args);
            if(child && ObjectName(child)==part) {if(next)return false;next=child;}}
        node=next;begin=end+1;
    }
    if(!node || !InvokeValue(Contract("visual.get_local_position"),node,nullptr,owner->visual_original))return false;
    owner->visual_pristine=owner->visual_original;
    if(owner->saved_original && owner->saved_original->visual_root_path==path)owner->visual_pristine=owner->saved_original->visual_pristine;
    const auto& t=bem.root_offset.translation;const auto base=owner->visual_pristine;
    owner->visual_custom={base.x+t[0],base.y+t[1],base.z+t[2]};owner->visual_root=node;owner->visual_root_path=path;
    for(float v:{base.x,base.y,base.z,owner->visual_custom.x,owner->visual_custom.y,owner->visual_custom.z})if(!std::isfinite(v))return false;
    Log("Root offset prepared path="+path+" y="+std::to_string(t[1]));return true;
}
// RenderPipeline is a managed IDisposable, not a UnityEngine.Object. It has no
// native Unity instance ID and must never go through Object.op_Implicit.
struct WeakManagedReference {
    uint32_t handle=0;
    WeakManagedReference()=default;
    WeakManagedReference(const WeakManagedReference&)=delete;
    ~WeakManagedReference() { Reset(); }
    void Reset() {
        if (handle && g_host && !g_process_terminating.load()) g_host->gchandle_free(g_host->context,handle);
        handle=0;
    }
    bool Set(void* object) { Reset(); handle=object && g_weak_new?g_weak_new(object,false):0; return handle!=0; }
    void* Get() const { return handle && g_weak_target?RootTemporary(g_weak_target(handle)):nullptr; }
};
#include "model_material_cache.inc"
using StaticFieldFn=void(*)(const void*,void*);
StaticFieldFn g_static_get=nullptr,g_static_set=nullptr;
bool SafeStaticField(StaticFieldFn call,const void* field,void* value) {
    if (!call || !field || !value) return false;
    __try { call(field,value); return true; }
    __except(EXCEPTION_EXECUTE_HANDLER) { return false; }
}
struct LodField {
    const char* name;
    const char* type;
    size_t size;
    BE_ResolvedFieldV1 resolved{};
    std::array<uint8_t,8> original{},desired{};
};
// Current EnableForceLOD0 writes this bias to the parent and art tags 0..34.
// It does not install a persistent lock: RegisterArtTagLODBias writes them again
// on quality changes. Resolve the two setters by metadata, never by native RVA.
constexpr float kForcedLodBias=1e-7f;
constexpr uint32_t kForcedLodArtTagCount=35;
std::atomic_bool g_lod_bias_locked{false};
using ParentLodBiasFn=void(__fastcall*)(float,void*);
using ArtTagLodBiasFn=void(__fastcall*)(uint32_t,float,void*);
ParentLodBiasFn g_original_parent_lod_bias=nullptr;
ArtTagLodBiasFn g_original_art_tag_lod_bias=nullptr;
void __fastcall ParentLodBias(float bias,void* method) {
    if (g_lod_bias_locked.load(std::memory_order_acquire) && bias!=kForcedLodBias) {
        bias=kForcedLodBias;
    }
    g_original_parent_lod_bias(bias,method);
}
void __fastcall ArtTagLodBias(uint32_t tag,float bias,void* method) {
    if (tag<kForcedLodArtTagCount && g_lod_bias_locked.load(std::memory_order_acquire) && bias!=kForcedLodBias) {
        bias=kForcedLodBias;
    }
    g_original_art_tag_lod_bias(tag,bias,method);
}
#if defined(__ANDROID__)
using RegisterLodBiasFn=void(*)(void*,void*);
RegisterLodBiasFn g_original_register_bias=nullptr;
thread_local bool g_in_register_bias=false;
void AndroidRegisterLodBias(void* pipeline,void* method) {
    g_original_register_bias(pipeline,method);
    if (!g_lod_bias_locked.load(std::memory_order_acquire) || g_in_register_bias) return;
    g_in_register_bias=true;
    // Compiled RegisterArtTagLODBias calls native icalls directly, bypassing
    // the managed setters. Reapply only after the game's registration returns.
    const bool ready=InvokeVoid(Contract("pipeline.enable_force_lod0"),pipeline,nullptr);
    static unsigned logged=0;
    if (!ready || logged++<8) Log(std::string("Android pipeline bias after game registration ")+(ready?"PASS":"FAIL"));
    g_in_register_bias=false;
}
#endif
#include "model_lod_state.inc"

// Diagnostic copies contain no new strong Unity references. They never repair
// the receiver: a game-side palette overwrite must remain observable.
struct SkinBindingAudit {
    bool root_known=false,poses_known=false;
    WeakObject root_bone;
    std::vector<Matrix4x4Raw> poses;
    std::string last_state;
    uint32_t reports=0;
};
bool ReadAuditBindposes(void* mesh,std::vector<Matrix4x4Raw>& result) {
    result.clear();
    if (!mesh) return false;
    void* poses=Invoke(Contract("mesh.get_bindposes"),mesh,nullptr,false);
    const int count=ArrayLength(poses);
    if (!poses || count<0 || count>1024) return false;
    result.resize(static_cast<size_t>(count));
    for (int i=0;i<count;++i)
        if (!Unbox(ArrayValue(poses,i),result[static_cast<size_t>(i)])) {result.clear();return false;}
    return true;
}
void CaptureSkinBindingAudit(void* renderer,void* mesh,SkinBindingAudit& audit) {
    void* root=nullptr;
    audit.root_known=ReadNullableObject(Contract("skinned.get_root_bone"),renderer,root);
    if (root && !audit.root_bone.Set(root)) audit.root_known=false;
    audit.poses_known=ReadAuditBindposes(mesh,audit.poses);
}
struct CompletedBinding {
    GenericMatching::ReceiverKey receiver_key;
    uint32_t component_id=0;
    std::string renderer_name;
    WeakObject renderer;
    WeakObject mesh;
    struct BlendShapeSlot {
        std::string name;
        int32_t index;
    };
    std::vector<BlendShapeSlot> blend_shapes;
    std::vector<WeakObject> materials;
    // Authored per-slot overrides, kept so a driver that stops can hand the
    // property back to the package's static value instead of to a guess.
    std::vector<BemMaterialOverrides> material_overrides;
    bool enabled=false;
    bool generated_mesh=false;
    bool generated_materials=false;
#if defined(__ANDROID__)
    bool check_shadow=false;
    int32_t shadow=0;
    WeakObject shadow_mesh;
#endif
    std::vector<std::string> bone_names;
    std::vector<std::string> bone_paths;
    std::vector<WeakObject> bones;
    std::shared_ptr<SavedOriginalBinding> original;
    SkinBindingAudit skin_audit;
};
struct CompletedResource {
    const CharacterAdapter* adapter=nullptr;
    std::shared_ptr<OwnedCharacterAdapter> adapter_owner;
    std::string selection_key;
    WeakObject root;
    std::vector<CompletedBinding> bindings;
    std::vector<WeakObject> experimental_assets; // Only objects created by this module.
    uint64_t last_seen_ms=GetTickCount64();
    bool retired_drop=false; // pruning mark (hot switch retired-generation bound)
};
std::vector<CompletedResource> g_completed;
std::shared_ptr<OwnedCharacterAdapter> OwnAdapter(const CharacterAdapter& adapter);
std::string ActiveSelectionKey(const CharacterAdapter& adapter);
bool SameAdapter(const CharacterAdapter& a,const CharacterAdapter& b) {
    return std::string_view(a.id)==b.id && std::string_view(a.world_resource)==b.world_resource &&
        std::string_view(a.ui_resource)==b.ui_resource && std::string_view(a.resource_id)==b.resource_id &&
        a.explicit_resource==b.explicit_resource && a.receiver_lod==b.receiver_lod;
}
// The clock is single-package. Keep the adapter alive across registry swaps,
// and retain its selection until the old targets have been restored.
struct DriverResourceOwner {
    const CharacterAdapter* adapter=nullptr;
    std::shared_ptr<OwnedCharacterAdapter> adapter_owner;
    std::string selection_key;
};
std::vector<DriverResourceOwner> g_driver_resources;
bool IsDriverResource(const CompletedResource& resource) {
    return resource.adapter && std::any_of(g_driver_resources.begin(),g_driver_resources.end(),[&](const auto& owner) {
        return SameAdapter(*resource.adapter,*owner.adapter) && resource.selection_key==owner.selection_key;
    });
}
bool DriverOwnerMatchesRegistry(const ModRegistry& registry) {
    if (g_driver_resources.empty() || g_animation_driver.DriverCount()==0) return false;
    const auto& path=g_animation_driver.LoadedPath();
    size_t count=0;
    for (const auto& mod:registry.enabled) if (PackagePathUtf8(mod.package)==path) {
        ++count;
        if (std::none_of(g_driver_resources.begin(),g_driver_resources.end(),[&](const auto& owner) {
            return SameAdapter(*owner.adapter,*mod.adapter) && owner.selection_key==mod.selection_key;
        })) return false;
    }
    return count==g_driver_resources.size();
}
// ---- Section 10/13 driver morph application ----
struct DriverMorphValue {
    std::string parameter;
    float range_low=0.0f,range_high=1.0f,weight=0.0f;
};
// Every live mesh that captured a morph source, resolved through the bindings
// currently attached to the character.
template <typename Fn>
void ForEachMorphedMesh(Fn&& visit) {
    for (const auto& completed:g_completed) {
        if (!IsDriverResource(completed)) continue;
        void* root=completed.root.Get();
        if (!root || !IsNativeObjectAlive(root)) continue;
        for (const auto& binding:completed.bindings) {
            const int32_t mesh_id=binding.mesh.instance_id;
            if (mesh_id==0) continue;
            const auto found=g_dynamic_meshes.find(mesh_id);
            if (found==g_dynamic_meshes.end() || !found->second.morph) continue;
            void* mesh=binding.mesh.Get();
            if (!mesh || !IsNativeObjectAlive(mesh)) continue;
            void* renderer=binding.renderer.Get();
            if (!renderer || !IsNativeObjectAlive(renderer) || GetRendererMesh(renderer)!=mesh) continue;
            visit(mesh,*found->second.morph,renderer);
        }
    }
}
// Rebuilds the position stream of every registered mesh from its captured base
// and uploads only the vertex runs that moved. `values` empty means no driver is
// playing, which restores the authored weights once and then idles.
void ApplyDriverMorph(const std::vector<DriverMorphValue>& values) {
    if (!g_driver_morph_active && values.empty()) return;
    const bool had_work=g_driver_morph_active;
    g_driver_morph_active=!values.empty();
    if (!g_driver_morph_active && !had_work) return;

    thread_local std::vector<float> accumulated;
    thread_local std::vector<uint32_t> touched;
#if !defined(__ANDROID__)
    thread_local std::vector<uint8_t> run_bytes;
#endif

    ForEachMorphedMesh([&](void* mesh,const BemMorphSet& set,void* renderer) {
        (void)renderer; // desktop-only MarkModified target
        if (set.vertices.empty() || set.channels.empty() || set.base.empty()) return;
        // accumulated[] is indexed exactly like BemMorphSet::vertices, which is
        // the layout SampleMorphChannel validates against.
        accumulated.assign(set.vertices.size()*3,0.0f);
        for (const auto& channel:set.channels) {
            const DriverMorphValue* driven=nullptr;
            for (const auto& value:values)
                if (value.parameter == channel.parameter) {driven=&value;break;}
            // A negative weight is the restore sample: it replays whatever the
            // load-time tick selected, so parameters no driver owns keep their
            // authored pose while a neighbour is animated.
            if (driven) SampleMorphChannel(channel,set,driven->range_low,driven->range_high,driven->weight,accumulated);
            else SampleMorphChannel(channel,set,0.0f,1.0f,-1.0f,accumulated);
        }
        touched.clear();
        for (size_t i=0;i<set.vertices.size();++i) {
            const float* d=accumulated.data()+i*3;
            if (d[0]!=0.0f || d[1]!=0.0f || d[2]!=0.0f) touched.push_back(static_cast<uint32_t>(i));
        }
        // An empty delta set is a legitimate frame: Stop restoring the authored
        // pose, or a pingpong/sine sample at phase 0. For a mesh the driver has
        // already displaced, the GPU still holds that pose, so the frame must be
        // uploaded anyway -- skipping it is what left the mesh frozen after Stop.
        // Meshes that were never displaced are still skipped, so an unrelated
        // driver does not force a whole-channel upload every frame.
        const bool was_dirty = g_driver_dirty_meshes.contains(mesh);
        if (touched.empty() && !was_dirty) return;
        // Two upload paths produce identical vertex data, so this is a pure A/B
        // the user can flip at runtime:
        //   channel    - replace the whole Position channel (default; portable)
        //   byte_range - rewrite only the moved vertex runs (desktop only)
        // Android has no byte-range setter, so it always takes the channel path.
        int upload_mode = g_vertex_upload_mode.load(std::memory_order_relaxed);
        if (!g_vertex_upload_available[upload_mode])
            upload_mode = g_vertex_upload_available[kVertexUploadChannel]
                ? kVertexUploadChannel : kVertexUploadByteRange;
        // The byte-range path writes only moved runs, so it cannot express an
        // empty restore. Fall back to the whole-channel write for that one frame.
        if (upload_mode == kVertexUploadChannel || touched.empty()) {
            UploadMeshPositions(mesh, set, accumulated, touched);
        } else {
#if defined(__ANDROID__)
            // Unreachable: the byte-range icall is stripped on Android.
#else
        // Sub-range upload into Stream 0 only. Stream 1 (normal/tangent/UV) and
        // Stream 2 (skin) are never touched, which is what keeps shading intact.
        // Each range is rebuilt from the captured base so the stride bytes that
        // carry UV and skin data stay correct.
        auto upload=[&](size_t begin,size_t end) {
            const uint32_t first_vertex=set.vertices[touched[begin]];
            const uint32_t last_vertex=set.vertices[touched[end-1]];
            const size_t byte_offset=size_t(first_vertex)*set.stride;
            const size_t byte_count=size_t(last_vertex-first_vertex+1)*set.stride;
            if (byte_offset+byte_count>set.base.size()) return;
            run_bytes.assign(set.base.begin()+byte_offset,set.base.begin()+byte_offset+byte_count);
            for (size_t i=begin;i<end;++i) {
                const size_t index=touched[i];
                auto* target=run_bytes.data()+(size_t(set.vertices[index])-first_vertex)*set.stride+set.offset;
                float position[3];std::memcpy(position,target,12);
                const float* d=accumulated.data()+index*3;
                for (size_t axis=0;axis<3;++axis) position[axis]+=d[axis];
                std::memcpy(target,position,12);
            }
            SafeUpdateSubVertexBuffer(mesh,static_cast<int32_t>(set.stream),run_bytes.data(),
                static_cast<int32_t>(byte_offset),static_cast<int32_t>(byte_count));
        };
        constexpr size_t kMaxMorphRuns=32;
        size_t runs=1;
        for (size_t i=1;i<touched.size();++i)
            if (set.vertices[touched[i]]!=set.vertices[touched[i-1]]+1) ++runs;
        if (runs>kMaxMorphRuns) { upload(0,touched.size()); }
        else {
            size_t begin=0;
            for (size_t i=1;i<=touched.size();++i) {
                if (i<touched.size() && set.vertices[touched[i]]==set.vertices[touched[i-1]]+1) continue;
                upload(begin,i);
                begin=i;
            }
        }
        // Ask the renderer to drop its cached copy of the mesh. Optional: when the
        // method is not available the sub-range upload above is still what the
        // slider path does, and that path is known to animate correctly.
        InvokeVoid(Contract("mesh.mark_modified"),mesh,nullptr);
        InvokeVoid(Contract("skinned.mark_modified"),renderer,nullptr);
#endif
        }
        // Record whether the device now holds a driver pose for this mesh.
        if (touched.empty()) g_driver_dirty_meshes.erase(mesh);
        else g_driver_dirty_meshes.insert(mesh);
    });
}

// Reconciles running sound voices with the drivers that are actually playing.
// Called once per animation frame; the set difference is normally empty.
void SyncDriverSounds() {
    if (!DriverAudioAvailable()) return;
    const auto summaries = g_animation_driver.Summaries();
    const auto sounds = g_animation_driver.Sounds();
    // `sounding` is true only while a driver is actually advancing: a paused or
    // finished one holds its pose but must not keep a looping voice alive.
    std::unordered_map<std::string, bool> sounding;
    for (const auto& summary : summaries) sounding.emplace(summary.id, summary.sounding);
    std::unordered_map<std::string, DriverSound> by_driver;
    for (const auto& [driver_id, sound] : sounds) {
        const auto state = sounding.find(driver_id);
        if (state != sounding.end() && state->second) by_driver.emplace(driver_id, sound);
    }
    // Retire voices whose driver stopped.
    for (auto it = g_sounding_drivers.begin(); it != g_sounding_drivers.end();) {
        if (by_driver.contains(*it)) { ++it; continue; }
        StopDriverSound(*it);
        it = g_sounding_drivers.erase(it);
    }
    // Start voices whose driver began playing. Starting an already-running key
    // is a no-op inside the backend, so this stays idempotent.
    for (const auto& entry : by_driver) {
        DriverSoundSpec spec;
        spec.path = entry.second.path;
        spec.gain = entry.second.gain;
        spec.loop = entry.second.loop;
        spec.offset_seconds = entry.second.offset_seconds;
        if (spec.path.empty()) continue;
        if (StartDriverSound(entry.first, spec))
            g_sounding_drivers.insert(entry.first);
    }
}

// ---- Section 13 material modulation ----
// A material property is driver-owned only while a playing driver keeps writing
// it. Ownership is reconciled every frame instead of on explicit stop calls, so
// pause, package switch, model retirement and module shutdown all resolve the
// same way: the first frame that stops writing a property restores it.
enum class DriverMaterialKind:uint8_t {Float,Color,Keyword};
struct DriverMaterialWrite {
    DriverMaterialKind kind=DriverMaterialKind::Float;
    uint32_t component=0,slot=0;
    std::string name;                     // shader property or keyword
    float rest_float=0.0f;                // weight 0 endpoint of the binding
    std::array<float,4> rest_color{};
    bool matched=false;                   // a live material accepted a write
    bool warned=false;
};
std::unordered_map<uint64_t,DriverMaterialWrite> g_driver_material_writes;

uint64_t DriverMaterialKey(DriverMaterialKind kind,uint32_t component,uint32_t slot,
                           const std::string& name) {
    uint64_t h=1469598103934665603ull;
    const auto mix=[&h](uint64_t v){
        for(int i=0;i<8;++i){h^=(v>>(i*8))&0xffu;h*=1099511628211ull;}
    };
    mix(static_cast<uint64_t>(kind)); mix(component); mix(slot);
    for (unsigned char c:name) {h^=c;h*=1099511628211ull;}
    return h;
}
template <typename Fn>
void ForEachOwnedMaterial(const CompletedBinding& binding,uint32_t slot,Fn&& visit) {
    if (size_t(slot)>=binding.materials.size()) return;
    void* renderer=binding.renderer.Get();
    if (!renderer || !IsNativeObjectAlive(renderer) || !binding.mesh.Is(GetRendererMesh(renderer))) return;
    void* material=binding.materials[slot].Get();
    if (!material || !IsNativeObjectAlive(material)) return;
    void* current=Invoke(Contract("renderer.get_shared_materials"),renderer,nullptr);
    void* live=ArrayValue(current,static_cast<int>(slot));
    if (live!=material) {
        // A live game copy is owned only when its controller still identifies
        // this exact package source. Never drive an unrelated replacement or FX.
        void* source=RendererMaterialIdentityBase(renderer,current);
        if (ArrayValue(source,static_cast<int>(slot))!=material || !live || !IsNativeObjectAlive(live)) return;
    }
    visit(material,binding);
    if (live!=material) visit(live,binding);
}
template <typename Fn>
void ForEachDriverMaterial(uint32_t component,uint32_t slot,Fn&& visit) {
    for (const auto& completed:g_completed) {
        if (!IsDriverResource(completed)) continue;
        void* root=completed.root.Get();
        if (!root || !IsNativeObjectAlive(root)) continue;
        for (const auto& binding:completed.bindings) {
            if (binding.component_id!=component || size_t(slot)>=binding.materials.size()) continue;
            ForEachOwnedMaterial(binding,slot,visit);
        }
    }
}
// One property write. `keyword_on` is ignored for float and colour targets.
void SetDriverMaterialProperty(void* material,DriverMaterialKind kind,const std::string& name,
                               float value,const std::array<float,4>& color,bool keyword_on) {
    void* key=NewString(name.c_str());
    if (kind==DriverMaterialKind::Float) {
        float sampled=value; void* args[]{key,&sampled};
        InvokeVoid(Contract("material.set_float"),material,args);
    } else if (kind==DriverMaterialKind::Color) {
        float rgba[4]{color[0],color[1],color[2],color[3]}; void* args[]{key,rgba};
        InvokeVoid(Contract("material.set_color"),material,args);
    } else {
        void* args[]{key};
        InvokeVoid(Contract(keyword_on?"material.enable_keyword":"material.disable_keyword"),
                   material,args);
    }
}
void ApplyDriverMaterialValue(const DriverMaterialWrite& write,float value,
                              const std::array<float,4>& color,bool keyword_on) {
    ForEachDriverMaterial(write.component,write.slot,[&](void* material,const CompletedBinding&){
        SetDriverMaterialProperty(material,write.kind,write.name,value,color,keyword_on);
    });
}
// Restores the package's own static value for this property when it has one.
// Returns false when the package never authored the property, where the
// binding's lower endpoint is the rest state by definition of section 13.
bool ReplayDriverMaterialRest(const DriverMaterialWrite& write,void* material,const CompletedBinding& binding) {
    if (size_t(write.slot)>=binding.material_overrides.size()) return false;
    const auto& overrides=binding.material_overrides[write.slot];
    if (write.kind==DriverMaterialKind::Float) {
        for (const auto& entry:overrides.floats) if (entry.first==write.name) {
            SetDriverMaterialProperty(material,write.kind,write.name,entry.second,{},false); return true;
        }
    } else if (write.kind==DriverMaterialKind::Color) {
        for (const auto& entry:overrides.colors) if (entry.first==write.name) {
            SetDriverMaterialProperty(material,write.kind,write.name,0.0f,entry.second,false); return true;
        }
    } else {
        for (const auto& name:overrides.enable_keywords) if (name==write.name) {
            SetDriverMaterialProperty(material,write.kind,write.name,0.0f,{},true); return true;
        }
        for (const auto& name:overrides.disable_keywords) if (name==write.name) {
            SetDriverMaterialProperty(material,write.kind,write.name,0.0f,{},false); return true;
        }
    }
    return false;
}
void ReleaseDriverMaterials(const std::unordered_set<uint64_t>* keep) {
    if (g_driver_material_writes.empty()) return;
    size_t restored=0,given_back=0;
    for (auto it=g_driver_material_writes.begin();it!=g_driver_material_writes.end();) {
        if (keep && keep->count(it->first)) {++it;continue;}
        const DriverMaterialWrite& write=it->second;
        // Each target can have its own authored baseline. An override on one
        // material must not suppress the lower-bound restore on another.
        ForEachDriverMaterial(write.component,write.slot,[&](void* material,const CompletedBinding& binding) {
            if (ReplayDriverMaterialRest(write,material,binding)) ++restored;
            else {
                SetDriverMaterialProperty(material,write.kind,write.name,write.rest_float,write.rest_color,false);
                ++given_back;
            }
        });
        it=g_driver_material_writes.erase(it);
    }
    if (restored||given_back) Log("[AnimDriver] Released material modulation: "+
        std::to_string(restored)+" restored to authored value, "+
        std::to_string(given_back)+" to binding lower bound.");
}

void UnloadAnimationDriver(const char* reason) {
    const bool loaded=g_animation_driver.DriverCount()!=0 || !g_driver_resources.empty();
    if (!loaded && !g_driver_morph_active && g_driver_dirty_meshes.empty() &&
        g_driver_material_writes.empty() && g_sounding_drivers.empty()) return;
    const auto package=g_animation_driver.LoadedPath();
    const auto resources=g_driver_resources.size();
    // Restore while the old owner and completed bindings are still available.
    g_animation_driver.SetAllPlaying(false);
    if (!g_driver_dirty_meshes.empty()) g_driver_morph_active=true;
    ApplyDriverMorph({});
    ReleaseDriverMaterials(nullptr);
    StopAllDriverSounds();
    g_sounding_drivers.clear();
    g_driver_dirty_meshes.clear();
    g_driver_morph_active=false;
    g_animation_driver.Reset();
    g_driver_applied_overrides.clear();
    g_driver_resources.clear();
#if defined(_WIN32)
    g_model_overlay.PublishDrivers({},{});
#elif defined(__ANDROID__)
    ClearAndroidDriverOverrides();
#endif
    if (loaded) Log("[AnimDriver] Unloaded: package="+package+" resources="+
        std::to_string(resources)+" reason="+reason);
}
void WriteDriverMaterial(DriverMaterialKind kind,uint32_t component,uint32_t slot,
                         const std::string& name,float value,const std::array<float,4>& color,
                         bool keyword_on,float rest_float,const std::array<float,4>& rest_color,
                         std::unordered_set<uint64_t>& touched) {
    const uint64_t key=DriverMaterialKey(kind,component,slot,name);
    auto [it,inserted]=g_driver_material_writes.try_emplace(key);
    if (inserted) {
        it->second.kind=kind; it->second.component=component; it->second.slot=slot;
        it->second.name=name; it->second.rest_float=rest_float; it->second.rest_color=rest_color;
    }
    touched.insert(key);
    DriverMaterialWrite& write=it->second;
    bool matched=false;
    ForEachDriverMaterial(component,slot,[&](void* material,const CompletedBinding&){
        SetDriverMaterialProperty(material,kind,name,value,color,keyword_on);
        matched=true;
    });
    if (matched) write.matched=true;
    else if (!write.warned) {
        write.warned=true;
        Log("[AnimDriver] No live material for component "+std::to_string(component)+
            " slot "+std::to_string(slot)+"; '"+name+"' is not modulated.");
    }
}

std::string RelativeBonePath(void* bone) {
    auto path=BuildTransformPath(bone); const auto slash=path.find('/');
    return slash==path.npos?path:path.substr(slash+1);
}
// Original materials are only weakly remembered. A dead weak handle is not
// proof of unloading (the wrapper may be recycled), so the same native object
// (exact instance ID, never a name) is searched in the renderers of resources
// this module still has loaded: the receiver's root, the roots of completed
// records of this role and, on Android, the role's UI donor. Nothing is
// guessed; when the Original is gone the caller refuses with `why`.
bool CollectLiveMaterials(void* root,const std::vector<const WeakObject*>& wanted,std::vector<void*>& found) {
    if (!root || !IsNativeObjectAlive(root)) return false;
    bool inactive=true; void* args[]{ModelRendererType(),&inactive};
    void* renderers=Invoke(Contract("game_object.renderers"),root,args);
    const int count=ArrayLength(renderers);
    if (count<=0 || count>4096) return false;
    for (int r=0;r<count;++r) {
        void* materials=Invoke(Contract("renderer.get_shared_materials"),ArrayValue(renderers,r),nullptr);
        const int material_count=ArrayLength(materials);
        for (int m=0;m<material_count && m<256;++m) {
            void* material=ArrayValue(materials,m);
            for (size_t i=0;i<wanted.size();++i) if (!found[i] && wanted[i]->Is(material)) found[i]=material;
        }
    }
    return std::all_of(found.begin(),found.end(),[](void* value){return value!=nullptr;});
}
void* ResolveOriginalMaterials(const SavedOriginalBinding& original,void* type_template,void* asset,
    const CharacterAdapter& adapter,std::string& why) {
    const int count=static_cast<int>(original.materials.size());
    void* result=count>0 && type_template?NewArrayLike(type_template,count,true):nullptr;
    if (!result) { why="no Original material array recorded"; return nullptr; }
    std::vector<void*> found(original.materials.size(),nullptr);
    std::vector<const WeakObject*> wanted;std::vector<size_t> slots;
    for (size_t i=0;i<original.materials.size();++i) {
        if (!original.material_present[i]) continue;
        found[i]=original.materials[i].Get();
        if (!found[i]) { wanted.push_back(&original.materials[i]); slots.push_back(i); }
    }
    if (!wanted.empty()) {
        std::vector<void*> live(wanted.size(),nullptr);
        bool complete=CollectLiveMaterials(asset,wanted,live);
        for (const auto& record:g_completed) if (!complete && SameAdapter(*record.adapter,adapter))
            if (void* root=record.root.Get(); root && root!=asset) complete=CollectLiveMaterials(root,wanted,live);
#if defined(__ANDROID__)
        if (!complete && !adapter.explicit_resource) {
            void* handle=nullptr; uint32_t handle_root=0;
            struct Release { void*& handle; uint32_t& root; ~Release(){ betterendfieldnext::AndroidReleaseUiDonor(handle,root); } } release{handle,handle_root};
            if (void* donor=RootTemporary(betterendfieldnext::AndroidLoadUiDonor(adapter.ui_resource,handle,handle_root)); donor && donor!=asset)
                complete=CollectLiveMaterials(donor,wanted,live);
        }
#endif
        for (size_t i=0;i<wanted.size();++i) {
            if (!live[i]) {
                why="Original material #"+std::to_string(slots[i])+" ("+original.material_names[slots[i]]+
                    ") was unloaded and no loaded world/UI resource still holds it";
                return nullptr;
            }
            found[slots[i]]=live[i];
        }
        Log("Hot switch recovered "+std::to_string(wanted.size())+" Original material(s) by exact identity from loaded resources");
    }
    for (int i=0;i<count;++i) if (!SetArrayValue(result,i,found[i])) { why="Original material array assignment failed"; return nullptr; }
    return result;
}
bool UseSavedOriginal(void* asset,PreparedBinding& binding,bool require_materials,bool restoring_original) {
    const auto refuse=[&](const std::string& reason) {
        if (g_instance_rebind_active) Log("Hot switch Original binding refused resource="+ObjectName(asset)+
            " receiver="+binding.receiver_key.path+" reason="+reason);
        return false;
    };
    const bool owns_record=std::any_of(g_completed.begin(),g_completed.end(),[&](const auto& record) {
        GenericMatching::ReceiverKey key;
        return record.root.Is(asset) && MakeReceiverKey(*record.adapter,asset,binding.renderer,key) &&
            (binding.receiver_key.character.empty() || binding.receiver_key==key) &&
            std::any_of(record.bindings.begin(),record.bindings.end(),[&](const auto& old) {return old.receiver_key==key;});
    });
    for (auto record=g_completed.rbegin();record!=g_completed.rend();++record) {
        GenericMatching::ReceiverKey key;
        if (!MakeReceiverKey(*record->adapter,asset,binding.renderer,key) ||
            (!binding.receiver_key.character.empty() && binding.receiver_key!=key)) continue;
        for (const auto& old:record->bindings) {
            if (!old.original || old.receiver_key!=key) continue;
            const bool same_root=record->root.Is(asset);
            if (owns_record && !same_root) continue;
            if (!same_root && !old.mesh.Is(binding.original_mesh)) continue;
            // On another root (a scene instance cloned from this template) the
            // game may give the instance its own material copies. A Mesh this
            // module generated proves the generation. Restoring the Original
            // also accepts the recorded Original Mesh itself (keep parts,
            // shadow proxies); a build keeps the receiver's own materials then.
            const bool mesh_proves=old.generated_mesh ||
                (restoring_original && old.original && old.original->mesh.Is(binding.original_mesh));
            if (!same_root && !mesh_proves) {
                void* identity_materials=RendererMaterialIdentityBase(binding.renderer,binding.original_materials);
                if (ArrayLength(identity_materials)!=static_cast<int>(old.materials.size())) continue;
                bool matches=true;
                for (size_t i=0;i<old.materials.size();++i)
                    if (!old.materials[i].Is(ArrayValue(identity_materials,static_cast<int>(i)))) { matches=false; break; }
                if (!matches) continue;
            }
            binding.saved_original=old.original;
            binding.donor_mesh=old.original->ResolveMesh(binding.original_mesh);
            binding.donor_bones=old.original->bones.Get();
            binding.donor_enabled=old.original->enabled;
            if (!binding.donor_mesh || !IsNativeObjectAlive(binding.donor_mesh)) {
                Log("Hot switch Original Mesh unavailable: "+key.resource+"/"+key.path); return false;
            }
            std::string why;
            binding.donor_materials=ResolveOriginalMaterials(*old.original,binding.original_materials,asset,*record->adapter,why);
            if (!binding.donor_materials) {
                Log("Hot switch Original materials unavailable: "+key.resource+"/"+key.path+" ("+why+")"+
                    (require_materials?"; refused":"; continuing without them"));
                if (require_materials) return false;
            }
            if (!old.original->bone_paths.empty()) {
                BE_ResolvedClassV1 transform{};
                if (!g_host->resolve_class || g_host->resolve_class(g_host->context,"UnityEngine.CoreModule.dll",
                    "UnityEngine","Transform",&transform)!=BE_Result_Ok || !transform.type_object) return refuse("Transform-type-unavailable");
                bool inactive=true; void* args[]{transform.type_object,&inactive};
                void* transforms=Invoke(Contract("game_object.renderers"),asset,args);
                const int count=ArrayLength(transforms);
                if (count<0 || count>8192) return refuse("Transform-count-invalid");
                // A pooled UI prefab may contain unrelated helper/decorative
                // transforms with duplicate or transient paths. Restoration
                // only needs the paths captured in this Original palette.
                const std::unordered_set<std::string> wanted(old.original->bone_paths.begin(),old.original->bone_paths.end());
                std::unordered_map<std::string,void*> paths;
                for (int i=0;i<count;++i) {
                    void* bone=ArrayValue(transforms,i);
                    std::string path;
                    if (!bone || !IsNativeObjectAlive(bone) || !ResourceRelativePath(asset,bone,path) || !wanted.contains(path)) continue;
                    if (!paths.emplace(path,bone).second)
                        return refuse("Original-bone-path-ambiguous path="+path);
                }
                void* mapped=NewArrayLike(binding.original_bones,static_cast<int>(old.original->bone_paths.size()));
                if (!mapped) return refuse("bone-array-allocation-failed");
                for (size_t i=0;i<old.original->bone_paths.size();++i) {
                    const auto& path=old.original->bone_paths[i];
                    const auto found=paths.find(path);
                    if (found==paths.end() || !SetArrayValue(mapped,static_cast<int>(i),found->second)) return refuse("Original-bone-path-unavailable path="+path);
                }
                binding.donor_bones=mapped;
                auto local_original=std::make_shared<SavedOriginalBinding>();
                local_original->CopyIdentity(*old.original);
                if (!local_original->mesh.Is(binding.donor_mesh) || !local_original->bones.Set(mapped)) return refuse("local-Original-capture-failed");
#if defined(__ANDROID__)
                local_original->change_shadow=old.original->change_shadow; local_original->shadow=old.original->shadow;
                if (!local_original->shadow_mesh.Set(old.original->shadow_mesh.Get())) return refuse("Original-shadow-unavailable");
#endif
                binding.saved_original=std::move(local_original);
            }
            if (!binding.donor_bones && !IsStaticRenderer(binding.renderer)) return refuse("Original-bones-unavailable");
            return true;
        }
    }
    return true;
}
#if defined(__ANDROID__)
WeakObject g_android_test_world;
#endif
bool RememberResource(const CharacterAdapter& adapter,void* asset,
    const std::vector<PreparedBinding>& bindings,CompletedResource& record,std::string selection_key={},bool retain_original=false) {
    record.adapter=&adapter;
    record.adapter_owner=OwnAdapter(adapter); record.selection_key=std::move(selection_key);
    if (!record.root.Set(asset)) return false;
    for (const auto& binding:bindings) {
        if (!PrepareRendererMaterialCache(binding)) return false;
        // First capture must retain the game's base cache, not a renderer
        // array with transient additive VFX appended. Rollback still uses
        // binding.original_materials, which is the complete live array.
        void* original_base_materials=DonorMaterials(binding);
        if (!binding.saved_original && binding.material_cache)
            original_base_materials=binding.material_cache->source.Get();
        CompletedBinding completed;
        if (!MakeReceiverKey(adapter,asset,binding.renderer,completed.receiver_key)) return false;
        completed.renderer_name=ObjectName(binding.renderer);
        completed.renderer.Set(binding.renderer);
        completed.component_id=binding.component_id; completed.enabled=binding.custom_enabled;
        completed.material_overrides=binding.material_overrides;
        completed.generated_mesh=binding.custom_mesh!=DonorMesh(binding);
        completed.original=binding.saved_original;
        // Normal deliveries can revisit a cached UI template or a clone with
        // game-owned material copies even when hot switching is disabled.
        // Keep its donor lineage for every applied selection; otherwise an
        // unchanged hide/keep Mesh is mistaken for an unknown generated Mesh.
        if (!completed.original && (retain_original || g_hot_switch_runtime.load() || g_clone_support_runtime.load() || !record.selection_key.empty())) {
            auto original=std::make_shared<SavedOriginalBinding>();
            if (!original->mesh.Set(DonorMesh(binding)) || !original->SetMaterials(original_base_materials) ||
                (!IsStaticRenderer(binding.renderer) && !original->bones.Set(DonorBones(binding)))) return false;
            original->enabled=binding.original_enabled;
            original->visual_root_path=binding.visual_root_path; original->visual_pristine=binding.visual_pristine;
            for (int i=0;i<ArrayLength(DonorBones(binding));++i) {
                std::string path;
                if (!ResourceRelativePath(asset,ArrayValue(DonorBones(binding),i),path)) return false;
                original->bone_paths.push_back(std::move(path));
            }
#if defined(__ANDROID__)
            original->change_shadow=binding.change_shadow; original->shadow=binding.original_shadow;
            if (!original->shadow_mesh.Set(binding.original_shadow_mesh)) return false;
            if (binding.change_shadow && binding.original_shadow_mesh && binding.original_shadow_mesh!=binding.custom_shadow_mesh) {
                original->shadow_mesh_pin=AcquireOriginalMeshPin(binding.original_shadow_mesh);
                if (!original->shadow_mesh_pin) return false;
            }
#endif
            completed.original=std::move(original);
        }
        if (binding.material_cache && completed.original &&
            !completed.original->material_cache_materials.handle &&
            !completed.original->material_cache_materials.Set(original_base_materials)) return false;
        // Pin only an Original the receiver is about to stop referencing.
        if (completed.original && binding.custom_mesh!=DonorMesh(binding)) completed.original->EnsurePinned(DonorMesh(binding));
#if defined(__ANDROID__)
        if (completed.original && binding.change_shadow && !completed.original->shadow_mesh_pin) {
            if (void* original=completed.original->shadow_mesh.Get();original && original!=binding.custom_shadow_mesh) {
                completed.original->shadow_mesh_pin=AcquireOriginalMeshPin(original);
                if (!completed.original->shadow_mesh_pin) return false;
            }
        }
        completed.check_shadow=binding.change_shadow; completed.shadow=binding.custom_shadow;
        if (binding.change_shadow && binding.custom_shadow_mesh && !completed.shadow_mesh.Set(binding.custom_shadow_mesh)) return false;
#endif
        completed.bone_names=binding.bone_names;
        void* bones=binding.custom_bones?binding.custom_bones:binding.original_bones;
        for (int i=0;i<ArrayLength(bones);++i) {
            void* bone=ArrayValue(bones,i);std::string path;WeakObject weak;
            if (!ResourceRelativePath(asset,bone,path) || !weak.Set(bone)) return false;
            completed.bone_paths.push_back(std::move(path));completed.bones.push_back(std::move(weak));
        }
        if (!completed.mesh.Set(binding.custom_mesh)) return false;
        for (int i=0;i<ArrayLength(binding.custom_materials);++i) {
            void* custom=ArrayValue(binding.custom_materials,i);bool original=false;
            for (int j=0;j<ArrayLength(DonorMaterials(binding));++j) if (custom==ArrayValue(DonorMaterials(binding),j)) original=true;
            if (!original) completed.generated_materials=true;
            WeakObject material;
            if (!material.Set(ArrayValue(binding.custom_materials,i))) return false;
            completed.materials.push_back(std::move(material));
        }
        if (completed.generated_mesh) {
            void* m = completed.mesh.Get();
            if (m) {
                int32_t bs_count = 0;
                if (InvokeValue(Contract("mesh.get_blend_shape_count"), m, nullptr, bs_count) && bs_count > 0) {
                    for (int32_t i = 0; i < bs_count; ++i) {
                        void* p[]{ &i };
                        void* name_obj = Invoke(Contract("mesh.get_blend_shape_name"), m, p);
                        if (name_obj) {
                            std::string name = ManagedString(name_obj);
                            completed.blend_shapes.push_back({ std::move(name), i });
                            Log("Renderer " + completed.renderer_name + " cached BlendShape [" + std::to_string(i) + "]: " + completed.blend_shapes.back().name);
                        }
                    }
                }
            }
        }
        record.bindings.push_back(std::move(completed));
    }
    return true;
}
bool IsCompletedResource(const CharacterAdapter& adapter,void* asset,std::string_view selection_key={}) {
    if (!IsGameObjectResource(asset)) return false;
    // Natural clones may share the completed template's meshes/materials. The
    // full per-component identity must agree; names alone never prove completion.
    bool inactive=true; void* args[]{ModelRendererType(),&inactive};
    void* renderers=Invoke(Contract("game_object.renderers"),asset,args);
    const int count=ArrayLength(renderers);
    if (!renderers || count<=0 || count>4096) return false;
    for (const auto& record:g_completed) {
        if (!SameAdapter(*record.adapter,adapter) || record.selection_key!=selection_key) continue;
        bool matches=true;
        for (const auto& binding:record.bindings) {
            void* renderer=nullptr;
            for (int i=0;i<count;++i) {
                void* candidate=ArrayValue(renderers,i);
                GenericMatching::ReceiverKey key;
                if (MakeReceiverKey(adapter,asset,candidate,key) && key==binding.receiver_key) {
                    if (renderer) { matches=false; break; }
                    renderer=candidate;
                }
            }
            bool enabled=false;
            if (!renderer || !binding.mesh.Is(GetRendererMesh(renderer)) ||
                !GetRendererEnabled(renderer,enabled) || enabled!=binding.enabled) { matches=false; break; }
#if defined(__ANDROID__)
            if (binding.check_shadow) {
                int32_t mode=-1;void* proxy=nullptr;
                if (!InvokeValue(Contract("android.shadow_get"),renderer,nullptr,mode) || mode!=binding.shadow ||
                    !ReadNullableObject(Contract("android.shadow_mesh_get"),renderer,proxy) ||
                    (proxy?!binding.shadow_mesh.Is(proxy):binding.shadow_mesh.instance_id!=0)) { matches=false; break; }
            }
#endif
            if (!binding.bone_paths.empty()) {
                void* bones=Invoke(Contract("skinned.get_bones"),renderer,nullptr);
                if (ArrayLength(bones)!=static_cast<int>(binding.bone_paths.size())) { matches=false; break; }
                for (size_t b=0;b<binding.bone_paths.size();++b) {
                    void* bone=ArrayValue(bones,static_cast<int>(b));std::string path;
                    if (!ResourceRelativePath(asset,bone,path) || path!=binding.bone_paths[b] ||
                        (record.root.Is(asset) && !binding.bones[b].Is(bone))) {matches=false;break;}
                }
                if (!matches) break;
            }
            void* materials=Invoke(Contract("renderer.get_shared_materials"),renderer,nullptr);
            materials=RendererMaterialIdentityBase(renderer,materials);
            if (ArrayLength(materials)!=static_cast<int>(binding.materials.size())) { matches=false; break; }
            for (size_t i=0;i<binding.materials.size();++i)
                if (!binding.materials[i].Is(ArrayValue(materials,static_cast<int>(i)))) { matches=false; break; }
            if (!matches) break;
        }
        if (matches) return true;
    }
    return false;
}
// Index textures currently bound to live published receivers of this role,
// through the renderers' actual material arrays (no extra roots are kept).
void IndexLivePublishedTextures(const CharacterAdapter& adapter,std::unordered_map<std::string,void*>& live) {
    std::unordered_set<void*> materials_seen;
    for (const auto& record:g_completed) {
        if (!SameAdapter(*record.adapter,adapter)) continue;
        void* root=record.root.Get(); if (!root) continue;
        bool inactive=true; void* args[]{ModelRendererType(),&inactive};
        void* renderers=Invoke(Contract("game_object.renderers"),root,args);
        const int count=ArrayLength(renderers);
        if (count<=0 || count>4096) continue;
        for (int i=0;i<count;++i) {
            void* materials=Invoke(Contract("renderer.get_shared_materials"),ArrayValue(renderers,i),nullptr);
            const int material_count=ArrayLength(materials);
            for (int m=0;m<material_count && m<256;++m) {
                void* material=ArrayValue(materials,m);
                if (!material || !materials_seen.insert(material).second) continue;
                void* ids=Invoke(Contract("material.get_texture_property_ids"),material,nullptr);
                const int id_count=ArrayLength(ids);
                for (int k=0;k<id_count && k<256;++k) {
                    int32_t slot=0; if (!Unbox(ArrayValue(ids,k),slot)) break;
                    void* parameters[1]{&slot};
                    void* texture=Invoke(Contract("material.get_texture_by_id"),material,parameters,false);
                    int32_t instance=0;
                    if (!texture || !InvokeValue(Contract("object.instance_id"),texture,nullptr,instance)) continue;
                    const auto identity=g_generated_texture_identity.find(instance);
                    if (identity!=g_generated_texture_identity.end()) live.emplace(identity->second,texture);
                }
            }
        }
    }
}
std::string DescribeSkinBindingState(const CompletedBinding& binding,void* renderer) {
    auto id=[](void* object) {
        int32_t value=0;
        if (object) InvokeValue(Contract("object.instance_id"),object,nullptr,value);
        return value;
    };
    void* mesh=GetRendererMesh(renderer);
    void* bones=GetRendererBones(renderer);
    const int count=ArrayLength(bones);
    size_t mismatches=0;int first=-1;int32_t first_id=0;
    if (bones && count>=0 && count<=1024) for (int i=0;i<count;++i) {
        const auto actual=id(ArrayValue(bones,i));
        if (static_cast<size_t>(i)>=binding.bones.size() || actual!=binding.bones[i].instance_id) {
            ++mismatches;if (first<0) {first=i;first_id=actual;}
        }
    }
    std::string state="mesh="+std::to_string(id(mesh))+" expectedMesh="+std::to_string(binding.mesh.instance_id)+
        " bones="+std::to_string(count)+"/"+std::to_string(binding.bones.size())+
        " boneMismatches="+std::to_string(mismatches);
    if (!bones || count<0 || count>1024) state+=" bonesRead=unavailable";
    if (first>=0) {
        state+=" firstBone="+std::to_string(first)+" actualId="+std::to_string(first_id);
        if (static_cast<size_t>(first)<binding.bones.size()) state+=
            " expectedId="+std::to_string(binding.bones[first].instance_id)+" expectedPath="+binding.bone_paths[first];
    }
    void* root=nullptr;
    if (ReadNullableObject(Contract("skinned.get_root_bone"),renderer,root))
        state+=" rootBone="+std::to_string(id(root))+" expectedRoot="+
            (binding.skin_audit.root_known?std::to_string(binding.skin_audit.root_bone.instance_id):"unavailable");
    else state+=" rootBone=unavailable";
    std::vector<Matrix4x4Raw> poses;
    if (ReadAuditBindposes(mesh,poses) && binding.skin_audit.poses_known) {
        size_t changed=0;int first_pose=-1;
        for (size_t i=0;i<std::min(poses.size(),binding.skin_audit.poses.size());++i)
            for (size_t j=0;j<16;++j) if (poses[i].m[j]!=binding.skin_audit.poses[i].m[j]) {
                ++changed;if (first_pose<0) first_pose=static_cast<int>(i);break;
            }
        state+=" bindposes="+std::to_string(poses.size())+"/"+std::to_string(binding.skin_audit.poses.size())+
            " changedPoses="+std::to_string(changed)+" firstPose="+std::to_string(first_pose);
    } else state+=" bindposes=unavailable";
    int32_t vertices=-1;
    if (mesh) InvokeValue(Contract("mesh.get_vertex_count"),mesh,nullptr,vertices);
    state+=" vertices="+std::to_string(vertices)+DescribeRendererVisibility(renderer)+
        " expectedEnabled="+std::to_string(binding.enabled);
    void* materials=Invoke(Contract("renderer.get_shared_materials"),renderer,nullptr);
    const int material_count=ArrayLength(materials);
    state+=" materialSlots="+std::to_string(material_count)+"/"+std::to_string(binding.materials.size());
    for (int i=0;i<material_count && i<32;++i) {
        void* material=ArrayValue(materials,i);
        state+=" material["+std::to_string(i)+"]="+DescribeBindingObject(material)+
            " expected="+(static_cast<size_t>(i)<binding.materials.size()?
                std::to_string(binding.materials[size_t(i)].instance_id):"none");
        if (material && IsNativeObjectAlive(material)) state+=" shader["+std::to_string(i)+"]="+
            DescribeBindingObject(Invoke(Contract("material.get_shader"),material,nullptr,false));
    }
    return state;
}
#include "model_retired_resources.inc"
void PruneCompletedResources() {
    const auto now=GetTickCount64();
    // Scene instances and pooled UI models can retain a retired generation
    // with hot switching off. A recycled wrapper does not prove its native
    // Mesh unused. Keep bounded lineage so later deliveries can recover donors.
    constexpr size_t kRetiredGenerationsPerRole=4;
    std::unordered_map<const CharacterAdapter*,size_t> retired;
    for (auto record=g_completed.rbegin();record!=g_completed.rend();++record) {
        if (record->root.Get()) {record->last_seen_ms=now;continue;}
        // Replaced materials, geometry and hidden parts can all survive in
        // pooled instances; a restored-Original record needs no retention.
        if (std::none_of(record->bindings.begin(),record->bindings.end(),
                [](const auto& binding){return binding.generated_mesh || binding.generated_materials ||
                    (binding.original && binding.enabled!=binding.original->enabled);})) {
            record->retired_drop=true;continue;
        }
        const CharacterAdapter* role=record->adapter;
        for (const auto& [seen,count]:retired) if (SameAdapter(*seen,*role)) {role=seen;break;}
        if (++retired[role]>kRetiredGenerationsPerRole) record->retired_drop=true;
    }
    std::erase_if(g_completed,[](const auto& record){return record.retired_drop;});
}
#if defined(__ANDROID__)
void InspectAndroidRenderers() {
    static uint64_t next=0;
    if (!betterendfieldnext::AndroidInspectionEnabled() || GetTickCount64()<next || g_completed.empty()) return;
    next=GetTickCount64()+5000;
    void* args[]{ModelRendererType()};
    void* renderers=Invoke(Contract("android.all_renderers"),nullptr,args);
    const int count=ArrayLength(renderers);
    if (count<0 || count>30000) return;
    std::array<int,4> originals{},customs{}; int examples=0;
    for (int i=0;i<count;++i) {
        void* renderer=ArrayValue(renderers,i); const auto name=ObjectName(renderer);
        if (name.size()<5 || name.substr(name.size()-5,4)!="_lod" || name.back()<'0' || name.back()>'3') continue;
        bool target=false;
        for (const auto& record:g_completed) for (const auto& component:record.adapter->components)
            if (name.substr(0,name.size()-1)==std::string_view(component.name).substr(0,std::strlen(component.name)-1)) target=true;
        if (!target) continue;
        bool enabled=false,visible=false;
        if (!GetRendererEnabled(renderer,enabled) || !enabled ||
            !InvokeValue(Contract("android.renderer_visible"),renderer,nullptr,visible) || !visible) continue;
        void* mesh=GetRendererMesh(renderer); bool custom=false;
        for (const auto& record:g_completed) for (const auto& binding:record.bindings)
            if (binding.generated_mesh && binding.mesh.Is(mesh)) custom=true;
        const size_t lod=name.back()-'0';
        if (custom) ++customs[lod]; else ++originals[lod];
        if (!custom && examples++<3) Log("Android visible source renderer: "+BuildTransformPath(renderer)+
            " bones="+std::to_string(ArrayLength(GetRendererBones(renderer))));
    }
    std::string status="Android visible renderer audit";
    for (size_t i=0;i<4;++i) status+=" LOD"+std::to_string(i)+" custom/source="+
        std::to_string(customs[i])+"/"+std::to_string(originals[i]);
    Log(status);
}
#endif
#if defined(__ANDROID__)
bool ReadCompletedAndroidDonor(const CharacterAdapter& adapter,const BemPocData& bem,void* asset,
    std::vector<PreparedBinding>& bindings) {
    if (asset==g_ready_resource && g_ready_bindings) {bindings=*g_ready_bindings;return true;}
    if (!IsCompletedResource(adapter,asset,ActiveSelectionKey(adapter))) return false;
    bool inactive=true; void* args[]{ModelRendererType(),&inactive};
    void* renderers=Invoke(Contract("game_object.renderers"),asset,args);
    for (const auto& component:bem.components) {
        PreparedBinding binding; binding.component_id=component.info.component_id;
        GenericMatching::ReceiverKey expected;
        for (const auto& record:g_completed) if (SameAdapter(*record.adapter,adapter) && record.selection_key==ActiveSelectionKey(adapter))
            for (const auto& old:record.bindings) if (old.component_id==binding.component_id && old.receiver_key.region==GenericMatching::Region::Lod0) expected=old.receiver_key;
        if (expected.path.empty()) return false;
        for (int i=0;i<ArrayLength(renderers);++i) {
            void* renderer=ArrayValue(renderers,i);
            GenericMatching::ReceiverKey key;
            if (!MakeReceiverKey(adapter,asset,renderer,key) || key!=expected) continue;
            if (binding.renderer) return false;
            binding.renderer=renderer;
        }
        if (!binding.renderer) return false;
        binding.receiver_key=expected;
        binding.custom_mesh=GetRendererMesh(binding.renderer);
        binding.custom_materials=Invoke(Contract("renderer.get_shared_materials"),binding.renderer,nullptr);
        if (!binding.custom_mesh || !binding.custom_materials ||
            !GetRendererEnabled(binding.renderer,binding.original_enabled)) return false;
        if (component.info.flags&kComponentFlagNoGeometry) binding.original_mesh=binding.custom_mesh;
        else {
            binding.custom_bones=GetRendererBones(binding.renderer);
            if (ArrayLength(binding.custom_bones)!=static_cast<int>(component.bone_names.size())) return false;
            for (size_t b=0;b<component.bone_names.size();++b) {
                const auto name=ObjectName(ArrayValue(binding.custom_bones,static_cast<int>(b)));
                if (!bem.skip_validation && !component.BoneNameMatchesForResource(b,name,1)) return false;
                binding.bone_names.push_back(name);
            }
        }
        binding.original_materials=binding.custom_materials;
        binding.original_bones=GetRendererBones(binding.renderer);
        if (!UseSavedOriginal(asset,binding)) return false;
        binding.material_overrides=component.draw_material_overrides;
        bindings.push_back(binding);
    }
    Log("Android donor reuses a verified committed UI resource");
    return true;
}
#endif
// Lineage of a receiver this module already completed. Identity is native
// (WeakObject::Is, instance ID), so a recycled managed wrapper still resolves.
// PublishCompleted keeps one active record per root: on that root the
// receiver's Mesh alone proves ownership (it is this module's recorded output
// or the recorded Original), so a material array rewritten after a
// synchronous delivery cannot hide the saved Original. An unknown Mesh on a
// completed receiver is refused, never promoted to pristine. Other roots
// (natural clones) still need the complete Mesh+material identity of one
// generation. identity.detail names the refused branch for diagnostics.
void LogGenericLineageNote(const GenericMatching::ReceiverKey& key,const char* note) {
    static std::unordered_set<std::string> logged;
    auto entry=key.resource+"/"+key.path+"|"+note;
    if (logged.size()>=256 || !logged.insert(entry).second) return;
    Log(std::string("Generic lineage: ")+note+": "+key.resource+"/"+key.path);
}
bool ReadGenericPristineMesh(const CharacterAdapter& adapter,void* asset,
    const GenericRendererCandidate& candidate,GenericMatching::MeshIdentity& identity) {
    using Origin=GenericMatching::DonorOrigin;
    void* lineage_mesh=candidate.mesh;
    identity={};void* pristine=lineage_mesh;bool known_generated=false;
    void* identity_materials=RendererMaterialIdentityBase(candidate.renderer,candidate.materials);
    // A receiver's own completed record is stronger evidence than another
    // template/clone sharing an unchanged Mesh. Retired records must not veto it.
    const bool owns_record=std::any_of(g_completed.begin(),g_completed.end(),[&](const auto& record) {
        return SameAdapter(*record.adapter,adapter) && record.root.Is(asset) &&
            std::any_of(record.bindings.begin(),record.bindings.end(),[&](const auto& binding) {
                return binding.receiver_key==candidate.key;
            });
    });
    bool saw_completed=false;
    Origin unavailable_origin=Origin::Unavailable;
    std::string unavailable_detail="completed Mesh has no verified Original lineage";
    auto refuse=[&](Origin origin,const char* detail) {
        identity={};identity.origin=origin;identity.detail=detail;return false;
    };
    auto same_object=[](void* a,void* b) {
        int32_t x=0,y=0;
        return a && b && (a==b || (IsNativeObjectAlive(a) && IsNativeObjectAlive(b) &&
            InvokeValue(Contract("object.instance_id"),a,nullptr,x) &&
            InvokeValue(Contract("object.instance_id"),b,nullptr,y) && x==y));
    };
    for (const auto& record:g_completed) if (SameAdapter(*record.adapter,adapter)) for (const auto& old:record.bindings) {
        if (old.receiver_key!=candidate.key) continue;
        const bool same_root=record.root.Is(asset);
        if (owns_record && !same_root) continue;
        const bool same_mesh=old.mesh.Is(lineage_mesh);
        void* saved=old.original?old.original->ResolveMesh(lineage_mesh):nullptr;
        if (!same_root && !same_mesh) continue;
        saw_completed=true;
        if (same_root && !same_mesh && !same_object(saved,lineage_mesh))
            return refuse(Origin::Unavailable,!old.original?
                "completed receiver holds an unknown Mesh and no Original was saved":
                saved?"completed receiver holds a Mesh that is neither this module's output nor the saved Original":
                "saved Original Mesh released");
        if (!same_mesh) {
            // A completed receiver can return to its exact saved Original after
            // a reload/rollback. Re-prepare it without accepting an unknown Mesh.
            if (same_root && same_object(saved,lineage_mesh)) { identity.mesh=saved; pristine=saved; }
            continue;
        }
        if (!old.mesh.Get()) LogGenericLineageNote(candidate.key,"managed wrapper recycled; matched by instance ID");
        const int count=ArrayLength(identity_materials);
        bool custom_materials=count==static_cast<int>(old.materials.size());
        for (size_t i=0;custom_materials && i<old.materials.size();++i)
            custom_materials=old.materials[i].Is(ArrayValue(identity_materials,int(i)));
        bool original_materials=old.original && static_cast<int>(old.original->materials.size())==count;
        for (int i=0;original_materials && i<count;++i) {
            void* material=ArrayValue(identity_materials,i);
            original_materials=old.original->material_present[size_t(i)]?
                old.original->materials[size_t(i)].Is(material):material==nullptr;
        }
        if (!custom_materials && !original_materials) {
            // Another root: a different generation may be the exact owner.
            // Unknown third-party bindings never become pristine by name.
            if (!same_root && !old.generated_mesh) {
                // This query identifies geometry only. An exact saved Original
                // Mesh remains pristine when the game gives a clone different
                // material slots; UseSavedOriginal validates material ownership
                // separately before a build or restoration can borrow them.
                if (!same_object(saved,lineage_mesh)) {
                    unavailable_detail=count!=static_cast<int>(old.materials.size())?
                        "clone material count differs from its completed generation":
                        "clone materials do not identify a completed generation";
                    continue;
                }
            }
            // A Mesh this module generated is held by no third party: on a
            // natural clone it proves the generation even when the game gave
            // the instance its own material copies.
            LogGenericLineageNote(candidate.key,same_root?
                "receiver materials changed after commit; Mesh lineage keeps the saved Original":
                old.generated_mesh?"clone materials differ; generated Mesh lineage keeps the saved Original":
                "clone materials changed; exact Original Mesh identifies geometry only");
        }
        known_generated=old.generated_mesh || !old.materials.empty();
        if (!old.original) {
            unavailable_origin=Origin::CompletedWithoutOriginal;
            unavailable_detail="completed without a saved Original (hot switch was off at that commit)";
            if (same_root) return refuse(unavailable_origin,unavailable_detail.c_str());
            continue;
        }
        if (!saved || !IsNativeObjectAlive(saved)) {
            unavailable_detail="saved Original Mesh is no longer alive";
            if (same_root) return refuse(Origin::Unavailable,unavailable_detail.c_str());
            continue;
        }
        if (identity.mesh && identity.mesh!=saved) return refuse(Origin::Ambiguous,"completed generations claim different Originals");
        identity.mesh=saved;pristine=saved;
    }
    if (saw_completed && !identity.mesh) return refuse(unavailable_origin,unavailable_detail.c_str());
    if (!pristine || !IsNativeObjectAlive(pristine)) return refuse(Origin::Unavailable,"receiver Mesh missing or not alive");
    if (!ReadGenericMeshIdentity(pristine,known_generated?Origin::SavedOriginal:Origin::Pristine,identity)) {
        identity.detail="Mesh identity unreadable (name/sub-mesh/index count)";return false;
    }
    return true;
}
#if defined(_WIN32)
// Discovery must not rebuild bone arrays or materials. For a generated Mesh,
// use its proven saved Original identity and resolve that palette's paths in
// this root; the currently displayed custom palette can have a different order.
bool ValidateAuroraLegacyCandidate(const CharacterAdapter& adapter,void* asset,uint32_t component,
    const GenericRendererCandidate& candidate,const GenericMatching::MeshIdentity& identity) {
    std::array<void*,AuroraLegacy::kBoneCount> bones{};
    if (identity.origin==GenericMatching::DonorOrigin::Pristine) {
        if (ArrayLength(candidate.bones)!=int(bones.size()) || ArrayLength(candidate.materials)!=1) return false;
        for (size_t i=0;i<bones.size();++i) bones[i]=ArrayValue(candidate.bones,int(i));
        return ValidateAuroraLegacyParts(asset,candidate.key,component,identity.mesh,ArrayValue(candidate.materials,0),bones);
    }
    if (identity.origin!=GenericMatching::DonorOrigin::SavedOriginal) return false;
    const SavedOriginalBinding* original=nullptr;
    for (const auto& record:g_completed) if (SameAdapter(*record.adapter,adapter))
        for (const auto& old:record.bindings) if (old.original && old.receiver_key==candidate.key &&
            old.original->ResolveMesh(candidate.mesh)==identity.mesh) {
            if (old.original->bone_paths.size()!=bones.size() || old.original->materials.size()!=1 ||
                old.original->material_present.size()!=1 || !old.original->material_present[0]) return false;
            for (size_t i=0;i<bones.size();++i)
                if (old.original->bone_paths[i]!=AuroraLegacy::kCurrentBones[i].path) return false;
            original=old.original.get();
        }
    if (!original) return false;
    void* material=original->materials[0].Get();
    if (!material) {
        std::vector<const WeakObject*> wanted{&original->materials[0]};std::vector<void*> live(1,nullptr);
        bool complete=CollectLiveMaterials(asset,wanted,live);
        for (const auto& record:g_completed) if (!complete && SameAdapter(*record.adapter,adapter))
            if (void* root=record.root.Get();root && root!=asset) complete=CollectLiveMaterials(root,wanted,live);
        if (!complete) return false;
        material=live[0];
    }
    BE_ResolvedClassV1 transform{};
    if (!g_host->resolve_class || g_host->resolve_class(g_host->context,"UnityEngine.CoreModule.dll",
        "UnityEngine","Transform",&transform)!=BE_Result_Ok || !transform.type_object) return false;
    bool inactive=true;void* args[]{transform.type_object,&inactive};
    void* transforms=Invoke(Contract("game_object.renderers"),asset,args);
    const int count=ArrayLength(transforms);
    if (count<0 || count>8192) return false;
    std::unordered_map<std::string_view,size_t> wanted;
    for (size_t i=0;i<bones.size();++i) wanted.emplace(AuroraLegacy::kCurrentBones[i].path,i);
    for (int i=0;i<count;++i) {
        void* bone=ArrayValue(transforms,i);std::string path;
        if (!IsNativeObjectAlive(bone) || !ResourceRelativePath(asset,bone,path)) continue;
        const auto found=wanted.find(path);
        if (found==wanted.end()) continue;
        if (bones[found->second]) return false;
        bones[found->second]=bone;
    }
    return ValidateAuroraLegacyParts(asset,candidate.key,component,identity.mesh,material,bones);
}
#endif
GenericMatching::Match SelectCompatibleModelDonor(const CharacterAdapter& adapter,void* asset,
    const std::vector<GenericRendererCandidate>& index,const std::vector<GenericMatching::Candidate>& candidates,
    uint32_t component_id,bool static_mesh,GenericMatching::Request& request,bool windows_compat,bool& legacy_aurora) {
    legacy_aurora=false;
    const auto candidate_valid=[&](const auto& candidate) {
        const auto found=std::find_if(index.begin(),index.end(),[&](const auto& entry){return entry.renderer==candidate.renderer;});
        return found!=index.end() && candidate.pristine.mesh && !GenericCandidateContractError(asset,*found,static_mesh);
    };
    auto match=GenericMatching::SelectUnique(candidates,request,candidate_valid);
#if defined(_WIN32)
    const bool is_legacy_aurora = windows_compat &&
        AuroraLegacy::IsLegacyTarget(adapter.id,request.resource,component_id,request.mesh_name,request.indices,adapter.explicit_resource);
    if (is_legacy_aurora && (match.status==GenericMatching::MatchStatus::Missing || !request.check_indices)) {
        // The package/header contract remains unchanged. When the normal match has no candidate
        // or when check_indices was bypassed (e.g. skip_validation=true), adapt to the verified
        // 1.6.3 donor so the exact receiver path and 10-slot bone remapping are always enforced.
        request.indices=AuroraLegacy::kCurrentIndices;
        request.verified_receiver_path=AuroraLegacy::kReceiverPath;
        auto adapted_match=GenericMatching::SelectUnique(candidates,request,[&](const auto& candidate) {
            if (!candidate_valid(candidate)) return false;
            const auto found=std::find_if(index.begin(),index.end(),[&](const auto& entry){return entry.renderer==candidate.renderer;});
            return ValidateAuroraLegacyCandidate(adapter,asset,component_id,*found,candidate.pristine);
        });
        if (adapted_match.status==GenericMatching::MatchStatus::Matched) {
            match=adapted_match;
            legacy_aurora=true;
        }
    }
#elif defined(__ANDROID__)
    (void)windows_compat;
    if (match.status==GenericMatching::MatchStatus::Missing && adapter.explicit_resource) {
        for (size_t i=0;i<candidates.size();++i) {
            const auto& c=candidates[i];
            if (c.renderer && c.key.character==request.character && c.key.resource==request.resource &&
                GenericMatching::SiblingLodPathMatches(request.verified_receiver_path, c.key.path) && candidate_valid(c)) {
                match.status=GenericMatching::MatchStatus::Matched;
                match.index=i; match.valid_count=1;
                Log("SelectCompatibleModelDonor: matched sibling LOD candidate path=" + c.key.path);
                break;
            }
        }
    }
#else
    (void)windows_compat;
#endif
    return match;
}
bool CaptureGenericResourceBindings(const CharacterAdapter& adapter,const BemPocData& bem,void* asset,
    std::vector<PreparedBinding>& bindings) {
    if (!IsGameObjectResource(asset)) { Log("CaptureGenericResourceBindings: !IsGameObjectResource"); return false; }
    if (!ValidatePayloadAdapter(adapter,bem)) { Log("CaptureGenericResourceBindings: !ValidatePayloadAdapter for " + std::string(adapter.world_resource)); return false; }
    bool inactive=true;void* args[]{ModelRendererType(),&inactive};
    void* renderers=Invoke(Contract("game_object.renderers"),asset,args);
    std::vector<GenericRendererCandidate> index;
    if (!BuildGenericRendererIndex(adapter,asset,renderers,index) || index.empty()) {
        Log("CaptureGenericResourceBindings: BuildGenericRendererIndex failed or empty: count=" + std::to_string(index.size()));
        return false;
    }
    std::vector<GenericMatching::Candidate> candidates;
    for (const auto& candidate:index) {
        GenericMatching::Candidate c;c.key=candidate.key;c.renderer=candidate.renderer;
        ReadGenericPristineMesh(adapter,asset,candidate,c.pristine);candidates.push_back(std::move(c));
    }
    std::vector<size_t> chosen;std::vector<void*> unique;
#if defined(_WIN32)
    std::vector<bool> legacy_aurora;
#endif
    for (const auto& component:bem.components) {
        const auto& identity=adapter.components[component.info.component_id];
        GenericMatching::Request request{adapter.id,candidates.front().key.resource,identity.name,
            adapter.explicit_resource?GenericMatching::Region::Explicit:GenericMatching::Region::Lod0,
            identity.indices,!bem.skip_validation,adapter.explicit_resource?identity.receiver_path:""};
        bool compatible_aurora=false;
        const auto match=SelectCompatibleModelDonor(adapter,asset,index,candidates,component.info.component_id,
            component.static_mesh,request,true,compatible_aurora);
        if (match.status!=GenericMatching::MatchStatus::Matched) {
            Log("Generic model donor is missing/ambiguous: "+std::string(identity.name)+
                DescribeGenericDonorFailure(asset,index,candidates,request,match.status,component.static_mesh));return false;
        }
        chosen.push_back(match.index);unique.push_back(index[match.index].renderer);
#if defined(_WIN32)
        legacy_aurora.push_back(compatible_aurora);
#endif
    }
    if (!GenericMatching::DistinctReceivers(unique)) return false;
    for (size_t i=0;i<chosen.size();++i) {
        const auto& c=index[chosen[i]];const auto& component=bem.components[i];PreparedBinding b;
        b.receiver_key=c.key;b.component_id=component.info.component_id;b.renderer=c.renderer;b.original_mesh=c.mesh;
        b.original_materials=c.materials;b.original_bones=c.bones;b.original_enabled=c.enabled;
        if (!UseSavedOriginal(asset,b)) return false;
#if defined(_WIN32)
        if (legacy_aurora[i]) {
            if (!ValidateAuroraLegacyDonor(asset,b)) {
                Log("Aurora legacy donor palette/path/material/bindpose contract refused; original retained.");return false;
            }
            Log("Aurora legacy donor accepted resource="+b.receiver_key.resource+
                " oldIndices=87120 currentIndices=87288 vertices=23141 palette=87 remappedSlots=10");
        }
#endif
        b.custom_mesh=DonorMesh(b);
        // Generated material slots follow output draw order, not donor slots.
        b.material_overrides=component.draw_material_overrides;
        b.custom_enabled=(component.info.flags&kComponentFlagHidden)?false:
            (b.saved_original?b.donor_enabled:b.original_enabled);
        if (b.saved_original) b.custom_bones=DonorBones(b);
#if defined(__ANDROID__)
        if (adapter.explicit_resource && c.renderer) {
            void* trans = Invoke(Contract("component.get_transform"), c.renderer, nullptr);
            void* lod_parent = trans ? Invoke(Contract("transform.get_parent"), trans, nullptr) : nullptr;
            void* mesh_all = lod_parent ? Invoke(Contract("transform.get_parent"), lod_parent, nullptr) : nullptr;
            if (mesh_all && ObjectName(mesh_all) == "Mesh_all") {
                int32_t count = 0;
                if (InvokeValue(Contract("transform.child_count"), mesh_all, nullptr, count) && count > 1 && count <= 16) {
                    bool inactive = true;
                    void* rend_args[]{ModelRendererType(), &inactive};
                    const auto stem_trans = GenericMatching::StripKnownMeshSuffix(ObjectName(trans));
                    for (int32_t i = 0; i < count; ++i) {
                        void* i_arg[]{&i};
                        void* child = Invoke(Contract("transform.get_child"), mesh_all, i_arg);
                        if (child && child != lod_parent) {
                            void* child_go = Invoke(Contract("component.get_game_object"), child, nullptr);
                            void* child_renderers = child_go ? Invoke(Contract("game_object.renderers"), child_go, rend_args) : nullptr;
                            int32_t rcount = ArrayLength(child_renderers);
                            for (int32_t r = 0; r < rcount; ++r) {
                                void* sib_renderer = ArrayValue(child_renderers, r);
                                if (sib_renderer && sib_renderer != c.renderer) {
                                    void* sib_trans = Invoke(Contract("component.get_transform"), sib_renderer, nullptr);
                                    const auto stem_sib = sib_trans ? GenericMatching::StripKnownMeshSuffix(ObjectName(sib_trans)) : std::string_view{};
                                    if (rcount == 1 || stem_trans == stem_sib || (stem_trans.size() > 5 && stem_sib.size() > 5 && stem_trans.substr(0, stem_trans.size() - 5) == stem_sib.substr(0, stem_sib.size() - 5))) {
                                        PreparedBinding::SiblingRenderer sib;
                                        sib.renderer = sib_renderer;
                                        sib.original_mesh = GetRendererMesh(sib_renderer);
                                        sib.original_materials = Invoke(Contract("renderer.get_shared_materials"), sib_renderer, nullptr);
                                        if (!IsStaticRenderer(sib_renderer)) sib.original_bones = GetRendererBones(sib_renderer);
                                        b.sibling_renderers.push_back(sib);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
#endif
        if (!b.custom_mesh) return false;bindings.push_back(std::move(b));
    }
    return !g_construction->failed;
}
#if defined(__ANDROID__)
bool AndroidShadowContractsAvailable() {
    for (const auto* key:{"android.shadow_get","android.shadow_set","android.shadow_mesh_get","android.shadow_mesh_set"}) {
        const auto* method=Contract(key);if (!method || !method->resolved) return false;
    }
    return true;
}
void* PristineShadowMesh(const PreparedBinding& binding) {
    if (binding.saved_original && binding.saved_original->change_shadow)
        return binding.saved_original->shadow_mesh.Get();
    return binding.original_shadow_mesh;
}
bool RestoreSavedShadowState(PreparedBinding& binding) {
    if (!binding.saved_original || !binding.saved_original->change_shadow) return false;
    binding.change_shadow=true;binding.custom_shadow=binding.saved_original->shadow;
    binding.custom_shadow_mesh=binding.saved_original->shadow_mesh.Get();return true;
}
#include "explicit_android_shadows.inc"
#endif
struct PayloadCacheEntry {
    std::filesystem::path path;
    std::string appearance;
    std::string selection_key;
    std::shared_ptr<const BemPocData> payload;
    size_t bytes=0;
    uint64_t expires=0;
};
// Short CPU cache for closely spaced world/UI deliveries of one selection.
// Texture payloads are deferred (decoded per texture at upload and released
// right after LoadRawTextureData), so an entry holds geometry + metadata only.
// Unused roles are never preloaded.
constexpr size_t kPayloadCacheLimit=48u*1024u*1024u;
constexpr uint64_t kPayloadCacheTtlMs=3000;
std::vector<PayloadCacheEntry> g_payload_cache;
void PrunePayloadCache(uint64_t now) {
    std::erase_if(g_payload_cache,[&](const auto& entry){return entry.expires<=now;});
}
bool PayloadGenerationCurrent(const BemPocData& payload) {
    if (!payload.payload_source) return true;
    std::error_code size_error,time_error;
    const auto size=std::filesystem::file_size(payload.payload_source->path,size_error);
    const auto time=std::filesystem::last_write_time(payload.payload_source->path,time_error);
    return !size_error && !time_error && size==payload.payload_source->file_size &&
        static_cast<int64_t>(time.time_since_epoch().count())==payload.payload_source->write_time;
}
std::shared_ptr<const BemPocData> AcquirePayload(const EnabledMod& mod) {
    const auto now=GetTickCount64(); PrunePayloadCache(now);
    std::erase_if(g_payload_cache,[](const auto& entry){return !PayloadGenerationCurrent(*entry.payload);});
    for (auto& entry:g_payload_cache) if (entry.path==mod.package && entry.appearance==mod.appearance && entry.selection_key==mod.selection_key && entry.payload->skip_validation==mod.skip_validation && entry.payload->loading_optimization==mod.loading_optimization) {
        entry.expires=now+kPayloadCacheTtlMs; return entry.payload;
    }
    auto payload=std::make_shared<BemPocData>(); std::string error;
    if (!LoadBem(mod.package,*payload,error,mod.appearance,nullptr,mod.skip_validation,mod.loading_optimization,mod.parameters,UINT64_MAX,true,mod.resource_id) ||
        !ValidatePayloadAdapter(*mod.adapter,*payload)) {
        Log("Package refused for "+std::string(mod.adapter->id)+": "+error); return {};
    }
    size_t size=0;
    for (const auto& component:payload->components) {
        for (const auto& stream:component.streams) size+=stream.size();
        size+=component.indices.size();
    }
    for (const auto& texture:payload->textures) size+=texture.data.size(); // 0 when deferred
    if (size<=kPayloadCacheLimit) {
        size_t held=0; for (const auto& entry:g_payload_cache) held+=entry.bytes;
        while (held+size>kPayloadCacheLimit && !g_payload_cache.empty()) {
            held-=g_payload_cache.front().bytes; g_payload_cache.erase(g_payload_cache.begin());
        }
        g_payload_cache.push_back({mod.package,mod.appearance,mod.selection_key,payload,size,now+kPayloadCacheTtlMs});
    }
    return payload;
}
ModRegistry g_registry;
uint64_t g_model_revision=1;
// Names, rather than adapter pointers, survive several accepted updates before
// the next frame. Discovery resolves them against the current registry.
std::unordered_set<std::string> g_pending_model_resources;
#include "model_asset_cache.inc"
std::filesystem::path g_registry_root;
std::string g_last_registry_text,g_pending_registry_text;
std::mutex g_registry_request_mutex;
uint64_t g_next_registry_scan=0;
std::shared_ptr<OwnedCharacterAdapter> OwnAdapter(const CharacterAdapter& adapter) {
    for (const auto& owned:g_registry.owned_adapters) if (&owned->adapter==&adapter) return owned;
    for (const auto& record:g_completed) if (record.adapter==&adapter && record.adapter_owner) return record.adapter_owner;
    return {};
}
std::string ActiveSelectionKey(const CharacterAdapter& adapter) {
    for (const auto& mod:g_registry.enabled) if (SameAdapter(*mod.adapter,adapter)) return mod.selection_key;
    return {};
}
bool InstallRegistryUpdate(std::string_view text) {
    ModRegistry candidate; std::string error;
    if (!ParseModRegistry(text,g_registry_root,candidate,error)) { Log("Hot switch configuration rejected; previous selection retained: "+error); return false; }
    if (candidate.hot_switch!=g_registry.hot_switch || candidate.skip_validation!=g_registry.skip_validation ||
        candidate.fast_loading!=g_registry.fast_loading || candidate.clone_support!=g_registry.clone_support ||
        candidate.release_retired_resources!=g_registry.release_retired_resources) {
        Log("Hot switch flags changed; restart the game to apply experimental/validation settings."); return false;
    }
    for (const auto& diagnostic:candidate.diagnostics) if (!diagnostic.starts_with("Developer mode:")) {
        Log("Hot switch configuration rejected; previous selection retained: "+diagnostic); return false;
    }
    // Registry parsing validates metadata/selections. Payload work is admitted
    // by the global CPU queue; a failed Job preserves the currently bound model.
    const bool selections_changed=candidate.enabled.size()!=g_registry.enabled.size() ||
        !std::equal(candidate.enabled.begin(),candidate.enabled.end(),g_registry.enabled.begin(),
            [](const EnabledMod& a,const EnabledMod& b){return a.selection_key==b.selection_key;});
    if (selections_changed) {
        const auto changed=[&](const EnabledMod& mod,const char* state) {
            const auto& adapter=*mod.adapter;
            for (const auto* resource:{adapter.world_resource,adapter.ui_resource}) {
                g_pending_model_resources.emplace(resource);
            }
            Log("Hot switch selection changed owner="+std::string(adapter.id)+" world="+adapter.world_resource+
                " ui="+adapter.ui_resource+" state="+state+" package="+mod.package_id+
                " revision="+std::to_string(g_model_revision+1));
        };
        for (const auto& mod:candidate.enabled) {
            const auto old=std::find_if(g_registry.enabled.begin(),g_registry.enabled.end(),[&](const auto& entry){
                return SameAdapter(*entry.adapter,*mod.adapter);
            });
            if (old==g_registry.enabled.end()) changed(mod,"enabled");
            else if (old->selection_key!=mod.selection_key) changed(mod,"changed");
        }
        for (const auto& old:g_registry.enabled) if (std::none_of(candidate.enabled.begin(),candidate.enabled.end(),[&](const auto& mod){
            return SameAdapter(*old.adapter,*mod.adapter);
        })) changed(old,"disabled");
    }
    if (!g_driver_resources.empty() && !DriverOwnerMatchesRegistry(candidate))
        UnloadAnimationDriver("registry-selection-retired");
    g_registry=std::move(candidate);
    // Overlay visibility, unknown fields and dormant selections do not invalidate
    // jobs/assets. Actual model edits keep the established generation path.
    if(selections_changed) {
        ++g_model_revision;
        Log("Experimental hot switch selection accepted; resource discovery pending.");
    }
    return true;
}
void ReloadRegistryAtDelivery() {
    if (!g_hot_switch_runtime.load()) return;
    std::string text;
#if defined(__ANDROID__)
    { std::lock_guard lock(g_registry_request_mutex); text.swap(g_pending_registry_text); }
    if (text.empty()) return;
#else
    const uint64_t now=GetTickCount64(); if (now<g_next_registry_scan) return;
    g_next_registry_scan=now+500;
    // Atomic writers may rename the file during this short read. Share DELETE
    // and close before registry/manifest processing; never hold the settings
    // mutex or a restrictive CRT stream across Unity maintenance.
    try { text=Settings::ReadFile(g_registry_root/"runtime.ini"); }
    catch(const std::exception&) {return;}
#endif
    if (text==g_last_registry_text) return;
    g_last_registry_text=text;
    ++g_registry_generation;
    const bool accepted=InstallRegistryUpdate(text);
#if defined(_WIN32)
    g_model_overlay.Result(text,accepted);
#endif
}
#include "../../../tools/CustomModel/developer-tools/native_probe.inl"
LodState g_lod;
#if defined(__ANDROID__)
#include "android_texture_budget_runtime.inc"
#endif
std::atomic_bool g_enabled{false},g_stopping{false},g_standalone_lod{false},g_shutdown_ack{false};
OriginalMeshPin::~OriginalMeshPin() {
    if (const auto found=g_original_mesh_pins.find(instance_id);found!=g_original_mesh_pins.end() && found->second.expired())
        g_original_mesh_pins.erase(found);
    // Clear only the flag this pin added, while the runtime is serving calls.
    if (!flag_added || !g_host || g_process_terminating.load() || !g_enabled.load()) return;
    try {
        void* mesh=handle.Get(); int32_t flags=0;
        if (!mesh || !IsNativeObjectAlive(mesh) || !InvokeValue(Contract("object.get_hide_flags"),mesh,nullptr,flags)) return;
        flags&=~kHideFlagDontUnloadUnusedAsset; void* args[]{&flags};
        InvokeVoid(Contract("object.set_hide_flags"),mesh,args);
    } catch (...) { /* Release must not throw. */ }
}
std::mutex g_state_mutex,g_shutdown_mutex;
std::condition_variable g_shutdown_cv;
thread_local bool g_in_delivery=false;
uint64_t g_next_prune=0;
using DeliveryFn=void(__fastcall*)(void*,void*,void*);
using PumpFn=void(__fastcall*)(void*);
using RetireHooksFn=BE_Result(BE_CALL*)(void*,const char*);
DeliveryFn g_original_finish=nullptr;
PumpFn g_original_pump=nullptr;
RetireHooksFn g_retire_hooks=nullptr;
void PublishCompleted(CompletedResource&& record,void* asset) {
    CaptureExperimentalGeneratedAssets(record);
    for (auto& old:g_completed) if (old.root.Is(asset)) QueueExperimentalRetiredAssets(old,GetTickCount64());
    // Natural clones may still reference the preceding template generation.
    // Keep only its weak generated-asset lineage; never root old custom assets.
    for (auto& old:g_completed) if (!old.selection_key.empty() && old.root.Is(asset)) old.root.Reset();
    std::erase_if(g_completed,[&](const auto& old){ return old.root.Is(asset); });
    // A receiver that shows its Original Mesh again keeps it alive itself.
    for (auto& binding:record.bindings) if (binding.original && !binding.generated_mesh) binding.original->mesh_pin.reset();
#if defined(__ANDROID__)
    for (auto& binding:record.bindings) if (binding.original && binding.check_shadow &&
        binding.shadow_mesh.Is(binding.original->shadow_mesh.Get())) binding.original->shadow_mesh_pin.reset();
#endif
    g_completed.push_back(std::move(record));
    if (g_clone_support_runtime.load()) {
        try {WatchModelCloneSource(asset);}
        catch (...) {Log("Experimental clone source observation failed; committed resource retained");}
    }
}
bool PrepareDisabledResource(void* asset,const CharacterAdapter* adapter,std::vector<PreparedBinding>& bindings) {
    bool inactive=true; void* args[]{ModelRendererType(),&inactive};
    void* renderers=Invoke(Contract("game_object.renderers"),asset,args);
    for (int i=0;i<ArrayLength(renderers);++i) {
        PreparedBinding binding; binding.renderer=ArrayValue(renderers,i);
        GenericMatching::ReceiverKey key;
        if (!MakeReceiverKey(*adapter,asset,binding.renderer,key)) continue;
        bool known=false;
        for (const auto& record:g_completed) if (SameAdapter(*record.adapter,*adapter)) {
            for (const auto& old:record.bindings) if (old.receiver_key==key && old.original) {
                binding.component_id=old.component_id; known=true; break;
            }
            if (known) break;
        }
        if (!known) continue;
        binding.receiver_key=key;
        binding.original_mesh=GetRendererMesh(binding.renderer);
        binding.original_materials=Invoke(Contract("renderer.get_shared_materials"),binding.renderer,nullptr);
        binding.original_bones=GetRendererBones(binding.renderer);
        if (!GetRendererEnabled(binding.renderer,binding.original_enabled)) {
            if (g_instance_rebind_active) Log("Hot switch Original restore refused resource="+ObjectName(asset)+" receiver="+key.path+" reason=renderer-state-unavailable");return false;
        }
        if (!UseSavedOriginal(asset,binding,true,true) || !binding.saved_original) {
            if (g_instance_rebind_active) Log("Hot switch Original restore refused resource="+ObjectName(asset)+" receiver="+key.path+" reason=saved-binding-unavailable");return false;
        }
        binding.custom_mesh=DonorMesh(binding); binding.custom_materials=DonorMaterials(binding);
        binding.custom_bones=DonorBones(binding); binding.custom_enabled=binding.donor_enabled;
        for (int b=0;b<ArrayLength(binding.custom_bones);++b)
            binding.bone_names.push_back(ObjectName(ArrayValue(binding.custom_bones,b)));
#if defined(__ANDROID__)
        if (binding.saved_original->change_shadow) {
            binding.change_shadow=true; binding.custom_shadow=binding.saved_original->shadow;
            binding.custom_shadow_mesh=binding.saved_original->shadow_mesh.Get();
            if (!InvokeValue(Contract("android.shadow_get"),binding.renderer,nullptr,binding.original_shadow)) return false;
            if (!ReadNullableObject(Contract("android.shadow_mesh_get"),binding.renderer,binding.original_shadow_mesh)) return false;
        }
#endif
        bindings.push_back(std::move(binding));
    }
    BemPocData disabled;
    return !bindings.empty() && PrepareVisualRoot(asset,disabled,bindings);
}
#include "model_receiver_root.inc"
#include "model_clone_lineage.inc"
bool RestoreDisabledResource(void* asset,std::string_view name,ConstructionScope& construction) {
    if (!IsGameObjectResource(asset) || !g_hot_switch_runtime.load()) return false;
    const auto refuse=[&](const char* reason) {
        if (g_instance_rebind_active) Log("Hot switch disable preparation refused resource="+ObjectName(asset)+" reason="+reason);return false;
    };
    name=ResourceBaseName(name);
    const CharacterAdapter* adapter=nullptr;
    for (auto record=g_completed.rbegin();record!=g_completed.rend();++record) {
        if (name!=record->adapter->world_resource && name!=record->adapter->ui_resource) continue;
        if (IsCompletedResource(*record->adapter,asset,"")) return true;
        // The root's own active record identifies it even when the game later
        // rewrote a binding (e.g. materials after a synchronous delivery);
        // PrepareDisabledResource still restores only saved Originals.
        if (IsCompletedResource(*record->adapter,asset,record->selection_key) || record->root.Is(asset)) {
            adapter=record->adapter; break;
        }
        // A scene instance cloned from a committed template is identified by
        // a Mesh this module generated for that role (hot switch rebind).
        bool inactive=true; void* args[]{ModelRendererType(),&inactive};
        void* renderers=Invoke(Contract("game_object.renderers"),asset,args);
        for (int i=0;!adapter && i<ArrayLength(renderers);++i) {
            void* mesh=GetRendererMesh(ArrayValue(renderers,i));
            for (const auto& binding:record->bindings)
                if (binding.generated_mesh && mesh && binding.mesh.Is(mesh)) {adapter=record->adapter;break;}
        }
        if (adapter) break;
    }
    if (!adapter) return refuse("completed-owner-unavailable");
    std::vector<PreparedBinding> bindings;
    if (!PrepareDisabledResource(asset,adapter,bindings)) return refuse("original-bindings-unavailable");
    CompletedResource completed;
    if (!RememberResource(*adapter,asset,bindings,completed) || construction.failed) return refuse("original-record-capture-failed");
    auto* transaction=&bindings;
#if defined(__ANDROID__)
    void* paired_ui_asset=nullptr; void* handle=nullptr; uint32_t handle_root=0;
    struct ReleaseUi { void*& handle; uint32_t& root; ~ReleaseUi(){ betterendfieldnext::AndroidReleaseUiDonor(handle,root); } } release{handle,handle_root};
    CompletedResource paired_completed; std::vector<PreparedBinding> ui_bindings,paired_transaction;
    if (!adapter->explicit_resource && name==adapter->world_resource) {
        paired_ui_asset=RootTemporary(betterendfieldnext::AndroidLoadUiDonor(adapter->ui_resource,handle,handle_root));
        if (!paired_ui_asset) return refuse("paired-UI-donor-unavailable");
        bool paired_modified=false;
        if (!IsCompletedResource(*adapter,paired_ui_asset,"")) for (const auto& old:g_completed)
            if (SameAdapter(*old.adapter,*adapter) && !old.selection_key.empty() &&
                (old.root.Is(paired_ui_asset) || IsCompletedResource(*adapter,paired_ui_asset,old.selection_key))) { paired_modified=true; break; }
        if (paired_modified) {
            if (!PrepareDisabledResource(paired_ui_asset,adapter,ui_bindings) ||
                !RememberResource(*adapter,paired_ui_asset,ui_bindings,paired_completed) || construction.failed) return refuse("paired-UI-original-restore-unavailable");
            paired_transaction=bindings; paired_transaction.insert(paired_transaction.end(),ui_bindings.begin(),ui_bindings.end());
            transaction=&paired_transaction;
        }
    }
#endif
    g_completed.reserve(g_completed.size()+2);
    const auto result=CommitResource<PreparedBinding>(*transaction,ApplyPreparedBinding,RestorePreparedBinding);
    if (result!=CommitResult::Committed) {
        if (result==CommitResult::RestoreFailed) construction.published=true;
        Log("Hot switch disable failed; previous bindings retained: "+std::string(name)); return false;
    }
    construction.published=true; PublishCompleted(std::move(completed),asset);
#if defined(__ANDROID__)
    if (!ui_bindings.empty()) PublishCompleted(std::move(paired_completed),paired_ui_asset);
#endif
    Log("Hot switch restored original resource: "+std::string(name));
    return true;
}

bool ProcessResource(void* asset,ConstructionScope& construction) {
    if (!IsGameObjectResource(asset) || !RootTemporary(asset)) return false;
    asset=ResolveLegacyModelReceiverRoot(asset);
    if (!asset || !RootTemporary(asset)) return false;
    const auto name=ObjectName(asset);
    construction.resource_name=name;
    const EnabledMod* mod=g_registry.Match(name);
    if (!mod) return RestoreDisabledResource(asset,name,construction);
    // Registered clones keep their Unity "(Clone)" suffix; route them like the
    // template they were instantiated from (the matcher strips it as well).
    const std::string_view base_name=ResourceBaseName(name);
    (void)base_name;
#if defined(__ANDROID__)
    auto ensure_ui=[&]() {
        if (mod->adapter->explicit_resource || base_name!=mod->adapter->world_resource) return true;
        void* handle=nullptr; uint32_t root=0;
        struct ReleaseDonor {
            void*& handle; uint32_t& root;
            ~ReleaseDonor() { betterendfieldnext::AndroidReleaseUiDonor(handle,root); }
        } release{handle,root};
        bool ready=false;
        try {
            ConstructionScope ui_scope;
            void* donor=RootTemporary(betterendfieldnext::AndroidLoadUiDonor(mod->adapter->ui_resource,handle,root));
            ready=donor && ProcessResource(donor,ui_scope);
        } catch (...) { ready=false; }
        if (ready && betterendfieldnext::AndroidMeshRollbackTest() && betterendfieldnext::AndroidPipelineLodEnabled()) {
            const bool restored=g_lod.Restore();
            const bool reapplied=restored && g_lod.Update(true);
            Log(std::string("Android LOD restore/reapply ")+(reapplied?"PASS":"FAIL"));
            ready=reapplied;
        }
        Log(std::string("Android paired world/UI ")+
            (betterendfieldnext::AndroidMeshRollbackTest()?"validation ":"publication ")+(ready?"PASS":"FAIL"));
        return ready;
    };
#endif
#if !defined(__ANDROID__)
    // LOD is already requested by configuration; ensure it is actually applied
    // before publishing a package which contains only LOD0 geometry.
    if (!g_lod.Update(EffectiveLodEnabled(!g_registry.enabled.empty(),g_standalone_lod.load()))) {
        Log("LOD prerequisite unavailable; original resource delivered: "+name); return false;
    }
#endif
    PruneCompletedResources();
    if (IsCompletedResource(*mod->adapter,asset,mod->selection_key)) return true;
    auto payload=AcquirePayload(*mod);
    if (!payload) return false;
    std::vector<PreparedBinding> bindings;
    bool prepared=false;
#if defined(__ANDROID__)
    std::vector<PreparedBinding> paired_ui_bindings;
    void* paired_ui_asset=nullptr;
    if (!mod->adapter->explicit_resource && base_name==mod->adapter->world_resource)
        prepared=PrepareAndroidWorldResource(*mod->adapter,*payload,asset,bindings,&paired_ui_bindings,&paired_ui_asset,
            AndroidLodRelations(),AndroidLodAssetScope());
    else
#endif
        prepared=PrepareResource(*mod->adapter,*payload,asset,bindings);
    if (!prepared) {
        Log("Resource preparation failed; original retained: "+name); return false;
    }
    CompletedResource completed;
    if (!RememberResource(*mod->adapter,asset,bindings,completed,mod->selection_key) || construction.failed) return false;
    auto* transaction=&bindings;
#if defined(__ANDROID__)
    CompletedResource paired_completed;
    std::vector<PreparedBinding> paired_transaction;
    if (!paired_ui_bindings.empty()) {
        if (!RememberResource(*mod->adapter,paired_ui_asset,paired_ui_bindings,paired_completed,mod->selection_key) || construction.failed) return false;
        paired_transaction=bindings;
        paired_transaction.insert(paired_transaction.end(),paired_ui_bindings.begin(),paired_ui_bindings.end());
        transaction=&paired_transaction;
    }
#endif
    // Allocate bookkeeping before publication. The final move cannot allocate.
    g_completed.reserve(g_completed.size()+2);
    const auto result=CommitResource<PreparedBinding>(*transaction,ApplyPreparedBinding,RestorePreparedBinding);
    if (result==CommitResult::Committed) {
#if defined(__ANDROID__)
        if (betterendfieldnext::AndroidMeshRollbackTest()) {
            // Bindings already point at generated assets; preserve them if an
            // unexpected diagnostic exception interrupts restoration.
            construction.published=true;
            bool cache_ready=true;
            // Exercise the opposite load order while the UI is still bound:
            // a subsequent world delivery must reuse the verified UI result,
            // not compare its replacement counts to the original BEM counts.
            if (!mod->adapter->explicit_resource && base_name==mod->adapter->ui_resource && g_android_test_world.Get()) {
                g_completed.push_back(std::move(completed));
                cache_ready=false;
                try {
                    ConstructionScope cache_probe;
                    std::vector<PreparedBinding> rebound;
                    cache_ready=PrepareAndroidWorldResource(*mod->adapter,*payload,
                        g_android_test_world.Get(),rebound,nullptr,nullptr,
                        AndroidLodRelations(),AndroidLodAssetScope());
                } catch (...) { cache_ready=false; }
                g_completed.pop_back();
                Log(std::string("Android cached UI donor/world prepare ")+(cache_ready?"PASS":"FAIL"));
            }
            bool restored=true;
            for (auto it=transaction->rbegin();it!=transaction->rend();++it)
                if (!RestorePreparedBinding(*it)) restored=false;
            construction.published=!restored;
            if (restored && !mod->adapter->explicit_resource && base_name==mod->adapter->world_resource) g_android_test_world.Set(asset);
            Log(std::string("Android renderer commit/restore ")+(restored?"PASS: ":"FAIL: ")+name+
                " components="+std::to_string(bindings.size()));
            return restored && cache_ready && ensure_ui();
        }
#endif
        construction.published=true;
        PublishCompleted(std::move(completed),asset);
#if defined(__ANDROID__)
        if (!paired_ui_bindings.empty()) PublishCompleted(std::move(paired_completed),paired_ui_asset);
#endif
        Log("Resource committed: "+name+" components="+std::to_string(bindings.size()));
#if defined(__ANDROID__)
        // The donor may already be cached after our synchronous load, so a
        // later UI request need not hit _FinishWithAsset again. Explicitly
        // process it now through the same guarded transaction.
        return ensure_ui();
#else
        return true;
#endif
    }
    if (result==CommitResult::RestoreFailed) {
        // Never destroy an asset which a failed setter may have left bound.
        // This is a hard engine failure, not a successful or atomic replacement.
        construction.published=true;
        Log("CRITICAL: resource restoration failed; potentially bound assets preserved: "+name);
    } else Log("Resource commit rejected and original bindings restored: "+name);
    return false;
}
#include "model_job_runtime.inc"
// The pre-async delivery transaction: build and commit the template before
// the game receives it, so every later Instantiate copies the replacement.
// Used whenever asynchronous registration cannot be shown to be complete.
std::atomic<uint64_t> g_sync_deliveries{0};
void SynchronousModelDelivery(void* asset,const char* reason) {
    if (!IsGameObjectResource(asset)) return;
    std::lock_guard lock(g_state_mutex);
    if (!g_enabled.load() || g_stopping.load()) return;
    ReloadRegistryAtDelivery();
    // Separate temporary ownership so a failed observation cannot
    // contaminate the following replacement transaction.
    try { ConstructionScope observation; CaptureNativeProbe(asset); }
    catch (const std::exception& error) { Log(std::string("Native probe failed: ")+error.what()); }
    catch (...) { Log("Native probe failed."); }
    if (g_probe.sweep) return;
    if (!g_sync_deliveries.fetch_add(1))
        Log(std::string("Model delivery mode=synchronous reason=")+reason+" (first delivery)");
    ConstructionScope construction;
    const auto name=ObjectName(asset);
    void* receiver=RootTemporary(asset);
    receiver=ResolveLegacyModelReceiverRoot(receiver);
    if (!receiver || !RootTemporary(receiver)) return;
    const EnabledMod* mod=g_registry.Match(ObjectName(receiver));
    const std::string selection=mod?mod->selection_key:std::string{};
    if (mod) Log("Model delivery synchronous resource="+name+" role="+std::string(mod->adapter->id)+" reason="+reason);
    const bool ok=ProcessResource(receiver,construction);
    if (ok && mod && g_clone_support_runtime.load()) WatchModelCloneSource(asset);
    // Record the receiver for hot switch / verification. A refused selection
    // is not handed to an identical asynchronous retry.
    if (mod && !g_probe.sweep) RegisterModelDelivery(receiver,nullptr,true,ok?std::string{}:selection);
}
void __fastcall ResourceFinish(void* proxy,void* asset,void* method) {
    if (g_enabled.load(std::memory_order_acquire) && !g_stopping.load() && !g_in_delivery &&
        IsGameObjectResource(asset)) {
        g_in_delivery=true;
        try {
            if (const char* blocker=ModelAsyncDeliveryBlocker(GetTickCount64())) {
                SynchronousModelDelivery(asset,blocker);
            } else if (!RegisterModelDelivery(asset)) {
                // Queue saturated/rooting failed: never lose the delivery.
                SynchronousModelDelivery(asset,"async-registration-refused");
            } else {
                // Instances cloned before the frame-sliced commit are registered too.
                WatchModelCloneSource(asset);
                if (g_model_deliveries_registered.load()==1)
                    Log("Model delivery mode=frame-sliced (first registration; clone coverage + pump confirmed)");
            }
        } catch (const std::exception& error) { Log(std::string("Resource delivery failed: ")+error.what()); }
        catch (...) { Log("Resource delivery failed with a native C++ exception."); }
        g_in_delivery=false;
    }
    // Unmatched, duplicate, failed and reentrant deliveries all continue the
    // original game chain exactly once. Construction roots end before delivery.
    g_original_finish(proxy,asset,method);
}
void __fastcall ResourcePump(void* method) {
    g_original_pump(method);
    if (!g_enabled.load(std::memory_order_acquire) || g_in_delivery) return;
#if defined(_WIN32)
    g_model_overlay.Tick(); // Win32 process lifetime only; no overlay-to-Unity calls
#endif
    DWORD unconfirmed=0;
    g_pump_thread.compare_exchange_strong(unconfirmed,GetCurrentThreadId());
    if (g_pump_thread.load()!=GetCurrentThreadId()) return;
    try {
        std::lock_guard lock(g_state_mutex);
        if (!g_enabled.load()) return;
        ConstructionScope construction;
        const bool stop=g_stopping.load();
        int32_t frame=0;
        const bool frame_known=InvokeValue(Contract("time.frame_count"),nullptr,nullptr,frame) && frame>=0;
        if (!frame_known) {
            // Never let a missing frame counter stop LOD maintenance as well.
            static bool logged=false;
            if (!logged) { logged=true; Log("Model pump: UnityEngine.Time.get_frameCount unavailable; async Jobs paused, deliveries stay synchronous"); }
        }
        if (!stop) { ReloadRegistryAtDelivery(); if (frame_known) PumpModelJobs(static_cast<uint64_t>(frame)); }
        else CancelModelJobs();

        // Continuous Animation Driver Tick
        static uint64_t s_anim_last_tick = 0;
        uint64_t now_ms = GetTickCount64();
        float dt = (s_anim_last_tick == 0) ? 0.016f : static_cast<float>(now_ms - s_anim_last_tick) / 1000.0f;
        if (dt > 0.1f) dt = 0.1f;
        s_anim_last_tick = now_ms;

        if (!stop) {
            EnsureAnimationDriverLoaded();
            PollAnimationDriverControls();
        }
        if (stop) {
            UnloadAnimationDriver("module-stopping");
        }
        else if (g_animation_driver.HasActiveDrivers() && !g_completed.empty()) {
            DriverSink sink;
            thread_local std::unordered_set<uint64_t> touched;
            touched.clear();
            sink.on_material_float = [&](uint32_t component_id, uint32_t slot,
                                         const std::string& prop, float val, float rest) {
                    WriteDriverMaterial(DriverMaterialKind::Float, component_id, slot, prop, val, {},
                                        false, rest, {}, touched);
                };
            sink.on_material_color = [&](uint32_t component_id, uint32_t slot,
                                         const std::string& prop,
                                         const std::array<float, 4>& rgba,
                                         const std::array<float, 4>& rest) {
                    WriteDriverMaterial(DriverMaterialKind::Color, component_id, slot, prop, 0.0f,
                                        rgba, false, 0.0f, rest, touched);
                };
            sink.on_material_keyword = [&](uint32_t component_id, uint32_t slot,
                                           const std::string& keyword, bool enabled) {
                    WriteDriverMaterial(DriverMaterialKind::Keyword, component_id, slot, keyword,
                                        0.0f, {}, enabled, 0.0f, {}, touched);
                };
            // Morph bindings are collected first and applied once after the tick:
            // a mesh can be driven on several parameters at the same time, and
            // section 10 sums their deltas over one base instead of layering
            // uploads on top of each other.
            std::vector<DriverMorphValue> morph_values;
            sink.on_morph_parameter = [&](const std::string& param, float low, float high, float val) {
                    morph_values.push_back(DriverMorphValue{param, low, high, val});
                };
            g_animation_driver.Tick(dt, sink);
            ApplyDriverMorph(morph_values);
            // Anything this frame did not write has gone back to the package.
            ReleaseDriverMaterials(&touched);
            // Sound attachments follow the same rule: a driver that is not
            // playing this frame simply has no voice. Platform audio mixes with
            // the game's own output, so nothing here pauses or replaces it.
            SyncDriverSounds();
#if defined(_WIN32)
            PublishDriverStateIfChanged();
#endif
        }
        else {
            ApplyDriverMorph({});
            ReleaseDriverMaterials(nullptr);
            SyncDriverSounds();
#if defined(_WIN32)
            // Keep the shared block truthful after a stop, so a later start does
            // not compare against a stale "playing" snapshot.
            PublishDriverStateIfChanged();
#endif
        }
#if defined(__ANDROID__)
        // Keep the configured highest-available-LOD bias for every enabled
        // mobile model, including explicit LOD1 resources. The geometry LOD
        // contract stays unchanged; texture-pool coordination is separate.
        const bool ready=g_lod.MaintainAndroid(stop,g_registry);
        const bool unbinding_pending=!stop && g_android_budget_policy.HasOwnedOverride() &&
            g_hot_switch_runtime.load() && (!g_pending_model_resources.empty() ||
                InstanceRebindOutstanding() || g_instance_rebind_handled!=g_model_revision);
        const bool texture_ready=TickAndroidTextureBudget(!stop &&
            ((g_lod.active && g_lod.applied) || unbinding_pending),stop);
#else
        const bool desired=!stop && EffectiveLodEnabled(!g_registry.enabled.empty(),g_standalone_lod.load());
        const bool ready=g_lod.Update(desired);
#endif
        if (stop && ready
#if defined(__ANDROID__)
            && texture_ready
#endif
        ) { g_shutdown_ack.store(true); g_shutdown_cv.notify_all(); }
        const uint64_t now=GetTickCount64();
        if (!stop) TickExperimentalRetiredResources(now,!g_model_jobs.empty());
        if (now>=g_next_prune) {
            TickNativeProbe();
            PrunePayloadCache(now);
            PruneModelAssets(now);
            PruneModelCloneWatch();
            PruneCompletedResources();
#if defined(__ANDROID__)
            InspectAndroidRenderers();
            if (!stop && (!g_registry.enabled.empty() || g_hot_switch_runtime.load()))
                ObserveAndroidTextureStreamingPeriodic();
#endif
            g_next_prune=now+1000;
        }
    } catch (const std::exception& error) { Log(std::string("Resource maintenance failed: ")+error.what()); }
    catch (...) { Log("Resource maintenance failed."); }
}
// UTF-8, not path::string(): package names are routinely non-ASCII and the
// companion compares this against its own UTF-8 path text.
std::string PackagePathUtf8(const std::filesystem::path& path) {
    const auto bytes=path.u8string();
    return std::string(reinterpret_cast<const char*>(bytes.data()),bytes.size());
}
#if defined(_WIN32)
void PublishAnimationDrivers(const std::filesystem::path& package) {
    std::vector<DriverPanelState> panel;
    for (const auto& summary:g_animation_driver.Summaries())
        panel.push_back(DriverPanelState{summary.id,summary.name,
            static_cast<int>(summary.state),summary.playback==DriverPlayback::Once,
            summary.speed_millis});
    g_model_overlay.PublishDrivers(PackagePathUtf8(package),panel);
}

// The module owns the transition into `finished`, so the companion has to learn
// about it or its button would stay on "Pause" for a driver that already ended.
// Only a real change is published: republishing every frame would race with a
// click the companion just made.
void PublishDriverStateIfChanged() {
    static std::vector<int> published;
    const auto summaries=g_animation_driver.Summaries();
    std::vector<int> current;
    current.reserve(summaries.size());
    for (const auto& summary:summaries) current.push_back(static_cast<int>(summary.state));
    if (current==published) return;
    published=current;
    if (g_animation_driver.LoadedPath().empty()) return;
    g_model_overlay.PublishDriverStates(current);
}
#endif

void ApplyModDriverOverrides(const std::string& package, const std::string& drivers, std::string& applied_overrides) {
    if (drivers.empty() || applied_overrides == package + "\n" + drivers) return;
    applied_overrides = package + "\n" + drivers;
    std::string rem = drivers;
    while (!rem.empty()) {
        auto amp = rem.find('&');
        auto pair = rem.substr(0, amp);
        auto colon = pair.find(':');
        if (colon != std::string::npos) {
            std::string did = pair.substr(0, colon);
            std::string val = pair.substr(colon + 1);
            auto colon2 = val.find(':');
            if (colon2 != std::string::npos) {
                std::string play_str = val.substr(0, colon2);
                std::string speed_str = val.substr(colon2 + 1);
                bool play = (play_str == "play" || play_str == "1" || play_str == "true");
                g_animation_driver.SetPlaying(did, play);
                try {
                    uint32_t sp = static_cast<uint32_t>(std::stoul(speed_str));
                    if (sp >= 250 && sp <= 4000) g_animation_driver.SetSpeedMillis(did, sp);
                } catch (...) {}
            } else {
                bool play = (val == "play" || val == "1" || val == "true");
                g_animation_driver.SetPlaying(did, play);
            }
        }
        rem = (amp == std::string::npos) ? "" : rem.substr(amp + 1);
    }
}

// Section 13 sound payloads live inside the package, but both audio backends
// play from a file, so each one is written once into the model cache directory
// and the runtime keeps that path. Extraction failures only silence the driver:
// the model and the animation clock are never affected.
void MaterializeDriverSounds(const std::filesystem::path& package) {
    const auto sounds = g_animation_driver.Sounds();
    if (sounds.empty()) return;
    if (!DriverAudioAvailable()) {
        Log("[driver-audio] no audio backend on this platform; driver sounds stay silent");
        return;
    }
    std::error_code error;
    const auto cache = g_registry_root / "driver-audio";
    std::filesystem::create_directories(cache, error);
    const uint64_t package_key = Crc32(PackagePathUtf8(package));
    size_t written = 0;
    for (const auto& [driver_id, sound] : sounds) {
        (void)driver_id; // the payload id is what identifies the cached file
        std::string extension = sound.extension;
        for (char& c : extension) c = static_cast<char>(std::tolower(static_cast<unsigned char>(c)));
        if (extension.empty()) extension = "bin";
        const auto target = cache / (std::to_string(package_key) + "-" +
            std::to_string(sound.payload) + "." + extension);
        std::string why;
        if (!ExtractBemPayload(package, sound.payload, target, why)) {
            Log("[driver-audio] payload " + std::to_string(sound.payload) + " unavailable: " + why);
            continue;
        }
        g_animation_driver.SetSoundPathByPayload(sound.payload, PackagePathUtf8(target));
        ++written;
    }
    if (written)
        Log("[driver-audio] cached " + std::to_string(written) + " driver sound payload(s)");
}

void EnsureAnimationDriverLoaded() {
    static uint64_t last_registry_gen=0;
    static std::unordered_set<std::string> driverless_packages;
    static std::unordered_map<std::string,uint64_t> retry_at;
    if (last_registry_gen!=g_registry_generation) {
        last_registry_gen=g_registry_generation;
        driverless_packages.clear(); retry_at.clear();
    }
    if (DriverOwnerMatchesRegistry(g_registry)) {
        const auto& path=g_animation_driver.LoadedPath();
        for (const auto& mod:g_registry.enabled) if (PackagePathUtf8(mod.package)==path) {
            ApplyModDriverOverrides(path,mod.drivers,g_driver_applied_overrides);
            return;
        }
    }
    UnloadAnimationDriver(g_registry.enabled.empty()?"all-models-disabled":"owner-changed");
    // Parse into a candidate: probing an ordinary or invalid package must never
    // mutate the active clock, controls, audio or ownership.
    const uint64_t now=GetTickCount64();
    for (const auto& mod:g_registry.enabled) {
        const std::string package=PackagePathUtf8(mod.package);
        if (driverless_packages.contains(package)) continue;
        const auto retry=retry_at.find(package);
        if (retry!=retry_at.end() && now<retry->second) continue;
        BemPackageInfo info; std::string why;
        if (!ReadBemManagementInfo(mod.package,info,why,true) || info.manifest_json.empty()) {
            retry_at[package]=now+5000;
            Log("[AnimDriver] Manifest unavailable; retry deferred: "+package+" reason="+why);
            continue;
        }
        AnimationDriverHost candidate;
        candidate.SetLogger([](const std::string& message){Log(message);});
        if (!candidate.LoadFromManifest(info.manifest_json,package)) {
            driverless_packages.insert(package); continue;
        }
        g_animation_driver=std::move(candidate);
        for (const auto& owner:g_registry.enabled) if (PackagePathUtf8(owner.package)==package)
            g_driver_resources.push_back({owner.adapter,OwnAdapter(*owner.adapter),owner.selection_key});
        MaterializeDriverSounds(mod.package);
        retry_at.erase(package);
#if defined(__ANDROID__)
        ReapplyAndroidDriverOverrides();
#endif
        ApplyModDriverOverrides(package,mod.drivers,g_driver_applied_overrides);
#if defined(_WIN32)
        PublishAnimationDrivers(mod.package);
#endif
        Log("[AnimDriver] Owner loaded: package="+package+" resources="+
            std::to_string(g_driver_resources.size())+" drivers="+std::to_string(g_animation_driver.DriverCount()));
        return;
    }
}

void PollAnimationDriverControls() {
#if defined(_WIN32)
    thread_local std::vector<DriverPanelState> panel;
    if (g_model_overlay.PollDriverControls(panel)) {
        for (const auto& state : panel) {
            g_animation_driver.ApplyState(state.id, static_cast<DriverState>(state.state));
            g_animation_driver.SetSpeedMillis(state.id, state.speed_millis);
        }
    }
    // Vertex upload A/B switch from the overlay. Applies from the next morph
    // frame; nothing is rebuilt because both paths start from the same base.
    int mode = kVertexUploadChannel;
    if (g_model_overlay.PollVertexUploadMode(mode) && g_vertex_upload_available[mode])
        g_vertex_upload_mode.store(mode, std::memory_order_relaxed);
#endif
}

#if defined(__ANDROID__)
// Android has no shared-memory panel, so the JNI bridge is the control surface.
// It carries the same four-state value the desktop panel uses, which keeps pause
// and stop distinct on both platforms.
struct AndroidDriverOverride { int state = 0; uint32_t speed_millis = 1000; };
static std::unordered_map<std::string, AndroidDriverOverride> s_driver_overrides;
void ClearAndroidDriverOverrides() {s_driver_overrides.clear();}

void ReapplyAndroidDriverOverrides() {
    for (const auto& entry : s_driver_overrides) {
        g_animation_driver.ApplyState(entry.first, static_cast<DriverState>(entry.second.state));
        g_animation_driver.SetSpeedMillis(entry.first, entry.second.speed_millis);
    }
}

bool SetAndroidDriver(const char* id, int state, int speed_millis) {
    if (!id || *id == '\0') return false;
    std::lock_guard lock(g_state_mutex);
    if (g_driver_resources.empty() || g_animation_driver.DriverCount()==0) return false;
    const int clamped = state < 0 ? 0 : (state > 2 ? 2 : state);
    const uint32_t sp = (speed_millis >= 250 && speed_millis <= 4000)
        ? static_cast<uint32_t>(speed_millis) : 1000;
    s_driver_overrides[id] = AndroidDriverOverride{clamped, sp};
    g_animation_driver.ApplyState(id, static_cast<DriverState>(clamped));
    g_animation_driver.SetSpeedMillis(id, sp);
    return true;
}

std::string GetAndroidDriversJson() {
    std::lock_guard lock(g_state_mutex);
    std::string json = "[";
    bool first = true;
    for (const auto& s : g_animation_driver.Summaries()) {
        if (!first) json += ",";
        first = false;
        json += "{\"id\":\"" + s.id + "\",\"name\":\"" + s.name +
            "\",\"state\":" + std::to_string(static_cast<int>(s.state)) +
            ",\"once\":" + (s.playback == DriverPlayback::Once ? "true" : "false") +
            ",\"speed_millis\":" + std::to_string(s.speed_millis) + "}";
    }
    json += "]";
    return json;
}
#endif

bool ReadRuntimeRegistry() {
    std::array<char,4096> catalog{};
    if (g_host->copy_catalog_root(g_host->context,catalog.data(),catalog.size())<=0) return false;
    const auto root=Utf8Path(catalog.data())/"custom-model";
    g_registry_root=root;
    ++g_registry_generation;
    try { ReadProbeRequest(root); }
    catch (const std::exception& error) { g_probe.active=false; WriteProbeStatus("request_failed",error.what()); Log(std::string("Native probe disabled: ")+error.what()); }
    std::string text;
#if defined(__ANDROID__)
    // Android modules do not have permission to create a sibling directory
    // below /data/local/tmp.  The host supplies the generated registry in
    // memory while package paths may still point at readable absolute files.
    std::vector<char> configured(1024*1024+1);
    const int configured_size = g_host->copy_module_configuration(
        g_host->context, kModuleId, configured.data(), configured.size());
    if (configured_size > 0) text.assign(configured.data(),
        static_cast<size_t>(configured_size));
#else
    try {text=Settings::ReadFile(root/"runtime.ini");}
    catch(const std::exception&) {return false;}
#endif
    std::string error;
    if (!ParseModRegistry(text,root,g_registry,error)) { Log(error); return false; }
    g_fast_loading=g_registry.fast_loading;
    Log(std::string("Model loading mode=")+(g_fast_loading?"fast (render sync every 128MiB)":"low-peak (render sync every 4MiB)"));
    g_last_registry_text=text;
    g_hot_switch_runtime.store(g_registry.hot_switch);
    g_clone_support_runtime.store(g_registry.clone_support);
    g_release_retired_resources_runtime.store(g_registry.release_retired_resources);
    Log(std::string("[RetiredResourceCleanup] enabled=")+(g_registry.release_retired_resources?"1 delayMs=1000":"0"));
    g_model_clone_hooks_requested=g_registry.clone_support;
    Log(std::string("Experimental clone support=")+(g_registry.clone_support?"on; template delivery remains synchronous":"off"));
    if(g_probe.sweep) {
        g_registry.enabled.clear();
        Log("Native sweep session: model replacement paused in memory; saved configuration unchanged.");
    }
    g_standalone_lod.store(g_registry.standalone_lod);
    for (const auto& message:g_registry.diagnostics) Log(message);
    return true;
}
bool ResolveRuntimeContracts() {
    const bool models=!g_registry.enabled.empty() || g_probe.active || g_registry.hot_switch;
    for (auto& method:g_methods) {
        const std::string_view key(method.key);
        if (key.starts_with("probe.") && !g_probe.active) continue;
#if defined(__ANDROID__)
        if (key.starts_with("quality.") ||
            (!betterendfieldnext::AndroidPipelineLodEnabled() && (key.starts_with("pipeline.") || key.starts_with("culling.")))) continue;
#endif
        const bool lod=key.starts_with("pipeline.") || key.starts_with("quality.") ||
            key.starts_with("culling.") ||
            key=="pump.canvas_will_render" || key=="time.frame_count" || key=="object.instance_id" || key=="object.is_alive";
        if (!models && !lod) continue;
        BE_ResolvedMethodV1 resolved{};
        method.resolved=g_host->resolve_method(g_host->context,&method.descriptor,&resolved)==BE_Result_Ok && resolved.method_info;
        method.method_info=resolved.method_info; method.pointer=resolved.method_pointer;
        if (key.starts_with("probe.") && !method.resolved) {
            WriteProbeStatus("contract_unavailable",key);
            Log("Native probe contract unavailable: "+std::string(key));
            g_probe.active=false;
        }
        if (!method.resolved && method.required) {
            Log("Required contract unavailable: "+std::string(key)); return false;
        }
    }
    const auto resolve_class=[&](const char* name,BE_ResolvedClassV1& result) {
        return g_host->resolve_class(g_host->context,"UnityEngine.CoreModule.dll","UnityEngine",name,&result)==BE_Result_Ok &&
            result.class_info && result.type_object;
    };
    if (models && (!resolve_class("GameObject",g_game_object_class) ||
        !resolve_class("Mesh",g_mesh_class) || !resolve_class("SkinnedMeshRenderer",g_skinned_renderer_class) ||
        !resolve_class("Renderer",g_renderer_class) || !resolve_class("MeshRenderer",g_static_renderer_class) ||
        !resolve_class("MeshFilter",g_mesh_filter_class) ||
        !resolve_class("Texture2D",g_texture2d_class) || !resolve_class("Material",g_material_class) ||
        !resolve_class("Vector3",g_vector3_class))) return false;
    HMODULE game=GetModuleHandleW(L"GameAssembly.dll");
    if (!game) return false;
    g_il2cpp_array_new = reinterpret_cast<ArrayNewFn>(GetProcAddress(game, "il2cpp_array_new"));
    g_il2cpp_array_header_size = reinterpret_cast<ArrayHeaderSizeFn>(GetProcAddress(game, "il2cpp_array_object_header_size"));
    g_weak_new=reinterpret_cast<WeakNewFn>(GetProcAddress(game,"il2cpp_gchandle_new_weakref"));
    g_weak_target=reinterpret_cast<WeakTargetFn>(GetProcAddress(game,"il2cpp_gchandle_get_target"));
    g_static_get=reinterpret_cast<StaticFieldFn>(GetProcAddress(game,"il2cpp_field_static_get_value"));
    g_static_set=reinterpret_cast<StaticFieldFn>(GetProcAddress(game,"il2cpp_field_static_set_value"));
    // Object class is mandatory before accepting an AssetProxy receiver.
    // Typed array allocation remains optional for v24.
    g_object_class=reinterpret_cast<ObjectClassFn>(GetProcAddress(game,"il2cpp_object_get_class"));
    g_array_new_specific=reinterpret_cast<ArrayNewSpecificFn>(GetProcAddress(game,"il2cpp_array_new_specific"));
#if defined(_WIN32)
    Log(std::string("[AnimDriver] Contract check: add_blend_shape_frame=") + (Contract("mesh.add_blend_shape_frame") && Contract("mesh.add_blend_shape_frame")->resolved ? "OK" : "FAILED") +
        " get_blend_shape_count=" + (Contract("mesh.get_blend_shape_count") && Contract("mesh.get_blend_shape_count")->resolved ? "OK" : "FAILED") +
        " get_blend_shape_name=" + (Contract("mesh.get_blend_shape_name") && Contract("mesh.get_blend_shape_name")->resolved ? "OK" : "FAILED") +
        " set_blend_shape_weight=" + (Contract("skinned.set_blend_shape_weight") && Contract("skinned.set_blend_shape_weight")->resolved ? "OK" : "FAILED") +
        " vector3=" + (g_vector3_class.class_info ? "OK" : "FAILED") +
        " array_new=" + (g_il2cpp_array_new ? "OK" : "FAILED"));
#endif
    return g_weak_new && g_weak_target &&
#if !defined(__ANDROID__)
        g_lod.Resolve() &&
#else
        (!betterendfieldnext::AndroidNpcParametersEnabled() || g_lod.Resolve()) &&
#endif
        (!models || (g_object_class && ResolveEngineBindings()));
}
BE_Result BE_CALL InitializeResourceModule(const BE_HostApiV1* host) {
#if defined(__ANDROID__)
    // The private Android platform adapter must be configured before this
    // shared transaction can be enabled. Never resolve PC raw setters here.
    if (!betterendfieldnext::AndroidMeshBuilderReady()) return BE_Result_NotReady;
#endif
    if (!host || host->abi_version!=BETTER_ENDFIELD_NEXT_MODULE_ABI_V1 || !host->log || !host->resolve_method ||
        !host->resolve_field || !host->resolve_class || !host->create_hook || !host->copy_catalog_root ||
        !host->copy_managed_string || !host->runtime_invoke || !host->object_new || !host->object_unbox ||
        !host->string_new || !host->gchandle_new || !host->gchandle_free) return BE_Result_InvalidArgument;
    g_host=host;
    try {
        BeginProbeStatus();
        g_animation_driver.SetLogger([](const std::string& msg){ Log(msg); });
        SetDriverAudioLogger([](const std::string& msg){ Log(msg); });
    if (!ReadRuntimeRegistry() || !ResolveRuntimeContracts()) { WriteProbeStatus("initialization_failed"); return BE_Result_ContractMismatch; }
#if defined(__ANDROID__)
        g_retire_hooks=host->release_module_hooks;
        if (!g_retire_hooks) return BE_Result_ContractMismatch;
#else
        HMODULE host_module=nullptr;
        if (!GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS|GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
            reinterpret_cast<LPCWSTR>(host->create_hook),&host_module)) return BE_Result_ContractMismatch;
        g_retire_hooks=reinterpret_cast<RetireHooksFn>(GetProcAddress(host_module,"BetterEndfieldNext_RetireModuleHooksV1"));
        if (!g_retire_hooks) { Log("Host lacks safe hook retirement; update Host and CustomModel together."); return BE_Result_ContractMismatch; }
        HMODULE pinned=nullptr;
        if (!GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS|GET_MODULE_HANDLE_EX_FLAG_PIN,
            reinterpret_cast<LPCWSTR>(&InitializeResourceModule),&pinned)) return BE_Result_Failed;
#endif
        const auto install=[&](const char* key,void* detour,void** original) {
            auto* method=Contract(key);
            return method && method->pointer && host->create_hook(host->context,kModuleId,method->pointer,detour,original)==BE_Result_Ok && *original;
        };
        if (!install("pump.canvas_will_render",reinterpret_cast<void*>(&ResourcePump),reinterpret_cast<void**>(&g_original_pump)) ||
#if defined(__ANDROID__)
            (betterendfieldnext::AndroidPipelineLodEnabled() && (
                !install("pipeline.register_bias",reinterpret_cast<void*>(&AndroidRegisterLodBias),reinterpret_cast<void**>(&g_original_register_bias)) ||
                !install("culling.set_parent_lod_bias",reinterpret_cast<void*>(&ParentLodBias),reinterpret_cast<void**>(&g_original_parent_lod_bias)) ||
                !install("culling.set_art_tag_lod_bias",reinterpret_cast<void*>(&ArtTagLodBias),reinterpret_cast<void**>(&g_original_art_tag_lod_bias)))) ||
#endif
#if !defined(__ANDROID__)
            !install("culling.set_parent_lod_bias",reinterpret_cast<void*>(&ParentLodBias),reinterpret_cast<void**>(&g_original_parent_lod_bias)) ||
            !install("culling.set_art_tag_lod_bias",reinterpret_cast<void*>(&ArtTagLodBias),reinterpret_cast<void**>(&g_original_art_tag_lod_bias)) ||
#endif
            ((!g_registry.enabled.empty() || g_probe.active || g_registry.hot_switch) && !install("resource.finish",reinterpret_cast<void*>(&ResourceFinish),reinterpret_cast<void**>(&g_original_finish)))) {
            g_retire_hooks(host->context,kModuleId);
            Log("Hook installation failed; entry points retired, module remains disabled.");
            return BE_Result_Failed;
        }
        g_pump_thread.store(0);g_model_frame=UINT64_MAX;g_model_foreground_streak=0;g_model_pump_ms.store(0);
        g_sync_deliveries.store(0);g_model_deliveries_registered.store(0);g_model_deliveries_dropped.store(0);
        g_model_frame_budget=FrameBudget{FrameBudget::Config{32*kLoadingMiB,256*kLoadingMiB,8,2,std::chrono::milliseconds(2)}};
        ResetInstanceRebinds();
#if defined(__ANDROID__)
        g_android_texture_diagnostics={};
        InitializeAndroidTextureBudget();
        ResetAndroidLodStreamingObserver();
        auto* offset_contract=Contract("android.lod_streaming_offset");
        void* offset_entry=offset_contract && offset_contract->resolved
            ? betterendfieldnext::AndroidLodStreamingOffsetEntry() : nullptr;
        const bool offset_observer=offset_entry && host->create_hook(host->context,kModuleId,offset_entry,
            reinterpret_cast<void*>(&AndroidObserveLodStreamingOffset),
            reinterpret_cast<void**>(&g_original_android_lod_streaming_offset))==BE_Result_Ok && g_original_android_lod_streaming_offset;
        g_android_lod_streaming_observer_installed.store(offset_observer,std::memory_order_release);
        Log(offset_observer ? "Android LOD streaming offset observer active: original values forwarded; no policy writes" :
            "Android LOD streaming offset observer unavailable; model/LOD policy unchanged");
#endif
        g_stopping.store(false); g_shutdown_ack.store(false); g_enabled.store(true,std::memory_order_release);
        InitializeModelJobs();
        InstallModelCloneHooks();
        if (g_probe.active) {
            if(g_probe.sweep && !g_probe.persistent) {
                const auto consumed=g_probe.request.parent_path()/(g_probe.run+".started");
                if(!MoveFileExW(g_probe.request.c_str(),consumed.c_str(),MOVEFILE_REPLACE_EXISTING)) {
                    g_probe.active=false; WriteProbeStatus("request_consume_failed");
                }
            }
            if(g_probe.active) WriteProbeStatus("ready");
        }
        Log("Resource runtime enabled: mods="+std::to_string(g_registry.enabled.size())+
            " standaloneLOD="+std::to_string(g_standalone_lod.load()));
#if defined(__ANDROID__)
        Log(std::string("Android replacement mode=")+
            (betterendfieldnext::AndroidMeshRollbackTest()?"rollback (original bindings restored)":"replace (bindings retained)")+
            (betterendfieldnext::AndroidNpcParametersEnabled()?"; pipeline + NPC parameters; geometry quality unchanged":
            betterendfieldnext::AndroidPipelineLodEnabled()?"; pipeline bias only; quality/NPC/camera culling unchanged":
                "; global LOD/culling overrides disabled"));
#endif
#if defined(_WIN32)
        if(!g_model_overlay.Start(g_registry_root,g_hot_switch_runtime.load(),
            reinterpret_cast<const void*>(&InitializeResourceModule)))
            Log("Model overlay IPC initialization failed; resource runtime remains available.");
#endif
        return BE_Result_Ok;
    } catch (const std::exception& error) {
#if defined(_WIN32)
        g_model_overlay.Stop();
#endif
        g_enabled.store(false);
        DisableModelCloneHooks();
        if (g_model_loader) g_model_loader->Shutdown();
        if (g_texture_streamer) g_texture_streamer->Shutdown();
        if (g_retire_hooks) g_retire_hooks(host->context,kModuleId);
        Log(std::string("Initialization failed: ")+error.what()); return BE_Result_Failed;
    }
}
BE_Result BE_CALL ResourceConfigurationChanged(const char* configuration) {
    if (!configuration) return BE_Result_InvalidArgument;
    if (std::string_view(configuration).starts_with("[CustomModel]")) {
        if (!g_hot_switch_runtime.load()) return BE_Result_NotReady;
        if (std::strlen(configuration)>1024*1024) return BE_Result_InvalidArgument;
        std::lock_guard lock(g_registry_request_mutex);
        g_pending_registry_text=configuration;
        return BE_Result_Ok;
    }
    // Ordinary Host settings still carry only the independent LOD preference.
    std::string_view remaining(configuration);
    while (!remaining.empty()) {
        const auto end=remaining.find('\n'); auto line=remaining.substr(0,end);
        if (end==remaining.npos) remaining={}; else remaining.remove_prefix(end+1);
        if (line.ends_with('\r')) line.remove_suffix(1);
        constexpr std::string_view key="StandaloneLod=";
        if (!line.starts_with(key)) continue;
        const auto value=line.substr(key.size());
        if (value=="true" || value=="1") g_standalone_lod.store(true);
        else if (value=="false" || value=="0") g_standalone_lod.store(false);
        else return BE_Result_InvalidArgument;
    }
    return BE_Result_Ok;
}
void BE_CALL ShutdownResourceModule() {
    if (!g_host) return;
#if defined(_WIN32)
    g_model_overlay.Stop();
#endif
    g_stopping.store(true);
    bool need_restore=false;
    {
        std::lock_guard lock(g_state_mutex); need_restore=g_lod.active || !g_model_jobs.empty();
#if defined(__ANDROID__)
        need_restore=need_restore || g_android_budget_policy.HasOwnedOverride() || AndroidBudgetHasLease();
#endif
        need_restore=need_restore || g_animation_driver.DriverCount()!=0;
        if (need_restore && g_pump_thread.load()==GetCurrentThreadId()) {
            ConstructionScope construction;
            UnloadAnimationDriver("module-shutdown");
            CancelModelJobs();
            bool restored=g_lod.Restore();
#if defined(__ANDROID__)
            restored=TickAndroidTextureBudget(false,true) && restored;
#endif
            g_shutdown_ack.store(restored);
        }
    }
    if (need_restore && !g_shutdown_ack.load()) {
        std::unique_lock lock(g_shutdown_mutex);
        g_shutdown_cv.wait_for(lock,std::chrono::seconds(2),[]{return g_shutdown_ack.load();});
        if (!g_shutdown_ack.load()) Log("LOD/texture-pool restoration was not acknowledged on the Unity thread before shutdown.");
    }
    g_enabled.store(false,std::memory_order_release);
    DisableModelCloneHooks();
    if (g_model_loader) g_model_loader->Shutdown();
    if (g_texture_streamer) g_texture_streamer->Shutdown();
    g_lod_bias_locked.store(false,std::memory_order_release);
    const bool hooks_retired=g_retire_hooks && g_retire_hooks(g_host->context,kModuleId)==BE_Result_Ok;
    if (g_retire_hooks && !hooks_retired)
        Log("Hook disable reported failure; pinned inactive detours remain pass-through.");
#if defined(__ANDROID__)
    if (hooks_retired) g_android_lod_streaming_observer_installed.store(false,std::memory_order_release);
    if (hooks_retired) g_android_budget_hooks_ready.store(false,std::memory_order_release);
#endif
    std::lock_guard lock(g_state_mutex);
    if (g_model_jobs.empty()) { g_model_loader.reset(); g_texture_streamer.reset(); }
    // Pending scene work owns strong roots. It belongs to this module lifetime,
    // never to a later start, even though published bindings stay in the game.
    ResetInstanceRebinds();g_pending_model_resources.clear();
    StopAllDriverSounds(); g_sounding_drivers.clear();
    g_animation_driver.Reset(); g_driver_resources.clear(); g_driver_applied_overrides.clear();
    g_driver_morph_active=false; g_driver_dirty_meshes.clear(); g_driver_material_writes.clear();
    g_dynamic_meshes.clear();
#if defined(__ANDROID__)
    ClearAndroidDriverOverrides();
#endif
    g_retired_generated_assets.clear();g_release_retired_resources_runtime.store(false);
    g_payload_cache.clear(); g_completed.clear(); g_lod.pipeline.Reset();
    g_generated_texture_identity.clear(); g_generated_texture_order.clear();
    g_model_assets.clear();g_model_plans.clear();g_model_targets.clear();g_model_file_leases.clear();
    g_cpu_geometry.clear();g_cpu_geometry_bytes=0;g_cpu_geometry_serial=0;
    g_hot_switch_runtime.store(false);
    // Model bindings are intentionally not rolled back on module shutdown.
}
const BE_ModuleApiV1 kResourceApi{
    {kModuleId,"Custom Model","0.1.0",BETTER_ENDFIELD_NEXT_MODULE_ABI_V1},
    InitializeResourceModule,ResourceConfigurationChanged,ShutdownResourceModule
};
}
BE_EXPORT const BE_ModuleApiV1* BE_CALL BetterEndfieldNext_GetModuleApiV1() { return &kResourceApi; }
BE_EXPORT BE_Result BE_CALL BetterEndfieldNext_QueryCustomModelGeometryV1(void* mesh,
    BE_CustomModelGeometryVisitorV1 visitor,void* context) {
    if(!mesh || !visitor) return BE_Result_InvalidArgument;
    if(!g_host || !g_enabled.load() || g_stopping.load() || g_in_delivery || g_pump_thread.load()!=GetCurrentThreadId()) return BE_Result_NotReady;
    try {
        std::unique_lock lock(g_state_mutex,std::try_to_lock);
        if(!lock.owns_lock()) return BE_Result_NotReady;
        ConstructionScope temporary;
        PruneCpuGeometry();
        for(const auto& entry:g_cpu_geometry) if(entry->mesh.Get()==mesh) {
            entry->used=++g_cpu_geometry_serial;
            const BE_CustomModelGeometryV1 view{sizeof(BE_CustomModelGeometryV1),BETTER_ENDFIELD_NEXT_CUSTOM_MODEL_GEOMETRY_V1,
                entry->vertices,static_cast<uint32_t>(entry->indices.size()),entry->positions.data(),entry->skin.data(),entry->stride,
                entry->indices.data(),entry->draws.data(),static_cast<uint32_t>(entry->draws.size())};
            return visitor(context,&view);
        }
        return BE_Result_NotFound;
    } catch(...) {return BE_Result_Failed;}
}
}
#if defined(__ANDROID__)
namespace betterendfieldnext {
bool AndroidSetCustomModelDriver(const char* id, int state, int speed_millis) {
    return BetterEndfieldNext::CustomModel::SetAndroidDriver(id, state, speed_millis);
}

std::string AndroidGetCustomModelDriversJson() {
    return BetterEndfieldNext::CustomModel::GetAndroidDriversJson();
}
} // namespace betterendfieldnext
#endif

BOOL WINAPI DllMain(HINSTANCE instance,DWORD reason,LPVOID reserved) {
    if (reason==DLL_PROCESS_ATTACH) DisableThreadLibraryCalls(instance);
    if (reason==DLL_PROCESS_DETACH && reserved) BetterEndfieldNext::CustomModel::g_process_terminating.store(true);
    return TRUE;
}
