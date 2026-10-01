#pragma once
#define _CRT_SECURE_NO_WARNINGS

#include <algorithm>
#include <array>
#include <cmath>
#include <cstddef>
#include <cstdint>
#include <cstdio>
#include <cstring>
#include <limits>
#include <map>
#include <memory>
#include <new>
#include <string>
#include <utility>
#include <vector>
#if defined(__ANDROID__)
#include "../compat/android_encoding.h"
#else
#include <windows.h>
#endif


enum class VmdSection : uint8_t {
  Header = 0,
  Bone,
  Morph,
  Camera,
  Light,
  SelfShadow,
  ModelDisplayIk,
  Count
};

struct VmdSectionBoundary {
  bool present = false;
  size_t begin = 0;
  size_t dataBegin = 0;
  size_t end = 0;
  uint32_t count = 0;
};

struct VmdBoneKeyframe {
  std::string boneName;
  uint32_t frame = 0;
  float pos[3] = {};
  float rot[4] = {0.0f, 0.0f, 0.0f, 1.0f};
  uint8_t interp[64] = {};
};

struct VmdBoneTimeline {
  std::string boneName;
  std::vector<VmdBoneKeyframe> keys;
};

struct VmdMorphKeyframe {
  std::string morphName;
  uint32_t frame = 0;
  float weight = 0.0f;
};

struct VmdMorphTimeline {
  std::string morphName;
  std::vector<VmdMorphKeyframe> keys;

  float Sample(float frameF) const;
};

struct VmdCameraKeyframe {
  uint32_t frame = 0;
  float distance = 0.0f;
  float position[3] = {};
  float rotation[3] = {};
  uint8_t interp[24] = {};
  uint32_t fov = 0;
  uint8_t perspective = 0;
};

struct VmdLightKeyframe {
  uint32_t frame = 0;
  float color[3] = {};
  float position[3] = {};
};

struct VmdSelfShadowKeyframe {
  uint32_t frame = 0;
  uint8_t mode = 0;
  float distance = 0.0f;
};

struct VmdIkState {
  std::string ikName;
  bool enabled = true;
};

struct VmdModelDisplayKeyframe {
  uint32_t frame = 0;
  bool visible = true;
  std::vector<VmdIkState> ikStates;
};

struct VmdIkKeyframe {
  uint32_t frame = 0;
  bool enabled = true;
};

struct VmdIkTimeline {
  std::string ikName;
  std::vector<VmdIkKeyframe> keys;
};

struct VmdParseLimits {
  size_t maxFileBytes = 512ull * 1024ull * 1024ull;
  uint32_t maxBoneKeys = 1000000;
  uint32_t maxMorphKeys = 1000000;
  uint32_t maxCameraKeys = 250000;
  uint32_t maxLightKeys = 250000;
  uint32_t maxSelfShadowKeys = 250000;
  uint32_t maxModelDisplayKeys = 250000;
  uint32_t maxIkEntriesPerFrame = 4096;
  uint32_t maxTotalIkEntries = 1000000;
};

struct VmdFile {
  char signature[31];
  char modelName[64];
  uint32_t totalFrames;
  uint32_t formatVersion;

  std::map<std::string, VmdBoneTimeline> boneTimelines;
  std::map<std::string, VmdMorphTimeline> morphTimelines;
  std::vector<VmdCameraKeyframe> cameraKeys;
  std::vector<VmdLightKeyframe> lightKeys;
  std::vector<VmdSelfShadowKeyframe> selfShadowKeys;
  std::vector<VmdModelDisplayKeyframe> modelDisplayKeys;
  std::map<std::string, VmdIkTimeline> ikTimelines;

  std::array<VmdSectionBoundary,
             static_cast<size_t>(VmdSection::Count)> sections;
  size_t fileSize;
  size_t parsedBytes;
  size_t trailingBytes;
  bool loaded;
  std::string error;
  std::vector<std::string> warnings;

  VmdFile()
      : totalFrames(0), formatVersion(0), fileSize(0), parsedBytes(0),
        trailingBytes(0), loaded(false) {
    std::memset(signature, 0, sizeof(signature));
    std::memset(modelName, 0, sizeof(modelName));
  }

  const VmdSectionBoundary &Section(VmdSection section) const {
    return sections[static_cast<size_t>(section)];
  }
};


constexpr double kVmdFramesPerSecond = 30.0;

struct VmdVec3 {
  float x = 0.0f;
  float y = 0.0f;
  float z = 0.0f;
};

struct VmdQuaternion {
  float x = 0.0f;
  float y = 0.0f;
  float z = 0.0f;
  float w = 1.0f;
};

struct VmdBezierControl {
  float x1 = 0.0f;
  float y1 = 0.0f;
  float x2 = 1.0f;
  float y2 = 1.0f;
};

enum class VmdBoneCurve : uint8_t { X = 0, Y = 1, Z = 2, Rotation = 3 };
enum class VmdCameraCurve : uint8_t {
  X = 0,
  Y = 1,
  Z = 2,
  Rotation = 3,
  Distance = 4,
  Fov = 5
};

struct VmdBoneSample {
  VmdVec3 position;
  VmdQuaternion rotation;
  bool valid = false;
};

struct VmdCameraSample {
  VmdVec3 interest;
  VmdVec3 rotation;
  float distance = 0.0f;
  float fov = 0.0f;
  uint8_t perspective = 0;
  bool valid = false;
};


static inline std::string SjisToUtf8(const char *sjis, int maxLen) {
  if (!sjis || maxLen <= 0) return std::string();

  int sjisLen = 0;
  while (sjisLen < maxLen && sjis[sjisLen] != '\0') ++sjisLen;
  if (sjisLen == 0) return std::string();

#if defined(__ANDROID__)
  return BetterEndfield::EiemAndroid::DecodeCp932(sjis, static_cast<size_t>(sjisLen));
#else
  const int wideLen =
      MultiByteToWideChar(932, 0, sjis, sjisLen, nullptr, 0);
  if (wideLen <= 0) return std::string();
  std::vector<wchar_t> wide(static_cast<size_t>(wideLen));
  if (MultiByteToWideChar(932, 0, sjis, sjisLen, wide.data(), wideLen) !=
      wideLen) {
    return std::string();
  }

  const int utf8Len = WideCharToMultiByte(CP_UTF8, 0, wide.data(), wideLen,
                                           nullptr, 0, nullptr, nullptr);
  if (utf8Len <= 0) return std::string();
  std::string result(static_cast<size_t>(utf8Len), '\0');
  if (WideCharToMultiByte(CP_UTF8, 0, wide.data(), wideLen, &result[0],
                          utf8Len, nullptr, nullptr) != utf8Len) {
    return std::string();
  }
  return result;
#endif
}

static inline bool VmdIsFinite(float value) {
  return std::isfinite(static_cast<double>(value));
}

static inline VmdQuaternion VmdNormalizeQuaternion(VmdQuaternion q,
                                                    bool *wasZero = nullptr) {
  const double lenSq = static_cast<double>(q.x) * q.x +
                       static_cast<double>(q.y) * q.y +
                       static_cast<double>(q.z) * q.z +
                       static_cast<double>(q.w) * q.w;
  const bool zero = !std::isfinite(lenSq) || lenSq <= 1.0e-20;
  if (wasZero) *wasZero = zero;
  if (zero) return VmdQuaternion{};

  const float invLen = static_cast<float>(1.0 / std::sqrt(lenSq));
  q.x *= invLen;
  q.y *= invLen;
  q.z *= invLen;
  q.w *= invLen;
  return q;
}

static inline float VmdQuaternionDot(const VmdQuaternion &a,
                                     const VmdQuaternion &b) {
  return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
}

static inline VmdQuaternion VmdSlerpShortest(VmdQuaternion a,
                                             VmdQuaternion b, float t) {
  a = VmdNormalizeQuaternion(a);
  b = VmdNormalizeQuaternion(b);
  t = (std::max)(0.0f, (std::min)(1.0f, t));

  float dot = VmdQuaternionDot(a, b);
  if (dot < 0.0f) {
    b.x = -b.x;
    b.y = -b.y;
    b.z = -b.z;
    b.w = -b.w;
    dot = -dot;
  }
  dot = (std::max)(-1.0f, (std::min)(1.0f, dot));

  if (dot > 0.9995f) {
    VmdQuaternion result = {
        a.x + (b.x - a.x) * t, a.y + (b.y - a.y) * t,
        a.z + (b.z - a.z) * t, a.w + (b.w - a.w) * t};
    return VmdNormalizeQuaternion(result);
  }

  const float theta = std::acos(dot);
  const float sinTheta = std::sin(theta);
  if (std::fabs(sinTheta) <= 1.0e-7f) return a;
  const float wa = std::sin((1.0f - t) * theta) / sinTheta;
  const float wb = std::sin(t * theta) / sinTheta;
  return VmdNormalizeQuaternion(
      {wa * a.x + wb * b.x, wa * a.y + wb * b.y,
       wa * a.z + wb * b.z, wa * a.w + wb * b.w});
}

static inline double VmdBezierAxis(double p1, double p2, double u) {
  const double oneMinus = 1.0 - u;
  return 3.0 * oneMinus * oneMinus * u * p1 +
         3.0 * oneMinus * u * u * p2 + u * u * u;
}

static inline float VmdEvaluateBezier(const VmdBezierControl &curve,
                                      float linearTime) {
  if (!VmdIsFinite(linearTime) || linearTime <= 0.0f) return 0.0f;
  if (linearTime >= 1.0f) return 1.0f;

  const double target = linearTime;
  double low = 0.0;
  double high = 1.0;
  for (int i = 0; i < 40; ++i) {
    const double mid = (low + high) * 0.5;
    if (VmdBezierAxis(curve.x1, curve.x2, mid) < target)
      low = mid;
    else
      high = mid;
  }
  const double u = (low + high) * 0.5;
  const double y = VmdBezierAxis(curve.y1, curve.y2, u);
  return static_cast<float>((std::max)(0.0, (std::min)(1.0, y)));
}

static inline VmdBezierControl VmdGetBoneBezier(const uint8_t interp[64],
                                                VmdBoneCurve channel) {
  const size_t c = static_cast<size_t>(channel);
  constexpr float kInv127 = 1.0f / 127.0f;
  return {interp[c] * kInv127, interp[4 + c] * kInv127,
          interp[8 + c] * kInv127, interp[12 + c] * kInv127};
}

static inline VmdBezierControl VmdGetCameraBezier(
    const uint8_t interp[24], VmdCameraCurve channel) {
  const size_t offset = static_cast<size_t>(channel) * 4;
  constexpr float kInv127 = 1.0f / 127.0f;
  return {interp[offset] * kInv127, interp[offset + 2] * kInv127,
          interp[offset + 1] * kInv127, interp[offset + 3] * kInv127};
}

static inline double VmdSecondsToFrame(double seconds) {
  return seconds * kVmdFramesPerSecond;
}

static inline double VmdFrameToSeconds(double frame) {
  return frame / kVmdFramesPerSecond;
}


namespace vmd_detail {

constexpr size_t kBoneRecordBytes = 111;
constexpr size_t kMorphRecordBytes = 23;
constexpr size_t kCameraRecordBytes = 61;
constexpr size_t kLightRecordBytes = 28;
constexpr size_t kSelfShadowRecordBytes = 9;
constexpr size_t kModelDisplayBaseBytes = 9;
constexpr size_t kIkInfoBytes = 21;

class Reader {
 public:
  Reader(const uint8_t *data, size_t size) : data_(data), size_(size) {}

  size_t Position() const { return position_; }
  size_t Size() const { return size_; }
  size_t Remaining() const { return size_ - position_; }

  bool ReadBytes(void *destination, size_t byteCount) {
    if (byteCount > Remaining()) return false;
    if (byteCount != 0)
      std::memcpy(destination, data_ + position_, byteCount);
    position_ += byteCount;
    return true;
  }

  bool ReadU8(uint8_t *value) { return ReadBytes(value, 1); }

  bool ReadU32(uint32_t *value) {
    uint8_t bytes[4];
    if (!ReadBytes(bytes, sizeof(bytes))) return false;
    *value = static_cast<uint32_t>(bytes[0]) |
             (static_cast<uint32_t>(bytes[1]) << 8) |
             (static_cast<uint32_t>(bytes[2]) << 16) |
             (static_cast<uint32_t>(bytes[3]) << 24);
    return true;
  }

  bool ReadFloat(float *value) {
    uint32_t bits = 0;
    if (!ReadU32(&bits)) return false;
    static_assert(sizeof(float) == sizeof(uint32_t),
                  "VMD parser requires IEEE-754 32-bit float");
    std::memcpy(value, &bits, sizeof(bits));
    return true;
  }

 private:
  const uint8_t *data_ = nullptr;
  size_t size_ = 0;
  size_t position_ = 0;
};

static inline const char *SectionName(VmdSection section) {
  switch (section) {
    case VmdSection::Header: return "Header";
    case VmdSection::Bone: return "Bone";
    case VmdSection::Morph: return "Morph";
    case VmdSection::Camera: return "Camera";
    case VmdSection::Light: return "Light";
    case VmdSection::SelfShadow: return "Self Shadow";
    case VmdSection::ModelDisplayIk: return "Model Display / IK";
    default: return "Unknown";
  }
}

template <typename Key>
static size_t SortAndKeepLastDuplicate(std::vector<Key> *keys) {
  if (!keys || keys->size() < 2) return 0;
  std::stable_sort(keys->begin(), keys->end(),
                   [](const Key &a, const Key &b) {
                     return a.frame < b.frame;
                   });

  size_t write = 0;
  size_t duplicates = 0;
  for (size_t read = 0; read < keys->size(); ++read) {
    if (write != 0 && (*keys)[write - 1].frame == (*keys)[read].frame) {
      (*keys)[write - 1] = std::move((*keys)[read]);
      ++duplicates;
    } else {
      if (write != read) (*keys)[write] = std::move((*keys)[read]);
      ++write;
    }
  }
  keys->resize(write);
  return duplicates;
}

class Parser {
 public:
  Parser(const uint8_t *data, size_t size, const VmdParseLimits &limits,
         VmdFile *output)
      : reader_(data, size), limits_(limits), output_(output) {
    output_->fileSize = size;
  }

  bool Parse() {
    if (!ParseHeader() || !ParseBones() || !ParseMorphs() ||
        !ParseCameras() || !ParseLights() || !ParseSelfShadows() ||
        !ParseModelDisplayIk()) {
      output_->parsedBytes = reader_.Position();
      return false;
    }

    output_->parsedBytes = reader_.Position();
    output_->trailingBytes = reader_.Remaining();
    if (output_->trailingBytes != 0) {
      output_->warnings.push_back(
          "ignored " + std::to_string(output_->trailingBytes) +
          " trailing byte(s) after the final section");
    }
    SortAndDeduplicate();
    output_->loaded = true;
    return true;
  }

 private:
  VmdSectionBoundary &Boundary(VmdSection section) {
    return output_->sections[static_cast<size_t>(section)];
  }

  bool Fail(VmdSection section, const std::string &message) {
    output_->loaded = false;
    output_->error = std::string(SectionName(section)) + " @ byte " +
                     std::to_string(reader_.Position()) + ": " + message;
    return false;
  }

  bool ReadFloatFinite(VmdSection section, const char *field, float *value) {
    if (!reader_.ReadFloat(value))
      return Fail(section, std::string("truncated ") + field);
    if (!VmdIsFinite(*value))
      return Fail(section, std::string("non-finite ") + field);
    return true;
  }

  bool BeginFixedSection(VmdSection section, size_t recordBytes,
                         uint32_t maxCount, bool required, uint32_t *count) {
    VmdSectionBoundary &boundary = Boundary(section);
    boundary.begin = reader_.Position();
    boundary.dataBegin = reader_.Position();
    boundary.end = reader_.Position();
    boundary.count = 0;

    if (!required && reader_.Remaining() == 0) return true;
    boundary.present = true;
    if (reader_.Remaining() < sizeof(uint32_t))
      return Fail(section, "truncated section count");
    if (!reader_.ReadU32(count)) return Fail(section, "cannot read count");
    boundary.count = *count;
    boundary.dataBegin = reader_.Position();
    if (*count > maxCount) {
      return Fail(section, "count " + std::to_string(*count) +
                               " exceeds limit " +
                               std::to_string(maxCount));
    }
    if (recordBytes != 0 &&
        static_cast<size_t>(*count) > reader_.Remaining() / recordBytes) {
      return Fail(section, "count " + std::to_string(*count) +
                               " exceeds remaining file size for " +
                               std::to_string(recordBytes) +
                               "-byte records");
    }
    return true;
  }

  bool ParseHeader() {
    VmdSectionBoundary &boundary = Boundary(VmdSection::Header);
    boundary.present = true;
    boundary.begin = 0;
    boundary.dataBegin = 0;
    boundary.count = 1;

    char signature[30] = {};
    if (!reader_.ReadBytes(signature, sizeof(signature)))
      return Fail(VmdSection::Header, "file is smaller than 30-byte magic");
    std::memcpy(output_->signature, signature, sizeof(signature));
    output_->signature[30] = '\0';

    size_t signatureLength = 0;
    while (signatureLength < sizeof(signature) &&
           signature[signatureLength] != '\0') {
      ++signatureLength;
    }
    const std::string magic(signature, signatureLength);
    size_t modelNameBytes = 0;
    if (magic == "Vocaloid Motion Data 0002") {
      output_->formatVersion = 2;
      modelNameBytes = 20;
    } else if (magic == "Vocaloid Motion Data file" ||
               magic == "Vocaloid Motion Data") {
      output_->formatVersion = 1;
      modelNameBytes = 10;
    } else {
      return Fail(VmdSection::Header, "invalid VMD signature '" + magic +
                                          "'");
    }

    char modelRaw[20] = {};
    if (!reader_.ReadBytes(modelRaw, modelNameBytes))
      return Fail(VmdSection::Header, "truncated model name");
    const std::string modelUtf8 =
        SjisToUtf8(modelRaw, static_cast<int>(modelNameBytes));
    std::snprintf(output_->modelName, sizeof(output_->modelName), "%s",
                  modelUtf8.c_str());
    boundary.end = reader_.Position();
    return true;
  }

  bool ParseBones() {
    uint32_t count = 0;
    if (!BeginFixedSection(VmdSection::Bone, kBoneRecordBytes,
                           limits_.maxBoneKeys, true, &count)) {
      return false;
    }

    for (uint32_t i = 0; i < count; ++i) {
      char nameRaw[15] = {};
      VmdBoneKeyframe key;
      if (!reader_.ReadBytes(nameRaw, sizeof(nameRaw)) ||
          !reader_.ReadU32(&key.frame)) {
        return Fail(VmdSection::Bone,
                    "truncated record " + std::to_string(i));
      }
      key.boneName = SjisToUtf8(nameRaw, static_cast<int>(sizeof(nameRaw)));
      for (int axis = 0; axis < 3; ++axis) {
        if (!ReadFloatFinite(VmdSection::Bone, "position", &key.pos[axis]))
          return false;
      }
      for (int component = 0; component < 4; ++component) {
        if (!ReadFloatFinite(VmdSection::Bone, "rotation",
                             &key.rot[component])) {
          return false;
        }
      }
      if (!reader_.ReadBytes(key.interp, sizeof(key.interp))) {
        return Fail(VmdSection::Bone,
                    "truncated interpolation in record " +
                        std::to_string(i));
      }
      for (size_t byteIndex = 0; byteIndex < 16; ++byteIndex) {
        if (key.interp[byteIndex] > 127) {
          return Fail(VmdSection::Bone,
                      "Bezier byte exceeds 127 in record " +
                          std::to_string(i));
        }
      }

      bool zeroQuaternion = false;
      VmdQuaternion normalized = VmdNormalizeQuaternion(
          {key.rot[0], key.rot[1], key.rot[2], key.rot[3]},
          &zeroQuaternion);
      key.rot[0] = normalized.x;
      key.rot[1] = normalized.y;
      key.rot[2] = normalized.z;
      key.rot[3] = normalized.w;
      if (zeroQuaternion) {
        output_->warnings.push_back(
            "Bone '" + key.boneName + "' frame " +
            std::to_string(key.frame) +
            " had a zero-length quaternion; identity was substituted");
      }

      VmdBoneTimeline &timeline = output_->boneTimelines[key.boneName];
      if (timeline.boneName.empty()) timeline.boneName = key.boneName;
      timeline.keys.push_back(std::move(key));
    }
    Boundary(VmdSection::Bone).end = reader_.Position();
    return true;
  }

  bool ParseMorphs() {
    uint32_t count = 0;
    if (!BeginFixedSection(VmdSection::Morph, kMorphRecordBytes,
                           limits_.maxMorphKeys, false, &count)) {
      return false;
    }
    if (!Boundary(VmdSection::Morph).present) return true;

    for (uint32_t i = 0; i < count; ++i) {
      char nameRaw[15] = {};
      VmdMorphKeyframe key;
      if (!reader_.ReadBytes(nameRaw, sizeof(nameRaw)) ||
          !reader_.ReadU32(&key.frame)) {
        return Fail(VmdSection::Morph,
                    "truncated record " + std::to_string(i));
      }
      key.morphName = SjisToUtf8(nameRaw, static_cast<int>(sizeof(nameRaw)));
      if (!ReadFloatFinite(VmdSection::Morph, "weight", &key.weight))
        return false;
      VmdMorphTimeline &timeline = output_->morphTimelines[key.morphName];
      if (timeline.morphName.empty()) timeline.morphName = key.morphName;
      timeline.keys.push_back(std::move(key));
    }
    Boundary(VmdSection::Morph).end = reader_.Position();
    return true;
  }

  bool ParseCameras() {
    uint32_t count = 0;
    if (!BeginFixedSection(VmdSection::Camera, kCameraRecordBytes,
                           limits_.maxCameraKeys, false, &count)) {
      return false;
    }
    if (!Boundary(VmdSection::Camera).present) return true;
    output_->cameraKeys.reserve(count);

    for (uint32_t i = 0; i < count; ++i) {
      VmdCameraKeyframe key;
      if (!reader_.ReadU32(&key.frame) ||
          !ReadFloatFinite(VmdSection::Camera, "distance", &key.distance)) {
        if (output_->error.empty())
          Fail(VmdSection::Camera,
               "truncated record " + std::to_string(i));
        return false;
      }
      for (int axis = 0; axis < 3; ++axis) {
        if (!ReadFloatFinite(VmdSection::Camera, "position",
                             &key.position[axis])) {
          return false;
        }
      }
      for (int axis = 0; axis < 3; ++axis) {
        if (!ReadFloatFinite(VmdSection::Camera, "rotation",
                             &key.rotation[axis])) {
          return false;
        }
      }
      if (!reader_.ReadBytes(key.interp, sizeof(key.interp)) ||
          !reader_.ReadU32(&key.fov) ||
          !reader_.ReadU8(&key.perspective)) {
        return Fail(VmdSection::Camera,
                    "truncated record " + std::to_string(i));
      }
      for (uint8_t byte : key.interp) {
        if (byte > 127) {
          return Fail(VmdSection::Camera,
                      "Bezier byte exceeds 127 in record " +
                          std::to_string(i));
        }
      }
      output_->cameraKeys.push_back(key);
    }
    Boundary(VmdSection::Camera).end = reader_.Position();
    return true;
  }

  bool ParseLights() {
    uint32_t count = 0;
    if (!BeginFixedSection(VmdSection::Light, kLightRecordBytes,
                           limits_.maxLightKeys, false, &count)) {
      return false;
    }
    if (!Boundary(VmdSection::Light).present) return true;
    output_->lightKeys.reserve(count);

    for (uint32_t i = 0; i < count; ++i) {
      VmdLightKeyframe key;
      if (!reader_.ReadU32(&key.frame))
        return Fail(VmdSection::Light,
                    "truncated record " + std::to_string(i));
      for (int component = 0; component < 3; ++component) {
        if (!ReadFloatFinite(VmdSection::Light, "color",
                             &key.color[component])) {
          return false;
        }
      }
      for (int axis = 0; axis < 3; ++axis) {
        if (!ReadFloatFinite(VmdSection::Light, "position",
                             &key.position[axis])) {
          return false;
        }
      }
      output_->lightKeys.push_back(key);
    }
    Boundary(VmdSection::Light).end = reader_.Position();
    return true;
  }

  bool ParseSelfShadows() {
    uint32_t count = 0;
    if (!BeginFixedSection(VmdSection::SelfShadow, kSelfShadowRecordBytes,
                           limits_.maxSelfShadowKeys, false, &count)) {
      return false;
    }
    if (!Boundary(VmdSection::SelfShadow).present) return true;
    output_->selfShadowKeys.reserve(count);

    for (uint32_t i = 0; i < count; ++i) {
      VmdSelfShadowKeyframe key;
      if (!reader_.ReadU32(&key.frame) || !reader_.ReadU8(&key.mode)) {
        return Fail(VmdSection::SelfShadow,
                    "truncated record " + std::to_string(i));
      }
      if (!ReadFloatFinite(VmdSection::SelfShadow, "distance",
                           &key.distance)) {
        return false;
      }
      output_->selfShadowKeys.push_back(key);
    }
    Boundary(VmdSection::SelfShadow).end = reader_.Position();
    return true;
  }

  bool ParseModelDisplayIk() {
    uint32_t count = 0;
    if (!BeginFixedSection(VmdSection::ModelDisplayIk,
                           kModelDisplayBaseBytes,
                           limits_.maxModelDisplayKeys, false, &count)) {
      return false;
    }
    if (!Boundary(VmdSection::ModelDisplayIk).present) return true;
    output_->modelDisplayKeys.reserve(count);

    uint64_t totalIkEntries = 0;
    for (uint32_t i = 0; i < count; ++i) {
      VmdModelDisplayKeyframe key;
      uint8_t visible = 0;
      uint32_t ikCount = 0;
      if (!reader_.ReadU32(&key.frame) || !reader_.ReadU8(&visible) ||
          !reader_.ReadU32(&ikCount)) {
        return Fail(VmdSection::ModelDisplayIk,
                    "truncated frame record " + std::to_string(i));
      }
      key.visible = visible != 0;
      if (ikCount > limits_.maxIkEntriesPerFrame) {
        return Fail(VmdSection::ModelDisplayIk,
                    "IK count " + std::to_string(ikCount) +
                        " exceeds per-frame limit " +
                        std::to_string(limits_.maxIkEntriesPerFrame));
      }
      totalIkEntries += ikCount;
      if (totalIkEntries > limits_.maxTotalIkEntries) {
        return Fail(VmdSection::ModelDisplayIk,
                    "total IK entry count exceeds limit " +
                        std::to_string(limits_.maxTotalIkEntries));
      }
      if (static_cast<size_t>(ikCount) >
          reader_.Remaining() / kIkInfoBytes) {
        return Fail(VmdSection::ModelDisplayIk,
                    "IK count exceeds remaining file size in frame record " +
                        std::to_string(i));
      }

      key.ikStates.reserve(ikCount);
      for (uint32_t ikIndex = 0; ikIndex < ikCount; ++ikIndex) {
        char nameRaw[20] = {};
        uint8_t enabled = 0;
        if (!reader_.ReadBytes(nameRaw, sizeof(nameRaw)) ||
            !reader_.ReadU8(&enabled)) {
          return Fail(VmdSection::ModelDisplayIk,
                      "truncated IK entry " + std::to_string(ikIndex) +
                          " in frame record " + std::to_string(i));
        }
        VmdIkState state;
        state.ikName =
            SjisToUtf8(nameRaw, static_cast<int>(sizeof(nameRaw)));
        state.enabled = enabled != 0;
        key.ikStates.push_back(state);

        VmdIkTimeline &timeline = output_->ikTimelines[state.ikName];
        if (timeline.ikName.empty()) timeline.ikName = state.ikName;
        timeline.keys.push_back({key.frame, state.enabled});
      }
      output_->modelDisplayKeys.push_back(std::move(key));
    }
    Boundary(VmdSection::ModelDisplayIk).end = reader_.Position();
    return true;
  }

  void WarnDuplicates(const char *section, const std::string &name,
                      size_t duplicates) {
    if (duplicates == 0) return;
    std::string warning(section);
    if (!name.empty()) warning += " '" + name + "'";
    warning += " discarded " + std::to_string(duplicates) +
               " duplicate frame(s); last file occurrence wins";
    output_->warnings.push_back(std::move(warning));
  }

  void SortAndDeduplicate() {
    uint32_t maximumFrame = 0;
    for (auto &entry : output_->boneTimelines) {
      WarnDuplicates("Bone", entry.first,
                     SortAndKeepLastDuplicate(&entry.second.keys));
      if (!entry.second.keys.empty())
        maximumFrame = (std::max)(maximumFrame,
                                  entry.second.keys.back().frame);
    }
    for (auto &entry : output_->morphTimelines) {
      WarnDuplicates("Morph", entry.first,
                     SortAndKeepLastDuplicate(&entry.second.keys));
      if (!entry.second.keys.empty())
        maximumFrame = (std::max)(maximumFrame,
                                  entry.second.keys.back().frame);
    }

    WarnDuplicates("Camera", std::string(),
                   SortAndKeepLastDuplicate(&output_->cameraKeys));
    WarnDuplicates("Light", std::string(),
                   SortAndKeepLastDuplicate(&output_->lightKeys));
    WarnDuplicates("Self Shadow", std::string(),
                   SortAndKeepLastDuplicate(&output_->selfShadowKeys));
    WarnDuplicates("Model Display", std::string(),
                   SortAndKeepLastDuplicate(&output_->modelDisplayKeys));

    if (!output_->cameraKeys.empty())
      maximumFrame = (std::max)(maximumFrame,
                                output_->cameraKeys.back().frame);
    if (!output_->lightKeys.empty())
      maximumFrame =
          (std::max)(maximumFrame, output_->lightKeys.back().frame);
    if (!output_->selfShadowKeys.empty())
      maximumFrame = (std::max)(maximumFrame,
                                output_->selfShadowKeys.back().frame);
    if (!output_->modelDisplayKeys.empty())
      maximumFrame = (std::max)(maximumFrame,
                                output_->modelDisplayKeys.back().frame);

    for (auto &entry : output_->ikTimelines) {
      WarnDuplicates("IK", entry.first,
                     SortAndKeepLastDuplicate(&entry.second.keys));
      if (!entry.second.keys.empty())
        maximumFrame = (std::max)(maximumFrame,
                                  entry.second.keys.back().frame);
    }
    output_->totalFrames = maximumFrame;
  }

  Reader reader_;
  const VmdParseLimits &limits_;
  VmdFile *output_ = nullptr;
};

template <typename Key>
static inline typename std::vector<Key>::const_iterator UpperFrame(
    const std::vector<Key> &keys, double frame) {
  return std::upper_bound(
      keys.begin(), keys.end(), frame,
      [](double value, const Key &key) {
        return value < static_cast<double>(key.frame);
      });
}

}

static inline bool ParseVmdBytes(const uint8_t *data, size_t size,
                                 VmdFile *output,
                                 const VmdParseLimits &limits =
                                     VmdParseLimits()) {
  if (!output) return false;
  *output = VmdFile();
  output->fileSize = size;
  if ((!data && size != 0) || size > limits.maxFileBytes) {
    output->error = size > limits.maxFileBytes
                        ? "File exceeds configured size limit"
                        : "Null data pointer with non-zero size";
    return false;
  }

  try {
    vmd_detail::Parser parser(data, size, limits, output);
    return parser.Parse();
  } catch (const std::bad_alloc &) {
    output->loaded = false;
    output->error = "Allocation failed while parsing VMD";
    return false;
  } catch (...) {
    output->loaded = false;
    output->error = "Unexpected exception while parsing VMD";
    return false;
  }
}

static inline int VmdFileSeek(FILE *file, int64_t offset, int origin) {
#if defined(__ANDROID__)
  return fseeko(file, static_cast<off_t>(offset), origin);
#else
  return _fseeki64(file, offset, origin);
#endif
}
static inline int64_t VmdFileTell(FILE *file) {
#if defined(__ANDROID__)
  return static_cast<int64_t>(ftello(file));
#else
  return _ftelli64(file);
#endif
}

static inline VmdFile *LoadVmd(const char *path) {
  std::unique_ptr<VmdFile> vmd(new VmdFile());
  if (!path || path[0] == '\0') {
    vmd->error = "Cannot open file: empty path";
    return vmd.release();
  }

  // BE-PATCH(utf8-path): Better Endfield passes UTF-8 paths (library folders
  // may contain non-ASCII names); open through the wide CRT API.
  FILE *file = nullptr;
#if defined(__ANDROID__)
  if (!BetterEndfield::EiemAndroid::ValidUtf8(path) || !(file = fopen(path, "rb"))) {
    vmd->error = "Cannot open UTF-8 file";
    return vmd.release();
  }
#else
  {
    const int wideLength = MultiByteToWideChar(CP_UTF8, MB_ERR_INVALID_CHARS,
                                               path, -1, nullptr, 0);
    std::wstring widePath(wideLength > 0 ? size_t(wideLength) : 0, L'\0');
    if (wideLength <= 0 ||
        MultiByteToWideChar(CP_UTF8, MB_ERR_INVALID_CHARS, path, -1,
                            widePath.data(), wideLength) != wideLength ||
        _wfopen_s(&file, widePath.c_str(), L"rb") != 0 || !file) {
      vmd->error = "Cannot open file";
      return vmd.release();
    }
  }

#endif
  if (VmdFileSeek(file, 0, SEEK_END) != 0) {
    vmd->error = "Cannot seek to end of file";
    std::fclose(file);
    return vmd.release();
  }
  const int64_t signedSize = VmdFileTell(file);
  if (signedSize < 0 ||
      static_cast<uint64_t>(signedSize) >
          static_cast<uint64_t>(VmdParseLimits().maxFileBytes)) {
    vmd->error = signedSize < 0 ? "Cannot determine file size"
                                : "File exceeds configured size limit";
    std::fclose(file);
    return vmd.release();
  }
  if (VmdFileSeek(file, 0, SEEK_SET) != 0) {
    vmd->error = "Cannot seek to start of file";
    std::fclose(file);
    return vmd.release();
  }

  const size_t size = static_cast<size_t>(signedSize);
  std::vector<uint8_t> bytes;
  try {
    bytes.resize(size);
  } catch (const std::bad_alloc &) {
    vmd->error = "Allocation failed while reading VMD";
    std::fclose(file);
    return vmd.release();
  }
  if (size != 0 && std::fread(bytes.data(), 1, size, file) != size) {
    vmd->error = "Cannot read complete file";
    std::fclose(file);
    return vmd.release();
  }
  std::fclose(file);

  ParseVmdBytes(bytes.data(), bytes.size(), vmd.get());
  return vmd.release();
}

static inline void FreeVmd(VmdFile *vmd) { delete vmd; }


static inline bool SampleVmdBone(const VmdBoneTimeline &timeline,
                                 double frame, VmdBoneSample *sample) {
  if (!sample) return false;
  *sample = VmdBoneSample();
  if (timeline.keys.empty() || !std::isfinite(frame)) return false;

  const auto &keys = timeline.keys;
  const VmdBoneKeyframe *key0 = nullptr;
  const VmdBoneKeyframe *key1 = nullptr;
  float linearTime = 0.0f;
  if (frame <= static_cast<double>(keys.front().frame)) {
    key0 = &keys.front();
  } else if (frame >= static_cast<double>(keys.back().frame)) {
    key0 = &keys.back();
  } else {
    const auto upper = vmd_detail::UpperFrame(keys, frame);
    key0 = &*(upper - 1);
    key1 = &*upper;
    const double range =
        static_cast<double>(key1->frame) - key0->frame;
    linearTime = static_cast<float>((frame - key0->frame) / range);
  }

  sample->position = {key0->pos[0], key0->pos[1], key0->pos[2]};
  sample->rotation =
      VmdNormalizeQuaternion({key0->rot[0], key0->rot[1], key0->rot[2],
                              key0->rot[3]});
  if (key1) {
    const float tx = VmdEvaluateBezier(
        VmdGetBoneBezier(key0->interp, VmdBoneCurve::X), linearTime);
    const float ty = VmdEvaluateBezier(
        VmdGetBoneBezier(key0->interp, VmdBoneCurve::Y), linearTime);
    const float tz = VmdEvaluateBezier(
        VmdGetBoneBezier(key0->interp, VmdBoneCurve::Z), linearTime);
    const float tr = VmdEvaluateBezier(
        VmdGetBoneBezier(key0->interp, VmdBoneCurve::Rotation), linearTime);
    sample->position = {
        key0->pos[0] + (key1->pos[0] - key0->pos[0]) * tx,
        key0->pos[1] + (key1->pos[1] - key0->pos[1]) * ty,
        key0->pos[2] + (key1->pos[2] - key0->pos[2]) * tz};
    sample->rotation = VmdSlerpShortest(
        sample->rotation,
        {key1->rot[0], key1->rot[1], key1->rot[2], key1->rot[3]}, tr);
  }
  sample->valid = true;
  return true;
}

static inline bool SampleVmdBone(const VmdFile &vmd,
                                 const std::string &boneName, double frame,
                                 VmdBoneSample *sample) {
  const auto found = vmd.boneTimelines.find(boneName);
  if (found == vmd.boneTimelines.end()) {
    if (sample) *sample = VmdBoneSample();
    return false;
  }
  return SampleVmdBone(found->second, frame, sample);
}

static inline bool SampleVmdMorph(const VmdFile &vmd,
                                  const std::string &morphName, double frame,
                                  float *weight) {
  if (!weight) return false;
  *weight = 0.0f;
  if (!std::isfinite(frame)) return false;
  const auto found = vmd.morphTimelines.find(morphName);
  if (found == vmd.morphTimelines.end() || found->second.keys.empty())
    return false;
  *weight = found->second.Sample(static_cast<float>(frame));
  return true;
}

static inline bool SampleVmdCamera(const VmdFile &vmd, double frame,
                                   VmdCameraSample *sample) {
  if (!sample) return false;
  *sample = VmdCameraSample();
  if (vmd.cameraKeys.empty() || !std::isfinite(frame)) return false;

  const auto &keys = vmd.cameraKeys;
  const VmdCameraKeyframe *key0 = nullptr;
  const VmdCameraKeyframe *key1 = nullptr;
  float linearTime = 0.0f;
  if (frame <= static_cast<double>(keys.front().frame)) {
    key0 = &keys.front();
  } else if (frame >= static_cast<double>(keys.back().frame)) {
    key0 = &keys.back();
  } else {
    const auto upper = vmd_detail::UpperFrame(keys, frame);
    key0 = &*(upper - 1);
    key1 = &*upper;
    const double range =
        static_cast<double>(key1->frame) - key0->frame;
    linearTime = static_cast<float>((frame - key0->frame) / range);
  }

  sample->interest =
      {key0->position[0], key0->position[1], key0->position[2]};
  sample->rotation =
      {key0->rotation[0], key0->rotation[1], key0->rotation[2]};
  sample->distance = key0->distance;
  sample->fov = static_cast<float>(key0->fov);
  sample->perspective = key0->perspective;
  if (key1 && key1->frame - key0->frame > 1) {
    const auto curveWeight = [&](VmdCameraCurve curve) {
      return VmdEvaluateBezier(VmdGetCameraBezier(key0->interp, curve),
                               linearTime);
    };
    const float tx = curveWeight(VmdCameraCurve::X);
    const float ty = curveWeight(VmdCameraCurve::Y);
    const float tz = curveWeight(VmdCameraCurve::Z);
    const float tr = curveWeight(VmdCameraCurve::Rotation);
    const float td = curveWeight(VmdCameraCurve::Distance);
    const float tf = curveWeight(VmdCameraCurve::Fov);
    sample->interest = {
        key0->position[0] + (key1->position[0] - key0->position[0]) * tx,
        key0->position[1] + (key1->position[1] - key0->position[1]) * ty,
        key0->position[2] + (key1->position[2] - key0->position[2]) * tz};
    sample->rotation = {
        key0->rotation[0] +
            (key1->rotation[0] - key0->rotation[0]) * tr,
        key0->rotation[1] +
            (key1->rotation[1] - key0->rotation[1]) * tr,
        key0->rotation[2] +
            (key1->rotation[2] - key0->rotation[2]) * tr};
    sample->distance =
        key0->distance + (key1->distance - key0->distance) * td;
    sample->fov = static_cast<float>(key0->fov) +
                  (static_cast<float>(key1->fov) - key0->fov) * tf;
    sample->perspective = key0->perspective;
  }
  sample->valid = true;
  return true;
}

static inline bool SampleVmdIkEnabled(const VmdFile &vmd,
                                      const std::string &ikName,
                                      double frame) {
  if (!std::isfinite(frame)) return true;
  const auto found = vmd.ikTimelines.find(ikName);
  if (found == vmd.ikTimelines.end() || found->second.keys.empty()) return true;
  const auto &keys = found->second.keys;
  if (frame < static_cast<double>(keys.front().frame))
    return keys.front().enabled;
  const auto upper = vmd_detail::UpperFrame(keys, frame);
  if (upper == keys.begin()) return keys.front().enabled;
  return (upper - 1)->enabled;
}

static inline bool SampleVmdModelVisible(const VmdFile &vmd, double frame) {
  if (vmd.modelDisplayKeys.empty() || !std::isfinite(frame)) return true;
  const auto &keys = vmd.modelDisplayKeys;
  if (frame < static_cast<double>(keys.front().frame))
    return keys.front().visible;
  const auto upper = vmd_detail::UpperFrame(keys, frame);
  if (upper == keys.begin()) return keys.front().visible;
  return (upper - 1)->visible;
}

inline float VmdMorphTimeline::Sample(float frameF) const {
  if (keys.empty() || !VmdIsFinite(frameF)) return 0.0f;
  if (frameF <= static_cast<float>(keys.front().frame))
    return keys.front().weight;
  if (frameF >= static_cast<float>(keys.back().frame))
    return keys.back().weight;
  const auto upper = vmd_detail::UpperFrame(keys, frameF);
  const VmdMorphKeyframe &key0 = *(upper - 1);
  const VmdMorphKeyframe &key1 = *upper;
  const float t = (frameF - static_cast<float>(key0.frame)) /
                  static_cast<float>(key1.frame - key0.frame);
  return key0.weight + (key1.weight - key0.weight) * t;
}

struct DirectVmdResource {
  std::unique_ptr<VmdFile> clip;
  uint64_t generation = 0;

  DirectVmdResource() = default;
  DirectVmdResource(const DirectVmdResource &) = delete;
  DirectVmdResource &operator=(const DirectVmdResource &) = delete;
  DirectVmdResource(DirectVmdResource &&) = default;
  DirectVmdResource &operator=(DirectVmdResource &&) = default;

  bool Load(const char *path) {
    ++generation;
    clip.reset(LoadVmd(path));
    return clip && clip->loaded;
  }

  bool LoadBytes(const uint8_t *data, size_t size,
                 const VmdParseLimits &limits = VmdParseLimits()) {
    ++generation;
    std::unique_ptr<VmdFile> parsed(new VmdFile());
    const bool success = ParseVmdBytes(data, size, parsed.get(), limits);
    clip = std::move(parsed);
    return success;
  }

  void Reset() {
    ++generation;
    clip.reset();
  }

  const VmdFile *Get() const { return clip.get(); }
  VmdFile *Get() { return clip.get(); }
  bool IsLoaded() const { return clip && clip->loaded; }
};


static inline void DumpVmd(const VmdFile *vmd, FILE *out) {
  if (!vmd || !out) return;
  std::fprintf(out, "=== VMD File Info ===\n");
  std::fprintf(out, "Signature: %.30s\n", vmd->signature);
  std::fprintf(out, "Version: %u\n", vmd->formatVersion);
  std::fprintf(out, "Model: %s\n", vmd->modelName);
  std::fprintf(out, "Loaded: %s\n", vmd->loaded ? "YES" : "NO");
  if (!vmd->loaded) {
    std::fprintf(out, "Error: %s\n", vmd->error.c_str());
    return;
  }

  std::fprintf(out, "Total frames: %u (%.3f sec @ 30fps)\n",
               vmd->totalFrames,
               VmdFrameToSeconds(static_cast<double>(vmd->totalFrames)));
  std::fprintf(out, "Bone timelines: %zu\n", vmd->boneTimelines.size());
  std::fprintf(out, "Morph timelines: %zu\n", vmd->morphTimelines.size());
  std::fprintf(out, "Camera keys: %zu\n", vmd->cameraKeys.size());
  std::fprintf(out, "Light keys: %zu\n", vmd->lightKeys.size());
  std::fprintf(out, "Self-shadow keys: %zu\n", vmd->selfShadowKeys.size());
  std::fprintf(out, "Model-display keys: %zu\n",
               vmd->modelDisplayKeys.size());
  std::fprintf(out, "IK timelines: %zu\n", vmd->ikTimelines.size());
  std::fprintf(out, "Parsed/trailing bytes: %zu/%zu\n", vmd->parsedBytes,
               vmd->trailingBytes);

  for (const auto &entry : vmd->boneTimelines) {
    const VmdBoneTimeline &timeline = entry.second;
    std::fprintf(out, "  [BONE] %s: %zu keys", timeline.boneName.c_str(),
                 timeline.keys.size());
    if (!timeline.keys.empty())
      std::fprintf(out, " (frames %u-%u)", timeline.keys.front().frame,
                   timeline.keys.back().frame);
    std::fprintf(out, "\n");
  }
  for (const auto &entry : vmd->morphTimelines) {
    const VmdMorphTimeline &timeline = entry.second;
    std::fprintf(out, "  [MORPH] %s: %zu keys", timeline.morphName.c_str(),
                 timeline.keys.size());
    if (!timeline.keys.empty())
      std::fprintf(out, " (frames %u-%u)", timeline.keys.front().frame,
                   timeline.keys.back().frame);
    std::fprintf(out, "\n");
  }
  for (const std::string &warning : vmd->warnings)
    std::fprintf(out, "  [WARN] %s\n", warning.c_str());
}
