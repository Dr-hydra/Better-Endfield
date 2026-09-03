#include "core/log.h"
#include "core/runtime.h"
#include "modules/module.h"
#include "modules/character_voice/character_voice_module.h"
#include "modules/enhancement/enhancement_module.h"
#include "modules/login_model/login_model_module.h"

#include <jni.h>

#include <atomic>
#include <chrono>
#include <cstdlib>
#include <memory>
#include <thread>
#include <vector>

namespace betterendfield {
namespace {

constexpr auto kPollInterval = std::chrono::milliseconds(100);
constexpr auto kInitialDelay = std::chrono::seconds(1);
constexpr int kMaximumAttempts = 1200;
std::atomic_bool g_runtime_started{false};
std::vector<std::unique_ptr<Module>> g_modules;
std::unique_ptr<Il2CppRuntime> g_il2cpp_runtime;

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

    const char* voice_rules = std::getenv("BETTER_ENDFIELD_VOICE_RULES");
    if (voice_rules != nullptr && *voice_rules != '\0') {
        g_modules.emplace_back(std::make_unique<CharacterVoiceModule>());
    }
    const char* model_configuration = std::getenv("BETTER_ENDFIELD_MODEL_CONFIG");
    if (model_configuration != nullptr && *model_configuration != '\0') {
        g_modules.emplace_back(std::make_unique<LoginModelModule>());
    }
    const char* enhancement_configuration =
        std::getenv("BETTER_ENDFIELD_ENHANCEMENT_CONFIG");
    if (enhancement_configuration != nullptr && *enhancement_configuration != '\0') {
        g_modules.emplace_back(std::make_unique<EnhancementModule>());
    }

    for (const auto& module : g_modules) {
        const ModuleResult result = module->Start(runtime);
        LogInfo(module->Id(), result.message.c_str());
    }
}

}  // namespace
}  // namespace betterendfield

extern "C" JNIEXPORT jint JNICALL JNI_OnLoad(JavaVM*, void*) {
    const char* configured_rules = std::getenv(
        "BETTER_ENDFIELD_VOICE_RULES");
    const bool character_voice_requested = configured_rules != nullptr &&
        configured_rules[0] != '\0';
    const char* configured_model = std::getenv("BETTER_ENDFIELD_MODEL_CONFIG");
    const bool model_requested = configured_model != nullptr &&
        configured_model[0] != '\0';
    const char* configured_enhancement =
        std::getenv("BETTER_ENDFIELD_ENHANCEMENT_CONFIG");
    const bool enhancement_requested = configured_enhancement != nullptr &&
        configured_enhancement[0] != '\0';
    const bool any_requested = character_voice_requested || model_requested ||
        enhancement_requested;
    if (any_requested &&
        !betterendfield::g_runtime_started.exchange(true, std::memory_order_acq_rel)) {
        std::thread(betterendfield::RunModules).detach();
    } else if (!any_requested) {
        betterendfield::LogInfo(
            "runtime",
            "no Android modules selected; IL2CPP worker not started");
    }
    return JNI_VERSION_1_6;
}
