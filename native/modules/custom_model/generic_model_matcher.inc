// Include inside CustomModel after PreparedBinding/Donor* declarations.
// generic_model_matcher.h must be included at translation-unit scope.
struct GenericRendererCandidate {
    GenericMatching::ReceiverKey key;
    void* renderer=nullptr;
    void* mesh=nullptr;
    void* materials=nullptr;
    void* bones=nullptr;
    bool enabled=false;
};
bool ReadGenericMeshIdentity(void* mesh,GenericMatching::DonorOrigin origin,GenericMatching::MeshIdentity& identity) {
    identity={};
    if (!mesh || !IsNativeObjectAlive(mesh) ||
        (origin!=GenericMatching::DonorOrigin::Pristine && origin!=GenericMatching::DonorOrigin::SavedOriginal)) return false;
    int32_t submeshes=0;
    if (!InvokeValue(Contract("mesh.get_sub_mesh_count"),mesh,nullptr,submeshes) || submeshes<=0 || submeshes>256) return false;
    uint64_t total=0;
    for (int32_t sub=0;sub<submeshes;++sub) {
        uint32_t count=0; void* args[]{&sub};
        if (!InvokeValue(Contract("mesh.get_index_count"),mesh,args,count)) return false;
        total+=count;
    }
    identity.origin=origin; identity.mesh=mesh; identity.name=ObjectName(mesh);
    identity.indices=total; identity.indices_known=true;
    return !identity.name.empty();
}
bool ResourceRelativePath(void* asset,void* component,std::string& path) {
    const auto result=GenericMatching::CheckedRelativePath(
        Invoke(Contract("game_object.get_transform"),asset,nullptr),
        Invoke(Contract("component.get_transform"),component,nullptr),
        [](void* transform) { return Invoke(Contract("transform.get_parent"),transform,nullptr); },
        [](void* transform) { return ObjectName(transform); });
    if (result.status!=GenericMatching::PathStatus::Valid) return false;
    path=result.path; return true;
}
bool BuildGenericRendererIndex(const CharacterAdapter& adapter,void* asset,void* renderers,
    std::vector<GenericRendererCandidate>& candidates) {
    candidates.clear();
    const int count=ArrayLength(renderers);
    auto resource=ObjectName(asset);
    resource=std::string(ResourceBaseName(resource));
    if (!asset || !renderers || count<=0 || count>4096 ||
        (resource!=adapter.ui_resource && resource!=adapter.world_resource)) return false;
    std::unordered_map<std::string,void*> paths;
    for (int i=0;i<count;++i) {
        GenericRendererCandidate c; c.renderer=ArrayValue(renderers,i);
        std::string path;
        if (!c.renderer || !IsNativeObjectAlive(c.renderer) || !ResourceRelativePath(asset,c.renderer,path) || path.empty() ||
            !paths.emplace(path,c.renderer).second) return false;
        c.key={adapter.id,resource,path,GenericMatching::ClassifyReceiver(path)};
        c.mesh=Invoke(Contract("skinned.get_shared_mesh"),c.renderer,nullptr);
        c.materials=Invoke(Contract("renderer.get_shared_materials"),c.renderer,nullptr);
        c.bones=Invoke(Contract("skinned.get_bones"),c.renderer,nullptr);
        if (!GetRendererEnabled(c.renderer,c.enabled)) return false;
        candidates.push_back(std::move(c));
    }
    return true;
}
// Runtime glue supplies a read-only lineage lookup. Known custom output must
// return CompletedWithoutOriginal/Unavailable, never Pristine just by its name.
// No bone arrays, material clones, Meshes or completed records are allocated.
bool ReadGenericPristineMesh(const CharacterAdapter& adapter,void* asset,
    const GenericRendererCandidate& candidate,GenericMatching::MeshIdentity& identity);
