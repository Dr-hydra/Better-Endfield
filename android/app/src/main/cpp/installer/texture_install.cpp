#include "texture_install.h"
#include "astcenc.h"
#define BCDEC_IMPLEMENTATION
#include "bcdec.h"
#include <algorithm>
#include <cmath>
#include <cstring>
#include <memory>
#include <mutex>
#include <stdexcept>

namespace betterendfield {
using J=BetterEndfield::CustomModel::BemJson;
namespace {
std::mutex activeMutex;
astcenc_context* activeContext=nullptr;
thread_local const std::function<void(float)>* activeProgress=nullptr;
void reportProgress(float percent) {if(activeProgress) (*activeProgress)(percent);}
struct CompressionScope {
    CompressionScope(astcenc_context* context,const std::function<void(float)>& progress) {
        std::lock_guard<std::mutex> lock(activeMutex);activeContext=context;activeProgress=&progress;
    }
    ~CompressionScope() {
        std::lock_guard<std::mutex> lock(activeMutex);activeContext=nullptr;activeProgress=nullptr;
    }
};
void require(bool value,const char* message) {if(!value) throw std::runtime_error(message);}
void check(astcenc_error error) {if(error!=ASTCENC_SUCCESS) throw std::runtime_error(astcenc_get_error_string(error));}
size_t mipSize(int format,unsigned w,unsigned h) {
    switch(format) {
    case 4:return size_t(w)*h*4;
    case 63:return size_t(w)*h;
    case 10:case 26:return size_t((w+3)/4)*((h+3)/4)*8;
    case 12:case 25:case 27:return size_t((w+3)/4)*((h+3)/4)*16;
    case 48:case 49:case 50: {unsigned b=unsigned(format-44);return size_t((w+b-1)/b)*((h+b-1)/b)*16;}
    default:throw std::runtime_error("Unsupported source texture format");
    }
}
std::vector<uint8_t> decode(const uint8_t* source,int format,unsigned w,unsigned h,const std::function<void()>& checkpoint) {
    std::vector<uint8_t> pixels(size_t(w)*h*4,255);
    if(format==4) {std::memcpy(pixels.data(),source,pixels.size());return pixels;}
    if(format==63) {for(size_t i=0;i<size_t(w)*h;++i) {pixels[i*4]=source[i];pixels[i*4+1]=pixels[i*4+2]=0;}return pixels;}
    unsigned step=(format==10||format==26)?8:16;
    for(unsigned y=0;y<h;y+=4) {
        checkpoint();
        for(unsigned x=0;x<w;x+=4,source+=step) {
            alignas(16) uint8_t block[64]{};
            switch(format) {
            case 10:bcdec_bc1(source,block,16);break;
            case 12:bcdec_bc3(source,block,16);break;
            case 25:require(source[0]!=0,"Invalid BC7 mode");bcdec_bc7(source,block,16);break;
            case 26:bcdec_bc4(source,block,4);break;
            case 27:bcdec_bc5(source,block,8);break;
            default:throw std::runtime_error("Cannot decode this source format");
            }
            for(unsigned by=0;by<4 && y+by<h;++by) for(unsigned bx=0;bx<4 && x+bx<w;++bx) {
                auto* p=pixels.data()+(size_t(y+by)*w+x+bx)*4;unsigned i=by*4+bx;
                if(format==26) {p[0]=block[i];p[1]=p[2]=0;}
                else if(format==27) {p[0]=block[i*2];p[1]=block[i*2+1];p[2]=0;}
                else std::memcpy(p,block+i*4,4);
            }
        }
    }
    return pixels;
}
// Box-filtered 1/factor decode, processed in row bands so an oversized single-level
// texture (BEM 1.2 allows 256 MiB) never needs a full-resolution RGBA copy.
std::vector<uint8_t> decodeScaled(const uint8_t* source,int format,unsigned w,unsigned h,unsigned factor,
    const std::function<void()>& checkpoint) {
    if(factor==1) return decode(source,format,w,h,checkpoint);
    const unsigned ow=std::max(w/factor,1u),oh=std::max(h/factor,1u),band=std::max(factor,4u);
    std::vector<uint8_t> out(size_t(ow)*oh*4);
    for(unsigned y=0;y<h;y+=band) {
        const unsigned rows=std::min(band,h-y);
        const auto pixels=decode(source+mipSize(format,w,y),format,w,rows,checkpoint);
        for(unsigned oy=y/factor;oy<oh && oy*factor<y+rows;++oy) for(unsigned ox=0;ox<ow;++ox) {
            unsigned sum[4]{},count=0;
            for(unsigned dy=0;dy<factor;++dy) {
                const unsigned sy=oy*factor+dy; if(sy<y || sy>=y+rows) continue;
                for(unsigned dx=0;dx<factor;++dx) {
                    const unsigned sx=ox*factor+dx; if(sx>=w) continue;
                    const auto* p=pixels.data()+(size_t(sy-y)*w+sx)*4;
                    for(unsigned c=0;c<4;++c) sum[c]+=p[c];
                    ++count;
                }
            }
            auto* q=out.data()+(size_t(oy)*ow+ox)*4;
            for(unsigned c=0;c<4;++c) q[c]=uint8_t((sum[c]+count/2)/count);
        }
    }
    return out;
}
constexpr size_t kInstalledTextureLimit=64ull*1024*1024;
constexpr unsigned kInstalledTextureSize=8192;
}
void CancelTextureCompression() {
    std::lock_guard<std::mutex> lock(activeMutex);
    if(activeContext) astcenc_compress_cancel(activeContext);
}
void ConvertInstalledTexture(const J& manifest,J& texture,std::vector<uint8_t>& bytes,const J& rules,bool astc,
    const std::function<void()>& checkpoint,
    const std::function<void(unsigned, unsigned, float)>& progress) {
    const auto w=texture.at("width").get<unsigned>(), h=texture.at("height").get<unsigned>(), mips=texture.at("mips").get<unsigned>();
    int format=texture.at("format").get<int>(); bool srgb=texture.at("srgb").get<bool>();
    require(w && h && w<=32768 && h<=32768 && mips && mips<=16,"Invalid texture dimensions");
    size_t size=0;for(unsigned m=0;m<mips;++m) size+=mipSize(format,std::max(w>>m,1u),std::max(h>>m,1u));
    require(size==bytes.size(),"Texture mip byte count mismatch");
    const bool installable=w<=kInstalledTextureSize && h<=kInstalledTextureSize && size<=kInstalledTextureLimit;
    if(format>=48 && format<=50) {
        require(astc,"ASTC package requires an ASTC capable device");
        require(installable,"ASTC texture exceeds the Android 8192/64 MiB install limit");return;
    }
    std::string encoding=texture.value("normal_encoding",std::string{});
    std::string semantic=texture.value("semantic",std::string{});
    bool normal=semantic=="normal";
    auto character=manifest.at("target").at("character_id").get<std::string>();
    auto name=texture.at("original_name").get<std::string>();
    const J* rule=nullptr;
    if(rules.contains(character) && rules.at(character).contains(name)) {rule=&rules.at(character).at(name);normal=true;}
    // Known original material slots establish semantics; never infer from a filename suffix.
    if(encoding.empty() && rule) {
        if(format==27) encoding="xy-unorm";
        else if(format==4) encoding="xyz-unorm"; // already adapted RGBA source
        else if(format==25) encoding=rule->value("bc7_encoding",std::string{});
    }
    require(!normal || !encoding.empty(),"Normal slot has no verified source encoding; add normal_encoding metadata");
    require(encoding.empty() || encoding=="xy-unorm" || encoding=="xyz-unorm","Unsupported normal encoding");
    if(!encoding.empty()) {normal=true;require(!srgb,"Normal texture must be linear");}
    if(format==63) {
        require(!normal,"Single-channel normal unsupported");
        require(installable,"R8 texture exceeds the Android 8192/64 MiB install limit");return;
    }
    // Conservative 4x4 for linear data preserves packed channels; color maps use 6x6.
    unsigned block=srgb?6:4;
    // BEM 1.2 PC textures may exceed the Android install limits. Drop top mips
    // (or box-filter a single-level texture) until the converted chain fits.
    auto converted=[&](unsigned tw,unsigned th,unsigned levels) {
        size_t total=0;
        for(unsigned m=0;m<levels;++m) {
            const auto lw=std::max(tw>>m,1u),lh=std::max(th>>m,1u);
            total+=astc?size_t((lw+block-1)/block)*((lh+block-1)/block)*16:size_t(lw)*lh*4;
        }
        return total;
    };
    unsigned skip=0;
    auto levelsAfter=[&](unsigned s) {return s<mips?mips-s:1u;};
    while(std::max(std::max(w>>skip,1u),std::max(h>>skip,1u))>kInstalledTextureSize ||
          converted(std::max(w>>skip,1u),std::max(h>>skip,1u),levelsAfter(skip))>kInstalledTextureLimit) {
        require(++skip<16,"Texture cannot be reduced to the Android install limit");
    }
    const unsigned base=std::min(skip,mips-1),factor=1u<<(skip-base),levels=levelsAfter(skip);
    astcenc_config config{};
    std::unique_ptr<astcenc_context,decltype(&astcenc_context_free)> context(nullptr,astcenc_context_free);
    if(astc) {
        check(astcenc_config_init(srgb?ASTCENC_PRF_LDR_SRGB:ASTCENC_PRF_LDR,block,block,1,ASTCENC_PRE_FAST,0,&config));
        config.progress_callback=reportProgress;
        astcenc_context* raw=nullptr;check(astcenc_context_alloc(&config,1,&raw));context.reset(raw);
    }
    std::vector<uint8_t> output;size_t pos=0;
    for(unsigned m=0;m<base;++m) pos+=mipSize(format,std::max(w>>m,1u),std::max(h>>m,1u));
    for(unsigned m=0;m<levels;++m) {
        checkpoint();const unsigned level=base+m;
        const auto sw=std::max(w>>level,1u),sh=std::max(h>>level,1u);
        const unsigned scale=m?1u:factor;
        const auto lw=std::max(sw/scale,1u),lh=std::max(sh/scale,1u);
        if(progress) progress(m+1,levels,0);
        auto rgba=decodeScaled(bytes.data()+pos,format,sw,sh,scale,checkpoint);pos+=mipSize(format,sw,sh);
        if(encoding=="xy-unorm") {
            // Reviewed PC BC5 _BumpMap slots consume RG. BC7 replacements may
            // carry arbitrary B data/padding; its value does not define encoding.
            // Explicit package normal_encoding takes precedence over slot rules.
            for(size_t i=0;i<rgba.size();i+=4) {
                float x=rgba[i]*(2.0f/255)-1,y=rgba[i+1]*(2.0f/255)-1;
                rgba[i+2]=uint8_t(std::lround((std::sqrt(std::max(0.0f,1-x*x-y*y))+1)*127.5f));
            }
        }
        size_t count=astc?size_t((lw+block-1)/block)*((lh+block-1)/block)*16:rgba.size();
        require(output.size()+count<=kInstalledTextureLimit,"Converted texture exceeds 64 MiB; use ASTC or a smaller texture");
        auto offset=output.size();output.resize(offset+count);
        if(astc) {
            void* slice=rgba.data();astcenc_image image{lw,lh,1,ASTCENC_TYPE_U8,&slice};
            const astcenc_swizzle swizzle{ASTCENC_SWZ_R,ASTCENC_SWZ_G,ASTCENC_SWZ_B,ASTCENC_SWZ_A};
            const std::function<void(float)> report=[&](float percent) {if(progress) progress(m+1,levels,percent);};
            {
                CompressionScope scope(context.get(),report);
                checkpoint();
                check(astcenc_compress_image(context.get(),&image,&swizzle,output.data()+offset,count,0));
                checkpoint();
            }
            check(astcenc_compress_reset(context.get()));
        } else std::memcpy(output.data()+offset,rgba.data(),count);
        if(progress) progress(m+1,levels,100);
    }
    checkpoint();bytes=std::move(output);texture["format"]=astc?(block==6?50:48):4;
    if(skip) {
        texture["width"]=std::max(w>>skip,1u);texture["height"]=std::max(h>>skip,1u);texture["mips"]=levels;
        texture["android_install_mip_skip"]=skip;
    }
    if(normal) {texture["semantic"]="normal";texture["normal_encoding"]="xyz-unorm";}
}
}
