#include <BetterEndfield/ModuleApi.h>

#include <Windows.h>

#include <algorithm>
#include <atomic>
#include <chrono>
#include <cctype>
#include <cmath>
#include <condition_variable>
#include <cstdint>
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
    bool group_by_damage_type = true;
    bool save_raw_events = false;
    bool diagnostics = false;
    double minimum_damage = 0.0;
    int start_vk = VK_F7;
    int stop_vk = VK_F8;
    bool start_ctrl = false;
    bool stop_ctrl = false;
};

struct DamageEvent {
    uint64_t session_id = 0;
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

struct Session {
    uint64_t id = 0;
    bool active = false;
    std::chrono::steady_clock::time_point started{};
    std::chrono::steady_clock::time_point ended{};
    double total_damage = 0.0;
    uint64_t hits = 0;
    uint64_t critical_hits = 0;
    std::map<std::string, Aggregate> characters;
    std::map<std::string, Aggregate> skills;
    std::map<std::string, Aggregate> damage_types;
    std::vector<DamageEvent> raw_events;
};

using DamageInitFn = void(__fastcall*)(void* instance, void* modifier, void* method);
using DamageTextFn = void(__fastcall*)(
    void* instance, void* pack_data, void* blackboard, void* context,
    void* args_for_server, void* method);
using BattleTickFn = void(__fastcall*)(void* instance, float delta_time, void* method);

const BE_HostApiV1* g_host = nullptr;
std::atomic<ModuleState> g_state{ModuleState::Created};
std::atomic_bool g_hooks_installed{false};
std::atomic_bool g_damage_hook_installed{false};
std::atomic_bool g_text_hook_installed{false};
std::atomic_bool g_tick_hook_installed{false};
std::atomic_bool g_session_active{false};
std::atomic_bool g_worker_stop{false};
std::atomic_bool g_save_requested{false};
std::atomic<uint64_t> g_damage_hits{0};
std::atomic<uint64_t> g_damage_dropped{0};
std::atomic<uint64_t> g_text_hits{0};

std::mutex g_config_mutex;
Configuration g_configuration;
std::mutex g_queue_mutex;
std::condition_variable g_queue_cv;
std::deque<DamageEvent> g_queue;
std::thread g_worker;
std::mutex g_session_mutex;
Session g_session;

RuntimeMethod g_damage_init;
RuntimeMethod g_damage_text;
RuntimeMethod g_battle_tick;
RuntimeField g_attacker_id;
RuntimeField g_damage_value;
RuntimeField g_hp_delta;
RuntimeField g_damage_type;
RuntimeField g_damage_decorate_mask;
RuntimeField g_origin_skill_id;
RuntimeField g_is_critical;

DamageInitFn g_original_damage_init = nullptr;
DamageTextFn g_original_damage_text = nullptr;
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
    result.group_by_damage_type = ParseBool(values, "group_by_damage_type", true);
    result.save_raw_events = ParseBool(values, "save_raw_events", false);
    result.diagnostics = ParseBool(values, "diagnostics", false);
    result.minimum_damage = ParseNumber(values, "minimum_damage", 0.0, 0.0, 1.0e12);
    if (const auto found = values.find("hotkey_start"); found != values.end())
        result.start_vk = ParseVirtualKey(found->second, VK_F7, result.start_ctrl);
    if (const auto found = values.find("hotkey_stop"); found != values.end())
        result.stop_vk = ParseVirtualKey(found->second, VK_F8, result.stop_ctrl);
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
    }
    return output.resolved;
}

bool ResolveRuntimeContract() {
    g_state.store(ModuleState::Resolving, std::memory_order_release);
    const bool damage_init = ResolveMethod(g_damage_init, "damage.detail.init",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core.BattleManager",
        "BattleRecorder.DamageDetail", "Init", 1, "System.Void");
    const bool text_process = ResolveMethod(g_damage_text, "damage.text.process",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core", "DamageTextProcessor",
        "ProcessDamagePackDataInternal", 4, "System.Void");
    const bool battle_tick = ResolveMethod(g_battle_tick, "battle.manager.tick",
        "Gameplay.Beyond.dll", "Beyond.Gameplay.Core", "BattleManager", "Tick", 1,
        "System.Void");
    const char* assembly = "Gameplay.Beyond.dll";
    const char* namespc = "Beyond.Gameplay.Core.BattleManager";
    const char* klass = "BattleRecorder.DamageDetail";
    const bool fields =
        ResolveField(g_attacker_id, "damage.attacker", assembly, namespc, klass, "attackerId") &&
        ResolveField(g_damage_value, "damage.value", assembly, namespc, klass, "damageValue") &&
        ResolveField(g_hp_delta, "damage.hp-delta", assembly, namespc, klass, "hpDelta") &&
        ResolveField(g_damage_type, "damage.type", assembly, namespc, klass, "damageType") &&
        ResolveField(g_damage_decorate_mask, "damage.decorate-mask", assembly, namespc, klass,
            "damageDecorateMask") &&
        ResolveField(g_origin_skill_id, "damage.skill", assembly, namespc, klass, "originSkillId") &&
        ResolveField(g_is_critical, "damage.critical", assembly, namespc, klass, "isCritical");
    if (!damage_init || !fields) {
        Log("[combat-contract] damage recording unavailable; module remains isolated");
    }
    if (!text_process) {
        Log("[combat-contract] damage number hiding unavailable; recording can still work");
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
    const auto started = std::chrono::system_clock::now();
    const auto stamp = std::chrono::duration_cast<std::chrono::seconds>(
        started.time_since_epoch()).count();
    const auto path = directory / ("combat-" + std::to_string(stamp) + "-" +
        std::to_string(session.id) + ".json");
    std::ofstream output(path, std::ios::binary | std::ios::trunc);
    if (!output) {
        Log("[combat-save] open result failed");
        return;
    }
    const double duration = std::max(0.001, std::chrono::duration<double>(
        session.ended - session.started).count());
    output << "{\n  \"schemaVersion\":1,\n";
    output << "  \"sessionId\":" << session.id << ",\n";
    output << "  \"durationSeconds\":" << std::setprecision(15) << duration << ",\n";
    output << "  \"totalDamage\":" << session.total_damage << ",\n";
    output << "  \"dps\":" << session.total_damage / duration << ",\n";
    output << "  \"hitCount\":" << session.hits << ",\n";
    output << "  \"criticalCount\":" << session.critical_hits << ",\n";
    output << "  \"characters\":"; WriteAggregateMap(output, session.characters); output << ",\n";
    output << "  \"skills\":"; WriteAggregateMap(output, session.skills); output << ",\n";
    output << "  \"damageTypes\":"; WriteAggregateMap(output, session.damage_types);
    if (!session.raw_events.empty()) {
        output << ",\n  \"events\":[";
        for (size_t index = 0; index < session.raw_events.size(); ++index) {
            if (index) output << ",";
            const auto& event = session.raw_events[index];
            output << "{\"damage\":" << event.damage << ",\"hpDelta\":" << event.hp_delta;
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
    std::scoped_lock lock(g_session_mutex);
    if (event.session_id != g_session.id) return;
    const Configuration config = ConfigurationSnapshot();
    if (!config.record_all_damage && event.attacker[0] == '\0') return;
    double amount = std::abs(event.damage);
    const double health_change = std::abs(event.hp_delta);
    if (!config.include_overkill && health_change > 0.0)
        amount = std::min(amount, health_change);
    if (amount < config.minimum_damage) return;
    g_session.total_damage += amount;
    ++g_session.hits;
    if (event.critical) ++g_session.critical_hits;
    const std::string character = event.attacker[0] ? event.attacker : "<unknown>";
    const std::string skill = event.skill[0] ? event.skill : "<unknown>";
    const std::string type = "type_" + std::to_string(event.damage_type) +
        "_mask_" + std::to_string(event.decorate_mask);
    auto add = [&event, amount](Aggregate& aggregate) {
        aggregate.damage += amount;
        ++aggregate.hits;
        if (event.critical) ++aggregate.critical_hits;
    };
    if (config.group_by_character) add(g_session.characters[character]);
    if (config.group_by_skill) add(g_session.skills[skill]);
    if (config.group_by_damage_type) add(g_session.damage_types[type]);
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
    g_session.started = std::chrono::steady_clock::now();
    Log("[combat-session] started");
}

void StopSession() {
    if (!g_session_active.exchange(false)) return;
    {
        std::scoped_lock lock(g_session_mutex);
        g_session.active = false;
        g_session.ended = std::chrono::steady_clock::now();
    }
    g_save_requested.store(true, std::memory_order_release);
    g_queue_cv.notify_one();
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
    static bool start_previous = false;
    static bool stop_previous = false;
    const Configuration config = ConfigurationSnapshot();
    if (!config.stats_enabled) return;
    if (KeyPressed(config.start_vk, config.start_ctrl, start_previous)) StartSession();
    if (KeyPressed(config.stop_vk, config.stop_ctrl, stop_previous)) StopSession();
}

void __fastcall DamageDetailInitHook(void* instance, void* modifier, void* method) {
    if (g_original_damage_init) g_original_damage_init(instance, modifier, method);
    if (!g_session_active.load(std::memory_order_acquire)) return;
    DamageEvent event{};
    {
        std::scoped_lock lock(g_session_mutex);
        event.session_id = g_session.id;
    }
    ReadValue(instance, g_damage_value, event.damage);
    ReadValue(instance, g_hp_delta, event.hp_delta);
    ReadValue(instance, g_damage_type, event.damage_type);
    int32_t decorate_mask = 0;
    ReadValue(instance, g_damage_decorate_mask, decorate_mask);
    event.decorate_mask = static_cast<uint32_t>(decorate_mask);
    ReadValue(instance, g_is_critical, event.critical);
    void* attacker = nullptr;
    void* skill = nullptr;
    ReadValue(instance, g_attacker_id, attacker);
    ReadValue(instance, g_origin_skill_id, skill);
    const std::string attacker_text = CopyString(attacker);
    const std::string skill_text = CopyString(skill);
    std::snprintf(event.attacker, sizeof(event.attacker), "%s", attacker_text.c_str());
    std::snprintf(event.skill, sizeof(event.skill), "%s", skill_text.c_str());
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

void __fastcall DamageTextHook(void* instance, void* pack_data, void* blackboard,
    void* context, void* args_for_server, void* method) {
    ++g_text_hits;
    const Configuration config = ConfigurationSnapshot();
    if (config.hide_damage_numbers) return;
    if (g_original_damage_text)
        g_original_damage_text(instance, pack_data, blackboard, context, args_for_server, method);
}

void __fastcall BattleTickHook(void* instance, float delta_time, void* method) {
    if (g_original_battle_tick) g_original_battle_tick(instance, delta_time, method);
    PollHotkeys();
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
    if (config.hide_damage_numbers && !g_text_hook_installed.load() && g_damage_text.pointer) {
        g_text_hook_installed.store(Hook(g_damage_text, reinterpret_cast<void*>(&DamageTextHook),
            reinterpret_cast<void**>(&g_original_damage_text)));
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
    g_hooks_installed.store(g_text_hook_installed || g_damage_hook_installed || g_tick_hook_installed);
    g_state.store(g_hooks_installed ? ModuleState::Active : ModuleState::Ready);
}

void StopHooks() {
    if (g_host && g_host->release_module_hooks && g_hooks_installed.load())
        g_host->release_module_hooks(g_host->context, kModuleId);
    g_hooks_installed.store(false);
    g_text_hook_installed.store(false);
    g_damage_hook_installed.store(false);
    g_tick_hook_installed.store(false);
    g_original_damage_init = nullptr;
    g_original_damage_text = nullptr;
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
        " start=" + std::to_string(next.start_vk) + " stop=" + std::to_string(next.stop_vk));
    if (!next.stats_enabled) {
        StopSession();
    }
    if (!next.enabled && !next.hide_damage_numbers) {
        StopHooks();
        g_state.store(ModuleState::Disabled);
    } else {
        InstallHooks(next);
    }
    return BE_Result_Ok;
}

void BE_CALL Shutdown() {
    StopSession();
    StopHooks();
    g_worker_stop.store(true, std::memory_order_release);
    g_queue_cv.notify_all();
    if (g_worker.joinable()) g_worker.join();
    g_state.store(ModuleState::Stopped);
    g_host = nullptr;
}

const BE_ModuleApiV1 kApi{
    {kModuleId, "Combat Statistics", "2.1.1", BETTER_ENDFIELD_MODULE_ABI_V1},
    &Initialize,
    &ConfigurationChanged,
    &Shutdown};

} // namespace BetterEndfield::CombatStats

BE_EXPORT const BE_ModuleApiV1* BE_CALL BetterEndfield_GetModuleApiV1() {
    return &BetterEndfield::CombatStats::kApi;
}
