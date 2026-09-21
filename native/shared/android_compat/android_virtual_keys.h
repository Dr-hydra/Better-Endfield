#pragma once

#include <cstdint>

// The desktop UI and camera modules decide what to do by polling
// GetAsyncKeyState. A phone has no keyboard, so the in-game panel presses the
// keys instead and the desktop hotkey code runs unchanged.
//
// This is deliberately not routed through the runtime command pump: the pump is
// a single-slot, generation-checked queue drained on a Unity hook, which is the
// right shape for configuration but would drop the release event of a
// press-and-hold control. A latch is a plain atomic, so the panel can write it
// from the Android UI thread while the camera input thread polls it every 5 ms.
namespace betterendfield {

enum class VirtualKeyAction : int {
    Release = 0,
    // Held until an explicit Release. Used by the free-camera movement pad.
    Press = 1,
    // Auto-releases after kVirtualKeyPulseMs. Used by every toggle, so one tap
    // produces exactly one rising edge no matter how the game is polling.
    Pulse = 2,
};

// Long enough for the UI module's per-frame pump and the camera module's 5 ms
// input thread to both observe the press, short enough that a second tap is
// never swallowed.
constexpr std::uint64_t kVirtualKeyPulseMs = 180;

// Returns false for an out-of-range code so a malformed command cannot index
// outside the table.
bool SetVirtualKey(int virtual_key, VirtualKeyAction action);

// Releases every latched key. Called when the panel goes away, so a control
// cannot be left stuck down by a torn-down Activity.
void ReleaseAllVirtualKeys();

bool VirtualKeyDown(int virtual_key);

}  // namespace betterendfield
