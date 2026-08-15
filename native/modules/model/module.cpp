#include "BetterEndfield/ModuleApi.h"

#include <Windows.h>

#include <algorithm>
#include <array>
#include <atomic>
#include <cctype>
#include <cerrno>
#include <cmath>
#include <cstdint>
#include <cstdio>
#include <cstdlib>
#include <cstring>
#include <mutex>
#include <string>
#include <string_view>
#include <vector>

namespace BetterEndfield::Model {
namespace {

constexpr char kModuleId[] = "betterendfield.model";
constexpr uint8_t kMainCategory = 0;
constexpr size_t kClipCount = 4;
constexpr float kVerticalFollowResponseSeconds = 0.45f;
constexpr uint64_t kAnimationGateTimeoutMs = 5000;
constexpr int kUnscaledGameTime = 2;
constexpr int kWrapHold = 0;
constexpr int kWrapLoop = 1;

enum class ModuleState : uint8_t {
    Created,
    Resolving,
    Ready,
    Active,
    Disabled,
    ContractMismatch,
    Failed,
    Stopped,
};

enum class SequencePhase : uint8_t {
    SitLoop = 0,
    SitSpecial = 1,
    SitToWalk = 2,
    Final = 3,
    None = 0xff,
};

struct Vector3 {
    float x;
    float y;
    float z;
};

struct Quaternion {
    float x;
    float y;
    float z;
    float w;
};

struct ClipConfiguration {
    std::string path;
    std::string label;
    uint64_t path_hash = 0;
    float speed = 1.0f;
    bool loop = false;
};

struct ModelConfiguration {
    bool enabled = false;
    bool diagnostics = true;
    std::string character_id;
    std::string model_path;
    uint64_t model_path_hash = 0;
    float scale = 1.0f;
    float start_yaw = -120.0f;
    float turn_duration = 3.0333335f;
    float forward_lean_sample = 1.0f;
    bool final_native_loop = false;
    bool final_loop = true;
    bool force_loop = false;
    bool use_crossfade = false;
    float loop_start = 0.968f;
    float loop_end = 2.3760002f;
    float crossfade_duration = 0.20f;
    std::array<ClipConfiguration, kClipCount> clips{};
};

struct RuntimeMethod {
    const char* key = nullptr;
    void* pointer = nullptr;
    const void* method_info = nullptr;
};

struct RuntimeMethods {
    RuntimeMethod login_bind;
    RuntimeMethod init_main_hash;
    RuntimeMethod init_initial_hash;
    RuntimeMethod anim_tick;
    RuntimeMethod anim_release;
    RuntimeMethod anim_change_state;
    RuntimeMethod anim_reset_a1;
    RuntimeMethod anim_play_special;
    RuntimeMethod anim_play_transition;
    RuntimeMethod clone_with_parent;
    RuntimeMethod object_name;
    RuntimeMethod object_destroy;
    RuntimeMethod game_object_transform;
    RuntimeMethod game_object_set_active;
    RuntimeMethod game_object_get_layer;
    RuntimeMethod game_object_set_layer_recursive;
    RuntimeMethod game_object_get_animator;
    RuntimeMethod game_object_get_components;
    RuntimeMethod game_object_find;
    RuntimeMethod array_get_length;
    RuntimeMethod array_get_value;
    RuntimeMethod renderer_set_enabled;
    RuntimeMethod transform_parent;
    RuntimeMethod transform_find_name;
    RuntimeMethod transform_position;
    RuntimeMethod transform_set_position;
    RuntimeMethod transform_local_position;
    RuntimeMethod transform_set_local_position;
    RuntimeMethod transform_local_rotation;
    RuntimeMethod transform_set_local_rotation;
    RuntimeMethod transform_local_scale;
    RuntimeMethod transform_set_local_scale;
    RuntimeMethod behaviour_set_enabled;
    RuntimeMethod animator_rebind;
    RuntimeMethod animator_culling;
    RuntimeMethod animator_update;
    RuntimeMethod animator_initialized;
    RuntimeMethod animator_human;
    RuntimeMethod animator_avatar;
    RuntimeMethod clip_length;
    RuntimeMethod clip_empty;
    RuntimeMethod clip_human_motion;
    RuntimeMethod i18n_load;
    RuntimeMethod asset_handle_get;
    RuntimeMethod resource_initialized;
    RuntimeMethod resource_async_initing;
    RuntimeMethod playable_graph_create;
    RuntimeMethod playable_graph_destroy;
    RuntimeMethod playable_graph_play;
    RuntimeMethod playable_graph_evaluate;
    RuntimeMethod playable_graph_set_update_mode;
    RuntimeMethod clip_playable_create;
    RuntimeMethod clip_playable_override_loop;
    RuntimeMethod clip_playable_set_loop;
    RuntimeMethod animation_output_create;
    RuntimeMethod animation_output_set_target;
    RuntimeMethod playable_output_set_source;
    RuntimeMethod playable_set_wrap_mode;
    RuntimeMethod playable_set_speed;
    RuntimeMethod playable_set_time;
    RuntimeMethod playable_get_time;
    RuntimeMethod mixer_playable_create;
    RuntimeMethod playable_set_input_count;
    RuntimeMethod playable_connect;
    RuntimeMethod playable_set_input_weight;
};

struct RuntimeClasses {
    BE_ResolvedClassV1 game_object{};
    BE_ResolvedClassV1 animator{};
    BE_ResolvedClassV1 animation_clip{};
    BE_ResolvedClassV1 renderer{};
};

struct PlayableData {
    void* handle = nullptr;
    uint32_t version = 0;
    uint32_t padding = 0;
};

struct LiveActor {
    void* original = nullptr;
    void* parent = nullptr;
    void* controller = nullptr;
    void* replacement = nullptr;
    void* original_transform = nullptr;
    void* replacement_transform = nullptr;
    void* original_anchor = nullptr;
    void* replacement_anchor = nullptr;
    void* animator = nullptr;
    PlayableData graph{};
    PlayableData playable{};
    PlayableData mixer{};
    std::array<PlayableData, 2> mixer_playables{};
    std::vector<void*> disabled_renderers;
    int mixer_active_input = 0;
    uint32_t mixer_blend_count = 0;
    bool mixer_blend_active = false;
    bool mixer_configured = false;
    bool mixer_fault_logged = false;
    bool original_renderers_hidden = false;
    bool replacement_attempted = false;
    bool gate_requested = false;
    bool tick_held = false;
    bool gate_timed_out = false;
    bool anchor_alignment_logged = false;
    uint64_t gate_start_tick = 0;
    SequencePhase phase = SequencePhase::None;
    SequencePhase pending = SequencePhase::None;
    SequencePhase last_requested = SequencePhase::None;
    float phase_length = 0.0f;
    double phase_elapsed = 0.0;
};

const BE_HostApiV1* g_host = nullptr;
RuntimeMethods g_methods;
RuntimeClasses g_classes;
std::atomic<ModuleState> g_state{ModuleState::Created};
std::atomic_bool g_diagnostics{true};
std::atomic_bool g_main_hash_ready{false};
std::atomic_bool g_initial_hash_ready{false};
std::atomic_bool g_cleanup_requested{false};
std::atomic_uint64_t g_configuration_revision{0};
std::atomic_uint64_t g_loaded_revision{0};
std::atomic<SequencePhase> g_requested_phase{SequencePhase::SitLoop};
std::mutex g_configuration_mutex;
ModelConfiguration g_configuration;
LiveActor g_actor;
void* g_model_prefab = nullptr;
uint32_t g_model_prefab_root = 0;
std::array<void*, kClipCount> g_clips{};
std::array<uint32_t, kClipCount> g_clip_roots{};
uint64_t g_next_asset_retry_tick = 0;
uint64_t g_next_actor_discovery_tick = 0;
bool g_assets_ready = false;
bool g_hooks_installed = false;

using VoidInstanceFn = void(__fastcall*)(void*, void*);
using VoidStaticFn = void(__fastcall*)(void*);
using TickFn = void(__fastcall*)(void*, float, void*);
using ChangeStateFn = void(__fastcall*)(void*, int, void*);
using CloneWithParentFn = void*(__fastcall*)(void*, void*, bool, void*);
using AssetGetFn = void*(__fastcall*)(void*, void*);
using PlayableGraphCreateFn = void(__fastcall*)(
    void*, PlayableData*, void*);
using ClipPlayableCreateFn = bool(__fastcall*)(
    PlayableData*, void*, PlayableData*, void*);
using PlayableBoolFn = void(__fastcall*)(PlayableData*, bool, void*);
using AnimationOutputCreateFn = bool(__fastcall*)(
    PlayableData*, void*, PlayableData*, void*);
using AnimationOutputSetTargetFn = void(__fastcall*)(
    PlayableData*, void*, void*);
using PlayableOutputSetSourceFn = void(__fastcall*)(
    PlayableData*, PlayableData*, int, void*);
using PlayableIntFn = void(__fastcall*)(PlayableData*, int, void*);
using PlayableDoubleFn = void(__fastcall*)(PlayableData*, double, void*);
using PlayableGetDoubleFn = double(__fastcall*)(PlayableData*, void*);
using PlayableGraphIntFn = void(__fastcall*)(PlayableData*, int, void*);
using PlayableGraphActionFn = void(__fastcall*)(PlayableData*, void*);
using PlayableGraphEvaluateFn = void(__fastcall*)(PlayableData*, float, void*);
using MixerPlayableCreateFn = bool(__fastcall*)(
    PlayableData*, PlayableData*, void*);
using PlayableConnectFn = bool(__fastcall*)(PlayableData*, PlayableData*, int,
    PlayableData*, int, void*);
using PlayableWeightFn = void(__fastcall*)(PlayableData*, int, float, void*);

VoidInstanceFn g_original_login_bind = nullptr;
VoidStaticFn g_original_init_main_hash = nullptr;
VoidStaticFn g_original_init_initial_hash = nullptr;
TickFn g_original_anim_tick = nullptr;
VoidInstanceFn g_original_anim_release = nullptr;
ChangeStateFn g_original_anim_change_state = nullptr;
VoidInstanceFn g_original_anim_reset_a1 = nullptr;
VoidInstanceFn g_original_anim_play_special = nullptr;
VoidInstanceFn g_original_anim_play_transition = nullptr;
CloneWithParentFn g_original_clone_with_parent = nullptr;

std::string_view Trim(std::string_view value) {
    while (!value.empty() &&
        std::isspace(static_cast<unsigned char>(value.front()))) {
        value.remove_prefix(1);
    }
    while (!value.empty() &&
        std::isspace(static_cast<unsigned char>(value.back()))) {
        value.remove_suffix(1);
    }
    return value;
}

std::string Lower(std::string_view value) {
    std::string output(Trim(value));
    std::transform(output.begin(), output.end(), output.begin(),
        [](unsigned char item) { return static_cast<char>(std::tolower(item)); });
    return output;
}

bool ParseBoolean(std::string_view value, bool& output) {
    const std::string normalized = Lower(value);
    if (normalized == "1" || normalized == "true" || normalized == "yes" ||
        normalized == "on") {
        output = true;
        return true;
    }
    if (normalized == "0" || normalized == "false" || normalized == "no" ||
        normalized == "off") {
        output = false;
        return true;
    }
    return false;
}

bool ParseFloat(std::string_view value, float& output) {
    const std::string copy(Trim(value));
    char* end = nullptr;
    errno = 0;
    const float result = std::strtof(copy.c_str(), &end);
    if (copy.empty() || end == copy.c_str() || *end != '\0' || errno == ERANGE ||
        !std::isfinite(result)) {
        return false;
    }
    output = result;
    return true;
}

bool ParseUInt64(std::string_view value, uint64_t& output) {
    const std::string copy(Trim(value));
    char* end = nullptr;
    errno = 0;
    const unsigned long long result = std::strtoull(copy.c_str(), &end, 0);
    if (copy.empty() || end == copy.c_str() || *end != '\0' || errno == ERANGE) {
        return false;
    }
    output = static_cast<uint64_t>(result);
    return true;
}

bool ParseConfiguration(const char* text, ModelConfiguration& output,
    std::string& error) {
    output = {};
    output.clips[0].loop = true;
    output.clips[3].loop = true;
    if (!text || *text == '\0') {
        return true;
    }

    std::string_view remaining(text);
    while (!remaining.empty()) {
        const size_t line_end = remaining.find_first_of("\r\n");
        std::string_view line = remaining.substr(0, line_end);
        remaining = line_end == std::string_view::npos
            ? std::string_view{}
            : remaining.substr(line_end + 1);
        line = Trim(line);
        if (line.empty() || line.front() == ';' || line.front() == '#') {
            continue;
        }
        const size_t separator = line.find('=');
        if (separator == std::string_view::npos) {
            error = "model configuration line has no '='";
            return false;
        }
        const std::string key = Lower(line.substr(0, separator));
        const std::string_view value = Trim(line.substr(separator + 1));
        if (key == "enabled" || key == "model_enabled" ||
            key == "model_replacement_enabled") {
            if (!ParseBoolean(value, output.enabled)) {
                error = "enabled must be a boolean";
                return false;
            }
        } else if (key == "diagnostics") {
            if (!ParseBoolean(value, output.diagnostics)) {
                error = "diagnostics must be a boolean";
                return false;
            }
        } else if (key == "character" || key == "character_id") {
            output.character_id.assign(value);
        } else if (key == "model_path") {
            output.model_path.assign(value);
        } else if (key == "model_path_hash") {
            if (!ParseUInt64(value, output.model_path_hash)) {
                error = "model_path_hash must be an integer";
                return false;
            }
        } else if (key == "scale") {
            if (!ParseFloat(value, output.scale) || output.scale <= 0.0f ||
                output.scale > 10.0f) {
                error = "scale must be in (0, 10]";
                return false;
            }
        } else if (key == "start_yaw") {
            if (!ParseFloat(value, output.start_yaw) ||
                output.start_yaw < -360.0f || output.start_yaw > 360.0f) {
                error = "start_yaw must be in [-360, 360]";
                return false;
            }
        } else if (key == "turn_duration") {
            if (!ParseFloat(value, output.turn_duration) ||
                output.turn_duration < 0.10f || output.turn_duration > 15.0f) {
                error = "turn_duration must be in [0.10, 15]";
                return false;
            }
        } else if (key == "forward_lean_sample") {
            if (!ParseFloat(value, output.forward_lean_sample) ||
                output.forward_lean_sample < 0.0f ||
                output.forward_lean_sample > 5.0f) {
                error = "forward_lean_sample must be in [0, 5]";
                return false;
            }
        } else if (key == "final_native_loop") {
            if (!ParseBoolean(value, output.final_native_loop)) {
                error = "final_native_loop must be a boolean";
                return false;
            }
        } else if (key == "final_loop") {
            if (!ParseBoolean(value, output.final_loop)) {
                error = "final_loop must be a boolean";
                return false;
            }
        } else if (key == "force_loop") {
            if (!ParseBoolean(value, output.force_loop)) {
                error = "force_loop must be a boolean";
                return false;
            }
        } else if (key == "use_crossfade") {
            if (!ParseBoolean(value, output.use_crossfade)) {
                error = "use_crossfade must be a boolean";
                return false;
            }
        } else if (key == "loop_start") {
            if (!ParseFloat(value, output.loop_start) ||
                output.loop_start < 0.0f || output.loop_start > 30.0f) {
                error = "loop_start must be in [0, 30]";
                return false;
            }
        } else if (key == "loop_end") {
            if (!ParseFloat(value, output.loop_end) ||
                output.loop_end < 0.05f || output.loop_end > 60.0f) {
                error = "loop_end must be in [0.05, 60]";
                return false;
            }
        } else if (key == "crossfade_duration") {
            if (!ParseFloat(value, output.crossfade_duration) ||
                output.crossfade_duration < 0.01f ||
                output.crossfade_duration > 30.0f) {
                error = "crossfade_duration must be in [0.01, 30]";
                return false;
            }
        } else {
            constexpr std::array<const char*, kClipCount> prefixes{
                "sit_loop_", "sit_special_", "sit_to_walk_", "final_"};
            for (size_t index = 0; index < prefixes.size(); ++index) {
                const std::string prefix(prefixes[index]);
                if (!key.starts_with(prefix)) {
                    continue;
                }
                const std::string suffix = key.substr(prefix.size());
                if (suffix == "path") {
                    output.clips[index].path.assign(value);
                } else if (suffix == "path_hash") {
                    if (!ParseUInt64(value, output.clips[index].path_hash)) {
                        error = key + " must be an integer";
                        return false;
                    }
                } else if (suffix == "label") {
                    output.clips[index].label.assign(value);
                } else if (suffix == "speed") {
                    if (!ParseFloat(value, output.clips[index].speed) ||
                        output.clips[index].speed <= 0.0f ||
                        output.clips[index].speed > 10.0f) {
                        error = key + " must be in (0, 10]";
                        return false;
                    }
                }
                break;
            }
        }
    }

    if (output.loop_end < output.loop_start + 0.05f) {
        error = "loop_end must be at least 0.05 seconds after loop_start";
        return false;
    }
    const float maximum_crossfade =
        (output.loop_end - output.loop_start) * 0.5f;
    if (output.crossfade_duration > maximum_crossfade) {
        output.crossfade_duration = maximum_crossfade;
    }
    output.clips[3].loop = output.final_loop;
    if (output.enabled) {
        if (output.model_path_hash == 0 || output.model_path.empty()) {
            error = "enabled model replacement requires model_path and model_path_hash";
            return false;
        }
        for (size_t index = 0; index < output.clips.size(); ++index) {
            if (output.clips[index].path_hash == 0 || output.clips[index].path.empty()) {
                error = "enabled model replacement requires all four clip paths and hashes";
                return false;
            }
            if (output.clips[index].label.empty()) {
                output.clips[index].label = output.clips[index].path;
            }
        }
    }
    return true;
}

const char* StateName(ModuleState state) {
    switch (state) {
    case ModuleState::Created: return "created";
    case ModuleState::Resolving: return "resolving";
    case ModuleState::Ready: return "ready";
    case ModuleState::Active: return "active";
    case ModuleState::Disabled: return "disabled";
    case ModuleState::ContractMismatch: return "contract-mismatch";
    case ModuleState::Failed: return "failed";
    case ModuleState::Stopped: return "stopped";
    }
    return "unknown";
}

const char* ResultName(BE_Result result) {
    switch (result) {
    case BE_Result_Ok: return "ok";
    case BE_Result_InvalidArgument: return "invalid-argument";
    case BE_Result_NotReady: return "not-ready";
    case BE_Result_NotFound: return "not-found";
    case BE_Result_ContractMismatch: return "contract-mismatch";
    case BE_Result_Conflict: return "conflict";
    case BE_Result_Failed: return "failed";
    }
    return "unknown";
}

const char* PhaseName(SequencePhase phase) {
    switch (phase) {
    case SequencePhase::SitLoop: return "sit_loop";
    case SequencePhase::SitSpecial: return "sit_special";
    case SequencePhase::SitToWalk: return "sit_to_walk";
    case SequencePhase::Final: return "final";
    case SequencePhase::None: return "none";
    }
    return "unknown";
}

void Log(const std::string& message) {
    if (g_host && g_host->log) {
        g_host->log(g_host->context, kModuleId, message.c_str());
    }
}

void LogState(ModuleState state, const char* reason) {
    std::string message = "[model-state] state=";
    message += StateName(state);
    if (reason && *reason) {
        message += " reason=";
        message += reason;
    }
    Log(message);
}

bool Resolve(RuntimeMethod& output, const char* key, const char* assembly,
    const char* namespc, const char* klass, const char* method,
    const char* parameters, const char* return_type, uint32_t parameter_count) {
    output = {};
    output.key = key;
    const BE_MethodDescriptorV1 descriptor{
        assembly, namespc, klass, method, parameters, return_type, parameter_count};
    BE_ResolvedMethodV1 result{};
    const BE_Result status =
        g_host->resolve_method(g_host->context, &descriptor, &result);
    if (status == BE_Result_Ok && result.method_pointer && result.method_info) {
        output.pointer = result.method_pointer;
        output.method_info = result.method_info;
        Log(std::string("[model-contract] resolved key=") + key);
        return true;
    }
    Log(std::string("[model-contract] missing key=") + key +
        " result=" + ResultName(status));
    return false;
}

bool ResolveClass(BE_ResolvedClassV1& output, const char* assembly,
    const char* namespc, const char* klass, bool require_type_object) {
    const BE_Result status = g_host->resolve_class(g_host->context, assembly,
        namespc, klass, &output);
    const bool resolved = status == BE_Result_Ok && output.class_info &&
        (!require_type_object || output.type_object);
    Log(std::string("[model-contract] class=") + namespc + "." + klass +
        " result=" + (resolved ? "ok" : ResultName(status)));
    return resolved;
}

bool ResolveRuntimeContract() {
    g_state.store(ModuleState::Resolving, std::memory_order_release);
    size_t missing = 0;
    auto required = [&missing](bool result) {
        if (!result) {
            ++missing;
        }
    };

    required(Resolve(g_methods.login_bind, "login.scene_root.bind",
        "Entry.Beyond.dll", "Beyond.Login", "LoginSceneRoot", "OnBindToManager",
        nullptr, "System.Void", 0));
    required(Resolve(g_methods.init_main_hash, "resource.main_hash",
        "Common.Beyond.dll", "Beyond.Resource", "HashStringPathProcessor",
        "InitMainPathHash", nullptr, "System.Void", 0));
    required(Resolve(g_methods.init_initial_hash, "resource.initial_hash",
        "Common.Beyond.dll", "Beyond.Resource", "HashStringPathProcessor",
        "InitInitPathHash", nullptr, "System.Void", 0));
    required(Resolve(g_methods.anim_tick, "login.animation.tick",
        "Entry.Beyond.dll", "Beyond.Login", "LoginSceneAnimCtrl", "Tick",
        "System.Single", "System.Void", 1));
    required(Resolve(g_methods.anim_release, "login.animation.release",
        "Entry.Beyond.dll", "Beyond.Login", "LoginSceneAnimCtrl", "OnRelease",
        nullptr, "System.Void", 0));
    required(Resolve(g_methods.anim_change_state, "login.animation.change_state",
        "Entry.Beyond.dll", "Beyond.Login", "LoginSceneAnimCtrl", "_ChangeToState",
        nullptr, "System.Void", 1));
    required(Resolve(g_methods.anim_reset_a1, "login.animation.reset_a1",
        "Entry.Beyond.dll", "Beyond.Login", "LoginSceneAnimCtrl", "_ResetToA1",
        nullptr, "System.Void", 0));
    required(Resolve(g_methods.anim_play_special, "login.animation.special",
        "Entry.Beyond.dll", "Beyond.Login", "LoginSceneAnimCtrl",
        "_PlayA1sAndTriggerNext", nullptr, "System.Void", 0));
    required(Resolve(g_methods.anim_play_transition, "login.animation.transition",
        "Entry.Beyond.dll", "Beyond.Login", "LoginSceneAnimCtrl", "_PlayA1A2Impl",
        nullptr, "System.Void", 0));
    required(Resolve(g_methods.clone_with_parent, "unity.object.clone_with_parent",
        "UnityEngine.CoreModule.dll", "UnityEngine", "Object",
        "Internal_CloneSingleWithParent",
        "UnityEngine.Object|UnityEngine.Transform|System.Boolean",
        "UnityEngine.Object", 3));
    required(Resolve(g_methods.object_name, "unity.object.name",
        "UnityEngine.CoreModule.dll", "UnityEngine", "Object", "get_name",
        nullptr, "System.String", 0));
    required(Resolve(g_methods.object_destroy, "unity.object.destroy",
        "UnityEngine.CoreModule.dll", "UnityEngine", "Object", "Destroy",
        "UnityEngine.Object", "System.Void", 1));
    required(Resolve(g_methods.game_object_transform, "unity.game_object.transform",
        "UnityEngine.CoreModule.dll", "UnityEngine", "GameObject", "get_transform",
        nullptr, "UnityEngine.Transform", 0));
    required(Resolve(g_methods.game_object_set_active, "unity.game_object.active",
        "UnityEngine.CoreModule.dll", "UnityEngine", "GameObject", "SetActive",
        "System.Boolean", "System.Void", 1));
    required(Resolve(g_methods.game_object_get_layer, "unity.game_object.layer",
        "UnityEngine.CoreModule.dll", "UnityEngine", "GameObject", "get_layer",
        nullptr, "System.Int32", 0));
    required(Resolve(g_methods.game_object_set_layer_recursive,
        "unity.game_object.layer_recursive", "UnityEngine.CoreModule.dll",
        "UnityEngine", "GameObject", "SetLayerRecursive", "System.Int32",
        "System.Boolean", 1));
    required(Resolve(g_methods.game_object_get_animator,
        "unity.game_object.animator_child", "UnityEngine.CoreModule.dll",
        "UnityEngine", "GameObject", "GetComponentInChildren",
        "System.Type|System.Boolean", "UnityEngine.Component", 2));
    required(Resolve(g_methods.game_object_get_components,
        "unity.game_object.components_children", "UnityEngine.CoreModule.dll",
        "UnityEngine", "GameObject", "GetComponentsInChildren",
        "System.Type|System.Boolean", "UnityEngine.Component[]", 2));
    required(Resolve(g_methods.game_object_find, "unity.game_object.find",
        "UnityEngine.CoreModule.dll", "UnityEngine", "GameObject", "Find",
        "System.String", "UnityEngine.GameObject", 1));
    required(Resolve(g_methods.array_get_length, "system.array.length",
        "mscorlib.dll", "System", "Array", "GetLength", "System.Int32",
        "System.Int32", 1));
    required(Resolve(g_methods.array_get_value, "system.array.value",
        "mscorlib.dll", "System", "Array", "GetValue", "System.Int32",
        "System.Object", 1));
    required(Resolve(g_methods.renderer_set_enabled, "unity.renderer.enabled",
        "UnityEngine.CoreModule.dll", "UnityEngine", "Renderer", "set_enabled",
        "System.Boolean", "System.Void", 1));
    required(Resolve(g_methods.transform_parent, "unity.transform.parent",
        "UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "get_parent",
        nullptr, "UnityEngine.Transform", 0));
    required(Resolve(g_methods.transform_find_name,
        "unity.transform.find_name", "UnityEngine.CoreModule.dll",
        "UnityEngine", "Transform", "FindTransformWithName",
        "UnityEngine.Transform|System.String", "UnityEngine.Transform", 2));
    required(Resolve(g_methods.transform_position,
        "unity.transform.position", "UnityEngine.CoreModule.dll",
        "UnityEngine", "Transform", "get_position", nullptr,
        "UnityEngine.Vector3", 0));
    required(Resolve(g_methods.transform_set_position,
        "unity.transform.set_position", "UnityEngine.CoreModule.dll",
        "UnityEngine", "Transform", "set_position", "UnityEngine.Vector3",
        "System.Void", 1));
    required(Resolve(g_methods.transform_local_position,
        "unity.transform.local_position", "UnityEngine.CoreModule.dll",
        "UnityEngine", "Transform", "get_localPosition", nullptr,
        "UnityEngine.Vector3", 0));
    required(Resolve(g_methods.transform_set_local_position,
        "unity.transform.set_local_position", "UnityEngine.CoreModule.dll",
        "UnityEngine", "Transform", "set_localPosition", "UnityEngine.Vector3",
        "System.Void", 1));
    required(Resolve(g_methods.transform_local_rotation,
        "unity.transform.local_rotation", "UnityEngine.CoreModule.dll",
        "UnityEngine", "Transform", "get_localRotation", nullptr,
        "UnityEngine.Quaternion", 0));
    required(Resolve(g_methods.transform_set_local_rotation,
        "unity.transform.set_local_rotation", "UnityEngine.CoreModule.dll",
        "UnityEngine", "Transform", "set_localRotation", "UnityEngine.Quaternion",
        "System.Void", 1));
    required(Resolve(g_methods.transform_local_scale, "unity.transform.local_scale",
        "UnityEngine.CoreModule.dll", "UnityEngine", "Transform", "get_localScale",
        nullptr, "UnityEngine.Vector3", 0));
    required(Resolve(g_methods.transform_set_local_scale,
        "unity.transform.set_local_scale", "UnityEngine.CoreModule.dll",
        "UnityEngine", "Transform", "set_localScale", "UnityEngine.Vector3",
        "System.Void", 1));
    required(Resolve(g_methods.behaviour_set_enabled, "unity.behaviour.enabled",
        "UnityEngine.CoreModule.dll", "UnityEngine", "Behaviour", "set_enabled",
        "System.Boolean", "System.Void", 1));
    required(Resolve(g_methods.animator_rebind, "unity.animator.rebind",
        "UnityEngine.AnimationModule.dll", "UnityEngine", "Animator", "Rebind",
        "System.Boolean", "System.Void", 1));
    required(Resolve(g_methods.animator_culling, "unity.animator.culling",
        "UnityEngine.AnimationModule.dll", "UnityEngine", "Animator",
        "set_cullingMode", "UnityEngine.AnimatorCullingMode", "System.Void", 1));
    required(Resolve(g_methods.animator_update, "unity.animator.update_mode",
        "UnityEngine.AnimationModule.dll", "UnityEngine", "Animator",
        "set_updateMode", "UnityEngine.AnimatorUpdateMode", "System.Void", 1));
    required(Resolve(g_methods.animator_initialized,
        "unity.animator.initialized", "UnityEngine.AnimationModule.dll",
        "UnityEngine", "Animator", "get_isInitialized", nullptr,
        "System.Boolean", 0));
    required(Resolve(g_methods.animator_human, "unity.animator.human",
        "UnityEngine.AnimationModule.dll", "UnityEngine", "Animator",
        "get_isHuman", nullptr, "System.Boolean", 0));
    required(Resolve(g_methods.animator_avatar, "unity.animator.avatar",
        "UnityEngine.AnimationModule.dll", "UnityEngine", "Animator",
        "get_avatar", nullptr, "UnityEngine.Avatar", 0));
    required(Resolve(g_methods.clip_length, "unity.animation_clip.length",
        "UnityEngine.AnimationModule.dll", "UnityEngine", "AnimationClip",
        "get_length", nullptr, "System.Single", 0));
    required(Resolve(g_methods.clip_empty, "unity.animation_clip.empty",
        "UnityEngine.AnimationModule.dll", "UnityEngine", "AnimationClip",
        "get_empty", nullptr, "System.Boolean", 0));
    required(Resolve(g_methods.clip_human_motion,
        "unity.animation_clip.human_motion", "UnityEngine.AnimationModule.dll",
        "UnityEngine", "AnimationClip", "get_humanMotion", nullptr,
        "System.Boolean", 0));
    required(Resolve(g_methods.i18n_load, "resource.i18n_load",
        "Common.Beyond.dll", "Beyond.Resource", "I18NAssetLoader", "Load",
        "Beyond.Resource.StringPathHash|System.Type|Beyond.Resource.RootCategory",
        "Beyond.Resource.FAssetProxyHandle", 3));
    required(Resolve(g_methods.asset_handle_get, "resource.asset_handle.get",
        "Common.Beyond.dll", "Beyond.Resource", "FAssetProxyHandle", "Get",
        nullptr, "UnityEngine.Object", 0));
    required(Resolve(g_methods.resource_initialized, "resource.initialized",
        "Common.Beyond.dll", "Beyond.Resource", "ResourceManager", "IsInitialized",
        nullptr, "System.Boolean", 0));
    required(Resolve(g_methods.resource_async_initing, "resource.async_initing",
        "Common.Beyond.dll", "Beyond.Resource", "ResourceManager", "IsAsyncIniting",
        nullptr, "System.Boolean", 0));
    required(Resolve(g_methods.playable_graph_create,
        "playables.graph.create", "UnityEngine.CoreModule.dll",
        "UnityEngine.Playables", "PlayableGraph", "Create_Injected",
        "System.String|UnityEngine.Playables.PlayableGraph&", "System.Void", 2));
    required(Resolve(g_methods.playable_graph_destroy,
        "playables.graph.destroy", "UnityEngine.CoreModule.dll",
        "UnityEngine.Playables", "PlayableGraph", "Destroy_Injected",
        "UnityEngine.Playables.PlayableGraph&", "System.Void", 1));
    required(Resolve(g_methods.playable_graph_play,
        "playables.graph.play", "UnityEngine.CoreModule.dll",
        "UnityEngine.Playables", "PlayableGraph", "Play_Injected",
        "UnityEngine.Playables.PlayableGraph&", "System.Void", 1));
    required(Resolve(g_methods.playable_graph_evaluate,
        "playables.graph.evaluate", "UnityEngine.CoreModule.dll",
        "UnityEngine.Playables", "PlayableGraph", "Evaluate_Injected",
        "UnityEngine.Playables.PlayableGraph&|System.Single", "System.Void", 2));
    required(Resolve(g_methods.playable_graph_set_update_mode,
        "playables.graph.update_mode", "UnityEngine.CoreModule.dll",
        "UnityEngine.Playables", "PlayableGraph", "SetTimeUpdateMode_Injected",
        "UnityEngine.Playables.PlayableGraph&|UnityEngine.Playables.DirectorUpdateMode",
        "System.Void", 2));
    required(Resolve(g_methods.clip_playable_create,
        "playables.clip.create", "UnityEngine.AnimationModule.dll",
        "UnityEngine.Animations", "AnimationClipPlayable",
        "CreateHandleInternal_Injected",
        "UnityEngine.Playables.PlayableGraph&|UnityEngine.AnimationClip|UnityEngine.Playables.PlayableHandle&",
        "System.Boolean", 3));
    required(Resolve(g_methods.clip_playable_override_loop,
        "playables.clip.override_loop", "UnityEngine.AnimationModule.dll",
        "UnityEngine.Animations", "AnimationClipPlayable",
        "SetOverrideLoopTimeInternal",
        "UnityEngine.Playables.PlayableHandle&|System.Boolean", "System.Void", 2));
    required(Resolve(g_methods.clip_playable_set_loop,
        "playables.clip.set_loop", "UnityEngine.AnimationModule.dll",
        "UnityEngine.Animations", "AnimationClipPlayable", "SetLoopTimeInternal",
        "UnityEngine.Playables.PlayableHandle&|System.Boolean", "System.Void", 2));
    required(Resolve(g_methods.animation_output_create,
        "playables.output.create", "UnityEngine.AnimationModule.dll",
        "UnityEngine.Animations", "AnimationPlayableGraphExtensions",
        "InternalCreateAnimationOutput",
        "UnityEngine.Playables.PlayableGraph&|System.String|UnityEngine.Playables.PlayableOutputHandle&",
        "System.Boolean", 3));
    required(Resolve(g_methods.animation_output_set_target,
        "playables.output.target", "UnityEngine.AnimationModule.dll",
        "UnityEngine.Animations", "AnimationPlayableOutput", "InternalSetTarget",
        "UnityEngine.Playables.PlayableOutputHandle&|UnityEngine.Animator",
        "System.Void", 2));
    required(Resolve(g_methods.playable_output_set_source,
        "playables.output.source", "UnityEngine.CoreModule.dll",
        "UnityEngine.Playables", "PlayableOutputHandle",
        "SetSourcePlayable_Injected",
        "UnityEngine.Playables.PlayableOutputHandle&|UnityEngine.Playables.PlayableHandle&|System.Int32",
        "System.Void", 3));
    required(Resolve(g_methods.playable_set_wrap_mode,
        "playables.handle.wrap", "UnityEngine.CoreModule.dll",
        "UnityEngine.Playables", "PlayableHandle", "SetTimeWrapMode_Injected",
        "UnityEngine.Playables.PlayableHandle&|UnityEngine.Playables.DirectorWrapMode",
        "System.Void", 2));
    required(Resolve(g_methods.playable_set_speed,
        "playables.handle.speed", "UnityEngine.CoreModule.dll",
        "UnityEngine.Playables", "PlayableHandle", "SetSpeed_Injected",
        "UnityEngine.Playables.PlayableHandle&|System.Double", "System.Void", 2));
    required(Resolve(g_methods.playable_set_time,
        "playables.handle.time", "UnityEngine.CoreModule.dll",
        "UnityEngine.Playables", "PlayableHandle", "SetTime_Injected",
        "UnityEngine.Playables.PlayableHandle&|System.Double", "System.Void", 2));
    required(Resolve(g_methods.playable_get_time,
        "playables.handle.get_time", "UnityEngine.CoreModule.dll",
        "UnityEngine.Playables", "PlayableHandle", "GetTime_Injected",
        "UnityEngine.Playables.PlayableHandle&", "System.Double", 1));
    required(Resolve(g_methods.mixer_playable_create,
        "playables.mixer.create", "UnityEngine.AnimationModule.dll",
        "UnityEngine.Animations", "AnimationMixerPlayable",
        "CreateHandleInternal_Injected",
        "UnityEngine.Playables.PlayableGraph&|UnityEngine.Playables.PlayableHandle&",
        "System.Boolean", 2));
    required(Resolve(g_methods.playable_set_input_count,
        "playables.handle.input_count", "UnityEngine.CoreModule.dll",
        "UnityEngine.Playables", "PlayableHandle", "SetInputCount_Injected",
        "UnityEngine.Playables.PlayableHandle&|System.Int32", "System.Void", 2));
    required(Resolve(g_methods.playable_connect,
        "playables.graph.connect", "UnityEngine.CoreModule.dll",
        "UnityEngine.Playables", "PlayableGraph", "ConnectInternal_Injected",
        "UnityEngine.Playables.PlayableGraph&|UnityEngine.Playables.PlayableHandle&|System.Int32|UnityEngine.Playables.PlayableHandle&|System.Int32",
        "System.Boolean", 5));
    required(Resolve(g_methods.playable_set_input_weight,
        "playables.handle.input_weight", "UnityEngine.CoreModule.dll",
        "UnityEngine.Playables", "PlayableHandle",
        "SetInputWeightFromIndex_Injected",
        "UnityEngine.Playables.PlayableHandle&|System.Int32|System.Single",
        "System.Void", 3));

    required(ResolveClass(g_classes.game_object, "UnityEngine.CoreModule.dll",
        "UnityEngine", "GameObject", true));
    required(ResolveClass(g_classes.animator, "UnityEngine.AnimationModule.dll",
        "UnityEngine", "Animator", true));
    required(ResolveClass(g_classes.animation_clip,
        "UnityEngine.AnimationModule.dll", "UnityEngine", "AnimationClip", true));
    required(ResolveClass(g_classes.renderer, "UnityEngine.CoreModule.dll",
        "UnityEngine", "Renderer", true));
    if (missing != 0) {
        char message[128]{};
        std::snprintf(message, sizeof(message),
            "dynamic model contract missing=%zu", missing);
        g_state.store(ModuleState::ContractMismatch, std::memory_order_release);
        LogState(ModuleState::ContractMismatch, message);
        return false;
    }
    g_state.store(ModuleState::Ready, std::memory_order_release);
    LogState(ModuleState::Ready,
        "login, Main VFS, Unity object, and PlayableGraph contracts resolved");
    return true;
}

void* Invoke(const RuntimeMethod& method, void* instance, void** parameters,
    const char* operation) {
    if (!g_host || !g_host->runtime_invoke || !method.method_info) {
        return nullptr;
    }
    void* exception = nullptr;
    void* result = g_host->runtime_invoke(g_host->context, method.method_info,
        instance, parameters, &exception);
    if (exception) {
        Log(std::string("[model-runtime] managed exception operation=") + operation);
        return nullptr;
    }
    return result;
}

bool InvokeVoid(const RuntimeMethod& method, void* instance, void** parameters,
    const char* operation) {
    if (!g_host || !g_host->runtime_invoke || !method.method_info) {
        return false;
    }
    void* exception = nullptr;
    g_host->runtime_invoke(g_host->context, method.method_info, instance,
        parameters, &exception);
    if (exception) {
        Log(std::string("[model-runtime] managed exception operation=") + operation);
        return false;
    }
    return true;
}

template <typename T>
bool Unbox(void* boxed, T& output) {
    if (!boxed || !g_host || !g_host->object_unbox) {
        return false;
    }
    void* value = g_host->object_unbox(g_host->context, boxed);
    if (!value) {
        return false;
    }
    __try {
        std::memcpy(&output, value, sizeof(output));
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

void* SafeAssetGet(void* handle) {
    __try {
        return reinterpret_cast<AssetGetFn>(g_methods.asset_handle_get.pointer)(
            handle, const_cast<void*>(g_methods.asset_handle_get.method_info));
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return nullptr;
    }
}

void* SafeCloneWithParent(void* original, void* parent) {
    __try {
        return reinterpret_cast<CloneWithParentFn>(
            g_methods.clone_with_parent.pointer)(original, parent, false,
            const_cast<void*>(g_methods.clone_with_parent.method_info));
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return nullptr;
    }
}

std::string ObjectName(void* object) {
    void* managed = object ? Invoke(g_methods.object_name, object, nullptr,
        "Object.get_name") : nullptr;
    char text[512]{};
    return managed && g_host->copy_managed_string &&
        g_host->copy_managed_string(g_host->context, managed, text, sizeof(text)) > 0
        ? std::string(text)
        : std::string{};
}

bool SetActive(void* object, bool active) {
    if (!object) {
        return false;
    }
    void* parameters[1]{&active};
    return InvokeVoid(g_methods.game_object_set_active, object, parameters,
        "GameObject.SetActive");
}

void DestroyObject(void* object) {
    if (!object) {
        return;
    }
    void* parameters[1]{object};
    Invoke(g_methods.object_destroy, nullptr, parameters, "Object.Destroy");
}

ModelConfiguration ConfigurationSnapshot() {
    std::lock_guard lock(g_configuration_mutex);
    return g_configuration;
}

void FreeRoot(uint32_t& handle) {
    if (handle && g_host && g_host->gchandle_free) {
        g_host->gchandle_free(g_host->context, handle);
    }
    handle = 0;
}

void ReleaseAssets() {
    FreeRoot(g_model_prefab_root);
    g_model_prefab = nullptr;
    for (size_t index = 0; index < g_clips.size(); ++index) {
        FreeRoot(g_clip_roots[index]);
        g_clips[index] = nullptr;
    }
    g_assets_ready = false;
    g_loaded_revision.store(0, std::memory_order_release);
}

bool TryDestroyPlayableGraph(PlayableData* graph) {
    __try {
        reinterpret_cast<PlayableGraphActionFn>(
            g_methods.playable_graph_destroy.pointer)(
            graph,
            const_cast<void*>(g_methods.playable_graph_destroy.method_info));
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

void DestroyPlayableGraph() {
    if (g_actor.graph.handle && !TryDestroyPlayableGraph(&g_actor.graph)) {
        Log("[model-animation] native exception destroying PlayableGraph");
    }
    g_actor.graph = {};
    g_actor.playable = {};
    g_actor.mixer = {};
    g_actor.mixer_playables = {};
    g_actor.mixer_active_input = 0;
    g_actor.mixer_blend_count = 0;
    g_actor.mixer_blend_active = false;
    g_actor.mixer_configured = false;
    g_actor.mixer_fault_logged = false;
}

void DestroyGraph() {
    DestroyPlayableGraph();
    g_actor.animator = nullptr;
    g_actor.phase = SequencePhase::None;
    g_actor.pending = SequencePhase::None;
    g_actor.phase_elapsed = 0.0;
    g_actor.phase_length = 0.0f;
}

int ManagedArrayLength(void* array) {
    int dimension = 0;
    int length = 0;
    void* parameters[1]{&dimension};
    return array && Unbox(Invoke(g_methods.array_get_length, array, parameters,
        "Array.GetLength"), length) ? length : 0;
}

void* ManagedArrayValue(void* array, int index) {
    void* parameters[1]{&index};
    return array ? Invoke(g_methods.array_get_value, array, parameters,
        "Array.GetValue") : nullptr;
}

void* GetComponentsInChildren(void* root, void* type_object) {
    bool include_inactive = true;
    void* parameters[2]{type_object, &include_inactive};
    return root && type_object
        ? Invoke(g_methods.game_object_get_components, root, parameters,
            "GameObject.GetComponentsInChildren")
        : nullptr;
}

bool SetRendererEnabled(void* renderer, bool enabled) {
    void* parameters[1]{&enabled};
    return renderer && InvokeVoid(g_methods.renderer_set_enabled, renderer,
        parameters, "Renderer.set_enabled");
}

void RestoreOriginalRenderers() {
    for (void* renderer : g_actor.disabled_renderers) {
        SetRendererEnabled(renderer, true);
    }
    if (!g_actor.disabled_renderers.empty()) {
        Log("[model-render] original renderer hierarchy restored count=" +
            std::to_string(g_actor.disabled_renderers.size()));
    }
    g_actor.disabled_renderers.clear();
    g_actor.original_renderers_hidden = false;
}

bool DisableOriginalRenderers() {
    if (!g_actor.original) {
        return false;
    }
    void* animators = GetComponentsInChildren(
        g_actor.original, g_classes.animator.type_object);
    const int animator_count = ManagedArrayLength(animators);
    int always_animate = 0;
    for (int index = 0; index < animator_count; ++index) {
        void* animator = ManagedArrayValue(animators, index);
        void* parameters[1]{&always_animate};
        InvokeVoid(g_methods.animator_culling, animator, parameters,
            "Animator.set_cullingMode(original)");
    }

    void* renderers = GetComponentsInChildren(
        g_actor.original, g_classes.renderer.type_object);
    const int renderer_count = ManagedArrayLength(renderers);
    if (!renderers || renderer_count <= 0) {
        Log("[model-render] original renderer enumeration returned no objects");
        return false;
    }
    g_actor.disabled_renderers.clear();
    for (int index = 0; index < renderer_count; ++index) {
        void* renderer = ManagedArrayValue(renderers, index);
        if (!renderer || !SetRendererEnabled(renderer, false)) {
            RestoreOriginalRenderers();
            return false;
        }
        g_actor.disabled_renderers.push_back(renderer);
    }
    g_actor.original_renderers_hidden = true;
    Log("[model-render] original visuals hidden; root and Animator remain active "
        "renderers=" + std::to_string(renderer_count) +
        " animators=" + std::to_string(animator_count));
    return true;
}

void* FindTransformByName(void* root_transform, const char* name) {
    if (!root_transform || !name || !g_host || !g_host->string_new) {
        return nullptr;
    }
    void* managed_name = g_host->string_new(g_host->context, name);
    void* parameters[2]{root_transform, managed_name};
    return managed_name ? Invoke(g_methods.transform_find_name, nullptr,
        parameters, "Transform.FindTransformWithName") : nullptr;
}

bool AlignReplacementAnchor(bool snap_vertical, float delta_time = 0.0f) {
    if (!g_actor.original_anchor || !g_actor.replacement_anchor ||
        !g_actor.replacement_transform) {
        return false;
    }
    Vector3 original_position{};
    Vector3 replacement_position{};
    Vector3 root_position{};
    if (!Unbox(Invoke(g_methods.transform_position, g_actor.original_anchor,
            nullptr, "Transform.get_position(original pelvis)"),
            original_position) ||
        !Unbox(Invoke(g_methods.transform_position, g_actor.replacement_anchor,
            nullptr, "Transform.get_position(replacement pelvis)"),
            replacement_position) ||
        !Unbox(Invoke(g_methods.transform_position, g_actor.replacement_transform,
            nullptr, "Transform.get_position(replacement root)"), root_position)) {
        return false;
    }

    float vertical_correction = original_position.y - replacement_position.y;
    if (!snap_vertical) {
        const float safe_delta = std::clamp(delta_time, 0.0f, 0.1f);
        const float alpha = 1.0f - std::exp(
            -safe_delta / kVerticalFollowResponseSeconds);
        vertical_correction *= alpha;
    }
    Vector3 aligned{
        root_position.x + original_position.x - replacement_position.x,
        root_position.y + vertical_correction,
        root_position.z + original_position.z - replacement_position.z,
    };
    void* parameters[1]{&aligned};
    if (!InvokeVoid(g_methods.transform_set_position,
            g_actor.replacement_transform, parameters,
            "Transform.set_position(replacement root)")) {
        return false;
    }
    if (snap_vertical && !g_actor.anchor_alignment_logged) {
        g_actor.anchor_alignment_logged = true;
        char message[384]{};
        std::snprintf(message, sizeof(message),
            "[model-anchor] pelvis aligned; exact XZ and smoothed Y enabled "
            "delta=(%.3f,%.3f,%.3f) root=(%.3f,%.3f,%.3f)",
            static_cast<double>(original_position.x - replacement_position.x),
            static_cast<double>(original_position.y - replacement_position.y),
            static_cast<double>(original_position.z - replacement_position.z),
            static_cast<double>(aligned.x), static_cast<double>(aligned.y),
            static_cast<double>(aligned.z));
        Log(message);
    }
    return true;
}

bool SetReplacementYaw(float yaw_degrees) {
    if (!g_actor.replacement_transform) {
        return false;
    }
    constexpr float kPi = 3.14159265358979323846f;
    const float half_radians = yaw_degrees * kPi / 360.0f;
    Quaternion rotation{0.0f, std::sin(half_radians), 0.0f,
        std::cos(half_radians)};
    void* parameters[1]{&rotation};
    return InvokeVoid(g_methods.transform_set_local_rotation,
        g_actor.replacement_transform, parameters,
        "Transform.set_localRotation(phase facing)");
}

bool ApplyPhaseFacing(SequencePhase phase) {
    const ModelConfiguration configuration = ConfigurationSnapshot();
    const float yaw = phase == SequencePhase::Final
        ? 0.0f : configuration.start_yaw;
    if (!SetReplacementYaw(yaw)) {
        return false;
    }
    char message[256]{};
    std::snprintf(message, sizeof(message),
        "[model-facing] phase=%s localYaw=%.1f%s",
        PhaseName(phase), static_cast<double>(yaw),
        phase == SequencePhase::SitToWalk ? " smooth-to-zero=true" : "");
    Log(message);
    return true;
}

void UpdatePhaseFacing() {
    if (g_actor.phase != SequencePhase::SitToWalk) {
        return;
    }
    const ModelConfiguration configuration = ConfigurationSnapshot();
    const double normalized = std::clamp(
        g_actor.phase_elapsed / static_cast<double>(configuration.turn_duration),
        0.0, 1.0);
    const double eased = normalized * normalized * (3.0 - 2.0 * normalized);
    SetReplacementYaw(static_cast<float>(
        configuration.start_yaw * (1.0 - eased)));
}

void CleanupScene(const char* reason) {
    DestroyGraph();
    RestoreOriginalRenderers();
    if (g_actor.replacement) {
        SetActive(g_actor.replacement, false);
        DestroyObject(g_actor.replacement);
    }
    if (g_actor.original || g_actor.replacement) {
        Log(std::string("[model-cleanup] reason=") + reason);
    }
    g_actor = {};
    g_next_actor_discovery_tick = 0;
}

bool ResourcesReady() {
    bool initialized = false;
    bool async_initing = true;
    return Unbox(Invoke(g_methods.resource_initialized, nullptr, nullptr,
               "ResourceManager.IsInitialized"), initialized) &&
        Unbox(Invoke(g_methods.resource_async_initing, nullptr, nullptr,
               "ResourceManager.IsAsyncIniting"), async_initing) &&
        initialized && !async_initing;
}

void* LoadAsset(uint64_t path_hash, void* type_object, const char* label) {
    uint8_t category = kMainCategory;
    void* parameters[3]{&path_hash, type_object, &category};
    void* boxed_handle = Invoke(g_methods.i18n_load, nullptr, parameters,
        "I18NAssetLoader.Load");
    void* handle = boxed_handle && g_host->object_unbox
        ? g_host->object_unbox(g_host->context, boxed_handle)
        : nullptr;
    void* asset = handle ? SafeAssetGet(handle) : nullptr;
    char message[256]{};
    std::snprintf(message, sizeof(message),
        "[model-load] asset=%s hash=%016llX result=%p",
        label, static_cast<unsigned long long>(path_hash), asset);
    Log(message);
    return asset;
}

bool ValidateLoadedClips(const ModelConfiguration& configuration) {
    for (size_t index = 0; index < g_clips.size(); ++index) {
        float length = -1.0f;
        bool empty = true;
        bool human_motion = false;
        const bool length_readable = Unbox(Invoke(g_methods.clip_length,
            g_clips[index], nullptr, "AnimationClip.get_length"), length);
        const bool empty_readable = Unbox(Invoke(g_methods.clip_empty,
            g_clips[index], nullptr, "AnimationClip.get_empty"), empty);
        const bool human_readable = Unbox(Invoke(g_methods.clip_human_motion,
            g_clips[index], nullptr, "AnimationClip.get_humanMotion"),
            human_motion);
        if (length_readable && length > 0.0f && empty_readable && !empty &&
            human_readable && human_motion) {
            continue;
        }
        char message[384]{};
        std::snprintf(message, sizeof(message),
            "[model-load] clip invalid label=%s lengthReadable=%s length=%.3f "
            "emptyReadable=%s empty=%s humanReadable=%s humanMotion=%s",
            configuration.clips[index].label.c_str(),
            length_readable ? "true" : "false", static_cast<double>(length),
            empty_readable ? "true" : "false", empty ? "true" : "false",
            human_readable ? "true" : "false",
            human_motion ? "true" : "false");
        Log(message);
        return false;
    }
    return true;
}

bool LoadConfiguredAssets() {
    if (g_assets_ready) {
        return true;
    }
    const uint64_t now = GetTickCount64();
    if (now < g_next_asset_retry_tick || !g_main_hash_ready.load() ||
        !g_initial_hash_ready.load() || !ResourcesReady()) {
        return false;
    }
    const ModelConfiguration configuration = ConfigurationSnapshot();
    if (!configuration.enabled) {
        return false;
    }

    ReleaseAssets();
    g_model_prefab = LoadAsset(configuration.model_path_hash,
        g_classes.game_object.type_object, "model");
    if (g_model_prefab && g_host->gchandle_new) {
        g_model_prefab_root =
            g_host->gchandle_new(g_host->context, g_model_prefab, 0);
    }
    for (size_t index = 0; index < g_clips.size() && g_model_prefab; ++index) {
        g_clips[index] = LoadAsset(configuration.clips[index].path_hash,
            g_classes.animation_clip.type_object,
            configuration.clips[index].label.c_str());
        if (g_clips[index] && g_host->gchandle_new) {
            g_clip_roots[index] =
                g_host->gchandle_new(g_host->context, g_clips[index], 0);
        }
    }
    g_assets_ready = g_model_prefab && g_model_prefab_root != 0 &&
        std::all_of(g_clips.begin(), g_clips.end(),
            [](void* clip) { return clip != nullptr; }) &&
        std::all_of(g_clip_roots.begin(), g_clip_roots.end(),
            [](uint32_t root) { return root != 0; }) &&
        ValidateLoadedClips(configuration);
    if (!g_assets_ready) {
        ReleaseAssets();
        g_next_asset_retry_tick = now + 2000;
        Log("[model-load] configured sequence incomplete; retry scheduled");
        return false;
    }
    g_loaded_revision.store(g_configuration_revision.load(),
        std::memory_order_release);
    Log("[model-load] model and all four animation clips are resident");
    return true;
}

bool CopyTransformAndLayer(void* original, void* replacement, float scale_factor) {
    void* original_transform = Invoke(g_methods.game_object_transform, original,
        nullptr, "GameObject.get_transform(original)");
    void* replacement_transform = Invoke(g_methods.game_object_transform,
        replacement, nullptr, "GameObject.get_transform(replacement)");
    if (!original_transform || !replacement_transform) {
        return false;
    }

    Vector3 position{};
    Quaternion rotation{};
    Vector3 scale{};
    if (!Unbox(Invoke(g_methods.transform_local_position, original_transform,
            nullptr, "Transform.get_localPosition"), position) ||
        !Unbox(Invoke(g_methods.transform_local_rotation, original_transform,
            nullptr, "Transform.get_localRotation"), rotation) ||
        !Unbox(Invoke(g_methods.transform_local_scale, original_transform,
            nullptr, "Transform.get_localScale"), scale)) {
        return false;
    }
    scale.x *= scale_factor;
    scale.y *= scale_factor;
    scale.z *= scale_factor;
    void* position_parameters[1]{&position};
    void* rotation_parameters[1]{&rotation};
    void* scale_parameters[1]{&scale};
    const bool transform_updated =
        InvokeVoid(g_methods.transform_set_local_position, replacement_transform,
            position_parameters, "Transform.set_localPosition") &&
        InvokeVoid(g_methods.transform_set_local_rotation, replacement_transform,
            rotation_parameters, "Transform.set_localRotation") &&
        InvokeVoid(g_methods.transform_set_local_scale, replacement_transform,
            scale_parameters, "Transform.set_localScale");
    if (!transform_updated) {
        return false;
    }

    int layer = 0;
    if (Unbox(Invoke(g_methods.game_object_get_layer, original, nullptr,
            "GameObject.get_layer"), layer)) {
        void* layer_parameters[1]{&layer};
        if (!InvokeVoid(g_methods.game_object_set_layer_recursive, replacement,
                layer_parameters, "GameObject.SetLayerRecursive")) {
            return false;
        }
    }
    char message[256]{};
    std::snprintf(message, sizeof(message),
        "[model-transform] scaleFactor=%.3f localPosition=(%.3f,%.3f,%.3f) layer=%d",
        static_cast<double>(scale_factor), static_cast<double>(position.x),
        static_cast<double>(position.y), static_cast<double>(position.z), layer);
    Log(message);
    g_actor.original_transform = original_transform;
    g_actor.replacement_transform = replacement_transform;
    g_actor.original_anchor = FindTransformByName(
        original_transform, "Bip001_Pelvis");
    g_actor.replacement_anchor = FindTransformByName(
        replacement_transform, "Bip001_Pelvis");
    if (!g_actor.original_anchor || !g_actor.replacement_anchor) {
        Log("[model-anchor] Bip001_Pelvis missing; copied sibling root pose retained");
    }
    return true;
}

bool BuildPlayableGraph(void* animator, void* clip, double speed, bool loop,
    double sample_time, bool use_mixer, PlayableData& graph,
    PlayableData& primary, PlayableData& secondary, PlayableData& mixer) {
    graph = {};
    primary = {};
    secondary = {};
    mixer = {};
    PlayableData output{};
    void* graph_name = g_host->string_new(g_host->context,
        "Better Endfield Login Animation");
    void* output_name = g_host->string_new(g_host->context,
        "Better Endfield Login Output");
    if (!graph_name || !output_name) {
        return false;
    }

    bool created = false;
    __try {
        reinterpret_cast<PlayableGraphCreateFn>(
            g_methods.playable_graph_create.pointer)(
            graph_name, &graph,
            const_cast<void*>(g_methods.playable_graph_create.method_info));
        created = graph.handle && reinterpret_cast<ClipPlayableCreateFn>(
            g_methods.clip_playable_create.pointer)(
            &graph, clip, &primary,
            const_cast<void*>(g_methods.clip_playable_create.method_info));
        if (created) {
            reinterpret_cast<PlayableBoolFn>(
                g_methods.clip_playable_override_loop.pointer)(
                &primary, true,
                const_cast<void*>(g_methods.clip_playable_override_loop.method_info));
            reinterpret_cast<PlayableBoolFn>(
                g_methods.clip_playable_set_loop.pointer)(
                &primary, loop,
                const_cast<void*>(g_methods.clip_playable_set_loop.method_info));
        }

        PlayableData* graph_source = &primary;
        if (created && use_mixer) {
            created = reinterpret_cast<ClipPlayableCreateFn>(
                g_methods.clip_playable_create.pointer)(
                &graph, clip, &secondary,
                const_cast<void*>(g_methods.clip_playable_create.method_info));
            if (created) {
                reinterpret_cast<PlayableBoolFn>(
                    g_methods.clip_playable_override_loop.pointer)(
                    &secondary, true,
                    const_cast<void*>(
                        g_methods.clip_playable_override_loop.method_info));
                reinterpret_cast<PlayableBoolFn>(
                    g_methods.clip_playable_set_loop.pointer)(
                    &secondary, false,
                    const_cast<void*>(g_methods.clip_playable_set_loop.method_info));
                created = reinterpret_cast<MixerPlayableCreateFn>(
                    g_methods.mixer_playable_create.pointer)(
                    &graph, &mixer,
                    const_cast<void*>(g_methods.mixer_playable_create.method_info));
            }
            if (created) {
                reinterpret_cast<PlayableIntFn>(
                    g_methods.playable_set_input_count.pointer)(
                    &mixer, 2,
                    const_cast<void*>(g_methods.playable_set_input_count.method_info));
                const bool first_connected = reinterpret_cast<PlayableConnectFn>(
                    g_methods.playable_connect.pointer)(
                    &graph, &primary, 0, &mixer, 0,
                    const_cast<void*>(g_methods.playable_connect.method_info));
                const bool second_connected = reinterpret_cast<PlayableConnectFn>(
                    g_methods.playable_connect.pointer)(
                    &graph, &secondary, 0, &mixer, 1,
                    const_cast<void*>(g_methods.playable_connect.method_info));
                created = first_connected && second_connected;
            }
            if (created) {
                reinterpret_cast<PlayableWeightFn>(
                    g_methods.playable_set_input_weight.pointer)(
                    &mixer, 0, 1.0f,
                    const_cast<void*>(g_methods.playable_set_input_weight.method_info));
                reinterpret_cast<PlayableWeightFn>(
                    g_methods.playable_set_input_weight.pointer)(
                    &mixer, 1, 0.0f,
                    const_cast<void*>(g_methods.playable_set_input_weight.method_info));
                graph_source = &mixer;
            }
        }
        if (created) {
            created = reinterpret_cast<AnimationOutputCreateFn>(
                g_methods.animation_output_create.pointer)(
                &graph, output_name, &output,
                const_cast<void*>(g_methods.animation_output_create.method_info));
        }
        if (created) {
            reinterpret_cast<PlayableGraphIntFn>(
                g_methods.playable_graph_set_update_mode.pointer)(
                &graph, kUnscaledGameTime,
                const_cast<void*>(g_methods.playable_graph_set_update_mode.method_info));
            reinterpret_cast<AnimationOutputSetTargetFn>(
                g_methods.animation_output_set_target.pointer)(
                &output, animator,
                const_cast<void*>(g_methods.animation_output_set_target.method_info));
            reinterpret_cast<PlayableOutputSetSourceFn>(
                g_methods.playable_output_set_source.pointer)(
                &output, graph_source, 0,
                const_cast<void*>(g_methods.playable_output_set_source.method_info));
            reinterpret_cast<PlayableIntFn>(
                g_methods.playable_set_wrap_mode.pointer)(
                &primary, loop ? kWrapLoop : kWrapHold,
                const_cast<void*>(g_methods.playable_set_wrap_mode.method_info));
            reinterpret_cast<PlayableDoubleFn>(
                g_methods.playable_set_speed.pointer)(
                &primary, speed,
                const_cast<void*>(g_methods.playable_set_speed.method_info));
            reinterpret_cast<PlayableDoubleFn>(
                g_methods.playable_set_time.pointer)(
                &primary, sample_time,
                const_cast<void*>(g_methods.playable_set_time.method_info));
            if (use_mixer) {
                reinterpret_cast<PlayableIntFn>(
                    g_methods.playable_set_wrap_mode.pointer)(
                    &secondary, kWrapHold,
                    const_cast<void*>(g_methods.playable_set_wrap_mode.method_info));
                reinterpret_cast<PlayableDoubleFn>(
                    g_methods.playable_set_speed.pointer)(
                    &secondary, 0.0,
                    const_cast<void*>(g_methods.playable_set_speed.method_info));
                reinterpret_cast<PlayableDoubleFn>(
                    g_methods.playable_set_time.pointer)(
                    &secondary, 0.0,
                    const_cast<void*>(g_methods.playable_set_time.method_info));
            }
            reinterpret_cast<PlayableGraphActionFn>(
                g_methods.playable_graph_play.pointer)(
                &graph,
                const_cast<void*>(g_methods.playable_graph_play.method_info));
            reinterpret_cast<PlayableGraphEvaluateFn>(
                g_methods.playable_graph_evaluate.pointer)(
                &graph, 0.0f,
                const_cast<void*>(g_methods.playable_graph_evaluate.method_info));
        }
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        created = false;
    }
    if (!created && graph.handle) {
        TryDestroyPlayableGraph(&graph);
        graph = {};
        primary = {};
        secondary = {};
        mixer = {};
    }
    return created;
}

bool StartPhase(SequencePhase phase, const char* source) {
    const size_t phase_index = static_cast<size_t>(phase);
    if (!g_actor.animator || phase_index >= g_clips.size()) {
        return false;
    }
    const ModelConfiguration configuration = ConfigurationSnapshot();
    const size_t clip_index = phase == SequencePhase::SitLoop
        ? static_cast<size_t>(SequencePhase::SitToWalk)
        : phase_index;
    void* clip = g_clips[clip_index];
    if (!clip) {
        return false;
    }
    const bool hold_forward_pose = phase == SequencePhase::SitLoop;
    const bool use_mixer = phase == SequencePhase::Final &&
        configuration.use_crossfade;
    const double sample_time =
        phase == SequencePhase::SitLoop || phase == SequencePhase::SitToWalk
        ? static_cast<double>(configuration.forward_lean_sample) : 0.0;
    const double speed = hold_forward_pose
        ? 0.0 : static_cast<double>(configuration.clips[phase_index].speed);
    const bool final_native_loop = phase == SequencePhase::Final &&
        (configuration.force_loop ||
            (configuration.clips[phase_index].loop &&
                configuration.final_native_loop));
    const bool playback_loop = !hold_forward_pose && !use_mixer &&
        (phase == SequencePhase::Final
            ? final_native_loop : configuration.clips[phase_index].loop);
    float length = 0.0f;
    Unbox(Invoke(g_methods.clip_length, clip, nullptr,
        "AnimationClip.get_length"), length);
    if (!(length > 0.0f)) {
        Log("[model-sequence] clip length is invalid");
        return false;
    }
    DestroyPlayableGraph();
    PlayableData graph{};
    PlayableData primary{};
    PlayableData secondary{};
    PlayableData mixer{};
    if (!BuildPlayableGraph(g_actor.animator, clip, speed, playback_loop,
            sample_time, use_mixer, graph, primary, secondary, mixer)) {
        Log(std::string("[model-animation] PlayableGraph setup failed phase=") +
            PhaseName(phase));
        return false;
    }
    g_actor.graph = graph;
    g_actor.playable = use_mixer ? mixer : primary;
    g_actor.mixer = mixer;
    g_actor.mixer_playables = {primary, secondary};
    g_actor.mixer_active_input = 0;
    g_actor.mixer_blend_count = 0;
    g_actor.mixer_blend_active = false;
    g_actor.mixer_configured = use_mixer;
    g_actor.mixer_fault_logged = false;
    g_actor.phase = phase;
    g_actor.pending = SequencePhase::None;
    if (phase == SequencePhase::SitLoop) {
        g_actor.phase_length = 0.0f;
    } else if (phase == SequencePhase::SitToWalk) {
        g_actor.phase_length = std::max(0.0f,
            length - configuration.forward_lean_sample);
    } else if (use_mixer) {
        g_actor.phase_length = std::min(length, configuration.loop_end);
    } else {
        g_actor.phase_length = length;
    }
    g_actor.phase_elapsed = 0.0;
    if (!ApplyPhaseFacing(phase)) {
        Log("[model-facing] warning: phase-facing update failed");
    }
    char message[384]{};
    std::snprintf(message, sizeof(message),
        "[model-sequence] phase=%s label=%s source=%s sourceClip=%s "
        "length=%.3f effectiveLength=%.3f speed=%.2f loop=%s sample=%.3f",
        PhaseName(phase), configuration.clips[phase_index].label.c_str(),
        source ? source : "unknown",
        configuration.clips[clip_index].label.c_str(),
        static_cast<double>(length), static_cast<double>(g_actor.phase_length),
        speed, playback_loop ? "true" : "false", sample_time);
    Log(message);
    char graph_message[420]{};
    std::snprintf(graph_message, sizeof(graph_message),
        "[model-animation] graph started phase=%s graph=%p version=%u "
        "playable=%p speed=%.2f loop=%s sample=%.3f hold=%s mixer=%s "
        "updateMode=UnscaledGameTime evaluated=true",
        PhaseName(phase), graph.handle, graph.version, g_actor.playable.handle,
        speed, playback_loop ? "true" : "false", sample_time,
        hold_forward_pose ? "true" : "false", use_mixer ? "true" : "false");
    Log(graph_message);
    return true;
}

bool PrepareAnimation(void* replacement) {
    void* animator_type = g_classes.animator.type_object;
    bool include_inactive = true;
    void* parameters[2]{animator_type, &include_inactive};
    void* animator = Invoke(g_methods.game_object_get_animator, replacement,
        parameters, "GameObject.GetComponentInChildren(Animator)");
    if (!animator) {
        Log("[model-animation] replacement has no Animator");
        return false;
    }

    bool enabled = true;
    bool write_defaults = true;
    int culling = 0;
    int update_mode = 2;
    void* enabled_parameters[1]{&enabled};
    void* culling_parameters[1]{&culling};
    void* update_parameters[1]{&update_mode};
    void* rebind_parameters[1]{&write_defaults};
    if (!InvokeVoid(g_methods.behaviour_set_enabled, animator, enabled_parameters,
            "Behaviour.set_enabled") ||
        !InvokeVoid(g_methods.animator_culling, animator, culling_parameters,
            "Animator.set_cullingMode") ||
        !InvokeVoid(g_methods.animator_update, animator, update_parameters,
            "Animator.set_updateMode") ||
        !InvokeVoid(g_methods.animator_rebind, animator, rebind_parameters,
            "Animator.Rebind")) {
        return false;
    }

    bool initialized = false;
    bool human = false;
    void* avatar = Invoke(g_methods.animator_avatar, animator, nullptr,
        "Animator.get_avatar");
    const bool initialized_readable = Unbox(Invoke(
        g_methods.animator_initialized, animator, nullptr,
        "Animator.get_isInitialized"), initialized);
    const bool human_readable = Unbox(Invoke(g_methods.animator_human,
        animator, nullptr, "Animator.get_isHuman"), human);
    char readiness[320]{};
    std::snprintf(readiness, sizeof(readiness),
        "[model-animation] Animator ready initialized=%s readable=%s "
        "human=%s humanReadable=%s avatar=%p",
        initialized ? "true" : "false",
        initialized_readable ? "true" : "false",
        human ? "true" : "false", human_readable ? "true" : "false", avatar);
    Log(readiness);
    if (!initialized_readable || !initialized || !human_readable || !human ||
        !avatar) {
        return false;
    }

    g_actor.animator = animator;
    const SequencePhase requested = g_requested_phase.load();
    if (!StartPhase(
            requested == SequencePhase::None ? SequencePhase::SitLoop : requested,
            "replacement-created")) {
        DestroyGraph();
        return false;
    }
    return true;
}

bool InstantiateReplacement() {
    if (g_actor.replacement) {
        return true;
    }
    if (!g_actor.original || !g_model_prefab ||
        g_actor.replacement_attempted) {
        return false;
    }
    g_actor.replacement_attempted = true;
    void* replacement = SafeCloneWithParent(g_model_prefab, g_actor.parent);
    if (!replacement) {
        Log("[model-replace] replacement Instantiate returned null");
        return false;
    }
    if (!SetActive(replacement, false)) {
        DestroyObject(replacement);
        return false;
    }
    const ModelConfiguration configuration = ConfigurationSnapshot();
    if (!CopyTransformAndLayer(g_actor.original, replacement, configuration.scale)) {
        DestroyObject(replacement);
        Log("[model-replace] transform synchronization failed");
        return false;
    }
    g_actor.replacement = replacement;
    if (!DisableOriginalRenderers()) {
        DestroyObject(replacement);
        g_actor.replacement = nullptr;
        Log("[model-replace] original renderer hierarchy could not be hidden");
        return false;
    }
    if (!SetActive(replacement, true)) {
        RestoreOriginalRenderers();
        DestroyObject(replacement);
        g_actor.replacement = nullptr;
        return false;
    }
    if (!PrepareAnimation(replacement)) {
        RestoreOriginalRenderers();
        SetActive(replacement, false);
        DestroyObject(replacement);
        g_actor.replacement = nullptr;
        Log("[model-replace] animation graph setup failed; original retained");
        return false;
    }
    if (!AlignReplacementAnchor(true)) {
        Log("[model-anchor] initial alignment unavailable; copied root pose retained");
    }
    Log("[model-replace] complete actor replacement active original=" +
        ObjectName(g_actor.original) + " replacement=" + ObjectName(replacement) +
        " originalRootActive=true");
    return true;
}

void CaptureActor(void* instance, void* parent, const char* source) {
    if (!instance || g_actor.original == instance) {
        return;
    }
    if (g_actor.original) {
        return;
    }
    g_actor.original = instance;
    g_actor.parent = parent;
    Log(std::string("[model-capture] source=") + source + " name=" +
        ObjectName(instance));
}

void DiscoverExistingLoginActor() {
    if (g_actor.original || !g_host || !g_host->string_new) {
        return;
    }
    const uint64_t now = GetTickCount64();
    if (now < g_next_actor_discovery_tick) {
        return;
    }
    g_next_actor_discovery_tick = now + 250;

    static constexpr std::array<const char*, 4> kActorNames{
        "SK_actor_female(Clone)",
        "SK_actor_female",
        "SK_actor_male(Clone)",
        "SK_actor_male",
    };
    for (const char* name : kActorNames) {
        void* managed_name = g_host->string_new(g_host->context, name);
        if (!managed_name) {
            continue;
        }
        void* find_parameters[1]{managed_name};
        void* game_object = Invoke(g_methods.game_object_find, nullptr,
            find_parameters, "GameObject.Find(login actor)");
        if (!game_object) {
            continue;
        }
        void* transform = Invoke(g_methods.game_object_transform, game_object,
            nullptr, "GameObject.get_transform(login actor)");
        void* parent = transform
            ? Invoke(g_methods.transform_parent, transform, nullptr,
                "Transform.get_parent(login actor)")
            : nullptr;
        CaptureActor(game_object, parent, "targeted GameObject.Find");
        return;
    }
}

bool TryGetPlayableTime(PlayableData* playable, double& time) {
    __try {
        time = reinterpret_cast<PlayableGetDoubleFn>(
            g_methods.playable_get_time.pointer)(
            playable,
            const_cast<void*>(g_methods.playable_get_time.method_info));
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

bool TryBeginMixerBlend(PlayableData* mixer, int current_input, int next_input,
    PlayableData* next, double next_time, double speed) {
    __try {
        reinterpret_cast<PlayableWeightFn>(
            g_methods.playable_set_input_weight.pointer)(
            mixer, current_input, 1.0f,
            const_cast<void*>(g_methods.playable_set_input_weight.method_info));
        reinterpret_cast<PlayableWeightFn>(
            g_methods.playable_set_input_weight.pointer)(
            mixer, next_input, 0.0f,
            const_cast<void*>(g_methods.playable_set_input_weight.method_info));
        reinterpret_cast<PlayableDoubleFn>(g_methods.playable_set_time.pointer)(
            next, next_time,
            const_cast<void*>(g_methods.playable_set_time.method_info));
        reinterpret_cast<PlayableDoubleFn>(g_methods.playable_set_speed.pointer)(
            next, speed,
            const_cast<void*>(g_methods.playable_set_speed.method_info));
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

bool TrySetMixerWeights(PlayableData* mixer, int current_input, int next_input,
    float current_weight, float next_weight) {
    __try {
        reinterpret_cast<PlayableWeightFn>(
            g_methods.playable_set_input_weight.pointer)(
            mixer, current_input, current_weight,
            const_cast<void*>(g_methods.playable_set_input_weight.method_info));
        reinterpret_cast<PlayableWeightFn>(
            g_methods.playable_set_input_weight.pointer)(
            mixer, next_input, next_weight,
            const_cast<void*>(g_methods.playable_set_input_weight.method_info));
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

bool TryStopPlayable(PlayableData* playable) {
    __try {
        reinterpret_cast<PlayableDoubleFn>(g_methods.playable_set_speed.pointer)(
            playable, 0.0,
            const_cast<void*>(g_methods.playable_set_speed.method_info));
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

bool TryEvaluatePlayableGraph(float delta_time) {
    __try {
        reinterpret_cast<PlayableGraphEvaluateFn>(
            g_methods.playable_graph_evaluate.pointer)(
            &g_actor.graph, delta_time,
            const_cast<void*>(g_methods.playable_graph_evaluate.method_info));
        return true;
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return false;
    }
}

bool TickFinalMixer() {
    if (!g_actor.mixer_configured || !g_actor.mixer.handle ||
        !g_actor.mixer_playables[0].handle ||
        !g_actor.mixer_playables[1].handle) {
        return false;
    }
    const ModelConfiguration configuration = ConfigurationSnapshot();
    const int current_input = g_actor.mixer_active_input == 1 ? 1 : 0;
    const int next_input = 1 - current_input;
    PlayableData& current = g_actor.mixer_playables[current_input];
    PlayableData& next = g_actor.mixer_playables[next_input];
    double current_time = -1.0;
    if (!TryGetPlayableTime(&current, current_time) || current_time < 0.0) {
        return false;
    }
    g_actor.phase_elapsed = current_time;

    const double loop_end = std::min(
        static_cast<double>(configuration.loop_end),
        static_cast<double>(g_actor.phase_length));
    const double loop_start = configuration.loop_start < loop_end
        ? static_cast<double>(configuration.loop_start) : 0.0;
    const double loop_length = loop_end - loop_start;
    const double fade_clip_duration = std::min(
        static_cast<double>(configuration.crossfade_duration) *
            static_cast<double>(configuration.clips[3].speed),
        loop_length * 0.5);
    const double transition_start = loop_end - fade_clip_duration;
    if (loop_length <= 0.0 || fade_clip_duration <= 0.0) {
        return false;
    }

    bool blend_started = false;
    double next_time = -1.0;
    if (!g_actor.mixer_blend_active) {
        if (current_time + 0.0005 < transition_start) {
            return true;
        }
        next_time = std::min(
            loop_start + (current_time - transition_start),
            loop_start + fade_clip_duration);
        if (!TryBeginMixerBlend(&g_actor.mixer, current_input, next_input,
                &next, next_time, configuration.clips[3].speed)) {
            return false;
        }
        g_actor.mixer_blend_active = true;
        blend_started = true;
    }

    const double normalized = std::clamp(
        (current_time - transition_start) / fade_clip_duration, 0.0, 1.0);
    const double eased = normalized * normalized * (3.0 - 2.0 * normalized);
    if (!TrySetMixerWeights(&g_actor.mixer, current_input, next_input,
            static_cast<float>(1.0 - eased), static_cast<float>(eased))) {
        return false;
    }
    if (blend_started) {
        TryEvaluatePlayableGraph(0.0f);
        char message[320]{};
        std::snprintf(message, sizeof(message),
            "[model-crossfade] cycle=%u begin from=%d to=%d "
            "sourceTime=%.3f targetTime=%.3f fade=%.3f",
            g_actor.mixer_blend_count + 1, current_input, next_input,
            current_time, next_time,
            static_cast<double>(configuration.crossfade_duration));
        Log(message);
    }
    if (normalized + 0.0005 < 1.0) {
        return true;
    }

    if (!TrySetMixerWeights(&g_actor.mixer, current_input, next_input,
            0.0f, 1.0f) || !TryStopPlayable(&current)) {
        return false;
    }
    double completed_time = loop_start + fade_clip_duration;
    TryGetPlayableTime(&next, completed_time);
    g_actor.mixer_active_input = next_input;
    g_actor.mixer_blend_active = false;
    g_actor.phase_elapsed = completed_time;
    ++g_actor.mixer_blend_count;
    if (g_actor.mixer_blend_count <= 4 ||
        g_actor.mixer_blend_count % 30 == 0) {
        char message[256]{};
        std::snprintf(message, sizeof(message),
            "[model-crossfade] cycle=%u complete active=%d time=%.3f",
            g_actor.mixer_blend_count, g_actor.mixer_active_input,
            completed_time);
        Log(message);
    }
    return true;
}

void ProcessSequence(float delta_time) {
    if (!g_actor.graph.handle || g_actor.phase == SequencePhase::None) {
        return;
    }
    if (g_actor.pending != SequencePhase::None) {
        const SequencePhase next = g_actor.pending;
        StartPhase(next, "login-state");
        return;
    }
    const ModelConfiguration configuration = ConfigurationSnapshot();
    const size_t index = static_cast<size_t>(g_actor.phase);
    const float speed = configuration.clips[index].speed;
    if (g_actor.phase_length <= 0.0f) {
        return;
    }
    g_actor.phase_elapsed += static_cast<double>(std::max(delta_time, 0.0f)) *
        static_cast<double>(speed);
    UpdatePhaseFacing();
    if (g_actor.phase == SequencePhase::Final &&
        configuration.use_crossfade) {
        if (!TickFinalMixer() && !g_actor.mixer_fault_logged) {
            g_actor.mixer_fault_logged = true;
            Log("[model-crossfade] mixer update failed; animation held");
        }
        return;
    }
    if (g_actor.phase_elapsed + 0.001f < g_actor.phase_length) {
        return;
    }
    bool phase_loops = configuration.clips[index].loop;
    if (g_actor.phase == SequencePhase::Final) {
        phase_loops = configuration.force_loop ||
            (configuration.clips[index].loop &&
                configuration.final_native_loop);
    }
    if (phase_loops) {
        while (g_actor.phase_elapsed >= g_actor.phase_length) {
            g_actor.phase_elapsed -= g_actor.phase_length;
        }
        double playable_time = -1.0;
        const bool readable = TryGetPlayableTime(
            &g_actor.playable, playable_time);
        char message[256]{};
        std::snprintf(message, sizeof(message),
            "[model-sequence] native loop phase=%s playableTime=%.3f readable=%s",
            PhaseName(g_actor.phase), playable_time,
            readable ? "true" : "false");
        Log(message);
        return;
    }
    if (g_actor.phase == SequencePhase::SitSpecial) {
        StartPhase(SequencePhase::SitLoop, "clip-complete");
    } else if (g_actor.phase == SequencePhase::SitToWalk) {
        StartPhase(SequencePhase::Final, "clip-complete");
    }
}

void TryActivate() {
    const ModelConfiguration configuration = ConfigurationSnapshot();
    if (!configuration.enabled) {
        if (g_actor.replacement) {
            CleanupScene("configuration-disabled");
        }
        return;
    }
    if (g_cleanup_requested.exchange(false)) {
        CleanupScene("configuration-changed");
        ReleaseAssets();
    }
    if (g_loaded_revision.load() != 0 &&
        g_loaded_revision.load() != g_configuration_revision.load()) {
        CleanupScene("asset-selection-changed");
        ReleaseAssets();
    }
    DiscoverExistingLoginActor();
    if (!LoadConfiguredAssets()) {
        return;
    }
    InstantiateReplacement();
}

void QueuePhase(void* controller, SequencePhase phase, const char* source) {
    if (!controller) {
        return;
    }
    if (!g_actor.controller) {
        g_actor.controller = controller;
    }
    if (g_actor.controller != controller) {
        return;
    }
    g_requested_phase.store(phase);
    if (phase == SequencePhase::SitLoop && !g_actor.replacement &&
        !g_actor.gate_requested) {
        g_actor.gate_requested = true;
        g_actor.gate_start_tick = GetTickCount64();
        g_actor.gate_timed_out = false;
    }
    if (g_actor.last_requested == phase) {
        return;
    }
    g_actor.last_requested = phase;
    if (g_actor.replacement) {
        g_actor.pending = phase;
    }
    Log(std::string("[model-sequence] queued phase=") + PhaseName(phase) +
        " source=" + (source ? source : "unknown") +
        " replacementReady=" + (g_actor.replacement ? "true" : "false"));
}

void __fastcall LoginBindHook(void* instance, void* method) {
    g_requested_phase.store(SequencePhase::SitLoop);
    if (g_original_login_bind) {
        g_original_login_bind(instance, method);
    }
    TryActivate();
}

void* __fastcall CloneWithParentHook(void* original, void* parent,
    bool world_position_stays, void* method) {
    void* instance = g_original_clone_with_parent
        ? g_original_clone_with_parent(original, parent, world_position_stays, method)
        : nullptr;
    if (original && instance && !g_actor.original) {
        const std::string name = ObjectName(original);
        if (name.starts_with("SK_actor_")) {
            CaptureActor(instance, parent, "login Instantiate");
        }
    }
    return instance;
}

void __fastcall InitMainHashHook(void* method) {
    if (g_original_init_main_hash) {
        g_original_init_main_hash(method);
    }
    g_main_hash_ready.store(true);
    Log("[model-resource] Main path hash ready");
    TryActivate();
}

void __fastcall InitInitialHashHook(void* method) {
    g_initial_hash_ready.store(false);
    CleanupScene("Initial path hash rebuild");
    ReleaseAssets();
    if (g_original_init_initial_hash) {
        g_original_init_initial_hash(method);
    }
    g_initial_hash_ready.store(true);
    Log("[model-resource] Initial path hash ready");
    TryActivate();
}

void __fastcall AnimationTickHook(void* instance, float delta_time, void* method) {
    if (!g_actor.controller) {
        g_actor.controller = instance;
    }
    if (g_actor.controller != instance) {
        if (g_original_anim_tick) {
            g_original_anim_tick(instance, delta_time, method);
        }
        return;
    }
    TryActivate();

    const ModelConfiguration configuration = ConfigurationSnapshot();
    const bool replacement_ready = g_actor.replacement &&
        g_actor.graph.handle && g_actor.original_renderers_hidden;
    const bool replacement_loading = configuration.enabled &&
        !replacement_ready && !g_actor.replacement_attempted;
    if (g_actor.gate_requested && replacement_loading) {
        const uint64_t elapsed = g_actor.gate_start_tick == 0
            ? 0 : GetTickCount64() - g_actor.gate_start_tick;
        if (elapsed < kAnimationGateTimeoutMs) {
            if (!g_actor.tick_held) {
                g_actor.tick_held = true;
                Log("[model-gate] holding A1 Tick until replacement is ready");
            }
            return;
        }
        g_actor.tick_held = false;
        if (!g_actor.gate_timed_out) {
            g_actor.gate_timed_out = true;
            Log("[model-gate] timed out after 5 seconds; original Tick resumed");
        }
    }

    if (g_actor.gate_requested &&
        (replacement_ready || g_actor.replacement_attempted)) {
        const bool was_held = g_actor.tick_held;
        g_actor.gate_requested = false;
        g_actor.tick_held = false;
        g_actor.gate_timed_out = false;
        g_actor.gate_start_tick = 0;
        if (replacement_ready && g_original_anim_reset_a1) {
            g_original_anim_reset_a1(instance, nullptr);
            g_requested_phase.store(SequencePhase::SitLoop);
            g_actor.last_requested = SequencePhase::SitLoop;
            g_actor.pending = SequencePhase::SitLoop;
            Log(was_held
                ? "[model-gate] replacement ready; released Tick and restarted A1"
                : "[model-gate] replacement ready before first Tick; restarted A1");
        } else {
            Log("[model-gate] replacement setup failed; original animation retained");
        }
    }

    if (g_original_anim_tick) {
        g_original_anim_tick(instance, delta_time, method);
    }
    ProcessSequence(delta_time);
    AlignReplacementAnchor(false, delta_time);
}

void __fastcall AnimationReleaseHook(void* instance, void* method) {
    CleanupScene("LoginSceneAnimCtrl.OnRelease");
    g_requested_phase.store(SequencePhase::SitLoop);
    if (g_original_anim_release) {
        g_original_anim_release(instance, method);
    }
}

void __fastcall AnimationChangeStateHook(void* instance, int target, void* method) {
    if (g_original_anim_change_state) {
        g_original_anim_change_state(instance, target, method);
    }
    if (target == 1) {
        QueuePhase(instance, SequencePhase::SitLoop, "LoginSceneAnimCtrl.A1");
    }
}

void __fastcall AnimationResetA1Hook(void* instance, void* method) {
    if (g_original_anim_reset_a1) {
        g_original_anim_reset_a1(instance, method);
    }
    QueuePhase(instance, SequencePhase::SitLoop,
        "LoginSceneAnimCtrl._ResetToA1");
}

void __fastcall AnimationSpecialHook(void* instance, void* method) {
    if (g_original_anim_play_special) {
        g_original_anim_play_special(instance, method);
    }
    QueuePhase(instance, SequencePhase::SitSpecial,
        "LoginSceneAnimCtrl._PlayA1sAndTriggerNext");
}

void __fastcall AnimationTransitionHook(void* instance, void* method) {
    if (g_original_anim_play_transition) {
        g_original_anim_play_transition(instance, method);
    }
    QueuePhase(instance, SequencePhase::SitToWalk,
        "LoginSceneAnimCtrl._PlayA1A2Impl");
}

bool Hook(RuntimeMethod& method, void* detour, void** original) {
    const BE_Result status = g_host->create_hook(g_host->context, kModuleId,
        method.pointer, detour, original);
    if (status != BE_Result_Ok) {
        Log(std::string("[model-hook] failed key=") + method.key +
            " result=" + ResultName(status));
        return false;
    }
    Log(std::string("[model-hook] installed key=") + method.key);
    return true;
}

void ClearOriginals() {
    g_original_login_bind = nullptr;
    g_original_init_main_hash = nullptr;
    g_original_init_initial_hash = nullptr;
    g_original_anim_tick = nullptr;
    g_original_anim_release = nullptr;
    g_original_anim_change_state = nullptr;
    g_original_anim_reset_a1 = nullptr;
    g_original_anim_play_special = nullptr;
    g_original_anim_play_transition = nullptr;
    g_original_clone_with_parent = nullptr;
}

bool InstallHooks() {
    if (g_hooks_installed) {
        g_state.store(ModuleState::Active);
        return true;
    }
    const bool installed =
        Hook(g_methods.login_bind, reinterpret_cast<void*>(&LoginBindHook),
            reinterpret_cast<void**>(&g_original_login_bind)) &&
        Hook(g_methods.init_main_hash, reinterpret_cast<void*>(&InitMainHashHook),
            reinterpret_cast<void**>(&g_original_init_main_hash)) &&
        Hook(g_methods.init_initial_hash, reinterpret_cast<void*>(&InitInitialHashHook),
            reinterpret_cast<void**>(&g_original_init_initial_hash)) &&
        Hook(g_methods.anim_tick, reinterpret_cast<void*>(&AnimationTickHook),
            reinterpret_cast<void**>(&g_original_anim_tick)) &&
        Hook(g_methods.anim_release, reinterpret_cast<void*>(&AnimationReleaseHook),
            reinterpret_cast<void**>(&g_original_anim_release)) &&
        Hook(g_methods.anim_change_state,
            reinterpret_cast<void*>(&AnimationChangeStateHook),
            reinterpret_cast<void**>(&g_original_anim_change_state)) &&
        Hook(g_methods.anim_reset_a1, reinterpret_cast<void*>(&AnimationResetA1Hook),
            reinterpret_cast<void**>(&g_original_anim_reset_a1)) &&
        Hook(g_methods.anim_play_special,
            reinterpret_cast<void*>(&AnimationSpecialHook),
            reinterpret_cast<void**>(&g_original_anim_play_special)) &&
        Hook(g_methods.anim_play_transition,
            reinterpret_cast<void*>(&AnimationTransitionHook),
            reinterpret_cast<void**>(&g_original_anim_play_transition)) &&
        Hook(g_methods.clone_with_parent,
            reinterpret_cast<void*>(&CloneWithParentHook),
            reinterpret_cast<void**>(&g_original_clone_with_parent));
    if (!installed) {
        g_host->release_module_hooks(g_host->context, kModuleId);
        ClearOriginals();
        g_state.store(ModuleState::Failed);
        return false;
    }
    g_hooks_installed = true;
    g_state.store(ModuleState::Active);
    LogState(ModuleState::Active,
        "dynamic login capture, replacement, and four-stage animation active");
    return true;
}

void StopHooks() {
    if (g_hooks_installed && g_host && g_host->release_module_hooks) {
        g_host->release_module_hooks(g_host->context, kModuleId);
    }
    g_hooks_installed = false;
    ClearOriginals();
}

BE_Result BE_CALL Initialize(const BE_HostApiV1* host) {
    if (!host || host->abi_version != BETTER_ENDFIELD_MODULE_ABI_V1 ||
        !host->resolve_method || !host->resolve_class ||
        !host->create_hook || !host->release_module_hooks || !host->runtime_invoke ||
        !host->object_unbox || !host->object_new || !host->string_new ||
        !host->copy_managed_string ||
        !host->gchandle_new || !host->gchandle_free || !host->log) {
        return BE_Result_ContractMismatch;
    }
    g_host = host;
    if (!ResolveRuntimeContract()) {
        return BE_Result_ContractMismatch;
    }
    Log("[model-config] awaiting configuration; dynamic metadata contract only");
    return BE_Result_Ok;
}

BE_Result BE_CALL ConfigurationChanged(const char* text) {
    if (!g_host) {
        return BE_Result_NotReady;
    }
    ModelConfiguration next;
    std::string error;
    if (!ParseConfiguration(text, next, error)) {
        Log("[model-config] rejected: " + error);
        return BE_Result_InvalidArgument;
    }
    const bool enabled = next.enabled;
    const bool diagnostics = next.diagnostics;
    const std::string character = next.character_id;
    {
        std::lock_guard lock(g_configuration_mutex);
        g_configuration = std::move(next);
    }
    g_diagnostics.store(diagnostics);
    g_configuration_revision.fetch_add(1);
    g_cleanup_requested.store(true);
    Log("[model-config] enabled=" + std::string(enabled ? "true" : "false") +
        " character=" + (character.empty() ? "<unknown>" : character) +
        " data-driven=true");

    const ModuleState state = g_state.load();
    if (state == ModuleState::ContractMismatch || state == ModuleState::Failed ||
        state == ModuleState::Stopped) {
        return BE_Result_ContractMismatch;
    }
    if (!enabled) {
        g_state.store(ModuleState::Disabled);
        LogState(ModuleState::Disabled,
            "configuration disabled replacement; cleanup is queued on game thread");
        return BE_Result_Ok;
    }
    return InstallHooks() ? BE_Result_Ok : BE_Result_Failed;
}

void BE_CALL Shutdown() {
    StopHooks();
    ReleaseAssets();
    g_state.store(ModuleState::Stopped);
    LogState(ModuleState::Stopped, "module shutdown");
    g_host = nullptr;
}

const BE_ModuleApiV1 kApi{
    {kModuleId, "Login Model", "2.0.0", BETTER_ENDFIELD_MODULE_ABI_V1},
    &Initialize,
    &ConfigurationChanged,
    &Shutdown};

} // namespace
} // namespace BetterEndfield::Model

BE_EXPORT const BE_ModuleApiV1* BE_CALL BetterEndfield_GetModuleApiV1() {
    return &BetterEndfield::Model::kApi;
}
