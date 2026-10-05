#pragma once
#include "cloth_asset_vfs.h"
#include "cloth_asset_constants.h"
namespace eiem_cloth_asset {
inline void BundleCipher(uint8_t *bytes,size_t count) {
  std::array<uint8_t,176> keys{};std::copy(std::begin(BundleKey),std::end(BundleKey),keys.begin());uint8_t rc=1;
  auto xt=[](uint8_t x){return uint8_t((x<<1)^((x&128)?0x1b:0));};
  for(size_t n=16;n<keys.size();n+=4) {
    uint8_t word[4];memcpy(word,keys.data()+n-4,4);
    if(n%16==0){uint8_t first=word[0];for(int k=0;k<3;++k)word[k]=BundleSbox[word[k+1]];word[3]=BundleSbox[first];word[0]^=rc;rc=xt(rc);}
    for(int k=0;k<4;++k)keys[n+k]=keys[n-16+k]^word[k];
  }
  uint8_t previous[16];memcpy(previous,BundleIv,16);
  for(size_t offset=0;offset<count;offset+=16) {
    uint8_t state[16];memcpy(state,previous,16);for(int k=0;k<16;++k)state[k]^=keys[k];
    for(int round=1;round<=10;++round) {
      for(auto &v:state)v=BundleSbox[v];uint8_t shifted[16];
      for(int c=0;c<4;++c)for(int r=0;r<4;++r)shifted[c*4+r]=state[((c+r)%4)*4+r];memcpy(state,shifted,16);
      if(round<10)for(int c=0;c<4;++c){auto a=state+c*4;const uint8_t t=a[0]^a[1]^a[2]^a[3],u=a[0];a[0]^=t^xt(a[0]^a[1]);a[1]^=t^xt(a[1]^a[2]);a[2]^=t^xt(a[2]^a[3]);a[3]^=t^xt(a[3]^u);}
      for(int k=0;k<16;++k)state[k]^=keys[round*16+k];
    }
    for(size_t k=0;k<16&&offset+k<count;++k)bytes[offset+k]^=state[k];
    for(unsigned k=0;k<16;++k){uint8_t t=state[k]^uint8_t(31*k)^uint8_t(0xf19ab7752cdd0196ull>>((k*8)&0x38));t=uint8_t((t>>5)|(t<<3));previous[k]=BundleSbox[t];}
  }
}
inline void BundleDecrypt(Bytes &bytes) {
  if(bytes.size()<=256){BundleCipher(bytes.data(),bytes.size());return;}
  const size_t blocks=bytes.size()/16,number=(std::min)(blocks,size_t(256)),step=blocks>256?1:256/blocks;uint8_t selected[256]{};
  for(size_t k=0;k<number;++k)memcpy(selected+k*step,bytes.data()+k*16,step);
  BundleCipher(selected,256);for(size_t k=0;k<number;++k)memcpy(bytes.data()+k*16,selected+k*step,step);
}
struct BundleNode {std::string name;uint32_t flags=0;Bytes bytes;};
inline std::vector<BundleNode> DecodeBundle(const Bytes &bytes,const std::atomic<bool> *cancel=nullptr) {
  Reader r(bytes,true);const auto a=r.U32(),b=r.U32();Need(b==(((4*(a^0x4a92f0cd))&0xffff0000)^RotR(a^0x4a92f0cd,14)^0xd8b1e637),"asset-bundle-header");
  const auto packed2=r.U16();const auto flags2=r.U32();auto enc=r.U32();const auto size2=r.U32(),flags1=r.U32();const auto raw1=r.U16();r.U32();const auto raw2=r.U16();const auto size1=r.U32();const auto packed1=r.U16();r.U8();
  auto decodeSize=[](uint16_t hi,uint16_t lo){return RotR((uint32_t(uint16_t(hi^lo^0xa121))<<16)|lo,18)^0xf74324ee;};
  const auto packedSize=decodeSize(packed1,packed2),rawSize=decodeSize(raw1,raw2),flags=flags1^flags2^0xa7f49310;enc^=flags2;
  const uint64_t encodedSize=RotR64((uint64_t(size1^size2^0xdad76848)<<32)|size2,18)^0xa4f1a11747816520ull;
  const auto reportedSize=uint32_t(encodedSize);size_t offset=enc>=7?48:40;
  Need(rawSize<=64*1024*1024&&packedSize<=bytes.size(),"asset-bundle-info-budget");
  if(flags&0x80){Need(reportedSize==bytes.size()&&packedSize<=reportedSize,"asset-bundle-end-info-size");r.Seek(reportedSize-packedSize);}else r.Seek(offset);
  auto info=r.Blob(packedSize);if(flags&0x3f){BundleDecrypt(info);info=Lz4(info,rawSize);}else Need(info.size()==rawSize,"asset-bundle-info-size");
  if(!(flags&0x80))offset+=(flags&0x200)?(packedSize+15)&~size_t(15):packedSize;
  Reader ir(info,true);auto count=[&](uint32_t xorValue,uint32_t mask){const auto raw=ir.U32()^xorValue;const auto low=uint16_t(raw),high=uint16_t(raw>>16);const auto n=RotR((uint32_t(low^high)<<16)|low,18)^mask;Need(n<=10000,"asset-bundle-table-budget");return n;};
  const auto blockCount=count(0x23f77b8a,0x91ce0a4f);
  struct Block{uint32_t raw,packed;uint16_t flags;};std::vector<Block> blocks;
  for(uint32_t k=0;k<blockCount;++k) {
    const auto a0=ir.U16(),b0=ir.U16(),c0=ir.U16();const uint16_t ef=ir.U16()^0x9cd6;const auto d0=ir.U16();const auto lo=uint8_t(ef),hi=uint8_t(ef>>8);
    const auto f=uint16_t((uint16_t(lo^hi)<<8)|lo);const auto decoded=uint16_t(c0^uint16_t((f<<14)|(f>>2))^0x523f);
    blocks.push_back({decodeSize(a0,c0),decodeSize(b0,d0),decoded});
  }
  struct Node{std::string name;uint64_t offset,size;uint32_t flags;};std::vector<Node> nodes;std::set<std::string> names;
  const auto nodeCount=count(0x6b0ae55d,0xe4c1d9f2);
  for(uint32_t k=0;k<nodeCount;++k) {
    const auto x=ir.U32()^0x8e06a9f8,y=ir.U32(),z=ir.U32(),w=ir.U32();std::string name=ir.Z(64);for(size_t j=0;j<name.size();++j)name[j]^=char((j^0x97)&255);const auto e=ir.U32();
    Need(ResourcePath(name)&&name.find('/')==std::string::npos&&names.insert(Lower(name)).second,"asset-bundle-node-name");
    const auto lo=uint16_t(x),hi=uint16_t(x>>16);const auto nf=RotR((uint32_t(hi^lo)<<16)|lo,18)^0xf13927c4^y;
    const auto no=RotL64((uint64_t(w^z^0xdad76848)<<32)|z,14)^0xa4f1a11747816520ull;
    const auto ns=RotL64((uint64_t(y^e^0xdad76848)<<32)|e,14)^0xa4f1a11747816520ull;nodes.push_back({std::move(name),no,ns,nf});
  }
  Need(!ir.Left(),"asset-bundle-info-tail");Reader payload(bytes);payload.Seek(offset);Bytes decoded;
  for(const auto &block:blocks) {
    CheckCancel(cancel);Need(block.raw<=128*1024*1024&&block.raw<=256*1024*1024-decoded.size(),"asset-bundle-block-budget");auto data=payload.Blob(block.packed);
    if(block.flags==5){BundleDecrypt(data);data=Lz4(data,block.raw,true);}else Need(block.flags==0&&data.size()==block.raw,"asset-bundle-block-mode");
    decoded.insert(decoded.end(),data.begin(),data.end());
  }
  std::vector<BundleNode> result;
  for(const auto &node:nodes){Need(node.offset<=decoded.size()&&node.size<=decoded.size()-node.offset,"asset-bundle-node-range");result.push_back({node.name,node.flags,Bytes(decoded.begin()+size_t(node.offset),decoded.begin()+size_t(node.offset+node.size))});}
  return result;
}
}
