#include "BetterEndfield/ModuleApi.h"

#include <Windows.h>

#include <algorithm>
#include <array>
#include <atomic>
#include <cctype>
#include <cstdint>
#include <cstdio>
#include <cstring>
#include <string>
#include <string_view>
#include <vector>

namespace BetterEndfield::CustomModelModule {
namespace {

constexpr char kModuleId[] = "betterendfield.custom_model";
constexpr char kTargetHint[] = "endminf";
constexpr int kDumpHotkey = VK_F8;
constexpr int kMaxRenderers = 4096;
constexpr int kMaxBones = 1024;
constexpr int kMaxMaterials = 128;
constexpr int kMaxSubMeshes = 256;

struct EfmiComponentSignature {
    int component;
    uint32_t index_count;
};

// Endmin Casualwear reference sample. These counts are diagnostics only: they
// annotate exact runtime submesh matches and are never used as an identity or
// replacement key.
constexpr std::array<EfmiComponentSignature, 11> kSampleEfmiComponents{{
    {0, 27615}, {1, 9000}, {2, 4524}, {3, 20577}, {4, 1638}, {5, 16524},
    {6, 117}, {7, 1386}, {8, 90}, {9, 101994}, {10, 2286},
}};

struct MethodContract {
    const char* key;
    BE_MethodDescriptorV1 descriptor;
    bool required;
    void* pointer = nullptr;
    const void* method_info = nullptr;
    bool resolved = false;
};

const BE_HostApiV1* g_host = nullptr;
BE_ResolvedClassV1 g_skinned_renderer_class{};
std::atomic_bool g_dump_in_progress{false};
bool g_hotkey_was_down = false;

using VoidInstanceFn = void(__fastcall*)(void* instance, void* method_info);
VoidInstanceFn g_original_pump = nullptr;

MethodContract g_methods[]{
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
    {"object.get_instance_id",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Object", "GetInstanceID",
            nullptr, "System.Int32", 0}, false},
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
    {"renderer.get_shared_materials",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Renderer",
            "get_sharedMaterials", nullptr, "UnityEngine.Material[]", 0}, true},
    {"renderer.get_enabled",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Renderer", "get_enabled",
            nullptr, "System.Boolean", 0}, false},
    {"skinned.get_shared_mesh",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer",
            "get_sharedMesh", nullptr, "UnityEngine.Mesh", 0}, true},
    {"skinned.get_bones",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer",
            "get_bones", nullptr, "UnityEngine.Transform[]", 0}, true},
    {"skinned.get_root_bone",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer",
            "get_rootBone", nullptr, "UnityEngine.Transform", 0}, true},
    {"mesh.get_vertex_count",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "get_vertexCount",
            nullptr, "System.Int32", 0}, true},
    {"mesh.get_sub_mesh_count",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "get_subMeshCount",
            nullptr, "System.Int32", 0}, true},
    {"mesh.get_index_count",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "GetIndexCount",
            "System.Int32", "System.UInt32", 1}, true},
    {"mesh.get_base_vertex",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "GetBaseVertex",
            "System.Int32", "System.UInt32", 1}, false},
    {"mesh.get_bindposes",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "get_bindposes",
            nullptr, "UnityEngine.Matrix4x4[]", 0}, true},
    {"mesh.get_index_format",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "get_indexFormat",
            nullptr, "UnityEngine.Rendering.IndexFormat", 0}, false},
    {"mesh.get_is_readable",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "get_isReadable",
            nullptr, "System.Boolean", 0}, false},
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

std::string ManagedString(void* value) {
    if (!value || !g_host || !g_host->copy_managed_string) return {};
    std::array<char, 1024> buffer{};
    const int copied = g_host->copy_managed_string(
        g_host->context, value, buffer.data(), buffer.size());
    return copied > 0 ? std::string(buffer.data(), static_cast<size_t>(copied))
                      : std::string{};
}

std::string ObjectName(void* object) {
    return object ? ManagedString(Invoke(Contract("object.get_name"), object, nullptr))
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
    if (value.empty()) return false;
    return LowerAscii(std::string(value)).find(kTargetHint) != std::string::npos;
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

struct RendererSummary {
    void* renderer = nullptr;
    void* mesh = nullptr;
    std::string renderer_name;
    std::string mesh_name;
    std::string path;
    std::string root_bone_name;
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
    return summary;
}

bool IsTargetRenderer(const RendererSummary& summary) {
    return ContainsTargetHint(summary.renderer_name) ||
        ContainsTargetHint(summary.mesh_name) ||
        ContainsTargetHint(summary.path) ||
        ContainsTargetHint(summary.root_bone_name);
}

void DumpMaterials(void* renderer) {
    void* materials = Invoke(Contract("renderer.get_shared_materials"), renderer, nullptr);
    const int count = std::min(ArrayLength(materials), kMaxMaterials);
    Log("    materials=" + std::to_string(count));
    for (int index = 0; index < count; ++index) {
        void* material = ArrayValue(materials, index);
        Log("      material[" + std::to_string(index) + "] name=\"" +
            ObjectName(material) + "\"");
    }
}

void DumpBones(void* renderer) {
    void* bones = Invoke(Contract("skinned.get_bones"), renderer, nullptr);
    const int raw_count = ArrayLength(bones);
    const int count = std::min(raw_count, kMaxBones);
    void* root_bone = Invoke(Contract("skinned.get_root_bone"), renderer, nullptr);
    Log("    bones=" + std::to_string(raw_count) +
        " rootBone=\"" + ObjectName(root_bone) + "\"");
    for (int index = 0; index < count; ++index) {
        void* bone = ArrayValue(bones, index);
        Log("      bone[" + std::to_string(index) + "] name=\"" +
            ObjectName(bone) + "\"");
    }
    if (raw_count > count) {
        Log("      ... bone output truncated at " + std::to_string(count));
    }
}

void DumpMesh(void* mesh) {
    if (!mesh) {
        Log("    mesh=<null>");
        return;
    }

    int vertex_count = -1;
    int sub_mesh_count = -1;
    int index_format = -1;
    bool readable = false;
    InvokeValue(Contract("mesh.get_vertex_count"), mesh, nullptr, vertex_count);
    InvokeValue(Contract("mesh.get_sub_mesh_count"), mesh, nullptr, sub_mesh_count);
    if (auto* method = Contract("mesh.get_index_format"); method && method->resolved) {
        InvokeValue(method, mesh, nullptr, index_format);
    }
    bool has_readable = false;
    if (auto* method = Contract("mesh.get_is_readable"); method && method->resolved) {
        has_readable = InvokeValue(method, mesh, nullptr, readable);
    }

    void* bindposes = Invoke(Contract("mesh.get_bindposes"), mesh, nullptr);
    const int bindpose_count = ArrayLength(bindposes);

    std::string header = "    mesh name=\"" + ObjectName(mesh) +
        "\" vertexCount=" + std::to_string(vertex_count) +
        " subMeshCount=" + std::to_string(sub_mesh_count) +
        " bindposes=" + std::to_string(bindpose_count);
    if (index_format >= 0) {
        header += " indexFormat=" + std::to_string(index_format) +
            (index_format == 0 ? "(UInt16)" : index_format == 1 ? "(UInt32)" : "");
    }
    if (has_readable) {
        header += std::string(" isReadable=") + (readable ? "true" : "false");
    }
    Log(header);

    if (sub_mesh_count < 0) return;
    const int count = std::min(sub_mesh_count, kMaxSubMeshes);
    uint64_t total_indices = 0;
    for (int submesh = 0; submesh < count; ++submesh) {
        uint32_t index_count = 0;
        uint32_t base_vertex = 0;
        void* parameters[1]{&submesh};
        const bool got_count = InvokeValue(
            Contract("mesh.get_index_count"), mesh, parameters, index_count);
        bool got_base_vertex = false;
        if (auto* method = Contract("mesh.get_base_vertex");
            method && method->resolved) {
            got_base_vertex = InvokeValue(method, mesh, parameters, base_vertex);
        }
        if (got_count) total_indices += index_count;

        std::string line = "      subMesh[" + std::to_string(submesh) + "]";
        line += got_count ? " indexCount=" + std::to_string(index_count)
                          : " indexCount=<unavailable>";
        if (got_base_vertex) line += " baseVertex=" + std::to_string(base_vertex);
        if (got_count) {
            const std::string match = EfmiComponentMatch(index_count);
            if (!match.empty()) line += " EFMI_EXACT=" + match;
        }
        Log(line);
    }
    Log("    totalSubMeshIndices=" + std::to_string(total_indices));
}

void DumpRenderer(const RendererSummary& summary, int ordinal) {
    int instance_id = 0;
    bool got_instance_id = false;
    if (auto* method = Contract("object.get_instance_id"); method && method->resolved) {
        got_instance_id = InvokeValue(method, summary.renderer, nullptr, instance_id);
    }
    bool enabled = false;
    bool got_enabled = false;
    if (auto* method = Contract("renderer.get_enabled"); method && method->resolved) {
        got_enabled = InvokeValue(method, summary.renderer, nullptr, enabled);
    }

    std::string line = "  renderer[" + std::to_string(ordinal) + "] name=\"" +
        summary.renderer_name + "\"";
    if (got_instance_id) line += " instanceId=" + std::to_string(instance_id);
    if (got_enabled) line += std::string(" enabled=") + (enabled ? "true" : "false");
    Log(line);
    Log("    path=\"" + summary.path + "\"");
    Log("    rootBone=\"" + summary.root_bone_name + "\"");
    DumpMesh(summary.mesh);
    DumpMaterials(summary.renderer);
    DumpBones(summary.renderer);
}

void DumpFallbackSummaries(const std::vector<RendererSummary>& renderers) {
    Log("No renderer matched target hint 'endminf'; dumping renderer summaries only.");
    const int count = std::min(static_cast<int>(renderers.size()), 1024);
    for (int index = 0; index < count; ++index) {
        const auto& item = renderers[index];
        Log("  summary[" + std::to_string(index) + "] renderer=\"" +
            item.renderer_name + "\" mesh=\"" + item.mesh_name +
            "\" root=\"" + item.root_bone_name + "\" path=\"" + item.path + "\"");
    }
    if (static_cast<int>(renderers.size()) > count) {
        Log("  ... summary output truncated at " + std::to_string(count));
    }
}

void DumpTargetRenderers() {
    if (g_dump_in_progress.exchange(true, std::memory_order_acq_rel)) return;

    Log("=== CustomModel probe dump begin; target_hint=endminf; sample=Endmin Casualwear ===");

    void* type_object = g_skinned_renderer_class.type_object;
    void* parameters[1]{type_object};
    void* objects = Invoke(Contract("resources.find_objects_of_type_all"), nullptr, parameters);
    const int raw_count = ArrayLength(objects);
    const int count = std::min(raw_count, kMaxRenderers);
    Log("SkinnedMeshRenderer objects discovered=" + std::to_string(raw_count));

    std::vector<RendererSummary> summaries;
    summaries.reserve(static_cast<size_t>(std::max(count, 0)));
    std::vector<size_t> target_indices;

    for (int index = 0; index < count; ++index) {
        void* renderer = ArrayValue(objects, index);
        if (!renderer) continue;
        RendererSummary summary = SummarizeRenderer(renderer);
        summaries.push_back(std::move(summary));
        if (IsTargetRenderer(summaries.back())) {
            target_indices.push_back(summaries.size() - 1);
        }
    }

    Log("Target renderer candidates=" + std::to_string(target_indices.size()));
    if (target_indices.empty()) {
        DumpFallbackSummaries(summaries);
    } else {
        int ordinal = 0;
        for (size_t index : target_indices) {
            DumpRenderer(summaries[index], ordinal++);
        }
    }

    Log("=== CustomModel probe dump end ===");
    g_dump_in_progress.store(false, std::memory_order_release);
}

void PumpProbe() {
    const bool down = (GetAsyncKeyState(kDumpHotkey) & 0x8000) != 0;
    const bool pressed = down && !g_hotkey_was_down;
    g_hotkey_was_down = down;
    if (pressed) DumpTargetRenderers();
}

void __fastcall DetourPump(void* instance, void* method_info) {
    if (g_original_pump) g_original_pump(instance, method_info);
    PumpProbe();
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
            Log(std::string("Resolved method contract: ") + method.key);
        } else {
            Log(std::string(method.required ? "Required" : "Optional") +
                " method not found: " + method.key);
            if (method.required) required_ready = false;
        }
    }

    if (g_host->resolve_class(g_host->context,
            "UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer",
            &g_skinned_renderer_class) != BE_Result_Ok ||
        !g_skinned_renderer_class.type_object) {
        Log("Required class contract not found: UnityEngine.SkinnedMeshRenderer");
        required_ready = false;
    } else {
        Log("Resolved class contract: UnityEngine.SkinnedMeshRenderer");
    }
    return required_ready;
}

bool InstallPumpHook() {
    constexpr std::array<std::string_view, 2> candidates{
        "pump.evaluate_touched_entities", "pump.process_dither_trace"};
    for (std::string_view key : candidates) {
        MethodContract* method = Contract(key);
        if (!method || !method->resolved || !method->pointer) continue;
        void* original = nullptr;
        const BE_Result result = g_host->create_hook(
            g_host->context, kModuleId, method->pointer,
            reinterpret_cast<void*>(&DetourPump), &original);
        if (result == BE_Result_Ok) {
            g_original_pump = reinterpret_cast<VoidInstanceFn>(original);
            Log(std::string("Probe main-thread pump installed: ") + method->key);
            return true;
        }
        Log(std::string("Probe pump unavailable: ") + method->key +
            " result=" + std::to_string(static_cast<int>(result)));
    }
    return false;
}

BE_Result BE_CALL Initialize(const BE_HostApiV1* host) {
    if (!host || host->abi_version != BETTER_ENDFIELD_MODULE_ABI_V1 ||
        !host->resolve_method || !host->resolve_class || !host->runtime_invoke ||
        !host->object_unbox || !host->copy_managed_string || !host->create_hook ||
        !host->release_module_hooks || !host->log) {
        return BE_Result_InvalidArgument;
    }
    g_host = host;
    if (!ResolveContracts()) {
        Log("CustomModel probe contract resolution failed.");
        return BE_Result_ContractMismatch;
    }
    if (!InstallPumpHook()) {
        Log("CustomModel probe could not install a conflict-free main-thread pump.");
        return BE_Result_Conflict;
    }
    Log("BetterEndfield.CustomModel probe ready. Load Endministrator (F), then press F8 once to dump mesh/skeleton data.");
    return BE_Result_Ok;
}

BE_Result BE_CALL ConfigurationChanged(const char*) {
    return BE_Result_Ok;
}

void BE_CALL Shutdown() {
    if (g_host && g_host->release_module_hooks) {
        g_host->release_module_hooks(g_host->context, kModuleId);
    }
    g_original_pump = nullptr;
    g_hotkey_was_down = false;
    g_host = nullptr;
}

const BE_ModuleApiV1 kApi{
    {kModuleId, "Custom Model (Research Probe)", "0.0.1-probe",
        BETTER_ENDFIELD_MODULE_ABI_V1},
    &Initialize,
    &ConfigurationChanged,
    &Shutdown};

} // namespace
} // namespace BetterEndfield::CustomModelModule

BE_EXPORT const BE_ModuleApiV1* BE_CALL BetterEndfield_GetModuleApiV1() {
    return &BetterEndfield::CustomModelModule::kApi;
}
