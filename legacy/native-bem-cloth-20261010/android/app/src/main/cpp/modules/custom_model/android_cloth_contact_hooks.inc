// Method hooks only: one named broad-phase method and two closed
// IJobExtensions.ScheduleManagedCrossFrameJob<T> methods
// methods obtained through managed MakeGenericMethod. No RVA hook targets.
namespace contact_hook {
using namespace BetterEndfieldNext::CustomModel::ClothContact;
void Need(bool ok,const char* why){if(!ok)throw std::runtime_error(why);}
using BroadFn=Handle(*)(void*,Handle,const MethodInfo*);
using ScheduleFn=Handle(*)(const Job*,int32_t,Handle,const MethodInfo*);
HookBroker broker;
void* stubs[3]{};
BroadFn original_broad=nullptr;
ScheduleFn original_schedule[2]{};
ResolvedMethod broad,complete_all,complete_handle;
ResolvedField list_fields[2];
ResolvedField cross_frame_flag;
void* manager=nullptr;
void* cloth_manager=nullptr;
void* owner_thread=nullptr;
std::vector<uint32_t> roots;
std::atomic<bool> active{false},fault{false};
std::atomic<uint64_t> repairs[2]{},schedules[2]{};
size_t length_offset=0;
struct Frame {uintptr_t headers[2]{};Frame* previous=nullptr;};
thread_local Frame* frame=nullptr;

Handle Schedule(int kind,const Job* job,int32_t priority,Handle dependency,const MethodInfo* method) {
    if(!active.load(std::memory_order_acquire)||!frame)
        return original_schedule[kind](job,priority,dependency,method);
    Job copy{};const auto repair=job?BindCounter(*job,frame->headers[kind],length_offset,copy):Repair::Refused;
    ++schedules[kind];
    if(repair==Repair::Refused){fault.store(true);return dependency;}
    if(repair==Repair::Bound){++repairs[kind];return original_schedule[kind](&copy,priority,dependency,method);}
    return original_schedule[kind](job,priority,dependency,method);
}
Handle Edge(const Job* job,int32_t priority,Handle dependency,const MethodInfo* method){return Schedule(0,job,priority,dependency,method);}
Handle Point(const Job* job,int32_t priority,Handle dependency,const MethodInfo* method){return Schedule(1,job,priority,dependency,method);}
Handle Broad(void* self,Handle dependency,const MethodInfo* method) {
    if(!active.load(std::memory_order_acquire)||self!=manager||runtime->CurrentThread()!=owner_thread)
        return original_broad(self,dependency,method);
    Frame local;local.previous=frame;
    struct Restore {Frame* before;~Restore(){frame=before;}} restore{frame};
    try {
        Scope scope;
        auto flag=scope.Root(runtime->ReadFieldObject(cross_frame_flag,nullptr));auto flag_data=runtime->Unbox(flag);
        Need(flag_data&&*static_cast<bool*>(flag_data),"contact cross-frame mode changed");
        for(int i=0;i<2;++i) {
            auto box=scope.Root(runtime->ReadFieldObject(list_fields[i],self));auto data=runtime->Unbox(box);
            Need(data,"contact list readback unavailable");Container list{};std::memcpy(&list,data,sizeof(list));
            Need(list.data!=0,"contact list header unavailable");local.headers[i]=list.data;
        }
        frame=&local;return original_broad(self,dependency,method);
    } catch(...) {fault.store(true);return dependency;}
}
const Json& Field(const Json& type,const char* name) {
    for(const auto& field:type.at("fields"))if(field.at("name")==name)return field;
    throw std::runtime_error(std::string("contact ABI field unavailable: ")+name);
}
void Check(const Json& type,const char* field,const char* name,size_t offset,size_t header) {
    const auto& f=Field(type,field);
    Need(f.at("type")==name&&f.at("boxed_offset").get<size_t>()==offset+header,"contact ABI named field mismatch");
}
void ValidateLayout(Scope& s,const Json& types) {
    const char* jobs[]{"SelfCollisionConstraint.UpdateEdgeEdgeBroadPhaseCrossFrameJob",
                       "SelfCollisionConstraint.UpdatePointTriangleBroadPhaseCrossFrameJob"};
    const char* lists[]{"edgeEdgeContactList","pointTriangleContactList"};
    Job zero{};auto cls=runtime->ResolveClass(kCloth,"BeyondDynamicBone",jobs[0]);
    auto box=s.Root(runtime->BoxValue(cls.info,&zero));auto data=runtime->Unbox(box);
    Need(box&&data,"contact boxed ABI unavailable");
    const size_t header=reinterpret_cast<uintptr_t>(data)-reinterpret_cast<uintptr_t>(box);
    Need(header>0&&header<=64,"contact boxed ABI invalid");
    for(int i=0;i<2;++i) {
        const auto& t=types.at(jobs[i]);Need(t.at("size")==sizeof(Job)&&t.at("alignment")==alignof(Job),"contact job size mismatch");
        Check(t,"indexCount","Unity.Collections.NativeReference<System.Int32>",offsetof(Job,count),header);
        Check(t,"nextPosArray","Unity.Collections.NativeArray<Unity.Mathematics.double3>",offsetof(Job,next),header);
        Check(t,"oldPosArray","Unity.Collections.NativeArray<Unity.Mathematics.double3>",offsetof(Job,old),header);
        const auto& list=Field(t,lists[i]);
        Need(list.at("boxed_offset").get<size_t>()==offsetof(Job,list)+header&&
            list.at("layout").at("size")==sizeof(Container),"contact list ABI mismatch");
        const auto& count=Field(t,"indexCount").at("layout");Need(count.at("size")==sizeof(Container),"contact reference size mismatch");
        Check(count,"m_Data","System.Void*",offsetof(Container,data),header);
        Check(count,"m_AllocatorLabel","Unity.Collections.AllocatorManager.AllocatorHandle",offsetof(Container,tail),header);
        Need(Field(list.at("layout"),"m_ListData").at("boxed_offset")==header,"contact list pointer mismatch");
        for(auto name:{"nextPosArray","oldPosArray"}) {
            const auto& a=Field(t,name).at("layout");Need(a.at("size")==sizeof(Container),"contact array size mismatch");
            Check(a,"m_Buffer","System.Void*",0,header);Check(a,"m_Length","System.Int32",8,header);
        }
    }
    const auto& handle=types.at("JobHandle");Need(handle.at("size")==sizeof(Handle),"contact JobHandle size mismatch");
    Check(handle,"jobGroup","System.UInt64",offsetof(Handle,group),header);
    Check(handle,"jobType","System.Int32",offsetof(Handle,type_and_padding),header);
    const auto& list=types.at("UntypedUnsafeList");Need(list.at("size")==32,"native list header size mismatch");
    Check(list,"Ptr","System.Void*",0,header);Check(list,"m_capacity","System.Int32",12,header);
    Check(list,"m_length","System.Int32",sizeof(uintptr_t),header);
    length_offset=Field(list,"m_length").at("boxed_offset").get<size_t>()-header;
}
bool CallsNamedSchedule(const ResolvedMethod& from,const ResolvedMethod& target) {
    // Verify the current method calls this closed generic method. Only the
    // metadata-resolved method entry is hooked; decoded calls are evidence.
    return ContactCallsMethod(from,target,true);
}
void Complete(Scope& s){if(cloth_manager)s.Call(complete_all,cloth_manager);}
bool Release(Scope& s) {
    Complete(s);active.store(false,std::memory_order_release);
    bool removed=true;for(auto& stub:stubs)if(stub)removed=broker.Remove(stub)&&removed;
    if(!removed)return false;
    for(auto h:roots)runtime->FreeGcHandle(h);roots.clear();manager=nullptr;cloth_manager=nullptr;return true;
}
void Install(Scope& s) {
    Need(!active.load()&&!manager,"contact hook lease already active");
    const auto snapshot=ContactAbiSnapshot();ValidateLayout(s,snapshot.at("types"));
    ResolvedMethod schedule[2]{ContactScheduleMethod(s,"SelfCollisionConstraint.UpdateEdgeEdgeBroadPhaseCrossFrameJob"),
        ContactScheduleMethod(s,"SelfCollisionConstraint.UpdatePointTriangleBroadPhaseCrossFrameJob")};
    broad=s.Method(kCloth,"BeyondDynamicBone","SelfCollisionConstraint","UpdateBroadPhase","Unity.Jobs.JobHandle","Unity.Jobs.JobHandle",1);
    Need(broad.entry&&schedule[0].entry!=schedule[1].entry&&CallsNamedSchedule(broad,schedule[0])&&CallsNamedSchedule(broad,schedule[1]),
        "named Schedule methods do not match the broad-phase call graph");
    auto sim=s.Call(s.Method(kCloth,"BeyondDynamicBone","MagicaManager","get_Simulation","","BeyondDynamicBone.SimulationManager",0),nullptr);
    auto owner=s.Field(sim,"SimulationManager","selfCollisionConstraint");Need(owner,"contact manager unavailable");
    auto cloth=s.Call(s.Method(kCloth,"BeyondDynamicBone","MagicaManager","get_Cloth","","BeyondDynamicBone.ClothManager",0),nullptr);
    complete_all=s.Method(kCloth,"BeyondDynamicBone","ClothManager","ForceCompleteAllJob","","System.Void",0);
    complete_handle=s.Method(kUnity,"Unity.Jobs","JobHandle","Complete","","System.Void",0);
    Need(cloth&&complete_all.info&&complete_handle.info,"contact completion boundary unavailable");
    list_fields[0]=runtime->ResolveField(kCloth,"BeyondDynamicBone","SelfCollisionConstraint","edgeEdgeContactList");
    list_fields[1]=runtime->ResolveField(kCloth,"BeyondDynamicBone","SelfCollisionConstraint","pointTriangleContactList");
    cross_frame_flag=runtime->ResolveField(kCloth,"BeyondDynamicBone","MagicaManager","UseCrossFrameJob");
    auto flag=s.Root(runtime->ReadFieldObject(cross_frame_flag,nullptr));auto flag_data=runtime->Unbox(flag);
    Need(flag_data&&*static_cast<bool*>(flag_data),"contact test requires the game's cross-frame mode");
    Need(list_fields[0].info&&list_fields[1].info,"contact manager list fields unavailable");
    for(auto object:{owner,cloth}){auto h=runtime->NewGcHandle(object,false);Need(h!=0,"contact lease root failed");roots.push_back(h);}
    manager=owner;cloth_manager=cloth;owner_thread=runtime->CurrentThread();Complete(s);
    std::string error;Need(broker.Initialize(error),"contact hook broker unavailable");
    fault.store(false);for(int i=0;i<2;++i){repairs[i]=0;schedules[i]=0;}
    void* detours[]{reinterpret_cast<void*>(&Edge),reinterpret_cast<void*>(&Point)};
    for(int i=0;i<2;++i)if(!broker.Install("betterendfieldnext.custom_model.cloth",schedule[i].entry,detours[i],
            reinterpret_cast<void**>(&original_schedule[i]),stubs[i+1],error)) {
        Release(s);throw std::runtime_error("named contact Schedule hook failed: "+error);
    }
    if(!broker.Install("betterendfieldnext.custom_model.cloth",broad.entry,reinterpret_cast<void*>(&Broad),
            reinterpret_cast<void**>(&original_broad),stubs[0],error)) {
        Release(s);throw std::runtime_error("named contact broad-phase hook failed: "+error);
    }
    active.store(true,std::memory_order_release);
}
Json SmokeTest(Scope& s) {
    Install(s);Complete(s);
    for(int i=0;i<2;++i) {
        auto box=s.Root(runtime->ReadFieldObject(list_fields[i],manager));auto data=runtime->Unbox(box);Need(data,"contact list missing");
        Container list{};std::memcpy(&list,data,sizeof(list));Need(list.data!=0,"contact list not allocated");
        int32_t count=0;std::memcpy(&count,reinterpret_cast<void*>(list.data+length_offset),sizeof(count));
        Need(count==0,"scheduler smoke test requires empty contact lists");
    }
    Handle dependency{};void* args[]{&dependency};auto box=s.Call(broad,manager,args);auto result=runtime->Unbox(box);
    Need(result,"contact scheduler returned no JobHandle");s.Call(complete_handle,result);
    Need(!fault.load()&&schedules[0]==1&&schedules[1]==1&&repairs[0]==1&&repairs[1]==1,
        "contact method hook smoke test did not repair both jobs");
    return {{"method_hooks_verified",true},{"edge_repairs",repairs[0].load()},{"point_repairs",repairs[1].load()},
        {"jobs_completed",true},{"dependencies_changed",false},{"native_containers_written",false}};
}
}
