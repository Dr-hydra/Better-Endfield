#include <BetterEndfield/ModuleApi.h>

#include "combat_overlay_protocol.h"

#include <Windows.h>

#include <algorithm>
#include <array>
#include <atomic>
#include <chrono>
#include <cctype>
#include <cmath>
#include <condition_variable>
#include <cstdint>
#include <cstring>
#include <cstdio>
#include <cstdlib>
#include <deque>
#include <filesystem>
#include <fstream>
#include <iomanip>
#include <iterator>
#include <map>
#include <mutex>
#include <sstream>
#include <string>
#include <string_view>
#include <thread>
#include <unordered_map>
#include <utility>
#include <vector>

namespace BetterEndfield::CombatStats {
namespace {

constexpr char kModuleId[] = "betterendfield.combat_stats";
constexpr size_t kMaxPendingEvents = 8192;
constexpr size_t kMaxRawEvents = 100000;

enum class ModuleState : uint8_t {
    Created,
    Resolving,
    Ready,
    Active,
    Disabled,
    Stopped,
};

struct RuntimeMethod {
    const char* key = nullptr;
    void* pointer = nullptr;
    const void* method_info = nullptr;
};

struct RuntimeField {
    const char* key = nullptr;
    BE_ResolvedFieldV1 value{};
    bool resolved = false;
};

struct Configuration {
    bool enabled = false;
    bool stats_enabled = false;
    bool hide_damage_numbers = false;
    bool record_all_damage = true;
    bool include_overkill = false;
    bool group_by_character = true;
    bool group_by_skill = true;
    bool group_by_damage_category = true;
    bool save_raw_events = false;
    bool diagnostics = false;
    double minimum_damage = 0.0;
    bool overlay_enabled = true;
    int toggle_vk = VK_F11;
    int overlay_vk = VK_F12;
    bool toggle_ctrl = false;
    bool overlay_ctrl = false;
};

struct DamageEvent {
    uint64_t session_id = 0;
    double elapsed_seconds = 0.0;
    double damage = 0.0;
    double hp_delta = 0.0;
    int32_t damage_type = 0;
    uint64_t decorate_mask = 0;
    bool critical = false;
    char attacker[160]{};
    char skill[160]{};
};

struct Aggregate {
    double damage = 0.0;
    uint64_t hits = 0;
    uint64_t critical_hits = 0;
};

struct CharacterAggregate : Aggregate {
    std::array<double, CombatOverlayProtocol::kDamageCategoryCount> damage_by_category{};
};

struct TimelineBucket {
    std::array<double, CombatOverlayProtocol::kDamageCategoryCount> damage_by_category{};
    std::map<std::string, double> damage_by_character;
};

struct Session {
    uint64_t id = 0;
    bool active = false;
    std::chrono::steady_clock::time_point started{};
    std::chrono::steady_clock::time_point ended{};
    double total_damage = 0.0;
    uint64_t hits = 0;
    uint64_t critical_hits = 0;
    uint64_t record_damage_calls = 0;
    uint64_t record_detail_calls = 0;
    uint64_t detail_init_calls = 0;
    uint64_t damage_text_calls = 0;
    uint64_t record_damage_baseline = 0;
    uint64_t record_detail_baseline = 0;
    uint64_t detail_init_baseline = 0;
    uint64_t damage_text_baseline = 0;
    std::map<std::string, CharacterAggregate> characters;
    std::map<std::string, Aggregate> skills;
    std::map<std::string, Aggregate> damage_categories;
    std::map<uint32_t, TimelineBucket> timeline;
    std::vector<DamageEvent> raw_events;
    std::chrono::system_clock::time_point started_utc{};
};

using DamageInitFn = void(__fastcall*)(void* instance, void* modifier, void* method);
using RecordDamageFn = void(__fastcall*)(void* instance, void* modifier, void* method);
using HpChangedFn = void(__fastcall*)(void* instance, void* event_data, void* method);
using BattleTickFn = void(__fastcall*)(void* instance, float delta_time, void* method);

const BE_HostApiV1* g_host = nullptr;
std::atomic<ModuleState> g_state{ModuleState::Created};
std::atomic_bool g_hooks_installed{false};
std::atomic_bool g_damage_hook_installed{false};
std::atomic_bool g_record_damage_hook_installed{false};
std::atomic_bool g_record_detail_hook_installed{false};
std::atomic_bool g_text_v1_hook_installed{false};
std::atomic_bool g_text_v2_hook_installed{false};
std::atomic_bool g_tick_hook_installed{false};
std::atomic_bool g_session_active{false};
std::atomic_bool g_worker_stop{false};
std::atomic_bool g_save_requested{false};
std::atomic<uint64_t> g_damage_hits{0};
std::atomic<uint64_t> g_damage_dropped{0};
std::atomic<uint64_t> g_record_damage_calls{0};
std::atomic<uint64_t> g_record_detail_calls{0};
std::atomic<uint64_t> g_detail_init_calls{0};
std::atomic<uint64_t> g_text_hits{0};
std::atomic<uint32_t> g_modifier_samples{0};
std::atomic_bool g_overlay_visible{true};

std::mutex g_config_mutex;
Configuration g_configuration;
std::mutex g_queue_mutex;
std::condition_variable g_queue_cv;
std::deque<DamageEvent> g_queue;
std::thread g_worker;
std::mutex g_session_mutex;
Session g_session;
std::mutex g_overlay_mutex;
HANDLE g_overlay_mapping = nullptr;
CombatOverlayProtocol::SharedSnapshot* g_overlay_snapshot = nullptr;
HANDLE g_overlay_process = nullptr;
std::chrono::steady_clock::time_point g_last_overlay_publish{};
std::chrono::steady_clock::time_point g_last_overlay_launch_check{};

RuntimeMethod g_damage_init;
RuntimeMethod g_record_damage;
RuntimeMethod g_record_damage_detail;
RuntimeMethod g_damage_text_v1;
RuntimeMethod g_damage_text_v2;
RuntimeMethod g_battle_tick;
RuntimeField g_attacker_id;
RuntimeField g_damage_value;
RuntimeField g_hp_delta;
RuntimeField g_damage_type;
RuntimeField g_damage_decorate_mask;
RuntimeField g_origin_skill_id;
RuntimeField g_is_critical;
RuntimeField g_modifier_source;
RuntimeField g_modifier_shared_flags;
RuntimeField g_modifier_delta_type;
RuntimeField g_modifier_value;
RuntimeField g_modifier_skill_cast_info;
RuntimeField g_modifier_real_delta;
RuntimeField g_modifier_damage_type;
RuntimeField g_modifier_damage_decorate_mask;
RuntimeField g_skill_cast_origin_skill;
RuntimeField g_ability_entity;
RuntimeField g_entity_name;
RuntimeField g_skill_id;
bool g_modifier_contract_ready = false;

DamageInitFn g_original_damage_init = nullptr;
RecordDamageFn g_original_record_damage = nullptr;
RecordDamageFn g_original_record_damage_detail = nullptr;
HpChangedFn g_original_damage_text_v1 = nullptr;
HpChangedFn g_original_damage_text_v2 = nullptr;
BattleTickFn g_original_battle_tick = nullptr;

void Log(const std::string& message) {
    if (g_host && g_host->log) {
        g_host->log(g_host->context, kModuleId, message.c_str());
    }
}

const char* ResultName(BE_Result result) {
    switch (result) {
    case BE_Result_Ok: return "ok";
    case BE_Result_NotFound: return "not-found";
    case BE_Result_ContractMismatch: return "contract-mismatch";
    case BE_Result_Conflict: return "conflict";
    default: return "failed";
    }
}

std::string Trim(std::string value) {
    const auto whitespace = [](unsigned char c) { return std::isspace(c) != 0; };
    value.erase(value.begin(), std::find_if_not(value.begin(), value.end(), whitespace));
    value.erase(std::find_if_not(value.rbegin(), value.rend(), whitespace).base(), value.end());
    return value;
}

bool ParseBool(const std::unordered_map<std::string, std::string>& values,
    const char* key, bool fallback) {
    const auto found = values.find(key);
    if (found == values.end()) return fallback;
    std::string value = found->second;
    std::transform(value.begin(), value.end(), value.begin(),
        [](unsigned char c) { return static_cast<char>(std::tolower(c)); });
    return value == "true" || value == "1" || value == "yes" || value == "on";
}

double ParseNumber(const std::unordered_map<std::string, std::string>& values,
    const char* key, double fallback, double minimum, double maximum) {
    const auto found = values.find(key);
    if (found == values.end()) return fallback;
    char* end = nullptr;
    const double parsed = std::strtod(found->second.c_str(), &end);
    return end != found->second.c_str() && std::isfinite(parsed)
        ? std::clamp(parsed, minimum, maximum) : fallback;
}

int ParseVirtualKey(std::string value, int fallback, bool& ctrl) {
    std::transform(value.begin(), value.end(), value.begin(),
        [](unsigned char c) { return static_cast<char>(std::toupper(c)); });
    value = Trim(std::move(value));
    ctrl = value.find("CTRL+") == 0 || value.find("CONTROL+") == 0;
    const size_t plus = value.find('+');
    if (plus != std::string::npos) value = value.substr(plus + 1);
    if (value.size() > 1 && value[0] == 'F') {
        const int number = std::atoi(value.c_str() + 1);
        if (number >= 1 && number <= 24) return VK_F1 + number - 1;
    }
    if (value.size() == 1) return static_cast<unsigned char>(value[0]);
    if (value == "SPACE") return VK_SPACE;
    if (value == "ENTER") return VK_RETURN;
    if (value == "NUMPAD0") return VK_NUMPAD0;
    return fallback;
}

Configuration ParseConfiguration(const char* text) {
    std::unordered_map<std::string, std::string> values;
    std::string_view source = text ? text : "";
    while (!source.empty()) {
        const size_t newline = source.find('\n');
        std::string line(source.substr(0, newline));
        source = newline == std::string_view::npos ? std::string_view{} :
            source.substr(newline + 1);
        line = Trim(std::move(line));
        if (line.empty() || line.front() == ';' || line.front() == '#') continue;
        const size_t separator = line.find('=');
        if (separator == std::string::npos || separator == 0) continue;
        std::string key = Trim(line.substr(0, separator));
        std::transform(key.begin(), key.end(), key.begin(),
            [](unsigned char c) { return static_cast<char>(std::tolower(c)); });
        values[std::move(key)] = Trim(line.substr(separator + 1));
    }
    Configuration result;
    result.enabled = ParseBool(values, "enabled", false);
    result.stats_enabled = ParseBool(values, "combat_stats_enabled", result.enabled);
    result.hide_damage_numbers = ParseBool(values, "hide_damage_numbers", false);
    result.record_all_damage = ParseBool(values, "record_all_damage", true);
    result.include_overkill = ParseBool(values, "include_overkill", false);
    result.group_by_character = ParseBool(values, "group_by_character", true);
    result.group_by_skill = ParseBool(values, "group_by_skill", true);
    result.group_by_damage_category = ParseBool(values, "group_by_damage_category",
        ParseBool(values, "group_by_damage_type", true));
    result.save_raw_events = ParseBool(values, "save_raw_events", false);
    result.diagnostics = ParseBool(values, "diagnostics", false);
    result.overlay_enabled = ParseBool(values, "overlay_enabled", true);
    result.minimum_damage = ParseNumber(values, "minimum_damage", 0.0, 0.0, 1.0e12);
    if (const auto found = values.find("hotkey_toggle"); found != values.end()) {
        result.toggle_vk = ParseVirtualKey(found->second, VK_F11, result.toggle_ctrl);
    } else if (const auto legacy = values.find("hotkey_start"); legacy != values.end()) {
        result.toggle_vk = ParseVirtualKey(legacy->second, VK_F11, result.toggle_ctrl);
    }
    if (const auto found = values.find("overlay_hotkey"); found != values.end())
        result.overlay_vk = ParseVirtualKey(found->second, VK_F12, result.overlay_ctrl);
    return result;
}

Configuration ConfigurationSnapshot() {
    std::scoped_lock lock(g_config_mutex);
    return g_configuration;
}

bool ResolveMethod(RuntimeMethod& output, const char* key, const char* assembly,
    const char* namespc, const char* klass, const char* method,
    uint32_t parameter_count, const char* return_type) {
    output = {};
    output.key = key;
    BE_MethodDescriptorV1 descriptor{
        assembly, namespc, klass, method, nullptr, return_type, parameter_count};
    BE_ResolvedMethodV1 result{};
    const BE_Result status = g_host->resolve_method(
        g_host->context, &descriptor, &result);
    if (status == BE_Result_Ok && result.method_info && result.method_pointer) {
        output.pointer = result.method_pointer;
        output.method_info = result.method_info;
        Log(std::string("[combat-contract] resolved ") + key);
        return true;
    }
    Log(std::string("[combat-contract] missing ") + key + " result=" + ResultName(status));
    return false;
}

bool ResolveField(RuntimeField& output, const char* key, const char* assembly,
    const char* namespc, const char* klass, const char* field) {
    output = {};
    output.key = key;
    BE_FieldDescriptorV1 descriptor{assembly, namespc, klass, field, nullptr};
    const BE_Result status = g_host->resolve_field(g_host->context, &descriptor, &output.value);
    output.resolved = status == BE_Result_Ok && output.value.field_info && output.value.offset >= 0;
    if (!output.resolved) {
        Log(std::string("[combat-contract] missing field ") + key + " result=" + ResultName(status));
    } else {
        Log(std::string("[combat-contract] resolved field ") + key +
            " offset=" + std::to_string(output.value.offset));
    }
    return output.resolved;
}

bool ResolveRuntimeContract() {
    g_state.store(ModuleState::Resolving, std::memory_order_release);
    const bool damage_init = ResolveMethod(g_damage_init, "damage.detail.init",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core",
        "BattleManager.BattleRecorder.DamageDetail", "Init", 1, "System.Void");
    ResolveMethod(g_record_damage, "battle.recorder.record-damage",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core",
        "BattleManager.BattleRecorder", "RecordDamage", 1, "System.Void");
    ResolveMethod(g_record_damage_detail, "battle.recorder.record-damage-detail",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core",
        "BattleManager.BattleRecorder", "_RecordDamageDetail", 1, "System.Void");
    const bool text_v1 = ResolveMethod(g_damage_text_v1, "damage.text.hp-changed-v1",
        "UI.Gameplay.Beyond.dll", "Beyond.UI", "DamageTextCtrl",
        "_OnHpChanged", 1, "System.Void");
    const bool text_v2 = ResolveMethod(g_damage_text_v2, "damage.text.hp-changed-v2",
        "UI.Gameplay.Beyond.dll", "Beyond.UI", "DamageTextCtrlV2",
        "_OnHpChanged", 1, "System.Void");
    const bool battle_tick = ResolveMethod(g_battle_tick, "battle.manager.tick",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core", "BattleManager", "Tick", 1,
        "System.Void");
    const char* assembly = "Gameplay.Beyond.dll";
    const char* namespc = "Beyond.Gameplay.Core";
    const char* klass = "BattleManager.BattleRecorder.DamageDetail";
    const bool fields =
        ResolveField(g_attacker_id, "damage.attacker", assembly, namespc, klass, "attackerId") &&
        ResolveField(g_damage_value, "damage.value", assembly, namespc, klass, "damageValue") &&
        ResolveField(g_hp_delta, "damage.hp-delta", assembly, namespc, klass, "hpDelta") &&
        ResolveField(g_damage_type, "damage.type", assembly, namespc, klass, "damageType") &&
        ResolveField(g_damage_decorate_mask, "damage.decorate-mask", assembly, namespc, klass,
            "damageDecorateMask") &&
        ResolveField(g_origin_skill_id, "damage.skill", assembly, namespc, klass, "originSkillId") &&
        ResolveField(g_is_critical, "damage.critical", assembly, namespc, klass, "isCritical");
    const char* modifier_class = "AbilitySystem.Modifier";
    const bool source_field = ResolveField(g_modifier_source, "modifier.source", assembly,
        namespc, modifier_class, "m_source");
    const bool shared_flags_field = ResolveField(g_modifier_shared_flags,
        "modifier.shared-flags", assembly, namespc, modifier_class, "m_sharedFlagMask");
    const bool delta_type_field = ResolveField(g_modifier_delta_type, "modifier.delta-type",
        assembly, namespc, modifier_class, "<deltaType>k__BackingField");
    const bool value_field = ResolveField(g_modifier_value, "modifier.value", assembly,
        namespc, modifier_class, "<value>k__BackingField");
    const bool skill_cast_field = ResolveField(g_modifier_skill_cast_info,
        "modifier.skill-cast-info", assembly, namespc, modifier_class,
        "<skillCastInfo>k__BackingField");
    const bool real_delta_field = ResolveField(g_modifier_real_delta, "modifier.real-delta",
        assembly, namespc, modifier_class, "<realDelta>k__BackingField");
    const bool modifier_damage_type_field = ResolveField(g_modifier_damage_type,
        "modifier.damage-type", assembly, namespc, modifier_class,
        "<damageType>k__BackingField");
    const bool modifier_mask_field = ResolveField(g_modifier_damage_decorate_mask,
        "modifier.damage-decorate-mask", assembly, namespc, modifier_class,
        "<damageDecorateMask>k__BackingField");
    const bool origin_skill_field = ResolveField(g_skill_cast_origin_skill,
        "skill-cast.origin-skill", assembly, namespc, "Skill.SkillCastInfo", "originSkill");
    const bool ability_entity_field = ResolveField(g_ability_entity, "ability.entity", assembly,
        namespc, "BaseComponent", "<entity>k__BackingField");
    const bool entity_name_field = ResolveField(g_entity_name, "entity.name", assembly, namespc,
        "Entity", "<name>k__BackingField");
    const bool skill_id_field = ResolveField(g_skill_id, "skill.id", assembly, namespc,
        "Skill", "m_skillId");
    g_modifier_contract_ready = shared_flags_field && delta_type_field && value_field &&
        real_delta_field && modifier_damage_type_field && modifier_mask_field;
    if (!damage_init || !fields) {
        Log("[combat-contract] damage recording unavailable; module remains isolated");
    } else {
        Log("[combat-contract] damage recording contract ready");
    }
    if (!g_modifier_contract_ready) {
        Log("[combat-contract] universal Modifier recording unavailable");
    } else {
        Log("[combat-contract] universal Modifier recording contract ready");
    }
    if (!source_field || !ability_entity_field || !entity_name_field) {
        Log("[combat-contract] attacker labels unavailable; totals remain available");
    }
    if (!skill_cast_field || !origin_skill_field || !skill_id_field) {
        Log("[combat-contract] skill labels unavailable; totals remain available");
    }
    if (!text_v1 && !text_v2) {
        Log("[combat-contract] damage number hiding unavailable; recording can still work");
    } else {
        Log("[combat-contract] final damage text UI contract ready");
    }
    if (!battle_tick) {
        Log("[combat-contract] hotkey tick unavailable; use recording only when a session is armed externally");
    }
    return true;
}

std::string CopyString(void* value) {
    if (!value || !g_host || !g_host->copy_managed_string) return {};
    char buffer[160]{};
    g_host->copy_managed_string(g_host->context, value, buffer, sizeof(buffer));
    return buffer;
}

template <typename T>
bool ReadValue(void* instance, const RuntimeField& field, T& value) {
    if (!instance || !field.resolved) return false;
    __try {
        value = *reinterpret_cast<T*>(reinterpret_cast<uint8_t*>(instance) + field.value.offset);
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

// IL2CPP metadata field offsets for value types include the 16-byte boxed object
// header. A value type passed by ref points directly at its unboxed payload.
template <typename T>
bool ReadValueTypeField(void* value_type, const RuntimeField& field, T& value) {
    if (!value_type || !field.resolved || field.value.offset < 16) return false;
    __try {
        value = *reinterpret_cast<T*>(reinterpret_cast<uint8_t*>(value_type) +
            field.value.offset - 16);
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

void* EmbeddedValueType(void* owner, const RuntimeField& field) {
    if (!owner || !field.resolved || field.value.offset < 16) return nullptr;
    return reinterpret_cast<uint8_t*>(owner) + field.value.offset - 16;
}

std::string ResolveAttackerName(void* modifier) {
    void* ability = nullptr;
    if (!ReadValueTypeField(modifier, g_modifier_source, ability) || !ability) return {};
    void* entity = nullptr;
    if (!ReadValue(ability, g_ability_entity, entity) || !entity) return {};
    void* name = nullptr;
    if (!ReadValue(entity, g_entity_name, name) || !name) return {};
    return CopyString(name);
}

std::string ResolveSkillId(void* modifier) {
    void* cast_info = EmbeddedValueType(modifier, g_modifier_skill_cast_info);
    if (!cast_info) return {};
    void* skill = nullptr;
    if (!ReadValueTypeField(cast_info, g_skill_cast_origin_skill, skill) || !skill) return {};
    void* id = nullptr;
    if (!ReadValue(skill, g_skill_id, id) || !id) return {};
    return CopyString(id);
}

std::string JsonEscape(std::string_view value) {
    std::string result;
    result.reserve(value.size() + 8);
    for (const unsigned char c : value) {
        switch (c) {
        case '\\': result += "\\\\"; break;
        case '"': result += "\\\""; break;
        case '\n': result += "\\n"; break;
        case '\r': result += "\\r"; break;
        case '\t': result += "\\t"; break;
        default:
            if (c < 0x20) {
                char escaped[8]{};
                std::snprintf(escaped, sizeof(escaped), "\\u%04x", c);
                result += escaped;
            } else {
                result.push_back(static_cast<char>(c));
            }
        }
    }
    return result;
}

std::string NormalizeAttackerId(std::string value) {
    const size_t separator = value.rfind('_');
    if (separator == std::string::npos || separator + 1 >= value.size()) return value;
    const bool numeric_suffix = std::all_of(value.begin() + separator + 1, value.end(),
        [](unsigned char c) { return std::isdigit(c) != 0; });
    if (numeric_suffix) value.resize(separator);
    return value;
}

size_t SkillCategoryFromId(std::string_view skill) {
    if (skill.find("_combo") != std::string_view::npos) return 3;
    if (skill.find("_ultimate_skill") != std::string_view::npos) return 2;
    if (skill.find("_normal_skill") != std::string_view::npos) return 1;
    if (skill.find("_attack") != std::string_view::npos) return 0;
    if (skill.find("_passive_skill") != std::string_view::npos) return 4;
    return 5;
}

void WriteAggregateMap(std::ostream& output, const std::map<std::string, Aggregate>& values) {
    output << "{";
    bool first = true;
    for (const auto& [key, value] : values) {
        if (!first) output << ",";
        first = false;
        output << "\"" << JsonEscape(key) << "\":{";
        output << "\"damage\":" << std::setprecision(15) << value.damage;
        output << ",\"hits\":" << value.hits;
        output << ",\"criticalHits\":" << value.critical_hits << "}";
    }
    output << "}";
}

void WriteCharacterMap(std::ostream& output,
    const std::map<std::string, CharacterAggregate>& values) {
    output << "{";
    bool first = true;
    for (const auto& [key, value] : values) {
        if (!first) output << ",";
        first = false;
        output << "\"" << JsonEscape(key) << "\":{";
        output << "\"damage\":" << std::setprecision(15) << value.damage;
        output << ",\"hits\":" << value.hits;
        output << ",\"criticalHits\":" << value.critical_hits;
        output << ",\"damageByCategory\":[";
        for (size_t index = 0; index < value.damage_by_category.size(); ++index) {
            if (index) output << ",";
            output << value.damage_by_category[index];
        }
        output << "]}";
    }
    output << "}";
}

std::filesystem::path SessionsDirectory() {
    wchar_t buffer[32768]{};
    const DWORD length = GetEnvironmentVariableW(L"LOCALAPPDATA", buffer,
        static_cast<DWORD>(std::size(buffer)));
    const std::filesystem::path root = length ? std::filesystem::path(buffer) :
        std::filesystem::temp_directory_path();
    return root / L"BetterEndfield" / L"combat-sessions";
}

void SaveSession(const Session& session) {
    if (session.id == 0) return;
    std::error_code error;
    const auto directory = SessionsDirectory();
    std::filesystem::create_directories(directory, error);
    if (error) {
        Log("[combat-save] create directory failed");
        return;
    }
    const auto stamp = std::chrono::duration_cast<std::chrono::seconds>(
        session.started_utc.time_since_epoch()).count();
    const auto path = directory / ("combat-" + std::to_string(stamp) + "-" +
        std::to_string(session.id) + ".json");
    std::ofstream output(path, std::ios::binary | std::ios::trunc);
    if (!output) {
        Log("[combat-save] open result failed");
        return;
    }
    const double duration = std::max(0.001, std::chrono::duration<double>(
        session.ended - session.started).count());
    output << "{\n  \"schemaVersion\":3,\n";
    output << "  \"sessionId\":" << session.id << ",\n";
    output << "  \"startedUnixSeconds\":" << stamp << ",\n";
    output << "  \"durationSeconds\":" << std::setprecision(15) << duration << ",\n";
    output << "  \"totalDamage\":" << session.total_damage << ",\n";
    output << "  \"dps\":" << session.total_damage / duration << ",\n";
    output << "  \"hitCount\":" << session.hits << ",\n";
    output << "  \"criticalCount\":" << session.critical_hits << ",\n";
    output << "  \"routeHits\":{\"recordDamage\":" << session.record_damage_calls;
    output << ",\"recordDamageDetail\":" << session.record_detail_calls;
    output << ",\"damageDetailInit\":" << session.detail_init_calls;
    output << ",\"damageText\":" << session.damage_text_calls << "},\n";
    output << "  \"characters\":"; WriteCharacterMap(output, session.characters); output << ",\n";
    output << "  \"skills\":"; WriteAggregateMap(output, session.skills); output << ",\n";
    output << "  \"damageCategories\":";
    WriteAggregateMap(output, session.damage_categories);
    output << ",\n  \"timelineBucketSeconds\":0.25,\n";
    output << "  \"timeline\":[";
    bool first_bucket = true;
    for (const auto& [bucket_index, bucket] : session.timeline) {
        if (!first_bucket) output << ",";
        first_bucket = false;
        output << "{\"time\":" << std::setprecision(15)
            << static_cast<double>(bucket_index) * 0.25 << ",\"damageByCategory\":[";
        for (size_t index = 0; index < bucket.damage_by_category.size(); ++index) {
            if (index) output << ",";
            output << bucket.damage_by_category[index];
        }
        output << "],\"damageByCharacter\":{";
        bool first_character = true;
        for (const auto& [character, damage] : bucket.damage_by_character) {
            if (!first_character) output << ",";
            first_character = false;
            output << "\"" << JsonEscape(character) << "\":" << damage;
        }
        output << "}}";
    }
    output << "]";
    if (!session.raw_events.empty()) {
        output << ",\n  \"events\":[";
        for (size_t index = 0; index < session.raw_events.size(); ++index) {
            if (index) output << ",";
            const auto& event = session.raw_events[index];
            output << "{\"time\":" << event.elapsed_seconds
                << ",\"damage\":" << event.damage << ",\"hpDelta\":" << event.hp_delta;
            output << ",\"damageType\":" << event.damage_type << ",\"damageDecorateMask\":"
                << event.decorate_mask << ",\"critical\":" << (event.critical ? "true" : "false");
            output << ",\"attackerId\":\"" << JsonEscape(event.attacker);
            output << "\",\"originSkillId\":\"" << JsonEscape(event.skill) << "\"}";
        }
        output << "]";
    }
    output << "\n}\n";
    Log(std::string("[combat-save] wrote ") + path.string());
}

void ProcessEvent(const DamageEvent& event) {
    const Configuration config = ConfigurationSnapshot();
    std::scoped_lock lock(g_session_mutex);
    if (event.session_id != g_session.id) return;
    if (!config.record_all_damage && event.attacker[0] == '\0') return;
    double amount = std::abs(event.damage);
    const double health_change = std::abs(event.hp_delta);
    if (!config.include_overkill && health_change > 0.0)
        amount = std::min(amount, health_change);
    if (amount < config.minimum_damage) return;
    g_session.total_damage += amount;
    ++g_session.hits;
    if (event.critical) ++g_session.critical_hits;
    const std::string character = event.attacker[0]
        ? NormalizeAttackerId(event.attacker) : "<unknown>";
    const std::string skill = event.skill[0] ? event.skill : "<unknown>";
    const size_t skill_category = SkillCategoryFromId(skill);
    static constexpr std::array<const char*, CombatOverlayProtocol::kDamageCategoryCount>
        kCategoryIds{"basic_attack", "skill", "ultimate", "combo", "passive", "other"};
    auto add = [&event, amount](Aggregate& aggregate) {
        aggregate.damage += amount;
        ++aggregate.hits;
        if (event.critical) ++aggregate.critical_hits;
    };
    if (config.group_by_character) {
        CharacterAggregate& aggregate = g_session.characters[character];
        add(aggregate);
        aggregate.damage_by_category[skill_category] += amount;
    }
    if (config.group_by_skill) add(g_session.skills[skill]);
    if (config.group_by_damage_category)
        add(g_session.damage_categories[kCategoryIds[skill_category]]);
    constexpr double kTimelineBucketSeconds = 0.25;
    const auto bucket_index = static_cast<uint32_t>(std::max(0.0,
        std::floor(event.elapsed_seconds / kTimelineBucketSeconds)));
    TimelineBucket& bucket = g_session.timeline[bucket_index];
    bucket.damage_by_category[skill_category] += amount;
    bucket.damage_by_character[character] += amount;
    if (config.save_raw_events && g_session.raw_events.size() < kMaxRawEvents)
        g_session.raw_events.push_back(event);
}

void WorkerLoop() {
    while (!g_worker_stop.load(std::memory_order_acquire)) {
        DamageEvent event{};
        bool has_event = false;
        {
            std::unique_lock lock(g_queue_mutex);
            g_queue_cv.wait(lock, [] {
                return g_worker_stop.load(std::memory_order_acquire) ||
                    !g_queue.empty() || g_save_requested.load(std::memory_order_acquire);
            });
            if (!g_queue.empty()) {
                event = g_queue.front();
                g_queue.pop_front();
                has_event = true;
            }
        }
        if (has_event) {
            ProcessEvent(event);
            continue;
        }
        if (g_save_requested.exchange(false, std::memory_order_acq_rel)) {
            Session snapshot;
            {
                std::scoped_lock lock(g_session_mutex);
                snapshot = g_session;
            }
            SaveSession(snapshot);
        }
    }
    DamageEvent event{};
    while (true) {
        {
            std::scoped_lock lock(g_queue_mutex);
            if (g_queue.empty()) break;
            event = g_queue.front();
            g_queue.pop_front();
        }
        ProcessEvent(event);
    }
    if (g_save_requested.exchange(false, std::memory_order_acq_rel)) {
        Session snapshot;
        {
            std::scoped_lock lock(g_session_mutex);
            snapshot = g_session;
        }
        SaveSession(snapshot);
    }
}

bool EnsureOverlayMappingLocked() {
    if (g_overlay_snapshot) return true;
    const std::wstring mapping_name = CombatOverlayProtocol::MappingName(GetCurrentProcessId());
    g_overlay_mapping = CreateFileMappingW(INVALID_HANDLE_VALUE, nullptr, PAGE_READWRITE, 0,
        static_cast<DWORD>(sizeof(CombatOverlayProtocol::SharedSnapshot)), mapping_name.c_str());
    if (!g_overlay_mapping) {
        Log("[combat-overlay] CreateFileMapping failed error=" +
            std::to_string(GetLastError()));
        return false;
    }
    g_overlay_snapshot = static_cast<CombatOverlayProtocol::SharedSnapshot*>(MapViewOfFile(
        g_overlay_mapping, FILE_MAP_ALL_ACCESS, 0, 0,
        sizeof(CombatOverlayProtocol::SharedSnapshot)));
    if (!g_overlay_snapshot) {
        Log("[combat-overlay] MapViewOfFile failed error=" +
            std::to_string(GetLastError()));
        CloseHandle(g_overlay_mapping);
        g_overlay_mapping = nullptr;
        return false;
    }
    std::memset(g_overlay_snapshot, 0, sizeof(*g_overlay_snapshot));
    g_overlay_snapshot->magic = CombatOverlayProtocol::kMagic;
    g_overlay_snapshot->version = CombatOverlayProtocol::kVersion;
    g_overlay_snapshot->structure_size = sizeof(*g_overlay_snapshot);
    g_overlay_snapshot->game_pid = GetCurrentProcessId();
    Log("[combat-overlay] shared snapshot ready name=Local\\BetterEndfield.CombatStats." +
        std::to_string(GetCurrentProcessId()));
    return true;
}

std::filesystem::path OverlayExecutablePath() {
    HMODULE module = nullptr;
    if (!GetModuleHandleExW(GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS |
            GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
            reinterpret_cast<LPCWSTR>(&OverlayExecutablePath), &module)) {
        return {};
    }
    std::wstring path(32768, L'\0');
    const DWORD length = GetModuleFileNameW(module, path.data(),
        static_cast<DWORD>(path.size()));
    if (!length || length >= path.size()) return {};
    path.resize(length);
    return std::filesystem::path(path).parent_path() / L"BetterEndfield.CombatOverlay.exe";
}

void EnsureOverlayProcess() {
    std::scoped_lock lock(g_overlay_mutex);
    if (!EnsureOverlayMappingLocked()) return;
    if (g_overlay_process) {
        if (WaitForSingleObject(g_overlay_process, 0) == WAIT_TIMEOUT) return;
        CloseHandle(g_overlay_process);
        g_overlay_process = nullptr;
    }
    const auto executable = OverlayExecutablePath();
    if (executable.empty() || !std::filesystem::is_regular_file(executable)) {
        Log("[combat-overlay] companion executable not found beside CombatStats DLL");
        return;
    }
    const std::wstring mapping_name = CombatOverlayProtocol::MappingName(GetCurrentProcessId());
    std::wstring command = L"\"" + executable.wstring() + L"\" --game-pid " +
        std::to_wstring(GetCurrentProcessId()) + L" --mapping \"" + mapping_name + L"\"";
    STARTUPINFOW startup{};
    startup.cb = sizeof(startup);
    PROCESS_INFORMATION process{};
    const std::wstring working_directory = executable.parent_path().wstring();
    if (!CreateProcessW(executable.c_str(), command.data(), nullptr, nullptr, FALSE,
            CREATE_NO_WINDOW, nullptr, working_directory.c_str(), &startup, &process)) {
        Log("[combat-overlay] launch failed error=" + std::to_string(GetLastError()));
        return;
    }
    CloseHandle(process.hThread);
    g_overlay_process = process.hProcess;
    Log("[combat-overlay] companion started pid=" + std::to_string(process.dwProcessId));
}

void PublishOverlaySnapshot(const Configuration& config, bool shutdown = false) {
    Session session;
    {
        std::scoped_lock lock(g_session_mutex);
        session = g_session;
    }
    double duration = 0.0;
    if (session.id != 0) {
        const auto end = g_session_active.load(std::memory_order_acquire)
            ? std::chrono::steady_clock::now() : session.ended;
        duration = std::max(0.0, std::chrono::duration<double>(end - session.started).count());
    }
    std::vector<std::pair<std::string, CharacterAggregate>> characters(
        session.characters.begin(), session.characters.end());
    std::sort(characters.begin(), characters.end(), [](const auto& left, const auto& right) {
        return left.second.damage > right.second.damage;
    });

    std::scoped_lock lock(g_overlay_mutex);
    if (!EnsureOverlayMappingLocked()) return;
    LONG sequence = InterlockedIncrement(&g_overlay_snapshot->sequence);
    if ((sequence & 1) == 0) InterlockedIncrement(&g_overlay_snapshot->sequence);
    MemoryBarrier();
    g_overlay_snapshot->magic = CombatOverlayProtocol::kMagic;
    g_overlay_snapshot->version = CombatOverlayProtocol::kVersion;
    g_overlay_snapshot->structure_size = sizeof(*g_overlay_snapshot);
    g_overlay_snapshot->game_pid = GetCurrentProcessId();
    g_overlay_snapshot->overlay_enabled = config.stats_enabled && config.overlay_enabled;
    g_overlay_snapshot->overlay_visible = g_overlay_visible.load(std::memory_order_relaxed);
    g_overlay_snapshot->session_active = g_session_active.load(std::memory_order_relaxed);
    g_overlay_snapshot->shutdown_requested = shutdown;
    g_overlay_snapshot->session_id = session.id;
    g_overlay_snapshot->duration_seconds = duration;
    g_overlay_snapshot->total_damage = session.total_damage;
    g_overlay_snapshot->dps = duration > 0.0 ? session.total_damage / duration : 0.0;
    g_overlay_snapshot->hit_count = session.hits;
    g_overlay_snapshot->critical_count = session.critical_hits;
    g_overlay_snapshot->character_count = static_cast<uint32_t>(std::min<size_t>(
        characters.size(), CombatOverlayProtocol::kMaxCharacters));
    std::memset(g_overlay_snapshot->characters, 0, sizeof(g_overlay_snapshot->characters));
    for (uint32_t index = 0; index < g_overlay_snapshot->character_count; ++index) {
        auto& destination = g_overlay_snapshot->characters[index];
        const auto& [id, aggregate] = characters[index];
        std::snprintf(destination.id, sizeof(destination.id), "%s", id.c_str());
        destination.total_damage = aggregate.damage;
        destination.hits = aggregate.hits;
        destination.critical_hits = aggregate.critical_hits;
        std::copy(aggregate.damage_by_category.begin(), aggregate.damage_by_category.end(),
            destination.damage_by_category);
    }
    MemoryBarrier();
    InterlockedIncrement(&g_overlay_snapshot->sequence);
}

void ShutdownOverlay() {
    const Configuration config = ConfigurationSnapshot();
    PublishOverlaySnapshot(config, true);
    std::scoped_lock lock(g_overlay_mutex);
    if (g_overlay_process) {
        CloseHandle(g_overlay_process);
        g_overlay_process = nullptr;
    }
    if (g_overlay_snapshot) {
        UnmapViewOfFile(g_overlay_snapshot);
        g_overlay_snapshot = nullptr;
    }
    if (g_overlay_mapping) {
        CloseHandle(g_overlay_mapping);
        g_overlay_mapping = nullptr;
    }
}

void StartSession() {
    if (g_save_requested.load(std::memory_order_acquire)) {
        Log("[combat-session] previous result is still being saved");
        return;
    }
    std::scoped_lock lock(g_session_mutex);
    if (g_session_active.exchange(true)) return;
    g_session = {};
    g_session.id = static_cast<uint64_t>(
        std::chrono::steady_clock::now().time_since_epoch().count());
    g_modifier_samples.store(0, std::memory_order_relaxed);
    g_session.started = std::chrono::steady_clock::now();
    g_session.started_utc = std::chrono::system_clock::now();
    g_session.record_damage_baseline = g_record_damage_calls.load(
        std::memory_order_relaxed);
    g_session.record_detail_baseline = g_record_detail_calls.load(
        std::memory_order_relaxed);
    g_session.detail_init_baseline = g_detail_init_calls.load(
        std::memory_order_relaxed);
    g_session.damage_text_baseline = g_text_hits.load(
        std::memory_order_relaxed);
    Log("[combat-session] started");
}

void StopSession() {
    if (!g_session_active.exchange(false)) return;
    uint64_t record_damage_calls = 0;
    uint64_t record_detail_calls = 0;
    uint64_t detail_init_calls = 0;
    uint64_t damage_text_calls = 0;
    {
        std::scoped_lock lock(g_session_mutex);
        g_session.active = false;
        g_session.ended = std::chrono::steady_clock::now();
        g_session.record_damage_calls = g_record_damage_calls.load(
            std::memory_order_relaxed) - g_session.record_damage_baseline;
        g_session.record_detail_calls = g_record_detail_calls.load(
            std::memory_order_relaxed) - g_session.record_detail_baseline;
        g_session.detail_init_calls = g_detail_init_calls.load(
            std::memory_order_relaxed) - g_session.detail_init_baseline;
        g_session.damage_text_calls = g_text_hits.load(
            std::memory_order_relaxed) - g_session.damage_text_baseline;
        record_damage_calls = g_session.record_damage_calls;
        record_detail_calls = g_session.record_detail_calls;
        detail_init_calls = g_session.detail_init_calls;
        damage_text_calls = g_session.damage_text_calls;
    }
    g_save_requested.store(true, std::memory_order_release);
    g_queue_cv.notify_one();
    Log("[combat-diag] session route hits recordDamage=" +
        std::to_string(record_damage_calls) + " recordDamageDetail=" +
        std::to_string(record_detail_calls) + " damageDetailInit=" +
        std::to_string(detail_init_calls) + " damageText=" +
        std::to_string(damage_text_calls) + " queued=" +
        std::to_string(g_damage_hits.load(std::memory_order_relaxed)) +
        " dropped=" +
        std::to_string(g_damage_dropped.load(std::memory_order_relaxed)));
    Log("[combat-session] stop requested; result will be saved after queued hits");
}

bool KeyPressed(int vk, bool ctrl, bool& previous) {
    const bool down = (GetAsyncKeyState(vk) & 0x8000) != 0 &&
        (!ctrl || (GetAsyncKeyState(VK_CONTROL) & 0x8000) != 0);
    const bool pressed = down && !previous;
    previous = down;
    return pressed;
}

void PollHotkeys() {
    static bool toggle_previous = false;
    static bool overlay_previous = false;
    const Configuration config = ConfigurationSnapshot();
    if (!config.stats_enabled) return;
    if (KeyPressed(config.toggle_vk, config.toggle_ctrl, toggle_previous)) {
        if (g_session_active.load(std::memory_order_acquire)) StopSession();
        else StartSession();
    }
    if (config.overlay_enabled &&
        KeyPressed(config.overlay_vk, config.overlay_ctrl, overlay_previous)) {
        const bool visible = !g_overlay_visible.load(std::memory_order_relaxed);
        g_overlay_visible.store(visible, std::memory_order_relaxed);
        Log(std::string("[combat-overlay] visibility=") + (visible ? "shown" : "hidden"));
    }
}

void __fastcall DamageDetailInitHook(void* instance, void* modifier, void* method) {
    ++g_detail_init_calls;
    if (g_original_damage_init) g_original_damage_init(instance, modifier, method);
}

void __fastcall RecordDamageHook(void* instance, void* modifier, void* method) {
    ++g_record_damage_calls;
    if (g_original_record_damage) {
        g_original_record_damage(instance, modifier, method);
    }
    if (!modifier || !g_session_active.load(std::memory_order_acquire)) return;

    DamageEvent event{};
    int32_t shared_flags = 0;
    int32_t delta_type = 0;
    int64_t decorate_mask = 0;
    if (!ReadValueTypeField(modifier, g_modifier_value, event.damage) ||
        !ReadValueTypeField(modifier, g_modifier_real_delta, event.hp_delta) ||
        !ReadValueTypeField(modifier, g_modifier_delta_type, delta_type) ||
        !ReadValueTypeField(modifier, g_modifier_damage_type, event.damage_type) ||
        !ReadValueTypeField(modifier, g_modifier_damage_decorate_mask, decorate_mask) ||
        !ReadValueTypeField(modifier, g_modifier_shared_flags, shared_flags)) {
        return;
    }
    if (delta_type != 2 || !std::isfinite(event.damage) || !std::isfinite(event.hp_delta)) return;
    event.decorate_mask = static_cast<uint64_t>(decorate_mask);
    event.critical = (shared_flags & 1) != 0;
    const std::string attacker_text = ResolveAttackerName(modifier);
    const std::string skill_text = ResolveSkillId(modifier);
    std::snprintf(event.attacker, sizeof(event.attacker), "%s", attacker_text.c_str());
    std::snprintf(event.skill, sizeof(event.skill), "%s", skill_text.c_str());
    {
        std::scoped_lock lock(g_session_mutex);
        event.session_id = g_session.id;
        event.elapsed_seconds = std::max(0.0, std::chrono::duration<double>(
            std::chrono::steady_clock::now() - g_session.started).count());
    }

    const Configuration config = ConfigurationSnapshot();
    const uint32_t sample = g_modifier_samples.fetch_add(1, std::memory_order_relaxed);
    // Keep a small always-on sample while the fourth module is being validated;
    // diagnostics=true extends it to sixteen hits without creating per-hit spam.
    if (sample < (config.diagnostics ? 16u : 8u)) {
        std::ostringstream output;
        output << "[combat-capture] value=" << std::setprecision(10) << event.damage
            << " realDelta=" << event.hp_delta << " deltaType=" << delta_type
            << " damageType=" << event.damage_type << " flags=" << shared_flags
            << " attacker=\"" << attacker_text << "\" skill=\"" << skill_text << "\"";
        Log(output.str());
    }

    ++g_damage_hits;
    {
        std::scoped_lock lock(g_queue_mutex);
        if (g_queue.size() >= kMaxPendingEvents) {
            ++g_damage_dropped;
            return;
        }
        g_queue.push_back(event);
    }
    g_queue_cv.notify_one();
}

void __fastcall RecordDamageDetailHook(void* instance, void* modifier, void* method) {
    ++g_record_detail_calls;
    if (g_original_record_damage_detail) {
        g_original_record_damage_detail(instance, modifier, method);
    }
}

void __fastcall DamageTextV1Hook(void* instance, void* event_data, void* method) {
    ++g_text_hits;
    const Configuration config = ConfigurationSnapshot();
    if (config.hide_damage_numbers) return;
    if (g_original_damage_text_v1)
        g_original_damage_text_v1(instance, event_data, method);
}

void __fastcall DamageTextV2Hook(void* instance, void* event_data, void* method) {
    ++g_text_hits;
    const Configuration config = ConfigurationSnapshot();
    if (config.hide_damage_numbers) return;
    if (g_original_damage_text_v2)
        g_original_damage_text_v2(instance, event_data, method);
}

void __fastcall BattleTickHook(void* instance, float delta_time, void* method) {
    if (g_original_battle_tick) g_original_battle_tick(instance, delta_time, method);
    PollHotkeys();
    const Configuration config = ConfigurationSnapshot();
    const auto now = std::chrono::steady_clock::now();
    if (now - g_last_overlay_publish >= std::chrono::milliseconds(100)) {
        g_last_overlay_publish = now;
        PublishOverlaySnapshot(config);
    }
    if (config.stats_enabled && config.overlay_enabled &&
        now - g_last_overlay_launch_check >= std::chrono::seconds(2)) {
        g_last_overlay_launch_check = now;
        EnsureOverlayProcess();
    }
}

bool Hook(const RuntimeMethod& method, void* detour, void** original) {
    if (!method.pointer || !g_host || !g_host->create_hook) return false;
    const BE_Result status = g_host->create_hook(g_host->context, kModuleId,
        method.pointer, detour, original);
    if (status != BE_Result_Ok) {
        Log(std::string("[combat-hook] failed ") + (method.key ? method.key : "<unknown>") +
            " result=" + ResultName(status));
        return false;
    }
    Log(std::string("[combat-hook] installed ") + method.key);
    return true;
}

void InstallHooks(const Configuration& config) {
    if (config.hide_damage_numbers && !g_text_v1_hook_installed.load() &&
        g_damage_text_v1.pointer) {
        g_text_v1_hook_installed.store(Hook(g_damage_text_v1,
            reinterpret_cast<void*>(&DamageTextV1Hook),
            reinterpret_cast<void**>(&g_original_damage_text_v1)));
    }
    if (config.hide_damage_numbers && !g_text_v2_hook_installed.load() &&
        g_damage_text_v2.pointer) {
        g_text_v2_hook_installed.store(Hook(g_damage_text_v2,
            reinterpret_cast<void*>(&DamageTextV2Hook),
            reinterpret_cast<void**>(&g_original_damage_text_v2)));
    }
    if (config.stats_enabled && g_modifier_contract_ready &&
        !g_record_damage_hook_installed.load() &&
        g_record_damage.pointer) {
        g_record_damage_hook_installed.store(Hook(g_record_damage,
            reinterpret_cast<void*>(&RecordDamageHook),
            reinterpret_cast<void**>(&g_original_record_damage)));
    }
    if (config.stats_enabled && !g_record_detail_hook_installed.load() &&
        g_record_damage_detail.pointer) {
        g_record_detail_hook_installed.store(Hook(g_record_damage_detail,
            reinterpret_cast<void*>(&RecordDamageDetailHook),
            reinterpret_cast<void**>(&g_original_record_damage_detail)));
    }
    if (config.stats_enabled && g_damage_init.pointer && g_damage_init.method_info &&
        !g_damage_hook_installed.load()) {
        const bool fields_ready = g_attacker_id.resolved && g_damage_value.resolved &&
            g_hp_delta.resolved && g_damage_type.resolved && g_damage_decorate_mask.resolved &&
            g_origin_skill_id.resolved && g_is_critical.resolved;
        if (fields_ready) {
            g_damage_hook_installed.store(Hook(g_damage_init,
                reinterpret_cast<void*>(&DamageDetailInitHook),
                reinterpret_cast<void**>(&g_original_damage_init)));
        }
    }
    if (config.stats_enabled && g_battle_tick.pointer &&
        !g_tick_hook_installed.load()) {
        g_tick_hook_installed.store(Hook(g_battle_tick, reinterpret_cast<void*>(&BattleTickHook),
            reinterpret_cast<void**>(&g_original_battle_tick)));
    }
    g_hooks_installed.store(g_text_v1_hook_installed || g_text_v2_hook_installed ||
        g_damage_hook_installed ||
        g_record_damage_hook_installed || g_record_detail_hook_installed ||
        g_tick_hook_installed);
    g_state.store(g_hooks_installed ? ModuleState::Active : ModuleState::Ready);
}

void StopHooks() {
    if (g_host && g_host->release_module_hooks && g_hooks_installed.load())
        g_host->release_module_hooks(g_host->context, kModuleId);
    g_hooks_installed.store(false);
    g_text_v1_hook_installed.store(false);
    g_text_v2_hook_installed.store(false);
    g_damage_hook_installed.store(false);
    g_record_damage_hook_installed.store(false);
    g_record_detail_hook_installed.store(false);
    g_tick_hook_installed.store(false);
    g_original_damage_init = nullptr;
    g_original_record_damage = nullptr;
    g_original_record_damage_detail = nullptr;
    g_original_damage_text_v1 = nullptr;
    g_original_damage_text_v2 = nullptr;
    g_original_battle_tick = nullptr;
}

} // namespace

BE_Result BE_CALL Initialize(const BE_HostApiV1* host) {
    if (!host || host->abi_version != BETTER_ENDFIELD_MODULE_ABI_V1 ||
        !host->resolve_method || !host->resolve_field || !host->create_hook ||
        !host->release_module_hooks || !host->copy_managed_string || !host->log) {
        return BE_Result_ContractMismatch;
    }
    g_host = host;
    ResolveRuntimeContract();
    g_worker_stop.store(false);
    g_worker = std::thread(WorkerLoop);
    Log("[combat-module] initialized; awaiting configuration");
    return BE_Result_Ok;
}

BE_Result BE_CALL ConfigurationChanged(const char* text) {
    if (!g_host) return BE_Result_NotReady;
    Configuration next = ParseConfiguration(text);
    {
        std::scoped_lock lock(g_config_mutex);
        g_configuration = next;
    }
    Log(std::string("[combat-config] enabled=") + (next.enabled ? "true" : "false") +
        " stats=" + (next.stats_enabled ? "true" : "false") +
        " hide=" + (next.hide_damage_numbers ? "true" : "false") +
        " overlay=" + (next.overlay_enabled ? "true" : "false") +
        " toggle=" + std::to_string(next.toggle_vk) +
        " overlayHotkey=" + std::to_string(next.overlay_vk));
    if (!next.stats_enabled) {
        StopSession();
    }
    if (!next.enabled && !next.hide_damage_numbers) {
        StopHooks();
        g_state.store(ModuleState::Disabled);
    } else {
        InstallHooks(next);
    }
    PublishOverlaySnapshot(next);
    if (next.stats_enabled && next.overlay_enabled) EnsureOverlayProcess();
    return BE_Result_Ok;
}

void BE_CALL Shutdown() {
    StopSession();
    ShutdownOverlay();
    StopHooks();
    g_worker_stop.store(true, std::memory_order_release);
    g_queue_cv.notify_all();
    if (g_worker.joinable()) g_worker.join();
    g_state.store(ModuleState::Stopped);
    g_host = nullptr;
}

const BE_ModuleApiV1 kApi{
    {kModuleId, "Combat Statistics", "2.2.1", BETTER_ENDFIELD_MODULE_ABI_V1},
    &Initialize,
    &ConfigurationChanged,
    &Shutdown};

} // namespace BetterEndfield::CombatStats

BE_EXPORT const BE_ModuleApiV1* BE_CALL BetterEndfield_GetModuleApiV1() {
    return &BetterEndfield::CombatStats::kApi;
}
