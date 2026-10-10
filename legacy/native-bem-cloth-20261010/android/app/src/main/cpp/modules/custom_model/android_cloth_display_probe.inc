// Packed channel writes are tested on an unpublished clone before any actor
// binding. This cannot remove or deform the user's visible clothing.
void* display_probe_mesh=nullptr;
uint32_t display_probe_handle=0;
bool FinishDisplayProbe(Scope& s) {
    if(!display_probe_mesh)return true;
    void* args[]{display_probe_mesh};
    auto alive=s.Method(kUnity,"UnityEngine","Object","op_Implicit","UnityEngine.Object","System.Boolean",1);
    if(CandidateBool(s,alive,nullptr,args))return false;
    runtime->FreeGcHandle(display_probe_handle);display_probe_handle=0;display_probe_mesh=nullptr;
    report["display_writer"]["cleanup_confirmed"]=true;return true;
}
void ProbeDisplayWriter(Scope& s,void* root) {
    if(report.contains("display_writer"))return;
    const auto renderer_type=runtime->ResolveClass(kUnity,"UnityEngine","SkinnedMeshRenderer");
    CandidateNeed(renderer_type.type_object,"display source type unavailable");
    bool inactive=true;void* args[]{renderer_type.type_object,&inactive};
    auto array=s.Call(s.Method(kUnity,"UnityEngine","GameObject","GetComponentsInChildren",
        "System.Type|System.Boolean","UnityEngine.Component[]",2),root,args);
    const auto length=s.Method("mscorlib.dll","System","Array","get_Length","","System.Int32",0);
    const auto item=s.Method("mscorlib.dll","System","Array","GetValue","System.Int32","System.Object",1);
    const auto name=s.Method(kUnity,"UnityEngine","Object","get_name","","System.String",0);
    const auto mesh_get=s.Method(kUnity,"UnityEngine","SkinnedMeshRenderer","get_sharedMesh","","UnityEngine.Mesh",0);
    const int count=s.Int(length,array);CandidateNeed(count>=0&&count<=128,"display renderer budget invalid");
    for(int i=0;i<count;++i) {
        void* at[]{&i};auto renderer=s.Call(item,array,at);const auto label=s.String(s.Call(name,renderer));
        if(label!="S_actor_aglina_cloth_01_lod0"&&label!="S_actor_aglina_cloth_01_lod1")continue;
        auto source=s.Call(mesh_get,renderer);BetterEndfieldNext::CustomModel::BemComponent original;
        if(!AndroidReadMeshGeometry(source,original)||original.info.vertex_count!=18588)continue;
        if(!ClaimCandidateRequest("display_writer"))return;
        report["display_writer"]={{"status","checking"},{"source_renderer",label},{"render_binding_applied",false}};
        try {
            CandidateNeed(original.info.stride0==16&&original.info.stride1==12&&original.info.stride2==12,
                "display source is not the authored packed layout");
            void* clone_args[]{source};display_probe_mesh=s.Call(s.Method(kUnity,"UnityEngine","Object","Instantiate",
                "UnityEngine.Object","UnityEngine.Object",1),nullptr,clone_args);
            display_probe_handle=runtime->NewGcHandle(display_probe_mesh,false);
            CandidateNeed(display_probe_mesh&&display_probe_handle,"display clone unavailable");
            std::vector<float> positions(size_t(original.info.vertex_count)*3);
            std::vector<uint32_t> frames(original.info.vertex_count);
            for(size_t v=0;v<frames.size();++v) {
                std::memcpy(positions.data()+v*3,original.streams[0].data()+v*16,12);
                std::memcpy(&frames[v],original.streams[0].data()+v*16+12,4);
            }
            const float old=positions[0];positions[0]+=0.001f;
            CandidateNeed(AndroidUpdateMeshPackedGeometry(display_probe_mesh,positions,frames),"packed channel setter unavailable or rejected");
            BetterEndfieldNext::CustomModel::BemComponent edited;
            CandidateNeed(AndroidReadMeshGeometry(display_probe_mesh,edited),"edited display clone unreadable");
            auto expected=original.streams;std::memcpy(expected[0].data(),positions.data(),4);
            CandidateNeed(edited.attributes==original.attributes&&edited.streams==expected&&edited.indices==original.indices,
                "packed channel write changed layout or unrelated bytes");
            positions[0]=old;CandidateNeed(AndroidUpdateMeshPackedGeometry(display_probe_mesh,positions,frames),"display clone restoration failed");
            CandidateNeed(AndroidReadMeshGeometry(display_probe_mesh,edited)&&edited.attributes==original.attributes&&
                edited.streams==original.streams&&edited.indices==original.indices,"display clone roundtrip differs");
            BetterEndfieldNext::CustomModel::BemComponent source_after;
            CandidateNeed(AndroidReadMeshGeometry(source,source_after)&&source_after.attributes==original.attributes&&
                source_after.streams==original.streams&&source_after.indices==original.indices,"display source changed");
            report["display_writer"].update({{"status","packed_writer_verified"},{"source_unchanged",true},
                {"position_edit_verified",true},{"restoration_verified",true},{"layout_preserved",true}});
        }catch(const std::exception& e) {
            report["display_writer"]["status"]="failed";report["display_writer"]["error"]=e.what();
            report["display_writer"]["setter_signature"]=runtime->DescribeMethod(kUnity,"UnityEngine","Mesh","SetNativeArrayForChannelImpl");
        }
        if(display_probe_mesh) {
            void* destroy_args[]{display_probe_mesh};s.Call(s.Method(kUnity,"UnityEngine","Object","Destroy",
                "UnityEngine.Object","System.Void",1),nullptr,destroy_args);
        }else report["display_writer"]["cleanup_confirmed"]=true;
        return;
    }
}
