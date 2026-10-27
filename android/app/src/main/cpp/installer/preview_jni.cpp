#include "bem_preview.h"
#include <jni.h>
#include <stdexcept>
namespace {
struct Text {
    JNIEnv* env;jstring source;const char* value;
    Text(JNIEnv* e,jstring s):env(e),source(s),value(e->GetStringUTFChars(s,nullptr)) {if(!value)throw std::runtime_error("Cannot read preview request");}
    ~Text(){env->ReleaseStringUTFChars(source,value);}
};
}
extern "C" JNIEXPORT jstring JNICALL Java_dev_betterendfield_next_BemPreviewActivity_previewNative(
    JNIEnv* env,jclass,jstring input,jstring output,jstring selection,jstring parameters,jstring resource,jstring cancel,jstring capabilities) {
    try {
        Text path(env,input),destination(env,output),options(env,selection),ticks(env,parameters),selectedResource(env,resource),cancelPath(env,cancel),caps(env,capabilities);
        nlohmann::json report;std::string error;
        if(std::char_traits<char>::length(caps.value)>1024)throw std::runtime_error("Invalid preview GPU capabilities");
        auto json=nlohmann::json::parse(caps.value);BetterEndfieldNext::CustomModel::BemPreviewGpu gpu;
        gpu.native_assets=std::filesystem::u8path(json.value("nativeAssets",std::string{}));gpu.astc=json.value("astc",false);gpu.bc7=json.value("bc7",false);gpu.max_texture_size=json.value("maxTextureSize",0u);
        gpu.composition_draft=json.value("compositionDraft",false);
        gpu.texture_cache=std::filesystem::u8path(json.value("textureCache",std::string{}));
        if(gpu.max_texture_size>32768)throw std::runtime_error("Invalid preview texture size");
        auto checkpoint=[&]{if(std::filesystem::exists(cancelPath.value))throw std::runtime_error("Preview cancelled");};
        if(!BetterEndfieldNext::CustomModel::ExportBemPreview(path.value,destination.value,options.value,ticks.value,selectedResource.value,checkpoint,report,error,gpu)) throw std::runtime_error(error);
        return env->NewStringUTF(report.dump().c_str());
    } catch(const std::exception& exception) {env->ThrowNew(env->FindClass("java/io/IOException"),exception.what());return nullptr;}
}
