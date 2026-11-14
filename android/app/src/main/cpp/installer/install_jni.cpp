#include "texture_install.h"
#include "bem_baseline_table.generated.h"
#include <jni.h>
#include <atomic>
#include <stdexcept>
namespace { std::atomic_bool cancelled{false},busy{false};
struct Utf {JNIEnv* env;jstring value;const char* data;Utf(JNIEnv* e,jstring v):env(e),value(v),data(e->GetStringUTFChars(v,nullptr)) {if(!data) throw std::runtime_error("Cannot read JNI string");}~Utf(){env->ReleaseStringUTFChars(value,data);}};
}
extern "C" JNIEXPORT void JNICALL Java_dev_betterendfield_next_BemInstaller_cancelNative(JNIEnv*,jclass) {
    cancelled=true;betterendfieldnext::CancelTextureCompression();
}
// Import validates every appearance but leaves the selected package byte-for-byte intact.
extern "C" JNIEXPORT jstring JNICALL Java_dev_betterendfield_next_BemInstaller_inspectNativeWithOptions(JNIEnv* env,jclass,jstring src,jboolean skip_validation) {
    try {
        Utf input(env,src);
        using namespace BetterEndfieldNext::CustomModel;
        BemPackageInfo info; std::string error;
        if(!ReadBemPackageInfo(input.data,info,error,skip_validation)) throw std::runtime_error(error);
        auto selections=info.appearances;
        if(info.minor) selections.push_back(info.default_options);
        for(const auto& appearance:selections) {
            BemPocData parsed;
            if(!LoadBem(input.data,parsed,error,appearance,nullptr,skip_validation)) throw std::runtime_error(error);
        }
        BemJson report={{"package_id",info.package_id},{"character_id",info.character_id},{"name",info.name},
            {"target_kind",info.target_kind},{"target_id",info.target_id},{"resource_keys",info.resource_keys},
            {"bytes",std::filesystem::file_size(input.data)},{"bem_minor",info.minor}};
        // Section 15. Absent for packages built before the field existed.
        if(!info.baseline_windows.empty())
            report["game_baseline"]={{"windows-x64",info.baseline_windows},
                {"android-arm64",info.baseline_android},{"hotfix",info.baseline_hotfix}};
        if(info.minor) {report["default_options"]=info.default_options;
            report["option_groups"]=BemJson::parse(info.option_groups_json);
            report["selection_constraints"]=BemJson::parse(info.selection_constraints_json);}
        else {report["default_appearance"]=info.default_appearance;report["appearances"]=info.appearances;}
        report["parameters"]=BemJson::parse(info.parameter_groups_json);
        report["parameter_groups_json"]=info.parameter_groups_json;
        report["default_parameters"]=info.default_parameters;
        if (!info.manifest_json.empty()) {
            try {
                auto manifest = BemJson::parse(info.manifest_json);
                if (manifest.contains("drivers") && manifest["drivers"].is_array()) {
                    report["drivers"] = manifest["drivers"];
                }
            } catch (...) {}
        }
        auto encoded=report.dump();
        return env->NewStringUTF(encoded.c_str());
    } catch(const std::exception& error) {env->ThrowNew(env->FindClass("java/io/IOException"),error.what());return nullptr;}
}
// Metadata only: the header and manifest, never a payload block. The management
// UI uses this to decide whether a package needs a baseline before opening it.
extern "C" JNIEXPORT jstring JNICALL Java_dev_betterendfield_next_BemInstaller_metadataNative(
    JNIEnv* env,jclass,jstring src) {
    try {
        Utf input(env,src);
        using namespace BetterEndfieldNext::CustomModel;
        BemPackageInfo info; std::string error;
        if(!ReadBemManagementInfo(input.data,info,error)) throw std::runtime_error(error);
        BemJson report={{"package_id",info.package_id},{"name",info.name},{"revision",info.revision},
            {"bem_minor",info.minor},{"bytes",std::filesystem::file_size(input.data)}};
        if(!info.baseline_windows.empty())
            report["game_baseline"]={{"windows-x64",info.baseline_windows},
                {"android-arm64",info.baseline_android},{"hotfix",info.baseline_hotfix}};
        return env->NewStringUTF(report.dump().c_str());
    } catch(const std::exception& error) {env->ThrowNew(env->FindClass("java/io/IOException"),error.what());return nullptr;}
}
// Returns null when the batch table does not know this revision, which is the
// signal to leave the package unknown instead of guessing a version.
extern "C" JNIEXPORT jstring JNICALL Java_dev_betterendfield_next_BemInstaller_baselineForRevisionNative(
    JNIEnv* env,jclass,jstring revision) {
    try {
        Utf value(env,revision);
        using namespace BetterEndfieldNext::CustomModel;
        for(size_t i=0;i<kBaselineBatchCount;++i) {
            const auto& batch=kBaselineBatches[i];
            if(std::string_view(batch.revision)==value.data)
                return env->NewStringUTF(BemJson{{"windows-x64",batch.windows},
                    {"android-arm64",batch.android},{"hotfix",batch.hotfix}}.dump().c_str());
        }
        return nullptr;
    } catch(const std::exception& error) {env->ThrowNew(env->FindClass("java/io/IOException"),error.what());return nullptr;}
}
// Backfills the section 15 baseline into an older package. Payload blocks are
// copied verbatim, so this stays cheap even for large packages, and a package
// that already declares the field is reported as skipped rather than rewritten.
extern "C" JNIEXPORT jstring JNICALL Java_dev_betterendfield_next_BemInstaller_insertBaselineNative(
    JNIEnv* env,jclass,jstring src,jstring dst,jstring windows,jstring android,jint hotfix) {
    try {
        if(hotfix<0) throw std::runtime_error("Invalid hotfix counter");
        Utf input(env,src),output(env,dst),w(env,windows),a(env,android);
        using namespace BetterEndfieldNext::CustomModel;
        auto value=BemJson{{"windows-x64",w.data},{"android-arm64",a.data},{"hotfix",static_cast<int>(hotfix)}}.dump();
        std::string error;
        if(!InsertBemManifestField(input.data,output.data,"game_baseline",value,error)) {
            if(error=="Field already present") return env->NewStringUTF("skipped");
            throw std::runtime_error(error);
        }
        return env->NewStringUTF("written");
    } catch(const std::exception& error) {env->ThrowNew(env->FindClass("java/io/IOException"),error.what());return nullptr;}
}
extern "C" JNIEXPORT jstring JNICALL Java_dev_betterendfield_next_BemInstaller_convertNative(JNIEnv* env,jclass owner,jstring src,jstring dst,jstring rules,jboolean astc) {    if(busy.exchange(true)) {env->ThrowNew(env->FindClass("java/io/IOException"),"Another conversion is running");return nullptr;}
    struct Guard {~Guard(){busy=false;}} guard; cancelled=false;
    try {
        Utf input(env,src),output(env,dst),mapping(env,rules);
        auto table=BetterEndfieldNext::CustomModel::BemJson::parse(mapping.data);
        auto checkpoint=[] {if(cancelled.load()) throw std::runtime_error("Installation cancelled");};
        auto progressMethod=env->GetStaticMethodID(owner,"conversionProgress","(Ljava/lang/String;IIIIF)V");
        if(!progressMethod) return nullptr;
        unsigned textureIndex=0;
        std::string report,error;
        bool ok=BetterEndfieldNext::CustomModel::RewriteBemTextures(input.data,output.data,
            [&](const auto& manifest,auto& texture,auto& bytes) {
                ++textureIndex;
                auto name=texture.at("original_name").template get<std::string>();
                auto progress=[&](unsigned mip,unsigned mips,float percent) {
                    auto label=env->NewStringUTF(name.c_str());
                    if(!label) throw std::runtime_error("Cannot report conversion progress");
                    env->CallStaticVoidMethod(owner,progressMethod,label,jint(textureIndex),jint(manifest.at("textures").size()),jint(mip),jint(mips),jfloat(percent));
                    env->DeleteLocalRef(label);
                };
                betterendfieldnext::ConvertInstalledTexture(manifest,texture,bytes,table,astc,checkpoint,progress);
            },checkpoint,report,error);
        if(!ok) throw std::runtime_error(error);
        return env->NewStringUTF(report.c_str());
    } catch(const std::exception& error) {env->ThrowNew(env->FindClass("java/io/IOException"),error.what());return nullptr;}
}
extern "C" JNIEXPORT jstring JNICALL Java_dev_betterendfield_next_BemInstaller_repairNormalsNative(JNIEnv* env,jclass owner,jstring src,jstring dst,jboolean astc) {
    if(busy.exchange(true)) {env->ThrowNew(env->FindClass("java/io/IOException"),"Another operation is running");return nullptr;}
    struct Guard {~Guard(){busy=false;}} guard; cancelled=false;
    try {
        Utf input(env,src),output(env,dst);
        auto checkpoint=[] {if(cancelled.load()) throw std::runtime_error("Operation cancelled");};
        auto progressMethod=env->GetStaticMethodID(owner,"conversionProgress","(Ljava/lang/String;IIIIF)V");
        if(!progressMethod) return nullptr;
        unsigned textureIndex=0,repairedCount=0;
        std::string report,error;
        bool ok=BetterEndfieldNext::CustomModel::RewriteBemTextures(input.data,output.data,
            [&](const auto& manifest,auto& texture,auto& bytes) {
                ++textureIndex;
                auto name=texture.at("original_name").template get<std::string>();
                auto progress=[&](unsigned mip,unsigned mips,float percent) {
                    auto label=env->NewStringUTF(name.c_str());
                    if(!label) throw std::runtime_error("Cannot report progress");
                    env->CallStaticVoidMethod(owner,progressMethod,label,jint(textureIndex),jint(manifest.at("textures").size()),jint(mip),jint(mips),jfloat(percent));
                    env->DeleteLocalRef(label);
                };
                if(betterendfieldnext::RepairNormalTexture(manifest,texture,bytes,astc,checkpoint,progress)) {
                    ++repairedCount;
                }
            },checkpoint,report,error);
        if(!ok) throw std::runtime_error(error);
        if(repairedCount==0) {
            std::filesystem::remove(output.data);
            return env->NewStringUTF("{\"repaired\":false,\"count\":0}");
        }
        return env->NewStringUTF(report.c_str());
    } catch(const std::exception& error) {env->ThrowNew(env->FindClass("java/io/IOException"),error.what());return nullptr;}
}
