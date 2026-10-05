#include "core/log.h"
#include "core/jni_binding.h"
#include "core/runtime_status.h"
#include "android_frame.h"
#include "android_camera.h"
#include "core/local_music_android.h"
#include <fcntl.h>
#include <sys/file.h>
#include <unistd.h>
#include <exception>
#include "core/runtime.h"
#include "core/command_pump.h"
#include "modules/module.h"
#include "modules/character_voice/character_voice_module.h"
#include "modules/desktop/desktop_module.h"
#include "modules/login_model/login_model_module.h"
#include "modules/custom_model/resource_probe.h"
#include "modules/custom_model/custom_model_module.h"
#include "../../../../../native/shared/third_party_modules/third_party_host.h"

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

namespace betterendfield { bool AndroidGlobalFov(bool enabled, float fov); }

// Each desktop feature module keeps its own entry point; the Android CMake build
// renames the shared BetterEndfield_GetModuleApiV1 symbol per translation unit so
// all of them can live in this one shared library.
extern "C" const BE_ModuleApiV1* BetterEndfield_GetUiModuleApiV1();
extern "C" const BE_ModuleApiV1* BetterEndfield_GetCameraModuleApiV1();
extern "C" const BE_ModuleApiV1* BetterEndfield_GetActionsModuleApiV1();

namespace betterendfield {
namespace {

constexpr auto kPollInterval = std::chrono::milliseconds(100);

constexpr int kMaximumAttempts = 1200;
struct Session {
    std::atomic_bool started{false};
    std::atomic_bool connected{false};
    RuntimeStatus status;
    Il2CppRuntime runtime;
    std::vector<std::unique_ptr<Module>> modules;
    BetterEndfield::ThirdParty::ThirdPartyHost third_party;
    HookBroker third_party_hooks;
    std::unique_ptr<DesktopModule> third_party_helper;
    int lock_fd = -1;
};
// One session lives for the process lifetime. Detached bootstrap and installed
// hooks must never race static destruction / dlclose at application shutdown.
Session& State() { static auto* state = new Session; return *state; }

const char* Configured(const char* variable) {
    const char* value = std::getenv(variable);
    return value != nullptr && value[0] != '\0' ? value : nullptr;
}

void RunModules() {
    auto& state = State();
    auto& runtime = state.runtime;
    if(const char* index=Configured("BETTER_ENDFIELD_THIRD_PARTY_INDEX")) {
        std::string error;const bool ready=state.third_party_hooks.Initialize(error);
        state.third_party.Start(index,"android-arm64",[](const auto& id,const auto& message){LogInfo(id.c_str(),message.c_str());},nullptr,
            ready?state.third_party_hooks.ChainApi():nullptr);
    }
    state.status.Set("runtime", "waiting_il2cpp");
    for (int attempt = 0; attempt < kMaximumAttempts; ++attempt) {
        if (runtime.Connect() && runtime.HasAssembly("mscorlib.dll") && betterendfield::HasAndroidFrameBridge()) break;
        if (attempt + 1 == kMaximumAttempts) {
            state.status.Set("runtime", "failed_il2cpp_timeout");
            LogError("runtime", "IL2CPP exports/domain/loaded images did not become ready");
            return;
        }
        std::this_thread::sleep_for(kPollInterval);
    }
    state.connected.store(true, std::memory_order_release);
    Il2CppThreadScope thread(runtime);
    if (!thread.attached()) {
        state.status.Set("runtime", "failed_thread_attach");
        return;
    }
    state.status.Set("runtime", "starting_modules");
    if(Configured("BETTER_ENDFIELD_THIRD_PARTY_INDEX")) {
        static const BE_ModuleApiV1 helper{{"third-party.runtime.helper","Third-party optional helpers","1",1},
            [](const BE_HostApiV1*)->BE_Result{return BE_Result_Ok;},[](const char*)->BE_Result{return BE_Result_Ok;},[](){}};
        state.third_party_helper=std::make_unique<DesktopModule>("third-party.runtime.helper","BETTER_ENDFIELD_THIRD_PARTY_INDEX",
            []()->const BE_ModuleApiV1*{return &helper;},"Third-party runtime helper ready");
        if(state.third_party_helper->Start(runtime).active)state.third_party.SetRuntime(state.third_party_helper->OptionalHostApi());
    }
    const char* custom_probe = std::getenv("BETTER_ENDFIELD_CUSTOM_MODEL_PROBE");
    if (Configured("BETTER_ENDFIELD_CUSTOM_MODEL_CONFIG") != nullptr) {
        state.modules.emplace_back(std::make_unique<CustomModelModule>());
    }
    if (custom_probe != nullptr && std::string(custom_probe) == "1") {
        state.modules.emplace_back(std::make_unique<CustomModelResourceProbe>());
    }
    if (Configured("BETTER_ENDFIELD_VOICE_RULES") != nullptr) {
        state.modules.emplace_back(std::make_unique<CharacterVoiceModule>());
    }
    if (Configured("BETTER_ENDFIELD_MODEL_CONFIG") != nullptr) {
        state.modules.emplace_back(std::make_unique<LoginModelModule>());
    }
    // The three ported desktop modules. Their configurations are independent, so
    // a user who only wants one of them never has the others in the process.
    if (Configured("BETTER_ENDFIELD_UI_CONFIG") != nullptr) {
        state.modules.emplace_back(std::make_unique<DesktopModule>(
            "betterendfield.ui",
            "BETTER_ENDFIELD_UI_CONFIG",
            &BetterEndfield_GetUiModuleApiV1,
            "same-source desktop UI module active (hide UID/watermark, all-HUD toggle)"));
    }
    if (Configured("BETTER_ENDFIELD_CAMERA_CONFIG") != nullptr) {
        state.modules.emplace_back(std::make_unique<DesktopModule>(
            "betterendfield.camera",
            "BETTER_ENDFIELD_CAMERA_CONFIG",
            &BetterEndfield_GetCameraModuleApiV1,
            "same-source desktop camera module active (free camera, world pause, "
            "first person, near-camera dither)"));
    }
    if (Configured("BETTER_ENDFIELD_ACTIONS_CONFIG") != nullptr) {
        state.modules.emplace_back(std::make_unique<DesktopModule>(
            "betterendfield.actions",
            "BETTER_ENDFIELD_ACTIONS_CONFIG",
            &BetterEndfield_GetActionsModuleApiV1,
            "same-source desktop sustained-dash module active"));
    }

    // Missing hot-update metadata delays only the affected module. The actual
    // Start calls remain serialized; abandoning a timed-out C++ thread is unsafe
    // and parallel Dobby patches are not a substitute for fixing JNI/ABI errors.
    std::vector<bool> finished(state.modules.size(), false);
    for (const auto& module : state.modules) state.status.Set(module->Id(), "waiting_metadata");
    auto dependencies_ready = [&runtime](const char* id) {
        const std::string name(id);
        if (name == "voice.character") return runtime.HasAssembly("Audio.Beyond.dll") &&
            runtime.HasAssembly("AK.Wwise.Unity.API.dll");
        if (name == "betterendfield.ui") return runtime.HasAssembly("Common.Beyond.dll") &&
            runtime.HasAssembly("UI.Beyond.dll") && runtime.HasAssembly("Gameplay.Beyond.dll") &&
            runtime.HasAssembly("UnityEngine.UI.dll");
        if (name == "betterendfield.camera") return runtime.HasAssembly("Gameplay.Beyond.dll") &&
            runtime.HasAssembly("Cinemachine.dll");
        if (name == "betterendfield.actions") return runtime.HasAssembly("Gameplay.Beyond.dll") &&
            runtime.HasAssembly("Audio.Beyond.dll");
        return runtime.HasAssembly("Gameplay.Beyond.dll");
    };
    size_t remaining = state.modules.size();
    for (int attempt = 0; remaining && attempt < kMaximumAttempts; ++attempt) {
        for (size_t i = 0; i < state.modules.size(); ++i) {
            if (finished[i] || !dependencies_ready(state.modules[i]->Id())) continue;
            Module& module = *state.modules[i];
            state.status.Set(module.Id(), "starting");
            LogInfo(module.Id(), "startup entered");
            try {
                const auto result = module.Start(runtime);
                state.status.Set(module.Id(), result.active ? "ready" : "failed");
                LogInfo(module.Id(), result.message.c_str());
            } catch (const std::exception& error) {
                state.status.Set(module.Id(), "failed_exception");
                LogError(module.Id(), error.what());
            } catch (...) {
                state.status.Set(module.Id(), "failed_exception");
                LogError(module.Id(), "unknown startup exception");
            }
            finished[i] = true;
            --remaining;
        }
        if (remaining) std::this_thread::sleep_for(kPollInterval);
    }
    for (size_t i = 0; i < finished.size(); ++i) {
        if (!finished[i]) state.status.Set(state.modules[i]->Id(), "failed_metadata_timeout");
    }
    state.status.Set("runtime", "startup_complete");
}

bool AnyModuleRequested() {
    static constexpr const char* kVariables[]{
        "BETTER_ENDFIELD_VOICE_RULES",
        "BETTER_ENDFIELD_MODEL_CONFIG",
        "BETTER_ENDFIELD_UI_CONFIG",
        "BETTER_ENDFIELD_CAMERA_CONFIG",
        "BETTER_ENDFIELD_ACTIONS_CONFIG",
        "BETTER_ENDFIELD_CUSTOM_MODEL_CONFIG",
        "BETTER_ENDFIELD_THIRD_PARTY_INDEX",
    };
    for (const char* variable : kVariables) {
        if (Configured(variable) != nullptr) return true;
    }
    const char* custom_probe = std::getenv("BETTER_ENDFIELD_CUSTOM_MODEL_PROBE");
    return custom_probe != nullptr && std::string(custom_probe) == "1";
}

}  // namespace
}  // namespace betterendfield

extern "C" JNIEXPORT jboolean JNICALL
Java_dev_betterendfield_android_NativeCommandBridge_updateThirdPartyRuntime(JNIEnv* env,jclass,jstring index) {
    if(!env || !index || env->GetStringUTFLength(index)>1024*1024)return JNI_FALSE;
    const char* json=env->GetStringUTFChars(index,nullptr);if(!json)return JNI_FALSE;
    const bool accepted=betterendfield::State().third_party.UpdateIndex(json);env->ReleaseStringUTFChars(index,json);return accepted?JNI_TRUE:JNI_FALSE;
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

extern "C" JNIEXPORT jint JNICALL
Java_dev_betterendfield_android_NativeCommandBridge_protocolVersion(JNIEnv*, jclass) { return 1; }
extern "C" JNIEXPORT jboolean JNICALL
Java_dev_betterendfield_android_NativeCommandBridge_updateCustomModelConfig(JNIEnv* env,jclass,jstring configuration) {
    if (!env || !configuration || env->GetStringUTFLength(configuration)>1024*1024) return JNI_FALSE;
    const char* text=env->GetStringUTFChars(configuration,nullptr); if (!text) return JNI_FALSE;
    const bool queued=betterendfield::CustomModelModule::QueueConfiguration(text);
    env->ReleaseStringUTFChars(configuration,text);
    return queued?JNI_TRUE:JNI_FALSE;
}
extern "C" JNIEXPORT void JNICALL
Java_dev_betterendfield_android_NativeCommandBridge_frame(JNIEnv*, jclass) {
    auto& state = betterendfield::State();
    try {
        if (state.connected.load(std::memory_order_acquire)) {
            betterendfield::Il2CppThreadScope thread(state.runtime);
            if (!thread.attached()) return;
            betterendfield::CustomModelModule::ApplyPendingConfiguration();
            betterendfield::DispatchAndroidFrame();
        } else {
            // Publish the render-thread identity before the worker starts.
            // No module callback can be installed before connected is published.
            betterendfield::DispatchAndroidFrame();
        }
    } catch (const std::exception& error) {
        betterendfield::LogError("runtime.frame", error.what());
    } catch (...) {
        betterendfield::LogError("runtime.frame", "frame callback failed");
    }
}
extern "C" JNIEXPORT void JNICALL
Java_dev_betterendfield_android_NativeCommandBridge_foreground(JNIEnv*, jclass, jboolean visible) {
    betterendfield::SetAndroidForeground(visible == JNI_TRUE);
}
extern "C" JNIEXPORT jstring JNICALL
Java_dev_betterendfield_android_NativeCommandBridge_runtimeStatus(JNIEnv* env, jclass) {
    const auto status = betterendfield::State().status.Copy() + betterendfield::AndroidCameraValuesStatus() +
        "camera.capabilities=" + std::to_string(betterendfield::AndroidCameraCapabilities()) + "\n" +
        "camera.active=" + std::to_string(betterendfield::AndroidCameraActive()) + "\n" +
        "ui.hud_hidden=" + (betterendfield::AndroidHudHidden() ? "1\n" : "0\n");
    return env->NewStringUTF(status.c_str());
}
extern "C" JNIEXPORT void JNICALL
Java_dev_betterendfield_android_NativeCommandBridge_look(JNIEnv*, jclass, jint dx, jint dy) {
    betterendfield::AddAndroidLook(dx, dy);
}
extern "C" JNIEXPORT void JNICALL
Java_dev_betterendfield_android_NativeCommandBridge_cameraValues(JNIEnv*, jclass, jfloat speed, jfloat fov) {
    betterendfield::AndroidCameraValues(speed, fov);
}
extern "C" JNIEXPORT jboolean JNICALL
Java_dev_betterendfield_android_NativeCommandBridge_globalFov(JNIEnv*, jclass, jboolean enabled, jfloat fov) {
    return betterendfield::AndroidGlobalFov(enabled == JNI_TRUE, fov) ? JNI_TRUE : JNI_FALSE;
}
extern "C" JNIEXPORT jstring JNICALL
Java_dev_betterendfield_android_NativeCommandBridge_mmdStatus(JNIEnv* env, jclass) {
    return env->NewStringUTF(betterendfield::AndroidMmdStatus().c_str());
}
extern "C" JNIEXPORT jboolean JNICALL
Java_dev_betterendfield_android_NativeCommandBridge_mmd(JNIEnv* env, jclass, jint type,
        jint argument, jdouble value, jstring text) {
    if (!text || env->GetStringUTFLength(text) >= 256) return JNI_FALSE;
    const char* utf8 = env->GetStringUTFChars(text, nullptr);
    if (!utf8) return JNI_FALSE;
    const std::string command_text(utf8);
    env->ReleaseStringUTFChars(text, utf8);
    try { return betterendfield::AndroidMmdCommand(type, argument, value, command_text) ? JNI_TRUE : JNI_FALSE; }
    catch (...) { return JNI_FALSE; }
}
extern "C" JNIEXPORT jint JNICALL JNI_OnLoad(JavaVM* vm, void*) {
    JNIEnv* env = nullptr;
    if (!vm || vm->GetEnv(reinterpret_cast<void**>(&env), JNI_VERSION_1_6) != JNI_OK) return JNI_ERR;
    auto& state = betterendfield::State();
    // flock is shared across library copies/namespaces, unlike a C++ static.
    // The private game cache path is set before nativeLoad. Keep the descriptor
    // open for the process lifetime; the kernel releases it on process death.
    const char* lock_path = std::getenv("BETTER_ENDFIELD_RUNTIME_LOCK");
    if (!lock_path || !*lock_path) return JNI_ERR;
    int fd = open(lock_path, O_CREAT | O_RDWR | O_CLOEXEC | O_NOFOLLOW, 0600);
    if (fd < 0) return JNI_ERR;
    if (flock(fd, LOCK_EX | LOCK_NB) != 0) { close(fd); return JNI_ERR; }
#define BE_NATIVE(name, signature) {const_cast<char*>(#name), const_cast<char*>(signature), \
    reinterpret_cast<void*>(&Java_dev_betterendfield_android_NativeCommandBridge_##name)}
    const JNINativeMethod methods[]{
        BE_NATIVE(submit, "(Ljava/lang/String;)Z"), BE_NATIVE(status, "()Ljava/lang/String;"),
        BE_NATIVE(key, "(II)Z"), BE_NATIVE(releaseKeys, "()V"), BE_NATIVE(protocolVersion, "()I"),
        BE_NATIVE(frame, "()V"), BE_NATIVE(foreground, "(Z)V"), BE_NATIVE(look, "(II)V"), BE_NATIVE(runtimeStatus, "()Ljava/lang/String;"),
        BE_NATIVE(cameraValues, "(FF)V"), BE_NATIVE(mmdStatus, "()Ljava/lang/String;"),
        BE_NATIVE(globalFov, "(ZF)Z"),
        BE_NATIVE(updateCustomModelConfig, "(Ljava/lang/String;)Z"),
        BE_NATIVE(updateThirdPartyRuntime, "(Ljava/lang/String;)Z"),
        BE_NATIVE(mmd, "(IIDLjava/lang/String;)Z")
    };
#undef BE_NATIVE
    jclass bridge_class = nullptr;
    if (!betterendfield::BindContextLoaderNatives(env, "dev.betterendfield.android.NativeCommandBridge",
            methods, static_cast<jint>(sizeof(methods) / sizeof(methods[0])), &bridge_class)) {
        close(fd);
        return JNI_ERR;
    }
    if (!betterendfield::InitializeAndroidMusic(vm, env, bridge_class))
        betterendfield::LogError("mmd.music", "Java media API unavailable; body and camera remain usable");
    state.lock_fd = fd;
    state.status.Set("runtime", "loaded");
    if (betterendfield::AnyModuleRequested() && !state.started.exchange(true)) {
        // Once registered, do not unload the library on worker creation failure:
        // published JNI pointers must remain valid so status can report it.
        try {
            std::thread([] {
                try { betterendfield::RunModules(); }
                catch (const std::exception& e) {
                    betterendfield::State().status.Set("runtime", "failed_exception");
                    betterendfield::LogError("runtime", e.what());
                }
                catch (...) { betterendfield::State().status.Set("runtime", "failed_exception"); }
            }).detach();
        } catch (...) { state.status.Set("runtime", "failed_worker_creation"); }
    }
    return JNI_VERSION_1_6;
}
