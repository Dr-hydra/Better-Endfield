#pragma once
#include <array>
#include <bit>
#include <cstdint>
#include <span>
#include <string>

namespace BetterEndfield::CustomModel {
// CPU worker only. A small certificate replaces ordinal/name-based identity;
// the full payload is never retained/copied by the finished-asset cache.
inline std::string ModelContentSha256(std::span<const uint8_t> input) {
    constexpr uint32_t constants[]{
        0x428a2f98,0x71374491,0xb5c0fbcf,0xe9b5dba5,0x3956c25b,0x59f111f1,0x923f82a4,0xab1c5ed5,
        0xd807aa98,0x12835b01,0x243185be,0x550c7dc3,0x72be5d74,0x80deb1fe,0x9bdc06a7,0xc19bf174,
        0xe49b69c1,0xefbe4786,0x0fc19dc6,0x240ca1cc,0x2de92c6f,0x4a7484aa,0x5cb0a9dc,0x76f988da,
        0x983e5152,0xa831c66d,0xb00327c8,0xbf597fc7,0xc6e00bf3,0xd5a79147,0x06ca6351,0x14292967,
        0x27b70a85,0x2e1b2138,0x4d2c6dfc,0x53380d13,0x650a7354,0x766a0abb,0x81c2c92e,0x92722c85,
        0xa2bfe8a1,0xa81a664b,0xc24b8b70,0xc76c51a3,0xd192e819,0xd6990624,0xf40e3585,0x106aa070,
        0x19a4c116,0x1e376c08,0x2748774c,0x34b0bcb5,0x391c0cb3,0x4ed8aa4a,0x5b9cca4f,0x682e6ff3,
        0x748f82ee,0x78a5636f,0x84c87814,0x8cc70208,0x90befffa,0xa4506ceb,0xbef9a3f7,0xc67178f2};
    std::array<uint32_t,8> state{0x6a09e667,0xbb67ae85,0x3c6ef372,0xa54ff53a,0x510e527f,0x9b05688c,0x1f83d9ab,0x5be0cd19};
    const auto block=[&](const uint8_t* bytes) {
        uint32_t words[64]{};
        for(size_t i=0;i<16;++i) words[i]=uint32_t(bytes[i*4])<<24 | uint32_t(bytes[i*4+1])<<16 |
            uint32_t(bytes[i*4+2])<<8 | uint32_t(bytes[i*4+3]);
        for(size_t i=16;i<64;++i) {
            const auto a=words[i-15],b=words[i-2];
            words[i]=words[i-16]+(std::rotr(a,7)^std::rotr(a,18)^(a>>3))+words[i-7]+(std::rotr(b,17)^std::rotr(b,19)^(b>>10));
        }
        auto a=state[0],b=state[1],c=state[2],d=state[3],e=state[4],f=state[5],g=state[6],h=state[7];
        for(size_t i=0;i<64;++i) {
            const auto first=h+(std::rotr(e,6)^std::rotr(e,11)^std::rotr(e,25))+((e&f)^(~e&g))+constants[i]+words[i];
            const auto second=(std::rotr(a,2)^std::rotr(a,13)^std::rotr(a,22))+((a&b)^(a&c)^(b&c));
            h=g;g=f;f=e;e=d+first;d=c;c=b;b=a;a=first+second;
        }
        state[0]+=a;state[1]+=b;state[2]+=c;state[3]+=d;state[4]+=e;state[5]+=f;state[6]+=g;state[7]+=h;
    };
    size_t offset=0;for(;input.size()-offset>=64;offset+=64) block(input.data()+offset);
    std::array<uint8_t,128> tail{};const auto remaining=input.size()-offset;
    for(size_t i=0;i<remaining;++i) tail[i]=input[offset+i];tail[remaining]=0x80;
    const auto length=remaining<56?64u:128u;const auto bits=uint64_t(input.size())*8;
    for(size_t i=0;i<8;++i) tail[length-1-i]=uint8_t(bits>>(8*i));
    block(tail.data());if(length==128) block(tail.data()+64);
    constexpr char hex[]="0123456789abcdef";std::string output;output.reserve(64);
    for(uint32_t word:state) for(int shift=28;shift>=0;shift-=4) output.push_back(hex[(word>>shift)&15]);
    return output;
}
}
