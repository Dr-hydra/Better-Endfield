// NPC postmodels can wrap the ordinary model in one same-named child. Keep
// the delivered prefab intact; the actual model root owns every binding.
std::string_view ObservedNpcModelResource(std::string_view resource) {
#if defined(_WIN32)
    // Dijiang NPC sample 2026-10-17: the 02 prefab wraps the standard root;
    // all 16 LOD0 mesh/index/material/bone-name contracts matched. Do not
    // generalize numeric suffixes to unobserved NPC or story resources.
    if (resource=="chr_0036_jsspsi02_postmodel") return "chr_0036_jsspsi_postmodel";
#endif
    return resource;
}
bool AllowsOrdinaryNpcModelRoot(const CharacterAdapter& adapter) {
    if (!adapter.explicit_resource) return true;
#if defined(_WIN32)
    // Only the ordinary Windows character LOD0 layout can use this alias.
    // Arbitrary explicit roots, weapons, UI and other LODs keep their paths.
    const std::string_view id=adapter.id;
    if (!id.starts_with("chr_") || std::string(adapter.world_resource)!=std::string(id)+"_postmodel" ||
        adapter.receiver_lod!=0 || adapter.components.empty()) return false;
    return std::all_of(adapter.components.begin(),adapter.components.end(),[](const auto& component) {
        const std::string_view path=component.receiver_path;
        constexpr std::string_view prefix="Mesh_all/lod0/";
        return !component.static_mesh && path.starts_with(prefix) && path.size()>prefix.size();
    });
#else
    return false;
#endif
}
void* ResolveLegacyModelReceiverRoot(void* asset) {
    if (!IsGameObjectResource(asset) || !IsNativeObjectAlive(asset)) return asset;
    const auto name=ObjectName(asset);
    const auto base=ResourceBaseName(name);
    const auto model_resource=ObservedNpcModelResource(base);
    const CharacterAdapter* adapter=nullptr;
    if (const auto* mod=g_registry.Match(model_resource)) adapter=mod->adapter;
    else for (const auto& completed:g_completed) {
        if (AllowsOrdinaryNpcModelRoot(*completed.adapter) && model_resource==completed.adapter->world_resource) {
            adapter=completed.adapter;break;
        }
    }
    if (!adapter || !AllowsOrdinaryNpcModelRoot(*adapter) || model_resource!=adapter->world_resource) return asset;
    const auto* child_count=Contract("transform.child_count");
    const auto* get_child=Contract("transform.get_child");
    const auto* game_object=Contract("component.get_game_object");
    if (!child_count || !child_count->resolved || !get_child || !get_child->resolved ||
        !game_object || !game_object->resolved) return asset;
    void* transform=Invoke(Contract("game_object.get_transform"),asset,nullptr);
    int32_t count=0;
    if (!transform || !InvokeValue(child_count,transform,nullptr,count) || count<0 || count>128) return asset;
    void* model=nullptr;
    bool ambiguous=false;
    for (int32_t i=0;i<count;++i) {
        void* args[]{&i};void* child=Invoke(get_child,transform,args);
        if (!child || !IsNativeObjectAlive(child)) return asset;
        const auto child_name=ObjectName(child);
        if (child_name=="Mesh_all") return asset; // Already the ordinary model root.
        if (ResourceBaseName(child_name)!=model_resource) continue;
        if (model) ambiguous=true;
        else model=child;
    }
    if (!model || ambiguous) return asset;
    int32_t model_children=0;
    if (!InvokeValue(child_count,model,nullptr,model_children) || model_children<1 || model_children>128) return asset;
    bool has_mesh_branch=false;
    for (int32_t i=0;i<model_children;++i) {
        void* args[]{&i};void* child=Invoke(get_child,model,args);
        if (!child || !IsNativeObjectAlive(child)) return asset;
        if (ObjectName(child)=="Mesh_all") {
            if (has_mesh_branch) return asset;
            has_mesh_branch=true;
        }
    }
    if (!has_mesh_branch) return asset;
    void* receiver=Invoke(game_object,model,nullptr);
    if (!IsGameObjectResource(receiver) || !IsNativeObjectAlive(receiver)) return asset;
    // Geometry, mesh space, indices, materials and bones are still verified by
    // PrepareResource / the Android paired-world adapter before any mutation.
    Log("Model receiver wrapper resolved: "+name+" -> "+ObjectName(receiver)+
        (adapter->explicit_resource?" layout=explicit-character-lod0":" layout=legacy"));
    return receiver;
}
