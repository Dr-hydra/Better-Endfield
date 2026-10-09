// Read-only Unity texture streaming observations for Android issue #25.
// Included after the generated-texture identity table. Never changes quality,
// requested mips, LOD/culling, native asset ownership, or the streaming budget.
struct AndroidTextureDiagnostics {
    bool baseline_seen=false,missing_logged=false,pending_upload_observation=false;
    uint64_t next_summary_ms=0,next_mip_sample_ms=0;
    bool mip_idle_seen=false;
    uint64_t mip_idle_since_ms=0;
    std::string last_mip_request_token;
    unsigned upload_summaries=0;
};
AndroidTextureDiagnostics g_android_texture_diagnostics;

bool AndroidTextureDiagnosticsOnPump() {
    const auto pump=g_pump_thread.load(std::memory_order_acquire);
    return pump && pump==GetCurrentThreadId();
}

bool AndroidTextureMipTokenValid(const std::string& token) {
    if (token.empty() || token.size()>64) return false;
    return std::all_of(token.begin(),token.end(),[](unsigned char value) {
        return (value>='A' && value<='Z') || (value>='a' && value<='z') ||
            (value>='0' && value<='9') || value=='-' || value=='_';
    });
}

bool AndroidTextureMipSceneName(std::string name) {
    for (char& value:name) if (value>='A' && value<='Z') value=static_cast<char>(value-'A'+'a');
    for (const char* part:{"rock","wall","stone","terrain","ground","map01","_mod_"})
        if (name.find(part)!=std::string::npos) return true;
    return false;
}

void SampleAndroidTextureMips(uint64_t now,const std::string& request_token={}) {
    auto& state=g_android_texture_diagnostics;
    const bool requested=!request_token.empty();
    // Enumeration itself visits the engine inventory. Both opt-in paths share
    // a one-minute limit; an independent request permits 8192 follow-up reads
    // and 12 game samples. Inspection retains its 1024/6 fallback semantics.
    if (!AndroidTextureDiagnosticsOnPump() ||
        (requested?(!AndroidTextureMipTokenValid(request_token) || request_token==state.last_mip_request_token):
            !betterendfieldnext::AndroidInspectionEnabled()) || now<state.next_mip_sample_ms ||
        !g_texture2d_class.type_object) return;
    state.next_mip_sample_ms=now+60000;
    void* args[]{g_texture2d_class.type_object};
    void* textures=Invoke(Contract("resources.find_all"),nullptr,args,false);
    if (!textures) return;
    const int total=ArrayLength(textures);
    if (total<0 || total>100000) return;
    if (requested) state.last_mip_request_token=request_token;
    const int cap=std::min(total,requested?8192:1024);
    unsigned streaming=0,behind=0,generated=0,examples=0,equal=0,ahead=0;
    std::array<std::string,12> preferred_samples{},fallback_samples{};
    unsigned preferred_count=0,fallback_count=0;
    for (int i=0;i<cap;++i) {
        void* texture=ArrayValue(textures,i);if (!texture) continue;
        int32_t instance=0;
        const bool instance_ok=InvokeValue(Contract("object.instance_id"),texture,nullptr,instance);
        const bool mod=instance_ok && g_generated_texture_identity.contains(instance);
        if (mod) ++generated;
        uint8_t streams=0;
        if (!InvokeValue(Contract("android.texture_is_streaming"),texture,nullptr,streams) || !streams) continue;
        ++streaming;
        int32_t loaded=-1,desired=-1;
        if (!InvokeValue(Contract("android.texture_loaded_mip"),texture,nullptr,loaded) ||
            !InvokeValue(Contract("android.texture_desired_mip"),texture,nullptr,desired) ||
            loaded<0 || desired<0) continue;
        if (loaded>desired) ++behind;else if (loaded==desired) ++equal;else ++ahead;
        if (requested && (mod || !instance_ok || preferred_count>=12)) continue;
        if (!requested && (loaded<=desired || examples>=6)) continue;
        const std::string name=ObjectName(texture);
        const bool preferred=requested && AndroidTextureMipSceneName(name);
        if (requested && !preferred && fallback_count>=12) continue;
        int32_t requested_mip=-1,width=-1,height=-1;
        const bool requested_ok=InvokeValue(Contract("android.texture_requested_mip"),texture,nullptr,requested_mip);
        InvokeValue(Contract("texture.get_width"),texture,nullptr,width);
        InvokeValue(Contract("texture.get_height"),texture,nullptr,height);
        std::string sample="Android texture mip sample name="+name+
            " origin="+(mod?std::string("BEM"):std::string("game-or-untracked"))+
            " size="+std::to_string(width)+"x"+std::to_string(height)+
            " loaded="+std::to_string(loaded)+" desired="+std::to_string(desired)+
            " requested="+(requested_ok?std::to_string(requested_mip):std::string("unavailable"));
        if (requested) {
            sample+=" requestToken="+request_token;
            if (preferred) preferred_samples[preferred_count++]=std::move(sample);
            else fallback_samples[fallback_count++]=std::move(sample);
        } else {++examples;Log(sample);}
    }
    if (requested) {
        for (unsigned i=0;i<preferred_count;++i) {Log(preferred_samples[i]);++examples;}
        for (unsigned i=0;i<fallback_count && examples<12;++i) {Log(fallback_samples[i]);++examples;}
    }
    Log("Android texture mip sample scanned="+std::to_string(cap)+" total="+std::to_string(total)+
        " streamingInSample="+std::to_string(streaming)+" behindDesiredInSample="+std::to_string(behind)+
        " equalDesiredInSample="+std::to_string(equal)+" aheadDesiredInSample="+std::to_string(ahead)+
        " BEMInSample="+std::to_string(generated)+" loggedSamples="+std::to_string(examples)+
        (requested?" requestToken="+request_token:" mode=inspection")+
        " (bounded sample, not residency totals)");
}

void ObserveAndroidTextureStreaming(const char* event,bool force=false,std::string upload={},bool periodic_request=false) noexcept {
    try {
        // Synchronous AssetProxy deliveries need not be on the confirmed pump.
        // Do not introduce any new Unity queries on an unconfirmed thread.
        if (!AndroidTextureDiagnosticsOnPump()) return;
        auto& state=g_android_texture_diagnostics;const uint64_t now=GetTickCount64();
        if (!force && now<state.next_summary_ms) return;
        state.next_summary_ms=now+15000;
        std::string line="Android texture streaming event="+std::string(event),missing;
        unsigned available=0;
        const auto read=[&]<class T>(const char* key,const char* label,T& value) {
            const bool ok=InvokeValue(Contract(key),nullptr,nullptr,value);
            line+=" "+std::string(label)+"="+(ok?std::to_string(value):std::string("unavailable"));
            if (ok) ++available;else {if (!missing.empty()) missing+=",";missing+=key;}
            return ok;
        };
        uint8_t active=0;float budget=0;uint64_t nonstreaming=0,current=0,desired=0,target=0;
        uint64_t pending=0,loading=0,streaming_count=0,nonstreaming_count=0;
        const bool active_ok=read("android.streaming_active","active",active);
        const bool budget_ok=read("android.streaming_budget","budgetMiB",budget) && std::isfinite(budget) && budget>0;
        const bool nonstreaming_ok=read("android.texture_nonstreaming","nonStreamingBytes",nonstreaming);
        int32_t maximum_lod=-1,lod_offset=-1,global_mip_limit=-1,master_texture_limit=-1,max_reduction=-1;
        uint8_t lod_streaming=0,lod_status=0;
        read("android.quality_max_lod","qualityMaximumLOD",maximum_lod);
        read("android.quality_lod_offset","qualityLODOffset",lod_offset);
        read("android.global_mip_limit","globalMipLimit",global_mip_limit);
        // The shipped Unity 2021 fork may expose the older property name.
        read("android.master_texture_limit","masterTextureLimit",master_texture_limit);
        read("android.streaming_max_reduction","maxMipReduction",max_reduction);
        read("android.lod_streaming_active","HGLODStreamingActive",lod_streaming);
        // Status uses the low flag bits. Copy one byte so the boxed Byte enum
        // is never over-read because the native ABI returns w0. Idle does not
        // prove that high-detail textures are resident.
        read("android.lod_streaming_status","HGLODStreamingStatusBits",lod_status);
        read("android.texture_current","currentBytes",current);
        read("android.texture_desired","desiredBytes",desired);
        read("android.texture_target","targetBytes",target);
        const bool pending_ok=read("android.texture_pending","pendingLoads",pending);
        const bool loading_ok=read("android.texture_loading","loading",loading);
        read("android.texture_streaming_count","streamingCount",streaming_count);
        read("android.texture_nonstreaming_count","nonStreamingCount",nonstreaming_count);
        if (!missing.empty() && !state.missing_logged) {
            state.missing_logged=true;Log("Android texture streaming diagnostics partially unavailable: "+missing+
                "; model/LOD behavior unchanged");
        }
        // This establishes a budget-pressure condition, not that BEM caused it.
        if (active_ok && active && budget_ok && nonstreaming_ok &&
            static_cast<long double>(nonstreaming)>=static_cast<long double>(budget)*1024*1024)
            line+=" pressure=nonstreaming-at-or-above-budget";
        if (available) Log(line+upload+" "+AndroidLodStreamingOffsetReport());
        std::string request_token;
        if (periodic_request) {
            // Missing getters cannot establish idle. Require two zero-value
            // periodic observations separated by at least 15 seconds.
            if (!pending_ok || !loading_ok || pending || loading ||
                (state.mip_idle_seen && now<state.mip_idle_since_ms)) state.mip_idle_seen=false;
            else if (!state.mip_idle_seen) {state.mip_idle_seen=true;state.mip_idle_since_ms=now;}
            else if (now-state.mip_idle_since_ms>=15000 && now>=state.next_mip_sample_ms &&
                g_texture2d_class.type_object) {
                auto token=betterendfieldnext::AndroidTextureMipRequestToken();
                if (AndroidTextureMipTokenValid(token) && token!=state.last_mip_request_token)
                    request_token=std::move(token);
            }
        }
        SampleAndroidTextureMips(now,request_token);
    } catch (...) { /* Optional telemetry cannot interrupt publication/rollback. */ }
}
void ObserveAndroidTextureStreamingBeforeUpload() noexcept {
    if (g_android_texture_diagnostics.baseline_seen) return;
    if (!AndroidTextureDiagnosticsOnPump()) {
        g_android_texture_diagnostics.pending_upload_observation=true;return;
    }
    g_android_texture_diagnostics.baseline_seen=true;
    const bool deferred=g_android_texture_diagnostics.pending_upload_observation;
    g_android_texture_diagnostics.pending_upload_observation=false;
    ObserveAndroidTextureStreaming(deferred?"before-observed-upload (earlier upload baseline unavailable)":"before-first-model-upload",true);
}
void ObserveAndroidTextureStreamingAfterUpload(bool published,uint64_t count,uint64_t bytes,
    uint64_t dedup,uint64_t live_reuse) noexcept {
    try {
        if (!AndroidTextureDiagnosticsOnPump()) {
            g_android_texture_diagnostics.pending_upload_observation=true;return;
        }
        // First 8 transactions show upload deltas; subsequent snapshots are
        // limited to once per 15 seconds regardless of model/clone frequency.
        const bool force=g_android_texture_diagnostics.upload_summaries<8;
        if (force) ++g_android_texture_diagnostics.upload_summaries;
        ObserveAndroidTextureStreaming("after-model-upload",force,
            " transactionPublished="+std::string(published?"1":"0")+
            " transactionTextureCount="+std::to_string(count)+" transactionSubmittedBytes="+std::to_string(bytes)+
            " transactionDedupSavedBytes="+std::to_string(dedup)+" transactionLiveReuseBytes="+std::to_string(live_reuse)+
            " (transaction bytes are submitted BEM payloads, not measured GPU residency)");
    } catch (...) {}
}
void ObserveAndroidTextureStreamingPeriodic() noexcept {
    if (!AndroidTextureDiagnosticsOnPump()) return;
    if (g_android_texture_diagnostics.pending_upload_observation) {
        g_android_texture_diagnostics.pending_upload_observation=false;
        const bool first=!g_android_texture_diagnostics.baseline_seen;
        g_android_texture_diagnostics.baseline_seen=true;
        ObserveAndroidTextureStreaming(first?"deferred-upload-observation (before-first-upload unavailable)":"deferred-upload-observation",first,{},true);return;
    }
    if (!g_android_texture_diagnostics.baseline_seen) {
        g_android_texture_diagnostics.baseline_seen=true;
        ObserveAndroidTextureStreaming("first-confirmed-pump",true,{},true);return;
    }
    ObserveAndroidTextureStreaming("periodic",false,{},true);
}
