#include "../modules/custom_model/bem.h"
#include "../modules/custom_model/mod_registry.h"
#include <iostream>
#include <fstream>
using namespace BetterEndfield::CustomModel;
#ifdef _WIN32
int wmain(int argc,wchar_t** argv) {
#else
int main(int argc,char** argv) {
#endif
    if(argc<2) return 2;
    std::string error; BemPackageInfo info;
    if(!ReadBemPackageInfo(argv[1],info,error)) {std::cerr<<error;return 1;}
    for(const auto& appearance:info.appearances) {
        BemPocData data;
        if(!LoadBem(argv[1],data,error,appearance)) {std::cerr<<appearance<<": "<<error;return 1;}
        std::cout<<appearance<<": components="<<data.components.size()<<" textures="<<data.textures.size()<<'\n';
    }
    auto utf=std::filesystem::path(argv[1]).filename().u8string();
    std::string ini="[CustomModel]\nstandalone_lod=false\n[Mod.test]\nenabled=true\npackage="+
        std::string(reinterpret_cast<const char*>(utf.data()),utf.size())+"\nappearance="+info.default_appearance+"\n";
    ModRegistry registry;
    if(!ParseModRegistry(ini,std::filesystem::path(argv[1]).parent_path(),registry,error) || registry.enabled.size()!=1 ||
        !registry.Match(info.world_resource)||!registry.Match(info.ui_resource)) {std::cerr<<"Registry: "<<error;return 1;}
    std::cout<<"BEMv1 package and world/UI routing accepted\n";
    return 0;
}
