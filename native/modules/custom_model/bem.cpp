#include "bem.h"
#include "bem_rewrite.h"
#include "../../shared/third_party/nlohmann/json.hpp"
#include "../../shared/third_party/zstd/lib/zstd.h"
#include <algorithm>
#include <cstring>
#include <fstream>
#include <map>
#include <set>
#include <stdexcept>
#include <functional>

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
struct Container {
    J manifest;
    std::vector<Entry> directory;
    std::function<std::vector<uint8_t>(uint64_t,size_t)> read;
    BemPackageInfo info;
    void Open(uint64_t size) {
        Check(size>=sizeof(Header) && size<=2ull*1024*MiB,"Invalid BEM file size");
        auto raw=read(0,sizeof(Header)); Header h{}; std::memcpy(&h,raw.data(),sizeof(h));
        Check(std::memcmp(h.magic,"BEM\0PKG\0",8)==0 && h.major==1 && h.minor==0 && h.size==sizeof(h) && !h.flags,
            "Only BEM 1.0 packages are supported; convert source Mod in creator tools");
        Check(h.file==size && h.manifest>0 && h.manifest<=4*MiB && h.count<=4096 &&
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
    void Metadata() {
        const auto& m=manifest;
        Check(U(m.at("schema"))==1,"Unknown manifest schema");
        info.package_id=Id(m.at("package_id")); info.name=S(m.at("name")); info.author=S(m.at("author"));
        info.version=S(m.at("version")); info.default_appearance=Id(m.at("default_appearance_id"));
        const std::set<std::string> caps{"native-materials","palette-u8","indices-u32","fixed-appearances","texture-astc"};
        for(const auto& c:m.at("required_capabilities")) Check(caps.contains(S(c)),"Unsupported required capability");
        const auto& t=m.at("target"); Check(S(t.at("platform"))=="windows-x64","Unsupported target platform");
        info.character_id=Id(t.at("character_id")); Id(t.at("profile_id")); Id(t.at("revision"));
        info.world_resource=Id(t.at("world_resource")); info.ui_resource=Id(t.at("ui_resource"));
        Check(info.world_resource!=info.ui_resource,"Duplicate resource roots");
        const auto& cs=t.at("components"); Check(cs.is_array() && !cs.empty() && cs.size()<=64,"Invalid target components");
        std::set<std::string> names;
        for(size_t i=0;i<cs.size();++i) {
            const auto& c=cs[i]; auto name=S(c.at("mesh_name")); auto count=U(c.at("original_index_count"));
            Check(U(c.at("id"))==i && names.insert(name).second && count && count%3==0,"Invalid target identity");
            Check(c.at("bone_names").is_array() && c.at("bone_names").size()<=65536 &&
                c.at("materials").is_array() && c.at("materials").size()<=256,"Invalid target donor tables");
            info.component_names.push_back(name); info.original_counts.push_back(count);
        }
        const auto& apps=m.at("appearances"); Check(apps.is_array() && !apps.empty() && apps.size()<=64,"Invalid appearances");
        std::set<std::string> ids;
        for(const auto& a:apps) {
            auto id=Id(a.at("id")); S(a.at("name")); Check(ids.insert(id).second,"Duplicate appearance ID");
            info.appearances.push_back(id);
            const auto& ops=a.at("components"); Check(ops.is_array() && ops.size()==cs.size(),"Incomplete appearance");
            for(size_t i=0;i<ops.size();++i) {
                const auto& op=ops[i]; auto action=S(op.at("operation"));
                Check(U(op.at("target"))==i && (action=="keep"||action=="hide"||action=="replace"),"Invalid component operation");
                if(action=="replace") Check(U(op.at("mesh"))<m.at("meshes").size(),"Missing mesh reference");
            }
            if(a.contains("preview")) Check(U(a.at("preview"))<directory.size(),"Missing preview payload");
        }
        Check(ids.contains(info.default_appearance),"Default appearance missing");
        Check(m.at("meshes").is_array() && m.at("meshes").size()<=4096 &&
            m.at("textures").is_array() && m.at("textures").size()<=4096,"Invalid resource tables");
        for(const auto& mesh:m.at("meshes")) {
            Check(mesh.at("streams").is_array() && mesh.at("streams").size()==3,"Three streams required");
            for(const auto& s:mesh.at("streams")) Check(U(s.at("payload"))<directory.size(),"Missing stream payload");
            Check(U(mesh.at("indices"))<directory.size(),"Missing index payload");
            for(const auto& d:mesh.at("draws")) for(const auto& tx:d.at("textures"))
                Check(U(tx)<m.at("textures").size(),"Missing texture reference");
        }
        for(const auto& texture:m.at("textures")) Check(U(texture.at("payload"))<directory.size(),"Missing texture payload");
    }
    uint64_t decoded=0;
    std::map<uint32_t,std::vector<uint8_t>> cache;
    const std::vector<uint8_t>& Payload(const J& reference,uint64_t expected) {
        auto id=U(reference); Check(id<directory.size(),"Missing payload"); const auto& e=directory[id];
        Check(e.decoded==expected,"Payload decoded size differs from resource description");
        auto it=cache.find(id); if(it!=cache.end()) return it->second;
        Check(decoded<=Budget-e.decoded,"Selected appearance exceeds 512 MiB decoded budget"); decoded+=e.decoded;
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
    void Decode(std::string_view requested,BemPocData& out) {
        const auto& m=manifest; const J* selected=nullptr;
        const auto choice=requested.empty()?info.default_appearance:std::string(requested);
        for(const auto& a:m.at("appearances")) if(a.at("id")==choice) selected=&a;
        Check(selected!=nullptr,"Selected appearance missing; select an available appearance in Mod manager");
        out.header.version=1;
        out.header.component_count=static_cast<uint32_t>(info.component_names.size());
        std::map<uint32_t,uint32_t> textures;
        uint64_t resident=0;
        auto reserve=[&](uint64_t bytes) { Check(bytes<=Budget-resident,"Appearance exceeds runtime memory budget"); resident+=bytes; };
        for(const auto& op:selected->at("components")) {
            BemComponent c; auto cid=U(op.at("target")); c.info.component_id=cid; c.info.original_index_count=info.original_counts[cid];
            const auto action=S(op.at("operation"));
            if(action!="replace") { c.info.flags=kComponentFlagNoGeometry|(action=="hide"?kComponentFlagHidden:0); out.components.push_back(std::move(c)); continue; }
            const auto& mesh=m.at("meshes").at(U(op.at("mesh")));
            auto& h=c.info; h.vertex_count=U(mesh.at("vertex_count")); h.index_count=U(mesh.at("index_count"));
            h.index_element_size=U(mesh.at("index_size")); h.stream_count=3;
            Check(h.vertex_count && h.vertex_count<=1048576 && h.index_count && h.index_count<=16777216 && h.index_count%3==0 &&
                (h.index_element_size==2||h.index_element_size==4),"Invalid geometry counts/index type");
            std::array<uint32_t,3> strides{};
            for(size_t s=0;s<3;++s) {
                const auto& stream=mesh.at("streams")[s]; strides[s]=U(stream.at("stride"));
                Check(strides[s] && strides[s]<=64,"Invalid stream stride");
                const auto size=uint64_t(h.vertex_count)*strides[s]; reserve(size); c.streams[s]=Payload(stream.at("payload"),size);
            }
            h.stride0=strides[0]; h.stride1=strides[1]; h.stride2=strides[2];
            Check(h.stride2==4||h.stride2==12,"Unsupported skin layout");
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
            Check(offsets==strides && c.attributes.back()==std::array<int32_t,4>{13,6,4,2},"Declaration/stride or skin indices differ");
            std::vector<std::array<int32_t,4>> skinAttributes;
            for(const auto& a:c.attributes) if(a[3]==2) skinAttributes.push_back(a);
            const std::vector<std::array<int32_t,4>> expectedSkin=h.stride2==4?
                std::vector<std::array<int32_t,4>>{{13,6,4,2}}:
                std::vector<std::array<int32_t,4>>{{12,4,4,2},{13,6,4,2}};
            Check(skinAttributes==expectedSkin,"Unsupported skin declaration");
            c.layout_crc=Crc(std::string_view(reinterpret_cast<const char*>(c.attributes.data()),c.attributes.size()*16));
            auto indexBytes=uint64_t(h.index_count)*h.index_element_size; reserve(indexBytes); c.indices=Payload(mesh.at("indices"),indexBytes);
            for(size_t n=0;n<h.index_count;++n) { uint32_t index=0; std::memcpy(&index,c.indices.data()+n*h.index_element_size,h.index_element_size); Check(index<h.vertex_count,"Index outside vertex buffer"); }
            const auto& bones=mesh.at("bones"); Check(bones.is_array() && !bones.empty() && bones.size()<=256,"Invalid palette");
            for(const auto& b:bones) {
                auto donor=U(b.at("component")), index=U(b.at("index")); auto name=S(b.at("name"));
                Check(donor<info.component_names.size() && index<65536 && m.at("target").at("components").at(donor).at("bone_names").at(index)==name,"Bone identity differs");
                c.bones.push_back({donor,index,Crc(name)}); c.bone_names.push_back(name);
            }
            for(uint32_t n=0;n<h.vertex_count;++n) for(uint32_t k=0;k<4;++k) {
                auto b=c.streams[2][size_t(n)*h.stride2+h.stride2-4+k]; Check(b<c.bones.size(),"Skin index outside palette"); h.max_bone=std::max(h.max_bone,uint32_t(b));
            }
            if(h.stride2==12) for(uint32_t n=0;n<h.vertex_count;++n) {
                uint16_t w[4]; std::memcpy(w,c.streams[2].data()+size_t(n)*12,8);
                const uint32_t sum=uint32_t(w[0])+w[1]+w[2]+w[3];
                Check(sum>=64880 && sum<=66190,"Invalid skin weight sum");
            }
            const auto& draws=mesh.at("draws"); Check(draws.is_array() && !draws.empty() && draws.size()<=256,"Invalid draws");
            uint64_t end=0;
            for(const auto& d:draws) {
                auto start=U(d.at("start")),count=U(d.at("count")),donor=U(d.at("material_component")),slot=U(d.at("material_slot")); auto name=S(d.at("material_name"));
                Check(start==end && count && count%3==0 && donor<info.component_names.size() && slot<256 &&
                    m.at("target").at("components").at(donor).at("materials").at(slot)==name,"Invalid material draw"); end+=count;
                uint32_t mask=0;
                for(const auto& ref:d.at("textures")) {
                    auto id=U(ref); auto found=textures.find(id); uint32_t tid;
                    if(found==textures.end()) {
                        Check(textures.size()<32,"Appearance exceeds 32 texture bindings");
                        const auto& t=m.at("textures").at(id); BemTexture tex;
                        auto& ti=tex.info; ti.width=U(t.at("width")); ti.height=U(t.at("height")); ti.mip_count=U(t.at("mips"));
                        ti.create_format=int32_t(U(t.at("format"))); ti.create_srgb=t.at("srgb").get<bool>()?1:0;
                        uint32_t block=0,pixel=0,blockWidth=4; switch(ti.create_format) {case 4:pixel=4;break;case 63:pixel=1;Check(!ti.create_srgb,"R8 texture must be linear");break;case 10:case 26:block=8;break;case 12:case 25:case 27:block=16;break;case 48:block=16;break;case 49:block=16;blockWidth=5;break;case 50:block=16;blockWidth=6;break;default:Check(false,"Unsupported texture format");}
                        Check(ti.width && ti.height && ti.width<=32768 && ti.height<=32768 && (pixel || ti.create_format>=48 || (ti.width%4==0 && ti.height%4==0)) && ti.mip_count && ti.mip_count<=16,"Invalid texture dimensions");
                        uint64_t size=0; for(uint32_t level=0;level<ti.mip_count;++level) {
                            const uint64_t mipWidth=std::max(ti.width>>level,1u),mipHeight=std::max(ti.height>>level,1u);
                            size+=pixel?mipWidth*mipHeight*pixel:((mipWidth+blockWidth-1)/blockWidth)*((mipHeight+blockWidth-1)/blockWidth)*block;
                        }
                        Check(size<=64*MiB,"Texture exceeds 64 MiB"); reserve(size); tex.data=Payload(t.at("payload"),size);
                        ti.data_size=static_cast<uint32_t>(size); ti.reserved=2; tex.original_name=S(t.at("original_name")); tex.name=tex.original_name;
                        ti.explicit_slot=static_cast<int32_t>(Crc(tex.original_name));
                        tid=static_cast<uint32_t>(out.textures.size()); textures[id]=tid; out.textures.push_back(std::move(tex));
                    } else tid=found->second;
                    Check(!(mask&(uint32_t{1}<<tid)),"Duplicate draw texture"); mask|=uint32_t{1}<<tid;
                }
                c.draws.push_back({start,count,donor,slot,Crc(name),mask}); c.material_names.push_back(name);
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
bool LoadBem(const std::filesystem::path& path,BemPocData& out,std::string& error,std::string_view appearance) {
    out={}; BemPocData parsed; if(!File(path,error,[&](Container& c){c.Decode(appearance,parsed);})) return false;
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
        for(const auto& appearance:c.info.appearances) {
            checkpoint(); BemPocData parsed; c.cache.clear(); c.decoded=0;
            c.Decode(appearance,parsed);
        }
        c.cache.clear(); c.decoded=0;
        std::ofstream data(spool,std::ios::binary); Check(bool(data),"Cannot create staging payloads");
        std::vector<Entry> entries;
        auto append=[&](uint32_t codec,uint64_t decoded,const std::vector<uint8_t>& bytes) {
            Check(entries.size()<4096,"Installed payload directory exceeds limit");
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
        for(auto& mesh:manifest["meshes"]) {for(auto& stream:mesh["streams"]) copy(stream["payload"]); copy(mesh["indices"]);}
        for(auto& appearance:manifest["appearances"]) if(appearance.contains("preview")) copy(appearance["preview"]);
        BemJson changes=BemJson::array();
        // Identical texture descriptors share converted output; geometry/texture aliases are kept separate.
        std::map<std::string,BemJson> converted;
        for(auto& texture:manifest["textures"]) {
            checkpoint(); auto key=texture.dump(); auto found=converted.find(key);
            if(found!=converted.end()) { texture=found->second; continue; }
            auto original=texture; auto id=U(texture.at("payload"));
            c.cache.clear(); c.decoded=0;
            auto bytes=c.Payload(id,c.directory.at(id).decoded); c.cache.clear();
            Check(bytes.size()<=64*MiB,"Texture exceeds 64 MiB");
            transform(c.manifest,texture,bytes);
            Check(!bytes.empty() && bytes.size()<=64*MiB,"Converted texture exceeds 64 MiB");
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
        Header header{}; std::memcpy(header.magic,"BEM\0PKG\0",8); header.major=1;header.size=sizeof(Header);
        header.file=offset;header.manifest=json.size();header.count=static_cast<uint32_t>(entries.size());
        std::ofstream out(output,std::ios::binary); Check(bool(out),"Cannot create installed package");
        out.write(reinterpret_cast<const char*>(&header),sizeof(header)); out.write(json.data(),json.size());
        out.write(reinterpret_cast<const char*>(entries.data()),entries.size()*sizeof(Entry));
        std::ifstream payloads(spool,std::ios::binary); std::array<char,65536> buffer{};
        while(payloads) {checkpoint();payloads.read(buffer.data(),buffer.size());out.write(buffer.data(),payloads.gcount());}
        Check(payloads.eof(),"Staging read failed"); out.close(); Check(bool(out),"Installed package flush failed");
        for(const auto& appearance:c.info.appearances) {checkpoint();BemPocData parsed;std::string failure;
            Check(LoadBem(output,parsed,failure,appearance),"Installed validation: "+failure);}
        report=BemJson({{"package_id",c.info.package_id},{"character_id",c.info.character_id},{"name",c.info.name},
            {"default_appearance",c.info.default_appearance},{"appearances",c.info.appearances},{"textures",changes},{"bytes",offset},{"revision",1}}).dump();
    });
    std::error_code ignored; std::filesystem::remove(spool,ignored);
    if(!ok) std::filesystem::remove(output,ignored);
    return ok;
}
}
