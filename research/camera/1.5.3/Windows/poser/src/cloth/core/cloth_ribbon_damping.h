#pragma once
#include <cmath>
#include <string_view>

namespace poser_cloth_ribbon {
inline bool Component(std::string_view name) {
  constexpr std::string_view word="ribbon";
  for(size_t i=0;i+word.size()<=name.size();++i) {
    size_t n=0;
    for(;n<word.size();++n) {
      char c=name[i+n];if(c>='A'&&c<='Z')c+=char('a'-'A');
      if(c!=word[n])break;
    }
    if(n==word.size())return true;
  }
  return false;
}
inline bool Value(float original,float strength,float &result) {
  if(!std::isfinite(original)||original<0||original>1||
      !std::isfinite(strength)||strength<0||strength>1)return false;
  // Retain the authored depth curve; raise only its scalar amplitude.
  // Even at 100%, preserve substantial motion rather than pinning particles.
  result=original+(1-original)*(.35f*strength);
  return true;
}
inline bool Curve(const float (&before)[16],const float (&after)[16]) {
  for(int n=0;n<16;++n)
    if(!std::isfinite(before[n])||!std::isfinite(after[n])||before[n]<0||before[n]>1||
        after[n]<before[n]-1e-6f||after[n]>1)return false;
  return true;
}
}
