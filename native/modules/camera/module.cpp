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
    bool pause_game_enabled = false;
    bool diagnostics = true;
    float movement_speed = 5.0f;
    float field_of_view = 60.0f;
    int toggle_key = '9';
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
std::atomic_bool g_pause_game_enabled{false};
std::atomic_bool g_diagnostics_enabled{true};
std::atomic<float> g_movement_speed{5.0f};
std::atomic<float> g_field_of_view{60.0f};
std::atomic_int g_toggle_key{'9'};

using CameraTickFn = void(__fastcall*)(void* instance, void* method);
CameraTickFn g_original_camera_tick = nullptr;

bool g_free_camera_contract_ready = false;
bool g_dither_contract_ready = false;
bool g_free_camera_active = false;
bool g_toggle_was_down = false;
uint64_t g_last_tick = 0;
void* g_active_camera = nullptr;
uint32_t g_active_camera_root = 0;
Vector3 g_original_position{};
Vector3 g_free_position{};
float g_original_fov = 60.0f;
float g_original_time_scale = 1.0f;
bool g_changed_time_scale = false;

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

bool KeyDown(int key) {
    return (GetAsyncKeyState(key) & 0x8000) != 0;
}

void ReleaseCameraRoot() {
    if (g_active_camera_root && g_host && g_host->gchandle_free) {
        g_host->gchandle_free(g_host->context, g_active_camera_root);
    }
    g_active_camera_root = 0;
    g_active_camera = nullptr;
}

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
    if (g_changed_time_scale) {
        SetValue(Contract("unity.time.scale.set"), nullptr, g_original_time_scale);
    }

    g_free_camera_active = false;
    g_changed_time_scale = false;
    ReleaseCameraRoot();
    Log(std::string("Free camera disabled: ") + reason);
}

bool EnterFreeCamera() {
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

    if (g_pause_game_enabled.load(std::memory_order_acquire) &&
        GetValue(Contract("unity.time.scale.get"), nullptr, g_original_time_scale)) {
        g_changed_time_scale = SetValue(
            Contract("unity.time.scale.set"), nullptr, 0.0f);
    }

    g_free_camera_active = true;
    Log("Free camera enabled (arrow keys move, PageUp/PageDown change height).");
    return true;
}

void ApplyFreeCamera() {
    const bool should_pause = g_pause_game_enabled.load(std::memory_order_acquire);
    if (should_pause && !g_changed_time_scale &&
        GetValue(Contract("unity.time.scale.get"), nullptr, g_original_time_scale)) {
        g_changed_time_scale = SetValue(
            Contract("unity.time.scale.set"), nullptr, 0.0f);
    } else if (!should_pause && g_changed_time_scale) {
        SetValue(Contract("unity.time.scale.set"), nullptr, g_original_time_scale);
        g_changed_time_scale = false;
    }

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

void PumpFreeCamera() {
    const bool allowed = g_free_camera_enabled.load(std::memory_order_acquire) &&
        g_free_camera_contract_ready;
    const bool toggle_down = KeyDown(g_toggle_key.load(std::memory_order_relaxed));
    const bool toggle_pressed = toggle_down && !g_toggle_was_down;
    g_toggle_was_down = toggle_down;

    if (!allowed) {
        ExitFreeCamera("feature disabled");
        return;
    }
    if (toggle_pressed) {
        if (g_free_camera_active) {
            ExitFreeCamera("toggle hotkey");
        } else {
            EnterFreeCamera();
        }
    }
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
    PumpFreeCamera();
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
        else if (key == "pause_game_enabled")
            config.pause_game_enabled = ParseBoolean(value, config.pause_game_enabled);
        else if (key == "movement_speed")
            config.movement_speed = ParseFloat(value, config.movement_speed);
        else if (key == "field_of_view")
            config.field_of_view = ParseFloat(value, config.field_of_view);
        else if (key == "toggle_hotkey")
            config.toggle_key = ParseVirtualKey(value, config.toggle_key);
        else if (key == "diagnostics")
            config.diagnostics = ParseBoolean(value, config.diagnostics);
    }
    if (config.schema_version < 3) {
        config.pause_game_enabled = false;
        config.toggle_key = '9';
    }
    config.movement_speed = std::clamp(config.movement_speed, 0.1f, 100.0f);
    config.field_of_view = std::clamp(config.field_of_view, 20.0f, 120.0f);
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
    Log(std::string("Camera feature contracts: free_camera=") +
        (g_free_camera_contract_ready ? "ready" : "unavailable") +
        ", anti_dither=" + (g_dither_contract_ready ? "ready" : "unavailable"));
    return g_free_camera_contract_ready || g_dither_contract_ready;
}

bool InstallHook() {
    MethodContract* tick = Contract("camera.process_dither");
    if (!tick || !tick->resolved || !g_host || !g_host->create_hook) {
        return false;
    }
    return g_host->create_hook(g_host->context, kModuleId, tick->pointer,
        reinterpret_cast<void*>(&DetourCameraTick),
        reinterpret_cast<void**>(&g_original_camera_tick)) == BE_Result_Ok;
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
    g_state.store(ModuleState::Ready, std::memory_order_release);
    Log("BetterEndfield.Camera module initialized successfully.");
    return BE_Result_Ok;
}

BE_Result BE_CALL ConfigurationChanged(const char* raw_configuration) {
    const CameraConfiguration config = ParseConfiguration(raw_configuration);
    const bool free_camera = config.enabled && config.free_camera_enabled;
    const bool anti_dither = config.enabled && config.disable_dither_enabled;
    g_free_camera_enabled.store(free_camera, std::memory_order_release);
    g_disable_dither_enabled.store(anti_dither, std::memory_order_release);
    g_pause_game_enabled.store(config.pause_game_enabled, std::memory_order_release);
    g_diagnostics_enabled.store(config.diagnostics, std::memory_order_release);
    g_movement_speed.store(config.movement_speed, std::memory_order_release);
    g_field_of_view.store(config.field_of_view, std::memory_order_release);
    g_toggle_key.store(config.toggle_key, std::memory_order_release);
    g_state.store(free_camera || anti_dither
        ? ModuleState::Active
        : ModuleState::Disabled, std::memory_order_release);

    char buffer[320];
    std::snprintf(buffer, sizeof(buffer),
        "Camera configuration applied: enabled=%s, free_camera=%s, anti_dither=%s, "
        "pause=%s, hotkey_vk=%d, speed=%.2f, fov=%.1f",
        config.enabled ? "true" : "false", free_camera ? "true" : "false",
        anti_dither ? "true" : "false", config.pause_game_enabled ? "true" : "false",
        config.toggle_key, config.movement_speed, config.field_of_view);
    Log(buffer);
    return BE_Result_Ok;
}

void BE_CALL Shutdown() {
    g_free_camera_enabled.store(false, std::memory_order_release);
    g_disable_dither_enabled.store(false, std::memory_order_release);
    // Host shutdown occurs while the game is closing. Release managed roots;
    // never invoke Unity from this worker-thread path.
    ReleaseCameraRoot();
    if (g_host && g_host->release_module_hooks) {
        g_host->release_module_hooks(g_host->context, kModuleId);
    }
    g_free_camera_active = false;
    g_state.store(ModuleState::Stopped, std::memory_order_release);
    g_host = nullptr;
}

const BE_ModuleApiV1 kApi{
    {kModuleId, "Camera Enhancements", "1.2.0", BETTER_ENDFIELD_MODULE_ABI_V1},
    &Initialize,
    &ConfigurationChanged,
    &Shutdown};

} // namespace
} // namespace BetterEndfield::CameraModule

BE_EXPORT const BE_ModuleApiV1* BE_CALL BetterEndfield_GetModuleApiV1() {
    return &BetterEndfield::CameraModule::kApi;
}
