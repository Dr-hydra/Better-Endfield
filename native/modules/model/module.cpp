#include "BetterEndfield/ModuleApi.h"

#include <cstdint>

#if defined(_WIN32)
#include <Windows.h>
#else
#include <chrono>
#ifndef __fastcall
#define __fastcall
#endif
#define __try try
#define __except(filter) catch (...)
#define EXCEPTION_EXECUTE_HANDLER 1
static uint64_t GetTickCount64() {
    return static_cast<uint64_t>(std::chrono::duration_cast<std::chrono::milliseconds>(
        std::chrono::steady_clock::now().time_since_epoch()).count());
}
#endif

#include <algorithm>
#include <array>
#include <atomic>
#include <cctype>
#include <cerrno>
#include <cmath>
#include <cstdio>
#include <cstdlib>
#include <cstring>
#include <mutex>
#include <string>
#include <string_view>
#include <utility>
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

struct Color {
    float r;
    float g;
    float b;
    float a;
};

struct Vector2 {
    float x;
    float y;
};

struct Vector4 {
    float x;
    float y;
    float z;
    float w;
};

struct Rect {
    float x;
    float y;
    float width;
    float height;
};

struct ClipConfiguration {
    std::string path;
    std::string label;
    uint64_t path_hash = 0;
    float speed = 1.0f;
    bool loop = false;
};

struct ModelConfiguration {
    bool module_enabled = false;
    bool model_replacement_enabled = false;
    bool logo_theme_enabled = false;
    Color logo_theme_color{1.0f, 0.7882353f, 0.15686275f, 1.0f};
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
    RuntimeMethod login_enter_value_changed;
    RuntimeMethod login_material_animation_late_tick;
    RuntimeMethod canvas_update_perform;
    RuntimeMethod graphic_get_canvas_renderer;
    RuntimeMethod canvas_renderer_get_color;
    RuntimeMethod canvas_renderer_set_color;
    RuntimeMethod canvas_group_get_color;
    RuntimeMethod canvas_group_set_color;
    RuntimeMethod canvas_group_get_alpha;
    RuntimeMethod image_set_sprite;
    RuntimeMethod raw_image_set_texture;
    RuntimeMethod sprite_get_texture;
    RuntimeMethod sprite_get_rect;
    RuntimeMethod sprite_get_pivot;
    RuntimeMethod sprite_get_pixels_per_unit;
    RuntimeMethod sprite_get_border;
    RuntimeMethod sprite_create;
    RuntimeMethod texture_get_width;
    RuntimeMethod texture_get_height;
    RuntimeMethod texture2d_ctor;
    RuntimeMethod texture2d_read_pixels;
    RuntimeMethod texture2d_apply;
    RuntimeMethod texture2d_get_pixels32;
    RuntimeMethod texture2d_set_pixels32;
    RuntimeMethod render_texture_get_temporary;
    RuntimeMethod render_texture_release_temporary;
    RuntimeMethod render_texture_get_active;
    RuntimeMethod render_texture_set_active;
    RuntimeMethod graphics_blit;
    RuntimeMethod login_decorate_tick;
    RuntimeMethod login_decorate_release;
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
    RuntimeMethod component_game_object;
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
    RuntimeMethod behaviour_get_enabled;
    RuntimeMethod graphic_get_color;
    RuntimeMethod graphic_set_color;
    RuntimeMethod graphic_get_material;
    RuntimeMethod graphic_set_material;
    RuntimeMethod graphic_get_main_texture;
    RuntimeMethod image_get_sprite;
    RuntimeMethod raw_image_get_texture;
    RuntimeMethod logo_game_object_find;
    RuntimeMethod object_get_type;
    RuntimeMethod runtime_type_full_name;
    RuntimeMethod material_get_shader;
    RuntimeMethod material_has_property;
    RuntimeMethod material_get_color;
    RuntimeMethod material_set_color;
    RuntimeMethod material_ctor_copy;
    RuntimeMethod animator_rebind;
    RuntimeMethod animator_culling;
    RuntimeMethod animator_update;
    RuntimeMethod animator_initialized;
    RuntimeMethod animator_human;
    RuntimeMethod animator_avatar;
    RuntimeMethod animator_set_avatar;
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
    BE_ResolvedClassV1 graphic{};
    BE_ResolvedClassV1 material{};
    BE_ResolvedClassV1 canvas_group{};
    BE_ResolvedClassV1 texture2d{};
};

struct RuntimeFields {
    BE_ResolvedFieldV1 logo_canvas_group{};
    BE_ResolvedFieldV1 logo_glow_target{};
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

struct LoginBandGraphicState {
    void* graphic = nullptr;
    std::string path;
    std::string type;
    Color last_color{};
    std::string last_material;
    std::string last_shader;
    std::string last_main_texture;
    std::string last_sprite;
    std::string last_raw_texture;
    Color original_color{};
    void* original_material = nullptr;
    void* source_material = nullptr;
    void* themed_material = nullptr;
    std::string material_color_property;
    Color themed_color{};
    bool themed_material_owned = false;
    bool material_remap_attempted = false;
    bool material_remap_applied = false;
    bool theme_target = false;
    bool initialized = false;
    // CanvasRenderer.color multiplies the whole mesh at render time,
    // independently of Graphic.color; captured once so it can be restored.
    Color original_renderer_color{1.0f, 1.0f, 1.0f, 1.0f};
    bool renderer_color_captured = false;
    bool neutral_color_applied = false;
    // One-shot drift diagnostics: which game write-back was observed after
    // the theme had already been applied.
    bool color_drift_logged = false;
    bool material_drift_logged = false;
    bool renderer_drift_logged = false;
    // Sprite currently shown by the game (original) and our neutralized copy.
    void* original_sprite = nullptr;
    void* themed_sprite = nullptr;
    bool is_image = false;
    bool sprite_swap_logged = false;
};

// The band's sprites (`login_deco_line*`, `login_deco_glitch*`) have the
// brand yellow baked into their texels, so no amount of color multiplication
// can reach an arbitrary theme color. Each distinct sprite is copied once into
// a readable RGBA32 texture whose saturated texels are desaturated to their
// brightness; the material tint then supplies the theme. Cached per original
// sprite because the glitch animation cycles sprites on the same Image.
struct LoginBandSpriteCopy {
    // Original Sprite, or original Texture when `raw_texture` is set.
    void* original_sprite = nullptr;
    void* sprite = nullptr;
    void* texture = nullptr;
    bool raw_texture = false;
    std::string name;
};

// This client's CanvasGroup carries an HG-specific `color` that is multiplied
// onto every descendant at render time (the RGB analogue of CanvasGroup.alpha).
// Groups above the band are tracked so their tint can be neutralized/restored.
struct LoginBandGroupState {
    void* group = nullptr;
    std::string path;
    Color original_color{1.0f, 1.0f, 1.0f, 1.0f};
    Color last_color{};
    bool drift_logged = false;
};

struct LoginBandState {
    void* instance = nullptr;
    void* panel_transform = nullptr;
    std::vector<LoginBandGraphicState> graphics;
    std::vector<LoginBandGroupState> groups;
    std::vector<LoginBandSpriteCopy> sprites;
    uint64_t next_discovery_tick = 0;
    bool apply_logged = false;
    bool sprite_contract_logged = false;
};

struct LogoGraphicState {
    void* graphic = nullptr;
    Color original_color{};
    std::string source;
    std::string name;
    // The layered copies behind the black logo (GameLogoRaw/Image*) carry the
    // brand yellow in their sprite texels, exactly like the band; they get the
    // same neutralized-sprite treatment so Graphic.color alone sets the hue.
    bool is_image = false;
    bool is_raw_image = false;
    void* original_sprite = nullptr;
    void* themed_sprite = nullptr;
    bool sprite_swap_logged = false;
};

struct LogoState {
    void* instance = nullptr;
    std::vector<LogoGraphicState> graphics;
    std::vector<LoginBandSpriteCopy> sprites;
    uint64_t next_discovery_tick = 0;
    bool game_logo_found = false;
    bool game_logo_apply_logged = false;
    bool diagnostic_logged = false;
};

const BE_HostApiV1* g_host = nullptr;
RuntimeMethods g_methods;
RuntimeClasses g_classes;
RuntimeFields g_fields;
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
LoginBandState g_login_band;
LogoState g_logo;
// Unity release callbacks can re-enter while a Graphic property call is active.
// Keep cached vector storage alive until the outer theme pass unwinds.
std::atomic_bool g_login_band_applying{false};
std::atomic_bool g_login_band_clear_pending{false};
std::atomic_bool g_login_band_scene_begin_pending{false};
std::atomic_bool g_login_scene_releasing{false};
std::atomic_bool g_login_band_release_skip_logged{false};
std::atomic_uint64_t g_login_band_generation{1};
void* g_model_prefab = nullptr;
uint32_t g_model_prefab_root = 0;
std::array<void*, kClipCount> g_clips{};
std::array<uint32_t, kClipCount> g_clip_roots{};
uint64_t g_next_asset_retry_tick = 0;
uint64_t g_next_actor_discovery_tick = 0;
bool g_assets_ready = false;
bool g_hooks_installed = false;
bool g_model_contract_ready = false;
bool g_logo_contract_ready = false;
bool g_login_band_contract_ready = false;
bool g_model_hooks_installed = false;
bool g_logo_hooks_installed = false;
bool g_login_band_hook_installed = false;
bool g_login_band_animation_hook_installed = false;
bool g_login_band_canvas_hook_installed = false;
bool g_login_band_sprite_contract_ready = false;

using VoidInstanceFn = void(__fastcall*)(void*, void*);
using ObjectArgInstanceFn = void(__fastcall*)(void*, void*, void*);
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
ObjectArgInstanceFn g_original_login_enter_value_changed = nullptr;
TickFn g_original_login_decorate_tick = nullptr;
TickFn g_original_login_material_animation_late_tick = nullptr;
VoidInstanceFn g_original_canvas_update_perform = nullptr;
VoidInstanceFn g_original_login_decorate_release = nullptr;
VoidInstanceFn g_original_init_main_hash = nullptr;
VoidInstanceFn g_original_init_initial_hash = nullptr;
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

bool ParseHexColor(std::string_view value, Color& output) {
    value = Trim(value);
    if (!value.empty() && value.front() == '#') {
        value.remove_prefix(1);
    }
    if (value.size() != 6) {
        return false;
    }
    auto channel = [value](size_t offset, float& result) {
        const std::string text(value.substr(offset, 2));
        char* end = nullptr;
        errno = 0;
        const unsigned long parsed = std::strtoul(text.c_str(), &end, 16);
        if (!end || *end != '\0' || errno == ERANGE || parsed > 0xff) {
            return false;
        }
        result = static_cast<float>(parsed) / 255.0f;
        return true;
    };
    Color parsed{0.0f, 0.0f, 0.0f, 1.0f};
    if (!channel(0, parsed.r) || !channel(2, parsed.g) ||
        !channel(4, parsed.b)) {
        return false;
    }
    output = parsed;
    return true;
}

bool ParseConfiguration(const char* text, ModelConfiguration& output,
    std::string& error) {
    output = {};
    output.clips[0].loop = true;
    output.clips[3].loop = true;
    bool model_switch_present = false;
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
        if (key == "enabled") {
            if (!ParseBoolean(value, output.module_enabled)) {
                error = "enabled must be a boolean";
                return false;
            }
        } else if (key == "model_enabled" ||
            key == "model_replacement_enabled") {
            model_switch_present = true;
            if (!ParseBoolean(value, output.model_replacement_enabled)) {
                error = "model_replacement_enabled must be a boolean";
                return false;
            }
        } else if (key == "logo_theme_enabled") {
            if (!ParseBoolean(value, output.logo_theme_enabled)) {
                error = "logo_theme_enabled must be a boolean";
                return false;
            }
        } else if (key == "logo_theme_color") {
            if (!ParseHexColor(value, output.logo_theme_color)) {
                error = "logo_theme_color must use #RRGGBB";
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

    if (!model_switch_present) {
        output.model_replacement_enabled = output.module_enabled;
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
    if (output.model_replacement_enabled) {
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

bool SameModelSettings(const ModelConfiguration& left,
    const ModelConfiguration& right) {
    if (left.model_replacement_enabled != right.model_replacement_enabled ||
        left.character_id != right.character_id ||
        left.model_path != right.model_path ||
        left.model_path_hash != right.model_path_hash ||
        left.scale != right.scale || left.start_yaw != right.start_yaw ||
        left.turn_duration != right.turn_duration ||
        left.forward_lean_sample != right.forward_lean_sample ||
        left.final_native_loop != right.final_native_loop ||
        left.final_loop != right.final_loop ||
        left.force_loop != right.force_loop ||
        left.use_crossfade != right.use_crossfade ||
        left.loop_start != right.loop_start || left.loop_end != right.loop_end ||
        left.crossfade_duration != right.crossfade_duration) {
        return false;
    }
    for (size_t index = 0; index < left.clips.size(); ++index) {
        const ClipConfiguration& a = left.clips[index];
        const ClipConfiguration& b = right.clips[index];
        if (a.path != b.path || a.label != b.label ||
            a.path_hash != b.path_hash || a.speed != b.speed ||
            a.loop != b.loop) {
            return false;
        }
    }
    return true;
}

bool AnyFeatureEnabled(const ModelConfiguration& configuration) {
    return configuration.model_replacement_enabled ||
        configuration.logo_theme_enabled;
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

bool ResolveField(BE_ResolvedFieldV1& output, const char* key,
    const char* assembly, const char* namespc, const char* klass,
    const char* field, const char* field_type) {
    output = {};
    const BE_FieldDescriptorV1 descriptor{
        assembly, namespc, klass, field, field_type};
    const BE_Result status =
        g_host->resolve_field(g_host->context, &descriptor, &output);
    if (status == BE_Result_Ok && output.field_info && output.offset >= 0) {
        Log(std::string("[model-contract] resolved key=") + key);
        return true;
    }
    Log(std::string("[model-contract] missing key=") + key +
        " result=" + ResultName(status));
    output = {};
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
    // Hook the StringPathHashBinary implementations rather than the static
    // HashStringPathProcessor.Init*PathHash wrappers: the PC build inlines the
    // InitMainPathHash wrapper into GameInitState._ReloadResourceIndexes, which
    // then calls StringPathHashBinary.InitMain directly, so a hook on the
    // wrapper never fires there. Both wrappers tail-call these methods.
    required(Resolve(g_methods.init_main_hash, "resource.main_hash",
        "Common.Beyond.dll", "Beyond.Resource", "StringPathHashBinary",
        "InitMain", nullptr, "System.Void", 0));
    required(Resolve(g_methods.init_initial_hash, "resource.initial_hash",
        "Common.Beyond.dll", "Beyond.Resource", "StringPathHashBinary",
        "InitInit", nullptr, "System.Void", 0));
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
    Resolve(g_methods.animator_set_avatar, "unity.animator.set_avatar",
        "UnityEngine.AnimationModule.dll", "UnityEngine", "Animator",
        "set_avatar", "UnityEngine.Avatar", "System.Void", 1);
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
    g_model_contract_ready = missing == 0;
    if (!g_model_contract_ready) {
        char message[128]{};
        std::snprintf(message, sizeof(message),
            "dynamic model contract missing=%zu", missing);
        Log(std::string("[model-contract] feature=model result=unavailable reason=") +
            message);
    } else {
        Log("[model-contract] feature=model result=ready");
    }

    const bool visual_component_game_object = Resolve(g_methods.component_game_object,
        "unity.component.game_object.visual", "UnityEngine.CoreModule.dll",
        "UnityEngine", "Component", "get_gameObject", nullptr,
        "UnityEngine.GameObject", 0);
    const bool visual_game_object_transform = Resolve(g_methods.game_object_transform,
        "unity.game_object.transform.visual", "UnityEngine.CoreModule.dll",
        "UnityEngine", "GameObject", "get_transform", nullptr,
        "UnityEngine.Transform", 0);
    const bool visual_game_object_components = Resolve(
        g_methods.game_object_get_components,
        "unity.game_object.components_children.visual", "UnityEngine.CoreModule.dll",
        "UnityEngine", "GameObject", "GetComponentsInChildren",
        "System.Type|System.Boolean", "UnityEngine.Component[]", 2);
    const bool visual_game_object_find = Resolve(g_methods.game_object_find,
        "unity.game_object.find.visual", "UnityEngine.CoreModule.dll", "UnityEngine",
        "GameObject", "Find", "System.String", "UnityEngine.GameObject", 1);
    const bool visual_transform_parent = Resolve(g_methods.transform_parent,
        "unity.transform.parent.visual", "UnityEngine.CoreModule.dll", "UnityEngine",
        "Transform", "get_parent", nullptr, "UnityEngine.Transform", 0);
    const bool visual_transform_find = Resolve(g_methods.transform_find_name,
        "unity.transform.find_name.visual", "UnityEngine.CoreModule.dll",
        "UnityEngine", "Transform", "FindTransformWithName",
        "UnityEngine.Transform|System.String", "UnityEngine.Transform", 2);
    const bool visual_array_length = Resolve(g_methods.array_get_length,
        "system.array.length.visual", "mscorlib.dll", "System", "Array",
        "GetLength", "System.Int32", "System.Int32", 1);
    const bool visual_array_value = Resolve(g_methods.array_get_value,
        "system.array.value.visual", "mscorlib.dll", "System", "Array",
        "GetValue", "System.Int32", "System.Object", 1);
    const bool visual_object_name = Resolve(g_methods.object_name,
        "unity.object.name.visual", "UnityEngine.CoreModule.dll", "UnityEngine",
        "Object", "get_name", nullptr, "System.String", 0);
    const bool visual_material_shader = Resolve(g_methods.material_get_shader,
        "unity.material.shader.visual", "UnityEngine.CoreModule.dll",
        "UnityEngine", "Material", "get_shader", nullptr,
        "UnityEngine.Shader", 0);

    size_t logo_missing = 0;
    auto logo_required = [&logo_missing](bool result) {
        if (!result) {
            ++logo_missing;
        }
    };
    logo_required(Resolve(g_methods.login_decorate_tick,
        "login.decorate.tick", "Entry.Beyond.dll", "Beyond.Login",
        "LoginDecorateUI", "Tick", "System.Single", "System.Void", 1));
    logo_required(Resolve(g_methods.login_decorate_release,
        "login.decorate.release", "Entry.Beyond.dll", "Beyond.Login",
        "LoginDecorateUI", "OnRelease", nullptr, "System.Void", 0));
    logo_required(ResolveField(g_fields.logo_canvas_group, "login.decorate.logo",
        "Entry.Beyond.dll", "Beyond.Login", "LoginDecorateUI", "_imgLogo",
        "UnityEngine.CanvasGroup"));
    logo_required(ResolveField(g_fields.logo_glow_target,
        "login.decorate.glow", "Entry.Beyond.dll", "Beyond.Login",
        "LoginDecorateUI", "_targetGlow", "UnityEngine.Animator"));
    logo_required(Resolve(g_methods.component_game_object,
        "unity.component.game_object", "UnityEngine.CoreModule.dll", "UnityEngine",
        "Component", "get_gameObject", nullptr, "UnityEngine.GameObject", 0));
    logo_required(Resolve(g_methods.game_object_get_components,
        "unity.game_object.components_children.logo", "UnityEngine.CoreModule.dll",
        "UnityEngine", "GameObject", "GetComponentsInChildren",
        "System.Type|System.Boolean", "UnityEngine.Component[]", 2));
    logo_required(Resolve(g_methods.array_get_length, "system.array.length.logo",
        "mscorlib.dll", "System", "Array", "GetLength", "System.Int32",
        "System.Int32", 1));
    logo_required(Resolve(g_methods.array_get_value, "system.array.value.logo",
        "mscorlib.dll", "System", "Array", "GetValue", "System.Int32",
        "System.Object", 1));
    logo_required(Resolve(g_methods.graphic_get_color, "unity.graphic.get_color",
        "UnityEngine.UI.dll", "UnityEngine.UI", "Graphic", "get_color", nullptr,
        "UnityEngine.Color", 0));
    logo_required(Resolve(g_methods.graphic_set_color, "unity.graphic.set_color",
        "UnityEngine.UI.dll", "UnityEngine.UI", "Graphic", "set_color",
        "UnityEngine.Color", "System.Void", 1));
    logo_required(ResolveClass(g_classes.graphic, "UnityEngine.UI.dll",
        "UnityEngine.UI", "Graphic", true));
    logo_required(Resolve(g_methods.logo_game_object_find,
        "unity.game_object.find.logo", "UnityEngine.CoreModule.dll",
        "UnityEngine", "GameObject", "Find", "System.String",
        "UnityEngine.GameObject", 1));

    Resolve(g_methods.object_name, "unity.object.name.logo",
        "UnityEngine.CoreModule.dll", "UnityEngine", "Object", "get_name",
        nullptr, "System.String", 0);
    Resolve(g_methods.graphic_get_material, "unity.graphic.material",
        "UnityEngine.UI.dll", "UnityEngine.UI", "Graphic", "get_material",
        nullptr, "UnityEngine.Material", 0);
    Resolve(g_methods.graphic_get_main_texture, "unity.graphic.main_texture",
        "UnityEngine.UI.dll", "UnityEngine.UI", "Graphic", "get_mainTexture",
        nullptr, "UnityEngine.Texture", 0);
    Resolve(g_methods.material_get_shader, "unity.material.shader",
        "UnityEngine.CoreModule.dll", "UnityEngine", "Material", "get_shader",
        nullptr, "UnityEngine.Shader", 0);
    Resolve(g_methods.material_has_property, "unity.material.has_property",
        "UnityEngine.CoreModule.dll", "UnityEngine", "Material", "HasProperty",
        "System.String", "System.Boolean", 1);
    Resolve(g_methods.material_get_color, "unity.material.get_color",
        "UnityEngine.CoreModule.dll", "UnityEngine", "Material", "GetColor",
        "System.String", "UnityEngine.Color", 1);
    g_logo_contract_ready = logo_missing == 0;
    Log(std::string("[model-contract] feature=logo result=") +
        (g_logo_contract_ready ? "ready" : "unavailable") +
        " missing=" + std::to_string(logo_missing));

    size_t login_band_missing = 0;
    auto login_band_required = [&login_band_missing](bool result) {
        if (!result) {
            ++login_band_missing;
        }
    };
    login_band_required(Resolve(g_methods.login_enter_value_changed,
        "login.enter_game.value_changed", "Entry.Beyond.dll", "Beyond.Login",
        "LoginEnterGamePanel", "OnValueChanged", "Beyond.Login.LoginViewModel",
        "System.Void", 1));
    Resolve(g_methods.login_material_animation_late_tick,
        "login.band.material_animation.late_tick", "UI.Beyond.dll", "Beyond.UI",
        "UIMaterialAnimation", "LateTick", "System.Single", "System.Void", 1);
    // Optional: Canvas.willRenderCanvases -> CanvasUpdateRegistry.PerformUpdate
    // runs after every Update/Animator/LateUpdate write of the frame, so a
    // prefix there is the last chance to neutralize game write-backs before
    // the band meshes are rebuilt.
    Resolve(g_methods.canvas_update_perform,
        "login.band.canvas_update.perform", "UnityEngine.UI.dll", "UnityEngine.UI",
        "CanvasUpdateRegistry", "PerformUpdate", nullptr, "System.Void", 0);
    Resolve(g_methods.graphic_get_canvas_renderer,
        "unity.graphic.canvas_renderer.login_band", "UnityEngine.UI.dll",
        "UnityEngine.UI", "Graphic", "get_canvasRenderer", nullptr,
        "UnityEngine.CanvasRenderer", 0);
    Resolve(g_methods.canvas_renderer_get_color,
        "unity.canvas_renderer.get_color.login_band", "UnityEngine.UIModule.dll",
        "UnityEngine", "CanvasRenderer", "GetColor", nullptr,
        "UnityEngine.Color", 0);
    Resolve(g_methods.canvas_renderer_set_color,
        "unity.canvas_renderer.set_color.login_band", "UnityEngine.UIModule.dll",
        "UnityEngine", "CanvasRenderer", "SetColor", "UnityEngine.Color",
        "System.Void", 1);
    // Optional: HG-extended CanvasGroup.color (absent from stock Unity).
    ResolveClass(g_classes.canvas_group, "UnityEngine.UIModule.dll",
        "UnityEngine", "CanvasGroup", true);
    Resolve(g_methods.canvas_group_get_color,
        "unity.canvas_group.get_color.login_band", "UnityEngine.UIModule.dll",
        "UnityEngine", "CanvasGroup", "get_color", nullptr,
        "UnityEngine.Color", 0);
    Resolve(g_methods.canvas_group_set_color,
        "unity.canvas_group.set_color.login_band", "UnityEngine.UIModule.dll",
        "UnityEngine", "CanvasGroup", "set_color", "UnityEngine.Color",
        "System.Void", 1);
    Resolve(g_methods.canvas_group_get_alpha,
        "unity.canvas_group.get_alpha.login_band", "UnityEngine.UIModule.dll",
        "UnityEngine", "CanvasGroup", "get_alpha", nullptr,
        "System.Single", 0);
    // Optional: sprite neutralization (readable copy via Blit + ReadPixels).
    size_t sprite_missing = 0;
    auto sprite_required = [&sprite_missing](bool result) {
        if (!result) {
            ++sprite_missing;
        }
    };
    sprite_required(Resolve(g_methods.image_set_sprite,
        "unity.image.set_sprite.login_band", "UnityEngine.UI.dll",
        "UnityEngine.UI", "Image", "set_sprite", "UnityEngine.Sprite",
        "System.Void", 1));
    // Optional within the optional group: RawImage targets fall back to
    // color-only theming when missing.
    Resolve(g_methods.raw_image_set_texture,
        "unity.raw_image.set_texture.login_band", "UnityEngine.UI.dll",
        "UnityEngine.UI", "RawImage", "set_texture", "UnityEngine.Texture",
        "System.Void", 1);
    sprite_required(Resolve(g_methods.sprite_get_texture,
        "unity.sprite.texture.login_band", "UnityEngine.CoreModule.dll",
        "UnityEngine", "Sprite", "get_texture", nullptr,
        "UnityEngine.Texture2D", 0));
    sprite_required(Resolve(g_methods.sprite_get_rect,
        "unity.sprite.rect.login_band", "UnityEngine.CoreModule.dll",
        "UnityEngine", "Sprite", "get_rect", nullptr, "UnityEngine.Rect", 0));
    sprite_required(Resolve(g_methods.sprite_get_pivot,
        "unity.sprite.pivot.login_band", "UnityEngine.CoreModule.dll",
        "UnityEngine", "Sprite", "get_pivot", nullptr, "UnityEngine.Vector2", 0));
    sprite_required(Resolve(g_methods.sprite_get_pixels_per_unit,
        "unity.sprite.pixels_per_unit.login_band", "UnityEngine.CoreModule.dll",
        "UnityEngine", "Sprite", "get_pixelsPerUnit", nullptr,
        "System.Single", 0));
    sprite_required(Resolve(g_methods.sprite_get_border,
        "unity.sprite.border.login_band", "UnityEngine.CoreModule.dll",
        "UnityEngine", "Sprite", "get_border", nullptr, "UnityEngine.Vector4", 0));
    sprite_required(Resolve(g_methods.sprite_create,
        "unity.sprite.create.login_band", "UnityEngine.CoreModule.dll",
        "UnityEngine", "Sprite", "Create",
        "UnityEngine.Texture2D|UnityEngine.Rect|UnityEngine.Vector2|System.Single|"
        "System.UInt32|UnityEngine.SpriteMeshType|UnityEngine.Vector4",
        "UnityEngine.Sprite", 7));
    sprite_required(Resolve(g_methods.texture_get_width,
        "unity.texture.width.login_band", "UnityEngine.CoreModule.dll",
        "UnityEngine", "Texture", "get_width", nullptr, "System.Int32", 0));
    sprite_required(Resolve(g_methods.texture_get_height,
        "unity.texture.height.login_band", "UnityEngine.CoreModule.dll",
        "UnityEngine", "Texture", "get_height", nullptr, "System.Int32", 0));
    sprite_required(ResolveClass(g_classes.texture2d, "UnityEngine.CoreModule.dll",
        "UnityEngine", "Texture2D", false));
    sprite_required(Resolve(g_methods.texture2d_ctor,
        "unity.texture2d.ctor.login_band", "UnityEngine.CoreModule.dll",
        "UnityEngine", "Texture2D", ".ctor",
        "System.Int32|System.Int32|UnityEngine.TextureFormat|System.Boolean",
        "System.Void", 4));
    sprite_required(Resolve(g_methods.texture2d_read_pixels,
        "unity.texture2d.read_pixels.login_band", "UnityEngine.CoreModule.dll",
        "UnityEngine", "Texture2D", "ReadPixels",
        "UnityEngine.Rect|System.Int32|System.Int32", "System.Void", 3));
    sprite_required(Resolve(g_methods.texture2d_apply,
        "unity.texture2d.apply.login_band", "UnityEngine.CoreModule.dll",
        "UnityEngine", "Texture2D", "Apply", nullptr, "System.Void", 0));
    sprite_required(Resolve(g_methods.texture2d_get_pixels32,
        "unity.texture2d.get_pixels32.login_band", "UnityEngine.CoreModule.dll",
        "UnityEngine", "Texture2D", "GetPixels32", nullptr,
        "UnityEngine.Color32[]", 0));
    sprite_required(Resolve(g_methods.texture2d_set_pixels32,
        "unity.texture2d.set_pixels32.login_band", "UnityEngine.CoreModule.dll",
        "UnityEngine", "Texture2D", "SetPixels32", "UnityEngine.Color32[]",
        "System.Void", 1));
    sprite_required(Resolve(g_methods.render_texture_get_temporary,
        "unity.render_texture.get_temporary.login_band",
        "UnityEngine.CoreModule.dll", "UnityEngine", "RenderTexture",
        "GetTemporary", "System.Int32|System.Int32|System.Int32",
        "UnityEngine.RenderTexture", 3));
    sprite_required(Resolve(g_methods.render_texture_release_temporary,
        "unity.render_texture.release_temporary.login_band",
        "UnityEngine.CoreModule.dll", "UnityEngine", "RenderTexture",
        "ReleaseTemporary", "UnityEngine.RenderTexture", "System.Void", 1));
    sprite_required(Resolve(g_methods.render_texture_get_active,
        "unity.render_texture.get_active.login_band",
        "UnityEngine.CoreModule.dll", "UnityEngine", "RenderTexture",
        "get_active", nullptr, "UnityEngine.RenderTexture", 0));
    sprite_required(Resolve(g_methods.render_texture_set_active,
        "unity.render_texture.set_active.login_band",
        "UnityEngine.CoreModule.dll", "UnityEngine", "RenderTexture",
        "set_active", "UnityEngine.RenderTexture", "System.Void", 1));
    sprite_required(Resolve(g_methods.graphics_blit,
        "unity.graphics.blit.login_band", "UnityEngine.CoreModule.dll",
        "UnityEngine", "Graphics", "Blit",
        "UnityEngine.Texture|UnityEngine.RenderTexture", "System.Void", 2));
    g_login_band_sprite_contract_ready = sprite_missing == 0;
    Log(std::string("[model-contract] feature=login-band-sprite result=") +
        (g_login_band_sprite_contract_ready ? "ready" : "unavailable") +
        " missing=" + std::to_string(sprite_missing));
    login_band_required(Resolve(g_methods.object_get_type,
        "system.object.get_type.login_band", "mscorlib.dll", "System", "Object",
        "GetType", nullptr, "System.Type", 0));
    login_band_required(Resolve(g_methods.runtime_type_full_name,
        "system.runtime_type.full_name.login_band", "mscorlib.dll", "System",
        "RuntimeType", "get_FullName", nullptr, "System.String", 0));
    login_band_required(Resolve(g_methods.image_get_sprite,
        "unity.image.sprite.login_band", "UnityEngine.UI.dll", "UnityEngine.UI",
        "Image", "get_sprite", nullptr, "UnityEngine.Sprite", 0));
    login_band_required(Resolve(g_methods.raw_image_get_texture,
        "unity.raw_image.texture.login_band", "UnityEngine.UI.dll",
        "UnityEngine.UI", "RawImage", "get_texture", nullptr,
        "UnityEngine.Texture", 0));
    login_band_required(visual_component_game_object);
    login_band_required(visual_game_object_transform);
    login_band_required(visual_game_object_components);
    login_band_required(visual_game_object_find);
    login_band_required(visual_transform_parent);
    login_band_required(visual_transform_find);
    login_band_required(visual_array_length);
    login_band_required(visual_array_value);
    login_band_required(visual_object_name);
    login_band_required(Resolve(g_methods.graphic_get_color,
        "unity.graphic.get_color.login_band", "UnityEngine.UI.dll",
        "UnityEngine.UI", "Graphic", "get_color", nullptr,
        "UnityEngine.Color", 0));
    login_band_required(Resolve(g_methods.graphic_set_color,
        "unity.graphic.set_color.login_band", "UnityEngine.UI.dll",
        "UnityEngine.UI", "Graphic", "set_color", "UnityEngine.Color",
        "System.Void", 1));
    login_band_required(Resolve(g_methods.graphic_get_material,
        "unity.graphic.material.login_band", "UnityEngine.UI.dll",
        "UnityEngine.UI", "Graphic", "get_material", nullptr,
        "UnityEngine.Material", 0));
    login_band_required(Resolve(g_methods.graphic_set_material,
        "unity.graphic.set_material.login_band", "UnityEngine.UI.dll",
        "UnityEngine.UI", "Graphic", "set_material",
        "UnityEngine.Material", "System.Void", 1));
    login_band_required(Resolve(g_methods.graphic_get_main_texture,
        "unity.graphic.main_texture.login_band", "UnityEngine.UI.dll",
        "UnityEngine.UI", "Graphic", "get_mainTexture", nullptr,
        "UnityEngine.Texture", 0));
    login_band_required(ResolveClass(g_classes.graphic, "UnityEngine.UI.dll",
        "UnityEngine.UI", "Graphic", true));
    login_band_required(ResolveClass(g_classes.material,
        "UnityEngine.CoreModule.dll", "UnityEngine", "Material", true));
    login_band_required(visual_material_shader);
    login_band_required(Resolve(g_methods.material_has_property,
        "unity.material.has_property.login_band",
        "UnityEngine.CoreModule.dll", "UnityEngine", "Material",
        "HasProperty", "System.String", "System.Boolean", 1));
    login_band_required(Resolve(g_methods.material_set_color,
        "unity.material.set_color.login_band",
        "UnityEngine.CoreModule.dll", "UnityEngine", "Material",
        "SetColor", "System.String|UnityEngine.Color", "System.Void", 2));
    login_band_required(Resolve(g_methods.material_ctor_copy,
        "unity.material.ctor_copy.login_band",
        "UnityEngine.CoreModule.dll", "UnityEngine", "Material", ".ctor",
        "UnityEngine.Material", "System.Void", 1));
    g_login_band_contract_ready = login_band_missing == 0;
    Log(std::string("[model-contract] feature=login-band result=") +
        (g_login_band_contract_ready ? "ready" : "unavailable") +
        " missing=" + std::to_string(login_band_missing));

    if (!g_model_contract_ready && !g_logo_contract_ready &&
        !g_login_band_contract_ready) {
        g_state.store(ModuleState::ContractMismatch, std::memory_order_release);
        LogState(ModuleState::ContractMismatch,
            "model, logo, and login-band contracts are all unavailable");
        return false;
    }
    g_state.store(ModuleState::Ready, std::memory_order_release);
    LogState(ModuleState::Ready,
        "feature contracts resolved independently through IL2CPP metadata");
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

void* FieldObject(const BE_ResolvedFieldV1& field, void* instance) {
    if (!instance || !field.field_info || !g_host ||
        !g_host->field_get_value_object) {
        return nullptr;
    }
    __try {
        return g_host->field_get_value_object(
            g_host->context, field.field_info, instance);
    }
    __except (EXCEPTION_EXECUTE_HANDLER) {
        return nullptr;
    }
}

std::string VisualObjectName(void* object) {
    if (!object || !g_methods.object_name.method_info || !g_host ||
        !g_host->copy_managed_string) {
        return "<unknown>";
    }
    void* managed = Invoke(g_methods.object_name, object, nullptr,
        "Object.get_name");
    char name[256]{};
    return managed && g_host->copy_managed_string(g_host->context, managed,
        name, sizeof(name)) > 0 ? std::string(name) : std::string("<unknown>");
}

std::string ManagedStringText(void* managed, const char* fallback = "<unknown>") {
    if (!managed || !g_host || !g_host->copy_managed_string) {
        return fallback ? fallback : "";
    }
    char text[512]{};
    return g_host->copy_managed_string(g_host->context, managed, text,
        sizeof(text)) > 0 ? std::string(text) : std::string(fallback ? fallback : "");
}

std::string ManagedTypeName(void* object) {
    void* type = object ? Invoke(g_methods.object_get_type, object, nullptr,
        "Object.GetType(login band)") : nullptr;
    void* full_name = type ? Invoke(g_methods.runtime_type_full_name, type, nullptr,
        "RuntimeType.get_FullName(login band)") : nullptr;
    return ManagedStringText(full_name);
}

std::string ColorText(const Color& color);

void* FindNamedGameObject(const char* name) {
    if (!name || !g_host || !g_host->string_new ||
        !g_methods.game_object_find.method_info) {
        return nullptr;
    }
    void* managed_name = g_host->string_new(g_host->context, name);
    void* parameters[1]{managed_name};
    return managed_name ? Invoke(g_methods.game_object_find, nullptr,
        parameters, "GameObject.Find(login visual)") : nullptr;
}

bool SetGraphicColor(void* graphic, const Color& color) {
    Color copy = color;
    void* parameters[1]{&copy};
    return graphic && InvokeVoid(g_methods.graphic_set_color, graphic,
        parameters, "Graphic.set_color");
}

bool GetGraphicColor(void* graphic, Color& color) {
    return graphic && Unbox(Invoke(g_methods.graphic_get_color, graphic,
        nullptr, "Graphic.get_color"), color);
}

void* GetGraphicCanvasRenderer(void* graphic) {
    return graphic && g_methods.graphic_get_canvas_renderer.method_info
        ? Invoke(g_methods.graphic_get_canvas_renderer, graphic, nullptr,
            "Graphic.get_canvasRenderer(login band)") : nullptr;
}

bool GetCanvasRendererColor(void* renderer, Color& color) {
    return renderer && g_methods.canvas_renderer_get_color.method_info &&
        Unbox(Invoke(g_methods.canvas_renderer_get_color, renderer, nullptr,
            "CanvasRenderer.GetColor(login band)"), color);
}

bool SetCanvasRendererColor(void* renderer, const Color& color) {
    Color copy = color;
    void* parameters[1]{&copy};
    return renderer && g_methods.canvas_renderer_set_color.method_info &&
        InvokeVoid(g_methods.canvas_renderer_set_color, renderer, parameters,
            "CanvasRenderer.SetColor(login band)");
}

bool GetCanvasGroupColor(void* group, Color& color) {
    return group && g_methods.canvas_group_get_color.method_info &&
        Unbox(Invoke(g_methods.canvas_group_get_color, group, nullptr,
            "CanvasGroup.get_color(login band)"), color);
}

bool SetCanvasGroupColor(void* group, const Color& color) {
    Color copy = color;
    void* parameters[1]{&copy};
    return group && g_methods.canvas_group_set_color.method_info &&
        InvokeVoid(g_methods.canvas_group_set_color, group, parameters,
            "CanvasGroup.set_color(login band)");
}

bool GetCanvasGroupAlpha(void* group, float& alpha) {
    return group && g_methods.canvas_group_get_alpha.method_info &&
        Unbox(Invoke(g_methods.canvas_group_get_alpha, group, nullptr,
            "CanvasGroup.get_alpha(login band)"), alpha);
}

void* GetImageSprite(void* image) {
    return image && g_methods.image_get_sprite.method_info
        ? Invoke(g_methods.image_get_sprite, image, nullptr,
            "Image.get_sprite(login band)") : nullptr;
}

bool SetImageSprite(void* image, void* sprite) {
    void* parameters[1]{sprite};
    return image && InvokeVoid(g_methods.image_set_sprite, image, parameters,
        "Image.set_sprite(login band)");
}

// Desaturates every saturated texel of a Color32 array in place: the texel
// keeps its brightness (max channel) and alpha, so a flat brand-yellow shape
// becomes a white shape and gray/white texels stay untouched.
// IL2CPP arrays store elements right after the 32-byte header; the array is
// freshly allocated by GetPixels32 and the GC is non-moving.
size_t DesaturateColor32Array(void* array, int expected_count, size_t& opaque) {
    const int count = ManagedArrayLength(array);
    opaque = 0;
    if (!array || count <= 0 || count != expected_count) {
        return 0;
    }
    auto* texels = reinterpret_cast<uint8_t*>(array) + 32;
    size_t changed = 0;
    for (int index = 0; index < count; ++index) {
        uint8_t* texel = texels + static_cast<size_t>(index) * 4;
        if (texel[3] < 16) {
            continue;
        }
        ++opaque;
        const uint8_t r = texel[0];
        const uint8_t g = texel[1];
        const uint8_t b = texel[2];
        const uint8_t peak = std::max(r, std::max(g, b));
        const uint8_t low = std::min(r, std::min(g, b));
        if (peak == 0 || (peak - low) * 5 <= peak) {
            continue;
        }
        texel[0] = peak;
        texel[1] = peak;
        texel[2] = peak;
        ++changed;
    }
    return changed;
}

// Reads `rect` of `source` back into a fresh readable RGBA32 Texture2D and
// desaturates its accent-colored texels. Returns false (nothing allocated)
// when the readback fails or when the visible texels carry no accent color, in
// which case the caller keeps the original asset.
bool CreateNeutralTextureCopy(void* source, const Rect& rect, const char* label,
    void*& texture, size_t& opaque, size_t& changed, int& width, int& height) {
    texture = nullptr;
    opaque = 0;
    changed = 0;
    if (!source || !g_login_band_sprite_contract_ready || !g_host ||
        !g_host->object_new || !g_classes.texture2d.class_info) {
        return false;
    }
    int source_width = 0;
    int source_height = 0;
    if (!Unbox(Invoke(g_methods.texture_get_width, source, nullptr,
            "Texture.get_width(login band)"), source_width) ||
        !Unbox(Invoke(g_methods.texture_get_height, source, nullptr,
            "Texture.get_height(login band)"), source_height)) {
        return false;
    }
    width = static_cast<int>(std::lround(rect.width));
    height = static_cast<int>(std::lround(rect.height));
    if (width <= 0 || height <= 0 || source_width <= 0 || source_height <= 0 ||
        width > 4096 || height > 4096) {
        return false;
    }

    // GPU copy of the (possibly compressed / non-readable) source into a
    // temporary RenderTexture, then a CPU readback of just the wanted rect.
    int depth = 0;
    void* temporary_parameters[3]{&source_width, &source_height, &depth};
    void* render_texture = Invoke(g_methods.render_texture_get_temporary, nullptr,
        temporary_parameters, "RenderTexture.GetTemporary(login band)");
    if (!render_texture) {
        return false;
    }
    void* blit_parameters[2]{source, render_texture};
    const bool blitted = InvokeVoid(g_methods.graphics_blit, nullptr,
        blit_parameters, "Graphics.Blit(login band)");
    void* previous_active = Invoke(g_methods.render_texture_get_active, nullptr,
        nullptr, "RenderTexture.get_active(login band)");
    void* activate_parameters[1]{render_texture};
    const bool activated = blitted && InvokeVoid(g_methods.render_texture_set_active,
        nullptr, activate_parameters, "RenderTexture.set_active(login band)");

    bool read = false;
    if (activated) {
        texture = g_host->object_new(g_host->context, g_classes.texture2d.class_info);
        int texture_format = 4;  // TextureFormat.RGBA32
        bool mip_chain = false;
        void* ctor_parameters[4]{&width, &height, &texture_format, &mip_chain};
        if (!texture || !InvokeVoid(g_methods.texture2d_ctor, texture,
                ctor_parameters, "Texture2D..ctor(login band)")) {
            texture = nullptr;
        }
        Rect read_rect{rect.x, rect.y, static_cast<float>(width),
            static_cast<float>(height)};
        int destination_x = 0;
        int destination_y = 0;
        void* read_parameters[3]{&read_rect, &destination_x, &destination_y};
        read = texture && InvokeVoid(g_methods.texture2d_read_pixels, texture,
            read_parameters, "Texture2D.ReadPixels(login band)");
    }
    void* restore_parameters[1]{previous_active};
    InvokeVoid(g_methods.render_texture_set_active, nullptr, restore_parameters,
        "RenderTexture.set_active(restore, login band)");
    void* release_parameters[1]{render_texture};
    InvokeVoid(g_methods.render_texture_release_temporary, nullptr,
        release_parameters, "RenderTexture.ReleaseTemporary(login band)");
    if (!read) {
        if (texture) {
            DestroyObject(texture);
            texture = nullptr;
        }
        return false;
    }

    void* pixels = Invoke(g_methods.texture2d_get_pixels32, texture, nullptr,
        "Texture2D.GetPixels32(login band)");
    changed = DesaturateColor32Array(pixels, width * height, opaque);
    // Visible texels that are (almost) all gray or white carry no baked accent
    // color; leave the original in place rather than swap in an identical copy.
    if (opaque == 0 || changed * 100 < opaque * 15) {
        DestroyObject(texture);
        texture = nullptr;
        if (g_diagnostics.load()) {
            Log("[login-band-sprite] neutral " + std::string(label ? label : "") +
                " opaque=" + std::to_string(opaque) +
                " colored=" + std::to_string(changed) + " result=kept-original");
        }
        return false;
    }
    void* set_parameters[1]{pixels};
    if (!pixels ||
        !InvokeVoid(g_methods.texture2d_set_pixels32, texture, set_parameters,
            "Texture2D.SetPixels32(login band)") ||
        !InvokeVoid(g_methods.texture2d_apply, texture, nullptr,
            "Texture2D.Apply(login band)")) {
        DestroyObject(texture);
        texture = nullptr;
        return false;
    }
    return true;
}

// Builds a neutralized copy of `sprite` (see LoginBandSpriteCopy). Returns
// false and leaves nothing allocated on any failure; the caller then keeps the
// original sprite on that Image.
bool CreateNeutralSpriteCopy(void* sprite, LoginBandSpriteCopy& output) {
    if (!sprite || !g_login_band_sprite_contract_ready) {
        return false;
    }
    void* source = Invoke(g_methods.sprite_get_texture, sprite, nullptr,
        "Sprite.get_texture(login band)");
    Rect rect{};
    Vector2 pivot{};
    float pixels_per_unit = 100.0f;
    Vector4 border{};
    if (!source ||
        !Unbox(Invoke(g_methods.sprite_get_rect, sprite, nullptr,
            "Sprite.get_rect(login band)"), rect) ||
        !Unbox(Invoke(g_methods.sprite_get_pivot, sprite, nullptr,
            "Sprite.get_pivot(login band)"), pivot) ||
        !Unbox(Invoke(g_methods.sprite_get_pixels_per_unit, sprite, nullptr,
            "Sprite.get_pixelsPerUnit(login band)"), pixels_per_unit) ||
        !Unbox(Invoke(g_methods.sprite_get_border, sprite, nullptr,
            "Sprite.get_border(login band)"), border)) {
        return false;
    }
    const std::string name = VisualObjectName(sprite);
    void* texture = nullptr;
    size_t opaque = 0;
    size_t changed = 0;
    int width = 0;
    int height = 0;
    if (!CreateNeutralTextureCopy(source, rect, ("sprite=" + name).c_str(),
            texture, opaque, changed, width, height)) {
        return false;
    }

    Rect copy_rect{0.0f, 0.0f, static_cast<float>(width),
        static_cast<float>(height)};
    Vector2 normalized_pivot{pivot.x / static_cast<float>(width),
        pivot.y / static_cast<float>(height)};
    uint32_t extrude = 0;
    int mesh_type = 0;  // SpriteMeshType.FullRect
    void* create_parameters[7]{texture, &copy_rect, &normalized_pivot,
        &pixels_per_unit, &extrude, &mesh_type, &border};
    void* copy = Invoke(g_methods.sprite_create, nullptr, create_parameters,
        "Sprite.Create(login band)");
    if (!copy) {
        DestroyObject(texture);
        return false;
    }
    output.original_sprite = sprite;
    output.sprite = copy;
    output.texture = texture;
    output.name = name;
    Log("[login-band-sprite] neutralized sprite=" + output.name +
        " size=" + std::to_string(width) + "x" + std::to_string(height) +
        " opaque=" + std::to_string(opaque) +
        " texelsChanged=" + std::to_string(changed));
    return true;
}

// RawImage variant: the whole texture is copied and the RawImage keeps its
// uvRect. `output.sprite` stays null; `output.texture` is the replacement.
bool CreateNeutralRawTextureCopy(void* source, LoginBandSpriteCopy& output) {
    if (!source || !g_login_band_sprite_contract_ready) {
        return false;
    }
    int source_width = 0;
    int source_height = 0;
    if (!Unbox(Invoke(g_methods.texture_get_width, source, nullptr,
            "Texture.get_width(raw, login band)"), source_width) ||
        !Unbox(Invoke(g_methods.texture_get_height, source, nullptr,
            "Texture.get_height(raw, login band)"), source_height)) {
        return false;
    }
    const std::string name = VisualObjectName(source);
    Rect rect{0.0f, 0.0f, static_cast<float>(source_width),
        static_cast<float>(source_height)};
    void* texture = nullptr;
    size_t opaque = 0;
    size_t changed = 0;
    int width = 0;
    int height = 0;
    if (!CreateNeutralTextureCopy(source, rect, ("texture=" + name).c_str(),
            texture, opaque, changed, width, height)) {
        return false;
    }
    output.original_sprite = source;
    output.sprite = nullptr;
    output.texture = texture;
    output.raw_texture = true;
    output.name = name;
    Log("[login-band-sprite] neutralized texture=" + output.name +
        " size=" + std::to_string(width) + "x" + std::to_string(height) +
        " opaque=" + std::to_string(opaque) +
        " texelsChanged=" + std::to_string(changed));
    return true;
}

// Returns the neutralized copy for `sprite`, creating it on first use.
// Returns nullptr when the copy cannot be produced (sprite stays original).
void* NeutralSpriteFor(std::vector<LoginBandSpriteCopy>& cache, void* sprite) {
    if (!sprite) {
        return nullptr;
    }
    for (const LoginBandSpriteCopy& copy : cache) {
        if (copy.raw_texture) {
            continue;
        }
        if (copy.original_sprite == sprite) {
            return copy.sprite;
        }
        if (copy.sprite == sprite) {
            return sprite;
        }
    }
    LoginBandSpriteCopy copy{};
    if (!CreateNeutralSpriteCopy(sprite, copy)) {
        // Remember the failure so the Image is not retried every tick.
        copy.original_sprite = sprite;
        copy.sprite = nullptr;
        copy.texture = nullptr;
        cache.push_back(std::move(copy));
        return nullptr;
    }
    cache.push_back(std::move(copy));
    return cache.back().sprite;
}

// RawImage counterpart: returns the neutralized texture for `texture`.
void* NeutralRawTextureFor(std::vector<LoginBandSpriteCopy>& cache, void* texture) {
    if (!texture) {
        return nullptr;
    }
    for (const LoginBandSpriteCopy& copy : cache) {
        if (!copy.raw_texture) {
            continue;
        }
        if (copy.original_sprite == texture) {
            return copy.texture;
        }
        if (copy.texture == texture) {
            return texture;
        }
    }
    LoginBandSpriteCopy copy{};
    if (!CreateNeutralRawTextureCopy(texture, copy)) {
        copy.original_sprite = texture;
        copy.sprite = nullptr;
        copy.texture = nullptr;
        copy.raw_texture = true;
        cache.push_back(std::move(copy));
        return nullptr;
    }
    cache.push_back(std::move(copy));
    return cache.back().texture;
}

void* GetRawImageTexture(void* raw_image) {
    return raw_image && g_methods.raw_image_get_texture.method_info
        ? Invoke(g_methods.raw_image_get_texture, raw_image, nullptr,
            "RawImage.get_texture(login band)") : nullptr;
}

bool SetRawImageTexture(void* raw_image, void* texture) {
    void* parameters[1]{texture};
    return raw_image && g_methods.raw_image_set_texture.method_info &&
        InvokeVoid(g_methods.raw_image_set_texture, raw_image, parameters,
            "RawImage.set_texture(login band)");
}

void DestroyNeutralSprites(std::vector<LoginBandSpriteCopy>& cache,
    const char* owner) {
    size_t destroyed = 0;
    for (const LoginBandSpriteCopy& copy : cache) {
        if (g_login_scene_releasing.load(std::memory_order_acquire)) {
            break;
        }
        if (copy.sprite) {
            DestroyObject(copy.sprite);
            ++destroyed;
        }
        if (copy.texture) {
            DestroyObject(copy.texture);
        }
    }
    if (destroyed > 0) {
        Log(std::string("[") + owner + "-sprite] copies destroyed=" +
            std::to_string(destroyed));
    }
    cache.clear();
}

// Shared by the band and the logo: keeps an Image on the neutralized copy of
// whatever sprite the game currently shows on it. Sprites cycled in by the
// game are treated as new originals, not as drift to revert. Returns true when
// the Image now shows one of our copies.
bool MaintainNeutralSprite(std::vector<LoginBandSpriteCopy>& cache, void* image,
    void*& original_sprite, void*& themed_sprite, bool& swap_logged,
    const char* owner, const std::string& label, const char* phase) {
    if (!image || !g_login_band_sprite_contract_ready) {
        return false;
    }
    void* current = GetImageSprite(image);
    if (!current) {
        return false;
    }
    if (current == themed_sprite) {
        return true;
    }
    void* neutral = NeutralSpriteFor(cache, current);
    if (neutral == current) {
        // Already one of our copies (e.g. re-applied by the game).
        themed_sprite = current;
        return true;
    }
    original_sprite = current;
    if (!neutral) {
        themed_sprite = nullptr;
        return false;
    }
    if (!SetImageSprite(image, neutral)) {
        return false;
    }
    themed_sprite = neutral;
    if (g_diagnostics.load() && !swap_logged) {
        swap_logged = true;
        Log(std::string("[") + owner + "-sprite] applied target=" + label +
            " phase=" + (phase ? phase : "unknown") +
            " sprite=" + VisualObjectName(current));
    }
    return true;
}

// RawImage counterpart of MaintainNeutralSprite. GameLogoRaw draws the logo
// texture (`login_logo*`) whose yellow edge layers and "ARKNIGHTS ENDFIELD"
// caption are baked into the texels.
bool MaintainNeutralRawTexture(std::vector<LoginBandSpriteCopy>& cache,
    void* raw_image, void*& original_texture, void*& themed_texture,
    bool& swap_logged, const char* owner, const std::string& label,
    const char* phase) {
    if (!raw_image || !g_login_band_sprite_contract_ready ||
        !g_methods.raw_image_set_texture.method_info) {
        return false;
    }
    void* current = GetRawImageTexture(raw_image);
    if (!current) {
        return false;
    }
    if (current == themed_texture) {
        return true;
    }
    void* neutral = NeutralRawTextureFor(cache, current);
    if (neutral == current) {
        themed_texture = current;
        return true;
    }
    original_texture = current;
    if (!neutral) {
        themed_texture = nullptr;
        return false;
    }
    if (!SetRawImageTexture(raw_image, neutral)) {
        return false;
    }
    themed_texture = neutral;
    if (g_diagnostics.load() && !swap_logged) {
        swap_logged = true;
        Log(std::string("[") + owner + "-sprite] applied target=" + label +
            " phase=" + (phase ? phase : "unknown") +
            " texture=" + VisualObjectName(current));
    }
    return true;
}

void* GetGraphicMaterial(void* graphic) {
    return graphic && g_methods.graphic_get_material.method_info
        ? Invoke(g_methods.graphic_get_material, graphic, nullptr,
            "Graphic.get_material(login band remap)") : nullptr;
}

bool SetGraphicMaterial(void* graphic, void* material) {
    void* parameters[1]{material};
    return graphic && material && InvokeVoid(g_methods.graphic_set_material,
        graphic, parameters, "Graphic.set_material(login band remap)");
}

bool HasMaterialProperty(void* material, const char* property) {
    if (!material || !property || !g_host || !g_host->string_new ||
        !g_methods.material_has_property.method_info) {
        return false;
    }
    void* managed = g_host->string_new(g_host->context, property);
    void* parameters[1]{managed};
    bool present = false;
    return managed && Unbox(Invoke(g_methods.material_has_property, material,
        parameters, "Material.HasProperty(login band remap)"), present) && present;
}

std::string SelectMaterialColorProperty(void* material) {
    constexpr std::array<const char*, 4> properties{
        "_Color", "_TintColor", "_BaseColor", "_GlowColor"};
    for (const char* property : properties) {
        if (g_login_scene_releasing.load(std::memory_order_acquire)) {
            return {};
        }
        if (HasMaterialProperty(material, property)) {
            return property;
        }
    }
    return {};
}

bool SetMaterialColor(void* material, const std::string& property,
    const Color& color) {
    if (!material || property.empty() || !g_host || !g_host->string_new ||
        !g_methods.material_set_color.method_info) {
        return false;
    }
    void* managed_property = g_host->string_new(g_host->context,
        property.c_str());
    Color copy = color;
    void* parameters[2]{managed_property, &copy};
    return managed_property && InvokeVoid(g_methods.material_set_color, material,
        parameters, "Material.SetColor(login band remap)");
}

void* CloneMaterial(void* source) {
    if (!source || !g_classes.material.class_info || !g_host ||
        !g_host->object_new || !g_methods.material_ctor_copy.method_info) {
        return nullptr;
    }
    void* clone = g_host->object_new(g_host->context,
        g_classes.material.class_info);
    if (!clone) {
        return nullptr;
    }
    void* parameters[1]{source};
    if (!InvokeVoid(g_methods.material_ctor_copy, clone, parameters,
            "Material..ctor(Material) login band remap")) {
        if (!g_login_scene_releasing.load(std::memory_order_acquire)) {
            DestroyObject(clone);
        }
        return nullptr;
    }
    return clone;
}

bool RgbDiffer(const Color& left, const Color& right) {
    constexpr float epsilon = 0.0005f;
    return std::fabs(left.r - right.r) > epsilon ||
        std::fabs(left.g - right.g) > epsilon ||
        std::fabs(left.b - right.b) > epsilon;
}

std::string ColorText(const Color& color) {
    char value[96]{};
    std::snprintf(value, sizeof(value), "%.3f,%.3f,%.3f,%.3f",
        color.r, color.g, color.b, color.a);
    return value;
}

void LogMaterialColors(void* material) {
    if (!material || !g_diagnostics.load() ||
        !g_methods.material_has_property.method_info ||
        !g_methods.material_get_color.method_info || !g_host ||
        !g_host->string_new) {
        return;
    }
    constexpr std::array<const char*, 4> properties{
        "_Color", "_TintColor", "_EmissionColor", "_GlowColor"};
    for (const char* property : properties) {
        void* managed = g_host->string_new(g_host->context, property);
        void* parameters[1]{managed};
        bool present = false;
        if (!managed || !Unbox(Invoke(g_methods.material_has_property,
                material, parameters, "Material.HasProperty"), present) ||
            !present) {
            continue;
        }
        Color color{};
        if (Unbox(Invoke(g_methods.material_get_color, material, parameters,
                "Material.GetColor"), color)) {
            Log(std::string("[logo-diag] materialProperty=") + property +
                " color=" + ColorText(color));
        }
    }
}

void LogLogoGraphic(void* graphic, const Color& color, int index,
    const char* root_role) {
    if (!g_diagnostics.load()) {
        return;
    }
    void* material = g_methods.graphic_get_material.method_info
        ? Invoke(g_methods.graphic_get_material, graphic, nullptr,
            "Graphic.get_material") : nullptr;
    void* texture = g_methods.graphic_get_main_texture.method_info
        ? Invoke(g_methods.graphic_get_main_texture, graphic, nullptr,
            "Graphic.get_mainTexture") : nullptr;
    void* shader = material && g_methods.material_get_shader.method_info
        ? Invoke(g_methods.material_get_shader, material, nullptr,
            "Material.get_shader") : nullptr;
    Log("[logo-diag] index=" + std::to_string(index) +
        " rootRole=" + (root_role ? root_role : "unknown") +
        " graphic=" + VisualObjectName(graphic) +
        " color=" + ColorText(color) +
        " material=" + VisualObjectName(material) +
        " shader=" + VisualObjectName(shader) +
        " texture=" + VisualObjectName(texture));
    LogMaterialColors(material);
}

size_t AppendLogoGraphicsFromRoot(void* root, const char* root_role) {
    void* graphics = root ? GetComponentsInChildren(
        root, g_classes.graphic.type_object) : nullptr;
    const int count = ManagedArrayLength(graphics);
    if (!root || count <= 0) {
        return 0;
    }
    const size_t before = g_logo.graphics.size();
    for (int index = 0; index < count; ++index) {
        void* graphic = ManagedArrayValue(graphics, index);
        if (std::any_of(g_logo.graphics.begin(), g_logo.graphics.end(),
                [graphic](const LogoGraphicState& state) {
                    return state.graphic == graphic;
                })) {
            continue;
        }
        Color color{};
        if (!graphic || !GetGraphicColor(graphic, color)) {
            continue;
        }
        LogoGraphicState state{};
        state.graphic = graphic;
        state.original_color = color;
        state.source = root_role ? root_role : "unknown";
        state.name = VisualObjectName(graphic);
        const std::string type = ManagedTypeName(graphic);
        state.is_raw_image = type.find("RawImage") != std::string::npos;
        state.is_image = !state.is_raw_image &&
            type.find("Image") != std::string::npos;
        g_logo.graphics.push_back(std::move(state));
        LogLogoGraphic(graphic, color, index, root_role);
    }
    Log("[logo-diag] rootRole=" + std::string(root_role ? root_role : "unknown") +
        " root=" + VisualObjectName(root) +
        " graphicsAdded=" + std::to_string(g_logo.graphics.size() - before));
    return g_logo.graphics.size() - before;
}

size_t AppendLogoGraphics(void* component, const char* root_role) {
    void* root = component
        ? Invoke(g_methods.component_game_object, component, nullptr,
            "Component.get_gameObject(logo decoration)") : nullptr;
    return AppendLogoGraphicsFromRoot(root, root_role);
}

void* FindLogoGameObject(const char* name) {
    if (!name || !g_host || !g_host->string_new ||
        !g_methods.logo_game_object_find.method_info) {
        return nullptr;
    }
    void* managed_name = g_host->string_new(g_host->context, name);
    void* parameters[1]{managed_name};
    return managed_name ? Invoke(g_methods.logo_game_object_find, nullptr,
        parameters, "GameObject.Find(logo target)") : nullptr;
}

bool DiscoverGameLogoRaw() {
    if (g_logo.game_logo_found) {
        return true;
    }
    void* game_logo = FindLogoGameObject("GameLogoRaw");
    if (!game_logo) {
        return false;
    }
    const size_t added = AppendLogoGraphicsFromRoot(
        game_logo, "login-game-logo");
    g_logo.game_logo_found = true;
    Log("[logo-diag] exact target=GameLogoRaw result=found graphicsAdded=" +
        std::to_string(added) +
        " source=login world hierarchy");
    return true;
}

bool DiscoverLogoGraphics(void* instance) {
    void* canvas_group = FieldObject(g_fields.logo_canvas_group, instance);
    void* glow_target = FieldObject(g_fields.logo_glow_target, instance);
    g_logo.graphics.clear();
    g_logo.game_logo_found = false;
    const size_t logo_count = AppendLogoGraphics(canvas_group, "logo");
    const size_t glow_count = AppendLogoGraphics(glow_target, "glow");
    const bool game_logo_found = DiscoverGameLogoRaw();
    if (g_logo.graphics.empty()) {
        const uint64_t now = GetTickCount64();
        if (now >= g_logo.next_discovery_tick) {
            Log("[logo-diag] _imgLogo and _targetGlow have no Graphic children");
            g_logo.next_discovery_tick = now + 1000;
        }
        return false;
    }
    Log("[logo-diag] discovered logoGraphics=" + std::to_string(logo_count) +
        " glowGraphics=" + std::to_string(glow_count) +
        " gameLogoRaw=" + (game_logo_found ? "true" : "false") +
        " source=LoginDecorateUI fields and exact login target");
    g_logo.diagnostic_logged = true;
    return !g_logo.graphics.empty();
}

void RestoreLogoColors(const char* reason) {
    size_t restored = 0;
    for (const LogoGraphicState& state : g_logo.graphics) {
        Color current{};
        Color original = state.original_color;
        if (GetGraphicColor(state.graphic, current)) {
            original.a = current.a;
        }
        if (SetGraphicColor(state.graphic, original)) {
            ++restored;
        }
        if (state.themed_sprite && state.original_sprite) {
            if (state.is_image &&
                GetImageSprite(state.graphic) == state.themed_sprite) {
                SetImageSprite(state.graphic, state.original_sprite);
            } else if (state.is_raw_image &&
                GetRawImageTexture(state.graphic) == state.themed_sprite) {
                SetRawImageTexture(state.graphic, state.original_sprite);
            }
        }
    }
    DestroyNeutralSprites(g_logo.sprites, "logo");
    if (!g_logo.graphics.empty()) {
        Log("[logo-theme] restored=" + std::to_string(restored) +
            " reason=" + (reason ? reason : "unknown"));
    }
    g_logo.graphics.clear();
    g_logo.diagnostic_logged = false;
}

void ApplyLogoTheme(void* instance, const ModelConfiguration& configuration) {
    if (g_logo.instance != instance) {
        g_logo = {};
        g_logo.instance = instance;
    }
    if (!configuration.logo_theme_enabled) {
        RestoreLogoColors("configuration-disabled");
        return;
    }
    if (g_logo.graphics.empty() && !DiscoverLogoGraphics(instance)) {
        return;
    }
    const uint64_t now = GetTickCount64();
    if (!g_logo.game_logo_found && now >= g_logo.next_discovery_tick) {
        DiscoverGameLogoRaw();
        g_logo.next_discovery_tick = now + 500;
    }
    size_t applied = 0;
    size_t game_logo_applied = 0;
    for (LogoGraphicState& state : g_logo.graphics) {
        if (state.is_image) {
            MaintainNeutralSprite(g_logo.sprites, state.graphic,
                state.original_sprite, state.themed_sprite,
                state.sprite_swap_logged, "logo", state.name, "decorate-tick");
        } else if (state.is_raw_image) {
            MaintainNeutralRawTexture(g_logo.sprites, state.graphic,
                state.original_sprite, state.themed_sprite,
                state.sprite_swap_logged, "logo", state.name, "decorate-tick");
        }
        Color current{};
        if (!GetGraphicColor(state.graphic, current)) {
            continue;
        }
        Color replacement = configuration.logo_theme_color;
        replacement.a = current.a;
        if (SetGraphicColor(state.graphic, replacement)) {
            ++applied;
            if (state.source == "login-game-logo") {
                ++game_logo_applied;
            }
        }
    }
    if (applied > 0 && g_logo.diagnostic_logged) {
        Log("[logo-theme] applied=#" +
            [&configuration]() {
                char rgb[7]{};
                std::snprintf(rgb, sizeof(rgb), "%02X%02X%02X",
                    static_cast<int>(std::round(configuration.logo_theme_color.r * 255.0f)),
                    static_cast<int>(std::round(configuration.logo_theme_color.g * 255.0f)),
                    static_cast<int>(std::round(configuration.logo_theme_color.b * 255.0f)));
                return std::string(rgb);
            }() + " graphics=" + std::to_string(applied) +
            " gameLogoRaw=" + std::to_string(game_logo_applied) +
            " alpha=preserved");
        g_logo.diagnostic_logged = false;
    }
    if (game_logo_applied > 0 && !g_logo.game_logo_apply_logged) {
        Log("[logo-theme] exact target=GameLogoRaw applied=" +
            std::to_string(game_logo_applied) + " alpha=preserved");
        g_logo.game_logo_apply_logged = true;
    }
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

std::string HierarchyPath(void* component, void* stop_transform) {
    void* game_object = component ? Invoke(g_methods.component_game_object,
        component, nullptr, "Component.get_gameObject(login band path)") : nullptr;
    void* transform = game_object ? Invoke(g_methods.game_object_transform,
        game_object, nullptr, "GameObject.get_transform(login band path)") : nullptr;
    if (!transform) {
        return "<unknown>";
    }

    std::vector<std::string> parts;
    for (int depth = 0; transform && depth < 64; ++depth) {
        parts.push_back(VisualObjectName(transform));
        if (transform == stop_transform) {
            break;
        }
        transform = Invoke(g_methods.transform_parent, transform, nullptr,
            "Transform.get_parent(login band path)");
    }
    std::reverse(parts.begin(), parts.end());
    std::string path;
    for (const std::string& part : parts) {
        if (!path.empty()) {
            path += '/';
        }
        path += part;
    }
    return path.empty() ? "<unknown>" : path;
}

bool ColorsDiffer(const Color& left, const Color& right) {
    constexpr float epsilon = 0.0005f;
    return std::fabs(left.r - right.r) > epsilon ||
        std::fabs(left.g - right.g) > epsilon ||
        std::fabs(left.b - right.b) > epsilon ||
        std::fabs(left.a - right.a) > epsilon;
}

bool IsLoginBandThemeColor(const Color& color) {
    return color.r >= 0.90f && color.g >= 0.75f && color.b <= 0.35f &&
        color.r - color.g <= 0.35f;
}

bool IsLoginBandThemeTarget(const LoginBandGraphicState& state,
    const Color& color, const std::string& sprite_name,
    const std::string& raw_texture_name) {
    const bool line_resource =
        sprite_name.rfind("login_deco_line", 0) == 0 ||
        raw_texture_name.rfind("login_deco_line", 0) == 0;
    const bool final_line =
        state.path == "EnterGamePanel/MiddlePanel/Line" ||
        state.path == "EnterGamePanel/MiddlePanel/Line/LineLeft";
    const bool adjacent_glitch =
        state.path.rfind("EnterGamePanel/MiddlePanel/Line/LineDecoLeft", 0) == 0 ||
        state.path.rfind("EnterGamePanel/MiddlePanel/Line/LineDecoRight", 0) == 0;
    return IsLoginBandThemeColor(color) || line_resource || final_line ||
        adjacent_glitch;
}

bool SnapshotLoginBandGraphic(LoginBandGraphicState& state, const char* phase,
    bool force_log) {
    Color color{};
    if (!state.graphic || !GetGraphicColor(state.graphic, color)) {
        return false;
    }
    void* material = Invoke(g_methods.graphic_get_material, state.graphic,
        nullptr, "Graphic.get_material(login band)");
    void* shader = material ? Invoke(g_methods.material_get_shader, material,
        nullptr, "Material.get_shader(login band)") : nullptr;
    void* main_texture = Invoke(g_methods.graphic_get_main_texture,
        state.graphic, nullptr, "Graphic.get_mainTexture(login band)");

    void* sprite = nullptr;
    std::string sprite_name = "<not-image>";
    std::string raw_texture_name = "<not-raw-image>";
    if (state.type.find("RawImage") != std::string::npos) {
        raw_texture_name = VisualObjectName(Invoke(g_methods.raw_image_get_texture,
            state.graphic, nullptr, "RawImage.get_texture(login band)"));
    } else if (state.type.find("Image") != std::string::npos) {
        sprite = Invoke(g_methods.image_get_sprite, state.graphic, nullptr,
            "Image.get_sprite(login band)");
        sprite_name = VisualObjectName(sprite);
    }

    const std::string material_name = VisualObjectName(material);
    const std::string shader_name = VisualObjectName(shader);
    const std::string main_texture_name = VisualObjectName(main_texture);
    const bool theme_target = IsLoginBandThemeTarget(
        state, color, sprite_name, raw_texture_name);
    const bool changed = state.initialized &&
        (ColorsDiffer(state.last_color, color) ||
            state.last_material != material_name ||
            state.last_shader != shader_name ||
            state.last_main_texture != main_texture_name ||
            state.last_sprite != sprite_name ||
            state.last_raw_texture != raw_texture_name);
    if (g_diagnostics.load() && (force_log || changed || !state.initialized)) {
        Log("[login-band-diag] phase=" + std::string(phase ? phase : "unknown") +
            " changed=" + (changed ? "true" : "false") +
            " path=" + state.path +
            " type=" + state.type +
            " color=" + ColorText(color) +
            " material=" + material_name +
            " shader=" + shader_name +
            " mainTexture=" + main_texture_name +
            " sprite=" + sprite_name +
            " rawTexture=" + raw_texture_name +
            " themeTarget=" +
                (theme_target ? "true" : "false") +
            " remapProperty=" +
                (theme_target ? SelectMaterialColorProperty(material) : "<none>"));
    }

    if (!state.initialized) {
        state.original_color = color;
        state.original_material = material;
        state.source_material = material;
        state.theme_target = theme_target;
        if (theme_target) {
            state.material_color_property = SelectMaterialColorProperty(material);
        }
    }
    state.last_color = color;
    state.last_material = material_name;
    state.last_shader = shader_name;
    state.last_main_texture = main_texture_name;
    state.last_sprite = sprite_name;
    state.last_raw_texture = raw_texture_name;
    state.initialized = true;
    return changed;
}

size_t AppendLoginBandGraphics(void* root_transform, void* panel_transform) {
    void* root = root_transform ? Invoke(g_methods.component_game_object,
        root_transform, nullptr, "Component.get_gameObject(login band root)") : nullptr;
    void* graphics = root ? GetComponentsInChildren(
        root, g_classes.graphic.type_object) : nullptr;
    const int count = ManagedArrayLength(graphics);
    const size_t before = g_login_band.graphics.size();
    for (int index = 0; index < count; ++index) {
        void* graphic = ManagedArrayValue(graphics, index);
        if (!graphic || std::any_of(g_login_band.graphics.begin(),
                g_login_band.graphics.end(), [graphic](const auto& state) {
                    return state.graphic == graphic;
                })) {
            continue;
        }
        LoginBandGraphicState state{};
        state.graphic = graphic;
        state.path = HierarchyPath(graphic, panel_transform);
        state.type = ManagedTypeName(graphic);
        // Text, raycast-only graphics and the logo subtree (owned by the logo
        // theme) are never band targets.
        if (state.type.find("Text") != std::string::npos ||
            state.type.find("NonDrawing") != std::string::npos ||
            state.path.find("GameLogoRaw") != std::string::npos) {
            continue;
        }
        state.is_image = state.type.find("Image") != std::string::npos &&
            state.type.find("RawImage") == std::string::npos;
        g_login_band.graphics.push_back(std::move(state));
    }
    return g_login_band.graphics.size() - before;
}

// Collects every CanvasGroup on the band's ancestor chain (Line and above, up
// to the canvas root) so its HG color tint can be neutralized like the
// per-Graphic vertex color. Logged in full because this tint is invisible
// through Graphic.color and CanvasRenderer.color.
size_t CaptureLoginBandGroups(void* line_transform) {
    g_login_band.groups.clear();
    if (!line_transform || !g_classes.canvas_group.type_object ||
        !g_methods.canvas_group_get_color.method_info) {
        return 0;
    }
    std::vector<void*> ancestors;
    void* topmost = line_transform;
    for (void* transform = line_transform; transform && ancestors.size() < 64;) {
        ancestors.push_back(transform);
        topmost = transform;
        transform = Invoke(g_methods.transform_parent, transform, nullptr,
            "Transform.get_parent(login band groups)");
        if (g_login_scene_releasing.load(std::memory_order_acquire)) {
            return 0;
        }
    }
    void* root = Invoke(g_methods.component_game_object, topmost, nullptr,
        "Component.get_gameObject(login band group root)");
    void* groups = root ? GetComponentsInChildren(
        root, g_classes.canvas_group.type_object) : nullptr;
    const int count = ManagedArrayLength(groups);
    for (int index = 0; index < count; ++index) {
        if (g_login_scene_releasing.load(std::memory_order_acquire)) {
            break;
        }
        void* group = ManagedArrayValue(groups, index);
        void* game_object = group ? Invoke(g_methods.component_game_object,
            group, nullptr, "Component.get_gameObject(login band group)") : nullptr;
        void* transform = game_object ? Invoke(g_methods.game_object_transform,
            game_object, nullptr, "GameObject.get_transform(login band group)")
            : nullptr;
        if (!transform || std::find(ancestors.begin(), ancestors.end(),
                transform) == ancestors.end()) {
            continue;
        }
        Color color{};
        if (!GetCanvasGroupColor(group, color)) {
            continue;
        }
        float alpha = -1.0f;
        GetCanvasGroupAlpha(group, alpha);
        LoginBandGroupState state{};
        state.group = group;
        state.path = HierarchyPath(group, nullptr);
        state.original_color = color;
        state.last_color = color;
        g_login_band.groups.push_back(std::move(state));
        Log("[login-band-group] path=" + g_login_band.groups.back().path +
            " color=" + ColorText(color) +
            " alpha=" + std::to_string(alpha) +
            " tinted=" + (RgbDiffer(color, Color{1.0f, 1.0f, 1.0f, color.a})
                ? "true" : "false"));
    }
    return g_login_band.groups.size();
}

void RestoreLoginBandTheme(const char* reason);

void FinishPendingLoginBandClear() {
    if (!g_login_band_clear_pending.load(std::memory_order_acquire) ||
        g_login_band_applying.load(std::memory_order_acquire)) {
        return;
    }
    bool expected = true;
    if (!g_login_band_clear_pending.compare_exchange_strong(expected, false,
            std::memory_order_acq_rel, std::memory_order_acquire)) {
        return;
    }
    g_login_band = {};
    const uint64_t generation =
        g_login_band_generation.fetch_add(1, std::memory_order_acq_rel) + 1;
    Log("[login-band] clear completed generation=" +
        std::to_string(generation));
}

void BeginLoginBandScene(const char* source) {
    if (!g_login_scene_releasing.load(std::memory_order_acquire) &&
        !g_login_band_clear_pending.load(std::memory_order_acquire)) {
        return;
    }
    if (g_login_band_applying.load(std::memory_order_acquire)) {
        const bool already_pending =
            g_login_band_scene_begin_pending.exchange(true,
                std::memory_order_acq_rel);
        if (!already_pending) {
            Log("[login-band] scene activation deferred until current apply returns");
        }
        return;
    }
    g_login_band_clear_pending.store(false, std::memory_order_release);
    g_login_band_scene_begin_pending.store(false, std::memory_order_release);
    g_login_band = {};
    const uint64_t generation =
        g_login_band_generation.fetch_add(1, std::memory_order_acq_rel) + 1;
    g_login_scene_releasing.store(false, std::memory_order_release);
    g_login_band_release_skip_logged.store(false, std::memory_order_release);
    Log("[login-band] scene active generation=" + std::to_string(generation) +
        " source=" + std::string(source ? source : "unknown"));
}

class LoginBandApplyScope {
public:
    LoginBandApplyScope() {
        bool expected = false;
        if (!g_login_band_applying.compare_exchange_strong(expected, true,
                std::memory_order_acq_rel, std::memory_order_acquire)) {
            return;
        }
        if (g_login_scene_releasing.load(std::memory_order_acquire)) {
            g_login_band_applying.store(false, std::memory_order_release);
            FinishPendingLoginBandClear();
            return;
        }
        generation_ = g_login_band_generation.load(std::memory_order_acquire);
        active_ = true;
    }

    ~LoginBandApplyScope() {
        if (!active_) {
            return;
        }
        active_ = false;
        g_login_band_applying.store(false, std::memory_order_release);
        FinishPendingLoginBandClear();
        if (g_login_band_scene_begin_pending.exchange(false,
                std::memory_order_acq_rel)) {
            BeginLoginBandScene("deferred scene activation");
        }
    }

    bool IsValid() const {
        return active_ &&
            !g_login_scene_releasing.load(std::memory_order_acquire) &&
            generation_ ==
                g_login_band_generation.load(std::memory_order_acquire);
    }

private:
    bool active_ = false;
    uint64_t generation_ = 0;
};

bool CaptureLoginBand(void* instance, void* panel_game_object, const char* source) {
    if (!panel_game_object ||
        g_login_scene_releasing.load(std::memory_order_acquire)) {
        return false;
    }
    void* panel_transform = Invoke(g_methods.game_object_transform,
        panel_game_object, nullptr, "GameObject.get_transform(login enter panel)");
    if (!panel_transform ||
        g_login_scene_releasing.load(std::memory_order_acquire)) {
        return false;
    }
    const bool cached_targets = std::any_of(g_login_band.graphics.begin(),
        g_login_band.graphics.end(), [](const LoginBandGraphicState& state) {
            return state.theme_target;
        });
    if (g_login_band.panel_transform == panel_transform && cached_targets) {
        if (instance) {
            g_login_band.instance = instance;
        }
        return true;
    }
    if (g_login_band.panel_transform) {
        RestoreLoginBandTheme("re-capture");
    }
    if (g_login_scene_releasing.load(std::memory_order_acquire)) {
        return false;
    }
    void* middle = FindTransformByName(panel_transform, "MiddlePanel");
    void* line = FindTransformByName(middle ? middle : panel_transform, "Line");

    g_login_band = {};
    g_login_band.instance = instance;
    g_login_band.panel_transform = panel_transform;
    // Scan the whole MiddlePanel: besides the Line subtree, the logo frame and
    // corner decorations carry the same baked accent color (e.g. TextDeco
    // under the "ARKNIGHTS ENDFIELD" block).
    void* scan_root = middle ? middle : panel_transform;
    const size_t line_count = AppendLoginBandGraphics(scan_root, panel_transform);
    for (auto& state : g_login_band.graphics) {
        SnapshotLoginBandGraphic(state, "initial", g_diagnostics.load());
    }
    // Promote Images whose sprite texels are themselves accent-colored: the
    // color/name/path rules cannot see baked texture color, but the
    // neutralized copy's texel statistics can.
    size_t promoted = 0;
    for (auto& state : g_login_band.graphics) {
        if (g_login_scene_releasing.load(std::memory_order_acquire)) {
            return false;
        }
        if (state.theme_target || !state.is_image ||
            !g_login_band_sprite_contract_ready) {
            continue;
        }
        void* sprite = GetImageSprite(state.graphic);
        if (!sprite || !NeutralSpriteFor(g_login_band.sprites, sprite)) {
            continue;
        }
        state.theme_target = true;
        state.material_color_property =
            SelectMaterialColorProperty(state.source_material);
        ++promoted;
        Log("[login-band-diag] promoted path=" + state.path +
            " reason=accent-colored-sprite sprite=" + VisualObjectName(sprite));
    }
    const size_t group_count = CaptureLoginBandGroups(line ? line : scan_root);
    const size_t target_count = static_cast<size_t>(std::count_if(
        g_login_band.graphics.begin(), g_login_band.graphics.end(),
        [](const LoginBandGraphicState& state) { return state.theme_target; }));
    Log("[login-band] captured source=" +
        std::string(source ? source : "unknown") +
        " panel=" + VisualObjectName(panel_game_object) +
        " middlePanel=" + (middle ? "found" : "missing") +
        " line=" + (line ? "found" : "missing") +
        " graphics=" + std::to_string(line_count) +
        " themeTargets=" + std::to_string(target_count) +
        " promoted=" + std::to_string(promoted) +
        " canvasGroups=" + std::to_string(group_count));
    return target_count > 0;
}

bool CaptureLoginBandFromInstance(void* instance, const char* source) {
    void* panel_game_object = instance ? Invoke(g_methods.component_game_object,
        instance, nullptr, "Component.get_gameObject(login enter panel)") : nullptr;
    return CaptureLoginBand(instance, panel_game_object, source);
}

void RestoreLoginBandTheme(const char* reason) {
    size_t restored = 0;
    size_t materials_destroyed = 0;
    for (LoginBandGraphicState& state : g_login_band.graphics) {
        if (g_login_scene_releasing.load(std::memory_order_acquire)) {
            break;
        }
        if (!state.theme_target) {
            continue;
        }
        Color current{};
        Color original = state.original_color;
        if (GetGraphicColor(state.graphic, current)) {
            original.a = current.a;
        }
        if (g_login_scene_releasing.load(std::memory_order_acquire)) {
            break;
        }
        if (state.themed_sprite && state.original_sprite &&
            GetImageSprite(state.graphic) == state.themed_sprite) {
            if (g_login_scene_releasing.load(std::memory_order_acquire)) {
                break;
            }
            SetImageSprite(state.graphic, state.original_sprite);
        }
        if (g_login_scene_releasing.load(std::memory_order_acquire)) {
            break;
        }
        void* current_material = state.themed_material
            ? GetGraphicMaterial(state.graphic) : nullptr;
        if (g_login_scene_releasing.load(std::memory_order_acquire)) {
            break;
        }
        if (state.themed_material &&
            current_material == state.themed_material &&
            state.source_material) {
            SetGraphicMaterial(state.graphic, state.source_material);
        }
        if (g_login_scene_releasing.load(std::memory_order_acquire)) {
            break;
        }
        if (SetGraphicColor(state.graphic, original)) {
            ++restored;
        }
        if (g_login_scene_releasing.load(std::memory_order_acquire)) {
            break;
        }
        if (state.renderer_color_captured) {
            void* renderer = GetGraphicCanvasRenderer(state.graphic);
            Color renderer_current{};
            Color renderer_original = state.original_renderer_color;
            if (g_login_scene_releasing.load(std::memory_order_acquire)) {
                break;
            }
            if (renderer && GetCanvasRendererColor(renderer, renderer_current)) {
                renderer_original.a = renderer_current.a;
                if (g_login_scene_releasing.load(std::memory_order_acquire)) {
                    break;
                }
                SetCanvasRendererColor(renderer, renderer_original);
            }
            if (g_login_scene_releasing.load(std::memory_order_acquire)) {
                break;
            }
        }
        if (state.themed_material_owned && state.themed_material) {
            DestroyObject(state.themed_material);
            if (g_login_scene_releasing.load(std::memory_order_acquire)) {
                break;
            }
            ++materials_destroyed;
        }
        state.themed_material = nullptr;
        state.themed_material_owned = false;
        state.material_remap_applied = false;
    }
    for (const LoginBandGroupState& group : g_login_band.groups) {
        if (g_login_scene_releasing.load(std::memory_order_acquire)) {
            break;
        }
        Color current{};
        Color original = group.original_color;
        if (GetCanvasGroupColor(group.group, current)) {
            original.a = current.a;
        }
        if (g_login_scene_releasing.load(std::memory_order_acquire)) {
            break;
        }
        SetCanvasGroupColor(group.group, original);
    }
    DestroyNeutralSprites(g_login_band.sprites, "login-band");
    if (g_login_band.panel_transform) {
        Log("[login-band] restored=" + std::to_string(restored) +
            " materialsDestroyed=" + std::to_string(materials_destroyed) +
            " reason=" +
            std::string(reason ? reason : "unknown"));
    }
    g_login_band = {};
}

// Keeps an Image target on the neutralized copy of whatever sprite the game
// currently shows on it. The glitch decorations cycle through several sprites,
// so a sprite that is neither our copy nor the last known original is treated
// as a new original rather than as drift to be reverted.
void MaintainLoginBandSprite(LoginBandGraphicState& state, const char* phase) {
    if (!state.is_image) {
        return;
    }
    MaintainNeutralSprite(g_login_band.sprites, state.graphic,
        state.original_sprite, state.themed_sprite, state.sprite_swap_logged,
        "login-band", state.path, phase);
}

void ApplyLoginBandTheme(const ModelConfiguration& configuration,
    void* panel_instance = nullptr, const char* phase = "tick") {
    if (g_login_scene_releasing.load(std::memory_order_acquire)) {
        if (!g_login_band_release_skip_logged.exchange(true,
                std::memory_order_acq_rel)) {
            Log("[login-band] tick skipped scene-releasing");
        }
        return;
    }
    LoginBandApplyScope apply_scope;
    if (!apply_scope.IsValid()) {
        return;
    }
    if (!configuration.logo_theme_enabled) {
        RestoreLoginBandTheme("configuration-disabled");
        return;
    }
    if (!g_login_band_contract_ready) {
        return;
    }
    const uint64_t now = GetTickCount64();
    if (panel_instance &&
        (g_login_band.instance != panel_instance ||
            !g_login_band.panel_transform)) {
        if (!CaptureLoginBandFromInstance(panel_instance,
                "LoginEnterGamePanel.OnValueChanged.postfix") ||
            !apply_scope.IsValid()) {
            return;
        }
    }
    if (!g_login_band.panel_transform) {
        if (now < g_login_band.next_discovery_tick) {
            return;
        }
        void* panel = FindNamedGameObject("EnterGamePanel");
        if (!CaptureLoginBand(nullptr, panel, "scene-fallback")) {
            g_login_band.next_discovery_tick = now + 500;
            return;
        }
        if (!apply_scope.IsValid()) {
            return;
        }
    }

    size_t applied = 0;
    size_t remap_failed = 0;
    for (LoginBandGraphicState& state : g_login_band.graphics) {
        if (!apply_scope.IsValid()) {
            break;
        }
        if (!state.theme_target) {
            continue;
        }
        const Color themed_color = configuration.logo_theme_color;
        if (!state.material_remap_attempted) {
            state.material_remap_attempted = true;
            void* source_material = GetGraphicMaterial(state.graphic);
            if (!apply_scope.IsValid()) {
                break;
            }
            state.source_material = source_material;
            const std::string color_property = SelectMaterialColorProperty(
                source_material);
            if (!apply_scope.IsValid()) {
                break;
            }
            state.material_color_property = color_property;
            if (!state.source_material || state.material_color_property.empty()) {
                ++remap_failed;
                if (g_diagnostics.load()) {
                    Log("[login-band-remap] unavailable path=" + state.path +
                        " reason=no-source-material-color-property");
                }
                continue;
            }
            state.themed_material = CloneMaterial(state.source_material);
            if (!apply_scope.IsValid()) {
                break;
            }
            const bool color_applied = state.themed_material &&
                SetMaterialColor(state.themed_material,
                    state.material_color_property, themed_color);
            if (!apply_scope.IsValid()) {
                break;
            }
            const bool material_applied = color_applied &&
                SetGraphicMaterial(state.graphic, state.themed_material);
            if (!apply_scope.IsValid()) {
                break;
            }
            if (!state.themed_material || !color_applied || !material_applied) {
                ++remap_failed;
                if (state.themed_material) {
                    DestroyObject(state.themed_material);
                }
                if (!apply_scope.IsValid()) {
                    break;
                }
                state.themed_material = nullptr;
                state.themed_material_owned = false;
                if (g_diagnostics.load()) {
                    const std::string source_name =
                        VisualObjectName(state.source_material);
                    if (!apply_scope.IsValid()) {
                        break;
                    }
                    Log("[login-band-remap] failed path=" + state.path +
                        " sourceMaterial=" + source_name +
                        " property=" + state.material_color_property);
                }
                continue;
            }
            if (!apply_scope.IsValid()) {
                break;
            }
            state.themed_material_owned = true;
            state.material_remap_applied = true;
            state.themed_color = themed_color;
            ++applied;
            if (g_diagnostics.load()) {
                const std::string source_name =
                    VisualObjectName(state.source_material);
                if (!apply_scope.IsValid()) {
                    break;
                }
                const std::string themed_name =
                    VisualObjectName(state.themed_material);
                if (!apply_scope.IsValid()) {
                    break;
                }
                Log("[login-band-remap] applied path=" + state.path +
                    " sourceMaterial=" + source_name +
                    " themedMaterial=" + themed_name +
                    " property=" + state.material_color_property +
                    " sprite-preserved=true");
            }
        } else if (state.themed_material && state.material_remap_applied) {
            if (RgbDiffer(state.themed_color, themed_color)) {
                const bool color_updated = SetMaterialColor(
                    state.themed_material, state.material_color_property,
                    themed_color);
                if (!apply_scope.IsValid()) {
                    break;
                }
                if (color_updated) {
                    state.themed_color = themed_color;
                    ++applied;
                }
            }
            void* current_material = GetGraphicMaterial(state.graphic);
            if (!apply_scope.IsValid()) {
                break;
            }
            if (current_material != state.themed_material) {
                if (g_diagnostics.load() && !state.material_drift_logged) {
                    state.material_drift_logged = true;
                    const std::string current_name =
                        VisualObjectName(current_material);
                    if (!apply_scope.IsValid()) {
                        break;
                    }
                    Log("[login-band-drift] kind=material phase=" +
                        std::string(phase ? phase : "unknown") +
                        " path=" + state.path + " material=" + current_name);
                }
                SetGraphicMaterial(state.graphic, state.themed_material);
                if (!apply_scope.IsValid()) {
                    break;
                }
            }
        }
        if (state.material_remap_applied) {
            MaintainLoginBandSprite(state, phase);
            if (!apply_scope.IsValid()) {
                break;
            }
        }
        Color current{};
        if (!GetGraphicColor(state.graphic, current)) {
            if (!apply_scope.IsValid()) {
                break;
            }
            continue;
        }
        if (!apply_scope.IsValid()) {
            break;
        }
        Color neutral{1.0f, 1.0f, 1.0f, current.a};
        if (RgbDiffer(current, neutral)) {
            // A yellow RGB here after the first apply means the game rewrote
            // Graphic.color behind us (animation write-back).
            if (g_diagnostics.load() && state.neutral_color_applied &&
                !state.color_drift_logged) {
                state.color_drift_logged = true;
                Log("[login-band-drift] kind=graphic-color phase=" +
                    std::string(phase ? phase : "unknown") +
                    " path=" + state.path + " color=" + ColorText(current));
            }
            if (SetGraphicColor(state.graphic, neutral)) {
                state.neutral_color_applied = true;
            }
            if (!apply_scope.IsValid()) {
                break;
            }
        }
        // CanvasRenderer.color is multiplied onto the mesh at render time and
        // is not visible through Graphic.color; neutralize its RGB as well and
        // keep the alpha the game animates through it.
        void* renderer = GetGraphicCanvasRenderer(state.graphic);
        if (!apply_scope.IsValid()) {
            break;
        }
        Color renderer_color{};
        if (renderer && GetCanvasRendererColor(renderer, renderer_color)) {
            if (!apply_scope.IsValid()) {
                break;
            }
            if (!state.renderer_color_captured) {
                state.original_renderer_color = renderer_color;
                state.renderer_color_captured = true;
            }
            Color renderer_neutral{1.0f, 1.0f, 1.0f, renderer_color.a};
            if (RgbDiffer(renderer_color, renderer_neutral)) {
                if (g_diagnostics.load() && !state.renderer_drift_logged) {
                    state.renderer_drift_logged = true;
                    Log("[login-band-drift] kind=canvas-renderer-color phase=" +
                        std::string(phase ? phase : "unknown") +
                        " path=" + state.path +
                        " color=" + ColorText(renderer_color));
                }
                SetCanvasRendererColor(renderer, renderer_neutral);
                if (!apply_scope.IsValid()) {
                    break;
                }
            }
        } else if (!apply_scope.IsValid()) {
            break;
        }
    }
    // Ancestor CanvasGroup tint: neutralize RGB, keep the animated alpha.
    for (LoginBandGroupState& group : g_login_band.groups) {
        if (!apply_scope.IsValid()) {
            break;
        }
        Color current{};
        if (!GetCanvasGroupColor(group.group, current)) {
            if (!apply_scope.IsValid()) {
                break;
            }
            continue;
        }
        if (!apply_scope.IsValid()) {
            break;
        }
        Color neutral{1.0f, 1.0f, 1.0f, current.a};
        if (RgbDiffer(current, neutral)) {
            if (g_diagnostics.load() && !group.drift_logged &&
                RgbDiffer(current, group.last_color)) {
                group.drift_logged = true;
                Log("[login-band-drift] kind=canvas-group-color phase=" +
                    std::string(phase ? phase : "unknown") +
                    " path=" + group.path + " color=" + ColorText(current));
            }
            SetCanvasGroupColor(group.group, neutral);
            if (!apply_scope.IsValid()) {
                break;
            }
            ++applied;
        }
        group.last_color = neutral;
    }
    if (!apply_scope.IsValid()) {
        Log("[login-band] apply aborted after scene release generation=" +
            std::to_string(g_login_band_generation.load(
                std::memory_order_acquire)));
        return;
    }
    if ((applied > 0 || remap_failed > 0) && !g_login_band.apply_logged) {
        Log("[login-band] theme applied graphics=" + std::to_string(applied) +
            " remapFailed=" + std::to_string(remap_failed) +
            " mode=material-clone sprite-preserved=true alpha=preserved");
        g_login_band.apply_logged = true;
    }
}

void ClearLoginBandState(const char* reason) {
    const std::string_view release_reason = reason ? reason : "";
    if (release_reason.find("OnRelease") != std::string_view::npos) {
        const bool already_releasing =
            g_login_scene_releasing.exchange(true,
                std::memory_order_acq_rel);
        g_login_band_clear_pending.store(true, std::memory_order_release);
        if (!already_releasing) {
            const bool during_apply =
                g_login_band_applying.load(std::memory_order_acquire);
            Log("[login-band] release requested generation=" +
                std::to_string(g_login_band_generation.load(
                    std::memory_order_acquire)) +
                " duringApply=" + (during_apply ? "true" : "false") +
                " reason=" + std::string(release_reason));
        }
        FinishPendingLoginBandClear();
        return;
    }
    if (g_login_band.panel_transform) {
        RestoreLoginBandTheme(reason);
        return;
    }
    g_login_band = {};
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
    if (now < g_next_asset_retry_tick || !g_initial_hash_ready.load()) {
        return false;
    }
    // Wait for the real InitMain hook. Forcing readiness once the
    // resource manager reports initialized loads the prefab before its
    // dependency set is complete; the clone then carries an Animator with
    // avatar=null / human=false (verified on Android 1.5.3).
    if (!g_main_hash_ready.load(std::memory_order_acquire) || !ResourcesReady()) {
        return false;
    }
    const ModelConfiguration configuration = ConfigurationSnapshot();
    if (!configuration.model_replacement_enabled) {
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
    void* animators = GetComponentsInChildren(
        replacement, g_classes.animator.type_object);
    const int animator_count = ManagedArrayLength(animators);
    if (!animators || animator_count <= 0) {
        Log("[model-animation] replacement has no Animator");
        return false;
    }

    struct AnimatorStatus {
        void* animator = nullptr;
        void* avatar = nullptr;
        bool initialized = false;
        bool initialized_readable = false;
        bool human = false;
        bool human_readable = false;
    };
    auto inspect = [](void* animator) {
        AnimatorStatus status{};
        status.animator = animator;
        status.avatar = Invoke(g_methods.animator_avatar, animator, nullptr,
            "Animator.get_avatar");
        status.initialized_readable = Unbox(Invoke(
            g_methods.animator_initialized, animator, nullptr,
            "Animator.get_isInitialized"), status.initialized);
        status.human_readable = Unbox(Invoke(g_methods.animator_human,
            animator, nullptr, "Animator.get_isHuman"), status.human);
        return status;
    };
    auto usable = [](const AnimatorStatus& status) {
        return status.animator && status.avatar && status.initialized_readable &&
            status.initialized && status.human_readable && status.human;
    };
    auto configure = [](void* animator) {
        bool enabled = true;
        bool write_defaults = true;
        int culling = 0;
        int update_mode = kUnscaledGameTime;
        void* enabled_parameters[1]{&enabled};
        void* culling_parameters[1]{&culling};
        void* update_parameters[1]{&update_mode};
        void* rebind_parameters[1]{&write_defaults};
        return InvokeVoid(g_methods.behaviour_set_enabled, animator,
                   enabled_parameters, "Behaviour.set_enabled") &&
            InvokeVoid(g_methods.animator_culling, animator,
                culling_parameters, "Animator.set_cullingMode") &&
            InvokeVoid(g_methods.animator_update, animator,
                update_parameters, "Animator.set_updateMode") &&
            InvokeVoid(g_methods.animator_rebind, animator,
                rebind_parameters, "Animator.Rebind");
    };
    auto log_candidate = [animator_count](const AnimatorStatus& status,
                             int index, const char* source) {
        char readiness[512]{};
        std::snprintf(readiness, sizeof(readiness),
            "[model-animation] %s candidate=%d/%d name=%s initialized=%s "
            "readable=%s human=%s humanReadable=%s avatar=%p",
            source, index + 1, animator_count,
            ObjectName(status.animator).c_str(),
            status.initialized ? "true" : "false",
            status.initialized_readable ? "true" : "false",
            status.human ? "true" : "false",
            status.human_readable ? "true" : "false", status.avatar);
        Log(readiness);
    };

    std::vector<AnimatorStatus> candidates;
    candidates.reserve(static_cast<size_t>(animator_count));
    void* animator = nullptr;
    void* generic_animator = nullptr;
    for (int index = 0; index < animator_count; ++index) {
        void* candidate = ManagedArrayValue(animators, index);
        if (!candidate || !configure(candidate)) {
            Log("[model-animation] candidate setup failed index=" +
                std::to_string(index + 1));
            continue;
        }
        AnimatorStatus status = inspect(candidate);
        log_candidate(status, index, "replacement");
        candidates.push_back(status);
        if (!animator && usable(status)) {
            animator = candidate;
        }
        if (!generic_animator && status.initialized_readable &&
            status.initialized) {
            generic_animator = candidate;
        }
    }

    if (!animator && g_methods.animator_set_avatar.method_info && g_actor.original) {
        void* original_animators = GetComponentsInChildren(
            g_actor.original, g_classes.animator.type_object);
        const int original_count = ManagedArrayLength(original_animators);
        void* fallback_avatar = nullptr;
        for (int index = 0; index < original_count; ++index) {
            AnimatorStatus original_status = inspect(
                ManagedArrayValue(original_animators, index));
            char message[512]{};
            std::snprintf(message, sizeof(message),
                "[model-animation] original candidate=%d/%d name=%s avatar=%p",
                index + 1, original_count,
                ObjectName(original_status.animator).c_str(),
                original_status.avatar);
            Log(message);
            if (!fallback_avatar && original_status.avatar) {
                fallback_avatar = original_status.avatar;
            }
        }
        if (fallback_avatar) {
            for (AnimatorStatus& status : candidates) {
                if (status.avatar) {
                    continue;
                }
                void* avatar_parameters[1]{fallback_avatar};
                if (!InvokeVoid(g_methods.animator_set_avatar, status.animator,
                        avatar_parameters, "Animator.set_avatar") ||
                    !configure(status.animator)) {
                    continue;
                }
                status = inspect(status.animator);
                if (usable(status)) {
                    animator = status.animator;
                    Log("[model-animation] avatar copy fallback applied name=" +
                        ObjectName(animator) + " human=true");
                    break;
                }
            }
        } else {
            Log("[model-animation] avatar copy fallback failed: original actor "
                "has no Animator with Avatar");
        }
    }
    if (!animator && generic_animator) {
        animator = generic_animator;
        Log("[model-animation] no humanoid Avatar available; using initialized "
            "generic Animator name=" + ObjectName(animator));
    }
    if (!animator) {
        Log("[model-animation] no initialized Animator was found; replacement rejected");
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

void RollbackReplacement(void* replacement, const char* reason) {
    DestroyGraph();
    RestoreOriginalRenderers();
    if (replacement) {
        SetActive(replacement, false);
        DestroyObject(replacement);
    }
    g_actor.replacement = nullptr;
    g_actor.replacement_transform = nullptr;
    g_actor.replacement_anchor = nullptr;
    g_actor.anchor_alignment_logged = false;
    Log(std::string("[model-replace] rollback reason=") +
        (reason ? reason : "unknown") +
        " replacementDestroyed=true originalRestored=true");
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
        RollbackReplacement(replacement, "deactivation-failed");
        return false;
    }
    const ModelConfiguration configuration = ConfigurationSnapshot();
    if (!CopyTransformAndLayer(g_actor.original, replacement, configuration.scale)) {
        RollbackReplacement(replacement, "transform-synchronization-failed");
        return false;
    }
    g_actor.replacement = replacement;
    if (!SetActive(replacement, true)) {
        RollbackReplacement(replacement, "activation-failed");
        return false;
    }
    if (!PrepareAnimation(replacement)) {
        RollbackReplacement(replacement, "animation-graph-setup-failed");
        return false;
    }
    if (!DisableOriginalRenderers()) {
        RollbackReplacement(replacement, "original-renderer-hide-failed");
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
    if (!configuration.model_replacement_enabled) {
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

void __fastcall LoginEnterGamePanelValueChangedHook(void* instance, void* value,
    void* method) {
    if (g_original_login_enter_value_changed) {
        g_original_login_enter_value_changed(instance, value, method);
    }
    BeginLoginBandScene("LoginEnterGamePanel.OnValueChanged");
    const ModelConfiguration configuration = ConfigurationSnapshot();
    if (configuration.logo_theme_enabled && g_login_band_contract_ready) {
        ApplyLoginBandTheme(configuration, instance, "value-changed");
    }
}

void __fastcall LoginDecorateTickHook(void* instance, float delta_time,
    void* method) {
    if (g_original_login_decorate_tick) {
        g_original_login_decorate_tick(instance, delta_time, method);
    }
    if (g_login_scene_releasing.load(std::memory_order_acquire)) {
        if (!g_login_band_release_skip_logged.exchange(true,
                std::memory_order_acq_rel)) {
            Log("[login-band] decorate tick skipped scene-releasing");
        }
        return;
    }
    const ModelConfiguration configuration = ConfigurationSnapshot();
    ApplyLogoTheme(instance, configuration);
    ApplyLoginBandTheme(configuration, nullptr, "decorate-tick");
}

void __fastcall LoginMaterialAnimationLateTickHook(void* instance,
    float delta_time, void* method) {
    if (g_original_login_material_animation_late_tick) {
        g_original_login_material_animation_late_tick(instance, delta_time, method);
    }
    const ModelConfiguration configuration = ConfigurationSnapshot();
    if (configuration.logo_theme_enabled && g_login_band_contract_ready) {
        ApplyLoginBandTheme(configuration, nullptr, "material-late-tick");
    }
}

// Prefix on CanvasUpdateRegistry.PerformUpdate (Canvas.willRenderCanvases).
// Runs once per frame after Animator/LateUpdate write-backs and before the
// UI meshes are rebuilt, so the neutralized colors are what actually renders.
// Registering dirty graphics here is legal: m_PerformingGraphicUpdate is
// still false. Only touches a band that an earlier hook already captured.
void __fastcall CanvasUpdatePerformHook(void* instance, void* method) {
    if (g_login_band.panel_transform &&
        !g_login_scene_releasing.load(std::memory_order_acquire) &&
        g_login_band_contract_ready) {
        const ModelConfiguration configuration = ConfigurationSnapshot();
        if (configuration.logo_theme_enabled) {
            ApplyLoginBandTheme(configuration, nullptr, "canvas-pre-render");
        }
    }
    if (g_original_canvas_update_perform) {
        g_original_canvas_update_perform(instance, method);
    }
}

void __fastcall LoginDecorateReleaseHook(void* instance, void* method) {
    ClearLoginBandState("LoginDecorateUI.OnRelease");
    if (g_logo.instance == instance) {
        g_logo = {};
        Log("[logo-theme] LoginDecorateUI released; cached Graphic state cleared");
    }
    if (g_original_login_decorate_release) {
        g_original_login_decorate_release(instance, method);
    }
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
    BeginLoginBandScene("LoginSceneRoot.OnBindToManager");
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
    if (original && instance && !g_actor.original &&
        !g_login_scene_releasing.load(std::memory_order_acquire)) {
        const std::string name = ObjectName(original);
        if (name.starts_with("SK_actor_")) {
            CaptureActor(instance, parent, "login Instantiate");
        }
    }
    return instance;
}

void __fastcall InitMainHashHook(void* instance, void* method) {
    if (g_original_init_main_hash) {
        g_original_init_main_hash(instance, method);
    }
    g_main_hash_ready.store(true);
    Log("[model-resource] Main path hash ready");
    TryActivate();
}

void __fastcall InitInitialHashHook(void* instance, void* method) {
    g_initial_hash_ready.store(false);
    CleanupScene("Initial path hash rebuild");
    ReleaseAssets();
    if (g_original_init_initial_hash) {
        g_original_init_initial_hash(instance, method);
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
    const bool replacement_loading = configuration.model_replacement_enabled &&
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
    // The decorate controller is released before the EnterGamePanel's static
    // band is destroyed. Keep the band material clone alive until the login
    // scene animation controller releases the whole login scene.
    ClearLoginBandState("LoginSceneAnimCtrl.OnRelease");
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
    g_original_login_enter_value_changed = nullptr;
    g_original_login_decorate_tick = nullptr;
    g_original_login_material_animation_late_tick = nullptr;
    g_original_canvas_update_perform = nullptr;
    g_original_login_decorate_release = nullptr;
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

bool InstallModelHooks() {
    if (g_model_hooks_installed) {
        return true;
    }
    if (!g_model_contract_ready) {
        Log("[model-hook] feature=model unavailable; contract is incomplete");
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
        g_hooks_installed = false;
        g_model_hooks_installed = false;
        g_logo_hooks_installed = false;
        g_login_band_hook_installed = false;
        g_login_band_animation_hook_installed = false;
        g_login_band_canvas_hook_installed = false;
        g_state.store(ModuleState::Failed);
        return false;
    }
    g_model_hooks_installed = true;
    g_hooks_installed = true;
    Log("[model-hook] feature=model active");
    return true;
}

void InstallLogoHooks() {
    if (g_logo_hooks_installed) {
        return;
    }
    if (!g_logo_contract_ready) {
        Log("[model-hook] feature=logo unavailable; contract is incomplete");
        return;
    }
    if (!Hook(g_methods.login_decorate_tick,
            reinterpret_cast<void*>(&LoginDecorateTickHook),
            reinterpret_cast<void**>(&g_original_login_decorate_tick))) {
        return;
    }
    g_logo_hooks_installed = true;
    g_hooks_installed = true;
    if (!Hook(g_methods.login_decorate_release,
            reinterpret_cast<void*>(&LoginDecorateReleaseHook),
            reinterpret_cast<void**>(&g_original_login_decorate_release))) {
        Log("[model-hook] logo cleanup hook unavailable; Tick hook remains active");
    }
    Log("[model-hook] feature=logo active");
}

void InstallLoginBandHook() {
    if (g_login_band_hook_installed) {
        return;
    }
    if (!g_login_band_contract_ready) {
        Log("[model-hook] feature=login-band unavailable; contract is incomplete");
        return;
    }
    if (!Hook(g_methods.login_enter_value_changed,
            reinterpret_cast<void*>(&LoginEnterGamePanelValueChangedHook),
            reinterpret_cast<void**>(&g_original_login_enter_value_changed))) {
        return;
    }
    g_login_band_hook_installed = true;
    g_hooks_installed = true;
    if (g_methods.login_material_animation_late_tick.method_info &&
        Hook(g_methods.login_material_animation_late_tick,
            reinterpret_cast<void*>(&LoginMaterialAnimationLateTickHook),
            reinterpret_cast<void**>(&g_original_login_material_animation_late_tick))) {
        g_login_band_animation_hook_installed = true;
        Log("[model-hook] login-band post-animation hook active");
    } else {
        Log("[model-hook] login-band post-animation hook unavailable; value hook remains active");
    }
    if (g_methods.canvas_update_perform.method_info &&
        Hook(g_methods.canvas_update_perform,
            reinterpret_cast<void*>(&CanvasUpdatePerformHook),
            reinterpret_cast<void**>(&g_original_canvas_update_perform))) {
        g_login_band_canvas_hook_installed = true;
        Log("[model-hook] login-band canvas pre-render hook active");
    } else {
        Log("[model-hook] login-band canvas pre-render hook unavailable; tick hooks remain active");
    }
    Log("[model-hook] feature=login-band active mode=material-clone sprite-preserved alpha-preserved");
}

bool InstallHooks(const ModelConfiguration& configuration) {
    if (configuration.model_replacement_enabled && !InstallModelHooks()) {
        return false;
    }
    if (configuration.logo_theme_enabled) {
        InstallLogoHooks();
        InstallLoginBandHook();
    }
    if (g_hooks_installed) {
        g_state.store(ModuleState::Active);
        LogState(ModuleState::Active,
            "requested opening-screen feature hooks are active");
    } else {
        g_state.store(ModuleState::Ready);
        LogState(ModuleState::Ready,
            "requested feature contracts were unavailable; module remains isolated");
    }
    return true;
}

void StopHooks() {
    if (g_hooks_installed && g_host && g_host->release_module_hooks) {
        g_host->release_module_hooks(g_host->context, kModuleId);
    }
    g_hooks_installed = false;
    g_model_hooks_installed = false;
    g_logo_hooks_installed = false;
    g_login_band_hook_installed = false;
    g_login_band_animation_hook_installed = false;
    g_login_band_canvas_hook_installed = false;
    ClearOriginals();
}

BE_Result BE_CALL Initialize(const BE_HostApiV1* host) {
    if (!host || host->abi_version != BETTER_ENDFIELD_MODULE_ABI_V1 ||
        !host->resolve_method || !host->resolve_field || !host->resolve_class ||
        !host->create_hook || !host->release_module_hooks || !host->runtime_invoke ||
        !host->object_unbox || !host->object_new || !host->string_new ||
        !host->copy_managed_string || !host->field_get_value_object ||
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
    const bool enabled = AnyFeatureEnabled(next);
    const bool diagnostics = next.diagnostics;
    const std::string character = next.character_id;
    const bool model_enabled = next.model_replacement_enabled;
    const bool logo_enabled = next.logo_theme_enabled;
    bool model_changed = false;
    {
        std::lock_guard lock(g_configuration_mutex);
        model_changed = !SameModelSettings(g_configuration, next);
        g_configuration = std::move(next);
    }
    g_diagnostics.store(diagnostics);
    if (model_changed) {
        g_configuration_revision.fetch_add(1);
        g_cleanup_requested.store(true);
    }
    Log("[model-config] enabled=" + std::string(enabled ? "true" : "false") +
        " replacement=" + (model_enabled ? "true" : "false") +
        " logo=" + (logo_enabled ? "true" : "false") +
        " character=" + (character.empty() ? "<unknown>" : character) +
        " modelChanged=" + (model_changed ? "true" : "false") +
        " data-driven=true");

    const ModuleState state = g_state.load();
    if (state == ModuleState::Failed || state == ModuleState::Stopped) {
        return BE_Result_ContractMismatch;
    }
    if (!enabled) {
        g_state.store(ModuleState::Disabled);
        LogState(ModuleState::Disabled,
            "all opening-screen features are disabled");
        return BE_Result_Ok;
    }
    return InstallHooks(ConfigurationSnapshot())
        ? BE_Result_Ok : BE_Result_Failed;
}

void BE_CALL Shutdown() {
    StopHooks();
    ReleaseAssets();
    g_state.store(ModuleState::Stopped);
    LogState(ModuleState::Stopped, "module shutdown");
    g_host = nullptr;
}

const BE_ModuleApiV1 kApi{
    {kModuleId, "Login Model", "3.1.1", BETTER_ENDFIELD_MODULE_ABI_V1},
    &Initialize,
    &ConfigurationChanged,
    &Shutdown};

} // namespace
} // namespace BetterEndfield::Model

BE_EXPORT const BE_ModuleApiV1* BE_CALL BetterEndfield_GetModuleApiV1() {
    return &BetterEndfield::Model::kApi;
}
