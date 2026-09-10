#include "BetterEndfield/ModuleApi.h"

#include <Windows.h>

#include <algorithm>
#include <array>
#include <atomic>
#include <cctype>
#include <cstdint>
#include <cstring>
#include <filesystem>
#include <fstream>
#include <string>
#include <string_view>
#include <vector>

namespace BetterEndfield::CustomModelModule {
namespace {

constexpr char kModuleId[] = "betterendfield.custom_model";
constexpr char kTargetRendererName[] = "S_actor_endminf_cloth_01_lod0";
constexpr char kTargetMeshName[] = "S_actor_endminf_cloth_01_lod0";
constexpr char kDefaultBemRelativePath[] = "custom-model/endmin-casualwear-c9.bempoc";
constexpr int kToggleHotkey = VK_F9;
constexpr int kMaxRenderers = 4096;
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

constexpr uint32_t kBemFlagUv0 = 1u << 0;
constexpr uint32_t kBemFlagSkin4 = 1u << 1;
constexpr uint32_t kBemFlagIndex16 = 1u << 2;
constexpr uint32_t kBemFlagPackedNormal = 1u << 3;
constexpr uint32_t kBemFlagTexCoord4SNorm = 1u << 4;
constexpr uint64_t kActiveRefreshInterval = 120;

#pragma pack(push, 1)
struct BemPocHeader {
    char magic[8];
    uint32_t version;
    uint32_t component_id;
    uint32_t original_index_count;
    uint32_t vertex_count;
    uint32_t index_count;
    uint32_t max_bone;
    uint32_t flags;
};
#pragma pack(pop)
static_assert(sizeof(BemPocHeader) == 36);

struct Float2 {
    float x;
    float y;
};

struct Float3 {
    float x;
    float y;
    float z;
};

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

struct BemPocData {
    BemPocHeader header{};
    std::vector<Float3> positions;
    std::vector<Float2> uvs;
    std::vector<uint32_t> packed_normals;
    std::vector<Byte4> texcoord4_snorm;
    std::vector<BoneWeightPoc> bone_weights;
    std::vector<uint16_t> indices;
};

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

std::atomic_bool g_toggle_requested{false};
std::atomic_bool g_operation_in_progress{false};
std::atomic_bool g_hotkey_thread_stop{false};
std::atomic_uint64_t g_pump_hits{0};
HANDLE g_hotkey_thread = nullptr;

void* g_replaced_renderer = nullptr;
void* g_original_mesh = nullptr;
void* g_custom_mesh = nullptr;
uint32_t g_renderer_handle = 0;
uint32_t g_original_mesh_handle = 0;
uint32_t g_custom_mesh_handle = 0;

using VoidInstanceFn = void(__fastcall*)(void* instance, void* method_info);
VoidInstanceFn g_original_pump = nullptr;

MethodContract g_methods[]{
    {"pump.process_dither_pitch",
        {"Gameplay.Beyond.dll", "Beyond.Gameplay.View", "CameraMono",
            "_ProcessDitherByPitch", nullptr, "System.Void", 0}, false},
    {"pump.evaluate_touched_entities",
        {"Gameplay.Beyond.dll", "Beyond.Gameplay.View", "CameraMono",
            "EvaluateAllTouchedEntities", nullptr, "System.Void", 0}, false},
    {"pump.process_dither_trace",
        {"Gameplay.Beyond.dll", "Beyond.Gameplay.View", "CameraMono",
            "_ProcessDitherByTrace", nullptr, "System.Void", 0}, false},

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

    // Metadata-only Mesh declaration queries. These never expose native graphics
    // resources and are used to prove the EFMI semantic remap against the live
    // source Mesh before any custom data is written.
    {"mesh.has_vertex_attribute",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "HasVertexAttribute",
            "UnityEngine.Rendering.VertexAttribute", "System.Boolean", 1}, false},
    {"mesh.get_vertex_attribute_dimension",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "GetVertexAttributeDimension",
            "UnityEngine.Rendering.VertexAttribute", "System.Int32", 1}, false},
    {"mesh.get_vertex_attribute_format",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "GetVertexAttributeFormat",
            "UnityEngine.Rendering.VertexAttribute",
            "UnityEngine.Rendering.VertexAttributeFormat", 1}, false},
    {"mesh.get_vertex_attribute_stream",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "GetVertexAttributeStream",
            "UnityEngine.Rendering.VertexAttribute", "System.Int32", 1}, false},
    {"mesh.get_vertex_attribute_offset",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "GetVertexAttributeOffset",
            "UnityEngine.Rendering.VertexAttribute", "System.Int32", 1}, false},

    // This is the non-generic CPU->Mesh channel writer used by Unity's
    // NativeArray channel setters. Unlike PoC-3, it does not retrieve or mutate
    // a GraphicsBuffer object. If stripped from the client we simply refuse the
    // raw-channel PoC instead of falling back to any native graphics path.
    {"mesh.set_native_array_for_channel",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "SetNativeArrayForChannelImpl",
            "UnityEngine.Rendering.VertexAttribute|UnityEngine.Rendering.VertexAttributeFormat|System.Int32|System.IntPtr|System.Int32|System.Int32|System.Int32|UnityEngine.Rendering.MeshUpdateFlags",
            "System.Void", 8}, false},

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
