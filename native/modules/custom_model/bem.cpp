#include "bem.h"
#include "bem_rewrite.h"
#include "../../shared/third_party/nlohmann/json.hpp"
#include "../../shared/third_party/zstd/lib/zstd.h"
#include "../../shared/third_party/miniz/miniz.h" 
#include <algorithm>
#include <cmath>
#include <cstring>
#include <fstream>
#include <map>
#include <set>
#include <stdexcept>
#include <functional>
#include <optional>
#include <charconv>

namespace BetterEndfieldNext::CustomModel {
struct BemSelectionMetadata {
    nlohmann::json manifest,parameters;
};
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
BemRootOffset ReadRootOffset(const J& manifest, bool skip_validation = false) {
    BemRootOffset result;
    const char* key = manifest.contains("root_offset") ? "root_offset" : (manifest.contains("visual_root_offset") ? "visual_root_offset" : nullptr);
    if (!key) return result;
    const auto& p = manifest.at(key);
    if (!p.is_object() || !p.contains("path")) {
        if (!skip_validation) throw std::runtime_error("Invalid root offset format");
        return result;
    }
    result.path = p.at("path").get<std::string>();
    if (!ValidVisualPath(result.path)) {
        if (!skip_validation) throw std::runtime_error("Invalid root offset path");
        return result;
    }
    float squared = 0.0f;
    if (p.contains("translation") && p.at("translation").is_array() && p.at("translation").size() == 3) {
        const auto& arr = p.at("translation");
        for (size_t i = 0; i < 3; ++i) {
            float f = arr[i].get<float>();
            if (!std::isfinite(f)) {
                if (!skip_validation) throw std::runtime_error("Nonfinite root translation");
                f = 0.0f;
            }
            result.translation[i] = f;
            squared += f * f;
        }
    } else if (p.contains("translation_bits") && p.at("translation_bits").is_array() && p.at("translation_bits").size() == 3) {
        const auto& arr = p.at("translation_bits");
        for (size_t i = 0; i < 3; ++i) {
            uint32_t bits = arr[i].get<uint32_t>();
            float f = 0.0f;
            std::memcpy(&f, &bits, sizeof(float));
            if (!std::isfinite(f)) {
                if (!skip_validation) throw std::runtime_error("Nonfinite root translation bits");
                f = 0.0f;
            }
            result.translation[i] = f;
            squared += f * f;
        }
    } else {
        if (!skip_validation) throw std::runtime_error("root_offset missing translation");
    }
    const float max_squared = skip_validation ? 1.0f : 0.040001f;
    if (squared > max_squared) {
        if (!skip_validation) throw std::runtime_error("Root offset translation exceeds maximum limit (0.2m)");
        float len = std::sqrt(squared);
        if (len > 1e-4f) {
            float scale = std::sqrt(max_squared) / len;
            result.translation[0] *= scale;
            result.translation[1] *= scale;
            result.translation[2] *= scale;
        }
    }
    return result;
}

uint32_t Crc(std::string_view s) {
    uint32_t n=0xffffffff;
    for(auto b:s) { n^=static_cast<uint8_t>(b); for(int i=0;i<8;++i) n=(n>>1)^(0xedb88320u & (0u-(n&1))); }
    return n^0xffffffff;
}
#include "bem_targets.inc"
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
using ParameterTicks=std::map<std::string,uint32_t>;
ParameterTicks ParseParameters(const J& definitions,std::string_view requested,std::string* canonical=nullptr) {
    ParameterTicks saved,result;
    while(!requested.empty()) {
        const auto end=requested.find('&'); const auto token=requested.substr(0,end); const auto colon=token.find(':');
        Check(colon!=token.npos && colon>0 && colon+1<token.size() && token.find(':',colon+1)==token.npos,"Invalid parameter selection");
        auto id=Id(J(std::string(token.substr(0,colon)))); const auto text=token.substr(colon+1);
        uint32_t tick=0; const auto parsed=std::from_chars(text.data(),text.data()+text.size(),tick);
        Check(parsed.ec==std::errc{} && parsed.ptr==text.data()+text.size(),"Parameter value must be an unsigned integer tick");
        Check(saved.emplace(id,tick).second,"Duplicate parameter selection");
        requested=end==requested.npos?std::string_view{}:requested.substr(end+1);
        Check(end==std::string_view::npos || !requested.empty(),"Trailing parameter separator");
    }
    if(canonical) canonical->clear();
    for(const auto& parameter:definitions) {
        const auto id=parameter.at("id").get<std::string>();
        const auto tick=saved.contains(id)?saved.at(id):U(parameter.at("default"));
        const auto min=U(parameter.at("min")),max=U(parameter.at("max")),step=U(parameter.at("step"));
        Check(tick>=min && tick<=max && (tick-min)%step==0,"Parameter tick is outside range or step");
        result.emplace(id,tick);
        if(canonical) { if(!canonical->empty()) *canonical+='&'; *canonical+=id+":"+std::to_string(tick); }
    }
    for(const auto& [id,_]:saved) Check(result.contains(id),"Unknown parameter");
    return result;
}
static size_t ZipReadFunc(void *pOpaque, mz_uint64 file_ofs, void *pBuf, size_t n) {
    auto* read_fn = static_cast<const std::function<std::vector<uint8_t>(uint64_t, size_t)>*>(pOpaque);
    if (!read_fn || !(*read_fn)) return 0;
    auto bytes = (*read_fn)(file_ofs, n);
    if (bytes.empty()) return 0;
    std::memcpy(pBuf, bytes.data(), bytes.size());
    return bytes.size();
}

struct Container {
    bool manifestOnly=false;
    bool skip_validation=false;
    bool loading_optimization=false;
    // Texture payloads are described, not read; their decoded bytes are
    // excluded from consumer counts and from the load-plan reservation.
    bool deferTextures=false;
    void Validate(bool ok,const std::string& message) const { if(!skip_validation) Check(ok,message); }
    uint16_t minor=0;
    Limits limits=LimitsFor(0);
    BemLoadStats* stats=nullptr;
    // BEM 1.2: target component -> bone index -> alias names.
    std::vector<std::map<uint32_t,std::vector<std::string>>> boneAliases;
    std::vector<std::map<uint32_t,std::array<std::vector<std::string>,2>>> boneAliasResources;
    std::set<std::string> slotIds;
    J manifest;
    std::vector<Entry> directory;
    std::function<std::vector<uint8_t>(uint64_t,size_t)> read;
    BemPackageInfo info;
    J parameters=J::array();
    bool is_zip = false;
    mz_zip_archive zip{};
    std::vector<int> zip_payload_file_indices;
    std::string zip_prefix;
    ~Container() {
        if (is_zip) {
            mz_zip_reader_end(&zip);
        }
    }
    void Open(uint64_t size) {
        Check(size >= 4, "Invalid BEM file size");
        // Read the whole header, not just the 8-byte signature: the version check below
        // inspects every field, and a short probe would copy uninitialised heap bytes.
        auto probe = read(0, std::min<uint64_t>(size, sizeof(Header)));
        Check(probe.size() >= 4, "Cannot read package signature");

        if (probe.size() >= 8 && std::memcmp(probe.data(), "BEM\0PKG\0", 8) == 0) {
            Check(size>=sizeof(Header),"Invalid BEM file size");
            Validate(size<=2ull*1024*MiB,"Invalid BEM file size");
            Header h{}; std::memcpy(&h,probe.data(),sizeof(h));
            Check(std::memcmp(h.magic,"BEM\0PKG\0",8)==0 && h.major==1 && h.minor<=5 && h.size==sizeof(h) && !h.flags,
                "Only BEM 1.0/1.1/1.2/1.3/1.4/1.5 packages are supported");
            minor=h.minor; limits=LimitsFor(minor);
            if(skip_validation) {
                limits.choices=SIZE_MAX; limits.rules=SIZE_MAX; limits.textures=kMaxBemTextures;
                limits.textureBytes=UINT32_MAX; limits.budget=UINT64_MAX; limits.directory=UINT32_MAX;
            }
            Check(h.file==size && h.manifest>0 && h.manifest<=size-sizeof(h) &&
                uint64_t(h.count)<=(size-sizeof(h)-h.manifest)/sizeof(Entry),"Invalid BEM directory sizes");
            Validate(h.manifest<=4*MiB && h.count<=limits.directory,"Invalid BEM directory sizes");
            if(manifestOnly) Check(h.count<=65536 && h.manifest<=16*MiB,"Management metadata exceeds limit");
            auto json=read(sizeof(h),static_cast<size_t>(h.manifest));
            std::vector<std::set<std::string>> keys;
            auto callback=[&](int depth,nlohmann::json::parse_event_t event,J& value) {
                Check(depth<=48,"Manifest nesting exceeds 48");
                if(event==J::parse_event_t::object_start) keys.emplace_back();
                if(event==J::parse_event_t::key) Check(keys.back().insert(value.get<std::string>()).second,"Duplicate manifest key");
                if(event==J::parse_event_t::object_end) keys.pop_back();
                return true;
            };
            manifest=J::parse(json.begin(),json.end(),callback);
            if(manifestOnly) {
                directory.resize(h.count);
                Metadata(); return;
            }
            auto table=read(sizeof(h)+h.manifest,h.count*sizeof(Entry)); directory.resize(h.count);
            if(!table.empty()) std::memcpy(directory.data(),table.data(),table.size());
            uint64_t end=sizeof(h)+h.manifest+table.size();
            for(const auto& e:directory) {
                Check(e.codec<=1 && !e.reserved && e.offset==end && e.stored>0 &&
                    e.decoded>0 && e.stored<=size-end,"Invalid payload extent/codec/budget");
                Validate(e.stored<=Budget && e.decoded<=Budget,"Invalid payload extent/codec/budget");
                Check(e.codec || e.stored==e.decoded,"Raw payload size differs"); end+=e.stored;
            }
            Check(end==size,"Trailing or missing BEM bytes");
            Metadata();
            return;
        }

        if (probe[0] == 'P' && probe[1] == 'K' && probe[2] == 3 && probe[3] == 4) {
            is_zip = true;
            std::memset(&zip, 0, sizeof(zip));
            zip.m_pRead = ZipReadFunc;
            zip.m_pIO_opaque = &this->read;
            Check(mz_zip_reader_init(&zip, size, 0) != 0, "Corrupt or invalid ZIP package");

            mz_uint num_files = mz_zip_reader_get_num_files(&zip);
            int manifest_file_index = -1;
            bool has_bem_files = false;
            for (mz_uint i = 0; i < num_files; ++i) {
                mz_zip_archive_file_stat stat;
                if (!mz_zip_reader_file_stat(&zip, i, &stat)) continue;
                std::string name(stat.m_filename);
                for (char& c : name) if (c == '\\') c = '/';
                if (name.size() >= 4 && (name.ends_with(".bem") || name.ends_with(".BEM"))) {
                    has_bem_files = true;
                }
                if (name == "manifest.json") {
                    manifest_file_index = static_cast<int>(i);
                    zip_prefix.clear();
                } else if (name.ends_with("/manifest.json") && manifest_file_index < 0) {
                    auto first_slash = name.find('/');
                    if (first_slash != std::string::npos && first_slash == name.rfind('/')) {
                        manifest_file_index = static_cast<int>(i);
                        zip_prefix = name.substr(0, first_slash + 1);
                    }
                }
            }
            Check(manifest_file_index >= 0, has_bem_files ? 
                "ZIP contains BEM bundle members, not a single BEM model manifest" : 
                "ZIP package is missing manifest.json");

            size_t manifest_size = 0;
            void* pManifest = mz_zip_reader_extract_to_heap(&zip, manifest_file_index, &manifest_size, 0);
            Check(pManifest != nullptr, "Failed to extract manifest.json from ZIP");
            std::string manifest_str(reinterpret_cast<const char*>(pManifest), manifest_size);
            mz_free(pManifest);

            manifest = J::parse(manifest_str);

            // A ZIP container carries no binary header, so the minor version must come from
            // the manifest: an explicit format_version wins, otherwise the presence of
            // version-specific fields decides, and declared capabilities can only raise it.
            // The default used to be 1, which rejected every BEM 1.0 package because 1.x
            // manifests are then required to carry option_groups.
            minor = 0;
            if (manifest.contains("format_version") && manifest["format_version"].is_string()) {
                std::string fv = manifest["format_version"].get<std::string>();
                if (fv.starts_with("1.")) {
                    try { minor = static_cast<uint16_t>(std::stoi(fv.substr(2))); } catch (...) {}
                }
            } else {
                if (manifest.contains("option_groups")) minor = std::max<uint16_t>(minor, 1);
                if (manifest.contains("texture_slots")) minor = std::max<uint16_t>(minor, 2);
                if (manifest.contains("parameters") || manifest.contains("mesh_deformations"))
                    minor = std::max<uint16_t>(minor, 3);
                if (manifest.contains("resources")) minor = std::max<uint16_t>(minor, 4);
                if (manifest.contains("root_offset") || manifest.contains("drivers"))
                    minor = std::max<uint16_t>(minor, 5);
            }
            if (manifest.contains("required_capabilities") && manifest["required_capabilities"].is_array()) {
                for (const auto& cap : manifest["required_capabilities"]) {
                    // Both the documented capability names and the early prototype spellings
                    // are honoured here; Metadata() keeps validating against the documented set.
                    if (cap == "material-overrides-v1" || cap == "root-offset-v1" || cap == "dynamic-drivers-v1")
                        minor = std::max<uint16_t>(minor, 5);
                    else if (cap == "multi-resource-targets" || cap == "static-meshes" ||
                             cap == "multi-resource-v1" || cap == "static-mesh-v1")
                        minor = std::max<uint16_t>(minor, 4);
                    else if (cap == "body-parameters" || cap == "mesh-position-deltas")
                        minor = std::max<uint16_t>(minor, 3);
                    else if (cap == "texture-slots" || cap == "resource-bone-aliases")
                        minor = std::max<uint16_t>(minor, 2);
                    else if (cap == "composable-options" || cap == "keep-material-textures")
                        minor = std::max<uint16_t>(minor, 1);
                }
            }
            limits = LimitsFor(minor);
            if (skip_validation) {
                limits.choices = SIZE_MAX; limits.rules = SIZE_MAX; limits.textures = kMaxBemTextures;
                limits.textureBytes = UINT32_MAX; limits.budget = UINT64_MAX; limits.directory = UINT32_MAX;
            }

            std::map<std::string, int> name_to_zip_idx;
            for (mz_uint i = 0; i < num_files; ++i) {
                mz_zip_archive_file_stat stat;
                if (!mz_zip_reader_file_stat(&zip, i, &stat)) continue;
                std::string name(stat.m_filename);
                for (char& c : name) if (c == '\\') c = '/';
                if (!zip_prefix.empty() && name.starts_with(zip_prefix)) {
                    name = name.substr(zip_prefix.size());
                }
                name_to_zip_idx[name] = static_cast<int>(i);
            }

            uint32_t max_payload_id = 0;
            bool found_any_payload = false;
            auto inspect_payload = [&](const J& ref) {
                if (ref.is_number_integer()) {
                    uint32_t pid = ref.get<uint32_t>();
                    max_payload_id = std::max(max_payload_id, pid);
                    found_any_payload = true;
                }
            };

            for (const auto& mesh : manifest.value("meshes", J::array())) {
                for (const auto& s : mesh.value("streams", J::array())) if (s.contains("payload")) inspect_payload(s["payload"]);
                if (mesh.contains("indices")) inspect_payload(mesh["indices"]);
                for (const auto& d : mesh.value("draws", J::array())) if (d.contains("indices")) inspect_payload(d["indices"]);
            }
            for (const auto& t : manifest.value("textures", J::array())) if (t.contains("payload")) inspect_payload(t["payload"]);
            for (const auto& ch : manifest.value("mesh_deformations", J::array())) {
                for (const auto& f : ch.value("frames", J::array())) if (f.contains("payload")) inspect_payload(f["payload"]);
            }
            for (const auto& app : manifest.value("appearances", J::array())) if (app.contains("preview")) inspect_payload(app["preview"]);
            // Section 13 driver sound effects reference their audio payload the same
            // way, so a ZIP container must register those payload ids too.
            for (const auto& driver : manifest.value("drivers", J::array())) {
                if (!driver.is_object()) continue;
                if (driver.contains("sound") && driver["sound"].is_object() &&
                    driver["sound"].contains("payload")) inspect_payload(driver["sound"]["payload"]);
            }

            size_t total_payloads = found_any_payload ? (max_payload_id + 1) : 0;
            directory.resize(total_payloads);
            zip_payload_file_indices.resize(total_payloads, -1);

            for (uint32_t pid = 0; pid < total_payloads; ++pid) {
                char buf[64];
                snprintf(buf, sizeof(buf), "payloads/%04u.bin", pid);
                std::string c1 = buf;
                snprintf(buf, sizeof(buf), "payloads/%u.bin", pid);
                std::string c2 = buf;
                snprintf(buf, sizeof(buf), "%u.bin", pid);
                std::string c3 = buf;

                int zip_idx = -1;
                if (auto it = name_to_zip_idx.find(c1); it != name_to_zip_idx.end()) zip_idx = it->second;
                else if (auto it = name_to_zip_idx.find(c2); it != name_to_zip_idx.end()) zip_idx = it->second;
                else if (auto it = name_to_zip_idx.find(c3); it != name_to_zip_idx.end()) zip_idx = it->second;

                if (zip_idx >= 0) {
                    mz_zip_archive_file_stat stat;
                    mz_zip_reader_file_stat(&zip, zip_idx, &stat);
                    Entry e{};
                    e.codec = 0;
                    e.reserved = 0;
                    e.offset = static_cast<uint64_t>(zip_idx);
                    e.stored = stat.m_comp_size;
                    e.decoded = stat.m_uncomp_size;
                    directory[pid] = e;
                    zip_payload_file_indices[pid] = zip_idx;
                }
            }

            Metadata();
            return;
        }

        Check(false, "Unrecognized package signature; expected BEM binary or ZIP package");
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
        info.manifest_json=m.dump();
        Check(U(m.at("schema"))==1,"Unknown manifest schema");
        info.package_id=Id(m.at("package_id")); info.name=S(m.at("name")); info.author=S(m.at("author"));
        info.version=S(m.at("version")); info.minor=minor;
        Check(m.contains("option_groups")==bool(minor),"BEM version/manifest mismatch");
        if(minor) Check(!m.contains("appearances") && !m.contains("default_appearance_id"),
            "BEM 1.1 must not enumerate fixed appearances");
        if(!minor) info.default_appearance=Id(m.at("default_appearance_id"));
        std::set<std::string> caps{"native-materials","palette-u8","indices-u32","fixed-appearances","texture-astc","composable-options","keep-material-textures"};
        if(minor>=2) caps.insert({"texture-slots","resource-bone-aliases"});
        if(minor>=3) caps.insert({"body-parameters","mesh-position-deltas"});
        if(minor>=4) caps.insert({"multi-resource-targets","static-meshes"});
        if(minor>=5) caps.insert({"material-overrides-v1", "root-offset-v1", "dynamic-drivers-v1"});
        bool composable=false,fixed=false,keepTextureCapability=false,slotCapability=false,aliasCapability=false;
        std::set<std::string> declared;
        for(const auto& c:m.at("required_capabilities")) {auto cap=S(c);Validate(caps.contains(cap),"Unsupported required capability");
            Validate(declared.insert(cap).second,"Duplicate required capability");
            composable|=cap=="composable-options";fixed|=cap=="fixed-appearances";
            keepTextureCapability|=cap=="keep-material-textures";
            slotCapability|=cap=="texture-slots";aliasCapability|=cap=="resource-bone-aliases";}
        Validate(minor?composable&&!fixed:!composable,"BEM capability/version mismatch");
        const auto& t=m.at("target"); Validate(S(t.at("platform"))=="windows-x64","Unsupported target platform");
        Id(t.at("profile_id")); Id(t.at("revision"));
        // The target structure, not the header minor version, picks the parsing
        // route: 1.5 extends 1.3, and the 1.4 explicit resource targets remain an
        // optional branch that only applies when the target actually carries them.
        const bool explicit_targets=t.contains("resources");
        if(explicit_targets) {
            Check(declared.contains("multi-resource-targets"),"Multi-resource capability is required");
            ReadResourceTargets(t,info);
            const bool hasStatic=std::any_of(info.component_static.begin(),info.component_static.end(),[](bool value){return value;});
            Check(!hasStatic || declared.contains("static-meshes"),"Static mesh capability is required");
        } else {
            Check(!t.contains("kind") && !t.contains("id"),"Mixed target structure");
            Check(!declared.contains("multi-resource-targets") && !declared.contains("static-meshes"),"Resource capability without explicit resource targets");
            info.character_id=Id(t.at("character_id")); info.target_id=info.character_id;
            info.world_resource=Id(t.at("world_resource")); info.ui_resource=Id(t.at("ui_resource"));
            Validate(info.world_resource!=info.ui_resource,"Duplicate resource roots");
            for(const char* platform:{"windows-x64","android-arm64"}) for(const auto* root:{&info.world_resource,&info.ui_resource})
                info.resource_keys.push_back(std::string(platform)+":"+*root);
        }
        const auto& cs=t.at("components"); Check(cs.is_array() && !cs.empty(),"Invalid target components");
        Validate(cs.size()<=64,"Invalid target components");
        std::set<std::string> names; bool hasAliases=false; boneAliases.assign(cs.size(),{}); boneAliasResources.assign(cs.size(),{});
        for(size_t i=0;i<cs.size();++i) {
            const auto& c=cs[i]; auto name=S(c.at("mesh_name")); auto count=U(c.at("original_index_count"));
            Check(U(c.at("id"))==i && count,"Invalid target identity");
            Validate(names.insert(explicit_targets?info.component_resources[i]+":"+name:name).second && count%3==0,"Invalid target identity");
            if(!explicit_targets) Check(!c.contains("renderer_kind") && !c.contains("resource") && !c.contains("renderer_path"),"Resource components require explicit resource targets");
            Check(c.at("bone_names").is_array() && c.at("materials").is_array(),"Invalid target donor tables");
            Validate(c.at("bone_names").size()<=65536 && c.at("materials").size()<=256,"Invalid target donor tables");
            if(c.contains("bone_name_aliases")) {
                const auto& aliases=c.at("bone_name_aliases"); Check(aliases.is_array(),"Invalid bone name aliases");
                std::set<std::pair<uint32_t,std::string>> seen;
                for(const auto& alias:aliases) {
                    Check(alias.is_object() && alias.size()==3,"Invalid bone name alias");
                    auto index=U(alias.at("index")); auto resource=S(alias.at("resource")); auto aliasName=S(alias.at("name"));
                    Check(index<c.at("bone_names").size() && (resource=="world"||resource=="ui") &&
                        seen.emplace(index,resource).second && c.at("bone_names").at(index)!=aliasName,"Invalid bone name alias");
                    boneAliases[i][index].push_back(aliasName);
                    boneAliasResources[i][index][resource=="world"?0:1].push_back(aliasName); hasAliases=true;
                }
            }
            info.component_names.push_back(name); info.original_counts.push_back(count);
        }
        // Supported requirements may be a superset of the features actually used.
        Validate(!hasAliases || aliasCapability,"Bone name alias capability mismatch");
        ChoiceTable optionChoices;size_t candidateCount=0;bool hasKeepTextures=false;
        if(!minor) {
            const auto& apps=m.at("appearances"); Check(apps.is_array() && !apps.empty(),"Invalid appearances"); Validate(apps.size()<=64,"Invalid appearances");
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
            const auto& groups=m.at("option_groups");Check(groups.is_array() && (!groups.empty() || minor>=3),"Invalid option groups"); Validate(groups.size()<=64,"Invalid option groups");
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
                const auto& slots=m.at("texture_slots");Check(slots.is_array(),"Invalid texture slots"); Validate(slots.size()<=1024,"Invalid texture slots");
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
            Validate(slotIds.empty() || slotCapability,"Texture slot capability mismatch");
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
                        Check(overrides.is_array()&&!overrides.empty(),"Invalid keep material overrides"); Validate(overrides.size()<=256,"Invalid keep material overrides");
                        std::set<uint32_t> slots;
                        for(const auto& override:overrides) {
                            auto slot=U(override.at("material_slot"));
                            const auto& materials=cs.at(i).at("materials");
                            Check(slot<materials.size(),"Keep material reference outside donor table");
                            Validate(slots.insert(slot).second&&materials.at(slot)==override.at("material_name"),"Keep material identity differs from target");
                            const auto& refs=override.at("textures");
                            Check(refs.is_array()&&!refs.empty()&&refs.size()<=limits.textures,"Invalid keep material textures");
                            TextureRefs(refs,"Invalid keep material texture reference");
                        }
                    }
                    if(candidate.contains("when")) Condition(candidate.at("when"),optionChoices);
                }
            }
        }
        Validate(!hasKeepTextures || keepTextureCapability,"Keep material override capability mismatch");
        Check(m.at("meshes").is_array() && m.at("textures").is_array(),"Invalid resource tables");
        Validate(m.at("meshes").size()<=4096 && m.at("textures").size()<=4096,"Invalid resource tables");
        if(explicit_targets) ValidateResourceMeshes(m,info);
        if(m.contains("root_offset")||m.contains("visual_root_offset")) ReadRootOffset(m,skip_validation);
        for(const auto& mesh:m.at("meshes")) {
            const bool isStatic=explicit_targets && RendererStatic(mesh);
            Check(explicit_targets || !mesh.contains("renderer_kind"),"Renderer kind requires explicit resource targets");
            Check(!isStatic || declared.contains("static-meshes"),"Static mesh capability is required");
            Check(mesh.at("streams").is_array() && (isStatic ? !mesh.at("streams").empty() && mesh.at("streams").size()<=3 : mesh.at("streams").size()==3),"Invalid stream count for renderer kind");
            for(const auto& s:mesh.at("streams")) Check(U(s.at("payload"))<directory.size(),"Missing stream payload");
            if(!minor) Check(U(mesh.at("indices"))<directory.size(),"Missing index payload");
            const auto& draws=mesh.at("draws");
            if(minor) Check(draws.is_array() && !draws.empty() && draws.size()<=limits.rules,"Invalid draw candidates");
            for(const auto& d:draws) {
                if(minor) {Check(U(d.at("indices"))<directory.size(),"Missing draw index payload");
                    Check(U(d.at("count"))>0,"Invalid draw index count"); Validate(U(d.at("count"))%3==0,"Invalid draw index count");}
                if(minor) {++candidateCount;if(d.contains("when")) Condition(d.at("when"),optionChoices);}
                Check(d.at("textures").is_array(),"Invalid draw textures");
                TextureRefs(d.at("textures"),"Missing texture reference");
            }
        }
        if(minor) Check(candidateCount<=limits.rules,"More than "+std::to_string(limits.rules)+" candidate rules/draws");
        for(const auto& texture:m.at("textures")) Check(U(texture.at("payload"))<directory.size(),"Missing texture payload");
        if(m.contains("parameters")) {
            Check(minor>=3 && m.at("parameters").is_array(),"Parameters require BEM 1.3");
            Validate(m.at("parameters").size()<=64,"More than 64 body parameters");
            std::set<std::string> ids;
            for(auto parameter:m.at("parameters")) {
                const auto id=Id(parameter.at("id")); S(parameter.at("name"));
                Check(ids.insert(id).second,"Duplicate body parameter");
                if(!parameter.contains("min")) parameter["min"]=0;
                if(!parameter.contains("max")) parameter["max"]=1000;
                if(!parameter.contains("neutral")) parameter["neutral"]=0;
                if(!parameter.contains("default")) parameter["default"]=parameter.at("neutral");
                if(!parameter.contains("step")) parameter["step"]=1;
                const auto min=U(parameter.at("min")),max=U(parameter.at("max")),neutral=U(parameter.at("neutral")),def=U(parameter.at("default")),step=U(parameter.at("step"));
                Check(min<max && max<=1000 && step>0 && (max-min)%step==0 && neutral>=min && neutral<=max && def>=min && def<=max &&
                    (neutral-min)%step==0 && (def-min)%step==0,"Invalid body parameter range/default/neutral/step");
                if(parameter.contains("available_when")) Condition(parameter.at("available_when"),optionChoices);
                parameters.push_back(std::move(parameter));
            }
        }
        Validate(parameters.empty() || declared.contains("body-parameters"),"Body parameter capability mismatch");
        info.parameter_groups_json=parameters.dump();
        info.parameter_frame_values.resize(parameters.size());
        ParseParameters(parameters,{},&info.default_parameters);
        if(m.contains("mesh_deformations")) {
            Check(minor>=3 && m.at("mesh_deformations").is_array(),"Mesh deformations require BEM 1.3");
            Validate(m.at("mesh_deformations").size()<=4096,"More than 4096 mesh deformation channels");
            std::set<std::pair<uint32_t,std::string>> channels;
            for(const auto& channel:m.at("mesh_deformations")) {
                const auto mesh=U(channel.at("mesh")); const auto id=Id(channel.at("parameter"));
                Check(mesh<m.at("meshes").size() && channels.emplace(mesh,id).second,"Missing mesh or duplicate deformation channel");
                const J* parameter=nullptr;size_t parameterIndex=0;
                for(size_t index=0;index<parameters.size();++index) if(parameters.at(index).at("id")==id) {parameter=&parameters.at(index);parameterIndex=index;}
                Check(parameter,"Unknown deformation parameter");
                const auto min=U(parameter->at("min")),max=U(parameter->at("max")),neutral=U(parameter->at("neutral")),step=U(parameter->at("step"));
                const auto& frames=channel.at("frames");
                Check(frames.is_array() && frames.size()>=2 && frames.size()<=64,"Invalid deformation frame count");
                bool hasNeutral=false; uint32_t previous=0;
                for(size_t i=0;i<frames.size();++i) {
                    const auto& frame=frames.at(i); const auto value=U(frame.at("value"));
                    Check(value>=min && value<=max && (value-min)%step==0 && (!i || value>previous),"Deformation frame values must be ordered and in parameter range");
                    previous=value;
                    auto& samples=info.parameter_frame_values.at(parameterIndex);
                    if(std::find(samples.begin(),samples.end(),value)==samples.end()) samples.push_back(value);
                    if(frame.value("neutral",false)) {
                        Check(value==neutral && !hasNeutral && !frame.contains("payload") && !frame.contains("count") && !frame.contains("encoding"),"Invalid neutral deformation frame");
                        hasNeutral=true;
                    } else {
                        Check(value!=neutral && U(frame.at("payload"))<directory.size() &&
                            U(frame.at("count"))<=U(m.at("meshes").at(mesh).at("vertex_count")) && S(frame.at("encoding"))=="sparse-position-f32","Invalid deformation payload reference/count/encoding");
                        const auto id=U(frame.at("payload"));
                        if(!manifestOnly) Check(directory[id].decoded>=4ull+16ull*U(frame.at("count")),"Truncated position delta payload");
                    }
                }
                Check(hasNeutral && U(frames.front().at("value"))==min && U(frames.back().at("value"))==max,"Deformation frames must include neutral and range endpoints");
                const auto& meshDefinition=m.at("meshes").at(mesh); bool validPosition=false;
                for(const auto& a:meshDefinition.at("attributes")) if(a.is_array() && a.size()==5 && U(a.at(0))==0)
                    validPosition=U(a.at(1))==0 && U(a.at(2))==3;
                Check(validPosition,"Position deformations require Float32 XYZ position attribute");
            }
            Validate(m.at("mesh_deformations").empty() || declared.contains("mesh-position-deltas"),"Position delta capability mismatch");
        }
        // Section 13 sound attachments. The payload holds encoded audio, so only
        // its presence and size can be checked here; the platform decoder is the
        // authority on whether the bytes are playable.
        if(m.contains("drivers")) {
            Check(minor>=5 && m.at("drivers").is_array(),"Driver sound attachments require BEM 1.5");
            for(const auto& driver:m.at("drivers")) {
                if(!driver.is_object() || !driver.contains("sound")) continue;
                const auto& sound=driver.at("sound");
                Check(sound.is_object(),"Invalid driver sound object");
                Check(U(sound.at("payload"))<directory.size(),"Missing driver sound payload");
                const auto id=U(sound.at("payload"));
                if(!manifestOnly) Check(directory[id].decoded>=16 && directory[id].decoded<=32ull*MiB,
                    "Driver sound payload extent is outside 16 B..32 MiB");
                if(sound.contains("gain")) {const auto gain=sound.at("gain").get<float>();
                    Check(std::isfinite(gain) && gain>=0.0f && gain<=2.0f,"Driver sound gain must be inside [0,2]");}
                if(sound.contains("offset_seconds")) {const auto offset=sound.at("offset_seconds").get<double>();
                    Check(std::isfinite(offset) && offset>=0.0 && offset<=60.0,"Driver sound offset must be inside [0,60] s");}
            }
        }
        auto selection=std::make_shared<BemSelectionMetadata>();
        selection->manifest=J::object();
        for(const auto* key:{"appearances","option_groups","component_rules","selection_constraints","texture_slots"})
            if(m.contains(key)) selection->manifest[key]=m.at(key);
        selection->parameters=parameters;info.selection_metadata=std::move(selection);
    }
    uint64_t decoded=0;
    std::map<uint32_t,std::vector<uint8_t>> cache;
    std::map<uint32_t,size_t> payloadUses;
    uint64_t cacheBytes=0;
    // Stored bytes of one payload, container-agnostic. In a ZIP container the directory
    // offset field holds the entry index, so a raw file read would return the wrong bytes;
    // rewriting a package therefore has to go through here, not through read().
    std::vector<uint8_t> Stored(uint32_t id) {
        Check(id<directory.size(),"Missing payload");
        const auto& e=directory[id];
        if(!is_zip) return read(e.offset,static_cast<size_t>(e.stored));
        int zip_idx=(id<zip_payload_file_indices.size())?zip_payload_file_indices[id]:-1;
        Check(zip_idx>=0,"ZIP payload entry missing");
        // The ZIP directory records compressed and decoded extents; a rewritten package
        // stores payloads raw, so the buffer must be sized by the decoded extent.
        std::vector<uint8_t> bytes(static_cast<size_t>(e.decoded));
        Check(mz_zip_reader_extract_to_mem(&zip,zip_idx,bytes.data(),bytes.size(),0)!=0,
            "Failed to decompress ZIP payload entry");
        return bytes;
    }
    // Container-agnostic payload decode without any bookkeeping.
    std::vector<uint8_t> DecodePayloadBytes(uint32_t id) {
        const auto& e=directory[id];
        std::vector<uint8_t> bytes;
        if (is_zip) {
            bytes.resize(static_cast<size_t>(e.decoded));
            int zip_idx = (id < zip_payload_file_indices.size()) ? zip_payload_file_indices[id] : -1;
            Check(zip_idx >= 0, "ZIP payload entry missing");
            mz_bool ok = mz_zip_reader_extract_to_mem(&zip, zip_idx, bytes.data(), bytes.size(), 0);
            Check(ok != 0, "Failed to decompress ZIP payload entry");
        } else {
            bytes = read(e.offset,static_cast<size_t>(e.stored));
            if(e.codec) {
                Check(ZSTD_findFrameCompressedSize(bytes.data(),bytes.size())==bytes.size() &&
                    ZSTD_getFrameContentSize(bytes.data(),bytes.size())==e.decoded,"Zstd frame extent/content size differs");
                std::vector<uint8_t> result(static_cast<size_t>(e.decoded));
                auto n=ZSTD_decompress(result.data(),result.size(),bytes.data(),bytes.size());
                Check(!ZSTD_isError(n) && n==result.size(),"Invalid Zstd payload"); bytes=std::move(result);
            }
        }
        return bytes;
    }
    const std::vector<uint8_t>& Payload(const J& reference,uint64_t expected) {
        auto id=U(reference); Check(id<directory.size(),"Missing payload"); const auto& e=directory[id];
        Check(e.decoded==expected,"Payload decoded size differs from resource description");
        auto it=cache.find(id); if(it!=cache.end()) return it->second;
        const uint64_t decodedBudget=limits.budget;
        Check(decoded<=decodedBudget-e.decoded,"Selected appearance exceeds decoded payload budget"); decoded+=e.decoded;
        if(stats) stats->payload_ids.push_back(id);
        std::vector<uint8_t> bytes=DecodePayloadBytes(id);
        cacheBytes+=bytes.size();
        if(stats) {
            stats->decoded_cache_peak_bytes=std::max(stats->decoded_cache_peak_bytes,cacheBytes);
            stats->decoded_cache_remaining_bytes=cacheBytes;
        }
        return cache.emplace(id,std::move(bytes)).first->second;
    }
    void ReleasePayload(uint32_t id) {
        if(!loading_optimization) return;
        auto use=payloadUses.find(id);
        Check(use!=payloadUses.end() && use->second,"Selected payload consumer count differs");
        if(--use->second==0) {
            auto entry=cache.find(id);
            Check(entry!=cache.end(),"Selected payload missing from cache");
            cacheBytes-=directory[id].decoded;
            cache.erase(entry); payloadUses.erase(use);
            if(stats) stats->decoded_cache_remaining_bytes=cacheBytes;
        }
    }
    std::vector<uint8_t> TakePayload(const J& reference,uint64_t expected) {
        const auto id=U(reference); const auto& bytes=Payload(reference,expected);
        if(loading_optimization) {
            auto use=payloadUses.find(id);
            Check(use!=payloadUses.end() && use->second,"Selected payload consumer count differs");
            if(use->second==1) {
                if(stats) stats->payload_move_bytes+=bytes.size();
                auto result=std::move(cache.at(id)); ReleasePayload(id); return result;
            }
        }
        if(stats) stats->payload_copy_bytes+=bytes.size();
        auto result=bytes; ReleasePayload(id); return result;
    }
    void AppendPayload(const J& reference,uint64_t expected,std::vector<uint8_t>& output) {
        const auto& bytes=Payload(reference,expected);
        output.insert(output.end(),bytes.begin(),bytes.end());
        if(stats) stats->payload_copy_bytes+=bytes.size();
        ReleasePayload(U(reference));
    }
    struct Selection { J operations;std::map<std::string,std::string> effective; };
    Selection Select(std::string_view requested,const J* management_manifest=nullptr) const {
        const auto& m=management_manifest?*management_manifest:manifest;
        Selection result;
        if(!minor) {
            const auto choice=requested.empty()?info.default_appearance:std::string(requested);
            for(const auto& appearance:m.at("appearances")) if(appearance.at("id")==choice) {
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
        for(const auto& group:m.at("option_groups")) {
            auto id=group.at("id").get<std::string>();known.insert(id);
            auto choice=saved.contains(id)?saved.at(id):group.at("default").get<std::string>();
            bool valid=false;for(const auto& item:group.at("choices")) valid|=item.at("id")==choice;
            Check(valid,"Unknown option choice");
            if(!group.contains("available_when") || Evaluate(group.at("available_when"),result.effective))
                result.effective.emplace(id,choice);
        }
        for(const auto& [group,_]:saved) Check(known.contains(group),"Unknown option group");
        if(m.contains("selection_constraints")) for(const auto& rule:m.at("selection_constraints"))
            Check(Evaluate(rule,result.effective),"Unreachable option combination");
        result.operations=J::array();
        for(const auto& rule:m.at("component_rules")) {
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
    struct WeightedFrame { const J* frame; double weight; };
    std::vector<WeightedFrame> MorphFrames(uint32_t mesh,const ParameterTicks& ticks) const {
        std::vector<WeightedFrame> result;
        if(!manifest.contains("mesh_deformations")) return result;
        for(const auto& channel:manifest.at("mesh_deformations")) {
            if(U(channel.at("mesh"))!=mesh) continue;
            const auto tick=ticks.at(channel.at("parameter").get<std::string>());
            const auto& frames=channel.at("frames");
            auto right=std::lower_bound(frames.begin(),frames.end(),tick,[](const J& frame,uint32_t value){return U(frame.at("value"))<value;});
            Check(right!=frames.end(),"Deformation interpolation outside parameter range");
            auto add=[&](const J& frame,double weight) {if(weight>0 && !frame.value("neutral",false)) result.push_back({&frame,weight});};
            if(U(right->at("value"))==tick) {add(*right,1);continue;}
            Check(right!=frames.begin(),"Deformation interpolation outside parameter range");
            const auto& left=*(right-1);
            const double blend=double(tick-U(left.at("value")))/double(U(right->at("value"))-U(left.at("value")));
            add(left,1-blend);add(*right,blend);
        }
        return result;
    }
    // Stream index and in-stride byte offset of the mesh's Float32 XYZ position.
    // Section 10 requires exactly one, so a second one is a package error.
    std::pair<uint32_t,uint32_t> MorphAttribute(uint32_t mesh) const {
        uint32_t stream=0,offset=0;bool found=false;
        for(const auto& attribute:manifest.at("meshes").at(mesh).at("attributes")) if(U(attribute.at(0))==0) {
            Check(!found && U(attribute.at(1))==0 && U(attribute.at(2))==3,"Position deformations require one Float32 XYZ position attribute");
            stream=U(attribute.at(3));offset=U(attribute.at(4));found=true;
        }
        Check(found && stream<3,"Missing morph position stream");
        return {stream,offset};
    }
    void ApplyMorph(uint32_t mesh,const ParameterTicks& ticks,BemComponent& component) {
        const auto frames=MorphFrames(mesh,ticks); if(frames.empty()) return;
        const auto attribute=MorphAttribute(mesh);const auto stream=attribute.first,offset=attribute.second;
        const uint32_t strides[]{component.info.stride0,component.info.stride1,component.info.stride2};
        Check(offset<=strides[stream] && 12<=strides[stream]-offset,"Morph position attribute exceeds stream stride");
        // Each load starts with immutable package bytes. Channels only add position
        // deltas; skin, directions, UVs, indices and material ownership are unchanged.
        for(const auto& selected:frames) {
            const auto& frame=*selected.frame; const auto id=U(frame.at("payload"));
            const auto& bytes=Payload(frame.at("payload"),directory.at(id).decoded);
            uint32_t jsonLength=0; Check(bytes.size()>=4,"Truncated position delta header");
            std::memcpy(&jsonLength,bytes.data(),4);
            const auto count=U(frame.at("count"));
            Check(jsonLength>0 && jsonLength<=MiB && uint64_t(jsonLength)+4+uint64_t(count)*16==bytes.size(),"Position delta payload extent differs");
            const auto header=J::parse(bytes.begin()+4,bytes.begin()+4+jsonLength);
            Check(header.is_object() && header.size()==2 && U(header.at("count"))==count &&
                S(header.at("encoding"))=="sparse-position-f32","Position delta header differs from frame");
            std::set<uint32_t> vertices;
            for(uint32_t n=0;n<count;++n) {
                const auto* record=bytes.data()+4+jsonLength+size_t(n)*16;
                uint32_t vertex=0;float delta[3];std::memcpy(&vertex,record,4);std::memcpy(delta,record+4,12);
                Check(vertex<component.info.vertex_count && vertices.insert(vertex).second,"Position delta vertex is outside mesh or duplicated");
                const auto address=size_t(vertex)*strides[stream]+offset;
                auto* target=component.streams[stream].data()+address;float position[3];std::memcpy(position,target,12);
                for(size_t axis=0;axis<3;++axis) {
                    Check(std::isfinite(delta[axis]) && std::isfinite(position[axis]),"Non-finite morph position/delta");
                    const double next=double(position[axis])+selected.weight*double(delta[axis]);
                    position[axis]=static_cast<float>(next);
                    Check(std::isfinite(position[axis]),"Morphed position overflow");
                }
                std::memcpy(target,position,12);
            }
            ReleasePayload(id);
        }
    }
    // Lowers every mesh_deformations channel of `mesh` into a self-contained
    // sample set and snapshots the unmorphed position stream. Runs once per
    // load, before ApplyMorph; the driver runtime then never parses again.
    // Returns null when this mesh has no deformation channels, which is the
    // normal case for every package that predates 1.3.
    std::shared_ptr<BemMorphSet> CaptureMorph(uint32_t mesh,const BemComponent& component,const ParameterTicks& ticks) {
        if(!manifest.contains("mesh_deformations")) return nullptr;
        bool present=false;
        for(const auto& channel:manifest.at("mesh_deformations")) if(U(channel.at("mesh"))==mesh) {present=true;break;}
        if(!present) return nullptr;
        const auto attribute=MorphAttribute(mesh);const auto stream=attribute.first,offset=attribute.second;
        const uint32_t strides[]{component.info.stride0,component.info.stride1,component.info.stride2};
        Check(offset<=strides[stream] && 12<=strides[stream]-offset,"Morph position attribute exceeds stream stride");
        const auto& source=component.streams[stream];
        Check(source.size()>=size_t(component.info.vertex_count)*strides[stream],"Position stream is shorter than the vertex count");

        auto morph=std::make_shared<BemMorphSet>();
        morph->stream=stream;morph->offset=offset;morph->stride=strides[stream];
        std::map<std::string,std::array<uint32_t,4>> domains;
        if(manifest.contains("parameters")) for(const auto& parameter:manifest.at("parameters"))
            domains.emplace(parameter.at("id").get<std::string>(),std::array<uint32_t,4>{
                U(parameter.at("min")),U(parameter.at("max")),U(parameter.at("step")),U(parameter.at("neutral"))});
        for(const auto& channel:manifest.at("mesh_deformations")) {
            if(U(channel.at("mesh"))!=mesh) continue;
            BemMorphChannel lowered;lowered.parameter=channel.at("parameter").get<std::string>();
            const auto domain=domains.find(lowered.parameter);
            if(domain!=domains.end()) {
                lowered.min_tick=domain->second[0];lowered.max_tick=domain->second[1];
                lowered.step=domain->second[2]?domain->second[2]:1;lowered.neutral_tick=domain->second[3];
            }
            for(const auto& frame:channel.at("frames")) {
                BemMorphFrame lowered_frame;lowered_frame.tick=U(frame.at("value"));lowered_frame.neutral=frame.value("neutral",false);
                if(!lowered_frame.neutral) {
                    const auto id=U(frame.at("payload"));
                    // Transient decode: capture must not disturb the planned
                    // payload closure or the streaming cache.
                    const auto bytes=DecodePayloadBytes(id);
                    uint32_t jsonLength=0;Check(bytes.size()>=4,"Truncated position delta header");
                    std::memcpy(&jsonLength,bytes.data(),4);
                    const auto count=U(frame.at("count"));
                    Check(jsonLength>0 && jsonLength<=MiB && uint64_t(jsonLength)+4+uint64_t(count)*16==bytes.size(),"Position delta payload extent differs");
                    const auto header=J::parse(bytes.begin()+4,bytes.begin()+4+jsonLength);
                    Check(header.is_object() && header.size()==2 && U(header.at("count"))==count &&
                        S(header.at("encoding"))=="sparse-position-f32","Position delta header differs from frame");
                    lowered_frame.deltas.reserve(count);
                    std::set<uint32_t> seen;
                    for(uint32_t n=0;n<count;++n) {
                        const auto* record=bytes.data()+4+jsonLength+size_t(n)*16;
                        BemMorphDelta delta{};std::memcpy(&delta.vertex,record,4);
                        std::memcpy(&delta.dx,record+4,4);std::memcpy(&delta.dy,record+8,4);std::memcpy(&delta.dz,record+12,4);
                        Check(delta.vertex<component.info.vertex_count && seen.insert(delta.vertex).second,
                            "Position delta vertex is outside mesh or duplicated");
                        Check(std::isfinite(delta.dx) && std::isfinite(delta.dy) && std::isfinite(delta.dz),"Non-finite morph delta");
                        lowered_frame.deltas.push_back(delta);
                    }
                }
                lowered.frames.push_back(std::move(lowered_frame));
            }
            if(!lowered.frames.empty()) {
                // Load-time weights, computed the same way MorphFrames blends a
                // tick but clamped rather than fatal: the runtime keeps these so
                // a driver taking over another parameter leaves this one alone.
                const auto tick=ticks.find(lowered.parameter);
                if(tick!=ticks.end()) {
                    const auto value=tick->second;
                    auto right=std::lower_bound(lowered.frames.begin(),lowered.frames.end(),value,
                        [](const BemMorphFrame& frame,uint32_t probe){return frame.tick<probe;});
                    if(right==lowered.frames.end()) right=std::prev(lowered.frames.end());
                    if(right->tick==value) lowered.settled.push_back({uint32_t(right-lowered.frames.begin()),1.0f});
                    else if(right!=lowered.frames.begin()) {
                        const auto& left=*(right-1);
                        const double blend=double(value-left.tick)/double(right->tick-left.tick);
                        if(blend<1.0) lowered.settled.push_back({uint32_t((right-1)-lowered.frames.begin()),float(1.0-blend)});
                        if(blend>0.0) lowered.settled.push_back({uint32_t(right-lowered.frames.begin()),float(blend)});
                    }
                }
                morph->channels.push_back(std::move(lowered));
            }
        }
        if(morph->channels.empty()) return nullptr;

        std::set<uint32_t> touched;
        for(const auto& channel:morph->channels) for(const auto& frame:channel.frames)
            for(const auto& delta:frame.deltas) touched.insert(delta.vertex);
        morph->vertices.assign(touched.begin(),touched.end());
        morph->base=source;
        return morph;
    }

    void Decode(std::string_view requested,BemPocData& out,std::string_view parameterSelection={},BemLoadPlan* plan=nullptr,std::string_view resourceSelection={}) {
        out.skip_validation=skip_validation;
        out.loading_optimization=loading_optimization;
        const auto& m=manifest;auto selection=Select(requested);
        std::map<uint32_t,uint32_t> componentMap;
        if(!resourceSelection.empty()) {
            const auto resource=std::find_if(info.resources.begin(),info.resources.end(),[&](const auto& r){return r.id==resourceSelection;});
            Check(!info.resources.empty() && resource!=info.resources.end(),"Unknown resource selection");
            for(const auto id:resource->component_ids) componentMap.emplace(id,static_cast<uint32_t>(componentMap.size()));
            J selected=J::array();
            for(const auto& op:selection.operations) if(componentMap.contains(U(op.at("target")))) selected.push_back(op);
            selection.operations=std::move(selected);
        } else for(uint32_t id=0;id<info.component_names.size();++id) componentMap.emplace(id,id);
        auto localComponent=[&](uint32_t id) {
            const auto found=componentMap.find(id); Check(found!=componentMap.end(),"Cross-resource selected donor");return found->second;
        };
        auto ticks=ParseParameters(parameters,parameterSelection);
        for(const auto& parameter:parameters) if(parameter.contains("available_when") && !Evaluate(parameter.at("available_when"),selection.effective))
            ticks.at(parameter.at("id").get<std::string>())=U(parameter.at("neutral"));
        out.header.version=1;
        out.header.component_count=static_cast<uint32_t>(componentMap.size());
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
        if(loading_optimization || plan) {
            // Count exactly the selected consumers before decoding any payload.
            // A payload may back several streams, textures or concatenated draws;
            // only its last consumer may take ownership of the cached vector.
            std::set<uint32_t> selectedTextures;
            auto add=[&](const J& reference,std::optional<uint64_t> expected=std::nullopt) {
                const auto id=U(reference);Check(id<directory.size(),"Missing payload");
                if(expected) Check(directory[id].decoded==*expected,"Payload decoded size differs from resource description");
                ++payloadUses[id];
            };
            auto addTextures=[&](const J& refs) {
                for(const auto& ref:refs) {
                    const auto id=ref.is_object()?slots.at(ref.at("slot").get<std::string>()):std::optional<uint32_t>(U(ref));
                    if(id) selectedTextures.insert(*id);
                }
            };
            for(const auto& op:selection.operations) {
                const auto action=op.at("operation").get<std::string>();
                if(action=="keep" && op.contains("material_overrides"))
                    for(const auto& material:op.at("material_overrides")) addTextures(material.at("textures"));
                if(action!="replace") continue;
                const auto& mesh=m.at("meshes").at(U(op.at("mesh")));
                for(const auto& frame:MorphFrames(U(op.at("mesh")),ticks)) add(frame.frame->at("payload"));
                for(const auto& stream:mesh.at("streams"))
                    add(stream.at("payload"),uint64_t(U(mesh.at("vertex_count")))*U(stream.at("stride")));
                if(!minor) add(mesh.at("indices"),uint64_t(U(mesh.at("index_count")))*U(mesh.at("index_size")));
                for(const auto& draw:mesh.at("draws")) {
                    if(minor && draw.contains("when") && !Evaluate(draw.at("when"),selection.effective)) continue;
                    if(minor) add(draw.at("indices"),uint64_t(U(draw.at("count")))*U(mesh.at("index_size")));
                    addTextures(draw.at("textures"));
                }
            }
            if(!deferTextures) for(auto id:selectedTextures) add(m.at("textures").at(id).at("payload"));
        }
        if(plan) {
            plan->package=info;
            for(const auto& [id,uses]:payloadUses) {
                Check(id<directory.size(),"Missing payload");
                const auto bytes=directory[id].decoded;
                Check(bytes<=limits.budget && plan->decoded_payload_bytes<=limits.budget-bytes,
                    "Selected appearance exceeds decoded payload budget");
                plan->decoded_payload_bytes+=bytes;
                // One cached allocation plus every selected consumer. This also
                // safely covers repeated references and temporary morph backing.
                Check(uses<UINT64_MAX && bytes<=UINT64_MAX/(uses+1),"Decoded reservation overflow");
                const auto bound=bytes*(uses+1);
                Check(plan->reservation_bytes<=UINT64_MAX-bound,"Decoded reservation overflow");
                plan->reservation_bytes+=bound;
                plan->payload_ids.push_back(id);
            }
            return; // Metadata and directory only; no payload reads/decompression.
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
                        uint32_t block=0,pixel=0,blockWidth=4; switch(ti.create_format) {case 4:pixel=4;break;case 63:pixel=1;Validate(!ti.create_srgb,"R8 texture must be linear");break;case 10:case 26:block=8;break;case 12:case 25:case 27:block=16;break;case 48:block=16;break;case 49:block=16;blockWidth=5;break;case 50:block=16;blockWidth=6;break;default:Check(false,"Unsupported texture format");}
                        Check(ti.width && ti.height && ti.mip_count && ti.mip_count<=32,"Invalid texture dimensions");
                        Validate(ti.width<=32768 && ti.height<=32768 && (pixel || ti.create_format>=48 || (ti.width%4==0 && ti.height%4==0)) && ti.mip_count<=16,"Invalid texture dimensions");
                        uint64_t size=0; for(uint32_t level=0;level<ti.mip_count;++level) {
                            const uint64_t mipWidth=std::max(ti.width>>level,1u),mipHeight=std::max(ti.height>>level,1u);
                            const uint64_t units=pixel?mipWidth*mipHeight:((mipWidth+blockWidth-1)/blockWidth)*((mipHeight+blockWidth-1)/blockWidth);
                            const uint64_t unitBytes=pixel?pixel:block;
                            Check(units<=(UINT32_MAX-size)/unitBytes,"Texture exceeds upload size representation"); size+=units*unitBytes;
                        }
                        Check(size<=limits.textureBytes,"Texture exceeds "+std::to_string(limits.textureBytes/MiB)+" MiB"); reserve(size);
                        if(deferTextures) {
                            const auto pid=U(t.at("payload")); Check(pid<directory.size(),"Missing payload");
                            const auto& e=directory[pid];
                            Check(e.decoded==size,"Payload decoded size differs from resource description");
                            tex.payload_id=pid; tex.payload_codec=e.codec; tex.payload_offset=e.offset; tex.payload_stored=e.stored;
                        } else tex.data=TakePayload(t.at("payload"),size);
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
            BemComponent c; c.skip_validation=skip_validation; auto cid=U(op.at("target")); c.info.component_id=localComponent(cid); c.info.original_index_count=info.original_counts[cid];
            c.static_mesh=!info.component_static.empty() && info.component_static.at(cid);
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
            h.index_element_size=U(mesh.at("index_size")); h.stream_count=static_cast<uint32_t>(mesh.at("streams").size());
            J chosenDraws=J::array();
            if(minor) {
                uint64_t count=0;
                for(const auto& draw:mesh.at("draws"))
                    if(!draw.contains("when") || Evaluate(draw.at("when"),selection.effective)) {
                        count+=U(draw.at("count"));Check(count<=UINT32_MAX,"Index count exceeds upload representation");
                        Validate(count<=16777216,"Selected index count exceeds limit");
                        chosenDraws.push_back(draw);
                    }
                Check(!chosenDraws.empty(),"Selected draw limit exceeded"); Validate(chosenDraws.size()<=256,"Selected draw limit exceeded");
                h.index_count=static_cast<uint32_t>(count);
            } else {h.index_count=U(mesh.at("index_count"));chosenDraws=mesh.at("draws");}
            Check(h.vertex_count && h.index_count &&
                (h.index_element_size==2||h.index_element_size==4),"Invalid geometry counts/index type");
            Validate(h.vertex_count<=1048576 && h.index_count<=16777216 && h.index_count%3==0,"Invalid geometry counts/index type");
            std::array<uint32_t,3> strides{};
            for(size_t s=0;s<h.stream_count;++s) {
                const auto& stream=mesh.at("streams")[s]; strides[s]=U(stream.at("stride"));
                Check(strides[s],"Invalid stream stride"); Validate(strides[s]<=64,"Invalid stream stride");
                const auto size=uint64_t(h.vertex_count)*strides[s]; reserve(size); c.streams[s]=TakePayload(stream.at("payload"),size);
            }
            h.stride0=strides[0]; h.stride1=strides[1]; h.stride2=strides[2];
            Check(c.static_mesh || h.stride2==4||h.stride2==12||((minor>=2||skip_validation)&&h.stride2==32),"Unsupported skin layout");
            const auto& attrs=mesh.at("attributes"); Check(attrs.is_array() && !attrs.empty() && attrs.size()<=16,"Invalid attributes");
            std::array<uint32_t,3> offsets{}; std::set<uint32_t> semantics;
            constexpr uint32_t sizes[]{4,2,1,1,2,2,1,1,2,2,4,4};
            for(const auto& a:attrs) {
                Check(a.is_array() && a.size()==5,"Attribute needs semantic/format/dimension/stream/offset");
                auto sem=U(a[0]),fmt=U(a[1]),dim=U(a[2]),stream=U(a[3]),off=U(a[4]);
                Check(sem<=13 && fmt<12 && dim>=1 && dim<=4 && stream<h.stream_count,"Invalid vertex attribute");
                Check(!c.static_mesh || (sem!=12 && sem!=13),"Static mesh has skin attributes"); Validate(semantics.insert(sem).second,"Invalid vertex attribute");
                Validate(off==offsets[stream],"Invalid vertex attribute offset"); offsets[stream]+=sizes[fmt]*dim;
                c.attributes.push_back({int32_t(sem),int32_t(fmt),int32_t(dim),int32_t(stream)});
            }
            // Stride 32 (float weights, UInt32 indices) is BEM 1.2's uncompressed native skin layout.
            const bool skin32=h.stride2==32;
            Validate(offsets==strides && (c.static_mesh || c.attributes.back()==std::array<int32_t,4>{13,skin32?10:6,4,2}),"Declaration/stride or skin indices differ");
            std::vector<std::array<int32_t,4>> skinAttributes;
            for(const auto& a:c.attributes) if(a[3]==2) skinAttributes.push_back(a);
            const std::vector<std::array<int32_t,4>> expectedSkin=h.stride2==4?
                std::vector<std::array<int32_t,4>>{{13,6,4,2}}:skin32?
                std::vector<std::array<int32_t,4>>{{12,0,4,2},{13,10,4,2}}:
                std::vector<std::array<int32_t,4>>{{12,4,4,2},{13,6,4,2}};
            Validate(c.static_mesh || skinAttributes==expectedSkin,"Unsupported skin declaration");
            c.layout_crc=Crc(std::string_view(reinterpret_cast<const char*>(c.attributes.data()),c.attributes.size()*16));
            // Capture before morphing so `base` is the authored position; the
            // load-time frame weights travel with each channel so a driver can
            // take over one parameter and hand it back on pause.
            const auto morphMesh=U(op.at("mesh"));
            auto morphSource=CaptureMorph(morphMesh,c,ticks);
            ApplyMorph(morphMesh,ticks,c);
            if(morphSource) c.morph=std::move(morphSource);
            auto indexBytes=uint64_t(h.index_count)*h.index_element_size; reserve(indexBytes);
            if(minor) {
                if(loading_optimization && chosenDraws.size()==1)
                    c.indices=TakePayload(chosenDraws[0].at("indices"),indexBytes);
                else {
                    c.indices.reserve(static_cast<size_t>(indexBytes));
                    for(const auto& draw:chosenDraws)
                        AppendPayload(draw.at("indices"),uint64_t(U(draw.at("count")))*h.index_element_size,c.indices);
                }
                Check(c.indices.size()==indexBytes,"Selected index buffer differs");
            } else c.indices=TakePayload(mesh.at("indices"),indexBytes);
            for(size_t n=0;n<h.index_count;++n) { uint32_t index=0; std::memcpy(&index,c.indices.data()+n*h.index_element_size,h.index_element_size); Validate(index<h.vertex_count,"Index outside vertex buffer"); }
            const auto& bones=mesh.at("bones"); Check(bones.is_array() && (c.static_mesh?bones.empty():!bones.empty()),"Invalid palette");
            Validate(bones.size()<=256,"Invalid palette");
            for(const auto& b:bones) {
                auto donor=U(b.at("component")), index=U(b.at("index")); auto name=S(b.at("name"));
                Check(donor<info.component_names.size() && index<m.at("target").at("components").at(donor).at("bone_names").size(),"Bone reference outside donor table");
                Validate(index<65536 && m.at("target").at("components").at(donor).at("bone_names").at(index)==name,"Bone identity differs");
                c.bones.push_back({localComponent(donor),index,Crc(name)}); c.bone_names.push_back(name);
                const auto& aliases=boneAliases.at(donor); auto alias=aliases.find(index);
                c.bone_aliases.push_back(alias==aliases.end()?std::vector<std::string>{}:alias->second);
                std::array<std::vector<std::string>,2> resourceAliases{};
                const auto& resourceTable=boneAliasResources.at(donor); auto resourceAlias=resourceTable.find(index);
                if(resourceAlias!=resourceTable.end()) resourceAliases=resourceAlias->second;
                c.bone_aliases_by_resource.push_back(std::move(resourceAliases));
            }
            if(!c.static_mesh) for(uint32_t n=0;n<h.vertex_count;++n) for(uint32_t k=0;k<4;++k) {
                uint32_t b=0;
                if(h.stride2==32) std::memcpy(&b,c.streams[2].data()+size_t(n)*32+16+k*4,4);
                else b=c.streams[2][size_t(n)*h.stride2+h.stride2-4+k];
                Validate(b<c.bones.size(),"Skin index outside palette"); h.max_bone=std::max(h.max_bone,b);
            }
            if(!c.static_mesh && h.stride2==32) for(uint32_t n=0;n<h.vertex_count;++n) {
                float w[4]; std::memcpy(w,c.streams[2].data()+size_t(n)*32,16);
                const float sum=w[0]+w[1]+w[2]+w[3];
                Validate(std::isfinite(sum) && sum>=0.99f && sum<=1.01f && w[0]>=0 && w[1]>=0 && w[2]>=0 && w[3]>=0,"Invalid skin weight sum");
            }
            if(!c.static_mesh && h.stride2==12) for(uint32_t n=0;n<h.vertex_count;++n) {
                uint16_t w[4]; std::memcpy(w,c.streams[2].data()+size_t(n)*12,8);
                const uint32_t sum=uint32_t(w[0])+w[1]+w[2]+w[3];
                Validate(sum>=64880 && sum<=66190,"Invalid skin weight sum");
            }
            const auto& draws=chosenDraws; Check(draws.is_array() && !draws.empty(),"Invalid draws"); Validate(draws.size()<=256,"Invalid draws");
            uint64_t end=0;
            for(const auto& d:draws) {
                auto start=minor?static_cast<uint32_t>(end):U(d.at("start"));
                auto count=U(d.at("count")),donor=U(d.at("material_component")),slot=U(d.at("material_slot")); auto name=S(d.at("material_name"));
                Check(count && donor<info.component_names.size() && slot<m.at("target").at("components").at(donor).at("materials").size(),"Material reference outside donor table");
                Validate(start==end && count%3==0 && slot<256 && m.at("target").at("components").at(donor).at("materials").at(slot)==name,"Invalid material draw"); end+=count;
                const auto mask=selectTextures(d.at("textures"),"Duplicate draw texture");
                BemMaterialOverrides overrides;
                if(d.contains("material_overrides")) {
                    const auto& ov=d.at("material_overrides");
                    Check(ov.is_object(),"material_overrides must be an object");
                    if(ov.contains("floats")) {
                        const auto& fl=ov.at("floats"); Check(fl.is_object(),"floats must be an object");
                        Validate(fl.size()<=(skip_validation?128u:32u),"Too many float overrides");
                        for(auto it=fl.begin();it!=fl.end();++it) {
                            Validate(ValidOverrideProperty(it.key(),skip_validation),"Invalid float property name");
                            float val=it.value().get<float>();
                            Validate(ValidOverrideFloat(val,skip_validation),"Invalid float value");
                            if(skip_validation) val=std::clamp(val,-128.0f,128.0f);
                            overrides.floats.emplace_back(it.key(),val);
                        }
                    }
                    if(ov.contains("colors")) {
                        const auto& cl=ov.at("colors"); Check(cl.is_object(),"colors must be an object");
                        Validate(cl.size()<=(skip_validation?64u:16u),"Too many color overrides");
                        for(auto it=cl.begin();it!=cl.end();++it) {
                            Validate(ValidOverrideProperty(it.key(),skip_validation),"Invalid color property name");
                            const auto& arr=it.value(); Check(arr.is_array()&&arr.size()==4,"Color must have 4 channels");
                            std::array<float,4> cval{arr[0].get<float>(),arr[1].get<float>(),arr[2].get<float>(),arr[3].get<float>()};
                            for(float& ch:cval) {
                                Validate(std::isfinite(ch)&&(skip_validation||(ch>=0.0f&&ch<=8.0f)),"Invalid color channel value");
                                if(skip_validation) ch=std::clamp(ch,0.0f,8.0f);
                            }
                            overrides.colors.emplace_back(it.key(),cval);
                        }
                    }
                    if(ov.contains("vectors")) {
                        const auto& vl=ov.at("vectors"); Check(vl.is_object(),"vectors must be an object");
                        Validate(vl.size()<=(skip_validation?32u:8u),"Too many vector overrides");
                        for(auto it=vl.begin();it!=vl.end();++it) {
                            Validate(ValidOverrideProperty(it.key(),skip_validation),"Invalid vector property name");
                            const auto& arr=it.value(); Check(arr.is_array()&&arr.size()==4,"Vector must have 4 components");
                            std::array<float,4> vval{arr[0].get<float>(),arr[1].get<float>(),arr[2].get<float>(),arr[3].get<float>()};
                            for(float& vh:vval) {
                                Validate(std::isfinite(vh)&&(skip_validation||(vh>=-65536.0f&&vh<=65536.0f)),"Invalid vector component value");
                                if(skip_validation) vh=std::clamp(vh,-65536.0f,65536.0f);
                            }
                            overrides.vectors.emplace_back(it.key(),vval);
                        }
                    }
                    if(ov.contains("disable_keywords")) {
                        const auto& dk=ov.at("disable_keywords"); Check(dk.is_array(),"disable_keywords must be an array");
                        Validate(dk.size()<=(skip_validation?64u:16u),"Too many disable_keywords");
                        for(const auto& kw:dk) {
                            std::string kws=kw.get<std::string>();
                            Validate(ValidOverrideKeyword(kws,skip_validation),"Invalid keyword in disable_keywords");
                            overrides.disable_keywords.push_back(std::move(kws));
                        }
                    }
                    if(ov.contains("enable_keywords")) {
                        const auto& ek=ov.at("enable_keywords"); Check(ek.is_array(),"enable_keywords must be an array");
                        Validate(ek.size()<=(skip_validation?64u:16u),"Too many enable_keywords");
                        for(const auto& kw:ek) {
                            std::string kws=kw.get<std::string>();
                            Validate(ValidOverrideKeyword(kws,skip_validation),"Invalid keyword in enable_keywords");
                            overrides.enable_keywords.push_back(std::move(kws));
                        }
                    }
                }
                c.draws.push_back({start,count,localComponent(donor),slot,Crc(name),0,mask});
                c.draw_material_overrides.push_back(std::move(overrides));
                c.material_names.push_back(name);
            }
            Validate(end==h.index_count,"Draws must partition IB"); h.reserved0=static_cast<uint32_t>(c.bones.size()); h.reserved1=static_cast<uint32_t>(c.draws.size());
            out.components.push_back(std::move(c));
        }
        out.header.texture_count=static_cast<uint32_t>(out.textures.size());
        out.root_offset=ReadRootOffset(m,skip_validation);
        Check(!loading_optimization || (payloadUses.empty() && cache.empty() && cacheBytes==0),
            "Selected payload consumers were not exhausted");
    }
};
template<class F> bool File(const std::filesystem::path& path,std::string& error,F action,bool skip_validation=false,bool loading_optimization=false,bool defer_textures=false,bool manifest_only=false) {
    error.clear(); try {
        std::ifstream in(path,std::ios::binary|std::ios::ate); Check(bool(in),"BEM package cannot be opened");
        auto size=in.tellg(); Check(size>=0,"BEM size unavailable"); Container c; c.skip_validation=skip_validation; c.loading_optimization=loading_optimization;
        c.deferTextures=defer_textures;
        c.manifestOnly=manifest_only;
        c.read=[&](uint64_t off,size_t count) {
            Check(off<=static_cast<uint64_t>(size) && count<=static_cast<uint64_t>(size)-off,"Truncated BEM");
            std::vector<uint8_t> bytes(count); in.seekg(static_cast<std::streamoff>(off));
            Check(bool(in.read(reinterpret_cast<char*>(bytes.data()),static_cast<std::streamsize>(count))),"BEM read failed"); return bytes; };
        c.Open(static_cast<uint64_t>(size)); action(c); return true;
    } catch(const std::exception& e) { error=e.what(); return false; }
}
}
bool ReadBemPackageInfo(const std::filesystem::path& path,BemPackageInfo& out,std::string& error,bool skip_validation) {
    out={}; return File(path,error,[&](Container& c){out=c.info;},skip_validation);
}
bool ReadBemManagementInfo(const std::filesystem::path& path,BemPackageInfo& out,std::string& error,bool skip_validation) {
    out={}; return File(path,error,[&](Container& c){out=c.info;},skip_validation,false,false,true);
}
bool ResolveBemSelection(const BemPackageInfo& info,std::string_view requested,
    std::string_view remembered,BemSelection& out,std::string& error) {
    out={}; error.clear();
    try {
        Check(bool(info.selection_metadata),"Selection metadata unavailable");
        const auto& manifest=info.selection_metadata->manifest;
        Container c; c.info.default_appearance=info.default_appearance; c.minor=info.minor;
        const auto selected=c.Select(requested,&manifest); // same availability/constraints/operations as Decode
        BemSelection result;
        if(!info.minor) result.options=requested.empty()?info.default_appearance:std::string(requested);
        else {
            std::map<std::string,std::string> saved;
            auto remaining=requested;
            while(!remaining.empty()) {
                const auto end=remaining.find('&'); const auto pair=remaining.substr(0,end); const auto colon=pair.find(':');
                saved.emplace(std::string(pair.substr(0,colon)),std::string(pair.substr(colon+1)));
                remaining=end==remaining.npos?std::string_view{}:remaining.substr(end+1);
            }
            for(const auto& group:manifest.at("option_groups")) {
                const auto id=group.at("id").get<std::string>();
                if(!result.options.empty()) result.options+='&';
                result.options+=id+":"+(saved.contains(id)?saved.at(id):group.at("default").get<std::string>());
                if(selected.effective.contains(id)) result.available_groups.push_back(id);
            }
            // Decode also requires exactly one selected texture-slot candidate.
            if(manifest.contains("texture_slots")) for(const auto& slot:manifest.at("texture_slots")) {
                size_t count=0;
                for(const auto& item:slot.at("candidates")) count+=!item.contains("when") || Evaluate(item.at("when"),selected.effective);
                Check(count==1,"Texture slot must have exactly one selected candidate");
            }
        }
        const auto& definitions=info.selection_metadata->parameters;
        auto ticks=ParseParameters(definitions,remembered,&result.parameters_saved);
        for(const auto& parameter:definitions) {
            const auto id=parameter.at("id").get<std::string>();
            const bool available=!parameter.contains("available_when") || Evaluate(parameter.at("available_when"),selected.effective);
            if(available) result.available_parameters.push_back(id);
            else ticks.at(id)=U(parameter.at("neutral"));
            if(!result.parameters.empty()) result.parameters+='&';
            result.parameters+=id+":"+std::to_string(ticks.at(id));
        }
        out=std::move(result); return true;
    } catch(const std::exception& e) {error=e.what(); return false;}
}
void SampleMorphChannel(const BemMorphChannel& channel,const BemMorphSet& set,
                        float range_low,float range_high,float weight,
                        std::vector<float>& accumulated) {
    if(accumulated.size()!=set.vertices.size()*3 || channel.frames.empty()) return;
    std::pair<uint32_t,float> resolved[2];size_t count=0;
    if(weight<0.0f) {
        // Restore path: the frame weights the load-time tick selected.
        for(const auto& entry:channel.settled) if(count<2) resolved[count++]=entry;
    } else {
        // Weight maps onto the parameter tick domain first (section 13 `range`),
        // then onto the authored frame ladder. Both ends are clamped: an odd
        // authoring decision must not become a crash inside the render tick.
        const double span=double(channel.max_tick-channel.min_tick);
        const float position=range_low+(range_high-range_low)*(weight<0.0f?0.0f:weight);
        long long tick=static_cast<long long>(channel.min_tick)+std::llround(span*double(position));
        tick=std::clamp<long long>(tick,channel.frames.front().tick,channel.frames.back().tick);
        auto right=std::lower_bound(channel.frames.begin(),channel.frames.end(),static_cast<uint32_t>(tick),
            [](const BemMorphFrame& frame,uint32_t probe){return frame.tick<probe;});
        if(right==channel.frames.end()) right=std::prev(channel.frames.end());
        if(right->tick==static_cast<uint32_t>(tick)) resolved[count++]={uint32_t(right-channel.frames.begin()),1.0f};
        else if(right!=channel.frames.begin()) {
            const auto& left=*(right-1);
            const double blend=double(tick-left.tick)/double(right->tick-left.tick);
            if(blend<1.0) resolved[count++]={uint32_t((right-1)-channel.frames.begin()),float(1.0-blend)};
            if(blend>0.0) resolved[count++]={uint32_t(right-channel.frames.begin()),float(blend)};
        }
    }
    for(size_t i=0;i<count;++i) {
        if(resolved[i].second==0.0f || resolved[i].first>=channel.frames.size()) continue;
        const auto& frame=channel.frames[resolved[i].first];
        if(frame.neutral) continue;
        for(const auto& delta:frame.deltas) {
            const auto local=std::lower_bound(set.vertices.begin(),set.vertices.end(),delta.vertex);
            if(local==set.vertices.end() || *local!=delta.vertex) continue;
            const size_t base=size_t(local-set.vertices.begin())*3;
            accumulated[base]+=resolved[i].second*delta.dx;
            accumulated[base+1]+=resolved[i].second*delta.dy;
            accumulated[base+2]+=resolved[i].second*delta.dz;
        }
    }
}
bool ResolveBemParameters(const BemPackageInfo& info,std::string_view requested,std::string& canonical,std::string& error) {
    canonical.clear();error.clear();
    try { ParseParameters(info.parameter_groups_json.empty()?J::array():J::parse(info.parameter_groups_json),requested,&canonical);return true; }
    catch(const std::exception& e) {error=e.what();canonical.clear();return false;}
}
bool ReadBemLoadPlan(const std::filesystem::path& path,BemLoadPlan& out,std::string& error,std::string_view appearance,bool skip_validation,std::string_view parameters,bool defer_texture_payloads,std::string_view resource_id) {
    out={};BemLoadPlan plan;
    if(!File(path,error,[&](Container& c){BemPocData unused;c.Decode(appearance,unused,parameters,&plan,resource_id);},skip_validation,false,defer_texture_payloads)) return false;
    out=std::move(plan);return true;
}
bool LoadBem(const std::filesystem::path& path,BemPocData& out,std::string& error,std::string_view appearance,BemLoadStats* stats,bool skip_validation,bool loading_optimization,std::string_view parameters,uint64_t max_decoded_reservation,bool defer_texture_payloads,std::string_view resource_id) {
    out={}; if(stats) *stats={};BemPocData parsed;
    std::shared_ptr<BemPayloadSource> source;
    if(defer_texture_payloads) {
        std::error_code size_error,time_error; source=std::make_shared<BemPayloadSource>(); source->path=path;
        source->file_size=std::filesystem::file_size(path,size_error);
        source->write_time=static_cast<int64_t>(std::filesystem::last_write_time(path,time_error).time_since_epoch().count());
        if(size_error || time_error) { error="BEM package generation unavailable"; return false; }
    }
    if(!File(path,error,[&](Container& c){
        if(max_decoded_reservation!=UINT64_MAX) {
            BemLoadPlan plan;BemPocData unused;c.Decode(appearance,unused,parameters,&plan,resource_id);
            Check(plan.reservation_bytes<=max_decoded_reservation,"Decoded reservation changed before load");
            c.payloadUses.clear();
        }
        c.stats=stats;c.Decode(appearance,parsed,parameters,nullptr,resource_id);
    },skip_validation,loading_optimization,defer_texture_payloads)) return false;
    if(source) {
        // The parsed directory belongs to the generation recorded above.
        std::error_code size_error,time_error;
        const auto size=std::filesystem::file_size(path,size_error);
        const auto time=std::filesystem::last_write_time(path,time_error);
        if(size_error || time_error || size!=source->file_size ||
            static_cast<int64_t>(time.time_since_epoch().count())!=source->write_time) {
            error="BEM package changed while loading"; return false;
        }
    }
    parsed.payload_source=std::move(source);
    out=std::move(parsed); return true;
}
bool ExtractBemPayload(const std::filesystem::path& package,uint32_t payload_id,
    const std::filesystem::path& output,std::string& error) {
    return File(package,error,[&](Container& c){
        Check(payload_id<c.directory.size(),"Missing payload");
        const auto bytes=c.DecodePayloadBytes(payload_id);
        Check(!bytes.empty(),"Payload is empty");
        std::error_code fs_error;
        if(auto parent=output.parent_path(); !parent.empty())
            std::filesystem::create_directories(parent,fs_error);
        std::ofstream out(output,std::ios::binary|std::ios::trunc);
        Check(bool(out),"Cannot write the audio cache file");
        out.write(reinterpret_cast<const char*>(bytes.data()),
            static_cast<std::streamsize>(bytes.size()));
        Check(bool(out),"Cannot write the audio cache file");
    },false,false,false,false);
}
bool DecodeBemTexturePayload(const BemPayloadSource& source,const BemTexture& texture,std::vector<uint8_t>& out,std::string& error) {
    out.clear(); error.clear();
    try {
        const uint64_t expected=texture.info.data_size;
        Check(texture.payload_id!=UINT32_MAX && texture.payload_codec<=1 && expected && texture.payload_stored &&
            texture.payload_stored<=Budget && expected<=Budget && (texture.payload_codec || texture.payload_stored==expected),
            "Invalid deferred texture payload extent");
        std::error_code ec;
        const auto size=std::filesystem::file_size(source.path,ec);
        Check(!ec && size==source.file_size,"BEM package changed since the selection was loaded");
        const auto time=std::filesystem::last_write_time(source.path,ec);
        Check(!ec && static_cast<int64_t>(time.time_since_epoch().count())==source.write_time,"BEM package changed since the selection was loaded");
        Check(texture.payload_offset<=size && texture.payload_stored<=size-texture.payload_offset,"Truncated BEM");
        std::ifstream in(source.path,std::ios::binary); Check(bool(in),"BEM package cannot be opened");
        in.seekg(static_cast<std::streamoff>(texture.payload_offset));
        out.resize(static_cast<size_t>(expected));
        if(!texture.payload_codec) {
            Check(bool(in.read(reinterpret_cast<char*>(out.data()),static_cast<std::streamsize>(expected))),"BEM read failed");
            return true;
        }
        // Stream the frame: the stored bytes and the decoded texture are never held whole together.
        struct Stream { ZSTD_DStream* s=ZSTD_createDStream(); ~Stream(){ ZSTD_freeDStream(s); } } stream;
        Check(stream.s && !ZSTD_isError(ZSTD_initDStream(stream.s)),"Zstd stream unavailable");
        std::vector<uint8_t> chunk(static_cast<size_t>(std::min<uint64_t>(texture.payload_stored,MiB)));
        uint64_t remaining=texture.payload_stored; ZSTD_outBuffer output{out.data(),out.size(),0}; size_t status=1; bool first=true;
        while(remaining) {
            const auto count=static_cast<size_t>(std::min<uint64_t>(remaining,chunk.size()));
            Check(bool(in.read(reinterpret_cast<char*>(chunk.data()),static_cast<std::streamsize>(count))),"BEM read failed");
            remaining-=count;
            if(first) {
                first=false;
                Check(ZSTD_getFrameContentSize(chunk.data(),count)==expected,"Zstd frame extent/content size differs");
            }
            ZSTD_inBuffer input{chunk.data(),count,0};
            while(input.pos<input.size) {
                Check(status!=0,"Trailing bytes after Zstd frame");
                const auto consumed=input.pos,produced=output.pos;
                status=ZSTD_decompressStream(stream.s,&output,&input);
                Check(!ZSTD_isError(status),"Invalid Zstd payload");
                // No progress with a full output buffer: the frame is larger than declared.
                Check(status==0 || input.pos!=consumed || output.pos!=produced,"Zstd frame exceeds texture size");
            }
        }
        Check(status==0 && output.pos==out.size(),"Invalid Zstd payload");
        return true;
    } catch(const std::exception& e) { out.clear(); out.shrink_to_fit(); error=e.what(); return false; }
}
bool ParseBem(std::span<const uint8_t> bytes,BemPocData& out,std::string& error,bool skip_validation,bool loading_optimization,std::string_view resource_id) {
    out={}; error.clear(); try {
        Container c; c.skip_validation=skip_validation; c.loading_optimization=loading_optimization; c.read=[&](uint64_t off,size_t n) { Check(off<=bytes.size() && n<=bytes.size()-off,"Truncated BEM");
            return std::vector<uint8_t>(bytes.begin()+off,bytes.begin()+off+n); };
        c.Open(bytes.size()); BemPocData parsed; c.Decode({},parsed,{},nullptr,resource_id); out=std::move(parsed); return true;
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
                auto bytes=c.Stored(old); auto codec=e.codec;
                // ZIP payloads are stored raw, so an installed package would balloon.
                // Compress them here to keep the rewritten package at normal size.
                if(!codec && !bytes.empty()) {
                    std::vector<uint8_t> packed(ZSTD_compressBound(bytes.size()));
                    auto written=ZSTD_compress(packed.data(),packed.size(),bytes.data(),bytes.size(),9);
                    if(!ZSTD_isError(written) && written<bytes.size()) {
                        packed.resize(written); bytes.swap(packed); codec=1;
                    }
                }
                it=copied.emplace(old,append(codec,e.decoded,bytes)).first; }
            ref=it->second;
        };
        auto manifest=c.manifest;
        // A ZIP container has no binary header, so a packer may record the version in the
        // manifest instead. The rewritten binary carries it in the header, so drop the hint.
        if(c.is_zip) manifest.erase("format_version");
        for(auto& mesh:manifest["meshes"]) {
            for(auto& stream:mesh["streams"]) copy(stream["payload"]);
            if(c.minor) for(auto& draw:mesh["draws"]) copy(draw["indices"]);
            else copy(mesh["indices"]);
        }
        if(manifest.contains("mesh_deformations")) for(auto& channel:manifest["mesh_deformations"])
            for(auto& frame:channel["frames"]) if(frame.contains("payload")) copy(frame["payload"]);
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
            if(texture == original) {
                copy(texture["payload"]);
            } else {
                size_t bound = ZSTD_compressBound(bytes.size());
                std::vector<uint8_t> compressed(bound);
                size_t cSize = ZSTD_compress(compressed.data(), compressed.size(), bytes.data(), bytes.size(), 3);
                if (!ZSTD_isError(cSize) && cSize < bytes.size()) {
                    compressed.resize(cSize);
                    texture["payload"] = append(1, bytes.size(), compressed);
                } else {
                    texture["payload"] = append(0, bytes.size(), bytes);
                }
            }
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
            // Evaluate the load before building the message: argument evaluation order is
            // unspecified, so inlining it would report an empty failure string.
            const bool loaded=LoadBem(output,parsed,failure,appearance);
            Check(loaded,"Installed validation: "+failure);}
        BemJson summary={{"package_id",c.info.package_id},{"character_id",c.info.character_id},{"name",c.info.name},
            {"target_kind",c.info.target_kind},{"target_id",c.info.target_id},{"resource_keys",c.info.resource_keys},
            {"textures",changes},{"bytes",offset},{"revision",1},{"bem_minor",c.minor}};
        if(c.minor) {summary["default_options"]=c.info.default_options;
            summary["option_groups"]=manifest.at("option_groups");
            summary["selection_constraints"]=manifest.value("selection_constraints",J::array());}
        else {summary["default_appearance"]=c.info.default_appearance;summary["appearances"]=c.info.appearances;}
        summary["parameters"]=c.parameters;
        summary["parameter_groups_json"]=c.info.parameter_groups_json;
        summary["default_parameters"]=c.info.default_parameters;
        report=summary.dump();
    });
    std::error_code ignored; std::filesystem::remove(spool,ignored);
    if(!ok) std::filesystem::remove(output,ignored);
    return ok;
}
}
