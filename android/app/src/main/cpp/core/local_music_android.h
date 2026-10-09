#pragma once
#include <jni.h>
namespace betterendfieldnext {
// Initialize before modules start; bridge_class is already a global ref.
bool InitializeAndroidMusic(JavaVM* vm, JNIEnv* env, jclass bridge_class);
}
