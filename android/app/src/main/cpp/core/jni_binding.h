#pragma once
#include <jni.h>
namespace betterendfieldnext {
inline bool BindContextLoaderNatives(JNIEnv* env, const char* class_name,
        const JNINativeMethod* methods, jint count, jclass* bound_class = nullptr) {
    if (!env || env->PushLocalFrame(8) != JNI_OK) return false;
    bool bound = false;
    do {
        jclass thread_class = env->FindClass("java/lang/Thread");
        if (!thread_class) break;
        jmethodID current = env->GetStaticMethodID(thread_class, "currentThread", "()Ljava/lang/Thread;");
        jmethodID context = env->GetMethodID(thread_class, "getContextClassLoader", "()Ljava/lang/ClassLoader;");
        if (!current || !context) break;
        jobject thread = env->CallStaticObjectMethod(thread_class, current);
        if (env->ExceptionCheck() || !thread) break;
        jobject loader = env->CallObjectMethod(thread, context);
        if (env->ExceptionCheck() || !loader) break;
        jclass loader_class = env->FindClass("java/lang/ClassLoader");
        if (!loader_class) break;
        jmethodID load = env->GetMethodID(loader_class, "loadClass", "(Ljava/lang/String;)Ljava/lang/Class;");
        if (!load) break;
        jstring name = env->NewStringUTF(class_name);
        if (!name) break;
        jclass target = static_cast<jclass>(env->CallObjectMethod(loader, load, name));
        if (env->ExceptionCheck() || !target) break;
        bound = env->RegisterNatives(target, methods, count) == JNI_OK;
        if (bound && bound_class) {
            *bound_class = static_cast<jclass>(env->NewGlobalRef(target));
            if (!*bound_class) bound = false;
        }
        if (!bound) {
            jthrowable failure = env->ExceptionOccurred();
            env->ExceptionClear();
            env->UnregisterNatives(target);
            if (env->ExceptionCheck()) env->ExceptionClear();
            if (failure) env->Throw(failure);
        }
    } while (false);
    // Leave any pending exception for the Java bootstrap to report; no silent
    // fallback to loading a second library into another namespace.
    env->PopLocalFrame(nullptr);
    return bound;
}
}
