#pragma once
#include <algorithm>
#include <cstddef>
#include <cstdint>
#include <string>
#include "android_cp932_table.h"

namespace BetterEndfield::EiemAndroid {
inline void AppendUtf8(std::string& out, uint32_t cp) {
  if (cp < 0x80) out += char(cp);
  else if (cp < 0x800) {
    out += char(0xc0 | (cp >> 6)); out += char(0x80 | (cp & 63));
  } else if (cp < 0x10000) {
    out += char(0xe0 | (cp >> 12)); out += char(0x80 | ((cp >> 6) & 63)); out += char(0x80 | (cp & 63));
  } else {
    out += char(0xf0 | (cp >> 18)); out += char(0x80 | ((cp >> 12) & 63));
    out += char(0x80 | ((cp >> 6) & 63)); out += char(0x80 | (cp & 63));
  }
}
// Strict decoding: invalid input has no fabricated/replacement bone name.
inline std::string DecodeCp932(const char* data, size_t length) {
  std::string out;
  for (size_t i = 0; i < length; ++i) {
    const auto lead = static_cast<uint8_t>(data[i]);
    uint32_t cp = lead;
    if (lead >= 0xa1 && lead <= 0xdf) cp = 0xff61 + lead - 0xa1;
    else if (lead == 0x80) cp = 0x80;
    else if (lead == 0xa0) cp = 0xf8f0;
    else if (lead >= 0xfd) cp = 0xf8f1 + lead - 0xfd;
    else if (lead >= 0x81) {
      if (++i == length) return {};
      const uint32_t key = (uint32_t(lead) << 8) | static_cast<uint8_t>(data[i]);
      const auto* first = kCp932Pairs;
      const auto* end = first + sizeof(kCp932Pairs) / sizeof(*first);
      const auto* p = std::lower_bound(first, end, key << 16);
      if (p == end || (*p >> 16) != key) return {};
      cp = *p & 0xffff;
    }
    AppendUtf8(out, cp);
  }
  return out;
}
inline bool NextUtf8(const std::string& s, size_t& i, uint32_t& cp) {
  const auto lead = static_cast<uint8_t>(s[i++]);
  if (lead < 0x80) { cp = lead; return true; }
  int extra;
  uint32_t minimum;
  if (lead >= 0xc2 && lead <= 0xdf) { extra = 1; minimum = 0x80; cp = lead & 31; }
  else if (lead >= 0xe0 && lead <= 0xef) { extra = 2; minimum = 0x800; cp = lead & 15; }
  else if (lead >= 0xf0 && lead <= 0xf4) { extra = 3; minimum = 0x10000; cp = lead & 7; }
  else return false;
  while (extra--) {
    if (i == s.size()) return false;
    const auto byte = static_cast<uint8_t>(s[i++]);
    if ((byte & 0xc0) != 0x80) return false;
    cp = (cp << 6) | (byte & 63);
  }
  return cp >= minimum && cp <= 0x10ffff && !(cp >= 0xd800 && cp <= 0xdfff);
}
inline bool ValidUtf8(const std::string& s) {
  size_t i = 0; uint32_t cp;
  while (i < s.size()) if (!NextUtf8(s, i, cp)) return false;
  return true;
}
inline std::wstring Utf8ToWide(const std::string& s) {
  std::wstring out;
  size_t i = 0; uint32_t cp;
  while (i < s.size()) {
    if (!NextUtf8(s, i, cp)) return {};
    if constexpr (sizeof(wchar_t) == 2) {
      if (cp > 0xffff) {
        cp -= 0x10000;
        out += static_cast<wchar_t>(0xd800 + (cp >> 10));
        out += static_cast<wchar_t>(0xdc00 + (cp & 1023));
      } else out += static_cast<wchar_t>(cp);
    } else out += static_cast<wchar_t>(cp);
  }
  return out;
}
inline std::string DecodeUtf16LE(const uint8_t* data, size_t size) {
  if (size % 2) return {};
  std::string out;
  for (size_t i = 0; i < size; i += 2) {
    uint32_t cp = uint32_t(data[i]) | (uint32_t(data[i + 1]) << 8);
    if (cp >= 0xd800 && cp <= 0xdbff) {
      i += 2;
      if (i == size) return {};
      const uint32_t low = uint32_t(data[i]) | (uint32_t(data[i + 1]) << 8);
      if (low < 0xdc00 || low > 0xdfff) return {};
      cp = 0x10000 + ((cp - 0xd800) << 10) + low - 0xdc00;
    } else if (cp >= 0xdc00 && cp <= 0xdfff) return {};
    AppendUtf8(out, cp);
  }
  return out;
}
}
