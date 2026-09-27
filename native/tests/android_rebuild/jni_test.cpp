#include "core/jni_binding.h"
static jint Protocol(JNIEnv*,jclass){return 77;}
extern "C" JNIEXPORT jint JNICALL JNI_OnLoad(JavaVM*vm,void*){
 JNIEnv*env=nullptr;if(vm->GetEnv(reinterpret_cast<void**>(&env),JNI_VERSION_1_6)!=JNI_OK)return JNI_ERR;
 JNINativeMethod methods[]{{const_cast<char*>("protocol"),const_cast<char*>("()I"),reinterpret_cast<void*>(&Protocol)}};
 return betterendfield::BindContextLoaderNatives(env,"bridge.Bridge",methods,1)?JNI_VERSION_1_6:JNI_ERR;
}
