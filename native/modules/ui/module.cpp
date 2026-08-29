#include "BetterEndfield/ModuleApi.h"

#include "touch_input.h"

#include <Windows.h>

#include <algorithm>
#include <array>
#include <atomic>
#include <cctype>
#include <cstdint>
#include <cstdio>
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
std::atomic_bool g_platform_spoof_enabled{false};
std::atomic_bool g_diagnostics_enabled{true};

// Touch styling only needs the input type; platform identity is a separate,
// account-visible claim.
inline bool PlatformSpoofActive() {
    return g_platform_spoof_enabled.load(std::memory_order_relaxed);
}

using GetBoolFn = bool(__fastcall*)(void* method);
using GetInt32Fn = int32_t(__fastcall*)(void* method);
using ChangeInputTypeFn = void(__fastcall*)(int32_t type, void* method);
using AwakeFn = void(__fastcall*)(void* instance, void* method);

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

// MethodInfo for Beyond.DeviceInfo.ChangeInputType, used to push an input-type
// switch through runtime_invoke from the game's own thread.
const void* g_change_input_type_method = nullptr;
// Static backing field behind DeviceInfo.inputType, read to recover the input
// type the game chose for itself before the module overrode it.
const void* g_input_type_field = nullptr;

// Bumped by every configuration push; the main-thread pump replays the switch
// whenever it falls behind, so a hot toggle reaches already-live UI.
std::atomic_uint32_t g_desired_generation{0};
std::atomic_uint32_t g_applied_generation{0};
std::atomic_int32_t g_restore_input_type{-1};

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
};

void Log(const std::string& message) {
    if (!g_host || !g_host->log) {
        return;
    }
    g_host->log(g_host->context, kModuleId, message.c_str());
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
}

void __fastcall DetourUIStyleByStateUpdateStyle(void* instance, void* method) {
    // Catches a toggle made while no new widget is being created; the pump is
    // a no-op once the pending switch has been applied.
    PumpInputType();
    if (g_original_ui_style_update) {
        g_original_ui_style_update(instance, method);
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

    const bool active = config.enabled && config.mobile_ui_enabled;
    g_mobile_ui_enabled.store(active, std::memory_order_release);
    g_platform_spoof_enabled.store(active && config.platform_spoof_enabled,
        std::memory_order_release);
    g_diagnostics_enabled.store(config.diagnostics, std::memory_order_release);
    TouchInput::SetEnabled(active);

    Log(std::string("UI Configuration applied: enabled=") + (config.enabled ? "true" : "false") +
        ", mobile_ui_enabled=" + (config.mobile_ui_enabled ? "true" : "false") +
        ", platform_spoof=" + (config.platform_spoof_enabled ? "true" : "false") +
        " (effective=" + (active ? "ACTIVE" : "INACTIVE") + ")");

    // Configuration arrives on the host's polling thread, which must not call
    // into managed code.  Record the request and let the UI-thread pump raise
    // the actual input-type switch.
    g_desired_generation.fetch_add(1, std::memory_order_acq_rel);

    g_state.store(active ? ModuleState::Active : ModuleState::Disabled);
    return BE_Result_Ok;
}

void BE_CALL Shutdown() {
    TouchInput::SetEnabled(false);
    TouchInput::Stop();
    StopHooks();
    g_state.store(ModuleState::Stopped);
    g_mobile_ui_enabled.store(false, std::memory_order_release);
    g_host = nullptr;
}

const BE_ModuleApiV1 kApi{
    {kModuleId, "UI Enhancements", "2.3.1", BETTER_ENDFIELD_MODULE_ABI_V1},
    &Initialize,
    &ConfigurationChanged,
    &Shutdown};

} // namespace
} // namespace BetterEndfield::UiModule

BE_EXPORT const BE_ModuleApiV1* BE_CALL BetterEndfield_GetModuleApiV1() {
    return &BetterEndfield::UiModule::kApi;
}
