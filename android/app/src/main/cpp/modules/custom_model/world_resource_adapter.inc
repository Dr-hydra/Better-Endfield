// Included inside the shared transaction implementation on Android only.
// Android world prefabs omit LOD0. Build from the exact UI LOD0 resource and
// rebind to world LOD1 using live world bones. Compatibility checks follow the
// same developer setting as UI publication; required objects and array bounds
// still have to exist. LOD0/LOD1 geometry and material tables remain distinct.
bool PrepareAndroidWorldResource(const CharacterAdapter& adapter,const BemPocData& bem,
    void* asset,std::vector<PreparedBinding>& bindings,
    std::vector<PreparedBinding>* prepared_ui=nullptr,void** prepared_ui_asset=nullptr,
    std::span<const GenericMatching::ExactLodRelation> lod_relations={},
    const GenericMatching::AssetScope& asset_scope={}) {
    auto refuse=[](const std::string& reason) {
        Log("Android world adapter refused: "+reason); return false;
    };
    void* handle=nullptr; uint32_t handle_root=0;
    struct DonorLifetime {
        void*& handle; uint32_t& root;
        ~DonorLifetime() { betterendfieldnext::AndroidReleaseUiDonor(handle,root); }
    } lifetime{handle,handle_root};
    void* donor=RootTemporary(betterendfieldnext::AndroidLoadUiDonor(adapter.ui_resource,handle,handle_root));
    if (!donor || (!bem.skip_validation && ObjectName(donor)!=adapter.ui_resource)) { Log("Android UI donor unavailable."); return false; }
    std::vector<PreparedBinding> sources;
    const bool committed_ui=ReadCompletedAndroidDonor(adapter,bem,donor,sources);
    if (!committed_ui) {
        sources.clear();
        if (!PrepareResource(adapter,bem,donor,sources)) return refuse("UI donor preparation failed");
        // Publish this freshly built donor with world in the caller's paired
        // transaction, including normal mode. Otherwise ensure_ui rebuilds it.
    }
    if (sources.size()!=bem.components.size()) return refuse("UI component mapping incomplete");
    // Compare UI/world receiver placement relative to their own resource roots.
    // A scene instance (hot switch rebind) carries its world pose; templates
    // and the frame-sliced Job already compare root-relative.
    struct MeshSpaceRoots {
        std::array<void*,2> previous=g_verified_mesh_space_roots;
        MeshSpaceRoots(void* ui,void* world) {g_verified_mesh_space_roots={ui,world};}
        ~MeshSpaceRoots() {g_verified_mesh_space_roots=previous;}
    } mesh_space_roots{donor,asset};
    bool inactive=true;
    void* args[]{g_skinned_renderer_class.type_object,&inactive};
    void* renderers=Invoke(Contract("game_object.renderers"),asset,args);
    int count=ArrayLength(renderers);
    BE_ResolvedClassV1 transform_class{};
    if (count<=0 || (!bem.skip_validation && count>4096) || g_host->resolve_class(g_host->context,"UnityEngine.CoreModule.dll",
        "UnityEngine","Transform",&transform_class)!=BE_Result_Ok || !transform_class.type_object)
        return refuse("renderer/Transform enumeration unavailable; renderers="+std::to_string(count));
    void* transform_args[]{transform_class.type_object,&inactive};
    void* transforms=Invoke(Contract("game_object.renderers"),asset,transform_args);
    const int transform_count=ArrayLength(transforms);
    if (transform_count<=0 || (!bem.skip_validation && transform_count>8192))
        return refuse("world Transform count="+std::to_string(transform_count));
    std::vector<GenericRendererCandidate> candidates;
    if (!BuildGenericRendererIndex(adapter,asset,renderers,candidates))
        return refuse("invalid world receiver root/path index");
    std::unordered_map<std::string,void*> world_bones;
    for (int i=0;i<transform_count;++i) {
        void* bone=ArrayValue(transforms,i); std::string path;
        if (!ResourceRelativePath(asset,bone,path)) return refuse("world Transform outside resource root");
        if (!path.empty() && !world_bones.emplace(path,bone).second)
            return refuse("duplicate world Transform path: "+path);
    }
    std::vector<void*> visible_pristine_meshes;
    std::vector<const GenericRendererCandidate*> lod_candidates;
    std::vector<GenericMatching::Candidate> lod_match_candidates;
    std::vector<std::string> expected_proxy_paths;
    std::vector<bool> changes_shadow;
    std::vector<void*> used_receivers;
    // Retain original references for every visible LOD1 receiver, including
    // parts absent from this BEM. Shared-owner ambiguity cannot be hidden by
    // selecting only the parts being replaced.
    for (const auto& candidate:candidates) if (candidate.key.region==GenericMatching::Region::Lod1) {
        GenericMatching::MeshIdentity identity;
        if (!ReadGenericPristineMesh(adapter,asset,candidate,identity) || !identity.mesh ||
            (identity.origin!=GenericMatching::DonorOrigin::Pristine &&
             identity.origin!=GenericMatching::DonorOrigin::SavedOriginal))
            return refuse("world pristine Mesh unavailable: "+candidate.key.path+
                (identity.detail.empty()?std::string{}:" ("+identity.detail+")"));
        visible_pristine_meshes.push_back(identity.mesh);
        lod_candidates.push_back(&candidate);
        lod_match_candidates.push_back({candidate.key,candidate.renderer,std::move(identity)});
    }
    std::vector<PreparedBinding> targets;
    std::vector<size_t> target_visible_indices;
    for (size_t i=0;i<sources.size();++i) {
        auto& source=sources[i];
        const auto component_it=std::find_if(bem.components.begin(),bem.components.end(),[&](const auto& c) {
            return c.info.component_id==source.component_id;
        });
        if (source.component_id>=adapter.components.size() || component_it==bem.components.end())
            return refuse("invalid UI component identity");
        const auto& component=*component_it;
        std::string source_path;
        GenericMatching::LodCounterpart relation;
        if (!ResourceRelativePath(donor,source.renderer,source_path))
            return refuse("UI receiver outside resource root");
        const auto relation_status=GenericMatching::FindExactAndroidLod1Relation(lod_relations,asset_scope,
            adapter.id,adapter.ui_resource,adapter.world_resource,source_path,
            adapter.components[source.component_id].name,relation);
        if (relation_status==GenericMatching::RelationStatus::Ambiguous)
            return refuse("ambiguous exact Android LOD relation: "+source_path);
        bool fuzzy_relation=false;
        if (relation_status!=GenericMatching::RelationStatus::Matched && bem.skip_validation) {
            const auto fuzzy=GenericMatching::SelectBoundedAndroidLod1(lod_match_candidates,
                {adapter.id,adapter.world_resource,source_path,adapter.components[source.component_id].name},
                [&](const auto& candidate) {
                    auto it=std::find_if(lod_candidates.begin(),lod_candidates.end(),
                        [&](const auto* value) { return value->renderer==candidate.renderer; });
                    return it!=lod_candidates.end() && (*it)->materials && (*it)->bones &&
                        ArrayLength((*it)->materials)>0 && ArrayLength((*it)->bones)>0;
                });
            if (fuzzy.status==GenericMatching::MatchStatus::Ambiguous ||
                fuzzy.status==GenericMatching::MatchStatus::InvalidIndex)
                return refuse("ambiguous or invalid bounded Android LOD fallback: "+source_path);
            if (fuzzy.status==GenericMatching::MatchStatus::Matched) {
                const auto& selected=lod_match_candidates[fuzzy.index];
                relation.target_path=selected.key.path;
                relation.target_renderer=selected.key.path.substr(selected.key.path.rfind('/')+1);
                relation.target_mesh=selected.pristine.name;
                relation.target_indices=selected.pristine.indices;
                relation.target_indices_known=selected.pristine.indices_known;
                fuzzy_relation=true;
                Log("Android bounded LOD fallback accepted: source="+source_path+
                    " target="+relation.target_path+" mesh="+relation.target_mesh+
                    " reason=exact relation missing; same component parent/LOD and _8/_20-canonical identity");
            } else if (fuzzy.status==GenericMatching::MatchStatus::Missing) {
                Log("Android bounded LOD fallback missing: source="+source_path+
                    " reason=no unique live candidate; retaining legacy unchecked route");
            }
        }
        if (!fuzzy_relation && relation_status!=GenericMatching::RelationStatus::Matched &&
            !GenericMatching::AndroidLod1Counterpart(source_path,relation))
            return refuse("unsupported UI receiver path: "+source_path);
        const auto& target_name=relation.target_renderer;
        const GenericRendererCandidate* matched=nullptr;
        size_t visible_index=0,matched_visible_index=0;
        for (const auto& candidate:candidates) {
            if (candidate.key.region!=GenericMatching::Region::Lod1) continue;
            if (candidate.key.path==relation.target_path) {
                if (matched) return refuse("duplicate world renderer: "+relation.target_path);
                matched=&candidate; matched_visible_index=visible_index;
            }
            ++visible_index;
        }
        void* renderer=matched?matched->renderer:nullptr;
        if (!renderer) { Log("Android world renderer missing: "+target_name); return false; }
        if (!bem.skip_validation && !SameMeshSpace(source.renderer,renderer)) {
            LogMeshSpaceDifference(source.renderer,renderer);
            // A scene instance was cloned from a template whose mesh space was
            // verified at commit; skinning uses bones and bindposes, not the
            // renderer Transform. Its generated-Mesh lineage is checked below.
            if (!g_instance_rebind_active) { Log("Android world mesh space differs: "+target_name); return false; }
        }
        used_receivers.push_back(renderer);
        if (!GenericMatching::DistinctReceivers(used_receivers)) return refuse("components share a world receiver");
        GenericMatching::MeshIdentity world_identity;
        if (!ReadGenericPristineMesh(adapter,asset,*matched,world_identity) || !world_identity.mesh ||
            (world_identity.origin!=GenericMatching::DonorOrigin::Pristine &&
            world_identity.origin!=GenericMatching::DonorOrigin::SavedOriginal) ||
            world_identity.mesh!=visible_pristine_meshes[matched_visible_index])
            return refuse("world original identity unavailable: "+relation.target_path+
                (world_identity.detail.empty()?std::string{}:" ("+world_identity.detail+")"));
        PreparedBinding target;
        target.component_id=source.component_id; target.renderer=renderer;
        target.original_mesh=matched->mesh; target.original_bones=matched->bones;
        target.original_materials=matched->materials; target.original_enabled=matched->enabled;
        // World Original materials are not pinned. A world switch needs them
        // only to validate keep overrides; materials come from the UI donor.
        if (!UseSavedOriginal(asset,target,false) || !DonorMesh(target) || !DonorMaterials(target) ||
            !DonorBones(target) || DonorMesh(target)!=world_identity.mesh)
            return refuse("world renderer original binding unavailable: "+target_name);
        if (target.saved_original && !target.donor_materials && !component.keep_material_overrides.empty())
            return refuse("world Original materials were unloaded; keep material overrides cannot be validated: "+target_name);
        if (!bem.skip_validation && (world_identity.name!=relation.target_mesh ||
            ObjectName(DonorMesh(target))!=world_identity.name || !world_identity.indices_known ||
            (relation.target_indices_known && relation.target_indices!=world_identity.indices)))
            return refuse("world mesh identity differs: expected="+relation.target_mesh+" actual="+world_identity.name);
        target.custom_enabled=(component.info.flags&kComponentFlagHidden)?false:
            (target.saved_original?target.donor_enabled:target.original_enabled);
        const bool generated_mesh=!(component.info.flags&kComponentFlagNoGeometry);
        target.custom_mesh=generated_mesh?source.custom_mesh:DonorMesh(target);
        if (!target.custom_mesh) return refuse("UI custom Mesh unavailable: "+source_path);
        if (target.saved_original) target.custom_bones=DonorBones(target);
        // Validate keep overrides against the world material using the same
        // shared-source texture policy used to prepare the UI material clone.
        for (size_t k=0;k<component.keep_material_overrides.size();++k) {
            const auto& override=component.keep_material_overrides[k];
            if (k>=component.keep_material_names.size()) return refuse("keep material identity missing: "+target_name);
            if (override.material_slot>=static_cast<uint32_t>(ArrayLength(DonorMaterials(target))) ||
                override.material_slot>=static_cast<uint32_t>(ArrayLength(DonorMaterials(source))))
                return refuse("keep material slot outside world/UI arrays: "+target_name+" slot="+std::to_string(override.material_slot));
            void* world_material=ArrayValue(DonorMaterials(target),override.material_slot);
            void* ui_material=ArrayValue(DonorMaterials(source),override.material_slot);
            const auto ui_name=ObjectName(ui_material),world_name=ObjectName(world_material);
            // Android's lower-LOD face material is a native counterpart of the
            // UI material, not a different character/material slot. The copied
            // UI shader and its texture pins were already validated above.
            constexpr std::string_view ui_prefix="M_actor_",world_prefix="M_actor_lod_";
            const bool world_lod_variant=ui_name.starts_with(ui_prefix) && world_name.starts_with(world_prefix) &&
                std::string_view(ui_name).substr(ui_prefix.size())==std::string_view(world_name).substr(world_prefix.size());
            if (!world_material || !ui_material || (!bem.skip_validation &&
                (ui_name!=component.keep_material_names[k] ||
                (ui_name!=world_name && !world_lod_variant))))
                return refuse("keep material identity differs: "+target_name+" expected="+component.keep_material_names[k]+
                    " ui="+ui_name+" world="+world_name);
            if (world_lod_variant) {
                // The old lower-LOD shader may omit properties from the copied
                // UI shader. Its texture table does not constrain that copy.
                Log("Android world keep material LOD counterpart accepted: "+ui_name+" -> "+world_name);
                continue;
            }
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
        if (!target.custom_materials) return refuse("UI custom materials unavailable: "+source_path);
        if (generated_mesh) {
            target.change_shadow=true;
            target.custom_shadow=1; // ShadowCastingMode.On, using the new visible mesh.
            if (!InvokeValue(Contract("android.shadow_get"),renderer,nullptr,target.original_shadow))
                return refuse("world shadow mode unavailable: "+target_name);
            target.original_shadow_mesh=Invoke(Contract("android.shadow_mesh_get"),renderer,nullptr);
        }
        if (source.custom_bones) {
            const int bones=ArrayLength(source.custom_bones);
            target.custom_bones=NewArrayLike(DonorBones(target),bones,bem.skip_validation);
            if (!target.custom_bones) return refuse("world bone array allocation rejected: "+target_name+" bones="+std::to_string(bones));
            for (int b=0;b<bones;++b) {
                void* ui_bone=ArrayValue(source.custom_bones,b);
                std::string path;
                if (!ResourceRelativePath(donor,ui_bone,path)) return refuse("UI bone outside resource root");
                auto found=world_bones.find(path);
                // BEM 1.2 aliases: the world skeleton may name this bone differently
                // at the same parent path; accept only a declared alias there.
                if (found==world_bones.end() && static_cast<size_t>(b)<component.bone_aliases.size()) {
                    const auto parent=path.rfind('/');
                    auto alias_match=world_bones.end();
                    for (const auto& alias:component.bone_aliases[b]) {
                        const auto candidate=world_bones.find(parent==path.npos?alias:path.substr(0,parent+1)+alias);
                        if (candidate==world_bones.end()) continue;
                        if (alias_match!=world_bones.end() && alias_match->second!=candidate->second)
                            return refuse("ambiguous declared world bone aliases: "+path);
                        alias_match=candidate;
                    }
                    found=alias_match;
                }
                if (found==world_bones.end()) {
                    Log("Android world bone path missing: "+path); return false;
                }
                if (!bem.skip_validation && !component.BoneNameMatchesForResource(static_cast<size_t>(b),ObjectName(found->second),0)) {
                    Log("Android world bone name differs from package: "+path); return false;
                }
                Matrix4x4Raw ui_matrix{},world_matrix{};
                if (!InvokeValue(Contract("transform.local_to_world"),ui_bone,nullptr,ui_matrix) ||
                    !InvokeValue(Contract("transform.local_to_world"),found->second,nullptr,world_matrix))
                    return refuse("bone matrix unavailable: "+path);
                bool rest_match=true;
                for (int n=0;n<16;++n) {
                    if (!std::isfinite(ui_matrix.m[n]) || !std::isfinite(world_matrix.m[n]))
                        return refuse("nonfinite bone matrix: "+path);
                    if (std::abs(ui_matrix.m[n]-world_matrix.m[n])>0.0001f) rest_match=false;
                }
                if (!rest_match) {
                    // Some mobile world prefabs author the same named skeleton
                    // under a different rest pose. The path/name contract is
                    // still exact, so use the world's live bones and leave the
                    // visual deformation to the owner's runtime validation.
                    Log("Android world bone rest space differs; matched path retained: "+path);
                }
                if (!SetArrayValue(target.custom_bones,b,found->second)) return refuse("world bone assignment failed: "+path);
                target.bone_names.push_back(ObjectName(found->second));
            }
        }
        targets.push_back(target); target_visible_indices.push_back(matched_visible_index);
        changes_shadow.push_back(generated_mesh || (component.info.flags&kComponentFlagHidden));
        std::string expected_proxy;
        if (relation.target_renderer.ends_with("_lod1")) {
            auto stem=relation.target_renderer; stem.resize(stem.size()-5);
            expected_proxy="Shadow_Proxy/SP_Mobile/"+stem+"_shadowProxyMobile";
        }
        expected_proxy_paths.push_back(std::move(expected_proxy));
    }
    size_t proxies=0;
    for (const auto& candidate:candidates) {
        if (std::none_of(changes_shadow.begin(),changes_shadow.end(),[](bool changed) {return changed;})) break;
        if (candidate.key.region!=GenericMatching::Region::MobileProxy) continue;
        GenericMatching::MeshIdentity identity;
        if (!ReadGenericPristineMesh(adapter,asset,candidate,identity) || !identity.mesh ||
            (identity.origin!=GenericMatching::DonorOrigin::Pristine &&
             identity.origin!=GenericMatching::DonorOrigin::SavedOriginal))
            return refuse("proxy pristine Mesh unavailable: "+candidate.key.path+
                (identity.detail.empty()?std::string{}:" ("+identity.detail+")"));
        const auto owner=GenericMatching::FindProxyOwner(identity.mesh,visible_pristine_meshes);
        if (owner.status==GenericMatching::ProxyOwnerStatus::Ambiguous) {
            bool changed_owner=false;
            for (size_t i=0;i<sources.size();++i)
                if (changes_shadow[i] && visible_pristine_meshes[target_visible_indices[i]]==identity.mesh) changed_owner=true;
            if (changed_owner) return refuse("ambiguous mobile proxy owner: "+candidate.key.path);
            continue;
        }
        // SP_Mobile proxies normally carry their own reduced Mesh, so they share
        // no Mesh with the visible LOD. The exact dedicated path is the relation
        // the shipped (pre-generic) adapter used; keep it for that case.
        size_t dedicated_owner=sources.size();
        if (owner.status==GenericMatching::ProxyOwnerStatus::Missing) {
            for (size_t i=0;i<expected_proxy_paths.size();++i) {
                if (!changes_shadow[i] || expected_proxy_paths[i].empty() || candidate.key.path!=expected_proxy_paths[i]) continue;
                if (dedicated_owner!=sources.size()) return refuse("ambiguous dedicated mobile proxy: "+candidate.key.path);
                dedicated_owner=i;
            }
            if (dedicated_owner==sources.size()) continue;
        }
        for (size_t i=0;i<sources.size();++i) {
            if (!changes_shadow[i]) continue;
            if (dedicated_owner!=sources.size() ? i!=dedicated_owner : target_visible_indices[i]!=owner.index) continue;
            PreparedBinding proxy;
            proxy.component_id=targets[i].component_id; proxy.renderer=candidate.renderer;
            proxy.original_mesh=proxy.custom_mesh=candidate.mesh;
            proxy.original_materials=proxy.custom_materials=candidate.materials;
            proxy.original_bones=candidate.bones; proxy.original_enabled=candidate.enabled;
            if (!proxy.original_mesh || !proxy.original_materials ||
                !UseSavedOriginal(asset,proxy,false) || DonorMesh(proxy)!=identity.mesh)
                return refuse("shadow proxy original binding unavailable: "+candidate.key.path);
            proxy.custom_enabled=false;
            targets.push_back(proxy); ++proxies;
            break;
        }
    }
    if (g_construction->failed) return refuse("temporary object rooting failed");
    bindings.insert(bindings.end(),targets.begin(),targets.end());
    if (!committed_ui && prepared_ui && prepared_ui_asset) { *prepared_ui=sources; *prepared_ui_asset=donor; }
    Log("Android shadow transaction prepared: replacement meshes cast; matching mobile proxies="+std::to_string(proxies));
    Log(std::string("Android UI LOD0 -> world LOD1 prepared (")+ (bem.skip_validation?"unchecked":"validated")+
        ") with live world bones: "+
        std::to_string(bindings.size())+" components");
    return true;
}
