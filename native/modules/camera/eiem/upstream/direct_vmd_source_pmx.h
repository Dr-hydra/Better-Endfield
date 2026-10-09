#pragma once
#include <functional>
#include <set>
#include <stdexcept>

namespace eiem_source {
inline bool Finite(VmdVec3 v) {
  return std::isfinite(v.x) && std::isfinite(v.y) && std::isfinite(v.z);
}
inline void Require(bool ok, const std::string &message) {
  if (!ok)
    throw std::runtime_error(message);
}
inline void Require(bool ok, const char *message) {
  if (!ok)
    throw std::runtime_error(message);
}
inline std::wstring Wide(const std::string &s) {
  Require(s.find('\0') == std::string::npos, "Embedded NUL in text");
  if (s.empty())
    return {};
#if defined(__ANDROID__)
  Require(BetterEndfieldNext::EiemAndroid::ValidUtf8(s), "Invalid UTF-8");
  return BetterEndfieldNext::EiemAndroid::Utf8ToWide(s);
#else
  const int n = MultiByteToWideChar(CP_UTF8, MB_ERR_INVALID_CHARS, s.data(),
                                    int(s.size()), nullptr, 0);
  Require(n > 0, "Invalid UTF-8");
  std::wstring w(size_t(n), L'\0');
  MultiByteToWideChar(CP_UTF8, MB_ERR_INVALID_CHARS, s.data(), int(s.size()),
                      &w[0], n);
  return w;
#endif
}
inline std::vector<uint8_t> ReadBytes(const std::string &path, size_t budget) {
  FILE *raw = nullptr;
  const auto wide = Wide(path);
#if defined(__ANDROID__)
  raw = fopen(path.c_str(), "rb");
  Require(raw != nullptr, "Cannot open: " + path);
#else
  Require(_wfopen_s(&raw, wide.c_str(), L"rb") == 0 && raw,
          "Cannot open: " + path);
#endif
  std::unique_ptr<FILE, decltype(&fclose)> file(raw, fclose);
  Require(VmdFileSeek(raw, 0, SEEK_END) == 0, "Cannot seek file");
  const auto size = VmdFileTell(raw);
  Require(size >= 0 && uint64_t(size) <= budget, "File size budget exceeded");
  Require(VmdFileSeek(raw, 0, SEEK_SET) == 0, "Cannot rewind file");
  std::vector<uint8_t> bytes(static_cast<size_t>(size));
  Require(bytes.empty() ||
              fread(bytes.data(), 1, bytes.size(), raw) == bytes.size(),
          "Short file read");
  return bytes;
}
struct PmxIkLink {
  int bone = -1;
  bool limited = false;
  VmdVec3 lo{}, hi{};
};
struct PmxBone {
  std::string name, english;
  VmdVec3 position{}, fixedAxis{};
  int parent = -1, tail = -1, append = -1, layer = 0;
  uint16_t flags = 0;
  float weight = 0;
  int ikTarget = -1, ikLoops = 0;
  float ikAngle = 0;
  std::vector<PmxIkLink> links;
};
struct PmxSkeleton {
  std::vector<PmxBone> bones;
  std::vector<int> order;
  std::set<int> physicsBones, morphBones;
};
class PmxReader {
  const std::vector<uint8_t> &bytes;
  size_t offset = 0, textBytes = 0;

public:
  explicit PmxReader(const std::vector<uint8_t> &b) : bytes(b) {}
  void Skip(size_t n) {
    if (n > bytes.size() - offset)
      throw std::runtime_error("Truncated PMX at byte " +
                               std::to_string(offset));
    offset += n;
  }
  template <class T> T Get() {
    T v;
    const size_t p = offset;
    Skip(sizeof(T));
    memcpy(&v, bytes.data() + p, sizeof(T));
    return v;
  }
  uint8_t U8() { return Get<uint8_t>(); }
  int Count(int max) {
    int n = Get<int32_t>();
    Require(n >= 0 && n <= max, "PMX count budget exceeded");
    return n;
  }
  float Float() {
    float f = Get<float>();
    Require(std::isfinite(f), "Non-finite PMX value");
    return f;
  }
  VmdVec3 Vec() {
    const float x = Float(), y = Float(), z = Float();
    return {x, y, z};
  }
  void Floats(int n) {
    while (n--)
      Float();
  }
  int Index(int width, bool vertex = false) {
    if (width == 1)
      return vertex ? int(Get<uint8_t>()) : int(Get<int8_t>());
    if (width == 2)
      return vertex ? int(Get<uint16_t>()) : int(Get<int16_t>());
    Require(width == 4, "Invalid PMX index width");
    return Get<int32_t>();
  }
  std::string Text(bool utf8) {
    const int n = Count(65536);
    textBytes += size_t(n);
    Require(textBytes <= 4 * 1024 * 1024, "PMX text budget exceeded");
    const size_t p = offset;
    Skip(size_t(n));
    if (!n)
      return {};
    if (utf8) {
      std::string s(reinterpret_cast<const char *>(bytes.data() + p), n);
      Wide(s);
      return s;
    }
    Require(n % 2 == 0, "Odd UTF-16 PMX text length");
#if defined(__ANDROID__)
    const auto decoded = BetterEndfieldNext::EiemAndroid::DecodeUtf16LE(bytes.data() + p, size_t(n));
    Require(!decoded.empty() && decoded.find('\0') == std::string::npos,
            "Invalid UTF-16 in PMX");
    return decoded;
#else
    std::wstring w(size_t(n / 2), L'\0');
    memcpy(&w[0], bytes.data() + p, size_t(n));
    Require(w.find(L'\0') == std::wstring::npos, "Embedded NUL in PMX text");
    const int count =
        WideCharToMultiByte(CP_UTF8, WC_ERR_INVALID_CHARS, w.data(), n / 2,
                            nullptr, 0, nullptr, nullptr);
    Require(count > 0, "Invalid UTF-16 in PMX");
    std::string s(size_t(count), '\0');
    WideCharToMultiByte(CP_UTF8, WC_ERR_INVALID_CHARS, w.data(), n / 2, &s[0],
                        count, nullptr, nullptr);
    return s;
#endif
  }
};
inline void ValidateGraph(PmxSkeleton &s) {
  const int n = int(s.bones.size());
  auto index = [n](int i) {
    Require(i >= -1 && i < n, "PMX bone index out of range");
  };
  std::set<std::string> names;
  for (int i = 0; i < n; ++i) {
    const auto &b = s.bones[i];
    Require(!b.name.empty() && names.insert(b.name).second,
            "Empty/duplicate PMX bone name: " + b.name);
    Require(b.name.size() <= 512 && b.english.size() <= 512,
            "PMX bone name too long");
    Require(Finite(b.position) && DirectVmdLength(b.position) < 1000000.0f &&
                std::isfinite(b.weight),
            "Non-finite skeleton");
    index(b.parent);
    index(b.tail);
    index(b.append);
    index(b.ikTarget);
    for (const auto &l : b.links) {
      index(l.bone);
      Require(l.bone >= 0, "Missing IK link");
    }
  }
  std::vector<int> degree(size_t(n), 0);
  std::vector<std::vector<int>> children(static_cast<size_t>(n));
  for (int i = 0; i < n; ++i)
    for (int d : {s.bones[i].parent, s.bones[i].append})
      if (d >= 0) {
        ++degree[i];
        children[d].push_back(i);
      }
  s.order.clear();
  for (int i = 0; i < n; ++i)
    if (!degree[i])
      s.order.push_back(i);
  for (size_t p = 0; p < s.order.size(); ++p)
    for (int c : children[s.order[p]])
      if (--degree[c] == 0)
        s.order.push_back(c);
  Require(s.order.size() == s.bones.size(),
          "PMX parent/append dependency cycle");
}
inline PmxSkeleton ParsePmx(const std::vector<uint8_t> &bytes) {
  Require(bytes.size() <= 256ull * 1024 * 1024, "PMX exceeds 256 MiB budget");
  PmxReader r(bytes);
  PmxSkeleton s;
  Require(r.Get<uint32_t>() == 0x20584d50, "Invalid PMX signature");
  const float version = r.Float();
  Require(version == 2.0f || version == 2.1f, "Only PMX 2.0/2.1 supported");
  Require(r.U8() == 8, "Unsupported PMX header extension");
  const int encoding = r.U8(), uv = r.U8();
  Require(encoding <= 1 && uv <= 4, "Invalid PMX encoding/additional UV count");
  const bool utf8 = encoding == 1;
  const int vi = r.U8(), ti = r.U8(), mi = r.U8(), bi = r.U8(), moi = r.U8(),
            ri = r.U8();
  for (int w : {vi, ti, mi, bi, moi, ri})
    Require(w == 1 || w == 2 || w == 4, "Invalid PMX index width");
  for (int i = 0; i < 4; ++i)
    r.Text(utf8);
  const int vertices = r.Count(2000000);
  int maxSkinBone = -1;
  auto skinIndex = [&]() {
    int i = r.Index(bi);
    Require(i >= -1, "Invalid skin bone index");
    maxSkinBone = (std::max)(maxSkinBone, i);
  };
  for (int i = 0; i < vertices; ++i) {
    r.Floats(8 + 4 * uv);
    int type = r.U8();
    if (type == 0)
      skinIndex();
    else if (type == 1 || type == 3) {
      skinIndex();
      skinIndex();
      r.Float();
      if (type == 3)
        r.Floats(9);
    } else if (type == 2 || (type == 4 && version == 2.1f)) {
      for (int j = 0; j < 4; ++j)
        skinIndex();
      r.Floats(4);
    } else
      Require(false, "Unsupported PMX skinning type");
    r.Float();
  }
  const int faces = r.Count(6000000);
  Require(faces % 3 == 0, "Invalid PMX triangle index count");
  for (int i = 0; i < faces; ++i) {
    int v = r.Index(vi, true);
    Require(v >= 0 && v < vertices, "PMX vertex index out of range");
  }
  const int textures = r.Count(16384);
  for (int i = 0; i < textures; ++i)
    r.Text(utf8);
  const int materials = r.Count(16384);
  int64_t materialFaces = 0;
  for (int i = 0; i < materials; ++i) {
    r.Text(utf8);
    r.Text(utf8);
    r.Floats(11);
    r.U8();
    r.Floats(5);
    for (int j = 0; j < 2; ++j) {
      int t = r.Index(ti);
      Require(t >= -1 && t < textures, "PMX texture index out of range");
    }
    Require(r.U8() <= 3, "Invalid sphere mode");
    const int toon = r.U8();
    Require(toon <= 1, "Invalid toon mode");
    if (toon)
      Require(r.U8() <= 9, "Invalid shared toon index");
    else {
      int t = r.Index(ti);
      Require(t >= -1 && t < textures, "Invalid toon texture index");
    }
    r.Text(utf8);
    materialFaces += r.Count(faces);
  }
  Require(materialFaces == faces, "PMX material face count mismatch");
  const int bones = r.Count(4096);
  Require(bones > 0 && maxSkinBone < bones,
          "Invalid PMX skeleton size/skin index");
  s.bones.resize(size_t(bones));
  auto boneIndex = [&](bool optional = true) {
    int b = r.Index(bi);
    Require(b >= (optional ? -1 : 0) && b < bones,
            "PMX bone index out of range");
    return b;
  };
  for (auto &b : s.bones) {
    b.name = r.Text(utf8);
    b.english = r.Text(utf8);
    b.position = r.Vec();
    b.parent = boneIndex();
    b.layer = r.Get<int32_t>();
    b.flags = r.Get<uint16_t>();
    Require((b.flags & 0xc040) == 0, "Unknown PMX bone flags");
    if (b.flags & 1)
      b.tail = boneIndex();
    else
      r.Vec();
    if (b.flags & 0x300) {
      b.append = boneIndex(false);
      b.weight = r.Float();
      Require(std::fabs(b.weight) <= 16, "PMX append weight budget exceeded");
    }
    if (b.flags & 0x400)
      b.fixedAxis = r.Vec();
    if (b.flags & 0x800) {
      r.Vec();
      r.Vec();
    }
    if (b.flags & 0x2000)
      r.Get<int32_t>();
    if (b.flags & 0x20) {
      b.ikTarget = boneIndex(false);
      b.ikLoops = r.Count(4096);
      b.ikAngle = r.Float();
      Require(b.ikAngle >= 0, "Negative PMX IK angle");
      int links = r.Count(256);
      for (int j = 0; j < links; ++j) {
        PmxIkLink l;
        l.bone = boneIndex(false);
        const auto flag = r.U8();
        Require(flag <= 1, "Invalid IK limit flag");
        l.limited = flag != 0;
        if (l.limited) {
          l.lo = r.Vec();
          l.hi = r.Vec();
          Require(l.lo.x <= l.hi.x && l.lo.y <= l.hi.y && l.lo.z <= l.hi.z,
                  "Invalid IK limit interval");
        }
        b.links.push_back(l);
      }
    }
  }
  ValidateGraph(s);
  const int morphs = r.Count(16384);
  for (int i = 0; i < morphs; ++i) {
    r.Text(utf8);
    r.Text(utf8);
    r.U8();
    const int type = r.U8(), count = r.Count(2000000);
    for (int j = 0; j < count; ++j) {
      switch (type) {
      case 0:
      case 9: {
        int m = r.Index(moi);
        Require(m >= 0 && m < morphs, "Invalid morph dependency");
        r.Float();
        break;
      }
      case 1:
      case 3:
      case 4:
      case 5:
      case 6:
      case 7: {
        int v = r.Index(vi, true);
        Require(v >= 0 && v < vertices, "Invalid morph vertex");
        r.Floats(type == 1 ? 3 : 4);
        break;
      }
      case 2:
        s.morphBones.insert(boneIndex(false));
        r.Floats(7);
        break;
      case 8: {
        int m = r.Index(mi);
        Require(m >= -1 && m < materials, "Invalid morph material");
        Require(r.U8() <= 1, "Invalid material morph mode");
        r.Floats(28);
        break;
      }
      case 10:
        r.Index(ri);
        Require(r.U8() <= 1, "Invalid impulse local flag");
        r.Floats(6);
        break;
      default:
        Require(false, "Unsupported PMX morph type");
      }
    }
  }
  const int frames = r.Count(16384);
  for (int i = 0; i < frames; ++i) {
    r.Text(utf8);
    r.Text(utf8);
    r.U8();
    int count = r.Count(65536);
    for (int j = 0; j < count; ++j) {
      const int type = r.U8();
      Require(type <= 1, "Invalid display element type");
      if (type) {
        int m = r.Index(moi);
        Require(m >= 0 && m < morphs, "Invalid display morph");
      } else
        boneIndex(false);
    }
  }
  const int rigid = r.Count(16384);
  for (int i = 0; i < rigid; ++i) {
    r.Text(utf8);
    r.Text(utf8);
    int b = boneIndex();
    r.U8();
    r.Get<uint16_t>();
    Require(r.U8() <= 2, "Invalid rigid shape");
    r.Floats(14);
    int mode = r.U8();
    Require(mode <= 2, "Invalid rigid mode");
    if (mode && b >= 0)
      s.physicsBones.insert(b);
  }
  return s;
}
}
