// Exercise the production cache transaction against the game's observed
// base-plus-submesh-repeated-VFX composition rule. No production API is replaced.
enum class CacheTestField { HelperController,Infos,Added,Renderer,Source,Submeshes,Replacing,Replacements,
    MeshController,Groups,GroupInfos,FloatProperties,VectorProperties,TextureProperties,KeywordStates,DepthControl,MaterialMap,Count };
int cache_test_field_tokens[static_cast<int>(CacheTestField::Count)]{};
int cache_test_helper_type=0,cache_test_info_class=0,cache_test_list_class=0;
int cache_test_dictionary_class=0,cache_test_keys_class=0,cache_test_mesh_controller_class=0;
int cache_test_group_class=0,cache_test_int_class=0,cache_test_string_class=0;
MethodContract cache_test_list_count{"material_cache.list_count",{},false};
MethodContract cache_test_list_item{"material_cache.list_item",{},false};
size_t cache_test_writes=0,cache_test_composes=0;
int cache_test_fail_compose=0,cache_test_corrupt_prefix=0;
const void* cache_test_drop_field=nullptr;
size_t cache_test_creates=0;
int cache_test_fail_create=0,cache_test_fail_property=0;
struct CacheTestDictionary {
    enum class Keys { Int,String,Object } kind=Keys::Int;
    std::vector<std::pair<void*,void*>> values;
};
struct CacheTestMaterial {
    std::map<int32_t,float> floats;
    std::map<int32_t,Float4> vectors;
    std::map<int32_t,void*> textures;
    std::map<std::string,bool> keywords,passes;
};
std::map<void*,CacheTestDictionary> cache_test_dictionaries;
std::map<void*,CacheTestMaterial> cache_test_materials;
std::map<void*,std::map<const void*,void*>> cache_test_fields;
std::map<std::string,MethodContract> cache_test_dynamic_methods;
Fake* CacheTestDictionaryObject(CacheTestDictionary::Keys kind) {
    auto* value=Make("cache-dictionary");value->runtime_class=&cache_test_dictionary_class;
    cache_test_dictionaries[value].kind=kind;return value;
}
Fake* CacheTestList(std::initializer_list<void*> values) {
    auto* list=Array(values);list->runtime_class=&cache_test_list_class;return list;
}
Fake* CacheTestFloat(float value) {uint32_t bits=0;std::memcpy(&bits,&value,4);return static_cast<Fake*>(Scalar(bits));}
Fake* CacheTestVector(Float4 value,uint8_t multiply=0) {
    auto* boxed=Make("boxed-vector-or-tuple");std::memset(&boxed->matrix,0,sizeof(boxed->matrix));
    std::memcpy(&boxed->matrix,&value,sizeof(value));
    reinterpret_cast<uint8_t*>(&boxed->matrix)[sizeof(value)]=multiply;return boxed;
}
bool CacheTestKeyMatches(const CacheTestDictionary& dictionary,void* key,void* argument) {
    if(dictionary.kind==CacheTestDictionary::Keys::Int)
        return static_cast<int32_t>(static_cast<Fake*>(key)->scalar)==*static_cast<int32_t*>(argument);
    if(dictionary.kind==CacheTestDictionary::Keys::String)
        return static_cast<Fake*>(key)->name==static_cast<Fake*>(argument)->name;
    return key==argument;
}
Fake* CacheTestCloneMaterial(void* source) {
    auto* copy=Make(static_cast<Fake*>(source)->name+"_VFXInstance");
    copy->shader=static_cast<Fake*>(source)->shader;copy->runtime_class=const_cast<void*>(g_material_class.class_info);
    cache_test_materials[copy]=cache_test_materials[source];return copy;
}

const void* CacheTestToken(CacheTestField field) { return &cache_test_field_tokens[static_cast<int>(field)]; }
void* BE_CALL CacheTestRead(void*,const void* field,void* object) {
    auto* n=static_cast<Fake*>(object);
    if(field==CacheTestToken(CacheTestField::HelperController))return n->cache_controller;
    if(field==CacheTestToken(CacheTestField::Infos))return n->cache_infos;
    if(field==CacheTestToken(CacheTestField::Added))return n->cache_added;
    if(field==CacheTestToken(CacheTestField::Renderer))return n->cache_renderer;
    if(field==CacheTestToken(CacheTestField::Source))return n->cache_source;
    if(field==CacheTestToken(CacheTestField::Submeshes))return Scalar(n->cache_submeshes);
    if(field==CacheTestToken(CacheTestField::Replacing))return Scalar(n->cache_replacing);
    if(field==CacheTestToken(CacheTestField::Replacements))return n->cache_replacements;
    if(field==CacheTestToken(CacheTestField::DepthControl))return Scalar(n->props["cache.depth"]);
    if(auto object_fields=cache_test_fields.find(object);object_fields!=cache_test_fields.end()) {
        if(auto value=object_fields->second.find(field);value!=object_fields->second.end())return value->second;
    }
    for(int i=static_cast<int>(CacheTestField::MeshController);i<static_cast<int>(CacheTestField::Count);++i)
        if(field==&cache_test_field_tokens[i])return nullptr;
    Check(false,"unknown material cache field read");return nullptr;
}
void CacheTestWrite(void* object,const void* field,void* value) {
    Check(field==CacheTestToken(CacheTestField::Submeshes) || field==CacheTestToken(CacheTestField::Replacing),
        "scalar IL2CPP setter received a reference or unrelated field");
    Check(value,"scalar IL2CPP setter received a null value address");
    ++cache_test_writes;auto* n=static_cast<Fake*>(object);
    if(field==cache_test_drop_field){cache_test_drop_field=nullptr;return;}
    if(field==CacheTestToken(CacheTestField::Replacing))n->cache_replacing=*static_cast<uint8_t*>(value);
    else n->cache_submeshes=*static_cast<int32_t*>(value);
}
void CacheTestWriteObject(void* object,const void* field,void* value) {
    Check(field==CacheTestToken(CacheTestField::Source) || field==CacheTestToken(CacheTestField::Replacements) ||
        field==CacheTestToken(CacheTestField::MaterialMap),"reference IL2CPP setter received a scalar or unrelated field");
    // The real object setter stores value directly. Reject stack addresses
    // instead of accidentally emulating an extra pointer dereference.
    Check(!value || std::any_of(objects.begin(),objects.end(),[&](const auto& candidate){return candidate.get()==value;}),
        "reference IL2CPP setter received a value address instead of an object");
    ++cache_test_writes;auto* n=static_cast<Fake*>(object);
    if(field==cache_test_drop_field){cache_test_drop_field=nullptr;return;}
    if(field==CacheTestToken(CacheTestField::Source))n->cache_source=value;
    else if(field==CacheTestToken(CacheTestField::Replacements))n->cache_replacements=value;
    else cache_test_fields[object][field]=value;
}
const void* CacheTestMethod(void* klass,const char* name,int count) {
    if(klass==&cache_test_dictionary_class || klass==&cache_test_keys_class ||
        klass==&cache_test_mesh_controller_class || klass==g_material_class.class_info) {
        const std::string key="cache_test."+std::string(name);
        auto [it,inserted]=cache_test_dynamic_methods.try_emplace(key,MethodContract{nullptr,{},false});
        it->second.key=it->first.c_str();return &it->second;
    }
    if(klass!=&cache_test_list_class)return nullptr;
    if(std::string_view(name)=="get_Count" && count==0)return &cache_test_list_count;
    if(std::string_view(name)=="get_Item" && count==1)return &cache_test_list_item;
    return nullptr;
}
void* BE_CALL InvokeCacheTest(void* context,const void* method,void* object,void** args,void** exception) {
    const auto key=std::string_view(static_cast<const MethodContract*>(method)->key);
    auto* n=static_cast<Fake*>(object);
    const auto& descriptor=static_cast<const MethodContract*>(method)->descriptor;
    const std::string_view member=key.starts_with("cache_test.")?key.substr(11):
        std::string_view(descriptor.method_name?descriptor.method_name:"");
    if(key=="material.copy") {
        void* result=InvokeFake(context,method,object,args,exception);
        if(!*exception && cache_test_materials.contains(args[0]))cache_test_materials[object]=cache_test_materials[args[0]];
        return result;
    }
    if(auto dictionary=cache_test_dictionaries.find(object);dictionary!=cache_test_dictionaries.end()) {
        auto& state=dictionary->second;
        if(member=="get_Count")return Scalar(state.values.size());
        if(member=="get_Keys") {auto* keys=Make("cache-keys");keys->runtime_class=&cache_test_keys_class;
            for(const auto& item:state.values)keys->array.push_back(item.first);return keys;}
        auto found=std::find_if(state.values.begin(),state.values.end(),[&](const auto& item){return CacheTestKeyMatches(state,item.first,args[0]);});
        if(member=="ContainsKey")return Scalar(found!=state.values.end());
        if(member=="Remove") {const bool present=found!=state.values.end();if(present)state.values.erase(found);return Scalar(present);}
        if(member=="get_Item") {if(found!=state.values.end())return found->second;*exception=n;return nullptr;}
    }
    if(n && n->runtime_class==&cache_test_keys_class && member=="CopyTo") {
        auto& destination=static_cast<Fake*>(args[0])->array;const int offset=*static_cast<int32_t*>(args[1]);
        Check(offset>=0 && destination.size()>=n->array.size()+size_t(offset),"key collection copy exceeded its managed array");
        std::copy(n->array.begin(),n->array.end(),destination.begin()+offset);return nullptr;
    }
    if(member=="_CreateReplaceMaterial") {
        ++cache_test_creates;if(cache_test_fail_create){--cache_test_fail_create;*exception=n;return nullptr;}
        auto* info=static_cast<Fake*>(args[0]);void* group=args[1];
        auto*& map=cache_test_fields[group][CacheTestToken(CacheTestField::MaterialMap)];
        if(!map)map=CacheTestDictionaryObject(CacheTestDictionary::Keys::Object);
        auto& mappings=cache_test_dictionaries[map].values;auto* copies=Array({});
        for(void* source:static_cast<Fake*>(info->cache_source)->array) {
            auto found=std::find_if(mappings.begin(),mappings.end(),[&](const auto& item){return item.first==source;});
            if(found==mappings.end()){mappings.emplace_back(source,CacheTestCloneMaterial(source));found=std::prev(mappings.end());}
            copies->array.push_back(found->second);
        }
        info->cache_replacements=copies;info->cache_replacing=1;
        static_cast<Fake*>(info->cache_renderer)->materials=copies;return nullptr;
    }
    if(member=="PropertyToID") {
        const auto& name=static_cast<Fake*>(args[0])->name;
        return Scalar(name=="_ZTestGBuffer"?901:name=="_OutlineZTest"?902:999);
    }
    if(n && (cache_test_materials.contains(object) || n->runtime_class==g_material_class.class_info)) {
        auto& state=cache_test_materials[object];
        const bool int_property=key.starts_with("cache_test.") ||
            (descriptor.parameter_types && std::string_view(descriptor.parameter_types).starts_with("System.Int32"));
        if(member=="IsKeywordEnabled")return Scalar(state.keywords[static_cast<Fake*>(args[0])->name]);
        if(member=="EnableKeyword" || member=="DisableKeyword") {
            if(cache_test_fail_property && !--cache_test_fail_property){*exception=n;return nullptr;}
            state.keywords[static_cast<Fake*>(args[0])->name]=member=="EnableKeyword";return nullptr;
        }
        if(member=="GetShaderPassEnabled" || member=="GetShaderPassEnabledAndExisted")
            return Scalar(state.passes[static_cast<Fake*>(args[0])->name]);
        if(member=="SetShaderPassEnabled") {
            if(cache_test_fail_property && !--cache_test_fail_property){*exception=n;return nullptr;}
            state.passes[static_cast<Fake*>(args[0])->name]=*static_cast<uint8_t*>(args[1])!=0;return nullptr;
        }
        if(!int_property && member=="SetFloat") {
            uint32_t bits=0;std::memcpy(&bits,args[1],sizeof(bits));
            n->props[static_cast<Fake*>(args[0])->name]=bits;return nullptr;
        }
        if(int_property && args) {
            const int32_t id=*static_cast<int32_t*>(args[0]);
            if(member=="HasProperty")return Scalar(state.floats.contains(id) || state.vectors.contains(id) || state.textures.contains(id));
            if(member=="GetFloat" || member=="GetFloatImpl")return CacheTestFloat(state.floats[id]);
            if(member=="GetVector" || member=="GetColor" || member=="GetColorImpl")return CacheTestVector(state.vectors[id]);
            if(member=="GetTexture" || member=="GetTextureImpl")return state.textures[id];
            if(member=="SetFloat" || member=="SetVector" || member=="SetTexture") {
                if(cache_test_fail_property && !--cache_test_fail_property){*exception=n;return nullptr;}
                if(member=="SetFloat")state.floats[id]=*static_cast<float*>(args[1]);
                if(member=="SetVector")state.vectors[id]=*static_cast<Float4*>(args[1]);
                if(member=="SetTexture")state.textures[id]=args[1];return nullptr;
            }
        }
    }
    if(key=="material_cache.list_count")return Scalar(n->array.size());
    if(key=="material_cache.list_item")return n->array.at(*static_cast<int32_t*>(args[0]));
    if(key=="material_cache.apply") {
        ++cache_test_composes;
        if(cache_test_fail_compose){--cache_test_fail_compose;*exception=n;return nullptr;}
        auto* info=static_cast<Fake*>(args[0]);
        auto* base=static_cast<Fake*>(info->cache_replacing?info->cache_replacements:info->cache_source);
        Check(base && info->cache_renderer,"invalid cache recomposition input");
        auto* result=Make("composed-cache-materials");result->array=base->array;
        for(void* effect:n->array)for(int i=0;i<info->cache_submeshes;++i)result->array.push_back(effect);
        if(cache_test_corrupt_prefix){--cache_test_corrupt_prefix;result->array[0]=Make("wrong-base-material");}
        static_cast<Fake*>(info->cache_renderer)->materials=result;return nullptr;
    }
    return InvokeFake(context,method,object,args,exception);
}
struct CacheTestFixture {
    Fake* renderer=Make("same-renderer-name");Fake* original_mesh=Make("original-mesh");
    Fake* original_materials=Array({Make("original-material")});
    Fake* helper=Make("helper");Fake* controller=Make("controller");
    Fake* infos=Array({});Fake* added=Array({});Fake* info=Make("info");
    CacheTestFixture() {
        renderer->mesh=original_mesh;renderer->materials=original_materials;renderer->cache_helper=helper;
        helper->cache_controller=controller;controller->cache_infos=infos;controller->cache_added=added;
        infos->runtime_class=&cache_test_list_class;infos->array={info};
        info->runtime_class=&cache_test_info_class;info->cache_renderer=renderer;info->cache_source=original_materials;
    }
    PreparedBinding Binding(int slots) {
        PreparedBinding b;b.renderer=renderer;b.original_mesh=renderer->mesh;b.original_materials=renderer->materials;
        b.original_enabled=renderer->enabled;b.custom_enabled=true;b.custom_mesh=Make("custom-mesh");
        static_cast<Fake*>(b.custom_mesh)->submesh_count=slots;
        auto* materials=Array({});for(int i=0;i<slots;++i)materials->array.push_back(Make("slot-"+std::to_string(i)));
        b.custom_materials=materials;return b;
    }
    void CheckSlots(void* base,int slots,int effects) {
        Check(info->cache_source==base && info->cache_submeshes==slots,"base cache did not track current mesh/materials");
        Check(MaterialCachePrefixMatches(renderer,base),"game effect overwrote replacement base materials");
        Check(static_cast<Fake*>(renderer->materials)->array.size()==size_t(slots*(1+effects)),"VFX slots did not follow replacement submesh count");
    }
    void Compose() {void* args[]{info};void* exception=nullptr;InvokeCacheTest(nullptr,Contract("material_cache.apply"),added,args,&exception);Check(!exception,"unexpected fixture effect exception");}
};
struct CacheActiveTestFixture : CacheTestFixture {
    Fake* mesh_controller=Make("mesh-material-controller");
    Fake* group=Make("active-material-group");
    Fake* effect_texture=Make("effect-texture");
    Fake* base_texture=Make("base-texture");
    Fake* map=CacheTestDictionaryObject(CacheTestDictionary::Keys::Object);
    CacheActiveTestFixture() {
        mesh_controller->runtime_class=&cache_test_mesh_controller_class;group->runtime_class=&cache_test_group_class;
        cache_test_fields[controller][CacheTestToken(CacheTestField::MeshController)]=mesh_controller;
        cache_test_fields[mesh_controller][CacheTestToken(CacheTestField::Groups)]=CacheTestList({group});
        cache_test_fields[group][CacheTestToken(CacheTestField::GroupInfos)]=CacheTestList({info});
        auto dictionary=[&](CacheTestField field,CacheTestDictionary::Keys kind,std::initializer_list<std::pair<void*,void*>> values) {
            auto* result=CacheTestDictionaryObject(kind);cache_test_dictionaries[result].values=values;
            cache_test_fields[group][CacheTestToken(field)]=result;
        };
        dictionary(CacheTestField::FloatProperties,CacheTestDictionary::Keys::Int,{{Scalar(11),CacheTestFloat(.65f)}});
        dictionary(CacheTestField::VectorProperties,CacheTestDictionary::Keys::Int,
            {{Scalar(12),CacheTestVector({2,3,4,5},1)},{Scalar(13),CacheTestVector({9,8,7,6})}});
        dictionary(CacheTestField::TextureProperties,CacheTestDictionary::Keys::Int,
            {{Scalar(14),effect_texture},{Scalar(15),nullptr}});
        dictionary(CacheTestField::KeywordStates,CacheTestDictionary::Keys::String,
            {{Make("TEST_ACTIVE"),Scalar(1)},{Make("_ALPHABLEND_ON"),Scalar(0)}});
        group->props["cache.depth"]=1;
        cache_test_fields[group][CacheTestToken(CacheTestField::MaterialMap)]=map;
        static_cast<Fake*>(original_materials->array[0])->name="M_actor_aglina_face_01";
        ConfigureMaterials(original_materials);
        auto* copy=CacheTestCloneMaterial(original_materials->array[0]);
        cache_test_dictionaries[map].values={{original_materials->array[0],copy}};
        info->cache_replacements=Array({copy});info->cache_replacing=1;renderer->materials=info->cache_replacements;
    }
    void ConfigureMaterials(void* materials) {
        for(void* material:static_cast<Fake*>(materials)->array) {
            auto* value=static_cast<Fake*>(material);value->runtime_class=const_cast<void*>(g_material_class.class_info);
            if(!value->shader)value->shader=Make("shader");
            auto& state=cache_test_materials[material];state.floats={{11,-1},{901,7},{902,8}};
            state.vectors={{12,{.5f,1,1.5f,2}},{13,{0,0,0,0}}};
            state.textures={{14,base_texture},{15,nullptr}};state.keywords={{"TEST_ACTIVE",false},{"_ALPHABLEND_ON",false}};
            state.passes={{"DepthOnly",false}};
        }
    }
    PreparedBinding ActiveBinding(int slots) {auto binding=Binding(slots);ConfigureMaterials(binding.custom_materials);return binding;}
    void CheckActive(void* base,int slots,int effects) {
        Check(info->cache_source==base && info->cache_submeshes==slots && info->cache_replacing,"active rebase lost source, submesh count or active flag");
        auto* copies=static_cast<Fake*>(info->cache_replacements);
        Check(copies && copies->array.size()==size_t(slots) && MaterialCachePrefixMatches(renderer,copies),"active copies were not published as live prefix");
        Check(static_cast<Fake*>(renderer->materials)->array.size()==size_t(slots*(1+effects)),"active VFX tail used obsolete submesh count");
        for(int i=0;i<slots;++i) {
            void* copy=copies->array[i];Check(copy!=ArrayValue(base,i),"active rebase published source material directly");
            auto& state=cache_test_materials[copy];const Float4 multiplied{1,3,6,10},direct{9,8,7,6};
            Check(state.floats[11]==.65f && !std::memcmp(&state.vectors[12],&multiplied,sizeof(Float4)) &&
                !std::memcmp(&state.vectors[13],&direct,sizeof(Float4)),"active float/vector properties were not replayed from true source");
            Check(state.textures[14]==effect_texture && state.textures[15]==nullptr && state.keywords["TEST_ACTIVE"] &&
                !state.keywords["_ALPHABLEND_ON"],"active nullable texture or keyword replay failed");
            Check(state.floats[901]==3 && state.floats[902]==3 && state.passes["DepthOnly"],"active depth control did not preserve game semantics");
        }
    }
};
void MaterialCacheSyncTests() {
    Check(roots==0 && tracked_handles.empty(),"material cache tests require clean GC roots");
    const auto saved_api=g_material_cache_api;const auto saved_host=g_host;
    const auto saved_weak_new=g_weak_new;const auto saved_weak_target=g_weak_target;
    const auto saved_object_class=g_object_class;const auto saved_game_object_class=g_game_object_class;
    const auto saved_material_class=g_material_class;
    const auto saved_array_new=g_il2cpp_array_new;const auto saved_array_specific=g_array_new_specific;
    std::vector<std::pair<const void*,bool>> saved_methods;
    for(auto& method:g_methods){saved_methods.emplace_back(method.method_info,method.resolved);method.method_info=&method;method.resolved=true;}
    BE_HostApiV1 host{};host.runtime_invoke=InvokeCacheTest;host.object_unbox=UnboxFake;host.copy_managed_string=StringFake;
    host.gchandle_new=TrackedRoot;host.gchandle_free=TrackedFree;host.field_get_value_object=CacheTestRead;host.object_new=NewFake;g_host=&host;
    host.string_new=[](void*,const char* value)->void*{return Make(value);};
    g_il2cpp_array_new=[](void*,uintptr_t count)->void*{auto* array=Make();array->array.resize(count);return array;};
    g_array_new_specific=[](void*,uintptr_t count)->void*{auto* array=Make();array->array.resize(count);return array;};
    g_weak_new=TrackedWeak;g_weak_target=[](uint32_t handle)->void*{auto it=tracked_handles.find(handle);return it==tracked_handles.end()?nullptr:it->second;};
    ConfigureFakeObjectClasses();g_material_class.class_info=Make("MaterialClass");g_material_cache_api={};
    auto& api=g_material_cache_api;api.attempted=api.required=api.ready=true;
    api.active_attempted=api.active_ready=true;api.int_class.class_info=&cache_test_int_class;
    api.string_class.class_info=&cache_test_string_class;api.group_class.class_info=&cache_test_group_class;
    api.helper.type_object=&cache_test_helper_type;api.info_class.class_info=&cache_test_info_class;
    api.set=CacheTestWrite;api.set_object=CacheTestWriteObject;api.method=CacheTestMethod;
    BE_ResolvedFieldV1* fields[]{&api.helper_controller,&api.infos,&api.added,&api.renderer,&api.source,&api.submeshes,&api.replacing,&api.replacements,
        &api.mesh_controller,&api.groups,&api.group_infos,&api.float_properties,&api.vector_properties,&api.texture_properties,&api.keyword_states,&api.depth_control,&api.material_map};
    for(size_t i=0;i<std::size(fields);++i)fields[i]->field_info=&cache_test_field_tokens[i];
    cache_test_writes=cache_test_composes=0;cache_test_fail_compose=cache_test_corrupt_prefix=0;cache_test_drop_field=nullptr;
    cache_test_creates=0;cache_test_fail_create=cache_test_fail_property=0;
    cache_test_dictionaries.clear();cache_test_materials.clear();cache_test_fields.clear();
    {
        ConstructionScope scope;CacheTestFixture f;
        auto* first=Array({Make("ABI-first-material")});
        auto* second=Array({Make("ABI-second-material"),Make("ABI-third-material")});
        const auto writes=cache_test_writes;
        Check(WriteMaterialCacheObject(f.info,api.source,first) && ReadMaterialCacheField(f.info,api.source)==first,
            "reference setter did not preserve the first material array identity");
        Check(WriteMaterialCacheObject(f.info,api.source,second) && ReadMaterialCacheField(f.info,api.source)==second,
            "reference setter did not preserve the second material array identity");
        Check(WriteMaterialCacheObject(f.info,api.source,nullptr) && !ReadMaterialCacheField(f.info,api.source),
            "reference setter did not store a null object directly");
        Check(WriteMaterialCacheObject(f.info,api.source,f.original_materials) &&
            ReadMaterialCacheField(f.info,api.source)==f.original_materials &&
            WriteMaterialCacheInt(f.info,api.submeshes,1) && f.info->cache_submeshes==1 &&
            cache_test_writes==writes+5,
            "separate reference and scalar setters lost value identity or write accounting");
        auto binding=f.Binding(19);
        Check(PrepareRendererMaterialCache(binding) && ApplyPreparedBinding(binding) &&
            ReadMaterialCacheField(f.info,api.source)==binding.custom_materials && cache_test_writes==writes+7,
            "source commit did not use the direct-object setter ABI");
        Check(RestorePreparedBinding(binding) && ReadMaterialCacheField(f.info,api.source)==f.original_materials &&
            cache_test_writes==writes+9,
            "source rollback did not restore the exact original array with the direct-object ABI");
        f.CheckSlots(f.original_materials,1,0);
    }
    Check(roots==0 && tracked_handles.empty(),"reference setter ABI regression leaked GC roots");
    {
        ConstructionScope scope;CacheTestFixture f;
        auto* asset=GameObject("cache_test_postmodel");f.renderer->parent=asset;
        const std::array<ComponentIdentity,1> components{{{"original-mesh",6,"same-renderer-name",false}}};
        const CharacterAdapter adapter{"cache_test","cache_test_postmodel","cache_test_postmodel","",false,
            components,"character","assets/test/cache_test_postmodel.prefab",0,true};
        f.added->array={Make("initial-effect-0"),Make("initial-effect-1")};f.Compose();
        auto* composed=f.renderer->materials;auto binding=f.Binding(19);
        binding.original_bones=Array({});binding.custom_mesh=f.original_mesh;
        CompletedResource record;
        Check(RememberResource(adapter,asset,{binding},record,"first-with-effects",true),
            "first enable with existing additive effects could not retain Original");
        Check(record.bindings.size()==1 && record.bindings[0].original,"first enable lost its Original record");
        const auto& original=*record.bindings[0].original;
        Check(original.materials.size()==1 && original.materials[0].Get()==f.original_materials->array[0] &&
            original.material_cache_materials.Get()==f.original_materials,
            "first enable retained additive effects as Original base materials");
        Check(binding.original_materials==composed && static_cast<Fake*>(composed)->array.size()==3,
            "saving Original discarded the complete rollback material array");
    }
    Check(roots==0 && tracked_handles.empty(),"first-enable Original cache capture leaked GC roots");
    {
        ConstructionScope scope;CacheTestFixture f;
        f.added->array={Make("keep-effect-0"),Make("keep-effect-1")};f.Compose();
        auto binding=f.Binding(1);binding.custom_mesh=f.original_mesh;void* donor=nullptr;
        const auto writes=cache_test_writes;
        Check(ResolveMaterialCopyDonor(binding,donor) && donor==f.original_materials && !binding.material_cache,
            "keep-copy probe retained an early cache snapshot or selected composed materials");
        Check(cache_test_writes==writes && f.renderer->materials==binding.original_materials,
            "keep-copy donor probe changed the live renderer or game cache");
        Check(CopyMaterials(binding) && binding.copied_donor_materials && ArrayLength(binding.custom_materials)==1 &&
            ArrayValue(binding.custom_materials,0)!=ArrayValue(f.original_materials,0) && !binding.material_cache,
            "first keep copy cloned additive effect slots into the base material array");
        Check(PrepareRendererMaterialCache(binding) && ApplyPreparedBinding(binding),
            "first keep copy with existing additive effects failed to commit");
        f.CheckSlots(binding.custom_materials,1,2);
        Check(RestorePreparedBinding(binding),"first keep copy failed to restore the original base");
        f.CheckSlots(f.original_materials,1,2);
    }
    Check(roots==0 && tracked_handles.empty(),"keep-copy cache probe leaked GC roots");
    {
        ConstructionScope scope;CacheTestFixture f;auto binding=f.Binding(1);
        Check(CopyMaterials(binding) && binding.copied_donor_materials && !binding.material_cache,
            "late cache initialization fixture failed to copy donor materials");
        f.info->cache_source=Array({Make("new-base-0"),Make("new-base-1")});
        f.info->cache_submeshes=2;f.Compose();
        Check(PrepareRendererMaterialCache(binding),"late cache source could not be captured at commit");
        const auto writes=cache_test_writes;
        Check(!ApplyRendererMaterialCache(binding) && !binding.material_cache->touched && cache_test_writes==writes,
            "final cache accepted a keep copy with an obsolete base material slot count");
    }
    Check(roots==0 && tracked_handles.empty(),"late keep-copy cache rejection leaked GC roots");
    {
        ConstructionScope scope;CacheTestFixture f;auto first=f.Binding(19);
        Check(PrepareRendererMaterialCache(first),"19-slot cache prepare failed");
        Check(StrongHandlesTo(f.original_materials)>0,"cache snapshot did not strongly retain original materials");
        Check(ApplyPreparedBinding(first),"19-slot cache commit failed");f.CheckSlots(first.custom_materials,19,0);
        auto* fx0=Make("teleport-0");auto* fx1=Make("teleport-1");f.added->array={fx0,fx1};f.Compose();f.CheckSlots(first.custom_materials,19,2);
        f.added->array={fx1};f.Compose();f.CheckSlots(first.custom_materials,19,1);
        f.added->array.clear();f.Compose();f.CheckSlots(first.custom_materials,19,0);
        auto second=f.Binding(8);Check(PrepareRendererMaterialCache(second) && ApplyPreparedBinding(second),"19-to-8 slot hot switch failed");
        f.added->array={fx0};f.Compose();f.CheckSlots(second.custom_materials,8,1);
        Check(RestorePreparedBinding(second),"second cache transaction did not restore first package");f.CheckSlots(first.custom_materials,19,1);
        Check(RestorePreparedBinding(first),"disabling cache transaction failed");f.CheckSlots(f.original_materials,1,1);
        Check(f.renderer->mesh==f.original_mesh,"disabling did not restore original mesh");
        f.added->array.clear();f.Compose();f.CheckSlots(f.original_materials,1,0);
    }
    Check(roots==0 && tracked_handles.empty(),"cache hot-switch cycle leaked GC roots");
    for(int changed=0;changed<6;++changed) {
        ConstructionScope scope;CacheTestFixture f;auto b=f.Binding(19);Check(PrepareRendererMaterialCache(b),"stale cache fixture preparation failed");
        if(changed==0)f.info->cache_source=Array({Make("newer-game-material")});
        if(changed==1)f.info->cache_submeshes=2;
        if(changed==2)f.info->cache_replacing=1;
        if(changed==3)f.info->cache_replacements=Array({Make("newer-game-replacement")});
        if(changed==4)static_cast<Fake*>(f.info->cache_source)->array[0]=Make("in-place-newer-game-material");
        if(changed==5)f.info->cache_renderer=Make("reassigned-renderer");
        const auto writes=cache_test_writes;const auto source=f.info->cache_source;const auto submeshes=f.info->cache_submeshes;
        Check(!ApplyRendererMaterialCache(b) && !b.material_cache->touched,"stale cache snapshot was accepted");
        Check(RestoreRendererMaterialCache(b) && cache_test_writes==writes && f.info->cache_source==source && f.info->cache_submeshes==submeshes,"rejected snapshot overwrote newer game state");
    }
    Check(roots==0 && tracked_handles.empty(),"stale cache rejection leaked GC roots");
    for(int failure=0;failure<4;++failure) {
        ConstructionScope scope;CacheTestFixture f;std::vector<PreparedBinding> bindings;bindings.push_back(f.Binding(19));
        Check(PrepareRendererMaterialCache(bindings[0]),"cache rollback preparation failed");
        if(failure==0)cache_test_drop_field=CacheTestToken(CacheTestField::Submeshes);
        if(failure==1)cache_test_fail_compose=1;
        if(failure==2)cache_test_corrupt_prefix=1;
        if(failure==3){reject_material_renderer=f.renderer;rejected=false;}
        Check(CommitResource<PreparedBinding>(bindings,ApplyPreparedBinding,RestorePreparedBinding)==CommitResult::Restored,"cache transaction failure did not roll back");
        Check(f.info->cache_source==f.original_materials && f.info->cache_submeshes==1 && f.renderer->mesh==f.original_mesh &&
            SameMaterialSet(f.renderer->materials,f.original_materials) && !bindings[0].material_cache->touched,"cache rollback left mixed renderer/cache state");
        reject_material_renderer=nullptr;rejected=false;cache_test_drop_field=nullptr;
    }
    Check(roots==0 && tracked_handles.empty(),"cache rollback leaked GC roots");
    {
        ConstructionScope scope;CacheTestFixture first,second;std::vector<PreparedBinding> bindings;
        bindings.push_back(first.Binding(19));bindings.push_back(second.Binding(8));
        for(auto& binding:bindings)Check(PrepareRendererMaterialCache(binding),"multi-renderer cache preparation failed");
        first.added->array={Make("still-active-effect")};reject_material_renderer=second.renderer;rejected=false;
        Check(CommitResource<PreparedBinding>(bindings,ApplyPreparedBinding,RestorePreparedBinding)==CommitResult::Restored,"later renderer failure did not restore earlier cache transaction");
        first.CheckSlots(first.original_materials,1,1);second.CheckSlots(second.original_materials,1,0);
        Check(first.renderer->mesh==first.original_mesh && second.renderer->mesh==second.original_mesh &&
            !bindings[0].material_cache->touched && !bindings[1].material_cache->touched,"multi-renderer rollback left a partially published package");
        reject_material_renderer=nullptr;rejected=false;
    }
    Check(roots==0 && tracked_handles.empty(),"multi-renderer cache rollback leaked GC roots");
    {
        ConstructionScope scope;CacheTestFixture f;auto b=f.Binding(19);
        Check(PrepareRendererMaterialCache(b) && ApplyPreparedBinding(b),"restore retry fixture setup failed");
        cache_test_drop_field=CacheTestToken(CacheTestField::Source);
        Check(!RestorePreparedBinding(b) && b.material_cache->touched,"failed cache restoration falsely reported success");
        Check(f.info->cache_submeshes==1,"source restore failure skipped independent submesh restoration");
        Check(RestorePreparedBinding(b) && !b.material_cache->touched,"failed cache restoration could not be retried");
        f.CheckSlots(f.original_materials,1,0);
    }
    Check(roots==0 && tracked_handles.empty(),"cache restore retry leaked GC roots");
    {
        ConstructionScope scope;CacheActiveTestFixture f;
        f.added->array={Make("active-effect-0"),Make("active-effect-1")};f.Compose();
        auto probe=f.ActiveBinding(1);void* donor=nullptr;const auto writes=cache_test_writes;
        Check(ResolveMaterialCopyDonor(probe,donor) && donor==f.original_materials && !probe.material_cache &&
            ObjectName(ArrayValue(probe.original_materials,0))=="M_actor_aglina_face_01_VFXInstance",
            "active donor probe did not resolve the true source behind the live VFX instance");
        BemPocData bem;BemComponent draw;draw.info.component_id=0;draw.draws.resize(1);
        draw.draws[0].material_component=0;draw.draws[0].material_slot=0;
        draw.material_names={"M_actor_aglina_face_01"};std::vector<PreparedBinding> bindings;bindings.push_back(f.ActiveBinding(1));
        Check(PrepareDrawMaterials(draw,bindings[0],bindings,bem) && !bindings[0].material_cache &&
            ObjectName(ArrayValue(bindings[0].custom_materials,0))=="M_actor_aglina_face_01",
            "active draw preparation compared live VFX instance name instead of true source");
        BemComponent keep;keep.keep_material_overrides.push_back({0,0,0});keep.keep_material_names=draw.material_names;
        auto kept=f.ActiveBinding(1);
        Check(PrepareKeepMaterials(keep,kept,bem) && ArrayLength(kept.custom_materials)==1 && !kept.material_cache &&
            ObjectName(ArrayValue(kept.custom_materials,0))=="M_actor_aglina_face_01",
            "active keep preparation failed to copy and validate true source");
        Check(cache_test_writes==writes,"active material preparation mutated the game cache");
    }
    Check(roots==0 && tracked_handles.empty(),"active material preparation leaked GC roots");
    for(int no_added=0;no_added<2;++no_added) {
        ConstructionScope scope;CacheActiveTestFixture f;
        if(no_added)f.controller->cache_added=nullptr;
        else f.added->array={Make("active-effect-0"),Make("active-effect-1")};
        const int effects=no_added?0:2;f.Compose();
        const auto creates=cache_test_creates;auto first=f.ActiveBinding(19);
        Check(PrepareRendererMaterialCache(first) && ApplyPreparedBinding(first),"active 19-slot enable failed");
        f.CheckActive(first.custom_materials,19,effects);
        auto second=f.ActiveBinding(8);
        Check(PrepareRendererMaterialCache(second) && ApplyPreparedBinding(second),"active 19-to-8 slot switch failed");
        f.CheckActive(second.custom_materials,8,effects);
        auto disabled=f.ActiveBinding(1);disabled.custom_mesh=f.original_mesh;disabled.custom_materials=f.original_materials;
        Check(PrepareRendererMaterialCache(disabled) && ApplyPreparedBinding(disabled),"active disable to original base failed");
        f.CheckActive(f.original_materials,1,effects);
        Check(cache_test_creates==creates+3 && cache_test_dictionaries[f.map].values.size()==28,
            "active rebase skipped recreation or cleared another existing material mapping");
        f.added->array.clear();f.Compose();f.CheckActive(f.original_materials,1,0);
    }
    Check(roots==0 && tracked_handles.empty(),"active enable/switch/disable leaked moved GC roots");
    {
        ConstructionScope scope;CacheActiveTestFixture f;f.added->array={Make("driver-effect-0"),Make("driver-effect-1")};f.Compose();
        auto binding=f.ActiveBinding(2);
        Check(PrepareRendererMaterialCache(binding) && ApplyPreparedBinding(binding),"active driver fixture could not commit");
        CompletedBinding owned;Check(owned.renderer.Set(f.renderer) && owned.mesh.Set(f.renderer->mesh),"active driver receiver identity could not be recorded");
        for(int i=0;i<2;++i) {owned.materials.emplace_back();Check(owned.materials.back().Set(ArrayValue(binding.custom_materials,i)),"active driver source material could not be recorded");}
        owned.material_overrides.resize(2);owned.material_overrides[0].floats.emplace_back("_TestDriven",.2f);
        void* source=ArrayValue(binding.custom_materials,0);void* live=ArrayValue(f.renderer->materials,0);
        auto sampled=[](void* material) {float result=0;const uint32_t bits=static_cast<uint32_t>(static_cast<Fake*>(material)->props["_TestDriven"]);std::memcpy(&result,&bits,sizeof(result));return result;};
        std::vector<void*> visited;
        ForEachOwnedMaterial(owned,0,[&](void* material,const CompletedBinding&) {
            visited.push_back(material);SetDriverMaterialProperty(material,DriverMaterialKind::Float,"_TestDriven",.9f,{},false);
        });
        Check(visited.size()==2 && visited[0]==source && visited[1]==live && sampled(source)==.9f && sampled(live)==.9f,
            "driver did not update the verified source and live game copy exactly once");
        for(int i=1;i<ArrayLength(f.renderer->materials);++i)
            Check(!static_cast<Fake*>(ArrayValue(f.renderer->materials,i))->props.contains("_TestDriven"),"driver touched another base slot or appended FX material");
        DriverMaterialWrite write{};write.slot=0;write.kind=DriverMaterialKind::Float;write.name="_TestDriven";
        ForEachOwnedMaterial(owned,0,[&](void* material,const CompletedBinding& record) {
            Check(ReplayDriverMaterialRest(write,material,record),"driver static rest was not found");
        });
        Check(sampled(source)==.2f && sampled(live)==.2f,"driver stop did not restore authored static rest on both owned materials");
        auto* valid=f.renderer->materials;auto* unknown=Make("unknown-third-party-material");f.renderer->materials=Array({unknown});visited.clear();
        ForEachOwnedMaterial(owned,0,[&](void* material,const CompletedBinding&) {visited.push_back(material);});
        Check(visited.empty() && !unknown->props.contains("_TestDriven") && sampled(source)==.2f,"driver accepted an unrelated live replacement");
        f.renderer->materials=valid;visited.clear();
        ForEachOwnedMaterial(owned,2,[&](void* material,const CompletedBinding&) {visited.push_back(material);});
        Check(visited.empty(),"driver interpreted an FX tail slot as an owned package material");
        f.renderer->materials=binding.custom_materials;
        ForEachOwnedMaterial(owned,0,[&](void* material,const CompletedBinding&) {visited.push_back(material);});
        Check(visited.size()==1 && visited[0]==source,"direct base driver path visited the same material more than once");
        f.renderer->materials=valid;f.renderer->mesh=Make("unrelated-mesh");visited.clear();
        ForEachOwnedMaterial(owned,0,[&](void* material,const CompletedBinding&) {visited.push_back(material);});
        Check(visited.empty(),"driver accepted a receiver that no longer had the recorded mesh");
    }
    Check(roots==0 && tracked_handles.empty(),"active driver ownership and static-rest tests leaked GC roots");
    for(int failure=0;failure<8;++failure) {
        const bool no_added=failure>=4;const int fault=failure%4;
        ConstructionScope scope;CacheActiveTestFixture f;
        auto* old_copies=f.info->cache_replacements;void* old_material=ArrayValue(old_copies,0);
        const auto old_state=cache_test_materials[old_material];
        if(no_added)f.controller->cache_added=nullptr;else f.added->array={Make("rollback-effect")};
        f.Compose();
        if(fault==3)cache_test_fields[f.group][CacheTestToken(CacheTestField::MaterialMap)]=nullptr;
        std::vector<PreparedBinding> bindings;bindings.push_back(f.ActiveBinding(fault==1?1:19));
        if(fault==1)bindings[0].custom_materials=f.original_materials;
        Check(PrepareRendererMaterialCache(bindings[0]),"active rollback fixture preparation failed");
        if(fault==0)cache_test_fail_create=1;
        else if(fault==2) {if(no_added)cache_test_drop_field=CacheTestToken(CacheTestField::Submeshes);else cache_test_fail_compose=1;}
        else cache_test_fail_property=2;
        Check(CommitResource<PreparedBinding>(bindings,ApplyPreparedBinding,RestorePreparedBinding)==CommitResult::Restored,
            "active replacement failure did not restore the transaction");
        Check(f.info->cache_source==f.original_materials && f.info->cache_replacements==old_copies &&
            f.info->cache_replacing==1 && f.info->cache_submeshes==1 && f.renderer->mesh==f.original_mesh &&
            MaterialCachePrefixMatches(f.renderer,old_copies) && ArrayLength(f.renderer->materials)==(no_added?1:2),
            "active rollback lost source/replacement identity, mesh, flags, counts or effect tail");
        Check(cache_test_fields[f.group][CacheTestToken(CacheTestField::MaterialMap)]==(fault==3?nullptr:f.map) &&
            cache_test_dictionaries[f.map].values.size()==1,"active rollback leaked new dictionary keys or replaced the previous map");
        const auto& restored=cache_test_materials[old_material];
        Check(restored.floats==old_state.floats && restored.textures==old_state.textures &&
            restored.keywords==old_state.keywords && restored.passes==old_state.passes &&
            !std::memcmp(&restored.vectors.at(12),&old_state.vectors.at(12),sizeof(Float4)) &&
            !std::memcmp(&restored.vectors.at(13),&old_state.vectors.at(13),sizeof(Float4)),
            "active rollback left reused game material properties partially changed");
        cache_test_fail_create=cache_test_fail_property=cache_test_fail_compose=0;
    }
    Check(roots==0 && tracked_handles.empty(),"active failure rollback leaked GC roots");
    {
        auto* object=Make("strong-reference-move");StrongReference source;Check(source.Set(object),"move fixture root failed");
        std::vector<StrongReference> moved;moved.push_back(std::move(source));moved.reserve(32);
        Check(!source.Get() && moved[0].Get()==object && StrongHandlesTo(object)==1,
            "StrongReference move duplicated or lost the retained GC handle");
        moved.clear();Check(StrongHandlesTo(object)==0,"moved StrongReference did not release its GC handle");
    }
    Check(roots==0 && tracked_handles.empty(),"StrongReference move leaked GC roots");
    {
        ConstructionScope scope;CacheTestFixture f;f.info->cache_replacing=1;f.info->cache_replacements=Array({Make("game-owned")});
        const auto writes=cache_test_writes,receiver_writes=fake_binding_write_calls;auto b=f.Binding(19);
        Check(!PrepareRendererMaterialCache(b) && !b.material_cache,"active replacement without a valid group owner was accepted");
        Check(cache_test_writes==writes && fake_binding_write_calls==receiver_writes && f.info->cache_source==f.original_materials,"active replacement rejection mutated state");
    }
    {
        ConstructionScope scope;CacheTestFixture f;CacheTestFixture decoy;f.infos->array.insert(f.infos->array.begin(),decoy.info);
        auto b=f.Binding(19);Check(PrepareRendererMaterialCache(b) && b.material_cache && b.material_cache->info.Get()==f.info,"cache matched by name or list position instead of renderer identity");
        Check(ApplyPreparedBinding(b),"exact renderer cache commit failed");f.CheckSlots(b.custom_materials,19,0);
        Check(decoy.info->cache_source==decoy.original_materials && decoy.info->cache_submeshes==1,"cache synchronization changed another renderer");
        Check(RestorePreparedBinding(b),"exact renderer cache restore failed");
        auto* duplicate=Make("duplicate-info");duplicate->runtime_class=&cache_test_info_class;duplicate->cache_renderer=f.renderer;f.infos->array.push_back(duplicate);
        auto ambiguous=f.Binding(8);const auto writes=cache_test_writes;
        Check(!PrepareRendererMaterialCache(ambiguous) && !ambiguous.material_cache && cache_test_writes==writes,"ambiguous renderer cache was accepted");
        f.infos->array={decoy.info};auto unmatched=f.Binding(8);Check(PrepareRendererMaterialCache(unmatched) && !unmatched.material_cache,"unmanaged receiver inherited a same-name cache");
        f.infos->array={f.info};f.info->runtime_class=Make("wrong-info-class");auto wrong_class=f.Binding(8);
        Check(!PrepareRendererMaterialCache(wrong_class) && !wrong_class.material_cache,"cache list item with the wrong runtime class was accepted");
        f.renderer->cache_helper=nullptr;auto prefab=f.Binding(8);Check(PrepareRendererMaterialCache(prefab) && !prefab.material_cache,"uninitialized prefab required a live game cache");
    }
    Check(roots==0 && tracked_handles.empty(),"material cache tests leaked GC roots");
    g_material_cache_api=saved_api;g_host=saved_host;g_weak_new=saved_weak_new;g_weak_target=saved_weak_target;
    g_object_class=saved_object_class;g_game_object_class=saved_game_object_class;
    g_material_class=saved_material_class;
    g_il2cpp_array_new=saved_array_new;g_array_new_specific=saved_array_specific;
    for(size_t i=0;i<saved_methods.size();++i){g_methods[i].method_info=saved_methods[i].first;g_methods[i].resolved=saved_methods[i].second;}
    std::cout<<"PASS material cache sync: direct-object/scalar setter ABI, true-source draw/keep prepare, active 19-to-8-to-original rebase, nullable texture/vector/keyword/depth replay, transactional rollback, exact renderer identity and moved GC roots"<<std::endl;
}
