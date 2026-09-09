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
Particle* finish_particle = nullptr;
void* BE_CALL InvokeFake(void*, const void* method, void* instance, void** args, void** exception) {
    *exception = nullptr;
    ++unity_calls;
    auto id = static_cast<MethodId>(reinterpret_cast<uintptr_t>(method) - 1);
    auto* p = static_cast<Particle*>(instance);
    switch (id) {
    case UnityAlive: return &static_cast<Particle*>(args[0])->alive;
    case ParticleMain: return p;
    case ParticleStop: ++p->stops; return nullptr;
    case ParticleSetLoop: p->loop = *static_cast<bool*>(args[0]); return nullptr;
    case ParticleSetDuration: p->duration = *static_cast<float*>(args[0]); return nullptr;
    default: CHECK(false); return nullptr;
    }
}
void* BE_CALL UnboxFake(void*, void* value) { return value; }
void BE_CALL FreeFake(void*, uint32_t handle) { freed.push_back(handle); }
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
    host.gchandle_free = &FreeFake;
    g_host = &host;
    g_game_thread = GetCurrentThreadId();
    for (int i = 0; i < MethodCount; ++i)
        g_methods[i].resolved.method_info = reinterpret_cast<void*>(static_cast<uintptr_t>(i + 1));
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
    g_host = nullptr;
    std::puts("Actions effect restoration: all checks passed.");
}
