#pragma once
#include "../../shared/motion/vmd.h"
#include <iomanip>
#include <locale>
#include <sstream>

namespace BetterEndfield::CameraPath {
inline constexpr size_t MaxKeys = 64, MaxFileBytes = 65536;
struct Key {
    Vmd::Vec3 position;
    Vmd::Quaternion rotation;
    float fov = 60;
};
struct Path {
    double segment_seconds = 3;
    bool loop = false;
    std::vector<Key> keys;
};
inline bool Validate(Path& path, std::string& error) {
    error.clear();
    if (!std::isfinite(path.segment_seconds) || path.segment_seconds < 0.2 || path.segment_seconds > 60) {
        error = "segment_seconds must be in [0.2, 60]"; return false;
    }
    if (path.keys.empty() || path.keys.size() > MaxKeys) {
        error = "path must contain 1..64 keys"; return false;
    }
    for (auto& key : path.keys) {
        const auto p = key.position; const auto q = key.rotation;
        for (float x : {p.x,p.y,p.z}) {
            if (!std::isfinite(x) || std::abs(x) > 1000000) {
                error = "non-finite/out-of-range position"; return false;
            }
        }
        const double norm = double(q.x)*q.x + double(q.y)*q.y + double(q.z)*q.z + double(q.w)*q.w;
        if (!std::isfinite(norm) || norm < 1e-12) { error = "invalid quaternion"; return false; }
        if (!std::isfinite(key.fov) || key.fov < 5 || key.fov > 150) {
            error = "FOV must be in [5, 150]"; return false;
        }
    }
    // Normalize only after the whole path has passed validation.
    for (auto& key : path.keys) key.rotation = Vmd::Normalize(key.rotation);
    return true;
}
inline bool Encode(Path path, std::string& text, std::string& error) {
    if (!Validate(path,error)) return false;
    std::ostringstream stream;
    stream.imbue(std::locale::classic());
    stream << std::setprecision(std::numeric_limits<double>::max_digits10);
    stream << "BE_CAMERA_PATH 1\nsegment_seconds " << path.segment_seconds << "\nloop "
        << (path.loop ? 1 : 0) << "\nkeys " << path.keys.size() << '\n';
    for (const auto& key : path.keys) {
        stream << key.position.x << ' ' << key.position.y << ' ' << key.position.z << ' '
            << key.rotation.x << ' ' << key.rotation.y << ' ' << key.rotation.z << ' '
            << key.rotation.w << ' ' << key.fov << '\n';
    }
    text = stream.str();
    return true;
}
inline bool Decode(std::string_view text, Path& output, std::string& error) {
    error.clear();
    if (text.size() > MaxFileBytes) { error = "path file exceeds 64 KiB"; return false; }
    if (text.starts_with("\xEF\xBB\xBF")) text.remove_prefix(3); // UTF-8 BOM from editors
    std::istringstream stream{std::string(text)};
    stream.imbue(std::locale::classic());
    Path path;
    std::string tag, version, loop;
    size_t count = 0;
    if (!(stream >> tag >> version) || tag != "BE_CAMERA_PATH" || version != "1") {
        error = "unsupported camera path signature/version"; return false;
    }
    if (!(stream >> tag >> path.segment_seconds) || tag != "segment_seconds" ||
        !(stream >> tag >> loop) || tag != "loop" || (loop != "0" && loop != "1") ||
        !(stream >> tag >> count) || tag != "keys" || count == 0 || count > MaxKeys) {
        error = "invalid camera path header"; return false;
    }
    path.loop = loop == "1";
    path.keys.resize(count);
    for (auto& k : path.keys) {
        if (!(stream >> k.position.x >> k.position.y >> k.position.z >> k.rotation.x >>
            k.rotation.y >> k.rotation.z >> k.rotation.w >> k.fov)) {
            error = "truncated/invalid camera path key"; return false;
        }
    }
    stream >> std::ws;
    if (!stream.eof()) { error = "unexpected tokens after camera path"; return false; }
    if (!Validate(path,error)) return false;
    output = std::move(path);
    return true;
}
inline float Cubic(float p0, float p1, float p2, float p3, double t) {
    const double a = p0, b = p1, c = p2, d = p3;
    return float(0.5*(2*b + (c-a)*t + (2*a-5*b+4*c-d)*t*t + (3*b-a-3*c+d)*t*t*t));
}
// Playback uses an immutable snapshot. Empty/single-key paths and exact endpoints
// are handled before any size-2 arithmetic; editing cannot invalidate a sample.
inline bool Sample(const Path& path, double seconds, Key& output, bool& finished) {
    finished = false;
    if (path.keys.empty() || !(path.segment_seconds > 0) || !std::isfinite(path.segment_seconds)) return false;
    if (path.keys.size() == 1) { output = path.keys.front(); finished = true; return true; }
    if (std::isnan(seconds) || seconds < 0) seconds = 0;
    const double total = path.segment_seconds*double(path.keys.size()-1);
    if (!std::isfinite(seconds)) seconds = total;
    if (!path.loop && seconds >= total) { output = path.keys.back(); finished = true; return true; }
    if (path.loop) seconds = std::fmod(seconds,total);
    const double progress = seconds/total;
    const double eased = path.loop ? progress : progress*progress*(3-2*progress);
    const double scaled = eased*double(path.keys.size()-1);
    const size_t index = std::min(size_t(scaled),path.keys.size()-2);
    const double t = scaled-double(index);
    const auto& a = path.keys[index ? index-1 : 0];
    const auto& b = path.keys[index]; const auto& c = path.keys[index+1];
    const auto& d = path.keys[std::min(index+2,path.keys.size()-1)];
    output.position = {Cubic(a.position.x,b.position.x,c.position.x,d.position.x,t),
        Cubic(a.position.y,b.position.y,c.position.y,d.position.y,t),
        Cubic(a.position.z,b.position.z,c.position.z,d.position.z,t)};
    output.rotation = Vmd::Slerp(b.rotation,c.rotation,t);
    output.fov = Vmd::Mix(b.fov,c.fov,t); // bounded linear FOV, no spline overshoot
    return true;
}
} // namespace BetterEndfield::CameraPath
