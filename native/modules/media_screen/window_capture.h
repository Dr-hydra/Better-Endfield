#pragma once
#include <cstdint>
#include <memory>
#include <string>
#include <vector>
#include "control_geometry.h"
namespace BetterEndfieldNext::MediaScreen {
struct WindowInputEvent {
    uint32_t message = 0, key = 0;
    int64_t data = 0;
    int32_t x = 0, y = 0;
};
class WindowCapture {
public:
    struct FrameData { uint64_t serial = 0; CaptureGeometry geometry{}; std::vector<unsigned char> rgba; };
    WindowCapture(); ~WindowCapture();
    void Start(uint64_t hwnd, uint32_t pid, int width, int height, int fps);
    void Stop();
    bool Take(uint64_t last_serial, FrameData& out);
    std::string Error();
    bool Control(bool enabled);
    bool Input(const WindowInputEvent& event);
    std::string InputError();
private:
    struct State;
    std::shared_ptr<State> state_;
};
}
