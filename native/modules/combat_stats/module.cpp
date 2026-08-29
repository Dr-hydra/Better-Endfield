#include <BetterEndfield/ModuleApi.h>

#include "combat_overlay_protocol.h"
#include "combat_semantics.h"
#include "rdps_math.h"

#include <Windows.h>
#include <shellapi.h>

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
#include <limits>
#include <map>
#include <memory>
#include <mutex>
#include <optional>
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
constexpr size_t kMaxBuffIntervals = 8192;
constexpr size_t kMaxRdpsModifiersPerHit = 32;
constexpr size_t kMaxRdpsSourcesPerHit = 16;
constexpr int32_t kAttributeTypeCount = 101;
constexpr size_t kAttributeModifierValueCount = 9;

enum class ModuleState : uint8_t {
    Created,
    Resolving,
    Ready,
    Active,
    Disabled,
    Stopped,
};

enum class RdpsContributionKind : uint8_t {
    DirectDamage = 0,
    Attack = 1,
    DamageIncrease = 2,
    Amplification = 3,
    Fragile = 4,
    VulnerabilityTaken = 5,
    Resistance = 6,
    ArtsStrength = 7,
    Other = 8,
};

struct DynamicCategoryMeta {
    const char* id;
    const char* name;
    uint32_t color_argb;
};

inline constexpr std::array<DynamicCategoryMeta, 9> kDynamicRdpsCategories{{
    {"direct_damage", "直伤", 0xFFD3D8E1},
    {"attack", "攻击力", 0xFFFFCE52},
    {"damage_increase", "增伤", 0xFF43C9FF},
    {"amplification", "增幅", 0xFFFF9148},
    {"fragile", "脆弱", 0xFFFF7A67},
    {"vulnerability_taken", "承伤易伤", 0xFFFF4F82},
    {"resistance", "减防/减抗", 0xFF57D99B},
    {"arts_strength", "法术强度", 0xFF54B3FF},
    {"other", "其他", 0xFF8F98AA},
}};

enum class BuffEffectKind : uint8_t {
    None = 0,
    MultiplierDelta = 1,
    DamageShare = 2,
};

constexpr size_t RdpsContributionIndex(RdpsContributionKind kind) {
    const size_t index = static_cast<size_t>(kind);
    return index < CombatOverlayProtocol::kDisplaySegmentCount ? index : (CombatOverlayProtocol::kDisplaySegmentCount - 1);
}

RdpsContributionKind ContributionKindForZone(SemanticZone zone) {
    switch (zone) {
    case SemanticZone::Attack: return RdpsContributionKind::Attack;
    case SemanticZone::DamageIncrease: return RdpsContributionKind::DamageIncrease;
    case SemanticZone::Amplification: return RdpsContributionKind::Amplification;
    case SemanticZone::Fragile: return RdpsContributionKind::Fragile;
    case SemanticZone::VulnerabilityTaken:
        return RdpsContributionKind::VulnerabilityTaken;
    case SemanticZone::Resistance: return RdpsContributionKind::Resistance;
    case SemanticZone::Combo: return RdpsContributionKind::DamageIncrease;
    case SemanticZone::ArtsStrength: return RdpsContributionKind::ArtsStrength;
    default: return RdpsContributionKind::Other;
    }
}

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
    bool diagnostics = false;
    bool rdps_display = false;
    bool overlay_enabled = true;
    bool auto_dungeon_session = true;
    int toggle_vk = VK_F11;
    int overlay_vk = VK_F12;
    bool toggle_ctrl = false;
    bool overlay_ctrl = false;
};

struct RdpsShare {
    double fraction = 0.0;
    RdpsContributionKind kind = RdpsContributionKind::Other;
    char source[160]{};
};

struct DamageEvent {
    uint64_t session_id = 0;
    uint64_t action_id = 0;
    double elapsed_seconds = 0.0;
    double damage = 0.0;
    double hp_delta = 0.0;
    int32_t damage_type = 0;
    uint64_t decorate_mask = 0;
    bool critical = false;
    uint8_t rdps_share_count = 0;
    char attacker[160]{};
    char skill[160]{};
    std::array<RdpsShare, kMaxRdpsSourcesPerHit> rdps_shares{};
};

struct CombatAction {
    uint64_t id = 0;
    double start_time = 0.0;
    double end_time = 0.0;
    uint64_t parent_action_id = 0;
    void* runtime_skill = nullptr;
    std::string actor;
    std::string skill;
    std::string type;
    int32_t finish_type = 0;
    int32_t interrupt_reason = 0;
    bool active = true;
    bool cancelled = false;
    bool end_observed = false;
    bool inferred_end = false;
    bool inferred = false;
};

struct BuffDiagnosticInfo {
    struct AttributeContribution {
        int32_t attribute_type = -1;
        double addition = 0.0;
        double multiplier = 0.0;
        double final_addition = 0.0;
        double final_scalar = 1.0;
        double base_addition = 0.0;
        double base_multiplier = 0.0;
        double base_final_addition = 0.0;
        double base_final_scalar = 1.0;
    };

    uint64_t inst_id = 0;
    std::string buff_id;
    std::string source;
    std::string owner;
    std::string origin_skill;
    std::string credited_actor_id;
    std::string source_kind;
    std::string source_template_id;
    std::string source_skill_id;
    std::string actor_attribution = "unknown";
    std::string source_attribution = "unknown";
    std::string attribution_reason;
    void* source_ability = nullptr;
    void* owner_ability = nullptr;
    void* blackboard = nullptr;
    void* buff_data = nullptr;
    std::vector<void*> damage_modifiers;
    double arts_strength_points = 0.0;
    bool has_arts_strength_points = false;
    std::vector<AttributeContribution> attribute_contributions;
    double start_time = 0.0;
    double end_time = 0.0;
    bool is_active = true;
    int32_t finish_reason = 0;
    bool is_relevant_for_rdps = false;
};

using BuffRuntimeState = BuffDiagnosticInfo;

struct Aggregate {
    double damage = 0.0;
    uint64_t hits = 0;
    uint64_t critical_hits = 0;
};

struct CharacterAggregate : Aggregate {
    std::array<double, CombatOverlayProtocol::kDisplaySegmentCount> damage_by_category{};
    std::array<double, CombatOverlayProtocol::kDisplaySegmentCount> rdps_by_contribution{};
};

struct TimelineBucket {
    std::array<double, CombatOverlayProtocol::kDisplaySegmentCount> damage_by_category{};
    std::array<double, CombatOverlayProtocol::kDisplaySegmentCount> rdps_by_contribution{};
    std::map<std::string, double> damage_by_character;
    std::map<std::string, double> rdps_by_character;
};

struct BuffInterval {
    uint64_t inst_id = 0;
    uint64_t action_id = 0;
    double start_time = 0.0;
    double end_time = 0.0;
    bool relevant = false;
    bool active = true;
    RdpsContributionKind kind = RdpsContributionKind::Other;
    BuffEffectKind effect_kind = BuffEffectKind::None;
    bool effect_observed = false;
    double effect_min = 0.0;
    double effect_max = 0.0;
    std::string buff_id;
    std::string source;
    std::string owner;
    std::string origin_skill;
    std::string credited_actor_id;
    std::string source_kind;
    std::string source_template_id;
    std::string source_skill_id;
    std::string actor_attribution = "unknown";
    std::string source_attribution = "unknown";
    std::string attribution_reason;
};

struct Session {
    struct SemanticCoverage {
        uint64_t observed = 0;
        uint64_t verified = 0;
        uint64_t candidate = 0;
        uint64_t excluded = 0;
        uint64_t unknown = 0;
        uint64_t element_mismatch = 0;
        uint64_t unresolved_dropped = 0;
    };

    struct SemanticAuditItem {
        std::string buff_id;
        SemanticStatus status = SemanticStatus::Unknown;
        SemanticZone observed_zone = SemanticZone::Unknown;
        uint64_t count = 0;
        bool element_mismatch = false;
    };

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
    uint64_t action_start_calls = 0;
    uint64_t action_end_calls = 0;
    uint64_t action_end_matches = 0;
    uint64_t dodge_calls = 0;
    uint64_t record_damage_baseline = 0;
    uint64_t record_detail_baseline = 0;
    uint64_t detail_init_baseline = 0;
    uint64_t damage_text_baseline = 0;
    uint32_t semantic_catalog_schema = 0;
    std::string semantic_game_version;
    std::string semantic_hotfix_version;
    std::string semantic_source_sha256;
    SemanticCoverage semantic_coverage{};
    std::vector<SemanticAuditItem> unresolved_semantics;
    std::map<std::string, CharacterAggregate> characters;
    std::map<std::string, CharacterAggregate> rdps_characters;
    std::map<std::string, Aggregate> skills;
    std::map<std::string, Aggregate> damage_categories;
    std::map<uint32_t, TimelineBucket> timeline;
    std::vector<BuffInterval> buff_intervals;
    std::unordered_map<uint64_t, size_t> open_buff_intervals;
    std::vector<DamageEvent> raw_events;
    std::vector<CombatAction> actions;
    std::unordered_map<void*, size_t> open_actions;
    uint64_t next_action_id = 1;
    uint64_t next_effect_id = 1;
    std::chrono::system_clock::time_point started_utc{};
    std::string dungeon_id;
    std::string mode_id;

    struct SquadMember {
        struct SkillLevel {
            std::string id;
            int32_t level = 0;
            int32_t max_level = 0;
        };
        struct Weapon {
            std::string template_id;
            int32_t level = 0;
            int32_t refine_level = 0;
            int32_t breakthrough_level = 0;
            std::vector<SkillLevel> skills;
        };
        struct Equipment {
            int32_t slot = 0;
            std::string template_id;
            std::map<int32_t, int32_t> enhance_levels;
        };
        struct EquipSuit {
            std::string suit_id;
            int32_t count = 0;
            std::vector<SkillLevel> skills;
        };
        std::string char_id;
        uint64_t char_inst_id = 0;
        int32_t level = 0;
        int32_t break_stage = 0;
        uint32_t potential = 0;
        std::vector<SkillLevel> skill_groups;
        std::optional<Weapon> weapon;
        std::vector<Equipment> equipments;
        std::vector<EquipSuit> equip_suits;
    };
    std::vector<SquadMember> squad;

    struct StaggerInterval {
        uint64_t inst_id = 0;
        double start_time = 0.0;
        double end_time = 0.0;
        std::string target_id;
        std::string buff_id;
        bool active = true;
    };
    std::vector<StaggerInterval> stagger_intervals;
    std::unordered_map<uint64_t, size_t> open_stagger_intervals;
};

using DamageInitFn = void(__fastcall*)(void* instance, void* modifier, void* method);
using RecordDamageFn = void(__fastcall*)(void* instance, void* modifier, void* method);
using HpChangedFn = void(__fastcall*)(void* instance, void* event_data, void* method);
using BattleTickFn = void(__fastcall*)(void* instance, float delta_time, void* method);
using BuffRecordStartFn = void(__fastcall*)(void* instance, void* buff, void* method);
using BuffRecordFinishFn = void(__fastcall*)(
    void* instance, void* buff, int32_t reason, void* method);
using RecordCastSkillFn = void(__fastcall*)(
    void* instance, void* caster, void* skill, void* method);
using RecordEndSkillFn = void(__fastcall*)(void* instance, void* skill,
    int32_t finish_type, int32_t interrupt_reason, bool is_cancelled, void* method);
using RecordDodgeSuccessFn = void(__fastcall*)(
    void* instance, void* source, void* method);
// Nullable<InterruptContext> exceeds 8 bytes, so the Windows x64 ABI passes
// it by reference; forwarding the register as an opaque pointer is exact.
using SkillCastEndFn = void(__fastcall*)(void* skill, int32_t finish_type,
    int32_t interrupt_reason, void* interrupt_context, void* method);
using UInt64GetterFn = uint64_t(__fastcall*)(void* instance, void* method);
using BoolGetterFn = bool(__fastcall*)(void* instance, void* method);
using ObjectGetterFn = void*(__fastcall*)(void* instance, void* method);
using EquipGetterFn = void*(__fastcall*)(
    void* instance, int32_t slot, void* method);
using DamageModifierProcessFn = void(__fastcall*)(void* instance, int32_t timing,
    void** modifier_handle, void* pack_data, void* method);
using ModifyDamageScaleZoneFn = void(__fastcall*)(void* pack_data, int32_t side,
    void* zone_name, double value, void* method);
using ApplyDamageModifierFn = void(__fastcall*)(
    void* pack_data, int32_t timing, void* method);
using CalculateDamageFn = double(__fastcall*)(void* pack_data, bool* is_critical,
    bool* is_blocked, void* report_info, void* method);
using InstantModifyAttributeFn = void(__fastcall*)(void* instance, void* pack_data,
    void* blackboard, void* context, void* server_args, void* method);
using DamageProcessorProcessFn = void(__fastcall*)(void* instance, int32_t timing,
    void* pack_data, void* blackboard, void* context, void* server_args, void* method);
using BlackboardDoubleGetValueFn = double(__fastcall*)(
    void* instance, void* blackboard, void* method);
using ActionBlackboardTryGetDoubleFn = bool(__fastcall*)(
    void* instance, void* key, double* value, void* method);
using BuffAttributeGetValueFn = bool(__fastcall*)(void* instance,
    int32_t attribute_type, double* addition, double* multiplier,
    double* final_addition, double* final_scalar, void* method);
using BuffBlackboardChangedFn = void(__fastcall*)(void* instance, void* method);
using BuffModifyAttributesFn = void(__fastcall*)(void* instance, void* input_data,
    void* input_blackboard, int32_t enhance_count, void* method);
using AttributeValuesFn = void*(__fastcall*)(
    void* instance, int32_t attribute_type, void* method);
using CalculateFinalAttributeFn = double(__fastcall*)(double raw_value,
    double addition, double multiplier, double final_addition,
    double final_multiplier, double base_addition, double base_multiplier,
    double base_final_addition, double base_final_multiplier, double minimum,
    double maximum, void* method);
using PackDoubleFn = double(__fastcall*)(void* pack_data, void* method);
using StaticObjectGetterFn = void*(__fastcall*)(void* method);
using CharInfoGetterFn = void*(__fastcall*)(void* instance, uint64_t char_id,
    void* method);
using ObjectIntGetterFn = void*(__fastcall*)(void* instance, int32_t index,
    void* method);

const BE_HostApiV1* g_host = nullptr;
std::atomic<ModuleState> g_state{ModuleState::Created};
std::atomic_bool g_hooks_installed{false};
std::atomic_bool g_damage_hook_installed{false};
std::atomic_bool g_record_damage_hook_installed{false};
std::atomic_bool g_record_detail_hook_installed{false};
std::atomic_bool g_text_v1_hook_installed{false};
std::atomic_bool g_text_v2_hook_installed{false};
std::atomic_bool g_tick_hook_installed{false};
std::atomic_bool g_buff_start_hook_installed{false};
std::atomic_bool g_buff_finish_hook_installed{false};
std::atomic_bool g_cast_skill_hook_installed{false};
std::atomic_bool g_end_skill_hook_installed{false};
std::atomic_bool g_dodge_success_hook_installed{false};
std::atomic_bool g_skill_cast_end_hook_installed{false};
std::atomic_bool g_buff_blackboard_hook_installed{false};
std::atomic_bool g_buff_modify_attributes_hook_installed{false};
std::atomic_bool g_damage_modifier_process_hook_installed{false};
std::atomic_bool g_damage_scale_zone_hook_installed{false};
std::atomic_bool g_apply_damage_modifier_hook_installed{false};
std::atomic_bool g_calculate_damage_hook_installed{false};
std::atomic_bool g_instant_attribute_hook_installed{false};
std::atomic_bool g_damage_processor_hook_installed{false};
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
std::atomic<uint32_t> g_rdps_damage_samples{0};
std::atomic<uint32_t> g_rdps_buff_samples{0};
std::atomic<uint32_t> g_rdps_value_samples{0};
std::atomic<uint32_t> g_rdps_flow_samples{0};
std::atomic<uint32_t> g_rdps_processor_samples{0};
std::atomic<uint32_t> g_rdps_attribute_share_samples{0};
std::atomic<uint64_t> g_semantic_observed{0};
std::atomic<uint64_t> g_semantic_verified{0};
std::atomic<uint64_t> g_semantic_candidate{0};
std::atomic<uint64_t> g_semantic_excluded{0};
std::atomic<uint64_t> g_semantic_unknown{0};
std::atomic<uint64_t> g_semantic_element_mismatch{0};
std::atomic<uint64_t> g_semantic_unresolved_dropped{0};
std::atomic<uint64_t> g_rdps_flow_sequence{0};
std::atomic<uint64_t> g_rdps_flow_generation{0};
std::atomic_bool g_overlay_visible{true};

std::mutex g_config_mutex;
Configuration g_configuration;
std::mutex g_semantic_catalog_mutex;
std::shared_ptr<const CombatSemanticCatalog> g_semantic_catalog;
std::filesystem::path g_semantic_catalog_path;
std::mutex g_queue_mutex;
std::condition_variable g_queue_cv;
std::deque<DamageEvent> g_queue;
std::thread g_worker;
std::thread g_overlay_worker;
std::atomic_bool g_overlay_worker_stop{false};
std::atomic_bool g_overlay_runtime_ready{false};
std::mutex g_overlay_wait_mutex;
std::condition_variable g_overlay_wait_cv;
std::mutex g_session_mutex;
Session g_session;
std::mutex g_overlay_mutex;
HANDLE g_overlay_mapping = nullptr;
CombatOverlayProtocol::SharedSnapshot* g_overlay_snapshot = nullptr;
HANDLE g_overlay_process = nullptr;
std::mutex g_buff_diagnostic_mutex;
std::unordered_map<uint64_t, BuffDiagnosticInfo> g_buff_diagnostic_map;
std::unordered_map<void*, uint64_t> g_modifier_to_buff_map;
std::mutex g_managed_key_mutex;
struct ManagedKeyString {
    void* object = nullptr;
    uint32_t gc_handle = 0;
};
std::unordered_map<std::string, ManagedKeyString> g_managed_key_strings;
std::mutex g_semantic_audit_mutex;
std::map<std::string, Session::SemanticAuditItem> g_semantic_audit;

void Log(const std::string& message);

std::filesystem::path ModuleDirectory() {
    HMODULE module = nullptr;
    if (!GetModuleHandleExW(
            GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS |
                GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
            reinterpret_cast<LPCWSTR>(&g_semantic_catalog), &module) || !module) {
        return {};
    }
    std::wstring buffer(32768, L'\0');
    const DWORD length = GetModuleFileNameW(
        module, buffer.data(), static_cast<DWORD>(buffer.size()));
    if (!length || length >= buffer.size()) return {};
    buffer.resize(length);
    return std::filesystem::path(buffer).parent_path();
}

std::shared_ptr<const CombatSemanticCatalog> SemanticCatalogSnapshot() {
    std::scoped_lock lock(g_semantic_catalog_mutex);
    return g_semantic_catalog;
}

bool LoadCombatSemanticCatalog() {
    std::vector<std::filesystem::path> candidates;
    const std::filesystem::path module_directory = ModuleDirectory();
    if (!module_directory.empty()) {
        candidates.push_back(module_directory / L"combat-semantics.besem");
    }

    std::string last_error = "catalogue was not found";
    for (const std::filesystem::path& candidate : candidates) {
        std::error_code exists_error;
        if (!std::filesystem::is_regular_file(candidate, exists_error)) continue;
        auto catalog = std::make_shared<CombatSemanticCatalog>();
        std::string error;
        if (!catalog->Load(candidate, error)) {
            last_error = error;
            continue;
        }
        const std::filesystem::path source_map =
            candidate.parent_path() / L"buff-sources.bemap";
        std::string source_error;
        if (!catalog->LoadSourceMap(source_map, source_error)) {
            Log("[combat-sources] unavailable: " + source_error);
        }
        const SemanticCatalogSummary& summary = catalog->Summary();
        {
            std::scoped_lock lock(g_semantic_catalog_mutex);
            g_semantic_catalog = catalog;
            g_semantic_catalog_path = candidate;
        }
        Log("[combat-semantics] loaded schema=" +
            std::to_string(summary.schema_version) + " game=" +
            summary.game_version + " hotfix=" + summary.hotfix_version +
            " buffs=" + std::to_string(summary.buff_count) + " effects=" +
            std::to_string(summary.effect_count) + " excluded=" +
            std::to_string(summary.excluded_count) + " aliases=" +
            std::to_string(summary.buff_alias_count) + " skills=" +
            std::to_string(summary.skill_alias_count) + " dynamic=" +
            std::to_string(summary.dynamic_count) + " path=\"" +
            candidate.string() + "\"");
        Log("[combat-sources] mappedBuffs=" +
            std::to_string(catalog->SourceMappedBuffCount()) +
            " candidates=" +
            std::to_string(catalog->SourceCandidateCount()) +
            " sourceSha256=" + catalog->SourceMapSha256() +
            " path=\"" + source_map.string() + "\"");
        return true;
    }
    {
        std::scoped_lock lock(g_semantic_catalog_mutex);
        g_semantic_catalog.reset();
        g_semantic_catalog_path.clear();
    }
    Log("[combat-semantics] unavailable: " + last_error);
    return false;
}

RuntimeMethod g_damage_init;
RuntimeMethod g_record_damage;
RuntimeMethod g_record_damage_detail;
RuntimeMethod g_damage_text_v1;
RuntimeMethod g_damage_text_v2;
RuntimeMethod g_battle_tick;
RuntimeMethod g_record_buff_start;
RuntimeMethod g_record_buff_finish;
RuntimeMethod g_record_cast_skill;
RuntimeMethod g_record_end_skill;
RuntimeMethod g_record_dodge_success;
RuntimeMethod g_skill_is_casting;
RuntimeMethod g_skill_cast_end;
RuntimeMethod g_buff_inst_id_getter;
RuntimeMethod g_buff_attribute_get_value;
RuntimeMethod g_buff_attribute_get_base_value;
RuntimeMethod g_buff_blackboard_value_change;
RuntimeMethod g_buff_modify_attributes;
RuntimeMethod g_ability_attributes_getter;
RuntimeMethod g_attributes_get_all_modifier_values;
RuntimeMethod g_calculate_final_attribute_value;
RuntimeMethod g_get_final_damage_scale;
RuntimeMethod g_get_def_resistance_value;
RuntimeMethod g_get_damage_type_resistance_value;
RuntimeMethod g_damage_pack_server_detail_getter;
RuntimeMethod g_damage_modifier_process;
RuntimeMethod g_damage_scale_zone_modify;
RuntimeMethod g_apply_damage_modifier;
RuntimeMethod g_calculate_damage;
RuntimeMethod g_instant_attribute_process;
RuntimeMethod g_damage_processor_process;
RuntimeMethod g_blackboard_double_get_value;
RuntimeMethod g_action_blackboard_try_get_double;
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
RuntimeField g_ability_source;
RuntimeField g_entity_name;
RuntimeField g_skill_id;
RuntimeField g_modifier_damage_pack;
RuntimeField g_server_detail_modifier_args;
RuntimeField g_server_detail_processor_debug_args;
RuntimeField g_modifier_args_attacker_modifiers;
RuntimeField g_modifier_args_defender_modifiers;
RuntimeField g_modifier_handle_buff_inst_id;
RuntimeField g_modifier_handle_index;
RuntimeField g_modifier_handle_processor_debug_args;
RuntimeField g_buff_id;
RuntimeField g_buff_owner;
RuntimeField g_buff_source;
RuntimeField g_buff_skill_cast_info;
RuntimeField g_buff_attribute_mask;
RuntimeField g_buff_blackboard;
RuntimeField g_buff_damage_modifiers;
RuntimeField g_buff_data;
RuntimeField g_damage_modifier_owner;
RuntimeField g_damage_modifier_data;
RuntimeField g_pack_calc_result;
RuntimeField g_pack_atk_scale;
RuntimeField g_pack_attacker_damage_scale_zones;
RuntimeField g_pack_attacker_attributes;
RuntimeField g_pack_defender_damage_scale_zones;
RuntimeField g_pack_defender_attributes;
RuntimeField g_pack_attacker;
RuntimeField g_pack_defender;
RuntimeField g_pack_skill_cast_info;
RuntimeField g_pack_damage_type;
RuntimeField g_pack_damage_decorate_mask;
RuntimeField g_attributes_data_min;
RuntimeField g_attributes_data_max;
RuntimeField g_calc_result_value;
RuntimeField g_instant_attribute_target_side;
RuntimeField g_instant_attribute_modifier;
RuntimeField g_attribute_modifier_modify_type;
RuntimeField g_attribute_modifier_attribute_type;
RuntimeField g_attribute_modifier_formula_item;
RuntimeField g_attribute_modifier_param;
RuntimeMethod g_game_instance_getter;
RuntimeMethod g_char_info_getter;
RuntimeMethod g_char_info_get_weapon;
RuntimeMethod g_char_info_get_equip;
RuntimeMethod g_server_char_info_get_weapon;
RuntimeMethod g_server_char_info_get_equip;
// Il2CppClass pointers used to dispatch CharInfo virtual overrides by the
// instance's actual runtime class instead of assuming ClientCharInfo.
const void* g_client_char_info_class = nullptr;
const void* g_server_char_info_class = nullptr;
RuntimeField g_game_instance_player;
RuntimeField g_game_player_squad_manager;
RuntimeField g_game_player_char_bag;
RuntimeField g_squad_manager_cur_squad;
RuntimeField g_squad_slots;
RuntimeField g_slot_char_inst_id;
RuntimeField g_slot_char_id;
RuntimeField g_char_info_potential_level;
RuntimeField g_char_info_level;
RuntimeField g_char_info_break_stage;
RuntimeField g_char_info_skill_groups;
RuntimeField g_char_info_equip_suit;
RuntimeField g_skill_group_level;
RuntimeField g_skill_group_max_level;
RuntimeField g_skill_group_id;
RuntimeField g_item_inst_template_id;
RuntimeField g_weapon_level;
RuntimeField g_weapon_refine_level;
RuntimeField g_weapon_breakthrough_level;
RuntimeField g_equip_enhance_levels;
RuntimeField g_game_player_dungeon_manager;
RuntimeField g_dungeon_manager_cur_dungeon_id;
RuntimeField g_dungeon_manager_cur_dungeon_id_fallback;
RuntimeField g_dungeon_manager_cur_mode_id;
RuntimeField g_battle_recorder_battle_info;
RuntimeField g_battle_info_mode_id;
RuntimeField g_battle_info_dungeon_id;
RuntimeField g_battle_info_battle_id;
bool g_modifier_contract_ready = false;
bool g_rdps_diagnostic_contract_ready = false;
bool g_buff_diagnostic_contract_ready = false;
bool g_buff_attribute_contract_ready = false;
bool g_rdps_value_contract_ready = false;
bool g_rdps_flow_contract_ready = false;
bool g_instant_attribute_contract_ready = false;
bool g_arts_strength_contract_ready = false;
bool g_squad_contract_ready = false;
bool g_dungeon_contract_ready = false;
std::atomic_bool g_squad_snapshot_logged{false};

DamageInitFn g_original_damage_init = nullptr;
RecordDamageFn g_original_record_damage = nullptr;
RecordDamageFn g_original_record_damage_detail = nullptr;
HpChangedFn g_original_damage_text_v1 = nullptr;
HpChangedFn g_original_damage_text_v2 = nullptr;
BattleTickFn g_original_battle_tick = nullptr;
BuffRecordStartFn g_original_record_buff_start = nullptr;
BuffRecordFinishFn g_original_record_buff_finish = nullptr;
RecordCastSkillFn g_original_record_cast_skill = nullptr;
RecordEndSkillFn g_original_record_end_skill = nullptr;
RecordDodgeSuccessFn g_original_record_dodge_success = nullptr;
SkillCastEndFn g_original_skill_cast_end = nullptr;
BuffBlackboardChangedFn g_original_buff_blackboard_value_change = nullptr;
BuffModifyAttributesFn g_original_buff_modify_attributes = nullptr;
DamageModifierProcessFn g_original_damage_modifier_process = nullptr;
ModifyDamageScaleZoneFn g_original_damage_scale_zone_modify = nullptr;
ApplyDamageModifierFn g_original_apply_damage_modifier = nullptr;
CalculateDamageFn g_original_calculate_damage = nullptr;
InstantModifyAttributeFn g_original_instant_attribute_process = nullptr;
DamageProcessorProcessFn g_original_damage_processor_process = nullptr;

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
    result.diagnostics = ParseBool(values, "diagnostics", false);
    result.rdps_display = ParseBool(values, "rdps_display", false);
    result.overlay_enabled = ParseBool(values, "overlay_enabled", true);
    result.auto_dungeon_session = ParseBool(values, "auto_dungeon_session", true);
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

bool ResolveMethodExact(RuntimeMethod& output, const char* key,
    const char* assembly, const char* namespc, const char* klass,
    const char* method, const char* parameter_types,
    uint32_t parameter_count, const char* return_type) {
    output = {};
    output.key = key;
    BE_MethodDescriptorV1 descriptor{assembly, namespc, klass, method,
        parameter_types, return_type, parameter_count};
    BE_ResolvedMethodV1 result{};
    const BE_Result status = g_host->resolve_method(
        g_host->context, &descriptor, &result);
    if (status == BE_Result_Ok && result.method_info && result.method_pointer) {
        output.pointer = result.method_pointer;
        output.method_info = result.method_info;
        Log(std::string("[combat-contract] resolved ") + key);
        return true;
    }
    Log(std::string("[combat-contract] missing ") + key +
        " result=" + ResultName(status));
    return false;
}

const void* ResolveClassInfo(const char* key, const char* assembly,
    const char* namespc, const char* klass) {
    if (!g_host || !g_host->resolve_class) {
        Log(std::string("[combat-contract] missing class ") + key +
            " result=host-api-unavailable");
        return nullptr;
    }
    BE_ResolvedClassV1 result{};
    const BE_Result status = g_host->resolve_class(
        g_host->context, assembly, namespc, klass, &result);
    if (status == BE_Result_Ok && result.class_info) {
        Log(std::string("[combat-contract] resolved class ") + key);
        return result.class_info;
    }
    Log(std::string("[combat-contract] missing class ") + key +
        " result=" + ResultName(status));
    return nullptr;
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

bool ResolveFieldWithOffsetFallback(RuntimeField& output, const char* key,
    const char* assembly, const char* namespc, const char* klass,
    const char* field, int32_t fallback_offset) {
    if (ResolveField(output, key, assembly, namespc, klass, field)) {
        return true;
    }
    output.resolved = fallback_offset >= 0;
    output.value.field_info = nullptr;
    output.value.offset = fallback_offset;
    if (output.resolved) {
        Log(std::string("[combat-contract] using schema-9 field offset ") + key +
            " offset=" + std::to_string(fallback_offset));
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
    ResolveMethod(g_record_buff_start, "battle.recorder.record-buff-start",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core",
        "BattleManager.BattleRecorder", "RecordBuffStart", 1, "System.Void");
    ResolveMethod(g_record_buff_finish, "battle.recorder.record-buff-finish",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core",
        "BattleManager.BattleRecorder", "RecordBuffFinish", 2, "System.Void");
    ResolveMethodExact(g_record_cast_skill, "battle.recorder.record-cast-skill",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core",
        "BattleManager.BattleRecorder", "RecordCastSkill",
        "Beyond.Gameplay.Core.AbilitySystem|Beyond.Gameplay.Core.Skill", 2,
        "System.Void");
    ResolveMethodExact(g_record_end_skill, "battle.recorder.record-end-skill",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core",
        "BattleManager.BattleRecorder", "RecordEndSkill",
        "Beyond.Gameplay.Core.Skill|Beyond.Gameplay.Core.Skill.FinishType|Beyond.Gameplay.Core.Skill.InterruptReason|System.Boolean",
        4, "System.Void");
    ResolveMethodExact(g_record_dodge_success,
        "battle.recorder.record-dodge-success", "Gameplay.Beyond.dll",
        "Beyond.Gameplay.Core", "BattleManager.BattleRecorder",
        "RecordDodgeSuccess", "Beyond.Gameplay.Core.AbilitySystem", 1,
        "System.Void");
    ResolveMethod(g_skill_is_casting, "skill.is-casting",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core", "Skill",
        "get_isCasting", 0, "System.Boolean");
    // Skill.CastEnd is the canonical end of every cast. BattleRecorder's
    // RecordEndSkill exists but is never invoked by the live game, so action
    // close events must come from the skill itself.
    ResolveMethod(g_skill_cast_end, "skill.cast-end",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core", "Skill",
        "CastEnd", 3, "System.Void");
    ResolveMethod(g_buff_inst_id_getter, "buff.get-inst-id",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core", "Buff", "get_buffInstId", 0,
        "System.UInt64");
    ResolveMethod(g_buff_attribute_get_value, "buff.get-attribute-value",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core", "Buff", "GetValue", 5,
        "System.Boolean");
    ResolveMethod(g_buff_attribute_get_base_value, "buff.get-base-attribute-value",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core", "Buff", "GetBaseValue", 5,
        "System.Boolean");
    ResolveMethod(g_buff_blackboard_value_change, "buff.blackboard-value-change",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core", "Buff",
        "OnBlackboardValueChange", 0, "System.Void");
    ResolveMethod(g_buff_modify_attributes, "buff.modify-attributes",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core", "Buff",
        "_ModifyAttributesModifier", 3, "System.Void");
    ResolveMethod(g_ability_attributes_getter, "ability.get-attributes",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core", "AbilitySystem",
        "get_attributes", 0, "Beyond.Gameplay.Core.Attributes");
    ResolveMethod(g_attributes_get_all_modifier_values,
        "attributes.get-all-modifier-values", "Gameplay.Beyond.dll",
        "Beyond.Gameplay.Core", "Attributes", "GetAllModifierValues", 1,
        "System.Double[]");
    ResolveMethod(g_calculate_final_attribute_value,
        "attributes-calculator.calculate-final", "Gameplay.Beyond.dll",
        "Beyond.Gameplay", "AttributesCalculator", "_CalculateFinalAttribute", 11,
        "System.Double");
    ResolveMethod(g_get_final_damage_scale, "damage-pack.get-final-damage-scale",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core", "DamagePackData",
        "_GetFinalDamageScale", 0, "System.Double");
    ResolveMethod(g_get_def_resistance_value, "battle-formula.get-def-resistance",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core", "BattleFormula",
        "_GetDefResistanceValue", 1, "System.Double");
    ResolveMethod(g_get_damage_type_resistance_value,
        "battle-formula.get-damage-type-resistance", "Gameplay.Beyond.dll",
        "Beyond.Gameplay.Core", "BattleFormula", "_GetDamageTypeResistanceValue", 1,
        "System.Double");
    ResolveMethod(g_damage_pack_server_detail_getter, "damage-pack.get-server-detail",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core", "DamagePackData",
        "get_serverDetail", 0, "Proto.BATTLE_DAMAGE_DETAIL");
    ResolveMethod(g_damage_modifier_process, "damage-modifier.process",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core", "DamageModifier",
        "_DoProcessDamagePackData", 3, "System.Void");
    ResolveMethod(g_damage_scale_zone_modify, "damage-pack.modify-scale-zone",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core", "DamagePackData",
        "ModifyDamageScaleZone", 3, "System.Void");
    ResolveMethod(g_apply_damage_modifier, "damage-pack.apply-modifiers",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core", "DamagePackData",
        "ApplyDamageModifer", 1, "System.Void");
    ResolveMethod(g_calculate_damage, "battle-formula.calculate-damage",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core", "BattleFormula",
        "CalculateDamage", 4, "System.Double");
    ResolveMethod(g_instant_attribute_process, "instant-attribute.process",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core", "InstantModifyAttribute",
        "ProcessDamagePackDataInternal", 4, "System.Void");
    ResolveMethod(g_damage_processor_process, "damage-processor.process",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core", "DamageProcessorBase",
        "ProcessDamagePackData", 5, "System.Void");
    ResolveMethod(g_blackboard_double_get_value, "blackboard-double.get-value",
        "Gameplay.Beyond.dll", "Beyond.Blackboard", "BlackboardDouble",
        "GetValue", 1, "System.Double");
    ResolveMethodExact(g_action_blackboard_try_get_double,
        "action-blackboard.try-get-double", "Gameplay.Beyond.dll", "Beyond",
        "ActionBlackboard", "TryGetDouble", "System.String|System.Double&", 2,
        "System.Boolean");
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
    const bool ability_source_field = ResolveField(g_ability_source,
        "ability.source", assembly, namespc, "AbilitySystem",
        "<source>k__BackingField");
    const bool entity_name_field = ResolveField(g_entity_name, "entity.name", assembly, namespc,
        "Entity", "<name>k__BackingField");
    const bool skill_id_field = ResolveField(g_skill_id, "skill.id", assembly, namespc,
        "Skill", "m_skillId");
    const bool damage_pack_field = ResolveField(g_modifier_damage_pack,
        "modifier.damage-pack", assembly, namespc, modifier_class,
        "<damagePackData>k__BackingField");
    const char* proto_assembly = "Beyond.Network.Proto.dll";
    const char* proto_namespace = "Proto";
    const bool server_modifier_args_field = ResolveField(g_server_detail_modifier_args,
        "server-detail.modifier-args", proto_assembly, proto_namespace,
        "BATTLE_DAMAGE_DETAIL", "modifierArgs_");
    const bool server_processor_args_field = ResolveField(
        g_server_detail_processor_debug_args, "server-detail.processor-debug-args",
        proto_assembly, proto_namespace, "BATTLE_DAMAGE_DETAIL", "processorDebugArgs_");
    const bool attacker_modifiers_field = ResolveField(g_modifier_args_attacker_modifiers,
        "modifier-args.attacker", proto_assembly, proto_namespace,
        "BATTLE_DAMAGE_MODIFIER_ARGS", "attackerModifiers_");
    const bool defender_modifiers_field = ResolveField(g_modifier_args_defender_modifiers,
        "modifier-args.defender", proto_assembly, proto_namespace,
        "BATTLE_DAMAGE_MODIFIER_ARGS", "defenderModifiers_");
    const bool handle_buff_field = ResolveField(g_modifier_handle_buff_inst_id,
        "modifier-handle.buff-inst-id", proto_assembly, proto_namespace,
        "BATTLE_MODIFIER_HANDLE", "buffInstId_");
    const bool handle_index_field = ResolveField(g_modifier_handle_index,
        "modifier-handle.index", proto_assembly, proto_namespace,
        "BATTLE_MODIFIER_HANDLE", "index_");
    const bool handle_processor_args_field = ResolveField(
        g_modifier_handle_processor_debug_args, "modifier-handle.processor-debug-args",
        proto_assembly, proto_namespace, "BATTLE_MODIFIER_HANDLE", "processorDebugArgs_");
    const bool buff_id_field = ResolveField(g_buff_id, "buff.id", assembly, namespc,
        "Buff", "<id>k__BackingField");
    const bool buff_owner_field = ResolveField(g_buff_owner, "buff.owner", assembly, namespc,
        "Buff", "<owner>k__BackingField");
    const bool buff_source_field = ResolveField(g_buff_source, "buff.source", assembly, namespc,
        "Buff", "<source>k__BackingField");
    const bool buff_skill_field = ResolveField(g_buff_skill_cast_info,
        "buff.skill-cast-info", assembly, namespc, "Buff",
        "<skillCastInfo>k__BackingField");
    const bool buff_attribute_mask_field = ResolveField(g_buff_attribute_mask,
        "buff.attribute-mask", assembly, namespc, "Buff",
        "<attributeMask>k__BackingField");
    const bool buff_blackboard_field = ResolveField(g_buff_blackboard,
        "buff.blackboard", assembly, namespc, "Buff",
        "<blackboard>k__BackingField");
    ResolveField(g_buff_damage_modifiers, "buff.damage-modifiers", assembly, namespc,
        "Buff", "m_damageModifiers");
    ResolveField(g_buff_data, "buff.data", assembly, namespc,
        "Buff", "m_data");
    ResolveField(g_damage_modifier_owner, "damage-modifier.owner", assembly, namespc,
        "DamageModifier", "m_owner");
    ResolveField(g_damage_modifier_data, "damage-modifier.data", assembly, namespc,
        "DamageModifier", "m_data");
    const bool pack_calc_result_field = ResolveField(g_pack_calc_result,
        "damage-pack.calc-result", assembly, namespc, "DamagePackData", "calcResult");
    const bool pack_atk_scale_field = ResolveField(g_pack_atk_scale,
        "damage-pack.atk-scale", assembly, namespc, "DamagePackData", "atkScale");
    const bool pack_attacker_zones_field = ResolveField(g_pack_attacker_damage_scale_zones,
        "damage-pack.attacker-zones", assembly, namespc, "DamagePackData",
        "attackerDamageScaleZones");
    const bool pack_attacker_attributes_field = ResolveField(g_pack_attacker_attributes,
        "damage-pack.attacker-attributes", assembly, namespc, "DamagePackData",
        "attackerAttributes");
    const bool pack_defender_zones_field = ResolveField(g_pack_defender_damage_scale_zones,
        "damage-pack.defender-zones", assembly, namespc, "DamagePackData",
        "defenderDamageScaleZones");
    const bool pack_defender_attributes_field = ResolveField(g_pack_defender_attributes,
        "damage-pack.defender-attributes", assembly, namespc, "DamagePackData",
        "defenderAttributes");
    const bool pack_attacker_field = ResolveField(g_pack_attacker,
        "damage-pack.attacker", assembly, namespc, "DamagePackData", "attacker");
    const bool pack_defender_field = ResolveField(g_pack_defender,
        "damage-pack.defender", assembly, namespc, "DamagePackData", "defender");
    const bool pack_skill_cast_info_field = ResolveField(g_pack_skill_cast_info,
        "damage-pack.skill-cast-info", assembly, namespc, "DamagePackData",
        "skillCastInfo");
    const bool pack_damage_type_field = ResolveField(g_pack_damage_type,
        "damage-pack.damage-type", assembly, namespc, "DamagePackData", "damageType");
    const bool pack_damage_decorate_mask_field = ResolveField(
        g_pack_damage_decorate_mask, "damage-pack.damage-decorate-mask", assembly,
        namespc, "DamagePackData", "damageDecorateMask");
    const bool attributes_data_min_field = ResolveField(g_attributes_data_min,
        "attributes.data-min", assembly, namespc, "Attributes", "m_dataMin");
    const bool attributes_data_max_field = ResolveField(g_attributes_data_max,
        "attributes.data-max", assembly, namespc, "Attributes", "m_dataMax");
    const bool calc_value_field = ResolveField(g_calc_result_value,
        "calc-result.value", assembly, namespc, "CalcResult", "value");
    const bool instant_target_side_field = ResolveField(g_instant_attribute_target_side,
        "instant-attribute.target-side", assembly, namespc, "InstantModifyAttribute",
        "modifyTargetSide");
    const bool instant_modifier_field = ResolveField(g_instant_attribute_modifier,
        "instant-attribute.modifier", assembly, namespc, "InstantModifyAttribute",
        "modifier");
    const bool attribute_modify_type_field = ResolveField(g_attribute_modifier_modify_type,
        "attribute-modifier.modify-type", assembly, "Beyond.Gameplay",
        "AttributeModifierData.AttributeModifier", "modifyAttributeType");
    const bool attribute_type_field = ResolveField(g_attribute_modifier_attribute_type,
        "attribute-modifier.attribute-type", assembly, "Beyond.Gameplay",
        "AttributeModifierData.AttributeModifier", "attributeType");
    const bool attribute_formula_field = ResolveField(g_attribute_modifier_formula_item,
        "attribute-modifier.formula-item", assembly, "Beyond.Gameplay",
        "AttributeModifierData.AttributeModifier", "formulaItem");
    const bool attribute_param_field = ResolveField(g_attribute_modifier_param,
        "attribute-modifier.param", assembly, "Beyond.Gameplay",
        "AttributeModifierData.AttributeModifier", "param");
    g_modifier_contract_ready = shared_flags_field && delta_type_field && value_field &&
        real_delta_field && modifier_damage_type_field && modifier_mask_field;
    g_rdps_diagnostic_contract_ready = damage_pack_field &&
        g_damage_pack_server_detail_getter.pointer && server_modifier_args_field &&
        server_processor_args_field && attacker_modifiers_field && defender_modifiers_field &&
        handle_buff_field && handle_index_field && handle_processor_args_field;
    g_buff_diagnostic_contract_ready = g_buff_inst_id_getter.pointer && buff_id_field &&
        buff_owner_field && buff_source_field && buff_skill_field;
    g_buff_attribute_contract_ready = g_buff_diagnostic_contract_ready &&
        g_buff_attribute_get_value.pointer && g_buff_attribute_get_base_value.pointer &&
        buff_attribute_mask_field && g_ability_attributes_getter.pointer &&
        g_attributes_get_all_modifier_values.pointer &&
        g_calculate_final_attribute_value.pointer && g_get_final_damage_scale.pointer &&
        g_get_def_resistance_value.pointer &&
        g_get_damage_type_resistance_value.pointer && pack_attacker_field &&
        pack_defender_field && pack_damage_type_field &&
        pack_damage_decorate_mask_field && attributes_data_min_field &&
        attributes_data_max_field;
    g_rdps_value_contract_ready = g_damage_modifier_process.pointer &&
        g_damage_scale_zone_modify.pointer && handle_buff_field && handle_index_field &&
        pack_calc_result_field && pack_atk_scale_field && pack_attacker_zones_field &&
        pack_attacker_attributes_field && pack_defender_zones_field &&
        pack_defender_attributes_field && calc_value_field;
    g_rdps_flow_contract_ready = g_rdps_value_contract_ready &&
        g_apply_damage_modifier.pointer && g_calculate_damage.pointer &&
        g_damage_pack_server_detail_getter.pointer && damage_pack_field &&
        pack_skill_cast_info_field;
    g_instant_attribute_contract_ready = g_rdps_flow_contract_ready &&
        g_instant_attribute_process.pointer &&
        instant_target_side_field && instant_modifier_field &&
        attribute_modify_type_field && attribute_type_field &&
        attribute_formula_field && attribute_param_field;
    g_arts_strength_contract_ready = g_buff_diagnostic_contract_ready &&
        buff_blackboard_field && g_action_blackboard_try_get_double.pointer &&
        g_host->string_new && g_host->gchandle_new && g_host->gchandle_free;
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
    Log(std::string("[rdps-diag] damage attribution contract ") +
        (g_rdps_diagnostic_contract_ready ? "ready" : "unavailable"));
    Log(std::string("[rdps-diag] buff lifecycle contract ") +
        (g_buff_diagnostic_contract_ready ? "ready" : "unavailable"));
    Log(std::string("[rdps-diag] persistent attribute attribution contract ") +
        (g_buff_attribute_contract_ready ? "ready" : "unavailable"));
    Log(std::string("[rdps-diag] arts-strength blackboard contract ") +
        (g_arts_strength_contract_ready ? "ready" : "unavailable"));
    Log(std::string("[rdps-diag] per-modifier numeric contract ") +
        (g_rdps_value_contract_ready ? "ready" : "unavailable"));
    Log(std::string("[rdps-diag] damage transaction contract ") +
        (g_rdps_flow_contract_ready ? "ready" : "unavailable"));
    Log(std::string("[rdps-diag] instant attribute processor contract ") +
        (g_instant_attribute_contract_ready ? "ready" : "unavailable"));
    if (!source_field || !ability_entity_field || !entity_name_field) {
        Log("[combat-contract] attacker labels unavailable; totals remain available");
    }
    if (!ability_source_field) {
        Log("[combat-contract] spawned-entity source chain unavailable");
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

    // Squad / character snapshot chain. GameInstance is reached through its
    // static property getter (get_instance) so no static-field address is
    // required; the remaining hops are ordinary instance fields.
    ResolveMethod(g_game_instance_getter, "game-instance.get",
        "Gameplay.Beyond.dll", "Beyond.Gameplay", "GameInstance", "get_instance",
        0, "Beyond.Gameplay.GameInstance");
    ResolveMethod(g_char_info_getter, "char-bag.get-char-info",
        "Gameplay.Beyond.dll", "Beyond.Gameplay", "CharBagSystem",
        "GetCharInfo", 1, "Beyond.Gameplay.CharInfo");
    ResolveMethod(g_char_info_get_weapon, "client-char-info.get-weapon",
        "Gameplay.Beyond.dll", "Beyond.Gameplay", "ClientCharInfo",
        "GetWeaponInstData", 0,
        "Beyond.Gameplay.InventorySystem.WeaponInstData");
    ResolveMethodExact(g_char_info_get_equip, "client-char-info.get-equip",
        "Gameplay.Beyond.dll", "Beyond.Gameplay", "ClientCharInfo",
        "GetEquipInstData", "System.Int32", 1,
        "Beyond.Gameplay.InventorySystem.EquipInstData");
    // Online characters are ServerCharInfo instances whose overrides fetch
    // weapon/equipment through the inventory rather than ClientCharInfo's own
    // fields. Resolve both overrides and dispatch by the actual runtime class.
    ResolveMethod(g_server_char_info_get_weapon, "server-char-info.get-weapon",
        "Gameplay.Beyond.dll", "Beyond.Gameplay", "ServerCharInfo",
        "GetWeaponInstData", 0,
        "Beyond.Gameplay.InventorySystem.WeaponInstData");
    ResolveMethodExact(g_server_char_info_get_equip, "server-char-info.get-equip",
        "Gameplay.Beyond.dll", "Beyond.Gameplay", "ServerCharInfo",
        "GetEquipInstData", "System.Int32", 1,
        "Beyond.Gameplay.InventorySystem.EquipInstData");
    g_client_char_info_class = ResolveClassInfo("client-char-info.class",
        "Gameplay.Beyond.dll", "Beyond.Gameplay", "ClientCharInfo");
    g_server_char_info_class = ResolveClassInfo("server-char-info.class",
        "Gameplay.Beyond.dll", "Beyond.Gameplay", "ServerCharInfo");
    ResolveField(g_game_instance_player, "game-instance.player",
        "Gameplay.Beyond.dll", "Beyond.Gameplay", "GameInstance", "m_player");
    ResolveField(g_game_player_squad_manager, "game-player.squad-manager",
        "Gameplay.Beyond.dll", "Beyond.Gameplay", "GamePlayer", "squadManager");
    ResolveField(g_game_player_char_bag, "game-player.char-bag",
        "Gameplay.Beyond.dll", "Beyond.Gameplay", "GamePlayer", "charBag");
    ResolveField(g_squad_manager_cur_squad, "squad-manager.cur-squad",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core", "SquadManager",
        "<curSquad>k__BackingField");
    ResolveField(g_squad_slots, "squad.slots",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core", "SquadManager.Squad",
        "slots");
    ResolveField(g_slot_char_inst_id, "slot.char-inst-id",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core",
        "SquadManager.Squad.Slot", "charInstId");
    ResolveField(g_slot_char_id, "slot.char-id",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core",
        "SquadManager.Squad.Slot", "charId");
    ResolveField(g_char_info_potential_level, "char-info.potential-level",
        "Gameplay.Beyond.dll", "Beyond.Gameplay", "CharInfo", "potentialLevel");
    ResolveField(g_char_info_level, "char-info.level",
        "Gameplay.Beyond.dll", "Beyond.Gameplay", "CharInfo", "level");
    ResolveField(g_char_info_break_stage, "char-info.break-stage",
        "Gameplay.Beyond.dll", "Beyond.Gameplay", "CharInfo", "breakStage");
    ResolveField(g_char_info_skill_groups, "char-info.skill-groups",
        "Gameplay.Beyond.dll", "Beyond.Gameplay", "CharInfo",
        "skillGroupLevelInfoList");
    ResolveField(g_char_info_equip_suit, "char-info.equip-suit",
        "Gameplay.Beyond.dll", "Beyond.Gameplay", "CharInfo", "equipSuit");
    ResolveField(g_skill_group_level, "skill-group.level",
        "Gameplay.Beyond.dll", "Beyond.Gameplay", "SkillGroupLevelInfo", "level");
    ResolveField(g_skill_group_max_level, "skill-group.max-level",
        "Gameplay.Beyond.dll", "Beyond.Gameplay", "SkillGroupLevelInfo", "maxLevel");
    ResolveField(g_skill_group_id, "skill-group.id",
        "Gameplay.Beyond.dll", "Beyond.Gameplay", "SkillGroupLevelInfo", "skillGroupId");
    ResolveFieldWithOffsetFallback(g_item_inst_template_id, "item-inst.template-id",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.InventorySystem", "ItemInstData",
        "templateId", 0x18);
    ResolveFieldWithOffsetFallback(g_weapon_level, "weapon-inst.level",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.InventorySystem", "WeaponInstData",
        "weaponLv", 0x38);
    ResolveFieldWithOffsetFallback(g_weapon_refine_level, "weapon-inst.refine-level",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.InventorySystem", "WeaponInstData",
        "refineLv", 0x3c);
    ResolveFieldWithOffsetFallback(g_weapon_breakthrough_level,
        "weapon-inst.breakthrough-level", "Gameplay.Beyond.dll",
        "Beyond.Gameplay.InventorySystem", "WeaponInstData", "breakthroughLv", 0x40);
    ResolveFieldWithOffsetFallback(g_equip_enhance_levels, "equip-inst.enhance-levels",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.InventorySystem", "EquipInstData",
        "enhanceAttrLevels", 0x38);
    ResolveField(g_game_player_dungeon_manager, "game-player.dungeon-manager",
        "Gameplay.Beyond.dll", "Beyond.Gameplay", "GamePlayer", "dungeonManager");
    ResolveField(g_dungeon_manager_cur_dungeon_id, "dungeon-manager.cur-dungeon-id",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core", "DungeonManager", "<curDungeonId>k__BackingField");
    ResolveField(g_dungeon_manager_cur_dungeon_id_fallback, "dungeon-manager.cur-dungeon-id-fb",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core", "DungeonManager", "m_curDungeonId");
    ResolveField(g_dungeon_manager_cur_mode_id, "dungeon-manager.cur-mode-id",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core", "DungeonManager", "<curModeId>k__BackingField");

    // Battle / dungeon metadata carried by the BattleRecorder instance that is
    // already the `this` of our RecordDamage / RecordBuffStart hooks.
    ResolveField(g_battle_recorder_battle_info, "battle-recorder.battle-info",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core",
        "BattleManager.BattleRecorder", "m_curBattleInfo");
    ResolveField(g_battle_info_mode_id, "battle-info.mode-id",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core",
        "BattleManager.BattleRecorder.BattleInfo", "modeId");
    ResolveField(g_battle_info_dungeon_id, "battle-info.dungeon-id",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core",
        "BattleManager.BattleRecorder.BattleInfo", "dungeonId");
    ResolveField(g_battle_info_battle_id, "battle-info.battle-id",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core",
        "BattleManager.BattleRecorder.BattleInfo", "battleId");

    g_squad_contract_ready = g_game_instance_getter.pointer &&
        g_char_info_getter.pointer && g_game_instance_player.resolved &&
        g_game_player_squad_manager.resolved && g_game_player_char_bag.resolved &&
        g_squad_manager_cur_squad.resolved && g_squad_slots.resolved &&
        g_slot_char_inst_id.resolved && g_slot_char_id.resolved &&
        g_char_info_potential_level.resolved && g_char_info_level.resolved &&
        g_char_info_break_stage.resolved && g_char_info_skill_groups.resolved;
    g_dungeon_contract_ready = (g_battle_recorder_battle_info.resolved &&
        g_battle_info_dungeon_id.resolved) ||
        (g_game_player_dungeon_manager.resolved &&
         (g_dungeon_manager_cur_dungeon_id.resolved || g_dungeon_manager_cur_dungeon_id_fallback.resolved));
    Log(std::string("[combat-squad] squad snapshot contract ") +
        (g_squad_contract_ready ? "ready" : "unavailable"));
    Log(std::string("[combat-dungeon] dungeon metadata contract ") +
        (g_dungeon_contract_ready ? "ready" : "unavailable"));
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

std::string ResolveAbilityName(void* ability) {
    if (!ability) return {};
    void* entity = nullptr;
    if (!ReadValue(ability, g_ability_entity, entity) || !entity) return {};
    void* name = nullptr;
    if (!ReadValue(entity, g_entity_name, name) || !name) return {};
    return CopyString(name);
}

std::string ResolveSkillIdFromCastInfo(void* cast_info) {
    if (!cast_info) return {};
    void* skill = nullptr;
    if (!ReadValueTypeField(cast_info, g_skill_cast_origin_skill, skill) || !skill) return {};
    void* id = nullptr;
    if (!ReadValue(skill, g_skill_id, id) || !id) return {};
    return CopyString(id);
}

uint64_t ReadBuffInstId(void* buff) {
    if (!buff || !g_buff_inst_id_getter.pointer) return 0;
    __try {
        return reinterpret_cast<UInt64GetterFn>(g_buff_inst_id_getter.pointer)(
            buff, const_cast<void*>(g_buff_inst_id_getter.method_info));
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return 0;
    }
}

void* ReadDamagePackServerDetail(void* damage_pack) {
    if (!damage_pack || !g_damage_pack_server_detail_getter.pointer) return nullptr;
    __try {
        return reinterpret_cast<ObjectGetterFn>(g_damage_pack_server_detail_getter.pointer)(
            damage_pack, const_cast<void*>(g_damage_pack_server_detail_getter.method_info));
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return nullptr;
    }
}

void* ReadDamageServerDetail(void* modifier) {
    return ReadDamagePackServerDetail(
        EmbeddedValueType(modifier, g_modifier_damage_pack));
}

int ManagedMapCount(void* map_field) {
    if (!map_field) return 0;
    __try {
        // Google.Protobuf MapField<TKey,TValue> stores its Dictionary at +0x10.
        // IL2CPP Dictionary count is _count - _freeCount at +0x20/+0x28.
        void* dictionary = *reinterpret_cast<void**>(
            reinterpret_cast<uint8_t*>(map_field) + 0x10);
        if (!dictionary) return 0;
        const int32_t allocated = *reinterpret_cast<int32_t*>(
            reinterpret_cast<uint8_t*>(dictionary) + 0x20);
        const int32_t free_count = *reinterpret_cast<int32_t*>(
            reinterpret_cast<uint8_t*>(dictionary) + 0x28);
        const int32_t count = allocated - free_count;
        return count >= 0 && count <= 4096 ? count : -1;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return -1;
    }
}

struct RepeatedObjects {
    void* array = nullptr;
    int32_t count = 0;
};

bool ReadRepeatedObjects(void* repeated_field, RepeatedObjects& result) {
    result = {};
    if (!repeated_field) return true;
    __try {
        void* array = *reinterpret_cast<void**>(
            reinterpret_cast<uint8_t*>(repeated_field) + 0x10);
        const int32_t count = *reinterpret_cast<int32_t*>(
            reinterpret_cast<uint8_t*>(repeated_field) + 0x18);
        if (count < 0 || count > 4096 || (count > 0 && !array)) return false;
        if (array) {
            const uintptr_t capacity = *reinterpret_cast<uintptr_t*>(
                reinterpret_cast<uint8_t*>(array) + 0x18);
            if (capacity > 4096 || static_cast<uintptr_t>(count) > capacity) return false;
        }
        result.array = array;
        result.count = count;
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

void* RepeatedObjectAt(const RepeatedObjects& repeated, int32_t index) {
    if (!repeated.array || index < 0 || index >= repeated.count) return nullptr;
    __try {
        return *reinterpret_cast<void**>(reinterpret_cast<uint8_t*>(repeated.array) +
            0x20 + static_cast<size_t>(index) * sizeof(void*));
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return nullptr;
    }
}

std::optional<std::string> VerifiedArtsStrengthValueKey(
    std::string_view buff_id) {
    const auto catalog = SemanticCatalogSnapshot();
    if (!catalog || buff_id.empty()) return std::nullopt;
    const BuffSemantic* semantic = catalog->FindBuff(buff_id);
    if (!semantic) return std::nullopt;
    for (const SemanticEffect& effect : semantic->effects) {
        if (effect.status == SemanticStatus::Verified &&
            effect.zone == SemanticZone::ArtsStrength &&
            !effect.value_key.empty()) {
            return effect.value_key;
        }
    }
    return std::nullopt;
}

void* GetOrCreateManagedKeyString(std::string_view key) {
    if (key.empty() || !g_host || !g_host->string_new ||
        !g_host->gchandle_new) {
        return nullptr;
    }
    std::scoped_lock lock(g_managed_key_mutex);
    const auto found = g_managed_key_strings.find(std::string(key));
    if (found != g_managed_key_strings.end()) return found->second.object;
    const std::string owned_key(key);
    void* object = g_host->string_new(g_host->context, owned_key.c_str());
    if (!object) return nullptr;
    const uint32_t handle = g_host->gchandle_new(g_host->context, object, 0);
    if (!handle) return nullptr;
    g_managed_key_strings.emplace(owned_key, ManagedKeyString{object, handle});
    return object;
}

bool CallActionBlackboardTryGetDouble(
    void* blackboard, void* managed_key, double& value) {
    __try {
        return reinterpret_cast<ActionBlackboardTryGetDoubleFn>(
            g_action_blackboard_try_get_double.pointer)(blackboard,
            managed_key, &value,
            const_cast<void*>(g_action_blackboard_try_get_double.method_info));
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

bool TryReadArtsStrengthPoints(void* blackboard, std::string_view buff_id,
    double& points) {
    points = 0.0;
    if (!blackboard || !g_arts_strength_contract_ready) return false;
    const std::optional<std::string> key =
        VerifiedArtsStrengthValueKey(buff_id);
    if (!key) return false;
    void* managed_key = GetOrCreateManagedKeyString(*key);
    if (!managed_key) return false;
    double value = 0.0;
    const bool found = CallActionBlackboardTryGetDouble(
        blackboard, managed_key, value);
    if (!found || !std::isfinite(value)) return false;
    points = value;
    return true;
}

int ManagedListCount(void* list) {
    if (!list) return -1;
    __try {
        const int32_t size = *reinterpret_cast<int32_t*>(
            reinterpret_cast<uint8_t*>(list) + 0x18);
        return size >= 0 && size <= 4096 ? size : -1;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return -1;
    }
}

void* ManagedListElement(void* list, int32_t index) {
    if (!list || index < 0) return nullptr;
    __try {
        void* items = *reinterpret_cast<void**>(
            reinterpret_cast<uint8_t*>(list) + 0x10);
        if (!items) return nullptr;
        const uintptr_t length = *reinterpret_cast<uintptr_t*>(
            reinterpret_cast<uint8_t*>(items) + 0x18);
        if (static_cast<uintptr_t>(index) >= length) return nullptr;
        return *reinterpret_cast<void**>(
            reinterpret_cast<uint8_t*>(items) + 0x20 + index * sizeof(void*));
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return nullptr;
    }
}

BuffDiagnosticInfo CaptureBuffDiagnosticInfo(void* buff) {
    BuffDiagnosticInfo result;
    if (!buff) return result;
    result.inst_id = ReadBuffInstId(buff);
    void* id = nullptr;
    ReadValue(buff, g_buff_id, id);
    result.buff_id = CopyString(id);
    void* source = nullptr;
    void* owner = nullptr;
    ReadValue(buff, g_buff_source, source);
    ReadValue(buff, g_buff_owner, owner);
    result.source_ability = source;
    result.owner_ability = owner;
    result.source = ResolveAbilityName(source);
    result.owner = ResolveAbilityName(owner);
    ReadValue(buff, g_buff_blackboard, result.blackboard);
    ReadValue(buff, g_buff_data, result.buff_data);
    result.has_arts_strength_points = TryReadArtsStrengthPoints(
        result.blackboard, result.buff_id, result.arts_strength_points);
    if (g_buff_skill_cast_info.resolved) {
        void* cast_info = reinterpret_cast<uint8_t*>(buff) +
            g_buff_skill_cast_info.value.offset;
        result.origin_skill = ResolveSkillIdFromCastInfo(cast_info);
    }
    if (g_buff_damage_modifiers.resolved) {
        void* mod_list = nullptr;
        ReadValue(buff, g_buff_damage_modifiers, mod_list);
        if (mod_list) {
            const int count = ManagedListCount(mod_list);
            if (count > 0) {
                result.damage_modifiers.reserve(count);
                for (int i = 0; i < count; ++i) {
                    void* mod = ManagedListElement(mod_list, i);
                    if (mod) result.damage_modifiers.push_back(mod);
                }
            }
        }
    }
    return result;
}

void* GetGameInstance() {
    if (!g_game_instance_getter.pointer) return nullptr;
    __try {
        return reinterpret_cast<StaticObjectGetterFn>(g_game_instance_getter.pointer)(
            const_cast<void*>(g_game_instance_getter.method_info));
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return nullptr;
    }
}

void* GetCharInfo(void* char_bag, uint64_t char_inst_id) {
    if (!char_bag || !g_char_info_getter.pointer) return nullptr;
    __try {
        return reinterpret_cast<CharInfoGetterFn>(g_char_info_getter.pointer)(
            char_bag, char_inst_id,
            const_cast<void*>(g_char_info_getter.method_info));
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return nullptr;
    }
}

// The first pointer of every IL2CPP object is its Il2CppClass. CharInfo has
// two live subclasses whose GetWeaponInstData/GetEquipInstData overrides read
// completely different storage, so the call must be dispatched by the
// instance's actual class instead of assuming ClientCharInfo.
const void* ReadObjectClass(void* object) {
    if (!object) return nullptr;
    __try {
        return *reinterpret_cast<const void**>(object);
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return nullptr;
    }
}

std::atomic_bool g_char_info_class_warning_logged{false};

const RuntimeMethod* SelectCharInfoOverride(void* char_info,
    const RuntimeMethod& client_method, const RuntimeMethod& server_method) {
    const void* klass = ReadObjectClass(char_info);
    if (!klass) return nullptr;
    if (klass == g_client_char_info_class && client_method.pointer) {
        return &client_method;
    }
    if (klass == g_server_char_info_class && server_method.pointer) {
        return &server_method;
    }
    if (!g_char_info_class_warning_logged.exchange(true,
            std::memory_order_acq_rel)) {
        std::ostringstream message;
        message << "[combat-squad] CharInfo runtime class unrecognized klass="
            << klass << " client=" << g_client_char_info_class
            << " server=" << g_server_char_info_class;
        Log(message.str());
    }
    return nullptr;
}

void* GetCharWeapon(void* char_info) {
    if (!char_info) return nullptr;
    const RuntimeMethod* method = SelectCharInfoOverride(char_info,
        g_char_info_get_weapon, g_server_char_info_get_weapon);
    if (!method) return nullptr;
    __try {
        return reinterpret_cast<ObjectGetterFn>(method->pointer)(
            char_info, const_cast<void*>(method->method_info));
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return nullptr;
    }
}

void* GetCharEquipment(void* char_info, int32_t slot) {
    if (!char_info) return nullptr;
    const RuntimeMethod* method = SelectCharInfoOverride(char_info,
        g_char_info_get_equip, g_server_char_info_get_equip);
    if (!method) return nullptr;
    __try {
        return reinterpret_cast<EquipGetterFn>(method->pointer)(
            char_info, slot, const_cast<void*>(method->method_info));
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return nullptr;
    }
}

int ManagedDictionaryCount(void* dictionary) {
    if (!dictionary) return -1;
    __try {
        const int32_t count = *reinterpret_cast<int32_t*>(
            reinterpret_cast<uint8_t*>(dictionary) + 0x20);
        const int32_t free_count = *reinterpret_cast<int32_t*>(
            reinterpret_cast<uint8_t*>(dictionary) + 0x2C);
        const int32_t used = count - free_count;
        return used >= 0 && used <= 4096 ? used : -1;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return -1;
    }
}

struct RawIntDictionaryItem {
    int32_t key = 0;
    int32_t value = 0;
};

bool TryReadIntDictionary(void* dictionary, RawIntDictionaryItem* output,
    size_t output_capacity, size_t* output_count) {
    if (!dictionary || !output || !output_count) return false;
    *output_count = 0;
    __try {
        void* entries = *reinterpret_cast<void**>(
            reinterpret_cast<uint8_t*>(dictionary) + 0x18);
        const int32_t count = *reinterpret_cast<int32_t*>(
            reinterpret_cast<uint8_t*>(dictionary) + 0x20);
        if (!entries || count < 0 || count > 128) return false;
        const uintptr_t capacity = *reinterpret_cast<uintptr_t*>(
            reinterpret_cast<uint8_t*>(entries) + 0x18);
        if (capacity > 4096 || static_cast<uintptr_t>(count) > capacity) return false;
        const auto* data = reinterpret_cast<const uint8_t*>(entries) + 0x20;
        for (int32_t index = 0; index < count; ++index) {
            const auto* entry = data + static_cast<size_t>(index) * 16;
            const int32_t hash_code = *reinterpret_cast<const int32_t*>(entry);
            if (hash_code < 0) continue;
            const int32_t key = *reinterpret_cast<const int32_t*>(entry + 8);
            const int32_t value = *reinterpret_cast<const int32_t*>(entry + 12);
            if (*output_count >= output_capacity) return false;
            output[(*output_count)++] = {key, value};
        }
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        *output_count = 0;
        return false;
    }
}

std::map<int32_t, int32_t> ReadIntDictionary(void* dictionary) {
    RawIntDictionaryItem items[128]{};
    size_t count = 0;
    std::map<int32_t, int32_t> result;
    if (!TryReadIntDictionary(dictionary, items, std::size(items), &count))
        return result;
    for (size_t index = 0; index < count; ++index)
        result[items[index].key] = items[index].value;
    return result;
}

struct RawEquipSuitItem {
    void* managed_key = nullptr;
    int32_t value = 0;
};

bool TryReadEquipSuits(void* dictionary, RawEquipSuitItem* output,
    size_t output_capacity, size_t* output_count) {
    if (!dictionary || !output || !output_count) return false;
    *output_count = 0;
    __try {
        void* entries = *reinterpret_cast<void**>(
            reinterpret_cast<uint8_t*>(dictionary) + 0x18);
        const int32_t count = *reinterpret_cast<int32_t*>(
            reinterpret_cast<uint8_t*>(dictionary) + 0x20);
        if (!entries || count < 0 || count > 64) return false;
        const uintptr_t capacity = *reinterpret_cast<uintptr_t*>(
            reinterpret_cast<uint8_t*>(entries) + 0x18);
        if (capacity > 4096 || static_cast<uintptr_t>(count) > capacity) return false;
        const auto* data = reinterpret_cast<const uint8_t*>(entries) + 0x20;
        for (int32_t index = 0; index < count; ++index) {
            const auto* entry = data + static_cast<size_t>(index) * 24;
            const int32_t hash_code = *reinterpret_cast<const int32_t*>(entry);
            if (hash_code < 0) continue;
            if (*output_count >= output_capacity) return false;
            output[(*output_count)++] = {
                *reinterpret_cast<void* const*>(entry + 8),
                *reinterpret_cast<const int32_t*>(entry + 16),
            };
        }
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        *output_count = 0;
        return false;
    }
}

std::vector<Session::SquadMember::EquipSuit> ReadEquipSuits(void* dictionary) {
    RawEquipSuitItem items[64]{};
    size_t count = 0;
    std::vector<Session::SquadMember::EquipSuit> result;
    if (!TryReadEquipSuits(dictionary, items, std::size(items), &count))
        return result;
    for (size_t index = 0; index < count; ++index) {
        std::string key = CopyString(items[index].managed_key);
        if (!key.empty()) result.push_back({key, items[index].value, {}});
    }
    return result;
}

void LogDungeonMetadata(void* battle_recorder) {
    if (!g_dungeon_contract_ready) return;
    std::string mode_str;
    std::string dungeon_str;
    int64_t battle_id = 0;

    // BattleInfo is an embedded value type. Its child field offsets include the
    // boxed object header, so ReadValueTypeField translates them to the unboxed
    // payload reached at BattleRecorder.m_curBattleInfo.
    if (battle_recorder && g_battle_recorder_battle_info.resolved) {
        void* battle_info = reinterpret_cast<uint8_t*>(battle_recorder) +
            g_battle_recorder_battle_info.value.offset;
        void* mode_id = nullptr;
        void* dungeon_id = nullptr;
        if (ReadValueTypeField(battle_info, g_battle_info_mode_id, mode_id)) {
            mode_str = CopyString(mode_id);
        }
        if (ReadValueTypeField(battle_info, g_battle_info_dungeon_id, dungeon_id)) {
            dungeon_str = CopyString(dungeon_id);
        }
        ReadValueTypeField(battle_info, g_battle_info_battle_id, battle_id);
    }

    // Fallback to GamePlayer.dungeonManager if dungeon_str is still empty.
    if (dungeon_str.empty()) {
        void* game_instance = GetGameInstance();
        if (game_instance) {
            void* player = nullptr;
            if (ReadValue(game_instance, g_game_instance_player, player) && player) {
                void* dungeon_mgr = nullptr;
                if (ReadValue(player, g_game_player_dungeon_manager, dungeon_mgr) && dungeon_mgr) {
                    void* cur_dungeon = nullptr;
                    if (ReadValue(dungeon_mgr, g_dungeon_manager_cur_dungeon_id, cur_dungeon) ||
                        ReadValue(dungeon_mgr, g_dungeon_manager_cur_dungeon_id_fallback, cur_dungeon)) {
                        dungeon_str = CopyString(cur_dungeon);
                    }
                    void* cur_mode = nullptr;
                    if (ReadValue(dungeon_mgr, g_dungeon_manager_cur_mode_id, cur_mode)) {
                        mode_str = CopyString(cur_mode);
                    }
                }
            }
        }
    }

    Log("[combat-dungeon] mode=\"" + mode_str +
        "\" dungeon=\"" + dungeon_str +
        "\" battleId=" + std::to_string(battle_id));

    if (!dungeon_str.empty()) {
        std::scoped_lock lock(g_session_mutex);
        g_session.dungeon_id = dungeon_str;
        g_session.mode_id = mode_str;
    }
}

std::string NormalizeAttackerId(std::string value);
bool StartsWithCharacterPrefix(std::string_view value);

void LogSquadSnapshot() {
    if (!g_squad_contract_ready) return;
    void* game_instance = GetGameInstance();
    if (!game_instance) {
        Log("[combat-squad] GameInstance unavailable");
        return;
    }
    void* player = nullptr;
    if (!ReadValue(game_instance, g_game_instance_player, player) || !player) {
        Log("[combat-squad] GamePlayer unavailable");
        return;
    }
    void* squad_manager = nullptr;
    void* char_bag = nullptr;
    ReadValue(player, g_game_player_squad_manager, squad_manager);
    ReadValue(player, g_game_player_char_bag, char_bag);
    if (!squad_manager || !char_bag) {
        Log("[combat-squad] squadManager or charBag unavailable");
        return;
    }
    void* cur_squad = nullptr;
    if (!ReadValue(squad_manager, g_squad_manager_cur_squad, cur_squad) ||
        !cur_squad) {
        Log("[combat-squad] curSquad unavailable");
        return;
    }
    void* slots = nullptr;
    if (!ReadValue(cur_squad, g_squad_slots, slots) || !slots) {
        Log("[combat-squad] squad.slots unavailable");
        return;
    }
    const int32_t slot_count = ManagedListCount(slots);
    if (slot_count <= 0) {
        Log("[combat-squad] squad slots empty");
        return;
    }
    std::vector<Session::SquadMember> squad_members;
    std::ostringstream output;
    output << "[combat-squad] slots=" << slot_count;
    for (int32_t index = 0; index < slot_count && index < 16; ++index) {
        void* slot = ManagedListElement(slots, index);
        if (!slot) continue;
        uint64_t char_inst_id = 0;
        void* char_id = nullptr;
        ReadValue(slot, g_slot_char_inst_id, char_inst_id);
        ReadValue(slot, g_slot_char_id, char_id);
        std::string char_id_str = CopyString(char_id);
        int32_t level = 0;
        int32_t break_stage = 0;
        uint32_t potential = 0;
        std::vector<Session::SquadMember::SkillLevel> skill_groups;
        std::optional<Session::SquadMember::Weapon> weapon;
        std::vector<Session::SquadMember::Equipment> equipments;
        std::vector<Session::SquadMember::EquipSuit> equip_suits;
        void* char_info = GetCharInfo(char_bag, char_inst_id);
        if (char_info) {
            ReadValue(char_info, g_char_info_level, level);
            ReadValue(char_info, g_char_info_break_stage, break_stage);
            ReadValue(char_info, g_char_info_potential_level, potential);
            void* managed_skill_groups = nullptr;
            if (ReadValue(char_info, g_char_info_skill_groups, managed_skill_groups) &&
                managed_skill_groups) {
                const int32_t count = ManagedListCount(managed_skill_groups);
                for (int32_t skill_index = 0; skill_index < count && skill_index < 32;
                    ++skill_index) {
                    void* info = ManagedListElement(managed_skill_groups, skill_index);
                    int32_t skill_level = 0;
                    int32_t max_level = 0;
                    void* skill_id = nullptr;
                    ReadValue(info, g_skill_group_level, skill_level);
                    ReadValue(info, g_skill_group_max_level, max_level);
                    ReadValue(info, g_skill_group_id, skill_id);
                    std::string id = CopyString(skill_id);
                    if (!id.empty()) {
                        skill_groups.push_back({id, skill_level, max_level});
                    }
                }
            }
            void* equip_suit = nullptr;
            if (ReadValue(char_info, g_char_info_equip_suit, equip_suit)) {
                equip_suits = ReadEquipSuits(equip_suit);
            }
            if (void* weapon_data = GetCharWeapon(char_info)) {
                void* template_id = nullptr;
                Session::SquadMember::Weapon snapshot;
                ReadValue(weapon_data, g_item_inst_template_id, template_id);
                ReadValue(weapon_data, g_weapon_level, snapshot.level);
                ReadValue(weapon_data, g_weapon_refine_level, snapshot.refine_level);
                ReadValue(weapon_data, g_weapon_breakthrough_level,
                    snapshot.breakthrough_level);
                snapshot.template_id = CopyString(template_id);
                if (!snapshot.template_id.empty()) weapon = std::move(snapshot);
            }
            for (int32_t equip_slot = 0; equip_slot < 8; ++equip_slot) {
                void* equip_data = GetCharEquipment(char_info, equip_slot);
                if (!equip_data) continue;
                void* template_id = nullptr;
                void* enhance_levels = nullptr;
                ReadValue(equip_data, g_item_inst_template_id, template_id);
                ReadValue(equip_data, g_equip_enhance_levels, enhance_levels);
                std::string template_id_str = CopyString(template_id);
                if (!template_id_str.empty()) {
                    equipments.push_back({
                        equip_slot, template_id_str, ReadIntDictionary(enhance_levels)});
                }
            }
        }
        const size_t skill_group_count = skill_groups.size();
        const size_t equipment_count = equipments.size();
        const size_t equip_suit_count = equip_suits.size();
        const std::string weapon_id = weapon ? weapon->template_id : std::string{};
        if (!char_id_str.empty()) {
            Session::SquadMember member;
            member.char_id = char_id_str;
            member.char_inst_id = char_inst_id;
            member.level = level;
            member.break_stage = break_stage;
            member.potential = potential;
            member.skill_groups = std::move(skill_groups);
            member.weapon = std::move(weapon);
            member.equipments = std::move(equipments);
            member.equip_suits = std::move(equip_suits);
            squad_members.push_back(std::move(member));
        }
        output << " [" << index << "] id=\"" << char_id_str
            << "\" inst=" << char_inst_id << " level=" << level
            << " breakStage=" << break_stage << " potential=" << potential
            << " skillGroups=" << skill_group_count
            << " weapon=\"" << weapon_id << "\""
            << " equips=" << equipment_count
            << " equipSuits=" << equip_suit_count
            << (char_info ? "" : " charInfo=missing");
    }
    Log(output.str());
    if (!squad_members.empty()) {
        std::scoped_lock lock(g_session_mutex);
        const auto belongs_to_squad = [&squad_members](std::string_view actor) {
            if (!StartsWithCharacterPrefix(actor)) return true;
            return std::any_of(squad_members.begin(), squad_members.end(),
                [actor](const Session::SquadMember& member) {
                    return _stricmp(member.char_id.c_str(),
                        std::string(actor).c_str()) == 0;
                });
        };
        size_t pruned_baselines = 0;
        if (g_session.active) {
            std::erase_if(g_session.buff_intervals,
                [&](const BuffInterval& interval) {
                    if (interval.start_time > 0.000001) return false;
                    const std::string owner = NormalizeAttackerId(interval.owner);
                    const bool stale_actor =
                        StartsWithCharacterPrefix(interval.credited_actor_id) &&
                        !belongs_to_squad(interval.credited_actor_id);
                    const bool stale_owner = StartsWithCharacterPrefix(owner) &&
                        !belongs_to_squad(owner);
                    if (!stale_actor && !stale_owner) return false;
                    ++pruned_baselines;
                    return true;
                });
            g_session.open_buff_intervals.clear();
            for (size_t interval_index = 0;
                 interval_index < g_session.buff_intervals.size(); ++interval_index) {
                const BuffInterval& interval = g_session.buff_intervals[interval_index];
                if (interval.active) {
                    g_session.open_buff_intervals[interval.inst_id] = interval_index;
                }
            }
        }
        g_session.squad = std::move(squad_members);
        if (pruned_baselines) {
            Log("[combat-squad] pruned stale baseline buffs=" +
                std::to_string(pruned_baselines));
        }
    }
}

struct AttributeMaskValue {
    uint64_t lower = 0;
    uint64_t higher = 0;
};

bool AttributeMaskContains(const AttributeMaskValue& mask, int32_t attribute_type) {
    if (attribute_type < 0 || attribute_type >= kAttributeTypeCount) return false;
    return attribute_type < 64
        ? (mask.lower & (uint64_t{1} << attribute_type)) != 0
        : (mask.higher & (uint64_t{1} << (attribute_type - 64))) != 0;
}

bool IsIdentityScalar(double value) {
    return std::isfinite(value) && std::abs(value - 1.0) <= 1.0e-10;
}

bool HasAttributeContribution(
    const BuffDiagnosticInfo::AttributeContribution& value) {
    return std::isfinite(value.addition) && std::isfinite(value.multiplier) &&
        std::isfinite(value.final_addition) && std::isfinite(value.final_scalar) &&
        std::isfinite(value.base_addition) &&
        std::isfinite(value.base_multiplier) &&
        std::isfinite(value.base_final_addition) &&
        std::isfinite(value.base_final_scalar) &&
        (std::abs(value.addition) > 1.0e-10 ||
            std::abs(value.multiplier) > 1.0e-10 ||
            std::abs(value.final_addition) > 1.0e-10 ||
            !IsIdentityScalar(value.final_scalar) ||
            std::abs(value.base_addition) > 1.0e-10 ||
            std::abs(value.base_multiplier) > 1.0e-10 ||
            std::abs(value.base_final_addition) > 1.0e-10 ||
            !IsIdentityScalar(value.base_final_scalar));
}

bool TryReadBuffAttributeContribution(void* buff, int32_t attribute_type,
    BuffDiagnosticInfo::AttributeContribution& contribution, bool& has_value,
    bool& has_base_value) {
    has_value = false;
    has_base_value = false;
    __try {
        has_value = reinterpret_cast<BuffAttributeGetValueFn>(
            g_buff_attribute_get_value.pointer)(buff, attribute_type,
            &contribution.addition, &contribution.multiplier,
            &contribution.final_addition, &contribution.final_scalar,
            const_cast<void*>(g_buff_attribute_get_value.method_info));
        has_base_value = reinterpret_cast<BuffAttributeGetValueFn>(
            g_buff_attribute_get_base_value.pointer)(buff, attribute_type,
            &contribution.base_addition, &contribution.base_multiplier,
            &contribution.base_final_addition, &contribution.base_final_scalar,
            const_cast<void*>(g_buff_attribute_get_base_value.method_info));
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        has_value = false;
        has_base_value = false;
        return false;
    }
}

std::vector<BuffDiagnosticInfo::AttributeContribution>
CaptureBuffAttributeContributions(void* buff) {
    std::vector<BuffDiagnosticInfo::AttributeContribution> result;
    if (!buff || !g_buff_attribute_contract_ready) return result;
    AttributeMaskValue mask{};
    if (!ReadValue(buff, g_buff_attribute_mask, mask)) return result;
    for (int32_t attribute_type = 0; attribute_type < kAttributeTypeCount;
         ++attribute_type) {
        if (!AttributeMaskContains(mask, attribute_type)) continue;
        BuffDiagnosticInfo::AttributeContribution contribution{};
        contribution.attribute_type = attribute_type;
        bool has_value = false;
        bool has_base_value = false;
        TryReadBuffAttributeContribution(buff, attribute_type, contribution,
            has_value, has_base_value);
        if ((has_value || has_base_value) && HasAttributeContribution(contribution)) {
            result.push_back(contribution);
        }
    }
    return result;
}

void RefreshActiveBuffAttributeContributions(void* buff) {
    if (!buff || (!g_buff_attribute_contract_ready &&
        !g_arts_strength_contract_ready)) return;
    const uint64_t inst_id = ReadBuffInstId(buff);
    if (!inst_id) return;
    auto contributions = g_buff_attribute_contract_ready
        ? CaptureBuffAttributeContributions(buff)
        : std::vector<BuffDiagnosticInfo::AttributeContribution>{};
    void* blackboard = nullptr;
    ReadValue(buff, g_buff_blackboard, blackboard);
    std::string buff_id;
    {
        std::scoped_lock lock(g_buff_diagnostic_mutex);
        const auto found = g_buff_diagnostic_map.find(inst_id);
        if (found != g_buff_diagnostic_map.end()) buff_id = found->second.buff_id;
    }
    double arts_strength_points = 0.0;
    const bool has_arts_strength_points = TryReadArtsStrengthPoints(
        blackboard, buff_id, arts_strength_points);
    std::scoped_lock lock(g_buff_diagnostic_mutex);
    const auto found = g_buff_diagnostic_map.find(inst_id);
    if (found != g_buff_diagnostic_map.end()) {
        if (g_buff_attribute_contract_ready) {
            found->second.attribute_contributions = std::move(contributions);
        }
        found->second.blackboard = blackboard;
        found->second.arts_strength_points = arts_strength_points;
        found->second.has_arts_strength_points = has_arts_strength_points;
    }
}

void AppendModifierDiagnostics(std::ostringstream& output, const char* side,
    void* repeated_field) {
    RepeatedObjects repeated;
    if (!ReadRepeatedObjects(repeated_field, repeated)) {
        output << " " << side << "=<layout-error>";
        return;
    }
    output << " " << side << "=" << repeated.count << "[";
    const int32_t limit = std::min(repeated.count, 24);
    for (int32_t index = 0; index < limit; ++index) {
        if (index) output << ",";
        void* handle = RepeatedObjectAt(repeated, index);
        uint64_t buff_inst_id = 0;
        int32_t modifier_index = -1;
        void* processor_args = nullptr;
        if (!handle ||
            !ReadValue(handle, g_modifier_handle_buff_inst_id, buff_inst_id) ||
            !ReadValue(handle, g_modifier_handle_index, modifier_index) ||
            !ReadValue(handle, g_modifier_handle_processor_debug_args, processor_args)) {
            output << "?";
            continue;
        }
        output << "{buff=" << buff_inst_id << ",idx=" << modifier_index
            << ",processors=" << ManagedMapCount(processor_args);
        {
            std::scoped_lock lock(g_buff_diagnostic_mutex);
            const auto found = g_buff_diagnostic_map.find(buff_inst_id);
            if (found != g_buff_diagnostic_map.end()) {
                output << ",id=\"" << found->second.buff_id << "\""
                    << ",src=\"" << found->second.source << "\"";
            }
        }
        output << "}";
    }
    if (repeated.count > limit) output << ",...";
    output << "]";
}

void LogRdpsDamageDiagnostic(void* modifier) {
    const Configuration config = ConfigurationSnapshot();
    if (!config.diagnostics || !g_rdps_diagnostic_contract_ready || !modifier) return;
    const uint32_t sample = g_rdps_damage_samples.fetch_add(1, std::memory_order_relaxed);
    if (sample >= 64) return;
    void* detail = ReadDamageServerDetail(modifier);
    if (!detail) {
        Log("[rdps-diag] damage sample=" + std::to_string(sample) +
            " serverDetail=null");
        return;
    }
    void* modifier_args = nullptr;
    void* global_processor_args = nullptr;
    ReadValue(detail, g_server_detail_modifier_args, modifier_args);
    ReadValue(detail, g_server_detail_processor_debug_args, global_processor_args);
    std::ostringstream output;
    output << "[rdps-diag] damage sample=" << sample << " serverDetail=present"
        << " globalProcessors=" << ManagedMapCount(global_processor_args);
    if (!modifier_args) {
        output << " modifierArgs=null";
        Log(output.str());
        return;
    }
    void* attacker = nullptr;
    void* defender = nullptr;
    ReadValue(modifier_args, g_modifier_args_attacker_modifiers, attacker);
    ReadValue(modifier_args, g_modifier_args_defender_modifiers, defender);
    AppendModifierDiagnostics(output, "attacker", attacker);
    AppendModifierDiagnostics(output, "defender", defender);
    Log(output.str());
}

constexpr size_t kRdpsArraySampleCapacity = 128;
constexpr size_t kRdpsZoneCallCapacity = 16;

struct DoubleArraySnapshot {
    bool valid = false;
    uint32_t count = 0;
    void* managed_array = nullptr;
    std::array<double, kRdpsArraySampleCapacity> values{};
};

struct DamagePackNumericSnapshot {
    bool valid = false;
    double calc_result = 0.0;
    float atk_scale = 0.0f;
    DoubleArraySnapshot attacker_zones;
    DoubleArraySnapshot attacker_attributes;
    DoubleArraySnapshot defender_zones;
    DoubleArraySnapshot defender_attributes;
};

struct ZoneCallSnapshot {
    int32_t side = -1;
    double value = 0.0;
    char name[96]{};
};

struct NumericHookContext {
    bool active = false;
    uint32_t zone_count = 0;
    void** modifier_handle_reference = nullptr;
    std::array<ZoneCallSnapshot, kRdpsZoneCallCapacity> zones{};
};

thread_local NumericHookContext g_numeric_hook_context;
thread_local int32_t g_damage_processor_timing = -1;

struct RdpsModifierSample {
    uint64_t buff_inst_id = 0;
    int32_t side = -1;
    uint32_t zone_index = 0;
    double delta = 0.0;
    RdpsContributionKind kind = RdpsContributionKind::Other;
    SemanticZone semantic_zone = SemanticZone::Unknown;
    SemanticStatus semantic_status = SemanticStatus::Unknown;
    char buff_id[192]{};
    char source[160]{};
};

struct RdpsFactorSample {
    uint64_t buff_inst_id = 0;
    double multiplier = 1.0;
    RdpsContributionKind kind = RdpsContributionKind::Other;
    SemanticZone semantic_zone = SemanticZone::Unknown;
    char buff_id[192]{};
    char source[160]{};
};

struct DamageFlowContext {
    bool active = false;
    bool calculated = false;
    uint64_t generation = 0;
    uint64_t transaction = 0;
    void* pack = nullptr;
    void* server_detail = nullptr;
    void* attacker_ability = nullptr;
    void* defender_ability = nullptr;
    int32_t damage_type = -1;
    uint64_t decorate_mask = 0;
    char attacker_name[160]{};
    char defender_name[160]{};
    char origin_skill[192]{};
    double calculated_damage = 0.0;
    uint8_t modifier_count = 0;
    uint8_t factor_count = 0;
    std::array<RdpsModifierSample, kMaxRdpsModifiersPerHit> modifiers{};
    std::array<RdpsFactorSample, kMaxRdpsModifiersPerHit> factors{};
};

thread_local DamageFlowContext g_damage_flow_context;

struct CompletedDamageFlow {
    uint64_t generation = 0;
    uint64_t transaction = 0;
    void* server_detail = nullptr;
    double calculated_damage = 0.0;
    uint8_t rdps_share_count = 0;
    std::array<RdpsShare, kMaxRdpsSourcesPerHit> rdps_shares{};
};

thread_local std::deque<CompletedDamageFlow> g_completed_damage_flows;
constexpr size_t kCompletedDamageFlowCapacity = 64;

void BeginDamageFlow(void* pack_data) {
    g_damage_flow_context = {};
    g_damage_flow_context.active = true;
    g_damage_flow_context.generation =
        g_rdps_flow_generation.load(std::memory_order_acquire);
    g_damage_flow_context.pack = pack_data;
    ReadValueTypeField(pack_data, g_pack_attacker,
        g_damage_flow_context.attacker_ability);
    ReadValueTypeField(pack_data, g_pack_defender,
        g_damage_flow_context.defender_ability);
    ReadValueTypeField(pack_data, g_pack_damage_type,
        g_damage_flow_context.damage_type);
    ReadValueTypeField(pack_data, g_pack_damage_decorate_mask,
        g_damage_flow_context.decorate_mask);
    const std::string attacker_name = ResolveAbilityName(
        g_damage_flow_context.attacker_ability);
    const std::string defender_name = ResolveAbilityName(
        g_damage_flow_context.defender_ability);
    const std::string origin_skill = ResolveSkillIdFromCastInfo(
        EmbeddedValueType(pack_data, g_pack_skill_cast_info));
    std::snprintf(g_damage_flow_context.attacker_name,
        sizeof(g_damage_flow_context.attacker_name), "%s", attacker_name.c_str());
    std::snprintf(g_damage_flow_context.defender_name,
        sizeof(g_damage_flow_context.defender_name), "%s", defender_name.c_str());
    std::snprintf(g_damage_flow_context.origin_skill,
        sizeof(g_damage_flow_context.origin_skill), "%s", origin_skill.c_str());
    g_damage_flow_context.transaction =
        g_rdps_flow_sequence.fetch_add(1, std::memory_order_relaxed) + 1;
}

uint64_t CurrentDamageTransaction(void* pack_data) {
    return g_damage_flow_context.active && g_damage_flow_context.pack == pack_data
        ? g_damage_flow_context.transaction : 0;
}

std::string NormalizeAttackerId(std::string value);

bool StartsWithCharacterPrefix(std::string_view value) {
    return value.size() >= 4 &&
        std::tolower(static_cast<unsigned char>(value[0])) == 'c' &&
        std::tolower(static_cast<unsigned char>(value[1])) == 'h' &&
        std::tolower(static_cast<unsigned char>(value[2])) == 'r' &&
        value[3] == '_';
}

void AppendAttributionReason(std::string& target, std::string_view reason) {
    if (reason.empty()) return;
    if (!target.empty()) target.push_back(';');
    target.append(reason);
}

std::string CanonicalSourceKind(std::string_view value) {
    if (value == "suit" || value == "equip_suit") return "equipSuit";
    if (value == "weapon") return "weapon";
    if (value == "characterSkill") return "characterSkill";
    if (value == "characterPassive") return "characterPassive";
    if (value == "character") return "character";
    if (value == "common" || value == "system") return "system";
    return {};
}

std::string ResolveAbilitySourceActor(void* ability, bool& used_source_chain) {
    used_source_chain = false;
    std::array<void*, 8> visited{};
    size_t visited_count = 0;
    void* current = ability;
    while (current && visited_count < visited.size()) {
        if (std::find(visited.begin(), visited.begin() + visited_count,
                current) != visited.begin() + visited_count) {
            break;
        }
        visited[visited_count++] = current;
        const std::string actor = NormalizeAttackerId(
            ResolveAbilityName(current));
        if (StartsWithCharacterPrefix(actor)) return actor;
        void* source = nullptr;
        if (!ReadValue(current, g_ability_source, source) ||
            !source || source == current) {
            break;
        }
        used_source_chain = true;
        current = source;
    }
    return {};
}

struct BuffSourceMatch {
    bool resolved = false;
    bool had_candidates = false;
    bool no_loadout_match = false;
    bool ambiguous = false;
    bool trigger_matched = false;
    bool skill_conflict = false;
    std::string kind;
    std::string template_id;
    std::string skill_id;
};

// Matches the exporter's ownership candidates for one Buff against a squad
// member's verified configuration. Pure data: safe both at apply time and in
// the save-time re-resolution pass once the squad snapshot exists.
BuffSourceMatch MatchBuffSourceCandidates(const CombatSemanticCatalog& catalog,
    const Session::SquadMember* member, const std::string& buff_id,
    const std::string& origin_skill) {
    BuffSourceMatch match;
    const std::vector<BuffSourceCandidate>* mapped =
        catalog.FindBuffSources(buff_id);
    if (!mapped || mapped->empty()) return match;
    match.had_candidates = true;
    std::vector<const BuffSourceCandidate*> eligible;
    std::vector<const BuffSourceCandidate*> trigger_matches;
    if (member) {
        for (const BuffSourceCandidate& candidate : *mapped) {
            bool configuration_match = false;
            if (candidate.kind == "characterSkill" ||
                candidate.kind == "characterPassive") {
                configuration_match = _stricmp(member->char_id.c_str(),
                    candidate.template_id.c_str()) == 0;
            } else if (candidate.kind == "weapon") {
                configuration_match = member->weapon && _stricmp(
                    member->weapon->template_id.c_str(),
                    candidate.template_id.c_str()) == 0;
            } else if (candidate.kind == "equipSuit") {
                configuration_match = std::any_of(
                    member->equip_suits.begin(), member->equip_suits.end(),
                    [&candidate](const Session::SquadMember::EquipSuit& suit) {
                        return suit.count > 0 && _stricmp(suit.suit_id.c_str(),
                            candidate.template_id.c_str()) == 0;
                    });
            } else if (candidate.kind == "system") {
                configuration_match = true;
            }
            if (!configuration_match) continue;
            eligible.push_back(&candidate);
            if (!origin_skill.empty() && !candidate.trigger_skill_id.empty() &&
                _stricmp(catalog.ResolveSkillId(origin_skill).c_str(),
                    catalog.ResolveSkillId(
                        candidate.trigger_skill_id).c_str()) == 0) {
                trigger_matches.push_back(&candidate);
            }
        }
    }
    const std::vector<const BuffSourceCandidate*>& selected =
        trigger_matches.empty() ? eligible : trigger_matches;
    if (selected.empty()) {
        match.no_loadout_match = true;
        return match;
    }
    match.trigger_matched = !trigger_matches.empty();
    const BuffSourceCandidate* first = selected.front();
    std::string source_skill = first->source_skill_id;
    for (const BuffSourceCandidate* candidate : selected) {
        if (candidate->kind != first->kind || _stricmp(
                candidate->template_id.c_str(),
                first->template_id.c_str()) != 0) {
            match.ambiguous = true;
            return match;
        }
        if (!candidate->source_skill_id.empty() && !source_skill.empty() &&
            _stricmp(candidate->source_skill_id.c_str(),
                source_skill.c_str()) != 0) {
            match.skill_conflict = true;
        } else if (source_skill.empty()) {
            source_skill = candidate->source_skill_id;
        }
    }
    match.resolved = true;
    match.kind = first->kind;
    match.template_id = first->template_id;
    if (!match.skill_conflict) match.skill_id = std::move(source_skill);
    return match;
}

// A runtime-observed origin skill whose own configuration directly references
// the Buff is exact evidence: the game reported which skill created the Buff
// and the static config confirms that skill attaches it. Credits the runtime
// actor as a characterSkill source without any candidate table.
bool ApplyOriginSkillReference(const CombatSemanticCatalog& catalog,
    const std::string& origin_skill, const std::string& buff_id,
    const std::string& credited_actor, const std::string& actor_attribution,
    std::string& out_kind, std::string& out_template, std::string& out_skill) {
    if (origin_skill.empty() || credited_actor.empty() ||
        actor_attribution != "runtimeExact" ||
        !StartsWithCharacterPrefix(credited_actor) ||
        !catalog.HasSkillBuffReference(origin_skill, buff_id)) {
        return false;
    }
    out_kind = "characterSkill";
    out_template = credited_actor;
    out_skill = origin_skill;
    return true;
}

void ResolveExactBuffAttribution(BuffDiagnosticInfo& info) {
    bool used_source_chain = false;
    const std::string source_actor = ResolveAbilitySourceActor(
        info.source_ability, used_source_chain);
    if (!source_actor.empty()) {
        info.credited_actor_id = source_actor;
        info.actor_attribution = "runtimeExact";
        if (used_source_chain) {
            AppendAttributionReason(info.attribution_reason,
                "runtime_source_chain_resolved");
        }
    } else {
        AppendAttributionReason(info.attribution_reason,
            info.source.empty() ? "runtime_source_missing" :
                "runtime_source_is_non_character_entity");
    }

    const auto catalog = SemanticCatalogSnapshot();
    if (catalog) {
        BuffSourceMatch match;
        {
            std::scoped_lock lock(g_session_mutex);
            const Session::SquadMember* member = nullptr;
            if (info.actor_attribution == "runtimeExact") {
                for (const auto& value : g_session.squad) {
                    if (_stricmp(value.char_id.c_str(),
                            info.credited_actor_id.c_str()) == 0) {
                        member = &value;
                        break;
                    }
                }
            }
            match = MatchBuffSourceCandidates(*catalog, member, info.buff_id,
                info.origin_skill);
        }
        if (match.resolved) {
            info.source_kind = std::move(match.kind);
            info.source_template_id = std::move(match.template_id);
            info.source_skill_id = std::move(match.skill_id);
            info.source_attribution = "dictionaryExact";
            AppendAttributionReason(info.attribution_reason,
                match.trigger_matched ?
                    "source_reference_trigger_and_loadout_exact" :
                    "source_reference_and_loadout_exact");
            if (match.skill_conflict) {
                AppendAttributionReason(info.attribution_reason,
                    "source_skill_candidates_conflict");
            }
            return;
        }
        if (match.ambiguous) {
            AppendAttributionReason(info.attribution_reason,
                "source_reference_candidates_ambiguous");
        } else if (match.no_loadout_match) {
            AppendAttributionReason(info.attribution_reason,
                "source_reference_no_loadout_match");
        }
        std::string edge_kind;
        std::string edge_template;
        std::string edge_skill;
        if (ApplyOriginSkillReference(*catalog, info.origin_skill, info.buff_id,
                info.credited_actor_id, info.actor_attribution,
                edge_kind, edge_template, edge_skill)) {
            info.source_kind = std::move(edge_kind);
            info.source_template_id = std::move(edge_template);
            info.source_skill_id = std::move(edge_skill);
            info.source_attribution = "dictionaryExact";
            AppendAttributionReason(info.attribution_reason,
                "origin_skill_config_reference");
            return;
        }
    }

    const BuffSemantic* semantic = catalog ? catalog->FindBuff(info.buff_id) : nullptr;
    if (!semantic || semantic->is_dynamic) {
        AppendAttributionReason(info.attribution_reason,
            semantic ? "dynamic_semantic_mapping_rejected" : "semantic_mapping_missing");
        return;
    }

    std::string source_kind;
    std::string source_id;
    std::string source_skill;
    bool found_exact_mapping = false;
    bool conflict = false;
    bool skill_conflict = false;
    for (const SemanticEffect& effect : semantic->effects) {
        const std::string kind = CanonicalSourceKind(effect.source_kind);
        if (kind.empty() || effect.source_id.empty()) continue;
        const bool exact_evidence = effect.status == SemanticStatus::Verified ||
            effect.confidence == "compiled_from_unpack";
        if (!exact_evidence) continue;
        if (!found_exact_mapping) {
            source_kind = kind;
            source_id = effect.source_id;
            source_skill = effect.skill_id;
            found_exact_mapping = true;
            continue;
        }
        if (source_kind != kind || source_id != effect.source_id) {
            conflict = true;
            break;
        }
        if (source_skill.empty()) {
            source_skill = effect.skill_id;
        } else if (!effect.skill_id.empty() && source_skill != effect.skill_id) {
            skill_conflict = true;
        }
    }
    if (!found_exact_mapping || conflict) {
        AppendAttributionReason(info.attribution_reason,
            conflict ? "semantic_mapping_conflict" :
                "semantic_mapping_not_exact");
        return;
    }

    if ((source_kind == "character" || source_kind == "characterSkill" ||
            source_kind == "characterPassive") &&
        info.actor_attribution == "runtimeExact" &&
        _stricmp(source_id.c_str(), info.credited_actor_id.c_str()) != 0) {
        AppendAttributionReason(info.attribution_reason,
            "runtime_actor_and_semantic_source_conflict");
        return;
    }
    info.source_kind = std::move(source_kind);
    info.source_template_id = std::move(source_id);
    if (!skill_conflict) info.source_skill_id = std::move(source_skill);
    info.source_attribution = "dictionaryExact";
    if (skill_conflict) {
        AppendAttributionReason(info.attribution_reason,
            "semantic_source_skill_conflict");
    }
}

std::string_view CreditedBuffSource(const BuffDiagnosticInfo& buff) {
    if (buff.actor_attribution == "runtimeExact" &&
        !buff.credited_actor_id.empty()) {
        return buff.credited_actor_id;
    }
    return buff.source;
}

bool IsExternalDamageSource(const BuffDiagnosticInfo& buff) {
    const std::string source = NormalizeAttackerId(
        std::string(CreditedBuffSource(buff)));
    if (!StartsWithCharacterPrefix(source)) return false;
    const std::string attacker = NormalizeAttackerId(
        g_damage_flow_context.attacker_name);
    return !source.empty() && !attacker.empty() &&
        _stricmp(source.c_str(), attacker.c_str()) != 0;
}

const DoubleArraySnapshot* RdpsZoneArray(
    const DamagePackNumericSnapshot& snapshot, int32_t side) {
    if (side == 0) return &snapshot.attacker_zones;
    if (side == 1) return &snapshot.defender_zones;
    return nullptr;
}

SemanticZone RuntimeSemanticZone(int32_t side, uint32_t index) {
    if (side == 0) {
        if (index == 1) return SemanticZone::DamageIncrease;
        if (index == 3) return SemanticZone::Amplification;
        if (index == 4) return SemanticZone::Combo;
    } else if (side == 1) {
        if (index == 1) return SemanticZone::VulnerabilityTaken;
        if (index == 5) return SemanticZone::Fragile;
    }
    return SemanticZone::Unknown;
}

struct SemanticResolution {
    SemanticStatus status = SemanticStatus::Unknown;
    SemanticZone zone = SemanticZone::Unknown;
    bool element_mismatch = false;
};

SemanticResolution ResolveBuffSemantic(std::string_view buff_id,
    SemanticZone observed_zone, int32_t damage_type) {
    SemanticResolution result;
    result.zone = observed_zone;
    const auto catalog = SemanticCatalogSnapshot();
    if (!catalog || buff_id.empty()) return result;
    const BuffSemantic* semantic = catalog->FindBuff(buff_id);
    if (!semantic) return result;
    if (semantic->status == SemanticStatus::Excluded ||
        semantic->status == SemanticStatus::Structural) {
        result.status = semantic->status;
        return result;
    }

    bool candidate = false;
    bool zone_seen = false;
    for (const SemanticEffect& effect : semantic->effects) {
        if (observed_zone != SemanticZone::Unknown && effect.zone != observed_zone) {
            continue;
        }
        zone_seen = true;
        if (!CombatSemanticCatalog::ElementMatches(effect.element, damage_type)) {
            result.element_mismatch = true;
            continue;
        }
        if (effect.status == SemanticStatus::Verified) {
            result.status = SemanticStatus::Verified;
            result.zone = effect.zone;
            return result;
        }
        candidate = candidate || effect.status == SemanticStatus::Candidate;
    }
    if (candidate || semantic->status == SemanticStatus::Candidate) {
        result.status = SemanticStatus::Candidate;
    } else if (zone_seen && result.element_mismatch) {
        result.status = SemanticStatus::Unknown;
    }
    return result;
}

const char* SemanticStatusId(SemanticStatus status) {
    switch (status) {
    case SemanticStatus::Verified: return "verified";
    case SemanticStatus::Candidate: return "candidate";
    case SemanticStatus::Excluded: return "excluded";
    case SemanticStatus::Structural: return "structural";
    default: return "unknown";
    }
}

void RecordSemanticResolution(const SemanticResolution& resolution,
    std::string_view buff_id = {},
    SemanticZone observed_zone = SemanticZone::Unknown) {
    g_semantic_observed.fetch_add(1, std::memory_order_relaxed);
    switch (resolution.status) {
    case SemanticStatus::Verified:
        g_semantic_verified.fetch_add(1, std::memory_order_relaxed);
        break;
    case SemanticStatus::Candidate:
        g_semantic_candidate.fetch_add(1, std::memory_order_relaxed);
        break;
    case SemanticStatus::Excluded:
    case SemanticStatus::Structural:
        g_semantic_excluded.fetch_add(1, std::memory_order_relaxed);
        break;
    default:
        g_semantic_unknown.fetch_add(1, std::memory_order_relaxed);
        break;
    }
    if (resolution.element_mismatch) {
        g_semantic_element_mismatch.fetch_add(1, std::memory_order_relaxed);
    }
    if (resolution.status == SemanticStatus::Verified || buff_id.empty()) return;
    const std::string key = std::string(SemanticStatusId(resolution.status)) +
        "|" + CombatSemanticCatalog::ZoneId(observed_zone) + "|" +
        std::string(buff_id) + (resolution.element_mismatch ? "|element" : "");
    std::scoped_lock lock(g_semantic_audit_mutex);
    const auto found = g_semantic_audit.find(key);
    if (found != g_semantic_audit.end()) {
        ++found->second.count;
        return;
    }
    constexpr size_t kMaxSemanticAuditItems = 256;
    if (g_semantic_audit.size() >= kMaxSemanticAuditItems) {
        g_semantic_unresolved_dropped.fetch_add(1, std::memory_order_relaxed);
        return;
    }
    Session::SemanticAuditItem item;
    item.buff_id = buff_id;
    item.status = resolution.status;
    item.observed_zone = observed_zone;
    item.count = 1;
    item.element_mismatch = resolution.element_mismatch;
    g_semantic_audit.emplace(key, std::move(item));
}

void CaptureRdpsModifierSamples(const DamagePackNumericSnapshot& before,
    const DamagePackNumericSnapshot& after, const BuffDiagnosticInfo& buff,
    uint64_t buff_inst_id) {
    if (!g_damage_flow_context.active || !IsExternalDamageSource(buff)) return;
    const auto capture = [&](int32_t side, const DoubleArraySnapshot& left,
        const DoubleArraySnapshot& right) {
        if (!left.valid || !right.valid) return;
        const size_t limit = std::min<size_t>(
            std::min(left.count, right.count), kRdpsArraySampleCapacity);
        for (size_t index = 0; index < limit; ++index) {
            const double delta = right.values[index] - left.values[index];
            if (!std::isfinite(delta) || delta <= 1.0e-9 ||
                g_damage_flow_context.modifier_count >=
                    g_damage_flow_context.modifiers.size()) {
                continue;
            }
            const SemanticZone zone = RuntimeSemanticZone(
                side, static_cast<uint32_t>(index));
            if (zone == SemanticZone::Unknown) {
                RecordSemanticResolution({}, buff.buff_id, zone);
                continue;
            }
            const SemanticResolution resolution = ResolveBuffSemantic(
                buff.buff_id, zone, g_damage_flow_context.damage_type);
            RecordSemanticResolution(resolution, buff.buff_id, zone);
            if (resolution.status != SemanticStatus::Verified ||
                resolution.zone == SemanticZone::Unknown) {
                continue;
            }
            RdpsModifierSample& sample = g_damage_flow_context.modifiers[
                g_damage_flow_context.modifier_count++];
            sample.buff_inst_id = buff_inst_id;
            sample.side = side;
            sample.zone_index = static_cast<uint32_t>(index);
            sample.delta = delta;
            sample.semantic_zone = resolution.zone;
            sample.semantic_status = resolution.status;
            sample.kind = ContributionKindForZone(resolution.zone);
            std::snprintf(sample.buff_id, sizeof(sample.buff_id), "%s",
                buff.buff_id.c_str());
            const std::string_view credited_source = CreditedBuffSource(buff);
            std::snprintf(sample.source, sizeof(sample.source), "%.*s",
                static_cast<int>(credited_source.size()), credited_source.data());
        }
    };
    capture(0, before.attacker_zones, after.attacker_zones);
    capture(1, before.defender_zones, after.defender_zones);
}

struct AttributeAggregate {
    std::array<double, kAttributeModifierValueCount> values{};
    double minimum = -std::numeric_limits<double>::max();
    double maximum = std::numeric_limits<double>::max();
};

struct AttributeSimulationGroup {
    int32_t side = -1;
    int32_t attribute_type = -1;
    void* ability = nullptr;
    SemanticZone zone = SemanticZone::Unknown;
    double full_value = 0.0;
    AttributeAggregate full{};
};

struct ActiveAttributeBuff {
    uint64_t inst_id = 0;
    std::string source;
    std::string buff_id;
    int32_t side = -1;
    RdpsContributionKind kind = RdpsContributionKind::Other;
    SemanticZone zone = SemanticZone::Unknown;
    std::vector<BuffDiagnosticInfo::AttributeContribution> contributions;
};

struct PersistentAttributeContributor {
    uint64_t inst_id = 0;
    std::string source;
    std::string buff_id;
    SemanticZone zone = SemanticZone::Unknown;
    RdpsContributionKind kind = RdpsContributionKind::Other;
    double rate = 0.0;
};

struct ArtsStrengthContributor {
    uint64_t inst_id = 0;
    std::string source;
    double points = 0.0;
};

struct ArtsStrengthEvidence {
    double multiplier = 1.0;
    double external_points = 0.0;
    std::vector<ArtsStrengthContributor> contributors;
};

void MarkBuffIntervalRelevant(uint64_t inst_id, RdpsContributionKind kind,
    double effect_value = 0.0,
    BuffEffectKind effect_kind = BuffEffectKind::None);

RdpsContributionKind ClassifyAttributeContribution(int32_t side,
    int32_t attribute_type) {
    if (side == 0 && attribute_type == 2) return RdpsContributionKind::Attack;
    if (side == 0 && (attribute_type == 17 || attribute_type == 28 ||
        attribute_type == 32 || attribute_type == 33 ||
        (attribute_type >= 50 && attribute_type <= 54))) {
        return RdpsContributionKind::DamageIncrease;
    }
    if (side == 0 && attribute_type >= 65 && attribute_type <= 68) {
        return RdpsContributionKind::Amplification;
    }
    if ((attribute_type >= 70 && attribute_type <= 74) ||
        (attribute_type >= 80 && attribute_type <= 85)) {
        return RdpsContributionKind::Fragile;
    }
    if (side == 1 && (attribute_type == 3 ||
        (attribute_type >= 94 && attribute_type <= 99))) {
        return RdpsContributionKind::Resistance;
    }
    return RdpsContributionKind::Other;
}

SemanticZone AttributeSemanticZone(int32_t side, int32_t attribute_type) {
    return side < 0 ? SemanticZone::Unknown : [&]() {
        const RdpsContributionKind kind =
            ClassifyAttributeContribution(side, attribute_type);
        switch (kind) {
        case RdpsContributionKind::Attack: return SemanticZone::Attack;
        case RdpsContributionKind::DamageIncrease:
            return SemanticZone::DamageIncrease;
        case RdpsContributionKind::Amplification:
            return SemanticZone::Amplification;
        case RdpsContributionKind::Fragile: return SemanticZone::Fragile;
        default: return SemanticZone::Unknown;
        }
    }();
}

SemanticElement AttributeSemanticElement(int32_t attribute_type) {
    switch (attribute_type) {
    case 50:
    case 70:
    case 80: return SemanticElement::Physical;
    case 51:
    case 65:
    case 71:
    case 84: return SemanticElement::Fire;
    case 52:
    case 66:
    case 72:
    case 83: return SemanticElement::Pulse;
    case 53:
    case 67:
    case 73:
    case 82: return SemanticElement::Cryst;
    case 54:
    case 68:
    case 74:
    case 81: return SemanticElement::Natural;
    case 32:
    case 33:
    case 85: return SemanticElement::Spell;
    default: return SemanticElement::All;
    }
}

bool AttributeAppliesToSkill(int32_t attribute_type, std::string_view skill) {
    if (attribute_type == 28) return skill.find("ultimate") != std::string_view::npos;
    if (attribute_type == 32) {
        return skill.find("normal_skill") != std::string_view::npos;
    }
    if (attribute_type == 33) return skill.find("combo") != std::string_view::npos;
    return true;
}

bool ReadManagedDouble(void* array, size_t index, double& output) {
    if (!array) return false;
    __try {
        const uintptr_t length = *reinterpret_cast<uintptr_t*>(
            reinterpret_cast<uint8_t*>(array) + 0x18);
        if (index >= length || length > 4096) return false;
        output = *reinterpret_cast<double*>(reinterpret_cast<uint8_t*>(array) +
            0x20 + index * sizeof(double));
        return std::isfinite(output);
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

void* GetAbilityAttributes(void* ability) {
    if (!ability || !g_ability_attributes_getter.pointer) return nullptr;
    __try {
        return reinterpret_cast<ObjectGetterFn>(g_ability_attributes_getter.pointer)(
            ability, const_cast<void*>(g_ability_attributes_getter.method_info));
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return nullptr;
    }
}

bool CaptureAttributeAggregate(void* ability, int32_t attribute_type,
    AttributeAggregate& output) {
    output = {};
    void* attributes = GetAbilityAttributes(ability);
    if (!attributes || !g_attributes_get_all_modifier_values.pointer) return false;
    void* values = nullptr;
    __try {
        values = reinterpret_cast<AttributeValuesFn>(
            g_attributes_get_all_modifier_values.pointer)(attributes, attribute_type,
            const_cast<void*>(g_attributes_get_all_modifier_values.method_info));
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
    if (!values) return false;
    for (size_t index = 0; index < output.values.size(); ++index) {
        if (!ReadManagedDouble(values, index, output.values[index])) return false;
    }
    void* minimum_values = nullptr;
    void* maximum_values = nullptr;
    if (!ReadValue(attributes, g_attributes_data_min, minimum_values) ||
        !ReadValue(attributes, g_attributes_data_max, maximum_values) ||
        !ReadManagedDouble(minimum_values, attribute_type, output.minimum) ||
        !ReadManagedDouble(maximum_values, attribute_type, output.maximum)) {
        return false;
    }
    return output.minimum <= output.maximum;
}

double CalculateFinalAttribute(const AttributeAggregate& aggregate) {
    if (!g_calculate_final_attribute_value.pointer) {
        return std::numeric_limits<double>::quiet_NaN();
    }
    __try {
        return reinterpret_cast<CalculateFinalAttributeFn>(
            g_calculate_final_attribute_value.pointer)(aggregate.values[0],
            aggregate.values[1], aggregate.values[2], aggregate.values[3],
            aggregate.values[4], aggregate.values[5], aggregate.values[6],
            aggregate.values[7], aggregate.values[8], aggregate.minimum,
            aggregate.maximum,
            const_cast<void*>(g_calculate_final_attribute_value.method_info));
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return std::numeric_limits<double>::quiet_NaN();
    }
}

bool CallPackDouble(const RuntimeMethod& method, void* pack_data, double& output) {
    if (!method.pointer || !pack_data) return false;
    __try {
        output = reinterpret_cast<PackDoubleFn>(method.pointer)(pack_data,
            const_cast<void*>(method.method_info));
        return std::isfinite(output);
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

bool ApplyAttributeContribution(AttributeAggregate& aggregate,
    const BuffDiagnosticInfo::AttributeContribution& contribution, bool add) {
    const double sign = add ? 1.0 : -1.0;
    aggregate.values[1] += sign * contribution.addition;
    aggregate.values[2] += sign * contribution.multiplier;
    aggregate.values[3] += sign * contribution.final_addition;
    aggregate.values[5] += sign * contribution.base_addition;
    aggregate.values[6] += sign * contribution.base_multiplier;
    aggregate.values[7] += sign * contribution.base_final_addition;
    const auto apply_scalar = [add](double& value, double scalar) {
        if (!std::isfinite(scalar) || std::abs(scalar) <= 1.0e-12) return false;
        value = add ? value * scalar : value / scalar;
        return std::isfinite(value);
    };
    return apply_scalar(aggregate.values[4], contribution.final_scalar) &&
        apply_scalar(aggregate.values[8], contribution.base_final_scalar);
}

bool SameRuntimeOwner(const BuffDiagnosticInfo& buff, int32_t side) {
    void* expected = side == 0 ? g_damage_flow_context.attacker_ability
                               : g_damage_flow_context.defender_ability;
    const char* expected_name = side == 0 ? g_damage_flow_context.attacker_name
                                          : g_damage_flow_context.defender_name;
    return (expected && buff.owner_ability == expected) ||
        (expected_name[0] != '\0' && !buff.owner.empty() &&
            _stricmp(expected_name, buff.owner.c_str()) == 0);
}

std::vector<ActiveAttributeBuff> CaptureActiveAttributeBuffsForDamage() {
    std::vector<ActiveAttributeBuff> result;
    std::scoped_lock lock(g_buff_diagnostic_mutex);
    for (const auto& [inst_id, buff] : g_buff_diagnostic_map) {
        if (buff.attribute_contributions.empty() ||
            !IsExternalDamageSource(buff)) {
            continue;
        }
        const std::string source = NormalizeAttackerId(
            std::string(CreditedBuffSource(buff)));
        const std::string owner = NormalizeAttackerId(buff.owner);
        if (source.empty() || _stricmp(source.c_str(), owner.c_str()) == 0) continue;
        int32_t side = -1;
        if (SameRuntimeOwner(buff, 0)) side = 0;
        else if (SameRuntimeOwner(buff, 1)) side = 1;
        if (side < 0) continue;

        std::array<std::vector<BuffDiagnosticInfo::AttributeContribution>,
            static_cast<size_t>(SemanticZone::ArtsStrength) + 1> by_zone;
        for (const auto& contribution : buff.attribute_contributions) {
            const SemanticZone zone = AttributeSemanticZone(
                side, contribution.attribute_type);
            if (zone == SemanticZone::Unknown ||
                !AttributeAppliesToSkill(contribution.attribute_type,
                    g_damage_flow_context.origin_skill) ||
                !CombatSemanticCatalog::ElementMatches(
                    AttributeSemanticElement(contribution.attribute_type),
                    g_damage_flow_context.damage_type)) {
                continue;
            }
            by_zone[static_cast<size_t>(zone)].push_back(contribution);
        }
        for (size_t zone_index = 1; zone_index < by_zone.size(); ++zone_index) {
            if (by_zone[zone_index].empty()) continue;
            const SemanticZone zone = static_cast<SemanticZone>(zone_index);
            const SemanticResolution semantic = ResolveBuffSemantic(
                buff.buff_id, zone, g_damage_flow_context.damage_type);
            RecordSemanticResolution(semantic, buff.buff_id, zone);
            if (semantic.status != SemanticStatus::Verified ||
                semantic.zone != zone) {
                continue;
            }
            ActiveAttributeBuff candidate;
            candidate.inst_id = inst_id;
            candidate.source = std::string(CreditedBuffSource(buff));
            candidate.buff_id = buff.buff_id;
            candidate.side = side;
            candidate.zone = zone;
            candidate.kind = ContributionKindForZone(zone);
            candidate.contributions = std::move(by_zone[zone_index]);
            result.push_back(std::move(candidate));
        }
    }
    return result;
}

bool IsArtsStrengthDamage(std::string_view origin_skill) {
    std::string skill(origin_skill);
    if (const auto catalog = SemanticCatalogSnapshot()) {
        skill = catalog->ResolveSkillId(skill);
    }
    std::transform(skill.begin(), skill.end(), skill.begin(),
        [](unsigned char value) { return static_cast<char>(std::tolower(value)); });
    if (skill == "buff_physical_airborne" ||
        skill == "buff_physical_crushed" ||
        skill == "buff_physical_knockdown" ||
        skill == "buff_physical_do_fracture") {
        return true;
    }
    static constexpr std::array<std::string_view, 4> kPrefixes{
        "buff_common_fire_", "buff_common_pulse_",
        "buff_common_cryst_", "buff_common_natural_"};
    return skill.ends_with("triggered") &&
        std::any_of(kPrefixes.begin(), kPrefixes.end(),
            [&skill](std::string_view prefix) { return skill.starts_with(prefix); });
}

ArtsStrengthEvidence CaptureArtsStrengthEvidence() {
    ArtsStrengthEvidence result;
    if (!g_arts_strength_contract_ready ||
        !IsArtsStrengthDamage(g_damage_flow_context.origin_skill)) {
        return result;
    }

    double self_points = 0.0;
    std::scoped_lock lock(g_buff_diagnostic_mutex);
    for (const auto& [inst_id, buff] : g_buff_diagnostic_map) {
        if (!buff.has_arts_strength_points ||
            !std::isfinite(buff.arts_strength_points) ||
            buff.arts_strength_points <= 1.0e-12 ||
            !SameRuntimeOwner(buff, 0) ||
            !StartsWithCharacterPrefix(CreditedBuffSource(buff))) {
            continue;
        }
        const SemanticResolution semantic = ResolveBuffSemantic(buff.buff_id,
            SemanticZone::ArtsStrength, g_damage_flow_context.damage_type);
        RecordSemanticResolution(semantic, buff.buff_id,
            SemanticZone::ArtsStrength);
        if (semantic.status != SemanticStatus::Verified ||
            semantic.zone != SemanticZone::ArtsStrength) {
            continue;
        }
        const std::string source = NormalizeAttackerId(
            std::string(CreditedBuffSource(buff)));
        const std::string owner = NormalizeAttackerId(buff.owner);
        if (source.empty()) continue;
        if ((!owner.empty() && _stricmp(source.c_str(), owner.c_str()) == 0) ||
            (g_damage_flow_context.attacker_name[0] != '\0' &&
                _stricmp(source.c_str(),
                    g_damage_flow_context.attacker_name) == 0)) {
            self_points += buff.arts_strength_points;
            continue;
        }
        result.external_points += buff.arts_strength_points;
        result.contributors.push_back(
            {inst_id, source, buff.arts_strength_points});
    }
    const double baseline = 100.0 + std::max(0.0, self_points);
    if (baseline > 1.0e-12 && result.external_points > 1.0e-12) {
        result.multiplier =
            (baseline + result.external_points) / baseline;
    }
    return result;
}

void MergeRdpsShare(std::array<RdpsShare, kMaxRdpsSourcesPerHit>& output,
    uint8_t& output_count, std::string_view source, double fraction,
    RdpsContributionKind kind) {
    if (source.empty() || !std::isfinite(fraction) || fraction <= 1.0e-12) return;
    for (uint8_t index = 0; index < output_count; ++index) {
        if (output[index].kind == kind &&
            _stricmp(output[index].source, std::string(source).c_str()) == 0) {
            output[index].fraction += fraction;
            return;
        }
    }
    if (output_count >= output.size()) return;
    RdpsShare& destination = output[output_count++];
    std::snprintf(destination.source, sizeof(destination.source), "%.*s",
        static_cast<int>(source.size()), source.data());
    destination.fraction = fraction;
    destination.kind = kind;
}

#if 0 // Removed: this legacy counterfactual path wrote into live managed arrays.
void ApplyPersistentAttributeRdps(const DamagePackNumericSnapshot& snapshot,
    std::array<RdpsShare, kMaxRdpsSourcesPerHit>& output, uint8_t& output_count) {
    if (!g_buff_attribute_contract_ready || !g_damage_flow_context.pack) return;
    std::vector<ActiveAttributeBuff> buffs = CaptureActiveAttributeBuffsForDamage();
    if (buffs.empty()) return;

    std::vector<AttributeSimulationGroup> groups;
    const auto find_group = [&](int32_t side, int32_t attribute_type)
        -> AttributeSimulationGroup* {
        const auto found = std::find_if(groups.begin(), groups.end(),
            [side, attribute_type](const AttributeSimulationGroup& group) {
                return group.side == side && group.attribute_type == attribute_type;
            });
        return found == groups.end() ? nullptr : &*found;
    };
    for (const ActiveAttributeBuff& buff : buffs) {
        for (const auto& contribution : buff.contributions) {
            if (find_group(buff.side, contribution.attribute_type)) continue;
            const DoubleArraySnapshot& side_array = buff.side == 0
                ? snapshot.attacker_attributes : snapshot.defender_attributes;
            void* ability = buff.side == 0 ? g_damage_flow_context.attacker_ability
                                           : g_damage_flow_context.defender_ability;
            if (!side_array.valid || !side_array.managed_array || !ability ||
                contribution.attribute_type < 0 ||
                static_cast<uint32_t>(contribution.attribute_type) >= side_array.count) {
                continue;
            }
            AttributeSimulationGroup group;
            group.side = buff.side;
            group.attribute_type = contribution.attribute_type;
            group.ability = ability;
            group.managed_array = side_array.managed_array;
            if (!ReadManagedDouble(group.managed_array, group.attribute_type,
                    group.pack_value) ||
                !CaptureAttributeAggregate(ability, group.attribute_type, group.full)) {
                continue;
            }
            group.calculated_full_value = CalculateFinalAttribute(group.full);
            if (!std::isfinite(group.calculated_full_value)) continue;
            groups.push_back(std::move(group));
        }
    }
    if (groups.empty()) return;

    const double full_factor = EvaluatePersistentAttributeDamageFactor(
        g_damage_flow_context.pack, snapshot);
    if (!std::isfinite(full_factor)) return;

    const auto restore_groups = [&]() {
        for (const AttributeSimulationGroup& group : groups) {
            WriteManagedDouble(group.managed_array, group.attribute_type,
                group.pack_value);
        }
    };
    const auto patch_without = [&](const ActiveAttributeBuff* only_buff,
                                   bool remove_all) {
        bool changed = false;
        for (AttributeSimulationGroup& group : groups) {
            AttributeAggregate aggregate = group.full;
            for (const ActiveAttributeBuff& buff : buffs) {
                if ((!remove_all && &buff != only_buff) || buff.side != group.side) {
                    continue;
                }
                for (const auto& contribution : buff.contributions) {
                    if (contribution.attribute_type == group.attribute_type &&
                        ApplyAttributeContribution(aggregate, contribution, false)) {
                        changed = true;
                    }
                }
            }
            const double value = MapCalculatedAttributeToPack(group, aggregate);
            if (!WriteManagedDouble(group.managed_array, group.attribute_type, value)) {
                restore_groups();
                return false;
            }
        }
        return changed;
    };

    if (!patch_without(nullptr, true)) {
        restore_groups();
        return;
    }
    const double baseline_factor = EvaluatePersistentAttributeDamageFactor(
        g_damage_flow_context.pack, snapshot);
    restore_groups();
    if (!std::isfinite(baseline_factor) || baseline_factor >= full_factor - 1.0e-12) {
        return;
    }

    std::vector<double> marginal_gains(buffs.size(), 0.0);
    double marginal_sum = 0.0;
    for (size_t index = 0; index < buffs.size(); ++index) {
        if (!patch_without(&buffs[index], false)) {
            restore_groups();
            continue;
        }
        const double without_factor = EvaluatePersistentAttributeDamageFactor(
            g_damage_flow_context.pack, snapshot);
        restore_groups();
        if (!std::isfinite(without_factor)) continue;
        const double gain = std::max(0.0, full_factor - without_factor);
        marginal_gains[index] = gain;
        marginal_sum += gain;
    }
    if (marginal_sum <= 1.0e-12) return;

    const double attribute_fraction = std::clamp(
        (full_factor - baseline_factor) / full_factor, 0.0, 0.999999);
    const double direct_scale = std::clamp(
        baseline_factor / full_factor, 0.0, 1.0);
    for (uint8_t index = 0; index < output_count; ++index) {
        output[index].fraction *= direct_scale;
    }
    for (size_t index = 0; index < buffs.size(); ++index) {
        if (marginal_gains[index] <= 0.0) continue;
        const double share_fraction = attribute_fraction *
            marginal_gains[index] / marginal_sum;
        MarkBuffIntervalRelevant(buffs[index].inst_id, buffs[index].kind,
            share_fraction, BuffEffectKind::DamageShare);
        MergeRdpsShare(output, output_count, buffs[index].source,
            share_fraction, buffs[index].kind);
    }

    const Configuration config = ConfigurationSnapshot();
    const uint32_t sample = config.diagnostics
        ? g_rdps_attribute_share_samples.fetch_add(1, std::memory_order_relaxed)
        : 64;
    if (config.diagnostics && sample < 64) {
        std::ostringstream diagnostic;
        diagnostic << std::setprecision(10)
            << "[rdps-attribute-share] sample=" << sample
            << " txn=" << g_damage_flow_context.transaction
            << " attacker=\"" << g_damage_flow_context.attacker_name << "\""
            << " defender=\"" << g_damage_flow_context.defender_name << "\""
            << " buffs=" << buffs.size() << " groups=" << groups.size()
            << " factor=" << baseline_factor << "->" << full_factor
            << " fraction=" << attribute_fraction;
        Log(diagnostic.str());
    }
}
#endif

std::vector<PersistentAttributeContributor>
CapturePersistentAttributeContributors() {
    const std::vector<ActiveAttributeBuff> buffs =
        CaptureActiveAttributeBuffsForDamage();
    if (buffs.empty()) return {};

    struct ContributorKey {
        uint64_t inst_id = 0;
        std::string source;
        std::string buff_id;
        RdpsContributionKind kind = RdpsContributionKind::Other;
    };
    struct ZoneEvidence {
        double external_rate = 0.0;
        std::vector<std::pair<ContributorKey, double>> contributors;
    };
    std::array<ZoneEvidence,
        static_cast<size_t>(SemanticZone::ArtsStrength) + 1> zones;

    std::vector<AttributeSimulationGroup> groups;
    for (const ActiveAttributeBuff& buff : buffs) {
        for (const auto& contribution : buff.contributions) {
            const auto duplicate = std::find_if(groups.begin(), groups.end(),
                [&buff, &contribution](const AttributeSimulationGroup& group) {
                    return group.side == buff.side &&
                        group.attribute_type == contribution.attribute_type;
                });
            if (duplicate != groups.end()) continue;
            void* ability = buff.side == 0 ? g_damage_flow_context.attacker_ability
                                           : g_damage_flow_context.defender_ability;
            AttributeSimulationGroup group;
            group.side = buff.side;
            group.attribute_type = contribution.attribute_type;
            group.ability = ability;
            group.zone = buff.zone;
            if (!ability || !CaptureAttributeAggregate(
                    ability, group.attribute_type, group.full)) {
                continue;
            }
            group.full_value = CalculateFinalAttribute(group.full);
            if (!std::isfinite(group.full_value)) continue;
            groups.push_back(std::move(group));
        }
    }

    for (const AttributeSimulationGroup& group : groups) {
        AttributeAggregate baseline = group.full;
        bool baseline_changed = false;
        for (const ActiveAttributeBuff& buff : buffs) {
            if (buff.side != group.side || buff.zone != group.zone) continue;
            for (const auto& contribution : buff.contributions) {
                if (contribution.attribute_type == group.attribute_type &&
                    ApplyAttributeContribution(baseline, contribution, false)) {
                    baseline_changed = true;
                }
            }
        }
        if (!baseline_changed) continue;
        const double baseline_value = CalculateFinalAttribute(baseline);
        const double external_rate = group.full_value - baseline_value;
        if (!std::isfinite(baseline_value) || !std::isfinite(external_rate) ||
            external_rate <= 1.0e-12) {
            continue;
        }

        struct Marginal {
            const ActiveAttributeBuff* buff = nullptr;
            double value = 0.0;
        };
        std::vector<Marginal> marginals;
        double marginal_sum = 0.0;
        for (const ActiveAttributeBuff& buff : buffs) {
            if (buff.side != group.side || buff.zone != group.zone) continue;
            AttributeAggregate without = group.full;
            bool changed = false;
            for (const auto& contribution : buff.contributions) {
                if (contribution.attribute_type == group.attribute_type &&
                    ApplyAttributeContribution(without, contribution, false)) {
                    changed = true;
                }
            }
            if (!changed) continue;
            const double without_value = CalculateFinalAttribute(without);
            const double marginal = group.full_value - without_value;
            if (!std::isfinite(marginal) || marginal <= 1.0e-12) continue;
            marginals.push_back({&buff, marginal});
            marginal_sum += marginal;
        }
        if (marginal_sum <= 1.0e-12) continue;

        ZoneEvidence& zone = zones[static_cast<size_t>(group.zone)];
        zone.external_rate += external_rate;
        for (const Marginal& marginal : marginals) {
            const ActiveAttributeBuff& buff = *marginal.buff;
            const double normalized_rate = external_rate *
                marginal.value / marginal_sum;
            const auto existing = std::find_if(zone.contributors.begin(),
                zone.contributors.end(), [&buff](const auto& entry) {
                    return entry.first.inst_id == buff.inst_id;
                });
            if (existing != zone.contributors.end()) {
                existing->second += normalized_rate;
            } else {
                zone.contributors.push_back({
                    {buff.inst_id, buff.source, buff.buff_id, buff.kind},
                    normalized_rate});
            }
        }
    }

    std::vector<PersistentAttributeContributor> result;
    for (size_t zone_index = 1; zone_index < zones.size(); ++zone_index) {
        const SemanticZone semantic_zone = static_cast<SemanticZone>(zone_index);
        for (const auto& [key, rate] : zones[zone_index].contributors) {
            if (!std::isfinite(rate) || rate <= 1.0e-12) continue;
            result.push_back({key.inst_id, key.source, key.buff_id,
                semantic_zone, key.kind, rate});
        }
    }
    return result;
}

void BuildRdpsShares(const DamagePackNumericSnapshot& final_snapshot,
    std::array<RdpsShare, kMaxRdpsSourcesPerHit>& output, uint8_t& output_count) {
    output = {};
    output_count = 0;
    struct ZoneContributor {
        uint64_t inst_id = 0;
        std::string source;
        RdpsContributionKind kind = RdpsContributionKind::Other;
        double delta = 0.0;
    };
    struct ZoneState {
        SemanticZone semantic_zone = SemanticZone::Unknown;
        int32_t side = -1;
        uint32_t index = 0;
        double initial = 0.0;
        double final_value = 0.0;
        double total_delta = 0.0;
        double multiplier = 1.0;
        bool logarithmic_contributors = false;
        std::vector<ZoneContributor> contributors;
    };
    std::vector<ZoneState> zones;
    const auto find_zone = [&](int32_t side, uint32_t index) -> ZoneState* {
        const auto found = std::find_if(zones.begin(), zones.end(),
            [side, index](const ZoneState& zone) {
                return zone.side == side && zone.index == index;
            });
        return found == zones.end() ? nullptr : &*found;
    };
    for (uint8_t index = 0; index < g_damage_flow_context.modifier_count; ++index) {
        const RdpsModifierSample& modifier = g_damage_flow_context.modifiers[index];
        const DoubleArraySnapshot* array = RdpsZoneArray(final_snapshot, modifier.side);
        if (!array || !array->valid || modifier.zone_index >= array->count ||
            modifier.zone_index >= array->values.size()) {
            continue;
        }
        ZoneState* zone = find_zone(modifier.side, modifier.zone_index);
        if (!zone) {
            zones.push_back({modifier.semantic_zone, modifier.side,
                modifier.zone_index, 0.0, array->values[modifier.zone_index],
                0.0, 1.0, false, {}});
            zone = &zones.back();
        }
        zone->total_delta += modifier.delta;
        zone->contributors.push_back({modifier.buff_inst_id, modifier.source,
            modifier.kind, modifier.delta});
    }

    const auto location_for_zone = [](SemanticZone zone,
        int32_t& side, uint32_t& index) {
        switch (zone) {
        case SemanticZone::DamageIncrease: side = 0; index = 1; return true;
        case SemanticZone::Amplification: side = 0; index = 3; return true;
        case SemanticZone::Combo: side = 0; index = 4; return true;
        case SemanticZone::VulnerabilityTaken: side = 1; index = 1; return true;
        case SemanticZone::Fragile: side = 1; index = 5; return true;
        case SemanticZone::Attack: side = 0; index = UINT32_MAX; return true;
        default: return false;
        }
    };
    for (const PersistentAttributeContributor& contributor :
         CapturePersistentAttributeContributors()) {
        int32_t side = -1;
        uint32_t index = 0;
        if (!location_for_zone(contributor.zone, side, index)) continue;
        const bool already_captured = std::any_of(zones.begin(), zones.end(),
            [&contributor](const ZoneState& zone) {
                return zone.semantic_zone == contributor.zone &&
                    std::any_of(zone.contributors.begin(), zone.contributors.end(),
                        [&contributor](const ZoneContributor& existing) {
                            return existing.inst_id == contributor.inst_id;
                        });
            });
        if (already_captured) continue;

        ZoneState* zone = find_zone(side, index);
        if (!zone) {
            double final_value = std::numeric_limits<double>::quiet_NaN();
            if (contributor.zone == SemanticZone::Attack) {
                if (final_snapshot.attacker_attributes.valid &&
                    final_snapshot.attacker_attributes.count > 2) {
                    final_value = final_snapshot.attacker_attributes.values[2];
                }
            } else {
                const DoubleArraySnapshot* array = RdpsZoneArray(final_snapshot, side);
                if (array && array->valid && index < array->count &&
                    index < array->values.size()) {
                    final_value = array->values[index];
                }
            }
            if (!std::isfinite(final_value)) continue;
            zones.push_back({contributor.zone, side, index, 0.0, final_value,
                0.0, 1.0, false, {}});
            zone = &zones.back();
        }
        zone->total_delta += contributor.rate;
        zone->contributors.push_back({contributor.inst_id, contributor.source,
            contributor.kind, contributor.rate});
    }
    for (uint8_t index = 0; index < g_damage_flow_context.factor_count; ++index) {
        const RdpsFactorSample& factor = g_damage_flow_context.factors[index];
        if (factor.semantic_zone == SemanticZone::Unknown ||
            factor.source[0] == '\0' || !std::isfinite(factor.multiplier) ||
            factor.multiplier <= 1.0 + 1.0e-12) {
            continue;
        }
        const int32_t side = -2;
        const uint32_t zone_index = static_cast<uint32_t>(factor.semantic_zone);
        ZoneState* zone = find_zone(side, zone_index);
        if (!zone) {
            zones.push_back({factor.semantic_zone, side, zone_index, 1.0, 1.0,
                0.0, 1.0, true, {}});
            zone = &zones.back();
        }
        const double weight = std::log(factor.multiplier);
        zone->final_value *= factor.multiplier;
        zone->total_delta += weight;
        zone->contributors.push_back({factor.buff_inst_id, factor.source,
            factor.kind, weight});
    }
    const ArtsStrengthEvidence arts_strength = CaptureArtsStrengthEvidence();
    if (std::isfinite(arts_strength.multiplier) &&
        arts_strength.multiplier > 1.0 + 1.0e-12 &&
        arts_strength.external_points > 1.0e-12) {
        const int32_t side = -2;
        const uint32_t zone_index =
            static_cast<uint32_t>(SemanticZone::ArtsStrength);
        ZoneState* zone = find_zone(side, zone_index);
        if (!zone) {
            zones.push_back({SemanticZone::ArtsStrength, side, zone_index,
                1.0, arts_strength.multiplier, 0.0, 1.0, true, {}});
            zone = &zones.back();
        } else {
            zone->final_value *= arts_strength.multiplier;
            zone->logarithmic_contributors = true;
        }
        for (const ArtsStrengthContributor& contributor :
             arts_strength.contributors) {
            zone->total_delta += contributor.points;
            zone->contributors.push_back({contributor.inst_id,
                contributor.source, RdpsContributionKind::ArtsStrength,
                contributor.points});
        }
    }
    zones.erase(std::remove_if(zones.begin(), zones.end(), [](ZoneState& zone) {
        if (zone.logarithmic_contributors) {
            zone.initial = 1.0;
            zone.multiplier = zone.final_value;
        } else {
            zone.initial = zone.final_value - zone.total_delta;
            zone.multiplier = zone.final_value / zone.initial;
        }
        return !std::isfinite(zone.final_value) || !std::isfinite(zone.initial) ||
            zone.final_value <= 1.0e-9 || zone.initial <= 1.0e-9 ||
            !std::isfinite(zone.multiplier) || zone.multiplier <= 1.0;
    }), zones.end());
    if (zones.empty()) return;

    std::vector<RdpsMathZone> math_zones;
    math_zones.reserve(zones.size());
    for (const ZoneState& zone : zones) {
        RdpsMathZone math_zone;
        math_zone.multiplier = zone.multiplier;
        math_zone.contributor_weights.reserve(zone.contributors.size());
        for (const ZoneContributor& contributor : zone.contributors) {
            math_zone.contributor_weights.push_back(contributor.delta);
        }
        math_zones.push_back(std::move(math_zone));
    }
    std::vector<std::vector<double>> allocations;
    double external_fraction = 0.0;
    if (!AllocateExternalFractions(math_zones, allocations,
            external_fraction)) {
        return;
    }
    for (size_t zone_index = 0; zone_index < zones.size(); ++zone_index) {
        const ZoneState& zone = zones[zone_index];
        for (size_t contributor_index = 0;
             contributor_index < zone.contributors.size(); ++contributor_index) {
            if (zone_index >= allocations.size() ||
                contributor_index >= allocations[zone_index].size()) {
                continue;
            }
            const ZoneContributor& contributor =
                zone.contributors[contributor_index];
            const double fraction = allocations[zone_index][contributor_index];
            if (contributor.source.empty() || !std::isfinite(fraction) ||
                fraction <= 1.0e-12) {
                continue;
            }
            MarkBuffIntervalRelevant(contributor.inst_id, contributor.kind,
                fraction, BuffEffectKind::DamageShare);
            MergeRdpsShare(output, output_count, contributor.source, fraction,
                contributor.kind);
        }
    }
}

void RememberCompletedDamageFlow(const DamagePackNumericSnapshot& final_snapshot) {
    if (!g_damage_flow_context.server_detail) return;
    CompletedDamageFlow completed{};
    completed.generation = g_damage_flow_context.generation;
    completed.transaction = g_damage_flow_context.transaction;
    completed.server_detail = g_damage_flow_context.server_detail;
    completed.calculated_damage = g_damage_flow_context.calculated_damage;
    BuildRdpsShares(final_snapshot, completed.rdps_shares,
        completed.rdps_share_count);
    g_completed_damage_flows.push_back(completed);
    while (g_completed_damage_flows.size() > kCompletedDamageFlowCapacity) {
        g_completed_damage_flows.pop_front();
    }
}

bool TakeCompletedDamageFlow(void* server_detail, CompletedDamageFlow& output) {
    if (!server_detail) return false;
    const uint64_t generation =
        g_rdps_flow_generation.load(std::memory_order_acquire);
    for (size_t offset = g_completed_damage_flows.size(); offset > 0; --offset) {
        const size_t index = offset - 1;
        const CompletedDamageFlow& candidate = g_completed_damage_flows[index];
        if (candidate.generation != generation ||
            candidate.server_detail != server_detail) {
            continue;
        }
        output = candidate;
        g_completed_damage_flows.erase(
            g_completed_damage_flows.begin() + static_cast<ptrdiff_t>(index));
        return true;
    }
    return false;
}

bool CaptureDoubleArray(void* array, DoubleArraySnapshot& output) {
    output = {};
    if (!array) {
        output.valid = true;
        return true;
    }
    __try {
        const uintptr_t length = *reinterpret_cast<uintptr_t*>(
            reinterpret_cast<uint8_t*>(array) + 0x18);
        if (length > 4096) return false;
        output.valid = true;
        output.count = static_cast<uint32_t>(length);
        output.managed_array = array;
        const size_t copied = std::min<size_t>(length, output.values.size());
        std::memcpy(output.values.data(), reinterpret_cast<uint8_t*>(array) + 0x20,
            copied * sizeof(double));
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

bool CapturePackNumericSnapshot(void* pack_data, DamagePackNumericSnapshot& output) {
    output = {};
    if (!pack_data) return false;
    void* calc_result = EmbeddedValueType(pack_data, g_pack_calc_result);
    if (!ReadValueTypeField(calc_result, g_calc_result_value, output.calc_result) ||
        !ReadValueTypeField(pack_data, g_pack_atk_scale, output.atk_scale)) {
        return false;
    }
    void* attacker_zones = nullptr;
    void* attacker_attributes = nullptr;
    void* defender_zones = nullptr;
    void* defender_attributes = nullptr;
    if (!ReadValueTypeField(pack_data, g_pack_attacker_damage_scale_zones, attacker_zones) ||
        !ReadValueTypeField(pack_data, g_pack_attacker_attributes, attacker_attributes) ||
        !ReadValueTypeField(pack_data, g_pack_defender_damage_scale_zones, defender_zones) ||
        !ReadValueTypeField(pack_data, g_pack_defender_attributes, defender_attributes)) {
        return false;
    }
    output.valid = CaptureDoubleArray(attacker_zones, output.attacker_zones) &&
        CaptureDoubleArray(attacker_attributes, output.attacker_attributes) &&
        CaptureDoubleArray(defender_zones, output.defender_zones) &&
        CaptureDoubleArray(defender_attributes, output.defender_attributes);
    return output.valid;
}

size_t AppendArrayChanges(std::ostringstream& output, const char* label,
    const DoubleArraySnapshot& before, const DoubleArraySnapshot& after,
    size_t already_written) {
    if (!before.valid || !after.valid) return 0;
    size_t changed = 0;
    const size_t limit = std::min<size_t>(
        std::min(before.count, after.count), kRdpsArraySampleCapacity);
    for (size_t index = 0; index < limit; ++index) {
        if (std::abs(before.values[index] - after.values[index]) <= 1.0e-9) continue;
        ++changed;
        if (already_written + changed <= 16) {
            output << " " << label << "[" << index << "]="
                << before.values[index] << "->" << after.values[index];
        }
    }
    if (before.count != after.count) {
        ++changed;
        if (already_written + changed <= 16) {
            output << " " << label << "Count=" << before.count << "->" << after.count;
        }
    }
    return changed;
}

size_t AppendNumericChanges(std::ostringstream& output,
    const DamagePackNumericSnapshot& before, const DamagePackNumericSnapshot& after) {
    size_t changed = 0;
    if (std::abs(before.calc_result - after.calc_result) > 1.0e-9) {
        output << " calc=" << before.calc_result << "->" << after.calc_result;
        ++changed;
    }
    if (std::abs(static_cast<double>(before.atk_scale - after.atk_scale)) > 1.0e-7) {
        output << " atkScale=" << before.atk_scale << "->" << after.atk_scale;
        ++changed;
    }
    changed += AppendArrayChanges(output, "atkZone", before.attacker_zones,
        after.attacker_zones, changed);
    changed += AppendArrayChanges(output, "atkAttr", before.attacker_attributes,
        after.attacker_attributes, changed);
    changed += AppendArrayChanges(output, "defZone", before.defender_zones,
        after.defender_zones, changed);
    changed += AppendArrayChanges(output, "defAttr", before.defender_attributes,
        after.defender_attributes, changed);
    return changed;
}

void* ReadHandleReference(void** handle_reference) {
    if (!handle_reference) return nullptr;
    __try {
        return *handle_reference;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return nullptr;
    }
}

bool ReadBoolPointer(const bool* pointer) {
    if (!pointer) return false;
    __try {
        return *pointer;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

bool ReadBlackboardDoubleValue(void* parameter, void* blackboard, double& value) {
    if (!parameter || !g_blackboard_double_get_value.pointer) return false;
    __try {
        value = reinterpret_cast<BlackboardDoubleGetValueFn>(
            g_blackboard_double_get_value.pointer)(parameter, blackboard,
            const_cast<void*>(g_blackboard_double_get_value.method_info));
        return std::isfinite(value);
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
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

bool IsEnemyAttacker(std::string_view value) {
    return value.size() >= 3 &&
        std::tolower(static_cast<unsigned char>(value[0])) == 'e' &&
        std::tolower(static_cast<unsigned char>(value[1])) == 'n' &&
        std::tolower(static_cast<unsigned char>(value[2])) == 'y';
}

const char* RdpsContributionId(RdpsContributionKind kind) {
    static constexpr std::array<const char*, 9> kIds{
        "direct", "attack", "damageIncrease", "amplification", "fragile",
        "vulnerabilityTaken", "resistance", "artsStrength", "other"};
    const size_t index = RdpsContributionIndex(kind);
    return index < kIds.size() ? kIds[index] : kIds.back();
}

RdpsContributionKind ClassifyBuffInterval(const BuffDiagnosticInfo& info) {
    if (auto cat = SemanticCatalogSnapshot()) {
        if (const auto* buff_sem = cat->FindBuff(info.buff_id)) {
            for (const auto& eff : buff_sem->effects) {
                if (eff.zone != SemanticZone::Unknown) {
                    return ContributionKindForZone(eff.zone);
                }
            }
        }
    }
    const std::string owner = NormalizeAttackerId(info.owner);
    const int32_t side = StartsWithCharacterPrefix(owner) ? 0 : 1;
    std::array<bool, 9> present{};
    for (const auto& contribution : info.attribute_contributions) {
        present[RdpsContributionIndex(ClassifyAttributeContribution(
            side, contribution.attribute_type))] = true;
    }
    static constexpr std::array<RdpsContributionKind, 7> kPriority{
        RdpsContributionKind::Resistance,
        RdpsContributionKind::Fragile,
        RdpsContributionKind::VulnerabilityTaken,
        RdpsContributionKind::Amplification,
        RdpsContributionKind::Attack,
        RdpsContributionKind::DamageIncrease,
        RdpsContributionKind::ArtsStrength};
    for (const RdpsContributionKind kind : kPriority) {
        if (present[RdpsContributionIndex(kind)]) return kind;
    }
    return RdpsContributionKind::Other;
}

uint64_t FindUniqueOpenActionLocked(
    std::string_view actor, std::string_view skill) {
    if (actor.empty() || skill.empty()) return 0;
    uint64_t result = 0;
    for (const auto& [runtime_skill, index] : g_session.open_actions) {
        (void)runtime_skill;
        if (index >= g_session.actions.size()) continue;
        const CombatAction& action = g_session.actions[index];
        if (!action.active || action.actor != actor || action.skill != skill) continue;
        if (result != 0) return 0;
        result = action.id;
    }
    return result;
}

double SessionElapsedLocked(std::chrono::steady_clock::time_point now) {
    if (g_session.id == 0) return 0.0;
    return std::max(0.0,
        std::chrono::duration<double>(now - g_session.started).count());
}

void TrackBuffStart(uint64_t inst_id, const BuffDiagnosticInfo& info,
    double forced_start = -1.0) {
    if (!inst_id || info.buff_id.empty() ||
        !g_session_active.load(std::memory_order_acquire)) {
        return;
    }
    const RdpsContributionKind kind = ClassifyBuffInterval(info);
    std::scoped_lock lock(g_session_mutex);
    if (!g_session.active || g_session.open_buff_intervals.contains(inst_id) ||
        g_session.buff_intervals.size() >= kMaxBuffIntervals) return;
    BuffInterval interval;
    interval.inst_id = inst_id;
    interval.start_time = forced_start >= 0.0 ? forced_start :
        SessionElapsedLocked(std::chrono::steady_clock::now());
    interval.kind = kind;
    interval.relevant = false;
    interval.buff_id = info.buff_id;
    interval.source = info.source;
    interval.owner = info.owner;
    interval.origin_skill = info.origin_skill;
    interval.credited_actor_id = info.credited_actor_id;
    interval.source_kind = info.source_kind;
    interval.source_template_id = info.source_template_id;
    interval.source_skill_id = info.source_skill_id;
    interval.actor_attribution = info.actor_attribution;
    interval.source_attribution = info.source_attribution;
    interval.attribution_reason = info.attribution_reason;
    interval.action_id = FindUniqueOpenActionLocked(
        interval.credited_actor_id.empty()
            ? NormalizeAttackerId(interval.source)
            : interval.credited_actor_id,
        interval.origin_skill);
    g_session.open_buff_intervals[inst_id] = g_session.buff_intervals.size();
    g_session.buff_intervals.push_back(std::move(interval));
}

void MarkBuffIntervalRelevant(uint64_t inst_id, RdpsContributionKind kind,
    double effect_value, BuffEffectKind effect_kind) {
    if (!inst_id || !g_session_active.load(std::memory_order_acquire)) return;
    std::scoped_lock lock(g_session_mutex);
    const auto found = g_session.open_buff_intervals.find(inst_id);
    if (found == g_session.open_buff_intervals.end() ||
        found->second >= g_session.buff_intervals.size()) return;
    BuffInterval& interval = g_session.buff_intervals[found->second];
    interval.relevant = true;
    // Runtime evidence is authoritative over the provisional classification
    // derived when the Buff interval first opened.
    interval.kind = kind;
    if (effect_kind != BuffEffectKind::None && std::isfinite(effect_value)) {
        if (!interval.effect_observed || interval.effect_kind != effect_kind) {
            interval.effect_kind = effect_kind;
            interval.effect_observed = true;
            interval.effect_min = effect_value;
            interval.effect_max = effect_value;
        } else {
            interval.effect_min = std::min(interval.effect_min, effect_value);
            interval.effect_max = std::max(interval.effect_max, effect_value);
        }
    }
}

void TrackBuffFinish(uint64_t inst_id) {
    if (!inst_id) return;
    std::scoped_lock lock(g_session_mutex);
    const auto found = g_session.open_buff_intervals.find(inst_id);
    if (found == g_session.open_buff_intervals.end()) return;
    if (found->second < g_session.buff_intervals.size()) {
        BuffInterval& interval = g_session.buff_intervals[found->second];
        interval.end_time = SessionElapsedLocked(std::chrono::steady_clock::now());
        interval.active = false;
    }
    g_session.open_buff_intervals.erase(found);
}

bool IsPoiseBreakBuff(std::string_view buff_id) {
    if (buff_id.empty()) return false;
    return buff_id == "buff_common_poise_break_damage_taken_scale" ||
           buff_id == "buff_common_poise_can_be_breaking_attacked" ||
           buff_id == "buff_common_mini_poise_break" ||
           buff_id == "buff_common_temp_poise_break" ||
           (buff_id.starts_with("buff_eny_") && (buff_id.ends_with("_poise_break") || buff_id.ends_with("_break")));
}

void TrackStaggerStart(uint64_t inst_id, const BuffDiagnosticInfo& info,
    double forced_start = -1.0) {
    if (!inst_id || !g_session_active.load(std::memory_order_acquire)) return;
    std::scoped_lock lock(g_session_mutex);
    if (!g_session.active || g_session.open_stagger_intervals.contains(inst_id) ||
        g_session.stagger_intervals.size() >= kMaxBuffIntervals) return;
    Session::StaggerInterval interval;
    interval.inst_id = inst_id;
    interval.start_time = forced_start >= 0.0 ? forced_start :
        SessionElapsedLocked(std::chrono::steady_clock::now());
    interval.target_id = NormalizeAttackerId(info.owner);
    interval.buff_id = info.buff_id;
    g_session.open_stagger_intervals[inst_id] = g_session.stagger_intervals.size();
    g_session.stagger_intervals.push_back(std::move(interval));
}

void TrackStaggerFinish(uint64_t inst_id) {
    if (!inst_id) return;
    std::scoped_lock lock(g_session_mutex);
    const auto found = g_session.open_stagger_intervals.find(inst_id);
    if (found == g_session.open_stagger_intervals.end()) return;
    if (found->second < g_session.stagger_intervals.size()) {
        Session::StaggerInterval& interval = g_session.stagger_intervals[found->second];
        interval.end_time = SessionElapsedLocked(std::chrono::steady_clock::now());
        interval.active = false;
    }
    g_session.open_stagger_intervals.erase(found);
}

size_t SkillCategoryFromId(std::string_view skill) {
    // Native records do not contain an authoritative skill-group category.
    // Keep unknown skills in "other" instead of classifying by an ID substring.
    (void)skill;
    return 5;
}

const char* ActionTypeId(std::string_view skill) {
    switch (SkillCategoryFromId(skill)) {
    case 0: return "basicAttack";
    case 1: return "skill";
    case 2: return "ultimate";
    case 3: return "combo";
    case 4: return "passive";
    default: return "system";
    }
}

int32_t ActionStage(std::string_view skill) {
    const size_t marker = skill.rfind("_attack");
    if (marker == std::string_view::npos) return 0;
    size_t cursor = marker + std::string_view("_attack").size();
    int32_t stage = 0;
    while (cursor < skill.size() && std::isdigit(
        static_cast<unsigned char>(skill[cursor]))) {
        stage = stage * 10 + (skill[cursor] - '0');
        ++cursor;
    }
    return stage;
}

void TrackActionStart(void* caster, void* skill) {
    if (!skill || !g_session_active.load(std::memory_order_acquire)) return;
    std::string actor = NormalizeAttackerId(ResolveAbilityName(caster));
    void* managed_skill_id = nullptr;
    std::string skill_id;
    if (ReadValue(skill, g_skill_id, managed_skill_id)) {
        skill_id = CopyString(managed_skill_id);
    }
    // BattleRecorder also reports casts performed by projectiles,
    // AbilityEntity instances and enemies. Those are results or hostile
    // activity, not squad operations.
    if (!StartsWithCharacterPrefix(actor) || skill_id.empty()) return;
    std::scoped_lock lock(g_session_mutex);
    if (!g_session.active || g_session.actions.size() >= kMaxRawEvents) return;
    ++g_session.action_start_calls;
    const double now = SessionElapsedLocked(std::chrono::steady_clock::now());
    if (const auto found = g_session.open_actions.find(skill);
        found != g_session.open_actions.end() &&
        found->second < g_session.actions.size()) {
        CombatAction& previous = g_session.actions[found->second];
        previous.end_time = now;
        previous.active = false;
        previous.inferred_end = true;
    }
    CombatAction action;
    action.id = g_session.next_action_id++;
    action.start_time = now;
    action.end_time = now;
    action.runtime_skill = skill;
    action.actor = std::move(actor);
    action.skill = std::move(skill_id);
    action.type = "skillCast";
    g_session.open_actions[skill] = g_session.actions.size();
    g_session.actions.push_back(std::move(action));
}

void TrackActionEnd(void* skill, int32_t finish_type,
    int32_t interrupt_reason, bool cancelled) {
    if (!skill) return;
    std::scoped_lock lock(g_session_mutex);
    if (!g_session.active) return;
    ++g_session.action_end_calls;
    const auto found = g_session.open_actions.find(skill);
    if (found == g_session.open_actions.end() ||
        found->second >= g_session.actions.size()) return;
    ++g_session.action_end_matches;
    CombatAction& action = g_session.actions[found->second];
    action.end_time = SessionElapsedLocked(std::chrono::steady_clock::now());
    action.finish_type = finish_type;
    action.interrupt_reason = interrupt_reason;
    action.cancelled = cancelled;
    action.active = false;
    action.end_observed = true;
    g_session.open_actions.erase(found);
}

bool TryReadSkillCasting(void* skill, bool& casting) {
    if (!skill || !g_skill_is_casting.pointer) return false;
    __try {
        casting = reinterpret_cast<BoolGetterFn>(g_skill_is_casting.pointer)(
            skill, const_cast<void*>(g_skill_is_casting.method_info));
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

void PollActionEnds() {
    if (!g_session_active.load(std::memory_order_acquire) ||
        !g_skill_is_casting.pointer) return;
    std::vector<std::pair<void*, size_t>> open_actions;
    {
        std::scoped_lock lock(g_session_mutex);
        if (!g_session.active) return;
        open_actions.reserve(g_session.open_actions.size());
        for (const auto& entry : g_session.open_actions) {
            open_actions.push_back(entry);
        }
    }
    for (const auto& [skill, action_index] : open_actions) {
        bool casting = true;
        if (!TryReadSkillCasting(skill, casting) || casting) continue;
        std::scoped_lock lock(g_session_mutex);
        const auto found = g_session.open_actions.find(skill);
        if (!g_session.active || found == g_session.open_actions.end() ||
            found->second != action_index || action_index >= g_session.actions.size()) {
            continue;
        }
        CombatAction& action = g_session.actions[action_index];
        action.end_time = SessionElapsedLocked(std::chrono::steady_clock::now());
        action.active = false;
        action.end_observed = true;
        ++g_session.action_end_calls;
        ++g_session.action_end_matches;
        g_session.open_actions.erase(found);
    }
}

void TrackDodgeAction(void* source) {
    if (!g_session_active.load(std::memory_order_acquire)) return;
    std::string actor = NormalizeAttackerId(ResolveAbilityName(source));
    if (actor.empty() || !StartsWithCharacterPrefix(actor)) return;
    std::scoped_lock lock(g_session_mutex);
    if (!g_session.active || g_session.actions.size() >= kMaxRawEvents) return;
    ++g_session.dodge_calls;
    const double now = SessionElapsedLocked(std::chrono::steady_clock::now());
    CombatAction action;
    action.id = g_session.next_action_id++;
    action.start_time = now;
    action.end_time = now;
    action.actor = std::move(actor);
    action.type = "dodge";
    action.active = false;
    action.end_observed = true;
    g_session.actions.push_back(std::move(action));
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
        output << "],\"rdpsByContribution\":[";
        for (size_t index = 0; index < value.rdps_by_contribution.size(); ++index) {
            if (index) output << ",";
            output << value.rdps_by_contribution[index];
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

const Session::SquadMember* FindSquadMember(
    const Session& session, std::string_view actor_id) {
    for (const auto& member : session.squad) {
        if (_stricmp(member.char_id.c_str(), std::string(actor_id).c_str()) == 0) {
            return &member;
        }
    }
    return nullptr;
}

bool IsBuffSourceConfigurationVerified(
    const Session& session, const BuffInterval& interval) {
    if (interval.source_attribution != "dictionaryExact" ||
        interval.source_kind.empty() || interval.source_template_id.empty()) {
        return false;
    }
    if (interval.source_kind == "system") return true;
    if (interval.actor_attribution != "runtimeExact" ||
        interval.credited_actor_id.empty()) {
        return false;
    }
    const Session::SquadMember* member = FindSquadMember(
        session, interval.credited_actor_id);
    if (!member) return false;
    if (interval.source_kind == "character" ||
        interval.source_kind == "characterSkill" ||
        interval.source_kind == "characterPassive") {
        return _stricmp(member->char_id.c_str(),
            interval.source_template_id.c_str()) == 0;
    }
    if (interval.source_kind == "weapon") {
        return member->weapon && _stricmp(member->weapon->template_id.c_str(),
            interval.source_template_id.c_str()) == 0;
    }
    if (interval.source_kind == "equipSuit") {
        return std::any_of(member->equip_suits.begin(), member->equip_suits.end(),
            [&interval](const Session::SquadMember::EquipSuit& suit) {
                return suit.count > 0 && _stricmp(suit.suit_id.c_str(),
                    interval.source_template_id.c_str()) == 0;
            });
    }
    return false;
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
    output << "{\n  \"schemaVersion\":11,\n";
    output << "  \"battle\":{\"sessionId\":" << session.id
        << ",\"startedUnixSeconds\":" << stamp
        << ",\"durationSeconds\":" << std::setprecision(15) << duration
        << ",\"dungeonId\":\"" << JsonEscape(session.dungeon_id)
        << "\",\"modeId\":\"" << JsonEscape(session.mode_id) << "\"},\n";
    output << "  \"dictionary\":{\"schemaVersion\":"
        << session.semantic_catalog_schema
        << ",\"gameVersion\":\"" << JsonEscape(session.semantic_game_version)
        << "\",\"hotfixVersion\":\""
        << JsonEscape(session.semantic_hotfix_version)
        << "\",\"sourceSha256\":\""
        << JsonEscape(session.semantic_source_sha256) << "\"},\n";
    if (!session.squad.empty()) {
        output << "  \"squad\":[";
        for (size_t index = 0; index < session.squad.size(); ++index) {
            if (index) output << ",";
            const auto& member = session.squad[index];
            output << "{\"charId\":\"" << JsonEscape(member.char_id)
                << "\",\"charInstId\":\"" << member.char_inst_id << "\""
                << ",\"level\":" << member.level
                << ",\"breakStage\":" << member.break_stage
                << ",\"potential\":" << member.potential
                << ",\"skillGroups\":[";
            for (size_t skill_index = 0; skill_index < member.skill_groups.size();
                ++skill_index) {
                if (skill_index) output << ",";
                const auto& skill = member.skill_groups[skill_index];
                output << "{\"skillGroupId\":\"" << JsonEscape(skill.id)
                    << "\",\"level\":" << skill.level
                    << ",\"maxLevel\":" << skill.max_level << "}";
            }
            output << "],\"weapon\":";
            if (member.weapon) {
                const auto& weapon = *member.weapon;
                output << "{\"templateId\":\"" << JsonEscape(weapon.template_id)
                    << "\",\"level\":" << weapon.level
                    << ",\"refineLevel\":" << weapon.refine_level
                    << ",\"breakthroughLevel\":" << weapon.breakthrough_level
                    << ",\"skills\":[";
                for (size_t skill_index = 0; skill_index < weapon.skills.size();
                    ++skill_index) {
                    if (skill_index) output << ",";
                    const auto& skill = weapon.skills[skill_index];
                    output << "{\"skillId\":\"" << JsonEscape(skill.id)
                        << "\",\"level\":" << skill.level
                        << ",\"maxLevel\":" << skill.max_level << "}";
                }
                output << "]}";
            } else {
                output << "null";
            }
            output << ",\"equipments\":[";
            for (size_t equip_index = 0; equip_index < member.equipments.size();
                ++equip_index) {
                if (equip_index) output << ",";
                const auto& equip = member.equipments[equip_index];
                output << "{\"slot\":" << equip.slot
                    << ",\"templateId\":\"" << JsonEscape(equip.template_id)
                    << "\",\"enhanceLevels\":{";
                bool first_level = true;
                for (const auto& [attribute, enhance_level] : equip.enhance_levels) {
                    if (!first_level) output << ",";
                    first_level = false;
                    output << "\"" << attribute << "\":" << enhance_level;
                }
                output << "}}";
            }
            output << "],\"equipSuits\":[";
            for (size_t suit_index = 0; suit_index < member.equip_suits.size();
                ++suit_index) {
                if (suit_index) output << ",";
                const auto& suit = member.equip_suits[suit_index];
                output << "{\"suitId\":\"" << JsonEscape(suit.suit_id)
                    << "\",\"count\":" << suit.count << ",\"skills\":[";
                for (size_t skill_index = 0; skill_index < suit.skills.size();
                    ++skill_index) {
                    if (skill_index) output << ",";
                    const auto& skill = suit.skills[skill_index];
                    output << "{\"skillId\":\"" << JsonEscape(skill.id)
                        << "\",\"level\":" << skill.level
                        << ",\"maxLevel\":" << skill.max_level << "}";
                }
                output << "]}";
            }
            output << "]}";
        }
        output << "],\n";
    } else {
        output << "  \"squad\":[],\n";
    }
    std::vector<CombatAction> saved_actions = session.actions;
    std::sort(saved_actions.begin(), saved_actions.end(),
        [](const CombatAction& left, const CombatAction& right) {
            if (left.start_time != right.start_time) return left.start_time < right.start_time;
            return left.id < right.id;
        });
    output << "  \"actions\":[";
    for (size_t index = 0; index < saved_actions.size(); ++index) {
        if (index) output << ",";
        const CombatAction& action = saved_actions[index];
        const char* result = action.cancelled ? "cancelled" :
            action.interrupt_reason != 0 ? "interrupted" :
            action.inferred_end ? "superseded" :
            action.end_observed ? "completed" : "openAtSessionEnd";
        output << "{\"id\":" << action.id
            << ",\"start\":" << std::setprecision(15) << action.start_time
            << ",\"end\":";
        if (action.end_observed) {
            output << std::max(action.start_time, action.end_time);
        } else {
            output << "null,\"observedUntil\":"
                << std::max(action.start_time, action.end_time);
        }
        output << ",\"actorId\":\"" << JsonEscape(action.actor)
            << "\",\"type\":\""
            << JsonEscape(action.type.empty() ? "skillCast" : action.type)
            << "\",\"skillId\":\"" << JsonEscape(action.skill)
            << "\",\"result\":\"" << result << "\"";
        if (action.parent_action_id) {
            output << ",\"parentActionId\":" << action.parent_action_id;
        }
        output << "}";
    }
    output << "],\n  \"effects\":[";
    uint64_t effect_id = 1;
    bool first_effect = true;
    for (const DamageEvent& event : session.raw_events) {
        if (!first_effect) output << ",";
        first_effect = false;
        const std::string source_entity = event.attacker;
        const std::string normalized_actor = NormalizeAttackerId(source_entity);
        const bool actor_exact = FindSquadMember(session, normalized_actor) != nullptr;
        const std::string credited_actor = actor_exact ? normalized_actor : "";
        const double raw_damage = std::abs(event.damage);
        const double health_change = std::abs(event.hp_delta);
        const double actual_damage = health_change > 0.0
            ? std::min(raw_damage, health_change) : raw_damage;
        output << "{\"id\":" << effect_id++
            << ",\"time\":" << std::setprecision(15) << event.elapsed_seconds
            << ",\"type\":\"damage\",\"sourceId\":\""
            << JsonEscape(credited_actor)
            << "\",\"sourceEntityId\":\"" << JsonEscape(source_entity)
            << "\",\"creditedActorId\":\"" << JsonEscape(credited_actor)
            << "\",\"actorAttribution\":\""
            << (actor_exact ? "runtimeExact" : "unknown")
            << "\",\"targetId\":\"enemy\",\"skillId\":\""
            << JsonEscape(event.skill) << "\",\"value\":"
            << actual_damage;
        if (event.action_id) output << ",\"actionId\":" << event.action_id;
        if (std::abs(raw_damage - actual_damage) > 0.000001) {
            output << ",\"rawValue\":" << raw_damage;
        }
        output << ",\"hpDelta\":" << event.hp_delta
            << ",\"damageType\":" << event.damage_type
            << ",\"decorateMask\":" << event.decorate_mask
            << ",\"critical\":" << (event.critical ? "true" : "false")
            << ",\"rdpsShares\":[";
        for (uint8_t share_index = 0;
             share_index < event.rdps_share_count; ++share_index) {
            if (share_index) output << ",";
            const RdpsShare& share = event.rdps_shares[share_index];
            output << "{\"sourceId\":\"" << JsonEscape(
                NormalizeAttackerId(share.source))
                << "\",\"fraction\":" << share.fraction
                << ",\"contributionType\":"
                << RdpsContributionIndex(share.kind)
                << ",\"contributionKind\":\""
                << RdpsContributionId(share.kind) << "\"}";
        }
        output << "]}";
    }
    for (const BuffInterval& interval : session.buff_intervals) {
        if (interval.buff_id.empty() || interval.end_time <= interval.start_time) continue;
        const std::string target = NormalizeAttackerId(interval.owner);
        const char* status_kind = IsPoiseBreakBuff(interval.buff_id)
            ? "stagger" : StartsWithCharacterPrefix(target) ? "buff" : "debuff";
        const bool source_verified = IsBuffSourceConfigurationVerified(
            session, interval);
        std::string attribution_reason = interval.attribution_reason;
        if (interval.source_attribution == "dictionaryExact" && !source_verified) {
            AppendAttributionReason(attribution_reason,
                "source_not_present_in_credited_actor_configuration");
        }
        if (!first_effect) output << ",";
        first_effect = false;
        output << "{\"id\":" << effect_id++
            << ",\"time\":" << std::setprecision(15) << interval.start_time
            << ",\"type\":\"statusApply\",\"sourceId\":\""
            << JsonEscape(interval.credited_actor_id)
            << "\",\"sourceEntityId\":\"" << JsonEscape(interval.source)
            << "\",\"creditedActorId\":\""
            << JsonEscape(interval.credited_actor_id)
            << "\",\"actorAttribution\":\""
            << JsonEscape(interval.actor_attribution)
            << "\",\"sourceAttribution\":\""
            << (source_verified ? "configurationVerified" : "unknown")
            << "\",\"sourceKind\":\""
            << JsonEscape(source_verified ? interval.source_kind : "unknown")
            << "\",\"sourceTemplateId\":\""
            << JsonEscape(source_verified ? interval.source_template_id : "")
            << "\",\"sourceSkillId\":\""
            << JsonEscape(source_verified ? interval.source_skill_id : "")
            << "\",\"attributionReason\":\""
            << JsonEscape(attribution_reason) << "\",\"targetId\":\""
            << JsonEscape(target) << "\",\"targetEntityId\":\""
            << JsonEscape(interval.owner) << "\",\"skillId\":\""
            << JsonEscape(interval.origin_skill) << "\",\"statusId\":\""
            << JsonEscape(interval.buff_id) << "\",\"statusInstanceId\":\""
            << interval.inst_id << "\",\"statusKind\":\"" << status_kind
            << "\",\"contributionType\":" << RdpsContributionIndex(interval.kind)
            << ",\"contributionKind\":\"" << RdpsContributionId(interval.kind)
            << "\",\"effectKind\":" << static_cast<uint32_t>(interval.effect_kind)
            << ",\"effectMin\":" << interval.effect_min
            << ",\"effectMax\":" << interval.effect_max;
        if (interval.action_id) output << ",\"actionId\":" << interval.action_id;
        output << "}";
        output << ",{\"id\":" << effect_id++
            << ",\"time\":" << interval.end_time
            << ",\"type\":\"statusRemove\",\"sourceId\":\""
            << JsonEscape(interval.credited_actor_id)
            << "\",\"sourceEntityId\":\"" << JsonEscape(interval.source)
            << "\",\"creditedActorId\":\""
            << JsonEscape(interval.credited_actor_id)
            << "\",\"targetId\":\""
            << JsonEscape(target) << "\",\"targetEntityId\":\""
            << JsonEscape(interval.owner) << "\",\"skillId\":\""
            << JsonEscape(interval.origin_skill) << "\",\"statusId\":\""
            << JsonEscape(interval.buff_id) << "\",\"statusInstanceId\":\""
            << interval.inst_id << "\",\"statusKind\":\"" << status_kind << "\"";
        if (interval.action_id) output << ",\"actionId\":" << interval.action_id;
        output << "}";
    }
    double total_rdps_damage = 0.0;
    for (const auto& [character, aggregate] : session.rdps_characters) {
        (void)character;
        total_rdps_damage += aggregate.damage;
    }
    output << "],\n  \"summary\":{\"totalDamage\":" << session.total_damage
        << ",\"dps\":" << session.total_damage / duration
        << ",\"rdps\":" << total_rdps_damage / duration
        << ",\"hitCount\":" << session.hits
        << ",\"criticalCount\":" << session.critical_hits << "}\n}\n";
    Log(std::string("[combat-save] wrote schema 11 ") + path.string());
    return;
    if (!session.stagger_intervals.empty()) {
        output << "  \"staggerIntervals\":[";
        bool first_stagger = true;
        for (const auto& stagger : session.stagger_intervals) {
            if (stagger.end_time <= stagger.start_time) continue;
            if (!first_stagger) output << ",";
            first_stagger = false;
            output << "{\"targetId\":\"" << JsonEscape(stagger.target_id)
                << "\",\"start\":" << std::setprecision(15) << stagger.start_time
                << ",\"end\":" << stagger.end_time
                << ",\"buffId\":\"" << JsonEscape(stagger.buff_id) << "\"}";
        }
        output << "],\n";
    } else {
        output << "  \"staggerIntervals\":[],\n";
    }
    output << "  \"routeHits\":{\"recordDamage\":" << session.record_damage_calls;
    output << ",\"recordDamageDetail\":" << session.record_detail_calls;
    output << ",\"damageDetailInit\":" << session.detail_init_calls;
    output << ",\"damageText\":" << session.damage_text_calls << "},\n";
    output << "  \"semanticCatalog\":{\"schemaVersion\":"
        << session.semantic_catalog_schema
        << ",\"gameVersion\":\"" << JsonEscape(session.semantic_game_version)
        << "\",\"hotfixVersion\":\""
        << JsonEscape(session.semantic_hotfix_version)
        << "\",\"sourceSha256\":\""
        << JsonEscape(session.semantic_source_sha256) << "\"},\n";
    const double semantic_coverage = session.semantic_coverage.observed
        ? static_cast<double>(session.semantic_coverage.verified) /
            static_cast<double>(session.semantic_coverage.observed)
        : 0.0;
    output << "  \"semanticCoverage\":{\"observed\":"
        << session.semantic_coverage.observed << ",\"verified\":"
        << session.semantic_coverage.verified << ",\"candidate\":"
        << session.semantic_coverage.candidate << ",\"excluded\":"
        << session.semantic_coverage.excluded << ",\"unknown\":"
        << session.semantic_coverage.unknown << ",\"elementMismatch\":"
        << session.semantic_coverage.element_mismatch << ",\"ratio\":"
        << semantic_coverage << ",\"unresolvedDropped\":"
        << session.semantic_coverage.unresolved_dropped << "},\n";
    output << "  \"unresolvedSemantics\":[";
    for (size_t index = 0; index < session.unresolved_semantics.size(); ++index) {
        if (index) output << ",";
        const Session::SemanticAuditItem& item =
            session.unresolved_semantics[index];
        output << "{\"buffId\":\"" << JsonEscape(item.buff_id)
            << "\",\"status\":\"" << SemanticStatusId(item.status)
            << "\",\"observedZone\":\""
            << CombatSemanticCatalog::ZoneId(item.observed_zone)
            << "\",\"count\":" << item.count
            << ",\"elementMismatch\":"
            << (item.element_mismatch ? "true" : "false") << "}";
    }
    output << "],\n";
    output << "  \"buffRegistry\":{";
    if (auto cat = SemanticCatalogSnapshot()) {
        bool first_reg = true;
        for (const auto& [buff_id, buff_sem] : cat->Buffs()) {
            if (!first_reg) output << ",";
            first_reg = false;
            output << "\"" << JsonEscape(buff_id) << "\":{";
            if (!buff_sem.name.empty()) {
                output << "\"name\":\"" << JsonEscape(buff_sem.name) << "\",";
            }
            if (!buff_sem.effects.empty()) {
                const auto& eff = buff_sem.effects[0];
                output << "\"sourceKind\":\"" << JsonEscape(eff.source_kind) << "\",";
                output << "\"sourceId\":\"" << JsonEscape(eff.source_id) << "\",";
                output << "\"skillId\":\"" << JsonEscape(eff.skill_id) << "\",";
                output << "\"zone\":\"" << CombatSemanticCatalog::ZoneId(eff.zone) << "\",";
                output << "\"element\":\"" << CombatSemanticCatalog::ElementId(eff.element) << "\",";
            }
            output << "\"status\":\"" << CombatSemanticCatalog::StatusId(buff_sem.status) << "\",";
            output << "\"isDynamic\":" << (buff_sem.is_dynamic ? "true" : "false") << "}";
        }
    }
    output << "},\n";
    output << "  \"characters\":"; WriteCharacterMap(output, session.characters); output << ",\n";
    output << "  \"rdpsCharacters\":";
    WriteCharacterMap(output, session.rdps_characters);
    output << ",\n";
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
        output << "],\"rdpsByContribution\":[";
        for (size_t index = 0; index < bucket.rdps_by_contribution.size(); ++index) {
            if (index) output << ",";
            output << bucket.rdps_by_contribution[index];
        }
        output << "],\"damageByCharacter\":{";
        bool first_character = true;
        for (const auto& [character, damage] : bucket.damage_by_character) {
            if (!first_character) output << ",";
            first_character = false;
            output << "\"" << JsonEscape(character) << "\":" << damage;
        }
        output << "},\"rdpsByCharacter\":{";
        first_character = true;
        for (const auto& [character, damage] : bucket.rdps_by_character) {
            if (!first_character) output << ",";
            first_character = false;
            output << "\"" << JsonEscape(character) << "\":" << damage;
        }
        output << "}}";
    }
    output << "],\n  \"buffIntervals\":[";
    bool first_interval = true;
    for (const BuffInterval& interval : session.buff_intervals) {
        if (!interval.relevant || interval.source.empty() ||
            interval.end_time <= interval.start_time) continue;
        if (!first_interval) output << ",";
        first_interval = false;
        output << "{\"start\":" << std::setprecision(15) << interval.start_time
            << ",\"end\":" << interval.end_time
            << ",\"buffId\":\"" << JsonEscape(interval.buff_id)
            << "\",\"sourceId\":\"" << JsonEscape(interval.source)
            << "\",\"targetId\":\"" << JsonEscape(interval.owner)
            << "\",\"originSkillId\":\"" << JsonEscape(interval.origin_skill)
            << "\",\"contributionType\":" << RdpsContributionIndex(interval.kind)
            << ",\"contributionKind\":\"" << RdpsContributionId(interval.kind)
            << "\",\"effectKind\":" << static_cast<uint32_t>(interval.effect_kind)
            << ",\"effectMin\":" << interval.effect_min
            << ",\"effectMax\":" << interval.effect_max << "}";
    }
    output << "],\n  \"events\":[";
    if (!session.raw_events.empty()) {
        for (size_t index = 0; index < session.raw_events.size(); ++index) {
            if (index) output << ",";
            const auto& event = session.raw_events[index];
            output << "{\"time\":" << event.elapsed_seconds
                << ",\"damage\":" << event.damage << ",\"hpDelta\":" << event.hp_delta;
            output << ",\"damageType\":" << event.damage_type << ",\"damageDecorateMask\":"
                << event.decorate_mask << ",\"critical\":" << (event.critical ? "true" : "false");
            output << ",\"attackerId\":\"" << JsonEscape(event.attacker);
            output << "\",\"originSkillId\":\"" << JsonEscape(event.skill)
                << "\",\"rdpsShares\":[";
            for (uint8_t share_index = 0;
                 share_index < event.rdps_share_count; ++share_index) {
                if (share_index) output << ",";
                const RdpsShare& share = event.rdps_shares[share_index];
                output << "{\"sourceId\":\"" << JsonEscape(share.source)
                    << "\",\"fraction\":" << share.fraction
                    << ",\"contributionType\":"
                    << RdpsContributionIndex(share.kind)
                    << ",\"contributionKind\":\""
                    << RdpsContributionId(share.kind) << "\"}";
            }
            output << "]}";
        }
    }
    output << "]";
    output << "\n}\n";
    Log(std::string("[combat-save] wrote ") + path.string());
}

void ProcessEvent(const DamageEvent& event) {
    std::scoped_lock lock(g_session_mutex);
    if (event.session_id != g_session.id) return;
    const std::string normalized_attacker = event.attacker[0]
        ? NormalizeAttackerId(event.attacker) : "";
    // Enemy attacks use the stable eny_* entity prefix. They are valid damage
    // records internally, but must never enter player DPS/rDPS totals.
    if (IsEnemyAttacker(normalized_attacker)) return;
    const std::string character = StartsWithCharacterPrefix(normalized_attacker)
        ? normalized_attacker : "<unknown>";
    double amount = std::abs(event.damage);
    const double health_change = std::abs(event.hp_delta);
    if (health_change > 0.0)
        amount = std::min(amount, health_change);
    g_session.total_damage += amount;
    ++g_session.hits;
    if (event.critical) ++g_session.critical_hits;
    const std::string skill = event.skill[0] ? event.skill : "<unknown>";
    const size_t skill_category = SkillCategoryFromId(skill);
    static constexpr std::array<const char*, CombatOverlayProtocol::kDamageCategoryCount>
        kCategoryIds{"basic_attack", "skill", "ultimate", "combo", "passive", "other"};
    auto add = [&event, amount](Aggregate& aggregate) {
        aggregate.damage += amount;
        ++aggregate.hits;
        if (event.critical) ++aggregate.critical_hits;
    };
    using RdpsSourceKey = std::pair<std::string, uint8_t>;
    std::map<RdpsSourceKey, double> external_fractions;
    for (uint8_t index = 0; index < event.rdps_share_count; ++index) {
        const RdpsShare& share = event.rdps_shares[index];
        if (!std::isfinite(share.fraction) || share.fraction <= 0.0 ||
            share.source[0] == '\0') continue;
        const std::string source = NormalizeAttackerId(share.source);
        if (!StartsWithCharacterPrefix(source) ||
            _stricmp(source.c_str(), character.c_str()) == 0) continue;
        external_fractions[{source, static_cast<uint8_t>(share.kind)}] +=
            share.fraction;
    }
    double transferred_fraction = 0.0;
    for (const auto& [key, fraction] : external_fractions) {
        (void)key;
        transferred_fraction += std::max(0.0, fraction);
    }
    const double rdps_scale = transferred_fraction > 0.999999
        ? 0.999999 / transferred_fraction : 1.0;
    transferred_fraction = std::min(0.999999, transferred_fraction);
    const double retained = amount * (1.0 - transferred_fraction);
    CharacterAggregate& aggregate = g_session.characters[character];
    add(aggregate);
    aggregate.damage_by_category[skill_category] += amount;

    CharacterAggregate& rdps_attacker = g_session.rdps_characters[character];
    rdps_attacker.damage += retained;
    ++rdps_attacker.hits;
    if (event.critical) ++rdps_attacker.critical_hits;
    rdps_attacker.damage_by_category[skill_category] += retained;
    rdps_attacker.rdps_by_contribution[RdpsContributionIndex(
        RdpsContributionKind::DirectDamage)] += retained;
    for (const auto& [key, fraction] : external_fractions) {
        const auto& [source, kind_value] = key;
        const double credited = amount * std::max(0.0, fraction) * rdps_scale;
        if (credited <= 0.0) continue;
        CharacterAggregate& support = g_session.rdps_characters[source];
        support.damage += credited;
        support.damage_by_category[skill_category] += credited;
        const size_t contribution = std::min<size_t>(kind_value,
            CombatOverlayProtocol::kRdpsContributionCount - 1);
        support.rdps_by_contribution[contribution] += credited;
    }
    add(g_session.skills[skill]);
    add(g_session.damage_categories[kCategoryIds[skill_category]]);
    constexpr double kTimelineBucketSeconds = 0.25;
    const auto bucket_index = static_cast<uint32_t>(std::max(0.0,
        std::floor(event.elapsed_seconds / kTimelineBucketSeconds)));
    TimelineBucket& bucket = g_session.timeline[bucket_index];
    bucket.damage_by_category[skill_category] += amount;
    bucket.damage_by_character[character] += amount;
    bucket.rdps_by_character[character] += retained;
    bucket.rdps_by_contribution[RdpsContributionIndex(
        RdpsContributionKind::DirectDamage)] += retained;
    for (const auto& [key, fraction] : external_fractions) {
        const auto& [source, kind_value] = key;
        const double credited = amount * std::max(0.0, fraction) * rdps_scale;
        bucket.rdps_by_character[source] += credited;
        const size_t contribution = std::min<size_t>(kind_value,
            CombatOverlayProtocol::kRdpsContributionCount - 1);
        bucket.rdps_by_contribution[contribution] += credited;
    }
    // Schema 11 is event sourced, so every atomic player-damage outcome is kept.
    if (g_session.raw_events.size() < kMaxRawEvents)
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
    const std::wstring parameters = L"--game-pid " +
        std::to_wstring(GetCurrentProcessId()) + L" --mapping \"" + mapping_name + L"\"";
    std::wstring command = L"\"" + executable.wstring() + L"\" " + parameters;
    STARTUPINFOW startup{};
    startup.cb = sizeof(startup);
    PROCESS_INFORMATION process{};
    const std::wstring working_directory = executable.parent_path().wstring();
    if (!CreateProcessW(executable.c_str(), command.data(), nullptr, nullptr, FALSE,
            CREATE_NO_WINDOW, nullptr, working_directory.c_str(), &startup, &process)) {
        const DWORD create_error = GetLastError();
        if (create_error != ERROR_ELEVATION_REQUIRED) {
            Log("[combat-overlay] launch failed error=" + std::to_string(create_error));
            return;
        }

        SHELLEXECUTEINFOW shell{};
        shell.cbSize = sizeof(shell);
        shell.fMask = SEE_MASK_NOCLOSEPROCESS;
        shell.lpVerb = L"runas";
        shell.lpFile = executable.c_str();
        shell.lpParameters = parameters.c_str();
        shell.lpDirectory = working_directory.c_str();
        shell.nShow = SW_SHOWNOACTIVATE;
        if (!ShellExecuteExW(&shell) || !shell.hProcess) {
            Log("[combat-overlay] elevation launch failed error=" +
                std::to_string(GetLastError()));
            return;
        }
        g_overlay_process = shell.hProcess;
        Log("[combat-overlay] companion elevated after CreateProcess error=740 pid=" +
            std::to_string(GetProcessId(shell.hProcess)));
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
    const auto& selected_characters = config.rdps_display
        ? session.rdps_characters : session.characters;
    double total_rdps_damage = 0.0;
    for (const auto& [id, aggregate] : session.rdps_characters) {
        (void)id;
        total_rdps_damage += aggregate.damage;
    }
    std::vector<std::pair<std::string, CharacterAggregate>> characters(
        selected_characters.begin(), selected_characters.end());
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
    g_overlay_snapshot->metric_mode = config.rdps_display ? 1u : 0u;
    g_overlay_snapshot->session_id = session.id;
    g_overlay_snapshot->duration_seconds = duration;
    g_overlay_snapshot->total_damage = session.total_damage;
    g_overlay_snapshot->rdps = duration > 0.0 ? total_rdps_damage / duration : 0.0;
    g_overlay_snapshot->dps = duration > 0.0
        ? (config.rdps_display ? total_rdps_damage : session.total_damage) / duration
        : 0.0;
    g_overlay_snapshot->hit_count = session.hits;
    g_overlay_snapshot->critical_count = session.critical_hits;
    if (config.rdps_display) {
        g_overlay_snapshot->active_category_count = static_cast<uint32_t>(kDynamicRdpsCategories.size());
        for (size_t i = 0; i < kDynamicRdpsCategories.size() && i < CombatOverlayProtocol::kDisplaySegmentCount; ++i) {
            std::snprintf(g_overlay_snapshot->categories[i].name,
                sizeof(g_overlay_snapshot->categories[i].name), "%s", kDynamicRdpsCategories[i].name);
            g_overlay_snapshot->categories[i].color_argb = kDynamicRdpsCategories[i].color_argb;
        }
    } else {
        g_overlay_snapshot->active_category_count = CombatOverlayProtocol::kDamageCategoryCount;
        static const char* const kDirectNames[CombatOverlayProtocol::kDamageCategoryCount] = {
            "普攻", "战技", "终结技", "连携", "被动", "其他"
        };
        static const uint32_t kDirectColorsArgb[CombatOverlayProtocol::kDamageCategoryCount] = {
            0xFFD3D8E1, 0xFF43C9FF, 0xFFFF6741, 0xFFAA82FF, 0xFF5BD68B, 0xFF8F98AA
        };
        for (uint32_t i = 0; i < CombatOverlayProtocol::kDamageCategoryCount; ++i) {
            std::snprintf(g_overlay_snapshot->categories[i].name,
                sizeof(g_overlay_snapshot->categories[i].name), "%s", kDirectNames[i]);
            g_overlay_snapshot->categories[i].color_argb = kDirectColorsArgb[i];
        }
    }
    g_overlay_snapshot->character_count = static_cast<uint32_t>(std::min<size_t>(
        characters.size(), CombatOverlayProtocol::kMaxCharacters));
    std::memset(g_overlay_snapshot->characters, 0, sizeof(g_overlay_snapshot->characters));
    for (uint32_t index = 0; index < g_overlay_snapshot->character_count; ++index) {
        auto& destination = g_overlay_snapshot->characters[index];
        const auto& [id, aggregate] = characters[index];
        std::snprintf(destination.id, sizeof(destination.id), "%s", id.c_str());
        destination.total_damage = aggregate.damage;
        destination.dps = duration > 0.0 ? aggregate.damage / duration : 0.0;
        destination.hits = aggregate.hits;
        destination.critical_hits = aggregate.critical_hits;
        for (auto action = session.actions.rbegin();
             action != session.actions.rend(); ++action) {
            if (action->active && action->actor == id) {
                std::snprintf(destination.current_action,
                    sizeof(destination.current_action), "%s", action->skill.c_str());
                break;
            }
        }
        if (config.rdps_display) {
            std::copy(aggregate.rdps_by_contribution.begin(),
                aggregate.rdps_by_contribution.end(),
                destination.damage_by_category);
        } else {
            std::copy(aggregate.damage_by_category.begin(),
                aggregate.damage_by_category.end(),
                destination.damage_by_category);
        }
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
    std::vector<std::pair<uint64_t, BuffDiagnosticInfo>> active_buffs;
    {
        std::scoped_lock buff_lock(g_buff_diagnostic_mutex);
        active_buffs.reserve(g_buff_diagnostic_map.size());
        for (const auto& entry : g_buff_diagnostic_map) active_buffs.push_back(entry);
    }
    std::scoped_lock lock(g_session_mutex);
    if (g_session_active.load(std::memory_order_acquire)) return;
    g_session = {};
    g_session.id = static_cast<uint64_t>(
        std::chrono::steady_clock::now().time_since_epoch().count());
    g_modifier_samples.store(0, std::memory_order_relaxed);
    g_rdps_damage_samples.store(0, std::memory_order_relaxed);
    g_rdps_buff_samples.store(0, std::memory_order_relaxed);
    g_rdps_value_samples.store(0, std::memory_order_relaxed);
    g_rdps_flow_samples.store(0, std::memory_order_relaxed);
    g_rdps_processor_samples.store(0, std::memory_order_relaxed);
    g_rdps_attribute_share_samples.store(0, std::memory_order_relaxed);
    g_semantic_observed.store(0, std::memory_order_relaxed);
    g_semantic_verified.store(0, std::memory_order_relaxed);
    g_semantic_candidate.store(0, std::memory_order_relaxed);
    g_semantic_excluded.store(0, std::memory_order_relaxed);
    g_semantic_unknown.store(0, std::memory_order_relaxed);
    g_semantic_element_mismatch.store(0, std::memory_order_relaxed);
    g_semantic_unresolved_dropped.store(0, std::memory_order_relaxed);
    {
        std::scoped_lock audit_lock(g_semantic_audit_mutex);
        g_semantic_audit.clear();
    }
    g_rdps_flow_sequence.store(0, std::memory_order_relaxed);
    g_rdps_flow_generation.fetch_add(1, std::memory_order_acq_rel);
    g_session.started = std::chrono::steady_clock::now();
    g_session.started_utc = std::chrono::system_clock::now();
    g_session.active = true;
    if (const auto catalog = SemanticCatalogSnapshot()) {
        const SemanticCatalogSummary& summary = catalog->Summary();
        g_session.semantic_catalog_schema = summary.schema_version;
        g_session.semantic_game_version = summary.game_version;
        g_session.semantic_hotfix_version = summary.hotfix_version;
        g_session.semantic_source_sha256 = summary.source_sha256;
    }
    g_session.record_damage_baseline = g_record_damage_calls.load(
        std::memory_order_relaxed);
    g_session.record_detail_baseline = g_record_detail_calls.load(
        std::memory_order_relaxed);
    g_session.detail_init_baseline = g_detail_init_calls.load(
        std::memory_order_relaxed);
    g_session.damage_text_baseline = g_text_hits.load(
        std::memory_order_relaxed);
    for (const auto& [inst_id, info] : active_buffs) {
        if (!inst_id || info.buff_id.empty() ||
            g_session.buff_intervals.size() >= kMaxBuffIntervals) continue;
        BuffInterval interval;
        interval.inst_id = inst_id;
        interval.start_time = 0.0;
        interval.kind = ClassifyBuffInterval(info);
        interval.relevant = false;
        interval.buff_id = info.buff_id;
        interval.source = info.source;
        interval.owner = info.owner;
        interval.origin_skill = info.origin_skill;
        interval.credited_actor_id = info.credited_actor_id;
        interval.source_kind = info.source_kind;
        interval.source_template_id = info.source_template_id;
        interval.source_skill_id = info.source_skill_id;
        interval.actor_attribution = info.actor_attribution;
        interval.source_attribution = info.source_attribution;
        interval.attribution_reason = info.attribution_reason;
        g_session.open_buff_intervals[inst_id] = g_session.buff_intervals.size();
        g_session.buff_intervals.push_back(std::move(interval));
    }
    g_session_active.store(true, std::memory_order_release);
    g_squad_snapshot_logged.store(false, std::memory_order_release);
    Log("[combat-session] started");
}

// Baseline Buffs (applied before the squad snapshot exists) resolve their
// dictionary attribution against an empty squad and stay unknown. Re-run the
// same exact matching against the final squad before the record is saved.
void ReResolveBuffAttributionsLocked() {
    const auto catalog = SemanticCatalogSnapshot();
    if (!catalog) return;
    size_t upgraded = 0;
    for (BuffInterval& interval : g_session.buff_intervals) {
        if (interval.source_attribution == "dictionaryExact" ||
            interval.buff_id.empty() ||
            interval.actor_attribution != "runtimeExact" ||
            interval.credited_actor_id.empty()) {
            continue;
        }
        const Session::SquadMember* member = nullptr;
        for (const auto& value : g_session.squad) {
            if (_stricmp(value.char_id.c_str(),
                    interval.credited_actor_id.c_str()) == 0) {
                member = &value;
                break;
            }
        }
        BuffSourceMatch match = MatchBuffSourceCandidates(*catalog, member,
            interval.buff_id, interval.origin_skill);
        if (match.resolved) {
            interval.source_kind = std::move(match.kind);
            interval.source_template_id = std::move(match.template_id);
            interval.source_skill_id = std::move(match.skill_id);
            interval.source_attribution = "dictionaryExact";
            AppendAttributionReason(interval.attribution_reason,
                match.trigger_matched ?
                    "reresolved_source_reference_trigger_and_loadout_exact" :
                    "reresolved_source_reference_and_loadout_exact");
            ++upgraded;
            continue;
        }
        std::string edge_kind;
        std::string edge_template;
        std::string edge_skill;
        if (ApplyOriginSkillReference(*catalog, interval.origin_skill,
                interval.buff_id, interval.credited_actor_id,
                interval.actor_attribution,
                edge_kind, edge_template, edge_skill)) {
            interval.source_kind = std::move(edge_kind);
            interval.source_template_id = std::move(edge_template);
            interval.source_skill_id = std::move(edge_skill);
            interval.source_attribution = "dictionaryExact";
            AppendAttributionReason(interval.attribution_reason,
                "origin_skill_config_reference");
            ++upgraded;
        }
    }
    if (upgraded) {
        Log("[combat-diag] save-time attribution upgraded=" +
            std::to_string(upgraded));
    }
}

void StopSession() {
    if (!g_session_active.exchange(false)) return;
    uint64_t record_damage_calls = 0;
    uint64_t record_detail_calls = 0;
    uint64_t detail_init_calls = 0;
    uint64_t damage_text_calls = 0;
    uint64_t action_start_calls = 0;
    uint64_t action_end_calls = 0;
    uint64_t action_end_matches = 0;
    uint64_t dodge_calls = 0;
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
        g_session.semantic_coverage.observed = g_semantic_observed.load(
            std::memory_order_relaxed);
        g_session.semantic_coverage.verified = g_semantic_verified.load(
            std::memory_order_relaxed);
        g_session.semantic_coverage.candidate = g_semantic_candidate.load(
            std::memory_order_relaxed);
        g_session.semantic_coverage.excluded = g_semantic_excluded.load(
            std::memory_order_relaxed);
        g_session.semantic_coverage.unknown = g_semantic_unknown.load(
            std::memory_order_relaxed);
        g_session.semantic_coverage.element_mismatch =
            g_semantic_element_mismatch.load(std::memory_order_relaxed);
        g_session.semantic_coverage.unresolved_dropped =
            g_semantic_unresolved_dropped.load(std::memory_order_relaxed);
        {
            std::scoped_lock audit_lock(g_semantic_audit_mutex);
            g_session.unresolved_semantics.clear();
            g_session.unresolved_semantics.reserve(g_semantic_audit.size());
            for (const auto& [key, value] : g_semantic_audit) {
                g_session.unresolved_semantics.push_back(value);
            }
        }
        std::sort(g_session.unresolved_semantics.begin(),
            g_session.unresolved_semantics.end(),
            [](const Session::SemanticAuditItem& left,
                const Session::SemanticAuditItem& right) {
                if (left.count != right.count) return left.count > right.count;
                return left.buff_id < right.buff_id;
            });
        constexpr size_t kSavedSemanticAuditItems = 64;
        if (g_session.unresolved_semantics.size() > kSavedSemanticAuditItems) {
            g_session.semantic_coverage.unresolved_dropped +=
                g_session.unresolved_semantics.size() - kSavedSemanticAuditItems;
            g_session.unresolved_semantics.resize(kSavedSemanticAuditItems);
        }
        const double stopped_at = SessionElapsedLocked(g_session.ended);
        for (const auto& [skill, index] : g_session.open_actions) {
            (void)skill;
            if (index >= g_session.actions.size()) continue;
            g_session.actions[index].end_time = stopped_at;
            g_session.actions[index].active = false;
        }
        g_session.open_actions.clear();
        for (const auto& [inst_id, index] : g_session.open_buff_intervals) {
            (void)inst_id;
            if (index >= g_session.buff_intervals.size()) continue;
            g_session.buff_intervals[index].end_time = stopped_at;
            g_session.buff_intervals[index].active = false;
        }
        g_session.open_buff_intervals.clear();
        for (const auto& [inst_id, index] : g_session.open_stagger_intervals) {
            (void)inst_id;
            if (index >= g_session.stagger_intervals.size()) continue;
            g_session.stagger_intervals[index].end_time = stopped_at;
            g_session.stagger_intervals[index].active = false;
        }
        g_session.open_stagger_intervals.clear();
        ReResolveBuffAttributionsLocked();
        record_damage_calls = g_session.record_damage_calls;
        record_detail_calls = g_session.record_detail_calls;
        detail_init_calls = g_session.detail_init_calls;
        damage_text_calls = g_session.damage_text_calls;
        action_start_calls = g_session.action_start_calls;
        action_end_calls = g_session.action_end_calls;
        action_end_matches = g_session.action_end_matches;
        dodge_calls = g_session.dodge_calls;
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
    Log("[combat-diag] action capture start=" + std::to_string(action_start_calls) +
        " end=" + std::to_string(action_end_calls) + " matched=" +
        std::to_string(action_end_matches) + " dodge=" +
        std::to_string(dodge_calls));
    Log("[combat-session] stop requested; result will be saved after queued hits");
}

struct HotkeyLatch {
    bool pressed = false;
    uint8_t released_samples = 0;
};

bool KeyPressed(int vk, bool ctrl, HotkeyLatch& latch) {
    const bool down = (GetAsyncKeyState(vk) & 0x8000) != 0 &&
        (!ctrl || (GetAsyncKeyState(VK_CONTROL) & 0x8000) != 0);
    if (down) {
        latch.released_samples = 0;
        if (latch.pressed) return false;
        latch.pressed = true;
        return true;
    }
    // Require three consecutive 100 ms release samples before rearming. This
    // filters transient key-state gaps while the game changes input contexts.
    if (latch.pressed && ++latch.released_samples >= 3) {
        latch.pressed = false;
        latch.released_samples = 0;
    }
    return false;
}

void PollHotkeys() {
    static HotkeyLatch toggle_latch;
    static HotkeyLatch overlay_latch;
    const Configuration config = ConfigurationSnapshot();
    if (!config.stats_enabled) return;
    if (KeyPressed(config.toggle_vk, config.toggle_ctrl, toggle_latch)) {
        if (g_session_active.load(std::memory_order_acquire)) StopSession();
        else StartSession();
    }
    if (config.overlay_enabled &&
        KeyPressed(config.overlay_vk, config.overlay_ctrl, overlay_latch)) {
        const bool visible = !g_overlay_visible.load(std::memory_order_relaxed);
        g_overlay_visible.store(visible, std::memory_order_relaxed);
        Log(std::string("[combat-overlay] visibility=") + (visible ? "shown" : "hidden"));
    }
}

void __fastcall DamageProcessorProcessHook(void* instance, int32_t timing,
    void* pack_data, void* blackboard, void* context, void* server_args, void* method) {
    const Configuration config = ConfigurationSnapshot();
    const bool observe = g_original_damage_processor_process && config.diagnostics &&
        g_session_active.load(std::memory_order_acquire) &&
        g_instant_attribute_contract_ready && pack_data;
    if (!observe) {
        if (g_original_damage_processor_process) {
            g_original_damage_processor_process(instance, timing, pack_data,
                blackboard, context, server_args, method);
        }
        return;
    }
    if (!g_damage_flow_context.active || g_damage_flow_context.pack != pack_data ||
        g_damage_flow_context.calculated) {
        BeginDamageFlow(pack_data);
    }
    const int32_t parent_timing = g_damage_processor_timing;
    g_damage_processor_timing = timing;
    g_original_damage_processor_process(instance, timing, pack_data,
        blackboard, context, server_args, method);
    g_damage_processor_timing = parent_timing;
}

void __fastcall InstantModifyAttributeHook(void* instance, void* pack_data,
    void* blackboard, void* context, void* server_args, void* method) {
    const Configuration config = ConfigurationSnapshot();
    const bool observe = g_original_instant_attribute_process && config.stats_enabled &&
        g_session_active.load(std::memory_order_acquire) &&
        g_instant_attribute_contract_ready && pack_data;
    if (!observe) {
        if (g_original_instant_attribute_process) {
            g_original_instant_attribute_process(instance, pack_data, blackboard,
                context, server_args, method);
        }
        return;
    }

    int32_t target_side = -1;
    int32_t modify_type = -1;
    int32_t attribute_type = -1;
    int32_t formula_item = -1;
    void* modifier = nullptr;
    void* parameter = nullptr;
    double parameter_value = 0.0;
    bool parameter_available = false;
    ReadValue(instance, g_instant_attribute_target_side, target_side);
    if (ReadValue(instance, g_instant_attribute_modifier, modifier) && modifier) {
        ReadValue(modifier, g_attribute_modifier_modify_type, modify_type);
        ReadValue(modifier, g_attribute_modifier_attribute_type, attribute_type);
        ReadValue(modifier, g_attribute_modifier_formula_item, formula_item);
        if (ReadValue(modifier, g_attribute_modifier_param, parameter)) {
            parameter_available = ReadBlackboardDoubleValue(
                parameter, blackboard, parameter_value);
        }
    }
    DamagePackNumericSnapshot before;
    const bool before_ok = CapturePackNumericSnapshot(pack_data, before);
    double defense_before = 0.0;
    double resistance_before = 0.0;
    const bool defense_before_ok = CallPackDouble(
        g_get_def_resistance_value, pack_data, defense_before);
    const bool resistance_before_ok = CallPackDouble(
        g_get_damage_type_resistance_value, pack_data, resistance_before);
    g_original_instant_attribute_process(instance, pack_data, blackboard,
        context, server_args, method);
    DamagePackNumericSnapshot after;
    const bool after_ok = CapturePackNumericSnapshot(pack_data, after);
    double defense_after = 0.0;
    double resistance_after = 0.0;
    const bool defense_after_ok = CallPackDouble(
        g_get_def_resistance_value, pack_data, defense_after);
    const bool resistance_after_ok = CallPackDouble(
        g_get_damage_type_resistance_value, pack_data, resistance_after);
    if (!before_ok || !after_ok) return;
    std::ostringstream changes;
    changes << std::setprecision(10);
    const size_t changed = AppendNumericChanges(changes, before, after);
    uint64_t buff_inst_id = 0;
    int32_t modifier_index = -1;
    void* handle = ReadHandleReference(
        g_numeric_hook_context.modifier_handle_reference);
    ReadValue(handle, g_modifier_handle_buff_inst_id, buff_inst_id);
    ReadValue(handle, g_modifier_handle_index, modifier_index);
    BuffDiagnosticInfo buff;
    {
        std::scoped_lock lock(g_buff_diagnostic_mutex);
        if (const auto found = g_buff_diagnostic_map.find(buff_inst_id);
            found != g_buff_diagnostic_map.end()) {
            buff = found->second;
        }
    }
    double factor_multiplier = 1.0;
    if (defense_before_ok && defense_after_ok && defense_before > 1.0e-12 &&
        defense_after > defense_before + 1.0e-12) {
        factor_multiplier *= defense_after / defense_before;
    }
    if (resistance_before_ok && resistance_after_ok &&
        resistance_before > 1.0e-12 &&
        resistance_after > resistance_before + 1.0e-12) {
        factor_multiplier *= resistance_after / resistance_before;
    }
    if (factor_multiplier > 1.0 + 1.0e-12 &&
        IsExternalDamageSource(buff)) {
        const SemanticResolution semantic = ResolveBuffSemantic(buff.buff_id,
            SemanticZone::Resistance, g_damage_flow_context.damage_type);
        RecordSemanticResolution(semantic, buff.buff_id,
            SemanticZone::Resistance);
        if (semantic.status == SemanticStatus::Verified &&
            semantic.zone == SemanticZone::Resistance &&
            g_damage_flow_context.factor_count <
                g_damage_flow_context.factors.size()) {
            RdpsFactorSample& factor = g_damage_flow_context.factors[
                g_damage_flow_context.factor_count++];
            factor.buff_inst_id = buff_inst_id;
            factor.multiplier = factor_multiplier;
            factor.kind = RdpsContributionKind::Resistance;
            factor.semantic_zone = SemanticZone::Resistance;
            std::snprintf(factor.buff_id, sizeof(factor.buff_id), "%s",
                buff.buff_id.c_str());
            const std::string_view credited_source = CreditedBuffSource(buff);
            std::snprintf(factor.source, sizeof(factor.source), "%.*s",
                static_cast<int>(credited_source.size()), credited_source.data());
        }
    }
    if (!config.diagnostics || (!changed && factor_multiplier <= 1.0 + 1.0e-12)) {
        return;
    }
    const uint32_t sample = g_rdps_processor_samples.fetch_add(1,
        std::memory_order_relaxed);
    if (sample >= 128) return;
    std::ostringstream output;
    output << std::setprecision(10)
        << "[rdps-processor] sample=" << sample
        << " txn=" << CurrentDamageTransaction(pack_data)
        << " timing=" << g_damage_processor_timing
        << " processor=InstantModifyAttribute"
        << " instance=" << instance
        << " side=" << target_side
        << " modifyType=" << modify_type
        << " attributeType=" << attribute_type
        << " formula=" << formula_item
        << " param=";
    if (parameter_available) output << parameter_value;
    else output << "<unavailable>";
    output
        << " buff=" << buff_inst_id
        << " modifier=" << modifier_index
        << " id=\"" << buff.buff_id << "\""
        << " source=\"" << buff.source << "\""
        << " owner=\"" << buff.owner << "\""
        << " skill=\"" << buff.origin_skill << "\""
        << " changed=" << changed << changes.str();
    if (factor_multiplier > 1.0 + 1.0e-12) {
        output << " factor=" << factor_multiplier
            << " defense=" << defense_before << "->" << defense_after
            << " resistance=" << resistance_before << "->" << resistance_after;
    }
    Log(output.str());
}

void __fastcall ApplyDamageModifierHook(
    void* pack_data, int32_t timing, void* method) {
    const Configuration config = ConfigurationSnapshot();
    const bool observe = g_original_apply_damage_modifier && config.stats_enabled &&
        g_session_active.load(std::memory_order_acquire) &&
        g_rdps_flow_contract_ready && pack_data;
    if (!observe) {
        if (g_original_apply_damage_modifier) {
            g_original_apply_damage_modifier(pack_data, timing, method);
        }
        return;
    }

    if (!g_damage_flow_context.active || g_damage_flow_context.pack != pack_data ||
        (timing == 1 && g_damage_flow_context.calculated)) {
        BeginDamageFlow(pack_data);
    }
    DamagePackNumericSnapshot before;
    const bool before_ok = CapturePackNumericSnapshot(pack_data, before);
    g_original_apply_damage_modifier(pack_data, timing, method);
    DamagePackNumericSnapshot after;
    const bool after_ok = CapturePackNumericSnapshot(pack_data, after);

    if (!config.diagnostics) return;
    const uint32_t sample = g_rdps_flow_samples.fetch_add(1,
        std::memory_order_relaxed);
    if (sample >= 384) return;
    std::ostringstream output;
    output << std::setprecision(10)
        << "[rdps-flow] sample=" << sample
        << " txn=" << CurrentDamageTransaction(pack_data)
        << " stage=apply timing=" << timing
        << " pack=" << pack_data;
    if (before_ok && after_ok) {
        std::ostringstream changes;
        changes << std::setprecision(10);
        const size_t changed = AppendNumericChanges(changes, before, after);
        output << " changed=" << changed << changes.str();
    } else {
        output << " snapshot=failed";
    }
    Log(output.str());
}

double __fastcall CalculateDamageHook(void* pack_data, bool* is_critical,
    bool* is_blocked, void* report_info, void* method) {
    const Configuration config = ConfigurationSnapshot();
    const bool observe = g_original_calculate_damage && config.stats_enabled &&
        g_session_active.load(std::memory_order_acquire) &&
        g_rdps_flow_contract_ready && pack_data;
    if (!observe) {
        return g_original_calculate_damage
            ? g_original_calculate_damage(
                pack_data, is_critical, is_blocked, report_info, method)
            : 0.0;
    }

    if (!g_damage_flow_context.active || g_damage_flow_context.pack != pack_data ||
        g_damage_flow_context.calculated) {
        BeginDamageFlow(pack_data);
    }
    const double result = g_original_calculate_damage(
        pack_data, is_critical, is_blocked, report_info, method);
    g_damage_flow_context.calculated = true;
    g_damage_flow_context.calculated_damage = result;
    g_damage_flow_context.server_detail = ReadDamagePackServerDetail(pack_data);
    DamagePackNumericSnapshot final_snapshot;
    const bool snapshot_ok = CapturePackNumericSnapshot(pack_data, final_snapshot);
    RememberCompletedDamageFlow(final_snapshot);
    const bool critical = ReadBoolPointer(is_critical);
    const bool blocked = ReadBoolPointer(is_blocked);
    const uint32_t sample = config.diagnostics
        ? g_rdps_flow_samples.fetch_add(1, std::memory_order_relaxed)
        : 384;
    if (config.diagnostics && sample < 384) {
        std::ostringstream output;
        output << std::setprecision(10)
            << "[rdps-flow] sample=" << sample
            << " txn=" << g_damage_flow_context.transaction
            << " stage=calculate pack=" << pack_data
            << " detail=" << g_damage_flow_context.server_detail
            << " result=" << result
            << " critical=" << (critical ? "true" : "false")
            << " blocked=" << (blocked ? "true" : "false");
        if (snapshot_ok) {
            output << " calcResult=" << final_snapshot.calc_result
                << " atkScale=" << final_snapshot.atk_scale;
        } else {
            output << " snapshot=failed";
        }
        Log(output.str());
    }
    return result;
}

void __fastcall DamageScaleZoneModifyHook(void* pack_data, int32_t side,
    void* zone_name, double value, void* method) {
    if (g_numeric_hook_context.active &&
        g_numeric_hook_context.zone_count < g_numeric_hook_context.zones.size()) {
        ZoneCallSnapshot& call =
            g_numeric_hook_context.zones[g_numeric_hook_context.zone_count++];
        call.side = side;
        call.value = value;
        const std::string name = CopyString(zone_name);
        std::snprintf(call.name, sizeof(call.name), "%s", name.c_str());
    }
    if (g_original_damage_scale_zone_modify) {
        g_original_damage_scale_zone_modify(pack_data, side, zone_name, value, method);
    }
}

void __fastcall DamageModifierProcessHook(void* instance, int32_t timing,
    void** modifier_handle, void* pack_data, void* method) {
    const Configuration config = ConfigurationSnapshot();
    if (!g_original_damage_modifier_process || !config.stats_enabled ||
        !g_session_active.load(std::memory_order_acquire) ||
        !g_rdps_value_contract_ready) {
        if (g_original_damage_modifier_process) {
            g_original_damage_modifier_process(
                instance, timing, modifier_handle, pack_data, method);
        }
        return;
    }

    DamagePackNumericSnapshot before;
    const bool before_ok = CapturePackNumericSnapshot(pack_data, before);
    const NumericHookContext parent_context = g_numeric_hook_context;
    g_numeric_hook_context = {};
    g_numeric_hook_context.active = true;
    g_numeric_hook_context.modifier_handle_reference = modifier_handle;
    g_original_damage_modifier_process(instance, timing, modifier_handle, pack_data, method);
    const NumericHookContext modifier_context = g_numeric_hook_context;
    g_numeric_hook_context = parent_context;
    DamagePackNumericSnapshot after;
    const bool after_ok = CapturePackNumericSnapshot(pack_data, after);
    if (!before_ok || !after_ok) {
        const uint32_t sample = g_rdps_value_samples.fetch_add(1,
            std::memory_order_relaxed);
        if (sample < 4) {
            Log("[rdps-value] pack snapshot failed timing=" +
                std::to_string(timing));
        }
        return;
    }

    std::ostringstream changes;
    changes << std::setprecision(10);
    const size_t changed = AppendNumericChanges(changes, before, after);
    if (!changed && modifier_context.zone_count == 0) return;

    void* handle = ReadHandleReference(modifier_handle);
    uint64_t buff_inst_id = 0;
    int32_t modifier_index = -1;
    if (handle) {
        ReadValue(handle, g_modifier_handle_buff_inst_id, buff_inst_id);
        ReadValue(handle, g_modifier_handle_index, modifier_index);
    }
    BuffDiagnosticInfo buff;
    {
        std::scoped_lock lock(g_buff_diagnostic_mutex);
        if (buff_inst_id == 0 && instance) {
            if (const auto mod_found = g_modifier_to_buff_map.find(instance);
                mod_found != g_modifier_to_buff_map.end()) {
                buff_inst_id = mod_found->second;
            }
        }
        if (const auto found = g_buff_diagnostic_map.find(buff_inst_id);
            found != g_buff_diagnostic_map.end()) {
            buff = found->second;
        }
    }
    const uint8_t modifier_count_before = g_damage_flow_context.modifier_count;
    CaptureRdpsModifierSamples(before, after, buff, buff_inst_id);
    for (uint8_t index = modifier_count_before;
         index < g_damage_flow_context.modifier_count; ++index) {
        MarkBuffIntervalRelevant(buff_inst_id,
            g_damage_flow_context.modifiers[index].kind,
            g_damage_flow_context.modifiers[index].delta,
            BuffEffectKind::MultiplierDelta);
    }
    if (!config.diagnostics) return;
    const uint32_t sample = g_rdps_value_samples.fetch_add(1,
        std::memory_order_relaxed);
    if (sample >= 256) return;
    std::ostringstream output;
    output << std::setprecision(10)
        << "[rdps-value] sample=" << sample
        << " txn=" << CurrentDamageTransaction(pack_data)
        << " timing=" << timing
        << " buff=" << buff_inst_id
        << " modifier=" << modifier_index
        << " id=\"" << buff.buff_id << "\""
        << " source=\"" << buff.source << "\""
        << " owner=\"" << buff.owner << "\""
        << " skill=\"" << buff.origin_skill << "\""
        << " changed=" << changed << changes.str();
    if (modifier_context.zone_count) {
        output << " zoneCalls=[";
        for (uint32_t index = 0; index < modifier_context.zone_count; ++index) {
            if (index) output << ",";
            const ZoneCallSnapshot& zone = modifier_context.zones[index];
            output << "{side=" << zone.side << ",name=\"" << zone.name
                << "\",value=" << zone.value << "}";
        }
        output << "]";
    }
    Log(output.str());
}

void __fastcall RecordCastSkillHook(
    void* instance, void* caster, void* skill, void* method) {
    TrackActionStart(caster, skill);
    if (g_original_record_cast_skill) {
        g_original_record_cast_skill(instance, caster, skill, method);
    }
}

void __fastcall RecordEndSkillHook(void* instance, void* skill,
    int32_t finish_type, int32_t interrupt_reason, bool is_cancelled,
    void* method) {
    TrackActionEnd(skill, finish_type, interrupt_reason, is_cancelled);
    if (g_original_record_end_skill) {
        g_original_record_end_skill(instance, skill, finish_type,
            interrupt_reason, is_cancelled, method);
    }
}

void __fastcall RecordDodgeSuccessHook(
    void* instance, void* source, void* method) {
    TrackDodgeAction(source);
    if (g_original_record_dodge_success) {
        g_original_record_dodge_success(instance, source, method);
    }
}

void __fastcall SkillCastEndHook(void* skill, int32_t finish_type,
    int32_t interrupt_reason, void* interrupt_context, void* method) {
    TrackActionEnd(skill, finish_type, interrupt_reason, false);
    if (g_original_skill_cast_end) {
        g_original_skill_cast_end(skill, finish_type, interrupt_reason,
            interrupt_context, method);
    }
}

void __fastcall RecordBuffStartHook(void* instance, void* buff, void* method) {
    if (g_original_record_buff_start) {
        g_original_record_buff_start(instance, buff, method);
    }
    if (!g_squad_snapshot_logged.exchange(true, std::memory_order_acquire)) {
        LogSquadSnapshot();
        LogDungeonMetadata(instance);
    }
    const Configuration config = ConfigurationSnapshot();
    if (!config.stats_enabled || !g_buff_diagnostic_contract_ready || !buff) return;
    const uint64_t inst_id = ReadBuffInstId(buff);
    BuffDiagnosticInfo info = CaptureBuffDiagnosticInfo(buff);
    info.attribute_contributions = CaptureBuffAttributeContributions(buff);
    ResolveExactBuffAttribution(info);
    if (inst_id != 0) {
        {
            std::scoped_lock lock(g_buff_diagnostic_mutex);
            g_buff_diagnostic_map[inst_id] = info;
            for (void* mod : info.damage_modifiers) {
                if (mod) g_modifier_to_buff_map[mod] = inst_id;
            }
        }
        TrackBuffStart(inst_id, info);
        if (IsPoiseBreakBuff(info.buff_id)) {
            TrackStaggerStart(inst_id, info);
        }
    }
    const uint32_t sample = config.diagnostics
        ? g_rdps_buff_samples.fetch_add(1, std::memory_order_relaxed)
        : 128;
    if (config.diagnostics && sample < 128) {
        std::ostringstream output;
        output << "[rdps-diag] buff-start inst=" << inst_id
            << " id=\"" << info.buff_id << "\" source=\"" << info.source
            << "\" owner=\"" << info.owner << "\" skill=\""
            << info.origin_skill << "\" attributes="
            << info.attribute_contributions.size();
        if (info.has_arts_strength_points) {
            output << " artsStrengthPoints=" << info.arts_strength_points;
        }
        Log(output.str());
    }
}

void __fastcall BuffBlackboardValueChangeHook(void* instance, void* method) {
    if (g_original_buff_blackboard_value_change) {
        g_original_buff_blackboard_value_change(instance, method);
    }
    RefreshActiveBuffAttributeContributions(instance);
}

void __fastcall BuffModifyAttributesHook(void* instance, void* input_data,
    void* input_blackboard, int32_t enhance_count, void* method) {
    if (g_original_buff_modify_attributes) {
        g_original_buff_modify_attributes(instance, input_data, input_blackboard,
            enhance_count, method);
    }
    RefreshActiveBuffAttributeContributions(instance);
}

void __fastcall RecordBuffFinishHook(
    void* instance, void* buff, int32_t reason, void* method) {
    const Configuration config = ConfigurationSnapshot();
    if (config.stats_enabled && g_buff_diagnostic_contract_ready && buff) {
        const uint64_t inst_id = ReadBuffInstId(buff);
        const uint32_t sample = config.diagnostics
            ? g_rdps_buff_samples.fetch_add(1, std::memory_order_relaxed)
            : 128;
        if (config.diagnostics && sample < 128) {
            Log("[rdps-diag] buff-finish inst=" + std::to_string(inst_id) +
                " reason=" + std::to_string(reason));
        }
        if (inst_id != 0) {
            TrackBuffFinish(inst_id);
            TrackStaggerFinish(inst_id);
            std::scoped_lock lock(g_buff_diagnostic_mutex);
            if (const auto found = g_buff_diagnostic_map.find(inst_id);
                found != g_buff_diagnostic_map.end()) {
                for (void* mod : found->second.damage_modifiers) {
                    if (mod) g_modifier_to_buff_map.erase(mod);
                }
                g_buff_diagnostic_map.erase(found);
            }
        }
    }
    if (g_original_record_buff_finish) {
        g_original_record_buff_finish(instance, buff, reason, method);
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

    LogRdpsDamageDiagnostic(modifier);

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
        event.action_id = FindUniqueOpenActionLocked(
            NormalizeAttackerId(attacker_text), skill_text);
    }

    const Configuration config = ConfigurationSnapshot();
    if (g_rdps_flow_contract_ready) {
        void* pack_copy = EmbeddedValueType(modifier, g_modifier_damage_pack);
        void* server_detail = ReadDamagePackServerDetail(pack_copy);
        CompletedDamageFlow completed;
        const bool matched = TakeCompletedDamageFlow(server_detail, completed);
        if (matched) {
            event.rdps_share_count = completed.rdps_share_count;
            std::copy_n(completed.rdps_shares.begin(), completed.rdps_share_count,
                event.rdps_shares.begin());
        }
        const uint32_t flow_sample = config.diagnostics
            ? g_rdps_flow_samples.fetch_add(1, std::memory_order_relaxed)
            : 384;
        if (config.diagnostics && flow_sample < 384) {
            std::ostringstream output;
            output << std::setprecision(10)
                << "[rdps-flow] sample=" << flow_sample
                << " txn=" << (matched ? completed.transaction : 0)
                << " stage=record packCopy=" << pack_copy
                << " detail=" << server_detail
                << " calculated=" << (matched ? completed.calculated_damage : 0.0)
                << " recorded=" << event.damage
                << " hpDelta=" << event.hp_delta
                << " attacker=\"" << attacker_text << "\""
                << " skill=\"" << skill_text << "\"";
            Log(output.str());
        }
        if (matched && g_damage_flow_context.server_detail == server_detail) {
            g_damage_flow_context.active = false;
        }
    }
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
    PollActionEnds();
}

bool GetCurrentDungeonMetadata(std::string& out_dungeon, std::string& out_mode) {
    if (!g_dungeon_contract_ready) return false;
    void* game_instance = GetGameInstance();
    if (!game_instance) return false;
    void* player = nullptr;
    if (!ReadValue(game_instance, g_game_instance_player, player) || !player) return false;
    void* dungeon_mgr = nullptr;
    if (!ReadValue(player, g_game_player_dungeon_manager, dungeon_mgr) || !dungeon_mgr) return false;
    void* cur_dungeon = nullptr;
    if (ReadValue(dungeon_mgr, g_dungeon_manager_cur_dungeon_id, cur_dungeon) ||
        ReadValue(dungeon_mgr, g_dungeon_manager_cur_dungeon_id_fallback, cur_dungeon)) {
        out_dungeon = CopyString(cur_dungeon);
    }
    void* cur_mode = nullptr;
    if (ReadValue(dungeon_mgr, g_dungeon_manager_cur_mode_id, cur_mode)) {
        out_mode = CopyString(cur_mode);
    }
    return !out_dungeon.empty();
}

void PollAutoDungeonSession(const Configuration& config) {
    if (!config.stats_enabled || !config.auto_dungeon_session) return;
    static std::string last_dungeon_id;
    static bool in_dungeon = false;

    std::string current_dungeon, current_mode;
    const bool has_dungeon = GetCurrentDungeonMetadata(current_dungeon, current_mode);

    if (has_dungeon && !in_dungeon) {
        in_dungeon = true;
        last_dungeon_id = current_dungeon;
        Log("[combat-auto] dungeon entered, starting session: " + current_dungeon);
        StartSession();
        {
            std::scoped_lock lock(g_session_mutex);
            g_session.dungeon_id = current_dungeon;
            g_session.mode_id = current_mode;
        }
    } else if (has_dungeon && in_dungeon) {
        if (current_dungeon != last_dungeon_id) {
            Log("[combat-auto] dungeon changed from " + last_dungeon_id + " to " + current_dungeon);
            StopSession();
            last_dungeon_id = current_dungeon;
            StartSession();
            {
                std::scoped_lock lock(g_session_mutex);
                g_session.dungeon_id = current_dungeon;
                g_session.mode_id = current_mode;
            }
        }
    } else if (!has_dungeon && in_dungeon) {
        in_dungeon = false;
        last_dungeon_id.clear();
        Log("[combat-auto] dungeon exited, stopping session");
        StopSession();
    }
}

void OverlayWorkerLoop() {
    auto next_launch_check = std::chrono::steady_clock::time_point{};
    Log("[combat-overlay] independent publisher started");
    while (!g_overlay_worker_stop.load(std::memory_order_acquire)) {
        if (!g_overlay_runtime_ready.load(std::memory_order_acquire)) {
            std::unique_lock wait_lock(g_overlay_wait_mutex);
            g_overlay_wait_cv.wait_for(wait_lock, std::chrono::milliseconds(100), [] {
                return g_overlay_worker_stop.load(std::memory_order_acquire) ||
                    g_overlay_runtime_ready.load(std::memory_order_acquire);
            });
            continue;
        }
        const Configuration config = ConfigurationSnapshot();
        PollHotkeys();
        PollAutoDungeonSession(config);
        PublishOverlaySnapshot(config);
        const auto now = std::chrono::steady_clock::now();
        if (config.stats_enabled && config.overlay_enabled && now >= next_launch_check) {
            EnsureOverlayProcess();
            next_launch_check = now + std::chrono::seconds(2);
        }
        std::unique_lock wait_lock(g_overlay_wait_mutex);
        g_overlay_wait_cv.wait_for(wait_lock, std::chrono::milliseconds(100), [] {
            return g_overlay_worker_stop.load(std::memory_order_acquire);
        });
    }
    Log("[combat-overlay] independent publisher stopped");
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
    if (config.stats_enabled && !g_cast_skill_hook_installed.load() &&
        g_record_cast_skill.pointer) {
        g_cast_skill_hook_installed.store(Hook(g_record_cast_skill,
            reinterpret_cast<void*>(&RecordCastSkillHook),
            reinterpret_cast<void**>(&g_original_record_cast_skill)));
    }
    if (config.stats_enabled && !g_end_skill_hook_installed.load() &&
        g_record_end_skill.pointer) {
        g_end_skill_hook_installed.store(Hook(g_record_end_skill,
            reinterpret_cast<void*>(&RecordEndSkillHook),
            reinterpret_cast<void**>(&g_original_record_end_skill)));
    }
    if (config.stats_enabled && !g_dodge_success_hook_installed.load() &&
        g_record_dodge_success.pointer) {
        g_dodge_success_hook_installed.store(Hook(g_record_dodge_success,
            reinterpret_cast<void*>(&RecordDodgeSuccessHook),
            reinterpret_cast<void**>(&g_original_record_dodge_success)));
    }
    if (config.stats_enabled && !g_skill_cast_end_hook_installed.load() &&
        g_skill_cast_end.pointer) {
        g_skill_cast_end_hook_installed.store(Hook(g_skill_cast_end,
            reinterpret_cast<void*>(&SkillCastEndHook),
            reinterpret_cast<void**>(&g_original_skill_cast_end)));
    }
    // BattleManager.Tick drives PollActionEnds as the fallback close path for
    // casts whose CastEnd is missed (e.g. skills already active at hook time).
    if (config.stats_enabled && !g_tick_hook_installed.load() &&
        g_battle_tick.pointer) {
        g_tick_hook_installed.store(Hook(g_battle_tick,
            reinterpret_cast<void*>(&BattleTickHook),
            reinterpret_cast<void**>(&g_original_battle_tick)));
    }
    if (config.stats_enabled && config.diagnostics &&
        g_instant_attribute_contract_ready &&
        !g_damage_processor_hook_installed.load() &&
        g_damage_processor_process.pointer) {
        g_damage_processor_hook_installed.store(Hook(g_damage_processor_process,
            reinterpret_cast<void*>(&DamageProcessorProcessHook),
            reinterpret_cast<void**>(&g_original_damage_processor_process)));
    }
    if (config.stats_enabled &&
        g_instant_attribute_contract_ready &&
        !g_instant_attribute_hook_installed.load() &&
        g_instant_attribute_process.pointer) {
        g_instant_attribute_hook_installed.store(Hook(g_instant_attribute_process,
            reinterpret_cast<void*>(&InstantModifyAttributeHook),
            reinterpret_cast<void**>(&g_original_instant_attribute_process)));
    }
    if (config.stats_enabled &&
        g_rdps_flow_contract_ready &&
        !g_apply_damage_modifier_hook_installed.load() &&
        g_apply_damage_modifier.pointer) {
        g_apply_damage_modifier_hook_installed.store(Hook(g_apply_damage_modifier,
            reinterpret_cast<void*>(&ApplyDamageModifierHook),
            reinterpret_cast<void**>(&g_original_apply_damage_modifier)));
    }
    if (config.stats_enabled &&
        g_rdps_flow_contract_ready &&
        !g_calculate_damage_hook_installed.load() &&
        g_calculate_damage.pointer) {
        g_calculate_damage_hook_installed.store(Hook(g_calculate_damage,
            reinterpret_cast<void*>(&CalculateDamageHook),
            reinterpret_cast<void**>(&g_original_calculate_damage)));
    }
    if (config.stats_enabled &&
        g_rdps_value_contract_ready &&
        !g_damage_scale_zone_hook_installed.load() &&
        g_damage_scale_zone_modify.pointer) {
        g_damage_scale_zone_hook_installed.store(Hook(g_damage_scale_zone_modify,
            reinterpret_cast<void*>(&DamageScaleZoneModifyHook),
            reinterpret_cast<void**>(&g_original_damage_scale_zone_modify)));
    }
    if (config.stats_enabled &&
        g_rdps_value_contract_ready &&
        !g_damage_modifier_process_hook_installed.load() &&
        g_damage_modifier_process.pointer) {
        g_damage_modifier_process_hook_installed.store(Hook(g_damage_modifier_process,
            reinterpret_cast<void*>(&DamageModifierProcessHook),
            reinterpret_cast<void**>(&g_original_damage_modifier_process)));
    }
    if (config.stats_enabled &&
        g_buff_diagnostic_contract_ready &&
        !g_buff_start_hook_installed.load() && g_record_buff_start.pointer) {
        g_buff_start_hook_installed.store(Hook(g_record_buff_start,
            reinterpret_cast<void*>(&RecordBuffStartHook),
            reinterpret_cast<void**>(&g_original_record_buff_start)));
    }
    if (config.stats_enabled &&
        g_buff_diagnostic_contract_ready &&
        !g_buff_finish_hook_installed.load() && g_record_buff_finish.pointer) {
        g_buff_finish_hook_installed.store(Hook(g_record_buff_finish,
            reinterpret_cast<void*>(&RecordBuffFinishHook),
            reinterpret_cast<void**>(&g_original_record_buff_finish)));
    }
    if (config.stats_enabled &&
        (g_buff_attribute_contract_ready || g_arts_strength_contract_ready) &&
        !g_buff_blackboard_hook_installed.load() &&
        g_buff_blackboard_value_change.pointer) {
        g_buff_blackboard_hook_installed.store(Hook(g_buff_blackboard_value_change,
            reinterpret_cast<void*>(&BuffBlackboardValueChangeHook),
            reinterpret_cast<void**>(&g_original_buff_blackboard_value_change)));
    }
    if (config.stats_enabled &&
        (g_buff_attribute_contract_ready || g_arts_strength_contract_ready) &&
        !g_buff_modify_attributes_hook_installed.load() &&
        g_buff_modify_attributes.pointer) {
        g_buff_modify_attributes_hook_installed.store(Hook(g_buff_modify_attributes,
            reinterpret_cast<void*>(&BuffModifyAttributesHook),
            reinterpret_cast<void**>(&g_original_buff_modify_attributes)));
    }
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
    // Overlay publication and hotkeys are Windows-only operations and no longer
    // depend on BattleManager.Tick. This keeps diagnostic hooks from affecting
    // the companion lifecycle and also works before a battle manager exists.
    g_hooks_installed.store(g_text_v1_hook_installed || g_text_v2_hook_installed ||
        g_damage_hook_installed ||
        g_record_damage_hook_installed || g_record_detail_hook_installed ||
        g_tick_hook_installed || g_buff_start_hook_installed ||
        g_cast_skill_hook_installed || g_end_skill_hook_installed ||
        g_dodge_success_hook_installed || g_skill_cast_end_hook_installed ||
        g_buff_finish_hook_installed || g_buff_blackboard_hook_installed ||
        g_buff_modify_attributes_hook_installed ||
        g_damage_modifier_process_hook_installed ||
        g_damage_scale_zone_hook_installed ||
        g_apply_damage_modifier_hook_installed ||
        g_calculate_damage_hook_installed ||
        g_instant_attribute_hook_installed ||
        g_damage_processor_hook_installed);
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
    g_buff_start_hook_installed.store(false);
    g_buff_finish_hook_installed.store(false);
    g_cast_skill_hook_installed.store(false);
    g_end_skill_hook_installed.store(false);
    g_dodge_success_hook_installed.store(false);
    g_skill_cast_end_hook_installed.store(false);
    g_buff_blackboard_hook_installed.store(false);
    g_buff_modify_attributes_hook_installed.store(false);
    g_damage_modifier_process_hook_installed.store(false);
    g_damage_scale_zone_hook_installed.store(false);
    g_apply_damage_modifier_hook_installed.store(false);
    g_calculate_damage_hook_installed.store(false);
    g_instant_attribute_hook_installed.store(false);
    g_damage_processor_hook_installed.store(false);
    g_original_damage_init = nullptr;
    g_original_record_damage = nullptr;
    g_original_record_damage_detail = nullptr;
    g_original_damage_text_v1 = nullptr;
    g_original_damage_text_v2 = nullptr;
    g_original_battle_tick = nullptr;
    g_original_record_buff_start = nullptr;
    g_original_record_buff_finish = nullptr;
    g_original_record_cast_skill = nullptr;
    g_original_record_end_skill = nullptr;
    g_original_record_dodge_success = nullptr;
    g_original_skill_cast_end = nullptr;
    g_original_buff_blackboard_value_change = nullptr;
    g_original_buff_modify_attributes = nullptr;
    g_original_damage_modifier_process = nullptr;
    g_original_damage_scale_zone_modify = nullptr;
    g_original_apply_damage_modifier = nullptr;
    g_original_calculate_damage = nullptr;
    g_original_instant_attribute_process = nullptr;
    g_original_damage_processor_process = nullptr;
    {
        std::scoped_lock lock(g_buff_diagnostic_mutex);
        g_buff_diagnostic_map.clear();
    }
}

} // namespace

BE_Result BE_CALL Initialize(const BE_HostApiV1* host) {
    if (!host || host->abi_version != BETTER_ENDFIELD_MODULE_ABI_V1 ||
        !host->resolve_method || !host->resolve_field || !host->create_hook ||
        !host->release_module_hooks || !host->copy_managed_string || !host->log) {
        return BE_Result_ContractMismatch;
    }
    g_host = host;
    LoadCombatSemanticCatalog();
    ResolveRuntimeContract();
    g_worker_stop.store(false);
    g_worker = std::thread(WorkerLoop);
    g_overlay_runtime_ready.store(false, std::memory_order_release);
    g_overlay_worker_stop.store(false, std::memory_order_release);
    g_overlay_worker = std::thread(OverlayWorkerLoop);
    Log("[combat-module] initialized; awaiting configuration");
    return BE_Result_Ok;
}

BE_Result BE_CALL ConfigurationChanged(const char* text) {
    if (!g_host) return BE_Result_NotReady;
    // Do not let the independent publisher observe a half-applied configuration.
    // In particular, launching the companion before InstallHooks completes can bind
    // it to Unity's transient startup window instead of the final game window.
    g_overlay_runtime_ready.store(false, std::memory_order_release);
    Configuration next = ParseConfiguration(text);
    {
        std::scoped_lock lock(g_config_mutex);
        g_configuration = next;
    }
    Log(std::string("[combat-config] enabled=") + (next.enabled ? "true" : "false") +
        " stats=" + (next.stats_enabled ? "true" : "false") +
        " hide=" + (next.hide_damage_numbers ? "true" : "false") +
        " overlay=" + (next.overlay_enabled ? "true" : "false") +
        " metric=" + (next.rdps_display ? "rdps" : "dps") +
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
    g_overlay_runtime_ready.store(true, std::memory_order_release);
    g_overlay_wait_cv.notify_all();
    Log("[combat-overlay] configuration committed; independent publisher released");
    return BE_Result_Ok;
}

void BE_CALL Shutdown() {
    StopSession();
    g_overlay_runtime_ready.store(false, std::memory_order_release);
    g_overlay_worker_stop.store(true, std::memory_order_release);
    g_overlay_wait_cv.notify_all();
    if (g_overlay_worker.joinable()) g_overlay_worker.join();
    ShutdownOverlay();
    StopHooks();
    g_worker_stop.store(true, std::memory_order_release);
    g_queue_cv.notify_all();
    if (g_worker.joinable()) g_worker.join();
    g_state.store(ModuleState::Stopped);
    {
        std::scoped_lock lock(g_semantic_catalog_mutex);
        g_semantic_catalog.reset();
        g_semantic_catalog_path.clear();
    }
    {
        std::scoped_lock lock(g_managed_key_mutex);
        if (g_host && g_host->gchandle_free) {
            for (const auto& [key, value] : g_managed_key_strings) {
                if (value.gc_handle) {
                    g_host->gchandle_free(g_host->context, value.gc_handle);
                }
            }
        }
        g_managed_key_strings.clear();
    }
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
