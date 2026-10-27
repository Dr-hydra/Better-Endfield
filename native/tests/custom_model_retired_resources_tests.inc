// Run the production publication and delayed retirement paths against fake
// Unity objects; the assertion is on destruction of old versus current resources.
bool cleanup_test_fail_inspection=false;
void* BE_CALL InvokeCleanupTest(void* context,const void* method,void* object,void** args,void** exception) {
    const auto key=std::string_view(static_cast<const MethodContract*>(method)->key);
    if (cleanup_test_fail_inspection && key=="material.get_texture_property_ids") {
        *exception=Make("cleanup inspection failed");return nullptr;
    }
    return InvokeFake(context,method,object,args,exception);
}
void RetiredResourceCleanupTests() {
    Check(roots==0 && tracked_handles.empty(),"cleanup fixture requires clean roots");
    ModRegistry parsed;std::string error;
    Check(ParseModRegistry("[CustomModel]\n",{},parsed,error) && !parsed.release_retired_resources,"cleanup must default off");
    Check(ParseModRegistry("[CustomModel]\nrelease_retired_resources=true\n",{},parsed,error) && parsed.release_retired_resources,"cleanup flag missing");
    Check(!ParseModRegistry("[CustomModel]\nrelease_retired_resources=invalid\n",{},parsed,error),"invalid cleanup flag accepted");
    BE_HostApiV1 host{};host.runtime_invoke=InvokeCleanupTest;host.object_unbox=UnboxFake;
    host.copy_managed_string=StringFake;host.gchandle_new=TrackedRoot;host.gchandle_free=TrackedFree;g_host=&host;
    for (auto& method:g_methods) {method.method_info=&method;method.resolved=true;}
    g_weak_new=TrackedWeak;
    g_weak_target=[](uint32_t handle)->void* {auto it=tracked_handles.find(handle);return it==tracked_handles.end()?nullptr:it->second;};
    ConfigureFakeObjectClasses();g_material_cache_api={};g_clone_support_runtime=false;
    g_completed.clear();g_retired_generated_assets.clear();g_release_retired_resources_runtime=false;
    const CharacterAdapter adapter{"cleanup-test","cleanup-root","cleanup-ui","",false,{}};
    auto* root=GameObject("cleanup-root");auto* renderer=Make("cleanup-renderer");
    auto* original=Make("game-original");auto* old_mesh=Make("old-custom-mesh");
    auto* old_material=Make("old-custom-material");auto* shared_texture=Make("shared-custom-texture");
    auto record=[&](const char* selection) {
        CompletedResource r;r.adapter=&adapter;r.selection_key=selection;Check(r.root.Set(root),"record root failed");
        CompletedBinding b;Check(b.renderer.Set(renderer),"record renderer failed");r.bindings.push_back(std::move(b));return r;
    };
    {ConstructionScope scope;scope.published=true;scope.assets={old_mesh};
        auto r=record("disabled");CaptureExperimentalGeneratedAssets(r);QueueExperimentalRetiredAssets(r,0);
        Check(r.experimental_assets.empty() && g_retired_generated_assets.empty(),"disabled cleanup tracked resources");}
    g_release_retired_resources_runtime=true;
    {ConstructionScope scope;scope.published=true;scope.assets={old_mesh,old_material,shared_texture};
        PublishCompleted(record("A"),root);}
    Check(g_retired_generated_assets.empty(),"initial publication queued game originals");
    auto* new_mesh=Make("new-custom-mesh");auto* new_material=Make("new-custom-material");
    new_material->textures[7]=shared_texture;renderer->mesh=new_mesh;renderer->materials=Array({new_material});
    {ConstructionScope scope;scope.published=true;scope.assets={new_mesh,new_material};PublishCompleted(record("B"),root);}
    Check(g_retired_generated_assets.size()==3,"replacement did not queue old generated assets");
    uint64_t due=0;for (const auto& [id,pending]:g_retired_generated_assets) due=std::max(due,pending.due);
    TickExperimentalRetiredResources(due-1,false);
    Check(old_mesh->destroy_calls==0,"cleanup ignored delay");
    TickExperimentalRetiredResources(due,true);
    Check(old_mesh->destroy_calls==0,"cleanup ran while a model job was preparing assets");
    cleanup_test_fail_inspection=true;TickExperimentalRetiredResources(due,false);cleanup_test_fail_inspection=false;
    Check(old_mesh->destroy_calls==0 && old_material->destroy_calls==0,"failed inspection destroyed resources");
    TickExperimentalRetiredResources(due,false);
    Check(old_mesh->destroy_calls==1 && old_material->destroy_calls==1,"old generated objects not destroyed");
    Check(shared_texture->destroy_calls==0,"cross-generation reused texture destroyed");
    Check(original->destroy_calls==0 && new_mesh->destroy_calls==0 && new_material->destroy_calls==0,"game/current object destroyed");
    Check(g_retired_generated_assets.size()==1,"shared texture retirement was lost");
    auto* failed=Make("failed-new-generation");
    {ConstructionScope rollback;rollback.assets={failed};}
    Check(failed->destroy_calls==1 && g_retired_generated_assets.size()==1,"failed build retired current generation");
    new_material->textures.clear();TickExperimentalRetiredResources(due+kRetiredResourceDelayMs,false);
    Check(shared_texture->destroy_calls==1 && g_retired_generated_assets.empty(),"unused shared texture did not retire later");
    auto r=record("dead-wrapper");WeakObject dead;Check(dead.Set(Make("gone")),"dead fixture failed");
    auto* replacement=Make("reused-wrapper-address");tracked_handles[dead.handle]=replacement;
    r.experimental_assets.push_back(std::move(dead));QueueExperimentalRetiredAssets(r,0);
    TickExperimentalRetiredResources(kRetiredResourceDelayMs,false);
    Check(replacement->destroy_calls==0 && g_retired_generated_assets.empty(),"stale identity destroyed another object");
    r.experimental_assets.clear();r.bindings.clear();r.root.Reset();
    g_release_retired_resources_runtime=false;g_completed.clear();g_retired_generated_assets.clear();
    Check(roots==0 && tracked_handles.empty(),"cleanup bookkeeping retained GC roots");
    g_weak_new=nullptr;g_weak_target=nullptr;g_host=nullptr;
    std::cout<<"PASS retired resources: default-off/parser, committed replacement only, delay, busy/inspection deferral, original/current protection, cross-generation textures and stale identity\n";
}
