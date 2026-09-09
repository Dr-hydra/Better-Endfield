// Main-thread, character-scoped visual overlay. No controller replacement,
// Animator disable, world-root movement, scale writes or AssetBundle calls.
bool g_pose_contract = true;
TickFn g_pose_tail = nullptr;
PoseBank g_pose_bank;
bool g_pose_data_attempted = false, g_pose_data_ready = false;
uint64_t g_pose_generation=0;
struct PoseBinding {
    void* transform = nullptr;uint32_t pin=0;size_t sample=0;
    BonePose baseline{},last{};bool wrote=false;
};
struct PoseOwner {
    void* component=nullptr;void* animator=nullptr;void* root=nullptr;
    uint32_t component_pin=0,animator_pin=0,root_pin=0;
    std::vector<PoseBinding> bindings;
    PoseClock clock;uint32_t writes=0;double microseconds=0;uint32_t missing=0;
} g_pose_owner;
bool PoseReadLocal(void* transform,BonePose& value){
    bool ok=true;void* args[]{&value.position,&value.rotation};Invoke(PoseGetLocal,transform,args,ok);
    const float n=PoseDot(value.rotation,value.rotation);
    if(!ok||!std::isfinite(n)||n<.5f||n>1.5f||!std::isfinite(value.position.x)||!std::isfinite(value.position.y)||!std::isfinite(value.position.z))return false;
    value.rotation=PoseNormalize(value.rotation);return true;
}
bool PoseWriteLocal(void* transform,const BonePose& value){
    bool ok=true;BonePose copy=value;void* args[]{&copy.position,&copy.rotation};Invoke(PoseSetLocal,transform,args,ok);return ok;
}
void FreePoseOwner(PoseOwner& owner){
    for(auto& bone:owner.bindings)if(bone.pin)g_host->gchandle_free(g_host->context,bone.pin);
    for(auto pin:{owner.component_pin,owner.animator_pin,owner.root_pin})if(pin)g_host->gchandle_free(g_host->context,pin);
    owner={};
}
bool PoseOwnerMatches(const PoseOwner& owner){
    if(!owner.component||!UnityObjectAlive(owner.animator)||!UnityObjectAlive(owner.root))return false;
    bool ok=true;return Object(GetAnimator,owner.component,ok)==owner.animator&&ok&&Object(PoseComponentTransform,owner.animator,ok)==owner.root&&ok;
}
void ReleasePoseOverlay(bool restore){
    ++g_pose_generation;
    auto old=std::move(g_pose_owner);g_pose_owner={};
    if(restore&&PoseOwnerMatches(old))for(auto& bone:old.bindings){
        if(g_pose_owner.component)break; // Reentrant new session owns any future writes.
        if(!bone.wrote||!UnityObjectAlive(bone.transform))continue;
        bool child_ok=true;void* root_args[]{old.root};if(!Value<bool>(PoseIsChild,bone.transform,child_ok,root_args)||!child_ok)continue;
        BonePose current;
        if(PoseReadLocal(bone.transform,current)){
            BonePose clean=PoseNativeBaseline(current,bone.last,bone.baseline,true);
            if(!PoseSame(clean.position,current.position)||!PoseSame(clean.rotation,current.rotation))PoseWriteLocal(bone.transform,clean);
        }
    }
    if(old.component&&old.writes){
        char text[192]{};std::snprintf(text,sizeof(text),"Aglina v12: pose overlay released, frames=%u, mean_apply_us=%.1f, restored=%d.",old.writes,old.microseconds/old.writes,restore);
        Log(text);
    }
    FreePoseOwner(old);
}
bool LoadPoseBank(){
    if(g_pose_data_attempted)return g_pose_data_ready;
    g_pose_data_attempted=true;
    wchar_t name[32768]{};HMODULE module=nullptr;
    if(!GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS|GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,reinterpret_cast<LPCWSTR>(&g_pose_bank),&module)||
       !GetModuleFileNameW(module,name,static_cast<DWORD>(std::size(name))))return false;
    const auto file=std::filesystem::path(name).parent_path()/L"actions"/L"aglina_pose_v12.bin";
    std::ifstream input(file,std::ios::binary);std::string error;
    if(!input){Log("Aglina v12: bone-pose file absent; native v9 hold only.");return false;}
    try {g_pose_data_ready=g_pose_bank.Load(input,error);}catch(const std::exception& ex){error=ex.what();}
    if(!g_pose_data_ready){Log(("Aglina v12: pose file rejected: "+error).c_str());return false;}
    Log("Aglina v12: bone-pose file loaded; 104 samples, independent 103/60-second loop.");return true;
}
bool BeginPoseOverlay(void* component){
    ReleasePoseOverlay(true);
    if(!g_pose_contract||!LoadPoseBank())return false;
    using StringNew=void*(*)(const char*);
    auto string_new=reinterpret_cast<StringNew>(GetProcAddress(GetModuleHandleW(L"GameAssembly.dll"),"il2cpp_string_new"));
    if(!string_new)return false;
    PoseOwner owner;owner.component=component;bool ok=true;
    owner.animator=Object(GetAnimator,component,ok);owner.root=Object(PoseComponentTransform,owner.animator,ok);
    if(!ok||!UnityObjectAlive(owner.root))return false;
    owner.component_pin=g_host->gchandle_new(g_host->context,component,1);
    owner.animator_pin=g_host->gchandle_new(g_host->context,owner.animator,1);
    owner.root_pin=g_host->gchandle_new(g_host->context,owner.root,1);
    if(!owner.component_pin||!owner.animator_pin||!owner.root_pin){FreePoseOwner(owner);return false;}
    unsigned missing_required=0;
    for(size_t i=0;i<g_pose_bank.bones.size();i++){
        const auto& spec=g_pose_bank.bones[i];auto path=string_new(spec.path.c_str());
        bool found=path!=nullptr;void* args[]{path};auto transform=Invoke(PoseFind,owner.root,args,found);
        PoseBinding bone;bone.transform=transform;bone.sample=i;
        if(found&&UnityObjectAlive(transform)&&PoseReadLocal(transform,bone.baseline))bone.pin=g_host->gchandle_new(g_host->context,transform,1);
        if(!bone.pin){
            ++owner.missing;if(spec.required)++missing_required;
            if(owner.missing<=4)Log(("Aglina v12: bone unavailable: "+spec.path).c_str());
        }else owner.bindings.push_back(bone);
    }
    if(missing_required||owner.bindings.size()*5<g_pose_bank.bones.size()*4||g_session.component!=component){
        char text[160]{};std::snprintf(text,sizeof(text),"Aglina v12: bone binding rejected: matched=%zu missing_required=%u; native hold only.",owner.bindings.size(),missing_required);Log(text);
        FreePoseOwner(owner);return false;
    }
    char text[160]{};std::snprintf(text,sizeof(text),"Aglina v12: bone overlay bound: matched=%zu missing_optional=%u; waiting for process window.",owner.bindings.size(),owner.missing);Log(text);
    ++g_pose_generation;g_pose_owner=std::move(owner);return true;
}
void ApplyPoseOverlay(void* component,float delta){
    if(component!=g_pose_owner.component||GetCurrentThreadId()!=g_game_thread.load(std::memory_order_relaxed))return;
    const uint64_t generation=g_pose_generation;
    if(g_stopping||!PoseOwnerMatches(g_pose_owner)){ReleasePoseOverlay(false);return;}
    if(generation!=g_pose_generation)return;
    bool ok=true;int frame_id=Unbox<int>(Invoke(PoseFrameCount,nullptr,nullptr,ok),ok);
    if(generation!=g_pose_generation)return;
    if(!ok){ReleasePoseOverlay(true);return;}
    if(frame_id==g_pose_owner.clock.last_frame)return;
    Frame frame{};int hash=0;bool read=ReadFrame(component,frame,hash);
    const auto config=g_config.load();
    bool allowed=read&&g_session.component==component&&g_session.policy.active&&config->enabled&&config->external_loop&&
        g_session.config==config&&frame.current_special&&!frame.outgoing&&CurrentRequestHandle(component)==g_session.handle;
    if(generation!=g_pose_generation)return;
    if(!allowed&&g_pose_owner.clock.state!=PoseClock::State::Waiting)g_pose_owner.clock.End();
    const int side=hash==g_hashes[0]?0:hash==g_hashes[1]?1:-1;
    bool apply=g_pose_owner.clock.Tick(frame_id,delta,allowed,side,read?frame.time:0.f,read?frame.length:1.f,g_pose_bank.entry);
    if(g_pose_owner.clock.state==PoseClock::State::Done){ReleasePoseOverlay(true);return;}
    if(!apply)return;
    LARGE_INTEGER begin{},end{},frequency{};QueryPerformanceFrequency(&frequency);QueryPerformanceCounter(&begin);
    const float w=g_pose_owner.clock.weight;const float weight=w*w*(3-2*w);
    const int selected=g_pose_owner.clock.side;const double seconds=g_pose_owner.clock.seconds;
    for(auto& bone:g_pose_owner.bindings){
        bool child_ok=true;void* root_args[]{g_pose_owner.root};bool child=Value<bool>(PoseIsChild,bone.transform,child_ok,root_args);
        if(generation!=g_pose_generation)return;
        if(!child_ok||!child){Log("Aglina v12: bone left owned hierarchy; ending overlay.");ReleasePoseOverlay(true);return;}
        BonePose current;
        const bool read_ok=PoseReadLocal(bone.transform,current);
        if(generation!=g_pose_generation)return;
        if(!read_ok){
            Log("Aglina v12: bone read failed; ending overlay.");ReleasePoseOverlay(true);return;
        }
        bone.baseline=PoseNativeBaseline(current,bone.last,bone.baseline,bone.wrote);
        auto sampled=g_pose_bank.Sample(selected,bone.sample,seconds);
        auto output=PoseBlend(bone.baseline,sampled,weight);
        const bool write_ok=PoseWriteLocal(bone.transform,output);
        if(generation!=g_pose_generation)return;
        if(!write_ok){
            Log("Aglina v12: bone write failed; ending overlay.");ReleasePoseOverlay(true);return;
        }
        bone.last=output;bone.wrote=true;
    }
    QueryPerformanceCounter(&end);g_pose_owner.microseconds+=(end.QuadPart-begin.QuadPart)*1e6/frequency.QuadPart;
    ++g_pose_owner.writes;
    if(g_pose_owner.writes<=2||g_pose_owner.writes%120==0){
        char text[240]{};std::snprintf(text,sizeof(text),"Aglina v12: TailLate pose applied side=%s bones=%zu weight=%.3f time=%.3f frame=%d count=%u mean_us=%.1f; Animator remains native.",selected==0?"left":"right",g_pose_owner.bindings.size(),weight,seconds,frame_id,g_pose_owner.writes,g_pose_owner.microseconds/g_pose_owner.writes);Log(text);
    }
}
void __fastcall PoseTailDetour(void* component,float delta,const void* method){
    g_pose_tail(component,delta,method);
    ApplyPoseOverlay(component,delta);
}
