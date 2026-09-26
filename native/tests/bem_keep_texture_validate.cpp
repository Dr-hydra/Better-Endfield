#include "../modules/custom_model/bem.h"
#include <filesystem>
#include <iostream>
using namespace BetterEndfield::CustomModel;
#ifdef _WIN32
int wmain(int argc,wchar_t** argv) {
#else
int main(int argc,char** argv) {
#endif
    if(argc!=2) return 2;
    BemPocData bem;std::string error;
    if(!LoadBem(std::filesystem::path(argv[1]),bem,error,"base:on")) {std::cerr<<error;return 1;}
    if(bem.components.size()!=10 || bem.textures.size()!=2) return 1;
    for(size_t id:{size_t{2},size_t{3}}) {
        const auto& c=bem.components[id];
        if(!(c.info.flags&kComponentFlagNoGeometry) || c.keep_material_overrides.size()!=1 ||
            c.keep_material_names.size()!=1 || c.keep_material_overrides[0].material_slot!=0 ||
            c.keep_material_overrides[0].textures!=3) return 1;
    }
    std::cout<<"PASS: keep C2/C3 retain native geometry and select both cloth texture payloads\n";
    return 0;
}
