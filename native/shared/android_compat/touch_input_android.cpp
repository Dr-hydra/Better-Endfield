#include "touch_input.h"

// The desktop UI module injects synthetic Windows touch so Unity creates a
// Touchscreen device and the mobile HUD starts receiving fingers. An Android
// client already has a real Touchscreen, so there is nothing to convert and no
// mouse to convert from. These are the whole-file stand-ins for the desktop
// implementation; the module source is shared and calls them unconditionally.
namespace BetterEndfield::UiModule::TouchInput {

bool Start(LogFn log) {
    if (log != nullptr) {
        log("Mouse-to-touch conversion is not used on Android; the client has a "
            "real Touchscreen device.");
    }
    return true;
}

void Stop() {}

void SetEnabled(bool) {}

}  // namespace BetterEndfield::UiModule::TouchInput
