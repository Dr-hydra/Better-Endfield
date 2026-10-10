// EIEM-style visible skirt physics. Rin's skirt joints are recreated as new
// Transforms under the live pelvis, the receiver renderer is rebound to a
// mesh carrying one extra bindpose per joint, and native BeyondBoneCloth teams
// (configuration copied from aglina's MC_coat, including its leg colliders)
// move those Transforms. Ordinary skinning shows the result; no vertex
// positions are written per frame. Inner/outer are FullMesh sync partners.
namespace bone_cloth {
namespace CB=BetterEndfieldNext::CustomModel::ClothBone;
namespace CD=BetterEndfieldNext::CustomModel::ClothDisplay;
struct Binding {
    void *renderer=nullptr,*root=nullptr,*source=nullptr,*source_bones=nullptr,*mesh=nullptr,*pelvis=nullptr;
    std::vector<void*> joints;          // Transforms, authoring order
    std::vector<void*> render_joints;   // fixed roots use pelvis-owned anchors
    std::vector<CD::V> rest_pelvis;
    Json authoring;
    CD::M pelvis_bind{};
    std::map<std::string,int> joint_index;
    std::shared_ptr<void> source_pin;
    int palette=0;
    uint32_t vertex_count=0;
    bool ui=false;
    int seam_captures=0;
    bool applied=false;
};
std::unique_ptr<Binding> binding;
std::array<int,3> stable_receiver{};
int stable_receiver_samples=0;

void CaptureSeam(Scope& s) {
    if(!capture_seam_request||!binding||binding->seam_captures>=2)return;
    auto& b=*binding;auto& c=*candidate;
    contact_hook::Complete(s);
    BetterEndfieldNext::CustomModel::BemComponent mesh;
    CandidateNeed(AndroidReadMeshGeometry(b.mesh,mesh),"seam mesh geometry unavailable");
    auto bones=s.Call(s.Method(kUnity,"UnityEngine","SkinnedMeshRenderer","get_bones","","UnityEngine.Transform[]",0),b.renderer);
    auto poses=s.Call(s.Method(kUnity,"UnityEngine","Mesh","get_bindposes","","UnityEngine.Matrix4x4[]",0),b.mesh);
    auto inverse=live_cloth::Matrix(s,s.Method(kUnity,"UnityEngine","Transform","get_worldToLocalMatrix","","UnityEngine.Matrix4x4",0),live_cloth::Transform(s,b.renderer));
    std::vector<CD::M> skin;
    Json bone_rows=Json::array();
    for(int i=0;i<b.palette;++i) {
        auto bone=live_cloth::Item(s,bones,i);
        auto world=live_cloth::Matrix(s,s.Method(kUnity,"UnityEngine","Transform","get_localToWorldMatrix","","UnityEngine.Matrix4x4",0),bone);
        auto box=live_cloth::Item(s,poses,i);float f[16];CandidateNeed(runtime->Unbox(box),"seam bindpose unavailable");
        std::memcpy(f,runtime->Unbox(box),sizeof(f));CD::M bind{};for(int k=0;k<16;++k)bind[k]=f[k];
        auto transform=CD::Multiply(CD::Multiply(inverse,world),bind);skin.push_back(transform);
        bone_rows.push_back({{"name",live_cloth::Name(s,bone)},{"skin",transform}});
    }
    Json result={{"context",b.ui?"details-ui":"world"},{"bones",bone_rows},{"vertices",Json::array()},
        {"triangles",Json::array()},{"joint_vertices",b.authoring.at("vertices")},{"colliders",Json::array()}};
    for(uint32_t i=0;i<mesh.info.vertex_count;++i) {
        float p[3];std::memcpy(p,mesh.streams[0].data()+size_t(i)*16,sizeof(p));
        const auto* data=mesh.streams[2].data()+size_t(i)*12;
        std::array<uint16_t,4> weights;std::array<uint8_t,4> indices;
        std::memcpy(weights.data(),data,8);std::memcpy(indices.data(),data+8,4);
        const auto posed=CD::Point(CD::Skin(skin,weights,indices),{p[0],p[1],p[2]});
        result["vertices"].push_back(Json::array({posed.x,posed.y,posed.z}));
    }
    const size_t stride=mesh.info.index_element_size;
    CandidateNeed(stride==2||stride==4,"seam index format invalid");
    for(uint32_t i=0;i<mesh.info.index_count;i+=3) {
        Json face=Json::array();
        for(int k=0;k<3;++k){uint32_t index=0;std::memcpy(&index,mesh.indices.data()+size_t(i+k)*stride,stride);face.push_back(index);}
        result["triangles"].push_back(std::move(face));
    }
    for(auto collider:c.body_colliders) {
        auto t=live_cloth::Transform(s,collider);
        const auto world=live_cloth::Matrix(s,s.Method(kUnity,"UnityEngine","Transform","get_localToWorldMatrix","","UnityEngine.Matrix4x4",0),t);
        auto transform=CD::Multiply(inverse,world);auto center=CD::Point(transform,{0,0,0});
        auto size=s.Call(s.Method(kCloth,"BeyondDynamicBone","ColliderComponent","GetSize","","UnityEngine.Vector3",0),collider);
        float dims[3];CandidateNeed(runtime->Unbox(size),"seam collider size unavailable");std::memcpy(dims,runtime->Unbox(size),sizeof(dims));
        auto axis=CD::Direction(transform,{1,0,0});double radius=dims[0]*std::sqrt(CD::Dot(axis,axis));
        result["colliders"].push_back({{"center",Json::array({center.x,center.y,center.z})},{"radius",radius}});
    }
    Json lengths=Json::array();
    for(int i=0;i<2;++i) {
        auto box=s.Root(runtime->ReadFieldObject(contact_hook::list_fields[i],contact_hook::manager));
        BetterEndfieldNext::CustomModel::ClothContact::Container list{};
        CandidateNeed(runtime->Unbox(box),"seam contact header unavailable");std::memcpy(&list,runtime->Unbox(box),sizeof(list));
        int32_t count=0;CandidateNeed(list.data,"seam contact list unavailable");
        std::memcpy(&count,reinterpret_cast<void*>(list.data+contact_hook::length_offset),sizeof(count));lengths.push_back(count);
    }
    result["contact_list_lengths"]=lengths;
    auto path=candidate_claim_path.parent_path()/(std::string("bem-cloth-seam-")+(b.ui?"ui-":"world-")+std::to_string(++b.seam_captures)+".json");
    std::ofstream file(path,std::ios::trunc);file<<result.dump();CandidateNeed(bool(file),"seam capture write failed");
    c.result["seam_capture_count"]=b.seam_captures;c.result["native_contact_list_lengths"]=lengths;
}

void SetLocal(Scope& s,void* transform,CD::V position,std::array<double,4> rotation={0,0,0,1}) {
    float p[3]{float(position.x),float(position.y),float(position.z)},q[4]{float(rotation[0]),float(rotation[1]),float(rotation[2]),float(rotation[3])},one[3]{1,1,1};
    void* a[]{p};s.Call(s.Method(kUnity,"UnityEngine","Transform","set_localPosition","UnityEngine.Vector3","System.Void",1),transform,a);
    void* b[]{q};s.Call(s.Method(kUnity,"UnityEngine","Transform","set_localRotation","UnityEngine.Quaternion","System.Void",1),transform,b);
    void* c[]{one};s.Call(s.Method(kUnity,"UnityEngine","Transform","set_localScale","UnityEngine.Vector3","System.Void",1),transform,c);
}
void* NewArray(Scope& s,const char* assembly,const char* ns,const char* cls,int length) {
    auto type=runtime->ResolveClass(assembly,ns,cls);CandidateNeed(type.info,"bone cloth array element class missing");
    return s.Root(runtime->NewArraySpecific(type.info,length));
}
void SetItem(Scope& s,void* array,int index,void* value) {
    void* a[]{value,&index};
    s.Call(s.Method("mscorlib.dll","System","Array","SetValue","System.Object|System.Int32","System.Void",2),array,a);
}
// Managed reflection: Dictionary<Transform,VertexAttribute>.Add(key,value).
void DictionaryAdd(Scope& s,void* dict,void* key,void* boxed_value) {
    auto type=s.Call(s.Method("mscorlib.dll","System","Object","GetType","","System.Type",0),dict);
    auto name=s.Root(runtime->NewString("Add"));void* g[]{name};
    auto method=s.Call(s.Method("mscorlib.dll","System","Type","GetMethod","System.String","System.Reflection.MethodInfo",1),type,g);
    CandidateNeed(method,"Dictionary.Add unavailable");
    auto args=NewArray(s,"mscorlib.dll","System","Object",2);SetItem(s,args,0,key);SetItem(s,args,1,boxed_value);
    void* invoke[]{dict,args};
    s.Call(s.Method("mscorlib.dll","System.Reflection","MethodBase","Invoke","System.Object|System.Object[]","System.Object",2),method,invoke);
}
int DictionaryCount(Scope& s,void* dict) {
    auto type=s.Call(s.Method("mscorlib.dll","System","Object","GetType","","System.Type",0),dict);
    auto name=s.Root(runtime->NewString("get_Count"));void* g[]{name};
    auto method=s.Call(s.Method("mscorlib.dll","System","Type","GetMethod","System.String","System.Reflection.MethodInfo",1),type,g);
    auto args=NewArray(s,"mscorlib.dll","System","Object",0);void* invoke[]{dict,args};
    auto box=s.Call(s.Method("mscorlib.dll","System.Reflection","MethodBase","Invoke","System.Object|System.Object[]","System.Object",2),method,invoke);
    auto v=runtime->Unbox(box);CandidateNeed(v,"Dictionary.Count unavailable");int out=0;std::memcpy(&out,v,4);return out;
}

void ConfigureLayer(Scope& s,ClothCandidate& c,ClothCandidate::Layer& layer,void* donor,const Json& original,
                    const Json& spec,const std::vector<int>& members,const std::set<int>& roots) {
    auto& b=*binding;
    auto cls=runtime->ResolveClass(kCloth,"BeyondDynamicBone","ClothSerializeData");CandidateNeed(cls.info,"bone cloth configuration class missing");
    layer.data=c.Hold(runtime->NewObject(cls.info));s.Call(s.Method(kCloth,"BeyondDynamicBone","ClothSerializeData",".ctor","","System.Void",0),layer.data);
    bool deep=true;void* copy_args[]{donor,&deep};
    s.Call(s.Method(kCloth,"BeyondDynamicBone","ClothSerializeData","Import","BeyondDynamicBone.ClothSerializeData|System.Boolean","System.Void",2),layer.data,copy_args);
    for(auto name:{"rootBones","ignoreFromRootBones","selfCollisionConstraint","colliderCollisionConstraint","cullingSettings"}) {
        auto x=s.Field(donor,"ClothSerializeData",name),y=s.Field(layer.data,"ClothSerializeData",name);
        CandidateNeed(x&&y&&x!=y,"bone cloth configuration aliases original field");
    }
    const auto reference_pattern=original.at("selfCollisionConstraint").at("syncPartner");
    Json root_refs=Json::array();
    for(int j:members)if(roots.contains(j))root_refs.push_back(CandidateReference(reference_pattern,CandidateInstance(s,b.joints[j])));
    const auto connection=spec.at("connection").get<std::string>();
    CandidateNeed(connection=="SequentialLoopMesh"||connection=="SequentialNonLoopMesh","bone cloth connection invalid");
    // Leg colliders: the donor's own MC_coat collider references, unchanged.
    auto colliders=b.ui?Json::array():original.at("colliderCollisionConstraint").at("colliderList");
    const size_t borrowed_count=colliders.size();
    const auto collider_pattern=colliders.empty()?reference_pattern:colliders[0];
    for(auto collider:c.body_colliders)
        colliders.push_back(CandidateReference(collider_pattern,CandidateInstance(s,collider)));
    Json patch={{"clothType",CandidateEnum(s,"ClothProcess.ClothType","BoneCloth")},
        {"normalAxis",CandidateEnum(s,"ClothNormalAxis","Up")},
        {"updateMode",CandidateEnum(s,"ClothUpdateMode",b.ui?"Unscaled":"Normal")},
        {"connectionMode",CandidateEnum(s,"RenderSetupData.BoneConnectionMode",connection.c_str())},
        {"sourceRenderers",Json::array()},{"rootBones",root_refs},{"ignoreFromRootBones",Json::array()},
        {"animationPoseRatio",0.0},{"clothSimulateWeight",1.0},{"blendWeight",1.0},
        // Fixed root positions alone still allow their output rotation to
        // swing waist vertices. Keep the attachment orientation on the pelvis.
        {"rootRotation",0.0},
        // MC_coat's authored radius is 112 mm. Rin's short skirt needs a
        // millimetre-scale contact envelope, not the original coat's spacing.
        {"radius",{{"useCurve",false},{"value",spec.at("name")=="inner"?0.004:0.006}}},
        {"damping",{{"useCurve",false},{"value",0.25}}},{"gravity",3.0},
        {"angleRestorationConstraint",{{"useAngleRestoration",true},
            {"stiffness",{{"useCurve",false},{"value",0.65}}}}},
        {"angleLimitConstraint",{{"useAngleLimit",false},
            {"limitAngle",{{"useCurve",false},{"value",45.0}}},{"stiffness",1.0}}},
        {"motionConstraint",{{"useMaxDistance",false},
            {"maxDistance",{{"useCurve",false},{"value",spec.at("name")=="inner"?0.08:0.12}}}}},
        {"selfCollisionConstraint",{{"clothMass",0.5},
            {"surfaceThickness",{{"useCurve",false},{"value",0.003}}}}},
        {"cullingSettings",{{"cameraCullingMode",CandidateEnum(s,"CullingSettings.CameraCullingMode","Off")},
            {"cameraCullingRenderers",Json::array()},{"distanceCullingLength",{{"use",false}}}}},
        {"colliderCollisionConstraint",{{"colliderList",colliders},{"collisionBones",Json::array()},
            {"mode",CandidateEnum(s,"ColliderCollisionConstraint.Mode","Edge")}}}};
    CandidatePatch(s,layer.data,patch);
    const auto observed=Serialize(s,layer.data);
    CandidateNeed(observed.at("rootBones")==root_refs&&observed.at("clothType")==patch["clothType"]&&
        observed.at("connectionMode")==patch["connectionMode"]&&
        observed.at("colliderCollisionConstraint").at("colliderList")==colliders,"bone cloth configuration readback mismatch");
    c.result["original_colliders_borrowed"]=borrowed_count>0;
    c.result["borrowed_collider_count"]=borrowed_count;
    c.result["fitted_collider_count"]=c.body_colliders.size();
    c.result["body_collision_mode"]="Edge";
    c.result["layer_parameters"][spec.at("name").get<std::string>()]={{"radius",observed.at("radius")},
        {"updateMode",observed.at("updateMode")},
        {"motionConstraint",observed.at("motionConstraint")},
        {"angleLimitConstraint",observed.at("angleLimitConstraint")},
        {"selfCollisionConstraint",observed.at("selfCollisionConstraint")}};
    void* data_args[]{layer.data};
    s.Call(s.Method(kCloth,"BeyondDynamicBone","BeyondBoneCloth","set_SerializeData","BeyondDynamicBone.ClothSerializeData","System.Void",1),layer.cloth,data_args);
    CandidateNeed(s.Call(s.Method(kCloth,"BeyondDynamicBone","BeyondBoneCloth","get_SerializeData","","BeyondDynamicBone.ClothSerializeData",0),layer.cloth)==layer.data,
        "bone cloth SerializeData was not installed");
    auto data2=s.Call(s.Method(kCloth,"BeyondDynamicBone","BeyondBoneCloth","GetSerializeData2","","BeyondDynamicBone.ClothSerializeData2",0),layer.cloth);
    auto dict=s.Field(data2,"ClothSerializeData2","boneAttributeDict");CandidateNeed(dict,"bone attribute dictionary missing");
    CandidateNeed(DictionaryCount(s,dict)==0,"bone attribute dictionary not empty");
    auto attribute=runtime->ResolveClass(kCloth,"BeyondDynamicBone","VertexAttribute");CandidateNeed(attribute.info,"VertexAttribute missing");
    const uint8_t fixed=uint8_t(CandidateEnum(s,"VertexAttribute","Flag_Fixed")),move=uint8_t(CandidateEnum(s,"VertexAttribute","Flag_Move"));
    for(int j:members) {
        uint8_t value=roots.contains(j)?fixed:move;
        DictionaryAdd(s,dict,b.joints[j],s.Root(runtime->BoxValue(attribute.info,&value)));
    }
    CandidateNeed(DictionaryCount(s,dict)==int(members.size()),"bone attribute dictionary count mismatch");
    layer.process=c.Hold(s.Call(s.Method(kCloth,"BeyondDynamicBone","BeyondBoneCloth","get_Process","","BeyondDynamicBone.ClothProcess",0),layer.cloth));
}

void Rebind(Scope& s,const Json& authoring,BetterEndfieldNext::CustomModel::BemComponent g,
            const std::vector<std::string>& bone_names,const std::vector<CD::M>& poses,const std::vector<CD::M>& joint_poses) {
    auto& b=*binding;auto& c=*candidate;
    const int old_count=int(bone_names.size()),total=old_count+int(b.joints.size());
    CandidateNeed(total<=256,"bone cloth palette exceeds 256");
    CandidateNeed(g.info.stride0==16&&g.info.stride2==12&&g.streams[2].size()==size_t(g.info.vertex_count)*12,"receiver skin stream layout unexpected");
    bool weights=false,indices=false;
    for(const auto& a:g.attributes){if(a[0]==12&&a[1]==4&&a[2]==4&&a[3]==2)weights=true;if(a[0]==13&&a[1]==6&&a[2]==4&&a[3]==2)indices=true;}
    CandidateNeed(weights&&indices,"receiver skin attributes are not UNorm16x4/UInt8x4 in stream 2");
    std::map<std::string,int> by_name;for(int i=0;i<old_count;++i)by_name.emplace(bone_names[i],i);
    const auto pelvis_index=by_name.find("Bip001_Pelvis");CandidateNeed(pelvis_index!=by_name.end(),"waist attachment pelvis missing");
    uint32_t sewn_vertices=0;
    for(const auto& row:authoring.at("vertices")) {
        const int v=row.at("v");CandidateNeed(v>=0&&v<int(g.info.vertex_count),"bone cloth vertex out of range");
        float rest[3];std::memcpy(rest,g.streams[0].data()+size_t(v)*16,12);
        for(int k=0;k<3;++k)CandidateNeed(std::abs(rest[k]-row.at("rest")[k].get<double>())<1e-4,"receiver geometry differs from bone cloth authoring");
        std::vector<CB::Influence> influences;
        for(const auto& e:row.at("skin")) {
            const auto kind=e[0].get<std::string>(),name=e[1].get<std::string>();
            int palette=-1;
            if(kind=="joint"){auto f=b.joint_index.find(name);CandidateNeed(f!=b.joint_index.end(),"bone cloth joint reference unknown");palette=old_count+f->second;}
            else {auto f=by_name.find(name);CandidateNeed(f!=by_name.end(),"bone cloth native bone not on receiver");palette=f->second;}
            influences.push_back({palette,e[2].get<double>()});
        }
        // The posterior seam includes vertices weighted to the second moving
        // row, not just the nominally fixed roots. Sew a narrow rear waist
        // band to the pelvis and fade into the existing simulated skin.
        const auto smooth=[](double low,double high,double v){double t=std::clamp((v-low)/(high-low),0.0,1.0);return t*t*(3-2*t);};
        const double attachment=smooth(0.965,1.025,rest[2])*smooth(0.015,0.075,rest[1]);
        if(attachment>1e-6) {
            for(auto& influence:influences)influence.weight*=1-attachment;
            auto found=std::find_if(influences.begin(),influences.end(),[&](const auto& x){return x.palette==pelvis_index->second;});
            if(found!=influences.end())found->weight+=attachment;
            else influences.push_back({pelvis_index->second,attachment});
            ++sewn_vertices;
        }
        CB::EncodeSkin(g.streams[2].data()+size_t(v)*12,influences);
    }
    uint32_t max_bone=0;
    for(uint32_t v=0;v<g.info.vertex_count;++v) {
        const uint8_t* p=g.streams[2].data()+size_t(v)*12;uint16_t w[4];std::memcpy(w,p,8);
        for(int k=0;k<4;++k)if(w[k])max_bone=std::max<uint32_t>(max_bone,p[8+k]);
    }
    CandidateNeed(max_bone<uint32_t(total),"bone cloth skin references beyond palette");
    g.info.max_bone=max_bone;
    auto matrix=runtime->ResolveClass(kUnity,"UnityEngine","Matrix4x4");CandidateNeed(matrix.info,"Matrix4x4 missing");
    auto pose_array=NewArray(s,kUnity,"UnityEngine","Matrix4x4",total);
    auto put=[&](int i,const CD::M& m){float f[16];for(int k=0;k<16;++k)f[k]=float(m[k]);SetItem(s,pose_array,i,s.Root(runtime->BoxValue(matrix.info,f)));};
    for(int i=0;i<old_count;++i)put(i,poses[i]);
    for(size_t j=0;j<joint_poses.size();++j)put(old_count+int(j),joint_poses[j]);
    b.mesh=BetterEndfieldNext::CustomModel::BuildModelMeshForCloth(g,b.source,pose_array);
    CandidateNeed(b.mesh,"bone cloth mesh build refused by loader");c.Hold(b.mesh);
    auto bone_array=NewArray(s,kUnity,"UnityEngine","Transform",total);
    for(int i=0;i<old_count;++i)SetItem(s,bone_array,i,live_cloth::Item(s,b.source_bones,i));
    for(size_t j=0;j<b.joints.size();++j)SetItem(s,bone_array,old_count+int(j),b.render_joints[j]);
    void* ba[]{bone_array};s.Call(s.Method(kUnity,"UnityEngine","SkinnedMeshRenderer","set_bones","UnityEngine.Transform[]","System.Void",1),b.renderer,ba);
    void* ma[]{b.mesh};s.Call(s.Method(kUnity,"UnityEngine","SkinnedMeshRenderer","set_sharedMesh","UnityEngine.Mesh","System.Void",1),b.renderer,ma);
    auto current=s.Call(s.Method(kUnity,"UnityEngine","SkinnedMeshRenderer","get_sharedMesh","","UnityEngine.Mesh",0),b.renderer);
    auto bones_now=s.Call(s.Method(kUnity,"UnityEngine","SkinnedMeshRenderer","get_bones","","UnityEngine.Transform[]",0),b.renderer);
    CandidateNeed(current==b.mesh&&live_cloth::Length(s,bones_now)==total,"bone cloth renderer rebind readback failed");
    b.applied=true;b.palette=total;
    c.result["render_binding_applied"]=true;c.result["palette"]={{"native",old_count},{"joints",b.joints.size()}};
    c.result["reskinned_vertices"]=authoring.at("vertices").size();
    c.result["rear_waist_attachment_vertices"]=sewn_vertices;
}

struct ReceiverChoice {void* renderer=nullptr;void* root=nullptr;bool ui=false;};
ReceiverChoice VisibleReceiver(Scope& s,uint32_t vertex_count) {
    using namespace live_cloth;
    auto type=runtime->ResolveClass(kUnity,"UnityEngine","SkinnedMeshRenderer");CandidateNeed(type.type_object,"renderer class unavailable");
    void* a[]{type.type_object};auto renderers=s.Call(s.Method(kUnity,"UnityEngine","Resources","FindObjectsOfTypeAll","System.Type","UnityEngine.Object[]",1),nullptr,a);
    const int count=Length(s,renderers);CandidateNeed(count>=0&&count<=65536,"renderer budget invalid");
    ReceiverChoice choice;Json discovery={{"renderer_count",count},{"receivers",Json::array()}};
    for(int i=0;i<count;++i) {
        auto r=Item(s,renderers,i);auto name=Name(s,r);
        if(name!="S_actor_aglina_cloth_01_lod0"&&name!="S_actor_aglina_cloth_01_lod1")continue;
        const bool visible=CandidateBool(s,s.Method(kUnity,"UnityEngine","Renderer","get_isVisible","","System.Boolean",0),r);
        if(!visible)continue;
        auto go=GameObject(s,r);
        if(!CandidateBool(s,s.Method(kUnity,"UnityEngine","GameObject","get_activeInHierarchy","","System.Boolean",0),go))continue;
        void* ancestor=Transform(s,r);void* found=nullptr;bool ui=false;
        for(int depth=0;ancestor&&depth<64;++depth) {
            const auto canonical=BetterEndfieldNext::CustomModel::ResourceBaseName(Name(s,ancestor));
            if(canonical=="chr_0013_aglina_postmodel"||canonical=="chr_0013_aglina_uimodel") {
                found=GameObject(s,ancestor);ui=canonical=="chr_0013_aglina_uimodel";break;
            }
            ancestor=s.Call(s.Method(kUnity,"UnityEngine","Transform","get_parent","","UnityEngine.Transform",0),ancestor);
        }
        if(!found)continue;
        // Details can instantiate a stripped postmodel rather than a uimodel.
        // Classify that live receiver by its native coat, not its prefab name.
        if(!ui) {
            bool native_coat=false;
            auto cloths=Children(s,found,kCloth,"BeyondDynamicBone","BeyondBoneCloth");
            for(int j=0;j<Length(s,cloths);++j)if(Name(s,Item(s,cloths,j))=="MC_coat"){native_coat=true;break;}
            ui=!native_coat;
        }
        auto mesh=s.Call(s.Method(kUnity,"UnityEngine","SkinnedMeshRenderer","get_sharedMesh","","UnityEngine.Mesh",0),r);
        if(!Alive(s,mesh)||s.Int(s.Method(kUnity,"UnityEngine","Mesh","get_vertexCount","","System.Int32",0),mesh)!=int(vertex_count))continue;
        discovery["receivers"].push_back({{"name",name},{"ui",ui},{"instance_id",CandidateInstance(s,r)}});
        // A visible details model takes priority over the world behind its UI.
        if(choice.renderer&&choice.ui&&!ui)continue;
        if(choice.renderer&&choice.ui==ui)CandidateNeed(false,"multiple active Rin receivers in one display context");
        choice={r,found,ui};
    }
    report["live_discovery"]=std::move(discovery);
    return choice;
}

void DiscoverAndStart(Scope& s,const Json& authoring) {
    using namespace live_cloth;
    CandidateNeed(authoring.value("kind","")=="bem-bonecloth-authoring"&&authoring.value("schema",0)==1,"bone cloth authoring format invalid");
    const auto& target=authoring.at("target");
    const auto choice=VisibleReceiver(s,target.at("vertex_count").get<uint32_t>());
    void* receiver=choice.renderer;void* root=choice.root;
    BetterEndfieldNext::CustomModel::BemComponent geometry;
    if(!receiver){stable_receiver_samples=0;stable_receiver={};return;}
    auto receiver_mesh=s.Call(s.Method(kUnity,"UnityEngine","SkinnedMeshRenderer","get_sharedMesh","","UnityEngine.Mesh",0),receiver);
    const std::array<int,3> identity{CandidateInstance(s,receiver),CandidateInstance(s,root),CandidateInstance(s,receiver_mesh)};
    stable_receiver_samples=identity==stable_receiver?stable_receiver_samples+1:1;
    stable_receiver=identity;
    report["live_discovery"]["stable_receiver_samples"]=stable_receiver_samples;
    // Model delivery/LOD setup can replace a visible startup mesh a moment
    // later. Wait across pump samples before taking ownership of its bones.
    if(stable_receiver_samples<3)return;
    if(!ClaimCandidateRequest())return;
    stable_receiver_samples=0;
    CandidateNeed(AndroidReadMeshGeometry(receiver_mesh,geometry),"visible receiver geometry unavailable");
    binding=std::make_unique<Binding>();auto& b=*binding;b.renderer=receiver;b.root=root;
    b.authoring=authoring;
    b.ui=choice.ui;b.vertex_count=target.at("vertex_count").get<uint32_t>();
    b.source=s.Call(s.Method(kUnity,"UnityEngine","SkinnedMeshRenderer","get_sharedMesh","","UnityEngine.Mesh",0),receiver);
    b.source_pin=BetterEndfieldNext::CustomModel::PinModelMeshForCloth(b.source);CandidateNeed(bool(b.source_pin),"source mesh pin unavailable");
    b.source_bones=s.Call(s.Method(kUnity,"UnityEngine","SkinnedMeshRenderer","get_bones","","UnityEngine.Transform[]",0),receiver);
    auto poses_array=s.Call(s.Method(kUnity,"UnityEngine","Mesh","get_bindposes","","UnityEngine.Matrix4x4[]",0),b.source);
    const int n=Length(s,b.source_bones);CandidateNeed(n>0&&n<=256&&Length(s,poses_array)==n,"receiver palette mismatch");
    std::vector<std::string> names;std::vector<CD::M> poses;CD::M pelvis_bind{};
    for(int i=0;i<n;++i) {
        auto bone=Item(s,b.source_bones,i),box=Item(s,poses_array,i);auto ptr=runtime->Unbox(box);
        CandidateNeed(ptr&&Alive(s,bone),"receiver palette entry unavailable");
        float f[16];std::memcpy(f,ptr,sizeof(f));CD::M m{};for(int k=0;k<16;++k)m[k]=f[k];
        names.push_back(Name(s,bone));poses.push_back(m);
        if(names.back()=="Bip001_Pelvis"){CandidateNeed(!b.pelvis,"duplicate pelvis");b.pelvis=bone;pelvis_bind=m;}
    }
    CandidateNeed(b.pelvis,"receiver pelvis missing");
    b.pelvis_bind=pelvis_bind;
    // Donor configuration: the actor's own MC_coat.
    auto cloths=Children(s,root,kCloth,"BeyondDynamicBone","BeyondBoneCloth");void* donor=nullptr;
    for(int i=0;i<Length(s,cloths);++i){auto cl=Item(s,cloths,i);if(Name(s,cl)=="MC_coat")donor=s.Call(s.Method(kCloth,"BeyondDynamicBone","BeyondBoneCloth","get_SerializeData","","BeyondDynamicBone.ClothSerializeData",0),cl);}
    if(!donor&&authoring.contains("donor_recipe")) {
        b.ui=true;
        auto cls=runtime->ResolveClass(kCloth,"BeyondDynamicBone","ClothSerializeData");
        CandidateNeed(cls.info,"UI cloth recipe class unavailable");
        donor=s.Root(runtime->NewObject(cls.info));
        s.Call(s.Method(kCloth,"BeyondDynamicBone","ClothSerializeData",".ctor","","System.Void",0),donor);
        CandidatePatch(s,donor,authoring.at("donor_recipe"));
    }
    CandidateNeed(donor,"MC_coat donor/portable UI recipe unavailable");
    const auto original=Serialize(s,donor);

    candidate=std::make_unique<ClothCandidate>();auto& c=*candidate;c.started=std::chrono::steady_clock::now();
    c.result["display_context"]=b.ui?"details-ui":"world";
    c.result["mode"]="bonecloth";c.result["layers"]=Json::array();c.result["coordinate_space"]="live-pelvis-joints";
    c.Hold(b.source);c.Hold(b.renderer);c.Hold(b.root);c.Hold(b.pelvis);c.Hold(b.source_bones);c.Hold(donor);
    // Under the actor root: the native solver uses the component Transform as
    // its inertia centre, so it must travel with the character.
    c.owner=CandidateGameObject(s,c,"BEM_BoneCloth",b.root);
    // Joints: identity local rotation, pelvis-local bind positions.
    std::vector<CB::Joint> joints;std::vector<std::string> layer_of;
    for(const auto& j:authoring.at("joints")) {
        CB::Joint joint;joint.name=j.at("name");
        if(!j.at("parent").is_null()){auto f=b.joint_index.find(j.at("parent").get<std::string>());CandidateNeed(f!=b.joint_index.end(),"joint parent unknown");joint.parent=f->second;}
        const auto& p=j.at("position");joint.mesh_position={p[0].get<double>(),p[1].get<double>(),p[2].get<double>()};
        if(j.contains("bind_frame")) {
            CandidateNeed(j.at("bind_frame").size()==16,"source joint bind frame size invalid");
            CD::M frame{};for(int k=0;k<16;++k)frame[k]=j.at("bind_frame")[k].get<double>();joint.mesh_frame=frame;
        }
        CandidateNeed(b.joint_index.emplace(joint.name,int(joints.size())).second,"duplicate joint");
        joints.push_back(joint);layer_of.push_back(j.at("layer"));
    }
    CandidateNeed(!joints.empty()&&joints.size()<=128,"joint budget invalid");
    const auto placed=CB::Place(joints,pelvis_bind);
    auto pelvis_go=GameObject(s,b.pelvis);
    for(size_t i=0;i<joints.size();++i) {
        auto parent=joints[i].parent<0?pelvis_go:GameObject(s,b.joints[size_t(joints[i].parent)]);
        auto go=CandidateGameObject(s,c,joints[i].name.c_str(),parent);
        // go is a GameObject, not a Component. Invoking Component.get_transform
        // with this receiver crashes inside Unity before any cloth is built.
        auto t=s.Call(s.Method(kUnity,"UnityEngine","GameObject","get_transform","","UnityEngine.Transform",0),go);
        CandidateNeed(t,"bone cloth joint Transform unavailable");
        SetLocal(s,t,placed.local_positions[i],CB::Quaternion(placed.local_rotations[i]));CandidateActive(s,go,true);
        b.joints.push_back(c.Hold(t));
        b.rest_pelvis.push_back(CD::Point(pelvis_bind,joints[i].mesh_position));
        if(joints[i].parent<0) {
            // A fixed solver particle still gets an output rotation. It must
            // not rotate the sewn waist vertices around its fixed position.
            // The simulation hierarchy stays intact; only its rendered root
            // palette entry uses the original pelvis-relative bind frame.
            auto label=joints[i].name+"_WaistAnchor";
            auto anchor=CandidateGameObject(s,c,label.c_str(),pelvis_go);
            auto at=s.Call(s.Method(kUnity,"UnityEngine","GameObject","get_transform","","UnityEngine.Transform",0),anchor);
            SetLocal(s,at,placed.local_positions[i],CB::Quaternion(placed.local_rotations[i]));
            CandidateActive(s,anchor,true);b.render_joints.push_back(c.Hold(at));
        } else b.render_joints.push_back(t);
    }
    c.result["joints_created"]=joints.size();
    c.result["waist_render_anchors"]=std::count_if(joints.begin(),joints.end(),[](const auto& j){return j.parent<0;});
    c.result["source_joint_frames_preserved"]=std::all_of(joints.begin(),joints.end(),[](const auto& j){return j.mesh_frame.has_value();});
    // Fitted body shapes belong to this candidate and follow the active
    // receiver's skeleton. Never resize the game's shared colliders.
    for(const auto& shape:authoring.value("body_spheres",Json::array())) {
        CandidateNeed(c.body_colliders.size()<64,"fitted body collider budget exceeded");
        const auto name=shape.at("bone").get<std::string>();
        const auto found=std::find(names.begin(),names.end(),name);
        CandidateNeed(found!=names.end(),"fitted body bone missing on receiver");
        const auto index=size_t(found-names.begin());
        const auto& p=shape.at("position");
        const CD::V point{p[0].get<double>(),p[1].get<double>(),p[2].get<double>()};
        float radius=shape.at("radius").get<float>();
        CandidateNeed(std::isfinite(radius)&&radius>0&&radius<=0.25f&&
            std::isfinite(point.x)&&std::isfinite(point.y)&&std::isfinite(point.z),"fitted body geometry invalid");
        auto parent=Item(s,b.source_bones,int(index));
        auto label=std::string("BEM_BodySphere_")+std::to_string(c.body_colliders.size());
        auto go=CandidateGameObject(s,c,label.c_str(),GameObject(s,parent));
        auto t=s.Call(s.Method(kUnity,"UnityEngine","GameObject","get_transform","","UnityEngine.Transform",0),go);
        SetLocal(s,t,CD::Point(poses[index],point));
        auto collider=CandidateComponent(s,c,go,kCloth,"BeyondDynamicBone","BeyondBoneSphereCollider");
        void* size_args[]{&radius};
        s.Call(s.Method(kCloth,"BeyondDynamicBone","BeyondBoneSphereCollider","SetSize","System.Single","System.Void",1),collider,size_args);
        s.Call(s.Method(kCloth,"BeyondDynamicBone","ColliderComponent","UpdateParameters","","System.Void",0),collider);
        CandidateActive(s,go,true);c.body_colliders.push_back(collider);
    }
    Rebind(s,authoring,std::move(geometry),names,poses,placed.bindposes);
    // Layers: inner (consumer) first in the candidate, outer (producer) second.
    for(int i=0;i<2;++i) {
        const auto& spec=authoring.at("layers").at(i);
        CandidateNeed(spec.at("name")==(i?"outer":"inner"),"bone cloth layer order invalid");
        std::vector<int> members;std::set<int> roots;
        for(size_t j=0;j<joints.size();++j)if(layer_of[j]==spec.at("name"))members.push_back(int(j));
        for(const auto& r:spec.at("roots")){auto f=b.joint_index.find(r.get<std::string>());CandidateNeed(f!=b.joint_index.end(),"layer root unknown");roots.insert(f->second);}
        auto& layer=c.layers[i];layer.go=CandidateGameObject(s,c,i?"BEM_BoneCloth_Outer":"BEM_BoneCloth_Inner",c.owner);
        layer.cloth=CandidateComponent(s,c,layer.go,kCloth,"BeyondDynamicBone","BeyondBoneCloth");
        s.Call(s.Method(kCloth,"BeyondDynamicBone","BeyondBoneCloth","DisableAutoBuild","","System.Void",0),layer.cloth);
        ConfigureLayer(s,c,layer,donor,original,spec,members,roots);
        c.result["layers"].push_back({{"name",spec.at("name")},{"joints",members.size()},{"roots",roots.size()},{"connection",spec.at("connection")}});
    }
    CandidateNeed(Serialize(s,donor)==original,"original configuration changed during bone cloth preparation");
    c.result["original_configuration_unchanged"]=true;c.result["prepared"]=true;c.result["build_accepted"]=Json::array();
    contact_hook::Install(s);
    c.result["layer_order_available"]=layer_hook::Install(s);
    c.pair_started=true;c.pair_time=std::chrono::steady_clock::now();
    CandidatePairPolicy(s,c,true,false);c.result["pair_policy_configured"]=true;c.result["pair_configured_before_build"]=true;
    CandidateActive(s,c.owner,true);
    for(int i:{1,0}) {
        auto& layer=c.layers[i];CandidateActive(s,layer.go,true);
        s.Call(s.Method(kCloth,"BeyondDynamicBone","BeyondBoneCloth","Initialize","","System.Void",0),layer.cloth);
        const bool accepted=CandidateBool(s,s.Method(kCloth,"BeyondDynamicBone","BeyondBoneCloth","BuildAndRun","","System.Boolean",0),layer.cloth);
        c.result["build_accepted"].push_back({{"layer",i?"outer":"inner"},{"accepted",accepted}});
        CandidateNeed(accepted,"native bone cloth build refused");
        bool skip=false;void* sk[]{&skip};
        s.Call(s.Method(kCloth,"BeyondDynamicBone","BeyondBoneCloth","SetSkipWriting","System.Boolean","System.Void",1),layer.cloth,sk);
    }
    c.result["status"]="building";c.result["bonecloth_bound"]=true;
}

// Release ownership if the game swaps the mesh/bones or retires the actor.
void Monitor(Scope& s) {
    if(!binding||!binding->applied||!candidate||candidate->finished||candidate->destroying)return;
    auto& b=*binding;
    CandidateNeed(live_cloth::Alive(s,b.renderer)&&live_cloth::Alive(s,b.root),"bone cloth actor retired");
    const auto preferred=VisibleReceiver(s,b.vertex_count);
    if(preferred.renderer&&CandidateInstance(s,preferred.renderer)!=CandidateInstance(s,b.renderer))
        CandidateNeed(false,"bone cloth display context changed");
    CandidateNeed(preferred.renderer,"bone cloth actor inactive");
    auto current=s.Call(s.Method(kUnity,"UnityEngine","SkinnedMeshRenderer","get_sharedMesh","","UnityEngine.Mesh",0),b.renderer);
    if(!live_cloth::Alive(s,current)||!live_cloth::Alive(s,b.mesh)) {
        candidate->result["replacement_mesh_unavailable"]=true;
        CandidateNeed(false,"bone cloth receiver mesh replaced by the game");
    }
    if(CandidateInstance(s,current)!=CandidateInstance(s,b.mesh)) {
        candidate->result["replacement_mesh"]={{"name",live_cloth::Name(s,current)},
            {"instance_id",CandidateInstance(s,current)},{"physics_mesh_id",CandidateInstance(s,b.mesh)}};
        CandidateNeed(false,"bone cloth receiver mesh replaced by the game");
    }
    auto bones=s.Call(s.Method(kUnity,"UnityEngine","SkinnedMeshRenderer","get_bones","","UnityEngine.Transform[]",0),b.renderer);
    CandidateNeed(live_cloth::Length(s,bones)==b.palette,"bone cloth receiver bones replaced");
    CandidateNeed(CandidateBool(s,s.Method(kUnity,"UnityEngine","GameObject","get_activeInHierarchy","","System.Boolean",0),b.root),"bone cloth actor inactive");
    auto inverse_box=s.Call(s.Method(kUnity,"UnityEngine","Transform","get_worldToLocalMatrix","","UnityEngine.Matrix4x4",0),b.pelvis);
    CandidateNeed(runtime->Unbox(inverse_box),"bone cloth pelvis pose unreadable");
    float inverse_values[16];std::memcpy(inverse_values,runtime->Unbox(inverse_box),sizeof(inverse_values));
    CD::M inverse{};for(int k=0;k<16;++k)inverse[k]=inverse_values[k];
    double maximum=0;
    for(size_t i=0;i<b.joints.size();++i) {
        auto box=s.Call(s.Method(kUnity,"UnityEngine","Transform","get_position","","UnityEngine.Vector3",0),b.joints[i]);
        float p[3];CandidateNeed(runtime->Unbox(box),"bone cloth joint pose unreadable");
        std::memcpy(p,runtime->Unbox(box),sizeof(p));
        CD::V delta=CD::Point(inverse,{p[0],p[1],p[2]})-b.rest_pelvis[i];
        const double distance=std::sqrt(CD::Dot(delta,delta));
        CandidateNeed(std::isfinite(distance),"bone cloth joint pose non-finite");
        maximum=std::max(maximum,distance);
    }
    auto& result=candidate->result;
    result["joint_pose_samples"]=result.value("joint_pose_samples",uint64_t{0})+1;
    result["joint_max_pelvis_displacement_m"]=std::max(maximum,result.value("joint_max_pelvis_displacement_m",0.0));
    result["joint_motion_observed"]=result["joint_max_pelvis_displacement_m"].get<double>()>0.0005;
    if(result.value("native_build_verified",false))CaptureSeam(s);
}
}  // namespace bone_cloth

void UpdateBoneLayerOrder(Scope& s) {
    auto& c=*candidate;auto& b=*bone_cloth::binding;
    c.result["layer_order"]=layer_hook::Status();
    if(!c.result.value("layer_order_available",false)||c.result.value("layer_order_armed",false))return;
    try {
        namespace L=BetterEndfieldNext::CustomModel::ClothLayer;
        namespace CD=BetterEndfieldNext::CustomModel::ClothDisplay;
        contact_hook::Complete(s);
        auto policy=std::make_shared<L::Policy>();
        auto box=s.Root(runtime->ReadFieldObject(contact_hook::list_fields[1],contact_hook::manager));
        CandidateNeed(runtime->Unbox(box),"layer order contact list unavailable");
        BetterEndfieldNext::CustomModel::ClothContact::Container list{};
        std::memcpy(&list,runtime->Unbox(box),sizeof(list));policy->list=list.data;
        const auto pelvis=live_cloth::Matrix(s,s.Method(kUnity,"UnityEngine","Transform","get_localToWorldMatrix","","UnityEngine.Matrix4x4",0),b.pelvis);
        for(int layer=0;layer<2;++layer) {
            auto& surface=layer?policy->outer:policy->inner;
            const int team=c.result.at("teams").at(layer).at("team_id");
            const auto data=CandidateTeam(s,team,layer==0);
            const auto positions=CandidateParticleArray(s,data,"basePosArray");
            std::vector<CD::V> reference;std::vector<bool> fixed;
            CD::V center{},tips{};int roots=0,ends=0;
            std::set<std::string> parents;
            for(const auto& j:b.authoring.at("joints"))if(!j.at("parent").is_null())parents.insert(j.at("parent").get<std::string>());
            for(const auto& j:b.authoring.at("joints"))if(j.at("layer")== (layer?"outer":"inner")) {
                const auto& p=j.at("position");CD::V v{p[0].get<double>(),p[1].get<double>(),p[2].get<double>()};
                reference.push_back(v);fixed.push_back(j.at("parent").is_null());
                if(j.at("parent").is_null()){center=center+v;++roots;}
                if(!parents.contains(j.at("name").get<std::string>())){tips=tips+v;++ends;}
            }
            CandidateNeed(roots&&ends&&reference.size()==positions.size(),"layer particle count differs from authored joints");
            surface.center=center*(1.0/roots);surface.axis=CD::Unit(tips*(1.0/ends)-surface.center);
            surface.team=team;surface.start=data.at("particleChunk").at("start");
            std::vector<bool> used(reference.size());double maximum=0;
            for(const auto& row:positions) {
                CD::V observed{row[0].get<double>(),row[1].get<double>(),row[2].get<double>()};
                double best=1e100;size_t index=reference.size();
                for(size_t j=0;j<reference.size();++j)if(!used[j]) {
                    auto expected=CD::Point(pelvis,CD::Point(b.pelvis_bind,reference[j]));
                    auto delta=expected-observed;double d=CD::Dot(delta,delta);
                    if(d<best){best=d;index=j;}
                }
                CandidateNeed(index<reference.size()&&best<0.002*0.002,"native layer particle correspondence is unconfirmed");
                used[index]=true;surface.rest.push_back(reference[index]);maximum=std::max(maximum,std::sqrt(best));
            }
            c.result["layer_order_mapping"][layer?"outer":"inner"]={{"particles",reference.size()},{"max_error_m",maximum}};
        }
        CandidateNeed(L::Valid(*policy),"layer order policy invalid");
        std::shared_ptr<const L::Policy> immutable=policy;
        std::atomic_store_explicit(&layer_hook::policy,immutable,std::memory_order_release);
        c.result["layer_order_armed"]=true;
        c.result.erase("layer_order_mapping_error");
    } catch(const std::exception& e) {c.result["layer_order_mapping_error"]=e.what();}
}

// Configuration references alone do not prove solver registration. Check
// the Process list and each collider's reciprocal team set after async build.
bool BoneBodyRegistration(Scope& s) {
    auto& c=*candidate;bool ready=true;Json layers=Json::array();
    ContactMetadata metadata;
    auto object_class=metadata.Export<Il2CppClass*(*)(void*)>("il2cpp_object_get_class");
    auto method=metadata.Export<const MethodInfo*(*)(Il2CppClass*,const char*,int)>("il2cpp_class_get_method_from_name");
    CandidateNeed(object_class&&method,"body collider collection metadata unavailable");
    for(int i=0;i<2;++i) {
        const auto& layer=c.layers[i];
        int team=CandidateNumber(s,s.Field(layer.process,"ClothProcess","<TeamId>k__BackingField"));
        auto list=s.Field(layer.process,"ClothProcess","colliderList");
        CandidateNeed(list,"body collider Process list unavailable");
        auto cls=object_class(list);
        const int count=s.Int({method(cls,"get_Count",0),nullptr},list);
        CandidateNeed(count>=0&&count<=128,"body collider Process list budget exceeded");
        const int expected=c.result.at("borrowed_collider_count").get<int>()+int(c.body_colliders.size());
        ready&=count==expected;Json rows=Json::array();
        std::set<int> identities;
        for(int j=0;j<count;++j) {
            void* item_args[]{&j};auto collider=s.Call({method(cls,"get_Item",1),nullptr},list,item_args);
            CandidateNeed(collider,"body collider Process entry unavailable");
            const int identity=CandidateInstance(s,collider);
            CandidateNeed(identities.insert(identity).second,"duplicate body collider registration");
            auto t=live_cloth::Transform(s,collider);auto ancestor=t;bool owned=false;
            for(int depth=0;ancestor&&depth<64;++depth) {
                if(live_cloth::GameObject(s,ancestor)==bone_cloth::binding->root){owned=true;break;}
                ancestor=s.Call(s.Method(kUnity,"UnityEngine","Transform","get_parent","","UnityEngine.Transform",0),ancestor);
            }
            CandidateNeed(owned,"body collider belongs to a different actor");
            auto set=s.Field(collider,"ColliderComponent","teamIdSet");
            CandidateNeed(set,"body collider team set unavailable");
            void* team_args[]{&team};
            const bool member=CandidateBool(s,{method(object_class(set),"Contains",1),nullptr},set,team_args);
            const bool enabled=CandidateBool(s,s.Method(kUnity,"UnityEngine","Behaviour","get_isActiveAndEnabled","","System.Boolean",0),collider);
            auto size=s.Call(s.Method(kCloth,"BeyondDynamicBone","ColliderComponent","GetSize","","UnityEngine.Vector3",0),collider);
            auto scale=s.Call(s.Method(kUnity,"UnityEngine","Transform","get_lossyScale","","UnityEngine.Vector3",0),t);
            auto parent=s.Call(s.Method(kUnity,"UnityEngine","Transform","get_parent","","UnityEngine.Transform",0),t);
            float dims[3]{},sc[3]{};
            CandidateNeed(runtime->Unbox(size)&&runtime->Unbox(scale),"body collider geometry readback unavailable");
            std::memcpy(dims,runtime->Unbox(size),sizeof(dims));std::memcpy(sc,runtime->Unbox(scale),sizeof(sc));
            for(int k=0;k<3;++k)CandidateNeed(std::isfinite(dims[k])&&std::isfinite(sc[k])&&sc[k]>0,"body collider scale invalid");
            ready&=member&&enabled;
            rows.push_back({{"instance_id",identity},{"name",live_cloth::Name(s,collider)},
                {"parent",live_cloth::Name(s,parent)},{"team_member",member},{"enabled",enabled},
                {"size",Json::array({dims[0],dims[1],dims[2]})},{"world_scale",Json::array({sc[0],sc[1],sc[2]})}});
        }
        auto manager=s.Call(s.Method(kCloth,"BeyondDynamicBone","MagicaManager","get_Team","","BeyondDynamicBone.TeamManager",0),nullptr);
        void* args[]{&team};
        auto parameters=s.Call(s.Method(kCloth,"BeyondDynamicBone","TeamManager","GetParametersRef","System.Int32","BeyondDynamicBone.ClothParameters&",1),manager,args);
        auto constraint=s.Field(parameters,"ClothParameters","colliderCollisionConstraint");
        const int mode=CandidateNumber(s,s.Field(constraint,"ColliderCollisionConstraint.ColliderCollisionConstraintParams","mode"));
        ready&=mode==CandidateEnum(s,"ColliderCollisionConstraint.Mode","Edge");
        layers.push_back({{"layer",i?"outer":"inner"},{"team_id",team},{"expected",expected},{"effective_collision_mode",mode},{"colliders",rows}});
    }
    c.result["body_registration"]=std::move(layers);c.result["body_collider_registration_verified"]=ready;
    return ready;
}

// Restore before any joint/component destruction.
void BoneDetach(Scope& s) {
    using namespace bone_cloth;
    if(!binding)return;auto& b=*binding;
    if(b.applied&&live_cloth::Alive(s,b.renderer)) {
        auto current=s.Call(s.Method(kUnity,"UnityEngine","SkinnedMeshRenderer","get_sharedMesh","","UnityEngine.Mesh",0),b.renderer);
        if(current==b.mesh&&live_cloth::Alive(s,b.source)) {
            void* ma[]{b.source};s.Call(s.Method(kUnity,"UnityEngine","SkinnedMeshRenderer","set_sharedMesh","UnityEngine.Mesh","System.Void",1),b.renderer,ma);
            void* ba[]{b.source_bones};s.Call(s.Method(kUnity,"UnityEngine","SkinnedMeshRenderer","set_bones","UnityEngine.Transform[]","System.Void",1),b.renderer,ba);
            if(candidate)candidate->result["display_binding_restored"]=true;
        }
    }
    if(b.mesh&&live_cloth::Alive(s,b.mesh)){void* a[]{b.mesh};s.Call(s.Method(kUnity,"UnityEngine","Object","Destroy","UnityEngine.Object","System.Void",1),nullptr,a);}
    binding.reset();
}
