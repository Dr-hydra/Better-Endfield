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
constexpr char kTargetRendererNameLod0[] = "S_actor_endminf_cloth_01_lod0";
constexpr char kTargetMeshNameLod0[] = "S_actor_endminf_cloth_01_lod0";
constexpr char kTargetRendererNameLod1[] = "S_actor_endminf_cloth_01_lod1";
constexpr char kTargetMeshNameLod1[] = "S_actor_endminf_cloth_01_lod1";
constexpr char kDefaultBemRelativePath[] = "custom-model/endmin-casualwear-c9.bempoc";
constexpr int kToggleHotkey = VK_F9;
constexpr int kMaxRenderers = 4096;
constexpr int kMaxSubMeshes = 256;
constexpr uint32_t kComponent9OriginalIndexCountLod0 = 101994;
constexpr uint32_t kComponent9OriginalIndexCountLod1 = 59301;
constexpr uint32_t kExpectedBemVertices = 12873;
constexpr uint32_t kExpectedBemIndices = 59073;
constexpr uint32_t kExpectedBemMaxBoneLod0 = 155;
constexpr uint32_t kExpectedBemMaxBoneLod1 = 148;
constexpr uint32_t kExpectedVb0Stride = 16;
constexpr uint32_t kExpectedVb1Lod0Stride = 12;
constexpr uint32_t kExpectedVb2Lod0Stride = 12;
constexpr uint32_t kExpectedVb1Lod1Stride = 8;
constexpr uint32_t kExpectedVb2Lod1Stride = 12;
constexpr uint64_t kActiveRefreshInterval = 120;

// The PoC only writes an Int32[] index list through the high-level Mesh API.
// Vertex streams are kept byte-for-byte identical to EFMI and copied through
// Unity's private graphics-API-independent Mesh::InternalSetVertexBufferData.
constexpr size_t kIl2CppArrayLengthOffset = 0x18;
constexpr size_t kIl2CppArrayDataOffset = 0x20;
constexpr int32_t kBoxedValueHeaderSize = 16;

#pragma pack(push, 1)
struct BemPocHeader {
    char magic[8];
    uint32_t version;
    uint32_t component_id;
    uint32_t vertex_count;
    uint32_t index_count;
    uint32_t max_bone_lod0;
    uint32_t max_bone_lod1;
    uint32_t original_index_count_lod0;
    uint32_t original_index_count_lod1;
    uint32_t vb0_stride;
    uint32_t vb1_lod0_stride;
    uint32_t vb2_lod0_stride;
    uint32_t vb1_lod1_stride;
    uint32_t vb2_lod1_stride;
};
#pragma pack(pop)
static_assert(sizeof(BemPocHeader) == 60);

struct BemPocData {
    BemPocHeader header{};
    std::vector<uint8_t> vb0;
    std::vector<uint8_t> vb1_lod0;
    std::vector<uint8_t> vb2_lod0;
    std::vector<uint8_t> vb1_lod1;
    std::vector<uint8_t> vb2_lod1;
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
BE_ResolvedClassV1 g_int32_class{};

std::atomic_bool g_toggle_requested{false};
std::atomic_bool g_operation_in_progress{false};
std::atomic_bool g_hotkey_thread_stop{false};
std::atomic_uint64_t g_pump_hits{0};
HANDLE g_hotkey_thread = nullptr;

struct MeshVariantState {
    void* original_mesh = nullptr;
    void* custom_mesh = nullptr;
    uint32_t original_handle = 0;
    uint32_t custom_handle = 0;
};

MeshVariantState g_lod0{};
MeshVariantState g_lod1{};

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
    {"object.instantiate",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Object", "Instantiate",
            "UnityEngine.Object", "UnityEngine.Object", 1}, true},

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
    {"mesh.get_vertex_buffer_count",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "get_vertexBufferCount",
            nullptr, "System.Int32", 0}, true},
    {"mesh.get_vertex_buffer_stride",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "GetVertexBufferStride",
            "System.Int32", "System.Int32", 1}, true},
    {"mesh.internal_set_vertex_buffer_data",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "InternalSetVertexBufferData",
            "System.Int32|System.IntPtr|System.Int32|System.Int32|System.Int32|System.Int32|UnityEngine.Rendering.MeshUpdateFlags",
            "System.Void", 7}, true},
    {"mesh.set_vertices",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "set_vertices",
            "UnityEngine.Vector3[]", "System.Void", 1}, true},
    {"mesh.set_uv",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "set_uv",
            "UnityEngine.Vector2[]", "System.Void", 1}, true},
    {"mesh.set_normals",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "set_normals",
            "UnityEngine.Vector3[]", "System.Void", 1}, true},
    {"mesh.set_colors",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "set_colors",
            "UnityEngine.Color[]", "System.Void", 1}, true},
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
    {"mesh.recalculate_normals",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "RecalculateNormals",
            nullptr, "System.Void", 0}, false},
    {"mesh.recalculate_tangents",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "RecalculateTangents",
            nullptr, "System.Void", 0}, false},
    {"mesh.recalculate_bounds",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "RecalculateBounds",
            nullptr, "System.Void", 0}, true},
};

constexpr std::array<FieldContract, 8> kBoneWeightFields{{
    {"weight0", {"UnityEngine.CoreModule.dll", "UnityEngine", "BoneWeight",