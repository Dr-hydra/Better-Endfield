// Observe game-owned delivery and assembly. Never retain or replace assets.
BE_ResolvedMethodV1 g_delivery_finish{}, g_delivery_hash{}, g_delivery_uid{};
BE_ResolvedMethodV1 g_delivery_status{}, g_delivery_clone{}, g_delivery_sms{};
BE_ResolvedMethodV1 g_delivery_instantiate{};
BE_ResolvedMethodV1 g_delivery_part_model{}, g_delivery_part_post{}, g_delivery_complex_post{};
using DeliveryPartFn = void(__fastcall*)(void*, void*);
DeliveryPartFn g_delivery_original_part_post = nullptr;
DeliveryPartFn g_delivery_original_complex_post = nullptr;
using DeliveryInstantiateFn = void*(__fastcall*)(void*, void*, void*, void*, void*);
DeliveryInstantiateFn g_delivery_original_instantiate = nullptr;
std::atomic_uint32_t g_delivery_sms_calls{0};
using DeliveryFinishFn = void(__fastcall*)(void*, void*, void*);
using DeliveryCloneFn = void*(__fastcall*)(void*, void*);
using DeliverySmsFn = void(__fastcall*)(void*, int32_t, void*, void*, void*,
    void*, void**, void**, bool, void*);
DeliveryFinishFn g_delivery_original_finish = nullptr;
DeliveryCloneFn g_delivery_original_clone = nullptr;
DeliverySmsFn g_delivery_original_sms = nullptr;
std::atomic_uint64_t g_delivery_sequence{0};
std::atomic_uint32_t g_delivery_events{0};
std::atomic_bool g_delivery_enabled{false};
thread_local bool g_delivery_logging = false;

struct DeliveryLogGuard {
    DeliveryLogGuard() { g_delivery_logging = true; }
    ~DeliveryLogGuard() { g_delivery_logging = false; }
};

template<typename T>
bool DeliveryValue(const BE_ResolvedMethodV1& method, void* object, T& value) {
    if (!object || !method.method_info || !g_host) return false;
    void* exception = nullptr;
    void* boxed = g_host->runtime_invoke(g_host->context, method.method_info,
        object, nullptr, &exception);
    return !exception && Unbox(boxed, value);
}

bool DeliveryTargetName(const std::string& name) {
    return name.find("endminf") != std::string::npos ||
        name.find("aglina") != std::string::npos;
}

bool DeliveryBudget() {
    const auto count = g_delivery_events.fetch_add(1);
    if (count == 512) Log("[ASSET-DELIVERY] event limit reached; restart for a new capture.");
    return count < 512;
}

std::string DeliveryStamp(uint64_t sequence) {
    return " seq=" + std::to_string(sequence) +
        " tick=" + std::to_string(GetTickCount64()) +
        " tid=" + std::to_string(GetCurrentThreadId());
}

std::string DeliveryObject(void* object) {
    int32_t id = -1;
    const bool have_id = F12DumpInstanceId(object, id);
    return NaturalTracePointer(object) + " id=" +
        (have_id ? std::to_string(id) : "unavailable");
}

void DeliveryMeshLine(const char* stage, uint64_t sequence, int slot, void* renderer) {
    void* mesh = Invoke(Contract("skinned.get_shared_mesh"), renderer, nullptr, false);
    const int bones = ArrayLength(Invoke(Contract("skinned.get_bones"), renderer, nullptr, false));
    Log(std::string("[ASSET-DELIVERY] ") + stage + DeliveryStamp(sequence) +
        " slot=" + std::to_string(slot) + " renderer=" + DeliveryObject(renderer) +
        " mesh=" + DeliveryObject(mesh) + " meshName=\"" + ObjectName(mesh) +
        "\" bones=" + std::to_string(bones));
}

void __fastcall DeliveryFinishHook(void* proxy, void* asset, void* method) {
    // Only managed scalar getters on this path: asset loading may run off-thread.
    bool trace = false;
    uint64_t hash = 0, sequence = 0;
    uint32_t uid = 0;
    if (g_delivery_enabled.load() && !g_delivery_logging) {
        DeliveryLogGuard guard;
        trace = DeliveryValue(g_delivery_hash, proxy, hash) &&
            (hash == kPreheatEndminPostmodelHash || hash == kPreheatAglinaPostmodelHash);
        if (trace) trace = DeliveryBudget();
        if (trace) {
            sequence = ++g_delivery_sequence;
            const bool have_uid = DeliveryValue(g_delivery_uid, proxy, uid);
            Log("[ASSET-DELIVERY] FINISH-BEGIN" + DeliveryStamp(sequence) +
                " hash=" + NaturalTraceHash(hash) + " proxy=" + NaturalTracePointer(proxy) +
                " uid=" + (have_uid ? std::to_string(uid) : "unavailable") +
                " asset=" + NaturalTracePointer(asset));
        }
    }
    g_delivery_original_finish(proxy, asset, method);
    // Dynamic diagnostic build: leave initial assets intact; F12 owns replacement.
    if (trace) {
        DeliveryLogGuard guard;
        int32_t status = -1;
        const bool have_status = DeliveryValue(g_delivery_status, proxy, status);
        Log("[ASSET-DELIVERY] FINISH-END" + DeliveryStamp(sequence) +
            " hash=" + NaturalTraceHash(hash) + " proxy=" + NaturalTracePointer(proxy) +
            " asset=" + NaturalTracePointer(asset) + " status=" +
            (have_status ? std::to_string(status) : "unavailable"));
    }
}

void* DeliveryObserveClone(void* original, void* clone, const char* stage) {
    if (!g_delivery_enabled.load() || g_delivery_logging || !original) return clone;
    DeliveryLogGuard guard;
    if (!DeliveryTargetName(ObjectName(original)) || !DeliveryBudget()) return clone;
    const auto sequence = ++g_delivery_sequence;
    Log(std::string("[ASSET-DELIVERY] ") + stage + DeliveryStamp(sequence) +
        " original=" + DeliveryObject(original) + " clone=" + DeliveryObject(clone) +
        " name=\"" + ObjectName(original) + "\"");
    // Inspect only this target's children; no global renderer scan here.
    if (!clone || NaturalTraceObjectTypeName(clone) != "UnityEngine.GameObject") return clone;
    bool inactive = true;
    void* parameters[2]{g_skinned_renderer_class.type_object, &inactive};
    void* exception = nullptr;
    void* renderers = g_host->runtime_invoke(g_host->context,
        g_natural_trace_get_components.method_info, clone, parameters, &exception);
    const int count = exception ? 0 : ArrayLength(renderers);
    for (int index = 0; index < count && index < 96; ++index) {
        void* renderer = ArrayValue(renderers, index);
        if (renderer) DeliveryMeshLine("CLONE-MESH", sequence, index, renderer);
    }
    return clone;
}

void* __fastcall DeliveryCloneHook(void* original, void* method) {
    return DeliveryObserveClone(original, g_delivery_original_clone(original, method), "CLONE-SINGLE");
}

// Windows x64 passes these 12/16-byte value arguments by reference. Confirmed
// at RVA 03BD3740; the original forwards directly to the engine icall.
void* __fastcall DeliveryInstantiateHook(void* original, void* position,
    void* rotation, void* parent, void* method) {
    return DeliveryObserveClone(original,
        g_delivery_original_instantiate(original, position, rotation, parent, method),
        "INSTANTIATE-POS-PARENT");
}

void DeliveryPartSnapshot(void* part, const char* stage) {
    if (!g_delivery_enabled.load() || g_delivery_logging) return;
    DeliveryLogGuard guard;
    void* exception = nullptr;
    void* model = g_host->runtime_invoke(g_host->context,
        g_delivery_part_model.method_info, part, nullptr, &exception);
    if (exception || !model || !DeliveryTargetName(ObjectName(model)) || !DeliveryBudget()) return;
    const auto sequence = ++g_delivery_sequence;
    Log(std::string("[ASSET-DELIVERY] ") + stage + DeliveryStamp(sequence) +
        " part=" + NaturalTracePointer(part) + " model=" + DeliveryObject(model) +
        " name=\"" + ObjectName(model) + "\"");
    bool inactive = true;
    void* parameters[2]{g_skinned_renderer_class.type_object, &inactive};
    void* renderers = g_host->runtime_invoke(g_host->context,
        g_natural_trace_get_components.method_info, model, parameters, &exception);
    const int count = exception ? 0 : ArrayLength(renderers);
    for (int i = 0; i < count && i < 96; ++i) {
        void* renderer = ArrayValue(renderers, i);
        if (renderer) DeliveryMeshLine(stage, sequence, i, renderer);
    }
}

void __fastcall DeliveryPartPostHook(void* part, void* method) {
    DeliveryPartSnapshot(part, "PART-BASE-BEGIN");
    g_delivery_original_part_post(part, method);
    DeliveryPartSnapshot(part, "PART-BASE-END");
}

void __fastcall DeliveryComplexPostHook(void* part, void* method) {
    DeliveryPartSnapshot(part, "PART-COMPLEX-BEGIN");
    g_delivery_original_complex_post(part, method);
    DeliveryPartSnapshot(part, "PART-COMPLEX-END");
}

void __fastcall DeliverySmsHook(void* config, int32_t lod, void* pool,
    void* parent, void* renders, void* materials, void** smrs, void** roots,
    bool lod3_as_lod2, void* method) {
    const bool probe = g_delivery_enabled.load() && !g_delivery_logging;
    const auto call = probe ? g_delivery_sms_calls.fetch_add(1) + 1 : 0;
    if (call && call <= 8) {
        Log("[ASSET-DELIVERY] SMS-ENTER call=" + std::to_string(call) +
            " lod=" + std::to_string(lod) + " config=" + NaturalTracePointer(config));
    }
    g_delivery_original_sms(config, lod, pool, parent, renders, materials,
        smrs, roots, lod3_as_lod2, method);
    if (!g_delivery_enabled.load() || g_delivery_logging) return;
    DeliveryLogGuard guard;
    const std::string path = parent ? BuildTransformPath(parent) : "";
    const int count = smrs && *smrs ? ArrayLength(*smrs) : 0;
    bool target = DeliveryTargetName(path);
    for (int index = 0; !target && index < count && index < 96; ++index)
        target = DeliveryTargetName(ObjectName(ArrayValue(*smrs, index)));
    if (call && call <= 8) {
        Log("[ASSET-DELIVERY] SMS-EXIT call=" + std::to_string(call) +
            " target=" + (target ? "true" : "false") + " slots=" + std::to_string(count) +
            " path=\"" + path + "\"");
    }
    if (!target || !DeliveryBudget()) return;
    const auto sequence = ++g_delivery_sequence;
    Log("[ASSET-DELIVERY] ASSEMBLY-LOD" + DeliveryStamp(sequence) +
        " config=" + NaturalTracePointer(config) + " parent=" + DeliveryObject(parent) +
        " lod=" + std::to_string(lod) + " slots=" + std::to_string(count) +
        " path=\"" + path + "\"");
    for (int index = 0; index < count && index < 96; ++index) {
        void* renderer = ArrayValue(*smrs, index);
        if (!renderer) {
            Log("[ASSET-DELIVERY] ASSEMBLY-NULL" + DeliveryStamp(sequence) +
                " slot=" + std::to_string(index));
            continue;
        }
        DeliveryMeshLine("ASSEMBLY-MESH", sequence, index, renderer);
    }
}

BE_Result BE_CALL InitializeWithAssetDeliveryTrace(const BE_HostApiV1* host) {
    if (!host) return BE_Result_InvalidArgument;
    // Resolve before installing any hooks: the host unloads a rejected module.
    g_host = host;
    g_natural_trace_real_host = host;
    bool ready = true;
    ready &= NaturalTraceResolveMethod(g_early_main_thread, "UnityEngine.CoreModule.dll", "UnityEngine",
        "Object", "CurrentThreadIsMainThread", nullptr, "System.Boolean", 0, "Object.CurrentThreadIsMainThread");
    ready &= NaturalTraceResolveMethod(g_early_material_ctor, "UnityEngine.CoreModule.dll", "UnityEngine",
        "Material", ".ctor", "UnityEngine.Material", "System.Void", 1, "Material.copy ctor");
    ready &= NaturalTraceResolveMethod(g_early_set_material, "UnityEngine.CoreModule.dll", "UnityEngine",
        "Renderer", "set_sharedMaterials", "UnityEngine.Material[]", "System.Void", 1, "Renderer.set_sharedMaterials");
    ready &= NaturalTraceResolveMethod(g_early_array_set, "mscorlib.dll", "System",
        "Array", "SetValue", "System.Object|System.Int32", "System.Void", 2, "Array.SetValue(object,index)");
    const char* ns = "Beyond.Resource.Runtime";
    ready &= NaturalTraceResolveMethod(g_delivery_finish, "Common.Beyond.dll", ns,
        "BundleLoader.AssetProxy", "_FinishWithAsset", "UnityEngine.Object", "System.Void", 1, "AssetProxy._FinishWithAsset");
    ready &= NaturalTraceResolveMethod(g_delivery_hash, "Common.Beyond.dll", ns,
        "BundleLoader.AssetProxy", "get_pathOrNameHash", nullptr, "System.Int64", 0, "AssetProxy.pathOrNameHash");
    ready &= NaturalTraceResolveMethod(g_delivery_uid, "Common.Beyond.dll", ns,
        "BundleLoader.BundleAssetProxy", "get_instanceUid", nullptr, "System.UInt32", 0, "BundleAssetProxy.instanceUid");
    ready &= NaturalTraceResolveMethod(g_delivery_status, "Common.Beyond.dll", ns,
        "BundleLoader.BundleAssetProxy", "get_status", nullptr, "Beyond.Resource.Runtime.BundleLoader.ELoadStatus", 0, "BundleAssetProxy.status");
    ready &= NaturalTraceResolveMethod(g_delivery_clone, "UnityEngine.CoreModule.dll", "UnityEngine",
        "Object", "Internal_CloneSingle", "UnityEngine.Object", "UnityEngine.Object", 1, "Object.Internal_CloneSingle");
    ready &= NaturalTraceResolveMethod(g_delivery_instantiate, "UnityEngine.CoreModule.dll", "UnityEngine",
        "Object", "Instantiate", "UnityEngine.Object|UnityEngine.Vector3|UnityEngine.Quaternion|UnityEngine.Transform",
        "UnityEngine.Object", 4, "Object.Instantiate(position,rotation,parent)");
    ready &= NaturalTraceResolveMethod(g_delivery_sms, "Gameplay.Beyond.dll", "Beyond.NPC.Avatar",
        "NPCAvatarCreatorUtils", "CreateSMSInfoForPostModel",
        "Beyond.NPC.Avatar.INPCMeshAssets|Beyond.NPC.Lod.ELODLevel|Beyond.NPC.NPCGoPool|UnityEngine.Transform|System.Collections.Generic.List<System.String>|System.Collections.Generic.List<System.Int32>|UnityEngine.SkinnedMeshRenderer[]&|Beyond.NPC.Avatar.RootBoneInfo[]&|System.Boolean",
        "System.Void", 9, "NPCAvatarCreatorUtils.CreateSMSInfoForPostModel");
    ready &= NaturalTraceResolveMethod(g_delivery_part_model, "Gameplay.Beyond.dll", "Beyond.Gameplay.View",
        "BaseModelViewPart", "get_model", nullptr, "UnityEngine.GameObject", 0, "BaseModelViewPart.model");
    ready &= NaturalTraceResolveMethod(g_delivery_part_post, "Gameplay.Beyond.dll", "Beyond.Gameplay.View",
        "BaseModelViewPart", "PostDealLoadedModel", nullptr, "System.Void", 0, "BaseModelViewPart.PostDealLoadedModel");
    ready &= NaturalTraceResolveMethod(g_delivery_complex_post, "Gameplay.Beyond.dll", "Beyond.Gameplay.View",
        "ComplexModelViewPart", "PostDealLoadedModel", nullptr, "System.Void", 0, "ComplexModelViewPart.PostDealLoadedModel");
    if (!ready) return BE_Result_Failed;
    const auto result = InitializeWithPreheatControlTrace(host);
    if (result != BE_Result_Ok) return result;
    g_delivery_sequence.store(0);
    g_delivery_events.store(0);
    g_delivery_sms_calls.store(0);
    const auto install = [&](const BE_ResolvedMethodV1& target, void* detour, void** original) {
        return host->create_hook(host->context, kModuleId, target.method_pointer,
            detour, original) == BE_Result_Ok && *original;
    };
    if (!install(g_delivery_finish, reinterpret_cast<void*>(&DeliveryFinishHook), reinterpret_cast<void**>(&g_delivery_original_finish)) ||
        !install(g_delivery_clone, reinterpret_cast<void*>(&DeliveryCloneHook), reinterpret_cast<void**>(&g_delivery_original_clone)) ||
        !install(g_delivery_sms, reinterpret_cast<void*>(&DeliverySmsHook), reinterpret_cast<void**>(&g_delivery_original_sms)) ||
        !install(g_delivery_instantiate, reinterpret_cast<void*>(&DeliveryInstantiateHook), reinterpret_cast<void**>(&g_delivery_original_instantiate)) ||
        !install(g_delivery_part_post, reinterpret_cast<void*>(&DeliveryPartPostHook), reinterpret_cast<void**>(&g_delivery_original_part_post)) ||
        !install(g_delivery_complex_post, reinterpret_cast<void*>(&DeliveryComplexPostHook), reinterpret_cast<void**>(&g_delivery_original_complex_post))) {
        // Earlier layers and any partial delivery hooks remain live. Keep this
        // DLL resident; disabled delivery detours only forward to originals.
        Log("[ASSET-DELIVERY] hook install failed; delivery trace disabled, module kept loaded for existing hooks.");
        return BE_Result_Ok;
    }
    g_delivery_enabled.store(true);
    MeshTraceInitialize(host);
    Log("[ASSET-DELIVERY] armed v0.0.43: F12 replace/refresh, F11 restore, 0 snapshot, 9 C5 field A/B. Early replacement disabled.");
    return BE_Result_Ok;
}

void BE_CALL ShutdownWithAssetDeliveryTrace() {
    g_mesh_trace_enabled.store(false);
    g_delivery_enabled.store(false);
    EarlyDeliveryShutdown();
    ShutdownWithPreheatControlTrace();
}
