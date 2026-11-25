#pragma once
#include "third_party/nlohmann/json.hpp"
#include <algorithm>
#include <array>
#include <charconv>
#include <filesystem>
#include <fstream>
#include <vector>

namespace BetterEndfieldNext::Resources {
inline bool Version(const std::string& text,std::array<unsigned,4>& value) {
    size_t begin=0;
    for(size_t i=0;i<4;++i) {
        auto end=text.find('.',begin);if((i==3)!=(end==std::string::npos))return false;
        if(end==std::string::npos)end=text.size();
        const auto parsed=std::from_chars(text.data()+begin,text.data()+end,value[i]);
        if(parsed.ec!=std::errc{} || parsed.ptr!=text.data()+end)return false;begin=end+1;
    }return true;
}
inline std::filesystem::path CoreFile(const std::filesystem::path& moduleDirectory,const std::string& relative) {
    try {
        if(relative.empty() || relative.front()=='/' || relative.find('\\')!=relative.npos || relative.find(':')!=relative.npos)return {};
        const auto rel=std::filesystem::path(std::u8string(relative.begin(),relative.end()));for(const auto& part:rel)if(part==".." || part=="." || part.empty())return {};
        const auto resources=moduleDirectory.parent_path()/"resources";
        const auto directory=resources/"core/windows";
        if(!std::filesystem::is_directory(directory))return {};
        auto trim=[](std::string& s) {
            while(!s.empty() && (s.back()=='\r' || s.back()=='\n' || s.back()==' ' || s.back()=='\t'))s.pop_back();
        };
        bool hasBundled=false;std::string bundledVersion,bundledToken;
        if(std::filesystem::is_regular_file(directory/".bundled")) {
            hasBundled=true;
            std::ifstream marker(directory/".bundled");
            if(!std::getline(marker,bundledVersion))return {};
            trim(bundledVersion);
            std::array<unsigned,4> parsed{};
            if(!Version(bundledVersion,parsed))return {};
            if(std::getline(marker,bundledToken))trim(bundledToken);
        }
        std::string selected;
        if(std::filesystem::is_regular_file(resources/"state.json")) {
            try {
                std::ifstream input(resources/"state.json");const auto state=nlohmann::json::parse(input);
                if(hasBundled) {
                    const bool matched=bundledToken.empty()
                        ? state.value("bundled.core.windows",std::string{})==bundledVersion
                        : state.value("bundled.token.windows",std::string{})==bundledToken;
                    selected=matched?state.value("core.windows",bundledVersion):bundledVersion;
                } else {
                    selected=state.value("core.windows",std::string{});
                }
            } catch(const std::exception&) {
                if(hasBundled)selected=bundledVersion; else return {};
            }
        } else if(hasBundled) {
            selected=bundledVersion;
        }
        std::vector<std::pair<std::array<unsigned,4>,std::filesystem::path>> candidates;
        for(const auto& entry:std::filesystem::directory_iterator(directory)) {
            std::array<unsigned,4> version{};const auto name=entry.path().filename().string();
            if(entry.is_directory() && Version(name,version) && (selected.empty() || selected==name))candidates.emplace_back(version,entry.path());
        }
        std::sort(candidates.rbegin(),candidates.rend());
        for(const auto& candidate:candidates) {
            std::ifstream input(candidate.second/"manifest.json");if(!input)continue;
            const auto m=nlohmann::json::parse(input);
            if(m.at("schemaVersion")!=1 || m.at("kind")!="betterendfield-resource-pack" || m.at("packageType")!="core" ||
                m.at("platform")!="windows" || m.at("version")!=candidate.second.filename().string())continue;
            if(!m.at("files").contains(relative))return {};
            const auto root=std::filesystem::canonical(candidate.second),path=std::filesystem::canonical(root/rel);
            const auto mismatch=std::mismatch(root.begin(),root.end(),path.begin(),path.end());
            if(mismatch.first!=root.end() || mismatch.second==path.end())return {};
            if(std::filesystem::file_size(path)!=m.at("files").at(relative).get<uint64_t>())return {};
            return path;
        }
    }catch(const std::exception&){}return {};
}
}
