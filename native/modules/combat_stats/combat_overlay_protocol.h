#pragma once

#include <Windows.h>

#include <cstddef>
#include <cstdint>
#include <string>

namespace BetterEndfield::CombatOverlayProtocol {

inline constexpr uint32_t kMagic = 0x53434542; // "BECS"
inline constexpr uint32_t kVersion = 2;
inline constexpr uint32_t kDamageCategoryCount = 6;
inline constexpr uint32_t kMaxCharacters = 16;
inline constexpr size_t kCharacterIdCapacity = 64;

#pragma pack(push, 8)
struct CharacterSnapshot {
    char id[kCharacterIdCapacity]{};
    double total_damage = 0.0;
    double damage_by_category[kDamageCategoryCount]{};
    uint64_t hits = 0;
    uint64_t critical_hits = 0;
};

struct SharedSnapshot {
    uint32_t magic = kMagic;
    uint32_t version = kVersion;
    uint32_t structure_size = 0;
    volatile LONG sequence = 0;
    uint32_t game_pid = 0;
    uint32_t overlay_enabled = 0;
    uint32_t overlay_visible = 1;
    uint32_t session_active = 0;
    uint32_t shutdown_requested = 0;
    uint32_t character_count = 0;
    uint32_t reserved = 0;
    uint64_t session_id = 0;
    double duration_seconds = 0.0;
    double total_damage = 0.0;
    double dps = 0.0;
    uint64_t hit_count = 0;
    uint64_t critical_count = 0;
    CharacterSnapshot characters[kMaxCharacters]{};
};
#pragma pack(pop)

inline std::wstring MappingName(DWORD game_pid) {
    return L"Local\\BetterEndfield.CombatStats." + std::to_wstring(game_pid);
}

} // namespace BetterEndfield::CombatOverlayProtocol
