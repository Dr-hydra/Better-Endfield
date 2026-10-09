#pragma once

#include <Windows.h>

#include <cctype>
#include <cstdlib>
#include <string>
#include <string_view>

namespace BetterEndfieldNext::Input {

// Low 16 bits contain the virtual key. Higher bits contain modifiers and the
// numpad-enter distinction. Keeping the binding in one integer lets existing
// module configuration fields remain ABI-neutral.
constexpr int kCtrl = 1 << 16;
constexpr int kAlt = 1 << 17;
constexpr int kShift = 1 << 18;
constexpr int kWin = 1 << 19;
constexpr int kNumpadEnter = 1 << 24;
constexpr int kKeyMask = 0xFFFF;

inline int BaseKey(int binding) { return binding & kKeyMask; }
inline bool HasCtrl(int binding) { return (binding & kCtrl) != 0; }
inline bool HasAlt(int binding) { return (binding & kAlt) != 0; }
inline bool HasShift(int binding) { return (binding & kShift) != 0; }
inline bool HasWin(int binding) { return (binding & kWin) != 0; }
inline bool IsNumpadEnter(int binding) { return (binding & kNumpadEnter) != 0; }

inline bool Down(int key) {
    return key != 0 && (GetAsyncKeyState(key) & 0x8000) != 0;
}

inline bool ModifiersDown(int binding) {
    return (!HasCtrl(binding) || Down(VK_CONTROL)) &&
        (!HasAlt(binding) || Down(VK_MENU)) &&
        (!HasShift(binding) || Down(VK_SHIFT)) &&
        (!HasWin(binding) || Down(VK_LWIN) || Down(VK_RWIN));
}

inline bool IsDown(int binding) {
    if (!binding || !ModifiersDown(binding)) return false;
    return Down(BaseKey(binding));
}

inline int ParseKey(std::string_view value, int fallback) {
    std::string text(value);
    while (!text.empty() && std::isspace(static_cast<unsigned char>(text.front()))) text.erase(text.begin());
    while (!text.empty() && std::isspace(static_cast<unsigned char>(text.back()))) text.pop_back();
    for (char& c : text) c = static_cast<char>(std::toupper(static_cast<unsigned char>(c)));
    if (text.empty()) return fallback;
    if (text == "+") text = "PLUS";
    else if (text.size() >= 2 && text.ends_with("++")) { text.resize(text.size() - 1); text += "PLUS"; }

    int binding = 0;
    size_t start = 0;
    while (true) {
        const size_t plus = text.find('+', start);
        const std::string token = text.substr(start, plus == std::string::npos ? plus : plus - start);
        if (token == "CTRL" || token == "CONTROL") binding |= kCtrl;
        else if (token == "ALT" || token == "MENU") binding |= kAlt;
        else if (token == "SHIFT") binding |= kShift;
        else if (token == "WIN" || token == "WINDOWS") binding |= kWin;
        else {
            int key = 0;
            if (token == "NONE" || token == "OFF" || token == "DISABLED") key = 0;
            else if (token == "-" || token == "MINUS" || token == "OEM_MINUS") key = VK_OEM_MINUS;
            else if (token == "=" || token == "PLUS" || token == "OEM_PLUS" || token == "OEMPLUS") key = 0xBB;
            else if (token == "SUBTRACT" || token == "NUMPAD-") key = VK_SUBTRACT;
            else if (token == "ADD" || token == "NUMPAD+") key = VK_ADD;
            else if (token == "DECIMAL" || token == "NUMPAD.") key = VK_DECIMAL;
            else if (token == "MULTIPLY" || token == "NUMPAD*") key = VK_MULTIPLY;
            else if (token == "DIVIDE" || token == "NUMPAD/") key = VK_DIVIDE;
            else if (token == "NUMPAD_ENTER" || token == "NUMPADENTER" || token == "NUMPAD_RETURN") {
                key = VK_RETURN; binding |= kNumpadEnter;
            } else if (token.size() == 1 && std::isalnum(static_cast<unsigned char>(token[0]))) {
                key = static_cast<unsigned char>(token[0]);
            } else if (token.size() > 1 && token[0] == 'F') {
                const int number = std::atoi(token.c_str() + 1);
                if (number >= 1 && number <= 24) key = VK_F1 + number - 1;
            } else if (token.rfind("NUMPAD", 0) == 0 && token.size() == 7 && token.back() >= '0' && token.back() <= '9') {
                key = VK_NUMPAD0 + token.back() - '0';
            } else if (token == "ENTER" || token == "RETURN") key = VK_RETURN;
            else if (token == "SPACE") key = VK_SPACE;
            else if (token == "TAB") key = VK_TAB;
            else if (token == "ESC" || token == "ESCAPE") key = VK_ESCAPE;
            else if (token == "UP") key = VK_UP;
            else if (token == "DOWN") key = VK_DOWN;
            else if (token == "LEFT") key = VK_LEFT;
            else if (token == "RIGHT") key = VK_RIGHT;
            if (key == 0 && token != "NONE" && token != "OFF" && token != "DISABLED") return fallback;
            binding |= key;
        }
        if (plus == std::string::npos) break;
        start = plus + 1;
    }
    return binding;
}

} // namespace BetterEndfieldNext::Input
