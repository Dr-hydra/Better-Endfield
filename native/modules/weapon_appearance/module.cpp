#include "BetterEndfieldNext/ModuleApi.h"
#include "mapping.h"

#include <Windows.h>

#include <algorithm>
#include <atomic>
#include <cctype>
#include <cstdint>
#include <cstdio>
#include <cstring>
#include <memory>
#include <mutex>
#include <string>
#include <string_view>
#include <unordered_map>
#include <unordered_set>
#include <vector>

namespace BetterEndfieldNext::WeaponAppearance {
namespace {

constexpr char kId[] = "betterendfieldnext.weapon_appearance";
constexpr char kResourceAssembly[] = "Common.Beyond.dll";
constexpr char kCoreAssembly[] = "UnityEngine.CoreModule.dll";

constexpr uint8_t kMainCategory = 0;

constexpr char kInkWeaponPath[] = "assets/beyond/dynamicassets/gameplay/"
                                  "prefabs/weapons/wpn_sword_0021_ink.prefab";
constexpr uint64_t kInkWeaponHash = 150471484204821180ull;

constexpr char kInkCharacterFemalePath[] =
    "assets/beyond/dynamicassets/gameplay/actors/postmodels/characters/"
    "chr_0003_endminfink_postmodel.prefab";
constexpr uint64_t kInkCharacterFemaleHash = 428420327571254834ull;

constexpr char kUnmaskedCharacterFemalePath[] =
    "assets/beyond/dynamicassets/gameplay/actors/postmodels/characters/"
    "chr_0003_endminfunmasked_postmodel.prefab";

constexpr char kInkCharacterMalePath[] =
    "assets/beyond/dynamicassets/gameplay/actors/postmodels/characters/"
    "chr_0002_endminmink_postmodel.prefab";
constexpr uint64_t kInkCharacterMaleHash = 346739328816472612ull;

constexpr char kUnmaskedCharacterMalePath[] =
    "assets/beyond/dynamicassets/gameplay/actors/postmodels/characters/"
    "chr_0002_endminmunmasked_postmodel.prefab";

enum class CharacterMode {
  kInk,
  kUnmasked,
  kDefault
};

enum class WeaponMode {
  kInk,
  kDefault
};

enum class EffectsMode {
  kInk,
  kDefault
};

struct Configuration {
  bool enabled = true;
  bool replace_weapon = true;
  WeaponMode weapon_mode = WeaponMode::kInk;
  bool replace_character = true;
  CharacterMode character_mode = CharacterMode::kInk;
  bool replace_effects = true;
  EffectsMode effects_mode = EffectsMode::kInk;
};

// MSVC specialises std::atomic<std::shared_ptr>, libc++ does not, so the
// snapshot is guarded by a mutex instead. FinishDetour copies the pointer
// under the lock and releases it before touching managed state.
std::mutex g_config_mutex;
std::shared_ptr<const Configuration> g_config{std::make_shared<Configuration>()};

std::shared_ptr<const Configuration> LoadConfiguration() {
  std::lock_guard<std::mutex> lock(g_config_mutex);
  return g_config;
}
const BE_HostApiV1 *g_host = nullptr;
std::atomic_bool g_stopping{false};

void Log(const std::string &message) {
  if (g_host && g_host->log) {
    g_host->log(g_host->context, kId, message.c_str());
  }
}

std::string ManagedString(const void *value) {
  if (!g_host || !value)
    return {};
  std::vector<char> buffer(512);
  const int copied = g_host->copy_managed_string(g_host->context, value,
                                                 buffer.data(), buffer.size());
  return copied > 0 ? std::string(buffer.data(), static_cast<size_t>(copied))
                    : std::string{};
}

struct Contract {
  const char *key;
  BE_MethodDescriptorV1 descriptor;
  const void *method = nullptr;
  void *pointer = nullptr;
  bool required = false;
};

Contract g_contracts[]{
    {"asset.finish",
     {kResourceAssembly, "Beyond.Resource.Runtime", "BundleLoader.AssetProxy",
      "_FinishWithAsset", "UnityEngine.Object", "System.Void", 1},
     nullptr,
     nullptr,
     true},
    {"asset.load",
     {kResourceAssembly, "Beyond.Resource", "I18NAssetLoader", "Load",
      "Beyond.Resource.StringPathHash|System.Type|Beyond.Resource.RootCategory",
      "Beyond.Resource.FAssetProxyHandle", 3},
     nullptr,
     nullptr,
     true},
    {"asset.handle.get",
     {kResourceAssembly, "Beyond.Resource", "FAssetProxyHandle", "Get", nullptr,
      "UnityEngine.Object", 0},
     nullptr,
     nullptr,
     true},
    {"hash.string",
     {kResourceAssembly, "Beyond.Resource", "HashStringPathProcessor",
      "GetStringPathHash", "System.String", "System.Int64", 1},
     nullptr,
     nullptr,
     false},
    {"object.name",
     {kCoreAssembly, "UnityEngine", "Object", "get_name", nullptr,
      "System.String", 0},
     nullptr,
     nullptr,
     true},
    {"game_object.renderers",
     {kCoreAssembly, "UnityEngine", "GameObject", "GetComponentsInChildren",
      "System.Type|System.Boolean", "UnityEngine.Component[]", 2},
     nullptr,
     nullptr,
     false},
    {"skinned.get_shared_mesh",
     {kCoreAssembly, "UnityEngine", "SkinnedMeshRenderer", "get_sharedMesh",
      nullptr, "UnityEngine.Mesh", 0},
     nullptr,
     nullptr,
     false},
    {"renderer.get_shared_materials",
     {kCoreAssembly, "UnityEngine", "Renderer", "get_sharedMaterials",
      nullptr, "UnityEngine.Material[]", 0},
     nullptr,
     nullptr,
     false},
    {"renderer.set_shared_materials",
     {kCoreAssembly, "UnityEngine", "Renderer", "set_sharedMaterials",
      "UnityEngine.Material[]", "System.Void", 1},
     nullptr,
     nullptr,
     false},
    {"renderer.get_enabled",
     {kCoreAssembly, "UnityEngine", "Renderer", "get_enabled",
      nullptr, "System.Boolean", 0},
     nullptr,
     nullptr,
     false},
    {"renderer.set_enabled",
     {kCoreAssembly, "UnityEngine", "Renderer", "set_enabled",
      "System.Boolean", "System.Void", 1},
     nullptr,
     nullptr,
     false},
    {"array.get_length",
     {"mscorlib.dll", "System", "Array", "GetLength",
      "System.Int32", "System.Int32", 1},
     nullptr,
     nullptr,
     false},
    {"array.get_value",
     {"mscorlib.dll", "System", "Array", "GetValue",
      "System.Int32", "System.Object", 1},
     nullptr,
     nullptr,
     false},
            };

Contract *Find(const char *key) {
  for (auto &contract : g_contracts) {
    if (std::strcmp(contract.key, key) == 0)
      return &contract;
  }
  return nullptr;
}

bool ResolveContracts() {
  size_t missing = 0;
  for (auto &contract : g_contracts) {
    BE_ResolvedMethodV1 resolved{};
    const bool ok =
        g_host->resolve_method(g_host->context, &contract.descriptor,
                               &resolved) == BE_Result_Ok &&
        resolved.method_info && resolved.method_pointer;
    contract.method = resolved.method_info;
    contract.pointer = resolved.method_pointer;
    if (!ok && contract.required) {
      ++missing;
      Log(std::string("Required contract unavailable: ") + contract.key);
    }
  }
  return missing == 0;
}

using AssetGetFn = void *(__fastcall *)(void *, void *);

void *SafeAssetGet(void *handle) {
  auto *contract = Find("asset.handle.get");
  if (!contract || !contract->pointer)
    return nullptr;
#if defined(__ANDROID__)
  // Structured exception handling is MSVC-only; on ARM64 the managed adapter
  // owns the same lifetime as every other ported module, which calls through
  // without an SEH frame.
  return reinterpret_cast<AssetGetFn>(contract->pointer)(
      handle, const_cast<void *>(contract->method));
#else
  __try {
    return reinterpret_cast<AssetGetFn>(contract->pointer)(
        handle, const_cast<void *>(contract->method));
  } __except (EXCEPTION_EXECUTE_HANDLER) {
    return nullptr;
  }
#endif
}

void *Invoke(const char *key, void *instance, void **parameters, bool log_err = false) {
  auto *contract = Find(key);
  if (!contract || !contract->method || !g_host || !g_host->runtime_invoke)
    return nullptr;
  void *exception = nullptr;
  void *result = g_host->runtime_invoke(g_host->context, contract->method,
                                        instance, parameters, &exception);
  if (exception && log_err) {
    Log(std::string("Managed exception from ") + key);
  }
  return exception ? nullptr : result;
}

bool InvokeVoid(const char *key, void *instance, void **parameters) {
  auto *contract = Find(key);
  if (!contract || !contract->method || !g_host || !g_host->runtime_invoke)
    return false;
  void *exception = nullptr;
  g_host->runtime_invoke(g_host->context, contract->method, instance,
                         parameters, &exception);
  return exception == nullptr;
}

template <typename T>
bool Unbox(void *boxed, T &out_val) {
  if (!boxed || !g_host || !g_host->object_unbox)
    return false;
  void *raw = g_host->object_unbox(g_host->context, boxed);
  if (!raw)
    return false;
  std::memcpy(&out_val, raw, sizeof(T));
  return true;
}

template <typename T>
bool InvokeValue(const char *key, void *instance, void **parameters, T &out_val) {
  void *boxed = Invoke(key, instance, parameters);
  return Unbox(boxed, out_val);
}

int ArrayLength(void *array) {
  if (!array)
    return 0;
  int dimension = 0;
  int length = 0;
  void *params[1]{&dimension};
  return InvokeValue("array.get_length", array, params, length) ? length : 0;
}

void *ArrayValue(void *array, int index) {
  if (!array || index < 0)
    return nullptr;
  void *params[1]{&index};
  return Invoke("array.get_value", array, params);
}

BE_ResolvedClassV1 g_game_object_class{};
BE_ResolvedClassV1 g_renderer_class{};
BE_ResolvedClassV1 g_skinned_renderer_class{};

using ObjectClassFn = void *(*)(void *);
ObjectClassFn g_object_class = nullptr;

bool IsGameObject(const void *object) {
  return object && g_object_class && g_game_object_class.class_info &&
         g_object_class(const_cast<void *>(object)) ==
             g_game_object_class.class_info;
}

std::string ObjectName(const void *object) {
  Contract *contract = Find("object.name");
  return contract ? ManagedString(Invoke("object.name",
                                         const_cast<void *>(object), nullptr))
                  : std::string{};
}

struct InkAssetEntry {
  std::string path;
  uint64_t fallback_hash = 0;
  uint64_t resolved_hash = 0;
  void *asset = nullptr;
  uint32_t asset_root = 0;
  uint32_t handle_root = 0;
  std::atomic_bool failed{false};
  std::atomic<uint64_t> next_retry_ms{0};
};

InkAssetEntry g_ink_weapon{kInkWeaponPath, kInkWeaponHash};
InkAssetEntry g_ink_character_female{kInkCharacterFemalePath,
                                     kInkCharacterFemaleHash};
InkAssetEntry g_unmasked_character_female{kUnmaskedCharacterFemalePath, 0};
InkAssetEntry g_ink_character_male{kInkCharacterMalePath,
                                   kInkCharacterMaleHash};
InkAssetEntry g_unmasked_character_male{kUnmaskedCharacterMalePath, 0};

std::unordered_map<std::string, InkAssetEntry> g_ink_effects;

std::string g_last_reported_weapon;
std::string g_last_reported_female;
std::string g_last_reported_female_ui;
std::string g_last_reported_male;
std::string g_last_reported_male_ui;
std::unordered_set<std::string> g_reported_deliveries;
std::recursive_mutex g_appearance_mutex;

thread_local bool g_resolving_ink = false;

bool ResolveInkAsset(InkAssetEntry &entry, const char *label) {
  std::lock_guard<std::recursive_mutex> lock(g_appearance_mutex);
  if (entry.asset)
    return true;
  if (entry.failed.load())
    return false;
  if (g_resolving_ink)
    return false;
  if (GetTickCount64() < entry.next_retry_ms.load(std::memory_order_relaxed)) {
    return false;
  }

  auto *load_contract = Find("asset.load");
  auto *get_contract = Find("asset.handle.get");
  if (!load_contract || !get_contract || !g_game_object_class.type_object) {
    entry.failed.store(true);
    Log(std::string(label) +
        " unavailable: contracts or GameObject type missing.");
    return false;
  }

  uint64_t named_hash = 0;
  auto *hash_contract = Find("hash.string");
  if (hash_contract && hash_contract->method) {
    void *path = g_host->string_new(g_host->context, entry.path.c_str());
    void *hash_parameters[]{path};
    void *boxed_hash =
        path ? Invoke("hash.string", nullptr, hash_parameters) : nullptr;
    void *unboxed_hash = boxed_hash
                             ? g_host->object_unbox(g_host->context, boxed_hash)
                             : nullptr;
    if (unboxed_hash)
      named_hash = *static_cast<uint64_t *>(unboxed_hash);
  }

  uint64_t candidates[2]{named_hash, entry.fallback_hash};
  for (size_t index = 0; index < 2; ++index) {
    const uint64_t candidate = candidates[index];
    if (!candidate)
      continue;
    if (index == 1 && candidates[1] == candidates[0])
      break;

    uint8_t category = kMainCategory;
    void *load_parameters[]{&candidates[index], g_game_object_class.type_object,
                            &category};
    g_resolving_ink = true;
    void *boxed_handle = Invoke("asset.load", nullptr, load_parameters);
    void *handle = boxed_handle
                       ? g_host->object_unbox(g_host->context, boxed_handle)
                       : nullptr;
    void *asset = handle ? SafeAssetGet(handle) : nullptr;
    g_resolving_ink = false;

    if (!asset || !IsGameObject(asset)) {
      continue;
    }

    const uint32_t asset_root = g_host->gchandle_new(g_host->context, asset, 0);
    if (!asset_root) {
      continue;
    }

    entry.asset = asset;
    entry.resolved_hash = candidate;
    entry.asset_root = asset_root;
    if (boxed_handle) {
      entry.handle_root =
          g_host->gchandle_new(g_host->context, boxed_handle, 0);
    }

    char message[256]{};
    std::snprintf(message, sizeof(message),
                  "%s resolved: %s hash=%016llX asset=%p", label,
                  entry.path.c_str(),
                  static_cast<unsigned long long>(entry.resolved_hash), asset);
    Log(message);
    return true;
  }

  entry.next_retry_ms.store(GetTickCount64() + 2000, std::memory_order_relaxed);
  return false;
}

void *ResolveInkEffect(std::string_view ink_name) {
  std::lock_guard<std::recursive_mutex> lock(g_appearance_mutex);
  const std::string name_str(ink_name);
  auto &entry = g_ink_effects[name_str];
  if (entry.asset)
    return entry.asset;
  if (entry.failed.load())
    return nullptr;
  if (entry.path.empty()) {
    entry.path = "assets/beyond/dynamicassets/gameplay/effects/prefabs/" +
                 name_str + ".prefab";
  }
  if (ResolveInkAsset(entry, name_str.c_str())) {
    return entry.asset;
  }
  return nullptr;
}

enum class BodyPart {
  kUnknown = 0,
  kHairShadow,
  kHair,
  kEyeshadow,
  kEyebrow,
  kIris,
  kFace,
  kCloth01,
  kCloth02,
  kCloth03,
  kCloth04,
  kCloth05,
  kBody,
  kVfxPart
};

const char *BodyPartName(BodyPart part) {
  switch (part) {
  case BodyPart::kHairShadow: return "hairshadow";
  case BodyPart::kHair: return "hair";
  case BodyPart::kEyeshadow: return "eyeshadow";
  case BodyPart::kEyebrow: return "eyebrow";
  case BodyPart::kIris: return "iris";
  case BodyPart::kFace: return "face";
  case BodyPart::kCloth01: return "cloth_01";
  case BodyPart::kCloth02: return "cloth_02";
  case BodyPart::kCloth03: return "cloth_03";
  case BodyPart::kCloth04: return "cloth_04";
  case BodyPart::kCloth05: return "cloth_05";
  case BodyPart::kBody: return "body";
  case BodyPart::kVfxPart: return "vfxpart";
  default: return "unknown";
  }
}

BodyPart ClassifyRenderer(void *renderer) {
  if (!renderer)
    return BodyPart::kUnknown;
  std::string obj_name = ObjectName(renderer);
  void *mesh = Invoke("skinned.get_shared_mesh", renderer, nullptr);
  std::string mesh_name = mesh ? ObjectName(mesh) : std::string{};

  std::string text = obj_name + " " + mesh_name;
  for (char &c : text) {
    c = static_cast<char>(std::tolower(static_cast<unsigned char>(c)));
  }

  if (text.find("hairshadow") != std::string::npos)
    return BodyPart::kHairShadow;
  if (text.find("hair") != std::string::npos)
    return BodyPart::kHair;
  if (text.find("eyeshadow") != std::string::npos)
    return BodyPart::kEyeshadow;
  if (text.find("eyebrow") != std::string::npos ||
      text.find("brow") != std::string::npos)
    return BodyPart::kEyebrow;
  if (text.find("iris") != std::string::npos)
    return BodyPart::kIris;
  if (text.find("face") != std::string::npos)
    return BodyPart::kFace;
  if (text.find("cloth_01") != std::string::npos ||
      text.find("cloth01") != std::string::npos ||
      text.find("cloth_1") != std::string::npos)
    return BodyPart::kCloth01;
  if (text.find("cloth_02") != std::string::npos ||
      text.find("cloth02") != std::string::npos ||
      text.find("cloth_2") != std::string::npos)
    return BodyPart::kCloth02;
  if (text.find("cloth_03") != std::string::npos ||
      text.find("cloth03") != std::string::npos ||
      text.find("cloth_3") != std::string::npos)
    return BodyPart::kCloth03;
  if (text.find("cloth_04") != std::string::npos ||
      text.find("cloth04") != std::string::npos ||
      text.find("cloth_4") != std::string::npos)
    return BodyPart::kCloth04;
  if (text.find("cloth_05") != std::string::npos ||
      text.find("cloth05") != std::string::npos ||
      text.find("cloth_5") != std::string::npos)
    return BodyPart::kCloth05;
  if (text.find("body") != std::string::npos)
    return BodyPart::kBody;
  if (text.find("vfx") != std::string::npos)
    return BodyPart::kVfxPart;

  return BodyPart::kUnknown;
}

// Donor postmodels ship four LOD levels plus shadow proxies. Materials are
// only trustworthy from a visible LOD0 renderer, so score candidates instead
// of taking whichever one the hierarchy happens to return first.
int DonorPartScore(void *renderer) {
  if (!renderer)
    return -1000;
  std::string text = ObjectName(renderer);
  void *mesh = Invoke("skinned.get_shared_mesh", renderer, nullptr);
  if (mesh)
    text += " " + ObjectName(mesh);
  for (char &c : text)
    c = static_cast<char>(std::tolower(static_cast<unsigned char>(c)));
  int score = 0;
  if (text.find("shadow") != std::string::npos)
    score -= 100;
  if (text.find("_lod0") != std::string::npos)
    score += 20;
  else if (text.find("_lod3") != std::string::npos)
    score -= 3;
  else if (text.find("_lod2") != std::string::npos)
    score -= 2;
  else if (text.find("_lod1") != std::string::npos)
    score -= 1;
  return score;
}

// The UI detail model owns its skeleton, Avatar and bindposes. Geometry is
// replaced only when every donor bone can be resolved by name in the UI
// hierarchy; otherwise materials alone are transferred, which never distorts
// the model. Mask parts stay hidden in the unmasked variant.
void TransferAppearance(void *target_root, void *donor_root, const char *label,
                        bool hide_mask) {
  if (!target_root || !donor_root)
    return;

  void *rend_type = g_skinned_renderer_class.type_object
                        ? g_skinned_renderer_class.type_object
                        : g_renderer_class.type_object;
  if (!rend_type) {
    Log(std::string(label) + ": Renderer class type unavailable.");
    return;
  }

  bool inactive = true;
  void *args[]{rend_type, &inactive};

  void *donor_array = Invoke("game_object.renderers", donor_root, args);
  const int donor_len = ArrayLength(donor_array);
  if (donor_len <= 0) {
    Log(std::string(label) + ": donor has no renderers.");
    return;
  }

  std::unordered_map<BodyPart, void *> donor_by_part;
  for (int i = 0; i < donor_len; ++i) {
    void *rend = ArrayValue(donor_array, i);
    if (!rend)
      continue;
    const BodyPart part = ClassifyRenderer(rend);
    if (part == BodyPart::kUnknown)
      continue;
    const int score = DonorPartScore(rend);
    if (score < -50)
      continue;
    auto existing = donor_by_part.find(part);
    if (existing == donor_by_part.end())
      donor_by_part.emplace(part, rend);
    else if (score > DonorPartScore(existing->second))
      existing->second = rend;
  }

  void *target_array = Invoke("game_object.renderers", target_root, args);
  const int target_len = ArrayLength(target_array);
  if (target_len <= 0) {
    Log(std::string(label) + ": target has no renderers.");
    return;
  }

  size_t materials = 0, unmatched = 0;
  for (int i = 0; i < target_len; ++i) {
    void *target_rend = ArrayValue(target_array, i);
    if (!target_rend)
      continue;
    const BodyPart part = ClassifyRenderer(target_rend);

    // Mask parts follow the selected variant. Everything else stays visible:
    // the detail model must render exactly as it did before the swap.
    const bool is_mask =
        part == BodyPart::kCloth03 || part == BodyPart::kCloth05;
    if (is_mask) {
      // cloth_03 is the mask and cloth_05 its hair shadow. The unmasked
      // variant must hide both; the ink variant keeps the game's own state.
      if (hide_mask) {
        bool enabled = false;
        void *en_args[]{&enabled};
        InvokeVoid("renderer.set_enabled", target_rend, en_args);
      }
    }

    auto it = donor_by_part.find(part);
    if (it == donor_by_part.end()) {
      ++unmatched;
      continue;
    }

    void *donor_mats = Invoke("renderer.get_shared_materials", it->second, nullptr);
    void *target_mats =
        Invoke("renderer.get_shared_materials", target_rend, nullptr);
    const int donor_slots = ArrayLength(donor_mats);
    const int target_slots = ArrayLength(target_mats);
    if (donor_mats && donor_slots > 0 && donor_slots == target_slots) {
      void *mat_args[]{donor_mats};
      if (InvokeVoid("renderer.set_shared_materials", target_rend, mat_args))
        ++materials;
    } else {
      Log(std::string(label) + ": material slot mismatch part=" +
          std::to_string(static_cast<int>(part)) + " donor=" +
          std::to_string(donor_slots) + " target=" +
          std::to_string(target_slots));
    }

  }

  char msg[192]{};
  std::snprintf(msg, sizeof(msg),
                "%s: materials=%zu/%d unmatched=%zu mask=%s (geometry kept)",
                label, materials, target_len, unmatched,
                hide_mask ? "hidden" : "shown");
  Log(msg);
}

using FinishFn = void(__fastcall *)(void *, void *, const void *);
FinishFn g_original_finish = nullptr;

void __fastcall FinishDetour(void *proxy, void *asset, const void *method) {
  auto config = LoadConfiguration();
  if (!g_stopping.load() && !g_resolving_ink && config && config->enabled &&
      IsGameObject(asset)) {
    std::lock_guard<std::recursive_mutex> lock(g_appearance_mutex);
    const std::string name = ObjectName(asset);

    // 1. Weapon replacement
    if (config->replace_weapon && config->weapon_mode == WeaponMode::kInk &&
        IsWeaponRoot(name) && ResolveInkAsset(g_ink_weapon, "Ink weapon")) {
      if (g_last_reported_weapon != name) {
        g_last_reported_weapon = name;
        Log("Weapon redirected: " + name + " -> " +
            std::string(kWeaponInkRoot));
      }
      asset = g_ink_weapon.asset;
    }
    // 2. Character (female administrator - world postmodel) replacement
    else if (config->replace_character && IsCharacterFemaleRoot(name)) {
      if (config->character_mode == CharacterMode::kInk &&
          ResolveInkAsset(g_ink_character_female, "Ink female administrator")) {
        if (g_last_reported_female != name + "->ink") {
          g_last_reported_female = name + "->ink";
          Log("Character (female world) redirected: " + name + " -> " +
              std::string(kCharacterFemaleInkRoot));
        }
        asset = g_ink_character_female.asset;
      } else if (config->character_mode == CharacterMode::kUnmasked &&
                 ResolveInkAsset(g_unmasked_character_female,
                                 "Unmasked female administrator")) {
        if (g_last_reported_female != name + "->unmasked") {
          g_last_reported_female = name + "->unmasked";
          Log("Character (female world) redirected: " + name + " -> " +
              std::string(kCharacterFemaleUnmaskedRoot));
        }
        asset = g_unmasked_character_female.asset;
      }
    }
    // 3. Character (female administrator - UI detail model) appearance transfer
    else if (config->replace_character && IsCharacterFemaleUiRoot(name)) {
      void *donor = nullptr;
      const char *mode_label = "default";
      if (config->character_mode == CharacterMode::kInk &&
          ResolveInkAsset(g_ink_character_female, "Ink female administrator")) {
        donor = g_ink_character_female.asset;
        mode_label = "ink";
      } else if (config->character_mode == CharacterMode::kUnmasked &&
                 ResolveInkAsset(g_unmasked_character_female, "Unmasked female administrator")) {
        donor = g_unmasked_character_female.asset;
        mode_label = "unmasked";
      }
      if (donor) {
        if (g_last_reported_female_ui != name + "->" + mode_label) {
          g_last_reported_female_ui = name + "->" + mode_label;
          Log("Character (female UI) transferring appearance: " + name + " -> " + mode_label);
        }
        TransferAppearance(asset, donor, "Female UI Model",
                          config->character_mode == CharacterMode::kUnmasked);
      }
    }
    // 4. Character (male administrator - world postmodel) replacement
    else if (config->replace_character && IsCharacterMaleRoot(name)) {
      if (config->character_mode == CharacterMode::kInk &&
          ResolveInkAsset(g_ink_character_male, "Ink male administrator")) {
        if (g_last_reported_male != name + "->ink") {
          g_last_reported_male = name + "->ink";
          Log("Character (male world) redirected: " + name + " -> " +
              std::string(kCharacterMaleInkRoot));
        }
        asset = g_ink_character_male.asset;
      } else if (config->character_mode == CharacterMode::kUnmasked &&
                 ResolveInkAsset(g_unmasked_character_male,
                                 "Unmasked male administrator")) {
        if (g_last_reported_male != name + "->unmasked") {
          g_last_reported_male = name + "->unmasked";
          Log("Character (male world) redirected: " + name + " -> " +
              std::string(kCharacterMaleUnmaskedRoot));
        }
        asset = g_unmasked_character_male.asset;
      }
    }
    // 5. Character (male administrator - UI detail model) appearance transfer
    else if (config->replace_character && IsCharacterMaleUiRoot(name)) {
      void *donor = nullptr;
      const char *mode_label = "default";
      if (config->character_mode == CharacterMode::kInk &&
          ResolveInkAsset(g_ink_character_male, "Ink male administrator")) {
        donor = g_ink_character_male.asset;
        mode_label = "ink";
      } else if (config->character_mode == CharacterMode::kUnmasked &&
                 ResolveInkAsset(g_unmasked_character_male, "Unmasked male administrator")) {
        donor = g_unmasked_character_male.asset;
        mode_label = "unmasked";
      }
      if (donor) {
        if (g_last_reported_male_ui != name + "->" + mode_label) {
          g_last_reported_male_ui = name + "->" + mode_label;
          Log("Character (male UI) transferring appearance: " + name + " -> " + mode_label);
        }
        TransferAppearance(asset, donor, "Male UI Model",
                          config->character_mode == CharacterMode::kUnmasked);
      }
    }
    // 6. Combat & Blade VFX replacement
    else if (config->replace_effects && config->effects_mode == EffectsMode::kInk) {
      if (const EffectPair *pair = FindEffectPair(name)) {
        if (void *ink_asset = ResolveInkEffect(pair->ink)) {
          if (g_reported_deliveries.insert(std::string(pair->original)).second) {
            Log("Effect redirected (delivery): " + name + " -> " +
                std::string(pair->ink));
          }
          asset = ink_asset;
        }
      }
    }
  }
  if (g_original_finish)
    g_original_finish(proxy, asset, method);
}

bool ParseBool(std::string_view value, bool fallback) {
  if (value == "1" || value == "true" || value == "yes" || value == "on")
    return true;
  if (value == "0" || value == "false" || value == "no" || value == "off")
    return false;
  return fallback;
}

std::string_view Trim(std::string_view text) {
  while (!text.empty() && (text.front() == ' ' || text.front() == '\t'))
    text.remove_prefix(1);
  while (!text.empty() && (text.back() == ' ' || text.back() == '\t' ||
                           text.back() == '\r' || text.back() == '\n'))
    text.remove_suffix(1);
  return text;
}

bool ParseConfiguration(std::string_view text, Configuration &output) {
  while (!text.empty()) {
    const size_t end = text.find('\n');
    const std::string_view line = Trim(text.substr(0, end));
    text = end == std::string_view::npos ? std::string_view{}
                                         : text.substr(end + 1);
    if (line.empty() || line.front() == ';' || line.front() == '#' ||
        line.front() == '[') {
      continue;
    }
    const size_t equals = line.find('=');
    if (equals == std::string_view::npos)
      continue;
    const std::string_view key = Trim(line.substr(0, equals));
    const std::string_view value = Trim(line.substr(equals + 1));
    if (key == "enabled")
      output.enabled = ParseBool(value, output.enabled);
    else if (key == "replace_weapon")
      output.replace_weapon = ParseBool(value, output.replace_weapon);
    else if (key == "weapon_mode") {
      if (value == "default" || value == "none" || value == "off")
        output.weapon_mode = WeaponMode::kDefault;
      else
        output.weapon_mode = WeaponMode::kInk;
    } else if (key == "replace_character")
      output.replace_character = ParseBool(value, output.replace_character);
    else if (key == "character_mode") {
      if (value == "unmasked" || value == "unmask" || value == "2")
        output.character_mode = CharacterMode::kUnmasked;
      else if (value == "default" || value == "none" || value == "off" || value == "0")
        output.character_mode = CharacterMode::kDefault;
      else
        output.character_mode = CharacterMode::kInk;
    } else if (key == "replace_effects")
      output.replace_effects = ParseBool(value, output.replace_effects);
    else if (key == "effects_mode") {
      if (value == "default" || value == "none" || value == "off")
        output.effects_mode = EffectsMode::kDefault;
      else
        output.effects_mode = EffectsMode::kInk;
    }
  }
  return true;
}

void FreeEntry(InkAssetEntry &entry) {
  if (entry.asset_root) {
    g_host->gchandle_free(g_host->context, entry.asset_root);
    entry.asset_root = 0;
  }
  if (entry.handle_root) {
    g_host->gchandle_free(g_host->context, entry.handle_root);
    entry.handle_root = 0;
  }
  entry.asset = nullptr;
}

} // namespace

BE_Result BE_CALL Initialize(const BE_HostApiV1 *host) {
  if (!host || host->abi_version != BETTER_ENDFIELD_NEXT_MODULE_ABI_V1 ||
      !host->resolve_method || !host->runtime_invoke || !host->string_new ||
      !host->object_unbox || !host->gchandle_new ||
      !host->copy_managed_string || !host->resolve_class ||
      !host->create_hook) {
    return BE_Result_InvalidArgument;
  }
  g_host = host;
  g_stopping.store(false);

  if (host->copy_module_configuration) {
    std::vector<char> config_buffer(4096);
    int len = host->copy_module_configuration(host->context, kId,
                                              config_buffer.data(),
                                              config_buffer.size());
    if (len > 0) {
      Configuration initial;
      ParseConfiguration(std::string_view(config_buffer.data(), static_cast<size_t>(len)),
                         initial);
      {
        std::lock_guard<std::mutex> lock(g_config_mutex);
        g_config = std::make_shared<Configuration>(initial);
      }
    }
  }

  if (!ResolveContracts())
    return BE_Result_ContractMismatch;

  if (host->resolve_class(host->context, kCoreAssembly, "UnityEngine",
                          "GameObject", &g_game_object_class) != BE_Result_Ok ||
      !g_game_object_class.class_info || !g_game_object_class.type_object) {
    Log("GameObject class unavailable.");
    return BE_Result_ContractMismatch;
  }

  if (host->resolve_class(host->context, kCoreAssembly, "UnityEngine",
                          "Renderer", &g_renderer_class) != BE_Result_Ok ||
      !g_renderer_class.class_info || !g_renderer_class.type_object) {
    Log("Renderer class unavailable.");
  }

  if (host->resolve_class(host->context, kCoreAssembly, "UnityEngine",
                          "SkinnedMeshRenderer", &g_skinned_renderer_class) != BE_Result_Ok ||
      !g_skinned_renderer_class.class_info || !g_skinned_renderer_class.type_object) {
    Log("SkinnedMeshRenderer class unavailable.");
  }

  const HMODULE game = GetModuleHandleW(L"GameAssembly.dll");
  g_object_class = game ? reinterpret_cast<ObjectClassFn>(
                              GetProcAddress(game, "il2cpp_object_get_class"))
                        : nullptr;  if (!g_object_class) {
    Log("il2cpp_object_get_class unavailable.");
    return BE_Result_ContractMismatch;
  }

  if (host->create_hook(host->context, kId, Find("asset.finish")->pointer,
                        reinterpret_cast<void *>(&FinishDetour),
                        reinterpret_cast<void **>(&g_original_finish)) !=
          BE_Result_Ok ||
      !g_original_finish) {
    Log("Asset delivery hook unavailable.");
    host->release_module_hooks(host->context, kId);
    return BE_Result_Failed;
  }

  Log("Appearance module ready: Administrator appearances & Hongyuan ink.");
  return BE_Result_Ok;
}

BE_Result BE_CALL Configure(const char *text) {
  Configuration next;
  if (!ParseConfiguration(text ? text : "", next))
    return BE_Result_InvalidArgument;
  {
    std::lock_guard<std::mutex> lock(g_config_mutex);
    g_config = std::make_shared<Configuration>(next);
  }
  Log("Appearance configuration reloaded.");
  return BE_Result_Ok;
}

void BE_CALL Shutdown() {
  g_stopping.store(true);
  std::lock_guard<std::recursive_mutex> lock(g_appearance_mutex);

  FreeEntry(g_ink_weapon);
  FreeEntry(g_ink_character_female);
  FreeEntry(g_unmasked_character_female);
  FreeEntry(g_ink_character_male);
  FreeEntry(g_unmasked_character_male);
  for (auto &pair : g_ink_effects) {
    FreeEntry(pair.second);
  }
  g_ink_effects.clear();

  if (g_host && g_host->release_module_hooks) {
    g_host->release_module_hooks(g_host->context, kId);
  }
  g_original_finish = nullptr;
  Log("Appearance module shut down.");
}

const BE_ModuleApiV1 kApi{
    {kId, "Administrator Appearance", "0.5.3", BETTER_ENDFIELD_NEXT_MODULE_ABI_V1},
    &Initialize,
    &Configure,
    &Shutdown};

} // namespace BetterEndfieldNext::WeaponAppearance

BE_EXPORT const BE_ModuleApiV1 *BE_CALL BetterEndfieldNext_GetModuleApiV1() {
  return &BetterEndfieldNext::WeaponAppearance::kApi;
}
