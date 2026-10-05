#pragma once
#include <cstring>
namespace eiem_cloth_asset {
inline bool SourceBodyContact(const char *sha,const char *component) {
  return sha&&component&&!strcmp(sha,"be354bab7fe43f2b82d3e752fa734dc5dac323c8808f79b98b74be4b5a6e8d63")&&
      !strcmp(component,"MC_Laevat_Skirt");
}
}
