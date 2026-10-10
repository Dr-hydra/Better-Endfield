// Included after the binding test fixtures. Uses production ProcessResource,
// scene scanning and the instance pump; only Unity objects and Mesh upload are
// mocked. Natural clones are deliberately never registered through clone hooks.
void SceneRebindTests(const std::filesystem::path& package_path) {
    BemPocData fixture;std::string error;
    Check(LoadBem(package_path,fixture,error),error.c_str());
    Check(fixture.components.size()==2 && !fixture.components[0].bones.empty(),"scene fixture needs the two-component skinned palette");
    BE_HostApiV1 host{};host.runtime_invoke=InvokeFake;host.object_unbox=UnboxFake;host.copy_managed_string=StringFake;
    host.object_new=NewFake;host.gchandle_new=TrackedRoot;host.gchandle_free=TrackedFree;
    host.log=[](void*,const char*,const char* text){captured_logs.emplace_back(text?text:"");};g_host=&host;
    ConfigureFakeObjectClasses();g_weak_new=TrackedWeak;
    g_weak_target=[](uint32_t h)->void* {auto it=tracked_handles.find(h);return it==tracked_handles.end()?nullptr:it->second;};
    g_array_new_specific=[](void*,uintptr_t count)->void* {auto* array=Make();array->array.resize(count);return array;};
    fake_transform_type=Make("TransformType");
    host.resolve_class=[](void*,const char*,const char*,const char*,BE_ResolvedClassV1* result)->BE_Result {
        result->class_info=fake_transform_type;result->type_object=fake_transform_type;return BE_Result_Ok;
    };
    for (auto& method:g_methods) {method.method_info=&method;method.resolved=true;}
    g_renderer_class.type_object=Make("RendererType");g_static_renderer_class.class_info=Make("MeshRendererClass");
    g_skinned_renderer_class.class_info=Make("SkinnedMeshRendererClass");g_mesh_filter_class.type_object=Make("MeshFilterType");
    g_material_class.class_info=Make("MaterialClass");
    g_static_get=[](const void* field,void* value){std::memcpy(value,fake_static_fields[field].data(),8);};
    g_static_set=[](const void* field,void* value){std::memcpy(fake_static_fields[field].data(),value,8);};
    for (auto& field:g_lod.fields) field.resolved.field_info=&field;
    g_model_build_mesh=[](const BemComponent&,void* donor,void*& mesh,void* poses)->bool {
        ++fake_mesh_builds;fake_build_donors.push_back(donor);
        if (std::exchange(fail_next_mesh_build,false)) return false;
        auto* built=static_cast<Fake*>(NewAsset(g_material_class.class_info));if (!built) return false;
        built->name="scene-generated-"+std::to_string(fake_mesh_builds);built->poses=poses;mesh=built;return true;
    };
    g_original_finish=[](void*,void* asset,void*) {++finish_calls;last_finished_asset=asset;};
    g_enabled=true;g_stopping=false;g_probe.active=false;g_model_clone_coverage=false;

    for (bool static_mesh:{false,true}) {
        g_completed.clear();g_model_targets.clear();g_payload_cache.clear();captured_logs.clear();
        ResetInstanceRebinds();g_model_frame_budget=FrameBudget{};fail_next_mesh_build=false;
        g_hot_switch_runtime=true;g_instance_rebind_handled=g_model_revision;
        auto payload=std::make_shared<BemPocData>(fixture);payload->textures.clear();
        payload->payload_source.reset();payload->loading_optimization=false;
        if (static_mesh) payload->components.resize(1);
        for (auto& c:payload->components) {
            c.info.original_index_count=6;c.keep_material_overrides.clear();c.keep_material_names.clear();
            if (!(c.info.flags&kComponentFlagNoGeometry)) {
                Check(!c.draws.empty(),"scene geometry fixture needs a material draw");c.draws.resize(1);
                c.draws[0].material_component=c.info.component_id;c.draws[0].material_slot=0;c.draws[0].textures=0;
                c.material_names={"material"+std::to_string(c.info.component_id)};
            }
            c.static_mesh=static_mesh;
            if (static_mesh) {
                c.bones.clear();c.bone_names.clear();c.streams[2].clear();c.info.stride2=0;c.info.max_bone=0;
                std::erase_if(c.attributes,[](const auto& attribute){return attribute[0]==12 || attribute[0]==13;});
            }
        }
        auto owner=std::make_shared<OwnedCharacterAdapter>();owner->id=static_mesh?"wpn_scene":"chr_scene";
        owner->world="scene_world";owner->ui=static_mesh?owner->world:"scene_ui";
        const auto count=payload->components.size();owner->names.reserve(count);owner->receiver_paths.reserve(count);
        for (size_t i=0;i<count;++i) {
            owner->names.push_back("mesh"+std::to_string(i));
            owner->receiver_paths.push_back("Mesh_all/lod0/receiver"+std::to_string(i));
        }
        for (size_t i=0;i<count;++i) owner->components.push_back({owner->names[i].c_str(),6,
            static_mesh?owner->receiver_paths[i].c_str():"",static_mesh});
        owner->adapter={owner->id.c_str(),owner->world.c_str(),owner->ui.c_str(),"",false,owner->components,
            static_mesh?"weapon":"",static_mesh?"assets/test/scene_world.prefab":"",0,static_mesh};
        g_registry={};g_registry.hot_switch=true;g_registry.owned_adapters.push_back(owner);
        EnabledMod a{&owner->adapter,package_path,"default",false,false,"scene-selection-A",""};
        EnabledMod b=a;b.selection_key="scene-selection-B";
        const auto cache=[&](const EnabledMod& mod) {
            g_payload_cache.push_back({mod.package,mod.appearance,mod.selection_key,payload,0,GetTickCount64()+60000});
        };
        cache(a);cache(b);g_registry.enabled={a};
        std::vector<Fake*> original_meshes,original_materials;
        for (size_t i=0;i<count;++i) {
            auto* mesh=Make(owner->names[i]);mesh->poses=Array({Make()});original_meshes.push_back(mesh);
            original_materials.push_back(Make("material"+std::to_string(i)));
        }
        struct Receiver {Fake* root;std::vector<Fake*> renderers,bones;};
        const auto receiver=[&](std::string name) {
            Receiver result{GameObject(std::move(name)),{}, {}};
            auto* mesh_all=Make("Mesh_all");mesh_all->parent=result.root;
            auto* lod=Make("lod0");lod->parent=mesh_all;result.root->transforms=Array({mesh_all,lod});
            for (size_t i=0;i<count;++i) {
                auto* r=Make("receiver"+std::to_string(i));r->parent=lod;r->materials=Array({original_materials[i]});
                r->runtime_class=const_cast<void*>(static_mesh?g_static_renderer_class.class_info:g_skinned_renderer_class.class_info);
                if (static_mesh) {auto* filter=Make("filter");filter->mesh=original_meshes[i];r->filter=filter;}
                else {
                    r->mesh=original_meshes[i];auto* bone=Make("bone"+std::to_string(i));bone->parent=result.root;
                    r->bones=Array({bone});result.bones.push_back(bone);
                    static_cast<Fake*>(result.root->transforms)->array.push_back(bone);
                }
                result.renderers.push_back(r);result.root->array.push_back(r);
            }
            return result;
        };
        const auto mesh=[&](Fake* renderer) {return static_mesh?static_cast<Fake*>(renderer->filter)->mesh:renderer->mesh;};
        auto prefab=receiver(owner->world);
        {ConstructionScope scope;const bool committed=ProcessResource(prefab.root,scope);
            if (!committed) for (const auto& line:captured_logs) std::cerr<<line<<'\n';
            Check(committed,"initial scene template did not commit");}
        Check(mesh(prefab.renderers[0])!=original_meshes[0],"initial scene template retained original geometry");
        {ConstructionScope scope;Check(RememberModelTarget(prefab.root,a)!=nullptr,"scene template registration failed");}
        std::unique_ptr<Receiver> ui_prefab;
        if (!static_mesh) {
            ui_prefab=std::make_unique<Receiver>(receiver(owner->ui));ConstructionScope scope;
            Check(ProcessResource(ui_prefab->root,scope) && RememberModelTarget(ui_prefab->root,a),"UI scene template did not commit/register");
        }
        std::vector<Receiver> clones;
        const auto natural_clone=[&](const Receiver& source_prefab,std::string name) {
            auto clone=receiver(std::move(name));
            for (size_t i=0;i<count;++i) {
                auto* source=source_prefab.renderers[i];auto* target=clone.renderers[i];
                if (static_mesh) static_cast<Fake*>(target->filter)->mesh=mesh(source);else target->mesh=mesh(source);
                auto* materials=Array({});materials->array=static_cast<Fake*>(source->materials)->array;target->materials=materials;
                if (!static_mesh) {
                    auto* bones=Array({});
                    for (auto* bone:static_cast<Fake*>(source->bones)->array) {
                        const auto found=std::find_if(clone.bones.begin(),clone.bones.end(),[&](const auto* own){return own->name==static_cast<Fake*>(bone)->name;});
                        Check(found!=clone.bones.end(),"natural clone bone correspondence unavailable");bones->array.push_back(*found);
                    }
                    target->bones=bones;
                }
            }
            return clone;
        };
        for (int serial=7;serial<12;++serial)
            clones.push_back(natural_clone(prefab,owner->world+"(Clone)#"+std::to_string(serial)));
        if (ui_prefab) for (int serial=27;serial<29;++serial)
            clones.push_back(natural_clone(*ui_prefab,owner->ui+"(Clone)#"+std::to_string(serial)));
        auto* scene=Array({});for (auto* r:prefab.renderers) scene->array.push_back(r);
        if (ui_prefab) for (auto* r:ui_prefab->renderers) scene->array.push_back(r);
        for (const auto& clone:clones) for (auto* r:clone.renderers) scene->array.push_back(r);
        fake_scene_renderers=scene;
        Check(g_model_targets.size()==(static_mesh?1u:2u) && g_model_deliveries.empty(),"natural scene clones were manually registered");
        const auto pump_switch=[&] {
            ++g_model_revision;
            for (int i=0;i<64;++i) {
                ++fake_frame;ConstructionScope scope;PumpInstanceRebind(fake_frame);
                if (g_instance_rebind_handled==g_model_revision && g_instance_rebind_queue.empty()) return;
            }
            Check(false,"scene rebind queue did not drain");
        };
        g_registry.enabled.clear();pump_switch();
        for (const auto& clone:clones) {
            if (mesh(clone.renderers[0])!=original_meshes[0])
                for (const auto& line:captured_logs) std::cerr<<line<<'\n';
            Check(mesh(clone.renderers[0])==original_meshes[0],"unregistered natural clone was not restored by the real scene scan/pump");
            bool recorded=false;
            for (const auto& record:g_completed) if (record.root.Is(clone.root) && record.selection_key.empty()) recorded=true;
            Check(recorded,"restored natural clone has no verified inactive record");
        }
        Check(mesh(prefab.renderers[0])==original_meshes[0],"disable did not restore the registered template");
        // Previously verified roots can subsequently be renamed or die. None
        // of those records authorizes a match based on their former names.
        clones[1].root->name="";clones[2].root->name="renamed_scene_actor";
        clones[3].root->name="different_resource(Clone)#10";clones[4].root->alive=false;
        g_registry.enabled={b};captured_logs.clear();
        AsyncBemLoader::Backend backend;
        backend.plan=[](const BemRequest&,BemLoadPlan& plan,std::string&){plan.reservation_bytes=1024;return true;};
        backend.load=[payload](const BemRequest&,uint64_t,BemPocData& out,BemLoadStats&,std::string&){out=*payload;return true;};
        g_model_loader=std::make_unique<AsyncBemLoader>(AsyncBemLoader::Config{},std::move(backend));
        Check(EnqueueResourceBuild(prefab.root,b) && !g_model_jobs.empty(),"template Job was not queued before instance rebind");
        pump_switch();
        // The old implementation returned immediately at stale.empty(): all
        // generated meshes had been restored, so this actual clone stayed bare.
        const bool natural_clone_reactivated=mesh(clones[0].renderers[0])!=original_meshes[0];
        if (!natural_clone_reactivated) {
            // Leave no async jobs or fake GC handles for process teardown on
            // the intentional before-fix failure path.
            {ConstructionScope scope;CancelModelJobs();}
            g_model_loader->Shutdown();g_model_loader.reset();
            g_instance_rebind_queue.clear();g_model_targets.clear();g_completed.clear();g_original_mesh_pins.clear();
            {ConstructionScope scope;g_lod.Restore();g_lod.pipeline.Reset();}
            g_host=nullptr;
        }
        Check(natural_clone_reactivated,"enable after disable left the verified natural clone on Original Mesh");
        Check(mesh(prefab.renderers[0])!=original_meshes[0],"enable after disable left the registered template on Original Mesh");
        Check(g_model_jobs.empty(),"scene rebind competed with the same template's asynchronous Job");
        Check(LogContains(static_mesh?"restoredRootsQueued=2":"restoredRootsQueued=5"),"reactivation did not report exactly the valid clones and templates");
        if (ui_prefab) {
            Check(mesh(ui_prefab->renderers[0])!=original_meshes[0],"UI template did not reactivate");
            for (size_t i=5;i<clones.size();++i) Check(mesh(clones[i].renderers[0])!=original_meshes[0],"natural UI clone did not reactivate");
        }
        for (size_t i=1;i<5;++i)
            Check(mesh(clones[i].renderers[0])==original_meshes[0],"renamed, unnamed, wrong-root or dead scene receiver was rebound");
        // A second transition spans multiple frames and receives another
        // selection before draining. Templates must never acquire a competing
        // Job while any instance of this adapter is pending.
        g_registry.enabled.clear();pump_switch();
        EnabledMod c=b;c.selection_key="scene-selection-C";cache(c);
        EnabledMod d=b;d.selection_key="scene-selection-D";cache(d);
        g_registry.enabled={c};++g_model_revision;
        {++fake_frame;ConstructionScope scope;PumpInstanceRebind(fake_frame);}
        Check(!g_instance_rebind_queue.empty(),"multi-root rebind fixture did not span multiple frames");
        Check(!EnqueueResourceBuild(prefab.root,c) && g_model_jobs.empty(),"pending instance rebind admitted a competing template Job");
        if (ui_prefab) Check(!EnqueueResourceBuild(ui_prefab->root,c),"pending world/clone rebind admitted a competing UI Job");
        g_registry.enabled={d};pump_switch();
        for (const auto& target:g_model_targets)
            Check(target.desired==d.selection_key && target.verified_selection==d.selection_key,
                "synchronous instance rebind did not publish the latest template selection");
        for (const auto& clone:clones) if (clone.root->alive &&
                (ResourceBaseName(clone.root->name)==owner->world || ResourceBaseName(clone.root->name)==owner->ui)) {
            bool latest=false;for (const auto& record:g_completed)
                if (record.root.Is(clone.root) && record.selection_key==d.selection_key) latest=true;
            Check(latest,"revision arriving mid-queue left a verified clone on an older selection");
        }
        Check(g_model_jobs.empty() && g_model_targets.size()==(static_mesh?1u:2u),"natural clones leaked into targets or competing Jobs survived");
        if (static_mesh) {
            // Exercise the actual ProcessResource failure/pump boundary: failed
            // roots retry with backoff, do not block healthy work, and retain an
            // unresolved lease after the per-revision attempt bound is reached.
            const auto queue=[&](Fake* root) {
                auto pending=std::make_unique<PendingInstanceRebind>();Check(pending->Set(root),"retry root handle failed");
                pending->adapter=&owner->adapter;pending->adapter_owner=owner;pending->revision=g_model_revision;
                pending->selection_key=ActiveSelectionKey(owner->adapter);
                g_instance_rebind_queue.push_back(std::move(pending));
            };
            const auto pump=[&] {++fake_frame;ConstructionScope scope;PumpInstanceRebind(fake_frame);};
            ResetInstanceRebinds();g_pending_model_resources.clear();
            EnabledMod retry=d;retry.selection_key="scene-retry";cache(retry);g_registry.enabled={retry};++g_model_revision;
            g_instance_rebind_handled=g_model_revision;g_instance_rebind_revision=g_model_revision;
            queue(clones[0].root);const auto previous_mesh=mesh(clones[0].renderers[0]);
            fail_next_mesh_build=true;pump();
            Check(mesh(clones[0].renderers[0])==previous_mesh && g_instance_rebind_queue.size()==1 &&
                g_instance_rebind_queue.front()->attempts==1 && g_instance_rebind_queue.front()->next_retry_ms>GetTickCount64(),
                "failed instance was dropped, mutated or retried without backoff");
            const auto builds_before_backoff=fake_mesh_builds;pump();
            Check(fake_mesh_builds==builds_before_backoff,"cooldown attempted another Mesh build");
            queue(prefab.root);pump();
            Check(IsCompletedResource(owner->adapter,prefab.root,retry.selection_key) && g_instance_rebind_queue.size()==1,
                "a waiting retry blocked a healthy template");
            g_instance_rebind_queue.front()->next_retry_ms=0;pump();
            Check(g_instance_rebind_queue.empty() && g_failed_instance_rebinds.empty() &&
                IsCompletedResource(owner->adapter,clones[0].root,retry.selection_key),"transient instance failure did not recover");
            EnabledMod refused_mod=retry;refused_mod.selection_key="scene-retry-rejected";cache(refused_mod);g_registry.enabled={refused_mod};++g_model_revision;
            g_instance_rebind_handled=g_model_revision;g_instance_rebind_revision=g_model_revision;queue(clones[0].root);
            const auto retained_mesh=mesh(clones[0].renderers[0]);
            for (unsigned attempt=0;attempt<kInstanceRebindAttempts;++attempt) {
                g_instance_rebind_queue.front()->next_retry_ms=0;fail_next_mesh_build=true;pump();
            }
            Check(g_instance_rebind_queue.empty() && g_failed_instance_rebinds.size()==1 && InstanceRebindOutstanding() &&
                mesh(clones[0].renderers[0])==retained_mesh,"exhausted retries lost their unresolved state or previous binding");
            const auto bounded_builds=fake_mesh_builds;pump();Check(fake_mesh_builds==bounded_builds,"exhausted request rebuilt indefinitely");
            // A separate, unchanged role must remain exhausted when this role
            // receives a new selection (device: Liino revived Pelica's request).
            auto peer_owner=std::make_shared<OwnedCharacterAdapter>();peer_owner->id="chr_retry_peer";
            peer_owner->world="retry_peer_world";peer_owner->ui="retry_peer_ui";
            peer_owner->adapter={peer_owner->id.c_str(),peer_owner->world.c_str(),peer_owner->ui.c_str(),"",false,{}};
            EnabledMod peer{&peer_owner->adapter,package_path,"default",false,false,"unchanged-peer-selection",""};
            auto* peer_root=GameObject(peer_owner->world);
            auto peer_failure=std::make_unique<PendingInstanceRebind>();Check(peer_failure->Set(peer_root),"peer failure root handle failed");
            peer_failure->adapter=&peer_owner->adapter;peer_failure->adapter_owner=peer_owner;
            peer_failure->selection_key=peer.selection_key;peer_failure->attempts=kInstanceRebindAttempts;peer_failure->revision=g_model_revision;
            const auto peer_revision=peer_failure->revision;g_failed_instance_rebinds.push_back(std::move(peer_failure));
            EnabledMod latest=refused_mod;latest.selection_key="scene-retry-latest";cache(latest);g_registry.enabled={latest,peer};++g_model_revision;
            g_instance_rebind_handled=g_model_revision;pump();
            Check(g_instance_rebind_queue.empty() && g_failed_instance_rebinds.size()==1 &&
                g_failed_instance_rebinds.front()->Get()==peer_root && g_failed_instance_rebinds.front()->attempts==kInstanceRebindAttempts &&
                g_failed_instance_rebinds.front()->revision==peer_revision && InstanceRebindExhausted(peer_root) &&
                !InstanceRebindExhausted(clones[0].root),"another role's edit replayed or reset an unchanged failed request");
            g_failed_instance_rebinds.clear();
            Check(!InstanceRebindOutstanding() && IsCompletedResource(owner->adapter,clones[0].root,latest.selection_key),
                "new revision did not retry an exhausted root with the latest selection");
            // A normal later resource delivery can also resolve a refusal;
            // otherwise a destroyed native root must release its strong handle.
            g_registry.enabled={refused_mod};++g_model_revision;g_instance_rebind_handled=g_model_revision;
            g_instance_rebind_revision=g_model_revision;queue(clones[0].root);
            for (unsigned attempt=0;attempt<kInstanceRebindAttempts;++attempt) {
                g_instance_rebind_queue.front()->next_retry_ms=0;fail_next_mesh_build=true;pump();
            }
            {ConstructionScope scope;Check(ProcessResource(clones[0].root,scope),"normal delivery failed to repair the rejected root");}
            g_failed_instance_prune_ms=0;pump();Check(!InstanceRebindOutstanding(),"successful normal delivery retained a failed lease");
            g_registry.enabled={latest};++g_model_revision;g_instance_rebind_handled=g_model_revision;
            g_instance_rebind_revision=g_model_revision;queue(clones[0].root);
            for (unsigned attempt=0;attempt<kInstanceRebindAttempts;++attempt) {
                g_instance_rebind_queue.front()->next_retry_ms=0;fail_next_mesh_build=true;pump();
            }
            clones[0].root->alive=false;g_failed_instance_prune_ms=0;pump();
            Check(!InstanceRebindOutstanding(),"destroyed native root kept a failed lease");
            clones[0].root->alive=true;
            std::cout<<"PASS instance retry: delayed transient recovery, healthy work fairness, attempt bound, unresolved lease, superseding revision, normal delivery and destroyed-root cleanup\n";
        }
        {ConstructionScope scope;CancelModelJobs();}
        g_model_loader->Shutdown();g_model_loader.reset();
        ResetInstanceRebinds();g_model_targets.clear();g_completed.clear();g_original_mesh_pins.clear();
        fake_scene_renderers=nullptr;

        // A duplicate delivery with a changed binding has no authority to use
        // generated geometry as pristine when the original was never saved.
        g_hot_switch_runtime=false;g_registry.hot_switch=false;g_model_clone_coverage=false;
        EnabledMod unsaved=b;unsaved.selection_key="scene-without-original";cache(unsaved);g_registry.enabled={unsaved};
        auto drift=receiver(owner->world);
        ResourceFinish(nullptr,drift.root,nullptr);g_model_deliveries.clear();
        // Current normal deliveries retain provenance even with hot switch
        // off. Explicitly retire it to exercise the missing-Original gate.
        for (auto& record:g_completed) if (record.root.Is(drift.root))
            for (auto& binding:record.bindings) binding.original.reset();
        const auto builds=fake_mesh_builds;const auto original_calls=finish_calls;
        ResourceFinish(nullptr,drift.root,nullptr);g_model_deliveries.clear();
        Check(fake_mesh_builds==builds && finish_calls==original_calls+1,"unchanged duplicate Finish rebuilt geometry or swallowed delivery");
        auto* changed=Make("changed-material");drift.renderers[0]->materials=Array({changed});
        if (!static_mesh) drift.renderers[0]->bones=Array({Make("foreign-bone")});
        captured_logs.clear();ResourceFinish(nullptr,drift.root,nullptr);g_model_deliveries.clear();
        Check(fake_mesh_builds==builds && finish_calls==original_calls+2,"binding drift without Original caused another build or lost delivery");
        Check(LogContains("completed without a saved Original") && LogContains("expectedIndices="),
            "duplicate Finish drift refusal omitted the saved-Original/candidate diagnostic");
        if (!static_mesh) {
            auto outside=receiver(owner->world);outside.renderers[0]->bones=Array({Make("foreign-bone")});captured_logs.clear();
            {ConstructionScope scope;Check(!ProcessResource(outside.root,scope),"bone outside the resource root was accepted");}
            Check(LogContains("reason=bone-outside-resource-root"),"bone-root rejection did not explain its exact contract failure");
        }
        g_completed.clear();g_model_targets.clear();g_payload_cache.clear();g_original_mesh_pins.clear();
        g_registry={};g_instance_rebind_handled=0;g_model_frame_budget=FrameBudget{};
        Check(roots==1,"scene case leaked handles beyond the active LOD pipeline");
    }
    {ConstructionScope scope;g_lod.Restore();g_lod.pipeline.Reset();}
    g_model_build_mesh=&BuildModelMesh;g_hot_switch_runtime=false;g_enabled=false;fake_scene_renderers=nullptr;fake_transform_type=nullptr;
    g_renderer_class={};g_static_renderer_class={};g_skinned_renderer_class={};g_mesh_filter_class={};g_game_object_class={};
    g_static_get=nullptr;g_static_set=nullptr;for (auto& field:g_lod.fields) field.resolved.field_info=nullptr;
    Check(roots==0 && tracked_handles.empty(),"scene rebind lifecycle leaked GC handles");
    g_weak_new=nullptr;g_weak_target=nullptr;g_host=nullptr;
    std::cout<<"PASS scene rebind: unregistered legacy/skinned and explicit/static natural clones A-off-B, verified Original reactivation, rejected renamed/dead roots, template Job exclusion, repeated Finish drift diagnostics and handle cleanup\n";
}
