// Default-off experiment: assume retired generated assets may be destroyed after
// a short grace period. This deliberately does not prove absence of pooled or
// unobserved game references. Original/game-created assets are never candidates.
constexpr uint64_t kRetiredResourceDelayMs=1000;
struct RetiredGeneratedAsset {
    WeakObject object;
    uint64_t due=0;
};
std::unordered_map<int32_t,RetiredGeneratedAsset> g_retired_generated_assets;
void CaptureExperimentalGeneratedAssets(CompletedResource& record) {
    if (!g_release_retired_resources_runtime.load() || !g_construction) return;
    std::unordered_set<int32_t> seen;
    for (void* asset:g_construction->assets) {
        if (!asset || !IsNativeObjectAlive(asset)) continue;
        WeakObject object;
        if (object.Set(asset) && seen.insert(object.instance_id).second)
            record.experimental_assets.push_back(std::move(object));
    }
}
void QueueExperimentalRetiredAssets(CompletedResource& record,uint64_t now) {
    if (!g_release_retired_resources_runtime.load()) return;
    size_t queued=0;
    for (auto& object:record.experimental_assets) {
        if (!object.instance_id) continue;
        const int32_t id=object.instance_id;
        RetiredGeneratedAsset pending{std::move(object),now+kRetiredResourceDelayMs};
        g_retired_generated_assets.insert_or_assign(id,std::move(pending));++queued;
    }
    record.experimental_assets.clear();
    if (queued) Log("[RetiredResourceCleanup] queued role="+std::string(record.adapter->id)+
        " selection="+record.selection_key+" objects="+std::to_string(queued)+
        " delayMs="+std::to_string(kRetiredResourceDelayMs));
}
bool ProtectCleanupMaterialArray(void* materials,std::unordered_set<int32_t>& protected_ids) {
    if (!materials) return false;
    const int count=ArrayLength(materials);
    if (count<0 || count>4096) return false;
    for (int i=0;i<count;++i) {
        void* material=ArrayValue(materials,i);if (!material) continue;
        int32_t id=0;
        if (!InvokeValue(Contract("object.instance_id"),material,nullptr,id)) return false;
        if (!protected_ids.insert(id).second) continue;
        void* slots=Invoke(Contract("material.get_texture_property_ids"),material,nullptr);
        if (!slots) return false;
        const int n=ArrayLength(slots);if (n<0 || n>4096) return false;
        for (int k=0;k<n;++k) {
            int32_t slot=0;if (!Unbox(ArrayValue(slots,k),slot)) return false;
            void* args[]{&slot};
            void* texture=Invoke(Contract("material.get_texture_by_id"),material,args,false);
            if (!texture) continue;
            if (!InvokeValue(Contract("object.instance_id"),texture,nullptr,id)) return false;
            protected_ids.insert(id);
        }
    }
    return true;
}
bool ProtectCurrentGeneratedAssets(std::unordered_set<int32_t>& protected_ids) {
    // Protect known current bindings and cross-generation texture reuse. The
    // experiment still intentionally assumes unobserved retired instances are gone.
    for (const auto& record:g_completed) {
        if (!record.root.Get()) continue;
        for (const auto& binding:record.bindings) {
            void* renderer=binding.renderer.Get();if (!renderer) continue;
            if (void* mesh=GetRendererMesh(renderer)) {
                int32_t id=0;if (!InvokeValue(Contract("object.instance_id"),mesh,nullptr,id)) return false;
                protected_ids.insert(id);
            }
#if defined(__ANDROID__) || defined(_WIN32)
            if (void* mesh=Invoke(Contract("android.shadow_mesh_get"),renderer,nullptr,false)) {
                int32_t id=0;if (!InvokeValue(Contract("object.instance_id"),mesh,nullptr,id)) return false;
                protected_ids.insert(id);
            }
#endif
            void* materials=Invoke(Contract("renderer.get_shared_materials"),renderer,nullptr);
            if (!ProtectCleanupMaterialArray(materials,protected_ids)) return false;
            void* source=RendererMaterialIdentityBase(renderer,materials);
            if (source!=materials && !ProtectCleanupMaterialArray(source,protected_ids)) return false;
        }
    }
    return true;
}
void TickExperimentalRetiredResources(uint64_t now,bool loading_busy) {
    if (!g_release_retired_resources_runtime.load() || loading_busy || g_retired_generated_assets.empty()) return;
    if (std::none_of(g_retired_generated_assets.begin(),g_retired_generated_assets.end(),
        [&](const auto& item){return item.second.due<=now;})) return;
    std::unordered_set<int32_t> protected_ids;
    if (!ProtectCurrentGeneratedAssets(protected_ids)) {
        static uint64_t next_log=0;
        if (now>=next_log) {next_log=now+10000;Log("[RetiredResourceCleanup] deferred: current binding inspection unavailable");}
        return;
    }
    size_t destroyed=0,gone=0,kept=0,failed=0;
    for (auto it=g_retired_generated_assets.begin();it!=g_retired_generated_assets.end();) {
        auto& pending=it->second;
        if (pending.due>now) {++it;continue;}
        void* object=pending.object.Get();
        if (!object) {it=g_retired_generated_assets.erase(it);++gone;continue;}
        if (protected_ids.contains(it->first)) {pending.due=now+kRetiredResourceDelayMs;++kept;++it;continue;}
        // DestroyUnityObject is intentionally limited to the current construction
        // transaction; these are tracked assets from an earlier committed one.
        void* args[]{object};
        if (!InvokeVoid(Contract("object.destroy"),nullptr,args)) {pending.due=now+kRetiredResourceDelayMs;++failed;++it;continue;}
        g_dynamic_meshes.erase(it->first);g_generated_texture_identity.erase(it->first);
        it=g_retired_generated_assets.erase(it);++destroyed;
    }
    static uint64_t next_summary=0;
    if (destroyed || gone || failed || now>=next_summary) {
        next_summary=now+10000;
        Log("[RetiredResourceCleanup] released="+std::to_string(destroyed)+" alreadyGone="+std::to_string(gone)+
            " currentShared="+std::to_string(kept)+" failed="+std::to_string(failed)+
            " pending="+std::to_string(g_retired_generated_assets.size()));
    }
}
