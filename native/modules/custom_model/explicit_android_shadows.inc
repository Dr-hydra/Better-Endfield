// Explicit BEM resources use their own renderers. Never borrow the legacy UI
// donor or infer proxy ownership from a character/renderer name.
bool PrepareExplicitAndroidShadows(const CharacterAdapter& adapter,const BemPocData& bem,
    void* asset,std::vector<PreparedBinding>& bindings) {
    if (!adapter.explicit_resource) return true;
    const auto refuse=[](const std::string& why) {Log("Explicit Android shadow mapping refused: "+why);return false;};
    if (bindings.size()!=bem.components.size()) return refuse("component transaction is incomplete");
    std::vector<bool> changed(bindings.size(),false);
    bool any_changed=false;
    for (size_t i=0;i<bindings.size();++i) {
        changed[i]=!(bem.components[i].info.flags&kComponentFlagNoGeometry) ||
            (bem.components[i].info.flags&kComponentFlagHidden);
        any_changed|=changed[i];
    }
    // Work on a copy: a refused ownership proof leaves the prepared transaction
    // untouched and, like every preparation stage, never writes to Unity.
    auto prepared=bindings;
    bool restoring=false;
    for (size_t i=0;i<bindings.size();++i) if (!changed[i]) restoring|=RestoreSavedShadowState(prepared[i]);
    if (!any_changed && !restoring) return true;
    if (!AndroidShadowContractsAvailable()) return refuse("shadow getter/setter contract unavailable");
    for (size_t i=0;i<bindings.size();++i) {
        auto& b=prepared[i];
        if (!InvokeValue(Contract("android.shadow_get"),b.renderer,nullptr,b.original_shadow) ||
            b.original_shadow<0 || b.original_shadow>3 ||
            !ReadNullableObject(Contract("android.shadow_mesh_get"),b.renderer,b.original_shadow_mesh))
            return refuse("unreadable shadow state: "+b.receiver_key.path);
        if (!changed[i]) continue;
        b.change_shadow=true;b.custom_shadow=b.original_shadow;b.custom_shadow_mesh=nullptr;
    }
    const auto same_bones=[](void* left,void* right) {
        if (ArrayLength(left)!=ArrayLength(right)) return false;
        for (int i=0;i<ArrayLength(left);++i) if (ArrayValue(left,i)!=ArrayValue(right,i)) return false;
        return true;
    };
    const auto shares_bones=[](void* left,void* right) {
        if (!ArrayLength(left) && !ArrayLength(right)) return true;
        for (int i=0;i<ArrayLength(left);++i) for (int j=0;j<ArrayLength(right);++j)
            if (ArrayValue(left,i)==ArrayValue(right,j)) return true;
        return false;
    };
    bool inactive=true;void* args[]{ModelRendererType(),&inactive};
    void* renderers=Invoke(Contract("game_object.renderers"),asset,args);
    const int count=ArrayLength(renderers);
    if (!renderers || count<0 || count>4096) return refuse("renderer enumeration unavailable");
    for (int n=0;n<count;++n) {
        void* renderer=ArrayValue(renderers,n);std::string path;
        if (!IsModelRenderer(renderer) || !ResourceRelativePath(asset,renderer,path)) continue;
        if (GenericMatching::ClassifyReceiver(path)!=GenericMatching::Region::MobileProxy) continue;
        if (std::any_of(bindings.begin(),bindings.end(),[&](const auto& b){return b.renderer==renderer;})) continue;
        GenericRendererCandidate candidate;
        candidate.key={adapter.id,std::string(ResourceBaseName(ObjectName(asset))),path,GenericMatching::Region::Explicit};
        candidate.renderer=renderer;candidate.mesh=GetRendererMesh(renderer);candidate.bones=GetRendererBones(renderer);
        candidate.materials=Invoke(Contract("renderer.get_shared_materials"),renderer,nullptr);
        if (!GetRendererEnabled(renderer,candidate.enabled)) return refuse("proxy enabled state unavailable: "+path);
        GenericMatching::MeshIdentity identity;
        if (!ReadGenericPristineMesh(adapter,asset,candidate,identity) || !identity.mesh)
            return refuse("proxy pristine Mesh unavailable: "+path);
        size_t owner=0,owners=0;bool changed_owner=false,possibly_affected=false;
        for (size_t i=0;i<bindings.size();++i) {
            const auto& b=prepared[i];
            const bool mesh_link=identity.mesh==DonorMesh(b) || identity.mesh==PristineShadowMesh(b);
            if (changed[i] && (mesh_link || shares_bones(candidate.bones,DonorBones(b)))) possibly_affected=true;
            if (!SameMeshSpace(renderer,b.renderer)) continue;
            if (IsStaticRenderer(renderer)!=IsStaticRenderer(b.renderer) || !same_bones(candidate.bones,DonorBones(b))) continue;
            if (!mesh_link) continue;
            owner=i;++owners;changed_owner|=changed[i];
        }
        // A package may target only part of this prefab. Read the other live
        // receivers to disprove uniqueness, never to turn them into targets.
        for (int v=0;v<count;++v) {
            void* other=ArrayValue(renderers,v);std::string other_path;
            if (other==renderer || !IsModelRenderer(other) || !IsNativeObjectAlive(other) ||
                std::any_of(bindings.begin(),bindings.end(),[&](const auto& b){return b.renderer==other;}) ||
                !ResourceRelativePath(asset,other,other_path)) continue;
            const auto region=GenericMatching::ClassifyReceiver(other_path);
            if (region==GenericMatching::Region::MobileProxy || region==GenericMatching::Region::DesktopProxy) continue;
            if (IsStaticRenderer(other)!=IsStaticRenderer(renderer) || !SameMeshSpace(other,renderer) ||
                !same_bones(GetRendererBones(other),candidate.bones)) continue;
            GenericRendererCandidate visible;visible.key={adapter.id,candidate.key.resource,other_path,region};
            visible.renderer=other;visible.mesh=GetRendererMesh(other);
            GenericMatching::MeshIdentity original;void* shadow=nullptr;
            if (!ReadGenericPristineMesh(adapter,asset,visible,original) ||
                !ReadNullableObject(Contract("android.shadow_mesh_get"),other,shadow))
                return refuse("potential proxy owner identity unavailable: "+other_path);
            if (identity.mesh==original.mesh || identity.mesh==shadow) ++owners;
        }
        // Kept receivers recover shadows hidden by the preceding appearance.
        const bool restore_proxy=restoring && !changed_owner &&
            ((owners==1) || (!owners && !possibly_affected));
        if (owners==1 && !changed_owner && !restore_proxy) continue;
        if (!owners && !possibly_affected && !restore_proxy) continue;
        if (!restore_proxy && (owners!=1 || !changed_owner))
            return refuse("independent proxy has no unique Mesh/bones/space owner; add an explicit proxy mapping: "+path);
        PreparedBinding proxy;
        proxy.receiver_key=candidate.key;proxy.component_id=prepared[owner].component_id;proxy.renderer=renderer;
        proxy.original_mesh=proxy.custom_mesh=candidate.mesh;proxy.original_materials=proxy.custom_materials=candidate.materials;
        proxy.original_bones=candidate.bones;proxy.original_enabled=candidate.enabled;proxy.custom_enabled=false;
        if (!proxy.original_mesh || !proxy.original_materials || !UseSavedOriginal(asset,proxy,false) || DonorMesh(proxy)!=identity.mesh)
            return refuse("proxy Original binding unavailable: "+path);
        if (restore_proxy) {
            if (!proxy.saved_original) continue;
            proxy.custom_enabled=proxy.donor_enabled;
            prepared.push_back(std::move(proxy));continue;
        }
        // Original Off plus a live independent caster means the receiver was
        // delegating its shadow. The new visible Mesh takes over that caster.
        if (candidate.enabled && prepared[owner].custom_enabled && prepared[owner].custom_shadow==0)
            prepared[owner].custom_shadow=1;
        prepared.push_back(std::move(proxy));
    }
    bindings=std::move(prepared);
    return !g_construction->failed;
}
