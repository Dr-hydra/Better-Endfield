#include "BetterEndfield/ModuleApi.h"

#include "touch_input.h"

#include <Windows.h>

#include <algorithm>
#include <array>
#include <atomic>
#include <cctype>
#include <cstdint>
#include <cstdio>
#include <cstdlib>
#include <cstring>
#include <mutex>
#include <string>
#include <string_view>
#include <vector>

namespace BetterEndfield::UiModule {
namespace {

constexpr char kModuleId[] = "betterendfield.ui";

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

struct UiConfiguration {
    bool enabled = false;
    bool mobile_ui_enabled = false;
    bool hide_uid_enabled = false;
    bool hide_hud_enabled = false;
    int hide_hud_hotkey = '0';
    // Reporting the client as Android/cloud is not needed for the touch layout
    // and is visible to account validation, so it is opt-in and separate.
    bool platform_spoof_enabled = false;
    bool diagnostics = true;
};

struct MethodContract {
    const char* key;
    BE_MethodDescriptorV1 descriptor;
    bool required;
    void* pointer = nullptr;
    const void* method_info = nullptr;
    bool resolved = false;
};

const BE_HostApiV1* g_host = nullptr;
std::atomic<ModuleState> g_state{ModuleState::Created};
UiConfiguration g_configuration;
std::mutex g_configuration_mutex;
std::atomic_bool g_mobile_ui_enabled{false};
std::atomic_bool g_hide_uid_enabled{false};
std::atomic_bool g_hide_hud_enabled{false};
std::atomic_int g_hide_hud_hotkey{'0'};
std::atomic_bool g_platform_spoof_enabled{false};
std::atomic_bool g_diagnostics_enabled{true};

// Touch styling only needs the input type; platform identity is a separate,
// account-visible claim.
inline bool PlatformSpoofActive() {
    return g_platform_spoof_enabled.load(std::memory_order_relaxed);
}

using GetBoolFn = bool(__fastcall*)(void* method);
using GetInstanceBoolFn = bool(__fastcall*)(void* instance, void* method);
using GetInt32Fn = int32_t(__fastcall*)(void* method);
using ChangeInputTypeFn = void(__fastcall*)(int32_t type, void* method);
using AwakeFn = void(__fastcall*)(void* instance, void* method);
using GetObjectFn = void*(__fastcall*)(void* instance, void* method);
using SetActiveFn = void(__fastcall*)(void* instance, bool active, void* method);

GetBoolFn g_original_get_is_mobile = nullptr;
GetBoolFn g_original_get_is_mobile_user = nullptr;
GetBoolFn g_original_get_is_android = nullptr;
GetBoolFn g_original_get_is_android_user = nullptr;
GetBoolFn g_original_get_supports_touch = nullptr;
GetBoolFn g_original_get_using_touch = nullptr;
GetBoolFn g_original_get_using_keyboard = nullptr;
GetBoolFn g_original_get_using_controller = nullptr;
GetInt32Fn g_original_get_input_type = nullptr;
GetBoolFn g_original_get_is_pc = nullptr;
GetBoolFn g_original_get_is_pc_user = nullptr;
GetBoolFn g_original_get_is_pc_or_console = nullptr;
GetBoolFn g_original_get_is_pc_or_console_user = nullptr;
GetBoolFn g_original_get_is_console = nullptr;
GetBoolFn g_original_get_is_console_user = nullptr;
GetInt32Fn g_original_get_platform = nullptr;
GetInt32Fn g_original_get_user_platform = nullptr;
ChangeInputTypeFn g_original_change_input_type = nullptr;

GetBoolFn g_original_app_get_is_mobile_platform = nullptr;
GetInt32Fn g_original_app_get_platform = nullptr;
GetBoolFn g_original_app_get_is_cloud_game = nullptr;

GetBoolFn g_original_cloud_util_is_cloud_game = nullptr;
GetBoolFn g_original_cloud_game_get_enabled = nullptr;
GetBoolFn g_original_cloud_game_get_is_mobile_platform = nullptr;
GetBoolFn g_original_cloud_game_get_is_pc_platform = nullptr;

AwakeFn g_original_ui_style_awake = nullptr;
AwakeFn g_original_ui_style_update = nullptr;
AwakeFn g_original_event_system_update = nullptr;
GetInstanceBoolFn g_original_camera_get_hide_hud = nullptr;
GetObjectFn g_object_get_name = nullptr;
SetActiveFn g_original_game_object_set_active = nullptr;

// MethodInfo for Beyond.DeviceInfo.ChangeInputType, used to push an input-type
// switch through runtime_invoke from the game's own thread.
const void* g_change_input_type_method = nullptr;
const void* g_object_get_name_method = nullptr;
const void* g_game_action_disable_hud_fade_method = nullptr;
const void* g_event_manager_send_global_method = nullptr;
const void* g_lua_manager_get_instance_method = nullptr;
const void* g_lua_manager_get_event_system_method = nullptr;
const void* g_lua_event_system_dispatch_method = nullptr;
const void* g_game_object_find_method = nullptr;
const void* g_game_object_set_active_method = nullptr;
const void* g_game_object_get_components_method = nullptr;
const void* g_array_get_length_method = nullptr;
const void* g_array_get_value_method = nullptr;
const void* g_behaviour_get_enabled_method = nullptr;
const void* g_behaviour_set_enabled_method = nullptr;
BE_ResolvedClassV1 g_lua_manager_class{};
BE_ResolvedClassV1 g_canvas_class{};
BE_ResolvedClassV1 g_graphic_class{};
// Static backing field behind DeviceInfo.inputType, read to recover the input
// type the game chose for itself before the module overrode it.
const void* g_input_type_field = nullptr;
const void* g_clear_screen_off_field = nullptr;
const void* g_clear_screen_on_field = nullptr;
const void* g_lua_event_system_field = nullptr;

// Bumped by every configuration push; the main-thread pump replays the switch
// whenever it falls behind, so a hot toggle reaches already-live UI.
std::atomic_uint32_t g_desired_generation{0};
std::atomic_uint32_t g_applied_generation{0};
std::atomic_int32_t g_restore_input_type{-1};
std::atomic_uint32_t g_uid_desired_generation{0};
std::atomic_uint32_t g_uid_applied_generation{0};
std::atomic_uint64_t g_next_uid_scan_tick{0};
std::atomic_uint64_t g_next_hud_scan_tick{0};

struct HiddenHudCanvas {
    void* object = nullptr;
    uint32_t root_handle = 0;
    bool original_enabled = false;
};

std::mutex g_hidden_hud_mutex;
std::vector<HiddenHudCanvas> g_hidden_hud_canvases;
bool g_hud_hidden = false;
bool g_hud_hotkey_was_down = false;

struct HiddenUidObject {
    void* object = nullptr;
    uint32_t root_handle = 0;
};

std::mutex g_hidden_uid_mutex;
std::vector<HiddenUidObject> g_hidden_uid_objects;

constexpr std::array<std::string_view, 4> kUidObjectNames{
    "uidpanelpanel",
    "watermarkgridpanel",
    "watermarkcell",
    "bottomnodewatermarkui",
};

constexpr std::array<const char*, 8> kUidFindNames{
    "UIDPanelPanel",
    "WaterMarkGridPanel",
    "WaterMarkCell",
    "BottomNodeWatermarkUI",
    "uidpanelpanel",
    "watermarkgridpanel",
    "watermarkcell",
    "bottomnodewatermarkui",
};

// UnityEngine.RuntimePlatform.Android = 11
constexpr int32_t kPlatformAndroid = 11;
// Beyond.DeviceInfo.InputType.Touch = 1
constexpr int32_t kInputTypeTouch = 1;

MethodContract g_contracts[]{
    {"device.is_mobile",
        {"Common.Beyond.dll", "Beyond", "DeviceInfo", "get_isMobile",
            nullptr, "System.Boolean", 0},
        false},
    {"device.is_mobile_user",
        {"Common.Beyond.dll", "Beyond", "DeviceInfo", "get_isMobileUser",
            nullptr, "System.Boolean", 0},
        false},
    {"device.is_android",
        {"Common.Beyond.dll", "Beyond", "DeviceInfo", "get_isAndroid",
            nullptr, "System.Boolean", 0},
        false},
    {"device.is_android_user",
        {"Common.Beyond.dll", "Beyond", "DeviceInfo", "get_isAndroidUser",
            nullptr, "System.Boolean", 0},
        false},
    {"device.supports_touch",
        {"Common.Beyond.dll", "Beyond", "DeviceInfo", "get_supportsTouch",
            nullptr, "System.Boolean", 0},
        false},
    {"device.using_touch",
        {"Common.Beyond.dll", "Beyond", "DeviceInfo", "get_usingTouch",
            nullptr, "System.Boolean", 0},
        false},
    {"device.using_keyboard",
        {"Common.Beyond.dll", "Beyond", "DeviceInfo", "get_usingKeyboard",
            nullptr, "System.Boolean", 0},
        false},
    {"device.using_controller",
        {"Common.Beyond.dll", "Beyond", "DeviceInfo", "get_usingController",
            nullptr, "System.Boolean", 0},
        false},
    {"device.input_type",
        {"Common.Beyond.dll", "Beyond", "DeviceInfo", "get_inputType",
            nullptr, nullptr, 0},
        false},
    {"device.is_pc",
        {"Common.Beyond.dll", "Beyond", "DeviceInfo", "get_isPC",
            nullptr, "System.Boolean", 0},
        false},
    {"device.is_pc_user",
        {"Common.Beyond.dll", "Beyond", "DeviceInfo", "get_isPCUser",
            nullptr, "System.Boolean", 0},
        false},
    {"device.is_pc_or_console",
        {"Common.Beyond.dll", "Beyond", "DeviceInfo", "get_isPCorConsole",
            nullptr, "System.Boolean", 0},
        false},
    {"device.is_pc_or_console_user",
        {"Common.Beyond.dll", "Beyond", "DeviceInfo", "get_isPCorConsoleUser",
            nullptr, "System.Boolean", 0},
        false},
    {"device.is_console",
        {"Common.Beyond.dll", "Beyond", "DeviceInfo", "get_isConsole",
            nullptr, "System.Boolean", 0},
        false},
    {"device.is_console_user",
        {"Common.Beyond.dll", "Beyond", "DeviceInfo", "get_isConsoleUser",
            nullptr, "System.Boolean", 0},
        false},
    {"device.platform",
        {"Common.Beyond.dll", "Beyond", "DeviceInfo", "get_platform",
            nullptr, nullptr, 0},
        false},
    {"device.user_platform",
        {"Common.Beyond.dll", "Beyond", "DeviceInfo", "get_userPlatform",
            nullptr, nullptr, 0},
        false},
    {"device.change_input_type",
        {"Common.Beyond.dll", "Beyond", "DeviceInfo", "ChangeInputType",
            nullptr, "System.Void", 1},
        false},
    {"app.is_mobile_platform",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Application", "get_isMobilePlatform",
            nullptr, "System.Boolean", 0},
        false},
    {"app.platform",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Application", "get_platform",
            nullptr, nullptr, 0},
        false},
    {"app.is_cloud_game",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Application", "get_isCloudGame",
            nullptr, "System.Boolean", 0},
        false},
    {"cloud_util.is_cloud_game",
        {"Common.Beyond.dll", "Beyond", "CloudGameUtility", "IsCloudGame",
            nullptr, "System.Boolean", 0},
        false},
    {"cloud_game.enabled",
        {"Common.Beyond.dll", "Beyond", "CloudGame", "get_enabled",
            nullptr, "System.Boolean", 0},
        false},
    {"cloud_game.is_mobile_platform",
        {"Common.Beyond.dll", "Beyond", "CloudGame", "get_isMobilePlatform",
            nullptr, "System.Boolean", 0},
        false},
    {"cloud_game.is_pc_platform",
        {"Common.Beyond.dll", "Beyond", "CloudGame", "get_isPCPlatform",
            nullptr, "System.Boolean", 0},
        false},
    // Runs on the game's UI thread for every styled widget that comes alive,
    // which is where the module is allowed to invoke managed code.
    {"ui_style.awake",
        {"UI.Beyond.dll", "Beyond.UI", "UIStyleByState", "Awake",
            nullptr, "System.Void", 0},
        false},
    {"ui_style.update",
        {"UI.Beyond.dll", "Beyond.UI", "UIStyleByState", "UpdateStyle",
            nullptr, "System.Void", 0},
        false},
    {"event_system.update",
        {"UnityEngine.UI.dll", "UnityEngine.EventSystems", "EventSystem", "Update",
            nullptr, "System.Void", 0},
        false},
    {"camera_controller.hide_hud",
        {"Gameplay.Beyond.dll", "Beyond.Gameplay.View", "CameraControllerBase",
            "get_hideHUD", nullptr, "System.Boolean", 0},
        false},
    {"game_action.disable_hud_fade",
        {"Gameplay.Beyond.dll", "Beyond.Gameplay.Actions", "GameAction",
            "DisableHudFade", "System.Boolean", "System.Void", 1},
        false},
    {"event_manager.send_global",
        {"Common.Beyond.dll", "Beyond", "EventManager",
            "SendGlobal", "System.Int32", "System.Void", 1},
        false},
    {"lua_manager.instance",
        {"Lua.Beyond.dll", "Beyond.Lua", "LuaManager",
            "get_instance", nullptr, nullptr, 0},
        false},
    {"lua_manager.event_system",
        {"Lua.Beyond.dll", "Beyond.Lua", "LuaManager",
            "get_luaEventSystem", nullptr, nullptr, 0},
        false},
    {"lua_event_system.dispatch",
        {"Lua.Beyond.dll", "Beyond.Lua", "LuaEventSystem",
            "DispatchEvent", "System.String", "System.Void", 1},
        false},
    {"object.get_name",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Object", "get_name",
            nullptr, "System.String", 0},
        false},
    {"game_object.find",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "GameObject", "Find",
            "System.String", "UnityEngine.GameObject", 1},
        false},
    {"game_object.set_active",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "GameObject", "SetActive",
            "System.Boolean", "System.Void", 1},
        false},
    {"game_object.get_components",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "GameObject",
            "GetComponentsInChildren", "System.Type|System.Boolean",
            "UnityEngine.Component[]", 2},
        false},
    {"array.get_length",
        {"mscorlib.dll", "System", "Array", "GetLength",
            "System.Int32", "System.Int32", 1},
        false},
    {"array.get_value",
        {"mscorlib.dll", "System", "Array", "GetValue",
            "System.Int32", "System.Object", 1},
        false},
    {"behaviour.get_enabled",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Behaviour", "get_enabled",
            nullptr, "System.Boolean", 0},
        false},
    {"behaviour.set_enabled",
        {"UnityEngine.CoreModule.dll", "UnityEngine", "Behaviour", "set_enabled",
            "System.Boolean", "System.Void", 1},
        false},
};

void Log(const std::string& message) {
    if (!g_host || !g_host->log) {
        return;
    }
    g_host->log(g_host->context, kModuleId, message.c_str());
}

MethodContract* Contract(std::string_view key) {
    for (auto& contract : g_contracts) {
        if (contract.key == key) {
            return &contract;
        }
    }
    return nullptr;
}

void* Invoke(const void* method_info, void* instance, void** parameters) {
    if (!method_info || !g_host || !g_host->runtime_invoke) {
        return nullptr;
    }
    void* exception = nullptr;
    void* result = g_host->runtime_invoke(
        g_host->context, method_info, instance, parameters, &exception);
    return exception ? nullptr : result;
}

void DiscoverLuaEventSystemAccessor() {
    if ((!g_lua_manager_class.class_info) ||
        (g_lua_manager_get_event_system_method && g_lua_event_system_field)) {
        return;
    }

    HMODULE game_assembly = GetModuleHandleW(L"GameAssembly.dll");
    if (!game_assembly) {
        Log("Lua event-system discovery skipped: GameAssembly is unavailable.");
        return;
    }

    using ClassGetParentFn = void*(__cdecl*)(void* klass);
    using ClassGetMethodsFn = void*(__cdecl*)(void* klass, void** iterator);
    using ClassGetFieldsFn = void*(__cdecl*)(void* klass, void** iterator);
    using MethodGetNameFn = const char*(__cdecl*)(const void* method);
    using MethodGetParameterCountFn = uint32_t(__cdecl*)(const void* method);
    using FieldGetNameFn = const char*(__cdecl*)(const void* field);

    const auto class_get_parent = reinterpret_cast<ClassGetParentFn>(
        GetProcAddress(game_assembly, "il2cpp_class_get_parent"));
    const auto class_get_methods = reinterpret_cast<ClassGetMethodsFn>(
        GetProcAddress(game_assembly, "il2cpp_class_get_methods"));
    const auto class_get_fields = reinterpret_cast<ClassGetFieldsFn>(
        GetProcAddress(game_assembly, "il2cpp_class_get_fields"));
    const auto method_get_name = reinterpret_cast<MethodGetNameFn>(
        GetProcAddress(game_assembly, "il2cpp_method_get_name"));
    const auto method_get_parameter_count =
        reinterpret_cast<MethodGetParameterCountFn>(
            GetProcAddress(game_assembly, "il2cpp_method_get_param_count"));
    const auto field_get_name = reinterpret_cast<FieldGetNameFn>(
        GetProcAddress(game_assembly, "il2cpp_field_get_name"));
    if (!class_get_parent || !class_get_methods || !class_get_fields ||
        !method_get_name || !method_get_parameter_count || !field_get_name) {
        Log("Lua event-system discovery skipped: IL2CPP reflection exports are incomplete.");
        return;
    }

    void* klass = const_cast<void*>(g_lua_manager_class.class_info);
    for (int depth = 0; klass && depth < 16; ++depth) {
        if (!g_lua_manager_get_event_system_method) {
            void* iterator = nullptr;
            while (void* method = class_get_methods(klass, &iterator)) {
                const char* name = method_get_name(method);
                if (name && std::string_view(name) == "get_luaEventSystem" &&
                    method_get_parameter_count(method) == 0) {
                    g_lua_manager_get_event_system_method = method;
                    Log("Discovered inherited method: lua_manager.event_system");
                    break;
                }
            }
        }

        if (!g_lua_event_system_field) {
            void* iterator = nullptr;
            while (void* field = class_get_fields(klass, &iterator)) {
                const char* name = field_get_name(field);
                if (!name) {
                    continue;
                }
                std::string normalized(name);
                std::transform(normalized.begin(), normalized.end(),
                    normalized.begin(), [](unsigned char c) {
                        return static_cast<char>(std::tolower(c));
                    });
                if (normalized.find("luaeventsystem") != std::string::npos) {
                    g_lua_event_system_field = field;
                    Log(std::string("Discovered Lua event-system field: ") + name);
                    break;
                }
            }
        }

        if (g_lua_manager_get_event_system_method || g_lua_event_system_field) {
            return;
        }
        klass = class_get_parent(klass);
    }
    Log("Lua event-system accessor was not found in LuaManager hierarchy.");
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

void* SafeGetObjectName(void* instance) {
    __try {
        return g_object_get_name(
            instance, const_cast<void*>(g_object_get_name_method));
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return nullptr;
    }
}

std::string NormalizedObjectName(void* instance) {
    if (!instance || !g_object_get_name || !g_host ||
        !g_host->copy_managed_string) {
        return {};
    }

    void* managed_name = SafeGetObjectName(instance);
    char buffer[256]{};
    if (!managed_name || g_host->copy_managed_string(
            g_host->context, managed_name, buffer, sizeof(buffer)) <= 0) {
        return {};
    }

    std::string name(buffer);
    std::transform(name.begin(), name.end(), name.begin(),
        [](unsigned char c) { return static_cast<char>(std::tolower(c)); });
    constexpr std::string_view clone_suffix = "(clone)";
    if (name.ends_with(clone_suffix)) {
        name.resize(name.size() - clone_suffix.size());
    }
    while (!name.empty() && std::isspace(static_cast<unsigned char>(name.back()))) {
        name.pop_back();
    }
    return name;
}

bool IsUidObject(void* instance) {
    const std::string name = NormalizedObjectName(instance);
    return std::find(kUidObjectNames.begin(), kUidObjectNames.end(), name) !=
        kUidObjectNames.end();
}

void TrackHiddenUidObject(void* object) {
    if (!object) {
        return;
    }
    std::lock_guard lock(g_hidden_uid_mutex);
    if (std::any_of(g_hidden_uid_objects.begin(), g_hidden_uid_objects.end(),
            [object](const HiddenUidObject& item) { return item.object == object; })) {
        return;
    }
    const uint32_t handle = g_host && g_host->gchandle_new
        ? g_host->gchandle_new(g_host->context, object, 0)
        : 0;
    g_hidden_uid_objects.push_back({object, handle});
}

void ReleaseHiddenUidRoots() {
    std::lock_guard lock(g_hidden_uid_mutex);
    if (g_host && g_host->gchandle_free) {
        for (const auto& item : g_hidden_uid_objects) {
            if (item.root_handle) {
                g_host->gchandle_free(g_host->context, item.root_handle);
            }
        }
    }
    g_hidden_uid_objects.clear();
}

void RestoreHiddenUidObjects() {
    std::vector<HiddenUidObject> objects;
    {
        std::lock_guard lock(g_hidden_uid_mutex);
        objects.swap(g_hidden_uid_objects);
    }

    for (const auto& item : objects) {
        bool active = true;
        void* parameters[1]{&active};
        void* exception = nullptr;
        if (g_host && g_host->runtime_invoke && g_game_object_set_active_method) {
            g_host->runtime_invoke(g_host->context,
                g_game_object_set_active_method, item.object, parameters, &exception);
        }
        if (item.root_handle && g_host && g_host->gchandle_free) {
            g_host->gchandle_free(g_host->context, item.root_handle);
        }
    }
}

void FindAndHideUidObjects() {
    if (!g_host || !g_host->runtime_invoke || !g_host->string_new ||
        !g_game_object_find_method || !g_original_game_object_set_active) {
        return;
    }

    for (const char* name : kUidFindNames) {
        // Several WaterMarkCell instances may share one name. Deactivate each
        // active match, then ask Unity again until none remain.
        for (int match = 0; match < 64; ++match) {
            void* managed_name = g_host->string_new(g_host->context, name);
            void* parameters[1]{managed_name};
            void* exception = nullptr;
            void* object = g_host->runtime_invoke(g_host->context,
                g_game_object_find_method, nullptr, parameters, &exception);
            if (exception || !object) {
                break;
            }
            TrackHiddenUidObject(object);
            g_original_game_object_set_active(object, false,
                const_cast<void*>(g_game_object_set_active_method));
        }
    }
}

void PumpUidVisibility() {
    static thread_local bool in_progress = false;
    if (in_progress) {
        return;
    }
    in_progress = true;
    struct Guard {
        bool& flag;
        ~Guard() { flag = false; }
    } guard{in_progress};

    const bool hidden = g_hide_uid_enabled.load(std::memory_order_acquire);
    const uint32_t desired = g_uid_desired_generation.load(std::memory_order_acquire);
    const uint32_t applied = g_uid_applied_generation.load(std::memory_order_relaxed);
    const uint64_t now = GetTickCount64();

    if (!hidden) {
        if (applied != desired) {
            RestoreHiddenUidObjects();
            g_uid_applied_generation.store(desired, std::memory_order_release);
        }
        return;
    }

    if (applied != desired || now >= g_next_uid_scan_tick.load()) {
        FindAndHideUidObjects();
        g_uid_applied_generation.store(desired, std::memory_order_release);
        g_next_uid_scan_tick.store(now + 2000, std::memory_order_release);
    }
}

void* FindGameObject(const char* name) {
    if (!name || !g_host || !g_host->string_new || !g_game_object_find_method) {
        return nullptr;
    }
    void* managed_name = g_host->string_new(g_host->context, name);
    void* parameters[1]{managed_name};
    return Invoke(g_game_object_find_method, nullptr, parameters);
}

int ManagedArrayLength(void* array) {
    int dimension = 0;
    int length = 0;
    void* parameters[1]{&dimension};
    return array && Unbox(Invoke(g_array_get_length_method,
        array, parameters), length) ? length : 0;
}

void* ManagedArrayValue(void* array, int index) {
    void* parameters[1]{&index};
    return array ? Invoke(g_array_get_value_method, array, parameters) : nullptr;
}

bool CanvasEnabled(void* canvas, bool& enabled) {
    return canvas && Unbox(Invoke(g_behaviour_get_enabled_method,
        canvas, nullptr), enabled);
}

void SetCanvasEnabled(void* canvas, bool enabled) {
    void* parameters[1]{&enabled};
    Invoke(g_behaviour_set_enabled_method, canvas, parameters);
}

void TrackAndDisableHudCanvas(void* canvas) {
    if (!canvas) {
        return;
    }
    {
        std::lock_guard lock(g_hidden_hud_mutex);
        const auto found = std::find_if(
            g_hidden_hud_canvases.begin(), g_hidden_hud_canvases.end(),
            [canvas](const HiddenHudCanvas& item) { return item.object == canvas; });
        if (found == g_hidden_hud_canvases.end()) {
            bool originally_enabled = false;
            if (!CanvasEnabled(canvas, originally_enabled)) {
                return;
            }
            const uint32_t handle = g_host && g_host->gchandle_new
                ? g_host->gchandle_new(g_host->context, canvas, 0)
                : 0;
            g_hidden_hud_canvases.push_back(
                {canvas, handle, originally_enabled});
        }
    }
    SetCanvasEnabled(canvas, false);
}

int FindAndHideHudCanvases() {
    if ((!g_canvas_class.type_object && !g_graphic_class.type_object) ||
        !g_game_object_get_components_method) {
        return -2;
    }
    void* root = FindGameObject("MainHudRoot");
    if (!root) {
        root = FindGameObject("MainHudRoot(Clone)");
    }
    if (!root) {
        root = FindGameObject("mainhudroot");
    }
    if (!root) {
        return -1;
    }

    const auto disable_components = [root](void* component_type) {
        if (!component_type) {
            return 0;
        }
        bool include_inactive = true;
        void* parameters[2]{component_type, &include_inactive};
        void* components = Invoke(
            g_game_object_get_components_method, root, parameters);
        const int length = std::min(ManagedArrayLength(components), 2048);
        for (int index = 0; index < length; ++index) {
            TrackAndDisableHudCanvas(ManagedArrayValue(components, index));
        }
        return length;
    };

    // A disabled parent Canvas suppresses every child Graphic at once. The
    // Graphic path is a fallback for HUD prefab variants without a local Canvas.
    const int canvas_count = disable_components(g_canvas_class.type_object);
    if (canvas_count > 0) {
        return canvas_count;
    }
    return disable_components(g_graphic_class.type_object);
}

void RestoreHudCanvases() {
    std::vector<HiddenHudCanvas> canvases;
    {
        std::lock_guard lock(g_hidden_hud_mutex);
        canvases.swap(g_hidden_hud_canvases);
    }
    for (const auto& item : canvases) {
        SetCanvasEnabled(item.object, item.original_enabled);
        if (item.root_handle && g_host && g_host->gchandle_free) {
            g_host->gchandle_free(g_host->context, item.root_handle);
        }
    }
}

void ReleaseHudCanvasRoots() {
    std::lock_guard lock(g_hidden_hud_mutex);
    if (g_host && g_host->gchandle_free) {
        for (const auto& item : g_hidden_hud_canvases) {
            if (item.root_handle) {
                g_host->gchandle_free(g_host->context, item.root_handle);
            }
        }
    }
    g_hidden_hud_canvases.clear();
}

bool InvokeStaticAction(const void* method_info, void** parameters) {
    if (!method_info || !g_host || !g_host->runtime_invoke) {
        return false;
    }
    void* exception = nullptr;
    g_host->runtime_invoke(
        g_host->context, method_info, nullptr, parameters, &exception);
    return exception == nullptr;
}

struct HudVisibilityResult {
    bool clear_screen = false;
    bool hud_fade = false;
};

void* GetLuaEventSystem() {
    void* manager = Invoke(g_lua_manager_get_instance_method, nullptr, nullptr);
    if (!manager) {
        return nullptr;
    }
    if (void* event_system = Invoke(
            g_lua_manager_get_event_system_method, manager, nullptr)) {
        return event_system;
    }
    return g_lua_event_system_field && g_host && g_host->field_get_value_object
        ? g_host->field_get_value_object(
              g_host->context, g_lua_event_system_field, manager)
        : nullptr;
}

bool DispatchClearScreenEvent(bool show_hud) {
    if (g_event_manager_send_global_method && g_clear_screen_off_field &&
        g_clear_screen_on_field && g_host && g_host->runtime_invoke &&
        g_host->field_get_value_object) {
        const void* event_field = show_hud
            ? g_clear_screen_off_field
            : g_clear_screen_on_field;
        void* boxed_event_key = g_host->field_get_value_object(
            g_host->context, event_field, nullptr);
        int32_t event_key = 0;
        if (Unbox(boxed_event_key, event_key)) {
            void* parameters[1]{&event_key};
            void* exception = nullptr;
            g_host->runtime_invoke(g_host->context,
                g_event_manager_send_global_method, nullptr, parameters,
                &exception);
            if (!exception) {
                return true;
            }
        }
    }

    // Compatibility fallback for clients where the native event-bus contract
    // is unavailable but LuaManager still exposes its LuaEventSystem instance.
    if (!g_lua_event_system_dispatch_method || !g_host ||
        !g_host->runtime_invoke || !g_host->string_new) {
        return false;
    }
    void* event_system = GetLuaEventSystem();
    if (!event_system) {
        return false;
    }
    void* event_name = g_host->string_new(g_host->context,
        show_hud ? "CLEAR_SCREEN_OFF" : "CLEAR_SCREEN_ON");
    void* parameters[1]{event_name};
    void* exception = nullptr;
    g_host->runtime_invoke(g_host->context,
        g_lua_event_system_dispatch_method, event_system, parameters, &exception);
    return exception == nullptr;
}

HudVisibilityResult ApplyGameHudVisibility(bool show_hud) {
    HudVisibilityResult result{};
    void* fade_parameters[1]{&show_hud};

    result.clear_screen = DispatchClearScreenEvent(show_hud);
    result.hud_fade = InvokeStaticAction(
        g_game_action_disable_hud_fade_method, fade_parameters);
    return result;
}

std::string HudVisibilityStatus(const HudVisibilityResult& result) {
    return std::string("clear_screen=") +
        (result.clear_screen ? "applied" : "unavailable") +
        ", hud_fade=" +
        (result.hud_fade ? "applied" : "unavailable");
}

void PumpHudVisibility() {
    const bool allowed = g_hide_hud_enabled.load(std::memory_order_acquire);
    const bool hotkey_down = allowed &&
        (GetAsyncKeyState(g_hide_hud_hotkey.load(std::memory_order_relaxed)) &
            0x8000) != 0;
    const bool hotkey_pressed = hotkey_down && !g_hud_hotkey_was_down;
    g_hud_hotkey_was_down = hotkey_down;

    if (!allowed) {
        if (g_hud_hidden) {
            const HudVisibilityResult action = ApplyGameHudVisibility(true);
            RestoreHudCanvases();
            g_hud_hidden = false;
            Log(std::string("All-HUD hiding disabled; restored HUD: ") +
                HudVisibilityStatus(action));
        }
        return;
    }

    if (hotkey_pressed) {
        g_hud_hidden = !g_hud_hidden;
        if (g_hud_hidden) {
            const HudVisibilityResult action = ApplyGameHudVisibility(false);
            const int fallback_count = FindAndHideHudCanvases();
            g_next_hud_scan_tick.store(GetTickCount64() + 2000,
                std::memory_order_release);
            Log(std::string("All HUD hide requested by hotkey: ") +
                HudVisibilityStatus(action) + ", camera_contract=" +
                (g_original_camera_get_hide_hud ? "ready" : "unavailable") +
                ", fallback_components=" + std::to_string(fallback_count));
        } else {
            const HudVisibilityResult action = ApplyGameHudVisibility(true);
            RestoreHudCanvases();
            Log(std::string("All HUD restored by hotkey: ") +
                HudVisibilityStatus(action));
        }
    }

    const uint64_t now = GetTickCount64();
    if (g_hud_hidden && now >= g_next_hud_scan_tick.load()) {
        FindAndHideHudCanvases();
        g_next_hud_scan_tick.store(now + 2000, std::memory_order_release);
    }
}

bool __fastcall DetourGetIsMobile(void* method) {
    if (PlatformSpoofActive()) {
        return true;
    }
    return g_original_get_is_mobile ? g_original_get_is_mobile(method) : false;
}

bool __fastcall DetourGetIsMobileUser(void* method) {
    if (PlatformSpoofActive()) {
        return true;
    }
    return g_original_get_is_mobile_user ? g_original_get_is_mobile_user(method) : false;
}

bool __fastcall DetourGetIsAndroid(void* method) {
    if (PlatformSpoofActive()) {
        return true;
    }
    return g_original_get_is_android ? g_original_get_is_android(method) : false;
}

bool __fastcall DetourGetIsAndroidUser(void* method) {
    if (PlatformSpoofActive()) {
        return true;
    }
    return g_original_get_is_android_user ? g_original_get_is_android_user(method) : false;
}

bool __fastcall DetourGetSupportsTouch(void* method) {
    if (g_mobile_ui_enabled.load(std::memory_order_relaxed)) {
        return true;
    }
    return g_original_get_supports_touch ? g_original_get_supports_touch(method) : false;
}

bool __fastcall DetourGetUsingTouch(void* method) {
    if (g_mobile_ui_enabled.load(std::memory_order_relaxed)) {
        return true;
    }
    return g_original_get_using_touch ? g_original_get_using_touch(method) : false;
}

bool __fastcall DetourGetUsingKeyboard(void* method) {
    if (g_mobile_ui_enabled.load(std::memory_order_relaxed)) {
        return false;
    }
    return g_original_get_using_keyboard ? g_original_get_using_keyboard(method) : true;
}

bool __fastcall DetourGetUsingController(void* method) {
    if (g_mobile_ui_enabled.load(std::memory_order_relaxed)) {
        return false;
    }
    return g_original_get_using_controller ? g_original_get_using_controller(method) : false;
}

int32_t __fastcall DetourGetInputType(void* method) {
    if (g_mobile_ui_enabled.load(std::memory_order_relaxed)) {
        return kInputTypeTouch;
    }
    return g_original_get_input_type ? g_original_get_input_type(method) : 0;
}

bool __fastcall DetourGetIsPC(void* method) {
    if (PlatformSpoofActive()) {
        return false;
    }
    return g_original_get_is_pc ? g_original_get_is_pc(method) : true;
}

bool __fastcall DetourGetIsPCUser(void* method) {
    if (PlatformSpoofActive()) {
        return false;
    }
    return g_original_get_is_pc_user ? g_original_get_is_pc_user(method) : true;
}

bool __fastcall DetourGetIsPCorConsole(void* method) {
    if (PlatformSpoofActive()) {
        return false;
    }
    return g_original_get_is_pc_or_console ? g_original_get_is_pc_or_console(method) : true;
}

bool __fastcall DetourGetIsPCorConsoleUser(void* method) {
    if (PlatformSpoofActive()) {
        return false;
    }
    return g_original_get_is_pc_or_console_user ? g_original_get_is_pc_or_console_user(method) : true;
}

bool __fastcall DetourGetIsConsole(void* method) {
    if (PlatformSpoofActive()) {
        return false;
    }
    return g_original_get_is_console ? g_original_get_is_console(method) : false;
}

bool __fastcall DetourGetIsConsoleUser(void* method) {
    if (PlatformSpoofActive()) {
        return false;
    }
    return g_original_get_is_console_user ? g_original_get_is_console_user(method) : false;
}

int32_t __fastcall DetourGetPlatform(void* method) {
    if (PlatformSpoofActive()) {
        return kPlatformAndroid;
    }
    return g_original_get_platform ? g_original_get_platform(method) : 2;
}

int32_t __fastcall DetourGetUserPlatform(void* method) {
    if (PlatformSpoofActive()) {
        return kPlatformAndroid;
    }
    return g_original_get_user_platform ? g_original_get_user_platform(method) : 2;
}

void __fastcall DetourChangeInputType(int32_t type, void* method) {
    if (g_mobile_ui_enabled.load(std::memory_order_relaxed)) {
        type = kInputTypeTouch;
    }
    if (g_original_change_input_type) {
        g_original_change_input_type(type, method);
    }
}

bool __fastcall DetourAppGetIsMobilePlatform(void* method) {
    if (PlatformSpoofActive()) {
        return true;
    }
    return g_original_app_get_is_mobile_platform ? g_original_app_get_is_mobile_platform(method) : false;
}

int32_t __fastcall DetourAppGetPlatform(void* method) {
    if (PlatformSpoofActive()) {
        return kPlatformAndroid;
    }
    return g_original_app_get_platform ? g_original_app_get_platform(method) : 2;
}

bool __fastcall DetourAppGetIsCloudGame(void* method) {
    if (PlatformSpoofActive()) {
        return true;
    }
    return g_original_app_get_is_cloud_game ? g_original_app_get_is_cloud_game(method) : false;
}

bool __fastcall DetourCloudUtilIsCloudGame(void* method) {
    if (PlatformSpoofActive()) {
        return true;
    }
    return g_original_cloud_util_is_cloud_game ? g_original_cloud_util_is_cloud_game(method) : false;
}

bool __fastcall DetourCloudGameGetEnabled(void* method) {
    if (PlatformSpoofActive()) {
        return true;
    }
    return g_original_cloud_game_get_enabled ? g_original_cloud_game_get_enabled(method) : false;
}

bool __fastcall DetourCloudGameGetIsMobilePlatform(void* method) {
    if (PlatformSpoofActive()) {
        return true;
    }
    return g_original_cloud_game_get_is_mobile_platform ? g_original_cloud_game_get_is_mobile_platform(method) : false;
}

bool __fastcall DetourCloudGameGetIsPCPlatform(void* method) {
    if (PlatformSpoofActive()) {
        return false;
    }
    return g_original_cloud_game_get_is_pc_platform ? g_original_cloud_game_get_is_pc_platform(method) : true;
}

// Reads the DeviceInfo.inputType backing field through the host's boxing
// helpers.  Hooking the getter is not enough: IL2CPP emitted two identical
// copies of get_inputType and the one published in the metadata is the copy
// almost nothing calls, so the field itself is the only reliable source.
bool TryReadInputType(int32_t& value) {
    if (!g_host || !g_input_type_field || !g_host->field_get_value_object ||
        !g_host->object_unbox) {
        return false;
    }
    void* boxed = g_host->field_get_value_object(
        g_host->context, g_input_type_field, nullptr);
    if (!boxed) {
        return false;
    }
    void* raw = g_host->object_unbox(g_host->context, boxed);
    if (!raw) {
        return false;
    }
    value = *static_cast<int32_t*>(raw);
    return true;
}

// Drives the game's own ChangeInputType so it writes the backing field and
// raises onInputTypeChanged.  Every UIStyleByState subscribes to that event in
// Awake, so this is what makes already-instantiated panels re-apply their
// touch or desktop layout; overriding the getters cannot reach them.
void PumpInputType() {
    const uint32_t desired = g_desired_generation.load(std::memory_order_acquire);
    if (g_applied_generation.load(std::memory_order_relaxed) == desired) {
        return;
    }
    // The switch re-enters through every UIStyleByState it refreshes; let the
    // outermost call own the update.
    static thread_local bool in_progress = false;
    if (in_progress) {
        return;
    }
    in_progress = true;
    struct Guard {
        bool& flag;
        ~Guard() { flag = false; }
    } guard{in_progress};
    if (!g_host || !g_host->runtime_invoke || !g_change_input_type_method) {
        g_applied_generation.store(desired, std::memory_order_release);
        return;
    }

    const bool active = g_mobile_ui_enabled.load(std::memory_order_acquire);

    int32_t current = 0;
    const bool have_current = TryReadInputType(current);
    if (active && have_current && current != kInputTypeTouch) {
        // Remember what the game had picked so disabling can hand it back.
        g_restore_input_type.store(current, std::memory_order_release);
    }

    int32_t target = kInputTypeTouch;
    if (!active) {
        const int32_t restore = g_restore_input_type.load(std::memory_order_acquire);
        target = restore >= 0 ? restore : 0;
    }

    if (have_current && current == target) {
        g_applied_generation.store(desired, std::memory_order_release);
        return;
    }

    void* parameters[1] = {&target};
    void* exception = nullptr;
    g_host->runtime_invoke(g_host->context, g_change_input_type_method,
        nullptr, parameters, &exception);

    g_applied_generation.store(desired, std::memory_order_release);

    if (g_diagnostics_enabled.load(std::memory_order_relaxed)) {
        // Read back: the game can refuse a switch, so "sent" is not "applied".
        int32_t observed = -1;
        TryReadInputType(observed);
        char buffer[200];
        std::snprintf(buffer, sizeof(buffer),
            "Input type pushed to %d (was %d, now %d, active=%s)%s%s",
            target, have_current ? current : -1, observed,
            active ? "true" : "false",
            observed == target ? "" : " [REJECTED]",
            exception ? " [managed exception]" : "");
        Log(buffer);
    }
}

void __fastcall DetourUIStyleByStateAwake(void* instance, void* method) {
    if (g_original_ui_style_awake) {
        g_original_ui_style_awake(instance, method);
    }
    // Awake has already registered this widget with onInputTypeChanged, so a
    // switch raised now reaches it along with every earlier one.
    PumpInputType();
    PumpUidVisibility();
    PumpHudVisibility();
}

void __fastcall DetourUIStyleByStateUpdateStyle(void* instance, void* method) {
    // Catches a toggle made while no new widget is being created; the pump is
    // a no-op once the pending switch has been applied.
    PumpInputType();
    PumpUidVisibility();
    PumpHudVisibility();
    if (g_original_ui_style_update) {
        g_original_ui_style_update(instance, method);
    }
}

void __fastcall DetourEventSystemUpdate(void* instance, void* method) {
    if (g_original_event_system_update) {
        g_original_event_system_update(instance, method);
    }
    PumpInputType();
    PumpUidVisibility();
    PumpHudVisibility();
}

bool __fastcall DetourCameraGetHideHud(void* instance, void* method) {
    if (g_hide_hud_enabled.load(std::memory_order_acquire) && g_hud_hidden) {
        return true;
    }
    return g_original_camera_get_hide_hud
        ? g_original_camera_get_hide_hud(instance, method)
        : false;
}

void __fastcall DetourGameObjectSetActive(
    void* instance, bool active, void* method) {
    if (active && g_hide_uid_enabled.load(std::memory_order_acquire) &&
        IsUidObject(instance)) {
        TrackHiddenUidObject(instance);
        active = false;
    }
    if (g_original_game_object_set_active) {
        g_original_game_object_set_active(instance, active, method);
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

UiConfiguration ParseConfigurationText(const char* raw_configuration) {
    UiConfiguration config;
    if (!raw_configuration) {
        return config;
    }

    std::string_view text(raw_configuration);
    // The host hands each module its own section body, without the header line.
    // Start inside the section so those bare keys are read, while still
    // honouring a header if a whole file is ever passed in.
    bool in_section = true;
    size_t line_start = 0;

    while (line_start < text.size()) {
        size_t line_end = text.find_first_of("\r\n", line_start);
        if (line_end == std::string_view::npos) {
            line_end = text.size();
        }
        std::string line = Trim(text.substr(line_start, line_end - line_start));
        line_start = text.find_first_not_of("\r\n", line_end);

        if (line.empty() || line.front() == ';') {
            continue;
        }

        if (line.front() == '[' && line.back() == ']') {
            in_section = (line == "[betterendfield.ui]");
            continue;
        }

        if (!in_section) {
            continue;
        }

        const size_t equals = line.find('=');
        if (equals == std::string_view::npos) {
            continue;
        }

        const std::string key = Trim(line.substr(0, equals));
        const std::string value = Trim(line.substr(equals + 1));

        if (key == "enabled") {
            config.enabled = ParseBoolean(value, config.enabled);
        } else if (key == "mobile_ui_enabled") {
            config.mobile_ui_enabled = ParseBoolean(value, config.mobile_ui_enabled);
        } else if (key == "hide_uid_enabled") {
            config.hide_uid_enabled = ParseBoolean(value, config.hide_uid_enabled);
        } else if (key == "hide_hud_enabled") {
            config.hide_hud_enabled = ParseBoolean(value, config.hide_hud_enabled);
        } else if (key == "hide_hud_hotkey") {
            config.hide_hud_hotkey = ParseVirtualKey(value, config.hide_hud_hotkey);
        } else if (key == "platform_spoof_enabled") {
            config.platform_spoof_enabled =
                ParseBoolean(value, config.platform_spoof_enabled);
        } else if (key == "diagnostics") {
            config.diagnostics = ParseBoolean(value, config.diagnostics);
        }
    }

    return config;
}

bool ResolveContracts() {
    if (!g_host || !g_host->resolve_method) {
        return false;
    }

    int resolved_count = 0;
    for (auto& contract : g_contracts) {
        BE_ResolvedMethodV1 resolved{};
        const BE_Result result = g_host->resolve_method(
            g_host->context, &contract.descriptor, &resolved);
        if (result == BE_Result_Ok && resolved.method_pointer != nullptr) {
            contract.pointer = resolved.method_pointer;
            contract.method_info = resolved.method_info;
            contract.resolved = true;
            resolved_count++;
            if (std::string_view(contract.key) == "device.change_input_type") {
                g_change_input_type_method = resolved.method_info;
            } else if (std::string_view(contract.key) ==
                "game_action.disable_hud_fade") {
                g_game_action_disable_hud_fade_method = resolved.method_info;
            } else if (std::string_view(contract.key) ==
                "event_manager.send_global") {
                g_event_manager_send_global_method = resolved.method_info;
            } else if (std::string_view(contract.key) ==
                "lua_manager.instance") {
                g_lua_manager_get_instance_method = resolved.method_info;
            } else if (std::string_view(contract.key) ==
                "lua_manager.event_system") {
                g_lua_manager_get_event_system_method = resolved.method_info;
            } else if (std::string_view(contract.key) ==
                "lua_event_system.dispatch") {
                g_lua_event_system_dispatch_method = resolved.method_info;
            } else if (std::string_view(contract.key) == "object.get_name") {
                g_object_get_name = reinterpret_cast<GetObjectFn>(resolved.method_pointer);
                g_object_get_name_method = resolved.method_info;
            } else if (std::string_view(contract.key) == "game_object.find") {
                g_game_object_find_method = resolved.method_info;
            } else if (std::string_view(contract.key) == "game_object.set_active") {
                g_game_object_set_active_method = resolved.method_info;
            } else if (std::string_view(contract.key) == "game_object.get_components") {
                g_game_object_get_components_method = resolved.method_info;
            } else if (std::string_view(contract.key) == "array.get_length") {
                g_array_get_length_method = resolved.method_info;
            } else if (std::string_view(contract.key) == "array.get_value") {
                g_array_get_value_method = resolved.method_info;
            } else if (std::string_view(contract.key) == "behaviour.get_enabled") {
                g_behaviour_get_enabled_method = resolved.method_info;
            } else if (std::string_view(contract.key) == "behaviour.set_enabled") {
                g_behaviour_set_enabled_method = resolved.method_info;
            }
            Log(std::string("Resolved method contract: ") + contract.key);
        } else {
            contract.resolved = false;
            Log(std::string("Optional method not found: ") + contract.key);
        }
    }

    if (g_host->resolve_field) {
        const BE_FieldDescriptorV1 descriptor{
            "Common.Beyond.dll", "Beyond", "DeviceInfo",
            "<inputType>k__BackingField", nullptr};
        BE_ResolvedFieldV1 field{};
        if (g_host->resolve_field(g_host->context, &descriptor, &field) ==
                BE_Result_Ok &&
            field.field_info != nullptr) {
            g_input_type_field = field.field_info;
            Log("Resolved field contract: device.input_type_backing");
        } else {
            Log("Optional field not found: device.input_type_backing");
        }

        const BE_FieldDescriptorV1 clear_screen_off_descriptor{
            "Common.Beyond.dll", "Beyond", "PredefinedEventKeys",
            "CLEAR_SCREEN_OFF", "System.Int32"};
        field = {};
        if (g_host->resolve_field(g_host->context,
                &clear_screen_off_descriptor, &field) == BE_Result_Ok &&
            field.field_info != nullptr) {
            g_clear_screen_off_field = field.field_info;
            Log("Resolved field contract: predefined_event.clear_screen_off");
        } else {
            Log("Optional field not found: predefined_event.clear_screen_off");
        }

        const BE_FieldDescriptorV1 clear_screen_on_descriptor{
            "Common.Beyond.dll", "Beyond", "PredefinedEventKeys",
            "CLEAR_SCREEN_ON", "System.Int32"};
        field = {};
        if (g_host->resolve_field(g_host->context,
                &clear_screen_on_descriptor, &field) == BE_Result_Ok &&
            field.field_info != nullptr) {
            g_clear_screen_on_field = field.field_info;
            Log("Resolved field contract: predefined_event.clear_screen_on");
        } else {
            Log("Optional field not found: predefined_event.clear_screen_on");
        }

        const BE_FieldDescriptorV1 lua_event_descriptor{
            "Lua.Beyond.dll", "Beyond.Lua", "LuaManager",
            "<luaEventSystem>k__BackingField", nullptr};
        field = {};
        if (g_host->resolve_field(g_host->context, &lua_event_descriptor, &field) ==
                BE_Result_Ok &&
            field.field_info != nullptr) {
            g_lua_event_system_field = field.field_info;
            Log("Resolved field contract: lua_manager.event_system_backing");
        } else {
            Log("Optional field not found: lua_manager.event_system_backing");
        }
    }

    if (g_host->resolve_class &&
        g_host->resolve_class(g_host->context, "Lua.Beyond.dll",
            "Beyond.Lua", "LuaManager", &g_lua_manager_class) == BE_Result_Ok &&
        g_lua_manager_class.class_info) {
        Log("Resolved class contract: lua_manager");
        DiscoverLuaEventSystemAccessor();
    } else {
        g_lua_manager_class = {};
        Log("Optional class not found: lua_manager");
    }

    if (g_host->resolve_class &&
        g_host->resolve_class(g_host->context, "UnityEngine.UIModule.dll",
            "UnityEngine", "Canvas", &g_canvas_class) == BE_Result_Ok &&
        g_canvas_class.type_object) {
        Log("Resolved class contract: unity.canvas");
    } else {
        g_canvas_class = {};
        Log("Optional class not found: unity.canvas");
    }
    if (g_host->resolve_class &&
        g_host->resolve_class(g_host->context, "UnityEngine.UI.dll",
            "UnityEngine.UI", "Graphic", &g_graphic_class) == BE_Result_Ok &&
        g_graphic_class.type_object) {
        Log("Resolved class contract: unity.ui.graphic");
    } else {
        g_graphic_class = {};
        Log("Optional class not found: unity.ui.graphic");
    }

    return resolved_count > 0;
}

bool InstallHooks() {
    if (!g_host || !g_host->create_hook) {
        return false;
    }

    for (auto& contract : g_contracts) {
        if (!contract.resolved || !contract.pointer) {
            continue;
        }

        void* detour = nullptr;
        void** original = nullptr;

        std::string_view key(contract.key);
        if (key == "device.is_mobile") {
            detour = reinterpret_cast<void*>(&DetourGetIsMobile);
            original = reinterpret_cast<void**>(&g_original_get_is_mobile);
        } else if (key == "device.is_mobile_user") {
            detour = reinterpret_cast<void*>(&DetourGetIsMobileUser);
            original = reinterpret_cast<void**>(&g_original_get_is_mobile_user);
        } else if (key == "device.is_android") {
            detour = reinterpret_cast<void*>(&DetourGetIsAndroid);
            original = reinterpret_cast<void**>(&g_original_get_is_android);
        } else if (key == "device.is_android_user") {
            detour = reinterpret_cast<void*>(&DetourGetIsAndroidUser);
            original = reinterpret_cast<void**>(&g_original_get_is_android_user);
        } else if (key == "device.supports_touch") {
            detour = reinterpret_cast<void*>(&DetourGetSupportsTouch);
            original = reinterpret_cast<void**>(&g_original_get_supports_touch);
        } else if (key == "device.using_touch") {
            detour = reinterpret_cast<void*>(&DetourGetUsingTouch);
            original = reinterpret_cast<void**>(&g_original_get_using_touch);
        } else if (key == "device.using_keyboard") {
            detour = reinterpret_cast<void*>(&DetourGetUsingKeyboard);
            original = reinterpret_cast<void**>(&g_original_get_using_keyboard);
        } else if (key == "device.using_controller") {
            detour = reinterpret_cast<void*>(&DetourGetUsingController);
            original = reinterpret_cast<void**>(&g_original_get_using_controller);
        } else if (key == "device.input_type") {
            detour = reinterpret_cast<void*>(&DetourGetInputType);
            original = reinterpret_cast<void**>(&g_original_get_input_type);
        } else if (key == "device.is_pc") {
            detour = reinterpret_cast<void*>(&DetourGetIsPC);
            original = reinterpret_cast<void**>(&g_original_get_is_pc);
        } else if (key == "device.is_pc_user") {
            detour = reinterpret_cast<void*>(&DetourGetIsPCUser);
            original = reinterpret_cast<void**>(&g_original_get_is_pc_user);
        } else if (key == "device.is_pc_or_console") {
            detour = reinterpret_cast<void*>(&DetourGetIsPCorConsole);
            original = reinterpret_cast<void**>(&g_original_get_is_pc_or_console);
        } else if (key == "device.is_pc_or_console_user") {
            detour = reinterpret_cast<void*>(&DetourGetIsPCorConsoleUser);
            original = reinterpret_cast<void**>(&g_original_get_is_pc_or_console_user);
        } else if (key == "device.is_console") {
            detour = reinterpret_cast<void*>(&DetourGetIsConsole);
            original = reinterpret_cast<void**>(&g_original_get_is_console);
        } else if (key == "device.is_console_user") {
            detour = reinterpret_cast<void*>(&DetourGetIsConsoleUser);
            original = reinterpret_cast<void**>(&g_original_get_is_console_user);
        } else if (key == "device.platform") {
            detour = reinterpret_cast<void*>(&DetourGetPlatform);
            original = reinterpret_cast<void**>(&g_original_get_platform);
        } else if (key == "device.user_platform") {
            detour = reinterpret_cast<void*>(&DetourGetUserPlatform);
            original = reinterpret_cast<void**>(&g_original_get_user_platform);
        } else if (key == "device.change_input_type") {
            detour = reinterpret_cast<void*>(&DetourChangeInputType);
            original = reinterpret_cast<void**>(&g_original_change_input_type);
        } else if (key == "app.is_mobile_platform") {
            detour = reinterpret_cast<void*>(&DetourAppGetIsMobilePlatform);
            original = reinterpret_cast<void**>(&g_original_app_get_is_mobile_platform);
        } else if (key == "app.platform") {
            detour = reinterpret_cast<void*>(&DetourAppGetPlatform);
            original = reinterpret_cast<void**>(&g_original_app_get_platform);
        } else if (key == "app.is_cloud_game") {
            detour = reinterpret_cast<void*>(&DetourAppGetIsCloudGame);
            original = reinterpret_cast<void**>(&g_original_app_get_is_cloud_game);
        } else if (key == "cloud_util.is_cloud_game") {
            detour = reinterpret_cast<void*>(&DetourCloudUtilIsCloudGame);
            original = reinterpret_cast<void**>(&g_original_cloud_util_is_cloud_game);
        } else if (key == "cloud_game.enabled") {
            detour = reinterpret_cast<void*>(&DetourCloudGameGetEnabled);
            original = reinterpret_cast<void**>(&g_original_cloud_game_get_enabled);
        } else if (key == "cloud_game.is_mobile_platform") {
            detour = reinterpret_cast<void*>(&DetourCloudGameGetIsMobilePlatform);
            original = reinterpret_cast<void**>(&g_original_cloud_game_get_is_mobile_platform);
        } else if (key == "cloud_game.is_pc_platform") {
            detour = reinterpret_cast<void*>(&DetourCloudGameGetIsPCPlatform);
            original = reinterpret_cast<void**>(&g_original_cloud_game_get_is_pc_platform);
        } else if (key == "ui_style.awake") {
            detour = reinterpret_cast<void*>(&DetourUIStyleByStateAwake);
            original = reinterpret_cast<void**>(&g_original_ui_style_awake);
        } else if (key == "ui_style.update") {
            detour = reinterpret_cast<void*>(&DetourUIStyleByStateUpdateStyle);
            original = reinterpret_cast<void**>(&g_original_ui_style_update);
        } else if (key == "event_system.update") {
            detour = reinterpret_cast<void*>(&DetourEventSystemUpdate);
            original = reinterpret_cast<void**>(&g_original_event_system_update);
        } else if (key == "camera_controller.hide_hud") {
            detour = reinterpret_cast<void*>(&DetourCameraGetHideHud);
            original = reinterpret_cast<void**>(&g_original_camera_get_hide_hud);
        } else if (key == "game_object.set_active") {
            detour = reinterpret_cast<void*>(&DetourGameObjectSetActive);
            original = reinterpret_cast<void**>(&g_original_game_object_set_active);
        }

        if (detour && original) {
            const BE_Result res = g_host->create_hook(
                g_host->context, kModuleId, contract.pointer, detour, original);
            if (res != BE_Result_Ok) {
                Log(std::string("Failed to install hook for: ") + contract.key);
            } else {
                Log(std::string("Successfully hooked: ") + contract.key);
            }
        }
    }

    return true;
}

void StopHooks() {
    if (g_host && g_host->release_module_hooks) {
        g_host->release_module_hooks(g_host->context, kModuleId);
    }
}

BE_Result BE_CALL Initialize(const BE_HostApiV1* host) {
    if (!host || host->abi_version != BETTER_ENDFIELD_MODULE_ABI_V1) {
        return BE_Result_InvalidArgument;
    }
    g_host = host;
    g_state.store(ModuleState::Resolving);

    if (!ResolveContracts()) {
        g_state.store(ModuleState::ContractMismatch);
        Log("Module contract resolution failed for UI enhancements.");
        return BE_Result_ContractMismatch;
    }

    if (!InstallHooks()) {
        g_state.store(ModuleState::Failed);
        Log("Failed to install UI module hooks.");
        return BE_Result_Failed;
    }

    // The touch layout is unusable without a finger to drive it, so the input
    // half starts with the module and stays gated on the same configuration.
    TouchInput::Start([](const char* message) { Log(message); });

    g_state.store(ModuleState::Ready);
    Log("BetterEndfield.UI module initialized successfully.");
    return BE_Result_Ok;
}

BE_Result BE_CALL ConfigurationChanged(const char* raw_configuration) {
    const UiConfiguration config = ParseConfigurationText(raw_configuration);
    {
        std::lock_guard<std::mutex> lock(g_configuration_mutex);
        g_configuration = config;
    }

    const bool mobile_active = config.enabled && config.mobile_ui_enabled;
    const bool uid_active = config.enabled && config.hide_uid_enabled;
    const bool hud_active = config.enabled && config.hide_hud_enabled;
    const bool active = mobile_active || uid_active || hud_active;
    g_mobile_ui_enabled.store(mobile_active, std::memory_order_release);
    g_hide_uid_enabled.store(uid_active, std::memory_order_release);
    g_hide_hud_enabled.store(hud_active, std::memory_order_release);
    g_hide_hud_hotkey.store(config.hide_hud_hotkey, std::memory_order_release);
    g_platform_spoof_enabled.store(mobile_active && config.platform_spoof_enabled,
        std::memory_order_release);
    g_diagnostics_enabled.store(config.diagnostics, std::memory_order_release);
    TouchInput::SetEnabled(mobile_active);

    Log(std::string("UI Configuration applied: enabled=") + (config.enabled ? "true" : "false") +
        ", mobile_ui_enabled=" + (config.mobile_ui_enabled ? "true" : "false") +
        ", hide_uid_enabled=" + (config.hide_uid_enabled ? "true" : "false") +
        ", hide_hud_enabled=" + (config.hide_hud_enabled ? "true" : "false") +
        ", hide_hud_hotkey_vk=" + std::to_string(config.hide_hud_hotkey) +
        ", platform_spoof=" + (config.platform_spoof_enabled ? "true" : "false") +
        " (effective=" + (active ? "ACTIVE" : "INACTIVE") + ")");

    // Configuration arrives on the host's polling thread, which must not call
    // into managed code.  Record the request and let the UI-thread pump raise
    // the actual input-type switch.
    g_desired_generation.fetch_add(1, std::memory_order_acq_rel);
    g_uid_desired_generation.fetch_add(1, std::memory_order_acq_rel);
    g_next_uid_scan_tick.store(0, std::memory_order_release);
    g_next_hud_scan_tick.store(0, std::memory_order_release);

    g_state.store(active ? ModuleState::Active : ModuleState::Disabled);
    return BE_Result_Ok;
}

void BE_CALL Shutdown() {
    TouchInput::SetEnabled(false);
    TouchInput::Stop();
    g_hide_uid_enabled.store(false, std::memory_order_release);
    g_hide_hud_enabled.store(false, std::memory_order_release);
    ReleaseHiddenUidRoots();
    ReleaseHudCanvasRoots();
    StopHooks();
    g_state.store(ModuleState::Stopped);
    g_mobile_ui_enabled.store(false, std::memory_order_release);
    g_host = nullptr;
}

const BE_ModuleApiV1 kApi{
    {kModuleId, "UI Enhancements", "3.0.2", BETTER_ENDFIELD_MODULE_ABI_V1},
    &Initialize,
    &ConfigurationChanged,
    &Shutdown};

} // namespace
} // namespace BetterEndfield::UiModule

BE_EXPORT const BE_ModuleApiV1* BE_CALL BetterEndfield_GetModuleApiV1() {
    return &BetterEndfield::UiModule::kApi;
}
