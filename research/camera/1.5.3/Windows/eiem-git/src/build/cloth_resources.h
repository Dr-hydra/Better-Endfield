#pragma once
#include <algorithm>
#include <array>
#include <cstdint>
#include <cstring>
#include <filesystem>
#include <fstream>
#include <iomanip>
#include <iostream>
#include <iterator>
#include <map>
#include <set>
#include <sstream>
#include <stdexcept>
#include <string>
#include <utility>
#include <vector>
#include "../cloth/resources/cloth_embedded_resources.h"
#include <bcrypt.h>
#pragma comment(lib, "bcrypt.lib")

struct Vector3 { float x, y, z; };
struct Quaternion { float x, y, z, w; };
#include "../cloth/bonecloth/cloth_bonecloth_recipe.h"
#include "../cloth/resources/cloth_bonecloth_cache.h"
#include "../cloth/generated/cloth_bonecloth_local_generated.h"
#include "../cloth/generated/cloth_bonecloth_layer_generated.h"
#include "../cloth/generated/cloth_bonecloth_strip_generated.h"

namespace eiem_build {
namespace fs = std::filesystem;
using Bytes = std::vector<unsigned char>;
using eiem_cloth_resource::EmbeddedLimit;
using eiem_cloth_resource::ResourceHash;

inline void Require(bool valid, const std::string &message) {
  if (!valid) throw std::runtime_error(message);
}
inline Bytes Read(const fs::path &path, size_t limit) {
  std::ifstream in(path, std::ios::binary | std::ios::ate);
  Require(bool(in), "cannot read " + path.u8string());
  const auto size = in.tellg();
  Require(size > 0 && static_cast<uint64_t>(size) <= limit, "file size outside budget: " + path.u8string());
  Bytes data(static_cast<size_t>(size));
  in.seekg(0); in.read(reinterpret_cast<char *>(data.data()), size);
  Require(bool(in) && in.peek() == std::char_traits<char>::eof(), "incomplete/changed file: " + path.u8string());
  return data;
}
inline void Write(const fs::path &path, const Bytes &data) {
  if (fs::exists(path) && fs::file_size(path) == data.size() &&
      (data.empty() || Read(path, data.size()) == data)) return;
  auto temporary = path;
  temporary += L"." + std::to_wstring(GetCurrentProcessId()) + L".tmp";
  try {
    std::ofstream out(temporary, std::ios::binary | std::ios::trunc);
    Require(bool(out), "cannot write " + temporary.u8string());
    out.write(reinterpret_cast<const char *>(data.data()), static_cast<std::streamsize>(data.size()));
    out.close(); Require(bool(out), "incomplete write: " + temporary.u8string());
    Require(MoveFileExW(temporary.c_str(), path.c_str(), MOVEFILE_REPLACE_EXISTING | MOVEFILE_WRITE_THROUGH) != FALSE,
            "cannot replace " + path.u8string());
  } catch (...) {
    std::error_code ignored; fs::remove(temporary, ignored); throw;
  }
}
template<class T> inline T Value(const Bytes &data, size_t offset) {
  Require(offset <= data.size() && sizeof(T) <= data.size() - offset, "binary range is not backed by bytes");
  T result{}; std::memcpy(&result, data.data() + offset, sizeof(T)); return result;
}
template<class T> inline void Put(Bytes &data, size_t offset, T value) {
  Require(offset <= data.size() && sizeof(T) <= data.size() - offset, "output range is not backed by bytes");
  std::memcpy(data.data() + offset, &value, sizeof(T));
}
inline Bytes Pack(unsigned id, const Bytes &original) {
  Require(id >= 4303 && id <= 4314 && !original.empty() && original.size() <= EmbeddedLimit,
          "resource identity/size outside contract");
  COMPRESSOR_HANDLE handle = nullptr;
  Require(CreateCompressor(COMPRESS_ALGORITHM_LZMS, nullptr, &handle) != FALSE, "CreateCompressor failed");
  Bytes compressed;
  try {
    SIZE_T count = 0;
    const BOOL queried = Compress(handle, original.data(), original.size(), nullptr, 0, &count);
    Require(!queried && GetLastError() == ERROR_INSUFFICIENT_BUFFER && count && count <= EmbeddedLimit + 65536,
            "compression size query failed");
    compressed.resize(count);
    Require(Compress(handle, original.data(), original.size(), compressed.data(), compressed.size(), &count) != FALSE,
            "compression failed");
    compressed.resize(count);
  } catch (...) { CloseCompressor(handle); throw; }
  CloseCompressor(handle);
  const bool smaller = compressed.size() < original.size();
  const auto &payload = smaller ? compressed : original;
  Bytes packed(32 + payload.size());
  std::memcpy(packed.data(), "EIEMRS01", 8);
  Put<uint32_t>(packed, 8, id); Put<uint32_t>(packed, 12, static_cast<uint32_t>(original.size()));
  Put<uint32_t>(packed, 16, static_cast<uint32_t>(payload.size()));
  Put<uint32_t>(packed, 20, smaller ? COMPRESS_ALGORITHM_LZMS : 0);
  Put<uint64_t>(packed, 24, ResourceHash(original.data(), original.size()));
  std::copy(payload.begin(), payload.end(), packed.begin() + 32);
  Bytes decoded; DWORD error = 0;
  Require(eiem_cloth_resource::Decode(id, packed.data(), packed.size(), original.size(), decoded, error) && decoded == original,
          "resource round trip mismatch");
  return packed;
}

struct Contract {
  unsigned id;
  std::string file;
  size_t bytes;
  uint64_t hash;
  std::string name;
};
struct Resource { Contract contract; Bytes original, packed; };

inline std::vector<Contract> Contracts(const Bytes &catalog) {
  Require(catalog.size() >= 20 && !std::memcmp(catalog.data(), "EIEMBC05", 8), "invalid catalog header");
  eiem_cloth_cache::Catalog parsed;
  Require(parsed.Parse(catalog.data(), catalog.size()), "invalid catalog content/checksum");
  std::vector<Contract> result{
      {4303, "cloth_bone_local.bundle", ClothLocalBundleBytes, ClothLocalBundleHash, ClothLocalBundleName},
      {4304, "cloth_bone_inner.bundle", ClothInnerBundleBytes, ClothInnerBundleHash, ClothInnerBundleName},
      {4305, "cloth_profiles.bin", catalog.size(), ResourceHash(catalog.data(), catalog.size()), "embedded-catalog"}};
  Require(ClothStripRecipe.meshCount == std::size(ClothStripMeshes) && ClothStripRecipe.meshes == ClothStripMeshes,
          "generated strip resource count/identity mismatch");
  for (size_t n = 0; n < std::size(ClothStripMeshes); ++n) {
    const auto &mesh = ClothStripMeshes[n];
    Require(mesh.resource == 4306 + n && mesh.bundleName, "generated strip resource ID/name mismatch");
    result.push_back({mesh.resource, "cloth_bone_strip_" + std::to_string(n) + ".bundle", mesh.bytes, mesh.hash, mesh.bundleName});
  }
  Require(result.size() == eiem_cloth_resource::LastResource - eiem_cloth_resource::FirstResource + 1,
          "embedded cache and generated resource count disagree");
  std::set<unsigned> ids; std::set<std::string> names;
  for (const auto &r : result)
    Require(r.id >= 4303 && r.id <= 4314 && ids.insert(r.id).second && !r.name.empty() && names.insert(r.name).second &&
                r.bytes && r.bytes <= (r.id == 4305 ? EmbeddedLimit : 2u * 1024u * 1024u),
            "invalid/duplicate generated resource contract");
  return result;
}
inline void CheckRc(const std::string &text, const std::vector<Contract> &contracts) {
  std::map<unsigned, std::string> wanted;
  for (const auto &r : contracts) wanted.emplace(r.id, "bin/cloth/resources/" + std::to_string(r.id) + ".bin");
  std::set<unsigned> seen; std::istringstream lines(text); std::string line;
  while (std::getline(lines, line)) {
    std::istringstream row(line); unsigned id = 0; std::string kind, path;
    if (!(row >> id) || !wanted.count(id)) continue;
    Require(bool(row >> kind >> std::quoted(path)) && kind == "RCDATA" && seen.insert(id).second,
            "invalid/duplicate version.rc entry");
    std::replace(path.begin(), path.end(), '\\', '/');
    Require(path == wanted.at(id), "version.rc path does not match resource " + std::to_string(id));
  }
  Require(seen.size() == wanted.size(), "version.rc is missing a required cloth resource");
}
inline std::vector<Resource> Sources(const fs::path &repo) {
  const auto catalog = Read(repo / "assets/cloth/cloth_profiles.bin", EmbeddedLimit);
  const auto contracts = Contracts(catalog);
  const auto rc = Read(repo / "src/version.rc", 1024 * 1024);
  CheckRc(std::string(rc.begin(), rc.end()), contracts);
  std::vector<Resource> result;
  for (const auto &c : contracts) {
    auto bytes = Read(repo / "assets/cloth" / c.file, c.bytes);
    Require(bytes.size() == c.bytes && ResourceHash(bytes.data(), bytes.size()) == c.hash,
            "stale/corrupt source resource " + std::to_string(c.id) + " " + c.file);
    result.push_back({c, std::move(bytes), {}});
  }
  return result;
}
inline void PackAll(const fs::path &repo) {
  auto resources = Sources(repo); size_t original = 0, packed = 0;
  for (auto &r : resources) {
    r.packed = Pack(r.contract.id, r.original);
    original += r.original.size(); packed += r.packed.size();
  }
  const auto output = repo / "bin/cloth/resources"; fs::create_directories(output);
  for (const auto &r : resources) Write(output / (std::to_string(r.contract.id) + ".bin"), r.packed);
  std::cout << "Packed " << original << " -> " << packed << " bytes; " << resources.size() << " contracts verified\n";
}

inline std::map<unsigned, Bytes> PeResources(const Bytes &data) {
  Require(Value<uint16_t>(data, 0) == 0x5a4d, "not a PE file");
  const size_t pe = Value<uint32_t>(data, 60);
  Require(Value<uint32_t>(data, pe) == 0x4550, "invalid PE signature");
  const auto machine = Value<uint16_t>(data, pe + 4), count = Value<uint16_t>(data, pe + 6);
  const auto optionalSize = Value<uint16_t>(data, pe + 20); const size_t opt = pe + 24;
  Require(machine == 0x8664 && count && count <= 96 && optionalSize >= 136 &&
              Value<uint16_t>(data, opt) == 0x20b && Value<uint32_t>(data, opt + 108) >= 3,
          "expected AMD64 PE32+ with resources");
  Require(opt <= data.size() && optionalSize <= data.size() - opt, "truncated PE optional header");
  struct Section { uint32_t start, size, file; };
  std::vector<Section> sections;
  for (unsigned n = 0; n < count; ++n) {
    const size_t offset = opt + optionalSize + n * 40;
    Value<uint32_t>(data, offset + 36);
    sections.push_back({Value<uint32_t>(data, offset + 12), Value<uint32_t>(data, offset + 16), Value<uint32_t>(data, offset + 20)});
  }
  const auto mapped = [&](uint64_t rva, size_t size) {
    size_t found = 0, file = 0;
    for (const auto &s : sections) if (rva >= s.start && rva + size <= uint64_t(s.start) + s.size) {
      ++found; file = size_t(s.file) + size_t(rva - s.start);
    }
    Require(found == 1 && file <= data.size() && size <= data.size() - file, "PE RVA is unbacked or ambiguous");
    return file;
  };
  const auto root = Value<uint32_t>(data, opt + 128), limit = Value<uint32_t>(data, opt + 132);
  Require(root && limit >= 16 && limit <= 64 * 1024 * 1024, "missing/oversized PE resource directory");
  const auto relative = [&](size_t offset, size_t size) {
    Require(offset <= limit && size <= limit - offset, "resource directory offset out of range");
    return mapped(uint64_t(root) + offset, size);
  };
  const auto entries = [&](size_t offset) {
    const auto header = relative(offset, 16);
    const unsigned length = unsigned(Value<uint16_t>(data, header + 12)) + Value<uint16_t>(data, header + 14);
    Require(length <= 4096, "resource entry budget exceeded");
    const auto rows = relative(offset + 16, length * 8); std::map<unsigned, unsigned> result;
    for (unsigned n = 0; n < length; ++n)
      Require(result.emplace(Value<uint32_t>(data, rows + n * 8), Value<uint32_t>(data, rows + n * 8 + 4)).second,
              "duplicate resource identity");
    return result;
  };
  const auto directory = [&](uint32_t target) {
    Require((target & 0x80000000u) != 0, "missing resource directory");
    return entries(target & 0x7fffffffu);
  };
  const auto types = entries(0); Require(types.count(10) == 1, "no RCDATA in DLL");
  std::map<unsigned, Bytes> result;
  for (const auto &entry : directory(types.at(10))) {
    if (entry.first & 0x80000000u) continue;
    const auto languages = directory(entry.second);
    Require(languages.size() == 1, "ambiguous/absent resource language");
    const auto leaf = languages.begin()->second;
    Require(!(leaf & 0x80000000u), "unexpected nested resource directory");
    const auto info = relative(leaf, 16);
    const auto rva = Value<uint32_t>(data, info), size = Value<uint32_t>(data, info + 4);
    Require(size && size <= EmbeddedLimit + 32, "resource data budget exceeded");
    const auto file = mapped(rva, size);
    result.emplace(entry.first, Bytes(data.begin() + file, data.begin() + file + size));
  }
  return result;
}
inline void CheckEmbedded(const Resource &r, const Bytes &packed) {
  Bytes decoded; DWORD error = 0;
  Require(eiem_cloth_resource::Decode(r.contract.id, packed.data(), packed.size(), r.contract.bytes, decoded, error) &&
              decoded == r.original, "DLL differs from source resource " + std::to_string(r.contract.id));
}
inline std::string Sha256(const Bytes &data) {
  BCRYPT_ALG_HANDLE algorithm = nullptr;
  Require(BCryptOpenAlgorithmProvider(&algorithm, BCRYPT_SHA256_ALGORITHM, nullptr, 0) >= 0, "SHA-256 provider unavailable");
  std::array<unsigned char, 32> hash{};
  const auto status = BCryptHash(algorithm, nullptr, 0, const_cast<PUCHAR>(data.data()), static_cast<ULONG>(data.size()), hash.data(), static_cast<ULONG>(hash.size()));
  BCryptCloseAlgorithmProvider(algorithm, 0);
  Require(status >= 0, "SHA-256 failed");
  std::ostringstream out; out << std::hex << std::setfill('0');
  for (const auto value : hash) out << std::setw(2) << unsigned(value);
  return out.str();
}
inline void Verify(const fs::path &repo, const fs::path &dll, const fs::path &report = {}) {
  const auto resources = Sources(repo); const auto embedded = PeResources(Read(dll, 256 * 1024 * 1024));
  std::ostringstream json; json << "[\n";
  for (size_t n = 0; n < resources.size(); ++n) {
    const auto &r = resources[n]; const auto found = embedded.find(r.contract.id);
    Require(found != embedded.end(), "DLL missing resource " + std::to_string(r.contract.id));
    CheckEmbedded(r, found->second);
    json << "  {\"resource\": " << r.contract.id << ", \"file\": \"" << r.contract.file << "\", \"bytes\": " << r.original.size()
         << ", \"fnv\": \"" << std::hex << std::setfill('0') << std::setw(16) << r.contract.hash << std::dec
         << "\", \"sha256\": \"" << Sha256(r.original) << "\", \"storedBytes\": " << found->second.size()
         << ", \"embeddedConfirmed\": true}" << (n + 1 < resources.size() ? ",\n" : "\n");
  }
  json << "]\n";
  if (!report.empty()) { const auto text = json.str(); Write(report, Bytes(text.begin(), text.end())); }
  std::cout << "PASS: " << resources.size() << " native-cloth contracts; decoded DLL and source bytes identical\n";
}
}
