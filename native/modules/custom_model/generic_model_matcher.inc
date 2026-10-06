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
        if (!IsModelRenderer(c.renderer) || (!adapter.explicit_resource && IsStaticRenderer(c.renderer))) continue;
        if (!IsNativeObjectAlive(c.renderer) || !ResourceRelativePath(asset,c.renderer,path) || (!adapter.explicit_resource && path.empty()) ||
            !paths.emplace(path,c.renderer).second) return false;
        if (adapter.explicit_resource && std::none_of(adapter.components.begin(),adapter.components.end(),[&](const auto& identity){
            return path==identity.receiver_path && identity.static_mesh==IsStaticRenderer(c.renderer);
        })) continue;
        c.key={adapter.id,resource,path,adapter.explicit_resource?GenericMatching::Region::Explicit:GenericMatching::ClassifyReceiver(path)};
        c.mesh=GetRendererMesh(c.renderer);
        c.materials=Invoke(Contract("renderer.get_shared_materials"),c.renderer,nullptr);
        c.bones=GetRendererBones(c.renderer);
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

// Shared read-only compatibility check. Keep failure reasons alongside the
// actual gate so diagnostics cannot claim a different contract was checked.
const char* GenericCandidateContractError(void* asset,const GenericRendererCandidate& candidate,bool static_mesh) {
    if (static_mesh!=IsStaticRenderer(candidate.renderer)) return "renderer-kind";
    const int materials=ArrayLength(candidate.materials),bones=ArrayLength(candidate.bones);
    if (materials<=0 || materials>256) return "material-count";
    if (bones<0 || bones>256) return "bone-count";
    for (int i=0;i<materials;++i) {
        void* material=ArrayValue(candidate.materials,i);
        if (!material || !IsNativeObjectAlive(material)) return "material-missing-or-dead";
        void* shader=Invoke(Contract("material.get_shader"),material,nullptr);
        if (!shader || !IsNativeObjectAlive(shader)) return "shader-missing-or-dead";
    }
    for (int i=0;i<bones;++i) {
        void* bone=ArrayValue(candidate.bones,i);std::string path;
        if (!bone || !IsNativeObjectAlive(bone)) return "bone-missing-or-dead";
        if (!ResourceRelativePath(asset,bone,path)) return "bone-outside-resource-root";
    }
    return nullptr;
}
std::string DescribeGenericDonorFailure(void* asset,const std::vector<GenericRendererCandidate>& index,
    const std::vector<GenericMatching::Candidate>& candidates,const GenericMatching::Request& request,
    GenericMatching::MatchStatus status,bool static_mesh) {
    std::string text=" resource="+std::string(request.resource)+" status="+
        (status==GenericMatching::MatchStatus::Ambiguous?"ambiguous":
         status==GenericMatching::MatchStatus::InvalidIndex?"invalid-index":"missing")+
        " expectedIndices="+std::to_string(request.indices)+" checked="+(request.check_indices?"true":"false");
    if (request.region==GenericMatching::Region::Explicit) text+=" receiver="+std::string(request.verified_receiver_path);
    size_t relevant=0,shown=0;
    for (const auto& c:candidates) if (c.key.region==request.region) ++relevant;
    text+=" candidates="+std::to_string(relevant);
    // Show exact-name candidates first; unrelated meshes are useful only when
    // the expected donor is absent. Bound diagnostics for large prefab graphs.
    for (int pass=0;pass<2 && shown<6;++pass) for (size_t i=0;i<candidates.size() && shown<6;++i) {
        const auto& c=candidates[i];const auto& p=c.pristine;
        if (c.key.region!=request.region || (p.name==request.mesh_name)!=(pass==0)) continue;
        std::string reason;
        if (!p.detail.empty()) reason=p.detail;
        else if (p.origin!=GenericMatching::DonorOrigin::Pristine && p.origin!=GenericMatching::DonorOrigin::SavedOriginal) reason="unverified-lineage";
        else if (p.name!=request.mesh_name) reason="mesh-name";
        else if ((request.region==GenericMatching::Region::Explicit || !request.verified_receiver_path.empty()) && c.key.path!=request.verified_receiver_path) reason="receiver-path";
        else if (request.check_indices && (!p.indices_known || p.indices!=request.indices)) reason="index-count";
        else if (const char* error=GenericCandidateContractError(asset,index[i],static_mesh)) reason=error;
        else reason="compatible";
        text+=" ["+c.key.path+" mesh="+p.name+" indices="+
            (p.indices_known?std::to_string(p.indices):"unknown")+" reason="+reason+"]";
        ++shown;
    }
    return text;
}
