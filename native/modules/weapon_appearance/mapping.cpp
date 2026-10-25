#include "mapping.h"

#include <algorithm>
#include <cctype>
#include <string_view>

namespace BetterEndfieldNext::WeaponAppearance {
namespace {

bool EqualFold(std::string_view a, std::string_view b) {
  if (a.size() != b.size())
    return false;
  for (size_t i = 0; i < a.size(); ++i) {
    const auto ca = static_cast<unsigned char>(a[i]);
    const auto cb = static_cast<unsigned char>(b[i]);
    if (std::tolower(ca) != std::tolower(cb))
      return false;
  }
  return true;
}

} // namespace

std::string_view BaseName(std::string_view path) {
  const auto slash = path.find_last_of("/\\");
  std::string_view name =
      slash == std::string_view::npos ? path : path.substr(slash + 1);
  constexpr std::string_view kSuffix = ".prefab";
  if (name.size() > kSuffix.size() &&
      EqualFold(name.substr(name.size() - kSuffix.size()), kSuffix)) {
    name.remove_suffix(kSuffix.size());
  }
  return name;
}

bool IsWeaponRoot(std::string_view path) {
  return EqualFold(BaseName(path), kWeaponRoot);
}

bool IsCharacterFemaleRoot(std::string_view path) {
  const auto base = BaseName(path);
  return EqualFold(base, kCharacterFemaleRoot) ||
         EqualFold(base, kCharacterFemaleUnmaskedRoot);
}

bool IsCharacterFemaleUiRoot(std::string_view path) {
  return EqualFold(BaseName(path), kCharacterFemaleUiRoot);
}

bool IsCharacterMaleRoot(std::string_view path) {
  const auto base = BaseName(path);
  return EqualFold(base, kCharacterMaleRoot) ||
         EqualFold(base, kCharacterMaleUnmaskedRoot);
}

bool IsCharacterMaleUiRoot(std::string_view path) {
  return EqualFold(BaseName(path), kCharacterMaleUiRoot);
}

const EffectPair *FindEffectPair(std::string_view name) {
  const std::string_view base = BaseName(name);
  for (const auto &pair : kEffectPairs) {
    if (EqualFold(base, pair.original))
      return &pair;
  }
  return nullptr;
}

} // namespace BetterEndfieldNext::WeaponAppearance