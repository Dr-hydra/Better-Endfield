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
    int hide_flags = 0;
    void* materials = nullptr;
    void* parent = nullptr;
    bool enabled = true;
    bool reject_set = false;
};
std::vector<void*> renderers;
std::vector<std::unique_ptr<std::vector<void*>>> material_arrays;
void* MaterialArray(std::vector<void*> values) {
    material_arrays.push_back(std::make_unique<std::vector<void*>>(std::move(values)));
    return material_arrays.back().get();
}
std::unordered_map<void*, std::unique_ptr<Object>> objects;
std::unordered_set<void*> destroyed;
std::unordered_map<uint32_t, void*> handles;
uint32_t next_handle = 1;
bool fail_snapshot = false;
int palette_count = 156;
uint64_t fake_boxed = 0;
void* installed_hook = nullptr;
std::vector<std::string> logs;

void Check(bool ok, const char* reason) {
    if (!ok) { for (const auto& log : logs) std::cerr << log << "\n"; std::cerr << "FAIL: " << reason << '\n'; std::exit(1); }
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
void* Box(uint64_t value) { fake_boxed = value; return &fake_boxed; }
void* BE_CALL InvokeFake(void*, const void* method, void* instance,
    void** args, void** exception) {
    if (method == &g_early_material_ctor) {
        Obj(instance).name = Obj(args[0]).name + " (Instance)";
        return nullptr;
    }
    if (method == &g_early_array_set) {
        static_cast<std::vector<void*>*>(instance)->at(*static_cast<int*>(args[1])) = args[0];
        return nullptr;
    }
    const std::string_view key = static_cast<const MethodContract*>(method)->key;
    if (key == "array.clone") return MaterialArray(*static_cast<std::vector<void*>*>(instance));
    if (key == "resources.find_objects_of_type_all") {
        if (fail_snapshot) { *exception = &fail_snapshot; return nullptr; }
        return &renderers;
    }
    if (key == "renderer.get_shared_materials") return Obj(instance).materials;
    // Unity may return an already-instanced material unchanged after rollback.
    if (key == "renderer.get_materials") return Obj(instance).materials;
    if (key == "renderer.set_shared_materials") { Obj(instance).materials = args[0]; return nullptr; }
    if (key == "array.get_length" || key == "array.get_value") {
        for (const auto& a : material_arrays) if (instance == a.get())
            return key == "array.get_length" ? Box(a->size()) : a->at(*static_cast<int*>(args[0]));
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
    if (key == "object.get_hide_flags") return Box(Obj(instance).hide_flags);
    if (key == "object.set_hide_flags") { Obj(instance).hide_flags = *static_cast<int*>(args[0]); return nullptr; }
    if (key == "transform.get_parent") return Obj(instance).parent;
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
    if (g_host) {
        for (size_t i = 0; i < 2; ++i) { CharacterScope scope(i); ClearReplacementState(); g_character_sessions[i].armed = false; }
    }
    g_multi_character = false; g_resource_path = false;
    Check(handles.empty(), "leaked GC handles");
    renderers.clear(); material_arrays.clear(); objects.clear(); destroyed.clear(); logs.clear();
    SelectCharacterTarget("endminf");
    fail_snapshot = false; installed_hook = nullptr;
    g_mask_visible = false; g_next_refresh_tick = 0;
    g_operation_in_progress = false; g_pump_hits = 0;
    host.object_new = [](void*, const void*) -> void* { return Make("new material"); };
    g_early_material_ctor.method_info = &g_early_material_ctor;
    g_early_array_set.method_info = &g_early_array_set;
    g_early_set_material.method_info = nullptr;
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
    // inherited custom mesh; newly built receiver meshes are covered separately.
    for (int i = 0; i < 4500; ++i) renderers.push_back(Make("background"));
    auto ui = Make("S_actor_endminf_cloth_01_lod0", 0, custom);
    renderers.push_back(ui);
    Check(EnumerateLiveTargetRenderers().size() == 2, "discovery truncated at 4096");
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
    RefreshActiveReplacement(EnumerateLiveTargetRenderers());
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
    Check(g_replacement_active && destroyed.empty() && handles.size() == 3 + g_retained_assets.size(),
        "snapshot failure discarded live ownership");
    fail_snapshot = false; Obj(renderer).reject_set = true;
    Check(!RollbackReplacement(), "rejected restoration reported success");
    Check(g_replacement_active && destroyed.empty() && handles.size() == 3 + g_retained_assets.size(),
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
    // Empty skin fixture isolates ownership/write-back; GPU skin validation
    // requires the real engine and is outside this fake Host test.
    BemComponent component{};
    component.info.component_id = 9; component.info.stride2 = 4;
    g_active_bem.components.push_back(component);
    for (int i = 0; i < 20; ++i) {
        Obj(renderer).mesh = original; Obj(renderer).enabled = true;
        RefreshActiveReplacement(EnumerateLiveTargetRenderers());
        Check(Obj(renderer).mesh == custom && !Obj(renderer).enabled,
            "write-back or mask drift not repaired");
    }
    Check(g_replaced.size() == 1 && handles.size() == 3 + g_retained_assets.size(), "write-backs accumulate ownership records");
    g_mask_visible = true; RefreshActiveReplacement(EnumerateLiveTargetRenderers());
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
    RefreshActiveReplacement(EnumerateLiveTargetRenderers());
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
void PelicaTargetIsolation() {
    Reset();
    Check(SelectCharacterTarget("pelica"), "Pelica target unavailable");
    Check(!SelectCharacterTarget("unknown") && g_target_pelica, "unknown target changed routing");
    auto original = Make("S_actor_pelica_face_01_lod0", 9186);
    auto pelica = Make("S_actor_pelica_face_01_lod0", 0, original);
    auto endmin = Make("S_actor_endminf_face_01_lod0", 0, original);
    auto lod1 = Make("S_actor_pelica_face_01_lod1", 0, original);
    auto shadow = Make("S_actor_pelica_face_01_shadowProxyDesktop", 0, original);
    renderers = {endmin, lod1, shadow, pelica};
    const auto live = EnumerateLiveTargetRenderers();
    Check(live.size() == 1 && live[0].renderer == pelica, "Pelica selected another character/LOD/proxy");
    BemComponentHeaderRaw info{};
    info.component_id = 1; info.original_index_count = 9186;
    Check(MatchesTargetComponent(info, live[0]), "verified Pelica face rejected");
    auto wrong = live[0]; wrong.mesh_name = "wrong source mesh";
    Check(!MatchesTargetComponent(info, wrong), "count-only mesh match accepted");
    info.component_id = 0;
    Check(!MatchesTargetComponent(info, live[0]), "wrong component identity accepted");
    Check(IsGameplayPostModelPath("root/chr_0004_pelica_postmodel(Clone)/Mesh_all"), "Pelica container missed");
    Check(!IsGameplayPostModelPath("root/chr_0003_endminf_postmodel(Clone)"), "administrator container accepted");
}
void PelicaPayloadContract() {
    Reset(); SelectCharacterTarget("pelica");
    BemPocData bem;
    for (uint32_t id = 0; id < kPelicaComponents.size(); ++id) {
        BemComponent component{};
        component.info.component_id = id;
        component.info.original_index_count = kPelicaComponents[id].indices;
        bem.components.push_back(component);
    }
    Check(ValidateTargetPayload(bem), "valid Pelica identities refused");
    bem.components[1].info.component_id = 0;
    Check(!ValidateTargetPayload(bem), "duplicate Pelica ID accepted");
    bem.components[1].info.component_id = 1;
    bem.components[1].info.original_index_count++;
    Check(!ValidateTargetPayload(bem), "wrong source count accepted");
    bem.components[1].info.original_index_count--;
    bem.textures.resize(1);
    Check(!ValidateTargetPayload(bem), "unmapped Pelica textures accepted");
}
void PelicaRefreshAndRollback() {
    Reset(); SelectCharacterTarget("pelica");
    auto original = Make("S_actor_pelica_face_01_lod0", 9186);
    auto custom = Make("custom Pelica face", 9186);
    auto first = Make("S_actor_pelica_face_01_lod0", 0, custom);
    auto second = Make("S_actor_pelica_face_01_lod0", 0, custom);
    auto wrong_part = Make("S_actor_pelica_hair_01_lod0", 0, original);
    auto endmin = Make("S_actor_endminf_face_01_lod0", 0, original);
    renderers = {first, second, wrong_part, endmin};
    Record(first, original, custom);
    g_replaced[0].component_id = 1;
    g_replaced[0].original_index_count = 9186;
    RefreshActiveReplacement(EnumerateLiveTargetRenderers());
    Check(Obj(second).mesh == custom && g_replaced.size() == 2, "new Pelica instance not refreshed");
    Check(Obj(wrong_part).mesh == original && Obj(endmin).mesh == original, "refresh crossed component/character boundary");
    Check(RollbackReplacement(), "Pelica rollback failed");
    Check(Obj(first).mesh == original && Obj(second).mesh == original && handles.empty(), "Pelica rollback incomplete");
}

void FreshReceiversNeverBorrowMeshes() {
    Reset(); g_multi_character = true;
    auto original = Make("source cloth", 101994);
    auto donor_mesh = Make("donor replacement", 59073);
    auto donor = Make("S_actor_endminf_cloth_01_lod0", 0, donor_mesh);
    Record(donor, original, donor_mesh);
    auto world = Make("S_actor_endminf_cloth_01_lod0", 0, original);
    auto ui = Make("S_actor_endminf_cloth_01_lod0", 0, original);
    Obj(ui).enabled = false;
    auto world_materials = MaterialArray({Make("world original material")});
    auto ui_materials = MaterialArray({Make("UI original material")});
    Obj(world).materials = world_materials; Obj(ui).materials = ui_materials;
    g_active_bem.textures.resize(1);
    BemComponent component{};
    component.info.component_id = 9; component.info.original_index_count = 101994;
    component.info.stride2 = 4;
    g_active_bem.components.push_back(component);
    auto builder = +[](const BemComponent&, void* source, void*& result) -> bool {
        result = Make("independently built", 59073, source); return true;
    };
    ReplacedComponent w, u;
    Check(PrepareReplacement(SummarizeRenderer(world, "world"), w, builder), "world prepare failed");
    g_replaced.push_back(w);
    Check(PrepareReplacement(SummarizeRenderer(ui, "details"), u, builder), "UI prepare failed");
    g_replaced.push_back(u);
    Check(w.custom_mesh != u.custom_mesh && w.custom_mesh != donor_mesh &&
        u.custom_mesh != donor_mesh, "new receivers borrowed an existing mesh");
    Check(Obj(w.custom_mesh).mesh == original && Obj(u.custom_mesh).mesh == original,
        "builder did not receive each receiver's source asset");
    Check(!u.original_enabled && w.original_enabled, "receiver enabled state copied from donor");
    Check(w.original_material == world_materials && u.original_material == ui_materials &&
        w.custom_material != u.custom_material &&
        ArrayValue(w.custom_material, 0) != ArrayValue(u.custom_material, 0),
        "fresh receivers borrowed material or rollback state");
    renderers = {donor, world, ui};
    Check(RollbackReplacement(), "independent receiver rollback failed");
    Check(Obj(world).mesh == original && Obj(ui).mesh == original &&
        destroyed.size() == 3 && handles.empty(), "independent meshes not restored/released");
    Check(Obj(world).materials == world_materials && Obj(ui).materials == ui_materials,
        "independent materials not restored");
}

void ModelTextureGroupsUseObjectIdentity() {
    Reset();
    auto world = Make("chr_0003_endminf_postmodel(Clone)");
    auto ui = Make("chr_0003_endminf_uimodel(Clone)");
    auto ui2 = Make("chr_0003_endminf_uimodel(Clone)");
    auto w = Make("S_actor_endminf_cloth_01_lod0");
    auto u9 = Make("S_actor_endminf_cloth_01_lod0");
    auto u10 = Make("S_actor_endminf_cloth_02_lod0");
    auto other = Make("S_actor_endminf_cloth_01_lod0");
    Obj(w).parent = world; Obj(u9).parent = Obj(u10).parent = ui; Obj(other).parent = ui2;
    const auto groups = GroupModelTextureRenderers({{9, w}, {9, u9}, {10, u10}, {9, other}});
    Check(groups.size() == 3 && groups.at(ui).size() == 2 && groups.at(ui2).size() == 1,
        "texture installation split UI components or merged separate models");
    Check(TextureModelRoot(u9) == TextureModelRoot(u10), "same model components separated");
    Check(TextureModelRoot(u9) != TextureModelRoot(w) &&
        TextureModelRoot(u9) != TextureModelRoot(other), "different models merged by name");
}

void AdminSharedTextureMask() {
    Reset();
    auto cloth = Make("T_actor_endminf_cloth_01_D");
    auto unrelated = Make("different texture with the same dimensions");
    std::vector<LiveTextureSlot> slots(3);
    slots[0].texture = slots[1].texture = cloth;
    slots[0].component_mask = 1u << 9; slots[1].component_mask = 1u << 10;
    slots[2].texture = unrelated; slots[2].component_mask = 1u << 4;
    UnionTextureComponentMasks(slots);
    Check(slots[0].component_mask == ((1u << 9) | (1u << 10)) &&
        slots[1].component_mask == slots[0].component_mask,
        "admin shared cloth texture lost full component mask");
    Check(slots[2].component_mask == (1u << 4), "unrelated texture joined cloth mask");
}

void PelicaPrivateMaterials() {
    Reset(); SelectCharacterTarget("pelica");
    auto material = Make("M_actor_pelica_cloth_01");
    auto original_array = MaterialArray({material});
    auto renderer = Make("S_actor_pelica_cloth_01_lod0");
    auto other = Make("unrelated user of shared material");
    Obj(renderer).materials = Obj(other).materials = original_array;
    ReplacedComponent record; record.renderer = renderer;
    Check(PreparePelicaMaterials(record), "private material preparation failed");
    Check(record.original_material == original_array, "original materials lost");
    Check(Obj(renderer).materials == record.custom_material &&
        Obj(other).materials == original_array, "private preparation changed another renderer");
    Check(ArrayValue(record.custom_material, 0) != material, "material was not cloned");
    Check(ApplyRendererMaterials(renderer, record.original_material), "private materials did not restore");
    Check(Obj(renderer).materials == original_array, "renderer still uses private material");
    ReleaseRetainedAssets(true);
}

void MultipleCharactersIndependentRollback() {
    Reset(); g_multi_character = true;
    auto a = Make("S_actor_endminf_cloth_01_lod0", 101994);
    auto ac = Make("custom endmin", 59073);
    auto ar = Make("S_actor_endminf_cloth_01_lod0", 0, ac);
    auto b = Make("S_actor_pelica_face_01_lod0", 9186);
    auto bc = Make("custom pelica", 9186);
    auto br = Make("S_actor_pelica_face_01_lod0", 0, bc);
    renderers = {ar, br};
    Record(ar, a, ac);
    {
        CharacterScope scope(1);
        Record(br, b, bc); g_replaced[0].component_id = 1;
        Check(g_replaced.size() == 1 && g_replaced[0].renderer == br, "Pelica session contains admin record");
    }
    Check(g_replaced.size() == 1 && g_replaced[0].renderer == ar, "admin state lost after scope");
    Check(RollbackReplacement(), "admin-only rollback failed");
    Check(Obj(ar).mesh == a && Obj(br).mesh == bc && AnyCharacterActive(), "admin rollback affected Pelica");
    {
        CharacterScope scope(1);
        Check(RollbackReplacement(), "Pelica-only rollback failed");
    }
    Check(!AnyCharacterActive() && Obj(br).mesh == b && handles.empty(), "multi rollback leaked state");
}
void SharedAssetRetentionAcrossCharacters() {
    Reset(); g_multi_character = true;
    auto shared = Make("shared original texture");
    Check(RetainAsset(shared), "admin retain failed");
    { CharacterScope scope(1); Check(RetainAsset(shared), "Pelica retain failed"); }
    ReleaseRetainedAssets(true);
    Check((Obj(shared).hide_flags & kDontUnloadUnusedAsset) != 0, "admin released Pelica unload protection");
    { CharacterScope scope(1); ReleaseRetainedAssets(true); }
    Check(Obj(shared).hide_flags == 0 && handles.empty(), "last owner did not restore protection");
}
void ResourceModeDoesNotFallBackToSceneScan() {
    Reset(); g_multi_character = true; g_resource_path = true;
    for (auto& session : g_character_sessions) session.armed = true;
    // Fail the global scan: resource mode must not silently use it to replace.
    fail_snapshot = true;
    PumpMultiCharacter();
    Check(!AnyCharacterActive(), "resource mode became instance replacement");
    for (const auto& line : logs)
        Check(line.find("resources.find_objects") == std::string::npos, "resource mode performed global renderer scan");
    Check(MultiCharacterForAsset("chr_0004_pelica_postmodel") == 1 &&
        MultiCharacterForAsset("chr_0003_endminf_postmodel") == 0 &&
        MultiCharacterForAsset("chr_0013_aglina_postmodel") == -1, "resource routing crossed character");
}

void RepeatedMaterialGeneration() {
    Reset(); g_multi_character = true;
    auto material = Make("already instanced administrator cloth");
    auto original = MaterialArray({material});
    auto renderer = Make("S_actor_endminf_cloth_01_lod0");
    Obj(renderer).materials = original;
    std::unordered_set<void*> copies;
    for (int i = 0; i < 5; ++i) {
        ReplacedComponent record; record.renderer = renderer; record.component_id = 9;
        Check(PreparePelicaMaterials(record), "repeat generation rejected existing material instance");
        auto copy = ArrayValue(record.custom_material, 0);
        Check(copy != material && copies.insert(copy).second, "repeat generation reused old instance");
        Check(record.original_material == original, "repeat generation captured wrong rollback material");
        Check(ApplyRendererMaterials(renderer, record.original_material), "repeat material rollback failed");
        ReleaseRetainedAssets(true);
        Check(handles.empty(), "repeat material preparation leaked handles");
    }
}

}
int main(int argc, char** argv) {
    FullScanAndCanvasRefresh();
    InheritedClone();
    RollbackBeforeRefresh();
    FailureRetainsOwnership();
    RepeatedWriteBackAndMask();
    DifferentAssetDoesNotBorrowPalette();
    RetargetedRendererRollback();
    PelicaTargetIsolation();
    PelicaPayloadContract();
    PelicaRefreshAndRollback();
    PelicaPrivateMaterials();
    FreshReceiversNeverBorrowMeshes();
    ModelTextureGroupsUseObjectIdentity();
    AdminSharedTextureMask();
    RepeatedMaterialGeneration();
    MultipleCharactersIndependentRollback();
    SharedAssetRetentionAcrossCharacters();
    ResourceModeDoesNotFallBackToSceneScan();
    Reset();
    std::cout << "PASS: 18 custom model lifecycle scenarios\n";
    if (argc > 1) {
        SelectCharacterTarget("pelica");
        BemPocData payload;
        Check(LoadBemPoc(Utf8Path(argv[1]), payload), "real Pelica payload rejected by runtime loader");
        Check(payload.components.size() == 10, "unexpected real payload contents");
        std::cout << "PASS: real Pelica payload loaded by production parser\n";
    }
}
