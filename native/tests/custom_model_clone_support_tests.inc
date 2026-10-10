// Production capture/commit, Instantiate observation, pump registration and
// Original restoration. The game/GPU are replaced by the existing typed fake.
void CloneSupportTests() {
    BE_HostApiV1 host{};host.runtime_invoke=InvokeFake;host.object_unbox=UnboxFake;host.copy_managed_string=StringFake;
    host.object_new=NewFake;host.gchandle_new=TrackedRoot;host.gchandle_free=TrackedFree;
    host.log=[](void*,const char*,const char* text){captured_logs.emplace_back(text?text:"");};g_host=&host;
    ConfigureFakeObjectClasses();g_weak_new=TrackedWeak;
    g_weak_target=[](uint32_t h)->void* {auto it=tracked_handles.find(h);return it==tracked_handles.end()?nullptr:it->second;};
    g_array_new_specific=[](void*,uintptr_t count)->void* {auto* a=Make();a->array.resize(count);return a;};
    for (auto& method:g_methods) {method.method_info=&method;method.resolved=true;}
    g_renderer_class.type_object=Make("Renderer");g_static_renderer_class.class_info=Make("MeshRenderer");
    g_mesh_filter_class.type_object=Make("MeshFilter");g_material_class.class_info=Make("Material");
    g_skinned_renderer_class.class_info=Make("SkinnedRenderer");
    fake_transform_type=Make("TransformType");
    host.resolve_class=[](void*,const char*,const char*,const char*,BE_ResolvedClassV1* result)->BE_Result {
        result->class_info=fake_transform_type;result->type_object=fake_transform_type;return BE_Result_Ok;
    };
    g_enabled=true;g_stopping=false;g_probe={};g_hot_switch_runtime=false;g_clone_support_runtime=true;
    g_model_clone_coverage=true;g_model_pump_ms=GetTickCount64();
    Check(std::string_view(ModelAsyncDeliveryBlocker(GetTickCount64()))=="experimental-clone-synchronous",
        "clone support unexpectedly enabled async template delivery");
    InitializeModelJobs();
    ModRegistry parsed;std::string error;
    Check(ParseModRegistry("[CustomModel]\nclone_support=true\n",{},parsed,error) && parsed.clone_support && !parsed.hot_switch,
        "clone support is not independently parsed");
    Check(ParseModRegistry("[CustomModel]\n",{},parsed,error) && !parsed.clone_support,"clone support does not default off");
    Check(!ParseModRegistry("[CustomModel]\nclone_support=invalid\n",{},parsed,error),"invalid clone switch accepted");
    g_registry={};
    auto owner=std::make_shared<OwnedCharacterAdapter>();owner->id="wpn_clone";owner->world=owner->ui="wpn_clone_postmodel";
    owner->names={"blade_mesh"};owner->components={{owner->names[0].c_str(),6,"Meshes/blade",true}};
    owner->adapter={owner->id.c_str(),owner->world.c_str(),owner->ui.c_str(),"",false,owner->components,"weapon","assets/wpn_clone.prefab",0,true};
    g_registry.owned_adapters.push_back(owner);g_registry.enabled.push_back({&owner->adapter,{},"",false,true,"A"});
    struct Receiver {Fake* root;Fake* renderer;Fake* filter;Fake* bone;};
    const auto receiver=[&](std::string name,void* mesh,bool skinned=false) {
        auto* root=GameObject(std::move(name));auto* group=Make("Meshes");group->parent=root;
        auto* renderer=Make("blade");renderer->parent=group;root->array={renderer};
        renderer->materials=Array({Make("private-material-1"),Make("private-material-2")});
        auto* filter=Make("filter");renderer->filter=filter;filter->mesh=mesh;renderer->mesh=mesh;
        renderer->runtime_class=const_cast<void*>((skinned?g_skinned_renderer_class:g_static_renderer_class).class_info);
        auto* bone=Make("bone");bone->parent=root;renderer->bones=Array({bone});root->transforms=Array({bone});
        return Receiver{root,renderer,filter,bone};
    };
    auto* original=Make("blade_mesh");auto* output=Make("BEM-output-A");auto source=receiver(owner->world,original);
    BemPocData bem;BemComponent component;component.static_mesh=true;component.info.component_id=0;component.info.original_index_count=6;
    bem.components.push_back(component);
    std::shared_ptr<const ModelCloneLineage> snapshot;
    {
        ConstructionScope scope;std::vector<PreparedBinding> bindings;
        Check(CaptureGenericResourceBindings(owner->adapter,bem,source.root,bindings),"source capture failed");
        bindings[0].custom_mesh=output;bindings[0].custom_materials=Array({Make("output-1"),Make("output-2")});
        CompletedResource record;Check(RememberResource(owner->adapter,source.root,bindings,record,"A"),"source completion failed");
        Check(record.bindings[0].original!=nullptr,"clone support without hot switch lost Original");
        Check(CommitResource<PreparedBinding>(bindings,ApplyPreparedBinding,RestorePreparedBinding)==CommitResult::Committed,"source commit failed");
        g_completed.push_back(std::move(record));WatchModelCloneSource(source.root);
        snapshot=CaptureModelCloneLineage(source.root);Check(bool(snapshot),"source lineage not captured");
    }
    auto clone=receiver(owner->world+"(Clone)#1",output),nested=receiver(owner->world+"(Clone)#2",output);
    void* private_materials=clone.renderer->materials;
    NoteModelClone(source.root,clone.root);NoteModelClone(clone.root,nested.root);
    Check(g_model_deliveries.size()==2 && g_model_deliveries[0].clone_lineage && g_model_deliveries[1].clone_lineage,
        "clone-of-clone before pump lost generation provenance");
    {
        ConstructionScope scope;PumpModelJobs(1);
        if (!IsCompletedResource(owner->adapter,clone.root,"A") || !IsCompletedResource(owner->adapter,nested.root,"A"))
            for (const auto& line:captured_logs) std::cerr<<line<<"\n";
        Check(IsCompletedResource(owner->adapter,clone.root,"A") && IsCompletedResource(owner->adapter,nested.root,"A"),
            "observed clone/private material copies were not adopted by pump");
        Check(clone.renderer->materials==private_materials && clone.filter->mesh==output,"inheritance rewrote instance material state");
        Check(g_model_jobs.empty(),"already inherited clone was rebuilt");
        clone.renderer->enabled=false;PumpModelJobs(2);
        Check(g_model_jobs.empty() && !clone.renderer->enabled,"game visibility change caused a synchronous-mode clone rebuild");
        clone.renderer->enabled=true;
        std::vector<PreparedBinding> restore;
        Check(PrepareDisabledResource(clone.root,&owner->adapter,restore),"clone Original preparation failed");
        Check(CommitResource<PreparedBinding>(restore,ApplyPreparedBinding,RestorePreparedBinding)==CommitResult::Committed && clone.filter->mesh==original,
            "clone disable did not restore recorded Original");
        std::vector<PreparedBinding> again;
        Check(CaptureGenericResourceBindings(owner->adapter,bem,clone.root,again),"restored clone was refused on redelivery");
    }
    {
        ConstructionScope scope;std::string why;
        auto unknown=receiver(owner->world+"(Clone)#3",Make("blade_mesh"));
        Check(!AdoptObservedModelClone(unknown.root,g_registry.enabled[0],*snapshot,why) && why.find("neither")!=why.npos,
            "same-name unknown Mesh acquired clone provenance");
        Check(unknown.filter->mesh!=original,"clone refusal mutated displayed Mesh");
        auto reset=receiver(owner->world+"(Clone)#4",original);
        Check(AdoptObservedModelClone(reset.root,g_registry.enabled[0],*snapshot,why) &&
            !IsCompletedResource(owner->adapter,reset.root,"A"),"reset clone was incorrectly marked as completed output");
        std::vector<PreparedBinding> bindings;
        Check(CaptureGenericResourceBindings(owner->adapter,bem,reset.root,bindings),"reset clone could not re-prepare");
        auto bad_indices=receiver(owner->world+"(Clone)#5",output);original->index_count=7;
        Check(!AdoptObservedModelClone(bad_indices.root,g_registry.enabled[0],*snapshot,why),"clone bypassed Original index validation");
        original->index_count=6;
        auto wrong_root=receiver("other_resource(Clone)",output);
        Check(!AdoptObservedModelClone(wrong_root.root,g_registry.enabled[0],*snapshot,why),"clone source bypassed resource identity");
        g_clone_support_runtime=false;
        Check(!AdoptObservedModelClone(bad_indices.root,g_registry.enabled[0],*snapshot,why),"disabled clone support still adopted instances");
        g_clone_support_runtime=true;
        // A queued clone remains tied to A even when its source now shows B.
        auto old_clone=receiver(owner->world+"(Clone)#6",output);
        NoteModelClone(source.root,old_clone.root);
        Check(g_model_deliveries.size()==1 && g_model_deliveries[0].clone_lineage->selection_key=="A","queued generation changed");
        source.filter->mesh=Make("BEM-output-B");g_registry.enabled[0].selection_key="B";
        Check(AdoptObservedModelClone(old_clone.root,g_registry.enabled[0],*g_model_deliveries[0].clone_lineage,why) &&
            IsCompletedResource(owner->adapter,old_clone.root,"A") && !IsCompletedResource(owner->adapter,old_clone.root,"B"),
            "old clone was mislabeled as the current package generation");
        g_model_deliveries.clear();
    }
    // Skinned clones use their own bone palette, including on restoration.
    {
        ConstructionScope scope;auto skin_owner=std::make_shared<OwnedCharacterAdapter>();
        skin_owner->id="chr_clone";skin_owner->world=skin_owner->ui="chr_clone_postmodel";
        skin_owner->names={"blade_mesh"};skin_owner->components={{skin_owner->names[0].c_str(),6,"Meshes/blade",false}};
        skin_owner->adapter={skin_owner->id.c_str(),skin_owner->world.c_str(),skin_owner->ui.c_str(),"",false,skin_owner->components,"normal","assets/chr_clone.prefab",0,true};
        g_registry.owned_adapters.push_back(skin_owner);g_registry.enabled.push_back({&skin_owner->adapter,{},"",false,true,"skin"});
        auto skin_source=receiver(skin_owner->world,original,true),skin_clone=receiver(skin_owner->world+"(Clone)",output,true);
        auto skin_bem=bem;skin_bem.components[0].static_mesh=false;
        std::vector<PreparedBinding> bindings;
        Check(CaptureGenericResourceBindings(skin_owner->adapter,skin_bem,skin_source.root,bindings),"skinned source capture failed");
        bindings[0].custom_mesh=output;bindings[0].custom_materials=skin_source.renderer->materials;bindings[0].custom_bones=skin_source.renderer->bones;
        CompletedResource record;Check(RememberResource(skin_owner->adapter,skin_source.root,bindings,record,"skin"),"skinned Original capture failed");
        Check(CommitResource<PreparedBinding>(bindings,ApplyPreparedBinding,RestorePreparedBinding)==CommitResult::Committed,"skinned commit failed");
        g_completed.push_back(std::move(record));auto skin_snapshot=CaptureModelCloneLineage(skin_source.root);
        Check(bool(skin_snapshot),"skinned provenance capture failed");std::string why;
        Check(AdoptObservedModelClone(skin_clone.root,g_registry.enabled.back(),*skin_snapshot,why),"skinned clone adoption failed");
        const auto& saved=g_completed.back().bindings[0].original;
        Check(ArrayValue(saved->bones.Get(),0)==skin_clone.bone && ArrayValue(saved->bones.Get(),0)!=skin_source.bone,
            "skinned clone borrowed template Transform");
        auto missing=receiver(skin_owner->world+"(Clone)#7",output,true);missing.root->transforms=Array({});
        Check(!AdoptObservedModelClone(missing.root,g_registry.enabled.back(),*skin_snapshot,why),"missing Original bone path accepted");
        auto ambiguous=receiver(skin_owner->world+"(Clone)#8",output,true);auto* duplicate=Make("bone");duplicate->parent=ambiguous.root;
        static_cast<Fake*>(ambiguous.root->transforms)->array.push_back(duplicate);
        Check(!AdoptObservedModelClone(ambiguous.root,g_registry.enabled.back(),*skin_snapshot,why),"ambiguous Original bone path accepted");
    }
    snapshot.reset();DisableModelCloneHooks();g_model_targets.clear();g_model_jobs.clear();g_model_deliveries.clear();
    g_completed.clear();g_original_mesh_pins.clear();g_model_loader.reset();g_texture_streamer.reset();
    g_registry={};g_enabled=false;g_clone_support_runtime=false;g_hot_switch_runtime=false;g_model_pump_ms=0;
    Check(roots==0 && tracked_handles.empty(),"clone support leaked GC handles");
    g_renderer_class={};g_static_renderer_class={};g_mesh_filter_class={};g_skinned_renderer_class={};
    fake_transform_type=nullptr;g_weak_new=nullptr;g_weak_target=nullptr;g_host=nullptr;
    std::cout<<"PASS experimental clones: synchronous delivery, hot-switch-off Original, private materials, nested observation, pump adoption, reset, restoration, generations and bone isolation/refusal\n";
}
