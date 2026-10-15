#include "touch_pointer_policy.h"

// Pointer-reuse diagnosis/fix reference: orangetooth/endfield-touch-patch v3,
// commit 55a4553a126da4efc29623d03f47e881e4f1fd0e. This module adapts that
// approach through Unity's existing pointerBehavior setting rather than file
// offsets. See docs/ui/TOUCH_POINTER_REUSE_FIX.md for attribution and validation.

#include <Windows.h>
#include <algorithm>
#include <atomic>
#include <cstdint>
#include <cstdio>
#include <cstring>
#include <mutex>
#include <vector>

namespace BetterEndfieldNext::UiModule::TouchPointerPolicy {
namespace {

constexpr char kModuleId[] = "betterendfieldnext.ui";
constexpr char kAssembly[] = "Unity.InputSystem.dll";
constexpr char kNamespace[] = "UnityEngine.InputSystem.UI";
constexpr char kClass[] = "InputSystemUIInputModule";
constexpr char kEnum[] = "UnityEngine.InputSystem.UI.UIPointerBehavior";

const BE_HostApiV1* g_host = nullptr;
LogFn g_log = nullptr;
std::atomic_bool g_enabled{false};
std::atomic_bool g_ready{false};
int32_t g_offset = -1;
int32_t g_independent_mode = -1;
using ProcessFn = void(__fastcall*)(void*, void*);
using RouteFn = int32_t(__fastcall*)(void*, void*, bool, void*);
ProcessFn g_original_process = nullptr;
RouteFn g_original_route = nullptr;

struct Override {
    void* instance;
    uint32_t handle;
    int32_t original_mode;
};
std::mutex g_mutex;
std::vector<Override> g_overrides;

void Log(const char* message) {
    if (g_log) g_log(message);
}

bool ReadMode(void* instance, int32_t& value) {
    if (!instance || g_offset < 0) return false;
    // The field offset is resolved by its exact class/type contract, never by
    // a client version/RVA. This scalar is the backing field of Unity's public
    // pointerBehavior property; no Unity object API is called on config threads.
    __try {
        std::memcpy(&value, static_cast<uint8_t*>(instance) + g_offset, sizeof(value));
        return true;
    } __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

bool WriteMode(void* instance, int32_t value) {
    if (!instance || g_offset < 0) return false;
    __try {
        std::memcpy(static_cast<uint8_t*>(instance) + g_offset, &value, sizeof(value));
        return true;
    } __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

void Restore(const Override& item) {
    int32_t current = -1;
    // A later game setting belongs to the game. Restore only our own value.
    if (ReadMode(item.instance, current) && current == g_independent_mode) {
        WriteMode(item.instance, item.original_mode);
    }
    g_host->gchandle_free(g_host->context, item.handle);
}

void Apply(void* instance) {
    std::lock_guard lock(g_mutex);
    if (!g_ready.load(std::memory_order_acquire) || !instance) return;
    auto found = std::find_if(g_overrides.begin(), g_overrides.end(),
        [instance](const Override& item) { return item.instance == instance; });
    if (!g_enabled.load(std::memory_order_acquire)) {
        if (found != g_overrides.end()) {
            Restore(*found);
            g_overrides.erase(found);
            Log("Touch pointer policy: restored original UI pointer behavior.");
        }
        return;
    }
    int32_t current = -1;
    if (!ReadMode(instance, current) || current == g_independent_mode) return;
    if (found == g_overrides.end()) {
        // Pin the managed object before retaining its address across frames.
        const uint32_t handle = g_host->gchandle_new(g_host->context, instance, 1);
        if (!handle) {
            Log("Touch pointer policy: could not retain UI module; left its setting unchanged.");
            return;
        }
        g_overrides.push_back({instance, handle, current});
        found = g_overrides.end() - 1;
    } else {
        found->original_mode = current;
    }
    if (!WriteMode(instance, g_independent_mode)) {
        g_host->gchandle_free(g_host->context, found->handle);
        g_overrides.erase(found);
        Log("Touch pointer policy: could not write UI pointer behavior.");
        return;
    }
    char message[180];
    std::snprintf(message, sizeof(message),
        "Touch pointer policy: AllPointersAsIs applied before UI input (was %d, now %d).",
        current, g_independent_mode);
    Log(message);
}

int32_t __fastcall DetourRoute(void* instance, void* control, bool create, void* method) {
    // Input action callbacks run before EventSystem.Update. Apply here as well
    // as Process, so the first mouse/touch event cannot reuse a finger's state.
    Apply(instance);
    return g_original_route ? g_original_route(instance, control, create, method) : -1;
}

void __fastcall DetourProcess(void* instance, void* method) {
    Apply(instance);
    if (g_original_process) g_original_process(instance, method);
}

}  // namespace

bool Start(const BE_HostApiV1* host, LogFn log) {
    std::lock_guard lock(g_mutex);
    if (g_ready.load()) return true;
    g_host = host;
    g_log = log;
    if (!host || !host->resolve_field || !host->resolve_method || !host->create_hook ||
        !host->field_get_value_object || !host->object_unbox ||
        !host->gchandle_new || !host->gchandle_free) {
        Log("Touch pointer policy: host contracts unavailable; no override installed.");
        return false;
    }
    const BE_FieldDescriptorV1 behavior{kAssembly, kNamespace, kClass, "m_PointerBehavior", kEnum};
    const BE_FieldDescriptorV1 independent{kAssembly, kNamespace, "UIPointerBehavior", "AllPointersAsIs", kEnum};
    BE_ResolvedFieldV1 field{}, enum_field{};
    const BE_MethodDescriptorV1 process{kAssembly, kNamespace, kClass, "Process", nullptr, "System.Void", 0};
    const BE_MethodDescriptorV1 route{kAssembly, kNamespace, kClass, "GetPointerStateIndexFor",
        "UnityEngine.InputSystem.InputControl|System.Boolean", "System.Int32", 2};
    BE_ResolvedMethodV1 resolved_process{}, resolved_route{};
    if (host->resolve_field(host->context, &behavior, &field) != BE_Result_Ok ||
        !field.field_info || field.offset < 16 || field.offset > 4096 || field.offset % 4 != 0 ||
        host->resolve_field(host->context, &independent, &enum_field) != BE_Result_Ok ||
        !enum_field.field_info ||
        host->resolve_method(host->context, &process, &resolved_process) != BE_Result_Ok ||
        !resolved_process.method_pointer ||
        host->resolve_method(host->context, &route, &resolved_route) != BE_Result_Ok ||
        !resolved_route.method_pointer) {
        Log("Touch pointer policy: metadata contract mismatch; no override installed.");
        return false;
    }
    void* boxed = host->field_get_value_object(host->context, enum_field.field_info, nullptr);
    void* raw = boxed ? host->object_unbox(host->context, boxed) : nullptr;
    if (!raw) {
        Log("Touch pointer policy: enum value unavailable; no override installed.");
        return false;
    }
    std::memcpy(&g_independent_mode, raw, sizeof(g_independent_mode));
    g_offset = field.offset;
    if (host->create_hook(host->context, kModuleId, resolved_process.method_pointer,
            reinterpret_cast<void*>(&DetourProcess), reinterpret_cast<void**>(&g_original_process)) != BE_Result_Ok ||
        host->create_hook(host->context, kModuleId, resolved_route.method_pointer,
            reinterpret_cast<void*>(&DetourRoute), reinterpret_cast<void**>(&g_original_route)) != BE_Result_Ok) {
        Log("Touch pointer policy: hook installation failed; override remains disabled.");
        return false;
    }
    g_ready.store(true, std::memory_order_release);
    Log("Touch pointer policy: independent UI pointer routing ready.");
    return true;
}

void SetEnabled(bool enabled) {
    g_enabled.store(enabled, std::memory_order_release);
}

void Stop() {
    std::lock_guard lock(g_mutex);
    g_enabled.store(false, std::memory_order_release);
    g_ready.store(false, std::memory_order_release);
    for (const auto& item : g_overrides) Restore(item);
    g_overrides.clear();
    // The caller owns/retires all hooks of betterendfieldnext.ui together.
}

}  // namespace BetterEndfieldNext::UiModule::TouchPointerPolicy
