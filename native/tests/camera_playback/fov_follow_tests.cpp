#include "../../modules/camera/module.cpp"
#include "eiem_body_fake.h"
#include "test_support.h"
#include <array>
#include <limits>

using namespace BetterEndfield::CameraModule;

namespace {
enum Method : uintptr_t { Main = 1, GameObject, Orthographic, Destroyed,
    Character, ModelCom, ModelGo, Transform, Position };
struct Object {
    bool alive = true, orthographic = false;
    Object* go = nullptr;
    Object* component = nullptr;
    Object* model = nullptr;
    Object* transform = nullptr;
    Vector3 position{};
};
Object camera, brain, camera_go, other_go, character, model_com, model, transform;
Object* current_character = &character;
float delivered_fov = 0;
template<class T> void* Box(T value) { static thread_local T result; result = value; return &result; }
void* BE_CALL InvokeFake(void*, const void* method, void* instance, void** args, void** exception) {
    *exception = nullptr;
    auto* object = static_cast<Object*>(instance);
    switch (reinterpret_cast<uintptr_t>(method)) {
    case Main: return &camera;
    case GameObject: return object->go;
    case Orthographic: return Box(object->orthographic);
    case Destroyed: return Box(!args[0] || !static_cast<Object*>(args[0])->alive);
    case Character: return current_character;
    case ModelCom: return object->component;
    case ModelGo: return object->model;
    case Transform: return object->transform;
    case Position: return Box(object->position);
    default: *exception = reinterpret_cast<void*>(1); return nullptr;
    }
}
void* BE_CALL UnboxFake(void*, void* value) { return value; }
void __fastcall PushFake(void*, void* state, void*) {
    CHECK(ReadBytes(state, g_state_layout.lens, &delivered_fov, sizeof(delivered_fov)));
}
void Bind(const char* key, Method id) {
    for (auto& contract : g_contracts) if (std::string_view(contract.key) == key) {
        contract.method_info = reinterpret_cast<void*>(static_cast<uintptr_t>(id));
        contract.resolved = true;
        return;
    }
    CHECK(false);
}
}

int main() {
    static BE_HostApiV1 host{};
    host.runtime_invoke = InvokeFake;
    host.object_unbox = UnboxFake;
    g_host = &host;
    Bind("unity.camera.main", Main);
    Bind("unity.component.game_object", GameObject);
    Bind("unity.camera.orthographic.get", Orthographic);
    Bind("unity.object.op_equality", Destroyed);
    Bind("player_controller.get_main_character", Character);
    Bind("entity.get_model_com", ModelCom);
    Bind("base_model_component.get_model_go", ModelGo);
    Bind("unity.game_object.transform", Transform);
    Bind("unity.transform.position.get", Position);
    camera.go = brain.go = &camera_go;
    character.component = &model_com;
    model_com.model = &model;
    model.transform = &transform;
    g_state_layout.ready = true;
    g_state_layout.lens = 32;
    g_state_layout.lens_field_of_view = 0;
    g_original_push_state = PushFake;
    std::array<uint8_t, 128> state{};
    const float native_fov = 63;
    CHECK(WriteBytes(state.data(), 32, &native_fov, sizeof(native_fov)));
    auto original_state = state;
    g_global_fov_enabled = true;
    g_global_fov = 81;
    DetourPushState(&brain, state.data(), nullptr);
    CHECK(delivered_fov == 81 && state == original_state);
    g_global_fov_enabled = false;
    DetourPushState(&brain, state.data(), nullptr);
    CHECK(delivered_fov == 63 && state == original_state);
    g_global_fov_enabled = true;
    brain.go = &other_go;
    DetourPushState(&brain, state.data(), nullptr);
    CHECK(delivered_fov == 63);
    brain.go = &camera_go;
    camera.orthographic = true;
    DetourPushState(&brain, state.data(), nullptr);
    CHECK(delivered_fov == 63);
    camera.orthographic = false;
    // Other camera modes own their FOV, so the ordinary override stays inactive.
    for (int mode = 0; mode < 2; ++mode) {
        g_free_camera_active = mode == 0;
        g_first_person_active = mode == 1;
        ScopedGlobalFovState override(&brain, state.data());
        CHECK(state == original_state);
    }
    g_free_camera_active = false;
    g_first_person_active = false;
    g_global_fov = std::numeric_limits<float>::quiet_NaN();
    DetourPushState(&brain, state.data(), nullptr);
    CHECK(delivered_fov == 63);
    auto config = ParseConfiguration("enabled=true\nglobal_fov_enabled=true\nglobal_fov=200\nfree_camera_follow_character=true\n");
    CHECK(config.global_fov_enabled && config.global_fov == 150 && config.free_camera_follow_character);
    CHECK(!ParseConfiguration("").global_fov_enabled && !ParseConfiguration("").free_camera_follow_character);

    // Exercise the production follow path, including smoothing origin, rebinding,
    // null character windows and playback transitions.
    g_free_camera_follow_character = true;
    g_free_target.position = {10, 20, 30};
    g_free_smoothed.position = {8, 18, 28};
    g_free_view.position = {9, 19, 29};
    g_free_view.rotation = {0, 0, 0, 1};
    g_free_follow_anchor.Reset();
    FollowCharacterTranslation();
    transform.position = {2, 3, 4};
    FollowCharacterTranslation();
    CHECK(g_free_target.position.x == 12 && g_free_target.position.y == 23 && g_free_target.position.z == 34);
    CHECK(g_free_smoothed.position.x == 10 && g_free_view.position.x == 11);
    CHECK(g_free_view.rotation.w == 1);
    current_character = nullptr;
    FollowCharacterTranslation();
    current_character = &character;
    transform.position = {100, 100, 100};
    FollowCharacterTranslation();
    CHECK(g_free_target.position.x == 12); // missing pose never accumulates a jump
    transform.position.x = 101;
    FollowCharacterTranslation();
    CHECK(g_free_target.position.x == 13);
    transform.position.x = 1000;
    FollowCharacterTranslation();
    CHECK(g_free_target.position.x == 13); // teleport reanchors
    g_playback = FreePlayback::Keyframes;
    transform.position.x = 1001;
    FollowCharacterTranslation();
    g_playback = FreePlayback::None;
    FollowCharacterTranslation();
    CHECK(g_free_target.position.x == 13); // no accumulated motion after playback
    Object replacement_transform;
    replacement_transform.position = {3, 4, 5};
    model.transform = &replacement_transform;
    FollowCharacterTranslation();
    CHECK(g_free_target.position.x == 13); // model replacement reanchors
    replacement_transform.position.x = std::numeric_limits<float>::infinity();
    FollowCharacterTranslation();
    CHECK(std::isfinite(g_free_target.position.x));
    g_free_camera_follow_character = false;
    FollowCharacterTranslation();
    replacement_transform.position = {};
    g_free_camera_follow_character = true;
    FollowCharacterTranslation();
    CHECK(g_free_target.position.x == 13);
    std::cout << checks << " FOV/follow regression checks passed\n";
}
