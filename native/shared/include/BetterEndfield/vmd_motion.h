#pragma once

#include "BetterEndfield/vmd_parser.h"

#include <algorithm>
#include <cmath>
#include <cstddef>
#include <cstdint>
#include <string>
#include <string_view>
#include <unordered_map>
#include <utility>
#include <vector>

// Portable VMD sampling and timeline grouping.  This layer deliberately has
// no Unity, IL2CPP, or game-version assumptions.  The camera module adapts the
// returned samples to the current runtime only after it has validated the
// resolved transform/component contracts.
namespace BetterEndfield::Vmd {

struct Vec3Sample {
    float x = 0.0f;
    float y = 0.0f;
    float z = 0.0f;
};

struct QuaternionSample {
    float x = 0.0f;
    float y = 0.0f;
    float z = 0.0f;
    float w = 1.0f;
};

struct BoneSample {
    Vec3Sample position{};
    QuaternionSample rotation{};
    bool valid = false;
};

struct MorphSample {
    float weight = 0.0f;
    bool valid = false;
};

struct IkSample {
    bool show = false;
    std::vector<IkToggle> toggles;
    bool valid = false;
};

struct Motion {
    std::unordered_map<std::string, std::vector<BoneKey>> bones;
    std::unordered_map<std::string, std::vector<MorphKey>> morphs;
    std::vector<IkKey> ik;
};

inline Motion BuildMotion(const Document& document) {
    Motion motion;
    motion.ik = document.ik;
    for (const BoneKey& key : document.bones) {
        motion.bones[key.name].push_back(key);
    }
    for (const MorphKey& key : document.morphs) {
        motion.morphs[key.name].push_back(key);
    }
    return motion;
}

namespace motion_detail {

inline QuaternionSample Normalize(QuaternionSample value) {
    const double length = static_cast<double>(value.x) * value.x +
        static_cast<double>(value.y) * value.y +
        static_cast<double>(value.z) * value.z +
        static_cast<double>(value.w) * value.w;
    if (!std::isfinite(length) || length <= 1.0e-20) {
        return {};
    }
    const float inverse = static_cast<float>(1.0 / std::sqrt(length));
    value.x *= inverse;
    value.y *= inverse;
    value.z *= inverse;
    value.w *= inverse;
    return value;
}

inline float Dot(QuaternionSample a, QuaternionSample b) {
    return a.x * b.x + a.y * b.y + a.z * b.z + a.w * b.w;
}

inline QuaternionSample Slerp(QuaternionSample a, QuaternionSample b, float t) {
    a = Normalize(a);
    b = Normalize(b);
    t = (std::max)(0.0f, (std::min)(1.0f, t));
    float dot = Dot(a, b);
    if (dot < 0.0f) {
        b.x = -b.x; b.y = -b.y; b.z = -b.z; b.w = -b.w;
        dot = -dot;
    }
    dot = (std::max)(-1.0f, (std::min)(1.0f, dot));
    if (dot > 0.9995f) {
        return Normalize({a.x + (b.x - a.x) * t,
            a.y + (b.y - a.y) * t, a.z + (b.z - a.z) * t,
            a.w + (b.w - a.w) * t});
    }
    const float angle = std::acos(dot);
    const float sine = std::sin(angle);
    if (std::fabs(sine) <= 1.0e-7f) return a;
    const float first = std::sin((1.0f - t) * angle) / sine;
    const float second = std::sin(t * angle) / sine;
    return Normalize({first * a.x + second * b.x,
        first * a.y + second * b.y, first * a.z + second * b.z,
        first * a.w + second * b.w});
}

inline float Cubic(float first, float second, float u) {
    const float inverse = 1.0f - u;
    return 3.0f * inverse * inverse * u * first +
        3.0f * inverse * u * u * second + u * u * u;
}

inline float Bezier(std::array<uint8_t, 64> const& interpolation,
    size_t channel, float linear) {
    if (!std::isfinite(linear) || linear <= 0.0f) return 0.0f;
    if (linear >= 1.0f) return 1.0f;
    constexpr float inverse = 1.0f / 127.0f;
    const float x1 = interpolation[channel] * inverse;
    const float y1 = interpolation[4 + channel] * inverse;
    const float x2 = interpolation[8 + channel] * inverse;
    const float y2 = interpolation[12 + channel] * inverse;
    float low = 0.0f;
    float high = 1.0f;
    for (int i = 0; i < 32; ++i) {
        const float middle = (low + high) * 0.5f;
        if (Cubic(x1, x2, middle) < linear) low = middle;
        else high = middle;
    }
    return (std::max)(0.0f, (std::min)(1.0f,
        Cubic(y1, y2, (low + high) * 0.5f)));
}

template <class Key>
inline std::pair<const Key*, const Key*> Bracket(const std::vector<Key>& keys,
    double frame, float& linear) {
    linear = 0.0f;
    if (keys.empty() || !std::isfinite(frame)) return {nullptr, nullptr};
    if (frame <= static_cast<double>(keys.front().frame)) return {&keys.front(), nullptr};
    if (frame >= static_cast<double>(keys.back().frame)) return {&keys.back(), nullptr};
    const auto upper = std::upper_bound(keys.begin(), keys.end(), frame,
        [](double value, const Key& key) {
            return value < static_cast<double>(key.frame);
        });
    const Key* first = &*(upper - 1);
    const Key* second = &*upper;
    const double range = static_cast<double>(second->frame) - first->frame;
    linear = range > 0.0 ? static_cast<float>((frame - first->frame) / range) : 0.0f;
    return {first, second};
}

} // namespace motion_detail

inline bool SampleBone(const Motion& motion, std::string_view name, double frame,
    BoneSample& sample) {
    sample = {};
    const auto found = motion.bones.find(std::string(name));
    if (found == motion.bones.end()) return false;
    float linear = 0.0f;
    const auto [first, second] = motion_detail::Bracket(found->second, frame, linear);
    if (!first) return false;
    sample.position = {first->position[0], first->position[1], first->position[2]};
    sample.rotation = motion_detail::Normalize({first->rotation[0], first->rotation[1],
        first->rotation[2], first->rotation[3]});
    if (second) {
        const float tx = motion_detail::Bezier(first->interpolation, 0, linear);
        const float ty = motion_detail::Bezier(first->interpolation, 1, linear);
        const float tz = motion_detail::Bezier(first->interpolation, 2, linear);
        const float tr = motion_detail::Bezier(first->interpolation, 3, linear);
        sample.position = {
            first->position[0] + (second->position[0] - first->position[0]) * tx,
            first->position[1] + (second->position[1] - first->position[1]) * ty,
            first->position[2] + (second->position[2] - first->position[2]) * tz};
        sample.rotation = motion_detail::Slerp(sample.rotation,
            {second->rotation[0], second->rotation[1], second->rotation[2], second->rotation[3]}, tr);
    }
    sample.valid = true;
    return true;
}

inline bool SampleMorph(const Motion& motion, std::string_view name, double frame,
    MorphSample& sample) {
    sample = {};
    const auto found = motion.morphs.find(std::string(name));
    if (found == motion.morphs.end()) return false;
    float linear = 0.0f;
    const auto [first, second] = motion_detail::Bracket(found->second, frame, linear);
    if (!first) return false;
    sample.weight = first->weight;
    if (second) sample.weight += (second->weight - first->weight) * linear;
    sample.weight = (std::max)(0.0f, (std::min)(100.0f, sample.weight));
    sample.valid = std::isfinite(sample.weight);
    return sample.valid;
}

inline bool SampleIk(const Motion& motion, double frame, IkSample& sample) {
    sample = {};
    if (motion.ik.empty() || !std::isfinite(frame)) return false;
    const auto upper = std::upper_bound(motion.ik.begin(), motion.ik.end(), frame,
        [](double value, const IkKey& key) {
            return value < static_cast<double>(key.frame);
        });
    const IkKey& key = upper == motion.ik.begin() ? motion.ik.front() : *(upper - 1);
    sample.show = key.show;
    sample.toggles = key.toggles;
    sample.valid = true;
    return true;
}

} // namespace BetterEndfield::Vmd
