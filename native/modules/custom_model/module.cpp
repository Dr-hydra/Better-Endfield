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
#include <limits>
#include <string>
#include <string_view>
#include <vector>

namespace BetterEndfield::CustomModelModule {
namespace {

constexpr char kModuleId[] = "betterendfield.custom_model";
constexpr char kTargetHint[] = "endminf";
constexpr int kToggleHotkey = VK_F9;
constexpr int kMaxRenderers = 4096;
constexpr int kMaxBones = 1024;
constexpr int kMaxSubMeshes = 256;
constexpr uint32_t kComponent9OriginalIndexCount = 101994;
constexpr char kDefaultBemRelativePath[] = "custom-model/endmin-casualwear-c9.bempoc";

// PoC-1 intentionally uses the same 64-bit IL2CPP array ABI already relied on
// by the repository's research probes: max_length @ 0x18, first element @ 0x20.
// This is kept local to the research PoC and is not part of the public Host ABI.
constexpr size_t kIl2CppArrayLengthOffset = 0x18;
constexpr size_t kIl2CppArrayDataOffset = 0x20;
constexpr int32_t kBoxedValueHeaderSize = 16;

constexpr uint32_t kBemFlagUv0 = 1u << 0;
constexpr uint32_t kBemFlagSkin4 = 1u << 1;
constexpr uint32_t kBemFlagIndex16 = 1u << 2;

struct EfmiComponentSignature {
    int component;
    uint32_t index_count;
};

constexpr std::array<EfmiComponentSignature, 11> kSampleEfmiComponents{{
    {0, 27615}, {1, 9000}, {2, 4524}, {3, 20577}, {4, 1638}, {5, 16524},
    {6, 117}, {7, 1386}, {8, 90}, {9, 101994}, {10, 2286},
}};

struct DrawRange {
    const char* name;
    uint32_t offset;
    uint32_t count;
};

// Component9 in the validated Endmin Casualwear EFMI sample shares one VB/IB
// across seven replacement draws. The generic BEM schema will carry this later;
// PoC-1 keeps it explicit so the existing converter remains compatible.
constexpr std::array<DrawRange, 7> kComponent9DrawRanges{{
    {"Clothes 01", 0, 10485},
    {"Clothes 02", 10485, 2424},
    {"Clothes 03", 12909, 5208},
    {"Shoes", 18117, 9564},
    {"Thighhighs", 27681, 28080},
    {"Watch", 55761, 1437},
    {"Component9.005", 57198, 1875},
}};

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
    bool resolved = false;
    int32_t offset = -1;
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
    {"object.get_instance_id",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Object", "GetInstanceID",
            nullptr, "System.Int32", 0}, false},
    {"object.destroy",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Object", "Destroy",
            "UnityEngine.Object", "System.Void", 1}, false},

    {"array.create_instance",
        {"mscorlib.dll", "System", "Array", "CreateInstance",
            "System.Type|System.Int32", "System.Array", 2}, true},
    {"array.get_length",
        {"mscorlib.dll", "System", "Array", "GetLength", "System.Int32",
            "System.Int32", 1}, true},
    {"array.get_value",
        {"mscorlib.dll", "System", "Array", "GetValue", "System.Int32",
            "System.Object", 1}, true},

    {"component.get_transform",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Component", "get_transform",
            nullptr, "UnityEngine.Transform", 0}, false},
    {"transform.get_parent",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "get_parent",
            nullptr, "UnityEngine.Transform", 0}, false},
    {"renderer.get_enabled",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Renderer", "get_enabled",
            nullptr, "System.Boolean", 0}, false},

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
            "get_rootBone", nullptr, "UnityEngine.Transform", 0}, true},

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
            "System.Int32[]|System.Int32|System.Boolean|System.Int32", "System.Void", 4}, true},
    {"mesh.recalculate_normals",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "RecalculateNormals",
            nullptr, "System.Void", 0}, true},
    {"mesh.recalculate_tangents",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "RecalculateTangents",
            nullptr, "System.Void", 0}, false},
    {"mesh.recalculate_bounds",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "RecalculateBounds",
            nullptr, "System.Void", 0}, true},
};

FieldContract g_bone_weight_fields[]{
    {"boneweight.weight0",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "BoneWeight", "m_Weight0", "System.Single"},
        kBoxedValueHeaderSize + 0},
    {"boneweight.weight1",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "BoneWeight", "m_Weight1", "System.Single"},
        kBoxedValueHeaderSize + 4},
    {"boneweight.weight2",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "BoneWeight", "m_Weight2", "System.Single"},
        kBoxedValueHeaderSize + 8},
    {"boneweight.weight3",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "BoneWeight", "m_Weight3", "System.Single"},
        kBoxedValueHeaderSize + 12},
    {"boneweight.index0",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "BoneWeight", "m_BoneIndex0", "System.Int32"},
        kBoxedValueHeaderSize + 16},
    {"boneweight.index1",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "BoneWeight", "m_BoneIndex1", "System.Int32"},
        kBoxedValueHeaderSize + 20},
    {"boneweight.index2",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "BoneWeight", "m_BoneIndex2", "System.Int32"},
        kBoxedValueHeaderSize + 24},
    {"boneweight.index3",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "BoneWeight", "m_BoneIndex3", "System.Int32"},
        kBoxedValueHeaderSize + 28},
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
    if (!method || !method->resolved) return false;
    void* exception = nullptr;
    g_host->runtime_invoke(g_host->context, method->method_info, instance, parameters,
        &exception);
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
    return object ? ManagedString(Invoke(Contract("object.get_name"), object, nullptr, false))
                  : std::string{};
}

int ArrayLength(void* array) {
    int dimension = 0;
    int length = 0;
    void* parameters[1]{&dimension};
    return array && InvokeValue(Contract("array.get_length"), array, parameters, length)
        ? length : 0;
}

void* ArrayValue(void* array, int index) {
    void* parameters[1]{&index};
    return array ? Invoke(Contract("array.get_value"), array, parameters, false)
                 : nullptr;
}

std::string LowerAscii(std::string value) {
    std::transform(value.begin(), value.end(), value.begin(),
        [](unsigned char c) { return static_cast<char>(std::tolower(c)); });
    return value;
}

bool ContainsTargetHint(std::string_view value) {
    return !value.empty() &&
        LowerAscii(std::string(value)).find(kTargetHint) != std::string::npos;
}

std::string BuildTransformPath(void* component) {
    MethodContract* get_transform = Contract("component.get_transform");
    MethodContract* get_parent = Contract("transform.get_parent");
    if (!component || !get_transform || !get_transform->resolved ||
        !get_parent || !get_parent->resolved) {
        return {};
    }

    std::vector<std::string> names;
    void* transform = Invoke(get_transform, component, nullptr, false);
    for (int depth = 0; transform && depth < 24; ++depth) {
        std::string name = ObjectName(transform);
        names.push_back(name.empty() ? "<unnamed>" : std::move(name));
        transform = Invoke(get_parent, transform, nullptr, false);
    }
    std::reverse(names.begin(), names.end());

    std::string path;
    for (const auto& name : names) {
        if (!path.empty()) path.push_back('/');
        path += name;
    }
    return path;
}

std::string EfmiComponentMatch(uint32_t index_count) {
    for (const auto& signature : kSampleEfmiComponents) {
        if (signature.index_count == index_count) {
            return "C" + std::to_string(signature.component);
        }
    }
    return {};
}

std::filesystem::path Utf8Path(std::string_view value) {
    if (value.empty()) return {};
    const int wide_count = MultiByteToWideChar(CP_UTF8, MB_ERR_INVALID_CHARS,
        value.data(), static_cast<int>(value.size()), nullptr, 0);
    if (wide_count <= 0) return std::filesystem::path(std::string(value));
    std::wstring wide(static_cast<size_t>(wide_count), L'\0');
    MultiByteToWideChar(CP_UTF8, MB_ERR_INVALID_CHARS, value.data(),
        static_cast<int>(value.size()), wide.data(), wide_count);
    return std::filesystem::path(wide);
}

std::filesystem::path BemPath() {
    if (!g_host || !g_host->copy_catalog_root) return {};
    std::array<char, 4096> root{};
    if (g_host->copy_catalog_root(g_host->context, root.data(), root.size()) <= 0) {
        return {};
    }
    return Utf8Path(root.data()) / Utf8Path(kDefaultBemRelativePath);
}

bool CheckedAdvance(size_t& cursor, size_t amount, size_t limit) {
    if (amount > limit || cursor > limit - amount) return false;
    cursor += amount;
    return true;
}

bool LoadBemPoc(const std::filesystem::path& path, BemPocData& output) {
    output = {};
    std::ifstream input(path, std::ios::binary | std::ios::ate);
    if (!input) {
        Log("BEM PoC file not found: " + path.string());
        return false;
    }
    const std::streamoff end = input.tellg();
    if (end < static_cast<std::streamoff>(sizeof(BemPocHeader)) ||
        end > static_cast<std::streamoff>(256 * 1024 * 1024)) {
        Log("BEM PoC file size is invalid.");
        return false;
    }
    std::vector<uint8_t> bytes(static_cast<size_t>(end));
    input.seekg(0, std::ios::beg);
    if (!input.read(reinterpret_cast<char*>(bytes.data()),
            static_cast<std::streamsize>(bytes.size()))) {
        Log("Failed to read BEM PoC file.");
        return false;
    }

    std::memcpy(&output.header, bytes.data(), sizeof(output.header));
    const std::array<char, 8> expected_magic{'B','E','M','P','O','C','1','\0'};
    if (std::memcmp(output.header.magic, expected_magic.data(), expected_magic.size()) != 0 ||
        output.header.version != 1 || output.header.component_id != 9 ||
        output.header.original_index_count != kComponent9OriginalIndexCount) {
        Log("BEM PoC header does not match Endmin Casualwear Component9 PoC-1.");
        return false;
    }
    const uint32_t required_flags = kBemFlagUv0 | kBemFlagSkin4 | kBemFlagIndex16;
    if ((output.header.flags & required_flags) != required_flags ||
        output.header.vertex_count == 0 || output.header.index_count == 0 ||
        output.header.vertex_count > 1'000'000 || output.header.index_count > 6'000'000 ||
        output.header.max_bone > 4095) {
        Log("BEM PoC counts or flags are invalid.");
        return false;
    }

    size_t cursor = sizeof(BemPocHeader);
    const size_t position_bytes = static_cast<size_t>(output.header.vertex_count) * sizeof(Float3);
    const size_t uv_bytes = static_cast<size_t>(output.header.vertex_count) * sizeof(Float2);
    const size_t weight_bytes = static_cast<size_t>(output.header.vertex_count) * sizeof(float) * 4;
    const size_t bone_index_bytes = static_cast<size_t>(output.header.vertex_count) * 4;
    const size_t index_bytes = static_cast<size_t>(output.header.index_count) * sizeof(uint16_t);
    const size_t expected_size = cursor + position_bytes + uv_bytes + weight_bytes +
        bone_index_bytes + index_bytes;
    if (expected_size != bytes.size()) {
        Log("BEM PoC payload size mismatch.");
        return false;
    }

    output.positions.resize(output.header.vertex_count);
    std::memcpy(output.positions.data(), bytes.data() + cursor, position_bytes);
    if (!CheckedAdvance(cursor, position_bytes, bytes.size())) return false;

    output.uvs.resize(output.header.vertex_count);
    std::memcpy(output.uvs.data(), bytes.data() + cursor, uv_bytes);
    if (!CheckedAdvance(cursor, uv_bytes, bytes.size())) return false;

    const uint8_t* weights = bytes.data() + cursor;
    if (!CheckedAdvance(cursor, weight_bytes, bytes.size())) return false;
    const uint8_t* bones = bytes.data() + cursor;
    if (!CheckedAdvance(cursor, bone_index_bytes, bytes.size())) return false;

    output.bone_weights.resize(output.header.vertex_count);
    for (uint32_t i = 0; i < output.header.vertex_count; ++i) {
        BoneWeightPoc item{};
        std::memcpy(&item.weight0, weights + static_cast<size_t>(i) * 16, 16);
        const uint8_t* bi = bones + static_cast<size_t>(i) * 4;
        item.bone_index0 = bi[0];
        item.bone_index1 = bi[1];
        item.bone_index2 = bi[2];
        item.bone_index3 = bi[3];
        output.bone_weights[i] = item;
    }

    output.indices.resize(output.header.index_count);
    std::memcpy(output.indices.data(), bytes.data() + cursor, index_bytes);
    if (!CheckedAdvance(cursor, index_bytes, bytes.size()) || cursor != bytes.size()) {
        return false;
    }

    for (uint16_t index : output.indices) {
        if (index >= output.header.vertex_count) {
            Log("BEM PoC index buffer references a vertex outside vertexCount.");
            return false;
        }
    }
    for (const DrawRange& range : kComponent9DrawRanges) {
        if (range.offset > output.header.index_count ||
            range.count > output.header.index_count - range.offset) {
            Log("BEM PoC Component9 draw range exceeds index buffer.");
            return false;
        }
    }

    Log("Loaded BEM PoC: vertices=" + std::to_string(output.header.vertex_count) +
        " indices=" + std::to_string(output.header.index_count) +
        " maxBone=" + std::to_string(output.header.max_bone));
    return true;
}

void* CreateManagedArray(const BE_ResolvedClassV1& element_class, int count) {
    if (!element_class.type_object || count < 0) return nullptr;
    void* parameters[2]{element_class.type_object, &count};
    void* array = Invoke(Contract("array.create_instance"), nullptr, parameters);
    if (!array || ArrayLength(array) != count) return nullptr;
    return array;
}

template <typename T>
T* ManagedArrayData(void* array, size_t expected_count) {
    if (!array) return nullptr;
    __try {
        const uintptr_t length = *reinterpret_cast<const uintptr_t*>(
            static_cast<const uint8_t*>(array) + kIl2CppArrayLengthOffset);
        if (length != expected_count) return nullptr;
        return reinterpret_cast<T*>(static_cast<uint8_t*>(array) + kIl2CppArrayDataOffset);
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return nullptr;
    }
}

bool ValidateBoneWeightLayout() {
    bool complete = true;
    for (auto& field : g_bone_weight_fields) {
        BE_ResolvedFieldV1 result{};
        const BE_Result status = g_host->resolve_field(
            g_host->context, &field.descriptor, &result);
        field.resolved = status == BE_Result_Ok && result.field_info;
        field.offset = result.offset;
        if (!field.resolved || field.offset != field.expected_offset) {
            complete = false;
            Log(std::string("BoneWeight layout mismatch: ") + field.key +
                " expected=" + std::to_string(field.expected_offset) +
                " actual=" + std::to_string(field.offset));
        }
    }
    if (complete) {
        Log("Validated UnityEngine.BoneWeight 32-byte PoC layout.");
    }
    return complete;
}

struct RendererSummary {
    void* renderer = nullptr;
    void* mesh = nullptr;
    std::string renderer_name;
    std::string mesh_name;
    std::string path;
    std::string root_bone_name;
    uint64_t total_indices = 0;
    bool c9_exact_submesh = false;
    bool c9_exact_total = false;
    int bones_count = 0;
    int bindpose_count = 0;
};

RendererSummary SummarizeRenderer(void* renderer) {
    RendererSummary summary;
    summary.renderer = renderer;
    summary.renderer_name = ObjectName(renderer);
    summary.path = BuildTransformPath(renderer);
    summary.mesh = Invoke(Contract("skinned.get_shared_mesh"), renderer, nullptr, false);
    summary.mesh_name = ObjectName(summary.mesh);
    void* root_bone = Invoke(Contract("skinned.get_root_bone"), renderer, nullptr, false);
    summary.root_bone_name = ObjectName(root_bone);
    summary.bones_count = ArrayLength(Invoke(Contract("skinned.get_bones"), renderer, nullptr, false));

    if (summary.mesh) {
        int sub_mesh_count = 0;
        if (InvokeValue(Contract("mesh.get_sub_mesh_count"), summary.mesh, nullptr,
                sub_mesh_count)) {
            const int count = std::min(std::max(sub_mesh_count, 0), kMaxSubMeshes);
            for (int submesh = 0; submesh < count; ++submesh) {
                uint32_t index_count = 0;
                void* parameters[1]{&submesh};
                if (InvokeValue(Contract("mesh.get_index_count"), summary.mesh,
                        parameters, index_count)) {
                    summary.total_indices += index_count;
                    if (index_count == kComponent9OriginalIndexCount) {
                        summary.c9_exact_submesh = true;
                    }
                }
            }
        }
        summary.c9_exact_total = summary.total_indices == kComponent9OriginalIndexCount;
        summary.bindpose_count = ArrayLength(
            Invoke(Contract("mesh.get_bindposes"), summary.mesh, nullptr, false));
    }
    return summary;
}

void LogRendererSummary(const RendererSummary& summary, int ordinal) {
    std::string line = "  candidate[" + std::to_string(ordinal) + "] renderer=\"" +
        summary.renderer_name + "\" mesh=\"" + summary.mesh_name +
        "\" totalIndices=" + std::to_string(summary.total_indices) +
        " bones=" + std::to_string(summary.bones_count) +
        " bindposes=" + std::to_string(summary.bindpose_count);
    if (summary.c9_exact_submesh) line += " C9_EXACT_SUBMESH";
    if (summary.c9_exact_total) line += " C9_EXACT_TOTAL";
    if (ContainsTargetHint(summary.renderer_name) || ContainsTargetHint(summary.mesh_name) ||
        ContainsTargetHint(summary.path) || ContainsTargetHint(summary.root_bone_name)) {
        line += " TARGET_HINT";
    }
    Log(line);
    if (!summary.path.empty()) Log("    path=\"" + summary.path + "\"");
}

std::vector<RendererSummary> EnumerateRenderers() {
    std::vector<RendererSummary> summaries;
    void* parameters[1]{g_skinned_renderer_class.type_object};
    void* objects = Invoke(Contract("resources.find_objects_of_type_all"), nullptr,
        parameters);
    const int raw_count = ArrayLength(objects);
    const int count = std::min(std::max(raw_count, 0), kMaxRenderers);
    summaries.reserve(static_cast<size_t>(count));
    for (int index = 0; index < count; ++index) {
        void* renderer = ArrayValue(objects, index);
        if (renderer) summaries.push_back(SummarizeRenderer(renderer));
    }
    Log("SkinnedMeshRenderer objects discovered=" + std::to_string(raw_count));
    return summaries;
}

RendererSummary* SelectComponent9Renderer(std::vector<RendererSummary>& summaries) {
    std::vector<size_t> exact;
    std::vector<size_t> hinted_exact;
    for (size_t i = 0; i < summaries.size(); ++i) {
        const auto& item = summaries[i];
        const bool signature = item.c9_exact_submesh || item.c9_exact_total;
        if (!signature) continue;
        exact.push_back(i);
        const bool hinted = ContainsTargetHint(item.renderer_name) ||
            ContainsTargetHint(item.mesh_name) || ContainsTargetHint(item.path) ||
            ContainsTargetHint(item.root_bone_name);
        if (hinted) hinted_exact.push_back(i);
    }

    if (hinted_exact.size() == 1) return &summaries[hinted_exact.front()];
    if (exact.size() == 1) return &summaries[exact.front()];

    Log("Could not select a unique Component9 renderer. exact=" +
        std::to_string(exact.size()) + " hintedExact=" +
        std::to_string(hinted_exact.size()));
    int ordinal = 0;
    for (const auto& item : summaries) {
        if (item.c9_exact_submesh || item.c9_exact_total ||
            ContainsTargetHint(item.renderer_name) || ContainsTargetHint(item.mesh_name) ||
            ContainsTargetHint(item.path) || ContainsTargetHint(item.root_bone_name)) {
            LogRendererSummary(item, ordinal++);
        }
    }
    return nullptr;
}

bool SetSharedMesh(void* renderer, void* mesh) {
    void* parameters[1]{mesh};
    return InvokeVoid(Contract("skinned.set_shared_mesh"), renderer, parameters);
}

void ReleaseReplacementHandles() {
    if (!g_host || !g_host->gchandle_free) return;
    if (g_renderer_handle) g_host->gchandle_free(g_host->context, g_renderer_handle);
    if (g_original_mesh_handle) g_host->gchandle_free(g_host->context, g_original_mesh_handle);
    if (g_custom_mesh_handle) g_host->gchandle_free(g_host->context, g_custom_mesh_handle);
    g_renderer_handle = 0;
    g_original_mesh_handle = 0;
    g_custom_mesh_handle = 0;
}

void DestroyCustomMesh(void* mesh) {
    MethodContract* destroy = Contract("object.destroy");
    if (!mesh || !destroy || !destroy->resolved) return;
    void* parameters[1]{mesh};
    InvokeVoid(destroy, nullptr, parameters);
}

void ClearReplacementState(bool destroy_custom) {
    void* doomed = g_custom_mesh;
    ReleaseReplacementHandles();
    g_replaced_renderer = nullptr;
    g_original_mesh = nullptr;
    g_custom_mesh = nullptr;
    if (destroy_custom) DestroyCustomMesh(doomed);
}

bool BuildMeshFromBem(const BemPocData& bem, void* original_mesh, void*& new_mesh) {
    new_mesh = nullptr;
    if (!ValidateBoneWeightLayout()) {
        Log("Refusing PoC replacement because BoneWeight layout validation failed.");
        return false;
    }

    void* bindposes = Invoke(Contract("mesh.get_bindposes"), original_mesh, nullptr);
    const int bindpose_count = ArrayLength(bindposes);
    if (!bindposes || bindpose_count <= static_cast<int>(bem.header.max_bone)) {
        Log("Original C9 bindpose palette is too small for BEM maxBone=" +
            std::to_string(bem.header.max_bone) + " bindposes=" +
            std::to_string(bindpose_count));
        return false;
    }

    void* positions = CreateManagedArray(g_vector3_class,
        static_cast<int>(bem.positions.size()));
    void* uvs = CreateManagedArray(g_vector2_class, static_cast<int>(bem.uvs.size()));
    void* bone_weights = CreateManagedArray(g_bone_weight_class,
        static_cast<int>(bem.bone_weights.size()));
    if (!positions || !uvs || !bone_weights) {
        Log("Failed to allocate managed vertex arrays.");
        return false;
    }

    Float3* position_data = ManagedArrayData<Float3>(positions, bem.positions.size());
    Float2* uv_data = ManagedArrayData<Float2>(uvs, bem.uvs.size());
    BoneWeightPoc* weight_data = ManagedArrayData<BoneWeightPoc>(
        bone_weights, bem.bone_weights.size());
    if (!position_data || !uv_data || !weight_data) {
        Log("IL2CPP managed array layout validation failed.");
        return false;
    }
    std::memcpy(position_data, bem.positions.data(), bem.positions.size() * sizeof(Float3));
    std::memcpy(uv_data, bem.uvs.data(), bem.uvs.size() * sizeof(Float2));
    std::memcpy(weight_data, bem.bone_weights.data(),
        bem.bone_weights.size() * sizeof(BoneWeightPoc));

    void* mesh = g_host->object_new(g_host->context, g_mesh_class.class_info);
    if (!mesh || !InvokeVoid(Contract("mesh.ctor"), mesh, nullptr)) {
        Log("Failed to construct UnityEngine.Mesh.");
        return false;
    }

    if (auto* set_name = Contract("object.set_name"); set_name && set_name->resolved &&
        g_host->string_new) {
        void* name = g_host->string_new(g_host->context, "BetterEndfield.C9.BEM.PoC");
        void* parameters[1]{name};
        InvokeVoid(set_name, mesh, parameters);
    }

    void* p_vertices[1]{positions};
    void* p_uvs[1]{uvs};
    void* p_weights[1]{bone_weights};
    void* p_bindposes[1]{bindposes};
    if (!InvokeVoid(Contract("mesh.set_vertices"), mesh, p_vertices) ||
        !InvokeVoid(Contract("mesh.set_uv"), mesh, p_uvs) ||
        !InvokeVoid(Contract("mesh.set_bone_weights"), mesh, p_weights) ||
        !InvokeVoid(Contract("mesh.set_bindposes"), mesh, p_bindposes)) {
        Log("Failed while assigning vertex/UV/skin/bindpose data to custom mesh.");
        DestroyCustomMesh(mesh);
        return false;
    }

    int sub_mesh_count = static_cast<int>(kComponent9DrawRanges.size());
    void* p_sub_mesh_count[1]{&sub_mesh_count};
    if (!InvokeVoid(Contract("mesh.set_sub_mesh_count"), mesh, p_sub_mesh_count)) {
        DestroyCustomMesh(mesh);
        return false;
    }

    for (int submesh = 0; submesh < sub_mesh_count; ++submesh) {
        const DrawRange& range = kComponent9DrawRanges[static_cast<size_t>(submesh)];
        void* triangle_array = CreateManagedArray(g_int32_class,
            static_cast<int>(range.count));
        int32_t* triangle_data = ManagedArrayData<int32_t>(triangle_array, range.count);
        if (!triangle_array || !triangle_data) {
            Log("Failed to allocate index array for draw range " +
                std::string(range.name));
            DestroyCustomMesh(mesh);
            return false;
        }
        for (uint32_t i = 0; i < range.count; ++i) {
            triangle_data[i] = bem.indices[static_cast<size_t>(range.offset) + i];
        }
        bool calculate_bounds = false;
        int base_vertex = 0;
        void* parameters[4]{triangle_array, &submesh, &calculate_bounds, &base_vertex};
        if (!InvokeVoid(Contract("mesh.set_triangles"), mesh, parameters)) {
            Log("SetTriangles failed for draw range " + std::string(range.name));
            DestroyCustomMesh(mesh);
            return false;
        }
    }

    if (!InvokeVoid(Contract("mesh.recalculate_normals"), mesh, nullptr)) {
        DestroyCustomMesh(mesh);
        return false;
    }
    if (auto* tangents = Contract("mesh.recalculate_tangents");
        tangents && tangents->resolved) {
        InvokeVoid(tangents, mesh, nullptr);
    }
    if (!InvokeVoid(Contract("mesh.recalculate_bounds"), mesh, nullptr)) {
        DestroyCustomMesh(mesh);
        return false;
    }

    new_mesh = mesh;
    return true;
}

bool ApplyReplacement() {
    BemPocData bem;
    const std::filesystem::path path = BemPath();
    if (path.empty()) {
        Log("Catalog root is unavailable; cannot locate BEM PoC file.");
        return false;
    }
    if (!LoadBemPoc(path, bem)) {
        Log("Generate it with tools/CustomModel/convert_efmi_poc.py, then press F9 again.");
        return false;
    }

    std::vector<RendererSummary> summaries = EnumerateRenderers();
    RendererSummary* target = SelectComponent9Renderer(summaries);
    if (!target || !target->renderer || !target->mesh) return false;

    if (target->bones_count <= static_cast<int>(bem.header.max_bone)) {
        Log("Selected C9 renderer bone palette is too small: bones=" +
            std::to_string(target->bones_count) + " maxBone=" +
            std::to_string(bem.header.max_bone));
        LogRendererSummary(*target, 0);
        return false;
    }

    Log("Selected Component9 renderer for PoC replacement:");
    LogRendererSummary(*target, 0);

    void* custom_mesh = nullptr;
    if (!BuildMeshFromBem(bem, target->mesh, custom_mesh)) return false;
    if (!SetSharedMesh(target->renderer, custom_mesh)) {
        Log("Failed to assign custom sharedMesh; original renderer left unchanged.");
        DestroyCustomMesh(custom_mesh);
        return false;
    }

    g_replaced_renderer = target->renderer;
    g_original_mesh = target->mesh;
    g_custom_mesh = custom_mesh;
    if (g_host->gchandle_new) {
        g_renderer_handle = g_host->gchandle_new(g_host->context, g_replaced_renderer, 0);
        g_original_mesh_handle = g_host->gchandle_new(g_host->context, g_original_mesh, 0);
        g_custom_mesh_handle = g_host->gchandle_new(g_host->context, g_custom_mesh, 0);
    }

    Log("F9 replacement applied: Component9 custom BEM mesh is now active. Press F9 again to restore original sharedMesh.");
    return true;
}

bool RollbackReplacement() {
    if (!g_custom_mesh || !g_original_mesh) {
        ClearReplacementState(false);
        return true;
    }

    bool restored = false;
    std::vector<RendererSummary> summaries = EnumerateRenderers();
    for (auto& item : summaries) {
        if (item.mesh == g_custom_mesh) {
            restored = SetSharedMesh(item.renderer, g_original_mesh);
            if (restored) {
                Log("Restored original sharedMesh on current renderer instance.");
                break;
            }
        }
    }
    if (!restored && g_replaced_renderer) {
        void* current = Invoke(Contract("skinned.get_shared_mesh"), g_replaced_renderer,
            nullptr, false);
        if (current == g_custom_mesh) {
            restored = SetSharedMesh(g_replaced_renderer, g_original_mesh);
        }
    }

    if (!restored) {
        Log("Rollback could not find the renderer carrying the PoC mesh; clearing session state without touching other renderers.");
        ClearReplacementState(false);
        return false;
    }

    void* old_custom = g_custom_mesh;
    ReleaseReplacementHandles();
    g_replaced_renderer = nullptr;
    g_original_mesh = nullptr;
    g_custom_mesh = nullptr;
    DestroyCustomMesh(old_custom);
    Log("F9 rollback complete. Original Component9 mesh restored.");
    return true;
}

void ToggleReplacement() {
    if (g_operation_in_progress.exchange(true, std::memory_order_acq_rel)) return;
    if (g_custom_mesh) {
        RollbackReplacement();
    } else {
        ApplyReplacement();
    }
    g_operation_in_progress.store(false, std::memory_order_release);
}

DWORD WINAPI HotkeyThread(void*) {
    bool held = false;
    while (!g_hotkey_thread_stop.load(std::memory_order_acquire)) {
        const bool down = (GetAsyncKeyState(kToggleHotkey) & 0x8000) != 0;
        if (down && !held) {
            g_toggle_requested.store(true, std::memory_order_release);
        }
        held = down;
        Sleep(15);
    }
    return 0;
}

void PumpCustomModel() {
    const uint64_t hit = g_pump_hits.fetch_add(1, std::memory_order_relaxed) + 1;
    if (hit == 1) {
        Log("CustomModel main-thread pump observed first runtime call.");
    }
    if (g_toggle_requested.exchange(false, std::memory_order_acq_rel)) {
        Log("F9 CustomModel request consumed on Unity main thread.");
        ToggleReplacement();
    }
}

void __fastcall DetourPump(void* instance, void* method_info) {
    if (g_original_pump) g_original_pump(instance, method_info);
    PumpCustomModel();
}

bool ResolveClass(const char* assembly, const char* namespc, const char* klass,
    BE_ResolvedClassV1& output) {
    output = {};
    const BE_Result status = g_host->resolve_class(
        g_host->context, assembly, namespc, klass, &output);
    if (status != BE_Result_Ok || !output.class_info || !output.type_object) {
        Log(std::string("Required class contract not found: ") + namespc + "." + klass);
        return false;
    }
    return true;
}

bool ResolveContracts() {
    bool required_ready = true;
    for (auto& method : g_methods) {
        BE_ResolvedMethodV1 resolved{};
        const BE_Result result = g_host->resolve_method(
            g_host->context, &method.descriptor, &resolved);
        if (result == BE_Result_Ok && resolved.method_info && resolved.method_pointer) {
            method.pointer = resolved.method_pointer;
            method.method_info = resolved.method_info;
            method.resolved = true;
        } else {
            Log(std::string(method.required ? "Required" : "Optional") +
                " method not found: " + method.key);
            if (method.required) required_ready = false;
        }
    }

    required_ready &= ResolveClass("UnityEngine.CoreModule.dll", "UnityEngine",
        "SkinnedMeshRenderer", g_skinned_renderer_class);
    required_ready &= ResolveClass("UnityEngine.CoreModule.dll", "UnityEngine",
        "Mesh", g_mesh_class);
    required_ready &= ResolveClass("UnityEngine.CoreModule.dll", "UnityEngine",
        "Vector3", g_vector3_class);
    required_ready &= ResolveClass("UnityEngine.CoreModule.dll", "UnityEngine",
        "Vector2", g_vector2_class);
    required_ready &= ResolveClass("UnityEngine.CoreModule.dll", "UnityEngine",
        "BoneWeight", g_bone_weight_class);
    required_ready &= ResolveClass("mscorlib.dll", "System", "Int32", g_int32_class);
    return required_ready;
}

bool InstallPumpHook() {
    constexpr std::array<std::string_view, 3> candidates{
        "pump.process_dither_pitch",
        "pump.evaluate_touched_entities",
        "pump.process_dither_trace"};

    for (std::string_view key : candidates) {
        MethodContract* method = Contract(key);
        if (!method || !method->resolved || !method->pointer) continue;
        void* original = nullptr;
        const BE_Result result = g_host->create_hook(
            g_host->context, kModuleId, method->pointer,
            reinterpret_cast<void*>(&DetourPump), &original);
        if (result == BE_Result_Ok) {
            g_original_pump = reinterpret_cast<VoidInstanceFn>(original);
            Log(std::string("CustomModel main-thread pump installed: ") + method->key);
            return true;
        }
        Log(std::string("CustomModel pump unavailable: ") + method->key +
            " result=" + std::to_string(static_cast<int>(result)));
    }
    return false;
}

bool StartHotkeyThread() {
    g_hotkey_thread_stop.store(false, std::memory_order_release);
    g_hotkey_thread = CreateThread(nullptr, 0, &HotkeyThread, nullptr, 0, nullptr);
    if (!g_hotkey_thread) {
        Log("Failed to start F9 hotkey latch thread.");
        return false;
    }
    return true;
}

void StopHotkeyThread() {
    g_hotkey_thread_stop.store(true, std::memory_order_release);
    if (g_hotkey_thread) {
        WaitForSingleObject(g_hotkey_thread, 1000);
        CloseHandle(g_hotkey_thread);
        g_hotkey_thread = nullptr;
    }
}

BE_Result BE_CALL Initialize(const BE_HostApiV1* host) {
    if (!host || host->abi_version != BETTER_ENDFIELD_MODULE_ABI_V1 ||
        !host->resolve_method || !host->resolve_field || !host->resolve_class ||
        !host->object_new || !host->runtime_invoke || !host->object_unbox ||
        !host->copy_managed_string || !host->copy_catalog_root ||
        !host->create_hook || !host->release_module_hooks || !host->log) {
        return BE_Result_InvalidArgument;
    }

    g_host = host;
    g_toggle_requested.store(false, std::memory_order_release);
    g_operation_in_progress.store(false, std::memory_order_release);
    g_pump_hits.store(0, std::memory_order_release);
    ClearReplacementState(false);

    if (!ResolveContracts()) {
        Log("CustomModel PoC-1 contract resolution failed.");
        return BE_Result_ContractMismatch;
    }
    if (!InstallPumpHook()) {
        Log("CustomModel PoC-1 could not install a main-thread pump.");
        return BE_Result_Conflict;
    }
    if (!StartHotkeyThread()) {
        g_host->release_module_hooks(g_host->context, kModuleId);
        g_original_pump = nullptr;
        return BE_Result_Failed;
    }

    const std::filesystem::path path = BemPath();
    Log("BetterEndfield.CustomModel PoC-1 ready. Load Endministrator (F), then press F9 to toggle Component9 replacement.");
    if (!path.empty()) Log("Expected BEM PoC path: " + path.string());
    return BE_Result_Ok;
}

BE_Result BE_CALL ConfigurationChanged(const char*) {
    return BE_Result_Ok;
}

void BE_CALL Shutdown() {
    StopHotkeyThread();
    if (g_host && g_host->release_module_hooks) {
        g_host->release_module_hooks(g_host->context, kModuleId);
    }
    // Shutdown can run off the Unity main thread. Do not invoke Unity APIs here;
    // the process-lifetime Host normally exits with the game anyway.
    ReleaseReplacementHandles();
    g_replaced_renderer = nullptr;
    g_original_mesh = nullptr;
    g_custom_mesh = nullptr;
    g_original_pump = nullptr;
    g_toggle_requested.store(false, std::memory_order_release);
    g_operation_in_progress.store(false, std::memory_order_release);
    g_pump_hits.store(0, std::memory_order_release);
    g_host = nullptr;
}

const BE_ModuleApiV1 kApi{
    {kModuleId, "Custom Model (BEM PoC-1)", "0.0.3-poc1",
        BETTER_ENDFIELD_MODULE_ABI_V1},
    &Initialize,
    &ConfigurationChanged,
    &Shutdown};

} // namespace
} // namespace BetterEndfield::CustomModelModule

BE_EXPORT const BE_ModuleApiV1* BE_CALL BetterEndfield_GetModuleApiV1() {
    return &BetterEndfield::CustomModelModule::kApi;
}
