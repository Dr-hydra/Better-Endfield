// Run the actual first-enable scanner with Android's paired UI donor route.
// The injected loader only replaces the game resource API; pristine identity,
// exact LOD relation, material/bone contracts and queueing are production code.
namespace {
Fake* fake_legacy_ui_donor=nullptr;
size_t legacy_ui_loads=0,legacy_ui_releases=0;
}
void AndroidLegacyDiscoveryTests(const std::filesystem::path& input) {
    const auto path=std::filesystem::absolute(input);
    BE_HostApiV1 host{};host.runtime_invoke=InvokeFake;host.object_unbox=UnboxFake;host.copy_managed_string=StringFake;
    host.object_new=NewFake;host.gchandle_new=TrackedRoot;host.gchandle_free=TrackedFree;
    host.log=[](void*,const char*,const char* message){captured_logs.emplace_back(message?message:"");};g_host=&host;
    ConfigureFakeObjectClasses();g_weak_new=TrackedWeak;
    g_weak_target=[](uint32_t handle)->void* {const auto it=tracked_handles.find(handle);return it==tracked_handles.end()?nullptr:it->second;};
    g_array_new_specific=[](void*,uintptr_t count)->void* {auto* array=Make();array->array.resize(count);return array;};
    for (auto& method:g_methods) {method.method_info=&method;method.resolved=true;}
    g_renderer_class.type_object=Make("RendererType");g_material_class.class_info=Make("MaterialClass");
    g_static_renderer_class.class_info=Make("StaticRendererClass");g_skinned_renderer_class.class_info=Make("SkinnedRendererClass");
    g_mesh_filter_class.type_object=Make("MeshFilterType");
    fake_transform_type=Make("TransformType");
    g_hot_switch_runtime=true;g_enabled=true;g_stopping=false;
    g_registry={};g_registry.hot_switch=true;g_registry_root=path.parent_path();
    g_completed.clear();g_model_targets.clear();g_instance_rebind_queue.clear();g_pending_model_resources.clear();
    g_instance_scan_retry_ms=0;g_instance_rebind_handled=g_model_revision;
    const std::string disabled="[CustomModel]\nhot_switch=true\n";
    const std::string enabled=disabled+"[Mod.legacy]\nenabled=true\npackage="+path.filename().string()+"\n";
    ModRegistry preview;std::string error;
    Check(ParseModRegistry(enabled,g_registry_root,preview,error) && preview.enabled.size()==1,error.c_str());
    const auto& adapter=*preview.enabled.front().adapter;
    Check(!adapter.explicit_resource && !adapter.components.empty(),"paired first-enable fixture must use legacy character metadata");
    std::vector<GenericMatching::ExactLodRelation> relations;
    const GenericMatching::AssetScope scope{"Android","synthetic-legacy-discovery"};
    for (size_t i=0;i<adapter.components.size();++i) {
        uint64_t world_indices=31+i;if (world_indices==adapter.components[i].indices) ++world_indices;
        relations.push_back({scope,adapter.id,adapter.ui_resource,adapter.world_resource,
            "Mesh_all/lod0/part"+std::to_string(i)+"_lod0",adapter.components[i].name,
            "Mesh_all/lod1/part"+std::to_string(i)+"_lod1","mobile_mesh"+std::to_string(i)+"_lod1",
            true,true,world_indices,true});
    }
    ModelAndroidDiscoveryRoute route;
    route.load_ui=[](const char*,void*& handle,uint32_t& handle_root)->void* {
        ++legacy_ui_loads;handle=fake_legacy_ui_donor;handle_root=0;return fake_legacy_ui_donor;
    };
    route.release_ui=[](void*& handle,uint32_t& handle_root){++legacy_ui_releases;handle=nullptr;handle_root=0;};
    route.relations=relations;route.scope=scope;
    legacy_ui_loads=legacy_ui_releases=0;
    struct Receiver {Fake* root;std::vector<Fake*> renderers,originals;};
    auto* scene=Array({});fake_scene_renderers=scene;
    const auto receiver=[&](bool ui,std::string name,const Receiver* source=nullptr) {
        Receiver result{GameObject(std::move(name)),{}, {}};
        auto* mesh_all=Make("Mesh_all");mesh_all->parent=result.root;
        auto* lod=Make(ui?"lod0":"lod1");lod->parent=mesh_all;
        result.root->transforms=Array({mesh_all,lod});
        for (size_t i=0;i<adapter.components.size();++i) {
            auto* renderer=Make("part"+std::to_string(i)+(ui?"_lod0":"_lod1"));renderer->parent=lod;
            renderer->runtime_class=const_cast<void*>(g_skinned_renderer_class.class_info);
            auto* mesh=source?source->originals[i]:Make(ui?adapter.components[i].name:relations[i].target_mesh);
            mesh->index_count=ui?adapter.components[i].indices:static_cast<uint32_t>(relations[i].target_indices);
            mesh->poses=Array({Make()});renderer->mesh=mesh;
            auto* bone=Make("bone"+std::to_string(i));bone->parent=result.root;renderer->bones=Array({bone});
            auto* material=Make("material"+std::to_string(i));material->shader=Make("shader");renderer->materials=Array({material});
            static_cast<Fake*>(result.root->transforms)->array.push_back(bone);
            result.root->array.push_back(renderer);scene->array.push_back(renderer);
            result.renderers.push_back(renderer);result.originals.push_back(mesh);
        }
        return result;
    };
    auto ui=receiver(true,adapter.ui_resource);fake_legacy_ui_donor=ui.root;
    auto world=receiver(false,adapter.world_resource);
    auto clone=receiver(false,std::string(adapter.world_resource)+"(Clone)#27",&world);
    std::vector<Receiver> invalid;
    for (const auto* defect:{"mesh","count","lod","bones","foreign-bone","material","shader","type","dead"}) {
        auto root=receiver(false,std::string(adapter.world_resource)+"(Clone)#"+std::to_string(100+invalid.size()));
        const std::string_view kind=defect;
        if (kind=="mesh") root.originals.front()->name="foreign-mobile-mesh";
        if (kind=="count") ++root.originals.front()->index_count;
        if (kind=="lod") static_cast<Fake*>(root.renderers.front()->parent)->name="lod0";
        if (kind=="bones") root.renderers.front()->bones=Array({});
        if (kind=="foreign-bone") root.renderers.front()->bones=Array({Make("outside-resource")});
        if (kind=="material") root.renderers.front()->materials=Array({});
        if (kind=="shader") static_cast<Fake*>(ArrayValue(root.renderers.front()->materials,0))->shader=Make("dead-shader");
        if (kind=="shader") static_cast<Fake*>(static_cast<Fake*>(ArrayValue(root.renderers.front()->materials,0))->shader)->alive=false;
        if (kind=="type") root.root->runtime_class=Make("TextureClass");
        if (kind=="dead") root.root->alive=false;
        invalid.push_back(std::move(root));
    }
    const auto queued=[&](Fake* root){return std::any_of(g_instance_rebind_queue.begin(),g_instance_rebind_queue.end(),[&](const auto& ref){return ref->Get()==root;});};
    const auto scan=[&]{ConstructionScope construction;return ScanSceneInstancesForRebind(ModelDiscoveryPlatform::Android,route);};
    const auto check_pair=[&](const ModelAndroidDiscoveryRoute& selected,std::string& reason) {
        ConstructionScope construction;
        return HasCompatibleModelTarget(g_registry.enabled.front(),world.root,reason,ModelDiscoveryPlatform::Android,selected);
    };
    const auto builds=fake_mesh_builds;
    Check(g_completed.empty() && g_model_targets.empty(),"legacy first enable already owns a generation");
    Check(InstallRegistryUpdate(enabled),"legacy first-enable registry rejected");
    Check(scan(),"legacy first-enable paired scanner failed");
    Check(queued(world.root) && queued(clone.root) && queued(ui.root) && g_instance_rebind_queue.size()==3,
        "legacy first enable failed to queue exactly cached world/UI and natural clone");
    for (const auto& root:invalid) Check(!queued(root.root),"legacy paired preflight admitted a mismatched/dead/foreign receiver");
    Check(fake_mesh_builds==builds,"legacy discovery constructed a replacement Mesh");
    for (size_t i=0;i<world.renderers.size();++i)
        Check(world.renderers[i]->mesh==world.originals[i] && clone.renderers[i]->mesh==clone.originals[i],
            "legacy discovery published or mutated a cached receiver");
    Check(legacy_ui_loads>0 && legacy_ui_loads==legacy_ui_releases,"paired preflight leaked its UI donor handle");
    // The known world count is independent from the UI LOD0 package count.
    Check(world.originals.front()->index_count!=adapter.components.front().indices,
        "fixture did not distinguish world LOD1 from the UI LOD0 donor");
    std::string reason;
    auto wrong_scope=route;wrong_scope.scope.snapshot="unrelated-snapshot";
    Check(!check_pair(wrong_scope,reason),"legacy discovery borrowed a relation from an unrelated snapshot");
    auto ambiguous_relations=relations;ambiguous_relations.push_back(relations.front());
    auto ambiguous=route;ambiguous.relations=ambiguous_relations;
    Check(!check_pair(ambiguous,reason) && reason=="Android-legacy-ambiguous-LOD-relation",
        "legacy discovery accepted an ambiguous UI/world mapping");
    ++ui.originals.front()->index_count;
    Check(!check_pair(route,reason),"legacy discovery compared only world without validating the paired UI donor");
    --ui.originals.front()->index_count;
    const auto ui_bones=ui.renderers.front()->bones;ui.renderers.front()->bones=Array({Make("foreign-UI-bone")});
    Check(!check_pair(route,reason),"legacy paired preflight accepted UI bones outside the donor resource");
    ui.renderers.front()->bones=ui_bones;
    const auto ui_name=ui.root->name;ui.root->name="wrong-UI-resource";
    Check(!check_pair(route,reason) && reason=="Android-legacy-UI-donor-identity-mismatch",
        "legacy paired preflight accepted a foreign UI donor");ui.root->name=ui_name;
    // A temporarily unavailable donor keeps only that role pending. The UI
    // template already queued on the first pass is not queued a second time.
    g_instance_rebind_queue.clear();
    Check(InstallRegistryUpdate(disabled) && InstallRegistryUpdate(enabled),"legacy retry update rejected");
    fake_legacy_ui_donor=nullptr;
    Check(!scan() && g_pending_model_resources.contains(adapter.world_resource),"missing UI donor lost the pending world discovery");
    Check(queued(ui.root) && !queued(world.root) && !queued(clone.root),"missing paired donor incorrectly queued world receivers");
    fake_legacy_ui_donor=ui.root;
    Check(scan() && g_pending_model_resources.empty() && g_instance_rebind_queue.size()==3,
        "legacy discovery failed to retry when the exact UI donor became available");
    g_instance_rebind_queue.clear();
    // A/B lineage is carried by the real completion/Original bookkeeping.
    // Preflight must read saved Originals, never replacement names/counts.
    const auto publish_previous=[&](const Receiver& root) {
        const auto* mod=g_registry.Match(root.root->name);ConstructionScope construction;
        std::vector<PreparedBinding> bindings;
        for (size_t i=0;i<root.renderers.size();++i) {
            PreparedBinding binding;binding.component_id=static_cast<uint32_t>(i);binding.renderer=root.renderers[i];
            binding.original_mesh=root.originals[i];binding.original_bones=root.renderers[i]->bones;
            binding.original_materials=root.renderers[i]->materials;binding.original_enabled=binding.custom_enabled=true;
            binding.custom_mesh=Make("previous-generation-replacement");binding.custom_bones=binding.original_bones;
            binding.custom_materials=binding.original_materials;
            Check(MakeReceiverKey(*mod->adapter,root.root,binding.renderer,binding.receiver_key),"legacy lineage receiver key failed");
            bindings.push_back(std::move(binding));
        }
        CompletedResource record;
        Check(RememberResource(*mod->adapter,root.root,bindings,record,"previous-selection-A",true),"legacy Original retention failed");
        Check(CommitResource<PreparedBinding>(bindings,ApplyPreparedBinding,RestorePreparedBinding)==CommitResult::Committed,
            "legacy shared binding publication failed");
        PublishCompleted(std::move(record),root.root);construction.published=true;
    };
    publish_previous(ui);publish_previous(world);publish_previous(clone);
    Check(check_pair(route,reason),"legacy paired preflight lost pristine donor identity after an A/B generation");
    g_pending_model_resources.emplace(adapter.world_resource);g_pending_model_resources.emplace(adapter.ui_resource);
    Check(scan() && g_instance_rebind_queue.size()==3 && queued(world.root) && queued(clone.root) && queued(ui.root),
        "legacy A/B generations were not queued through saved Original identity");
    g_instance_rebind_queue.clear();
    g_completed.clear();g_model_targets.clear();g_pending_model_resources.clear();g_original_mesh_pins.clear();
    g_registry={};g_registry_root.clear();g_hot_switch_runtime=false;g_enabled=false;fake_scene_renderers=nullptr;
    fake_transform_type=nullptr;fake_legacy_ui_donor=nullptr;
    g_renderer_class={};g_static_renderer_class={};g_skinned_renderer_class={};g_mesh_filter_class={};g_game_object_class={};
    Check(roots==0 && tracked_handles.empty(),"legacy Android discovery leaked GC handles");
    g_weak_new=nullptr;g_weak_target=nullptr;g_host=nullptr;
    std::cout<<"PASS Android legacy first-enable: actual paired UI LOD0/world LOD1 discovery, cached prefab+clone, strict identity/material/bone gates, no upload, transient donor retry, A/B Original lineage\n";
}
