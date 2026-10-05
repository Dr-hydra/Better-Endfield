#pragma once
#if defined(_WIN32)
#include <Windows.h>
#include <cstdint>
#include <string>
namespace BetterEndfield::CustomModel::OverlayProtocol {
inline constexpr uint32_t kMagic=0x4C444D42, kVersion=1;
inline constexpr size_t kPathCapacity=32768;
struct Shared {
    uint32_t magic=kMagic, version=kVersion, structure_size=sizeof(Shared), game_pid=0;
    volatile LONG shutdown_requested=0;
    volatile LONG runtime_hot_switch=0; // startup mode; companion cannot change hooks
    volatile LONG last_result=0; // 0 unknown, 1 accepted by existing pump, -1 rejected
    volatile LONG reserved=0;
    alignas(8) volatile LONG64 last_config_hash=0;
    wchar_t runtime_ini[kPathCapacity]{};
    wchar_t package_directory[kPathCapacity]{};
};
inline std::wstring MappingName(DWORD pid) {return L"Local\\BetterEndfield.ModelOverlay."+std::to_wstring(pid);}
inline uint64_t ConfigHash(std::string_view text) {
    uint64_t hash=14695981039346656037ull;
    for(const unsigned char c:text) {hash^=c;hash*=1099511628211ull;} return hash;
}
} // namespace BetterEndfield::CustomModel::OverlayProtocol
#endif
