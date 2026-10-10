// NPC postmodels can wrap the ordinary model in one same-named child. Keep
// the original delivered prefab intact; bind its actual model root with the
// existing BEM, donor checks and publication/restore transaction.
void* ResolveLegacyModelReceiverRoot(void* asset) {
    if (!IsGameObjectResource(asset) || !IsNativeObjectAlive(asset)) return asset;
    const auto name=ObjectName(asset);
    const auto base=ResourceBaseName(name);
    const CharacterAdapter* adapter=nullptr;
    if (const auto* mod=g_registry.Match(name)) adapter=mod->adapter;
    else for (const auto& completed:g_completed) {
        if (!completed.adapter->explicit_resource && base==completed.adapter->world_resource) {
            adapter=completed.adapter;break;
        }
    }
    // Explicit resource paths stay relative to their declared prefab root.
    if (!adapter || adapter->explicit_resource || base!=adapter->world_resource) return asset;
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
        if (ResourceBaseName(child_name)!=base) continue;
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
    Log("Model receiver wrapper resolved: "+name+" -> "+ObjectName(receiver));
    return receiver;
}
