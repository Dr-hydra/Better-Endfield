#pragma once
#include <array>
#include <cmath>
#include <cstdint>
#include <string>
#include <utility>
#include <vector>

namespace BetterEndfieldNext::CustomModel {

struct BemRootOffset {
    std::string path;
    std::array<float, 3> translation{};

    bool empty() const { return path.empty(); }
};

inline bool ValidVisualPath(const std::string& path) {
    if (path.empty() || path.size() > 256 || path.front() == '/' || path.back() == '/' ||
        path.find("..") != path.npos || path.find("//") != path.npos) return false;
    for (unsigned char c : path) {
        if (!(c == '/' || c == '_' || c == '-' || c == ' ' || (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9')))
            return false;
    }
    return true;
}

struct BemMaterialOverrides {
    std::vector<std::pair<std::string, float>> floats;
    std::vector<std::pair<std::string, std::array<float, 4>>> colors;
    std::vector<std::pair<std::string, std::array<float, 4>>> vectors;
    std::vector<std::string> disable_keywords;
    std::vector<std::string> enable_keywords;

    bool empty() const {
        return floats.empty() && colors.empty() && vectors.empty() &&
               disable_keywords.empty() && enable_keywords.empty();
    }
};

inline bool ValidOverrideFloat(float v, bool skip_validation = false) {
    if (!std::isfinite(v)) return false;
    if (skip_validation) return true;
    return v >= -128.0f && v <= 128.0f;
}

inline bool ValidOverrideProperty(const std::string& name, bool skip_validation = false) {
    if (name.size() < 2 || name.size() > 96) return false;
    if (!skip_validation && name[0] != '_') return false;
    for (unsigned char c : name) {
        if (!(c == '_' || (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9'))) {
            if (!skip_validation || c < 32 || c > 126) return false;
        }
    }
    return true;
}

inline bool ValidOverrideKeyword(const std::string& kw, bool skip_validation = false) {
    if (kw.size() < 2 || kw.size() > 96) return false;
    for (unsigned char c : kw) {
        if (!skip_validation) {
            if (!(c == '_' || (c >= 'A' && c <= 'Z') || (c >= '0' && c <= '9'))) return false;
        } else {
            if (c < 32 || c > 126) return false;
        }
    }
    return true;
}

} // namespace BetterEndfieldNext::CustomModel
