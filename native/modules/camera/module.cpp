#include "BetterEndfield/ModuleApi.h"

#include <Windows.h>

#include <algorithm>
#include <atomic>
#include <cctype>
#include <cmath>
#include <cstdint>
#include <cstdio>
#include <cstdlib>
#include <cstring>
#include <string>
#include <string_view>
#include <thread>
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
    bool diagnostics = true;
    float movement_speed = 5.0f;
    float field_of_view = 60.0f;
    float first_person_fov = 75.0f;
    int toggle_key = '9';
    int pause_key = '8';
    int first_person_key = VK_OEM_MINUS; // 0xBD default '-'
};

struct Vector3 {
    float x = 0.0f;
    float y = 0.0f;
    float z = 0.0f;
};

struct MethodContract {
    const char* key;
    BE_MethodDescriptorV1 descriptor;
    void* pointer = nullptr;
    const void* method_info = nullptr;
    bool resolved = false;
};

const BE_HostApiV1* g_host = nullptr;
std::atomic<ModuleState> g_state{ModuleState::Created};
std::atomic_bool g_free_camera_enabled{false};
std::atomic_bool g_disable_dither_enabled{false};
std::atomic_bool g_pause_enabled{false};
std::atomic_bool g_first_person_camera_enabled{false};
std::atomic_bool g_first_person_hide_head{true};
std::atomic_bool g_diagnostics_enabled{true};
std::atomic<float> g_movement_speed{5.0f};
std::atomic<float> g_field_of_view{60.0f};
std::atomic<float> g_first_person_fov{75.0f};
std::atomic_int g_toggle_key{'9'};
std::atomic_int g_pause_key{'8'};
std::atomic_int g_first_person_key{VK_OEM_MINUS};

using CameraTickFn = void(__fastcall*)(void* instance, void* method);
CameraTickFn g_original_camera_tick = nullptr;
using TimeUnscaledDeltaFn = float(__fastcall*)(void* method);
TimeUnscaledDeltaFn g_original_time_unscaled_delta = nullptr;

bool g_free_camera_contract_ready = false;
bool g_dither_contract_ready = false;
bool g_time_heartbeat_contract_ready = false;
bool g_first_person_contract_ready = false;

bool g_free_camera_active = false;
std::atomic_bool g_toggle_request{false};
std::atomic_bool g_pause_request{false};
std::atomic_bool g_force_exit_request{false};

bool g_first_person_active = false;
std::atomic_bool g_first_person_toggle_request{false};
std::atomic_bool g_first_person_exit_request{false};

std::atomic_bool g_input_thread_stop{false};
std::thread g_input_thread;
uint64_t g_last_tick = 0;
void* g_active_camera = nullptr;
uint32_t g_active_camera_root = 0;
Vector3 g_original_position{};
Vector3 g_free_position{};
float g_original_fov = 60.0f;
float g_original_time_scale = 1.0f;
bool g_changed_time_scale = false;

// First-person state tracking
BE_ResolvedClassV1 g_skinned_mesh_renderer_class{};
BE_ResolvedClassV1 g_mesh_class{};
BE_ResolvedFieldV1 g_override_target_field{};

struct HiddenRendererRecord {
    void* renderer = nullptr;
    void* original_mesh = nullptr;
    uint32_t renderer_handle = 0;
    uint32_t mesh_handle = 0;
};
std::vector<HiddenRendererRecord> g_hidden_head_renderers;
void* g_empty_mesh = nullptr;
uint32_t g_empty_mesh_handle = 0;
void* g_first_person_head_tf = nullptr;
uint32_t g_first_person_head_root = 0;
void* g_last_known_target = nullptr;
float g_first_person_orig_fov = 60.0f;

MethodContract g_contracts[]{
    {"camera.process_dither",
        {"Gameplay.Beyond.dll", "Beyond.Gameplay.View", "CameraMono",
            "_ProcessDitherByPitch", nullptr, "System.Void", 0}},
    {"camera.force_clear_dither",
        {"Gameplay.Beyond.dll", "Beyond.Gameplay.View", "CameraMono",
            "ForceClearDither", nullptr, "System.Void", 0}},
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
    {"unity.transform.position.get",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "get_position",
            nullptr, "UnityEngine.Vector3", 0}},
    {"unity.transform.position.set",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "set_position",
            "UnityEngine.Vector3", "System.Void", 1}},
    {"unity.transform.forward",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "get_forward",
            nullptr, "UnityEngine.Vector3", 0}},
    {"unity.transform.right",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "get_right",
            nullptr, "UnityEngine.Vector3", 0}},
    {"unity.transform.up",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "get_up",
            nullptr, "UnityEngine.Vector3", 0}},
    {"unity.time.scale.get",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Time", "get_timeScale",
            nullptr, "System.Single", 0}},
    {"unity.time.scale.set",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Time", "set_timeScale",
            "System.Single", "System.Void", 1}},
    {"unity.time.unscaled_delta.get",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Time",
            "get_unscaledDeltaTime", nullptr, "System.Single", 0}},
    {"unity.transform.find",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "Find",
            "System.String", "UnityEngine.Transform", 1}},
    {"unity.transform.root.get",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "get_root",
            nullptr, "UnityEngine.Transform", 0}},
    {"unity.component.game_object",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Component", "get_gameObject",
            nullptr, "UnityEngine.GameObject", 0}},
    {"unity.game_object.get_components_in_children",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "GameObject", "GetComponentsInChildren",
            "System.Type|System.Boolean", "UnityEngine.Component[]", 2}},
    {"unity.game_object.find_with_tag",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "GameObject", "FindWithTag",
            "System.String", "UnityEngine.GameObject", 1}},
    {"unity.object.name.get",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Object", "get_name",
            nullptr, "System.String", 0}},
    {"skinned.get_shared_mesh",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer", "get_sharedMesh",
            nullptr, "UnityEngine.Mesh", 0}},
    {"skinned.set_shared_mesh",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer", "set_sharedMesh",
            "UnityEngine.Mesh", "System.Void", 1}},
    {"renderer.set_shadow_proxy_mesh",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Renderer", "set_shadowProxyMesh",
            "UnityEngine.Mesh", "System.Void", 1}},
    {"skinned.set_update_offscreen",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "SkinnedMeshRenderer", "set_updateWhenOffscreen",
            "System.Boolean", "System.Void", 1}},
    {"unity.mesh.ctor",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Mesh", ".ctor",
            nullptr, "System.Void", 0}},
};

MethodContract* Contract(std::string_view key) {
    for (auto& contract : g_contracts) {
        if (contract.key == key) {
            return &contract;
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

Vector3 Scale(Vector3 value, float scale) {
    return {value.x * scale, value.y * scale, value.z * scale};
}

Vector3 Normalize(Vector3 value) {
    const float length = std::sqrt(
        value.x * value.x + value.y * value.y + value.z * value.z);
    return length > 0.0001f ? Scale(value, 1.0f / length) : Vector3{};
}

constexpr size_t kIl2CppArrayLengthOffset = 0x18;
int ArrayLength(void* array) {
    if (!array) return 0;
    return *reinterpret_cast<const int32_t*>(
        static_cast<const uint8_t*>(array) + kIl2CppArrayLengthOffset);
}

void* ArrayValue(void* array, int index) {
    if (!array || index < 0 || index >= ArrayLength(array)) return nullptr;
    return reinterpret_cast<void* const*>(
        static_cast<const uint8_t*>(array) + 0x20)[index];
}

std::string ReadManagedString(void* managed_string) {
    if (!managed_string || !g_host || !g_host->copy_managed_string) return {};
    char buffer[256]{};
    g_host->copy_managed_string(g_host->context, managed_string, buffer, sizeof(buffer));
    return std::string(buffer);
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

void InputThreadMain() {
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
        const bool first_person_down = focused && first_person_enabled && KeyDown(first_person_key);

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
        Sleep(5);
    }
}

void ReleaseCameraRoot() {
    if (g_active_camera_root && g_host && g_host->gchandle_free) {
        g_host->gchandle_free(g_host->context, g_active_camera_root);
    }
    g_active_camera_root = 0;
    if (!g_free_camera_active && !g_first_person_active) {
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

void ExitFreeCamera(const char* reason) {
    if (!g_free_camera_active) {
        return;
    }

    void* transform = Invoke(Contract("unity.component.transform"),
        g_active_camera, nullptr);
    if (transform) {
        SetValue(Contract("unity.transform.position.set"), transform,
            g_original_position);
    }
    SetValue(Contract("unity.camera.fov.set"), g_active_camera, g_original_fov);
    RestoreWorldPause("free camera exit");

    g_free_camera_active = false;
    g_changed_time_scale = false;
    ReleaseCameraRoot();
    Log(std::string("Free camera disabled: ") + reason);
}

bool EnterFreeCamera() {
    if (g_first_person_active) {
        ExitFirstPerson("switching to free camera");
    }
    void* camera = Invoke(Contract("unity.camera.main"), nullptr, nullptr);
    void* transform = Invoke(Contract("unity.component.transform"), camera, nullptr);
    if (!camera || !transform ||
        !GetValue(Contract("unity.transform.position.get"), transform,
            g_original_position) ||
        !GetValue(Contract("unity.camera.fov.get"), camera, g_original_fov)) {
        Log("Free camera could not capture the active Unity camera.");
        return false;
    }

    g_active_camera = camera;
    g_active_camera_root = g_host && g_host->gchandle_new
        ? g_host->gchandle_new(g_host->context, camera, 0)
        : 0;
    g_free_position = g_original_position;
    g_last_tick = GetTickCount64();

    g_free_camera_active = true;
    Log("Free camera enabled (arrow keys move, PageUp/PageDown change height).");
    return true;
}

void ApplyFreeCamera() {
    void* current_camera = Invoke(Contract("unity.camera.main"), nullptr, nullptr);
    if (!current_camera || current_camera != g_active_camera) {
        ExitFreeCamera("active camera changed");
        return;
    }
    void* transform = Invoke(Contract("unity.component.transform"),
        g_active_camera, nullptr);
    if (!transform) {
        ExitFreeCamera("camera transform unavailable");
        return;
    }

    const uint64_t now = GetTickCount64();
    const float delta_seconds = std::clamp(
        static_cast<float>(now - g_last_tick) / 1000.0f, 0.0f, 0.05f);
    g_last_tick = now;

    Vector3 forward{};
    Vector3 right{};
    Vector3 up{};
    if (!GetValue(Contract("unity.transform.forward"), transform, forward) ||
        !GetValue(Contract("unity.transform.right"), transform, right) ||
        !GetValue(Contract("unity.transform.up"), transform, up)) {
        return;
    }

    Vector3 direction{};
    if (KeyDown(VK_UP)) direction = Add(direction, forward);
    if (KeyDown(VK_DOWN)) direction = Add(direction, Scale(forward, -1.0f));
    if (KeyDown(VK_RIGHT)) direction = Add(direction, right);
    if (KeyDown(VK_LEFT)) direction = Add(direction, Scale(right, -1.0f));
    if (KeyDown(VK_PRIOR)) direction = Add(direction, up);
    if (KeyDown(VK_NEXT)) direction = Add(direction, Scale(up, -1.0f));
    direction = Normalize(direction);

    const float speed = g_movement_speed.load(std::memory_order_relaxed);
    g_free_position = Add(g_free_position, Scale(direction, speed * delta_seconds));

    SetValue(Contract("unity.transform.position.set"), transform, g_free_position);
    SetValue(Contract("unity.camera.fov.set"), g_active_camera,
        g_field_of_view.load(std::memory_order_relaxed));
}

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
            RestoreWorldPause("pause hotkey");
        } else if (GetValue(Contract("unity.time.scale.get"), nullptr,
                g_original_time_scale)) {
            const bool paused = SetValue(Contract("unity.time.scale.set"),
                nullptr, 0.0f);
            if (paused) {
                g_changed_time_scale = true;
                Log("World time frozen by pause hotkey.");
            } else {
                Log("World time freeze failed.");
            }
        }
    }
    if (!g_pause_enabled.load(std::memory_order_acquire)) {
        RestoreWorldPause("pause feature disabled");
    }
}

void PumpFreeCamera() {
    PumpFreeCameraControl();
    if (g_free_camera_active) {
        ApplyFreeCamera();
    }
}

// ---------------------------------------------------------------------------
// First-Person Camera Implementation
// ---------------------------------------------------------------------------

void* GetOverrideTarget(void* camera_mono) {
    if (!camera_mono) return nullptr;
    if (g_override_target_field.field_info && g_host && g_host->field_get_value_object) {
        return g_host->field_get_value_object(g_host->context, g_override_target_field.field_info, camera_mono);
    }
    if (g_override_target_field.offset > 0) {
        return *reinterpret_cast<void**>(static_cast<uint8_t*>(camera_mono) + g_override_target_field.offset);
    }
    return *reinterpret_cast<void**>(static_cast<uint8_t*>(camera_mono) + 0xb8);
}

void HideHeadMeshes(void* root_tf) {
    if (!g_first_person_hide_head.load(std::memory_order_relaxed)) return;
    if (!root_tf || !g_skinned_mesh_renderer_class.type_object) return;

    if (!g_empty_mesh) {
        if (g_mesh_class.class_info && g_host && g_host->object_new) {
            g_empty_mesh = g_host->object_new(g_host->context, g_mesh_class.class_info);
            InvokeVoid(Contract("unity.mesh.ctor"), g_empty_mesh, nullptr);
            if (g_host->gchandle_new) {
                g_empty_mesh_handle = g_host->gchandle_new(g_host->context, g_empty_mesh, 1);
            }
        }
    }
    if (!g_empty_mesh) return;

    void* go = Invoke(Contract("unity.component.game_object"), root_tf, nullptr);
    if (!go) return;

    bool include_inactive = true;
    void* params[2]{g_skinned_mesh_renderer_class.type_object, &include_inactive};
    void* renderers_arr = Invoke(Contract("unity.game_object.get_components_in_children"), go, params);
    const int count = ArrayLength(renderers_arr);
    for (int i = 0; i < count; ++i) {
        void* renderer = ArrayValue(renderers_arr, i);
        if (!renderer) continue;
        void* name_obj = Invoke(Contract("unity.object.name.get"), renderer, nullptr);
        std::string name = ReadManagedString(name_obj);
        std::transform(name.begin(), name.end(), name.begin(),
            [](unsigned char c) { return static_cast<char>(std::tolower(c)); });

        bool is_head = (name.find("face") != std::string::npos ||
                        name.find("hair") != std::string::npos ||
                        name.find("brow") != std::string::npos ||
                        name.find("eyebrow") != std::string::npos ||
                        name.find("iris") != std::string::npos ||
                        name.find("eyeshadow") != std::string::npos ||
                        name.find("hairshadow") != std::string::npos);
        if (is_head) {
            void* orig_mesh = Invoke(Contract("skinned.get_shared_mesh"), renderer, nullptr);
            if (orig_mesh && orig_mesh != g_empty_mesh) {
                HiddenRendererRecord record{};
                record.renderer = renderer;
                record.original_mesh = orig_mesh;
                if (g_host->gchandle_new) {
                    record.renderer_handle = g_host->gchandle_new(g_host->context, renderer, 1);
                    record.mesh_handle = g_host->gchandle_new(g_host->context, orig_mesh, 1);
                }
                g_hidden_head_renderers.push_back(record);

                // Preserve original mesh as shadowProxyMesh so shadow is preserved!
                void* shadow_params[1]{orig_mesh};
                InvokeVoid(Contract("renderer.set_shadow_proxy_mesh"), renderer, shadow_params);

                // Clear visual geometry with empty mesh
                void* empty_params[1]{g_empty_mesh};
                InvokeVoid(Contract("skinned.set_shared_mesh"), renderer, empty_params);

                bool offscreen = true;
                void* offscreen_params[1]{&offscreen};
                InvokeVoid(Contract("skinned.set_update_offscreen"), renderer, offscreen_params);
            }
        }
    }
}

void RestoreHeadMeshes() {
    for (auto& record : g_hidden_head_renderers) {
        if (record.renderer && record.original_mesh) {
            void* mesh_params[1]{record.original_mesh};
            InvokeVoid(Contract("skinned.set_shared_mesh"), record.renderer, mesh_params);
            void* shadow_params[1]{nullptr};
            InvokeVoid(Contract("renderer.set_shadow_proxy_mesh"), record.renderer, shadow_params);
        }
        if (g_host && g_host->gchandle_free) {
            if (record.renderer_handle) g_host->gchandle_free(g_host->context, record.renderer_handle);
            if (record.mesh_handle) g_host->gchandle_free(g_host->context, record.mesh_handle);
        }
    }
    g_hidden_head_renderers.clear();
}

void ExitFirstPerson(const char* reason) {
    if (!g_first_person_active) {
        return;
    }
    RestoreHeadMeshes();
    if (g_active_camera) {
        SetValue(Contract("unity.camera.fov.set"), g_active_camera, g_first_person_orig_fov);
    }
    if (g_first_person_head_root && g_host && g_host->gchandle_free) {
        g_host->gchandle_free(g_host->context, g_first_person_head_root);
    }
    g_first_person_head_root = 0;
    g_first_person_head_tf = nullptr;
    g_last_known_target = nullptr;
    g_first_person_active = false;
    ReleaseCameraRoot();
    Log(std::string("First person camera disabled: ") + reason);
}

bool EnterFirstPerson(void* camera_mono) {
    if (g_free_camera_active) {
        ExitFreeCamera("switching to first person");
    }
    void* camera = Invoke(Contract("unity.camera.main"), nullptr, nullptr);
    if (!camera) {
        Log("First person camera could not capture the active Unity camera.");
        return false;
    }
    g_active_camera = camera;
    if (g_host && g_host->gchandle_new) {
        g_active_camera_root = g_host->gchandle_new(g_host->context, camera, 0);
    }
    GetValue(Contract("unity.camera.fov.get"), camera, g_first_person_orig_fov);

    void* target = GetOverrideTarget(camera_mono);
    if (!target) {
        void* player_tag = g_host->string_new(g_host->context, "Player");
        void* tag_params[1]{player_tag};
        void* player_go = Invoke(Contract("unity.game_object.find_with_tag"), nullptr, tag_params);
        if (player_go) {
            target = Invoke(Contract("unity.component.transform"), player_go, nullptr);
        }
    }

    if (target) {
        g_last_known_target = target;
        void* root = Invoke(Contract("unity.transform.root.get"), target, nullptr);
        if (!root) root = target;
        void* head_path = g_host->string_new(g_host->context,
            "Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_Neck/Bip001_Head");
        void* find_params[1]{head_path};
        g_first_person_head_tf = Invoke(Contract("unity.transform.find"), root, find_params);
        if (!g_first_person_head_tf) {
            g_first_person_head_tf = target;
        }
        if (g_first_person_head_tf && g_host->gchandle_new) {
            g_first_person_head_root = g_host->gchandle_new(g_host->context, g_first_person_head_tf, 0);
        }
        HideHeadMeshes(root);
    }

    g_first_person_active = true;
    Log("First person camera enabled (hotkey: -).");
    return true;
}

void ApplyFirstPersonCamera(void* camera_mono) {
    void* current_camera = Invoke(Contract("unity.camera.main"), nullptr, nullptr);
    if (!current_camera || current_camera != g_active_camera) {
        ExitFirstPerson("active camera changed");
        return;
    }
    void* cam_tf = Invoke(Contract("unity.component.transform"), g_active_camera, nullptr);
    if (!cam_tf) {
        ExitFirstPerson("camera transform unavailable");
        return;
    }

    void* current_target = GetOverrideTarget(camera_mono);
    if (current_target && current_target != g_last_known_target) {
        RestoreHeadMeshes();
        g_last_known_target = current_target;
        void* root = Invoke(Contract("unity.transform.root.get"), current_target, nullptr);
        if (!root) root = current_target;
        void* head_path = g_host->string_new(g_host->context,
            "Root/Bip001/Bip001_Pelvis/Bip001_Spine/Bip001_Spine1/Bip001_Spine2/Bip001_Neck/Bip001_Head");
        void* find_params[1]{head_path};
        g_first_person_head_tf = Invoke(Contract("unity.transform.find"), root, find_params);
        if (!g_first_person_head_tf) {
            g_first_person_head_tf = current_target;
        }
        HideHeadMeshes(root);
    }

    if (!g_first_person_head_tf) {
        return;
    }

    Vector3 head_pos{};
    if (!GetValue(Contract("unity.transform.position.get"), g_first_person_head_tf, head_pos)) {
        return;
    }

    Vector3 cam_fwd{};
    Vector3 cam_up{};
    GetValue(Contract("unity.transform.forward"), cam_tf, cam_fwd);
    GetValue(Contract("unity.transform.up"), cam_tf, cam_up);

    Vector3 eye_pos = Add(head_pos, Add(Scale(cam_fwd, 0.12f), Scale(cam_up, 0.05f)));

    SetValue(Contract("unity.transform.position.set"), cam_tf, eye_pos);
    SetValue(Contract("unity.camera.fov.set"), g_active_camera,
        g_first_person_fov.load(std::memory_order_relaxed));
}

void PumpFirstPersonControl(void* camera_mono) {
    const bool allowed = g_first_person_camera_enabled.load(std::memory_order_acquire) &&
        g_first_person_contract_ready;

    if (!allowed) {
        g_first_person_toggle_request.store(false, std::memory_order_release);
        if (g_first_person_active ||
            g_first_person_exit_request.exchange(false, std::memory_order_acq_rel)) {
            ExitFirstPerson("feature disabled");
        }
        return;
    }

    g_first_person_exit_request.store(false, std::memory_order_release);
    if (g_first_person_toggle_request.exchange(false, std::memory_order_acq_rel)) {
        if (g_first_person_active) {
            ExitFirstPerson("toggle hotkey");
        } else {
            EnterFirstPerson(camera_mono);
        }
    }
}

void PumpFirstPerson(void* camera_mono) {
    PumpFirstPersonControl(camera_mono);
    if (g_first_person_active) {
        ApplyFirstPersonCamera(camera_mono);
    }
}

float __fastcall DetourTimeUnscaledDelta(void* method) {
    const float result = g_original_time_unscaled_delta
        ? g_original_time_unscaled_delta(method)
        : 0.0f;
    if (g_time_heartbeat_contract_ready) {
        PumpFreeCameraControl();
    }
    return result;
}

void __fastcall DetourCameraTick(void* instance, void* method) {
    if (g_original_camera_tick) {
        g_original_camera_tick(instance, method);
    }
    if (g_disable_dither_enabled.load(std::memory_order_acquire) &&
        g_dither_contract_ready) {
        InvokeVoid(Contract("camera.force_clear_dither"), instance, nullptr);
    }
    PumpFreeCamera();
    PumpFirstPerson(instance);
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
    if (text == "1" || text == "true" || text == "yes" || text == "on") return true;
    if (text == "0" || text == "false" || text == "no" || text == "off") return false;
    return default_value;
}

float ParseFloat(std::string_view value, float default_value) {
    const std::string text = Trim(value);
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
        const std::string key = Trim(std::string_view(line).substr(0, equals));
        const std::string value = Trim(std::string_view(line).substr(equals + 1));
        if (key == "schema_version") config.schema_version = std::atoi(value.c_str());
        else if (key == "enabled") config.enabled = ParseBoolean(value, config.enabled);
        else if (key == "free_camera_enabled")
            config.free_camera_enabled = ParseBoolean(value, config.free_camera_enabled);
        else if (key == "disable_dither_enabled")
            config.disable_dither_enabled = ParseBoolean(value, config.disable_dither_enabled);
        else if (key == "pause_enabled" || key == "pause_game_enabled")
            config.pause_enabled = ParseBoolean(value, config.pause_enabled);
        else if (key == "first_person_camera_enabled" || key == "first_person_enabled")
            config.first_person_camera_enabled = ParseBoolean(value, config.first_person_camera_enabled);
        else if (key == "first_person_hide_head")
            config.first_person_hide_head = ParseBoolean(value, config.first_person_hide_head);
        else if (key == "first_person_fov")
            config.first_person_fov = ParseFloat(value, config.first_person_fov);
        else if (key == "first_person_hotkey")
            config.first_person_key = ParseVirtualKey(value, config.first_person_key);
        else if (key == "movement_speed")
            config.movement_speed = ParseFloat(value, config.movement_speed);
        else if (key == "field_of_view")
            config.field_of_view = ParseFloat(value, config.field_of_view);
        else if (key == "toggle_hotkey")
            config.toggle_key = ParseVirtualKey(value, config.toggle_key);
        else if (key == "pause_hotkey")
            config.pause_key = ParseVirtualKey(value, config.pause_key);
        else if (key == "diagnostics")
            config.diagnostics = ParseBoolean(value, config.diagnostics);
    }
    if (config.schema_version < 3) {
        config.pause_enabled = false;
        config.toggle_key = '9';
        config.pause_key = '8';
    }
    config.movement_speed = std::clamp(config.movement_speed, 0.1f, 100.0f);
    config.field_of_view = std::clamp(config.field_of_view, 20.0f, 120.0f);
    config.first_person_fov = std::clamp(config.first_person_fov, 20.0f, 120.0f);
    return config;
}

bool ResolveContracts() {
    if (!g_host || !g_host->resolve_method) {
        return false;
    }
    for (auto& contract : g_contracts) {
        BE_ResolvedMethodV1 resolved{};
        if (g_host->resolve_method(g_host->context, &contract.descriptor, &resolved) ==
                BE_Result_Ok && resolved.method_pointer && resolved.method_info) {
            contract.pointer = resolved.method_pointer;
            contract.method_info = resolved.method_info;
            contract.resolved = true;
            Log(std::string("Resolved method contract: ") + contract.key);
        } else {
            Log(std::string("Method contract not found: ") + contract.key);
        }
    }

    if (g_host && g_host->resolve_class) {
        BE_ResolvedClassV1 skinned_class{};
        if (g_host->resolve_class(g_host->context, "UnityEngine.CoreModule.dll",
                "UnityEngine", "SkinnedMeshRenderer", &skinned_class) == BE_Result_Ok) {
            g_skinned_mesh_renderer_class = skinned_class;
            Log("Resolved class: UnityEngine.SkinnedMeshRenderer");
        }
        BE_ResolvedClassV1 mesh_class{};
        if (g_host->resolve_class(g_host->context, "UnityEngine.CoreModule.dll",
                "UnityEngine", "Mesh", &mesh_class) == BE_Result_Ok) {
            g_mesh_class = mesh_class;
            Log("Resolved class: UnityEngine.Mesh");
        }
    }

    if (g_host && g_host->resolve_field) {
        BE_FieldDescriptorV1 target_desc{
            "Gameplay.Beyond.dll", "Beyond.Gameplay.View", "CameraMono",
            "m_overrideTarget", "UnityEngine.Transform"};
        if (g_host->resolve_field(g_host->context, &target_desc, &g_override_target_field) == BE_Result_Ok) {
            Log("Resolved field: CameraMono::m_overrideTarget");
        }
    }

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
        ready("unity.transform.up") && ready("unity.time.scale.get") &&
        ready("unity.time.scale.set");
    g_time_heartbeat_contract_ready = ready("unity.time.unscaled_delta.get");
    g_first_person_contract_ready = ready("camera.process_dither") &&
        ready("unity.camera.main") && ready("unity.camera.fov.get") &&
        ready("unity.camera.fov.set") && ready("unity.component.transform") &&
        ready("unity.transform.position.get") &&
        ready("unity.transform.position.set") &&
        ready("unity.transform.forward") && ready("unity.transform.up");

    Log(std::string("Camera feature contracts: free_camera=") +
        (g_free_camera_contract_ready ? "ready" : "unavailable") +
        ", first_person=" + (g_first_person_contract_ready ? "ready" : "unavailable") +
        ", anti_dither=" + (g_dither_contract_ready ? "ready" : "unavailable") +
        ", time_heartbeat=" +
        (g_time_heartbeat_contract_ready ? "ready" : "unavailable"));
    return g_free_camera_contract_ready || g_dither_contract_ready || g_first_person_contract_ready;
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
    g_diagnostics_enabled.store(config.diagnostics, std::memory_order_release);
    g_movement_speed.store(config.movement_speed, std::memory_order_release);
    g_field_of_view.store(config.field_of_view, std::memory_order_release);
    g_first_person_fov.store(config.first_person_fov, std::memory_order_release);
    g_toggle_key.store(config.toggle_key, std::memory_order_release);
    g_pause_key.store(config.pause_key, std::memory_order_release);
    g_first_person_key.store(config.first_person_key, std::memory_order_release);

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
        "anti_dither=%s, pause_enabled=%s, free_hotkey_vk=%d, fp_hotkey_vk=%d, fp_fov=%.1f",
        config.enabled ? "true" : "false", free_camera ? "true" : "false",
        first_person ? "true" : "false", anti_dither ? "true" : "false",
        config.pause_enabled ? "true" : "false", config.toggle_key,
        config.first_person_key, config.first_person_fov);
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
    RestoreHeadMeshes();
    if (g_empty_mesh_handle && g_host && g_host->gchandle_free) {
        g_host->gchandle_free(g_host->context, g_empty_mesh_handle);
    }
    g_empty_mesh_handle = 0;
    g_empty_mesh = nullptr;
    ReleaseCameraRoot();
    if (g_host && g_host->release_module_hooks) {
        g_host->release_module_hooks(g_host->context, kModuleId);
    }
    g_free_camera_active = false;
    g_first_person_active = false;
    g_state.store(ModuleState::Stopped, std::memory_order_release);
    g_host = nullptr;
}

const BE_ModuleApiV1 kApi{
    {kModuleId, "Camera Enhancements", "1.3.0", BETTER_ENDFIELD_MODULE_ABI_V1},
    &Initialize,
    &ConfigurationChanged,
    &Shutdown};

} // namespace
} // namespace BetterEndfield::CameraModule

BE_EXPORT const BE_ModuleApiV1* BE_CALL BetterEndfield_GetModuleApiV1() {
    return &BetterEndfield::CameraModule::kApi;
}
