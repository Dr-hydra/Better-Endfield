#include "texture_install.h"
#include <jni.h>
#include <atomic>
#include <stdexcept>
namespace { std::atomic_bool cancelled{false},busy{false};
struct Utf {JNIEnv* env;jstring value;const char* data;Utf(JNIEnv* e,jstring v):env(e),value(v),data(e->GetStringUTFChars(v,nullptr)) {if(!data) throw std::runtime_error("Cannot read JNI string");}~Utf(){env->ReleaseStringUTFChars(value,data);}};
}
extern "C" JNIEXPORT void JNICALL Java_dev_betterendfield_android_BemInstaller_cancelNative(JNIEnv*,jclass) {
    cancelled=true;betterendfield::CancelTextureCompression();
}
// Import validates every appearance but leaves the selected package byte-for-byte intact.
extern "C" JNIEXPORT jstring JNICALL Java_dev_betterendfield_android_BemInstaller_inspectNative(JNIEnv* env,jclass,jstring src) {
    try {
        Utf input(env,src);
        using namespace BetterEndfield::CustomModel;
        BemPackageInfo info; std::string error;
        if(!ReadBemPackageInfo(input.data,info,error)) throw std::runtime_error(error);
        auto selections=info.appearances;
        if(info.minor) selections.push_back(info.default_options);
        for(const auto& appearance:selections) {
            BemPocData parsed;
            if(!LoadBem(input.data,parsed,error,appearance)) throw std::runtime_error(error);
        }
        BemJson report={{"package_id",info.package_id},{"character_id",info.character_id},{"name",info.name},
            {"bytes",std::filesystem::file_size(input.data)},{"bem_minor",info.minor}};
        if(info.minor) {report["default_options"]=info.default_options;
            report["option_groups"]=BemJson::parse(info.option_groups_json);
            report["selection_constraints"]=BemJson::parse(info.selection_constraints_json);}
        else {report["default_appearance"]=info.default_appearance;report["appearances"]=info.appearances;}
        auto encoded=report.dump();
        return env->NewStringUTF(encoded.c_str());
    } catch(const std::exception& error) {env->ThrowNew(env->FindClass("java/io/IOException"),error.what());return nullptr;}
}
extern "C" JNIEXPORT jstring JNICALL Java_dev_betterendfield_android_BemInstaller_convertNative(JNIEnv* env,jclass owner,jstring src,jstring dst,jstring rules,jboolean astc) {
    if(busy.exchange(true)) {env->ThrowNew(env->FindClass("java/io/IOException"),"Another conversion is running");return nullptr;}
    struct Guard {~Guard(){busy=false;}} guard; cancelled=false;
    try {
        Utf input(env,src),output(env,dst),mapping(env,rules);
        auto table=BetterEndfield::CustomModel::BemJson::parse(mapping.data);
        auto checkpoint=[] {if(cancelled.load()) throw std::runtime_error("Installation cancelled");};
        auto progressMethod=env->GetStaticMethodID(owner,"conversionProgress","(Ljava/lang/String;IIIIF)V");
        if(!progressMethod) return nullptr;
        unsigned textureIndex=0;
        std::string report,error;
        bool ok=BetterEndfield::CustomModel::RewriteBemTextures(input.data,output.data,
            [&](const auto& manifest,auto& texture,auto& bytes) {
                ++textureIndex;
                auto name=texture.at("original_name").template get<std::string>();
                auto progress=[&](unsigned mip,unsigned mips,float percent) {
                    auto label=env->NewStringUTF(name.c_str());
                    if(!label) throw std::runtime_error("Cannot report conversion progress");
                    env->CallStaticVoidMethod(owner,progressMethod,label,jint(textureIndex),jint(manifest.at("textures").size()),jint(mip),jint(mips),jfloat(percent));
                    env->DeleteLocalRef(label);
                };
                betterendfield::ConvertInstalledTexture(manifest,texture,bytes,table,astc,checkpoint,progress);
            },checkpoint,report,error);
        if(!ok) throw std::runtime_error(error);
        return env->NewStringUTF(report.c_str());
    } catch(const std::exception& error) {env->ThrowNew(env->FindClass("java/io/IOException"),error.what());return nullptr;}
}
