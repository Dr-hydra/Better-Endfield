// Opt-in, bounded metadata collection at the existing resource delivery boundary.
// Included inside module.cpp's anonymous namespace after the read helpers.
struct NativeProbeItem { std::string resource,path,mesh,id; uint32_t indices=0; };
struct NativeProbeState {
    std::string run,manifest,cl;
    std::filesystem::path request,output;
    std::vector<NativeProbeItem> items;
    std::set<std::string> done;
    std::set<std::string> resources;
    std::map<std::string,int> attempts;
    bool active=false,sweep=false,persistent=false;
} g_probe;
std::filesystem::path g_probe_status_path;
uint64_t g_probe_delivery_count=0;
std::vector<std::string> g_probe_seen_resources;
std::string ProbeFloat(float value) {
    std::array<char,64> data{};
    const auto result=std::to_chars(data.data(),data.data()+data.size(),value,std::chars_format::general,9);
    return std::string(data.data(),result.ptr);
}
std::string ProbeJson(std::string_view text) {
    const char* hex="0123456789abcdef";
    std::string out="\"";
    for (unsigned char c:text) {
        if (c=='"' || c=='\\') { out+='\\'; out+=c; }
        else if (c<32) { out+="\\u00"; out+=hex[c>>4]; out+=hex[c&15]; }
        else out+=c;
    }
    return out+'"';
}
std::string ProbeCanonicalPath(std::string path) {
    // Match the existing resource router's Unity clone suffix handling, but
    // only at the root; preserve all child identities verbatim.
    const auto slash=path.find('/');
    const auto root=path.substr(0,slash);
    constexpr std::string_view clone="(Clone)";
    if(root.ends_with(clone)) path.erase(root.size()-clone.size(),clone.size());
    return path;
}
void WriteProbeStatus(std::string_view stage,std::string_view detail={}) noexcept {
    if (g_probe_status_path.empty()) return;
    try {
        HMODULE module=nullptr; std::array<wchar_t,32768> filename{};
        GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS|GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
            reinterpret_cast<LPCWSTR>(&WriteProbeStatus),&module);
        GetModuleFileNameW(module,filename.data(),static_cast<DWORD>(filename.size()));
        const auto encoded=std::filesystem::path(filename.data()).u8string();
        const std::string module_path(reinterpret_cast<const char*>(encoded.data()),encoded.size());
        const auto temporary=g_probe_status_path.wstring()+L".tmp";
        std::ofstream out(temporary,std::ios::binary|std::ios::trunc);
        out<<"{\"schema\":1,\"build\":\"native-probe-sweep-v2\",\"process_id\":"<<GetCurrentProcessId()
            <<",\"module_path\":"<<ProbeJson(module_path)<<",\"stage\":"<<ProbeJson(stage)
            <<",\"detail\":"<<ProbeJson(detail)<<",\"run\":"<<ProbeJson(g_probe.run)
            <<",\"delivery_count\":"<<g_probe_delivery_count<<",\"seen_resources\":[";
        for(size_t i=0;i<g_probe_seen_resources.size();++i) { if(i) out<<','; out<<ProbeJson(g_probe_seen_resources[i]); }
        out<<"],\"captured_resources\":[";
        bool first=true; for(const auto& name:g_probe.done) { if(!first) out<<','; first=false; out<<ProbeJson(name); }
        const auto output_name=g_probe.output.filename().u8string();
        out<<"],\"mode\":"<<ProbeJson(g_probe.sweep?"sweep":"targeted")
           <<",\"persistent\":"<<(g_probe.persistent?"true":"false")
           <<",\"session_file\":"<<ProbeJson(std::string(reinterpret_cast<const char*>(output_name.data()),output_name.size()))
           <<",\"expected_resources\":"<<g_probe.resources.size()<<",\"attempts\":{";
        first=true; for(const auto& [name,attempt]:g_probe.attempts) { if(!first) out<<','; first=false; out<<ProbeJson(name)<<':'<<attempt; }
        out<<"}}"; out.close();
        if(out) MoveFileExW(temporary.c_str(),g_probe_status_path.c_str(),MOVEFILE_REPLACE_EXISTING);
    } catch (...) { /* Diagnostics must never interrupt resource delivery. */ }
}
void BeginProbeStatus() {
    g_probe_status_path.clear(); g_probe_delivery_count=0; g_probe_seen_resources.clear();
    std::array<wchar_t,32768> local{};
    const DWORD length=GetEnvironmentVariableW(L"LOCALAPPDATA",local.data(),static_cast<DWORD>(local.size()));
    if(!length || length>=local.size()) return;
    const auto root=std::filesystem::path(local.data())/"BetterEndfield"/"catalog"/"custom-model";
    if(!std::filesystem::exists(root/"native-probe.request")) return;
    g_probe_status_path=root/"native-probe.status.json";
    WriteProbeStatus("initializing");
}
bool ParseProbeRequest(std::istream& in,NativeProbeState& state) {
    std::string line;
    bool malformed=false;
    const auto read=[&](std::string& value) {
        if (!std::getline(in,value)) return false;
        if (value.size()>2048) { malformed=true; return false; }
        if (!value.empty() && value.back()=='\r') value.pop_back();
        return true;
    };
    if (!read(line) || (line!="BE_NATIVE_PROBE_V1" && line!="BE_NATIVE_PROBE_SWEEP_V1" && line!="BE_NATIVE_PROBE_SWEEP_PERSIST_V1")) return false;
    state.persistent=line=="BE_NATIVE_PROBE_SWEEP_PERSIST_V1";
    state.sweep=state.persistent || line=="BE_NATIVE_PROBE_SWEEP_V1";
    if (!read(state.run) || state.run.empty() || state.run.size()>80 ||
        state.run.find_first_not_of("abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-")!=line.npos ||
        !read(state.manifest) || state.manifest.empty() || !read(state.cl)) return false;
    std::set<std::string> identities,paths,resources;
    while (read(line)) {
        if (line.empty()) continue;
        if(state.sweep) {
            if(!line.starts_with("chr_") || !(line.ends_with("_uimodel") || line.ends_with("_postmodel")) ||
               line.find_first_not_of("abcdefghijklmnopqrstuvwxyz0123456789_")!=line.npos || line.size()>128 ||
               state.resources.size()>=512 || !state.resources.insert(line).second) return false;
            continue;
        }
        std::vector<std::string> fields;
        size_t start=0,pos=0;
        while ((pos=line.find('\t',start))!=line.npos) { fields.push_back(line.substr(start,pos-start)); start=pos+1; }
        fields.push_back(line.substr(start));
        if (fields.size()!=5 || state.items.size()>=16) return false;
        for (const auto& f:fields) if (f.empty() || f.find_first_of("\r\n")!=f.npos) return false;
        if (!fields[1].starts_with(fields[0]+"/") || !identities.insert(fields[4]).second ||
            !paths.insert(fields[1]).second) return false;
        uint64_t count=0;
        for (char c:fields[3]) { if (c<'0'||c>'9') return false; count=count*10+(c-'0'); if (count>UINT32_MAX) return false; }
        if (!count || count%3) return false;
        resources.insert(fields[0]);
        if (resources.size()>2) return false;
        state.items.push_back({fields[0],fields[1],fields[2],fields[4],static_cast<uint32_t>(count)});
    }
    return !malformed && in.eof() && (state.sweep?!state.resources.empty():!state.items.empty());
}
void ReadProbeRequest(const std::filesystem::path& root) {
    g_probe={};
    const auto request=root/"native-probe.request";
    const auto encoded=request.u8string();
    const std::string requested_path(reinterpret_cast<const char*>(encoded.data()),encoded.size());
    if (!std::filesystem::exists(request)) { WriteProbeStatus("request_not_found",requested_path); return; }
    if (g_probe_status_path.empty()) g_probe_status_path=root/"native-probe.status.json";
    if (std::filesystem::file_size(request)>131072) { WriteProbeStatus("request_invalid","too large"); Log("Native probe request too large; disabled."); return; }
    std::ifstream in(request,std::ios::binary);
    NativeProbeState state;
    if (!ParseProbeRequest(in,state)) { WriteProbeStatus("request_invalid",requested_path); Log("Invalid native probe request; disabled."); return; }
    std::ifstream stop(root/"native-probe.stop"); std::string stopped_run; std::getline(stop,stopped_run);
    if(stopped_run==state.run) { WriteProbeStatus("stopped","This collection was explicitly stopped."); return; }
    state.request=request;
    const auto suffix=state.persistent?"."+std::to_string(GetCurrentProcessId())+"."+std::to_string(GetTickCount64()):"";
    state.output=root/"native-probe"/(state.run+suffix+".jsonl");
    std::filesystem::create_directories(state.output.parent_path());
    // A run ID cannot append across launches and accidentally mix observations.
    if (std::filesystem::exists(state.output)) { WriteProbeStatus("run_already_exists",state.run); Log("Native probe run already exists; generate a fresh request."); return; }
    state.active=true; g_probe=std::move(state);
    Log("Native probe armed: "+g_probe.run+" targets="+std::to_string(g_probe.items.size()));
    WriteProbeStatus("armed",requested_path);
}
std::string ProbeRenderer(const NativeProbeItem& expected,void* renderer,bool& ok) {
    std::string json="{\"id\":"+ProbeJson(expected.id)+",\"resource_root\":"+ProbeJson(expected.resource)+
        ",\"path\":"+ProbeJson(expected.path)+",\"mesh_name\":"+ProbeJson(expected.mesh);
    void* mesh=Invoke(Contract("skinned.get_shared_mesh"),renderer,nullptr);
    int32_t subs=0,vertices=0; uint64_t count=0;
    VertexDeclaration declaration{}; std::vector<int32_t> strides;
    bool valid=mesh && ObjectName(mesh)==expected.mesh && ReadVertexDeclaration(mesh,declaration) && ReadMeshStrides(mesh,strides) &&
        InvokeValue(Contract("mesh.get_vertex_count"),mesh,nullptr,vertices) &&
        InvokeValue(Contract("mesh.get_sub_mesh_count"),mesh,nullptr,subs) && subs>0 && subs<=256;
    for (int32_t s=0;valid && s<subs;++s) { uint32_t n=0; void* args[]{&s}; valid=InvokeValue(Contract("mesh.get_index_count"),mesh,args,n); count+=n; }
    valid=valid && count>0 && count<=UINT32_MAX && (expected.indices==0 || count==expected.indices);
    json+=",\"original_index_count\":"+std::to_string(count)+",\"vertex_count\":"+std::to_string(vertices)+
        ",\"submesh_count\":"+std::to_string(subs)+",\"strides\":[";
    for (size_t i=0;i<strides.size();++i) { if (i) json+=','; json+=std::to_string(strides[i]); }
    json+="],\"attributes\":[";
    for (int i=0;i<declaration.count;++i) { const auto& a=declaration.entries[i]; if(i) json+=',';
        json+='['+std::to_string(a.attribute)+','+std::to_string(a.format)+','+std::to_string(a.dimension)+','+std::to_string(a.stream)+']'; }
    json+="],\"bones\":[";
    void* bones=Invoke(Contract("skinned.get_bones"),renderer,nullptr); int bone_count=ArrayLength(bones);
    if (bone_count<=0 || bone_count>1024) { valid=false; bone_count=0; }
    for (int i=0;i<bone_count;++i) { if(i) json+=','; void* bone=ArrayValue(bones,i);
        const auto name=ObjectName(bone); if (name.empty()) valid=false;
        json+="{\"name\":"+ProbeJson(name)+",\"path\":"+ProbeJson(ProbeCanonicalPath(BuildTransformPath(bone)))+'}'; }
    json+=']';
    if(g_probe.sweep) {
        void* poses=Invoke(Contract("mesh.get_bindposes"),mesh,nullptr,false);
        const int pose_count=ArrayLength(poses);
        if(pose_count!=bone_count || pose_count<=0 || pose_count>1024) valid=false;
        json+=",\"bindposes\":[";
        for(int i=0;i<pose_count && i<1024;++i) {
            if(i) json+=','; Matrix4x4Raw matrix{};
            const bool readable=Unbox(ArrayValue(poses,i),matrix); valid=valid && readable;
            json+='[';
            for(int j=0;j<16;++j) { if(j) json+=',';
                const bool finite=std::isfinite(matrix.m[j]); valid=valid && finite;
                json+=finite?ProbeFloat(matrix.m[j]):"null";
            }
            json+=']';
        }
        json+=']';
    }
    json+=",\"materials\":[";
    void* materials=Invoke(Contract("renderer.get_shared_materials"),renderer,nullptr); int material_count=ArrayLength(materials);
    if (material_count<=0 || material_count>256) { valid=false; material_count=0; }
    for (int i=0;i<material_count;++i) {
        if(i) json+=','; void* material=ArrayValue(materials,i);
        void* shader=Invoke(Contract("probe.material_shader"),material,nullptr,false);
        json+="{\"slot\":"+std::to_string(i)+",\"name\":"+ProbeJson(ObjectName(material))+
            ",\"shader\":"+ProbeJson(ObjectName(shader))+",\"textures\":[";
        // These same ID-based contracts already serve production texture replacement.
        // GetTexturePropertyNames is stripped from the live client; do not depend on it.
        void* ids=Invoke(Contract("material.get_texture_property_ids"),material,nullptr,false); int texture_count=ArrayLength(ids);
        if (!material || !shader || !ids || texture_count>256) { valid=false; texture_count=0; }
        for (int t=0;t<texture_count;++t) {
            if(t) json+=','; int32_t id=0; const bool id_ok=Unbox(ArrayValue(ids,t),id); valid=valid && id_ok;
            void* args[]{&id};
            void* texture=id_ok?Invoke(Contract("material.get_texture_by_id"),material,args,false):nullptr;
            int32_t w=0,h=0,format=0;
            if (texture) valid=InvokeValue(Contract("texture.get_width"),texture,nullptr,w) &&
                InvokeValue(Contract("texture.get_height"),texture,nullptr,h) &&
                InvokeValue(Contract("texture.get_graphics_format"),texture,nullptr,format) && valid;
            json+="{\"property\":null,\"property_id\":"+std::to_string(id)+",\"name\":"+ProbeJson(ObjectName(texture))+
                ",\"width\":"+std::to_string(w)+",\"height\":"+std::to_string(h)+",\"graphics_format\":"+std::to_string(format)+'}';
        }
        json+="]}";
    }
    json+="] ,\"complete\":"+std::string(valid?"true":"false")+'}';
    ok=ok && valid; return json;
}
void CaptureNativeProbe(void* asset) {
    if (!g_probe.active || !asset) return;
    const auto observed_resource=ObjectName(asset);
    const auto resource=ProbeCanonicalPath(observed_resource);
    ++g_probe_delivery_count;
    if (g_probe_delivery_count==1) WriteProbeStatus("receiving_resources",resource);
    if ((resource.find("postmodel")!=resource.npos || resource.find("uimodel")!=resource.npos ||
         resource.find("zhuang")!=resource.npos) && g_probe_seen_resources.size()<512 &&
        std::find(g_probe_seen_resources.begin(),g_probe_seen_resources.end(),resource)==g_probe_seen_resources.end()) {
        g_probe_seen_resources.push_back(resource); WriteProbeStatus("receiving_resources",resource);
    }
    std::vector<const NativeProbeItem*> targets;
    for (const auto& item:g_probe.items) if(item.resource==resource) targets.push_back(&item);
    if ((g_probe.sweep?!g_probe.resources.contains(resource):targets.empty()) || g_probe.done.contains(resource) || g_probe.attempts[resource]>=3) return;
    ++g_probe.attempts[resource];
    bool inactive=true; void* args[]{g_skinned_renderer_class.type_object,&inactive};
    void* renderers=Invoke(Contract("game_object.renderers"),asset,args); int count=ArrayLength(renderers);
    const bool can_enumerate=count>0 && count<=(g_probe.sweep?512:4096);
    bool complete=can_enumerate;
    std::string rows="["; bool first=true;
    if(g_probe.sweep) {
        std::set<std::string> paths;
        for(int i=0;can_enumerate && i<count;++i) {
            void* renderer=ArrayValue(renderers,i);
            const auto path=ProbeCanonicalPath(BuildTransformPath(renderer));
            void* mesh=Invoke(Contract("skinned.get_shared_mesh"),renderer,nullptr,false);
            const auto mesh_name=ObjectName(mesh);
            if(!path.starts_with(resource+"/") || !paths.insert(path).second || mesh_name.empty() ||
               mesh_name.starts_with("BetterEndfield.")) { complete=false; continue; }
            NativeProbeItem observed{resource,path,mesh_name,"runtime:"+path,0};
            if(!first) rows+=','; first=false;
            rows+=ProbeRenderer(observed,renderer,complete);
            if(rows.size()>16*1024*1024) { WriteProbeStatus("capture_limit",resource); return; }
        }
    }
    for (const auto* expected:targets) {
        void* found=nullptr; bool ambiguous=false;
        for (int i=0;can_enumerate && i<count;++i) {
            void* renderer=ArrayValue(renderers,i);
            if (ObjectName(renderer)!=expected->mesh || ProbeCanonicalPath(BuildTransformPath(renderer))!=expected->path) continue;
            if (found) ambiguous=true; else found=renderer;
        }
        if (!found || ambiguous) { complete=false; Log("Native probe target missing/ambiguous: "+expected->path); continue; }
        if(!first) rows+=','; first=false; rows+=ProbeRenderer(*expected,found,complete);
    }
    rows+=']';
    if(std::filesystem::exists(g_probe.output) && std::filesystem::file_size(g_probe.output)>512ull*1024*1024) {
        g_probe.active=false; WriteProbeStatus("capture_limit","session size limit"); return;
    }
    std::ofstream out(g_probe.output,std::ios::binary|std::ios::app);
    out<<"{\"schema\":1,\"run\":"<<ProbeJson(g_probe.run)<<",\"manifest_version\":"<<ProbeJson(g_probe.manifest)
       <<",\"perforce_cl\":"<<ProbeJson(g_probe.cl)<<",\"resource_root\":"<<ProbeJson(resource)
       <<",\"observed_resource_root\":"<<ProbeJson(observed_resource)
       <<",\"process_id\":"<<GetCurrentProcessId()<<",\"mode\":"<<ProbeJson(g_probe.sweep?"sweep":"targeted")
       <<",\"renderer_count\":"<<count<<",\"complete\":"<<(complete?"true":"false")<<",\"renderers\":"<<rows<<"}\n";
    out.flush();
    if (!out) { WriteProbeStatus("capture_write_failed",resource); Log("Native probe write failed."); return; }
    Log("Native probe captured: "+resource+" complete="+std::to_string(complete));
    if (complete) g_probe.done.insert(resource);
    WriteProbeStatus(complete?"captured":"capture_incomplete",resource);
    bool all=true;
    if(g_probe.sweep) { for(const auto& name:g_probe.resources) if(!g_probe.done.contains(name)) all=false; }
    else { for(const auto& item:g_probe.items) if(!g_probe.done.contains(item.resource)) all=false; }
    if (all && !g_probe.persistent) {
        g_probe.active=false;
        const auto completed=g_probe.request.parent_path()/(g_probe.run+".completed");
        if (std::filesystem::exists(g_probe.request) && !MoveFileExW(g_probe.request.c_str(),completed.c_str(),MOVEFILE_REPLACE_EXISTING)) Log("Native probe disarm rename failed; run-ID guard remains active.");
        Log("Native probe complete and disabled: "+g_probe.run);
        WriteProbeStatus("complete");
    }
}
void TickNativeProbe() {
    if(!g_probe.active || !g_probe.sweep) return;
    std::ifstream stop(g_probe.request.parent_path()/"native-probe.stop");
    std::string run; std::getline(stop,run);
    if(run!=g_probe.run) return;
    g_probe.active=false;
    WriteProbeStatus("stopped","Raw observations saved; model replacement remains paused until restart.");
}
