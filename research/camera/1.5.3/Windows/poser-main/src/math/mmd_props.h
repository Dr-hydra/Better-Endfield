#pragma once
#include <cctype>
#include <string>

namespace mmd {
// Match dedicated prop/effect containers and meshes, never generic attachment
// slots, a hand, or a piece of clothing. The entire walk is scoped to the
// actor.
inline bool IsPlaybackPropNode(std::string name) {
  for (char &c : name)
    c = char(std::tolower(static_cast<unsigned char>(c)));
  for (const char *prefix : {"propbone_", "prop_", "wep_", "weapon_", "wpn_"})
    if (name.rfind(prefix, 0) == 0)
      return true;
  return name.find("_vfxpart_") != std::string::npos;
}
} // namespace mmd
