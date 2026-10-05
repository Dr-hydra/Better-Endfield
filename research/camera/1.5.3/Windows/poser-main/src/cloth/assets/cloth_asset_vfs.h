#pragma once
#ifndef NOMINMAX
#define NOMINMAX
#endif
#include <windows.h>
#include <bcrypt.h>
#include <map>
#include <set>
#include "cloth_asset_bytes.h"
#pragma comment(lib,"bcrypt.lib")

namespace eiem_cloth_asset {
constexpr size_t MaxFileBytes=128*1024*1024;
inline std::wstring Wide(const std::string &text) {
  Need(text.size()<=INT_MAX,"asset-path-length");int n=MultiByteToWideChar(CP_UTF8,MB_ERR_INVALID_CHARS,text.data(),int(text.size()),nullptr,0);
  Need(n>0,"asset-path-utf8");std::wstring out(n,L'\0');Need(MultiByteToWideChar(CP_UTF8,MB_ERR_INVALID_CHARS,text.data(),int(text.size()),out.data(),n)==n,"asset-path-utf8");return out;
}
struct File {
  HANDLE h=INVALID_HANDLE_VALUE;
  explicit File(const std::wstring &path):h(CreateFileW(path.c_str(),GENERIC_READ,FILE_SHARE_READ,nullptr,OPEN_EXISTING,FILE_ATTRIBUTE_NORMAL,nullptr)){}
  ~File(){if(h!=INVALID_HANDLE_VALUE)CloseHandle(h);}
  File(const File&)=delete;File &operator=(const File&)=delete;
};
inline Bytes ReadFileRange(const std::wstring &path,uint64_t offset,size_t count,bool entire=false) {
  File f(path);Need(f.h!=INVALID_HANDLE_VALUE,"asset-source-file-unavailable");LARGE_INTEGER size{};
  Need(GetFileSizeEx(f.h,&size)&&size.QuadPart>=0,"asset-source-size");
  if(entire){Need(uint64_t(size.QuadPart)<=count,"asset-source-file-budget");count=size_t(size.QuadPart);}
  Need(offset<=uint64_t(size.QuadPart)&&count<=uint64_t(size.QuadPart)-offset&&count<=MaxFileBytes,"asset-source-range");
  LARGE_INTEGER at{};at.QuadPart=LONGLONG(offset);Need(SetFilePointerEx(f.h,at,nullptr,FILE_BEGIN)!=0,"asset-source-seek");
  Bytes data(count);size_t done=0;while(done<count){DWORD n=0;Need(ReadFile(f.h,data.data()+done,DWORD((std::min)(count-done,size_t(1024*1024))),&n,nullptr)&&n,"asset-source-short-read");done+=n;}return data;
}
inline std::string Digest(const uint8_t *data,size_t bytes,LPCWSTR type=BCRYPT_SHA256_ALGORITHM) {
  BCRYPT_ALG_HANDLE alg=nullptr;BCRYPT_HASH_HANDLE hash=nullptr;std::array<uint8_t,32> out{};ULONG result=0,length=0;
  bool ok=BCryptOpenAlgorithmProvider(&alg,type,nullptr,0)>=0;
  if(ok)ok=BCryptGetProperty(alg,BCRYPT_HASH_LENGTH,reinterpret_cast<PUCHAR>(&length),sizeof(length),&result,0)>=0&&length<=out.size();
  if(ok)ok=BCryptCreateHash(alg,&hash,nullptr,0,nullptr,0,0)>=0;
  if(ok)ok=bytes<=ULONG_MAX&&BCryptHashData(hash,const_cast<PUCHAR>(data),ULONG(bytes),0)>=0&&BCryptFinishHash(hash,out.data(),length,0)>=0;
  if(hash)BCryptDestroyHash(hash);if(alg)BCryptCloseAlgorithmProvider(alg,0);Need(ok,"asset-digest-failed");return Hex(out.data(),length);
}
inline std::string Digest(const Bytes &b){return Digest(b.data(),b.size());}
struct VfsSource {
  std::string name,chunkName,chunkContent,fileChunk,fileData;
  uint64_t offset=0,length=0,chunkLength=0,iv=0;uint8_t type=0,tag=0;
  bool encrypted=false;int layer=0;
  bool Same(const VfsSource &b) const {
    return type==b.type&&chunkName==b.chunkName&&chunkContent==b.chunkContent&&fileChunk==b.fileChunk&&fileData==b.fileData&&
      offset==b.offset&&length==b.length&&chunkLength==b.chunkLength&&iv==b.iv&&encrypted==b.encrypted&&tag==b.tag;
  }
};
struct VfsIndex { int version=0,code=0;uint8_t type=0;uint64_t buildTime=0;std::string buildNote;std::vector<VfsSource> files; };
inline VfsIndex ParseVfs(const Bytes &decrypted,uint8_t expectedType,int layer) {
  Need(decrypted.size()>=4,"asset-index-too-short");Reader crc(decrypted.data()+decrypted.size()-4,4);
  Need(crc.U32()==Crc32(decrypted.data(),decrypted.size()-4),"asset-index-crc");Reader r(decrypted.data(),decrypted.size()-4);
  VfsIndex out;const int raw=r.I32();if(raw<11){out.code=raw;out.version=r.I32();}else{out.code=3;out.version=raw;}
  Need(out.code>=3&&out.code<=4&&out.version>0,"asset-index-version");r.Text(r.U16());r.U64();const auto total=r.Count(1000000);r.U64();out.type=r.U8();
  Need(out.type==expectedType,"asset-index-type");const auto chunks=r.Count(200000);std::set<std::string> names;
  for(uint32_t c=0;c<chunks;++c) {
    VfsSource base;base.chunkName=Hex(r.Take(16),16,true)+".chk";base.chunkContent=Hex(r.Take(16),16);base.chunkLength=r.U64();
    Need(base.chunkLength<=uint64_t(INT64_MAX)&&r.U8()==expectedType,"asset-index-chunk");
    if(out.code>3){const auto tag=r.U32();Need(tag<=1,"asset-index-chunk-tag");}
    const auto files=r.Count(1000000);Need(files<=1000000-out.files.size(),"asset-index-total-budget");
    for(uint32_t n=0;n<files;++n) {
      VfsSource f=base;f.name=r.Text(r.U16());Need(ResourcePath(f.name)&&names.insert(Lower(f.name)).second,"asset-index-duplicate-or-invalid-path");
      r.U64();f.fileChunk=Hex(r.Take(16),16);f.fileData=Hex(r.Take(16),16);f.offset=r.U64();f.length=r.U64();f.type=r.U8();
      const auto encrypted=r.U8();Need(encrypted<=1,"asset-index-encryption-flag");f.encrypted=encrypted!=0;f.iv=f.encrypted?r.U64():0;
      if(out.code>3){const auto tag=r.U32();Need(tag<=1,"asset-index-file-tag");f.tag=uint8_t(tag);}
      Need(f.type==expectedType&&f.offset<=f.chunkLength&&f.length<=f.chunkLength-f.offset,"asset-index-file-range");f.layer=layer;out.files.push_back(std::move(f));
    }
  }
  if(r.Left()){out.buildTime=r.U64();out.buildNote=r.Text(r.U16());}
  Need(!r.Left()&&out.files.size()==total,"asset-index-count-or-tail");return out;
}
inline const wchar_t *BlockDirectory(uint8_t type) {
  switch(type){case 2:return L"0CE8FA57";case 4:return L"1CDDBF1F";case 11:return L"7064D8E2";default:throw std::runtime_error("asset-block-type-unsupported");}
}
constexpr uint8_t VfsKey[32]{0xe9,0x5b,0x31,0x7a,0xc4,0xf8,0x28,0x56,0x9d,0x23,0xa8,0x6b,0xf2,0x71,0xdc,0xb5,0x3e,0x84,0x6f,0xa7,0x5c,0x92,0x4d,0x67,0x1d,0xba,0x8e,0x38,0xf4,0xca,0x52,0xe1};
struct Vfs {
  std::wstring game;std::map<std::string,std::vector<VfsSource>> files;
  std::vector<std::pair<std::wstring,std::string>> indices;
  const std::atomic<bool> *cancel=nullptr;
  std::wstring LayerRoot(int layer) const {return game+(layer?L"\\Persistent\\VFS\\":L"\\StreamingAssets\\VFS\\");}
  std::wstring Path(const VfsSource &f) const {return LayerRoot(f.layer)+BlockDirectory(f.type)+L"\\"+Wide(f.chunkName);}
  void Open(const std::wstring &dataDirectory) {
    game=dataDirectory;files.clear();indices.clear();
    for(int layer=0;layer<2;++layer)for(uint8_t type:{uint8_t(2),uint8_t(11),uint8_t(4)}) {
      CheckCancel(cancel);const std::wstring path=LayerRoot(layer)+BlockDirectory(type)+L"\\"+BlockDirectory(type)+L".blc";
      const DWORD attr=GetFileAttributesW(path.c_str());if(attr==INVALID_FILE_ATTRIBUTES){auto e=GetLastError();Need(e==ERROR_FILE_NOT_FOUND||e==ERROR_PATH_NOT_FOUND,"asset-index-unreadable");indices.push_back({path,"absent"});continue;}
      const auto raw=ReadFileRange(path,0,64*1024*1024,true);indices.push_back({path,Digest(raw)});Need(raw.size()>=12,"asset-index-header");
      Bytes plain(raw.begin()+12,raw.end());ChaCha(plain,VfsKey,raw.data());auto index=ParseVfs(plain,type,layer);
      for(auto &f:index.files){auto &list=files[Lower(f.name)];for(const auto &old:list)Need(old.layer!=layer,"asset-path-ambiguous-block");list.push_back(std::move(f));}
    }
    Need(!files.empty(),"asset-no-installed-index");
  }
  bool Present(const VfsSource &f) const {WIN32_FILE_ATTRIBUTE_DATA a{};return GetFileAttributesExW(Path(f).c_str(),GetFileExInfoStandard,&a)&&!(a.dwFileAttributes&FILE_ATTRIBUTE_DIRECTORY)&&((uint64_t(a.nFileSizeHigh)<<32)|a.nFileSizeLow)>=f.offset+f.length;}
  VfsSource Resolve(const std::string &name) const {
    Need(ResourcePath(name),"asset-request-path");auto i=files.find(Lower(name));Need(i!=files.end()&&!i->second.empty(),"asset-resource-not-indexed");const auto &wanted=i->second.back();
    if(Present(wanted))return wanted;
    for(const auto &f:i->second)if(f.Same(wanted)&&Present(f))return f;
    throw std::runtime_error("asset-current-chunk-missing-no-exact-fallback");
  }
  Bytes Read(const std::string &name) const {
    CheckCancel(cancel);const auto f=Resolve(name);Need(f.length<=MaxFileBytes,"asset-resource-byte-budget");auto bytes=ReadFileRange(Path(f),f.offset,size_t(f.length));
    if(f.encrypted){uint8_t nonce[12]{3};for(int k=0;k<8;++k)nonce[k+4]=uint8_t(f.iv>>(k*8));ChaCha(bytes,VfsKey,nonce);}
    CheckCancel(cancel);return bytes;
  }
  bool StillCurrent() const {
    for(const auto &i:indices) {
      CheckCancel(cancel);if(i.second=="absent"){if(GetFileAttributesW(i.first.c_str())!=INVALID_FILE_ATTRIBUTES)return false;const auto e=GetLastError();if(e!=ERROR_FILE_NOT_FOUND&&e!=ERROR_PATH_NOT_FOUND)return false;}
      else if(Digest(ReadFileRange(i.first,0,64*1024*1024,true))!=i.second)return false;
    }return true;
  }
};
}
