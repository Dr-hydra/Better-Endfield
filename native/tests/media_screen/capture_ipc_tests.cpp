#include "../../modules/media_screen/window_capture.h"
#include "../../modules/media_screen/capture_ipc.h"
#include <chrono>
#include <iostream>
#include <thread>
using namespace BetterEndfieldNext::MediaScreen;
namespace {
void Check(bool value, const char* text) { if (!value) throw std::runtime_error(text); }
int Source(DWORD parent_pid) {
    HANDLE parent = OpenProcess(SYNCHRONIZE, FALSE, parent_pid);
    if (!parent) return 2;
    auto title = L"BE.MediaCapture.Test.Source." + std::to_wstring(parent_pid);
    HWND hwnd = CreateWindowW(L"STATIC", title.c_str(), WS_POPUP, 0, 0, 64, 64, nullptr, nullptr, GetModuleHandleW(nullptr), nullptr);
    if (!hwnd) { CloseHandle(parent); return 2; }
    while (IsWindow(hwnd) && WaitForSingleObject(parent, 0) != WAIT_OBJECT_0) {
        MSG message{}; while (PeekMessageW(&message, nullptr, 0, 0, PM_REMOVE)) { TranslateMessage(&message); DispatchMessageW(&message); }
        std::this_thread::sleep_for(std::chrono::milliseconds(10));
    }
    if (IsWindow(hwnd)) DestroyWindow(hwnd);
    CloseHandle(parent); return 0;
}
struct SourceProcess {
    PROCESS_INFORMATION process{};
    HWND hwnd = nullptr;
    SourceProcess() {
        wchar_t executable[32768]{}; GetModuleFileNameW(nullptr, executable, 32768);
        auto command = L"\"" + std::wstring(executable) + L"\" --source " + std::to_wstring(GetCurrentProcessId());
        STARTUPINFOW startup{}; startup.cb = sizeof(startup);
        Check(CreateProcessW(executable, command.data(), nullptr, nullptr, FALSE, CREATE_NO_WINDOW, nullptr, nullptr, &startup, &process), "source fixture launch failed");
        const auto title = L"BE.MediaCapture.Test.Source." + std::to_wstring(GetCurrentProcessId());
        for (int i = 0; i < 200 && !hwnd; ++i) { hwnd = FindWindowW(L"STATIC", title.c_str()); std::this_thread::sleep_for(std::chrono::milliseconds(10)); }
    }
    ~SourceProcess() {
        if (hwnd) PostMessageW(hwnd, WM_CLOSE, 0, 0);
        if (process.hProcess && WaitForSingleObject(process.hProcess, 2000) == WAIT_TIMEOUT) TerminateProcess(process.hProcess, 2);
        if (process.hThread) CloseHandle(process.hThread);
        if (process.hProcess) CloseHandle(process.hProcess);
    }
};
template<class Fn> void Edit(Ipc::Channel& channel, Fn fn) { Ipc::Lock lock(channel); Check(lock.acquired, "fixture lock failed"); fn(*channel.data); }
void AwaitError(WindowCapture& capture, const char* expected) {
    for (int i = 0; i < 100 && capture.Error().empty(); ++i) std::this_thread::sleep_for(std::chrono::milliseconds(5));
    Check(capture.Error().find(expected) != std::string::npos, "IPC failure did not reach capture client");
}
}
int main(int argc, char** argv) {
    if (argc == 3 && std::string(argv[1]) == "--source") return Source(static_cast<DWORD>(std::stoul(argv[2])));
    try {
        SourceProcess source; Check(source.hwnd != nullptr, "source fixture window missing");
        Ipc::Channel channel(true);
        Edit(channel, [](auto& value) {
            std::memset(&value, 0, offsetof(Ipc::Shared, pixels)); value.magic = Ipc::kMagic; value.version = Ipc::kVersion;
            value.broker_pid = GetCurrentProcessId(); value.broker_heartbeat = GetTickCount64();
        });
        WindowCapture capture;
        Check(!channel.data->active, "idle helper started capturing automatically");
        auto start = [&] { capture.Start(reinterpret_cast<uintptr_t>(source.hwnd), source.process.dwProcessId, 64, 64, 5); };
        start(); uint64_t first_request = 0;
        Edit(channel, [&](auto& value) {
            Check(value.active && value.client_pid == GetCurrentProcessId(), "explicit capture did not claim helper"); first_request = value.request;
            std::memset(value.pixels, 37, 64 * 64 * 4); value.frame_serial = 1; value.frame_bytes = 64 * 64 * 4; value.status = Ipc::Playing;
        });
        WindowCapture::FrameData frame;
        for (int i = 0; i < 100 && !capture.Take(0, frame); ++i) std::this_thread::sleep_for(std::chrono::milliseconds(5));
        Check(frame.serial == 1 && frame.rgba.size() == 64 * 64 * 4 && frame.rgba[0] == 37, "shared frame was not delivered intact");
        Check(!capture.Take(frame.serial, frame), "same shared frame was delivered twice");
        Check(!capture.Input({WM_KEYDOWN,'A',0,0,0}), "input sent without explicit control mode");
        Check(capture.Control(true), "explicit control mode could not claim session");
        auto generation=channel.data->control_generation;
        Check(capture.Input({WM_KEYDOWN,'A',0,0,0}) && channel.data->input_write==1 && channel.data->input[0].key=='A', "directed key missing from queue");
        Check(capture.Control(false) && !channel.data->control_active && channel.data->control_generation>generation && !channel.data->input_write,
            "control exit retained queued input or generation");
        Check(capture.Control(true) && channel.data->control_active==1, "control re-entry rejected");
        for (size_t i=0;i<Ipc::kInputCapacity;++i) Check(capture.Input({WM_KEYDOWN,'A',0,0,0}), "valid input capacity rejected");
        Check(!capture.Input({WM_KEYDOWN,'A',0,0,0}) && !channel.data->control_active && !capture.InputError().empty(), "queue overflow retained control or hid error");
        capture.Stop(); Check(!channel.data->active && !channel.data->client_pid, "stop retained helper ownership");
        capture.Start(reinterpret_cast<uintptr_t>(source.hwnd), source.process.dwProcessId, 3840, 2160, 120);
        Edit(channel, [](auto& value) {
            Check(Ipc::Dimensions(value), "4K/120 request rejected");
            std::memset(value.pixels, 91, Ipc::kMaxBytes);
            value.frame_serial = 1; value.frame_bytes = Ipc::kMaxBytes; value.status = Ipc::Playing;
        });
        frame = {};
        for (int i = 0; i < 100 && !capture.Take(0, frame); ++i) std::this_thread::sleep_for(std::chrono::milliseconds(5));
        Check(frame.serial == 1 && frame.rgba.size() == Ipc::kMaxBytes && frame.rgba.front() == 91 && frame.rgba.back() == 91,
            "maximum 4K frame was truncated or not delivered");
        capture.Stop();
        start(); Check(channel.data->request > first_request && !channel.data->frame_serial, "restart retained previous frame generation");
        Edit(channel, [](auto& value) { value.status = Ipc::Playing; value.frame_serial = 1; value.frame_bytes = Ipc::kMaxBytes + 1; });
        AwaitError(capture, "invalid"); capture.Stop();
        start(); Edit(channel, [](auto& value) { Ipc::Error(value, "capture permission denied fixture"); });
        AwaitError(capture, "permission denied"); capture.Stop();
        start(); Edit(channel, [](auto& value) { ++value.request; });
        AwaitError(capture, "replaced"); capture.Stop();
        // Stop must not cancel a successor request that replaced this session.
        Check(channel.data->active, "old client cancelled a successor request");
        Edit(channel, [&](auto& value) { value.client_pid = source.process.dwProcessId; value.client_heartbeat = GetTickCount64(); });
        bool rejected = false; try { start(); } catch (const std::exception&) { rejected = true; }
        Check(rejected && channel.data->client_pid == source.process.dwProcessId, "another live client was overwritten");
        Edit(channel, [](auto& value) { value.active = 0; value.client_pid = 0; });
        start(); Edit(channel, [](auto& value) { value.broker_heartbeat = 0; });
        AwaitError(capture, "helper stopped"); capture.Stop();
        rejected = false; try { start(); } catch (const std::exception&) { rejected = true; }
        Check(rejected && !channel.data->active, "stale helper accepted a new request");
        std::cout << "PASS capture IPC: explicit start, intact frames, restart, malformed frames, errors, replacement, owner isolation, helper loss\n";
        return 0;
    } catch (const std::exception& error) { std::cerr << "FAIL " << error.what() << '\n'; return 1; }
}
