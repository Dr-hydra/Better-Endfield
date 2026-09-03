#pragma once

// Mouse-to-touch conversion for the mobile UI.
//
// Switching DeviceInfo.inputType only changes how the UI looks; it produces no
// input.  The touch widgets read EnhancedTouch.Touch.activeTouches, which is
// filled exclusively from a Touchscreen InputDevice, so on a desktop that
// array is always empty and the touch HUD waits for a finger that never comes.
//
// Injecting synthetic Windows touch makes Unity's own backend create that
// Touchscreen device (verified 2026-08-29), so this module turns the left
// mouse button into a finger rather than faking anything inside IL2CPP.

namespace BetterEndfield::UiModule::TouchInput {

using LogFn = void (*)(const char*);

// Creates the synthetic pointer device and starts the hook thread.  Safe to
// call more than once; only the first call does anything.
bool Start(LogFn log);

// Releases any held contact, removes the hook and destroys the device.
void Stop();

// Follows the touch UI: conversion only runs while the mobile layout is on.
void SetEnabled(bool enabled);

}  // namespace BetterEndfield::UiModule::TouchInput
