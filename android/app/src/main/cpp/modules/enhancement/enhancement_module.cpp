#include "modules/enhancement/enhancement_module.h"

#include "core/log.h"

#include <algorithm>
#include <array>
#include <cctype>
#include <chrono>
#include <cstdlib>
#include <cstring>
#include <string_view>

namespace betterendfield {
namespace {

constexpr char kConfigurationVariable[] = "BETTER_ENDFIELD_ENHANCEMENT_CONFIG";
constexpr uint64_t kUidScanIntervalMs = 2000;

// Same object names the desktop UI module hides (lower-case, "(Clone)" removed).
constexpr std::array<std::string_view, 4> kUidObjectNames{
    "uidpanelpanel",
    "watermarkgridpanel",
    "watermarkcell",
    "bottomnodewatermarkui",
};

// GameObject.Find is case-sensitive, so both spellings are probed like on desktop.
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

uint64_t MonotonicMilliseconds() {
    return static_cast<uint64_t>(std::chrono::duration_cast<std::chrono::milliseconds>(
        std::chrono::steady_clock::now().time_since_epoch()).count());
}

bool ShouldLog(uint64_t count) {
    return count <= 8 || count % 256 == 0;
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

bool ParseBoolean(std::string_view value, bool default_value) {
    std::string text = Trim(value);
    std::transform(text.begin(), text.end(), text.begin(),
        [](unsigned char c) { return static_cast<char>(std::tolower(c)); });
    if (text == "1" || text == "true" || text == "yes" || text == "on") return true;
    if (text == "0" || text == "false" || text == "no" || text == "off") return false;
    return default_value;
}

const char* Availability(bool requested, bool ready) {
    return !requested ? "off" : (ready ? "ready" : "unavailable");
}

}  // namespace

std::atomic<EnhancementModule*> EnhancementModule::active_{nullptr};
void* EnhancementModule::process_dither_original_ = nullptr;
void* EnhancementModule::ui_style_awake_original_ = nullptr;
void* EnhancementModule::ui_style_update_original_ = nullptr;
void* EnhancementModule::event_system_update_original_ = nullptr;
void* EnhancementModule::game_object_set_active_original_ = nullptr;

const char* EnhancementModule::Id() const {
    return "betterendfield.enhancement";
}

EnhancementModule::~EnhancementModule() {
    active_.store(nullptr, std::memory_order_release);
    hide_uid_enabled_.store(false, std::memory_order_release);
    disable_dither_enabled_.store(false, std::memory_order_release);
    hook_broker_.Remove(game_object_set_active_stub_);
    hook_broker_.Remove(event_system_update_stub_);
    hook_broker_.Remove(ui_style_update_stub_);
    hook_broker_.Remove(ui_style_awake_stub_);
    hook_broker_.Remove(process_dither_stub_);
}

EnhancementModule::Configuration EnhancementModule::ParseConfiguration(const char* text) {
    Configuration configuration;
    if (text == nullptr) {
        return configuration;
    }
    std::string_view remaining(text);
    while (!remaining.empty()) {
        const size_t line_end = remaining.find_first_of("\r\n");
        const std::string line = Trim(remaining.substr(0, line_end));
        remaining = line_end == std::string_view::npos
            ? std::string_view{}
            : remaining.substr(line_end + 1);
        if (line.empty() || line.front() == ';' || line.front() == '#' ||
            line.front() == '[') {
            continue;
        }
        const size_t equals = line.find('=');
        if (equals == std::string::npos) {
            continue;
        }
        const std::string key = Trim(std::string_view(line).substr(0, equals));
        const std::string_view value = std::string_view(line).substr(equals + 1);
        if (key == "hide_uid") {
            configuration.hide_uid = ParseBoolean(value, configuration.hide_uid);
        } else if (key == "disable_dither") {
            configuration.disable_dither = ParseBoolean(value, configuration.disable_dither);
        }
    }
    return configuration;
}

ModuleResult EnhancementModule::Start(Il2CppRuntime& runtime) {
    runtime_ = &runtime;
    configuration_ = ParseConfiguration(std::getenv(kConfigurationVariable));
    if (!configuration_.hide_uid && !configuration_.disable_dither) {
        return {false, "disabled; no enhancement was selected"};
    }

    // Desktop BetterEndfield.Camera: camera.process_dither / camera.force_clear_dither.
    const ResolvedMethod process_dither = runtime.ResolveMethodExact(
        "Gameplay.Beyond.dll", "Beyond.Gameplay.View", "CameraMono",
        "_ProcessDitherByPitch", nullptr, "System.Void", 0);
    force_clear_dither_ = runtime.ResolveMethodExact(
        "Gameplay.Beyond.dll", "Beyond.Gameplay.View", "CameraMono",
        "ForceClearDither", nullptr, "System.Void", 0);

    // Desktop BetterEndfield.UI: ui_style.awake / ui_style.update /
    // event_system.update drive the pump; object.get_name, game_object.find and
    // game_object.set_active do the hiding.
    const ResolvedMethod ui_style_awake = runtime.ResolveMethodExact(
        "UI.Beyond.dll", "Beyond.UI", "UIStyleByState", "Awake",
        nullptr, "System.Void", 0);
    const ResolvedMethod ui_style_update = runtime.ResolveMethodExact(
        "UI.Beyond.dll", "Beyond.UI", "UIStyleByState", "UpdateStyle",
        nullptr, "System.Void", 0);
    const ResolvedMethod event_system_update = runtime.ResolveMethodExact(
        "UnityEngine.UI.dll", "UnityEngine.EventSystems", "EventSystem", "Update",
        nullptr, "System.Void", 0);
    object_get_name_ = runtime.ResolveMethodExact(
        "UnityEngine.CoreModule.dll", "UnityEngine", "Object", "get_name",
        nullptr, "System.String", 0);
    game_object_find_ = runtime.ResolveMethodExact(
        "UnityEngine.CoreModule.dll", "UnityEngine", "GameObject", "Find",
        "System.String", "UnityEngine.GameObject", 1);
    game_object_set_active_ = runtime.ResolveMethodExact(
        "UnityEngine.CoreModule.dll", "UnityEngine", "GameObject", "SetActive",
        "System.Boolean", "System.Void", 1);

    const bool dither_ready = process_dither.entry != nullptr &&
        force_clear_dither_.info != nullptr;
    const bool uid_pump_ready = ui_style_awake.entry != nullptr ||
        ui_style_update.entry != nullptr || event_system_update.entry != nullptr;
    const bool uid_ready = object_get_name_.info != nullptr &&
        game_object_find_.info != nullptr &&
        game_object_set_active_.entry != nullptr && uid_pump_ready;

    const bool want_dither = configuration_.disable_dither && dither_ready;
    const bool want_uid = configuration_.hide_uid && uid_ready;
    const std::string summary = std::string("anti_dither=") +
        Availability(configuration_.disable_dither, dither_ready) +
        " hide_uid=" + Availability(configuration_.hide_uid, uid_ready);
    if (!want_dither && !want_uid) {
        return {false, "contract mismatch; " + summary};
    }

    std::string hook_error;
    if (!hook_broker_.Initialize(hook_error)) {
        return {false, std::move(hook_error)};
    }
    active_.store(this, std::memory_order_release);

    if (want_dither) {
        if (!hook_broker_.Install(
                process_dither.entry,
                reinterpret_cast<void*>(&HookProcessDither),
                &process_dither_original_,
                process_dither_stub_,
                hook_error)) {
            return {false, "camera.process_dither hook failed: " + hook_error};
        }
        disable_dither_enabled_.store(true, std::memory_order_release);
    }

    if (want_uid) {
        if (!hook_broker_.Install(
                game_object_set_active_.entry,
                reinterpret_cast<void*>(&HookGameObjectSetActive),
                &game_object_set_active_original_,
                game_object_set_active_stub_,
                hook_error)) {
            return {false, "game_object.set_active hook failed: " + hook_error};
        }
        // The pump hooks are individually optional on desktop as well; one
        // surviving tick source is enough to keep the scan running.
        int pump_hooks = 0;
        if (ui_style_awake.entry != nullptr && hook_broker_.Install(
                ui_style_awake.entry,
                reinterpret_cast<void*>(&HookUIStyleAwake),
                &ui_style_awake_original_,
                ui_style_awake_stub_,
                hook_error)) {
            ++pump_hooks;
        }
        if (ui_style_update.entry != nullptr && hook_broker_.Install(
                ui_style_update.entry,
                reinterpret_cast<void*>(&HookUIStyleUpdate),
                &ui_style_update_original_,
                ui_style_update_stub_,
                hook_error)) {
            ++pump_hooks;
        }
        if (event_system_update.entry != nullptr && hook_broker_.Install(
                event_system_update.entry,
                reinterpret_cast<void*>(&HookEventSystemUpdate),
                &event_system_update_original_,
                event_system_update_stub_,
                hook_error)) {
            ++pump_hooks;
        }
        if (pump_hooks == 0) {
            return {false, "hide_uid pump hooks failed: " + hook_error};
        }
        next_uid_scan_ms_.store(0, std::memory_order_release);
        hide_uid_enabled_.store(true, std::memory_order_release);
    }

    return {true, "desktop enhancement hooks active; " + summary};
}

void EnhancementModule::HookProcessDither(void* instance, const void* method_info) {
    const auto original = reinterpret_cast<VoidInstanceFn>(process_dither_original_);
    if (original != nullptr) {
        original(instance, method_info);
    }
    EnhancementModule* module = active_.load(std::memory_order_acquire);
    if (module == nullptr || module->runtime_ == nullptr ||
        !module->disable_dither_enabled_.load(std::memory_order_acquire) ||
        module->force_clear_dither_.info == nullptr) {
        return;
    }
    // Same as desktop: let the game run its own pitch-based dither update,
    // then ask it to clear the result through its own cleanup path.
    void* exception = nullptr;
    module->runtime_->Invoke(
        module->force_clear_dither_.info, instance, nullptr, &exception);
    const uint64_t hit = module->dither_hits_.fetch_add(1, std::memory_order_relaxed) + 1;
    if (ShouldLog(hit) || exception != nullptr) {
        LogInfo(module->Id(),
            ("ForceClearDither hit=" + std::to_string(hit) +
                (exception != nullptr ? " [managed exception]" : "")).c_str());
    }
}

void EnhancementModule::HookUIStyleAwake(void* instance, const void* method_info) {
    const auto original = reinterpret_cast<VoidInstanceFn>(ui_style_awake_original_);
    if (original != nullptr) {
        original(instance, method_info);
    }
    EnhancementModule* module = active_.load(std::memory_order_acquire);
    if (module != nullptr) {
        module->PumpUidVisibility();
    }
}

void EnhancementModule::HookUIStyleUpdate(void* instance, const void* method_info) {
    EnhancementModule* module = active_.load(std::memory_order_acquire);
    if (module != nullptr) {
        module->PumpUidVisibility();
    }
    const auto original = reinterpret_cast<VoidInstanceFn>(ui_style_update_original_);
    if (original != nullptr) {
        original(instance, method_info);
    }
}

void EnhancementModule::HookEventSystemUpdate(void* instance, const void* method_info) {
    const auto original = reinterpret_cast<VoidInstanceFn>(event_system_update_original_);
    if (original != nullptr) {
        original(instance, method_info);
    }
    EnhancementModule* module = active_.load(std::memory_order_acquire);
    if (module != nullptr) {
        module->PumpUidVisibility();
    }
}

void EnhancementModule::HookGameObjectSetActive(
    void* instance, bool active, const void* method_info) {
    EnhancementModule* module = active_.load(std::memory_order_acquire);
    if (module != nullptr && active &&
        module->hide_uid_enabled_.load(std::memory_order_acquire) &&
        module->IsUidObject(instance)) {
        active = false;
        const uint64_t hit = module->uid_intercepts_.fetch_add(
            1, std::memory_order_relaxed) + 1;
        if (ShouldLog(hit)) {
            LogInfo(module->Id(),
                ("UID object activation intercepted hit=" + std::to_string(hit)).c_str());
        }
    }
    const auto original = reinterpret_cast<SetActiveFn>(game_object_set_active_original_);
    if (original != nullptr) {
        original(instance, active, method_info);
    }
}

bool EnhancementModule::IsUidObject(void* instance) const {
    if (instance == nullptr || runtime_ == nullptr || object_get_name_.info == nullptr) {
        return false;
    }
    void* exception = nullptr;
    void* managed_name = runtime_->Invoke(object_get_name_.info, instance, nullptr, &exception);
    if (exception != nullptr || managed_name == nullptr) {
        return false;
    }
    std::string name = runtime_->CopyString(managed_name);
    std::transform(name.begin(), name.end(), name.begin(),
        [](unsigned char c) { return static_cast<char>(std::tolower(c)); });
    constexpr std::string_view clone_suffix = "(clone)";
    if (name.ends_with(clone_suffix)) {
        name.resize(name.size() - clone_suffix.size());
    }
    while (!name.empty() && std::isspace(static_cast<unsigned char>(name.back()))) {
        name.pop_back();
    }
    return std::find(kUidObjectNames.begin(), kUidObjectNames.end(), name) !=
        kUidObjectNames.end();
}

void EnhancementModule::FindAndHideUidObjects() {
    const auto set_active = reinterpret_cast<SetActiveFn>(game_object_set_active_original_);
    if (runtime_ == nullptr || game_object_find_.info == nullptr || set_active == nullptr) {
        return;
    }
    std::lock_guard lock(uid_scan_mutex_);
    for (const char* name : kUidFindNames) {
        // Several WaterMarkCell instances may share one name. GameObject.Find
        // only returns active objects, so deactivating each match and asking
        // again terminates once none remain.
        for (int match = 0; match < 64; ++match) {
            void* managed_name = runtime_->NewString(name);
            if (managed_name == nullptr) {
                return;
            }
            void* parameters[1]{managed_name};
            void* exception = nullptr;
            void* object = runtime_->Invoke(
                game_object_find_.info, nullptr, parameters, &exception);
            if (exception != nullptr || object == nullptr) {
                break;
            }
            set_active(object, false, game_object_set_active_.info);
            const uint64_t hit = uid_scan_hides_.fetch_add(1, std::memory_order_relaxed) + 1;
            if (ShouldLog(hit)) {
                LogInfo(Id(),
                    (std::string("UID object hidden by scan name=") + name +
                        " hit=" + std::to_string(hit)).c_str());
            }
        }
    }
}

void EnhancementModule::PumpUidVisibility() {
    static thread_local bool in_progress = false;
    if (in_progress || !hide_uid_enabled_.load(std::memory_order_acquire)) {
        return;
    }
    const uint64_t now = MonotonicMilliseconds();
    if (now < next_uid_scan_ms_.load(std::memory_order_acquire)) {
        return;
    }
    in_progress = true;
    struct Guard {
        bool& flag;
        ~Guard() { flag = false; }
    } guard{in_progress};
    FindAndHideUidObjects();
    next_uid_scan_ms_.store(now + kUidScanIntervalMs, std::memory_order_release);
}

}  // namespace betterendfield
