#include "custom_model_module.h"
#include "android_mesh_builder.h"
#include "core/log.h"
#include "core/command_pump.h"

#include <cstdlib>
#include <cstdio>
#include <cstring>
#include <string_view>
#include <array>
#include <set>
#include <vector>
#include <fstream>
#include <dlfcn.h>
#include <sys/syscall.h>
#include <unistd.h>

namespace betterendfield {
using namespace BetterEndfield::CustomModel;
extern "C" const BE_ModuleApiV1* BetterEndfield_GetCustomModelModuleApiV1();
CustomModelModule* CustomModelModule::instance_ = nullptr;
CustomModelModule::FinishFn CustomModelModule::original_ = nullptr;
CustomModelModule::CopyFn CustomModelModule::copy_original_ = nullptr;
CustomModelModule::CreateFn CustomModelModule::create_original_ = nullptr;
std::atomic<uint32_t> g_copy_probe_count{0};
std::atomic<uint32_t> g_create_probe_count{0};

namespace {
std::string ConfigValue(std::string_view config, std::string_view key) {
    while (!config.empty()) {
        const size_t end = config.find_first_of("\n;");
        std::string_view line = config.substr(0, end);
        if (line.size()>key.size() && line.starts_with(key) && line[key.size()] == '=') return std::string(line.substr(key.size() + 1));
        if (end == std::string_view::npos) break;
        config.remove_prefix(end + 1);
    }
    return {};
}
std::vector<std::filesystem::path> ConfigPackages(std::string_view value) {
    std::vector<std::filesystem::path> result;
    while (!value.empty()) {
        const size_t comma = value.find(',');
        auto item = value.substr(0, comma);
        if (!item.empty()) result.push_back(std::filesystem::u8path(std::string(item)));
        if (comma == std::string_view::npos) break;
        value.remove_prefix(comma + 1);
    }
    return result;
}
std::vector<std::string> ConfigStrings(std::string_view value) {
    std::vector<std::string> result;
    while (!value.empty()) {
        const size_t comma = value.find(',');
        auto item = value.substr(0, comma);
        if (!item.empty()) result.emplace_back(item);
        if (comma == std::string_view::npos) break;
        value.remove_prefix(comma + 1);
    }
    return result;
}
}

CustomModelModule* CustomModelModule::HostSelf(void* context) {
    return static_cast<CustomModelModule*>(context);
}

void CustomModelModule::HostLog(void* context, const char* module_id, const char* message) {
    auto* self = HostSelf(context);
    LogInfo(module_id && *module_id ? module_id : (self ? self->Id() : "custom_model"),
        message ? message : "");
}

BE_Result CustomModelModule::HostResolveMethod(void* context,
        const BE_MethodDescriptorV1* descriptor, BE_ResolvedMethodV1* result) {
    auto* self = HostSelf(context);
    if (!self || !self->runtime_ || !descriptor || !result) return BE_Result_InvalidArgument;
    const auto method = self->runtime_->ResolveMethodExact(
        descriptor->assembly_name, descriptor->namespace_name, descriptor->class_name,
        descriptor->method_name, descriptor->parameter_types, descriptor->return_type,
        static_cast<int>(descriptor->parameter_count));
    if (!method.info || !method.entry) { *result = {}; return BE_Result_NotFound; }
    result->method_info = method.info; result->method_pointer = method.entry;
    return BE_Result_Ok;
}

BE_Result CustomModelModule::HostResolveField(void* context,
        const BE_FieldDescriptorV1* descriptor, BE_ResolvedFieldV1* result) {
    auto* self = HostSelf(context);
    if (!self || !self->runtime_ || !descriptor || !result) return BE_Result_InvalidArgument;
    const auto field = self->runtime_->ResolveField(
        descriptor->assembly_name, descriptor->namespace_name,
        descriptor->class_name, descriptor->field_name);
    if (!field.info || field.offset < 0) { *result = {}; return BE_Result_NotFound; }
    result->field_info = field.info; result->offset = field.offset;
    return BE_Result_Ok;
}

BE_Result CustomModelModule::HostCreateHook(void* context, const char* module_id,
        void* target, void* detour, void** original) {
    auto* self = HostSelf(context);
    if (!self || !target || !detour || !original) return BE_Result_InvalidArgument;
    void* stub = nullptr; std::string error;
    if (!self->replacement_broker_.Install(target, detour, original, stub, error)) {
        LogError(self->Id(), error.c_str()); return BE_Result_Failed;
    }
    self->replacement_hooks_.push_back({module_id ? module_id : self->Id(), stub});
    return BE_Result_Ok;
}

BE_Result CustomModelModule::RetireSharedHooks(const char* module_id) {
    const std::string wanted = module_id ? module_id : "";
    for (auto& hook : replacement_hooks_) {
        if (wanted.empty() || hook.module_id == wanted)
            replacement_broker_.Remove(hook.stub);
    }
    replacement_hooks_.erase(std::remove_if(replacement_hooks_.begin(),
        replacement_hooks_.end(), [](const HookRecord& hook) { return hook.stub == nullptr; }),
        replacement_hooks_.end());
    return BE_Result_Ok;
}

BE_Result CustomModelModule::HostReleaseHooks(void* context, const char* module_id) {
    auto* self = HostSelf(context);
    return self ? self->RetireSharedHooks(module_id) : BE_Result_InvalidArgument;
}

int CustomModelModule::HostCopyConfiguration(void* context, const char* module_id,
        char* destination, size_t destination_size) {
    auto* self = HostSelf(context);
    if (!self || !destination || !destination_size ||
            (module_id && std::strcmp(module_id, self->Id()) != 0)) return 0;
    const size_t count = std::min(destination_size - 1, self->replacement_config_.size());
    std::memcpy(destination, self->replacement_config_.data(), count);
    destination[count] = '\0'; return static_cast<int>(count);
}

int CustomModelModule::HostCopyCatalogRoot(void* context, char* destination, size_t destination_size) {
    auto* self = HostSelf(context);
    if (!self || !destination || !destination_size) return 0;
    const std::string root = self->replacement_root_.string();
    const size_t count = std::min(destination_size - 1, root.size());
    std::memcpy(destination, root.data(), count); destination[count] = '\0';
    return static_cast<int>(count);
}

int CustomModelModule::HostCopyManagedString(void* context, const void* managed,
        char* destination, size_t destination_size) {
    auto* self = HostSelf(context);
    if (!self || !self->runtime_ || !destination || !destination_size) return 0;
    const auto value = self->runtime_->CopyString(const_cast<void*>(managed));
    const size_t count = std::min(destination_size - 1, value.size());
    std::memcpy(destination, value.data(), count); destination[count] = '\0';
    return static_cast<int>(count);
}

BE_Result CustomModelModule::HostResolveClass(void* context, const char* assembly,
        const char* ns, const char* name, BE_ResolvedClassV1* result) {
    auto* self = HostSelf(context);
    if (!self || !self->runtime_ || !result) return BE_Result_InvalidArgument;
    const auto klass = self->runtime_->ResolveClass(assembly, ns, name);
    if (!klass.info || !klass.type_object) { *result = {}; return BE_Result_NotFound; }
    result->class_info = klass.info; result->type_info = klass.type;
    result->type_object = klass.type_object; return BE_Result_Ok;
}

void* CustomModelModule::HostObjectNew(void* context, const void* klass) {
    auto* self = HostSelf(context);
    return self && self->runtime_ && klass
        ? self->runtime_->NewObject(reinterpret_cast<const Il2CppClass*>(klass)) : nullptr;
}
void* CustomModelModule::HostStringNew(void* context, const char* value) {
    auto* self = HostSelf(context);
    return self && self->runtime_ ? self->runtime_->NewString(value ? value : "") : nullptr;
}
void* CustomModelModule::HostRuntimeInvoke(void* context, const void* method,
        void* instance, void** parameters, void** exception) {
    auto* self = HostSelf(context);
    return self && self->runtime_ && method
        ? self->runtime_->Invoke(reinterpret_cast<const MethodInfo*>(method),
            instance, parameters, exception) : nullptr;
}
void* CustomModelModule::HostObjectUnbox(void* context, void* boxed) {
    auto* self = HostSelf(context);
    return self && self->runtime_ ? self->runtime_->Unbox(boxed) : nullptr;
}
uint32_t CustomModelModule::HostGcHandleNew(void* context, void* object, int pinned) {
    auto* self = HostSelf(context);
    return self && self->runtime_ ? self->runtime_->NewGcHandle(object, pinned != 0) : 0;
}
void CustomModelModule::HostGcHandleFree(void* context, uint32_t handle) {
    auto* self = HostSelf(context);
    if (self && self->runtime_) self->runtime_->FreeGcHandle(handle);
}
void* CustomModelModule::HostFieldGetValueObject(void* context, const void* field, void* instance) {
    auto* self = HostSelf(context);
    return self && self->runtime_ && field
        ? self->runtime_->ReadFieldObject(reinterpret_cast<const FieldInfo*>(field), instance) : nullptr;
}

bool CustomModelModule::InitializeSharedReplacement(const std::string& config) {
    // /data/local/tmp is readable by the game for the pushed BEM package, but
    // the game UID cannot create a new directory there.  Android supplies the
    // complete registry through copy_module_configuration instead.
    replacement_root_ = std::filesystem::path("/data/local/tmp");
    if (ConfigValue(config,"capture") == "1") {
        const char* diagnostics = std::getenv("BETTER_ENDFIELD_DIAGNOSTICS_PATH");
        if (!diagnostics || !*diagnostics) return false;
        replacement_root_ = std::filesystem::path(diagnostics).parent_path() / "custom-model-probe";
        std::filesystem::create_directories(replacement_root_/"custom-model");
        std::ofstream request(replacement_root_/"custom-model"/"native-probe.request",std::ios::trunc);
        request << "BE_NATIVE_PROBE_SWEEP_V1\nandroid-" << getpid() << "\nandroid-runtime\n\n"
            << world_resource_ << '\n' << ui_resource_ << '\n';
        if (!request) return false;
    }
    replacement_config_ = "[CustomModel]\nstandalone_lod=false\n";
    const auto configured_appearances=ConfigStrings(ConfigValue(config,"appearances"));
    for (size_t i = 0; i < package_paths_.size(); ++i) {
        replacement_config_ += "[Mod.android" + std::to_string(i) + "]\nenabled=true\npackage=" +
            package_paths_[i].string() + "\n";
        const std::string selected = i < configured_appearances.size() ? configured_appearances[i] :
            (i == 0 ? appearance_ : std::string{});
        if (!selected.empty()) replacement_config_ += "appearance=" + selected + "\n";
    }
    host_ = {};
    host_.abi_version = BETTER_ENDFIELD_MODULE_ABI_V1; host_.context = this;
    host_.log = &HostLog; host_.resolve_method = &HostResolveMethod;
    host_.resolve_field = &HostResolveField; host_.create_hook = &HostCreateHook;
    host_.release_module_hooks = &HostReleaseHooks;
    host_.copy_module_configuration = &HostCopyConfiguration;
    host_.copy_catalog_root = &HostCopyCatalogRoot;
    host_.copy_managed_string = &HostCopyManagedString;
    host_.resolve_class = &HostResolveClass; host_.object_new = &HostObjectNew;
    host_.string_new = &HostStringNew; host_.runtime_invoke = &HostRuntimeInvoke;
    host_.object_unbox = &HostObjectUnbox; host_.gchandle_new = &HostGcHandleNew;
    host_.gchandle_free = &HostGcHandleFree; host_.field_get_value_object = &HostFieldGetValueObject;
    std::string hook_error;
    if (!replacement_broker_.Initialize(hook_error)) { LogError(Id(), hook_error.c_str()); return false; }
    replacement_api_ = BetterEndfield_GetCustomModelModuleApiV1();
    if (!replacement_api_ || !replacement_api_->initialize ||
            replacement_api_->initialize(&host_) != BE_Result_Ok) {
        LogError(Id(), "shared custom model initialization failed"); return false;
    }
    replacement_active_ = true;
    LogInfo(Id(), "shared PC replacement transaction active on Android");
    return true;
}

namespace {
void ConsumeModelCommand() {
    std::string payload;
    if (!ConsumeRuntimeCommand(payload)) return;
    const size_t first = payload.find('\n', 14);
    const size_t second = first == std::string::npos ? std::string::npos : payload.find('\n', first + 1);
    const std::string command = first == std::string::npos || second == std::string::npos
        ? std::string{} : payload.substr(first + 1, second - first - 1);
    const char* result = command == "overlay_hide" ? "applied" : "unsupported";
    AcknowledgeRuntimeCommand(result);
    LogInfo("command_pump", ("Unity-thread command " + std::string(result) + ": " + command).c_str());
}

std::string NativeAddress(void* address) {
    if (!address) return "unresolved";
    Dl_info info{};
    if (!dladdr(address, &info) || !info.dli_fname || !info.dli_fbase) return "outside-loaded-ELF";
    const auto offset = reinterpret_cast<uintptr_t>(address) - reinterpret_cast<uintptr_t>(info.dli_fbase);
    char text[192];
    std::snprintf(text, sizeof(text), "%s+0x%zx", info.dli_fname, static_cast<size_t>(offset));
    return text;
}

void AuditMeshMethodAddresses(Il2CppRuntime& runtime) {
    struct Candidate { const char* name; int parameters; };
    constexpr Candidate methods[] = {
        {"Internal_Create", 1}, {"ClearImpl", 1}, {"UploadMeshDataImpl", 1},
        {"GetBonesPerVertexValue", 0}, {"get_boneWeightCount", 0},
        {"InternalSetBoneWeights", 7}, {"SetBoneWeights", 7}, {"SetBoneWeightsImpl", 7}
    };
    for (const auto& candidate : methods) {
        const auto resolved = runtime.ResolveMethod("UnityEngine.CoreModule.dll", "UnityEngine",
            "Mesh", candidate.name, candidate.parameters);
        LogInfo("custom_model", (std::string("Mesh method ") + candidate.name + " => " +
            NativeAddress(resolved.entry)).c_str());
    }
    constexpr const char* icalls[] = {
        "UnityEngine.Mesh::Internal_Create", "UnityEngine.Mesh::ClearImpl",
        "UnityEngine.Mesh::UploadMeshDataImpl", "UnityEngine.Mesh::GetBonesPerVertexValue",
        "UnityEngine.Mesh::InternalSetBoneWeights", "UnityEngine.Mesh::SetBoneWeights"
    };
    for (const char* name : icalls) {
        LogInfo("custom_model", (std::string("Mesh icall ") + name + " => " +
            NativeAddress(runtime.ResolveIcall(name))).c_str());
    }
    constexpr const char* mesh_data[] = {
        "SetVertexBufferParamsFromArray", "SetIndexBufferParamsImpl",
        "SetSubMeshCount", "SetSubMeshImpl_Injected", "GetVertexDataPtr",
        "GetVertexDataSize", "GetIndexDataPtr", "GetIndexDataSize"
    };
    for (const char* name : mesh_data) {
        const std::string description = runtime.DescribeMethod(
            "UnityEngine.CoreModule.dll", "UnityEngine", "Mesh.MeshData", name);
        LogInfo("custom_model", (std::string("MeshData method ") + name +
            " => " + description).c_str());
    }
    constexpr const char* mesh_data_array[] = {
        "CreateNewMeshDatas", "ApplyToMeshImpl", "ReleaseMeshDatas", ".ctor", "Dispose", "get_Item"
    };
    for (const char* name : mesh_data_array) {
        const std::string description = runtime.DescribeMethod(
            "UnityEngine.CoreModule.dll", "UnityEngine", "Mesh.MeshDataArray", name);
        LogInfo("custom_model", (std::string("MeshDataArray method ") + name +
            " => " + description).c_str());
    }
    for (const char* name : {"AllocateWritableMeshData", "ApplyAndDisposeWritableMeshData"})
        LogInfo("custom_model", runtime.DescribeMethod(
            "UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", name).c_str());
    // Name lookup only; none of the returned functions is invoked here.
    for (const char* name : mesh_data) {
        const auto full = std::string("UnityEngine.Mesh/MeshData::") + name;
        LogInfo("custom_model", (full + " => " + NativeAddress(runtime.ResolveIcall(full.c_str()))).c_str());
    }
    for (const char* name : {"CreateNewMeshDatas", "ApplyToMeshImpl", "ReleaseMeshDatas"}) {
        const auto full = std::string("UnityEngine.Mesh/MeshDataArray::") + name;
        LogInfo("custom_model", (full + " => " + NativeAddress(runtime.ResolveIcall(full.c_str()))).c_str());
    }
}

// Capture only bounded executable windows reachable from named Mesh icalls.
// Disk code differs from loaded code in this player; offline bytes alone are
// insufficient to determine the native object path.
void AuditLoadedMeshCode(Il2CppRuntime& runtime) {
    std::vector<std::pair<std::string,void*>> roots;
    for (const char* name : {"UnityEngine.Mesh::Internal_Create","UnityEngine.Mesh::GetBonesPerVertexValue"})
        roots.emplace_back(name,runtime.ResolveIcall(name));
    for (const char* name : {"EnableForceLOD0","DisableForceLOD0","RegisterArtTagLODBias"}) {
        auto method=runtime.ResolveMethod("HG.RenderPipelines.Runtime.dll","HG.Rendering.Runtime","HGRenderPipeline",name,0);
        LogInfo("custom_model",(std::string("pipeline method ")+name+" => "+runtime.DescribeMethod(
            "HG.RenderPipelines.Runtime.dll","HG.Rendering.Runtime","HGRenderPipeline",name)).c_str());
        roots.emplace_back(std::string("HGRenderPipeline.")+name,method.entry);
    }
    for (const auto& [label,root] : roots) {
        const char* name=label.c_str();
        Dl_info owner{};
        if (!root || !dladdr(root, &owner)) continue;
        const auto base = reinterpret_cast<uintptr_t>(owner.dli_fbase);
        std::vector<std::pair<uintptr_t, unsigned>> queue{{reinterpret_cast<uintptr_t>(root), 0}};
        std::set<uintptr_t> seen;
        for (size_t n = 0; n < queue.size() && seen.size() < 12; ++n) {
            const auto [address, depth] = queue[n];
            Dl_info current{};
            if (!seen.insert(address).second || !dladdr(reinterpret_cast<void*>(address), &current) ||
                    current.dli_fbase != owner.dli_fbase) continue;
            std::array<uint8_t, 384> bytes{};
            if (!ReadOwnMemory(address, bytes.data(), bytes.size())) continue;
            char prefix[160];
            std::snprintf(prefix, sizeof(prefix), "mesh-code root=%s offset=0x%zx bytes=", name, address-base);
            std::string line(prefix);
            constexpr char digits[] = "0123456789abcdef";
            for (uint8_t b : bytes) { line += digits[b >> 4]; line += digits[b & 15]; }
            LogInfo("custom_model", line.c_str());
            if (depth >= 2) continue;
            for (size_t at = 0; at < bytes.size(); at += 4) {
                uint32_t word; std::memcpy(&word, bytes.data()+at, 4);
                if (word == 0xd65f03c0) break;
                if ((word & 0x7c000000) != 0x14000000) continue;
                int64_t delta = word & 0x03ffffff;
                if (delta & 0x02000000) delta -= 0x04000000;
                queue.emplace_back(static_cast<uintptr_t>(address+at+delta*4), depth+1);
                if ((word & 0xfc000000) == 0x14000000) break;
            }
        }
    }
}
}

ModuleResult CustomModelModule::Start(Il2CppRuntime& runtime) {
    const char* raw = std::getenv("BETTER_ENDFIELD_CUSTOM_MODEL_CONFIG");
    if (!raw || *raw == '\0') return {false, "no Android BEM package configured"};
    const std::string_view config(raw);
    if (ConfigValue(config, "api_audit") == "1") {
        LogInfo(Id(), "Android Mesh API audit v2: metadata and icall lookup only; no resource hook or Mesh creation");
        AuditMeshMethodAddresses(runtime);
        return {true, "Android Mesh API audit v2 complete; no geometry writes"};
    }
    resource_name_ = ConfigValue(config, "resource");
    const auto configured_packages = ConfigPackages(ConfigValue(config, "packages"));
    package_paths_ = configured_packages;
    if (package_paths_.empty()) {
        const auto single = ConfigValue(config, "package");
        if (!single.empty()) package_paths_.push_back(std::filesystem::u8path(single));
    }
    package_path_ = package_paths_.empty() ? std::filesystem::path{} : package_paths_.front();
    for (auto& path : package_paths_) if (path.is_relative()) path = std::filesystem::path("/data/local/tmp") / path;
    package_path_ = package_paths_.empty() ? std::filesystem::path{} : package_paths_.front();
    appearance_ = ConfigValue(config, "appearance");
    const auto configured_appearances=ConfigStrings(ConfigValue(config,"appearances"));
    if (!configured_appearances.empty()) appearance_=configured_appearances.front();
    if (resource_name_.empty() || package_paths_.empty()) return {false, "custom model config needs resource and package"};
    std::string error;
    BemPackageInfo info;
    if (!ReadBemPackageInfo(package_path_, info, error)) return {false, "BEM metadata rejected: " + error};
    world_resource_=info.world_resource; ui_resource_=info.ui_resource;
    detached_probe_=ConfigValue(config,"mesh_probe")=="1";
    LogInfo(Id(), ("BEM target world="+world_resource_+" ui="+ui_resource_).c_str());
    if (appearance_.empty()) appearance_ = info.default_appearance;
    if (!LoadBem(package_path_, package_, error, appearance_)) return {false, "BEM package rejected: " + error};
    if (package_.components.empty()) return {false, "BEM package has no components"};
    runtime_ = &runtime;
    ConfigureAndroidMeshBuilder(runtime, ConfigValue(config, "rollback") == "1", ConfigValue(config,"lod_pipeline")=="1",
        ConfigValue(config,"lod_npc")=="1",ConfigValue(config,"inspect")=="1");
    if (ConfigValue(config,"lod_audit")=="1") AuditLoadedMeshCode(runtime);
    const std::string mesh_data_mode = ConfigValue(config, "mesh_data_probe");
    mesh_data_probe_ = mesh_data_mode == "1" || mesh_data_mode == "2";
    mesh_data_write_probe_ = mesh_data_mode == "2";
    if (ConfigValue(config, "replace") == "1") {
        const bool ready = InitializeSharedReplacement(std::string(config));
        return {ready, ready ? "Android MeshData replacement transaction ready" :
            "Android replacement contracts unavailable; original resources retained"};
    }

    auto delivery = runtime.ResolveMethodExact("Common.Beyond.dll", "Beyond.Resource.Runtime",
        "BundleLoader.AssetProxy", "_FinishWithAsset", "UnityEngine.Object", "System.Void", 1);
    object_name_ = runtime.ResolveMethodExact("UnityEngine.CoreModule.dll", "UnityEngine",
        "Object", "get_name", "", "System.String", 0);
    game_object_ = runtime.ResolveClass("UnityEngine.CoreModule.dll", "UnityEngine", "GameObject");
    skinned_renderer_ = runtime.ResolveClass("UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer");
    renderers_ = runtime.ResolveMethodExact("UnityEngine.CoreModule.dll", "UnityEngine", "GameObject",
        "GetComponentsInChildren", "System.Type|System.Boolean", "UnityEngine.Component[]", 2);
    array_length_ = runtime.ResolveMethodExact("mscorlib.dll", "System", "Array", "get_Length", "", "System.Int32", 0);
    array_get_ = runtime.ResolveMethodExact("mscorlib.dll", "System", "Array", "GetValue", "System.Int32", "System.Object", 1);
    shared_mesh_ = runtime.ResolveMethodExact("UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer",
        "get_sharedMesh", "", "UnityEngine.Mesh", 0);
    cached_ptr_ = runtime.ResolveField("UnityEngine.CoreModule.dll", "UnityEngine", "Object", "m_CachedPtr");
    mesh_layout_ = ProbeLoadedUnityMeshLayout();
    mesh_metadata_ = std::make_unique<MeshSkinMetadataAdapter>(mesh_layout_);
    LogInfo(Id(), ("Mesh layout paths: "+mesh_layout_.status).c_str());
    AuditMeshMethodAddresses(runtime);
    if (ConfigValue(config, "code_probe") == "1") AuditLoadedMeshCode(runtime);
    if (!delivery.entry || !object_name_.info || !game_object_.info || !renderers_.info ||
        !skinned_renderer_.type_object || !array_length_.info || !array_get_.info ||
        !shared_mesh_.info || !cached_ptr_.info)
        return {false, "custom model resource contracts unavailable"};
    instance_ = this;
    std::string hook_error;
    if (!broker_.Initialize(hook_error) || !broker_.Install(delivery.entry,
            reinterpret_cast<void*>(&Finish), reinterpret_cast<void**>(&original_), stub_, hook_error)) {
        instance_ = nullptr;
        return {false, hook_error};
    }
    if (ConfigValue(config, "copy_hook") == "1" && mesh_layout_.image_base != 0 &&
            mesh_layout_.copy_function_offsets.size() == 1) {
        void* target = reinterpret_cast<void*>(mesh_layout_.image_base +
            mesh_layout_.copy_function_offsets.front());
        std::string copy_error;
        if (!broker_.Install(target, reinterpret_cast<void*>(&CopyProbe),
                reinterpret_cast<void**>(&copy_original_), copy_stub_, copy_error)) {
            LogError(Id(), ("copy probe hook unavailable: " + copy_error).c_str());
        } else {
            LogInfo(Id(), "copy probe hook installed; read-only argument tracing enabled");
        }
    }
    if (ConfigValue(config, "create_hook") == "1") {
        void* target = runtime.ResolveIcall("UnityEngine.Mesh::Internal_Create");
        std::string create_error;
        if (!broker_.Install(target, reinterpret_cast<void*>(&CreateProbe),
                reinterpret_cast<void**>(&create_original_), create_stub_, create_error)) {
            LogError(Id(), ("create probe hook unavailable: " + create_error).c_str());
        } else {
            LogInfo(Id(), "Mesh.Internal_Create probe hook installed; read-only observation enabled");
        }
    }
    return {true, "BEM package validated; resource match is active, mesh publication remains evidence-gated"};
}

void CustomModelModule::CopyProbe(void* mesh, void* source) {
    const uint32_t count = g_copy_probe_count.fetch_add(1, std::memory_order_relaxed);
    if (count < 8 && mesh && source) {
        uint32_t source_value = 0;
        uint32_t target_before = 0;
        ReadOwnMemory(reinterpret_cast<uintptr_t>(source) + 0x14, &source_value, sizeof(source_value));
        ReadOwnMemory(reinterpret_cast<uintptr_t>(mesh) + 0x1c0, &target_before, sizeof(target_before));
        LogInfo("betterendfield.custom_model", ("copy probe before source_plus_14=" +
            std::to_string(source_value) + " target_plus_1c0=" + std::to_string(target_before)).c_str());
    }
    if (copy_original_) copy_original_(mesh, source);
    if (count < 8 && mesh) {
        uint32_t target_after = 0;
        ReadOwnMemory(reinterpret_cast<uintptr_t>(mesh) + 0x1c0, &target_after, sizeof(target_after));
        LogInfo("betterendfield.custom_model", ("copy probe after target_plus_1c0=" +
            std::to_string(target_after)).c_str());
    }
}

void CustomModelModule::CreateProbe(void* mesh) {
    if (create_original_) create_original_(mesh);
    const uint32_t count = g_create_probe_count.fetch_add(1, std::memory_order_relaxed);
    if (count >= 16 || !mesh || !instance_ || !instance_->runtime_) return;
    void* boxed = instance_->runtime_->ReadFieldObject(instance_->cached_ptr_, mesh);
    void* value = instance_->runtime_->Unbox(boxed);
    uintptr_t native = 0;
    if (value) std::memcpy(&native, value, sizeof(native));
    uint32_t candidate = 0;
    const bool readable = instance_->mesh_metadata_ && instance_->mesh_metadata_->Read(native, candidate);
    LogInfo("betterendfield.custom_model", ("create probe native=" + std::to_string(native) +
        " candidate=" + std::to_string(candidate) + " readable=" + (readable ? "yes" : "no")).c_str());
}

void CustomModelModule::ObserveDelivery(void* asset) {
    if (!asset || !runtime_->IsInstanceOf(asset, game_object_)) return;
    if (mesh_data_probe_ && !mesh_data_probe_done_.load()) {
        if (void* template_mesh = FindFirstMesh(asset)) {
            mesh_data_probe_done_.store(true);
            const bool passed = ProbeMeshDataBuilder(template_mesh);
            LogInfo(Id(), (std::string("Android MeshData scratch builder ") +
                (passed ? "PASS" : "FAIL") + " on Unity resource thread").c_str());
        }
    }
    void* exception = nullptr;
    void* asset_name = runtime_->Invoke(object_name_.info, asset, nullptr, &exception);
    const auto name=runtime_->CopyString(asset_name);
    if (detached_probe_ && deliveries_.fetch_add(1)<512)
        LogInfo(Id(), ("GameObject delivery name="+name).c_str());
    const bool matches=resource_name_=="auto" ? name==world_resource_ || name==ui_resource_ : name==resource_name_;
    if (exception || !matches) return;
    const uint32_t count = matched_.fetch_add(1, std::memory_order_relaxed) + 1;
    if (count <= 4) {
        LogInfo(Id(), ("matched resource " + name + " package=" + package_path_.string() +
            " appearance=" + appearance_ + " components=" + std::to_string(package_.components.size()) +
            "; publication withheld until MeshSkinMetadataAdapter is authorized").c_str());
    }
    // Keep the evidence pass bounded while a scene streams repeated clones.
    if (count <= 4) ObserveMatchedMeshes(asset);
}

void* CustomModelModule::FindFirstMesh(void* asset) {
    if (!runtime_ || !asset || !renderers_.info || !skinned_renderer_.type_object ||
            !array_length_.info || !array_get_.info || !shared_mesh_.info) return nullptr;
    bool include_inactive = true;
    void* args[]{skinned_renderer_.type_object, &include_inactive};
    void* exception = nullptr;
    void* array = runtime_->Invoke(renderers_.info, asset, args, &exception);
    if (exception || !array) return nullptr;
    void* length_box = runtime_->Invoke(array_length_.info, array, nullptr, &exception);
    void* length_raw = runtime_->Unbox(length_box);
    int32_t count = 0;
    if (exception || !length_raw) return nullptr;
    std::memcpy(&count, length_raw, sizeof(count));
    if (count <= 0 || count > 256) return nullptr;
    for (int32_t i = 0; i < count; ++i) {
        void* index_args[]{&i};
        exception = nullptr;
        void* renderer = runtime_->Invoke(array_get_.info, array, index_args, &exception);
        if (exception || !renderer) continue;
        exception = nullptr;
        void* mesh = runtime_->Invoke(shared_mesh_.info, renderer, nullptr, &exception);
        if (!exception && mesh) return mesh;
    }
    return nullptr;
}

void CustomModelModule::ObserveMatchedMeshes(void* asset) {
    if (!runtime_ || !mesh_metadata_ || !asset || !runtime_->IsInstanceOf(asset, game_object_)) return;
    bool include_inactive = true;
    void* args[]{skinned_renderer_.type_object, &include_inactive};
    void* exception = nullptr;
    void* array = runtime_->Invoke(renderers_.info, asset, args, &exception);
    if (exception || !array) return;
    auto invoke = [&](const ResolvedMethod& method, void* object, void** parameters) -> void* {
        void* error = nullptr;
        void* result = runtime_->Invoke(method.info, object, parameters, &error);
        return error ? nullptr : result;
    };
    void* length_box = runtime_->Unbox(invoke(array_length_, array, nullptr));
    if (!length_box) return;
    int32_t count = 0; std::memcpy(&count, length_box, sizeof(count));
    if (count <= 0 || count > 64) return;
    for (int32_t i = 0; i < count; ++i) {
        void* index_args[]{&i};
        void* renderer = invoke(array_get_, array, index_args);
        void* mesh = renderer ? invoke(shared_mesh_, renderer, nullptr) : nullptr;
        if (!mesh) continue;
        void* native_box = runtime_->ReadFieldObject(cached_ptr_, mesh);
        void* native_value = runtime_->Unbox(native_box);
        uintptr_t native = 0;
        if (native_value) std::memcpy(&native, native_value, sizeof(native));
        uint32_t candidate = 0;
        const bool readable = mesh_metadata_->Read(native, candidate);
        if (readable && detached_probe_ && !detached_probe_done_.exchange(true)) ProbeDetachedMesh(mesh);
        std::string name = runtime_->CopyString(invoke(object_name_, mesh, nullptr));
        LogInfo(Id(), ("matched mesh name=" + name + " native=" +
            std::to_string(native) + " candidate=" + std::to_string(candidate) +
            " readable=" + (readable ? "yes" : "no") +
            " evidence=" + mesh_layout_.status).c_str());
    }
}

bool CustomModelModule::ProbeMeshDataBuilder(void* template_mesh) {
    if (!runtime_ || !template_mesh || !AndroidMeshBuilderReady()) return false;
    auto mesh_class = runtime_->ResolveClass("UnityEngine.CoreModule.dll","UnityEngine","Mesh");
    auto ctor = runtime_->ResolveMethodExact("UnityEngine.CoreModule.dll","UnityEngine","Mesh",".ctor","","System.Void",0);
    auto destroy = runtime_->ResolveMethodExact("UnityEngine.CoreModule.dll","UnityEngine","Object","DestroyImmediate","UnityEngine.Object|System.Boolean","System.Void",2);
    if (!mesh_class.info || !ctor.info || !destroy.info) return false;
    if (!mesh_data_write_probe_) return true;
    for (const auto& component : package_.components) {
        if (component.info.flags & (kComponentFlagNoGeometry | kComponentFlagHidden)) continue;
        void* mesh = runtime_->NewObject(mesh_class.info);
        uint32_t root = runtime_->NewGcHandle(mesh,false);
        if (!mesh || !root) return false;
        void* exception = nullptr;
        runtime_->Invoke(ctor.info,mesh,nullptr,&exception);
        bool passed = !exception && AndroidSubmitMesh(mesh,component);
        bool assets = false; void* args[]{mesh,&assets};
        runtime_->Invoke(destroy.info,nullptr,args,&exception);
        runtime_->FreeGcHandle(root);
        if (!passed || exception) return false;
    }
    return true;
}

void CustomModelModule::ProbeDetachedMesh(void* source_mesh) {
    // Only a newly allocated, unpublished Mesh is writable in this experiment.
    // The resource's original Mesh and renderer bindings are never modified.
    if (!mesh_metadata_->LayoutVerified()) { LogError(Id(),"detached Mesh probe refused: independent paths missing"); return; }
    const auto klass=runtime_->ResolveClass("UnityEngine.CoreModule.dll","UnityEngine","Mesh");
    const auto ctor=runtime_->ResolveMethodExact("UnityEngine.CoreModule.dll","UnityEngine","Mesh",".ctor","","System.Void",0);
    const auto destroy=runtime_->ResolveMethodExact("UnityEngine.CoreModule.dll","UnityEngine","Object","DestroyImmediate","UnityEngine.Object|System.Boolean","System.Void",2);
    if (!klass.info || !ctor.info || !destroy.info) { LogError(Id(),"detached Mesh probe contracts missing"); return; }
    auto pointer=[&](void* mesh) {
        uintptr_t value=0; void* box=runtime_->ReadFieldObject(cached_ptr_,mesh);
        void* raw=runtime_->Unbox(box); if (raw) std::memcpy(&value,raw,sizeof(value)); return value;
    };
    uint32_t value=0;
    if (!mesh_metadata_->Read(pointer(source_mesh),value)) return;
    void* mesh=runtime_->NewObject(klass.info);
    if (!mesh) return;
    const auto root=runtime_->NewGcHandle(mesh,false);
    if (!root) return;
    void* exception=nullptr;
    runtime_->Invoke(ctor.info,mesh,nullptr,&exception);
    const auto native=exception?0:pointer(mesh);
    const bool written=native && mesh_metadata_->Write(native,value,true);
    LogInfo(Id(),("detached Mesh field initialize="+std::string(written?"PASS":"FAIL")+" source_value="+std::to_string(value)).c_str());
    if (native) {
        bool allow=false; void* args[]{mesh,&allow}; exception=nullptr;
        runtime_->Invoke(destroy.info,nullptr,args,&exception);
        LogInfo(Id(),exception?"detached Mesh destroy failed":"detached Mesh destroyed; original renderer unchanged");
    }
    runtime_->FreeGcHandle(root);
}

void CustomModelModule::Finish(void* proxy, void* asset, void* method) {
    auto* self = instance_;
    ConsumeModelCommand();
    if (self) {
        try { self->ObserveDelivery(asset); }
        catch (...) { LogError(self->Id(), "BEM resource match failed; original delivery retained"); }
    }
    original_(proxy, asset, method);
}
}

extern "C" __attribute__((visibility("default"))) BE_Result
BetterEndfield_RetireModuleHooksV1(void* context, const char* module_id) {
    auto* self = static_cast<betterendfield::CustomModelModule*>(context);
    return self ? self->RetireSharedHooks(module_id) : BE_Result_InvalidArgument;
}
