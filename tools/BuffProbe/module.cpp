// Buff resource table probe.
//
// A local development tool, not part of any release. It dumps every BuffData
// the client can load into one JSON file so the web build can ship the buff
// mechanics as a static table instead of repeating them in every combat record.
//
// Why this has to run inside the game: the 2,872 files under
// Data/Json/BuffData are MemoryPack blobs with a .json suffix, and MemoryPack
// is positional — it stores no field names. Reading them offline means
// reversing the schema first. The client already has a loader that does it, so
// the probe borrows that instead: BattleDataLoader.TryGetBuff.
//
// Getting the loader instance: BaseGameManager exposes no static accessor in
// the 1.4.4 dump, so the probe hooks TryGetBuff purely to capture `this` from
// the first real call, then drives the loader itself.
//
// Triggering it: a polling thread latches the hotkey and BattleManager's
// TailLateTick runs the dump on the game thread. Tick itself is off limits --
// combat_stats owns it and the broker allows one owner per target.
//
// Built out of native/CMakeLists.txt but kept here, next to the script that
// consumes its output; the target is EXCLUDE_FROM_ALL and never ships.
//
//     cmake --build build --config Release --target BetterEndfield.BuffProbe
//     python tools/CombatDataExporter/build_buff_table.py --emit-id-list <modules dir>
//
// Copy BetterEndfield.BuffProbe.dll, its .module.ini and combat-buff-ids.txt
// into the injector's modules directory, enter any battle, press the hotkey,
// then delete the three files again so ordinary injections do not load it.
// Output: %LOCALAPPDATA%\BetterEndfield\combat-buffs.json

#include <BetterEndfield/ModuleApi.h>

#include <Windows.h>

#include <atomic>
#include <cctype>
#include <cmath>
#include <cstdint>
#include <cstdio>
#include <cstdlib>
#include <filesystem>
#include <fstream>
#include <iterator>
#include <string>
#include <string_view>
#include <system_error>
#include <vector>

namespace {

constexpr const char* kModuleId = "betterendfield.buff-probe";
constexpr const char* kAssembly = "Gameplay.Beyond.dll";
constexpr const char* kCore = "Beyond.Gameplay.Core";
constexpr const char* kGameplay = "Beyond.Gameplay";
constexpr const char* kBlackboard = "Beyond.Blackboard";

// IL2CPP object layout constants. List<T> keeps its backing array at 0x10 and
// its size at 0x18; every array keeps its length at 0x18 and its first element
// at 0x20.
constexpr size_t kListItems = 0x10;
constexpr size_t kListSize = 0x18;
constexpr size_t kArrayLength = 0x18;
constexpr size_t kArrayData = 0x20;
// Metadata offsets for fields of a value type include the 16-byte boxed header.
constexpr int32_t kBoxHeader = 16;
constexpr int32_t kSaneCount = 65536;
// BlackboardParamBase<,> layout, used only when the generic base cannot be
// resolved by name. See ResolveBlackboardField for why these are what they are.
constexpr int32_t kBlackboardUseKeyOffset = 0x10;
constexpr int32_t kBlackboardValueOffset = 0x14;
constexpr int32_t kBlackboardKeyOffset = 0x18;

const BE_HostApiV1* g_host = nullptr;

struct RuntimeMethod {
    const char* key = nullptr;
    void* pointer = nullptr;
    const void* method_info = nullptr;
};

struct RuntimeField {
    const char* key = nullptr;
    bool resolved = false;
    int32_t offset = -1;
};

using TryGetBuffFn = bool(__fastcall*)(void* instance, void* buff_id,
    void** out_buff_data, void* method);
using ClearCacheFn = void(__fastcall*)(void* instance, void* method);
using StringGetterFn = void*(__fastcall*)(void* instance, void* method);
using LateTickFn = void(__fastcall*)(void* instance, float delta_time, void* method);

RuntimeMethod g_try_get_buff;
RuntimeMethod g_clear_cache;
RuntimeMethod g_tag_name_getter;
RuntimeMethod g_late_tick;

RuntimeField g_buff_id;
RuntimeField g_buff_icon_config;
RuntimeField g_buff_has_icon;
RuntimeField g_buff_attribute_modifier;
RuntimeField g_buff_damage_modifier;
RuntimeField g_buff_heal_modifier;
RuntimeField g_buff_poise_modifier;
RuntimeField g_buff_global_modifier;
RuntimeField g_buff_shield_configs;
RuntimeField g_buff_apply_tags;
RuntimeField g_buff_life_type;
RuntimeField g_buff_duration;
RuntimeField g_buff_trigger_interval;
RuntimeField g_buff_wait_first_trigger;
RuntimeField g_buff_max_trigger_count;
RuntimeField g_buff_stacking;
RuntimeField g_buff_dispel;

RuntimeField g_icon_sprite_path;

RuntimeField g_attr_data_converted;
RuntimeField g_attr_data_modifiers;
RuntimeField g_attr_modify_type;
RuntimeField g_attr_attribute_type;
RuntimeField g_attr_formula_item;
RuntimeField g_attr_param;

RuntimeField g_bb_use_key;
RuntimeField g_bb_value;
RuntimeField g_bb_key;
RuntimeField g_bbi_use_key;
RuntimeField g_bbi_value;
RuntimeField g_bbi_key;

RuntimeField g_dm_enable_side;
RuntimeField g_dm_processors;
RuntimeField g_scale_side;
RuntimeField g_scale_zone_name;
RuntimeField g_scale_addition;

RuntimeField g_stacking_identifier_type;
RuntimeField g_stacking_type;
RuntimeField g_stacking_key;
RuntimeField g_stacking_use_max_key;
RuntimeField g_stacking_max_key;
RuntimeField g_stacking_max_count;

RuntimeField g_dispel_can_dispel;
RuntimeField g_dispel_level;

const void* g_damage_scale_processor_class = nullptr;

TryGetBuffFn g_original_try_get_buff = nullptr;
LateTickFn g_original_late_tick = nullptr;

std::atomic<void*> g_loader{nullptr};
std::atomic_bool g_hooks_installed{false};
std::atomic_bool g_dump_done{false};
std::atomic_bool g_dump_requested{false};
std::atomic_bool g_thread_stop{false};
HANDLE g_hotkey_thread = nullptr;
int g_hotkey_vk = VK_F9;
bool g_hotkey_ctrl = true;

void Log(const std::string& message) {
    if (g_host && g_host->log) g_host->log(g_host->context, kModuleId, message.c_str());
}

const char* ResultName(BE_Result value) {
    switch (value) {
        case BE_Result_Ok: return "ok";
        case BE_Result_InvalidArgument: return "invalid-argument";
        case BE_Result_NotReady: return "not-ready";
        case BE_Result_NotFound: return "not-found";
        case BE_Result_ContractMismatch: return "contract-mismatch";
        case BE_Result_Conflict: return "conflict";
        default: return "failed";
    }
}

bool ResolveMethod(RuntimeMethod& output, const char* key, const char* namespc,
    const char* klass, const char* method, uint32_t parameter_count,
    const char* return_type) {
    output = {};
    output.key = key;
    BE_MethodDescriptorV1 descriptor{kAssembly, namespc, klass, method, nullptr,
        return_type, parameter_count};
    BE_ResolvedMethodV1 result{};
    const BE_Result status = g_host->resolve_method(g_host->context, &descriptor, &result);
    if (status == BE_Result_Ok && result.method_info && result.method_pointer) {
        output.pointer = result.method_pointer;
        output.method_info = result.method_info;
        return true;
    }
    Log(std::string("[buff-probe] missing method ") + key + " result=" + ResultName(status));
    return false;
}

bool ResolveField(RuntimeField& output, const char* key, const char* namespc,
    const char* klass, const char* field) {
    output = {};
    output.key = key;
    BE_FieldDescriptorV1 descriptor{kAssembly, namespc, klass, field, nullptr};
    BE_ResolvedFieldV1 result{};
    const BE_Result status = g_host->resolve_field(g_host->context, &descriptor, &result);
    output.resolved = status == BE_Result_Ok && result.field_info && result.offset >= 0;
    output.offset = result.offset;
    if (!output.resolved) {
        Log(std::string("[buff-probe] missing field ") + key + " result=" + ResultName(status));
    }
    return output.resolved;
}

/**
 * BlackboardDouble/BlackboardInt declare nothing themselves: useBlackboardKey,
 * value and blackboardKey all live on BlackboardParamBase<,>, and IL2CPP's
 * field-by-name lookup does not walk base classes, so asking for them on the
 * concrete class returns not-found. Try the generic definition, then fall back
 * to the layout both subclasses share.
 *
 * Both are `size: 0x20`, i.e. 16 bytes of fields after the object header, which
 * only fits as bool @0x10 + a 4-byte value @0x14 + a string pointer @0x18. That
 * also settles the value type: BlackboardDouble's TSerializeValue is Single
 * (see its .ctor(System.Single)), not Double — reading 8 bytes there is garbage.
 */
bool ResolveBlackboardField(RuntimeField& output, const char* key, const char* klass,
    const char* field, int32_t fallback_offset) {
    if (ResolveField(output, key, kBlackboard, klass, field)) return true;
    if (ResolveField(output, key, kBlackboard, "BlackboardParamBase`2", field)) {
        Log(std::string("[buff-probe] ") + key + " resolved on the generic base");
        return true;
    }
    output.key = key;
    output.offset = fallback_offset;
    output.resolved = true;
    Log(std::string("[buff-probe] ") + key + " using assumed offset " +
        std::to_string(fallback_offset));
    return true;
}

// ---------------------------------------------------------------------------
// raw reads

template <typename T>
bool ReadAt(void* base, size_t offset, T& value) {
    if (!base) return false;
    __try {
        value = *reinterpret_cast<T*>(reinterpret_cast<uint8_t*>(base) + offset);
        return true;
    } __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

/** Field of a reference type: metadata offsets are absolute from the object. */
template <typename T>
bool ReadField(void* object, const RuntimeField& field, T& value) {
    if (!object || !field.resolved) return false;
    return ReadAt(object, static_cast<size_t>(field.offset), value);
}

/** Address of a value-type field embedded in a reference type. */
void* FieldAddress(void* object, const RuntimeField& field) {
    if (!object || !field.resolved) return nullptr;
    return reinterpret_cast<uint8_t*>(object) + field.offset;
}

/** Field of an unboxed value type: drop the boxed header the metadata counts. */
template <typename T>
bool ReadUnboxed(void* value_type, const RuntimeField& field, T& value) {
    if (!value_type || !field.resolved || field.offset < kBoxHeader) return false;
    return ReadAt(value_type, static_cast<size_t>(field.offset - kBoxHeader), value);
}

std::string CopyString(void* managed) {
    if (!managed || !g_host || !g_host->copy_managed_string) return {};
    char buffer[512]{};
    g_host->copy_managed_string(g_host->context, managed, buffer, sizeof(buffer));
    return buffer;
}

int32_t ListCount(void* list) {
    int32_t size = 0;
    if (!list || !ReadAt(list, kListSize, size)) return 0;
    return size >= 0 && size <= kSaneCount ? size : 0;
}

void* ListElement(void* list, int32_t index) {
    void* items = nullptr;
    if (!list || index < 0 || !ReadAt(list, kListItems, items) || !items) return nullptr;
    uintptr_t length = 0;
    if (!ReadAt(items, kArrayLength, length) ||
        static_cast<uintptr_t>(index) >= length) {
        return nullptr;
    }
    void* element = nullptr;
    return ReadAt(items, kArrayData + static_cast<size_t>(index) * sizeof(void*), element)
        ? element : nullptr;
}

int32_t ArrayLength(void* array) {
    uintptr_t length = 0;
    if (!array || !ReadAt(array, kArrayLength, length)) return 0;
    return length <= static_cast<uintptr_t>(kSaneCount) ? static_cast<int32_t>(length) : 0;
}

void* ArrayObjectAt(void* array, int32_t index) {
    void* element = nullptr;
    return ReadAt(array, kArrayData + static_cast<size_t>(index) * sizeof(void*), element)
        ? element : nullptr;
}

const void* ObjectClass(void* object) {
    const void* klass = nullptr;
    return ReadAt(object, 0, klass) ? klass : nullptr;
}

// SEH cannot live in a function that owns unwindable objects, so every guarded
// call into the runtime goes through one of these.
void* SafeCallStringGetter(const RuntimeMethod& method, void* instance) {
    if (!method.pointer || !instance) return nullptr;
    __try {
        return reinterpret_cast<StringGetterFn>(method.pointer)(
            instance, const_cast<void*>(method.method_info));
    } __except (EXCEPTION_EXECUTE_HANDLER) {
        return nullptr;
    }
}

bool SafeTryGetBuff(void* loader, void* managed_id, void** out_buff_data) {
    __try {
        return g_original_try_get_buff(loader, managed_id, out_buff_data,
            const_cast<void*>(g_try_get_buff.method_info));
    } __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

void SafeClearCache(void* loader) {
    if (!g_clear_cache.pointer) return;
    __try {
        reinterpret_cast<ClearCacheFn>(g_clear_cache.pointer)(
            loader, const_cast<void*>(g_clear_cache.method_info));
    } __except (EXCEPTION_EXECUTE_HANDLER) {
    }
}

// ---------------------------------------------------------------------------
// json

std::string Escape(std::string_view value) {
    std::string out;
    out.reserve(value.size() + 8);
    for (const char raw : value) {
        const unsigned char c = static_cast<unsigned char>(raw);
        switch (c) {
            case '"': out += "\\\""; break;
            case '\\': out += "\\\\"; break;
            case '\n': out += "\\n"; break;
            case '\r': out += "\\r"; break;
            case '\t': out += "\\t"; break;
            default:
                if (c < 0x20) {
                    char buffer[8];
                    std::snprintf(buffer, sizeof(buffer), "\\u%04x", c);
                    out += buffer;
                } else {
                    out += raw;
                }
        }
    }
    return out;
}

std::string Number(double value) {
    if (!std::isfinite(value)) return "null";
    char buffer[64];
    std::snprintf(buffer, sizeof(buffer), "%.9g", value);
    return buffer;
}

std::string Bool(bool value) { return value ? "true" : "false"; }

std::string Quote(std::string_view value) {
    return "\"" + Escape(value) + "\"";
}

/**
 * A BlackboardDouble/BlackboardInt is either a literal or a blackboard key
 * resolved at runtime. Only the literal is static data, so the key case is
 * emitted as {"k":"<key>"} and the consumer treats it as dynamic.
 *
 * The stored value is 4 bytes in both: BlackboardDouble serializes a Single and
 * widens on read, BlackboardInt an Int32. Only the return type is double.
 */
template <typename T>
std::string WriteBlackboard(void* param, const RuntimeField& use_key,
    const RuntimeField& value_field, const RuntimeField& key_field) {
    if (!param) return "null";
    bool uses_key = false;
    if (ReadField(param, use_key, uses_key) && uses_key) {
        void* key = nullptr;
        ReadField(param, key_field, key);
        return "{\"k\":" + Quote(CopyString(key)) + "}";
    }
    T literal{};
    if (!ReadField(param, value_field, literal)) return "null";
    return "{\"v\":" + Number(static_cast<double>(literal)) + "}";
}

std::string WriteBlackboardDouble(void* param, const RuntimeField& use_key,
    const RuntimeField& value_field, const RuntimeField& key_field) {
    return WriteBlackboard<float>(param, use_key, value_field, key_field);
}

std::string WriteTags(void* array) {
    std::string out = "[";
    const int32_t count = ArrayLength(array);
    for (int32_t index = 0; index < count; ++index) {
        if (index) out += ",";
        // GameplayTag is a 4-byte value type, so the elements sit inline.
        void* element = reinterpret_cast<uint8_t*>(array) + kArrayData +
            static_cast<size_t>(index) * sizeof(int32_t);
        int32_t tag_id = 0;
        ReadAt(element, 0, tag_id);
        const std::string name =
            CopyString(SafeCallStringGetter(g_tag_name_getter, element));
        out += "{\"id\":" + std::to_string(tag_id) + ",\"n\":" + Quote(name) + "}";
    }
    return out + "]";
}

std::string WriteAttributeModifiers(void* data) {
    if (!data) return "null";
    bool converted = false;
    ReadField(data, g_attr_data_converted, converted);
    void* array = nullptr;
    ReadField(data, g_attr_data_modifiers, array);
    std::string out = "{\"converted\":" + Bool(converted) + ",\"m\":[";
    const int32_t count = ArrayLength(array);
    for (int32_t index = 0; index < count; ++index) {
        void* modifier = ArrayObjectAt(array, index);
        if (!modifier) continue;
        if (index) out += ",";
        int32_t modify_type = 0, attribute_type = 0, formula_item = 0;
        ReadField(modifier, g_attr_modify_type, modify_type);
        ReadField(modifier, g_attr_attribute_type, attribute_type);
        ReadField(modifier, g_attr_formula_item, formula_item);
        void* param = nullptr;
        ReadField(modifier, g_attr_param, param);
        out += "{\"mt\":" + std::to_string(modify_type) +
            ",\"at\":" + std::to_string(attribute_type) +
            ",\"fi\":" + std::to_string(formula_item) +
            ",\"p\":" + WriteBlackboardDouble(param, g_bb_use_key, g_bb_value, g_bb_key) + "}";
    }
    return out + "]}";
}

/**
 * Only DamageScaleProcessor entries carry a zone name and an addition; the
 * other DamageProcessorBase subclasses have a different layout, so they are
 * counted rather than guessed at.
 */
std::string WriteDamageModifiers(void* list, int32_t& unknown_processors) {
    std::string out = "[";
    const int32_t count = ListCount(list);
    bool first = true;
    for (int32_t index = 0; index < count; ++index) {
        void* data = ListElement(list, index);
        if (!data) continue;
        int32_t enable_side = 0;
        ReadField(data, g_dm_enable_side, enable_side);
        void* processors = nullptr;
        ReadField(data, g_dm_processors, processors);
        std::string zones = "[";
        const int32_t processor_count = ListCount(processors);
        bool first_zone = true;
        for (int32_t slot = 0; slot < processor_count; ++slot) {
            void* processor = ListElement(processors, slot);
            if (!processor) continue;
            if (ObjectClass(processor) != g_damage_scale_processor_class) {
                ++unknown_processors;
                continue;
            }
            int32_t side = 0;
            ReadField(processor, g_scale_side, side);
            void* zone_name = nullptr;
            ReadField(processor, g_scale_zone_name, zone_name);
            void* addition = nullptr;
            ReadField(processor, g_scale_addition, addition);
            if (!first_zone) zones += ",";
            first_zone = false;
            zones += "{\"side\":" + std::to_string(side) +
                ",\"zone\":" + Quote(CopyString(zone_name)) +
                ",\"add\":" +
                WriteBlackboardDouble(addition, g_bb_use_key, g_bb_value, g_bb_key) + "}";
        }
        zones += "]";
        if (!first) out += ",";
        first = false;
        out += "{\"side\":" + std::to_string(enable_side) + ",\"zones\":" + zones + "}";
    }
    return out + "]";
}

std::string WriteStacking(void* buff_data) {
    void* settings = nullptr;
    if (!ReadField(buff_data, g_buff_stacking, settings) || !settings) return "null";
    int16_t identifier_type = 0, stacking_type = 0;
    ReadField(settings, g_stacking_identifier_type, identifier_type);
    ReadField(settings, g_stacking_type, stacking_type);
    void* key = nullptr;
    ReadField(settings, g_stacking_key, key);
    bool use_max_key = false;
    ReadField(settings, g_stacking_use_max_key, use_max_key);
    void* max_key = nullptr;
    ReadField(settings, g_stacking_max_key, max_key);
    int32_t max_count = 0;
    ReadField(settings, g_stacking_max_count, max_count);
    return "{\"idType\":" + std::to_string(identifier_type) +
        ",\"type\":" + std::to_string(stacking_type) +
        ",\"key\":" + Quote(CopyString(key)) +
        ",\"useMaxKey\":" + Bool(use_max_key) +
        ",\"maxKey\":" + Quote(CopyString(max_key)) +
        ",\"maxCount\":" + std::to_string(max_count) + "}";
}

std::string WriteDispel(void* buff_data) {
    void* dispel = FieldAddress(buff_data, g_buff_dispel);
    if (!dispel) return "null";
    bool can_dispel = false;
    int32_t level = 0;
    ReadUnboxed(dispel, g_dispel_can_dispel, can_dispel);
    ReadUnboxed(dispel, g_dispel_level, level);
    return "{\"can\":" + Bool(can_dispel) + ",\"level\":" + std::to_string(level) + "}";
}

std::string WriteBuff(void* buff_data, int32_t& unknown_processors) {
    void* id = nullptr;
    ReadField(buff_data, g_buff_id, id);
    void* icon_config = nullptr;
    ReadField(buff_data, g_buff_icon_config, icon_config);
    void* sprite_path = nullptr;
    if (icon_config) ReadField(icon_config, g_icon_sprite_path, sprite_path);
    bool has_icon = false;
    ReadField(buff_data, g_buff_has_icon, has_icon);
    void* attribute_modifier = nullptr;
    ReadField(buff_data, g_buff_attribute_modifier, attribute_modifier);
    void* damage_modifier = nullptr;
    ReadField(buff_data, g_buff_damage_modifier, damage_modifier);
    void* heal = nullptr;
    void* poise = nullptr;
    void* global = nullptr;
    void* shields = nullptr;
    ReadField(buff_data, g_buff_heal_modifier, heal);
    ReadField(buff_data, g_buff_poise_modifier, poise);
    ReadField(buff_data, g_buff_global_modifier, global);
    ReadField(buff_data, g_buff_shield_configs, shields);
    void* apply_tags = nullptr;
    ReadField(buff_data, g_buff_apply_tags, apply_tags);
    int32_t life_type = 0;
    ReadField(buff_data, g_buff_life_type, life_type);
    void* duration = nullptr;
    void* trigger_interval = nullptr;
    void* max_trigger = nullptr;
    ReadField(buff_data, g_buff_duration, duration);
    ReadField(buff_data, g_buff_trigger_interval, trigger_interval);
    ReadField(buff_data, g_buff_max_trigger_count, max_trigger);
    bool wait_first = false;
    ReadField(buff_data, g_buff_wait_first_trigger, wait_first);

    return "{\"id\":" + Quote(CopyString(id)) +
        ",\"icon\":" + Quote(CopyString(sprite_path)) +
        ",\"hasIcon\":" + Bool(has_icon) +
        ",\"attr\":" + WriteAttributeModifiers(attribute_modifier) +
        ",\"dmg\":" + WriteDamageModifiers(damage_modifier, unknown_processors) +
        ",\"heal\":" + std::to_string(ListCount(heal)) +
        ",\"poise\":" + std::to_string(ListCount(poise)) +
        ",\"global\":" + std::to_string(ListCount(global)) +
        ",\"shield\":" + std::to_string(ListCount(shields)) +
        ",\"tags\":" + WriteTags(apply_tags) +
        ",\"lifeType\":" + std::to_string(life_type) +
        ",\"duration\":" +
            WriteBlackboardDouble(duration, g_bb_use_key, g_bb_value, g_bb_key) +
        ",\"triggerInterval\":" +
            WriteBlackboardDouble(trigger_interval, g_bb_use_key, g_bb_value, g_bb_key) +
        ",\"waitFirstTrigger\":" + Bool(wait_first) +
        ",\"maxTrigger\":" +
            WriteBlackboard<int32_t>(max_trigger, g_bbi_use_key, g_bbi_value, g_bbi_key) +
        ",\"stacking\":" + WriteStacking(buff_data) +
        ",\"dispel\":" + WriteDispel(buff_data) + "}";
}

// ---------------------------------------------------------------------------
// dump

std::filesystem::path ModuleDirectory() {
    HMODULE module = nullptr;
    if (!GetModuleHandleExW(
            GET_MODULE_HANDLE_EX_FLAG_FROM_ADDRESS |
                GET_MODULE_HANDLE_EX_FLAG_UNCHANGED_REFCOUNT,
            reinterpret_cast<LPCWSTR>(&g_host), &module) || !module) {
        return {};
    }
    std::wstring buffer(32768, L'\0');
    const DWORD length = GetModuleFileNameW(module, buffer.data(),
        static_cast<DWORD>(buffer.size()));
    if (!length || length >= buffer.size()) return {};
    buffer.resize(length);
    return std::filesystem::path(buffer).parent_path();
}

std::filesystem::path OutputPath() {
    wchar_t buffer[32768]{};
    const DWORD length = GetEnvironmentVariableW(L"LOCALAPPDATA", buffer,
        static_cast<DWORD>(std::size(buffer)));
    const std::filesystem::path root = length ? std::filesystem::path(buffer) :
        std::filesystem::temp_directory_path();
    return root / L"BetterEndfield" / L"combat-buffs.json";
}

std::vector<std::string> LoadBuffIds() {
    std::vector<std::string> ids;
    const std::filesystem::path path = ModuleDirectory() / L"combat-buff-ids.txt";
    std::ifstream input(path);
    if (!input) {
        Log("[buff-probe] combat-buff-ids.txt not found next to the probe: " +
            path.string());
        return ids;
    }
    std::string line;
    while (std::getline(input, line)) {
        while (!line.empty() && (line.back() == '\r' || line.back() == ' ')) line.pop_back();
        if (!line.empty() && line.front() != '#') ids.push_back(line);
    }
    return ids;
}

bool CallTryGetBuff(void* loader, const std::string& id, void*& out_buff_data) {
    out_buff_data = nullptr;
    void* managed_id = g_host->string_new(g_host->context, id.c_str());
    if (!managed_id) return false;
    return SafeTryGetBuff(loader, managed_id, &out_buff_data);
}

void RunDump() {
    void* loader = g_loader.load(std::memory_order_acquire);
    if (!loader || !g_original_try_get_buff) {
        Log("[buff-probe] no BattleDataLoader instance captured yet; enter a battle first");
        return;
    }
    const std::vector<std::string> ids = LoadBuffIds();
    if (ids.empty()) {
        Log("[buff-probe] no buff ids to dump");
        return;
    }
    const ULONGLONG started = GetTickCount64();
    std::string body;
    body.reserve(ids.size() * 512);
    size_t written = 0;
    std::vector<std::string> missing;
    int32_t unknown_processors = 0;
    for (const std::string& id : ids) {
        void* buff_data = nullptr;
        if (!CallTryGetBuff(loader, id, buff_data) || !buff_data) {
            if (missing.size() < 64) missing.push_back(id);
            continue;
        }
        if (written) body += ",\n";
        body += WriteBuff(buff_data, unknown_processors);
        ++written;
    }

    std::error_code error;
    const std::filesystem::path path = OutputPath();
    std::filesystem::create_directories(path.parent_path(), error);
    std::ofstream output(path, std::ios::binary | std::ios::trunc);
    if (!output) {
        Log("[buff-probe] cannot open " + path.string());
        return;
    }
    output << "{\n  \"schemaVersion\": 1,\n  \"requested\": " << ids.size()
           << ",\n  \"exported\": " << written
           << ",\n  \"missing\": [";
    for (size_t index = 0; index < missing.size(); ++index) {
        if (index) output << ", ";
        output << "\"" << Escape(missing[index]) << "\"";
    }
    output << "],\n  \"buffs\": [\n" << body << "\n  ]\n}\n";
    output.close();

    // 2,872 BuffData objects would otherwise stay resident for the session.
    SafeClearCache(loader);
    Log("[buff-probe] exported " + std::to_string(written) + "/" +
        std::to_string(ids.size()) + " buffs in " +
        std::to_string(GetTickCount64() - started) + " ms, unknownProcessors=" +
        std::to_string(unknown_processors) + " -> " + path.string());
}

/**
 * The key is sampled on its own thread and the request is latched, because the
 * only safe place to *run* the dump is the game thread inside TryGetBuffHook.
 * Sampling there directly missed every press: the hook fires when the game
 * happens to ask for a buff, so a 100 ms press almost never overlaps a call.
 */
DWORD WINAPI HotkeyThread(LPVOID) {
    bool held = false;
    while (!g_thread_stop.load(std::memory_order_acquire)) {
        const bool down = (GetAsyncKeyState(g_hotkey_vk) & 0x8000) != 0 &&
            (!g_hotkey_ctrl || (GetAsyncKeyState(VK_CONTROL) & 0x8000) != 0);
        if (down && !held) {
            held = true;
            if (!g_dump_requested.exchange(true, std::memory_order_acq_rel)) {
                Log("[buff-probe] dump requested; it runs on the next buff lookup");
            }
        } else if (!down) {
            held = false;
        }
        Sleep(50);
    }
    return 0;
}

// ---------------------------------------------------------------------------
// hooks

/** Exists only to capture the loader instance. */
bool __fastcall TryGetBuffHook(void* instance, void* buff_id, void** out_buff_data,
    void* method) {
    if (instance && !g_loader.load(std::memory_order_relaxed)) {
        g_loader.store(instance, std::memory_order_release);
        Log("[buff-probe] BattleDataLoader captured; press the hotkey to dump");
    }
    return g_original_try_get_buff
        ? g_original_try_get_buff(instance, buff_id, out_buff_data, method)
        : false;
}

/**
 * The dump needs the game thread, and this is the only per-frame target free to
 * take: combat_stats owns BattleManager.Tick, and the broker refuses a second
 * owner -- hooking it made the shipped module lose its cast-close fallback.
 * TailLateTick runs once per frame and nothing else hooks it.
 *
 * Running it from the TryGetBuff hook was the other idea and it does not work:
 * the client preloads buff data when a battle starts and then stops asking, so
 * the hook simply never fires again.
 */
void __fastcall LateTickHook(void* instance, float delta_time, void* method) {
    if (g_original_late_tick) g_original_late_tick(instance, delta_time, method);
    if (g_dump_done.load(std::memory_order_relaxed)) return;
    if (!g_dump_requested.load(std::memory_order_acquire)) return;
    g_dump_done.store(true, std::memory_order_release);
    RunDump();
}

bool Hook(const RuntimeMethod& method, void* detour, void** original) {
    if (!method.pointer || !g_host || !g_host->create_hook) return false;
    const BE_Result status = g_host->create_hook(g_host->context, kModuleId,
        method.pointer, detour, original);
    if (status != BE_Result_Ok) {
        Log(std::string("[buff-probe] hook failed ") + method.key +
            " result=" + ResultName(status));
        return false;
    }
    return true;
}

// ---------------------------------------------------------------------------
// lifecycle

bool ResolveContract() {
    bool ok = true;
    ok &= ResolveMethod(g_try_get_buff, "battle-data-loader.try-get-buff", kCore,
        "BattleDataLoader", "TryGetBuff", 2, "System.Boolean");
    ok &= ResolveMethod(g_late_tick, "battle.manager.tail-late-tick", kCore,
        "BattleManager", "TailLateTick", 1, "System.Void");
    ResolveMethod(g_clear_cache, "battle-data-loader.clear-cache", kCore,
        "BattleDataLoader", "ClearCache", 0, "System.Void");
    ResolveMethod(g_tag_name_getter, "gameplay-tag.get-tag-name", kCore,
        "GameplayTag", "get_tagName", 0, "System.String");

    ok &= ResolveField(g_buff_id, "buff-data.id", kCore, "BuffData", "id");
    ResolveField(g_buff_icon_config, "buff-data.icon-config", kCore, "BuffData", "iconConfig");
    ResolveField(g_buff_has_icon, "buff-data.has-icon", kCore, "BuffData", "hasIcon");
    ResolveField(g_buff_attribute_modifier, "buff-data.attribute-modifier", kCore,
        "BuffData", "attributeModifier");
    ResolveField(g_buff_damage_modifier, "buff-data.damage-modifier", kCore,
        "BuffData", "damageModifier");
    ResolveField(g_buff_heal_modifier, "buff-data.heal-modifier", kCore,
        "BuffData", "healModifier");
    ResolveField(g_buff_poise_modifier, "buff-data.poise-modifier", kCore,
        "BuffData", "poiseModifier");
    ResolveField(g_buff_global_modifier, "buff-data.global-modifier", kCore,
        "BuffData", "globalModifier");
    ResolveField(g_buff_shield_configs, "buff-data.shield-configs", kCore,
        "BuffData", "shieldConfigs");
    ResolveField(g_buff_apply_tags, "buff-data.apply-tags", kCore, "BuffData", "applyTags");
    ResolveField(g_buff_life_type, "buff-data.life-type", kCore, "BuffData", "lifeType");
    ResolveField(g_buff_duration, "buff-data.duration", kCore, "BuffData", "duration");
    ResolveField(g_buff_trigger_interval, "buff-data.trigger-interval", kCore,
        "BuffData", "triggerInterval");
    ResolveField(g_buff_wait_first_trigger, "buff-data.wait-first-trigger", kCore,
        "BuffData", "waitFirstTriggerInterval");
    ResolveField(g_buff_max_trigger_count, "buff-data.max-trigger-count", kCore,
        "BuffData", "maxTriggerCnt");
    ResolveField(g_buff_stacking, "buff-data.stacking-settings", kCore,
        "BuffData", "stackingSettings");
    ResolveField(g_buff_dispel, "buff-data.dispel-config", kCore, "BuffData", "dispelConfig");

    ResolveField(g_icon_sprite_path, "buff-icon-config.sprite-path", kCore,
        "BuffIconConfig", "_spritePath");

    ResolveField(g_attr_data_converted, "attribute-modifier-data.converted", kGameplay,
        "AttributeModifierData", "isConvertedAttribute");
    ResolveField(g_attr_data_modifiers, "attribute-modifier-data.modifiers", kGameplay,
        "AttributeModifierData", "attributeModifiers");
    ResolveField(g_attr_modify_type, "attribute-modifier.modify-type", kGameplay,
        "AttributeModifierData.AttributeModifier", "modifyAttributeType");
    ResolveField(g_attr_attribute_type, "attribute-modifier.attribute-type", kGameplay,
        "AttributeModifierData.AttributeModifier", "attributeType");
    ResolveField(g_attr_formula_item, "attribute-modifier.formula-item", kGameplay,
        "AttributeModifierData.AttributeModifier", "formulaItem");
    ResolveField(g_attr_param, "attribute-modifier.param", kGameplay,
        "AttributeModifierData.AttributeModifier", "param");

    ResolveBlackboardField(g_bb_use_key, "blackboard-double.use-key",
        "BlackboardDouble", "useBlackboardKey", kBlackboardUseKeyOffset);
    ResolveBlackboardField(g_bb_value, "blackboard-double.value",
        "BlackboardDouble", "value", kBlackboardValueOffset);
    ResolveBlackboardField(g_bb_key, "blackboard-double.key",
        "BlackboardDouble", "blackboardKey", kBlackboardKeyOffset);
    ResolveBlackboardField(g_bbi_use_key, "blackboard-int.use-key",
        "BlackboardInt", "useBlackboardKey", kBlackboardUseKeyOffset);
    ResolveBlackboardField(g_bbi_value, "blackboard-int.value",
        "BlackboardInt", "value", kBlackboardValueOffset);
    ResolveBlackboardField(g_bbi_key, "blackboard-int.key",
        "BlackboardInt", "blackboardKey", kBlackboardKeyOffset);

    ResolveField(g_dm_enable_side, "damage-modifier-data.enable-side", kCore,
        "DamageModifier.Data", "enableSide");
    ResolveField(g_dm_processors, "damage-modifier-data.processors", kCore,
        "DamageModifier.Data", "damageProcessors");
    ResolveField(g_scale_side, "damage-scale-processor.side", kCore,
        "DamageScaleProcessor", "side");
    ResolveField(g_scale_zone_name, "damage-scale-processor.zone-name", kCore,
        "DamageScaleProcessor", "zoneName");
    ResolveField(g_scale_addition, "damage-scale-processor.addition", kCore,
        "DamageScaleProcessor", "addition");

    ResolveField(g_stacking_identifier_type, "buff-stacking.identifier-type", kCore,
        "BuffStackingSettings", "identifierType");
    ResolveField(g_stacking_type, "buff-stacking.type", kCore,
        "BuffStackingSettings", "stackingType");
    ResolveField(g_stacking_key, "buff-stacking.key", kCore,
        "BuffStackingSettings", "stackingKey");
    ResolveField(g_stacking_use_max_key, "buff-stacking.use-max-key", kCore,
        "BuffStackingSettings", "useMaxStackCntKey");
    ResolveField(g_stacking_max_key, "buff-stacking.max-key", kCore,
        "BuffStackingSettings", "maxStackCntKey");
    ResolveField(g_stacking_max_count, "buff-stacking.max-count", kCore,
        "BuffStackingSettings", "maxStackCnt");

    ResolveField(g_dispel_can_dispel, "dispel-config.can-be-dispelled", kCore,
        "DispelConfig", "canBeDispelled");
    ResolveField(g_dispel_level, "dispel-config.level", kCore,
        "DispelConfig", "dispelledLevel");

    BE_ResolvedClassV1 scale_class{};
    if (g_host->resolve_class(g_host->context, kAssembly, kCore,
            "DamageScaleProcessor", &scale_class) == BE_Result_Ok) {
        g_damage_scale_processor_class = scale_class.class_info;
    } else {
        Log("[buff-probe] DamageScaleProcessor class unavailable; zone writes will be skipped");
    }
    return ok;
}

void ApplyConfiguration(const char* text) {
    if (!text) return;
    std::string_view source(text);
    while (!source.empty()) {
        const size_t newline = source.find('\n');
        std::string line(source.substr(0, newline));
        source = newline == std::string_view::npos ? std::string_view{}
                                                   : source.substr(newline + 1);
        const size_t separator = line.find('=');
        if (separator == std::string::npos) continue;
        std::string key = line.substr(0, separator);
        std::string value = line.substr(separator + 1);
        while (!value.empty() && (value.back() == '\r' || value.back() == ' ')) value.pop_back();
        if (key != "hotkey") continue;
        for (char& c : value) c = static_cast<char>(std::toupper(static_cast<unsigned char>(c)));
        g_hotkey_ctrl = value.rfind("CTRL+", 0) == 0;
        const size_t plus = value.find('+');
        if (plus != std::string::npos) value = value.substr(plus + 1);
        if (value.size() > 1 && value[0] == 'F') {
            const int number = std::atoi(value.c_str() + 1);
            if (number >= 1 && number <= 24) g_hotkey_vk = VK_F1 + number - 1;
        } else if (value.size() == 1) {
            g_hotkey_vk = static_cast<unsigned char>(value[0]);
        }
    }
}

BE_Result BE_CALL Initialize(const BE_HostApiV1* host) {
    g_host = host;
    if (!g_host || !g_host->resolve_method || !g_host->resolve_field ||
        !g_host->create_hook || !g_host->string_new || !g_host->copy_managed_string) {
        return BE_Result_ContractMismatch;
    }
    char configuration[4096]{};
    if (g_host->copy_module_configuration) {
        g_host->copy_module_configuration(g_host->context, kModuleId, configuration,
            sizeof(configuration));
        ApplyConfiguration(configuration);
    }
    if (!ResolveContract()) {
        Log("[buff-probe] required contract unavailable");
        return BE_Result_ContractMismatch;
    }
    const bool hooked =
        Hook(g_try_get_buff, reinterpret_cast<void*>(&TryGetBuffHook),
            reinterpret_cast<void**>(&g_original_try_get_buff)) &&
        Hook(g_late_tick, reinterpret_cast<void*>(&LateTickHook),
            reinterpret_cast<void**>(&g_original_late_tick));
    g_hooks_installed.store(hooked);
    if (!hooked) return BE_Result_ContractMismatch;
    g_hotkey_thread = CreateThread(nullptr, 0, &HotkeyThread, nullptr, 0, nullptr);
    Log("[buff-probe] ready; enter a battle, then press the dump hotkey");
    return BE_Result_Ok;
}

BE_Result BE_CALL ConfigurationChanged(const char* configuration) {
    ApplyConfiguration(configuration);
    return BE_Result_Ok;
}

void BE_CALL Shutdown() {
    g_thread_stop.store(true, std::memory_order_release);
    if (g_hotkey_thread) {
        WaitForSingleObject(g_hotkey_thread, 1000);
        CloseHandle(g_hotkey_thread);
        g_hotkey_thread = nullptr;
    }
    if (g_hooks_installed.load() && g_host && g_host->release_module_hooks) {
        g_host->release_module_hooks(g_host->context, kModuleId);
    }
    g_hooks_installed.store(false);
    g_host = nullptr;
}

const BE_ModuleApiV1 kApi{
    {kModuleId, "Better Endfield Buff Table Probe", "1.0.0",
        BETTER_ENDFIELD_MODULE_ABI_V1},
    &Initialize,
    &ConfigurationChanged,
    &Shutdown,
};

} // namespace

BE_EXPORT const BE_ModuleApiV1* BE_CALL BetterEndfield_GetModuleApiV1(void) {
    return &kApi;
}
