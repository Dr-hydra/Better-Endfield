#include "core/log.h"
#include "core/runtime.h"
#include "core/command_pump.h"
#include "modules/module.h"
#include "modules/character_voice/character_voice_module.h"
#include "modules/desktop/desktop_module.h"
#include "modules/login_model/login_model_module.h"
#include "modules/custom_model/resource_probe.h"
#include "modules/custom_model/custom_model_module.h"

#include "android_virtual_keys.h"

#include <jni.h>

#include <atomic>
#include <chrono>
#include <cstdlib>
#include <cstring>
#include <memory>
#include <thread>
#include <vector>
#include <string>

// Each desktop feature module keeps its own entry point; the Android CMake build
// renames the shared BetterEndfield_GetModuleApiV1 symbol per translation unit so
// all of them can live in this one shared library.
extern "C" const BE_ModuleApiV1* BetterEndfield_GetUiModuleApiV1();
extern "C" const BE_ModuleApiV1* BetterEndfield_GetCameraModuleApiV1();
extern "C" const BE_ModuleApiV1* BetterEndfield_GetActionsModuleApiV1();

namespace betterendfield {
namespace {

constexpr auto kPollInterval = std::chrono::milliseconds(100);
constexpr auto kInitialDelay = std::chrono::seconds(1);
constexpr int kMaximumAttempts = 1200;
std::atomic_bool g_runtime_started{false};
std::vector<std::unique_ptr<Module>> g_modules;
std::unique_ptr<Il2CppRuntime> g_il2cpp_runtime;

const char* Configured(const char* variable) {
    const char* value = std::getenv(variable);
    return value != nullptr && value[0] != '\0' ? value : nullptr;
}

void RunModules() {
    // libil2cpp.so is mapped before the IL2CPP domain is safe to enter. The
    // proven read-only POC used this guard; connecting immediately can call
    // il2cpp_thread_attach while domain initialization is still in progress.
    std::this_thread::sleep_for(kInitialDelay);
    LogInfo("runtime", "Android module runtime started");

    g_il2cpp_runtime = std::make_unique<Il2CppRuntime>();
    Il2CppRuntime& runtime = *g_il2cpp_runtime;
    for (int attempt = 1; attempt <= kMaximumAttempts; ++attempt) {
        if (runtime.Connect()) {
            break;
        }
        if (attempt == kMaximumAttempts) {
            LogError("runtime", "timed out waiting for libil2cpp.so");
            return;
        }
        std::this_thread::sleep_for(kPollInterval);
    }

    Il2CppThreadScope thread(runtime);
    if (!thread.attached()) {
        LogError("runtime", "failed to attach worker to the IL2CPP domain");
        return;
    }

    const char* custom_probe = std::getenv("BETTER_ENDFIELD_CUSTOM_MODEL_PROBE");
    if (Configured("BETTER_ENDFIELD_CUSTOM_MODEL_CONFIG") != nullptr) {
        g_modules.emplace_back(std::make_unique<CustomModelModule>());
    }
    if (custom_probe != nullptr && std::string(custom_probe) == "1") {
        g_modules.emplace_back(std::make_unique<CustomModelResourceProbe>());
    }
    if (Configured("BETTER_ENDFIELD_VOICE_RULES") != nullptr) {
        g_modules.emplace_back(std::make_unique<CharacterVoiceModule>());
    }
    if (Configured("BETTER_ENDFIELD_MODEL_CONFIG") != nullptr) {
        g_modules.emplace_back(std::make_unique<LoginModelModule>());
    }
    // The three ported desktop modules. Their configurations are independent, so
    // a user who only wants one of them never has the others in the process.
    if (Configured("BETTER_ENDFIELD_UI_CONFIG") != nullptr) {
        g_modules.emplace_back(std::make_unique<DesktopModule>(
            "betterendfield.ui",
            "BETTER_ENDFIELD_UI_CONFIG",
            &BetterEndfield_GetUiModuleApiV1,
            "same-source desktop UI module active (hide UID/watermark, all-HUD toggle)"));
    }
    if (Configured("BETTER_ENDFIELD_CAMERA_CONFIG") != nullptr) {
        g_modules.emplace_back(std::make_unique<DesktopModule>(
            "betterendfield.camera",
            "BETTER_ENDFIELD_CAMERA_CONFIG",
            &BetterEndfield_GetCameraModuleApiV1,
            "same-source desktop camera module active (free camera, world pause, "
            "first person, near-camera dither)"));
    }
    if (Configured("BETTER_ENDFIELD_ACTIONS_CONFIG") != nullptr) {
        g_modules.emplace_back(std::make_unique<DesktopModule>(
            "betterendfield.actions",
            "BETTER_ENDFIELD_ACTIONS_CONFIG",
            &BetterEndfield_GetActionsModuleApiV1,
            "same-source desktop sustained-dash module active"));
    }

    for (const auto& module : g_modules) {
        const ModuleResult result = module->Start(runtime);
        LogInfo(module->Id(), result.message.c_str());
    }
}

bool AnyModuleRequested() {
    static constexpr const char* kVariables[]{
        "BETTER_ENDFIELD_VOICE_RULES",
        "BETTER_ENDFIELD_MODEL_CONFIG",
        "BETTER_ENDFIELD_UI_CONFIG",
        "BETTER_ENDFIELD_CAMERA_CONFIG",
        "BETTER_ENDFIELD_ACTIONS_CONFIG",
        "BETTER_ENDFIELD_CUSTOM_MODEL_CONFIG",
    };
    for (const char* variable : kVariables) {
        if (Configured(variable) != nullptr) return true;
    }
    const char* custom_probe = std::getenv("BETTER_ENDFIELD_CUSTOM_MODEL_PROBE");
    return custom_probe != nullptr && std::string(custom_probe) == "1";
}

}  // namespace
}  // namespace betterendfield

extern "C" JNIEXPORT jint JNICALL JNI_OnLoad(JavaVM*, void*) {
    if (!betterendfield::AnyModuleRequested()) {
        betterendfield::LogInfo(
            "runtime", "no Android modules selected; IL2CPP worker not started");
        return JNI_VERSION_1_6;
    }
    if (!betterendfield::g_runtime_started.exchange(true, std::memory_order_acq_rel)) {
        std::thread(betterendfield::RunModules).detach();
    }
    return JNI_VERSION_1_6;
}

extern "C" JNIEXPORT jboolean JNICALL
Java_dev_betterendfield_android_NativeCommandBridge_submit(
        JNIEnv* environment, jclass, jstring payload) {
    if (!environment || !payload) return JNI_FALSE;
    const char* text = environment->GetStringUTFChars(payload, nullptr);
    if (!text) return JNI_FALSE;
    const bool accepted = betterendfield::SubmitRuntimeCommand(text, std::strlen(text));
    environment->ReleaseStringUTFChars(payload, text);
    return accepted ? JNI_TRUE : JNI_FALSE;
}

extern "C" JNIEXPORT jstring JNICALL
Java_dev_betterendfield_android_NativeCommandBridge_status(
        JNIEnv* environment, jclass) {
    if (!environment) return nullptr;
    const std::string status = betterendfield::CopyRuntimeCommandStatus();
    return environment->NewStringUTF(status.c_str());
}

// The in-game panel's controls. This deliberately bypasses the runtime command
// pump: the pump is a single-slot, generation-checked queue drained on a Unity
// hook, which is right for configuration but would drop the release event of a
// press-and-hold control such as the free-camera movement pad. The latch is a
// plain atomic, so a press is visible to the desktop polling code immediately.
extern "C" JNIEXPORT jboolean JNICALL
Java_dev_betterendfield_android_NativeCommandBridge_key(
        JNIEnv*, jclass, jint virtual_key, jint action) {
    return betterendfield::SetVirtualKey(
        static_cast<int>(virtual_key),
        static_cast<betterendfield::VirtualKeyAction>(action)) ? JNI_TRUE : JNI_FALSE;
}

extern "C" JNIEXPORT void JNICALL
Java_dev_betterendfield_android_NativeCommandBridge_releaseKeys(JNIEnv*, jclass) {
    betterendfield::ReleaseAllVirtualKeys();
}
