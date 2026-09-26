#include "bem.h"
#include "bem_rewrite.h"
#include "../../shared/third_party/nlohmann/json.hpp"
#include "../../shared/third_party/zstd/lib/zstd.h"
#include <algorithm>
#include <cmath>
#include <cstring>
#include <fstream>
#include <map>
#include <set>
#include <stdexcept>
#include <functional>
#include <optional>

namespace BetterEndfield::CustomModel {
namespace {
using J = nlohmann::json;
constexpr uint64_t MiB=1024*1024, Budget=512*MiB;
#pragma pack(push,1)
struct Header { char magic[8]; uint16_t major,minor; uint32_t size; uint64_t file,manifest; uint32_t count,flags; };
struct Entry { uint32_t codec,reserved; uint64_t offset,stored,decoded; };
#pragma pack(pop)
static_assert(sizeof(Header)==40 && sizeof(Entry)==32);
void Check(bool ok,const std::string& message) { if(!ok) throw std::runtime_error(message); }
// Per header minor. 1.0 has no option rules; its selected-state budget is 512 MiB.
struct Limits { size_t choices,rules,textures; uint64_t textureBytes,budget; uint32_t directory; };
Limits LimitsFor(uint16_t minor) {
    if(minor>=2) return {64,4096,kMaxBemTextures,kMaxBemTextureBytes,1536*MiB,16384};
    if(minor==1) return {16,512,32,64*MiB,768*MiB,4096};
    return {0,0,32,64*MiB,Budget,4096};
}
uint32_t U(const J& j) {
    Check(j.is_number_unsigned() || j.is_number_integer(),"Expected unsigned integer");
    auto n=j.get<int64_t>(); Check(n>=0 && n<=UINT32_MAX,"Integer out of range"); return static_cast<uint32_t>(n);
}
std::string S(const J& j,size_t limit=256) {
    auto s=j.get<std::string>(); Check(!s.empty() && s.size()<=limit && s.find('\0')==s.npos,"Invalid string"); return s;
}
std::string Id(const J& j) {
    auto s=S(j,96); Check(std::all_of(s.begin(),s.end(),[](unsigned char c){return
        (c>='A'&&c<='Z')||(c>='a'&&c<='z')||(c>='0'&&c<='9')||c=='_'||c=='-'||c=='.';}),"Invalid stable ID");
    Check(s[0]!='.' && s[0]!='-' && s[0]!='_',"Invalid stable ID start"); return s;
}
uint32_t Crc(std::string_view s) {
    uint32_t n=0xffffffff;
    for(auto b:s) { n^=static_cast<uint8_t>(b); for(int i=0;i<8;++i) n=(n>>1)^(0xedb88320u & (0u-(n&1))); }
    return n^0xffffffff;
}
using ChoiceTable=std::map<std::string,std::set<std::string>>;
void Condition(const J& node,const ChoiceTable& groups,const std::set<std::string>* earlier=nullptr,unsigned depth=0) {
    Check(depth<=16,"Condition exceeds depth 16");
    if(node.is_boolean()) return;
    Check(node.is_object() && node.size()==1,"Invalid condition node");
    auto item=node.begin(); const auto kind=item.key(); const auto& value=item.value();
    if(kind=="eq") {
        Check(value.is_array() && value.size()==2,"eq needs group and choice");
        auto group=Id(value[0]),choice=Id(value[1]);auto found=groups.find(group);
        Check(found!=groups.end() && found->second.contains(choice),"Condition references missing choice");
        if(earlier) Check(earlier->contains(group),"available_when must reference an earlier group");
    } else if(kind=="all"||kind=="any") {
        Check(value.is_array() && !value.empty() && value.size()<=32,"Invalid condition operands");
        for(const auto& child:value) Condition(child,groups,earlier,depth+1);
    } else if(kind=="not") Condition(value,groups,earlier,depth+1);
    else Check(false,"Unsupported condition");
}
bool Evaluate(const J& node,const std::map<std::string,std::string>& effective) {
    if(node.is_boolean()) return node.get<bool>();
    auto item=node.begin(); const auto& value=item.value();
    if(item.key()=="eq") {auto found=effective.find(value[0].get<std::string>());
        return found!=effective.end() && found->second==value[1].get<std::string>();}
    if(item.key()=="all") return std::all_of(value.begin(),value.end(),[&](const J& child){return Evaluate(child,effective);});
    if(item.key()=="any") return std::any_of(value.begin(),value.end(),[&](const J& child){return Evaluate(child,effective);});
    return !Evaluate(value,effective);
}
struct Container {
    uint16_t minor=0;
    Limits limits=LimitsFor(0);
    BemLoadStats* stats=nullptr;
    // BEM 1.2: target component -> bone index -> alias names.
    std::vector<std::map<uint32_t,std::vector<std::string>>> boneAliases;
    std::set<std::string> slotIds;
    J manifest;
    std::vector<Entry> directory;
    std::function<std::vector<uint8_t>(uint64_t,size_t)> read;
    BemPackageInfo info;
    void Open(uint64_t size) {
        Check(size>=sizeof(Header) && size<=2ull*1024*MiB,"Invalid BEM file size");
        auto raw=read(0,sizeof(Header)); Header h{}; std::memcpy(&h,raw.data(),sizeof(h));
        Check(std::memcmp(h.magic,"BEM\0PKG\0",8)==0 && h.major==1 && h.minor<=2 && h.size==sizeof(h) && !h.flags,
            "Only BEM 1.0/1.1/1.2 packages are supported");
        minor=h.minor; limits=LimitsFor(minor);
        Check(h.file==size && h.manifest>0 && h.manifest<=4*MiB && h.count<=limits.directory &&
            h.manifest+sizeof(h)+uint64_t(h.count)*sizeof(Entry)<=size,"Invalid BEM directory sizes");
        auto json=read(sizeof(h),static_cast<size_t>(h.manifest));
        // Reject duplicate keys, including nested objects, instead of accepting last-wins metadata.
        std::vector<std::set<std::string>> keys;
        auto callback=[&](int depth,nlohmann::json::parse_event_t event,J& value) {
            Check(depth<=48,"Manifest nesting exceeds 48");
            if(event==J::parse_event_t::object_start) keys.emplace_back();
            if(event==J::parse_event_t::key) Check(keys.back().insert(value.get<std::string>()).second,"Duplicate manifest key");
            if(event==J::parse_event_t::object_end) keys.pop_back();
            return true;
        };
        manifest=J::parse(json.begin(),json.end(),callback);
        auto table=read(sizeof(h)+h.manifest,h.count*sizeof(Entry)); directory.resize(h.count);
        if(!table.empty()) std::memcpy(directory.data(),table.data(),table.size());
        uint64_t end=sizeof(h)+h.manifest+table.size();
        for(const auto& e:directory) {
            Check(e.codec<=1 && !e.reserved && e.offset==end && e.stored>0 && e.stored<=Budget &&
                e.decoded>0 && e.decoded<=Budget && e.stored<=size-end,"Invalid payload extent/codec/budget");
            Check(e.codec || e.stored==e.decoded,"Raw payload size differs"); end+=e.stored;
        }
        Check(end==size,"Trailing or missing BEM bytes");
        Metadata();
    }
    // Fixed texture index, or {"slot":id} (BEM 1.2) resolved per selection.
    void TextureRefs(const J& refs,const std::string& message) const {
        std::set<uint32_t> fixed; std::set<std::string> named;
        for(const auto& ref:refs) {
            if(ref.is_object()) {Check(minor>=2 && ref.size()==1,message);
                auto slot=Id(ref.at("slot"));Check(slotIds.contains(slot) && named.insert(slot).second,message);}
            else Check(U(ref)<manifest.at("textures").size() && fixed.insert(U(ref)).second,message);
        }
    }
    void Metadata() {
        const auto& m=manifest;
        Check(U(m.at("schema"))==1,"Unknown manifest schema");
        info.package_id=Id(m.at("package_id")); info.name=S(m.at("name")); info.author=S(m.at("author"));
        info.version=S(m.at("version")); info.minor=minor;
        Check(m.contains("option_groups")==bool(minor),"BEM version/manifest mismatch");
        if(minor) Check(!m.contains("appearances") && !m.contains("default_appearance_id"),
            "BEM 1.1 must not enumerate fixed appearances");
        if(!minor) info.default_appearance=Id(m.at("default_appearance_id"));
        std::set<std::string> caps{"native-materials","palette-u8","indices-u32","fixed-appearances","texture-astc","composable-options","keep-material-textures"};
        if(minor>=2) caps.insert({"texture-slots","resource-bone-aliases"});
        bool composable=false,fixed=false,keepTextureCapability=false,slotCapability=false,aliasCapability=false;
        std::set<std::string> declared;
        for(const auto& c:m.at("required_capabilities")) {auto cap=S(c);Check(caps.contains(cap),"Unsupported required capability");
            Check(declared.insert(cap).second,"Duplicate required capability");
            composable|=cap=="composable-options";fixed|=cap=="fixed-appearances";
            keepTextureCapability|=cap=="keep-material-textures";
            slotCapability|=cap=="texture-slots";aliasCapability|=cap=="resource-bone-aliases";}
        Check(minor?composable&&!fixed:!composable,"BEM capability/version mismatch");
        const auto& t=m.at("target"); Check(S(t.at("platform"))=="windows-x64","Unsupported target platform");
        info.character_id=Id(t.at("character_id")); Id(t.at("profile_id")); Id(t.at("revision"));
        info.world_resource=Id(t.at("world_resource")); info.ui_resource=Id(t.at("ui_resource"));
        Check(info.world_resource!=info.ui_resource,"Duplicate resource roots");
        const auto& cs=t.at("components"); Check(cs.is_array() && !cs.empty() && cs.size()<=64,"Invalid target components");
        std::set<std::string> names; bool hasAliases=false; boneAliases.assign(cs.size(),{});
        for(size_t i=0;i<cs.size();++i) {
            const auto& c=cs[i]; auto name=S(c.at("mesh_name")); auto count=U(c.at("original_index_count"));
            Check(U(c.at("id"))==i && names.insert(name).second && count && count%3==0,"Invalid target identity");
            Check(c.at("bone_names").is_array() && c.at("bone_names").size()<=65536 &&
                c.at("materials").is_array() && c.at("materials").size()<=256,"Invalid target donor tables");
            if(c.contains("bone_name_aliases")) {
                const auto& aliases=c.at("bone_name_aliases"); Check(aliases.is_array(),"Invalid bone name aliases");
                std::set<std::pair<uint32_t,std::string>> seen;
                for(const auto& alias:aliases) {
                    Check(alias.is_object() && alias.size()==3,"Invalid bone name alias");
                    auto index=U(alias.at("index")); auto resource=S(alias.at("resource")); auto aliasName=S(alias.at("name"));
                    Check(index<c.at("bone_names").size() && (resource=="world"||resource=="ui") &&
                        seen.emplace(index,resource).second && c.at("bone_names").at(index)!=aliasName,"Invalid bone name alias");
                    boneAliases[i][index].push_back(aliasName); hasAliases=true;
                }
            }
            info.component_names.push_back(name); info.original_counts.push_back(count);
        }
        Check(aliasCapability==hasAliases,"Bone name alias capability mismatch");
        ChoiceTable optionChoices;size_t candidateCount=0;bool hasKeepTextures=false;
        if(!minor) {
            const auto& apps=m.at("appearances"); Check(apps.is_array() && !apps.empty() && apps.size()<=64,"Invalid appearances");
            std::set<std::string> ids;
            for(const auto& a:apps) {
                auto id=Id(a.at("id")); S(a.at("name")); Check(ids.insert(id).second,"Duplicate appearance ID");
                info.appearances.push_back(id);
                const auto& ops=a.at("components"); Check(ops.is_array() && ops.size()==cs.size(),"Incomplete appearance");
                for(size_t i=0;i<ops.size();++i) {
                    const auto& op=ops[i]; auto action=S(op.at("operation"));
                    Check(U(op.at("target"))==i && (action=="keep"||action=="hide"||action=="replace"),"Invalid component operation");
                    Check(!op.contains("material_overrides"),"Keep material overrides require BEM 1.1");
                    if(action=="replace") Check(U(op.at("mesh"))<m.at("meshes").size(),"Missing mesh reference");
                }
                if(a.contains("preview")) Check(U(a.at("preview"))<directory.size(),"Missing preview payload");
            }
            Check(ids.contains(info.default_appearance),"Default appearance missing");
        } else {
            const auto& groups=m.at("option_groups");Check(groups.is_array() && !groups.empty() && groups.size()<=64,"Invalid option groups");
            std::set<std::string> earlier;
            for(const auto& group:groups) {
                auto id=Id(group.at("id"));S(group.at("name"));
                Check(!optionChoices.contains(id),"Duplicate option group");
                const auto& items=group.at("choices");Check(items.is_array() && !items.empty() && items.size()<=limits.choices,"Invalid group choices");
                std::set<std::string> values;
                for(const auto& item:items) {auto choice=Id(item.at("id"));S(item.at("name"));
                    Check(values.insert(choice).second,"Duplicate choice ID");}
                Check(values.contains(Id(group.at("default"))),"Group default missing");optionChoices[id]=values;
                if(group.contains("available_when")) Condition(group.at("available_when"),optionChoices,&earlier);
                earlier.insert(id);
                if(!info.default_options.empty()) info.default_options+='&';
                info.default_options+=id+":"+group.at("default").get<std::string>();
            }
            info.option_groups_json=groups.dump();
            if(m.contains("selection_constraints")) {Check(m.at("selection_constraints").is_array(),"Invalid constraints");
                for(const auto& condition:m.at("selection_constraints")) Condition(condition,optionChoices);}
            info.selection_constraints_json=m.value("selection_constraints",J::array()).dump();
            if(m.contains("texture_slots")) {
                const auto& slots=m.at("texture_slots");Check(slots.is_array() && slots.size()<=1024,"Invalid texture slots");
                for(const auto& slot:slots) {
                    Check(slot.is_object() && slot.size()==2,"Invalid texture slot");
                    Check(slotIds.insert(Id(slot.at("id"))).second,"Duplicate texture slot");
                    const auto& candidates=slot.at("candidates");
                    Check(candidates.is_array() && !candidates.empty() && candidates.size()<=limits.rules,"Invalid texture slot candidates");
                    std::set<std::string> originals;
                    for(const auto& candidate:candidates) {
                        ++candidateCount; Check(candidate.is_object() && candidate.contains("texture") &&
                            candidate.size()==(candidate.contains("when")?2u:1u),"Invalid texture slot candidate");
                        const auto& texture=candidate.at("texture");
                        if(!texture.is_null()) {Check(U(texture)<m.at("textures").size(),"Missing texture reference");
                            originals.insert(S(m.at("textures").at(U(texture)).at("original_name")));}
                        if(candidate.contains("when")) Condition(candidate.at("when"),optionChoices);
                    }
                    Check(originals.size()==1,"Texture slot candidates must replace one original texture");
                }
            }
            Check(slotCapability==!slotIds.empty(),"Texture slot capability mismatch");
            const auto& rules=m.at("component_rules");Check(rules.is_array() && rules.size()==cs.size(),"Incomplete component rules");
            for(size_t i=0;i<rules.size();++i) {
                const auto& rule=rules[i];Check(U(rule.at("target"))==i,"Invalid component rule target");
                const auto& candidates=rule.at("candidates");Check(candidates.is_array() && !candidates.empty() && candidates.size()<=limits.rules,"Invalid candidates");
                for(const auto& candidate:candidates) {
                    ++candidateCount;auto action=S(candidate.at("operation"));
                    Check(action=="keep"||action=="hide"||action=="replace","Invalid component operation");
                    Check(action=="keep"||!candidate.contains("material_overrides"),"Only keep may override original materials");
                    if(action=="replace") Check(U(candidate.at("mesh"))<m.at("meshes").size(),"Missing mesh reference");
                    if(candidate.contains("material_overrides")) {
                        hasKeepTextures=true;
                        const auto& overrides=candidate.at("material_overrides");
                        Check(overrides.is_array()&&!overrides.empty()&&overrides.size()<=256,"Invalid keep material overrides");
                        std::set<uint32_t> slots;
                        for(const auto& override:overrides) {
                            auto slot=U(override.at("material_slot"));
                            const auto& materials=cs.at(i).at("materials");
                            Check(slot<materials.size()&&slots.insert(slot).second&&
                                materials.at(slot)==override.at("material_name"),"Keep material identity differs from target");
                            const auto& refs=override.at("textures");
                            Check(refs.is_array()&&!refs.empty()&&refs.size()<=limits.textures,"Invalid keep material textures");
                            TextureRefs(refs,"Invalid keep material texture reference");
                        }
                    }
                    if(candidate.contains("when")) Condition(candidate.at("when"),optionChoices);
                }
            }
        }
        Check(keepTextureCapability==hasKeepTextures,"Keep material override capability mismatch");
        Check(m.at("meshes").is_array() && m.at("meshes").size()<=4096 &&
            m.at("textures").is_array() && m.at("textures").size()<=4096,"Invalid resource tables");
        for(const auto& mesh:m.at("meshes")) {
            Check(mesh.at("streams").is_array() && mesh.at("streams").size()==3,"Three streams required");
            for(const auto& s:mesh.at("streams")) Check(U(s.at("payload"))<directory.size(),"Missing stream payload");
            if(!minor) Check(U(mesh.at("indices"))<directory.size(),"Missing index payload");
            const auto& draws=mesh.at("draws");
            if(minor) Check(draws.is_array() && !draws.empty() && draws.size()<=limits.rules,"Invalid draw candidates");
            for(const auto& d:draws) {
                if(minor) {Check(U(d.at("indices"))<directory.size(),"Missing draw index payload");
                    Check(U(d.at("count"))>0 && U(d.at("count"))%3==0,"Invalid draw index count");}
                if(minor) {++candidateCount;if(d.contains("when")) Condition(d.at("when"),optionChoices);}
                Check(d.at("textures").is_array(),"Invalid draw textures");
                TextureRefs(d.at("textures"),"Missing texture reference");
            }
        }
        if(minor) Check(candidateCount<=limits.rules,"More than "+std::to_string(limits.rules)+" candidate rules/draws");
        for(const auto& texture:m.at("textures")) Check(U(texture.at("payload"))<directory.size(),"Missing texture payload");
    }
    uint64_t decoded=0;
    std::map<uint32_t,std::vector<uint8_t>> cache;
    const std::vector<uint8_t>& Payload(const J& reference,uint64_t expected) {
        auto id=U(reference); Check(id<directory.size(),"Missing payload"); const auto& e=directory[id];
        Check(e.decoded==expected,"Payload decoded size differs from resource description");
        auto it=cache.find(id); if(it!=cache.end()) return it->second;
        const uint64_t decodedBudget=limits.budget;
        Check(decoded<=decodedBudget-e.decoded,"Selected appearance exceeds decoded payload budget"); decoded+=e.decoded;
        if(stats) stats->payload_ids.push_back(id);
        auto bytes=read(e.offset,static_cast<size_t>(e.stored));
        if(e.codec) {
            Check(ZSTD_findFrameCompressedSize(bytes.data(),bytes.size())==bytes.size() &&
                ZSTD_getFrameContentSize(bytes.data(),bytes.size())==e.decoded,"Zstd frame extent/content size differs");
            std::vector<uint8_t> result(static_cast<size_t>(e.decoded));
            auto n=ZSTD_decompress(result.data(),result.size(),bytes.data(),bytes.size());
            Check(!ZSTD_isError(n) && n==result.size(),"Invalid Zstd payload"); bytes=std::move(result);
        }
        return cache.emplace(id,std::move(bytes)).first->second;
    }
    struct Selection { J operations;std::map<std::string,std::string> effective; };
    Selection Select(std::string_view requested) const {
        Selection result;
        if(!minor) {
            const auto choice=requested.empty()?info.default_appearance:std::string(requested);
            for(const auto& appearance:manifest.at("appearances")) if(appearance.at("id")==choice) {
                result.operations=appearance.at("components");return result;
            }
            Check(false,"Selected appearance missing; select an available appearance in Mod manager");
        }
        std::map<std::string,std::string> saved;
        while(!requested.empty()) {
            auto end=requested.find('&');auto token=requested.substr(0,end);auto colon=token.find(':');
            Check(colon!=token.npos && colon>0 && colon+1<token.size() && token.find(':',colon+1)==token.npos,
                "Invalid option selection");
            auto group=Id(J(std::string(token.substr(0,colon))));auto choice=Id(J(std::string(token.substr(colon+1))));
            Check(saved.emplace(group,choice).second,"Duplicate option group selection");
            requested=end==requested.npos?std::string_view{}:requested.substr(end+1);
            Check(end==requested.npos || !requested.empty(),"Trailing option separator");
        }
        std::set<std::string> known;
        for(const auto& group:manifest.at("option_groups")) {
            auto id=group.at("id").get<std::string>();known.insert(id);
            auto choice=saved.contains(id)?saved.at(id):group.at("default").get<std::string>();
            bool valid=false;for(const auto& item:group.at("choices")) valid|=item.at("id")==choice;
            Check(valid,"Unknown option choice");
            if(!group.contains("available_when") || Evaluate(group.at("available_when"),result.effective))
                result.effective.emplace(id,choice);
        }
        for(const auto& [group,_]:saved) Check(known.contains(group),"Unknown option group");
        if(manifest.contains("selection_constraints")) for(const auto& rule:manifest.at("selection_constraints"))
            Check(Evaluate(rule,result.effective),"Unreachable option combination");
        result.operations=J::array();
        for(const auto& rule:manifest.at("component_rules")) {
            const J* selected=nullptr;
            for(const auto& candidate:rule.at("candidates"))
                if(!candidate.contains("when") || Evaluate(candidate.at("when"),result.effective)) {
                    Check(!selected,"Target has multiple selected operations");selected=&candidate;
                }
            Check(selected,"Target has no selected operation");
            auto operation=*selected;operation["target"]=rule.at("target");
            result.operations.push_back(std::move(operation));
        }
        return result;
    }
    void Decode(std::string_view requested,BemPocData& out) {
        const auto& m=manifest;auto selection=Select(requested);
        out.header.version=1;
        out.header.component_count=static_cast<uint32_t>(info.component_names.size());
        std::map<uint32_t,uint32_t> textures;
        std::map<std::string,std::optional<uint32_t>> slots;
        if(m.contains("texture_slots")) for(const auto& slot:m.at("texture_slots")) {
            const J* selected=nullptr;
            for(const auto& candidate:slot.at("candidates"))
                if(!candidate.contains("when") || Evaluate(candidate.at("when"),selection.effective)) {
                    Check(!selected,"Texture slot has multiple selected candidates");selected=&candidate;
                }
            Check(selected,"Texture slot has no selected candidate");
            const auto& texture=selected->at("texture");
            slots[slot.at("id").get<std::string>()]=texture.is_null()?std::nullopt:std::optional<uint32_t>(U(texture));
        }
        uint64_t resident=0;
        const uint64_t selectedBudget=limits.budget;
        auto reserve=[&](uint64_t bytes) { Check(bytes<=selectedBudget-resident,"Appearance exceeds runtime memory budget"); resident+=bytes; };
        auto selectTexture=[&](const J& ref)->uint32_t {
                    auto id=U(ref); auto found=textures.find(id); uint32_t tid;
                    if(found==textures.end()) {
                        Check(textures.size()<limits.textures,"Appearance exceeds "+std::to_string(limits.textures)+" texture bindings");
                        const auto& t=m.at("textures").at(id); BemTexture tex;
                        auto& ti=tex.info; ti.width=U(t.at("width")); ti.height=U(t.at("height")); ti.mip_count=U(t.at("mips"));
                        ti.create_format=int32_t(U(t.at("format"))); ti.create_srgb=t.at("srgb").get<bool>()?1:0;
                        uint32_t block=0,pixel=0,blockWidth=4; switch(ti.create_format) {case 4:pixel=4;break;case 63:pixel=1;Check(!ti.create_srgb,"R8 texture must be linear");break;case 10:case 26:block=8;break;case 12:case 25:case 27:block=16;break;case 48:block=16;break;case 49:block=16;blockWidth=5;break;case 50:block=16;blockWidth=6;break;default:Check(false,"Unsupported texture format");}
                        Check(ti.width && ti.height && ti.width<=32768 && ti.height<=32768 && (pixel || ti.create_format>=48 || (ti.width%4==0 && ti.height%4==0)) && ti.mip_count && ti.mip_count<=16,"Invalid texture dimensions");
                        uint64_t size=0; for(uint32_t level=0;level<ti.mip_count;++level) {
                            const uint64_t mipWidth=std::max(ti.width>>level,1u),mipHeight=std::max(ti.height>>level,1u);
                            size+=pixel?mipWidth*mipHeight*pixel:((mipWidth+blockWidth-1)/blockWidth)*((mipHeight+blockWidth-1)/blockWidth)*block;
                        }
                        Check(size<=limits.textureBytes,"Texture exceeds "+std::to_string(limits.textureBytes/MiB)+" MiB"); reserve(size); tex.data=Payload(t.at("payload"),size);
                        ti.data_size=static_cast<uint32_t>(size); ti.reserved=2; tex.original_name=S(t.at("original_name")); tex.name=tex.original_name;
                        ti.explicit_slot=static_cast<int32_t>(Crc(tex.original_name));
                        tid=static_cast<uint32_t>(out.textures.size()); textures[id]=tid; out.textures.push_back(std::move(tex));
                    } else tid=found->second;
            return tid;
        };
        // Selected texture ids of a draw/keep list; a null slot keeps the native texture.
        auto selectTextures=[&](const J& refs,const char* duplicate)->uint64_t {
            uint64_t mask=0;
            for(const auto& ref:refs) {
                std::optional<uint32_t> id;
                if(ref.is_object()) id=slots.at(ref.at("slot").get<std::string>()); else id=U(ref);
                if(!id) continue;
                auto tid=selectTexture(J(*id));
                Check(!(mask&(uint64_t{1}<<tid)),duplicate); mask|=uint64_t{1}<<tid;
            }
            return mask;
        };
        for(const auto& op:selection.operations) {
            BemComponent c; auto cid=U(op.at("target")); c.info.component_id=cid; c.info.original_index_count=info.original_counts[cid];
            const auto action=S(op.at("operation"));
            if(action!="replace") {
                c.info.flags=kComponentFlagNoGeometry|(action=="hide"?kComponentFlagHidden:0);
                if(action=="keep" && op.contains("material_overrides"))
                    for(const auto& override:op.at("material_overrides")) {
                        const auto mask=selectTextures(override.at("textures"),"Duplicate keep texture");
                        auto name=S(override.at("material_name"));
                        c.keep_material_overrides.push_back({U(override.at("material_slot")),Crc(name),mask});
                        c.keep_material_names.push_back(name);
                    }
                out.components.push_back(std::move(c)); continue;
            }
            const auto& mesh=m.at("meshes").at(U(op.at("mesh")));
            auto& h=c.info; h.vertex_count=U(mesh.at("vertex_count"));
            h.index_element_size=U(mesh.at("index_size")); h.stream_count=3;
            J chosenDraws=J::array();
            if(minor) {
                uint64_t count=0;
                for(const auto& draw:mesh.at("draws"))
                    if(!draw.contains("when") || Evaluate(draw.at("when"),selection.effective)) {
                        count+=U(draw.at("count"));Check(count<=16777216,"Selected index count exceeds limit");
                        chosenDraws.push_back(draw);
                    }
                Check(!chosenDraws.empty() && chosenDraws.size()<=256,"Selected draw limit exceeded");
                h.index_count=static_cast<uint32_t>(count);
            } else {h.index_count=U(mesh.at("index_count"));chosenDraws=mesh.at("draws");}
            Check(h.vertex_count && h.vertex_count<=1048576 && h.index_count && h.index_count<=16777216 && h.index_count%3==0 &&
                (h.index_element_size==2||h.index_element_size==4),"Invalid geometry counts/index type");
            std::array<uint32_t,3> strides{};
            for(size_t s=0;s<3;++s) {
                const auto& stream=mesh.at("streams")[s]; strides[s]=U(stream.at("stride"));
                Check(strides[s] && strides[s]<=64,"Invalid stream stride");
                const auto size=uint64_t(h.vertex_count)*strides[s]; reserve(size); c.streams[s]=Payload(stream.at("payload"),size);
            }
            h.stride0=strides[0]; h.stride1=strides[1]; h.stride2=strides[2];
            Check(h.stride2==4||h.stride2==12||(minor>=2&&h.stride2==32),"Unsupported skin layout");
            const auto& attrs=mesh.at("attributes"); Check(attrs.is_array() && !attrs.empty() && attrs.size()<=16,"Invalid attributes");
            std::array<uint32_t,3> offsets{}; std::set<uint32_t> semantics;
            constexpr uint32_t sizes[]{4,2,1,1,2,2,1,1,2,2,4,4};
            for(const auto& a:attrs) {
                Check(a.is_array() && a.size()==5,"Attribute needs semantic/format/dimension/stream/offset");
                auto sem=U(a[0]),fmt=U(a[1]),dim=U(a[2]),stream=U(a[3]),off=U(a[4]);
                Check(sem<=13 && semantics.insert(sem).second && fmt<12 && dim>=1 && dim<=4 && stream<3,"Invalid vertex attribute");
                Check(off==offsets[stream],"Invalid vertex attribute offset"); offsets[stream]+=sizes[fmt]*dim;
                c.attributes.push_back({int32_t(sem),int32_t(fmt),int32_t(dim),int32_t(stream)});
            }
            // Stride 32 (float weights, UInt32 indices) is BEM 1.2's uncompressed native skin layout.
            const bool skin32=h.stride2==32;
            Check(offsets==strides && c.attributes.back()==std::array<int32_t,4>{13,skin32?10:6,4,2},"Declaration/stride or skin indices differ");
            std::vector<std::array<int32_t,4>> skinAttributes;
            for(const auto& a:c.attributes) if(a[3]==2) skinAttributes.push_back(a);
            const std::vector<std::array<int32_t,4>> expectedSkin=h.stride2==4?
                std::vector<std::array<int32_t,4>>{{13,6,4,2}}:skin32?
                std::vector<std::array<int32_t,4>>{{12,0,4,2},{13,10,4,2}}:
                std::vector<std::array<int32_t,4>>{{12,4,4,2},{13,6,4,2}};
            Check(skinAttributes==expectedSkin,"Unsupported skin declaration");
            c.layout_crc=Crc(std::string_view(reinterpret_cast<const char*>(c.attributes.data()),c.attributes.size()*16));
            auto indexBytes=uint64_t(h.index_count)*h.index_element_size; reserve(indexBytes);
            if(minor) {
                c.indices.reserve(static_cast<size_t>(indexBytes));
                for(const auto& draw:chosenDraws) {
                    const auto& bytes=Payload(draw.at("indices"),uint64_t(U(draw.at("count")))*h.index_element_size);
                    c.indices.insert(c.indices.end(),bytes.begin(),bytes.end());
                }
                Check(c.indices.size()==indexBytes,"Selected index buffer differs");
            } else c.indices=Payload(mesh.at("indices"),indexBytes);
            for(size_t n=0;n<h.index_count;++n) { uint32_t index=0; std::memcpy(&index,c.indices.data()+n*h.index_element_size,h.index_element_size); Check(index<h.vertex_count,"Index outside vertex buffer"); }
            const auto& bones=mesh.at("bones"); Check(bones.is_array() && !bones.empty() && bones.size()<=256,"Invalid palette");
            for(const auto& b:bones) {
                auto donor=U(b.at("component")), index=U(b.at("index")); auto name=S(b.at("name"));
                Check(donor<info.component_names.size() && index<65536 && m.at("target").at("components").at(donor).at("bone_names").at(index)==name,"Bone identity differs");
                c.bones.push_back({donor,index,Crc(name)}); c.bone_names.push_back(name);
                const auto& aliases=boneAliases.at(donor); auto alias=aliases.find(index);
                c.bone_aliases.push_back(alias==aliases.end()?std::vector<std::string>{}:alias->second);
            }
            for(uint32_t n=0;n<h.vertex_count;++n) for(uint32_t k=0;k<4;++k) {
                uint32_t b=0;
                if(h.stride2==32) std::memcpy(&b,c.streams[2].data()+size_t(n)*32+16+k*4,4);
                else b=c.streams[2][size_t(n)*h.stride2+h.stride2-4+k];
                Check(b<c.bones.size(),"Skin index outside palette"); h.max_bone=std::max(h.max_bone,b);
            }
            if(h.stride2==32) for(uint32_t n=0;n<h.vertex_count;++n) {
                float w[4]; std::memcpy(w,c.streams[2].data()+size_t(n)*32,16);
                const float sum=w[0]+w[1]+w[2]+w[3];
                Check(std::isfinite(sum) && sum>=0.99f && sum<=1.01f && w[0]>=0 && w[1]>=0 && w[2]>=0 && w[3]>=0,"Invalid skin weight sum");
            }
            if(h.stride2==12) for(uint32_t n=0;n<h.vertex_count;++n) {
                uint16_t w[4]; std::memcpy(w,c.streams[2].data()+size_t(n)*12,8);
                const uint32_t sum=uint32_t(w[0])+w[1]+w[2]+w[3];
                Check(sum>=64880 && sum<=66190,"Invalid skin weight sum");
            }
            const auto& draws=chosenDraws; Check(draws.is_array() && !draws.empty() && draws.size()<=256,"Invalid draws");
            uint64_t end=0;
            for(const auto& d:draws) {
                auto start=minor?static_cast<uint32_t>(end):U(d.at("start"));
                auto count=U(d.at("count")),donor=U(d.at("material_component")),slot=U(d.at("material_slot")); auto name=S(d.at("material_name"));
                Check(start==end && count && count%3==0 && donor<info.component_names.size() && slot<256 &&
                    m.at("target").at("components").at(donor).at("materials").at(slot)==name,"Invalid material draw"); end+=count;
                const auto mask=selectTextures(d.at("textures"),"Duplicate draw texture");
                c.draws.push_back({start,count,donor,slot,Crc(name),0,mask}); c.material_names.push_back(name);
            }
            Check(end==h.index_count,"Draws must partition IB"); h.reserved0=static_cast<uint32_t>(c.bones.size()); h.reserved1=static_cast<uint32_t>(c.draws.size());
            out.components.push_back(std::move(c));
        }
        out.header.texture_count=static_cast<uint32_t>(out.textures.size());
    }
};
template<class F> bool File(const std::filesystem::path& path,std::string& error,F action) {
    error.clear(); try {
        std::ifstream in(path,std::ios::binary|std::ios::ate); Check(bool(in),"BEM package cannot be opened");
        auto size=in.tellg(); Check(size>=0,"BEM size unavailable"); Container c;
        c.read=[&](uint64_t off,size_t count) { std::vector<uint8_t> bytes(count); in.seekg(static_cast<std::streamoff>(off));
            Check(bool(in.read(reinterpret_cast<char*>(bytes.data()),static_cast<std::streamsize>(count))),"BEM read failed"); return bytes; };
        c.Open(static_cast<uint64_t>(size)); action(c); return true;
    } catch(const std::exception& e) { error=e.what(); return false; }
}
}
bool ReadBemPackageInfo(const std::filesystem::path& path,BemPackageInfo& out,std::string& error) {
    out={}; return File(path,error,[&](Container& c){out=c.info;});
}
bool LoadBem(const std::filesystem::path& path,BemPocData& out,std::string& error,std::string_view appearance,BemLoadStats* stats) {
    out={}; if(stats) stats->payload_ids.clear();BemPocData parsed;
    if(!File(path,error,[&](Container& c){c.stats=stats;c.Decode(appearance,parsed);})) return false;
    out=std::move(parsed); return true;
}
bool ParseBem(std::span<const uint8_t> bytes,BemPocData& out,std::string& error) {
    out={}; error.clear(); try {
        Container c; c.read=[&](uint64_t off,size_t n) { Check(off<=bytes.size() && n<=bytes.size()-off,"Truncated BEM");
            return std::vector<uint8_t>(bytes.begin()+off,bytes.begin()+off+n); };
        c.Open(bytes.size()); BemPocData parsed; c.Decode({},parsed); out=std::move(parsed); return true;
    } catch(const std::exception& e) {error=e.what(); return false;}
}
bool RewriteBemTextures(const std::filesystem::path& input,const std::filesystem::path& output,
    const TextureTransform& transform,const std::function<void()>& checkpoint,std::string& report,std::string& error) {
    // Caller owns a unique staging directory. Never overwrite a published package.
    if (std::filesystem::exists(output)) { error="Output already exists"; return false; }
    auto spool=output; spool += ".payloads";
    if (std::filesystem::exists(spool)) { error="Staging payload file already exists"; return false; }
    bool ok=File(input,error,[&](Container& c) {
        std::vector<std::string> validationSelections=c.info.appearances;
        if(c.minor) validationSelections.push_back(c.info.default_options);
        for(const auto& appearance:validationSelections) {
            checkpoint(); BemPocData parsed; c.cache.clear(); c.decoded=0;
            c.Decode(appearance,parsed);
        }
        c.cache.clear(); c.decoded=0;
        std::ofstream data(spool,std::ios::binary); Check(bool(data),"Cannot create staging payloads");
        std::vector<Entry> entries;
        auto append=[&](uint32_t codec,uint64_t decoded,const std::vector<uint8_t>& bytes) {
            Check(entries.size()<c.limits.directory,"Installed payload directory exceeds limit");
            auto id=entries.size(); entries.push_back({codec,0,0,bytes.size(),decoded});
            data.write(reinterpret_cast<const char*>(bytes.data()),bytes.size()); Check(bool(data),"Staging disk write failed");
            return id;
        };
        std::map<uint32_t,size_t> copied;
        auto copy=[&](BemJson& ref) {
            auto old=U(ref); auto it=copied.find(old);
            if(it==copied.end()) { checkpoint(); const auto& e=c.directory.at(old);
                it=copied.emplace(old,append(e.codec,e.decoded,c.read(e.offset,static_cast<size_t>(e.stored)))).first; }
            ref=it->second;
        };
        auto manifest=c.manifest;
        for(auto& mesh:manifest["meshes"]) {
            for(auto& stream:mesh["streams"]) copy(stream["payload"]);
            if(c.minor) for(auto& draw:mesh["draws"]) copy(draw["indices"]);
            else copy(mesh["indices"]);
        }
        if(!c.minor) for(auto& appearance:manifest["appearances"]) if(appearance.contains("preview")) copy(appearance["preview"]);
        BemJson changes=BemJson::array();
        // Identical texture descriptors share converted output; geometry/texture aliases are kept separate.
        std::map<std::string,BemJson> converted;
        for(auto& texture:manifest["textures"]) {
            checkpoint(); auto key=texture.dump(); auto found=converted.find(key);
            if(found!=converted.end()) { texture=found->second; continue; }
            auto original=texture; auto id=U(texture.at("payload"));
            c.cache.clear(); c.decoded=0;
            auto bytes=c.Payload(id,c.directory.at(id).decoded); c.cache.clear();
            Check(bytes.size()<=c.limits.textureBytes,"Texture exceeds format limit");
            transform(c.manifest,texture,bytes);
            Check(!bytes.empty() && bytes.size()<=c.limits.textureBytes,"Converted texture exceeds format limit");
            texture["payload"]=append(0,bytes.size(),bytes);
            converted.emplace(key,texture);
            changes.push_back({{"name",texture.at("original_name")},{"source_format",original.at("format")},
                {"format",texture.at("format")},{"bytes",bytes.size()}});
        }
        data.close(); Check(bool(data),"Payload flush failed"); checkpoint();
        bool astc=false; for(const auto& t:manifest["textures"]) if(U(t["format"])>=48 && U(t["format"])<=50) astc=true;
        if(astc && std::find(manifest["required_capabilities"].begin(),manifest["required_capabilities"].end(),"texture-astc")==manifest["required_capabilities"].end())
            manifest["required_capabilities"].push_back("texture-astc");
        manifest["android_install"]={{"revision",1},{"source_platform",manifest["target"]["platform"]}};
        auto json=manifest.dump(); Check(json.size()<=4*MiB,"Installed manifest exceeds limit");
        uint64_t offset=sizeof(Header)+json.size()+entries.size()*sizeof(Entry);
        for(auto& e:entries) {e.offset=offset;offset+=e.stored;}
        Check(offset<=2ull*1024*MiB,"Installed package exceeds 2 GiB");
        Header header{}; std::memcpy(header.magic,"BEM\0PKG\0",8); header.major=1;header.minor=c.minor;header.size=sizeof(Header);
        header.file=offset;header.manifest=json.size();header.count=static_cast<uint32_t>(entries.size());
        std::ofstream out(output,std::ios::binary); Check(bool(out),"Cannot create installed package");
        out.write(reinterpret_cast<const char*>(&header),sizeof(header)); out.write(json.data(),json.size());
        out.write(reinterpret_cast<const char*>(entries.data()),entries.size()*sizeof(Entry));
        std::ifstream payloads(spool,std::ios::binary); std::array<char,65536> buffer{};
        while(payloads) {checkpoint();payloads.read(buffer.data(),buffer.size());out.write(buffer.data(),payloads.gcount());}
        Check(payloads.eof(),"Staging read failed"); out.close(); Check(bool(out),"Installed package flush failed");
        for(const auto& appearance:validationSelections) {checkpoint();BemPocData parsed;std::string failure;
            Check(LoadBem(output,parsed,failure,appearance),"Installed validation: "+failure);}
        BemJson summary={{"package_id",c.info.package_id},{"character_id",c.info.character_id},{"name",c.info.name},
            {"textures",changes},{"bytes",offset},{"revision",1},{"bem_minor",c.minor}};
        if(c.minor) {summary["default_options"]=c.info.default_options;
            summary["option_groups"]=manifest.at("option_groups");
            summary["selection_constraints"]=manifest.value("selection_constraints",J::array());}
        else {summary["default_appearance"]=c.info.default_appearance;summary["appearances"]=c.info.appearances;}
        report=summary.dump();
    });
    std::error_code ignored; std::filesystem::remove(spool,ignored);
    if(!ok) std::filesystem::remove(output,ignored);
    return ok;
}
}
