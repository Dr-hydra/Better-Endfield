#pragma once
#include <cstddef>

namespace eiem_cloth_resource {
constexpr size_t AuthoredBytes=2u*1024u*1024u;
constexpr size_t RuntimeBytes=8u*1024u*1024u;
inline size_t Limit(bool runtimeGenerated){return runtimeGenerated?RuntimeBytes:AuthoredBytes;}
inline bool Fits(size_t bytes,size_t limit){return bytes>0&&(limit==AuthoredBytes||limit==RuntimeBytes)&&bytes<=limit;}
}
