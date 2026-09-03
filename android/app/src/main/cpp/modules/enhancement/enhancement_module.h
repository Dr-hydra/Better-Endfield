#pragma once

#include "core/hook_broker.h"
#include "core/runtime.h"
#include "modules/module.h"

#include <atomic>
#include <cstdint>
#include <mutex>
#include <string>
#include <vector>

namespace betterendfield {

struct MethodInfo;

// Ports the desktop UI/camera enhancements that need no keyboard: hiding the
// UID/watermark panels (BetterEndfield.UI) and clearing the near-camera mesh
// dither (BetterEndfield.Camera). The hook points are the desktop ones.
class EnhancementModule final : public Module {
public:
    const char* Id() const override;
    ModuleResult Start(Il2CppRuntime& runtime) override;
    ~EnhancementModule() override;

private:
    struct Configuration {
        bool hide_uid = false;
        bool disable_dither = false;
    };

    using VoidInstanceFn = void (*)(void* instance, const void* method_info);
    using SetActiveFn = void (*)(void* instance, bool active, const void* method_info);

    static Configuration ParseConfiguration(const char* text);

    static void HookProcessDither(void* instance, const void* method_info);
    static void HookUIStyleAwake(void* instance, const void* method_info);
    static void HookUIStyleUpdate(void* instance, const void* method_info);
    static void HookEventSystemUpdate(void* instance, const void* method_info);
    static void HookGameObjectSetActive(
        void* instance, bool active, const void* method_info);

    bool IsUidObject(void* instance) const;
    void FindAndHideUidObjects();
    void PumpUidVisibility();

    static std::atomic<EnhancementModule*> active_;
    // Originals stay static so a detour can always forward to the game even if
    // the module instance has gone away; skipping EventSystem.Update or
    // GameObject.SetActive would break the game.
    static void* process_dither_original_;
    static void* ui_style_awake_original_;
    static void* ui_style_update_original_;
    static void* event_system_update_original_;
    static void* game_object_set_active_original_;

    Il2CppRuntime* runtime_ = nullptr;
    HookBroker hook_broker_;
    Configuration configuration_{};
    std::atomic_bool hide_uid_enabled_{false};
    std::atomic_bool disable_dither_enabled_{false};

    ResolvedMethod force_clear_dither_{};
    ResolvedMethod object_get_name_{};
    ResolvedMethod game_object_find_{};
    ResolvedMethod game_object_set_active_{};

    void* process_dither_stub_ = nullptr;
    void* ui_style_awake_stub_ = nullptr;
    void* ui_style_update_stub_ = nullptr;
    void* event_system_update_stub_ = nullptr;
    void* game_object_set_active_stub_ = nullptr;

    std::atomic<uint64_t> next_uid_scan_ms_{0};
    std::atomic<uint64_t> dither_hits_{0};
    std::atomic<uint64_t> uid_intercepts_{0};
    std::atomic<uint64_t> uid_scan_hides_{0};
    std::mutex uid_scan_mutex_;
};

}  // namespace betterendfield
