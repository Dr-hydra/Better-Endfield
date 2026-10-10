// Real production delivery/commit/restore on ordinary and wrapped receivers.
// One BEM selection is shared; every instance keeps its own original state.
void NpcRootTests() {
    BE_HostApiV1 host{};host.runtime_invoke=InvokeFake;host.object_unbox=UnboxFake;host.copy_managed_string=StringFake;
    host.object_new=NewFake;host.gchandle_new=TrackedRoot;host.gchandle_free=TrackedFree;g_host=&host;
    host.log=[](void*,const char*,const char* message){captured_logs.emplace_back(message?message:"");};
    ConfigureFakeObjectClasses();g_weak_new=TrackedWeak;
    fake_transform_type=Make("TransformType");
    host.resolve_class=[](void*,const char*,const char*,const char*,BE_ResolvedClassV1* result)->BE_Result {
        result->class_info=fake_transform_type;result->type_object=fake_transform_type;return BE_Result_Ok;
    };
    g_weak_target=[](uint32_t h)->void* {auto it=tracked_handles.find(h);return it==tracked_handles.end()?nullptr:it->second;};
    g_array_new_specific=[](void*,uintptr_t count)->void* {auto* a=Make();a->array.resize(count);return a;};
    for (auto& method:g_methods) {method.method_info=&method;method.resolved=true;}
    g_renderer_class.type_object=Make("RendererType");g_material_class.class_info=Make("MaterialClass");
    g_skinned_renderer_class.class_info=Make("SkinnedRenderer");g_static_renderer_class.class_info=Make("StaticRenderer");
    g_static_get=[](const void* field,void* value){std::memcpy(value,fake_static_fields[field].data(),8);};
    g_static_set=[](const void* field,void* value){std::memcpy(fake_static_fields[field].data(),value,8);};
    for (auto& field:g_lod.fields) field.resolved.field_info=&field;
    g_original_finish=[](void*,void* asset,void*) {++finish_calls;last_finished_asset=asset;};
    g_enabled=true;g_stopping=false;g_hot_switch_runtime=true;g_probe.active=false;g_probe.sweep=false;
    g_model_clone_coverage=false;g_registry={};
    auto owner=std::make_shared<OwnedCharacterAdapter>();owner->id="chr_0013_aglina";
    owner->world="chr_0013_aglina_postmodel";owner->ui="chr_0013_aglina_uimodel";
    owner->names={"body_lod0"};owner->components={{owner->names[0].c_str(),6,"",false}};
    owner->adapter={owner->id.c_str(),owner->world.c_str(),owner->ui.c_str(),"",false,owner->components};
    g_registry.owned_adapters={owner};
    EnabledMod mod{&owner->adapter,"npc-root-fixture.bem","default",false,false,"same-selection",""};
    g_registry.enabled={mod};
    auto payload=std::make_shared<BemPocData>();BemComponent component;
    component.info.component_id=0;component.info.original_index_count=6;
    component.info.flags=kComponentFlagNoGeometry|kComponentFlagHidden;payload->components.push_back(component);
    g_payload_cache.push_back({mod.package,mod.appearance,mod.selection_key,payload,0,GetTickCount64()+60000});
    auto* original=Make("body_lod0");original->poses=Array({Make()});
    struct Receiver {Fake *delivered,*model,*renderer,*materials;};
    const auto receiver=[&](bool wrapped,bool clone,uint32_t indices=6) {
        auto* delivered=GameObject(owner->world+(clone?"(Clone)":""));
        auto* model=wrapped?GameObject(owner->world):delivered;if(wrapped) model->parent=delivered;
        auto* group=Make("Mesh_all");group->parent=model;auto* lod=Make("lod0");lod->parent=group;
        auto* renderer=Make("body");renderer->parent=lod;renderer->runtime_class=const_cast<void*>(g_skinned_renderer_class.class_info);
        renderer->mesh=indices==6?original:Make("body_lod0");static_cast<Fake*>(renderer->mesh)->index_count=indices;
        auto* bone=Make("bone");bone->parent=model;renderer->bones=Array({bone});
        model->transforms=Array({group,lod,bone});
        renderer->materials=Array({Make("material")});model->array={renderer};delivered->array={renderer};
        return Receiver{delivered,model,renderer,static_cast<Fake*>(renderer->materials)};
    };
    auto party=receiver(false,false),npc=receiver(true,false),clone=receiver(true,true);
    for (auto r:{party,npc,clone}) {
        const auto before=finish_calls;ResourceFinish(nullptr,r.delivered,nullptr);
        Check(finish_calls==before+1 && last_finished_asset==r.delivered,"NPC delivery changed the original prefab or called the game twice");
        Check(!r.renderer->enabled,"same BEM selection did not commit to a party/NPC/clone receiver");
        Check(r.renderer->mesh==original,"NPC hide changed a shared original mesh");
        Check(std::any_of(g_completed.begin(),g_completed.end(),[&](const auto& c){return c.root.Is(r.model);}),"completed resource used the NPC wrapper instead of actual model root");
    }
    AsyncBemLoader::Backend backend;
    backend.plan=[](const BemRequest&,BemLoadPlan& plan,std::string&){plan.reservation_bytes=1024;return true;};
    backend.load=[payload](const BemRequest&,uint64_t,BemPocData& out,BemLoadStats&,std::string&){out=*payload;return true;};
    g_model_loader=std::make_unique<AsyncBemLoader>(AsyncBemLoader::Config{},std::move(backend));
    g_instance_rebind_handled=g_model_revision;
    {ConstructionScope scope;PumpModelJobs(++fake_frame);}
    for (auto r:{party,npc,clone})
        Check(std::any_of(g_model_targets.begin(),g_model_targets.end(),[&](const auto& target){return target.root.Is(r.model);}),
            "delivery pump registered a wrapper instead of its actual model receiver");
    Check(std::none_of(g_model_targets.begin(),g_model_targets.end(),[&](const auto& target){return target.root.Is(npc.delivered) || target.root.Is(clone.delivered);}),
        "delivery pump retained an NPC wrapper as a replacement target");
    {ConstructionScope scope;CancelModelJobs();}
    g_model_loader->Shutdown();g_model_loader.reset();
    auto bad=receiver(true,false,9);ResourceFinish(nullptr,bad.delivered,nullptr);
    Check(bad.renderer->enabled && bad.renderer->materials==bad.materials,"NPC wrapper bypassed original index validation");
    auto ambiguous=receiver(true,false);auto* second=GameObject(owner->world);second->parent=ambiguous.delivered;
    ResourceFinish(nullptr,ambiguous.delivered,nullptr);
    Check(ambiguous.renderer->enabled,"ambiguous NPC wrapper modified a receiver");
    for (auto& method:g_methods) if (std::string_view(method.key)=="transform.get_child") method.resolved=false;
    auto unavailable=receiver(true,false);ResourceFinish(nullptr,unavailable.delivered,nullptr);
    Check(unavailable.renderer->enabled,"missing wrapper contracts modified a receiver");
    for (auto& method:g_methods) if (std::string_view(method.key)=="transform.get_child") method.resolved=true;
    owner->adapter.explicit_resource=true;
    {ConstructionScope scope;Check(ResolveLegacyModelReceiverRoot(npc.delivered)==npc.delivered,"explicit resource root was rewritten");}
    owner->adapter.explicit_resource=false;
    g_registry.enabled.clear();
    g_instance_rebind_active=true;
    for (auto r:{party,npc,clone}) {
        ConstructionScope scope;
        const bool restored=ProcessResource(r.delivered,scope);
        if (!restored) for (const auto& message:captured_logs) std::cerr<<message<<'\n';
        Check(restored,"NPC disable failed to restore through the wrapper");
        Check(r.renderer->enabled && SameMaterialSet(r.renderer->materials,r.materials) && r.renderer->mesh==original,"NPC original state was not restored");
    }
    g_instance_rebind_active=false;
    {ConstructionScope scope;CancelModelJobs();g_lod.Restore();}
    g_completed.clear();g_payload_cache.clear();g_original_mesh_pins.clear();g_model_targets.clear();g_model_deliveries.clear();
    g_registry={};g_enabled=false;g_hot_switch_runtime=false;g_static_get=nullptr;g_static_set=nullptr;
    for (auto& field:g_lod.fields) field.resolved.field_info=nullptr;
    Check(roots==0 && tracked_handles.empty(),"NPC root tests leaked ownership handles");
    g_weak_new=nullptr;g_weak_target=nullptr;g_host=nullptr;
    std::cout<<"PASS same BEM party/NPC/clone delivery, original-prefab handoff, donor refusal, ambiguity, optional contracts, explicit-root isolation and disable restoration\n";
}
