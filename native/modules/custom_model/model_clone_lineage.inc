// Clone provenance is captured on the frame pump/synchronous delivery, before
// the game clones a watched source. Hook callbacks only enqueue that immutable
// generation; a later template hot switch must not change its Original donor.
struct ModelCloneLineageBinding {
    GenericMatching::ReceiverKey key;
    int32_t output_mesh_id=0;
    uint32_t component_id=0;
    std::string original_name;
    uint64_t original_indices=0;
    bool static_mesh=false;
    bool check_shadow=false;
    std::shared_ptr<SavedOriginalBinding> original;
};
struct ModelCloneLineage {
    const CharacterAdapter* adapter=nullptr;
    std::shared_ptr<OwnedCharacterAdapter> owner;
    std::string resource,selection_key;
    int32_t receiver_id=0;
    std::vector<ModelCloneLineageBinding> bindings;
};
int32_t ModelInstanceId(void* object);
std::shared_ptr<const ModelCloneLineage> CaptureModelCloneLineage(void* delivered) {
    void* asset=ResolveLegacyModelReceiverRoot(delivered);
    if (!asset || !IsNativeObjectAlive(asset)) return {};
    const auto* mod=g_registry.Match(ObjectName(asset));
    if (!mod) return {};
    std::vector<GenericRendererCandidate> index;
    bool inactive=true;void* args[]{ModelRendererType(),&inactive};
    if (!BuildGenericRendererIndex(*mod->adapter,asset,Invoke(Contract("game_object.renderers"),asset,args),index)) return {};
    const CompletedResource* completed=nullptr;
    for (auto it=g_completed.rbegin();it!=g_completed.rend();++it)
        if (SameAdapter(*it->adapter,*mod->adapter) && it->root.Is(asset)) {completed=&*it;break;}
    if (!completed || completed->bindings.empty()) return {};
    auto lineage=std::make_shared<ModelCloneLineage>();
    lineage->adapter=completed->adapter;lineage->owner=completed->adapter_owner;lineage->receiver_id=ModelInstanceId(asset);
    lineage->selection_key=completed->selection_key;lineage->resource=ResourceBaseName(ObjectName(asset));
    for (const auto& old:completed->bindings) {
        const auto found=std::find_if(index.begin(),index.end(),[&](const auto& c){return c.key==old.receiver_key;});
        if (found==index.end() || !old.original || !old.mesh.Is(found->mesh) || !old.mesh.instance_id ||
            !old.original->ResolveMesh(found->mesh)) return {};
        GenericMatching::MeshIdentity identity;
        if (!ReadGenericMeshIdentity(old.original->ResolveMesh(found->mesh),GenericMatching::DonorOrigin::SavedOriginal,identity)) return {};
        lineage->bindings.push_back({old.receiver_key,old.mesh.instance_id,old.component_id,identity.name,identity.indices,IsStaticRenderer(found->renderer),
#if defined(__ANDROID__)
            old.check_shadow,
#else
            false,
#endif
            old.original});
    }
    return lineage;
}
bool AdoptObservedModelClone(void* asset,const EnabledMod& mod,const ModelCloneLineage& lineage,std::string& why) {
    const auto refuse=[&](const char* text){why=text;return false;};
    if (!g_clone_support_runtime.load()) return refuse("clone support disabled");
    if (!lineage.adapter || !SameAdapter(*lineage.adapter,*mod.adapter) ||
        ResourceBaseName(ObjectName(asset))!=lineage.resource) return refuse("source resource/adapter differs");
    // A receiver's own record always wins. Never overwrite its Original with a
    // foreign template or turn an externally changed binding into pristine.
    for (const auto& old:g_completed)
        if (SameAdapter(*old.adapter,*mod.adapter) && old.root.Is(asset)) return true;
    if (g_completed.size()>=4096) return refuse("completed instance limit");
    std::vector<GenericRendererCandidate> index;
    bool inactive=true;void* args[]{ModelRendererType(),&inactive};
    if (!BuildGenericRendererIndex(*mod.adapter,asset,Invoke(Contract("game_object.renderers"),asset,args),index))
        return refuse("receiver paths unavailable/ambiguous");
    CompletedResource record;record.adapter=lineage.adapter;record.adapter_owner=lineage.owner;
    record.selection_key=lineage.selection_key;
    if (!record.root.Set(asset)) return refuse("clone identity unavailable");
    bool all_output=true;
    for (const auto& source:lineage.bindings) {
        const auto found=std::find_if(index.begin(),index.end(),[&](const auto& c){return c.key==source.key;});
        if (found==index.end() || !source.original) return refuse("source component/Original unavailable");
        const auto& candidate=*found;
        if (const char* error=GenericCandidateContractError(asset,candidate,source.static_mesh)) return refuse(error);
        const bool output=ModelInstanceId(candidate.mesh)==source.output_mesh_id;
        if (!output && !source.original->mesh.Is(candidate.mesh)) return refuse("clone Mesh is neither source output nor saved Original");
        all_output&=output;
        void* original_mesh=source.original->ResolveMesh(candidate.mesh);
        if (!original_mesh || !IsNativeObjectAlive(original_mesh)) return refuse("saved Original Mesh released");
        CompletedBinding binding;binding.receiver_key=candidate.key;binding.renderer_name=ObjectName(candidate.renderer);
        if (source.component_id>=mod.adapter->components.size() || source.component_id>=lineage.adapter->components.size())
            return refuse("Original component identity differs");
        const auto& component=mod.adapter->components[source.component_id];
        const auto& declared=lineage.adapter->components[source.component_id];
        if (std::string_view(component.name)!=declared.name || component.indices!=declared.indices ||
            component.static_mesh!=declared.static_mesh || std::string_view(component.receiver_path)!=declared.receiver_path)
            return refuse("source component declaration changed");
        GenericMatching::MeshIdentity identity;
        if (!ReadGenericMeshIdentity(original_mesh,GenericMatching::DonorOrigin::SavedOriginal,identity) ||
            identity.name!=source.original_name || identity.indices!=source.original_indices)
            return refuse("saved Original Mesh identity changed");
#if defined(__ANDROID__)
        // Legacy world LOD1 was validated through the UI LOD0 -> world adapter
        // when the source committed. Its exact saved Original is a world donor,
        // so applying the UI component's LOD0 name/count would reject every clone.
        const bool paired_world=!mod.adapter->explicit_resource && lineage.resource==mod.adapter->world_resource;
#else
        const bool paired_world=false;
#endif
        if (!paired_world && (identity.name!=component.name || (!mod.skip_validation && identity.indices!=component.indices)))
            return refuse("Original Mesh name/index count differs");
        binding.component_id=source.component_id;
        binding.enabled=candidate.enabled;binding.generated_mesh=!source.original->mesh.Is(candidate.mesh);
        if (!binding.mesh.Set(candidate.mesh)) return refuse("clone Mesh identity unavailable");
        for (int i=0;i<ArrayLength(candidate.materials);++i) {
            WeakObject material;
            if (!material.Set(ArrayValue(candidate.materials,i))) return refuse("clone material identity unavailable");
            binding.materials.push_back(std::move(material));
        }
        binding.generated_materials=binding.materials.size()!=source.original->materials.size();
        for (size_t i=0;!binding.generated_materials && i<binding.materials.size();++i)
            binding.generated_materials=!source.original->materials[i].Is(ArrayValue(candidate.materials,static_cast<int>(i)));
        binding.original=std::make_shared<SavedOriginalBinding>();binding.original->CopyIdentity(*source.original);
        if (!source.static_mesh) {
            // Every displayed bone and every Original palette entry belongs to
            // this clone. Template Transform objects are never borrowed.
            for (int i=0;i<ArrayLength(candidate.bones);++i) {
                void* bone=ArrayValue(candidate.bones,i);std::string path;WeakObject weak;
                if (!ResourceRelativePath(asset,bone,path) || !weak.Set(bone)) return refuse("clone bone identity unavailable");
                binding.bone_names.push_back(ObjectName(bone));binding.bone_paths.push_back(std::move(path));
                binding.bones.push_back(std::move(weak));
            }
            void* original_bones=candidate.bones;
            if (!source.original->bone_paths.empty()) {
                BE_ResolvedClassV1 transform{};
                if (!g_host->resolve_class || g_host->resolve_class(g_host->context,"UnityEngine.CoreModule.dll",
                    "UnityEngine","Transform",&transform)!=BE_Result_Ok || !transform.type_object)
                    return refuse("Transform type unavailable");
                void* parameters[]{transform.type_object,&inactive};
                void* transforms=Invoke(Contract("game_object.renderers"),asset,parameters);
                const int count=ArrayLength(transforms);
                if (count<0 || count>8192) return refuse("clone Transform count invalid");
                const std::unordered_set<std::string> wanted(source.original->bone_paths.begin(),source.original->bone_paths.end());
                std::unordered_map<std::string,void*> paths;
                for (int i=0;i<count;++i) {
                    void* bone=ArrayValue(transforms,i);std::string path;
                    if (!bone || !IsNativeObjectAlive(bone) || !ResourceRelativePath(asset,bone,path) || !wanted.contains(path)) continue;
                    if (!paths.emplace(path,bone).second) return refuse("Original bone path ambiguous");
                }
                original_bones=NewArrayLike(candidate.bones,static_cast<int>(source.original->bone_paths.size()));
                if (!original_bones) return refuse("clone Original palette allocation failed");
                for (size_t i=0;i<source.original->bone_paths.size();++i) {
                    const auto found=paths.find(source.original->bone_paths[i]);
                    if (found==paths.end() || !SetArrayValue(original_bones,static_cast<int>(i),found->second))
                        return refuse("Original bone path unavailable");
                }
            }
            if (!binding.original->bones.Set(original_bones)) return refuse("clone Original palette capture failed");
        }
#if defined(__ANDROID__)
        binding.check_shadow=source.check_shadow;
        if (source.check_shadow) {
            void* shadow_mesh=nullptr;
            if (!InvokeValue(Contract("android.shadow_get"),candidate.renderer,nullptr,binding.shadow) ||
                !ReadNullableObject(Contract("android.shadow_mesh_get"),candidate.renderer,shadow_mesh) ||
                !binding.shadow_mesh.Set(shadow_mesh)) return refuse("clone shadow identity unavailable");
        }
        if (!binding.original->shadow_mesh.Set(source.original->shadow_mesh.Get())) return refuse("Original shadow unavailable");
#endif
        record.bindings.push_back(std::move(binding));
    }
    if (record.bindings.empty()) return refuse("source has no model bindings");
    // A reset Mesh needs a rebuild; recording it as the current package would
    // incorrectly make IsCompletedResource return success on the bare clone.
    if (!all_output) record.selection_key.clear();
    g_completed.push_back(std::move(record));
    Log("Experimental clone lineage accepted: "+ObjectName(asset)+" generation="+lineage.selection_key+
        " components="+std::to_string(lineage.bindings.size())+(all_output?" inherited":" re-prepare"));
    return true;
}
