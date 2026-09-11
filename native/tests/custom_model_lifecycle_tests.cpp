// Exercise the production lifecycle through a fake Host/Unity boundary. No game
// process, native graphics buffers or asset files are needed.
#include "../modules/custom_model/module.cpp"
#include <cstdlib>
#include <iostream>
#include <unordered_map>
#include <unordered_set>
#include <memory>

using namespace BetterEndfield::CustomModelModule;

namespace {
struct Object {
    std::string name;
    void* mesh = nullptr;
    uint32_t indices = 0;
    int submeshes = 1;
    bool enabled = true;
    bool reject_set = false;
};
std::vector<void*> renderers;
std::unordered_map<void*, std::unique_ptr<Object>> objects;
std::unordered_set<void*> destroyed;
std::unordered_map<uint32_t, void*> handles;
uint32_t next_handle = 1;
bool fail_snapshot = false;
int palette_count = 156;
uint64_t boxed = 0;
void* installed_hook = nullptr;
std::vector<std::string> logs;

void Check(bool ok, const char* reason) {
    if (!ok) { std::cerr << "FAIL: " << reason << '\n'; std::exit(1); }
}
void* Make(std::string name, uint32_t indices = 0, void* mesh = nullptr) {
    auto object = std::make_unique<Object>();
    object->name = std::move(name);
    object->indices = indices;
    object->mesh = mesh;
    void* key = object.get();
    objects.emplace(key, std::move(object));
    return key;
}
Object& Obj(void* object) { return *objects.at(object); }
void* Box(uint64_t value) { boxed = value; return &boxed; }
void* BE_CALL InvokeFake(void*, const void* method, void* instance,
    void** args, void** exception) {
    const std::string_view key = static_cast<const MethodContract*>(method)->key;
    if (key == "resources.find_objects_of_type_all") {
        if (fail_snapshot) { *exception = &fail_snapshot; return nullptr; }
        return &renderers;
    }
    if (key == "array.get_length") {
        return Box(instance == &renderers ? renderers.size() : palette_count);
    }
    if (key == "array.get_value") return renderers.at(*static_cast<int*>(args[0]));
    if (key == "object.get_name") return &Obj(instance).name;
    if (key == "component.get_transform") return instance;
    if (key == "component.get_game_object") return instance;
    if (key == "game_object.get_active_in_hierarchy") return Box(1);
    if (key == "game_object.get_scene") return Box(1);
    if (key == "time.get_frame_count") return Box(1);
    if (key == "object.is_alive") return Box(1);
    if (key == "transform.get_parent") return nullptr;
    if (key == "skinned.get_shared_mesh") return Obj(instance).mesh;
    if (key == "skinned.set_shared_mesh") {
        if (Obj(instance).reject_set) { *exception = instance; return nullptr; }
        Obj(instance).mesh = args[0]; return nullptr;
    }
    if (key == "skinned.get_bones" || key == "mesh.get_bindposes") return &palette_count;
    if (key == "mesh.get_sub_mesh_count") return Box(Obj(instance).submeshes);
    if (key == "mesh.get_index_count") return Box(Obj(instance).indices);
    if (key == "renderer.get_enabled") return Box(Obj(instance).enabled);
    if (key == "renderer.set_enabled") {
        Obj(instance).enabled = *static_cast<bool*>(args[0]); return nullptr;
    }
    if (key == "object.destroy") {
        for (void* renderer : renderers) {
            Check(Obj(renderer).mesh != args[0], "destroyed a Mesh still referenced by a renderer");
        }
        Check(destroyed.insert(args[0]).second, "destroyed a Mesh twice");
        return nullptr;
    }
    std::cerr << "Unexpected runtime call: " << key << '\n';
    std::exit(1);
}
void BE_CALL LogFake(void*, const char*, const char* message) { logs.emplace_back(message); }
void* BE_CALL UnboxFake(void*, void* value) { return value; }
int BE_CALL StringFake(void*, const void* value, char* output, size_t capacity) {
    const auto& text = *static_cast<const std::string*>(value);
    const auto size = std::min(capacity - 1, text.size());
    std::memcpy(output, text.data(), size); output[size] = 0;
    return static_cast<int>(size);
}
uint32_t BE_CALL PinFake(void*, void* object, int) {
    const auto id = next_handle++;
    handles[id] = object; return id;
}
void BE_CALL FreeFake(void*, uint32_t id) {
    Check(handles.erase(id) == 1, "GC handle double free");
}
BE_Result BE_CALL HookFake(void*, const char*, void* target, void*, void** original) {
    installed_hook = target; *original = nullptr; return BE_Result_Ok;
}
BE_HostApiV1 host{};
void Reset() {
    if (g_host) ClearReplacementState();
    Check(handles.empty(), "leaked GC handles");
    renderers.clear(); objects.clear(); destroyed.clear(); logs.clear();
    fail_snapshot = false; installed_hook = nullptr;
    g_mask_visible = false; g_next_refresh_tick = 0;
    g_operation_in_progress = false; g_pump_hits = 0;
    host.runtime_invoke = InvokeFake; host.object_unbox = UnboxFake;
    host.copy_managed_string = StringFake; host.log = LogFake;
    host.gchandle_new = PinFake; host.gchandle_free = FreeFake;
    host.create_hook = HookFake;
    g_host = &host;
    for (auto& method : g_methods) {
        method.resolved = true; method.method_info = &method; method.pointer = &method;
    }
}
ReplacedComponent Record(void* renderer, void* original, void* custom, bool hidden = false) {
    ReplacedComponent record;
    record.component_id = 9; record.original_index_count = 101994;
    record.renderer = renderer; record.original_mesh = original;
    record.custom_mesh = custom; record.hidden_by_mod = hidden;
    record.original_enabled = true;
    PinReplacement(record); g_replaced.push_back(record);
    g_replacement_active = true;
    return record;
}
void FullScanAndCanvasRefresh() {
    Reset();
    auto original = Make("original", 101994);
    auto custom = Make("custom", 59073);
    auto prefab = Make("S_actor_endminf_cloth_01_lod0", 0, custom);
    renderers.push_back(prefab); Record(prefab, original, custom);
    // The UI instance occurs after the old hard limit. It starts with an
    // original mesh even though its material would already be shared/patched.
    for (int i = 0; i < 4500; ++i) renderers.push_back(Make("background"));
    auto ui = Make("S_actor_endminf_cloth_01_lod0", 0, original);
    renderers.push_back(ui);
    Check(EnumerateLiveEndminfRenderers().size() == 2, "discovery truncated at 4096");
    Check(InstallPumpHook(), "no main-thread pump installed");
    Check(installed_hook == Contract("pump.canvas_will_render")->pointer,
        "UI pump not preferred over world camera");
    // No CameraMono calls; the UI event alone must discover and replace it.
    DetourPump(nullptr);
    Check(Obj(ui).mesh == custom, "details mesh not replaced by UI event");
    Check(g_replaced.size() == 2, "UI record missing");
    Check(RollbackReplacement(), "full scan rollback failed");
    Check(Obj(ui).mesh == original && Obj(prefab).mesh == original, "tail renderer not restored");
    Check(destroyed.size() == 1 && handles.empty(), "mesh or handles leaked");
}
void InheritedClone() {
    Reset();
    auto original = Make("original", 101994);
    auto custom = Make("custom", 59073);
    auto prefab = Make("S_actor_endminf_cloth_01_lod0", 0, custom);
    auto clone = Make("S_actor_endminf_cloth_01_lod0", 0, custom);
    renderers = {prefab, clone}; Record(prefab, original, custom, true);
    Obj(prefab).enabled = Obj(clone).enabled = false;
    RefreshActiveReplacement(EnumerateLiveEndminfRenderers());
    Check(g_replaced.size() == 2, "inherited custom Mesh not adopted");
    Check(g_replaced.back().original_mesh == original, "custom Mesh captured as rollback original");
    Check(g_replaced.back().original_enabled, "inherited hidden state became rollback default");
    Check(handles.size() == 6, "clone does not own distinct object handles");
    Check(RollbackReplacement(), "inherited clone rollback failed");
    Check(Obj(clone).enabled && Obj(clone).mesh == original, "clone not fully restored");
}
void RollbackBeforeRefresh() {
    Reset();
    auto original = Make("original", 101994);
    auto custom = Make("custom", 59073);
    auto prefab = Make("S_actor_endminf_cloth_01_lod0", 0, custom);
    // An inherited clone has not yet been reconciled and has a different name.
    auto clone = Make("renamed detail draw", 0, custom);
    auto null_mesh = Make("renderer still loading");
    renderers = {prefab, clone, null_mesh}; Record(prefab, original, custom);
    Check(RollbackReplacement(), "rollback failed on untracked inherited clone");
    Check(Obj(clone).mesh == original, "untracked clone lost its Mesh on rollback");
}
void FailureRetainsOwnership() {
    Reset();
    auto original = Make("original", 101994);
    auto custom = Make("custom", 59073);
    auto renderer = Make("S_actor_endminf_cloth_01_lod0", 0, custom);
    renderers = {renderer}; Record(renderer, original, custom);
    fail_snapshot = true;
    Check(!RollbackReplacement(), "incomplete snapshot permitted destruction");
    Check(g_replacement_active && destroyed.empty() && handles.size() == 3,
        "snapshot failure discarded live ownership");
    fail_snapshot = false; Obj(renderer).reject_set = true;
    Check(!RollbackReplacement(), "rejected restoration reported success");
    Check(g_replacement_active && destroyed.empty() && handles.size() == 3,
        "failed restore discarded live ownership");
    Obj(renderer).reject_set = false;
    Check(RollbackReplacement() && handles.empty(), "rollback could not retry");
}
void RepeatedWriteBackAndMask() {
    Reset();
    auto original = Make("original", 101994);
    auto custom = Make("custom", 59073);
    auto renderer = Make("S_actor_endminf_cloth_01_lod0", 0, custom);
    renderers = {renderer}; Record(renderer, original, custom, true);
    for (int i = 0; i < 20; ++i) {
        Obj(renderer).mesh = original; Obj(renderer).enabled = true;
        RefreshActiveReplacement(EnumerateLiveEndminfRenderers());
        Check(Obj(renderer).mesh == custom && !Obj(renderer).enabled,
            "write-back or mask drift not repaired");
    }
    Check(g_replaced.size() == 1 && handles.size() == 3, "write-backs accumulate ownership records");
    g_mask_visible = true; RefreshActiveReplacement(EnumerateLiveEndminfRenderers());
    Check(Obj(renderer).enabled, "mask toggle not honored");
    Check(RollbackReplacement(), "write-back rollback failed");
}
void DifferentAssetDoesNotBorrowPalette() {
    Reset();
    auto original = Make("original", 101994);
    auto custom = Make("custom", 59073);
    auto different = Make("different bindpose palette", 101994);
    auto renderer = Make("S_actor_endminf_cloth_01_lod0", 0, custom);
    auto clone = Make("S_actor_endminf_cloth_01_lod0", 0, different);
    renderers = {renderer, clone}; Record(renderer, original, custom);
    // With no payload available, there is no safe way to build for this asset.
    // The old count-only fallback silently assigned the donor's palette.
    RefreshActiveReplacement(EnumerateLiveEndminfRenderers());
    Check(Obj(clone).mesh == different, "different source borrowed donor bindposes");
    Check(RollbackReplacement(), "different asset rollback failed");
}
void RetargetedRendererRollback() {
    Reset();
    auto original = Make("original", 101994);
    auto custom = Make("custom", 59073);
    auto new_game_mesh = Make("game replacement", 123);
    auto renderer = Make("S_actor_endminf_cloth_01_lod0", 0, custom);
    renderers = {renderer}; Record(renderer, original, custom, true);
    Obj(renderer).mesh = new_game_mesh; Obj(renderer).enabled = false;
    Check(RollbackReplacement(), "retargeted renderer rollback failed");
    Check(Obj(renderer).mesh == new_game_mesh && !Obj(renderer).enabled,
        "rollback overwrote game-owned new state");
}
}
int main() {
    FullScanAndCanvasRefresh();
    InheritedClone();
    RollbackBeforeRefresh();
    FailureRetainsOwnership();
    RepeatedWriteBackAndMask();
    DifferentAssetDoesNotBorrowPalette();
    RetargetedRendererRollback();
    Reset();
    std::cout << "PASS: 7 custom model lifecycle scenarios\n";
}
