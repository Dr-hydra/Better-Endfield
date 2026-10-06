// Exercise the production registry and scanner with Android discovery policy.
// The host publication route still rejects Windows LOD1. Android world/shadow
// publication itself is covered by AndroidWorldBindingTests, using its actual
// platform adapter. These package/resource identities are wholly synthetic.
void AndroidDiscoveryTests(const std::filesystem::path& legacy_input,const std::filesystem::path& explicit_input) {
    const auto package=std::filesystem::absolute(explicit_input);
    BE_HostApiV1 host{};host.runtime_invoke=InvokeFake;host.object_unbox=UnboxFake;host.copy_managed_string=StringFake;
    host.object_new=NewFake;host.gchandle_new=TrackedRoot;host.gchandle_free=TrackedFree;
    host.log=[](void*,const char*,const char* message){captured_logs.emplace_back(message?message:"");};g_host=&host;
    ConfigureFakeObjectClasses();g_weak_new=TrackedWeak;
    g_weak_target=[](uint32_t handle)->void* {const auto found=tracked_handles.find(handle);return found==tracked_handles.end()?nullptr:found->second;};
    g_array_new_specific=[](void*,uintptr_t count)->void* {auto* result=Make();result->array.resize(count);return result;};
    fake_transform_type=Make("TransformType");
    host.resolve_class=[](void*,const char*,const char*,const char*,BE_ResolvedClassV1* result)->BE_Result {
        result->class_info=fake_transform_type;result->type_object=fake_transform_type;return BE_Result_Ok;
    };
    for (auto& method:g_methods) {method.method_info=&method;method.resolved=true;}
    g_renderer_class.type_object=Make("RendererType");g_material_class.class_info=Make("MaterialClass");
    g_static_renderer_class.class_info=Make("MeshRendererClass");g_skinned_renderer_class.class_info=Make("SkinnedMeshRendererClass");
    g_mesh_filter_class.type_object=Make("MeshFilterType");
    g_hot_switch_runtime=true;g_enabled=true;g_stopping=false;g_model_clone_coverage=false;
    g_registry={};g_registry.hot_switch=true;g_registry_root=package.parent_path();
    g_completed.clear();g_model_targets.clear();g_instance_rebind_queue.clear();g_pending_model_resources.clear();
    g_instance_rebind_handled=g_model_revision;g_instance_scan_retry_ms=0;g_model_frame_budget=FrameBudget{};
    const std::string disabled="[CustomModel]\nhot_switch=true\n";
    const std::string enabled=disabled+"[Mod.android]\nenabled=true\npackage="+package.filename().string()+"\n";
    ModRegistry preview;std::string error;
    Check(ParseModRegistry(enabled,g_registry_root,preview,error) && preview.enabled.size()==3,error.c_str());
    BemPackageInfo metadata;Check(ReadBemPackageInfo(package,metadata,error),error.c_str());
    for (const auto& resource:metadata.resources)
        Check(resource.lod==1 && std::find(resource.platforms.begin(),resource.platforms.end(),"android-arm64")!=resource.platforms.end(),
            "Android discovery fixture must explicitly declare Android and LOD1");
    struct Receiver {Fake* root;Fake* renderer;Fake* original;std::string resource;};
    auto* scene=Array({});fake_scene_renderers=scene;
    const auto receiver=[&](const EnabledMod& mod,std::string name) {
        Check(mod.adapter->components.size()==1,"Android discovery fixture expects one receiver per explicit resource");
        const auto& identity=mod.adapter->components.front();
        auto* root=GameObject(std::move(name));auto* mesh_all=Make("Mesh_all");mesh_all->parent=root;
        auto* lod=Make("lod1");lod->parent=mesh_all;
        auto* r=Make(identity.name);r->parent=lod;
        r->runtime_class=const_cast<void*>(identity.static_mesh?g_static_renderer_class.class_info:g_skinned_renderer_class.class_info);
        auto* original=Make(identity.name);original->index_count=identity.indices;original->poses=Array({Make()});
        r->materials=Array({Make("material")});
        root->transforms=Array({mesh_all,lod});
        if (identity.static_mesh) {r->filter=Make("filter");static_cast<Fake*>(r->filter)->mesh=original;}
        else {r->mesh=original;auto* bone=Make("root");bone->parent=root;r->bones=Array({bone});static_cast<Fake*>(root->transforms)->array.push_back(bone);}
        root->array.push_back(r);scene->array.push_back(r);
        return Receiver{root,r,original,mod.adapter->world_resource};
    };
    std::vector<Receiver> valid,invalid;
    int serial=100;
    for (const auto& mod:preview.enabled) {
        valid.push_back(receiver(mod,mod.adapter->world_resource));
        valid.push_back(receiver(mod,std::string(mod.adapter->world_resource)+"(Clone)#37"));
        for (const auto* defect:{"mesh","count","path","kind","material","shader","type","dead","bone"}) {
            auto root=receiver(mod,std::string(mod.adapter->world_resource)+"(Clone)#"+std::to_string(serial++));
            const std::string_view kind=defect;
            if (kind=="mesh") root.original->name="unknown-original";
            if (kind=="count") ++root.original->index_count;
            if (kind=="path") static_cast<Fake*>(root.renderer->parent)->name="lod0";
            if (kind=="kind") root.renderer->runtime_class=Make("wrong-renderer-class");
            if (kind=="material") root.renderer->materials=Array({});
            if (kind=="shader") {auto* material=static_cast<Fake*>(ArrayValue(root.renderer->materials,0));material->shader=Make("dead-shader");static_cast<Fake*>(material->shader)->alive=false;}
            if (kind=="type") root.root->runtime_class=Make("SpriteClass");
            if (kind=="dead") root.root->alive=false;
            if (kind=="bone") {
                if (mod.adapter->components.front().static_mesh) root.root->name="unknown-static-resource";
                else root.renderer->bones=Array({Make("root")}); // outside this resource
            }
            invalid.push_back(std::move(root));
        }
        invalid.push_back(receiver(mod,"unknown-resource"));
    }
    std::vector<std::string> failures;
    const auto verify=[&](bool ok,const char* why){if (!ok) failures.emplace_back(why);};
    const auto queued=[&](Fake* root){return std::any_of(g_instance_rebind_queue.begin(),g_instance_rebind_queue.end(),[&](const auto& ref){return ref->Get()==root;});};
    const auto scan=[&](ModelDiscoveryPlatform platform){ConstructionScope scope;return ScanSceneInstancesForRebind(platform);};
    Check(g_completed.empty() && g_model_targets.empty(),"Android first-enable fixture already owns targets");
    Check(InstallRegistryUpdate(enabled),"Android first-enable registry update rejected");
    // The exact same LOD1 package must remain forbidden to the Windows scan.
    verify(scan(ModelDiscoveryPlatform::Windows) && g_instance_rebind_queue.empty(),"Windows discovery admitted Android LOD1 receivers");
    Check(InstallRegistryUpdate(disabled) && InstallRegistryUpdate(enabled),"Android first-enable retry registry rejected");
    verify(scan(ModelDiscoveryPlatform::Android),"Android first-enable scan failed");
    verify(g_instance_rebind_queue.size()==valid.size(),"Android first-enable did not discover exactly the valid cached roots");
    for (const auto& root:valid) verify(queued(root.root),"Android first-enable omitted a static/skinned prefab or natural clone");
    for (const auto& root:invalid) verify(!queued(root.root),"Android discovery admitted a mismatched/dead/foreign receiver");
    for (const auto& root:valid) {
        const auto* mod=g_registry.Match(root.root->name);std::string reason;
        ConstructionScope scope;
        BemPocData payload;
        Check(LoadBem(mod->package,payload,error,mod->appearance,nullptr,mod->skip_validation,mod->loading_optimization,
            mod->parameters,UINT64_MAX,false,mod->resource_id),error.c_str());
        verify(!ValidatePayloadAdapter(*mod->adapter,payload),"Android scanner policy bypassed the Windows LOD0 publication gate");
        const auto saved_class=g_game_object_class;g_game_object_class={};
        verify(!HasCompatibleModelTarget(*mod,root.root,reason,ModelDiscoveryPlatform::Android),"Android discovery ignored missing GameObject class contract");
        g_game_object_class=saved_class;const auto saved_object_class=g_object_class;g_object_class=nullptr;
        verify(!HasCompatibleModelTarget(*mod,root.root,reason,ModelDiscoveryPlatform::Android),"Android discovery ignored missing object class lookup");
        g_object_class=saved_object_class;
        auto inconsistent=*mod->adapter;inconsistent.receiver_lod=0;auto wrong=*mod;wrong.adapter=&inconsistent;
        verify(!HasCompatibleModelTarget(wrong,root.root,reason,ModelDiscoveryPlatform::Android),"Android discovery ignored receiver/resource LOD mismatch");
    }
    // Preserve the established legacy Android route: its world input is LOD1
    // while its package identities describe the UI LOD0 donor.
    ModRegistry legacy;const auto legacy_path=std::filesystem::absolute(legacy_input);
    Check(ParseModRegistry(disabled+"[Mod.legacy]\nenabled=true\npackage="+legacy_path.string()+"\n",legacy_path.parent_path(),legacy,error) &&
        legacy.enabled.size()==1,"legacy discovery fixture rejected");
    {ConstructionScope scope;std::string reason;
        verify(!HasCompatibleModelTarget(legacy.enabled[0],valid.front().root,reason,ModelDiscoveryPlatform::Android) &&
            reason=="Android-legacy-requires-paired-world-route","Android legacy world was routed through explicit own-donor discovery");}
    g_instance_rebind_queue.clear();
    // Model Android publication through the real shared commit/Original
    // bookkeeping after the preflight above. Do not invoke the Windows LOD1
    // upload route merely to imitate an Android game or shadow implementation.
    const auto publish=[&](const Receiver& root) {
        const auto* mod=g_registry.Match(root.root->name);ConstructionScope scope;
        PreparedBinding b;b.component_id=0;b.renderer=root.renderer;
        b.original_mesh=root.original;b.original_materials=root.renderer->materials;b.original_bones=root.renderer->bones;
        b.original_enabled=b.custom_enabled=true;b.custom_mesh=Make("Android synthetic generated");
        b.custom_materials=b.original_materials;b.custom_bones=b.original_bones;
        Check(MakeReceiverKey(*mod->adapter,root.root,b.renderer,b.receiver_key),"Android synthetic receiver identity failed");
        std::vector<PreparedBinding> bindings{b};CompletedResource record;
        Check(RememberResource(*mod->adapter,root.root,bindings,record,mod->selection_key,true),"Android Original retention failed");
        Check(CommitResource<PreparedBinding>(bindings,ApplyPreparedBinding,RestorePreparedBinding)==CommitResult::Committed,
            "Android shared receiver publication failed");
        PublishCompleted(std::move(record),root.root);scope.published=true;
    };
    for (const auto& root:valid) publish(root);
    Check(InstallRegistryUpdate(disabled),"Android disable rejected");
    for (int i=0;i<64;++i) {++fake_frame;ConstructionScope scope;PumpInstanceRebind(fake_frame);}
    for (const auto& root:valid) verify(GetRendererMesh(root.renderer)==root.original,"Android discovered root did not restore Original on disable");
    Check(InstallRegistryUpdate(enabled),"Android re-enable rejected");
    verify(scan(ModelDiscoveryPlatform::Android) && g_instance_rebind_queue.size()==valid.size(),"Android restored roots were not queued on re-enable");
    publish(valid.front());const auto published_mesh=GetRendererMesh(valid.front().renderer);
    Check(InstallRegistryUpdate(disabled) && InstallRegistryUpdate(enabled),"Android queued shutdown revision fixture rejected");
    g_instance_scan_retry_ms=GetTickCount64()+1000;
    ShutdownResourceModule();
    verify(g_instance_rebind_queue.empty() && g_pending_model_resources.empty() && !g_instance_rebind_handled && !g_instance_scan_retry_ms,
        "module shutdown retained old scene roots, pending resource names or rebind generation/retry state");
    verify(GetRendererMesh(valid.front().renderer)==published_mesh,"module shutdown rolled back a published Android receiver");
    g_stopping=false;
    g_registry={};g_registry_root.clear();g_hot_switch_runtime=false;g_enabled=false;fake_scene_renderers=nullptr;fake_transform_type=nullptr;
    g_renderer_class={};g_static_renderer_class={};g_skinned_renderer_class={};g_mesh_filter_class={};g_game_object_class={};
    verify(roots==0 && tracked_handles.empty(),"Android discovery/disable lifecycle leaked GC handles");
    g_weak_new=nullptr;g_weak_target=nullptr;g_host=nullptr;
    for (const auto& failure:failures) std::cerr<<failure<<'\n';
    Check(failures.empty(),"Android explicit discovery regression failed");
    std::cout<<"PASS Android explicit discovery: registry enable -> exact LOD1 static/skinned prefab+clone queue, Windows LOD0 boundary, missing type contracts, strict donor rejection, Original disable/re-enable, actual module shutdown cleanup and unchanged legacy route\n";
}
