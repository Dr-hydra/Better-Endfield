#pragma once

#include <cstddef>
#include <cstdint>
#include <string_view>

namespace BetterEndfield::CombatStats::DamageCategory {

inline constexpr size_t kBasicAttack = 0;
inline constexpr size_t kSkill = 1;
inline constexpr size_t kUltimate = 2;
inline constexpr size_t kCombo = 3;
inline constexpr size_t kPassive = 4;
inline constexpr size_t kOther = 5;

// DamageDecorateMask stores one bit per non-None DamageDecorateType value.
// These four bits are present on the recorded Modifier and are more reliable
// than broad skill-id substring matching for ordinary character attacks.
inline constexpr uint64_t kNormalAttackMask = uint64_t{1} << 7;
inline constexpr uint64_t kNormalSkillMask = uint64_t{1} << 8;
inline constexpr uint64_t kUltimateSkillMask = uint64_t{1} << 9;
inline constexpr uint64_t kComboSkillMask = uint64_t{1} << 13;
inline constexpr uint64_t kTalentDamageMask = uint64_t{1} << 29;

constexpr bool Contains(std::string_view value, std::string_view token) noexcept {
    return value.find(token) != std::string_view::npos;
}

constexpr bool EndsWith(std::string_view value, std::string_view suffix) noexcept {
    return value.size() >= suffix.size() &&
        value.substr(value.size() - suffix.size()) == suffix;
}

constexpr size_t Classify(std::string_view skill, uint64_t decorate_mask) noexcept {
    // Transformed attacks may deliberately keep NormalAttack in the runtime
    // mask. Their origin ids retain the owning skill group, so handle these
    // explicit forms before consulting the mask.
    if (Contains(skill, "_combo_skill")) return kCombo;
    if (Contains(skill, "_ultimate") ||
        (Contains(skill, "_attack") && EndsWith(skill, "_ult"))) {
        return kUltimate;
    }
    if (Contains(skill, "_talent") || Contains(skill, "passive")) return kPassive;

    if (decorate_mask & kComboSkillMask) return kCombo;
    if (decorate_mask & kUltimateSkillMask) return kUltimate;
    if (decorate_mask & kNormalSkillMask) return kSkill;
    if (decorate_mask & kNormalAttackMask) return kBasicAttack;
    if (decorate_mask & kTalentDamageMask) return kPassive;

    // Older clients and system-generated damage do not always set a decorate
    // bit. Keep the stable runtime-id conventions as a compatibility fallback.
    if (Contains(skill, "_normal_skill")) return kSkill;
    if (Contains(skill, "_attack") || Contains(skill, "_power_attack") ||
        Contains(skill, "_plunging_attack")) {
        return kBasicAttack;
    }
    return kOther;
}

static_assert(Classify("chr_test_attack1", kNormalAttackMask) == kBasicAttack);
static_assert(Classify("chr_test_normal_skill", kNormalSkillMask) == kSkill);
static_assert(Classify("chr_test_ultimate_skill", kUltimateSkillMask) == kUltimate);
static_assert(Classify("chr_test_combo_skill", kComboSkillMask) == kCombo);
static_assert(Classify("chr_test_attack1_ult", kNormalAttackMask) == kUltimate);
static_assert(Classify("chr_test_combo_skill_ult", kNormalAttackMask) == kCombo);
static_assert(Classify("chr_test_normal_skill_ult", kNormalSkillMask) == kSkill);
static_assert(Classify("chr_test_talent_1", kTalentDamageMask) == kPassive);
static_assert(Classify("opaque_skill", 0) == kOther);

} // namespace BetterEndfield::CombatStats::DamageCategory
