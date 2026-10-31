// Windows counterpart of the mobile world shadow transaction. Work only inside
// this delivered resource; never scan the scene or mutate Unity during prepare.
bool PrepareWindowsShadows(const CharacterAdapter& adapter,const BemPocData& bem,
    void* asset,std::vector<PreparedBinding>& bindings) {
    const auto refuse=[](const std::string& why) {Log("Windows shadow mapping refused: "+why);return false;};
    if (bindings.size()!=bem.components.size()) return refuse("component transaction is incomplete");
    auto prepared=bindings;
    std::vector<bool> changed(bindings.size(),false);
    bool any_changed=false,restoring=false;
    for (size_t i=0;i<bindings.size();++i) {
        changed[i]=!(bem.components[i].info.flags&kComponentFlagNoGeometry) ||
            (bem.components[i].info.flags&kComponentFlagHidden);
        any_changed|=changed[i];
        if (!changed[i]) restoring|=RestoreSavedShadowState(prepared[i]);
    }
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
        // A visible replacement takes over projection, including when the
        // original renderer delegated casting to a separate reduced proxy.
        if (b.custom_enabled && b.custom_shadow==0) b.custom_shadow=1;
    }
    const auto dedicated_path=[](std::string_view receiver) {
        if (GenericMatching::ClassifyReceiver(receiver)!=GenericMatching::Region::Lod0 &&
            GenericMatching::ClassifyReceiver(receiver)!=GenericMatching::Region::Lod1 &&
            GenericMatching::ClassifyReceiver(receiver)!=GenericMatching::Region::Lod2 &&
            GenericMatching::ClassifyReceiver(receiver)!=GenericMatching::Region::Lod3) return std::string{};
        auto leaf=receiver.substr(receiver.rfind('/')+1);
        if (leaf.size()<5 || leaf.substr(leaf.size()-5,4)!="_lod" || leaf.back()<'0' || leaf.back()>'3') return std::string{};
        return "Shadow_Proxy/SP_Desktop/"+std::string(leaf.substr(0,leaf.size()-5))+"_shadowProxyDesktop";
    };
    const auto compatible_bones=[](void* proxy,void* visible,bool exact) {
        const int count=ArrayLength(proxy),other=ArrayLength(visible);
        if (count<0 || other<0 || (exact && count!=other)) return false;
        for (int i=0;i<count;++i) {
            void* bone=ArrayValue(proxy,i);if (!bone) return false;
            if (exact) {if (bone!=ArrayValue(visible,i)) return false;}
            else {
                bool found=false;for (int j=0;j<other;++j) found|=bone==ArrayValue(visible,j);
                if (!found) return false;
            }
        }
        return true;
    };
    bool inactive=true;void* args[]{ModelRendererType(),&inactive};
    void* renderers=Invoke(Contract("game_object.renderers"),asset,args);
    const int count=ArrayLength(renderers);
    if (!renderers || count<0 || count>4096) return refuse("renderer enumeration unavailable");
    for (int n=0;n<count;++n) {
        void* renderer=ArrayValue(renderers,n);std::string path;
        if (!IsModelRenderer(renderer) || !ResourceRelativePath(asset,renderer,path) ||
            GenericMatching::ClassifyReceiver(path)!=GenericMatching::Region::DesktopProxy ||
            std::any_of(bindings.begin(),bindings.end(),[&](const auto& b){return b.renderer==renderer;})) continue;
        GenericRendererCandidate candidate;
        candidate.key={adapter.id,std::string(ResourceBaseName(ObjectName(asset))),path,
            adapter.explicit_resource?GenericMatching::Region::Explicit:GenericMatching::Region::DesktopProxy};
        candidate.renderer=renderer;candidate.mesh=GetRendererMesh(renderer);candidate.bones=GetRendererBones(renderer);
        candidate.materials=Invoke(Contract("renderer.get_shared_materials"),renderer,nullptr);
        if (!GetRendererEnabled(renderer,candidate.enabled)) return refuse("proxy enabled state unavailable: "+path);
        GenericMatching::MeshIdentity identity;
        if (!ReadGenericPristineMesh(adapter,asset,candidate,identity) || !identity.mesh)
            return refuse("proxy pristine Mesh unavailable: "+path);
        size_t owner=0,owners=0;bool related=false,used_path=false;
        // The exact resource-relative LOD/proxy pair also supports the game
        // authoring a distinct reduced shadow mesh or a subset bone palette.
        // A dedicated proxy may have its own local axes in a live instance.
        // We only toggle its enabled state; no geometry or bindposes cross
        // between these renderers. Mesh space does not constrain exact-path
        // or mesh-identity ownership and cannot prove uniqueness.
        for (int pass=0;pass<2 && !owners;++pass) for (size_t i=0;i<bindings.size();++i) {
            const auto& b=prepared[i];
            const bool link=pass==0?dedicated_path(b.receiver_key.path)==path:
                identity.mesh==DonorMesh(b) || identity.mesh==PristineShadowMesh(b);
            if (!link) continue;
            related|=changed[i] || (b.saved_original && b.saved_original->change_shadow);
            const bool type_ok=IsStaticRenderer(renderer)==IsStaticRenderer(b.renderer);
            const bool bones_ok=compatible_bones(candidate.bones,DonorBones(b),pass!=0);
            if (!type_ok || !bones_ok) continue;
            owner=i;++owners;used_path=pass==0;
        }
        if (!used_path && owners==1) for (int v=0;v<count;++v) {
            void* other=ArrayValue(renderers,v);std::string other_path;
            if (other==renderer || !IsModelRenderer(other) || !IsNativeObjectAlive(other) ||
                std::any_of(bindings.begin(),bindings.end(),[&](const auto& b){return b.renderer==other;}) ||
                !ResourceRelativePath(asset,other,other_path)) continue;
            const auto region=GenericMatching::ClassifyReceiver(other_path);
            if (region==GenericMatching::Region::DesktopProxy || region==GenericMatching::Region::MobileProxy ||
                IsStaticRenderer(other)!=IsStaticRenderer(renderer) ||
                !compatible_bones(candidate.bones,GetRendererBones(other),true)) continue;
            GenericRendererCandidate visible;visible.key={adapter.id,candidate.key.resource,other_path,
                adapter.explicit_resource?GenericMatching::Region::Explicit:region};
            visible.renderer=other;visible.mesh=GetRendererMesh(other);
            GenericMatching::MeshIdentity original;void* shadow=nullptr;
            if (!ReadGenericPristineMesh(adapter,asset,visible,original) ||
                !ReadNullableObject(Contract("android.shadow_mesh_get"),other,shadow))
                return refuse("potential proxy owner identity unavailable: "+other_path);
            if (identity.mesh==original.mesh || identity.mesh==shadow) ++owners;
        }
        if (!owners) {if (related) return refuse("proxy Mesh/bones ownership unavailable: "+path);continue;}
        if (owners!=1) {if (related) return refuse("ambiguous desktop proxy owner: "+path);continue;}
        if (!changed[owner] && !(restoring && prepared[owner].saved_original &&
            prepared[owner].saved_original->change_shadow)) continue;
        PreparedBinding proxy;proxy.receiver_key=candidate.key;proxy.component_id=prepared[owner].component_id;
        proxy.renderer=renderer;proxy.original_mesh=proxy.custom_mesh=candidate.mesh;
        proxy.original_materials=proxy.custom_materials=candidate.materials;proxy.original_bones=candidate.bones;
        proxy.original_enabled=candidate.enabled;proxy.custom_enabled=false;
        if (!proxy.original_mesh || !proxy.original_materials || !UseSavedOriginal(asset,proxy,false) || DonorMesh(proxy)!=identity.mesh)
            return refuse("proxy Original binding unavailable: "+path);
        if (!changed[owner]) {
            if (!proxy.saved_original) continue;
            proxy.custom_enabled=proxy.donor_enabled;
        }
        prepared.push_back(std::move(proxy));
    }
    if (g_construction->failed) return false;
    bindings=std::move(prepared);
    return true;
}
