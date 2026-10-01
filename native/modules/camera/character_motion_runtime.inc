// PC-only character motion. Bodies are driven by the EIEM DirectVmd port
// (eiem/eiem_body.cpp): hidden ghost rig, FK + twist + FinalIK legs, optional
// terrain follow and SkeletalMorphCore face. This file picks the characters,
// holds their pose leases against the Actions module, feeds the clock and
// reports state to the MMD director. Called after CameraManager.TailLateTick.
//
// Squad playback: a work with motion2..motion4 also drives the other members
// of the current squad that are in the field, in squad slot order. They share
// the controlled character's position and facing as the stage origin, so the
// formation authored in multi-dancer VMDs is kept.
double MmdDirectorSeconds();
bool MmdDirectorPlaying();
namespace Mmd {std::filesystem::path ModuleDirectory();}
namespace CharacterMotion {
constexpr char kOwner[] = "betterendfield.camera.character";
constexpr int kMaxActors = EiemBody::kMaxActors;
struct Config {
    bool enabled=false, face=false, terrain=false, loop=false;
    float motion_scale=1;
    EiemBody::ClothMode cloth=EiemBody::ClothMode::Stable;
    std::string file;
};
struct Part {std::string motion, face;};
// NDK libc++ does not implement atomic<shared_ptr>; the standard atomic free
// functions retain the same immutable snapshot semantics on both platforms.
std::shared_ptr<const Config> config = std::make_shared<const Config>();
std::atomic_uint requests{0}; // 1 play/stop, 2 pause/resume, 4 stop, 8 MMD load, 16 MMD stop
// MMD director link: the director asks for a load (bit 8) with these files and
// then drives the pose from MmdDirectorSeconds() instead of the local clock.
enum DirectorPhase : int { DirectorIdle = 0, DirectorLoading = 1, DirectorBound = 2, DirectorFailed = 3 };
std::mutex director_mutex;
std::vector<Part> director_parts; // guarded by director_mutex; [0] is the controlled character
std::atomic_int director_phase{DirectorIdle};
std::atomic<double> director_duration{0.0};
std::atomic_int director_dancers{0};
std::atomic_bool stopping{false};
std::recursive_mutex gate;
thread_local bool in_callback=false;

struct Method {
    BE_MethodDescriptorV1 spec;
    BE_ResolvedMethodV1 value{};
};
constexpr const char* kCore="UnityEngine.CoreModule.dll";
constexpr const char* kGame="Gameplay.Beyond.dll";
Method methods[]{
    {{kCore,"UnityEngine","Object","op_Implicit","UnityEngine.Object","System.Boolean",1}},
    {{kCore,"UnityEngine","Component","get_transform",nullptr,"UnityEngine.Transform",0}},
    {{kCore,"UnityEngine","GameObject","GetComponentsInChildren","System.Type|System.Boolean","UnityEngine.Component[]",2}},
    {{"mscorlib.dll","System","Array","GetLength","System.Int32","System.Int32",1}},
    {{"mscorlib.dll","System","Array","GetValue","System.Int32","System.Object",1}},
    {{kCore,"UnityEngine","Transform","IsChildOf","UnityEngine.Transform","System.Boolean",1}},
    // Optional: without it EIEM runs without grounder/terrain support.
    {{kGame,"Beyond.Gameplay.Core","Entity","get_movementComponent",nullptr,"Beyond.Gameplay.Core.MovementComponent",0}},
    // Optional squad access (GameInstance.player.squadManager), as in Endfield-Poser.
    {{kGame,"Beyond.Gameplay","GameInstance","get_player",nullptr,"Beyond.Gameplay.GamePlayer",0}},
    {{kGame,"Beyond.Gameplay.Core","SquadManager","get_isSquadLoading",nullptr,"System.Boolean",0}},
    {{kGame,"Beyond.Gameplay.Core","SquadManager","get_slotCount",nullptr,"System.Int32",0}},
    {{kGame,"Beyond.Gameplay.Core","SquadManager","GetMemberBySlot","System.Int32","Beyond.Gameplay.Core.Entity",1}},
    // Optional: picks the body rig when the model carries more than one Animator.
    {{"UnityEngine.AnimationModule.dll","UnityEngine","Animator","get_isHuman",nullptr,"System.Boolean",0}},
    {{kCore,"UnityEngine","Behaviour","get_isActiveAndEnabled",nullptr,"System.Boolean",0}},
};
enum Id {Alive,Transform,Children,ArrayLength,ArrayValue,IsChild,Movement,Player,SquadLoading,SquadCount,SquadMember,IsHuman,ActiveEnabled};
void* Call(Id id,void* self,void** args=nullptr,bool* success=nullptr) {
    if(success)*success=false;
    if(!g_host || !g_host->runtime_invoke || !methods[id].value.method_info)return nullptr;
    void* exception=nullptr;
    void* result=g_host->runtime_invoke(g_host->context,methods[id].value.method_info,self,args,&exception);
    if(success)*success=exception==nullptr;
    return exception ? nullptr : result;
}
template<class T> bool Value(Id id,void* self,T& out,void** args=nullptr) {
    bool ok=false;void* boxed=Call(id,self,args,&ok);
    return ok && Unbox(boxed,out);
}
bool AliveObject(void* object) {
    if(!object)return false;
    bool alive=false;void* args[]{object};return Value(Alive,nullptr,alive,args)&&alive;
}
void* Character() {return Invoke(Contract("player_controller.get_main_character"),nullptr,nullptr);}
void* ModelOf(void* entity) {
    void* model=entity?Invoke(Contract("entity.get_model_com"),entity,nullptr):nullptr;
    return model?Invoke(Contract("base_model_component.get_model_go"),model,nullptr):nullptr;
}
void* Model() {return ModelOf(Character());}
std::wstring EiemLogPath() {
    // <install>\modules\ -> <install>\logs\ when present, else <install>\.
    const std::filesystem::path modules=Mmd::ModuleDirectory();
    if(modules.empty())return {};
    const auto install=modules.parent_path();
    std::error_code error;
    const auto logs=install/L"logs";
    return ((std::filesystem::is_directory(logs,error)?logs:install)/L"BetterEndfield.EiemBody.log").wstring();
}
struct Actor {
    void* entity=nullptr;
    void* model=nullptr;
    void* animator=nullptr;
    void* root=nullptr;
    uint64_t lease=0;
    std::vector<uint32_t> pins;
    bool loading=false,active=false;
    bool Bound()const{return lease!=0;}
};
struct Runtime final {
    const BE_PoseLeaseApiV1* leases=nullptr;
    BE_ResolvedClassV1 animator_class{};
    int squad_manager_offset=-1;
    std::shared_ptr<const Config> applied;
    CharacterPose::Clock clock;
    DWORD thread_id=0;
    Actor actors[kMaxActors];
    bool resolved=false,eiem_tried=false;
    bool director=false,director_stopping=false;
    bool Pin(Actor& actor,void* object) {
        if(!object || !g_host || !g_host->gchandle_new)return false;
        auto pin=g_host->gchandle_new(g_host->context,object,1);
        if(!pin)return false;
        try {actor.pins.push_back(pin);}catch(...){g_host->gchandle_free(g_host->context,pin);throw;}
        return true;
    }
    bool RootValid(const Actor& actor) {
        if(!AliveObject(actor.model)||!AliveObject(actor.animator)||!AliveObject(actor.root)||Call(Transform,actor.animator)!=actor.root)return false;
        void* model_root=Invoke(Contract("unity.game_object.transform"),actor.model,nullptr);
        bool inside=false;void* args[]{model_root};return model_root&&Value(IsChild,actor.root,inside,args)&&inside;
    }
    bool Loading()const{for(const auto& a:actors)if(a.loading)return true;return false;}
    bool Playing()const{for(const auto& a:actors)if(a.active)return true;return false;}
    int Dancers()const{int n=0;for(const auto& a:actors)if(a.active||a.loading)++n;return n;}
    // main_thread=false: shutdown on Host's thread; Unity must not be touched.
    void Release(int index,bool main_thread,const char* reason) {
        Actor& actor=actors[index];
        if(main_thread && thread_id==GetCurrentThreadId())EiemBody::Stop(index,reason);
        if(actor.lease && leases)leases->release(actor.root,kOwner,actor.lease);
        for(auto pin:actor.pins)if(g_host&&g_host->gchandle_free)g_host->gchandle_free(g_host->context,pin);
        actor=Actor{};
    }
    void Stop(bool main_thread,const char* reason) {
        const bool was=Loading()||Playing();
        for(int i=0;i<kMaxActors;++i)Release(i,main_thread,reason);
        if(main_thread && thread_id==GetCurrentThreadId())EiemBody::SetSharedAnchor(false,nullptr,nullptr);
        if(was)Log(std::string("Character VMD stopped: ")+reason);
        director_dancers.store(0);
        if(director){director=false;director_phase.store(director_stopping?DirectorIdle:DirectorFailed);}
    }
    bool Resolve() {
        if(resolved)return true;
        if(!g_host || !g_host->resolve_method || !g_host->resolve_class || !g_host->gchandle_new || !g_host->gchandle_free)return false;
#if defined(__ANDROID__)
        leases = BetterEndfield_GetPoseLeaseApiV1();
#else
        auto get=reinterpret_cast<BE_GetPoseLeaseApiV1Fn>(GetProcAddress(
            GetModuleHandleW(L"BetterEndfield.Host.dll"),"BetterEndfield_GetPoseLeaseApiV1"));
        leases=get?get():nullptr;
#endif
        if(!leases || leases->version!=1 || !leases->acquire || !leases->owns || !leases->release) {
            Log("Character VMD unavailable: install the matching Host with pose leases.");return false;
        }
        bool core=true;
        for(size_t i=0;i<std::size(methods);++i) {
            if(g_host->resolve_method(g_host->context,&methods[i].spec,&methods[i].value)!=BE_Result_Ok) {
                if(i<Movement)core=false;
                Log(std::string("Character VMD contract missing: ")+methods[i].spec.class_name+"."+methods[i].spec.method_name);
            }
        }
        if(g_host->resolve_field) {
            const BE_FieldDescriptorV1 field{kGame,"Beyond.Gameplay","GamePlayer","squadManager","Beyond.Gameplay.Core.SquadManager"};
            BE_ResolvedFieldV1 value{};
            if(g_host->resolve_field(g_host->context,&field,&value)==BE_Result_Ok && value.offset>=16 && value.offset<0x4000)
                squad_manager_offset=value.offset;
        }
        core &= g_host->resolve_class(g_host->context,"UnityEngine.AnimationModule.dll","UnityEngine","Animator",&animator_class)==BE_Result_Ok && animator_class.type_object;
        resolved=core;return resolved;
    }
    bool SquadReady()const {
        return squad_manager_offset>=16 && methods[Player].value.method_info && methods[SquadLoading].value.method_info &&
            methods[SquadCount].value.method_info && methods[SquadMember].value.method_info;
    }
    bool Eiem() {
        if(!eiem_tried){eiem_tried=true;EiemBody::Initialize(g_host,EiemLogPath());}
        if(!EiemBody::Available()){Log("Character VMD unavailable: EIEM body port could not resolve this client (see BetterEndfield.EiemBody.log).");return false;}
        return true;
    }
    bool CooperativeActions() {
#if defined(__ANDROID__)
        return leases != nullptr; // Actions shares this same registry in the Android library.
#else
        auto actions=GetModuleHandleW(L"BetterEndfield.Actions.dll");
        if(!actions)return true;
        using Version=uint32_t(BE_CALL*)();
        auto version=reinterpret_cast<Version>(GetProcAddress(actions,"BetterEndfield_ActionsPoseLeaseVersionV1"));
        if(version&&version()==1)return true;
        Log("Character VMD refused: Actions DLL lacks the pose-lease handshake; replace Host/Actions/Camera together.");return false;
#endif
    }
    // The model's body Animator. With several (weapons, props, effect rigs) the
    // only active humanoid one is taken; anything more ambiguous is refused, as
    // picking the first could silently animate a sub-rig. Candidates are logged.
    void* SingleAnimator(Actor& actor) {
        bool inactive=true;void* args[]{animator_class.type_object,&inactive};auto array=Call(Children,actor.model,args);
        if(!Pin(actor,array))return nullptr;
        int dim=0,count=0;void* length_args[]{&dim};
        if(!Value(ArrayLength,array,count,length_args)||count<1){Log("Character VMD: the model has no Animator.");return nullptr;}
        std::vector<void*> animators;
        for(int index=0;index<std::min(count,16);++index) {
            void* index_args[]{&index};
            if(auto object=Call(ArrayValue,array,index_args))animators.push_back(object);
        }
        if(count==1)return !animators.empty()&&Pin(actor,animators[0])?animators[0]:nullptr;
        void* active_human=nullptr;void* human=nullptr;int active_humans=0,humans=0;
        std::string names;
        for(void* animator:animators) {
            bool is_human=false,enabled=false;
            const bool human_known=methods[IsHuman].value.method_info&&Value(IsHuman,animator,is_human);
            const bool enabled_known=methods[ActiveEnabled].value.method_info&&Value(ActiveEnabled,animator,enabled);
            if(human_known&&is_human){++humans;human=animator;if(enabled_known&&enabled){++active_humans;active_human=animator;}}
            names+=(names.empty()?"'":", '")+ObjectName(animator)+"' human="+(human_known?(is_human?"1":"0"):"?")+
                " active="+(enabled_known?(enabled?"1":"0"):"?");
        }
        void* pick=active_humans==1?active_human:active_humans==0&&humans==1?human:nullptr;
        Log("Character VMD: the model has "+std::to_string(count)+" Animators ("+names+"); "+
            (pick?"using '"+ObjectName(pick)+"'.":"no single active humanoid one."));
        return pick&&Pin(actor,pick)?pick:nullptr;
    }
    // Squad members in slot order, the controlled character excluded.
    std::vector<void*> SquadMembers(void* leader) {
        std::vector<void*> members;
        if(!SquadReady())return members;
        void* player=Call(Player,nullptr);
        if(!player)return members;
        void* manager=*reinterpret_cast<void**>(static_cast<char*>(player)+squad_manager_offset);
        bool loading=true;int count=0;
        if(!manager||!Value(SquadLoading,manager,loading)||loading||!Value(SquadCount,manager,count))return members;
        for(int slot=0;slot<std::min(count,8);++slot) {
            void* args[]{&slot};void* entity=Call(SquadMember,manager,args);
            if(entity&&entity!=leader)members.push_back(entity);
        }
        return members;
    }
    // Takes the pose lease on an entity's rig and hands the rig to EIEM.
    bool Bind(int index,void* entity) {
        Actor& actor=actors[index];
        actor.entity=entity;actor.model=ModelOf(entity);
        if(!AliveObject(actor.model)||!Pin(actor,entity)||!Pin(actor,actor.model))return false;
        actor.animator=SingleAnimator(actor);
        if(!actor.animator){Log("Character VMD: no usable body Animator under the model '"+ObjectName(actor.model)+"'.");return false;}
        actor.root=Call(Transform,actor.animator);
        if(!Pin(actor,actor.root)||!RootValid(actor))return false;
        actor.lease=leases->acquire(actor.root,kOwner);
        if(!actor.lease){Log("Character VMD waiting for no writer: sustained-dash pose already owns this rig. Stop dash and press play again.");return false;}
        if(!EiemBody::EnsureActor(index))return false;
        void* movement=methods[Movement].value.method_info?Call(Movement,entity):nullptr;
        if(movement&&!Pin(actor,movement))movement=nullptr;
        EiemBody::SetTarget(index,entity,actor.animator,movement);
        if(!movement&&index==0)Log("Character VMD: MovementComponent unavailable; terrain follow disabled.");
        return true;
    }
    bool SharedAnchor() {
        Vector3 position{};Quaternion rotation{};
        if(!GetValue(Contract("unity.transform.position.get"),actors[0].root,position)||
            !Unbox(Invoke(Contract("unity.transform.rotation.get"),actors[0].root,nullptr),rotation))return false;
        const float p[3]{position.x,position.y,position.z},q[4]{rotation.x,rotation.y,rotation.z,rotation.w};
        for(float v:p)if(!std::isfinite(v))return false;
        for(float v:q)if(!std::isfinite(v))return false;
        EiemBody::SetSharedAnchor(true,p,q);
        return true;
    }
    bool BeginLoad(const std::vector<Part>& parts) {
        if(parts.empty()||parts[0].motion.empty())return false;
        if(!Resolve()||!CooperativeActions()||!Eiem())return false;
        void* leader=Character();
        if(!leader||!Bind(0,leader)){Log("Character VMD: enter the playable world first.");Release(0,true,"bind failed");return false;}
        int bound=1;
        std::vector<int> cast{0};
        if(parts.size()>1) {
            if(!SquadReady())Log("Character VMD: squad contracts unavailable; only the controlled character dances.");
            auto members=SquadMembers(leader);
            size_t next=0;
            for(size_t part=1;part<parts.size()&&bound<kMaxActors;++part) {
                if(parts[part].motion.empty())continue;
                bool placed=false;
                while(next<members.size()&&!placed) {
                    void* entity=members[next++];
                    if(Bind(bound,entity)){cast.push_back(int(part));placed=true;++bound;}
                    else Release(bound,true,"member unavailable");
                }
                if(!placed){Log("Character VMD: not enough squad members in the field for motion"+std::to_string(part+1)+".");break;}
            }
        }
        EiemBody::Options options;
        options.face=applied->face;options.terrain=applied->terrain;options.motion_scale=applied->motion_scale;
        options.cloth=applied->cloth;
        EiemBody::SetOptions(options);
        if(bound>1&&!SharedAnchor()){Log("Character VMD: could not read the stage origin; squad playback cancelled.");return false;}
        if(bound==1)EiemBody::SetSharedAnchor(false,nullptr,nullptr);
        for(int i=0;i<bound;++i) {
            const Part& part=parts[size_t(cast[size_t(i)])];
            EiemBody::Load(i,part.motion,applied->face?part.face:std::string());
            actors[i].loading=true;
        }
        director_dancers.store(bound);
        Log("Character VMD loading (EIEM DirectVmd), dancers="+std::to_string(bound)+".");
        return true;
    }
    void PublishClock() {
        if(director){EiemBody::PublishClock(MmdDirectorSeconds(),MmdDirectorPlaying());return;}
        double seconds=0;
        if(!clock.Advance(NowSeconds(),seconds)){Stop(true,"end of clip");return;}
        EiemBody::PublishClock(seconds,!clock.Paused());
    }
    // All loads settled: start every ready actor on the same clock.
    void Started() {
        double duration=0;
        for(int i=0;i<kMaxActors;++i)if(actors[i].Bound())duration=std::max(duration,EiemBody::DurationSeconds(i));
        if(!director)clock.Start(NowSeconds(),duration,applied->loop);
        PublishClock();
        int started=0;
        for(int i=0;i<kMaxActors;++i) {
            if(!actors[i].Bound())continue;
            if(EiemBody::Start(i,director?"mmd-director":"camera-hotkey")){actors[i].active=true;++started;}
            else if(i==0){Stop(true,"EIEM refused to enter DirectVmd");return;}
            else Release(i,true,"EIEM refused to enter DirectVmd");
        }
        if(director){director_duration.store(duration);director_phase.store(DirectorBound);}
        director_dancers.store(started);
        Log("Character VMD bound (EIEM DirectVmd): "+std::to_string(started)+" dancer(s), "+std::to_string(duration)+" s.");
    }
    void Validate() {
        const Actor& leader=actors[0];
        if(!AliveObject(leader.model)||Model()!=leader.model||!RootValid(leader)||!leases->owns(leader.root,kOwner,leader.lease)||!CooperativeActions()) {
            Stop(true,"character/rig/ownership changed");return;
        }
        for(int i=1;i<kMaxActors;++i) {
            const Actor& actor=actors[i];
            if(!actor.Bound())continue;
            if(!AliveObject(actor.model)||ModelOf(actor.entity)!=actor.model||!RootValid(actor)||!leases->owns(actor.root,kOwner,actor.lease)) {
                Release(i,true,"squad member left");Log("Character VMD: squad member "+std::to_string(i+1)+" stopped (left the field or rig changed).");
            }
        }
    }
    void Pump() {
        if(!thread_id)thread_id=GetCurrentThreadId();
        if(thread_id!=GetCurrentThreadId())return;
        auto next=std::atomic_load_explicit(&config, std::memory_order_acquire);
        if(next!=applied){Stop(true,"configuration changed");applied=std::move(next);}
        const unsigned wanted=requests.exchange(0,std::memory_order_acq_rel);
        if(!applied->enabled){
            if(Playing()||Loading())Stop(true,"disabled");
            if(wanted&8)director_phase.store(DirectorFailed);
            return;
        }
        if(wanted&16){director_stopping=true;Stop(true,"MMD stop");director_stopping=false;director=false;director_phase.store(DirectorIdle);}
        if(wanted&4){Stop(true,"stop key");return;}
        if(wanted&8) {
            director_stopping=true;Stop(true,"MMD load");director_stopping=false;
            std::vector<Part> parts;
            {std::lock_guard lock(director_mutex);parts=director_parts;}
            director=true;director_duration.store(0.0);director_phase.store(DirectorLoading);
            if(!BeginLoad(parts))Stop(true,"MMD load unavailable");
        } else if(wanted&1) {
            if(Playing()||Loading()){Stop(true,"play/stop key");return;}
            if(!BeginLoad({Part{applied->file,{}}}))Stop(true,"load unavailable");
        }
        if(!actors[0].Bound())return;
        Validate();
        if(!actors[0].Bound())return;
        if(Loading()) {
            for(int i=0;i<kMaxActors;++i) {
                Actor& actor=actors[i];
                if(!actor.loading)continue;
                switch(EiemBody::LoadStatus(i)) {
                case EiemBody::LoadState::Ready: actor.loading=false;break;
                case EiemBody::LoadState::Loading: break;
                default:
                    Log("Character VMD file rejected for dancer "+std::to_string(i+1)+" (see BetterEndfield.EiemBody.log).");
                    if(i==0){Stop(true,"load failed");return;}
                    Release(i,true,"load failed");break;
                }
            }
            if(!Loading())Started();
            return;
        }
        if((wanted&2) && !director) {clock.Pause(!clock.Paused(),NowSeconds());Log(clock.Paused()?"Character VMD paused.":"Character VMD resumed.");}
        PublishClock();
    }
};
Runtime runtime;
} // namespace CharacterMotion
void PumpCharacterMotion(bool allow_capture=true) {
    if(CharacterMotion::stopping.load(std::memory_order_acquire)||CharacterMotion::in_callback)return;
    std::lock_guard lock(CharacterMotion::gate);
    if(CharacterMotion::stopping.load(std::memory_order_acquire))return;
    if(!allow_capture && CharacterMotion::runtime.thread_id==0)return;
    struct Guard {Guard(){CharacterMotion::in_callback=true;}~Guard(){CharacterMotion::in_callback=false;}} guard;
    try {CharacterMotion::runtime.Pump();}
    catch(const std::exception& e){CharacterMotion::runtime.Stop(true,"exception");Log(std::string("Character VMD error: ")+e.what());}
    catch(...){CharacterMotion::runtime.Stop(true,"exception");Log("Character VMD error: unknown exception.");}
}
void PublishCharacterConfiguration(const CameraConfiguration& value) {
    auto next=std::make_shared<CharacterMotion::Config>();
    // EIEM drives the whole rig; face and eyes ride on the body session.
    next->enabled=value.enabled&&value.vmd_body_enabled;
    next->face=value.vmd_face_enabled;next->terrain=value.vmd_terrain_enabled;next->motion_scale=value.vmd_motion_scale;
    next->cloth=static_cast<EiemBody::ClothMode>(std::clamp(value.vmd_cloth_mode,0,2));
    if(next->enabled && !g_original_tail_late_tick
#if defined(__ANDROID__)
        && !betterendfield::HasAndroidFrameBridge()
#endif
    ) {
        next->enabled=false;Log("Character VMD unavailable: a successfully installed CameraManager.TailLateTick is required.");
    }
    next->file=value.vmd_motion_file;next->loop=value.vmd_motion_loop;
    std::atomic_store_explicit(&CharacterMotion::config, std::shared_ptr<const CharacterMotion::Config>(next), std::memory_order_release);
    g_character_preview_enabled.store(next->enabled,std::memory_order_release);
}
void StopCharacterMotion() {
    CharacterMotion::stopping.store(true,std::memory_order_release);
    std::lock_guard lock(CharacterMotion::gate);
    // Shutdown may run on Host's worker. Never touch Transform/Animator APIs here.
    CharacterMotion::runtime.Stop(false,"shutdown");
    EiemBody::Shutdown();
}

void RequestCharacterMotion(unsigned bits) {CharacterMotion::requests.fetch_or(bits,std::memory_order_release);}
// parts[0] drives the controlled character, parts[1..3] the next squad members.
void RequestCharacterDirectorLoad(const std::vector<CharacterMotion::Part>& parts) {
    {std::lock_guard lock(CharacterMotion::director_mutex);CharacterMotion::director_parts=parts;}
    CharacterMotion::director_phase.store(CharacterMotion::DirectorLoading);
    RequestCharacterMotion(8);
}
void RequestCharacterDirectorLoad(const std::string& motion,const std::string& face) {
    RequestCharacterDirectorLoad(std::vector<CharacterMotion::Part>{{motion,face}});
}
void StartCharacterMotion() {CharacterMotion::stopping.store(false,std::memory_order_release);}
