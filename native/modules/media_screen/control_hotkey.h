#pragma once
#include "../../shared/input/hotkey.h"
namespace BetterEndfieldNext::MediaScreen {
inline thread_local unsigned real_control_key_reads=0;
struct RealControlKeys { RealControlKeys(){++real_control_key_reads;} ~RealControlKeys(){--real_control_key_reads;} };
inline bool RealKeyDown(int key){RealControlKeys allow;return Input::Down(key);}
inline bool ExactModifiers(int binding) {
    RealControlKeys allow;
    return Input::HasCtrl(binding)==Input::Down(VK_CONTROL) && Input::HasAlt(binding)==Input::Down(VK_MENU) &&
        Input::HasShift(binding)==Input::Down(VK_SHIFT) && Input::HasWin(binding)==(Input::Down(VK_LWIN)||Input::Down(VK_RWIN));
}
inline bool ExactKeyDown(int binding) { RealControlKeys allow;return binding && ExactModifiers(binding) && Input::Down(Input::BaseKey(binding)); }
}
