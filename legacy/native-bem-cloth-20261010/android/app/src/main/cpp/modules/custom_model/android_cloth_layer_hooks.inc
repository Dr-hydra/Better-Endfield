// Named BurstDirectCall getters supply native kernel pointers. No RVAs or
// whole-library disassembly; all worker code uses bounded POD data only.
namespace layer_hook {
namespace L=BetterEndfieldNext::CustomModel::ClothLayer;
HookBroker broker;
void* stubs[2]{};
using Fn=void(*)(void*,const L::Contact*,int*,int*,int32_t);
Fn original[2]{};
std::shared_ptr<const L::Policy> policy;
std::atomic<uint64_t> kept{},changed{},invalid{},enabled_changed{},calls[2]{};
Json evidence;
struct List {uintptr_t data;int32_t length,capacity;};

std::shared_ptr<const L::Policy> Snapshot(const L::Contact* buffer,int n) {
    auto p=std::atomic_load_explicit(&policy,std::memory_order_acquire);
    if(!p||!buffer||n<0||n>131072)return {};
    const auto& list=*reinterpret_cast<const List*>(p->list);
    if(list.data!=reinterpret_cast<uintptr_t>(buffer)||list.length<n||list.length<0||list.capacity<list.length)return {};
    return p;
}
L::Result CorrectLocal(const L::Policy& p,const L::Contact& source,L::Contact& copy) {
    const auto result=L::Correct(p,source,copy);
    if(result==L::Result::Changed){++changed;if(source.flags[0]&0x80000000u)++enabled_changed;}
    else if(result==L::Result::Kept)++kept;
    else if(result==L::Result::Invalid)++invalid;
    return result;
}
void Single(void* next,const L::Contact* buffer,int* counts,int* sums,int32_t index) {
    ++calls[0];L::Contact copy;
    auto p=index>=0&&index<131072?Snapshot(buffer,index+1):nullptr;
    if(p&&CorrectLocal(*p,buffer[index],copy)==L::Result::Changed){buffer=&copy;index=0;}
    original[0](next,buffer,counts,sums,index);
}
void Range(void* next,const L::Contact* buffer,int* counts,int* sums,int32_t length) {
    ++calls[1];auto p=Snapshot(buffer,length);
    if(p) {
        try {
            std::vector<L::Contact> copies;L::Contact copy;
            for(int i=0;i<length;++i)if(CorrectLocal(*p,buffer[i],copy)==L::Result::Changed) {
                if(copies.empty())copies.assign(buffer,buffer+length);
                copies[size_t(i)]=copy;
            }
            if(!copies.empty()){original[1](next,copies.data(),counts,sums,length);return;}
        } catch(...) { /* Forward the unmodified source on allocation failure. */ }
    }
    original[1](next,buffer,counts,sums,length);
}
void Clear(Scope& s) {
    std::atomic_store_explicit(&policy,std::shared_ptr<const L::Policy>{},std::memory_order_release);
    contact_hook::Complete(s);
}
bool Release(Scope& s) {
    Clear(s);bool ok=true;for(auto& stub:stubs)if(stub)ok=broker.Remove(stub)&&ok;
    return ok;
}
void Validate(Scope& s) {
    ContactMetadata metadata;
    auto cls=runtime->ResolveClass(kCloth,"BeyondDynamicBone","SelfCollisionConstraint.PointTriangleContact");
    CandidateNeed(cls.info,"layer contact class unavailable");
    L::Contact zero;auto box=s.Root(runtime->BoxValue(cls.info,&zero));
    CandidateNeed(box&&runtime->Unbox(box),"layer contact boxing unavailable");
    const size_t header=reinterpret_cast<uintptr_t>(runtime->Unbox(box))-reinterpret_cast<uintptr_t>(box);
    const auto layout=metadata.Layout(cls.info);CandidateNeed(layout.at("size")==sizeof(L::Contact),"layer contact size mismatch");
    struct F {const char* name;const char* type;size_t offset;};
    for(auto f:{F{"flagAndTeamId0","System.UInt32",0},F{"flagAndTeamId1","System.UInt32",4},
        F{"thickness","Unity.Mathematics.half",8},F{"sign","Unity.Mathematics.half",10},
        F{"pointParticleIndex","System.Int32",12},F{"triangleParticleIndex","Unity.Mathematics.int3",16},
        F{"pointInvMass","Unity.Mathematics.half",28},F{"triangleInvMass","Unity.Mathematics.half3",30}})
        contact_hook::Check(layout,f.name,f.type,f.offset,header);
}
void* NativeGetter(Scope& s,const char* prefix) {
    ContactMetadata api;
    auto domain=api.Export<void*(*)()>("il2cpp_domain_get");
    auto assemblies=api.Export<const void**(*)(void*,size_t*)>("il2cpp_domain_get_assemblies");
    auto assembly_image=api.Export<const void*(*)(const void*)>("il2cpp_assembly_get_image");
    auto image_name=api.Export<const char*(*)(const void*)>("il2cpp_image_get_name");
    auto class_count=api.Export<size_t(*)(const void*)>("il2cpp_image_get_class_count");
    auto get_class=api.Export<Il2CppClass*(*)(const void*,size_t)>("il2cpp_image_get_class");
    auto class_name=api.Export<const char*(*)(Il2CppClass*)>("il2cpp_class_get_name");
    auto method=api.Export<const MethodInfo*(*)(Il2CppClass*,const char*,int)>("il2cpp_class_get_method_from_name");
    auto returns=api.Export<const Il2CppType*(*)(const MethodInfo*)>("il2cpp_method_get_return_type");
    auto param=api.Export<const Il2CppType*(*)(const MethodInfo*,uint32_t)>("il2cpp_method_get_param");
    auto method_flags=api.Export<uint32_t(*)(const MethodInfo*,uint32_t*)>("il2cpp_method_get_flags");
    CandidateNeed(domain&&assemblies&&assembly_image&&image_name&&class_count&&get_class&&class_name&&method&&returns&&param&&method_flags&&api.type_name&&api.free_name,
        "layer kernel metadata exports unavailable");
    const MethodInfo* getter=nullptr;std::string type_name;size_t count=0;
    auto list=assemblies(domain(),&count);CandidateNeed(count<=1024,"layer assembly budget exceeded");
    for(size_t i=0;i<count;++i) {
        auto image=assembly_image(list[i]);const char* name=image_name(image);
        if(!name||std::string_view(name)!=kCloth)continue;
        const size_t n=class_count(image);CandidateNeed(n<=65536,"layer class budget exceeded");
        for(size_t j=0;j<n;++j) {
            auto cls=get_class(image,j);const char* label=class_name(cls);if(!label)continue;
            const std::string_view text(label);
            if(!text.starts_with(prefix)||!text.ends_with("$BurstDirectCall"))continue;
            CandidateNeed(!getter,"ambiguous named layer kernel getter");
            auto invoke=method(cls,"Invoke",5);CandidateNeed(invoke,"layer kernel Invoke signature unavailable");
            const char* arguments[]{"Unity.Mathematics.double3*","BeyondDynamicBone.SelfCollisionConstraint.PointTriangleContact*",
                "System.Int32*","System.Int32*","System.Int32"};
            for(uint32_t k=0;k<5;++k) {
                char* raw=api.type_name(param(invoke,k));bool match=raw&&std::string_view(raw)==arguments[k];
                if(raw)api.free_name(raw);CandidateNeed(match,"layer kernel Invoke parameter mismatch");
            }
            char* result=api.type_name(returns(invoke));bool is_void=result&&std::string_view(result)=="System.Void";
            if(result)api.free_name(result);CandidateNeed(is_void,"layer kernel Invoke return mismatch");
            getter=method(cls,"GetFunctionPointer",0);type_name=label;
        }
    }
    CandidateNeed(getter,"named layer kernel getter missing");
    uint32_t implementation=0;CandidateNeed(method_flags(getter,&implementation)&0x10,"layer kernel getter must be static");
    char* raw=api.type_name(returns(getter));const bool intptr=raw&&std::string_view(raw)=="System.IntPtr";
    if(raw)api.free_name(raw);CandidateNeed(intptr,"layer kernel getter return signature mismatch");
    auto value=runtime->Unbox(s.Call({getter,nullptr},nullptr));CandidateNeed(value,"layer kernel pointer result missing");
    uintptr_t address=0;std::memcpy(&address,value,sizeof(address));
    auto code=ContactCodeSnapshot({getter,reinterpret_cast<void*>(address)},64);
    CandidateNeed(address&&!(address&3)&&code.contains("code_hex"),"layer kernel pointer not executable");
    evidence[type_name+".GetFunctionPointer() -> System.IntPtr"]=std::move(code);
    return reinterpret_cast<void*>(address);
}
bool Install(Scope& s) {
    if(stubs[0]&&stubs[1])return true;
    try {
        Validate(s);
        void* targets[]{NativeGetter(s,"SolverPointTriangleCrossFrameKernel_"),
            NativeGetter(s,"SolverPointTriangleCrossFrameRangeKernel_")};
        CandidateNeed(targets[0]!=targets[1],"layer kernels unexpectedly alias");
        std::string error;CandidateNeed(broker.Initialize(error),"layer hook broker unavailable");
        void* detours[]{reinterpret_cast<void*>(&Single),reinterpret_cast<void*>(&Range)};
        for(int i=0;i<2;++i)CandidateNeed(broker.Install("betterendfieldnext.custom_model.layer",targets[i],detours[i],
            reinterpret_cast<void**>(&original[i]),stubs[i],error),"layer kernel hook install failed");
        kept=0;changed=0;invalid=0;enabled_changed=0;calls[0]=0;calls[1]=0;return true;
    } catch(const std::exception& e) {evidence["error"]=e.what();Release(s);return false;}
}
Json Status() {
    return {{"installed",bool(stubs[0]&&stubs[1])},{"matched",kept.load()+changed.load()},
        {"corrected",changed.load()},{"enabled_corrected",enabled_changed.load()},
        {"invalid",invalid.load()},{"calls",Json::array({calls[0].load(),calls[1].load()})},
        {"evidence",evidence},{"shared_contacts_modified",false}};
}
}
