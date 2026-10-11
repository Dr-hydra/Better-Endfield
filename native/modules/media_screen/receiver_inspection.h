// One bounded, read-only survey after explicit projection activation. No object
// is bound or changed by this survey; it only helps identify new display targets.
BE_ResolvedClassV1 inspection_renderer_class{};
struct Bounds { Vec3 center, extents; };
void* inspection_array=nullptr;
uint32_t inspection_handle=0;
int inspection_index=0, inspection_count=0, inspection_nearby=0;
uint64_t inspection_deadline=0;
bool inspection_done=false;
Vec3 inspection_camera{};
void CancelReceiverInspection() {
    if (inspection_handle && host) host->gchandle_free(host->context,inspection_handle);
    inspection_handle=0; inspection_array=nullptr;
}
std::string InspectionPath(void* renderer) {
    auto transform=Invoke(ControlTransform,renderer); std::string path;
    for (int i=0;i<24 && IsAlive(transform);++i) {
        std::string name=String(Invoke(GetName,transform));
        path=path.empty()?name:name+"/"+path;
        transform=Invoke(InspectParent,transform);
    }
    return path;
}
std::string InspectionVector(Vec3 value) {
    return std::to_string(value.x)+","+std::to_string(value.y)+","+std::to_string(value.z);
}
void BeginReceiverInspection() {
    if (inspection_done || inspection_array) return;
    try {
        if (!inspection_renderer_class.type_object) return;
        auto camera=Invoke(ControlCamera,nullptr); if (!IsAlive(camera)) return;
        auto transform=Invoke(ControlTransform,camera);
        inspection_camera=Value<Vec3>(InspectPosition,transform);
        uint8_t inactive=0; void* args[]{inspection_renderer_class.type_object,&inactive};
        inspection_array=Invoke(InspectFind,nullptr,args);
        if (!inspection_array) return;
        inspection_handle=host->gchandle_new(host->context,inspection_array,0);
        if (!inspection_handle) throw std::runtime_error("Cannot root renderer survey");
        int32_t dimension=0; void* size_args[]{&dimension}; inspection_count=Value<int32_t>(ControlArrayLength,inspection_array,size_args);
        if (inspection_count<0 || inspection_count>100000) throw std::runtime_error("Renderer survey exceeds bounds");
        inspection_index=inspection_nearby=0; inspection_deadline=Now()+10000;
        Log("Screen receiver survey started: renderers="+std::to_string(inspection_count)+" camera="+InspectionVector(inspection_camera)+" radius=15; read-only");
    } catch (const std::exception& error) { CancelReceiverInspection(); Log(std::string("Screen receiver survey unavailable: ")+error.what()); }
}
void ReceiverInspectionFrame() {
    if (!inspection_array) return;
    try {
        uint64_t tick=Now(); int batch=0;
        while (inspection_index<inspection_count && inspection_nearby<96 && Now()-tick<3 && ++batch<=64) {
            int32_t index=inspection_index++; void* args[]{&index}; auto renderer=Invoke(ControlArrayValue,inspection_array,args);
            if (!IsAlive(renderer)) continue;
            auto bounds=Value<Bounds>(InspectBounds,renderer);
            auto delta=Sub(bounds.center,inspection_camera);
            if (!std::isfinite(Dot(delta,delta)) || Dot(delta,delta)>225) continue;
            ++inspection_nearby;
            int id=Value<int32_t>(InspectInstance,renderer);
            auto path=InspectionPath(renderer);
            std::string mesh_name="unavailable";
            try {
                void* component_args[]{control_mesh_class.type_object}; auto filter=Invoke(ControlComponent,renderer,component_args);
                if (IsAlive(filter)) { auto mesh=Invoke(ControlMesh,filter); if (IsAlive(mesh)) mesh_name=String(Invoke(GetName,mesh)); }
            } catch (...) {}
            Log("Screen survey receiver: id="+std::to_string(id)+" path="+path+" center="+InspectionVector(bounds.center)+" extents="+InspectionVector(bounds.extents)+" mesh="+mesh_name);
            auto array=Invoke(InspectMaterials,renderer);
            if (!array) continue;
            auto handle=host->gchandle_new(host->context,array,0);
            if (!handle) throw std::runtime_error("Cannot root survey materials");
            try {
                int32_t dimension=0; void* size_args[]{&dimension}; int count=Value<int32_t>(ControlArrayLength,array,size_args);
                if (count<0 || count>32) throw std::runtime_error("Unexpected renderer material count");
                for (int32_t slot=0;slot<count;++slot) {
                    void* slot_args[]{&slot}; auto material=Invoke(ControlArrayValue,array,slot_args); if (!IsAlive(material)) continue;
                    auto shader=Invoke(MaterialShader,material);
                    std::string info="Screen survey material: receiver="+std::to_string(id)+" slot="+std::to_string(slot)+" name="+String(Invoke(GetName,material))+" shader="+(IsAlive(shader)?String(Invoke(GetName,shader)):"none");
                    for (const char* property:{"_MainTex","_BaseColorMap","_EmissiveMap","_BaseMap","_EmissionMap","_ScreenTex"}) {
                        auto text=host->string_new(host->context,property); void* property_args[]{text}; int32_t property_id=Value<int32_t>(PropertyId,nullptr,property_args);
                        void* id_args[]{&property_id}; if (!Value<uint8_t>(MaterialHasProperty,material,id_args)) continue;
                        auto texture=Invoke(MaterialGetTexture,material,id_args); if (!IsAlive(texture)) continue;
                        info+=" "+std::string(property)+"="+String(Invoke(GetName,texture));
                    }
                    Log(info);
                }
            } catch (...) { host->gchandle_free(host->context,handle); throw; }
            host->gchandle_free(host->context,handle);
        }
        if (inspection_index>=inspection_count || inspection_nearby>=96 || Now()>=inspection_deadline) {
            Log("Screen receiver survey finished: scanned="+std::to_string(inspection_index)+"/"+std::to_string(inspection_count)+" nearby="+std::to_string(inspection_nearby)+" truncated="+(inspection_index<inspection_count?"true":"false"));
            CancelReceiverInspection(); inspection_done=true;
        }
    } catch (const std::exception& error) { CancelReceiverInspection(); inspection_done=true; Log(std::string("Screen receiver survey stopped: ")+error.what()); }
}
void InspectionContracts() {
    Class(kCore,"UnityEngine","Renderer",inspection_renderer_class,false);
    auto core=[](MethodId id,const char* klass,const char* name,const char* args,const char* result,int count) { Resolve(id,kCore,"UnityEngine",klass,name,args,result,count,false); };
    core(InspectBounds,"Renderer","get_bounds",nullptr,"UnityEngine.Bounds",0);
    core(InspectPosition,"Transform","get_position",nullptr,"UnityEngine.Vector3",0);
    core(InspectInstance,"Object","GetInstanceID",nullptr,"System.Int32",0);
}
