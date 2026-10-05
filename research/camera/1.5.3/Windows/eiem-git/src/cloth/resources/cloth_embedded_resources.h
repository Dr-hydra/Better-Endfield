#pragma once
#ifndef NOMINMAX
#define NOMINMAX
#endif
#include <windows.h>
#include <compressapi.h>
#include <array>
#include <atomic>
#include <cstdint>
#include <cstring>
#include <vector>
#pragma comment(lib, "cabinet.lib")

namespace eiem_cloth_resource {
constexpr size_t EmbeddedLimit = 16u*1024u*1024u+20u;
constexpr unsigned FirstResource = 4303, LastResource = 4314;
inline uint64_t ResourceHash(const void *data,size_t size) {
  uint64_t h=14695981039346656037ULL;
  for(size_t n=0;n<size;++n)h=(h^static_cast<const unsigned char*>(data)[n])*1099511628211ULL;
  return h;
}
inline bool Decode(unsigned id,const void *data,size_t count,size_t limit,
                   std::vector<unsigned char> &bytes,DWORD &error) {
  bytes.clear();error=ERROR_INVALID_DATA;
  if(!data || count<32 || id<FirstResource || id>LastResource || memcmp(data,"EIEMRS01",8))return false;
  uint32_t fields[4]{};uint64_t checksum=0;
  memcpy(fields,static_cast<const unsigned char*>(data)+8,sizeof(fields));
  memcpy(&checksum,static_cast<const unsigned char*>(data)+24,sizeof(checksum));
  const size_t size=fields[1],stored=fields[2];const unsigned codec=fields[3];
  if(fields[0]!=id || !size || size>limit || size>EmbeddedLimit || !stored || stored!=count-32 ||
      (codec!=0 && codec!=COMPRESS_ALGORITHM_LZMS))return false;
  const auto *payload=static_cast<const unsigned char*>(data)+32;
  std::vector<unsigned char> decoded;
  try { decoded.resize(size); } catch(...) { error=ERROR_NOT_ENOUGH_MEMORY;return false; }
  if(!codec) {
    if(stored!=size)return false;
    memcpy(decoded.data(),payload,size);
  } else {
    DECOMPRESSOR_HANDLE decoder=nullptr;
    if(!CreateDecompressor(COMPRESS_ALGORITHM_LZMS,nullptr,&decoder)){error=GetLastError();return false;}
    SIZE_T written=0;
    const bool ok=Decompress(decoder,payload,stored,decoded.data(),decoded.size(),&written)!=FALSE;
    const DWORD failure=ok?ERROR_INVALID_DATA:GetLastError();
    CloseDecompressor(decoder);
    if(!ok || written!=size){error=failure;return false;}
  }
  if(ResourceHash(decoded.data(),decoded.size())!=checksum)return false;
  bytes.swap(decoded);error=0;return true;
}
inline bool Load(HMODULE module,unsigned id,size_t limit,std::vector<unsigned char> &bytes,DWORD &error) {
  bytes.clear();error=0;
  auto resource=FindResourceW(module,MAKEINTRESOURCEW(id),MAKEINTRESOURCEW(10));
  if(!resource){error=GetLastError();return false;}
  const DWORD size=SizeofResource(module,resource);
  if(size<32 || size>EmbeddedLimit+32){error=ERROR_INVALID_DATA;return false;}
  auto loaded=LoadResource(module,resource);const void *data=loaded?LockResource(loaded):nullptr;
  return Decode(id,data,size,limit,bytes,error);
}
class EmbeddedStore {
  std::array<std::vector<unsigned char>,LastResource-FirstResource+1> values_;
  std::atomic<bool> started_{false},ready_{false};
public:
  bool Prepare(HMODULE module,DWORD &error) {
    bool expected=false;
    if(!started_.compare_exchange_strong(expected,true)){error=ERROR_ALREADY_INITIALIZED;return ready_.load(std::memory_order_acquire);}
    decltype(values_) next;
    for(unsigned id=FirstResource;id<=LastResource;++id)
      if(!Load(module,id,id==4305?EmbeddedLimit:2u*1024u*1024u,next[id-FirstResource],error))return false;
    values_.swap(next);ready_.store(true,std::memory_order_release);error=0;return true;
  }
  bool Get(unsigned id,const void *&bytes,DWORD &size) const {
    bytes=nullptr;size=0;
    if(id<FirstResource || id>LastResource || !ready_.load(std::memory_order_acquire))return false;
    const auto &value=values_[id-FirstResource];bytes=value.data();size=static_cast<DWORD>(value.size());return true;
  }
};
inline EmbeddedStore Embedded;
}
