#include "../modules/custom_model/bem.h"
#include "../modules/custom_model/mod_registry.h"
#include "../shared/third_party/nlohmann/json.hpp"
#include <chrono>
#include <cmath>
#include <cstring>
#include <filesystem>
#include <fstream>
#include <iostream>
#include <limits>
#include <stdexcept>
using namespace BetterEndfield::CustomModel;
using J=nlohmann::json;
namespace {
size_t checks=0;
void Check(bool condition,const std::string& reason) {++checks;if(!condition) throw std::runtime_error(reason);}
#pragma pack(push,1)
struct Header {char magic[8];uint16_t major,minor;uint32_t size;uint64_t file,manifest;uint32_t count,flags;};
struct Entry {uint32_t codec,reserved;uint64_t offset,stored,decoded;};
#pragma pack(pop)
template<class T> std::vector<uint8_t> Bytes(const std::vector<T>& values) {
    std::vector<uint8_t> result(values.size()*sizeof(T));std::memcpy(result.data(),values.data(),result.size());return result;
}
std::vector<uint8_t> Delta(uint32_t vertex,float x,float y,float z,bool empty=false) {
    const auto header=J{{"count",empty?0:1},{"encoding","sparse-position-f32"}}.dump();
    std::vector<uint8_t> result(4+header.size()+(empty?0:16));const uint32_t length=static_cast<uint32_t>(header.size());
    std::memcpy(result.data(),&length,4);std::memcpy(result.data()+4,header.data(),header.size());
    if(!empty) {std::memcpy(result.data()+4+length,&vertex,4);const float xyz[]{x,y,z};std::memcpy(result.data()+8+length,xyz,12);}
    return result;
}
struct Fixture {
    J manifest;
    std::vector<std::vector<uint8_t>> payloads;
    Fixture() {
        payloads={Bytes<float>({1,2,3,4,5,6,7,8,9}),Bytes<float>({0,0,1,0,0,1}),
            std::vector<uint8_t>(12),Bytes<uint16_t>({0,1,2}),Delta(0,2,0,0),Delta(0,6,0,0),Delta(0,0,0,-2),Delta(0,0,0,4)};
        const auto frame=[](uint32_t tick,uint32_t payload){return J{{"value",tick},{"payload",payload},{"count",1},{"encoding","sparse-position-f32"}};};
        manifest={{"schema",1},{"package_id","bem.morph.test"},{"name","Morph test"},{"author","Tests"},{"version","1"},
            {"required_capabilities",J::array({"composable-options","body-parameters","mesh-position-deltas"})},
            {"target",{{"platform","windows-x64"},{"character_id","chr_test"},{"profile_id","test"},{"revision","r1"},
                {"world_resource","chr_test_postmodel"},{"ui_resource","chr_test_uimodel"},
                {"components",J::array({{{"id",0},{"mesh_name","Body"},{"original_index_count",3},{"bone_names",J::array({"root"})},{"materials",J::array({"body"})}}})}}},
            {"option_groups",J::array({{{"id","base"},{"name","Base"},{"default","on"},
                {"choices",J::array({{{"id","on"},{"name","On"}},{{"id","off"},{"name","Off"}}})}}})},
            {"component_rules",J::array({{{"target",0},{"candidates",J::array({{{"operation","replace"},{"mesh",0}}})}}})},
            {"meshes",J::array({{{"vertex_count",3},{"index_size",2},
                {"streams",J::array({{{"payload",0},{"stride",12}},{{"payload",1},{"stride",8}},{{"payload",2},{"stride",4}}})},
                {"attributes",J::array({J::array({0,0,3,0,0}),J::array({4,0,2,1,0}),J::array({13,6,4,2,0})})},
                {"bones",J::array({{{"component",0},{"index",0},{"name","root"}}})},
                {"draws",J::array({{{"indices",3},{"count",3},{"material_component",0},{"material_slot",0},{"material_name","body"},{"textures",J::array()}}})}}})},
            {"textures",J::array()},
            {"parameters",J::array({{{"id","width"},{"name","Width"},{"min",0},{"max",1000},{"neutral",0},{"default",0},{"step",1}},
                {{"id","height"},{"name","Height"},{"min",0},{"max",1000},{"neutral",500},{"default",500},{"step",1},{"available_when",{{"eq",J::array({"base","on"})}}}}})},
            {"mesh_deformations",J::array({{{"mesh",0},{"parameter","width"},{"frames",J::array({{{"value",0},{"neutral",true}},frame(500,4),frame(1000,5)})}},
                {{"mesh",0},{"parameter","height"},{"frames",J::array({frame(0,6),{{"value",500},{"neutral",true}},frame(1000,7)})}}})}};
    }
    std::vector<uint8_t> Pack(uint16_t minor=3) const {
        const auto json=manifest.dump();Header header{};std::memcpy(header.magic,"BEM\0PKG\0",8);header.major=1;header.minor=minor;
        header.size=sizeof(Header);header.manifest=json.size();header.count=static_cast<uint32_t>(payloads.size());
        uint64_t offset=sizeof(Header)+json.size()+payloads.size()*sizeof(Entry);std::vector<Entry> entries;
        for(const auto& bytes:payloads) {entries.push_back({0,0,offset,bytes.size(),bytes.size()});offset+=bytes.size();}header.file=offset;
        std::vector<uint8_t> result(static_cast<size_t>(offset));std::memcpy(result.data(),&header,sizeof(header));
        std::memcpy(result.data()+sizeof(header),json.data(),json.size());std::memcpy(result.data()+sizeof(header)+json.size(),entries.data(),entries.size()*sizeof(Entry));
        for(size_t i=0;i<payloads.size();++i) std::memcpy(result.data()+entries[i].offset,payloads[i].data(),payloads[i].size());return result;
    }
};
struct Temp {
    std::filesystem::path path=std::filesystem::temp_directory_path()/("bem13-morph-"+std::to_string(std::chrono::steady_clock::now().time_since_epoch().count())+".bem");
    ~Temp(){std::error_code ignored;std::filesystem::remove(path,ignored);}
    void Write(const std::vector<uint8_t>& bytes){std::ofstream out(path,std::ios::binary);out.write(reinterpret_cast<const char*>(bytes.data()),bytes.size());Check(bool(out),"Fixture write failed");}
};
void Position(const BemPocData& data,float x,float y,float z) {
    float xyz[3];std::memcpy(xyz,data.components.at(0).streams[0].data(),12);
    Check(std::abs(xyz[0]-x)<0.00001f && std::abs(xyz[1]-y)<0.00001f && std::abs(xyz[2]-z)<0.00001f,"Unexpected morphed position");
}
}
int main() {try {
    Fixture fixture;Temp file;file.Write(fixture.Pack());std::string error;BemPackageInfo info;
    Check(ReadBemPackageInfo(file.path,info,error),error);Check(info.minor==3 && info.default_parameters=="width:0&height:500","Missing parameter metadata");
    std::string canonical;Check(ResolveBemParameters(info,"height:750&width:750",canonical,error),error);
    Check(canonical=="width:750&height:750","Parameter order is not canonical");
    Check(!ResolveBemParameters(info,"width:1001",canonical,error),"Out-of-range tick accepted");
    Check(!ResolveBemParameters(info,"width:2.5",canonical,error),"Fractional wire tick accepted");
    Check(!ResolveBemParameters(info,"width:-1",canonical,error),"Negative tick accepted");
    Check(!ResolveBemParameters(info,"width:1&width:2",canonical,error),"Duplicate parameter accepted");
    Check(!ResolveBemParameters(info,"removed:1",canonical,error),"Unknown parameter accepted");
    Check(!ResolveBemParameters(info,"width:1&",canonical,error),"Trailing parameter separator accepted");
    BemPocData base,a,b,again,optimized;BemLoadStats neutralStats,stats,optStats;
    Check(LoadBem(file.path,base,error,{},&neutralStats),error);Position(base,1,2,3);
    Check(neutralStats.payload_ids==std::vector<uint32_t>({0,1,2,3}),"Neutral read delta payloads");
    Check(LoadBem(file.path,a,error,{},&stats,false,false,"width:750&height:750"),error);Position(a,5,2,5);
    Check(LoadBem(file.path,b,error,{},nullptr,false,false,"width:250&height:0"),error);Position(b,2,2,1);
    Check(LoadBem(file.path,again,error,{},nullptr,false,false,"width:750&height:750"),error);
    Check(a.components[0].streams==again.components[0].streams,"A-B-A accumulated morph drift");
    Check(a.components[0].streams[1]==base.components[0].streams[1] && a.components[0].streams[2]==base.components[0].streams[2] &&
        a.components[0].indices==base.components[0].indices && a.components[0].bone_names==base.components[0].bone_names &&
        a.components[0].material_names==base.components[0].material_names,"Morph changed skin/UV/index/material data");
    Check(LoadBem(file.path,optimized,error,{},&optStats,false,true,"width:750&height:750"),error);
    Check(optimized.components[0].streams==a.components[0].streams && optStats.payload_ids==stats.payload_ids && optStats.decoded_cache_remaining_bytes==0,"Optimized morph decode changed output or retained buffers");
    Check(LoadBem(file.path,b,error,"base:off",nullptr,false,false,"width:750&height:750"),error);Position(b,5,2,3);
    const auto ini="[CustomModel]\nhot_switch=true\n[Mod.test]\nenabled=true\npackage="+file.path.string()+"\nparameters=height:750&width:750\n";
    ModRegistry registry;Check(ParseModRegistry(ini,{},registry,error),error);
    Check(registry.enabled.size()==1 && registry.enabled[0].parameters=="width:750&height:750","Runtime registry lost parameters");
    const auto key=registry.enabled[0].selection_key;
    Check(ParseModRegistry(ini+"options=base:off\n",{},registry,error),error);Check(key!=registry.enabled[0].selection_key,"Option change was absent from selection identity");
    Check(ParseModRegistry(ini.substr(0,ini.find("parameters="))+"parameters=width:250\n",{},registry,error),error);
    Check(key!=registry.enabled[0].selection_key,"Parameter change was absent from selection identity");
    auto reject=[&](Fixture bad,const char* reason,const char* selected="width:1000&height:500") {
        file.Write(bad.Pack());BemPocData output;Check(!LoadBem(file.path,output,error,{},nullptr,false,false,selected),reason);Check(output.components.empty(),"Failed decode exposed partial output");
    };
    {auto bad=fixture;bad.payloads[5]=Delta(3,1,0,0);reject(bad,"Out-of-range delta vertex accepted");}
    {auto bad=fixture;bad.payloads[5]=Delta(0,std::numeric_limits<float>::quiet_NaN(),0,0);reject(bad,"NaN delta accepted");}
    {auto bad=fixture;bad.payloads[5]=Delta(0,std::numeric_limits<float>::infinity(),0,0);reject(bad,"Infinite delta accepted");}
    {auto bad=fixture;bad.payloads[5]=Delta(0,std::numeric_limits<float>::max(),0,0);
        const float value=std::numeric_limits<float>::max();std::memcpy(bad.payloads[0].data(),&value,4);reject(bad,"Overflowing morphed position accepted");}
    {auto bad=fixture;const auto header=J{{"count",2},{"encoding","sparse-position-f32"}}.dump();
        const auto record=std::vector<uint8_t>(bad.payloads[5].end()-16,bad.payloads[5].end());
        bad.payloads[5].resize(4+header.size()+32);const uint32_t size=static_cast<uint32_t>(header.size());
        std::memcpy(bad.payloads[5].data(),&size,4);std::memcpy(bad.payloads[5].data()+4,header.data(),header.size());
        for(size_t i=0;i<2;++i) std::memcpy(bad.payloads[5].data()+4+header.size()+i*16,record.data(),16);
        bad.manifest["mesh_deformations"][0]["frames"][2]["count"]=2;reject(bad,"Duplicate delta vertex accepted");}
    {auto bad=fixture;bad.payloads[5].push_back(0);reject(bad,"Trailing delta bytes accepted");}
    {auto bad=fixture;bad.manifest["mesh_deformations"][0]["frames"][2]["count"]=2;reject(bad,"Frame/payload count mismatch accepted");}
    {auto bad=fixture;bad.manifest["mesh_deformations"][0]["frames"][0]["value"]=1;reject(bad,"Wrong neutral accepted");}
    {auto bad=fixture;bad.manifest["mesh_deformations"][0]["frames"][1]["value"]=1000;reject(bad,"Unordered frames accepted");}
    {auto bad=fixture;bad.manifest["required_capabilities"]=J::array({"composable-options","body-parameters"});reject(bad,"Missing delta capability accepted");}
    {auto bad=fixture;bad.manifest["meshes"][0]["attributes"][0][1]=1;reject(bad,"Float16 morph position accepted");}
    {auto bad=fixture;bad.manifest["parameters"][0]["step"]=3;reject(bad,"Misaligned parameter range accepted");}
    {auto empty=fixture;empty.payloads[5]=Delta(0,0,0,0,true);empty.manifest["mesh_deformations"][0]["frames"][2]["count"]=0;
        file.Write(empty.Pack());Check(LoadBem(file.path,b,error,{},nullptr,false,false,"width:1000"),error);Position(b,1,2,3);}
    {auto shapeOnly=fixture;shapeOnly.manifest["option_groups"]=J::array();shapeOnly.manifest["parameters"][1].erase("available_when");
        file.Write(shapeOnly.Pack());Check(LoadBem(file.path,b,error,{},nullptr,false,false,"width:750&height:750"),error);Position(b,5,2,5);}
    for(uint16_t minor:{uint16_t{1},uint16_t{2}}) {
        auto old=fixture;old.manifest.erase("parameters");old.manifest.erase("mesh_deformations");old.manifest["required_capabilities"]=J::array({"composable-options"});
        file.Write(old.Pack(minor));Check(LoadBem(file.path,b,error),error);Position(b,1,2,3);
        Check(ReadBemPackageInfo(file.path,info,error) && info.default_parameters.empty(),"Legacy package changed metadata");
    }
    {auto old=fixture;old.manifest.erase("parameters");old.manifest.erase("mesh_deformations");old.manifest.erase("option_groups");old.manifest.erase("component_rules");
        old.manifest["required_capabilities"]=J::array({"fixed-appearances"});old.manifest["default_appearance_id"]="default";
        old.manifest["appearances"]=J::array({{{"id","default"},{"name","Default"},{"components",J::array({{{"target",0},{"operation","replace"},{"mesh",0}}})}}});
        old.manifest["meshes"][0]["indices"]=3;old.manifest["meshes"][0]["index_count"]=3;old.manifest["meshes"][0]["draws"][0]["start"]=0;
        file.Write(old.Pack(0));Check(LoadBem(file.path,b,error),error);Position(b,1,2,3);}
    file.Write(fixture.Pack(2));Check(!LoadBem(file.path,b,error),"BEM 1.2 accepted 1.3 feature");
    std::cout<<"PASS: BEM 1.3 position morph, multi-axis/neutral/availability/A-B-A/selected payloads/optimization/registry/legacy ("<<checks<<" checks)\n";
    return 0;
}catch(const std::exception& error){std::cerr<<error.what()<<'\n';return 1;}}
