#pragma once
#ifndef NOMINMAX
#define NOMINMAX
#endif
#include <windows.h>
#include "cloth_bonecloth_cache.h"
#include "cloth_embedded_resources.h"

namespace eiem_cloth_cache {
enum class Source { Embedded, Rejected };
inline const char *SourceName(Source source) {
  switch(source) {
    case Source::Embedded:return "embedded";
    default:return "rejected";
  }
}
inline Source LoadBytes(HMODULE module,std::vector<unsigned char> &bytes,DWORD &error) {
  bytes.clear();error=0;
  return eiem_cloth_resource::Load(module,4305,MaxBytes+20,bytes,error)
      ? Source::Embedded : Source::Rejected;
}
}
