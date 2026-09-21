#pragma once
#include "modules/module.h"
#include "core/hook_broker.h"
#include "core/runtime.h"
#include "mesh_layout_probe.h"
#include "mesh_skin_metadata_adapter.h"
#include <atomic>
#include <memory>

namespace betterendfield {
// Read-only first-stage adapter; enabled explicitly in debug builds only.
class CustomModelResourceProbe final : public Module {
public:
    const char* Id() const override { return "betterendfield.custom_model.probe"; }
    ModuleResult Start(Il2CppRuntime& runtime) override;
private:
    static void Finish(void* proxy, void* asset, void* method);
    void ObserveMeshes(void* asset);
    static std::atomic<CustomModelResourceProbe*> instance_;
    using FinishFn = void(*)(void*, void*, void*);
    static FinishFn original_;
    Il2CppRuntime* runtime_ = nullptr;
    ResolvedMethod object_name_{};
    ResolvedClass game_object_{}, skinned_renderer_{};
    ResolvedMethod renderers_{}, array_length_{}, array_get_{}, shared_mesh_{}, bones_value_{};
    ResolvedField cached_ptr_{};
    bool mesh_observation_ready_ = false;
    MeshLayoutEvidence mesh_layout_{};
    std::unique_ptr<MeshSkinMetadataAdapter> mesh_metadata_;
    std::atomic<uint32_t> mesh_assets_{0};
    HookBroker broker_;
    void* stub_ = nullptr;
    std::atomic<uint64_t> observed_{0};
};
}
