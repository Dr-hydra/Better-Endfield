#include "config.h"
#include <fstream>
#include <sstream>
#include <algorithm>
#include <cstdlib>

namespace ecl {

namespace {
std::string Trim(const std::string& s) {
    size_t b = s.find_first_not_of(" \t\r\n");
    if (b == std::string::npos) return "";
    size_t e = s.find_last_not_of(" \t\r\n");
    return s.substr(b, e - b + 1);
}
bool ParseBool(const std::string& v, bool def) {
    std::string s = Trim(v);
    std::transform(s.begin(), s.end(), s.begin(),
                   [](char c) { return static_cast<char>(::tolower(c)); });
    if (s == "1" || s == "true" || s == "yes" || s == "on") return true;
    if (s == "0" || s == "false" || s == "no" || s == "off") return false;
    return def;
}

// 逗号/空格分隔的 9 个浮点(偏移坐标系基矩阵, 行主序)
void ParseFloats9(const std::string& v, float out[9]) {
    std::string s = v;
    for (char& c : s) {
        if (c == ',' || c == ';' || c == '\t') c = ' ';
    }
    std::istringstream is(s);
    float tmp[9] = {};
    int n = 0;
    while (n < 9 && (is >> tmp[n])) ++n;
    if (n == 9) {
        for (int i = 0; i < 9; ++i) out[i] = tmp[i];
    }
}
}  // namespace

LinkConfig LoadConfig(const std::string& iniPath) {
    LinkConfig cfg;
    std::ifstream in(iniPath);
    if (!in.is_open()) return cfg;
    bool section = false;
    std::string line;
    while (std::getline(in, line)) {
        line = Trim(line);
        if (line.empty() || line[0] == ';' || line[0] == '#') continue;
        if (line.front() == '[' && line.back() == ']') {
            section = (line == "[link]");
            continue;
        }
        if (!section) continue;
        const size_t eq = line.find('=');
        if (eq == std::string::npos) continue;
        const std::string key = Trim(line.substr(0, eq));
        const std::string val = Trim(line.substr(eq + 1));
        if (key == "enabled") cfg.enabled = ParseBool(val, cfg.enabled);
        else if (key == "port") cfg.port = static_cast<uint16_t>(std::atoi(val.c_str()));
        else if (key == "unix_space") cfg.unix_space = ParseBool(val, cfg.unix_space);
        else if (key == "pos_scale") cfg.pos_scale = static_cast<float>(std::atof(val.c_str()));
        else if (key == "fov_enabled") cfg.fov_enabled = ParseBool(val, cfg.fov_enabled);
        else if (key == "rotation_enabled") cfg.rotation_enabled = ParseBool(val, cfg.rotation_enabled);
        else if (key == "timeout_ms") cfg.timeout_ms = static_cast<uint32_t>(std::atoi(val.c_str()));
        else if (key == "hook_delay_ms") cfg.hook_delay_ms = static_cast<uint32_t>(std::atoi(val.c_str()));
        else if (key == "inventory") cfg.inventory = ParseBool(val, cfg.inventory);
        else if (key == "shmem") cfg.shmem = ParseBool(val, cfg.shmem);
        else if (key == "gamesys_probe") cfg.gamesys_probe = ParseBool(val, cfg.gamesys_probe);
        else if (key == "probe_open_marketing") cfg.probe_open_marketing = ParseBool(val, cfg.probe_open_marketing);
        else if (key == "probe_fov") cfg.probe_fov = static_cast<float>(std::atof(val.c_str()));
        else if (key == "probe_aperture") cfg.probe_aperture = static_cast<float>(std::atof(val.c_str()));
        else if (key == "pose_mode") cfg.pose_mode = std::atoi(val.c_str());
        else if (key == "lens_enabled") cfg.lens_enabled = ParseBool(val, cfg.lens_enabled);
        else if (key == "pose_relative") cfg.pose_relative = ParseBool(val, cfg.pose_relative);
        else if (key == "pose_max_offset") cfg.pose_max_offset = static_cast<float>(std::atof(val.c_str()));
        else if (key == "pose_rot_absolute") cfg.pose_rot_absolute = ParseBool(val, cfg.pose_rot_absolute);
        // v1.0.0at: 相对模式的旋转增量所在系(0 标准/1 世界系/2 本地帧)
        else if (key == "pose_rot_increment_frame") {
            const int v = std::atoi(val.c_str());
            cfg.pose_rot_increment_frame = (v >= 0 && v <= 2) ? v : 0;
        }
        else if (key == "pose_origin_dx") cfg.pose_origin_dx = static_cast<float>(std::atof(val.c_str()));
        else if (key == "pose_origin_dy") cfg.pose_origin_dy = static_cast<float>(std::atof(val.c_str()));
        else if (key == "pose_origin_dz") cfg.pose_origin_dz = static_cast<float>(std::atof(val.c_str()));
        else if (key == "pose_offset_basis") ParseFloats9(val, cfg.pose_offset_basis);
        else if (key == "pose_anchor_ox") cfg.pose_anchor_ox = static_cast<float>(std::atof(val.c_str()));
        else if (key == "pose_anchor_oy") cfg.pose_anchor_oy = static_cast<float>(std::atof(val.c_str()));
        else if (key == "pose_anchor_oz") cfg.pose_anchor_oz = static_cast<float>(std::atof(val.c_str()));
        // v1.0.0ah: 写入时刻补读锚点(**默认 true**; 要关掉写 pose_late_anchor=false)
        else if (key == "pose_late_anchor") cfg.pose_late_anchor = ParseBool(val, cfg.pose_late_anchor);
        // v1.0.0am: "隐藏游戏 UI"两档参数(策略 + 世界相机清位)
        else if (key == "ui_hide_strict") cfg.ui_hide_strict = ParseBool(val, cfg.ui_hide_strict);
        // v1.0.0ar: 主(世界)相机保活(ESC 时不让游戏把世界相机彻底关掉)
        else if (key == "ui_hide_world_keepalive")
            cfg.ui_hide_world_keepalive = ParseBool(val, cfg.ui_hide_world_keepalive);
        // v1.0.0ar: 隐藏 UI 时同时禁用 UI 相机(它遮罩=0 也照样清屏, 把世界画面擦黑)
        else if (key == "ui_hide_disable_uicam")
            cfg.ui_hide_disable_uicam = ParseBool(val, cfg.ui_hide_disable_uicam);
        // v1.0.0as: 光圈"景深保活"(改数值时 Apply + 周期性重施加)
        else if (key == "lens_dof_keepalive")
            cfg.lens_dof_keepalive = ParseBool(val, cfg.lens_dof_keepalive);
        // v1.0.0ap: 锚点来源自动回退
        else if (key == "pose_anchor_auto_fallback")
            cfg.pose_anchor_auto_fallback = ParseBool(val, cfg.pose_anchor_auto_fallback);
        else if (key == "ui_hide_maincam_clear_bits") {
            const std::string s = Trim(val);
            int v = 0;
            if (s.size() > 2 && s[0] == '0' && (s[1] == 'x' || s[1] == 'X'))
                v = static_cast<int>(std::strtol(s.c_str() + 2, nullptr, 16));
            else
                v = std::atoi(s.c_str());
            if (v < 0) v = 0;
            cfg.ui_hide_maincam_clear_bits = v;
        }
        else if (key == "pose_anchor_source") {
            std::string s = Trim(val);
            std::transform(s.begin(), s.end(), s.begin(),
                           [](char c) { return static_cast<char>(::tolower(c)); });
            if (s == "vcamfollow" || s == "follow") cfg.pose_anchor_source = 1;
            else if (s == "vcamlookat" || s == "lookat") cfg.pose_anchor_source = 2;
            else if (s == "controllertrans" || s == "controller") cfg.pose_anchor_source = 3;
            else if (s == "camminusoffset" || s == "offset") cfg.pose_anchor_source = 4;
            // v1.0.0ab(3 号方案): 5 = 引擎相机位置(游戏自己的机位, 带游戏的平滑/跟随阻尼)
            else if (s == "enginecam" || s == "enginecamera" || s == "engine") cfg.pose_anchor_source = 5;
            else cfg.pose_anchor_source = std::atoi(s.c_str());
            if (cfg.pose_anchor_source < 0 || cfg.pose_anchor_source > 5) cfg.pose_anchor_source = 0;
        }
        else if (key == "pose_anchor") {
            std::string s = Trim(val);
            std::transform(s.begin(), s.end(), s.begin(),
                           [](char c) { return static_cast<char>(::tolower(c)); });
            if (s == "reference" || s == "ref" || s == "cube" || s == "object")
                cfg.pose_anchor_reference = true;
            else if (s == "camera" || s == "cam" || s == "pose")
                cfg.pose_anchor_reference = false;
            else
                cfg.pose_anchor_reference = ParseBool(s, cfg.pose_anchor_reference);
        }
    }
    return cfg;
}

}  // namespace ecl
