// Production coordinator. Public integration points (called on the pump unless
// stated otherwise): EnqueueResourceBuild, PumpModelJobs, RegisterModelClone.
// CPU tickets never own Unity data. Job destruction/Dispose runs on the pump.
struct SmallModelPlan {
    std::shared_ptr<const void> file_lease;
    BemPocData data;
    std::vector<uint64_t> mesh_bytes;
    uint64_t used=0;
};
std::unordered_map<std::string,std::shared_ptr<SmallModelPlan>> g_model_plans;
constexpr uint64_t kModelPlanIdleMs=10000; // metadata-only plans
std::unique_ptr<AsyncBemLoader> g_model_loader;
// Deferred texture entries are decoded one at a time on a background worker,
// shared by every role. Decoded-but-unconsumed bytes stay under 96 MiB; one
// larger texture is decoded only while nothing else is held.
std::unique_ptr<TexturePayloadStreamer> g_texture_streamer;
FrameBudget g_model_frame_budget{FrameBudget::Config{32*kLoadingMiB,256*kLoadingMiB,8,2,std::chrono::milliseconds(2)}};
uint64_t g_model_frame=UINT64_MAX,g_model_service_serial=0;
unsigned g_model_foreground_streak=0;
struct ModelFileLease {
    uint64_t epoch=0;
#if defined(_WIN32)
    HANDLE file=INVALID_HANDLE_VALUE;
    explicit ModelFileLease(const std::filesystem::path& path) {
        file=CreateFileW(path.c_str(),GENERIC_READ,FILE_SHARE_READ,nullptr,OPEN_EXISTING,FILE_ATTRIBUTE_NORMAL,nullptr);
    }
    bool Valid() const {return file!=INVALID_HANDLE_VALUE;}
    ~ModelFileLease() {if (Valid()) CloseHandle(file);}
#else
    std::ifstream file;
    explicit ModelFileLease(const std::filesystem::path& path):file(path,std::ios::binary) {}
    bool Valid() const {return bool(file);}
#endif
};
std::unordered_map<std::string,std::weak_ptr<ModelFileLease>> g_model_file_leases;
uint64_t g_model_file_epoch=0;
std::shared_ptr<ModelFileLease> AcquireModelFileLease(const std::filesystem::path& path) {
    const auto key=path.lexically_normal().generic_string();
    if (auto found=g_model_file_leases.find(key);found!=g_model_file_leases.end()) if (auto lease=found->second.lock()) return lease;
    auto lease=std::make_shared<ModelFileLease>(path);if (!lease->Valid()) return {};
    lease->epoch=++g_model_file_epoch;g_model_file_leases[key]=lease;
    std::erase_if(g_model_file_leases,[](const auto& entry){return entry.second.expired();});return lease;
}
struct KnownModelTarget {
    WeakObject root;
    std::shared_ptr<OwnedCharacterAdapter> owner;
    const CharacterAdapter* adapter=nullptr;
    std::string desired;
    std::string failed_selection;
    std::vector<WeakObject> renderers;
    LoadPriority priority=LoadPriority::Visible;
    uint64_t next_visibility_ms=0;
    // A verified-complete receiver is re-verified at a bounded interval: the
    // full identity check walks every renderer and bone path per call.
    std::string verified_selection;
    uint64_t next_verify_ms=0;
};
std::vector<KnownModelTarget> g_model_targets;
struct PendingModelDelivery {
    std::unique_ptr<StrongReference> asset,source;
    // Set when the synchronous transaction already handled this delivery: the
    // pump only records the receiver (hot switch), and a refused selection is
    // not immediately retried by a second, identical asynchronous build.
    bool synchronous=false;
    std::string refused_selection;
};
std::mutex g_model_delivery_mutex;
std::vector<PendingModelDelivery> g_model_deliveries;
std::atomic<uint64_t> g_model_deliveries_registered{0},g_model_deliveries_dropped{0};
bool RegisterModelDelivery(void* asset,void* source=nullptr,bool synchronous=false,std::string refused_selection={}) {
    if (!IsGameObjectResource(asset) || (source && !IsGameObjectResource(source)) ||
        !g_host || !g_enabled.load() || g_stopping.load()) return false;
    PendingModelDelivery pending;pending.asset=std::make_unique<StrongReference>();
    if (!pending.asset->Set(asset)) return false;
    if (source) { pending.source=std::make_unique<StrongReference>();if (!pending.source->Set(source)) return false; }
    pending.synchronous=synchronous;pending.refused_selection=std::move(refused_selection);
    std::lock_guard lock(g_model_delivery_mutex);
    if (g_model_deliveries.size()>=256) {g_model_deliveries_dropped.fetch_add(1);return false;}
    g_model_deliveries.push_back(std::move(pending));g_model_deliveries_registered.fetch_add(1);
    return true;
}
[[maybe_unused]] void RegisterModelClone(void* source,void* clone) { RegisterModelDelivery(clone,source); }
// Clone coverage decides the template delivery mode (ModelAsyncDeliveryBlocker):
//   clone hooks installed  -> Finish delivers the original at once, the pump
//                             builds over several frames and commits to the
//                             template plus every instance cloned meanwhile;
//   not requested/refused  -> proven synchronous transaction inside Finish.
// UnityEngine.Object::Internal_CloneSingleWithParent is also hooked by
// betterendfield.model (login actor capture). The Host create_hook chains
// detours per target (PC and Android, registration order; `original` is the
// next link), so both modules coexist. An exclusive Host refused the second
// owner on 2026-10-03 ("native hook target already owned"); with such a Host
// set this to false.
// Off: clone hooks run on every Instantiate in the game and instances cloned
// before a frame-sliced commit are not yet rebound reliably (device 2026-10-03).
constexpr bool kModelCloneHooksRequested=false;
bool g_model_clone_hooks_requested=kModelCloneHooksRequested; // tests may override
std::atomic_bool g_model_clone_coverage{false};
using ModelCloneSingleFn=void*(__fastcall*)(void*,void*);
using ModelCloneWithParentFn=void*(__fastcall*)(void*,void*,bool,void*);
struct ModelCloneVector3 { float x,y,z; };
struct ModelCloneQuaternion { float x,y,z,w; };
using ModelInstantiateFn=void*(__fastcall*)(void*,ModelCloneVector3,ModelCloneQuaternion,void*);
using ModelInstantiateWithParentFn=void*(__fastcall*)(void*,void*,ModelCloneVector3,ModelCloneQuaternion,void*);
ModelCloneSingleFn g_original_clone_single=nullptr;
ModelCloneWithParentFn g_original_clone_with_parent=nullptr;
ModelInstantiateFn g_original_instantiate=nullptr;
ModelInstantiateWithParentFn g_original_instantiate_with_parent=nullptr;
// Managed addresses of registered templates/receivers (and their clones). The
// hot path checks an atomic count first; only watched sources take the lock.
std::mutex g_model_clone_watch_mutex;
std::unordered_set<void*> g_model_clone_watch;
std::atomic<size_t> g_model_clone_watch_size{0};
std::atomic<uint64_t> g_model_clones_registered{0};
void WatchModelCloneSource(void* asset) {
    if (!asset || !g_model_clone_coverage.load()) return;
    std::lock_guard lock(g_model_clone_watch_mutex);
    g_model_clone_watch.insert(asset);g_model_clone_watch_size.store(g_model_clone_watch.size());
}
void NoteModelClone(void* source,void* clone) {
    if (!source || !clone || !g_model_clone_watch_size.load(std::memory_order_relaxed) ||
        !g_enabled.load(std::memory_order_acquire) || g_stopping.load()) return;
    {
        std::lock_guard lock(g_model_clone_watch_mutex);
        if (!g_model_clone_watch.contains(source)) return;
        g_model_clone_watch.insert(clone);g_model_clone_watch_size.store(g_model_clone_watch.size());
    }
    try { if (RegisterModelDelivery(clone,source)) g_model_clones_registered.fetch_add(1); }
    catch (...) { /* Never disturb the game's Instantiate. */ }
}
void* __fastcall ModelCloneSingle(void* data,void* method) {
    void* clone=g_original_clone_single?g_original_clone_single(data,method):nullptr;
    NoteModelClone(data,clone);return clone;
}
void* __fastcall ModelCloneWithParent(void* data,void* parent,bool world_position_stays,void* method) {
    void* clone=g_original_clone_with_parent?g_original_clone_with_parent(data,parent,world_position_stays,method):nullptr;
    NoteModelClone(data,clone);return clone;
}
void* __fastcall ModelInstantiate(void* data,ModelCloneVector3 position,ModelCloneQuaternion rotation,void* method) {
    void* clone=g_original_instantiate?g_original_instantiate(data,position,rotation,method):nullptr;
    NoteModelClone(data,clone);return clone;
}
void* __fastcall ModelInstantiateWithParent(void* data,void* parent,ModelCloneVector3 position,ModelCloneQuaternion rotation,void* method) {
    void* clone=g_original_instantiate_with_parent?g_original_instantiate_with_parent(data,parent,position,rotation,method):nullptr;
    NoteModelClone(data,clone);return clone;
}
// Coverage: both Internal_CloneSingle* entries hooked, and every Instantiate
// (position/rotation) entry that exists in this build hooked as well.
void InstallModelCloneHooks() {
    g_model_clone_coverage.store(false);
    if (!g_model_clone_hooks_requested) {
        Log("Model clone hooks not requested (requires Host hook chaining); template delivery=synchronous");
        return;
    }
    std::string failed,installed;
    const auto install=[&](const char* key,bool required,void* detour,void** original) {
        auto* method=Contract(key);
        if (!required && (!method || !method->pointer)) return true; // stripped entry: never called
        const BE_Result result=!method || !method->pointer || !g_host || !g_host->create_hook?BE_Result_ContractMismatch:
            g_host->create_hook(g_host->context,kModuleId,method->pointer,detour,original);
        if (result==BE_Result_Ok && *original) {installed+=std::string(installed.empty()?"":",")+key;return true;}
        failed+=std::string(failed.empty()?"":",")+key+"="+std::to_string(int(result));
        return false;
    };
    bool complete=install("clone.single",true,reinterpret_cast<void*>(&ModelCloneSingle),reinterpret_cast<void**>(&g_original_clone_single));
    complete=install("clone.with_parent",true,reinterpret_cast<void*>(&ModelCloneWithParent),reinterpret_cast<void**>(&g_original_clone_with_parent)) && complete;
    complete=install("clone.instantiate",false,reinterpret_cast<void*>(&ModelInstantiate),reinterpret_cast<void**>(&g_original_instantiate)) && complete;
    complete=install("clone.instantiate_with_parent",false,reinterpret_cast<void*>(&ModelInstantiateWithParent),
        reinterpret_cast<void**>(&g_original_instantiate_with_parent)) && complete;
    g_model_clone_coverage.store(complete);
    if (complete)
        Log("Model clone hooks installed ("+installed+"); template delivery=frame-sliced");
    else
        Log("Model clone hook unavailable ("+failed+"; installed="+installed+"); template delivery=synchronous fallback");
}
// Pump maintenance (1 s): keep only live registered receivers and pending
// deliveries in the clone watch set; new templates/clones are added at once.
void PruneModelCloneWatch() {
    if (!g_model_clone_coverage.load()) return;
    std::unordered_set<void*> watch;
    for (const auto& target:g_model_targets) if (void* root=target.root.Get()) watch.insert(root);
    {std::lock_guard pending(g_model_delivery_mutex);
        for (const auto& delivery:g_model_deliveries) if (void* asset=delivery.asset->Get()) watch.insert(asset);}
    std::lock_guard lock(g_model_clone_watch_mutex);
    g_model_clone_watch.swap(watch);g_model_clone_watch_size.store(g_model_clone_watch.size());
}
void DisableModelCloneHooks() {
    g_model_clone_coverage.store(false);
    std::lock_guard lock(g_model_clone_watch_mutex);
    g_model_clone_watch.clear();g_model_clone_watch_size.store(0);
}
constexpr uint64_t kModelPumpStallMs=2000;
std::atomic<uint64_t> g_model_pump_ms{0};
uint64_t g_model_verify_interval_ms=500;
// Returns null only when an asynchronous registration is known to be complete:
// every clone variant is observed and the Unity frame pump has run recently.
const char* ModelAsyncDeliveryBlocker(uint64_t now) {
    if (!g_model_clone_coverage.load()) return "clone-coverage-unavailable";
    const auto pumped=g_model_pump_ms.load();
    if (!pumped) return "pump-not-started";
    if (now>pumped+kModelPumpStallMs) return "pump-stalled";
    return nullptr;
}
struct ModelTextureWork {
    size_t index=0;
    void* donor=nullptr;
    void* object=nullptr;
    std::string sampler,identity;
    unsigned phase=0;
    bool live=false,decode_requested=false;
    // Deferred payload: requested ahead, taken right before LoadRawTextureData
    // and released immediately after it.
    TexturePayloadStreamer::Ticket decode;
    std::vector<std::pair<void*,std::vector<int32_t>>> pins;
};
struct PendingModelAsset { std::string key;void* object=nullptr;std::vector<void*> donors;uint64_t bytes=0; };
struct ModelBuildJob {
    enum class Phase { Decode, Capture, Mesh, Materials, Texture, Commit, Done } phase=Phase::Decode;
    EnabledMod mod;
    std::shared_ptr<OwnedCharacterAdapter> owner;
    LoadPriority priority=LoadPriority::Visible;
    WeakObject target;
    void* asset=nullptr;
    void* ui_asset=nullptr;
    ConstructionScope scope{false};
    AsyncBemLoader::Ticket ticket;
    std::shared_ptr<const BemResult> decoded;
    std::shared_ptr<ModelFileLease> file_lease;
    std::shared_ptr<SmallModelPlan> plan;
    std::vector<PreparedBinding> bindings;
    std::vector<ModelTextureWork> textures;
    std::vector<PendingModelAsset> publish;
    // Live published textures of this role, indexed once per Job (borrowed).
    bool live_indexed=false;
    std::unordered_map<std::string,void*> live_textures;
    // Decoded bytes taken from the streamer whose upload permit was refused.
    TexturePayloadStreamer::Bytes pending_bytes;
    void* palette_poses=nullptr;
    size_t component=0,texture=0;
    uint64_t served=0,mesh_hits=0,texture_hits=0;
    bool invalid=false;
    bool decode_failed=false; // CPU decode refused: a synchronous retry would fail identically.
    std::string failure;      // First recorded reason; logged once when the Job is dropped.
#if defined(__ANDROID__)
    void* donor_handle=nullptr;uint32_t donor_handle_root=0;
    ~ModelBuildJob() { betterendfield::AndroidReleaseUiDonor(donor_handle,donor_handle_root); }
#endif
};
std::vector<std::unique_ptr<ModelBuildJob>> g_model_jobs;
thread_local ModelBuildJob* g_binding_job=nullptr;
bool JobFail(ModelBuildJob& job,std::string reason) {
    if (job.failure.empty()) job.failure=std::move(reason);
    return false;
}
const char* ModelJobPhaseName(ModelBuildJob::Phase phase) {
    using Phase=ModelBuildJob::Phase;
    switch (phase) {
    case Phase::Decode: return "Decode";
    case Phase::Capture: return "Capture";
    case Phase::Mesh: return "Mesh";
    case Phase::Materials: return "Materials";
    case Phase::Texture: return "Texture";
    case Phase::Commit: return "Commit";
    case Phase::Done: return "Done";
    }
    return "?";
}
AsyncBemLoader::Ticket RequestModelCpu(const EnabledMod& mod,const std::shared_ptr<ModelFileLease>& lease,LoadPriority priority) {
    BemRequest request;request.key=mod.selection_key;request.revision=mod.selection_key;
    request.path=mod.package;request.generation=g_model_revision;
    request.appearance=mod.appearance;request.parameters=mod.parameters;request.resource_id=mod.resource_id;
    request.skip_validation=mod.skip_validation;request.loading_optimization=mod.loading_optimization;
    request.defer_texture_payloads=true; // geometry + metadata; textures stream per entry
    request.file_lease=lease;request.revision+="\nlease="+std::to_string(lease->epoch);
    request.priority=priority;
    return g_model_loader->Request(std::move(request));
}
std::shared_ptr<SmallModelPlan> MakeSmallModelPlan(const BemPocData& data) {
    auto plan=std::make_shared<SmallModelPlan>();auto& small=plan->data;
    small.header=data.header;small.skip_validation=data.skip_validation;small.loading_optimization=data.loading_optimization;
    for (const auto& component:data.components) {
        BemComponent c;c.info=component.info;c.skip_validation=component.skip_validation;c.layout_crc=component.layout_crc;
        c.static_mesh=component.static_mesh;
        c.bones=component.bones;c.draws=component.draws;c.keep_material_overrides=component.keep_material_overrides;
        c.attributes=component.attributes;c.bone_names=component.bone_names;c.material_names=component.material_names;
        c.keep_material_names=component.keep_material_names;c.bone_aliases=component.bone_aliases;
        c.bone_aliases_by_resource=component.bone_aliases_by_resource;
        uint64_t bytes=component.indices.size();for (const auto& stream:component.streams) bytes+=stream.size();
        plan->mesh_bytes.push_back(bytes);small.components.push_back(std::move(c));
    }
    for (const auto& texture:data.textures) {
        BemTexture t;t.info=texture.info;t.name=texture.name;t.original_name=texture.original_name;
        t.payload_id=texture.payload_id;t.payload_codec=texture.payload_codec;
        t.payload_offset=texture.payload_offset;t.payload_stored=texture.payload_stored;
        small.textures.push_back(std::move(t));
    }
    small.payload_source=data.payload_source;
    plan->used=GetTickCount64();return plan;
}
bool AwaitModelDecode(ModelBuildJob& job) {
    if (job.decoded) return true;
    if (!job.ticket) {job.ticket=RequestModelCpu(job.mod,job.file_lease,job.priority);return false;}
    const auto result=g_model_loader->Poll(job.ticket);
    if (result.status==AsyncLoadStatus::Pending) return false;
    if (result.status!=AsyncLoadStatus::Ready || !ValidatePayloadAdapter(*job.mod.adapter,result.result->data)) {
        Log("Model CPU load refused: "+result.error);job.invalid=true;job.decode_failed=true;
        JobFail(job,"CPU decode refused status="+std::to_string(int(result.status))+" "+result.error);return false;
    }
    job.decoded=result.result;
    if (!job.plan) {
        job.plan=MakeSmallModelPlan(job.decoded->data);
        job.plan->file_lease=job.file_lease;
        g_model_plans[job.mod.selection_key]=job.plan;
        while (g_model_plans.size()>32) {
            auto oldest=std::min_element(g_model_plans.begin(),g_model_plans.end(),[](const auto& a,const auto& b){return a.second->used<b.second->used;});
            g_model_plans.erase(oldest);
        }
    }
    return true;
}
// Material preparation stages each texture slot. One upload per selected entry
// and donor sampler state per Job (dedup), or a live published texture with
// the same immutable content (borrowed, nothing retained when idle).
bool StageModelTexture(void* copy,size_t index,void* donor,const std::vector<int32_t>& pins) {
    auto& job=*g_binding_job;const auto& tex=job.plan->data.textures.at(index);
    std::string sampler;
    if (!TextureSamplerIdentity(donor,sampler)) {
        sampler="source:";const auto source=reinterpret_cast<uintptr_t>(donor);
        sampler.append(reinterpret_cast<const char*>(&source),sizeof(source));
    }
    ++job.scope.texture_references;
    for (auto& work:job.textures) if (work.index==index && work.sampler==sampler) {
        if (!work.live) {++job.scope.texture_dedup_hits;job.scope.texture_dedup_bytes+=tex.info.data_size;}
        work.pins.emplace_back(copy,pins);return true;
    }
    ModelTextureWork work;work.index=index;work.donor=donor;work.sampler=std::move(sampler);
    work.identity=BemTextureContentIdentity(job.plan->data,index);
    if (!work.identity.empty() && !g_generated_texture_identity.empty()) {
        if (!job.live_indexed) {job.live_indexed=true;IndexLivePublishedTextures(*job.mod.adapter,job.live_textures);}
        const auto found=job.live_textures.find(work.identity);
        if (found!=job.live_textures.end() && LiveTextureMatches(found->second,tex.info,work.sampler))
            work.object=RootTemporary(found->second);
    }
    if (work.object) {
        work.phase=3;work.live=true;++job.texture_hits;
        ++job.scope.texture_live_reuse;job.scope.texture_live_reuse_bytes+=tex.info.data_size;
    }
    work.pins.emplace_back(copy,pins);job.textures.push_back(std::move(work));return true;
}
// Ask the shared streamer for one deferred texture entry (no-op otherwise).
void RequestModelTexturePayload(ModelBuildJob& job,ModelTextureWork& work) {
    if (work.decode_requested || work.phase>1 || !g_texture_streamer) return;
    const auto& tex=job.plan->data.textures[work.index];
    if (!tex.Deferred() || !job.plan->data.payload_source) return;
    auto source=job.plan->data.payload_source;BemTexture entry=tex;
    work.decode_requested=true;
    work.decode=g_texture_streamer->Request(tex.info.data_size,job.priority,
        [source=std::move(source),entry=std::move(entry)](std::vector<uint8_t>& bytes,std::string& error) {
            return DecodeBemTexturePayload(*source,entry,bytes,error);
        });
}
bool BindModelTexture(ModelTextureWork& work) {
    for (const auto& [copy,pins]:work.pins) for (int32_t slot:pins) {
        void* args[]{&slot,work.object};void* read[]{&slot};
        if (!InvokeVoid(Contract("material.set_texture_by_id"),copy,args) ||
            Invoke(Contract("material.get_texture_by_id"),copy,read)!=work.object) return false;
    }
    return true;
}
bool AdvanceModelTexture(ModelBuildJob& job,uint64_t frame) {
    auto& work=job.textures[job.texture];const auto& planned=job.plan->data.textures[work.index];const auto& info=planned.info;
    if (work.phase==3) {
        auto permit=g_model_frame_budget.TryBegin(frame,{0,1,false,false});if (!permit) return true;
        if (!BindModelTexture(work)) return JobFail(job,"cached texture bind/readback failed t="+planned.name);
        ++job.texture;return true;
    }
    const bool streamed=planned.Deferred() && job.plan->data.payload_source;
    if (streamed) RequestModelTexturePayload(job,work); // decode overlaps the ctor frame
    else if (!AwaitModelDecode(job)) return !job.invalid;
    const auto& tex=streamed?planned:job.decoded->data.textures[work.index];
    const uint64_t payload_bytes=info.data_size;
    if (payload_bytes>256*kLoadingMiB) return JobFail(job,"texture exceeds exclusive submission limit t="+tex.name);
    // Phase 1 takes the decoded bytes before it asks for an upload permit. A
    // refused permit keeps them on the Job (still counted) for the next frame.
    if (streamed && work.phase==1 && !job.pending_bytes) {
        if (!work.decode) return JobFail(job,"texture decode request refused t="+tex.name);
        auto taken=g_texture_streamer->Take(work.decode);
        if (taken.status==AsyncLoadStatus::Pending) return true;
        if (taken.status!=AsyncLoadStatus::Ready) return JobFail(job,"texture payload decode failed t="+tex.name+" "+taken.error);
        job.pending_bytes=std::move(taken.bytes);work.decode.Reset();
        job.scope.HoldDecoded(job.pending_bytes->size());
    }
    auto permit=g_model_frame_budget.TryBegin(frame,{work.phase?payload_bytes:0,1,true,true});
    if (!permit) return true;
    job.scope.current_frame=frame;
    if (work.phase==0) {
#if defined(__ANDROID__)
        int32_t supported_format=info.create_format;uint8_t supported=0;void* supported_args[]{&supported_format};
        if (!InvokeValue(Contract("android.texture_supported"),nullptr,supported_args,supported) || !supported)
            return JobFail(job,"texture format unsupported t="+tex.name+" format="+std::to_string(supported_format));
#endif
        work.object=NewAsset(g_texture2d_class.class_info);
        int32_t width=info.width,height=info.height,format=info.create_format,mips=info.mip_count;
        uint8_t linear=!info.create_srgb;void* args[]{&width,&height,&format,&mips,&linear};
        if (!work.object || !InvokeVoid(Contract("texture2d.ctor"),work.object,args)) return JobFail(job,"Texture2D ctor failed t="+tex.name);
        ++job.scope.texture_constructed;
    } else if (work.phase==1) {
        const std::vector<uint8_t>& data_bytes=streamed?*job.pending_bytes:tex.data;
        if (data_bytes.empty() || data_bytes.size()>INT32_MAX || data_bytes.size()!=info.data_size) return JobFail(job,"texture payload size mismatch t="+tex.name);
        void* data=const_cast<uint8_t*>(data_bytes.data());int32_t bytes=static_cast<int32_t>(data_bytes.size());void* args[]{&data,&bytes};
        const bool loaded=InvokeVoid(Contract("texture2d.load_raw_texture_data"),work.object,args);
        if (streamed) {job.scope.ReleaseDecoded(job.pending_bytes->size());job.pending_bytes.reset();} // Unity owns its copy now
        if (!loaded) return JobFail(job,"LoadRawTextureData failed t="+tex.name);
        // Prefetch the next texture only now: a Job holds at most one decoded
        // texture, and every held byte is released without waiting on the
        // streamer, so an exclusive (over-budget) entry cannot deadlock.
        for (size_t next=job.texture+1;next<job.textures.size();++next)
            if (job.textures[next].phase<2) {RequestModelTexturePayload(job,job.textures[next]);break;}
    } else {
        // Count attempted submissions, including a failed Apply or later rollback.
        ++job.scope.texture_submitted;job.scope.texture_payload_bytes+=payload_bytes;
        job.scope.largest_texture_payload_bytes=std::max(job.scope.largest_texture_payload_bytes,payload_bytes);
        job.scope.RecordUpload(payload_bytes);
        uint8_t mips=0,unreadable=1;void* args[]{&mips,&unreadable};
        if (!InvokeVoid(Contract("texture2d.apply"),work.object,args)) return JobFail(job,"Texture2D.Apply failed t="+tex.name);
        int32_t width=0,height=0,format=0,expected=0;
        if (!InvokeValue(Contract("texture.get_width"),work.object,nullptr,width) ||
            !InvokeValue(Contract("texture.get_height"),work.object,nullptr,height) ||
            !InvokeValue(Contract("texture.get_graphics_format"),work.object,nullptr,format) ||
            !SlotGraphicsFormat(info.create_format,info.create_srgb,expected) || width!=int32_t(info.width) ||
            height!=int32_t(info.height) || format!=expected || !CopySamplerState(work.donor,work.object))
            return JobFail(job,"built texture readback/sampler mismatch t="+tex.name+" size="+std::to_string(width)+"x"+
                std::to_string(height)+" format="+std::to_string(format)+" expected="+std::to_string(expected));
#if defined(__ANDROID__)
        betterendfield::AndroidAuditTextureColorSpace(work.donor,work.object,tex.original_name);
        betterendfield::AndroidAuditNormalTexture(work.donor,tex.original_name);
#endif
        RememberGeneratedTexture(work.object,work.identity);
    }
    ++work.phase;
    Log("Model texture step frame="+std::to_string(frame)+" resource="+job.scope.resource_name+" t="+tex.name+
        " phase="+std::to_string(work.phase)+" bytes="+std::to_string(work.phase==1?0:payload_bytes)+
        " streamed="+(streamed?"true":"false")+" oversize="+std::to_string(g_model_frame_budget.Snapshot().oversize));
    return true;
}
std::string ModelMeshKey(ModelBuildJob& job,size_t index,void* poses,std::vector<void*>& donors) {
    const auto& c=job.plan->data.components[index];const auto& target=job.bindings[index];
    std::string key="mesh-v2";AssetKeyText(key,job.mod.selection_key);AssetKeyValue(key,c.info);
    AssetKeyValue(key,c.static_mesh);AssetKeyText(key,target.receiver_key.resource);AssetKeyText(key,target.receiver_key.path);
    AssetKeyValue(key,job.file_lease->epoch);
    for (const auto& a:c.attributes) AssetKeyValue(key,a);
    for (const auto& d:c.draws) AssetKeyValue(key,d);
    donors.push_back(DonorMesh(target));
    for (size_t i=0;i<c.bones.size();++i) {
        AssetKeyValue(key,c.bones[i]);const auto* source=FindPrepared(job.bindings,c.bones[i].component);
        if (!source) return {};
        donors.push_back(DonorMesh(*source));
        std::string path;
        if (!ResourceRelativePath(job.ui_asset,ArrayValue(target.custom_bones,int(i)),path)) return {};
        AssetKeyText(key,path);
        Matrix4x4Raw matrix{};if (!Unbox(ArrayValue(poses,int(i)),matrix)) return {};
        AssetKeyValue(key,matrix);
    }
    for (void* donor:donors) {
        int32_t id=0;if (!InvokeValue(Contract("object.instance_id"),donor,nullptr,id)) return {};
        AssetKeyValue(key,id);
    }
    return key;
}
bool ModelBindingsUnchanged(const std::vector<PreparedBinding>& bindings) {
    for (const auto& b:bindings) {
        bool enabled=false;
        if (!IsNativeObjectAlive(b.renderer) || !IsNativeObjectAlive(b.original_mesh) ||
            GetRendererMesh(b.renderer)!=b.original_mesh ||
            !GetRendererEnabled(b.renderer,enabled) || enabled!=b.original_enabled) return false;
        void* materials=Invoke(Contract("renderer.get_shared_materials"),b.renderer,nullptr);
        if (ArrayLength(materials)!=ArrayLength(b.original_materials)) return false;
        for (int i=0;i<ArrayLength(materials);++i) if (ArrayValue(materials,i)!=ArrayValue(b.original_materials,i)) return false;
        void* bones=GetRendererBones(b.renderer);
        if (ArrayLength(bones)!=ArrayLength(b.original_bones)) return false;
        for (int i=0;i<ArrayLength(bones);++i) if (ArrayValue(bones,i)!=ArrayValue(b.original_bones,i)) return false;
#if defined(__ANDROID__)
        if (b.change_shadow) {
            int32_t mode=-1;void* proxy=nullptr;
            if (!InvokeValue(Contract("android.shadow_get"),b.renderer,nullptr,mode) || mode!=b.original_shadow ||
                !ReadNullableObject(Contract("android.shadow_mesh_get"),b.renderer,proxy) || proxy!=b.original_shadow_mesh) return false;
        }
#endif
    }
    return true;
}
bool CommitModelJob(ModelBuildJob& job,uint64_t frame) {
    auto permit=g_model_frame_budget.TryBegin(frame,{0,1,true,false});if (!permit) return true;
    if (ActiveSelectionKey(*job.mod.adapter)!=job.mod.selection_key) return JobFail(job,"selection changed before commit");
    if (!job.target.Get()) return JobFail(job,"target released before commit");
    if (!ModelBindingsUnchanged(job.bindings)) return JobFail(job,"captured bindings changed before commit");
    auto transaction=job.bindings;CompletedResource completed,ui_completed;
    void* paired_ui=nullptr;
#if defined(__ANDROID__)
    if (job.mod.adapter->explicit_resource && !PrepareExplicitAndroidShadows(*job.mod.adapter,job.plan->data,job.asset,transaction))
        return JobFail(job,"explicit Android shadow ownership or transaction unavailable");
    if (job.asset!=job.ui_asset) {
        std::vector<PreparedBinding> world,ui;
        struct ReadyOverride {
            ReadyOverride(ModelBuildJob& j) {g_ready_resource=j.ui_asset;g_ready_bindings=&j.bindings;g_verified_mesh_space_roots={j.ui_asset,j.asset};}
            ~ReadyOverride() {g_ready_resource=nullptr;g_ready_bindings=nullptr;g_verified_mesh_space_roots={};}
        } ready(job);
        if (!PrepareAndroidWorldResource(*job.mod.adapter,job.plan->data,job.asset,world,&ui,&paired_ui,
            AndroidLodRelations(),AndroidLodAssetScope()))
            return JobFail(job,"Android world LOD1 preparation refused (see preceding adapter log)");
        // Adapter's geometry is shared, but each receiver has private materials.
        for (auto& b:world) {
            void* source=b.custom_materials;b.custom_materials=Invoke(Contract("array.clone"),source,nullptr);
            if (!b.custom_materials) return JobFail(job,"world material array clone failed");
            for (int i=0;i<ArrayLength(source);++i) {
                void* material=ArrayValue(source,i);void* copy=NewAsset(g_material_class.class_info);void* args[]{material};
                if (!copy || !InvokeVoid(Contract("material.copy"),copy,args) || !MaterialCopyCompatible(material,copy) || !SetArrayValue(b.custom_materials,i,copy))
                    return JobFail(job,"world private material copy failed");
#if defined(__ANDROID__)
                if (!betterendfield::AndroidAuditMaterialCopy(material,copy)) return JobFail(job,"world material copy audit failed");
#endif
            }
        }
        if (!RememberResource(*job.mod.adapter,job.asset,world,completed,job.mod.selection_key,true)) return JobFail(job,"world completion record failed");
        if (ui.empty()) ui=job.bindings;
        paired_ui=job.ui_asset;
        if (!RememberResource(*job.mod.adapter,paired_ui,ui,ui_completed,job.mod.selection_key,true)) return JobFail(job,"UI completion record failed");
        transaction=std::move(world);transaction.insert(transaction.end(),ui.begin(),ui.end());
    } else
#endif
    if (!RememberResource(*job.mod.adapter,job.asset,transaction,completed,job.mod.selection_key,true)) return JobFail(job,"completion record failed");
    if (job.scope.failed) return JobFail(job,"temporary object rooting failed");
    if (!ModelBindingsUnchanged(transaction)) return JobFail(job,"receiver bindings changed before commit");
    g_completed.reserve(g_completed.size()+2);
    const auto result=CommitResource<PreparedBinding>(transaction,ApplyPreparedBinding,RestorePreparedBinding);
    if (result!=CommitResult::Committed) {
        if (result==CommitResult::RestoreFailed) {job.scope.published=true;Log("CRITICAL model async rollback failed; bound assets preserved");}
        return JobFail(job,result==CommitResult::RestoreFailed?"commit rollback failed":"renderer commit rejected and restored");
    }
    job.scope.published=true; // Set before any diagnostic/bookkeeping allocation.
#if defined(__ANDROID__)
    if (betterendfield::AndroidMeshRollbackTest()) {
        bool restored=true;
        for (auto it=transaction.rbegin();it!=transaction.rend();++it) if (!RestorePreparedBinding(*it)) restored=false;
        job.scope.published=!restored;
        Log(std::string("Android renderer commit/restore ")+(restored?"PASS: ":"FAIL: ")+job.scope.resource_name);
        if (!restored) return JobFail(job,"rollback-test restore failed");
        for (auto& target:g_model_targets) if (target.root.Get()==job.asset) target.failed_selection=job.mod.selection_key;
        job.phase=ModelBuildJob::Phase::Done;return true;
    }
#endif
    PublishCompleted(std::move(completed),job.asset);
    if (paired_ui) PublishCompleted(std::move(ui_completed),paired_ui);
    for (auto& asset:job.publish) {
        try {PublishModelAsset(std::move(asset.key),asset.object,asset.donors,asset.bytes);} catch (...) { /* Cache is optional. */ }
    }
    Log("Model Ready committed frame="+std::to_string(frame)+" resource="+job.scope.resource_name+
        " meshHits="+std::to_string(job.mesh_hits)+" textureLiveHits="+std::to_string(job.texture_hits)+
        " uniqueTextures="+std::to_string(job.textures.size())+" textureRefs="+std::to_string(job.scope.texture_references));
    job.phase=ModelBuildJob::Phase::Done;return true;
}
bool AdvanceModelJob(ModelBuildJob& job,uint64_t frame) {
    ConstructionActivation activation(job.scope);
    using Phase=ModelBuildJob::Phase;
    if (job.phase==Phase::Decode) {
        if (!AwaitModelDecode(job)) return !job.invalid;
        job.phase=Phase::Capture;return true;
    }
    if (job.phase==Phase::Capture) {
        auto permit=g_model_frame_budget.TryBegin(frame,{0,1,true,false});if (!permit) return true;
        job.asset=job.target.Get();if (!job.asset) return JobFail(job,"target released before capture");
        job.scope.resource_name=ObjectName(job.asset);job.ui_asset=job.asset;
#if defined(__ANDROID__)
        if (!job.mod.adapter->explicit_resource && ResourceBaseName(job.scope.resource_name)==job.mod.adapter->world_resource) {
            // g_in_delivery supplies OriginalOnly for this synchronous donor acquisition.
            job.ui_asset=RootTemporary(betterendfield::AndroidLoadUiDonor(job.mod.adapter->ui_resource,job.donor_handle,job.donor_handle_root));
            if (!job.ui_asset) return JobFail(job,"Android UI donor unavailable: "+std::string(job.mod.adapter->ui_resource));
        }
#endif
        if (!RootTemporary(job.asset)) return JobFail(job,"target rooting failed");
        if (!PrepareResource(*job.mod.adapter,job.plan->data,job.ui_asset,job.bindings,true))
            return JobFail(job,"generic receiver capture refused on "+ObjectName(job.ui_asset));
        job.phase=Phase::Mesh;return true;
    }
    if (job.phase==Phase::Mesh) {
        if (job.component==job.bindings.size()) {job.component=0;job.phase=Phase::Materials;return true;}
        const auto& c=job.plan->data.components[job.component];auto& b=job.bindings[job.component];
        if (c.info.flags&kComponentFlagNoGeometry) {++job.component;return true;}
        // The plan is metadata-only (vertex streams dropped). Palette bones and
        // bindposes need metadata only; the packed skin stream is validated from
        // the decoded payload immediately before an actual Mesh build below.
        // (Validating it from the plan rejected every geometry component.)
        if (!job.palette_poses && !PreparePalette(c,b,job.bindings,job.palette_poses,job.mod.skip_validation,false))
            return JobFail(job,"palette preparation refused C"+std::to_string(c.info.component_id));
        void* poses=job.palette_poses;
        std::vector<void*> donors;const auto key=ModelMeshKey(job,job.component,poses,donors);
        if (key.empty()) return JobFail(job,"Mesh identity key unavailable C"+std::to_string(c.info.component_id));
        b.custom_mesh=BorrowModelAsset(key,donors);
        if (b.custom_mesh) {++job.mesh_hits;++job.component;job.palette_poses=nullptr;return true;}
        if (!AwaitModelDecode(job)) return !job.invalid;
        const auto& decoded=job.decoded->data.components[job.component];
        if (!decoded.static_mesh) {
            std::vector<uint8_t> counts;std::vector<BoneWeight1Raw> weights;
            if (!DecodeComponentSkin(decoded,counts,weights))
                return JobFail(job,"decoded skin stream invalid C"+std::to_string(c.info.component_id));
        }
        const auto bytes=job.plan->mesh_bytes[job.component];
        auto permit=g_model_frame_budget.TryBegin(frame,{bytes,1,true,false});if (!permit) return true;
        job.scope.current_frame=frame;
        if (!g_model_build_mesh(decoded,DonorMesh(b),b.custom_mesh,poses))
            return JobFail(job,"Mesh build/readback refused C"+std::to_string(c.info.component_id));
        job.publish.push_back({key,b.custom_mesh,std::move(donors),bytes});++job.component;job.palette_poses=nullptr;return true;
    }
    if (job.phase==Phase::Materials) {
        if (job.component==job.bindings.size()) {job.phase=Phase::Texture;return true;}
        auto permit=g_model_frame_budget.TryBegin(frame,{0,1,true,false});if (!permit) return true;
        struct DeferredBinding {
            explicit DeferredBinding(ModelBuildJob& j) {g_binding_job=&j;g_deferred_texture=&StageModelTexture;}
            ~DeferredBinding() {g_binding_job=nullptr;g_deferred_texture=nullptr;}
        } deferred(job);
        const auto& c=job.plan->data.components[job.component];auto& b=job.bindings[job.component];
        if (!(c.info.flags&kComponentFlagNoGeometry)) {
            if (!PrepareDrawMaterials(c,b,job.bindings,job.plan->data))
                return JobFail(job,"draw material preparation refused C"+std::to_string(c.info.component_id));
        } else if (!PrepareKeepMaterials(c,b,job.plan->data))
            return JobFail(job,"keep material preparation refused C"+std::to_string(c.info.component_id));
        ++job.component;return true;
    }
    if (job.phase==Phase::Texture) {
        if (job.texture==job.textures.size()) {job.phase=Phase::Commit;return true;}
        return AdvanceModelTexture(job,frame);
    }
    if (job.phase==Phase::Commit) return CommitModelJob(job,frame);
    return true;
}
const char* LoadPriorityName(LoadPriority priority) {
    return priority==LoadPriority::Foreground?"Foreground":priority==LoadPriority::Visible?"Visible":"Prewarm";
}
// Returns false when this target has no Job of its own yet (it waits behind
// its role's builder or a full queue) and must be offered again later.
bool ModelInstanceRebindPending(const CharacterAdapter& adapter);
bool EnqueueResourceBuild(void* asset,const EnabledMod& mod,LoadPriority priority=LoadPriority::Visible) {
    if (!IsGameObjectResource(asset)) return false;
    // A scene rebind can take several frames. Keep a later UI/world target
    // from recreating a Job while this role still has synchronous work queued.
    if (ModelInstanceRebindPending(*mod.adapter)) return false;
    for (const auto& job:g_model_jobs) if (job->target.Is(asset)) {
        job->priority=priority;if (job->ticket) g_model_loader->SetPriority(job->ticket,priority);
        if (g_texture_streamer) for (const auto& work:job->textures) if (work.decode) g_texture_streamer->SetPriority(work.decode,priority);
        return true;
    }
    // One builder per role: later template/clones attach using the published pool.
    for (const auto& job:g_model_jobs) if (SameAdapter(*job->mod.adapter,*mod.adapter)) {
        job->priority=std::max(job->priority,priority);
        if (job->ticket) g_model_loader->SetPriority(job->ticket,job->priority);return false;
    }
    if (g_model_jobs.size()>=5) {
        static unsigned logged=0;
        if (logged++<8) Log("Model Job deferred: queue full jobs="+std::to_string(g_model_jobs.size())+" resource="+ObjectName(asset));
        return false;
    }
    auto job=std::make_unique<ModelBuildJob>();job->mod=mod;job->owner=OwnAdapter(*mod.adapter);
    job->priority=priority;job->scope.delivery_mode="frame-sliced";
    job->file_lease=AcquireModelFileLease(mod.package);
    if (!job->file_lease) {Log("Model Job not queued: package unreadable "+mod.package.string());return true;}
    if (!job->target.Set(asset)) {Log("Model Job not queued: target weak reference failed");return true;}
    job->served=++g_model_service_serial;
    const bool cached=g_model_plans.contains(mod.selection_key);
    if (cached) {
        auto& plan=g_model_plans.at(mod.selection_key);
        job->plan=plan;job->plan->used=GetTickCount64();job->phase=ModelBuildJob::Phase::Capture;
    } else job->ticket=RequestModelCpu(mod,job->file_lease,priority);
    Log("Model Job queued resource="+ObjectName(asset)+" role="+std::string(mod.adapter->id)+
        " priority="+LoadPriorityName(priority)+" plan="+(cached?"cached":job->ticket?"decode-requested":"decode-pending")+
        " jobs="+std::to_string(g_model_jobs.size()+1));
    g_model_jobs.push_back(std::move(job));
    return true;
}
KnownModelTarget* RememberModelTarget(void* asset,const EnabledMod& mod) {
    if (!IsGameObjectResource(asset)) return nullptr;
    for (auto& target:g_model_targets) if (target.root.Get()==asset) {target.failed_selection.clear();return &target;}
    if (g_model_targets.size()>=128) return nullptr;
    KnownModelTarget target;target.owner=OwnAdapter(*mod.adapter);target.adapter=mod.adapter;
    if (!target.root.Set(asset)) return nullptr;
    bool inactive=true;void* args[]{ModelRendererType(),&inactive};
    void* renderers=Invoke(Contract("game_object.renderers"),asset,args);
    const int count=ArrayLength(renderers);
    if (count<0 || count>4096) return nullptr;
    for (int i=0;i<count;++i) {
        WeakObject renderer;if (renderer.Set(ArrayValue(renderers,i))) target.renderers.push_back(std::move(renderer));
    }
    g_model_targets.push_back(std::move(target));
    return &g_model_targets.back();
}
void CancelModelJobs() {
    for (auto& job:g_model_jobs) if (job->ticket && g_model_loader) g_model_loader->Cancel(job->ticket);
    g_model_jobs.clear();
    std::lock_guard lock(g_model_delivery_mutex);g_model_deliveries.clear();
}
void InitializeModelJobs() {
    if (g_model_loader) return;
    AsyncBemLoader::Backend backend;
    backend.load=[](const BemRequest& request,uint64_t reservation,BemPocData& data,BemLoadStats& stats,std::string& error) {
        return LoadBem(request.path,data,error,request.appearance,&stats,request.skip_validation,
            request.loading_optimization,request.parameters,reservation,request.defer_texture_payloads,request.resource_id);
    };
    g_model_loader=std::make_unique<AsyncBemLoader>(AsyncBemLoader::Config{},std::move(backend));
    if (!g_texture_streamer) g_texture_streamer=std::make_unique<TexturePayloadStreamer>();
}
// Last resort for a Job which failed after CPU decode succeeded: run the
// proven synchronous transaction once on this pump (same compatibility checks).
// A refused package is never retried in a loop; failed_selection gates it.
void RunSynchronousModelFallback(void* asset,const std::string& resource,const std::string& reason) {
    Log("Model fallback=synchronous resource="+resource+" reason="+reason);
    bool ok=false;
    try {ConstructionScope fallback;fallback.delivery_mode="synchronous-fallback";ok=ProcessResource(asset,fallback);}
    catch (const std::exception& error) {Log(std::string("Model synchronous fallback failed: ")+error.what());}
    catch (...) {Log("Model synchronous fallback failed with a native C++ exception.");}
    Log(std::string("Model fallback result=")+(ok?"committed":"refused")+" resource="+resource);
}
uint64_t g_model_next_status_ms=0;
bool ModelRootPosition(void* object,bool component,Float3& position) {
    if (!component && !IsGameObjectResource(object)) return false;
    void* transform=Invoke(Contract(component?"component.get_transform":"game_object.get_transform"),object,nullptr);
    Matrix4x4Raw matrix{};
    if (!transform || !InvokeValue(Contract("transform.local_to_world"),transform,nullptr,matrix)) return false;
    position={matrix.m[12],matrix.m[13],matrix.m[14]};
    return std::isfinite(position.x) && std::isfinite(position.y) && std::isfinite(position.z);
}
// Upload order across the shared budget (refreshed every 100 ms): a visible
// receiver is Foreground; with a main camera, only the visible receiver nearest
// to it (normally the controlled character) stays Foreground and other visible
// team members become Visible. This is an ordering hint, not an identity claim.
void RefreshModelTargetPriorities(uint64_t now) {
    static uint64_t next_ms=0;
    if (now<next_ms && std::none_of(g_model_targets.begin(),g_model_targets.end(),[](const auto& t){return t.next_visibility_ms==0;})) return;
    next_ms=now+100;
    Float3 camera{};bool camera_known=false;
    if (auto* main=Contract("camera.get_main");main && main->resolved)
        if (void* cam=Invoke(main,nullptr,nullptr,false)) camera_known=ModelRootPosition(cam,true,camera);
    KnownModelTarget* nearest=nullptr;float nearest_distance=0;
    for (auto& target:g_model_targets) {
        target.next_visibility_ms=now+100;
        bool visible=false;
        for (const auto& weak:target.renderers) if (void* renderer=weak.Get()) {
            uint8_t value=0;
            if (InvokeValue(Contract("renderer.is_visible"),renderer,nullptr,value) && value) {visible=true;break;}
        }
        target.priority=visible?LoadPriority::Foreground:LoadPriority::Visible;
        Float3 position{};void* root=target.root.Get();
        if (visible && camera_known && root && ModelRootPosition(root,false,position)) {
            const float dx=position.x-camera.x,dy=position.y-camera.y,dz=position.z-camera.z,distance=dx*dx+dy*dy+dz*dz;
            if (!nearest || distance<nearest_distance) {nearest=&target;nearest_distance=distance;}
        }
    }
    if (nearest) for (auto& target:g_model_targets)
        if (&target!=nearest && target.priority==LoadPriority::Foreground) target.priority=LoadPriority::Visible;
}
// Hot switch only rebuilds registered receivers (delivered templates). Scene
// instances were cloned from a template before the switch and keep the
// previous generation's Mesh/material references; the game does not deliver
// them again. After the template Jobs of a new revision settle, one scan finds
// every renderer that still shows a Mesh this module generated for a role whose
// active selection differs. Previously restored instances also have verified
// root/adapter records: re-enable those directly even though their Mesh is now
// Original. Each root uses the synchronous transaction, one root per frame.
uint64_t g_instance_rebind_handled=0;
std::vector<std::unique_ptr<StrongReference>> g_instance_rebind_queue;
bool ModelInstanceRebindPending(const CharacterAdapter& adapter) {
    for (const auto& pending:g_instance_rebind_queue) if (void* root=pending->Get()) {
        const auto name=ObjectName(root);const auto base=ResourceBaseName(name);
        if (base==adapter.world_resource || base==adapter.ui_resource) return true;
    }
    return false;
}
int32_t ModelInstanceId(void* object) {
    int32_t id=0;
    return object && IsNativeObjectAlive(object) && InvokeValue(Contract("object.instance_id"),object,nullptr,id)?id:0;
}
enum class ModelDiscoveryPlatform { Windows, Android };
#if defined(__ANDROID__)
constexpr auto kModelDiscoveryPlatform=ModelDiscoveryPlatform::Android;
#else
constexpr auto kModelDiscoveryPlatform=ModelDiscoveryPlatform::Windows;
#endif
// Discovery has no decoded payload and does not construct a Mesh/material.
// Check every declared donor before admitting a previously untracked root;
// ProcessResource repeats the full production checks before publication.
bool HasCompatibleModelTarget(const EnabledMod& mod,void* asset,std::string& reason,
    ModelDiscoveryPlatform platform=kModelDiscoveryPlatform) {
    if (!IsGameObjectResource(asset)) {reason="not-GameObject";return false;}
    const auto& adapter=*mod.adapter;
    // Legacy Android world bindings use the paired UI LOD0 -> world LOD1
    // adapter. They cannot be admitted by this own-donor preflight.
    if (platform==ModelDiscoveryPlatform::Android && !adapter.explicit_resource) {
        reason="Android-legacy-requires-paired-world-route";return false;
    }
    if (platform==ModelDiscoveryPlatform::Windows && adapter.explicit_resource && adapter.receiver_lod!=0) {
        reason="unsupported-Windows-LOD";return false;
    }
    if (adapter.explicit_resource && std::any_of(adapter.components.begin(),adapter.components.end(),[&](const auto& identity) {
        return !GenericMatching::ReceiverLodAgrees(identity.receiver_path,adapter.receiver_lod);
    })) {reason="receiver-resource-LOD-mismatch";return false;}
    bool inactive=true;void* args[]{ModelRendererType(),&inactive};
    void* renderers=Invoke(Contract("game_object.renderers"),asset,args);
    std::vector<GenericRendererCandidate> index;
    if (!BuildGenericRendererIndex(adapter,asset,renderers,index) || index.empty()) {reason="renderer-index-unavailable";return false;}
    std::vector<GenericMatching::Candidate> candidates;
    for (const auto& entry:index) {
        GenericMatching::Candidate c;c.key=entry.key;c.renderer=entry.renderer;
        ReadGenericPristineMesh(adapter,asset,entry,c.pristine);candidates.push_back(std::move(c));
    }
    std::vector<void*> receivers;
    for (const auto& identity:adapter.components) {
        GenericMatching::Request request{adapter.id,candidates.front().key.resource,identity.name,
            adapter.explicit_resource?GenericMatching::Region::Explicit:GenericMatching::Region::Lod0,
            identity.indices,!mod.skip_validation,adapter.explicit_resource?identity.receiver_path:""};
        const auto match=GenericMatching::SelectUnique(candidates,request,[&](const auto& c) {
            const auto found=std::find_if(index.begin(),index.end(),[&](const auto& entry){return entry.renderer==c.renderer;});
            return found!=index.end() && !GenericCandidateContractError(asset,*found,identity.static_mesh);
        });
        if (match.status!=GenericMatching::MatchStatus::Matched) {
            reason="donor="+std::string(identity.name)+DescribeGenericDonorFailure(asset,index,candidates,request,match.status,identity.static_mesh);
            return false;
        }
        receivers.push_back(index[match.index].renderer);
    }
    if (!GenericMatching::DistinctReceivers(receivers)) {reason="duplicate-receivers";return false;}
    return true;
}
bool ScanSceneInstancesForRebind(ModelDiscoveryPlatform platform=kModelDiscoveryPlatform) {
    const auto scan_started_ms=GetTickCount64();
    // Generated Meshes of a generation that is no longer the role's selection.
    std::unordered_map<int32_t,const CharacterAdapter*> stale;
    std::vector<std::pair<void*,const CharacterAdapter*>> restored_roots;
    for (const auto& record:g_completed) {
        const auto active=ActiveSelectionKey(*record.adapter);
        if (record.selection_key==active) continue;
        if (record.selection_key.empty()) {
            // Disable wrote this record only after a validated restoration.
            // It remains useful ownership evidence while the root is alive;
            // requiring generated Mesh identity here loses pooled instances.
            if (void* root=record.root.Get()) restored_roots.emplace_back(root,record.adapter);
            continue;
        }
        // The recorded instance ID survives a recycled managed wrapper.
        for (const auto& binding:record.bindings) if (binding.generated_mesh && binding.mesh.instance_id)
            stale.emplace(binding.mesh.instance_id,record.adapter);
    }
    std::unordered_map<std::string,const EnabledMod*> discover;
    // An off Mod is not registered at delivery. When it is first enabled,
    // cached prefabs and their already-existing clones have no completed
    // generation. Restrict discovery to precisely the changed resource names.
    for (const auto& mod:g_registry.enabled) {
        if (platform==ModelDiscoveryPlatform::Android && !mod.adapter->explicit_resource) continue;
        for (const auto* resource:{mod.adapter->world_resource,mod.adapter->ui_resource})
            if (g_pending_model_resources.contains(resource)) discover.emplace(resource,&mod);
    }
    if (stale.empty() && restored_roots.empty() && discover.empty()) {
        if (!g_pending_model_resources.empty()) Log("Hot switch discovery: no affected loaded generation or enabled resource");
        g_pending_model_resources.clear();return true;
    }
    std::unordered_set<int32_t> queued;
    for (const auto& pending:g_instance_rebind_queue) queued.insert(ModelInstanceId(pending->Get()));
    size_t found=0;
    const auto queue_root=[&](void* object,const CharacterAdapter& role) {
        if (!IsGameObjectResource(object)) return;
        const auto name=ObjectName(object);const auto base=ResourceBaseName(name);
        if (base!=role.world_resource && base!=role.ui_resource) return;
        const int32_t id=ModelInstanceId(object);
        if (!id || queued.contains(id)) return;
        auto reference=std::make_unique<StrongReference>();
        if (!reference->Set(object)) return;
        queued.insert(id);
        // Keep synchronous rebinds and frame-sliced Jobs mutually exclusive
        // for this role in the revision, including restored templates.
        const bool registered=std::any_of(g_model_targets.begin(),g_model_targets.end(),[&](const auto& target){return target.root.Is(object);});
        std::erase_if(g_model_jobs,[&](const auto& job) {
            if (!SameAdapter(*job->mod.adapter,role)) return false;
            if (job->ticket) g_model_loader->Cancel(job->ticket);
            // A template absent from FindAll is not necessarily in the rebind
            // queue. Its cancelled Job must not leave a false "already built"
            // desired key: admit it again once synchronous role work finishes.
            for (auto& target:g_model_targets) if (target.root.instance_id==job->target.instance_id) {
                target.desired.clear();target.verified_selection.clear();target.next_verify_ms=0;
            }
            Log("Hot switch template Job replaced by synchronous rebind resource="+name);
            return true;
        });
        if (registered) g_instance_rebind_queue.push_back(std::move(reference));
        else g_instance_rebind_queue.insert(g_instance_rebind_queue.begin(),std::move(reference));
        ++found;
    };
    for (const auto& [root,adapter]:restored_roots) queue_root(root,*adapter);
    const auto restored_queued=found;
    if (stale.empty() && discover.empty()) {
        Log("Hot switch instance scan renderers=0 staleMeshes=0 restoredRootsQueued="+std::to_string(restored_queued)+
            " instancesQueued="+std::to_string(found));g_pending_model_resources.clear();return true;
    }
    const auto* find_all=Contract("resources.find_all");const auto* game_object=Contract("component.get_game_object");
    if (!find_all || !find_all->resolved || !game_object || !game_object->resolved) {
        Log("Hot switch instance discovery unavailable: Resources.FindObjectsOfTypeAll/Component.gameObject not resolved; restoredRootsQueued="+
            std::to_string(restored_queued));return false;
    }
    void* args[]{ModelRendererType()};
    void* renderers=Invoke(find_all,nullptr,args);
    int32_t count=0,dimension=0;void* length_args[]{&dimension};
    if (!renderers || !InvokeValue(Contract("array.get_length"),renderers,length_args,count)) {
        Log("Hot switch instance discovery failed: renderer array/length unavailable");return false;
    }
    if (count<0 || count>65536) {Log("Hot switch instance rebind skipped: renderer count "+std::to_string(count));return false;}
    // Keep ownership discovery below, but do not walk the whole scene twice
    // more just to print diagnostic root and ancestor summaries.
    for (int i=0;!stale.empty() && i<count;++i) {
        void* renderer=ArrayValue(renderers,i);
        const auto owner=stale.find(ModelInstanceId(GetRendererMesh(renderer)));
        if (owner==stale.end()) continue;
        // The instance root is the nearest ancestor named after the role's
        // world/UI resource (Unity appends "(Clone)" to instances).
        void* root=nullptr;void* transform=Invoke(Contract("component.get_transform"),renderer,nullptr);
        for (int depth=0;transform && depth<64;++depth) {
            const std::string name(ResourceBaseName(ObjectName(transform)));
            if (name==owner->second->world_resource || name==owner->second->ui_resource) {root=transform;break;}
            transform=Invoke(Contract("transform.get_parent"),transform,nullptr);
        }
        void* object=root?Invoke(game_object,root,nullptr):nullptr;
        queue_root(object,*owner->second);
    }
    size_t discovered=0,refused=0;
    std::unordered_set<int32_t> checked;
    for (int i=0;!discover.empty() && i<count;++i) {
        void* renderer=ArrayValue(renderers,i);
        if (!IsModelRenderer(renderer) || !IsNativeObjectAlive(renderer)) continue;
        void* transform=Invoke(Contract("component.get_transform"),renderer,nullptr);
        for (int depth=0;transform && depth<64;++depth) {
            const auto name=ObjectName(transform);const auto role=discover.find(std::string(ResourceBaseName(name)));
            if (role!=discover.end()) {
                void* object=Invoke(game_object,transform,nullptr);const int32_t id=ModelInstanceId(object);
                if (id && !queued.contains(id) && checked.insert(id).second) {
                    std::string reason;
                    if (HasCompatibleModelTarget(*role->second,object,reason,platform)) {
                        const auto before=found;queue_root(object,*role->second->adapter);
                        if (found!=before) ++discovered;
                    } else if (refused++<8) Log("Hot switch discovery refused resource="+name+" reason="+reason);
                }
                break;
            }
            transform=Invoke(Contract("transform.get_parent"),transform,nullptr);
        }
    }
    Log("Hot switch discovery resources="+std::to_string(discover.size())+" rootsQueued="+std::to_string(discovered)+
        " rootsRefused="+std::to_string(refused));
    Log("Hot switch instance scan renderers="+std::to_string(count)+" staleMeshes="+std::to_string(stale.size())+
        " restoredRootsQueued="+std::to_string(restored_queued)+" instancesQueued="+std::to_string(found)+
        " elapsedMs="+std::to_string(GetTickCount64()-scan_started_ms));
    g_pending_model_resources.clear();return true;
}
uint64_t g_instance_scan_retry_ms=0;
void PumpInstanceRebind(uint64_t frame) {
    if (!g_hot_switch_runtime.load()) return;
    if (!g_instance_rebind_handled && g_pending_model_resources.empty()) {
        g_instance_rebind_handled=g_model_revision;return; // startup is not a switch
    }
    // The visible scene instance is rebound first. Registered templates are
    // rebuilt afterwards by their Jobs, which then reuse the instance's live
    // published textures instead of uploading them a second time.
    if (g_instance_rebind_handled!=g_model_revision && GetTickCount64()>=g_instance_scan_retry_ms) {
        bool scanned=false;
        try {scanned=ScanSceneInstancesForRebind();}
        catch (...) {Log("Hot switch instance scan failed with a native exception");}
        if (scanned) {g_instance_rebind_handled=g_model_revision;g_instance_scan_retry_ms=0;}
        else g_instance_scan_retry_ms=GetTickCount64()+1000;
    }
    if (g_instance_rebind_queue.empty()) return;
    auto permit=g_model_frame_budget.TryBegin(frame,{0,1,true,false});
    if (!permit) return;
    auto reference=std::move(g_instance_rebind_queue.front());
    g_instance_rebind_queue.erase(g_instance_rebind_queue.begin());
    void* asset=reference->Get();
    if (!asset || !IsNativeObjectAlive(asset)) return;
    const auto name=ObjectName(asset);
    bool ok=false;
    struct RebindFlag {RebindFlag(){g_instance_rebind_active=true;}~RebindFlag(){g_instance_rebind_active=false;}} flag;
    try {ConstructionScope scope;scope.delivery_mode="hot-switch-instance";ok=ProcessResource(asset,scope);}
    catch (const std::exception& error) {Log(std::string("Hot switch instance rebind failed: ")+error.what());}
    catch (...) {Log("Hot switch instance rebind failed with a native C++ exception.");}
    if (ok) for (auto& target:g_model_targets) if (target.root.Is(asset)) {
        const auto* mod=g_registry.Match(name);
        target.desired=mod?mod->selection_key:std::string{};
        target.verified_selection=target.desired;target.failed_selection.clear();
        target.next_verify_ms=GetTickCount64()+g_model_verify_interval_ms;
    }
    Log(std::string("Hot switch instance rebind ")+(ok?"committed":"refused")+" resource="+name);
}
void PumpModelJobs(uint64_t frame) {
    if (g_model_frame==frame || !g_model_loader) return;
    g_model_frame=frame;
    const auto now=GetTickCount64();
    if (!g_model_pump_ms.exchange(now))
        Log("Model pump first run frame="+std::to_string(frame)+" thread="+std::to_string(GetCurrentThreadId())+
            " cloneCoverage="+(g_model_clone_coverage.load()?"complete":"none")+
            " deliveryMode="+(ModelAsyncDeliveryBlocker(now)?"synchronous":"async"));
    std::erase_if(g_model_plans,[&](const auto& entry){return entry.second.use_count()==1 && entry.second->used+kModelPlanIdleMs<=now;});
    struct DeliveryGuard {DeliveryGuard(){g_in_delivery=true;}~DeliveryGuard(){g_in_delivery=false;}} guard;
    std::vector<PendingModelDelivery> deliveries;
    {std::lock_guard lock(g_model_delivery_mutex);deliveries.swap(g_model_deliveries);}
    for (auto& delivery:deliveries) {
        void* asset=delivery.asset->Get();if (!asset || !IsNativeObjectAlive(asset)) continue;
        if (!delivery.synchronous) {
            try {CaptureNativeProbe(asset);} catch (...) {Log("Native probe delivery failed");}
        }
        if (g_probe.sweep) continue;
        const auto name=ObjectName(asset);
        const EnabledMod* mod=g_registry.Match(name);
        std::string source_name;
        if (!mod && delivery.source) {source_name=ObjectName(delivery.source->Get());mod=g_registry.Match(source_name);}
        if (!mod) continue;
        auto* target=RememberModelTarget(asset,*mod);
        if (target && delivery.synchronous) {
            // The synchronous transaction already handled this selection.
            target->desired=mod->selection_key;target->verified_selection.clear();
            if (!delivery.refused_selection.empty()) target->failed_selection=delivery.refused_selection;
        }
        Log("Model delivery registered resource="+name+(source_name.empty()?"":" source="+source_name)+
            " role="+std::string(mod->adapter->id)+" mode="+(delivery.synchronous?"after-synchronous":"async")+
            (delivery.refused_selection.empty()?"":" refused=true")+" targets="+std::to_string(g_model_targets.size())+
            (target?"":" (target table full)"));
    }
    std::erase_if(g_model_jobs,[&](const auto& job) {
        if (job->target.Get() && ActiveSelectionKey(*job->mod.adapter)==job->mod.selection_key) return false;
        if (job->ticket) g_model_loader->Cancel(job->ticket);
        Log("Model stale Job cancelled afterSubmittedBytes="+std::to_string(job->scope.texture_payload_bytes));return true;
    });
    std::erase_if(g_model_targets,[](const auto& target){return !target.root.Get();});
    RefreshModelTargetPriorities(now);
    // Only registered receivers are queried; no scene-wide discovery/scan.
    for (auto& target:g_model_targets) {
        void* asset=target.root.Get();const EnabledMod* mod=nullptr;
        for (const auto& candidate:g_registry.enabled) if (SameAdapter(*candidate.adapter,*target.adapter)) {mod=&candidate;break;}
        if (!mod) {
            target.verified_selection.clear();
            if (g_hot_switch_runtime.load() && !target.desired.empty()) {
                auto permit=g_model_frame_budget.TryBegin(frame,{0,1,true,false});
                if (permit && RestoreDisabledResource(asset,ObjectName(asset),*g_construction)) target.desired.clear();
            }
            continue;
        }
        if (target.verified_selection==mod->selection_key && now<target.next_verify_ms) continue;
        // Synchronous template mode: rebuild only for a new selection (hot
        // switch). Game/camera renderer toggles on a committed receiver must
        // not trigger another decode, so its bindings are not re-verified.
        if (!g_model_clone_coverage.load() && target.desired==mod->selection_key) continue;
        bool owns_record=false;
        for (const auto& record:g_completed) if (record.root.Is(asset) && record.selection_key==mod->selection_key) {owns_record=true;break;}
        // With synchronous template delivery a natural clone shares the
        // committed template's verified assets; it needs no private rebuild.
        const bool natural_clone_complete=!g_model_clone_coverage.load();
        if ((owns_record || natural_clone_complete) && IsCompletedResource(*mod->adapter,asset,mod->selection_key)) {
            target.desired=mod->selection_key;target.verified_selection=mod->selection_key;
            target.next_verify_ms=now+g_model_verify_interval_ms;continue;
        }
        target.verified_selection.clear();
        if (target.failed_selection==mod->selection_key) continue;
        // In synchronous template mode `desired` gates every later rebuild, so
        // it is recorded only once this target owns a Job (world and UI of one
        // role are built one after the other, never silently skipped).
        if (EnqueueResourceBuild(asset,*mod,target.priority)) target.desired=mod->selection_key;
    }
    PumpInstanceRebind(frame);
    // Round robin is shared by every observed role (four team roles plus UI).
    const bool fair=g_model_foreground_streak>=3 && std::any_of(g_model_jobs.begin(),g_model_jobs.end(),[](const auto& job){return job->priority!=LoadPriority::Foreground;});
    std::sort(g_model_jobs.begin(),g_model_jobs.end(),[&](const auto& a,const auto& b){
        if (a->priority!=b->priority) return fair?a->priority<b->priority:a->priority>b->priority;
        return a->served<b->served;
    });
    for (auto& job:g_model_jobs) {
        const auto before=g_model_frame_budget.Snapshot();
        const auto phase=job->phase;
        try {if (!AdvanceModelJob(*job,frame)) job->invalid=true;}
        catch (const std::exception& e) {job->invalid=true;JobFail(*job,std::string("exception: ")+e.what());}
        catch (...) {job->invalid=true;JobFail(*job,"native C++ exception");}
        if (job->phase!=phase && !job->invalid)
            Log("Model Job phase resource="+job->scope.resource_name+" "+ModelJobPhaseName(phase)+"->"+
                ModelJobPhaseName(job->phase)+" frame="+std::to_string(frame));
        job->served=++g_model_service_serial;
        const auto budget=g_model_frame_budget.Snapshot();
        if (budget.frame_id==frame && budget.steps>(before.frame_id==frame?before.steps:0))
            g_model_foreground_streak=job->priority==LoadPriority::Foreground?std::min(g_model_foreground_streak+1,3u):0;
        if (budget.frame_id==frame && (budget.heavy || budget.oversize)) break;
    }
    struct FailedModelJob { std::unique_ptr<StrongReference> asset; std::string resource,reason; };
    std::vector<FailedModelJob> fallbacks;
    std::erase_if(g_model_jobs,[&](auto& job){
        if (job->invalid) {
            void* target_asset=job->target.Get();
            for (auto& target:g_model_targets) if (target.root.Get()==target_asset) target.failed_selection=job->mod.selection_key;
            const auto resource=job->scope.resource_name.empty()&&target_asset?ObjectName(target_asset):job->scope.resource_name;
            Log("Model Job failed resource="+resource+" phase="+ModelJobPhaseName(job->phase)+
                " component="+std::to_string(job->component)+" texture="+std::to_string(job->texture)+
                " reason="+(job->failure.empty()?std::string("unspecified"):job->failure));
            if (job->decode_failed) Log("Model fallback skipped resource="+resource+" reason=package decode refused");
            else if (target_asset && ActiveSelectionKey(*job->mod.adapter)==job->mod.selection_key) {
                FailedModelJob failed{std::make_unique<StrongReference>(),resource,job->failure};
                if (failed.asset->Set(target_asset)) fallbacks.push_back(std::move(failed));
            }
        }
        return job->invalid || job->phase==ModelBuildJob::Phase::Done;
    });
    // Jobs (and their unpublished assets) are destroyed above before fallback.
    for (auto& failed:fallbacks) if (void* asset=failed.asset->Get())
        RunSynchronousModelFallback(asset,failed.resource,"async Job failed: "+failed.reason);
    if ((!g_model_jobs.empty() || !fallbacks.empty()) && now>=g_model_next_status_ms) {
        g_model_next_status_ms=now+5000;
        const auto cpu=g_model_loader->Snapshot();
        size_t pending=0;{std::lock_guard lock(g_model_delivery_mutex);pending=g_model_deliveries.size();}
        std::string phases;
        for (const auto& job:g_model_jobs) phases+=" "+(job->scope.resource_name.empty()?std::string(job->mod.adapter->id):job->scope.resource_name)+
            ":"+ModelJobPhaseName(job->phase);
        Log("Model queue frame="+std::to_string(frame)+" jobs="+std::to_string(g_model_jobs.size())+
            " targets="+std::to_string(g_model_targets.size())+" pendingDeliveries="+std::to_string(pending)+
            " droppedDeliveries="+std::to_string(g_model_deliveries_dropped.load())+
            " cpuPending="+std::to_string(cpu.pending)+" cpuActive="+std::to_string(cpu.active_workers)+
            " decodedLiveBytes="+std::to_string(cpu.live_decoded)+
            " textureDecodeLiveBytes="+std::to_string(g_texture_streamer?g_texture_streamer->Snapshot().live:0)+
            " textureDecodePeakBytes="+std::to_string(g_texture_streamer?g_texture_streamer->Snapshot().peak:0)+
            " clonesRegistered="+std::to_string(g_model_clones_registered.load())+
            " maxFrameUploadBytes="+std::to_string(std::exchange(ConstructionScope::global_max_frame_bytes,0))+phases);
    }
}
