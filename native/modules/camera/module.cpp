#include "BetterEndfield/ModuleApi.h"
#include "BetterEndfield/vmd_parser.h"
#include "first_person_mesh.h"
#include "first_person_retry.h"

#include <Windows.h>

#include <algorithm>
#include <atomic>
#include <cctype>
#include <cmath>
#include <cstdint>
#include <cstdio>
#include <cstdlib>
#include <cstring>
#include <mutex>
#include <string>
#include <string_view>
#include <thread>
#include <unordered_map>
#include <unordered_set>
#include <vector>

namespace BetterEndfield::CameraModule {
namespace {

constexpr char kModuleId[] = "betterendfield.camera";

enum class ModuleState : uint8_t {
    Created,
    Resolving,
    Ready,
    Active,
    Disabled,
    ContractMismatch,
    Failed,
    Stopped,
};

struct CameraConfiguration {
    int schema_version = 0;
    bool enabled = false;
    bool free_camera_enabled = false;
    bool disable_dither_enabled = false;
    bool pause_enabled = false;
    bool first_person_camera_enabled = false;
    bool first_person_hide_head = true;
    bool first_person_fill_neck_hole = true;
    bool diagnostics = true;
    float movement_speed = 5.0f;
    float field_of_view = 60.0f;
    float first_person_fov = 75.0f;
    float first_person_neck_plug_scale = 1.0f;
    int toggle_key = '9';
    int pause_key = '8';
    int first_person_key = VK_OEM_MINUS;
    // Free camera look, motion presets, keyframes and VMD camera playback.
    bool mouse_look = true;
    bool mouse_invert_y = false;
    float mouse_sensitivity = 0.1f;
    float smoothing = 0.3f;
    int motion_preset = 0;
    float motion_speed = 1.0f;
    float orbit_speed = 20.0f;
    float motion_duration = 0.0f;
    float motion_target_height = 1.2f;
    float keyframe_segment_seconds = 3.0f;
    bool keyframe_loop = false;
    std::string vmd_camera_file;
    float vmd_camera_scale = 0.07f;
    float vmd_camera_fov_bias = 5.0f;
    bool vmd_camera_loop = false;
    int roll_left_key = VK_NUMPAD7;
    int roll_right_key = VK_NUMPAD9;
    int fov_wide_key = VK_NUMPAD1;
    int fov_narrow_key = VK_NUMPAD3;
    int view_reset_key = VK_NUMPAD5;
    int motion_key = VK_NUMPAD8;
    int keyframe_add_key = VK_NUMPAD0;
    int keyframe_play_key = VK_NUMPAD2;
    int keyframe_clear_key = VK_NUMPAD4;
    int vmd_play_key = VK_NUMPAD6;
};

struct Vector3 {
    float x = 0.0f;
    float y = 0.0f;
    float z = 0.0f;
};

struct Quaternion {
    float x = 0.0f;
    float y = 0.0f;
    float z = 0.0f;
    float w = 1.0f;
};

struct MethodContract {
    const char* key;
    BE_MethodDescriptorV1 descriptor;
    // Some managed signatures differ only in how IL2CPP reports a byref
    // parameter. The alternate text is only consulted when the primary
    // descriptor does not resolve.
    const char* alternate_parameter_types = nullptr;
    void* pointer = nullptr;
    const void* method_info = nullptr;
    bool resolved = false;
};

struct FieldContract {
    const char* key;
    BE_FieldDescriptorV1 descriptor;
    BE_ResolvedFieldV1 resolved{};
    bool ready = false;
};

const BE_HostApiV1* g_host = nullptr;
std::atomic<ModuleState> g_state{ModuleState::Created};
std::atomic_bool g_free_camera_enabled{false};
std::atomic_bool g_disable_dither_enabled{false};
std::atomic_bool g_pause_enabled{false};
std::atomic_bool g_first_person_camera_enabled{false};
std::atomic_bool g_first_person_hide_head{true};
std::atomic_bool g_first_person_fill_neck_hole{true};
std::atomic_bool g_diagnostics_enabled{true};
std::atomic<float> g_movement_speed{5.0f};
std::atomic<float> g_field_of_view{60.0f};
std::atomic<float> g_first_person_fov{75.0f};
std::atomic<float> g_first_person_neck_plug_scale{1.0f};
std::atomic_int g_toggle_key{'9'};
std::atomic_int g_pause_key{'8'};
std::atomic_int g_first_person_key{VK_OEM_MINUS};
std::atomic_bool g_mouse_look_enabled{true};
std::atomic_bool g_mouse_invert_y{false};
std::atomic<float> g_mouse_sensitivity{0.1f};
std::atomic<float> g_free_smoothing{0.3f};
std::atomic_int g_motion_preset{0};
std::atomic<float> g_motion_speed{1.0f};
std::atomic<float> g_orbit_speed{20.0f};
std::atomic<float> g_motion_duration{0.0f};
std::atomic<float> g_motion_target_height{1.2f};
std::atomic<float> g_keyframe_segment_seconds{3.0f};
std::atomic_bool g_keyframe_loop{false};
std::atomic<float> g_vmd_camera_scale{0.07f};
std::atomic<float> g_vmd_camera_fov_bias{5.0f};
std::atomic_bool g_vmd_camera_loop{false};
std::mutex g_vmd_path_mutex;
std::string g_vmd_camera_file;
std::atomic_int g_roll_left_key{VK_NUMPAD7};
std::atomic_int g_roll_right_key{VK_NUMPAD9};
std::atomic_int g_fov_wide_key{VK_NUMPAD1};
std::atomic_int g_fov_narrow_key{VK_NUMPAD3};
std::atomic_int g_view_reset_key{VK_NUMPAD5};
std::atomic_int g_motion_key{VK_NUMPAD8};
std::atomic_int g_keyframe_add_key{VK_NUMPAD0};
std::atomic_int g_keyframe_play_key{VK_NUMPAD2};
std::atomic_int g_keyframe_clear_key{VK_NUMPAD4};
std::atomic_int g_vmd_play_key{VK_NUMPAD6};

using CameraTickFn = void(__fastcall*)(void* instance, void* method);
CameraTickFn g_original_camera_tick = nullptr;

using TailLateTickFn = void(__fastcall*)(void* instance, float deltaTime, void* method);
TailLateTickFn g_original_tail_late_tick = nullptr;

using TimeUnscaledDeltaFn = float(__fastcall*)(void* method);
TimeUnscaledDeltaFn g_original_time_unscaled_delta = nullptr;

// CinemachineBrain::PushStateToUnityCamera(Cinemachine.CameraState& state) is the
// last stage of the Cinemachine pipeline. Rewriting the state there is the only
// place where the pose cannot be overwritten later in the frame.
using PushStateFn = void(__fastcall*)(void* instance, void* state, void* method);
PushStateFn g_original_push_state = nullptr;
bool g_push_state_hook_ready = false;
std::atomic_uint64_t g_push_state_calls{0};
std::atomic_uint64_t g_push_state_patches{0};

bool g_free_camera_contract_ready = false;
bool g_dither_contract_ready = false;
bool g_time_heartbeat_contract_ready = false;
bool g_first_person_contract_ready = false;

bool g_free_camera_active = false;
std::atomic_bool g_toggle_request{false};
std::atomic_bool g_pause_request{false};
std::atomic_bool g_force_exit_request{false};
std::atomic_bool g_motion_request{false};
std::atomic_bool g_keyframe_add_request{false};
std::atomic_bool g_keyframe_play_request{false};
std::atomic_bool g_keyframe_clear_request{false};
std::atomic_bool g_vmd_play_request{false};
// Mouse look input, accumulated by the input thread's low-level mouse hook.
std::atomic_bool g_free_camera_running{false};
std::atomic_bool g_mouse_capture{false};
std::atomic_int g_mouse_dx{0};
std::atomic_int g_mouse_dy{0};
std::atomic_int g_mouse_wheel{0};

std::atomic_bool g_first_person_active{false};
std::atomic_bool g_first_person_toggle_request{false};
std::atomic_bool g_first_person_exit_request{false};

std::atomic_bool g_input_thread_stop{false};
std::thread g_input_thread;

void* g_active_camera = nullptr;
uint32_t g_active_camera_root = 0;
Vector3 g_original_position{};
float g_original_fov = 60.0f;
float g_original_time_scale = 1.0f;
bool g_changed_time_scale = false;

// ---------------------------------------------------------------------------
// First-person tuning constants. The eye anchor follows the head bone, so the
// two offsets below are the only artistic knobs: forward moves the eye towards
// the face, up moves it towards the brow.
// ---------------------------------------------------------------------------
constexpr float kFirstPersonEyeForward = 0.10f;
constexpr float kFirstPersonEyeUp = 0.06f;
constexpr float kFirstPersonNearClip = 0.05f;
constexpr size_t kMaxFieldOffset = 0x400;
constexpr int32_t kHumanBodyBonesHead = 11;

// Value type metadata offsets in IL2CPP are relative to the boxed layout, which
// carries an object header in front of the inline data. The header size is
// probed through UnityEngine.Vector3.x (the first field of a 12 byte struct)
// instead of being hardcoded, so the geometry below survives Unity upgrades.
int32_t g_value_type_header = 0;

struct CameraStateLayout {
    int32_t raw_position = -1;              // CameraState body relative
    int32_t raw_orientation = -1;
    int32_t position_correction = -1;
    int32_t orientation_correction = -1;
    int32_t lens = -1;                      // CameraState.Lens, body relative
    int32_t lens_field_of_view = -1;        // LensSettings body relative
    int32_t lens_near_clip = -1;
    int32_t lens_dutch = -1;
    bool ready = false;
};
CameraStateLayout g_state_layout;

BE_ResolvedClassV1 g_snapshot_controller_class{};
BE_ResolvedClassV1 g_animator_class{};
BE_ResolvedClassV1 g_skinned_mesh_renderer_class{};

// Renderer pointers inside a probe are only valid during the scan that produced
// them. Bound mesh patches keep GC handles separately because the game can
// rebuild character parts (clothes, hair, LOD levels) at any time.
struct HeadPartProbe {
    std::string object_name;
    std::string mesh_name;
    int32_t vertex_count = -1;
    bool skinned = false;
    bool matched = false;
    void* renderer = nullptr;
};

struct FirstPersonSession {
    void* character = nullptr;
    void* head = nullptr;
    uint32_t head_handle = 0;
    void* neck = nullptr;
    uint32_t neck_handle = 0;
    void* snapshot_controller = nullptr;
    uint32_t snapshot_handle = 0;
    bool head_hide_applied = false;
    Vector3 last_eye{};
    bool last_eye_valid = false;
};
FirstPersonSession g_first_person;

uint64_t g_first_person_pump_frames = 0;
uint64_t g_first_person_reassert_frames = 0;
bool g_first_person_head_logged = false;
bool g_first_person_health_warned = false;

MethodContract g_contracts[]{
    {"camera.process_dither",
        {"Gameplay.Beyond.dll", "Beyond.Gameplay.View", "CameraMono",
            "_ProcessDitherByPitch", nullptr, "System.Void", 0}},
    {"camera.force_clear_dither",
        {"Gameplay.Beyond.dll", "Beyond.Gameplay.View", "CameraMono",
            "ForceClearDither", nullptr, "System.Void", 0}},
    {"camera_manager.tail_late_tick",
        {"Gameplay.Beyond.dll", "Beyond.Gameplay.View", "CameraManager",
            "TailLateTick", nullptr, "System.Void", 1}},
    {"player_controller.get_main_character",
        {"Gameplay.Beyond.dll", "Beyond.Gameplay.Core", "PlayerController",
            "GetMainCharacter", nullptr, nullptr, 0}},
    {"entity.get_model_com",
        {"Gameplay.Beyond.dll", "Beyond.Gameplay.Core", "Entity",
            "get_modelCom", nullptr, nullptr, 0}},
    {"base_model_component.get_model_go",
        {"Gameplay.Beyond.dll", "Beyond.Gameplay.View", "BaseModelComponent",
            "GetModelGo", nullptr, "UnityEngine.GameObject", 0}},
    {"cinemachine.push_state",
        {"Cinemachine.dll", "Cinemachine", "CinemachineBrain",
            "PushStateToUnityCamera", "Cinemachine.CameraState&", "System.Void", 1},
        "Cinemachine.CameraState"},
    {"snapshot.set_first_person",
        {"Gameplay.Beyond.dll", "Beyond.Gameplay.View", "SnapshotCameraController",
            "SetFirstPerson", "System.Boolean", "System.Void", 1}},
    {"snapshot.show_char",
        {"Gameplay.Beyond.dll", "Beyond.Gameplay.View", "SnapshotCameraController",
            "_ShowChar", nullptr, "System.Void", 0}},
    {"unity.camera.main",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Camera", "get_main",
            nullptr, "UnityEngine.Camera", 0}},
    {"unity.camera.fov.get",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Camera", "get_fieldOfView",
            nullptr, "System.Single", 0}},
    {"unity.camera.fov.set",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Camera", "set_fieldOfView",
            "System.Single", "System.Void", 1}},
    {"unity.component.transform",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Component", "get_transform",
            nullptr, "UnityEngine.Transform", 0}},
    {"unity.component.game_object",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Component", "get_gameObject",
            nullptr, "UnityEngine.GameObject", 0}},
    {"unity.game_object.transform",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "GameObject", "get_transform",
            nullptr, "UnityEngine.Transform", 0}},
    {"unity.game_object.get_component",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "GameObject", "GetComponent",
            "System.Type", "UnityEngine.Component", 1}},
    {"unity.game_object.find_with_tag",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "GameObject", "FindWithTag",
            "System.String", "UnityEngine.GameObject", 1}},
    {"unity.object.find_object_of_type",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Object", "FindObjectOfType",
            "System.Type|System.Boolean", "UnityEngine.Object", 2}},
    {"unity.object.name.get",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Object", "get_name",
            nullptr, "System.String", 0}},
    {"unity.skinned_mesh_renderer.shared_mesh.get",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer",
            "get_sharedMesh", nullptr, "UnityEngine.Mesh", 0}},
    {"unity.mesh.vertex_count.get",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "get_vertexCount",
            nullptr, "System.Int32", 0}},
    {"unity.object.destroy",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Object", "Destroy",
            "UnityEngine.Object", "System.Void", 1}},
    {"unity.mesh.bindposes.get",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", "get_bindposes",
            nullptr, "UnityEngine.Matrix4x4[]", 0}},
    {"unity.skinned_mesh_renderer.bones.get",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer",
            "get_bones", nullptr, "UnityEngine.Transform[]", 0}},
    {"unity.transform.parent.get",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "get_parent",
            nullptr, "UnityEngine.Transform", 0}},
    {"unity.object.op_equality",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Object", "op_Equality",
            "UnityEngine.Object|UnityEngine.Object", "System.Boolean", 2}},
    {"unity.animator.get_bone_transform",
        {"UnityEngine.AnimationModule.dll", "UnityEngine", "Animator",
            "GetBoneTransform", "UnityEngine.HumanBodyBones",
            "UnityEngine.Transform", 1}},
    {"unity.transform.position.get",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "get_position",
            nullptr, "UnityEngine.Vector3", 0}},
    {"unity.transform.position.set",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "set_position",
            "UnityEngine.Vector3", "System.Void", 1}},
    {"unity.transform.rotation.get",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "get_rotation",
            nullptr, "UnityEngine.Quaternion", 0}},
    {"unity.transform.rotation.set",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "set_rotation",
            "UnityEngine.Quaternion", "System.Void", 1}},
    {"unity.transform.forward",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "get_forward",
            nullptr, "UnityEngine.Vector3", 0}},
    {"unity.transform.right",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "get_right",
            nullptr, "UnityEngine.Vector3", 0}},
    {"unity.transform.up",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "get_up",
            nullptr, "UnityEngine.Vector3", 0}},
    {"unity.transform.child_count.get",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "get_childCount",
            nullptr, "System.Int32", 0}},
    {"unity.transform.get_child",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "GetChild",
            "System.Int32", "UnityEngine.Transform", 1}},
    {"unity.transform.find",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "Find",
            "System.String", "UnityEngine.Transform", 1}},
    {"unity.time.scale.get",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Time", "get_timeScale",
            nullptr, "System.Single", 0}},
    {"unity.time.scale.set",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Time", "set_timeScale",
            "System.Single", "System.Void", 1}},
    {"unity.time.unscaled_delta.get",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Time",
            "get_unscaledDeltaTime", nullptr, "System.Single", 0}},
};

FieldContract g_fields[]{
    {"unity.vector3.x",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Vector3", "x",
            "System.Single"}},
    {"cinemachine.state.raw_position",
        {"Cinemachine.dll", "Cinemachine", "CameraState", "RawPosition",
            "UnityEngine.Vector3"}},
    {"cinemachine.state.raw_orientation",
        {"Cinemachine.dll", "Cinemachine", "CameraState", "RawOrientation",
            "UnityEngine.Quaternion"}},
    {"cinemachine.state.position_correction",
        {"Cinemachine.dll", "Cinemachine", "CameraState", "PositionCorrection",
            "UnityEngine.Vector3"}},
    {"cinemachine.state.orientation_correction",
        {"Cinemachine.dll", "Cinemachine", "CameraState", "OrientationCorrection",
            "UnityEngine.Quaternion"}},
    {"cinemachine.state.lens",
        {"Cinemachine.dll", "Cinemachine", "CameraState", "Lens",
            "Cinemachine.LensSettings"}},
    {"cinemachine.lens.field_of_view",
        {"Cinemachine.dll", "Cinemachine", "LensSettings", "FieldOfView",
            "System.Single"}},
    {"cinemachine.lens.near_clip",
        {"Cinemachine.dll", "Cinemachine", "LensSettings", "NearClipPlane",
            "System.Single"}},
    {"cinemachine.lens.dutch",
        {"Cinemachine.dll", "Cinemachine", "LensSettings", "Dutch",
            "System.Single"}},
    {"snapshot.is_first_person",
        {"Gameplay.Beyond.dll", "Beyond.Gameplay.View", "SnapshotCameraController",
            "<isFirstPerson>k__BackingField", "System.Boolean"}},
};

MethodContract* Contract(std::string_view key) {
    for (auto& contract : g_contracts) {
        if (contract.key == key) {
            return &contract;
        }
    }
    return nullptr;
}

FieldContract* Field(std::string_view key) {
    for (auto& field : g_fields) {
        if (field.key == key) {
            return &field;
        }
    }
    return nullptr;
}

void Log(const std::string& message) {
    if (g_host && g_host->log) {
        g_host->log(g_host->context, kModuleId, message.c_str());
    }
}

void* Invoke(const MethodContract* method, void* instance, void** parameters) {
    if (!method || !method->method_info || !g_host || !g_host->runtime_invoke) {
        return nullptr;
    }
    void* exception = nullptr;
    void* result = g_host->runtime_invoke(g_host->context, method->method_info,
        instance, parameters, &exception);
    return exception ? nullptr : result;
}

bool InvokeVoid(const MethodContract* method, void* instance, void** parameters) {
    if (!method || !method->method_info || !g_host || !g_host->runtime_invoke) {
        return false;
    }
    void* exception = nullptr;
    g_host->runtime_invoke(g_host->context, method->method_info,
        instance, parameters, &exception);
    return exception == nullptr;
}

template <typename T>
bool Unbox(void* boxed, T& value) {
    if (!boxed || !g_host || !g_host->object_unbox) {
        return false;
    }
    void* raw = g_host->object_unbox(g_host->context, boxed);
    if (!raw) {
        return false;
    }
    std::memcpy(&value, raw, sizeof(T));
    return true;
}

bool GetValue(const MethodContract* method, void* instance, Vector3& value) {
    return Unbox(Invoke(method, instance, nullptr), value);
}

bool GetValue(const MethodContract* method, void* instance, float& value) {
    return Unbox(Invoke(method, instance, nullptr), value);
}

bool GetValue(const MethodContract* method, void* instance, int& value) {
    return Unbox(Invoke(method, instance, nullptr), value);
}

bool GetValue(const MethodContract* method, void* instance, bool& value) {
    return Unbox(Invoke(method, instance, nullptr), value);
}

bool SetValue(const MethodContract* method, void* instance, Vector3 value) {
    void* parameters[1]{&value};
    return InvokeVoid(method, instance, parameters);
}

bool SetValue(const MethodContract* method, void* instance, float value) {
    void* parameters[1]{&value};
    return InvokeVoid(method, instance, parameters);
}

Vector3 Add(Vector3 left, Vector3 right) {
    return {left.x + right.x, left.y + right.y, left.z + right.z};
}

Vector3 Subtract(Vector3 left, Vector3 right) {
    return {left.x - right.x, left.y - right.y, left.z - right.z};
}

Vector3 Scale(Vector3 value, float scale) {
    return {value.x * scale, value.y * scale, value.z * scale};
}

Vector3 Normalize(Vector3 value) {
    const float length = std::sqrt(
        value.x * value.x + value.y * value.y + value.z * value.z);
    return length > 0.0001f ? Scale(value, 1.0f / length) : Vector3{};
}

float Magnitude(Vector3 value) {
    return std::sqrt(value.x * value.x + value.y * value.y + value.z * value.z);
}

bool IsFinite(Vector3 value) {
    return std::isfinite(value.x) && std::isfinite(value.y) &&
        std::isfinite(value.z);
}

bool IsUnitQuaternion(Quaternion value) {
    if (!std::isfinite(value.x) || !std::isfinite(value.y) ||
        !std::isfinite(value.z) || !std::isfinite(value.w)) {
        return false;
    }
    const float length = value.x * value.x + value.y * value.y +
        value.z * value.z + value.w * value.w;
    return length > 0.5f && length < 1.5f;
}

// Rotates a vector by a unit quaternion without touching managed code.
Vector3 RotateVector(Quaternion rotation, Vector3 vector) {
    const Vector3 axis{rotation.x, rotation.y, rotation.z};
    const Vector3 doubled_cross{
        2.0f * (axis.y * vector.z - axis.z * vector.y),
        2.0f * (axis.z * vector.x - axis.x * vector.z),
        2.0f * (axis.x * vector.y - axis.y * vector.x)};
    return {
        vector.x + rotation.w * doubled_cross.x +
            (axis.y * doubled_cross.z - axis.z * doubled_cross.y),
        vector.y + rotation.w * doubled_cross.y +
            (axis.z * doubled_cross.x - axis.x * doubled_cross.z),
        vector.z + rotation.w * doubled_cross.z +
            (axis.x * doubled_cross.y - axis.y * doubled_cross.x)};
}

std::string ReadManagedString(void* managed_string) {
    if (!managed_string || !g_host || !g_host->copy_managed_string) return {};
    char buffer[256]{};
    g_host->copy_managed_string(g_host->context, managed_string, buffer, sizeof(buffer));
    return std::string(buffer);
}

std::string ObjectName(void* object) {
    if (!object) return "<null>";
    return ReadManagedString(Invoke(Contract("unity.object.name.get"), object, nullptr));
}

// UnityEngine.Object::op_Equality is the safe way to tell whether a scene
// object is still alive: it compares the cached native pointers instead of
// dereferencing them, so a destroyed object cannot crash the check.
bool IsObjectAlive(void* object) {
    if (!object) {
        return false;
    }
    void* parameters[2]{object, nullptr};
    bool destroyed = false;
    if (!Unbox(Invoke(Contract("unity.object.op_equality"), nullptr, parameters),
            destroyed)) {
        // Without the contract the object cannot be verified; keeping the anchor
        // is safer than dropping it every frame.
        return true;
    }
    return !destroyed;
}

bool KeyDown(int key) {
    if (key == VK_OEM_MINUS) {
        return ((GetAsyncKeyState(VK_OEM_MINUS) & 0x8000) != 0) ||
               ((GetAsyncKeyState(VK_SUBTRACT) & 0x8000) != 0);
    }
    return (GetAsyncKeyState(key) & 0x8000) != 0;
}

bool GameWindowHasFocus() {
    HWND foreground = GetForegroundWindow();
    if (!foreground) {
        return false;
    }
    DWORD process_id = 0;
    GetWindowThreadProcessId(foreground, &process_id);
    return process_id == GetCurrentProcessId();
}

LRESULT CALLBACK FreeCameraMouseHook(int code, WPARAM message, LPARAM data);

struct HotkeyRequest {
    std::atomic_int* key;
    std::atomic_bool* request;
    bool was_down = false;
};

void InputThreadMain() {
    HotkeyRequest playback_keys[]{
        {&g_motion_key, &g_motion_request},
        {&g_keyframe_add_key, &g_keyframe_add_request},
        {&g_keyframe_play_key, &g_keyframe_play_request},
        {&g_keyframe_clear_key, &g_keyframe_clear_request},
        {&g_vmd_play_key, &g_vmd_play_request},
    };
    HHOOK mouse_hook = nullptr;
    bool toggle_was_down = false;
    bool pause_was_down = false;
    bool first_person_was_down = false;
    while (!g_input_thread_stop.load(std::memory_order_acquire)) {
        const bool free_enabled = g_free_camera_enabled.load(std::memory_order_acquire) &&
            g_free_camera_contract_ready;
        const bool first_person_enabled = g_first_person_camera_enabled.load(std::memory_order_acquire) &&
            g_first_person_contract_ready;
        const int toggle_key = g_toggle_key.load(std::memory_order_relaxed);
        const int pause_key = g_pause_key.load(std::memory_order_relaxed);
        const int first_person_key = g_first_person_key.load(std::memory_order_relaxed);
        const bool focused = (free_enabled || first_person_enabled) && GameWindowHasFocus();

        const bool toggle_down = focused && free_enabled && KeyDown(toggle_key);
        const bool pause_down = focused && free_enabled && KeyDown(pause_key);
        const bool first_person_key_down = KeyDown(first_person_key) ||
            (first_person_key == VK_OEM_MINUS && KeyDown(VK_SUBTRACT)) ||
            (first_person_key == VK_SUBTRACT && KeyDown(VK_OEM_MINUS));
        const bool first_person_down = focused && first_person_enabled && first_person_key_down;

        if (toggle_down && !toggle_was_down) {
            g_toggle_request.store(true, std::memory_order_release);
        }
        if (pause_down && !pause_was_down) {
            g_pause_request.store(true, std::memory_order_release);
        }
        if (first_person_down && !first_person_was_down) {
            g_first_person_toggle_request.store(true, std::memory_order_release);
        }
        toggle_was_down = toggle_down;
        pause_was_down = pause_down;
        first_person_was_down = first_person_down;
        for (HotkeyRequest& binding : playback_keys) {
            const bool down = focused && free_enabled &&
                KeyDown(binding.key->load(std::memory_order_relaxed));
            if (down && !binding.was_down) {
                binding.request->store(true, std::memory_order_release);
            }
            binding.was_down = down;
        }

        // The low-level mouse hook only exists while the free camera runs in the
        // focused game window; its callbacks arrive through this thread's queue.
        const bool capture = focused && free_enabled &&
            g_mouse_look_enabled.load(std::memory_order_relaxed) &&
            g_free_camera_running.load(std::memory_order_acquire);
        g_mouse_capture.store(capture, std::memory_order_relaxed);
        if (capture && !mouse_hook) {
            mouse_hook = SetWindowsHookExW(WH_MOUSE_LL, &FreeCameraMouseHook,
                GetModuleHandleW(nullptr), 0);
        } else if (!capture && mouse_hook) {
            UnhookWindowsHookEx(mouse_hook);
            mouse_hook = nullptr;
        }
        MsgWaitForMultipleObjects(0, nullptr, FALSE, 5, QS_ALLINPUT);
        MSG message{};
        while (PeekMessageW(&message, nullptr, 0, 0, PM_REMOVE)) {
            TranslateMessage(&message);
            DispatchMessageW(&message);
        }
    }
    g_mouse_capture.store(false, std::memory_order_relaxed);
    if (mouse_hook) {
        UnhookWindowsHookEx(mouse_hook);
    }
}

void ReleaseCameraRoot() {
    if (g_active_camera_root && g_host && g_host->gchandle_free) {
        g_host->gchandle_free(g_host->context, g_active_camera_root);
    }
    g_active_camera_root = 0;
    if (!g_free_camera_active && !g_first_person_active.load(std::memory_order_acquire)) {
        g_active_camera = nullptr;
    }
}

void RestoreWorldPause(const char* reason) {
    if (!g_changed_time_scale) {
        return;
    }
    const bool restored = SetValue(Contract("unity.time.scale.set"), nullptr,
        g_original_time_scale);
    g_changed_time_scale = false;
    Log(std::string("World time restored: ") + reason +
        (restored ? " (ok)" : " (failed)"));
}

void ExitFirstPerson(const char* reason);

// Implemented in free_camera_runtime.inc.
void ExitFreeCamera(const char* reason);
bool EnterFreeCamera();
void ApplyFreeCamera();
void PumpFreeCameraRequests();

void PumpFreeCameraControl() {
    const bool allowed = g_free_camera_enabled.load(std::memory_order_acquire) &&
        g_free_camera_contract_ready;

    if (!allowed) {
        g_toggle_request.store(false, std::memory_order_release);
        g_pause_request.store(false, std::memory_order_release);
        if (g_free_camera_active ||
            g_force_exit_request.exchange(false, std::memory_order_acq_rel)) {
            ExitFreeCamera("feature disabled");
        }
        return;
    }

    g_force_exit_request.store(false, std::memory_order_release);
    if (g_toggle_request.exchange(false, std::memory_order_acq_rel)) {
        if (g_free_camera_active) {
            ExitFreeCamera("toggle hotkey");
        } else {
            EnterFreeCamera();
        }
    }
    const bool pause_pressed = g_pause_request.exchange(false,
        std::memory_order_acq_rel);
    if (pause_pressed && g_free_camera_active) {
        if (!g_pause_enabled.load(std::memory_order_acquire)) {
            return;
        }
        if (g_changed_time_scale) {
            RestoreWorldPause("toggle hotkey");
        } else {
            if (GetValue(Contract("unity.time.scale.get"), nullptr,
                g_original_time_scale) &&
                SetValue(Contract("unity.time.scale.set"), nullptr, 0.0f)) {
                g_changed_time_scale = true;
                Log("World time paused.");
            }
        }
    }
    PumpFreeCameraRequests();
}

void PumpFreeCamera() {
    PumpFreeCameraControl();
    if (g_free_camera_active) {
        ApplyFreeCamera();
    }
}

// ---------------------------------------------------------------------------
// First-Person Camera
//
// The pose is applied at the very last stage of the Cinemachine pipeline, in
// CinemachineBrain::PushStateToUnityCamera(Cinemachine.CameraState&): the state
// that is about to be handed to the Unity camera is rewritten in place, so no
// later system can overwrite it. Head hiding uses the game's own first-person
// path (SnapshotCameraController) instead of touching meshes, which keeps the
// feature independent from renderer and vertex layout changes.
// ---------------------------------------------------------------------------

bool ReadBytes(const void* base, int32_t offset, void* destination, size_t size) {
    if (!base || offset < 0 || static_cast<size_t>(offset) > kMaxFieldOffset) {
        return false;
    }
    std::memcpy(destination, static_cast<const uint8_t*>(base) + offset, size);
    return true;
}

bool WriteBytes(void* base, int32_t offset, const void* source, size_t size) {
    if (!base || offset < 0 || static_cast<size_t>(offset) > kMaxFieldOffset) {
        return false;
    }
    std::memcpy(static_cast<uint8_t*>(base) + offset, source, size);
    return true;
}

// Normalizes one metadata field offset into a struct body relative offset. The
// arithmetic is intentionally repeated per nesting level: every value type
// carries its own boxed header, so CameraState.Lens needs one subtraction and
// LensSettings.FieldOfView needs another.
bool StructFieldOffset(std::string_view key, int32_t& offset) {
    const FieldContract* field = Field(key);
    if (!field || !field->ready || field->resolved.offset <= 0) {
        return false;
    }
    const int32_t normalized = field->resolved.offset - g_value_type_header;
    if (normalized < 0 || static_cast<size_t>(normalized) > kMaxFieldOffset) {
        return false;
    }
    offset = normalized;
    return true;
}

bool BuildCameraStateLayout() {
    CameraStateLayout layout;
    const bool complete =
        StructFieldOffset("cinemachine.state.raw_position", layout.raw_position) &&
        StructFieldOffset("cinemachine.state.raw_orientation", layout.raw_orientation) &&
        StructFieldOffset("cinemachine.state.position_correction", layout.position_correction) &&
        StructFieldOffset("cinemachine.state.orientation_correction", layout.orientation_correction) &&
        StructFieldOffset("cinemachine.state.lens", layout.lens) &&
        StructFieldOffset("cinemachine.lens.field_of_view", layout.lens_field_of_view) &&
        StructFieldOffset("cinemachine.lens.near_clip", layout.lens_near_clip) &&
        StructFieldOffset("cinemachine.lens.dutch", layout.lens_dutch);
    if (!complete) {
        Log("First person: CameraState layout could not be resolved on this build.");
        return false;
    }
    layout.ready = true;
    g_state_layout = layout;
    char buffer[256];
    std::snprintf(buffer, sizeof(buffer),
        "First person: CameraState layout resolved (header=0x%X rawPos=0x%X rawRot=0x%X "
        "lens=0x%X fov=0x%X nearClip=0x%X dutch=0x%X)",
        g_value_type_header, layout.raw_position, layout.raw_orientation,
        layout.lens, layout.lens_field_of_view, layout.lens_near_clip,
        layout.lens_dutch);
    Log(buffer);
    return true;
}

bool ReadBoolField(void* instance, std::string_view key, bool& value) {
    const FieldContract* field = Field(key);
    if (!instance || !field || !field->ready || field->resolved.offset <= 0) {
        return false;
    }
    value = *reinterpret_cast<const uint8_t*>(
        static_cast<const uint8_t*>(instance) + field->resolved.offset) != 0;
    return true;
}

void* FindSnapshotCameraController() {
    if (g_first_person.snapshot_controller) {
        if (IsObjectAlive(g_first_person.snapshot_controller)) {
            return g_first_person.snapshot_controller;
        }
        if (g_first_person.snapshot_handle && g_host && g_host->gchandle_free) {
            g_host->gchandle_free(g_host->context, g_first_person.snapshot_handle);
        }
        g_first_person.snapshot_handle = 0;
        g_first_person.snapshot_controller = nullptr;
    }
    if (!g_snapshot_controller_class.type_object) {
        return nullptr;
    }
    bool include_inactive = true;
    void* parameters[2]{g_snapshot_controller_class.type_object, &include_inactive};
    void* controller = Invoke(Contract("unity.object.find_object_of_type"),
        nullptr, parameters);
    if (!controller) {
        return nullptr;
    }
    g_first_person.snapshot_controller = controller;
    g_first_person.snapshot_handle = g_host && g_host->gchandle_new
        ? g_host->gchandle_new(g_host->context, controller, 0)
        : 0;
    Log("First person: SnapshotCameraController found (" + ObjectName(controller) + ").");
    return controller;
}

// ---------------------------------------------------------------------------
// First-person head hiding
//
// The reference implementation hides the head, the hair and the head
// accessories, while the body, the arms, the equipment and the shadows of the
// character stay untouched. The game exposes exactly that granularity:
// BaseModelComponent.SetVisibleByNameContainsStr keeps its own hidden-name set
// and re-applies it whenever parts or LOD levels change. Driving the feature by
// part names instead of mesh buffers keeps it free of vertex, index and field
// layout assumptions, so it keeps working across game updates.
// ---------------------------------------------------------------------------

constexpr const char* kHeadPartTokens[]{
    "head", "face", "hair", "brow", "eyelid", "eyes", "iris", "mouth", "horn"};

constexpr int kMaxPartScanDepth = 6;
constexpr int kMaxPartScanNodes = 256;
constexpr int kMaxPartLogEntries = 32;

std::string LowerAscii(std::string value) {
    std::transform(value.begin(), value.end(), value.begin(),
        [](unsigned char character) {
            return static_cast<char>(std::tolower(character));
        });
    return value;
}

bool MatchesHeadPartToken(const std::string& name) {
    if (name.empty()) {
        return false;
    }
    const std::string lower = LowerAscii(name);
    for (const char* token : kHeadPartTokens) {
        if (lower.find(token) != std::string::npos) {
            return true;
        }
    }
    return false;
}

void ReadMeshProbe(void* mesh, HeadPartProbe& probe) {
    if (!mesh) {
        return;
    }
    probe.mesh_name = ObjectName(mesh);
    const MethodContract* vertex_count = Contract("unity.mesh.vertex_count.get");
    int count = 0;
    if (vertex_count && vertex_count->resolved &&
        GetValue(vertex_count, mesh, count)) {
        probe.vertex_count = count;
    }
}

// Only skinned renderers are probed: the head, the hair and the head
// accessories are skinned parts, and leaving the other component types out keeps
// the probe independent of the prefab layout.
void ReadPartComponents(void* game_object, HeadPartProbe& probe) {
    const MethodContract* get_component = Contract("unity.game_object.get_component");
    if (!game_object || !get_component || !get_component->resolved ||
        !g_skinned_mesh_renderer_class.type_object) {
        return;
    }
    void* parameters[1]{g_skinned_mesh_renderer_class.type_object};
    void* renderer = Invoke(get_component, game_object, parameters);
    if (!renderer) {
        return;
    }
    probe.skinned = true;
    probe.renderer = renderer;
    ReadMeshProbe(Invoke(
        Contract("unity.skinned_mesh_renderer.shared_mesh.get"), renderer, nullptr),
        probe);
}

void ScanPartNodes(void* transform, int depth, int& visited,
    std::vector<HeadPartProbe>& parts, std::string& tree, int& logged) {
    if (!transform || depth > kMaxPartScanDepth || visited >= kMaxPartScanNodes) {
        return;
    }
    ++visited;
    HeadPartProbe probe;
    probe.object_name = ObjectName(transform);
    ReadPartComponents(
        Invoke(Contract("unity.component.game_object"), transform, nullptr), probe);
    probe.matched = MatchesHeadPartToken(probe.object_name) ||
        MatchesHeadPartToken(probe.mesh_name);

    if (probe.renderer || probe.matched) {
        if (logged < kMaxPartLogEntries) {
            ++logged;
            std::string line(static_cast<size_t>(depth) * 2, ' ');
            line += probe.object_name;
            if (!probe.mesh_name.empty()) {
                line += " [mesh='" + probe.mesh_name + "'";
                if (probe.vertex_count >= 0) {
                    line += ", " + std::to_string(probe.vertex_count) + " vertices";
                }
                line += "]";
            }
            if (probe.matched) {
                line += " <-- head part";
            }
            tree += "\n  " + line;
        } else if (logged == kMaxPartLogEntries) {
            ++logged;
            tree += "\n  ...";
        }
    }
    parts.push_back(probe);

    const MethodContract* child_count = Contract("unity.transform.child_count.get");
    const MethodContract* get_child = Contract("unity.transform.get_child");
    int children = 0;
    if (!child_count || !get_child || !GetValue(child_count, transform, children)) {
        return;
    }
    for (int index = 0; index < children; ++index) {
        void* parameters[1]{&index};
        ScanPartNodes(Invoke(get_child, transform, parameters), depth + 1, visited,
            parts, tree, logged);
    }
}

void* FindModelComponent() {
    void* entity = Invoke(Contract("player_controller.get_main_character"),
        nullptr, nullptr);
    return entity ? Invoke(Contract("entity.get_model_com"), entity, nullptr)
                  : nullptr;
}

void* FindModelObject() {
    void* model_component = FindModelComponent();
    return model_component
        ? Invoke(Contract("base_model_component.get_model_go"), model_component,
            nullptr)
        : nullptr;
}

void* FindModelTransform() {
    void* model_object = FindModelObject();
    return model_object
        ? Invoke(Contract("unity.game_object.transform"), model_object, nullptr)
        : nullptr;
}

#include "first_person_runtime.inc"
#include "free_camera_runtime.inc"

void ReleaseHeadTransform() {
    if (g_first_person.head_handle && g_host && g_host->gchandle_free) {
        g_host->gchandle_free(g_host->context, g_first_person.head_handle);
    }
    g_first_person.head_handle = 0;
    g_first_person.head = nullptr;
    if (g_first_person.neck_handle && g_host && g_host->gchandle_free) {
        g_host->gchandle_free(g_host->context, g_first_person.neck_handle);
    }
    g_first_person.neck_handle = 0;
    g_first_person.neck = nullptr;
    g_first_person.last_eye_valid = false;
    g_first_person_head_logged = false;
}

// The neck bone defines the search frame for the real opening. Missing neck
// metadata disables cap detection; head hiding can still use the head palette.
void BindNeckBone(void* head) {
    if (g_first_person.neck) {
        return;
    }
    void* parent = head
        ? Invoke(Contract("unity.transform.parent.get"), head, nullptr)
        : nullptr;
    if (!parent) {
        return;
    }
    std::string name = LowerAscii(ObjectName(parent));
    if (name.find("neck") == std::string::npos) {
        return;
    }
    g_first_person.neck = parent;
    g_first_person.neck_handle = g_host && g_host->gchandle_new
        ? g_host->gchandle_new(g_host->context, parent, 0)
        : 0;
}

bool LooksLikeHeadBone(void* transform) {
    const std::string name = ObjectName(transform);
    std::string lower = name;
    std::transform(lower.begin(), lower.end(), lower.begin(),
        [](unsigned char c) { return static_cast<char>(std::tolower(c)); });
    return lower.find("head") != std::string::npos;
}

void* FindHeadBoneRecursive(void* current, int depth) {
    if (!current || depth > 12) {
        return nullptr;
    }
    if (LooksLikeHeadBone(current)) {
        return current;
    }
    int child_count = 0;
    if (!GetValue(Contract("unity.transform.child_count.get"), current, child_count)) {
        return nullptr;
    }
    for (int index = 0; index < child_count; ++index) {
        void* parameters[1]{&index};
        void* child = Invoke(Contract("unity.transform.get_child"), current, parameters);
        if (child) {
            if (void* found = FindHeadBoneRecursive(child, depth + 1)) {
                return found;
            }
        }
    }
    return nullptr;
}

void* TryHumanoidHeadBone(void* model_root) {
    if (!model_root || !g_animator_class.type_object) {
        return nullptr;
    }
    void* game_object = Invoke(Contract("unity.component.game_object"), model_root, nullptr);
    if (!game_object) {
        return nullptr;
    }
    void* find_parameters[1]{g_animator_class.type_object};
    void* animator = Invoke(Contract("unity.game_object.get_component"),
        game_object, find_parameters);
    if (!animator) {
        return nullptr;
    }
    int32_t bone = kHumanBodyBonesHead;
    void* parameters[1]{&bone};
    void* head = Invoke(Contract("unity.animator.get_bone_transform"), animator, parameters);
    // The humanoid enum value is verified by name instead of being trusted, so a
    // shifted enum on another game build degrades into the name search below.
    return head && LooksLikeHeadBone(head) ? head : nullptr;
}

void* ResolvePlayerHeadTransform() {
    void* model_root = nullptr;
    void* entity = Invoke(Contract("player_controller.get_main_character"), nullptr, nullptr);
    if (entity) {
        void* model_component = Invoke(Contract("entity.get_model_com"), entity, nullptr);
        void* model_object = model_component
            ? Invoke(Contract("base_model_component.get_model_go"), model_component, nullptr)
            : nullptr;
        if (model_object) {
            model_root = Invoke(Contract("unity.game_object.transform"),
                model_object, nullptr);
        }
    }
    if (!model_root) {
        void* player_tag = g_host->string_new(g_host->context, "Player");
        void* parameters[1]{player_tag};
        void* player = Invoke(Contract("unity.game_object.find_with_tag"), nullptr, parameters);
        if (player) {
            model_root = Invoke(Contract("unity.game_object.transform"),
                player, nullptr);
        }
    }
    if (!model_root) {
        return nullptr;
    }

    if (void* head = TryHumanoidHeadBone(model_root)) {
        return head;
    }
    void* head_path = g_host->string_new(g_host->context,
        "Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_Neck/Bip001_Head");
    void* find_parameters[1]{head_path};
    if (void* head = Invoke(Contract("unity.transform.find"), model_root, find_parameters)) {
        return head;
    }
    if (void* head = FindHeadBoneRecursive(model_root, 0)) {
        return head;
    }
    // Last resort: anchor to the model root so the view still follows the body.
    return model_root;
}

void TryBindHeadTransform() {
    if (g_first_person.head) {
        return;
    }
    void* head = ResolvePlayerHeadTransform();
    if (!head) {
        if (!g_first_person_head_logged) {
            g_first_person_head_logged = true;
            Log("First person: the player model is not loaded yet; retrying.");
        }
        return;
    }
    g_first_person.head = head;
    g_first_person.head_handle = g_host && g_host->gchandle_new
        ? g_host->gchandle_new(g_host->context, head, 0)
        : 0;
    BindNeckBone(head);
    Log("First person: head anchor bound to \"" + ObjectName(head) + "\".");
}

// Rewrites the CameraState that Cinemachine is about to push to the Unity
// camera. The orientation is kept exactly as the game produced it (so aim
// offsets and look input stay authoritative) and only the position is moved to
// the eye anchor. The corrections are cleared so the pushed pose equals the raw
// pose instead of being nudged back by the collider/damping stages.
void ApplyFirstPersonState(void* state) {
    if (!g_state_layout.ready || !IsObjectAlive(g_first_person.head)) {
        return;
    }

    Quaternion orientation{};
    if (!ReadBytes(state, g_state_layout.raw_orientation, &orientation,
            sizeof(orientation)) || !IsUnitQuaternion(orientation)) {
        return;
    }
    Vector3 head{};
    if (!GetValue(Contract("unity.transform.position.get"),
            g_first_person.head, head) || !IsFinite(head)) {
        return;
    }

    const Vector3 forward = RotateVector(orientation, Vector3{0.0f, 0.0f, 1.0f});
    const Vector3 up = RotateVector(orientation, Vector3{0.0f, 1.0f, 0.0f});
    const Vector3 eye = Add(head,
        Add(Scale(forward, kFirstPersonEyeForward), Scale(up, kFirstPersonEyeUp)));

    const float field_of_view = g_first_person_fov.load(std::memory_order_relaxed);
    const float near_clip = kFirstPersonNearClip;
    const Vector3 no_correction{};
    const Quaternion no_rotation_correction{0.0f, 0.0f, 0.0f, 1.0f};
    const int32_t lens = g_state_layout.lens;

    WriteBytes(state, g_state_layout.raw_position, &eye, sizeof(eye));
    WriteBytes(state, g_state_layout.position_correction, &no_correction,
        sizeof(no_correction));
    WriteBytes(state, g_state_layout.orientation_correction,
        &no_rotation_correction, sizeof(no_rotation_correction));
    WriteBytes(state, lens + g_state_layout.lens_field_of_view, &field_of_view,
        sizeof(field_of_view));
    WriteBytes(state, lens + g_state_layout.lens_near_clip, &near_clip,
        sizeof(near_clip));

    g_first_person.last_eye = eye;
    g_first_person.last_eye_valid = true;
    g_push_state_patches.fetch_add(1, std::memory_order_relaxed);
}

// Confirms that the patched state actually reached the Unity camera. A large
// distance means the offsets or the hook are wrong on this game build, which is
// reported instead of silently producing a broken view.
void FirstPersonHealthCheck() {
    if (!g_first_person.last_eye_valid) {
        return;
    }
    void* camera = Invoke(Contract("unity.camera.main"), nullptr, nullptr);
    void* transform = camera
        ? Invoke(Contract("unity.component.transform"), camera, nullptr)
        : nullptr;
    Vector3 camera_position{};
    if (!transform ||
        !GetValue(Contract("unity.transform.position.get"), transform, camera_position)) {
        return;
    }
    const float distance = Magnitude(Subtract(camera_position, g_first_person.last_eye));
    const uint64_t patches = g_push_state_patches.load(std::memory_order_relaxed);
    const uint64_t calls = g_push_state_calls.load(std::memory_order_relaxed);
    if (g_diagnostics_enabled.load(std::memory_order_relaxed)) {
        char buffer[256];
        std::snprintf(buffer, sizeof(buffer),
            "First person pose check: eyeDistance=%.2f (pushCalls=%llu patches=%llu)",
            distance, static_cast<unsigned long long>(calls),
            static_cast<unsigned long long>(patches));
        Log(buffer);
    }
    if (calls == 0) {
        if (!g_first_person_health_warned) {
            g_first_person_health_warned = true;
            Log("First person: the CameraState hook is installed but never called on "
                "this build; the camera may be driven by a different pipeline.");
        }
    } else if (distance > 1.5f && !g_first_person_health_warned) {
        g_first_person_health_warned = true;
        Log("First person: the Unity camera is not following the pushed state; the "
            "CameraState offsets are likely wrong on this build.");
    }
}

bool EnterFirstPerson() {
    if (!g_state_layout.ready || !g_push_state_hook_ready) {
        Log("First person camera is unavailable: the Cinemachine CameraState hook "
            "is not active on this build.");
        return false;
    }
    if (g_free_camera_active) {
        ExitFreeCamera("switching to first person");
    }
    void* camera = Invoke(Contract("unity.camera.main"), nullptr, nullptr);
    if (!camera) {
        Log("First person camera could not capture the active Unity camera.");
        return false;
    }
    g_active_camera = camera;
    g_active_camera_root = g_host && g_host->gchandle_new
        ? g_host->gchandle_new(g_host->context, camera, 0)
        : 0;
    float original_fov = 0.0f;
    GetValue(Contract("unity.camera.fov.get"), camera, original_fov);

    g_first_person.character = nullptr;
    g_first_person_head_logged = false;
    g_first_person_pump_frames = 0;
    TryBindHeadTransform();
    if (g_first_person_hide_head.load(std::memory_order_relaxed)) {
        ExitGameFirstPersonMode();
        ApplyHeadPartHide();
        EnsureNeckCap();
    } else {
        Log("First person: head hiding is disabled in the configuration.");
    }

    g_first_person.last_eye_valid = false;
    g_first_person_reassert_frames = 0;
    g_first_person_health_warned = false;
    g_first_person_active.store(true, std::memory_order_release);
    char buffer[256];
    std::snprintf(buffer, sizeof(buffer),
        "First person camera enabled (hotkey=0x%X, fov=%.1f, sourceFov=%.1f, "
        "eyeForward=%.2f, eyeUp=%.2f, nearClip=%.2f).",
        g_first_person_key.load(std::memory_order_relaxed),
        g_first_person_fov.load(std::memory_order_relaxed), original_fov,
        kFirstPersonEyeForward, kFirstPersonEyeUp, kFirstPersonNearClip);
    Log(buffer);
    return true;
}

void ExitFirstPerson(const char* reason) {
    if (!g_first_person_active.load(std::memory_order_acquire)) {
        return;
    }
    g_first_person_active.store(false, std::memory_order_release);
    ReleaseHeadPartHide();
    ReleaseNeckCap();
    if (g_first_person.snapshot_handle && g_host && g_host->gchandle_free) {
        g_host->gchandle_free(g_host->context, g_first_person.snapshot_handle);
    }
    g_first_person.snapshot_handle = 0;
    g_first_person.snapshot_controller = nullptr;
    ReleaseHeadTransform();
    g_first_person.character = nullptr;
    const uint64_t patches = g_push_state_patches.load(std::memory_order_relaxed);
    ReleaseCameraRoot();
    Log(std::string("First person camera disabled: ") + reason +
        " (patched frames=" + std::to_string(patches) + ").");
}

// Follows leader switches and respawns: the head anchor and the game's own
// first-person state belong to a character, so both are rebound.
void RefreshFirstPersonTarget() {
    void* character = Invoke(Contract("player_controller.get_main_character"),
        nullptr, nullptr);
    if (character && character != g_first_person.character) {
        if (g_first_person.character) {
            Log("First person: main character changed; rebinding the eye anchor.");
            ReleaseHeadTransform();
            ReleaseHeadPartHide();
            ReleaseNeckCap();
        }
        g_first_person.character = character;
        if (g_first_person_hide_head.load(std::memory_order_relaxed)) {
            ExitGameFirstPersonMode();
            ApplyHeadPartHide();
            EnsureNeckCap();
        }
    }
    if (g_first_person.head && !IsObjectAlive(g_first_person.head)) {
        Log("First person: the eye anchor was destroyed; rebinding.");
        ReleaseHeadTransform();
    }
    TryBindHeadTransform();
}

void PumpFirstPerson() {
    const bool allowed = g_first_person_camera_enabled.load(std::memory_order_acquire) &&
        g_first_person_contract_ready;
    if (!allowed) {
        g_first_person_toggle_request.store(false, std::memory_order_release);
        if (g_first_person_active.load(std::memory_order_acquire) ||
            g_first_person_exit_request.exchange(false, std::memory_order_acq_rel)) {
            ExitFirstPerson("feature disabled");
        }
        return;
    }
    g_first_person_exit_request.store(false, std::memory_order_release);
    if (g_first_person_toggle_request.exchange(false, std::memory_order_acq_rel)) {
        if (g_first_person_active.load(std::memory_order_acquire)) {
            ExitFirstPerson("toggle hotkey");
        } else {
            EnterFirstPerson();
        }
    }
    if (!g_first_person_active.load(std::memory_order_acquire)) {
        return;
    }
    RefreshFirstPersonTarget();
    if (!g_first_person_hide_head.load(std::memory_order_relaxed)) {
        if (g_first_person.head_hide_applied) ReleaseHeadPartHide();
    } else if (!g_first_person.head_hide_applied) {
        ApplyHeadPartHide();
    }
    if (g_first_person.head_hide_applied) {
        EnsureNeckCap();
        if (++g_first_person_reassert_frames % 30 == 0) {
            ReapplyHeadPartHide();
        }
    }
    if (++g_first_person_pump_frames % 30 == 0) {
        FirstPersonHealthCheck();
    }
}

// Time.unscaledDeltaTime keeps being read while Time.timeScale is 0, so it is
// the heartbeat for the hotkeys and for the free camera while the world is
// frozen. The guard stops managed calls made from here from re-entering.
thread_local bool t_in_heartbeat = false;

float __fastcall DetourTimeUnscaledDelta(void* method) {
    const float result = g_original_time_unscaled_delta
        ? g_original_time_unscaled_delta(method)
        : 0.0f;
    if (g_time_heartbeat_contract_ready && !t_in_heartbeat) {
        t_in_heartbeat = true;
        PumpFreeCameraControl();
        if (g_free_camera_active) {
            ApplyFreeCameraHeartbeat();
        }
        t_in_heartbeat = false;
    }
    return result;
}

void __fastcall DetourPushState(void* instance, void* state, void* method) {
    g_push_state_calls.fetch_add(1, std::memory_order_relaxed);
    if (state && g_first_person_active.load(std::memory_order_acquire)) {
        ApplyFirstPersonState(state);
    } else if (state && g_free_camera_active && g_state_layout.ready &&
        BrainDrivesActiveCamera(instance)) {
        ApplyFreeCameraState(state);
    }
    if (g_original_push_state) {
        g_original_push_state(instance, state, method);
    }
}

void __fastcall DetourTailLateTick(void* instance, float deltaTime, void* method) {
    if (g_original_tail_late_tick) {
        g_original_tail_late_tick(instance, deltaTime, method);
    }
    // TailLateTick runs at the very tail of the frame. The first-person pose is
    // applied inside the Cinemachine push itself, so this only drives the toggle,
    // the eye anchor bookkeeping and the free camera.
    (void)instance;
    PumpFreeCameraControl();
    PumpFirstPerson();
    if (g_free_camera_active) {
        ApplyFreeCamera();
    }
}

void __fastcall DetourCameraTick(void* instance, void* method) {
    if (g_original_camera_tick) {
        g_original_camera_tick(instance, method);
    }
    if (g_disable_dither_enabled.load(std::memory_order_acquire) &&
        g_dither_contract_ready) {
        InvokeVoid(Contract("camera.force_clear_dither"), instance, nullptr);
    }
    // If TailLateTick is not hooked, fall back to the camera tick for the pumps.
    if (!g_original_tail_late_tick) {
        PumpFreeCamera();
        PumpFirstPerson();
    }
}

std::string Trim(std::string_view value) {
    while (!value.empty() && std::isspace(static_cast<unsigned char>(value.front()))) {
        value.remove_prefix(1);
    }
    while (!value.empty() && std::isspace(static_cast<unsigned char>(value.back()))) {
        value.remove_suffix(1);
    }
    return std::string(value);
}

bool ParseBoolean(std::string_view value, bool default_value = false) {
    std::string text = Trim(value);
    std::transform(text.begin(), text.end(), text.begin(),
        [](unsigned char c) { return static_cast<char>(std::tolower(c)); });
    if (text == "1" || text == "true" || text == "yes" || text == "on") {
        return true;
    }
    if (text == "0" || text == "false" || text == "no" || text == "off") {
        return false;
    }
    return default_value;
}

float ParseFloat(std::string_view value, float default_value) {
    std::string text = Trim(value);
    if (text.empty()) {
        return default_value;
    }
    char* end = nullptr;
    const float parsed = std::strtof(text.c_str(), &end);
    return end && end != text.c_str() && std::isfinite(parsed)
        ? parsed
        : default_value;
}

int ParseVirtualKey(std::string_view value, int fallback) {
    std::string key = Trim(value);
    std::transform(key.begin(), key.end(), key.begin(),
        [](unsigned char c) { return static_cast<char>(std::toupper(c)); });
    if (key == "-" || key == "MINUS" || key == "OEM_MINUS") {
        return VK_OEM_MINUS;
    }
    if (key == "SUBTRACT" || key == "NUMPAD-" || key == "NUMPAD_MINUS" || key == "NUMPADSUBTRACT") {
        return VK_SUBTRACT;
    }
    if (key.size() == 1 && std::isalnum(static_cast<unsigned char>(key[0]))) {
        return static_cast<unsigned char>(key[0]);
    }
    if (key.size() > 1 && key.front() == 'F') {
        const int number = std::atoi(key.c_str() + 1);
        if (number >= 1 && number <= 24) {
            return VK_F1 + number - 1;
        }
    }
    constexpr std::string_view numpad_prefix = "NUMPAD";
    if (key.size() == numpad_prefix.size() + 1 &&
        key.starts_with(numpad_prefix) && key.back() >= '0' && key.back() <= '9') {
        return VK_NUMPAD0 + key.back() - '0';
    }
    return fallback;
}

int ParseMotionPreset(std::string_view value, int fallback) {
    std::string text = Trim(value);
    std::transform(text.begin(), text.end(), text.begin(),
        [](unsigned char c) { return static_cast<char>(std::tolower(c)); });
    if (text == "orbit") return 0;
    if (text == "dolly_zoom" || text == "dolly") return 1;
    if (text == "crane") return 2;
    if (text == "truck" || text == "pan") return 3;
    return fallback;
}

std::string Unquote(std::string_view value) {
    std::string text = Trim(value);
    if (text.size() >= 2 && text.front() == '"' && text.back() == '"') {
        text = text.substr(1, text.size() - 2);
    }
    return text;
}

CameraConfiguration ParseConfiguration(const char* raw_configuration) {
    CameraConfiguration config;
    if (!raw_configuration) {
        return config;
    }
    std::string_view text(raw_configuration);
    bool in_section = true;
    size_t line_start = 0;
    while (line_start < text.size()) {
        size_t line_end = text.find_first_of("\r\n", line_start);
        if (line_end == std::string_view::npos) line_end = text.size();
        const std::string line = Trim(text.substr(line_start, line_end - line_start));
        line_start = text.find_first_not_of("\r\n", line_end);
        if (line.empty() || line.front() == ';' || line.front() == '#') continue;
        if (line.front() == '[' && line.back() == ']') {
            in_section = line == "[betterendfield.camera]";
            continue;
        }
        if (!in_section) continue;
        const size_t equals = line.find('=');
        if (equals == std::string::npos) continue;
        const std::string key = Trim(line.substr(0, equals));
        const std::string value = Trim(line.substr(equals + 1));
        if (key == "schema_version") config.schema_version = std::atoi(value.c_str());
        else if (key == "enabled") config.enabled = ParseBoolean(value, config.enabled);
        else if (key == "free_camera_enabled") config.free_camera_enabled = ParseBoolean(value, config.free_camera_enabled);
        else if (key == "disable_dither_enabled") config.disable_dither_enabled = ParseBoolean(value, config.disable_dither_enabled);
        else if (key == "pause_enabled") config.pause_enabled = ParseBoolean(value, config.pause_enabled);
        else if (key == "first_person_camera_enabled" || key == "first_person_enabled") config.first_person_camera_enabled = ParseBoolean(value, config.first_person_camera_enabled);
        else if (key == "first_person_hide_head") config.first_person_hide_head = ParseBoolean(value, config.first_person_hide_head);
        else if (key == "first_person_fill_neck_hole") config.first_person_fill_neck_hole = ParseBoolean(value, config.first_person_fill_neck_hole);
        else if (key == "first_person_neck_plug_scale") config.first_person_neck_plug_scale = ParseFloat(value, config.first_person_neck_plug_scale);
        else if (key == "diagnostics") config.diagnostics = ParseBoolean(value, config.diagnostics);
        else if (key == "movement_speed") config.movement_speed = ParseFloat(value, config.movement_speed);
        else if (key == "field_of_view") config.field_of_view = ParseFloat(value, config.field_of_view);
        else if (key == "first_person_fov") config.first_person_fov = ParseFloat(value, config.first_person_fov);
        else if (key == "toggle_hotkey") config.toggle_key = ParseVirtualKey(value, config.toggle_key);
        else if (key == "pause_hotkey") config.pause_key = ParseVirtualKey(value, config.pause_key);
        else if (key == "first_person_hotkey") config.first_person_key = ParseVirtualKey(value, config.first_person_key);
        else if (key == "free_camera_mouse_look") config.mouse_look = ParseBoolean(value, config.mouse_look);
        else if (key == "mouse_invert_y") config.mouse_invert_y = ParseBoolean(value, config.mouse_invert_y);
        else if (key == "mouse_sensitivity") config.mouse_sensitivity = ParseFloat(value, config.mouse_sensitivity);
        else if (key == "free_camera_smoothing") config.smoothing = ParseFloat(value, config.smoothing);
        else if (key == "motion_preset") config.motion_preset = ParseMotionPreset(value, config.motion_preset);
        else if (key == "motion_speed") config.motion_speed = ParseFloat(value, config.motion_speed);
        else if (key == "orbit_speed") config.orbit_speed = ParseFloat(value, config.orbit_speed);
        else if (key == "motion_duration") config.motion_duration = ParseFloat(value, config.motion_duration);
        else if (key == "motion_target_height") config.motion_target_height = ParseFloat(value, config.motion_target_height);
        else if (key == "keyframe_segment_seconds") config.keyframe_segment_seconds = ParseFloat(value, config.keyframe_segment_seconds);
        else if (key == "keyframe_loop") config.keyframe_loop = ParseBoolean(value, config.keyframe_loop);
        else if (key == "vmd_camera_file") config.vmd_camera_file = Unquote(value);
        else if (key == "vmd_camera_scale") config.vmd_camera_scale = ParseFloat(value, config.vmd_camera_scale);
        else if (key == "vmd_camera_fov_bias") config.vmd_camera_fov_bias = ParseFloat(value, config.vmd_camera_fov_bias);
        else if (key == "vmd_camera_loop") config.vmd_camera_loop = ParseBoolean(value, config.vmd_camera_loop);
        else if (key == "roll_left_hotkey") config.roll_left_key = ParseVirtualKey(value, config.roll_left_key);
        else if (key == "roll_right_hotkey") config.roll_right_key = ParseVirtualKey(value, config.roll_right_key);
        else if (key == "fov_wide_hotkey") config.fov_wide_key = ParseVirtualKey(value, config.fov_wide_key);
        else if (key == "fov_narrow_hotkey") config.fov_narrow_key = ParseVirtualKey(value, config.fov_narrow_key);
        else if (key == "view_reset_hotkey") config.view_reset_key = ParseVirtualKey(value, config.view_reset_key);
        else if (key == "motion_hotkey") config.motion_key = ParseVirtualKey(value, config.motion_key);
        else if (key == "keyframe_add_hotkey") config.keyframe_add_key = ParseVirtualKey(value, config.keyframe_add_key);
        else if (key == "keyframe_play_hotkey") config.keyframe_play_key = ParseVirtualKey(value, config.keyframe_play_key);
        else if (key == "keyframe_clear_hotkey") config.keyframe_clear_key = ParseVirtualKey(value, config.keyframe_clear_key);
        else if (key == "vmd_play_hotkey") config.vmd_play_key = ParseVirtualKey(value, config.vmd_play_key);
    }
    config.movement_speed = std::clamp(config.movement_speed, 0.5f, 100.0f);
    config.field_of_view = std::clamp(config.field_of_view, 20.0f, 120.0f);
    config.first_person_fov = std::clamp(config.first_person_fov, 20.0f, 120.0f);
    config.first_person_neck_plug_scale =
        std::clamp(config.first_person_neck_plug_scale, 0.2f, 3.0f);
    config.mouse_sensitivity = std::clamp(config.mouse_sensitivity, 0.01f, 2.0f);
    config.smoothing = std::clamp(config.smoothing, 0.0f, 0.95f);
    config.motion_speed = std::clamp(config.motion_speed, -20.0f, 20.0f);
    config.orbit_speed = std::clamp(config.orbit_speed, -180.0f, 180.0f);
    config.motion_duration = std::clamp(config.motion_duration, 0.0f, 600.0f);
    config.motion_target_height = std::clamp(config.motion_target_height, -5.0f, 5.0f);
    config.keyframe_segment_seconds = std::clamp(config.keyframe_segment_seconds, 0.2f, 60.0f);
    config.vmd_camera_scale = std::clamp(config.vmd_camera_scale, 0.001f, 10.0f);
    config.vmd_camera_fov_bias = std::clamp(config.vmd_camera_fov_bias, -60.0f, 60.0f);
    return config;
}

bool ResolveContracts() {
    if (!g_host || !g_host->resolve_method) {
        return false;
    }
    for (auto& contract : g_contracts) {
        BE_ResolvedMethodV1 resolved{};
        BE_Result status = g_host->resolve_method(g_host->context,
            &contract.descriptor, &resolved);
        if ((status != BE_Result_Ok || !resolved.method_pointer ||
                !resolved.method_info) &&
            contract.alternate_parameter_types) {
            BE_MethodDescriptorV1 alternate = contract.descriptor;
            alternate.parameter_types = contract.alternate_parameter_types;
            status = g_host->resolve_method(g_host->context, &alternate, &resolved);
        }
        if (status == BE_Result_Ok && resolved.method_pointer &&
            resolved.method_info) {
            contract.pointer = resolved.method_pointer;
            contract.method_info = resolved.method_info;
            contract.resolved = true;
            Log(std::string("Resolved method contract: ") + contract.key);
        } else {
            Log(std::string("Method contract not found: ") + contract.key);
        }
    }

    if (g_host->resolve_field) {
        for (auto& field : g_fields) {
            BE_ResolvedFieldV1 resolved{};
            if (g_host->resolve_field(g_host->context, &field.descriptor, &resolved) ==
                    BE_Result_Ok && resolved.offset > 0) {
                field.resolved = resolved;
                field.ready = true;
                Log(std::string("Resolved field contract: ") + field.key +
                    " (offset=0x" + [] (int32_t offset) {
                        char text[16];
                        std::snprintf(text, sizeof(text), "%X", offset);
                        return std::string(text);
                    }(resolved.offset) + ")");
            } else {
                Log(std::string("Field contract not found: ") + field.key);
            }
        }
    }

    // The metadata offset of the first field of a value type is exactly the size
    // of the boxed object header, which is what the CameraState arithmetic needs.
    const FieldContract* probe = Field("unity.vector3.x");
    if (probe && probe->ready && probe->resolved.offset > 0 &&
        probe->resolved.offset <= 0x20) {
        g_value_type_header = probe->resolved.offset;
    } else {
        g_value_type_header = 0;
        Log("First person: the value type header probe failed; assuming metadata "
            "offsets are struct relative.");
    }

    if (g_host->resolve_class) {
        BE_ResolvedClassV1 snapshot_class{};
        if (g_host->resolve_class(g_host->context, "Gameplay.Beyond.dll",
                "Beyond.Gameplay.View", "SnapshotCameraController",
                &snapshot_class) == BE_Result_Ok) {
            g_snapshot_controller_class = snapshot_class;
            Log("Resolved class: Beyond.Gameplay.View.SnapshotCameraController");
        } else {
            Log("Class not found: Beyond.Gameplay.View.SnapshotCameraController");
        }
        BE_ResolvedClassV1 animator_class{};
        if (g_host->resolve_class(g_host->context, "UnityEngine.AnimationModule.dll",
                "UnityEngine", "Animator", &animator_class) == BE_Result_Ok) {
            g_animator_class = animator_class;
        }
        BE_ResolvedClassV1 skinned_class{};
        if (g_host->resolve_class(g_host->context, "UnityEngine.CoreModule.dll",
                "UnityEngine", "SkinnedMeshRenderer", &skinned_class) == BE_Result_Ok) {
            g_skinned_mesh_renderer_class = skinned_class;
            Log("Resolved class: UnityEngine.SkinnedMeshRenderer");
        } else {
            Log("Class not found: UnityEngine.SkinnedMeshRenderer");
        }

    }

    BuildCameraStateLayout();

    const auto ready = [](std::string_view key) {
        const MethodContract* method = Contract(key);
        return method && method->resolved;
    };
    g_dither_contract_ready = ready("camera.process_dither") &&
        ready("camera.force_clear_dither");
    g_free_camera_contract_ready = ready("camera.process_dither") &&
        ready("unity.camera.main") && ready("unity.camera.fov.get") &&
        ready("unity.camera.fov.set") && ready("unity.component.transform") &&
        ready("unity.transform.position.get") &&
        ready("unity.transform.position.set") &&
        ready("unity.transform.forward") && ready("unity.transform.right") &&
        ready("unity.transform.up") && ready("unity.transform.rotation.set") &&
        ready("unity.time.scale.get") &&
        ready("unity.time.scale.set");
    g_time_heartbeat_contract_ready = ready("unity.time.unscaled_delta.get");
    g_first_person_contract_ready =
        (ready("camera_manager.tail_late_tick") || ready("camera.process_dither")) &&
        ready("cinemachine.push_state") &&
        ready("player_controller.get_main_character") &&
        ready("entity.get_model_com") &&
        ready("base_model_component.get_model_go") &&
        ready("unity.game_object.transform") &&
        ready("unity.object.name.get") &&
        ready("unity.transform.position.get") &&
        ready("unity.transform.child_count.get") &&
        ready("unity.transform.get_child") &&
        ready("unity.transform.find") &&
        ready("unity.camera.main") &&
        ready("unity.camera.fov.get") &&
        g_state_layout.ready;

    Log(std::string("Camera feature contracts: free_camera=") +
        (g_free_camera_contract_ready ? "ready" : "unavailable") +
        ", first_person=" + (g_first_person_contract_ready ? "ready" : "unavailable") +
        ", anti_dither=" + (g_dither_contract_ready ? "ready" : "unavailable") +
        ", time_heartbeat=" +
        (g_time_heartbeat_contract_ready ? "ready" : "unavailable"));
    const FieldContract* first_person_flag = Field("snapshot.is_first_person");
    const bool photo_mode_exit_ready = ready("snapshot.set_first_person") &&
        ready("snapshot.show_char") && ready("unity.object.find_object_of_type") &&
        first_person_flag && first_person_flag->ready &&
        g_snapshot_controller_class.type_object;
    const bool head_part_probe_ready = ready("unity.game_object.get_component") &&
        ready("unity.skinned_mesh_renderer.shared_mesh.get") &&
        ready("unity.mesh.vertex_count.get") &&
        g_skinned_mesh_renderer_class.type_object;
    const bool neck_cap_ready = ready("unity.skinned_mesh_renderer.bones.get") &&
        ready("unity.mesh.bindposes.get") && ready("unity.object.destroy");
    Log(std::string("First person optional contracts: photo_mode_exit=") +
        (photo_mode_exit_ready ? "ready" : "unavailable") +
        ", head_part_probe=" + (head_part_probe_ready ? "ready" : "unavailable") +
        ", neck_cap=" + (neck_cap_ready ? "ready" : "unavailable") +
        ", humanoid_head_bone=" +
        ((ready("unity.animator.get_bone_transform") &&
            ready("unity.game_object.get_component") &&
            g_animator_class.type_object) ? "ready" : "unavailable"));
    return g_free_camera_contract_ready || g_dither_contract_ready ||
        g_first_person_contract_ready;
}

bool InstallHook() {
    MethodContract* tick = Contract("camera.process_dither");
    if (!tick || !tick->resolved || !g_host || !g_host->create_hook) {
        return false;
    }
    if (g_host->create_hook(g_host->context, kModuleId, tick->pointer,
        reinterpret_cast<void*>(&DetourCameraTick),
        reinterpret_cast<void**>(&g_original_camera_tick)) != BE_Result_Ok) {
        return false;
    }

    MethodContract* tail_tick = Contract("camera_manager.tail_late_tick");
    if (tail_tick && tail_tick->resolved) {
        if (g_host->create_hook(g_host->context, kModuleId, tail_tick->pointer,
            reinterpret_cast<void*>(&DetourTailLateTick),
            reinterpret_cast<void**>(&g_original_tail_late_tick)) == BE_Result_Ok) {
            Log("Successfully installed CameraManager::TailLateTick hook.");
        } else {
            Log("Warning: Failed to install CameraManager::TailLateTick hook; using CameraMono tick fallback.");
        }
    }

    MethodContract* push_state = Contract("cinemachine.push_state");
    if (push_state && push_state->resolved) {
        if (g_host->create_hook(g_host->context, kModuleId, push_state->pointer,
            reinterpret_cast<void*>(&DetourPushState),
            reinterpret_cast<void**>(&g_original_push_state)) == BE_Result_Ok) {
            g_push_state_hook_ready = true;
            Log("Successfully installed CinemachineBrain::PushStateToUnityCamera hook.");
        } else {
            Log("Failed to install the Cinemachine CameraState hook; the first-person "
                "camera stays unavailable.");
        }
    }

    MethodContract* heartbeat = Contract("unity.time.unscaled_delta.get");
    if (heartbeat && heartbeat->resolved) {
        if (g_host->create_hook(g_host->context, kModuleId, heartbeat->pointer,
            reinterpret_cast<void*>(&DetourTimeUnscaledDelta),
            reinterpret_cast<void**>(&g_original_time_unscaled_delta)) != BE_Result_Ok) {
            Log("Failed to install unscaled time heartbeat hook; camera hook fallback remains active.");
        }
    }
    return true;
}

BE_Result BE_CALL Initialize(const BE_HostApiV1* host) {
    if (!host || host->abi_version != BETTER_ENDFIELD_MODULE_ABI_V1 ||
        !host->resolve_method || !host->create_hook || !host->runtime_invoke ||
        !host->object_unbox || !host->log) {
        return BE_Result_InvalidArgument;
    }
    g_host = host;
    g_state.store(ModuleState::Resolving, std::memory_order_release);
    if (!ResolveContracts()) {
        g_state.store(ModuleState::ContractMismatch, std::memory_order_release);
        return BE_Result_ContractMismatch;
    }
    if (!InstallHook()) {
        g_state.store(ModuleState::Failed, std::memory_order_release);
        Log("Failed to install camera update hook.");
        return BE_Result_Failed;
    }
    g_input_thread_stop.store(false, std::memory_order_release);
    g_input_thread = std::thread(InputThreadMain);
    g_state.store(ModuleState::Ready, std::memory_order_release);
    Log("BetterEndfield.Camera module initialized successfully.");
    return BE_Result_Ok;
}

BE_Result BE_CALL ConfigurationChanged(const char* raw_configuration) {
    const CameraConfiguration config = ParseConfiguration(raw_configuration);
    const bool free_camera = config.enabled && config.free_camera_enabled;
    const bool anti_dither = config.enabled && config.disable_dither_enabled;
    const bool first_person = config.enabled && config.first_person_camera_enabled;
    const bool was_free = g_free_camera_enabled.load(std::memory_order_acquire);
    const bool was_first_person = g_first_person_camera_enabled.load(std::memory_order_acquire);

    g_free_camera_enabled.store(free_camera, std::memory_order_release);
    g_disable_dither_enabled.store(anti_dither, std::memory_order_release);
    g_pause_enabled.store(config.pause_enabled, std::memory_order_release);
    g_first_person_camera_enabled.store(first_person, std::memory_order_release);
    g_first_person_hide_head.store(config.first_person_hide_head, std::memory_order_release);
    g_first_person_fill_neck_hole.store(config.first_person_fill_neck_hole,
        std::memory_order_release);
    g_diagnostics_enabled.store(config.diagnostics, std::memory_order_release);
    g_movement_speed.store(config.movement_speed, std::memory_order_release);
    g_field_of_view.store(config.field_of_view, std::memory_order_release);
    g_first_person_fov.store(config.first_person_fov, std::memory_order_release);
    g_first_person_neck_plug_scale.store(config.first_person_neck_plug_scale,
        std::memory_order_release);
    g_toggle_key.store(config.toggle_key, std::memory_order_release);
    g_pause_key.store(config.pause_key, std::memory_order_release);
    g_first_person_key.store(config.first_person_key, std::memory_order_release);
    g_mouse_look_enabled.store(config.mouse_look, std::memory_order_release);
    g_mouse_invert_y.store(config.mouse_invert_y, std::memory_order_release);
    g_mouse_sensitivity.store(config.mouse_sensitivity, std::memory_order_release);
    g_free_smoothing.store(config.smoothing, std::memory_order_release);
    g_motion_preset.store(config.motion_preset, std::memory_order_release);
    g_motion_speed.store(config.motion_speed, std::memory_order_release);
    g_orbit_speed.store(config.orbit_speed, std::memory_order_release);
    g_motion_duration.store(config.motion_duration, std::memory_order_release);
    g_motion_target_height.store(config.motion_target_height, std::memory_order_release);
    g_keyframe_segment_seconds.store(config.keyframe_segment_seconds,
        std::memory_order_release);
    g_keyframe_loop.store(config.keyframe_loop, std::memory_order_release);
    g_vmd_camera_scale.store(config.vmd_camera_scale, std::memory_order_release);
    g_vmd_camera_fov_bias.store(config.vmd_camera_fov_bias, std::memory_order_release);
    g_vmd_camera_loop.store(config.vmd_camera_loop, std::memory_order_release);
    {
        std::lock_guard<std::mutex> lock(g_vmd_path_mutex);
        g_vmd_camera_file = config.vmd_camera_file;
    }
    g_roll_left_key.store(config.roll_left_key, std::memory_order_release);
    g_roll_right_key.store(config.roll_right_key, std::memory_order_release);
    g_fov_wide_key.store(config.fov_wide_key, std::memory_order_release);
    g_fov_narrow_key.store(config.fov_narrow_key, std::memory_order_release);
    g_view_reset_key.store(config.view_reset_key, std::memory_order_release);
    g_motion_key.store(config.motion_key, std::memory_order_release);
    g_keyframe_add_key.store(config.keyframe_add_key, std::memory_order_release);
    g_keyframe_play_key.store(config.keyframe_play_key, std::memory_order_release);
    g_keyframe_clear_key.store(config.keyframe_clear_key, std::memory_order_release);
    g_vmd_play_key.store(config.vmd_play_key, std::memory_order_release);

    if (was_free && !free_camera) {
        g_force_exit_request.store(true, std::memory_order_release);
    }
    if (was_first_person && !first_person) {
        g_first_person_exit_request.store(true, std::memory_order_release);
    }

    g_state.store(free_camera || anti_dither || first_person
        ? ModuleState::Active
        : ModuleState::Disabled, std::memory_order_release);

    char buffer[384];
    std::snprintf(buffer, sizeof(buffer),
        "Camera configuration applied: enabled=%s, free_camera=%s, first_person=%s, "
        "hide_head=%s, fill_neck_hole=%s, neck_plug_scale=%.2f, anti_dither=%s, "
        "pause_enabled=%s, free_hotkey_vk=%d, fp_hotkey_vk=%d, fp_fov=%.1f",
        config.enabled ? "true" : "false", free_camera ? "true" : "false",
        first_person ? "true" : "false",
        config.first_person_hide_head ? "true" : "false",
        config.first_person_fill_neck_hole ? "true" : "false",
        config.first_person_neck_plug_scale,
        anti_dither ? "true" : "false",
        config.pause_enabled ? "true" : "false", config.toggle_key,
        config.first_person_key, config.first_person_fov);
    Log(buffer);
    std::snprintf(buffer, sizeof(buffer),
        "Free camera extras: mouse_look=%s, sensitivity=%.2f, smoothing=%.2f, "
        "motion_preset=%d, motion_speed=%.2f, orbit_speed=%.1f, keyframe_segment=%.1f, "
        "vmd_file=%s, vmd_scale=%.3f",
        config.mouse_look ? "true" : "false", config.mouse_sensitivity,
        config.smoothing, config.motion_preset, config.motion_speed,
        config.orbit_speed, config.keyframe_segment_seconds,
        config.vmd_camera_file.empty() ? "<none>" : "set", config.vmd_camera_scale);
    Log(buffer);
    return BE_Result_Ok;
}

void BE_CALL Shutdown() {
    g_free_camera_enabled.store(false, std::memory_order_release);
    g_disable_dither_enabled.store(false, std::memory_order_release);
    g_first_person_camera_enabled.store(false, std::memory_order_release);
    g_input_thread_stop.store(true, std::memory_order_release);
    if (g_input_thread.joinable()) {
        g_input_thread.join();
    }
    ExitFirstPerson("shutdown");
    ExitFreeCamera("shutdown");
    ReleaseCameraRoot();
    if (g_host && g_host->release_module_hooks) {
        g_host->release_module_hooks(g_host->context, kModuleId);
    }
    g_push_state_hook_ready = false;
    g_original_push_state = nullptr;
    g_original_tail_late_tick = nullptr;
    g_original_camera_tick = nullptr;
    g_original_time_unscaled_delta = nullptr;
    g_free_camera_active = false;
    g_first_person_active.store(false, std::memory_order_release);
    g_state.store(ModuleState::Stopped, std::memory_order_release);
    g_host = nullptr;
}

const BE_ModuleApiV1 kApi{
    {kModuleId, "Camera Enhancements", "1.6.0", BETTER_ENDFIELD_MODULE_ABI_V1},
    &Initialize,
    &ConfigurationChanged,
    &Shutdown};

} // namespace
} // namespace BetterEndfield::CameraModule

BE_EXPORT const BE_ModuleApiV1* BE_CALL BetterEndfield_GetModuleApiV1() {
    return &BetterEndfield::CameraModule::kApi;
}
