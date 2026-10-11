#pragma once
#include "../../shared/input/hotkey.h"
#include "output_limits.h"
#include <algorithm>
#include <charconv>
#include <cmath>
#include <cstdint>
#include <locale>
#include <map>
#include <sstream>
#include <string>
#include <string_view>
#include <vector>

namespace BetterEndfieldNext::MediaScreen {
inline std::string Trim(std::string_view text) {
    const auto first = text.find_first_not_of(" \t\r");
    if (first == text.npos) return {};
    return std::string(text.substr(first, text.find_last_not_of(" \t\r") - first + 1));
}
struct Configuration {
    bool enabled = false, audio = true, loop = true, pause_window_bgm = true;
    bool wall_screen_enabled = true, desk_screen_enabled = true;
    std::string desk_fit_mode = "fit";
    std::string source = "image", path, images;
    std::string target = "ReceptionRoomImagePoster";
    int hotkey = VK_OEM_MINUS, pause_hotkey = Input::kCtrl | VK_OEM_MINUS, control_hotkey = Input::kAlt | VK_OEM_MINUS;
    int width = kDefaultOutputWidth, height = kDefaultOutputHeight, fps = kDefaultCaptureFps;
    double interval = 10, gain = 1;
    uint64_t window = 0;
    uint32_t window_pid = 0;
    bool operator==(const Configuration&) const = default;
};
inline bool ParseConfiguration(std::string_view text, Configuration& out, std::string& error) {
    if (text.find('\0') != text.npos) { error = "Invalid media configuration"; return false; }
    std::map<std::string, std::string> values;
    while (!text.empty()) {
        auto end = text.find('\n');
        auto line = Trim(text.substr(0, end));
        text = end == text.npos ? std::string_view{} : text.substr(end + 1);
        if (line.empty() || line[0] == ';' || line[0] == '#' || line[0] == '[') continue;
        auto equal = line.find('=');
        if (equal != line.npos) values[Trim(std::string_view(line).substr(0, equal))] = Trim(std::string_view(line).substr(equal + 1));
    }
    Configuration next;
    auto string = [&](const char* key, std::string& value) { if (values.contains(key)) value = values[key]; };
    auto boolean = [&](const char* key, bool& value) {
        if (!values.contains(key)) return true;
        const auto& v = values[key];
        if (v == "true" || v == "1") value = true;
        else if (v == "false" || v == "0") value = false;
        else return false;
        return true;
    };
    auto integer = [&](const char* key, auto& value) {
        if (!values.contains(key)) return true;
        const auto& v = values[key];
        auto result = std::from_chars(v.data(), v.data() + v.size(), value);
        return result.ec == std::errc{} && result.ptr == v.data() + v.size();
    };
    auto number = [&](const char* key, double& value) {
        if (!values.contains(key)) return true;
        const auto& v = values[key];
        // NDK 27 libc++ does not provide floating-point from_chars. Parsing
        // with the classic locale also keeps INI decimals independent of OS locale.
        std::istringstream input(v); input.imbue(std::locale::classic());
        input >> std::noskipws >> value;
        return !input.fail() && input.eof() && std::isfinite(value);
    };
    string("source", next.source); string("source_path", next.path);
    string("image_paths", next.images); string("target_object", next.target);
    string("desk_fit_mode", next.desk_fit_mode);
    if (!boolean("enabled", next.enabled) || !boolean("audio_enabled", next.audio) || !boolean("loop", next.loop) ||
        !boolean("pause_window_bgm", next.pause_window_bgm) ||
        !boolean("wall_screen_enabled", next.wall_screen_enabled) || !boolean("desk_screen_enabled", next.desk_screen_enabled) ||
        !integer("width", next.width) || !integer("height", next.height) ||
        !integer("fps", next.fps) || !integer("window_handle", next.window) || !integer("window_pid", next.window_pid) ||
        !number("slide_interval", next.interval) || !number("gain", next.gain)) {
        error = "Invalid media screen setting"; return false;
    }
    // Text bindings use the same key/modifier encoding as camera and HUD.
    // Migrate the old prototype's fixed F8 default to the main-keyboard minus.
    if (values.contains("toggle_hotkey")) next.hotkey = Input::ParseKey(values["toggle_hotkey"], -1);
    else if (values.contains("hotkey_vk")) {
        if (!integer("hotkey_vk", next.hotkey) || next.hotkey < 0 || next.hotkey > 255) {
            error = "Invalid legacy media hotkey"; return false;
        }
        if (next.hotkey == VK_F1 + 7) next.hotkey = VK_OEM_MINUS;
    }
    if (values.contains("pause_hotkey")) next.pause_hotkey = values["pause_hotkey"].empty() ? 0 : Input::ParseKey(values["pause_hotkey"], -1);
    if (values.contains("control_hotkey")) next.control_hotkey = values["control_hotkey"].empty() ? 0 : Input::ParseKey(values["control_hotkey"], -1);
    auto valid_binding = [](int key) {
        return key == 0 || (key > 0 && Input::BaseKey(key) > 0 && Input::BaseKey(key) <= 255);
    };
    if ((next.source != "image" && next.source != "slideshow" && next.source != "video" && next.source != "window") ||
        next.target.empty() || !valid_binding(next.hotkey) || !valid_binding(next.pause_hotkey) || !valid_binding(next.control_hotkey) ||
        (next.desk_fit_mode!="fit" && next.desk_fit_mode!="crop" && next.desk_fit_mode!="stretch") ||
        (next.hotkey && (next.hotkey == next.pause_hotkey || next.hotkey == next.control_hotkey)) ||
        (next.pause_hotkey && next.pause_hotkey == next.control_hotkey) ||
        !ValidOutput(next.width, next.height, next.fps) || next.interval < 1 || next.interval > 3600 || next.gain < 0 || next.gain > 2) {
        error = "Media screen setting is out of range"; return false;
    }
    out = std::move(next); error.clear(); return true;
}
inline std::vector<std::string> ImagePaths(const Configuration& config) {
    if (config.source != "slideshow") return config.path.empty() ? std::vector<std::string>{} : std::vector<std::string>{config.path};
    std::vector<std::string> result;
    std::string_view list(config.images);
    while (!list.empty() && result.size() < 256) {
        auto end = list.find('|'); auto item = Trim(list.substr(0, end));
        if (!item.empty()) result.push_back(std::move(item));
        list = end == list.npos ? std::string_view{} : list.substr(end + 1);
    }
    return result;
}
}
