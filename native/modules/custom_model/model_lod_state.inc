// Shared production LOD state machine; included after its host/pipeline helpers.
struct LodState {
    bool active=false;
    bool applied=false;
#if defined(__ANDROID__)
    bool npc_snapshot=false;
#endif
    int32_t original_max_lod=0;
    WeakManagedReference pipeline;
    std::array<LodField,5> fields{{
        {"s_visibleModelDistance","UnityEngine.Vector2",8},
        {"s_visibileAtmosphericModelDistance","UnityEngine.Vector2",8},
        {"s_maxNPCRenderNum","System.Int32",4},
        {"s_cameraCullRenderIdx","System.Int32",4},
        {"s_enableCameraPhysicCull","System.Boolean",1}
    }};
    bool Resolve() {
        const Float2 distance{1000,1000}; const int32_t number=100,cull=0; const bool physic=false;
        std::memcpy(fields[0].desired.data(),&distance,8); std::memcpy(fields[1].desired.data(),&distance,8);
        std::memcpy(fields[2].desired.data(),&number,4); std::memcpy(fields[3].desired.data(),&cull,4);
        std::memcpy(fields[4].desired.data(),&physic,1);
        for (auto& field:fields) {
            const BE_FieldDescriptorV1 descriptor{"Gameplay.Beyond.dll","Beyond.NPC.Lod","NPCCrowdLODSetting",field.name,field.type};
            if (g_host->resolve_field(g_host->context,&descriptor,&field.resolved)!=BE_Result_Ok || !field.resolved.field_info) return false;
        }
        return g_static_get && g_static_set;
    }
    bool Write(LodField& field,std::array<uint8_t,8>& bytes) {
        std::array<uint8_t,8> read{};
        return SafeStaticField(g_static_set,field.resolved.field_info,bytes.data()) &&
            SafeStaticField(g_static_get,field.resolved.field_info,read.data()) &&
            std::equal(bytes.begin(),bytes.begin()+field.size,read.begin());
    }
    bool Restore() {
        // DisableForceLOD0 must be able to restore the current game settings.
        g_lod_bias_locked.store(false,std::memory_order_release);
        if (!active) return true;
        applied=false;
        bool success=true;
#if defined(__ANDROID__)
        if (npc_snapshot) for (auto& field:fields) if (!Write(field,field.original)) success=false;
#endif
#if !defined(__ANDROID__)
        for (auto& field:fields) if (!Write(field,field.original)) success=false;
        void* args[]{&original_max_lod}; int32_t read=-1;
        if (!InvokeVoid(Contract("quality.set_max_lod"),nullptr,args) ||
            !InvokeValue(Contract("quality.get_max_lod"),nullptr,nullptr,read) || read!=original_max_lod) success=false;
#endif
        if (void* object=pipeline.Get()) {
            if (!InvokeVoid(Contract("pipeline.disable_force_lod0"),object,nullptr)) success=false;
            else pipeline.Reset();
        }
        if (success) {
            active=false; pipeline.Reset();
#if defined(__ANDROID__)
            npc_snapshot=false;
#endif
        }
        return success;
    }
    bool MaintainParameters() {
        int32_t read=-1;
        if (!InvokeValue(Contract("quality.get_max_lod"),nullptr,nullptr,read)) return false;
        if (read!=0) {
            int32_t zero=0; void* args[]{&zero};
            if (!InvokeVoid(Contract("quality.set_max_lod"),nullptr,args) ||
                !InvokeValue(Contract("quality.get_max_lod"),nullptr,nullptr,read) || read!=0) return false;
        }
        for (auto& field:fields) {
            std::array<uint8_t,8> value{};
            if (!SafeStaticField(g_static_get,field.resolved.field_info,value.data())) return false;
            if (!std::equal(field.desired.begin(),field.desired.begin()+field.size,value.begin())) {
                if (!Write(field,field.desired)) return false;
            }
        }
        return true;
    }
#if defined(__ANDROID__)
    // EnableForceLOD0 is a pipeline-bias API name. On mobile it preserves
    // the highest available resource LOD (normally LOD1), not QualitySettings
    // maximumLODLevel=0. Explicit resources need the same configured policy.
    bool MaintainAndroid(bool stopping,const ModRegistry& registry) {
        return Update(!stopping && !registry.enabled.empty() && betterendfield::AndroidPipelineLodEnabled());
    }
#endif
    bool Update(bool desired) {
        if (!desired) return Restore();
#if defined(__ANDROID__)
        void* current=Invoke(Contract("pipeline.current"),nullptr,nullptr);
        if (!current) return false;
        if (active && pipeline.Get()==current && applied) {
            if (npc_snapshot) for (auto& field:fields) {
                std::array<uint8_t,8> value{};
                if (!SafeStaticField(g_static_get,field.resolved.field_info,value.data())) return false;
                if (!std::equal(field.desired.begin(),field.desired.begin()+field.size,value.begin()) && !Write(field,field.desired)) return false;
            }
            return true;
        }
        if (active && !Restore()) return false;
        if (betterendfield::AndroidNpcParametersEnabled()) {
            for (auto& field:fields) if (!SafeStaticField(g_static_get,field.resolved.field_info,field.original.data())) return false;
            npc_snapshot=true;
        }
        if (!pipeline.Set(current)) return false;
        // Pipeline bias only. Mobile NPC and quality/culling configuration
        // must remain untouched, even while character replacements are active.
        active=true;
        if (!InvokeVoid(Contract("pipeline.enable_force_lod0"),current,nullptr)) { Restore(); return false; }
        if (npc_snapshot) for (auto& field:fields) if (!Write(field,field.desired)) { Restore(); return false; }
        applied=true;
        g_lod_bias_locked.store(true,std::memory_order_release);
        Log(npc_snapshot?"Android pipeline LOD bias + NPC parameters applied/readback PASS; QualitySettings unchanged":
            "Android pipeline LOD bias applied; quality/NPC/camera culling unchanged");
        return true;
#else
        void* current=Invoke(Contract("pipeline.current"),nullptr,nullptr);
        if (!current) return false;
        if (active && applied && pipeline.Get()==current) {
            if (MaintainParameters()) return true;
            Restore(); return false;
        }
        if (active && !Restore()) return false;
        if (!InvokeValue(Contract("quality.get_max_lod"),nullptr,nullptr,original_max_lod)) return false;
        for (auto& field:fields) if (!SafeStaticField(g_static_get,field.resolved.field_info,field.original.data())) return false;
        if (!pipeline.Set(current)) return false;
        if (!InvokeVoid(Contract("pipeline.enable_force_lod0"),current,nullptr)) { pipeline.Reset(); return false; }
        active=true; applied=false;
        int32_t zero=0,read=-1; void* args[]{&zero};
        bool success=InvokeVoid(Contract("quality.set_max_lod"),nullptr,args) &&
            InvokeValue(Contract("quality.get_max_lod"),nullptr,nullptr,read) && read==0;
        for (auto& field:fields) if (!Write(field,field.desired)) success=false;
        if (!success) Restore();
        else {
            applied=true;
            g_lod_bias_locked.store(true,std::memory_order_release);
        }
        return success;
#endif
    }
};
