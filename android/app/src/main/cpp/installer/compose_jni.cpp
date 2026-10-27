#include "bem_compose.h"
#include <jni.h>
#include <fstream>
#include <stdexcept>
namespace {
struct Text {
    JNIEnv* env; jstring source; const char* value;
    Text(JNIEnv* e,jstring s):env(e),source(s),value(s?e->GetStringUTFChars(s,nullptr):nullptr) {
        if(!value) throw std::runtime_error("Invalid composition request");
    }
    ~Text(){env->ReleaseStringUTFChars(source,value);}
};
nlohmann::json Read(const char* path) {
    if(std::filesystem::file_size(path)>32ull*1024*1024)throw std::runtime_error("Composition JSON exceeds 32 MiB");
    std::ifstream stream(path,std::ios::binary);if(!stream)throw std::runtime_error("Cannot open composition input");
    return nlohmann::json::parse(stream);
}
}
extern "C" JNIEXPORT jstring JNICALL Java_dev_betterendfield_next_BemComposeSession_runNative(
    JNIEnv* env,jclass,jstring action,jstring input,jstring choices,jstring output,jstring cancel) {
    try {
        Text verb(env,action),source(env,input),defaults(env,choices),destination(env,output),signal(env,cancel);
        nlohmann::json result;std::string failure;
        auto checkpoint=[&]{if(std::filesystem::exists(signal.value))throw std::runtime_error("COMPOSE_CANCELLED");};
        checkpoint();bool ok=false;using namespace BetterEndfieldNext::CustomModel;
        if(std::string(verb.value)=="plan") {
            auto request=Read(source.value);
            if(request.value("platform","")!="android-arm64")throw std::runtime_error("COMPOSE_UNSUPPORTED_PLATFORM");
            ok=PlanBemComposition(request,result,failure);
        } else {
            auto plan=Read(source.value);
            if(plan.value("platform","")!="android-arm64")throw std::runtime_error("COMPOSE_UNSUPPORTED_PLATFORM");
            if(std::string(verb.value)=="draft")ok=DraftBemComposition(plan,Read(defaults.value),destination.value,result,checkpoint,failure);
            else if(std::string(verb.value)=="build")ok=BuildBemComposition(plan,Read(defaults.value),destination.value,result,checkpoint,failure);
            else throw std::runtime_error("Invalid composition action");
        }
        if(!ok)throw std::runtime_error(failure);checkpoint();
        return env->NewStringUTF(result.dump().c_str());
    } catch(const std::exception& error){env->ThrowNew(env->FindClass("java/io/IOException"),error.what());return nullptr;}
}
