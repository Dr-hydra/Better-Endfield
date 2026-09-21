// Included inside the shared transaction implementation on Android only.
// Android world prefabs omit LOD0. Build from the exact UI LOD0 resource and
// rebind to world LOD1 only after matching renderer space and every bone path
// and rest matrix. This never equates LOD0/LOD1 geometry or material tables.
bool PrepareAndroidWorldResource(const CharacterAdapter& adapter,const BemPocData& bem,
    void* asset,std::vector<PreparedBinding>& bindings) {
    void* handle=nullptr; uint32_t handle_root=0;
    struct DonorLifetime {
        void*& handle; uint32_t& root;
        ~DonorLifetime() { betterendfield::AndroidReleaseUiDonor(handle,root); }
    } lifetime{handle,handle_root};
    void* donor=RootTemporary(betterendfield::AndroidLoadUiDonor(adapter.ui_resource,handle,handle_root));
    if (!donor || ObjectName(donor)!=adapter.ui_resource) { Log("Android UI donor unavailable."); return false; }
    std::vector<PreparedBinding> sources;
    if (!ReadCompletedAndroidDonor(adapter,bem,donor,sources)) {
        sources.clear();
        if (!PrepareResource(adapter,bem,donor,sources)) return false;
    }
    bool inactive=true;
    void* args[]{g_skinned_renderer_class.type_object,&inactive};
    void* renderers=Invoke(Contract("game_object.renderers"),asset,args);
    int count=ArrayLength(renderers);
    BE_ResolvedClassV1 transform_class{};
    if (count<=0 || count>4096 || g_host->resolve_class(g_host->context,"UnityEngine.CoreModule.dll",
        "UnityEngine","Transform",&transform_class)!=BE_Result_Ok || !transform_class.type_object) return false;
    void* transform_args[]{transform_class.type_object,&inactive};
    void* transforms=Invoke(Contract("game_object.renderers"),asset,transform_args);
    const int transform_count=ArrayLength(transforms);
    if (transform_count<=0 || transform_count>8192) return false;
    auto relative=[](void* object) {
        auto path=BuildTransformPath(object);
        const auto slash=path.find('/');
        return slash==path.npos?std::string{}:path.substr(slash+1);
    };
    std::unordered_map<std::string,void*> world_bones;
    for (int i=0;i<transform_count;++i) {
        void* bone=ArrayValue(transforms,i); const auto path=relative(bone);
        if (!path.empty() && !world_bones.emplace(path,bone).second) return false;
    }
    for (size_t i=0;i<sources.size();++i) {
        auto& source=sources[i];
        std::string target_name=adapter.components[source.component_id].name;
        if (!target_name.ends_with("_lod0")) return false;
        target_name.back()='1';
        void* renderer=nullptr;
        for (int j=0;j<count;++j) {
            void* candidate=ArrayValue(renderers,j);
            if (ObjectName(candidate)!=target_name || relative(candidate)!=std::string("Mesh_all/lod1/")+target_name) continue;
            if (renderer) return false;
            renderer=candidate;
        }
        if (!renderer || !SameMeshSpace(source.renderer,renderer)) {
            Log("Android world renderer missing or mesh space differs: "+target_name); return false;
        }
        PreparedBinding target;
        target.component_id=source.component_id; target.renderer=renderer;
        target.original_mesh=Invoke(Contract("skinned.get_shared_mesh"),renderer,nullptr);
        target.original_bones=Invoke(Contract("skinned.get_bones"),renderer,nullptr);
        target.original_materials=Invoke(Contract("renderer.get_shared_materials"),renderer,nullptr);
        if (!target.original_mesh || ObjectName(target.original_mesh)!=target_name ||
            !GetRendererEnabled(renderer,target.original_enabled)) return false;
        target.custom_enabled=(bem.components[i].info.flags&kComponentFlagHidden)?false:target.original_enabled;
        target.custom_mesh=source.custom_mesh==source.original_mesh?target.original_mesh:source.custom_mesh;
        target.custom_materials=source.custom_materials;
        target.bone_names=source.bone_names;
        if (source.custom_mesh!=source.original_mesh) {
            target.change_shadow=true;
            target.custom_shadow=1; // ShadowCastingMode.On, using the new visible mesh.
            if (!InvokeValue(Contract("android.shadow_get"),renderer,nullptr,target.original_shadow)) return false;
            target.original_shadow_mesh=Invoke(Contract("android.shadow_mesh_get"),renderer,nullptr);
        }
        if (source.custom_bones) {
            const int bones=ArrayLength(source.custom_bones);
            target.custom_bones=NewArrayLike(target.original_bones,bones);
            if (!target.custom_bones) return false;
            for (int b=0;b<bones;++b) {
                void* ui_bone=ArrayValue(source.custom_bones,b);
                const auto path=relative(ui_bone);
                auto found=world_bones.find(path);
                if (found==world_bones.end() || ObjectName(ui_bone)!=ObjectName(found->second)) {
                    Log("Android world bone path missing: "+path); return false;
                }
                Matrix4x4Raw ui_matrix{},world_matrix{};
                if (!InvokeValue(Contract("transform.local_to_world"),ui_bone,nullptr,ui_matrix) ||
                    !InvokeValue(Contract("transform.local_to_world"),found->second,nullptr,world_matrix)) return false;
                bool rest_match=true;
                for (int n=0;n<16;++n) if (!std::isfinite(ui_matrix.m[n]) || !std::isfinite(world_matrix.m[n]) ||
                    std::abs(ui_matrix.m[n]-world_matrix.m[n])>0.0001f) { rest_match=false; break; }
                if (!rest_match) {
                    // Some mobile world prefabs author the same named skeleton
                    // under a different rest pose. The path/name contract is
                    // still exact, so use the world's live bones and leave the
                    // visual deformation to the owner's runtime validation.
                    Log("Android world bone rest space differs; matched path retained: "+path);
                }
                if (!SetArrayValue(target.custom_bones,b,found->second)) return false;
            }
        }
        bindings.push_back(target);
    }
    size_t proxies=0;
    for (const auto& source:sources) {
        const auto& component=bem.components[source.component_id];
        if (source.custom_mesh==source.original_mesh && !(component.info.flags&kComponentFlagHidden)) continue;
        std::string stem=adapter.components[source.component_id].name;
        stem.resize(stem.size()-5); // Exact adapter name has the _lod0 suffix.
        const auto proxy_name=stem+"_shadowProxyMobile";
        for (int j=0;j<count;++j) {
            void* renderer=ArrayValue(renderers,j);
            if (ObjectName(renderer)!=proxy_name ||
                relative(renderer)!=std::string("Shadow_Proxy/SP_Mobile/")+proxy_name) continue;
            PreparedBinding proxy;
            proxy.component_id=source.component_id; proxy.renderer=renderer;
            proxy.original_mesh=proxy.custom_mesh=Invoke(Contract("skinned.get_shared_mesh"),renderer,nullptr);
            proxy.original_materials=proxy.custom_materials=Invoke(Contract("renderer.get_shared_materials"),renderer,nullptr);
            if (!proxy.original_mesh || !proxy.original_materials ||
                !GetRendererEnabled(renderer,proxy.original_enabled)) return false;
            proxy.custom_enabled=false;
            bindings.push_back(proxy); ++proxies;
        }
    }
    Log("Android shadow transaction prepared: replacement meshes cast; matching mobile proxies="+std::to_string(proxies));
    Log("Android UI LOD0 -> world LOD1 prepared with exact bone paths/rest matrices: "+
        std::to_string(bindings.size())+" components");
    return !g_construction->failed;
}
