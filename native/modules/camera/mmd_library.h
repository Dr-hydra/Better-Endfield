#pragma once
// MMD work library: <install>\mmd\<work folder>\set.ini plus the files it names.
// The manager copies imported files there; the camera module and the overlay
// only read it. set.ini is UTF-8:
//   [set]
//   name=Display name
//   motion=motion.vmd      ; body/face motion VMD (required)
//   camera=camera.vmd      ; optional, falls back to the motion's camera keys
//   face=face.vmd          ; optional, replaces the motion's morph keys
//   motion2..motion4       ; optional, squad playback: 2nd..4th dancer
//   face2..face4           ; optional, their face VMDs
//   music=music.mp3        ; optional WAV/MP3/M4A/AAC/FLAC/WMA
//   audio_offset=0         ; seconds; music position = motion time + offset
// File entries must be plain names inside the work folder.
#include <algorithm>
#include <cmath>
#include <cstdlib>
#include <filesystem>
#include <fstream>
#include <string>
#include <string_view>
#include <system_error>
#include <vector>

namespace BetterEndfield::MmdLibrary {

inline constexpr size_t kMaxWorks = 512;
inline constexpr uintmax_t kMaxSetFileBytes = 16u * 1024u;
inline constexpr size_t kExtraDancers = 3; // motion2..motion4

struct Work {
    std::filesystem::path folder;
    std::string folder_name; // UTF-8 folder name, the stable identifier
    std::string name;        // UTF-8 display name
    std::filesystem::path motion, camera, face, music;
    std::filesystem::path extra_motion[kExtraDancers], extra_face[kExtraDancers];
    double audio_offset = 0.0;
};

inline std::string Trim(std::string_view text) {
    size_t begin = 0, end = text.size();
    while (begin < end && static_cast<unsigned char>(text[begin]) <= ' ') ++begin;
    while (end > begin && static_cast<unsigned char>(text[end - 1]) <= ' ') --end;
    return std::string(text.substr(begin, end - begin));
}

inline std::filesystem::path FromUtf8(std::string_view text) {
    return std::filesystem::path(std::u8string(text.begin(), text.end()));
}

inline std::string ToUtf8(const std::filesystem::path& path) {
    const std::u8string text = path.u8string();
    return std::string(text.begin(), text.end());
}

// Only a bare file name is accepted: no separators, drive letters or "..".
inline bool PlainFileName(std::string_view name) {
    if (name.empty() || name.size() > 200 || name == "." || name == "..") return false;
    for (char character : name) {
        if (character == '/' || character == '\\' || character == ':' ||
            static_cast<unsigned char>(character) < ' ') {
            return false;
        }
    }
    return true;
}

inline bool PlainFolderName(std::string_view name) { return PlainFileName(name); }

inline bool LoadWork(const std::filesystem::path& folder, Work& work, std::string& error) {
    std::error_code status;
    const std::filesystem::path file = folder / L"set.ini";
    const uintmax_t size = std::filesystem::file_size(file, status);
    if (status || size == 0 || size > kMaxSetFileBytes) {
        error = "set.ini missing or too large";
        return false;
    }
    std::ifstream input(file, std::ios::binary);
    std::string text((std::istreambuf_iterator<char>(input)), std::istreambuf_iterator<char>());
    if (text.size() >= 3 && static_cast<unsigned char>(text[0]) == 0xEF &&
        static_cast<unsigned char>(text[1]) == 0xBB && static_cast<unsigned char>(text[2]) == 0xBF) {
        text.erase(0, 3);
    }
    Work next;
    next.folder = folder;
    next.folder_name = ToUtf8(folder.filename());
    next.name = next.folder_name;
    size_t position = 0;
    while (position <= text.size()) {
        size_t end = text.find('\n', position);
        if (end == std::string::npos) end = text.size();
        const std::string line = Trim(std::string_view(text).substr(position, end - position));
        position = end + 1;
        if (line.empty() || line[0] == ';' || line[0] == '#' || line[0] == '[') continue;
        const size_t equals = line.find('=');
        if (equals == std::string::npos) continue;
        std::string key = Trim(std::string_view(line).substr(0, equals));
        std::transform(key.begin(), key.end(), key.begin(),
            [](unsigned char c) { return static_cast<char>(std::tolower(c)); });
        const std::string value = Trim(std::string_view(line).substr(equals + 1));
        const auto file_entry = [&](std::filesystem::path& target) {
            if (value.empty()) return true;
            if (!PlainFileName(value)) {
                error = key + " must be a file name inside the work folder";
                return false;
            }
            target = folder / FromUtf8(value);
            return true;
        };
        if (key == "name" && !value.empty()) next.name = value.substr(0, 120);
        else if (key == "motion") { if (!file_entry(next.motion)) return false; }
        else if (key == "camera") { if (!file_entry(next.camera)) return false; }
        else if (key == "face") { if (!file_entry(next.face)) return false; }
        else if (key == "music") { if (!file_entry(next.music)) return false; }
        else if (key.size() == 7 && key.compare(0, 6, "motion") == 0 && key[6] >= '2' && key[6] <= '4') {
            if (!file_entry(next.extra_motion[key[6] - '2'])) return false;
        } else if (key.size() == 5 && key.compare(0, 4, "face") == 0 && key[4] >= '2' && key[4] <= '4') {
            if (!file_entry(next.extra_face[key[4] - '2'])) return false;
        }
        else if (key == "audio_offset") {
            const double offset = std::strtod(value.c_str(), nullptr);
            next.audio_offset = std::isfinite(offset) ? std::clamp(offset, -600.0, 600.0) : 0.0;
        }
    }
    if (next.motion.empty() && next.camera.empty()) {
        error = "set.ini names neither a motion nor a camera VMD";
        return false;
    }
    work = std::move(next);
    return true;
}

inline std::vector<Work> Scan(const std::filesystem::path& root) {
    std::vector<Work> works;
    std::error_code status;
    if (!std::filesystem::is_directory(root, status)) return works;
    for (std::filesystem::directory_iterator it(root, status), end; !status && it != end;
         it.increment(status)) {
        if (!it->is_directory(status) || works.size() >= kMaxWorks) continue;
        Work work;
        std::string error;
        if (LoadWork(it->path(), work, error)) works.push_back(std::move(work));
    }
    std::sort(works.begin(), works.end(),
        [](const Work& a, const Work& b) { return a.name < b.name; });
    return works;
}

} // namespace BetterEndfield::MmdLibrary
