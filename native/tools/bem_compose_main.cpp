#include "../modules/custom_model/bem_compose.h"
#include "../modules/custom_model/bem_preview.h"
#include <algorithm>
#include <csignal>
#include <fstream>
#include <iostream>
#include <map>
using namespace BetterEndfieldNext::CustomModel;
using J=nlohmann::json;
namespace {
volatile std::sig_atomic_t cancelled=0;
void Cancel(int) {cancelled=1;}
std::string U8(const wchar_t* value) {auto text=std::filesystem::path(value).u8string();return {text.begin(),text.end()};}
J Read(const std::string& path) {
    auto p=std::filesystem::u8path(path);
    if(std::filesystem::file_size(p)>4ull*1024*1024) throw std::runtime_error("JSON input exceeds 4 MiB");
    std::ifstream stream(p,std::ios::binary);if(!stream) throw std::runtime_error("Cannot open JSON input");
    return J::parse(stream);
}
}
int wmain(int argc,wchar_t** argv) {
    std::signal(SIGINT,Cancel);
    try {
        if(argc<2) throw std::runtime_error("Usage: BemCompose plan --request request.json [--json plan.json] | build (or draft for temporary previews) --plan plan.json [--choices defaults.json] --output-dir NEW_DIRECTORY | validate --package file.bem");
        const auto command=U8(argv[1]);std::map<std::string,std::string> args;
        J request={{"schema",1},{"platform","windows-x64"},{"sources",J::array()}};
        for(int i=2;i<argc;i+=2) {
            if(i+1>=argc) throw std::runtime_error("Missing argument value");
            const auto key=U8(argv[i]),value=U8(argv[i+1]);
            if(key=="--source") request["sources"].push_back({{"path",value},{"selection",""}});
            else if(key=="--selection") {
                if(request["sources"].empty()) throw std::runtime_error("--selection must follow --source");
                request["sources"].back()["selection"]=value;
            } else {
                if(key!="--gpu" && key!="--resource" && key!="--parameters" && key!="--options" && key!="--request" && key!="--json" && key!="--plan" && key!="--choices" && key!="--output-dir" && key!="--package" &&
                   key!="--platform" && key!="--project-id" && key!="--name" && key!="--author" && key!="--version" && key!="--cancel-file")
                    throw std::runtime_error("Unknown argument: "+key);
                if(!args.emplace(key,value).second) throw std::runtime_error("Duplicate argument: "+key);
            }
        }
        if(args.contains("--json") && std::filesystem::exists(std::filesystem::u8path(args.at("--json"))))
            throw std::runtime_error("JSON output already exists");
        auto checkpoint=[&] {
            if(cancelled || (args.contains("--cancel-file") && std::filesystem::exists(std::filesystem::u8path(args.at("--cancel-file")))))
                throw std::runtime_error("COMPOSE_CANCELLED");
        };
        J result;std::string failure;bool ok=false;checkpoint();
        if(command=="plan") {
            if(args.contains("--request")) request=Read(args.at("--request"));
            else {
                for(const auto& [flag,key]:std::map<std::string,std::string>{{"--platform","platform"},{"--project-id","project_id"},{"--name","name"},{"--author","author"},{"--version","version"}})
                    if(args.contains(flag)) request[key]=args.at(flag);
            }
            ok=PlanBemComposition(request,result,failure);
        } else if(command=="build" || command=="draft") {
            ok=(command=="draft" ? DraftBemComposition : BuildBemComposition)(Read(args.at("--plan")),args.contains("--choices")?Read(args.at("--choices")):J::object(),
                std::filesystem::u8path(args.at("--output-dir")),result,checkpoint,failure);
        } else if(command=="preview") {
            BemPreviewGpu gpu;
            if(args.contains("--gpu")) {
                if(args.at("--gpu").size()>1024)throw std::runtime_error("Invalid preview GPU capabilities");
                auto caps=J::parse(args.at("--gpu"));gpu.native_assets=std::filesystem::u8path(caps.value("nativeAssets",std::string{}));gpu.astc=caps.value("astc",false);gpu.bc7=caps.value("bc7",false);gpu.max_texture_size=caps.value("maxTextureSize",0u);
                gpu.texture_cache=std::filesystem::u8path(caps.value("textureCache",std::string{}));
                gpu.composition_draft=caps.value("compositionDraft",false);
                gpu.desktop=caps.value("desktop",false);
                if(gpu.max_texture_size>32768)throw std::runtime_error("Invalid preview texture size");
            }
            ok=ExportBemPreview(std::filesystem::u8path(args.at("--package")),std::filesystem::u8path(args.at("--output-dir")),
                args.contains("--options")?args.at("--options"):"",args.contains("--parameters")?args.at("--parameters"):"",
                args.contains("--resource")?args.at("--resource"):"",checkpoint,result,failure,gpu);
        } else if(command=="validate") {
            if(args.contains("--platform") && args.at("--platform")!="windows-x64") throw std::runtime_error("COMPOSE_UNSUPPORTED_PLATFORM");
            const auto path=std::filesystem::u8path(args.at("--package")); BemPackageInfo info;
            ok=ReadBemPackageInfo(path,info,failure);
            if(ok) {
                auto load=[&](const std::string& resource) {
                    checkpoint();BemPocData parsed;
                    if(!LoadBem(path,parsed,failure,{},nullptr,false,true,{},UINT64_MAX,false,resource)) throw std::runtime_error(failure);
                };
                if(info.resources.empty()) load({});else {
                    bool windows=false;
                    for(const auto& resource:info.resources) if(std::find(resource.platforms.begin(),resource.platforms.end(),"windows-x64")!=resource.platforms.end()) {windows=true;load(resource.id);}
                    if(!windows) throw std::runtime_error("COMPOSE_UNSUPPORTED_PLATFORM");
                }
                result={{"package_id",info.package_id},{"bem_minor",info.minor}};
            }
        } else throw std::runtime_error("Unknown command: "+command);
        if(!ok) throw std::runtime_error(failure);
        // A plan/report export must never overwrite any existing input or output.
        if(args.contains("--json")) {
            auto path=std::filesystem::u8path(args.at("--json"));
            if(std::filesystem::exists(path)) throw std::runtime_error("JSON output already exists");
            std::ofstream stream(path,std::ios::binary);stream<<result.dump(2);stream.close();
            if(!stream) throw std::runtime_error("Cannot write JSON output");
        }
        std::cout<<J{{"success",true},{"result",result}}.dump(2)<<'\n';return 0;
    } catch(const std::exception& e) {std::cout<<J{{"success",false},{"error",e.what()}}.dump(2)<<'\n';return 2;}
}
