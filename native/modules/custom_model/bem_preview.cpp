#include "bem_preview.h"
#include "bem_package_reader.h"
#include "../../shared/third_party/astcenc/Source/astcenc.h"
#define BCDEC_STATIC
#define BCDEC_IMPLEMENTATION
#include "../../shared/third_party/bcdec/bcdec.h"
#include <algorithm>
#include <cmath>
#include <chrono>
#include <charconv>
#include <cstring>
#include <fstream>
#include <set>
#include <map>
#include <stdexcept>
#if defined(_WIN32)
#ifndef NOMINMAX
#define NOMINMAX
#endif
#ifndef WIN32_LEAN_AND_MEAN
#define WIN32_LEAN_AND_MEAN
#endif
#include <windows.h>
#endif
namespace BetterEndfieldNext::CustomModel {
namespace {
using J=nlohmann::json;
uint64_t SceneBudget(bool desktop) {
    if(!desktop)return 192ull*1024*1024;
#if defined(_WIN32)
    MEMORYSTATUSEX memory{};memory.dwLength=sizeof(memory);
    if(GlobalMemoryStatusEx(&memory))
        return std::clamp<uint64_t>(memory.ullAvailPhys/4,256ull*1024*1024,4ull*1024*1024*1024);
#endif
    return 512ull*1024*1024;
}
constexpr uint32_t Sizes[]{4,2,1,1,2,2,1,1,2,2,4,4};
void Require(bool value,const std::string& message) {if(!value) throw std::runtime_error(message);}
#include "bem_preview_native.h"
#include "bem_preview_cache.h"
float Half(uint16_t value) {
    const unsigned sign=value>>15,exp=(value>>10)&31,mantissa=value&1023;
    const float result=exp==0?std::ldexp(float(mantissa),-24):exp==31?INFINITY:std::ldexp(float(1024+mantissa),int(exp)-25);
    return sign?-result:result;
}
float Number(const uint8_t* p,int format) {
    float f;uint16_t h;
    switch(format) {
    case 0:std::memcpy(&f,p,4);return f;
    case 1:std::memcpy(&h,p,2);return Half(h);
    case 2:return *p/255.f;
    case 3:return std::max(-1.f,float(int8_t(*p))/127.f);
    default:throw std::runtime_error("Unsupported preview vertex format");
    }
}
std::vector<float> Attribute(const BemComponent& c,int semantic,int dimensions) {
    std::array<uint32_t,3> offsets{};
    const uint32_t strides[]{c.info.stride0,c.info.stride1,c.info.stride2};
    for(const auto& a:c.attributes) {
        const auto [sem,fmt,dim,stream]=a;
        Require(fmt>=0 && fmt<12 && stream>=0 && stream<3,"Invalid preview vertex layout");
        const auto offset=offsets[stream];offsets[stream]+=Sizes[fmt]*dim;
        if(sem!=semantic) continue;
        Require(dim>=dimensions,"Preview vertex attribute has too few dimensions");
        std::vector<float> result(size_t(c.info.vertex_count)*dimensions);
        for(uint32_t v=0;v<c.info.vertex_count;++v) for(int k=0;k<dimensions;++k) {
            const auto at=size_t(v)*strides[stream]+offset+Sizes[fmt]*k;
            Require(at+Sizes[fmt]<=c.streams[stream].size(),"Preview vertex stream is truncated");
            const float value=Number(c.streams[stream].data()+at,fmt);
            Require(std::isfinite(value),"Preview vertex value is not finite");result[size_t(v)*dimensions+k]=value;
        }
        return result;
    }
    if(semantic==0) throw std::runtime_error("Preview mesh has no positions");
    return {};
}
std::vector<uint8_t> SampleTexture(const uint8_t* source,int format,uint32_t width,uint32_t height,uint32_t outWidth,uint32_t outHeight) {
    std::vector<uint8_t> rgba(size_t(outWidth)*outHeight*4);
    uint8_t block[64]{};size_t cached=SIZE_MAX;
    const size_t blockSize=(format==10 || format==26)?8:16;
    for(uint32_t y=0;y<outHeight;++y) for(uint32_t x=0;x<outWidth;++x) {
        const uint32_t sx=uint64_t(x)*width/outWidth,sy=uint64_t(y)*height/outHeight;
        auto* dst=rgba.data()+(size_t(y)*outWidth+x)*4;
        if(format==4) {std::memcpy(dst,source+(size_t(sy)*width+sx)*4,4);continue;}
        const auto index=size_t(sy/4)*((width+3)/4)+sx/4;
        if(index!=cached) {
            const auto* input=source+index*blockSize;
            switch(format) {
            case 10:bcdec_bc1(input,block,16);break;
            case 12:bcdec_bc3(input,block,16);break;
            case 25:bcdec_bc7(input,block,16);break;
            case 26:bcdec_bc4(input,block,4);break;
            case 27:bcdec_bc5(input,block,8);break;
            default:throw std::runtime_error("Unsupported preview texture format");
            }
            cached=index;
        }
        const auto pixel=(sy%4)*4+sx%4;
        if(format==26) {dst[0]=dst[1]=dst[2]=block[pixel];dst[3]=255;}
        else if(format==27) {dst[0]=block[pixel*2];dst[1]=block[pixel*2+1];dst[2]=0;dst[3]=255;}
        else std::memcpy(dst,block+pixel*4,4);
    }
    return rgba;
}
// Keep one compressed block row and the small output image, even for a 12K atlas.
std::vector<uint8_t> SampleDeferredTexture(const BemPayloadSource& source,const BemTexture& texture,
    uint64_t mipOffset,uint32_t width,uint32_t height,uint32_t outWidth,uint32_t outHeight,
    const std::function<void()>& checkpoint,std::string& error) {
    const int format=texture.info.create_format;
    const bool astc=format>=48 && format<=50;
    const uint32_t block=astc?uint32_t(format-44):format==4?1:4,stride=format==4?4:(format==10||format==26)?8:16;
    std::unique_ptr<astcenc_context,decltype(&astcenc_context_free)> context(nullptr,astcenc_context_free);
    std::vector<uint8_t> astcRow;
    if(astc) {
        astcenc_config config{};
        Require(astcenc_config_init(ASTCENC_PRF_LDR,block,block,1,ASTCENC_PRE_FAST,ASTCENC_FLG_DECOMPRESS_ONLY,&config)==ASTCENC_SUCCESS,"Cannot configure ASTC preview decoder");
        astcenc_context* raw=nullptr;
        Require(astcenc_context_alloc(&config,1,&raw)==ASTCENC_SUCCESS,"Cannot allocate ASTC preview decoder");
        context.reset(raw);astcRow.resize(size_t(width)*block*4);
    }
    const size_t rowBytes=size_t((width+block-1)/block)*stride;
    const uint64_t mipBytes=uint64_t(rowBytes)*((height+block-1)/block);
    std::vector<uint8_t> row(rowBytes),rgba(size_t(outWidth)*outHeight*4);
    uint64_t offset=0;size_t filled=0;uint32_t rowIndex=0,nextY=0;
    Require(VisitBemTexturePayload(source,texture,[&](std::span<const uint8_t> bytes){
        checkpoint();const auto start=std::max(offset,mipOffset),end=std::min(offset+bytes.size(),mipOffset+mipBytes);
        size_t at=start>offset?size_t(start-offset):0;
        if(start<end)while(at<size_t(end-offset)) {
            const auto count=std::min(rowBytes-filled,size_t(end-offset)-at);
            std::memcpy(row.data()+filled,bytes.data()+at,count);filled+=count;at+=count;
            if(filled!=rowBytes)continue;
            if(nextY<outHeight && (uint64_t(nextY)*height/outHeight)/block==rowIndex) {
                std::vector<uint8_t> pixels;
                if(astc) {
                    void* slice=astcRow.data();astcenc_image image{width,block,1,ASTCENC_TYPE_U8,&slice};
                    astcenc_swizzle swizzle{ASTCENC_SWZ_R,ASTCENC_SWZ_G,ASTCENC_SWZ_B,ASTCENC_SWZ_A};
                    Require(astcenc_decompress_image(context.get(),row.data(),row.size(),&image,&swizzle,0)==ASTCENC_SUCCESS,"Cannot decode ASTC preview block row");
                    Require(astcenc_decompress_reset(context.get())==ASTCENC_SUCCESS,"Cannot reset ASTC preview decoder");
                    pixels=SampleTexture(astcRow.data(),4,width,block,outWidth,block);
                } else pixels=SampleTexture(row.data(),format,width,block,outWidth,block);
                while(nextY<outHeight && (uint64_t(nextY)*height/outHeight)/block==rowIndex) {
                    const auto sy=(uint64_t(nextY)*height/outHeight)%block;
                    std::memcpy(rgba.data()+size_t(nextY)*outWidth*4,pixels.data()+sy*outWidth*4,size_t(outWidth)*4);++nextY;
                }
            }
            filled=0;++rowIndex;
        }
        offset+=bytes.size();
    },error,26),error);
    Require(nextY==outHeight && !filled,"Incomplete sampled texture mip");return rgba;
}
int AlbedoScore(const BemTexture& texture) {
    std::string name=texture.name+" "+texture.original_name;
    std::transform(name.begin(),name.end(),name.begin(),[](unsigned char c){return char(std::tolower(c));});
    auto token=[&](const std::string& value){size_t at=0;while((at=name.find(value,at))!=name.npos){const auto end=at+value.size();if(end==name.size() || name[end]=='_' || name[end]=='.' || name[end]==' ')return true;++at;}return false;};
    if(token("_d") || name.find("albedo")!=name.npos || name.find("diffuse")!=name.npos || name.find("basecolor")!=name.npos) return 2;
    if(token("_n") || name.find("normal")!=name.npos || token("_e") || token("_m")) return -1;
    return 0;
}
J Family(int format) {
    switch(format) {
    case 4:return {{"family","rgba32"},{"block",1},{"bytes",4}};
    case 10:return {{"family","bc1"},{"block",4},{"bytes",8}};
    case 12:return {{"family","bc3"},{"block",4},{"bytes",16}};
    case 25:return {{"family","bc7"},{"block",4},{"bytes",16}};
    case 26:return {{"family","bc4"},{"block",4},{"bytes",8}};
    case 27:return {{"family","bc5"},{"block",4},{"bytes",16}};
    case 48:return {{"family","astc"},{"block",4},{"bytes",16}};
    case 49:return {{"family","astc"},{"block",5},{"bytes",16}};
    case 50:return {{"family","astc"},{"block",6},{"bytes",16}};
    default:return {{"family","unknown"},{"block",1},{"bytes",0}};
    }
}
}
bool ExportBemPreview(const std::filesystem::path& package,const std::filesystem::path& output,
    std::string_view requested,std::string_view parameters,std::string_view resource,
    const std::function<void()>& checkpoint,J& report,std::string& error,const BemPreviewGpu& gpu) {
    const uint64_t Budget=SceneBudget(gpu.desktop);
    const auto started=std::chrono::steady_clock::now();
    auto elapsed=[&]{return std::chrono::duration_cast<std::chrono::milliseconds>(std::chrono::steady_clock::now()-started).count();};
    bool owned=false;
    try {
        checkpoint();Require(!std::filesystem::exists(output),"Preview output must be a new directory");
        BemPackageInfo info;Require(ReadBemPackageInfo(package,info,error),error);
        auto manifest=J::parse(info.manifest_json);
        std::string selected(requested),savedParameters(parameters),resourceId(resource),effectiveParameters(parameters);
        // Saved preferences can retain sliders absent after a package downgrade.
        // Validate that memory separately; only declared sliders reach the runtime loader.
        std::set<std::string> declared,seen;
        for(const auto& item:manifest.value("parameters",J::array()))declared.insert(item.at("id").get<std::string>());
        std::string known,unknown;std::string_view remaining=parameters;
        while(!remaining.empty()) {
            const auto end=remaining.find('&');const auto pair=remaining.substr(0,end);const auto colon=pair.find(':');
            Require(colon>0 && colon<pair.size()-1,"Invalid parameter selection");
            const auto id=std::string(pair.substr(0,colon));const auto value=pair.substr(colon+1);
            auto alpha=[](char c){return (c>='A'&&c<='Z')||(c>='a'&&c<='z')||(c>='0'&&c<='9');};
            Require(id.size()<=96 && alpha(id.front()) && std::all_of(id.begin(),id.end(),[&](char c){return alpha(c)||c=='_'||c=='.'||c=='-';}),"Invalid parameter id");
            uint32_t tick=0;auto parsed=std::from_chars(value.data(),value.data()+value.size(),tick);
            Require(parsed.ec==std::errc{} && parsed.ptr==value.data()+value.size() && tick<=1000,"Invalid parameter tick");
            Require(seen.insert(id).second,"Duplicate parameter selection");
            auto& target=declared.contains(id)?known:unknown;
            if(!target.empty())target+='&';target+=id+":"+std::to_string(tick);
            remaining=end==remaining.npos?std::string_view{}:remaining.substr(end+1);
            Require(end==std::string_view::npos || !remaining.empty(),"Trailing parameter separator");
        }
        BemSelection state;Require(ResolveBemSelection(info,selected,known,state,error),error);
        selected=state.options;savedParameters=state.parameters_saved;effectiveParameters=state.parameters;
        if(!unknown.empty()){if(!savedParameters.empty())savedParameters+='&';savedParameters+=unknown;}
        if(resourceId.empty() && !info.resources.empty()) {
            auto found=std::find_if(info.resources.begin(),info.resources.end(),[&](const auto& r){return r.id==info.world_resource;});
            resourceId=(found==info.resources.end()?info.resources.front():*found).id;
        }
        BemPocData data;
        char prefix[2]{};{std::ifstream source(package,std::ios::binary);source.read(prefix,2);}
        // ZIP entries have no raw on-disk payload extent; use the container loader there.
        const bool defer=!(prefix[0]=='P' && prefix[1]=='K');
        Require(LoadBem(package,data,error,selected,nullptr,false,true,effectiveParameters,Budget,defer,resourceId),error);
        const auto loadedMs=elapsed();
        std::map<uint32_t,std::vector<std::string>> drawSegments;
        std::map<uint32_t,std::vector<J>> drawBodies;
        if(manifest.contains("composition_segments")) {
            BemPackageReader reader;J snapshot;
            Require(reader.Open(package,error),error);Require(reader.Snapshot(selected,snapshot,error),error);
            for(const auto& op:snapshot.at("operations")) if(op.at("operation")=="replace")
                for(const auto& draw:snapshot.at("meshes").at(op.at("mesh").get<size_t>()).at("draws"))
                {
                    const auto target=op.at("target").get<uint32_t>();
                    drawSegments[target].push_back(draw.value("composition_segment",std::string{}));
                    drawBodies[target].push_back({{"bodyDraw",draw.value("composition_body_draw",std::string{})},{"bodyRegion",draw.value("composition_body_region",std::string{})},{"bodyTriangles",draw.value("composition_triangles",J())}});
                }
        }
        checkpoint();Require(std::filesystem::create_directories(output),"Cannot create preview directory");owned=true;
        uint64_t bytes=0;size_t sequence=0;
        auto blob=[&](const void* ptr,size_t length) {
            checkpoint();Require(length<=Budget && bytes<=Budget-length,"Preview data exceeds available-memory budget");bytes+=length;
            auto name=std::to_string(sequence++)+".bin";std::ofstream stream(output/name,std::ios::binary);
            stream.write(static_cast<const char*>(ptr),std::streamsize(length));stream.close();Require(bool(stream),"Cannot write preview data");return name;
        };
        auto textureBlob=[&](const BemTexture& texture,uint64_t offset,size_t length) {
            checkpoint();Require(length<=Budget && bytes<=Budget-length,"Preview data exceeds available-memory budget");
            bytes+=length;auto name=std::to_string(sequence++)+".bin";
            std::ofstream stream(output/name,std::ios::binary);uint64_t position=0,written=0;
            if(texture.Deferred()) {
                Require(data.payload_source && VisitBemTexturePayload(*data.payload_source,texture,[&](std::span<const uint8_t> chunk){
                    checkpoint();const auto start=std::max(position,offset),end=std::min(position+chunk.size(),offset+length);
                    if(start<end){stream.write(reinterpret_cast<const char*>(chunk.data()+size_t(start-position)),std::streamsize(end-start));written+=end-start;}
                    position+=chunk.size();
                },error,26),error);
            } else {
                Require(offset<=texture.data.size() && length<=texture.data.size()-offset,"Truncated preview texture");
                stream.write(reinterpret_cast<const char*>(texture.data.data()+offset),std::streamsize(length));written=length;
            }
            stream.close();Require(bool(stream) && written==length,"Cannot write compressed preview texture");return name;
        };
        J scene={{"schema",1},{"package",{{"name",info.name},{"formatVersion","1."+std::to_string(info.minor)}}},
            {"resources",J::array()},{"resource",resourceId},{"selection",selected},{"parameters",savedParameters},
            {"meshes",J::array()},{"textures",J::array()},{"channels",J::array()},{"warnings",J::array()}};
        scene["controls"]={{"composable",info.minor>0},{"optionGroups",manifest.value("option_groups",J::array())},
            {"appearances",manifest.value("appearances",J::array())},{"parameters",manifest.value("parameters",J::array())},
            {"availableGroups",state.available_groups},{"availableParameters",state.available_parameters}};
        scene["compositionSegments"]=manifest.value("composition_segments",J::array());
        for(const auto& r:info.resources) scene["resources"].push_back({{"id",r.id},{"name",r.name.empty()?r.asset_path:r.name},{"path",r.asset_path}});
        std::array<float,3> minimum{INFINITY,INFINITY,INFINITY},maximum{-INFINITY,-INFINITY,-INFINITY};
        std::set<size_t> usedTextures;
        const BemResourceInfo* active=nullptr;for(const auto& r:info.resources) if(r.id==resourceId) active=&r;
        NativePreviewHead head;head.Load(gpu.native_assets,info,active,resourceId,data,gpu.desktop);
        scene["nativeCompletion"]={{"status",head.diagnostic},{"reason",head.reason},{"resourceVersion",head.version},{"meshes",0},{"textures",head.textures.size()}};
        auto globalId=[&](uint32_t id){return active && id<active->component_ids.size()?active->component_ids[id]:id;};
        for(const auto& c:data.components) {
            checkpoint();if(c.info.flags&kComponentFlagHidden)continue;
            const auto nativeGlobal=globalId(c.info.component_id);const auto nativeRenderer=head.Renderer(nativeGlobal);
            if(c.info.flags&kComponentFlagNoGeometry) {
                if(!nativeRenderer || head.Projected(*nativeRenderer))continue;
                try {
                    const auto& mesh=head.pack.at("meshes").at(nativeRenderer->at("previewMesh").get<size_t>());
                    const auto vertices=mesh.at("vertex_count").get<size_t>();Require(vertices && vertices<=1048576,"Invalid native preview vertices");
                    auto positions=head.Read(mesh.at("payloads").at("positions"),vertices*12);
                    auto uvs=head.Read(mesh.at("payloads").at("uvs"),vertices*8);
                    size_t count=0;for(const auto& sub:mesh.at("submeshes"))count+=sub.at("index_count").get<size_t>();
                    Require(count && count<=16777216 && count%3==0,"Invalid native preview indices");
                    auto indices=head.Read(mesh.at("payloads").at("indices"),count*4);
                    for(size_t i=0;i<indices.size();i+=4){uint32_t index;std::memcpy(&index,indices.data()+i,4);Require(index<vertices,"Native preview index outside mesh");}
                    for(size_t v=0;v<positions.size();v+=4){float f;std::memcpy(&f,positions.data()+v,4);Require(std::isfinite(f),"Nonfinite native preview position");const auto k=(v/4)%3;minimum[k]=std::min(minimum[k],f);maximum[k]=std::max(maximum[k],f);}
                    J item={{"index",scene["meshes"].size()},{"name",info.component_names[nativeGlobal]},{"component",nativeGlobal},{"native",true},
                        {"vertexCount",vertices},{"indexSize",4},{"positionsBlob",blob(positions.data(),positions.size())},{"uvsBlob",blob(uvs.data(),uvs.size())},{"draws",J::array()}};
                    const auto indexBlob=blob(indices.data(),indices.size());size_t first=0,slot=0;
                    for(const auto& sub:mesh.at("submeshes")) {
                        uint64_t mask=0;for(const auto& override:c.keep_material_overrides)if(override.material_slot==slot)mask=override.textures;
                        const int texture=head.Texture(nativeRenderer,uint32_t(slot),mask,data);J refs=J::array();if(texture>=0){refs.push_back(texture);usedTextures.insert(size_t(texture));}
                        item["draws"].push_back({{"indicesBlob",indexBlob},{"indexSize",4},{"first",first},{"count",sub.at("index_count")},{"textures",refs},{"uvTransform",head.Uv(nativeRenderer,uint32_t(slot))},{"when",true}});
                        first+=sub.at("index_count").get<size_t>();++slot;
                    }
                    scene["meshes"].push_back(std::move(item));scene["nativeCompletion"]["meshes"]=scene["nativeCompletion"]["meshes"].get<int>()+1;
                }catch(const std::exception&){scene["nativeCompletion"]["status"]="partial";}
                continue;
            }
            auto positions=Attribute(c,0,3),uv=Attribute(c,4,2);
            for(size_t v=0;v<positions.size();v+=3)for(size_t k=0;k<3;++k) {minimum[k]=std::min(minimum[k],positions[v+k]);maximum[k]=std::max(maximum[k],positions[v+k]);}
            const auto id=c.info.component_id;
            const auto global=active && id<active->component_ids.size()?active->component_ids[id]:id;
            std::string name=global<info.component_names.size()?info.component_names[global]:"component "+std::to_string(global);
            J mesh={{"index",scene["meshes"].size()},{"name",name},{"component",global},
                {"vertexCount",c.info.vertex_count},{"indexSize",c.info.index_element_size},{"positionsBlob",blob(positions.data(),positions.size()*4)},
                {"uvsBlob",uv.empty()?J(nullptr):J(blob(uv.data(),uv.size()*4))},{"draws",J::array()}};
            const auto indices=blob(c.indices.data(),c.indices.size());
            size_t drawIndex=0;
            for(const auto& draw:c.draws) {
                J textures=J::array();
                int best=-1,score=-1;
                for(size_t t=0;t<data.textures.size() && t<64;++t) if(draw.textures&(uint64_t(1)<<t)) {
                    const auto candidate=AlbedoScore(data.textures[t]);if(candidate>score) {best=int(t);score=candidate;}
                }
                const auto donor=head.Renderer(globalId(draw.material_component));
                if(donor && head.Base(*donor,draw.material_slot))best=head.Texture(donor,draw.material_slot,draw.textures,data);
                if(best>=0) {textures.push_back(best);usedTextures.insert(size_t(best));}
                mesh["draws"].push_back({{"indicesBlob",indices},{"indexSize",c.info.index_element_size},
                    {"first",draw.start},{"count",draw.count},{"textures",textures},{"uvTransform",head.Uv(donor,draw.material_slot)},{"when",true},
                    {"segment",drawSegments.contains(global) && drawIndex<drawSegments.at(global).size()?drawSegments.at(global)[drawIndex]:std::string{}}});
                if(drawBodies.contains(global) && drawIndex<drawBodies.at(global).size())
                    for(const auto& [key,value]:drawBodies.at(global)[drawIndex].items())mesh["draws"].back()[key]=value;
                ++drawIndex;
            }
            scene["meshes"].push_back(std::move(mesh));
        }
        scene["orientation"]={{"rotated",!scene["meshes"].empty() && maximum[2]-minimum[2]>(maximum[1]-minimum[1])*1.05f}};
        const auto geometryMs=elapsed();
        // Decode only referenced texture payloads; ship a bounded mip, never full RGBA chains.
        uint64_t texturePixels=0;
        const uint32_t fallbackSide=gpu.max_texture_size?std::min(4096u,gpu.max_texture_size):1024u;
        const uint64_t pixelBudget=(gpu.max_texture_size?16ull:8ull)*1024*1024;
        PreviewTextureCache cache(gpu.texture_cache);
        std::map<uint32_t,J> origins;
        if(gpu.composition_draft) for(const auto& texture:manifest.value("textures",J::array()))
            if(texture.contains("preview_source")) origins[texture.at("payload").get<uint32_t>()]=texture.at("preview_source");
        size_t cacheHits=0;
        for(size_t t=0;t<data.textures.size();++t) {
            const auto& texture=data.textures[t];const auto family=Family(texture.info.create_format);
            J item={{"index",t},{"name",texture.name},{"family",family.at("family")},{"srgb",texture.info.create_srgb!=0},{"blob",nullptr},{"sourceWidth",texture.info.width},{"sourceHeight",texture.info.height}};
            J cacheKey;
            if(!gpu.texture_cache.empty() && usedTextures.contains(t) && texture.Deferred()) {
                const auto remaining=std::distance(usedTextures.lower_bound(t),usedTextures.end());
                J source;
                if(origins.contains(texture.payload_id))source=origins.at(texture.payload_id);
                else {
                    const auto utf8=package.u8string();
                    source={{"path",std::string(utf8.begin(),utf8.end())},{"size",std::filesystem::file_size(package)},
                        {"time",std::filesystem::last_write_time(package).time_since_epoch().count()},{"payload",texture.payload_id}};
                }
                cacheKey={{"schema",1},{"source",source},{"format",texture.info.create_format},{"srgb",texture.info.create_srgb},
                    {"width",texture.info.width},{"height",texture.info.height},{"mips",texture.info.mip_count},{"size",texture.info.data_size},
                    {"bc7",gpu.bc7},{"astc",gpu.astc},{"max_side",gpu.max_texture_size},
                    {"allowance",std::min((pixelBudget-texturePixels)/std::max<int64_t>(1,remaining),(Budget-bytes)/4)}};
                if(const auto* hit=cache.Find(cacheKey);hit && hit->size<=Budget-bytes) {
                    auto name=std::to_string(sequence++)+".bin";
                    std::error_code copyError;std::filesystem::copy_file(hit->data,output/name,copyError);
                    if(!copyError) {
                        item=hit->item;item["index"]=t;item["name"]=texture.name;item["blob"]=name;
                        item["cacheKey"]=cacheKey.dump();bytes+=hit->size;++cacheHits;
                        if(!item.value("gpuCompressed",false))texturePixels+=item.at("width").get<uint64_t>()*item.at("height").get<uint64_t>();
                        scene["textures"].push_back(std::move(item));continue;
                    }
                }
            }
            // Preserve authored BC7 or ASTC blocks when this WebView can sample them directly.
            // Only use an authored mip; never recompress or resize this path.
            if(usedTextures.contains(t) && ((texture.info.create_format==25 && gpu.bc7) || (family.at("family")=="astc" && gpu.astc)) && gpu.max_texture_size) {
                uint32_t w=texture.info.width,h=texture.info.height,level=0;uint64_t offset=0;
                const auto block=family.at("block").get<uint32_t>();
                auto size=[&]{return uint64_t((w+block-1)/block)*((h+block-1)/block)*16;};
                while((w>gpu.max_texture_size || h>gpu.max_texture_size || size()>Budget-bytes) && level+1<texture.info.mip_count){offset+=size();w=std::max(1u,w/2);h=std::max(1u,h/2);++level;}
                if(w<=gpu.max_texture_size && h<=gpu.max_texture_size && size()<=Budget-bytes) {
                    item["blob"]=textureBlob(texture,offset,size());item["width"]=w;item["height"]=h;item["gpuCompressed"]=true;item["mip"]=level;item["block"]=block;
                    if(!cacheKey.is_null())item["cacheKey"]=cacheKey.dump();
                    cache.Store(cacheKey,item,output/item["blob"].get<std::string>());
                    scene["textures"].push_back(std::move(item));continue;
                }
            }
            if(usedTextures.contains(t) && family.at("family")!="unknown" && texture.info.data_size<=512u*1024*1024) {
                checkpoint();std::vector<uint8_t> decoded;
                const bool stream=texture.Deferred() && data.payload_source;
                if(!stream) {
                    if(texture.info.data_size>64u*1024*1024) {scene["warnings"].push_back("Texture exceeds non-streaming preview budget: "+texture.name);scene["textures"].push_back(std::move(item));continue;}
                    if(texture.Deferred())Require(data.payload_source && DecodeBemTexturePayload(*data.payload_source,texture,decoded,error),error);
                    else decoded=texture.data;
                }
                // Share the remaining software budget, so a large body atlas cannot
                // consume every pixel and starve iris/face textures exported later.
                const auto remainingTextures=std::distance(usedTextures.lower_bound(t),usedTextures.end());
                const uint64_t allowance=std::min((pixelBudget-texturePixels)/std::max<int64_t>(1,remainingTextures),(Budget-bytes)/4);
                uint32_t w=texture.info.width,h=texture.info.height;size_t offset=0;
                const auto block=family.at("block").get<size_t>(),stride=family.at("bytes").get<size_t>();
                auto size=[&]{return size_t((w+block-1)/block)*((h+block-1)/block)*stride;};
                uint32_t level=0;while((std::max(w,h)>fallbackSide || uint64_t(w)*h>allowance) && level+1<texture.info.mip_count) {offset+=size();w=std::max(1u,w/2);h=std::max(1u,h/2);++level;}
                uint32_t outWidth=w,outHeight=h;
                while((outWidth>1 || outHeight>1) && (std::max(outWidth,outHeight)>fallbackSide || uint64_t(outWidth)*outHeight>allowance)) {outWidth=std::max(1u,outWidth/2);outHeight=std::max(1u,outHeight/2);}
                if(texturePixels+uint64_t(outWidth)*outHeight<=pixelBudget && uint64_t(outWidth)*outHeight*4<=Budget-bytes && offset<=texture.info.data_size && size()<=texture.info.data_size-offset && (stream || family.at("family")!="astc" || uint64_t(w)*h<=16ull*1024*1024)) {
                    if(stream) {
                        auto rgba=SampleDeferredTexture(*data.payload_source,texture,offset,w,h,outWidth,outHeight,checkpoint,error);
                        item["blob"]=blob(rgba.data(),rgba.size());item["family"]="rgba32";
                    } else if(family.at("family")=="astc") {
                        astcenc_config config{};
                        Require(astcenc_config_init(ASTCENC_PRF_LDR,unsigned(block),unsigned(block),1,ASTCENC_PRE_FAST,ASTCENC_FLG_DECOMPRESS_ONLY,&config)==ASTCENC_SUCCESS,"Cannot configure ASTC preview decoder");
                        astcenc_context* raw=nullptr;
                        Require(astcenc_context_alloc(&config,1,&raw)==ASTCENC_SUCCESS,"Cannot allocate ASTC preview decoder");
                        std::unique_ptr<astcenc_context,decltype(&astcenc_context_free)> context(raw,astcenc_context_free);
                        std::vector<uint8_t> rgba(size_t(w)*h*4);void* slice=rgba.data();
                        astcenc_image image{w,h,1,ASTCENC_TYPE_U8,&slice};
                        astcenc_swizzle swizzle{ASTCENC_SWZ_R,ASTCENC_SWZ_G,ASTCENC_SWZ_B,ASTCENC_SWZ_A};
                        Require(astcenc_decompress_image(context.get(),decoded.data()+offset,size(),&image,&swizzle,0)==ASTCENC_SUCCESS,"Cannot decode ASTC preview texture");
                        if(w!=outWidth || h!=outHeight) rgba=SampleTexture(rgba.data(),4,w,h,outWidth,outHeight);
                        item["blob"]=blob(rgba.data(),rgba.size());item["family"]="rgba32";
                    } else if(w!=outWidth || h!=outHeight) {
                        auto rgba=SampleTexture(decoded.data()+offset,texture.info.create_format,w,h,outWidth,outHeight);
                        item["blob"]=blob(rgba.data(),rgba.size());item["family"]="rgba32";
                    } else item["blob"]=blob(decoded.data()+offset,size());
                    texturePixels+=uint64_t(outWidth)*outHeight;item["width"]=outWidth;item["height"]=outHeight;
                    item["block"]=block;
                }
            }
            if(usedTextures.contains(t) && item["blob"].is_null()) scene["warnings"].push_back("Texture unavailable or exceeds preview budget: "+texture.name);
            if(!item["blob"].is_null()) {
                if(!cacheKey.is_null())item["cacheKey"]=cacheKey.dump();
                cache.Store(cacheKey,item,output/item["blob"].get<std::string>());
            }
            scene["textures"].push_back(std::move(item));
        }
        if(manifest.contains("drivers") && !manifest["drivers"].empty()) scene["warnings"].push_back("Driver animations are not played in preview");
        if(manifest.contains("root_offset")) scene["warnings"].push_back("Game root offset is not applied in preview");
        scene["stats"]={{"bytes",bytes},{"meshes",scene["meshes"].size()},{"textures",usedTextures.size()}};
        std::ofstream json(output/"scene.json",std::ios::binary);json<<scene.dump();json.close();Require(bool(json),"Cannot write preview scene");
        report={{"meshes",scene["meshes"].size()},{"bytes",bytes},{"resource",resourceId},{"texture_cache_hits",cacheHits},{"scene_budget_bytes",Budget},{"timings_ms",{{"load",loadedMs},{"geometry",geometryMs-loadedMs},{"textures",elapsed()-geometryMs},{"total",elapsed()}}}};return true;
    } catch(const std::exception& e) {error=e.what();if(owned) {std::error_code ignored;std::filesystem::remove_all(output,ignored);}return false;}
}
}
