// Rebase an already-active game material group without resetting its controller.
// Group membership is disjoint in the game; properties belong to its one owner.
struct CacheVectorProperty { int32_t id=0;Float4 value{};uint8_t multiply=0; };
struct CacheTextureProperty { int32_t id=0;StrongReference value; };
struct CacheKeywordProperty { StrongReference name;uint8_t enabled=0; };
struct CacheMaterialState {
    StrongReference material;
    std::vector<std::pair<int32_t,float>> floats;
    std::vector<std::pair<int32_t,Float4>> vectors;
    std::vector<CacheTextureProperty> textures;
    std::vector<CacheKeywordProperty> keywords;
    uint8_t depth_pass=0;
};
struct RendererMaterialReplacementSnapshot {
    StrongReference controller,group,map;
    std::vector<std::pair<int32_t,float>> floats;
    std::vector<CacheVectorProperty> vectors;
    std::vector<CacheTextureProperty> textures;
    std::vector<CacheKeywordProperty> keywords;
    std::vector<StrongReference> inserted_sources;
    std::vector<CacheMaterialState> previous;
    uint8_t depth_control=0;
    int32_t ztest=0,outline_ztest=0;
    bool create_attempted=false;
};
bool ResolveRendererMaterialReplacementApi() {
    auto& api=g_material_cache_api;
    if (api.active_attempted) return api.active_ready;
    api.active_attempted=true;
    auto field=[&](const char* type,const char* name,BE_ResolvedFieldV1& result) {
        const BE_FieldDescriptorV1 descriptor{"Rendering.Beyond.dll","Beyond.Rendering",type,name,nullptr};
        const bool ok=g_host->resolve_field(g_host->context,&descriptor,&result)==BE_Result_Ok && result.field_info;
        if (!ok) Log(std::string("[MaterialCacheSync] Active rebase field unavailable: ")+type+"."+name);
        return ok;
    };
    constexpr auto owner="EntityRenderHelperMaterialController";
    constexpr auto mesh="EntityRenderHelperMaterialController/MeshMaterialController";
    constexpr auto group="EntityRenderHelperMaterialController/MeshMaterialGroupData";
    const char* methods[]{"cache.has_property","cache.get_float","cache.set_float","cache.get_vector",
        "cache.get_color","cache.set_vector","cache.keyword_enabled","cache.get_pass","cache.set_pass",
        "cache.property_id","material.get_texture_by_id","material.set_texture_by_id",
        "material.enable_keyword","material.disable_keyword"};
    for (auto key:methods) if (!Contract(key) || !Contract(key)->resolved) {
        Log(std::string("[MaterialCacheSync] Active rebase method unavailable: ")+key);return false;
    }
    api.active_ready=g_il2cpp_array_new &&
        g_host->resolve_class(g_host->context,"mscorlib.dll","System","Int32",&api.int_class)==BE_Result_Ok && api.int_class.class_info &&
        g_host->resolve_class(g_host->context,"mscorlib.dll","System","String",&api.string_class)==BE_Result_Ok && api.string_class.class_info &&
        g_host->resolve_class(g_host->context,"Rendering.Beyond.dll","Beyond.Rendering",group,&api.group_class)==BE_Result_Ok && api.group_class.class_info &&
        field(owner,"m_meshMaterialController",api.mesh_controller) && field(mesh,"m_meshMaterialDatas",api.groups) &&
        field(group,"rendererInfos",api.group_infos) && field(group,"floatProperties",api.float_properties) &&
        field(group,"vectorProperties",api.vector_properties) && field(group,"textureProperties",api.texture_properties) &&
        field(group,"keywordStates",api.keyword_states) && field(group,"depthOnlyPassControlling",api.depth_control) &&
        field(group,"src2dstMaterialDict",api.material_map);
    if (!api.active_ready) Log("[MaterialCacheSync] Active game material rebase contracts unavailable");
    return api.active_ready;
}
bool CacheCollectionCount(void* collection,int32_t& count) {
    bool ok=false;count=-1;
    return collection && Unbox(InvokeMaterialCacheList(collection,"get_Count",nullptr,0,ok),count) && ok && count>=0 && count<=4096;
}
template<class F> bool CacheDictionaryEach(void* dictionary,bool strings,F read) {
    if (!dictionary) return true;
    int32_t count=0;if (!CacheCollectionCount(dictionary,count)) return false;
    if (!count) return true;
    bool ok=false;void* keys=InvokeMaterialCacheList(dictionary,"get_Keys",nullptr,0,ok);
    if (!ok || !keys) return false;
    auto& api=g_material_cache_api;
    void* array=RootTemporary(g_il2cpp_array_new(const_cast<void*>(strings?api.string_class.class_info:api.int_class.class_info),static_cast<uintptr_t>(count)));
    int32_t zero=0;void* copy_args[]{array,&zero};
    if (!array || ArrayLength(array)!=count) return false;
    InvokeMaterialCacheList(keys,"CopyTo",copy_args,2,ok);if (!ok) return false;
    for (int32_t i=0;i<count;++i) {
        void* key=ArrayValue(array,i);int32_t id=0;
        if (!key || (!strings && !Unbox(key,id))) return false;
        void* args[]{strings?key:static_cast<void*>(&id)};
        void* value=InvokeMaterialCacheList(dictionary,"get_Item",args,1,ok);
        if (!ok || !read(key,value)) return false;
    }
    return true;
}
bool CacheMapContains(void* map,void* source,bool& contains) {
    contains=false;if (!map) return true;
    bool ok=false;uint8_t present=0;void* args[]{source};
    if (!Unbox(InvokeMaterialCacheList(map,"ContainsKey",args,1,ok),present) || !ok) return false;
    contains=present!=0;return true;
}
bool PrepareRendererMaterialReplacement(void* owner,RendererMaterialCacheSnapshot& snapshot) {
    if (!ResolveRendererMaterialReplacementApi()) return false;
    auto& api=g_material_cache_api;auto result=std::make_shared<RendererMaterialReplacementSnapshot>();
    void* controller=ReadMaterialCacheField(owner,api.mesh_controller);
    void* groups=ReadMaterialCacheField(controller,api.groups);int32_t count=0;
    if (!controller || !CacheCollectionCount(groups,count)) {Log("[MaterialCacheSync] Active rebase group list unavailable");return false;}
    void* matched=nullptr;bool ok=false;
    for (int32_t i=0;i<count;++i) {
        void* args[]{&i};void* group=InvokeMaterialCacheList(groups,"get_Item",args,1,ok);
        if (!ok || !group || g_object_class(group)!=api.group_class.class_info) return false;
        void* infos=ReadMaterialCacheField(group,api.group_infos);int32_t members=0;
        if (!CacheCollectionCount(infos,members)) return false;
        for (int32_t j=0;j<members;++j) {
            void* member_args[]{&j};void* info=InvokeMaterialCacheList(infos,"get_Item",member_args,1,ok);
            if (!ok) return false;
            if (info==snapshot.info.Get()) {
                if (matched && matched!=group) {Log("[MaterialCacheSync] Active rebase renderer belongs to multiple groups");return false;}
                matched=group;
            }
        }
    }
    if (!matched) {Log("[MaterialCacheSync] Active rebase renderer group not found");return false;}
    if (!result->controller.Set(controller) || !result->group.Set(matched) ||
        !result->map.Set(ReadMaterialCacheField(matched,api.material_map)) ||
        !ReadMaterialCacheValue(matched,api.depth_control,result->depth_control)) return false;
    if (!CacheDictionaryEach(ReadMaterialCacheField(matched,api.float_properties),false,[&](void* key,void* value) {
        int32_t id=0;float number=0;if (!Unbox(key,id) || !Unbox(value,number)) return false;
        result->floats.emplace_back(id,number);return true;
    }) || !CacheDictionaryEach(ReadMaterialCacheField(matched,api.vector_properties),false,[&](void* key,void* value) {
        CacheVectorProperty property;if (!Unbox(key,property.id) || !value || !g_host->object_unbox) return false;
        const auto* payload=static_cast<const uint8_t*>(g_host->object_unbox(g_host->context,value));
        if (!payload) return false;
        std::memcpy(&property.value,payload,sizeof(Float4));property.multiply=payload[16];
        result->vectors.push_back(property);return true;
    }) || !CacheDictionaryEach(ReadMaterialCacheField(matched,api.texture_properties),false,[&](void* key,void* value) {
        CacheTextureProperty property;if (!Unbox(key,property.id) || !property.value.Set(value)) return false;
        result->textures.push_back(std::move(property));return true;
    }) || !CacheDictionaryEach(ReadMaterialCacheField(matched,api.keyword_states),true,[&](void* key,void* value) {
        CacheKeywordProperty property;if (!property.name.Set(key) || !Unbox(value,property.enabled)) return false;
        result->keywords.push_back(std::move(property));return true;
    })) return false;
    if (result->depth_control) {
        void* z_args[]{NewString("_ZTestGBuffer")};void* outline_args[]{NewString("_OutlineZTest")};
        if (!InvokeValue(Contract("cache.property_id"),nullptr,z_args,result->ztest) ||
            !InvokeValue(Contract("cache.property_id"),nullptr,outline_args,result->outline_ztest)) return false;
    }
    snapshot.replacement=std::move(result);return true;
}
bool CacheSetFloat(void* material,int32_t id,float value) {
    void* args[]{&id,&value};return InvokeVoid(Contract("cache.set_float"),material,args);
}
bool CacheSetVector(void* material,int32_t id,Float4 value) {
    void* args[]{&id,&value};return InvokeVoid(Contract("cache.set_vector"),material,args);
}
bool CacheHasProperty(void* material,int32_t id,bool& has) {
    uint8_t value=0;void* args[]{&id};if (!InvokeValue(Contract("cache.has_property"),material,args,value)) return false;
    has=value!=0;return true;
}
bool CaptureCacheMaterialState(void* material,const RendererMaterialReplacementSnapshot& properties,CacheMaterialState& state) {
    if (!state.material.Set(material)) return false;
    std::vector<int32_t> float_ids;
    for (const auto& property:properties.floats) float_ids.push_back(property.first);
    if (properties.depth_control) {float_ids.push_back(properties.ztest);float_ids.push_back(properties.outline_ztest);}
    std::sort(float_ids.begin(),float_ids.end());float_ids.erase(std::unique(float_ids.begin(),float_ids.end()),float_ids.end());
    for (auto id:float_ids) {
        float value=0;void* args[]{&id};if (!InvokeValue(Contract("cache.get_float"),material,args,value)) return false;
        state.floats.emplace_back(id,value);
    }
    for (const auto& property:properties.vectors) {
        auto id=property.id;Float4 value{};void* args[]{&id};
        if (!InvokeValue(Contract("cache.get_vector"),material,args,value)) return false;
        state.vectors.emplace_back(id,value);
    }
    for (const auto& property:properties.textures) {
        CacheTextureProperty saved;saved.id=property.id;void* args[]{&saved.id};void* value=nullptr;
        if (!ReadNullableObject(Contract("material.get_texture_by_id"),material,value,args) || !saved.value.Set(value)) return false;
        state.textures.push_back(std::move(saved));
    }
    for (const auto& property:properties.keywords) {
        CacheKeywordProperty saved;void* args[]{property.name.Get()};
        if (!saved.name.Set(args[0]) || !InvokeValue(Contract("cache.keyword_enabled"),material,args,saved.enabled)) return false;
        state.keywords.push_back(std::move(saved));
    }
    if (properties.depth_control) {
        void* args[]{NewString("DepthOnly")};if (!InvokeValue(Contract("cache.get_pass"),material,args,state.depth_pass)) return false;
    }
    return true;
}
bool ReplayCacheMaterialProperties(void* material,void* source,const RendererMaterialReplacementSnapshot& properties) {
    for (const auto& property:properties.floats) if (!CacheSetFloat(material,property.first,property.second)) return false;
    for (const auto& property:properties.vectors) {
        auto id=property.id;auto value=property.value;
        if (property.multiply) {
            bool has=false;if (!CacheHasProperty(source,id,has)) return false;if (!has) continue;
            Float4 original{};void* args[]{&id};if (!InvokeValue(Contract("cache.get_color"),source,args,original)) return false;
            value={value.x*original.x,value.y*original.y,value.z*original.z,value.w*original.w};
        }
        if (!CacheSetVector(material,id,value)) return false;
    }
    for (const auto& property:properties.textures) {
        auto id=property.id;void* args[]{&id,property.value.Get()};
        if (!InvokeVoid(Contract("material.set_texture_by_id"),material,args)) return false;
    }
    for (const auto& property:properties.keywords) {
        void* args[]{property.name.Get()};if (!InvokeVoid(Contract(property.enabled?"material.enable_keyword":"material.disable_keyword"),material,args)) return false;
    }
    if (properties.depth_control) {
        bool z=false,outline=false;
        if (!CacheHasProperty(material,properties.ztest,z) || !CacheHasProperty(material,properties.outline_ztest,outline)) return false;
        uint8_t alpha=0;void* keyword[]{NewString("_ALPHABLEND_ON")};
        if (outline && !InvokeValue(Contract("cache.keyword_enabled"),material,keyword,alpha)) return false;
        if (z || (outline && !alpha)) {
            uint8_t enabled=1;void* args[]{NewString("DepthOnly"),&enabled};
            if (!InvokeVoid(Contract("cache.set_pass"),material,args)) return false;
        }
        if ((z && !CacheSetFloat(material,properties.ztest,3.f)) ||
            (outline && !alpha && !CacheSetFloat(material,properties.outline_ztest,3.f))) return false;
    }
    return true;
}
bool ApplyRendererMaterialReplacement(const PreparedBinding& binding,RendererMaterialCacheSnapshot& snapshot) {
    if (!snapshot.replacement) return !snapshot.replacing;
    auto& properties=*snapshot.replacement;auto& api=g_material_cache_api;
    void* map=properties.map.Get();std::unordered_set<void*> sources;
    for (int i=0;i<ArrayLength(binding.custom_materials);++i) {
        void* source=ArrayValue(binding.custom_materials,i);bool contains=false;
        if (!source || !sources.insert(source).second) continue;
        if (!CacheMapContains(map,source,contains)) return false;
        if (!contains) {StrongReference key;if (!key.Set(source)) return false;properties.inserted_sources.push_back(std::move(key));}
    }
    properties.create_attempted=true;bool ok=false;void* args[]{snapshot.info.Get(),properties.group.Get()};
    InvokeMaterialCacheList(properties.controller.Get(),"_CreateReplaceMaterial",args,2,ok);
    if (!ok) return false;
    void* copies=ReadMaterialCacheField(snapshot.info.Get(),api.replacements);
    if (!copies || ArrayLength(copies)!=ArrayLength(binding.custom_materials)) return false;
    std::unordered_set<void*> seen;
    for (int i=0;i<ArrayLength(copies);++i) {
        void* copy=ArrayValue(copies,i);if (!copy) return false;
        if (seen.insert(copy).second) {
            CacheMaterialState state;if (!CaptureCacheMaterialState(copy,properties,state)) return false;
            properties.previous.push_back(std::move(state));
        }
    }
    seen.clear();
    for (int i=0;i<ArrayLength(copies);++i) {
        void* copy=ArrayValue(copies,i);
        if (seen.insert(copy).second && !ReplayCacheMaterialProperties(copy,ArrayValue(binding.custom_materials,i),properties)) return false;
    }
    return true;
}
bool RestoreRendererMaterialReplacement(RendererMaterialCacheSnapshot& snapshot) {
    if (!snapshot.replacement || !snapshot.replacement->create_attempted) return true;
    auto& properties=*snapshot.replacement;auto& api=g_material_cache_api;bool restored=true;
    auto remember=[&](bool ok){restored=ok && restored;};
    for (const auto& state:properties.previous) {
        void* material=state.material.Get();
        for (const auto& property:state.floats) remember(CacheSetFloat(material,property.first,property.second));
        for (const auto& property:state.vectors) remember(CacheSetVector(material,property.first,property.second));
        for (const auto& property:state.textures) {auto id=property.id;void* args[]{&id,property.value.Get()};remember(InvokeVoid(Contract("material.set_texture_by_id"),material,args));}
        for (const auto& property:state.keywords) {void* args[]{property.name.Get()};remember(InvokeVoid(Contract(property.enabled?"material.enable_keyword":"material.disable_keyword"),material,args));}
        if (properties.depth_control) {uint8_t enabled=state.depth_pass;void* args[]{NewString("DepthOnly"),&enabled};remember(InvokeVoid(Contract("cache.set_pass"),material,args));}
    }
    remember(WriteMaterialCacheObject(snapshot.info.Get(),api.replacements,snapshot.replacements.Get()));
    remember(WriteMaterialCacheBool(snapshot.info.Get(),api.replacing,snapshot.replacing));
    void* current_map=ReadMaterialCacheField(properties.group.Get(),api.material_map);
    if (!properties.map.Get()) remember(WriteMaterialCacheObject(properties.group.Get(),api.material_map,nullptr));
    else if (current_map!=properties.map.Get()) remember(false);
    else for (const auto& source:properties.inserted_sources) {
        bool contains=false;
        if (!CacheMapContains(current_map,source.Get(),contains)) {remember(false);continue;}
        if (contains) {bool ok=false;uint8_t removed=0;void* args[]{source.Get()};remember(Unbox(InvokeMaterialCacheList(current_map,"Remove",args,1,ok),removed) && ok && removed);}
    }
    return restored;
}
