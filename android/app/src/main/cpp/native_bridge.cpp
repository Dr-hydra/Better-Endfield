#include <android/log.h>
#include <dlfcn.h>
#include <jni.h>

#include <atomic>
#include <chrono>
#include <cstdint>
#include <thread>

namespace {

constexpr char kLogTag[] = "BetterEndfield";
constexpr char kGameplayAssembly[] = "Gameplay.Beyond.dll";
constexpr char kVoiceNamespace[] = "Beyond.Gameplay.Audio";
constexpr char kVoiceClass[] = "VoiceI18n";
constexpr char kVoiceMethod[] = "GetCurrentLanguage";
constexpr auto kInitialDelay = std::chrono::seconds(1);
constexpr auto kPollInterval = std::chrono::milliseconds(250);
constexpr int kMaximumAttempts = 480;

struct Il2CppDomain;
struct Il2CppAssembly;
struct Il2CppImage;
struct Il2CppClass;
struct MethodInfo;

using DomainGetFn = Il2CppDomain* (*)();
using ThreadAttachFn = void* (*)(Il2CppDomain* domain);
using ThreadDetachFn = void (*)(void* thread);
using DomainAssemblyOpenFn = const Il2CppAssembly* (*)(
    Il2CppDomain* domain, const char* name);
using AssemblyGetImageFn = const Il2CppImage* (*)(const Il2CppAssembly* assembly);
using ClassFromNameFn = Il2CppClass* (*)(
    const Il2CppImage* image, const char* namespaze, const char* name);
using ClassGetMethodFromNameFn = const MethodInfo* (*)(
    Il2CppClass* klass, const char* name, int argument_count);

std::atomic_bool g_probe_started{false};

void LogInfo(const char* message) {
    __android_log_write(ANDROID_LOG_INFO, kLogTag, message);
}

void LogError(const char* message) {
    __android_log_write(ANDROID_LOG_ERROR, kLogTag, message);
}

template <typename Function>
Function Resolve(void* library, const char* name) {
    return reinterpret_cast<Function>(dlsym(library, name));
}

void RunProbe() {
    std::this_thread::sleep_for(kInitialDelay);
    LogInfo("read-only IL2CPP probe started");

    for (int attempt = 1; attempt <= kMaximumAttempts; ++attempt) {
        void* il2cpp = dlopen("libil2cpp.so", RTLD_NOW | RTLD_NOLOAD);
        if (il2cpp == nullptr) {
            std::this_thread::sleep_for(kPollInterval);
            continue;
        }

        const auto domain_get = Resolve<DomainGetFn>(il2cpp, "il2cpp_domain_get");
        const auto thread_attach = Resolve<ThreadAttachFn>(il2cpp, "il2cpp_thread_attach");
        const auto thread_detach = Resolve<ThreadDetachFn>(il2cpp, "il2cpp_thread_detach");
        const auto domain_assembly_open = Resolve<DomainAssemblyOpenFn>(
            il2cpp, "il2cpp_domain_assembly_open");
        const auto assembly_get_image = Resolve<AssemblyGetImageFn>(
            il2cpp, "il2cpp_assembly_get_image");
        const auto class_from_name = Resolve<ClassFromNameFn>(
            il2cpp, "il2cpp_class_from_name");
        const auto class_get_method_from_name = Resolve<ClassGetMethodFromNameFn>(
            il2cpp, "il2cpp_class_get_method_from_name");

        if (domain_get == nullptr || thread_attach == nullptr || thread_detach == nullptr ||
            domain_assembly_open == nullptr || assembly_get_image == nullptr ||
            class_from_name == nullptr || class_get_method_from_name == nullptr) {
            dlclose(il2cpp);
            LogError("one or more required IL2CPP exports are missing");
            return;
        }

        Il2CppDomain* domain = domain_get();
        if (domain == nullptr) {
            dlclose(il2cpp);
            std::this_thread::sleep_for(kPollInterval);
            continue;
        }

        void* attached_thread = thread_attach(domain);
        if (attached_thread == nullptr) {
            dlclose(il2cpp);
            LogError("failed to attach probe thread to IL2CPP");
            return;
        }

        const Il2CppAssembly* assembly = domain_assembly_open(domain, kGameplayAssembly);
        const Il2CppImage* image = assembly == nullptr
            ? nullptr
            : assembly_get_image(assembly);
        Il2CppClass* klass = image == nullptr
            ? nullptr
            : class_from_name(image, kVoiceNamespace, kVoiceClass);
        const MethodInfo* method = klass == nullptr
            ? nullptr
            : class_get_method_from_name(klass, kVoiceMethod, 0);

        if (method != nullptr) {
            thread_detach(attached_thread);
            dlclose(il2cpp);
            __android_log_print(
                ANDROID_LOG_INFO,
                kLogTag,
                "resolved %s.%s::%s() method_info=%p attempt=%d",
                kVoiceNamespace,
                kVoiceClass,
                kVoiceMethod,
                static_cast<const void*>(method),
                attempt);
            return;
        }

        thread_detach(attached_thread);
        dlclose(il2cpp);
        if (attempt == 1 || attempt % 40 == 0) {
            __android_log_print(
                ANDROID_LOG_INFO,
                kLogTag,
                "IL2CPP ready; voice contract not available yet attempt=%d",
                attempt);
        }
        std::this_thread::sleep_for(kPollInterval);
    }

    LogError("timed out resolving the voice IL2CPP contract");
}

}  // namespace

extern "C" JNIEXPORT jint JNICALL JNI_OnLoad(JavaVM*, void*) {
    if (!g_probe_started.exchange(true, std::memory_order_acq_rel)) {
        std::thread(RunProbe).detach();
    }
    return JNI_VERSION_1_6;
}
