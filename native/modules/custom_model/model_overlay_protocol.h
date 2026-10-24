#pragma once
#if defined(_WIN32)
#include <Windows.h>
#include <cstdint>
#include <string>
namespace BetterEndfieldNext::CustomModel::OverlayProtocol {
inline constexpr uint32_t kMagic=0x4C444D42, kVersion=4;
inline constexpr size_t kPathCapacity=32768;
// Section 13 driver panel. The list is published by the game module and edited
// by the companion, so both sides must agree on the capacity.
inline constexpr uint32_t kDriverCapacity=16;
inline constexpr size_t kDriverIdCapacity=64, kDriverNameCapacity=128;
// Four-state control shared with the companion. Values match
// CustomModel::DriverState: 0 stopped, 1 playing, 2 paused, 3 finished.
// Paused and finished keep delivering the held pose; only stopped releases it.
struct DriverState {
    wchar_t id[kDriverIdCapacity]{};
    wchar_t name[kDriverNameCapacity]{};
    volatile LONG state=0;
    // 0 loop, 1 once. The panel only needs this to label a finished driver as
    // "replay" instead of "start".
    volatile LONG playback=0;
    // Speed is an integer so a peer never reads a torn float across processes.
    volatile LONG speed_millis=1000;
};
struct Shared {
    uint32_t magic=kMagic, version=kVersion, structure_size=sizeof(Shared), game_pid=0;
    volatile LONG shutdown_requested=0;
    volatile LONG runtime_hot_switch=0; // startup mode; companion cannot change hooks
    volatile LONG last_result=0; // 0 unknown, 1 accepted by existing pump, -1 rejected
    volatile LONG reserved=0;
    alignas(8) volatile LONG64 last_config_hash=0;
    wchar_t runtime_ini[kPathCapacity]{};
    wchar_t package_directory[kPathCapacity]{};
    // Package the driver list below belongs to. Only one driver set is live at a
    // time, so the panel attaches controls to the package that owns it.
    wchar_t driver_package[kPathCapacity]{};
    // Bumped by the module after it rewrote ids, names and defaults.
    volatile LONG driver_revision=0;
    // Bumped by the companion after it edited `state` or `speed_millis`.
    volatile LONG driver_control_revision=0;
    // Written last while publishing, so a reader that sees the new count also
    // sees the entries behind it.
    volatile LONG driver_count=0;
    DriverState drivers[kDriverCapacity]{};
    // Which vertex write path the desktop module uses for morph frames.
    // 0 = SetNativeArrayForChannelImpl (whole Position channel, default),
    // 1 = InternalSetVertexBufferData (byte range of the moved vertices only).
    // Both rebuild from the same captured base, so switching is a pure A/B and
    // never needs a model reload.
    volatile LONG vertex_upload_mode=0;
    // Bumped by the companion after it edited vertex_upload_mode.
    volatile LONG vertex_upload_revision=0;
};
inline std::wstring MappingName(DWORD pid) {return L"Local\\BetterEndfieldNext.ModelOverlay."+std::to_wstring(pid);}
inline uint64_t ConfigHash(std::string_view text) {
    uint64_t hash=14695981039346656037ull;
    for(const unsigned char c:text) {hash^=c;hash*=1099511628211ull;} return hash;
}
} // namespace BetterEndfieldNext::CustomModel::OverlayProtocol
#endif
