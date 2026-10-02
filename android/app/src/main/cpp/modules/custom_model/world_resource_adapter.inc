// Included inside the shared transaction implementation on Android only.
// Android world prefabs omit LOD0. Build from the exact UI LOD0 resource and
// rebind to world LOD1 using live world bones. Compatibility checks follow the
// same developer setting as UI publication; required objects and array bounds
// still have to exist. LOD0/LOD1 geometry and material tables remain distinct.
bool PrepareAndroidWorldResource(const CharacterAdapter& adapter,const BemPocData& bem,
    void* asset,std::vector<PreparedBinding>& bindings,
    std::vector<PreparedBinding>* prepared_ui=nullptr,void** prepared_ui_asset=nullptr) {
    void* handle=nullptr; uint32_t handle_root=0;
    struct DonorLifetime {
        void*& handle; uint32_t& root;
        ~DonorLifetime() { betterendfield::AndroidReleaseUiDonor(handle,root); }
    } lifetime{handle,handle_root};
    void* donor=RootTemporary(betterendfield::AndroidLoadUiDonor(adapter.ui_resource,handle,handle_root));
    if (!donor || (!bem.skip_validation && ObjectName(donor)!=adapter.ui_resource)) { Log("Android UI donor unavailable."); return false; }
    std::vector<PreparedBinding> sources;
    if (!ReadCompletedAndroidDonor(adapter,bem,donor,sources)) {
        sources.clear();
        if (!PrepareResource(adapter,bem,donor,sources)) return false;
        if (prepared_ui && prepared_ui_asset && g_hot_switch_runtime.load()) { *prepared_ui=sources; *prepared_ui_asset=donor; }
    }
    bool inactive=true;
    void* args[]{g_skinned_renderer_class.type_object,&inactive};
    void* renderers=Invoke(Contract("game_object.renderers"),asset,args);
    int count=ArrayLength(renderers);
    BE_ResolvedClassV1 transform_class{};
    if (count<=0 || (!bem.skip_validation && count>4096) || g_host->resolve_class(g_host->context,"UnityEngine.CoreModule.dll",
        "UnityEngine","Transform",&transform_class)!=BE_Result_Ok || !transform_class.type_object) return false;
    void* transform_args[]{transform_class.type_object,&inactive};
    void* transforms=Invoke(Contract("game_object.renderers"),asset,transform_args);
    const int transform_count=ArrayLength(transforms);
    if (transform_count<=0 || (!bem.skip_validation && transform_count>8192)) return false;
    auto relative=[](void* object) {
        auto path=BuildTransformPath(object);
        const auto slash=path.find('/');
        return slash==path.npos?std::string{}:path.substr(slash+1);
    };
    std::unordered_map<std::string,void*> world_bones;
    for (int i=0;i<transform_count;++i) {
        void* bone=ArrayValue(transforms,i); const auto path=relative(bone);
        if (!path.empty() && !world_bones.emplace(path,bone).second && !bem.skip_validation) return false;
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
            if (renderer) {
                if (bem.skip_validation) continue;
                return false;
            }
            renderer=candidate;
        }
        if (!renderer || (!bem.skip_validation && !SameMeshSpace(source.renderer,renderer))) {
            Log("Android world renderer missing or mesh space differs: "+target_name); return false;
        }
        PreparedBinding target;
        target.component_id=source.component_id; target.renderer=renderer;
        target.original_mesh=Invoke(Contract("skinned.get_shared_mesh"),renderer,nullptr);
        target.original_bones=Invoke(Contract("skinned.get_bones"),renderer,nullptr);
        target.original_materials=Invoke(Contract("renderer.get_shared_materials"),renderer,nullptr);
        if (!GetRendererEnabled(renderer,target.original_enabled) || !UseSavedOriginal(asset,target) ||
            !DonorMesh(target) || (!bem.skip_validation && ObjectName(DonorMesh(target))!=target_name)) return false;
        target.custom_enabled=(bem.components[i].info.flags&kComponentFlagHidden)?false:
            (target.saved_original?target.donor_enabled:target.original_enabled);
        target.custom_mesh=source.custom_mesh==DonorMesh(source)?DonorMesh(target):source.custom_mesh;
        if (target.saved_original) target.custom_bones=DonorBones(target);
        // Validate keep overrides against the world material using the same
        // shared-source texture policy used to prepare the UI material clone.
        for (size_t k=0;k<bem.components[i].keep_material_overrides.size();++k) {
            const auto& override=bem.components[i].keep_material_overrides[k];
            if (override.material_slot>=static_cast<uint32_t>(ArrayLength(DonorMaterials(target))) ||
                override.material_slot>=static_cast<uint32_t>(ArrayLength(DonorMaterials(source)))) return false;
            void* world_material=ArrayValue(DonorMaterials(target),override.material_slot);
            void* ui_material=ArrayValue(DonorMaterials(source),override.material_slot);
            if (!world_material || !ui_material || (!bem.skip_validation &&
                (ObjectName(world_material)!=bem.components[i].keep_material_names[k] ||
                ObjectName(ui_material)!=ObjectName(world_material)))) return false;
            const auto pins=ReadMaterialTextureSlots(world_material);
            for (size_t t=0;t<bem.textures.size();++t) if (override.textures&(uint64_t{1}<<t)) {
                const auto match=MatchTexturePins(pins,bem.textures[t].original_name,bem.skip_validation,ObjectName);
                if (!bem.skip_validation && match.status!=TexturePinStatus::Matched) {
                    LogTexturePinFailure(match.status==TexturePinStatus::Missing?
                        "Android world texture pin missing.":"Android world texture pin ambiguous.",
                        world_material,pins,bem.textures[t].original_name);
                    return false;
                }
            }
        }
        target.custom_materials=source.custom_materials;
        if (source.custom_mesh!=DonorMesh(source)) {
            target.change_shadow=true;
            target.custom_shadow=1; // ShadowCastingMode.On, using the new visible mesh.
            if (!InvokeValue(Contract("android.shadow_get"),renderer,nullptr,target.original_shadow)) return false;
            target.original_shadow_mesh=Invoke(Contract("android.shadow_mesh_get"),renderer,nullptr);
        }
        if (source.custom_bones) {
            const int bones=ArrayLength(source.custom_bones);
            target.custom_bones=NewArrayLike(DonorBones(target),bones,bem.skip_validation);
            if (!target.custom_bones) return false;
            const auto& component=bem.components[source.component_id];
            for (int b=0;b<bones;++b) {
                void* ui_bone=ArrayValue(source.custom_bones,b);
                const auto path=relative(ui_bone);
                auto found=world_bones.find(path);
                // BEM 1.2 aliases: the world skeleton may name this bone differently
                // at the same parent path; accept only a declared alias there.
                if (found==world_bones.end() && static_cast<size_t>(b)<component.bone_aliases.size()) {
                    const auto parent=path.rfind('/');
                    for (const auto& alias:component.bone_aliases[b]) {
                        found=world_bones.find(parent==path.npos?alias:path.substr(0,parent+1)+alias);
                        if (found!=world_bones.end()) break;
                    }
                }
                if (found==world_bones.end()) {
                    Log("Android world bone path missing: "+path); return false;
                }
                if (!bem.skip_validation && !component.BoneNameMatches(static_cast<size_t>(b),ObjectName(found->second))) {
                    Log("Android world bone name differs from package: "+path); return false;
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
                target.bone_names.push_back(ObjectName(found->second));
            }
        }
        bindings.push_back(target);
    }
    size_t proxies=0;
    for (const auto& source:sources) {
        const auto& component=bem.components[source.component_id];
        if (source.custom_mesh==DonorMesh(source) && !(component.info.flags&kComponentFlagHidden)) continue;
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
            proxy.original_bones=Invoke(Contract("skinned.get_bones"),renderer,nullptr);
            if (!proxy.original_mesh || !proxy.original_materials ||
                !GetRendererEnabled(renderer,proxy.original_enabled) || !UseSavedOriginal(asset,proxy)) return false;
            proxy.custom_enabled=false;
            bindings.push_back(proxy); ++proxies;
        }
    }
    Log("Android shadow transaction prepared: replacement meshes cast; matching mobile proxies="+std::to_string(proxies));
    Log(std::string("Android UI LOD0 -> world LOD1 prepared (")+ (bem.skip_validation?"unchecked":"validated")+
        ") with live world bones: "+
        std::to_string(bindings.size())+" components");
    return !g_construction->failed;
}
