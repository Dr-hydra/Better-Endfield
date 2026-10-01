// Tests the production v25 parser, palette/material preparation and commit
// rollback. No game or graphics device is touched.
#include "../modules/custom_model/module.cpp"
#include <iostream>
#include <cstdlib>
#include <sstream>
using namespace BetterEndfield::CustomModel;
namespace {
struct Fake {
    std::string name;
    std::vector<void*> array;
    void* bones=nullptr; void* poses=nullptr; void* materials=nullptr; void* mesh=nullptr;
    bool enabled=true;
    void* parent=nullptr;
    void* transforms=nullptr;
    Matrix4x4Raw matrix{};
    uint64_t scalar=0;
    std::map<int32_t,void*> textures;
    std::vector<uint8_t> data;
    int width=4, height=4, format=10;
    size_t destroy_calls=0;
};
std::vector<std::unique_ptr<Fake>> objects;
void* reject_material_renderer=nullptr;
bool rejected=false;
size_t roots=0;
void* fake_transform_type=nullptr;
Fake* Make(std::string name={}) {
    auto p=std::make_unique<Fake>(); p->name=std::move(name);
    for (int i=0;i<4;++i) p->matrix.m[i*5]=1;
    auto* result=p.get(); objects.push_back(std::move(p)); return result;
}
Fake* Array(std::initializer_list<void*> values) { auto* n=Make(); n->array=values; return n; }
void Check(bool result,const char* message) { if (!result) { std::cerr<<message<<'\n'; std::exit(1); } }
void* Scalar(uint64_t value) { auto* n=Make("scalar"); n->scalar=value; return n; }
void* BE_CALL InvokeFake(void*,const void* method,void* object,void** args,void** exception) {
    const auto key=std::string_view(static_cast<const MethodContract*>(method)->key);
    auto* n=static_cast<Fake*>(object);
    if (key=="object.get_type" || key=="type.get_element_type" || key=="component.get_transform") return object;
    if (key=="transform.get_parent") return n->parent;
    if (key=="game_object.renderers") return fake_transform_type && args[0]==fake_transform_type?n->transforms:object;
    if (key=="mesh.get_vertex_count") return Scalar(3);
    if (key=="mesh.get_sub_mesh_count") return Scalar(1);
    if (key=="mesh.get_index_count") return Scalar(6);
    if (key=="mesh.get_vertex_attribute_count") return Scalar(n->array.size());
    if (key=="mesh.get_vertex_attribute") return n->array.at(*static_cast<int*>(args[0]));
    if (key=="probe.material_shader") return Make("shader");
    if (key=="array.create") { auto* a=Make(); a->array.resize(*static_cast<int*>(args[1])); return a; }
    if (key=="array.get_length") return Scalar(n->array.size());
    if (key=="array.get_value") return n->array.at(*static_cast<int*>(args[0]));
    if (key=="array.set_value") { n->array.at(*static_cast<int*>(args[1]))=args[0]; return nullptr; }
    if (key=="array.clone") { auto* a=Make(); a->array=n->array; return a; }
    if (key=="object.get_name") return object;
    if (key=="object.instance_id") return Scalar(reinterpret_cast<uintptr_t>(object)&0x7fffffff);
    if (key=="object.is_alive") return Scalar(args[0]!=nullptr);
    if (key=="transform.local_to_world") return object;
    if (key=="mesh.get_bindposes") return n->poses;
    if (key=="skinned.get_bones") return n->bones;
    if (key=="skinned.set_bones") { n->bones=args[0]; return nullptr; }
    if (key=="skinned.get_shared_mesh") return n->mesh;
    if (key=="skinned.set_shared_mesh") { n->mesh=args[0]; return nullptr; }
    if (key=="renderer.get_shared_materials") return n->materials;
    if (key=="renderer.set_shared_materials") {
        if (object==reject_material_renderer && !rejected) { rejected=true; *exception=object; }
        else n->materials=args[0];
        return nullptr;
    }
    if (key=="renderer.get_enabled") return Scalar(n->enabled);
    if (key=="renderer.set_enabled") { n->enabled=*static_cast<bool*>(args[0]); return nullptr; }
    if (key=="material.copy") { auto* donor=static_cast<Fake*>(args[0]); n->name=donor->name; n->textures=donor->textures; return nullptr; }
    if (key=="material.get_texture_property_ids") { auto* a=Array({}); for (auto [id,tex]:n->textures) a->array.push_back(Scalar(id)); return a; }
    if (key=="material.get_texture_by_id") return n->textures.at(*static_cast<int*>(args[0]));
    if (key=="material.set_texture_by_id") { n->textures[*static_cast<int*>(args[0])]=args[1]; return nullptr; }
    if (key=="texture2d.ctor") { n->width=*static_cast<int*>(args[0]); n->height=*static_cast<int*>(args[1]); n->format=*static_cast<int*>(args[2]); return nullptr; }
    if (key=="texture2d.load_raw_texture_data") { auto* raw=*static_cast<uint8_t**>(args[0]); n->data.assign(raw,raw+*static_cast<int*>(args[1])); return nullptr; }
    if (key=="texture2d.apply") return nullptr;
    if (key=="texture.get_width") return Scalar(n->width);
    if (key=="texture.get_height") return Scalar(n->height);
    if (key=="texture.get_graphics_format") return Scalar(n->format);
    if (key=="graphics_format_utility.get_graphics_format") return Scalar(*static_cast<int*>(args[0]));
    if (key.starts_with("texture.get_")) return Scalar(0);
    if (key.starts_with("texture.set_")) return nullptr;
    if (key=="object.destroy") { ++static_cast<Fake*>(args[0])->destroy_calls; return nullptr; }
    std::cerr<<"Unhandled fake call "<<key<<'\n'; std::exit(1);
}
void* BE_CALL UnboxFake(void*,void* value) {
    auto* n=static_cast<Fake*>(value); return n->name=="scalar"?static_cast<void*>(&n->scalar):&n->matrix;
}
int BE_CALL StringFake(void*,const void* value,char* output,size_t size) {
    const auto& text=static_cast<const Fake*>(value)->name;
    if (size<=text.size()) return 0;
    std::memcpy(output,text.c_str(),text.size()+1); return static_cast<int>(text.size());
}
uint32_t BE_CALL RootFake(void*,void*,int) { ++roots; return 1; }
void BE_CALL FreeFake(void*,uint32_t) { --roots; }
void* BE_CALL NewFake(void*,const void*) { return Make(); }
void ParserTests(const std::filesystem::path& path) {
    BemPocData bem; std::string error;
    Check(LoadBem(path,bem,error),error.c_str());
    Check(bem.header.version==1 && bem.components[0].bones.size()==2 && bem.components[0].draws.size()==2,"BEMv1 structure");
    std::ifstream f(path,std::ios::binary); std::vector<uint8_t> bytes((std::istreambuf_iterator<char>(f)),{});
    auto corrupt=bytes; corrupt.pop_back();
    Check(!ParseBem(corrupt,bem,error) && bem.components.empty(),"truncation accepted");
    corrupt=bytes; corrupt[8]=2; Check(!ParseBem(corrupt,bem,error),"unsupported major accepted");
    Check(ParseBem(bytes,bem,error),"valid package rejected after malformed data");
    BemPocData optimized; BemLoadStats baseline_stats,optimized_stats;
    Check(LoadBem(path,bem,error,{},&baseline_stats),"baseline fixture load failed");
    Check(LoadBem(path,optimized,error,{},&optimized_stats,false,true),"optimized fixture load failed");
    Check(!bem.loading_optimization && optimized.loading_optimization,"loading optimization must be opt-in");
    Check(optimized_stats.payload_ids==baseline_stats.payload_ids &&
        optimized_stats.decoded_cache_remaining_bytes==0 && optimized_stats.payload_move_bytes>0 &&
        optimized_stats.payload_copy_bytes<baseline_stats.payload_copy_bytes,"optimized decoder did not release/move payloads");
    for(size_t i=0;i<bem.components.size();++i)
        Check(bem.components[i].streams==optimized.components[i].streams && bem.components[i].indices==optimized.components[i].indices,
            "optimized geometry bytes differ");
    for(size_t i=0;i<bem.textures.size();++i)
        Check(bem.textures[i].data==optimized.textures[i].data,"optimized texture bytes differ");
    Check(ParseBem(bytes,optimized,error,false,true) && optimized.loading_optimization,"optimized memory reader failed");
    corrupt=bytes; corrupt.pop_back();
    Check(!ParseBem(corrupt,optimized,error,false,true) && optimized.components.empty(),"optimized decoder accepted truncation");
    BE_HostApiV1 host{};
    host.runtime_invoke=InvokeFake; host.object_unbox=UnboxFake; host.copy_managed_string=StringFake;
    host.object_new=NewFake; host.gchandle_new=RootFake; host.gchandle_free=FreeFake;
    g_host=&host; g_material_class.class_info=&host; g_texture2d_class.class_info=&host;
    g_object_class=[](void* array)->void* { return array; };
    g_array_new_specific=[](void*,uintptr_t size)->void* { auto* a=Make(); a->array.resize(size); return a; };
    for (auto& method:g_methods) { method.method_info=&method; method.resolved=true; }
    {
        ConstructionScope scope;
        auto* r0=Make("renderer0"); auto* r1=Make("renderer1");
        auto* m0=Make("mesh0"); auto* m1=Make("mesh1");
        auto* bone0=Make("bone0"); auto* bone1=Make("bone1");
        auto* pose0=Make(); auto* pose1=Make(); pose1->matrix.m[12]=2;
        m0->poses=Array({pose0}); m1->poses=Array({pose1});
        r0->bones=Array({bone0}); r1->bones=Array({bone1});
        r0->materials=Array({Make("material0")}); r1->materials=Array({Make("material1")});
        auto* original_texture=Make("original");
        for (auto* r:{r0,r1}) static_cast<Fake*>(ArrayValue(r->materials,0))->textures[7]=original_texture;
        r0->mesh=m0; r1->mesh=m1;
        std::vector<PreparedBinding> bindings(2);
        for (int i=0;i<2;++i) {
            auto* r=i?r1:r0; auto& b=bindings[i]; b.component_id=i; b.renderer=r;
            b.original_mesh=r->mesh; b.custom_mesh=Make("replacement"); b.original_bones=r->bones;
            b.original_materials=r->materials; b.custom_materials=r->materials;
        }
        void* poses=nullptr;
        Check(PreparePalette(bem.components[0],bindings[0],bindings,poses),"palette preparation failed");
        Check(ArrayValue(bindings[0].custom_bones,1)==bone1 && ArrayValue(poses,1)==pose1,"cross-source bone/pose pair lost");
        Check(r0->bones==bindings[0].original_bones,"palette published before commit");
        Check(PrepareDrawMaterials(bem.components[0],bindings[0],bindings,bem),"draw materials failed");
        void* a=ArrayValue(bindings[0].custom_materials,0); void* b=ArrayValue(bindings[0].custom_materials,1);
        Check(a!=b && ObjectName(a)=="material0" && ObjectName(b)=="material1","donor materials not separated");
        auto* tex_a=static_cast<Fake*>(static_cast<Fake*>(a)->textures[7]);
        auto* tex_b=static_cast<Fake*>(static_cast<Fake*>(b)->textures[7]);
        Check(tex_a!=tex_b && tex_a->data[0]==1 && tex_b->data[0]==2 && original_texture->data.empty(),"same-name per-draw textures leaked across materials");
        // Typhoea cloth_05 shares one native mask between clear-coat and
        // fresnel properties. Both slots must retain one replacement object.
        auto* alias_material=Make("alias_material"); auto* unrelated=Make("unrelated");
        alias_material->textures={{7,original_texture},{42,original_texture},{99,unrelated}};
        std::map<std::pair<size_t,void*>,void*> alias_cache;
        Check(ApplyTextureMask(alias_material,1,bem,alias_cache),"same source texture in multiple slots rejected");
        Check(alias_material->textures[7]==alias_material->textures[42] &&
            alias_material->textures[7]!=original_texture && alias_cache.size()==1,
            "shared texture properties did not receive one replacement");
        Check(alias_material->textures[99]==unrelated && original_texture->data.empty(),
            "shared-slot replacement changed an unrelated slot or original texture");
        auto* ambiguous_material=Make("ambiguous_material"); auto* other_original=Make("original");
        ambiguous_material->textures={{7,original_texture},{42,other_original}};
        std::map<std::pair<size_t,void*>,void*> refused_cache;
        Check(!ApplyTextureMask(ambiguous_material,1,bem,refused_cache) && refused_cache.empty() &&
            ambiguous_material->textures[7]==original_texture && ambiguous_material->textures[42]==other_original,
            "distinct same-name source textures accepted or changed");
        auto* missing_material=Make("missing_material"); missing_material->textures={{99,unrelated}};
        Check(!ApplyTextureMask(missing_material,1,bem,refused_cache) && refused_cache.empty() &&
            missing_material->textures[99]==unrelated,"missing texture name was guessed or changed");
        auto* conflict_material=Make("conflict_material"); conflict_material->textures={{7,original_texture},{42,original_texture}};
        Check(!ApplyTextureMask(conflict_material,3,bem,refused_cache),"two payloads for the same slots accepted");
        auto unchecked_bem=bem; unchecked_bem.skip_validation=true;
        auto* unchecked_material=Make("unchecked_material");
        unchecked_material->textures={{7,original_texture},{42,other_original},{99,unrelated}};
        std::map<std::pair<size_t,void*>,void*> unchecked_cache;
        Check(ApplyTextureMask(unchecked_material,1,unchecked_bem,unchecked_cache) &&
            unchecked_material->textures[7]==unchecked_material->textures[42] &&
            unchecked_material->textures[7]!=original_texture,"developer mode still rejected ambiguous texture names");
        Check(ApplyTextureMask(missing_material,1,unchecked_bem,unchecked_cache) &&
            missing_material->textures[99]==unrelated,"developer mode changed an unmatched texture");
        auto repeated=bem.components[0]; repeated.draws[1]=repeated.draws[0]; repeated.material_names[1]=repeated.material_names[0];
        auto repeated_target=bindings[0];
        Check(PrepareDrawMaterials(repeated,repeated_target,bindings,bem),"repeated material preparation failed");
        auto* repeat_a=static_cast<Fake*>(ArrayValue(repeated_target.custom_materials,0));
        auto* repeat_b=static_cast<Fake*>(ArrayValue(repeated_target.custom_materials,1));
        Check(repeat_a!=repeat_b && repeat_a->textures[7]==repeat_b->textures[7],"same immutable payload/sampler not reused");
        for(bool optimize:{false,true}) {
            Fake* first_texture=nullptr; Fake* second_texture=nullptr;
            {
                ConstructionScope transaction;
                auto experimental=bem; experimental.loading_optimization=optimize;
                auto single=bem.components[0]; single.draws.resize(1); single.material_names.resize(1);
                auto first_target=bindings[0],second_target=bindings[1];
                std::map<std::pair<size_t,void*>,void*> cache;
                Check(PrepareDrawMaterials(single,first_target,bindings,experimental,&cache) &&
                    PrepareDrawMaterials(single,second_target,bindings,experimental,&cache),"transaction materials failed");
                first_texture=static_cast<Fake*>(static_cast<Fake*>(ArrayValue(first_target.custom_materials,0))->textures[7]);
                second_texture=static_cast<Fake*>(static_cast<Fake*>(ArrayValue(second_target.custom_materials,0))->textures[7]);
                Check((first_texture==second_texture)==optimize,"cross-component texture reuse ignored experimental flag");
                Check(cache.size()==(optimize?1:0),"disabled optimization populated shared cache");
                // A different source Texture keeps its own copied sampler state.
                auto* different_material=Make("material0"); different_material->textures={{7,other_original}};
                auto distinct=bindings[0]; distinct.original_materials=Array({different_material}); distinct.donor_materials=nullptr;
                auto keep=single; keep.keep_material_names={"material0"}; keep.keep_material_overrides={{0,0,1}};
                Check(PrepareKeepMaterials(keep,distinct,experimental,&cache),"different source texture preparation failed");
                auto* distinct_texture=static_cast<Fake*>(static_cast<Fake*>(ArrayValue(distinct.custom_materials,0))->textures[7]);
                Check(distinct_texture!=first_texture,"different sampler source was incorrectly merged");
                if(optimize) Check(cache.size()==2,"source Texture identity missing from transaction key");
                auto invalid=single; invalid.material_names[0]="wrong";
                Check(!PrepareDrawMaterials(invalid,second_target,bindings,experimental,&cache),"failure fixture unexpectedly prepared");
                // Leave transaction unpublished to exercise shared asset cleanup.
            }
            Check(first_texture->destroy_calls==1 && second_texture->destroy_calls==1,
                "failed transaction leaked or destroyed shared texture twice");
            Check(original_texture->destroy_calls==0 && other_original->destroy_calls==0,
                "failed texture transaction destroyed original assets");
        }
        auto bad_component=bem.components[0]; bad_component.bone_names[1]="wrong";
        auto temporary=bindings[0];
        Check(!PreparePalette(bad_component,temporary,bindings,poses),"wrong bone identity accepted");
        r1->matrix.m[12]=3; temporary=bindings[0];
        Check(!PreparePalette(bem.components[0],temporary,bindings,poses),"different mesh space accepted");
        r1->matrix.m[12]=0;
        reject_material_renderer=r1;
        Check(CommitResource<PreparedBinding>(bindings,ApplyPreparedBinding,RestorePreparedBinding)==CommitResult::Restored,"partial failure not rolled back");
        Check(r0->bones==bindings[0].original_bones && r0->mesh==m0 && r0->materials==bindings[0].original_materials,"rollback did not restore complete renderer");
        Check(CommitResource<PreparedBinding>(bindings,ApplyPreparedBinding,RestorePreparedBinding)==CommitResult::Committed,"commit rejected");
        scope.published=true;
    }
    Check(roots==0,"construction roots leaked");
    g_host=nullptr;
}
void RegistryTests(const std::filesystem::path& path) {
    auto filename=path.filename().u8string();
    const std::string config="[CustomModel]\nstandalone_lod=true\n[Mod.test]\nenabled=true\npackage="+
        std::string(reinterpret_cast<const char*>(filename.data()),filename.size())+"\nappearance=hidden\n";
    ModRegistry registry; std::string error;
    Check(ParseModRegistry(config,path.parent_path(),registry,error),error.c_str());
    ModRegistry moved=std::move(registry);
    Check(moved.standalone_lod && moved.enabled.size()==1 && moved.enabled[0].appearance=="hidden","appearance/LOD selection lost");
    Check(!moved.skip_validation && !moved.enabled[0].skip_validation,"validation must be enabled by default");
    Check(moved.Match("world(Clone)") && std::string(moved.Match("ui")->adapter->components[1].name)=="mesh1","owned adapter invalid after move");
    Check(ParseModRegistry(config+"[Mod.duplicate]\nenabled=true\npackage="+
        std::string(reinterpret_cast<const char*>(filename.data()),filename.size())+"\n",path.parent_path(),registry,error),error.c_str());
    Check(registry.enabled.empty(),"conflicting enabled packages were not disabled");
    std::string unchecked_config=config;
    unchecked_config.insert(unchecked_config.find('\n')+1,"skip_validation=true\n");
    Check(ParseModRegistry(unchecked_config,path.parent_path(),registry,error) && registry.skip_validation &&
        registry.enabled.size()==1 && registry.enabled[0].skip_validation,"developer option not passed to the package loader");
    unchecked_config.replace(unchecked_config.find("skip_validation=true"),20,"skip_validation=invalid");
    Check(!ParseModRegistry(unchecked_config,path.parent_path(),registry,error),"malformed developer option accepted");
    std::string experimental=config;
    experimental.insert(experimental.find('\n')+1,"hot_switch=true\nloading_optimization=true\n");
    Check(ParseModRegistry(experimental,path.parent_path(),registry,error) && registry.hot_switch &&
        registry.loading_optimization && registry.enabled[0].loading_optimization,"experimental flags not propagated");
    auto owner=registry.owned_adapters[0]; auto key=registry.enabled[0].selection_key;
    Check(ParseModRegistry(config,path.parent_path(),registry,error) && !registry.hot_switch && !registry.loading_optimization,
        "experimental switches not disabled by default");
    Check(std::string(owner->adapter.id)=="chr_test","old adapter lifetime lost across registry replacement");
    Check(registry.enabled[0].selection_key!=key,"optimization absent from selection cache identity");
    const auto default_key=registry.enabled[0].selection_key;
    std::string default_selection=config; default_selection.replace(default_selection.find("appearance=hidden"),17,"appearance=default");
    Check(ParseModRegistry(default_selection,path.parent_path(),registry,error) && registry.enabled[0].selection_key!=default_key,
        "appearance/options absent from selection cache identity");
    g_registry=std::move(registry); g_registry.hot_switch=true; g_registry_root=path.parent_path();
    const auto working_key=g_registry.enabled[0].selection_key;
    Check(!InstallRegistryUpdate("[CustomModel]\nhot_switch=true\n[Mod.bad]\nenabled=true\npackage=missing.bem\n") &&
        g_registry.enabled.size()==1 && g_registry.enabled[0].selection_key==working_key,
        "metadata-refused candidate was treated as disabling the working package");
    Check(!InstallRegistryUpdate("[CustomModel]\nhot_switch=true\nloading_optimization=true\n") &&
        g_registry.enabled.size()==1,"startup experiment flags changed during hot switch");
    Check(InstallRegistryUpdate("[CustomModel]\nhot_switch=true\n") && g_registry.enabled.empty(),
        "explicitly disabling all packages rejected");
    std::string reenabling=default_selection; reenabling.insert(reenabling.find('\n')+1,"hot_switch=true\n");
    Check(InstallRegistryUpdate(reenabling) && g_registry.enabled.size()==1,"re-enabling a valid package rejected");
    g_registry={}; g_payload_cache.clear();
    experimental.replace(experimental.find("hot_switch=true"),15,"hot_switch=invalid");
    Check(!ParseModRegistry(experimental,path.parent_path(),registry,error),"invalid hot switch flag accepted");
}
std::map<uint32_t,void*> tracked_handles;
uint32_t next_handle=1;
uint32_t BE_CALL TrackedRoot(void*,void* object,int) { const auto handle=next_handle++; tracked_handles[handle]=object; ++roots; return handle; }
void BE_CALL TrackedFree(void*,uint32_t handle) { Check(tracked_handles.erase(handle)==1,"invalid hot-switch handle release"); --roots; }
void HotSwitchTests(const std::filesystem::path& path) {
    BemPocData payload; std::string error; Check(LoadBem(path,payload,error),error.c_str());
    // Keep geometry lets the fixture exercise the production donor/material,
    // resource identity and rollback paths without a graphics device.
    for (auto& component:payload.components) {
        component.info.flags=kComponentFlagNoGeometry; component.info.original_index_count=6;
        component.keep_material_overrides={{0,0,1}};
        component.keep_material_names={"material"+std::to_string(component.info.component_id)};
    }
    BE_HostApiV1 host{}; host.runtime_invoke=InvokeFake; host.object_unbox=UnboxFake; host.copy_managed_string=StringFake;
    host.object_new=NewFake; host.gchandle_new=TrackedRoot; host.gchandle_free=TrackedFree;
    g_host=&host; g_material_class.class_info=&host; g_texture2d_class.class_info=&host;
    fake_transform_type=Make("TransformType");
    host.resolve_class=[](void*,const char*,const char*,const char* name,BE_ResolvedClassV1* type)->BE_Result {
        if (std::string_view(name)!="Transform") return BE_Result_NotReady;
        type->class_info=fake_transform_type; type->type_object=fake_transform_type; return BE_Result_Ok;
    };
    g_weak_new=[](void* object,bool)->uint32_t { return TrackedRoot(nullptr,object,0); };
    g_weak_target=[](uint32_t handle)->void* { const auto found=tracked_handles.find(handle); return found==tracked_handles.end()?nullptr:found->second; };
    g_object_class=[](void* array)->void* { return array; };
    g_array_new_specific=[](void*,uintptr_t count)->void* { auto* a=Make(); a->array.resize(count); return a; };
    for (auto& method:g_methods) { method.method_info=&method; method.resolved=true; }
    auto owner=std::make_shared<OwnedCharacterAdapter>(); owner->id="chr_test"; owner->world="world"; owner->ui="ui";
    owner->names={"mesh0","mesh1"}; for (const auto& name:owner->names) owner->components.push_back({name.c_str(),6});
    owner->adapter={owner->id.c_str(),owner->world.c_str(),owner->ui.c_str(),"",false,owner->components};
    g_registry={}; g_registry.hot_switch=true; g_registry.owned_adapters.push_back(owner); g_hot_switch_runtime=true;
    auto* asset=Make("world"); auto* original_texture=Make("original");
    std::array<Fake*,2> renderers{},original_materials{};
    for (int i=0;i<2;++i) {
        auto* renderer=Make("mesh"+std::to_string(i)); renderer->parent=asset;
        renderer->mesh=Make(renderer->name); renderer->bones=Array({Make("bone"+std::to_string(i))});
        static_cast<Fake*>(renderer->mesh)->poses=Array({Make()});
        auto* material=Make("material"+std::to_string(i)); material->textures[7]=original_texture;
        renderer->materials=original_materials[i]=Array({material}); renderers[i]=renderer; asset->array.push_back(renderer);
    }
    auto publish=[&](const char* key,uint8_t value,bool reject) {
        ConstructionScope scope; payload.textures[0].data.assign(8,value);
        std::vector<PreparedBinding> bindings;
        Check(PrepareResource(owner->adapter,payload,asset,bindings),"hot switch could not rebuild from pristine donor");
        if (!g_completed.empty()) Check(DonorMaterials(bindings[0])==original_materials[0],"hot switch used previous package as material donor");
        CompletedResource record; Check(RememberResource(owner->adapter,asset,bindings,record,key),"hot switch original snapshot not saved");
        reject_material_renderer=reject?renderers[1]:nullptr; rejected=false;
        const auto result=CommitResource<PreparedBinding>(bindings,ApplyPreparedBinding,RestorePreparedBinding);
        if (reject) { Check(result==CommitResult::Restored,"failed package switch not rolled back"); return; }
        Check(result==CommitResult::Committed,"hot switch commit failed"); scope.published=true; PublishCompleted(std::move(record),asset);
        Check(IsCompletedResource(owner->adapter,asset,key),"current selection not deduplicated");
        Check(!IsCompletedResource(owner->adapter,asset,"other"),"previous selection blocked a new package");
        Check(static_cast<Fake*>(static_cast<Fake*>(ArrayValue(renderers[0]->materials,0))->textures[7])->data[0]==value,
            "wrong package texture after hot switch");
        Check(std::count_if(g_completed.begin(),g_completed.end(),[&](const auto& record){return record.root.Get()==asset;})==1,
            "cached-root switches accumulated active completed generations");
    };
    publish("A",1,false); auto* a_materials=renderers[0]->materials; auto* a_materials1=renderers[1]->materials;
    publish("B",2,false); auto* b_materials=renderers[0]->materials;
    Check(a_materials!=b_materials,"A to B reused generated materials");
    {
        ConstructionScope scope; auto* clone=Make("world(Clone)");
        auto* clone0=Make("mesh0"); clone0->parent=clone; clone0->mesh=renderers[0]->mesh;
        clone0->materials=a_materials; auto* bone=Make("bone0"); bone->parent=clone;
        clone0->bones=Array({bone}); clone->array={clone0}; clone->transforms=Array({bone});
        PreparedBinding binding; binding.component_id=1; binding.renderer=clone0;
        binding.original_mesh=clone0->mesh; binding.original_bones=clone0->bones; binding.original_materials=clone0->materials;
        Check(UseSavedOriginal(clone,binding) && binding.saved_original && DonorMaterials(binding)==original_materials[0] &&
            ArrayValue(DonorBones(binding),0)==bone,
            "old natural clone lost original renderer donor after template A to B");
        Check(binding.original_materials==a_materials,"clone rollback stopped pointing at package A");
        auto* clone1=Make("mesh1"); clone1->parent=clone; clone1->mesh=renderers[1]->mesh;
        clone1->materials=a_materials1; clone1->bones=renderers[1]->bones; clone->array.push_back(clone1);
        Check(IsCompletedResource(owner->adapter,clone,"A") && !IsCompletedResource(owner->adapter,clone,"B"),
            "retired clone lineage confused old and current selection");
    }
    {
        ConstructionScope scope; const auto mesh_before=renderers[0]->mesh,bones_before=renderers[0]->bones;
        auto* previous_mesh=Make("previous-custom"); previous_mesh->poses=Array({Make("wrong-pose")});
        renderers[0]->mesh=previous_mesh; renderers[0]->bones=Array({Make("wrong-bone")});
        std::vector<PreparedBinding> rebinding(2);
        for (int i=0;i<2;++i) {
            auto& binding=rebinding[i]; binding.component_id=i; binding.renderer=renderers[i];
            binding.original_mesh=renderers[i]->mesh; binding.original_bones=renderers[i]->bones;
            binding.original_materials=renderers[i]->materials;
            Check(UseSavedOriginal(asset,binding),"saved geometry donor unavailable");
        }
        auto reordered=rebinding[0]; reordered.component_id=1;
        reordered.donor_mesh=nullptr; reordered.donor_materials=nullptr; reordered.donor_bones=nullptr;
        reordered.saved_original.reset();
        Check(UseSavedOriginal(asset,reordered) && reordered.saved_original &&
            DonorMesh(reordered)==mesh_before && DonorMaterials(reordered)==original_materials[0],
            "target component ID reorder lost the original renderer donor");
        void* poses=nullptr;
        Check(PreparePalette(payload.components[0],rebinding[0],rebinding,poses) &&
            ObjectName(ArrayValue(rebinding[0].custom_bones,0))=="bone0" &&
            ObjectName(ArrayValue(rebinding[0].custom_bones,1))=="bone1",
            "new palette borrowed previous custom mesh/bones instead of pristine donors");
        Check(rebinding[0].original_mesh==previous_mesh && DonorMesh(rebinding[0])==mesh_before,
            "rollback and pristine geometry donors were conflated");
        renderers[0]->mesh=mesh_before; renderers[0]->bones=bones_before;
    }
    publish("failed",3,true); Check(renderers[0]->materials==b_materials && IsCompletedResource(owner->adapter,asset,"B"),
        "failed B replacement lost previous package");
    publish("A",1,false);
    { ConstructionScope scope; Check(RestoreDisabledResource(asset,"world",scope),"disabling cached package failed"); }
    Check(renderers[0]->materials==original_materials[0] && renderers[1]->materials==original_materials[1],
        "disable did not restore pristine materials");
    publish("options-changed",2,false);
    g_completed.clear(); g_registry={}; g_hot_switch_runtime=false; g_weak_new=nullptr; g_weak_target=nullptr; fake_transform_type=nullptr;
    Check(roots==0 && tracked_handles.empty(),"hot switch original/weak handles leaked"); g_host=nullptr;
    std::cout<<"PASS experimental hot switch: cached A-B-A, option identity, disable, rollback, original donor and ownership\n";
}
void ProbeTests(const std::filesystem::path& output) {
    Check(!Contract("probe.texture_names") && !Contract("probe.texture_by_name"),"probe still requires stripped name APIs");
    Check(ProbeCanonicalPath("world(Clone)/child(Clone)")=="world/child(Clone)","clone root normalization changed children");
    NativeProbeState request;
    std::istringstream good("BE_NATIVE_PROBE_V1\ntest\nversion\n\nworld\tworld/mesh\tmesh\t6\tid:1\n");
    Check(ParseProbeRequest(good,request),"probe request rejected");
    NativeProbeState bad;
    std::istringstream traversal("BE_NATIVE_PROBE_V1\n../escape\nversion\n\nworld\tworld/mesh\tmesh\t6\tid:1\n");
    Check(!ParseProbeRequest(traversal,bad),"unsafe run ID accepted");
    bad={}; std::istringstream duplicate("BE_NATIVE_PROBE_V1\ntest\nversion\n\nworld\tworld/mesh\tmesh\t6\tid:1\nworld\tworld/mesh\tmesh\t6\tid:1\n");
    Check(!ParseProbeRequest(duplicate,bad),"duplicate probe identity accepted");
    Check(ProbeJson("a\n\"\\")=="\"a\\u000a\\\"\\\\\"","JSON escaping failed");
    std::filesystem::create_directories(output);
    g_probe={}; g_probe.run="fixture-"+std::to_string(GetTickCount64()); g_probe.manifest="version";
    g_probe.request=output/(g_probe.run+".request"); g_probe.output=output/(g_probe.run+".jsonl"); g_probe.active=true;
    std::ofstream(g_probe.request)<<"fixture";
    BE_HostApiV1 host{}; host.runtime_invoke=InvokeFake; host.object_unbox=UnboxFake;
    host.copy_managed_string=StringFake; host.gchandle_new=RootFake; host.gchandle_free=FreeFake;
    g_host=&host;
    for (auto& method:g_methods) { method.method_info=&method; method.resolved=true; }
    g_engine.resolved=true;
    g_engine.get_vertex_buffer_count=[](void*)->int32_t { return 3; };
    g_engine.get_vertex_buffer_stride=[](void*,int s)->int32_t { return s==0?16:12; };
    std::vector<Fake*> resources;
    for(const auto root:{"world","ui"}) {
        auto* asset=Make(std::string(root)+(std::string_view(root)=="ui"?"(Clone)":"")); auto* renderer=Make("mesh"); renderer->parent=asset; asset->array={renderer};
        auto* mesh=Make("mesh"); renderer->mesh=mesh;
        for(const auto a:std::array<VertexAttributeDescriptorRaw,6>{{{0,0,3,0},{1,3,4,0},{4,0,2,1},{6,3,4,1},{12,4,4,2},{13,6,4,2}}}) {
            auto* boxed=Make(); std::memcpy(&boxed->matrix,&a,sizeof(a)); mesh->array.push_back(boxed);
        }
        auto* bone=Make("bone"); bone->parent=asset; renderer->bones=Array({bone});
        auto* material=Make("material"); material->textures[7]=Make("texture"); renderer->materials=Array({material});
        g_probe.items.push_back({root,std::string(root)+"/mesh","mesh",std::string(root)+":1",6});
        resources.push_back(asset);
    }
    { ConstructionScope scope; CaptureNativeProbe(Make("unrelated")); }
    Check(!std::filesystem::exists(g_probe.output),"unrelated resource recorded");
    for(auto* asset:resources) {
        auto* renderer=static_cast<Fake*>(asset->array[0]); const auto mesh=renderer->mesh;
        { ConstructionScope scope; CaptureNativeProbe(asset); }
        Check(renderer->mesh==mesh && renderer->enabled,"probe changed resource");
        const auto size=std::filesystem::file_size(g_probe.output);
        { ConstructionScope scope; CaptureNativeProbe(asset); }
        Check(std::filesystem::file_size(g_probe.output)==size,"probe did not deduplicate");
    }
    Check(!g_probe.active && !std::filesystem::exists(g_probe.request),"probe did not disarm");
    Check(roots==0,"probe roots leaked");
    NativeProbeState sweep;
    std::istringstream sweep_request("BE_NATIVE_PROBE_SWEEP_V1\nsweep-test\nversion\n\nchr_0004_pelica_uimodel\nchr_0003_endminf_postmodel\n");
    Check(ParseProbeRequest(sweep_request,sweep) && sweep.sweep && sweep.resources.size()==2,"sweep request not parsed");
    NativeProbeState unsafe_sweep;
    std::istringstream unsafe_request("BE_NATIVE_PROBE_SWEEP_V1\nsweep-test\nversion\n\nchr_0004_pelica/../../uimodel\n");
    Check(!ParseProbeRequest(unsafe_request,unsafe_sweep),"unsafe sweep resource accepted");
    NativeProbeState persistent_request;
    std::istringstream persistent_text("BE_NATIVE_PROBE_SWEEP_PERSIST_V1\npersistent-test\nversion\n\nchr_0004_pelica_uimodel\n");
    Check(ParseProbeRequest(persistent_text,persistent_request) && persistent_request.sweep && persistent_request.persistent,"persistent request rejected");
    g_probe={};g_probe.sweep=true;g_probe.active=true;g_probe.run="sweep-"+std::to_string(GetTickCount64());g_probe.manifest="version";
    g_probe.output=output/(g_probe.run+".jsonl");g_probe.request=output/"native-probe.request";
    g_probe.resources={"chr_0004_pelica_uimodel","chr_0003_endminf_postmodel","chr_0009_unseen_uimodel"};
    for(size_t i=0;i<resources.size();++i) {
        auto* asset=resources[i]; asset->name=i?"chr_0003_endminf_postmodel":"chr_0004_pelica_uimodel";
        auto* renderer=static_cast<Fake*>(asset->array[0]); auto* mesh=static_cast<Fake*>(renderer->mesh);
        mesh->poses=Array({Make("bindpose")});
        { ConstructionScope scope;CaptureNativeProbe(asset); }
        Check(g_probe.done.contains(asset->name),"sweep did not discover renderer without offline target");
        const auto size=std::filesystem::file_size(g_probe.output);
        { ConstructionScope scope;CaptureNativeProbe(asset); }
        Check(std::filesystem::file_size(g_probe.output)==size,"sweep duplicate capture");
        Check(renderer->mesh==mesh && renderer->enabled,"sweep changed native renderer");
    }
    Check(g_probe.active,"sweep stopped before unobserved resources");
    std::ofstream(output/"native-probe.stop")<<g_probe.run;
    TickNativeProbe();Check(!g_probe.active && g_probe.sweep,"stop did not retain clean-session replacement pause");
    g_probe.persistent=true;g_probe.active=true;g_probe.done.clear();g_probe.attempts.clear();
    g_probe.run+="-persistent";g_probe.output=output/(g_probe.run+".jsonl");g_probe.resources={resources[0]->name};
    { ConstructionScope scope;CaptureNativeProbe(resources[0]); }
    Check(g_probe.done.size()==1 && g_probe.active,"persistent sweep disarmed after all resources in a process");
    Check(roots==0,"sweep roots leaked");
    g_host=nullptr;
    std::cout<<"PASS: probe request bounds, JSON escaping, read-only collection, deduplication, disarming, root release\n";
}
struct ProbeStartupContext { std::string catalog; bool armed=false; };
void ProbeDllStartup(const std::filesystem::path& dll,const std::filesystem::path& catalog) {
    auto encoded=std::filesystem::absolute(catalog).u8string();
    ProbeStartupContext context{std::string(reinterpret_cast<const char*>(encoded.data()),encoded.size())};
    BE_HostApiV1 host{}; host.abi_version=BETTER_ENDFIELD_MODULE_ABI_V1; host.context=&context;
    host.log=[](void* c,const char*,const char* message) { auto& state=*static_cast<ProbeStartupContext*>(c);
        if(std::string_view(message).starts_with("Native probe armed:")) state.armed=true;
        std::cout<<message<<'\n'; };
    host.copy_catalog_root=[](void* c,char* out,size_t size)->int { const auto& path=static_cast<ProbeStartupContext*>(c)->catalog;
        if(size<=path.size()) return 0; std::memcpy(out,path.c_str(),path.size()+1); return static_cast<int>(path.size()); };
    host.resolve_method=[](void*,const BE_MethodDescriptorV1*,BE_ResolvedMethodV1*) { return BE_Result_NotReady; };
    host.resolve_field=[](void*,const BE_FieldDescriptorV1*,BE_ResolvedFieldV1*) { return BE_Result_NotReady; };
    host.resolve_class=[](void*,const char*,const char*,const char*,BE_ResolvedClassV1*) { return BE_Result_NotReady; };
    host.create_hook=[](void*,const char*,void*,void*,void**) { Check(false,"startup check must never install hooks"); return BE_Result_Failed; };
    host.runtime_invoke=InvokeFake; host.object_unbox=UnboxFake; host.copy_managed_string=StringFake;
    host.object_new=NewFake; host.string_new=[](void*,const char* name)->void* { return Make(name); };
    host.gchandle_new=RootFake; host.gchandle_free=FreeFake;
    HMODULE library=LoadLibraryExW(std::filesystem::absolute(dll).c_str(),nullptr,LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR|LOAD_LIBRARY_SEARCH_DEFAULT_DIRS);
    Check(library!=nullptr,"probe DLL load failed");
    auto api=reinterpret_cast<BE_GetModuleApiV1Fn>(GetProcAddress(library,"BetterEndfield_GetModuleApiV1"));
    Check(api!=nullptr,"module API missing");
    Check(api()->initialize(&host)==BE_Result_ContractMismatch,"offline startup should stop at unavailable game contracts");
    Check(context.armed,"actual DLL did not arm the real request");
    FreeLibrary(library);
    std::cout<<"PASS: actual DLL export reads request via Host API before any game calls/hooks\n";
}
}
int main(int argc,char** argv) {
    if(argc==3 && std::string_view(argv[1])=="--index32") {
        std::ifstream file(argv[2],std::ios::binary);
        std::vector<uint8_t> bytes((std::istreambuf_iterator<char>(file)),{});
        BemPocData bem;std::string error;
        Check(ParseBem(bytes,bem,error),error.c_str());
        const auto& c=bem.components[0];
        Check(c.info.vertex_count==65538 && c.info.index_element_size==4,"large fixture missing");
        uint32_t last=0;std::memcpy(&last,c.indices.data()+65537*4,4);Check(last==65537,"32-bit index truncated");
        static int format=-1,count=0,uploaded=0;
        g_engine.set_index_buffer_params=[](void*,int32_t n,int32_t f) { count=n;format=f; };
        g_engine.set_index_buffer_data=[](void*,const void*,int32_t,int32_t,int32_t n,int32_t element,int32_t) { Check(element==1,"byte upload changed");uploaded=n; };
        Check(UploadComponentIndices(nullptr,c),"32-bit upload rejected");
        Check(format==1 && count==static_cast<int>(c.info.index_count) && uploaded==static_cast<int>(c.indices.size()),"32-bit upload format/count mismatch");
        BemComponent small;small.info.index_count=3;small.info.index_element_size=2;small.indices.resize(6);
        Check(UploadComponentIndices(nullptr,small) && format==0 && count==3 && uploaded==6,"16-bit upload changed");
        small.indices.pop_back();Check(!UploadComponentIndices(nullptr,small),"malformed upload accepted");
        std::cout<<"PASS: uint32 parser bounds and production index upload; uint16 path preserved\n";return 0;
    }
    if(argc==3 && std::string_view(argv[1])=="--validate-package") {
        BemPocData package; std::string error;
        Check(LoadBem(argv[2],package,error),error.c_str());
        Check(package.header.version==1,"expected BEMv1 package");
        for(const auto& c:package.components)
            std::cout<<"C"<<c.info.component_id<<" vertices="<<c.info.vertex_count
                <<" indices="<<c.info.index_count<<" palette="<<c.bones.size()<<" draws="<<c.draws.size()<<'\n';
        std::cout<<"PASS: production parser accepted BEMv1 package; textures="<<package.textures.size()<<'\n';
        return 0;
    }
    if(argc==4 && std::string_view(argv[1])=="--probe-dll") { ProbeDllStartup(argv[2],argv[3]); return 0; }
    if (argc==3 && std::string_view(argv[1])=="--probe") { ProbeTests(argv[2]); return 0; }
    Check(argc>=2,"pass synthetic BEMv1 package path");
    ParserTests(argv[1]); RegistryTests(argv[1]); HotSwitchTests(argv[1]);
    std::cout<<"PASS: BEMv1 parser, exact donor identity, material isolation, rollback, ownership, appearance/LOD routing\n";
}
