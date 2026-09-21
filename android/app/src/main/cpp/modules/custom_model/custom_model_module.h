#pragma once

#include "core/hook_broker.h"
#include "core/runtime.h"
#include "BetterEndfield/ModuleApi.h"
#include "modules/module.h"
#include "bem.h"
#include "mesh_skin_metadata_adapter.h"

#include <atomic>
#include <filesystem>
#include <memory>
#include <string>
#include <vector>

namespace betterendfield {

class CustomModelModule final : public Module {
public:
    const char* Id() const override { return "betterendfield.custom_model"; }
    ModuleResult Start(Il2CppRuntime& runtime) override;
    BE_Result RetireSharedHooks(const char* module_id);

private:
    static CustomModelModule* instance_;
    static void Finish(void* proxy, void* asset, void* method);
    static void CopyProbe(void* mesh, void* source);
    static void CreateProbe(void* mesh);
    void ObserveDelivery(void* asset);
    void ObserveMatchedMeshes(void* asset);
    void* FindFirstMesh(void* asset);
    void ProbeDetachedMesh(void* source_mesh);
    bool ProbeMeshDataBuilder(void* template_mesh);
    static CustomModelModule* HostSelf(void* context);
    static void HostLog(void* context, const char* module_id, const char* message);
    static BE_Result HostResolveMethod(void*, const BE_MethodDescriptorV1*, BE_ResolvedMethodV1*);
    static BE_Result HostResolveField(void*, const BE_FieldDescriptorV1*, BE_ResolvedFieldV1*);
    static BE_Result HostCreateHook(void*, const char*, void*, void*, void**);
    static BE_Result HostReleaseHooks(void*, const char*);
    static int HostCopyConfiguration(void*, const char*, char*, size_t);
    static int HostCopyCatalogRoot(void*, char*, size_t);
    static int HostCopyManagedString(void*, const void*, char*, size_t);
    static BE_Result HostResolveClass(void*, const char*, const char*, const char*, BE_ResolvedClassV1*);
    static void* HostObjectNew(void*, const void*);
    static void* HostStringNew(void*, const char*);
    static void* HostRuntimeInvoke(void*, const void*, void*, void**, void**);
    static void* HostObjectUnbox(void*, void*);
    static uint32_t HostGcHandleNew(void*, void*, int);
    static void HostGcHandleFree(void*, uint32_t);
    static void* HostFieldGetValueObject(void*, const void*, void*);
    bool InitializeSharedReplacement(const std::string& config);

    using FinishFn = void(*)(void*, void*, void*);
    using CopyFn = void(*)(void*, void*);
    using CreateFn = void(*)(void*);
    static FinishFn original_;
    static CopyFn copy_original_;
    static CreateFn create_original_;
    Il2CppRuntime* runtime_ = nullptr;
    HookBroker broker_;
    void* stub_ = nullptr;
    void* copy_stub_ = nullptr;
    void* create_stub_ = nullptr;
    ResolvedMethod object_name_{};
    ResolvedClass game_object_{};
    ResolvedMethod renderers_{};
    ResolvedClass skinned_renderer_{};
    ResolvedMethod array_length_{};
    ResolvedMethod array_get_{};
    ResolvedMethod shared_mesh_{};
    ResolvedField cached_ptr_{};
    MeshLayoutEvidence mesh_layout_{};
    std::unique_ptr<MeshSkinMetadataAdapter> mesh_metadata_;
    std::string resource_name_;
    std::string world_resource_, ui_resource_;
    bool detached_probe_ = false;
    std::atomic<bool> detached_probe_done_{false};
    bool mesh_data_probe_ = false;
    bool mesh_data_write_probe_ = false;
    std::atomic<bool> mesh_data_probe_done_{false};
    std::filesystem::path package_path_;
    std::vector<std::filesystem::path> package_paths_;
    std::string appearance_;
    BetterEndfield::CustomModel::BemPocData package_{};
    std::atomic<uint32_t> matched_{0};
    std::atomic<uint32_t> deliveries_{0};
    struct HookRecord { std::string module_id; void* stub = nullptr; };
    BE_HostApiV1 host_{};
    HookBroker replacement_broker_;
    std::vector<HookRecord> replacement_hooks_;
    const BE_ModuleApiV1* replacement_api_ = nullptr;
    std::filesystem::path replacement_root_;
    std::string replacement_config_;
    bool replacement_active_ = false;
};
}
