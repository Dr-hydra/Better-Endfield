// Instance-owned bridge. Every engine entry is resolved by name and signature.
// The private source mesh stays pinned; only an owned clone is deformed.
namespace live_cloth {
namespace CD=BetterEndfieldNext::CustomModel::ClothDisplay;
struct Display {
    void *renderer=nullptr,*source=nullptr,*mesh=nullptr,*pelvis=nullptr,*root=nullptr,*donor=nullptr;
    BetterEndfieldNext::CustomModel::BemComponent geometry;
    Json authored;
    Json donor_before;
    std::vector<void*> bones;
    std::vector<CD::M> bindposes;
    std::array<std::vector<int>,2> authored_to_native;
    std::array<Json,2> teams;
    CD::M pelvis_bind{};
    std::vector<float> positions;
    std::vector<uint32_t> frames;
    std::shared_ptr<void> source_pin;
    uint64_t writes=0;
    bool bound=false,prepared=false;
    bool show_rest=false;
    bool force_gpu_upload=false;
    int demo_displacement_mm=0;
    int thickness_mm=-1;
};
std::unique_ptr<Display> display;
HookBroker broker;
using UpdateFn=void(*)(void*,const MethodInfo*);
UpdateFn original_update=nullptr;
void* stub=nullptr;
bool update_installed=false;

bool Alive(Scope& s,void* p) {
    if(!p)return false;void* a[]{p};
    return CandidateBool(s,s.Method(kUnity,"UnityEngine","Object","op_Implicit","UnityEngine.Object","System.Boolean",1),nullptr,a);
}
template<class T> T Value(Scope& s,ResolvedMethod m,void* self,void** args=nullptr) {
    auto box=s.Call(m,self,args);auto ptr=runtime->Unbox(box);CandidateNeed(ptr,"live cloth value missing");
    T result{};std::memcpy(&result,ptr,sizeof(result));return result;
}
CD::M Matrix(Scope& s,ResolvedMethod m,void* self) {
    auto f=Value<std::array<float,16>>(s,m,self);CD::M out{};
    for(int i=0;i<16;++i){CandidateNeed(std::isfinite(f[i]),"live cloth matrix invalid");out[i]=f[i];}
    return out;
}
std::string Name(Scope& s,void* p) {
    if(!p)return {};
    return s.String(s.Call(s.Method(kUnity,"UnityEngine","Object","get_name","","System.String",0),p));
}
void* GameObject(Scope& s,void* component) {
    return s.Call(s.Method(kUnity,"UnityEngine","Component","get_gameObject","","UnityEngine.GameObject",0),component);
}
void* Transform(Scope& s,void* component) {
    return s.Call(s.Method(kUnity,"UnityEngine","Component","get_transform","","UnityEngine.Transform",0),component);
}
int Length(Scope& s,void* a) {return s.Int(s.Method("mscorlib.dll","System","Array","get_Length","","System.Int32",0),a);}
void* Item(Scope& s,void* a,int i) {void* p[]{&i};return s.Call(s.Method("mscorlib.dll","System","Array","GetValue","System.Int32","System.Object",1),a,p);}
void* Children(Scope& s,void* go,const char* assembly,const char* ns,const char* cls) {
    auto type=runtime->ResolveClass(assembly,ns,cls);CandidateNeed(type.type_object,"live component type missing");
    bool inactive=true;void* args[]{type.type_object,&inactive};
    return s.Call(s.Method(kUnity,"UnityEngine","GameObject","GetComponentsInChildren","System.Type|System.Boolean","UnityEngine.Component[]",2),go,args);
}
void Bind(Scope& s,Display& d) {
    auto& c=*candidate;contact_hook::Complete(s);
    for(int l=0;l<2;++l) {
        int id=CandidateNumber(s,s.Field(c.layers[l].process,"ClothProcess","<TeamId>k__BackingField"));
        CandidateNeed(id>0,"live team not ready");d.teams[l]=CandidateTeam(s,id,l==0);
        const auto base=CandidateParticleArray(s,d.teams[l],"basePosArray");
        const auto matrix=Matrix(s,s.Method(kUnity,"UnityEngine","Transform","get_localToWorldMatrix","","UnityEngine.Matrix4x4",0),d.pelvis);
        const auto& points=d.authored["layers"][l]["positions"];
        CandidateNeed(base.size()==points.size(),"live proxy was reduced or expanded");
        std::set<int> used;double maximum=0;
        for(const auto& p:points) {
            auto expected=CD::Point(matrix,CD::Point(d.pelvis_bind,{p[0],p[1],p[2]}));
            int best=-1;double distance=1e9;
            for(int n=0;n<int(base.size());++n) {
                CD::V v{base[n][0],base[n][1],base[n][2]};double error=CD::Dot(v-expected,v-expected);
                if(error<distance){distance=error;best=n;}
            }
            CandidateNeed(best>=0&&distance<0.000025&&used.insert(best).second,"live particle correspondence not bijective");
            d.authored_to_native[l].push_back(best);maximum=std::max(maximum,std::sqrt(distance));
        }
        c.result["live_mapping"].push_back({{"layer",l?"outer":"inner"},{"count",points.size()},{"max_error_m",maximum},
            {"authored_to_native",d.authored_to_native[l]}});
    }
    d.prepared=true;c.result["coordinate_space"]="live-pelvis-bindpose";
}
void Tick(Scope& s) {
    if(!display||!candidate||candidate->finished||candidate->destroying)return;
    auto& d=*display;auto& c=*candidate;
    CandidateNeed(Alive(s,d.renderer)&&Alive(s,d.root)&&Alive(s,d.pelvis),"live actor retired");
    auto current=s.Call(s.Method(kUnity,"UnityEngine","SkinnedMeshRenderer","get_sharedMesh","","UnityEngine.Mesh",0),d.renderer);
    CandidateNeed(current==(d.bound&&!d.show_rest?d.mesh:d.source),"live actor mesh changed; release physics ownership");
    auto active=s.Method(kUnity,"UnityEngine","GameObject","get_activeInHierarchy","","System.Boolean",0);
    CandidateNeed(CandidateBool(s,active,d.root),"live actor inactive; release physics ownership");
    if(d.show_rest)return;
    if(!d.prepared) {
        for(auto& l:c.layers)if(!CandidateBool(s,s.Method(kCloth,"BeyondDynamicBone","ClothProcess","IsRunning","","System.Boolean",0),l.process))return;
        Bind(s,d);
    }
    CandidateNeed(!contact_hook::fault.load(),"live contact adapter rejected job");
    contact_hook::Complete(s);
    std::vector<CD::M> palette;palette.reserve(d.bones.size());
    auto world=s.Method(kUnity,"UnityEngine","Transform","get_localToWorldMatrix","","UnityEngine.Matrix4x4",0);
    const auto rest_world=CD::Multiply(Matrix(s,world,d.pelvis),d.pelvis_bind);
    for(size_t i=0;i<d.bones.size();++i)palette.push_back(CD::Multiply(Matrix(s,world,d.bones[i]),d.bindposes[i]));
    size_t changed=0;double max_motion=0;
    const bool snapshot=d.writes%30==0;
    Json frame;
    if(snapshot)frame={{"frame",d.writes},{"layers",Json::array()},{"display_world_vertices",Json::array()},
        {"renderer_visible",CandidateBool(s,s.Method(kUnity,"UnityEngine","Renderer","get_isVisible","","System.Boolean",0),d.renderer)},
        {"renderer_holds_owned_mesh",current==d.mesh}};
    for(int l=0;l<2;++l) {
        const auto native=CandidateParticleArray(s,d.teams[l],"dispPosArray");
        if(snapshot)frame["layers"].push_back({{"display",native}});
        std::vector<CD::V> points;points.reserve(d.authored_to_native[l].size());
        for(int i:d.authored_to_native[l]){CandidateNeed(i>=0&&i<int(native.size()),"live particle disappeared");points.push_back({native[i][0],native[i][1],native[i][2]});}
        const auto& layer=d.authored["layers"][l];
        for(const auto& binding:layer["display_bindings"]) {
            const int v=binding.at("vertex");CD::V desired{};
            if(binding.contains("proxy_vertex"))desired=points.at(binding.at("proxy_vertex").get<int>());
            else {
                auto tri=layer["triangles"].at(binding.at("triangle").get<int>());
                const auto& bary=binding.at("barycentric");const auto& off=binding.at("rest_offset_tbn");
                // Transport the authored triangle offset into pelvis space first,
                // including nonuniform scale, then into the simulated frame.
                const auto& rest=layer["positions"];
                auto rp=[&](int i){const auto& p=rest[i];return CD::V{p[0],p[1],p[2]};};
                auto a=rp(tri[0]),b=rp(tri[1]),cc=rp(tri[2]);
                auto offset=CD::TransportOffset(rest_world,a,b,cc,{off[0],off[1],off[2]});
                desired=CD::Follow(points.at(tri[0].get<int>()),points.at(tri[1].get<int>()),points.at(tri[2].get<int>()),
                    {bary[0],bary[1],bary[2]},offset);
            }
            std::array<uint16_t,4> weights{};std::array<uint8_t,4> indices{};
            auto packed=d.geometry.streams[2].data()+size_t(v)*12;std::memcpy(weights.data(),packed,8);std::memcpy(indices.data(),packed+8,4);
            const auto skin=CD::Skin(palette,weights,indices);auto local=CD::Point(CD::Inverse(skin),desired);
            float rest[3];std::memcpy(rest,d.geometry.streams[0].data()+size_t(v)*16,12);
            const auto original=CD::Point(skin,{rest[0],rest[1],rest[2]});double movement=std::sqrt(CD::Dot(desired-original,desired-original));
            if(d.demo_displacement_mm) {
                const double fade=l?std::clamp((1.091-double(rest[2]))/.05,0.0,1.0):0;
                desired=original+CD::V{0,d.demo_displacement_mm*.001*fade,0};
                local=CD::Point(CD::Inverse(skin),desired);
                movement=std::sqrt(CD::Dot(desired-original,desired-original));
            }
            CandidateNeed(std::isfinite(movement)&&movement<0.7,"live cloth displacement exceeds safety bound");
            max_motion=std::max(max_motion,movement);
            d.positions[size_t(v)*3]=float(local.x);d.positions[size_t(v)*3+1]=float(local.y);d.positions[size_t(v)*3+2]=float(local.z);++changed;
            if(snapshot){auto actual=CD::Point(skin,{float(local.x),float(local.y),float(local.z)});
                frame["display_world_vertices"].push_back({v,actual.x,actual.y,actual.z});}
        }
    }
    CandidateNeed(AndroidUpdateMeshPackedGeometry(d.mesh,d.positions,d.frames),"live position write rejected");
    if(!d.bound||d.force_gpu_upload) {
        bool unreadable=false;void* a[]{&unreadable};
        s.Call(s.Method(kUnity,"UnityEngine","Mesh","UploadMeshData","System.Boolean","System.Void",1),d.mesh,a);
        c.result["explicit_gpu_submission"]=true;
    }
    if(d.writes==0) {
        BetterEndfieldNext::CustomModel::BemComponent readback;
        CandidateNeed(AndroidReadMeshGeometry(d.mesh,readback),"live display mesh unreadable");
        auto expected=d.geometry.streams;
        for(size_t v=0;v<d.frames.size();++v)std::memcpy(expected[0].data()+v*16,d.positions.data()+v*3,12);
        CandidateNeed(readback.attributes==d.geometry.attributes&&readback.streams==expected&&readback.indices==d.geometry.indices,
            "live display readback changed packed layout or unrelated bytes");
        c.result["live_mesh_readback_verified"]=true;
    }
    if(!d.bound) {
        void* a[]{d.mesh};s.Call(s.Method(kUnity,"UnityEngine","SkinnedMeshRenderer","set_sharedMesh","UnityEngine.Mesh","System.Void",1),d.renderer,a);
        CandidateNeed(s.Call(s.Method(kUnity,"UnityEngine","SkinnedMeshRenderer","get_sharedMesh","","UnityEngine.Mesh",0),d.renderer)==d.mesh,
            "live display publication readback failed");
        d.bound=true;c.result["render_binding_applied"]=true;
        if(snapshot)frame["renderer_holds_owned_mesh"]=true;
    }
    ++d.writes;c.result["display_frames_written"]=d.writes;c.result["display_vertices_written"]=changed;
    c.result["max_display_motion_m"]=max_motion;
    c.result["status"]="live_display_running";
    c.result["demo_displacement_mm"]=d.demo_displacement_mm;
    if(snapshot)c.result["live_frame"]=std::move(frame);
}
void DisplayTest(int displacement,bool upload) {
    CandidateNeed(displacement>=0&&displacement<=120,"display demonstration displacement out of range");
    if(display){display->demo_displacement_mm=displacement;display->force_gpu_upload=upload;}
}
void ShowRest(Scope& s,bool requested) {
    if(!display||!candidate||candidate->finished||candidate->destroying||!display->bound||display->show_rest==requested)return;
    auto& d=*display;
    auto current=s.Call(s.Method(kUnity,"UnityEngine","SkinnedMeshRenderer","get_sharedMesh","","UnityEngine.Mesh",0),d.renderer);
    CandidateNeed(current==(d.show_rest?d.source:d.mesh),"comparison mesh ownership lost");
    void* args[]{requested?d.source:d.mesh};s.Call(s.Method(kUnity,"UnityEngine","SkinnedMeshRenderer","set_sharedMesh","UnityEngine.Mesh","System.Void",1),d.renderer,args);
    d.show_rest=requested;
    candidate->result["show_rest_mesh"]=requested;
}
void ContactPolicy(Scope& s,int millimeters) {
    if(!display||!candidate||candidate->finished||candidate->destroying||display->thickness_mm==millimeters)return;
    CandidateNeed(millimeters>=1&&millimeters<=16,"cloth contact thickness out of range");
    contact_hook::Complete(s);
    for(auto& layer:candidate->layers) {
        auto self=s.Field(layer.data,"ClothSerializeData","selfCollisionConstraint");
        auto original=s.Field(display->donor,"ClothSerializeData","selfCollisionConstraint");
        auto curve=s.Field(self,"SelfCollisionConstraint.SerializeData","surfaceThickness");
        auto original_curve=s.Field(original,"SelfCollisionConstraint.SerializeData","surfaceThickness");
        CandidateNeed(curve&&original_curve&&curve!=original_curve,"contact curve aliases original solver");
        CandidatePatch(s,self,{{"clothMass",0.5},{"surfaceThickness",{{"useCurve",false},{"value",millimeters*.001}}}});
        auto observed=Serialize(s,self);
        CandidateNeed(std::abs(observed["surfaceThickness"]["value"].get<double>()-millimeters*.001)<1e-6&&
            !observed["surfaceThickness"]["useCurve"].get<bool>(),"cloth contact thickness readback failed");
        s.Call(s.Method(kCloth,"BeyondDynamicBone","BeyondBoneCloth","SetParameterChange","","System.Void",0),layer.cloth);
    }
    CandidateNeed(Serialize(s,display->donor)==display->donor_before,"contact policy changed original solver");
    display->thickness_mm=millimeters;candidate->result["contact_thickness_mm_per_layer"]=millimeters;
}
void ContactCounts(Scope& s) {
    if(!display||!candidate||candidate->finished||candidate->destroying)return;
    contact_hook::Complete(s);Json counts=Json::array();
    for(int i=0;i<2;++i) {
        auto box=s.Root(runtime->ReadFieldObject(contact_hook::list_fields[i],contact_hook::manager));auto ptr=runtime->Unbox(box);
        CandidateNeed(ptr,"contact list readback missing");
        BetterEndfieldNext::CustomModel::ClothContact::Container list{};std::memcpy(&list,ptr,sizeof(list));
        CandidateNeed(list.data,"contact list storage missing");
        int32_t length=0;std::memcpy(&length,reinterpret_cast<void*>(list.data+contact_hook::length_offset),sizeof(length));
        CandidateNeed(length>=0&&length<1048576,"contact list length invalid");counts.push_back(length);
    }
    candidate->result["native_contact_list_lengths"]=std::move(counts);
    Json parameters=Json::array();
    auto manager=s.Call(s.Method(kCloth,"BeyondDynamicBone","MagicaManager","get_Team","","BeyondDynamicBone.TeamManager",0),nullptr);
    for(auto& layer:candidate->layers) {
        int team=CandidateNumber(s,s.Field(layer.process,"ClothProcess","<TeamId>k__BackingField"));
        if(team<=0)continue;void* args[]{&team};
        auto p=s.Call(s.Method(kCloth,"BeyondDynamicBone","TeamManager","GetParametersRef","System.Int32","BeyondDynamicBone.ClothParameters&",1),manager,args);
        parameters.push_back(Serialize(s,s.Field(p,"ClothParameters","selfCollisionConstraint")));
    }
    candidate->result["effective_contact_parameters"]=std::move(parameters);
}
void Update(void* self,const MethodInfo* method) {
    original_update(self,method);
    if(!display||!candidate||runtime->CurrentThread()!=contact_hook::owner_thread)return;
    try{Scope s;Tick(s);}catch(const std::exception& e) {
        const std::string why=e.what();
        const bool retired=why=="live actor retired"||why=="live actor inactive; release physics ownership"||
            why=="live actor mesh changed; release physics ownership";
        candidate->result["status"]=retired?"actor_retired":"failed";
        candidate->result[retired?"retirement_reason":"error"]=why;candidate->finished=true;
        LogError("bem_cloth_live",e.what());
    }
}
void InstallUpdate(Scope& s) {
    if(update_installed)return;
    auto method=s.Method(kCloth,"BeyondDynamicBone","ClothManager","OnAfterLateUpdate","","System.Void",0);
    CandidateNeed(method.entry,"named cloth display update boundary unavailable");
    std::string error;CandidateNeed(broker.Initialize(error),"cloth display broker unavailable");
    CandidateNeed(broker.Install("betterendfieldnext.custom_model.cloth.display",method.entry,reinterpret_cast<void*>(&Update),
        reinterpret_cast<void**>(&original_update),stub,error),"cloth display hook installation failed");
    update_installed=true;
}
}

void LiveDetach(Scope& s) {
    using namespace live_cloth;
    if(display) {
        auto& d=*display;
        if(d.bound&&Alive(s,d.renderer)) {
            auto current=s.Call(s.Method(kUnity,"UnityEngine","SkinnedMeshRenderer","get_sharedMesh","","UnityEngine.Mesh",0),d.renderer);
            if(current==d.mesh&&Alive(s,d.source)) {
                void* a[]{d.source};s.Call(s.Method(kUnity,"UnityEngine","SkinnedMeshRenderer","set_sharedMesh","UnityEngine.Mesh","System.Void",1),d.renderer,a);
                if(candidate)candidate->result["display_binding_restored"]=true;
            }
        }
        if(Alive(s,d.mesh)){void* a[]{d.mesh};s.Call(s.Method(kUnity,"UnityEngine","Object","Destroy","UnityEngine.Object","System.Void",1),nullptr,a);}
        display.reset();
    }
    if(update_installed) {
        CandidateNeed(broker.Remove(stub),"cloth display hook release failed");
        update_installed=false;original_update=nullptr;stub=nullptr;
    }
}

void LiveDiscoverAndStart(Scope& s,const Json& authored,const Json& controls) {
    using namespace live_cloth;
    const auto scan_started=std::chrono::steady_clock::now();
    auto type=runtime->ResolveClass(kUnity,"UnityEngine","SkinnedMeshRenderer");CandidateNeed(type.type_object,"live renderer class unavailable");
    void* a[]{type.type_object};auto renderers=s.Call(s.Method(kUnity,"UnityEngine","Resources","FindObjectsOfTypeAll","System.Type","UnityEngine.Object[]",1),nullptr,a);
    int count=Length(s,renderers);CandidateNeed(count>=0&&count<=65536,"live renderer budget invalid");
    void *receiver=nullptr,*root=nullptr;
    Json discovery={{"renderer_count",count},{"receivers",Json::array()}};
    BetterEndfieldNext::CustomModel::BemComponent geometry;
    for(int i=0;i<count;++i) {
        auto r=Item(s,renderers,i);auto name=Name(s,r);
        if(name!="S_actor_aglina_cloth_01_lod0"&&name!="S_actor_aglina_cloth_01_lod1")continue;
        auto visible=CandidateBool(s,s.Method(kUnity,"UnityEngine","Renderer","get_isVisible","","System.Boolean",0),r);
        auto& row=discovery["receivers"].emplace_back(Json{{"name",name},{"visible",visible}});
        // Loading/prefab instances do not provide a display receiver. Wait for
        // a world camera to render the actor before starting native construction.
        if(!visible)continue;
        auto go=GameObject(s,r);
        if(!CandidateBool(s,s.Method(kUnity,"UnityEngine","GameObject","get_activeInHierarchy","","System.Boolean",0),go)||
            !CandidateBool(s,s.Method(kUnity,"UnityEngine","Renderer","get_enabled","","System.Boolean",0),r))continue;
        auto scene=s.Call(s.Method(kUnity,"UnityEngine","GameObject","get_scene","","UnityEngine.SceneManagement.Scene",0),go);
        auto scene_ptr=runtime->Unbox(scene);if(!scene_ptr||!CandidateBool(s,s.Method(kUnity,"UnityEngine.SceneManagement","Scene","IsValid","","System.Boolean",0),scene_ptr)||
            !CandidateBool(s,s.Method(kUnity,"UnityEngine.SceneManagement","Scene","get_isLoaded","","System.Boolean",0),scene_ptr))continue;
        void* ancestor=Transform(s,r);void* found=nullptr;
        row["ancestors"]=Json::array();
        for(int depth=0;ancestor&&depth<64;++depth) {
            auto label=Name(s,ancestor);
            if(depth<16)row["ancestors"].push_back(label);
            const auto canonical=BetterEndfieldNext::CustomModel::ResourceBaseName(label);
            if(canonical=="chr_0013_aglina_postmodel"){found=GameObject(s,ancestor);break;}
            if(canonical=="chr_0013_aglina_uimodel")break;
            ancestor=s.Call(s.Method(kUnity,"UnityEngine","Transform","get_parent","","UnityEngine.Transform",0),ancestor);
        }
        if(!found)continue;
        auto mesh=s.Call(s.Method(kUnity,"UnityEngine","SkinnedMeshRenderer","get_sharedMesh","","UnityEngine.Mesh",0),r);
        BetterEndfieldNext::CustomModel::BemComponent g;
        if(!AndroidReadMeshGeometry(mesh,g)||g.info.vertex_count!=authored["target"]["vertex_count"]||g.info.stride0!=16||g.info.stride1!=12||g.info.stride2!=12)continue;
        CandidateNeed(!receiver,"multiple active Rin receivers; refuse ambiguous actor");receiver=r;root=found;geometry=std::move(g);
    }
    discovery["scan_ms"]=std::chrono::duration_cast<std::chrono::milliseconds>(std::chrono::steady_clock::now()-scan_started).count();
    report["live_discovery"]=std::move(discovery);
    if(!receiver)return; // Game may still be at login or loading.
    if(!ClaimCandidateRequest())return;
    auto d=std::make_unique<Display>();d->renderer=receiver;d->root=root;d->geometry=std::move(geometry);d->authored=authored;
    d->demo_displacement_mm=controls.value("demo_displacement_mm",0);
    d->force_gpu_upload=controls.value("force_gpu_upload",false);
    CandidateNeed(d->demo_displacement_mm>=0&&d->demo_displacement_mm<=120,"display demonstration displacement out of range");
    d->source=s.Call(s.Method(kUnity,"UnityEngine","SkinnedMeshRenderer","get_sharedMesh","","UnityEngine.Mesh",0),receiver);
    d->source_pin=BetterEndfieldNext::CustomModel::PinModelMeshForCloth(d->source);
    CandidateNeed(bool(d->source_pin),"live source mesh native pin unavailable");
    auto bones=s.Call(s.Method(kUnity,"UnityEngine","SkinnedMeshRenderer","get_bones","","UnityEngine.Transform[]",0),receiver);
    auto poses=s.Call(s.Method(kUnity,"UnityEngine","Mesh","get_bindposes","","UnityEngine.Matrix4x4[]",0),d->source);
    int n=Length(s,bones);CandidateNeed(n>0&&n<=256&&Length(s,poses)==n,"live bone palette mismatch");
    for(int i=0;i<n;++i) {
        auto bone=Item(s,bones,i),box=Item(s,poses,i);auto ptr=runtime->Unbox(box);CandidateNeed(ptr&&Alive(s,bone),"live palette unavailable");
        std::array<float,16> f{};std::memcpy(f.data(),ptr,sizeof(f));CD::M m{};for(int k=0;k<16;++k)m[k]=f[k];
        CD::Inverse(m);d->bones.push_back(bone);d->bindposes.push_back(m);
        if(Name(s,bone)=="Bip001_Pelvis"){CandidateNeed(!d->pelvis,"duplicate pelvis");d->pelvis=bone;d->pelvis_bind=m;}
    }
    CandidateNeed(d->pelvis,"live pelvis missing");
    std::set<int> bound;
    for(const auto& layer:authored["layers"])for(const auto& b:layer["display_bindings"]) {
        int v=b.at("vertex");CandidateNeed(v>=0&&v<int(d->geometry.info.vertex_count)&&bound.insert(v).second,"display vertex ownership invalid");
        const auto& ps=layer["positions"];CD::V expected{};
        if(b.contains("proxy_vertex")){const auto& p=ps.at(b["proxy_vertex"].get<int>());expected={p[0],p[1],p[2]};}
        else {const auto& t=layer["triangles"].at(b["triangle"].get<int>());auto p=[&](int i){return CD::V{ps[i][0],ps[i][1],ps[i][2]};};const auto& w=b["barycentric"];const auto& o=b["rest_offset_tbn"];expected=CD::Follow(p(t[0]),p(t[1]),p(t[2]),{w[0],w[1],w[2]},{o[0],o[1],o[2]});}
        float rest[3];std::memcpy(rest,d->geometry.streams[0].data()+size_t(v)*16,12);
        CandidateNeed(CD::Dot(expected-CD::V{rest[0],rest[1],rest[2]},expected-CD::V{rest[0],rest[1],rest[2]})<1e-8,"live geometry differs from physics authoring");
    }
    auto cloths=Children(s,root,kCloth,"BeyondDynamicBone","BeyondBoneCloth");int nc=Length(s,cloths);CandidateNeed(nc<=64,"live cloth component budget invalid");void* donor=nullptr;
    for(int i=0;i<nc;++i){auto cloth=Item(s,cloths,i);if(Name(s,cloth)=="MC_coat")donor=s.Call(s.Method(kCloth,"BeyondDynamicBone","BeyondBoneCloth","get_SerializeData","","BeyondDynamicBone.ClothSerializeData",0),cloth);}
    CandidateNeed(donor,"live coat donor unavailable");
    d->donor=donor;d->donor_before=Serialize(s,donor);
    Json local=authored;
    for(auto& layer:local["layers"]) {
        layer["live_space"]=true;
        for(auto& p:layer["positions"]){auto v=CD::Point(d->pelvis_bind,{p[0],p[1],p[2]});p={v.x,v.y,v.z};}
    }
    // Build under the live pelvis: fixed proxy points follow actual animation.
    CandidateStart(s,donor,local,GameObject(s,d->pelvis));auto& c=*candidate;
    c.Hold(d->source);c.Hold(d->renderer);c.Hold(d->root);c.Hold(d->pelvis);c.Hold(d->donor);for(auto b:d->bones)c.Hold(b);
    d->positions.resize(size_t(d->geometry.info.vertex_count)*3);d->frames.resize(d->geometry.info.vertex_count);
    for(size_t v=0;v<d->frames.size();++v){std::memcpy(d->positions.data()+v*3,d->geometry.streams[0].data()+v*16,12);std::memcpy(&d->frames[v],d->geometry.streams[0].data()+v*16+12,4);}
    display=std::move(d);
    void* clone[]{display->source};display->mesh=s.Call(s.Method(kUnity,"UnityEngine","Object","Instantiate","UnityEngine.Object","UnityEngine.Object",1),nullptr,clone);
    c.Hold(display->mesh);
    s.Call(s.Method(kUnity,"UnityEngine","Mesh","MarkDynamic","","System.Void",0),display->mesh);
    InstallUpdate(s);c.result["source_native_pin_active"]=true;
    c.result["stage"]="waiting_for_live_teams";c.result["coordinate_space"]="live-pelvis-bindpose";
    c.result["display_normals"]="original-packed-tbn";c.result["body_collision_enabled"]=false;
}
