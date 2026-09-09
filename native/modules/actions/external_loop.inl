// Included inside the module namespace after ReadFrame. All Unity operations
// run on the game thread; original assets/controllers are never mutated.
bool g_external_contract = true;
using ExternalNewString = void*(*)(const char*);
using ExternalNewObject = void*(*)(const void*);
ExternalNewString g_external_string = nullptr;
ExternalNewObject g_external_object = nullptr;
const void* g_override_class = nullptr;
struct ExternalAssets {
    bool attempted = false, ready = false;
    void* bundle = nullptr;
    void* clips[2]{};
    uint32_t bundle_root = 0, clip_roots[2]{};
} g_external_assets;
struct ExternalOwner {
    void* component = nullptr;
    void* animator = nullptr;
    void* original = nullptr;
    void* wrapper = nullptr;
    uint32_t component_root = 0, animator_root = 0, original_root = 0, wrapper_root = 0;
    bool pending_restore = false;
} g_external_owner;

void FreeExternalOwner(ExternalOwner& old) {
    for (auto pin : {old.component_root, old.animator_root, old.original_root, old.wrapper_root})
        if (pin) g_host->gchandle_free(g_host->context, pin);
    old = {};
}
void RestoreExternal(bool unity_calls) {
    ExternalOwner old = g_external_owner;
    g_external_owner = {}; // Clear ownership before any reentrant managed call.
    bool can_destroy = false;
    if (unity_calls && old.animator && UnityObjectAlive(old.animator)) {
        bool ok = true;
        void* current = Object(ControllerGet, old.animator, ok);
        // If the game changed controllers, do not overwrite its new decision.
        if (ok && current == old.wrapper && UnityObjectAlive(old.original)) {
            void* args[]{old.original};
            Invoke(ControllerSet, old.animator, args, ok);
            if (!ok && !g_external_owner.component) {
                bool check = true;
                if (Object(ControllerGet, old.animator, check) == old.wrapper && check) {
                    old.pending_restore = true; g_external_owner = old;
                    Log("Aglina v11: controller restore deferred after managed failure; ownership retained.");
                    return;
                }
            }
            can_destroy = ok && Object(ControllerGet, old.animator, ok) == old.original;
            Log(ok ? "Aglina v11: original controller restored." : "Aglina v11: controller restore invocation failed.");
        }
    }
    if (can_destroy && UnityObjectAlive(old.wrapper)) { bool ok = true; void* args[]{old.wrapper}; Invoke(DestroyOwned, nullptr, args, ok); }
    FreeExternalOwner(old);
}
void ExternalRestoreWhenIdle(void* component) {
    if (component != g_external_owner.component || !g_external_owner.pending_restore) return;
    Frame frame; int hash = 0;
    if (!UnityObjectAlive(g_external_owner.animator)) { RestoreExternal(false); return; }
    if (ReadFrame(component, frame, hash) && !frame.current_special && !frame.entering_special)
        RestoreExternal(true);
}
void FreeExternalAssets(bool unload) {
    auto old = g_external_assets;
    g_external_assets = {}; g_external_assets.attempted = true;
    if (unload && old.bundle && UnityObjectAlive(old.bundle)) {
        bool ok = true, all = true; void* args[]{&all}; Invoke(BundleUnload, old.bundle, args, ok);
    }
    for (auto pin : {old.clip_roots[0], old.clip_roots[1], old.bundle_root})
        if (pin) g_host->gchandle_free(g_host->context, pin);
}
void AbandonExternalOwner(ExternalOwner& owner) {
    if (UnityObjectAlive(owner.wrapper)) { bool ok = true; void* args[]{owner.wrapper}; Invoke(DestroyOwned, nullptr, args, ok); }
    FreeExternalOwner(owner);
}
void TraceExternalLoad(const std::filesystem::path& file, const char* message) {
    Log(message);
    // The host logger is buffered. Persist sparse load milestones before native calls,
    // so a Unity access violation cannot erase the last known load stage.
    const auto path = file.parent_path() / L"aglina_native_return_v2.load.log";
    HANDLE handle = CreateFileW(path.c_str(), FILE_APPEND_DATA, FILE_SHARE_READ | FILE_SHARE_WRITE,
        nullptr, OPEN_ALWAYS, FILE_ATTRIBUTE_NORMAL, nullptr);
    if (handle == INVALID_HANDLE_VALUE) return;
    SYSTEMTIME time{}; GetLocalTime(&time);
    char line[768]{};
    std::snprintf(line, sizeof(line), "%04u-%02u-%02u %02u:%02u:%02u %s\r\n",
        time.wYear, time.wMonth, time.wDay, time.wHour, time.wMinute, time.wSecond, message);
    DWORD written = 0; WriteFile(handle, line, static_cast<DWORD>(std::strlen(line)), &written, nullptr);
    FlushFileBuffers(handle); CloseHandle(handle);
}
void* LoadNativeExternalClip(int side, void* type, bool& ok) {
    if (side < 0 || side > 1 || !type) { ok = false; return nullptr; }
    // Existing resource lookup IDs copied from the original native hash container.
    int64_t asset_hash = side == 0 ? 0x0EC405CB4A5FF5ECll : 0x00AB35F9779D7048ll;
    void* args[]{&asset_hash, type};
    return Invoke(BundleAsset, g_external_assets.bundle, args, ok);
}
bool LoadExternalAssets() {
    if (g_external_assets.attempted) return g_external_assets.ready;
    g_external_assets.attempted = true;
    if (!g_external_contract) { Log("Aglina v11: optional external API contract unavailable; using v9 loop."); return false; }
    wchar_t module_path[32768]{}; HMODULE module = nullptr;
    if (!GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS | GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
        reinterpret_cast<LPCWSTR>(&g_external_assets), &module) ||
        !GetModuleFileNameW(module, module_path, static_cast<DWORD>(std::size(module_path)))) {
        Log("Aglina v11: module resource path unavailable; using v9 loop."); return false;
    }
    const auto file = std::filesystem::path(module_path).parent_path() / L"actions" / L"aglina_native_return_v2.bundle";
    if (GetFileAttributesW(file.c_str()) == INVALID_FILE_ATTRIBUTES) {
        Log("Aglina v11: external bundle absent; using v9 loop."); return false;
    }
    auto game = GetModuleHandleW(L"GameAssembly.dll");
    g_external_string = reinterpret_cast<ExternalNewString>(GetProcAddress(game, "il2cpp_string_new"));
    g_external_object = reinterpret_cast<ExternalNewObject>(GetProcAddress(game, "il2cpp_object_new"));
    using ClassType = const void*(*)(const void*); using TypeObject = void*(*)(const void*);
    auto class_type = reinterpret_cast<ClassType>(GetProcAddress(game, "il2cpp_class_get_type"));
    auto type_object = reinterpret_cast<TypeObject>(GetProcAddress(game, "il2cpp_type_get_object"));
    BE_ResolvedClassV1 clip_class{}, override_class{};
    if (!g_external_string || !g_external_object || !class_type || !type_object ||
        g_host->resolve_class(g_host->context, kUnity, "UnityEngine", "AnimationClip", &clip_class) != BE_Result_Ok ||
        g_host->resolve_class(g_host->context, kUnity, "UnityEngine", "AnimatorOverrideController", &override_class) != BE_Result_Ok) {
        Log("Aglina v11: IL2CPP exports/classes unavailable; using v9 loop."); return false;
    }
    g_override_class = override_class.class_info;
    void* type = type_object(class_type(clip_class.class_info));
    int size = WideCharToMultiByte(CP_UTF8, 0, file.c_str(), -1, nullptr, 0, nullptr, nullptr);
    if (!type || size <= 0) { Log("Aglina v11: clip type/path conversion failed; using v9 loop."); return false; }
    std::string path(static_cast<size_t>(size), '\0');
    WideCharToMultiByte(CP_UTF8, 0, file.c_str(), -1, path.data(), size, nullptr, nullptr);
    void* managed_path = g_external_string(path.c_str());
    bool ok = managed_path != nullptr;
    void* args[]{managed_path};
    TraceExternalLoad(file, "Aglina v11: LoadFromFile begin, native-schema ACL bundle.");
    g_external_assets.bundle = Invoke(BundleLoad, nullptr, args, ok);
    const char* failed_stage = !ok ? "LoadFromFile managed invocation/path allocation" : "LoadFromFile returned null/dead bundle (see Player.log)";
    if (ok && UnityObjectAlive(g_external_assets.bundle)) {
        TraceExternalLoad(file, "Aglina v11: archive accepted by LoadFromFile; loading native AnimationClip assets.");
        g_external_assets.bundle_root = g_host->gchandle_new(g_host->context, g_external_assets.bundle, 1);
        ok = g_external_assets.bundle_root != 0;
        failed_stage = "bundle GC pin";
        for (int side = 0; side < 2 && ok; ++side) {
            failed_stage = side == 0 ? "left LoadAsset" : "right LoadAsset";
            TraceExternalLoad(file, side == 0 ? "Aglina v11: left LoadAsset(Int64, Type) begin." : "Aglina v11: right LoadAsset(Int64, Type) begin.");
            auto clip = LoadNativeExternalClip(side, type, ok);
            TraceExternalLoad(file, side == 0 ? "Aglina v11: left LoadAsset returned." : "Aglina v11: right LoadAsset returned.");
            g_external_assets.clips[side] = clip;
            if (ok && UnityObjectAlive(clip)) {
                failed_stage = side == 0 ? "left AnimationClip properties/validation" : "right AnimationClip properties/validation";
                const float length = Value<float>(ClipLength, clip, ok);
                const bool human = Value<bool>(ClipHuman, clip, ok);
                void* clip_name = Object(ClipName, clip, ok);
                char actual_name[160]{}, detail[384]{};
                if (clip_name) g_host->copy_managed_string(g_host->context, clip_name, actual_name, sizeof(actual_name));
                std::snprintf(detail, sizeof(detail), "Aglina v11: %s clip name=%s length=%.6f human=%d properties_ok=%d",
                    side == 0 ? "left" : "right", actual_name, length, human, ok);
                TraceExternalLoad(file, detail);
                ok = ok && human && std::isfinite(length) && std::fabs(length - 208.0f / 60.0f) < .02f &&
                    StringEquals(clip_name, side == 0 ? "BE_Aglina_Return_L" : "BE_Aglina_Return_R");
                if (ok) {
                    failed_stage = side == 0 ? "left clip GC pin" : "right clip GC pin";
                    g_external_assets.clip_roots[side] = g_host->gchandle_new(g_host->context, clip, 1); ok = g_external_assets.clip_roots[side] != 0;
                }
            } else ok = false;
        }
    } else ok = false;
    if (!ok) {
        char detail[256]{};
        std::snprintf(detail, sizeof(detail), "Aglina v11: external load failed at %s; using v9 loop.", failed_stage);
        TraceExternalLoad(file, detail);
        FreeExternalAssets(true);
        return false;
    }
    g_external_assets.ready = true;
    TraceExternalLoad(file, "Aglina v11: native Humanoid clips loaded: BE_Aglina_Return_L/R, length=3.466667.");
    return true;
}
bool InstallExternal(void* component) {
    if (!LoadExternalAssets()) return false;
    if (g_external_owner.component) {
        bool ok = true;
        if (component == g_external_owner.component &&
            Object(ControllerGet, g_external_owner.animator, ok) == g_external_owner.wrapper && ok) {
            g_external_owner.pending_restore = false; return true;
        }
        RestoreExternal(true);
    }
    bool ok = true;
    auto animator = Object(GetAnimator, component, ok);
    auto original = Object(ControllerGet, animator, ok);
    auto clips = Object(ControllerClips, original, ok);
    const int count = Value<int>(ArrayLength, clips, ok);
    void* originals[2]{};
    for (int i = 0; ok && i < count && i < 4096; ++i) {
        void* args[]{&i}; auto clip = Invoke(ArrayItem, clips, args, ok);
        auto name = Object(ClipName, clip, ok);
        if (StringEquals(name, "A_actor_aglina_sprint_dash_sp_l")) originals[0] = clip;
        if (StringEquals(name, "A_actor_aglina_sprint_dash_sp_r")) originals[1] = clip;
    }
    if (!ok || count < 2 || count > 4096 || !originals[0] || !originals[1]) {
        Log("Aglina v11: original dash clips unavailable in this controller; using v9 loop."); return false;
    }
    ExternalOwner owner;
    owner.component = component; owner.animator = animator; owner.original = original;
    owner.wrapper = g_external_object(g_override_class);
    if (!owner.wrapper) return false;
    owner.wrapper_root = g_host->gchandle_new(g_host->context, owner.wrapper, 1);
    owner.original_root = g_host->gchandle_new(g_host->context, original, 1);
    owner.component_root = g_host->gchandle_new(g_host->context, component, 1);
    owner.animator_root = g_host->gchandle_new(g_host->context, animator, 1);
    if (!owner.wrapper_root || !owner.original_root || !owner.component_root || !owner.animator_root) { AbandonExternalOwner(owner); return false; }
    void* ctor_args[]{original}; Invoke(OverrideCtor, owner.wrapper, ctor_args, ok);
    bool notify = true;
    for (int side = 0; side < 2; ++side) {
        void* args[]{originals[side], g_external_assets.clips[side], &notify};
        Invoke(OverrideClip, owner.wrapper, args, ok);
    }
    if (!ok) { AbandonExternalOwner(owner); Log("Aglina v11: private override setup failed; using v9 loop."); return false; }
    g_external_owner = owner;
    void* bind_args[]{owner.wrapper}; Invoke(ControllerSet, animator, bind_args, ok);
    if (!ok) { RestoreExternal(true); return false; }
    Log("Aglina v11: private return controller installed; awaiting actual clip confirmation.");
    return true;
}
bool ExternalControllerOwned(void* component) {
    if (component != g_external_owner.component) return false;
    bool ok = true;
    return Object(ControllerGet, g_external_owner.animator, ok) == g_external_owner.wrapper && ok;
}
bool ExternalClipPlaying(void* component, int hash) {
    if (component != g_external_owner.component) return false;
    bool ok = true; int layer = 0; void* args[]{&layer};
    auto infos = Invoke(CurrentClipInfos, g_external_owner.animator, args, ok);
    int count = Value<int>(ArrayLength, infos, ok);
    const char* expected = hash == g_hashes[0] ? "BE_Aglina_Return_L" : "BE_Aglina_Return_R";
    for (int i = 0; ok && i < count && i < 8; ++i) {
        void* at[]{&i}; auto boxed = Invoke(ArrayItem, infos, at, ok);
        void* value = boxed ? g_host->object_unbox(g_host->context, boxed) : nullptr;
        auto clip = Object(InfoClip, value, ok);
        auto name = Object(ClipName, clip, ok);
        if (ok && StringEquals(name, expected)) return true;
    }
    return false;
}
