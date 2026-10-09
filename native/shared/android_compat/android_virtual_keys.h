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
namespace betterendfieldnext {

enum class VirtualKeyAction : int {
    Release = 0,
    // Held until an explicit Release. Used by the free-camera movement pad.
    Press = 1,
    // Queued edge for one logical key consumer: one observed high followed
    // by one observed low. Rapid taps cannot extend/merge a timed pulse.
    Pulse = 2,
};

// Returns false for an out-of-range code so a malformed command cannot index
// outside the table.
bool SetVirtualKey(int virtual_key, VirtualKeyAction action);

// Releases every latched key. Called when the panel goes away, so a control
// cannot be left stuck down by a torn-down Activity.
void ReleaseAllVirtualKeys();

bool VirtualKeyDown(int virtual_key);

}  // namespace betterendfieldnext
