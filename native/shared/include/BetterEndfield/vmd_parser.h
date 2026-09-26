#pragma once

// Portions of the VMD record layout follow EIEM's vmd_parser.h
// (https://github.com/Sasye/EIEM), AGPL-3.0.

#include <algorithm>
#include <array>
#include <cmath>
#include <cstddef>
#include <cstdint>
#include <cstring>
#include <limits>
#include <span>
#include <string>
#include <string_view>
#include <utility>
#include <vector>

namespace BetterEndfield::Vmd {

struct BoneKey {
    uint32_t frame = 0;
    std::array<float, 3> position{};
    std::array<float, 4> rotation{0.0f, 0.0f, 0.0f, 1.0f};
    std::array<uint8_t, 64> interpolation{};
};

struct MorphKey {
    uint32_t frame = 0;
    float weight = 0.0f;
};

struct CameraKey {
    uint32_t frame = 0;
    float distance = 0.0f;
    std::array<float, 3> interest{};
    std::array<float, 3> euler{};
    std::array<uint8_t, 24> interpolation{};
    float fov = 30.0f;
    bool perspective = true;
};

struct LightKey {
    uint32_t frame = 0;
    std::array<float, 3> color{};
    std::array<float, 3> position{};
};

struct SelfShadowKey {
    uint32_t frame = 0;
    uint8_t mode = 0;
    float distance = 0.0f;
};

struct IkToggle {
    std::string name;
    bool enabled = false;
};

struct IkKey {
    uint32_t frame = 0;
    bool show = false;
    std::vector<IkToggle> toggles;
};

struct Limits {
    uint32_t max_bone_keys = 2'000'000;
    uint32_t max_morph_keys = 2'000'000;
    uint32_t max_camera_keys = 2'000'000;
    uint32_t max_light_keys = 2'000'000;
    uint32_t max_shadow_keys = 2'000'000;
    uint32_t max_ik_keys = 2'000'000;
    uint32_t max_ik_toggles_per_key = 16'384;
};

struct Document {
    bool version_2 = false;
    std::string model_name;
    std::vector<BoneKey> bones;
    std::vector<MorphKey> morphs;
    std::vector<CameraKey> cameras;
    std::vector<LightKey> lights;
    std::vector<SelfShadowKey> self_shadows;
    std::vector<IkKey> ik;
};

namespace detail {

class Reader {
public:
    explicit Reader(std::span<const uint8_t> bytes) : bytes_(bytes) {}

    size_t remaining() const { return bytes_.size() - offset_; }

    bool ReadU8(uint8_t& value) {
        if (remaining() < 1) return false;
        value = bytes_[offset_++];
        return true;
    }

    bool ReadU32(uint32_t& value) {
        if (remaining() < sizeof(value)) return false;
        std::memcpy(&value, bytes_.data() + offset_, sizeof(value));
        offset_ += sizeof(value);
        return true;
    }

    bool ReadFloat(float& value) {
        if (remaining() < sizeof(value)) return false;
        std::memcpy(&value, bytes_.data() + offset_, sizeof(value));
        offset_ += sizeof(value);
        return std::isfinite(value);
    }

    bool ReadBytes(void* destination, size_t size) {
        if (size > remaining()) return false;
        std::memcpy(destination, bytes_.data() + offset_, size);
        offset_ += size;
        return true;
    }

    bool Skip(size_t size) {
        if (size > remaining()) return false;
        offset_ += size;
        return true;
    }

    bool ReadFixedString(size_t size, std::string& value) {
        if (size > remaining()) return false;
        const char* begin = reinterpret_cast<const char*>(bytes_.data() + offset_);
        size_t length = 0;
        while (length < size && begin[length] != '\0') ++length;
        value.assign(begin, length);
        offset_ += size;
        return true;
    }

private:
    std::span<const uint8_t> bytes_;
    size_t offset_ = 0;
};

inline bool ReadCount(Reader& reader, uint32_t limit, uint32_t& count,
    std::string& error, const char* section) {
    if (!reader.ReadU32(count)) {
        error = std::string("truncated ") + section + " count";
        return false;
    }
    if (count > limit) {
        error = std::string(section) + " count exceeds the safety limit";
        return false;
    }
    return true;
}

template <class T>
bool ReadFloatArray(Reader& reader, T& values) {
    for (float& value : values) if (!reader.ReadFloat(value)) return false;
    return true;
}

template <class T>
void SortAndKeepLast(std::vector<T>& keys) {
    std::stable_sort(keys.begin(), keys.end(),
        [](const T& a, const T& b) { return a.frame < b.frame; });
    size_t output = 0;
    for (size_t i = 0; i < keys.size();) {
        size_t end = i + 1;
        while (end < keys.size() && keys[end].frame == keys[i].frame) ++end;
        if (output != end - 1) keys[output] = std::move(keys[end - 1]);
        ++output;
        i = end;
    }
    keys.resize(output);
}

inline void SortAndUnique(std::vector<BoneKey>& keys) {
    SortAndKeepLast(keys);
}

inline void SortAndUnique(std::vector<MorphKey>& keys) {
    SortAndKeepLast(keys);
}

inline void SortAndUnique(std::vector<CameraKey>& keys) {
    SortAndKeepLast(keys);
}

inline void SortAndUnique(std::vector<LightKey>& keys) {
    SortAndKeepLast(keys);
}

inline void SortAndUnique(std::vector<SelfShadowKey>& keys) {
    SortAndKeepLast(keys);
}

inline void SortAndUnique(std::vector<IkKey>& keys) {
    SortAndKeepLast(keys);
}

} // namespace detail

inline bool Parse(std::span<const uint8_t> bytes, Document& document, std::string& error,
    const Limits& limits = {}) {
    document = {};
    error.clear();
    detail::Reader reader(bytes);
    if (reader.remaining() < 30) {
        error = "the file is not a VMD motion";
        return false;
    }

    std::array<char, 30> header{};
    if (!reader.ReadBytes(header.data(), header.size())) {
        error = "truncated VMD header";
        return false;
    }
    const std::string_view signature(header.data(), header.size());
    size_t model_name_bytes = 0;
    if (signature.starts_with("Vocaloid Motion Data 0002")) {
        document.version_2 = true;
        model_name_bytes = 20;
    } else if (signature.starts_with("Vocaloid Motion Data file")) {
        model_name_bytes = 10;
    } else {
        error = "the file is not a VMD motion";
        return false;
    }
    if (!reader.ReadFixedString(model_name_bytes, document.model_name)) {
        error = "truncated VMD model name";
        return false;
    }

    uint32_t count = 0;
    if (!detail::ReadCount(reader, limits.max_bone_keys, count, error, "bone")) return false;
    document.bones.reserve(count);
    for (uint32_t i = 0; i < count; ++i) {
        BoneKey key;
        std::string ignored_name;
        if (!reader.ReadFixedString(15, ignored_name) || !reader.ReadU32(key.frame) ||
            !detail::ReadFloatArray(reader, key.position) ||
            !detail::ReadFloatArray(reader, key.rotation) ||
            !reader.ReadBytes(key.interpolation.data(), key.interpolation.size())) {
            error = "truncated bone keyframe section";
            return false;
        }
        document.bones.push_back(key);
    }

    if (!detail::ReadCount(reader, limits.max_morph_keys, count, error, "morph")) return false;
    document.morphs.reserve(count);
    for (uint32_t i = 0; i < count; ++i) {
        MorphKey key;
        std::string ignored_name;
        if (!reader.ReadFixedString(15, ignored_name) || !reader.ReadU32(key.frame) ||
            !reader.ReadFloat(key.weight)) {
            error = "truncated morph keyframe section";
            return false;
        }
        document.morphs.push_back(key);
    }

    if (!detail::ReadCount(reader, limits.max_camera_keys, count, error, "camera")) return false;
    document.cameras.reserve(count);
    for (uint32_t i = 0; i < count; ++i) {
        CameraKey key;
        uint32_t fov = 0;
        uint8_t perspective = 0;
        if (!reader.ReadU32(key.frame) || !reader.ReadFloat(key.distance) ||
            !detail::ReadFloatArray(reader, key.interest) ||
            !detail::ReadFloatArray(reader, key.euler) ||
            !reader.ReadBytes(key.interpolation.data(), key.interpolation.size()) ||
            !reader.ReadU32(fov) || !reader.ReadU8(perspective)) {
            error = "truncated camera keyframe section";
            return false;
        }
        if (fov == 0 || fov > 180) continue;
        key.fov = static_cast<float>(fov);
        key.perspective = perspective != 0;
        document.cameras.push_back(key);
    }

    // Light, self-shadow and IK sections are optional in older VMD files. If a
    // file ends after the camera section it is still a valid camera motion.
    if (reader.remaining() == 0) {
        detail::SortAndUnique(document.bones);
        detail::SortAndUnique(document.morphs);
        detail::SortAndUnique(document.cameras);
        return true;
    }
    if (!detail::ReadCount(reader, limits.max_light_keys, count, error, "light")) return false;
    document.lights.reserve(count);
    for (uint32_t i = 0; i < count; ++i) {
        LightKey key;
        if (!reader.ReadU32(key.frame) || !detail::ReadFloatArray(reader, key.color) ||
            !detail::ReadFloatArray(reader, key.position)) {
            error = "truncated light keyframe section";
            return false;
        }
        document.lights.push_back(key);
    }

    if (reader.remaining() == 0) {
        detail::SortAndUnique(document.bones);
        detail::SortAndUnique(document.morphs);
        detail::SortAndUnique(document.cameras);
        detail::SortAndUnique(document.lights);
        return true;
    }
    if (!detail::ReadCount(reader, limits.max_shadow_keys, count, error, "self-shadow")) return false;
    document.self_shadows.reserve(count);
    for (uint32_t i = 0; i < count; ++i) {
        SelfShadowKey key;
        if (!reader.ReadU32(key.frame) || !reader.ReadU8(key.mode) ||
            !reader.ReadFloat(key.distance)) {
            error = "truncated self-shadow keyframe section";
            return false;
        }
        document.self_shadows.push_back(key);
    }

    if (reader.remaining() != 0) {
        if (!detail::ReadCount(reader, limits.max_ik_keys, count, error, "IK")) return false;
        document.ik.reserve(count);
        for (uint32_t i = 0; i < count; ++i) {
            IkKey key;
            uint32_t toggle_count = 0;
            uint8_t show = 0;
            if (!reader.ReadU32(key.frame) || !reader.ReadU8(show) ||
                !detail::ReadCount(reader, limits.max_ik_toggles_per_key, toggle_count, error, "IK toggle")) {
                error = "truncated IK keyframe section";
                return false;
            }
            key.show = show != 0;
            key.toggles.reserve(toggle_count);
            for (uint32_t j = 0; j < toggle_count; ++j) {
                IkToggle toggle;
                uint8_t enabled = 0;
                if (!reader.ReadFixedString(20, toggle.name) || !reader.ReadU8(enabled)) {
                    error = "truncated IK toggle section";
                    return false;
                }
                toggle.enabled = enabled != 0;
                key.toggles.push_back(std::move(toggle));
            }
            document.ik.push_back(std::move(key));
        }
    }

    detail::SortAndUnique(document.bones);
    detail::SortAndUnique(document.morphs);
    detail::SortAndUnique(document.cameras);
    detail::SortAndUnique(document.lights);
    detail::SortAndUnique(document.self_shadows);
    detail::SortAndUnique(document.ik);
    return true;
}

} // namespace BetterEndfield::Vmd
