#pragma once
#include <algorithm>
#include <array>
#include <atomic>
#include <cstdint>
#include <cstring>
#include <limits>
#include <stdexcept>
#include <string>
#include <vector>
#if defined(_M_X64) || defined(__SSE2__)
#include <emmintrin.h>
#endif

namespace eiem_cloth_asset {
using Bytes=std::vector<uint8_t>;
inline void Need(bool value,const char *reason) { if(!value)throw std::runtime_error(reason); }
inline void CheckCancel(const std::atomic<bool> *cancel) {
  if(cancel && cancel->load(std::memory_order_acquire))throw std::runtime_error("asset-request-cancelled");
}
struct Reader {
  const uint8_t *data=nullptr;size_t size=0,pos=0;bool big=false;
  Reader(const uint8_t *p,size_t n,bool be=false):data(p),size(n),big(be){}
  explicit Reader(const Bytes &b,bool be=false):Reader(b.data(),b.size(),be){}
  size_t Left() const {return size-pos;}
  const uint8_t *Take(size_t n) {Need(n<=Left(),"asset-byte-range");auto p=data+pos;pos+=n;return p;}
  void Seek(size_t n) {Need(n<=size,"asset-seek-range");pos=n;}
  void Align(size_t n=4) {Need(n && !(n&(n-1)),"asset-alignment");Take((n-pos%n)%n);}
  uint64_t U(size_t n) {Need(n>0&&n<=8,"asset-integer-size");auto p=Take(n);uint64_t v=0;for(size_t k=0;k<n;++k)v|=uint64_t(p[k])<<(8*(big?n-1-k:k));return v;}
  uint8_t U8(){return uint8_t(U(1));} uint16_t U16(){return uint16_t(U(2));}
  uint32_t U32(){return uint32_t(U(4));} uint64_t U64(){return U(8);}
  int32_t I32(){return int32_t(U32());} int64_t I64(){return int64_t(U64());}
  uint32_t Count(uint32_t maximum) {const auto n=U32();Need(n<=maximum,"asset-count-budget");return n;}
  std::string Text(size_t n) {auto p=Take(n);std::string s(reinterpret_cast<const char*>(p),n);Need(s.find('\0')==std::string::npos,"asset-embedded-null");return s;}
  std::string Z(size_t max=4096) {size_t n=0;while(n<Left()&&n<max&&data[pos+n])++n;Need(n<Left()&&n<max,"asset-string-terminator");auto s=Text(n);Take(1);return s;}
  Bytes Blob(size_t n) {auto p=Take(n);return Bytes(p,p+n);}
};
inline uint32_t RotL(uint32_t v,unsigned n){return (v<<n)|(v>>(32-n));}
inline uint32_t RotR(uint32_t v,unsigned n){return (v>>n)|(v<<(32-n));}
inline uint64_t RotL64(uint64_t v,unsigned n){return (v<<n)|(v>>(64-n));}
inline uint64_t RotR64(uint64_t v,unsigned n){return (v>>n)|(v<<(64-n));}
inline std::string Lower(std::string s){for(auto &c:s)if(c>='A'&&c<='Z')c=char(c+'a'-'A');return s;}
inline std::string Hex(const uint8_t *p,size_t n,bool upper=false) {
  const char *digits=upper?"0123456789ABCDEF":"0123456789abcdef";std::string out;out.reserve(n*2);
  for(size_t k=0;k<n;++k){out+=digits[p[k]>>4];out+=digits[p[k]&15];}return out;
}
inline bool ResourcePath(const std::string &p) {
  if(p.empty()||p.size()>4096||p.front()=='/'||p.back()=='/')return false;
  size_t start=0;
  for(size_t n=0;n<=p.size();++n) {
    if(n<p.size()&&(uint8_t(p[n])<32||p[n]=='\\'||p[n]==':'))return false;
    if(n==p.size()||p[n]=='/') {auto s=p.substr(start,n-start);if(s.empty()||s=="."||s=="..")return false;start=n+1;}
  }return true;
}
inline uint32_t Crc32(const uint8_t *p,size_t n) {
  constexpr auto table=[] {std::array<uint32_t,256> a{};for(unsigned k=0;k<256;++k){uint32_t v=k;for(int b=0;b<8;++b)v=(v>>1)^(0xedb88320u&uint32_t(-int(v&1)));a[k]=v;}return a;}();
  uint32_t crc=~0u;for(size_t k=0;k<n;++k)crc=table[(crc^p[k])&255]^(crc>>8);return ~crc;
}
inline void ChaChaScalar(uint8_t *data,size_t size,const uint8_t key[32],const uint8_t nonce[12],uint32_t counter) {
  uint32_t state[16]{0x61707865,0x3320646e,0x79622d32,0x6b206574};
  Reader kr(key,32),nr(nonce,12);for(int n=4;n<12;++n)state[n]=kr.U32();state[12]=counter;for(int n=13;n<16;++n)state[n]=nr.U32();
  for(size_t offset=0;offset<size;offset+=64) {
    uint32_t w[16];memcpy(w,state,sizeof(w));
    auto q=[&](int a,int b,int c,int d){w[a]+=w[b];w[d]=RotL(w[d]^w[a],16);w[c]+=w[d];w[b]=RotL(w[b]^w[c],12);w[a]+=w[b];w[d]=RotL(w[d]^w[a],8);w[c]+=w[d];w[b]=RotL(w[b]^w[c],7);};
    for(int n=0;n<10;++n){q(0,4,8,12);q(1,5,9,13);q(2,6,10,14);q(3,7,11,15);q(0,5,10,15);q(1,6,11,12);q(2,7,8,13);q(3,4,9,14);}
    for(size_t n=0;n<64&&offset+n<size;++n)data[offset+n]^=uint8_t((w[n/4]+state[n/4])>>(8*(n%4)));
    Need(state[12]!=UINT32_MAX||offset+64>=size,"asset-cipher-counter-overflow");++state[12];
  }
}
#if defined(_M_X64) || defined(__SSE2__)
template<int Bits> inline __m128i CipherRotate(__m128i value){return _mm_or_si128(_mm_slli_epi32(value,Bits),_mm_srli_epi32(value,32-Bits));}
inline void CipherQuarter(__m128i &a,__m128i &b,__m128i &c,__m128i &d){
  a=_mm_add_epi32(a,b);d=CipherRotate<16>(_mm_xor_si128(d,a));c=_mm_add_epi32(c,d);b=CipherRotate<12>(_mm_xor_si128(b,c));
  a=_mm_add_epi32(a,b);d=CipherRotate<8>(_mm_xor_si128(d,a));c=_mm_add_epi32(c,d);b=CipherRotate<7>(_mm_xor_si128(b,c));
}
#endif
inline void ChaCha(Bytes &data,const uint8_t key[32],const uint8_t nonce[12],uint32_t counter=1) {
  Need(data.size()/64+(data.size()%64!=0)<=uint64_t(UINT32_MAX)-counter+1,"asset-cipher-counter-overflow");size_t offset=0;
#if defined(_M_X64) || defined(__SSE2__)
  if(data.size()>=256){uint32_t words[16]{0x61707865,0x3320646e,0x79622d32,0x6b206574};Reader kr(key,32),nr(nonce,12);
    for(int n=4;n<12;++n)words[n]=kr.U32();for(int n=13;n<16;++n)words[n]=nr.U32();
    __m128i state[16];for(int n=0;n<16;++n)state[n]=_mm_set1_epi32(int(words[n]));
    for(;data.size()-offset>=256;offset+=256,counter+=4){
      state[12]=_mm_set_epi32(int(counter+3),int(counter+2),int(counter+1),int(counter));__m128i w[16];memcpy(w,state,sizeof(w));
      for(int round=0;round<10;++round){CipherQuarter(w[0],w[4],w[8],w[12]);CipherQuarter(w[1],w[5],w[9],w[13]);CipherQuarter(w[2],w[6],w[10],w[14]);CipherQuarter(w[3],w[7],w[11],w[15]);
        CipherQuarter(w[0],w[5],w[10],w[15]);CipherQuarter(w[1],w[6],w[11],w[12]);CipherQuarter(w[2],w[7],w[8],w[13]);CipherQuarter(w[3],w[4],w[9],w[14]);}
      for(int n=0;n<16;n+=4){const auto a=_mm_add_epi32(w[n],state[n]),b=_mm_add_epi32(w[n+1],state[n+1]),c=_mm_add_epi32(w[n+2],state[n+2]),d=_mm_add_epi32(w[n+3],state[n+3]);
        const auto ab0=_mm_unpacklo_epi32(a,b),ab1=_mm_unpackhi_epi32(a,b),cd0=_mm_unpacklo_epi32(c,d),cd1=_mm_unpackhi_epi32(c,d);
        const __m128i blocks[]{_mm_unpacklo_epi64(ab0,cd0),_mm_unpackhi_epi64(ab0,cd0),_mm_unpacklo_epi64(ab1,cd1),_mm_unpackhi_epi64(ab1,cd1)};
        for(int lane=0;lane<4;++lane){auto p=reinterpret_cast<__m128i*>(data.data()+offset+size_t(lane)*64+size_t(n)*4);_mm_storeu_si128(p,_mm_xor_si128(_mm_loadu_si128(p),blocks[lane]));}}
    }
  }
#endif
  if(offset<data.size())ChaChaScalar(data.data()+offset,data.size()-offset,key,nonce,counter);
}
inline Bytes Lz4(const Bytes &source,size_t length,bool inverted=false) {
  Need(length<=256*1024*1024,"asset-lz4-budget");Reader r(source);Bytes out;out.reserve(length);
  auto extend=[&](size_t n){if(n==15){uint8_t b;do{b=r.U8();Need(n<=length&&size_t(b)<=length-n,"asset-lz4-length");n+=b;}while(b==255);}return n;};
  while(r.Left()) {
    const uint8_t t=r.U8();size_t literal=t>>4,match=t&15;
    if(inverted){const unsigned l=t&0x33,m=(t&0xcc)>>2;literal=(l&3)|(l>>2);match=(m&3)|(m>>2);}
    literal=extend(literal);Need(literal<=length-out.size(),"asset-lz4-literal-range");auto p=r.Take(literal);out.insert(out.end(),p,p+literal);
    if(!r.Left())break;
    auto a=r.U8(),b=r.U8();const size_t back=inverted?(size_t(a)<<8)|b:a|(size_t(b)<<8);
    Need(back>0&&back<=out.size(),"asset-lz4-back-reference");match=extend(match);
    Need(match<=length-out.size()&&4<=length-out.size()-match,"asset-lz4-match-range");match+=4;
    for(size_t n=0;n<match;++n)out.push_back(out[out.size()-back]);
  }
  Need(out.size()==length,"asset-lz4-short-output");return out;
}
}
