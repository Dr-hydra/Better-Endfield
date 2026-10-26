// Drive the production jsspsi_quality path through scripted dashes with fake
// Unity objects: StartSpDash, PreLate rewinds, TailLate seam and accessory
// writes, the release fade, pose lease contention, missing bones or mount, and
// the dash sound stop. With the option off nothing beyond the lightweight path
// may run. This is not a simulation of the game's animation or cloth.
#include "module.cpp"
#include "../../shared/motion/pose_lease_registry.h"
#include <cstdlib>
#include <deque>
#include <map>
#include <set>
#include <string>
#include <thread>
#include <vector>
using namespace BetterEndfieldNext;
using namespace BetterEndfieldNext::Actions;
#define CHECK(x) do { if (!(x)) { std::fprintf(stderr, "line %d: %s\n", __LINE__, #x); std::exit(1); } } while (false)
namespace {
constexpr int kRight = -445642423, kLeft = 528430122, kMount = 340743731;
constexpr float kLength = 5.667f, kDelta = 1.f / 60;
const LoopQualityData& data = JsspsiQuality::kData;
struct Fake { bool alive = true; };
struct ParticleFake : Fake {
    bool loop = false;
    float duration = 1;
    int plays = 0, stops = 0;
};
struct EffectFake : Fake {
    std::string name = "UnrelatedEffect";
    bool available = true;
    int count = 1;
    ParticleFake particles[16];
    float last_delta = -1;
};
struct TransformFake : Fake { void* root = nullptr; BonePose pose{}; uint32_t writes = 0; };
struct StateFake { int hash = 0; float time = 0, length = kLength; };
struct AnimatorFake : Fake {
    TransformFake* root = nullptr;
    StateFake current, next;
    bool transitioning = false;
    int transition_frames = 0;
    bool pending = false;
    float pending_duration = 0, pending_offset = 0;
    std::vector<float> durations;
};
// Native pose of a seam bone: the end pose E at the loop begin and D*E at the
// loop target, so the fake clip has exactly the seam the data describes.
struct BoneModel {
    BonePose end{};
    const QualitySeamBone* seam = nullptr;
    int accessory = -1;
    bool constant = false; // Native animation never rewrites this bone.
};
struct World {
    Fake component, entity, charctrl, blackboard, movement, gait, request, handle, dictionary, template_data, tmp[3];
    AnimatorFake rider, mount, sides[2];
    TransformFake rider_root, mount_root, side_roots[2];
    std::map<std::string, TransformFake> rider_bones, mount_bones;
    std::map<TransformFake*, BoneModel> models;
    std::set<std::string> missing;
    int frame = 1000;
    bool mount_available = true;
    bool effects_available = false, handle_current = true;
    Fake effect_dictionary;
    EffectFake effects[kEffectSlotCount];
};
World* w = nullptr;
AnimatorFake* replacement_rider = nullptr;
AnimatorFake* replacement_mount = nullptr;
TransformFake* replacement_root = nullptr;
std::map<std::string, TransformFake>* replacement_bones = nullptr;
Motion::PoseLeaseRegistry* registry = nullptr;
std::deque<std::string> strings;
std::vector<std::string> logs;
std::map<int, int> calls;
int side_hash = kRight;
bool yes = true, no = false, moving = true;
int move_mode = 20;
const char* perform = "CharIntPerform_Jsspsi_SpdashR";
uint32_t pins = 0, frees = 0, stops = 0, stop_event = 0, stop_playing = 0;
int stop_fade = 0, stop_action = -1, stop_curve = -1, interrupts = 0, mono_posts = 0;
uint64_t sound_object = 0x12345678abcdef12ULL, stop_object = 0, posted_object = 0;
uint32_t posted_event = 0;
int32_t sound_result = 1;
bool sound_lookup_fails = false, sound_stop_fails = false, wrong_audio_component = false;
float mount_enter_u = 0;              // The mount enters its dash state once the rider reaches this u.
TransformFake* failing_write = nullptr;
const void* kGetItem = &kGetItem;
const void* kContains = &kContains;
const void* kLock = &kLock;
const void* kHashFields[4] = {&kHashFields[0], &kHashFields[1], &kHashFields[2], &kHashFields[3]};
int hash_values[4] = {kLeft, kRight, 111, 222};

AnimatorFake* A(void* p) { return static_cast<AnimatorFake*>(p); }
TransformFake* T(void* p) { return static_cast<TransformFake*>(p); }
void* BE_CALL InvokeFake(void*, const void* method, void* instance, void** args, void** exception) {
    *exception = nullptr;
    if (method == kContains) return &w->effects[*static_cast<int*>(args[0])].available;
    if (method == kLock) return instance;
    if (method == kGetItem) {
        const int slot = *static_cast<int*>(args[0]);
        return instance == &w->effect_dictionary ? static_cast<void*>(&w->effects[slot]) : &w->tmp[slot];
    }
    const auto id = static_cast<MethodId>(reinterpret_cast<uintptr_t>(method) - 1);
    ++calls[id];
    switch (id) {
    case GetEntity: return &w->entity;
    case EntityValid: case IsMain: case IsPlayingPerform: return &yes;
    case GetTemplate: return &w->template_data;
    case GetCharCtrl: return &w->charctrl;
    case GetBlackboard: return &w->blackboard;
    case IsDead: case StateLoop: return &no;
    case GetMovement: return &w->movement;
    case MoveMode: return &move_mode;
    case Moving: return &moving;
    case Airborne: return &no;
    case GetGait: return &w->gait;
    case GaitValue: return &g_sprint;
    case PerformId: return const_cast<char*>(perform);
    case GetAnimator: return replacement_rider ? replacement_rider : &w->rider;
    case InTransition: return &A(instance)->transitioning;
    case CurrentState: return &A(instance)->current;
    case NextState: return &A(instance)->next;
    case StateHash: return &static_cast<StateFake*>(instance)->hash;
    case StateTime: return &static_cast<StateFake*>(instance)->time;
    case StateLength: return &static_cast<StateFake*>(instance)->length;
    case CrossFade: {
        auto* a = A(instance);
        a->pending = true;
        a->pending_duration = *static_cast<float*>(args[1]);
        a->pending_offset = *static_cast<float*>(args[3]);
        a->durations.push_back(a->pending_duration);
        return nullptr;
    }
    case JsspsiTmpAnimator: {
        const auto slot = static_cast<Fake*>(instance) - w->tmp;
        if (slot == 0) return w->mount_available ? (replacement_mount ? replacement_mount : &w->mount) : nullptr;
        return &w->sides[slot - 1];
    }
    case PoseComponentTransform: return A(instance)->root;
    case PoseFind: {
        const std::string path = static_cast<const char*>(args[0]);
        auto& bones = instance == replacement_root ? *replacement_bones :
            instance == &w->rider_root ? w->rider_bones : w->mount_bones;
        auto found = bones.find(path);
        return w->missing.count(path) || found == bones.end() ? nullptr : &found->second;
    }
    case PoseGetLocal: {
        const auto& pose = T(instance)->pose;
        *static_cast<PoseVector*>(args[0]) = pose.position;
        *static_cast<PoseQuaternion*>(args[1]) = pose.rotation;
        return nullptr;
    }
    case PoseSetLocal:
        if (instance == failing_write) { *exception = &w->entity; return nullptr; }
        T(instance)->pose = {*static_cast<PoseQuaternion*>(args[1]), *static_cast<PoseVector*>(args[0])};
        ++T(instance)->writes;
        return nullptr;
    case PoseIsChild: { static bool child; child = T(instance)->root == args[0]; return &child; }
    case PoseFrameCount: return &w->frame;
    case UnityAlive: return &static_cast<Fake*>(args[0])->alive;
    case EffectName: return static_cast<EffectFake*>(instance)->name.data();
    case EffectObject: case ComponentsInChildren: case ParticleMain: return instance;
    case ArrayLength: return &static_cast<EffectFake*>(instance)->count;
    case ArrayItem: return &static_cast<EffectFake*>(instance)->particles[*static_cast<int*>(args[0])];
    case ParticleLoop: return &static_cast<ParticleFake*>(instance)->loop;
    case ParticleDuration: return &static_cast<ParticleFake*>(instance)->duration;
    case ParticleSetLoop: static_cast<ParticleFake*>(instance)->loop = *static_cast<bool*>(args[0]); return nullptr;
    case ParticleSetDuration: static_cast<ParticleFake*>(instance)->duration = *static_cast<float*>(args[0]); return nullptr;
    case ParticlePlay: ++static_cast<ParticleFake*>(instance)->plays; return nullptr;
    case ParticleStop: ++static_cast<ParticleFake*>(instance)->stops; return nullptr;
    case AudioMonoComponent: return wrong_audio_component ? &w->entity : &w->component;
    case JsspsiAudioObjectId:
        CHECK(instance == nullptr && args[0] == &w->entity);
        if (sound_lookup_fails) { *exception = &w->entity; return nullptr; }
        return &sound_object;
    case JsspsiStopSound:
        CHECK(instance == nullptr);
        ++stops;
        stop_event = *static_cast<uint32_t*>(args[0]); stop_action = *static_cast<int32_t*>(args[1]);
        stop_object = *static_cast<uint64_t*>(args[2]); stop_fade = *static_cast<int32_t*>(args[3]);
        stop_curve = *static_cast<int32_t*>(args[4]); stop_playing = *static_cast<uint32_t*>(args[5]);
        if (sound_stop_fails) { *exception = &w->entity; return nullptr; }
        return &sound_result;
    default: std::fprintf(stderr, "unexpected method %d\n", id); CHECK(false); return nullptr;
    }
}
void* BE_CALL FieldFake(void*, const void* field, void*) {
    for (int i = 0; i < 4; ++i) if (field == kHashFields[i]) return &hash_values[i];
    switch (static_cast<FieldId>(reinterpret_cast<uintptr_t>(field) - 1)) {
    case TemplateId: return const_cast<char*>("chr_0036_jsspsi");
    case Request: return &w->request;
    case RequestHandle: return w->handle_current ? &w->handle : &w->request;
    case Effects: return w->effects_available ? &w->effect_dictionary : nullptr;
    case JsspsiTmpObjects: return &w->dictionary;
    case JsspsiRenderHelper: return nullptr;
    default: CHECK(false); return nullptr;
    }
}
void* BE_CALL UnboxFake(void*, void* value) { return value; }
int BE_CALL CopyStringFake(void*, const void* value, char* output, size_t capacity) {
    return std::snprintf(output, capacity, "%s", static_cast<const char*>(value));
}
uint32_t BE_CALL PinFake(void*, void*, int) { return ++pins; }
void BE_CALL FreeFake(void*, uint32_t) { ++frees; }
void BE_CALL LogFake(void*, const char*, const char* text) { logs.emplace_back(text); }
void* StringNewFake(const char* text) { strings.emplace_back(text); return strings.back().data(); }
void* ClassFake(void* object) { return object; }
const void* MethodFake(void*, const char* name, int) {
    if (std::strcmp(name, "get_Item") == 0) return kGetItem;
    if (std::strcmp(name, "ContainsKey") == 0) return kContains;
    return std::strcmp(name, "Lock") == 0 ? kLock : nullptr;
}
void* ParentFake(void*) { return nullptr; }
uint64_t BE_CALL Acquire(const void* r, const char* o) { return registry->Acquire(r, o); }
int BE_CALL Owns(const void* r, const char* o, uint64_t t) { return registry->Owns(r, o, t); }
int BE_CALL ReleaseLease(const void* r, const char* o, uint64_t t) { return registry->Release(r, o, t); }
void __fastcall StartFake(void*, int, const void*) {}
void __fastcall TickFake(void*, float, const void*) {}
void __fastcall InterruptFake(void*, const void*) { ++interrupts; }
void __fastcall ReleaseFake(void*, bool, const void*) {}
void __fastcall DurationFake(void* instance, float delta, const void*) {
    static_cast<EffectFake*>(instance)->last_delta = delta;
}
uint32_t __fastcall EntityPostFake(void*, uint32_t, int, void*, void*, const void*) { return 777; }
void __fastcall MonoPostFake(void*, uint32_t hash, float, const void*) {
    ++mono_posts;
    // Match the native route: post directly, without the Entity PostEvent hook.
    posted_event = hash;
    posted_object = sound_object;
}
bool Logged(const char* text) {
    for (const auto& line : logs) if (line.find(text) != std::string::npos) return true;
    return false;
}
float Degrees(PoseQuaternion a, PoseQuaternion b) {
    const auto d = QualityMul(a, QualityConj(b));
    const double v = std::sqrt(double(d.x)*d.x + double(d.y)*d.y + double(d.z)*d.z);
    return float(2 * std::atan2(v, std::fabs(double(d.w))) * 57.29577951308232);
}
float Distance(PoseVector a, PoseVector b) {
    return std::sqrt((a.x-b.x)*(a.x-b.x) + (a.y-b.y)*(a.y-b.y) + (a.z-b.z)*(a.z-b.z));
}
BonePose Native(const BoneModel& model, float u) {
    if (model.accessory >= 0) { // A big native swing the accessory lap must replace.
        const float a = .9f * std::sin(u * 31.4f) + model.accessory * .01f;
        return {PoseNormalize({0, std::sin(a / 2), 0, std::cos(a / 2)}), model.end.position};
    }
    const auto d = QualitySeamRotation(*model.seam);
    const auto dp = QualitySeamPosition(*model.seam);
    const BonePose start{PoseNormalize(QualityMul(d, model.end.rotation)),
        {model.end.position.x + dp.x, model.end.position.y + dp.y, model.end.position.z + dp.z}};
    const float x = std::clamp((u - data.target) / (data.begin - data.target), 0.f, 1.f);
    return PoseBlend(start, model.end, x);
}
void AddBones(std::map<std::string, TransformFake>& bones, TransformFake& root, const QualityTree& tree,
    const QualitySeamEntry* seam, int side_index) {
    uint32_t seed = 7;
    auto random = [&] { seed = seed * 1664525u + 1013904223u; return (seed >> 8) / float(1u << 24) * 2 - 1; };
    std::string path;
    for (uint16_t i = 0; i < tree.count; ++i) {
        CHECK(QualityPath(tree, i, path));
        auto& bone = bones[path];
        bone.root = &root;
        bone.pose = {PoseNormalize({random(), random(), random(), random() + 2}), {random() * .1f, random() * .1f, random() * .1f}};
    }
    for (uint16_t i = 0; seam && i < seam->count; ++i) {
        CHECK(QualityPath(tree, seam->bones[i].node, path));
        auto& model = w->models[&bones[path]];
        model.end = bones[path].pose;
        model.seam = &seam->bones[i];
    }
    for (uint16_t i = 0; side_index >= 0 && i < data.accessory_count; ++i) {
        CHECK(QualityPath(tree, data.accessory_nodes[i], path));
        auto& model = w->models[&bones[path]];
        model.end = bones[path].pose;
        model.seam = nullptr;
        model.accessory = i;
    }
}
void ResetCounters() {
    calls.clear(); logs.clear(); stops = stop_event = stop_playing = posted_event = 0;
    stop_object = posted_object = 0; stop_action = stop_curve = -1;
    stop_fade = interrupts = mono_posts = 0;
}
void NewWorld(int hash) {
    delete w;
    w = new World;
    side_hash = hash;
    perform = hash == kRight ? "CharIntPerform_Jsspsi_SpdashR" : "CharIntPerform_Jsspsi_SpdashL";
    w->rider.root = &w->rider_root;
    w->mount.root = &w->mount_root;
    for (int i = 0; i < 2; ++i) {
        w->sides[i].root = &w->side_roots[i];
        w->sides[i].current = {i ? -96389345 : -1924790391, .05f};
    }
    w->rider.current = {hash, 0};
    w->mount.current = {kMount, 0};
    const int side = hash == kRight ? 0 : 1;
    AddBones(w->rider_bones, w->rider_root, data.rider, &data.rider_seam[side], side);
    AddBones(w->mount_bones, w->mount_root, data.mount, &data.mount_seam, -1);
    moving = true; move_mode = g_dash_mode;
    sound_object = 0x12345678abcdef12ULL; sound_result = 1;
    sound_lookup_fails = sound_stop_fails = wrong_audio_component = false;
    mount_enter_u = 0; failing_write = nullptr;
    ResetCounters();
}
void SetQuality(bool quality) {
    std::string text = "[betterendfieldnext.actions]\nschema_version=3\nenabled=true\ncharacters=jsspsi\ndiagnostics=true\n";
    if (quality) text += "jsspsi_quality=true\n";
    CHECK(Configure(text.c_str()) == BE_Result_Ok && g_config.load()->jsspsi_quality == quality);
}
void Evaluate(AnimatorFake& a) {
    if (a.pending) {
        a.pending = false;
        if (a.pending_duration == 0) { a.current.time = a.pending_offset; a.transitioning = false; }
        else { a.next = a.current; a.next.time = a.pending_offset; a.transitioning = true; a.transition_frames = 4; }
        return;
    }
    a.current.time += kDelta / kLength;
    if (a.transitioning) {
        a.next.time += kDelta / kLength;
        if (--a.transition_frames == 0) { a.current = a.next; a.transitioning = false; }
    }
}
// One game frame: Animator evaluation writes native poses, then PreLate and TailLate.
void Step() {
    ++w->frame;
    if (w->mount.current.hash != kMount && w->rider.current.time >= mount_enter_u) w->mount.current = {kMount, 0};
    Evaluate(w->rider);
    Evaluate(w->mount);
    for (auto& [bone, model] : w->models) {
        if (model.constant) continue;
        const auto& animator = bone->root == &w->rider_root ? w->rider : w->mount;
        bone->pose = Native(model, animator.transitioning ? animator.next.time : animator.current.time);
    }
    PreLateDetour(&w->component, kDelta, nullptr);
    PoseTailDetour(&w->component, kDelta, nullptr);
}
void StartDash() {
    StartDetour(&w->component, 0, nullptr);
    CHECK(g_session.component == &w->component && g_session.profile == &kCharacters[2]);
}
bool QualityOwned() { return g_quality.component == &w->component; }
size_t Leases() {
    size_t held = 0;
    for (void* root : {static_cast<void*>(&w->rider_root), static_cast<void*>(&w->mount_root)}) {
        const auto token = registry->Acquire(root, "probe");
        if (!token) ++held; else CHECK(registry->Release(root, "probe", token));
    }
    return held;
}
// Max per-frame rotation step of the final seam-bone poses, split into the
// frames where the rider rewound and all other frames.
struct Steps { float rewind = 0, other = 0; int rewinds = 0; };
Steps RunLaps(int laps, bool sound) {
    Steps steps;
    std::map<TransformFake*, BonePose> previous;
    float last_time = w->rider.current.time;
    if (sound) AudioMonoDetour(&w->rider, kCharacters[2].start_sound_event, 0, nullptr);
    while (static_cast<int>(w->rider.durations.size()) < laps && w->frame < 100000) {
        Step();
        const bool rewound = w->rider.current.time < last_time;
        last_time = w->rider.current.time;
        steps.rewinds += rewound;
        for (auto& [bone, model] : w->models) {
            if (!model.seam) continue;
            if (previous.count(bone)) {
                const float step = Degrees(previous[bone].rotation, bone->pose.rotation);
                (rewound ? steps.rewind : steps.other) = std::max(rewound ? steps.rewind : steps.other, step);
            }
            previous[bone] = bone->pose;
        }
    }
    for (int i = 0; i < 40; ++i) Step(); // Land after the last rewind.
    return steps;
}
void CheckAccessoryOnLap(int side) {
    double phase = 0;
    CHECK(QualityPhase(w->rider.current.time, data.begin, data.target, phase));
    size_t checked = 0;
    for (auto& [bone, model] : w->models) {
        if (model.accessory < 0) continue;
        const auto expected = QualityAccessorySample(data, side, static_cast<uint16_t>(model.accessory), phase);
        CHECK(Degrees(bone->pose.rotation, expected.rotation) < 1e-3f && Distance(bone->pose.position, expected.position) < 1e-6f);
        ++checked;
    }
    CHECK(checked == data.accessory_count);
}
void EndDash() {
    moving = false; move_mode = g_grounded_mode;
    Step(); // PreLate cancels; TailLate starts the fade.
    CHECK(!g_session.component && interrupts == 1);
}
// Names and particle counts come from the 2026-10-18 Jsspsi probe, independent
// of the production profile. Keep unrelated slots present to detect overreach.
void AddEffects() {
    w->effects_available = true;
    for (int slot = 0; slot < 10; ++slot) {
        char name[96];
        std::snprintf(name, sizeof(name), "P_fxbat_jsspsi_sprint_dash_sp_start_%02d",
            slot == 0 ? 1 : slot == 1 ? 2 : slot);
        w->effects[slot].name = name;
    }
    for (int slot = 0; slot < 6; ++slot) w->effects[slot].count = 0;
    w->effects[13].name = "P_fxbat_jsspsi_sprint_dash_sp_Bip001_jnt";
    w->effects[13].count = 8;
    // Even a correct name in the wrong slot must not be held.
    w->effects[14].name = w->effects[13].name;
    for (int i = 0; i < 8; ++i) {
        auto& p = w->effects[13].particles[i];
        p.loop = i == 3;
        p.duration = 1.f + i * .5f;
    }
}
void CheckEffectClocks(unsigned held) {
    for (int slot = 0; slot < static_cast<int>(kEffectSlotCount); ++slot) {
        auto& effect = w->effects[slot];
        EffectDurationDetour(&effect, kDelta, nullptr);
        const float expected = held & (1u << slot) ? 0.f : kDelta;
        if (effect.last_delta != expected)
            std::fprintf(stderr, "effect clock slot=%d actual=%g expected=%g\n", slot, effect.last_delta, expected);
        CHECK(effect.last_delta == expected);
    }
}
void CheckTrail(bool held, int stops_expected = 0) {
    for (int i = 0; i < 8; ++i) {
        const auto& p = w->effects[13].particles[i];
        CHECK(p.loop == (held || i == 3));
        CHECK(p.duration == 1.f + i * .5f && p.stops == stops_expected);
        CHECK(p.plays == (held || stops_expected ? 1 : 0));
    }
}
void FirstEffectRewind() {
    while (w->rider.durations.empty()) {
        CheckEffectClocks(0);
        CheckTrail(false);
        CHECK(calls[EffectName] == 0 && calls[ParticleSetLoop] == 0);
        Step();
    }
    CHECK(g_session.animation_replays == 1);
}
// Exercise the real Start/PreLate/Duration/Cancel paths, including the first
// rewind boundary and restoration on release, configuration change or lost handle.
void QualityEffects(int exit_mode, int hash = kRight) {
    NewWorld(hash); SetQuality(true); AddEffects(); StartDash();
    FirstEffectRewind();
    CheckEffectClocks(0x203f);
    CheckTrail(true);
    CHECK(calls[ParticleSetLoop] == 8 && calls[ParticlePlay] == 8);
    RunLaps(3, false);
    CheckEffectClocks(0x203f);
    CheckTrail(true);
    if (exit_mode == 0) EndDash();
    else {
        if (exit_mode == 1) SetQuality(false);
        else w->handle_current = false;
        // Clock holds stop immediately, before the next PreLate cleanup.
        CheckEffectClocks(0);
        Step();
    }
    CHECK(!g_session.component);
    CheckEffectClocks(0);
    CheckTrail(false, 1);
    for (int i = 0; i < 40; ++i) Step();
    CHECK(pins == frees);
}
void QualityEffectsOff() {
    NewWorld(kRight); SetQuality(false); AddEffects(); StartDash();
    FirstEffectRewind();
    RunLaps(3, false);
    CheckEffectClocks(0);
    CheckTrail(false);
    CHECK(calls[EffectName] == 0 && calls[ParticleSetLoop] == 0);
    EndDash();
    CHECK(pins == frees);
}
void QualityEffectsIdentity() {
    NewWorld(kRight); SetQuality(true); AddEffects(); StartDash();
    w->effects[0].name = w->effects[13].name;
    w->effects[13].available = false;
    FirstEffectRewind();
    CheckEffectClocks(0x3e);
    CheckTrail(false);
    // Async creation is retried; path and prefab suffix handling is shared.
    w->effects[13].available = true;
    w->effects[13].name = "effects/P_fxbat_jsspsi_sprint_dash_sp_Bip001_jnt.prefab";
    Step();
    CheckEffectClocks(0x203e);
    CheckTrail(true);
    EndDash();
    CheckTrail(false, 1);
    for (int i = 0; i < 40; ++i) Step();
    CHECK(pins == frees);
}
// Off: no quality owner, lease, bone access or sound stop; crossfades keep the
// lightweight blend for rider and mount.
void OffMatchesLightweight() {
    NewWorld(kRight); SetQuality(false);
    StartDash();
    CHECK(!g_quality.component);
    RunLaps(3, true);
    EndDash();
    for (int i = 0; i < 40; ++i) Step();
    for (int id : {PoseFind, PoseGetLocal, PoseSetLocal, PoseIsChild, PoseFrameCount, JsspsiAudioObjectId, JsspsiStopSound, AudioMonoComponent})
        CHECK(calls[id] == 0);
    CHECK(Leases() == 0 && !g_quality.component && stops == 0 && mono_posts == 1);
    CHECK(w->rider.durations.size() == 3 && w->mount.durations.size() == 3);
    for (float d : w->rider.durations) CHECK(d == kJsspsiLoop.blend);
    for (float d : w->mount.durations) CHECK(d == kJsspsiLoop.blend);
    for (auto& [bone, model] : w->models) CHECK(bone->writes == 0);
    CHECK(!Logged("Jsspsi quality"));
}
// On: seam armed during the first lap, every rewind is a hard cut for rider and
// mount, the rendered seam bones stay continuous across it, the accessory lap
// starts at the first rewind, the start sound stops, and the release fades.
void OnSide(int hash) {
    NewWorld(hash); SetQuality(true);
    const int side = hash == kRight ? 0 : 1;
    const uint32_t pins_before = pins - frees;
    StartDash();
    CHECK(QualityOwned());
    AudioMonoDetour(&w->rider, kCharacters[2].start_sound_event, 0, nullptr);
    CHECK(g_quality.sound_object == sound_object && stops == 0);
    CHECK(mono_posts == 1 && posted_event == 1063030539u && posted_object == sound_object);
    CHECK(calls[JsspsiAudioObjectId] == 1);
    // First lap: accessory bones stay native until the first rewind request.
    while (w->rider.durations.empty()) {
        for (auto& [bone, model] : w->models) if (model.accessory >= 0) CHECK(bone->writes == 0);
        Step();
    }
    CHECK(Logged("rider bound side=") && Logged("mount bound seam=") && Logged("seam armed"));
    CHECK(Leases() == 2);
    CHECK(stops == 1 && stop_event == posted_event && stop_object == posted_object);
    CHECK(stop_action == 0 && stop_curve == 4 && stop_playing == 0 && stop_fade == 300);
    CHECK(Logged("dash sound stop requested") && Logged("result=1 ok=1"));
    CheckAccessoryOnLap(side);
    const Steps steps = RunLaps(5, false);
    CHECK(steps.rewinds >= 4);
    // A hard cut onto the split seam moves no more than ordinary playback does.
    CHECK(steps.rewind <= steps.other + .5f && steps.rewind < 6);
    for (float d : w->rider.durations) CHECK(d == 0);
    for (float d : w->mount.durations) CHECK(d == 0);
    CheckAccessoryOnLap(side);
    CHECK(g_quality.seam_frames > 0 && g_quality.accessory_frames > 0 && stops == 1);
    // Release: accessory bones ease from the held lap pose to native over 0.4 s.
    std::map<TransformFake*, BonePose> held;
    for (auto& [bone, model] : w->models) if (model.accessory >= 0) held[bone] = bone->pose;
    EndDash();
    CHECK(QualityOwned() && g_quality.fading);
    int frames = 1;
    float previous = 1;
    while (QualityOwned() && frames < 100) {
        const float fade = g_quality.fade;
        CHECK(fade < previous);
        previous = fade;
        for (auto& [bone, model] : w->models) {
            if (model.accessory < 0) continue;
            const float weight = fade * fade * (3 - 2 * fade);
            const auto native = Native(model, w->rider.current.time);
            const auto expected = PoseBlend(native, held[bone], weight);
            CHECK(Degrees(bone->pose.rotation, expected.rotation) < 1e-3f);
        }
        Step(); ++frames;
    }
    CHECK(!QualityOwned() && frames >= 24 && frames <= 26);
    CHECK(Leases() == 0 && pins - frees == pins_before && Logged("quality: released"));
    const uint32_t writes = calls[PoseSetLocal];
    for (int i = 0; i < 10; ++i) Step();
    CHECK(calls[PoseSetLocal] == static_cast<int>(writes));
}
// Another visual writer owns the rider root: nothing is written and both
// crossfades keep the lightweight blend.
void LeaseHeldByAnotherWriter() {
    NewWorld(kRight); SetQuality(true);
    const auto token = registry->Acquire(&w->rider_root, "camera");
    StartDash();
    RunLaps(2, true);
    CHECK(Logged("pose ownership unavailable") && Logged("rider binding rejected"));
    // No bone is written; the start sound is not a pose write and still stops.
    CHECK(calls[PoseSetLocal] == 0 && stops == 1);
    for (float d : w->rider.durations) CHECK(d == kJsspsiLoop.blend);
    for (float d : w->mount.durations) CHECK(d == kJsspsiLoop.blend);
    EndDash();
    for (int i = 0; i < 5; ++i) Step();
    CHECK(!QualityOwned() && registry->Owns(&w->rider_root, "camera", token));
    CHECK(registry->Release(&w->rider_root, "camera", token));
}
// No mount to rewind with the rider: the seam never arms, the lightweight blend
// stays for every rewind, and the accessory lap still runs.
void MountUnavailable() {
    NewWorld(kRight); SetQuality(true);
    w->mount_available = false;
    StartDash();
    RunLaps(3, false);
    CHECK(Logged("seam unavailable (mount not bound before the first half lap)"));
    for (float d : w->rider.durations) CHECK(d == kJsspsiLoop.blend);
    for (auto& [bone, model] : w->models) if (model.seam) CHECK(bone->writes == 0);
    CheckAccessoryOnLap(0);
    EndDash();
    while (QualityOwned()) Step();
    CHECK(Leases() == 0);
}
// The mount reaching its dash state well after the rider must not arm the
// seam halfway through a lap; one frame of lag still arms it.
void MountOutOfStep() {
    NewWorld(kRight); SetQuality(true);
    w->mount.current = {1, 0}; mount_enter_u = .12f;
    StartDash();
    const Steps late = RunLaps(3, false);
    CHECK(Logged("seam unavailable (rider and mount out of step"));
    for (float d : w->rider.durations) CHECK(d == kJsspsiLoop.blend);
    for (auto& [bone, model] : w->models) if (model.seam) CHECK(bone->writes == 0);
    CHECK(late.rewinds >= 2);
    EndDash();
    while (QualityOwned()) Step();

    NewWorld(kRight); SetQuality(true);
    w->mount.current = {1, 0}; mount_enter_u = kDelta / kLength * 1.5f;
    StartDash();
    const Steps steps = RunLaps(3, false);
    CHECK(Logged("seam armed") && !Logged("out of step"));
    for (float d : w->rider.durations) CHECK(d == 0);
    CHECK(steps.rewind <= steps.other + .5f);
    EndDash();
    while (QualityOwned()) Step();
}
// A failed seam write ends the seam and the hard cut for the rest of the dash.
void SeamWriteFails() {
    NewWorld(kLeft); SetQuality(true);
    StartDash();
    while (w->rider.durations.empty()) Step();
    CHECK(g_quality.seam_armed);
    for (auto& [bone, model] : w->models) if (model.seam && bone->root == &w->rider_root) { failing_write = bone; break; }
    const size_t before = w->rider.durations.size();
    RunLaps(3, false);
    CHECK(Logged("seam unavailable (bone write failed)") && g_quality.seam_failed);
    for (size_t i = before; i < w->rider.durations.size(); ++i) CHECK(w->rider.durations[i] == kJsspsiLoop.blend);
    CHECK(g_quality.accessory_frames > 0); // The accessory lap is independent.
    failing_write = nullptr;
    EndDash();
    while (QualityOwned()) Step();
    CHECK(Leases() == 0);
}
// Too many bones absent from this rig: the rider binding is rejected and the
// dash runs exactly as the lightweight loop.
void MissingBonesRejected() {
    NewWorld(kLeft); SetQuality(true);
    std::string path;
    for (uint16_t i = 0; i < data.rider_seam[1].count / 3; ++i) {
        CHECK(QualityPath(data.rider, data.rider_seam[1].bones[i].node, path));
        w->missing.insert(path);
    }
    StartDash();
    RunLaps(2, false);
    CHECK(Logged("rider binding rejected") && calls[PoseSetLocal] == 0 && Leases() == 0);
    for (float d : w->rider.durations) CHECK(d == kJsspsiLoop.blend);
    EndDash();
}
// A channel native animation leaves alone must not accumulate the correction,
// and the release puts its native value back.
void ConstantChannelDoesNotAccumulate() {
    NewWorld(kRight); SetQuality(true);
    TransformFake* fixed = nullptr;
    for (auto& [bone, model] : w->models) if (model.seam && bone->root == &w->rider_root) { fixed = bone; model.constant = true; break; }
    const BonePose native = fixed->pose;
    const auto& seam = *w->models[fixed].seam;
    StartDash();
    RunLaps(4, false);
    for (int i = 0; i < 30; ++i) {
        Step();
        const float phase = std::clamp((w->rider.current.time - data.target) / (data.begin - data.target), 0.f, 1.f);
        const auto expected = QualitySeamApply(native, seam, QualitySeamShare(phase, true));
        CHECK(Degrees(fixed->pose.rotation, expected.rotation) < 1e-3f && Distance(fixed->pose.position, expected.position) < 1e-6f);
    }
    EndDash();
    CHECK(Degrees(fixed->pose.rotation, native.rotation) < 1e-4f && Distance(fixed->pose.position, native.position) < 1e-7f);
    while (QualityOwned()) Step();
}
// Missing contracts, failed lookups and unrelated posts leave native audio
// alone. A direct AnimatorMono post needs no Entity PostEvent interception.
void SoundCaptureAndFallback() {
    for (int mode = 0; mode < 8; ++mode) {
        NewWorld(kRight); SetQuality(true);
        const auto object_method = g_methods[JsspsiAudioObjectId].resolved;
        const auto stop_method = g_methods[JsspsiStopSound].resolved;
        if (mode == 0) g_methods[JsspsiAudioObjectId].resolved = {};
        if (mode == 1) g_methods[JsspsiStopSound].resolved = {};
        if (mode == 2) sound_object = 0;
        if (mode == 3) sound_object = UINT64_MAX;
        if (mode == 4) sound_lookup_fails = true;
        if (mode == 5) wrong_audio_component = true;
        StartDash();
        const uint32_t event = mode == 6 ? 12345 : 1063030539u;
        if (mode == 7) {
            std::thread worker([&] { AudioMonoDetour(&w->rider, event, 0, nullptr); });
            worker.join();
        } else AudioMonoDetour(&w->rider, event, 0, nullptr);
        CHECK(mono_posts == 1 && posted_event == event && !g_quality.sound_object);
        if (mode == 6 || mode == 7) CHECK(calls[AudioMonoComponent] == 0 && calls[JsspsiAudioObjectId] == 0);
        RunLaps(2, false);
        CHECK(stops == 0 && Logged("dash sound object not captured"));
        CHECK(g_quality.seam_frames > 0 && g_quality.accessory_frames > 0);
        EndDash();
        while (QualityOwned()) Step();
        g_methods[JsspsiAudioObjectId].resolved = object_method;
        g_methods[JsspsiStopSound].resolved = stop_method;
    }
    for (int mode = 0; mode < 3; ++mode) {
        NewWorld(kRight); SetQuality(true); StartDash();
        AudioMonoDetour(&w->rider, 1063030539u, 0, nullptr);
        const uint64_t captured = sound_object;
        // A duplicate post or later lookup must not replace the captured id.
        sound_object = 123;
        AudioMonoDetour(&w->rider, 1063030539u, 0, nullptr);
        CHECK(calls[JsspsiAudioObjectId] == 1 && stops == 0);
        if (mode == 1) sound_result = 2;
        if (mode == 2) sound_stop_fails = true;
        while (w->rider.durations.empty()) Step();
        CHECK(stops == 1 && stop_event == 1063030539u && stop_object == captured);
        CHECK(stop_action == 0 && stop_curve == 4 && stop_playing == 0 && stop_fade == 300);
        CHECK(Logged(mode == 0 ? "result=1 ok=1" : "ok=0"));
        AudioMonoDetour(&w->rider, 1063030539u, 0, nullptr);
        RunLaps(3, false);
        CHECK(stops == 1 && calls[JsspsiAudioObjectId] == 1);
        EndDash();
        while (QualityOwned()) Step();
    }
}
// Replace a live animator or its root immediately before the first rewind.
// The old root remains alive and leased, but only the current pair may hard cut.
void RootReplacement(int mode, bool before_rewind = true) {
    NewWorld(kRight); SetQuality(true);
    TransformFake* fixed = nullptr;
    for (auto& [bone, model] : w->models) if (model.seam && bone->root == &w->rider_root) {
        fixed = bone; model.constant = true; break;
    }
    CHECK(fixed);
    const BonePose native = fixed->pose;
    StartDash();
    while (before_rewind ? w->rider.current.time + kDelta / kLength < data.begin : !g_quality.seam_armed) Step();
    CHECK(g_quality.seam_armed && QualitySeamArmed());
    CHECK(w->rider.durations.empty() && w->mount.durations.empty());
    CHECK(QualityRootOwned(g_quality.rider) && QualityRootOwned(g_quality.mount));
    TransformFake fresh_root;
    AnimatorFake fresh_animator;
    std::map<std::string, TransformFake> fresh_bones;
    replacement_root = &fresh_root; replacement_bones = &fresh_bones;
    AddBones(fresh_bones, fresh_root, mode < 2 ? data.rider : data.mount,
        mode < 2 ? &data.rider_seam[0] : &data.mount_seam, mode < 2 ? 0 : -1);
    fresh_animator.root = &fresh_root;
    fresh_animator.current = mode < 2 ? w->rider.current : w->mount.current;
    if (mode == 0) w->rider.root = &fresh_root;
    if (mode == 1) replacement_rider = &fresh_animator;
    if (mode == 2) w->mount.root = &fresh_root;
    if (mode == 3) replacement_mount = &fresh_animator;
    auto& rider = replacement_rider ? *replacement_rider : w->rider;
    auto& mount = replacement_mount ? *replacement_mount : w->mount;
    ++w->frame; Evaluate(rider); Evaluate(mount);
    CHECK(w->rider_root.alive && w->mount_root.alive && w->rider.alive && w->mount.alive);
    CHECK(QualityRootOwned(g_quality.rider) && QualityRootOwned(g_quality.mount));
    const auto seam_frames = g_quality.seam_frames;
    const int writes = calls[PoseSetLocal];
    PreLateDetour(&w->component, kDelta, nullptr);
    CHECK(calls[PoseSetLocal] == writes);
    if (before_rewind) {
        CHECK(rider.durations.size() == 1 && mount.durations.size() == 1);
        std::printf("replacement=%d rider=%.3f mount=%.3f old_leases=1\n", mode, rider.durations.back(), mount.durations.back());
        CHECK(rider.durations.back() == kJsspsiLoop.blend && mount.durations.back() == kJsspsiLoop.blend);
        CHECK(!g_quality.seam_armed && g_quality.seam_failed);
    } else CHECK(rider.durations.empty() && mount.durations.empty());
    PoseTailDetour(&w->component, kDelta, nullptr);
    CHECK(!QualitySeamArmed());
    if (mode < 2) CHECK(!QualityOwned());
    else {
        CHECK(g_quality.seam_failed && g_quality.seam_frames == seam_frames);
        CHECK(Degrees(fixed->pose.rotation, native.rotation) < 1e-4f && Distance(fixed->pose.position, native.position) < 1e-7f);
    }
    for (const auto& [path, bone] : fresh_bones) CHECK(bone.writes == 0);
    CHECK(fresh_root.writes == 0);
    Cancel("replacement test", false); ReleaseLoopQuality(false);
    CHECK(Leases() == 0);
    replacement_rider = replacement_mount = nullptr;
    replacement_root = nullptr; replacement_bones = nullptr;
}
// Turning the option off mid-dash ends the session (configuration changed) and
// the owner without a fade; a new dash during a fade replaces the old owner;
// a component release frees everything at once.
void LifecycleEdges() {
    NewWorld(kRight); SetQuality(true);
    TransformFake* fixed_seam = nullptr;
    TransformFake* fixed_accessory = nullptr;
    for (auto& [bone, model] : w->models) {
        if (!fixed_seam && model.seam && bone->root == &w->rider_root) { fixed_seam = bone; model.constant = true; }
        if (!fixed_accessory && model.accessory >= 0) { fixed_accessory = bone; model.constant = true; }
    }
    const BonePose seam_native = fixed_seam->pose, accessory_native = fixed_accessory->pose;
    StartDash();
    RunLaps(2, false);
    CHECK(fixed_seam->writes > 0 && fixed_accessory->writes > 0);
    SetQuality(false);
    Step();
    CHECK(!g_session.component && !QualityOwned() && Leases() == 0);
    // Channels native animation never rewrites get their native values back.
    CHECK(Degrees(fixed_seam->pose.rotation, seam_native.rotation) < 1e-4f && Distance(fixed_seam->pose.position, seam_native.position) < 1e-7f);
    CHECK(Degrees(fixed_accessory->pose.rotation, accessory_native.rotation) < 1e-4f);

    NewWorld(kRight); SetQuality(true);
    StartDash();
    RunLaps(2, false);
    EndDash();
    CHECK(g_quality.fading);
    moving = true; move_mode = g_dash_mode;
    w->rider.current.time = 0;
    w->mount.current.time = 0;
    StartDash();
    CHECK(QualityOwned() && !g_quality.fading && !g_quality.rider.root && Leases() == 0);
    RunLaps(1, false);
    CHECK(Leases() == 2);
    g_release = ReleaseFake;
    ReleaseDetour(&w->component, true, nullptr);
    CHECK(!g_session.component && !QualityOwned() && Leases() == 0);

    // Worker-thread shutdown: no Unity call, lease returned.
    NewWorld(kRight); SetQuality(true);
    StartDash();
    RunLaps(1, false);
    ResetCounters();
    g_stopping = true;
    ReleaseLoopQuality(false);
    g_stopping = false;
    CHECK(calls.empty() && Leases() == 0);
    Cancel("test", false);
}
void ConfigurationAndProfile() {
    auto parse = [](const char* text) { CHECK(Configure(text) == BE_Result_Ok); return g_config.load(); };
    CHECK(!parse("schema_version=3\nenabled=true\n")->jsspsi_quality);
    CHECK(parse("schema_version=3\nenabled=true\njsspsi_quality=1\n")->jsspsi_quality);
    CHECK(parse("schema_version=3\nenabled=true\njsspsi_quality=0\n")->enabled);
    CHECK(!parse("schema_version=3\nenabled=true\njsspsi_quality=yes\n")->enabled);
    CHECK(kCharacters[2].quality == &JsspsiQuality::kData && kCharacters[2].start_sound_event == 1063030539u);
    CHECK(!kCharacters[0].quality && !kCharacters[1].quality && !kCharacters[0].start_sound_event);
    CHECK(std::strcmp(g_methods[JsspsiAudioObjectId].desc.method_name, "GetAudioObjectId") == 0 && JsspsiAudioObjectId > JsspsiTmpAnimator);
    CHECK(std::strcmp(g_methods[JsspsiStopSound].desc.method_name, "ExecuteActionOnEvent") == 0 && JsspsiStopSound > JsspsiTmpAnimator);
}
}
int main(int argc, char** argv) {
    Motion::PoseLeaseRegistry leases;
    registry = &leases;
    BE_HostApiV1 host{};
    host.runtime_invoke = &InvokeFake;
    host.object_unbox = &UnboxFake;
    host.copy_managed_string = &CopyStringFake;
    host.gchandle_free = &FreeFake;
    host.gchandle_new = &PinFake;
    host.field_get_value_object = &FieldFake;
    host.log = &LogFake;
    g_host = &host;
    const BE_PoseLeaseApiV1 api{1, Acquire, Owns, ReleaseLease};
    g_pose_leases = &api;
    g_pose_lease_ready = true;
    g_game_thread = GetCurrentThreadId();
    for (int i = 0; i < MethodCount; ++i)
        g_methods[i].resolved.method_info = reinterpret_cast<void*>(static_cast<uintptr_t>(i + 1));
    for (int i = 0; i < FieldCount; ++i)
        g_fields[i].resolved.field_info = reinterpret_cast<void*>(static_cast<uintptr_t>(i + 1));
    g_object_class = ClassFake; g_class_method = MethodFake; g_class_parent = ParentFake;
    g_quality_string_new = StringNewFake;
    g_start = StartFake; g_pre_late = TickFake; g_pose_tail = TickFake; g_interrupt = InterruptFake;
    g_audio_mono = MonoPostFake; g_audio_entity = EntityPostFake;
    g_effect_duration = DurationFake;
    for (int i = 0; i < 2; ++i) { g_hash_fields[i] = kHashFields[i]; g_locomotion_fields[i] = kHashFields[i + 2]; }
    g_dash_mode = 20; g_grounded_mode = 1; g_sprint = 2; g_audio_none = 0;
    g_blend_style = 1; g_blend_interrupt = 2;
    struct Case { const char* name; void (*run)(); };
    const Case cases[]{
        {"config", ConfigurationAndProfile},
        {"off", OffMatchesLightweight},
        {"effects", [] { QualityEffects(0); }},
        {"effects_left", [] { QualityEffects(0, kLeft); }},
        {"effects_off", QualityEffectsOff},
        {"effects_disable", [] { QualityEffects(1); }},
        {"effects_invalid", [] { QualityEffects(2); }},
        {"effects_identity", QualityEffectsIdentity},
        {"right", [] { OnSide(kRight); }},
        {"left", [] { OnSide(kLeft); }},
        {"lease", LeaseHeldByAnotherWriter},
        {"mount", MountUnavailable},
        {"missing", MissingBonesRejected},
        {"step", MountOutOfStep},
        {"writefail", SeamWriteFails},
        {"constant", ConstantChannelDoesNotAccumulate},
        {"sound", SoundCaptureAndFallback},
        {"rider_root", [] { RootReplacement(0); }},
        {"rider_animator", [] { RootReplacement(1); }},
        {"mount_root", [] { RootReplacement(2); }},
        {"mount_animator", [] { RootReplacement(3); }},
        {"mount_animator_tail", [] { RootReplacement(3, false); }},
        {"lifecycle", LifecycleEdges},
    };
    int ran = 0;
    for (const auto& item : cases) {
        if (argc > 1 && std::strcmp(argv[1], item.name) != 0) continue;
        item.run();
        if (g_session.component) Cancel("test end", false);
        ReleaseLoopQuality(false);
        ++ran;
    }
    CHECK(ran > 0);
    delete w;
    g_host = nullptr;
    std::printf("Jsspsi quality: %d scenario(s) passed (off path, effects, both sides, hard-cut seam, accessory lap, fade, lease, mount, mount out of step, write failure, missing bones, constant channel, sound, root replacement, lifecycle).\n", ran);
}
