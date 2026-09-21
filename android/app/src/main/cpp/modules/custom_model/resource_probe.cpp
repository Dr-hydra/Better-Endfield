#include "resource_probe.h"
#include "core/log.h"
#include "core/command_pump.h"
#include <cstdio>
#include <cstring>
#include <array>
#include <sys/uio.h>
#include <sys/syscall.h>
#include <unistd.h>

namespace betterendfield {
namespace {
// Read only our own mapped code, without dereferencing a possibly stale pointer.
void LogGetterCode(const char* component, const Il2CppRuntime& runtime) {
    void* getter = runtime.ResolveIcall("UnityEngine.Mesh::GetBonesPerVertexValue");
    if (!getter) return;
    auto read = [](uintptr_t address, void* output, size_t count) {
        iovec local{output, count}, remote{reinterpret_cast<void*>(address), count};
        return process_vm_readv(getpid(), &local, 1, &remote, 1, 0) == static_cast<ssize_t>(count);
    };
    auto dump = [&](uintptr_t address, const uint8_t* bytes, size_t size) {
        char prefix[80];
        std::snprintf(prefix, sizeof(prefix), "getter-code address=%p bytes=", reinterpret_cast<void*>(address));
        std::string line(prefix);
        constexpr char digits[] = "0123456789abcdef";
        for (size_t i = 0; i < size; ++i) {
            line.push_back(digits[bytes[i] >> 4]); line.push_back(digits[bytes[i] & 15]);
        }
        LogInfo(component, line.c_str());
    };
    std::array<uint8_t, 256> code{};
    auto address = reinterpret_cast<uintptr_t>(getter);
    if (!read(address, code.data(), code.size())) {
        LogInfo(component, "getter code read unavailable"); return;
    }
    dump(address, code.data(), code.size());
    for (size_t i = 0; i < code.size(); i += 4) {
        uint32_t instruction; std::memcpy(&instruction, code.data() + i, sizeof(instruction));
        if (instruction == 0xd65f03c0) break; // RET: do not inspect the next function.
        if ((instruction & 0xfc000000) != 0x94000000) continue;
        int64_t displacement = instruction & 0x03ffffff;
        if (displacement & 0x02000000) displacement -= 0x04000000;
        auto target = static_cast<uintptr_t>(static_cast<int64_t>(address + i) + displacement * 4);
        std::array<uint8_t, 32> called{};
        if (read(target, called.data(), called.size())) dump(target, called.data(), called.size());
    }
}
}
std::atomic<CustomModelResourceProbe*> CustomModelResourceProbe::instance_{nullptr};
CustomModelResourceProbe::FinishFn CustomModelResourceProbe::original_ = nullptr;

ModuleResult CustomModelResourceProbe::Start(Il2CppRuntime& runtime) {
    auto delivery = runtime.ResolveMethodExact("Common.Beyond.dll", "Beyond.Resource.Runtime",
        "BundleLoader.AssetProxy", "_FinishWithAsset", "UnityEngine.Object", "System.Void", 1);
    object_name_ = runtime.ResolveMethodExact("UnityEngine.CoreModule.dll", "UnityEngine",
        "Object", "get_name", "", "System.String", 0);
    if (!delivery.entry || !object_name_.info) {
        LogInfo(Id(), runtime.DescribeMethod("Common.Beyond.dll", "Beyond.Resource.Runtime",
            "BundleLoader.AssetProxy", "_FinishWithAsset").c_str());
        LogInfo(Id(), runtime.DescribeMethod("UnityEngine.CoreModule.dll", "UnityEngine",
            "Object", "get_name").c_str());
        return {false, "resource delivery/name contract unavailable; no hook installed"};
    }
    runtime_ = &runtime;
    game_object_ = runtime.ResolveClass("UnityEngine.CoreModule.dll", "UnityEngine", "GameObject");
    skinned_renderer_ = runtime.ResolveClass("UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer");
    renderers_ = runtime.ResolveMethodExact("UnityEngine.CoreModule.dll", "UnityEngine", "GameObject",
        "GetComponentsInChildren", "System.Type|System.Boolean", "UnityEngine.Component[]", 2);
    array_length_ = runtime.ResolveMethodExact("mscorlib.dll", "System", "Array", "get_Length", "", "System.Int32", 0);
    array_get_ = runtime.ResolveMethodExact("mscorlib.dll", "System", "Array", "GetValue", "System.Int32", "System.Object", 1);
    shared_mesh_ = runtime.ResolveMethodExact("UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer",
        "get_sharedMesh", "", "UnityEngine.Mesh", 0);
    bones_value_ = runtime.ResolveMethodExact("UnityEngine.CoreModule.dll", "UnityEngine", "Mesh",
        "GetBonesPerVertexValue", "", "System.Int32", 0);
    cached_ptr_ = runtime.ResolveField("UnityEngine.CoreModule.dll", "UnityEngine", "Object", "m_CachedPtr");
    mesh_observation_ready_ = game_object_.info && skinned_renderer_.type_object && renderers_.info &&
        array_length_.info && array_get_.info && shared_mesh_.info && cached_ptr_.info;
    LogInfo(Id(), runtime.DescribeMethod("UnityEngine.CoreModule.dll", "UnityEngine",
        "Mesh", "GetBonesPerVertexValue").c_str());
    LogInfo(Id(), mesh_observation_ready_ ? "Mesh observation contracts ready" : "Mesh observation contracts incomplete");
    LogGetterCode(Id(), runtime);
    mesh_layout_ = ProbeLoadedUnityMeshLayout();
    mesh_metadata_ = std::make_unique<MeshSkinMetadataAdapter>(mesh_layout_);
    LogInfo(Id(), ("dynamic Mesh layout: candidate=" + std::to_string(mesh_layout_.candidate_offset) +
        " archive_refs=" + std::to_string(mesh_layout_.named_references) +
        " archive_functions=" + std::to_string(mesh_layout_.archive_functions) +
        " copy_functions=" + std::to_string(mesh_layout_.copy_functions) +
        " read_functions=" + std::to_string(mesh_layout_.field_read_functions) +
        " write_functions=" + std::to_string(mesh_layout_.field_write_functions) +
        " clamp_sites=" + std::to_string(mesh_layout_.clamp_consumers) + " " + mesh_layout_.status).c_str());
    for (const auto offset : mesh_layout_.copy_function_offsets) {
        LogInfo(Id(), ("dynamic Mesh copy candidate libunity.so+0x" +
            [&] { char buffer[32]; std::snprintf(buffer, sizeof(buffer), "%zx", offset); return std::string(buffer); }()).c_str());
    }
    instance_.store(this, std::memory_order_release);
    std::string error;
    if (!broker_.Initialize(error) || !broker_.Install(delivery.entry,
            reinterpret_cast<void*>(&Finish), reinterpret_cast<void**>(&original_), stub_, error)) {
        instance_.store(nullptr, std::memory_order_release);
        return {false, error};
    }
    return {true, "read-only AssetProxy._FinishWithAsset observation ready; field writes disabled"};
}

void CustomModelResourceProbe::ObserveMeshes(void* asset) {
    if (!mesh_observation_ready_ || mesh_assets_.load() >= 32 || !runtime_->IsInstanceOf(asset, game_object_)) return;
    bool include_inactive = true;
    void* args[]{skinned_renderer_.type_object, &include_inactive};
    void* exception = nullptr;
    void* array = runtime_->Invoke(renderers_.info, asset, args, &exception);
    if (exception || !array) return;
    auto invoke = [&](const ResolvedMethod& method, void* object, void** parameters) -> void* {
        if (!method.info) return nullptr;
        void* error = nullptr;
        void* result = runtime_->Invoke(method.info, object, parameters, &error);
        return error ? nullptr : result;
    };
    void* length = runtime_->Unbox(invoke(array_length_, array, nullptr));
    if (!length) return;
    int32_t count = 0; std::memcpy(&count, length, sizeof(count));
    if (count <= 0 || count > 64) return;
    if (mesh_assets_.fetch_add(1) >= 32) return;
    for (int32_t i = 0; i < count; ++i) {
        void* index_args[]{&i};
        void* renderer = invoke(array_get_, array, index_args);
        if (!renderer) continue;
        void* mesh = invoke(shared_mesh_, renderer, nullptr);
        if (!mesh) continue;
        std::string name = runtime_->CopyString(invoke(object_name_, mesh, nullptr));
        void* native_box = runtime_->ReadFieldObject(cached_ptr_, mesh);
        void* native_value = runtime_->Unbox(native_box);
        uintptr_t native = 0;
        if (native_value) std::memcpy(&native, native_value, sizeof(native));
        int32_t bones = -1;
        void* bones_box = runtime_->Unbox(invoke(bones_value_, mesh, nullptr));
        if (bones_box) std::memcpy(&bones, bones_box, sizeof(bones));
        uint32_t observed = 0;
        bool read = mesh_metadata_ && mesh_metadata_->Read(native, observed);
        char message[256];
        std::snprintf(message, sizeof(message), "Mesh source native=%p getter=%d candidate_value=%u readable=%s name=",
            reinterpret_cast<void*>(native), bones, observed, read ? "yes" : "no");
        LogInfo(Id(), (std::string(message) + name).c_str());
    }
}

void CustomModelResourceProbe::Finish(void* proxy, void* asset, void* method) {
    // No ownership escapes the delivery callback. Reentrant deliveries still
    // pass to the game's original function exactly once.
    static thread_local bool observing = false;
    auto* self = instance_.load(std::memory_order_acquire);
    std::string command;
    if (ConsumeRuntimeCommand(command)) {
        const size_t first = command.find('\n', 14);
        const size_t second = first == std::string::npos ? std::string::npos : command.find('\n', first + 1);
        const std::string name = first == std::string::npos || second == std::string::npos
            ? std::string{} : command.substr(first + 1, second - first - 1);
        const char* result = name == "overlay_hide" ? "applied" : "unsupported";
        AcknowledgeRuntimeCommand(result);
        LogInfo("command_pump", ("Unity-thread command " + std::string(result) + ": " + name).c_str());
    }
    if (self && asset && !observing) {
        observing = true;
        try {
            auto count = self->observed_.fetch_add(1, std::memory_order_relaxed) + 1;
            // Bound log and metadata overhead during large loading batches.
            if (count <= 32 || count % 256 == 0) {
                void* exception = nullptr;
                void* name = self->runtime_->Invoke(self->object_name_.info, asset, nullptr, &exception);
                std::string identity = exception ? "<name exception>" : self->runtime_->CopyString(name);
                char context[160];
                std::snprintf(context, sizeof(context), "delivery=%llu tid=%ld proxy=%p asset=%p name=",
                    static_cast<unsigned long long>(count), syscall(SYS_gettid), proxy, asset);
                LogInfo(self->Id(), (std::string(context) + identity).c_str());
            }
            self->ObserveMeshes(asset);
        } catch (...) { LogError(self->Id(), "resource observation failed; original delivery retained"); }
        observing = false;
    }
    original_(proxy, asset, method);
}
}
