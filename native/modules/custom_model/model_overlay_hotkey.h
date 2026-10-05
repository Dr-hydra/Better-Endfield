#pragma once
#if defined(_WIN32)
#include "../../shared/input/hotkey.h"
#include <stdexcept>
namespace BetterEndfield::CustomModel {
inline int ParseModelOverlayHotkey(std::string_view value) {
    const int parsed=Input::ParseKey(value.empty()?"PLUS":value,-1);
    if(parsed==-1) throw std::runtime_error("Invalid model overlay hotkey");
    return parsed;
}
} // namespace BetterEndfield::CustomModel
#endif
