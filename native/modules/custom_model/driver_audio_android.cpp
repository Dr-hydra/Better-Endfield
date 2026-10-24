#if defined(__ANDROID__)
// Android driver-sound backend. Every voice is an independent MediaPlayer
// stream, so the OS mixes it with the game's own audio and BGM: nothing in the
// Unity/Wwise graph is touched and no existing sound is replaced.
#include "driver_audio.h"

#include <jni.h>

#include <algorithm>
#include <atomic>
#include <mutex>
#include <string>
#include <unordered_map>
#include <vector>

namespace BetterEndfieldNext::CustomModel {
namespace {

constexpr size_t kMaxVoices = 8;

std::function<void(const std::string&)> g_log_sink;
void Log(const std::string& message) { if (g_log_sink) g_log_sink(message); }

JavaVM* g_vm = nullptr;
jclass g_bridge = nullptr;          // global ref to NativeCommandBridge
jmethodID g_start = nullptr;        // (Ljava/lang/String;Ljava/lang/String;FZ)I
jmethodID g_stop = nullptr;         // (Ljava/lang/String;)V
jmethodID g_stop_all = nullptr;     // ()V
jmethodID g_status = nullptr;       // ()Ljava/lang/String;

std::mutex g_mutex;
std::unordered_map<std::string, std::string> g_active;  // key -> path

struct Env {
    JNIEnv* env = nullptr;
    bool attached = false;
    Env() {
        if (!g_vm) return;
        if (g_vm->GetEnv(reinterpret_cast<void**>(&env), JNI_VERSION_1_6) == JNI_EDETACHED) {
            attached = g_vm->AttachCurrentThread(&env, nullptr) == JNI_OK;
            if (!attached) env = nullptr;
        }
    }
    ~Env() { if (attached) g_vm->DetachCurrentThread(); }
    bool Ok() {
        if (!env) return false;
        if (!env->ExceptionCheck()) return true;
        env->ExceptionClear();
        return false;
    }
};

}  // namespace

void SetDriverAudioLogger(std::function<void(const std::string&)> sink) {
    g_log_sink = std::move(sink);
}

bool InitializeAndroidDriverAudio(void* vm, void* env_pointer, void* bridge_class) {
    if (!vm || !env_pointer || !bridge_class) return false;
    g_vm = static_cast<JavaVM*>(vm);
    auto* env = static_cast<JNIEnv*>(env_pointer);
    auto* local = static_cast<jclass>(bridge_class);
    if (env->ExceptionCheck()) env->ExceptionClear();
    g_bridge = static_cast<jclass>(env->NewGlobalRef(local));
    if (!g_bridge) return false;
    g_start = env->GetStaticMethodID(g_bridge, "driverAudioStart", "(Ljava/lang/String;Ljava/lang/String;FZ)I");
    g_stop = env->GetStaticMethodID(g_bridge, "driverAudioStop", "(Ljava/lang/String;)V");
    g_stop_all = env->GetStaticMethodID(g_bridge, "driverAudioStopAll", "()V");
    g_status = env->GetStaticMethodID(g_bridge, "driverAudioStatus", "()Ljava/lang/String;");
    if (env->ExceptionCheck()) { env->ExceptionClear(); return false; }
    if (!g_start || !g_stop || !g_stop_all || !g_status) return false;
    Log("[driver-audio] Android MediaPlayer voice backend ready");
    return true;
}

bool DriverAudioAvailable() { return g_vm != nullptr && g_bridge != nullptr && g_start != nullptr; }

bool StartDriverSound(const std::string& key, const DriverSoundSpec& spec) {
    if (key.empty() || spec.path.empty() || !DriverAudioAvailable()) return false;
    {
        std::scoped_lock lock(g_mutex);
        const auto found = g_active.find(key);
        if (found != g_active.end() && found->second == spec.path) return true; // already playing
        if (found == g_active.end() && g_active.size() >= kMaxVoices) {
            Log("[driver-audio] voice quota reached; '" + key + "' dropped");
            return false;
        }
    }
    Env call;
    if (!call.env) return false;
    jstring jkey = call.env->NewStringUTF(key.c_str());
    jstring jpath = call.env->NewStringUTF(spec.path.c_str());
    if (!jkey || !jpath || !call.Ok()) {
        if (jkey) call.env->DeleteLocalRef(jkey);
        if (jpath) call.env->DeleteLocalRef(jpath);
        return false;
    }
    const jint started = call.env->CallStaticIntMethod(g_bridge, g_start, jkey, jpath,
        static_cast<jfloat>(std::clamp(spec.gain, 0.0f, 2.0f)),
        spec.loop ? JNI_TRUE : JNI_FALSE);
    call.env->DeleteLocalRef(jkey);
    call.env->DeleteLocalRef(jpath);
    if (!call.Ok() || started == 0) return false;
    std::scoped_lock lock(g_mutex);
    g_active[key] = spec.path;
    return true;
}

void StopDriverSound(const std::string& key) {
    {
        std::scoped_lock lock(g_mutex);
        if (g_active.erase(key) == 0) return;
    }
    if (!DriverAudioAvailable()) return;
    Env call;
    if (!call.env) return;
    jstring jkey = call.env->NewStringUTF(key.c_str());
    if (!jkey) return;
    call.env->CallStaticVoidMethod(g_bridge, g_stop, jkey);
    call.env->DeleteLocalRef(jkey);
    call.Ok();
}

void StopAllDriverSounds() {
    { std::scoped_lock lock(g_mutex); g_active.clear(); }
    if (!DriverAudioAvailable()) return;
    Env call;
    if (!call.env) return;
    call.env->CallStaticVoidMethod(g_bridge, g_stop_all);
    call.Ok();
}

std::string DriverAudioStatus() {
    if (!DriverAudioAvailable()) return {};
    Env call;
    if (!call.env) return {};
    auto text = static_cast<jstring>(call.env->CallStaticObjectMethod(g_bridge, g_status));
    if (!call.Ok() || !text) return {};
    const char* raw = call.env->GetStringUTFChars(text, nullptr);
    std::string value = raw ? raw : "";
    if (raw) call.env->ReleaseStringUTFChars(text, raw);
    call.env->DeleteLocalRef(text);
    return value;
}

}  // namespace BetterEndfieldNext::CustomModel
#endif
