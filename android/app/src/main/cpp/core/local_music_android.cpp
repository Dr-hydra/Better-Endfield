#include "local_music_android.h"
#include "android_camera.h"
#include <algorithm>
#include <cstring>
#include <cmath>
namespace betterendfieldnext {
namespace {
JavaVM* java_vm = nullptr;
jclass bridge = nullptr;
jmethodID open_method = nullptr, control_method = nullptr, status_method = nullptr, error_method = nullptr;
struct Environment {
    JNIEnv* env = nullptr;
    bool attached = false;
    Environment() {
        if (!java_vm) return;
        if (java_vm->GetEnv(reinterpret_cast<void**>(&env), JNI_VERSION_1_6) == JNI_EDETACHED) {
            attached = java_vm->AttachCurrentThread(&env, nullptr) == JNI_OK;
            if (!attached) env = nullptr;
        }
    }
    ~Environment() { if (attached) java_vm->DetachCurrentThread(); }
    bool Ok() { if (!env) return false; if (!env->ExceptionCheck()) return true; env->ExceptionClear(); return false; }
};
uint64_t BE_CALL Open(const char* path, const char*) {
    if (!path || !*path) return 0;
    Environment call; if (!call.env) return 0;
    jstring name = call.env->NewStringUTF(path);
    if (!name || !call.Ok()) return 0;
    const jlong token = call.env->CallStaticLongMethod(bridge, open_method, name);
    call.env->DeleteLocalRef(name);
    return call.Ok() && token > 0 ? uint64_t(token) : 0;
}
int Control(uint64_t token, int operation, double value) {
    if (!token || !std::isfinite(value)) return 0;
    Environment call; if (!call.env) return 0;
    int result = call.env->CallStaticIntMethod(bridge, control_method, jlong(token), jint(operation), jdouble(value));
    return call.Ok() ? result : 0;
}
int BE_CALL Play(uint64_t token, double value) { return Control(token, 0, value); }
int BE_CALL Pause(uint64_t token) { return Control(token, 1, 0); }
int BE_CALL Seek(uint64_t token, double value) { return Control(token, 2, value); }
void BE_CALL Close(uint64_t token) { Control(token, 3, 0); }
int BE_CALL Gain(uint64_t token, float value) { return Control(token, 4, value); }
int BE_CALL Status(uint64_t token, BE_LocalMusicStatusV1* out) {
    if (!token || !out || out->size < sizeof(*out)) return 0;
    Environment call; if (!call.env) return 0;
    auto array = static_cast<jdoubleArray>(call.env->CallStaticObjectMethod(bridge, status_method, jlong(token)));
    if (!call.Ok() || !array) return 0;
    double data[4]{};
    bool ok = call.env->GetArrayLength(array) == 4;
    if (ok) call.env->GetDoubleArrayRegion(array, 0, 4, data);
    call.env->DeleteLocalRef(array);
    if (!call.Ok() || !ok) return 0;
    *out = {}; out->size = sizeof(*out); out->state = int32_t(data[0]);
    out->position_seconds = data[1]; out->duration_seconds = data[2]; out->output_active = data[3] != 0;
    if (out->state == BE_LocalMusic_Error) {
        auto error = static_cast<jstring>(call.env->CallStaticObjectMethod(bridge, error_method, jlong(token)));
        if (call.Ok() && error) {
            const char* text = call.env->GetStringUTFChars(error, nullptr);
            if (text) { std::strncpy(out->error, text, sizeof(out->error) - 1); call.env->ReleaseStringUTFChars(error, text); }
            call.env->DeleteLocalRef(error); call.Ok();
        }
    }
    return 1;
}
const BE_LocalMusicApiV1 api{1, sizeof(BE_LocalMusicApiV1), Open, Play, Pause, Seek, Close, Status, Gain};
}
bool InitializeAndroidMusic(JavaVM* vm, JNIEnv* env, jclass target) {
    open_method = env->GetStaticMethodID(target, "audioOpen", "(Ljava/lang/String;)J");
    control_method = env->GetStaticMethodID(target, "audioControl", "(JID)I");
    status_method = env->GetStaticMethodID(target, "audioStatus", "(J)[D");
    error_method = env->GetStaticMethodID(target, "audioError", "(J)Ljava/lang/String;");
    if (env->ExceptionCheck()) { env->ExceptionClear(); return false; }
    if (!open_method || !control_method || !status_method || !error_method) return false;
    bridge = target; java_vm = vm; return true;
}
const BE_LocalMusicApiV1* AndroidLocalMusicApi() { return java_vm ? &api : nullptr; }
}
