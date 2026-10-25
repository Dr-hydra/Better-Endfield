#pragma once

// Pure name mapping for the Hongyuan ink appearance override. Free of
// Windows/IL2CPP dependencies so the table is unit testable directly.
//
// Every pair below was verified against the installed Windows 1.6.3 manifest
// (snapshot 2954fa80-23c1-1579-2b22-4ecfd6d70418): both the original and the
// ink name must exist as real effect prefabs. Do not add speculative entries.

#include <cstddef>
#include <string_view>

namespace BetterEndfieldNext::WeaponAppearance {

// Weapon resource roots. The ink prefab is the game's own authored Hongyuan
// ink appearance: identical meshes and material slots, one ink base map, and
// three extra ink renderers parented under P_fxbat_endminf_skin_ink_wep_r.
// Redirecting the request lets the game load it through its normal path, so
// nothing has to be cloned, rebuilt or re-uploaded by this module.
inline constexpr std::string_view kWeaponRoot = "wpn_sword_0021";
inline constexpr std::string_view kWeaponInkRoot = "wpn_sword_0021_ink";

// wpn_sword_0021_refined has no ink counterpart in the current manifest. It is
// deliberately not remapped: an unknown target must fail closed rather than
// point at a resource that does not exist.
inline constexpr std::string_view kWeaponRefinedRoot = "wpn_sword_0021_refined";

// Character postmodel roots. Female administrator (chr_0003_endminf) and male
// administrator (chr_0002_endminm) both have official authored ink postmodel
// prefabs in the game package.
inline constexpr std::string_view kCharacterFemaleRoot =
    "chr_0003_endminf_postmodel";
inline constexpr std::string_view kCharacterFemaleUnmaskedRoot =
    "chr_0003_endminfunmasked_postmodel";
inline constexpr std::string_view kCharacterFemaleInkRoot =
    "chr_0003_endminfink_postmodel";
inline constexpr std::string_view kCharacterFemaleUiRoot =
    "chr_0003_endminf_uimodel";

inline constexpr std::string_view kCharacterMaleRoot =
    "chr_0002_endminm_postmodel";
inline constexpr std::string_view kCharacterMaleUnmaskedRoot =
    "chr_0002_endminmunmasked_postmodel";
inline constexpr std::string_view kCharacterMaleInkRoot =
    "chr_0002_endminmink_postmodel";
inline constexpr std::string_view kCharacterMaleUiRoot =
    "chr_0002_endminm_uimodel";

struct EffectPair {
  std::string_view original;
  std::string_view ink;
};

// Female administrator (chr_0003_endminf) ink attack effects. The ink skin is
// the complete set; the male variant only ships a subset, so this table is
// intentionally endminf-only.
inline constexpr EffectPair kEffectPairs[]{
    {"p_fxbat_endminf_attack_03_start_03",
     "p_fxbat_endminf_skin_ink_attack_03_start_03"},
    {"p_fxbat_endminf_attack_03_crack",
     "p_fxbat_endminf_skin_ink_attack_03_crack"},
    {"p_fxbat_endminf_attack_04_start",
     "p_fxbat_endminf_skin_ink_attack_04_start"},
    {"p_fxbat_endminf_attack_04_start_03",
     "p_fxbat_endminf_skin_ink_attack_04_start_03"},
    {"p_fxbat_endminf_attack_04_start_04",
     "p_fxbat_endminf_skin_ink_attack_04_start_04"},
    {"p_fxbat_endminf_attack_05_start_02",
     "p_fxbat_endminf_skin_ink_attack_05_start_02"},
    {"p_fxbat_endminf_attack_05_hit", "p_fxbat_endminf_skin_ink_attack_05_hit"},
    {"p_fxbat_endminf_common_hit_02", "p_fxbat_endminf_skin_ink_common_hit_02"},
    {"p_fxbat_endminf_common_hit_03", "p_fxbat_endminf_skin_ink_common_hit_03"},
    {"p_fxgp_endminf_bash", "p_fxgp_endminf_skin_ink_bash"},
    {"p_endminf_attack_01_start", "p_fxbat_endminf_skin_ink_attack_01_start"},
    {"p_endminf_attack_02_start", "p_fxbat_endminf_skin_ink_attack_02_start"},
    {"p_endminf_attack_03_start", "p_fxbat_endminf_skin_ink_attack_03_start"},
    {"p_endminf_attack_03_start_02",
     "p_fxbat_endminf_skin_ink_attack_03_start_02"},
    {"p_endminf_attack_04_start", "p_fxbat_endminf_skin_ink_attack_04_start"},
    {"p_endminf_attack_04_start_03",
     "p_fxbat_endminf_skin_ink_attack_04_start_03"},
    {"p_endminf_attack_04_start_04",
     "p_fxbat_endminf_skin_ink_attack_04_start_04"},
    {"p_endminf_attack_05_start_02",
     "p_fxbat_endminf_skin_ink_attack_05_start_02"},
    {"p_endminf_attack_05_hit", "p_fxbat_endminf_skin_ink_attack_05_hit"},
    {"p_endminf_air_atk_start_01", "p_fxbat_endminf_skin_ink_air_atk_start_01"},
    {"p_endminf_air_atk_start_atk",
     "p_fxbat_endminf_skin_ink_air_atk_start_atk"},
    {"p_endminf_combo_skill_01_trail", "p_fxbat_endminf_skin_ink_attack_trail"},
    {"p_endminf_attack_trail", "p_fxbat_endminf_skin_ink_attack_trail"},
    {"p_fxbat_endminf_attack_trail", "p_fxbat_endminf_skin_ink_attack_trail"},
    {"p_endminf_trail", "p_fxbat_endminf_skin_ink_attack_trail"},
    {"p_endminf_attack_01_trail", "p_fxbat_endminf_skin_ink_attack_trail"},
    {"p_endminf_attack_02_trail", "p_fxbat_endminf_skin_ink_attack_trail"},
    {"p_endminf_attack_03_trail", "p_fxbat_endminf_skin_ink_attack_trail"},
    {"p_endminf_attack_04_trail", "p_fxbat_endminf_skin_ink_attack_trail"},
    {"p_endminf_attack_05_trail", "p_fxbat_endminf_skin_ink_attack_trail"},
};

inline constexpr std::string_view kTrailInk =
    "p_fxbat_endminf_skin_ink_attack_trail";

// Returns the file stem without directories or a trailing ".prefab".
std::string_view BaseName(std::string_view path);

// True when the delivered resource root is the base Hongyuan prefab.
bool IsWeaponRoot(std::string_view path);

// True when the delivered resource root is the female administrator postmodel.
bool IsCharacterFemaleRoot(std::string_view path);
bool IsCharacterFemaleUiRoot(std::string_view path);

// True when the delivered resource root is the male administrator postmodel.
bool IsCharacterMaleRoot(std::string_view path);
bool IsCharacterMaleUiRoot(std::string_view path);

// Returns the ink replacement for a known original effect name, or nullptr.
// Matching is case-insensitive and ignores directories and the ".prefab"
// suffix, because the game's effect names are authored inconsistently.
const EffectPair *FindEffectPair(std::string_view name);

} // namespace BetterEndfieldNext::WeaponAppearance