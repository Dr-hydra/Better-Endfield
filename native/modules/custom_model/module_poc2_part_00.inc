#include "BetterEndfield/ModuleApi.h"

#include <Windows.h>

#include <algorithm>
#include <array>
#include <atomic>
#include <cctype>
#include <cmath>
#include <cstdint>
#include <cstdio>
#include <cstring>
#include <filesystem>
#include <fstream>
#include <mutex>
#include <string>
#include <string_view>
#include <vector>

namespace BetterEndfield::CustomModelModule {
namespace {

constexpr char kModuleId[] = "betterendfield.custom_model";
constexpr char kTargetRendererName[] = "S_actor_endminf_cloth_01_lod0";
constexpr char kTargetMeshName[] = "S_actor_endminf_cloth_01_lod0";
constexpr char kActorMeshPrefix[] = "S_actor_endminf_";
constexpr char kDefaultBemRelativePath[] = "custom-model/endmin-casualwear-c9.bempoc";
constexpr int kToggleHotkey = VK_F12;
constexpr int kRollbackHotkey = VK_F11;
// The F10 diagnostic box and F11 baked-identity controls are gone. They existed
// to separate "this renderer will not display a runtime-created Mesh at all"
// from "the geometry or its channels are wrong", which the working
// declaration-reproducing path has since settled; their reasoning is kept in
// docs/CUSTOM_MODEL_POC22_RAW_STREAM.md.
//
// F8 reads the renderer's GraphicsBuffer metadata. Kept on its own key because
// PoC-3 crashed the client on the Mesh-level equivalent.
constexpr int kBufferProbeHotkey = VK_F8;
// F7 validates the assumed native ABI of the stripped engine bindings against
// answers already known from the managed enumeration API, before any writer is
// trusted. A wrong ABI crashes the client, so this must pass first.
constexpr int kAbiProbeHotkey = VK_F7;
// F6 reports the C9 material's texture slots so the EFMI DDS set can be joined
// against them offline. Read-only.
constexpr int kTextureProbeHotkey = VK_F6;
// 0 arms a short multi-frame skin diagnostic: every lod0 renderer's original
// index count, vertex declaration, stream strides and material texture slots.
// This is what extending the replacement past Component9 is derived from,
// because the EFMI sample's components do not share one vertex layout.
// Read-only.
constexpr int kModelProbeHotkey = '0';
constexpr int32_t kModelProbeCaptureCount = 12;
constexpr int32_t kModelProbeCaptureIntervalFrames = 30;

// F4 toggles the components the mod hides by default. In the Endmin Casualwear
// sample those are its two mask draws, and hiding them is the mod's own default
// state, so F4 turns the mask back on rather than off.
constexpr int kMaskHotkey = VK_F4;

// Scan the full managed array. Resources includes loaded prefab assets as well
// as instances; real scenes already exceed 4096 entries.
constexpr uint32_t kMaxBemComponents = 64;
constexpr int kMaxSubMeshes = 256;
constexpr uint32_t kComponent9OriginalIndexCount = 101994;
constexpr uint32_t kExpectedBemVertices = 12873;
constexpr uint32_t kExpectedBemIndices = 59073;
constexpr uint32_t kExpectedBemMaxBone = 155;

// This research PoC intentionally uses the same 64-bit IL2CPP array ABI that
// other repository probes already validate: max_length @ 0x18, data @ 0x20.
constexpr size_t kIl2CppArrayLengthOffset = 0x18;
constexpr size_t kIl2CppArrayDataOffset = 0x20;
constexpr int32_t kBoxedValueHeaderSize = 16;

constexpr uint32_t kBemFlagIndex16 = 1u << 2;
constexpr uint32_t kBemFlagRawStream0 = 1u << 5;
constexpr uint32_t kBemFlagRawStream1 = 1u << 6;
constexpr uint32_t kBemFlagRawStream2 = 1u << 7;
constexpr uint32_t kBemFlagTextures = 1u << 8;
constexpr uint64_t kActiveRefreshIntervalMs = 500;
constexpr uint32_t kMaxBemTextures = 32;
constexpr uint32_t kMaxBemTextureBytes = 64u * 1024u * 1024u;

#pragma pack(push, 1)
struct BemFileHeader {
    char magic[8];
    uint32_t version;
    uint32_t component_count;
    uint32_t texture_count;
    uint32_t flags;
    uint32_t reserved;
};
#pragma pack(pop)
static_assert(sizeof(BemFileHeader) == 28);

#pragma pack(push, 1)
struct BemComponentHeaderRaw {
    uint32_t component_id;
    // The join key onto the live model: the index count of the draw this
    // component replaces, which equals the renderer's summed submesh index
    // count. Names are deliberately not used.
    uint32_t original_index_count;
    uint32_t vertex_count;
    uint32_t index_count;
    uint32_t max_bone;
    uint32_t stream_count;
    uint32_t stride0;
    uint32_t stride1;
    uint32_t stride2;
    uint32_t index_element_size;
    uint32_t reserved0;
    uint32_t flags;
    uint32_t reserved1;
};
#pragma pack(pop)
static_assert(sizeof(BemComponentHeaderRaw) == 52);

// A component the mod does not replace is still carried, so the runtime can
// identify its renderer while working out which components bind each texture.
constexpr uint32_t kComponentFlagHidden = 1u << 0;
constexpr uint32_t kComponentFlagNoGeometry = 1u << 1;

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

struct Byte4 {
    uint8_t x;
    uint8_t y;
    uint8_t z;
    uint8_t w;
};
static_assert(sizeof(Byte4) == 4);

struct BoneWeightPoc {
    float weight0;
    float weight1;
    float weight2;
    float weight3;
    int32_t bone_index0;
    int32_t bone_index1;
    int32_t bone_index2;
    int32_t bone_index3;
};
static_assert(sizeof(BoneWeightPoc) == 32);

#pragma pack(push, 1)
struct BemTextureEntryRaw {
    uint32_t width;
    uint32_t height;
    uint32_t mip_count;
    uint32_t data_size;
    // How to rebuild the replacement surface: the DDS payload's own format.
    int32_t create_format;
    int32_t create_srgb;
    // How to find the slot it belongs to: the format of the original texture
    // EFMI's content hash identifies, which is what the live material still
    // holds. These two differ whenever the mod ships a re-encoded replacement.
    int32_t slot_format;
    int32_t slot_srgb;
    // Which components' draws sampled the original texture. This is the actual
    // slot key: the same set can be computed from the live materials, so a
    // texture EFMI saw in exactly components {1,4} is the one texture both the
    // face and the eyebrow material bind.
    uint32_t component_mask;
    // Non-zero pins this texture to one slot, for the cases the component set
    // cannot separate. `reserved` says how to read it: 0 or 1 means the value
    // is the material property id, 2 means it is the CRC-32 of the original
    // texture's own object name. Both are set by the converter from the
    // payload's bem-slots.json.
    int32_t explicit_slot;
    uint32_t name_length;
    uint32_t reserved;
};
#pragma pack(pop)
static_assert(sizeof(BemTextureEntryRaw) == 48);

struct BemTexture {
    BemTextureEntryRaw info{};
    std::string name;
    std::vector<uint8_t> data;  // complete block-compressed mip chain
};

// PoC-2.4 carries each component's three EFMI vertex streams and index buffer
// verbatim. Nothing is decoded, renormalized or repacked: the runtime declares
// the live source mesh's own layout and writes these bytes into it.
struct BemComponent {
    BemComponentHeaderRaw info{};
    std::array<std::vector<uint8_t>, 3> streams;
    std::vector<uint8_t> indices;
};

struct BemPocData {
    BemFileHeader header{};
    std::vector<BemComponent> components;
    std::vector<BemTexture> textures;
};

constexpr uint32_t kBemStreamCount = 3;
constexpr int32_t kIndexElementSize = 2;

struct MethodContract {
    const char* key;
    BE_MethodDescriptorV1 descriptor;
    bool required;
    void* pointer = nullptr;
    const void* method_info = nullptr;
    bool resolved = false;
};

struct FieldContract {
    const char* key;
    BE_FieldDescriptorV1 descriptor;
    int32_t expected_offset;
};

const BE_HostApiV1* g_host = nullptr;
BE_ResolvedClassV1 g_skinned_renderer_class{};
BE_ResolvedClassV1 g_mesh_class{};
BE_ResolvedClassV1 g_vector3_class{};
BE_ResolvedClassV1 g_vector2_class{};
BE_ResolvedClassV1 g_bone_weight_class{};
BE_ResolvedClassV1 g_int32_class{};
BE_ResolvedClassV1 g_texture2d_class{};
BE_ResolvedClassV1 g_material_class{};

std::atomic_bool g_toggle_requested{false};
// F11 rolls the replacement back. F12 always ends in the replaced
// state: it re-derives that state from the payload instead of deciding by
// whether a replacement is already up, because the game can reconfigure the
// model under the mod (LOD swap, re-instantiation, screen transition) and the
// old toggle then needed several presses to land on a good state.
std::atomic_bool g_rollback_requested{false};
std::atomic_bool g_mask_requested{false};
std::atomic_bool g_buffer_probe_requested{false};
std::atomic_bool g_abi_probe_requested{false};
std::atomic_bool g_texture_probe_requested{false};
std::atomic_bool g_model_probe_requested{false};

// Raw pointers to engine bindings that IL2CPP metadata stripping hid. All of
// these are declared HasExplicitThis, so the instance is the first native
// argument. Signatures are taken from Unity 2021 LTS Mesh.bindings.cs and are
// treated as unverified until the F7 probe confirms them.
using MeshQueryByAttributeFn = int32_t(*)(void* mesh, int32_t argument);
using MeshQueryFn = int32_t(*)(void* mesh);
using MeshSetNativeChannelFn = void(*)(
    void* mesh, int32_t channel, int32_t format, int32_t dimension,
    void* values, int32_t array_size, int32_t values_start,
    int32_t values_count, int32_t flags);

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
    MeshQueryByAttributeFn get_attribute_dimension = nullptr;
    MeshQueryByAttributeFn get_attribute_format = nullptr;
    MeshQueryByAttributeFn get_attribute_stream = nullptr;
    MeshQueryByAttributeFn get_vertex_buffer_stride = nullptr;
    MeshQueryFn get_vertex_buffer_count = nullptr;
    MeshSetNativeChannelFn set_native_array_for_channel = nullptr;
    MeshSetVertexBufferParamsFn set_vertex_buffer_params = nullptr;
    MeshSetVertexBufferDataFn set_vertex_buffer_data = nullptr;
    MeshSetIndexBufferParamsFn set_index_buffer_params = nullptr;
    MeshSetIndexBufferDataFn set_index_buffer_data = nullptr;
    MeshSetSubMeshFn set_sub_mesh = nullptr;
    MeshUploadDataFn upload_mesh_data = nullptr;
    bool resolved = false;
};
EngineBindings g_engine{};

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
// MeshUpdateFlags.Default keeps Unity's own validation and bounds bookkeeping.
constexpr int32_t kMeshUpdateDefault = 0;
std::atomic_bool g_operation_in_progress{false};
std::atomic_bool g_hotkey_thread_stop{false};
std::atomic_uint64_t g_pump_hits{0};
HANDLE g_hotkey_thread = nullptr;

// One record per renderer whose mesh we replaced. There are several instances
// of the model live at once and ten components in each, so this replaces the
// single-target bookkeeping the Component9-only PoC used.
struct ReplacedComponent {
    uint32_t component_id = 0;
    // The live renderer's original index count, which is the key every
    // component is joined on. Kept so a later instance can be matched even
    // when it was built from a different Mesh object.
    uint32_t original_index_count = 0;
    void* renderer = nullptr;
    void* original_mesh = nullptr;
    void* custom_mesh = nullptr;
    uint32_t renderer_handle = 0;
    uint32_t original_mesh_handle = 0;
    uint32_t custom_mesh_handle = 0;
    bool original_enabled = true;
    bool hidden_by_mod = false;
};
std::vector<ReplacedComponent> g_replaced;
BemPocData g_active_bem;
bool g_replacement_active = false;
bool g_refresh_suspended = false;
int32_t g_pending_apply_frame = -1;
int32_t g_earliest_apply_frame = -1;
uint64_t g_next_refresh_tick = 0;
constexpr int32_t kDontUnloadUnusedAsset = 32;
struct RetainedAsset {
    void* object = nullptr;
    uint32_t handle = 0;
    bool added_unload_protection = false;
};
std::vector<RetainedAsset> g_retained_assets;
// Mirrors the mod's own mask toggle. The sample's default is mask-off.
bool g_mask_visible = false;

// One record per material slot we overwrote, so a rollback can put the exact
// original Texture object back rather than clearing the slot.
struct InstalledTextureSlot {
    void* material = nullptr;
    int32_t slot_id = 0;
    void* original = nullptr;
    void* replacement = nullptr;
    uint32_t material_handle = 0;
    uint32_t original_handle = 0;
};
std::vector<InstalledTextureSlot> g_installed_slots;

struct CreatedTexture {
    void* texture = nullptr;
    uint32_t handle = 0;
};
std::vector<CreatedTexture> g_created_textures;

using VoidStaticFn = void(__fastcall*)(void* method_info);
VoidStaticFn g_original_pump = nullptr;
int32_t g_last_pump_frame = -1;
int32_t g_model_probe_captures_remaining = 0;
int32_t g_model_probe_next_frame = -1;
int32_t g_model_probe_sequence = 0;

MethodContract g_methods[]{
    // Restore the callback that covers both gameplay and the detail screen.
    // Moving this to GameApp.Update regressed shadow geometry in game testing.
    {"pump.canvas_will_render",
        {"UnityEngine.UIModule.dll", "UnityEngine", "Canvas",
            "SendWillRenderCanvases", nullptr, "System.Void", 0}, true},
    {"skinned.get_force_matrix",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer",
            "get_forceMatrixRecalculationPerRender", nullptr, "System.Boolean", 0}, false},

    {"resources.find_objects_of_type_all",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Resources",
            "FindObjectsOfTypeAll", "System.Type", "UnityEngine.Object[]", 1}, true},
    {"object.get_name",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Object", "get_name",
            nullptr, "System.String", 0}, true},
    {"object.set_name",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Object", "set_name",
            "System.String", "System.Void", 1}, false},
    {"object.destroy",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Object", "Destroy",
            "UnityEngine.Object", "System.Void", 1}, false},
    {"object.is_alive",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Object", "op_Implicit",
            "UnityEngine.Object", "System.Boolean", 1}, true},
    {"object.get_hide_flags",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Object", "get_hideFlags",
            nullptr, "UnityEngine.HideFlags", 0}, true},
    {"object.set_hide_flags",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Object", "set_hideFlags",
            "UnityEngine.HideFlags", "System.Void", 1}, true},
    {"time.get_frame_count",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Time", "get_frameCount",
            nullptr, "System.Int32", 0}, true},

    {"array.create_instance",
        {"mscorlib.dll", "System", "Array", "CreateInstance",
            "System.Type|System.Int32", "System.Array", 2}, true},
    {"array.get_length",
        {"mscorlib.dll", "System", "Array", "GetLength",
            "System.Int32", "System.Int32", 1}, true},
    {"array.get_value",
        {"mscorlib.dll", "System", "Array", "GetValue",
            "System.Int32", "System.Object", 1}, true},

    {"component.get_transform",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Component", "get_transform",
            nullptr, "UnityEngine.Transform", 0}, true},
    {"component.get_game_object",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Component", "get_gameObject",
            nullptr, "UnityEngine.GameObject", 0}, true},
    {"game_object.get_active_in_hierarchy",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "GameObject", "get_activeInHierarchy",
            nullptr, "System.Boolean", 0}, true},
    {"game_object.get_scene",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "GameObject", "get_scene",
            nullptr, "UnityEngine.SceneManagement.Scene", 0}, true},
    {"transform.get_parent",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "get_parent",
            nullptr, "UnityEngine.Transform", 0}, true},

    {"skinned.get_shared_mesh",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer",
            "get_sharedMesh", nullptr, "UnityEngine.Mesh", 0}, true},
    {"skinned.set_shared_mesh",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer",
            "set_sharedMesh", "UnityEngine.Mesh", "System.Void", 1}, true},
    {"skinned.get_bones",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer",
            "get_bones", nullptr, "UnityEngine.Transform[]", 0}, true},
    {"skinned.get_root_bone",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer",
            "get_rootBone", nullptr, "UnityEngine.Transform", 0}, false},
    {"skinned.get_skinning_root",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer",
            "get_skinningRoot", nullptr, "UnityEngine.Transform", 0}, false},
    {"transform.get_local_to_world",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Transform",
            "get_localToWorldMatrix", nullptr, "UnityEngine.Matrix4x4", 0}, false},
    {"renderer.get_local_to_world",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Renderer",
            "get_localToWorldMatrix", nullptr, "UnityEngine.Matrix4x4", 0}, false},
    {"skinned.bake_mesh_no_scale",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer",
            "BakeMesh", "UnityEngine.Mesh|System.Boolean", "System.Void", 2}, false},

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
    {"mesh.set_bone_weights_native",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "InternalSetBoneWeights",
            "System.IntPtr|System.Int32|System.IntPtr|System.Int32", "System.Void", 4}, true},
    {"mesh.has_bone_weights",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "HasBoneWeights",
            nullptr, "System.Boolean", 0}, true},
    {"mesh.get_skin_weight_count",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "GetAllBoneWeightsArraySize",
            nullptr, "System.Int32", 0}, true},
    {"mesh.get_bones_per_vertex_value",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "GetBonesPerVertexValue",
            nullptr, "System.Int32", 0}, false},
    // Bounds is mesh metadata rather than vertex data, so unlike vertices it
    // stays readable on the isReadable=false meshes this client ships. It is
    // the only way to compare the original geometry's extent against ours.
    {"mesh.get_bounds",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "get_bounds",
            nullptr, "UnityEngine.Bounds", 0}, false},

    // Metadata-only Mesh declaration queries. These never expose native graphics
    // resources and are used to prove the EFMI semantic remap against the live
    // source Mesh before any custom data is written.
    //
    // The validated client strips the per-attribute convenience getters
    // (GetVertexAttributeDimension/Format/Stream/Offset), so the declaration is
    // read through the enumeration API instead. GetVertexAttribute returns a
    // VertexAttributeDescriptor by value; runtime_invoke boxes it and the
    // existing Unbox path copies the four Int32 fields out directly.
    {"mesh.get_vertex_attribute_count",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh",
            "get_vertexAttributeCount", nullptr, "System.Int32", 0}, false},
    {"mesh.get_vertex_attribute",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "GetVertexAttribute",
            "System.Int32",
            "UnityEngine.Rendering.VertexAttributeDescriptor", 1}, false},

    // This is the non-generic CPU->Mesh channel writer used by Unity's
    // NativeArray channel setters. Unlike PoC-3, it does not retrieve or mutate
    // a GraphicsBuffer object. If stripped from the client we simply refuse the
    // raw-channel PoC instead of falling back to any native graphics path.
    {"mesh.set_native_array_for_channel",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "SetNativeArrayForChannelImpl",
            "UnityEngine.Rendering.VertexAttribute|UnityEngine.Rendering.VertexAttributeFormat|System.Int32|System.IntPtr|System.Int32|System.Int32|System.Int32|UnityEngine.Rendering.MeshUpdateFlags",
            "System.Void", 8}, false},

    // BakeMesh writes the renderer's current skinned result, topology included,
    // into a Mesh we own. That is the only way to read the original C9
    // geometry: every candidate mesh in this client is isReadable=false.
    {"skinned.bake_mesh",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer",
            "BakeMesh", "UnityEngine.Mesh", "System.Void", 1}, false},
    {"mesh.get_vertices",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "get_vertices",
            nullptr, "UnityEngine.Vector3[]", 0}, false},
    {"mesh.get_triangles",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "GetTriangles",
            "System.Int32", "System.Int32[]", 1}, false},

    // GraphicsBuffer.Target is a nested enum whose il2cpp type name spelling is
    // not certain here, so these leave the return type unconstrained and match
    // on name plus parameter count alone.
    {"mesh.get_vertex_buffer_target",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh",
            "get_vertexBufferTarget", nullptr, nullptr, 0}, false},
    {"skinned.get_vertex_buffer_target",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer",
            "get_vertexBufferTarget", nullptr, nullptr, 0}, false},

    // The HG-modified SkinnedMeshRenderer exposes the buffers the draw call
    // actually consumes. PoC-3 crashed inside the Mesh-level equivalent, so
    // these stay behind their own hotkey and are only ever read.
    {"skinned.get_vertex_buffer",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer",
            "GetVertexBuffer", nullptr, "UnityEngine.GraphicsBuffer", 0}, false},
    {"skinned.get_previous_vertex_buffer",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer",
            "GetPreviousVertexBuffer", nullptr,
            "UnityEngine.GraphicsBuffer", 0}, false},
    {"graphics_buffer.get_count",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "GraphicsBuffer",
            "get_count", nullptr, "System.Int32", 0}, false},
    {"graphics_buffer.get_stride",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "GraphicsBuffer",
            "get_stride", nullptr, "System.Int32", 0}, false},
    {"graphics_buffer.get_target",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "GraphicsBuffer",
            "get_target", nullptr, nullptr, 0}, false},

    // Material/texture slot inspection. EFMI replaces textures by content hash
    // at the graphics API layer and therefore records no slot information, so
    // the mapping from its DDS set to this material's properties has to be
    // rebuilt here. The join key is (width, height, format), which is exactly
    // what these read back.
    {"renderer.get_shared_materials",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Renderer",
            "get_sharedMaterials", nullptr, "UnityEngine.Material[]", 0}, false},
    // Hiding a component means disabling its renderer, which is how the mod's
    // skipped draw calls are reproduced without a graphics hook.
    {"renderer.get_enabled",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Renderer",
            "get_enabled", nullptr, "System.Boolean", 0}, false},
    {"renderer.set_enabled",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Renderer",
            "set_enabled", "System.Boolean", "System.Void", 1}, false},
    {"renderer.get_force_rendering_off",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Renderer",
            "get_forceRenderingOff", nullptr, "System.Boolean", 0}, false},
    {"renderer.get_local_bounds",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Renderer",
            "get_localBounds", nullptr, "UnityEngine.Bounds", 0}, false},
    {"renderer.get_shadow_casting_mode",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Renderer",
            "get_shadowCastingMode", nullptr, nullptr, 0}, false},
    {"renderer.get_shadow_proxy_mesh",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Renderer",
            "get_shadowProxyMesh", nullptr, "UnityEngine.Mesh", 0}, false},
    {"material.get_texture_property_ids",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Material",
            "GetTexturePropertyNameIDs", nullptr, "System.Int32[]", 0}, false},
    {"material.get_texture_by_id",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Material", "GetTexture",
            "System.Int32", "UnityEngine.Texture", 1}, false},
    {"texture.get_width",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Texture", "get_width",
            nullptr, "System.Int32", 0}, false},
    {"texture.get_height",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Texture", "get_height",
            nullptr, "System.Int32", 0}, false},
    // Return types left unconstrained: GraphicsFormat and TextureFormat are
    // enums whose il2cpp type name spelling is not certain here.
    {"texture.get_graphics_format",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Texture",
            "get_graphicsFormat", nullptr, nullptr, 0}, false},
    {"texture2d.get_format",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Texture2D",
            "get_format", nullptr, nullptr, 0}, false},

    // Texture installation. The replacement surfaces arrive as complete
    // block-compressed mip chains, so they are uploaded through the managed
    // Texture2D path rather than any raw graphics binding: this ctor performs
    // Unity's own format validation, and the created texture's graphicsFormat
    // is read back afterwards to confirm what the engine actually built.
    {"texture2d.ctor",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Texture2D", ".ctor",
            "System.Int32|System.Int32|UnityEngine.TextureFormat|"
            "System.Int32|System.Boolean",
            "System.Void", 5}, false},
    {"texture2d.load_raw_texture_data",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Texture2D",
            "LoadRawTextureData", "System.IntPtr|System.Int32",
            "System.Void", 2}, false},
    {"texture2d.apply",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Texture2D", "Apply",
            "System.Boolean|System.Boolean", "System.Void", 2}, false},
    {"material.set_texture_by_id",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Material", "SetTexture",
            "System.Int32|UnityEngine.Texture", "System.Void", 2}, false},

    // Turns the payload's (TextureFormat, sRGB) slot key into the same
    // GraphicsFormat value the live material reports, using the engine's own
    // table. This is what makes the join possible without hard-coding any
    // GraphicsFormat enum values, whose absolute numbering this client's
    // stripped metadata does not reveal.
    {"graphics_format_utility.get_graphics_format",
        {"UnityEngine.CoreModule.dll", "UnityEngine.Experimental.Rendering",
            "GraphicsFormatUtility", "GetGraphicsFormat",
            "UnityEngine.TextureFormat|System.Boolean", nullptr, 2}, false},

    // Sampler state is copied from the texture being replaced so the new
    // surface is filtered exactly like the one the material expected.
    // Enum-typed, so the parameter types are matched on arity alone.
    {"texture.get_wrap_mode",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Texture",
            "get_wrapMode", nullptr, nullptr, 0}, false},
    {"texture.set_wrap_mode",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Texture",
            "set_wrapMode", nullptr, "System.Void", 1}, false},
    {"texture.get_filter_mode",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Texture",
            "get_filterMode", nullptr, nullptr, 0}, false},
    {"texture.set_filter_mode",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Texture",
            "set_filterMode", nullptr, "System.Void", 1}, false},
    {"texture.get_aniso_level",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Texture",
            "get_anisoLevel", nullptr, "System.Int32", 0}, false},
    {"texture.set_aniso_level",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Texture",
            "set_anisoLevel", "System.Int32", "System.Void", 1}, false},
    {"texture.get_mip_map_bias",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Texture",
            "get_mipMapBias", nullptr, "System.Single", 0}, false},
    {"texture.set_mip_map_bias",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Texture",
            "set_mipMapBias", "System.Single", "System.Void", 1}, false},

    {"mesh.set_vertices",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "set_vertices",
            "UnityEngine.Vector3[]", "System.Void", 1}, true},
    {"mesh.set_uv",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "set_uv",
            "UnityEngine.Vector2[]", "System.Void", 1}, true},
    {"mesh.set_bone_weights",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "set_boneWeights",
            "UnityEngine.BoneWeight[]", "System.Void", 1}, true},
    {"mesh.set_bindposes",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "set_bindposes",
            "UnityEngine.Matrix4x4[]", "System.Void", 1}, true},
    {"mesh.set_sub_mesh_count",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "set_subMeshCount",
            "System.Int32", "System.Void", 1}, true},
    {"mesh.set_triangles",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "SetTriangles",
            "System.Int32[]|System.Int32|System.Boolean|System.Int32",
            "System.Void", 4}, true},
    {"mesh.recalculate_bounds",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "RecalculateBounds",
            nullptr, "System.Void", 0}, true},
};

constexpr std::array<FieldContract, 8> kBoneWeightFields{{
    {"weight0", {"UnityEngine.CoreModule.dll", "UnityEngine", "BoneWeight",
        "m_Weight0", "System.Single"}, kBoxedValueHeaderSize + 0},
    {"weight1", {"UnityEngine.CoreModule.dll", "UnityEngine", "BoneWeight",
        "m_Weight1", "System.Single"}, kBoxedValueHeaderSize + 4},
    {"weight2", {"UnityEngine.CoreModule.dll", "UnityEngine", "BoneWeight",
        "m_Weight2", "System.Single"}, kBoxedValueHeaderSize + 8},
    {"weight3", {"UnityEngine.CoreModule.dll", "UnityEngine", "BoneWeight",
        "m_Weight3", "System.Single"}, kBoxedValueHeaderSize + 12},
    {"index0", {"UnityEngine.CoreModule.dll", "UnityEngine", "BoneWeight",
        "m_BoneIndex0", "System.Int32"}, kBoxedValueHeaderSize + 16},
    {"index1", {"UnityEngine.CoreModule.dll", "UnityEngine", "BoneWeight",
        "m_BoneIndex1", "System.Int32"}, kBoxedValueHeaderSize + 20},
    {"index2", {"UnityEngine.CoreModule.dll", "UnityEngine", "BoneWeight",
        "m_BoneIndex2", "System.Int32"}, kBoxedValueHeaderSize + 24},
    {"index3", {"UnityEngine.CoreModule.dll", "UnityEngine", "BoneWeight",
        "m_BoneIndex3", "System.Int32"}, kBoxedValueHeaderSize + 28},
}};

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
    return exception ? nullptr : result;
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
