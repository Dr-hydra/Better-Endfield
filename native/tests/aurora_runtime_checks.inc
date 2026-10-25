// Exercises production capture, preflight, Job capture, and merged palettes.
// The fixture has no graphics device, package file, or real game objects.
void AuroraRuntimeTests() {
    namespace A = AuroraLegacy;
    BE_HostApiV1 host{};
    host.runtime_invoke=InvokeFake;host.object_unbox=UnboxFake;host.copy_managed_string=StringFake;
    host.object_new=NewFake;host.gchandle_new=TrackedRoot;host.gchandle_free=TrackedFree;g_host=&host;
    g_weak_new=TrackedWeak;
    g_weak_target=[](uint32_t handle)->void* {const auto it=tracked_handles.find(handle);return it==tracked_handles.end()?nullptr:it->second;};
    ConfigureFakeObjectClasses();
    g_array_new_specific=[](void*,uintptr_t count)->void* {auto* result=Make();result->array.resize(count);return result;};
    for (auto& method:g_methods) {method.method_info=&method;method.resolved=true;}
    g_renderer_class.type_object=Make("RendererType");g_material_class.class_info=Make("MaterialClass");
    g_static_renderer_class.class_info=Make("MeshRendererClass");g_skinned_renderer_class.class_info=Make("SkinnedRendererClass");
    fake_transform_type=Make("TransformType");
    host.resolve_class=[](void*,const char*,const char*,const char*,BE_ResolvedClassV1* result)->BE_Result {
        result->class_info=fake_transform_type;result->type_object=fake_transform_type;return BE_Result_Ok;
    };
    g_hot_switch_runtime=false;g_completed.clear();g_registry={};g_model_frame_budget=FrameBudget{};
    unsigned checks=0;
    const auto verify=[&](bool ok,const char* message) {++checks;Check(ok,message);};
    for (const char* resource: {"chr_0014_aurora_postmodel","chr_0014_aurora_uimodel"}) {
        auto owner=std::make_shared<OwnedCharacterAdapter>();
        owner->id="chr_0014_aurora";owner->world="chr_0014_aurora_postmodel";owner->ui="chr_0014_aurora_uimodel";
        owner->names={"ordinary_mesh",std::string(A::kMeshName)};
        owner->components={{owner->names[0].c_str(),6},{owner->names[1].c_str(),87120}};
        owner->adapter={owner->id.c_str(),owner->world.c_str(),owner->ui.c_str(),"",false,owner->components};
        auto* asset=GameObject(resource);
        std::map<std::string,Fake*> nodes{{"",asset}};
        const auto node=[&](std::string_view path) {
            std::string accumulated;
            Fake* parent=asset;
            size_t start=0;
            while(start<path.size()) {
                size_t end=path.find('/',start);if(end==path.npos)end=path.size();
                if(!accumulated.empty())accumulated+='/';accumulated+=path.substr(start,end-start);
                auto found=nodes.find(accumulated);
                if(found==nodes.end()) {auto* part=Make(std::string(path.substr(start,end-start)));part->parent=parent;nodes.emplace(accumulated,part);parent=part;}
                else parent=found->second;
                start=end+1;
            }
            return parent;
        };
        auto* body=node("Mesh_all/lod0/ordinary_renderer");
        auto* cloth=node(A::kReceiverPath);
        body->runtime_class=cloth->runtime_class=const_cast<void*>(g_skinned_renderer_class.class_info);
        body->mesh=Make("ordinary_mesh");cloth->mesh=Make(std::string(A::kMeshName));
        auto* clothMesh=static_cast<Fake*>(cloth->mesh);clothMesh->index_count=87288;clothMesh->vertex_count=23141;
        auto* bodyBone=node("Root/body_bone");
        body->bones=Array({bodyBone});static_cast<Fake*>(body->mesh)->poses=Array({Make()});
        auto* clothBones=Array({});auto* clothPoses=Array({});
        for(const auto& contract:A::kCurrentBones) {
            auto* bone=node(contract.path);verify(bone->name==contract.name,"Aurora fixture bone path/name disagree");
            auto* pose=Make();std::copy(contract.bindpose.begin(),contract.bindpose.end(),std::begin(pose->matrix.m));
            clothBones->array.push_back(bone);clothPoses->array.push_back(pose);
        }
        cloth->bones=clothBones;clothMesh->poses=clothPoses;
        body->materials=Array({Make("ordinary_material")});cloth->materials=Array({Make(std::string(A::kMaterialName))});
        asset->array={body,cloth};
        asset->transforms=Array({});
        for(const auto& [path,part]:nodes) if(!path.empty()) static_cast<Fake*>(asset->transforms)->array.push_back(part);
        BemPocData payload;
        for(uint32_t component=0;component<2;++component) {
            BemComponent item;item.info.component_id=component;item.info.original_index_count=component?87120:6;
            item.info.flags=kComponentFlagNoGeometry;payload.components.push_back(std::move(item));
        }
        EnabledMod mod{};mod.adapter=&owner->adapter;
        const auto writes=fake_binding_write_calls;
        const auto unchanged=[&] {
            verify(fake_binding_write_calls==writes && cloth->mesh==clothMesh && cloth->bones==clothBones && cloth->enabled &&
                g_completed.empty(),"Aurora read-only preparation changed live bindings");
        };
        for(uint32_t flags:{kComponentFlagNoGeometry,kComponentFlagNoGeometry|kComponentFlagHidden}) {
            payload.components[1].info.flags=flags;
            ConstructionScope scope;
            std::vector<PreparedBinding> bindings;
            verify(PrepareResource(owner->adapter,payload,asset,bindings),"Aurora synchronous keep/hide preparation rejected exact donor");
            verify(bindings.size()==2 && bindings[1].aurora_legacy_163 && !bindings[0].aurora_legacy_163,
                "Aurora compatibility flag escaped verified C1");
            verify(bindings[1].custom_enabled==!(flags&kComponentFlagHidden),"Aurora hide/keep behavior changed");
        {
            BemPocData skipPayload = payload;
            skipPayload.skip_validation = true;
            ConstructionScope scope;
            std::vector<PreparedBinding> bindings;
            verify(PrepareResource(owner->adapter,skipPayload,asset,bindings),"Aurora skip_validation preparation failed");
            verify(bindings.size()==2 && bindings[1].aurora_legacy_163,"CRITICAL BUG: Aurora legacy compat was bypassed when skip_validation=true!");
        }

            unchanged();
            // Cross-component palettes must remap the referenced donor C1,
            // even when the replaced component itself is ordinary C0.
            BemComponent merged;merged.info.component_id=0;
            merged.bones={{1,37},{1,64},{0,0}};
            merged.bone_names={std::string(A::kCurrentBones[38].name),std::string(A::kCurrentBones[65].name),bodyBone->name};
            void* poses=nullptr;
            verify(PreparePalette(merged,bindings[0],bindings,poses,false,false),"Aurora cross-component palette remapping failed");
            verify(ArrayValue(bindings[0].custom_bones,0)==clothBones->array[38] && ArrayValue(poses,0)==clothPoses->array[38] &&
                ArrayValue(bindings[0].custom_bones,1)==clothBones->array[65] && ArrayValue(poses,1)==clothPoses->array[65] &&
                ArrayValue(bindings[0].custom_bones,2)==bodyBone,"Aurora merged palette used mismatched bone/bindpose slots");
            BemComponent complete;complete.info.component_id=1;
            for(uint32_t old=0;old<87;++old) {
                complete.bones.push_back({1,old});
                complete.bone_names.push_back(std::string(A::kCurrentBones[A::kLegacyToCurrent[old]].name));
            }
            void* completePoses=nullptr;
            verify(PreparePalette(complete,bindings[1],bindings,completePoses,false,false),"Aurora complete legacy palette rejected");
            for(uint32_t old=0;old<87;++old) {
                const auto current=A::kLegacyToCurrent[old];
                verify(ArrayValue(bindings[1].custom_bones,old)==clothBones->array[current] &&
                    ArrayValue(completePoses,old)==clothPoses->array[current],"Aurora complete palette changed legacy slot ordering");
            }
            merged.bone_names[0]="unknown_bone";poses=nullptr;
            verify(!PreparePalette(merged,bindings[0],bindings,poses,false,false),"Aurora palette accepted unknown legacy bone name");
            std::string reason;
            verify(HasCompatibleModelTarget(mod,asset,reason),"Aurora hot-switch discovery rejected exact compatible donor");
            unchanged();
            ModelBuildJob job;job.mod=mod;job.owner=owner;job.plan=MakeSmallModelPlan(payload);job.phase=ModelBuildJob::Phase::Capture;
            verify(job.target.Set(asset),"Aurora Job target rooting failed");
            g_model_frame_budget=FrameBudget{};
            verify(AdvanceModelJob(job,++fake_frame) && job.phase==ModelBuildJob::Phase::Mesh && job.bindings.size()==2 &&
                job.bindings[1].aurora_legacy_163,"Aurora Job capture did not share the synchronous compatibility proof");
            for(unsigned steps=0;job.phase!=ModelBuildJob::Phase::Commit && steps<20;++steps)
                verify(AdvanceModelJob(job,++fake_frame),"Aurora keep/hide Job preparation failed before commit");
            verify(job.phase==ModelBuildJob::Phase::Commit,"Aurora keep/hide Job preparation did not finish");
            unchanged();
        }
        // Exact old assets and packages remain on the unmodified normal path.
        payload.components[1].info.flags=kComponentFlagNoGeometry;
        clothMesh->index_count=87120;
        {
            ConstructionScope scope;std::vector<PreparedBinding> bindings;
            verify(CaptureGenericResourceBindings(owner->adapter,payload,asset,bindings) && !bindings[1].aurora_legacy_163,
                "Aurora unchanged old donor incorrectly entered compatibility path");
        }
        clothMesh->index_count=87288;
        owner->components[1].indices=87288;payload.components[1].info.original_index_count=87288;
        {
            ConstructionScope scope;std::vector<PreparedBinding> bindings;
            verify(CaptureGenericResourceBindings(owner->adapter,payload,asset,bindings) && !bindings[1].aurora_legacy_163,
                "Aurora new package incorrectly entered legacy compatibility path");
        }
        owner->components[1].indices=87120;payload.components[1].info.original_index_count=87120;
        for(unsigned failure=0;failure<5;++failure) {
            auto* material=static_cast<Fake*>(static_cast<Fake*>(cloth->materials)->array[0]);
            const auto materialName=material->name;
            auto* bone=static_cast<Fake*>(clothBones->array[38]);const auto boneName=bone->name;
            auto* pose=static_cast<Fake*>(clothPoses->array[38]);const auto matrix=pose->matrix;
            if(failure==0)clothMesh->index_count=87291;
            if(failure==1)material->name="foreign_material";
            if(failure==2)bone->name=static_cast<Fake*>(clothBones->array[37])->name;
            if(failure==3)pose->matrix.m[12]+=0.01f;
            if(failure==4)clothMesh->vertex_count=23142;
            {
                ConstructionScope scope;std::vector<PreparedBinding> bindings;std::string reason;
                verify(!CaptureGenericResourceBindings(owner->adapter,payload,asset,bindings),"Aurora capture accepted unproved donor");
                verify(!HasCompatibleModelTarget(mod,asset,reason),"Aurora discovery accepted unproved donor");
                unchanged();
            }
            clothMesh->index_count=87288;clothMesh->vertex_count=23141;material->name=materialName;bone->name=boneName;pose->matrix=matrix;
        }
        // After one real fake commit, the live custom palette is in old package
        // order. Discovery must use the saved original rather than treating
        // that reordered palette as the current 87-slot source contract.
        g_hot_switch_runtime=true;
        {
            ConstructionScope scope;std::vector<PreparedBinding> bindings;
            verify(PrepareResource(owner->adapter,payload,asset,bindings),"Aurora saved-original setup preparation failed");
            BemComponent complete;complete.info.component_id=1;
            for(uint32_t old=0;old<87;++old) {
                complete.bones.push_back({1,old});
                complete.bone_names.push_back(std::string(A::kCurrentBones[A::kLegacyToCurrent[old]].name));
            }
            void* poses=nullptr;
            verify(PreparePalette(complete,bindings[1],bindings,poses,false,false),"Aurora saved-original palette preparation failed");
            auto* generated=Make("AuroraLegacyCustomMesh");generated->poses=poses;bindings[1].custom_mesh=generated;
            CompletedResource record;
            verify(RememberResource(owner->adapter,asset,bindings,record,"aurora-test-selection"),"Aurora original snapshot failed");
            verify(CommitResource<PreparedBinding>(bindings,ApplyPreparedBinding,RestorePreparedBinding)==CommitResult::Committed,
                "Aurora fake commit failed");
            scope.published=true;PublishCompleted(std::move(record),asset);
            verify(cloth->bones!=clothBones && ArrayValue(cloth->bones,37)==clothBones->array[38],
                "Aurora saved-original fixture did not reorder the live palette");
            const auto committedWrites=fake_binding_write_calls;
            std::string reason;
            verify(HasCompatibleModelTarget(mod,asset,reason),"Aurora discovery confused saved Original with current custom palette");
            verify(fake_binding_write_calls==committedWrites && cloth->mesh==generated,
                "Aurora saved-original discovery modified the committed renderer");
            {
                ConstructionScope recapture;std::vector<PreparedBinding> next;
                verify(CaptureGenericResourceBindings(owner->adapter,payload,asset,next) && next[1].aurora_legacy_163 &&
                    DonorMesh(next[1])==clothMesh,"Aurora recapture failed to prove the saved pristine donor");
            }
            auto& originals=g_completed.back().bindings;
            auto found=std::find_if(originals.begin(),originals.end(),[](const auto& binding){return binding.component_id==1;});
            verify(found!=originals.end() && found->original,"Aurora saved-original record missing");
            const auto originalPath=found->original->bone_paths[37];found->original->bone_paths[37]="foreign/root/bone";
            verify(!HasCompatibleModelTarget(mod,asset,reason),"Aurora discovery ignored changed saved-original path proof");
            verify(fake_binding_write_calls==committedWrites,"Aurora refused discovery mutated the committed renderer");
            found->original->bone_paths[37]=originalPath;
            for(auto& binding:bindings)verify(RestorePreparedBinding(binding),"Aurora fixture original restore failed");
            g_completed.clear();
        }
        g_hot_switch_runtime=false;
    }
    g_registry={};g_completed.clear();g_weak_new=nullptr;g_weak_target=nullptr;g_host=nullptr;
    std::cout<<"PASS Aurora production compatibility: "<<checks<<" checks (UI/world, keep/hide, palette, discovery, Job, refusal)\n";
}
