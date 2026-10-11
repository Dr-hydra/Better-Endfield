#pragma once
#include <Windows.h>
#include <cstdint>
#include <cstring>
#include <stdexcept>
#include <string>
#include "output_limits.h"
#include "window_capture.h"

namespace BetterEndfieldNext::MediaScreen::Ipc {
#ifdef BE_MEDIA_CAPTURE_IPC_TEST
#define BE_MEDIA_CAPTURE_PREFIX L"Local\\BetterEndfieldNext.MediaCapture.Test.V3"
#else
#define BE_MEDIA_CAPTURE_PREFIX L"Local\\BetterEndfieldNext.MediaCapture.V3"
#endif
inline constexpr wchar_t kMapping[] = BE_MEDIA_CAPTURE_PREFIX;
inline constexpr wchar_t kMutex[] = BE_MEDIA_CAPTURE_PREFIX L".Lock";
inline constexpr wchar_t kInstance[] = BE_MEDIA_CAPTURE_PREFIX L".Instance";
#undef BE_MEDIA_CAPTURE_PREFIX
inline constexpr uint32_t kMagic = 0x42454D43, kVersion = 3;
inline constexpr size_t kInputCapacity = 256;
inline constexpr uint32_t kMaxBytes = kMaxOutputPixels * 4;
enum Status : uint32_t { Idle, Loading, Playing, Failed };
struct Shared {
    uint32_t magic, version, broker_pid, client_pid;
    uint64_t broker_heartbeat, client_heartbeat, request, window, frame_serial;
    uint32_t active, window_pid, width, height, fps, status, frame_bytes;
    CaptureGeometry geometry;
    uint64_t control_generation, input_read, input_write;
    uint32_t control_active;
    char input_error[512];
    WindowInputEvent input[kInputCapacity];
    char error[512];
    unsigned char pixels[kMaxBytes];
};
class Channel {
    HANDLE mapping_ = nullptr, mutex_ = nullptr;
public:
    Shared* data = nullptr;
    explicit Channel(bool create) {
        mutex_ = create ? CreateMutexW(nullptr, FALSE, kMutex) : OpenMutexW(SYNCHRONIZE | MUTEX_MODIFY_STATE, FALSE, kMutex);
        if (mutex_) mapping_ = create
            ? CreateFileMappingW(INVALID_HANDLE_VALUE, nullptr, PAGE_READWRITE, 0, sizeof(Shared), kMapping)
            : OpenFileMappingW(FILE_MAP_ALL_ACCESS, FALSE, kMapping);
        if (mapping_) data = static_cast<Shared*>(MapViewOfFile(mapping_, FILE_MAP_ALL_ACCESS, 0, 0, sizeof(Shared)));
        if (!data) {
            const auto code = GetLastError(); Close();
            throw std::runtime_error("Window capture helper unavailable (Win32 " + std::to_string(code) + "). Open the Better Endfield controller.");
        }
    }
    Channel(const Channel&) = delete;
    Channel& operator=(const Channel&) = delete;
    ~Channel() { Close(); }
    void Close() {
        if (data) UnmapViewOfFile(data);
        if (mapping_) CloseHandle(mapping_);
        if (mutex_) CloseHandle(mutex_);
        data = nullptr; mapping_ = mutex_ = nullptr;
    }
    bool Lock(DWORD timeout = 100) {
        const auto result = WaitForSingleObject(mutex_, timeout);
        return result == WAIT_OBJECT_0 || result == WAIT_ABANDONED;
    }
    void Unlock() { ReleaseMutex(mutex_); }
};
class Lock {
    Channel& channel_;
public:
    bool acquired;
    explicit Lock(Channel& channel, DWORD timeout = 100) : channel_(channel), acquired(channel.Lock(timeout)) {}
    ~Lock() { if (acquired) channel_.Unlock(); }
};
inline bool Valid(const Shared& value) { return value.magic == kMagic && value.version == kVersion; }
inline bool Dimensions(const Shared& value) {
    return ValidOutput(value.width, value.height, value.fps);
}
inline void Error(Shared& value, const std::string& error) {
    value.status = Failed; value.frame_bytes = 0;
    const auto size = (error.size() < sizeof(value.error) - 1) ? error.size() : sizeof(value.error) - 1;
    std::memcpy(value.error, error.data(), size); value.error[size] = '\0';
}
inline void InputError(Shared& value, const std::string& error) {
    value.control_active = 0;
    const auto size = std::min(error.size(), sizeof(value.input_error)-1);
    std::memcpy(value.input_error, error.data(), size); value.input_error[size]='\0';
}
}
