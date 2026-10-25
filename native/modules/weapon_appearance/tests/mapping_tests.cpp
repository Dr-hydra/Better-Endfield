#include "../mapping.h"

#include <cstdio>
#include <cstdlib>
#include <string_view>

using namespace BetterEndfieldNext::WeaponAppearance;

#define CHECK(expression)                                                      \
  do {                                                                         \
    if (!(expression)) {                                                       \
      std::fprintf(stderr, "line %d: %s\n", __LINE__, #expression);            \
      std::exit(1);                                                            \
    }                                                                          \
  } while (false)

int main() {
  // The delivered object is a GameObject whose name is the prefab stem.
  CHECK(IsWeaponRoot("wpn_sword_0021"));
  CHECK(IsWeaponRoot("WPN_SWORD_0021"));
  CHECK(IsWeaponRoot("wpn_sword_0021.prefab"));
  CHECK(IsWeaponRoot("assets/beyond/dynamicassets/gameplay/prefabs/weapons/"
                     "wpn_sword_0021.prefab"));
  CHECK(
      IsWeaponRoot("assets\\beyond\\dynamicassets\\gameplay\\prefabs\\weapons\\"
                   "wpn_sword_0021"));
  CHECK(!IsWeaponRoot("wpn_sword_0021_ink"));
  CHECK(!IsWeaponRoot("wpn_sword_0021_refined"));
  CHECK(!IsWeaponRoot("wpn_sword_0021_01"));
  CHECK(!IsWeaponRoot(""));
  CHECK(!IsWeaponRoot(".prefab"));

  // Character root identification (both masked and unmasked postmodel stems).
  CHECK(IsCharacterFemaleRoot("chr_0003_endminf_postmodel"));
  CHECK(IsCharacterFemaleRoot("CHR_0003_ENDMINF_POSTMODEL"));
  CHECK(IsCharacterFemaleRoot("chr_0003_endminf_postmodel.prefab"));
  CHECK(IsCharacterFemaleRoot("actors/postmodels/characters/"
                              "chr_0003_endminf_postmodel.prefab"));
  CHECK(IsCharacterFemaleRoot("chr_0003_endminfunmasked_postmodel"));
  CHECK(!IsCharacterFemaleRoot("chr_0003_endminfink_postmodel"));
  CHECK(!IsCharacterFemaleRoot("chr_0002_endminm_postmodel"));
  CHECK(IsCharacterFemaleUiRoot("chr_0003_endminf_uimodel"));
  CHECK(IsCharacterFemaleUiRoot("CHR_0003_ENDMINF_UIMODEL.prefab"));
  CHECK(!IsCharacterFemaleUiRoot("chr_0002_endminm_uimodel"));

  CHECK(IsCharacterMaleRoot("chr_0002_endminm_postmodel"));
  CHECK(IsCharacterMaleRoot("CHR_0002_ENDMINM_POSTMODEL"));
  CHECK(IsCharacterMaleRoot("chr_0002_endminm_postmodel.prefab"));
  CHECK(IsCharacterMaleRoot("chr_0002_endminmunmasked_postmodel"));
  CHECK(!IsCharacterMaleRoot("chr_0002_endminmink_postmodel"));
  CHECK(!IsCharacterMaleRoot("chr_0003_endminf_postmodel"));
  CHECK(IsCharacterMaleUiRoot("chr_0002_endminm_uimodel"));
  CHECK(IsCharacterMaleUiRoot("CHR_0002_ENDMINM_UIMODEL.prefab"));
  CHECK(!IsCharacterMaleUiRoot("chr_0003_endminf_uimodel"));

  // Every audited pair must resolve, case-insensitively and through paths.
  for (const auto &pair : kEffectPairs) {
    const EffectPair *found = FindEffectPair(pair.original);
    CHECK(found == &pair);
    CHECK(found->ink == pair.ink);
  }
  CHECK(FindEffectPair("p_endminf_attack_01_start")->ink ==
        "p_fxbat_endminf_skin_ink_attack_01_start");
  CHECK(FindEffectPair("P_ENDMINF_ATTACK_01_START") != nullptr);
  CHECK(FindEffectPair("effects/prefabs/P_Endminf_Attack_01_Start.prefab") !=
        nullptr);
  CHECK(FindEffectPair("p_endminf_combo_skill_01_trail")->ink == kTrailInk);
  CHECK(FindEffectPair("p_endminf_unknown_effect") == nullptr);
  CHECK(FindEffectPair("") == nullptr);
  CHECK(FindEffectPair("p_endminf_attack_01_start_extra") == nullptr);

  // Original names must be unique: a duplicate would be shadowed by the
  // first match. Ink targets may legitimately be shared, because the skin
  // authors alias differently-prefixed source names onto one prefab.
  for (size_t left = 0; left < sizeof(kEffectPairs) / sizeof(kEffectPairs[0]);
       ++left) {
    for (size_t right = left + 1;
         right < sizeof(kEffectPairs) / sizeof(kEffectPairs[0]); ++right) {
      CHECK(kEffectPairs[left].original != kEffectPairs[right].original);
    }
  }
  CHECK(FindEffectPair("p_endminf_attack_04_start")->ink ==
        FindEffectPair("p_fxbat_endminf_attack_04_start")->ink);
  CHECK(FindEffectPair("p_endminf_attack_05_hit")->ink ==
        FindEffectPair("p_fxbat_endminf_attack_05_hit")->ink);

  std::puts("weapon appearance mapping tests passed");
  return 0;
}