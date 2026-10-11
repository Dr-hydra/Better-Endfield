// The three logo planes identified on the guest terminal. Search only after
// explicit activation; no persistent instance IDs or changes to the table atlas.
BE_ResolvedClassV1 desk_renderer_class{};
struct DeskCandidate { void* renderer; void* material; void* group; int kind; };
struct DeskLayer { void* renderer; void* material; void* group; bool enabled; bool changed=false; int index=0; };
struct DeskDisplay { void* renderer; void* material; void* group; int index; };
std::vector<DeskCandidate> desk_candidates;
std::vector<DeskLayer> desk_layers;
std::vector<DeskDisplay> desk_displays;
void* desk_array=nullptr;
uint32_t desk_array_handle=0;
int desk_index=0, desk_count=0;
uint64_t desk_deadline=0;

void CancelDeskReceivers() {
    if (desk_array_handle && host) host->gchandle_free(host->context,desk_array_handle);
    desk_array_handle=0; desk_array=nullptr; desk_candidates.clear();
}
bool DeskName(const std::string& name, const char* expected) {
    std::string_view value(name), prefix(expected);
    if (value==prefix) return true;
    if (!value.starts_with(prefix)) return false;
    value.remove_prefix(prefix.size());
    return value=="(Clone)" || value.starts_with("(Clone)#");
}
int DeskKind(const std::string& name) {
    constexpr const char* names[]{"logo_bd","logo_bd_02","logo_bd_03",
        "wenli","wenli_02","wenli_03","glow_ad","glow_ad_02","glow_ad_03"};
    for (int i=0;i<9;++i) if (name==names[i]) return i;
    return -1;
}
void* DeskGroup(void* renderer) {
    auto transform=Invoke(ControlTransform,renderer);
    auto internal=IsAlive(transform)?Invoke(InspectParent,transform):nullptr;
    if (!IsAlive(internal) || String(Invoke(GetName,internal))!="Internal") return nullptr;
    auto fx=Invoke(InspectParent,internal);
    if (!IsAlive(fx) || !DeskName(String(Invoke(GetName,fx)),"P_fxint_s_interactive_screen_table_2501")) return nullptr;
    for (int depth=0;depth<20 && IsAlive(fx);++depth) {
        if (DeskName(String(Invoke(GetName,fx)),"int_system_spaceship_guest_terminal")) return internal;
        fx=Invoke(InspectParent,fx);
    }
    return nullptr;
}
void* DeskSingleMaterial(void* renderer) {
    auto array=Invoke(InspectMaterials,renderer); if (!array) return nullptr;
    auto handle=host->gchandle_new(host->context,array,0);
    if (!handle) throw std::runtime_error("Cannot root desk material array");
    void* material=nullptr;
    try {
        int32_t zero=0; void* args[]{&zero};
        if (Value<int32_t>(ControlArrayLength,array,args)==1) material=Invoke(ControlArrayValue,array,args);
    } catch (...) { host->gchandle_free(host->context,handle); throw; }
    host->gchandle_free(host->context,handle); return material;
}
bool DeskMaterial(void* material, int kind) {
    if (!IsAlive(material)) return false;
    const char* expected=kind<3?"M_fx_base01_lv001_ship_monitor_1144":
        kind<6?"M_fx_s_interactive_screen_table_2501":"M_fx_s_interactive_screen_table_2502";
    if (String(Invoke(GetName,material))!=expected) return false;
    auto shader=Invoke(MaterialShader,material);
    if (!IsAlive(shader) || String(Invoke(GetName,shader))!="HGRP/Effect/VFXBaseV2") return false;
    if (kind>=6) return true;
    void* property[]{&texture_property};
    if (!Value<uint8_t>(MaterialHasProperty,material,property)) return false;
    auto texture=Invoke(MaterialGetTexture,material,property);
    return IsAlive(texture) && String(Invoke(GetName,texture))==(kind<3?"T_fx_ui_1103_UI":"T_fx_flow_1104_M");
}
void DeskProperty(void* material, const char* name, float value) {
    void* text=host->string_new(host->context,name); void* args[]{text};
    int32_t id=Value<int32_t>(PropertyId,nullptr,args); void* property[]{&id};
    if (Value<uint8_t>(MaterialHasProperty,material,property)) {
        void* set[]{&id,&value}; Invoke(DeskFloat,material,set);
    }
}
void PrepareDeskMaterial(void* material) {
    // Preserve this plane's shader, depth/blend settings and identity UVs. The
    // original logo tint is grey; vertex/animation alpha should not dim a video.
    DeskProperty(material,"_DisableVertColor",1);
    DeskProperty(material,"_TintColorIntensity",1);
    DeskProperty(material,"_ProcedureAlpha",1);
    DeskProperty(material,"_UseMainTexAsAlpha",0);
    auto text=host->string_new(host->context,"_TintColor"); void* args[]{text};
    int32_t id=Value<int32_t>(PropertyId,nullptr,args); void* property[]{&id};
    if (Value<uint8_t>(MaterialHasProperty,material,property)) {
        struct Color { float r,g,b,a; } white{1,1,1,1};
        void* set[]{&id,&white}; Invoke(DeskColor,material,set);
    }
}
void RestoreDeskLayer(DeskLayer& layer) {
    if (!layer.changed || !IsAlive(layer.renderer)) return;
    // Leave a new material owner alone. Restore only the enabled state changed
    // by this session, while the known original decoration is still assigned.
    if (Invoke(RendererMaterial,layer.renderer)==layer.material && !Value<uint8_t>(DeskEnabled,layer.renderer))
        SetValue<uint8_t>(DeskSetEnabled,layer.renderer,layer.enabled?1:0);
    layer.changed=false;
}
void RestoreDeskLayers(bool restore) {
    if (restore) for (auto& layer:desk_layers) {
        try { RestoreDeskLayer(layer); } catch (...) { Log("Could not restore a desk screen layer"); }
    }
    desk_layers.clear(); desk_displays.clear();
}
void PaintDeskLayers() {
    for (auto& layer:desk_layers) {
        try {
            bool owns=false;
            for (const auto& display:desk_displays) if (display.group==layer.group && display.index==layer.index && IsAlive(display.renderer) &&
                Invoke(RendererMaterial,display.renderer)==display.material) { owns=true; break; }
            if (!owns) { RestoreDeskLayer(layer); continue; }
            if (IsAlive(layer.renderer) && Invoke(RendererMaterial,layer.renderer)==layer.material && Value<uint8_t>(DeskEnabled,layer.renderer)) {
                SetValue<uint8_t>(DeskSetEnabled,layer.renderer,0); layer.changed=true;
            }
        } catch (...) { /* Optional desk decoration cannot interrupt wall video. */ }
    }
}
double DeskAspect(void* renderer) {
    struct Vector3 { float x,y,z; }; struct LocalBounds { Vector3 center,extents; };
    auto bounds=Value<LocalBounds>(DeskLocalBounds,renderer);
    auto transform=Invoke(ControlTransform,renderer);
    auto scale=Value<Vector3>(DeskScale,transform);
    // These verified Plane001 meshes lie in local XY. World AABBs depend on
    // camera-independent screen rotation and cannot give the screen ratio.
    double width=std::abs(double(bounds.extents.x)*scale.x),height=std::abs(double(bounds.extents.y)*scale.y);
    if (!std::isfinite(width) || !std::isfinite(height) || width<.0001 || height<.0001)
        throw std::runtime_error("Desk screen dimensions unavailable");
    auto aspect=width/height;
    if (aspect<.25 || aspect>8) throw std::runtime_error("Desk screen dimensions out of range");
    return aspect;
}
void BindDeskCandidates() {
    size_t count=0;
    for (const auto& candidate:desk_candidates) {
        // wenli is the complete wide screen; logo_bd is only a square logo in
        // its center. Use the logo's known image shader on the full screen plane.
        if (candidate.kind<3 || candidate.kind>=6 || !IsAlive(candidate.renderer) || !IsAlive(candidate.material)) continue;
        // A duplicate leaf in one group is ambiguous and is never adopted.
        int duplicates=0,logos=0; void* logo_material=nullptr;
        for (const auto& other:desk_candidates) if (other.group==candidate.group) {
            if (other.kind==candidate.kind) ++duplicates;
            if (other.kind==candidate.kind-3 && IsAlive(other.renderer) && Invoke(RendererMaterial,other.renderer)==other.material) {
                ++logos; logo_material=other.material;
            }
        }
        if (duplicates!=1 || logos!=1 || Invoke(RendererMaterial,candidate.renderer)!=candidate.material) continue;
        auto checkpoint=assets.size();
        try {
            auto aspect=DeskAspect(candidate.renderer);
            auto material=New(material_class); void* copy[]{logo_material}; Invoke(MaterialCtor,material,copy);
            PrepareDeskMaterial(material);
            auto name=String(Invoke(GetName,candidate.renderer));
            desk_displays.push_back({candidate.renderer,material,candidate.group,candidate.kind-3});
            try { receivers.push_back({candidate.renderer,material,candidate.material,true,false,aspect}); }
            catch (...) { desk_displays.pop_back(); throw; }
            ++count;
            Log("Desk screen bound: "+name+" texture=_MainTex aspect="+std::to_string(aspect)+"; full monitor plane");
        } catch (const std::exception& error) {
            while (assets.size()>checkpoint) {
                auto asset=assets.back(); assets.pop_back();
                if (asset.owned && IsAlive(asset.object)) { try { void* args[]{asset.object}; Invoke(Destroy,nullptr,args); } catch (...) {} }
                host->gchandle_free(host->context,asset.handle);
            }
            Log(std::string("Desk screen skipped: ")+error.what());
        }
    }
    for (const auto& candidate:desk_candidates) {
        if ((candidate.kind>=3 && candidate.kind<6) || !IsAlive(candidate.renderer)) continue;
        bool selected=false;
        int index=candidate.kind<3?candidate.kind:candidate.kind-6;
        for (const auto& display:desk_displays) if (display.group==candidate.group && display.index==index) { selected=true; break; }
        if (!selected || Invoke(RendererMaterial,candidate.renderer)!=candidate.material) continue;
        desk_layers.push_back({candidate.renderer,candidate.material,candidate.group,Value<uint8_t>(DeskEnabled,candidate.renderer)!=0,false,index});
    }
    if (count) paint_confirmed=false;
    Log("Desk screen binding finished: displays="+std::to_string(count)+" decoration_layers="+std::to_string(desk_layers.size())+"; shared media source");
}
void BeginDeskReceivers() {
    if (!running.desk_screen_enabled) return;
    if (!desk_renderer_class.type_object) { Log("Desk screen binding unavailable: Renderer class missing"); return; }
    try {
        for (auto id:{InspectFind,InspectMaterials,InspectParent,ControlTransform,ControlArrayLength,ControlArrayValue,
                      MaterialShader,DeskEnabled,DeskSetEnabled,DeskFloat,DeskColor,DeskLocalBounds,DeskScale,BlitCrop})
            if (!methods[id].resolved.method_info) { Log("Desk screen binding unavailable: missing interface "+std::to_string(id)); return; }
        uint8_t inactive=0; void* args[]{desk_renderer_class.type_object,&inactive};
        desk_array=Invoke(InspectFind,nullptr,args); if (!desk_array) return;
        desk_array_handle=host->gchandle_new(host->context,desk_array,0);
        if (!desk_array_handle) throw std::runtime_error("Cannot root desk screen search");
        int32_t zero=0; void* size[]{&zero}; desk_count=Value<int32_t>(ControlArrayLength,desk_array,size);
        if (desk_count<0 || desk_count>100000) throw std::runtime_error("Desk screen search exceeds bounds");
        desk_index=0; desk_deadline=Now()+10000;
        Log("Desk screen search started: renderers="+std::to_string(desk_count)+"; manual activation");
    } catch (const std::exception& error) { CancelDeskReceivers(); Log(std::string("Desk screen search unavailable: ")+error.what()); }
}
void DeskReceiversFrame() {
    if (!desk_array) return;
    try {
        auto tick=Now(); int batch=0;
        while (desk_index<desk_count && Now()-tick<3 && ++batch<=64) {
            int32_t index=desk_index++; void* args[]{&index}; auto renderer=Invoke(ControlArrayValue,desk_array,args);
            if (!IsAlive(renderer)) continue;
            int kind=DeskKind(String(Invoke(GetName,renderer))); if (kind<0) continue;
            auto group=DeskGroup(renderer); if (!group) continue;
            auto material=DeskSingleMaterial(renderer); if (!DeskMaterial(material,kind)) continue;
            if (desk_candidates.size()>=36) throw std::runtime_error("Too many matching desk screen candidates");
            Keep(renderer,false); Keep(material,false); Keep(group,false);
            desk_candidates.push_back({renderer,material,group,kind});
        }
        if (desk_index>=desk_count) { BindDeskCandidates(); CancelDeskReceivers(); }
        else if (Now()>=desk_deadline) { CancelDeskReceivers(); Log("Desk screen search timed out; wall projection continues"); }
    } catch (const std::exception& error) { CancelDeskReceivers(); Log(std::string("Desk screen search stopped: ")+error.what()); }
}
void DeskContracts() {
    Class(kCore,"UnityEngine","Renderer",desk_renderer_class,false);
    auto core=[](MethodId id,const char* klass,const char* name,const char* args,const char* result,int count) { Resolve(id,kCore,"UnityEngine",klass,name,args,result,count,false); };
    core(ControlTransform,"Component","get_transform",nullptr,"UnityEngine.Transform",0);
    Resolve(ControlArrayLength,"mscorlib.dll","System","Array","GetLength","System.Int32","System.Int32",1,false);
    Resolve(ControlArrayValue,"mscorlib.dll","System","Array","GetValue","System.Int32","System.Object",1,false);
    core(InspectFind,"Object","FindObjectsOfType","System.Type|System.Boolean","UnityEngine.Object[]",2);
    core(InspectMaterials,"Renderer","get_sharedMaterials",nullptr,"UnityEngine.Material[]",0);
    core(InspectParent,"Transform","get_parent",nullptr,"UnityEngine.Transform",0);
    core(DeskEnabled,"Renderer","get_enabled",nullptr,"System.Boolean",0);
    core(DeskSetEnabled,"Renderer","set_enabled","System.Boolean","System.Void",1);
    core(DeskFloat,"Material","SetFloat","System.Int32|System.Single","System.Void",2);
    core(DeskColor,"Material","SetColor","System.Int32|UnityEngine.Color","System.Void",2);
    core(DeskLocalBounds,"Renderer","get_localBounds",nullptr,"UnityEngine.Bounds",0);
    core(DeskScale,"Transform","get_lossyScale",nullptr,"UnityEngine.Vector3",0);
    core(BlitCrop,"Graphics","Blit","UnityEngine.Texture|UnityEngine.RenderTexture|UnityEngine.Vector2|UnityEngine.Vector2","System.Void",4);
    Resolve(VideoFrameIndex,kVideo,"UnityEngine.Video","VideoPlayer","get_frame",nullptr,"System.Int64",0,false);
    Resolve(VideoWidth,kVideo,"UnityEngine.Video","VideoPlayer","get_width",nullptr,"System.UInt32",0,false);
    Resolve(VideoHeight,kVideo,"UnityEngine.Video","VideoPlayer","get_height",nullptr,"System.UInt32",0,false);
}
