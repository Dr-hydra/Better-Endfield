#include "window_capture.h"
#include "capture_ipc.h"
#include <atomic>
#include <chrono>
#include <condition_variable>
#include <mutex>
#include <thread>

namespace BetterEndfieldNext::MediaScreen {
struct WindowCapture::State {
    std::thread worker;
    std::atomic_bool stop{false};
    std::mutex mutex;
    std::condition_variable wake;
    std::string error;
    FrameData frame;
    std::unique_ptr<Ipc::Channel> channel;
    uint64_t request = 0;
    void Fail(const std::string& value) { std::lock_guard lock(mutex); if (error.empty()) error = value; }
};
WindowCapture::WindowCapture() = default;
WindowCapture::~WindowCapture() { Stop(); }
void WindowCapture::Stop() {
    auto state = state_; if (!state) return;
    state->stop.store(true); state->wake.notify_all();
    if (state->worker.joinable()) state->worker.join();
    Ipc::Lock lock(*state->channel);
    if (lock.acquired && Ipc::Valid(*state->channel->data)) {
        auto& value = *state->channel->data;
        if (value.client_pid == GetCurrentProcessId() && value.request == state->request) { value.control_active = 0; ++value.control_generation; value.active = 0; value.client_pid = 0; }
    }
    state_.reset();
}
bool WindowCapture::Take(uint64_t last, FrameData& out) {
    auto state = state_; if (!state) return false;
    std::lock_guard lock(state->mutex);
    if (!state->frame.serial || state->frame.serial == last) return false;
    out = state->frame; return true;
}
std::string WindowCapture::Error() {
    auto state = state_; if (!state) return {};
    std::lock_guard lock(state->mutex); return state->error;
}
bool WindowCapture::Control(bool enabled) {
    auto state = state_; if (!state) return !enabled;
    Ipc::Lock lock(*state->channel);
    if (!lock.acquired) return false;
    auto& value=*state->channel->data;
    if (!Ipc::Valid(value) || value.client_pid!=GetCurrentProcessId() || value.request!=state->request || !value.active) return false;
    value.control_active=enabled?1:0; ++value.control_generation;
    value.input_read=value.input_write=0; value.input_error[0]='\0'; return true;
}
bool WindowCapture::Input(const WindowInputEvent& event) {
    auto state = state_; if (!state) return false;
    Ipc::Lock lock(*state->channel, 10); if (!lock.acquired) return false;
    auto& value=*state->channel->data;
    if (!Ipc::Valid(value) || !value.active || !value.control_active || value.client_pid!=GetCurrentProcessId() || value.request!=state->request) return false;
    if (value.input_write-value.input_read>=Ipc::kInputCapacity) { Ipc::InputError(value,"Window input queue overflow"); return false; }
    value.input[value.input_write++ % Ipc::kInputCapacity]=event; return true;
}
std::string WindowCapture::InputError() {
    auto state=state_; if (!state) return {};
    Ipc::Lock lock(*state->channel, 10); if (!lock.acquired) return {};
    const auto& value=*state->channel->data;
    if (!Ipc::Valid(value) || value.client_pid!=GetCurrentProcessId() || value.request!=state->request) return "Window input session was replaced";
    return std::string(value.input_error,strnlen_s(value.input_error,sizeof(value.input_error)));
}
void WindowCapture::Start(uint64_t window, uint32_t pid, int width, int height, int fps) {
    Stop();
    if (!ValidOutput(width, height, fps)) throw std::runtime_error("Capture output dimensions unsupported");
    HWND hwnd = reinterpret_cast<HWND>(static_cast<uintptr_t>(window)); DWORD actual = 0;
    if (!IsWindow(hwnd) || !GetWindowThreadProcessId(hwnd, &actual) || !pid || actual != pid || actual == GetCurrentProcessId())
        throw std::runtime_error("Selected capture window is no longer available");
    auto state = std::make_shared<State>();
    state->channel = std::make_unique<Ipc::Channel>(false);
    {
        Ipc::Lock lock(*state->channel);
        if (!lock.acquired) throw std::runtime_error("Window capture helper is busy");
        auto& value = *state->channel->data;
        if (!Ipc::Valid(value) || GetTickCount64() - value.broker_heartbeat > 3000)
            throw std::runtime_error("Window capture helper is not ready. Open the Better Endfield controller.");
        if (value.active && value.client_pid != GetCurrentProcessId() && GetTickCount64() - value.client_heartbeat < 3000)
            throw std::runtime_error("Window capture helper is already in use");
        value.width = width; value.height = height; value.fps = fps;
        if (!Ipc::Dimensions(value)) throw std::runtime_error("Capture output dimensions unsupported");
        value.client_pid = GetCurrentProcessId(); value.client_heartbeat = GetTickCount64();
        value.window = window; value.window_pid = pid; state->request = ++value.request;
        value.active = 1; value.status = Ipc::Loading; value.error[0] = '\0'; value.frame_serial = value.frame_bytes = 0;
        value.geometry={}; value.control_active=0; ++value.control_generation; value.input_read=value.input_write=0; value.input_error[0]='\0';
    }
    state_ = state;
    try {
        state->worker = std::thread([state] {
            uint64_t last = 0, received_at = GetTickCount64();
            try {
                while (!state->stop.load()) {
                    FrameData output; std::string error;
                    {
                        Ipc::Lock lock(*state->channel);
                        if (lock.acquired) {
                            auto& value = *state->channel->data;
                            if (!Ipc::Valid(value) || value.client_pid != GetCurrentProcessId() || value.request != state->request)
                                error = "Window capture request was replaced";
                            else if (GetTickCount64() - value.broker_heartbeat > 3000) error = "Window capture helper stopped";
                            else {
                                value.client_heartbeat = GetTickCount64();
                                if (value.status == Ipc::Failed) error.assign(value.error, strnlen_s(value.error, sizeof(value.error)));
                                else if (value.status == Ipc::Playing && value.frame_serial != last) {
                                    if (!Ipc::Dimensions(value) || value.frame_bytes != value.width * value.height * 4 || value.frame_bytes > Ipc::kMaxBytes)
                                        error = "Window capture frame is invalid";
                                    else { output.serial = value.frame_serial; output.geometry=value.geometry; output.rgba.assign(value.pixels, value.pixels + value.frame_bytes); }
                                }
                            }
                        }
                    }
                    if (!error.empty()) { state->Fail(error); break; }
                    if (output.serial) {
                        last = output.serial; received_at = GetTickCount64(); std::lock_guard lock(state->mutex); state->frame = std::move(output);
                    }
                    if (GetTickCount64() - received_at > 15000) { state->Fail("Window capture received no frames for 15 seconds"); break; }
                    std::unique_lock lock(state->mutex);
                    state->wake.wait_for(lock, std::chrono::milliseconds(3), [&] { return state->stop.load(); });
                }
            } catch (const std::exception& error) { state->Fail(error.what()); }
        });
    } catch (...) { Stop(); throw; }
}
}
