#pragma once

#include <Windows.h>

#include <cstddef>
#include <cstdint>
#include <string>

namespace BetterEndfield::CombatOverlayProtocol {

inline constexpr uint32_t kMagic = 0x53434542; // "BECS"
inline constexpr uint32_t kVersion = 7;
inline constexpr uint32_t kDamageCategoryCount = 6;
inline constexpr uint32_t kRdpsContributionCount = 10;
inline constexpr uint32_t kDisplaySegmentCount = 16;
inline constexpr uint32_t kMaxCharacters = 16;
inline constexpr size_t kCharacterIdCapacity = 64;
inline constexpr size_t kCategoryNameCapacity = 32;
inline constexpr size_t kActionIdCapacity = 96;

#pragma pack(push, 8)
struct CategoryDescriptor {
    char name[kCategoryNameCapacity]{};
    uint32_t color_argb = 0;
};

struct CharacterSnapshot {
    char id[kCharacterIdCapacity]{};
    double total_damage = 0.0;
    // DPS uses the first active_category_count (or default kDamageCategoryCount) entries;
    // rDPS uses active_category_count (or default kRdpsContributionCount) entries.
    double damage_by_category[kDisplaySegmentCount]{};
    uint64_t hits = 0;
    uint64_t critical_hits = 0;
    double dps = 0.0;
    char current_action[kActionIdCapacity]{};
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
    // 0 = direct DPS, 1 = raid-contribution DPS (rDPS).
    uint32_t metric_mode = 0;
    uint64_t session_id = 0;
    double duration_seconds = 0.0;
    double total_damage = 0.0;
    double dps = 0.0;
    double rdps = 0.0;
    uint64_t hit_count = 0;
    uint64_t critical_count = 0;
    uint32_t active_category_count = 0;
    CategoryDescriptor categories[kDisplaySegmentCount]{};
    CharacterSnapshot characters[kMaxCharacters]{};
};
#pragma pack(pop)

inline std::wstring MappingName(DWORD game_pid) {
    return L"Local\\BetterEndfield.CombatStats." + std::to_wstring(game_pid);
}

} // namespace BetterEndfield::CombatOverlayProtocol
