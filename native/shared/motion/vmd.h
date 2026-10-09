#pragma once
// Portions derived from EIEM (https://github.com/Sasye/EIEM), AGPL-3.0:
// src/vmd_parser.h, record layouts and timeline semantics, reviewed at 447600b9.
// No engine, Windows, filesystem or packed-struct dependency. Names remain raw
// CP932 bytes: failed/lossy decoding must never merge unrelated bone tracks.
#include <algorithm>
#include <array>
#include <bit>
#include <cmath>
#include <cstdint>
#include <limits>
#include <map>
#include <span>
#include <stdexcept>
#include <string>
#include <string_view>
#include <utility>
#include <vector>

namespace BetterEndfieldNext::Vmd {
inline constexpr double FramesPerSecond = 30.0;
struct Vec3 { float x = 0, y = 0, z = 0; };
struct Quaternion { float x = 0, y = 0, z = 0, w = 1; };
struct BoneKey {
    uint32_t frame = 0;
    Vec3 position;
    Quaternion rotation;
    std::array<uint8_t, 64> interpolation{};
};
struct MorphKey { uint32_t frame = 0; float weight = 0; };
struct CameraKey {
    uint32_t frame = 0;
    float distance = 0;
    Vec3 interest, euler;
    std::array<uint8_t, 24> interpolation{};
    float fov = 30;
    bool orthographic = false;
};
struct LightKey { uint32_t frame = 0; Vec3 color, position; };
struct ShadowKey { uint32_t frame = 0; uint8_t mode = 0; float distance = 0; };
struct SwitchKey { uint32_t frame = 0; bool enabled = true; };
template<class Key> using Tracks = std::map<std::string, std::vector<Key>, std::less<>>;
struct Motion {
    unsigned version = 0;
    std::string model_name_cp932;
    Tracks<BoneKey> bones;
    Tracks<MorphKey> morphs;
    std::vector<CameraKey> cameras;
    std::vector<LightKey> lights;
    std::vector<ShadowKey> shadows;
    std::vector<SwitchKey> visibility;
    Tracks<SwitchKey> ik;
    uint32_t last_frame = 0;
    size_t duplicate_keys = 0;
    size_t zero_quaternions = 0;
};
struct Limits {
    size_t file_bytes = 64u << 20;
    uint32_t bone_keys = 500000, morph_keys = 500000, camera_keys = 250000;
    uint32_t auxiliary_keys = 100000, ik_per_frame = 256, ik_entries = 500000;
    size_t tracks = 4096;
    uint32_t last_frame = 24u * 60u * 60u * 30u;
};
inline Quaternion Normalize(Quaternion q) {
    const double length = std::sqrt(double(q.x)*q.x + double(q.y)*q.y +
        double(q.z)*q.z + double(q.w)*q.w);
    if (!(length > 1e-12) || !std::isfinite(length)) return {};
    return {float(q.x/length), float(q.y/length), float(q.z/length), float(q.w/length)};
}

namespace detail {
class Reader {
public:
    explicit Reader(std::span<const uint8_t> bytes) : bytes_(bytes) {}
    size_t remaining() const { return bytes_.size() - offset_; }
    void section(const char* name) { section_ = name; }
    [[noreturn]] void fail(const char* text) const {
        throw std::runtime_error(std::string(section_) + " @ byte " +
            std::to_string(offset_) + ": " + text);
    }
    std::span<const uint8_t> take(size_t count) {
        if (count > remaining()) fail("truncated data");
        auto result = bytes_.subspan(offset_, count);
        offset_ += count;
        return result;
    }
    uint8_t u8() { return take(1)[0]; }
    uint32_t u32() {
        auto b = take(4);
        return uint32_t(b[0]) | (uint32_t(b[1]) << 8) |
            (uint32_t(b[2]) << 16) | (uint32_t(b[3]) << 24);
    }
    float real() {
        const float value = std::bit_cast<float>(u32());
        if (!std::isfinite(value)) fail("non-finite number");
        return value;
    }
    Vec3 vec3() { return {real(), real(), real()}; }
    std::string name(size_t bytes) {
        auto b = take(bytes);
        auto end = std::find(b.begin(), b.end(), uint8_t{0});
        return {reinterpret_cast<const char*>(b.data()), size_t(end - b.begin())};
    }
    uint32_t frame(uint32_t limit) {
        uint32_t value = u32();
        if (value > limit) fail("frame exceeds duration limit");
        return value;
    }
    bool boolean() {
        uint8_t value = u8();
        if (value > 1) fail("invalid boolean");
        return value != 0;
    }
    uint32_t count(const char* name, size_t record_bytes, uint32_t limit, bool required = false) {
        section(name);
        if (!required && remaining() == 0) return 0; // older writers omit tails
        const uint32_t n = u32();
        if (n > limit) fail("record count exceeds limit");
        if (n > remaining() / record_bytes) fail("record count exceeds available bytes");
        return n;
    }
private:
    std::span<const uint8_t> bytes_;
    size_t offset_ = 0;
    const char* section_ = "header";
};
template<class Key>
void append(Tracks<Key>& tracks, std::string name, Key key, const Limits& limits, Reader& reader) {
    if (name.empty()) reader.fail("empty track name");
    auto it = tracks.find(name);
    if (it == tracks.end()) {
        if (tracks.size() >= limits.tracks) reader.fail("too many distinct tracks");
        it = tracks.emplace(std::move(name), std::vector<Key>{}).first;
    }
    it->second.push_back(std::move(key));
}
template<class Key>
void sort_keys(std::vector<Key>& keys, Motion& motion) {
    std::stable_sort(keys.begin(), keys.end(), [](const Key& a, const Key& b) { return a.frame < b.frame; });
    size_t written = 0;
    for (size_t i = 0; i < keys.size(); ++i) {
        if (written && keys[written - 1].frame == keys[i].frame) {
            keys[written - 1] = keys[i]; // last source record wins
            ++motion.duplicate_keys;
        } else {
            if (written != i) keys[written] = keys[i];
            ++written;
        }
    }
    keys.resize(written);
    if (!keys.empty()) motion.last_frame = std::max(motion.last_frame, keys.back().frame);
}
}

// Transactional: on error, output is unchanged. Counts are checked against both
// policy limits and remaining bytes before allocation. Diagnostics are bounded.
inline bool Parse(std::span<const uint8_t> bytes, Motion& output, std::string& error,
    const Limits& limits = {}) {
    error.clear();
    try {
        if (bytes.size() > limits.file_bytes) throw std::runtime_error("VMD exceeds file size limit");
        detail::Reader r(bytes);
        Motion m;
        const std::string magic = r.name(30);
        if (magic == "Vocaloid Motion Data 0002") m.version = 2;
        else if (magic == "Vocaloid Motion Data file" || magic == "Vocaloid Motion Data") m.version = 1;
        else r.fail("unrecognized VMD signature");
        m.model_name_cp932 = r.name(m.version == 2 ? 20 : 10);
        for (uint32_t i = 0, n = r.count("bones", 111, limits.bone_keys, true); i < n; ++i) {
            auto name = r.name(15);
            BoneKey k;
            k.frame = r.frame(limits.last_frame);
            k.position = r.vec3();
            k.rotation = {r.real(), r.real(), r.real(), r.real()};
            const double norm = double(k.rotation.x)*k.rotation.x + double(k.rotation.y)*k.rotation.y +
                double(k.rotation.z)*k.rotation.z + double(k.rotation.w)*k.rotation.w;
            if (norm <= 1e-24) ++m.zero_quaternions;
            k.rotation = Normalize(k.rotation);
            auto ip = r.take(64);
            std::copy(ip.begin(), ip.end(), k.interpolation.begin());
            for (size_t j = 0; j < 16; ++j) if (ip[j] > 127) r.fail("invalid bone Bezier control");
            detail::append(m.bones, std::move(name), k, limits, r);
        }
        for (uint32_t i = 0, n = r.count("morphs", 23, limits.morph_keys); i < n; ++i) {
            auto name = r.name(15);
            MorphKey k{r.frame(limits.last_frame), r.real()};
            detail::append(m.morphs, std::move(name), k, limits, r);
        }
        for (uint32_t i = 0, n = r.count("cameras", 61, limits.camera_keys); i < n; ++i) {
            CameraKey k;
            k.frame = r.frame(limits.last_frame);
            k.distance = r.real(); k.interest = r.vec3(); k.euler = r.vec3();
            auto ip = r.take(24);
            for (auto value : ip) if (value > 127) r.fail("invalid camera Bezier control");
            std::copy(ip.begin(), ip.end(), k.interpolation.begin());
            const auto fov = r.u32();
            if (fov == 0 || fov >= 180) r.fail("FOV must be in (0, 180)");
            k.fov = float(fov); k.orthographic = r.boolean();
            m.cameras.push_back(k);
        }
        for (uint32_t i = 0, n = r.count("lights", 28, limits.auxiliary_keys); i < n; ++i)
            m.lights.push_back({r.frame(limits.last_frame), r.vec3(), r.vec3()});
        for (uint32_t i = 0, n = r.count("shadows", 9, limits.auxiliary_keys); i < n; ++i) {
            ShadowKey k{r.frame(limits.last_frame), r.u8(), r.real()};
            if (k.mode > 2) r.fail("invalid self-shadow mode");
            m.shadows.push_back(k);
        }
        uint32_t total_ik = 0;
        for (uint32_t i = 0, n = r.count("display/IK", 9, limits.auxiliary_keys); i < n; ++i) {
            const uint32_t frame = r.frame(limits.last_frame);
            m.visibility.push_back({frame, r.boolean()});
            const uint32_t count = r.u32();
            if (count > limits.ik_per_frame || count > limits.ik_entries - total_ik)
                r.fail("IK entry count exceeds limit");
            if (count > r.remaining()/21) r.fail("truncated IK entries");
            total_ik += count;
            for (uint32_t j = 0; j < count; ++j) {
                auto name = r.name(20);
                SwitchKey k{frame, r.boolean()};
                detail::append(m.ik, std::move(name), k, limits, r);
            }
        }
        if (r.remaining()) r.fail("unexpected bytes after final section");
        for (auto& [name, keys] : m.bones) detail::sort_keys(keys, m);
        for (auto& [name, keys] : m.morphs) detail::sort_keys(keys, m);
        for (auto& [name, keys] : m.ik) detail::sort_keys(keys, m);
        detail::sort_keys(m.cameras, m); detail::sort_keys(m.lights, m);
        detail::sort_keys(m.shadows, m); detail::sort_keys(m.visibility, m);
        output = std::move(m);
        return true;
    } catch (const std::exception& e) {
        error = e.what();
        return false;
    }
}

// All sampling functions consume normalized/sorted Parse() output. NaN time is
// treated as the start, infinities clamp to endpoints; no frame-index overflow.
inline double SafeFrame(double frame) { return std::isnan(frame) ? 0.0 : frame; }
inline double Bezier(uint8_t x1, uint8_t y1, uint8_t x2, uint8_t y2, double time) {
    if (!std::isfinite(time)) return time > 0 ? 1 : 0;
    const double t = std::clamp(time, 0.0, 1.0);
    if (x1 == y1 && x2 == y2) return t;
    auto axis = [](double a, double b, double s) {
        double u = 1-s;
        return 3*u*u*s*a + 3*u*s*s*b + s*s*s;
    };
    // Bracketed bisection also converges for flat derivatives at either endpoint.
    double lo = 0, hi = 1;
    for (int i = 0; i < 32; ++i) {
        double s = (lo+hi)*0.5;
        if (axis(x1/127.0, x2/127.0, s) < t) lo = s; else hi = s;
    }
    return axis(y1/127.0, y2/127.0, (lo+hi)*0.5);
}
inline float Mix(float a, float b, double t) { return float(double(a)*(1-t) + double(b)*t); }
inline Quaternion Slerp(Quaternion a, Quaternion b, double t) {
    a = Normalize(a); b = Normalize(b);
    double dot = double(a.x)*b.x + double(a.y)*b.y + double(a.z)*b.z + double(a.w)*b.w;
    if (dot < 0) { b = {-b.x,-b.y,-b.z,-b.w}; dot = -dot; }
    double wa = 1-t, wb = t;
    if (dot < 0.9995) {
        const double theta = std::acos(std::clamp(dot, 0.0, 1.0));
        const double denom = std::sin(theta);
        wa = std::sin((1-t)*theta)/denom; wb = std::sin(t*theta)/denom;
    }
    return Normalize({float(a.x*wa+b.x*wb),float(a.y*wa+b.y*wb),
        float(a.z*wa+b.z*wb),float(a.w*wa+b.w*wb)});
}
template<class Key>
inline std::pair<const Key*, const Key*> Segment(const std::vector<Key>& keys, double frame) {
    if (keys.empty()) return {nullptr,nullptr};
    frame = SafeFrame(frame);
    auto next = std::upper_bound(keys.begin(), keys.end(), frame,
        [](double f, const Key& k) { return f < k.frame; });
    if (next == keys.begin()) return {&keys.front(),&keys.front()};
    if (next == keys.end()) return {&keys.back(),&keys.back()};
    return {&*(next-1),&*next};
}
struct BoneSample { Vec3 position; Quaternion rotation; };
inline bool SampleBone(const std::vector<BoneKey>& keys, double frame, BoneSample& out) {
    auto [a,b] = Segment(keys, frame);
    if (!a) return false;
    out = {a->position,a->rotation};
    if (a == b) return true;
    const double t = (SafeFrame(frame)-a->frame)/double(b->frame-a->frame);
    const auto& ip = b->interpolation;
    auto curve = [&](size_t i) { return Bezier(ip[i],ip[i+4],ip[i+8],ip[i+12],t); };
    out.position = {Mix(a->position.x,b->position.x,curve(0)),
        Mix(a->position.y,b->position.y,curve(1)),Mix(a->position.z,b->position.z,curve(2))};
    out.rotation = Slerp(a->rotation,b->rotation,curve(3));
    return true;
}
inline float SampleMorph(const std::vector<MorphKey>& keys, double frame) {
    auto [a,b] = Segment(keys, frame);
    if (!a) return 0;
    return a == b ? a->weight : Mix(a->weight,b->weight,
        (SafeFrame(frame)-a->frame)/double(b->frame-a->frame));
}
inline bool SampleSwitch(const std::vector<SwitchKey>& keys, double frame, bool fallback = true) {
    frame = SafeFrame(frame);
    auto next = std::upper_bound(keys.begin(), keys.end(), frame,
        [](double f, const SwitchKey& k) { return f < k.frame; });
    return next == keys.begin() ? fallback : (next-1)->enabled;
}
struct CameraSample {
    Vec3 interest, euler;
    float distance = 0, fov = 30;
    bool orthographic = false;
};
inline bool SampleCamera(const std::vector<CameraKey>& keys, double frame, CameraSample& out) {
    auto [a,b] = Segment(keys, frame);
    if (!a) return false;
    out = {a->interest,a->euler,a->distance,a->fov,a->orthographic};
    if (a == b || b->frame-a->frame <= 1) return true; // adjacent-frame camera cut
    const double t = (SafeFrame(frame)-a->frame)/double(b->frame-a->frame);
    const auto& ip = b->interpolation;
    // Camera stores x1,x2,y1,y2 per curve, unlike the interleaved bone matrix.
    auto curve = [&](size_t i) { i *= 4; return Bezier(ip[i],ip[i+2],ip[i+1],ip[i+3],t); };
    out.interest = {Mix(a->interest.x,b->interest.x,curve(0)),
        Mix(a->interest.y,b->interest.y,curve(1)),Mix(a->interest.z,b->interest.z,curve(2))};
    const double rotation = curve(3);
    out.euler = {Mix(a->euler.x,b->euler.x,rotation),Mix(a->euler.y,b->euler.y,rotation),
        Mix(a->euler.z,b->euler.z,rotation)};
    out.distance = Mix(a->distance,b->distance,curve(4));
    out.fov = Mix(a->fov,b->fov,curve(5));
    return true;
}
} // namespace BetterEndfieldNext::Vmd
