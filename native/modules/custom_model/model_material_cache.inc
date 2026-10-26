// Keep EntityRenderHelper's base materials in the same transaction as its
// renderer. The game composes additive VFX from this cache on show/hide.
// All fields and methods are resolved by metadata on each platform, not RVAs.
struct RendererMaterialCacheApi {
    using FieldSet=void(*)(void*,const void*,void*);
    using ClassMethod=const void*(*)(void*,const char*,int);
    bool attempted=false,required=false,ready=false;
    bool active_attempted=false,active_ready=false;
    BE_ResolvedClassV1 helper{},info_class{};
    BE_ResolvedClassV1 int_class{},string_class{},group_class{};
    BE_ResolvedFieldV1 helper_controller{},infos{},added{},renderer{},source{},submeshes{},replacing{},replacements{};
    BE_ResolvedFieldV1 mesh_controller{},groups{},group_infos{},float_properties{},vector_properties{},texture_properties{},keyword_states{},depth_control{},material_map{};
    FieldSet set=nullptr,set_object=nullptr;
    ClassMethod method=nullptr;
};
RendererMaterialCacheApi g_material_cache_api;

bool ResolveRendererMaterialCacheApi() {
    auto& api=g_material_cache_api;
    if (api.attempted) return api.ready;
    api.attempted=true;
    if (!g_host || !g_host->resolve_class ||
        g_host->resolve_class(g_host->context,"Gameplay.Beyond.dll","Beyond.Gameplay.View",
            "EntityRenderHelper",&api.helper)!=BE_Result_Ok || !api.helper.type_object) return false;
    api.required=true;
    HMODULE game=GetModuleHandleW(L"GameAssembly.dll");
    api.set=reinterpret_cast<RendererMaterialCacheApi::FieldSet>(GetProcAddress(game,"il2cpp_field_set_value"));
    api.set_object=reinterpret_cast<RendererMaterialCacheApi::FieldSet>(GetProcAddress(game,"il2cpp_field_set_value_object"));
    api.method=reinterpret_cast<RendererMaterialCacheApi::ClassMethod>(GetProcAddress(game,"il2cpp_class_get_method_from_name"));
    const auto* helper=Contract("material_cache.helper");
    const auto* apply=Contract("material_cache.apply");
    BE_ResolvedClassV1 info_class{};
    const bool info_ready=g_host->resolve_class(g_host->context,"Rendering.Beyond.dll","Beyond.Rendering",
        "EntityRenderHelperMaterialController/RendererInfo",&info_class)==BE_Result_Ok && info_class.class_info;
    api.info_class=info_class;
    if (!api.set || !api.set_object || !api.method || !g_object_class || !g_host->resolve_field || !g_host->field_get_value_object ||
        !helper || !helper->resolved || !apply || !apply->resolved ||
        !info_ready) {
        Log("[MaterialCacheSync] Required cache synchronization contracts unavailable; binding refused"
            " scalarSetter="+std::to_string(api.set!=nullptr)+" objectSetter="+std::to_string(api.set_object!=nullptr)+
            " classMethod="+std::to_string(api.method!=nullptr)+" objectClass="+std::to_string(g_object_class!=nullptr)+
            " fieldResolver="+std::to_string(g_host->resolve_field!=nullptr)+" fieldReader="+std::to_string(g_host->field_get_value_object!=nullptr)+
            " helperMethod="+std::to_string(helper && helper->resolved)+" applyMethod="+std::to_string(apply && apply->resolved)+
            " infoClass="+std::to_string(info_ready));return false;
    }
    auto field=[&](const char* assembly,const char* space,const char* type,const char* name,
            const char* value_type,BE_ResolvedFieldV1& result) {
        const BE_FieldDescriptorV1 descriptor{assembly,space,type,name,value_type};
        const bool ok=g_host->resolve_field(g_host->context,&descriptor,&result)==BE_Result_Ok && result.field_info;
        if (!ok) Log(std::string("[MaterialCacheSync] Field unavailable: ")+type+"."+name);
        return ok;
    };
    constexpr auto rendering="Rendering.Beyond.dll";
    constexpr auto space="Beyond.Rendering";
    constexpr auto controller="EntityRenderHelperMaterialController";
    constexpr auto info="EntityRenderHelperMaterialController/RendererInfo";
    api.ready=field("Gameplay.Beyond.dll","Beyond.Gameplay.View","EntityRenderHelper",
        "m_rendererAndMaterialController","Beyond.Rendering.EntityRenderHelperMaterialController",api.helper_controller) &&
        field(rendering,space,controller,"m_rendererInfos",nullptr,api.infos) &&
        field(rendering,space,controller,"m_addedMaterialController",nullptr,api.added) &&
        field(rendering,space,info,"m_renderer","UnityEngine.Renderer",api.renderer) &&
        field(rendering,space,info,"sourceMaterials","UnityEngine.Material[]",api.source) &&
        field(rendering,space,info,"subMeshCount","System.Int32",api.submeshes) &&
        field(rendering,space,info,"materialReplacing","System.Boolean",api.replacing) &&
        field(rendering,space,info,"replacingMaterials","UnityEngine.Material[]",api.replacements);
    if (api.ready) Log("[MaterialCacheSync] Base-material cache synchronization ready (metadata contracts)");
    return api.ready;
}
void* ReadMaterialCacheField(void* object,const BE_ResolvedFieldV1& field) {
    return object && field.field_info && g_host && g_host->field_get_value_object?
        RootTemporary(g_host->field_get_value_object(g_host->context,field.field_info,object)):nullptr;
}
template<class T> bool ReadMaterialCacheValue(void* object,const BE_ResolvedFieldV1& field,T& value) {
    return Unbox(ReadMaterialCacheField(object,field),value);
}
bool WriteMaterialCacheInt(void* object,const BE_ResolvedFieldV1& field,int32_t value) {
    if (!object || !field.field_info || !g_material_cache_api.set) return false;
    // Value-type fields receive a pointer to the scalar storage.
    __try { g_material_cache_api.set(object,field.field_info,&value);return true; }
    __except(EXCEPTION_EXECUTE_HANDLER) { return false; }
}
bool WriteMaterialCacheObject(void* object,const BE_ResolvedFieldV1& field,void* value) {
    if (!object || !field.field_info || !g_material_cache_api.set_object) return false;
    // Reference fields receive the managed object itself, never &value.
    // The dedicated object export also applies the GC write barrier.
    __try { g_material_cache_api.set_object(object,field.field_info,value);return true; }
    __except(EXCEPTION_EXECUTE_HANDLER) { return false; }
}
bool WriteMaterialCacheBool(void* object,const BE_ResolvedFieldV1& field,uint8_t value) {
    if (!object || !field.field_info || !g_material_cache_api.set) return false;
    __try { g_material_cache_api.set(object,field.field_info,&value);return true; }
    __except(EXCEPTION_EXECUTE_HANDLER) { return false; }
}
void* InvokeMaterialCacheList(void* list,const char* name,void** parameters,int count,bool& ok) {
    ok=false;
    if (!list) return nullptr;
    const auto* method=g_material_cache_api.method(g_object_class(list),name,count);
    if (!method) return nullptr;
    void* exception=nullptr;
    void* result=g_host->runtime_invoke(g_host->context,method,list,parameters,&exception);
    ok=exception==nullptr;
    return ok?RootTemporary(result):nullptr;
}
struct RendererMaterialReplacementSnapshot;
struct RendererMaterialCacheSnapshot {
    StrongReference info,added,source,replacements;
    std::vector<int32_t> source_ids;
    int32_t submeshes=0;
    uint8_t replacing=0;
    bool touched=false;
    std::shared_ptr<RendererMaterialReplacementSnapshot> replacement;
};
bool PrepareRendererMaterialReplacement(void* controller,RendererMaterialCacheSnapshot& snapshot);
bool MaterialCacheSourceIds(void* materials,std::vector<int32_t>& ids) {
    ids.clear();
    if (!materials) return true;
    int dimension=0,count=-1;void* args[]{&dimension};
    if (!InvokeValue(Contract("array.get_length"),materials,args,count) || count<0 || count>4096) return false;
    ids.reserve(static_cast<size_t>(count));
    for (int i=0;i<count;++i) {
        void* material=ArrayValue(materials,i);int32_t id=0;
        if (material && (!IsNativeObjectAlive(material) ||
            !InvokeValue(Contract("object.instance_id"),material,nullptr,id) || !id)) return false;
        ids.push_back(id);
    }
    return true;
}
bool PrepareRendererMaterialCache(const PreparedBinding& binding,bool inspect_only) {
    if (binding.material_cache) return true;
    auto& api=g_material_cache_api;
    if (!api.helper.type_object && (!g_host || !g_host->resolve_class ||
        g_host->resolve_class(g_host->context,"Gameplay.Beyond.dll","Beyond.Gameplay.View",
            "EntityRenderHelper",&api.helper)!=BE_Result_Ok || !api.helper.type_object)) return true;
    const auto* get_helper=Contract("material_cache.helper");
    if (!get_helper || !get_helper->resolved) return false;
    bool inactive=true;void* parameters[]{api.helper.type_object,&inactive};
    void* exception=nullptr;
    void* helper=RootTemporary(g_host->runtime_invoke(g_host->context,get_helper->method_info,
        binding.renderer,parameters,&exception));
    if (exception) return false;
    // Prefabs are normally delivered before a live helper is initialized. Its
    // subsequent _Init observes the already-modified renderer naturally.
    if (!helper) return true;
    if (!ResolveRendererMaterialCacheApi()) return false;
    void* controller=ReadMaterialCacheField(helper,api.helper_controller);
    void* list=ReadMaterialCacheField(controller,api.infos);
    if (!controller || !list) return true;
    bool ok=false;int32_t count=-1;
    if (!Unbox(InvokeMaterialCacheList(list,"get_Count",nullptr,0,ok),count) || !ok || count<0 || count>4096) return false;
    void* matched=nullptr;
    int32_t target_id=0;
    if (!InvokeValue(Contract("object.instance_id"),binding.renderer,nullptr,target_id) || !target_id) return false;
    for (int32_t i=0;i<count;++i) {
        void* args[]{&i};void* info=InvokeMaterialCacheList(list,"get_Item",args,1,ok);
        if (!ok || !info || g_object_class(info)!=api.info_class.class_info) return false;
        void* renderer=ReadMaterialCacheField(info,api.renderer);int32_t id=0;
        if (!renderer || !IsNativeObjectAlive(renderer) ||
            !InvokeValue(Contract("object.instance_id"),renderer,nullptr,id) || id!=target_id) continue;
        if (matched && matched!=info) {Log("[MaterialCacheSync] Ambiguous renderer cache; binding refused");return false;}
        matched=info;
    }
    if (!matched) return true; // This helper does not manage the receiver.
    auto snapshot=std::make_shared<RendererMaterialCacheSnapshot>();
    if (!ReadMaterialCacheValue(matched,api.replacing,snapshot->replacing) ||
        !ReadMaterialCacheValue(matched,api.submeshes,snapshot->submeshes) || snapshot->submeshes<0 || snapshot->submeshes>4096) return false;
    if (!snapshot->info.Set(matched) ||
        !snapshot->added.Set(ReadMaterialCacheField(controller,api.added)) ||
        !snapshot->source.Set(ReadMaterialCacheField(matched,api.source)) ||
        !snapshot->replacements.Set(ReadMaterialCacheField(matched,api.replacements))) return false;
    if (!MaterialCacheSourceIds(snapshot->source.Get(),snapshot->source_ids)) return false;
    if (snapshot->replacing && !inspect_only && !PrepareRendererMaterialReplacement(controller,*snapshot)) return false;
    binding.material_cache=std::move(snapshot);
    return true;
}
bool ResolveMaterialCopyDonor(const PreparedBinding& binding,void*& donor) {
    donor=DonorMaterials(binding);
    if (binding.saved_original) return donor!=nullptr;
    // Read just the base for a keep-material copy. Do not retain this early
    // snapshot across the job's frame slices; commit captures a fresh one.
    PreparedBinding probe;probe.renderer=binding.renderer;
    if (!PrepareRendererMaterialCache(probe,true)) return false;
    if (probe.material_cache) donor=RootTemporary(probe.material_cache->source.Get());
    return donor!=nullptr;
}
bool RecomposeRendererMaterialCache(const RendererMaterialCacheSnapshot& snapshot) {
    void* added=snapshot.added.Get();
    if (!added) {
        auto& api=g_material_cache_api;void* info=snapshot.info.Get();uint8_t replacing=0;
        if (!ReadMaterialCacheValue(info,api.replacing,replacing)) return false;
        void* renderer=ReadMaterialCacheField(info,api.renderer);
        void* materials=ReadMaterialCacheField(info,replacing?api.replacements:api.source);
        return renderer && materials && ApplyRendererMaterials(renderer,materials);
    }
    void* args[]{snapshot.info.Get()};
    return args[0] && InvokeVoid(Contract("material_cache.apply"),added,args);
}
bool MaterialCachePrefixMatches(void* renderer,void* base) {
    void* actual=Invoke(Contract("renderer.get_shared_materials"),renderer,nullptr);
    const int count=ArrayLength(base),actual_count=ArrayLength(actual);
    if (count<0 || actual_count<count || actual_count>65536) return false;
    for (int i=0;i<count;++i) if (ArrayValue(base,i)!=ArrayValue(actual,i)) return false;
    return true;
}
#include "model_material_replacement.inc"
void* RendererMaterialIdentityBase(void* renderer,void* actual) {
    PreparedBinding probe;probe.renderer=renderer;
    if (!PrepareRendererMaterialCache(probe,true) || !probe.material_cache) return actual;
    const auto& cache=*probe.material_cache;
    void* expected=cache.replacing?cache.replacements.Get():cache.source.Get();
    if (!expected || !MaterialCachePrefixMatches(renderer,expected)) return actual;
    return RootTemporary(cache.source.Get());
}
bool ApplyRendererMaterialCache(const PreparedBinding& binding) {
    if (!binding.material_cache) return true;
    auto& snapshot=*binding.material_cache;auto& api=g_material_cache_api;
    void* info=snapshot.info.Get();int32_t count=-1,current=-1;uint8_t replacing=0;
    int32_t target_id=0,cached_id=0;std::vector<int32_t> current_ids;
    // Capture immediately before commit, and refuse any changed cache state.
    if (!info || ReadMaterialCacheField(info,api.source)!=snapshot.source.Get() ||
        !ReadMaterialCacheValue(info,api.submeshes,current) || current!=snapshot.submeshes ||
        !ReadMaterialCacheValue(info,api.replacing,replacing) || replacing!=snapshot.replacing ||
        ReadMaterialCacheField(info,api.replacements)!=snapshot.replacements.Get() ||
        !MaterialCacheSourceIds(snapshot.source.Get(),current_ids) || current_ids!=snapshot.source_ids ||
        (binding.copied_donor_materials && !binding.saved_original &&
            ArrayLength(binding.custom_materials)!=static_cast<int>(snapshot.source_ids.size())) ||
        !InvokeValue(Contract("object.instance_id"),binding.renderer,nullptr,target_id) || !target_id ||
        !InvokeValue(Contract("object.instance_id"),ReadMaterialCacheField(info,api.renderer),nullptr,cached_id) ||
        cached_id!=target_id ||
        !InvokeValue(Contract("mesh.get_sub_mesh_count"),binding.custom_mesh,nullptr,count) || count<0 || count>4096) {
        Log("[MaterialCacheSync] Cache state changed or invalid before commit; binding refused renderer="+
            DescribeBindingObject(binding.renderer));return false;
    }
    snapshot.touched=true;
    void* materials=binding.custom_materials;
    if (!WriteMaterialCacheObject(info,api.source,materials) ||
        ReadMaterialCacheField(info,api.source)!=materials ||
        !WriteMaterialCacheInt(info,api.submeshes,count) ||
        !ReadMaterialCacheValue(info,api.submeshes,current) || current!=count ||
        !ApplyRendererMaterialReplacement(binding,snapshot) ||
        !RecomposeRendererMaterialCache(snapshot) ||
        !MaterialCachePrefixMatches(binding.renderer,snapshot.replacing?ReadMaterialCacheField(info,api.replacements):materials)) {
        Log("[MaterialCacheSync] Cache commit verification failed; transaction rollback required renderer="+
            DescribeBindingObject(binding.renderer));return false;
    }
    Log("[MaterialCacheSync] applied renderer="+DescribeBindingObject(binding.renderer)+
        " baseSlots="+std::to_string(ArrayLength(snapshot.source.Get()))+"->"+std::to_string(ArrayLength(materials))+
        " submeshes="+std::to_string(snapshot.submeshes)+"->"+std::to_string(count)+
        " additiveVfx="+std::to_string(snapshot.added.Get()!=nullptr)+
        " gameCopies="+std::to_string(snapshot.replacing!=0));
    return true;
}
bool RestoreRendererMaterialCache(const PreparedBinding& binding) {
    if (!binding.material_cache || !binding.material_cache->touched) return true;
    auto& snapshot=*binding.material_cache;auto& api=g_material_cache_api;
    void* info=snapshot.info.Get();void* source=snapshot.source.Get();int32_t count=-1;
    // Attempt every independent field restoration even if an earlier write fails.
    const bool source_ok=WriteMaterialCacheObject(info,api.source,source) && ReadMaterialCacheField(info,api.source)==source;
    const bool count_ok=WriteMaterialCacheInt(info,api.submeshes,snapshot.submeshes) &&
        ReadMaterialCacheValue(info,api.submeshes,count) && count==snapshot.submeshes;
    const bool replacement_ok=RestoreRendererMaterialReplacement(snapshot);
    const bool compose_ok=RecomposeRendererMaterialCache(snapshot);
    if (source_ok && count_ok && replacement_ok && compose_ok) snapshot.touched=false;
    return source_ok && count_ok && replacement_ok && compose_ok;
}
