// Exercises production geometry lineage and Original material ownership with
// different roots sharing an unchanged Mesh, as in mobile keep components.
void InstanceLineageTests() {
    BE_HostApiV1 host{};host.runtime_invoke=InvokeFake;host.object_unbox=UnboxFake;host.copy_managed_string=StringFake;
    host.object_new=NewFake;host.gchandle_new=TrackedRoot;host.gchandle_free=TrackedFree;
    host.log=[](void*,const char*,const char* text){captured_logs.emplace_back(text?text:"");};g_host=&host;
    ConfigureFakeObjectClasses();g_weak_new=TrackedWeak;
    g_weak_target=[](uint32_t h)->void* {auto it=tracked_handles.find(h);return it==tracked_handles.end()?nullptr:it->second;};
    g_array_new_specific=[](void*,uintptr_t count)->void* {auto* array=Make();array->array.resize(count);return array;};
    for (auto& method:g_methods) {method.method_info=&method;method.resolved=true;}
    g_skinned_renderer_class.class_info=Make("SkinnedRenderer");
    CharacterAdapter adapter{"chr_lineage","world","ui","",false,{}};
    {
        auto* root=GameObject("world(Clone)#33");auto* other_root=GameObject("world");
        auto* mesh_all=Make("Mesh_all");mesh_all->parent=root;
        auto* lod=Make("lod1");lod->parent=mesh_all;
        auto* renderer=Make("brow");renderer->parent=lod;
        renderer->runtime_class=const_cast<void*>(g_skinned_renderer_class.class_info);
        auto* mesh=Make("original-brow");auto* different=Make("different-Original");
        auto* own_materials=Array({Make("own-material-1"),Make("own-material-2")});
        auto* foreign_materials=Array({Make("template-material")});auto* bones=Array({Make("bone")});
        renderer->mesh=mesh;renderer->materials=own_materials;renderer->bones=bones;
        GenericMatching::ReceiverKey key;
        Check(MakeReceiverKey(adapter,root,renderer,key),"lineage receiver key unavailable");
        GenericRendererCandidate candidate{key,renderer,mesh,own_materials,bones,true};
        const auto record=[&](void* owner,void* recorded_mesh,void* materials,void* original_mesh,bool generated,bool save) {
            CompletedResource result;result.adapter=&adapter;Check(result.root.Set(owner),"lineage root handle unavailable");
            CompletedBinding binding;binding.receiver_key=key;binding.generated_mesh=generated;
            Check(binding.mesh.Set(recorded_mesh),"lineage Mesh handle unavailable");
            for (int i=0;i<ArrayLength(materials);++i) {
                WeakObject material;Check(material.Set(ArrayValue(materials,i)),"lineage material handle unavailable");
                binding.materials.push_back(std::move(material));
            }
            if (save) {
                binding.original=std::make_shared<SavedOriginalBinding>();
                Check(binding.original->mesh.Set(original_mesh) && binding.original->SetMaterials(materials) &&
                    binding.original->bones.Set(bones),"lineage Original capture unavailable");
            }
            result.bindings.push_back(std::move(binding));return result;
        };
        const auto read=[&](void* expected) {
            GenericMatching::MeshIdentity identity;ConstructionScope scope;
            Check(ReadGenericPristineMesh(adapter,root,candidate,identity) && identity.mesh==expected &&
                identity.origin==GenericMatching::DonorOrigin::SavedOriginal,"reliable Original was vetoed by another root's material state");
        };
        for (bool foreign_first:{false,true}) {
            g_completed.clear();
            if (foreign_first) g_completed.push_back(record(other_root,mesh,foreign_materials,different,false,true));
            g_completed.push_back(record(root,mesh,own_materials,mesh,false,true));
            if (!foreign_first) g_completed.push_back(record(other_root,mesh,foreign_materials,different,false,true));
            read(mesh);
            PreparedBinding binding;binding.renderer=renderer;binding.receiver_key=key;
            binding.original_mesh=mesh;binding.original_materials=own_materials;binding.original_bones=bones;
            ConstructionScope scope;
            Check(UseSavedOriginal(root,binding,true,true) && binding.saved_original &&
                DonorMesh(binding)==mesh && SameMaterialSet(DonorMaterials(binding),own_materials),
                "foreign template Original materials overrode the instance's own record");
        }
        g_completed.clear();g_completed.push_back(record(other_root,mesh,foreign_materials,mesh,false,true));
        read(mesh); // The exact unchanged Original identifies geometry only.
        {
            PreparedBinding binding;binding.renderer=renderer;binding.receiver_key=key;
            binding.original_mesh=mesh;binding.original_materials=own_materials;binding.original_bones=bones;
            ConstructionScope scope;
            Check(UseSavedOriginal(root,binding) && !binding.saved_original && DonorMaterials(binding)==own_materials,
                "geometry identity granted authority to borrow foreign Original materials");
        }
        g_completed.clear();g_completed.push_back(record(other_root,mesh,foreign_materials,different,false,true));
        {
            GenericMatching::MeshIdentity identity;ConstructionScope scope;
            Check(!ReadGenericPristineMesh(adapter,root,candidate,identity) && !identity.mesh,
                "unverified shared Mesh was promoted to pristine geometry");
        }
        g_completed.clear();g_completed.push_back(record(root,different,own_materials,different,true,true));
        g_completed.push_back(record(other_root,mesh,own_materials,mesh,false,true));
        {
            GenericMatching::MeshIdentity identity;ConstructionScope scope;
            Check(!ReadGenericPristineMesh(adapter,root,candidate,identity) && identity.detail.find("neither")!=identity.detail.npos,
                "a foreign record bypassed unknown Mesh rejection on an owned receiver");
        }
        g_completed.clear();g_completed.push_back(record(root,mesh,own_materials,mesh,true,false));
        {
            GenericMatching::MeshIdentity identity;ConstructionScope scope;
            Check(!ReadGenericPristineMesh(adapter,root,candidate,identity) &&
                identity.origin==GenericMatching::DonorOrigin::CompletedWithoutOriginal,"generated Mesh without Original was accepted");
        }
        g_completed.clear();g_completed.push_back(record(other_root,mesh,own_materials,mesh,true,true));
        g_completed.push_back(record(GameObject("world(Clone)#34"),mesh,own_materials,different,true,true));
        {
            GenericMatching::MeshIdentity identity;ConstructionScope scope;
            Check(!ReadGenericPristineMesh(adapter,root,candidate,identity) && identity.origin==GenericMatching::DonorOrigin::Ambiguous,
                "conflicting verified Original identities were accepted");
        }
        g_completed.clear();
        // Restore only the recorded palette. Unrelated duplicate or unusable
        // children must not reject it, but a required bone must remain unique.
        fake_transform_type=Make("TransformType");
        host.resolve_class=[](void*,const char*,const char*,const char*,BE_ResolvedClassV1* result)->BE_Result {
            result->class_info=fake_transform_type;result->type_object=fake_transform_type;return BE_Result_Ok;
        };
        auto* original_bone=static_cast<Fake*>(ArrayValue(bones,0));original_bone->parent=root;
        auto* decoration1=Make("decoration");decoration1->parent=root;
        auto* decoration2=Make("decoration");decoration2->parent=root;
        auto* outside=Make("outside");
        auto* transforms=Array({original_bone,decoration1,decoration2,outside,nullptr});root->transforms=transforms;
        g_completed.push_back(record(root,mesh,own_materials,mesh,false,true));
        g_completed.back().bindings.front().original->bone_paths={"bone"};
        const auto restore=[&](bool expected) {
            PreparedBinding binding;binding.renderer=renderer;binding.receiver_key=key;
            binding.original_mesh=mesh;binding.original_materials=own_materials;binding.original_bones=bones;
            ConstructionScope scope;const bool restored=UseSavedOriginal(root,binding,true,true);
            Check(restored==expected,"Original palette did not distinguish unrelated children from a missing/ambiguous required bone");
            if (restored) Check(ArrayLength(DonorBones(binding))==1 && ArrayValue(DonorBones(binding),0)==original_bone,
                "Original palette mapped to an unrelated Transform");
            Check(renderer->mesh==mesh && renderer->bones==bones && renderer->materials==own_materials,
                "Original palette preparation mutated the live renderer");
        };
        restore(true);
        auto* ambiguous_bone=Make("bone");ambiguous_bone->parent=root;transforms->array.push_back(ambiguous_bone);
        restore(false);transforms->array.pop_back();
        original_bone->alive=false;restore(false);original_bone->alive=true;
        g_completed.clear();fake_transform_type=nullptr;
    }
    Check(roots==0 && tracked_handles.empty(),"instance lineage regression leaked GC handles");
    g_skinned_renderer_class={};g_weak_new=nullptr;g_weak_target=nullptr;g_host=nullptr;
    std::cout<<"PASS instance lineage: own-record priority, independent geometry/material ownership, Original palette isolation and missing/ambiguous required-bone rejection\n";
}
