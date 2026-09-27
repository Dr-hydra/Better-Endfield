// PC-only, opt-in character preview. Called only after CameraManager.TailLateTick.
// No extra hook, root/controller write, Animator disable, private SMC offset or
// FinalIK call. A changed/destroyed hierarchy stops the session instead of rebinding.
namespace CharacterMotion {
using namespace CharacterPose;
constexpr char kOwner[] = "betterendfield.camera.character";
struct Config {
    bool enabled=false, body=false, eyes=false, morphs=false, loop=false;
    float weight=1;
    std::string file;
};
std::atomic<std::shared_ptr<const Config>> config{std::make_shared<const Config>()};
std::atomic_uint requests{0}; // 1 play/stop, 2 pause/resume, 4 stop
std::atomic_bool stopping{false};
std::recursive_mutex gate;
thread_local bool in_callback=false;

struct Method {
    BE_MethodDescriptorV1 spec;
    BE_ResolvedMethodV1 value{};
};
constexpr const char* kCore="UnityEngine.CoreModule.dll";
Method methods[]{
    {{kCore,"UnityEngine","Object","op_Implicit","UnityEngine.Object","System.Boolean",1}},
    {{kCore,"UnityEngine","Component","get_transform",nullptr,"UnityEngine.Transform",0}},
    {{kCore,"UnityEngine","Component","get_gameObject",nullptr,"UnityEngine.GameObject",0}},
    {{kCore,"UnityEngine","GameObject","GetComponentsInChildren","System.Type|System.Boolean","UnityEngine.Component[]",2}},
    {{"mscorlib.dll","System","Array","GetLength","System.Int32","System.Int32",1}},
    {{"mscorlib.dll","System","Array","GetValue","System.Int32","System.Object",1}},
    {{kCore,"UnityEngine","Transform","get_localRotation",nullptr,"UnityEngine.Quaternion",0}},
    {{kCore,"UnityEngine","Transform","set_localRotation","UnityEngine.Quaternion","System.Void",1}},
    {{kCore,"UnityEngine","Transform","get_rotation",nullptr,"UnityEngine.Quaternion",0}},
    {{kCore,"UnityEngine","Transform","IsChildOf","UnityEngine.Transform","System.Boolean",1}},
    {{kCore,"UnityEngine","Transform","get_childCount",nullptr,"System.Int32",0}},
    {{kCore,"UnityEngine","Transform","GetChild","System.Int32","UnityEngine.Transform",1}},
    {{kCore,"UnityEngine","Object","get_name",nullptr,"System.String",0}},
    {{kCore,"UnityEngine","Time","get_frameCount",nullptr,"System.Int32",0}},
    {{kCore,"UnityEngine","SkinnedMeshRenderer","get_sharedMesh",nullptr,"UnityEngine.Mesh",0}},
    {{kCore,"UnityEngine","Mesh","GetBlendShapeIndex","System.String","System.Int32",1}},
    {{kCore,"UnityEngine","Mesh","get_blendShapeCount",nullptr,"System.Int32",0}},
    {{kCore,"UnityEngine","SkinnedMeshRenderer","GetBlendShapeWeight","System.Int32","System.Single",1}},
    {{kCore,"UnityEngine","SkinnedMeshRenderer","SetBlendShapeWeight","System.Int32|System.Single","System.Void",2}},
};
enum Id {Alive,Transform,GameObject,Children,ArrayLength,ArrayValue,LocalGet,LocalSet,
    WorldGet,IsChild,ChildCount,ChildAt,Name,FrameCount,SharedMesh,ShapeIndex,ShapeCount,WeightGet,WeightSet};
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
void* Model() {
    void* character=Invoke(Contract("player_controller.get_main_character"),nullptr,nullptr);
    void* model=character?Invoke(Contract("entity.get_model_com"),character,nullptr):nullptr;
    return model?Invoke(Contract("base_model_component.get_model_go"),model,nullptr):nullptr;
}
struct Runtime final : Backend {
    CameraFiles::Worker files;
    const BE_PoseLeaseApiV1* leases=nullptr;
    BE_ResolvedClassV1 animator_class{},renderer_class{};
    std::shared_ptr<const Config> applied;
    Session session;
    Clock clock;
    uint64_t generation=0,lease=0;
    DWORD thread_id=0;
    int last_frame=-1;
    void* root=nullptr;
    void* model=nullptr;
    void* model_root=nullptr;
    void* animator=nullptr;
    void* pending_model=nullptr;
    uint32_t pending_pin=0;
    std::vector<uint32_t> pins;
    std::map<void*,void*> meshes;
    bool loading=false,ready=false,morph_ready=false;
    uint32_t frames=0;
    bool Pin(void* object) {
        if(!object || !g_host || !g_host->gchandle_new)return false;
        auto pin=g_host->gchandle_new(g_host->context,object,1);
        if(!pin)return false;
        try {pins.push_back(pin);}catch(...){g_host->gchandle_free(g_host->context,pin);throw;}
        return true;
    }
    void ClearPending() {
        if(pending_pin && g_host && g_host->gchandle_free)g_host->gchandle_free(g_host->context,pending_pin);
        pending_pin=0;pending_model=nullptr;loading=false;
    }
    bool RootValid() {
        if(!AliveObject(root)||!AliveObject(model_root)||!AliveObject(animator)||Call(Transform,animator)!=root)return false;
        bool inside=false;void* args[]{model_root};return Value(IsChild,root,inside,args)&&inside;
    }
    bool Within(void* bone) {
        if(!RootValid() || !AliveObject(bone))return false;
        bool inside=false;void* args[]{root};return Value(IsChild,bone,inside,args)&&inside;
    }
    bool ValidBone(uintptr_t target)override{return Within(reinterpret_cast<void*>(target));}
    bool ReadRotation(uintptr_t target,CharacterPose::Quaternion& out)override {
        if(!Value(LocalGet,reinterpret_cast<void*>(target),out)||!Finite(out))return false;
        out=Vmd::Normalize(out);return true;
    }
    bool WriteRotation(uintptr_t target,CharacterPose::Quaternion value)override {
        if(!Finite(value) || !ValidBone(target))return false;
        void* args[]{&value};bool ok=false;Call(LocalSet,reinterpret_cast<void*>(target),args,&ok);return ok;
    }
    bool ValidMorph(uintptr_t target,int index)override {
        auto renderer=reinterpret_cast<void*>(target);
        auto it=meshes.find(renderer);
        if(it==meshes.end() || !AliveObject(renderer) || !AliveObject(it->second) ||
            Call(SharedMesh,renderer)!=it->second || !Within(Call(Transform,renderer)))return false;
        int count=0;return Value(ShapeCount,it->second,count)&&index>=0&&index<count&&count<=4096;
    }
    bool ReadMorph(uintptr_t target,int index,float& value)override {
        void* args[]{&index};return Value(WeightGet,reinterpret_cast<void*>(target),value,args);
    }
    bool WriteMorph(uintptr_t target,int index,float value)override {
        if(!std::isfinite(value) || !ValidMorph(target,index))return false;
        void* args[]{&index,&value};bool ok=false;Call(WeightSet,reinterpret_cast<void*>(target),args,&ok);return ok;
    }
    void Stop(bool restore,const char* reason) {
        ++generation;
        const bool active=session.Active();
        session.Stop(*this,restore && thread_id==GetCurrentThreadId() && leases && leases->owns(root,kOwner,lease));
        if(lease && leases)leases->release(root,kOwner,lease);
        lease=0;root=nullptr;model=nullptr;model_root=nullptr;animator=nullptr;meshes.clear();
        for(auto pin:pins)if(g_host&&g_host->gchandle_free)g_host->gchandle_free(g_host->context,pin);
        pins.clear();ClearPending();
        if(active)Log(std::string("Character VMD stopped: ")+reason+"; frames="+std::to_string(frames));
        frames=0;
    }
    bool Resolve() {
        if(ready)return true;
        if(!g_host || !g_host->resolve_method || !g_host->resolve_class || !g_host->gchandle_new || !g_host->gchandle_free || !g_host->string_new)return false;
        auto get=reinterpret_cast<BE_GetPoseLeaseApiV1Fn>(GetProcAddress(
            GetModuleHandleW(L"BetterEndfield.Host.dll"),"BetterEndfield_GetPoseLeaseApiV1"));
        leases=get?get():nullptr;
        if(!leases || leases->version!=1 || !leases->acquire || !leases->owns || !leases->release) {
            Log("Character VMD unavailable: install the matching Host with pose leases.");return false;
        }
        bool core=true;morph_ready=true;
        for(size_t i=0;i<std::size(methods);++i) {
            if(g_host->resolve_method(g_host->context,&methods[i].spec,&methods[i].value)!=BE_Result_Ok) {
                if(i<SharedMesh)core=false;else morph_ready=false;
                Log(std::string("Character VMD contract missing: ")+methods[i].spec.class_name+"."+methods[i].spec.method_name);
            }
        }
        core &= g_host->resolve_class(g_host->context,"UnityEngine.AnimationModule.dll","UnityEngine","Animator",&animator_class)==BE_Result_Ok && animator_class.type_object;
        morph_ready &= g_host->resolve_class(g_host->context,kCore,"UnityEngine","SkinnedMeshRenderer",&renderer_class)==BE_Result_Ok && renderer_class.type_object;
        ready=core;return ready;
    }
    bool CooperativeActions() {
        auto actions=GetModuleHandleW(L"BetterEndfield.Actions.dll");
        if(!actions)return true;
        using Version=uint32_t(BE_CALL*)();
        auto version=reinterpret_cast<Version>(GetProcAddress(actions,"BetterEndfield_ActionsPoseLeaseVersionV1"));
        if(version&&version()==1)return true;
        Log("Character VMD refused: Actions DLL lacks the pose-lease handshake; replace Host/Actions/Camera together.");return false;
    }
    std::vector<void*> Components(void* go,void* type,size_t limit) {
        bool inactive=true;void* args[]{type,&inactive};auto array=Call(Children,go,args);
        if(!Pin(array))return {};
        int dim=0,count=0;void* length_args[]{&dim};
        if(!Value(ArrayLength,array,count,length_args)||count<0||size_t(count)>limit)return {};
        std::vector<void*> objects;
        for(int i=0;i<count;++i){void* index_args[]{&i};auto object=Call(ArrayValue,array,index_args);if(!Pin(object))return {};objects.push_back(object);}
        return objects;
    }
    bool Bind(std::shared_ptr<const Vmd::Motion> motion) {
        if(!CooperativeActions() || !AliveObject(pending_model) || Model()!=pending_model)return false;
        model=pending_model;
        if(!Pin(model))return false;
        auto animators=Components(model,animator_class.type_object,32);
        // Selecting the first of several rigs would silently animate a weapon or sub-rig.
        if(animators.size()!=1){Log("Character VMD binding requires exactly one Animator under the current model.");return false;}
        animator=animators.front();
        model_root=Invoke(Contract("unity.game_object.transform"),model,nullptr);
        if(!Pin(model_root))return false;
        root=Call(Transform,animator);
        if(!Pin(root)||!RootValid())return false;
        lease=leases->acquire(root,kOwner);
        if(!lease){Log("Character VMD waiting for no writer: sustained-dash pose already owns this rig. Stop dash and press F8 again.");return false;}
        std::map<std::string,void*> names;
        std::vector<std::pair<void*,unsigned>> pending{{root,0}};
        std::unordered_set<void*> visited;
        for(size_t i=0;i<pending.size();++i) {
            auto [node,depth]=pending[i];
            if(depth>64 || pending.size()>4096 || !visited.insert(node).second || !Pin(node))return false;
            auto name=ReadManagedString(Call(Name,node));
            auto [it,inserted]=names.emplace(name,node);if(!inserted)it->second=nullptr;
            int count=0;if(!Value(ChildCount,node,count)||count<0||count>512)return false;
            for(int n=0;n<count;++n){void* args[]{&n};auto child=Call(ChildAt,node,args);if(!child || !Pin(child))return false;pending.emplace_back(child,depth+1);}
        }
        CharacterPose::Quaternion root_rotation;
        if(!Value(WorldGet,root,root_rotation)||!Finite(root_rotation))return false;
        std::vector<Bone> bones;std::vector<Morph> morphs;size_t body_count=0;
        for(const auto& spec:DefaultRig()) {
            if(spec.eye?!applied->eyes:!applied->body)continue;
            auto it=names.find(spec.target);if(it==names.end()||!it->second)continue;
            Bone bone;bone.target=reinterpret_cast<uintptr_t>(it->second);
            for(const auto& source:spec.sources)if(motion->bones.contains(source))bone.sources.push_back(source);
            if(bone.sources.empty())continue;
            CharacterPose::Quaternion world;
            if(!Value(WorldGet,it->second,world)||!Finite(world))return false;
            bone.basis=CharacterPose::Multiply(CharacterPose::Inverse(world),root_rotation);
            if(!spec.eye)++body_count;
            bones.push_back(std::move(bone));
        }
        if(applied->body && body_count<4) {Log("Character VMD: insufficient mapped body tracks for a preview; at least four channels are required.");return false;}
        if(applied->morphs && morph_ready) {
            for(auto renderer:Components(model,renderer_class.type_object,64)) {
                auto mesh=Call(SharedMesh,renderer);if(!Pin(mesh))continue;
                int count=0;if(!Value(ShapeCount,mesh,count)||count<0||count>4096)continue;
                meshes[renderer]=mesh;
                for(const auto& spec:DefaultMorphs()) {
                    Morph morph;morph.target=reinterpret_cast<uintptr_t>(renderer);
                    for(const auto& source:spec.sources)if(motion->morphs.contains(source))morph.sources.push_back(source);
                    if(morph.sources.empty())continue;
                    auto name=g_host->string_new(g_host->context,spec.target);if(!Pin(name))return false;
                    void* args[]{name};if(!Value(ShapeIndex,mesh,morph.index,args)||morph.index<0||morph.index>=count)continue;
                    morphs.push_back(std::move(morph));
                }
            }
        }
        if(applied->morphs && morphs.empty())Log("Character VMD: no supported named BlendShapes. Private SMC face arrays are intentionally not modified.");
        std::string error;
        const auto bone_count=bones.size(),morph_count=morphs.size();
        // Duration is based on participating tracks, not unrelated camera/light keys.
        uint32_t last=0;
        for(const auto& b:bones)for(const auto& name:b.sources)last=std::max(last,motion->bones.at(name).back().frame);
        for(const auto& m:morphs)for(const auto& name:m.sources)last=std::max(last,motion->morphs.at(name).back().frame);
        if(!session.Start(motion,std::move(bones),std::move(morphs),*this,error)){Log("Character VMD bind failed: "+error);return false;}
        clock.Start(NowSeconds(),last/Vmd::FramesPerSecond,applied->loop);frames=0;
        Log("Character VMD relative FK preview bound: bones="+std::to_string(bone_count)+", morphs="+std::to_string(morph_count)+". Calibrated to current pose/source frame 0; no root translation, leg IK, Twist or cloth writes.");
        return true;
    }
    void Pump() {
        if(!thread_id)thread_id=GetCurrentThreadId();
        if(thread_id!=GetCurrentThreadId())return;
        // Reject foreign callbacks before consuming the one-slot completion.
        auto completed=files.Poll();
        auto next=config.load(std::memory_order_acquire);
        if(next!=applied){Stop(true,"configuration changed");applied=std::move(next);}
        const unsigned wanted=requests.exchange(0,std::memory_order_acq_rel);
        if(!applied->enabled){if(session.Active()||loading)Stop(true,"disabled");return;}
        if(wanted&4){Stop(true,"stop key");return;}
        if(wanted&1) {
            if(session.Active()||loading){Stop(true,"play/stop key");return;}
            if(!Resolve()||!CooperativeActions())return;
            pending_model=Model();
            if(!AliveObject(pending_model)){Log("Character VMD: enter the playable world first.");return;}
            pending_pin=g_host->gchandle_new(g_host->context,pending_model,1);
            if(!pending_pin){ClearPending();return;}
            files.Start();++generation;
            if(!files.Submit({CameraFiles::Kind::LoadMotion,applied->file,generation,{}})) {
                Log("Character VMD file worker busy; retry after pending I/O completes.");ClearPending();
            } else {loading=true;Log("Character VMD loading on file worker.");}
        }
        if(auto& result=completed; result) {
            if(loading && result->generation==generation) {
                if(!result->error.empty()){Log("Character VMD file rejected: "+result->error);ClearPending();}
                else if(!Bind(std::make_shared<const Vmd::Motion>(std::move(result->motion))))Stop(true,"bind unavailable");
                else ClearPending();
            }
        }
        if(!session.Active())return;
        if(!AliveObject(model)||Model()!=model||!RootValid()||!leases->owns(root,kOwner,lease)||!CooperativeActions()) {
            Stop(true,"character/rig/ownership changed");return;
        }
        int frame=0;if(!Value(FrameCount,nullptr,frame)){Stop(true,"frame contract failed");return;}
        if(wanted&2) {clock.Pause(!clock.Paused(),NowSeconds());Log(clock.Paused()?"Character VMD paused.":"Character VMD resumed.");}
        if(frame==last_frame)return;
        last_frame=frame;
        double seconds=0;
        if(!clock.Advance(NowSeconds(),seconds)){Stop(true,"end of clip");return;}
        std::string error;
        if(!session.Apply(seconds*Vmd::FramesPerSecond,applied->weight,*this,error)){Log("Character VMD apply failed: "+error);Stop(true,"write failure");return;}
        ++frames;
    }
};
Runtime runtime;
} // namespace CharacterMotion
void PumpCharacterMotion(bool allow_capture=true) {
#if !defined(__ANDROID__)
    if(CharacterMotion::stopping.load(std::memory_order_acquire)||CharacterMotion::in_callback)return;
    std::lock_guard lock(CharacterMotion::gate);
    if(CharacterMotion::stopping.load(std::memory_order_acquire))return;
    if(!allow_capture && CharacterMotion::runtime.thread_id==0)return;
    struct Guard {Guard(){CharacterMotion::in_callback=true;}~Guard(){CharacterMotion::in_callback=false;}} guard;
    try {CharacterMotion::runtime.Pump();}
    catch(const std::exception& e){CharacterMotion::runtime.Stop(true,"exception");Log(std::string("Character VMD error: ")+e.what());}
    catch(...){CharacterMotion::runtime.Stop(true,"exception");Log("Character VMD error: unknown exception.");}
#endif
}
void PublishCharacterConfiguration(const CameraConfiguration& value) {
    auto next=std::make_shared<CharacterMotion::Config>();
    next->body=value.vmd_body_enabled;next->eyes=value.vmd_eyes_enabled;next->morphs=value.vmd_face_enabled;
    next->enabled=value.enabled&&(next->body||next->eyes||next->morphs);
    if(next->enabled && !g_original_tail_late_tick) {
        next->enabled=false;Log("Character VMD unavailable: a successfully installed CameraManager.TailLateTick is required.");
    }
    next->file=value.vmd_motion_file;next->loop=value.vmd_motion_loop;next->weight=value.vmd_motion_weight;
    CharacterMotion::config.store(next,std::memory_order_release);
    g_character_preview_enabled.store(next->enabled,std::memory_order_release);
}
void StopCharacterMotion() {
    CharacterMotion::stopping.store(true,std::memory_order_release);
    std::lock_guard lock(CharacterMotion::gate);
    // Shutdown may run on Host's worker. Never touch Transform/BlendShape APIs here.
    CharacterMotion::runtime.Stop(false,"shutdown");CharacterMotion::runtime.files.Stop();
}

void RequestCharacterMotion(unsigned bits) {CharacterMotion::requests.fetch_or(bits,std::memory_order_release);}
void StartCharacterMotion() {CharacterMotion::stopping.store(false,std::memory_order_release);}
