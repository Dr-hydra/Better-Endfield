#include "../modules/custom_model/bem.h"
#include "../shared/third_party/nlohmann/json.hpp"
#include <chrono>
#include <cstring>
#include <filesystem>
#include <fstream>
#include <iostream>
#include <stdexcept>

using namespace BetterEndfield::CustomModel;
using J=nlohmann::json;

#pragma pack(push,1)
struct Header { char magic[8];uint16_t major,minor;uint32_t size;uint64_t file,manifest;uint32_t count,flags; };
#pragma pack(pop)
static_assert(sizeof(Header)==40);

static void Check(bool condition,const std::string& message) {
    if(!condition) throw std::runtime_error(message);
}
static std::vector<uint8_t> Package(size_t groupCount) {
    J groups=J::array();
    for(size_t i=0;i<groupCount;++i)
        groups.push_back({{"id","g"+std::to_string(i)},{"name","Group"},{"default","off"},
            {"choices",J::array({{{"id","off"},{"name","Off"}},{{"id","on"},{"name","On"}}})}});
    const auto last="g"+std::to_string(groupCount-1);
    J manifest={{"schema",1},{"package_id","bem.capacity.test"},{"name","Capacity test"},
        {"author","Tests"},{"version","1"},{"required_capabilities",J::array({"composable-options"})},
        {"target",{{"platform","windows-x64"},{"character_id","chr_test"},{"profile_id","test"},
            {"revision","r1"},{"world_resource","chr_test_postmodel"},{"ui_resource","chr_test_uimodel"},
            {"components",J::array({{{"id",0},{"mesh_name","Body"},{"original_index_count",3},
                {"bone_names",J::array()},{"materials",J::array()}}})}}},
        {"option_groups",groups},
        {"component_rules",J::array({{{"target",0},{"candidates",J::array({
            {{"operation","keep"},{"when",{{"eq",J::array({last,"off"})}}}},
            {{"operation","hide"},{"when",{{"eq",J::array({last,"on"})}}}}
        })}}})},
        {"meshes",J::array()},{"textures",J::array()}};
    auto json=manifest.dump();
    Header header{};std::memcpy(header.magic,"BEM\0PKG\0",8);
    header.major=1;header.minor=1;header.size=sizeof(Header);
    header.file=sizeof(Header)+json.size();header.manifest=json.size();
    std::vector<uint8_t> bytes(sizeof(Header)+json.size());
    std::memcpy(bytes.data(),&header,sizeof(Header));
    std::memcpy(bytes.data()+sizeof(Header),json.data(),json.size());
    return bytes;
}
int main() {
    try {
        for(size_t count:{size_t{33},size_t{64}}) {
            const auto bytes=Package(count);
            BemPocData parsed;std::string error;
            Check(ParseBem(bytes,parsed,error),error);
            Check(parsed.components.size()==1 && !(parsed.components[0].info.flags&kComponentFlagHidden),
                "Default option did not select keep");
            const auto path=std::filesystem::temp_directory_path()/
                ("bem-v11-capacity-"+std::to_string(std::chrono::steady_clock::now().time_since_epoch().count())+".bem");
            struct Cleanup {std::filesystem::path path;~Cleanup(){std::error_code ignored;std::filesystem::remove(path,ignored);}} cleanup{path};
            {std::ofstream out(path,std::ios::binary);Check(bool(out),"Cannot create test package");
                out.write(reinterpret_cast<const char*>(bytes.data()),bytes.size());Check(bool(out),"Cannot write test package");}
            BemPackageInfo info;
            Check(ReadBemPackageInfo(path,info,error),error);
            Check(info.minor==1 && J::parse(info.option_groups_json).size()==count,"Option group count changed");
            std::string selected;
            for(size_t i=0;i<count;++i) {if(i) selected+='&';selected+="g"+std::to_string(i)+":on";}
            Check(LoadBem(path,parsed,error,selected),error);
            Check(parsed.components.size()==1 && (parsed.components[0].info.flags&kComponentFlagHidden),
                "Last option group did not select hide");
            Check(!LoadBem(path,parsed,error,"g"+std::to_string(count-1)+":missing") &&
                error=="Unknown option choice","Invalid selected choice accepted");
        }
        BemPocData parsed;std::string error;
        Check(!ParseBem(Package(65),parsed,error) && error=="Invalid option groups",
            "More than 64 option groups accepted");
        std::cout<<"PASS: BEM 1.1 33/64-group metadata and selected-only runtime; 65 rejected\n";
        return 0;
    } catch(const std::exception& error) {std::cerr<<error.what()<<'\n';return 1;}
}
