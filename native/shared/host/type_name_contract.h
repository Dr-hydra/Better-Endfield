#pragma once
#include <string_view>

namespace BetterEndfield::Host {
// IL2CPP versions use '/', '+' or '.' for nested type names. All other
// characters (including generic arguments and by-ref '&') remain exact.
inline bool SameTypeText(const char* actual, std::string_view expected) {
    if (!actual) return false;
    const std::string_view value(actual);
    if (value.size() != expected.size()) return false;
    auto normalize = [](char c) { return c == '/' || c == '+' ? '.' : c; };
    for (size_t i = 0; i < value.size(); ++i)
        if (normalize(value[i]) != normalize(expected[i])) return false;
    return true;
}
}
