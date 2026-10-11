#include "window_capture.h"
#include "capture_ipc.h"
#include "window_input.h"
#include <shellapi.h>
#include <chrono>
#include <thread>
#include <winrt/base.h>

using namespace BetterEndfieldNext::MediaScreen;
int WINAPI wWinMain(HINSTANCE, HINSTANCE, PWSTR, int) {
    SetThreadDpiAwarenessContext(DPI_AWARENESS_CONTEXT_PER_MONITOR_AWARE_V2);
    HANDLE instance = CreateMutexW(nullptr, TRUE, Ipc::kInstance);
    if (!instance) return 1;
    if (GetLastError() == ERROR_ALREADY_EXISTS) { CloseHandle(instance); return 0; }
    HANDLE owner = nullptr;
    int argc = 0; auto argv = CommandLineToArgvW(GetCommandLineW(), &argc);
    if (argv && argc == 3 && std::wstring(argv[1]) == L"--owner") {
        try { owner = OpenProcess(SYNCHRONIZE, FALSE, std::stoul(argv[2])); } catch (...) {}
    }
    if (argv) LocalFree(argv);
    if (!owner) { CloseHandle(instance); return 2; }
    int result = 0;
    bool apartment_initialized = false;
    try {
        // Keep the MTA alive between sessions: cached WinRT factories become
        // invalid if the last capture worker uninitializes COM while idle.
        winrt::init_apartment(winrt::apartment_type::multi_threaded); apartment_initialized = true;
        Ipc::Channel channel(true);
        {
            Ipc::Lock lock(channel);
            if (!lock.acquired) throw std::runtime_error("Capture channel locked");
            std::memset(channel.data, 0, offsetof(Ipc::Shared, pixels));
            channel.data->magic = Ipc::kMagic; channel.data->version = Ipc::kVersion;
            channel.data->broker_pid = GetCurrentProcessId(); channel.data->broker_heartbeat = GetTickCount64();
        }
        WindowCapture capture;
        WindowInput input;
        uint64_t control_generation=0;
        bool controlling=false;
        uint64_t request = 0, last = 0;
        bool capturing = false;
        for (;;) {
            struct Request { uint64_t id, window; uint32_t pid, width, height, fps; bool active; } pending{};
            uint64_t generation=0; bool control=false; CaptureGeometry geometry{}; std::vector<WindowInputEvent> events;
            {
                Ipc::Lock lock(channel);
                if (lock.acquired) {
                    auto& value = *channel.data; value.broker_heartbeat = GetTickCount64();
                    if (value.active && GetTickCount64() - value.client_heartbeat > 3000) {
                        Ipc::Error(value, "Window capture client stopped"); value.active = 0;
                    }
                    pending = {value.request, value.window, value.window_pid, value.width, value.height, value.fps, value.active != 0};
                    if (pending.active && !Ipc::Dimensions(value)) {
                        Ipc::Error(value, "Capture output dimensions unsupported"); value.active = 0; pending.active = false;
                    }
                    DWORD foreground_pid=0; GetWindowThreadProcessId(GetForegroundWindow(),&foreground_pid);
                    if (value.control_active && foreground_pid!=value.client_pid) Ipc::InputError(value,"Game lost foreground focus");
                    if (value.control_active && value.window_pid==value.client_pid) Ipc::InputError(value,"Projection game cannot also be its own input source");
                    generation=value.control_generation; control=pending.active && value.control_active && value.status==Ipc::Playing;
                    geometry=value.geometry;
                    if (value.input_write<value.input_read || value.input_write-value.input_read>Ipc::kInputCapacity) {
                        Ipc::InputError(value,"Invalid window input queue"); control=false;
                    } else while (value.input_read<value.input_write) events.push_back(value.input[value.input_read++ % Ipc::kInputCapacity]);
                } else { std::this_thread::sleep_for(std::chrono::milliseconds(10)); continue; }
            }
            if (!control || generation!=control_generation || pending.id!=request) {
                input.Release(); controlling=false;
            }
            if (control && !controlling) {
                controlling=input.Bind(pending.window,pending.pid); control_generation=generation;
            }
            if(controlling&&!input.Heartbeat()){input.Release();controlling=false;}
            if (controlling) for (const auto& event:events) if (!input.Send(event,geometry)) { input.Release(); controlling=false; break; }
            if (control && !controlling) {
                Ipc::Lock lock(channel);
                if (lock.acquired && channel.data->request==pending.id && channel.data->control_generation==generation)
                    Ipc::InputError(*channel.data,input.Error());
            }
            if (!pending.active) {
                if (capturing) { capture.Stop(); capturing = false; }
                if (WaitForSingleObject(owner, 0) == WAIT_OBJECT_0) break;
            } else {
                if (request != pending.id) {
                    capture.Stop(); request = pending.id; last = 0; capturing = false;
                    try { capture.Start(pending.window, pending.pid, pending.width, pending.height, pending.fps); capturing = true; }
                    catch (const std::exception& error) {
                        Ipc::Lock lock(channel);
                        if (lock.acquired && channel.data->request == request) Ipc::Error(*channel.data, error.what());
                    }
                }
                if (capturing) {
                    auto error = capture.Error(); WindowCapture::FrameData frame;
                    const bool has_frame = error.empty() && capture.Take(last, frame);
                    if (!error.empty() || has_frame) {
                        Ipc::Lock lock(channel);
                        if (lock.acquired && channel.data->active && channel.data->request == request) {
                            auto& value = *channel.data;
                            if (!error.empty()) Ipc::Error(value, error);
                            else if (frame.rgba.size() == size_t(pending.width) * pending.height * 4 && frame.rgba.size() <= Ipc::kMaxBytes) {
                                std::memcpy(value.pixels, frame.rgba.data(), frame.rgba.size()); value.frame_bytes = static_cast<uint32_t>(frame.rgba.size());
                                value.frame_serial = last = frame.serial; value.status = Ipc::Playing;
                                value.geometry=frame.geometry;
                            } else Ipc::Error(value, "Window capture frame is invalid");
                        }
                    }
                }
            }
            std::this_thread::sleep_for(std::chrono::milliseconds(pending.active ? 3 : 10));
        }
        { Ipc::Lock lock(channel); if (lock.acquired) { channel.data->broker_pid = 0; channel.data->broker_heartbeat = 0; } }
    } catch (...) { result = 1; }
    if (apartment_initialized) { winrt::clear_factory_cache(); winrt::uninit_apartment(); }
    CloseHandle(owner); ReleaseMutex(instance); CloseHandle(instance); return result;
}
