// First activation differs from rebinding a previously replaced generation:
// these cached templates and natural clones have never had a completed record.
// Registry parsing, discovery, compatibility, preparation and publication are
// production code. Only Unity objects and the mesh upload are replaced by fakes.
void FirstEnableTests(const std::filesystem::path& legacy_path,const std::filesystem::path& resources_path) {
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
    g_static_get=[](const void* field,void* value){std::memcpy(value,fake_static_fields[field].data(),8);};
    g_static_set=[](const void* field,void* value){std::memcpy(fake_static_fields[field].data(),value,8);};
    for (auto& field:g_lod.fields) field.resolved.field_info=&field;
    g_model_build_mesh=[](const BemComponent& component,void* donor,void*& mesh,void* poses)->bool {
        ++fake_mesh_builds;fake_build_donors.push_back(donor);
        auto* built=static_cast<Fake*>(NewAsset(g_material_class.class_info));if (!built) return false;
        built->name="first-enable-generated-"+std::to_string(fake_mesh_builds);built->poses=poses;
        built->index_count=component.info.index_count;mesh=built;return true;
    };
    g_original_finish=[](void*,void* asset,void*) {++finish_calls;last_finished_asset=asset;};
    g_hot_switch_runtime=true;g_enabled=true;g_stopping=false;g_probe.active=false;g_probe.sweep=false;
    g_model_clone_coverage=false;g_model_clone_hooks_requested=false;
    std::vector<std::string> failures;
    const auto verify=[&](bool result,std::string message){if (!result) failures.push_back(std::move(message));};
    const auto strip_uploads=[](BemPocData& payload) {
        payload.textures.clear();payload.payload_source.reset();
        for (auto& component:payload.components) {
            for (auto& draw:component.draws) draw.textures=0;
            component.keep_material_overrides.clear();component.keep_material_names.clear();
        }
    };
    // normal, revision during the queue, enable before first pump, transient
    // FindAll failure, and a registered template absent from FindAll.
    for (const auto& input:{legacy_path,resources_path}) for (const int scenario:{0,1,2,3,4}) {
        const auto failures_before=failures.size();
        const auto path=std::filesystem::absolute(input);
        g_registry={};g_registry.hot_switch=true;g_registry_root=path.parent_path();
        g_completed.clear();g_model_targets.clear();g_payload_cache.clear();g_model_assets.clear();g_model_plans.clear();
        g_instance_rebind_queue.clear();captured_logs.clear();g_model_frame=UINT64_MAX;g_model_frame_budget=FrameBudget{};
        g_instance_rebind_handled=scenario==2?0:g_model_revision;g_model_verify_interval_ms=0;
        const std::string disabled="[CustomModel]\nhot_switch=true\n[Mod.test]\nenabled=false\npackage="+path.filename().string()+"\n";
        const std::string enabled="[CustomModel]\nhot_switch=true\n[Mod.test]\nenabled=true\npackage="+path.filename().string()+"\n";
        ModRegistry preview;std::string error;
        Check(ParseModRegistry(enabled,g_registry_root,preview,error) && preview.diagnostics.empty() && !preview.enabled.empty(),
            ("first-enable metadata fixture rejected: "+error).c_str());
        const bool explicit_resources=preview.enabled.front().adapter->explicit_resource;
        Check(explicit_resources?preview.enabled.size()==3:preview.enabled.size()==1,"first-enable fixture resource count differs");
        BemPackageInfo metadata;Check(ReadBemPackageInfo(path,metadata,error),error.c_str());
        const auto authored_components=nlohmann::json::parse(metadata.manifest_json).at("target").at("components");
        std::unordered_map<std::string,std::shared_ptr<const BemPocData>> fixtures;
        for (const auto& mod:preview.enabled) {
            auto payload=std::make_shared<BemPocData>();
            Check(LoadBem(mod.package,*payload,error,mod.appearance,nullptr,false,mod.loading_optimization,mod.parameters,
                UINT64_MAX,false,mod.resource_id),error.c_str());strip_uploads(*payload);fixtures.emplace(mod.resource_id,payload);
        }
        AsyncBemLoader::Backend backend;
        backend.plan=[](const BemRequest&,BemLoadPlan& plan,std::string&){plan.reservation_bytes=1024*1024;return true;};
        backend.load=[fixtures](const BemRequest& request,uint64_t,BemPocData& out,BemLoadStats&,std::string& error) {
            const auto found=fixtures.find(request.resource_id);
            if (found==fixtures.end()) {error="unknown test resource";return false;}out=*found->second;return true;
        };
        g_model_loader=std::make_unique<AsyncBemLoader>(AsyncBemLoader::Config{},std::move(backend));
        g_texture_streamer=std::make_unique<TexturePayloadStreamer>();
        struct Receiver {Fake* root=nullptr;std::vector<Fake*> renderers,originals;std::string resource,defect;};
        auto* scene=Array({});auto* scene_objects=Array({});
        fake_scene_renderers=scene;fake_scene_game_objects=scene_objects;
        const auto receiver=[&](const EnabledMod& mod,std::string root_name,std::string defect={},const Receiver* source=nullptr) {
            Receiver result;result.root=GameObject(std::move(root_name));result.resource=std::string(ResourceBaseName(result.root->name));result.defect=std::move(defect);
            result.root->transforms=Array({});scene_objects->array.push_back(result.root);
            const auto& payload=*fixtures.at(mod.resource_id);
            std::unordered_map<std::string,Fake*> nodes;
            for (size_t i=0;i<mod.adapter->components.size();++i) {
                const auto& identity=mod.adapter->components[i];const auto& component=payload.components[i];
                size_t authored_index=i;
                if (mod.adapter->explicit_resource) {
                    const auto resource=std::find_if(metadata.resources.begin(),metadata.resources.end(),[&](const auto& entry){return entry.id==mod.resource_id;});
                    Check(resource!=metadata.resources.end(),"first-enable resource metadata missing");authored_index=resource->component_ids.at(i);
                }
                const auto& authored=authored_components.at(authored_index);
                std::string renderer_path=mod.adapter->explicit_resource?identity.receiver_path:"Mesh_all/lod0/receiver"+std::to_string(i);
                if (result.defect=="wrong-path") renderer_path="Unrelated/"+renderer_path;
                if (result.defect=="wrong-lod" && !mod.adapter->explicit_resource) renderer_path="Mesh_all/lod1/receiver"+std::to_string(i);
                Fake* parent=result.root;std::string prefix;
                for (size_t start=0;;) {
                    const auto slash=renderer_path.find('/',start);if (slash==std::string::npos) break;
                    const auto part=renderer_path.substr(start,slash-start);prefix+=part+"/";
                    auto [found,created]=nodes.emplace(prefix,nullptr);
                    if (created) {found->second=Make(part);found->second->parent=parent;static_cast<Fake*>(result.root->transforms)->array.push_back(found->second);}
                    parent=found->second;start=slash+1;
                }
                const auto slash=renderer_path.rfind('/');auto* renderer=Make(renderer_path.substr(slash==std::string::npos?0:slash+1));renderer->parent=parent;
                renderer->runtime_class=const_cast<void*>(component.static_mesh?g_static_renderer_class.class_info:g_skinned_renderer_class.class_info);
                auto* original=source?source->originals[i]:Make(identity.name);original->index_count=identity.indices;
                original->poses=Array({Make()});result.originals.push_back(original);
                if (component.static_mesh) {renderer->filter=Make("filter");static_cast<Fake*>(renderer->filter)->mesh=original;}
                else renderer->mesh=original;
                auto* materials=Array({});for (const auto& name:authored.at("materials")) materials->array.push_back(Make(name.get<std::string>()));renderer->materials=materials;
                if (!component.static_mesh) {
                    auto* bones=Array({});
                    for (const auto& bone_name:authored.at("bone_names")) {
                        const auto name=bone_name.get<std::string>();
                        auto [found,created]=nodes.emplace("bone:"+name,nullptr);
                        if (created) {found->second=Make(name);found->second->parent=result.root;static_cast<Fake*>(result.root->transforms)->array.push_back(found->second);}
                        bones->array.push_back(found->second);
                    }
                    renderer->bones=bones;
                }
                result.renderers.push_back(renderer);result.root->array.push_back(renderer);scene->array.push_back(renderer);
            }
            if (result.defect=="wrong-mesh") result.originals[0]->name="unrelated_mesh";
            if (result.defect=="wrong-count") ++result.originals[0]->index_count;
            if (result.defect=="wrong-type") result.renderers[0]->runtime_class=Make("unrelated-renderer-class");
            if (result.defect=="missing-material") result.renderers[0]->materials=Array({});
            if (result.defect=="foreign-bone" && result.renderers[0]->bones) {
                auto* foreign=Make(payload.components[0].bone_names.empty()?"foreign-bone":payload.components[0].bone_names.front());
                foreign->parent=GameObject("unrelated-global-bone-owner");result.renderers[0]->bones=Array({foreign});
            }
            if (result.defect=="missing-receiver") result.root->array.erase(result.root->array.begin());
            if (result.defect=="dead") result.root->alive=false;
            if (result.defect=="non-gameobject-owner") {
                auto* sprite=Make(result.root->name);sprite->runtime_class=Make("SpriteClass");result.root->game_object=sprite;
            }
            return result;
        };
        std::vector<Receiver> valid,invalid;int invalid_serial=100;
        for (const auto& mod:preview.enabled) {
            const std::vector<std::string> names=mod.adapter->explicit_resource?std::vector<std::string>{mod.adapter->world_resource}:
                std::vector<std::string>{mod.adapter->world_resource,mod.adapter->ui_resource};
            for (const auto& name:names) {
                valid.push_back(receiver(mod,name));
                auto clone=receiver(mod,name+"(Clone)#37",{},&valid.back());valid.push_back(std::move(clone));
            }
            for (const auto* defect:{"wrong-mesh","wrong-count","wrong-type","missing-material","missing-receiver","dead","non-gameobject-owner"})
                invalid.push_back(receiver(mod,std::string(mod.adapter->world_resource)+"(Clone)#"+std::to_string(invalid_serial++),defect));
            invalid.push_back(receiver(mod,"unrelated-global-root"));
            invalid.push_back(receiver(mod,""));
            invalid.push_back(receiver(mod,std::string(mod.adapter->world_resource)+"(Clone)#"+std::to_string(invalid_serial++),
                mod.adapter->explicit_resource?"wrong-path":"wrong-lod"));
            if (!mod.adapter->components.front().static_mesh)
                invalid.push_back(receiver(mod,std::string(mod.adapter->world_resource)+"(Clone)#"+std::to_string(invalid_serial++),"foreign-bone"));
            // Even an invalid object returned by a simulated global enumeration
            // must be gated before any GameObject instance API is invoked.
            auto* sprite=Make(mod.adapter->world_resource);sprite->runtime_class=Make("SpriteClass");scene_objects->array.push_back(sprite);
        }
        const auto current_mesh=[](Fake* renderer)->void* {return renderer->filter?static_cast<Fake*>(renderer->filter)->mesh:renderer->mesh;};
        const auto tick=[&] {++fake_frame;ConstructionScope scope;PumpModelJobs(fake_frame);std::this_thread::yield();};
        const auto seed=[&] {
            for (const auto& mod:g_registry.enabled)
                g_payload_cache.push_back({mod.package,mod.appearance,mod.selection_key,fixtures.at(mod.resource_id),0,GetTickCount64()+600000});
        };
        Check(InstallRegistryUpdate(disabled),"first-enable disabled registry rejected");if (scenario!=2) tick();
        for (const auto& root:valid) ResourceFinish(nullptr,root.root,nullptr);
        if (scenario!=2) tick();
        verify(g_completed.empty() && g_model_targets.empty() && g_model_deliveries.empty(),"disabled deliveries created ownership before first enable");
        const auto builds_before=fake_mesh_builds;
        Check(InstallRegistryUpdate(enabled),"first-enable registry enable rejected");seed();
        if (scenario==1) {
            tick();
            verify(!g_instance_rebind_queue.empty(),"first-enable discovery did not leave a multi-frame queue for the revision race");
            Check(InstallRegistryUpdate(disabled) && InstallRegistryUpdate(enabled),"rapid first-enable registry sequence rejected");seed();
        }
        if (scenario==3) {
            fake_scene_renderers=nullptr;tick();
            verify(fake_mesh_builds==builds_before && g_instance_rebind_queue.empty(),"failed FindAll admitted first-enable mutation");
            fake_scene_renderers=scene;
            // Exercise the actual time gate without writing new production
            // retry state: this same fixture also compiles against old code.
            std::this_thread::sleep_for(std::chrono::milliseconds(1100));
        }
        if (scenario==4) {
            const auto* selection=g_registry.Match(valid.front().root->name);
            {ConstructionScope scope;
                Check(selection && RememberModelTarget(valid.front().root,*selection),"template race registration failed");
                Check(EnqueueResourceBuild(valid.front().root,*selection) && g_model_jobs.size()==1,"template race Job was not queued");}
            for (auto* renderer:valid.front().renderers) std::erase(scene->array,renderer);
            tick();
            verify(g_model_jobs.empty(),"discovered unregistered clone failed to cancel its absent registered template's Job");
        }
        // The historical implementation leaves every first-time target on its
        // original mesh: neither the target table nor completed lineage exists.
        for (int i=0;i<512;++i) tick();
        const std::string label=(explicit_resources?"explicit":"legacy")+std::string(" scenario=")+std::to_string(scenario);
        for (const auto& root:valid) {
            verify(current_mesh(root.renderers[0])!=root.originals[0],label+" first enable left cached prefab/natural clone on Original: "+root.root->name);
            bool found=false;
            for (const auto& record:g_completed) if (record.root.Is(root.root)) {
                const auto* selection=g_registry.Match(root.root->name);
                if (selection && record.selection_key==selection->selection_key) found=true;
            }
            verify(found,label+" first enable did not publish the latest selection for "+root.root->name);
        }
        verify(fake_mesh_builds>builds_before,label+" first enable never reached production Mesh construction");
        for (const auto& root:invalid) for (size_t i=0;i<root.renderers.size();++i)
            verify(current_mesh(root.renderers[i])==root.originals[i],label+" discovered an invalid/global receiver: "+root.defect+" "+root.root->name);
        // No ordinary frame may start replacing later unknown roots just
        // because they happen to share a resource name after the change scan.
        auto late=receiver(preview.enabled.front(),std::string(preview.enabled.front().adapter->world_resource)+"(Clone)#999");
        for (int i=0;i<8;++i) tick();
        verify(current_mesh(late.renderers[0])==late.originals[0],label+" performed unsolicited recurring discovery");
        Check(InstallRegistryUpdate(disabled),"first-enable final disable rejected");
        for (int i=0;i<128;++i) tick();
        for (const auto& root:valid)
            verify(current_mesh(root.renderers[0])==root.originals[0],label+" first-enable ownership could not restore Original on disable");
        if (failures.size()!=failures_before) {
            std::cerr<<"First-enable diagnostics "<<label<<'\n';int shown=0;
            for (const auto& line:captured_logs) if (line.find("refused")!=line.npos || line.find("failed")!=line.npos ||
                line.find("Generic model donor")!=line.npos || line.find("discovery resources=")!=line.npos) {
                if (shown++<12) std::cerr<<line<<'\n';
            }
        }
        {ConstructionScope scope;CancelModelJobs();}
        g_model_loader->Shutdown();g_model_loader.reset();g_texture_streamer.reset();
        g_instance_rebind_queue.clear();g_model_targets.clear();g_completed.clear();g_payload_cache.clear();
        g_model_assets.clear();g_model_plans.clear();g_original_mesh_pins.clear();g_registry={};g_registry_root.clear();
        fake_scene_renderers=nullptr;fake_scene_game_objects=nullptr;
    }
    {ConstructionScope scope;g_lod.Restore();g_lod.pipeline.Reset();}
    g_model_build_mesh=&BuildModelMesh;g_hot_switch_runtime=false;g_enabled=false;fake_transform_type=nullptr;
    g_renderer_class={};g_static_renderer_class={};g_skinned_renderer_class={};g_mesh_filter_class={};g_game_object_class={};
    g_static_get=nullptr;g_static_set=nullptr;for (auto& field:g_lod.fields) field.resolved.field_info=nullptr;
    verify(roots==0 && tracked_handles.empty(),"first-enable lifecycle leaked GC handles");
    g_weak_new=nullptr;g_weak_target=nullptr;g_host=nullptr;
    for (const auto& failure:failures) std::cerr<<failure<<'\n';
    Check(failures.empty(),"first-enable production regression failed");
    std::cout<<"PASS first enable: registry-to-pump discovery of untouched legacy world/UI and explicit static/ultimate templates+natural clones, strict donor boundaries, rapid revisions, startup, FindAll retry, absent-template Job exclusion and disable restoration\n";
}
