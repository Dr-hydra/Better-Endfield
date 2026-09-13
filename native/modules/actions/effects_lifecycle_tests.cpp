// Exercise the production restoration/finish paths with a minimal managed API
// double. This is not a simulation of Unity particle rendering or the game.
#include "module.cpp"
#include <cstdlib>
#include <vector>
using namespace BetterEndfield::Actions;
#define CHECK(x) do { if (!(x)) { std::fprintf(stderr, "line %d: %s\n", __LINE__, #x); std::exit(1); } } while (false)
namespace {
struct Particle { bool alive = true; bool loop = true; float duration = 2.5f; int stops = 0; };
std::vector<uint32_t> freed;
int unity_calls = 0, finish_calls = 0, stop_calls = 0;
int follow_calls = 0, action_calls = 0, mesh_calls = 0;
int visual_identity = 0, visual_handle = 0, visual_other = 0;
void* action_handle = &visual_handle;
unsigned visual_logic = 20;
int visual_actor = 4;
float visual_delay = 1.5667f;
bool moving = true, airborne = false;
bool main_character = true, visual_metadata_error = false;
const char* perform_id = "CharIntPerform_Liino_Spdash";
Particle* finish_particle = nullptr;
void* BE_CALL InvokeFake(void*, const void* method, void* instance, void** args, void** exception) {
    *exception = nullptr;
    ++unity_calls;
    auto id = static_cast<MethodId>(reinterpret_cast<uintptr_t>(method) - 1);
    auto* p = static_cast<Particle*>(instance);
    switch (id) {
    case GetEntity: case GetCharCtrl: case GetMovement: case GetGait:
    case GetBlackboard: case GetAnimator: case CurrentState: return &visual_identity;
    case EntityValid: { static bool yes = true; return &yes; }
    case IsMain: return &main_character;
    case VisualPerformId:
        if (visual_metadata_error) { *exception = &visual_other; return nullptr; }
        return const_cast<char*>(instance == &visual_handle ? perform_id : "OtherPerform");
    case VisualPerformEntity: return &visual_identity;
    case Moving: return &moving;
    case Airborne: return &airborne;
    case IsDead: case InTransition: case StateLoop: { static bool no = false; return &no; }
    case MoveMode: return &g_dash_mode;
    case GaitValue: return &g_sprint;
    case StateHash: return &g_hashes[0];
    case StateTime: { static float time = .5f; return &time; }
    case StateLength: { static float length = 173.f/60; return &length; }
    case EffectObject: return p;
    case EffectManualFollow: ++follow_calls; return nullptr;
    case UnityAlive: return &static_cast<Particle*>(args[0])->alive;
    case ParticleMain: return p;
    case ParticleStop: ++p->stops; return nullptr;
    case ParticleSetLoop: p->loop = *static_cast<bool*>(args[0]); return nullptr;
    case ParticleSetDuration: p->duration = *static_cast<float*>(args[0]); return nullptr;
    default: CHECK(false); return nullptr;
    }
}
void* BE_CALL UnboxFake(void*, void* value) { return value; }
int BE_CALL CopyStringFake(void*, const void* value, char* output, size_t capacity) {
    return std::snprintf(output, capacity, "%s", static_cast<const char*>(value));
}
void BE_CALL FreeFake(void*, uint32_t handle) { freed.push_back(handle); }
void* BE_CALL FieldFake(void*, const void* field, void*) {
    switch (static_cast<FieldId>(reinterpret_cast<uintptr_t>(field) - 1)) {
    case Request: return &visual_identity;
    case RequestHandle: return &visual_handle;
    case VisualActionHandle: return action_handle;
    case VisualActionData: return &visual_identity;
    case ActionLogic: return &visual_logic;
    case ActionIndex: return &visual_actor;
    case ActionDelay: return &visual_delay;
    default: CHECK(false); return nullptr;
    }
}
void __fastcall ActionFake(void*, const void*) { ++action_calls; }
void __fastcall MeshFake(void*, void*, int, bool, const void*) { ++mesh_calls; }
void __fastcall FinishFake(void*, bool, bool, const void*) {
    ++finish_calls;
    CHECK(!finish_particle->loop && finish_particle->duration == 5.0f);
}
void __fastcall StopFake(void*, const void*) { ++stop_calls; }
EffectHold Held(Particle& p, void* instance) {
    EffectHold e;
    e.object = instance; e.root = 102; e.prepared = true; e.count = 1;
    e.particles[0] = {&p, 101, false, 5.0f};
    return e;
}
}
int main() {
    BE_HostApiV1 host{};
    host.runtime_invoke = &InvokeFake;
    host.object_unbox = &UnboxFake;
    host.copy_managed_string = &CopyStringFake;
    host.gchandle_free = &FreeFake;
    host.field_get_value_object = &FieldFake;
    g_host = &host;
    g_game_thread = GetCurrentThreadId();
    for (int i = 0; i < MethodCount; ++i)
        g_methods[i].resolved.method_info = reinterpret_cast<void*>(static_cast<uintptr_t>(i + 1));
    for (int i = 0; i < FieldCount; ++i)
        g_fields[i].resolved.field_info = reinterpret_cast<void*>(static_cast<uintptr_t>(i + 1));
    int identity = 0, unrelated = 0;
    Particle p;
    auto effect = Held(p, &identity);
    RestoreEffect(effect, true);
    CHECK(!p.loop && p.duration == 5.0f && p.stops == 1);
    CHECK(effect.disabled && effect.object == nullptr && effect.count == 0);
    CHECK(freed == std::vector<uint32_t>({101,102}));
    RestoreEffect(effect, true);
    CHECK(freed.size() == 2 && p.stops == 1); // Idempotent during nested Finish/OnStop.

    freed.clear(); p = {}; p.alive = false;
    effect = Held(p, &identity);
    RestoreEffect(effect, true);
    CHECK(p.stops == 0 && freed.size() == 2); // Destroyed Unity objects only release managed pins.

    freed.clear(); p = {}; effect = Held(p, &identity);
    int before = unity_calls;
    RestoreEffect(effect, false);
    CHECK(unity_calls == before && freed.size() == 2); // No Unity calls on worker shutdown.

    freed.clear(); p = {}; finish_particle = &p;
    g_effect_finish = &FinishFake; g_effect_stop = &StopFake;
    g_session.effects[0] = Held(p, &identity);
    EffectStopDetour(&unrelated, nullptr);
    CHECK(stop_calls == 1 && p.loop && p.stops == 0 && freed.empty());
    EffectFinishDetour(&identity, false, false, nullptr);
    CHECK(finish_calls == 1 && !p.loop && p.stops == 1 && freed.size() == 2);
    EffectStopDetour(&identity, nullptr);
    CHECK(stop_calls == 2 && p.stops == 1 && freed.size() == 2);

    freed.clear(); p = {};
    g_session.component = &identity;
    g_session.effects[0] = Held(p, &identity);
    g_stopping = true; before = unity_calls;
    Cancel("shutdown test", false, false);
    CHECK(!g_session.component && unity_calls == before && freed.size() == 2);
    g_stopping = false;
    p = {};
    CHECK(RefreshEffectFollow(&p) && follow_calls == 1 && p.stops == 0 && p.duration == 2.5f);
    p.alive = false;
    CHECK(!RefreshEffectFollow(&p) && follow_calls == 1);
    CHECK(!RefreshEffectFollow(nullptr) && follow_calls == 1);

    auto config = std::make_shared<Configuration>(); config->enabled = true; config->liino_clean = false;
    g_config.store(config); g_session = {};
    g_session.component = &visual_identity; g_session.entity = &visual_identity;
    g_session.handle = &visual_handle; g_session.config = config;
    g_session.profile = &kCharacters[1]; g_session.policy.active = true;
    g_hashes[0] = 123; g_hashes[1] = 456; g_dash_mode = 20; g_sprint = 2;
    g_effect_action_play = ActionFake;
    EffectActionPlayDetour(&visual_identity, nullptr);
    CHECK(action_calls == 0 && g_session.visual_teardowns_skipped == 1);
    for (int actor = 14; actor <= 17; ++actor) {
        visual_actor = actor; visual_logic = actor < 16 ? 31 : 42; visual_delay = 1.6f;
        EffectActionPlayDetour(&visual_identity, nullptr);
    }
    CHECK(action_calls == 0 && g_session.visual_teardowns_skipped == 5);
    visual_actor = 4; visual_logic = 20; visual_delay = 1.5667f;
    action_handle = &visual_other; EffectActionPlayDetour(&visual_identity, nullptr);
    action_handle = &visual_handle;
    g_session.profile = &kCharacters[0]; EffectActionPlayDetour(&visual_identity, nullptr);
    g_session.profile = &kCharacters[1];
    airborne = true; EffectActionPlayDetour(&visual_identity, nullptr); airborne = false;
    visual_actor = 3; EffectActionPlayDetour(&visual_identity, nullptr); visual_actor = 4;
    visual_delay = 0; EffectActionPlayDetour(&visual_identity, nullptr); visual_delay = 1.5667f;
    g_stopping = true; EffectActionPlayDetour(&visual_identity, nullptr); g_stopping = false;
    CHECK(action_calls == 6); // Foreign actor/perform, entry, exit and shutdown pass through.
    g_mesh_group_show = MeshFake; g_liino_mesh_groups[0] = 1; g_liino_mesh_groups[1] = 2;
    MeshGroupShowDetour(&visual_handle, &visual_identity, 1, false, nullptr);
    MeshGroupShowDetour(&visual_handle, &visual_identity, 2, false, nullptr);
    CHECK(mesh_calls == 0);
    MeshGroupShowDetour(&visual_handle, &visual_other, 1, false, nullptr);
    MeshGroupShowDetour(&visual_handle, &visual_identity, 3, false, nullptr);
    MeshGroupShowDetour(&visual_handle, &visual_identity, 1, true, nullptr);
    airborne = true; MeshGroupShowDetour(&visual_handle, &visual_identity, 1, false, nullptr);
    CHECK(mesh_calls == 4);
    // No animation session yet: native StartSpDash already executes cosmetic
    // actions here. Ownership must come from the actual perform, not a session.
    g_session = {}; config = std::make_shared<Configuration>(); config->enabled = true;
    g_config.store(config); action_calls = mesh_calls = 0; airborne = false;
    for (int actor = 0; actor < 22; ++actor) {
        visual_actor = actor;
        EffectActionPlayDetour(&visual_identity, nullptr);
    }
    for (int group : {1, 2}) {
        MeshGroupShowDetour(&visual_handle, &visual_identity, group, true, nullptr);
        MeshGroupShowDetour(&visual_handle, &visual_identity, group, false, nullptr);
    }
    CHECK(action_calls == 0 && mesh_calls == 0); // No creation, no false restore cache.
    // The same scope holds through exit after the movement session has ended.
    airborne = true; moving = false;
    EffectActionPlayDetour(&visual_identity, nullptr);
    CHECK(action_calls == 0);
    action_handle = &visual_other; EffectActionPlayDetour(&visual_identity, nullptr);
    action_handle = &visual_handle;
    perform_id = "CharIntPerform_Aglina_Spdash"; EffectActionPlayDetour(&visual_identity, nullptr);
    perform_id = "CharIntPerform_Liino_Spdash";
    main_character = false; EffectActionPlayDetour(&visual_identity, nullptr); main_character = true;
    visual_metadata_error = true; EffectActionPlayDetour(&visual_identity, nullptr); visual_metadata_error = false;
    config->characters = 1; EffectActionPlayDetour(&visual_identity, nullptr); config->characters = 3;
    config->liino_clean = false; EffectActionPlayDetour(&visual_identity, nullptr); config->liino_clean = true;
    config->enabled = false; EffectActionPlayDetour(&visual_identity, nullptr); config->enabled = true;
    g_liino_clean_contract = false; EffectActionPlayDetour(&visual_identity, nullptr); g_liino_clean_contract = true;
    g_stopping = true; EffectActionPlayDetour(&visual_identity, nullptr); g_stopping = false;
    g_game_thread = GetCurrentThreadId() + 1; before = unity_calls;
    EffectActionPlayDetour(&visual_identity, nullptr); CHECK(unity_calls == before);
    g_game_thread = GetCurrentThreadId();
    CHECK(action_calls == 10);
    MeshGroupShowDetour(&visual_handle, &visual_other, 1, true, nullptr);
    MeshGroupShowDetour(&visual_other, &visual_identity, 1, true, nullptr);
    MeshGroupShowDetour(&visual_handle, &visual_identity, 3, true, nullptr);
    CHECK(mesh_calls == 3);
    g_host = nullptr;
    std::puts("Actions effects: restoration, retained visuals, clean entry/exit, all 22 VFX slots and ownership exclusions passed.");
}
