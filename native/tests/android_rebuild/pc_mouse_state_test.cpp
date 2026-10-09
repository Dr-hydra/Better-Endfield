#include "android_pc_mouse.h"
#include <cassert>
#include <iostream>
#include <limits>
#include <thread>
using betterendfieldnext::AndroidPcMouseState;
int main() {
    AndroidPcMouseState mouse;
    float x = 0, y = 0;
    mouse.Publish(true, true);
    assert(!mouse.CaptureRequested()); // no gameplay cursor intent yet
    mouse.CursorRequest(true);
    assert(!mouse.CaptureRequested()); // menus remain absolute
    assert(mouse.CursorMode() == 2);
    mouse.Absolute(0.2f, 0.3f);
    assert(mouse.ReadAbsolute(x, y) && x == 0.2f && y == 0.3f);
    mouse.NextFrame(); mouse.Publish(true, true); mouse.CursorRequest(true);
    assert(mouse.ReadAbsolute(x, y) && x == 0.2f && y == 0.3f); // stable publication/intent is idempotent
    mouse.DirectTouch(true);
    mouse.Absolute(0.4f, 0.5f);
    assert(!mouse.ReadAbsolute(x, y)); // real touchscreen owns position while fingers are down
    mouse.DirectTouch(false);
    assert(!mouse.ReadAbsolute(x, y)); // finger release waits for a new real mouse sample
    mouse.Absolute(0.4f, 0.5f);
    mouse.Foreground(false);
    assert(mouse.CursorMode() == 0 && !mouse.ReadAbsolute(x, y));
    mouse.Foreground(true);
    assert(mouse.CursorMode() == 2 && !mouse.ReadAbsolute(x, y));
    mouse.CursorRequest(false);
    assert(mouse.CaptureRequested());
    assert(mouse.CursorMode() == 1 && !mouse.ReadAbsolute(x, y));
    mouse.Motion(10, 20);
    assert(!mouse.Read(x, y)); // request is not proof capture was granted
    mouse.Captured(true);
    mouse.Motion(0.25f, -0.5f);
    mouse.Captured(true); // Java polling may reaffirm capture; pending deltas survive
    mouse.Motion(0.5f, -0.25f);
    assert(mouse.Read(x, y) && x == 0.75f && y == -0.75f);
    assert(mouse.Read(x, y) && x == 0.75f && y == -0.75f); // stable repeated reads
    mouse.Motion(10000, 20000);
    assert(mouse.Read(x, y) && x == 0.75f && y == -0.75f);
    mouse.NextFrame();
    assert(mouse.Read(x, y) && x == 10000 && y == 20000); // no screen-edge bound
    mouse.NextFrame();
    assert(mouse.Read(x, y) && x == 0 && y == 0); // no stale-frame drift
    mouse.Motion(std::numeric_limits<float>::quiet_NaN(), 5);
    mouse.NextFrame();
    assert(mouse.Read(x, y) && x == 0 && y == 0);
    std::thread events([&] { for (int i = 0; i < 1000; ++i) mouse.Motion(0.25f, -0.25f); });
    events.join();
    mouse.NextFrame();
    assert(mouse.Read(x, y) && x == 250 && y == -250); // subpixel events accumulate
    mouse.Motion(5, 5);
    mouse.CursorRequest(true);
    assert(!mouse.CaptureRequested() && !mouse.Read(x, y));
    mouse.CursorRequest(false);
    mouse.Captured(true);
    assert(mouse.Read(x, y) && x == 0 && y == 0); // menu clears queued movement
    mouse.Foreground(false);
    assert(!mouse.CaptureRequested() && !mouse.Read(x, y));
    mouse.Foreground(true);
    assert(mouse.CaptureRequested() && !mouse.Read(x, y)); // recapture acknowledgement required
    mouse.Captured(true);
    mouse.Motion(10, 10);
    mouse.Captured(false);
    mouse.Captured(true);
    assert(mouse.Read(x, y) && x == 0 && y == 0);
    mouse.Publish(false, true);
    assert(!mouse.CaptureRequested() && !mouse.Read(x, y));
    mouse.Publish(true, false);
    assert(!mouse.CaptureRequested()); // unresolved consumer never captures
    mouse.Publish(true, true);
    mouse.Captured(true);
    mouse.Reset();
    assert(!mouse.CaptureRequested() && !mouse.Read(x, y));
    assert(mouse.CursorMode() == 0 && !mouse.ReadAbsolute(x, y));
    mouse.Publish(true, true);
    assert(!mouse.CaptureRequested()); // a new session cannot reuse old intent
    std::cout << "PASS Android relative mouse state: capture gating, fractional accumulation, frame snapshots, unbounded movement, menu/focus/off/stop cleanup\n";
}
