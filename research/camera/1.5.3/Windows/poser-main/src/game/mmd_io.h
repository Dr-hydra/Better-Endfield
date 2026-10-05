#pragma once
#include "math/mmd_rig.h"
#include <filesystem>
#include <fstream>
#include <windows.h>

namespace mmd {
inline std::string Utf8(const std::wstring &w) {
  if (w.empty())
    return {};
  int n = WideCharToMultiByte(CP_UTF8, 0, w.data(), int(w.size()), nullptr, 0,
                              nullptr, nullptr);
  std::string s(n, 0);
  WideCharToMultiByte(CP_UTF8, 0, w.data(), int(w.size()), s.data(), n, nullptr,
                      nullptr);
  return s;
}
inline std::wstring Wide(const std::string &s, unsigned cp = CP_UTF8) {
  if (s.empty())
    return {};
  int n = MultiByteToWideChar(cp, 0, s.data(), int(s.size()), nullptr, 0);
  if (!n)
    throw std::runtime_error("Cannot decode MMD text");
  std::wstring w(n, 0);
  MultiByteToWideChar(cp, 0, s.data(), int(s.size()), w.data(), n);
  return w;
}
inline std::string Decode(const std::string &s, unsigned cp) {
  if (cp == 1200) {
    if (s.size() % 2)
      throw std::runtime_error("Invalid UTF-16 PMX string");
    std::wstring w(s.size() / 2, 0);
    std::memcpy(w.data(), s.data(), s.size());
    return Utf8(w);
  }
  return Utf8(Wide(s, cp));
}
inline std::vector<uint8_t> ReadFile(const std::filesystem::path &path) {
  std::ifstream f(path, std::ios::binary | std::ios::ate);
  if (!f)
    throw std::runtime_error("Cannot open MMD file");
  auto len = f.tellg();
  if (len < 0 || uint64_t(len) > 256ull * 1024 * 1024)
    throw std::runtime_error("MMD file exceeds 256 MiB");
  std::vector<uint8_t> data(static_cast<size_t>(len));
  f.seekg(0);
  if (!data.empty() &&
      !f.read(reinterpret_cast<char *>(data.data()), data.size()))
    throw std::runtime_error("Cannot read MMD file");
  return data;
}
inline MotionClip ReadVmdFile(const std::filesystem::path &path) {
  std::ifstream file(path, std::ios::binary | std::ios::ate);
  if (!file) throw std::runtime_error(u8"无法打开 VMD 文件");
  auto length = file.tellg();
  if (length < 0) throw std::runtime_error(u8"无法读取 VMD 文件大小");
  if (uint64_t(length) > MaxVmdFileBytes)
    throw std::runtime_error(u8"VMD 超过 1 GiB，保留此前动作");
  file.seekg(0);
  Reader reader(file, size_t(length));
  return ReadVmd(reader, Decode);
}
} // namespace mmd
