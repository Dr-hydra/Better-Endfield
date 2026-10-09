#pragma once
// Lossless key edits. Kept platform-neutral for Android and offline tests.
#include <algorithm>
#include <map>
#include <stdexcept>
#include <string>
#include <string_view>
#include <vector>

namespace BetterEndfieldNext::CustomModel::Settings {
inline constexpr size_t kMaxIniBytes=1024*1024;
inline std::string Trim(std::string_view value) {
    const auto first=value.find_first_not_of(" \t\r\n");
    return first==value.npos?std::string{}:std::string(value.substr(first,value.find_last_not_of(" \t\r\n")-first+1));
}
class Ini {
public:
    using Values=std::map<std::string,std::string>;
    using Sections=std::map<std::string,Values>;
    explicit Ini(std::string_view text={}) {
        if(text.size()>kMaxIniBytes) throw std::runtime_error("Runtime configuration exceeds 1 MiB");
        if(text.starts_with("\xef\xbb\xbf")) text.remove_prefix(3);
        while(!text.empty()) {
            const auto end=text.find('\n');
            lines_.emplace_back(text.substr(0,end));
            text=end==text.npos?std::string_view{}:text.substr(end+1);
        }
        Parse();
    }
    const Sections& All() const {return sections_;}
    std::string Get(const std::string& section,const std::string& key,std::string fallback={}) const {
        const auto s=sections_.find(section); if(s==sections_.end()) return fallback;
        const auto k=s->second.find(key); return k==s->second.end()?fallback:k->second;
    }
    bool Flag(const std::string& section,const std::string& key,bool fallback=false) const {
        const auto v=Get(section,key); if(v.empty()) return fallback;
        if(v=="true"||v=="1") return true;
        if(v=="false"||v=="0") return false;
        throw std::runtime_error("Invalid boolean: "+section+"."+key);
    }
    void Set(const std::string& section,const std::string& key,const std::string& value) {
        if(section.empty()||key.empty()||section.find_first_of("\r\n[]")!=section.npos||
            key.find_first_of("\r\n=")!=key.npos||value.find_first_of("\r\n\0",0,3)!=value.npos)
            throw std::runtime_error("Invalid INI edit");
        const auto s=indices_.find(section);
        if(s!=indices_.end()) {
            const auto k=s->second.find(key);
            if(k!=s->second.end()) {
                auto& line=lines_[k->second];
                const bool cr=line.ends_with('\r'); line=key+"="+value+(cr?"\r":""); Parse(); return;
            }
        }
        size_t insert=lines_.size(); bool found=false;
        for(size_t i=0;i<lines_.size();++i) {
            const auto line=Trim(lines_[i]);
            if(line.starts_with('[')&&line.ends_with(']')) {
                if(found) {insert=i; break;}
                if(line.substr(1,line.size()-2)==section) found=true;
            }
        }
        if(!found) {lines_.push_back("["+section+"]"); insert=lines_.size();}
        lines_.insert(lines_.begin()+insert,key+"="+value); Parse();
    }
    std::string Text() const {
        std::string text; for(const auto& line:lines_) {text+=line; text+='\n';}
        if(text.size()>kMaxIniBytes) throw std::runtime_error("Runtime configuration exceeds 1 MiB");
        return text;
    }
private:
    void Parse() {
        sections_.clear(); indices_.clear(); std::string section;
        for(size_t i=0;i<lines_.size();++i) {
            const auto line=Trim(lines_[i]);
            if(line.empty()||line[0]=='#'||line[0]==';') continue;
            if(line.starts_with('[')&&line.ends_with(']')) {
                section=line.substr(1,line.size()-2); sections_.try_emplace(section); continue;
            }
            const auto equal=line.find('=');
            if(section.empty()||equal==line.npos) throw std::runtime_error("Malformed runtime configuration");
            const auto key=Trim(std::string_view(line).substr(0,equal));
            if(key.empty()||!sections_[section].emplace(key,Trim(std::string_view(line).substr(equal+1))).second)
                throw std::runtime_error("Duplicate runtime key: "+section+"."+key);
            indices_[section][key]=i;
        }
    }
    std::vector<std::string> lines_;
    Sections sections_;
    std::map<std::string,std::map<std::string,size_t>> indices_;
};
// Patch one control into a complete canonical selection, retaining other controls.
inline std::string PatchPairs(std::string_view source,std::string_view id,std::string_view value) {
    std::string result; bool found=false;
    while(!source.empty()) {
        const auto end=source.find('&'); const auto pair=source.substr(0,end); const auto colon=pair.find(':');
        if(!result.empty()) result+='&';
        if(pair.substr(0,colon)==id) {result+=std::string(id)+":"+std::string(value);found=true;}
        else result+=pair;
        source=end==source.npos?std::string_view{}:source.substr(end+1);
    }
    if(!found) {if(!result.empty()) result+='&';result+=std::string(id)+":"+std::string(value);}
    return result;
}
inline std::string PairValue(std::string_view source,std::string_view id) {
    while(!source.empty()) {
        const auto end=source.find('&');const auto pair=source.substr(0,end);const auto colon=pair.find(':');
        if(pair.substr(0,colon)==id&&colon!=pair.npos) return std::string(pair.substr(colon+1));
        source=end==source.npos?std::string_view{}:source.substr(end+1);
    }
    return {};
}
} // namespace BetterEndfieldNext::CustomModel::Settings
