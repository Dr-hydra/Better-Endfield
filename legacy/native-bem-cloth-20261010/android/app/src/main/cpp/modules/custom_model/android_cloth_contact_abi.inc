// Read-only evidence for the Android contact scheduler. No hooks or writes.
struct ContactMetadata {
    void* library=OpenLoadedIl2Cpp();
    template<class T>T Export(const char* name) {return reinterpret_cast<T>(library?dlsym(library,name):nullptr);}
    using Class=Il2CppClass;
    int32_t (*size)(Class*,uint32_t*)=Export<decltype(size)>("il2cpp_class_value_size");
    bool (*value)(Class*)=Export<decltype(value)>("il2cpp_class_is_valuetype");
    const FieldInfo* (*fields)(Class*,void**)=Export<decltype(fields)>("il2cpp_class_get_fields");
    const char* (*name)(const FieldInfo*)=Export<decltype(name)>("il2cpp_field_get_name");
    size_t (*offset)(const FieldInfo*)=Export<decltype(offset)>("il2cpp_field_get_offset");
    int (*flags)(const FieldInfo*)=Export<decltype(flags)>("il2cpp_field_get_flags");
    const Il2CppType* (*type)(const FieldInfo*)=Export<decltype(type)>("il2cpp_field_get_type");
    char* (*type_name)(const Il2CppType*)=Export<decltype(type_name)>("il2cpp_type_get_name");
    void (*free_name)(void*)=Export<decltype(free_name)>("il2cpp_free");
    Class* (*from_type)(const Il2CppType*)=Export<decltype(from_type)>("il2cpp_class_from_type");
    ~ContactMetadata(){if(library)dlclose(library);}
    Json Layout(Class* cls,int depth=0) {
        if(!from_type)from_type=Export<decltype(from_type)>("il2cpp_class_from_il2cpp_type");
        if(!cls||!size||!value||!fields||!name||!offset||!flags||!type||!type_name||!free_name||!from_type)
            return {{"available",false}};
        Json out={{"available",true},{"value_type",value(cls)},{"fields",Json::array()}};
        if(value(cls)){uint32_t alignment=0;out["size"]=size(cls,&alignment);out["alignment"]=alignment;}
        void* iterator=nullptr;unsigned count=0;
        while(const auto f=fields(cls,&iterator)) {
            if(++count>64){out["truncated"]=true;break;}
            if(flags(f)&0x10)continue;
            const auto t=type(f);char* raw=type_name(t);std::string type_text=raw?raw:"";if(raw)free_name(raw);
            Json item={{"name",name(f)},{"type",type_text},{"boxed_offset",offset(f)}};
            if(depth<2&&type_text.starts_with("Unity.Collections.")&&!type_text.ends_with("*"))
                item["layout"]=Layout(from_type(t),depth+1);
            out["fields"].push_back(std::move(item));
        }
        return out;
    }
};
Json ContactCodeSnapshot(ResolvedMethod method,size_t wanted=768) {
    if(!method.entry)return {{"resolved",false}};
    const uintptr_t begin=reinterpret_cast<uintptr_t>(method.entry);
    FILE* maps=std::fopen("/proc/self/maps","r");if(!maps)return {{"resolved",true},{"readable",false}};
    char line[1024]{};size_t count=0;
    while(std::fgets(line,sizeof(line),maps)) {
        uintptr_t lo=0,hi=0;char permission[5]{};
        if(std::sscanf(line,"%lx-%lx %4s",&lo,&hi,permission)==3&&lo<=begin&&begin<hi&&
                permission[0]=='r'&&permission[2]=='x'){count=std::min(wanted,size_t(hi-begin));break;}
    }
    std::fclose(maps);Dl_info info{};
    if(!count||!dladdr(method.entry,&info)||!info.dli_fbase)return {{"resolved",true},{"readable",false}};
    static constexpr char hex[]="0123456789abcdef";std::string bytes;bytes.reserve(count*2);
    const auto data=reinterpret_cast<const uint8_t*>(method.entry);
    for(size_t i=0;i<count;++i){bytes+=hex[data[i]>>4];bytes+=hex[data[i]&15];}
    return {{"resolved",true},{"module",info.dli_fname?info.dli_fname:""},
        {"module_offset",begin-reinterpret_cast<uintptr_t>(info.dli_fbase)},{"code_hex",bytes}};
}
ResolvedMethod ContactScheduleSpecialization(Scope& scope,const char* job_type,const char* requested_name) {
    const bool terminal=std::string_view(requested_name)=="ScheduleManagedCrossFrameJob";
    ContactMetadata api;
    auto methods=api.Export<const MethodInfo*(*)(Il2CppClass*,void**)>("il2cpp_class_get_methods");
    auto method_name=api.Export<const char*(*)(const MethodInfo*)>("il2cpp_method_get_name");
    auto count=api.Export<uint32_t(*)(const MethodInfo*)>("il2cpp_method_get_param_count");
    auto param=api.Export<const Il2CppType*(*)(const MethodInfo*,uint32_t)>("il2cpp_method_get_param");
    auto return_type=api.Export<const Il2CppType*(*)(const MethodInfo*)>("il2cpp_method_get_return_type");
    auto method_flags=api.Export<uint32_t(*)(const MethodInfo*,uint32_t*)>("il2cpp_method_get_flags");
    auto object=api.Export<void*(*)(const MethodInfo*,Il2CppClass*)>("il2cpp_method_get_object");
    auto from_reflection=api.Export<const MethodInfo*(*)(void*)>("il2cpp_method_get_from_reflection");
    if(!methods||!method_name||!count||!param||!return_type||!method_flags||!object||!from_reflection||!api.type_name||!api.free_name)
        throw std::runtime_error("generic method metadata exports unavailable");
    auto cls=terminal?runtime->ResolveClass(kUnity,"Unity.Jobs","IJobExtensions"):
        runtime->ResolveClass(kCloth,"BeyondDynamicBone","CrossFrameJobUtils");
    const MethodInfo* definition=nullptr;void* iterator=nullptr;
    if(!cls.info)throw std::runtime_error("CrossFrameJobUtils unavailable");
    while(auto method=methods(cls.info,&iterator)) {
        if(std::strcmp(method_name(method),requested_name)||count(method)!=(terminal?3u:2u))continue;
        uint32_t implementation_flags=0;if(!(method_flags(method,&implementation_flags)&0x10))continue;
        char* result=api.type_name(return_type(method));const bool returns_handle=result&&std::string_view(result)=="Unity.Jobs.JobHandle";
        if(result)api.free_name(result);if(!returns_handle)continue;
        char* p=api.type_name(param(method,terminal?2:1));const bool match=p&&std::string_view(p)=="Unity.Jobs.JobHandle";
        if(p)api.free_name(p);if(!match)continue;
        p=api.type_name(param(method,0));const bool byref=p&&std::string_view(p).ends_with("&");
        if(p)api.free_name(p);if(byref==terminal)continue;
        if(terminal) {
            p=api.type_name(param(method,1));const bool priority=p&&std::string_view(p)=="Unity.Jobs.LowLevel.Unsafe.JobQueuePriority";
            if(p)api.free_name(p);if(!priority)continue;
        }
        if(definition)throw std::runtime_error("generic Schedule definition ambiguous");definition=method;
    }
    if(!definition)throw std::runtime_error("generic Schedule definition unavailable");
    auto reflected=scope.Root(object(definition,cls.info));
    auto job=runtime->ResolveClass(kCloth,"BeyondDynamicBone",job_type);
    auto type=runtime->ResolveClass("mscorlib.dll","System","Type");
    if(!reflected||!job.type_object||!type.info)throw std::runtime_error("generic Schedule type arguments unavailable");
    auto types=scope.Root(runtime->NewArraySpecific(type.info,1));int index=0;void* set_args[]{job.type_object,&index};
    scope.Call(scope.Method("mscorlib.dll","System","Array","SetValue","System.Object|System.Int32","System.Void",2),types,set_args);
    void* args[]{types};auto specialized=scope.Call(scope.Method("mscorlib.dll","System.Reflection","RuntimeMethodInfo",
        "MakeGenericMethod","System.Type[]","System.Reflection.MethodInfo",1),reflected,args);
    const auto info=from_reflection(specialized);void* entry=nullptr;
    if(info) {
        char* first=api.type_name(param(info,0));std::string actual=first?first:"";if(first)api.free_name(first);
        std::replace(actual.begin(),actual.end(),'/','.');std::replace(actual.begin(),actual.end(),'+','.');
        if(actual!=std::string("BeyondDynamicBone.")+job_type+(terminal?"":"&"))
            throw std::runtime_error("closed contact Schedule parameter type mismatch");
    }
    // Same public MethodInfo method-pointer prefix used by Il2CppRuntime.
    if(info)std::memcpy(&entry,info,sizeof(entry));
    if(!entry)throw std::runtime_error("generic Schedule AOT entry unavailable");
    return {info,entry};
}
bool ContactCallsMethod(const ResolvedMethod& from,const ResolvedMethod& target,bool via_wrapper=false) {
    const auto snapshot=ContactCodeSnapshot(from);if(!snapshot.contains("code_hex"))return false;
    const auto bytes=snapshot.at("code_hex").get<std::string>();unsigned found=0;
    for(size_t n=0;n+8<=bytes.size();n+=8) {
        uint32_t word=0;for(int b=0;b<4;++b)word|=uint32_t(std::stoul(bytes.substr(n+b*2,2),nullptr,16))<<(b*8);
        if((word&0xfc000000)==0x94000000) {
            int64_t delta=int64_t(word&0x03ffffff);if(delta&(1<<25))delta-=int64_t(1)<<26;
            const auto pc=reinterpret_cast<intptr_t>(from.entry)+intptr_t(n/2);
            const auto callee=pc+delta*4;
            if(callee==reinterpret_cast<intptr_t>(target.entry))++found;
            else if(via_wrapper&&ContactCallsMethod({nullptr,reinterpret_cast<void*>(callee)},target,false))++found;
        }
        if(word==0xd65f03c0)break;
    }
    return found==1;
}
ResolvedMethod ContactScheduleMethod(Scope& scope,const char* job_type) {
    const auto broad=runtime->ResolveMethodExact(kCloth,"BeyondDynamicBone","SelfCollisionConstraint",
        "UpdateBroadPhase","Unity.Jobs.JobHandle","Unity.Jobs.JobHandle",1);
    auto selected=ContactScheduleSpecialization(scope,job_type,"ScheduleManagedCrossFrameJob");
    if(!ContactCallsMethod(broad,selected,true))throw std::runtime_error("named cross-frame terminal does not match the call graph");
    return selected;
}
Json ContactAbiSnapshot() {
    ContactMetadata metadata;Json out={{"read_only",true},{"hooks_installed",false},{"types",Json::object()}};
    for(auto type:{"SelfCollisionConstraint.UpdateEdgeEdgeBroadPhaseCrossFrameJob",
                  "SelfCollisionConstraint.UpdatePointTriangleBroadPhaseCrossFrameJob"})
        out["types"][type]=metadata.Layout(runtime->ResolveClass(kCloth,"BeyondDynamicBone",type).info);
    for(auto type:{"JobsUtility.JobScheduleParameters","UntypedUnsafeList"}) {
        const char* ns=std::string_view(type).starts_with("JobsUtility")?"Unity.Jobs.LowLevel.Unsafe":"Unity.Collections.LowLevel.Unsafe";
        auto cls=runtime->ResolveClass(kUnity,ns,type);
        if(!cls.info)cls=runtime->ResolveClass("Unity.Collections.dll",ns,type);
        out["types"][type]=metadata.Layout(cls.info);
    }
    out["types"]["JobHandle"]=metadata.Layout(runtime->ResolveClass(kUnity,"Unity.Jobs","JobHandle").info);
    out["update_broad_phase"]=ContactCodeSnapshot(runtime->ResolveMethodExact(kCloth,"BeyondDynamicBone",
        "SelfCollisionConstraint","UpdateBroadPhase","Unity.Jobs.JobHandle","Unity.Jobs.JobHandle",1));
    const auto broad=runtime->ResolveMethodExact(kCloth,"BeyondDynamicBone","SelfCollisionConstraint",
        "UpdateBroadPhase","Unity.Jobs.JobHandle","Unity.Jobs.JobHandle",1);
    if(out["update_broad_phase"].contains("code_hex")) {
        const auto bytes=out["update_broad_phase"]["code_hex"].get<std::string>();
        out["broad_phase_callees"]=Json::array();
        for(size_t n=0;n+8<=bytes.size();n+=8) {
            uint32_t word=0;for(int b=0;b<4;++b)word|=uint32_t(std::stoul(bytes.substr(n+b*2,2),nullptr,16))<<(b*8);
            if((word&0xfc000000)==0x94000000&&out["broad_phase_callees"].size()<12) {
                int64_t delta=int64_t(word&0x03ffffff);if(delta&(1<<25))delta-=int64_t(1)<<26;
                const auto target=reinterpret_cast<intptr_t>(broad.entry)+intptr_t(n/2)+delta*4;
                out["broad_phase_callees"].push_back(ContactCodeSnapshot({nullptr,reinterpret_cast<void*>(target)},512));
            }
            if(word==0xd65f03c0)break;
        }
    }
    for(auto method:{"Schedule","Schedule_Injected"}) {
        out[method]=ContactCodeSnapshot(runtime->ResolveMethod(kUnity,"Unity.Jobs.LowLevel.Unsafe","JobsUtility",
            method,std::string_view(method)=="Schedule"?1:2));
        out[std::string(method)+"_signature"]=runtime->DescribeMethod(kUnity,"Unity.Jobs.LowLevel.Unsafe","JobsUtility",method);
    }
    for(auto job:{"SelfCollisionConstraint.UpdateEdgeEdgeBroadPhaseCrossFrameJob",
                  "SelfCollisionConstraint.UpdatePointTriangleBroadPhaseCrossFrameJob"}) {
        try {Scope scope;out["specialized_schedules"][job]=ContactCodeSnapshot(ContactScheduleMethod(scope,job));}
        catch(const std::exception& e){out["specialized_schedules"][job]={{"error",e.what()}};}
    }
    return out;
}
