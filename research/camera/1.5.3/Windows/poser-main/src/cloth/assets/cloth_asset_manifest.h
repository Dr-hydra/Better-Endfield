#pragma once
#include "cloth_asset_vfs.h"
#include "../../../deps/brotli/c/include/brotli/decode.h"
#include <memory>
namespace eiem_cloth_asset {
inline Bytes Brotli(const uint8_t *data,size_t size,size_t maximum,const std::atomic<bool> *cancel=nullptr) {
  Need(maximum<=256*1024*1024,"asset-brotli-budget");
  auto cleanup=[](BrotliDecoderState *p){BrotliDecoderDestroyInstance(p);};
  std::unique_ptr<BrotliDecoderState,decltype(cleanup)> decoder(BrotliDecoderCreateInstance(nullptr,nullptr,nullptr),cleanup);
  Need(bool(decoder),"asset-brotli-allocation");Bytes out;const uint8_t *input=data;size_t left=size;uint8_t chunk[65536];
  for(;;) {
    CheckCancel(cancel);size_t available=sizeof(chunk);uint8_t *dest=chunk;
    const auto state=BrotliDecoderDecompressStream(decoder.get(),&left,&input,&available,&dest,nullptr);const size_t written=sizeof(chunk)-available;
    Need(written<=maximum-out.size(),"asset-brotli-output-budget");out.insert(out.end(),chunk,chunk+written);
    if(state==BROTLI_DECODER_RESULT_SUCCESS){Need(left==0,"asset-brotli-trailing-input");return out;}
    Need(state==BROTLI_DECODER_RESULT_NEEDS_MORE_OUTPUT,"asset-brotli-invalid-or-truncated");
  }
}
struct ManifestAsset {std::string name,bundle;int64_t bytes=0;int bundleId=-1;};
struct ManifestBundle {std::string name;std::vector<int> dependencies;};
struct Manifest {
  std::string version,hash;std::vector<ManifestAsset> assets;std::vector<ManifestBundle> bundles;
  std::string sourceName,sourceHash;
  void Parse(const Bytes &data,const std::atomic<bool> *cancel=nullptr) {
    Reader r(data);Need(r.U32()==0xff11ff11,"asset-manifest-magic");
    auto text=[&](Reader &q,bool chars) {
      size_t n=q.Count(8192);if(chars)n*=2;Need(!(n&1),"asset-manifest-utf16");auto p=q.Take(n);
      if(n>=2&&p[n-1]==0&&p[n-2]==0)n-=2;
      std::wstring w(n/2,L'\0');for(size_t k=0;k<w.size();++k)w[k]=wchar_t(p[2*k]|(unsigned(p[2*k+1])<<8));
      Need(w.find(L'\0')==std::wstring::npos,"asset-manifest-null");
      if(w.empty())return std::string{};
      const int bytes=WideCharToMultiByte(CP_UTF8,WC_ERR_INVALID_CHARS,w.data(),int(w.size()),nullptr,0,nullptr,nullptr);
      Need(bytes>0,"asset-manifest-name");std::string out(bytes,'\0');Need(WideCharToMultiByte(CP_UTF8,WC_ERR_INVALID_CHARS,w.data(),int(w.size()),out.data(),bytes,nullptr,nullptr)==bytes,"asset-manifest-name");return out;
    };
    version=text(r,true);Need(r.U32()==0xf1f2f3f4,"asset-manifest-format");hash=text(r,true);text(r,true);
    const auto assetBytes=r.Count(uint32_t(data.size()));const auto ap=r.Take(assetBytes);Reader ar(ap,assetBytes);
    r.Take(r.Count(uint32_t(data.size())));
    const auto bundleBytes=r.Count(uint32_t(data.size()));const auto bp=r.Take(bundleBytes);Reader br(bp,bundleBytes);
    const auto dataBytes=r.Count(uint32_t(data.size()));const auto dp=r.Take(dataBytes);Need(r.Left()<=4,"asset-manifest-tail");
    auto slice=[&](uint32_t at,size_t n){Need(at<=dataBytes&&n<=dataBytes-at,"asset-manifest-data-range");return Reader(dp+at,n);};
    auto di=[&](uint32_t at){auto q=slice(at,4);return q.U32();};
    const auto count=br.Count(1000000),slots=ar.Count(1000000);Need(count&&uint64_t(count)*48+4==bundleBytes&&slots&&uint64_t(slots)*8+4<=assetBytes,"asset-manifest-table");
    bundles.clear();bundles.resize(count);
    for(uint32_t n=0;n<count;++n) {
      CheckCancel(cancel);Reader row(bp+4+size_t(n)*48,48);row.U32();const auto name=row.U32(),dep=row.U32();const auto len=di(name);
      Need(len<=8192&&name<=UINT32_MAX-4,"asset-manifest-bundle-name");auto q=slice(name,4+size_t(len));auto &b=bundles[n];b.name=text(q,false);Need(ResourcePath(b.name),"asset-manifest-bundle-path");
      const auto num=di(dep);Need(num<=count&&dep<=UINT32_MAX-4,"asset-manifest-dependency-count");auto d=slice(dep+4,size_t(num)*4);
      for(uint32_t k=0;k<num;++k){const auto id=d.U32();Need(id<count,"asset-manifest-dependency-id");b.dependencies.push_back(int(id));}
    }
    assets.clear();std::set<size_t> seen;std::map<std::string,std::pair<uint32_t,uint32_t>> names;
    for(uint32_t s=0;s<slots;++s) {
      CheckCancel(cancel);const auto offset=ar.U32(),number=ar.U32();if(!number)continue;
      Need(number<=1000000&&offset>=4+uint64_t(slots)*8&&offset<=assetBytes&&uint64_t(number)*24<=assetBytes-offset,"asset-manifest-slot");
      for(uint32_t n=0;n<number;++n) {
        const size_t at=offset+size_t(n)*24;Need(seen.insert(at).second&&seen.size()<=1000000,"asset-manifest-duplicate-record");Reader row(ap+at,24);row.U64();
        const auto off=row.U32(),id=row.U32(),bytes=row.U32();Need(id<count&&off<=UINT32_MAX-4,"asset-manifest-asset-record");const auto compressed=di(off);Need(compressed<=4096,"asset-manifest-compressed-name");auto packed=slice(off+4,compressed);
        auto decoded=Brotli(packed.data,packed.size,8192,cancel);Need(!(decoded.size()&1),"asset-manifest-asset-name");
        Bytes prefixed(4+decoded.size());uint32_t length=uint32_t(decoded.size());memcpy(prefixed.data(),&length,4);memcpy(prefixed.data()+4,decoded.data(),decoded.size());Reader nr(prefixed);auto name=text(nr,false);
        const auto lower=Lower(name);auto ends=[&](const char *suffix){size_t n=strlen(suffix);return lower.size()>=n&&lower.compare(lower.size()-n,n,suffix)==0;};if(!ends(".asset")&&!ends(".fbx")&&!ends(".prefab"))continue;
        Need(ResourcePath(name),"asset-manifest-asset-path");auto old=names.find(lower);if(old!=names.end()){Need(old->second==std::make_pair(id,bytes),"asset-manifest-ambiguous-path");continue;}names[lower]={id,bytes};
        assets.push_back({std::move(name),bundles[id].name,bytes,int(id)});
      }
    }
  }
  void Load(const Vfs &vfs) {
    constexpr char suffix[]="manifest.hgmmap";constexpr size_t length=sizeof(suffix)-1;
    std::vector<std::string> found;for(const auto &entry:vfs.files){const auto &n=entry.first;if(n.size()>=length&&n.compare(n.size()-length,length,suffix)==0)found.push_back(n);}
    Need(found.size()==1,"asset-manifest-missing-or-ambiguous");sourceName=found[0];auto packed=vfs.Read(sourceName);sourceHash=Digest(packed);
    const auto decoded=Brotli(packed.data(),packed.size(),256*1024*1024,vfs.cancel);Parse(decoded,vfs.cancel);
  }
};
}
