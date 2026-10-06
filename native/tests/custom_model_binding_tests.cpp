#include <cstring>
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
    bool visible=false;
    bool alive=true;
    void* parent=nullptr;
    void* transforms=nullptr;
    void* shader=nullptr;
    void* runtime_class=nullptr;
    void* filter=nullptr;
    void* transform=nullptr;
    void* game_object=nullptr;
    Matrix4x4Raw matrix{};
    uint64_t scalar=0;
    std::map<int32_t,void*> textures;
    std::map<std::string,uint64_t> props; // Texture sampler properties (raw 32-bit values)
    std::vector<uint8_t> data;
    int width=4, height=4, format=10;
    size_t destroy_calls=0;
};
std::vector<std::unique_ptr<Fake>> objects;
void* reject_material_renderer=nullptr;
bool rejected=false;
size_t roots=0;
uint64_t fake_frame=0,texture_ctors=0,texture_applies=0,finish_calls=0;
int32_t fake_max_lod=1;
void* fake_transform_type=nullptr;
void* fake_camera=nullptr;
void* fake_scene_renderers=nullptr;
int fake_game_object_class_token=0;
uint64_t game_object_calls=0;
uint64_t fake_runtime_calls=0;
void* last_finished_asset=nullptr;
void* FakeObjectClass(void* object) {
    auto* value=static_cast<Fake*>(object);
    return value->runtime_class?value->runtime_class:object;
}
void ConfigureFakeObjectClasses() {
    g_game_object_class.class_info=&fake_game_object_class_token;
    g_game_object_class.type_object=&fake_game_object_class_token;
    g_object_class=&FakeObjectClass;
}
Fake* Make(std::string name={}) {
    auto p=std::make_unique<Fake>(); p->name=std::move(name);
    for (int i=0;i<4;++i) p->matrix.m[i*5]=1;
    auto* result=p.get(); objects.push_back(std::move(p)); return result;
}
Fake* GameObject(std::string name={}) {
    auto* object=Make(std::move(name));object->runtime_class=&fake_game_object_class_token;object->game_object=object;return object;
}
Fake* Array(std::initializer_list<void*> values) { auto* n=Make(); n->array=values; return n; }
void Check(bool result,const char* message) { if (!result) { std::cerr<<message<<'\n'; std::exit(1); } }
void* Scalar(uint64_t value) { auto* n=Make("scalar"); n->scalar=value; return n; }
// Original materials are held weakly and re-collected into a fresh array.
bool SameMaterialSet(void* actual,void* expected) {
    return actual && expected && static_cast<Fake*>(actual)->array==static_cast<Fake*>(expected)->array;
}
void* BE_CALL InvokeFake(void*,const void* method,void* object,void** args,void** exception) {
    ++fake_runtime_calls;
    const auto key=std::string_view(static_cast<const MethodContract*>(method)->key);
    auto* n=static_cast<Fake*>(object);
    if (key.starts_with("game_object.")) {
        Check(n && n->runtime_class==&fake_game_object_class_token,
            "non-GameObject receiver reached a GameObject-specific runtime API");
        ++game_object_calls;
    }
    if (key=="component.get_transform") return n->transform?n->transform:object;
    if (key=="component.get_game_object") return n->game_object;
    if (key=="resources.find_all") return fake_scene_renderers;
    if (key=="object.get_type" || key=="type.get_element_type" || key=="game_object.get_transform") return object;
    if (key=="component.get_component") return n->filter;
    if (key=="time.frame_count") return Scalar(fake_frame);
    if (key=="transform.get_parent") return n->parent;
    if (key=="game_object.renderers") return fake_transform_type && args[0]==fake_transform_type?n->transforms:object;
    if (key=="mesh.get_vertex_count") return Scalar(3);
    if (key=="mesh.get_sub_mesh_count") return Scalar(1);
    if (key=="mesh.get_index_count") return Scalar(6);
    if (key=="mesh.get_vertex_attribute_count") return Scalar(n->array.size());
    if (key=="mesh.get_vertex_attribute") return n->array.at(*static_cast<int*>(args[0]));
    if (key=="probe.material_shader") return Make("shader");
    if (key=="material.get_shader") {if (!n->shader) n->shader=Make("shader");return n->shader;}
    if (key=="array.create") { auto* a=Make(); a->array.resize(*static_cast<int*>(args[1])); return a; }
    if (key=="array.get_length") return Scalar(n->array.size());
    if (key=="array.get_value") return n->array.at(*static_cast<int*>(args[0]));
    if (key=="array.set_value") { n->array.at(*static_cast<int*>(args[1]))=args[0]; return nullptr; }
    if (key=="array.clone") { auto* a=Make(); a->array=n->array; return a; }
    if (key=="object.get_name") return object;
    if (key=="object.instance_id") return Scalar(reinterpret_cast<uintptr_t>(object)&0x7fffffff);
    if (key=="object.is_alive") return Scalar(args[0] && static_cast<Fake*>(args[0])->alive);
    if (key=="object.get_hide_flags") return Scalar(n->props["hideFlags"]);
    if (key=="object.set_hide_flags") { n->props["hideFlags"]=*static_cast<int32_t*>(args[0]); return nullptr; }
    if (key=="transform.local_to_world") return object;
    if (key=="transform.world_to_local") {
        auto* inverse=Make();inverse->matrix=n->matrix;
        inverse->matrix.m[12]=-n->matrix.m[12];inverse->matrix.m[13]=-n->matrix.m[13];inverse->matrix.m[14]=-n->matrix.m[14];return inverse;
    }
    if (key=="mesh.get_bindposes") return n->poses;
    if (key.starts_with("skinned.") && n && n->runtime_class==g_static_renderer_class.class_info && n->runtime_class)
        Check(false,"static renderer reached a SkinnedMeshRenderer API");
    if (key=="filter.get_shared_mesh") return n->mesh;
    if (key=="filter.set_shared_mesh") { n->mesh=args[0]; return nullptr; }
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
    if (key=="renderer.is_visible") return Scalar(n->visible);
    if (key=="renderer.set_enabled") { n->enabled=*static_cast<bool*>(args[0]); return nullptr; }
    if (key=="material.copy") { auto* donor=static_cast<Fake*>(args[0]);if (!donor->shader) donor->shader=Make("shader");n->shader=donor->shader;n->name=donor->name; n->textures=donor->textures; return nullptr; }
    if (key=="material.get_texture_property_ids") { auto* a=Array({}); for (auto [id,tex]:n->textures) a->array.push_back(Scalar(id)); return a; }
    if (key=="material.get_texture_by_id") return n->textures.at(*static_cast<int*>(args[0]));
    if (key=="material.set_texture_by_id") { n->textures[*static_cast<int*>(args[0])]=args[1]; return nullptr; }
    if (key=="texture2d.ctor") { ++texture_ctors;n->width=*static_cast<int*>(args[0]); n->height=*static_cast<int*>(args[1]); n->format=*static_cast<int*>(args[2]); return nullptr; }
    if (key=="texture2d.load_raw_texture_data") { auto* raw=*static_cast<uint8_t**>(args[0]); n->data.assign(raw,raw+*static_cast<int*>(args[1])); return nullptr; }
    if (key=="texture2d.apply") {++texture_applies;return nullptr;}
    if (key=="texture.get_width") return Scalar(n->width);
    if (key=="texture.get_height") return Scalar(n->height);
    if (key=="texture.get_graphics_format") return Scalar(n->format);
    if (key=="graphics_format_utility.get_graphics_format") return Scalar(*static_cast<int*>(args[0]));
    if (key.starts_with("texture.get_")) { const auto found=n->props.find(std::string(key.substr(12))); return Scalar(found==n->props.end()?0:found->second); }
    if (key.starts_with("texture.set_")) { uint32_t bits=0; std::memcpy(&bits,args[0],4); n->props[std::string(key.substr(12))]=bits; return nullptr; }
    if (key=="camera.get_main") return fake_camera;
    if (key=="object.destroy") { ++static_cast<Fake*>(args[0])->destroy_calls; return nullptr; }
    // Windows synchronous delivery first establishes the configured LOD state.
    if (key=="pipeline.current") { static Fake* pipeline=Make("pipeline"); return pipeline; }
    if (key=="pipeline.enable_force_lod0" || key=="pipeline.disable_force_lod0") return nullptr;
    if (key=="quality.get_max_lod") return Scalar(fake_max_lod);
    if (key=="quality.set_max_lod") { fake_max_lod=*static_cast<int32_t*>(args[0]); return nullptr; }
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
    ConfigureFakeObjectClasses();
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
        TextureTransactionCache alias_cache;
        Check(ApplyTextureMask(alias_material,1,bem,alias_cache),"same source texture in multiple slots rejected");
        Check(alias_material->textures[7]==alias_material->textures[42] &&
            alias_material->textures[7]!=original_texture && alias_cache.built.size()==1,
            "shared texture properties did not receive one replacement");
        Check(alias_material->textures[99]==unrelated && original_texture->data.empty(),
            "shared-slot replacement changed an unrelated slot or original texture");
        auto* ambiguous_material=Make("ambiguous_material"); auto* other_original=Make("original");
        ambiguous_material->textures={{7,original_texture},{42,other_original}};
        TextureTransactionCache refused_cache;
        Check(!ApplyTextureMask(ambiguous_material,1,bem,refused_cache) && refused_cache.built.empty() &&
            ambiguous_material->textures[7]==original_texture && ambiguous_material->textures[42]==other_original,
            "distinct same-name source textures accepted or changed");
        auto* missing_material=Make("missing_material"); missing_material->textures={{99,unrelated}};
        Check(!ApplyTextureMask(missing_material,1,bem,refused_cache) && refused_cache.built.empty() &&
            missing_material->textures[99]==unrelated,"missing texture name was guessed or changed");
        auto* conflict_material=Make("conflict_material"); conflict_material->textures={{7,original_texture},{42,original_texture}};
        Check(!ApplyTextureMask(conflict_material,3,bem,refused_cache),"two payloads for the same slots accepted");
        auto unchecked_bem=bem; unchecked_bem.skip_validation=true;
        auto* unchecked_material=Make("unchecked_material");
        unchecked_material->textures={{7,original_texture},{42,other_original},{99,unrelated}};
        TextureTransactionCache unchecked_cache;
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
            Fake* first_texture=nullptr; Fake* second_texture=nullptr; Fake* distinct_texture=nullptr;
            {
                ConstructionScope transaction;
                auto experimental=bem; experimental.loading_optimization=optimize;
                auto single=bem.components[0]; single.draws.resize(1); single.material_names.resize(1);
                auto first_target=bindings[0],second_target=bindings[1];
                TextureTransactionCache cache;
                Check(PrepareDrawMaterials(single,first_target,bindings,experimental,&cache) &&
                    PrepareDrawMaterials(single,second_target,bindings,experimental,&cache),"transaction materials failed");
                first_texture=static_cast<Fake*>(static_cast<Fake*>(ArrayValue(first_target.custom_materials,0))->textures[7]);
                second_texture=static_cast<Fake*>(static_cast<Fake*>(ArrayValue(second_target.custom_materials,0))->textures[7]);
                // 2026-10-03: one upload per selected entry + donor sampler state
                // per transaction, independent of loading_optimization.
                Check(first_texture==second_texture && cache.built.size()==1 && transaction.texture_dedup_hits==1 &&
                    transaction.texture_dedup_bytes==experimental.textures[0].info.data_size && transaction.texture_references==2,
                    "same entry/sampler was uploaded twice in one transaction");
                // A source Texture with a different sampler state keeps its own copy.
                other_original->props["wrap_mode"]=1;
                auto* different_material=Make("material0"); different_material->textures={{7,other_original}};
                auto distinct=bindings[0]; distinct.original_materials=Array({different_material}); distinct.donor_materials=nullptr;
                auto keep=single; keep.keep_material_names={"material0"}; keep.keep_material_overrides={{0,0,1}};
                Check(PrepareKeepMaterials(keep,distinct,experimental,&cache),"different source texture preparation failed");
                distinct_texture=static_cast<Fake*>(static_cast<Fake*>(ArrayValue(distinct.custom_materials,0))->textures[7]);
                Check(distinct_texture!=first_texture && distinct_texture->props["wrap_mode"]==1,"different sampler source was incorrectly merged");
                Check(cache.built.size()==2,"sampler state missing from transaction key");
                // Another source Texture object with the SAME sampler state shares the upload.
                auto* same_sampler=Make("original"); auto* same_material=Make("material0"); same_material->textures={{7,same_sampler}};
                auto shared=bindings[0]; shared.original_materials=Array({same_material}); shared.donor_materials=nullptr;
                Check(PrepareKeepMaterials(keep,shared,experimental,&cache) &&
                    static_cast<Fake*>(ArrayValue(shared.custom_materials,0))->textures[7]==first_texture,
                    "equal sampler state from another donor Texture was uploaded again");
                other_original->props.erase("wrap_mode");
                const uint64_t expected_textures=2;
                Check(transaction.texture_constructed==expected_textures &&
                    transaction.texture_submitted==expected_textures &&
                    transaction.texture_payload_bytes==expected_textures*experimental.textures[0].data.size() &&
                    transaction.largest_texture_payload_bytes==experimental.textures[0].data.size(),
                    "upload counters do not represent actual unique texture submissions");
                auto invalid=single; invalid.material_names[0]="wrong";
                Check(!PrepareDrawMaterials(invalid,second_target,bindings,experimental,&cache),"failure fixture unexpectedly prepared");
                // Leave transaction unpublished to exercise shared asset cleanup.
            }
            Check(first_texture->destroy_calls==1 && distinct_texture->destroy_calls==1,
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
    experimental.insert(experimental.find('\n')+1,"hot_switch=true\nfast_loading=true\nloading_optimization=false\n");
    Check(ParseModRegistry(experimental,path.parent_path(),registry,error) && registry.hot_switch && registry.fast_loading &&
        registry.loading_optimization && registry.enabled[0].loading_optimization,"options not propagated or legacy key not ignored");
    auto owner=registry.owned_adapters[0]; auto key=registry.enabled[0].selection_key;
    Check(ParseModRegistry(config,path.parent_path(),registry,error) && !registry.hot_switch && !registry.fast_loading &&
        registry.loading_optimization && registry.enabled[0].loading_optimization,
        "hot switch/fast loading must be off and payload optimization on by default");
    Check(std::string(owner->adapter.id)=="chr_test","old adapter lifetime lost across registry replacement");
    Check(registry.enabled[0].selection_key==key,"upload pacing must not change the parse cache identity");
    const auto default_key=registry.enabled[0].selection_key;
    std::string default_selection=config; default_selection.replace(default_selection.find("appearance=hidden"),17,"appearance=default");
    Check(ParseModRegistry(default_selection,path.parent_path(),registry,error) && registry.enabled[0].selection_key!=default_key,
        "appearance/options absent from selection cache identity");
    g_registry=std::move(registry); g_registry.hot_switch=true; g_registry_root=path.parent_path();
    const auto working_key=g_registry.enabled[0].selection_key;
    Check(!InstallRegistryUpdate("[CustomModel]\nhot_switch=true\n[Mod.bad]\nenabled=true\npackage=missing.bem\n") &&
        g_registry.enabled.size()==1 && g_registry.enabled[0].selection_key==working_key,
        "metadata-refused candidate was treated as disabling the working package");
    Check(!InstallRegistryUpdate("[CustomModel]\nhot_switch=true\nfast_loading=true\n") &&
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
std::set<uint32_t> weak_handles;
uint32_t next_handle=1;
uint32_t BE_CALL TrackedRoot(void*,void* object,int) { const auto handle=next_handle++; tracked_handles[handle]=object; ++roots; return handle; }
void BE_CALL TrackedFree(void*,uint32_t handle) { Check(tracked_handles.erase(handle)==1,"invalid hot-switch handle release"); weak_handles.erase(handle); --roots; }
uint32_t TrackedWeak(void* object,bool) { const auto handle=TrackedRoot(nullptr,object,0); weak_handles.insert(handle); return handle; }
// Strong (non-weak) GC handles currently targeting `object`.
size_t StrongHandlesTo(void* object) {
    return size_t(std::count_if(tracked_handles.begin(),tracked_handles.end(),[&](const auto& entry){
        return entry.second==object && !weak_handles.contains(entry.first);}));
}
void CpuGeometryTests() {
    BE_HostApiV1 host{};host.runtime_invoke=InvokeFake;host.object_unbox=UnboxFake;
    host.gchandle_new=TrackedRoot;host.gchandle_free=TrackedFree;g_host=&host;
    for(auto& method:g_methods) {method.method_info=&method;method.resolved=true;}
    g_weak_new=[](void* object,bool)->uint32_t{return TrackedRoot(nullptr,object,0);};
    g_weak_target=[](uint32_t handle)->void*{const auto found=tracked_handles.find(handle);return found==tracked_handles.end()?nullptr:found->second;};
    g_enabled=true;g_stopping=false;g_pump_thread=GetCurrentThreadId();
    BemComponent component;component.info.vertex_count=3;component.info.index_count=3;
    component.info.index_element_size=2;component.info.stride0=12;component.info.stride1=8;component.info.stride2=12;
    component.attributes={{0,0,3,0},{4,0,2,1},{12,4,4,2},{13,6,4,2}};
    component.streams[0].resize(36);component.streams[1].resize(24);component.streams[2].resize(36);component.indices.resize(6);
    const float xyz[]{1,2,3,4,5,6,7,8,9};std::memcpy(component.streams[0].data(),xyz,sizeof(xyz));
    const uint16_t indices[]{0,1,2};std::memcpy(component.indices.data(),indices,sizeof(indices));component.draws={{0,3,0,0,0,0,0}};
    struct Copy {bool called=false;std::vector<float> positions;std::vector<uint32_t> indices;uint32_t stride=0;};
    const auto visitor=[](void* context,const BE_CustomModelGeometryV1* view)->BE_Result {
        auto& copy=*static_cast<Copy*>(context);
        Check(view->version==1 && view->struct_size==sizeof(*view) && view->draw_count==1 && view->draws[0].index_count==3,"bad geometry bridge descriptor");
        copy.called=true;copy.positions.assign(view->positions_xyz,view->positions_xyz+size_t(view->vertex_count)*3);
        copy.indices.assign(view->indices,view->indices+view->index_count);copy.stride=view->skin_stride;return BE_Result_Ok;
    };
    auto* mesh=Make("custom-mesh");Copy copy;
    {ConstructionScope scope;RememberCpuGeometry(mesh,component);}
    Check(BetterEndfield_QueryCustomModelGeometryV1(mesh,visitor,&copy)==BE_Result_Ok && copy.called && copy.stride==12 && copy.positions[0]==1 && copy.indices==std::vector<uint32_t>({0,1,2}),"geometry bridge did not copy current positions/skin/index");
    const float changed=11;std::memcpy(component.streams[0].data(),&changed,4);
    {ConstructionScope scope;RememberCpuGeometry(mesh,component);}
    copy={};Check(BetterEndfield_QueryCustomModelGeometryV1(mesh,visitor,&copy)==BE_Result_Ok && copy.positions[0]==11 && g_cpu_geometry.size()==1,"geometry bridge retained prior positions for the same mesh");
    Check(BetterEndfield_QueryCustomModelGeometryV1(Make("unregistered"),visitor,&copy)==BE_Result_NotFound,"geometry bridge confused unregistered mesh identity");
    Check(BetterEndfield_QueryCustomModelGeometryV1(mesh,nullptr,&copy)==BE_Result_InvalidArgument,"geometry bridge accepted missing visitor");
    g_pump_thread=0;Check(BetterEndfield_QueryCustomModelGeometryV1(mesh,visitor,&copy)==BE_Result_NotReady,"geometry bridge allowed wrong thread");g_pump_thread=GetCurrentThreadId();
    // A live managed address whose weak target now has another Unity instance ID
    // must not inherit an old generated mesh's CPU data.
    tracked_handles[g_cpu_geometry[0]->mesh.handle]=Make("reused-address");
    Check(BetterEndfield_QueryCustomModelGeometryV1(mesh,visitor,&copy)==BE_Result_NotFound && g_cpu_geometry.empty() && !g_cpu_geometry_bytes,"geometry bridge accepted stale Unity identity");
    auto oversized=component;oversized.info.vertex_count=UINT32_MAX;
    {ConstructionScope scope;RememberCpuGeometry(mesh,oversized);}
    Check(g_cpu_geometry.empty(),"geometry bridge exceeded memory budget");
    g_cpu_geometry.clear();g_cpu_geometry_bytes=0;g_cpu_geometry_serial=0;g_enabled=false;g_pump_thread=0;
    Check(roots==0 && tracked_handles.empty(),"geometry bridge leaked strong/weak references");
    g_weak_new=nullptr;g_weak_target=nullptr;g_host=nullptr;
    std::cout<<"PASS bounded CPU geometry bridge: current morph positions, stable draw indices, weak identity, fallback, thread and ownership\n";
}
void StaticResourceTests() {
    BE_HostApiV1 host{};host.runtime_invoke=InvokeFake;host.object_unbox=UnboxFake;host.copy_managed_string=StringFake;
    host.object_new=NewFake;host.gchandle_new=TrackedRoot;host.gchandle_free=TrackedFree;g_host=&host;
    g_weak_new=TrackedWeak;
    g_weak_target=[](uint32_t handle)->void* {const auto it=tracked_handles.find(handle);return it==tracked_handles.end()?nullptr:it->second;};
    ConfigureFakeObjectClasses();
    g_array_new_specific=[](void*,uintptr_t count)->void* {auto* n=Make();n->array.resize(count);return n;};
    for (auto& method:g_methods) {method.method_info=&method;method.resolved=true;}
    g_renderer_class.type_object=Make("Renderer");g_static_renderer_class.class_info=Make("MeshRenderer");
    g_mesh_filter_class.type_object=Make("MeshFilter");g_material_class.class_info=Make("Material");
    auto owner=std::make_shared<OwnedCharacterAdapter>();owner->id="wpn_test";owner->world=owner->ui="wpn_test_postmodel";
    owner->names={"blade_mesh"};owner->components={{owner->names[0].c_str(),6,"Meshes/blade",true}};
    owner->adapter={owner->id.c_str(),owner->world.c_str(),owner->ui.c_str(),"",false,owner->components,"weapon","assets/test/wpn_test_postmodel.prefab",0,true};
    g_registry={};g_registry.owned_adapters.push_back(owner);g_hot_switch_runtime=true;
    auto* asset=GameObject(owner->world);auto* group=Make("Meshes");group->parent=asset;
    auto* renderer=Make("blade");renderer->parent=group;renderer->runtime_class=const_cast<void*>(g_static_renderer_class.class_info);
    auto* filter=Make("filter");renderer->filter=filter;filter->mesh=Make("blade_mesh");
    renderer->materials=Array({Make("blade_material")});asset->array={renderer};
    BemPocData bem;BemComponent component;component.static_mesh=true;component.info.component_id=0;
    component.info.original_index_count=6;bem.components.push_back(component);
    {
        ConstructionScope scope;
        Check(MakeSmallModelPlan(bem)->data.components[0].static_mesh,"job plan lost static renderer kind");
        auto lower_lod=owner->adapter;lower_lod.receiver_lod=1;
        Check(!ValidatePayloadAdapter(lower_lod,bem),"unsupported Windows explicit nonzero LOD silently accepted");
        auto contradictory=owner->adapter;const std::array<ComponentIdentity,1> wrong_lod{{{"blade_mesh",6,"Mesh_all/lod1/blade",true}}};
        contradictory.components=wrong_lod;bem.skip_validation=true;
        Check(!ValidatePayloadAdapter(contradictory,bem),"developer validation flag bypassed explicit receiver LOD contradiction");bem.skip_validation=false;
        std::vector<PreparedBinding> bindings;
        Check(CaptureGenericResourceBindings(owner->adapter,bem,asset,bindings) && bindings.size()==1,"explicit static receiver capture failed");
        Check(bindings[0].original_mesh==filter->mesh && !bindings[0].original_bones,"static capture did not use the same-object MeshFilter");
        void* poses=reinterpret_cast<void*>(1);
        Check(PreparePalette(component,bindings[0],bindings,poses) && !poses && !bindings[0].custom_bones,"static palette required bones");
        auto malformed=component;malformed.bones.push_back({0,0,0});
        Check(!PreparePalette(malformed,bindings[0],bindings,poses),"static palette accepted bones");
        auto* original=filter->mesh;auto* replacement=Make("replacement_blade");
        bindings[0].custom_mesh=replacement;bindings[0].custom_materials=renderer->materials;
        reject_material_renderer=renderer;rejected=false;
        Check(CommitResource<PreparedBinding>(bindings,ApplyPreparedBinding,RestorePreparedBinding)==CommitResult::Restored && filter->mesh==original,
            "static MeshFilter was not rolled back after material failure");
        reject_material_renderer=nullptr;rejected=false;
        CompletedResource record;
        Check(RememberResource(owner->adapter,asset,bindings,record,"static",true),"static Original record required a bone array");
        Check(CommitResource<PreparedBinding>(bindings,ApplyPreparedBinding,RestorePreparedBinding)==CommitResult::Committed && filter->mesh==replacement,
            "static MeshFilter commit failed");
        g_completed.push_back(std::move(record));
        Check(IsCompletedResource(owner->adapter,asset,"static"),"static completion identity did not match");
        auto* clone=GameObject(owner->world+"(Clone)#7");auto* clone_group=Make("Meshes");clone_group->parent=clone;
        auto* clone_renderer=Make("blade");clone_renderer->parent=clone_group;clone_renderer->runtime_class=const_cast<void*>(g_static_renderer_class.class_info);
        auto* clone_filter=Make("filter");clone_filter->mesh=replacement;clone_renderer->filter=clone_filter;
        clone_renderer->materials=renderer->materials;clone->array={clone_renderer};
        Check(IsCompletedResource(owner->adapter,clone,"static"),"pooled static clone was not recognized");
        std::vector<PreparedBinding> restore;
        Check(PrepareDisabledResource(clone,&owner->adapter,restore),"static clone Original recovery failed");
        Check(CommitResource<PreparedBinding>(restore,ApplyPreparedBinding,RestorePreparedBinding)==CommitResult::Committed && clone_filter->mesh==original,
            "static clone disable failed to restore Original MeshFilter");
        auto root_adapter=owner->adapter;const std::array<ComponentIdentity,1> root_components{{{"blade_mesh",6,"",true}}};
        root_adapter.components=root_components;renderer->transform=asset;GenericMatching::ReceiverKey root_key;
        Check(MakeReceiverKey(root_adapter,asset,renderer,root_key) && root_key.path.empty(),"explicit root renderer was rejected");
        renderer->transform=nullptr;renderer->filter=nullptr;bindings.clear();
        Check(!CaptureGenericResourceBindings(owner->adapter,bem,asset,bindings),"static receiver without MeshFilter accepted");
        renderer->filter=filter;
        auto nested=owner->adapter;const std::array<ComponentIdentity,1> other_path{{{"blade_mesh",6,"Weapon/Meshes/blade",true}}};nested.components=other_path;
        Check(!CaptureGenericResourceBindings(nested,bem,asset,bindings),"explicit static receiver fell back to matching mesh name outside declared path");
    }
    g_completed.clear();g_original_mesh_pins.clear();g_registry={};g_hot_switch_runtime=false;
    Check(roots==0 && tracked_handles.empty(),"static resource test leaked handles");
    g_renderer_class={};g_static_renderer_class={};g_mesh_filter_class={};g_weak_new=nullptr;g_weak_target=nullptr;g_host=nullptr;
    std::cout<<"PASS static MeshFilter capture, exact path, rollback, pooled clone, disable and no-skin contract\n";
}
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
    ConfigureFakeObjectClasses();
    g_array_new_specific=[](void*,uintptr_t count)->void* { auto* a=Make(); a->array.resize(count); return a; };
    for (auto& method:g_methods) { method.method_info=&method; method.resolved=true; }
    auto owner=std::make_shared<OwnedCharacterAdapter>(); owner->id="chr_test"; owner->world="world"; owner->ui="ui";
    owner->names={"mesh0","mesh1"}; for (const auto& name:owner->names) owner->components.push_back({name.c_str(),6});
    owner->adapter={owner->id.c_str(),owner->world.c_str(),owner->ui.c_str(),"",false,owner->components};
    g_registry={}; g_registry.hot_switch=true; g_registry.owned_adapters.push_back(owner); g_hot_switch_runtime=true;
    auto* asset=GameObject("world"); auto* original_texture=Make("original");
    auto* mesh_all=Make("Mesh_all");mesh_all->parent=asset;auto* lod0=Make("lod0");lod0->parent=mesh_all;
    asset->transforms=Array({mesh_all,lod0});
    std::array<Fake*,2> renderers{},original_materials{};
    for (int i=0;i<2;++i) {
        auto* renderer=Make("mesh"+std::to_string(i)); renderer->parent=lod0;
        renderer->mesh=Make(renderer->name);auto* original_bone=Make("bone"+std::to_string(i));original_bone->parent=asset;
        renderer->bones=Array({original_bone});static_cast<Fake*>(asset->transforms)->array.push_back(original_bone);
        static_cast<Fake*>(renderer->mesh)->poses=Array({Make()});
        auto* material=Make("material"+std::to_string(i)); material->textures[7]=original_texture;
        renderer->materials=original_materials[i]=Array({material}); renderers[i]=renderer; asset->array.push_back(renderer);
    }
    {
        ConstructionScope scope;
        auto mapped=owner->adapter;
        mapped.id="chr_0025_ardelia";
        const std::array<ComponentIdentity,2> names{{
            {"S_actor_ardelia_fur_01_lod0_20",6},{"mesh1",6}}};
        mapped.components=names;
        auto* source=static_cast<Fake*>(renderers[0]->mesh);
        const auto original_renderer_name=renderers[0]->name,original_mesh_name=source->name;
        renderers[0]->name="S_actor_ardelia_fur_01_lod0";
        source->name="S_actor_ardelia_fur_01_lod0_20";
        std::vector<PreparedBinding> bindings;
        Check(PrepareResource(mapped,payload,asset,bindings),"exact Ardelia Mesh/renderer mapping rejected by production preparation");
        Check(DonorMesh(bindings[0])==source && source->name==names[0].name,
            "renderer mapping changed the validated source Mesh identity");
        source->name=renderers[0]->name;bindings.clear();
        Check(!PrepareResource(mapped,payload,asset,bindings),"renderer mapping skipped exact source Mesh validation");
        source->name=original_mesh_name;renderers[0]->name=original_renderer_name;
    }
    auto publish=[&](const char* key,uint8_t value,bool reject) {
        ConstructionScope scope; payload.textures[0].data.assign(8,value);
        std::vector<PreparedBinding> bindings;
        Check(PrepareResource(owner->adapter,payload,asset,bindings),"hot switch could not rebuild from pristine donor");
        if (!g_completed.empty()) {
            // Original materials are held weakly and re-collected into a fresh
            // array; the donor must hold the same Original material objects.
            auto* donor=static_cast<Fake*>(DonorMaterials(bindings[0]));
            Check(donor && donor->array==static_cast<Fake*>(original_materials[0])->array,"hot switch used previous package as material donor");
        }
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
        ConstructionScope scope; auto* clone=GameObject("world(Clone)");
        auto* clone_mesh_all=Make("Mesh_all");clone_mesh_all->parent=clone;auto* clone_lod=Make("lod0");clone_lod->parent=clone_mesh_all;
        auto* clone0=Make("mesh0"); clone0->parent=clone_lod; clone0->mesh=renderers[0]->mesh;
        clone0->materials=a_materials; auto* bone=Make("bone0"); bone->parent=clone;
        clone0->bones=Array({bone}); clone->array={clone0}; clone->transforms=Array({bone});
        PreparedBinding binding; binding.component_id=1; binding.renderer=clone0;
        binding.original_mesh=clone0->mesh; binding.original_bones=clone0->bones; binding.original_materials=clone0->materials;
        Check(UseSavedOriginal(clone,binding) && binding.saved_original && SameMaterialSet(DonorMaterials(binding),original_materials[0]) &&
            ArrayValue(DonorBones(binding),0)==bone,
            "old natural clone lost original renderer donor after template A to B");
        Check(binding.original_materials==a_materials,"clone rollback stopped pointing at package A");
        auto* clone1=Make("mesh1"); clone1->parent=clone_lod; clone1->mesh=renderers[1]->mesh;
        auto* clone_bone1=Make("bone1");clone_bone1->parent=clone;static_cast<Fake*>(clone->transforms)->array.push_back(clone_bone1);
        clone1->materials=a_materials1; clone1->bones=Array({clone_bone1}); clone->array.push_back(clone1);
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
            DonorMesh(reordered)==mesh_before && SameMaterialSet(DonorMaterials(reordered),original_materials[0]),
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
    Check(SameMaterialSet(renderers[0]->materials,original_materials[0]) && SameMaterialSet(renderers[1]->materials,original_materials[1]),
        "disable did not restore pristine materials");
    {
        ConstructionScope observation;
        for (auto& record:g_completed) if (record.selection_key.empty()) {
            for (const auto& binding:record.bindings) Check(!binding.generated_mesh && !binding.generated_materials,"disabled binding still counted as generated");
            record.root.Reset();
        }
        PruneCompletedResources();
        Check(std::none_of(g_completed.begin(),g_completed.end(),[](const auto& record){return record.selection_key.empty();}),
            "disabled Original/shadow lineage held itself alive through original materials");
    }
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
        auto* asset=GameObject(std::string(root)+(std::string_view(root)=="ui"?"(Clone)":"")); auto* renderer=Make("mesh"); renderer->parent=asset; asset->array={renderer};
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
void AsyncJobsTests(const std::filesystem::path& path) {
    Check(ModelContentSha256({})=="e3b0c44298fc1c149afbf4c8996fb92427ae41e4649b934ca495991b7852b855","texture content certificate is not SHA256");
    const std::array<uint8_t,3> sha_abc{'a','b','c'};
    Check(ModelContentSha256(sha_abc)=="ba7816bf8f01cfea414140de5dae2223b00361a396177a9cb410ff61f20015ad","nonempty texture content certificate is not SHA256");
    // Production Jobs load geometry + metadata and stream texture entries.
    BemPocData payload;std::string error;Check(LoadBem(path,payload,error,{},nullptr,false,false,{},UINT64_MAX,true),error.c_str());
    Check(payload.payload_source && !payload.textures.empty() &&
        std::all_of(payload.textures.begin(),payload.textures.end(),[](const auto& t){return t.Deferred();}),
        "deferred load still decoded texture payloads");
    for (auto& c:payload.components) {
        c.info.flags=kComponentFlagNoGeometry;c.info.original_index_count=6;
        c.keep_material_overrides={{0,0,1}};c.keep_material_names={"material"+std::to_string(c.info.component_id)};
    }
    BE_HostApiV1 host{};host.runtime_invoke=InvokeFake;host.object_unbox=UnboxFake;host.copy_managed_string=StringFake;
    host.object_new=NewFake;host.gchandle_new=TrackedRoot;host.gchandle_free=TrackedFree;
    g_host=&host;g_material_class.class_info=&host;g_texture2d_class.class_info=&host;
    g_weak_new=&TrackedWeak;
    g_weak_target=[](uint32_t h)->void*{auto it=tracked_handles.find(h);return it==tracked_handles.end()?nullptr:it->second;};
    ConfigureFakeObjectClasses();
    g_array_new_specific=[](void*,uintptr_t n)->void*{auto* a=Make();a->array.resize(n);return a;};
    fake_transform_type=Make("TransformType");host.resolve_class=[](void*,const char*,const char*,const char*,BE_ResolvedClassV1* result)->BE_Result {
        result->class_info=fake_transform_type;result->type_object=fake_transform_type;return BE_Result_Ok;
    };
    for (auto& method:g_methods) {method.method_info=&method;method.resolved=true;}
    auto owner=std::make_shared<OwnedCharacterAdapter>();owner->id="chr_test";owner->world="world";owner->ui="ui";
    owner->names={"mesh0","mesh1"};for(const auto& name:owner->names) owner->components.push_back({name.c_str(),6});
    owner->adapter={owner->id.c_str(),owner->world.c_str(),owner->ui.c_str(),"",false,owner->components};
    g_registry={};g_registry.owned_adapters.push_back(owner);
    EnabledMod mod{&owner->adapter,path,"default",false,true,"package\ndefault\nmorph=0\nchecked\noptimized\nrevision","morph=0"};
    g_registry.enabled.push_back(mod);g_hot_switch_runtime=true;g_enabled=true;g_stopping=false;g_pump_thread=GetCurrentThreadId();
    g_model_frame=UINT64_MAX;g_model_frame_budget=FrameBudget{};
    // These checks exercise the asynchronous template route itself: declare
    // complete clone coverage and a live pump, and verify bindings every frame.
    g_model_clone_coverage=true;g_model_pump_ms=GetTickCount64();g_model_verify_interval_ms=0;
    AsyncBemLoader::Backend backend;
    backend.plan=[](const BemRequest&,BemLoadPlan& p,std::string&)->bool{p.reservation_bytes=1024;return true;};
    backend.load=[payload](const BemRequest& request,uint64_t,BemPocData& out,BemLoadStats&,std::string&)->bool{
        Check(request.defer_texture_payloads,"production Job requested whole texture payloads");
        out=payload;return true;
    };
    g_model_loader=std::make_unique<AsyncBemLoader>(AsyncBemLoader::Config{},std::move(backend));
    g_texture_streamer=std::make_unique<TexturePayloadStreamer>();
    auto* original_texture=Make("original");std::array<Fake*,2> meshes{},materials{};
    for (int i=0;i<2;++i) {
        meshes[i]=Make("mesh"+std::to_string(i));meshes[i]->poses=Array({Make()});
        materials[i]=Make("material"+std::to_string(i));materials[i]->textures[7]=original_texture;
    }
    auto receiver=[&](const char* name) {
        auto* root=GameObject(name);auto* mesh_all=Make("Mesh_all");mesh_all->parent=root;
        auto* lod=Make("lod0");lod->parent=mesh_all;root->transforms=Array({mesh_all,lod});
        for (int i=0;i<2;++i) {
            auto* r=Make("receiver"+std::to_string(i));r->parent=lod;r->mesh=meshes[i];r->materials=Array({materials[i]});
            auto* bone=Make("bone"+std::to_string(i));bone->parent=root;r->bones=Array({bone});
            root->array.push_back(r);static_cast<Fake*>(root->transforms)->array.push_back(bone);
        }
        return root;
    };
    auto tick=[&]() {
        ++fake_frame;const auto applies=texture_applies,ctors=texture_ctors;
        {ConstructionScope observation;PumpModelJobs(fake_frame);PumpModelJobs(fake_frame);}
        Check(texture_applies-applies<=1 && texture_ctors-ctors<=1,"global repeated-frame pump submitted multiple textures");
        std::this_thread::yield();
    };
    {
        ConstructionScope observation;auto* ui_root=GameObject("template");auto* live_root=GameObject("scene-receiver");
        live_root->matrix.m[12]=123;auto* ui_renderer=Make();ui_renderer->parent=ui_root;
        auto* live_renderer=Make();live_renderer->parent=live_root;live_renderer->matrix.m[12]=123;
        g_verified_mesh_space_roots={ui_root,live_root};
        Check(SameMeshSpace(ui_renderer,live_renderer),"translated live clone compared scene position to UI prefab");
        live_renderer->matrix.m[12]=124;
        Check(!SameMeshSpace(ui_renderer,live_renderer),"local mesh-space mismatch accepted by verified roots");
        g_verified_mesh_space_roots={};
    }
    auto finish=[&](Fake* root) {
        g_original_finish=[](void*,void*,void*){++finish_calls;};const auto before=finish_calls,ctors=texture_ctors;
        ResourceFinish(nullptr,root,nullptr);
        Check(finish_calls==before+1 && texture_ctors==ctors,"cold Finish blocked/built or missed original delivery");
    };
    auto ready=[&](Fake* root) {
        for (unsigned i=0;i<20000;++i) {
            tick();ConstructionScope observation;
            for(const auto& record:g_completed) if (record.root.Get()==root && record.selection_key==g_registry.enabled[0].selection_key &&
                IsCompletedResource(owner->adapter,root,record.selection_key)) return;
        }
        Check(false,"registered target never reached Ready commit");
    };
    auto* first=receiver("ui");finish(first);ready(first);
    const auto built=texture_applies,constructed=texture_ctors,loads=g_model_loader->Snapshot().started;
    Check(built>=1 && constructed>=1,"production Job did not build textures");
    auto* replacement=static_cast<Fake*>(ArrayValue(static_cast<Fake*>(first->array[0])->materials,0))->textures[7];
    auto* second=receiver("ui");finish(second);ready(second);
    Check(texture_applies==built && texture_ctors==constructed && g_model_loader->Snapshot().started==loads,
        "new pristine UI did not reuse Ready pool before CPU decode/GPU build");
    auto* a=static_cast<Fake*>(first->array[0]);auto* b=static_cast<Fake*>(second->array[0]);
    Check(a->materials!=b->materials && ArrayValue(a->materials,0)!=ArrayValue(b->materials,0) &&
        static_cast<Fake*>(ArrayValue(b->materials,0))->textures[7]==replacement && a->bones!=b->bones,
        "cache hit reused private materials or another instance skeleton");
    g_registry.enabled[0].selection_key="package\ndefault\nmorph=1\nchecked\noptimized\nrevision";
    g_registry.enabled[0].parameters="morph=1";ready(first);ready(second);
    Check(texture_applies==built && texture_ctors==constructed,"Morph-only selection reuploaded textures");
    // Actual binding verification repairs game-side rewrites of the same root.
    b->materials=Array({materials[0]});ready(second);
    Check(static_cast<Fake*>(ArrayValue(b->materials,0))->textures[7]==replacement && texture_applies==built,
        "same-root completed shortcut ignored a game material rewrite");
    // No idle retention (2026-10-03): outside a transaction the module holds no
    // strong handle to a generated texture. The reuse above came from textures
    // still bound to published receivers; decoded bytes were streamed per entry
    // and released right after LoadRawTextureData.
    {ConstructionScope observation;PruneModelAssets(GetTickCount64());}
    Check(StrongHandlesTo(replacement)==0,"generated texture kept alive by an idle strong handle");
    {
        const auto streamed=g_texture_streamer->Snapshot();
        Check(streamed.decoded_count>=1 && streamed.live==0 && streamed.reserved==0 && streamed.peak<=payload.textures[0].info.data_size*2,
            "texture entries were not streamed one at a time and released after upload");
    }
    Check(static_cast<Fake*>(replacement)->destroy_calls==0 && original_texture->destroy_calls==0,
        "pruning destroyed borrowed/live/original assets");
    {
        // Borrowed Mesh ownership uses the same production pool as textures.
        auto* shared_mesh=Make("generated-mesh");
        {ConstructionScope observation;PublishModelAsset("mesh-owner-test",shared_mesh,{meshes[0]},64);}
        {ConstructionScope failed_transaction;Check(BorrowModelAsset("mesh-owner-test",{meshes[0]})==shared_mesh,"finished Mesh pool missed pristine donor");}
        Check(shared_mesh->destroy_calls==0,"failed borrower destroyed a shared Mesh");
        {ConstructionScope observation;
            Check(!BorrowModelAsset("mesh-owner-test",{meshes[1]}),"finished Mesh accepted another pristine donor");
            auto& entry=g_model_assets.back();tracked_handles[entry.object.handle]=Make("recycled-native-id");
            Check(!BorrowModelAsset("mesh-owner-test",{meshes[0]}),"finished Mesh accepted a stale native identity");
        }
    }
    {
        // A published asset is observed weakly only: once nothing references it
        // (managed wrapper collected) it is neither borrowed nor retained.
        auto* unused=Make("unused-generated");
        {ConstructionScope observation;PublishModelAsset("unused",unused,{meshes[0]},128*kLoadingMiB);
            Check(BorrowModelAsset("unused",{meshes[0]})==unused,"live published asset not borrowed");}
        Check(StrongHandlesTo(unused)==0,"published asset retained by an idle lease");
        for (auto& [handle,object]:tracked_handles) if (object==unused) object=nullptr;
        {ConstructionScope observation;Check(!BorrowModelAsset("unused",{meshes[0]}),"released asset was borrowed");
            PruneModelAssets(GetTickCount64());}
        Check(std::none_of(g_model_assets.begin(),g_model_assets.end(),[](const auto& entry){return entry.key=="unused";}) &&
            unused->destroy_calls==0,"released weak entry not pruned, or pruning destroyed it");
    }
    // Four independently observed team resources use the SAME global ledger.
    std::array<Fake*,4> team{receiver("ui"),nullptr,nullptr,nullptr};finish(team[0]);
    for (int i=1;i<4;++i) {
        auto role=std::make_shared<OwnedCharacterAdapter>();role->id="role"+std::to_string(i);
        role->world="world"+std::to_string(i);role->ui="ui"+std::to_string(i);role->names=owner->names;
        for(const auto& name:role->names) role->components.push_back({name.c_str(),6});
        role->adapter={role->id.c_str(),role->world.c_str(),role->ui.c_str(),"",false,role->components};
        auto selection=mod;selection.adapter=&role->adapter;selection.selection_key="role"+std::to_string(i)+"\ndefault\n\nchecked\noptimized\nrevision";
        g_registry.owned_adapters.push_back(role);g_registry.enabled.push_back(selection);
        team[i]=receiver(role->ui.c_str());static_cast<Fake*>(team[i]->array[0])->visible=(i==1);finish(team[i]);
    }
    tick();Check(g_model_jobs.size()==4,"four role requests were not admitted together");
    Check(std::any_of(g_model_jobs.begin(),g_model_jobs.end(),[](const auto& job){return job->priority==LoadPriority::Foreground;}) &&
        std::any_of(g_model_jobs.begin(),g_model_jobs.end(),[](const auto& job){return job->priority==LoadPriority::Visible;}),
        "actual visible target did not get priority, or every request was labelled Foreground");
    {
        // With a main camera, only the visible receiver nearest to it stays
        // Foreground; another visible team member shares the budget as Visible.
        fake_camera=Make("main-camera");team[1]->matrix.m[12]=10;team[2]->matrix.m[12]=2;
        static_cast<Fake*>(team[2]->array[0])->visible=true;
        for (auto& target:g_model_targets) target.next_visibility_ms=0;
        tick();
        auto job_for=[&](Fake* root)->ModelBuildJob* {
            for (const auto& job:g_model_jobs) if (job->target.Get()==root) return job.get();
            return nullptr;
        };
        ConstructionScope observation;auto* near_job=job_for(team[2]);auto* far_job=job_for(team[1]);
        Check(near_job && far_job && near_job->priority==LoadPriority::Foreground && far_job->priority==LoadPriority::Visible,
            "nearest visible receiver to the main camera was not prioritised");
    }
    bool team_ready=false;
    for(unsigned i=0;i<20000 && !team_ready;++i) {
        tick();team_ready=true;ConstructionScope observation;
        for(size_t role=0;role<team.size();++role) {
            bool found=false;
            for(const auto& record:g_completed) if(record.root.Get()==team[role] && record.selection_key==g_registry.enabled[role].selection_key) found=true;
            team_ready=team_ready && found;
        }
    }
    Check(team_ready,"a role starved in the shared frame queue");
    // A donor with its own sampler state cannot borrow the live replacement.
    auto* cancelled=receiver("ui");auto* cancellation_donor=Make("material0");cancellation_donor->textures[7]=Make("original");
    static_cast<Fake*>(cancellation_donor->textures[7])->props["wrap_mode"]=1;
    static_cast<Fake*>(cancelled->array[0])->materials=Array({cancellation_donor});
    g_registry.enabled[0].selection_key="package\nvariant\n\nchecked\noptimized\nrevision";finish(cancelled);
    Fake* cancelled_texture=nullptr;
    for(unsigned i=0;i<20000 && !cancelled_texture;++i) {
        tick();for(const auto& job:g_model_jobs) if(job->target.Get()==cancelled)
            for(const auto& work:job->textures) if(work.object && work.phase>=2 &&
                std::find(job->scope.assets.begin(),job->scope.assets.end(),work.object)!=job->scope.assets.end()) cancelled_texture=static_cast<Fake*>(work.object);
    }
    Check(cancelled_texture,"cancellation did not reach an owned partial texture");
    g_registry.enabled[0].selection_key="package\nlatest\n\nchecked\noptimized\nrevision";tick();
    Check(cancelled_texture->destroy_calls==1 && static_cast<Fake*>(replacement)->destroy_calls==0 && original_texture->destroy_calls==0,
        "revision cancellation leaked/double-destroyed owned texture or destroyed a shared/original asset");
    {ConstructionScope observation;CancelModelJobs();}
    Check(g_texture_streamer->Snapshot().live==0,"cancelled Job kept decoded texture bytes");
    g_model_loader->Shutdown();g_model_loader.reset();g_texture_streamer.reset();g_model_assets.clear();g_model_plans.clear();g_model_targets.clear();g_completed.clear();
    g_generated_texture_identity.clear();g_generated_texture_order.clear();
    g_registry={};g_hot_switch_runtime=false;g_enabled=false;g_pump_thread=0;fake_transform_type=nullptr;fake_camera=nullptr;
    g_model_clone_coverage=false;g_model_pump_ms=0;g_model_verify_interval_ms=500;
    Check(roots==0 && tracked_handles.empty(),"async jobs/cache leaked GC handles");
    g_weak_new=nullptr;g_weak_target=nullptr;g_host=nullptr;
    std::cout<<"PASS production async Finish, per-entry streamed textures released after upload, distinct-frame textures, live published texture reuse without idle holding, Morph texture reuse, private instances, actual binding repair, four-role fairness/camera priority and revision cancellation\n";
}
// Device regression 2026-10-03: every geometry component failed silently in the
// async Mesh phase (palette skin validated from the metadata-only plan), so no
// package was ever replaced. Also covers the synchronous fallbacks and the
// clone-hook ownership that broke betterendfield.model.
std::vector<std::string> captured_logs;
uint64_t fake_mesh_builds=0;
bool fail_next_mesh_build=false;
std::vector<void*> fake_build_donors;
std::map<const void*,std::array<uint8_t,8>> fake_static_fields;
bool LogContains(std::string_view text) {
    return std::any_of(captured_logs.begin(),captured_logs.end(),[&](const auto& line){return line.find(text)!=line.npos;});
}
void AsyncGeometryAndFallbackTests(const std::filesystem::path& path) {
    BemPocData payload;std::string error;Check(LoadBem(path,payload,error),error.c_str());
    Check(!payload.components.empty() && !(payload.components[0].info.flags&kComponentFlagNoGeometry) &&
        !payload.components[0].bones.empty(),"fixture lost its skinned geometry component");
    for (auto& c:payload.components) c.info.original_index_count=6;
    payload.loading_optimization=true;
    {
        // Root cause in isolation: the Job plan drops vertex streams.
        auto plan=MakeSmallModelPlan(payload);
        Check(plan->data.components[0].streams[2].empty(),"Job plan unexpectedly retained skin payload");
        std::vector<uint8_t> counts;std::vector<BoneWeight1Raw> weights;
        Check(!DecodeComponentSkin(plan->data.components[0],counts,weights) &&
            DecodeComponentSkin(payload.components[0],counts,weights),"skin validation fixture changed");
    }
    BE_HostApiV1 host{};host.runtime_invoke=InvokeFake;host.object_unbox=UnboxFake;host.copy_managed_string=StringFake;
    host.object_new=NewFake;host.gchandle_new=TrackedRoot;host.gchandle_free=TrackedFree;
    host.log=[](void*,const char*,const char* message){captured_logs.emplace_back(message?message:"");};
    g_host=&host;g_material_class.class_info=&host;g_texture2d_class.class_info=&host;
    g_weak_new=[](void* object,bool)->uint32_t{return TrackedRoot(nullptr,object,0);};
    g_weak_target=[](uint32_t h)->void*{auto it=tracked_handles.find(h);return it==tracked_handles.end()?nullptr:it->second;};
    ConfigureFakeObjectClasses();
    g_array_new_specific=[](void*,uintptr_t n)->void*{auto* a=Make();a->array.resize(n);return a;};
    fake_transform_type=Make("TransformType");host.resolve_class=[](void*,const char*,const char*,const char*,BE_ResolvedClassV1* result)->BE_Result {
        result->class_info=fake_transform_type;result->type_object=fake_transform_type;return BE_Result_Ok;
    };
    for (auto& method:g_methods) {method.method_info=&method;method.resolved=true;}
    g_static_get=[](const void* field,void* value){std::memcpy(value,fake_static_fields[field].data(),8);};
    g_static_set=[](const void* field,void* value){std::memcpy(fake_static_fields[field].data(),value,8);};
    for (auto& field:g_lod.fields) field.resolved.field_info=&field;
    g_model_build_mesh=[](const BemComponent& component,void* donor,void*& mesh,void*)->bool {
        ++fake_mesh_builds;mesh=nullptr;fake_build_donors.push_back(donor);
        if (std::exchange(fail_next_mesh_build,false)) return false;
        Check(component.streams[2].size()==size_t(component.info.vertex_count)*component.info.stride2,
            "Mesh builder received the metadata-only plan instead of decoded geometry");
        auto* built=static_cast<Fake*>(NewAsset(g_material_class.class_info));if (!built) return false;
        built->name="built-mesh";mesh=built;return true;
    };
    auto owner=std::make_shared<OwnedCharacterAdapter>();owner->id="chr_test";owner->world="world";owner->ui="ui";
    owner->names={"mesh0","mesh1"};for(const auto& name:owner->names) owner->components.push_back({name.c_str(),6});
    owner->adapter={owner->id.c_str(),owner->world.c_str(),owner->ui.c_str(),"",false,owner->components};
    g_registry={};g_registry.owned_adapters.push_back(owner);
    EnabledMod mod{&owner->adapter,path,"default",false,true,"package\ndefault\n\nchecked\noptimized\nrevision",""};
    g_registry.enabled.push_back(mod);g_hot_switch_runtime=true;g_enabled=true;g_stopping=false;g_pump_thread=GetCurrentThreadId();
    g_model_frame=UINT64_MAX;g_model_frame_budget=FrameBudget{};g_model_verify_interval_ms=0;
    AsyncBemLoader::Backend backend;
    backend.plan=[](const BemRequest&,BemLoadPlan& p,std::string&)->bool{p.reservation_bytes=1024;return true;};
    backend.load=[payload](const BemRequest&,uint64_t,BemPocData& out,BemLoadStats&,std::string&)->bool{
        out=payload;return true;
    };
    g_model_loader=std::make_unique<AsyncBemLoader>(AsyncBemLoader::Config{},std::move(backend));
    g_texture_streamer=std::make_unique<TexturePayloadStreamer>();
    // The synchronous transaction reads the same decoded payload from its cache.
    auto cached=std::make_shared<const BemPocData>(payload);
    auto seed_payload_cache=[&]{g_payload_cache.clear();g_payload_cache.push_back({mod.package,mod.appearance,mod.selection_key,cached,1,GetTickCount64()+600000});};
    auto* original_texture=Make("original");std::array<Fake*,2> meshes{},materials{};
    for (int i=0;i<2;++i) {
        meshes[i]=Make("mesh"+std::to_string(i));meshes[i]->poses=Array({Make()});
        materials[i]=Make("material"+std::to_string(i));materials[i]->textures[7]=original_texture;
    }
    auto receiver=[&](const char* name) {
        auto* root=GameObject(name);auto* mesh_all=Make("Mesh_all");mesh_all->parent=root;
        auto* lod=Make("lod0");lod->parent=mesh_all;root->transforms=Array({mesh_all,lod});
        for (int i=0;i<2;++i) {
            auto* r=Make("receiver"+std::to_string(i));r->parent=lod;r->mesh=meshes[i];r->materials=Array({materials[i]});
            auto* bone=Make("bone"+std::to_string(i));bone->parent=root;r->bones=Array({bone});
            root->array.push_back(r);static_cast<Fake*>(root->transforms)->array.push_back(bone);
        }
        return root;
    };
    auto tick=[&]{++fake_frame;ConstructionScope observation;PumpModelJobs(fake_frame);std::this_thread::yield();};
    auto completed=[&](Fake* root) {
        ConstructionScope observation;
        for (const auto& record:g_completed) if (record.root.Get()==root && record.selection_key==mod.selection_key) return IsCompletedResource(owner->adapter,root,mod.selection_key);
        return false;
    };
    auto finish=[&](Fake* root) {
        g_original_finish=[](void*,void*,void*){++finish_calls;};const auto before=finish_calls;
        ResourceFinish(nullptr,root,nullptr);Check(finish_calls==before+1,"Finish did not continue the original delivery exactly once");
    };
    auto replaced=[&](Fake* root) {
        auto* r0=static_cast<Fake*>(root->array[0]);
        return r0->mesh && static_cast<Fake*>(r0->mesh)->name=="built-mesh" && ArrayLength(r0->bones)==2;
    };
    {
        // Clone observation (2026-10-03). The Host create_hook chains several
        // modules on one target, so CustomModel and betterendfield.model both
        // hook Internal_CloneSingleWithParent. Coverage decides delivery mode.
        static std::vector<std::pair<std::string,void*>> owners;static bool exclusive=false;
        static int single_marker=0,parent_marker=0;
        host.create_hook=[](void*,const char* module,void* target,void* detour,void** original)->BE_Result {
            for (const auto& [owner,hooked]:owners) if (hooked==target && (exclusive || owner==module)) return BE_Result_Conflict;
            owners.emplace_back(module,target);*original=detour;return BE_Result_Ok;
        };
        auto* single=Contract("clone.single");auto* parent=Contract("clone.with_parent");
        Check(single && parent && std::string_view(parent->descriptor.method_name)=="Internal_CloneSingleWithParent" &&
            std::string_view(single->descriptor.method_name)=="Internal_CloneSingle" && !single->required && !parent->required,
            "clone contracts missing or required");
        single->pointer=&single_marker;parent->pointer=&parent_marker;
        Contract("clone.instantiate")->pointer=nullptr;Contract("clone.instantiate_with_parent")->pointer=nullptr; // stripped
        Check(!kModelCloneHooksRequested,"production build must keep clone hooks off until clone rebind is device-verified");
        // (a) Not requested: no hook, synchronous template delivery.
        g_model_clone_hooks_requested=false;InstallModelCloneHooks();
        Check(owners.empty() && !g_model_clone_coverage.load() &&
            std::string_view(ModelAsyncDeliveryBlocker(GetTickCount64()))=="clone-coverage-unavailable",
            "unrequested clone hooks changed the delivery mode");
        // (b) An exclusive Host refusing a second owner: logged synchronous fallback.
        g_model_clone_hooks_requested=true;exclusive=true;owners={{"betterendfield.model",&parent_marker}};
        InstallModelCloneHooks();
        Check(!g_model_clone_coverage.load() && LogContains("Model clone hook unavailable (clone.with_parent=") &&
            LogContains("template delivery=synchronous fallback") &&
            std::string_view(ModelAsyncDeliveryBlocker(GetTickCount64()))=="clone-coverage-unavailable",
            "refused clone hook did not fall back to the synchronous transaction");
        // (c) Chaining Host: both installed, betterendfield.model still hooks the same target.
        exclusive=false;owners.clear();g_original_clone_single=nullptr;g_original_clone_with_parent=nullptr;
        InstallModelCloneHooks();
        void* model_original=nullptr;
        Check(g_model_clone_coverage.load() && LogContains("Model clone hooks installed") && owners.size()==2 &&
            host.create_hook(nullptr,"betterendfield.model",&parent_marker,&parent_marker+1,&model_original)==BE_Result_Ok,
            "chained clone hooks not installed or betterendfield.model lost Internal_CloneSingleWithParent");
        g_model_pump_ms=GetTickCount64();
        Check(!ModelAsyncDeliveryBlocker(GetTickCount64()),"clone coverage + live pump did not select frame-sliced delivery");
        // (d) An existing Instantiate(position,rotation) entry that cannot be hooked breaks coverage.
        static int instantiate_marker=0;Contract("clone.instantiate")->pointer=&instantiate_marker;
        owners={{"betterendfield.other",&instantiate_marker}};exclusive=true;InstallModelCloneHooks();
        Check(!g_model_clone_coverage.load() && LogContains("clone.instantiate="),"unhooked Instantiate entry still claimed coverage");
        exclusive=false;owners.clear();InstallModelCloneHooks();
        Check(g_model_clone_coverage.load() && owners.size()==3,"present Instantiate entry was not hooked");
        Contract("clone.instantiate")->pointer=nullptr;
        g_original_clone_single=nullptr;g_original_clone_with_parent=nullptr;g_original_instantiate=nullptr;
        single->pointer=nullptr;parent->pointer=nullptr;host.create_hook=nullptr;
        g_model_clone_hooks_requested=kModelCloneHooksRequested;
    }
    {
        // A: asynchronous geometry Job reaches a Ready commit (failed on device).
        g_model_clone_coverage=true;g_model_pump_ms=GetTickCount64();
        auto* root=receiver("ui");const auto builds=fake_mesh_builds;finish(root);
        Check(fake_mesh_builds==builds && !completed(root),"async Finish built synchronously while the pump was confirmed");
        for (unsigned i=0;i<20000 && !completed(root);++i) tick();
        Check(completed(root) && replaced(root),"async geometry Job never committed (metadata-only plan rejected the palette)");
        Check(fake_mesh_builds>builds && LogContains("Model Ready committed") && LogContains("Model Job phase") &&
            LogContains("Model delivery registered"),"async geometry Job diagnostics missing");
    }
    {
        // B1: pump never ran -> proven synchronous transaction inside Finish.
        g_model_clone_coverage=true;g_model_pump_ms=0;seed_payload_cache();
        auto* root=receiver("ui");finish(root);
        Check(completed(root) && replaced(root),"pump-not-started Finish did not fall back to the synchronous build");
        Check(LogContains("Model delivery synchronous resource=ui role=chr_test reason=pump-not-started"),"synchronous fallback reason not logged");
        const auto jobs=g_model_jobs.size();tick();tick();
        Check(g_model_jobs.size()==jobs && completed(root),"synchronously committed delivery was rebuilt asynchronously");
        // B2: stalled pump.
        g_model_pump_ms=GetTickCount64()-kModelPumpStallMs-1;auto* stalled=receiver("ui");finish(stalled);
        Check(completed(stalled) && LogContains("reason=pump-stalled"),"stalled pump did not fall back to the synchronous build");
    }
    {
        // B3: production default (no parent-clone coverage) keeps templates
        // synchronous; a natural clone of that template is recognised as
        // complete instead of being rebuilt.
        g_model_clone_coverage=false;tick();seed_payload_cache();
        auto* root=receiver("ui");finish(root);
        Check(completed(root) && LogContains("reason=clone-coverage-unavailable"),"default delivery did not stay synchronous");
        auto* clone=GameObject("ui(Clone)");auto* mesh_all=Make("Mesh_all");mesh_all->parent=clone;
        auto* lod=Make("lod0");lod->parent=mesh_all;clone->transforms=Array({mesh_all,lod});
        for (int i=0;i<2;++i) {
            auto* source=static_cast<Fake*>(root->array[i]);
            auto* r=Make(source->name);r->parent=lod;r->mesh=source->mesh;r->materials=Array({});
            static_cast<Fake*>(r->materials)->array=static_cast<Fake*>(source->materials)->array;
            auto* bones=Array({});
            for (void* b:static_cast<Fake*>(source->bones)->array) {
                auto* bone=Make(static_cast<Fake*>(b)->name);bone->parent=clone;bones->array.push_back(bone);
                static_cast<Fake*>(clone->transforms)->array.push_back(bone);
            }
            r->bones=bones;clone->array.push_back(r);
        }
        const auto builds=fake_mesh_builds;RegisterModelClone(root,clone);
        for (int i=0;i<4;++i) tick();
        Check(g_model_jobs.empty() && fake_mesh_builds==builds,"natural clone of a synchronously committed template was rebuilt");
        // Game/camera renderer toggles on a committed receiver are not a new
        // selection: no second decode/build in synchronous template mode.
        static_cast<Fake*>(root->array[0])->enabled=false;
        for (int i=0;i<4;++i) tick();
        Check(g_model_jobs.empty() && fake_mesh_builds==builds,"renderer drift on a synchronous receiver queued a rebuild");
        static_cast<Fake*>(root->array[0])->enabled=true;
        g_model_clone_coverage=true;
    }
    {
        // C: async Job failure after decode -> one synchronous fallback.
        g_model_assets.clear();g_model_plans.clear();seed_payload_cache();
        g_model_pump_ms=GetTickCount64();auto* root=receiver("ui");
        fail_next_mesh_build=true;finish(root);
        for (unsigned i=0;i<20000 && !completed(root);++i) tick();
        Check(completed(root) && replaced(root),"failed async Job did not fall back to the synchronous build");
        Check(LogContains("Model Job failed resource=ui phase=Mesh") && LogContains("reason=Mesh build/readback refused") &&
            LogContains("Model fallback=synchronous resource=ui") && LogContains("Model fallback result=committed"),
            "Job failure/fallback reasons not logged");
    }
    {
        // D: frame-sliced template delivery. An instance cloned from the template
        // before the commit is observed by the clone hook and replaced as well.
        g_model_clone_coverage=true;g_model_pump_ms=GetTickCount64();g_model_assets.clear();g_model_plans.clear();
        auto* root=receiver("ui");finish(root);
        static Fake* early=nullptr;early=receiver("ui(Clone)");
        g_original_clone_with_parent=[](void*,void*,bool,void*)->void*{return early;};
        Check(ModelCloneWithParent(root,nullptr,false,nullptr)==early && !completed(root),
            "clone hook changed the Instantiate result or committed synchronously");
        for (unsigned i=0;i<20000 && !(completed(root) && completed(early));++i) tick();
        Check(completed(root) && replaced(root) && completed(early) && replaced(early) && g_model_clones_registered.load()>=1,
            "an instance cloned before the frame-sliced commit kept the original model");
        Check(LogContains("mode=frame-sliced"),"frame-sliced transaction summary missing");
        g_original_clone_with_parent=nullptr;
    }
    {
        // E: the synchronous transaction decodes deferred texture entries one at
        // a time and releases each after LoadRawTextureData (peak = one texture).
        BemPocData deferred;Check(LoadBem(path,deferred,error,{},nullptr,false,true,{},UINT64_MAX,true),error.c_str());
        for (auto& c:deferred.components) c.info.original_index_count=6;
        uint64_t largest=0,total=0;
        for (const auto& t:deferred.textures) {Check(t.Deferred(),"deferred fixture decoded a texture");largest=std::max<uint64_t>(largest,t.info.data_size);total+=t.info.data_size;}
        Check(deferred.textures.size()>=2 && total>largest,"fixture needs two textures");
        auto shared=std::make_shared<const BemPocData>(deferred);
        g_payload_cache.clear();g_payload_cache.push_back({mod.package,mod.appearance,mod.selection_key,shared,1,GetTickCount64()+600000});
        g_model_clone_coverage=false;const auto first_log=captured_logs.size();
        auto* root=receiver("ui");finish(root);
        Check(completed(root) && replaced(root),"deferred synchronous delivery failed");
        const auto summary=std::find_if(captured_logs.begin()+first_log,captured_logs.end(),[](const auto& line){
            return line.starts_with("Model upload transaction resource=ui mode=synchronous published=true");});
        Check(summary!=captured_logs.end() && summary->find(" decodedPeakBytes="+std::to_string(largest)+" ")!=std::string::npos &&
            summary->find(" textureConstructed="+std::to_string(deferred.textures.size())+" ")!=std::string::npos &&
            summary->find(" uploadFrames=1 ")!=std::string::npos,
            "synchronous transaction held more than one decoded texture or summary missing");
        g_model_clone_coverage=true;
    }
    {
        // F: device regression (diag5 2026-10-03). Without clone coverage the
        // world template is committed synchronously inside _FinishWithAsset.
        // Later the receiver still holds this module's generated Mesh while
        // (1) its material array was rewritten after delivery, or (2) the
        // managed wrappers seen at commit were recycled (weak handles dead,
        // native objects alive). Hot switch must trace the saved Original and
        // build the next package from it; disable must restore the Original.
        g_weak_new=&TrackedWeak;g_model_clone_coverage=false;g_model_assets.clear();g_model_plans.clear();
        g_model_targets.clear(); // earlier blocks' receivers would compete for the one restore per frame
        auto completed_key=[&](Fake* root,const std::string& key) {
            ConstructionScope observation;
            for (const auto& record:g_completed) if (record.root.Get()==root && record.selection_key==key)
                return IsCompletedResource(owner->adapter,root,key);
            return false;
        };
        auto rewrite_materials=[&](Fake* root) {
            for (void* r:root->array) {
                auto* receiver=static_cast<Fake*>(r);auto* rewritten=Array({});
                for (void* m:static_cast<Fake*>(receiver->materials)->array) {
                    auto* source=static_cast<Fake*>(m);auto* copy=Make(source->name);
                    copy->shader=source->shader;copy->textures=source->textures;rewritten->array.push_back(copy);
                }
                receiver->materials=rewritten;
            }
        };
        auto recycle_wrappers=[&](Fake* root) {
            for (auto& [handle,object]:tracked_handles)
                if (object && object!=root && weak_handles.contains(handle) && !StrongHandlesTo(object)) object=nullptr;
        };
        auto run_case=[&](const char* label,bool rewrite,bool recycle) {
            g_registry.enabled={mod};seed_payload_cache();
            auto* root=receiver("world");std::array<void*,2> original_bones{};
            for (int i=0;i<2;++i) original_bones[i]=ArrayValue(static_cast<Fake*>(root->array[i])->bones,0);
            finish(root);
            Check(completed_key(root,mod.selection_key) && replaced(root),"synchronous world template was not committed");
            tick(); // the pump registers the synchronously handled receiver (desired=A)
            auto* r0=static_cast<Fake*>(root->array[0]);void* first_mesh=r0->mesh;
            auto drift=[&]{if (rewrite) rewrite_materials(root);if (recycle) recycle_wrappers(root);};
            drift();
            EnabledMod next=mod;next.selection_key+="\nB";g_registry.enabled={next};
            g_payload_cache.push_back({next.package,next.appearance,next.selection_key,cached,1,GetTickCount64()+600000});
            fake_build_donors.clear();
            for (unsigned i=0;i<20000 && !completed_key(root,next.selection_key);++i) tick();
            if (!completed_key(root,next.selection_key) || !replaced(root) || r0->mesh==first_mesh) {
                std::cerr<<"case="<<label<<'\n';
                for (const auto& line:captured_logs) if (line.find("world")!=line.npos || line.find("lineage")!=line.npos) std::cerr<<"  log: "<<line<<'\n';
                for (auto& t:g_model_targets) std::cerr<<"  target "<<(t.root.Get()?ObjectName(t.root.Get()):"<dead>")<<" desired="<<t.desired.size()<<" failed="<<t.failed_selection.size()<<" verified="<<t.verified_selection.size()<<'\n';
                std::cerr<<"  jobs="<<g_model_jobs.size()<<'\n';
                Check(false,"world hot switch kept the previous package (saved Original not traced)");
            }
            Check(!fake_build_donors.empty() && std::all_of(fake_build_donors.begin(),fake_build_donors.end(),[&](void* donor){
                return donor==meshes[0] || donor==meshes[1];}),"hot switch built from the previous generated Mesh instead of the Original");
            drift();
            g_registry.enabled.clear();
            auto restored=[&]{
                for (int i=0;i<2;++i) {
                    auto* r=static_cast<Fake*>(root->array[i]);
                    if (r->mesh!=meshes[i] || ArrayLength(r->materials)!=1 || ArrayValue(r->materials,0)!=materials[i]) return false;
                }
                return true;
            };
            for (int i=0;i<200 && !restored();++i) tick(); // one restore transaction per frame across all targets
            for (int i=0;i<2;++i) {
                auto* r=static_cast<Fake*>(root->array[i]);
                if (r->mesh!=meshes[i] || ArrayLength(r->materials)!=1 || ArrayValue(r->materials,0)!=materials[i] ||
                    ArrayLength(r->bones)!=1 || ArrayValue(r->bones,0)!=original_bones[i]) {
                    std::cerr<<"case="<<label<<" receiver="<<i<<" mesh="<<(r->mesh==meshes[i])<<" mat="<<(ArrayLength(r->materials)==1 && ArrayValue(r->materials,0)==materials[i])
                        <<" bones="<<(ArrayLength(r->bones)==1 && ArrayValue(r->bones,0)==original_bones[i])<<" nbones="<<ArrayLength(r->bones)<<'\n';
                    for (const auto& line:captured_logs) if (line.find("Hot switch")!=line.npos || line.find("lineage")!=line.npos) std::cerr<<"  log: "<<line<<'\n';
                    Check(false,"hot switch disable did not restore the Original world binding");
                }
            }
            Check(LogContains("Hot switch restored original resource: world"),"disable restore not logged");
            std::erase_if(g_model_targets,[&](const auto& target){return target.root.Get()==root;});
        };
        run_case("materials-rewritten-after-sync-delivery",true,false);
        run_case("managed-wrappers-recycled",false,true);
        run_case("both",true,true);
        g_registry.enabled={mod};g_model_clone_coverage=true;
    }
    Check(LogContains("Model pump first run"),"first pump run not logged");
    {ConstructionScope observation;CancelModelJobs();g_lod.Restore();g_lod.pipeline.Reset();}
    g_model_loader->Shutdown();g_model_loader.reset();g_texture_streamer.reset();g_model_assets.clear();g_model_plans.clear();g_model_targets.clear();g_completed.clear();
    g_generated_texture_identity.clear();g_generated_texture_order.clear();
    {std::lock_guard lock(g_model_clone_watch_mutex);g_model_clone_watch.clear();g_model_clone_watch_size=0;}
    g_payload_cache.clear();cached.reset();
    g_registry={};g_hot_switch_runtime=false;g_enabled=false;g_pump_thread=0;fake_transform_type=nullptr;
    g_model_clone_coverage=false;g_model_pump_ms=0;g_model_verify_interval_ms=500;g_model_build_mesh=&BuildModelMesh;
    g_static_get=nullptr;g_static_set=nullptr;for (auto& field:g_lod.fields) field.resolved.field_info=nullptr;
    Check(roots==0 && tracked_handles.empty(),"fallback/async geometry tests leaked GC handles");
    g_weak_new=nullptr;g_weak_target=nullptr;g_host=nullptr;
    std::cout<<"PASS async geometry Job commit, pump-not-started/stalled synchronous fallback, chained clone hooks/refusal fallback, early clone replaced by frame-sliced commit, natural clone reuse, Job-failure fallback, per-texture synchronous decode and diagnostics\n";
}
void ResourceTypeBoundaryTests(const std::filesystem::path& package_path) {
    BE_HostApiV1 host{};host.runtime_invoke=InvokeFake;host.object_unbox=UnboxFake;host.copy_managed_string=StringFake;
    host.object_new=NewFake;host.gchandle_new=TrackedRoot;host.gchandle_free=TrackedFree;g_host=&host;
    ConfigureFakeObjectClasses();
    g_weak_new=TrackedWeak;
    g_weak_target=[](uint32_t h)->void* {auto it=tracked_handles.find(h);return it==tracked_handles.end()?nullptr:it->second;};
    g_array_new_specific=[](void*,uintptr_t count)->void* {auto* value=Make();value->array.resize(count);return value;};
    for (auto& method:g_methods) {method.method_info=&method;method.resolved=true;}
    g_renderer_class.type_object=Make("RendererType");g_static_renderer_class.class_info=Make("MeshRendererClass");
    g_skinned_renderer_class.class_info=Make("SkinnedMeshRendererClass");g_mesh_filter_class.type_object=Make("MeshFilterType");
    g_material_class.class_info=Make("MaterialClass");
    g_static_get=[](const void* field,void* value){std::memcpy(value,fake_static_fields[field].data(),8);};
    g_static_set=[](const void* field,void* value){std::memcpy(fake_static_fields[field].data(),value,8);};
    for (auto& field:g_lod.fields) field.resolved.field_info=&field;
    g_original_finish=[](void*,void* asset,void*) {++finish_calls;last_finished_asset=asset;};
    g_enabled=true;g_stopping=false;g_hot_switch_runtime=false;g_probe.active=false;

    for (bool static_mesh:{true,false}) {
        auto owner=std::make_shared<OwnedCharacterAdapter>();
        owner->id=static_mesh?"wpn_lance_0006":"chr_test_ult";owner->world=owner->ui=owner->id;
        owner->names={"body_mesh"};owner->components={{owner->names[0].c_str(),6,"Meshes/body",static_mesh}};
        owner->adapter={owner->id.c_str(),owner->world.c_str(),owner->ui.c_str(),"",false,owner->components,
            "explicit","assets/test/resource.prefab",0,true};
        g_registry={};g_registry.owned_adapters.push_back(owner);
        EnabledMod mod{&owner->adapter,package_path,"default",false,false,"type-boundary-selection",""};
        g_registry.enabled.push_back(mod);
        auto payload=std::make_shared<BemPocData>();BemComponent component;component.static_mesh=static_mesh;
        component.info.component_id=0;component.info.original_index_count=6;
        // A real hide operation exercises capture/material preparation/commit
        // without substituting any of those production functions or a GPU.
        component.info.flags=kComponentFlagNoGeometry|kComponentFlagHidden;payload->components.push_back(component);
        g_payload_cache.push_back({mod.package,mod.appearance,mod.selection_key,payload,0,GetTickCount64()+60000});
        const auto receiver=[&](bool clone) {
            auto* asset=GameObject(owner->world+(clone?"(Clone)":""));auto* group=Make("Meshes");group->parent=asset;
            auto* renderer=Make("body");renderer->parent=group;renderer->materials=Array({Make("material")});
            renderer->runtime_class=const_cast<void*>(static_mesh?g_static_renderer_class.class_info:g_skinned_renderer_class.class_info);
            auto* mesh=Make("body_mesh");
            if (static_mesh) {auto* filter=Make("filter");filter->mesh=mesh;renderer->filter=filter;}
            else {renderer->mesh=mesh;auto* bone=Make("bone");bone->parent=asset;renderer->bones=Array({bone});}
            asset->array={renderer};return asset;
        };
        auto* prefab=receiver(false);auto* clone=receiver(true);
        for (const char* type:{"SpriteClass","Texture2DClass"}) {
            auto* collision=Make(owner->world);collision->runtime_class=Make(type);
            // Even a misleadingly populated mock cannot masquerade as a prefab.
            collision->array=prefab->array;
            for (bool async:{false,true}) {
                g_model_clone_coverage=async;g_model_pump_ms=GetTickCount64();
                const auto calls=fake_runtime_calls,finished=finish_calls,held=roots;
                ResourceFinish(nullptr,collision,nullptr);
                Check(finish_calls==finished+1 && last_finished_asset==collision,"same-name non-prefab was not passed through exactly once");
                Check(fake_runtime_calls==calls && roots==held && g_model_deliveries.empty() && g_model_jobs.empty(),
                    "same-name non-prefab was invoked, rooted or queued by delivery");
            }
            const auto calls=fake_runtime_calls,held=roots;
            {
                ConstructionScope scope;std::vector<PreparedBinding> bindings;
                SynchronousModelDelivery(collision,"type-test");
                Check(!ProcessResource(collision,scope),"direct processing accepted a non-prefab");
                Check(!CaptureGenericResourceBindings(owner->adapter,*payload,collision,bindings),"capture accepted a non-prefab");
                Check(!IsCompletedResource(owner->adapter,collision),"completion accepted a non-prefab");
                Check(!RegisterModelDelivery(collision) && !RegisterModelDelivery(prefab,collision),"registration accepted a non-prefab receiver/source");
                Check(!EnqueueResourceBuild(collision,mod) && !RememberModelTarget(collision,mod),"async queue accepted a non-prefab");
            }
            Check(fake_runtime_calls==calls && roots==held,"direct rejection invoked Unity or retained a non-prefab");
        }
        // Unresolved reflection contracts also pass through, even for a valid
        // named prefab; a missing check is never treated as permission.
        for (bool missing_class:{true,false}) {
            const auto cls=g_game_object_class;const auto get_class=g_object_class;
            if (missing_class) g_game_object_class={};else g_object_class=nullptr;
            const auto calls=fake_runtime_calls,finished=finish_calls;
            ResourceFinish(nullptr,prefab,nullptr);
            Check(finish_calls==finished+1 && fake_runtime_calls==calls && g_model_deliveries.empty(),"missing type contract did not fail closed");
            g_game_object_class=cls;g_object_class=get_class;
        }
        g_model_clone_coverage=false;
        for (auto* asset:{prefab,clone}) {
            const auto calls=game_object_calls,finished=finish_calls;
            ResourceFinish(nullptr,asset,nullptr);
            Check(finish_calls==finished+1 && last_finished_asset==asset,"valid prefab/clone lost the original callback");
            Check(game_object_calls>calls && !static_cast<Fake*>(asset->array[0])->enabled,
                "valid static/skinned prefab or clone did not reach the real commit");
            Check(!g_completed.empty() && g_model_deliveries.size()==1,"committed prefab/clone was not recorded");
            g_model_deliveries.clear();
        }
        auto* pending=receiver(true);g_model_clone_coverage=true;g_model_pump_ms=GetTickCount64();
        ResourceFinish(nullptr,pending,nullptr);
        Check(g_model_deliveries.size()==1 && static_cast<Fake*>(pending->array[0])->enabled,"valid async clone did not register before commit");
        g_model_deliveries.clear();
        AsyncBemLoader::Backend backend;
        backend.plan=[](const BemRequest&,BemLoadPlan& plan,std::string&){plan.reservation_bytes=1024;return true;};
        backend.load=[payload](const BemRequest&,uint64_t,BemPocData& out,BemLoadStats&,std::string&){out=*payload;return true;};
        g_model_loader=std::make_unique<AsyncBemLoader>(AsyncBemLoader::Config{},std::move(backend));
        Check(EnqueueResourceBuild(pending,mod) && g_model_jobs.size()==1 && g_model_jobs[0]->target.Get()==pending,
            "valid static/skinned clone was refused by the production async queue");
        {ConstructionScope scope;CancelModelJobs();}
        g_model_loader->Shutdown();g_model_loader.reset();
        g_completed.clear();g_payload_cache.clear();g_original_mesh_pins.clear();
    }
    {ConstructionScope scope;g_lod.Restore();g_lod.pipeline.Reset();}
    {std::lock_guard lock(g_model_clone_watch_mutex);g_model_clone_watch.clear();g_model_clone_watch_size=0;}
    g_model_clone_coverage=false;g_model_pump_ms=0;g_registry={};g_enabled=false;
    g_renderer_class={};g_static_renderer_class={};g_skinned_renderer_class={};g_mesh_filter_class={};g_game_object_class={};
    g_static_get=nullptr;g_static_set=nullptr;for (auto& field:g_lod.fields) field.resolved.field_info=nullptr;
    Check(roots==0 && tracked_handles.empty(),"resource type boundary tests leaked roots");
    g_weak_new=nullptr;g_weak_target=nullptr;g_host=nullptr;
    std::cout<<"PASS resource type boundary: same-name Sprite/Texture2D passthrough, missing contracts, static/skinned prefab+clone real commit and async registration/queue\n";
}

#include "custom_model_scene_rebind_tests.inc"

int main(int argc,char** argv) {
    if(argc==3 && std::string_view(argv[1])=="--scene-rebind") {SceneRebindTests(argv[2]);return 0;}
    if(argc==3 && std::string_view(argv[1])=="--resource-types") {ResourceTypeBoundaryTests(argv[2]);return 0;}
    if(argc==2 && std::string_view(argv[1])=="--static-resource") {StaticResourceTests();return 0;}
    if(argc==3 && std::string_view(argv[1])=="--async") {AsyncJobsTests(argv[2]);AsyncGeometryAndFallbackTests(argv[2]);return 0;}
    if(argc==2 && std::string_view(argv[1])=="--geometry") {CpuGeometryTests();return 0;}
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
    if(argc==3 && std::string_view(argv[1])=="--compare-optimization") {
        // Same package parsed with and without loading_optimization must yield
        // byte-identical components and textures (default appearance).
        BemPocData plain,optimized; std::string e1,e2;
        const bool ok1=LoadBem(argv[2],plain,e1,{},nullptr,false,false);
        const bool ok2=LoadBem(argv[2],optimized,e2,{},nullptr,false,true);
        if(!ok1 || !ok2) { std::cout<<"LOADFAIL plain="<<ok1<<" opt="<<ok2<<" "<<e1<<" | "<<e2<<std::endl; return 2; }
        bool same=plain.components.size()==optimized.components.size() && plain.textures.size()==optimized.textures.size();
        for(size_t i=0;same && i<plain.components.size();++i) {
            const auto& a=plain.components[i]; const auto& b=optimized.components[i];
            same=std::memcmp(&a.info,&b.info,sizeof a.info)==0 && a.streams==b.streams && a.indices==b.indices &&
                a.layout_crc==b.layout_crc && a.bones.size()==b.bones.size() && a.draws.size()==b.draws.size();
            for(size_t d=0;same && d<a.draws.size();++d) same=std::memcmp(&a.draws[d],&b.draws[d],sizeof a.draws[d])==0;
            for(size_t d=0;same && d<a.bones.size();++d) same=std::memcmp(&a.bones[d],&b.bones[d],sizeof a.bones[d])==0;
        }
        for(size_t i=0;same && i<plain.textures.size();++i)
            same=plain.textures[i].name==optimized.textures[i].name && plain.textures[i].data==optimized.textures[i].data;
        std::cout<<(same?"SAME":"DIFF")<<std::endl; return same?0:1;
    }
    if(argc==4 && std::string_view(argv[1])=="--probe-dll") { ProbeDllStartup(argv[2],argv[3]); return 0; }
    if (argc==3 && std::string_view(argv[1])=="--probe") { ProbeTests(argv[2]); return 0; }
    Check(argc>=2,"pass synthetic BEMv1 package path");
    ParserTests(argv[1]); RegistryTests(argv[1]); HotSwitchTests(argv[1]); StaticResourceTests();ResourceTypeBoundaryTests(argv[1]);SceneRebindTests(argv[1]);
    std::cout<<"PASS: BEMv1 parser, exact donor identity, material isolation, rollback, ownership, appearance/LOD routing\n";
}
