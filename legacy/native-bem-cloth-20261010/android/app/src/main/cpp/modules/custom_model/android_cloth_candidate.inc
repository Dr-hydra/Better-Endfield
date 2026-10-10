// Private, invisible native MeshCloth pair experiment. Included inside the
// probe's anonymous namespace. No BEM capability is advertised by this code.
// Only newly created GameObjects, Meshes and configuration objects are written.
struct ClothCandidate {
    struct Layer { void *go=nullptr,*mesh=nullptr,*renderer=nullptr,*cloth=nullptr,*data=nullptr,*process=nullptr; };
    std::vector<uint32_t> handles;
    std::vector<void*> scene_objects;
    void* owner=nullptr;
    std::array<Layer,2> layers{};
    std::vector<void*> body_colliders;
    std::chrono::steady_clock::time_point started;
    bool destroying=false,finished=false;
    bool pair_started=false,pair_retiring=false;
    bool consumer_destroy_sent=false,producer_retire_notified=false;
    int retiring_consumer_team=0;
    int pair_stable_samples=0;
    std::chrono::steady_clock::time_point pair_time;
    Json result={{"render_binding_applied",false},{"original_solver_modified",false},
                 {"collision_effect_verified",false},{"coordinate_space","isolated-y-up-laboratory"}};
    ~ClothCandidate() { for(auto h:handles)runtime->FreeGcHandle(h); }
    void* Hold(void* value) {
        if(!value)throw std::runtime_error("candidate object unavailable");
        auto h=runtime->NewGcHandle(value,false);
        if(!h)throw std::runtime_error("candidate GC root unavailable");
        handles.push_back(h);return value;
    }
};
std::unique_ptr<ClothCandidate> candidate;
bool BoneBodyRegistration(Scope&);

void CandidateNeed(bool ok,const char* message) {if(!ok)throw std::runtime_error(message);}
bool CandidateBool(Scope& s,ResolvedMethod m,void* object,void** args=nullptr) {
    void* value=runtime->Unbox(s.Call(m,object,args));
    CandidateNeed(value,"candidate Boolean result missing");bool out=false;std::memcpy(&out,value,sizeof(out));return out;
}
int CandidateNumber(Scope& s,void* box) {
    CandidateNeed(box,"candidate numeric field missing");
    auto convert=s.Method("mscorlib.dll","System","Convert","ToInt32","System.Object","System.Int32",1);
    void* args[]{box};return s.Int(convert,nullptr,args);
}
int CandidateEnum(Scope& s,const char* type,const char* member) {
    const auto f=runtime->ResolveField(kCloth,"BeyondDynamicBone",type,member);
    CandidateNeed(f.info,"candidate enum member missing");
    return CandidateNumber(s,s.Root(runtime->ReadFieldObject(f,nullptr)));
}
void CandidatePatch(Scope& s,void* object,const Json& json) {
    auto text=s.Root(runtime->NewString(json.dump().c_str()));void* args[]{text,object};
    s.Call(s.Method("UnityEngine.JSONSerializeModule.dll","UnityEngine","JsonUtility","FromJsonOverwrite",
        "System.String|System.Object","System.Void",2),nullptr,args);
}
void CandidateActive(Scope& s,void* go,bool active) {
    void* args[]{&active};s.Call(s.Method(kUnity,"UnityEngine","GameObject","SetActive","System.Boolean","System.Void",1),go,args);
}
void* CandidateGameObject(Scope& s,ClothCandidate& c,const char* label,void* parent) {
    auto cls=runtime->ResolveClass(kUnity,"UnityEngine","GameObject");
    CandidateNeed(cls.info,"GameObject class unavailable");
    auto go=c.Hold(runtime->NewObject(cls.info));c.scene_objects.push_back(go);
    auto name=s.Root(runtime->NewString(label));void* args[]{name};
    s.Call(s.Method(kUnity,"UnityEngine","GameObject",".ctor","System.String","System.Void",1),go,args);
    CandidateActive(s,go,false);
    if(parent) {
        auto get=s.Method(kUnity,"UnityEngine","GameObject","get_transform","","UnityEngine.Transform",0);
        auto t=s.Call(get,go),p=s.Call(get,parent);bool keep_world=false;void* p_args[]{p,&keep_world};
        s.Call(s.Method(kUnity,"UnityEngine","Transform","SetParent","UnityEngine.Transform|System.Boolean","System.Void",2),t,p_args);
    }
    return go;
}
void* CandidateComponent(Scope& s,ClothCandidate& c,void* go,const char* assembly,const char* ns,const char* type) {
    auto cls=runtime->ResolveClass(assembly,ns,type);CandidateNeed(cls.type_object,"candidate component class missing");
    s.Root(cls.type_object);void* args[]{cls.type_object};
    return c.Hold(s.Call(s.Method(kUnity,"UnityEngine","GameObject","AddComponent","System.Type","UnityEngine.Component",1),go,args));
}
int CandidateInstance(Scope& s,void* object) {
    CandidateNeed(object,"Unity instance object unavailable");
    return s.Int(s.Method(kUnity,"UnityEngine","Object","GetInstanceID","","System.Int32",0),object);
}
Json CandidateReference(const Json& pattern,int instance) {
    if(pattern.is_object()&&pattern.contains("m_FileID")&&pattern.contains("m_PathID"))
        return {{"m_FileID",instance},{"m_PathID",0}};
    if(pattern.is_object()&&pattern.contains("instanceID"))return {{"instanceID",instance}};
    throw std::runtime_error("unrecognized runtime Unity reference serialization");
}
void CandidateMesh(Scope& s,ClothCandidate& c,ClothCandidate::Layer& out,const Json& layer) {
    const auto& points=layer.at("positions");const auto& faces=layer.at("triangles");const auto& fixed=layer.at("fixed_vertices");
    CandidateNeed(points.is_array()&&points.size()>=3&&points.size()<=1024,"candidate point budget invalid");
    CandidateNeed(faces.is_array()&&!faces.empty()&&faces.size()<=2048,"candidate face budget invalid");
    CandidateNeed(fixed.is_array()&&!fixed.empty()&&fixed.size()<points.size(),"candidate fixed region invalid");
    std::set<int> anchors;
    for(const auto& f:fixed) {CandidateNeed(f.is_number_integer(),"candidate anchor type invalid");int i=f.get<int>();CandidateNeed(i>=0&&size_t(i)<points.size()&&anchors.insert(i).second,"candidate anchor invalid");}
    std::vector<BetterEndfieldNext::CustomModel::ClothPrototype::Point> checked_points;
    std::vector<BetterEndfieldNext::CustomModel::ClothPrototype::Face> checked_faces;
    std::vector<uint32_t> checked_fixed;
    for(const auto& p:points){CandidateNeed(p.is_array()&&p.size()==3,"candidate vector invalid");checked_points.push_back({p[0].get<float>(),p[1].get<float>(),p[2].get<float>()});}
    for(const auto& f:faces){CandidateNeed(f.is_array()&&f.size()==3,"candidate face invalid");
        BetterEndfieldNext::CustomModel::ClothPrototype::Face face{};
        for(int i=0;i<3;++i){CandidateNeed(f[i].is_number_integer()&&f[i].get<int64_t>()>=0&&f[i].get<int64_t>()<int64_t(points.size()),"candidate index invalid");face[i]=f[i].get<uint32_t>();}
        checked_faces.push_back(face);}
    for(int i:anchors)checked_fixed.push_back(static_cast<uint32_t>(i));
    BetterEndfieldNext::CustomModel::ClothPrototype::Validate(checked_points,checked_faces,checked_fixed);
    auto vector=runtime->ResolveClass(kUnity,"UnityEngine","Vector3"),integer=runtime->ResolveClass("mscorlib.dll","System","Int32");
    auto mesh_type=runtime->ResolveClass(kUnity,"UnityEngine","Mesh");
    CandidateNeed(vector.info&&integer.info&&mesh_type.info,"candidate Mesh types missing");
    auto verts=s.Root(runtime->NewArraySpecific(vector.info,points.size()));
    auto indices=s.Root(runtime->NewArraySpecific(integer.info,faces.size()*3));
    CandidateNeed(verts&&indices,"candidate arrays unavailable");
    auto set=s.Method("mscorlib.dll","System","Array","SetValue","System.Object|System.Int32","System.Void",2);
    for(int i=0;i<int(points.size());++i) {
        const auto& p=points[i];CandidateNeed(p.is_array()&&p.size()==3,"candidate vector invalid");
        float point[3]{};for(int k=0;k<3;++k){CandidateNeed(p[k].is_number(),"candidate coordinate type invalid");point[k]=p[k].get<float>();CandidateNeed(std::isfinite(point[k])&&std::abs(point[k])<=3,"candidate coordinate invalid");}
        // Authoring is serialized mesh-local Z-up. The isolated experiment
        // uses Y-up world geometry; it is NOT yet a live renderer-space bind.
        float value[3]{point[0],point[2],-point[1]};
        if(layer.value("live_space",false))std::memcpy(value,point,sizeof(value));
        auto box=s.Root(runtime->BoxValue(vector.info,value));
        void* args[]{box,&i};s.Call(set,verts,args);
    }
    int at=0;
    for(const auto& face:faces) {
        CandidateNeed(face.is_array()&&face.size()==3,"candidate triangle invalid");std::set<int> unique;
        for(const auto& element:face) {
            CandidateNeed(element.is_number_integer(),"candidate index type invalid");int value=element.get<int>();
            CandidateNeed(value>=0&&size_t(value)<points.size()&&unique.insert(value).second,"candidate index invalid");
            auto box=s.Root(runtime->BoxValue(integer.info,&value));void* args[]{box,&at};s.Call(set,indices,args);++at;
        }
    }
    out.mesh=c.Hold(runtime->NewObject(mesh_type.info));s.Call(s.Method(kUnity,"UnityEngine","Mesh",".ctor","","System.Void",0),out.mesh);
    void* v_args[]{verts};s.Call(s.Method(kUnity,"UnityEngine","Mesh","set_vertices","UnityEngine.Vector3[]","System.Void",1),out.mesh,v_args);
    void* i_args[]{indices};s.Call(s.Method(kUnity,"UnityEngine","Mesh","set_triangles","System.Int32[]","System.Void",1),out.mesh,i_args);
    s.Call(s.Method(kUnity,"UnityEngine","Mesh","RecalculateBounds","","System.Void",0),out.mesh);
    s.Call(s.Method(kUnity,"UnityEngine","Mesh","RecalculateNormals","","System.Void",0),out.mesh);
    auto filter=CandidateComponent(s,c,out.go,kUnity,"UnityEngine","MeshFilter");void* mesh_args[]{out.mesh};
    s.Call(s.Method(kUnity,"UnityEngine","MeshFilter","set_sharedMesh","UnityEngine.Mesh","System.Void",1),filter,mesh_args);
    out.renderer=CandidateComponent(s,c,out.go,kUnity,"UnityEngine","MeshRenderer");bool enabled=false;void* enabled_args[]{&enabled};
    s.Call(s.Method(kUnity,"UnityEngine","Renderer","set_enabled","System.Boolean","System.Void",1),out.renderer,enabled_args);
    out.cloth=CandidateComponent(s,c,out.go,kCloth,"BeyondDynamicBone","BeyondBoneCloth");
    s.Call(s.Method(kCloth,"BeyondDynamicBone","BeyondBoneCloth","DisableAutoBuild","","System.Void",0),out.cloth);
}
void CandidateConfigure(Scope& s,ClothCandidate& c,ClothCandidate::Layer& layer,void* donor,const Json& authored,const Json& original) {
    auto cls=runtime->ResolveClass(kCloth,"BeyondDynamicBone","ClothSerializeData");CandidateNeed(cls.info,"candidate configuration class missing");
    layer.data=c.Hold(runtime->NewObject(cls.info));s.Call(s.Method(kCloth,"BeyondDynamicBone","ClothSerializeData",".ctor","","System.Void",0),layer.data);
    bool deep=true;void* copy_args[]{donor,&deep};
    s.Call(s.Method(kCloth,"BeyondDynamicBone","ClothSerializeData","Import","BeyondDynamicBone.ClothSerializeData|System.Boolean","System.Void",2),layer.data,copy_args);
    for(auto name:{"sourceRenderers","rootBones","ignoreFromRootBones","selfCollisionConstraint","colliderCollisionConstraint","cullingSettings"}) {
        auto a=s.Field(donor,"ClothSerializeData",name),b=s.Field(layer.data,"ClothSerializeData",name);
        CandidateNeed(a&&b&&a!=b,"candidate configuration aliases original field");
    }
    CandidateNeed(original.contains("rootBones")&&!original["rootBones"].empty(),"reference pattern unavailable");
    const auto ref=CandidateReference(original["rootBones"][0],CandidateInstance(s,layer.renderer));
    Json patch={{"clothType",CandidateEnum(s,"ClothProcess.ClothType","MeshCloth")},
        {"sourceRenderers",Json::array({ref})},{"rootBones",Json::array()},{"ignoreFromRootBones",Json::array()},
        {"animationPoseRatio",0.0},{"clothSimulateWeight",1.0},{"blendWeight",1.0},
        {"cullingSettings",{{"cameraCullingMode",CandidateEnum(s,"CullingSettings.CameraCullingMode","Off")},
            {"cameraCullingRenderers",Json::array()},{"distanceCullingLength",{{"use",false}}}}},
        {"colliderCollisionConstraint",{{"colliderList",Json::array()},{"collisionBones",Json::array()}}}};
    CandidatePatch(s,layer.data,patch);
    auto observed=Serialize(s,layer.data);
    CandidateNeed(observed.at("sourceRenderers")==patch["sourceRenderers"]&&observed.at("rootBones").empty()&&
        observed.at("clothType")==patch["clothType"]&&
        observed.at("colliderCollisionConstraint").at("colliderList").empty()&&
        observed.at("colliderCollisionConstraint").at("collisionBones").empty(),
        "candidate renderer/type/reference readback mismatch");
    // A configuration copy does not clone referenced Unity objects. The
    // isolated experiment must not register the prefab's original colliders.
    c.result["original_colliders_borrowed"]=false;
    void* data_args[]{layer.data};s.Call(s.Method(kCloth,"BeyondDynamicBone","BeyondBoneCloth","set_SerializeData","BeyondDynamicBone.ClothSerializeData","System.Void",1),layer.cloth,data_args);
    auto get=s.Method(kCloth,"BeyondDynamicBone","BeyondBoneCloth","get_SerializeData","","BeyondDynamicBone.ClothSerializeData",0);
    CandidateNeed(s.Call(get,layer.cloth)==layer.data,"candidate SerializeData was not installed");
    auto data2=s.Call(s.Method(kCloth,"BeyondDynamicBone","BeyondBoneCloth","GetSerializeData2","","BeyondDynamicBone.ClothSerializeData2",0),layer.cloth);
    auto selection=s.Field(data2,"ClothSerializeData2","selectionData");CandidateNeed(selection,"candidate selection data missing");
    const int fix=CandidateEnum(s,"VertexAttribute","Flag_Fixed"),move=CandidateEnum(s,"VertexAttribute","Flag_Move");
    std::set<int> anchors;for(const auto& v:authored.at("fixed_vertices"))anchors.insert(v.get<int>());
    Json ps=Json::array(),attrs=Json::array();int i=0;
    for(const auto& p:authored.at("positions")) {
        if(authored.value("live_space",false))ps.push_back({{"x",p[0]},{"y",p[1]},{"z",p[2]}});
        else ps.push_back({{"x",p[0]},{"y",p[2]},{"z",-p[1].get<double>()}});
        attrs.push_back({{"Value",anchors.contains(i++)?fix:move}});
    }
    CandidatePatch(s,selection,{{"positions",ps},{"attributes",attrs},{"maxConnectionDistance",0.05},{"userEdit",true}});
    // CopyString intentionally limits arbitrary managed strings to 32 KiB.
    // A full selection exceeds that limit; validate typed arrays in place.
    c.result["stage"]="selection_readback";
    auto count=s.Method(kCloth,"BeyondDynamicBone","SelectionData","get_Count","","System.Int32",0);
    auto is_edit=s.Method(kCloth,"BeyondDynamicBone","SelectionData","IsUserEdit","","System.Boolean",0);
    CandidateNeed(s.Int(count,selection)==int(ps.size())&&CandidateBool(s,is_edit,selection),"candidate selection size/edit mismatch");
    auto attribute_array=s.Field(selection,"SelectionData","attributes");
    auto length=s.Method("mscorlib.dll","System","Array","get_Length","","System.Int32",0);
    auto item=s.Method("mscorlib.dll","System","Array","GetValue","System.Int32","System.Object",1);
    CandidateNeed(attribute_array&&s.Int(length,attribute_array)==int(attrs.size()),"candidate attribute count mismatch");
    for(int n=0;n<int(attrs.size());++n) {
        void* args[]{&n};auto box=s.Call(item,attribute_array,args);
        CandidateNeed(CandidateNumber(s,s.Field(box,"VertexAttribute","Value"))==attrs[n]["Value"].get<int>(),
            "candidate attribute value mismatch");
    }
    layer.process=c.Hold(s.Call(s.Method(kCloth,"BeyondDynamicBone","BeyondBoneCloth","get_Process","","BeyondDynamicBone.ClothProcess",0),layer.cloth));
}
void CandidatePairPolicy(Scope&,ClothCandidate&,bool,bool notify=true);
void CandidateStart(Scope& s,void* donor,const Json& authoring,void* parent=nullptr) {
    CandidateNeed(!candidate,"candidate already active");
    CandidateNeed(authoring.value("kind","")=="bem-cloth-authoring-prototype"&&authoring.at("layers").size()==2,
        "candidate authoring format invalid");
    CandidateNeed(authoring.at("target").at("character_id")=="chr_0013_aglina","candidate character invalid");
    candidate=std::make_unique<ClothCandidate>();auto& c=*candidate;c.started=std::chrono::steady_clock::now();
    const auto original=Serialize(s,donor);
    c.owner=CandidateGameObject(s,c,"BEM_Physics_Candidate",parent);
    for(int i=0;i<2;++i) {
        c.result["stage"]=i?"outer_prepare":"inner_prepare";
        CandidateNeed(authoring["layers"][i]["id"]==(i?"outer":"inner"),"candidate layer order invalid");
        auto& layer=c.layers[i];layer.go=CandidateGameObject(s,c,i?"BEM_Outer_Proxy":"BEM_Inner_Proxy",c.owner);
        CandidateMesh(s,c,layer,authoring["layers"][i]);CandidateConfigure(s,c,layer,donor,authoring["layers"][i],original);
    }
    // Android's cross-frame edge contact job was observed dereferencing a null
    // indexCount in candidate 002. Do not enable FullMesh until the Android
    // scheduler adapter proves the counter lease and job dependencies.
    c.result["stage"]="independent_configuration";
    for(auto& layer:c.layers) {
        auto self=s.Field(layer.data,"ClothSerializeData","selfCollisionConstraint");
        CandidateNeed(self,"candidate self-collision data missing");
        const auto prior=Serialize(s,self);
        const int none=CandidateEnum(s,"SelfCollisionConstraint.SelfCollisionMode","None");
        CandidatePatch(s,self,{{"selfMode",none},{"syncMode",none},
            {"syncPartner",CandidateReference(prior.at("syncPartner"),0)}});
        const auto observed=Serialize(s,self);
        CandidateNeed(observed.at("selfMode")==none&&observed.at("syncMode")==none&&
            !s.Field(self,"SelfCollisionConstraint.SerializeData","syncPartner"),
            "independent candidate unexpectedly enables pair contact");
    }
    CandidateNeed(Serialize(s,donor)==original,"original configuration changed during candidate preparation");
    c.result["prepared"]=true;c.result["original_configuration_unchanged"]=true;
    c.result["pair_policy_configured"]=false;c.result["build_accepted"]=Json::array();
    if(report.value("mode","")=="build-proxy-pair"||report.value("mode","")=="bind-live-pair") {
        contact_hook::Install(s);
        c.pair_started=true;c.pair_time=std::chrono::steady_clock::now();
        CandidatePairPolicy(s,c,true,false);c.result["pair_policy_configured"]=true;
        c.result["pair_configured_before_build"]=true;
    }
    CandidateActive(s,c.owner,true);
    // Producer first, then its consumer; no reciprocal sync cycle.
    for(int i:{1,0}) {
        c.result["stage"]=i?"outer_build":"inner_build";
        auto& layer=c.layers[i];CandidateActive(s,layer.go,true);
        s.Call(s.Method(kCloth,"BeyondDynamicBone","BeyondBoneCloth","Initialize","","System.Void",0),layer.cloth);
        bool accepted=CandidateBool(s,s.Method(kCloth,"BeyondDynamicBone","BeyondBoneCloth","BuildAndRun","","System.Boolean",0),layer.cloth);
        c.result["build_accepted"].push_back({{"layer",i?"outer":"inner"},{"accepted",accepted}});
        CandidateNeed(accepted,"native candidate build refused");
    }
    c.result["status"]="building";
}
Json CandidateTeam(Scope& s,int id,bool consumer) {
    auto manager=s.Call(s.Method(kCloth,"BeyondDynamicBone","MagicaManager","get_Team","","BeyondDynamicBone.TeamManager",0),nullptr);
    void* args[]{&id};
    auto team=s.Call(s.Method(kCloth,"BeyondDynamicBone","TeamManager","GetTeamDataRef","System.Int32","BeyondDynamicBone.TeamManager.TeamData&",1),manager,args);
    CandidateNeed(team&&runtime->Unbox(team),"native team data unavailable");
    Json out={{"team_id",id},{"sync_team_id",CandidateNumber(s,s.Field(team,"TeamManager.TeamData","syncTeamId"))}};
    for(auto field:{"selfPointChunk","selfEdgeChunk","selfTriangleChunk","particleChunk"}) {
        auto chunk=s.Field(team,"TeamManager.TeamData",field);
        out[field]={{"start",CandidateNumber(s,s.Field(chunk,"DataChunk","startIndex"))},
            {"count",CandidateNumber(s,s.Field(chunk,"DataChunk","dataLength"))}};
    }
    auto flags=s.Field(team,"TeamManager.TeamData","flag");
    const auto is_set=s.Method("Unity.Collections.dll","Unity.Collections","BitField64","IsSet","System.Int32","System.Boolean",1);
    out["contact_flags"]=Json::array();
    for(auto suffix:{"EdgeEdge","PointTriangle","TrianglePoint"}) {
        auto name=std::string(consumer?"Flag_Sync_":"Flag_PSync_")+suffix;
        int bit=CandidateEnum(s,"TeamManager",name.c_str());void* flag_args[]{&bit};
        out["contact_flags"].push_back(CandidateBool(s,is_set,runtime->Unbox(flags),flag_args));
    }
    auto parents=s.Field(team,"TeamManager.TeamData","syncParentTeamId");
    ContactMetadata metadata;
    auto object_class=metadata.Export<Il2CppClass*(*)(void*)>("il2cpp_object_get_class");
    auto method=metadata.Export<const MethodInfo*(*)(Il2CppClass*,const char*,int)>("il2cpp_class_get_method_from_name");
    CandidateNeed(parents&&object_class&&method,"native reciprocal list metadata unavailable");
    auto cls=object_class(parents);auto ptr=runtime->Unbox(parents);
    int count=s.Int({method(cls,"get_Length",0),nullptr},ptr);CandidateNeed(count>=0&&count<=7,"native reciprocal list invalid");
    out["parent_teams"]=Json::array();
    for(int i=0;i<count;++i){void* item_args[]{&i};out["parent_teams"].push_back(s.Int({method(cls,"get_Item",1),nullptr},ptr,item_args));}
    auto parameters=s.Call(s.Method(kCloth,"BeyondDynamicBone","TeamManager","GetParametersRef","System.Int32","BeyondDynamicBone.ClothParameters&",1),manager,args);
    auto contact=s.Field(parameters,"ClothParameters","selfCollisionConstraint");
    out["effective_self_mode"]=CandidateNumber(s,s.Field(contact,"SelfCollisionConstraint.SelfCollisionConstraintParams","selfMode"));
    out["effective_sync_mode"]=CandidateNumber(s,s.Field(contact,"SelfCollisionConstraint.SelfCollisionConstraintParams","syncMode"));
    return out;
}
Json CandidateParticleArray(Scope& s,const Json& team,const char* name) {
    contact_hook::Complete(s);
    const int start=team.at("particleChunk").at("start"),count=team.at("particleChunk").at("count");
    CandidateNeed(start>=0&&count>0&&count<=2048,"candidate particle range invalid");
    auto simulation=s.Call(s.Method(kCloth,"BeyondDynamicBone","MagicaManager","get_Simulation","","BeyondDynamicBone.SimulationManager",0),nullptr);
    auto storage=s.Field(simulation,"SimulationManager",name);CandidateNeed(storage,"particle storage unavailable");
    ContactMetadata metadata;
    auto object_class=metadata.Export<Il2CppClass*(*)(void*)>("il2cpp_object_get_class");
    auto field_by_name=metadata.Export<const FieldInfo*(*)(Il2CppClass*,const char*)>("il2cpp_class_get_field_from_name");
    CandidateNeed(object_class&&field_by_name&&metadata.from_type,"particle metadata exports unavailable");
    auto field=field_by_name(object_class(storage),"nativeArray");CandidateNeed(field,"particle nativeArray field unavailable");
    auto box=s.Root(runtime->ReadFieldObject(field,storage));auto data=runtime->Unbox(box);
    CandidateNeed(data,"particle array box unavailable");
    const auto layout=metadata.Layout(metadata.from_type(metadata.type(field)));
    const size_t header=reinterpret_cast<uintptr_t>(data)-reinterpret_cast<uintptr_t>(box);
    CandidateNeed(layout.at("size")==sizeof(BetterEndfieldNext::CustomModel::ClothContact::Container),"particle array layout size mismatch");
    contact_hook::Check(layout,"m_Buffer","System.Void*",0,header);
    contact_hook::Check(layout,"m_Length","System.Int32",8,header);
    auto vector=runtime->ResolveClass("Unity.Mathematics.dll","Unity.Mathematics","double3");
    const auto vector_layout=metadata.Layout(vector.info);CandidateNeed(vector_layout.at("size")==24,"particle vector layout mismatch");
    contact_hook::Check(vector_layout,"x","System.Double",0,header);
    contact_hook::Check(vector_layout,"y","System.Double",8,header);
    contact_hook::Check(vector_layout,"z","System.Double",16,header);
    BetterEndfieldNext::CustomModel::ClothContact::Container array{};std::memcpy(&array,data,sizeof(array));
    const int length=static_cast<int32_t>(array.tail);
    CandidateNeed(array.data&&length>=count&&start<=length-count&&length<=1048576&&
        array.data<=UINTPTR_MAX-size_t(length)*24,"particle buffer range invalid");
    Json result=Json::array();
    for(int i=0;i<count;++i) {
        double point[3];std::memcpy(point,reinterpret_cast<void*>(array.data+size_t(start+i)*24),sizeof(point));
        for(double value:point)CandidateNeed(std::isfinite(value)&&std::abs(value)<100000,"particle output non-finite or unbounded");
        result.push_back({point[0],point[1],point[2]});
    }
    return result;
}
void CandidatePairPolicy(Scope& s,ClothCandidate& c,bool enable,bool notify) {
    contact_hook::Complete(s);
    auto self=s.Field(c.layers[0].data,"ClothSerializeData","selfCollisionConstraint");
    const auto before=Serialize(s,self);
    CandidatePatch(s,self,{{"syncMode",CandidateEnum(s,"SelfCollisionConstraint.SelfCollisionMode",enable?"FullMesh":"None")},
        {"syncPartner",CandidateReference(before.at("syncPartner"),enable?CandidateInstance(s,c.layers[1].cloth):0)},
        {"clothMass",0.5}});
    if(notify) {
        auto refresh=s.Method(kCloth,"BeyondDynamicBone","BeyondBoneCloth","SetParameterChange","","System.Void",0);
        s.Call(refresh,c.layers[1].cloth);s.Call(refresh,c.layers[0].cloth);
    }
}
bool CandidateCleanup(Scope& s) {
    if(!candidate)return true;auto& c=*candidate;
    layer_hook::Clear(s);
    LiveDetach(s);BoneDetach(s);
    contact_hook::Complete(s);
    if(c.pair_started&&!c.destroying) {
        if(!c.pair_retiring){CandidatePairPolicy(s,c,false);c.pair_retiring=true;return false;}
        const int inner=CandidateNumber(s,s.Field(c.layers[0].process,"ClothProcess","<TeamId>k__BackingField"));
        const int outer=CandidateNumber(s,s.Field(c.layers[1].process,"ClothProcess","<TeamId>k__BackingField"));
        if(!c.consumer_destroy_sent) {
            if(inner>0) {
                const auto a=CandidateTeam(s,inner,true);c.result["retiring_consumer"]=a;
                if(a["effective_sync_mode"]!=CandidateEnum(s,"SelfCollisionConstraint.SelfCollisionMode","None"))return false;
            }
            c.retiring_consumer_team=inner;
            auto alive=s.Method(kUnity,"UnityEngine","Object","op_Implicit","UnityEngine.Object","System.Boolean",1);
            void* args[]{c.layers[0].go};
            if(CandidateBool(s,alive,nullptr,args)) {
                CandidateActive(s,c.layers[0].go,false);
                s.Call(s.Method(kUnity,"UnityEngine","Object","Destroy","UnityEngine.Object","System.Void",1),nullptr,args);
            }
            c.consumer_destroy_sent=true;return false;
        }
        if(c.retiring_consumer_team>0) {
            auto manager=s.Call(s.Method(kCloth,"BeyondDynamicBone","MagicaManager","get_Team","","BeyondDynamicBone.TeamManager",0),nullptr);
            void* args[]{&c.retiring_consumer_team};
            if(CandidateBool(s,s.Method(kCloth,"BeyondDynamicBone","TeamManager","ContainsTeamData","System.Int32","System.Boolean",1),manager,args))return false;
        }
        if(outer>0) {
            if(!c.producer_retire_notified) {
                s.Call(s.Method(kCloth,"BeyondDynamicBone","BeyondBoneCloth","SetParameterChange","","System.Void",0),c.layers[1].cloth);
                c.producer_retire_notified=true;return false;
            }
            const auto b=CandidateTeam(s,outer,false);c.result["retiring_producer"]=b;
            if(!b["parent_teams"].empty())return false;
            for(auto field:{"selfPointChunk","selfEdgeChunk","selfTriangleChunk"})if(b[field]["count"]!=0)return false;
        }
        c.result["pair_detached"]=true;
    }
    auto alive=s.Method(kUnity,"UnityEngine","Object","op_Implicit","UnityEngine.Object","System.Boolean",1);
    auto destroy=s.Method(kUnity,"UnityEngine","Object","Destroy","UnityEngine.Object","System.Void",1);
    if(!c.destroying) {
        c.destroying=true;
        // Consumer before producer; parent last. This also handles partially
        // constructed objects without deleting a live partner first.
        auto remove=[&](void* go){void* args[]{go};if(CandidateBool(s,alive,nullptr,args)){CandidateActive(s,go,false);s.Call(destroy,nullptr,args);}};
        for(size_t i=1;i<c.scene_objects.size();++i)remove(c.scene_objects[i]);
        if(!c.scene_objects.empty())remove(c.scene_objects.front());
        for(auto& layer:c.layers)if(layer.mesh){void* args[]{layer.mesh};if(CandidateBool(s,alive,nullptr,args))s.Call(destroy,nullptr,args);}
    }
    bool gone=true;
    for(auto object:c.scene_objects){void* args[]{object};gone&=!CandidateBool(s,alive,nullptr,args);}
    for(auto& layer:c.layers)if(layer.mesh){void* args[]{layer.mesh};gone&=!CandidateBool(s,alive,nullptr,args);}
    if(gone) {
        if(!layer_hook::Release(s))return false;
        if(!contact_hook::Release(s))return false;
        report["candidate"]=c.result;report["candidate"]["cleanup_confirmed"]=true;
        report["candidate"]["contact_hooks_released"]=true;candidate.reset();return true;
    }
    return false;
}
bool CandidatePoll(Scope& s) {
    if(!candidate)return false;auto& c=*candidate;
    if(c.finished||c.destroying) {CandidateCleanup(s);return true;}
    bool running=true;std::set<int> teams;Json states=Json::array();
    for(int i=0;i<2;++i) {
        const auto& layer=c.layers[i];
        bool valid=CandidateBool(s,s.Method(kCloth,"BeyondDynamicBone","ClothProcess","IsValid","","System.Boolean",0),layer.process);
        bool run=CandidateBool(s,s.Method(kCloth,"BeyondDynamicBone","ClothProcess","IsRunning","","System.Boolean",0),layer.process);
        int team=CandidateNumber(s,s.Field(layer.process,"ClothProcess","<TeamId>k__BackingField"));
        states.push_back({{"layer",i?"outer":"inner"},{"valid",valid},{"running",run},{"team_id",team}});
        running&=valid&&run&&team>0;teams.insert(team);
    }
    c.result["teams"]=states;
    const auto elapsed=std::chrono::duration_cast<std::chrono::seconds>(std::chrono::steady_clock::now()-c.started).count();
    if(running&&teams.size()==2) {
        c.result["status"]="two_native_teams_running";c.result["native_build_verified"]=true;
        if(c.result.value("bonecloth_bound",false)&&!c.result.value("body_collider_registration_verified",false)) {
            if(!BoneBodyRegistration(s)) {
                c.result["status"]="body_registration_pending";
                if(elapsed>=20) {c.result["status"]="body_registration_timeout";c.finished=true;}
                report["candidate"]=c.result;
                if(c.finished)CandidateCleanup(s);
                return true;
            }
        }
        if(c.result.value("bonecloth_bound",false))UpdateBoneLayerOrder(s);
        if(report.value("mode","")=="verify-contact-scheduler") {
            c.result["contact_scheduler"]=contact_hook::SmokeTest(s);
            c.result["status"]="contact_scheduler_verified";
        }
        if(report.value("mode","")=="build-proxy-pair"||LiveMode(report.value("mode",""))) {
            CandidateNeed(!contact_hook::fault.load(),"contact scheduler rejected a native job");
            const int inner=c.result["teams"][0]["team_id"],outer=c.result["teams"][1]["team_id"];
            contact_hook::Complete(s);
            auto a=CandidateTeam(s,inner,true),b=CandidateTeam(s,outer,false);
            c.result["pair_readback"]=Json::array({a,b});
            if(!c.pair_started) {
                c.result["contact_scheduler"]=contact_hook::SmokeTest(s);
                c.pair_started=true;c.pair_time=std::chrono::steady_clock::now();
                CandidatePairPolicy(s,c,true);c.result["pair_policy_configured"]=true;
                c.result["status"]="pair_registration_pending";
            } else {
                const int full=CandidateEnum(s,"SelfCollisionConstraint.SelfCollisionMode","FullMesh");
                const int none=CandidateEnum(s,"SelfCollisionConstraint.SelfCollisionMode","None");
                bool ready=a["sync_team_id"]==outer&&b["sync_team_id"]==0&&
                    b["parent_teams"]==Json::array({inner})&&a["parent_teams"].empty()&&
                    a["effective_sync_mode"]==full&&a["effective_self_mode"]==none;
                for(const auto* row:{&a,&b}) {
                    for(const auto& flag:(*row)["contact_flags"])ready&=flag.get<bool>();
                    for(auto field:{"selfPointChunk","selfEdgeChunk","selfTriangleChunk"})ready&=(*row)[field]["count"].get<int>()>0;
                }
                c.pair_stable_samples=ready?c.pair_stable_samples+1:0;
                c.result["pair_stable_samples"]=c.pair_stable_samples;
                if(ready&&(c.pair_stable_samples==1||c.pair_stable_samples==5)) {
                    Json sample={{"sample",c.pair_stable_samples},{"layers",Json::array()}};
                    for(const auto* row:{&a,&b})sample["layers"].push_back({
                        {"base",CandidateParticleArray(s,*row,"basePosArray")},
                        {"display",CandidateParticleArray(s,*row,"dispPosArray")},
                        {"next",CandidateParticleArray(s,*row,"nextPosArray")}});
                    c.result["particle_samples"].push_back(std::move(sample));
                    c.result["particle_output_readable"]=true;
                }
                c.result["contact_repairs"]=Json::array({contact_hook::repairs[0].load(),contact_hook::repairs[1].load()});
                c.result["status"]=ready?"pair_registered_observing":"pair_registration_pending";
                if(c.pair_stable_samples>=5&&contact_hook::repairs[0]>1&&contact_hook::repairs[1]>1) {
                    c.result["status"]="native_pair_registration_verified";
                    c.result["pair_contact_registration_verified"]=true;
                    c.finished=!LiveMode(report.value("mode",""));
                } else if(std::chrono::steady_clock::now()-c.pair_time>std::chrono::seconds(20)) {
                    c.result["status"]="pair_registration_timeout";c.finished=true;
                }
            }
        } else {c.result["pair_contact_registration_verified"]=false;c.finished=true;}
    } else if(elapsed>=20) {c.result["status"]="build_timeout";c.finished=true;}
    report["candidate"]=c.result;
    if(c.finished)CandidateCleanup(s);
    return true;
}
