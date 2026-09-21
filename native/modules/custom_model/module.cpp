// Resource-delivery implementation. See CUSTOM_MODEL_RESOURCE_RELEASE_CALL_CHAIN_20260916.md.
// Mesh, material and texture algorithms migrated from the 84b88bfb PoC;
// all object ownership below is restricted to a single delivery transaction.
#include "BetterEndfield/ModuleApi.h"
#include "bem.h"
#include "native_mesh_layout.h"
#include "mod_registry.h"
#include "resource_policy.h"
#if defined(__ANDROID__)
#include "modules/custom_model/android_mesh_builder.h"
#endif
#if defined(_WIN32)
#include <Windows.h>
#else
#include "platform_compat.h"
#endif
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
namespace BetterEndfield::CustomModel {
namespace {
constexpr char kModuleId[]="betterendfield.custom_model";
const BE_HostApiV1* g_host=nullptr;
BE_ResolvedClassV1 g_skinned_renderer_class{},g_mesh_class{},g_texture2d_class{},g_material_class{};
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
    {"renderer.get_enabled",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Renderer",
            "get_enabled", nullptr, "System.Boolean", 0}, true},
    {"renderer.set_enabled",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Renderer",
            "set_enabled", "System.Boolean", "System.Void", 1}, true},
#if defined(__ANDROID__)
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
    {"skinned.set_shared_mesh",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer",
            "set_sharedMesh", "UnityEngine.Mesh", "System.Void", 1}, true},
    {"skinned.get_bones",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer",
            "get_bones", nullptr, "UnityEngine.Transform[]", 0}, true},
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
    {"probe.material_shader", {"UnityEngine.CoreModule.dll","UnityEngine","Material","get_shader",nullptr,"UnityEngine.Shader",0},false},
    {"material.copy", {"UnityEngine.CoreModule.dll","UnityEngine","Material",".ctor","UnityEngine.Material","System.Void",1},true},
    {"object.instance_id", {"UnityEngine.CoreModule.dll","UnityEngine","Object","GetInstanceID",nullptr,"System.Int32",0},true},
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
    std::vector<void*> assets;
    ConstructionScope* previous = g_construction;
    bool failed = false;
    bool published = false;
    ConstructionScope() { g_construction = this; }
    ConstructionScope(const ConstructionScope&) = delete;
    ~ConstructionScope();
    void* Root(void* object) {
        if (!object) return nullptr;
        for (const auto& root : roots) if (root.first == object) return object;
        roots.emplace_back(object,0);
        roots.back().second = g_host->gchandle_new(g_host->context,object,0);
        if (!roots.back().second) { failed = true; return nullptr; }
        return object;
    }
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
bool SetSharedMesh(void* renderer, void* mesh) {
    void* args[]{mesh};
    return InvokeVoid(Contract("skinned.set_shared_mesh"),renderer,args) &&
        Invoke(Contract("skinned.get_shared_mesh"),renderer,nullptr)==mesh;
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


bool ResolveEngineBindings() {
    if (g_engine.resolved) return true;

#if defined(__ANDROID__)
    if (!betterendfield::AndroidMeshBuilderReady()) return false;
    BE_FieldDescriptorV1 descriptor{"UnityEngine.CoreModule.dll", "UnityEngine", "Object", "m_CachedPtr", "System.IntPtr"};
    BE_ResolvedFieldV1 field{};
    std::string error;
    if (!g_host || g_host->resolve_field(g_host->context,&descriptor,&field)!=BE_Result_Ok ||
        field.offset < 0 || !ResolveNativeMeshLayout({},g_native_layout,error)) return false;
    g_engine.cached_ptr_offset = field.offset;
    HMODULE game = GetModuleHandleW(L"GameAssembly.dll");
    auto resolve = reinterpret_cast<ResolveIcallFn>(GetProcAddress(game,"il2cpp_resolve_icall"));
    g_engine.upload_mesh_data = reinterpret_cast<MeshUploadDataFn>(resolve ? resolve("UnityEngine.Mesh::UploadMeshDataImpl") : nullptr);
    g_engine.resolved = g_engine.upload_mesh_data != nullptr;
    return g_engine.resolved;
#else

    using ResolveIcallFn = void* (*)(const char*);
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
    return betterendfield::AndroidReadMeshStrides(mesh,strides);
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
    if ((info.stride2 != 4 && info.stride2 != 12) ||
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
            uint16_t packed = 65535;
            if (info.stride2 == 12) std::memcpy(&packed, data + k * 2, sizeof(packed));
            if (!packed) continue;
            const float weight = static_cast<float>(packed) / 65535.0f;
            influences[count++] = {weight, indices[k]};
            sum += weight;
        }
        if (!count || std::abs(sum - 1.0f) > 0.01f) {
            Log("C" + std::to_string(info.component_id) + " invalid skin weights at vertex " +
                std::to_string(v) + " sum=" + std::to_string(sum));
            return false;
        }
        std::sort(influences.begin(), influences.begin() + count,
            [](const auto& a, const auto& b) { return a.weight > b.weight; });
        for (int k = 0; k < count; ++k) {
            influences[k].weight /= sum;
            weights.push_back(influences[k]);
        }
        counts.push_back(count);
    }
    return true;
}


bool ValidateRendererSkin(const BemComponent& component, void* renderer, void* source_mesh) {
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
    if (!ReadVertexDeclaration(source_mesh, declaration)) {
        Log(label + " source declaration is unreadable; refusing.");
        return false;
    }
    if (component.attributes.size()!=static_cast<size_t>(declaration.count) ||
        std::memcmp(component.attributes.data(),declaration.entries.data(),component.attributes.size()*16)!=0 ||
        (component.layout_crc && Crc32(std::string_view(
        reinterpret_cast<const char*>(declaration.entries.data()),declaration.count*sizeof(VertexAttributeDescriptorRaw)))!=component.layout_crc)) {
        Log(label+" native vertex declaration differs from verified profile."); return false;
    }
    std::vector<int32_t> source_strides;
    if (!ReadMeshStrides(source_mesh, source_strides)) {
        Log(label + " source strides are unreadable; refusing.");
        return false;
    }

    const std::vector<int32_t> payload_strides{
        static_cast<int32_t>(info.stride0),
        static_cast<int32_t>(info.stride1),
        static_cast<int32_t>(info.stride2)};
    if (source_strides != payload_strides) {
        Log(label + " payload strides " + StrideText(payload_strides) +
            " do not match the live mesh's " + StrideText(source_strides) +
            "; refusing so the bytes cannot land on the wrong channels.");
        LogVertexDeclaration(declaration);
        return false;
    }

    void* bindposes = prepared_bindposes ? prepared_bindposes : Invoke(
        Contract("mesh.get_bindposes"), source_mesh, nullptr);
    const int bindpose_count = ArrayLength(bindposes);
    if (!bindposes || bindpose_count <= static_cast<int>(info.max_bone)) {
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
    if (source_native && new_native) {
        const bool read_source_ok = TryReadNativeUInt32(source_native + g_native_layout.bones_per_vertex_offset, source_native_influences);
        if (read_source_ok &&
            (source_native_influences == 1 || source_native_influences == 2 || source_native_influences == 4)) {
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

    if (!component.bones.empty()) {
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
    if (!betterendfield::AndroidSubmitMesh(mesh,component)) {
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

    // The source declaration already exposes the packed BlendWeight and
    // BlendIndices fields in stream 2. Keep that one authoritative storage:
    // InternalSetBoneWeights creates a second Unity skinning representation,
    // then the former second SetVertexBufferParams call replaced its layout
    // again. HG can retain either representation across a renderer refresh,
    // which leaves the draw in bind pose when they disagree. Upload the exact
    // source layout and its raw skin stream once instead.

    for (int32_t stream = 0; stream < static_cast<int32_t>(kBemStreamCount);
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
    void* p_bindposes[1]{bindposes};
    if (!InvokeVoid(Contract("mesh.set_bindposes"), mesh, p_bindposes)) {
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
    if (!poses_equal) { Log(label+" bindpose readback differs."); DestroyUnityObject(mesh); return false; }
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
    if (built_vertex_count != vertex_count ||
        built_index_count != static_cast<uint32_t>(index_count)) {
        Log(label + " built geometry counts disagree with the payload: vtx=" +
            std::to_string(built_vertex_count) + " idx=" +
            std::to_string(built_index_count));
        DestroyUnityObject(mesh);
        return false;
    }

    // Verify before assigning. The writer signatures are inferred from Unity's
    // published bindings, so a wrong one shows up here as a refusal rather
    // than as a corrupted renderer.
    if (!MatchesDeclaration(mesh, declaration, source_strides, label +
            " built")) {
        Log(label + " does not reproduce the source declaration; refusing.");
        DestroyUnityObject(mesh);
        return false;
    }

    // Submit the completed mesh before any renderer can consume it. The raw
    // setters populate CPU mesh storage; otherwise upload is left until a
    // later render after the resource has already been delivered.
    uint8_t has_skin = 0;
    const bool skin_ready = InvokeValue(Contract("mesh.has_bone_weights"), mesh, nullptr, has_skin) && has_skin;
    if (!skin_ready) {
        Log(label + " packed stream did not activate skin metadata; original mesh retained.");
        DestroyUnityObject(mesh);
        return false;
    }
    if (!SafeUploadMeshData(mesh)) {
        Log(label + " UploadMeshData(false) faulted; original mesh retained.");
        DestroyUnityObject(mesh);
        return false;
    }

    uint32_t final_influences = 0;
    if (!TryReadNativeUInt32(new_native + g_native_layout.bones_per_vertex_offset,final_influences) ||
        final_influences != source_native_influences) {
        Log(label + " native skin field changed after upload; refusing.");
        DestroyUnityObject(mesh); return false;
    }

    new_mesh = mesh;
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
    float bias=0,read=0;
    if (!InvokeValue(Contract("texture.get_mip_map_bias"),source,nullptr,bias) || !std::isfinite(bias)) return false;
    void* args[]{&bias};
    return InvokeVoid(Contract("texture.set_mip_map_bias"),destination,args) &&
        InvokeValue(Contract("texture.get_mip_map_bias"),destination,nullptr,read) && read==bias;
}

void* CreateTextureFromBem(const BemTexture& texture) {
    const BemTextureEntryRaw& info = texture.info;
#ifdef __ANDROID__
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

    void* data_pointer = const_cast<uint8_t*>(texture.data.data());
    int32_t data_size = static_cast<int32_t>(texture.data.size());
    void* upload_parameters[2]{&data_pointer, &data_size};
    if (!InvokeVoid(Contract("texture2d.load_raw_texture_data"), object,
            upload_parameters)) {
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
        const std::string name = "BetterEndfield.T." + texture.name;
        void* managed = NewString(name.c_str());
        void* parameters[1]{managed};
        InvokeVoid(set_name, object, parameters);
    }

    Log("  built t=" + texture.name + " " + std::to_string(built_width) + "x" +
        std::to_string(built_height) + " mips=" +
        std::to_string(info.mip_count) + " " +
        TextureFormatName(info.create_format) +
        (info.create_srgb != 0 ? " sRGB" : " linear") + " graphicsFormat=" +
        std::to_string(built_graphics_format));
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
    // No long-lived GC roots or DontUnloadUnusedAsset flags after delivery.
    g_construction = nullptr; // Cleanup must not allocate more temporary roots.
    if (!published) {
        for (size_t i=assets.size();i>0;--i) {
            try { if (assets[i-1]) DestroyUnityObject(assets[i-1]); }
            catch (...) { /* Preserve shutdown/exception unwinding. */ }
        }
    }
    for (const auto& root : roots) if (root.second) g_host->gchandle_free(g_host->context,root.second);
    g_construction = previous;
}
struct PreparedBinding {
    uint32_t component_id=0;
    void* renderer=nullptr;
    void* original_mesh=nullptr;
    void* custom_mesh=nullptr;
    void* original_materials=nullptr;
    void* custom_materials=nullptr;
    void* original_bones=nullptr;
    void* custom_bones=nullptr;
    std::vector<std::string> bone_names;
    bool original_enabled=true;
    bool custom_enabled=true;
#if defined(__ANDROID__)
    bool change_shadow=false;
    int32_t original_shadow=0,custom_shadow=0;
    void* original_shadow_mesh=nullptr;
#endif
};
bool CopyMaterials(PreparedBinding& binding) {
    binding.original_materials=Invoke(Contract("renderer.get_shared_materials"),binding.renderer,nullptr);
    const int count=ArrayLength(binding.original_materials);
    if (count<=0 || count>256) return false;
    binding.custom_materials=Invoke(Contract("array.clone"),binding.original_materials,nullptr);
    if (!binding.custom_materials || ArrayLength(binding.custom_materials)!=count) return false;
    for (int i=0;i<count;++i) {
        void* source=ArrayValue(binding.original_materials,i);
        void* copy=source?NewAsset(g_material_class.class_info):nullptr;
        void* ctor[]{source}; void* slot[]{copy,&i};
        if (!copy || !InvokeVoid(Contract("material.copy"),copy,ctor) ||
            !InvokeVoid(Contract("array.set_value"),binding.custom_materials,slot) ||
            ArrayValue(binding.custom_materials,i)!=copy) return false;
    }
    return true;
}
void* NewArrayLike(void* original, int count) {
    if (!original || count<=0 || count>256 || !g_object_class || !g_array_new_specific) return nullptr;
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
bool SameMeshSpace(void* a,void* b) {
    if (a==b) return true;
    Matrix4x4Raw ma{},mb{};
    void* ta=Invoke(Contract("component.get_transform"),a,nullptr);
    void* tb=Invoke(Contract("component.get_transform"),b,nullptr);
    if (!ta || !tb || !InvokeValue(Contract("transform.local_to_world"),ta,nullptr,ma) ||
        !InvokeValue(Contract("transform.local_to_world"),tb,nullptr,mb)) return false;
    for (int i=0;i<16;++i) if (!std::isfinite(ma.m[i]) || !std::isfinite(mb.m[i]) ||
        std::abs(ma.m[i]-mb.m[i])>0.0001f) return false;
    return true;
}
// Use the actual donor Transform/bindpose pair. Numeric ranges alone do not
// prove bone identity. Unequal mesh spaces need an explicit geometry-space
// conversion, which this bounded importer deliberately does not infer.
bool PreparePalette(const BemComponent& component,PreparedBinding& target,
    const std::vector<PreparedBinding>& bindings,void*& poses) {
    void* template_poses=Invoke(Contract("mesh.get_bindposes"),target.original_mesh,nullptr);
    target.custom_bones=NewArrayLike(target.original_bones,static_cast<int>(component.bones.size()));
    poses=NewArrayLike(template_poses,static_cast<int>(component.bones.size()));
    if (!target.custom_bones || !poses) return false;
    for (size_t i=0;i<component.bones.size();++i) {
        const auto& ref=component.bones[i]; const auto* donor=FindPrepared(bindings,ref.component);
        if (!donor || !SameMeshSpace(target.renderer,donor->renderer)) {
            Log("Merged palette donor missing or mesh spaces differ."); return false;
        }
        void* donor_poses=Invoke(Contract("mesh.get_bindposes"),donor->original_mesh,nullptr);
        if (ref.index>=static_cast<uint32_t>(ArrayLength(donor->original_bones)) ||
            ref.index>=static_cast<uint32_t>(ArrayLength(donor_poses))) return false;
        void* bone=ArrayValue(donor->original_bones,ref.index);
        void* pose=ArrayValue(donor_poses,ref.index); Matrix4x4Raw matrix{};
        if (!IsNativeObjectAlive(bone) || ObjectName(bone)!=component.bone_names[i] || !Unbox(pose,matrix)) return false;
        float magnitude=0;
        for (float f:matrix.m) { if (!std::isfinite(f)) return false; magnitude+=std::abs(f); }
        if (!magnitude || !SetArrayValue(target.custom_bones,static_cast<int>(i),bone) ||
            !SetArrayValue(poses,static_cast<int>(i),pose)) return false;
        target.bone_names.push_back(component.bone_names[i]);
    }
    std::vector<uint8_t> counts; std::vector<BoneWeight1Raw> weights;
    return DecodeComponentSkin(component,counts,weights);
}
bool PrepareDrawMaterials(const BemComponent& component,PreparedBinding& target,
    const std::vector<PreparedBinding>& bindings,const BemPocData& bem) {
    std::map<std::pair<size_t,void*>,void*> texture_cache;
    target.custom_materials=NewArrayLike(target.original_materials,static_cast<int>(component.draws.size()));
    if (!target.custom_materials) return false;
    for (size_t i=0;i<component.draws.size();++i) {
        const auto& draw=component.draws[i]; const auto* donor=FindPrepared(bindings,draw.material_component);
        if (!donor || draw.material_slot>=static_cast<uint32_t>(ArrayLength(donor->original_materials))) return false;
        void* material=ArrayValue(donor->original_materials,draw.material_slot);
        if (!material || ObjectName(material)!=component.material_names[i]) return false;
        void* copy=NewAsset(g_material_class.class_info); void* ctor[]{material};
        if (!copy || !InvokeVoid(Contract("material.copy"),copy,ctor) ||
            !SetArrayValue(target.custom_materials,static_cast<int>(i),copy)) return false;
#if defined(__ANDROID__)
        if (!betterendfield::AndroidAuditMaterialCopy(material,copy)) return false;
#endif
        const auto slots=ReadMaterialTextureSlots(copy);
        std::vector<int32_t> assigned;
        for (size_t t=0;t<bem.textures.size();++t) if (draw.textures&(uint32_t{1}<<t)) {
            const auto& tex=bem.textures[t]; const MaterialTextureSlot* match=nullptr;
            for (const auto& slot:slots) if (ObjectName(slot.texture)==tex.original_name) {
                if (match) { Log("Ambiguous v25 texture name pin."); return false; } match=&slot;
            }
            if (!match || std::find(assigned.begin(),assigned.end(),match->slot_id)!=assigned.end()) return false;
            assigned.push_back(match->slot_id);
            void*& texture=texture_cache[{t,match->texture}];
            if (!texture) {
                texture=CreateTextureFromBem(tex);
                if (!texture || !CopySamplerState(match->texture,texture)) return false;
#if defined(__ANDROID__)
                betterendfield::AndroidAuditTextureColorSpace(match->texture,texture,tex.original_name);
                betterendfield::AndroidAuditNormalTexture(match->texture,tex.original_name);
#endif
            }
            int32_t slot=match->slot_id; void* args[]{&slot,texture}; void* read[]{&slot};
            if (!InvokeVoid(Contract("material.set_texture_by_id"),copy,args) ||
                Invoke(Contract("material.get_texture_by_id"),copy,read)!=texture) return false;
        }
    }
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
bool ApplyPreparedBinding(PreparedBinding& binding) {
#if defined(__ANDROID__)
    if (binding.change_shadow) {
        void* mode[]{&binding.custom_shadow}; void* mesh[]{nullptr}; int32_t read=-1;
        if (!InvokeVoid(Contract("android.shadow_set"),binding.renderer,mode) ||
            !InvokeValue(Contract("android.shadow_get"),binding.renderer,nullptr,read) || read!=binding.custom_shadow ||
            !InvokeVoid(Contract("android.shadow_mesh_set"),binding.renderer,mesh) ||
            Invoke(Contract("android.shadow_mesh_get"),binding.renderer,nullptr)!=nullptr) return false;
    }
#endif
    if (binding.custom_bones && !SetRendererBones(binding.renderer,binding.custom_bones)) return false;
    if (binding.custom_mesh!=binding.original_mesh && !SetSharedMesh(binding.renderer,binding.custom_mesh)) return false;
    if (!ApplyRendererMaterials(binding.renderer,binding.custom_materials)) return false;
    bool read=false;
    return SetRendererEnabled(binding.renderer,binding.custom_enabled) &&
        GetRendererEnabled(binding.renderer,read) && read==binding.custom_enabled;
}
bool RestorePreparedBinding(PreparedBinding& binding) {
    // No short-circuit: independently restore every field even if another fails.
    const bool mesh=SetSharedMesh(binding.renderer,binding.original_mesh);
    const bool bones=!binding.custom_bones || SetRendererBones(binding.renderer,binding.original_bones);
    const bool materials=ApplyRendererMaterials(binding.renderer,binding.original_materials);
    bool read=false;
    const bool enabled=SetRendererEnabled(binding.renderer,binding.original_enabled) &&
        GetRendererEnabled(binding.renderer,read) && read==binding.original_enabled;
    bool shadow=true;
#if defined(__ANDROID__)
    if (binding.change_shadow) {
        void* mode[]{&binding.original_shadow}; void* proxy[]{binding.original_shadow_mesh}; int32_t read=-1;
        const bool restored_mode=InvokeVoid(Contract("android.shadow_set"),binding.renderer,mode) &&
            InvokeValue(Contract("android.shadow_get"),binding.renderer,nullptr,read) && read==binding.original_shadow;
        const bool restored_proxy=InvokeVoid(Contract("android.shadow_mesh_set"),binding.renderer,proxy) &&
            Invoke(Contract("android.shadow_mesh_get"),binding.renderer,nullptr)==binding.original_shadow_mesh;
        shadow=restored_mode && restored_proxy;
    }
#endif
    return mesh && bones && materials && enabled && shadow;
}
bool ValidatePayloadAdapter(const CharacterAdapter& adapter,const BemPocData& bem) {
    if (bem.components.size()!=adapter.components.size()) return false;
    for (const auto& component:bem.components) {
        if (component.info.component_id>=adapter.components.size() ||
            adapter.components[component.info.component_id].indices!=component.info.original_index_count) return false;
    }
    for (const auto& texture:bem.textures) if (texture.original_name.empty()) return false;
    return true;
}
bool PrepareResource(const CharacterAdapter& adapter,const BemPocData& bem,void* asset,
    std::vector<PreparedBinding>& bindings) {
    if (!ValidatePayloadAdapter(adapter,bem)) { Log("Package does not match character adapter."); return false; }
    bool inactive=true;
    void* args[]{g_skinned_renderer_class.type_object,&inactive};
    void* renderers=Invoke(Contract("game_object.renderers"),asset,args);
    const int count=ArrayLength(renderers);
    if (!renderers || count<=0 || count>4096) return false;
    for (const auto& component:bem.components) {
        const auto& identity=adapter.components[component.info.component_id];
        void* matched=nullptr;
        for (int i=0;i<count;++i) {
            void* renderer=ArrayValue(renderers,i);
            if (ObjectName(renderer)!=identity.name) continue;
            const auto path=BuildTransformPath(renderer);
            if (path.find("shadowProxy")!=path.npos || path.find("/lod1/")!=path.npos ||
                path.find("/lod2/")!=path.npos || path.find("/lod3/")!=path.npos ||
                path.find("SK_actor_female")!=path.npos) continue;
            if (matched) { Log("Ambiguous renderer identity: "+std::string(identity.name)); return false; }
            matched=renderer;
        }
        if (!matched) { Log("Required component missing: "+std::string(identity.name)); return false; }
        PreparedBinding binding;
        binding.component_id=component.info.component_id; binding.renderer=matched;
        binding.original_mesh=Invoke(Contract("skinned.get_shared_mesh"),matched,nullptr);
        binding.custom_mesh=binding.original_mesh;
        if (!binding.original_mesh || ObjectName(binding.original_mesh)!=identity.name ||
            !GetRendererEnabled(matched,binding.original_enabled)) return false;
        int32_t submeshes=0; uint64_t indices=0;
        if (!InvokeValue(Contract("mesh.get_sub_mesh_count"),binding.original_mesh,nullptr,submeshes) ||
            submeshes<=0 || submeshes>256) return false;
        for (int32_t sub=0;sub<submeshes;++sub) {
            uint32_t size=0; void* index[]{&sub};
            if (!InvokeValue(Contract("mesh.get_index_count"),binding.original_mesh,index,size)) return false;
            indices+=size;
        }
        if (indices!=identity.indices) { Log("Source mesh identity changed: "+std::string(identity.name)); return false; }
        const bool hidden=(component.info.flags&kComponentFlagHidden)!=0;
        binding.custom_enabled=hidden?false:binding.original_enabled;
        binding.original_materials=Invoke(Contract("renderer.get_shared_materials"),matched,nullptr);
        binding.original_bones=Invoke(Contract("skinned.get_bones"),matched,nullptr);
        bindings.push_back(binding);
    }
    for (size_t i=0;i<bem.components.size();++i) {
        const auto& component=bem.components[i]; auto& binding=bindings[i];
        if (!(component.info.flags&kComponentFlagNoGeometry)) {
            void* poses=nullptr;
            if (!PreparePalette(component,binding,bindings,poses) ||
                !BuildMeshFromComponent(component,binding.original_mesh,binding.custom_mesh,poses) ||
                !PrepareDrawMaterials(component,binding,bindings,bem)) return false;
        } else {
            if (!CopyMaterials(binding)) return false;
        }
    }
    return !g_construction->failed;
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
};
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
struct LodState {
    bool active=false;
    bool applied=false;
#if defined(__ANDROID__)
    bool npc_snapshot=false;
#endif
    int32_t original_max_lod=0;
    WeakManagedReference pipeline;
    std::array<LodField,5> fields{{
        {"s_visibleModelDistance","UnityEngine.Vector2",8},
        {"s_visibileAtmosphericModelDistance","UnityEngine.Vector2",8},
        {"s_maxNPCRenderNum","System.Int32",4},
        {"s_cameraCullRenderIdx","System.Int32",4},
        {"s_enableCameraPhysicCull","System.Boolean",1}
    }};
    bool Resolve() {
        const Float2 distance{1000,1000}; const int32_t number=100,cull=0; const bool physic=false;
        std::memcpy(fields[0].desired.data(),&distance,8); std::memcpy(fields[1].desired.data(),&distance,8);
        std::memcpy(fields[2].desired.data(),&number,4); std::memcpy(fields[3].desired.data(),&cull,4);
        std::memcpy(fields[4].desired.data(),&physic,1);
        for (auto& field:fields) {
            const BE_FieldDescriptorV1 descriptor{"Gameplay.Beyond.dll","Beyond.NPC.Lod","NPCCrowdLODSetting",field.name,field.type};
            if (g_host->resolve_field(g_host->context,&descriptor,&field.resolved)!=BE_Result_Ok || !field.resolved.field_info) return false;
        }
        return g_static_get && g_static_set;
    }
    bool Write(LodField& field,std::array<uint8_t,8>& bytes) {
        std::array<uint8_t,8> read{};
        return SafeStaticField(g_static_set,field.resolved.field_info,bytes.data()) &&
            SafeStaticField(g_static_get,field.resolved.field_info,read.data()) &&
            std::equal(bytes.begin(),bytes.begin()+field.size,read.begin());
    }
    bool Restore() {
        // DisableForceLOD0 must be able to restore the current game settings.
        g_lod_bias_locked.store(false,std::memory_order_release);
        if (!active) return true;
        applied=false;
        bool success=true;
#if defined(__ANDROID__)
        if (npc_snapshot) for (auto& field:fields) if (!Write(field,field.original)) success=false;
#endif
#if !defined(__ANDROID__)
        for (auto& field:fields) if (!Write(field,field.original)) success=false;
        void* args[]{&original_max_lod}; int32_t read=-1;
        if (!InvokeVoid(Contract("quality.set_max_lod"),nullptr,args) ||
            !InvokeValue(Contract("quality.get_max_lod"),nullptr,nullptr,read) || read!=original_max_lod) success=false;
#endif
        if (void* object=pipeline.Get()) {
            if (!InvokeVoid(Contract("pipeline.disable_force_lod0"),object,nullptr)) success=false;
            else pipeline.Reset();
        }
        if (success) {
            active=false; pipeline.Reset();
#if defined(__ANDROID__)
            npc_snapshot=false;
#endif
        }
        return success;
    }
    bool MaintainParameters() {
        int32_t read=-1;
        if (!InvokeValue(Contract("quality.get_max_lod"),nullptr,nullptr,read)) return false;
        if (read!=0) {
            int32_t zero=0; void* args[]{&zero};
            if (!InvokeVoid(Contract("quality.set_max_lod"),nullptr,args) ||
                !InvokeValue(Contract("quality.get_max_lod"),nullptr,nullptr,read) || read!=0) return false;
        }
        for (auto& field:fields) {
            std::array<uint8_t,8> value{};
            if (!SafeStaticField(g_static_get,field.resolved.field_info,value.data())) return false;
            if (!std::equal(field.desired.begin(),field.desired.begin()+field.size,value.begin())) {
                if (!Write(field,field.desired)) return false;
            }
        }
        return true;
    }
    bool Update(bool desired) {
        if (!desired) return Restore();
#if defined(__ANDROID__)
        void* current=Invoke(Contract("pipeline.current"),nullptr,nullptr);
        if (!current) return false;
        if (active && pipeline.Get()==current && applied) {
            if (npc_snapshot) for (auto& field:fields) {
                std::array<uint8_t,8> value{};
                if (!SafeStaticField(g_static_get,field.resolved.field_info,value.data())) return false;
                if (!std::equal(field.desired.begin(),field.desired.begin()+field.size,value.begin()) && !Write(field,field.desired)) return false;
            }
            return true;
        }
        if (active && !Restore()) return false;
        if (betterendfield::AndroidNpcParametersEnabled()) {
            for (auto& field:fields) if (!SafeStaticField(g_static_get,field.resolved.field_info,field.original.data())) return false;
            npc_snapshot=true;
        }
        if (!pipeline.Set(current)) return false;
        // Pipeline bias only. Mobile NPC and quality/culling configuration
        // must remain untouched, even while character replacements are active.
        active=true;
        if (!InvokeVoid(Contract("pipeline.enable_force_lod0"),current,nullptr)) { Restore(); return false; }
        if (npc_snapshot) for (auto& field:fields) if (!Write(field,field.desired)) { Restore(); return false; }
        applied=true;
        g_lod_bias_locked.store(true,std::memory_order_release);
        Log(npc_snapshot?"Android pipeline LOD bias + NPC parameters applied/readback PASS; QualitySettings unchanged":
            "Android pipeline LOD bias applied; quality/NPC/camera culling unchanged");
        return true;
#else
        void* current=Invoke(Contract("pipeline.current"),nullptr,nullptr);
        if (!current) return false;
        if (active && applied && pipeline.Get()==current) {
            if (MaintainParameters()) return true;
            Restore(); return false;
        }
        if (active && !Restore()) return false;
        if (!InvokeValue(Contract("quality.get_max_lod"),nullptr,nullptr,original_max_lod)) return false;
        for (auto& field:fields) if (!SafeStaticField(g_static_get,field.resolved.field_info,field.original.data())) return false;
        if (!pipeline.Set(current)) return false;
        if (!InvokeVoid(Contract("pipeline.enable_force_lod0"),current,nullptr)) { pipeline.Reset(); return false; }
        active=true; applied=false;
        int32_t zero=0,read=-1; void* args[]{&zero};
        bool success=InvokeVoid(Contract("quality.set_max_lod"),nullptr,args) &&
            InvokeValue(Contract("quality.get_max_lod"),nullptr,nullptr,read) && read==0;
        for (auto& field:fields) if (!Write(field,field.desired)) success=false;
        if (!success) Restore();
        else {
            applied=true;
            g_lod_bias_locked.store(true,std::memory_order_release);
        }
        return success;
#endif
    }
};
struct CompletedBinding {
    uint32_t component_id=0;
    std::string renderer_name;
    WeakObject mesh;
    std::vector<WeakObject> materials;
    bool enabled=false;
    bool generated_mesh=false;
#if defined(__ANDROID__)
    bool check_shadow=false;
    int32_t shadow=0;
#endif
    std::vector<std::string> bone_names;
};
struct CompletedResource {
    const CharacterAdapter* adapter=nullptr;
    WeakObject root;
    std::vector<CompletedBinding> bindings;
};
std::vector<CompletedResource> g_completed;
#if defined(__ANDROID__)
WeakObject g_android_test_world;
#endif
bool RememberResource(const CharacterAdapter& adapter,void* asset,
    const std::vector<PreparedBinding>& bindings,CompletedResource& record) {
    record.adapter=&adapter;
    if (!record.root.Set(asset)) return false;
    for (const auto& binding:bindings) {
        CompletedBinding completed;
        completed.renderer_name=ObjectName(binding.renderer);
        completed.component_id=binding.component_id; completed.enabled=binding.custom_enabled;
        completed.generated_mesh=binding.custom_mesh!=binding.original_mesh;
#if defined(__ANDROID__)
        completed.check_shadow=binding.change_shadow; completed.shadow=binding.custom_shadow;
#endif
        completed.bone_names=binding.bone_names;
        if (!completed.mesh.Set(binding.custom_mesh)) return false;
        for (int i=0;i<ArrayLength(binding.custom_materials);++i) {
            WeakObject material;
            if (!material.Set(ArrayValue(binding.custom_materials,i))) return false;
            completed.materials.push_back(std::move(material));
        }
        record.bindings.push_back(std::move(completed));
    }
    return true;
}
bool IsCompletedResource(const CharacterAdapter& adapter,void* asset) {
    for (const auto& record:g_completed)
        if (record.adapter==&adapter && record.root.Get()==asset) return true;
    // Natural clones may share the completed template's meshes/materials. The
    // full per-component identity must agree; names alone never prove completion.
    bool inactive=true; void* args[]{g_skinned_renderer_class.type_object,&inactive};
    void* renderers=Invoke(Contract("game_object.renderers"),asset,args);
    const int count=ArrayLength(renderers);
    if (!renderers || count<=0 || count>4096) return false;
    for (const auto& record:g_completed) {
        if (record.adapter!=&adapter) continue;
        bool matches=true;
        for (const auto& binding:record.bindings) {
            void* renderer=nullptr;
            for (int i=0;i<count;++i) {
                void* candidate=ArrayValue(renderers,i);
                if (ObjectName(candidate)==binding.renderer_name) {
                    if (renderer) { matches=false; break; }
                    renderer=candidate;
                }
            }
            void* mesh=binding.mesh.Get(); bool enabled=false;
            if (!renderer || !mesh || Invoke(Contract("skinned.get_shared_mesh"),renderer,nullptr)!=mesh ||
                !GetRendererEnabled(renderer,enabled) || enabled!=binding.enabled) { matches=false; break; }
#if defined(__ANDROID__)
            if (binding.check_shadow) {
                int32_t mode=-1;
                if (!InvokeValue(Contract("android.shadow_get"),renderer,nullptr,mode) || mode!=binding.shadow ||
                    Invoke(Contract("android.shadow_mesh_get"),renderer,nullptr)!=nullptr) { matches=false; break; }
            }
#endif
            if (!binding.bone_names.empty()) {
                void* bones=Invoke(Contract("skinned.get_bones"),renderer,nullptr);
                if (ArrayLength(bones)!=static_cast<int>(binding.bone_names.size())) { matches=false; break; }
                for (size_t b=0;b<binding.bone_names.size();++b)
                    if (ObjectName(ArrayValue(bones,static_cast<int>(b)))!=binding.bone_names[b]) { matches=false; break; }
                if (!matches) break;
            }
            void* materials=Invoke(Contract("renderer.get_shared_materials"),renderer,nullptr);
            if (ArrayLength(materials)!=static_cast<int>(binding.materials.size())) { matches=false; break; }
            for (size_t i=0;i<binding.materials.size();++i) {
                void* material=binding.materials[i].Get();
                if (!material || ArrayValue(materials,static_cast<int>(i))!=material) { matches=false; break; }
            }
            if (!matches) break;
        }
        if (matches) return true;
    }
    return false;
}
void PruneCompletedResources() {
    std::erase_if(g_completed,[](const auto& record) {
        if (record.root.Get()) return false;
        for (const auto& binding:record.bindings) {
            if (binding.generated_mesh && binding.mesh.Get()) return false;
            for (const auto& material:binding.materials) if (material.Get()) return false;
        }
        return true;
    });
}
#if defined(__ANDROID__)
void InspectAndroidRenderers() {
    static uint64_t next=0;
    if (!betterendfield::AndroidInspectionEnabled() || GetTickCount64()<next || g_completed.empty()) return;
    next=GetTickCount64()+5000;
    void* args[]{g_skinned_renderer_class.type_object};
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
        void* mesh=Invoke(Contract("skinned.get_shared_mesh"),renderer,nullptr); bool custom=false;
        for (const auto& record:g_completed) for (const auto& binding:record.bindings)
            if (binding.generated_mesh && binding.mesh.Get()==mesh) custom=true;
        const size_t lod=name.back()-'0';
        if (custom) ++customs[lod]; else ++originals[lod];
        if (!custom && examples++<3) Log("Android visible source renderer: "+BuildTransformPath(renderer)+
            " bones="+std::to_string(ArrayLength(Invoke(Contract("skinned.get_bones"),renderer,nullptr))));
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
    if (!IsCompletedResource(adapter,asset)) return false;
    bool inactive=true; void* args[]{g_skinned_renderer_class.type_object,&inactive};
    void* renderers=Invoke(Contract("game_object.renderers"),asset,args);
    for (const auto& component:bem.components) {
        PreparedBinding binding; binding.component_id=component.info.component_id;
        for (int i=0;i<ArrayLength(renderers);++i) {
            void* renderer=ArrayValue(renderers,i);
            if (ObjectName(renderer)!=adapter.components[binding.component_id].name) continue;
            if (binding.renderer) return false;
            binding.renderer=renderer;
        }
        if (!binding.renderer) return false;
        binding.custom_mesh=Invoke(Contract("skinned.get_shared_mesh"),binding.renderer,nullptr);
        binding.custom_materials=Invoke(Contract("renderer.get_shared_materials"),binding.renderer,nullptr);
        if (!binding.custom_mesh || !binding.custom_materials ||
            !GetRendererEnabled(binding.renderer,binding.original_enabled)) return false;
        if (component.info.flags&kComponentFlagNoGeometry) binding.original_mesh=binding.custom_mesh;
        else {
            binding.custom_bones=Invoke(Contract("skinned.get_bones"),binding.renderer,nullptr);
            binding.bone_names=component.bone_names;
            if (ArrayLength(binding.custom_bones)!=static_cast<int>(binding.bone_names.size())) return false;
        }
        bindings.push_back(binding);
    }
    Log("Android donor reuses a verified committed UI resource");
    return true;
}
#endif
struct PayloadCacheEntry {
    std::filesystem::path path;
    std::string appearance;
    std::shared_ptr<const BemPocData> payload;
    size_t bytes=0;
    uint64_t expires=0;
};
// Initial bounded CPU cache for closely spaced world/UI deliveries. Parameters
// remain an in-game measurement item; unused roles are never preloaded.
constexpr size_t kPayloadCacheLimit=128u*1024u*1024u;
constexpr uint64_t kPayloadCacheTtlMs=10000;
std::vector<PayloadCacheEntry> g_payload_cache;
void PrunePayloadCache(uint64_t now) {
    std::erase_if(g_payload_cache,[&](const auto& entry){return entry.expires<=now;});
}
std::shared_ptr<const BemPocData> AcquirePayload(const EnabledMod& mod) {
    const auto now=GetTickCount64(); PrunePayloadCache(now);
    for (auto& entry:g_payload_cache) if (entry.path==mod.package && entry.appearance==mod.appearance) {
        entry.expires=now+kPayloadCacheTtlMs; return entry.payload;
    }
    auto payload=std::make_shared<BemPocData>(); std::string error;
    if (!LoadBem(mod.package,*payload,error,mod.appearance) || !ValidatePayloadAdapter(*mod.adapter,*payload)) {
        Log("Package refused for "+std::string(mod.adapter->id)+": "+error); return {};
    }
    size_t size=0;
    for (const auto& component:payload->components) {
        for (const auto& stream:component.streams) size+=stream.size();
        size+=component.indices.size();
    }
    for (const auto& texture:payload->textures) size+=texture.data.size();
    if (size<=kPayloadCacheLimit) {
        size_t held=0; for (const auto& entry:g_payload_cache) held+=entry.bytes;
        while (held+size>kPayloadCacheLimit && !g_payload_cache.empty()) {
            held-=g_payload_cache.front().bytes; g_payload_cache.erase(g_payload_cache.begin());
        }
        g_payload_cache.push_back({mod.package,mod.appearance,payload,size,now+kPayloadCacheTtlMs});
    }
    return payload;
}
ModRegistry g_registry;
#include "../../../tools/CustomModel/developer-tools/native_probe.inl"
LodState g_lod;
std::atomic_bool g_enabled{false},g_stopping{false},g_standalone_lod{false},g_shutdown_ack{false};
std::mutex g_state_mutex,g_shutdown_mutex;
std::condition_variable g_shutdown_cv;
thread_local bool g_in_delivery=false;
uint64_t g_next_prune=0;
std::atomic<DWORD> g_pump_thread{0};
using DeliveryFn=void(__fastcall*)(void*,void*,void*);
using PumpFn=void(__fastcall*)(void*);
using RetireHooksFn=BE_Result(BE_CALL*)(void*,const char*);
DeliveryFn g_original_finish=nullptr;
PumpFn g_original_pump=nullptr;
RetireHooksFn g_retire_hooks=nullptr;

bool ProcessResource(void* asset,ConstructionScope& construction) {
    if (!RootTemporary(asset)) return false;
    const auto name=ObjectName(asset);
    const EnabledMod* mod=g_registry.Match(name);
    if (!mod) return false;
#if defined(__ANDROID__)
    auto ensure_ui=[&]() {
        if (name!=mod->adapter->world_resource) return true;
        void* handle=nullptr; uint32_t root=0;
        struct ReleaseDonor {
            void*& handle; uint32_t& root;
            ~ReleaseDonor() { betterendfield::AndroidReleaseUiDonor(handle,root); }
        } release{handle,root};
        bool ready=false;
        try {
            ConstructionScope ui_scope;
            void* donor=RootTemporary(betterendfield::AndroidLoadUiDonor(mod->adapter->ui_resource,handle,root));
            ready=donor && ProcessResource(donor,ui_scope);
        } catch (...) { ready=false; }
        if (ready && betterendfield::AndroidMeshRollbackTest() && betterendfield::AndroidPipelineLodEnabled()) {
            const bool restored=g_lod.Restore();
            const bool reapplied=restored && g_lod.Update(true);
            Log(std::string("Android LOD restore/reapply ")+(reapplied?"PASS":"FAIL"));
            ready=reapplied;
        }
        Log(std::string("Android paired world/UI ")+
            (betterendfield::AndroidMeshRollbackTest()?"validation ":"publication ")+(ready?"PASS":"FAIL"));
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
    if (IsCompletedResource(*mod->adapter,asset)) return true;
    auto payload=AcquirePayload(*mod);
    if (!payload) return false;
    std::vector<PreparedBinding> bindings;
    bool prepared=false;
#if defined(__ANDROID__)
    if (name==mod->adapter->world_resource)
        prepared=PrepareAndroidWorldResource(*mod->adapter,*payload,asset,bindings);
    else
#endif
        prepared=PrepareResource(*mod->adapter,*payload,asset,bindings);
    if (!prepared) {
        Log("Resource preparation failed; original retained: "+name); return false;
    }
    CompletedResource completed;
    if (!RememberResource(*mod->adapter,asset,bindings,completed) || construction.failed) return false;
    // Allocate bookkeeping before publication. The final move cannot allocate.
    g_completed.reserve(g_completed.size()+1);
    const auto result=CommitResource<PreparedBinding>(bindings,ApplyPreparedBinding,RestorePreparedBinding);
    if (result==CommitResult::Committed) {
#if defined(__ANDROID__)
        if (betterendfield::AndroidMeshRollbackTest()) {
            // Bindings already point at generated assets; preserve them if an
            // unexpected diagnostic exception interrupts restoration.
            construction.published=true;
            bool cache_ready=true;
            // Exercise the opposite load order while the UI is still bound:
            // a subsequent world delivery must reuse the verified UI result,
            // not compare its replacement counts to the original BEM counts.
            if (name==mod->adapter->ui_resource && g_android_test_world.Get()) {
                g_completed.push_back(std::move(completed));
                cache_ready=false;
                try {
                    ConstructionScope cache_probe;
                    std::vector<PreparedBinding> rebound;
                    cache_ready=PrepareAndroidWorldResource(*mod->adapter,*payload,
                        g_android_test_world.Get(),rebound);
                } catch (...) { cache_ready=false; }
                g_completed.pop_back();
                Log(std::string("Android cached UI donor/world prepare ")+(cache_ready?"PASS":"FAIL"));
            }
            bool restored=true;
            for (auto it=bindings.rbegin();it!=bindings.rend();++it)
                if (!RestorePreparedBinding(*it)) restored=false;
            construction.published=!restored;
            if (restored && name==mod->adapter->world_resource) g_android_test_world.Set(asset);
            Log(std::string("Android renderer commit/restore ")+(restored?"PASS: ":"FAIL: ")+name+
                " components="+std::to_string(bindings.size()));
            return restored && cache_ready && ensure_ui();
        }
#endif
        construction.published=true;
        g_completed.push_back(std::move(completed));
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
void __fastcall ResourceFinish(void* proxy,void* asset,void* method) {
    if (g_enabled.load(std::memory_order_acquire) && !g_stopping.load() && !g_in_delivery) {
        g_in_delivery=true;
        try {
            std::lock_guard lock(g_state_mutex);
            if (g_enabled.load() && !g_stopping.load()) {
                // Separate temporary ownership so a failed observation cannot
                // contaminate the following replacement transaction.
                try { ConstructionScope observation; CaptureNativeProbe(asset); }
                catch (const std::exception& error) { Log(std::string("Native probe failed: ")+error.what()); }
                catch (...) { Log("Native probe failed."); }
                ConstructionScope construction;
                if (!g_probe.sweep) ProcessResource(asset,construction);
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
    g_pump_thread.store(GetCurrentThreadId());
    try {
        std::lock_guard lock(g_state_mutex);
        if (!g_enabled.load()) return;
        ConstructionScope construction;
        const bool stop=g_stopping.load();
#if defined(__ANDROID__)
        // Mobile world resources start at LOD1. Preserve the game's LOD and
        // culling state; the Android adapter binds the actual resource LOD.
        const bool ready=g_lod.Update(!stop && !g_registry.enabled.empty() && betterendfield::AndroidPipelineLodEnabled());
#else
        const bool desired=!stop && EffectiveLodEnabled(!g_registry.enabled.empty(),g_standalone_lod.load());
        const bool ready=g_lod.Update(desired);
#endif
        if (stop && ready) { g_shutdown_ack.store(true); g_shutdown_cv.notify_all(); }
        const uint64_t now=GetTickCount64();
        if (now>=g_next_prune) {
            TickNativeProbe();
            PrunePayloadCache(now);
            PruneCompletedResources();
#if defined(__ANDROID__)
            InspectAndroidRenderers();
#endif
            g_next_prune=now+1000;
        }
    } catch (const std::exception& error) { Log(std::string("Resource maintenance failed: ")+error.what()); }
    catch (...) { Log("Resource maintenance failed."); }
}
bool ReadRuntimeRegistry() {
    std::array<char,4096> catalog{};
    if (g_host->copy_catalog_root(g_host->context,catalog.data(),catalog.size())<=0) return false;
    const auto root=Utf8Path(catalog.data())/"custom-model";
    try { ReadProbeRequest(root); }
    catch (const std::exception& error) { g_probe.active=false; WriteProbeStatus("request_failed",error.what()); Log(std::string("Native probe disabled: ")+error.what()); }
    std::string text;
#if defined(__ANDROID__)
    // Android modules do not have permission to create a sibling directory
    // below /data/local/tmp.  The host supplies the generated registry in
    // memory while package paths may still point at readable absolute files.
    std::array<char,65536> configured{};
    const int configured_size = g_host->copy_module_configuration(
        g_host->context, kModuleId, configured.data(), configured.size());
    if (configured_size > 0) text.assign(configured.data(),
        static_cast<size_t>(configured_size));
#else
    std::ifstream stream(root/"runtime.ini",std::ios::binary|std::ios::ate);
    if (stream) {
        const auto size=stream.tellg();
        if (size<0 || size>65536) return false;
        text.resize(static_cast<size_t>(size)); stream.seekg(0);
        if (!stream.read(text.data(),static_cast<std::streamsize>(text.size()))) return false;
    } else if (std::filesystem::exists(root/"runtime.ini")) return false;
#endif
    std::string error;
    if (!ParseModRegistry(text,root,g_registry,error)) { Log(error); return false; }
    if(g_probe.sweep) {
        g_registry.enabled.clear();
        Log("Native sweep session: model replacement paused in memory; saved configuration unchanged.");
    }
    g_standalone_lod.store(g_registry.standalone_lod);
    for (const auto& message:g_registry.diagnostics) Log(message);
    return true;
}
bool ResolveRuntimeContracts() {
    const bool models=!g_registry.enabled.empty() || g_probe.active;
    for (auto& method:g_methods) {
        const std::string_view key(method.key);
        if (key.starts_with("probe.") && !g_probe.active) continue;
#if defined(__ANDROID__)
        if (key.starts_with("quality.") ||
            (!betterendfield::AndroidPipelineLodEnabled() && (key.starts_with("pipeline.") || key.starts_with("culling.")))) continue;
#endif
        const bool lod=key.starts_with("pipeline.") || key.starts_with("quality.") ||
            key.starts_with("culling.") ||
            key=="pump.canvas_will_render" || key=="object.instance_id" || key=="object.is_alive";
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
    if (models && (!resolve_class("Mesh",g_mesh_class) || !resolve_class("SkinnedMeshRenderer",g_skinned_renderer_class) ||
        !resolve_class("Texture2D",g_texture2d_class) || !resolve_class("Material",g_material_class))) return false;
    HMODULE game=GetModuleHandleW(L"GameAssembly.dll");
    if (!game) return false;
    g_weak_new=reinterpret_cast<WeakNewFn>(GetProcAddress(game,"il2cpp_gchandle_new_weakref"));
    g_weak_target=reinterpret_cast<WeakTargetFn>(GetProcAddress(game,"il2cpp_gchandle_get_target"));
    g_static_get=reinterpret_cast<StaticFieldFn>(GetProcAddress(game,"il2cpp_field_static_get_value"));
    g_static_set=reinterpret_cast<StaticFieldFn>(GetProcAddress(game,"il2cpp_field_static_set_value"));
    // Optional for v24; v25 refuses construction if typed array allocation is unavailable.
    g_object_class=reinterpret_cast<ObjectClassFn>(GetProcAddress(game,"il2cpp_object_get_class"));
    g_array_new_specific=reinterpret_cast<ArrayNewSpecificFn>(GetProcAddress(game,"il2cpp_array_new_specific"));
    return g_weak_new && g_weak_target &&
#if !defined(__ANDROID__)
        g_lod.Resolve() &&
#else
        (!betterendfield::AndroidNpcParametersEnabled() || g_lod.Resolve()) &&
#endif
        (!models || ResolveEngineBindings());
}
BE_Result BE_CALL InitializeResourceModule(const BE_HostApiV1* host) {
#if defined(__ANDROID__)
    // The private Android platform adapter must be configured before this
    // shared transaction can be enabled. Never resolve PC raw setters here.
    if (!betterendfield::AndroidMeshBuilderReady()) return BE_Result_NotReady;
#endif
    if (!host || host->abi_version!=BETTER_ENDFIELD_MODULE_ABI_V1 || !host->log || !host->resolve_method ||
        !host->resolve_field || !host->resolve_class || !host->create_hook || !host->copy_catalog_root ||
        !host->copy_managed_string || !host->runtime_invoke || !host->object_new || !host->object_unbox ||
        !host->string_new || !host->gchandle_new || !host->gchandle_free) return BE_Result_InvalidArgument;
    g_host=host;
    try {
        BeginProbeStatus();
        if (!ReadRuntimeRegistry() || !ResolveRuntimeContracts()) { WriteProbeStatus("initialization_failed"); return BE_Result_ContractMismatch; }
#if defined(__ANDROID__)
        g_retire_hooks=host->release_module_hooks;
        if (!g_retire_hooks) return BE_Result_ContractMismatch;
#else
        HMODULE host_module=nullptr;
        if (!GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS|GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
            reinterpret_cast<LPCWSTR>(host->create_hook),&host_module)) return BE_Result_ContractMismatch;
        g_retire_hooks=reinterpret_cast<RetireHooksFn>(GetProcAddress(host_module,"BetterEndfield_RetireModuleHooksV1"));
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
            (betterendfield::AndroidPipelineLodEnabled() && (
                !install("pipeline.register_bias",reinterpret_cast<void*>(&AndroidRegisterLodBias),reinterpret_cast<void**>(&g_original_register_bias)) ||
                !install("culling.set_parent_lod_bias",reinterpret_cast<void*>(&ParentLodBias),reinterpret_cast<void**>(&g_original_parent_lod_bias)) ||
                !install("culling.set_art_tag_lod_bias",reinterpret_cast<void*>(&ArtTagLodBias),reinterpret_cast<void**>(&g_original_art_tag_lod_bias)))) ||
#endif
#if !defined(__ANDROID__)
            !install("culling.set_parent_lod_bias",reinterpret_cast<void*>(&ParentLodBias),reinterpret_cast<void**>(&g_original_parent_lod_bias)) ||
            !install("culling.set_art_tag_lod_bias",reinterpret_cast<void*>(&ArtTagLodBias),reinterpret_cast<void**>(&g_original_art_tag_lod_bias)) ||
#endif
            ((!g_registry.enabled.empty() || g_probe.active) && !install("resource.finish",reinterpret_cast<void*>(&ResourceFinish),reinterpret_cast<void**>(&g_original_finish)))) {
            g_retire_hooks(host->context,kModuleId);
            Log("Hook installation failed; entry points retired, module remains disabled.");
            return BE_Result_Failed;
        }
        g_stopping.store(false); g_shutdown_ack.store(false); g_enabled.store(true,std::memory_order_release);
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
            (betterendfield::AndroidMeshRollbackTest()?"rollback (original bindings restored)":"replace (bindings retained)")+
            (betterendfield::AndroidNpcParametersEnabled()?"; pipeline + NPC parameters; QualitySettings unchanged":
            betterendfield::AndroidPipelineLodEnabled()?"; pipeline bias only; quality/NPC/camera culling unchanged":
                "; global LOD/culling overrides disabled"));
#endif
        return BE_Result_Ok;
    } catch (const std::exception& error) {
        g_enabled.store(false);
        if (g_retire_hooks) g_retire_hooks(host->context,kModuleId);
        Log(std::string("Initialization failed: ")+error.what()); return BE_Result_Failed;
    }
}
BE_Result BE_CALL ResourceConfigurationChanged(const char* configuration) {
    if (!configuration) return BE_Result_InvalidArgument;
    // Host settings only expose the independent LOD preference. Package and
    // role selection remain fixed until restart.
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
    g_stopping.store(true);
    bool need_restore=false;
    {
        std::lock_guard lock(g_state_mutex); need_restore=g_lod.active;
        if (need_restore && g_pump_thread.load()==GetCurrentThreadId()) {
            ConstructionScope construction;
            bool restored=g_lod.Restore();
            g_shutdown_ack.store(restored);
        }
    }
    if (need_restore && !g_shutdown_ack.load()) {
        std::unique_lock lock(g_shutdown_mutex);
        g_shutdown_cv.wait_for(lock,std::chrono::seconds(2),[]{return g_shutdown_ack.load();});
        if (!g_shutdown_ack.load()) Log("LOD restoration was not acknowledged on the Unity thread before shutdown.");
    }
    g_enabled.store(false,std::memory_order_release);
    g_lod_bias_locked.store(false,std::memory_order_release);
    if (g_retire_hooks && g_retire_hooks(g_host->context,kModuleId)!=BE_Result_Ok)
        Log("Hook disable reported failure; pinned inactive detours remain pass-through.");
    std::lock_guard lock(g_state_mutex);
    g_payload_cache.clear(); g_completed.clear(); g_lod.pipeline.Reset();
    // Model bindings are intentionally not rolled back on module shutdown.
}
const BE_ModuleApiV1 kResourceApi{
    {kModuleId,"Custom Model","0.1.0",BETTER_ENDFIELD_MODULE_ABI_V1},
    InitializeResourceModule,ResourceConfigurationChanged,ShutdownResourceModule
};
}
BE_EXPORT const BE_ModuleApiV1* BE_CALL BetterEndfield_GetModuleApiV1() { return &kResourceApi; }
}
BOOL WINAPI DllMain(HINSTANCE instance,DWORD reason,LPVOID reserved) {
    if (reason==DLL_PROCESS_ATTACH) DisableThreadLibraryCalls(instance);
    if (reason==DLL_PROCESS_DETACH && reserved) BetterEndfield::CustomModel::g_process_terminating.store(true);
    return TRUE;
}
