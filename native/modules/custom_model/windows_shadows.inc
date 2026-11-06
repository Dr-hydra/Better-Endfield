// Windows counterpart of the mobile world shadow transaction. Work only inside
// this delivered resource; never scan the scene or mutate Unity during prepare.
// A decline only means "this resource keeps the game's original projection";
// the caller still publishes the geometry/material replacement. This function
// assigns `bindings` only on success, so every early return leaves the
// replacement fully prepared and the shadow state untouched.
bool PrepareWindowsShadows(const CharacterAdapter& adapter,const BemPocData& bem,
    void* asset,std::vector<PreparedBinding>& bindings) {
    const auto decline=[](const std::string& why) {Log("Windows shadow takeover declined: "+why);return false;};
    if (bindings.size()!=bem.components.size()) return decline("component transaction is incomplete");
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
    if (!AndroidShadowContractsAvailable()) return decline("shadow getter/setter contract unavailable");
    for (size_t i=0;i<bindings.size();++i) {
        auto& b=prepared[i];
        if (!InvokeValue(Contract("android.shadow_get"),b.renderer,nullptr,b.original_shadow) ||
            b.original_shadow<0 || b.original_shadow>3 ||
            !ReadNullableObject(Contract("android.shadow_mesh_get"),b.renderer,b.original_shadow_mesh))
            return decline("unreadable shadow state: "+b.receiver_key.path);
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
    // `small` must be a pointer-wise slice of `large`. Used only for pass 0,
    // where the canonical SP_Desktop path is already a unique 1:1 owner proof.
    const auto palette_contained=[](void* small,void* large) {
        const int n=ArrayLength(small),m=ArrayLength(large);
        if (n<0 || m<0 || n>m) return false;
        for (int i=0;i<n;++i) {
            void* bone=ArrayValue(small,i);if (!bone) return false;
            bool found=false;for (int j=0;j<m;++j) found|=bone==ArrayValue(large,j);
            if (!found) return false;
        }
        return true;
    };
    const auto compatible_bones=[&palette_contained](void* proxy,void* visible,bool exact) {
        const int count=ArrayLength(proxy),other=ArrayLength(visible);
        if (count<0 || other<0) return false;
        if (exact) {
            // Mesh-identity ownership has no canonical path: keep the full
            // length/order/pointer proof and the competing-owner scan.
            if (count!=other) return false;
            for (int i=0;i<count;++i) if (!ArrayValue(proxy,i) || ArrayValue(proxy,i)!=ArrayValue(visible,i)) return false;
            return true;
        }
        // Projection meshes are authored with their own bone palette slice of
        // the SAME live skeleton. It can be smaller than the visible LOD0
        // palette (most parts) or larger: yvonne cloth_04 uses 26 bones vs 24,
        // typhoea body_01 uses 39 vs 35, with the extra bones being spine /
        // cloth joints that also appear on other renderers of the same asset.
        // Only `enabled` crosses between these renderers - no geometry,
        // materials or bindposes - so order is irrelevant on this path.
        if (!count) return true;   // an empty palette cannot disprove the path
        if (!other) return false;  // no shared bone proves one skeleton
        for (int i=0;i<count;++i) if (!ArrayValue(proxy,i)) return false;
        // One palette must contain the other. A disjoint foreign skeleton
        // fails in both directions; a partial overlap fails too.
        return count<=other?palette_contained(proxy,visible):palette_contained(visible,proxy);
    };
    bool inactive=true;void* args[]{ModelRendererType(),&inactive};
    void* renderers=Invoke(Contract("game_object.renderers"),asset,args);
    const int count=ArrayLength(renderers);
    if (!renderers || count<0 || count>4096) return decline("renderer enumeration unavailable");
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
        if (!GetRendererEnabled(renderer,candidate.enabled)) return decline("proxy enabled state unavailable: "+path);
        GenericMatching::MeshIdentity identity;
        if (!ReadGenericPristineMesh(adapter,asset,candidate,identity) || !identity.mesh)
            return decline("proxy pristine Mesh unavailable: "+path);
        size_t owner=0,owners=0;bool related=false,used_path=false;
        // Pass 0 pairs the canonical resource-relative LOD/proxy path 1:1,
        // which is itself the uniqueness proof (no competing-owner scan is
        // needed, see `if (!used_path && owners==1)` below). Bones only prove
        // that both renderers address one live skeleton; the game may author
        // a distinct reduced mesh with a smaller OR larger palette slice.
        // Pass 1 (mesh identity) has no path proof and keeps exact bones plus
        // the competing-owner scan. A dedicated proxy may have its own local
        // axes in a live instance. Only `enabled` crosses between these
        // renderers; mesh space cannot prove uniqueness on either pass.
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
                return decline("potential proxy owner identity unavailable: "+other_path);
            if (identity.mesh==original.mesh || identity.mesh==shadow) ++owners;
        }
        if (!owners) {if (related) return decline("proxy Mesh/bones ownership unavailable: "+path);continue;}
        if (owners!=1) {if (related) return decline("ambiguous desktop proxy owner: "+path);continue;}
        if (!changed[owner] && !(restoring && prepared[owner].saved_original &&
            prepared[owner].saved_original->change_shadow)) continue;
        PreparedBinding proxy;proxy.receiver_key=candidate.key;proxy.component_id=prepared[owner].component_id;
        proxy.renderer=renderer;proxy.original_mesh=proxy.custom_mesh=candidate.mesh;
        proxy.original_materials=proxy.custom_materials=candidate.materials;proxy.original_bones=candidate.bones;
        proxy.original_enabled=candidate.enabled;proxy.custom_enabled=false;
        if (!proxy.original_mesh || !proxy.original_materials || !UseSavedOriginal(asset,proxy,false) || DonorMesh(proxy)!=identity.mesh)
            return decline("proxy Original binding unavailable: "+path);
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
