#pragma once
#include "math/character_face.h"
#include <filesystem>
#include <fstream>

namespace character_face_library {
static std::vector<std::shared_ptr<const character_face::Profile>> profiles;
static std::string error;

// Load once on the initialization worker, before any GUI/game hooks start.
// Readers subsequently share immutable profiles; no Unity objects are touched.
static void Load() {
  try {
    HMODULE module=nullptr;
    if(!GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS |
          GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
          reinterpret_cast<LPCWSTR>(&Load),&module))
      throw std::runtime_error("Cannot locate facial resources");
    wchar_t path[32768]={};
    DWORD length=GetModuleFileNameW(module,path,32768);
    if(!length||length>=32768)throw std::runtime_error("Plugin path too long");
    auto directory=std::filesystem::path(path).parent_path()/L"mmd"/L"character-faces";
    if(!std::filesystem::exists(directory)) {
      Log("[FACE] No character profiles installed; fixed mapping is available");
      return;
    }
    std::vector<std::filesystem::path> paths;
    uintmax_t total=0;
    for(const auto &entry:std::filesystem::directory_iterator(directory)) {
      auto name=entry.path().filename().u8string();
      if(entry.is_regular_file()&&name.size()>=10&&name.compare(name.size()-10,10,".face.json")==0) {
        paths.push_back(entry.path());
        auto size=entry.file_size();
        if(size>16*1024*1024||total>128*1024*1024-size||paths.size()>256)
          throw std::runtime_error("Character profile library exceeds size limits");
        total+=size;
      }
    }
    std::sort(paths.begin(),paths.end());
    for(const auto &file:paths) {
      try {
        std::ifstream stream(file,std::ios::binary);
        nlohmann::json json;stream>>json;
        profiles.push_back(std::make_shared<const character_face::Profile>(character_face::Read(json)));
      } catch(const std::exception &e) {
        error="Some character profiles could not be loaded";
        Log("[FACE] Skipping %s: %s",file.filename().u8string().c_str(),e.what());
      }
    }
    std::stable_sort(profiles.begin(),profiles.end(),[](const auto &a,const auto &b) {
      return a->key==b->key?a->label.size()<b->label.size():a->key<b->key;
    });
    Log("[FACE] Loaded %zu character profiles",profiles.size());
  } catch(const std::exception &e) {error=e.what();Log("[FACE] %s",error.c_str());}
}

static std::shared_ptr<const character_face::Profile> Select(const std::string &model,
    const std::vector<face_geometry::Bone> &nodes,const face_mixing::Hierarchy &hierarchy) {
  auto key=character_face::ModelKey(model);
  std::shared_ptr<const character_face::Profile> result;
  float best=1e30f;
  for(const auto &profile:profiles)if(profile->key==key) {
    if(!result)result=profile;
    if(hierarchy.ready) {
      auto binding=character_face::Bind(*profile,key,nodes,hierarchy);
      if(binding.ready&&binding.error<best){best=binding.error;result=profile;}
    }
  }
  return result;
}
} // namespace character_face_library
